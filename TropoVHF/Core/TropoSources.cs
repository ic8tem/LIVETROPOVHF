using System.Globalization;
using System.Text.RegularExpressions;

namespace TropoVHF.Core;

/// <summary>
/// Una regione di una fonte di mappe tropo. Le immagini di ogni regione seguono uno schema fisso:
/// F5LEN  …/wp-content/tools/Tropo/maps/{prefisso}{ore}.png        (03 … 192)
/// Hepburn …/tr_map/fcst/{codice}{ore a 3 cifre}.png?v{AAAAMMGG}     (006 … 144, corsa delle 12 UTC del giorno indicato)
/// Gli estremi lat/lon della cornice, quando noti, permettono di collocare l'immagine sulla griglia mondiale senza punti di controllo.
/// </summary>
public sealed class TropoRegion
{
    public string Fonte = "";        // "hepburn" o "f5len"
    public string Key = "";          // chiave unica, usata nelle impostazioni
    public string Nome = "";
    public string PageUrl = "";
    public string Prefix;            // F5LEN: prefisso del nome file ("" per l'Europa)
    public string Code;              // Hepburn: codice del nome file (eur, nwe…); null = lo ricava dalla pagina
    public double? LonW, LonE, LatS, LatN;
    public bool Mercator;            // latitudine in proiezione di Mercatore (altrimenti griglia regolare)
    public bool DefaultOn;
    public bool HasExtents => LonW.HasValue && LonE.HasValue && LatS.HasValue && LatN.HasValue;
}

public sealed record TropoImageRef(string Url, int Hours, DateTime? RunUtc);

/// <summary>Estremi salvati di una regione: letti dagli assi con l'OCR ("ocr") o scritti a mano ("manual"), con la cornice di allora.</summary>
public sealed record StoredExtents(double W, double E, double S, double N, bool Merc, int FW, int FH, string Src)
{
    public override string ToString() => string.Join(";", new[] { W, E, S, N }.Select(v => v.ToString("0.###", CultureInfo.InvariantCulture)))
        + $";{(Merc ? 1 : 0)};{FW};{FH};{Src}";

    public static StoredExtents Parse(string s)
    {
        var p = (s ?? "").Split(';');
        if (p.Length < 4) return null;
        var v = new double[4];
        for (int i = 0; i < 4; i++)
            if (!double.TryParse(p[i], NumberStyles.Float, CultureInfo.InvariantCulture, out v[i])) return null;
        int fw = p.Length > 5 && int.TryParse(p[5], out var a) ? a : 0, fh = p.Length > 6 && int.TryParse(p[6], out var b) ? b : 0;
        return new StoredExtents(v[0], v[1], v[2], v[3], p.Length > 4 && p[4] == "1", fw, fh, p.Length > 7 ? p[7] : "manual");
    }
}

public static class TropoSources
{
    public const string F5Maps = "https://tropo.f5len.org/wp-content/tools/Tropo/maps/";

    /// <summary>Ore di previsione di F5LEN: ogni 3 h fino a 48, ogni 6 fino a 96 (manca la 66), poi ogni 12 fino a 192.</summary>
    public static readonly int[] F5Hours =
        Enumerable.Range(1, 16).Select(i => i * 3)
        .Concat(new[] { 54, 60, 72, 78, 84, 90, 96 })
        .Concat(Enumerable.Range(0, 8).Select(i => 108 + i * 12)).ToArray();

    /// <summary>Ore di previsione di Hepburn: ogni 3 h da 6 a 42, poi ogni 6 h da 48 a 144.</summary>
    public static readonly int[] HepHours =
        Enumerable.Range(2, 13).Select(i => i * 3)
        .Concat(Enumerable.Range(0, 17).Select(i => 48 + i * 6)).ToArray();

    // Estremi misurati sulle immagini inviate dall'autore del programma (9/11/2026). Le altre regioni si tarano
    // scrivendo gli estremi nella linguetta della fonte, oppure con i punti di controllo.
    const string Hep = "https://www.dxinfocentre.com/tropo_";

