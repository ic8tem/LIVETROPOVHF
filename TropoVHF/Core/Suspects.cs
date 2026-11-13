using System.Globalization;

namespace TropoVHF.Core;

/// <summary>
/// Segnalazioni sospette e auto-apprendimento. Ogni controllo del programma che trova qualcosa di strano lascia qui una
/// segnalazione, raggruppata per tipo e nominativo. Per ogni coppia (tipo, nominativo) il programma conta gli esiti
/// favorevoli (+) e contrari (−): dopo AutoHits esiti tutti dello stesso segno impara da solo una regola. Una regola
/// decisa dall'utente vale sempre e non viene mai cambiata in automatico. Le regole stanno in Dati/APPRENDIMENTO.TXT.
///
/// Tipi:
///   eme      Q65/JT65 o "EME" oltre 1000 km con la Luna sopra l'orizzonte a entrambi i capi: escluso.
///            − ripetuti → stazione EME: i suoi Q65/JT65 lunghi si escludono anche senza Luna comune.
///            regola + → i suoi collegamenti lunghi in Q65/JT65 si trattano come tropo.
///   lungo    collegamento oltre 1000 km trattenuto in quarantena. Svelato = +, scaduto senza conferme = −.
///            regola + → i lunghi di quella stazione si mostrano subito; regola − → mai.
///   ascolto  ascoltatore PSKReporter scartato perché le stazioni che sente lavorano su altre bande (−),
///            riabilitato dai cluster umani (+). Regola + → mai scartato; regola − → sempre scartato.
///   locatore quadrato ricevuto diverso da quello di CALL3.TXT. Ogni volta lo stesso quadrato = +.
///            Regola + → vale il quadrato ricevuto (la stazione si è spostata); regola − → vale sempre CALL3.TXT.
///   centro   locatore a 6 caratteri che finisce con LL o MM: probabilmente il centro del quadrato, non il vero QTH.
///   skimmer  nominativo con -SDR, /SDR, -# o -n: per il locatore si usa il nominativo base.
/// </summary>
public sealed class SuspectStore
{
    public sealed class Item
    {
        public string Kind = "", Call = "", Other = "", Band = "", Mode = "", Detail = "";
        public double Km;
        public DateTime First, Last;
        public int Count;
    }

    public sealed class Rule
    {
        public string Kind = "", Call = "";
        public int Pos, Neg, Verdict;   // Verdict: +1 buono, −1 scarta, 0 nessuna regola
        public bool User;
        public DateTime When;
    }

    public static readonly string[] Kinds = { "eme", "lungo", "ascolto", "locatore", "cluster", "centro", "skimmer" };
    public int AutoHits = 3;
    public int Version;   // cambia a ogni novità: la linguetta si ridisegna solo quando serve

    readonly object _lock = new();
    readonly Dictionary<string, Item> _items = new();
    readonly Dictionary<string, Rule> _rules = new();
    int _dirty;
    public event Action<string> Log;

    static string PathFile => Path.Combine(Settings.DataDir, "APPRENDIMENTO.TXT");
    static string K(string kind, string call) => kind + "|" + call;

    /// <summary>Registra o aggiorna una segnalazione. Vero se è nuova o se non si vedeva da 6 ore (vale come nuovo esito).</summary>
    public bool Report(string kind, string call, string other, string band, string mode, double km, string detail, DateTime t)
    {
        if (string.IsNullOrEmpty(call)) return false;
        lock (_lock)
        {
            var key = kind + "|" + call + "|" + other + "|" + band;
            if (!_items.TryGetValue(key, out var it))
            {
                _items[key] = it = new Item { Kind = kind, Call = call, Other = other, Band = band, First = t };
                if (_items.Count > 600)
                    foreach (var old in _items.OrderBy(kv => kv.Value.Last).Take(100).Select(kv => kv.Key).ToList()) _items.Remove(old);
            }
            bool fresh = it.Count == 0 || t - it.Last > TimeSpan.FromHours(6);
            it.Mode = mode; it.Km = km; it.Detail = detail; it.Last = t; it.Count++;
            Version++;
            return fresh;
        }
    }

