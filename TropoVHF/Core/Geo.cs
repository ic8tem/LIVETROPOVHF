namespace TropoVHF.Core;

/// <summary>Punto geografico in gradi.</summary>
public readonly struct LatLon
{
    public readonly double Lat;
    public readonly double Lon;
    public LatLon(double lat, double lon) { Lat = lat; Lon = lon; }
    public override string ToString() => $"{Lat:0.00},{Lon:0.00}";
}

/// <summary>Locatore Maidenhead (4, 6, 8 o 10 caratteri) e conversioni.</summary>
public static class Locator
{
    public static bool TryToLatLon(string loc, out LatLon p)
    {
        p = default;
        if (string.IsNullOrWhiteSpace(loc)) return false;
        var l = loc.Trim().ToUpperInvariant();
        if (l.Length != 4 && l.Length != 6 && l.Length != 8 && l.Length != 10) return false;
        if (l[0] < 'A' || l[0] > 'R' || l[1] < 'A' || l[1] > 'R') return false;
        if (l[2] < '0' || l[2] > '9' || l[3] < '0' || l[3] > '9') return false;
        double lon = (l[0] - 'A') * 20 - 180 + (l[2] - '0') * 2;
        double lat = (l[1] - 'A') * 10 - 90 + (l[3] - '0');
        double w = 2, h = 1;
        // 6 caratteri: sottoquadrato; 8 e 10 caratteri (posizione precisa a circa 400 m e 20 m)
        if (l.Length >= 6)
        {
            if (l[4] < 'A' || l[4] > 'X' || l[5] < 'A' || l[5] > 'X') return false;
            w /= 24; h /= 24; lon += (l[4] - 'A') * w; lat += (l[5] - 'A') * h;
        }
        if (l.Length >= 8)
        {
            if (l[6] < '0' || l[6] > '9' || l[7] < '0' || l[7] > '9') return false;
            w /= 10; h /= 10; lon += (l[6] - '0') * w; lat += (l[7] - '0') * h;
        }
        if (l.Length >= 10)
        {
            if (l[8] < 'A' || l[8] > 'X' || l[9] < 'A' || l[9] > 'X') return false;
            w /= 24; h /= 24; lon += (l[8] - 'A') * w; lat += (l[9] - 'A') * h;
        }
        p = new LatLon(lat + h / 2, lon + w / 2);
        return true;
    }

    public static bool IsValid(string loc) => TryToLatLon(loc, out _);

    public static string FromLatLon(double lat, double lon)
    {
        double x = lon + 180, y = lat + 90;
        if (x < 0 || x >= 360 || y < 0 || y >= 180) return "";
        const string A = "ABCDEFGHIJKLMNOPQR";
        const string a = "abcdefghijklmnopqrstuvwx";
        var c = new char[6];
        c[0] = A[(int)(x / 20)];
        c[1] = A[(int)(y / 10)];
        c[2] = (char)('0' + (int)((x % 20) / 2));
        c[3] = (char)('0' + (int)(y % 10));
        c[4] = a[Math.Min(23, (int)((x % 2) * 12))];
        c[5] = a[Math.Min(23, (int)((y % 1) * 24))];
        return new string(c);
    }
}

public static class Geo
{
    public const double EarthKm = 6371.0;
    const double D2R = Math.PI / 180.0;

    public static double DistKm(LatLon a, LatLon b)
    {
        double dLat = (b.Lat - a.Lat) * D2R, dLon = (b.Lon - a.Lon) * D2R;
        double h = Math.Pow(Math.Sin(dLat / 2), 2) + Math.Cos(a.Lat * D2R) * Math.Cos(b.Lat * D2R) * Math.Pow(Math.Sin(dLon / 2), 2);
        return 2 * EarthKm * Math.Asin(Math.Min(1, Math.Sqrt(h)));
    }

    public static double Azimuth(LatLon a, LatLon b)
    {
        double dLon = (b.Lon - a.Lon) * D2R;
        double y = Math.Sin(dLon) * Math.Cos(b.Lat * D2R);
        double x = Math.Cos(a.Lat * D2R) * Math.Sin(b.Lat * D2R) - Math.Sin(a.Lat * D2R) * Math.Cos(b.Lat * D2R) * Math.Cos(dLon);
        return (Math.Atan2(y, x) / D2R + 360) % 360;
    }

