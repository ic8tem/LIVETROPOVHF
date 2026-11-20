using System.Globalization;
using System.Text;
using TropoVHF.Core;

namespace TropoVHF.Forecast;

/// <summary>
/// Registro dettagliato dei calcoli della linguetta Previsioni: ogni passo (dati scaricati, profili, strati, condotti,
/// perdite, bilancio) finisce in Dati/previsioni-calcoli.log accanto al programma e nella finestra del registro.
/// Il file ricomincia da capo oltre 8 MB (la copia precedente resta come .old).
/// </summary>
public static class CalcLog
{
    public static bool Enabled = true;
    public static event Action<string> Line;
    static readonly object Lock = new();
    static readonly List<string> Recent = new();

    public static string FilePath => Path.Combine(Settings.DataDir, "previsioni-calcoli.log");

    public static void W(string s)
    {
        if (!Enabled) return;
        var line = DateTime.UtcNow.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture) + "  " + s;
        lock (Lock)
        {
            Recent.Add(line);
            if (Recent.Count > 3000) Recent.RemoveRange(0, Recent.Count - 3000);
            try
            {
                var f = FilePath;
                var fi = new FileInfo(f);
                if (fi.Exists && fi.Length > 8 * 1024 * 1024) { Compat.MoveOver(f, f + ".old"); }
                File.AppendAllText(f, line + Environment.NewLine, Encoding.UTF8);
            }
            catch { }
        }
        try { Line?.Invoke(line); } catch { }
    }

    public static void Section(string title) => W("========== " + title + " ==========");

    public static string RecentText(int max = 1500)
    {
        lock (Lock) return string.Join(Environment.NewLine, Recent.Skip(Math.Max(0, Recent.Count - max)));
    }

    public static void Clear() { lock (Lock) Recent.Clear(); }

    static string F(double v, string fmt = "0.0") => double.IsNaN(v) ? "—" : v.ToString(fmt, CultureInfo.InvariantCulture);

    /// <summary>Tabella completa di un profilo: livelli, N, M, gradienti, strato migliore, condotto, guadagni per banda.</summary>
    public static void Profile(string where, ProfileResult p)
    {
        if (!Enabled) return;
        if (p == null) { W($"{where}: nessun profilo (dati mancanti)"); return; }
        W($"{where}: suolo modello {F(p.Elev, "0")} m, Ns {F(p.Ns)}, ΔN primo km {F(p.DN1km)} N/km, vento 10 m {F(p.Wind10)} m/s, strato limite {F(p.Blh, "0")} m, CAPE {F(p.Cape, "0")} J/kg");
        W("    quota m   p hPa    T °C   UR %      N        M     −dN/dh N/km   dM/dh");
        for (int i = 0; i < p.Levels.Length; i++)
        {
            var l = p.Levels[i];
            string g = "", gm = "";
            if (i > 0)
            {
                double dz = (l.Z - p.Levels[i - 1].Z) / 1000.0;
                if (dz > 0.001) { g = F(-(l.N - p.Levels[i - 1].N) / dz); gm = F((l.M - p.Levels[i - 1].M) / dz); }
            }
            W($"    {F(l.Z, "0"),7} {F(l.P, "0"),7} {F(l.T, "0.0"),7} {F(l.RH, "0"),6} {F(l.N, "0.0"),8} {F(l.M, "0.0"),8} {g,12} {gm,8}");
        }
        W($"    gradiente più forte {F(p.MaxGrad)} N/km; strato migliore: {(p.LayerName == "" ? "nessuno sopra il normale" : p.LayerName)}, guadagno dal gradiente {F(p.LayerGainDb)} dB{(p.Sub ? " (sub-rifrazione)" : "")}");
        if (p.HasDuct)
            W($"    condotto: base {F(p.DuctBase, "0")} m, cima {F(p.DuctTop, "0")} m, spessore {F(p.DuctThick, "0")} m, deficit ΔM {F(p.DuctDeficit)}{(p.SurfaceDuct ? ", al suolo" : ", elevato")}");
        else W("    nessun condotto (M non cala in nessuno strato)");
        foreach (var f in new[] { 144.3, 432.2, 1296.2 })
            W($"    {f:0} MHz: spessore minimo (Kerr) {F(ProfileResult.MinDuctM(f), "0")} m → guadagno {F(p.GainDb(f))} dB, indice {F(p.Index(f), "0.0")}");
    }
}
