using System.Text.RegularExpressions;

namespace TropoVHF.Core;

/// <summary>Una parola letta dall'OCR, con il suo riquadro in pixel dell'immagine passata.</summary>
public sealed record OcrWord(string Text, double X, double Y, double W, double H);

/// <summary>Motore OCR della piattaforma (su Windows 10/11 quello integrato nel sistema).</summary>
public interface ITextReader
{
    int MaxDimension { get; }
    Task<List<OcrWord>> ReadAsync(byte[] bgra, int width, int height);
}

/// <summary>
/// Legge le etichette degli assi (54N, 10W, 0, 5E…) stampate attorno alla cornice di una mappa e ne ricava
/// gli estremi lat/lon della cornice e il tipo di proiezione (griglia regolare o Mercatore).
/// </summary>
public static class AxisReader
{
    /// <summary>Null dove l'OCR non c'è (Mac, Linux): lì gli estremi si scrivono a mano.</summary>
    public static ITextReader Ocr;
    /// <summary>Ultimo testo letto dall'OCR attorno a una cornice (per capire perché una lettura non è riuscita).</summary>
    public static string LastText = "";

    public sealed record Result(double LonW, double LonE, double LatS, double LatN, bool Mercator, int LatLabels, int LonLabels, string Text);

    enum Side { Left, Right, Top, Bottom }

    public static async Task<Result> ReadAsync((int w, int h, int stride, byte[] px, bool rgba) img, TropoSources.Frame f)
    {
        if (Ocr == null) return null;
        var lat = new List<(double p, double v)>();
        var lon = new List<(double p, double v)>();
        var seen = new List<string>();
        foreach (var side in new[] { Side.Left, Side.Bottom, Side.Right, Side.Top })
        {
            bool vertical = side == Side.Left || side == Side.Right;
            if (vertical && lat.Count >= 3) continue;
            if (!vertical && lon.Count >= 3) continue;
            int x0, y0, x1, y1;
            switch (side)
            {
                case Side.Left: x0 = 0; x1 = f.X0 - 2; y0 = f.Y0 - 12; y1 = f.Y1 + 12; break;
                case Side.Right: x0 = f.X1 + 2; x1 = img.w - 1; y0 = f.Y0 - 12; y1 = f.Y1 + 12; break;
                case Side.Top: x0 = f.X0 - 30; x1 = f.X1 + 30; y0 = f.Y0 - 24; y1 = f.Y0 - 2; break;
                default: x0 = f.X0 - 30; x1 = f.X1 + 30; y0 = f.Y1 + 2; y1 = f.Y1 + 24; break;
            }
            x0 = Math.Max(0, x0); y0 = Math.Max(0, y0); x1 = Math.Min(img.w - 1, x1); y1 = Math.Min(img.h - 1, y1);
            if (x1 - x0 < 8 || y1 - y0 < 6) continue;
            // un'etichetta alla volta: ritagliata, ingrandita ×6 e letta da sola (sulle strisce intere l'OCR legge male)
            foreach (var (bx0, by0, bx1, by1) in Labels(img, x0, y0, x1, y1, vertical))
            {
                string text;
                try
                {
                    var (buf, w, h, _, _) = Strip(img, Math.Max(0, bx0 - 2), Math.Max(0, by0 - 2), Math.Min(img.w - 1, bx1 + 2), Math.Min(img.h - 1, by1 + 2), Ocr.MaxDimension, 6);
                    var words = await Ocr.ReadAsync(buf, w, h);
                    text = string.Concat(words.Select(x => x.Text));
                }
                catch { continue; }
                if (text == "") continue;
                seen.Add(text);
                double pos = vertical ? (by0 + by1) / 2.0 : (bx0 + bx1) / 2.0;
                foreach (var (v, hemi) in Parse(text))
                    foreach (var c in Candidates(v, hemi, vertical))
                        (vertical ? lat : lon).Add((pos, c));
            }
        }
        // longitudini a cavallo dell'antimeridiano: 170E 180 170W → 170 180 190
        if (lon.Any(l => l.v < 0) && lon.Any(l => l.v > 0) && lon.Max(l => l.v) - lon.Min(l => l.v) > 180)
            lon = lon.Select(l => (l.p, l.v < 0 ? l.v + 360 : l.v)).ToList();

        // la longitudine cresce verso destra, la latitudine verso l'alto (pixel y verso il basso)
        LastText = string.Join(" ", seen);
        var fl = FitLinear(lon, +1);
        var latLin = FitLinear(lat, -1);
        var latMer = FitLinear(lat.Select(l => (l.p, Merc(l.v))).ToList(), -1, InvMerc);
        if (fl == null || (latLin == null && latMer == null)) return null;
        bool merc = latMer != null && (latLin == null || latMer.Value.rmsPx < latLin.Value.rmsPx * 0.7);
        var fy = merc ? latMer.Value : latLin.Value;
        double lonW = fl.Value.a + fl.Value.b * f.X0, lonE = fl.Value.a + fl.Value.b * f.X1;
        double yN = fy.a + fy.b * f.Y0, yS = fy.a + fy.b * f.Y1;
        double latN = merc ? InvMerc(yN) : yN, latS = merc ? InvMerc(yS) : yS;
        if (lonE <= lonW || latN <= latS || latN > 90.5 || latS < -90.5) return null;
        return new Result(Math.Round(lonW, 2), Math.Round(lonE, 2), Math.Round(latS, 2), Math.Round(latN, 2), merc, fy.n, fl.Value.n, string.Join(" ", seen));
    }

