using System.Diagnostics;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace TropoVHF.Core;

/// <summary>
/// Aggiornamento automatico da GitHub, senza interazione: ogni 30 minuti legge l'ultima release di
/// github.com/ic8tem/LIVETROPOVHF. Se il numero di versione (tag vX.Y.Z) è più alto di quello in esecuzione e c'è
/// l'allegato per Windows 64 bit (LIVETROPOVHF-win-x64.zip con dentro LIVETROPOVHF.exe, oppure direttamente
/// LIVETROPOVHF-win-x64.exe), lo scarica in Dati\aggiornamento, controlla che sia un eseguibile Windows e lo installa:
/// l'eseguibile in uso viene rinominato in LIVETROPOVHF.old.exe (Windows lo permette anche mentre gira) e il nuovo
/// prende il suo posto; le tarature del pacchetto vengono copiate accanto. La nuova versione parte dal prossimo avvio;
/// il vecchio file si cancella da solo. Impostazioni, archivio e dati non vengono toccati. Solo Windows.
/// </summary>
public static class Updater
{
    public const string Repo = "ic8tem/LIVETROPOVHF";
    public static bool Enabled = true;
    public static string Status { get; private set; } = "";
    public static string Pending { get; private set; } = "";      // versione installata, attiva al prossimo avvio
    public static event Action Changed;
    static readonly TimeSpan Every = TimeSpan.FromMinutes(30);

    static Version Current => typeof(Updater).Assembly.GetName().Version ?? new Version(0, 0, 0);
    static string ExePath => Environment.ProcessPath ?? "";
    static void Set(string s) { Status = s; Changed?.Invoke(); }

    /// <summary>All'avvio: cancella l'eseguibile vecchio lasciato dall'aggiornamento precedente.</summary>
    public static void CleanUp()
    {
        try
        {
            var dir = Path.GetDirectoryName(ExePath) ?? "";
            var old = Path.Combine(dir, Path.GetFileNameWithoutExtension(ExePath) + ".old.exe");
            if (File.Exists(old)) File.Delete(old);
        }
        catch { }
    }

    public static async Task RunAsync(CancellationToken ct = default)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
        CleanUp();
        using var http = new HttpClient { Timeout = TimeSpan.FromMinutes(10) };
        http.DefaultRequestHeaders.UserAgent.ParseAdd("LIVETROPOVHF-updater/" + Current.ToString(3));
        http.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github+json");
        try { await Task.Delay(TimeSpan.FromMinutes(1), ct); } catch { return; }   // non subito all'avvio
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
        using var doc = JsonDocument.Parse(await resp.Content.ReadAsStringAsync(ct));
        var root = doc.RootElement;
        if (root.TryGetProperty("draft", out var dr) && dr.GetBoolean()) return;
        var tag = root.GetProperty("tag_name").GetString() ?? "";
        if (!Version.TryParse(tag.TrimStart('v', 'V').Split('-')[0], out var latest)) { Set("Aggiornamenti: tag non riconosciuto " + tag); return; }
        if (latest <= Current) { Set($"Aggiornato: {Current.ToString(3)} è l'ultima versione ({DateTime.Now:HH:mm})"); return; }

        string url = null, name = null;
        foreach (var a in root.GetProperty("assets").EnumerateArray())
        {
            var n = a.GetProperty("name").GetString() ?? "";
            if (n.Contains("win-x64", StringComparison.OrdinalIgnoreCase) && (n.EndsWith(".zip", StringComparison.OrdinalIgnoreCase) || n.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)))
            { url = a.GetProperty("browser_download_url").GetString(); name = n; break; }
        }
        if (url == null) { Set($"Aggiornamenti: {tag} su GitHub senza allegato win-x64"); return; }

        Set($"Scarico {tag}…");
        var dir = Path.GetDirectoryName(ExePath) ?? Settings.AppDir;
        var work = Path.Combine(Settings.DataDir, "aggiornamento");
        if (Directory.Exists(work)) Directory.Delete(work, true);
        Directory.CreateDirectory(work);
        var file = Path.Combine(work, name);
        await File.WriteAllBytesAsync(file, await http.GetByteArrayAsync(url, ct), ct);

        string newExe = file;
        string tarature = null;
        if (name.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
        {
            var ex = Path.Combine(work, "estratto");
            ZipFile.ExtractToDirectory(file, ex, true);
            newExe = Directory.GetFiles(ex, "LIVETROPOVHF.exe", SearchOption.AllDirectories).FirstOrDefault();
            if (newExe == null) { Set($"Aggiornamenti: nel pacchetto {tag} manca LIVETROPOVHF.exe"); return; }
            tarature = Directory.GetDirectories(ex, "Tarature", SearchOption.AllDirectories).FirstOrDefault();
        }
        // controllo minimo: eseguibile Windows (MZ) di dimensione credibile
        var head = new byte[2];
        using (var fs = File.OpenRead(newExe)) { if (fs.Length < 1_000_000 || fs.Read(head, 0, 2) != 2 || head[0] != 'M' || head[1] != 'Z') { Set("Aggiornamenti: file scaricato non valido"); return; } }

        var cur = ExePath;
        var old = Path.Combine(dir, Path.GetFileNameWithoutExtension(cur) + ".old.exe");
        if (File.Exists(old)) File.Delete(old);
        File.Move(cur, old);                       // l'eseguibile in uso si può rinominare
        try { File.Copy(newExe, cur, true); }
        catch { File.Move(old, cur); throw; }      // se la copia fallisce si rimette tutto com'era
        if (tarature != null)
            foreach (var f in Directory.GetFiles(tarature))
                File.Copy(f, Path.Combine(dir, "Tarature", Path.GetFileName(f)), true);
        Pending = latest.ToString(3);
        Set($"Versione {Pending} installata: sarà attiva al prossimo avvio");
    }
}
