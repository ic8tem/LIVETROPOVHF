using Avalonia.Media;

namespace TropoVHF.Ui;

/// <summary>Palette dei 18 temi (i sei originali del bozzetto e della prova in Go, piu' quelli armonizzati con E-LAYER ANALYZER).</summary>
public sealed class Palette
{
    public string Key = "";
    public bool Dark;
    public Color Page, Bar, Panel, Chip, Line, Line2, Ctl, CtlBg, Text, Text2, Muted, Sub, Accent, AccentFg, Band, BandFg, Clock, SelRow, AsText, MapSea, MapLand, MapCoast, MapBorder, MapGrid, MapGridLabel, Halo, Ink, Iso, Ok, Bad;

    static Color C(string hex) => Color.Parse(hex);

    public IBrush B(Color c) => new SolidColorBrush(c);

    // Armonizzazione (IC8TEM 15/11/2026): i 18 schemi di E-LAYER ANALYZER. Arancio, indaco e rosa restano quelli di LIVETROPOVHF (fanno
    // da pastello arancione, indaco e rosa); gli altri pastelli usano le tinte di YAEC, gli scuri i colori di IP-TRX-CTRL e di YA-MS.
    public static readonly string[] Keys = { "chiaro", "notte", "nero", "azzurro", "arancio", "acquamarina", "pverde", "rosa", "lilla", "indaco", "marrone",
        "scia", "ip-chiaro", "ip-notte", "ip-nero", "blu", "viola", "verde" };

    static Color Tinta(Color t, double k) => Color.FromRgb((byte)(255 + (t.R - 255) * k), (byte)(255 + (t.G - 255) * k), (byte)(255 + (t.B - 255) * k));
    static Color Scura(Color t, double k) => Color.FromRgb((byte)(t.R * (1 - k)), (byte)(t.G * (1 - k)), (byte)(t.B * (1 - k)));
    static Color Fra(Color a, Color b, double k) => Color.FromRgb((byte)(a.R + (b.R - a.R) * k), (byte)(a.G + (b.G - a.G) * k), (byte)(a.B + (b.B - a.B) * k));

    // pastello chiaro con la tinta di YAEC, sullo stampo del tema "arancio"
    static Palette Pastello(string key, Color t) => new Palette
    {
        Key = key, Page = Tinta(t, 0.09), Bar = Tinta(t, 0.20), Panel = Tinta(t, 0.04), Chip = Tinta(t, 0.15), Line = Tinta(t, 0.32), Line2 = Tinta(t, 0.22),
        Ctl = Tinta(t, 0.55), CtlBg = C("#FFFFFF"), Text = Scura(t, 0.78), Text2 = Scura(t, 0.72), Muted = Scura(t, 0.45), Sub = Scura(t, 0.62),
        Accent = Scura(t, 0.25), AccentFg = C("#FFFFFF"), Band = Scura(t, 0.30), BandFg = Tinta(t, 0.08), Clock = Scura(t, 0.25), SelRow = Tinta(t, 0.24),
        AsText = C("#8F6A00"), MapSea = Tinta(t, 0.13), MapLand = Tinta(t, 0.03), MapCoast = Scura(t, 0.50), MapBorder = Tinta(t, 0.38), MapGrid = Tinta(t, 0.48),
        MapGridLabel = Tinta(t, 0.32), Halo = Tinta(t, 0.13), Ink = Scura(t, 0.78), Iso = Scura(t, 0.48), Ok = C("#1F7A45"), Bad = C("#C62828")
    };

