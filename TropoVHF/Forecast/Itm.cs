using System.Numerics;

namespace TropoVHF.Forecast;

/// <summary>
/// Irregular Terrain Model (Longley-Rice) v1.2.2, modo punto-punto: traduzione in C# del codice ufficiale NTIA/ITS
/// (github.com/NTIA/itm, opera del governo USA, libera da diritti; vedi la nota di rilascio NTIA).
/// Dà la perdita di base in "atmosfera normale" (Ns, sette climi) su un profilo reale: spazio libero, vista con
/// riflessione dal suolo, diffrazione (spigolo + terra sferica pesati sulla rugosità Δh) e troposcatter,
/// più la variabilità statistica per tempo, luogo e situazione. NON modella i condotti.
/// Profilo pfl come nell'originale: pfl[0] = numero di intervalli, pfl[1] = passo in metri, poi le quote (np+1 valori).
/// </summary>
public static class Itm
{
    public enum Climate { Equatorial = 1, ContinentalSubtropical, MaritimeSubtropical, Desert, ContinentalTemperate, MaritimeTemperateLand, MaritimeTemperateSea }
    public const int ModeLos = 1, ModeDiffraction = 2, ModeTroposcatter = 3;

    public sealed class Result
    {
        public int Error;               // 0 = nessun errore (codici NTIA altrimenti)
        public long Warnings;
        public double AdB;              // perdita di base totale (spazio libero + A_ref + variabilità)
        public double ArefDb;           // attenuazione di riferimento sopra lo spazio libero (mediana)
        public double AfsDb;            // spazio libero
        public double Ns, DeltaH, Dkm;
        public double[] ThetaHzn = new double[2], DHzn = new double[2], He = new double[2];
        public int Mode;                // 1 vista, 2 diffrazione, 3 troposcatter
        public string ModeName => Mode switch { ModeLos => "vista", ModeDiffraction => "diffrazione", ModeTroposcatter => "troposcatter", _ => "?" };
    }

    const double A0 = 6370e3, A9000 = 9000e3, THIRD = 1.0 / 3.0;
    static double Dim(double x, double y) => x > y ? x - y : 0;

    /// <summary>ITM_P2P_TLS: time, location, situation in percento (0–100); mdvar come in NTIA (12 = broadcast senza variabilità di luogo? vedi documentazione: 0 messaggio singolo, 1 accidentale, 2 mobile, 3 broadcast, +10 senza variabilità di luogo, +20 senza variabilità di situazione).</summary>
    public static Result P2P(double hTx, double hRx, double[] pfl, Climate climate, double n0, double fMHz, bool vertical,
        double epsilon, double sigma, int mdvar, double time, double location, double situation)
    {
        var r = new Result();
        if (hTx < 0.5 || hTx > 3000 || hRx < 0.5 || hRx > 3000) { r.Error = 1000; return r; }
        if (n0 < 250 || n0 > 400) { r.Error = 1004; return r; }
        if (fMHz < 20 || fMHz > 20000) { r.Error = 1005; return r; }
        if (time <= 0 || time >= 100 || location <= 0 || location >= 100 || situation <= 0 || situation >= 100) { r.Error = 1002; return r; }
        r.Dkm = pfl[0] * pfl[1] / 1000;
        int np = (int)pfl[0];
        int p10 = (int)(0.1 * np);
        double hSys = 0;
        for (int i = p10; i <= np - p10; i++) hSys += pfl[i + 2];
        hSys /= (np - 2 * p10 + 1);

        // InitializePointToPoint
        double ns = hSys == 0.0 ? n0 : n0 * Math.Exp(-hSys / 9460.0);
        double gammaE = 157e-9 * (1.0 - 0.04665 * Math.Exp(ns / 179.3));
        var epR = new Complex(epsilon, 18000 * sigma / fMHz);
        var zg = Complex.Sqrt(epR - 1.0);
        if (vertical) zg /= epR;

        var h = new[] { hTx, hRx };
        var th = new double[2]; var dh = new double[2]; var he = new double[2];
        QuickPfl(pfl, gammaE, h, th, dh, he, out double deltaH, out double d);
        long warn = 0;
        int err = LongleyRice(th, fMHz, zg, dh, he, gammaE, ns, deltaH, h, d, ref warn, out double aRef, out int mode);
        r.Ns = ns; r.DeltaH = deltaH; r.ThetaHzn = th; r.DHzn = dh; r.He = he; r.Mode = mode;
        if (err != 0) { r.Error = err; r.Warnings = warn; return r; }
        double afs = FreeSpaceLoss(d, fMHz);
        r.AdB = Variability(time, location, situation, he, deltaH, fMHz, d, aRef, (int)climate, mdvar, ref warn) + afs;
        r.ArefDb = aRef; r.AfsDb = afs; r.Warnings = warn;
        return r;
    }

