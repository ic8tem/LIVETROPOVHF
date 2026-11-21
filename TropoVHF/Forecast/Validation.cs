using System.Globalization;
using System.Text;
using TropoVHF.Core;

namespace TropoVHF.Forecast;

/// <summary>
/// Validazione del modello su collegamenti reali (riga di comando: --valida dati.txt esito.txt).
/// Per ogni collegamento si calcola il guadagno previsto lungo la tratta all'ora del collegamento, dalla griglia GFS
/// d'archivio; lo stesso calcolo sulle stesse tratte e alle stesse ore 14 giorni dopo fa da confronto ("giorni normali").
/// Si ripete con e senza le correzioni meteo (Misme, subsidenza). Non usa SRTM: è il guadagno della mappa lungo la tratta.
/// </summary>
public static class Validation
{
    sealed class Qso { public DateTime Utc; public LatLon A, B; public string LA = "", LB = "", Cat = ""; public double Km, Freq; }
    sealed class Res { public double Mean = double.NaN, Frac10 = double.NaN, Max = double.NaN, Stab = double.NaN; public int Subs, Sci, N; }

    public static int Run(string inFile, string outFile)
    {
        var sb = new StringBuilder();
        void W(string s) { sb.AppendLine(s); Console.WriteLine(s); try { File.WriteAllText(outFile, sb.ToString()); } catch { } }
        var ci = CultureInfo.InvariantCulture;
        var qs = new List<Qso>();
        foreach (var line in File.ReadAllLines(inFile))
        {
            if (line.StartsWith("#") || line.Trim() == "") continue;
            var p = line.Split(';');
            if (p.Length < 5) continue;
            if (!DateTime.TryParse(p[0], ci, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out var t)) continue;
            if (!LocatorX.TryParse(p[1], out var a) || !LocatorX.TryParse(p[2], out var b)) continue;
            qs.Add(new Qso { Utc = DateTime.SpecifyKind(t, DateTimeKind.Utc), A = a, B = b, LA = p[1], LB = p[2], Freq = double.Parse(p[3], ci), Cat = p[4].Trim(), Km = Geo.DistKm(a, b) });
        }
        W($"VALIDAZIONE su {qs.Count} collegamenti da {inFile}");
        if (qs.Count == 0) { File.WriteAllText(outFile, sb.ToString()); return 1; }
        var d0 = qs.Min(q => q.Utc).Date; var d1 = qs.Max(q => q.Utc).Date;
        double la1 = qs.Min(q => Math.Min(q.A.Lat, q.B.Lat)) - 0.5, la2 = qs.Max(q => Math.Max(q.A.Lat, q.B.Lat)) + 0.5;
        double lo1 = qs.Min(q => Math.Min(q.A.Lon, q.B.Lon)) - 0.5, lo2 = qs.Max(q => Math.Max(q.A.Lon, q.B.Lon)) + 0.5;
        int days = (int)(d1 - d0).TotalDays + 1;
        const int shift = 14;
        var ev = new HistCase { Name = $"evento {d0:dd/MM/yyyy}–{d1:dd/MM/yyyy}", StartUtc = DateTime.SpecifyKind(d0, DateTimeKind.Utc), Days = days, LatMin = la1, LatMax = la2, LonMin = lo1, LonMax = lo2 };
        var ct = new HistCase { Name = $"confronto +{shift} giorni", StartUtc = ev.StartUtc.AddDays(shift), Days = days, LatMin = la1, LatMax = la2, LonMin = lo1, LonMax = lo2 };
        W($"riquadro {la1:0.0}–{la2:0.0} N, {lo1:0.0}–{lo2:0.0} E; evento {days} giorni dal {d0:dd/MM/yyyy}; confronto stesse tratte e ore {shift} giorni dopo");

        using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(180) };
        http.DefaultRequestHeaders.UserAgent.ParseAdd("LIVETROPOVHF-validazione/1.3");
        OwnForecast Load(HistCase h)
        {
            var own = new OwnForecast { Hist = h };
            var sw = System.Diagnostics.Stopwatch.StartNew();
            bool ok = Task.Run(() => own.UpdateAsync(http, h.LatMin, h.LatMax, h.LonMin, h.LonMax, null, CancellationToken.None)).GetAwaiter().GetResult();
            W($"griglia «{h.Name}»: {(ok ? "ok" : "NON riuscita")} — {own.Status} ({sw.Elapsed.TotalSeconds:0} s)");
            return ok ? own : null;
        }
        // base 1.3.1: le novità della 1.3.2 spente, si accendono una alla volta più sotto
        Refractivity.UseDuctCeiling = false; Refractivity.UseHighPressure = false; Refractivity.UseCrplNormal = false; Refractivity.UseItuSuper = false;
        var gEv = Load(ev); var gCt = Load(ct);
        if (gEv == null) { File.WriteAllText(outFile, sb.ToString()); return 2; }

