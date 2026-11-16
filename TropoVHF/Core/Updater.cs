#if NET7_0_OR_GREATER
using System.Formats.Tar;
#endif
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace TropoVHF.Core;

/// <summary>
/// Aggiornamento automatico dalle Release di GitHub, senza interazione (come YA-MS): un minuto dopo l'avvio e poi ogni
/// 30 minuti si legge l'ultima Release di github.com/ic8tem/LIVETROPOVHF. Se la versione è più nuova si scarica il
/// pacchetto portatile adatto al sistema (zip per Windows, tar.gz per Linux, Raspberry Pi e Alpine) e i file del
/// programma si sostituiscono sul posto: quelli in uso si rinominano in *.old-aggiornamento e si cancellano al
/// riavvio successivo. Non si toccano mai le impostazioni (LIVETROPOVHF.ini), l'archivio (.db), la cartella Dati e un
/// CALL3.TXT già presente. La nuova versione è attiva dal prossimo avvio. Sul Mac (app firmata) si segnala soltanto.
/// </summary>
public static class Updater
{
    public const string Repo = "ic8tem/LIVETROPOVHF";
    const string OldSuffix = ".old-aggiornamento";
    public static bool Enabled = true;
    public static string Status { get; private set; } = "";
    public static string Pending { get; private set; } = "";
    public static event Action Changed;
    static readonly TimeSpan Every = TimeSpan.FromMinutes(30);

    static Version Current => typeof(Updater).Assembly.GetName().Version ?? new Version(0, 0, 0);
    static string Dir => Settings.AppDir;
    static void Set(string s) { Status = s; Changed?.Invoke(); }

    /// <summary>Suffisso del pacchetto portatile per questo sistema (stessi nomi della Release).</summary>
    public static string AssetSuffix()
    {
        var a = RuntimeInformation.ProcessArchitecture;
#if WIN7
        return a == Architecture.X86 ? "-windows7-x86-portatile.zip" : "-windows7-x64-portatile.zip";
#else
        if (Compat.IsWindows)
            return a == Architecture.X86 ? "-windows-x86-portatile.zip" : a == Architecture.Arm64 ? "-windows-arm64-portatile.zip" : "-windows-x64-portatile.zip";
        if (Compat.IsMac)
            return a == Architecture.Arm64 ? "-mac-apple-silicon-app.zip" : "-mac-intel-app.zip";
        if (RuntimeInformation.RuntimeIdentifier.Contains("musl"))
            return a == Architecture.Arm64 ? "-alpine-arm64.tar.gz" : "-alpine-x64.tar.gz";
        if (a == Architecture.Arm64) return "-raspberry-pi-64bit.tar.gz";
        if (a == Architecture.Arm) return "-raspberry-pi-32bit.tar.gz";
        return "-linux-x64.tar.gz";
#endif
    }

    /// <summary>All'avvio: cancella i file lasciati dall'aggiornamento precedente.</summary>
    public static void CleanUp()
    {
        try
        {
            foreach (var f in Directory.EnumerateFiles(Dir, "*" + OldSuffix, SearchOption.AllDirectories))
            {
                if (f.Contains(Path.DirectorySeparatorChar + "Dati" + Path.DirectorySeparatorChar)) continue;
                try { File.Delete(f); } catch { }
            }
        }
        catch { }
    }

    public static async Task RunAsync(CancellationToken ct = default)
    {
        CleanUp();
        using var http = new HttpClient { Timeout = TimeSpan.FromMinutes(10) };
        http.DefaultRequestHeaders.UserAgent.ParseAdd($"LIVETROPOVHF/{Current.ToString(3)} (+https://github.com/{Repo})");
        http.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github+json");
        try { await Task.Delay(TimeSpan.FromMinutes(1), ct); } catch { return; }
        while (!ct.IsCancellationRequested)
        {
            if (Enabled && Pending == "") { try { await CheckOnce(http, ct); } catch (Exception ex) { Set("Aggiornamento: " + ex.Message); } }
            try { await Task.Delay(Every, ct); } catch { return; }
        }
    }