    public static readonly List<TropoRegion> Regions = new()
    {
        new() { Fonte = "hepburn", Key = "hep_eur", Nome = "Europe",              PageUrl = Hep + "eur.html", Code = "eur", LonW = -10, LonE = 40, LatS = 27, LatN = 56, DefaultOn = true },
        new() { Fonte = "hepburn", Key = "hep_nwe", Nome = "NW Europe",           PageUrl = Hep + "nwe.html", Code = "nwe", LonW = -24.6, LonE = 25.9, LatS = 42.05, LatN = 71.55, DefaultOn = true },
        new() { Fonte = "hepburn", Key = "hep_eeu", Nome = "E Europe",            PageUrl = Hep + "eeu.html", Code = "eeu", LonW = 12.95, LonE = 67.9, LatS = 39.55, LatN = 71.5, DefaultOn = true },
        new() { Fonte = "hepburn", Key = "hep_ent", Nome = "Ern No Atlantic",     PageUrl = Hep + "ent.html", Code = "ent", LonW = -32.1, LonE = 36.9, LatS = 14.5, LatN = 55.1, DefaultOn = true },
        new() { Fonte = "hepburn", Key = "hep_nsa", Nome = "Nrn So America",      PageUrl = Hep + "nsa.html", Code = "nsa", LonW = -84.5, LonE = -34.6, LatS = -8.45, LatN = 20.55, DefaultOn = true },
        new() { Fonte = "hepburn", Key = "hep_sam", Nome = "South America",       PageUrl = Hep + "sam.html", Code = "sam", LonW = -89.55, LonE = -27.65, LatS = -41.45, LatN = -5.35, DefaultOn = true },
        new() { Fonte = "hepburn", Key = "hep_nat", Nome = "North Atlantic",      PageUrl = Hep + "nat.html", Code = "nat", LonW = -94.05, LonE = 16.75, LatS = -5.9, LatN = 58.6, DefaultOn = true },
        new() { Fonte = "hepburn", Key = "hep_eam", Nome = "Ern No America",      PageUrl = Hep + "eam.html", Code = "eam", LonW = -102.55, LonE = -52.6, LatS = 24.55, LatN = 53.55, DefaultOn = true },
        new() { Fonte = "hepburn", Key = "hep_mid", Nome = "Middle East",         PageUrl = Hep + "mid.html", Code = "mid", LonW = 20.95, LonE = 83.85, LatS = 5.55, LatN = 42.1, DefaultOn = true },
        new() { Fonte = "hepburn", Key = "hep_nca", Nome = "No Cntrl Asia",       PageUrl = Hep + "nca.html", Code = "nca", LonW = 45.05, LonE = 109.3, LatS = 35.1, LatN = 72.55, DefaultOn = true },
        new() { Fonte = "hepburn", Key = "hep_sea", Nome = "Southeast Asia",      PageUrl = Hep + "sea.html", Code = "sea", LonW = 72.9, LonE = 134.9, LatS = -10.5, LatN = 25.6, DefaultOn = true },
        new() { Fonte = "hepburn", Key = "hep_eas", Nome = "Far East",            PageUrl = Hep + "eas.html", Code = "eas", LonW = 100.35, LonE = 158.9, LatS = 19.5, LatN = 53.6, DefaultOn = true },
        new() { Fonte = "f5len",   Key = "f5_eu",   Nome = "Europe",              PageUrl = "https://tropo.f5len.org/forecasts-for-europe/", Prefix = "", LonW = -15, LonE = 40, LatS = 30, LatN = 65, DefaultOn = true },
        new() { Fonte = "f5len",   Key = "f5_atl",  Nome = "Atlantic D4–EI",      PageUrl = "https://tropo.f5len.org/atlantic-ocean/atlantic-d4-ei/", Prefix = "atl", LonW = -30.05, LonE = -0.05, LatS = 15, LatN = 52.1, DefaultOn = true },
        new() { Fonte = "f5len",   Key = "f5_eakp4",Nome = "Atlantic EA–KP4",     PageUrl = "", Prefix = "atleakp4", LonW = -70.05, LonE = -5.1, LatS = 10, LatN = 40.05, DefaultOn = true },
        new() { Fonte = "f5len",   Key = "f5_d4fy", Nome = "Atlantic D4–FY",      PageUrl = "", Prefix = "atld4fy", LonW = -69.9, LonE = -12.15, LatS = 0, LatN = 30.1, DefaultOn = true },
        new() { Fonte = "f5len",   Key = "f5_vp",   Nome = "VP (Caribbean)",      PageUrl = "", Prefix = "vp", LonW = -100.1, LonE = -60.05, LatS = 8.05, LatN = 30.05, DefaultOn = true },
        new() { Fonte = "f5len",   Key = "f5_naw",  Nome = "North America W",     PageUrl = "", Prefix = "naw", LonW = -125.1, LonE = -90.05, LatS = 25.05, LatN = 50.1, DefaultOn = true },
        new() { Fonte = "f5len",   Key = "f5_nakh6",Nome = "North America – KH6", PageUrl = "", Prefix = "nakh6", DefaultOn = true },
        new() { Fonte = "f5len",   Key = "f5_ja",   Nome = "Japan – Korea",       PageUrl = "", Prefix = "ja", LonW = 109.9, LonE = 144.9, LatS = 20.05, LatN = 45.1, DefaultOn = true },
        new() { Fonte = "f5len",   Key = "f5_vk",   Nome = "Australia",           PageUrl = "https://tropo.f5len.org/australia/", Prefix = "vk", LonW = 109.75, LonE = 179.9, LatS = -47.95, LatN = -4.85, DefaultOn = true },
    };

