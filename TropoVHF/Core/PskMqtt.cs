using System.Text;
using System.Text.Json;
using MQTTnet;
using MQTTnet.Client;

namespace TropoVHF.Core;

/// <summary>Un rapporto di ricezione PSKReporter.</summary>
public sealed class RxReport
{
    public DateTime Time;
    public string Band = "";      // banda del programma: 144, 222, 432; per le altre bande il nome del topic (20m, 6m…)
    public bool OtherBand;        // banda non seguita dal programma: serve solo a verificare gli ascoltatori
    public string Mode = "";
    public string TxCall = "", RxCall = "", TxGrid = "", RxGrid = "";
    public int? Snr;
    public double FreqHz;
}

/// <summary>Stazione che sta ascoltando (ricevente PSKReporter).</summary>
public sealed class Listener
{
    public string Call = "";
    public string Grid = "";
    public string Band = "";
    public DateTime Last;
    public readonly HashSet<string> Modes = new();
    public readonly Dictionary<string, (string grid, DateTime time, int? snr)> Heard = new();
    public double MaxKm;
    /// <summary>Stazioni sentite in VHF che nello stesso momento risultano attive su altre bande.</summary>
    public readonly List<string> Suspect = new();
}

/// <summary>Flusso MQTT di mqtt.pskreporter.info: chi ascolta e chi viene sentito.</summary>
public sealed class PskMqtt
{
    public string Host = "mqtt.pskreporter.info";
    public int Port = 1883;
    public bool UseTls;
    public bool Reconnect = true;
    public bool AutoStart = true;
    /// <summary>Banda del programma → nome della banda nel topic (da verificare alla prima connessione).</summary>
    public Dictionary<string, string> BandTopics = new() { ["144"] = "2m", ["222"] = "1.25m", ["432"] = "70cm" };
    public HashSet<string> EnabledBands = new() { "144", "222", "432" };

    public bool Connected { get; private set; }
    public bool Wanted { get; private set; }
    public string LastError = "";
    public long Reports;

    public event Action<string> Log;
    public event Action StateChanged;
    public event Action<RxReport> Report;

    IMqttClient _client;
    CancellationTokenSource _cts;

    // stazioni trasmittenti seguite su tutte le bande per verificare gli ascoltatori VHF
    readonly Dictionary<string, DateTime> _watched = new();
    readonly object _wlock = new();
    public int MaxWatched = 300;
    // un messaggio può arrivare due volte (sottoscrizione di banda e sottoscrizione della stazione)
    readonly Dictionary<string, DateTime> _seen = new();

    // nei topic MQTT di PSKReporter la barra dei nominativi diventa un punto (la barra separa i livelli del topic)
    static string WatchTopic(string tx) => $"pskr/filter/v2/+/+/{tx.Replace('/', '.')}/#";
    static string FromTopic(string call) => (call ?? "").Replace('.', '/').ToUpperInvariant();

    /// <summary>Segue la stazione su tutte le bande per i prossimi 30 minuti.</summary>
    public void Watch(string txCall)
    {
        if (string.IsNullOrEmpty(txCall) || txCall.IndexOfAny(new[] { '+', '#' }) >= 0) return;
        bool isNew;
        lock (_wlock)
        {
            isNew = !_watched.ContainsKey(txCall);
            if (isNew && _watched.Count >= MaxWatched) return;
            _watched[txCall] = DateTime.UtcNow;
        }
        var c = _client;
        if (isNew && c != null && Connected)
            Task.Run(async () =>
            {
                try { await c.SubscribeAsync(new MqttFactory().CreateSubscribeOptionsBuilder().WithTopicFilter(f => f.WithTopic(WatchTopic(txCall))).Build()); }
                catch { }
            });
    }

