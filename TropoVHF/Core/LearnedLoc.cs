using System.Collections.Concurrent;
using System.Globalization;

namespace TropoVHF.Core;

/// <summary>
/// Locatori a 6 caratteri imparati dal vivo. Quando un nominativo che non è in CALL3.TXT (o che lì ha solo 4 caratteri)
/// arriva da una qualsiasi fonte con un locatore a 6 caratteri (PSKReporter come trasmettitore o ricevitore, cluster,
/// decodifiche UDP), il locatore viene ricordato e usato per tutti gli altri spot dello stesso nominativo che portano
/// solo 4 caratteri. Il file Dati/LOC6.TXT (formato CALL3: CALL,LOCATORE,data) sta accanto al programma, niente AppData.
/// Si tiene il più recente; le voci più vecchie di 2 anni non vengono ricaricate.
/// </summary>
public static class LearnedLoc
{
    static readonly ConcurrentDictionary<string, (string grid, DateTime when)> _map = new();
    static int _dirty;
    static string PathFile => Path.Combine(Settings.DataDir, "LOC6.TXT");
    public static int Count => _map.Count;

    public static void Load()
    {
        try
        {
            if (!File.Exists(PathFile)) return;
            foreach (var line in File.ReadLines(PathFile))
            {
                var p = line.Split(',');
                if (p.Length < 2 || line.StartsWith("//")) continue;
                var when = p.Length > 2 && DateTime.TryParseExact(p[2], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out var w) ? w : DateTime.UtcNow;
                if (DateTime.UtcNow - when > TimeSpan.FromDays(730)) continue;
                var g = p[1].Trim().ToUpperInvariant();
                if (g.Length == 6 && Locator.IsValid(g)) _map[p[0].Trim().ToUpperInvariant()] = (g, when);
            }
        }
        catch { }
    }

    public static void Learn(string call, string grid)
    {
        if (string.IsNullOrEmpty(call) || grid == null || grid.Length < 6) return;
        var g = grid[..6].ToUpperInvariant();
        if (!Locator.IsValid(g)) return;
        var c = call.Trim().ToUpperInvariant().Trim('<', '>');
        if (c.Length < 3 || c == "GRAVES") return;
        if (_map.TryGetValue(c, out var old) && old.grid == g && DateTime.UtcNow - old.when < TimeSpan.FromDays(1)) return;
        _map[c] = (g, DateTime.UtcNow);
        Interlocked.Exchange(ref _dirty, 1);
    }

    public static string Get(string call) => _map.TryGetValue(call, out var v) ? v.grid : "";

    public static void Save()
    {
        if (Interlocked.Exchange(ref _dirty, 0) == 0) return;
        try
        {
            var lines = new List<string> { "// TropoVHF: locatori a 6 caratteri imparati dal vivo (PSKReporter, cluster, UDP)" };
            lines.AddRange(_map.OrderBy(kv => kv.Key).Select(kv => $"{kv.Key},{kv.Value.grid},{kv.Value.when:yyyy-MM-dd}"));
            File.WriteAllLines(PathFile + ".tmp", lines);
            Compat.MoveOver(PathFile + ".tmp", PathFile);
        }
        catch { }
    }
}