    /// <summary>
    /// Troposcatter di NBS TN101 § 9 (Rice, Longley, Norton e Barsis, 1967), perdita di base mediana:
    /// L = 30 log f − 20 log d + F(θd) − F0 + H0, con F(θd) dalle funzioni analitiche dell'allegato III (Ns 301) corrette per Ns,
    /// la funzione di guadagno in frequenza H0 (§ 9.2) e l'efficienza di diffusione; è la stessa forma che l'ITM usa al proprio interno.
    /// Qui Ns è quello dei profili GFS e gli orizzonti vengono dal profilo reale. Restituisce NaN se fuori dal campo del metodo.
    /// </summary>
    public static double Tn101Scatter(double hTx, double hRx, double[] pfl, double ns, double fMHz, out double h0Db, out double fThetaD)
    {
        h0Db = double.NaN; fThetaD = double.NaN;
        if (ns < 250 || ns > 400 || fMHz < 20 || fMHz > 20000) return double.NaN;
        double gammaE = 157e-9 * (1.0 - 0.04665 * Math.Exp(ns / 179.3));
        var h = new[] { Math.Max(0.5, hTx), Math.Max(0.5, hRx) };
        var th = new double[2]; var dh = new double[2]; var he = new double[2];
        QuickPfl(pfl, gammaE, h, th, dh, he, out _, out double d);
        double ae = 1 / gammaE;
        double dML = dh[0] + dh[1];
        if (d <= dML) return double.NaN;                     // in vista: il troposcatter non conta
        double thetaLos = -Math.Max(th[0] + th[1], -dML / ae);
        double h0 = -1;
        double a = TroposcatterLoss(d, th, dh, he, ae, ns, fMHz, thetaLos, ref h0);
        if (a >= 1000 || double.IsNaN(a)) return double.NaN;
        h0Db = h0; fThetaD = FFunction((d / ae - thetaLos) * d);
        return FreeSpaceLoss(d, fMHz) + Math.Max(0, a);
    }

    public static double FreeSpaceLoss(double d, double f) => 32.45 + 20.0 * Math.Log10(f) + 20.0 * Math.Log10(d / 1000.0);

    static void QuickPfl(double[] pfl, double gammaE, double[] h, double[] thetaHzn, double[] dHzn, double[] he, out double deltaH, out double d)
    {
        d = pfl[0] * pfl[1];
        int np = (int)pfl[0];
        double ae = 1 / gammaE;
        FindHorizons(pfl, ae, h, thetaHzn, dHzn);
        double dStart = Math.Min(15.0 * h[0], 0.1 * dHzn[0]);
        double dEnd = d - Math.Min(15.0 * h[1], 0.1 * dHzn[1]);
        deltaH = ComputeDeltaH(pfl, dStart, dEnd);
        double fitTx, fitRx, q;
        if (dHzn[0] + dHzn[1] > 1.5 * d)
        {
            LinearLeastSquaresFit(pfl, dStart, dEnd, out fitTx, out fitRx);
            he[0] = h[0] + Dim(pfl[2], fitTx);
            he[1] = h[1] + Dim(pfl[np + 2], fitRx);
            for (int i = 0; i < 2; i++)
                dHzn[i] = Math.Sqrt(2.0 * he[i] * ae) * Math.Exp(-0.07 * Math.Sqrt(deltaH / Math.Max(he[i], 5.0)));
            double comb = dHzn[0] + dHzn[1];
            if (comb <= d)
            {
                q = Math.Pow(d / comb, 2);
                for (int i = 0; i < 2; i++)
                {
                    he[i] *= q;
                    dHzn[i] = Math.Sqrt(2.0 * he[i] * ae) * Math.Exp(-0.07 * Math.Sqrt(deltaH / Math.Max(he[i], 5.0)));
                }
            }
            for (int i = 0; i < 2; i++)
            {
                q = Math.Sqrt(2.0 * he[i] * ae);
                thetaHzn[i] = (0.65 * deltaH * (q / dHzn[i] - 1.0) - 2.0 * he[i]) / q;
            }
        }
        else
        {
            LinearLeastSquaresFit(pfl, dStart, 0.9 * dHzn[0], out fitTx, out _);
            he[0] = h[0] + Dim(pfl[2], fitTx);
            LinearLeastSquaresFit(pfl, d - 0.9 * dHzn[1], dEnd, out _, out fitRx);
            he[1] = h[1] + Dim(pfl[np + 2], fitRx);
        }
    }

