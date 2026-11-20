using System.IO.Compression;
using System.Reflection;

namespace TropoVHF.Forecast;

/// <summary>Valori climatologici dei condotti in un punto (ITU-R P.453, mappe annuali a 1,5°).</summary>
public struct DuctClimate
{
    public bool Ok;
    public double SOccur, SThick, SStren;              // condotto al suolo: % del tempo, spessore (m), intensità (M)
    public double EOccur, EBase, EThick, EStren;       // condotto elevato: % del tempo, base (m), spessore (m), intensità (M)
}

/// <summary>
/// Climatologia dei condotti di ITU-R P.453 (prodotti digitali della Study Group 3, griglia 1,5° × 1,5°,
/// righe da 90°N a 90°S, colonne da 0° verso est fino a 360°). Fonte: © ITU, uso personale e non commerciale con
/// citazione della fonte. File ricavati dal pacchetto PathProfile di G0MJW e compressi in binario.
/// </summary>
public static class P453
{
    const int Rows = 121, Cols = 241;
    static float[][] _maps;
    static readonly object _lock = new();

    static float[][] Maps
    {
        get
        {
            lock (_lock)
            {
                if (_maps != null) return _maps;
                try
                {
                    using var s = Assembly.GetExecutingAssembly().GetManifestResourceStream("TropoVHF.Forecast.p453.bin.gz");
                    if (s == null) return _maps = FromFiles();
                    using var gz = new GZipStream(s, CompressionMode.Decompress);
                    using var br = new BinaryReader(gz);
                    if (new string(br.ReadChars(4)) != "P453") throw new InvalidDataException("intestazione");
                    int n = br.ReadInt32(), r = br.ReadInt32(), c = br.ReadInt32();
                    if (r != Rows || c != Cols) throw new InvalidDataException("dimensioni");
                    var m = new float[n][];
                    for (int k = 0; k < n; k++)
                    {
                        m[k] = new float[r * c];
                        for (int i = 0; i < m[k].Length; i++) m[k][i] = br.ReadSingle();
                    }
                    CalcLog.W($"P.453: {n} mappe climatologiche dei condotti caricate (1,5°, © ITU)");
                    return _maps = m;
                }
                catch (Exception ex)
                {
                    CalcLog.W("P.453: errore nel caricamento dei dati: " + ex.Message);
                    return _maps = Array.Empty<float[]>();
                }
            }
        }
    }

    /// <summary>Cartella in cui mettere i file delle mappe P.453 scaricati dal sito dell'ITU.</summary>
    public static string Folder => Path.Combine(TropoVHF.Core.Settings.DataDir, "P453");
    static readonly string[] Names = { "S_OCCUR", "S_THICK", "S_STREN", "E_OCCUR", "E_base", "E_THICK", "E_STREN" };
    public static bool Available => Maps.Length >= 7;
    /// <summary>Pagina dei prodotti digitali della Study Group 3 e archivio dei condotti (stesso file per suolo ed elevati).</summary>
    public const string ItuPage = "https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx";
    public const string ItuZip = "https://www.itu.int/en/ITU-R/study-groups/rsg3/rwp3m/Documents/elevated_and_surface_duct.zip";

    /// <summary>Rilegge le mappe (dopo averle scaricate o copiate), senza riavviare.</summary>
    public static void Reload() { lock (_lock) _maps = null; }

