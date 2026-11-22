namespace TropoVHF.Core;

/// <summary>
/// Corridoi d'apertura dagli spot (criterio dell'autore): un collegamento da solo non disegna mai un condotto, perché può
/// essere lo scatter di un aereo, un'apertura di pochi minuti o un errore. Un corridoio esiste quando, nella stessa banda:
/// - almeno MinPaths coppie di stazioni indipendenti (nessuna stazione in comune) lavorano su tratte di almeno MinKm;
/// - le tratte sono parallele: azimut entro ±MaxAngle gradi, punti medi entro OffsetKm dall'asse comune;
/// - tutte negli ultimi WindowMin minuti, e il fascio si ripete a distanza di almeno RepeatMin minuti (persistenza);
/// - per l'APRS un salto conta solo se lo stesso salto si ripete (pacchetti brevi: aerei in transito).
/// Fonti: spot dei cluster, PSKReporter (MQTT), QSO e decodifiche proprie, salti APRS in 2 m.
///
/// Dalla 1.3.4 (per evitare la sovrastima vista con molti corridoi lunghi insieme):
/// - le tratte oltre MaxKm (1500 km) contano solo se il fascio è confermato da almeno due fonti diverse
///   (es. PSKReporter e cluster): da una fonte sola sono spesso EME, meteor scatter o locatori sbagliati;
/// - il minimo garantito vale solo vicino alle tratte (entro NearKm dalla linea) e nella loro parte centrale
///   (fra EndFrac e 1−EndFrac della lunghezza), non su tutto il rettangolo né sulle stazioni alle estremità;
/// - il minimo cresce con la lunghezza media, con il numero di tratte indipendenti e con l'S/N mediano (se noto).
/// </summary>
public sealed class Corridors
{
    public double MinKm = 300, MaxAngle = 15, OffsetKm = 150, MaxKm = 1500, NearKm = 60, EndFrac = 0.15;
    public int MinPaths = 3, WindowMin = 30, RepeatMin = 10, AprsRepeatMin = 2;

    public sealed class Obs { public DateTime T; public string Band = "", A = "", B = "", Src = ""; public LatLon PA, PB; public double Km, Az, Snr = double.NaN; }

    public sealed class Corridor
    {
        public string Band = "";
        public LatLon Mid; public double AzDeg, HalfLenKm, HalfWidthKm, MeanKm;
        public int Paths; public DateTime First, Last;
        public List<Obs> Members = new();
        public string Sources = "";
        /// <summary>S/N mediano delle tratte che lo portano (NaN se nessuna fonte lo dà, es. cluster).</summary>
        public double MedianSnr = double.NaN;
        /// <summary>
        /// Guadagno minimo garantito vicino alle tratte. Per la lunghezza: 10 dB a 300 km, 16 dB da 900 km in su.
        /// Poi si pesa (0..1) con il numero di tratte indipendenti (3 → 0,25, 6 o più → 1) e con l'S/N mediano
        /// (−20 dB → 0,5, 0 dB o più → 1; non noto → 0,75), metà e metà, a partire da 7 dB (inizio della scala).
        /// Esempi: 3 tratte senza S/N → 11,5 dB; 6 tratte a 0 dB su 900 km → 16 dB.
        /// </summary>
        public double FloorDb
        {
            get
            {
                double len = 10 + 6 * Math.Max(0, Math.Min(1, (MeanKm - 300) / 600));
                double kp = Math.Max(0.25, Math.Min(1, (Paths - 2) / 4.0));
                double ks = double.IsNaN(MedianSnr) ? 0.75 : Math.Max(0.5, Math.Min(1, (MedianSnr + 20) / 20 * 0.5 + 0.5));
                return 7 + (len - 7) * (0.5 * kp + 0.5 * ks);
            }
        }
    }

    readonly List<Obs> _obs = new();
    List<Corridor> _cache = new(); string _cacheKey = "";

