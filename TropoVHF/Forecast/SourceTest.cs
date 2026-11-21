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
        // ITM (Longley-Rice) tradotto in C#: confronto con il codice NTIA originale, senza rete
        var itmLines = new List<string>();
        bool itmOk = Itm.SelfTest(itmLines);
        W("ITM (NTIA) contro il codice originale: " + (itmOk ? "uguale" : "DIVERSO"));
        foreach (var l in itmLines) W(l);
        if (!itmOk) fails++;
        // terreno Copernicus GLO-30: lettore GeoTIFF e quote note attorno a Capri (il mare deve valere 0)
        try
        {
            var cu = "https://copernicus-dem-30m.s3.amazonaws.com/Copernicus_DSM_COG_10_N40_00_E014_00_DEM/Copernicus_DSM_COG_10_N40_00_E014_00_DEM.tif";
            using var hc = new HttpClient { Timeout = TimeSpan.FromSeconds(300) };
            var tif = hc.GetByteArrayAsync(cu).GetAwaiter().GetResult();
            W("Copernicus GLO-30 N40E014: " + tif.Length / 1024 + " kB");
            W(Srtm.SelfTest(tif, new[] { ("JN70CN83CL (circa 60 m)", 40.55616, 14.23420), ("mare a NO di Marina Grande (0)", 40.5617, 14.2290),
                ("mare a N della stazione (0)", 40.5650, 14.2342), ("Monte Solaro (589 m)", 40.5486, 14.2208), ("Vesuvio (circa 1280 m)", 40.8213, 14.4261) }).TrimEnd());
        }
        catch (Exception ex) { W("Copernicus: prova non riuscita: " + ex.Message); fails++; }
        // tratta di prova dalla stazione dell'autore (atmosfera normale, senza previsione): orizzonti e perdite col nuovo terreno
        try
        {
            CalcLog.Enabled = true;
            var srtm = new Srtm(new HttpClient { Timeout = TimeSpan.FromSeconds(300) });
            foreach (var (rl, rn) in new[] { ("JN45ls27tg", "Lombardia 722 km"), ("JN33qs43wc", "Nizza 677 km"), ("IM98re20no", "Alicante 1300 km"), ("JN11cj39nl", "Barcellona 1016 km"), ("JN64CK40lh", "Ravenna") })
            {
                LocatorX.TryParse("JN70CN83CL", out var tp); LocatorX.TryParse(rl, out var rp);
                var tx = new Station { Loc = "JN70CN83CL", Pos = tp, Precise = true, AglM = 8, PowerW = 400, BoomM = 9 };
                var rx = new Station { Loc = rl, Pos = rp, Precise = true, AglM = 10, BoomM = 9 };
                var lr = LinkCalc.Compute(tx, rx, new LinkCalc.Options { Utc = DateTime.UtcNow }, srtm, null, CancellationToken.None, sub: !rl.StartsWith("JN11")).GetAwaiter().GetResult();
                W(string.Format(CultureInfo.InvariantCulture, "tratta JN70CN83CL → {0} ({1}): suolo TX {2:0} m, orizzonti TX {3:0.0} / RX {4:0.0} mrad, θ {5:0.0} mrad, P.452 senza tropo {6:0.0} dB, ITM 50% {7:0.0} dB",
                    rl, rn, tx.GroundM, lr.ThetaTx, lr.ThetaRx, lr.ThetaMrad, lr.LbaseDb, lr.ItmDb));
                W(string.Format(CultureInfo.InvariantCulture, "  az {0:0.0}°, meccanismo {1}, Lpred {2:0.0} dB, ostacolo TX a {3:0.0} km, via monte {4}",
                    lr.AzDeg, lr.Mechanism, lr.LpredDb, lr.NearTxKm, lr.Via.Count));
                W(string.Format(CultureInfo.InvariantCulture, "  distanza {0:0} km, Lfs {1:0.0}, diffrazione {2:0.0}, scatter {3:0.0}, base {4:0.0}, Prx {5:0.0} dBm, S/N SSB {6:0.0} CW {7:0.0} FT8 {8:0.0}, quota RX {9:0} m",
                    lr.DistKm, lr.LfsDb, lr.LdiffDb, lr.LscatDb, lr.LbaseDb, lr.PrxDbm, lr.SnrSsb, lr.SnrCw, lr.SnrFt8, rx.GroundM));
                foreach (var v in lr.Via.OrderBy(v => v.TotalDb).Take(6))
                    W(string.Format(CultureInfo.InvariantCulture, "    via az {0:0}° {1:0.0} km quota {2:0} m {3}: tratta1 {4:0.0} conv {5:0.0} tratta2 {6:0.0} ({7}) totale {8:0.0} dB",
                        v.AzDeg, v.DistKm, v.HeightM, v.Loc10, v.L1Db, v.ConvDb, v.L2Db, v.Mechanism2, v.TotalDb));
            }
        }
        catch (Exception ex) { W("tratta di prova non riuscita: " + ex.Message); }
        // calcolo inverso: antenna puntata sul Vesuvio, dove arriva il segnale diffuso dal versante illuminato
        try
        {
            var srtm = new Srtm(new HttpClient { Timeout = TimeSpan.FromSeconds(300) });
            var ct = CancellationToken.None;
            LocatorX.TryParse("JN70CN83CL", out var tp);
            var tx = new Station { Loc = "JN70CN83CL", Pos = tp, Precise = true, AglM = 8, PowerW = 400, BoomM = 9 };
            var ves = new LatLon(40.8213, 14.4261);
            double az0 = Geo.Azimuth(tp, ves), d0 = Geo.DistKm(tp, ves);
            double h0 = srtm.Height(tp, ct).GetAwaiter().GetResult() + 8, ae = 4.0 / 3 * 6371;
            const double step = 0.15;
            var line = new List<LatLon>(); for (double dk = step; dk <= 60; dk += step) line.Add(Geo.Destination(tp, az0, dk));
            var hl = srtm.Heights(line, ct).GetAwaiter().GetResult().h;
            double best = double.MinValue; int bi = -1, iv = (int)(d0 / step) - 1;
            double angTop = double.NaN, maxBefore = double.MinValue;
            for (int i = 0; i < hl.Length; i++)
            {
                double dk = (i + 1) * step, ang = (hl[i] - dk * dk / (2 * ae) * 1000 - h0) / dk;
                if (ang > best) { best = ang; bi = i; }
                if (i < iv - 3) maxBefore = Math.Max(maxBefore, ang);
                if (i == iv) angTop = ang;
            }
            W(string.Format(CultureInfo.InvariantCulture, "VESUVIO: azimut {0:0.0}°, {1:0.0} km; suolo QTH {2:0} m; angolo cima {3:0.00} mrad, massimo ostacolo prima {4:0.00} mrad → cima {5}",
                az0, d0, h0 - 8, angTop, maxBefore, angTop > maxBefore ? "VISIBILE" : "COPERTA"));
            var sp = line[bi]; double km1 = (bi + 1) * step;
            W(string.Format(CultureInfo.InvariantCulture, "  punto illuminato più alto sulla linea: {0:0.0} km, {1:0} m, {2}", km1, hl[bi], LocatorX.To10(sp.Lat, sp.Lon)));
            // orizzonte dal punto illuminato in tutte le direzioni (entro 60 km)
            var ring = new List<LatLon>(); int nr = (int)(60 / 0.3);
            for (int a = 0; a < 360; a += 10) for (int k = 1; k <= nr; k++) ring.Add(Geo.Destination(sp, a, k * 0.3));
            var hr = srtm.Heights(ring, ct).GetAwaiter().GetResult().h;
            var hsb = new StringBuilder("  orizzonte dal Vesuvio (gradi):");
            for (int a = 0; a < 36; a++)
            {
                double hor = double.MinValue;
                for (int k = 1; k <= nr; k++) { double dk = k * 0.3; hor = Math.Max(hor, Math.Atan((hr[a * nr + k - 1] - dk * dk / (2 * ae) * 1000 - hl[bi] - 10) / (dk * 1000)) * 180 / Math.PI); }
                hsb.Append(string.Format(CultureInfo.InvariantCulture, " {0}°:{1:+0.0;-0.0}", a * 10, hor));
            }
            W(hsb.ToString());
            var peak = new Station { Loc = LocatorX.To10(sp.Lat, sp.Lon), Pos = sp, Precise = true, AglM = 10, BoomM = 9, PowerW = 400 };
            var opt = new LinkCalc.Options { Utc = DateTime.UtcNow };
            double lam = 299.792458 / 144.3;
            foreach (var (nm, la, lo) in new[] { ("Firenze", 43.77, 11.25), ("Milano", 45.46, 9.19), ("Venezia", 45.44, 12.33), ("Lubiana", 46.05, 14.51), ("Zagabria", 45.81, 15.98),
                ("Spalato", 43.51, 16.44), ("Budapest", 47.50, 19.04), ("Belgrado", 44.79, 20.45), ("Tirana", 41.33, 19.82), ("Salonicco", 40.64, 22.94), ("Corfù", 39.62, 19.92),
                ("Palermo", 38.12, 13.36), ("Malta", 35.90, 14.51), ("Tunisi", 36.81, 10.18), ("Cagliari", 39.22, 9.12), ("Bastia", 42.70, 9.45), ("Marsiglia", 43.30, 5.37), ("Barcellona", 41.39, 2.17) })
            {
                var rp = new LatLon(la, lo);
                var rx = new Station { Loc = LocatorX.To10(la, lo), Pos = rp, Precise = true, AglM = 10, BoomM = 9 };
                var dir = LinkCalc.Compute(tx, rx, opt, srtm, null, ct, sub: true).GetAwaiter().GetResult();
                var leg = LinkCalc.Compute(peak, rx, opt, srtm, null, ct, sub: true).GetAwaiter().GetResult();
                double bw = Math.Min(60, Math.Sqrt(29000 / Math.Pow(10, dir.GtxDbi / 10))) * Math.PI / 180, r1 = km1 * 1000;
                double lfs1 = 20 * Math.Log10(4 * Math.PI * r1 / lam), conv = 10 * Math.Log10(lam * lam / (4 * Math.PI * Math.Pow(10, -1.5) * Math.Min(2e7, Math.Pow(r1 * bw, 2))));
                double via = lfs1 + conv + leg.LpredDb;
                W(string.Format(CultureInfo.InvariantCulture, "  VES {0}: {1:0} km az dal Vesuvio {2:0}°, orizz. Vesuvio {3:0.0} mrad, tratta2 {4:0.0} ({5}) → via Vesuvio {6:0.0} dB [solo tratta2 {4:0.0}]; diretto az {7:0}° {8:0.0} dB ({9}), orizz. QTH {10:0.0} mrad → differenza {11:+0.0;-0.0} dB",
                    nm, dir.DistKm, leg.AzDeg, leg.ThetaTx, leg.LpredDb, leg.Mechanism, via, dir.AzDeg, dir.LpredDb, dir.Mechanism, dir.ThetaTx, dir.LpredDb - via));
            }
        }
        catch (Exception ex) { W("calcolo Vesuvio non riuscito: " + ex.Message); }
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