    public static IEnumerable<TropoRegion> For(string fonte) => Regions.Where(r => r.Fonte == fonte);

    public static string F5Url(string prefix, int h) => F5Maps + prefix + h.ToString("00", CultureInfo.InvariantCulture) + ".png";

    /// <summary>
    /// Ora di previsione dal nome del file. Con il prefisso noto lo toglie e legge il resto; altrimenti prova le ultime
    /// 3, 2 e 1 cifre tenendo solo un'ora valida per la fonte: così "atleakp406" dà 6 e non 406.
    /// </summary>
    public static int HoursFromName(string fonte, string url, string prefix = null)
    {
        string name;
        try { name = Path.GetFileNameWithoutExtension(new Uri(url).AbsolutePath); } catch { name = Path.GetFileNameWithoutExtension(url); }
        var valid = fonte == "hepburn" ? HepHours : F5Hours;
        if (prefix != null && name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
            && int.TryParse(name.Substring(prefix.Length), NumberStyles.None, CultureInfo.InvariantCulture, out var hp)) return hp;
        var m = Regex.Match(name, @"(\d+)$");
        if (!m.Success) return -1;
        var d = m.Groups[1].Value;
        for (int len = Math.Min(3, d.Length); len >= 1; len--)
            if (int.TryParse(d.Substring(d.Length - len), NumberStyles.None, CultureInfo.InvariantCulture, out var h) && valid.Contains(h)) return h;
        return -1;
    }