    static void FindHorizons(double[] pfl, double ae, double[] h, double[] thetaHzn, double[] dHzn)
    {
        int np = (int)pfl[0];
        double xi = pfl[1];
        double d = pfl[0] * pfl[1];
        double zTx = pfl[2] + h[0];
        double zRx = pfl[np + 2] + h[1];
        thetaHzn[0] = (zRx - zTx) / d - d / (2 * ae);
        thetaHzn[1] = -(zRx - zTx) / d - d / (2 * ae);
        dHzn[0] = d; dHzn[1] = d;
        double dTx = 0.0, dRx = d;
        for (int i = 1; i < np; i++)
        {
            dTx += xi; dRx -= xi;
            double tTx = (pfl[i + 2] - zTx) / dTx - dTx / (2 * ae);
            double tRx = -(zRx - pfl[i + 2]) / dRx - dRx / (2 * ae);
            if (tTx > thetaHzn[0]) { thetaHzn[0] = tTx; dHzn[0] = dTx; }
            if (tRx > thetaHzn[1]) { thetaHzn[1] = tRx; dHzn[1] = dRx; }
        }
    }

    static double ComputeDeltaH(double[] pfl, double dStart, double dEnd)
    {
        var s = new double[247];
        int np = (int)pfl[0];
        double xStart = dStart / pfl[1];
        double xEnd = dEnd / pfl[1];
        if (xEnd - xStart < 2.0) return 0;
        int p10 = (int)(0.1 * (xEnd - xStart + 8.0));
        p10 = Math.Min(Math.Max(4, p10), 25);
        int n = 10 * p10 - 5;
        int p90 = n - p10;
        double npS = n - 1;
        s[0] = npS; s[1] = 1.0;
        xEnd = (xEnd - xStart) / npS;
        int i = (int)xStart;
        xStart -= (float)(i + 1.0);
        for (int j = 0; j < n; j++)
        {
            while (xStart > 0.0 && (i + 1) < np) { xStart--; i++; }
            s[j + 2] = pfl[i + 3] + (pfl[i + 3] - pfl[i + 2]) * xStart;
            xStart += xEnd;
        }
        LinearLeastSquaresFit(s, 0.0, npS, out double y1, out double y2);
        y2 = (y2 - y1) / npS;
        var diffs = new double[n];
        for (int j = 0; j < n; j++) { diffs[j] = s[j + 2] - y1; y1 += y2; }
        Array.Sort(diffs); Array.Reverse(diffs);           // decrescente, come nth_element con greater
        double q10 = diffs[p10 - 1], q90 = diffs[p90];
        return (q10 - q90) / (1.0 - 0.8 * Math.Exp(-(dEnd - dStart) / 50e3));
    }

    static void LinearLeastSquaresFit(double[] pfl, double dStart, double dEnd, out double fitY1, out double fitY2)
    {
        int np = (int)pfl[0];
        int iStart = (int)Dim(dStart / pfl[1], 0.0);
        int iEnd = np - (int)Dim(np, dEnd / pfl[1]);
        if (iEnd <= iStart)
        {
            iStart = (int)Dim(iStart, 1.0);
            iEnd = np - (int)Dim(np, iEnd + 1.0);
        }
        double xLen = iEnd - iStart;
        double mid = -0.5 * xLen;
        double midEnd = iEnd + mid;
        double sumY = 0.5 * (pfl[iStart + 2] + pfl[iEnd + 2]);
        double scaled = 0.5 * (pfl[iStart + 2] - pfl[iEnd + 2]) * mid;
        for (int i = 2; i <= xLen; i++)
        {
            iStart++; mid++;
            sumY += pfl[iStart + 2];
            scaled += pfl[iStart + 2] * mid;
        }
        sumY /= xLen;
        scaled = scaled * 12.0 / ((xLen * xLen + 2.0) * xLen);
        fitY1 = sumY - scaled * midEnd;
        fitY2 = sumY + scaled * (np - midEnd);
    }