    static int Gcd(int a, int b) { a = Math.Abs(a); b = Math.Abs(b); while (b != 0) (a, b) = (b, a % b); return a; }

    static double Merc(double lat) => Math.Log(Math.Tan(Math.PI / 4 + Math.Clamp(lat, -85, 85) * Math.PI / 360)) * 180 / Math.PI;
    static double InvMerc(double y) => (2 * Math.Atan(Math.Exp(y * Math.PI / 180)) - Math.PI / 2) * 180 / Math.PI;

    /// <summary>
    /// Striscia delle etichette: testo nero su bianco, ingrandita (×4 se possibile) con interpolazione e poi soglia,
    /// perché l'OCR legga bene i caratteri piccoli; un bordo bianco attorno aiuta il riconoscimento.
    /// </summary>
    static (byte[] buf, int w, int h, int scale, int pad) Strip((int w, int h, int stride, byte[] px, bool rgba) img, int x0, int y0, int x1, int y1, int maxDim, int maxScale = 4)
    {
        const int pad = 20;
        int sw = x1 - x0 + 1, sh = y1 - y0 + 1;
        int scale = Math.Max(1, Math.Min(maxScale, (maxDim - 2 * pad - 1) / Math.Max(sw, sh)));
        var lum = new double[sw * sh];
        var hist = new int[256];
        for (int y = 0; y < sh; y++)
            for (int x = 0; x < sw; x++)
            {
                var (r, g, b) = TropoIndex.PixelAt(img, x0 + x, y0 + y);
                double l = r * 0.299 + g * 0.587 + b * 0.114;
                lum[y * sw + x] = l; hist[Math.Clamp((int)l, 0, 255)]++;
            }
        int bg = 0; for (int i = 1; i < 256; i++) if (hist[i] > hist[bg]) bg = i;     // colore di fondo = il più frequente
        double D(int x, int y) => Math.Abs(lum[Math.Clamp(y, 0, sh - 1) * sw + Math.Clamp(x, 0, sw - 1)] - bg);
        int W = sw * scale + 2 * pad, H = sh * scale + 2 * pad;
        var buf = new byte[W * H * 4];
        for (int i = 0; i < buf.Length; i++) buf[i] = 255;
        for (int y = 0; y < sh * scale; y++)
        {
            double fy = (y + 0.5) / scale - 0.5; int iy = (int)Math.Floor(fy); double ty = Math.Clamp(fy - iy, 0, 1);
            for (int x = 0; x < sw * scale; x++)
            {
                double fx = (x + 0.5) / scale - 0.5; int ix = (int)Math.Floor(fx); double tx = Math.Clamp(fx - ix, 0, 1);
                double v = (1 - ty) * ((1 - tx) * D(ix, iy) + tx * D(ix + 1, iy)) + ty * ((1 - tx) * D(ix, iy + 1) + tx * D(ix + 1, iy + 1));
                if (v <= 60) continue;
                int o = ((y + pad) * W + x + pad) * 4;
                buf[o] = 0; buf[o + 1] = 0; buf[o + 2] = 0;
            }
        }
        return (buf, W, H, scale, pad);
    }