    /// <summary>Smette di seguire le stazioni non più sentite in VHF da 30 minuti.</summary>
    public void PruneWatched()
    {
        List<string> old;
        lock (_wlock)
        {
            old = _watched.Where(kv => DateTime.UtcNow - kv.Value > TimeSpan.FromMinutes(30)).Select(kv => kv.Key).ToList();
            foreach (var k in old) _watched.Remove(k);
        }
        lock (_seen) { foreach (var k in _seen.Where(kv => DateTime.UtcNow - kv.Value > TimeSpan.FromSeconds(30)).Select(kv => kv.Key).ToList()) _seen.Remove(k); }
        var c = _client;
        if (old.Count > 0 && c != null && Connected)
            Task.Run(async () =>
            {
                try
                {
                    var b = new MqttFactory().CreateUnsubscribeOptionsBuilder();
                    foreach (var t in old) b = b.WithTopicFilter(WatchTopic(t));
                    await c.UnsubscribeAsync(b.Build());
                }
                catch { }
            });
    }

    public IEnumerable<string> Topics() =>
        EnabledBands.Where(b => BandTopics.ContainsKey(b)).Select(b => $"pskr/filter/v2/{BandTopics[b]}/#");

    public void Connect()
    {
        if (Wanted) return;
        Wanted = true;
        _cts = new CancellationTokenSource();
        var ct = _cts.Token;
        Task.Run(() => Run(ct));
    }

    public void Disconnect()
    {
        Wanted = false;
        try { _cts?.Cancel(); } catch { }
        var c = _client;
        _client = null;
        if (c != null)
        {
            Task.Run(async () =>
            {
                try { await c.DisconnectAsync(); } catch { }
                try { c.Dispose(); } catch { }
            });
        }
        Connected = false;
        StateChanged?.Invoke();
    }

    async Task Run(CancellationToken ct)
    {
        var factory = new MqttFactory();
        while (!ct.IsCancellationRequested)
        {
            var client = factory.CreateMqttClient();
            _client = client;
            var closed = new TaskCompletionSource<bool>();
            client.ApplicationMessageReceivedAsync += e =>
            {
                try { Handle(e.ApplicationMessage.Topic, e.ApplicationMessage.PayloadSegment); } catch { }
                return Task.CompletedTask;
            };
            client.DisconnectedAsync += e =>
            {
                closed.TrySetResult(true);
                return Task.CompletedTask;
            };
            try
            {
                Log?.Invoke($"— connessione a {Host}:{Port}{(UseTls ? " (TLS)" : "")} —");
                var ob = new MqttClientOptionsBuilder()
                    .WithTcpServer(Host, Port)
                    .WithClientId("LIVETROPOVHF-" + Guid.NewGuid().ToString("N").Substring(0, 8))
                    .WithCleanSession();
                if (UseTls) ob = ob.WithTls();
                await client.ConnectAsync(ob.Build(), ct);
                Connected = true; LastError = "";
                StateChanged?.Invoke();
                foreach (var t in Topics())
                {
                    var so = factory.CreateSubscribeOptionsBuilder().WithTopicFilter(f => f.WithTopic(t)).Build();
                    await client.SubscribeAsync(so, ct);
                    Log?.Invoke("> sottoscritto " + t);
                }
                List<string> again;
                lock (_wlock) again = _watched.Keys.ToList();
                foreach (var tx in again)
                    await client.SubscribeAsync(factory.CreateSubscribeOptionsBuilder().WithTopicFilter(f => f.WithTopic(WatchTopic(tx))).Build(), ct);
                await Task.WhenAny(closed.Task, Task.Delay(Timeout.Infinite, ct));
            }
            catch (Exception ex)
            {
                if (!ct.IsCancellationRequested) { LastError = "errore: " + ex.Message; Log?.Invoke("— " + ex.Message + " —"); }
            }
            Connected = false;
            StateChanged?.Invoke();
            try { client.Dispose(); } catch { }
            if (ct.IsCancellationRequested || !Wanted) return;
            if (!Reconnect) { Wanted = false; StateChanged?.Invoke(); return; }
            Log?.Invoke("— connessione chiusa, nuovo tentativo tra 15 s —");
            try { await Task.Delay(15000, ct); } catch { return; }
        }
    }