    static int LongleyRice(double[] thetaHzn, double f, Complex zg, double[] dHzn, double[] he, double gammaE, double ns,
        double deltaH, double[] h, double d, ref long warn, out double aRef, out int mode)
    {
        aRef = 0; mode = 0;
        double ae = 1 / gammaE;
        var dHznS = new double[2];
        for (int i = 0; i < 2; i++) dHznS[i] = Math.Sqrt(2.0 * he[i] * ae);
        double dSML = dHznS[0] + dHznS[1];
        double dML = dHzn[0] + dHzn[1];
        double thetaLos = -Math.Max(thetaHzn[0] + thetaHzn[1], -dML / ae);
        if (Math.Abs(thetaHzn[0]) > 200e-3) warn |= 0x1;
        if (Math.Abs(thetaHzn[1]) > 200e-3) warn |= 0x2;
        if (ns < 150) return 1010;
        if (ns > 400) return 1011;
        if (ae < 4000000 || ae > 13333333) return 1012;
        if (zg.Real <= Math.Abs(zg.Imaginary)) return 1013;
        double c3 = Math.Pow(ae * ae / f, THIRD);
        double d3 = Math.Max(dSML, dML + 5.0 * c3);
        double d4 = d3 + 10.0 * c3;
        double a3 = DiffractionLoss(d3, dHzn, he, zg, ae, deltaH, h, thetaLos, dSML, f);
        double a4 = DiffractionLoss(d4, dHzn, he, zg, ae, deltaH, h, thetaLos, dSML, f);
        double md = (a4 - a3) / (d4 - d3);
        double ad0 = a3 - md * d3;
        if (d > 1000e3) warn |= 0x100;
        if (d < dSML)
        {
            double aSML = dSML * md + ad0;
            double d0 = 0.04 * f * he[0] * he[1];
            double d1;
            if (ad0 >= 0.0)
            {
                d0 = Math.Min(d0, 0.5 * dML);
                d1 = d0 + 0.25 * (dML - d0);
            }
            else d1 = Math.Max(-ad0 / md, 0.25 * dML);
            double a1 = LineOfSightLoss(d1, he, zg, deltaH, md, ad0, dSML, f);
            bool flag = false;
            double k1 = 0, k2 = 0;
            if (d0 < d1)
            {
                double a0 = LineOfSightLoss(d0, he, zg, deltaH, md, ad0, dSML, f);
                double q = Math.Log(dSML / d0);
                k2 = Math.Max(0.0, ((dSML - d0) * (a1 - a0) - (d1 - d0) * (aSML - a0)) / ((dSML - d0) * Math.Log(d1 / d0) - (d1 - d0) * q));
                flag = ad0 > 0.0 || k2 > 0.0;
                if (flag)
                {
                    k1 = (aSML - a0 - k2 * q) / (dSML - d0);
                    if (k1 < 0.0)
                    {
                        k1 = 0.0;
                        k2 = Dim(aSML, a0) / q;
                        if (k2 == 0.0) k1 = md;
                    }
                }
            }
            if (!flag)
            {
                k1 = Dim(aSML, a1) / (dSML - d1);
                k2 = 0.0;
                if (k1 == 0.0) k1 = md;
            }
            double ao = aSML - k1 * dSML - k2 * Math.Log(dSML);
            aRef = ao + k1 * d + k2 * Math.Log(d);
            mode = ModeLos;
        }
        else
        {
            double d5 = dML + 200e3, d6 = dML + 400e3;
            double h0 = -1;
            double a6 = TroposcatterLoss(d6, thetaHzn, dHzn, he, ae, ns, f, thetaLos, ref h0);
            double a5 = TroposcatterLoss(d5, thetaHzn, dHzn, he, ae, ns, f, thetaLos, ref h0);
            double ms, as0, dx;
            if (a5 < 1000.0)
            {
                ms = (a6 - a5) / 200e3;
                dx = Math.Max(Math.Max(dSML, dML + 1.088 * c3 * Math.Log(f)), (a5 - ad0 - ms * d5) / (md - ms));
                as0 = (md - ms) * dx + ad0;
            }
            else { ms = md; as0 = ad0; dx = 10e6; }
            if (d > dx) { aRef = ms * d + as0; mode = ModeTroposcatter; }
            else { aRef = md * d + ad0; mode = ModeDiffraction; }
        }
        aRef = Math.Max(aRef, 0.0);
        return 0;
    }

    static double DiffractionLoss(double d, double[] dHzn, double[] he, Complex zg, double ae, double deltaH, double[] h,
        double thetaLos, double dSML, double f)
    {
        double ak = KnifeEdgeDiffraction(d, f, ae, thetaLos, dHzn);
        double ase = SmoothEarthDiffraction(d, f, ae, thetaLos, dHzn, he, zg);
        double sigmaHd = SigmaH(TerrainRoughness(dSML, deltaH));
        double afo = Math.Min(15.0, 5 * Math.Log10(1.0 + 1e-5 * h[0] * h[1] * f * sigmaHd));
        double dhd = TerrainRoughness(d, deltaH);
        double q = h[0] * h[1];
        double qk = he[0] * he[1] - q;
        q += 10.0;                                       // modo punto-punto
        double term1 = Math.Sqrt(1.0 + qk / q);
        double dML = dHzn[0] + dHzn[1];
        q = (term1 + (-thetaLos * ae + dML) / d) * Math.Min(dhd * f / 47.7, 6283.2);
        double w = 25.1 / (25.1 + Math.Sqrt(q));
        return w * ase + (1.0 - w) * ak + afo;
    }

