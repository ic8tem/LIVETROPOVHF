using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TropoVHF.Core;

/// <summary>Una stazione decodificata dal programma digitale dell'utente.</summary>
public sealed class Decode
{
    public DateTime Time;
    public string Band = "";
    public string Mode = "";      // modo dichiarato nello Status (es. FT8, MSK144)
    public string TxMode = "";    // carattere di modo della decodifica (~ FT8, + FT4, & MSK144…)
    public int Snr;
    public double Dt;
    public uint Df;
    public string Message = "";
    public string Call = "";
    public string Grid = "";
}

/// <summary>
/// Ascolta i pacchetti UDP di WSJT-X / JTDX / MSHV, li ripete identici sulla porta di inoltro
/// e ne ricava le stazioni decodificate.
/// </summary>
public sealed class WsjtxUdp
{
    public string ListenAddress = "127.0.0.1";
    public int ListenPort = 2237;
    public string ForwardAddress = "127.0.0.1";
    public int ForwardPort = 2238;
    public bool Forward = true;
    public bool AutoStart = true;

    public bool Running { get; private set; }
    public string LastError = "";
    public string ProgramId = "";
    public string ProgramVersion = "";
    public double DialHz;
    public string DialMode = "";
    public long Packets, Forwarded, Decodes;

    public event Action<string, bool> Debug;   // testo, è una decodifica
    public event Action StateChanged;
    public event Action<Decode> Decoded;
    /// <summary>Dallo Status: la stazione con cui si sta lavorando (DX Call), trasmissione abilitata, ora dell'ultimo Status.</summary>
    public string DxCall { get; private set; } = "";
    public bool TxEnabled { get; private set; }
    public DateTime StatusTime { get; private set; }

    UdpClient _rx, _tx;
    CancellationTokenSource _cts;

    public string CurrentBand => DialHz > 0 ? Bands.FromKHz(DialHz / 1000.0) : "";

    public void Start()
    {
        Stop();
        try
        {
            var listenIp = IPAddress.Parse(ListenAddress.Trim());
            bool multicast = listenIp.AddressFamily == AddressFamily.InterNetwork && (listenIp.GetAddressBytes()[0] & 0xF0) == 0xE0;
            _rx = new UdpClient(AddressFamily.InterNetwork);
            _rx.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            _rx.Client.Bind(new IPEndPoint(multicast ? IPAddress.Any : listenIp, ListenPort));
            if (multicast) _rx.JoinMulticastGroup(listenIp);
            _tx = new UdpClient(AddressFamily.InterNetwork);
            _cts = new CancellationTokenSource();
            var ct = _cts.Token;
            Running = true; LastError = "";
            StateChanged?.Invoke();
            Task.Run(() => Loop(ct));
        }
        catch (Exception ex)
        {
            LastError = ex.Message;
            Running = false;
            StateChanged?.Invoke();
        }
    }

    public void Stop()
    {
        try { _cts?.Cancel(); } catch { }
        try { _rx?.Close(); } catch { }
        try { _tx?.Close(); } catch { }
        _rx = null; _tx = null;
        if (Running) { Running = false; StateChanged?.Invoke(); }
    }

