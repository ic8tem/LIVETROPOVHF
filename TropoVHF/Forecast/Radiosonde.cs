using System.Globalization;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using TropoVHF.Core;

namespace TropoVHF.Forecast;

/// <summary>Un radiosondaggio con le grandezze che contano per la propagazione.</summary>
public sealed class Sounding
{
    public DateTime Utc; public double Lat, Lon, Elev;
    public double Ns = double.NaN, DN1km = double.NaN;     // rifrattività al suolo e variazione nel primo km (N/km)
    public bool HasDuct; public double DuctBase, DuctTop, DuctDeficit;
    public int Levels;
}

/// <summary>Confronto fra radiosondaggio e GFS alla stessa ora.</summary>
public sealed class SondeCheck
{
    public Sounding Obs; public ProfileResult Gfs;
    public double DnErr => Gfs == null || double.IsNaN(Obs.DN1km) ? double.NaN : Gfs.DN1km - Obs.DN1km;
}

/// <summary>Esito della verifica con la stazione di radiosondaggio più vicina.</summary>
public sealed class SondeReport
{
    public string StationId = "", StationName = ""; public double Lat, Lon, Km;
    public List<SondeCheck> Checks = new();
    public int Hits, Misses, FalseAlarms, BothNo;
    public double DnBias = double.NaN, DnRms = double.NaN;
    public string Error = "";
    public DateTime Made;
    /// <summary>Punteggio 0–1 della capacità del GFS di vedere i condotti qui (indice di Heidke semplificato in accordo percentuale).</summary>
    public double Skill
    {
        get
        {
            int n = Hits + Misses + FalseAlarms + BothNo;
            if (n < 6) return double.NaN;
            return (Hits + BothNo) / (double)n;
        }
    }
}

/// <summary>
/// 1.4 — Verifica dei profili GFS con i radiosondaggi reali dell'archivio IGRA v2 della NOAA (NCEI, dominio pubblico):
/// la stazione più vicina al QTH, gli ultimi giorni di lanci alle 00 e alle 12 UTC (il file «y2d» si aggiorna ogni pochi
/// giorni). Per ogni lancio si ricavano dal profilo osservato, ad alta risoluzione, la rifrattività al suolo, il gradiente del
/// primo km e i condotti (M che cala con la quota sotto i 3000 m); dal GFS d'archivio (Open-Meteo, previsioni storiche)
/// gli stessi valori con l'analisi del programma. Ne escono lo scarto medio del gradiente e quante volte il GFS ha visto o
/// mancato un condotto osservato: entra nell'affidabilità della previsione (manuale § 10 septies).
/// </summary>
public static class Radiosonde
{
    const string Base = "https://www.ncei.noaa.gov/data/integrated-global-radiosonde-archive/";
    static readonly CultureInfo Ci = CultureInfo.InvariantCulture;
    public static SondeReport Last;
    static string Folder { get { var d = Path.Combine(Settings.DataDir, "IGRA"); try { Directory.CreateDirectory(d); } catch { } return d; } }

