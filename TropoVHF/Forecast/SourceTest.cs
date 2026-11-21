using System.Globalization;
using System.Text;
using TropoVHF.Core;

namespace TropoVHF.Forecast;

/// <summary>
/// Prova delle sorgenti dei profili (riga di comando: --prova-fonti file.txt). La usa la compilazione automatica per
/// verificare con i servizi veri: NOMADS e il lettore GRIB2, Open-Meteo con le variabili nuove, l'archivio storico,
/// ICON-EU ed ECMWF. Non apre finestre e non scrive nella cache.
/// </summary>
public static class SourceTest
{
    public static int Run(string outFile)
    {
        var sb = new StringBuilder();
        void W(string s) { sb.AppendLine(s); Console.WriteLine(s); }
        int fails = 0;
        using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(120) };
        http.DefaultRequestHeaders.UserAgent.ParseAdd("LIVETROPOVHF-prova/1.3");
        var pts = new List<LatLon> { new LatLon(41.0, 14.0), new LatLon(41.0, 14.5), new LatLon(45.5, 9.0), new LatLon(40.0, -5.0) };
        string P(ProfileResult p) => p == null ? "nessun profilo" :
            string.Format(CultureInfo.InvariantCulture, "Ns {0:0.0} ΔN {1:0.0} w850 {2:0.000} m/s w700 {3:0.000} vento850 {4:0.0} m/s da {5:0} T850 {6:0.0} ΔW {7:0} Misme {8:+0.0;-0.0} K {9:0} calo K {10:0} subs {11} salita {12} scirocco {13} strati {14} guadagno144 {15:0.0}",
                p.Ns, p.DN1km, p.W850, p.W700, p.Wind850, p.Dir850, p.T850, p.DeltaW, p.StabDb, p.KSurf, p.KDrop, p.Subsidence, p.Ascent, p.Scirocco, p.Layers.Count, p.GainDb(144.3));
        void Show(string name, List<OwnForecast.PointSeries> ser)
        {
            W($"{name}: {ser.Count} serie");
            foreach (var s in ser.Take(4))
            {
                int ok = s.Profiles.Count(x => x != null);
                W($"  {s.Lat:0.##},{s.Lon:0.##} quota {s.Elev:0} m, ore {s.Times.Length} ({(s.Times.Length > 0 ? s.Times[0].ToString("dd/MM HH") : "—")}), profili validi {ok} [{s.From}]");
                W("    prima ora: " + P(s.Profiles.FirstOrDefault(x => x != null)));
            }
            if (ser.Count == 0 || ser.All(s => s.Profiles.All(x => x == null))) fails++;
        }
        async Task Try(string name, Func<Task> f)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            try { await f(); W($"  ({name}: {sw.Elapsed.TotalSeconds:0.0} s)"); }
            catch (Exception ex) { fails++; W($"{name}: ERRORE {ex.GetType().Name}: {ex.Message}"); }
        }
        Task.Run(async () =>
        {
            await Try("NOMADS", async () =>
            {
                var st = new FetchStats();
                var (raw, run) = await Nomads.FetchPoints(http, pts, 3, default, null, CancellationToken.None, st);
                W($"NOMADS: corsa {run:dd/MM HH} UTC, {st.Batches} file GRIB2, {st.Bytes / 1024} kB, punti {raw.Count(x => x != null)}/{pts.Count}");
                var ser = new List<OwnForecast.PointSeries>();
                for (int i = 0; i < raw.Length; i++)
                    if (raw[i] != null)
                    {
                        using var d = System.Text.Json.JsonDocument.Parse(raw[i]);
                        var ps = OwnForecast.ParsePoint(d.RootElement, pts[i]);
                        ps.From = "NOMADS"; ser.Add(ps);
                    }
                Show("NOMADS", ser);
                if (raw[0] != null) W("  JSON del primo punto (inizio): " + raw[0].Substring(0, Math.Min(500, raw[0].Length)));
            });
            foreach (var res in new[] { Nomads.Res.H050, Nomads.Res.D100 })
                await Try("NOMADS " + res, async () =>
                {
                    var st = new FetchStats();
                    var (raw, run) = await Nomads.FetchPoints(http, pts, 9, default, null, CancellationToken.None, st, res);
                    W($"NOMADS {res}: corsa {run:dd/MM HH} UTC, {st.Batches} file, {st.Bytes / 1024} kB, punti {raw.Count(x => x != null)}/{pts.Count}");
                    var ser = new List<OwnForecast.PointSeries>();
                    for (int i = 0; i < raw.Length; i++)
                        if (raw[i] != null) { using var d = System.Text.Json.JsonDocument.Parse(raw[i]); var ps = OwnForecast.ParsePoint(d.RootElement, pts[i]); ps.From = "NOMADS " + res; ser.Add(ps); }
                    Show("NOMADS " + res, ser);
                });
            foreach (var src in new[] { WxSource.OpenMeteo, WxSource.IconEu, WxSource.Ecmwf, WxSource.IconItalia })
                await Try(src.ToString(), async () => Show(src.ToString(), await OwnForecast.FetchPoints(http, pts.Take(2).ToList(), null, CancellationToken.None, new FetchStats(), default, src)));
            await Try("archivio storico", async () => Show("archivio 2022-08-13", await OwnForecast.FetchPoints(http, new List<LatLon> { new LatLon(45, -95) }, null, CancellationToken.None, new FetchStats(), default, WxSource.OpenMeteo, HistCase.Known[0])));
        }).GetAwaiter().GetResult();
        W(fails == 0 ? "ESITO: tutte le sorgenti rispondono" : $"ESITO: {fails} prove non riuscite");
        try { File.WriteAllText(outFile, sb.ToString()); } catch { }
        return fails;
    }
}