    public void Add(string band, string a, LatLon pa, string b, LatLon pb, DateTime t, string src, double snr = double.NaN)
    {
        double km = Geo.DistKm(pa, pb);
        if (km < MinKm || string.IsNullOrEmpty(band)) return;
        var o = new Obs { T = t, Band = band, A = (a ?? "").ToUpperInvariant(), B = (b ?? "").ToUpperInvariant(), PA = pa, PB = pb, Km = km, Src = src, Az = Geo.Azimuth(pa, pb), Snr = snr };
        lock (_obs)
        {
            _obs.Add(o);
            var cut = t.AddMinutes(-90);
            if (_obs.Count > 20000 || _obs[0].T < cut) _obs.RemoveAll(x => x.T < cut);
            _cacheKey = "";
        }
    }

    // coordinate locali in km attorno a un punto (equirettangolare, sufficiente per qualche centinaio di km)
    static (double x, double y) Local(LatLon o, LatLon p)
    {
        double k = 111.2;
        return ((p.Lon - o.Lon) * k * Math.Cos(o.Lat * Math.PI / 180), (p.Lat - o.Lat) * k);
    }
    static double AxialDiff(double a, double b) { double d = Math.Abs(((a - b) % 180 + 180) % 180); return Math.Min(d, 180 - d); }
    static LatLon MidOf(Obs o) => new LatLon((o.PA.Lat + o.PB.Lat) / 2, (o.PA.Lon + o.PB.Lon) / 2);

    /// <summary>Corridoi attivi per la banda, all'ora now (cache per minuto).</summary>
    public List<Corridor> Active(string band, DateTime now)
    {
        string key = band + "|" + now.ToString("yyyyMMddHHmm");
        lock (_obs)
        {
            if (key == _cacheKey) return _cache;
            var win = _obs.Where(o => o.Band == band && o.T <= now && (now - o.T).TotalMinutes <= WindowMin).ToList();
            // APRS: il salto conta solo se lo stesso salto (stessa coppia) si ripete a distanza di almeno AprsRepeatMin minuti
            win = win.Where(o => o.Src != "APRS" || win.Any(p => p.Src == "APRS" && Same(o, p) && Math.Abs((p.T - o.T).TotalMinutes) >= AprsRepeatMin)).ToList();
            var res = new List<Corridor>();
            var used = new HashSet<Obs>();
            foreach (var seed in win.OrderByDescending(o => o.T))
            {
                if (used.Contains(seed)) continue;
                var m0 = MidOf(seed);
                var fam = win.Where(o => !used.Contains(o) && AxialDiff(o.Az, seed.Az) <= MaxAngle).Where(o =>
                {
                    var (x, y) = Local(m0, MidOf(o));
                    double a = seed.Az * Math.PI / 180;
                    double along = x * Math.Sin(a) + y * Math.Cos(a), perp = -x * Math.Cos(a) + y * Math.Sin(a);
                    return Math.Abs(perp) <= OffsetKm && Math.Abs(along) <= (seed.Km + o.Km) / 2;
                }).ToList();
                // tratte molto lunghe: valgono solo se il fascio arriva da almeno due fonti diverse
                if (fam.Any(o => o.Km > MaxKm) && fam.Select(o => o.Src).Distinct().Count() < 2)
                    fam = fam.Where(o => o.Km <= MaxKm).ToList();
                if (fam.Count == 0) { used.Add(seed); continue; }
                // coppie indipendenti: nessuna stazione in comune
                var calls = new HashSet<string>(); var indep = new List<Obs>();
                foreach (var o in fam.OrderByDescending(o => o.T))
                    if (!calls.Contains(o.A) && !calls.Contains(o.B)) { indep.Add(o); calls.Add(o.A); calls.Add(o.B); }
                if (indep.Count < MinPaths) continue;
                // persistenza: il fascio deve coprire almeno RepeatMin minuti
                if ((fam.Max(o => o.T) - fam.Min(o => o.T)).TotalMinutes < RepeatMin) continue;
                foreach (var o in fam) used.Add(o);
                // asse: punto medio e azimut medio (assiale), estensione e larghezza dai membri
                double sx = 0, sy = 0;
                foreach (var o in fam) { double a2 = 2 * (o.Az % 180) * Math.PI / 180; sx += Math.Cos(a2); sy += Math.Sin(a2); }
                double az = (Math.Atan2(sy, sx) * 180 / Math.PI / 2 + 180) % 180;
                var mid = new LatLon(fam.Average(o => MidOf(o).Lat), fam.Average(o => MidOf(o).Lon));
                double ar = az * Math.PI / 180, half = 0, wid = 0;
                foreach (var o in fam)
                    foreach (var p in new[] { o.PA, o.PB })
                    {
                        var (x, y) = Local(mid, p);
                        half = Math.Max(half, Math.Abs(x * Math.Sin(ar) + y * Math.Cos(ar)));
                        wid = Math.Max(wid, Math.Abs(-x * Math.Cos(ar) + y * Math.Sin(ar)));
                    }
                res.Add(new Corridor
                {
                    Band = band, Mid = mid, AzDeg = az, HalfLenKm = half, HalfWidthKm = Math.Max(60, wid + 40), MeanKm = fam.Average(o => o.Km),
                    Paths = indep.Count, First = fam.Min(o => o.T), Last = fam.Max(o => o.T), Members = fam, MedianSnr = Median(fam.Select(o => o.Snr)),
                    Sources = string.Join(", ", fam.GroupBy(o => o.Src).Select(g => $"{g.Key} {g.Count()}"))
                });
            }
            _cache = res; _cacheKey = key;
            return res;
        }
    }

