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
    public double[] DistAlong = Array.Empty<double>(), Terrain = Array.Empty<double>();
    public double HtxAsl, HrxAsl;
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

    /// <summary>Temperatura d'antenna tipica verso l'orizzonte (K): rumore galattico e terreno.</summary>
    public static double AntennaTempK(double f) => f < 300 ? 400 : f < 1000 ? 120 : 70;

    /// <summary>Rumore nella banda B (Hz) con cifra di rumore e perdita del cavo, in dBm.</summary>
    public static double NoiseDbm(double bwHz, double nfDb, double cableDb, double f)
    {
        double t0 = 290;
        double lc = Math.Pow(10, cableDb / 10);
        // antenna + cavo (temperatura fisica 290 K) + ricevitore, riportati all'ingresso del ricevitore
        double tsys = AntennaTempK(f) / lc + t0 * (1 - 1 / lc) + t0 * (Math.Pow(10, nfDb / 10) - 1);
        return 10 * Math.Log10(1.380649e-23 * tsys * bwHz) + 30;
    }

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
    }

    /// <summary>Calcolo completo; prof = profili di previsione lungo il percorso (o null se non c'è previsione).</summary>
    public static async Task<LinkResult> Compute(Station tx, Station rx, Options o, Srtm srtm, IList<ProfileResult> prof, CancellationToken ct)
    {
        var r = new LinkResult { FreqMHz = o.FreqMHz };
        r.DistKm = Geo.DistKm(tx.Pos, rx.Pos);
        r.AzDeg = Geo.Azimuth(tx.Pos, rx.Pos);
        if (r.DistKm < 0.05) { r.Notes.Add("le due stazioni coincidono"); return r; }
        double step = Math.Max(0.03, Math.Min(0.25, r.DistKm / 4000));      // 30 m fino a 120 km, poi più largo
        var pts = PathPoints(tx.Pos, rx.Pos, step, out var dist);
        var (h, ok) = await srtm.Heights(pts, ct);
        r.TilesMissing = srtm.Missing;
        r.DistAlong = dist; r.Terrain = h;
        tx.GroundM = h[0]; rx.GroundM = h[h.Length - 1];
        double hts = tx.AntennaAsl, hrs = rx.AntennaAsl;
        if (!tx.Precise && tx.AslM > 0 && hts < tx.GroundM + 1) { r.Notes.Add($"la quota s.l.m. indicata ({tx.AslM:0} m) è sotto il suolo SRTM ({tx.GroundM:0} m): uso il suolo + 2 m"); hts = tx.GroundM + 2; }
        r.HtxAsl = hts; r.HrxAsl = hrs;

        // ---- raggio equivalente dalla previsione (media del primo km lungo il percorso)
        double k = 4.0 / 3;
        var good = prof?.Where(p => p != null).ToList() ?? new List<ProfileResult>();
        if (good.Count > 0) { k = KFromDn(good.Average(p => p.DN1km)); r.ForecastUsed = true; }
        r.K = k;
        double ae = k * Re, d = r.DistKm, lam = 299.792458 / o.FreqMHz, fGHz = o.FreqMHz / 1000;
        double ce = 1 / ae;

        // ---- spazio libero
        r.LfsDb = 92.44 + 20 * Math.Log10(fGHz) + 20 * Math.Log10(d);

        // ---- Bullington (P.452 § 4.2.1)
        int n = h.Length;
        double stim = double.MinValue;
        for (int i = 1; i < n - 1; i++) stim = Math.Max(stim, (h[i] + 500 * ce * dist[i] * (d - dist[i]) - hts) / dist[i]);
        double str = (hrs - hts) / d;
        double luc;
        if (n < 3 || stim < str)
        {
            r.Los = true;
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
            double srim = double.MinValue;
            for (int i = 1; i < n - 1; i++) srim = Math.Max(srim, (h[i] + 500 * ce * dist[i] * (d - dist[i]) - hrs) / (d - dist[i]));
            double db = (hrs - hts + srim * d) / (stim + srim);
            db = Math.Max(0.001, Math.Min(d - 0.001, db));
            double nub = (hts + stim * db - (hts * (d - db) + hrs * db) / d) * Math.Sqrt(0.002 * d / (lam * db * (d - db)));
            luc = J(nub);
        }
        r.LbullDb = luc + (1 - Math.Exp(-luc / 6)) * (10 + 0.02 * d);
        r.LdiffDb = r.LfsDb + r.LbullDb;

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

        // ---- combinazione (somma delle potenze) senza tropo
        r.LbaseDb = -10 * Math.Log10(Math.Pow(10, -r.LdiffDb / 10) + Math.Pow(10, -r.LscatDb / 10));
        r.LbaseDb = Math.Max(r.LbaseDb, r.LfsDb);

        // ---- tropo dalla previsione propria: guadagno medio lungo il percorso, condotto se copre quasi tutto il tragitto
        r.LpredDb = r.LbaseDb;
        r.Mechanism = r.Los ? "visibilità diretta" : r.LdiffDb <= r.LscatDb ? "diffrazione" : "troposcatter";
        if (good.Count > 0)
        {
            double gmean = good.Average(p => p.GainDb(o.FreqMHz));
            r.TropoGainDb = gmean;
            double dmin = ProfileResult.MinDuctM(o.FreqMHz);
            var ducts = good.Where(p => p.HasDuct && p.DuctThick >= dmin).ToList();
            r.DuctFrac = (double)ducts.Count / good.Count;
            // accoppiamento: un'antenna "entra" nel condotto se sta sotto la sua cima (e non troppo sotto la base di uno elevato)
            bool In(double hAsl) => ducts.Count > 0 && hAsl <= ducts.Average(p => p.DuctTop) + 50 && hAsl >= ducts.Average(p => p.SurfaceDuct ? 0 : p.DuctBase) - 150;
            bool inT = In(hts), inR = In(hrs);
            r.DuctCoupled = inT && inR;
            double lpred = r.LbaseDb - Math.Max(0, gmean);
            if (gmean < 0) lpred = r.LbaseDb - gmean;             // sub-rifrazione: perdita in più
            if (r.DuctFrac >= 0.8 && !r.Los)
            {
                double coupling = inT && inR ? 10 : inT || inR ? 20 : 35;
                r.LductDb = r.LfsDb + coupling + 0.01 * d;
                if (r.LductDb < lpred) { lpred = r.LductDb; r.Mechanism = "condotto"; }
            }
            else if (gmean >= 10 && !r.Los) r.Mechanism = "tropo (super-rifrazione)";
            r.LpredDb = Math.Max(r.LfsDb, lpred);
        }
        else r.Notes.Add("nessuna previsione GFS sul percorso: calcolo con atmosfera normale (k = 4/3)");

        // ---- livello e rapporto segnale/rumore
        r.EirpDbm = 10 * Math.Log10(Math.Max(0.001, tx.PowerW) * 1000) - tx.CableDb + r.GtxDbi;
        r.PrxDbm = r.EirpDbm - r.LpredDb + r.GrxDbi - rx.CableDb;
        r.NoiseSsbDbm = NoiseDbm(o.BwSsb, rx.NfDb, 0, o.FreqMHz);    // il cavo è già tolto dal segnale
        r.SnrSsb = r.PrxDbm - r.NoiseSsbDbm;
        r.SnrCw = r.PrxDbm - NoiseDbm(o.BwCw, rx.NfDb, 0, o.FreqMHz);
        r.SnrFt8 = r.PrxDbm - NoiseDbm(o.BwFt8, rx.NfDb, 0, o.FreqMHz);
        if (r.TilesMissing > 0) r.Notes.Add($"{r.TilesMissing} riquadri SRTM1 non disponibili: lì il terreno vale 0 m");
        return r;
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
        }
        return (az, first, far);
    }
}
