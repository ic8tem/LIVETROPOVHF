using System.Numerics;

namespace TropoVHF.Forecast;

/// <summary>
/// Calcolo di campo con l'equazione parabolica (split-step di Fourier, angolo stretto), sulla linea di PETOOL e del
/// framework wave-propagation di M. Lytaev (MIT): u(x+Δx) = e^(i·k·Δx·m) · F⁻¹[e^(−i·p²·Δx/2k) · F[u(x)]], con m = M·10⁻⁶
/// la rifrattività modificata (la curvatura della Terra è già dentro M). Suolo conduttore (polarizzazione orizzontale,
/// estensione dispari), terreno a gradini (sotto il suolo il campo si annulla), sorgente gaussiana con l'immagine,
/// assorbitore di Hann nella metà alta di un dominio di almeno 8 km, fascio di al massimo 6°. Verifica (prototipo):
/// atmosfera standard, 144 MHz, antenne a 30 m: 164 dB a 100 km; condotto elevato a 300 m: 145 dB a 200 km contro 216. Perdita di base: L = −20 log|u| + 20 log 4π + 10 log x − 30 log λ.
/// Non comprende il troposcatter: va sommato a parte (somma delle potenze).
/// </summary>
public static class ParabolicEq
{
    public sealed class Result
    {
        public double LossDb = double.NaN;          // perdita di base all'antenna RX
        public double StdLossDb = double.NaN;       // stessa tratta con atmosfera standard (M = 315 + 0,118·z)
        public double GainDb => StdLossDb - LossDb; // guadagno dovuto al profilo previsto
        public double[] Dist = Array.Empty<double>(), LossAtRx = Array.Empty<double>();   // perdita lungo la tratta alla quota RX
        public double Hmax, Dz, Dx; public int Nz, Steps; public double Ms;
        public string Note = "";
    }

    /// <summary>Profilo M(z) (z in m s.l.m.) di un punto: livelli GFS più i salti stimati dei condotti potenziali (calcolo A, 50 m).</summary>
    public static Func<double, double> MProfile(ProfileResult p)
    {
        if (p == null || p.Levels.Length < 2) return z => 315 + 0.118 * z;
        var pts = new List<(double z, double m)>();
        var L = p.Levels;
        for (int i = 0; i < L.Length; i++)
        {
            pts.Add((L[i].Z, L[i].M));
            if (i + 1 < L.Length)
            {
                // condotto potenziale fra i e i+1: il salto si concentra in 50 m a metà strato (stesso schema dell'indice)
                var ld = p.Layers.FirstOrDefault(l => Math.Abs(l.ZBot - L[i].Z) < 1 && Math.Abs(l.ZTop - L[i + 1].Z) < 1 && !l.Resolved && l.Deficit > 0);
                if (ld != null)
                {
                    double mid = (L[i].Z + L[i + 1].Z) / 2, z1 = mid - 25, z2 = mid + 25;
                    if (z1 > L[i].Z + 5 && z2 < L[i + 1].Z - 5)
                    {
                        double m1 = L[i].M + 0.118 * (z1 - L[i].Z);
                        double m2 = m1 - ld.Deficit;
                        pts.Add((z1, m1)); pts.Add((z2, m2));
                    }
                }
            }
        }
        pts = pts.OrderBy(q => q.z).ToList();
        var zs = pts.Select(q => q.z).ToArray(); var ms = pts.Select(q => q.m).ToArray();
        return z =>
        {
            if (z <= zs[0]) return ms[0] - 0.118 * (zs[0] - z);
            if (z >= zs[zs.Length - 1]) return ms[ms.Length - 1] + 0.118 * (z - zs[zs.Length - 1]);
            int j = Array.BinarySearch(zs, z); if (j < 0) j = ~j;
            double f = (z - zs[j - 1]) / Math.Max(1e-6, zs[j] - zs[j - 1]);
            return ms[j - 1] + f * (ms[j] - ms[j - 1]);
        };
    }

