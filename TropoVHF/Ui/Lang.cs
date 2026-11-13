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
        ("et","Eesti"),("lv","Latviešu"),("lt","Lietuvių"),("pl","Polski"),("cs","Čeština"),("hu","Magyar"),("ro","Română"),
        ("el","Ελληνικά"),("ru","Русский"),("uk","Українська"),("tr","Türkçe"),("af","Afrikaans"),("ar","العربية"),("he","עברית"),
        ("ur","اردو"),("hi","हिन्दी"),("bn","বাংলা"),("th","ไทย"),("id","Bahasa Indonesia"),("tl","Tagalog"),("zh","中文 (简体)"),
        ("zh-tw","中文 (繁體)"),("ja","日本語"),("ko","한국어")
    };

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
