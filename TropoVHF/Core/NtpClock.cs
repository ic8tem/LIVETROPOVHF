using System.Net;
using System.Net.Sockets;

namespace TropoVHF.Core;

public sealed class NtpServerResult
{
    public string Host = "";
    public bool Ok;
    public double OffsetMs;
    public double DelayMs;
    public string Error = "";
}

/// <summary>Orologio del programma allineato con NTP (l'orologio di sistema non viene toccato).</summary>
public static class Clock
{
    static long _offsetTicks;
    public static volatile bool NtpOk;
    public static DateTime UtcNow => DateTime.UtcNow.AddTicks(Interlocked.Read(ref _offsetTicks));
    public static double OffsetMs => Interlocked.Read(ref _offsetTicks) / 10000.0;
    public static void SetOffset(double ms) => Interlocked.Exchange(ref _offsetTicks, (long)(ms * 10000));
}

/// <summary>Interroga 10 server SNTP ogni N minuti e prende la mediana degli scarti.</summary>
public sealed class NtpChecker
{
    public static readonly string[] DefaultServers =
    {
        "0.pool.ntp.org", "1.pool.ntp.org", "2.pool.ntp.org", "3.pool.ntp.org", "it.pool.ntp.org",
        "ntp1.inrim.it", "ntp2.inrim.it", "ptbtime1.ptb.de", "time.google.com", "time.cloudflare.com"
    };

    public string[] Servers = DefaultServers;
    public int IntervalMinutes = 3;
    public int MinAnswers = 3;
    public List<NtpServerResult> Last = new();
    public DateTime LastCheckUtc;
    public event Action Updated;

    CancellationTokenSource _cts;

    public void Start()
    {
        Stop();
        _cts = new CancellationTokenSource();
        var ct = _cts.Token;
        Task.Run(async () =>
        {
            while (!ct.IsCancellationRequested)
            {
                await CheckNow();
                try { await Task.Delay(TimeSpan.FromMinutes(Math.Max(1, IntervalMinutes)), ct); } catch { }
            }
        });
    }

    public void Stop() { try { _cts?.Cancel(); } catch { } _cts = null; }

    public async Task CheckNow()
    {
        var tasks = Servers.Select(h => QueryAsync(h)).ToArray();
        NtpServerResult[] res;
        try { res = await Task.WhenAll(tasks); }
        catch { res = tasks.Where(t => t.Status == TaskStatus.RanToCompletion).Select(t => t.Result).ToArray(); }

        var ok = res.Where(r => r.Ok).ToList();
        bool aligned = false;
        if (ok.Count >= MinAnswers)
        {
            var offs = ok.Select(r => r.OffsetMs).OrderBy(x => x).ToList();
            double med = offs[offs.Count / 2];
            // scarta i valori lontani più di 250 ms dalla mediana
            var good = offs.Where(x => Math.Abs(x - med) <= 250).ToList();
            if (good.Count >= MinAnswers)
            {
                double med2 = good[good.Count / 2];
                Clock.SetOffset(med2);
                aligned = true;
            }
        }
        Clock.NtpOk = aligned;
        Last = res.ToList();
        LastCheckUtc = DateTime.UtcNow;
        Updated?.Invoke();
    }

    static async Task<NtpServerResult> QueryAsync(string host)
    {
        var r = new NtpServerResult { Host = host };
        try
        {
            using var udp = new UdpClient();
            udp.Client.ReceiveTimeout = 2500;
            var addrs = await Dns.GetHostAddressesAsync(host);
            var ip = addrs.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork) ?? addrs.FirstOrDefault();
            if (ip == null) { r.Error = "DNS"; return r; }
            udp.Connect(new IPEndPoint(ip, 123));
            var pkt = new byte[48];
            pkt[0] = 0x1B; // LI=0, VN=3, Mode=3 (client)
            var t1 = DateTime.UtcNow;
            await udp.SendAsync(pkt, pkt.Length);
            var recvTask = udp.ReceiveAsync();
            var done = await Task.WhenAny(recvTask, Task.Delay(2500));
            if (done != recvTask)
            {
                // la ricezione abbandonata fallisce (errore 995) quando il socket si chiude: la si osserva, così non finisce nel registro errori
                _ = recvTask.ContinueWith(t => _ = t.Exception, TaskContinuationOptions.OnlyOnFaulted);
                r.Error = "timeout"; return r;
            }
            var t4 = DateTime.UtcNow;
            var data = recvTask.Result.Buffer;
            if (data.Length < 48) { r.Error = "risposta corta"; return r; }
            var t2 = ReadTs(data, 32);
            var t3 = ReadTs(data, 40);
            double offset = ((t2 - t1).TotalMilliseconds + (t3 - t4).TotalMilliseconds) / 2.0;
            double delay = (t4 - t1).TotalMilliseconds - (t3 - t2).TotalMilliseconds;
            r.OffsetMs = offset;
            r.DelayMs = delay;
            r.Ok = true;
        }
        catch (Exception ex) { r.Error = ex.GetType().Name; }
        return r;
    }

    static DateTime ReadTs(byte[] b, int o)
    {
        ulong sec = ((ulong)b[o] << 24) | ((ulong)b[o + 1] << 16) | ((ulong)b[o + 2] << 8) | b[o + 3];
        ulong frac = ((ulong)b[o + 4] << 24) | ((ulong)b[o + 5] << 16) | ((ulong)b[o + 6] << 8) | b[o + 7];
        double ms = sec * 1000.0 + frac * 1000.0 / 4294967296.0;
        return new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(ms);
    }
}
