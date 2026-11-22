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

    /// <summary>Intestazione di ogni calcolo, sempre con la versione del programma e le opzioni del modello.</summary>
    public static void Section(string title) => W("========== " + title + " ==========  [LIVETROPOVHF " + Core.ErrorLog.Version +
        (Refractivity.UseCorrections ? "" : ", correzioni meteo spente") + (Refractivity.UseSoftDuct ? "" : ", condotto morbido spento") + "]");

    public static string RecentText(int max = 1500)
    {
        lock (Lock) return string.Join(Environment.NewLine, Recent.Skip(Math.Max(0, Recent.Count - max)));
    }

    public static void Clear() { lock (Lock) Recent.Clear(); }

    /// <summary>Ultime righe, per il registro degli errori.</summary>
    public static List<string> RecentLines(int n) { lock (Lock) return Recent.Skip(Math.Max(0, Recent.Count - n)).ToList(); }

    static string F(double v, string fmt = "0.0") => double.IsNaN(v) ? "—" : (Math.Abs(v) < 0.05 ? 0.0 : v).ToString(fmt, CultureInfo.InvariantCulture);   // niente «-+0.0»

    /// <summary>Errore di rete detto in parole: host non trovato (niente internet, DNS o firewall), tempo scaduto, risposta HTTP.</summary>
    public static string NetError(Exception ex, string host)
    {
        for (var e = ex; e != null; e = e.InnerException)
        {
            if (e is System.Net.Sockets.SocketException se)
                return se.SocketErrorCode == System.Net.Sockets.SocketError.HostNotFound || se.SocketErrorCode == System.Net.Sockets.SocketError.TryAgain
                    ? (Ui.Lang.Tr("il PC non trova {0}: connessione a internet assente, DNS o firewall che blocca il sito", "the PC cannot find {0}: no internet connection, DNS or a firewall blocking the site", host))
                    : (Ui.Lang.Tr("connessione a {0} non riuscita ({1})", "connection to {0} failed ({1})", host, se.SocketErrorCode));
            if (e is TaskCanceledException || e is TimeoutException)
                return Ui.Lang.Tr("{0} non risponde (tempo scaduto)", "{0} does not answer (timeout)", host);
        }
        return ex.Message;
    }

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
        if (p.Clim.Ok)
            W($"    climatologia P.453 ({F(p.Lat, "0.00")}, {F(p.Lon, "0.00")}): condotti al suolo {F(p.Clim.SOccur)}% del tempo, spessore {F(p.Clim.SThick, "0")} m, intensità {F(p.Clim.SStren)} M; " +
              $"elevati {F(p.Clim.EOccur)}%, base {F(p.Clim.EBase, "0")} m, spessore {F(p.Clim.EThick, "0")} m, intensità {F(p.Clim.EStren)} M");
        foreach (var l in p.Layers)
            W($"    strato {l.Name} ({F(l.ZBot, "0")}–{F(l.ZTop, "0")} m): calo oltre il normale {F(l.ExcessN)} N, gradiente termico {F(l.LapseKkm)} K/km, umidità sopra/sotto {F(l.QRatio, "0.00")}" +
              (l.Resolved ? " → condotto visibile al modello" : l.Inversion ? " → inversione: salto netto probabile" : " → senza segni di inversione") +
              (l.Deficit > 0 ? $"; condotto {(l.Resolved ? "risolto" : "potenziale")}: cima {F(l.DuctTop, "0")} m, base {F(l.DuctBase, "0")} m{(l.Surface ? " (suolo)" : "")}, deficit {F(l.Deficit)} M, λmax {F(l.LambdaMax, "0.00")} m" : "") +
              (l.Dual ? $"\n      doppio calcolo: A (salto in 50 m) {F(l.DeficitA)} M, λ {F(l.LambdaA, "0.00")} m; B (spessore P.453 {F(l.ClimThick, "0")} m) {F(l.DeficitB)} M, λ {F(l.LambdaB, "0.00")} m → usato il peggiore ({l.Chosen}); anomalia {F(l.Anomaly, "0.0")}× l'intensità tipica {F(l.ClimStren)} M" : ""));
        if (p.HasDuct)
            W($"    condotto: base {F(p.DuctBase, "0")} m, cima {F(p.DuctTop, "0")} m, spessore {F(p.DuctThick, "0")} m, deficit ΔM {F(p.DuctDeficit)}{(p.SurfaceDuct ? ", al suolo" : ", elevato")}");
        else W("    nessun condotto (M non cala in nessuno strato)");
        W($"    stabilità (Misme): ΔW primo km {F(p.DeltaW, "0")} J/kg (standard {Refractivity.DeltaWStd:0}) → {F(p.StabDb, "+0.0;-0.0")} dB" +
          $"; K al suolo {F(p.KSurf, "0")}, calo di K più forte {F(p.KDrop, "0")} ({(p.KDropLayer == "" ? "—" : p.KDropLayer)})" +
          $"; velocità verticale 850 hPa {F(p.W850 * 100, "+0.0;-0.0")} cm/s, 700 hPa {F(p.W700 * 100, "+0.0;-0.0")} cm/s" +
          (p.Subsidence ? " → subsidenza (+1,5 dB)" : p.Ascent ? " → aria in salita (−2 dB)" : "") +
          $"; vento 850 hPa {F(p.Wind850)} m/s da {F(p.Dir850, "0")}°, T850 {F(p.T850)} °C" + (p.Scirocco ? " → scirocco" : "") +
          (Refractivity.UseCorrections ? "" : " (correzioni spente)"));
        foreach (var f in new[] { 144.3, 432.2, 1296.2 })
        {
            var bd = p.BestDuct(f);
            W($"    {f:0} MHz (λ {299.792458 / f:0.00} m): {(bd == null ? "nessuno strato la intrappola" : $"intrappolata da {bd.Name}")} → guadagno {F(p.GainDb(f))} dB, indice {F(p.Index(f), "0.0")}");
        }
    }

    /// <summary>Durata leggibile: ms sotto il secondo, s sotto il minuto, poi min e s.</summary>
    public static string Dur(double ms) => ms < 1000 ? $"{ms:0} ms" : ms < 60000 ? $"{ms / 1000:0.0} s" : $"{Math.Floor(ms / 60000):0} min {(ms % 60000) / 1000:0} s";
}

