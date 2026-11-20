using TropoVHF.Core;

namespace TropoVHF.Forecast;

/// <summary>Locatore Maidenhead da 4 a 10 caratteri (a 8 e 10 caratteri il punto è preciso a circa 400 m e 20 m).</summary>
public static class LocatorX
{
    public static bool TryParse(string loc, out LatLon p)
    {
        p = default;
        if (string.IsNullOrWhiteSpace(loc)) return false;
        var l = loc.Trim().ToUpperInvariant();
        if (l.Length != 4 && l.Length != 6 && l.Length != 8 && l.Length != 10) return false;
        if (l[0] < 'A' || l[0] > 'R' || l[1] < 'A' || l[1] > 'R' || !char.IsDigit(l[2]) || !char.IsDigit(l[3])) return false;
        double lon = (l[0] - 'A') * 20 - 180 + (l[2] - '0') * 2, lat = (l[1] - 'A') * 10 - 90 + (l[3] - '0');
        double w = 2, h = 1;
        if (l.Length >= 6)
        {
            if (l[4] < 'A' || l[4] > 'X' || l[5] < 'A' || l[5] > 'X') return false;
            w /= 24; h /= 24; lon += (l[4] - 'A') * w; lat += (l[5] - 'A') * h;
        }
        if (l.Length >= 8)
        {
            if (!char.IsDigit(l[6]) || !char.IsDigit(l[7])) return false;
            w /= 10; h /= 10; lon += (l[6] - '0') * w; lat += (l[7] - '0') * h;
        }
        if (l.Length >= 10)
        {
            if (l[8] < 'A' || l[8] > 'X' || l[9] < 'A' || l[9] > 'X') return false;
            w /= 24; h /= 24; lon += (l[8] - 'A') * w; lat += (l[9] - 'A') * h;
        }
        p = new LatLon(lat + h / 2, lon + w / 2);
        return true;
    }

    public static string From(LatLon p, int len)
    {
        double x = p.Lon + 180, y = p.Lat + 90;
        if (x < 0 || x >= 360 || y < 0 || y >= 180) return "";
        var sb = new System.Text.StringBuilder();
        sb.Append((char)('A' + (int)(x / 20))).Append((char)('A' + (int)(y / 10)));
        x %= 20; y %= 10;
        sb.Append((char)('0' + (int)(x / 2))).Append((char)('0' + (int)y));
        x %= 2; y %= 1;
        if (len >= 6) { sb.Append((char)('a' + (int)(x * 12))).Append((char)('a' + (int)(y * 24))); x %= 1.0 / 12; y %= 1.0 / 24; }
        if (len >= 8) { sb.Append((char)('0' + (int)(x * 120))).Append((char)('0' + (int)(y * 240))); x %= 1.0 / 120; y %= 1.0 / 240; }
        if (len >= 10) { sb.Append((char)('a' + (int)(x * 2880))).Append((char)('a' + (int)(y * 5760))); }
        return sb.ToString();
    }
}

/// <summary>Stazione: posizione, altezze e impianto d'antenna.</summary>
public sealed class Station
{
    public string Loc = "";
    public LatLon Pos;
    public bool Precise;          // locatore a 8 o 10 caratteri: suolo dal profilo SRTM1
    public double AglM = 10;      // altezza dell'antenna dal suolo (con locatore preciso)
    public double AslM = 0;       // quota dell'antenna sul mare (con locatore a 4 o 6 caratteri)
    public double BoomM = 2;      // lunghezza del boom della yagi DL6WU (m)
    public double CableDb = 1;
    public double PowerW = 100;
    public double NfDb = 2;
    public double GroundM;        // suolo sotto l'antenna (dal profilo)
    public double AntennaAsl => Precise ? GroundM + AglM : (AslM > 0 ? AslM : GroundM + AglM);
}

/// <summary>Guadagno stimato di una yagi DL6WU dalla lunghezza del boom (dBd), con un taglio a 0,3 λ.</summary>
public static class Dl6wu
{
    // Approssimazione delle tabelle DL6WU: circa 8,6 dBd per 1 λ di boom e 2,4 dB a ogni raddoppio
    // (es. 9,5 λ → circa 16,4 dBd; una tabella DL6WU per 476,9 MHz dà 16,7 dBd con 6 m = 9,5 λ).
    public static double GainDbd(double boomM, double freqMHz)
    {
        double lam = 299.792458 / freqMHz;
        double bl = Math.Max(0.3, boomM / lam);
        return 8.6 + 8.0 * Math.Log10(bl);
    }
    public static double GainDbi(double boomM, double freqMHz) => GainDbd(boomM, freqMHz) + 2.15;
}

