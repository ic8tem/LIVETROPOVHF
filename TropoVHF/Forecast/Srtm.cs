using System.IO.Compression;
using TropoVHF.Core;

namespace TropoVHF.Forecast;

/// <summary>
/// Quote del terreno a 1 secondo d'arco (circa 30 m), riquadri da 1°×1°.
/// Fonte principale (dalla 1.3.2): Copernicus DEM GLO-30 (ESA, DLR e Airbus), GeoTIFF a 32 bit in virgola mobile,
/// tassellato e compresso (deflate), sul registro aperto di AWS senza chiave:
/// https://copernicus-dem-30m.s3.amazonaws.com/Copernicus_DSM_COG_10_N40_00_E014_00_DEM/Copernicus_DSM_COG_10_N40_00_E014_00_DEM.tif
/// Sul mare vale 0 e la costa è netta. Un riquadro che non esiste (404) è mare aperto.
/// Riserva, se Copernicus non risponde: Terrain Tiles su AWS (formato «skadi», HGT 3601×3601). Attenzione: vicino alla
/// costa skadi fonde l'SRTM con dati del fondale a bassa risoluzione e crea «rilievi fantasma» di 80–100 m in mare
/// (verificato attorno a Capri il 21/11/2026): per questo non è più la fonte principale.
/// I riquadri scaricati restano in Dati/COP30 (e Dati/SRTM1 per la riserva). Le quote negative valgono 0.
/// </summary>
public sealed class Srtm
{
    public static string Folder { get { var d = Path.Combine(Settings.DataDir, "COP30"); try { Directory.CreateDirectory(d); } catch { } return d; } }
    public static string FolderSkadi { get { var d = Path.Combine(Settings.DataDir, "SRTM1"); try { Directory.CreateDirectory(d); } catch { } return d; } }
    public const string SourceName = "Copernicus GLO-30";

    /// <summary>Riquadro in memoria: quote in metri (interi), griglia e posizione dei centri dei pixel.</summary>
    sealed class Grid
    {
        public short[] D; public int W, H;
        public double Lon0, Lat0, Dx, Dy;   // centro del pixel (0,0) e passo in gradi
        public string From = "";
    }

    readonly HttpClient _http;
    readonly Dictionary<string, Grid> _tiles = new();
    readonly LinkedList<string> _lru = new();
    readonly HashSet<string> _missing = new();
    const int MaxTiles = 8;

    public int Downloaded, Missing;
    public Action<string> Progress;

    public Srtm(HttpClient http) { _http = http; }
    public HttpClient Http => _http;

    public static string TileName(int lat, int lon) =>
        (lat >= 0 ? "N" : "S") + Math.Abs(lat).ToString("00") + (lon >= 0 ? "E" : "W") + Math.Abs(lon).ToString("000");

    static string CopName(int lat, int lon) =>
        "Copernicus_DSM_COG_10_" + (lat >= 0 ? "N" : "S") + Math.Abs(lat).ToString("00") + "_00_" + (lon >= 0 ? "E" : "W") + Math.Abs(lon).ToString("000") + "_00_DEM";

    async Task<Grid> Tile(int lat, int lon, CancellationToken ct)
    {
        var name = TileName(lat, lon);
        if (_tiles.TryGetValue(name, out var t)) { _lru.Remove(name); _lru.AddFirst(name); return t; }
        if (_missing.Contains(name)) return null;
        Grid g = null;
        // 1) Copernicus GLO-30
        var file = Path.Combine(Folder, name + ".tif");
        var sea = Path.Combine(Folder, name + ".mare");
        if (File.Exists(sea)) { _missing.Add(name); return null; }
        bool copDown = false;
        if (!File.Exists(file))
        {
            Progress?.Invoke($"terreno: scarico {name} da Copernicus (15–30 MB, la prima volta)…");
            var cn = CopName(lat, lon);
            var url = $"https://copernicus-dem-30m.s3.amazonaws.com/{cn}/{cn}.tif";
            for (int attempt = 1; attempt <= 2 && !File.Exists(file); attempt++)
            try
            {
                using var resp = await _http.GetAsync(url, ct);
                if ((int)resp.StatusCode == 404)
                {
                    // Copernicus non pubblica i riquadri tutti di mare
                    try { File.WriteAllText(sea, "mare aperto"); } catch { }
                    CalcLog.W($"terreno: {name} non esiste in Copernicus (mare aperto), quota 0");
                    _missing.Add(name); return null;
                }
                if (!resp.IsSuccessStatusCode) { copDown = true; CalcLog.W($"terreno: Copernicus {name} HTTP {(int)resp.StatusCode}"); break; }
                var bytes = await resp.Content.ReadAsByteArrayAsync();
                var tmp = file + ".part";
                await Compat.WriteBytes(tmp, bytes, ct);
                Compat.MoveOver(tmp, file);
                Downloaded++;
                CalcLog.W($"terreno: scaricato {name} ({bytes.Length / 1024} kB) da {url}");
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) { throw; }
            catch (Exception ex)
            {
                CalcLog.W($"terreno: Copernicus {name} tentativo {attempt} non riuscito: {ex.Message}");
                if (attempt == 2) copDown = true;
            }
        }
        if (File.Exists(file))
        {
            try { g = await Task.Run(() => ReadGeoTiff(File.ReadAllBytes(file)), ct); g.From = SourceName; }
            catch (OperationCanceledException) { throw; }
            catch (Exception ex) { CalcLog.W($"terreno: {name} illeggibile ({ex.Message}), lo riscarico la prossima volta"); try { File.Delete(file); } catch { } g = null; copDown = true; }
        }
        // 2) riserva: Terrain Tiles «skadi»
        if (g == null && copDown) g = await Skadi(name, ct);
        if (g == null) { _missing.Add(name); Missing++; return null; }
        _tiles[name] = g; _lru.AddFirst(name);
        CalcLog.W($"terreno: letto {name} ({g.From}), {g.W}×{g.H} punti");
        while (_lru.Count > MaxTiles) { var old = _lru.Last.Value; _lru.RemoveLast(); _tiles.Remove(old); }
        return g;
    }

