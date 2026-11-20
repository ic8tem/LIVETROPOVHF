using System.Globalization;
using System.Text.Json;
using TropoVHF.Core;

namespace TropoVHF.Forecast;

/// <summary>Un livello del profilo verticale: quota sul mare (m), pressione (hPa), temperatura (°C), umidità (%), N e M.</summary>
public struct Level
{
    public double Z, P, T, RH, N, M;
}

/// <summary>
/// Analisi di un profilo verticale (un punto, un'ora): strati di Bean e Dutton, condotto più forte, guadagno stimato.
/// Le soglie per strato di pressione sono quelle della tabella 6.5 di Bean e Dutton (NBS Monograph 92, 1966, p. 248).
/// </summary>
public sealed class ProfileResult
{
    public double Elev;            // quota del suolo del punto (m s.l.m., quella del modello)
    public double Ns;              // rifrattività al suolo
    public double DN1km;           // variazione di N nel primo km sopra il suolo (N/km, negativa = normale)
    public double LayerGainDb;     // guadagno dal solo gradiente (0..15 dB, −10 con sub-rifrazione)
    public string LayerName = "";  // strato che dà il guadagno
    public double MaxGrad;         // −dN/dh più forte trovato (N/km)
    public bool Sub;               // sub-rifrazione nello strato più basso
    public bool HasDuct;
    public double DuctBase, DuctTop, DuctDeficit;   // m s.l.m. e unità M
    public bool SurfaceDuct;       // la base del condotto è il suolo
    public double Wind10, Blh, Cape;
    public Level[] Levels = Array.Empty<Level>();

    public double DuctThick => HasDuct ? DuctTop - DuctBase : 0;

    /// <summary>Spessore minimo del condotto per una banda (Kerr 1951, eq. 18: λ[cm] = 0,014·d[ft]^1,5).</summary>
    public static double MinDuctM(double freqMHz)
    {
        double lamCm = 29979.2458 / freqMHz;
        return 0.3048 * Math.Pow(lamCm / 0.014, 2.0 / 3.0);
    }

    /// <summary>Guadagno stimato in dB rispetto al profilo normale, per una frequenza (con le correzioni semplici).</summary>
    public double GainDb(double freqMHz)
    {
        double g = LayerGainDb;
        if (HasDuct)
        {
            double dmin = MinDuctM(freqMHz);
            double dg = DuctThick >= dmin ? Math.Min(25, 20 + DuctDeficit / 5.0) : 15;
            g = Math.Max(g, dg);
        }
        // rimescolamento: vento forte al suolo o strato limite molto alto cancellano i condotti al suolo (Bean e Dutton § 4.4)
        if (HasDuct && SurfaceDuct && g > 10)
        {
            if (Wind10 > 8) g = 10 + (g - 10) * 0.4;
            else if (Wind10 > 5) g = 10 + (g - 10) * 0.7;
            if (Blh > 1500) g = 10 + (g - 10) * 0.6;
        }
        // convezione forte: strati rotti
        if (Cape > 1500) g -= 3; else if (Cape > 800) g -= 1.5;
        return g;
    }

    public double Index(double freqMHz) => Math.Max(0, Math.Min(10, GainDb(freqMHz) / 2.5));
}

/// <summary>Fisica del profilo: rifrattività, quote dei livelli, strati e condotti.</summary>
public static class Refractivity
{
    public static readonly int[] PLevels = { 1000, 975, 950, 925, 900, 850, 800, 700, 600, 500 };

    /// <summary>Pressione di saturazione del vapore (hPa) sull'acqua, formula di Magnus (ITU-R P.453).</summary>
    public static double Es(double tC) => 6.1121 * Math.Exp(17.502 * tC / (tC + 240.97));

    /// <summary>N = 77,6/T·(P + 4810·e/T) (ITU-R P.453), T in K, P ed e in hPa.</summary>
    public static double N(double pHpa, double tC, double rh)
    {
        double tk = tC + 273.15;
        double e = Math.Max(0, Math.Min(100, rh)) / 100.0 * Es(tC);
        return 77.6 / tk * (pHpa + 4810.0 * e / tk);
    }

