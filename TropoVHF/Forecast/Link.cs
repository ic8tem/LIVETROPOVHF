using TropoVHF.Core;

namespace TropoVHF.Forecast;

/// <summary>Locatore Maidenhead da 4 a 10 caratteri (a 8 e 10 caratteri il punto è preciso a circa 400 m e 20 m).</summary>
public static class LocatorX
{
    /// <summary>Locatore a 10 caratteri (es. JN70CN83CL): campo, quadrato, sottoquadrato, quadrato esteso, sottoquadrato esteso.</summary>
    public static string To10(double lat, double lon)
    {
        double x = lon + 180, y = lat + 90;
        if (x < 0 || x >= 360 || y < 0 || y >= 180) return "";
        var c = new char[10];
        c[0] = (char)('A' + (int)(x / 20)); c[1] = (char)('A' + (int)(y / 10));
        x %= 20; y %= 10;
        c[2] = (char)('0' + (int)(x / 2)); c[3] = (char)('0' + (int)y);
        x %= 2; y %= 1;
        int sx = Math.Min(23, (int)(x * 12)), sy = Math.Min(23, (int)(y * 24));
        c[4] = (char)('A' + sx); c[5] = (char)('A' + sy);
        x -= sx / 12.0; y -= sy / 24.0;
        int ex = Math.Min(9, (int)(x * 120)), ey = Math.Min(9, (int)(y * 240));
        c[6] = (char)('0' + ex); c[7] = (char)('0' + ey);
        x -= ex / 120.0; y -= ey / 240.0;
        c[8] = (char)('A' + Math.Min(23, (int)(x * 2880))); c[9] = (char)('A' + Math.Min(23, (int)(y * 5760)));
        return new string(c);
    }

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
    public bool Precise;          // locatore a 8 o 10 caratteri: suolo dal profilo DEM 30 m
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
/// <summary>Un rilievo vicino usato come diffusore («via monte»).</summary>
public sealed class ViaPeak
{
    public double AzDeg, DistKm, HeightM, L1Db, L2Db, ConvDb, TotalDb;
    public string Mechanism2 = "", Loc10 = "";
    /// <summary>Opzione «senza perdita fino al monte»: la stazione è portata sul punto, conta solo la tratta dal monte al corrispondente.</summary>
    public double IdealDb => L2Db;
    /// <summary>Perdita usata per scegliere il puntamento: quella ideale con l'opzione accesa, altrimenti il totale bistatico.</summary>
    public double RankDb => Refractivity.ViaIdeal ? L2Db : TotalDb;
    public LatLon Pos;
}

public sealed class LinkResult
{
    public double DistKm, AzDeg, FreqMHz, K;
    public bool Los;
    public double LfsDb, LdiffDb, LbullDb, LscatDb, LbaseDb, TropoGainDb, LductDb, LpredDb;
    // 1.3.2: ITM (Longley-Rice, NTIA) in atmosfera normale e tratto finale dopo l'uscita dal condotto
    public double ItmDb = double.NaN, Itm10Db = double.NaN, Itm1Db = double.NaN, ItmDeltaH = double.NaN, ItmN0 = double.NaN;
    public string ItmMode = "", ItmClimate = "", ItmErr = "";
    public double ExitKm = double.NaN, ExitHeightM = double.NaN, ItmTailDb = double.NaN, LexitDb = double.NaN;
    public string ItmTailMode = "";
    public double ThetaMrad, ThetaTx, ThetaRx;
    // rilievi vicini alle stazioni con il tetto di attenuazione (criterio dell'autore)
    public double NearTxMrad = double.NaN, NearTxKm, NearTxSavedDb, NearRxMrad = double.NaN, NearRxKm, NearRxSavedDb;
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
    // terra e mare lungo la tratta (CSIRO 2026: decide il tratto su terra); corridoio con strati di almeno 40 m
    public double SeaFrac, LandKm, LandInDuctKm, LandCov = double.NaN, FracUsed;
    // terzo calcolo: ITU-R P.452-16 § 4.5 (condotti e riflessione su strati) con ΔN del GFS e p = 20% come calibrazione
    public double P452DeltaN, P452K50, P452Beta, P452Lba, P452Lbs, P452Lb, P452Alf, P452Ast, P452Asr, P452Act, P452Acr, P452Ag;
    public bool P452Ok;
    // 1.3.0: troposcatter con tre modelli affiancati (come TropoLink) e loro dispersione come incertezza
    public double P617Db = double.NaN, P617F, P617N0, P617DeltaN, RsgbDb = double.NaN, ScatSpreadDb;
    // situazione meteo lungo la tratta (frazione dei profili)
    public double SubsFrac, AscentFrac, SciroccoFrac, StabDbMean, SeaAirDbMean;
    public int SoftN; public double SoftMax;          // punti con condotto morbido e fattore massimo
    // volume comune: dove si incrociano i raggi radenti delle due stazioni (base del volume di scatter)
    /// <summary>1.3.4: rinforzo pomeridiano del troposcatter (cima dello strato rimescolato vicino al volume comune), dB e quota della cima.</summary>
    public double MixScatterDb, MixTopM = double.NaN;
    public double CvKm = double.NaN, CvHeightM = double.NaN, CvLat = double.NaN, CvLon = double.NaN, DeqKm = double.NaN;
    public double CvGrad = double.NaN, CvRH = double.NaN, CvN = double.NaN;   // aria alla base del volume (dal profilo più vicino)
    public string CvLayer = "";
    // equazione parabolica nel profilo M del GFS (solo confronto): perdita, stessa tratta con atmosfera standard, guadagno
    public double PeLossDb = double.NaN, PeStdDb = double.NaN, PeTotalDb = double.NaN;
    public double PeGainDb => PeStdDb - PeLossDb;
    public double PersistFrac = double.NaN;           // persistenza media lungo la tratta (0–1)
    // 1.3.2: ingresso in un condotto che comincia lontano (diffrazione fino al suo inizio, poi percorso nel condotto)
    public double EntryKm = double.NaN, EntryHeightM, EntryNu, EntryNuKm, EntryDiffDb, LentryDb = double.NaN;
    public List<ViaPeak> Via = new();                 // «via monte»: rilievi vicini su cui puntare le antenne
    public double BestPointAz = double.NaN;           // puntamento consigliato (azimut), diretto se nessun diffusore conviene
    // 1.3.2: stima aggressiva (soglia classica 79 N/km), μ1 di P.452 sul tratto di terra, aggancio delle stazioni al condotto, strato limite e creste
    public double AggrGainDb = double.NaN, LpredAggrDb = double.NaN;
    public double Mu1 = double.NaN, SeaWeight = 1, DtmKm;
    public double DuctCouplingF = double.NaN;
    public int RidgeN, RidgeCovered; public double RidgeMaxM, BlhRidgeDb;
    public double HighPFrac;
    public double EvapDuctM, EvapFrac, DayMixFrac, NightRadFrac; public int LowLevelsUsed;
    public string TxVsDuct = "", RxVsDuct = "";
    public double FresnelMinRatio = double.NaN;        // minimo di (spazio libero sopra il terreno) / raggio della 1ª zona di Fresnel
    public List<(double km, double hM, double ratio, double nu)> Obstacles = new();
    public double[] RunDist = Array.Empty<double>(), RunTop = Array.Empty<double>(), RunBase = Array.Empty<double>();
    public int TilesMissing;
    public bool ForecastUsed;
    public List<string> Notes = new();
    public Timing Times = new();       // tempi di reazione per tappa

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
/// Calcolo del segnale su un percorso: profilo del terreno (DEM 30 m), raggio terrestre equivalente dalla previsione,
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
        /// <summary>Persistenza media lungo la tratta (frazione delle 24 ore precedenti sopra 13 dB), dalla griglia; NaN = non nota.</summary>
        public double PersistFrac = double.NaN;
    }