    static double Fresnel(double v2) => v2 < 5.76 ? 6.02 + 9.11 * Math.Sqrt(v2) - 1.27 * v2 : 12.953 + 10 * Math.Log10(v2);

    static double KnifeEdgeDiffraction(double d, double f, double ae, double thetaLos, double[] dHzn)
    {
        double dML = dHzn[0] + dHzn[1];
        double thNlos = d / ae - thetaLos;
        double dNlos = d - dML;
        double v1 = 0.0795775 * (f / 47.7) * thNlos * thNlos * dHzn[0] * dNlos / (dNlos + dHzn[0]);
        double v2 = 0.0795775 * (f / 47.7) * thNlos * thNlos * dHzn[1] * dNlos / (dNlos + dHzn[1]);
        return Fresnel(v1) + Fresnel(v2);
    }

    static double SmoothEarthDiffraction(double d, double f, double ae, double thetaLos, double[] dHzn, double[] he, Complex zg)
    {
        var a = new double[3]; var dkm = new double[3]; var k = new double[3]; var b0 = new double[3]; var x = new double[3]; var c0 = new double[3];
        double thNlos = d / ae - thetaLos;
        double dML = dHzn[0] + dHzn[1];
        a[0] = (d - dML) / (d / ae - thetaLos);
        a[1] = 0.5 * dHzn[0] * dHzn[0] / he[0];
        a[2] = 0.5 * dHzn[1] * dHzn[1] / he[1];
        dkm[0] = a[0] * thNlos / 1000.0;
        dkm[1] = dHzn[0] / 1000.0;
        dkm[2] = dHzn[1] / 1000.0;
        for (int i = 0; i < 3; i++)
        {
            c0[i] = Math.Pow((4.0 / 3.0) * A0 / a[i], THIRD);
            k[i] = 0.017778 * c0[i] * Math.Pow(f, -THIRD) / Complex.Abs(zg);
            b0[i] = 1.607 - k[i];
        }
        x[1] = b0[1] * c0[1] * c0[1] * Math.Pow(f, THIRD) * dkm[1];
        x[2] = b0[2] * c0[2] * c0[2] * Math.Pow(f, THIRD) * dkm[2];
        x[0] = b0[0] * c0[0] * c0[0] * Math.Pow(f, THIRD) * dkm[0] + x[1] + x[2];
        double f1 = HeightFunction(x[1], k[1]);
        double f2 = HeightFunction(x[2], k[2]);
        double g = 0.05751 * x[0] - 10.0 * Math.Log10(x[0]);
        return g - f1 - f2 - 20;
    }

    static double HeightFunction(double x, double k)
    {
        double w, result;
        if (x < 200.0)
        {
            w = -Math.Log(k);
            if (k < 1e-5 || x * w * w * w > 5495.0)
            {
                result = -117.0;
                if (x > 1.0) result = 17.372 * Math.Log(x) + result;
            }
            else result = 2.5e-5 * x * x / k - 8.686 * w - 15.0;
        }
        else
        {
            result = 0.05751 * x - 4.343 * Math.Log(x);
            if (x < 2000)
            {
                w = 0.0134 * x * Math.Exp(-0.005 * x);
                result = (1.0 - w) * result + w * (17.372 * Math.Log(x) - 117.0);
            }
        }
        return result;
    }

    static double LineOfSightLoss(double d, double[] he, Complex zg, double deltaH, double md, double ad0, double dSML, double f)
    {
        double sigmaHd = SigmaH(TerrainRoughness(d, deltaH));
        double wn = f / 47.7;
        double sinPsi = (he[0] + he[1]) / Math.Sqrt(d * d + Math.Pow(he[0] + he[1], 2));
        Complex re = (sinPsi - zg) / (sinPsi + zg) * Math.Exp(-Math.Min(10.0, wn * sigmaHd * sinPsi));
        double q = re.Real * re.Real + re.Imaginary * re.Imaginary;
        if (q < 0.25 || q < sinPsi) re *= Math.Sqrt(sinPsi / q);
        double dPhi = wn * 2.0 * he[0] * he[1] / d;
        if (dPhi > Math.PI / 2.0) dPhi = Math.PI - Math.Pow(Math.PI / 2.0, 2) / dPhi;
        var rr = new Complex(Math.Cos(dPhi), -Math.Sin(dPhi)) + re;
        double at = -10 * Math.Log10(rr.Real * rr.Real + rr.Imaginary * rr.Imaginary);
        double ad = md * d + ad0;
        double w = 1 / (1 + f * deltaH / Math.Max(10e3, dSML));
        return w * at + (1 - w) * ad;
    }