    void Handle(string topic, ArraySegment<byte> payload)
    {
        // pskr/filter/v2/{band}/{mode}/{tx_call}/{rx_call}/{tx_grid}/{rx_grid}/{tx_dxcc}/{rx_dxcc}
        var p = topic.Split('/');
        if (p.Length < 9) return;
        var band = BandTopics.FirstOrDefault(kv => string.Equals(kv.Value, p[3], StringComparison.OrdinalIgnoreCase)).Key;
        var key = topic + "|" + payload.Count + "|" + (payload.Count > 0 ? Convert.ToBase64String(payload.Array, payload.Offset, Math.Min(payload.Count, 64)) : "");
        lock (_seen)
        {
            if (_seen.ContainsKey(key)) return;
            _seen[key] = DateTime.UtcNow;
        }
        var r = new RxReport
        {
            Time = Clock.UtcNow,
            Band = band ?? p[3].ToLowerInvariant(),
            OtherBand = band == null,
            Mode = p[4].ToUpperInvariant(),
            TxCall = FromTopic(p[5]),
            RxCall = FromTopic(p[6]),
            TxGrid = Grid4or6(p[7]),
            RxGrid = Grid4or6(p[8]),
        };
        if (payload.Count > 0)
        {
            try
            {
                using var doc = JsonDocument.Parse(payload.AsMemory());
                var root = doc.RootElement;
                if (root.TryGetProperty("rp", out var rp) && rp.ValueKind == JsonValueKind.Number) r.Snr = rp.GetInt32();
                if (root.TryGetProperty("f", out var f) && f.ValueKind == JsonValueKind.Number) r.FreqHz = f.GetDouble();
                // nel messaggio i nominativi sono scritti per intero, con la barra (IV3/HB9CAT)
                if (root.TryGetProperty("sc", out var sc) && sc.ValueKind == JsonValueKind.String && sc.GetString() is { Length: > 2 } scs) r.TxCall = scs.Trim().ToUpperInvariant();
                if (root.TryGetProperty("rc", out var rc) && rc.ValueKind == JsonValueKind.String && rc.GetString() is { Length: > 2 } rcs) r.RxCall = rcs.Trim().ToUpperInvariant();
            }
            catch { }
        }
        if (!r.OtherBand) Interlocked.Increment(ref Reports);
        Report?.Invoke(r);
    }

    static string Grid4or6(string g)
    {
        if (string.IsNullOrEmpty(g)) return "";
        g = g.ToUpperInvariant();
        if (g.Length >= 6 && Locator.IsValid(g.Substring(0, 6))) return g.Substring(0, 6);
        if (g.Length >= 4 && Locator.IsValid(g.Substring(0, 4))) return g.Substring(0, 4);
        return "";
    }
}

/// <summary>Elenco delle stazioni in ascolto per banda.</summary>
public sealed class ListenerStore
{
    readonly object _lock = new();
    readonly Dictionary<string, Listener> _map = new();
    // stazione trasmittente → ultime volte in cui è stata sentita su una banda non VHF (banda, ora)
    readonly Dictionary<string, List<(string band, DateTime time)>> _other = new();

    /// <summary>Minuti di tolleranza: una stazione sentita in VHF e su un'altra banda entro questo scarto è sospetta.</summary>
    public int SameTimeMinutes = 10;
    /// <summary>Quante stazioni sospette bastano per scartare un ascoltatore.</summary>
    public int SuspectThreshold = 2;

    public event Action<string> Log;
    /// <summary>
    /// Riabilitazione: la stazione è confermata su quella banda dai DX cluster umani (call, banda, ora, ascoltatore).
    /// Una stazione confermata non conta come "sentita su altre bande".
    /// </summary>
    public Func<string, string, DateTime, string, bool> Confirmed = (_, _, _, _) => false;