    /// <summary>Calcolo completo; prof = profili di previsione lungo il percorso (o null se non c'è previsione).</summary>
    public static async Task<LinkResult> Compute(Station tx, Station rx, Options o, Srtm srtm, IList<ProfileResult> prof, CancellationToken ct, bool sub = false)
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
        r.Times.Lap("preparazione");
        var (h, ok) = await srtm.Heights(pts, ct);
        r.Times.Lap("terreno DEM 30 m");
        r.TilesMissing = srtm.Missing;
        {
            int imax = 0; for (int i = 1; i < h.Length; i++) if (h[i] > h[imax]) imax = i;
            CalcLog.W($"profilo DEM 30 m: {pts.Count} punti ogni {step * 1000:0} m; suolo TX {h[0]:0.0} m, suolo RX {h[h.Length - 1]:0.0} m; quota massima {h[imax]:0} m a {dist[imax]:0.0} km; punti senza riquadro {ok.Count(x => !x)}");
        }
        r.DistAlong = dist; r.Terrain = h;
        tx.GroundM = h[0]; rx.GroundM = h[h.Length - 1];
        double hts = tx.AntennaAsl, hrs = rx.AntennaAsl;
        if (!tx.Precise && tx.AslM > 0 && hts < tx.GroundM + 1) { r.Notes.Add($"la quota s.l.m. indicata ({tx.AslM:0} m) è sotto il suolo SRTM ({tx.GroundM:0} m): uso il suolo + 2 m"); hts = tx.GroundM + 2; }
        r.HtxAsl = hts; r.HrxAsl = hrs;
        CalcLog.W($"antenne sul mare: TX {hts:0.0} m, RX {hrs:0.0} m");
        // mare: quota SRTM a 0 m (anche i riquadri mancanti, che sono mare aperto)
        var sea = new bool[h.Length];
        double seaKm = 0;
        for (int i = 0; i < h.Length; i++) sea[i] = h[i] <= 0.5;
        for (int i = 1; i < h.Length; i++) { double dd = dist[i] - dist[i - 1]; if (sea[i]) seaKm += dd; else r.LandKm += dd; }
        r.SeaFrac = r.DistKm > 0 ? seaKm / r.DistKm : 0;
        CalcLog.W($"mare lungo la tratta {seaKm:0.0} km ({r.SeaFrac * 100:0}%), terra {r.LandKm:0.0} km");
        if (Refractivity.UseSeaWeight && seaKm > 0)
        {
            // ITU-R P.452 μ1: più lungo è il tratto continuo di terra (dtm), meno probabili i condotti; sul mare aperto μ1 → 1
            double run = 0, dtm = 0;
            for (int i = 1; i < h.Length; i++) { if (!sea[i]) { run += dist[i] - dist[i - 1]; dtm = Math.Max(dtm, run); } else run = 0; }
            double tau = 1 - Math.Exp(-4.12e-4 * Math.Pow(dtm, 2.41));
            r.DtmKm = dtm;
            r.Mu1 = Math.Min(1, Math.Pow(Math.Pow(10, -dtm / (16 - 6.6 * tau)) + Math.Pow(10, -5 * (0.496 + 0.354 * tau)), 0.2));
            r.SeaWeight = 0.8 + 0.4 * r.Mu1;
            CalcLog.W($"peso del mare (μ1 di P.452): tratto di terra continuo più lungo {dtm:0} km → μ1 {r.Mu1:0.00}, guadagno degli strati × {r.SeaWeight:0.00}");
        }

        // ---- raggio equivalente dalla previsione (media del primo km lungo il percorso)
        double k = 4.0 / 3;
        var good = prof?.Where(p => p != null).ToList() ?? new List<ProfileResult>();
        if (good.Count > 0) { k = KFromDn(good.Average(p => p.DN1km)); r.ForecastUsed = true; }
        r.K = k;
        CalcLog.W($"profili di previsione sul percorso: {good.Count} su {prof?.Count ?? 0}; ΔN medio primo km {(good.Count > 0 ? good.Average(p => p.DN1km).ToString("0.0") : "—")} N/km → k = 157/(157+ΔN) = {k:0.000} (limitato fra 0,6 e 4)");
        if (prof != null)
            for (int i = 0; i < prof.Count; i++)
            {
                // con la griglia larga più punti cadono nella stessa cella: il profilo si scrive una volta sola
                int same = -1; if (prof[i] != null) for (int j = 0; j < i; j++) if (ReferenceEquals(prof[j], prof[i])) { same = j; break; }
                if (same >= 0) CalcLog.W($"punto {i + 1}/{prof.Count} del percorso: stessa cella della griglia del punto {same + 1}");
                else CalcLog.Profile($"punto {i + 1}/{prof.Count} del percorso", prof[i]);
            }
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

        r.Times.Lap("diffrazione e Fresnel");
        // ---- angoli d'orizzonte (mrad) e troposcatter (P.452 eq. 45, p = 50%)
        double thT = double.MinValue, thR = double.MinValue;
        int iht = n / 2, ihr = n / 2;
        for (int i = 1; i < n - 1; i++)
        {
            double at = (h[i] - hts) / dist[i] - 1000 * dist[i] / (2 * ae);
            double ar = (h[i] - hrs) / (d - dist[i]) - 1000 * (d - dist[i]) / (2 * ae);
            if (at > thT) { thT = at; iht = i; }
            if (ar > thR) { thR = ar; ihr = i; }
        }
        double dlt = r.Los || n < 3 ? d / 2 : dist[iht], dlr = r.Los || n < 3 ? d / 2 : d - dist[ihr];
        double thTd = (hrs - hts) / d - 1000 * d / (2 * ae), thRd = (hts - hrs) / d - 1000 * d / (2 * ae);
        if (r.Los || n < 3) { thT = thTd; thR = thRd; }
        else { thT = Math.Max(thT, thTd); thR = Math.Max(thR, thRd); }
        // 1.3.2 (criterio dell'autore): i rilievi vicini a ciascuna stazione (entro NearKm) non possono costare più di
        // NearCapDb nel troposcatter. L'angolo d'orizzonte dovuto a loro si limita a quello dei rilievi lontani più
        // NearCapDb/0,573 mrad (P.452 eq. 45 conta 0,573 dB per mrad).
        if (!r.Los && n >= 3 && Refractivity.NearCapDb > 0)
        {
            double farT = thTd, farR = thRd;
            for (int i = 1; i < n - 1; i++)
            {
                if (dist[i] > Refractivity.NearKm) farT = Math.Max(farT, (h[i] - hts) / dist[i] - 1000 * dist[i] / (2 * ae));
                if (d - dist[i] > Refractivity.NearKm) farR = Math.Max(farR, (h[i] - hrs) / (d - dist[i]) - 1000 * (d - dist[i]) / (2 * ae));
            }
            double capM = Refractivity.NearCapDb / 0.573;
            if (dlt <= Refractivity.NearKm && thT > farT + capM)
            {
                r.NearTxMrad = thT; r.NearTxKm = dlt; r.NearTxSavedDb = 0.573 * (thT - farT - capM);
                CalcLog.W($"rilievo vicino al TX a {dlt:0.0} km: orizzonte {thT:0.0} mrad, lontano {farT:0.0} mrad → contato {farT + capM:0.0} mrad (al massimo +{Refractivity.NearCapDb:0} dB, risparmio {r.NearTxSavedDb:0.0} dB)");
                thT = farT + capM;
            }
            if (dlr <= Refractivity.NearKm && thR > farR + capM)
            {
                r.NearRxMrad = thR; r.NearRxKm = dlr; r.NearRxSavedDb = 0.573 * (thR - farR - capM);
                CalcLog.W($"rilievo vicino all'RX a {dlr:0.0} km: orizzonte {thR:0.0} mrad, lontano {farR:0.0} mrad → contato {farR + capM:0.0} mrad (al massimo +{Refractivity.NearCapDb:0} dB, risparmio {r.NearRxSavedDb:0.0} dB)");
                thR = farR + capM;
            }
        }
        r.ThetaTx = thT; r.ThetaRx = thR;
        r.ThetaMrad = 1000 * d / ae + thT + thR;
        r.GtxDbi = Dl6wu.GainDbi(tx.BoomM, o.FreqMHz); r.GrxDbi = Dl6wu.GainDbi(rx.BoomM, o.FreqMHz);
        double n0 = good.Count > 0 ? good.Average(p => p.Ns) : 320;
        double lf = 25 * Math.Log10(fGHz) - 2.5 * Math.Pow(Math.Log10(fGHz / 2), 2);   // f in GHz
        double lc = 0.051 * Math.Exp(0.055 * (r.GtxDbi + r.GrxDbi));
        r.LscatDb = 190.1 + lf + 20 * Math.Log10(d) + 0.573 * Math.Max(0, r.ThetaMrad) - 0.15 * n0 + lc;
        CalcLog.W($"angoli d'orizzonte: TX {thT:0.00} mrad, RX {thR:0.00} mrad; θ = 1000 d/ae + θt + θr = {r.ThetaMrad:0.00} mrad");
        CalcLog.W($"troposcatter P.452 eq. 45 (p = 50%): Lf {lf:0.00}, N0 {n0:0.0}, Lc = 0,051·e^(0,055(Gt+Gr)) {lc:0.00} → Lbs = {r.LscatDb:0.00} dB (Gt {r.GtxDbi:0.0} dBi, Gr {r.GrxDbi:0.0} dBi, DL6WU)");
        Scatter3(r, h, good, o.FreqMHz);
        CommonVolume(r, pts, prof, hts, hrs, ae);
        MixScatter(r, h, prof);