/// <summary>Cronometro a tappe per misurare i tempi di reazione della previsione.</summary>
public sealed class Timing
{
    readonly System.Diagnostics.Stopwatch _sw = System.Diagnostics.Stopwatch.StartNew();
    double _last;
    public readonly List<(string Name, double Ms)> Laps = new();
    /// <summary>Chiude una tappa: il tempo dall'ultima tappa.</summary>
    public void Lap(string name) { double t = _sw.Elapsed.TotalMilliseconds; Laps.Add((name, t - _last)); _last = t; }
    /// <summary>Aggiunge una voce misurata altrove (non sposta l'inizio della tappa successiva).</summary>
    public void Add(string name, double ms) => Laps.Add((name, ms));
    public double TotalMs => _sw.Elapsed.TotalMilliseconds;
    public string Summary() => string.Join(", ", Laps.Where(l => l.Ms >= 0.5).Select(l => $"{l.Name} {CalcLog.Dur(l.Ms)}"));
}

/// <summary>Statistiche di uno scaricamento GFS: rete, analisi, attese per il limite di Open-Meteo.</summary>
public sealed class FetchStats
{
    public double NetMs, ParseMs, WaitMs; public int Batches, Retries, Cached; public long Bytes;
    public string Summary() => (Cached > 0 ? $"{Cached} punti dalla cache, " : "") + $"rete {CalcLog.Dur(NetMs)} ({Batches} richieste, {Bytes / 1024} kB" + (Retries > 0 ? $", {Retries} ripetute per il limite" : "") +
                               $"), analisi dei profili {CalcLog.Dur(ParseMs)}, attese obbligate {CalcLog.Dur(WaitMs)}";
}
