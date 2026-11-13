using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.Json;
using Avalonia.Media.Imaging;

namespace TropoVHF.Core;

/// <summary>Taratura di un tipo di mappa (Hepburn o F5LEN): dove scaricarla, come leggere pixel e colori.</summary>
public sealed class TropoCalibration
{
    public string Nome = "";
    public string Fonte = "";            // "hepburn" o "f5len"
    public bool Attivo;
    public List<(int ore, string url)> Passi = new();
    public List<(double x, double y, double lat, double lon)> Punti = new();
    public List<(byte r, byte g, byte b, double valore)> Legenda = new();
    public int Tolleranza = 40;
    public double ScalaA = 1, ScalaB = 0;   // valore finale = A · valore letto + B (F5LEN: unità N → 0–10)
    public string File = "";

    double[] _fx, _fy;   // pixel = f(lon, lat)
    public bool HasFit => _fx != null && _fy != null;

    public static TropoCalibration Load(string path)
    {
        using var doc = JsonDocument.Parse(System.IO.File.ReadAllBytes(path));
        var r = doc.RootElement;
        var c = new TropoCalibration { File = Path.GetFileName(path) };
        if (r.TryGetProperty("nome", out var v)) c.Nome = v.GetString() ?? "";
        if (r.TryGetProperty("fonte", out v)) c.Fonte = (v.GetString() ?? "").ToLowerInvariant();
        if (r.TryGetProperty("attivo", out v)) c.Attivo = v.ValueKind == JsonValueKind.True;
        if (r.TryGetProperty("tolleranza", out v) && v.ValueKind == JsonValueKind.Number) c.Tolleranza = v.GetInt32();
        if (r.TryGetProperty("scala_a", out v) && v.ValueKind == JsonValueKind.Number) c.ScalaA = v.GetDouble();
        if (r.TryGetProperty("scala_b", out v) && v.ValueKind == JsonValueKind.Number) c.ScalaB = v.GetDouble();
        if (r.TryGetProperty("passi", out v))
            foreach (var p in v.EnumerateArray())
                c.Passi.Add((p.GetProperty("ore").GetInt32(), p.GetProperty("url").GetString() ?? ""));
        if (r.TryGetProperty("punti_controllo", out v))
            foreach (var p in v.EnumerateArray())
                c.Punti.Add((p.GetProperty("x").GetDouble(), p.GetProperty("y").GetDouble(), p.GetProperty("lat").GetDouble(), p.GetProperty("lon").GetDouble()));
        if (r.TryGetProperty("legenda", out v))
            foreach (var p in v.EnumerateArray())
            {
                var hex = (p.GetProperty("colore").GetString() ?? "#000000").TrimStart('#');
                byte R = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
                byte G = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
                byte B = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
                c.Legenda.Add((R, G, B, p.GetProperty("valore").GetDouble()));
            }
        c.Fit();
        return c;
    }

    /// <summary>Scrive la taratura nel suo file JSON (cartella Tarature).</summary>
    public void Save(string folder)
    {
        Directory.CreateDirectory(folder);
        if (string.IsNullOrEmpty(File)) File = Fonte + ".json";
        var obj = new Dictionary<string, object>
        {
            ["nome"] = Nome, ["fonte"] = Fonte, ["attivo"] = Attivo, ["tolleranza"] = Tolleranza,
            ["scala_a"] = ScalaA, ["scala_b"] = ScalaB,
            ["passi"] = Passi.Select(p => new Dictionary<string, object> { ["ore"] = p.ore, ["url"] = p.url }).ToList(),
            ["punti_controllo"] = Punti.Select(p => new Dictionary<string, object> { ["x"] = p.x, ["y"] = p.y, ["lat"] = p.lat, ["lon"] = p.lon }).ToList(),
            ["legenda"] = Legenda.Select(l => new Dictionary<string, object> { ["colore"] = $"#{l.r:X2}{l.g:X2}{l.b:X2}", ["valore"] = l.valore }).ToList(),
        };
        var json = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
        System.IO.File.WriteAllText(Path.Combine(folder, File), json);
    }