        // ---- combinazione (somma delle potenze) senza tropo
        r.LbaseDb = -10 * Math.Log10(Math.Pow(10, -r.LdiffDb / 10) + Math.Pow(10, -r.LscatDb / 10));
        r.LbaseDb = Math.Max(r.LbaseDb, r.LfsDb);
        CalcLog.W($"senza tropo (somma delle potenze di diffrazione e scatter, non sotto lo spazio libero): {r.LbaseDb:0.00} dB");
        // ---- confronto: ITM (Longley-Rice, NTIA) sullo stesso profilo, atmosfera normale con clima e variabilità nel tempo
        if (Refractivity.UseItm)
        {
            try
            {
                bool seaPath = r.SeaFrac >= 0.5;
                var clim = seaPath ? Itm.Climate.MaritimeTemperateSea : Itm.Climate.MaritimeTemperateLand;
                double itmN0 = seaPath ? 350 : 320;                       // NTIA 82-100: marittimo temperato su mare / su terra
                double eps = seaPath ? 81 : 15, sig = seaPath ? 5 : 0.005;
                var pfl = ItmProfile(h, 0, n - 1, d);
                double hT = Math.Max(0.5, Math.Min(3000, hts - h[0])), hR = Math.Max(0.5, Math.Min(3000, hrs - h[n - 1]));
                var i50 = Itm.P2P(hT, hR, pfl, clim, itmN0, o.FreqMHz, false, eps, sig, 13, 50, 50, 50);
                if (i50.Error == 0)
                {
                    var i10 = Itm.P2P(hT, hR, pfl, clim, itmN0, o.FreqMHz, false, eps, sig, 13, 10, 50, 50);
                    var i01 = Itm.P2P(hT, hR, pfl, clim, itmN0, o.FreqMHz, false, eps, sig, 13, 1, 50, 50);
                    r.ItmDb = i50.AdB; r.Itm10Db = i10.AdB; r.Itm1Db = i01.AdB; r.ItmMode = i50.ModeName; r.ItmDeltaH = i50.DeltaH; r.ItmN0 = itmN0;
                    r.ItmClimate = seaPath ? "marittimo temperato sul mare" : "marittimo temperato sulla terra";
                    CalcLog.W($"ITM (Longley-Rice, NTIA): clima {r.ItmClimate}, N0 {itmN0:0}, suolo εr {eps:0} σ {sig:0.###} S/m, polarizzazione orizzontale, Δh {i50.DeltaH:0} m, " +
                              $"antenne efficaci {i50.He[0]:0} / {i50.He[1]:0} m, meccanismo {i50.ModeName}: perdita {i50.AdB:0.0} dB al 50% del tempo, {i10.AdB:0.0} dB al 10%, {i01.AdB:0.0} dB all'1% " +
                              $"(P.452 qui {r.LbaseDb:0.0} dB, differenza {r.LbaseDb - i50.AdB:+0.0;-0.0} dB)");
                }
                else { r.ItmErr = $"codice {i50.Error}"; CalcLog.W($"ITM: dati fuori dai limiti del modello (codice {i50.Error})"); }
            }
            catch (Exception ex) { r.ItmErr = ex.Message; CalcLog.W("ITM: calcolo non riuscito: " + ex.Message); }
        }

