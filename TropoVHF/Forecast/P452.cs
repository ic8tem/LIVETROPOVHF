namespace TropoVHF.Forecast;

/// <summary>Dati d'ingresso di ITU-R P.452-18 per un profilo (distanze in km, quote in m s.l.m.).</summary>
public sealed class P452In
{
    public double[] Dist, H;            // profilo del terreno (senza clutter)
    public bool[] Sea;                  // punto sul mare (zona B)
    public double FGHz, P = 50;         // frequenza (GHz), percentuale di tempo (0,001–50)
    public double Htg, Hrg;             // antenne sopra il suolo (m)
    public double Gt, Gr;               // guadagni (dBi)
    public double Dct = 500, Dcr = 500; // distanza dalla costa sul tragitto, dal TX e dall'RX (km)
    public double DN = 45, N0 = 325;    // gradiente del primo km e rifrattività al livello del mare
    public double LatCenter;            // latitudine del centro della tratta (°)
}

/// <summary>Risultati di ITU-R P.452-18 con le grandezze intermedie (come nei casi di validazione dell'ITU).</summary>
public sealed class P452Out
{
    public double Ae, Theta, ThetaT, ThetaR, Hte, Hre, Hstd, Hsrd, Dlt, Dlr, Omega, Beta0;
    public double Lbfsg, Lb0p, Lb0b, Ldsph, Ld50, Ldp, Lbs, Lba, Lb;
    public bool Los;
}

/// <summary>
/// ITU-R P.452-18, previsione completa della perdita di base per una percentuale di tempo p (§ 4.1–4.6), senza clutter
/// e con l'assorbimento dei gas semplificato (P.676 allegato 2 al livello del mare). Validata in sviluppo sui casi
/// ufficiali dell'ITU (implementazione di riferimento di I. Stevanovic, Py452): vedi il manuale, § 11 bis.
/// </summary>
public static class P452
{
    const double Re = 6371;

