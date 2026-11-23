using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace TropoVHF.Core;

/// <summary>Una ricezione lontana da un sensore (AIS o FM-DX), per la mappa e la barra di stato.</summary>
public sealed class SensorHit
{
    public DateTime T;
    public string Kind = "";            // "AIS" o "FMDX"
    public string Rx = "", Tx = "";     // ricevitore e trasmettitore (nome, MMSI, città)
    public LatLon PRx, PTx;
    public double Km, Ratio = double.NaN, Sig = double.NaN, FreqMHz;
}

/// <summary>
/// 1.4 — AIS dal ricevitore dell'utente (AIS-catcher, rtl-ais, un ricevitore AIS con uscita NMEA in rete): frasi
/// !AIVDM in UDP sulla porta scelta (10110 di solito). Si decodificano le posizioni dei tipi 1, 2, 3 (classe A),
/// 4 (stazioni base a terra), 18 e 19 (classe B) e 21 (radiofari AtoN); il tipo 27 (satellite) si scarta.
/// A 162 MHz l'orizzonte radio normale fra un'antenna di nave e la propria è 4,12·(√h1 + √h2) km: una nave sentita
/// molto oltre, sul mare, è il segno più diretto di un condotto (idea di AISTropoAlert, J. Luszcz, Apache 2.0).
/// Si annotano le ricezioni oltre RatioMin volte l'orizzonte; il programma non manda nulla a nessuno.
/// </summary>
public sealed class AisReceiver
{
    public int Port = 10110;
    public double RxAntennaM = 10, RatioMin = 1.5, MinKm = 50;
    public bool Running { get; private set; }
    public long Sentences, Positions;
    public string LastError = "";
    // 1.4 — flusso aperto delle stazioni base costiere norvegesi (Kystverket, licenza NLOD): solo ricezione terrestre
    public string NetHost = "153.44.253.27"; public int NetPort = 5631;
    public double BaseAntennaM = 300, NetMinKm = 150;
    public bool NetRunning { get; private set; }
    public long NetSentences, NetPositions; public string NetError = "";
    readonly Dictionary<long, LatLon> _bases = new();
    CancellationTokenSource _netCts; TcpClient _tcp;
    public int Bases { get { lock (_bases) return _bases.Count; } }
    public Func<LatLon?> Qth = () => null;
    public event Action<SensorHit> Hit;
    readonly Dictionary<long, SensorHit> _last = new();
    readonly Dictionary<string, string[]> _frag = new();
    CancellationTokenSource _cts;
    UdpClient _udp;

    public void Start()
    {
        Stop();
        _cts = new CancellationTokenSource();
        var ct = _cts.Token;
        Task.Run(() => Loop(ct));
    }

    public void Stop()
    {
        try { _cts?.Cancel(); } catch { }
        try { _udp?.Close(); } catch { }
        _cts = null; _udp = null; Running = false;
    }

    public void StartNet()
    {
        StopNet();
        _netCts = new CancellationTokenSource();
        var ct = _netCts.Token;
        Task.Run(() => NetLoop(ct));
    }

    public void StopNet()
    {
        try { _netCts?.Cancel(); } catch { }
        try { _tcp?.Close(); } catch { }
        _netCts = null; _tcp = null; NetRunning = false;
    }