    /// <summary>Collegamento ascoltatore → stazione da mostrare (quarantena dei collegamenti lunghi).</summary>
    public Func<string, string, string, bool> Visible = (_, _, _) => true;
    /// <summary>Regola imparata per l'ascoltatore: +1 mai scartato, −1 sempre scartato, 0 controllo normale.</summary>
    public Func<string, int> Rule = _ => 0;
    /// <summary>Esito del controllo: (ascoltatore, banda, scartato?, motivo).</summary>
    public event Action<string, string, bool, string> Judged;

    /// <summary>Rapporto su una banda non seguita: dice solo che quella stazione trasmette lì in quel momento.</summary>
    public void AddOther(RxReport r)
    {
        if (string.IsNullOrEmpty(r.TxCall)) return;
        lock (_lock)
        {
            if (!_other.TryGetValue(r.TxCall, out var list)) _other[r.TxCall] = list = new();
            if (list.Count > 0 && list[^1].band == r.Band && (r.Time - list[^1].time).TotalSeconds <= 60) return;   // niente di nuovo
            list.Add((r.Band, r.Time));
            if (list.Count > 40) list.RemoveRange(0, list.Count - 40);
            foreach (var l in _map.Values) if (l.Heard.ContainsKey(r.TxCall)) Check(l);
        }
    }

    /// <summary>
    /// Un ascoltatore VHF non è genuino se almeno due delle stazioni che dice di sentire sono, negli stessi minuti,
    /// sentite su altre bande: con ogni probabilità il suo ricevitore è su un'altra banda e l'indicazione 144 è sbagliata.
    /// </summary>
    void Check(Listener l)
    {
        int before = l.Suspect.Count;
        l.Suspect.Clear();
        foreach (var kv in l.Heard)
            if (_other.TryGetValue(kv.Key, out var list) && list.Any(o => Math.Abs((o.time - kv.Value.time).TotalMinutes) <= SameTimeMinutes)
                && !Confirmed(kv.Key, l.Band, kv.Value.time, l.Call))     // confermata in banda dai cluster umani: non è sospetta
                l.Suspect.Add(kv.Key + " " + list.Last(o => Math.Abs((o.time - kv.Value.time).TotalMinutes) <= SameTimeMinutes).band);
        if (before < SuspectThreshold && l.Suspect.Count >= SuspectThreshold)
        {
            Log?.Invoke($"{DateTime.UtcNow:HH:mm:ss}  {l.Call} ({l.Band} MHz) scartato: {string.Join(", ", l.Suspect.Take(4))} sentiti anche su altre bande");
            Judged?.Invoke(l.Call, l.Band, true, "sente su altre bande: " + string.Join(", ", l.Suspect.Take(4)));
        }
        else if (before >= SuspectThreshold && l.Suspect.Count < SuspectThreshold)
        {
            Log?.Invoke($"{DateTime.UtcNow:HH:mm:ss}  {l.Call} ({l.Band} MHz) riabilitato: stazioni confermate in banda dai DX cluster");
            Judged?.Invoke(l.Call, l.Band, false, "riabilitato dai DX cluster umani");
        }
    }

    public bool IsGenuine(Listener l)
    {
        int r = Rule(l.Call);
        return r > 0 || r == 0 && l.Suspect.Count < SuspectThreshold;
    }

    /// <summary>Banda sotto i 144 MHz dal nome del topic (160m … 4m; 2m, 1.25m, 70cm, 23cm no).</summary>
    static bool BelowTwoMeters(string band)
    {
        var b = (band ?? "").Trim().ToLowerInvariant();
        if (!b.EndsWith("m") || b.EndsWith("cm") || b.EndsWith("mm")) return false;
        return double.TryParse(b.TrimEnd('m'), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var m) && m > 2.01;
    }

