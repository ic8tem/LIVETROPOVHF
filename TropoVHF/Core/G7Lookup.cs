using System.Collections.Concurrent;
using System.Globalization;
using System.Text.Json;

namespace TropoVHF.Core;

/// <summary>
/// Ultima fonte di locatori: l'API pubblica di G7VRD "locator by callsign"
/// (https://api.g7vrd.co.uk/v1/locators/by-callsign/CALL1,CALL2). Si interroga solo per i nominativi che, dopo voci a
/// mano, correzioni dal cluster, file aggiunti, CALL3.TXT e LOC6, non hanno ancora un locatore a 6 caratteri.
/// Si accetta solo un locatore a 6 caratteri valido, non il centro del quadrato (LL/MM). Le richieste sono raggruppate
/// (al massimo 40 nominativi al minuto) e ogni nominativo si chiede una volta ogni 24 ore: è un servizio hobbistico.
/// Risultati in Dati/CALL3-G7VRD.TXT accanto al programma.
/// </summary>
public static class G7Lookup
{
    public const string Api = "https://api.g7vrd.co.uk/v1/locators/by-callsign/";
    public static bool Enabled = true;
    static readonly ConcurrentDictionary<string, (string loc, DateTime when)> _found = new();
    static readonly ConcurrentDictionary<string, DateTime> _asked = new();
    static readonly ConcurrentQueue<string> _queue = new();
    static int _dirty;
    public static int Count => _found.Count;
    public static string LastStatus = "";
    static string PathFile => Path.Combine(Settings.DataDir, "CALL3-G7VRD.TXT");

    public static string Get(string call) => call != null && _found.TryGetValue(call, out var v) ? v.loc : "";

    /// <summary>Mette in coda il nominativo, se non è già stato chiesto nelle ultime 24 ore.</summary>
    public static void Request(string call)
    {
        if (!Enabled || string.IsNullOrEmpty(call) || call.Length < 3 || call.Contains('/') || call.Contains('-')) return;
        var now = DateTime.UtcNow;
        if (_asked.TryGetValue(call, out var t) && now - t < TimeSpan.FromHours(24)) return;
        _asked[call] = now;
        _queue.Enqueue(call);
    }

    public static void Load()
    {
        try
        {
            if (!File.Exists(PathFile)) return;
            foreach (var line in File.ReadLines(PathFile))
            {
                if (line.StartsWith("//")) continue;
                var p = line.Split(',');
                if (p.Length < 2 || p[1].Length != 6 || !Locator.IsValid(p[1])) continue;
                var w = p.Length > 2 && DateTime.TryParse(p[2], CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out var d) ? d : DateTime.UtcNow;
                if (DateTime.UtcNow - w > TimeSpan.FromDays(365)) continue;
                _found[p[0].ToUpperInvariant()] = (p[1].ToUpperInvariant(), w);
            }
        }
        catch { }
    }

    public static void Save()
    {
        if (Interlocked.Exchange(ref _dirty, 0) == 0) return;
        try
        {
            var lines = new List<string> { "// LIVETROPOVHF: locatori a 6 caratteri da api.g7vrd.co.uk (dati raccolti in automatico, ultima fonte). CALL,LOCATORE,data" };
            lines.AddRange(_found.OrderBy(kv => kv.Key).Select(kv => $"{kv.Key},{kv.Value.loc},{kv.Value.when:yyyy-MM-dd}"));
            File.WriteAllLines(PathFile + ".tmp", lines);
            File.Move(PathFile + ".tmp", PathFile, true);
        }
        catch { }
    }

    /// <summary>Ciclo in sottofondo: un gruppo di nominativi al minuto.</summary>
    public static async Task RunAsync(HttpClient http, Action changed, CancellationToken ct = default)
    {
        while (!ct.IsCancellationRequested)
        {
            try { await Task.Delay(TimeSpan.FromSeconds(60), ct); } catch { return; }
            if (!Enabled || _queue.IsEmpty) continue;
            var batch = new List<string>();
            while (batch.Count < 40 && _queue.TryDequeue(out var c)) batch.Add(c);
            try
            {
                var url = Api + string.Join(",", batch.Select(Uri.EscapeDataString));
                using var resp = await http.GetAsync(url, ct);
                if (!resp.IsSuccessStatusCode) { LastStatus = $"G7VRD: HTTP {(int)resp.StatusCode}"; continue; }
                using var doc = JsonDocument.Parse(await resp.Content.ReadAsStringAsync(ct));
                int n = 0;
                if (doc.RootElement.ValueKind == JsonValueKind.Object)
                    foreach (var prop in doc.RootElement.EnumerateObject())
                    {
                        var loc = Pick(prop.Value);
                        if (loc == "") continue;
                        _found[prop.Name.ToUpperInvariant()] = (loc, DateTime.UtcNow);
                        n++;
                    }
                if (n > 0) { Interlocked.Exchange(ref _dirty, 1); changed?.Invoke(); }
                LastStatus = $"G7VRD: {batch.Count} chiesti, {n} locatori a 6 caratteri";
            }
            catch (Exception ex) { LastStatus = "G7VRD: " + ex.Message; }
        }
    }

    /// <summary>Il valore può essere una stringa, un elenco o un oggetto: si prende il primo locatore a 6 caratteri utile.</summary>
    static string Pick(JsonElement v)
    {
        switch (v.ValueKind)
        {
            case JsonValueKind.String:
                var s = (v.GetString() ?? "").Trim().ToUpperInvariant();
                return s.Length == 6 && Locator.IsValid(s) && !s.EndsWith("LL") && !s.EndsWith("MM") ? s : "";
            case JsonValueKind.Array:
                foreach (var e in v.EnumerateArray()) { var r = Pick(e); if (r != "") return r; }
                return "";
            case JsonValueKind.Object:
                foreach (var name in new[] { "locator", "grid", "maidenhead", "value" })
                    if (v.TryGetProperty(name, out var x)) { var r = Pick(x); if (r != "") return r; }
                foreach (var p in v.EnumerateObject()) { var r = Pick(p.Value); if (r != "") return r; }
                return "";
            default: return "";
        }
    }
}