    async Task NetLoop(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try
            {
                _tcp = new TcpClient();
                var conn = _tcp.ConnectAsync(NetHost, NetPort);
                if (await Task.WhenAny(conn, Task.Delay(15000, ct)) != conn) throw new TimeoutException("Kystverket: nessuna risposta");
                await conn;
                using var rd = new StreamReader(_tcp.GetStream(), Encoding.ASCII);
                using (ct.Register(() => { try { _tcp?.Close(); } catch { } }))
                {
                    NetRunning = true; NetError = "";
                    string line;
                    while (!ct.IsCancellationRequested && (line = await rd.ReadLineAsync()) != null)
                        try { HandleLine(line.Trim(), Clock.UtcNow, true); } catch { }
                }
            }
            catch (Exception ex) when (!ct.IsCancellationRequested) { NetError = ex.Message; }
            catch { }
            NetRunning = false;
            try { _tcp?.Close(); } catch { }
            try { await Task.Delay(30000, ct); } catch { break; }
        }
        NetRunning = false;
    }

    async Task Loop(CancellationToken ct)
    {
        try
        {
            _udp = new UdpClient(new IPEndPoint(IPAddress.Any, Port));
            Running = true; LastError = "";
            while (!ct.IsCancellationRequested)
            {
                var r = await Compat.Receive(_udp, ct);
                foreach (var line in Encoding.ASCII.GetString(r.Buffer).Split('\n'))
                    try { HandleLine(line.Trim(), Clock.UtcNow); } catch { }
            }
        }
        catch (OperationCanceledException) { }
        catch (ObjectDisposedException) { }
        catch (Exception ex) { LastError = ex.Message; }
        Running = false;
    }

    /// <summary>Ricezioni oltre l'orizzonte dell'ultima ora (una per nave, la più recente).</summary>
    public List<SensorHit> Recent(DateTime now, int minutes = 60)
    {
        lock (_last) return _last.Values.Where(h => (now - h.T).TotalMinutes <= minutes && h.Ratio >= RatioMin && h.Km >= (h.Rx == "QTH" ? MinKm : NetMinKm)).OrderByDescending(h => h.Km).ToList();
    }

    /// <summary>Navi viste nell'ultima ora (tutte, anche entro l'orizzonte) e la più lontana.</summary>
    public (int ships, double maxKm) Stats(DateTime now)
    {
        lock (_last)
        {
            var v = _last.Values.Where(h => (now - h.T).TotalMinutes <= 60).ToList();
            return (v.Count, v.Count == 0 ? 0 : v.Max(h => h.Km));
        }
    }

    public void HandleLine(string line, DateTime now, bool net = false)
    {
        // blocco di tag IEC 61162-450 davanti alla frase: \s:stazione,c:tempo*hh\!AIVDM...
        long src = 0;
        if (line.StartsWith("\\"))
        {
            int e = line.IndexOf('\\', 1); if (e < 0) return;
            foreach (var t in line.Substring(1, e - 1).Split('*')[0].Split(','))
                if (t.StartsWith("s:")) { var d = new string(t.Substring(2).Where(char.IsDigit).ToArray()); if (d.Length > 0 && d.Length <= 9) long.TryParse(d, out src); }
            line = line.Substring(e + 1);
        }
        if (!(line.StartsWith("!AIVDM") || line.StartsWith("!BSVDM") || line.StartsWith("!ABVDM") || line.StartsWith("!BSVDO"))) return;
        int star = line.IndexOf('*'); if (star > 0) line = line.Substring(0, star);
        var f = line.Split(',');
        if (f.Length < 7) return;
        if (net) NetSentences++; else Sentences++;
        int count = int.TryParse(f[1], out var c1) ? c1 : 1, num = int.TryParse(f[2], out var n1) ? n1 : 1;
        string payload = f[5];
        if (count > 1)
        {
            string key = (net ? "n" : "u") + src + "/" + f[3] + "/" + f[4];
            lock (_frag)
            {
                if (!_frag.TryGetValue(key, out var parts) || parts.Length != count) _frag[key] = parts = new string[count];
                if (num < 1 || num > count) return;
                parts[num - 1] = payload;
                if (parts.Any(p => p == null)) return;
                payload = string.Concat(parts); _frag.Remove(key);
            }
        }
        Decode(payload, now, net, src);
    }

    void Decode(string payload, DateTime now, bool net = false, long src = 0)
    {
        var bits = new List<bool>(payload.Length * 6);
        foreach (char ch in payload)
        {
            int v = ch - 48; if (v > 40) v -= 8;
            for (int b = 5; b >= 0; b--) bits.Add(((v >> b) & 1) == 1);
        }
        long U(int start, int len) { long x = 0; for (int i = 0; i < len; i++) x = (x << 1) | (start + i < bits.Count && bits[start + i] ? 1L : 0L); return x; }
        long S(int start, int len) { long x = U(start, len); if (len > 0 && (x & (1L << (len - 1))) != 0) x -= 1L << len; return x; }
        if (bits.Count < 38) return;
        int type = (int)U(0, 6); long mmsi = U(8, 30);
        if (U(6, 2) != 0) return;                              // ritrasmesso da un ripetitore: non è una ricezione diretta
        int lonAt, latAt; double hShip;
        switch (type)
        {
            case 1: case 2: case 3: lonAt = 61; latAt = 89; hShip = 20; break;
            case 4: lonAt = 79; latAt = 107; hShip = 30; break;
            case 18: case 19: lonAt = 57; latAt = 85; hShip = 6; break;
            case 21: lonAt = 164; latAt = 192; hShip = 10; break;
            default: return;                                   // 27 (satellite) e i messaggi senza posizione
        }
        if (bits.Count < latAt + 27) return;
        double lon = S(lonAt, 28) / 600000.0, lat = S(latAt, 27) / 600000.0;
        if (Math.Abs(lon) > 180 || Math.Abs(lat) > 90 || (lat == 0 && lon == 0)) return;
        var p = new LatLon(lat, lon);
        // stazioni base (MMSI 00MIDxxxx): la loro posizione fa da ricevitore per il flusso di rete
        if (type == 4 && mmsi < 10_000_000) lock (_bases) _bases[mmsi] = p;
        SensorHit h;
        if (!net)
        {
            Positions++;
            var q = Qth(); if (q == null) return;
            double km = Geo.DistKm(q.Value, p);
            double hor = 4.12 * (Math.Sqrt(hShip) + Math.Sqrt(Math.Max(1, RxAntennaM)));
            h = new SensorHit { T = now, Kind = "AIS", Rx = "QTH", Tx = (type == 4 ? "base " : type == 21 ? "AtoN " : "") + mmsi, PRx = q.Value, PTx = p, Km = km, Ratio = km / hor, FreqMHz = 162 };
        }
        else
        {
            NetPositions++;
            if (type == 4) return;                             // le stazioni base fra loro non dicono nulla
            // stazione ricevente: quella del blocco di tag se è una base nota; altrimenti la base più vicina, che dà
            // un limite inferiore sicuro (chi ha ricevuto la nave è lontano almeno quanto la base più vicina)
            LatLon rx = default; long rxId = 0; double km = double.MaxValue;
            lock (_bases)
            {
                if (src != 0 && _bases.TryGetValue(src, out var b)) { rx = b; rxId = src; km = Geo.DistKm(b, p); }
                else foreach (var kv in _bases) { double d = Geo.DistKm(kv.Value, p); if (d < km) { km = d; rx = kv.Value; rxId = kv.Key; } }
            }
            if (rxId == 0) return;
            double hor = 4.12 * (Math.Sqrt(hShip) + Math.Sqrt(BaseAntennaM));
            h = new SensorHit { T = now, Kind = "AIS", Rx = "base " + rxId.ToString("D9"), Tx = (type == 21 ? "AtoN " : "") + mmsi, PRx = rx, PTx = p, Km = km, Ratio = km / hor, FreqMHz = 162 };
        }
        bool beyond = h.Ratio >= RatioMin && h.Km >= (net ? NetMinKm : MinKm);
        long key = net ? -mmsi : mmsi;
        lock (_last)
        {
            bool was = _last.TryGetValue(key, out var old) && old.Ratio >= RatioMin && (now - old.T).TotalMinutes < 30;
            _last[key] = h;
            if (_last.Count > 5000) foreach (var k in _last.Where(x => (now - x.Value.T).TotalHours > 2).Select(x => x.Key).ToList()) _last.Remove(k);
            if (beyond && !was) Hit?.Invoke(h);
        }
    }
}