        r.Times.Lap("orizzonti e troposcatter");
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
            var softPts = new List<double>();   // guadagno per punto con il condotto morbido (strato non intrappolante)
            foreach (bool wantTrap in new[] { true, false })
            {
                foreach (var key in keys)
                {
                    LayerDuct At(int i) => prof[i]?.Layers.FirstOrDefault(l => l.Key == key && (wantTrap ? l.Traps(o.FreqMHz) && l.Depth >= MinCorridorM : l.ExcessN > 0));
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
                    var l = prof[i].Layers.First(x => x.Key == best.key && (best.traps ? x.Traps(o.FreqMHz) && x.Depth >= MinCorridorM : x.ExcessN > 0));
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
                if (!best.traps && Refractivity.UseSoftDuct)
                {
                    double lam0 = 299.792458 / o.FreqMHz;
                    for (int q = 0; q < run.Count; q++)
                    {
                        var l = run[q].l; var pr = prof[best.a + q];
                        double sf = l.Deficit > 0 && l.LambdaMax > 0 ? Math.Max(0, Math.Min(1, (l.LambdaMax / lam0 - 1.0 / 3) / (2.0 / 3))) : 0;
                        softPts.Add(sf > 0 ? Math.Max(pr.LayerGainDb, 15 + sf * (ProfileResult.DuctGain(l.Deficit) - 15)) : pr.LayerGainDb);
                        if (sf > 0) r.SoftN++;
                        r.SoftMax = Math.Max(r.SoftMax, sf);
                    }
                    if (r.SoftN > 0) CalcLog.W($"   condotto morbido (λ fino a 3 volte λmax, Kerr/Hepburn): {r.SoftN} punti su {run.Count}, fattore massimo {r.SoftMax:0.00}");
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
                for (int i = 1; i < n; i++)
                    if (!sea[i] && dist[i] >= r.LayerFromKm && dist[i] <= r.LayerToKm) r.LandInDuctKm += dist[i] - dist[i - 1];
                r.LandCov = r.LandKm > 0 ? Math.Min(1, r.LandInDuctKm / r.LandKm) : double.NaN;
                CalcLog.W($"strato fondante: {r.LayerName}, {(best.traps ? $"condotto che intrappola λ {299.792458 / o.FreqMHz:0.00} m" : "solo super-rifrattivo (non intrappola la banda)")}; continuo da {r.LayerFromKm:0} a {r.LayerToKm:0} km ({r.LayerFrac * 100:0}% della tratta, {run.Count} punti su {np})");
                CalcLog.W($"   quota della cima {r.TopMean:0} m (da {r.TopMin:0} a {r.TopMax:0}), base media {r.BaseMean:0} m, inclinazione {r.TiltMkm:+0.0;-0.0} m/km, deficit medio {r.DeficitMean:0.0} M, λmax minima {r.LambdaMin:0.00} m; terreno dentro lo strato per {r.TerrainInDuctKm:0} km");
            }
            if (r.DualN > 0)
                CalcLog.W($"   doppio calcolo su {r.DualN} punti (condotti potenziali): A salto in 50 m → deficit medio {r.DeficitMeanA:0.0} M, λmax minima {r.LambdaMinA:0.00} m; " +
                          $"B spessore P.453 {r.ClimThickMean:0} m → {r.DeficitMeanB:0.0} M, {r.LambdaMinB:0.00} m; peggiore = B in {r.DualChosenB} punti su {r.DualN}; anomalia media {r.AnomalyMean:0.0}× la norma");
            else if (best.key < 0) CalcLog.W("strato fondante: nessuno strato con calo di N oltre il normale continuo su almeno un punto");

            // guadagno: conta solo lo strato continuo, in proporzione al tratto che copre (Bean e Dutton: 10–25 dB per strato)
            double g = 0;
            if (best.key >= 0)
            {
                double gl = best.traps ? Math.Min(25, 20 + r.DeficitMean / 5.0)
                                       : softPts.Count > 0 ? softPts.Average()
                                       : Enumerable.Range(best.a, best.b - best.a + 1).Average(i => prof[i].LayerGainDb);
                if (Refractivity.UseCorrections)
                {
                    // 1.3.0: stabilità di Misme (solo senza condotto) e moto verticale lungo il tratto dello strato
                    var rng = Enumerable.Range(best.a, best.b - best.a + 1).Select(i => prof[i]).ToList();
                    double corr = rng.Average(p => p.VertDb + p.HighPDb + p.SeaAirDb + (best.traps ? 0 : p.StabDb));
                    r.SeaAirDbMean = rng.Average(p => p.SeaAirDb);
                    if (Math.Abs(r.SeaAirDbMean) >= 0.05) CalcLog.W($"contrasto aria-mare sullo strato (θ925 − SST, T2m − SST, calo della SST nelle 72 ore): {r.SeaAirDbMean:+0.0;-0.0} dB");
                    r.HighPFrac = rng.Count(p => p.HighPDb > 0) / (double)rng.Count;
                    if (r.HighPFrac > 0) CalcLog.W($"alta pressione estesa e vento debole su {r.HighPFrac * 100:0}% dello strato: +{Refractivity.HighPDbBonus * r.HighPFrac:0.0} dB");
                    if (Math.Abs(corr) >= 0.05) CalcLog.W($"correzioni meteo sullo strato: {(best.traps ? "moto verticale" : "stabilità di Misme + moto verticale")} {corr:+0.0;-0.0} dB");
                    gl += corr;
                    if (Refractivity.UsePersistence && !double.IsNaN(o.PersistFrac) && gl >= 10)
                    {
                        double pb = Refractivity.PersistMaxDb * o.PersistFrac;
                        r.PersistFrac = o.PersistFrac;
                        CalcLog.W($"persistenza: {o.PersistFrac * 100:0}% delle {Refractivity.PersistHours} ore precedenti sopra {Refractivity.PersistThrDb:0} dB lungo la tratta → +{pb:0.0} dB");
                        gl += pb;
                    }
                    if (!best.traps && r.SoftN == 0) gl = Math.Min(gl, Refractivity.NoDuctCapDb);
                }
                // CSIRO 2026: sul mare il condotto c'è quasi sempre, la ricezione la decide il tratto su terra
                bool landMatters = r.LandKm >= 0.1 * d && !double.IsNaN(r.LandCov);
                r.FracUsed = landMatters ? Math.Min(r.LayerFrac, r.LandCov) : r.LayerFrac;
                g = gl * r.FracUsed;
                // stima aggressiva: stessa catena con la soglia classica di 79 N/km nello strato basso (solo senza condotto pieno)
                if (!best.traps)
                {
                    double da = Enumerable.Range(best.a, best.b - best.a + 1).Average(i => Math.Max(0, prof[i].LayerGainAggrDb - prof[i].LayerGainDb));
                    double ga = gl + da;
                    if (Refractivity.UseCorrections && r.SoftN == 0) ga = Math.Min(ga, Refractivity.NoDuctCapDb);
                    r.AggrGainDb = ga * r.FracUsed;
                }
                else r.AggrGainDb = g;
                // peso del mare (μ1)
                if (r.SeaWeight != 1) { g *= r.SeaWeight; r.AggrGainDb *= r.SeaWeight; }
                // aggancio delle stazioni al condotto anche quando copre solo parte della tratta: fuori dal condotto il segnale entra male
                if (best.traps && !(r.FracUsed >= 0.8 && !r.Los))
                {
                    double Off(double hAsl, double dk) { double tp = TopAt(r, dk), bs = BaseAt(r, dk); return hAsl > tp ? hAsl - tp : hAsl < bs ? bs - hAsl : 0; }
                    double dkT = Math.Max(r.LayerFromKm, 0), dkR = Math.Min(r.LayerToKm, d);
                    double oT = Off(hts, dkT), oR = Off(hrs, dkR);
                    r.DuctCouplingF = 0.5 + 0.5 * Math.Sqrt(Math.Exp(-oT / 300) * Math.Exp(-oR / 300));
                    g *= r.DuctCouplingF; r.AggrGainDb *= r.DuctCouplingF;
                    CalcLog.W($"quota delle stazioni rispetto al condotto: TX fuori di {oT:0} m, RX fuori di {oR:0} m → fattore {r.DuctCouplingF:0.00}");
                }
                CalcLog.W($"terra lungo la tratta {r.LandKm:0} km ({(1 - r.SeaFrac) * 100:0}%), di cui coperti dallo strato {r.LandInDuctKm:0} km" +
                          (landMatters ? $" → copertura della terra {r.LandCov:0.00}; frazione usata = min(continua {r.LayerFrac:0.00}, terra {r.LandCov:0.00}) = {r.FracUsed:0.00}" : " (tratto su terra sotto il 10%: conta solo la continuità)"));
                CalcLog.W($"guadagno dello strato {gl:0.0} dB × frazione usata {r.FracUsed:0.00} = {g:0.0} dB");
            }
            int nsub = good.Count(p => p.Sub);
            if (nsub * 2 > good.Count) { g = -10.0 * nsub / good.Count; CalcLog.W($"sub-rifrazione su {nsub} profili su {good.Count}: {g:0.0} dB"); }
            // strato limite sopra le creste (IT9GSF): se la cima dello strato limite supera le creste lungo la tratta lo strato non si spezza
            if (Refractivity.UseBlhRidges && best.key >= 0 && g >= 3)
            {
                for (int i = 2; i < n - 2; i++)
                {
                    if (h[i] < 300 || h[i] < h[i - 1] || h[i] < h[i + 1] || h[i] < h[i - 2] || h[i] < h[i + 2]) continue;
                    int pi = (int)Math.Round(dist[i] / Math.Max(1e-6, d) * (prof.Count - 1));
                    var pp = prof[Math.Max(0, Math.Min(prof.Count - 1, pi))];
                    if (pp == null || double.IsNaN(pp.Blh)) continue;
                    r.RidgeN++; r.RidgeMaxM = Math.Max(r.RidgeMaxM, h[i]);
                    if (pp.Elev + pp.Blh > h[i]) r.RidgeCovered++;
                }
                if (r.RidgeN > 0 && r.RidgeMaxM >= 500)
                {
                    r.BlhRidgeDb = Refractivity.BlhRidgeDbBonus * r.RidgeCovered / r.RidgeN;
                    g += r.BlhRidgeDb; r.AggrGainDb += r.BlhRidgeDb;
                    CalcLog.W($"strato limite e creste: {r.RidgeCovered} creste su {r.RidgeN} (la più alta {r.RidgeMaxM:0} m) sotto la cima dello strato limite → +{r.BlhRidgeDb:0.0} dB");
                }
            }
            r.TropoGainDb = g;
            r.DuctFrac = best.traps ? r.LayerFrac : 0;
            double lpred = r.LbaseDb - g;

            // condotto su (quasi) tutta la tratta: spazio libero + accoppiamento delle antenne + attenuazione residua
            if (best.traps && r.FracUsed >= 0.8 && !r.Los)
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
            // 1.3.2: condotto che comincia lontano (oltre l'orizzonte della stazione): come ci si entra. Si porta il segnale fino
            // al punto d'inizio, a metà del condotto, per diffrazione (spigolo principale con il parametro di Fresnel ν e terra
            // sferica liscia, il peggiore dei due), poi si viaggia nel condotto fino al corrispondente (spazio libero + γd + RX).
            if (best.traps && !r.Los && r.LayerFromKm >= 5 && r.LayerFromKm < d - 5)
            {
                double dE = r.LayerFromKm, hE = 0.5 * (TopAt(r, dE) + BaseAt(r, dE));
                double numax = double.NegativeInfinity, kmNu = 0, hNu = 0, d1Nu = 0, d2Nu = 0; int iNu = -1;
                for (int i = 1; i < n && dist[i] < dE; i++)
                {
                    double d1 = dist[i] * 1000, d2 = (dE - dist[i]) * 1000;
                    if (d2 < 100) break;
                    double line = hts + (hE - hts) * dist[i] / dE;
                    double bulge = d1 * d2 / (2 * ae * 1000);                     // rigonfiamento della terra equivalente (m)
                    double nu = (h[i] + bulge - line) * Math.Sqrt(2 / lam * (1 / d1 + 1 / d2));
                    if (nu > numax) { numax = nu; kmNu = dist[i]; iNu = i; hNu = h[i] + bulge - line; d1Nu = d1; d2Nu = d2; }
                }
                double jT = double.IsNegativeInfinity(numax) ? 0 : J(numax);
                // ostacolo arrotondato (ITU-R P.526-16 § 4.2): Barsis (NBS 1963) misura su tre lunghi percorsi oltre un
                // monte perdite 10–20 dB più alte dello spigolo ideale; la curvatura si stima dal terreno a ±1 km dalla cima
                double rRound = 0, tRound = 0;
                if (iNu > 0 && numax > -0.78)
                {
                    int ia = iNu, ib = iNu;
                    while (ia > 0 && dist[iNu] - dist[ia] < 1) ia--;
                    while (ib < n - 1 && dist[ib] - dist[iNu] < 1) ib++;
                    double da = (dist[iNu] - dist[ia]) * 1000, db = (dist[ib] - dist[iNu]) * 1000;
                    if (da > 300 && db > 300)
                    {
                        double kap = 2 * ((h[ia] - h[iNu]) / da + (h[ib] - h[iNu]) / db) / (da + db);   // curvatura (1/m), negativa sulla cima
                        if (kap < 0) rRound = Math.Max(200, Math.Min(30000, -1 / kap));
                    }
                    if (rRound > 0)
                    {
                        double pr = Math.PI * rRound / lam;
                        double mR = rRound * ((d1Nu + d2Nu) / (d1Nu * d2Nu)) / Math.Pow(pr, 1.0 / 3);
                        double nR = hNu * Math.Pow(pr, 2.0 / 3) / rRound;
                        double mn = mR * nR;
                        tRound = mn <= 4 ? 7.2 * Math.Sqrt(mR) - (2 - 12.5 * nR) * mR + 3.6 * Math.Pow(mR, 1.5) - 0.8 * mR * mR
                                         : -6 - 20 * Math.Log10(mn) + 7.2 * Math.Sqrt(mR) - (2 - 17 * nR) * mR + 3.6 * Math.Pow(mR, 1.5) - 0.8 * mR * mR;
                        tRound = Math.Max(0, tRound);
                        jT += tRound;
                    }
                }
                double jS = SphericalLoss(dE, Math.Max(1, hts - h[0]), Math.Max(1, hE), ae, o.FreqMHz);
                double entry = Math.Max(jT, jS);
                double rxOff = Math.Max(0, Math.Max(hrs - TopAt(r, Math.Min(d, r.LayerToKm)), BaseAt(r, Math.Min(d, r.LayerToKm)) - hrs));
                double rxC = Math.Min(40, 3 + rxOff / 10);
                double gamD = 5e-5 * ae * Math.Pow(fGHz, 1.0 / 3);
                double resid = gamD * (1000 * (d - dE) / ae);
                r.EntryKm = dE; r.EntryHeightM = hE; r.EntryNu = numax; r.EntryNuKm = kmNu; r.EntryDiffDb = entry;
                r.LentryDb = r.LfsDb + entry + rxC + resid;
                CalcLog.W($"ingresso nel condotto: comincia a {dE:0} km, quota media {hE:0} m; diffrazione fino lì: spigolo principale a {kmNu:0.0} km con ν {numax:0.00}{(rRound > 0 ? $", raggio della cima {rRound / 1000:0.0} km → arrotondamento T(m,n) {tRound:0.0} dB (P.526 § 4.2)" : "")} → {jT:0.0} dB, terra sferica {jS:0.0} dB → {entry:0.0} dB; " +
                          $"poi nel condotto: γd {resid:0.0} dB, RX {(rxOff > 0 ? $"fuori di {rxOff:0} m" : "dentro")} {rxC:0.0} dB → Lingresso = Lfs {r.LfsDb:0.0} + {entry:0.0} + {rxC:0.0} + {resid:0.0} = {r.LentryDb:0.0} dB");
                if (r.LentryDb < lpred) { lpred = r.LentryDb; r.Mechanism = "condotto (ingresso per diffrazione)"; }
            }
            // 1.3.2: condotto che finisce prima del corrispondente: dall'uscita al RX il tratto è su terra (o mare) normale e lo
            // calcola l'ITM (eccesso sopra lo spazio libero, al 50% del tempo), con un'antenna "virtuale" a metà del condotto
            // nel punto d'uscita. Lato TX: aggancio dell'antenna se il condotto parte vicino, altrimenti la diffrazione d'ingresso.
            if (Refractivity.UseItmTail && best.traps && !r.Los && r.LayerToKm > 5 && r.LayerToKm < d - 5)
            {
                try
                {
                    double dX = r.LayerToKm, hX = 0.5 * (TopAt(r, dX) + BaseAt(r, dX));
                    int iX = 0; while (iX < n - 1 && dist[iX] < dX) iX++;
                    if (n - 1 - iX >= 4)
                    {
                        var pflT = ItmProfile(h, iX, n - 1, d - dist[iX]);
                        int seaPts = 0; for (int i = iX; i < n; i++) if (sea[i]) seaPts++;
                        bool seaT = seaPts >= 0.5 * (n - iX);
                        double hT = Math.Max(0.5, Math.Min(3000, hX - h[iX])), hR = Math.Max(0.5, Math.Min(3000, hrs - h[n - 1]));
                        var it = Itm.P2P(hT, hR, pflT, seaT ? Itm.Climate.MaritimeTemperateSea : Itm.Climate.MaritimeTemperateLand, seaT ? 350 : 320,
                            o.FreqMHz, false, seaT ? 81 : 15, seaT ? 5 : 0.005, 13, 50, 50, 50);
                        if (it.Error == 0)
                        {
                            double tail = Math.Max(0, it.AdB - it.AfsDb);
                            double txPart; string txWhy;
                            if (r.LayerFromKm < 5)
                            {
                                double tp = TopAt(r, 0), bs = BaseAt(r, 0);
                                double off = hts > tp ? hts - tp : hts < bs ? bs - hts : 0;
                                txPart = Math.Min(40, 3 + off / 10); txWhy = off > 0 ? $"TX fuori di {off:0} m" : "TX dentro";
                            }
                            else if (!double.IsNaN(r.EntryDiffDb) && r.EntryDiffDb > 0) { txPart = r.EntryDiffDb; txWhy = "ingresso per diffrazione"; }
                            else { txPart = 40; txWhy = "ingresso sconosciuto"; }
                            double gamD = 5e-5 * ae * Math.Pow(fGHz, 1.0 / 3);
                            double resid = gamD * (1000 * Math.Max(0, dX - r.LayerFromKm) / ae);
                            r.ExitKm = dX; r.ExitHeightM = hX; r.ItmTailDb = tail; r.ItmTailMode = it.ModeName;
                            r.LexitDb = r.LfsDb + txPart + resid + tail;
                            CalcLog.W($"uscita dal condotto a {dX:0} km (quota media {hX:0} m, {hT:0} m sul suolo), poi {d - dist[iX]:0} km fino al RX: ITM {it.ModeName}, " +
                                      $"eccesso sullo spazio libero {tail:0.0} dB; Luscita = Lfs {r.LfsDb:0.0} + {txWhy} {txPart:0.0} + γd {resid:0.0} + tratto finale {tail:0.0} = {r.LexitDb:0.0} dB");
                            if (r.LexitDb < lpred) { lpred = r.LexitDb; r.Mechanism = "condotto (uscita e tratto finale ITM)"; }
                        }
                        else CalcLog.W($"uscita dal condotto a {dX:0} km: ITM fuori dai limiti (codice {it.Error})");
                    }
                }
                catch (Exception ex) { CalcLog.W("uscita dal condotto: calcolo non riuscito: " + ex.Message); }
            }
            r.LpredDb = Math.Max(r.LfsDb, lpred);
            if (!double.IsNaN(r.AggrGainDb)) r.LpredAggrDb = Math.Max(r.LfsDb, Math.Min(r.LpredDb, r.LbaseDb - r.AggrGainDb));
            if (!double.IsNaN(r.LpredAggrDb)) CalcLog.W($"stima prudente (Bean e Dutton, 100 N/km) {r.LpredDb:0.00} dB; stima aggressiva (79 N/km, k ≈ 2) {r.LpredAggrDb:0.00} dB");
            CalcLog.W($"perdita prevista {r.LpredDb:0.00} dB, meccanismo: {r.Mechanism}");
        }
        else r.Notes.Add("nessuna previsione GFS sul percorso: calcolo con atmosfera normale (k = 4/3)");

        r.Times.Lap("strato fondante e condotto");
        // ---- terzo calcolo, solo da confrontare: ITU-R P.452-16 § 4.5 con ΔN del GFS (CSIRO 2026: p = 20% come costante di calibrazione)
        if (good.Count > 0 && !r.Los)
        {
            try { P452Duct(r, h, dist, sea, hts, hrs, hte, hre, hst, hsr, dlt, dlr, thT, thR, good, 0.5 * (tx.Pos.Lat + rx.Pos.Lat), o.FreqMHz); }
            catch (Exception ex) { CalcLog.W("P.452: calcolo non riuscito: " + ex.Message); }
        }

        r.Times.Lap("confronto P.452");
        // ---- quarto calcolo, solo da confrontare: equazione parabolica nel profilo M del GFS lungo la tratta
        if (good.Count > 0 && Refractivity.UseParabolic && !sub)
        {
            try
            {
                double bw = Math.Min(30, Math.Sqrt(29000 / Math.Pow(10, r.GtxDbi / 10)));   // apertura approssimata dal guadagno
                var pe = ParabolicEq.Run(o.FreqMHz, d, hts, hrs, h, dist, prof, bw, ct);
                r.PeLossDb = pe.LossDb; r.PeStdDb = pe.StdLossDb;
                r.PeTotalDb = -10 * Math.Log10(Math.Pow(10, -pe.LossDb / 10) + Math.Pow(10, -r.LscatDb / 10));
                CalcLog.W($"equazione parabolica (split-step, {pe.Nz} punti in quota fino a {pe.Hmax:0} m, passo {pe.Dz:0.00} m e {pe.Dx:0} m, fascio {bw:0}°): " +
                          $"perdita {pe.LossDb:0.0} dB nel profilo GFS, {pe.StdLossDb:0.0} dB con atmosfera standard → guadagno {pe.StdLossDb - pe.LossDb:+0.0;-0.0} dB; " +
                          $"con il troposcatter (somma delle potenze) {r.PeTotalDb:0.0} dB (solo confronto)");
                CalcLog.W($"   propagatore ad angolo largo; fondo in polarizzazione orizzontale all'angolo radente {pe.Psi:0.00}°: mare Γ {pe.GammaSea:0.000} (εr 81, σ 5 S/m, rugosità Miller-Brown ×{pe.Rho:0.000}), pianura Γ {pe.GammaLand:0.000} (εr 13, σ 0,006 S/m)");
            }
            catch (OperationCanceledException) { throw; }
            catch (Exception ex) { CalcLog.W("equazione parabolica: calcolo non riuscito: " + ex.Message); }
        }
        r.Times.Lap("equazione parabolica");
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
        if (r.TilesMissing > 0) r.Notes.Add($"{r.TilesMissing} riquadri DEM 30 m non disponibili: lì il terreno vale 0 m");
        r.Times.Lap("bilancio e rumore");
        if (!sub && !r.Los && Refractivity.UseViaMountain)
        {
            try { await ViaMountain(r, tx, rx, o, srtm, prof, ct); }
            catch (OperationCanceledException) { throw; }
            catch (Exception ex) { CalcLog.W("via monte: calcolo non riuscito: " + ex.Message); }
            r.Times.Lap("via monte");
        }
        CalcLog.W($"TEMPI del calcolo sul percorso: {CalcLog.Dur(r.Times.TotalMs)} — {r.Times.Summary()}");
        return r;
    }