    static async Task CheckOnce(HttpClient http, CancellationToken ct)
    {
        using var resp = await http.GetAsync($"https://api.github.com/repos/{Repo}/releases/latest", ct);
        if ((int)resp.StatusCode == 404) { Set($"Aggiornamenti: nessuna release su GitHub ({DateTime.Now:HH:mm})"); return; }
        if (!resp.IsSuccessStatusCode) { Set($"Aggiornamenti: GitHub HTTP {(int)resp.StatusCode}"); return; }
        using var doc = JsonDocument.Parse(await Compat.ReadString(resp.Content, ct));
        var root = doc.RootElement;
        var tag = root.GetProperty("tag_name").GetString() ?? "";
        var vs = tag.TrimStart('v', 'V'); var cut = vs.IndexOfAny(new[] { '-', '+', ' ' }); if (cut > 0) vs = vs[..cut];
        if (!Version.TryParse(vs, out var latest)) { Set("Aggiornamenti: tag non riconosciuto " + tag); return; }
        if (latest <= Current) { Set($"Aggiornato: {Current.ToString(3)} è l'ultima versione ({DateTime.Now:HH:mm})"); return; }
        if (Compat.IsMac) { Set($"Nuova versione {latest.ToString(3)} su GitHub: scaricala dalla pagina delle Release"); return; }

        var suffix = AssetSuffix();
        string url = null, name = null;
        foreach (var a in root.GetProperty("assets").EnumerateArray())
        {
            var n = a.GetProperty("name").GetString() ?? "";
            if (n.StartsWith("LIVETROPOVHF-", StringComparison.OrdinalIgnoreCase) && n.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
            { url = a.GetProperty("browser_download_url").GetString(); name = n; break; }
        }
        if (url == null) { Set($"Aggiornamenti: {tag} senza il pacchetto *{suffix}"); return; }

        Set($"Scarico {tag}…");
        var work = Path.Combine(Settings.DataDir, "aggiornamento");
        if (Directory.Exists(work)) Directory.Delete(work, true);
        var ex = Path.Combine(work, "estratto");
        Directory.CreateDirectory(ex);
        var file = Path.Combine(work, name);
        await Compat.WriteBytes(file, await Compat.GetBytes(http, url, ct), ct);
        if (name.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)) ZipFile.ExtractToDirectory(file, ex);
        else
        {
#if NET7_0_OR_GREATER
            await using var fs = File.OpenRead(file);
            await using var gz = new GZipStream(fs, CompressionMode.Decompress);
            await TarFile.ExtractToDirectoryAsync(gz, ex, true, ct);
#else
            Set("Aggiornamenti: pacchetto tar.gz non gestito da questa versione"); return;
#endif
        }
        // il tar.gz contiene la cartella LIVETROPOVHF/, lo zip i file direttamente
        var src = ex;
        var dirs = Directory.GetDirectories(ex); var files = Directory.GetFiles(ex);
        if (files.Length == 0 && dirs.Length == 1) src = dirs[0];
        var exe = Compat.IsWindows ? "LIVETROPOVHF.exe" : "LIVETROPOVHF";
        if (!File.Exists(Path.Combine(src, exe))) { Set($"Aggiornamenti: nel pacchetto {tag} manca {exe}"); return; }

        int n2 = 0;
        foreach (var f in Directory.EnumerateFiles(src, "*", SearchOption.AllDirectories))
        {
            var rel = Compat.RelativePath(src, f);
            var low = rel.ToLowerInvariant();
            if (low.EndsWith(".ini") || low.EndsWith(".db") || low.StartsWith("dati" + Path.DirectorySeparatorChar)) continue;
            var dest = Path.Combine(Dir, rel);
            if (low == "call3.txt" && File.Exists(dest)) continue;      // il CALL3.TXT dell'utente resta
            Directory.CreateDirectory(Path.GetDirectoryName(dest)!);
            try { File.Copy(f, dest, true); }
            catch (Exception e) when (e is IOException || e is UnauthorizedAccessException)
            {
                // file in uso (l'eseguibile, le librerie caricate): si rinomina e si copia il nuovo al suo posto
                Compat.MoveOver(dest, dest + OldSuffix);
                File.Copy(f, dest, true);
            }
            n2++;
        }
#if NET7_0_OR_GREATER
        if (!Compat.IsWindows)
            try { File.SetUnixFileMode(Path.Combine(Dir, exe), (UnixFileMode)Convert.ToInt32("755", 8)); } catch { }
#endif
        try { Directory.Delete(work, true); } catch { }
        Pending = latest.ToString(3);
        Set($"Versione {Pending} installata ({n2} file): sarà attiva al prossimo avvio");
    }
}
