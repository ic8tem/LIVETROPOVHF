// Prove di validazione di LIVETROPOVHF (manuale delle previsioni, § 11 e § 14).
// I dati di prova NON sono qui (niente ridistribuzione dei dati ITU): scaricarli a parte.
//   dotnet run -- p452 <cartella tests/validation_examples di https://github.com/eeveetza/Py452>   (variabile FMAX in GHz, di base 2)
//   dotnet run -- itm  <cartella con p2p.csv e pfls.csv di https://github.com/NTIA/itm>
using System.Globalization;
using TropoVHF.Forecast;

static class Prova
{
    static int Main(string[] args)
    {
        if (args.Length < 2) { Console.WriteLine("uso: p452 <cartella> | itm <cartella>"); return 1; }
        string dir = args[1].TrimEnd('/', '\\') + "/";
        if (args[0] == "p452") P452Test(dir); else if (args[0] == "itm") ItmTest(dir); else return 1;
        return 0;
    }

    static void P452Test(string dir)
    {
        var ci = CultureInfo.InvariantCulture;
        
        double fmax = double.Parse(Environment.GetEnvironmentVariable("FMAX") ?? "2", ci);
        var cols = new[] { "omega","b0","hstd","hsrd","theta","hte","hre","dlt","dlr","Lbfsg","Lb0p","Lb0b","Ldsph","Ld50","Ldp","Lbs","Lba","Lb" };
        var worst = cols.ToDictionary(c => c, c => (0.0, "")); var sum = cols.ToDictionary(c => c, c => 0.0); int rows = 0;
        foreach (var pf in Directory.GetFiles(dir + "profiles").OrderBy(x => x))
        {
            var name = Path.GetFileName(pf);
            var pl = File.ReadAllLines(pf).Skip(1).Where(l => l.Trim() != "").Select(l => l.Split(',')).ToList();
            var dist = pl.Select(p => double.Parse(p[0], ci)).ToArray();
            var h = pl.Select(p => double.Parse(p[1], ci)).ToArray();
            var rr = pl.Select(p => double.Parse(p[2], ci)).ToArray();
            var sea = pl.Select(p => p[4].Trim() == "3").ToArray();
            if (rr.Any(x => x > 0)) continue;
            var res = File.ReadAllLines(dir + "results/" + name.Replace("test_profile", "test_result"));
            var hdr = res[0].Split(',').Select(s => s.Trim()).ToList();
            foreach (var line in res.Skip(1).Where(l => l.Trim() != ""))
            {
                var v = line.Split(',');
                double G(string k) => double.Parse(v[hdr.IndexOf(k)].Trim(), ci);
                double f = G("f (GHz)"); if (f > fmax) continue;
                var o = P452.Compute(new P452In { Dist = dist, H = h, Sea = sea, FGHz = f, P = G("p (%)"), Htg = G("htg (m)"), Hrg = G("hrg (m)"), Gt = G("Gt (dBi)"), Gr = G("Gr (dBi)"),
                    Dct = G("dct (km)"), Dcr = G("dcr (km)"), DN = G("DN"), N0 = G("N0"), LatCenter = (G("phit_n (deg)") + G("phir_n (deg)")) / 2 });
                var mine = new Dictionary<string, double> { ["omega"] = o.Omega, ["b0"] = o.Beta0, ["hstd"] = o.Hstd, ["hsrd"] = o.Hsrd, ["theta"] = o.Theta, ["hte"] = o.Hte, ["hre"] = o.Hre, ["dlt"] = o.Dlt, ["dlr"] = o.Dlr,
                    ["Lbfsg"] = o.Lbfsg, ["Lb0p"] = o.Lb0p, ["Lb0b"] = o.Lb0b, ["Ldsph"] = o.Ldsph, ["Ld50"] = o.Ld50, ["Ldp"] = o.Ldp, ["Lbs"] = o.Lbs, ["Lba"] = o.Lba, ["Lb"] = o.Lb };
                rows++;
                foreach (var (k, m) in mine) { double diff = m - G(k); sum[k] += Math.Abs(diff); if (Math.Abs(diff) > Math.Abs(worst[k].Item1)) worst[k] = (diff, $"{name.Replace("test_profile_", "")} f {f} p {G("p (%)")}: noi {m:0.###} rif {G(k):0.###}"); }
            }
        }
        Console.WriteLine($"righe {rows} (f ≤ {fmax} GHz, senza clutter)");
        foreach (var (k, w) in worst) Console.WriteLine($"{k,-6} medio {sum[k] / Math.Max(1, rows):0.000}  massimo {w.Item1:+0.000;-0.000}  {w.Item2}");

    }

    static void ItmTest(string dir)
    {
        var ci = CultureInfo.InvariantCulture;
        var cases = File.ReadAllLines(Path.Combine(dir, "p2p.csv")).Skip(1).Where(l => l.Trim() != "").Select(l => l.Split(',').Select(x => double.Parse(x, ci)).ToArray()).ToList();
        var pfls = File.ReadAllLines(Path.Combine(dir, "pfls.csv")).Where(l => l.Trim() != "").Select(l => l.Split(',').Where(x => x.Trim() != "").Select(x => double.Parse(x, ci)).ToArray()).ToList();
        for (int i = 0; i < cases.Count; i++)
        {
            var c = cases[i];
            var r = Itm.P2P(c[0], c[1], pfls[i], (Itm.Climate)(int)c[7], c[4], c[5], c[6] == 1, c[2], c[3], (int)c[11], c[8], c[9], c[10]);
            Console.WriteLine($"caso {i + 1}: noi {r.AdB:0.00} dB, NTIA {c[12]:0.00} dB → {(Math.Abs(r.AdB - c[12]) < 0.01 ? "ok" : "DIVERSO")} (err {r.Error})");
        }

    }
}