    /// <summary>
    /// Etichette nella striscia: gruppi di inchiostro separati da spazi. Sotto e sopra la cornice si separano per colonne
    /// (lettere distanti al massimo 3 pixel), ai lati per righe.
    /// </summary>
    static List<(int x0, int y0, int x1, int y1)> Labels((int w, int h, int stride, byte[] px, bool rgba) img, int x0, int y0, int x1, int y1, bool vertical)
    {
        int sw = x1 - x0 + 1, sh = y1 - y0 + 1;
        var lum = new int[sw * sh]; var hist = new int[256];
        for (int y = 0; y < sh; y++)
            for (int x = 0; x < sw; x++)
            {
                var (r, g, b) = TropoIndex.PixelAt(img, x0 + x, y0 + y);
                int l = (r * 299 + g * 587 + b * 114) / 1000;
                lum[y * sw + x] = l; hist[l]++;
            }
        int bg = 0; for (int i = 1; i < 256; i++) if (hist[i] > hist[bg]) bg = i;
        bool Ink(int x, int y) => Math.Abs(lum[y * sw + x] - bg) > 70;
        int across = vertical ? sw : sh;
        bool InkAt(int k, int t) => vertical ? Ink(t, k) : Ink(k, t);
        var res = new List<(int, int, int, int)>();
        int n = vertical ? sh : sw, gapMax = vertical ? 1 : 3;
        int start = -1, last = -1;
        for (int k = 0; k <= n; k++)
        {
            bool ink = false;
            if (k < n) for (int t = 0; t < across && !ink; t++) ink = InkAt(k, t);
            if (ink) { if (start < 0) start = k; last = k; continue; }
            if (start >= 0 && (k - last > gapMax || k == n))
            {
                int a0 = int.MaxValue, a1 = -1;
                for (int k2 = start; k2 <= last; k2++)
                    for (int t = 0; t < across; t++)
                        if (InkAt(k2, t)) { a0 = Math.Min(a0, t); a1 = Math.Max(a1, t); }
                int len = last - start + 1, wid = a1 - a0 + 1;
                // scarta tacche e segni troppo piccoli e riquadri troppo grandi (caselle della legenda, scritte lunghe)
                if (a1 >= 0 && (vertical ? (wid >= 4 && len >= 4 && len <= 30) : (len >= 3 && wid >= 4 && len <= 60)))
                    res.Add(vertical ? (x0 + a0, y0 + start, x0 + a1, y0 + last) : (x0 + start, y0 + a0, x0 + last, y0 + a1));
                start = -1;
            }
        }
        return res;
    }

    /// <summary>
    /// Numeri e lettere in una parola: "54N", "10W", "0", "EQ", anche attaccati ("10W5W") o con gli errori tipici
    /// dell'OCR (O→0, I→1, S→5 in testa, "20S" letto "205"). Lettera '?' = emisfero non letto.
    /// </summary>
    static IEnumerable<(int v, char hemi)> Parse(string text)
    {
        // errori tipici dell'OCR di Windows su questi caratteri piccoli: 5 letto €, å, Ë, §; 0 letto ò, ô, ó, ö, ø
        var fix = new System.Text.StringBuilder();
        foreach (var ch in text)
            fix.Append("€åÅËë§".IndexOf(ch) >= 0 ? '5' : "òóôöøÒÓÔÖØ".IndexOf(ch) >= 0 ? '0' : "ìíîïÌÍÎÏ".IndexOf(ch) >= 0 ? '1' : ch);
        var t = fix.ToString().ToUpperInvariant().Replace("°", "").Replace("º", "").Replace(" ", "").Replace("•", "").Replace("·", "");
        if (t == "EQ" || t == "E0" || t == "EO") return new[] { (0, 'N') };
        string Map(string s) => Regex.Replace(Regex.Replace(s, "[OQD]", "0"), "[IL|!]", "1").Replace('Z', '2').Replace('B', '8');
        var m0 = Map(t);
        var variants = new List<string> { m0 };
        if (m0.Length > 1 && m0[0] == 'S' && char.IsDigit(m0[1])) variants.Add("5" + m0.Substring(1));
        var res = new HashSet<(int, char)>();
        foreach (var s in variants)
            foreach (Match m in Regex.Matches(s, @"(\d{1,3})([A-Z])?"))
            {
                var g = m.Groups[1].Value;
                if (!int.TryParse(g, out var v)) continue;
                char h = m.Groups[2].Success ? m.Groups[2].Value[0] : '?';
                if (g.Length == 3 && v > 90) res.Add((int.Parse(g.Substring(0, 2)), '?'));   // "205" = 20S letto male
                if (v > 180) continue;
                res.Add((v, h));
            }
        return res;
    }