    public static async Task<SondeReport> Check(HttpClient http, LatLon qth, int days, CancellationToken ct, Action<string> progress = null)
    {
        var rep = new SondeReport { Made = DateTime.UtcNow };
        try
        {
            progress?.Invoke("IGRA: elenco delle stazioni…");
            var st = await Nearest(http, qth, ct);
            if (st.id == null) { rep.Error = "nessuna stazione di radiosondaggio attiva entro 600 km"; return Last = rep; }
            rep.StationId = st.id; rep.StationName = st.name; rep.Lat = st.lat; rep.Lon = st.lon; rep.Km = Geo.DistKm(qth, new LatLon(st.lat, st.lon));
            progress?.Invoke($"IGRA: radiosondaggi di {st.name}…");
            var obs = await Soundings(http, st.id, ct);
            var since = DateTime.UtcNow.AddDays(-days);
            obs = obs.Where(o => o.Utc >= since && (o.Utc.Hour == 0 || o.Utc.Hour == 12)).OrderBy(o => o.Utc).ToList();
            if (obs.Count == 0) { rep.Error = "nessun lancio negli ultimi giorni nell'archivio IGRA (si aggiorna ogni pochi giorni)"; return Last = rep; }
            progress?.Invoke($"IGRA: GFS d'archivio per {obs.Count} lanci…");
            var gfs = await GfsAt(http, st.lat, st.lon, obs.First().Utc.Date, obs.Last().Utc.Date, ct);
            foreach (var o in obs)
            {
                ProfileResult g = null;
                if (gfs != null) for (int i = 0; i < gfs.Times.Length; i++) if (gfs.Times[i] == o.Utc) { g = gfs.Profiles[i]; break; }
                rep.Checks.Add(new SondeCheck { Obs = o, Gfs = g });
            }
            var both = rep.Checks.Where(c => c.Gfs != null).ToList();
            foreach (var c in both)
            {
                bool o = c.Obs.HasDuct, m = c.Gfs.HasDuct;
                if (o && m) rep.Hits++; else if (o) rep.Misses++; else if (m) rep.FalseAlarms++; else rep.BothNo++;
            }
            var errs = both.Select(c => c.DnErr).Where(x => !double.IsNaN(x)).ToList();
            if (errs.Count > 0) { rep.DnBias = errs.Average(); rep.DnRms = Math.Sqrt(errs.Average(x => x * x)); }
            CalcLog.W($"verifica IGRA: {st.name} ({st.id}) a {rep.Km:0} km, {obs.Count} lanci, {both.Count} con GFS; condotti visti {rep.Hits}, mancati {rep.Misses}, falsi {rep.FalseAlarms}, nessuno {rep.BothNo}; " +
                      $"scarto del gradiente GFS − osservato {rep.DnBias:+0.0;-0.0} N/km (RMS {rep.DnRms:0.0})");
        }
        catch (OperationCanceledException) { throw; }
        catch (Exception ex) { rep.Error = ex.Message; }
        return Last = rep;
    }

    static double StationElev = double.NaN;

    static async Task<(string id, string name, double lat, double lon)> Nearest(HttpClient http, LatLon q, CancellationToken ct)
    {
        var f = Path.Combine(Folder, "igra2-station-list.txt");
        if (!File.Exists(f) || (DateTime.UtcNow - File.GetLastWriteTimeUtc(f)).TotalDays > 30)
        {
            var b = await http.GetByteArrayAsync(Base + "doc/igra2-station-list.txt");
            File.WriteAllBytes(f, b);
        }
        int year = DateTime.UtcNow.Year;
        (string, string, double, double) best = (null, null, 0, 0); double bestKm = 600;
        foreach (var l in File.ReadAllLines(f))
        {
            if (l.Length < 81) continue;
            if (!double.TryParse(l.Substring(12, 8), NumberStyles.Float, Ci, out var la) || !double.TryParse(l.Substring(21, 9), NumberStyles.Float, Ci, out var lo)) continue;
            if (!int.TryParse(l.Substring(77, 4), out var last) || last < year - 1) continue;
            double km = Geo.DistKm(q, new LatLon(la, lo));
            if (km < bestKm) { bestKm = km; StationElev = double.TryParse(l.Substring(31, 6), NumberStyles.Float, Ci, out var el) && el > -998 ? el : double.NaN; best = (l.Substring(0, 11).Trim(), l.Substring(41, Math.Min(30, l.Length - 41)).Trim(), la, lo); }
        }
        return best;
    }