        Res Path(OwnForecast g, Qso q, DateTime t)
        {
            var r = new Res();
            if (g == null) return r;
            int n = Math.Max(3, (int)Math.Ceiling(q.Km / 25) + 1);
            var pts = Geo.GreatCircle(q.A, q.B, n - 1);
            var vals = new List<double>();
            double stab = 0; int ns = 0;
            foreach (var p in pts)
            {
                double v = g.Index(p.Lat, p.Lon, t, q.Freq);
                if (double.IsNaN(v)) continue;
                vals.Add(v * 2.5);
                var pr = g.At(p.Lat, p.Lon, t);
                if (pr != null) { stab += pr.StabDb; ns++; if (pr.Subsidence) r.Subs++; if (pr.Scirocco) r.Sci++; }
            }
            r.N = pts.Count;
            if (vals.Count < 0.6 * pts.Count) return r;
            r.Mean = vals.Average(); r.Max = vals.Max(); r.Frac10 = vals.Count(x => x >= 10) / (double)vals.Count;
            r.Stab = ns > 0 ? stab / ns : double.NaN;
            return r;
        }

        // reload: la novità cambia l'analisi dei profili, quindi le griglie si rileggono (dalla cache) con le nuove regole
        var configs = new (string name, bool soft, bool corr, bool pers, bool detail, int v132, bool reload)[]
        {
            ("modello 1.2 (niente condotto morbido, niente correzioni)", false, false, false, false, 0, false),
            ("+ condotto morbido", true, false, false, false, 0, false),
            ("+ condotto morbido + Misme e subsidenza", true, true, false, false, 0, false),
            ("1.3.1 completo: + persistenza", true, true, true, true, 0, false),
            ("1.3.2: + alta pressione estesa e vento debole", true, true, true, false, 1, false),
            ("1.3.2: + condotti cercati solo sotto 2500 m", true, true, true, false, 2, true),
            ("1.3.2 alternativa: + profilo normale CRPL con Ns locale", true, true, true, false, 3, true),
            ("1.3.2 stima aggressiva: soglia classica di super-rifrazione 79 N/km, k ≈ 2 (senza CRPL)", true, true, true, false, 4, true),
            ("1.3.2 prudente (configurazione predefinita)", true, true, true, true, 5, true),
        };
        foreach (var cf in configs)
        {
            Refractivity.UseSoftDuct = cf.soft; Refractivity.UseCorrections = cf.corr; Refractivity.UsePersistence = cf.pers;
            Refractivity.UseHighPressure = cf.v132 >= 1;
            Refractivity.UseDuctCeiling = cf.v132 >= 2;
            Refractivity.UseCrplNormal = cf.v132 == 3;
            Refractivity.UseItuSuper = cf.v132 == 4;
            if (cf.reload) { gEv = Load(ev) ?? gEv; gCt = Load(ct) ?? gCt; }
            bool corr = cf.detail;
            W("");
            W($"===== {cf.name} =====");
            foreach (var cat in qs.Select(q => q.Cat).Distinct().OrderByDescending(c => c == "tropo"))
            {
                var sel = qs.Where(q => q.Cat == cat).ToList();
                var e = sel.Select(q => (q, r: Path(gEv, q, q.Utc))).Where(x => !double.IsNaN(x.r.Mean)).ToList();
                var c = sel.Select(q => (q, r: Path(gCt, q, q.Utc.AddDays(shift)))).Where(x => !double.IsNaN(x.r.Mean)).ToList();
                W($"--- categoria «{cat}»: {sel.Count} collegamenti, con griglia sul percorso {e.Count} (evento) / {c.Count} (confronto)");
                if (e.Count == 0) continue;
                string Q(IEnumerable<double> v) { var a = v.OrderBy(x => x).ToArray(); if (a.Length == 0) return "—"; double P(double f) => a[(int)Math.Min(a.Length - 1, Math.Floor(f * a.Length))]; return string.Format(ci, "mediana {0:0.0}, 10° {1:0.0}, 90° {2:0.0}", P(0.5), P(0.1), P(0.9)); }
                W($"   guadagno medio lungo la tratta (dB): evento {Q(e.Select(x => x.r.Mean))}; confronto {Q(c.Select(x => x.r.Mean))}");
                W($"   guadagno massimo lungo la tratta (dB): evento {Q(e.Select(x => x.r.Max))}; confronto {Q(c.Select(x => x.r.Max))}");
                foreach (var thr in new[] { 7.0, 10.0, 13.0, 16.0 })
                    W(string.Format(ci, "   tratte con guadagno medio ≥ {0:0} dB: evento {1:0}% (presi), confronto {2:0}% (falsi allarmi)", thr,
                        100.0 * e.Count(x => x.r.Mean >= thr) / e.Count, c.Count > 0 ? 100.0 * c.Count(x => x.r.Mean >= thr) / c.Count : double.NaN));
                W(string.Format(ci, "   frazione media della tratta sopra 10 dB: evento {0:0}%, confronto {1:0}%", 100 * e.Average(x => x.r.Frac10), c.Count > 0 ? 100 * c.Average(x => x.r.Frac10) : double.NaN));
                if (c.Count > 0)
                {
                    // AUC (Mann-Whitney): probabilità che una tratta dell'evento abbia un guadagno maggiore della stessa nel confronto
                    double wins = 0; foreach (var x in e) foreach (var y in c) wins += x.r.Mean > y.r.Mean ? 1 : x.r.Mean == y.r.Mean ? 0.5 : 0;
                    W(string.Format(ci, "   separazione evento/confronto (AUC, 0,5 = nessuna, 1 = perfetta): {0:0.00}", wins / ((double)e.Count * c.Count)));
                }
                W(string.Format(ci, "   termine di Misme medio sulle tratte: evento {0:+0.0;-0.0} dB, confronto {1:+0.0;-0.0} dB; punti con subsidenza: evento {2}, confronto {3}; con scirocco: evento {4}, confronto {5}",
                    e.Where(x => !double.IsNaN(x.r.Stab)).Select(x => x.r.Stab).DefaultIfEmpty(double.NaN).Average(), c.Where(x => !double.IsNaN(x.r.Stab)).Select(x => x.r.Stab).DefaultIfEmpty(double.NaN).Average(),
                    e.Sum(x => x.r.Subs), c.Sum(x => x.r.Subs), e.Sum(x => x.r.Sci), c.Sum(x => x.r.Sci)));
                if (cat != "tropo" || !corr) continue;
                W("   per giorno (mediana del guadagno medio, evento / confronto):");
                foreach (var g in e.GroupBy(x => x.q.Utc.Date).OrderBy(g => g.Key))
                {
                    var cc = c.Where(x => x.q.Utc.Date == g.Key).Select(x => x.r.Mean).OrderBy(v => v).ToList();
                    var ee = g.Select(x => x.r.Mean).OrderBy(v => v).ToList();
                    W(string.Format(ci, "      {0:dd/MM}: {1} QSO, {2:0.0} / {3:0.0} dB", g.Key, ee.Count, ee[ee.Count / 2], cc.Count > 0 ? cc[cc.Count / 2] : double.NaN));
                }
                W("   per distanza (mediana del guadagno medio, evento / confronto):");
                foreach (var (lo, hi) in new[] { (0.0, 500.0), (500.0, 900.0), (900.0, 1300.0), (1300.0, 5000.0) })
                {
                    var ee = e.Where(x => x.q.Km >= lo && x.q.Km < hi).Select(x => x.r.Mean).OrderBy(v => v).ToList();
                    var cc = c.Where(x => x.q.Km >= lo && x.q.Km < hi).Select(x => x.r.Mean).OrderBy(v => v).ToList();
                    if (ee.Count == 0) continue;
                    W(string.Format(ci, "      {0:0}–{1:0} km: {2} QSO, {3:0.0} / {4:0.0} dB", lo, hi, ee.Count, ee[ee.Count / 2], cc.Count > 0 ? cc[cc.Count / 2] : double.NaN));
                }
                W("   tratte con il guadagno previsto più basso (collegamenti che il modello non vede):");
                foreach (var x in e.OrderBy(x => x.r.Mean).Take(12))
                    W(string.Format(ci, "      {0:dd/MM HH:mm} {1}–{2} {3:0} km: medio {4:0.0} dB, massimo {5:0.0} dB", x.q.Utc, x.q.LA, x.q.LB, x.q.Km, x.r.Mean, x.r.Max));
            }
        }
        Refractivity.UseCorrections = true; Refractivity.UseSoftDuct = true; Refractivity.UsePersistence = true;

