namespace TropoVHF.Core;

/// <summary>
/// Quarantena dei collegamenti lunghi. Un collegamento oltre MinKm (1000 km) non si mostra subito: resta trattenuto
/// finché un altro collegamento, tra stazioni diverse e lungo almeno metà, non lo conferma con uno di questi criteri:
/// 1) stesso ascoltatore (o stessa stazione a un capo) e l'altra stazione nella stessa direzione, entro MaxAngle;
/// 2) corridoio simile: entrambi i capi entro CorridorKm dai capi dell'altro collegamento;
/// 3) parallelo: direzione entro MaxAngle e distanza laterale entro ParallelKm, sovrapposti lungo il percorso.
/// Allo svelamento si mostrano entrambi. I collegamenti "certi" (decodifiche del proprio ricevitore) sono sempre visibili
/// e servono da conferma per gli altri.
/// </summary>
public sealed class PathGate
{
    public double MinKm = 1000, CorridorKm = 200, ParallelKm = 200, MaxAngle = 10;
    public int KeepMinutes = 60;
    /// <summary>Vero se la stazione risulta attiva sotto i 144 MHz negli stessi minuti (dati MQTT delle altre bande).</summary>
    public Func<string, string, DateTime, bool> LowBand = (_, _, _) => false;
    /// <summary>Regola imparata per la stazione sui collegamenti lunghi: +1 mostra subito, −1 mai, 0 quarantena normale.</summary>
    public Func<string, int> StationRule = _ => 0;
    /// <summary>Eventi per le segnalazioni sospette: (banda, A, B, km, motivo).</summary>
    public event Action<string, string, string, double, string> Held, Revealed, Expired;

    bool Low(P p) => LowBand(p.A, p.Band, p.T) || LowBand(p.B, p.Band, p.T);

    sealed class P
    {
        public string Key = "", Band = "", A = "", B = "";
        public LatLon PA, PB;
        public double Km;
        public DateTime T;
        public bool Trusted;
    }

    readonly object _lock = new();
    readonly Dictionary<string, P> _paths = new();
    readonly HashSet<string> _revealed = new();
    public event Action<string> Log;

    static string Key(string band, string a, string b) =>
        band + "|" + (string.CompareOrdinal(a, b) < 0 ? a + "|" + b : b + "|" + a);

    public void Add(string band, string callA, LatLon pa, string callB, LatLon pb, DateTime t, bool trusted = false)
    {
        if (string.IsNullOrEmpty(callA) || string.IsNullOrEmpty(callB) || callA == callB) return;
        var p = new P { Key = Key(band, callA, callB), Band = band, A = callA, B = callB, PA = pa, PB = pb, Km = Geo.DistKm(pa, pb), T = t, Trusted = trusted };
        lock (_lock)
        {
            bool isNew = !_paths.TryGetValue(p.Key, out var old);
            if (!isNew) p.Trusted |= old.Trusted;
            _paths[p.Key] = p;
            if (isNew && !p.Trusted && p.Km > MinKm) Held?.Invoke(band, callA, callB, p.Km, "trattenuto in quarantena: nessuna conferma ancora");
            if (p.Trusted) _revealed.Add(p.Key);
            var cut = t.AddMinutes(-KeepMinutes);
            foreach (var o in _paths.Values)
            {
                if (o == p || o.Band != band || o.T < cut) continue;
                // il nuovo conferma un lungo trattenuto, o un altro conferma il nuovo lungo
                // chi è attivo sotto i 144 MHz non conferma e non viene confermato
                if ((o.Km > MinKm || p.Km > MinKm) && (Low(o) || Low(p))) continue;
                if (o.Km > MinKm && !_revealed.Contains(o.Key) && p.Km >= o.Km * 0.5 && Similar(o, p, out var why1)) Reveal(o, p, why1);
                if (p.Km > MinKm && !_revealed.Contains(p.Key) && o.Km >= p.Km * 0.5 && Similar(p, o, out var why2)) Reveal(p, o, why2);
            }
        }
    }

    void Reveal(P held, P by, string why)
    {
        bool a = _revealed.Add(held.Key);
        if (by.Km > MinKm) _revealed.Add(by.Key);
        if (a) Log?.Invoke($"{DateTime.UtcNow:HH:mm:ss}  svelato {held.A}–{held.B} {held.Km:0} km: {why} con {by.A}–{by.B} {by.Km:0} km");
        if (a) Revealed?.Invoke(held.Band, held.A, held.B, held.Km, $"svelato: {why} con {by.A}–{by.B} {by.Km:0} km");
    }