    static async Task<List<Sounding>> Soundings(HttpClient http, string id, CancellationToken ct)
    {
        int year = DateTime.UtcNow.Year;
        var f = Path.Combine(Folder, id + ".zip");
        if (!File.Exists(f) || (DateTime.UtcNow - File.GetLastWriteTimeUtc(f)).TotalHours > 12)
        {
            byte[] b = null;
            foreach (var y in new[] { year, year - 1 })
            {
                try { b = await http.GetByteArrayAsync(Base + $"access/data-y2d/{id}-data-beg{y}.txt.zip"); break; } catch { }
            }
            if (b == null) throw new Exception("archivio IGRA della stazione non disponibile");
            File.WriteAllBytes(f, b);
        }
        using var zip = ZipFile.OpenRead(f);
        var e = zip.Entries.First();
        using var rd = new StreamReader(e.Open(), Encoding.ASCII);
        return Parse(rd, StationElev);
    }

    /// <summary>Formato IGRA v2 (colonne fisse): intestazioni con «#», livelli con pressione (Pa), quota geopotenziale (m),
    /// temperatura e umidità in decimi, depressione del punto di rugiada in decimi di °C. I messaggi GTS (TEMP) danno la quota
    /// solo sui livelli standard: le altre si ricavano con l'equazione ipsometrica partendo dalla quota della stazione.</summary>
    public static List<Sounding> Parse(TextReader rd, double stationElev = double.NaN)
    {
        var res = new List<Sounding>();
        Sounding cur = null; var lev = new List<Lv>();
        void Flush() { if (cur != null) { Finish(cur, lev, stationElev); if (cur.Levels >= 5) res.Add(cur); } lev.Clear(); }
        string l;
        while ((l = rd.ReadLine()) != null)
        {
            if (l.StartsWith("#"))
            {
                Flush(); cur = null;
                if (l.Length < 26) continue;
                if (!int.TryParse(l.Substring(13, 4), out var Y) || !int.TryParse(l.Substring(18, 2), out var Mo) || !int.TryParse(l.Substring(21, 2), out var D) || !int.TryParse(l.Substring(24, 2), out var H) || H > 23) continue;
                cur = new Sounding { Utc = new DateTime(Y, Mo, D, H, 0, 0, DateTimeKind.Utc) };
                if (l.Length >= 71 && double.TryParse(l.Substring(55, 7), NumberStyles.Float, Ci, out var la) && double.TryParse(l.Substring(63, 8), NumberStyles.Float, Ci, out var lo)) { cur.Lat = la / 10000; cur.Lon = lo / 10000; }
                continue;
            }
            if (cur == null || l.Length < 39) continue;
            int I(int s, int n) => int.TryParse(l.Substring(s, n).Trim(), out var v) ? v : -9999;
            int p = I(9, 6), z = I(16, 5), t = I(22, 5), rh = I(28, 5), dpd = I(34, 5);
            if (p <= 0 || t <= -8888) continue;
            double P = p / 100.0, T = t / 10.0, e = double.NaN;
            double es = 6.112 * Math.Exp(17.62 * T / (243.12 + T));
            if (rh >= 0) e = es * rh / 1000.0;
            else if (dpd >= 0) { double Td = T - dpd / 10.0; e = 6.112 * Math.Exp(17.62 * Td / (243.12 + Td)); }
            lev.Add(new Lv { P = P, T = T, E = e, Z = z > -8888 ? z : double.NaN, Surf = l[1] == '1' });
        }
        Flush();
        return res;
    }

    struct Lv { public double P, T, E, Z, N; public bool Surf; }