    static double H0Curve(int j, double r)
    {
        double[] a = { 25.0, 80.0, 177.0, 395.0, 705.0 };
        double[] b = { 24.0, 45.0, 68.0, 80.0, 105.0 };
        return 10 * Math.Log10(1 + a[j] * Math.Pow(1 / r, 4) + b[j] * Math.Pow(1.0 / r, 2));
    }

    static double H0Function(double r, double etaS)
    {
        etaS = Math.Min(Math.Max(etaS, 1), 5);
        int i = (int)etaS;
        double q = etaS - i;
        double result = H0Curve(i - 1, r);
        if (q != 0.0) result = (1.0 - q) * result + q * H0Curve(i, r);
        return result;
    }

    static double FFunction(double td)
    {
        double[] a = { 133.4, 104.6, 71.8 };
        double[] b = { 0.332e-3, 0.212e-3, 0.157e-3 };
        double[] c = { -10, -2.5, 5 };
        int i = td <= 10e3 ? 0 : td <= 70e3 ? 1 : 2;
        return a[i] + b[i] * td + c[i] * Math.Log10(td);
    }

    static double TroposcatterLoss(double d, double[] thetaHzn, double[] dHzn, double[] he, double ae, double ns, double f, double thetaLos, ref double h0)
    {
        double H0;
        double wn = f / 47.7;
        if (h0 > 15.0) H0 = h0;
        else
        {
            double ad = dHzn[0] - dHzn[1];
            double rr = he[1] / he[0];
            if (ad < 0.0) { ad = -ad; rr = 1.0 / rr; }
            double theta = thetaHzn[0] + thetaHzn[1] + d / ae;
            double r1 = 2.0 * wn * theta * he[0];
            double r2 = 2.0 * wn * theta * he[1];
            if (r1 < 0.2 && r2 < 0.2) return 1001;
            double s = (d - ad) / (d + ad);
            double q = Math.Min(Math.Max(0.1, rr / s), 10.0);
            s = Math.Max(0.1, s);
            double h0m = (d - ad) * (d + ad) * theta * 0.25 / d;
            const double Z0 = 1.7556e3, Z1 = 8.0e3;
            double etaS = (h0m / Z0) * (1.0 + (0.031 - ns * 2.32e-3 + ns * ns * 5.67e-6) * Math.Exp(-Math.Pow(Math.Min(1.7, h0m / Z1), 6)));
            double h00 = (H0Function(r1, etaS) + H0Function(r2, etaS)) / 2;
            double dH0 = Math.Min(h00, 6.0 * (0.6 - Math.Log10(Math.Max(etaS, 1.0))) * Math.Log10(s) * Math.Log10(q));
            H0 = Math.Max(h00 + dH0, 0.0);
            if (etaS < 1.0)
                H0 = etaS * H0 + (1.0 - etaS) * 10 * Math.Log10(Math.Pow((1.0 + Math.Sqrt(2) / r1) * (1.0 + Math.Sqrt(2) / r2), 2) * (r1 + r2) / (r1 + r2 + 2 * Math.Sqrt(2)));
            if (H0 > 15.0 && h0 >= 0.0) H0 = h0;
        }
        h0 = H0;
        double th = d / ae - thetaLos;
        const double D0 = 40e3, Hm = 47.7;
        return FFunction(th * d) + 10 * Math.Log10(wn * Hm * Math.Pow(th, 4)) - 0.1 * (ns - 301.0) * Math.Exp(-th * d / D0) + H0;
    }

    static double SigmaH(double dh) => 0.78 * dh * Math.Exp(-0.5 * Math.Pow(dh, 0.25));
    static double TerrainRoughness(double d, double dh) => dh * (1.0 - 0.8 * Math.Exp(-d / 50e3));

    static double InvQ(double q)
    {
        const double C0 = 2.515516, C1 = 0.802853, C2 = 0.010328, D1 = 1.432788, D2 = 0.189269, D3 = 0.001308;
        double x = q > 0.5 ? 1.0 - q : q;
        double t = Math.Sqrt(-2.0 * Math.Log(x));
        double z = ((C2 * t + C1) * t + C0) / (((D3 * t + D2) * t + D1) * t + 1.0);
        double r = t - z;
        return q > 0.5 ? -r : r;
    }

    static double Curve(double c1, double c2, double x1, double x2, double x3, double de) =>
        (c1 + c2 / (1.0 + Math.Pow((de - x2) / x3, 2))) * Math.Pow(de / x1, 2) / (1.0 + Math.Pow(de / x1, 2));