    public bool Usable => Attivo && Legenda.Count > 1;

    public void Fit()
    {
        int n = Punti.Count;
        if (n < 3) return;
        bool quad = n >= 6;
        int m = quad ? 6 : 3;
        double[] Row(double lon, double lat) => quad
            ? new[] { 1, lon, lat, lon * lon, lat * lat, lon * lat }
            : new[] { 1, lon, lat };
        var A = Punti.Select(p => Row(p.lon, p.lat)).ToArray();
        _fx = LeastSquares(A, Punti.Select(p => p.x).ToArray(), m);
        _fy = LeastSquares(A, Punti.Select(p => p.y).ToArray(), m);
    }

    static double[] LeastSquares(double[][] A, double[] b, int m)
    {
        var N = new double[m, m + 1];
        for (int i = 0; i < A.Length; i++)
            for (int r = 0; r < m; r++)
            {
                for (int c = 0; c < m; c++) N[r, c] += A[i][r] * A[i][c];
                N[r, m] += A[i][r] * b[i];
            }
        for (int col = 0; col < m; col++)
        {
            int piv = col;
            for (int r = col + 1; r < m; r++) if (Math.Abs(N[r, col]) > Math.Abs(N[piv, col])) piv = r;
            if (Math.Abs(N[piv, col]) < 1e-12) return null;
            for (int c = 0; c <= m; c++) { var t = N[col, c]; N[col, c] = N[piv, c]; N[piv, c] = t; }
            for (int r = 0; r < m; r++)
            {
                if (r == col) continue;
                double f = N[r, col] / N[col, col];
                for (int c = col; c <= m; c++) N[r, c] -= f * N[col, c];
            }
        }
        var x = new double[m];
        for (int i = 0; i < m; i++) x[i] = N[i, m] / N[i, i];
        return x;
    }

    public (double x, double y) ToPixel(double lat, double lon)
    {
        double[] row = _fx.Length == 6 ? new[] { 1, lon, lat, lon * lon, lat * lat, lon * lat } : new[] { 1, lon, lat };
        double x = 0, y = 0;
        for (int i = 0; i < row.Length; i++) { x += _fx[i] * row[i]; y += _fy[i] * row[i]; }
        return (x, y);
    }

    public double ColorToValue(byte r, byte g, byte b)
    {
        double best = double.NaN; int bd = int.MaxValue;
        foreach (var l in Legenda)
        {
            int d = Math.Abs(l.r - r) + Math.Abs(l.g - g) + Math.Abs(l.b - b);
            if (d < bd) { bd = d; best = l.valore; }
        }
        return bd <= Tolleranza ? ScalaA * best + ScalaB : double.NaN;
    }
}

/// <summary>
/// Indice tropo mondiale. Ogni immagine scaricata diventa un TropoRaster collocato sulla Terra; il valore in un punto
/// si ricava, per ogni fonte, dalle immagini che coprono quel punto, scegliendo quella con l'ora valida più vicina
/// (a parità, la più dettagliata e poi la più recente). Dove nessuna immagine copre il punto il valore è NaN: nessun dato.
/// </summary>
public sealed class TropoIndex
{
    public List<TropoCalibration> Calibrations = new();
    public string Status = "Indice tropo: in attesa";
    public DateTime LastUpdateUtc;
    public event Action Changed;
    public event Action<string> Log;

    /// <summary>Quali regioni scaricare (impostazione dell'utente); di base quelle europee.</summary>
    public Func<TropoRegion, bool> RegionEnabled = r => r.DefaultOn;
    /// <summary>Estremi salvati (letti con l'OCR o scritti a mano) e loro salvataggio nelle impostazioni.</summary>
    public Func<TropoRegion, StoredExtents> RegionExtents = _ => null;
    public Action<TropoRegion, StoredExtents> SaveExtents = (_, _) => { };
    /// <summary>Stato di ogni regione all'ultimo aggiornamento (per le linguette Hepburn e F5LEN).</summary>
    public readonly Dictionary<string, string> RegionStatus = new();