    /// <summary>Esito di un controllo: +1 favorevole, −1 contrario. Dopo AutoHits esiti concordi nasce una regola automatica.</summary>
    public void Outcome(string kind, string call, int sign, string why = "")
    {
        if (string.IsNullOrEmpty(call) || sign == 0) return;
        string msg = null;
        lock (_lock)
        {
            if (!_rules.TryGetValue(K(kind, call), out var r)) _rules[K(kind, call)] = r = new Rule { Kind = kind, Call = call };
            if (sign > 0) r.Pos++; else r.Neg++;
            r.When = DateTime.UtcNow;
            if (!r.User)
            {
                int v = r.Pos >= AutoHits && r.Neg == 0 ? 1 : r.Neg >= AutoHits && r.Pos == 0 ? -1 : r.Verdict != 0 && r.Pos > 0 && r.Neg > 0 ? 0 : r.Verdict;
                if (v != r.Verdict) { r.Verdict = v; msg = $"{DateTime.UtcNow:HH:mm:ss}  imparato: {kind} {call} → {(v > 0 ? "buono" : v < 0 ? "scarta" : "nessuna regola")} ({r.Pos}+ {r.Neg}−){(why != "" ? " · " + why : "")}"; }
            }
            Interlocked.Exchange(ref _dirty, 1);
            Version++;
        }
        if (msg != null) Log?.Invoke(msg);
    }

    /// <summary>Regola in vigore per (tipo, nominativo): +1 buono, −1 scarta, 0 nessuna.</summary>
    public int Verdict(string kind, string call)
    {
        if (string.IsNullOrEmpty(call)) return 0;
        lock (_lock) return _rules.TryGetValue(K(kind, call), out var r) ? r.Verdict : 0;
    }

    /// <summary>Decisione dell'utente: vince sempre. verdict 0 = torna all'apprendimento automatico.</summary>
    public void SetUser(string kind, string call, int verdict)
    {
        lock (_lock)
        {
            if (!_rules.TryGetValue(K(kind, call), out var r)) _rules[K(kind, call)] = r = new Rule { Kind = kind, Call = call };
            r.User = verdict != 0;
            r.Verdict = verdict != 0 ? verdict : r.Pos >= AutoHits && r.Neg == 0 ? 1 : r.Neg >= AutoHits && r.Pos == 0 ? -1 : 0;
            r.When = DateTime.UtcNow;
            Interlocked.Exchange(ref _dirty, 1);
            Version++;
        }
        Log?.Invoke($"{DateTime.UtcNow:HH:mm:ss}  regola utente: {kind} {call} → {(verdict > 0 ? "buono" : verdict < 0 ? "scarta" : "automatico")}");
    }

    public List<Item> Items() { lock (_lock) return _items.Values.OrderByDescending(i => i.Last).Select(i => i).ToList(); }
    public List<Rule> Rules() { lock (_lock) return _rules.Values.Where(r => r.Verdict != 0 || r.User).OrderBy(r => r.Kind).ThenBy(r => r.Call).ToList(); }
    public Rule RuleOf(string kind, string call) { lock (_lock) return _rules.TryGetValue(K(kind, call), out var r) ? r : null; }

    public void Prune(DateTime now)
    {
        lock (_lock)
            foreach (var k in _items.Where(kv => now - kv.Value.Last > TimeSpan.FromHours(24)).Select(kv => kv.Key).ToList()) { _items.Remove(k); Version++; }
    }

    public void Load()
    {
        try
        {
            if (!File.Exists(PathFile)) return;
            lock (_lock)
                foreach (var line in File.ReadLines(PathFile))
                {
                    if (line.StartsWith("//")) continue;
                    var p = line.Split(',');
                    if (p.Length < 7) continue;
                    var r = new Rule
                    {
                        Kind = p[0], Call = p[1], Verdict = int.Parse(p[2], CultureInfo.InvariantCulture), User = p[3] == "1",
                        Pos = int.Parse(p[4], CultureInfo.InvariantCulture), Neg = int.Parse(p[5], CultureInfo.InvariantCulture),
                        When = DateTime.TryParseExact(p[6], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out var w) ? w : DateTime.UtcNow,
                    };
                    // le regole automatiche non rinforzate da un anno si dimenticano; quelle dell'utente restano
                    if (!r.User && DateTime.UtcNow - r.When > TimeSpan.FromDays(365)) continue;
                    _rules[K(r.Kind, r.Call)] = r;
                }
        }
        catch { }
    }

    public void Save()
    {
        if (Interlocked.Exchange(ref _dirty, 0) == 0) return;
        try
        {
            List<string> lines;
            lock (_lock)
            {
                lines = new List<string> { "// TropoVHF: regole imparate. tipo,nominativo,verdetto(+1/-1/0),utente(1/0),esiti+,esiti-,data" };
                lines.AddRange(_rules.Values.OrderBy(r => r.Kind).ThenBy(r => r.Call)
                    .Select(r => $"{r.Kind},{r.Call},{r.Verdict},{(r.User ? 1 : 0)},{r.Pos},{r.Neg},{r.When:yyyy-MM-dd}"));
            }
            File.WriteAllLines(PathFile + ".tmp", lines);
            File.Move(PathFile + ".tmp", PathFile, true);
        }
        catch { }
    }
}