/// <summary>Esito del calcolo su un percorso.</summary>
public sealed class LinkResult
{
    public double DistKm, AzDeg, FreqMHz, K;
    public bool Los;
    public double LfsDb, LdiffDb, LbullDb, LscatDb, LbaseDb, TropoGainDb, LductDb, LpredDb;
    public double ThetaMrad, ThetaTx, ThetaRx;
    public double DuctFrac;           // frazione del percorso con condotto utile alla banda
    public bool DuctCoupled;
    public string Mechanism = "";
    public double GtxDbi, GrxDbi, EirpDbm, PrxDbm;
    public double NoiseSsbDbm, SnrSsb, SnrCw, SnrFt8;
    public double TgalK, TmanK, TaK, TsysK, PantDbm;
    public double[] DistAlong = Array.Empty<double>(), Terrain = Array.Empty<double>();
    public double HtxAsl, HrxAsl;
    // strato fondante lungo la tratta
    public string LayerName = "";
    public bool LayerTraps;                 // lo strato intrappola la banda (condotto) o è solo super-rifrattivo
    public double LayerFromKm, LayerToKm, LayerFrac, TopMean, TopMin, TopMax, BaseMean, TiltMkm, DeficitMean, LambdaMin;
    // doppio calcolo dei condotti potenziali lungo lo strato fondante (A = salto in 50 m, B = spessore P.453)
    public int DualN, DualChosenB;
    public double DeficitMeanA, DeficitMeanB, LambdaMinA, LambdaMinB, ClimThickMean, AnomalyMean;
    public double CouplingTxDb, CouplingRxDb, ResidualDb, TerrainInDuctKm;
    public string TxVsDuct = "", RxVsDuct = "";
    public double FresnelMinRatio = double.NaN;        // minimo di (spazio libero sopra il terreno) / raggio della 1ª zona di Fresnel
    public List<(double km, double hM, double ratio, double nu)> Obstacles = new();
    public double[] RunDist = Array.Empty<double>(), RunTop = Array.Empty<double>(), RunBase = Array.Empty<double>();
    public int TilesMissing;
    public bool ForecastUsed;
    public List<string> Notes = new();

    /// <summary>S-meter IARU per VHF/UHF: S9 = −93 dBm, 6 dB per unità.</summary>
    public static string SMeter(double dbm)
    {
        double s = 9 + (dbm + 93) / 6.0;
        if (s >= 9) { double over = dbm + 93; return over < 1 ? "S9" : $"S9+{over:0}"; }
        if (s < 0) return "S0";
        return "S" + Math.Floor(s).ToString("0");
    }
}

/// <summary>
/// Calcolo del segnale su un percorso: profilo del terreno (SRTM1), raggio terrestre equivalente dalla previsione,
/// diffrazione con il metodo di Bullington (ITU-R P.452, § 4.2.1), troposcatter (ITU-R P.452 eq. 45 al 50% del tempo),
/// guadagno tropo della previsione propria e stima del condotto. Ricevitore: rumore termico con la cifra di rumore.
/// </summary>
public static class LinkCalc
{
    public const double Re = 6371.0;

    static double J(double nu) => nu > -0.78 ? 6.9 + 20 * Math.Log10(Math.Sqrt((nu - 0.1) * (nu - 0.1) + 1) + nu - 0.1) : 0;

    /// <summary>
    /// Rumore esterno secondo ITU-R P.372: galattico Fa = 52 − 23 log f (mediana) e rumore artificiale
    /// Fa = c − d log f per l'ambiente del ricevitore (città 76,8/27,7; residenziale 72,5/27,7; rurale 67,2/27,7;
    /// rurale tranquillo 53,6/28,6). Fa in dB sopra kT0b, T = 290·10^(Fa/10). Le curve del rumore artificiale
    /// sono date fino a 250 MHz: sopra si prolunga la stessa legge. env = "manual" usa manualK.
    /// </summary>
    public static (double tgal, double tman, double ta) AntennaTemp(double fMHz, string env, double manualK)
    {
        double lf = Math.Log10(fMHz);
        double tgal = 290 * Math.Pow(10, (52 - 23 * lf) / 10);
        double c, dd;
        switch (env)
        {
            case "city": c = 76.8; dd = 27.7; break;
            case "residential": c = 72.5; dd = 27.7; break;
            case "rural": c = 67.2; dd = 27.7; break;
            case "manual": return (0, 0, Math.Max(1, manualK));
            default: c = 53.6; dd = 28.6; break;     // rurale tranquillo
        }
        double tman = 290 * Math.Pow(10, (c - dd * lf) / 10);
        return (tgal, tman, tgal + tman);
    }

    /// <summary>
    /// Temperatura di sistema riportata ai morsetti d'antenna: antenna + cavo (perdita Lc a 290 K) + ricevitore (NF)
    /// visto attraverso il cavo: Tsys = Ta + 290·(Lc − 1) + 290·(F − 1)·Lc.
    /// </summary>
    public static double SystemTempK(double ta, double cableDb, double nfDb)
    {
        double lc = Math.Pow(10, cableDb / 10), f = Math.Pow(10, nfDb / 10);
        return ta + 290 * (lc - 1) + 290 * (f - 1) * lc;
    }

    /// <summary>Potenza di rumore kTB in dBm.</summary>
    public static double NoiseDbm(double tsysK, double bwHz) => 10 * Math.Log10(1.380649e-23 * tsysK * bwHz) + 30;

    /// <summary>Punti del cerchio massimo ogni stepKm (almeno 2 punti).</summary>
    public static List<LatLon> PathPoints(LatLon a, LatLon b, double stepKm, out double[] dist)
    {
        double d = Geo.DistKm(a, b);
        int n = Math.Max(2, (int)Math.Ceiling(d / stepKm) + 1);
        var pts = Geo.GreatCircle(a, b, n - 1);
        if (pts.Count != n) { pts = new List<LatLon>(); for (int i = 0; i < n; i++) pts.Add(Interp(a, b, (double)i / (n - 1))); }
        dist = new double[pts.Count];
        for (int i = 0; i < pts.Count; i++) dist[i] = d * i / (pts.Count - 1);
        return pts;
    }

    static LatLon Interp(LatLon a, LatLon b, double f) => new(a.Lat + (b.Lat - a.Lat) * f, a.Lon + (b.Lon - a.Lon) * f);