    /// <summary>
    /// Troposcatter con altri due modelli, solo per confronto e incertezza (come TropoLink):
    /// ITU-R P.617-5 (mediana) L = F + 22 log f + 35 log θ + 17 log d + Lc, con F = 0,18·N0·e^(−hs/7,35) − 0,23·ΔN e
    /// Lc = 0,07·e^(0,055(Gt+Gr)); qui 0,18·N0·e^(−hs/7,35) usa direttamente Ns dei profili e ΔN il calo del primo km
    /// del GFS, così il troposcatter segue l'ora invece della climatologia. Formula del VHF/UHF Handbook RSGB
    /// (DL2FZN): L = Lfs + 24,44 + 4,283 ln f + 0,06656 d.
    /// </summary>
    static void Scatter3(LinkResult r, double[] h, List<ProfileResult> good, double fMHz)
    {
        double d = r.DistKm;
        r.RsgbDb = r.LfsDb + 24.44 + 4.283 * Math.Log(fMHz) + 0.06656 * d;
        if (r.ThetaMrad > 0.1)
        {
            double ns = good.Count > 0 ? good.Average(p => p.Ns) : 315;
            double dn = good.Count > 0 ? -good.Average(p => p.DN1km) : 40;
            double hs = Math.Max(0, h.Average()) / 1000;
            r.P617N0 = ns * Math.Exp(hs / 7.35); r.P617DeltaN = dn;
            r.P617F = 0.18 * ns - 0.23 * dn;
            double lc = 0.07 * Math.Exp(0.055 * (r.GtxDbi + r.GrxDbi));
            r.P617Db = r.P617F + 22 * Math.Log10(fMHz) + 35 * Math.Log10(r.ThetaMrad) + 17 * Math.Log10(d) + lc;
        }
        var all = new[] { r.LscatDb, r.P617Db, r.RsgbDb }.Where(x => !double.IsNaN(x)).ToList();
        r.ScatSpreadDb = all.Count > 1 ? all.Max() - all.Min() : 0;
        CalcLog.W($"troposcatter, altri modelli: P.617-5 F = 0,18·Ns {(good.Count > 0 ? "(GFS)" : "(tipico)")} − 0,23·ΔN = {r.P617F:0.0} (Ns→N0 {r.P617N0:0}, ΔN {r.P617DeltaN:0} N/km) → {(double.IsNaN(r.P617Db) ? "—" : r.P617Db.ToString("0.0"))} dB; " +
                  $"RSGB (DL2FZN) {r.RsgbDb:0.0} dB; P.452 {r.LscatDb:0.0} dB → dispersione fra i modelli {r.ScatSpreadDb:0.0} dB");
        if (good.Count > 0)
        {
            r.SubsFrac = good.Count(p => p.Subsidence) / (double)good.Count;
            r.AscentFrac = good.Count(p => p.Ascent) / (double)good.Count;
            r.SciroccoFrac = good.Count(p => p.Scirocco) / (double)good.Count;
            r.StabDbMean = good.Average(p => p.StabDb);
            CalcLog.W($"meteo lungo la tratta: subsidenza {r.SubsFrac * 100:0}% dei profili, aria in salita {r.AscentFrac * 100:0}%, scirocco {r.SciroccoFrac * 100:0}%; termine di stabilità medio {r.StabDbMean:+0.0;-0.0} dB");
            // 1.3.2: condotto d'evaporazione sul mare e livelli vicini al suolo
            var seaP = good.Where(p => p.Sea).ToList();
            if (seaP.Count > 0)
            {
                var ev = seaP.Where(p => p.EvapDuctM > 0).ToList();
                r.EvapDuctM = ev.Count > 0 ? ev.Average(p => p.EvapDuctM) : 0;
                r.EvapFrac = seaP.Count > 0 ? ev.Count / (double)good.Count : 0;
                r.LowLevelsUsed = seaP.Sum(p => p.LowLevels);
                double minM = ProfileResult.MinDuctM(fMHz);
                CalcLog.W($"mare: {seaP.Count} profili su {good.Count}; condotto d'evaporazione medio {r.EvapDuctM:0.0} m su {ev.Count} (SST media {seaP.Where(p => !double.IsNaN(p.Sst)).Select(p => p.Sst).DefaultIfEmpty(double.NaN).Average():0.0} °C); " +
                          $"per intrappolare {fMHz:0} MHz servirebbero circa {minM:0} m (Kerr): {(r.EvapDuctM >= minM ? "intrappola" : "non intrappola, ma rinforza le antenne basse sul mare")}; livelli NOMADS vicini al suolo usati: {r.LowLevelsUsed}");
                if (r.EvapDuctM >= 5 && fMHz >= 400) r.Notes.Add($"condotto d'evaporazione sul mare di circa {r.EvapDuctM:0} m: con antenne basse vicino al mare a {fMHz:0} MHz il segnale può essere molto più forte (vedi l'equazione parabolica)");
            }
            // 1.3.2: avvisi per l'ora del giorno (osservazione quotidiana dell'autore: rimescolamento e condotti da irraggiamento)
            var landP = good.Where(p => !p.Sea && !double.IsNaN(p.SunElev)).ToList();
            if (landP.Count > 0)
            {
                r.DayMixFrac = landP.Count(p => p.DayMix) / (double)landP.Count;
                r.NightRadFrac = landP.Count(p => p.NightRad) / (double)landP.Count;
                if (r.DayMixFrac >= 0.3) r.Notes.Add($"giorno con sole alto sul {r.DayMixFrac * 100:0}% del tratto su terra: lo strato vicino al suolo è rimescolato, i condotti al suolo sulla terra sono probabilmente spariti (sul mare no)");
                if (r.NightRadFrac >= 0.3) r.Notes.Add($"notte con vento debole e strato limite basso sul {r.NightRadFrac * 100:0}% del tratto su terra: con cielo sereno possibili condotti al suolo da irraggiamento, che cadono dopo l'alba");
                CalcLog.W($"ora del giorno: sole medio {landP.Average(p => p.SunElev):0}° sulla terra; rimescolamento diurno {r.DayMixFrac * 100:0}%, condotti da irraggiamento possibili {r.NightRadFrac * 100:0}%");
            }
        }
    }