    /// <summary>Valori possibili di un'etichetta: con la lettera giusta il segno è certo, altrimenti si provano entrambi.</summary>
    static IEnumerable<double> Candidates(int v, char h, bool vertical)
    {
        if (v == 0) return new double[] { 0 };
        if (vertical && h == 'N') return new double[] { v };
        if (vertical && h == 'S') return new double[] { -v };
        if (!vertical && h == 'E') return new double[] { v };
        if (!vertical && h == 'W') return new double[] { -v };
        return new double[] { v, -v };
    }

    /// <summary>
    /// Retta valore = a + b · pixel robusta: prova tutte le coppie di etichette, tiene quella con più etichette in accordo
    /// (le letture sbagliate restano fuori), poi rifà la retta con i minimi quadrati. Servono almeno 3 etichette concordi.
    /// </summary>
    static (double a, double b, int n, double rmsPx)? FitLinear(List<(double p, double v)> pts, int sign, Func<double, double> toDeg = null)
    {
        pts = pts.Distinct().ToList();
        if (pts.Count < 3) return null;
        var vals = pts.Select(x => x.v).Distinct().OrderBy(x => x).ToList();
        if (vals.Count < 3) return null;
        var gaps = vals.Zip(vals.Skip(1), (u, w) => w - u).Where(x => x > 0.01).OrderBy(x => x).ToList();
        if (gaps.Count == 0) return null;
        double tol = Math.Max(0.3, gaps[gaps.Count / 2] * 0.25);
        List<(double p, double v)> bestIn = null; (int distinct, int n) bestScore = (0, 0);
        for (int i = 0; i < pts.Count; i++)
            for (int j = i + 1; j < pts.Count; j++)
            {
                if (Math.Abs(pts[j].p - pts[i].p) < 3 || pts[j].v == pts[i].v) continue;
                double b = (pts[j].v - pts[i].v) / (pts[j].p - pts[i].p), a = pts[i].v - b * pts[i].p;
                if (b * sign <= 0) continue;     // verso sbagliato: è la versione con il segno opposto
                var inl = pts.Where(q => Math.Abs(a + b * q.p - q.v) <= tol).ToList();
                // vince la retta con più valori diversi: su un asse ogni valore compare una sola volta,
                // quindi tre "0" letti male non battono 48N, 45N, 42N, 27N letti bene
                var score = (inl.Select(q => q.v).Distinct().Count(), inl.Count);
                if (bestIn == null || score.CompareTo(bestScore) > 0) { bestIn = inl; bestScore = score; }
            }
        // regole severe: almeno 4 etichette diverse e concordi, gradi interi con passo "tondo"
        if (bestIn == null || bestIn.Select(q => q.v).Distinct().Count() < 4) return null;
        var deg = bestIn.Select(q => (int)Math.Round(toDeg == null ? q.v : toDeg(q.v))).Distinct().OrderBy(x => x).ToList();
        int step = 0;
        for (int k = 1; k < deg.Count; k++) step = Gcd(step, deg[k] - deg[k - 1]);
        if (!new[] { 1, 2, 3, 4, 5, 10, 15, 20, 30 }.Contains(step)) return null;
        double n = bestIn.Count, sp = bestIn.Sum(q => q.p), sv = bestIn.Sum(q => q.v), spp = bestIn.Sum(q => q.p * q.p), spv = bestIn.Sum(q => q.p * q.v);
        double den = n * spp - sp * sp;
        if (Math.Abs(den) < 1e-9) return null;
        double B = (n * spv - sp * sv) / den, A = (sv - B * sp) / n;
        if (Math.Abs(B) < 1e-9) return null;
        double rms = Math.Sqrt(bestIn.Average(q => Math.Pow((q.v - A) / B - q.p, 2)));
        if (rms > 1.5) return null;
        return (A, B, bestIn.Count, rms);
    }
}