    static readonly double[,] AllYear =
    {
        {  -9.67,   -0.62,    1.26,   -9.21,   -0.62,   -0.39,      3.15 },
        {  12.7,     9.19,   15.5,     9.05,    9.19,    2.86,   857.9   },
        { 144.9e3, 228.9e3, 262.6e3,  84.1e3, 228.9e3, 141.7e3, 2222.0e3 },
        { 190.3e3, 205.2e3, 185.2e3, 101.1e3, 205.2e3, 315.9e3,  164.8e3 },
        { 133.8e3, 143.6e3,  99.8e3,  98.6e3, 143.6e3, 167.4e3,  116.3e3 }
    };
    static readonly double[] Bsm1 = { 2.13, 2.66, 6.11, 1.98, 2.68, 6.86, 8.51 };
    static readonly double[] Bsm2 = { 159.5, 7.67, 6.65, 13.11, 7.16, 10.38, 169.8 };
    static readonly double[] Xsm1 = { 762.2e3, 100.4e3, 138.2e3, 139.1e3, 93.7e3, 187.8e3, 609.8e3 };
    static readonly double[] Xsm2 = { 123.6e3, 172.5e3, 242.2e3, 132.7e3, 186.8e3, 169.6e3, 119.9e3 };
    static readonly double[] Xsm3 = { 94.5e3, 136.4e3, 178.6e3, 193.5e3, 133.5e3, 108.9e3, 106.6e3 };
    static readonly double[] Bsp1 = { 2.11, 6.87, 10.08, 3.68, 4.75, 8.58, 8.43 };
    static readonly double[] Bsp2 = { 102.3, 15.53, 9.60, 159.3, 8.12, 13.97, 8.19 };
    static readonly double[] Xsp1 = { 636.9e3, 138.7e3, 165.3e3, 464.4e3, 93.2e3, 216.0e3, 136.2e3 };
    static readonly double[] Xsp2 = { 134.8e3, 143.7e3, 225.7e3, 93.1e3, 135.9e3, 152.0e3, 188.5e3 };
    static readonly double[] Xsp3 = { 95.6e3, 98.6e3, 129.7e3, 94.2e3, 113.4e3, 122.7e3, 122.9e3 };
    static readonly double[] CD = { 1.224, 0.801, 1.380, 1.000, 1.224, 1.518, 1.518 };
    static readonly double[] ZD = { 1.282, 2.161, 1.282, 20.0, 1.282, 1.282, 1.282 };
    static readonly double[] Bfm1 = { 1.0, 1.0, 1.0, 1.0, 0.92, 1.0, 1.0 };
    static readonly double[] Bfm2 = { 0.0, 0.0, 0.0, 0.0, 0.25, 0.0, 0.0 };
    static readonly double[] Bfm3 = { 0.0, 0.0, 0.0, 0.0, 1.77, 0.0, 0.0 };
    static readonly double[] Bfp1 = { 1.0, 0.93, 1.0, 0.93, 0.93, 1.0, 1.0 };
    static readonly double[] Bfp2 = { 0.0, 0.31, 0.0, 0.19, 0.31, 0.0, 0.0 };
    static readonly double[] Bfp3 = { 0.0, 2.00, 0.0, 1.79, 2.00, 0.0, 0.0 };