    async Task Loop(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            UdpReceiveResult res;
            try { res = await _rx.ReceiveAsync(ct); }
            catch { break; }
            Interlocked.Increment(ref Packets);
            var data = res.Buffer;
            if (Forward && _tx != null)
            {
                try
                {
                    await _tx.SendAsync(data, data.Length, ForwardAddress.Trim(), ForwardPort);
                    Interlocked.Increment(ref Forwarded);
                }
                catch { }
            }
            try { Parse(data); }
            catch (Exception ex) { Debug?.Invoke($"{Clock.UtcNow:HH:mm:ss.fff}  pacchetto non leggibile: {ex.Message}", false); }
        }
        Running = false;
        StateChanged?.Invoke();
    }

    // ------------------------------------------------ protocollo (QDataStream, big-endian)

    sealed class Reader
    {
        readonly byte[] _b; int _p;
        public Reader(byte[] b) { _b = b; }
        public bool Eof => _p >= _b.Length;
        public uint U32() { uint v = (uint)(_b[_p] << 24 | _b[_p + 1] << 16 | _b[_p + 2] << 8 | _b[_p + 3]); _p += 4; return v; }
        public int I32() => (int)U32();
        public ulong U64() { ulong hi = U32(); ulong lo = U32(); return hi << 32 | lo; }
        public bool Bool() => _b[_p++] != 0;
        public double F64()
        {
            var t = new byte[8];
            Array.Copy(_b, _p, t, 0, 8); _p += 8;
            if (BitConverter.IsLittleEndian) Array.Reverse(t);
            return BitConverter.ToDouble(t, 0);
        }
        public string Utf8()
        {
            uint n = U32();
            if (n == 0xFFFFFFFF) return "";
            var s = Encoding.UTF8.GetString(_b, _p, (int)n); _p += (int)n; return s;
        }
    }

    void Parse(byte[] data)
    {
        var r = new Reader(data);
        if (data.Length < 12 || r.U32() != 0xADBCCBDA) { Debug?.Invoke($"{Clock.UtcNow:HH:mm:ss.fff}  pacchetto sconosciuto ({data.Length} byte)", false); return; }
        uint schema = r.U32();
        uint type = r.U32();
        string id = r.Utf8();
        string now = Clock.UtcNow.ToString("HH:mm:ss.fff");
        switch (type)
        {
            case 0: // Heartbeat
            {
                uint maxSchema = r.U32();
                string ver = r.Eof ? "" : r.Utf8();
                ProgramId = id; ProgramVersion = ver;
                Debug?.Invoke($"{now}  Heartbeat   id={id}  schema {schema}  versione {ver}", false);
                StateChanged?.Invoke();
                break;
            }
            case 1: // Status
            {
                ulong dial = r.U64();
                string mode = r.Utf8();
                string dxCall = r.Utf8();
                string report = r.Utf8();
                string txMode = r.Utf8();
                bool txEn = r.Bool(); bool tx = r.Bool(); bool dec = r.Bool();
                uint rxDf = r.U32(); uint txDf = r.U32();
                string deCall = r.Utf8(); string deGrid = r.Utf8();
                DialHz = dial; DialMode = mode; ProgramId = id;
                DxCall = (dxCall ?? "").Trim().ToUpperInvariant(); TxEnabled = txEn; StatusTime = DateTime.UtcNow;
                Debug?.Invoke($"{now}  Status      dial {dial / 1e6:0.000000} MHz  {mode}  DE {deCall} {deGrid}{(dec ? "  decodifica" : "")}{(tx ? "  TX" : "")}", false);
                StateChanged?.Invoke();
                break;
            }
            case 2: // Decode
            {
                bool isNew = r.Bool();
                uint ms = r.U32();
                int snr = r.I32();
                double dt = r.F64();
                uint df = r.U32();
                string mode = r.Utf8();
                string msg = r.Utf8();
                var d = new Decode { Time = Clock.UtcNow, Band = CurrentBand, Mode = DialMode, TxMode = mode, Snr = snr, Dt = dt, Df = df, Message = msg };
                ExtractStation(msg, d);
                var hh = TimeSpan.FromMilliseconds(ms);
                Debug?.Invoke($"{now}  Decode      {snr,3} {dt,4:0.0} {df,5} {mode}  {msg}   [{hh:hh\\:mm\\:ss}]", true);
                Interlocked.Increment(ref Decodes);
                if (d.Call != "") Decoded?.Invoke(d);
                break;
            }
            case 5: Debug?.Invoke($"{now}  QSO Logged  id={id}", false); break;
            case 6: Debug?.Invoke($"{now}  Close       id={id}", false); break;
            case 12: Debug?.Invoke($"{now}  Logged ADIF id={id}", false); break;
            default: Debug?.Invoke($"{now}  tipo {type}   id={id}", false); break;
        }
    }

    static readonly HashSet<string> CqWords = new(StringComparer.OrdinalIgnoreCase) { "DX", "EU", "TEST", "NA", "SA", "AS", "AF", "OC", "QRP", "POTA", "SOTA", "FD", "RU", "WW" };

    /// <summary>Ricava la stazione trasmittente e il suo locatore dal testo del messaggio.</summary>
    /// <summary>
    /// Messaggio di chiusura di un QSO: "IC8TEM IK3FHP RR73", "IK3FKP SV8PEX 73" o "… RRR". Le due stazioni hanno
    /// completato il collegamento: è un QSO avvenuto, desunto dalla decodifica.
    /// </summary>
    public static bool TryQso(string msg, out string a, out string b)
    {
        a = b = "";
        var t = (msg ?? "").Trim().ToUpperInvariant().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (t.Length != 3 || (t[2] != "RR73" && t[2] != "73" && t[2] != "RRR")) return false;
        static bool IsCall(string c) => c.Length >= 3 && c.Any(char.IsDigit) && c.Any(char.IsLetter) && c != "RR73" && !c.StartsWith("CQ");
        var x = t[0].Trim('<', '>'); var y = t[1].Trim('<', '>');
        if (!IsCall(x) || !IsCall(y) || x == y) return false;
        a = x; b = y;
        return true;
    }

    public static void ExtractStation(string msg, Decode d)
    {
        var t = (msg ?? "").Trim().ToUpperInvariant().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (t.Length == 0) return;
        int i;
        if (t[0] == "CQ" || t[0] == "QRZ" || t[0].StartsWith("CQ_"))
        {
            i = 1;
            if (i < t.Length && (CqWords.Contains(t[i]) || (t[i].All(char.IsDigit) && t[i].Length <= 3))) i++;
        }
        else i = 1;
        if (i >= t.Length) return;
        var call = t[i].Trim('<', '>');
        if (call.Length < 3 || !call.Any(char.IsDigit) || !call.Any(char.IsLetter) || call == "RR73" || call == "73") return;
        d.Call = call;
        if (i + 1 < t.Length)
        {
            var g = t[i + 1];
            if (g.Length == 4 && g != "RR73" && Locator.IsValid(g)) d.Grid = g;
        }
    }
}

