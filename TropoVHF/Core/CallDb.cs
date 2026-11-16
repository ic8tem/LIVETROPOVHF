namespace TropoVHF.Core;

/// <summary>Database nominativi → locatore (CALL3.TXT di JTDX, formato CALL,LOCATORE,,,).</summary>
public sealed class CallDb
{
    readonly object _lock = new();
    Dictionary<string, string> _map;
    public bool Ready { get; private set; }
    /// <summary>File effettivamente caricato ("" se non trovato).</summary>
    public string PathUsed { get; private set; } = "";

    /// <summary>Carica il primo CALL3.TXT trovato tra i percorsi indicati.</summary>
    public void Load(IEnumerable<string> candidates)
    {
        var list = candidates.Where(p => !string.IsNullOrEmpty(p)).Distinct().ToList();
        var found = list.FirstOrDefault(File.Exists);
        if (found == null)
        {
            Error = "CALL3.TXT non trovato (cercato in: " + string.Join("; ", list.Select(Path.GetDirectoryName).Distinct()) + ")";
            lock (_lock) _map = new Dictionary<string, string>();
            Ready = true;
            return;
        }
        PathUsed = found;
        Load(found);
    }
    public string Error { get; private set; } = "";

    public int Count { get { lock (_lock) return _map?.Count ?? 0; } }

    public void Load(string path)
    {
        var m = new Dictionary<string, string>(1_100_000, StringComparer.Ordinal);
        try
        {
            if (!File.Exists(path))
            {
                Error = "CALL3.TXT non trovato";
            }
            else
            {
                foreach (var raw in File.ReadLines(path))
                {
                    if (raw.Length == 0 || raw.StartsWith("//")) continue;
                    int c1 = raw.IndexOf(',');
                    if (c1 <= 0) continue;
                    int c2 = raw.IndexOf(',', c1 + 1);
                    var call = raw.Substring(0, c1).Trim().ToUpperInvariant();
                    var loc = (c2 > 0 ? raw.Substring(c1 + 1, c2 - c1 - 1) : raw.Substring(c1 + 1)).Trim().ToUpperInvariant();
                    if (Locator.IsValid(loc)) m[call] = loc;
                }
            }
        }
        catch (Exception ex)
        {
            Error = "CALL3.TXT: " + ex.Message;
        }
        lock (_lock) _map = m;
        Ready = true;
    }

    /// <summary>
    /// Cerca solo il nominativo esatto. Prefissi e suffissi di portatile (F/IC8TEM, IC8TEM/P) non si riconducono al
    /// nominativo base: la stazione potrebbe essere altrove, quindi senza voce esatta non ha locatore.
    /// </summary>
    public string Lookup(string call)
    {
        if (string.IsNullOrEmpty(call)) return "";
        call = call.ToUpperInvariant();
        lock (_lock)
        {
            if (_manual.TryGetValue(call, out var m)) return m;
            if (_cluster.TryGetValue(call, out var k)) return k.loc;
            if (_overlay.TryGetValue(call, out var o)) return o;
            if (_map == null) return "";
            return _map.TryGetValue(call, out var l) ? l : "";
        }
    }

    // ------------------------------------------------------------ strati sopra CALL3.TXT
    //
    // Ordine di precedenza: voci manuali (Dati/CALL3-MANUALE.TXT) > correzioni dal cluster (Dati/CALL3-CLUSTER.TXT,
    // locatore a 6 caratteri dichiarato dalla stazione stessa negli spot automatici, es. DM4X) > file aggiunti
    // (Dati/CALL3-AGGIUNTE/*.TXT, in ordine alfabetico: l'ultimo vince) > CALL3.TXT. Tutto accanto al programma.

    readonly Dictionary<string, string> _overlay = new(StringComparer.Ordinal);
    readonly Dictionary<string, string> _manual = new(StringComparer.Ordinal);
    public static string ManualPath => Path.Combine(Settings.DataDir, "CALL3-MANUALE.TXT");
    public static string OverlayDir { get { var d = Path.Combine(Settings.DataDir, "CALL3-AGGIUNTE"); try { Directory.CreateDirectory(d); } catch { } return d; } }
    public List<string> OverlayFiles { get; private set; } = new();
    public int OverlayCount { get { lock (_lock) return _overlay.Count; } }