    static void Finish(Sounding s, List<Lv> raw, double stationElev)
    {
        // dal basso verso l'alto (pressione che cala), un livello per pressione
        var v = raw.OrderByDescending(x => x.P).GroupBy(x => x.P).Select(g => g.OrderByDescending(x => double.IsNaN(x.E) ? 0 : 1).First()).ToList();
        int si = v.FindIndex(x => x.Surf); if (si > 0) v.RemoveRange(0, si);       // niente livelli «sotto terra»
        if (v.Count < 5) { s.Levels = 0; return; }
        // quote: ipsometrica (Rd/g = 29,27 m/K, temperatura virtuale), riallineata sulle quote geopotenziali dichiarate
        var a = v.ToArray();
        double z0 = !double.IsNaN(a[0].Z) ? a[0].Z : !double.IsNaN(stationElev) ? stationElev : double.NaN;
        if (double.IsNaN(z0)) { s.Levels = 0; return; }
        a[0].Z = z0;
        for (int i = 1; i < a.Length; i++)
        {
            if (!double.IsNaN(a[i].Z) && i > 0) continue;
            double Tv(Lv x) { double ee = double.IsNaN(x.E) ? 0 : x.E; return (x.T + 273.15) * (1 + 0.378 * ee / x.P); }
            a[i].Z = a[i - 1].Z + 29.27 * 0.5 * (Tv(a[i - 1]) + Tv(a[i])) * Math.Log(a[i - 1].P / a[i].P);
        }
        var w = new List<(double z, double n)>();
        foreach (var x in a)
        {
            if (double.IsNaN(x.E)) continue;
            double Tk = x.T + 273.15;
            w.Add((x.Z, 77.6 / Tk * (x.P + 4810 * x.E / Tk)));
        }
        s.Levels = w.Count;
        if (w.Count < 5 || w[0].z > z0 + 50) { s.Levels = 0; return; }   // serve l'umidità al suolo
        s.Elev = z0; s.Ns = w[0].n;
        double z1 = z0 + 1000;
        for (int i = 1; i < w.Count; i++)
            if (w[i].z >= z1 && w[i - 1].z <= z1)
            {
                double n1 = w[i - 1].n + (w[i].n - w[i - 1].n) * (z1 - w[i - 1].z) / Math.Max(1, w[i].z - w[i - 1].z);
                s.DN1km = n1 - s.Ns; break;
            }
        // condotti: M = N + 0,157·z che cala con la quota, sotto i 3000 m dal suolo; si tiene il calo più forte
        double best = 0;
        for (int i = 0; i < w.Count && w[i].z < z0 + 3000; i++)
        {
            double mTop = w[i].n + 0.157 * w[i].z;
            // cima di un tratto in cui M cala: cerco il minimo successivo prima che M torni a crescere oltre la cima
            double mMin = mTop; int jMin = i;
            for (int j = i + 1; j < w.Count && w[j].z < z0 + 3500; j++)
            {
                double m = w[j].n + 0.157 * w[j].z;
                if (m < mMin) { mMin = m; jMin = j; }
                if (m > mTop) break;
            }
            // il condotto va dalla quota i (dove M è massimo) alla jMin (minimo): lo strato «trappola» è jMin sopra i
            if (jMin > i && mTop - mMin > best) { best = mTop - mMin; s.DuctBase = w[i].z; s.DuctTop = w[jMin].z; }
        }
        s.DuctDeficit = best;
        s.HasDuct = best >= 2;      // salti di un paio di unità M: rumore della sonda
    }

    static async Task<OwnForecast.PointSeries> GfsAt(HttpClient http, double lat, double lon, DateTime d0, DateTime d1, CancellationToken ct)
    {
        string url = "https://historical-forecast-api.open-meteo.com/v1/forecast?latitude=" + lat.ToString("0.###", Ci) + "&longitude=" + lon.ToString("0.###", Ci) +
                     "&hourly=" + OwnForecast.HourlyVars() + "&models=gfs_seamless&start_date=" + d0.ToString("yyyy-MM-dd") + "&end_date=" + d1.ToString("yyyy-MM-dd") + "&wind_speed_unit=ms&timezone=GMT";
        using var resp = await http.GetAsync(url, ct);
        if (!resp.IsSuccessStatusCode) { CalcLog.W($"verifica IGRA: Open-Meteo previsioni storiche risponde {(int)resp.StatusCode}"); return null; }
        var json = await Compat.ReadString(resp.Content, ct);
        using var doc = JsonDocument.Parse(json);
        return OwnForecast.ParsePoint(doc.RootElement, new LatLon(lat, lon));
    }
}
