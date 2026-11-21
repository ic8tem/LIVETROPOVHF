using System.Globalization;
using System.Text;
using TropoVHF.Core;

namespace TropoVHF.Forecast;

/// <summary>
/// Seconda sorgente dello stesso modello GFS 0,25°: il filtro GRIB di NOMADS (NOAA/NCEP). Per ogni ora si chiedono
/// due ritagli del riquadro (temperatura e umidità ai livelli; poi suolo, vento e moto verticale) e si ricavano i profili
/// dei punti della griglia, scritti nello stesso formato JSON di Open-Meteo così che la cache e l'analisi restino uguali.
/// NOMADS blocca chi supera circa 120 richieste al minuto: si fa una richiesta ogni mezzo secondo.
/// </summary>
public static class Nomads
{
    public const string Filter = "https://nomads.ncep.noaa.gov/cgi-bin/filter_gfs_0p25_1hr.pl";
    const int DelayMs = 550;

    static string F(double v) => v.ToString("0.##", CultureInfo.InvariantCulture);

    static string Url(DateTime run, int fh, double top, double bottom, double left, double right, bool upper)
    {
        var sb = new StringBuilder(Filter);
        sb.Append("?dir=%2Fgfs.").Append(run.ToString("yyyyMMdd", CultureInfo.InvariantCulture)).Append("%2F").Append(run.ToString("HH", CultureInfo.InvariantCulture)).Append("%2Fatmos");
        sb.Append("&file=gfs.t").Append(run.ToString("HH", CultureInfo.InvariantCulture)).Append("z.pgrb2.0p25.f").Append(fh.ToString("000", CultureInfo.InvariantCulture));
        if (upper)
        {
            sb.Append("&var_TMP=on&var_RH=on&lev_2_m_above_ground=on");
            foreach (var p in Refractivity.PLevels) sb.Append("&lev_").Append(p).Append("_mb=on");
        }
        else
            sb.Append("&var_PRES=on&var_HGT=on&var_HPBL=on&var_CAPE=on&var_UGRD=on&var_VGRD=on&var_VVEL=on")
              .Append("&lev_surface=on&lev_10_m_above_ground=on&lev_850_mb=on&lev_700_mb=on");
        sb.Append("&subregion=&toplat=").Append(F(top)).Append("&leftlon=").Append(F(left))
          .Append("&rightlon=").Append(F(right)).Append("&bottomlat=").Append(F(bottom));
        return sb.ToString();
    }