    static readonly Regex ReHep = new(@"tr_map/fcst/([a-z]+)(\d{3})\.png(?:\?v(\d{8}))?", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    /// <summary>Elenco delle immagini di una regione con la loro ora di previsione (e, per Hepburn, la corsa se la pagina la indica).</summary>
    public static async Task<List<TropoImageRef>> ListImages(HttpClient http, TropoRegion r)
    {
        if (r.Fonte == "f5len" && r.Prefix != null)
            return F5Hours.Select(h => new TropoImageRef(F5Url(r.Prefix, h), h, null)).ToList();

        var list = new List<TropoImageRef>();
        if (r.Fonte == "hepburn")
        {
            string html = null;
            try { if (!string.IsNullOrEmpty(r.PageUrl)) html = await http.GetStringAsync(r.PageUrl); } catch { }
            if (html != null)
            {
                var matches = ReHep.Matches(html).Cast<Match>().ToList();
                var code = r.Code ?? matches.GroupBy(m => m.Groups[1].Value.ToLowerInvariant()).OrderByDescending(g => g.Count()).Select(g => g.Key).FirstOrDefault();
                var baseUri = new Uri(r.PageUrl);
                foreach (var m in matches.Where(m => string.Equals(m.Groups[1].Value, code, StringComparison.OrdinalIgnoreCase)))
                {
                    int h = int.Parse(m.Groups[2].Value, CultureInfo.InvariantCulture);
                    DateTime? run = null;
                    if (m.Groups[3].Success && DateTime.TryParseExact(m.Groups[3].Value, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out var d))
                        run = DateTime.SpecifyKind(d.Date.AddHours(12), DateTimeKind.Utc);
                    var url = new Uri(baseUri, m.Value).ToString();
                    if (!list.Any(x => x.Url == url)) list.Add(new TropoImageRef(url, h, run));
                }
            }
            if (list.Count == 0 && r.Code != null)
            {
                // pagina non raggiungibile: stessi nomi, con la data della corsa letta da un'altra pagina Hepburn
                var run = await HepburnRun(http);
                var v = run.HasValue ? "?v" + run.Value.ToString("yyyyMMdd", CultureInfo.InvariantCulture) : "";
                list = HepHours.Select(h => new TropoImageRef($"https://www.dxinfocentre.com/tr_map/fcst/{r.Code}{h:000}.png{v}", h, run)).ToList();
            }
        }
        return list.OrderBy(x => x.Hours).ToList();
    }

    static (DateTime when, DateTime? run) _hepRun;

    /// <summary>
    /// Corsa di Hepburn (12 UTC del giorno indicato dalla variabile ?vAAAAMMGG): è la stessa per tutte le regioni,
    /// quindi basta leggerla da una qualsiasi pagina Hepburn raggiungibile. Ricordata per 30 minuti.
    /// </summary>
    public static async Task<DateTime?> HepburnRun(HttpClient http)
    {
        if (DateTime.UtcNow - _hepRun.when < TimeSpan.FromMinutes(30) && _hepRun.run.HasValue) return _hepRun.run;
        foreach (var r in For("hepburn").Where(x => !string.IsNullOrEmpty(x.PageUrl)))
        {
            try
            {
                var html = await http.GetStringAsync(r.PageUrl);
                var m = ReHep.Matches(html).Cast<Match>().FirstOrDefault(x => x.Groups[3].Success);
                if (m != null && DateTime.TryParseExact(m.Groups[3].Value, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out var d))
                {
                    var run = DateTime.SpecifyKind(d.Date.AddHours(12), DateTimeKind.Utc);
                    _hepRun = (DateTime.UtcNow, run);
                    return run;
                }
            }
            catch { }
        }
        return null;
    }

    /// <summary>
    /// Corsa del modello quando il nome del file non la dice: dall'ora di produzione dell'immagine (Last-Modified).
    /// F5LEN: 4 corse al giorno, l'immagine esce 4–10 ore dopo la corsa (es. corsa 00Z, prodotta alle 07:39).
    /// Hepburn: una corsa al giorno alle 12Z, pubblicata verso le 18 UTC.
    /// </summary>
    public static DateTime EstimateRun(string fonte, DateTime producedUtc)
    {
        if (fonte == "hepburn")
        {
            var t = producedUtc.AddHours(-4);
            var d = t.Date.AddHours(12);
            return d > t ? d.AddDays(-1) : d;
        }
        var u = producedUtc.AddHours(-4);
        return u.Date.AddHours(Math.Floor(u.Hour / 6.0) * 6);
    }

    // ------------------------------------------------------------------ cornice dell'immagine

    public readonly record struct Frame(int X0, int Y0, int X1, int Y1);

    /// <summary>
    /// Trova il rettangolo della cornice: due linee orizzontali sottili dello stesso colore, con gli stessi estremi,
    /// lunghe tra il 40% e il 97% della larghezza, unite da due linee verticali (colori simili, piccole interruzioni ammesse). Funziona sia con la cornice bianca
    /// su fondo nero di Hepburn sia con quella scura di F5LEN.
    /// </summary>
    public static Frame? FindFrame((int w, int h, int stride, byte[] px, bool rgba) img)
    {
        int W = img.w, H = img.h;
        // colori "simili" (scarto massimo 60 per canale): le immagini ridimensionate hanno linee sfumate nel colore vicino
        bool Near((byte r, byte g, byte b) p, (byte r, byte g, byte b) q) =>
            Math.Abs(p.r - q.r) <= 60 && Math.Abs(p.g - q.g) <= 60 && Math.Abs(p.b - q.b) <= 60;
        (byte r, byte g, byte b) P(int x, int y) => TropoIndex.PixelAt(img, x, y);

        var lines = new List<(int y, int x0, int x1, (byte r, byte g, byte b) col)>();
        for (int y = 1; y < H - 1; y++)
        {
            int x = 0;
            while (x < W)
            {
                var col = P(x, y); int x0 = x, thin = 0, gap = 0, last = x;
                while (x < W)
                {
                    if (Near(P(x, y), col))
                    {
                        gap = 0; last = x;
                        if (!Near(P(x, y - 1), col) || !Near(P(x, y + 1), col)) thin++;
                    }
                    else if (++gap > 3) break;      // interruzioni brevi ammesse (scritte, tacche)
                    x++;
                }
                x = last + 1;
                int len = last - x0 + 1;
                if (len >= W * 0.4 && len <= W * 0.97 && thin >= len * 0.7) lines.Add((y, x0, last, col));
            }
        }
        Frame? best = null; int bestH = 0;
        for (int i = 0; i < lines.Count; i++)
            for (int j = i + 1; j < lines.Count; j++)
            {
                var a = lines[i]; var b = lines[j];
                // le etichette che toccano la cornice ("30N") allungano le linee: si tollerano estremi diversi fino a 40 pixel
                if (!Near(a.col, b.col) || Math.Abs(a.x0 - b.x0) > 40 || Math.Abs(a.x1 - b.x1) > 40) continue;
                int hgt = b.y - a.y;
                if (hgt < H * 0.3 || hgt <= bestH) continue;
                // lati verticali: la prima colonna, partendo dall'estremo, che per almeno metà altezza ha il colore della cornice
                bool IsSide(int x)
                {
                    if (x < 0 || x >= W) return false;
                    int n = 0;
                    for (int y = a.y; y <= b.y; y++) if (Near(P(x, y), a.col)) n++;
                    return n >= hgt * 0.5;
                }
                int? L = null, R = null;
                int lx = Math.Min(a.x0, b.x0), rx = Math.Max(a.x1, b.x1);
                for (int x = lx - 2; x < lx + 60 && L == null; x++) if (IsSide(x)) L = x;
                for (int x = rx + 2; x > rx - 60 && R == null; x--) if (IsSide(x)) R = x;
                if (L == null || R == null) continue;
                best = new Frame(L.Value, a.y, R.Value, b.y); bestH = hgt;
            }
        return best;
    }
}

/// <summary>
/// Un'immagine di previsione collocata sulla Terra: valori 0–10 per pixel, cornice, estremi lat/lon e ora valida.
/// Fuori dalla cornice non dice niente (nessun dato); dentro, un pixel senza colore di legenda vale 0 (atmosfera normale).
/// </summary>
public sealed class TropoRaster
{
    public string Fonte = "", Region = "", Url = "";
    public int Hours;
    public DateTime RunUtc, ValidUtc, FetchedUtc;
    public int W, H;
    public byte[] V;                         // 255 = non è un colore di legenda; altrimenti (valore + 2) · 20
    public TropoSources.Frame Frame;
    public double LonW, LonE, LatS, LatN;
    public bool Mercator;
    public TropoCalibration Fit;             // se non ci sono gli estremi: punti di controllo

