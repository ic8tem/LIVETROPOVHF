// LIVETROPOVHF — archivio di addestramento e modello statistico delle aperture tropo (1.4, sperimentale).
//
// Etichette: archivio pubblico degli spot WSPR (wspr.live, tutti gli spot inviati a wsprnet.org; uso consentito per
// progetti i cui risultati sono gratuiti per tutti, niente uso commerciale). I beacon WSPR trasmettono a orario fisso:
// se in un'ora un trasmettitore è attivo (qualcuno lo riceve) e un ricevitore è attivo (invia spot sui 2 m) ma la coppia
// non c'è, quella è una vera «non apertura», non mancanza di attività.
// Meteo: GFS d'archivio di Open-Meteo (previsioni storiche, CC BY 4.0), analizzato con lo stesso codice del programma.
//
//   dotnet run -c Release -- wspr <AAAA-MM> <AAAA-MM> <cartella>          spot e attività per mese (2 m)
//   dotnet run -c Release -- celle <cartella> <minimo>                     celle di 1° attraversate dalle tratte
//   dotnet run -c Release -- gfs <cartella> <budget chiamate>              profili GFS per cella e mese (riprende da dove era)
//   dotnet run -c Release -- dataset <cartella>                            tabella delle tratte con le grandezze fisiche
//   dotnet run -c Release -- addestra <cartella>                           LightGBM, prova per regioni lasciate fuori
using System.Globalization;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using Microsoft.ML;
using Microsoft.ML.Data;
using TropoVHF.Core;
using TropoVHF.Forecast;

static class Prog
{
    static readonly CultureInfo Ci = CultureInfo.InvariantCulture;
    static readonly HttpClient Http = MakeHttp();
    static HttpClient MakeHttp()
    {
        var h = new HttpClient { Timeout = TimeSpan.FromMinutes(5) };
        h.DefaultRequestHeaders.UserAgent.ParseAdd("LIVETROPOVHF-addestramento/1.4 (+https://github.com/ic8tem/LIVETROPOVHF)");
        return h;
    }

    static async Task<int> Main(string[] a)
    {
        CultureInfo.DefaultThreadCurrentCulture = Ci; Thread.CurrentThread.CurrentCulture = Ci;
        if (a.Length < 2) { Console.WriteLine("uso: wspr|celle|gfs|dataset|addestra ..."); return 1; }
        switch (a[0])
        {
            case "wspr": await Wspr(a[1], a[2], a[3]); break;
            case "celle": Celle(a[1], int.Parse(a[2])); break;
            case "gfs": return await Gfs(a[1], int.Parse(a[2]));
            case "dataset": Dataset(a[1]); break;
            case "addestra": Addestra(a[1]); break;
            default: return 1;
        }
        return 0;
    }

    // ───────────────────────── 1. WSPR ─────────────────────────
    static async Task<string> Sql(string q)
    {
        for (int k = 0; ; k++)
        {
            try
            {
                using var r = await Http.GetAsync("https://db1.wspr.live/?query=" + Uri.EscapeDataString(q + " FORMAT TSV"));
                var s = await r.Content.ReadAsStringAsync();
                if (!r.IsSuccessStatusCode) throw new Exception($"wspr.live {(int)r.StatusCode}: {s[..Math.Min(200, s.Length)]}");
                return s;
            }
            catch when (k < 4) { await Task.Delay(TimeSpan.FromSeconds(20 * (k + 1))); }
        }
    }