/// <summary>
/// 1.4 — Rete dei webserver FM-DX pubblici (servers.fmdx.org, progetto open source GPL-3.0 di M. Farkaš): ogni
/// webserver risponde a /api con la frequenza sintonizzata, il segnale (dBf) e il trasmettitore riconosciuto dal PI
/// RDS (txInfo: nome, città, distanza, azimut, dalla banca dati di maps.fmdx.org). Una stazione FM sentita a centinaia
/// di km in banda III-bassa (88–108 MHz) è un'osservazione di tropo vicina ai 2 m. Si interrogano soltanto i webserver
/// più vicini (MaxServers entro RadiusKm dal QTH), uno ogni pochi secondi, così ognuno riceve una richiesta ogni
/// tre minuti circa: lo stesso carico dell'«FM-DX Monitoring» della comunità. Si legge l'API pubblica, non le pagine.
/// </summary>
public sealed class FmDxNet
{
    public string ListUrl = "https://servers.fmdx.org/api/";
    public int MaxServers = 60, PollSeconds = 180;
    public double RadiusKm = 1500, MinKm = 150, MinSig = 10;
    public bool Running { get; private set; }
    public int Servers; public long Polls, Answers;
    public string LastError = "";
    public Func<LatLon?> Qth = () => null;
    public event Action<SensorHit> Hit;
    sealed class Srv { public string Name = "", Url = ""; public LatLon P; }
    readonly List<SensorHit> _hits = new();
    readonly Dictionary<string, DateTime> _seen = new();
    CancellationTokenSource _cts;
    static readonly HttpClient Http = MakeHttp();

    static HttpClient MakeHttp()
    {
        var h = new HttpClient { Timeout = TimeSpan.FromSeconds(8) };
        try { h.DefaultRequestHeaders.UserAgent.ParseAdd("LIVETROPOVHF/1.4 (+https://github.com/ic8tem/LIVETROPOVHF)"); } catch { }
        return h;
    }

    public void Start() { Stop(); _cts = new CancellationTokenSource(); var ct = _cts.Token; Task.Run(() => Loop(ct)); }
    public void Stop() { try { _cts?.Cancel(); } catch { } _cts = null; Running = false; }

