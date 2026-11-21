using System.Text.RegularExpressions;

namespace TropoVHF.Core;

public enum LineClass { Other, Spot, OutOfBand, Excluded }

public sealed class Spot
{
    public DateTime Recv;      // ora di ricezione (UTC allineato NTP)
    public string Src = "";    // DXC, RBN, PSK
    public string Spotter = "";
    public string Dx = "";
    public double FreqKHz;
    public string Band = "";
    public string Mode = "";
    public string Comment = "";
    public string Utc = "";    // HH:MM
    public string DeLoc = "";
    public string DxLoc = "";
    public bool Airscatter;
    /// <summary>MSK144, FSK441 o JTMS: si accetta solo se il collegamento è entro SpotParser.ShortMsKm.</summary>
    public bool ShortMs;
    public long Id;
}

public static class SpotParser
{
    static readonly Regex ReSpot = new(@"^DX de\s+([^:\s]+):?\s+([0-9]+(?:\.[0-9]+)?)\s+(\S+)\s+(.*?)\s*([0-9]{4})Z", RegexOptions.Compiled);
    static readonly Regex ReExMode = new(@"\b(MSK144|FSK441|JT6M|JTMS|ISCAT(?:-[AB])?)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    /// <summary>
    /// Modi da meteor scatter (MSK144, FSK441, JT6M, JTMS, ISCAT): esclusi ovunque, cluster, MQTT e decodifiche UDP.
    /// In WSJT-X il carattere di modo "&amp;" indica MSK144.
    /// </summary>
    public static bool IsExcludedMode(string mode) =>
        !string.IsNullOrEmpty(mode) && (ReExMode.IsMatch(mode) || mode.Trim() == "&");
    static readonly Regex ReExTag = new(@"<\s*(AUE|AU|ES|MS|FAI|F2)\s*>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    static readonly Regex ReShortMs = new(@"\b(MSK144|FSK441|JTMS)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    static readonly Regex ReHardEx = new(@"\b(JT6M|ISCAT(?:-[AB])?)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    static readonly Regex ReOtherTag = new(@"<\s*(AUE|AU|ES|FAI|F2)\s*>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    /// <summary>
    /// MSK144, FSK441 e JTMS sono modi da meteor scatter, ma a breve distanza (entro ShortMsKm) si usano anche in tropo:
    /// lì si accettano. JT6M e ISCAT restano esclusi sempre.
    /// </summary>
    public const double ShortMsKm = 250;
    public static bool IsShortMsMode(string mode) => !string.IsNullOrEmpty(mode) && ReShortMs.IsMatch(mode) && !ReHardEx.IsMatch(mode);
    /// <summary>Airscatter: &lt;AS&gt;, &lt;ACS&gt; (maiuscole/minuscole) o dichiarato a parole.</summary>
    public static readonly Regex ReAirscatter = new(@"<\s*(AS|ACS)\s*>|\bair\s*-?\s*scatter\b|\baircraft\s+scatter\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    static readonly Regex ReLoc = new(@"\b([A-R]{2}[0-9]{2}(?:[A-X]{2})?)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    static readonly Regex ReSuffix = new(@"-(#|[0-9]{1,2})$", RegexOptions.Compiled);
    static readonly string[] KnownModes = { "MSK144", "FSK441", "JT6M", "FT8", "FT4", "Q65", "JT65", "ISCAT", "CW", "SSB", "USB", "LSB", "FM", "RTTY", "PSK31" };

    public static string CleanCall(string c)
    {
        c = (c ?? "").Trim().ToUpperInvariant().TrimEnd(':');
        return ReSuffix.Replace(c, "");
    }

    public static (Spot spot, LineClass cls) Parse(string line, string src, DateTime nowUtc)
    {
        var m = ReSpot.Match((line ?? "").Trim());
        if (!m.Success) return (null, LineClass.Other);
        if (!double.TryParse(m.Groups[2].Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double f))
            return (null, LineClass.Other);
        var s = new Spot
        {
            Recv = nowUtc,
            Src = src,
            Spotter = CleanCall(m.Groups[1].Value),
            Dx = CleanCall(m.Groups[3].Value),
            FreqKHz = f,
            Comment = m.Groups[4].Value.Trim(),
            Utc = m.Groups[5].Value.Substring(0, 2) + ":" + m.Groups[5].Value.Substring(2),
        };
        s.Band = Bands.FromKHz(f);
        if (s.Band == "") return (s, LineClass.OutOfBand);

        var up = s.Comment.ToUpperInvariant();
        foreach (var tok in up.Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries))
        {
            foreach (var km in KnownModes)
                if (tok == km) { s.Mode = km; break; }
            if (s.Mode != "") break;
        }
        if (s.Mode == "USB" || s.Mode == "LSB") s.Mode = "SSB";

        if (ReExMode.IsMatch(s.Comment) || ReExTag.IsMatch(s.Comment))
        {
            // MSK144/FSK441/JTMS (anche con <MS>): si decide dopo, con la distanza; tutto il resto è escluso subito
            if (ReHardEx.IsMatch(s.Comment) || ReOtherTag.IsMatch(s.Comment) || !(ReShortMs.IsMatch(s.Comment) || ReShortMs.IsMatch(s.Mode)))
                return (s, LineClass.Excluded);
            s.ShortMs = true;
        }

        s.Airscatter = ReAirscatter.IsMatch(s.Comment);
        var locs = new List<string>();
        foreach (Match lm in ReLoc.Matches(s.Comment))
        {
            var lu = lm.Value.ToUpperInvariant();
            if (lu == s.Mode) continue;
            if (Locator.IsValid(lu)) locs.Add(lu);
        }
        if (locs.Count >= 2) { s.DeLoc = locs[0]; s.DxLoc = locs[1]; }
        else if (locs.Count == 1) s.DxLoc = locs[0];
        return (s, LineClass.Spot);
    }
}

/// <summary>Spot in memoria (ultimi 60 minuti), con gestione dei doppioni.</summary>
public sealed class SpotStore
{
    readonly object _lock = new();
    readonly List<Spot> _spots = new();
    long _nextId = 1;

    public void Add(Spot s)
    {
        lock (_lock)
        {
            for (int i = 0; i < _spots.Count; i++)
            {
                var o = _spots[i];
                if (o.Dx == s.Dx && o.Spotter == s.Spotter && o.Band == s.Band && o.Src == s.Src)
                {
                    s.Id = o.Id;
                    _spots[i] = s;
                    return;
                }
            }
            s.Id = _nextId++;
            _spots.Add(s);
        }
    }

    public void Prune(DateTime nowUtc, TimeSpan maxAge)
    {
        lock (_lock) _spots.RemoveAll(s => nowUtc - s.Recv > maxAge);
    }

    /// <summary>
    /// Conferma umana: la stazione è data attiva su quella banda, entro ±minutes, da uno spot di un DX cluster umano
    /// (sorgente DXC: ON0DXK, DXMAPS, S50CLX… non dm4x, RBN o altre fonti automatiche), come DX o come chi spotta,
    /// in uno spot fatto da un nominativo diverso da quello escluso.
    /// </summary>
    public bool HumanConfirmed(string call, string band, DateTime t, int minutes = 30, string excludeSpotter = "")
    {
        if (string.IsNullOrEmpty(call)) return false;
        lock (_lock)
            return _spots.Any(s => s.Src == "DXC" && s.Band == band && Math.Abs((s.Recv - t).TotalMinutes) <= minutes &&
                                   ((s.Dx == call && s.Spotter != excludeSpotter && s.Spotter != call) || (s.Spotter == call && s.Dx != call)));
    }

    /// <summary>Spot da mostrare (i collegamenti lunghi trattenuti dalla quarantena restano fuori).</summary>
    public Func<Spot, bool> Visible = _ => true;

    public List<Spot> Filter(string band, int minutes, DateTime nowUtc)
    {
        List<Spot> list;
        lock (_lock)
        {
            var cut = nowUtc.AddMinutes(-minutes);
            list = _spots.Where(s => s.Band == band && s.Recv > cut).OrderByDescending(s => s.Recv).ToList();
        }
        return list.Where(s => Visible(s)).ToList();
    }
}