    async Task<Grid> Skadi(string name, CancellationToken ct)
    {
        var file = Path.Combine(FolderSkadi, name + ".hgt.gz");
        if (!File.Exists(file))
        {
            var url = $"https://s3.amazonaws.com/elevation-tiles-prod/skadi/{name.Substring(0, 3)}/{name}.hgt.gz";
            try
            {
                Progress?.Invoke($"terreno: Copernicus non risponde, scarico {name} dalla riserva (Terrain Tiles)…");
                using var resp = await _http.GetAsync(url, ct);
                if (!resp.IsSuccessStatusCode) return null;
                var bytes = await resp.Content.ReadAsByteArrayAsync();
                var tmp = file + ".part";
                await Compat.WriteBytes(tmp, bytes, ct);
                Compat.MoveOver(tmp, file);
                Downloaded++;
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) { throw; }
            catch { return null; }
        }
        try
        {
            using var fs = File.OpenRead(file);
            using var gz = new GZipStream(fs, CompressionMode.Decompress);
            using var ms = new MemoryStream();
            await gz.CopyToAsync(ms, 81920, ct);
            var b = ms.GetBuffer(); int n = (int)ms.Length, count = n / 2;
            int side = (int)Math.Round(Math.Sqrt(count));
            if (side < 100 || side * side != count) return null;
            var d = new short[count];
            for (int i = 0; i < count; i++) d[i] = (short)((b[2 * i] << 8) | b[2 * i + 1]);
            int lat = int.Parse(name.Substring(1, 2)) * (name[0] == 'S' ? -1 : 1), lon = int.Parse(name.Substring(4, 3)) * (name[3] == 'W' ? -1 : 1);
            CalcLog.W($"terreno: {name} dalla riserva Terrain Tiles: vicino alla costa può avere rilievi fantasma in mare");
            return new Grid { D = d, W = side, H = side, Lon0 = lon, Lat0 = lat + 1, Dx = 1.0 / (side - 1), Dy = 1.0 / (side - 1), From = "Terrain Tiles (riserva)" };
        }
        catch (OperationCanceledException) { throw; }
        catch { try { File.Delete(file); } catch { } return null; }
    }