    public static P452Out Compute(P452In a)
    {
        var o = new P452Out();
        double[] h = a.H, dist = a.Dist; int n = h.Length; double d = dist[n - 1], f = a.FGHz, p = a.P;
        double lam = 0.299792458 / f;
        double k50 = 157 / (157 - a.DN), ae = Re * k50, ab = Re * 3; o.Ae = ae;
        double hts = h[0] + a.Htg, hrs = h[n - 1] + a.Hrg;
        o.Omega = a.Sea == null ? 0 : SeaFraction(dist, a.Sea);

        // § 5.1.1–5.1.3: orizzonti
        double thT = double.MinValue, thR = double.MinValue; int iht = 1, ihr = n - 2;
        for (int i = 1; i < n - 1; i++)
        {
            double at = (h[i] - hts) / dist[i] - 1000 * dist[i] / (2 * ae);
            double ar = (h[i] - hrs) / (d - dist[i]) - 1000 * (d - dist[i]) / (2 * ae);
            if (at > thT) { thT = at; iht = i; }
            if (ar > thR) { thR = ar; ihr = i; }
        }
        double thTd = (hrs - hts) / d - 1000 * d / (2 * ae), thRd = (hts - hrs) / d - 1000 * d / (2 * ae);
        o.Los = n < 3 || thT < thTd;
        double dlt, dlr;
        if (o.Los)
        {
            // in vista: orizzonti al punto con ν più alto (eq. 158)
            double numax = double.MinValue; int im = n / 2;
            for (int i = 1; i < n - 1; i++)
            {
                double nu = (h[i] + 500 * dist[i] * (d - dist[i]) / ae - (hts * (d - dist[i]) + hrs * dist[i]) / d) * Math.Sqrt(0.002 * d / (lam * dist[i] * (d - dist[i])));
                if (nu > numax) { numax = nu; im = i; }
            }
            dlt = dist[im]; dlr = d - dist[im]; thT = thTd; thR = thRd;
        }
        else { dlt = dist[iht]; dlr = d - dist[ihr]; }
        o.ThetaT = thT; o.ThetaR = thR; o.Dlt = dlt; o.Dlr = dlr;
        double theta = 1000 * d / ae + thT + thR; o.Theta = theta;

        // § 5.1.6: superficie liscia, altezze efficaci (ducting) e superficie corretta per la diffrazione
        double v1 = 0, v2 = 0;
        for (int i = 1; i < n; i++) { double dd = dist[i] - dist[i - 1]; v1 += dd * (h[i] + h[i - 1]); v2 += dd * (h[i] * (2 * dist[i] + dist[i - 1]) + h[i - 1] * (dist[i] + 2 * dist[i - 1])); }
        double hst0 = (2 * v1 * d - v2) / (d * d), hsr0 = (v2 - v1 * d) / (d * d);
        double hst = Math.Min(hst0, h[0]), hsr = Math.Min(hsr0, h[n - 1]);
        double hobs = double.MinValue, aobt = double.MinValue, aobr = double.MinValue;
        for (int i = 1; i < n - 1; i++) { double hh = h[i] - (hts * (d - dist[i]) + hrs * dist[i]) / d; hobs = Math.Max(hobs, hh); aobt = Math.Max(aobt, hh / dist[i]); aobr = Math.Max(aobr, hh / (d - dist[i])); }
        double hstp = hst0, hsrp = hsr0;
        if (hobs > 0) { double gt = aobt / (aobt + aobr), gr = aobr / (aobt + aobr); hstp = hst0 - hobs * gt; hsrp = hsr0 - hobs * gr; }
        double hstd = Math.Min(hstp, h[0]), hsrd = Math.Min(hsrp, h[n - 1]);
        o.Hstd = hstd; o.Hsrd = hsrd;
        double hte = hts - hst, hre = hrs - hsr; o.Hte = hte; o.Hre = hre;
        // rugosità (eq. 171) fra gli orizzonti
        double hm = 0;
        for (int i = 0; i < n; i++) if (dist[i] >= dlt && dist[i] <= d - dlr) hm = Math.Max(hm, h[i] - (hst + (hsr - hst) * dist[i] / d));

        // § 4.1: spazio libero, gas, multipath
        double ag = LinkCalc.GasDbKm(f, 7.5 + 2.5 * o.Omega) * d;
        double lbfsg = 92.4 + 20 * Math.Log10(f) + 20 * Math.Log10(d) + ag; o.Lbfsg = lbfsg;
        double esp = 2.6 * (1 - Math.Exp(-0.1 * (dlt + dlr))) * Math.Log10(p / 50);
        // § 4.4: β0
        double dtm = 0, dlm = 0; LandRuns(dist, a.Sea, out dtm, out dlm);
        double tau = 1 - Math.Exp(-4.12e-4 * Math.Pow(dlm, 2.41));
        double mu1 = Math.Min(1, Math.Pow(Math.Pow(10, -dtm / (16 - 6.6 * tau)) + Math.Pow(10, -5 * (0.496 + 0.354 * tau)), 0.2));
        double phi = Math.Abs(a.LatCenter);
        double mu4 = phi <= 70 ? Math.Pow(10, (-0.935 + 0.0176 * phi) * Math.Log10(mu1)) : Math.Pow(10, 0.3 * Math.Log10(mu1));
        double b0 = phi <= 70 ? Math.Pow(10, -0.015 * phi + 1.67) * mu1 * mu4 : 4.17 * mu1 * mu4; o.Beta0 = b0;
        double esb = 2.6 * (1 - Math.Exp(-0.1 * (dlt + dlr))) * Math.Log10(b0 / 50);
        o.Lb0p = lbfsg + esp; o.Lb0b = lbfsg + esb;

        // § 4.2: diffrazione (delta-Bullington) al 50% (ae) e a β0 (aβ = 3·6371)
        double Ld(double aa)
        {
            double ce = 1 / aa;
            double lbulla = LinkCalc.Bullington(h, dist, hts, hrs, d, ce, lam, out _, out _, out _, out _);
            double lbulls = LinkCalc.Bullington(new double[n], dist, hts - hstd, hrs - hsrd, d, ce, lam, out _, out _, out _, out _);
            double ldsph = LinkCalc.SphericalLoss(d, hts - hstd, hrs - hsrd, aa, f * 1000, o.Omega);
            if (aa == ae) o.Ldsph = ldsph;
            return lbulla + Math.Max(ldsph - lbulls, 0);
        }
        double ld50 = Ld(ae), ldb = Ld(ab); o.Ld50 = ld50;
        double fi = p >= 50 ? 0 : p > b0 ? InvCdf(p / 100) / InvCdf(b0 / 100) : 1;
        double ldp = ld50 + fi * (ldb - ld50); o.Ldp = ldp;
        double lbd50 = lbfsg + ld50, lbd = o.Lb0p + ldp;
        double lminb0p = p < b0 ? o.Lb0p + (1 - o.Omega) * ldp : lbd50 + (o.Lb0b + (1 - o.Omega) * ldp - lbd50) * fi;

        // § 4.3: troposcatter (eq. 45)
        double lf = 25 * Math.Log10(f) - 2.5 * Math.Pow(Math.Log10(f / 2), 2);
        double lc = 0.051 * Math.Exp(0.055 * (a.Gt + a.Gr));
        double agS = LinkCalc.GasDbKm(f, 3) * d;
        o.Lbs = 190 + lf + 20 * Math.Log10(d) + 0.573 * theta - 0.15 * a.N0 + lc + agS - 10.1 * Math.Pow(-Math.Log10(p / 50), 0.7);

        // § 4.5: condotti e riflessione da strati
        double alpha = Math.Max(-3.4, -0.6 - 3.5e-9 * Math.Pow(d, 3.1) * tau);
        double mu2 = Math.Min(1, Math.Pow(500 / ae * d * d / Math.Pow(Math.Sqrt(hte) + Math.Sqrt(hre), 2), alpha));
        double di = Math.Min(Math.Max(0, d - dlt - dlr), 40);
        double mu3 = hm <= 10 ? 1 : Math.Exp(-4.6e-5 * (hm - 10) * (43 + 6 * di));
        double beta = b0 * mu2 * mu3, lgb = Math.Log10(beta);
        double gam = 1.076 / Math.Pow(2.0058 - lgb, 1.012) * Math.Exp(-(9.51 - 4.8 * lgb + 0.198 * lgb * lgb) * 1e-6 * Math.Pow(d, 1.13));
        double ap = -12 + (1.2 + 3.7e-3 * d) * Math.Log10(p / beta) + 12 * Math.Pow(p / beta, gam);
        double alf = f < 0.5 ? 45.375 - 137.0 * f + 92.5 * f * f : 0;
        double Ast(double th, double dl) { double t2 = th - 0.1 * dl; return t2 > 0 ? 20 * Math.Log10(1 + 0.361 * t2 * Math.Sqrt(f * dl)) + 0.264 * t2 * Math.Pow(f, 1.0 / 3) : 0; }
        double Act(double dc, double dl, double hs) => o.Omega >= 0.75 && dc <= dl && dc <= 5 ? -3 * Math.Exp(-0.25 * dc * dc) * (1 + Math.Tanh(0.07 * (50 - hs))) : 0;
        double af = 102.45 + 20 * Math.Log10(f) + 20 * Math.Log10(dlt + dlr) + alf + Ast(thT, dlt) + Ast(thR, dlr) + Act(a.Dct, dlt, hts) + Act(a.Dcr, dlr, hrs);
        double thtp = thT <= 0.1 * dlt ? thT : 0.1 * dlt, thrp = thR <= 0.1 * dlr ? thR : 0.1 * dlr;
        double gd = 5e-5 * ae * Math.Pow(f, 1.0 / 3);
        o.Lba = af + gd * (1000 * d / ae + thtp + thrp) + ap + ag;

        // § 4.6: combinazione
        const double eta = 2.5;
        double lminbap = eta * Math.Log(Math.Exp(o.Lba / eta) + Math.Exp(o.Lb0p / eta));
        double fk = 1 - 0.5 * (1 + Math.Tanh(3 * 0.5 * (d - 20) / 20));
        double lbda = lminbap > lbd ? lbd : lminbap + (lbd - lminbap) * fk;
        double fj = 1 - 0.5 * (1 + Math.Tanh(3 * 0.8 * (theta - 0.3) / 0.3));
        double lbam = lbda + (lminb0p - lbda) * fj;
        o.Lb = -5 * Math.Log10(Math.Pow(10, -0.2 * o.Lbs) + Math.Pow(10, -0.2 * lbam));
        return o;
    }