    static double Median(IEnumerable<double> v)
    {
        var l = v.Where(x => !double.IsNaN(x)).OrderBy(x => x).ToList();
        if (l.Count == 0) return double.NaN;
        return l.Count % 2 == 1 ? l[l.Count / 2] : (l[l.Count / 2 - 1] + l[l.Count / 2]) / 2;
    }

    static bool Same(Obs a, Obs b) => (a.A == b.A && a.B == b.B) || (a.A == b.B && a.B == b.A);

    /// <summary>
    /// Corridoio con il minimo più alto che vale nel punto (o null): il punto deve stare entro NearKm da una delle tratte
    /// e nella sua parte centrale, non vicino alle stazioni.
    /// </summary>
    public Corridor At(string band, DateTime now, double lat, double lon)
    {
        Corridor best = null;
        var pt = new LatLon(lat, lon);
        foreach (var c in Active(band, now))
        {
            if (best != null && c.FloorDb <= best.FloorDb) continue;
            var (x, y) = Local(c.Mid, pt);
            double a = c.AzDeg * Math.PI / 180;
            if (Math.Abs(x * Math.Sin(a) + y * Math.Cos(a)) > c.HalfLenKm || Math.Abs(-x * Math.Cos(a) + y * Math.Sin(a)) > c.HalfWidthKm) continue;
            foreach (var o in c.Members)
                if (NearMiddle(o, pt)) { best = c; break; }
        }
        return best;
    }

    /// <summary>Il punto sta entro NearKm dalla tratta, fra EndFrac e 1−EndFrac della sua lunghezza.</summary>
    bool NearMiddle(Obs o, LatLon p)
    {
        var m = MidOf(o);
        var (ax, ay) = Local(m, o.PA); var (bx, by) = Local(m, o.PB); var (px, py) = Local(m, p);
        double dx = bx - ax, dy = by - ay, l2 = dx * dx + dy * dy;
        if (l2 < 1) return false;
        double t = ((px - ax) * dx + (py - ay) * dy) / l2;
        if (t < EndFrac || t > 1 - EndFrac) return false;
        double qx = ax + t * dx - px, qy = ay + t * dy - py;
        return qx * qx + qy * qy <= NearKm * NearKm;
    }

    /// <summary>Tratto centrale di una tratta (fra EndFrac e 1−EndFrac), per il disegno.</summary>
    public (LatLon a, LatLon b) MiddlePart(Obs o)
    {
        double d = o.Km, az = o.Az;
        return (Geo.Destination(o.PA, az, d * EndFrac), Geo.Destination(o.PA, az, d * (1 - EndFrac)));
    }

    /// <summary>I quattro vertici della fascia del corridoio (per il disegno).</summary>
    public static LatLon[] Polygon(Corridor c)
    {
        double a = c.AzDeg;
        var e1 = Geo.Destination(c.Mid, a, c.HalfLenKm); var e2 = Geo.Destination(c.Mid, a + 180, c.HalfLenKm);
        return new[] { Geo.Destination(e1, a + 90, c.HalfWidthKm), Geo.Destination(e1, a - 90, c.HalfWidthKm), Geo.Destination(e2, a - 90, c.HalfWidthKm), Geo.Destination(e2, a + 90, c.HalfWidthKm) };
    }
}
