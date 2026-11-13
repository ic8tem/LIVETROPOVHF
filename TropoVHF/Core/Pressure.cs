using System.Globalization;
using System.Text.Json;

namespace TropoVHF.Core;

public sealed class IsoSegment
{
    public int Level;
    public LatLon A, B;
}

public sealed class PressureCenter
{
    public bool High;
    public double Value;
    public LatLon Pos;
}

/// <summary>
/// Meteo dal modello GFS (tramite Open-Meteo) sulla zona che si sta guardando, con un passo da 1° a 5° scelto per
/// restare sotto circa 800 punti: pressione al livello del mare (isobare ogni 4 hPa, centri H e L), temperatura a 2 m,
/// a 925 hPa e a 850 hPa (isoterme a 850 hPa ogni 4 °C e inversione termica).
/// </summary>
public sealed class Pressure
{
    public double LatMin = 30, LatMax = 72, LonMin = -30, LonMax = 48, Step = 3;
    public string Model = "gfs_seamless";
    public int RefreshHours = 6;
    public int ForecastHours = 198;

    public DateTime[] Times = Array.Empty<DateTime>();
    /// <summary>[tempo][riga lat][colonna lon]: pressione in hPa.</summary>
    public double[][,] Grid = Array.Empty<double[,]>();
    /// <summary>Temperature in °C, stessa griglia: a 2 m, a 925 hPa, a 850 hPa.</summary>
    public double[][,] T2 = Array.Empty<double[,]>(), T925 = Array.Empty<double[,]>(), T850 = Array.Empty<double[,]>();
    public int Rows, Cols;
    public string Status = "Isobare: in attesa";
    public DateTime LastUpdateUtc;
    public event Action Changed;

    readonly Dictionary<int, (List<IsoSegment> segs, List<PressureCenter> centers)> _cache = new();
    readonly Dictionary<int, List<IsoSegment>> _isoT = new();

    static readonly string[] Vars = { "pressure_msl", "temperature_2m", "temperature_925hPa", "temperature_850hPa" };

    /// <summary>La zona già scaricata contiene il riquadro richiesto, con un passo adatto?</summary>
    public bool Covers((double lat1, double lat2, double lon1, double lon2) box)
    {
        if (Grid.Length == 0) return false;
        if (box.lat1 < LatMin - 0.01 || box.lat2 > LatMax + 0.01 || box.lon1 < LonMin - 0.01 || box.lon2 > LonMax + 0.01) return false;
        return PickStep(box) >= Step - 0.01;    // ingrandendo serve un passo più fitto
    }

    static readonly double[] Steps = { 1, 1.5, 2, 2.5, 3, 4, 5, 6, 7.5, 10, 15, 20 };
    /// <summary>
    /// Punti massimi per aggiornamento. Open-Meteo conta ogni punto come una richiesta (limiti: 600 al minuto,
    /// 5000 all'ora, 10 000 al giorno): oltre, rifiuta tutto e isobare e temperature spariscono.
    /// </summary>
    public int MaxPoints = 400;

    double PickStep((double lat1, double lat2, double lon1, double lon2) b)
    {
        foreach (var s in Steps)
            if ((Math.Floor((b.lat2 - b.lat1) / s) + 2) * (Math.Floor((b.lon2 - b.lon1) / s) + 2) <= MaxPoints) return s;
        return Steps[^1];
    }