    List<TropoRaster> _rasters = new();
    readonly Dictionary<(string, long), List<TropoRaster>> _cand = new();
    readonly object _lock = new();
    public IReadOnlyList<TropoRaster> Rasters { get { lock (_lock) return _rasters.ToList(); } }

    public static string Folder => Path.Combine(Settings.AppDir, "Tarature");

    public void LoadCalibrations()
    {
        var list = new List<TropoCalibration>();
        try
        {
            if (Directory.Exists(Folder))
                foreach (var f in Directory.GetFiles(Folder, "*.json"))
                {
                    try { list.Add(TropoCalibration.Load(f)); }
                    catch (Exception ex) { Log?.Invoke($"{Path.GetFileName(f)}: taratura non leggibile ({ex.Message})"); }
                }
        }
        catch { }
        Calibrations = list;   // sostituzione in un colpo solo: chi legge l'elenco non lo vede mai a metà
    }

    public bool HasData { get { lock (_lock) return _rasters.Count > 0; } }

    /// <summary>La regione con gli estremi salvati (OCR o a mano) se ci sono, altrimenti quelli di fabbrica.</summary>
    public TropoRegion Effective(TropoRegion r)
    {
        var e = Usable(r, RegionExtents(r));
        return e == null ? r : With(r, e.W, e.E, e.S, e.N, e.Merc);
    }

    /// <summary>
    /// Estremi salvati ancora validi: quelli scritti a mano valgono sempre; quelli dell'OCR solo se letti con le regole
    /// attuali ("ocr2") e solo per le regioni senza estremi di fabbrica (che sono misurati e hanno la precedenza).
    /// </summary>
    static StoredExtents Usable(TropoRegion r, StoredExtents e)
    {
        if (e == null) return null;
        if (e.Src == "manual") return e;
        if (e.Src != "ocr2") return null;          // letture OCR delle versioni precedenti: scartate e rilette
        return r.HasExtents ? null : e;
    }

    static TropoRegion With(TropoRegion r, double w, double e, double s, double n, bool merc) =>
        new() { Fonte = r.Fonte, Key = r.Key, Nome = r.Nome, PageUrl = r.PageUrl, Prefix = r.Prefix, Code = r.Code,
                LonW = w, LonE = e, LatS = s, LatN = n, Mercator = merc, DefaultOn = r.DefaultOn };

    static string Deg(double v, char pos, char neg) => Math.Abs(v).ToString("0.#", CultureInfo.InvariantCulture) + (v >= 0 ? pos : neg);
    public static string Describe(TropoRegion r) => !r.HasExtents ? "estremi non noti"
        : $"{Deg(r.LonW.Value, 'E', 'W')}–{Deg(r.LonE.Value > 180 ? r.LonE.Value - 360 : r.LonE.Value, 'E', 'W')}, {Deg(r.LatS.Value, 'N', 'S')}–{Deg(r.LatN.Value, 'N', 'S')}{(r.Mercator ? ", Mercatore" : "")}";

    /// <summary>Scarica un'immagine e ne restituisce i byte con l'ora di produzione (Last-Modified, se c'è).</summary>
    public static async Task<(byte[] bytes, DateTime produced)> Download(HttpClient http, string url)
    {
        using var resp = await http.GetAsync(url);
        resp.EnsureSuccessStatusCode();
        var bytes = await resp.Content.ReadAsByteArrayAsync();
        var lm = resp.Content.Headers.LastModified;
        return (bytes, lm.HasValue ? lm.Value.UtcDateTime : DateTime.UtcNow);
    }