    public List<SensorHit> Recent(DateTime now, int minutes = 60)
    {
        lock (_hits) return _hits.Where(h => (now - h.T).TotalMinutes <= minutes).OrderByDescending(h => h.Km).ToList();
    }

    async Task Loop(CancellationToken ct)
    {
        var list = new List<Srv>(); DateTime listAt = DateTime.MinValue; int idx = 0;
        Running = true;
        while (!ct.IsCancellationRequested)
        {
            try
            {
                if ((DateTime.UtcNow - listAt).TotalMinutes > 30 || list.Count == 0)
                {
                    list = await LoadList(ct); listAt = DateTime.UtcNow; idx = 0; Servers = list.Count; LastError = "";
                }
                if (list.Count == 0) { await Task.Delay(TimeSpan.FromMinutes(5), ct); continue; }
                var s = list[idx++ % list.Count];
                _ = PollOne(s, ct);
                await Task.Delay(TimeSpan.FromSeconds(Math.Max(2.0, PollSeconds / (double)list.Count)), ct);
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex) { LastError = ex.Message; try { await Task.Delay(TimeSpan.FromMinutes(2), ct); } catch { break; } }
        }
        Running = false;
    }

    async Task<List<Srv>> LoadList(CancellationToken ct)
    {
        var q = Qth();
        using var resp = await Http.GetAsync(ListUrl, ct);
        resp.EnsureSuccessStatusCode();
        var json = await Compat.ReadString(resp.Content, ct);
        using var doc = JsonDocument.Parse(json);
        var res = new List<Srv>();
        if (!doc.RootElement.TryGetProperty("dataset", out var ds)) return res;
        foreach (var e in ds.EnumerateArray())
        {
            try
            {
                if (e.TryGetProperty("status", out var st) && st.ValueKind == JsonValueKind.Number && st.GetInt32() != 1) continue;
                var c = e.GetProperty("coords");
                double la = double.Parse(c[0].GetString() ?? c[0].ToString(), CultureInfo.InvariantCulture), lo = double.Parse(c[1].GetString() ?? c[1].ToString(), CultureInfo.InvariantCulture);
                string url = e.GetProperty("url").GetString() ?? "";
                if (!url.StartsWith("http")) continue;
                res.Add(new Srv { Name = (e.TryGetProperty("city", out var ci) ? ci.GetString() : "") ?? "", Url = url.TrimEnd('/'), P = new LatLon(la, lo) });
            }
            catch { }
        }
        if (q != null) res = res.Where(s => Geo.DistKm(q.Value, s.P) <= RadiusKm).OrderBy(s => Geo.DistKm(q.Value, s.P)).ToList();
        return res.Take(MaxServers).ToList();
    }

    async Task PollOne(Srv s, CancellationToken ct)
    {
        try
        {
            Polls++;
            using var resp = await Http.GetAsync(s.Url + "/api", ct);
            if (!resp.IsSuccessStatusCode) return;
            var json = await Compat.ReadString(resp.Content, ct);
            using var doc = JsonDocument.Parse(json);
            var r = doc.RootElement;
            Answers++;
            if (!r.TryGetProperty("txInfo", out var tx) || tx.ValueKind != JsonValueKind.Object) return;
            double Num(JsonElement e, string k) { if (!e.TryGetProperty(k, out var v)) return double.NaN; return v.ValueKind == JsonValueKind.Number ? v.GetDouble() : double.TryParse(v.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out var d) ? d : double.NaN; }
            double km = Num(tx, "dist"), az = Num(tx, "azi"), sig = Num(r, "sig"), fq = Num(r, "freq");
            if (double.IsNaN(km) || double.IsNaN(az) || km < MinKm || double.IsNaN(sig) || sig < MinSig) return;
            string name = (tx.TryGetProperty("tx", out var tn) ? tn.ToString() : "") + (tx.TryGetProperty("city", out var tc) ? " (" + tc + ")" : "");
            var now = Clock.UtcNow;
            string key = s.Url + "|" + name;
            lock (_seen) { if (_seen.TryGetValue(key, out var t0) && (now - t0).TotalMinutes < 2) return; _seen[key] = now; }
            var h = new SensorHit { T = now, Kind = "FMDX", Rx = "FM-DX " + s.Name, Tx = name, PRx = s.P, PTx = Geo.Destination(s.P, az, km), Km = km, Sig = sig, FreqMHz = fq };
            lock (_hits) { _hits.Add(h); _hits.RemoveAll(x => (now - x.T).TotalMinutes > 120); }
            Hit?.Invoke(h);
        }
        catch { }
    }
}