    /// <summary>Scarica i dati sul riquadro (lat/lon) indicato, allargato del 25%.</summary>
    public async Task<bool> UpdateAsync(HttpClient http, (double lat1, double lat2, double lon1, double lon2) view)
    {
        bool ok = false;
        try
        {
            Status = "Isobare: scarico i dati GFS…";
            Changed?.Invoke();
            double dl = (view.lat2 - view.lat1) * 0.25, dn = (view.lon2 - view.lon1) * 0.25;
            double b1 = Math.Max(-80, view.lat1 - dl), b2 = Math.Min(80, view.lat2 + dl), b3 = Math.Max(-180, view.lon1 - dn), b4 = Math.Min(180, view.lon2 + dn);
            double step = PickStep((b1, b2, b3, b4));
            double latMin = Math.Max(-80, Math.Floor(b1 / step) * step), latMax = Math.Min(80, Math.Ceiling(b2 / step) * step);
            double lonMin = Math.Max(-180, Math.Floor(b3 / step) * step), lonMax = Math.Min(180, Math.Ceiling(b4 / step) * step);
            int rows = (int)Math.Round((latMax - latMin) / step) + 1;
            int cols = (int)Math.Round((lonMax - lonMin) / step) + 1;
            var pts = new List<(int r, int c, double lat, double lon)>();
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    pts.Add((r, c, latMin + r * step, Math.Min(180, lonMin + c * step)));

            DateTime[] times = null;
            var values = new Dictionary<(int, int), double[][]>();
            const int batch = 100;
            for (int i = 0; i < pts.Count; i += batch)
            {
                var part = pts.Skip(i).Take(batch).ToList();
                var lat = string.Join(",", part.Select(p => p.lat.ToString("0.##", CultureInfo.InvariantCulture)));
                var lon = string.Join(",", part.Select(p => p.lon.ToString("0.##", CultureInfo.InvariantCulture)));
                var url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&hourly={string.Join(",", Vars)}&models={Model}&forecast_hours={ForecastHours}&timezone=GMT";
                using var resp = await http.GetAsync(url);
                if ((int)resp.StatusCode == 429) throw new Exception("Open-Meteo: troppe richieste, nuovo tentativo tra qualche minuto");
                resp.EnsureSuccessStatusCode();
                var json = await resp.Content.ReadAsStringAsync();
                await Task.Delay(300);     // richieste distanziate
                using var doc = JsonDocument.Parse(json);
                var items = doc.RootElement.ValueKind == JsonValueKind.Array
                    ? doc.RootElement.EnumerateArray().ToList()
                    : new List<JsonElement> { doc.RootElement };
                for (int k = 0; k < items.Count && k < part.Count; k++)
                {
                    var h = items[k].GetProperty("hourly");
                    if (times == null)
                        times = h.GetProperty("time").EnumerateArray()
                            .Select(t => DateTime.SpecifyKind(DateTime.Parse(t.GetString(), CultureInfo.InvariantCulture), DateTimeKind.Utc)).ToArray();
                    var arr = new double[Vars.Length][];
                    for (int v = 0; v < Vars.Length; v++)
                        arr[v] = h.TryGetProperty(Vars[v], out var a)
                            ? a.EnumerateArray().Select(x => x.ValueKind == JsonValueKind.Number ? x.GetDouble() : double.NaN).ToArray()
                            : Array.Empty<double>();
                    values[(part[k].r, part[k].c)] = arr;
                }
            }
            if (times == null || times.Length == 0) throw new Exception("nessun dato");
            double[][,] Field(int v)
            {
                var res = new double[times.Length][,];
                for (int t = 0; t < times.Length; t++)
                {
                    var g = new double[rows, cols];
                    for (int r = 0; r < rows; r++)
                        for (int c = 0; c < cols; c++)
                            g[r, c] = values.TryGetValue((r, c), out var a) && t < a[v].Length ? a[v][t] : double.NaN;
                    res[t] = g;
                }
                return res;
            }
            var p0 = Field(0); var t2 = Field(1); var t925 = Field(2); var t850 = Field(3);
            lock (_cache)
            {
                LatMin = latMin; LatMax = latMax; LonMin = lonMin; LonMax = lonMax; Step = step; Rows = rows; Cols = cols;
                Times = times; Grid = p0; T2 = t2; T925 = t925; T850 = t850;
                _cache.Clear(); _isoT.Clear();
            }
            LastUpdateUtc = DateTime.UtcNow;
            Status = $"Isobare e temperature: GFS aggiornato {LastUpdateUtc:HH:mm} UTC (passo {step:0.#}°, {pts.Count} punti)";
            ok = true;
        }
        catch (Exception ex)
        {
            // i dati già scaricati restano validi: si mostrano finché il nuovo tentativo non riesce
            Status = (Grid.Length > 0 ? "Isobare: aggiornamento non riuscito (" : "Isobare non disponibili (") + ex.Message + ")";
        }
        Changed?.Invoke();
        return ok;
    }

