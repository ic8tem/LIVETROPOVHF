namespace TropoVHF.Core;

/// <summary>
/// Corridoi d'apertura dagli spot (criterio dell'autore): un collegamento da solo non disegna mai un condotto, perché può
/// essere lo scatter di un aereo, un'apertura di pochi minuti o un errore. Un corridoio esiste quando, nella stessa banda:
/// - almeno MinPaths coppie di stazioni indipendenti (nessuna stazione in comune) lavorano su tratte di almeno MinKm;
/// - le tratte sono parallele: azimut entro ±MaxAngle gradi, punti medi entro OffsetKm dall'asse comune;
/// - tutte negli ultimi WindowMin minuti, e il fascio si ripete a distanza di almeno RepeatMin minuti (persistenza);
/// - per l'APRS un salto conta solo se lo stesso salto si ripete (pacchetti brevi: aerei in transito).
/// Fonti: spot dei cluster, PSKReporter (MQTT), QSO e decodifiche proprie, salti APRS in 2 m.
/// </summary>
public sealed class Corridors
{
    public double MinKm = 300, MaxAngle = 15, OffsetKm = 150;
    public int MinPaths = 3, WindowMin = 30, RepeatMin = 10, AprsRepeatMin = 2;

    public sealed class Obs { public DateTime T; public string Band = "", A = "", B = "", Src = ""; public LatLon PA, PB; public double Km, Az; }

    public sealed class Corridor
    {
        public string Band = "";
        public LatLon Mid; public double AzDeg, HalfLenKm, HalfWidthKm, MeanKm;
        public int Paths; public DateTime First, Last;
        public List<Obs> Members = new();
        public string Sources = "";
        /// <summary>Guadagno minimo garantito sotto il corridoio: 10 dB a 300 km, 16 dB da 900 km in su.</summary>
        public double FloorDb => 10 + 6 * Math.Max(0, Math.Min(1, (MeanKm - 300) / 600));
    }

    readonly List<Obs> _obs = new();
    List<Corridor> _cache = new(); string _cacheKey = "";

    public void Add(string band, string a, LatLon pa, string b, LatLon pb, DateTime t, string src)
    {
        double km = Geo.DistKm(pa, pb);
        if (km < MinKm || string.IsNullOrEmpty(band)) return;
        var o = new Obs { T = t, Band = band, A = (a ?? "").ToUpperInvariant(), B = (b ?? "").ToUpperInvariant(), PA = pa, PB = pb, Km = km, Src = src, Az = Geo.Azimuth(pa, pb) };
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
                    Paths = indep.Count, First = fam.Min(o => o.T), Last = fam.Max(o => o.T), Members = fam,
                    Sources = string.Join(", ", fam.GroupBy(o => o.Src).Select(g => $"{g.Key} {g.Count()}"))
                });
            }
            _cache = res; _cacheKey = key;
            return res;
        }
    }

    static bool Same(Obs a, Obs b) => (a.A == b.A && a.B == b.B) || (a.A == b.B && a.B == b.A);

    /// <summary>Corridoio che copre il punto (o null).</summary>
    public Corridor At(string band, DateTime now, double lat, double lon)
    {
        foreach (var c in Active(band, now))
        {
            var (x, y) = Local(c.Mid, new LatLon(lat, lon));
            double a = c.AzDeg * Math.PI / 180;
            if (Math.Abs(x * Math.Sin(a) + y * Math.Cos(a)) <= c.HalfLenKm && Math.Abs(-x * Math.Cos(a) + y * Math.Sin(a)) <= c.HalfWidthKm) return c;
        }
        return null;
    }

    /// <summary>I quattro vertici della fascia del corridoio (per il disegno).</summary>
    public static LatLon[] Polygon(Corridor c)
    {
        double a = c.AzDeg;
        var e1 = Geo.Destination(c.Mid, a, c.HalfLenKm); var e2 = Geo.Destination(c.Mid, a + 180, c.HalfLenKm);
        return new[] { Geo.Destination(e1, a + 90, c.HalfWidthKm), Geo.Destination(e1, a - 90, c.HalfWidthKm), Geo.Destination(e2, a - 90, c.HalfWidthKm), Geo.Destination(e2, a + 90, c.HalfWidthKm) };
    }
}