    /// <summary>Soglie di Bean e Dutton (tab. 6.5): limite alto del profilo normale e soglia di super-rifrazione (N/km).</summary>
    static (string name, double pTop, double normHi, double superThr) LayerFor(double pMid)
    {
        if (pMid >= 850) return ("1000–850 hPa", 850, 60, 100);
        if (pMid >= 700) return ("850–700 hPa", 700, 50, 80);
        if (pMid >= 600) return ("700–600 hPa", 600, 40, 70);
        return ("600–500 hPa", 500, 30, 50);
    }

    /// <summary>
    /// Costruisce il profilo dal suolo a 500 hPa: quote con l'equazione ipsometrica (temperatura virtuale) a partire
    /// dalla quota e dalla pressione al suolo, e analizza strati e condotti.
    /// </summary>
    public static ProfileResult Analyze(double elev, double ps, double t2, double rh2, double[] tLev, double[] rhLev,
                                        double wind10, double blh, double cape)
    {
        var r = new ProfileResult { Elev = elev, Wind10 = wind10, Blh = blh, Cape = cape };
        if (double.IsNaN(ps) || double.IsNaN(t2) || double.IsNaN(rh2)) return null;
        var lev = new List<Level>();
        double z0 = elev + 2;
        var s = new Level { Z = z0, P = ps, T = t2, RH = rh2 };
        s.N = N(s.P, s.T, s.RH); s.M = s.N + 157 * s.Z / 1000.0;
        lev.Add(s);
        for (int i = 0; i < PLevels.Length; i++)
        {
            double p = PLevels[i];
            if (p >= ps - 2) continue;                 // livello sotto il suolo (o troppo vicino)
            if (double.IsNaN(tLev[i]) || double.IsNaN(rhLev[i])) continue;
            var prev = lev[lev.Count - 1];
            double tv1 = Tv(prev.T, prev.RH, prev.P), tv2 = Tv(tLev[i], rhLev[i], p);
            double z = prev.Z + 287.05 * (tv1 + tv2) / 2 / 9.80665 * Math.Log(prev.P / p);
            var l = new Level { Z = z, P = p, T = tLev[i], RH = rhLev[i] };
            l.N = N(p, l.T, l.RH); l.M = l.N + 157 * l.Z / 1000.0;
            lev.Add(l);
        }
        if (lev.Count < 3) return null;
        r.Levels = lev.ToArray();
        r.Ns = lev[0].N;
        r.DN1km = Interp(lev, z0 + 1000) - lev[0].N;

        // ---- strati di Bean e Dutton: gradiente più forte fra livelli vicini, classificato con le soglie dello strato
        double best = 0; string bestName = "";
        double maxGrad = double.MinValue;
        for (int i = 1; i < lev.Count; i++)
        {
            double dz = (lev[i].Z - lev[i - 1].Z) / 1000.0;
            if (dz <= 0.01) continue;
            double g = -(lev[i].N - lev[i - 1].N) / dz;           // −dN/dh in N/km
            if (g > maxGrad) maxGrad = g;
            var (name, _, normHi, superThr) = LayerFor((lev[i].P + lev[i - 1].P) / 2);
            double gain;
            if (g <= normHi) gain = 0;
            else if (g < superThr) gain = 10 * (g - normHi) / (superThr - normHi);
            else gain = 10 + 5 * Math.Min(1, (g - superThr) / Math.Max(1, 157 - superThr));
            if (gain > best) { best = gain; bestName = name; }
        }
        r.MaxGrad = maxGrad;
        // sub-rifrazione: N cresce con la quota nel primo strato sopra il suolo
        if (lev.Count > 1 && lev[1].N > lev[0].N && best < 1) { r.Sub = true; best = -10; bestName = Lang("sub"); }
        r.LayerGainDb = best; r.LayerName = bestName;

        // ---- condotto: strato in cui M cala con la quota (dM/dh < 0); si prende quello con il deficit maggiore
        for (int i = 1; i < lev.Count; i++)
        {
            if (lev[i].M >= lev[i - 1].M) continue;
            // cima: fine del tratto con M in calo
            int top = i;
            while (top + 1 < lev.Count && lev[top + 1].M < lev[top].M) top++;
            double mTop = lev[top].M;
            // massimo di M sotto la cima nel tratto in calo
            double mMax = lev[i - 1].M;
            // base: il punto più in basso (sotto la cima) dove M torna uguale a M della cima
            double baseZ = lev[0].Z; bool surf = true;
            for (int k = i - 1; k > 0; k--)
            {
                if (lev[k - 1].M <= mTop)
                {
                    double f = (mTop - lev[k - 1].M) / Math.Max(1e-9, lev[k].M - lev[k - 1].M);
                    baseZ = lev[k - 1].Z + f * (lev[k].Z - lev[k - 1].Z);
                    surf = false;
                    break;
                }
            }
            double deficit = mMax - mTop;
            if (!r.HasDuct || deficit > r.DuctDeficit)
            {
                r.HasDuct = true; r.DuctBase = baseZ; r.DuctTop = lev[top].Z; r.DuctDeficit = deficit; r.SurfaceDuct = surf;
            }
            i = top;
        }
        return r;
    }