    /// <summary>
    /// Voci iniziali. Quelle mancanti si aggiungono una volta sola per ogni versione dell'elenco (riga "// seed N" nel
    /// file), così una voce tolta dall'utente non ricompare.
    /// </summary>
    static readonly (string call, string loc)[] Seed =
    {
        ("IV3/HB9CAT", "JN65UQ"), ("IC8JHY", "JN60WR"), ("I8/IZ3CGC", "JN71DB"), ("IU5SPA", "JN53GU"), ("IZ6FLS", "JN62XS"),
        ("SWL1RUCAS", "JN34OS"), ("IZ1ERR", "JN35UI"), ("IK0SRL", "JN61IT"),
    };
    const int SeedVersion = 4;

    static IEnumerable<(string call, string loc)> ReadFile(string path)
    {
        foreach (var raw in File.ReadLines(path))
        {
            if (raw.Length == 0 || raw.StartsWith("//")) continue;
            var p = raw.Split(new[] { ',', ';', '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (p.Length < 2) continue;
            var call = p[0].Trim().ToUpperInvariant();
            var loc = p[1].Trim().ToUpperInvariant();
            if (call.Length >= 3 && Locator.IsValid(loc)) yield return (call, loc);
        }
    }

    /// <summary>Carica i file aggiunti e le voci manuali (si può richiamare per ricaricare).</summary>
    public void LoadLayers()
    {
        var ov = new Dictionary<string, string>(StringComparer.Ordinal);
        var files = new List<string>();
        try
        {
            foreach (var f in Directory.GetFiles(OverlayDir).Where(f => f.EndsWith(".txt", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".csv", StringComparison.OrdinalIgnoreCase)).OrderBy(f => f, StringComparer.OrdinalIgnoreCase))
            {
                int n = 0;
                foreach (var (c, l) in ReadFile(f)) { ov[c] = l; n++; }
                files.Add($"{Path.GetFileName(f)} ({n})");
            }
        }
        catch { }
        var man = new Dictionary<string, string>(StringComparer.Ordinal);
        try
        {
            int seen = 0;
            if (File.Exists(ManualPath))
            {
                foreach (var (c, l) in ReadFile(ManualPath)) man[c] = l;
                foreach (var line in File.ReadLines(ManualPath))
                    if (line.StartsWith("// seed ") && int.TryParse(line[8..].Trim(), out var v)) seen = Math.Max(seen, v);
            }
            if (seen < SeedVersion)
            {
                foreach (var (c, l) in Seed) man.TryAdd(c, l);
                SaveManual(man);
            }
        }
        catch { }
        lock (_lock)
        {
            _overlay.Clear(); foreach (var kv in ov) _overlay[kv.Key] = kv.Value;
            _manual.Clear(); foreach (var kv in man) _manual[kv.Key] = kv.Value;
            OverlayFiles = files;
        }
    }

    // ------------------------------------------------ correzioni dal cluster

    readonly Dictionary<string, (string loc, DateTime when)> _cluster = new(StringComparer.Ordinal);
    int _clusterDirty;
    public static string ClusterPath => Path.Combine(Settings.DataDir, "CALL3-CLUSTER.TXT");
    public int ClusterCount { get { lock (_lock) return _cluster.Count; } }

    /// <summary>
    /// Locatore a 6 caratteri dichiarato dalla stazione stessa (spot automatici DM4X/RBN: "KN09tp&lt;&gt;KN09", il primo è
    /// quello di chi spotta). Se è diverso da quello del database lo sostituisce. Restituisce il locatore precedente
    /// ("" se non c'era) quando c'è stato un cambio, altrimenti null.
    /// </summary>
    public string SetCluster(string call, string loc)
    {
        call = (call ?? "").Trim().ToUpperInvariant(); loc = (loc ?? "").Trim().ToUpperInvariant();
        if (call.Length < 3 || loc.Length != 6 || !Locator.IsValid(loc)) return null;
        lock (_lock)
        {
            if (_manual.ContainsKey(call)) return null;                     // la voce a mano vince sempre
            if (_cluster.TryGetValue(call, out var c) && c.loc == loc) { if (DateTime.UtcNow - c.when > TimeSpan.FromDays(1)) { _cluster[call] = (loc, DateTime.UtcNow); _clusterDirty = 1; } return null; }
            var before = _cluster.TryGetValue(call, out var c0) ? c0.loc : _overlay.TryGetValue(call, out var o) ? o : _map != null && _map.TryGetValue(call, out var m) ? m : "";
            if (before == loc) return null;
            _cluster[call] = (loc, DateTime.UtcNow);
            _clusterDirty = 1;
            return before;
        }
    }

    public void LoadCluster()
    {
        try
        {
            if (!File.Exists(ClusterPath)) return;
            lock (_lock)
                foreach (var line in File.ReadLines(ClusterPath))
                {
                    if (line.StartsWith("//")) continue;
                    var p = line.Split(',');
                    if (p.Length < 2 || !Locator.IsValid(p[1]) || p[1].Length != 6) continue;
                    var w = p.Length > 2 && DateTime.TryParse(p[2], System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal | System.Globalization.DateTimeStyles.AssumeUniversal, out var d) ? d : DateTime.UtcNow;
                    if (DateTime.UtcNow - w > TimeSpan.FromDays(730)) continue;   // non confermato da 2 anni: si lascia cadere
                    _cluster[p[0].ToUpperInvariant()] = (p[1].ToUpperInvariant(), w);
                }
        }
        catch { }
    }

    public void SaveCluster()
    {
        if (Interlocked.Exchange(ref _clusterDirty, 0) == 0) return;
        try
        {
            List<string> lines;
            lock (_lock)
            {
                lines = new List<string> { "// TropoVHF: locatori a 6 caratteri dichiarati dalle stazioni negli spot automatici (DM4X, RBN), diversi da CALL3.TXT. Formato CALL,LOCATORE,data" };
                lines.AddRange(_cluster.OrderBy(kv => kv.Key).Select(kv => $"{kv.Key},{kv.Value.loc},{kv.Value.when:yyyy-MM-dd}"));
            }
            File.WriteAllLines(ClusterPath + ".tmp", lines);
            Compat.MoveOver(ClusterPath + ".tmp", ClusterPath);
        }
        catch { }
    }

    public List<(string call, string loc)> Manual() { lock (_lock) return _manual.OrderBy(kv => kv.Key).Select(kv => (kv.Key, kv.Value)).ToList(); }
    public string ManualOf(string call) { if (string.IsNullOrEmpty(call)) return ""; lock (_lock) return _manual.TryGetValue(call.ToUpperInvariant(), out var l) ? l : ""; }

    /// <summary>Aggiunge o cambia una voce manuale (locatore vuoto = la toglie) e salva il file.</summary>
    public bool SetManual(string call, string loc)
    {
        call = (call ?? "").Trim().ToUpperInvariant();
        loc = (loc ?? "").Trim().ToUpperInvariant();
        if (call.Length < 3 || loc != "" && !Locator.IsValid(loc)) return false;
        Dictionary<string, string> copy;
        lock (_lock)
        {
            if (loc == "") _manual.Remove(call); else _manual[call] = loc;
            copy = new Dictionary<string, string>(_manual);
        }
        SaveManual(copy);
        return true;
    }

    static void SaveManual(Dictionary<string, string> m)
    {
        try
        {
            var lines = new List<string> { "// TropoVHF: nominativi inseriti a mano. Valgono più di CALL3.TXT e dei locatori ricevuti. Formato CALL,LOCATORE", $"// seed {SeedVersion}" };
            lines.AddRange(m.OrderBy(kv => kv.Key).Select(kv => $"{kv.Key},{kv.Value}"));
            File.WriteAllLines(ManualPath + ".tmp", lines);
            Compat.MoveOver(ManualPath + ".tmp", ManualPath);
        }
        catch { }
    }

    /// <summary>Copia un file nella cartella dei file aggiunti e ricarica. Restituisce il numero di voci lette.</summary>
    public int Import(string path)
    {
        var dest = Path.Combine(OverlayDir, Path.GetFileName(path));
        if (!string.Equals(Path.GetFullPath(path), Path.GetFullPath(dest), StringComparison.OrdinalIgnoreCase)) File.Copy(path, dest, true);
        LoadLayers();
        return ReadFile(dest).Count();
    }
}
