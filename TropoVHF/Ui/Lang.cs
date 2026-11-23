using System.Globalization;
using System.Reflection;
using System.Text.Json;

namespace TropoVHF.Ui;

/// <summary>
/// Le 36 lingue di YA-MS, nello stesso ordine. I testi stanno nei file Lang/{codice}.json incorporati nell'eseguibile.
/// Se una chiave manca nella lingua scelta si usa la lingua madre (pt-br → pt, zh-tw → zh), poi l'inglese, poi la chiave.
/// </summary>
public static class Lang
{
    public static readonly (string code, string name)[] All =
    {
        ("it","Italiano"),("en","English"),("de","Deutsch"),("fr","Français"),("es","Español"),("pt","Português (Portugal)"),
        ("pt-br","Português (Brasil)"),("nl","Nederlands"),("sv","Svenska"),("nb","Norsk (bokmål)"),("da","Dansk"),("is","Íslenska"),
        ("et","Eesti"),("lv","Latviešu"),("lt","Lietuvių"),("pl","Polski"),("cs","Čeština"),("sk","Slovenčina"),("hu","Magyar"),("ro","Română"),
        ("el","Ελληνικά"),("ru","Русский"),("uk","Українська"),("tr","Türkçe"),("af","Afrikaans"),("ar","العربية"),("he","עברית"),
        ("ur","اردو"),("hi","हिन्दी"),("bn","বাংলা"),("th","ไทย"),("id","Bahasa Indonesia"),("tl","Tagalog"),("zh","中文 (简体)"),
        ("zh-tw","中文 (繁體)"),("ja","日本語"),("ko","한국어")
    };

    /// <summary>
    /// Lingua del sistema operativo, per la prima partenza: si cerca fra le 36 lingue, altrimenti inglese.
    /// </summary>
    public static string SystemDefault()
    {
        var names = new List<string>();
        try { names.Add(CultureInfo.CurrentUICulture.Name); } catch { }
        try { names.Add(CultureInfo.InstalledUICulture.Name); } catch { }
        foreach (var v in new[] { "LC_ALL", "LC_MESSAGES", "LANG", "LANGUAGE" })
        {
            var e = Environment.GetEnvironmentVariable(v);
            if (!string.IsNullOrEmpty(e)) names.AddRange(e.Split(':'));
        }
        foreach (var raw in names)
        {
            var c = Match(raw);
            if (c != null) return c;
        }
        return "en";
    }

    static string Match(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return null;
        var n = raw.Split('.', '@')[0].Replace('_', '-').ToLowerInvariant();   // it_IT.UTF-8 → it-it
        if (n == "c" || n == "posix") return null;
        var p = n.Split('-');
        var lang = p[0];
        if (lang == "zh")
            return n.Contains("hant") || n.EndsWith("-tw") || n.EndsWith("-hk") || n.EndsWith("-mo") ? "zh-tw" : "zh";
        if (lang == "pt") return n.EndsWith("-br") ? "pt-br" : "pt";
        if (lang == "no" || lang == "nn" || lang == "nb") return "nb";
        if (lang == "fil") lang = "tl";
        if (lang == "iw") lang = "he";
        if (lang == "in") lang = "id";
        return All.Any(a => a.code == lang) ? lang : null;
    }

    /// <summary>
    /// Nomi dei file delle guide da cercare, nell'ordine: traduzione nella lingua scelta (Docs/lang/nome.codice.md, traduzione
    /// automatica), poi l'inglese, poi l'originale italiano. Il portoghese del Brasile usa il portoghese.
    /// </summary>
    public static IEnumerable<string> DocCandidates(string fileName)
    {
        var b = fileName.EndsWith(".md", StringComparison.OrdinalIgnoreCase) ? fileName.Substring(0, fileName.Length - 3) : fileName;
        var c = Current;
        if (c != "it")
        {
            yield return "." + b + "." + c.Replace('-', '_') + ".md";
            if (c == "pt-br") yield return "." + b + ".pt.md";
            if (c != "en") yield return "." + b + ".en.md";
        }
        yield return "." + fileName;
    }