/// <summary>Stazioni decodificate dall'utente per banda.</summary>
public sealed class DecodeStore
{
    readonly object _lock = new();
    readonly Dictionary<string, Decode> _map = new();

    public void Add(Decode d)
    {
        lock (_lock)
        {
            var key = d.Band + "|" + d.Call;
            if (_map.TryGetValue(key, out var o) && d.Grid == "" && o.Grid != "") d.Grid = o.Grid;
            _map[key] = d;
        }
    }

    public List<Decode> Get(string band, int minutes, DateTime nowUtc)
    {
        var cut = nowUtc.AddMinutes(-minutes);
        lock (_lock) return _map.Values.Where(d => d.Band == band && d.Time > cut).OrderByDescending(d => d.Time).ToList();
    }

    /// <summary>Ultimo locatore visto in chiaro nei messaggi di una stazione, su qualunque banda.</summary>
    public string GridOf(string call)
    {
        lock (_lock) return _map.Values.Where(d => d.Call == call && d.Grid != "").OrderByDescending(d => d.Time).Select(d => d.Grid).FirstOrDefault() ?? "";
    }

    public void Prune(DateTime nowUtc)
    {
        var cut = nowUtc.AddMinutes(-60);
        lock (_lock) foreach (var k in _map.Where(kv => kv.Value.Time < cut).Select(kv => kv.Key).ToList()) _map.Remove(k);
    }
}

/// <summary>QSO desunto da un messaggio di chiusura decodificato (RR73, 73, RRR).</summary>
public sealed class Qso
{
    public DateTime Time;
    public string Band = "";
    public string A = "", GridA = "", B = "", GridB = "";
}

/// <summary>QSO desunti dalle decodifiche, per banda; una coppia di stazioni conta una volta (vale l'ultima).</summary>
public sealed class QsoStore
{
    readonly object _lock = new();
    readonly Dictionary<string, Qso> _map = new();

    public void Add(Qso q)
    {
        var pair = string.CompareOrdinal(q.A, q.B) < 0 ? q.A + "|" + q.B : q.B + "|" + q.A;
        lock (_lock) _map[q.Band + "|" + pair] = q;
    }

    public Func<Qso, bool> Visible = _ => true;

    public List<Qso> Get(string band, int minutes, DateTime nowUtc)
    {
        var cut = nowUtc.AddMinutes(-minutes);
        List<Qso> list;
        lock (_lock) list = _map.Values.Where(q => q.Band == band && q.Time > cut).OrderByDescending(q => q.Time).ToList();
        return list.Where(q => Visible(q)).ToList();
    }

    public void Prune(DateTime nowUtc)
    {
        var cut = nowUtc.AddMinutes(-60);
        lock (_lock) foreach (var k in _map.Where(kv => kv.Value.Time < cut).Select(kv => kv.Key).ToList()) _map.Remove(k);
    }
}