    int IndexFor(DateTime utc)
    {
        if (Times.Length == 0) return -1;
        int best = 0; double bd = double.MaxValue;
        for (int i = 0; i < Times.Length; i++)
        {
            double d = Math.Abs((Times[i] - utc).TotalMinutes);
            if (d < bd) { bd = d; best = i; }
        }
        return bd <= 90 ? best : -1;
    }

    double Interp(double[][,] field, LatLon p, DateTime utc)
    {
        double[,] g; double latMin, lonMin, step; int rows, cols;
        lock (_cache)
        {
            int ti = IndexFor(utc);
            if (ti < 0 || ti >= field.Length) return double.NaN;
            g = field[ti]; latMin = LatMin; lonMin = LonMin; step = Step; rows = Rows; cols = Cols;
        }
        double fr = (p.Lat - latMin) / step, fc = (p.Lon - lonMin) / step;
        if (fr < 0 || fc < 0 || fr > rows - 1 || fc > cols - 1 || rows < 2 || cols < 2) return double.NaN;
        int r0 = Math.Min(rows - 2, (int)fr), c0 = Math.Min(cols - 2, (int)fc);
        double tr = fr - r0, tc = fc - c0;
        return (1 - tr) * ((1 - tc) * g[r0, c0] + tc * g[r0, c0 + 1]) + tr * ((1 - tc) * g[r0 + 1, c0] + tc * g[r0 + 1, c0 + 1]);
    }

    public double ValueAt(LatLon p, DateTime utc) => Interp(Grid, p, utc);
    public double T850At(LatLon p, DateTime utc) => Interp(T850, p, utc);
    public double T925At(LatLon p, DateTime utc) => Interp(T925, p, utc);
    public double T2At(LatLon p, DateTime utc) => Interp(T2, p, utc);

    /// <summary>
    /// Inversione termica in °C: quanto è più calda l'aria a 925 o a 850 hPa rispetto a quella a 2 m.
    /// Positiva = la temperatura sale con la quota, la condizione che piega le onde verso terra.
    /// </summary>
    public double InversionAt(LatLon p, DateTime utc)
    {
        double t2 = T2At(p, utc), a = T925At(p, utc), b = T850At(p, utc);
        if (double.IsNaN(t2)) return double.NaN;
        double up = double.IsNaN(a) ? b : double.IsNaN(b) ? a : Math.Max(a, b);
        return double.IsNaN(up) ? double.NaN : up - t2;
    }

    /// <summary>Isobare e centri per l'ora richiesta (risultato in cache).</summary>
    public (List<IsoSegment> segs, List<PressureCenter> centers) ContoursFor(DateTime utc)
    {
        lock (_cache)
        {
            int ti = IndexFor(utc);
            if (ti < 0 || ti >= Grid.Length) return (new List<IsoSegment>(), new List<PressureCenter>());
            if (_cache.TryGetValue(ti, out var c)) return c;
            var res = (Contours(Grid[ti], 4), Centers(Grid[ti]));
            _cache[ti] = res;
            return res;
        }
    }

    /// <summary>Isoterme a 850 hPa ogni 4 °C per l'ora richiesta (risultato in cache).</summary>
    public List<IsoSegment> IsothermsFor(DateTime utc)
    {
        lock (_cache)
        {
            int ti = IndexFor(utc);
            if (ti < 0 || ti >= T850.Length) return new List<IsoSegment>();
            if (_isoT.TryGetValue(ti, out var c)) return c;
            var res = Contours(T850[ti], 4);
            _isoT[ti] = res;
            return res;
        }
    }

