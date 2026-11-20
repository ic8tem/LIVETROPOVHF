using System.IO.Compression;
using System.Reflection;

namespace TropoVHF.Forecast;

/// <summary>Valori climatologici dei condotti in un punto (ITU-R P.453, mappe annuali a 1,5°).</summary>
public struct DuctClimate
{
    public bool Ok;
    public double SOccur, SThick, SStren;              // condotto al suolo: % del tempo, spessore (m), intensità (M)
    public double EOccur, EBase, EThick, EStren;       // condotto elevato: % del tempo, base (m), spessore (m), intensità (M)
}

/// <summary>
/// Climatologia dei condotti di ITU-R P.453 (prodotti digitali della Study Group 3, griglia 1,5° × 1,5°,
/// righe da 90°N a 90°S, colonne da 0° verso est fino a 360°). Fonte: © ITU, uso personale e non commerciale con
/// citazione della fonte. File ricavati dal pacchetto PathProfile di G0MJW e compressi in binario.
/// </summary>
public static class P453
{
    const int Rows = 121, Cols = 241;
    static float[][] _maps;
    static readonly object _lock = new();

    static float[][] Maps
    {
        get
        {
            lock (_lock)
            {
                if (_maps != null) return _maps;
                try
                {
                    using var s = Assembly.GetExecutingAssembly().GetManifestResourceStream("TropoVHF.Forecast.p453.bin.gz");
                    if (s == null) { CalcLog.W("P.453: dati climatologici non trovati nel programma"); return _maps = Array.Empty<float[]>(); }
                    using var gz = new GZipStream(s, CompressionMode.Decompress);
                    using var br = new BinaryReader(gz);
                    if (new string(br.ReadChars(4)) != "P453") throw new InvalidDataException("intestazione");
                    int n = br.ReadInt32(), r = br.ReadInt32(), c = br.ReadInt32();
                    if (r != Rows || c != Cols) throw new InvalidDataException("dimensioni");
                    var m = new float[n][];
                    for (int k = 0; k < n; k++)
                    {
                        m[k] = new float[r * c];
                        for (int i = 0; i < m[k].Length; i++) m[k][i] = br.ReadSingle();
                    }
                    CalcLog.W($"P.453: {n} mappe climatologiche dei condotti caricate (1,5°, © ITU)");
                    return _maps = m;
                }
                catch (Exception ex)
                {
                    CalcLog.W("P.453: errore nel caricamento dei dati: " + ex.Message);
                    return _maps = Array.Empty<float[]>();
                }
            }
        }
    }

    static double Bilinear(float[] m, double lat, double lon)
    {
        double r = (90 - Math.Max(-90, Math.Min(90, lat))) / 1.5;
        double c = (((lon % 360) + 360) % 360) / 1.5;
        int r0 = Math.Min(Rows - 2, (int)r), c0 = Math.Min(Cols - 2, (int)c);
        double tr = r - r0, tc = c - c0;
        return m[r0 * Cols + c0] * (1 - tr) * (1 - tc) + m[r0 * Cols + c0 + 1] * (1 - tr) * tc
             + m[(r0 + 1) * Cols + c0] * tr * (1 - tc) + m[(r0 + 1) * Cols + c0 + 1] * tr * tc;
    }

    public static DuctClimate At(double lat, double lon)
    {
        var m = Maps;
        if (m.Length < 7 || double.IsNaN(lat) || double.IsNaN(lon)) return default;
        return new DuctClimate
        {
            Ok = true,
            SOccur = Bilinear(m[0], lat, lon), SThick = Bilinear(m[1], lat, lon), SStren = Bilinear(m[2], lat, lon),
            EOccur = Bilinear(m[3], lat, lon), EBase = Bilinear(m[4], lat, lon), EThick = Bilinear(m[5], lat, lon), EStren = Bilinear(m[6], lat, lon)
        };
    }
}