    /// <summary>
    /// Volume comune: punto in cui si incrociano i raggi radenti delle due stazioni, ciascuno con il suo angolo
    /// d'orizzonte, su una Terra di raggio equivalente ae. Raggio dal TX: z = hts + x·θt + x²/(2ae); dal RX lo stesso
    /// con d − x; uguagliando: x = (hrs − hts + d·θr + d²/(2ae)) / θ, con θ = d/ae + θt + θr. Con orizzonti piatti e
    /// antenne alla stessa quota la base vale d²/(8·ae) (1,3 km a 300 km, 3,7 km a 500 km). Distanza equivalente di
    /// Bray (QST, novembre 1961): ogni grado d'orizzonte vale circa 111 km in più.
    /// </summary>
    /// <summary>Profilo per l'ITM fra gli indici i0 e i1 (punti equidistanti): pfl[0] intervalli, pfl[1] passo in metri, poi le quote (mare a 0).</summary>
    static double[] ItmProfile(double[] h, int i0, int i1, double lenKm)
    {
        int np = i1 - i0;
        var p = new double[np + 3];
        p[0] = np; p[1] = lenKm * 1000 / np;
        for (int i = 0; i <= np; i++) p[i + 2] = Math.Max(0, h[i0 + i]);
        return p;
    }

    static void CommonVolume(LinkResult r, List<LatLon> pts, IList<ProfileResult> prof, double hts, double hrs, double ae)
    {
        double d = r.DistKm, th = r.ThetaMrad / 1000, tt = r.ThetaTx / 1000, tr = r.ThetaRx / 1000;
        if (th <= 1e-6 || r.Los) { CalcLog.W("volume comune: stazioni in vista, nessun volume di scatter"); return; }
        double htk = hts / 1000, hrk = hrs / 1000;
        double x = (hrk - htk + d * tr + d * d / (2 * ae)) / th;
        x = Math.Max(0, Math.Min(d, x));
        double z = htk + x * tt + x * x / (2 * ae);
        r.CvKm = x; r.CvHeightM = z * 1000;
        r.DeqKm = d + 111.0 * (r.ThetaTx + r.ThetaRx) * 180 / Math.PI / 1000;
        if (pts != null && pts.Count > 1)
        {
            int i = (int)Math.Round(x / d * (pts.Count - 1));
            var p = pts[Math.Max(0, Math.Min(pts.Count - 1, i))];
            r.CvLat = p.Lat; r.CvLon = p.Lon;
        }
        // aria alla base del volume comune: profilo di previsione più vicino lungo la tratta, strato che contiene la quota
        if (prof != null && prof.Count > 0)
        {
            int k = (int)Math.Round(x / d * (prof.Count - 1));
            var pr = prof[Math.Max(0, Math.Min(prof.Count - 1, k))];
            if (pr != null && pr.Levels.Length > 1)
            {
                var L = pr.Levels;
                for (int j = 1; j < L.Length; j++)
                    if (L[j].Z >= r.CvHeightM || j == L.Length - 1)
                    {
                        double dz = (L[j].Z - L[j - 1].Z) / 1000;
                        if (dz > 0.01) r.CvGrad = -(L[j].N - L[j - 1].N) / dz;
                        double f = Math.Max(0, Math.Min(1, (r.CvHeightM - L[j - 1].Z) / Math.Max(1, L[j].Z - L[j - 1].Z)));
                        r.CvN = L[j - 1].N + f * (L[j].N - L[j - 1].N);
                        r.CvRH = L[j - 1].RH + f * (L[j].RH - L[j - 1].RH);
                        r.CvLayer = L[j - 1].P.ToString("0") + "–" + L[j].P.ToString("0") + " hPa";
                        break;
                    }
            }
        }
        CalcLog.W($"volume comune: base a {r.CvHeightM:0} m s.l.m., a {r.CvKm:0} km dal TX ({r.CvLat:0.00}, {r.CvLon:0.00}); " +
                  $"con orizzonti piatti sarebbe d²/(8·ae) = {d * d / (8 * ae) * 1000:0} m; distanza equivalente di Bray {r.DeqKm:0} km" +
                  (double.IsNaN(r.CvGrad) ? "" : $"; aria alla base ({r.CvLayer}): N {r.CvN:0}, −dN/dh {r.CvGrad:0} N/km, UR {r.CvRH:0}%"));
    }