    /// <summary>
    /// Perdita di base lungo la tratta. terrain/dist: profilo del terreno (m s.l.m., km); prof: profili lungo la tratta
    /// (equidistanti dal TX al RX, null = atmosfera standard); hts/hrs: antenne in m s.l.m.; bwDeg: apertura del fascio TX.
    /// </summary>
    public static Result Run(double fMHz, double dKm, double hts, double hrs, double[] terrain, double[] dist,
                             IList<ProfileResult> prof, double bwDeg, CancellationToken ct, bool alsoStandard = true)
    {
        // riferimento "normale": 315 + 0,118·z, oppure (1.3.2, alternativa) profilo CRPL con l'Ns medio della tratta
        Func<double, double> stdM = z => 315 + 0.118 * z;
        var nsl = prof?.Where(q => q != null && !double.IsNaN(q.Ns)).Select(q => q.Ns).ToList();
        if (Refractivity.UseCrplNormal && nsl != null && nsl.Count > 0)
        {
            double ns = nsl.Average(), c = -Math.Log(1 - Math.Min(0.5, Refractivity.CrplDN(ns) / ns));
            stdM = z => ns * Math.Exp(-c * Math.Max(0, z) / 1000) + 157 * z / 1000;
        }
        var r = new Result();
        double lam = 299.792458 / fMHz, k = 2 * Math.PI / lam;
        double tMax = terrain != null && terrain.Length > 0 ? terrain.Max() : 0;
        double top = Math.Max(Math.Max(hts, hrs), tMax);
        double hmax = Math.Max(8000, top + 6000);   // dominio alto e assorbitore spesso: con meno il campo oltre l'orizzonte è solo rumore numerico
        double dz = Math.Min(2.0, 5 * lam);
        int nz = 1; while (nz * dz < hmax) nz <<= 1;
        hmax = nz * dz;
        double dx = dKm > 300 ? 500 : dKm > 100 ? 250 : 100;
        r.Hmax = hmax; r.Dz = dz; r.Nz = nz; r.Dx = dx;
        var mFuncs = new List<Func<double, double>>();
        if (prof != null && prof.Any(p => p != null)) foreach (var p in prof) mFuncs.Add(p != null ? MProfile(p) : null);
        double[] Solve(bool std)
        {
            double th = Math.Max(2, Math.Min(6, bwDeg)) * Math.PI / 180;   // fascio stretto (≤ 6°): ad angoli grandi il metodo ad angolo stretto sbaglia e l'energia rimbalza sull'assorbitore
            double A = k * th / (2 * Math.Sqrt(2 * Math.PI * Math.Log(2)));
            var u = new Complex[nz];
            for (int i = 0; i < nz; i++)
            {
                double z = i * dz;
                double g1 = A * Math.Exp(-th * th * k * k * (z - hts) * (z - hts) / (8 * Math.Log(2)));
                double g2 = A * Math.Exp(-th * th * k * k * (z + hts) * (z + hts) / (8 * Math.Log(2)));
                u[i] = g1 - g2;
            }
            int n2 = 2 * nz;
            var prop = new Complex[n2];
            for (int i = 0; i < n2; i++)
            {
                double p = 2 * Math.PI * (i < nz ? i : i - n2) / (n2 * dz);
                prop[i] = Complex.FromPolarCoordinates(1, -p * p * dx / (2 * k));
            }
            var win = new double[nz]; int na = nz / 2;
            for (int i = 0; i < nz; i++) win[i] = i < nz - na ? 1 : 0.5 * (1 + Math.Cos(Math.PI * (i - (nz - na)) / na));
            var buf = new Complex[n2];
            int steps = (int)Math.Ceiling(dKm * 1000 / dx);
            var outD = new double[steps]; var outL = new double[steps];
            double[] mCur = new double[nz]; int mIdx = -2;
            for (int s = 1; s <= steps; s++)
            {
                ct.ThrowIfCancellationRequested();
                double x = Math.Min(s * dx, dKm * 1000);
                for (int i = 0; i < nz; i++) { buf[i] = u[i]; buf[n2 - 1 - i] = -u[i]; }
                Fft(buf, false);
                for (int i = 0; i < n2; i++) buf[i] *= prop[i];
                Fft(buf, true);
                // profilo M per questa distanza (il punto di previsione più vicino)
                int pi = std || mFuncs.Count == 0 ? -1 : (int)Math.Round(x / 1000 / dKm * (mFuncs.Count - 1));
                if (pi != mIdx)
                {
                    mIdx = pi;
                    var mf = pi >= 0 ? (mFuncs[Math.Max(0, Math.Min(mFuncs.Count - 1, pi))] ?? stdM) : stdM;
                    for (int i = 0; i < nz; i++) mCur[i] = mf(i * dz) * 1e-6;
                }
                // terreno a gradini: sotto il suolo il campo è nullo
                double hg = 0;
                if (terrain != null && dist != null && terrain.Length > 1)
                {
                    int j = Array.BinarySearch(dist, x / 1000); if (j < 0) j = ~j;
                    j = Math.Max(0, Math.Min(terrain.Length - 1, j));
                    hg = Math.Max(0, terrain[j]);
                }
                int ig = (int)(hg / dz);
                for (int i = 0; i < nz; i++)
                {
                    u[i] = i < ig ? Complex.Zero : buf[i] * Complex.FromPolarCoordinates(win[i], k * mCur[i] * dx);
                }
                double zr = hrs; int ir = (int)(zr / dz); double fr = zr / dz - ir;
                double amp = ir + 1 < nz ? (1 - fr) * u[ir].Magnitude + fr * u[ir + 1].Magnitude : 0;
                outD[s - 1] = x / 1000;
                outL[s - 1] = amp > 1e-30 ? -20 * Math.Log10(amp) + 20 * Math.Log10(4 * Math.PI) + 10 * Math.Log10(x) - 30 * Math.Log10(lam) : 400;
            }
            r.Steps = steps; r.Dist = outD;
            return outL;
        }
        var l = Solve(false);
        r.LossAtRx = l; r.LossDb = l.Length > 0 ? l[l.Length - 1] : double.NaN;
        if (alsoStandard && mFuncs.Count > 0) { var ls = Solve(true); r.StdLossDb = ls.Length > 0 ? ls[ls.Length - 1] : double.NaN; }
        else r.StdLossDb = r.LossDb;
        return r;
    }

    /// <summary>FFT complessa radix-2 sul posto (inversa normalizzata).</summary>
    static void Fft(Complex[] a, bool inverse)
    {
        int n = a.Length;
        for (int i = 1, j = 0; i < n; i++)
        {
            int bit = n >> 1;
            for (; (j & bit) != 0; bit >>= 1) j ^= bit;
            j ^= bit;
            if (i < j) { var t = a[i]; a[i] = a[j]; a[j] = t; }
        }
        for (int len = 2; len <= n; len <<= 1)
        {
            double ang = 2 * Math.PI / len * (inverse ? 1 : -1);
            var wl = new Complex(Math.Cos(ang), Math.Sin(ang));
            for (int i = 0; i < n; i += len)
            {
                var w = Complex.One;
                for (int j = 0; j < len / 2; j++)
                {
                    var uu = a[i + j]; var v = a[i + j + len / 2] * w;
                    a[i + j] = uu + v; a[i + j + len / 2] = uu - v;
                    w *= wl;
                }
            }
        }
        if (inverse) for (int i = 0; i < n; i++) a[i] /= n;
    }
}