        // ---- ITM (Longley-Rice, NTIA) sulle tratte reali, terreno da Open-Meteo (DEM Copernicus 90 m, 100 punti per tratta):
        // quanti QSO si spiegano con una giornata normale, e se il guadagno previsto copre quello che manca
        try { ItmSection(qs.Where(q => q.Cat == "tropo").ToList(), gEv, gCt, shift, W, ci, Path); }
        catch (Exception ex) { W("ITM: sezione non riuscita: " + ex.Message); }

        // ---- equazione parabolica nel profilo M del GFS: guadagno rispetto all'atmosfera standard, evento contro confronto
        // (lenta: circa 10 minuti; si rilancia con LIVETROPOVHF_VALIDA_PE=1)
        if (Environment.GetEnvironmentVariable("LIVETROPOVHF_VALIDA_PE") == "1")
        {
            var tq = qs.Where(q => q.Cat == "tropo").ToList();
            int every = Math.Max(1, tq.Count / 100);
            foreach (double hAnt in new[] { 30.0, 300.0 })
            {
            var sample = tq.Where((q, i) => i % every == 0).ToList();
            W("");
            W($"===== equazione parabolica (campione di {sample.Count} collegamenti, terreno piatto, antenne a {hAnt:0} m, 144 MHz) =====");
            var ge = new List<double>(); var gc = new List<double>();
            var sw = System.Diagnostics.Stopwatch.StartNew();
            foreach (var q in sample)
            {
                int n = Math.Max(3, (int)Math.Ceiling(q.Km / 25) + 1);
                var pts = Geo.GreatCircle(q.A, q.B, n - 1);
                var pe1 = pts.Select(p => gEv.At(p.Lat, p.Lon, q.Utc)).ToList();
                if (pe1.Count(p => p != null) < 0.6 * pe1.Count) continue;
                var std = ParabolicEq.Run(q.Freq, q.Km, hAnt, hAnt, null, null, null, 20, CancellationToken.None, false).LossDb;
                var le = ParabolicEq.Run(q.Freq, q.Km, hAnt, hAnt, null, null, pe1, 20, CancellationToken.None, false).LossDb;
                ge.Add(std - le);
                if (gCt != null)
                {
                    var pc = pts.Select(p => gCt.At(p.Lat, p.Lon, q.Utc.AddDays(shift))).ToList();
                    if (pc.Count(p => p != null) >= 0.6 * pc.Count)
                        gc.Add(std - ParabolicEq.Run(q.Freq, q.Km, hAnt, hAnt, null, null, pc, 20, CancellationToken.None, false).LossDb);
                }
            }
            string Qp(List<double> v) { if (v.Count == 0) return "—"; var a2 = v.OrderBy(x => x).ToArray(); return string.Format(ci, "mediana {0:0.0}, 10° {1:0.0}, 90° {2:0.0}", a2[a2.Length / 2], a2[a2.Length / 10], a2[Math.Min(a2.Length - 1, a2.Length * 9 / 10)]); }
            W($"   guadagno del profilo (dB): evento {Qp(ge)}; confronto {Qp(gc)}  ({sw.Elapsed.TotalSeconds:0} s)");
            foreach (var thr in new[] { 10.0, 20.0, 30.0 })
                W(string.Format(ci, "   tratte con guadagno ≥ {0:0} dB: evento {1:0}%, confronto {2:0}%", thr, ge.Count > 0 ? 100.0 * ge.Count(x => x >= thr) / ge.Count : double.NaN, gc.Count > 0 ? 100.0 * gc.Count(x => x >= thr) / gc.Count : double.NaN));
            if (ge.Count > 0 && gc.Count > 0)
            {
                double wins = 0; foreach (var x in ge) foreach (var y in gc) wins += x > y ? 1 : x == y ? 0.5 : 0;
                W(string.Format(ci, "   separazione evento/confronto (AUC): {0:0.00}", wins / ((double)ge.Count * gc.Count)));
            }
            }
        }

