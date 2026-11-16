using System.Globalization;
using System.Text;

namespace TropoVHF.Core;

/// <summary>Impostazioni in LIVETROPOVHF.ini accanto all'eseguibile (mai AppData, mai registro).</summary>
public sealed class Settings
{
    readonly string _path;
    readonly Dictionary<string, string> _kv = new(StringComparer.OrdinalIgnoreCase);

    public static string AppDir
    {
        get
        {
            var p = Compat.ProcessPath;
            if (!string.IsNullOrEmpty(p))
            {
                var d = Path.GetDirectoryName(p);
                // Mac: il programma sta dentro LIVETROPOVHF.app/Contents/MacOS; i dati nascono accanto all'app,
                // per non scrivere dentro il pacchetto firmato
                var mac = d?.IndexOf(".app/Contents/MacOS", StringComparison.Ordinal) ?? -1;
                if (mac > 0) { var app = d[..(mac + 4)]; return Path.GetDirectoryName(app) ?? d; }
                if (!string.IsNullOrEmpty(d)) return d;
            }
            return AppContext.BaseDirectory;
        }
    }

    public static string DataDir
    {
        get
        {
            var d = Path.Combine(AppDir, "Dati");
            try { Directory.CreateDirectory(d); } catch { }
            return d;
        }
    }

    public Settings(string dir)
    {
        _path = Path.Combine(dir, "LIVETROPOVHF.ini");
        // dal vecchio nome (TropoVHF.ini) si riprendono le impostazioni una volta sola
        try { var old = Path.Combine(dir, "TropoVHF.ini"); if (!File.Exists(_path) && File.Exists(old)) File.Copy(old, _path); } catch { }
        try
        {
            if (!File.Exists(_path)) return;
            foreach (var raw in File.ReadAllLines(_path))
            {
                var l = raw.Trim();
                if (l.Length == 0 || l.StartsWith(';') || l.StartsWith('#')) continue;
                int i = l.IndexOf('=');
                if (i > 0) _kv[l.Substring(0, i).Trim()] = l.Substring(i + 1).Trim();
            }
        }
        catch { }
    }

    public string Get(string k, string def) => _kv.TryGetValue(k, out var v) ? v : def;
    public int GetInt(string k, int def) => int.TryParse(Get(k, ""), NumberStyles.Integer, CultureInfo.InvariantCulture, out var v) ? v : def;
    public double GetDouble(string k, double def) => double.TryParse(Get(k, ""), NumberStyles.Float, CultureInfo.InvariantCulture, out var v) ? v : def;
    public bool GetBool(string k, bool def)
    {
        var v = Get(k, "");
        if (v == "") return def;
        return v == "1" || v.Equals("true", StringComparison.OrdinalIgnoreCase);
    }
    public void Set(string k, string v) => _kv[k] = v ?? "";
    public void Remove(string k) => _kv.Remove(k);
    public void SetInt(string k, int v) => _kv[k] = v.ToString(CultureInfo.InvariantCulture);
    public void SetDouble(string k, double v) => _kv[k] = v.ToString(CultureInfo.InvariantCulture);
    public void SetBool(string k, bool v) => _kv[k] = v ? "1" : "0";

    public void Save()
    {
        try
        {
            var sb = new StringBuilder();
            sb.Append("; LIVETROPOVHF - impostazioni (file portabile accanto al programma)\r\n");
            foreach (var k in _kv.Keys.OrderBy(x => x, StringComparer.OrdinalIgnoreCase))
                sb.Append(k).Append('=').Append(_kv[k]).Append("\r\n");
            File.WriteAllText(_path, sb.ToString(), new UTF8Encoding(false));
        }
        catch { }
    }
}