    /// <summary>Punti sul cerchio massimo da a a b (n segmenti).</summary>
    /// <summary>Punto a una distanza (km) e un azimut (gradi) da un punto di partenza, sul cerchio massimo.</summary>
    public static LatLon Destination(LatLon from, double azDeg, double km)
    {
        double d = km / EarthKm, th = azDeg * Math.PI / 180;
        double f1 = from.Lat * Math.PI / 180, l1 = from.Lon * Math.PI / 180;
        double f2 = Math.Asin(Math.Sin(f1) * Math.Cos(d) + Math.Cos(f1) * Math.Sin(d) * Math.Cos(th));
        double l2 = l1 + Math.Atan2(Math.Sin(th) * Math.Sin(d) * Math.Cos(f1), Math.Cos(d) - Math.Sin(f1) * Math.Sin(f2));
        return new LatLon(f2 * 180 / Math.PI, ((l2 * 180 / Math.PI + 540) % 360) - 180);
    }

    public static List<LatLon> GreatCircle(LatLon a, LatLon b, int n)
    {
        var res = new List<LatLon>(n + 1);
        double la1 = a.Lat * D2R, lo1 = a.Lon * D2R, la2 = b.Lat * D2R, lo2 = b.Lon * D2R;
        double d = DistKm(a, b) / EarthKm;
        if (d < 1e-9) { res.Add(a); res.Add(b); return res; }
        for (int i = 0; i <= n; i++)
        {
            double f = (double)i / n;
            double A = Math.Sin((1 - f) * d) / Math.Sin(d), B = Math.Sin(f * d) / Math.Sin(d);
            double x = A * Math.Cos(la1) * Math.Cos(lo1) + B * Math.Cos(la2) * Math.Cos(lo2);
            double y = A * Math.Cos(la1) * Math.Sin(lo1) + B * Math.Cos(la2) * Math.Sin(lo2);
            double z = A * Math.Sin(la1) + B * Math.Sin(la2);
            res.Add(new LatLon(Math.Atan2(z, Math.Sqrt(x * x + y * y)) / D2R, Math.Atan2(y, x) / D2R));
        }
        return res;
    }

    /// <summary>Perdita di base per tropo scatter (G3YGF, QST nov. 1983), terra liscia, dB.</summary>
    public static double TroposcatterLossDb(double distKm, double freqMHz)
    {
        if (distKm < 1 || freqMHz <= 0) return 0;
        return 53.5 + 20 * Math.Log10(distKm) + 30 * Math.Log10(freqMHz) + 0.0674 * distKm;
    }
}

/// <summary>Bande gestite e segmenti accettati.</summary>
public static class Bands
{
    // il programma lavora dai 144 MHz in su; sotto, solo GRAVES (143,050 MHz), mostrato con i 144
    public static readonly string[] All = { "144", "222", "432", "1296", "2320", "10368" };

    public static string Label(string b) => b == "10368" ? "10 GHz" : b + " MHz";

    /// <summary>Banda per una frequenza in kHz, oppure "" (sotto i 143 MHz o fuori segmento). GRAVES va con i 144.</summary>
    public static string FromKHz(double kHz)
    {
        if (Graves.IsGraves(kHz)) return "144";
        if (kHz < 143000) return "";
        if (kHz >= 144000 && kHz <= 148000) return "144";
        if (kHz >= 220000 && kHz <= 225000) return "222";
        if (kHz >= 420000 && kHz <= 450000) return "432";
        if (kHz >= 1240000 && kHz <= 1300000) return "1296";
        // QO-100 (satellite geostazionario): salita 2400,0–2400,5 MHz e discesa 10489,5–10490 MHz non sono propagazione tropo
        if (kHz >= 2400000 && kHz <= 2400500) return "";
        if (kHz >= 10489500 && kHz <= 10490000) return "";
        if (kHz >= 2300000 && kHz <= 2450000) return "2320";
        if (kHz >= 10000000 && kHz <= 10500000) return "10368";
        return "";
    }
}