    /// <summary>Linee di livello con i quadrati marcianti, su una griglia ricampionata ×3 per linee più morbide.</summary>
    List<IsoSegment> Contours(double[,] g0, int interval)
    {
        var segs = new List<IsoSegment>();
        if (Rows < 2 || Cols < 2) return segs;
        const int k = 3;
        int R = (Rows - 1) * k + 1, C = (Cols - 1) * k + 1;
        double st = Step / k;
        var g = new double[R, C];
        for (int r = 0; r < R; r++)
            for (int c = 0; c < C; c++)
            {
                double fr = (double)r / k, fc = (double)c / k;
                int r0 = Math.Min(Rows - 2, (int)fr), c0 = Math.Min(Cols - 2, (int)fc);
                double tr = fr - r0, tc = fc - c0;
                g[r, c] = (1 - tr) * ((1 - tc) * g0[r0, c0] + tc * g0[r0, c0 + 1]) + tr * ((1 - tc) * g0[r0 + 1, c0] + tc * g0[r0 + 1, c0 + 1]);
            }

        double min = double.MaxValue, max = double.MinValue;
        foreach (var v in g) if (!double.IsNaN(v)) { min = Math.Min(min, v); max = Math.Max(max, v); }
        if (min > max) return segs;
        int lo = (int)Math.Ceiling(min / interval) * interval, hi = (int)Math.Floor(max / interval) * interval;

        LatLon P(double r, double c) => new LatLon(LatMin + r * st, LonMin + c * st);

        for (int level = lo; level <= hi; level += interval)
        {
            for (int r = 0; r < R - 1; r++)
                for (int c = 0; c < C - 1; c++)
                {
                    double v0 = g[r, c], v1 = g[r, c + 1], v2 = g[r + 1, c + 1], v3 = g[r + 1, c];
                    if (double.IsNaN(v0) || double.IsNaN(v1) || double.IsNaN(v2) || double.IsNaN(v3)) continue;
                    int idx = (v0 >= level ? 1 : 0) | (v1 >= level ? 2 : 0) | (v2 >= level ? 4 : 0) | (v3 >= level ? 8 : 0);
                    if (idx == 0 || idx == 15) continue;
                    double t(double a, double b) => Math.Abs(b - a) < 1e-9 ? 0.5 : (level - a) / (b - a);
                    // punti sui lati: 0 basso (v0-v1), 1 destro (v1-v2), 2 alto (v3-v2), 3 sinistro (v0-v3)
                    LatLon e0 = P(r, c + t(v0, v1));
                    LatLon e1 = P(r + t(v1, v2), c + 1);
                    LatLon e2 = P(r + 1, c + t(v3, v2));
                    LatLon e3 = P(r + t(v0, v3), c);
                    void Add(LatLon a, LatLon b) => segs.Add(new IsoSegment { Level = level, A = a, B = b });
                    switch (idx)
                    {
                        case 1: case 14: Add(e3, e0); break;
                        case 2: case 13: Add(e0, e1); break;
                        case 3: case 12: Add(e3, e1); break;
                        case 4: case 11: Add(e1, e2); break;
                        case 5: Add(e3, e0); Add(e1, e2); break;
                        case 6: case 9: Add(e0, e2); break;
                        case 7: case 8: Add(e3, e2); break;
                        case 10: Add(e0, e1); Add(e2, e3); break;
                    }
                }
        }
        return segs;
    }

    /// <summary>Centri H e L: estremi locali sulla griglia originale (5×5 celle).</summary>
    List<PressureCenter> Centers(double[,] g0)
    {
        var centers = new List<PressureCenter>();
        for (int r = 1; r < Rows - 1; r++)
            for (int c = 1; c < Cols - 1; c++)
            {
                double v = g0[r, c];
                if (double.IsNaN(v)) continue;
                bool isMax = true, isMin = true;
                for (int dr = -2; dr <= 2; dr++)
                    for (int dc = -2; dc <= 2; dc++)
                    {
                        if (dr == 0 && dc == 0) continue;
                        int rr = r + dr, cc = c + dc;
                        if (rr < 0 || cc < 0 || rr >= Rows || cc >= Cols) continue;
                        double w = g0[rr, cc];
                        if (double.IsNaN(w)) continue;
                        if (w >= v) isMax = false;
                        if (w <= v) isMin = false;
                    }
                if (isMax && v >= 1018) centers.Add(new PressureCenter { High = true, Value = v, Pos = new LatLon(LatMin + r * Step, LonMin + c * Step) });
                if (isMin && v <= 1008) centers.Add(new PressureCenter { High = false, Value = v, Pos = new LatLon(LatMin + r * Step, LonMin + c * Step) });
            }
        return centers;
    }
}