    /// <summary>
    /// 1.3.4 (manuale § 8 ter, osservazioni di IK3XTV): di giorno sulla terra il sole rimescola l'aria e alla cima dello strato
    /// limite l'aria umida incontra quella secca: lì la turbolenza dell'indice di rifrazione è massima. Se quella cima (quota del
    /// suolo del modello + altezza dello strato limite) arriva entro 500 m dalla base del volume comune, il troposcatter guadagna
    /// fino a Refractivity.MixScatterDb (a metà fra 500 e 1000 m, niente oltre). Solo con rilievi lungo la tratta (≥ 800 m),
    /// volume comune sulla terra, sole oltre 15° e strato limite oltre 800 m. Valore provvisorio, da tarare.
    /// </summary>
    static void MixScatter(LinkResult r, double[] h, IList<ProfileResult> prof)
    {
        if (!Refractivity.UseCorrections || !Refractivity.UseMixScatter || double.IsNaN(r.CvHeightM) || prof == null || prof.Count == 0 || r.DistKm <= 0) return;
        if (h == null || h.Length == 0 || h.Max() < 800) return;
        int k = (int)Math.Round(r.CvKm / r.DistKm * (prof.Count - 1));
        var pr = prof[Math.Max(0, Math.Min(prof.Count - 1, k))];
        if (pr == null || pr.Sea || !(pr.SunElev > 15) || !(pr.Blh > 800)) return;
        double top = pr.Elev + pr.Blh, gap = Math.Abs(top - r.CvHeightM);
        double b = gap <= 500 ? Refractivity.MixScatterDb : gap <= 1000 ? Refractivity.MixScatterDb * (1000 - gap) / 500 : 0;
        r.MixTopM = top;
        if (b <= 0) { CalcLog.W($"troposcatter pomeridiano: cima dello strato rimescolato a {top:0} m, volume comune a {r.CvHeightM:0} m: troppo lontane, nessun rinforzo"); return; }
        r.MixScatterDb = b;
        r.LscatDb -= b;
        CalcLog.W($"troposcatter pomeridiano (§ 8 ter): cima dello strato rimescolato a {top:0} m (suolo {pr.Elev:0} + strato limite {pr.Blh:0}), volume comune a {r.CvHeightM:0} m, scarto {gap:0} m → −{b:0.0} dB sul troposcatter = {r.LscatDb:0.00} dB");
    }

    /// <summary>Spessore minimo di uno strato per contare come corridoio continuo (CSIRO 2026: strati sotto 40 m scartati).</summary>
    public const double MinCorridorM = 40;
    public const double P452P = 20;      // percentuale di tempo usata come calibrazione