    // ---- lettore GeoTIFF minimo: prima immagine (piena risoluzione), tasselli o strisce, deflate o nessuna compressione,
    // virgola mobile a 32 bit o interi a 16 bit, predittore 1/2/3. Posizione da ModelTiepoint e ModelPixelScale.
    static Grid ReadGeoTiff(byte[] b)
    {
        bool le = b[0] == 'I';
        if (!(b[0] == 'I' || b[0] == 'M')) throw new InvalidDataException("non è un TIFF");
        ushort U16(long o) => le ? (ushort)(b[o] | b[o + 1] << 8) : (ushort)(b[o] << 8 | b[o + 1]);
        uint U32(long o) => le ? (uint)(b[o] | b[o + 1] << 8 | b[o + 2] << 16 | b[o + 3] << 24) : (uint)(b[o] << 24 | b[o + 1] << 16 | b[o + 2] << 8 | b[o + 3]);
        double F64(long o)
        {
            var t = new byte[8]; Array.Copy(b, o, t, 0, 8);
            if (le != BitConverter.IsLittleEndian) Array.Reverse(t);
            return BitConverter.ToDouble(t, 0);
        }
        if (U16(2) != 42) throw new InvalidDataException("TIFF grande (BigTIFF) non gestito");
        long ifd = U32(4);
        int n = U16(ifd);
        int width = 0, height = 0, tw = 0, th = 0, rps = 0, comp = 1, pred = 1, bps = 16, fmt = 1, raster = 1;
        long[] offs = null, cnts = null; double[] scale = null, tie = null;
        int TypeSize(int ty) => ty switch { 1 or 2 or 6 or 7 => 1, 3 or 8 => 2, 4 or 9 or 11 => 4, 5 or 10 or 12 => 8, _ => 1 };
        long[] Longs(int ty, uint count, long valOff)
        {
            var r = new long[count];
            long p = count * TypeSize(ty) <= 4 ? valOff : U32(valOff);
            for (int i = 0; i < count; i++) r[i] = ty == 3 ? U16(p + 2 * i) : U32(p + 4 * i);
            return r;
        }
        double[] Doubles(uint count, long valOff) { long p = U32(valOff); var r = new double[count]; for (int i = 0; i < count; i++) r[i] = F64(p + 8 * i); return r; }
        for (int i = 0; i < n; i++)
        {
            long e = ifd + 2 + 12 * i;
            int tag = U16(e), ty = U16(e + 2); uint cnt = U32(e + 4); long vo = e + 8;
            long V() => ty == 3 ? U16(vo) : U32(vo);
            switch (tag)
            {
                case 256: width = (int)V(); break;
                case 257: height = (int)V(); break;
                case 258: bps = (int)(cnt == 1 ? V() : Longs(ty, cnt, vo)[0]); break;
                case 259: comp = (int)V(); break;
                case 273: case 324: offs = Longs(ty, cnt, vo); break;
                case 278: rps = (int)V(); break;
                case 279: case 325: cnts = Longs(ty, cnt, vo); break;
                case 317: pred = (int)V(); break;
                case 322: tw = (int)V(); break;
                case 323: th = (int)V(); break;
                case 339: fmt = (int)(cnt == 1 ? V() : Longs(ty, cnt, vo)[0]); break;
                case 33550: scale = Doubles(cnt, vo); break;
                case 33922: tie = Doubles(cnt, vo); break;
                case 34735:
                    {
                        var k = Longs(ty, cnt, vo);   // GeoKeyDirectory: cerca GTRasterTypeGeoKey (1025): 1 area, 2 punto
                        for (int j = 4; j + 3 < k.Length; j += 4) if (k[j] == 1025 && k[j + 1] == 0) raster = (int)k[j + 3];
                        break;
                    }
            }
        }
        if (width <= 0 || height <= 0 || offs == null || cnts == null || scale == null || tie == null) throw new InvalidDataException("tag GeoTIFF mancanti");
        if (comp != 1 && comp != 8 && comp != 32946) throw new InvalidDataException($"compressione {comp} non gestita");
        bool tiled = tw > 0 && th > 0;
        if (!tiled) { tw = width; th = rps > 0 ? rps : height; }
        int bytesPer = bps / 8;
        var d = new short[width * height];
        int across = (width + tw - 1) / tw;
        var buf = new byte[tw * th * bytesPer];
        for (int k = 0; k < offs.Length; k++)
        {
            int bx = tiled ? k % across : 0, by = tiled ? k / across : k;
            int x0 = bx * tw, y0 = by * th;
            if (y0 >= height) break;
            int got;
            if (comp == 1) { got = (int)Math.Min(buf.Length, cnts[k]); Array.Copy(b, offs[k], buf, 0, got); }
            else
            {
                // deflate con intestazione zlib (2 byte) che DeflateStream non vuole
                using var ms = new MemoryStream(b, (int)offs[k] + 2, (int)cnts[k] - 2);
                using var ds = new DeflateStream(ms, CompressionMode.Decompress);
                got = 0; int r;
                while (got < buf.Length && (r = ds.Read(buf, got, buf.Length - got)) > 0) got += r;
            }
            int rows = Math.Min(th, got / (tw * bytesPer));
            if (pred == 2) // differenze orizzontali su interi
                for (int y = 0; y < rows; y++) for (int x = 1; x < tw; x++)
                    { int o = (y * tw + x) * bytesPer, p = o - bytesPer; if (bytesPer == 2) { int v = (le ? buf[o] | buf[o + 1] << 8 : buf[o] << 8 | buf[o + 1]) + (le ? buf[p] | buf[p + 1] << 8 : buf[p] << 8 | buf[p + 1]); if (le) { buf[o] = (byte)v; buf[o + 1] = (byte)(v >> 8); } else { buf[o] = (byte)(v >> 8); buf[o + 1] = (byte)v; } } }
            float[] row3 = null;
            if (pred == 3) row3 = new float[tw];
            for (int y = 0; y < rows; y++)
            {
                int gy = y0 + y; if (gy >= height) break;
                if (pred == 3)
                {
                    // predittore in virgola mobile: somma dei byte, poi byte riordinati (più significativo prima)
                    int rb = tw * bytesPer, ro = y * rb;
                    for (int i = 1; i < rb; i++) buf[ro + i] = (byte)(buf[ro + i] + buf[ro + i - 1]);
                    var tmp = new byte[4];
                    for (int x = 0; x < tw; x++)
                    {
                        for (int s = 0; s < 4; s++) tmp[3 - s] = buf[ro + s * tw + x];
                        if (!BitConverter.IsLittleEndian) Array.Reverse(tmp);
                        row3[x] = BitConverter.ToSingle(tmp, 0);
                    }
                }
                for (int x = 0; x < tw; x++)
                {
                    int gx = x0 + x; if (gx >= width) break;
                    double v;
                    if (pred == 3) v = row3[x];
                    else
                    {
                        int o = (y * tw + x) * bytesPer;
                        if (fmt == 3 && bytesPer == 4)
                        {
                            var t4 = new byte[] { buf[o], buf[o + 1], buf[o + 2], buf[o + 3] };
                            if (le != BitConverter.IsLittleEndian) Array.Reverse(t4);
                            v = BitConverter.ToSingle(t4, 0);
                        }
                        else if (bytesPer == 2) v = (short)(le ? buf[o] | buf[o + 1] << 8 : buf[o] << 8 | buf[o + 1]);
                        else v = 0;
                    }
                    d[gy * width + gx] = double.IsNaN(v) || v < -1000 ? short.MinValue : (short)Math.Round(Math.Max(0, Math.Min(9000, v)));
                }
            }
        }
        // ModelTiepoint (i, j, k, X, Y, Z) e ModelPixelScale (sx, sy): centro del pixel (0,0)
        double sx = scale[0], sy = scale[1];
        double lon0 = tie[3] - tie[0] * sx, lat0 = tie[4] + tie[1] * sy;
        if (raster == 1) { lon0 += sx / 2; lat0 -= sy / 2; }   // PixelIsArea: il punto d'aggancio è l'angolo
        return new Grid { D = d, W = width, H = height, Lon0 = lon0, Lat0 = lat0, Dx = sx, Dy = sy };
    }

