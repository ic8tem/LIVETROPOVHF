using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace TropoVHF.Core;

/// <summary>
/// Registro degli errori, sempre attivo (anche con il registro dei calcoli spento): Dati/errori.log accanto al
/// programma. Ogni voce ha data e ora, versione, sistema, linguetta aperta, l'errore completo e le ultime righe di ciò
/// che il programma stava facendo. Il file ricomincia oltre 4 MB (la copia precedente resta come .old).
/// Dopo un errore resta un segnale (errori-da-inviare.txt): all'avvio successivo il programma propone di inviare il
/// rapporto, aprendo nel browser una segnalazione GitHub già compilata (senza token: la invia l'utente).
/// </summary>
public static class ErrorLog
{
    static readonly object Lock = new();
    /// <summary>Linguetta aperta, scritta dall'interfaccia.</summary>
    public static string CurrentTab = "";
    /// <summary>Ultime righe di contesto (registro dei calcoli), fornite dall'interfaccia se c'è.</summary>
    public static Func<IEnumerable<string>> Context;
    /// <summary>Avviso all'interfaccia: un errore è stato annotato (testo breve).</summary>
    public static event Action<string> Logged;

    public static string FilePath => Path.Combine(Settings.DataDir, "errori.log");
    static string FlagPath => Path.Combine(Settings.DataDir, "errori-da-inviare.txt");

    public static string Version => typeof(ErrorLog).Assembly.GetName().Version?.ToString(3) ?? "";

    public static string Platform
    {
        get
        {
#if WIN7
            const string flavor = "Windows 7/8.1 (.NET Framework 4.8)";
#else
            const string flavor = ".NET";
#endif
            string os;
            try { os = RuntimeInformation.OSDescription + " " + RuntimeInformation.OSArchitecture; } catch { os = Environment.OSVersion.ToString(); }
            return flavor + " · " + os;
        }
    }

    /// <summary>Annota un errore. fatal = il programma sta per chiudersi.</summary>
    public static void Write(Exception ex, string where, bool fatal = false)
    {
        if (ex == null) return;
        try
        {
            var sb = new StringBuilder();
            sb.AppendLine("==================== " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " (" + DateTime.UtcNow.ToString("HH:mm:ss") + " UTC)" + (fatal ? " · CHIUSURA" : ""));
            sb.AppendLine("versione " + Version + " · " + Platform);
            sb.AppendLine("linguetta: " + (CurrentTab == "" ? "—" : CurrentTab) + " · origine: " + where);
            sb.AppendLine(ex.ToString());
            var ctx = Context?.Invoke()?.ToList();
            if (ctx != null && ctx.Count > 0)
            {
                sb.AppendLine("--- ultime righe del registro dei calcoli ---");
                foreach (var l in ctx.Skip(Math.Max(0, ctx.Count - 50))) sb.AppendLine(l);
            }
            sb.AppendLine();
            lock (Lock)
            {
                Directory.CreateDirectory(Settings.DataDir);
                var fi = new FileInfo(FilePath);
                if (fi.Exists && fi.Length > 4 * 1024 * 1024) Compat.MoveOver(FilePath, FilePath + ".old");
                File.AppendAllText(FilePath, sb.ToString(), Encoding.UTF8);
                File.WriteAllText(FlagPath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n" + ex.GetType().Name + ": " + ex.Message, Encoding.UTF8);
            }
        }
        catch { }
        try { Logged?.Invoke(ex.GetType().Name + ": " + ex.Message); } catch { }
    }

    /// <summary>Annota un evento non di errore che serve a ricostruire i fatti (es. esito dei controlli di aggiornamento).</summary>
    public static void Note(string text)
    {
        try
        {
            lock (Lock)
            {
                Directory.CreateDirectory(Settings.DataDir);
                File.AppendAllText(FilePath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + text + Environment.NewLine, Encoding.UTF8);
            }
        }
        catch { }
    }

    /// <summary>C'è un errore non ancora proposto per l'invio?</summary>
    public static bool HasPending => File.Exists(FlagPath);
    public static string PendingSummary { get { try { return File.ReadAllText(FlagPath); } catch { return ""; } } }
    public static void ClearPending() { try { File.Delete(FlagPath); } catch { } }

    /// <summary>
    /// Indirizzo di una nuova segnalazione GitHub già compilata con l'ultimo errore. Nominativo e locatore sono tolti
    /// (sostituiti con [CALL] e [LOC]); il testo è accorciato perché l'indirizzo resti sotto i limiti del browser.
    /// </summary>
    public static string IssueUrl(string call, string loc)
    {
        string body;
        try
        {
            var all = File.Exists(FilePath) ? File.ReadAllText(FilePath) : "";
            int i = all.LastIndexOf("==================== ", StringComparison.Ordinal);
            body = i >= 0 ? all.Substring(i) : all;
        }
        catch { body = ""; }
        body = Anonymize(body, call, loc);
        if (body.Length > 5500) body = body.Substring(0, 5500) + "\n…(accorciato)";
        var title = "Errore " + Version + ": " + Anonymize(PendingSummary.Split('\n').LastOrDefault() ?? "", call, loc);
        if (title.Length > 120) title = title.Substring(0, 120);
        var text = "Rapporto automatico di LIVETROPOVHF (nominativo e locatore tolti).\n\nCosa stavo facendo: \n\n```\n" + body + "\n```\n";
        return "https://github.com/" + Updater.Repo + "/issues/new?labels=errore&title=" + Uri.EscapeDataString(title) + "&body=" + Uri.EscapeDataString(text);
    }

    static string Anonymize(string s, string call, string loc)
    {
        if (string.IsNullOrEmpty(s)) return s;
        if (!string.IsNullOrWhiteSpace(call)) s = Regex.Replace(s, Regex.Escape(call.Trim()), "[CALL]", RegexOptions.IgnoreCase);
        if (!string.IsNullOrWhiteSpace(loc) && loc.Trim().Length >= 4) s = Regex.Replace(s, Regex.Escape(loc.Trim().Substring(0, 4)) + @"[A-Za-z0-9]{0,6}", "[LOC]", RegexOptions.IgnoreCase);
        // percorsi con il nome dell'utente di Windows
        s = Regex.Replace(s, @"([A-Za-z]:\\Users\\)[^\\]+", "$1[utente]", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"(/home/|/Users/)[^/]+", "$1[utente]");
        return s;
    }
}