    public async Task UpdateAsync(HttpClient http)
    {
        LoadCalibrations();
        var fresh = new List<TropoRaster>();
        var problems = new List<string>();
        foreach (var fonte in new[] { "hepburn", "f5len" })
        {
            var cal = Calibrations.FirstOrDefault(c => c.Fonte == fonte);
            if (cal == null || !cal.Attivo || cal.Legenda.Count < 2) continue;
            foreach (var reg0 in TropoSources.For(fonte).Where(r => RegionEnabled(r)))
            {
                var stored = Usable(reg0, RegionExtents(reg0));
                var reg = Effective(reg0);
                List<TropoImageRef> refs;
                try { refs = await TropoSources.ListImages(http, reg); }
                catch (Exception ex) { problems.Add($"{fonte} {reg.Nome}: {ex.Message}"); RegionStatus[reg0.Key] = ex.Message; continue; }
                int ok = 0; bool axesDone = false; string axes = "";
                var mine = new List<TropoRaster>();
                foreach (var im in refs)
                {
                    try
                    {
                        var (bytes, produced) = await Download(http, im.Url);
                        var cache = Path.Combine(Settings.DataDir, $"tropo_{reg.Key}_{im.Hours:000}.png");
                        try { await File.WriteAllBytesAsync(cache, bytes); } catch { }
                        var img = ReadPixels(bytes);
                        var frame = TropoSources.FindFrame(img);
                        if (!axesDone && frame is { } fr)
                        {
                            // assi: alla prima immagine della regione, se non sono mai stati letti o se la cornice è cambiata
                            axesDone = true;
                            int fw = fr.X1 - fr.X0, fh = fr.Y1 - fr.Y0;
                            bool changed = stored != null && stored.Src == "ocr2" && (Math.Abs(stored.FW - fw) > 3 || Math.Abs(stored.FH - fh) > 3);
                            // l'OCR serve solo alle regioni senza estremi di fabbrica
                            if (AxisReader.Ocr != null && !reg0.HasExtents && (stored == null || changed))
                            {
                                var res = await AxisReader.ReadAsync(img, fr);
                                if (res != null)
                                {
                                    var se = new StoredExtents(res.LonW, res.LonE, res.LatS, res.LatN, res.Mercator, fw, fh, "ocr2");
                                    SaveExtents(reg0, se); stored = se;
                                    reg = With(reg0, se.W, se.E, se.S, se.N, se.Merc);
                                    axes = $"assi letti con l'OCR ({res.LatLabels}+{res.LonLabels} etichette): ";
                                    Log?.Invoke($"{DateTime.UtcNow:HH:mm:ss}  {reg.Nome}: assi letti {Describe(reg)}");
                                }
                                else
                                {
                                    var seenTxt = AxisReader.LastText.Length > 120 ? AxisReader.LastText.Substring(0, 120) + "…" : AxisReader.LastText;
                                    axes = $"assi non letti dall'OCR (testo letto: «{seenTxt}»): ";
                                }
                            }
                            else if (stored != null) axes = stored.Src == "ocr2" ? "assi letti con l'OCR: " : "estremi scritti a mano: ";
                            else if (reg0.HasExtents) axes = "estremi di fabbrica: ";
                        }
                        if (!reg.HasExtents && !cal.HasFit) { problems.Add($"{fonte} {reg.Nome}: estremi da inserire"); break; }
                        var r = TropoRaster.Build(cal, reg, img, frame, out var why);
                        if (r == null) { problems.Add($"{fonte} {reg.Nome} +{im.Hours} h: {why}"); continue; }
                        r.Url = im.Url; r.Hours = im.Hours; r.FetchedUtc = DateTime.UtcNow;
                        r.RunUtc = im.RunUtc ?? TropoSources.EstimateRun(fonte, produced);
                        r.ValidUtc = r.RunUtc.AddHours(im.Hours);
                        mine.Add(r); ok++;
                    }
                    catch (Exception ex) { problems.Add($"{fonte} {reg.Nome} +{im.Hours} h: {ex.Message}"); }
                }
                fresh.AddRange(mine);
                // ogni regione appena letta va subito sulla mappa, senza aspettare le altre
                lock (_lock) { _rasters = _rasters.Where(x => x.Region != reg0.Key).Concat(mine).ToList(); _cand.Clear(); }
                var run = mine.Count > 0 ? $" · corsa {mine[0].RunUtc:dd/MM HH}Z" : "";
                RegionStatus[reg0.Key] = $"{axes}{Describe(reg)} · {ok}/{refs.Count} mappe{run}";
                Log?.Invoke($"{DateTime.UtcNow:HH:mm:ss}  {(fonte == "hepburn" ? "Hepburn" : "F5LEN")} {reg.Nome}: {ok}/{refs.Count} mappe lette");
                Changed?.Invoke();
            }
        }
        foreach (var p in problems.Take(12)) Log?.Invoke($"{DateTime.UtcNow:HH:mm:ss}  {p}");
        lock (_lock)
        {
            var enabled = TropoSources.Regions.Where(r => RegionEnabled(r)).Select(r => r.Key).ToHashSet();
            _rasters = _rasters.Where(r => enabled.Contains(r.Region)).ToList();   // regioni spente: fuori dalla mappa
            _cand.Clear();
        }
        LastUpdateUtc = DateTime.UtcNow;
        Status = fresh.Count > 0
            ? $"Indice tropo: {fresh.Count} mappe lette alle {LastUpdateUtc:HH:mm} UTC"
            : (Calibrations.Any(c => c.Attivo) ? "Indice tropo: nessuna mappa letta" : "Indice tropo: fonti non attive (linguette Hepburn e F5LEN)");
        Changed?.Invoke();
    }