    static string Lang(string k) => k == "sub" ? "sub-rifrazione" : k;

    static double Tv(double tC, double rh, double p)
    {
        double e = Math.Max(0, Math.Min(100, rh)) / 100.0 * Es(tC);
        double q = 0.622 * e / Math.Max(1, p - 0.378 * e);
        return (tC + 273.15) * (1 + 0.61 * q);
    }

    static double Interp(List<Level> lev, double z)
    {
        for (int i = 1; i < lev.Count; i++)
            if (lev[i].Z >= z)
            {
                double f = (z - lev[i - 1].Z) / Math.Max(1e-6, lev[i].Z - lev[i - 1].Z);
                return lev[i - 1].N + f * (lev[i].N - lev[i - 1].N);
            }
        return lev[lev.Count - 1].N;
    }
}

/// <summary>
/// Previsione propria su una griglia: profili GFS (Open-Meteo) dal suolo a 500 hPa, ogni ora per 78 ore.
/// Per contenere le richieste a Open-Meteo la quota dei livelli si ricava con l'equazione ipsometrica invece di scaricarla.
/// </summary>
public sealed class OwnForecast
{
    public double LatMin, LatMax, LonMin, LonMax, Step;
    public int Rows, Cols;
    public DateTime[] Times = Array.Empty<DateTime>();
    /// <summary>[ora][riga, colonna]: profilo analizzato o null.</summary>
    public ProfileResult[][,] Grid = Array.Empty<ProfileResult[,]>();
    public string Status = "";
    public DateTime UpdatedUtc;
    public bool Busy;
    readonly object _lock = new();

    public const int Hours = 78;
    static readonly string[] SurfVars = { "surface_pressure", "temperature_2m", "relative_humidity_2m", "wind_speed_10m", "boundary_layer_height", "cape" };

    public static string HourlyVars()
    {
        var v = new List<string>(SurfVars);
        foreach (var p in Refractivity.PLevels) { v.Add($"temperature_{p}hPa"); v.Add($"relative_humidity_{p}hPa"); }
        return string.Join(",", v);
    }

    public sealed class PointSeries
    {
        public double Lat, Lon, Elev;
        public DateTime[] Times;
        public ProfileResult[] Profiles;
    }