    /// <summary>Visibile se corto, certo o svelato; un collegamento mai registrato (posizioni ignote) non si giudica.</summary>
    public bool Visible(string band, string a, string b)
    {
        if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b)) return true;
        var k = Key(band, a, b);
        lock (_lock)
        {
            if (!_paths.TryGetValue(k, out var p) || p.Km <= MinKm || p.Trusted) return true;
            int ra = StationRule(p.A), rb = StationRule(p.B);
            if (ra < 0 || rb < 0) return false;          // regola: i lunghi di questa stazione non si mostrano
            if ((ra > 0 || rb > 0) && !Low(p)) return true;
            // anche se svelato: se una delle due stazioni lavora sotto i 144 MHz in quei minuti, resta trattenuto
            return _revealed.Contains(k) && !Low(p);
        }
    }

    /// <summary>Collegamenti lunghi trattenuti nella banda e nell'intervallo.</summary>
    public int HeldCount(string band, int minutes, DateTime now)
    {
        var cut = now.AddMinutes(-minutes);
        lock (_lock) return _paths.Values.Count(p => p.Band == band && p.T > cut && p.Km > MinKm && !p.Trusted && (!_revealed.Contains(p.Key) || Low(p)));
    }

    public void Prune(DateTime now)
    {
        var cut = now.AddMinutes(-Math.Max(KeepMinutes, 60));
        lock (_lock)
            foreach (var k in _paths.Where(kv => kv.Value.T < cut).Select(kv => kv.Key).ToList())
            {
                var p = _paths[k];
                if (p.Km > MinKm && !p.Trusted && !_revealed.Contains(k)) Expired?.Invoke(p.Band, p.A, p.B, p.Km, "scaduto senza conferme");
                _paths.Remove(k); _revealed.Remove(k);
            }
    }

    // ------------------------------------------------------------------ geometria

    bool Similar(P p, P q, out string why)
    {
        why = "";
        if (p.Key == q.Key) return false;
        // 1) stazione in comune e stessa direzione
        foreach (var (s, ps, o) in new[] { (p.A, p.PA, p.PB), (p.B, p.PB, p.PA) })
        {
            LatLon? qo = q.A == s ? q.PB : q.B == s ? q.PA : (LatLon?)null;
            if (qo == null) continue;
            if (AngleDiff(Geo.Azimuth(ps, o), Geo.Azimuth(ps, qo.Value)) <= MaxAngle) { why = $"stessa stazione {s}, stessa direzione"; return true; }
        }
        // 2) corridoio simile
        if ((Geo.DistKm(p.PA, q.PA) <= CorridorKm && Geo.DistKm(p.PB, q.PB) <= CorridorKm) ||
            (Geo.DistKm(p.PA, q.PB) <= CorridorKm && Geo.DistKm(p.PB, q.PA) <= CorridorKm)) { why = "corridoio simile"; return true; }
        // 3) parallelo: direzione (senza verso) entro MaxAngle, centro dell'altro entro ParallelKm dalla linea e sopra il percorso
        var mp = Mid(p.PA, p.PB); var mq = Mid(q.PA, q.PB);
        double dirP = Geo.Azimuth(mp, p.PB), dirQ = Geo.Azimuth(mq, q.PB);
        double ang = Math.Min(AngleDiff(dirP, dirQ), AngleDiff(dirP, dirQ + 180));
        if (ang > MaxAngle) return false;
        var (xt, at) = CrossAlong(p.PA, p.PB, mq);
        if (Math.Abs(xt) <= ParallelKm && at >= 0 && at <= p.Km) { why = "percorso parallelo"; return true; }
        return false;
    }

    static double AngleDiff(double a, double b)
    {
        double d = Math.Abs(((a - b) % 360 + 360) % 360);
        return d > 180 ? 360 - d : d;
    }

    static double Rad(double d) => d * Math.PI / 180;

    static LatLon Mid(LatLon a, LatLon b)
    {
        double f1 = Rad(a.Lat), l1 = Rad(a.Lon), f2 = Rad(b.Lat), dl = Rad(b.Lon - a.Lon);
        double bx = Math.Cos(f2) * Math.Cos(dl), by = Math.Cos(f2) * Math.Sin(dl);
        double fm = Math.Atan2(Math.Sin(f1) + Math.Sin(f2), Math.Sqrt((Math.Cos(f1) + bx) * (Math.Cos(f1) + bx) + by * by));
        double lm = l1 + Math.Atan2(by, Math.Cos(f1) + bx);
        return new LatLon(fm * 180 / Math.PI, ((lm * 180 / Math.PI + 540) % 360) - 180);
    }

    /// <summary>Distanza laterale (cross-track) e lungo il percorso (along-track) di un punto rispetto al cerchio massimo A→B, in km.</summary>
    static (double xt, double at) CrossAlong(LatLon a, LatLon b, LatLon p)
    {
        double R = Geo.EarthKm;
        double d13 = Geo.DistKm(a, p) / R;
        double t13 = Rad(Geo.Azimuth(a, p)), t12 = Rad(Geo.Azimuth(a, b));
        double xt = Math.Asin(Math.Clamp(Math.Sin(d13) * Math.Sin(t13 - t12), -1, 1));
        double at = Math.Acos(Math.Clamp(Math.Cos(d13) / Math.Max(1e-12, Math.Cos(xt)), -1, 1));
        if (Math.Cos(t13 - t12) < 0) at = -at;      // il punto sta "dietro" l'inizio
        return (xt * R, at * R);
    }
}