    /// <summary>La stazione è stata sentita su una banda sotto i 144 MHz entro SameTimeMinutes da quell'ora?</summary>
    public bool ActiveBelow144(string call, DateTime t)
    {
        if (string.IsNullOrEmpty(call)) return false;
        lock (_lock)
            return _other.TryGetValue(call, out var list) && list.Any(o => BelowTwoMeters(o.band) && Math.Abs((o.time - t).TotalMinutes) <= SameTimeMinutes);
    }

    /// <summary>Ascoltatori scartati nella banda e nell'intervallo, con le stazioni sentite anche su altre bande.</summary>
    public List<(string call, string grid, DateTime last, List<string> why)> Discarded(string band, int minutes, DateTime nowUtc)
    {
        var cut = nowUtc.AddMinutes(-minutes);
        lock (_lock)
            return _map.Values.Where(l => l.Band == band && l.Last > cut && !IsGenuine(l))
                .OrderByDescending(l => l.Last).Select(l => (l.Call, l.Grid, l.Last, l.Suspect.ToList())).ToList();
    }

    public void Add(RxReport r)
    {
        if (string.IsNullOrEmpty(r.RxCall)) return;
        lock (_lock)
        {
            var key = r.Band + "|" + r.RxCall;
            if (!_map.TryGetValue(key, out var l)) { l = new Listener { Call = r.RxCall, Band = r.Band }; _map[key] = l; }
            if (r.RxGrid != "") l.Grid = r.RxGrid;
            l.Last = r.Time;
            if (r.Mode != "") l.Modes.Add(r.Mode);
            if (r.TxCall != "")
            {
                l.Heard[r.TxCall] = (r.TxGrid, r.Time, r.Snr);
                if (Locator.TryToLatLon(l.Grid, out var a) && Locator.TryToLatLon(r.TxGrid, out var b))
                    l.MaxKm = Math.Max(l.MaxKm, Geo.DistKm(a, b));
                Check(l);
            }
        }
    }

    public List<Listener> Get(string band, int minutes, DateTime nowUtc)
    {
        var cut = nowUtc.AddMinutes(-minutes);
        List<Listener> copies;
        lock (_lock)
        {
            // copie: chi disegna non deve vedere le modifiche del thread di rete
            copies = _map.Values.Where(l => l.Band == band && l.Last > cut && l.Grid != "" && IsGenuine(l)).Select(l =>
            {
                // locatori a 6 caratteri da CALL3.TXT quando quelli ricevuti sono più corti
                var c = new Listener { Call = l.Call, Grid = AppState.Refine(l.Call, l.Grid), Band = l.Band, Last = l.Last, MaxKm = l.MaxKm };
                foreach (var m in l.Modes) c.Modes.Add(m);
                foreach (var kv in l.Heard) c.Heard[kv.Key] = (AppState.Refine(kv.Key, kv.Value.grid), kv.Value.time, kv.Value.snr);
                return c;
            }).ToList();
        }
        // quarantena dei collegamenti lunghi fuori dal lock: la quarantena può chiedere a questo elenco (niente blocchi incrociati)
        foreach (var c in copies)
            foreach (var k in c.Heard.Keys.ToList())
                if (!Visible(c.Band, c.Call, k)) c.Heard.Remove(k);
        return copies;
    }

    public void Prune(DateTime nowUtc)
    {
        var cut = nowUtc.AddMinutes(-60);
        lock (_lock)
        {
            foreach (var k in _map.Where(kv => kv.Value.Last < cut).Select(kv => kv.Key).ToList()) _map.Remove(k);
            foreach (var l in _map.Values)
                foreach (var h in l.Heard.Where(kv => kv.Value.time < cut).Select(kv => kv.Key).ToList()) l.Heard.Remove(h);
            foreach (var k in _other.Where(kv => kv.Value.Count == 0 || kv.Value[^1].time < cut).Select(kv => kv.Key).ToList()) _other.Remove(k);
            foreach (var l in _map.Values) Check(l);
        }
    }
}