    /// <summary>Scarica e analizza una lista di punti (al massimo 80 per richiesta, con una pausa fra le richieste).</summary>
    public static async Task<List<PointSeries>> FetchPoints(HttpClient http, IList<LatLon> pts, Action<string> progress, CancellationToken ct)
    {
        var res = new List<PointSeries>();
        const int batch = 60;
        var vars = HourlyVars();
        for (int i = 0; i < pts.Count; i += batch)
        {
            var part = pts.Skip(i).Take(batch).ToList();
            var lat = string.Join(",", part.Select(p => p.Lat.ToString("0.###", CultureInfo.InvariantCulture)));
            var lon = string.Join(",", part.Select(p => p.Lon.ToString("0.###", CultureInfo.InvariantCulture)));
            var url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&hourly={vars}&models=gfs_seamless&forecast_hours={Hours}&wind_speed_unit=ms&timezone=GMT";
            string json = null;
            for (int attempt = 0; attempt < 3 && json == null; attempt++)
            {
                using var resp = await http.GetAsync(url, ct);
                if ((int)resp.StatusCode == 429) { progress?.Invoke("Open-Meteo: limite al minuto, attendo 60 s…"); await Task.Delay(60000, ct); continue; }
                resp.EnsureSuccessStatusCode();
                json = await resp.Content.ReadAsStringAsync(ct);
            }
            if (json == null) throw new Exception("Open-Meteo: troppe richieste");
            using (var doc = JsonDocument.Parse(json))
            {
                var items = doc.RootElement.ValueKind == JsonValueKind.Array ? doc.RootElement.EnumerateArray().ToList() : new List<JsonElement> { doc.RootElement };
                for (int k = 0; k < items.Count && k < part.Count; k++)
                {
                    var it = items[k];
                    var h = it.GetProperty("hourly");
                    var times = h.GetProperty("time").EnumerateArray()
                        .Select(t => DateTime.SpecifyKind(DateTime.Parse(t.GetString(), CultureInfo.InvariantCulture), DateTimeKind.Utc)).ToArray();
                    double elev = it.TryGetProperty("elevation", out var ev) && ev.ValueKind == JsonValueKind.Number ? ev.GetDouble() : 0;
                    double[] A(string name) => h.TryGetProperty(name, out var a)
                        ? a.EnumerateArray().Select(x => x.ValueKind == JsonValueKind.Number ? x.GetDouble() : double.NaN).ToArray()
                        : Enumerable.Repeat(double.NaN, times.Length).ToArray();
                    var ps = A("surface_pressure"); var t2 = A("temperature_2m"); var rh2 = A("relative_humidity_2m");
                    var w = A("wind_speed_10m"); var blh = A("boundary_layer_height"); var cape = A("cape");
                    var tl = Refractivity.PLevels.Select(p => A($"temperature_{p}hPa")).ToArray();
                    var rl = Refractivity.PLevels.Select(p => A($"relative_humidity_{p}hPa")).ToArray();
                    var prof = new ProfileResult[times.Length];
                    for (int t = 0; t < times.Length; t++)
                    {
                        var tt = new double[tl.Length]; var rr = new double[rl.Length];
                        for (int j = 0; j < tl.Length; j++) { tt[j] = t < tl[j].Length ? tl[j][t] : double.NaN; rr[j] = t < rl[j].Length ? rl[j][t] : double.NaN; }
                        prof[t] = Refractivity.Analyze(elev, At(ps, t), At(t2, t), At(rh2, t), tt, rr, At(w, t), At(blh, t), At(cape, t));
                    }
                    res.Add(new PointSeries { Lat = part[k].Lat, Lon = part[k].Lon, Elev = elev, Times = times, Profiles = prof });
                }
            }
            progress?.Invoke($"GFS: {Math.Min(pts.Count, i + batch)} / {pts.Count} punti");
            if (i + batch < pts.Count) await Task.Delay(20000, ct);   // circa 160 richieste equivalenti per lotto: si resta sotto 600 al minuto
        }
        return res;
    }

    static double At(double[] a, int t) => t < a.Length ? a[t] : double.NaN;