    static double Variability(double time, double location, double situation, double[] he, double deltaH, double f, double d,
        double aRef, int climate, int mdvar, ref long warn)
    {
        double zT = InvQ(time / 100), zL = InvQ(location / 100), zS = InvQ(situation / 100);
        int c = climate - 1;
        double wn = f / 47.7;
        double dex = Math.Sqrt(2 * A9000 * he[0]) + Math.Sqrt(2 * A9000 * he[1]) + Math.Pow(575.7e12 / wn, THIRD);
        double de = d < dex ? 130e3 * d / dex : 130e3 + d - dex;
        int mi = mdvar;
        bool plus20 = mi >= 20; if (plus20) mi -= 20;
        double sigmaS = plus20 ? 0.0 : 5.0 + 3.0 * Math.Exp(-de / 100e3);
        bool plus10 = mi >= 10; if (plus10) mi -= 10;
        double vMed = Curve(AllYear[0, c], AllYear[1, c], AllYear[2, c], AllYear[3, c], AllYear[4, c], de);
        if (mi == 0) { zT = zS; zL = zS; }
        else if (mi == 1) zL = zS;
        else if (mi == 2) zL = zT;
        if (Math.Abs(zT) > 3.10 || Math.Abs(zL) > 3.10 || Math.Abs(zS) > 3.10) warn |= 0x1000;
        double sigmaL = 0;
        if (!plus10)
        {
            double dhd = TerrainRoughness(d, deltaH);
            sigmaL = 10.0 * wn * dhd / (wn * dhd + 13.0);
        }
        double yL = sigmaL * zL;
        double q = Math.Log(0.133 * wn);
        double gm = Bfm1[c] + Bfm2[c] / (Math.Pow(Bfm3[c] * q, 2) + 1.0);
        double gp = Bfp1[c] + Bfp2[c] / (Math.Pow(Bfp3[c] * q, 2) + 1.0);
        double sTm = Curve(Bsm1[c], Bsm2[c], Xsm1[c], Xsm2[c], Xsm3[c], de) * gm;
        double sTp = Curve(Bsp1[c], Bsp2[c], Xsp1[c], Xsp2[c], Xsp3[c], de) * gp;
        double sTD = CD[c] * sTp;
        double tgtd = (sTp - sTD) * ZD[c];
        double sigmaT = zT < 0.0 ? sTm : zT <= ZD[c] ? sTp : sTD + tgtd / zT;
        double yT = sigmaT * zT;
        double ysTemp = sigmaS * sigmaS + yT * yT / (7.8 + zS * zS) + yL * yL / (24.0 + zS * zS);
        double yR, yS;
        if (mi == 0) { yR = 0.0; yS = Math.Sqrt(sigmaT * sigmaT + sigmaL * sigmaL + ysTemp) * zS; }
        else if (mi == 1) { yR = yT; yS = Math.Sqrt(sigmaL * sigmaL + ysTemp) * zS; }
        else if (mi == 2) { yR = Math.Sqrt(sigmaT * sigmaT + sigmaL * sigmaL) * zT; yS = Math.Sqrt(ysTemp) * zS; }
        else { yR = yT + yL; yS = Math.Sqrt(ysTemp) * zS; }
        double result = aRef - vMed - yR - yS;
        if (result < 0.0) result = result * (29.0 - result) / (29.0 - 10.0 * result);
        return result;
    }

    // ---- prova contro il codice NTIA originale (C++ compilato a parte): profili sintetici deterministici ----
    public static double[] TestProfile(int k)
    {
        int np = 400; double step = k == 0 ? 250 : k == 1 ? 500 : 750;   // 100, 200, 300 km
        var p = new double[np + 3]; p[0] = np; p[1] = step;
        for (int i = 0; i <= np; i++)
        {
            double x = i / (double)np;
            p[i + 2] = k == 0 ? 50 + 300 * Math.Sin(Math.PI * x) * Math.Sin(7 * x) * Math.Sin(7 * x)
                     : k == 1 ? Math.Max(0, 900 * Math.Exp(-Math.Pow((x - 0.3) / 0.08, 2)) + 120 * Math.Sin(23 * x) + 40)
                     : (x > 0.4 && x < 0.85 ? 0 : 200 + 150 * Math.Cos(11 * x));
        }
        return p;
    }
    public static readonly (int prof, double htx, double hrx, Climate cl, double n0, double f, bool vert, double eps, double sig, int mdvar, double t, double l, double s)[] TestCases =
    {
        (0, 8, 10, Climate.ContinentalTemperate, 301, 144, false, 15, 0.005, 12, 50, 50, 50),
        (1, 8, 20, Climate.MaritimeTemperateLand, 320, 144, false, 15, 0.005, 12, 50, 50, 50),
        (2, 15, 10, Climate.MaritimeTemperateSea, 350, 432, false, 81, 5, 12, 10, 50, 50),
        (2, 30, 30, Climate.MaritimeTemperateSea, 350, 1296, true, 81, 5, 3, 90, 70, 50),
        (1, 3, 3, Climate.ContinentalTemperate, 301, 50, false, 15, 0.005, 1, 50, 50, 50),
    };
    /// <summary>Valori del codice NTIA originale (C++ compilato) sugli stessi casi.</summary>
    public static readonly double[] TestExpected = { 163.657288, 183.080414, 174.447650, 216.626694, 163.940540 };

    /// <summary>Confronto con il codice NTIA: righe di testo e true se tutti entro 0,01 dB.</summary>
    public static bool SelfTest(List<string> lines)
    {
        bool ok = true;
        for (int i = 0; i < TestCases.Length; i++)
        {
            var c = TestCases[i];
            var r = P2P(c.htx, c.hrx, TestProfile(c.prof), c.cl, c.n0, c.f, c.vert, c.eps, c.sig, c.mdvar, c.t, c.l, c.s);
            bool good = r.Error == 0 && Math.Abs(r.AdB - TestExpected[i]) < 0.01;
            ok &= good;
            lines.Add($"  ITM caso {i + 1}: {r.AdB:0.000} dB ({r.ModeName}, Δh {r.DeltaH:0} m), NTIA {TestExpected[i]:0.000} dB → {(good ? "ok" : "DIVERSO")}");
        }
        return ok;
    }
}
