using System.Globalization;
using System.Text.Json;
using TropoVHF.Core;

namespace TropoVHF.Forecast;

/// <summary>Clima radio di una tratta secondo NBS TN101 § 10.2, ricavato dalla rianalisi ERA5.</summary>
public sealed class RadioClimateInfo
{
    public Itm.Climate Climate = Itm.Climate.MaritimeTemperateLand;
    public double NsMean = double.NaN, NsRange = double.NaN, N0 = double.NaN, RhMean = double.NaN, RainMm = double.NaN, ElevM, Lat, Lon;
    public bool FromEra5;          // false = regola di riserva (latitudine e mare), senza dati
    public string Why = "";        // perché è stato scelto quel clima (italiano; la UI lo traduce con Lang)
}

/// <summary>
/// Sceglie il clima radio del TN101 (§ 10.2, i sette climi dell'ITM) dal punto medio della tratta, con i valori giornalieri
/// della rianalisi ERA5 di tre anni presi da Open-Meteo (temperatura, umidità e pressione al suolo, pioggia):
/// rifrattività al suolo Ns = 77,6/T·(P + 4810·e/T), media annua e escursione delle medie mensili, umidità media e pioggia annua.
/// I dati si scaricano una volta sola per ogni cella di 0,5° e restano in Dati/CLIMA. Se Open-Meteo non risponde si usa una
/// regola di riserva con la sola latitudine e la frazione di mare. Criteri: manuale delle previsioni, § 10 sexies.
/// </summary>
public static class RadioClimate
{
    static readonly CultureInfo Ci = CultureInfo.InvariantCulture;
    static readonly Dictionary<string, RadioClimateInfo> _mem = new();
    static readonly object _lock = new();

    public static string Folder { get { var d = Path.Combine(Settings.DataDir, "CLIMA"); try { Directory.CreateDirectory(d); } catch { } return d; } }

    /// <summary>Nome italiano del clima (la UI lo traduce).</summary>
    public static string Name(Itm.Climate c) => c switch
    {
        Itm.Climate.Equatorial => "equatoriale",
        Itm.Climate.ContinentalSubtropical => "continentale subtropicale",
        Itm.Climate.MaritimeSubtropical => "marittimo subtropicale",
        Itm.Climate.Desert => "desertico",
        Itm.Climate.ContinentalTemperate => "continentale temperato",
        Itm.Climate.MaritimeTemperateLand => "marittimo temperato sulla terra",
        _ => "marittimo temperato sul mare",
    };

    /// <summary>N0 tipico del clima (ITM, NTIA 82-100), usato quando mancano i dati ERA5.</summary>
    public static double TypicalN0(Itm.Climate c) => c switch
    {
        Itm.Climate.Equatorial => 360,
        Itm.Climate.ContinentalSubtropical => 320,
        Itm.Climate.MaritimeSubtropical => 370,
        Itm.Climate.Desert => 280,
        Itm.Climate.ContinentalTemperate => 301,
        Itm.Climate.MaritimeTemperateLand => 320,
        _ => 350,
    };

    /// <summary>Clima della tratta. seaFrac = frazione di mare del profilo; overSea = entrambi gli orizzonti sul mare (TN101 § 10.2, clima 3).</summary>
    public static async Task<RadioClimateInfo> Get(HttpClient http, double lat, double lon, double seaFrac, bool overSea, CancellationToken ct)
    {
        double la = Math.Round(lat * 2) / 2, lo = Math.Round(lon * 2) / 2;
        string key = la.ToString("0.0", Ci) + "_" + lo.ToString("0.0", Ci);
        RadioClimateInfo raw = null;
        lock (_lock) _mem.TryGetValue(key, out raw);
        if (raw == null)
        {
            raw = Load(key) ?? await Download(http, la, lo, key, ct);
            if (raw != null) lock (_lock) _mem[key] = raw;
        }
        return Classify(raw, lat, seaFrac, overSea);
    }