    public double DegPerPx => Fit != null ? 0.05 : (LonE - LonW) / Math.Max(1, Frame.X1 - Frame.X0);

    public static byte Encode(double v) => double.IsNaN(v) ? (byte)255 : (byte)Math.Clamp(Math.Round((v + 2) * 20), 0, 254);
    public static double Decode(byte b) => b == 255 ? double.NaN : b / 20.0 - 2;

    bool ToPixel(double lat, double lon, out int x, out int y)
    {
        x = y = 0;
        if (Fit != null)
        {
            var (fx, fy) = Fit.ToPixel(lat, lon);
            x = (int)Math.Round(fx); y = (int)Math.Round(fy);
        }
        else
        {
            if (lon < LonW) lon += 360;          // regioni a cavallo dell'antimeridiano
            if (lon < LonW || lon > LonE || lat < LatS || lat > LatN) return false;
            x = (int)Math.Round(Frame.X0 + (lon - LonW) / (LonE - LonW) * (Frame.X1 - Frame.X0));
            double fy = Mercator ? (M(LatN) - M(lat)) / (M(LatN) - M(LatS)) : (LatN - lat) / (LatN - LatS);
            y = (int)Math.Round(Frame.Y0 + fy * (Frame.Y1 - Frame.Y0));
        }
        return x > Frame.X0 && x < Frame.X1 && y > Frame.Y0 && y < Frame.Y1;
    }