    /// <summary>Raggio equivalente dal gradiente del primo km (k = 157/(157 + dN/dh)), limitato a 0,6–4.</summary>
    public static double KFromDn(double dn1km)
    {
        if (double.IsNaN(dn1km)) return 4.0 / 3;
        double den = 157 + dn1km;
        if (den <= 157 / 4.0) return 4;
        return Math.Max(0.6, Math.Min(4, 157 / den));
    }

    public sealed class Options
    {
        public double FreqMHz = 144.3;
        public DateTime Utc;
        public double BwSsb = 2500, BwCw = 500, BwFt8 = 2500;
        public double ReqSsb = 6, ReqCw = 0, ReqFt8 = -21;
        public string NoiseEnv = "quiet";      // ambiente del ricevitore (P.372)
        public double ManualTaK = 300;
    }

    /// <summary>Calcolo completo; prof = profili di previsione lungo il percorso (o null se non c'è previsione).</summary>
    public static async Task<LinkResult> Compute(Station tx, Station rx, Options o, Srtm srtm, IList<ProfileResult> prof, CancellationToken ct)
    {
        var r = new LinkResult { FreqMHz = o.FreqMHz };
        r.DistKm = Geo.DistKm(tx.Pos, rx.Pos);
        r.AzDeg = Geo.Azimuth(tx.Pos, rx.Pos);
        CalcLog.Section($"Calcolo del segnale {tx.Loc} → {rx.Loc}, {o.FreqMHz:0.0} MHz, ora {o.Utc:dd/MM HH:mm} UTC");
        CalcLog.W($"TX {tx.Loc}: {tx.Pos.Lat:0.00000}, {tx.Pos.Lon:0.00000}; {(tx.Precise ? $"locatore preciso, antenna {tx.AglM:0.0} m dal suolo" : $"locatore a {tx.Loc.Length} caratteri, antenna {tx.AslM:0} m s.l.m.")}; {tx.PowerW:0.#} W, cavo {tx.CableDb:0.0} dB, boom {tx.BoomM:0.00} m");
        CalcLog.W($"RX {rx.Loc}: {rx.Pos.Lat:0.00000}, {rx.Pos.Lon:0.00000}; {(rx.Precise ? $"locatore preciso, antenna {rx.AglM:0.0} m dal suolo" : rx.AslM > 0 ? $"antenna {rx.AslM:0} m s.l.m." : $"antenna {rx.AglM:0.0} m sul suolo SRTM")}; boom {rx.BoomM:0.00} m, cavo {rx.CableDb:0.0} dB, NF {rx.NfDb:0.0} dB");
        CalcLog.W($"distanza {r.DistKm:0.000} km, azimut {r.AzDeg:0.0}°");
        if (r.DistKm < 0.05) { r.Notes.Add("le due stazioni coincidono"); return r; }
        double step = Math.Max(0.03, Math.Min(0.25, r.DistKm / 4000));      // 30 m fino a 120 km, poi più largo
        var pts = PathPoints(tx.Pos, rx.Pos, step, out var dist);
        var (h, ok) = await srtm.Heights(pts, ct);
        r.TilesMissing = srtm.Missing;
        {
            int imax = 0; for (int i = 1; i < h.Length; i++) if (h[i] > h[imax]) imax = i;
            CalcLog.W($"profilo SRTM1: {pts.Count} punti ogni {step * 1000:0} m; suolo TX {h[0]:0.0} m, suolo RX {h[h.Length - 1]:0.0} m; quota massima {h[imax]:0} m a {dist[imax]:0.0} km; punti senza riquadro {ok.Count(x => !x)}");
        }
        r.DistAlong = dist; r.Terrain = h;
        tx.GroundM = h[0]; rx.GroundM = h[h.Length - 1];
        double hts = tx.AntennaAsl, hrs = rx.AntennaAsl;
        if (!tx.Precise && tx.AslM > 0 && hts < tx.GroundM + 1) { r.Notes.Add($"la quota s.l.m. indicata ({tx.AslM:0} m) è sotto il suolo SRTM ({tx.GroundM:0} m): uso il suolo + 2 m"); hts = tx.GroundM + 2; }
        r.HtxAsl = hts; r.HrxAsl = hrs;
        CalcLog.W($"antenne sul mare: TX {hts:0.0} m, RX {hrs:0.0} m");

        // ---- raggio equivalente dalla previsione (media del primo km lungo il percorso)
        double k = 4.0 / 3;
        var good = prof?.Where(p => p != null).ToList() ?? new List<ProfileResult>();
        if (good.Count > 0) { k = KFromDn(good.Average(p => p.DN1km)); r.ForecastUsed = true; }
        r.K = k;
        CalcLog.W($"profili di previsione sul percorso: {good.Count} su {prof?.Count ?? 0}; ΔN medio primo km {(good.Count > 0 ? good.Average(p => p.DN1km).ToString("0.0") : "—")} N/km → k = 157/(157+ΔN) = {k:0.000} (limitato fra 0,6 e 4)");
        if (prof != null) for (int i = 0; i < prof.Count; i++) CalcLog.Profile($"punto {i + 1}/{prof.Count} del percorso", prof[i]);
        double ae = k * Re, d = r.DistKm, lam = 299.792458 / o.FreqMHz, fGHz = o.FreqMHz / 1000;
        double ce = 1 / ae;

        // ---- spazio libero
        r.LfsDb = 92.44 + 20 * Math.Log10(fGHz) + 20 * Math.Log10(d);
        CalcLog.W($"raggio equivalente ae = k·6371 = {ae:0} km; λ = {lam:0.000} m; spazio libero Lfs = 92,44 + 20 log f(GHz) + 20 log d = {r.LfsDb:0.00} dB");

        // ---- diffrazione: Bullington sul profilo reale + correzione di terra sferica (metodo "delta-Bullington", P.452 § 4.2)
        int n = h.Length;
        double lbulla = Bullington(h, dist, hts, hrs, d, ce, lam, out bool los, out double stim, out double str, out int io);
        r.Los = los;
        // superficie liscia ai minimi quadrati (P.452 eq. 161–164) e altezze efficaci delle antenne sopra di essa
        double v1 = 0, v2 = 0;
        for (int i = 1; i < n; i++)
        {
            double dd = dist[i] - dist[i - 1];
            v1 += dd * (h[i] + h[i - 1]);
            v2 += dd * (h[i] * (2 * dist[i] + dist[i - 1]) + h[i - 1] * (dist[i] + 2 * dist[i - 1]));
        }
        double hst = Math.Min((2 * v1 * d - v2) / (d * d), h[0]), hsr = Math.Min((v2 - v1 * d) / (d * d), h[n - 1]);
        double hte = Math.Max(1, hts - hst), hre = Math.Max(1, hrs - hsr);
        var zero = new double[n];
        double lbulls = Bullington(zero, dist, hte, hre, d, ce, lam, out _, out _, out _, out _);
        double ldsph = SphericalLoss(d, hte, hre, ae, o.FreqMHz);
        r.LbullDb = lbulla + Math.Max(ldsph - lbulls, 0);
        r.LdiffDb = r.LfsDb + r.LbullDb;
        CalcLog.W($"Bullington sul profilo: Stim {stim:0.000} m/km (ostacolo più alto visto dal TX: {h[io]:0} m a {dist[io]:0.0} km), Str {str:0.000} m/km → {(los ? "in vista" : "oltre l'orizzonte")}; Lbulla = {lbulla:0.00} dB");
        CalcLog.W($"terra liscia: superficie {hst:0} m al TX e {hsr:0} m al RX → altezze efficaci {hte:0.0} / {hre:0.0} m; Bullington liscio {lbulls:0.00} dB; terra sferica (P.526, primo termine) {ldsph:0.00} dB");
        // ---- prima zona di Fresnel lungo il raggio diretto: ostacoli che la invadono (ν di Fresnel-Kirchhoff, P.526)
        {
            r.FresnelMinRatio = double.MaxValue;
            var cand = new List<(double km, double hM, double ratio, double nu)>();
            for (int i = 1; i < n - 1; i++)
            {
                double d1 = dist[i], d2 = d - dist[i];
                double ray = hts + (hrs - hts) * d1 / d;                           // raggio diretto (m s.l.m.)
                double ground = h[i] + d1 * d2 / (2 * ae) * 1000;                  // terreno sollevato dalla curvatura
                double f1 = Math.Sqrt(lam * d1 * d2 * 1000 / d);                   // raggio 1ª zona (m), d in km
                double ratio = (ray - ground) / f1;
                r.FresnelMinRatio = Math.Min(r.FresnelMinRatio, ratio);
                bool peak = h[i] >= h[i - 1] && h[i] >= h[i + 1];
                if (peak && ratio < 0.6) cand.Add((d1, h[i], ratio, -ratio * Math.Sqrt(2)));   // ν = −√2 · spazio/F1
            }
            // le cime più invadenti, una ogni 5 km
            foreach (var c in cand.OrderBy(c => c.ratio))
            {
                if (r.Obstacles.Any(o2 => Math.Abs(o2.km - c.km) < 5)) continue;
                r.Obstacles.Add(c);
                if (r.Obstacles.Count >= 6) break;
            }
            r.Obstacles = r.Obstacles.OrderBy(c => c.km).ToList();
            CalcLog.W($"1ª zona di Fresnel: rapporto minimo spazio/raggio {r.FresnelMinRatio:0.00} (≥ 0,6 = libera); cime che la invadono: " +
                      (r.Obstacles.Count == 0 ? "nessuna" : string.Join("; ", r.Obstacles.Select(c => $"{c.hM:0} m a {c.km:0.0} km (spazio/F1 {c.ratio:0.0}, ν {c.nu:0.0}, J(ν) {J(c.nu):0.0} dB)"))));
        }
        CalcLog.W($"diffrazione = Lbulla + max(Ldsph − Lbulls, 0) = {r.LbullDb:0.00} dB oltre lo spazio libero → Ldiff = {r.LdiffDb:0.00} dB");

        // ---- angoli d'orizzonte (mrad) e troposcatter (P.452 eq. 45, p = 50%)
        double thT = double.MinValue, thR = double.MinValue;
        for (int i = 1; i < n - 1; i++)
        {
            thT = Math.Max(thT, (h[i] - hts) / dist[i] - 1000 * dist[i] / (2 * ae));
            thR = Math.Max(thR, (h[i] - hrs) / (d - dist[i]) - 1000 * (d - dist[i]) / (2 * ae));
        }
        double thTd = (hrs - hts) / d - 1000 * d / (2 * ae), thRd = (hts - hrs) / d - 1000 * d / (2 * ae);
        if (r.Los || n < 3) { thT = thTd; thR = thRd; }
        else { thT = Math.Max(thT, thTd); thR = Math.Max(thR, thRd); }
        r.ThetaTx = thT; r.ThetaRx = thR;
        r.ThetaMrad = 1000 * d / ae + thT + thR;
        r.GtxDbi = Dl6wu.GainDbi(tx.BoomM, o.FreqMHz); r.GrxDbi = Dl6wu.GainDbi(rx.BoomM, o.FreqMHz);
        double n0 = good.Count > 0 ? good.Average(p => p.Ns) : 320;
        double lf = 25 * Math.Log10(fGHz) - 2.5 * Math.Pow(Math.Log10(fGHz / 2), 2);   // f in GHz
        double lc = 0.051 * Math.Exp(0.055 * (r.GtxDbi + r.GrxDbi));
        r.LscatDb = 190.1 + lf + 20 * Math.Log10(d) + 0.573 * Math.Max(0, r.ThetaMrad) - 0.15 * n0 + lc;
        CalcLog.W($"angoli d'orizzonte: TX {thT:0.00} mrad, RX {thR:0.00} mrad; θ = 1000 d/ae + θt + θr = {r.ThetaMrad:0.00} mrad");
        CalcLog.W($"troposcatter P.452 eq. 45 (p = 50%): Lf {lf:0.00}, N0 {n0:0.0}, Lc = 0,051·e^(0,055(Gt+Gr)) {lc:0.00} → Lbs = {r.LscatDb:0.00} dB (Gt {r.GtxDbi:0.0} dBi, Gr {r.GrxDbi:0.0} dBi, DL6WU)");

        // ---- combinazione (somma delle potenze) senza tropo
        r.LbaseDb = -10 * Math.Log10(Math.Pow(10, -r.LdiffDb / 10) + Math.Pow(10, -r.LscatDb / 10));
        r.LbaseDb = Math.Max(r.LbaseDb, r.LfsDb);
        CalcLog.W($"senza tropo (somma delle potenze di diffrazione e scatter, non sotto lo spazio libero): {r.LbaseDb:0.00} dB");

        // ---- strato fondante: continuità, quota e inclinazione dello strato lungo la tratta
        r.LpredDb = r.LbaseDb;
        r.Mechanism = r.Los ? "visibilità diretta" : r.LdiffDb <= r.LscatDb ? "diffrazione" : "troposcatter";
        if (good.Count > 0)
        {
            int np = prof.Count;
            var pd = new double[np];
            for (int i = 0; i < np; i++) pd[i] = np > 1 ? d * i / (np - 1) : 0;
            double cell = np > 1 ? d / (np - 1) : d;
            var keys = prof.Where(p => p != null).SelectMany(p => p.Layers).Select(l => l.Key).Distinct().ToList();
            (int key, int a, int b, bool traps, double score) best = (-1, 0, -1, false, 0);
            foreach (bool wantTrap in new[] { true, false })
            {
                foreach (var key in keys)
                {
                    LayerDuct At(int i) => prof[i]?.Layers.FirstOrDefault(l => l.Key == key && (wantTrap ? l.Traps(o.FreqMHz) : l.ExcessN > 0));
                    int ra = -1, rb = -2, ca = -1;
                    for (int i = 0; i <= np; i++)
                    {
                        bool on = i < np && At(i) != null;
                        if (on && ca < 0) ca = i;
                        if (!on && ca >= 0) { if (i - 1 - ca > rb - ra) { ra = ca; rb = i - 1; } ca = -1; }
                    }
                    if (ra < 0) continue;
                    double score = (rb - ra + 1) + (wantTrap ? 1000 : 0);
                    if (score > best.score) best = (key, ra, rb, wantTrap, score);
                }
                if (best.key >= 0 && best.traps) break;
            }
            if (best.key >= 0)
            {
                var run = new List<(double dist, LayerDuct l)>();
                for (int i = best.a; i <= best.b; i++)
                {
                    var l = prof[i].Layers.First(x => x.Key == best.key && (best.traps ? x.Traps(o.FreqMHz) : x.ExcessN > 0));
                    run.Add((pd[i], l));
                }
                r.LayerName = run[0].l.Name; r.LayerTraps = best.traps;
                r.LayerFromKm = Math.Max(0, run[0].dist - cell / 2); r.LayerToKm = Math.Min(d, run[run.Count - 1].dist + cell / 2);
                r.LayerFrac = (r.LayerToKm - r.LayerFromKm) / d;
                Func<LayerDuct, double> top = l => l.Deficit > 0 ? l.DuctTop : l.ZTop, bas = l => l.Deficit > 0 ? l.DuctBase : l.ZBot;
                r.TopMean = run.Average(x => top(x.l)); r.TopMin = run.Min(x => top(x.l)); r.TopMax = run.Max(x => top(x.l));
                r.BaseMean = run.Average(x => bas(x.l));
                r.DeficitMean = run.Average(x => x.l.Deficit);
                r.LambdaMin = run.Min(x => x.l.LambdaMax);
                var dual = run.Where(x => x.l.Dual).Select(x => x.l).ToList();
                if (dual.Count > 0)
                {
                    r.DualN = dual.Count; r.DualChosenB = dual.Count(l => l.Chosen == "B");
                    r.DeficitMeanA = dual.Average(l => l.DeficitA); r.DeficitMeanB = dual.Average(l => l.DeficitB);
                    r.LambdaMinA = dual.Min(l => l.LambdaA); r.LambdaMinB = dual.Min(l => l.LambdaB);
                    r.ClimThickMean = dual.Average(l => l.ClimThick); r.AnomalyMean = dual.Average(l => l.Anomaly);
                }
                r.RunDist = run.Select(x => x.dist).ToArray(); r.RunTop = run.Select(x => top(x.l)).ToArray(); r.RunBase = run.Select(x => bas(x.l)).ToArray();
                if (run.Count >= 2)
                {
                    double mx = run.Average(x => x.dist), my = r.TopMean, sxy = 0, sxx = 0;
                    foreach (var x in run) { sxy += (x.dist - mx) * (top(x.l) - my); sxx += (x.dist - mx) * (x.dist - mx); }
                    r.TiltMkm = sxx > 0 ? sxy / sxx : 0;
                }
                // terreno che entra nello strato lungo il tratto in cui esiste
                for (int i = 0; i < n; i++)
                    if (dist[i] >= r.LayerFromKm && dist[i] <= r.LayerToKm)
                    {
                        double bse = BaseAt(r, dist[i]);
                        if (h[i] >= bse) r.TerrainInDuctKm += i > 0 ? dist[i] - dist[i - 1] : 0;
                    }
                CalcLog.W($"strato fondante: {r.LayerName}, {(best.traps ? $"condotto che intrappola λ {299.792458 / o.FreqMHz:0.00} m" : "solo super-rifrattivo (non intrappola la banda)")}; continuo da {r.LayerFromKm:0} a {r.LayerToKm:0} km ({r.LayerFrac * 100:0}% della tratta, {run.Count} punti su {np})");
                CalcLog.W($"   quota della cima {r.TopMean:0} m (da {r.TopMin:0} a {r.TopMax:0}), base media {r.BaseMean:0} m, inclinazione {r.TiltMkm:+0.0;-0.0} m/km, deficit medio {r.DeficitMean:0.0} M, λmax minima {r.LambdaMin:0.00} m; terreno dentro lo strato per {r.TerrainInDuctKm:0} km");
            }
            if (r.DualN > 0)
                CalcLog.W($"   doppio calcolo su {r.DualN} punti (condotti potenziali): A salto in 50 m → deficit medio {r.DeficitMeanA:0.0} M, λmax minima {r.LambdaMinA:0.00} m; " +
                          $"B spessore P.453 {r.ClimThickMean:0} m → {r.DeficitMeanB:0.0} M, {r.LambdaMinB:0.00} m; peggiore = B in {r.DualChosenB} punti su {r.DualN}; anomalia media {r.AnomalyMean:0.0}× la norma");
            else CalcLog.W("strato fondante: nessuno strato con calo di N oltre il normale continuo su almeno un punto");

            // guadagno: conta solo lo strato continuo, in proporzione al tratto che copre (Bean e Dutton: 10–25 dB per strato)
            double g = 0;
            if (best.key >= 0)
            {
                double gl = best.traps ? Math.Min(25, 20 + r.DeficitMean / 5.0)
                                       : Enumerable.Range(best.a, best.b - best.a + 1).Average(i => prof[i].LayerGainDb);
                g = gl * r.LayerFrac;
                CalcLog.W($"guadagno dello strato {gl:0.0} dB × frazione continua {r.LayerFrac:0.00} = {g:0.0} dB");
            }
            int nsub = good.Count(p => p.Sub);
            if (nsub * 2 > good.Count) { g = -10.0 * nsub / good.Count; CalcLog.W($"sub-rifrazione su {nsub} profili su {good.Count}: {g:0.0} dB"); }
            r.TropoGainDb = g;
            r.DuctFrac = best.traps ? r.LayerFrac : 0;
            double lpred = r.LbaseDb - g;

            // condotto su (quasi) tutta la tratta: spazio libero + accoppiamento delle antenne + attenuazione residua
            if (best.traps && r.LayerFrac >= 0.8 && !r.Los)
            {
                double Coupling(double hAsl, double dk, out string where)
                {
                    double tp = TopAt(r, dk), bs = BaseAt(r, dk);
                    if (hAsl > tp) { where = $"sopra la cima di {hAsl - tp:0} m"; return Math.Min(40, 3 + (hAsl - tp) / 10); }
                    if (hAsl < bs) { where = $"sotto la base di {bs - hAsl:0} m"; return Math.Min(40, 3 + (bs - hAsl) / 10); }
                    where = "dentro"; return 3;
                }
                r.CouplingTxDb = Coupling(hts, 0, out var wt); r.CouplingRxDb = Coupling(hrs, d, out var wr);
                r.TxVsDuct = wt; r.RxVsDuct = wr;
                r.DuctCoupled = wt == "dentro" && wr == "dentro";
                // attenuazione residua nel condotto: P.452 eq. 49, γd = 5·10⁻⁵ · ae · f(GHz)^(1/3) dB/mrad sulla distanza angolare
                double gammaD = 5e-5 * ae * Math.Pow(fGHz, 1.0 / 3);
                r.ResidualDb = gammaD * (1000 * d / ae);
                r.LductDb = r.LfsDb + r.CouplingTxDb + r.CouplingRxDb + r.ResidualDb;
                CalcLog.W($"condotto su tutta la tratta: TX {wt} ({r.CouplingTxDb:0.0} dB), RX {wr} ({r.CouplingRxDb:0.0} dB); attenuazione residua γd {gammaD:0.000} dB/mrad × {1000 * d / ae:0.0} mrad = {r.ResidualDb:0.0} dB; Lduct = {r.LductDb:0.00} dB");
                if (r.LductDb < lpred) { lpred = r.LductDb; r.Mechanism = "condotto"; }
            }
            else if (best.key >= 0 && g >= 3 && !r.Los) r.Mechanism = best.traps ? "tropo (condotto parziale)" : "tropo (super-rifrazione)";
            r.LpredDb = Math.Max(r.LfsDb, lpred);
            CalcLog.W($"perdita prevista {r.LpredDb:0.00} dB, meccanismo: {r.Mechanism}");
        }
        else r.Notes.Add("nessuna previsione GFS sul percorso: calcolo con atmosfera normale (k = 4/3)");

        // ---- livello e rapporto segnale/rumore
        r.EirpDbm = 10 * Math.Log10(Math.Max(0.001, tx.PowerW) * 1000) - tx.CableDb + r.GtxDbi;
        // tutto riportato ai morsetti dell'antenna ricevente: il cavo attenua il segnale e aggiunge rumore
        r.PantDbm = r.EirpDbm - r.LpredDb + r.GrxDbi;
        r.PrxDbm = r.PantDbm - rx.CableDb;                       // all'ingresso del ricevitore (S-meter)
        var (tg, tm, ta) = AntennaTemp(o.FreqMHz, o.NoiseEnv, o.ManualTaK);
        r.TgalK = tg; r.TmanK = tm; r.TaK = ta;
        r.TsysK = SystemTempK(ta, rx.CableDb, rx.NfDb);
        r.NoiseSsbDbm = NoiseDbm(r.TsysK, o.BwSsb);
        r.SnrSsb = r.PantDbm - r.NoiseSsbDbm;
        r.SnrCw = r.PantDbm - NoiseDbm(r.TsysK, o.BwCw);
        r.SnrFt8 = r.PantDbm - NoiseDbm(r.TsysK, o.BwFt8);
        CalcLog.W($"EIRP = {tx.PowerW:0.#} W ({10 * Math.Log10(Math.Max(0.001, tx.PowerW) * 1000):0.0} dBm) − cavo {tx.CableDb:0.0} + {r.GtxDbi:0.0} dBi = {r.EirpDbm:0.00} dBm");
        CalcLog.W($"ai morsetti dell'antenna RX: EIRP − {r.LpredDb:0.00} + {r.GrxDbi:0.0} dBi = {r.PantDbm:0.00} dBm; all'ingresso del ricevitore (− cavo {rx.CableDb:0.0}) {r.PrxDbm:0.00} dBm ({LinkResult.SMeter(r.PrxDbm)})");
        CalcLog.W(o.NoiseEnv == "manual"
            ? $"rumore: temperatura d'antenna impostata a mano {ta:0} K"
            : $"rumore esterno (ITU-R P.372, ambiente {o.NoiseEnv}): galattico {tg:0} K (Fa {10 * Math.Log10(tg / 290):0.0} dB) + artificiale {tm:0} K (Fa {10 * Math.Log10(Math.Max(1e-9, tm) / 290):0.0} dB) = Ta {ta:0} K" + (o.FreqMHz > 250 ? " (curve del rumore artificiale prolungate oltre 250 MHz)" : ""));
        CalcLog.W($"Tsys = Ta + 290(Lc−1) + 290(F−1)Lc = {ta:0} + {290 * (Math.Pow(10, rx.CableDb / 10) - 1):0} + {290 * (Math.Pow(10, rx.NfDb / 10) - 1) * Math.Pow(10, rx.CableDb / 10):0} = {r.TsysK:0} K");
        CalcLog.W($"rumore kTsysB: SSB {o.BwSsb:0} Hz {r.NoiseSsbDbm:0.00} dBm → S/N {r.SnrSsb:0.0} dB (soglia {o.ReqSsb:0}); CW {o.BwCw:0} Hz → S/N {r.SnrCw:0.0} dB (soglia {o.ReqCw:0}); FT8 {o.BwFt8:0} Hz → S/N {r.SnrFt8:0.0} dB (soglia {o.ReqFt8:0})");
        if (r.TilesMissing > 0) r.Notes.Add($"{r.TilesMissing} riquadri SRTM1 non disponibili: lì il terreno vale 0 m");
        return r;
    }

    static double Interp(double[] xs, double[] ys, double x)
    {
        if (xs.Length == 0) return double.NaN;
        if (x <= xs[0]) return ys[0];
        for (int i = 1; i < xs.Length; i++) if (x <= xs[i]) return ys[i - 1] + (ys[i] - ys[i - 1]) * (x - xs[i - 1]) / Math.Max(1e-9, xs[i] - xs[i - 1]);
        return ys[ys.Length - 1];
    }
    static double TopAt(LinkResult r, double dk) => Interp(r.RunDist, r.RunTop, dk);
    static double BaseAt(LinkResult r, double dk) => Interp(r.RunDist, r.RunBase, dk);

    /// <summary>Perdita di Bullington (dB oltre lo spazio libero) su un profilo, come in ITU-R P.452 § 4.2.1.</summary>
    static double Bullington(double[] h, double[] dist, double hts, double hrs, double d, double ce, double lam, out bool los, out double stim, out double str, out int io)
    {
        int n = h.Length; stim = double.MinValue; io = 1;
        for (int i = 1; i < n - 1; i++)
        {
            double v = (h[i] + 500 * ce * dist[i] * (d - dist[i]) - hts) / dist[i];
            if (v > stim) { stim = v; io = i; }
        }
        str = (hrs - hts) / d;
        double luc;
        if (n < 3 || stim < str)
        {
            los = true;
            double numax = double.MinValue;
            for (int i = 1; i < n - 1; i++)
            {
                double di = dist[i];
                double nu = (h[i] + 500 * ce * di * (d - di) - (hts * (d - di) + hrs * di) / d) * Math.Sqrt(0.002 * d / (lam * di * (d - di)));
                numax = Math.Max(numax, nu);
            }
            luc = n < 3 ? 0 : J(numax);
        }
        else
        {
            los = false;
            double srim = double.MinValue;
            for (int i = 1; i < n - 1; i++) srim = Math.Max(srim, (h[i] + 500 * ce * dist[i] * (d - dist[i]) - hrs) / (d - dist[i]));
            double db = (hrs - hts + srim * d) / (stim + srim);
            db = Math.Max(0.001, Math.Min(d - 0.001, db));
            double nub = (hts + stim * db - (hts * (d - db) + hrs * db) / d) * Math.Sqrt(0.002 * d / (lam * db * (d - db)));
            luc = J(nub);
        }
        return luc + (1 - Math.Exp(-luc / 6)) * (10 + 0.02 * d);
    }

    /// <summary>
    /// Perdita di diffrazione su terra sferica liscia oltre l'orizzonte (dB oltre lo spazio libero), formula del primo
    /// termine di ITU-R P.526 § 3.1.1 con polarizzazione orizzontale e terreno medio (ε 22, σ 0,003 S/m).
    /// Entro l'orizzonte radio della terra liscia vale 0 (lì basta Bullington).
    /// </summary>
    public static double SphericalLoss(double d, double h1, double h2, double ae, double fMHz)
    {
        double dlos = Math.Sqrt(2 * ae) * (Math.Sqrt(0.001 * h1) + Math.Sqrt(0.001 * h2));
        if (d <= dlos) return 0;
        double k = 0.36 * Math.Pow(ae * fMHz, -1.0 / 3) * Math.Pow(Math.Pow(22 - 1, 2) + Math.Pow(18000 * 0.003 / fMHz, 2), -0.25);
        double beta = 1;
        double x = 2.188 * beta * Math.Pow(fMHz, 1.0 / 3) * Math.Pow(ae, -2.0 / 3) * d;
        double F = x >= 1.6 ? 11 + 10 * Math.Log10(x) - 17.6 * x : -20 * Math.Log10(x) - 5.6488 * Math.Pow(x, 1.425);
        double G(double hm)
        {
            double y = 9.575e-3 * beta * Math.Pow(fMHz, 2.0 / 3) * Math.Pow(ae, -1.0 / 3) * hm, b = beta * y;
            if (b > 2) return 17.6 * Math.Sqrt(b - 1.1) - 5 * Math.Log10(b - 1.1) - 8;
            if (b > 10 * k) return 20 * Math.Log10(b + 0.1 * b * b * b);
            if (b > k / 10) return 2 + 20 * Math.Log10(k) + 9 * Math.Log10(b / k) * (Math.Log10(b / k) + 1);
            return 2 + 20 * Math.Log10(k);
        }
        return Math.Max(0, -(F + G(h1) + G(h2)));
    }

    /// <summary>
    /// Copertura in ottica dalla stazione: per ogni azimut (passo 2°) la distanza fino alla quale un'antenna a hRx metri
    /// dal suolo vede ancora l'antenna trasmittente senza ostacoli (prima ombra) e il punto visibile più lontano,
    /// con il raggio equivalente k. Il terreno viene da SRTM1 (passo 150 m).
    /// </summary>
    public static async Task<(double[] az, double[] firstShadowKm, double[] farthestKm)> LineOfSight(Station tx, double hRx, double k, double maxKm, Srtm srtm, CancellationToken ct)
    {
        const int naz = 180; const double stepKm = 0.15;
        int ns = (int)(maxKm / stepKm);
        var pts = new List<LatLon>(naz * ns + 1) { tx.Pos };
        for (int a = 0; a < naz; a++)
            for (int s = 1; s <= ns; s++) pts.Add(Geo.Destination(tx.Pos, a * 360.0 / naz, s * stepKm));
        var (h, _) = await srtm.Heights(pts, ct);
        tx.GroundM = h[0];
        double hts = tx.AntennaAsl, ae = k * Re;
        CalcLog.Section($"Copertura in ottica da {tx.Loc}");
        CalcLog.W($"suolo SRTM1 {tx.GroundM:0.0} m, antenna {hts:0.0} m s.l.m., ricevente {hRx:0.0} m dal suolo, k {k:0.000} (ae {ae:0} km), raggio {maxKm:0} km, {naz} azimut, passo {stepKm * 1000:0} m, {pts.Count} punti");
        var az = new double[naz]; var first = new double[naz]; var far = new double[naz];
        for (int a = 0; a < naz; a++)
        {
            az[a] = a * 360.0 / naz;
            double maxSlope = double.MinValue; first[a] = -1; far[a] = 0;
            for (int s = 1; s <= ns; s++)
            {
                double dk = s * stepKm, hg = h[1 + a * ns + (s - 1)];
                double drop = dk * dk / (2 * ae) * 1000;         // abbassamento per la curvatura (m)
                double slopeGround = (hg - drop - hts) / dk;      // pendenza verso il suolo
                double slopeRx = (hg + hRx - drop - hts) / dk;    // pendenza verso l'antenna ricevente
                bool visible = slopeRx >= maxSlope;
                if (visible) far[a] = dk;
                else if (first[a] < 0) first[a] = dk;
                maxSlope = Math.Max(maxSlope, slopeGround);
            }
            if (first[a] < 0) first[a] = maxKm;
            if (a % 5 == 0) CalcLog.W($"azimut {az[a],5:0}°: prima ombra {first[a],6:0.0} km, visibile più lontano {far[a],6:0.0} km");
        }
        return (az, first, far);
    }
}