    /// <summary>Frazione di tragitto sul mare, pesata sulle distanze.</summary>
    public static double SeaFraction(double[] dist, bool[] sea)
    {
        int n = dist.Length; double w = 0;
        for (int i = 1; i < n; i++) { double dd = dist[i] - dist[i - 1]; if (sea[i] && sea[i - 1]) w += dd; else if (sea[i] || sea[i - 1]) w += dd / 2; }
        return w / dist[n - 1];
    }

    /// <summary>Tratto di terra continuo più lungo (dtm) e tratto interno più lungo, oltre 50 km dalla costa (dlm).</summary>
    public static void LandRuns(double[] dist, bool[] sea, out double dtm, out double dlm)
    {
        int n = dist.Length; dtm = 0; dlm = 0; double run = 0; int start = -1;
        bool anySea = sea != null && sea.Any(x => x);
        for (int i = 1; i <= n; i++)
        {
            bool land = i < n && (sea == null || !sea[i]);
            if (land) { if (start < 0) start = i - 1; run += dist[i] - dist[i - 1]; }
            else if (start >= 0)
            {
                dtm = Math.Max(dtm, run);
                double cut = 0;
                if (anySea && (sea[start] || (start > 0 && sea[start - 1]))) cut += 50;
                if (anySea && i < n && sea[i]) cut += 50;
                dlm = Math.Max(dlm, anySea ? Math.Max(0, run - cut) : run);
                start = -1; run = 0;
            }
        }
    }

    /// <summary>Inversa della normale cumulativa complementare, approssimazione di P.1057 (eq. 177–179 di P.452).</summary>
    public static double InvCdf(double x)
    {
        double T(double y) => Math.Sqrt(-2 * Math.Log(y));
        double C(double y) { double t = T(y); return ((0.010328 * t + 0.802853) * t + 2.515516698) / (((0.001308 * t + 0.189269) * t + 1.432788) * t + 1); }
        return x <= 0.5 ? T(x) - C(x) : -(T(1 - x) - C(1 - x));
    }
}