    static double Sample(Grid g, double lat, double lon)
    {
        double fx = (lon - g.Lon0) / g.Dx, fy = (g.Lat0 - lat) / g.Dy;
        fx = Math.Max(0, Math.Min(g.W - 1.000001, fx)); fy = Math.Max(0, Math.Min(g.H - 1.000001, fy));
        int x0 = Math.Min(g.W - 2, (int)fx), y0 = Math.Min(g.H - 2, (int)fy);
        double tx = fx - x0, ty = fy - y0, s = 0, w = 0;
        void Add(int y, int x, double ww)
        {
            short v = g.D[y * g.W + x];
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
        foreach (var gr in groups)
        {
            ct.ThrowIfCancellationRequested();
            var g = await Tile(gr.Key.Item1, gr.Key.Item2, ct);
            foreach (var i in gr.Value)
            {
                if (g == null) { h[i] = 0; ok[i] = true; continue; }   // riquadro inesistente: quasi sempre mare aperto
                h[i] = Sample(g, pts[i].Lat, pts[i].Lon); ok[i] = true;
            }
            done++;
            Progress?.Invoke($"terreno: {done} / {groups.Count} riquadri");
        }
        return (h, ok);
    }

    public async Task<double> Height(LatLon p, CancellationToken ct) => (await Heights(new[] { p }, ct)).h[0];

    /// <summary>Prova del lettore (riga di comando, CI): quote in punti noti.</summary>
    public static string SelfTest(byte[] tif, IEnumerable<(string name, double lat, double lon)> pts)
    {
        var g = ReadGeoTiff(tif);
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"GeoTIFF {g.W}×{g.H}, centro del primo pixel {g.Lat0:0.000000} N {g.Lon0:0.000000} E, passo {g.Dx * 3600:0.###}\"×{g.Dy * 3600:0.###}\"");
        foreach (var p in pts) sb.AppendLine($"  {p.name}: {Sample(g, p.lat, p.lon):0.0} m");
        return sb.ToString();
    }
}