    static void WriteGz(string path, string text)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        using var f = File.Create(path); using var gz = new GZipStream(f, CompressionLevel.SmallestSize); using var w = new StreamWriter(gz, new UTF8Encoding(false));
        w.Write(text);
    }
    static IEnumerable<string[]> ReadGz(string path)
    {
        using var f = File.OpenRead(path); using var gz = new GZipStream(f, CompressionMode.Decompress); using var r = new StreamReader(gz);
        string l; while ((l = r.ReadLine()) != null) if (l.Length > 0 && l[0] != '#') yield return l.Split('\t');
    }

    static async Task Wspr(string from, string to, string dir)
    {
        var m = DateTime.ParseExact(from, "yyyy-MM", Ci); var end = DateTime.ParseExact(to, "yyyy-MM", Ci);
        for (; m <= end; m = m.AddMonths(1))
        {
            string tag = m.ToString("yyyy-MM"), t0 = m.ToString("yyyy-MM-dd"), t1 = m.AddMonths(1).ToString("yyyy-MM-dd");
            var fs = Path.Combine(dir, "wspr", tag + ".spot.tsv.gz");
            if (File.Exists(fs)) { Console.WriteLine($"{tag}: già presente"); continue; }
            // coppie per ora (spot ripetuti e doppioni accorpati): massimo SNR e numero di spot
            var spot = await Sql($@"SELECT toStartOfHour(time) h, tx_sign, round(any(tx_lat),3), round(any(tx_lon),3), rx_sign, round(any(rx_lat),3), round(any(rx_lon),3),
                any(distance), max(snr), count() FROM wspr.rx WHERE band=144 AND time>='{t0}' AND time<'{t1}' AND distance BETWEEN 30 AND 4000
                GROUP BY h, tx_sign, rx_sign ORDER BY h");
            // attività: chi trasmette (ricevuto da qualcuno, anche vicino) e chi ascolta (invia spot) in quell'ora sui 2 m
            var txa = await Sql($@"SELECT toStartOfHour(time) h, tx_sign, round(any(tx_lat),3), round(any(tx_lon),3) FROM wspr.rx WHERE band=144 AND time>='{t0}' AND time<'{t1}' GROUP BY h, tx_sign ORDER BY h");
            var rxa = await Sql($@"SELECT toStartOfHour(time) h, rx_sign, round(any(rx_lat),3), round(any(rx_lon),3) FROM wspr.rx WHERE band=144 AND time>='{t0}' AND time<'{t1}' GROUP BY h, rx_sign ORDER BY h");
            WriteGz(Path.Combine(dir, "wspr", tag + ".tx.tsv.gz"), txa);
            WriteGz(Path.Combine(dir, "wspr", tag + ".rx.tsv.gz"), rxa);
            WriteGz(fs, spot);
            Console.WriteLine($"{tag}: {spot.Count(c => c == '\n')} coppie-ora, {txa.Count(c => c == '\n')} TX-ora, {rxa.Count(c => c == '\n')} RX-ora");
            await Task.Delay(2000);
        }
    }

    // ───────────────────────── 2. celle ─────────────────────────
    static readonly double[] Frac = { 0.2, 0.4, 0.6, 0.8 };
    static (int la, int lo) Cell(LatLon p) => ((int)Math.Floor(p.Lat), (int)Math.Floor(p.Lon));
    static IEnumerable<(int, int)> PathCells(LatLon a, LatLon b)
    {
        double d = Geo.DistKm(a, b), az = Geo.Azimuth(a, b);
        foreach (var f in Frac) yield return Cell(Geo.Destination(a, az, d * f));
    }

    static void Celle(string dir, int min)
    {
        var count = new Dictionary<(int, int), int>();
        foreach (var f in Directory.GetFiles(Path.Combine(dir, "wspr"), "*.spot.tsv.gz"))
            foreach (var r in ReadGz(f))
            {
                double km = D(r[7]); if (km < 100 || km > 2000) continue;
                var A = new LatLon(D(r[2]), D(r[3])); var B = new LatLon(D(r[5]), D(r[6]));
                foreach (var c in PathCells(A, B).Distinct()) count[c] = count.GetValueOrDefault(c) + 1;
            }
        var sel = count.Where(kv => kv.Value >= min).OrderByDescending(kv => kv.Value).ToList();
        File.WriteAllLines(Path.Combine(dir, "celle.tsv"), sel.Select(kv => $"{kv.Key.Item1}\t{kv.Key.Item2}\t{kv.Value}"));
        Console.WriteLine($"{count.Count} celle attraversate, {sel.Count} con almeno {min} tratte");
    }
    static double D(string s) => double.Parse(s, Ci);

    // ───────────────────────── 3. GFS ─────────────────────────
    // Open-Meteo conta una chiamata «pesata»: oltre 10 variabili o oltre 2 settimane valgono di più (≈ variabili/10 × giorni/14).
    static async Task<int> Gfs(string dir, int budget)
    {
        var cells = File.ReadAllLines(Path.Combine(dir, "celle.tsv")).Select(l => l.Split('\t')).Select(x => (int.Parse(x[0]), int.Parse(x[1]))).ToList();
        var months = Directory.GetFiles(Path.Combine(dir, "wspr"), "*.spot.tsv.gz").Select(f => Path.GetFileName(f)[..7])
            .Where(m => string.CompareOrdinal(m, "2021-04") >= 0).OrderByDescending(m => m).ToList();   // l'archivio GFS di Open-Meteo parte dal 2021
        var vars = OwnForecast.HourlyVars();
        double weight = Math.Ceiling(vars.Split(',').Length / 10.0) * Math.Ceiling(31 / 14.0);
        var stateF = Path.Combine(dir, "gfs", "fatto.txt"); Directory.CreateDirectory(Path.GetDirectoryName(stateF));
        var done = File.Exists(stateF) ? new HashSet<string>(File.ReadAllLines(stateF)) : new HashSet<string>();
        double used = 0; int n = 0;
        foreach (var m in months)
            foreach (var (la, lo) in cells)
            {
                string key = $"{m} {la} {lo}";
                if (done.Contains(key)) continue;
                if (used + weight > budget) { Console.WriteLine($"budget esaurito: {n} celle-mese scaricate, {used:0} chiamate pesate"); return 0; }
                var d0 = DateTime.ParseExact(m, "yyyy-MM", Ci); var d1 = d0.AddMonths(1).AddDays(-1);
                if (d1 > DateTime.UtcNow.Date.AddDays(-2)) d1 = DateTime.UtcNow.Date.AddDays(-2);
                double lat = la + 0.5, lon = lo + 0.5;
                string url = "https://historical-forecast-api.open-meteo.com/v1/forecast?latitude=" + lat.ToString(Ci) + "&longitude=" + lon.ToString(Ci) +
                             "&hourly=" + vars + "&models=gfs_seamless&start_date=" + d0.ToString("yyyy-MM-dd") + "&end_date=" + d1.ToString("yyyy-MM-dd") + "&wind_speed_unit=ms&timezone=GMT";
                string json = null;
                for (int k = 0; k < 4 && json == null; k++)
                {
                    using var r = await Http.GetAsync(url);
                    if ((int)r.StatusCode == 429) { Console.WriteLine("limite di Open-Meteo raggiunto, riprendo al prossimo giro"); return 0; }
                    if (r.IsSuccessStatusCode) json = await r.Content.ReadAsStringAsync(); else await Task.Delay(10000);
                }
                used += weight;
                if (json == null) { Console.WriteLine($"{key}: nessuna risposta"); continue; }
                var sb = new StringBuilder();
                try
                {
                    using var doc = JsonDocument.Parse(json);
                    var ps = OwnForecast.ParsePoint(doc.RootElement, new LatLon(lat, lon));
                    for (int i = 0; i < ps.Times.Length; i++)
                    {
                        if (ps.Times[i].Hour % 3 != 0) continue;
                        var p = ps.Profiles[i]; if (p == null || double.IsNaN(p.Ns)) continue;
                        sb.Append(ps.Times[i].ToString("yyyy-MM-ddTHH", Ci)).Append('\t').Append(Row(p)).Append('\n');
                    }
                }
                catch (Exception ex) { Console.WriteLine($"{key}: {ex.Message}"); }
                File.AppendAllText(Path.Combine(dir, "gfs", $"{m}.{la}.{lo}.tsv"), sb.ToString());
                File.AppendAllText(stateF, key + "\n"); done.Add(key); n++;
                await Task.Delay(800);
            }
        Console.WriteLine($"tutto scaricato: {n} celle-mese in questo giro");
        return 0;
    }

    // grandezze fisiche di un profilo (nessuna coordinata: il modello deve valere ovunque)
    public static readonly string[] GfsCols = { "Ns", "DN1km", "MaxGrad", "Duct", "DuctBaseAgl", "DuctThick", "DuctDef", "SurfDuct", "LayerDb", "Blh", "Wind10", "Cape", "StabDb", "KDrop", "Sea", "SeaAirDb", "W850", "VertDb" };
    static string Row(ProfileResult p)
    {
        double V(double x) => double.IsNaN(x) || double.IsInfinity(x) ? -999 : Math.Round(x, 2);
        var v = new[] { V(p.Ns), V(p.DN1km), V(p.MaxGrad), p.HasDuct ? 1 : 0, p.HasDuct ? V(p.DuctBase - p.Elev) : -999, p.HasDuct ? V(p.DuctTop - p.DuctBase) : 0,
                        p.HasDuct ? V(p.DuctDeficit) : 0, p.SurfaceDuct ? 1 : 0, V(p.LayerGainDb), V(p.Blh), V(p.Wind10), V(p.Cape), V(p.StabDb), V(p.KDrop),
                        p.Sea ? 1 : 0, V(p.SeaAirDb), V(p.W850), V(p.VertDb) };
        return string.Join("\t", v.Select(x => x.ToString(Ci)));
    }

    // ───────────────────────── 4. dataset ─────────────────────────
    static void Dataset(string dir)
    {
        var gfs = new Dictionary<(int, int, DateTime), double[]>();
        foreach (var f in Directory.GetFiles(Path.Combine(dir, "gfs"), "*.tsv"))
        {
            var nm = Path.GetFileNameWithoutExtension(f).Split('.'); int la = int.Parse(nm[1]), lo = int.Parse(nm[2]);
            foreach (var l in File.ReadLines(f))
            {
                var x = l.Split('\t'); if (x.Length != GfsCols.Length + 1) continue;
                gfs[(la, lo, DateTime.ParseExact(x[0], "yyyy-MM-ddTHH", Ci))] = x.Skip(1).Select(D).ToArray();
            }
        }
        Console.WriteLine($"{gfs.Count} profili GFS");
        var rnd = new Random(14);
        var outp = new StringBuilder();
        outp.Append("Label\tRegion\tMonth\tKm\tLocalHour\tMonthSin\tMonthCos\tAbsLat\t" + string.Join("\t", GfsCols.SelectMany(c => new[] { c + "_mean", c + "_ext" })) + "\tCells\tSnr\n");
        int pos = 0, neg = 0;
        foreach (var f in Directory.GetFiles(Path.Combine(dir, "wspr"), "*.spot.tsv.gz").OrderBy(x => x))
        {
            string tag = Path.GetFileName(f)[..7];
            if (string.CompareOrdinal(tag, "2021-04") < 0) continue;
            var spots = new Dictionary<(DateTime, string, string), double>();
            foreach (var r in ReadGz(f)) spots[(DateTime.Parse(r[0], Ci), r[1], r[4])] = D(r[8]);
            var tx = ReadGz(Path.Combine(dir, "wspr", tag + ".tx.tsv.gz")).GroupBy(r => DateTime.Parse(r[0], Ci)).ToDictionary(g => g.Key, g => g.Select(r => (r[1], new LatLon(D(r[2]), D(r[3])))).ToList());
            var rx = ReadGz(Path.Combine(dir, "wspr", tag + ".rx.tsv.gz")).GroupBy(r => DateTime.Parse(r[0], Ci)).ToDictionary(g => g.Key, g => g.Select(r => (r[1], new LatLon(D(r[2]), D(r[3])))).ToList());
            foreach (var (h, txs) in tx)
            {
                if (!rx.TryGetValue(h, out var rxs)) continue;
                foreach (var (ts, tp) in txs)
                    foreach (var (rs, rp) in rxs)
                    {
                        if (ts == rs) continue;
                        double km = Geo.DistKm(tp, rp); if (km < 150 || km > 2000) continue;
                        bool isPos = spots.TryGetValue((h, ts, rs), out var snr);
                        // i «no» sono moltissimi: se ne tiene una parte, con un peso che il modello non vede (si ricalibra dopo)
                        if (!isPos && rnd.NextDouble() > 0.15) continue;
                        var feat = PathFeatures(gfs, tp, rp, h); if (feat == null) continue;
                        var mid = Geo.Destination(tp, Geo.Azimuth(tp, rp), km / 2);
                        double lh = ((h.Hour + mid.Lon / 15) % 24 + 24) % 24; int mo = h.Month;
                        double ms = Math.Sin(2 * Math.PI * (mo - 1) / 12), mc = Math.Cos(2 * Math.PI * (mo - 1) / 12);
                        if (mid.Lat < 0) { ms = -ms; mc = -mc; }       // stagione per emisfero: gennaio australe = luglio boreale
                        outp.Append(isPos ? 1 : 0).Append('\t').Append(Region(mid)).Append('\t').Append(tag).Append('\t').Append(Math.Round(km)).Append('\t').Append(Math.Round(lh, 1))
                            .Append('\t').Append(Math.Round(ms, 3)).Append('\t').Append(Math.Round(mc, 3)).Append('\t').Append(Math.Round(Math.Abs(mid.Lat), 1))
                            .Append('\t').Append(feat).Append('\t').Append(isPos ? snr.ToString(Ci) : "").Append('\n');
                        if (isPos) pos++; else neg++;
                    }
            }
            Console.WriteLine($"{tag}: {pos} aperture, {neg} non aperture (campione 15%) finora");
        }
        WriteGz(Path.Combine(dir, "dataset.tsv.gz"), outp.ToString());
    }

    static string PathFeatures(Dictionary<(int, int, DateTime), double[]> gfs, LatLon a, LatLon b, DateTime h)
    {
        var t = new DateTime(h.Year, h.Month, h.Day, h.Hour / 3 * 3, 0, 0);
        var rows = new List<double[]>();
        foreach (var (la, lo) in PathCells(a, b))
            if (gfs.TryGetValue((la, lo, t), out var v)) rows.Add(v);
        if (rows.Count < 3) return null;
        var sb = new StringBuilder();
        for (int j = 0; j < GfsCols.Length; j++)
        {
            var col = rows.Select(r => r[j]).Where(x => x > -998).ToList();
            double mean = col.Count > 0 ? col.Average() : -999;
            // valore «estremo» nel verso che favorisce la tropo: il gradiente più negativo, il condotto più forte
            double ext = col.Count == 0 ? -999 : GfsCols[j] is "DN1km" or "W850" or "DuctBaseAgl" ? col.Min() : col.Max();
            sb.Append(Math.Round(mean, 2).ToString(Ci)).Append('\t').Append(Math.Round(ext, 2).ToString(Ci)).Append('\t');
        }
        sb.Append(rows.Count);
        return sb.ToString();
    }

    static string Region(LatLon m) =>
        m.Lat < 0 && m.Lon > 100 ? "OC" :
        m.Lon >= -30 && m.Lon <= 50 && m.Lat > 25 ? "EU" :
        m.Lon < -30 ? "AM" :
        m.Lon > 100 && m.Lat > 0 ? "AS" : "ALTRO";

    // ───────────────────────── 5. addestramento ─────────────────────────
    static void Addestra(string dir)
    {
        var ml = new MLContext(seed: 14);
        var lines = ReadGzAll(Path.Combine(dir, "dataset.tsv.gz"));
        var head = lines[0].Split('\t');
        int iLab = 0, iReg = 1, iFeat0 = 3, iCells = head.Length - 2;
        var featNames = head[iFeat0..iCells];
        var rows = lines.Skip(1).Select(l => l.Split('\t')).Select(x => new Row14
        {
            Label = x[iLab] == "1", Region = x[iReg],
            F = x[iFeat0..iCells].Select(s => { var v = float.Parse(s, Ci); return v <= -998 ? float.NaN : v; }).ToArray()
        }).ToList();
        var rep = new StringBuilder("# Modello statistico delle aperture tropo sui 2 m — prova per regioni lasciate fuori\n\n");
        rep.Append($"Righe: {rows.Count} ({rows.Count(r => r.Label)} aperture, {rows.Count(r => !r.Label)} non aperture campionate al 15%).\n\n");
        rep.Append("| Regione di prova | Righe | Aperture | AUC modello | AUC solo condotto/gradiente | Brier |\n|---|---|---|---|---|---|\n");
        var schema = SchemaDefinition.Create(typeof(Row14));
        schema["F"].ColumnType = new VectorDataViewType(NumberDataViewType.Single, featNames.Length);
        IEstimator<ITransformer> Pipe() => ml.BinaryClassification.Trainers.LightGbm(new Microsoft.ML.Trainers.LightGbm.LightGbmBinaryTrainer.Options
        {
            LabelColumnName = "Label", FeatureColumnName = "F", NumberOfLeaves = 31, MinimumExampleCountPerLeaf = 50, LearningRate = 0.05, NumberOfIterations = 400,
            UnbalancedSets = false
        });
        foreach (var reg in new[] { "EU", "AM", "AS", "OC" })
        {
            var tr = rows.Where(r => r.Region != reg).ToList(); var te = rows.Where(r => r.Region == reg).ToList();
            if (te.Count(r => r.Label) < 50 || tr.Count(r => r.Label) < 200) { rep.Append($"| {reg} | {te.Count} | {te.Count(r => r.Label)} | — | — | — |\n"); continue; }
            var model = Pipe().Fit(ml.Data.LoadFromEnumerable(tr, schema));
            var pred = model.Transform(ml.Data.LoadFromEnumerable(te, schema));
            var m = ml.BinaryClassification.Evaluate(pred, "Label");
            var probs = ml.Data.CreateEnumerable<Out14>(pred, false).Select(o => o.Probability).ToArray();
            double brier = te.Select((r, i) => Math.Pow(probs[i] - (r.Label ? 1 : 0), 2)).Average();
            // riferimento: un solo numero fisico (il gradiente più negativo lungo la tratta)
            int iDn = Array.IndexOf(featNames, "DN1km_ext");
            double aucBase = Auc(te.Select(r => (double)-r.F[iDn]).ToArray(), te.Select(r => r.Label).ToArray());
            rep.Append($"| {reg} | {te.Count} | {te.Count(r => r.Label)} | {m.AreaUnderRocCurve:0.000} | {aucBase:0.000} | {brier:0.000} |\n");
        }
        var all = Pipe().Fit(ml.Data.LoadFromEnumerable(rows, schema));
        ml.Model.Save(all, ml.Data.LoadFromEnumerable(rows.Take(1), schema).Schema, Path.Combine(dir, "modello-tropo-2m.zip"));
        rep.Append("\nAUC 0,5 = a caso, 1 = perfetto. «Solo condotto/gradiente» usa il solo gradiente più negativo lungo la tratta: il modello serve se lo batte nella regione che non ha mai visto.\n");
        rep.Append("\nGrandezze: " + string.Join(", ", featNames) + ".\n");
        File.WriteAllText(Path.Combine(dir, "rapporto.md"), rep.ToString());
        Console.WriteLine(rep);
    }

    static List<string> ReadGzAll(string p) { using var f = File.OpenRead(p); using var gz = new GZipStream(f, CompressionMode.Decompress); using var r = new StreamReader(gz); var l = new List<string>(); string s; while ((s = r.ReadLine()) != null) l.Add(s); return l; }

    static double Auc(double[] score, bool[] lab)
    {
        var idx = Enumerable.Range(0, score.Length).Where(i => !double.IsNaN(score[i])).OrderBy(i => score[i]).ToArray();
        double rankSum = 0; long nPos = 0, nNeg = 0;
        for (int k = 0; k < idx.Length; k++) if (lab[idx[k]]) { rankSum += k + 1; nPos++; } else nNeg++;
        return nPos == 0 || nNeg == 0 ? double.NaN : (rankSum - nPos * (nPos + 1) / 2.0) / (nPos * (double)nNeg);
    }
}

public sealed class Row14 { public bool Label; public string Region = ""; public float[] F; }
public sealed class Out14 { public float Probability; }