    // scuro sullo stampo del tema "notte": fondo, riquadro, incavo, bordo, testo, testo spento, accento, testo sull'accento, ok, errore, giallo
    static Palette Scuro(string key, string bg, string pan, string ins, string bor, string txt, string mut, string acc, string accTxt, string ok, string bad, string yel)
    {
        Color b = C(bg), p = C(pan), i = C(ins), l = C(bor), t = C(txt), m = C(mut), a = C(acc);
        return new Palette
        {
            Key = key, Dark = true, Page = b, Bar = i, Panel = p, Chip = Fra(p, l, 0.35), Line = l, Line2 = Fra(p, l, 0.6), Ctl = Fra(l, t, 0.25), CtlBg = b,
            Text = t, Text2 = Fra(t, m, 0.25), Muted = m, Sub = Fra(t, m, 0.5), Accent = a, AccentFg = C(accTxt), Band = Fra(p, a, 0.25), BandFg = t,
            Clock = C(yel), SelRow = Fra(p, a, 0.18), AsText = C(yel), MapSea = Fra(b, C("#000000"), 0.2), MapLand = p, MapCoast = Fra(m, a, 0.3), MapBorder = l,
            MapGrid = Fra(l, a, 0.3), MapGridLabel = Fra(l, a, 0.5), Halo = Fra(b, C("#000000"), 0.2), Ink = t, Iso = m, Ok = C(ok), Bad = C(bad)
        };
    }

    // chiaro di IP-TRX-CTRL sullo stampo del tema "chiaro"
    static Palette ChiaroIp() => new Palette
    {
        Key = "ip-chiaro", Page = C("#F5F7FA"), Bar = C("#F0F2F5"), Panel = C("#FFFFFF"), Chip = C("#F0F2F5"), Line = C("#D5DAE1"), Line2 = C("#E3E7ED"), Ctl = C("#B9C0CA"),
        CtlBg = C("#FFFFFF"), Text = C("#1B1F24"), Text2 = C("#2B3138"), Muted = C("#555D68"), Sub = C("#3E4650"), Accent = C("#1F6FEB"), AccentFg = C("#FFFFFF"),
        Band = C("#141A22"), BandFg = C("#F5B942"), Clock = C("#B07A12"), SelRow = C("#E6EFFC"), AsText = C("#8F6A00"), MapSea = C("#E4EBF3"), MapLand = C("#FFFFFF"),
        MapCoast = C("#1F3A66"), MapBorder = C("#C3CBD5"), MapGrid = C("#9CC3EA"), MapGridLabel = C("#C6DDF4"), Halo = C("#E4EBF3"), Ink = C("#1B1F24"),
        Iso = C("#3E5368"), Ok = C("#1E7A3C"), Bad = C("#D92D20")
    };