    /// <summary>Griglia sul riquadro dato, con un passo scelto per restare sotto circa 240 punti.</summary>
    public async Task<bool> UpdateAsync(HttpClient http, double la1, double la2, double lo1, double lo2, Action changed, CancellationToken ct)
    {
        if (Busy) return false;
        Busy = true;
        try
        {
            la1 = Math.Max(-70, la1); la2 = Math.Min(75, la2); lo1 = Math.Max(-180, lo1); lo2 = Math.Min(180, lo2);
            double step = 0.5;
            foreach (var s in new[] { 0.5, 0.75, 1.0, 1.25, 1.5, 2.0, 2.5, 3.0, 4.0, 5.0 })
            {
                step = s;
                if ((Math.Floor((la2 - la1) / s) + 2) * (Math.Floor((lo2 - lo1) / s) + 2) <= 240) break;
            }
            double latMin = Math.Floor(la1 / step) * step, lonMin = Math.Floor(lo1 / step) * step;
            int rows = (int)Math.Ceiling((la2 - latMin) / step) + 1, cols = (int)Math.Ceiling((lo2 - lonMin) / step) + 1;
            var pts = new List<LatLon>();
            for (int r = 0; r < rows; r++) for (int c = 0; c < cols; c++) pts.Add(new LatLon(latMin + r * step, lonMin + c * step));
            Status = $"GFS: scarico {pts.Count} punti (passo {step:0.##}°)…"; changed?.Invoke();
            var series = await FetchPoints(http, pts, m => { Status = m; changed?.Invoke(); }, ct);
            var times = series.FirstOrDefault(s => s.Times != null && s.Times.Length > 0)?.Times ?? Array.Empty<DateTime>();
            var grid = new ProfileResult[times.Length][,];
            for (int t = 0; t < times.Length; t++) grid[t] = new ProfileResult[rows, cols];
            for (int i = 0; i < series.Count && i < pts.Count; i++)
            {
                int r = i / cols, c = i % cols;
                for (int t = 0; t < times.Length && t < series[i].Profiles.Length; t++) grid[t][r, c] = series[i].Profiles[t];
            }
            lock (_lock)
            {
                LatMin = latMin; LonMin = lonMin; LatMax = latMin + (rows - 1) * step; LonMax = lonMin + (cols - 1) * step;
                Step = step; Rows = rows; Cols = cols; Times = times; Grid = grid;
            }
            UpdatedUtc = DateTime.UtcNow;
            Status = $"Previsione propria: GFS {UpdatedUtc:dd/MM HH:mm} UTC, {pts.Count} punti, passo {step:0.##}°";
            return true;
        }
        catch (OperationCanceledException) { Status = "Previsione propria: interrotta"; return false; }
        catch (Exception ex) { Status = "Previsione propria non riuscita: " + ex.Message; return false; }
        finally { Busy = false; changed?.Invoke(); }
    }

    int TimeIndex(DateTime utc)
    {
        if (Times.Length == 0) return -1;
        int best = 0; double bd = double.MaxValue;
        for (int i = 0; i < Times.Length; i++) { double d = Math.Abs((Times[i] - utc).TotalMinutes); if (d < bd) { bd = d; best = i; } }
        return bd <= 90 ? best : -1;
    }

    /// <summary>Profilo della cella più vicina (null fuori dalla griglia o senza dati).</summary>
    public ProfileResult At(double lat, double lon, DateTime utc)
    {
        lock (_lock)
        {
            int t = TimeIndex(utc);
            if (t < 0 || Rows == 0) return null;
            double fr = (lat - LatMin) / Step, fc = (lon - LonMin) / Step;
            if (fr < -0.5 || fc < -0.5 || fr > Rows - 0.5 || fc > Cols - 0.5) return null;
            int r = (int)Math.Round(fr), c = (int)Math.Round(fc);
            r = Math.Max(0, Math.Min(Rows - 1, r)); c = Math.Max(0, Math.Min(Cols - 1, c));
            return Grid[t][r, c];
        }
    }

    /// <summary>Indice 0–10 interpolato fra le quattro celle vicine; NaN dove non ci sono dati.</summary>
    public double Index(double lat, double lon, DateTime utc, double freqMHz)
    {
        lock (_lock)
        {
            int t = TimeIndex(utc);
            if (t < 0 || Rows < 2 || Cols < 2) return double.NaN;
            double fr = (lat - LatMin) / Step, fc = (lon - LonMin) / Step;
            if (fr < 0 || fc < 0 || fr > Rows - 1 || fc > Cols - 1) return double.NaN;
            int r0 = Math.Min(Rows - 2, (int)fr), c0 = Math.Min(Cols - 2, (int)fc);
            double tr = fr - r0, tc = fc - c0, sum = 0, wsum = 0;
            void Add(int r, int c, double w)
            {
                var p = Grid[t][r, c];
                if (p == null || w <= 0) return;
                sum += w * p.Index(freqMHz); wsum += w;
            }
            Add(r0, c0, (1 - tr) * (1 - tc)); Add(r0, c0 + 1, (1 - tr) * tc); Add(r0 + 1, c0, tr * (1 - tc)); Add(r0 + 1, c0 + 1, tr * tc);
            return wsum < 0.25 ? double.NaN : sum / wsum;
        }
    }

    public bool HasData { get { lock (_lock) return Rows > 0 && Times.Length > 0; } }
}