    static async Task<byte[]> Get(HttpClient http, string url, CancellationToken ct, FetchStats st)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        for (int attempt = 0; attempt < 3; attempt++)
        {
            try
            {
                using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
                cts.CancelAfter(60000);
                using var resp = await http.GetAsync(url, cts.Token);
                var data = await resp.Content.ReadAsByteArrayAsync();
                st.NetMs += sw.Elapsed.TotalMilliseconds; sw.Restart();
                if (resp.IsSuccessStatusCode && data.Length > 4 && data[0] == 'G' && data[1] == 'R' && data[2] == 'I' && data[3] == 'B')
                { st.Batches++; st.Bytes += data.Length; return data; }
                if ((int)resp.StatusCode == 404 || (resp.IsSuccessStatusCode && Encoding.ASCII.GetString(data, 0, Math.Min(data.Length, 400)).Contains("not present")))
                    return null;                       // file della corsa non (ancora) presente
                if ((int)resp.StatusCode == 429 || (int)resp.StatusCode == 403 || (int)resp.StatusCode == 503)
                {
                    st.Retries++;
                    CalcLog.W($"NOMADS: risposta {(int)resp.StatusCode}, attendo 30 s");
                    await Task.Delay(30000, ct); st.WaitMs += 30000; continue;
                }
                CalcLog.W($"NOMADS: risposta inattesa {(int)resp.StatusCode} ({data.Length} byte)");
                return null;
            }
            catch (OperationCanceledException) when (!ct.IsCancellationRequested)
            {
                st.Retries++; CalcLog.W("NOMADS: tempo scaduto, riprovo");
            }
        }
        throw new IOException("NOMADS non risponde");
    }

    /// <summary>
    /// Profili GFS dei punti da NOMADS. runHint = corsa da provare per prima (quella di Open-Meteo, così i dati coincidono);
    /// se non è presente si prova la corsa precedente. Restituisce il JSON (formato Open-Meteo) per ogni punto e la corsa usata.
    /// </summary>
    public static async Task<(string[] raw, DateTime run)> FetchPoints(HttpClient http, IList<LatLon> pts, int hours, DateTime runHint,
                                                                      Action<string> progress, CancellationToken ct, FetchStats st)
    {
        var raw = new string[pts.Count];
        if (pts.Count == 0) return (raw, runHint);
        double top = pts.Max(p => p.Lat) + 0.25, bottom = pts.Min(p => p.Lat) - 0.25;
        double left = pts.Min(p => p.Lon) - 0.25, right = pts.Max(p => p.Lon) + 0.25;
        var start = DateTime.UtcNow; start = new DateTime(start.Year, start.Month, start.Day, start.Hour, 0, 0, DateTimeKind.Utc);
        DateTime run = runHint != default ? runHint : Floor6(DateTime.UtcNow.AddHours(-4));
        // corsa presente? si prova la prima ora utile
        byte[] first = null;
        for (int tries = 0; tries < 3 && first == null; tries++)
        {
            int fh0 = (int)Math.Round((start - run).TotalHours);
            first = await Get(http, Url(run, Math.Max(0, fh0), top, bottom, left, right, true), ct, st);
            if (first == null) { CalcLog.W($"NOMADS: corsa delle {run:dd/MM HH} UTC non presente, provo la precedente"); run = run.AddHours(-6); await Task.Delay(DelayMs, ct); }
        }
        if (first == null) throw new IOException("NOMADS: nessuna corsa GFS recente disponibile");
        CalcLog.W($"NOMADS: corsa GFS delle {run:dd/MM HH} UTC, riquadro {F(bottom)}–{F(top)} N, {F(left)}–{F(right)} E, {hours} ore");

        int np = pts.Count;
        var times = new List<DateTime>();
        string[] names = BuildNames();
        var series = new Dictionary<string, double[]>[np];
        for (int i = 0; i < np; i++) { series[i] = new Dictionary<string, double[]>(); foreach (var n in names) series[i][n] = new double[hours]; }
        var elev = Enumerable.Repeat(double.NaN, np).ToArray();
        var sw = new System.Diagnostics.Stopwatch();
        for (int k = 0; k < hours; k++)
        {
            ct.ThrowIfCancellationRequested();
            var valid = start.AddHours(k); times.Add(valid);
            int fh = (int)Math.Round((valid - run).TotalHours);
            bool have = fh >= 0 && (fh <= 120 || fh % 3 == 0);
            List<Grib2Field> up = null, lo = null;
            if (have)
            {
                byte[] a = k == 0 ? first : await Get(http, Url(run, fh, top, bottom, left, right, true), ct, st);
                await Task.Delay(DelayMs, ct); st.WaitMs += DelayMs;
                byte[] b = await Get(http, Url(run, fh, top, bottom, left, right, false), ct, st);
                if (k + 1 < hours) { await Task.Delay(DelayMs, ct); st.WaitMs += DelayMs; }
                sw.Restart();
                up = a != null ? Grib2.Read(a) : null; lo = b != null ? Grib2.Read(b) : null;
                st.ParseMs += sw.Elapsed.TotalMilliseconds;
            }
            for (int i = 0; i < np; i++)
            {
                var s = series[i]; double la = pts[i].Lat, lo2 = pts[i].Lon;
                double V(List<Grib2Field> fs, int cat, int num, int lt, double lv)
                {
                    if (fs == null) return double.NaN;
                    var f = fs.FirstOrDefault(x => x.Discipline == 0 && x.Category == cat && x.Number == num && x.LevelType == lt && (lt == 1 || Math.Abs(x.LevelValue - lv) < 0.5));
                    return f?.At(la, lo2) ?? double.NaN;
                }
                double ps = V(lo, 3, 0, 1, 0), hg = V(lo, 3, 5, 1, 0);
                if (!double.IsNaN(hg) && double.IsNaN(elev[i])) elev[i] = hg;
                s["surface_pressure"][k] = ps / 100;
                s["temperature_2m"][k] = V(up, 0, 0, 103, 2) - 273.15;
                s["relative_humidity_2m"][k] = V(up, 1, 1, 103, 2);
                double u10 = V(lo, 2, 2, 103, 10), v10 = V(lo, 2, 3, 103, 10);
                s["wind_speed_10m"][k] = Math.Sqrt(u10 * u10 + v10 * v10);
                s["boundary_layer_height"][k] = V(lo, 3, 196, 1, 0);
                s["cape"][k] = V(lo, 7, 6, 1, 0);
                foreach (var p in Refractivity.PLevels)
                {
                    s[$"temperature_{p}hPa"][k] = V(up, 0, 0, 100, p * 100) - 273.15;
                    s[$"relative_humidity_{p}hPa"][k] = V(up, 1, 1, 100, p * 100);
                }
                double u85 = V(lo, 2, 2, 100, 85000), v85 = V(lo, 2, 3, 100, 85000);
                s["wind_speed_850hPa"][k] = Math.Sqrt(u85 * u85 + v85 * v85);
                s["wind_direction_850hPa"][k] = (Math.Atan2(-u85, -v85) * 180 / Math.PI + 360) % 360;
                // ω (Pa/s, positivo verso il basso) → w (m/s, positivo verso l'alto): w = −ω·R·T/(p·g)
                double t85 = s["temperature_850hPa"][k] + 273.15, t70 = s["temperature_700hPa"][k] + 273.15;
                s["vertical_velocity_850hPa"][k] = -V(lo, 2, 8, 100, 85000) * 287.05 * t85 / (85000 * 9.80665);
                s["vertical_velocity_700hPa"][k] = -V(lo, 2, 8, 100, 70000) * 287.05 * t70 / (70000 * 9.80665);
            }
            progress?.Invoke($"NOMADS: ora {k + 1} / {hours} (corsa {run:HH} UTC)");
        }
        for (int i = 0; i < np; i++)
        {
            if (double.IsNaN(elev[i]) && series[i]["surface_pressure"].All(double.IsNaN)) continue;
            raw[i] = ToJson(pts[i], double.IsNaN(elev[i]) ? 0 : elev[i], times, series[i], names);
        }
        CalcLog.W($"NOMADS: {raw.Count(x => x != null)} punti su {np} ricavati dai ritagli GRIB2 ({st.Bytes / 1024} kB in {st.Batches} file)");
        return (raw, run);
    }

    public static DateTime Floor6(DateTime t) => new DateTime(t.Year, t.Month, t.Day, t.Hour / 6 * 6, 0, 0, DateTimeKind.Utc);

    static string[] BuildNames()
    {
        var v = new List<string> { "surface_pressure", "temperature_2m", "relative_humidity_2m", "wind_speed_10m", "boundary_layer_height", "cape" };
        foreach (var p in Refractivity.PLevels) { v.Add($"temperature_{p}hPa"); v.Add($"relative_humidity_{p}hPa"); }
        v.AddRange(OwnForecast.ExtraVars);
        return v.ToArray();
    }

    static string ToJson(LatLon p, double elev, List<DateTime> times, Dictionary<string, double[]> s, string[] names)
    {
        var ci = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        sb.Append("{\"latitude\":").Append(p.Lat.ToString("0.###", ci)).Append(",\"longitude\":").Append(p.Lon.ToString("0.###", ci))
          .Append(",\"elevation\":").Append(elev.ToString("0.#", ci)).Append(",\"source\":\"nomads\",\"hourly\":{\"time\":[");
        sb.Append(string.Join(",", times.Select(t => "\"" + t.ToString("yyyy-MM-dd'T'HH:mm", ci) + "\"")));
        sb.Append(']');
        foreach (var n in names)
        {
            sb.Append(",\"").Append(n).Append("\":[");
            var a = s[n];
            for (int i = 0; i < times.Count; i++)
            {
                if (i > 0) sb.Append(',');
                double v = i < a.Length ? a[i] : double.NaN;
                sb.Append(double.IsNaN(v) || double.IsInfinity(v) ? "null" : v.ToString("0.###", ci));
            }
            sb.Append(']');
        }
        sb.Append("}}");
        return sb.ToString();
    }
}
