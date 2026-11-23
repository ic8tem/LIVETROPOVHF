using System.IO.Pipes;
using System.Text;

namespace TropoVHF.Core;

/// <summary>
/// 1.4 — Una sola copia del programma per utente. La seconda copia chiede alla prima di mostrarsi (tubo con nome) e si chiude.
/// Dopo un aggiornamento la copia nuova parte con «--riavvio» e aspetta che la vecchia si chiuda invece di cederle il passo.
/// Le modalità a riga di comando (--prova-fonti, --valida) non passano di qui.
/// </summary>
public static class SingleInstance
{
    public const string RestartArg = "--riavvio";
    static Mutex _mutex;
    static CancellationTokenSource _cts;
    /// <summary>Un'altra copia ha chiesto di mostrare la finestra (chiamato fuori dal thread dell'interfaccia).</summary>
    public static event Action Activate;

    static string Id
    {
        get
        {
            var u = (Environment.UserName ?? "utente").ToLowerInvariant();
            var sb = new StringBuilder();
            foreach (var c in u) sb.Append(char.IsLetterOrDigit(c) ? c : '_');
            return "LIVETROPOVHF-" + sb;
        }
    }

    /// <summary>true = questa è l'unica copia e può partire; false = c'è già il programma aperto (portato in primo piano).</summary>
    public static bool Acquire(string[] args)
    {
        bool restart = args != null && Array.IndexOf(args, RestartArg) >= 0;
        try
        {
            string name = (Compat.IsWindows ? "Local\\" : "") + Id;
            _mutex = new Mutex(true, name, out bool created);
            if (created) { StartServer(); return true; }
            // un'altra copia è aperta: se risponde la si porta in primo piano e questa si chiude
            if (!restart && Signal()) return false;
            // non risponde (si sta chiudendo) oppure è il riavvio dopo un aggiornamento: si aspetta che liberi il posto
            bool got;
            try { got = _mutex.WaitOne(TimeSpan.FromSeconds(restart ? 60 : 10)); }
            catch (AbandonedMutexException) { got = true; }
            if (got) { StartServer(); return true; }
            return false;
        }
        catch (Exception ex)
        {
            // se il sistema non permette il controllo, meglio partire che non partire
            ErrorLog.Note("copia unica: controllo non disponibile (" + ex.Message + ")");
            return true;
        }
    }

    public static void Release()
    {
        try { _cts?.Cancel(); } catch { }
        try { _mutex?.ReleaseMutex(); } catch { }
        try { _mutex?.Dispose(); } catch { }
        _mutex = null;
    }

    static bool Signal()
    {
        try
        {
            using var c = new NamedPipeClientStream(".", Id, PipeDirection.InOut);
            c.Connect(2000);
            var w = new StreamWriter(c, new UTF8Encoding(false)) { AutoFlush = true };
            var r = new StreamReader(c, Encoding.UTF8);
            w.WriteLine("mostra");
            var t = Task.Run(() => r.ReadLine());
            return t.Wait(2000) && t.Result == "ok";
        }
        catch { return false; }
    }

    static void StartServer()
    {
        _cts = new CancellationTokenSource();
        var ct = _cts.Token;
        Task.Run(async () =>
        {
            while (!ct.IsCancellationRequested)
            {
                try
                {
                    using var s = new NamedPipeServerStream(Id, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
                    await s.WaitForConnectionAsync(ct);
                    var r = new StreamReader(s, Encoding.UTF8);
                    var w = new StreamWriter(s, new UTF8Encoding(false)) { AutoFlush = true };
                    var line = await r.ReadLineAsync();
                    if (line == "mostra")
                    {
                        try { Activate?.Invoke(); } catch { }
                        await w.WriteLineAsync("ok");
                    }
                }
                catch (OperationCanceledException) { break; }
                catch { try { await Task.Delay(1000, ct); } catch { break; } }
            }
        });
    }
}
