using System.IO.Compression;
using TropoVHF.Core;

namespace TropoVHF.Forecast;

/// <summary>
/// Quote del terreno a 1 secondo d'arco (circa 30 m), riquadri da 1°×1° in formato HGT (3601×3601, interi a 16 bit
/// big-endian). Fonte: Terrain Tiles su AWS (formato "skadi", SRTM e altri dati aperti, senza chiave):
/// https://s3.amazonaws.com/elevation-tiles-prod/skadi/N40/N40E014.hgt.gz
/// I riquadri scaricati restano in Dati/SRTM1 accanto al programma. Sul mare le quote negative valgono 0.
/// </summary>
public sealed class Srtm
{
    public static string Folder { get { var d = Path.Combine(Settings.DataDir, "SRTM1"); try { Directory.CreateDirectory(d); } catch { } return d; } }

    readonly HttpClient _http;
    readonly Dictionary<string, short[]> _tiles = new();
    readonly Dictionary<string, int> _side = new();
    readonly LinkedList<string> _lru = new();
    readonly HashSet<string> _missing = new();
    const int MaxTiles = 6;

    public int Downloaded, Missing;
    public Action<string> Progress;

    public Srtm(HttpClient http) { _http = http; }

    public static string TileName(int lat, int lon) =>
        (lat >= 0 ? "N" : "S") + Math.Abs(lat).ToString("00") + (lon >= 0 ? "E" : "W") + Math.Abs(lon).ToString("000");

    async Task<(short[] d, int side)> Tile(int lat, int lon, CancellationToken ct)
    {
        var name = TileName(lat, lon);
        if (_tiles.TryGetValue(name, out var t)) { _lru.Remove(name); _lru.AddFirst(name); return (t, _side[name]); }
        if (_missing.Contains(name)) return (null, 0);
        var file = Path.Combine(Folder, name + ".hgt.gz");
        if (!File.Exists(file))
        {
            Progress?.Invoke($"SRTM1: scarico {name} (6–7 MB, la prima volta)…");
            var url = $"https://s3.amazonaws.com/elevation-tiles-prod/skadi/{name.Substring(0, 3)}/{name}.hgt.gz";
            for (int attempt = 1; attempt <= 2 && !File.Exists(file); attempt++)
            try
            {
                using var resp = await _http.GetAsync(url, ct);
                if (!resp.IsSuccessStatusCode) { _missing.Add(name); Missing++; CalcLog.W($"SRTM1: {name} non disponibile (HTTP {(int)resp.StatusCode}), quota 0"); return (null, 0); }
                var bytes = await resp.Content.ReadAsByteArrayAsync();
                var tmp = file + ".part";
                await Compat.WriteBytes(tmp, bytes, ct);
                Compat.MoveOver(tmp, file);
                Downloaded++;
                CalcLog.W($"SRTM1: scaricato {name} ({bytes.Length / 1024} kB) da {url}");
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) { throw; }
            catch (Exception ex)
            {
                CalcLog.W($"SRTM1: {name} tentativo {attempt} non riuscito: {ex.Message}");
                if (attempt == 2) { _missing.Add(name); Missing++; return (null, 0); }
            }
        }
        short[] data; int side;
        try
        {
            using var fs = File.OpenRead(file);
            using var gz = new GZipStream(fs, CompressionMode.Decompress);
            using var ms = new MemoryStream();
            await gz.CopyToAsync(ms, 81920, ct);
            var b = ms.GetBuffer(); int n = (int)ms.Length;
            int count = n / 2;
            side = (int)Math.Round(Math.Sqrt(count));
            if (side < 100 || side * side != count) { _missing.Add(name); Missing++; return (null, 0); }
            data = new short[count];
            for (int i = 0; i < count; i++) data[i] = (short)((b[2 * i] << 8) | b[2 * i + 1]);
        }
        catch (OperationCanceledException) { throw; }
        catch { try { File.Delete(file); } catch { } _missing.Add(name); Missing++; return (null, 0); }
        _tiles[name] = data; _side[name] = side; _lru.AddFirst(name);
        CalcLog.W($"SRTM1: letto {name}, {side}×{side} punti");
        while (_lru.Count > MaxTiles) { var old = _lru.Last.Value; _lru.RemoveLast(); _tiles.Remove(old); _side.Remove(old); }
        return (data, side);
    }

    static double Sample(short[] d, int Size, double fy, double fx)
    {
        // fy: righe dall'alto (nord), fx: colonne da ovest; interpolazione bilineare, vuoti (−32768) ignorati
        int x0 = Math.Max(0, Math.Min(Size - 2, (int)fx)), y0 = Math.Max(0, Math.Min(Size - 2, (int)fy));
        double tx = fx - x0, ty = fy - y0, s = 0, w = 0;
        void Add(int y, int x, double ww)
        {
            short v = d[y * Size + x];
            if (v == short.MinValue || ww <= 0) return;
            s += ww * Math.Max(0, (int)v); w += ww;
        }
        Add(y0, x0, (1 - tx) * (1 - ty)); Add(y0, x0 + 1, tx * (1 - ty)); Add(y0 + 1, x0, (1 - tx) * ty); Add(y0 + 1, x0 + 1, tx * ty);
        return w > 0 ? s / w : 0;
    }

    /// <summary>
    /// Quote per un elenco di punti, caricando ogni riquadro una sola volta (i punti si raggruppano per riquadro).
    /// Dove il riquadro non esiste (mare aperto) la quota è 0; ok[i] = false se il riquadro non si è potuto leggere.
    /// </summary>
    public async Task<(double[] h, bool[] ok)> Heights(IList<LatLon> pts, CancellationToken ct)
    {
        var h = new double[pts.Count]; var ok = new bool[pts.Count];
        var groups = new Dictionary<(int, int), List<int>>();
        for (int i = 0; i < pts.Count; i++)
        {
            var key = ((int)Math.Floor(pts[i].Lat), (int)Math.Floor(pts[i].Lon));
            if (!groups.TryGetValue(key, out var l)) groups[key] = l = new List<int>();
            l.Add(i);
        }
        int done = 0;
        foreach (var g in groups)
        {
            ct.ThrowIfCancellationRequested();
            var (d, side) = await Tile(g.Key.Item1, g.Key.Item2, ct);
            foreach (var i in g.Value)
            {
                if (d == null) { h[i] = 0; ok[i] = _missingIsSea; continue; }
                double fy = (g.Key.Item1 + 1 - pts[i].Lat) * (side - 1), fx = (pts[i].Lon - g.Key.Item2) * (side - 1);
                h[i] = Sample(d, side, fy, fx); ok[i] = true;
            }
            done++;
            Progress?.Invoke($"SRTM1: {done} / {groups.Count} riquadri");
        }
        return (h, ok);
    }

    // un riquadro che il server non ha è quasi sempre mare aperto: quota 0 accettabile
    const bool _missingIsSea = true;

    public async Task<double> Height(LatLon p, CancellationToken ct) => (await Heights(new[] { p }, ct)).h[0];
}
