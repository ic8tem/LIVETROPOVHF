namespace TropoVHF.Core;

/// <summary>
/// Posizione della Luna a bassa precisione (circa 0,3°), formule del The Astronomical Almanac, "Low precision formulae
/// for the Moon". Basta per sapere se la Luna è sopra l'orizzonte a entrambi i capi di un collegamento (EME).
/// </summary>
public static class Moon
{
    static double R(double d) => d * Math.PI / 180;
    static double S(double d) => Math.Sin(R(d));
    static double C(double d) => Math.Cos(R(d));

    /// <summary>Elevazione topocentrica della Luna in gradi.</summary>
    public static double Elevation(LatLon p, DateTime utc)
    {
        double jd = utc.ToOADate() + 2415018.5, d = jd - 2451545.0, T = d / 36525.0;
        double lam = 218.32 + 481267.881 * T + 6.29 * S(135.0 + 477198.87 * T) - 1.27 * S(259.3 - 413335.36 * T)
                   + 0.66 * S(235.7 + 890534.22 * T) + 0.21 * S(269.9 + 954397.74 * T) - 0.19 * S(357.5 + 35999.05 * T)
                   - 0.11 * S(186.5 + 966404.03 * T);
        double bet = 5.13 * S(93.3 + 483202.02 * T) + 0.28 * S(228.2 + 960400.89 * T) - 0.28 * S(318.3 + 6003.15 * T)
                   - 0.17 * S(217.6 - 407332.21 * T);
        double par = 0.9508 + 0.0518 * C(135.0 + 477198.87 * T) + 0.0095 * C(259.3 - 413335.36 * T)
                   + 0.0078 * C(235.7 + 890534.22 * T) + 0.0028 * C(269.9 + 954397.74 * T);
        double eps = 23.439 - 0.0000004 * d;
        // eclittiche → equatoriali
        double x = C(bet) * C(lam), y = C(eps) * C(bet) * S(lam) - S(eps) * S(bet), z = S(eps) * C(bet) * S(lam) + C(eps) * S(bet);
        double ra = Math.Atan2(y, x) * 180 / Math.PI, dec = Math.Asin(z) * 180 / Math.PI;
        double gmst = 280.46061837 + 360.98564736629 * d;
        double ha = gmst + p.Lon - ra;
        double el = Math.Asin(S(p.Lat) * S(dec) + C(p.Lat) * C(dec) * C(ha)) * 180 / Math.PI;
        return el - par * Math.Cos(R(el));      // parallasse: la Luna vista dalla superficie è più bassa
    }

    /// <summary>Luna sopra l'orizzonte (oltre minEl) a entrambi i capi: un collegamento via Luna è possibile.</summary>
    public static bool Common(LatLon a, LatLon b, DateTime utc, double minEl = 0) =>
        Elevation(a, utc) > minEl && Elevation(b, utc) > minEl;
}