    public static Palette Get(string key)
    {
        switch (key)
        {
            case "nero":
                return new Palette { Key = key, Dark = true, Page = C("#000000"), Bar = C("#0B0B0B"), Panel = C("#111111"), Chip = C("#1C1C1C"), Line = C("#2A2A2A"), Line2 = C("#1F1F1F"), Ctl = C("#444444"), CtlBg = C("#000000"), Text = C("#F2F2F2"), Text2 = C("#E0E0E0"), Muted = C("#A3A3A3"), Sub = C("#CFCFCF"), Accent = C("#3B9BE8"), AccentFg = C("#000000"), Band = C("#16202B"), BandFg = C("#D6E6F5"), Clock = C("#F0B13A"), SelRow = C("#1E2A36"), AsText = C("#FFD54A"), MapSea = C("#0B0D10"), MapLand = C("#17191D"), MapCoast = C("#7C8FA6"), MapBorder = C("#3A4048"), MapGrid = C("#22405E"), MapGridLabel = C("#33557A"), Halo = C("#0B0D10"), Ink = C("#E8EDF2"), Iso = C("#B9C6D2"), Ok = C("#5BD08A"), Bad = C("#FF7A7A") };
            case "notte":
                return new Palette { Key = key, Dark = true, Page = C("#0B1220"), Bar = C("#0E1626"), Panel = C("#111B2E"), Chip = C("#18243A"), Line = C("#22314A"), Line2 = C("#1A2740"), Ctl = C("#3A4D6E"), CtlBg = C("#0B1220"), Text = C("#D9E2EE"), Text2 = C("#C7D2E0"), Muted = C("#8D9BB0"), Sub = C("#B4C0D0"), Accent = C("#6B9BF2"), AccentFg = C("#0B1220"), Band = C("#1A2A4A"), BandFg = C("#D9E6FF"), Clock = C("#E3A857"), SelRow = C("#1D2C47"), AsText = C("#FFD54A"), MapSea = C("#0C1424"), MapLand = C("#162238"), MapCoast = C("#6F87AE"), MapBorder = C("#33476A"), MapGrid = C("#25406A"), MapGridLabel = C("#34588F"), Halo = C("#0C1424"), Ink = C("#D9E2EE"), Iso = C("#8FA3C0"), Ok = C("#5BD08A"), Bad = C("#FF7A7A") };
            case "arancio":
                return new Palette { Key = key, Page = C("#FFF6EC"), Bar = C("#FDEBD6"), Panel = C("#FFFBF6"), Chip = C("#FCEEDD"), Line = C("#F0D9BE"), Line2 = C("#F5E4CF"), Ctl = C("#DDB88E"), CtlBg = C("#FFFFFF"), Text = C("#3A2A1A"), Text2 = C("#4A3622"), Muted = C("#7A5C40"), Sub = C("#5C4630"), Accent = C("#B85A0C"), AccentFg = C("#FFFFFF"), Band = C("#A9530C"), BandFg = C("#FFF1E2"), Clock = C("#B85A0C"), SelRow = C("#FBE3C8"), AsText = C("#8F6A00"), MapSea = C("#F6EBDD"), MapLand = C("#FFFBF4"), MapCoast = C("#7A5A3A"), MapBorder = C("#D8C2A8"), MapGrid = C("#E2BE96"), MapGridLabel = C("#E9CDAE"), Halo = C("#F6EBDD"), Ink = C("#3A2A1A"), Iso = C("#7A5A3A"), Ok = C("#1F7A45"), Bad = C("#C62828") };
            case "indaco":
                return new Palette { Key = key, Page = C("#F1F0FB"), Bar = C("#E6E4F8"), Panel = C("#FBFAFF"), Chip = C("#ECEAFB"), Line = C("#D7D3F2"), Line2 = C("#E4E1F7"), Ctl = C("#B6AEE6"), CtlBg = C("#FFFFFF"), Text = C("#221E3D"), Text2 = C("#2F2A52"), Muted = C("#5E5884"), Sub = C("#433D6B"), Accent = C("#5B4FC9"), AccentFg = C("#FFFFFF"), Band = C("#4C41B3"), BandFg = C("#F0EEFF"), Clock = C("#5B4FC9"), SelRow = C("#E2DEFA"), AsText = C("#8A6A00"), MapSea = C("#E9E7F8"), MapLand = C("#FBFAFF"), MapCoast = C("#4D467A"), MapBorder = C("#CFCAEA"), MapGrid = C("#BDB5EC"), MapGridLabel = C("#D3CDF3"), Halo = C("#E9E7F8"), Ink = C("#221E3D"), Iso = C("#4D467A"), Ok = C("#1F7A45"), Bad = C("#C62828") };
            case "rosa":
                return new Palette { Key = key, Page = C("#FDF1F5"), Bar = C("#FADFE8"), Panel = C("#FFF9FB"), Chip = C("#FBE8EF"), Line = C("#F0D2DD"), Line2 = C("#F6E0E8"), Ctl = C("#DDA9BC"), CtlBg = C("#FFFFFF"), Text = C("#3B1E29"), Text2 = C("#4E2837"), Muted = C("#805566"), Sub = C("#5E3A48"), Accent = C("#B23A64"), AccentFg = C("#FFFFFF"), Band = C("#9E3157"), BandFg = C("#FFEFF5"), Clock = C("#B23A64"), SelRow = C("#F9DCE6"), AsText = C("#8F6A00"), MapSea = C("#F8E9EE"), MapLand = C("#FFFBFC"), MapCoast = C("#7A4A5C"), MapBorder = C("#E6C9D4"), MapGrid = C("#E6B3C5"), MapGridLabel = C("#EFCCD8"), Halo = C("#F8E9EE"), Ink = C("#3B1E29"), Iso = C("#7A4A5C"), Ok = C("#1F7A45"), Bad = C("#C62828") };
            case "azzurro": return Pastello(key, Color.FromRgb(70, 150, 230));
            case "acquamarina": return Pastello(key, Color.FromRgb(40, 180, 170));
            case "pverde": return Pastello(key, Color.FromRgb(80, 170, 90));
            case "lilla": return Pastello(key, Color.FromRgb(160, 120, 210));
            case "marrone": return Pastello(key, Color.FromRgb(150, 105, 70));
            case "scia": return Scuro(key, "#161230", "#1C1740", "#0E0B22", "#3A3270", "#EEEBFA", "#B3ADD6", "#38BDF8", "#0E0B22", "#86EFAC", "#F87171", "#FDE047");
            case "ip-chiaro": return ChiaroIp();
            case "ip-notte": return Scuro(key, "#0F1A2B", "#15233A", "#0B1526", "#2A3D5C", "#E6ECF5", "#A9B6C9", "#5AA2FF", "#0B1526", "#8EE0A6", "#FF4D4F", "#F5B942");
            case "ip-nero": return Scuro(key, "#0B0B0B", "#151515", "#050505", "#303030", "#F2F2F2", "#A6A6A6", "#FF9F1C", "#000000", "#7CE29A", "#FF3B30", "#FFB000");
            case "blu": return Scuro(key, "#12417B", "#16498A", "#0C3162", "#2F64A8", "#FFFFFF", "#C9D8EC", "#FFD24A", "#0C3162", "#9BF0B5", "#FF5A52", "#FFD24A");
            case "viola": return Scuro(key, "#2A1A41", "#33204F", "#22143A", "#573A85", "#F4EEFB", "#CDBFDF", "#E2C6FF", "#2A1A41", "#B9F2A0", "#FF5C7A", "#FFD98A");
            case "verde": return Scuro(key, "#12301F", "#173B27", "#0B2215", "#2E6C49", "#E6F6EC", "#BAD4C5", "#A6E35F", "#0B2215", "#A6E35F", "#FF5A4E", "#E8F5A0");
            default:
                return new Palette { Key = "chiaro", Page = C("#F4F6F9"), Bar = C("#EEF0F4"), Panel = C("#FFFFFF"), Chip = C("#F0F3F7"), Line = C("#D9DFE6"), Line2 = C("#E6EAEF"), Ctl = C("#B9C2CD"), CtlBg = C("#FFFFFF"), Text = C("#1B2430"), Text2 = C("#2B3540"), Muted = C("#5F6B78"), Sub = C("#3E4A57"), Accent = C("#1E78E0"), AccentFg = C("#FFFFFF"), Band = C("#1F5FB8"), BandFg = C("#EAF2FF"), Clock = C("#B07A12"), SelRow = C("#E8F1FC"), AsText = C("#8F6A00"), MapSea = C("#E2ECF8"), MapLand = C("#FFFFFF"), MapCoast = C("#1F3A66"), MapBorder = C("#C3CBD5"), MapGrid = C("#9CC3EA"), MapGridLabel = C("#C6DDF4"), Halo = C("#E2ECF8"), Ink = C("#1B2430"), Iso = C("#3E5368"), Ok = C("#1F7A45"), Bad = C("#C62828") };
        }
    }

    /// <summary>Colore della scala dell'indice tropo 0–10.</summary>
    public static Color IndexColor(double v)
    {
        if (v < 2) return Color.Parse("#3B82C4");
        if (v < 4) return Color.Parse("#5BC07A");
        if (v < 6) return Color.Parse("#E8D24A");
        if (v < 7) return Color.Parse("#F08A3C");
        if (v < 8.5) return Color.Parse("#E5533D");
        return Color.Parse("#B05AD8");
    }

    public static readonly Color SpotBlue = Color.Parse("#1E78C8");
    public static readonly Color AirscatterYellow = Color.Parse("#F5C800");
    public static readonly Color AirscatterEdge = Color.Parse("#5C4A00");
    public static readonly Color DecodeRed = Color.Parse("#D62839");
    public static readonly Color LongLilac = Color.Parse("#A66FD9");     // quadrati con traffico oltre 1000 km
    public static readonly Color QsoGreen = Color.Parse("#1F9D55");      // QSO desunti dal live
    public static readonly Color ListenerViolet = Color.Parse("#B0125B");   // rosa scuro: stazioni in ascolto (MQTT)
    public static readonly Color ListenerLine = Color.Parse("#EC4899");     // rosa: ciò che ascoltano e rimandano in rete
}