    /// <summary>
    /// Perdita per condotti e riflessione su strati, ITU-R P.452-16 § 4.5 (eq. 46–57), con il ΔN del primo km dal GFS
    /// (mediana lungo la tratta) al posto di quello climatologico e p = 20% come costante di calibrazione
    /// (Indermuehle e Suzuki 2026). Combinata con diffrazione e troposcatter come somma morbida. Solo per confronto.
    /// </summary>
    static void P452Duct(LinkResult r, double[] h, double[] dist, bool[] sea, double hts, double hrs, double hte, double hre,
                         double hst, double hsr, double dlt, double dlr, double thT, double thR, List<ProfileResult> good, double latC, double fMHz)
    {
        int n = h.Length; double d = r.DistKm, f = fMHz / 1000, p = P452P;
        var dns = good.Select(x => -x.DN1km).Where(x => !double.IsNaN(x)).OrderBy(x => x).ToList();
        double dN = dns.Count > 0 ? dns[dns.Count / 2] : 45;
        dN = Math.Max(1, Math.Min(150, dN));                     // il metodo non è definito dentro un condotto (ΔN ≥ 157)
        double k50 = 157 / (157 - dN), ae = 6371 * k50;
        // tratti di terra: il più lungo (dtm) e il più lungo interno, oltre 50 km dalla costa (dlm)
        double dtm = 0, dlm = 0, run = 0; int runStart = -1;
        bool anySea = sea.Any(x => x);
        for (int i = 1; i <= n; i++)
        {
            bool land = i < n && !sea[i];
            if (land) { if (runStart < 0) runStart = i - 1; run += dist[i] - dist[i - 1]; }
            else if (runStart >= 0)
            {
                dtm = Math.Max(dtm, run);
                double cut = 0;
                if (sea[runStart] || (runStart > 0 && sea[runStart - 1])) cut += 50;
                if (i < n && sea[i]) cut += 50;
                dlm = Math.Max(dlm, anySea ? Math.Max(0, run - cut) : run);
                runStart = -1; run = 0;
            }
        }
        double tau = 1 - Math.Exp(-4.12e-4 * Math.Pow(dlm, 2.41));
        double mu1 = Math.Min(1, Math.Pow(Math.Pow(10, -dtm / (16 - 6.6 * tau)) + Math.Pow(10, -5 * (0.496 + 0.354 * tau)), 0.2));
        double phi = Math.Abs(latC);
        double mu4 = phi <= 70 ? Math.Pow(10, (-0.935 + 0.0176 * phi) * Math.Log10(mu1)) : Math.Pow(10, 0.3 * Math.Log10(mu1));
        double beta0 = phi <= 70 ? Math.Pow(10, -0.015 * phi + 1.67) * mu1 * mu4 : 4.17 * mu1 * mu4;
        double alpha = -0.6 - 3.5e-9 * Math.Pow(d, 3.1) * tau;
        double mu2 = Math.Min(1, Math.Pow(500 / ae * d * d / Math.Pow(Math.Sqrt(hte) + Math.Sqrt(hre), 2), alpha));
        // rugosità: quota massima del terreno sopra la superficie liscia fra i due orizzonti
        double hm = 0;
        for (int i = 0; i < n; i++)
            if (dist[i] >= dlt && dist[i] <= d - dlr) hm = Math.Max(hm, h[i] - (hst + (hsr - hst) * dist[i] / d));
        double di = Math.Min(Math.Max(0, d - dlt - dlr), 40);
        double mu3 = hm <= 10 ? 1 : Math.Exp(-4.6e-5 * (hm - 10) * (43 + 6 * di));
        double beta = beta0 * mu2 * mu3;
        double lgb = Math.Log10(beta);
        double gam = 1.076 / Math.Pow(2.0058 - lgb, 1.012) * Math.Exp(-(9.51 - 4.8 * lgb + 0.198 * lgb * lgb) * 1e-6 * Math.Pow(d, 1.13));
        double ap = -12 + (1.2 + 3.7e-3 * d) * Math.Log10(p / beta) + 12 * Math.Pow(p / beta, gam);
        // perdite fisse: accoppiamento empirico sotto 0,5 GHz, schermatura dei siti, accoppiamento sul mare
        double alf = f < 0.5 ? 45.375 - 137.0 * f + 92.5 * f * f : 0;
        double Ast(double th, double dl) { double t2 = th - 0.1 * dl; return t2 > 0 ? 20 * Math.Log10(1 + 0.361 * t2 * Math.Sqrt(f * dl)) + 0.264 * t2 * Math.Pow(f, 1.0 / 3) : 0; }
        double ast = Ast(thT, dlt), asr = Ast(thR, dlr);
        double omega = r.SeaFrac;
        double dct = 0; for (int i = 0; i < n && !sea[i]; i++) dct = dist[i];
        double dcr = 0; for (int i = n - 1; i >= 0 && !sea[i]; i--) dcr = d - dist[i];
        double Act(double dc, double dl, double hs) => omega >= 0.75 && dc <= dl && dc <= 5 ? -3 * Math.Exp(-0.25 * dc * dc) * (1 + Math.Tanh(0.07 * (50 - hs))) : 0;
        double act = Act(dct, dlt, hts), acr = Act(dcr, dlr, hrs);
        double af = 102.45 + 20 * Math.Log10(fMHz) + 20 * Math.Log10(Math.Max(0.1, dlt + dlr)) + alf + ast + asr + act + acr;
        double thtp = thT <= 0.1 * dlt ? thT : 0.1 * dlt, thrp = thR <= 0.1 * dlr ? thR : 0.1 * dlr;
        double thp = 1000 * d / ae + thtp + thrp;
        double gd = 5e-5 * ae * Math.Pow(f, 1.0 / 3);
        // gas (P.676 allegato 2 semplificato, livello del mare, 7,5 g/m³)
        double go = (7.2 / (f * f + 0.34) + 0.62 / (Math.Pow(54 - f, 1.16) + 0.83)) * f * f * 1e-3;
        double gw = (0.0313 + 3.98 / (Math.Pow(f - 22.235, 2) + 9.42)) * f * f * 7.5 * 1e-4;
        double ag = (go + gw) * d;
        double lba = af + gd * thp + ap + ag;
        // troposcatter al 20%: eq. 45 con il termine in p
        double lbs = r.LscatDb - 10.1 * Math.Pow(-Math.Log10(p / 50), 0.7);
        const double eta = 2.5;
        double lbam = -eta * Math.Log(Math.Exp(-lba / eta) + Math.Exp(-r.LdiffDb / eta));
        double lb = -5 * Math.Log10(Math.Pow(10, -0.2 * lbs) + Math.Pow(10, -0.2 * lbam));
        lb = Math.Max(lb, r.LfsDb);
        r.P452Ok = true; r.P452DeltaN = dN; r.P452K50 = k50; r.P452Beta = beta; r.P452Lba = lba; r.P452Lbs = lbs; r.P452Lb = lb;
        r.P452Alf = alf; r.P452Ast = ast; r.P452Asr = asr; r.P452Act = act; r.P452Acr = acr; r.P452Ag = ag;
        CalcLog.W($"P.452 § 4.5 (p = {p:0}%): ΔN mediano GFS {dN:0.0} → k50 {k50:0.00}, ae {ae:0} km; orizzonti dlt {dlt:0.0} km, dlr {dlr:0.0} km; terra dtm {dtm:0} km, interna dlm {dlm:0} km, τ {tau:0.000}");
        CalcLog.W($"   μ1 {mu1:0.000}, μ4 {mu4:0.000}, β0 {beta0:0.00}%, μ2 {mu2:0.000} (α {alpha:0.000}), μ3 {mu3:0.000} (hm {hm:0} m), β {beta:0.000}%, Γ {gam:0.000} → A(p) {ap:0.0} dB");
        CalcLog.W($"   Af = 102,45 + 20 log f + 20 log(dlt+dlr) + Alf {alf:0.0} + Ast {ast:0.0} + Asr {asr:0.0} + Act {act:0.0} + Acr {acr:0.0} = {af:0.0} dB; γd·θ' = {gd:0.000} × {thp:0.0} mrad = {gd * thp:0.0} dB; gas {ag:0.0} dB");
        CalcLog.W($"   Lba {lba:0.0} dB; troposcatter al {p:0}% {lbs:0.0} dB; diffrazione {r.LdiffDb:0.0} dB → P.452 complessiva {lb:0.0} dB (solo confronto, la perdita prevista resta la nostra {r.LpredDb:0.0} dB)");
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
    /// con il raggio equivalente k. Il terreno viene da DEM 30 m (passo 150 m).
    /// </summary>
    /// <summary>
    /// «Via monte»: con il corrispondente chiuso, si puntano le antenne su un rilievo vicino ben visibile. Il versante
    /// illuminato fa da diffusore: tratta 1 stazione → rilievo nello spazio libero, tratta 2 rilievo → corrispondente
    /// calcolata come un trasmettitore in cima (stessa previsione, senza equazione parabolica). Conversione bistatica
    /// (equazione del radar): L = Lfs(R1) + L2 + 10·log(λ²/(4π·σ)), σ = σ0·A, σ0 = −15 dB (versante montuoso in VHF/UHF),
    /// A = area illuminata dal fascio a distanza R1 (al massimo 20 km²).
    /// </summary>
    static async Task ViaMountain(LinkResult r, Station tx, Station rx, Options o, Srtm srtm, IList<ProfileResult> prof, CancellationToken ct)
    {
        // 1.3.2: giro completo d'orizzonte ogni 5° (tranne ±15° attorno al corrispondente): per ogni direzione il punto
        // d'orizzonte illuminato (sopra 100 m, fra 1 e 60 km) è un possibile diffusore. Stima rapida per tutti, calcolo
        // completo della tratta dal diffusore al corrispondente per i 4 migliori: si ottiene il puntamento consigliato.
        const double maxKm = 60, stepKm = 0.3, sigma0Db = -15, backKm = 30;
        int ns = (int)(maxKm / stepKm);
        var azs = Enumerable.Range(0, 72).Select(i => i * 5.0).Where(a => Math.Abs(((a - r.AzDeg + 540) % 360) - 180) > 15).ToArray();
        var pts = new List<LatLon>(azs.Length * ns);
        foreach (var a in azs) for (int sIdx = 1; sIdx <= ns; sIdx++) pts.Add(Geo.Destination(tx.Pos, a, sIdx * stepKm));
        var (h, _) = await srtm.Heights(pts, ct);
        double hts = r.HtxAsl, ae = 4.0 / 3 * Re;
        double lam = 299.792458 / o.FreqMHz;
        double bw = Math.Min(60, Math.Sqrt(29000 / Math.Pow(10, r.GtxDbi / 10))) * Math.PI / 180;
        var cands = new List<(double az, double km, double hM, LatLon pos)>();
        for (int ai = 0; ai < azs.Length; ai++)
        {
            double best = double.MinValue; int bi = -1;
            for (int sIdx = 1; sIdx <= ns; sIdx++)
            {
                double dk = sIdx * stepKm, hg = h[ai * ns + sIdx - 1];
                double ang = (hg - dk * dk / (2 * ae) * 1000 - hts) / dk;
                if (ang > best) { best = ang; bi = sIdx; }
            }
            if (bi < 0) continue;
            double hk = h[ai * ns + bi - 1], km = bi * stepKm;
            if (hk >= 100 && km >= 1 && best > 0) cands.Add((azs[ai], km, hk, pts[ai * ns + bi - 1]));
        }
        // stima rapida: spazio libero fino al diffusore + conversione bistatica + 10 dB per grado d'orizzonte dal diffusore
        // verso il corrispondente (G3YGF; Bray: 1° = 111 km)
        int nb = (int)(backKm / stepKm);
        var bpts = new List<LatLon>(cands.Count * nb);
        foreach (var c in cands) { double az2 = Geo.Azimuth(c.pos, rx.Pos); for (int sIdx = 1; sIdx <= nb; sIdx++) bpts.Add(Geo.Destination(c.pos, az2, sIdx * stepKm)); }
        var (hb, _) = cands.Count > 0 ? await srtm.Heights(bpts, ct) : (new double[0], new bool[0]);
        double Conv(double r1) => 10 * Math.Log10(lam * lam / (4 * Math.PI * Math.Pow(10, sigma0Db / 10) * Math.Min(2e7, Math.Pow(r1 * bw, 2))));
        var scored = new List<(double az, double km, double hM, LatLon pos, double quick, double horDeg)>();
        for (int ci = 0; ci < cands.Count; ci++)
        {
            var c = cands[ci]; double hp = c.hM + 10, hor = double.MinValue;
            for (int sIdx = 1; sIdx <= nb; sIdx++)
            {
                double dk = sIdx * stepKm, hg = hb[ci * nb + sIdx - 1];
                hor = Math.Max(hor, Math.Atan((hg - dk * dk / (2 * ae) * 1000 - hp) / (dk * 1000)) * 180 / Math.PI);
            }
            double r1 = c.km * 1000;
            // con l'opzione «senza perdita fino al monte» conta solo l'orizzonte dal punto verso il corrispondente (a parità, il punto più alto)
            double quick = Refractivity.ViaIdeal ? 10 * Math.Max(0, hor) - c.hM / 100 : 20 * Math.Log10(4 * Math.PI * r1 / lam) + Conv(r1) + 10 * Math.Max(0, hor);
            scored.Add((c.az, c.km, c.hM, c.pos, quick, hor));
        }
        var pick = new List<(double az, double km, double hM, LatLon pos, double quick, double horDeg)>();
        foreach (var c in scored.OrderBy(c => c.quick))
        {
            if (pick.Any(p => Math.Abs(((c.az - p.az + 540) % 360) - 180) < 15)) continue;
            pick.Add(c); if (pick.Count == 4) break;
        }
        CalcLog.Section($"Puntamento migliore da {tx.Loc} verso {rx.Loc}: giro d'orizzonte ogni 5°, {cands.Count} punti d'orizzonte illuminati sopra 100 m, calcolo completo per i {pick.Count} più promettenti");
        foreach (var c in scored.OrderBy(c => c.quick).Take(12))
            CalcLog.W($"   stima rapida: azimut {c.az:0}°, {c.km:0.0} km, {c.hM:0} m, orizzonte dal diffusore verso il corrispondente {c.horDeg:+0.0;-0.0}° → {c.quick:0.0} dB + tratta");
        foreach (var c in pick)
        {
            var peak = new Station { Loc = LocatorX.To10(c.pos.Lat, c.pos.Lon), Pos = c.pos, Precise = true, AglM = 10, BoomM = tx.BoomM, PowerW = tx.PowerW, CableDb = tx.CableDb };
            var leg = await Compute(peak, rx, o, srtm, prof, ct, sub: true);
            double r1 = c.km * 1000;
            double lfs1 = 20 * Math.Log10(4 * Math.PI * r1 / lam);
            double conv = Conv(r1);
            double tot = lfs1 + leg.LpredDb + conv;
            r.Via.Add(new ViaPeak { AzDeg = c.az, DistKm = c.km, HeightM = c.hM, Pos = c.pos, L1Db = lfs1, L2Db = leg.LpredDb, ConvDb = conv, TotalDb = tot, Mechanism2 = leg.Mechanism, Loc10 = peak.Loc });
            CalcLog.W($"via azimut {c.az:0}°: diffusore {peak.Loc} a {c.km:0.0} km, quota {c.hM:0} m → Lfs1 {lfs1:0.0} dB + tratta dal diffusore {leg.LpredDb:0.0} dB ({leg.Mechanism}) + conversione bistatica {conv:+0.0;-0.0} dB (σ0 {sigma0Db} dB) = {tot:0.0} dB; diretto {r.LpredDb:0.0} dB");
        }
        if (r.Via.Count > 0)
        {
            var b = r.Via.OrderBy(v => v.RankDb).First();
            r.BestPointAz = b.RankDb < r.LpredDb ? b.AzDeg : r.AzDeg;
            CalcLog.W($"puntamento consigliato{(Refractivity.ViaIdeal ? " (opzione senza perdita fino al monte: stazione portata sul punto)" : "")}: {(b.RankDb < r.LpredDb ? $"azimut {b.AzDeg:0}° (punto {b.Loc10} a {b.DistKm:0.0} km, {b.HeightM:0} m), {r.LpredDb - b.RankDb:0.0} dB meglio del diretto" : $"diretto, azimut {r.AzDeg:0}°")}");
        }
    }

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
        CalcLog.W($"suolo DEM 30 m {tx.GroundM:0.0} m, antenna {hts:0.0} m s.l.m., ricevente {hRx:0.0} m dal suolo, k {k:0.000} (ae {ae:0} km), raggio {maxKm:0} km, {naz} azimut, passo {stepKm * 1000:0} m, {pts.Count} punti");
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