        try { File.WriteAllText(outFile, sb.ToString()); } catch { }
        return 0;
    }

    /// <summary>Bilancio di riferimento per una stazione media in SSB: 100 W, cavo 2 dB per lato, antenne 14 dBi per lato,
    /// rumore −174 + 10·log 2400 + NF 2 dB, rapporto segnale/rumore 3 dB → perdita massima circa 210 dB.</summary>
    const double BudgetSsbDb = 50 - 2 + 14 + 14 - 2 - (-174 + 33.8 + 2 + 3);

    static void ItmSection(List<Qso> tq, OwnForecast gEv, OwnForecast gCt, int shift, Action<string> W, CultureInfo ci, Func<OwnForecast, Qso, DateTime, Res> path)
    {
        W("");
        W($"===== ITM (Longley-Rice, NTIA) sulle tratte reali: {tq.Count} QSO tropo, terreno Open-Meteo 90 m (100 punti), antenne a 10 m, 144 MHz =====");
        W(string.Format(ci, "   bilancio di riferimento SSB (100 W, 2×14 dBi, cavi 2+2 dB, NF 2 dB, S/N 3 dB in 2,4 kHz): perdita massima {0:0} dB", BudgetSsbDb));
        var cache = Path.Combine(Path.GetTempPath(), "livetropovhf-quote");
        Directory.CreateDirectory(cache);
        using var http = new System.Net.Http.HttpClient { Timeout = TimeSpan.FromSeconds(60) };
        http.DefaultRequestHeaders.UserAgent.ParseAdd("LIVETROPOVHF-validazione/1.3");
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var rows = new List<(Qso q, double l50, double l10, double l1, string mode, double gEv, double gCt)>();
        int fail = 0, done = 0;
        foreach (var q in tq)
        {
            var pts = Geo.GreatCircle(q.A, q.B, 99);
            string key = string.Format(ci, "{0:0.000}_{1:0.000}_{2:0.000}_{3:0.000}.txt", q.A.Lat, q.A.Lon, q.B.Lat, q.B.Lon);
            string fc = Path.Combine(cache, key);
            double[] hh = null;
            try
            {
                if (File.Exists(fc)) hh = File.ReadAllText(fc).Split(';').Select(x => double.Parse(x, ci)).ToArray();
                else
                {
                    string url = "https://api.open-meteo.com/v1/elevation?latitude=" + string.Join(",", pts.Select(p => p.Lat.ToString("0.0000", ci))) +
                                 "&longitude=" + string.Join(",", pts.Select(p => p.Lon.ToString("0.0000", ci)));
                    for (int att = 0; att < 3 && hh == null; att++)
                    {
                        var resp = http.GetAsync(url).GetAwaiter().GetResult();
                        if ((int)resp.StatusCode == 429) { Thread.Sleep(20000); continue; }
                        if (!resp.IsSuccessStatusCode) break;
                        var js = System.Text.Json.JsonDocument.Parse(resp.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                        hh = js.RootElement.GetProperty("elevation").EnumerateArray().Select(e => e.ValueKind == System.Text.Json.JsonValueKind.Number ? e.GetDouble() : 0).ToArray();
                    }
                    if (hh != null) File.WriteAllText(fc, string.Join(";", hh.Select(x => x.ToString("0.#", ci))));
                    Thread.Sleep(150);
                }
            }
            catch { hh = null; }
            if (hh == null || hh.Length != pts.Count) { fail++; continue; }
            int np = hh.Length - 1;
            var pfl = new double[np + 3]; pfl[0] = np; pfl[1] = q.Km * 1000 / np;
            int seaN = 0;
            for (int i = 0; i <= np; i++) { pfl[i + 2] = Math.Max(0, hh[i]); if (hh[i] <= 0.5) seaN++; }
            bool seaP = seaN >= 0.5 * (np + 1);
            var cl = seaP ? Itm.Climate.MaritimeTemperateSea : Itm.Climate.MaritimeTemperateLand;
            double n0 = seaP ? 350 : 320, eps = seaP ? 81 : 15, sig = seaP ? 5 : 0.005;
            var a50 = Itm.P2P(10, 10, pfl, cl, n0, q.Freq, false, eps, sig, 13, 50, 50, 50);
            if (a50.Error != 0) { fail++; continue; }
            var a10 = Itm.P2P(10, 10, pfl, cl, n0, q.Freq, false, eps, sig, 13, 10, 50, 50);
            var a01 = Itm.P2P(10, 10, pfl, cl, n0, q.Freq, false, eps, sig, 13, 1, 50, 50);
            rows.Add((q, a50.AdB, a10.AdB, a01.AdB, a50.ModeName, path(gEv, q, q.Utc).Mean, gCt != null ? path(gCt, q, q.Utc.AddDays(shift)).Mean : double.NaN));
            if (++done % 100 == 0) W($"   … {done} tratte ({sw.Elapsed.TotalSeconds:0} s)");
        }
        W($"   tratte calcolate {rows.Count}, senza terreno o fuori dai limiti {fail} ({sw.Elapsed.TotalSeconds:0} s)");
        if (rows.Count == 0) return;
        string Q(IEnumerable<double> v) { var a = v.Where(x => !double.IsNaN(x)).OrderBy(x => x).ToArray(); if (a.Length == 0) return "—"; double P(double f) => a[(int)Math.Min(a.Length - 1, Math.Floor(f * a.Length))]; return string.Format(ci, "mediana {0:0.0}, 10° {1:0.0}, 90° {2:0.0}", P(0.5), P(0.1), P(0.9)); }
        W($"   perdita ITM al 50% del tempo (dB): {Q(rows.Select(x => x.l50))}; al 10%: {Q(rows.Select(x => x.l10))}; all'1%: {Q(rows.Select(x => x.l1))}");
        foreach (var m in rows.GroupBy(x => x.mode)) W($"   meccanismo ITM «{m.Key}»: {m.Count()} tratte");
        double B = BudgetSsbDb;
        W(string.Format(ci, "   QSO spiegati da una giornata normale (perdita ≤ {0:0} dB): al 50% del tempo {1:0}%, nel 10% migliore {2:0}%, nell'1% migliore {3:0}%",
            B, 100.0 * rows.Count(x => x.l50 <= B) / rows.Count, 100.0 * rows.Count(x => x.l10 <= B) / rows.Count, 100.0 * rows.Count(x => x.l1 <= B) / rows.Count));
        var need = rows.Where(x => x.l50 > B).ToList();
        if (need.Count > 0)
        {
            W($"   dB mancanti rispetto all'ITM al 50% (solo i {need.Count} QSO che non bastano): {Q(need.Select(x => x.l50 - B))}");
            var ev = need.Where(x => !double.IsNaN(x.gEv)).ToList(); var ct = need.Where(x => !double.IsNaN(x.gCt)).ToList();
            if (ev.Count > 0)
                W(string.Format(ci, "   il guadagno medio previsto lungo la tratta copre i dB mancanti: evento {0:0}% ({1} QSO), stesse tratte 14 giorni dopo {2:0}% ({3}) — falsi allarmi",
                    100.0 * ev.Count(x => x.gEv >= x.l50 - B) / ev.Count, ev.Count, ct.Count > 0 ? 100.0 * ct.Count(x => x.gCt >= x.l50 - B) / ct.Count : double.NaN, ct.Count));
            foreach (var (lo, hi) in new[] { (0.0, 500.0), (500.0, 900.0), (900.0, 1300.0), (1300.0, 5000.0) })
            {
                var sel = rows.Where(x => x.q.Km >= lo && x.q.Km < hi).ToList();
                if (sel.Count == 0) continue;
                W(string.Format(ci, "      {0:0}–{1:0} km: {2} QSO, ITM 50% mediana {3:0}, spiegati al 50% {4:0}%, all'1% {5:0}%", lo, hi, sel.Count,
                    sel.Select(x => x.l50).OrderBy(x => x).ElementAt(sel.Count / 2), 100.0 * sel.Count(x => x.l50 <= B) / sel.Count, 100.0 * sel.Count(x => x.l1 <= B) / sel.Count));
            }
        }
    }
}