    public static bool IsRtl(string code) => code == "ar" || code == "he" || code == "ur";

    static string _current = "it";
    public static string Current
    {
        get => _current;
        set => _current = All.Any(a => a.code == value) ? value : "en";
    }

    static readonly Dictionary<string, Dictionary<string, string>> Cache = new();
    static readonly object Lock = new();

    static Dictionary<string, string> Load(string code)
    {
        lock (Lock)
        {
            if (Cache.TryGetValue(code, out var d)) return d;
            d = new Dictionary<string, string>();
            try
            {
                var asm = Assembly.GetExecutingAssembly();
                var name = asm.GetManifestResourceNames()
                    .FirstOrDefault(n => n.EndsWith(".Lang." + code + ".json", StringComparison.OrdinalIgnoreCase)
                                      || n.EndsWith("Lang\\" + code + ".json", StringComparison.OrdinalIgnoreCase)
                                      || n.EndsWith("Lang/" + code + ".json", StringComparison.OrdinalIgnoreCase));
                if (name != null)
                {
                    using var s = asm.GetManifestResourceStream(name)!;
                    d = JsonSerializer.Deserialize<Dictionary<string, string>>(s) ?? d;
                }
            }
            catch { }
            Cache[code] = d;
            return d;
        }
    }

    public static string T(string key)
    {
        if (Load(Current).TryGetValue(key, out var v) && !string.IsNullOrEmpty(v)) return v;
        var dash = Current.IndexOf('-');
        if (dash > 0 && Load(Current[..dash]).TryGetValue(key, out v) && !string.IsNullOrEmpty(v)) return v;
        if (Load("en").TryGetValue(key, out v) && !string.IsNullOrEmpty(v)) return v;
        return key;
    }

    /// <summary>Testo scritto nel codice in italiano e in inglese: le altre lingue lo cercano nel loro file
    /// alla chiave «it:» + testo italiano, altrimenti resta l'inglese. Con argomenti è un formato di string.Format.</summary>
    public static string Tr(string it, string en, params object[] args)
    {
        string fmt;
        if (Current == "it") fmt = it;
        else if (Current == "en" || string.IsNullOrEmpty(it)) fmt = en;
        else
        {
            var key = "it:" + it;
            if (!(Load(Current).TryGetValue(key, out fmt) && !string.IsNullOrEmpty(fmt)))
            {
                var dash = Current.IndexOf('-');
                if (!(dash > 0 && Load(Current[..dash]).TryGetValue(key, out fmt) && !string.IsNullOrEmpty(fmt))) fmt = en;
            }
        }
        if (args == null || args.Length == 0) return fmt;
        try { return string.Format(CultureInfo.CurrentCulture, fmt, args); }
        catch (FormatException) { try { return string.Format(CultureInfo.CurrentCulture, en, args); } catch (FormatException) { return en; } }
    }

    public static string F(string key, params object[] args)
    {
        var fmt = T(key);
        try { return string.Format(Culture, fmt, args); }
        catch (FormatException) { return fmt; }
    }

    public static CultureInfo Culture
    {
        get
        {
            try { return CultureInfo.GetCultureInfo(Current switch { "nb" => "nb-NO", "tl" => "fil", "pt-br" => "pt-BR", "zh-tw" => "zh-TW", "zh" => "zh-CN", _ => Current }); }
            catch { return CultureInfo.InvariantCulture; }
        }
    }

    /// <summary>Nomi brevi dei giorni, da domenica a sabato, nella lingua scelta.</summary>
    public static string[] DayNames()
    {
        try
        {
            var n = Culture.DateTimeFormat.AbbreviatedDayNames;
            if (n is { Length: 7 } && n.All(x => !string.IsNullOrWhiteSpace(x))) return n;
        }
        catch { }
        return new[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
    }
}