    static RadioClimateInfo Load(string key)
    {
        try
        {
            var f = Path.Combine(Folder, key + ".txt");
            if (!File.Exists(f)) return null;
            var p = File.ReadAllText(f).Trim().Split(';');
            if (p.Length < 7) return null;
            double D(int i) => double.Parse(p[i], Ci);
            return new RadioClimateInfo { Lat = D(0), Lon = D(1), ElevM = D(2), NsMean = D(3), NsRange = D(4), RhMean = D(5), RainMm = D(6), FromEra5 = true };
        }
        catch { return null; }
    }

    static async Task<RadioClimateInfo> Download(HttpClient http, double la, double lo, string key, CancellationToken ct)
    {
        int y = DateTime.UtcNow.Year - 1;
        string url = "https://archive-api.open-meteo.com/v1/archive?latitude=" + la.ToString("0.0", Ci) + "&longitude=" + lo.ToString("0.0", Ci) +
                     $"&start_date={y - 2}-01-01&end_date={y}-12-31&daily=temperature_2m_mean,relative_humidity_2m_mean,surface_pressure_mean,precipitation_sum&timezone=GMT";
        try
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            cts.CancelAfter(15000);
            using var resp = await http.GetAsync(url, cts.Token);
            if (!resp.IsSuccessStatusCode) { CalcLog.W($"clima radio: Open-Meteo ERA5 risponde {(int)resp.StatusCode}, uso la regola di riserva"); return null; }
            string json = await Compat.ReadString(resp.Content, cts.Token);
            var r = Parse(json, la, lo);
            if (r == null) return null;
            try
            {
                File.WriteAllText(Path.Combine(Folder, key + ".txt"), string.Join(";", new[] { r.Lat, r.Lon, r.ElevM, r.NsMean, r.NsRange, r.RhMean, r.RainMm }.Select(v => v.ToString("0.###", Ci))));
            }
            catch { }
            return r;
        }
        catch (OperationCanceledException) when (!ct.IsCancellationRequested) { CalcLog.W("clima radio: Open-Meteo ERA5 non risponde in 15 s, uso la regola di riserva"); return null; }
        catch (Exception ex) when (ex is not OperationCanceledException) { CalcLog.W("clima radio: dati ERA5 non disponibili (" + ex.Message + "), uso la regola di riserva"); return null; }
    }

    /// <summary>Dalla risposta giornaliera di Open-Meteo: Ns media annua (media delle medie mensili), escursione mensile, umidità, pioggia annua.</summary>
    public static RadioClimateInfo Parse(string json, double la, double lo)
    {
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;
        if (!root.TryGetProperty("daily", out var d)) return null;
        var time = d.GetProperty("time");
        var t = d.GetProperty("temperature_2m_mean"); var rh = d.GetProperty("relative_humidity_2m_mean");
        var ps = d.GetProperty("surface_pressure_mean"); var pr = d.GetProperty("precipitation_sum");
        var sum = new double[12]; var cnt = new int[12];
        double rhSum = 0, rain = 0; int n = 0;
        var years = new HashSet<string>();
        for (int i = 0; i < time.GetArrayLength(); i++)
        {
            if (t[i].ValueKind != JsonValueKind.Number || rh[i].ValueKind != JsonValueKind.Number || ps[i].ValueKind != JsonValueKind.Number) continue;
            double tc = t[i].GetDouble(), u = rh[i].GetDouble(), p = ps[i].GetDouble();
            double tk = tc + 273.15;
            double es = 6.112 * Math.Exp(17.62 * tc / (243.12 + tc));
            double ns = 77.6 / tk * (p + 4810 * es * u / 100 / tk);
            string day = time[i].GetString() ?? "";
            if (day.Length < 7) continue;
            int m = int.Parse(day.Substring(5, 2), Ci) - 1;
            years.Add(day.Substring(0, 4));
            sum[m] += ns; cnt[m]++;
            rhSum += u; n++;
            if (pr[i].ValueKind == JsonValueKind.Number) rain += pr[i].GetDouble();
        }
        if (n < 300 || cnt.Any(c => c == 0)) return null;
        var mon = Enumerable.Range(0, 12).Select(i => sum[i] / cnt[i]).ToArray();
        double elev = root.TryGetProperty("elevation", out var e) && e.ValueKind == JsonValueKind.Number ? e.GetDouble() : 0;
        return new RadioClimateInfo
        {
            Lat = la, Lon = lo, ElevM = Math.Max(0, elev), NsMean = mon.Average(), NsRange = mon.Max() - mon.Min(),
            RhMean = rhSum / n, RainMm = rain / Math.Max(1, years.Count), FromEra5 = true
        };
    }

    /// <summary>
    /// Regole (TN101 § 10.2, criterio dell'autore per i confini): polare oltre 60° → continentale temperato (il TN101 non ha curve polari);
    /// pioggia &lt; 250 mm/anno e umidità &lt; 50% → desertico; |lat| ≤ 20°, Ns ≥ 350 ed escursione ≤ 30 → equatoriale;
    /// |lat| ≤ 32° ed escursione ≥ 60 → continentale subtropicale (monsoni); 10–32° con Ns ≥ 340 → marittimo subtropicale;
    /// entrambi gli orizzonti sul mare oltre 20° → marittimo temperato sul mare; escursione ≤ 30 vicino al mare (mare sulla tratta
    /// o umidità ≥ 75%) → marittimo temperato sulla terra; altrimenti continentale temperato.
    /// </summary>
    public static RadioClimateInfo Classify(RadioClimateInfo raw, double lat, double seaFrac, bool overSea)
    {
        var r = raw == null ? new RadioClimateInfo() : new RadioClimateInfo
        {
            NsMean = raw.NsMean, NsRange = raw.NsRange, RhMean = raw.RhMean, RainMm = raw.RainMm, ElevM = raw.ElevM, Lat = raw.Lat, Lon = raw.Lon, FromEra5 = true
        };
        double al = Math.Abs(lat);
        bool nearSea = seaFrac > 0.05;
        if (!r.FromEra5)
        {
            // riserva: solo latitudine e mare
            r.Climate = al > 60 ? Itm.Climate.ContinentalTemperate
                      : al <= 10 ? Itm.Climate.Equatorial
                      : overSea && al >= 20 ? Itm.Climate.MaritimeTemperateSea
                      : al < 20 ? Itm.Climate.MaritimeSubtropical
                      : nearSea ? Itm.Climate.MaritimeTemperateLand : Itm.Climate.ContinentalTemperate;
            r.N0 = TypicalN0(r.Climate);
            r.Why = "senza dati ERA5: latitudine e mare";
            return r;
        }
        double ns = r.NsMean, rg = r.NsRange;
        bool humid = r.RhMean >= 75;
        if (al > 60) { r.Climate = Itm.Climate.ContinentalTemperate; r.Why = "oltre 60° di latitudine"; }
        else if (r.RainMm < 250 && r.RhMean < 50) { r.Climate = Itm.Climate.Desert; r.Why = "aria secca e poca pioggia"; }
        else if (al <= 20 && ns >= 350 && rg <= 30) { r.Climate = Itm.Climate.Equatorial; r.Why = "Ns alta e costante"; }
        else if (al <= 32 && rg >= 60) { r.Climate = Itm.Climate.ContinentalSubtropical; r.Why = "forte escursione stagionale"; }
        else if (al >= 10 && al <= 32 && ns >= 340) { r.Climate = Itm.Climate.MaritimeSubtropical; r.Why = "Ns alta, fascia subtropicale"; }
        else if (overSea && al >= 20) { r.Climate = Itm.Climate.MaritimeTemperateSea; r.Why = "entrambi gli orizzonti sul mare"; }
        else if (rg <= 30 && (nearSea || humid)) { r.Climate = Itm.Climate.MaritimeTemperateLand; r.Why = "escursione piccola, vicino al mare"; }
        else { r.Climate = Itm.Climate.ContinentalTemperate; r.Why = "escursione stagionale media"; }
        r.N0 = Math.Max(250, Math.Min(400, ns * Math.Exp(r.ElevM / 9460.0)));
        return r;
    }
}
