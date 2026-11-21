using Avalonia.Media;

namespace TropoVHF.Ui;

/// <summary>Palette dei sei temi (stessi colori del bozzetto e della prova in Go).</summary>
public sealed class Palette
{
    public string Key = "";
    public bool Dark;
    public Color Page, Bar, Panel, Chip, Line, Line2, Ctl, CtlBg, Text, Text2, Muted, Sub, Accent, AccentFg, Band, BandFg, Clock, SelRow, AsText, MapSea, MapLand, MapCoast, MapBorder, MapGrid, MapGridLabel, Halo, Ink, Iso, Ok, Bad;

    static Color C(string hex) => Color.Parse(hex);

    public IBrush B(Color c) => new SolidColorBrush(c);

    public static readonly string[] Keys = { "chiaro", "nero", "notte", "arancio", "indaco", "rosa" };

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
            default:
                return new Palette { Key = "chiaro", Page = C("#F4F6F9"), Bar = C("#EEF0F4"), Panel = C("#FFFFFF"), Chip = C("#F0F3F7"), Line = C("#D9DFE6"), Line2 = C("#E6EAEF"), Ctl = C("#B9C2CD"), CtlBg = C("#FFFFFF"), Text = C("#1B2430"), Text2 = C("#2B3540"), Muted = C("#5F6B78"), Sub = C("#3E4A57"), Accent = C("#1E78E0"), AccentFg = C("#FFFFFF"), Band = C("#1F5FB8"), BandFg = C("#EAF2FF"), Clock = C("#B07A12"), SelRow = C("#E8F1FC"), AsText = C("#8F6A00"), MapSea = C("#E2ECF8"), MapLand = C("#FFFFFF"), MapCoast = C("#1F3A66"), MapBorder = C("#C3CBD5"), MapGrid = C("#9CC3EA"), MapGridLabel = C("#C6DDF4"), Halo = C("#E2ECF8"), Ink = C("#1B2430"), Iso = C("#3E5368"), Ok = C("#1F7A45"), Bad = C("#C62828") };
        }
    }

    /// <summary>Colore della scala dell'indice tropo 0–10.</summary>
    /// <summary>
    /// Scala fissa della previsione propria, in dB di guadagno stimato: uguale con tutti i temi (colori opachi all'82%).
    /// Sotto 7 dB nessun colore.
    /// </summary>
    public static readonly (double Lo, double Hi, Color C)[] GainBands =
    {
        (7, 10, Color.Parse("#6CC4F0")), (10, 13, Color.Parse("#5BC07A")), (13, 16, Color.Parse("#E8D24A")),
        (16, 18.5, Color.Parse("#F08A3C")), (18.5, 21, Color.Parse("#E5533D")), (21, 999, Color.Parse("#B05AD8"))
    };
    public const double GainAlpha = 0.82;
    /// <summary>Colore della scala fissa per un guadagno in dB (null sotto 7 dB).</summary>
    public static Color? GainColor(double db)
    {
        foreach (var b in GainBands) if (db >= b.Lo && db < b.Hi) return b.C;
        return null;
    }
    /// <summary>Colore più scuro della stessa fascia, per le curve.</summary>
    public static Color GainLineColor(double db)
    {
        var c = GainColor(db + 0.01) ?? GainBands[0].C;
        return Color.FromRgb((byte)(c.R * 0.6), (byte)(c.G * 0.6), (byte)(c.B * 0.6));
    }

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

/// <summary>Scala dei testi dell'interfaccia (80–150%), scelta con il cursore accanto al tema; vale per le due versioni.</summary>
public static class UiScale
{
    public static double F = 1.0;
    public static int Percent { get => (int)Math.Round(F * 100); set => F = Math.Max(0.8, Math.Min(1.5, value / 100.0)); }
}