    /// <summary>
    /// Scarica l'archivio dei condotti dal sito dell'ITU per uso personale dell'utente ed estrae i sette file in Dati/P453
    /// (anche se nell'archivio stanno in sottocartelle o in un altro zip). Restituisce l'esito da mostrare.
    /// </summary>
    public static async Task<(bool ok, string msg)> DownloadFromItu(HttpClient http, Action<string> progress, CancellationToken ct)
    {
        Directory.CreateDirectory(Folder);
        var zipPath = Path.Combine(Folder, "elevated_and_surface_duct.zip");
        progress?.Invoke("ITU: scarico l'archivio dei condotti…");
        CalcLog.W("P.453: scarico " + ItuZip);
        using (var resp = await http.GetAsync(ItuZip, HttpCompletionOption.ResponseHeadersRead, ct))
        {
            resp.EnsureSuccessStatusCode();
            using var src = await resp.Content.ReadAsStreamAsync(ct);
            using var dst = File.Create(zipPath);
            await src.CopyToAsync(dst, ct);
        }
        CalcLog.W($"P.453: archivio salvato ({new FileInfo(zipPath).Length / 1024} kB), estraggo");
        var found = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var seen = new List<string>();
        void Scan(ZipArchive za, int depth)
        {
            foreach (var e in za.Entries)
            {
                if (string.IsNullOrEmpty(e.Name)) continue;
                seen.Add(e.FullName);
                var stem = Path.GetFileNameWithoutExtension(e.Name);
                var ext = Path.GetExtension(e.Name).ToLowerInvariant();
                var n = Names.FirstOrDefault(x => string.Equals(x, stem, StringComparison.OrdinalIgnoreCase));
                if (n != null && (ext == ".txt" || ext == ""))
                {
                    using var es = e.Open(); using var fo = File.Create(Path.Combine(Folder, n + ".TXT"));
                    es.CopyTo(fo); found.Add(n);
                }
                else if (ext == ".zip" && depth < 2)
                {
                    using var es = e.Open(); var ms = new MemoryStream(); es.CopyTo(ms); ms.Position = 0;
                    using var inner = new ZipArchive(ms, ZipArchiveMode.Read); Scan(inner, depth + 1);
                }
            }
        }
        using (var za = ZipFile.OpenRead(zipPath)) Scan(za, 0);
        CalcLog.W("P.453: contenuto dell'archivio: " + string.Join(", ", seen.Take(60)));
        var missing = Names.Where(n => !found.Contains(n)).ToList();
        Reload();
        if (missing.Count == 0 && Available)
        {
            try { File.Delete(zipPath); } catch { }
            return (true, $"Mappe P.453 scaricate dall'ITU e attive ({Folder}). Calcolo B dei condotti acceso.");
        }
        return (false, "Nell'archivio dell'ITU non ho trovato: " + string.Join(", ", missing) +
                       $". L'archivio è rimasto in {zipPath}: estrarre a mano i file in {Folder} (vedi il registro per il contenuto).");
    }

    /// <summary>
    /// Versioni pubbliche: i dati ITU non si ridistribuiscono, l'utente li scarica dalla pagina dei prodotti digitali
    /// della Study Group 3 e mette i file .TXT (S_OCCUR, S_THICK, S_STREN, E_OCCUR, E_base, E_THICK, E_STREN) in Dati/P453.
    /// </summary>
    static float[][] FromFiles()
    {
        try
        {
            var m = new float[Names.Length][];
            for (int k = 0; k < Names.Length; k++)
            {
                var f = Directory.Exists(Folder) ? Directory.GetFiles(Folder).FirstOrDefault(x => string.Equals(Path.GetFileNameWithoutExtension(x), Names[k], StringComparison.OrdinalIgnoreCase)) : null;
                if (f == null)
                {
                    CalcLog.W($"P.453: file {Names[k]}.TXT non trovato in {Folder}: il calcolo B dei condotti potenziali è spento (vedi il manuale delle previsioni, § 6)");
                    return Array.Empty<float[]>();
                }
                var vals = File.ReadAllText(f).Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                               .Select(x => float.Parse(x, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
                if (vals.Length != Rows * Cols) { CalcLog.W($"P.453: {Path.GetFileName(f)} ha {vals.Length} valori invece di {Rows * Cols}"); return Array.Empty<float[]>(); }
                m[k] = vals;
            }
            CalcLog.W($"P.453: {m.Length} mappe climatologiche dei condotti lette da {Folder} (1,5°, © ITU)");
            return m;
        }
        catch (Exception ex) { CalcLog.W("P.453: errore nella lettura dei file: " + ex.Message); return Array.Empty<float[]>(); }
    }

    static double Bilinear(float[] m, double lat, double lon)
    {
        double r = (90 - Math.Max(-90, Math.Min(90, lat))) / 1.5;
        double c = (((lon % 360) + 360) % 360) / 1.5;
        int r0 = Math.Min(Rows - 2, (int)r), c0 = Math.Min(Cols - 2, (int)c);
        double tr = r - r0, tc = c - c0;
        return m[r0 * Cols + c0] * (1 - tr) * (1 - tc) + m[r0 * Cols + c0 + 1] * (1 - tr) * tc
             + m[(r0 + 1) * Cols + c0] * tr * (1 - tc) + m[(r0 + 1) * Cols + c0 + 1] * tr * tc;
    }

    public static DuctClimate At(double lat, double lon)
    {
        var m = Maps;
        if (m.Length < 7 || double.IsNaN(lat) || double.IsNaN(lon)) return default;
        return new DuctClimate
        {
            Ok = true,
            SOccur = Bilinear(m[0], lat, lon), SThick = Bilinear(m[1], lat, lon), SStren = Bilinear(m[2], lat, lon),
            EOccur = Bilinear(m[3], lat, lon), EBase = Bilinear(m[4], lat, lon), EThick = Bilinear(m[5], lat, lon), EStren = Bilinear(m[6], lat, lon)
        };
    }
}