    static double M(double lat) => Math.Log(Math.Tan(Math.PI / 4 + Math.Clamp(lat, -85, 85) * Math.PI / 360));

    public bool Covers(double lat, double lon) => ToPixel(lat, lon, out _, out _);

    /// <summary>Valore nel punto; NaN se il punto è fuori dalla cornice.</summary>
    public double Sample(double lat, double lon)
    {
        if (!ToPixel(lat, lon, out int x, out int y)) return double.NaN;
        // coste, confini e scritte non sono in legenda: si guarda il colore più vicino entro 3 pixel
        for (int rad = 0; rad <= 3; rad++)
            for (int dy = -rad; dy <= rad; dy++)
                for (int dx = -rad; dx <= rad; dx++)
                {
                    if (Math.Max(Math.Abs(dx), Math.Abs(dy)) != rad) continue;
                    int xx = x + dx, yy = y + dy;
                    if (xx <= Frame.X0 || yy <= Frame.Y0 || xx >= Frame.X1 || yy >= Frame.Y1) continue;
                    byte b = V[yy * W + xx];
                    if (b != 255) return Decode(b);
                }
        return 0;
    }

    /// <summary>Legge l'immagine: cornice, colori della legenda, georeferenziazione. Null se non è collocabile.</summary>
    public static TropoRaster Build(TropoCalibration cal, TropoRegion reg, byte[] png, out string why)
    {
        var img = TropoIndex.ReadPixels(png);
        return Build(cal, reg, img, TropoSources.FindFrame(img), out why);
    }

    public static TropoRaster Build(TropoCalibration cal, TropoRegion reg, (int w, int h, int stride, byte[] px, bool rgba) img, TropoSources.Frame? frame, out string why)
    {
        why = "";
        var r = new TropoRaster { Fonte = cal.Fonte, Region = reg?.Key ?? "", W = img.w, H = img.h };
        if (reg != null && reg.HasExtents && frame != null)
        {
            r.Frame = frame.Value;
            r.LonW = reg.LonW.Value; r.LonE = reg.LonE.Value; r.LatS = reg.LatS.Value; r.LatN = reg.LatN.Value; r.Mercator = reg.Mercator;
            if (r.LonE < r.LonW) r.LonE += 360;
        }
        else if (cal.HasFit)
        {
            r.Fit = cal;
            r.Frame = frame ?? new TropoSources.Frame(0, 0, img.w - 1, img.h - 1);
        }
        else
        {
            why = frame == null ? "cornice non trovata" : "estremi lat/lon della regione da inserire";
            return null;
        }
        r.V = new byte[img.w * img.h];
        for (int y = 0; y < img.h; y++)
            for (int x = 0; x < img.w; x++)
            {
                var (R, G, B) = TropoIndex.PixelAt(img, x, y);
                r.V[y * img.w + x] = Encode(cal.ColorToValue(R, G, B));
            }
        return r;
    }
}