    /// <summary>Pixel di un'immagine: larghezza, altezza, riga in byte, dati, ordine RGBA (altrimenti BGRA).</summary>
    public static (int w, int h, int stride, byte[] px, bool rgba) ReadPixels(byte[] png)
    {
        using var ms = new MemoryStream(png);
        using var wb = WriteableBitmap.Decode(ms);
        int w = wb.PixelSize.Width, h = wb.PixelSize.Height;
        using var fb = wb.Lock();
        var px = new byte[fb.RowBytes * h];
        Marshal.Copy(fb.Address, px, 0, px.Length);
        return (w, h, fb.RowBytes, px, fb.Format.ToString().StartsWith("Rgba", StringComparison.OrdinalIgnoreCase));
    }

    public static (byte r, byte g, byte b) PixelAt((int w, int h, int stride, byte[] px, bool rgba) img, int x, int y)
    {
        x = Math.Clamp(x, 0, img.w - 1); y = Math.Clamp(y, 0, img.h - 1);
        int o = y * img.stride + x * 4;
        return img.rgba ? (img.px[o], img.px[o + 1], img.px[o + 2]) : (img.px[o + 2], img.px[o + 1], img.px[o]);
    }

    /// <summary>Valore in un punto: fonte "hepburn", "f5len" o "media"; ora valida in UTC.</summary>
    public double Value(string fonte, double lat, double lon, DateTime utc)
    {
        if (fonte == "media")
        {
            double h = Value("hepburn", lat, lon, utc), f = Value("f5len", lat, lon, utc);
            if (double.IsNaN(h)) return f;
            if (double.IsNaN(f)) return h;
            return (h + f) / 2;
        }
        // candidati per fonte e ora, già ordinati: nella zona di dettaglio vale la mappa di dettaglio
        var key = (fonte, utc.Ticks / TimeSpan.TicksPerMinute);
        List<TropoRaster> cand;
        lock (_lock)
        {
            if (!_cand.TryGetValue(key, out cand))
            {
                cand = _rasters.Where(r => r.Fonte == fonte && Math.Abs((r.ValidUtc - utc).TotalHours) <= 6.01)
                    // prima la mappa di dettaglio, se la sua ora è entro 1,5 h; poi l'ora più vicina; poi la corsa più recente
                    .OrderBy(r => Math.Abs((r.ValidUtc - utc).TotalHours) <= 1.51 ? 0 : 1)
                    .ThenBy(r => Math.Round(r.DegPerPx, 4))
                    .ThenBy(r => Math.Round(Math.Abs((r.ValidUtc - utc).TotalHours), 2))
                    .ThenByDescending(r => r.RunUtc).ToList();
                if (_cand.Count > 64) _cand.Clear();
                _cand[key] = cand;
            }
        }
        TropoRaster best = null;
        foreach (var r in cand) if (r.Covers(lat, lon)) { best = r; break; }
        return best == null ? double.NaN : best.Sample(lat, lon);
    }
}
