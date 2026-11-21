using System.Text;

namespace TropoVHF.Core;

/// <summary>Formule dei documenti: un piccolo sottoinsieme di LaTeX trasformato in un albero
/// (usato sia per il disegno nel programma sia per le equazioni native di Word).</summary>
public enum MKind { Var, Num, Func, Op, Punct, Space }

public abstract class MNode { }
public sealed class MRun : MNode { public string Text = ""; public MKind Kind; }
public sealed class MRow : MNode { public readonly List<MNode> Items = new(); }
public sealed class MFrac : MNode { public MRow Num = new(), Den = new(); }
public sealed class MScript : MNode { public MNode Base; public MRow Sub, Sup; }
public sealed class MSqrt : MNode { public MRow Body = new(); }

public static class MathParser
{
    static readonly Dictionary<string, string> Symbols = new()
    {
        ["alpha"] = "α", ["beta"] = "β", ["gamma"] = "γ", ["delta"] = "δ", ["epsilon"] = "ε", ["theta"] = "θ",
        ["lambda"] = "λ", ["mu"] = "μ", ["pi"] = "π", ["rho"] = "ρ", ["sigma"] = "σ", ["tau"] = "τ",
        ["phi"] = "ϕ", ["varphi"] = "φ", ["omega"] = "ω", ["Delta"] = "Δ", ["Phi"] = "Φ", ["Omega"] = "Ω",
        ["kappa"] = "κ", ["nu"] = "ν", ["eta"] = "η", ["xi"] = "ξ", ["chi"] = "χ", ["psi"] = "ψ", ["zeta"] = "ζ",
        ["Gamma"] = "Γ", ["Lambda"] = "Λ", ["Sigma"] = "Σ", ["Theta"] = "Θ", ["Psi"] = "Ψ", ["infty"] = "∞", ["partial"] = "∂",
    };
    static readonly Dictionary<string, string> Operators = new()
    {
        ["cdot"] = "·", ["times"] = "×", ["approx"] = "≈", ["le"] = "≤", ["ge"] = "≥", ["ne"] = "≠", ["pm"] = "±", ["to"] = "→",
        ["leq"] = "≤", ["geq"] = "≥", ["iff"] = "⟺", ["Leftrightarrow"] = "⇔", ["Rightarrow"] = "⇒", ["rightarrow"] = "→", ["propto"] = "∝",
        ["sim"] = "∼", ["simeq"] = "≃", ["ll"] = "≪", ["gg"] = "≫", ["equiv"] = "≡", ["neq"] = "≠", ["sum"] = "∑", ["int"] = "∫", ["ldots"] = "…", ["cdots"] = "⋯",
    };
    static readonly HashSet<string> Functions = new() { "log", "ln", "sin", "cos", "tan", "exp", "arcsin", "arccos", "arctan", "max", "min" };

    public static MRow Parse(string src)
    {
        int i = 0;
        return ParseRow(src ?? "", ref i, '\0');
    }

    static MRow ParseRow(string s, ref int i, char close)
    {
        var row = new MRow();
        while (i < s.Length)
        {
            char c = s[i];
            if (c == close) { i++; break; }
            var atom = ParseAtom(s, ref i);
            if (atom == null) continue;
            // apici e pedici
            MRow sub = null, sup = null;
            while (i < s.Length && (s[i] == '_' || s[i] == '^'))
            {
                bool isSub = s[i] == '_';
                i++;
                var g = ParseGroup(s, ref i);
                if (isSub) sub = g; else sup = g;
            }
            if (sub != null || sup != null) row.Items.Add(new MScript { Base = atom, Sub = sub, Sup = sup });
            else row.Items.Add(atom);
        }
        return row;
    }

    static MRow ParseGroup(string s, ref int i)
    {
        while (i < s.Length && s[i] == ' ') i++;
        if (i < s.Length && s[i] == '{') { i++; return ParseRow(s, ref i, '}'); }
        var r = new MRow();
        var a = ParseAtom(s, ref i);
        if (a != null) r.Items.Add(a);
        return r;
    }

    static MNode ParseAtom(string s, ref int i)
    {
        char c = s[i];
        if (c == ' ') { i++; return null; }
        if (c == '{') { i++; return ParseRow(s, ref i, '}'); }
        if (c == '\\')
        {
            i++;
            if (i < s.Length && !char.IsLetter(s[i]))
            {
                char x = s[i++];
                if (x == ',' || x == ';' || x == ' ') return new MRun { Text = " ", Kind = MKind.Space };
                return new MRun { Text = x.ToString(), Kind = MKind.Punct };
            }
            var sb = new StringBuilder();
            while (i < s.Length && char.IsLetter(s[i])) sb.Append(s[i++]);
            var cmd = sb.ToString();
            switch (cmd)
            {
                case "quad": return new MRun { Text = "  ", Kind = MKind.Space };
                case "qquad": return new MRun { Text = "    ", Kind = MKind.Space };
                case "big": case "Big": case "bigg": case "Bigg": case "notag": case "displaystyle": return null;
                case "circ": return new MRun { Text = "°", Kind = MKind.Punct };
                case "mathrm": case "text": case "textrm": case "operatorname":
                {
                    // testo in tondo: si prende così com'è fino alla graffa di chiusura
                    while (i < s.Length && s[i] == ' ') i++;
                    if (i >= s.Length || s[i] != '{') return null;
                    int depth = 0, start = ++i;
                    while (i < s.Length && (s[i] != '}' || depth > 0)) { if (s[i] == '{') depth++; else if (s[i] == '}') depth--; i++; }
                    var txt = s.Substring(start, i - start).Replace("\\,", " ").Replace("\\ ", " ").Replace("\\%", "%");
                    if (i < s.Length) i++;
                    return new MRun { Text = txt, Kind = MKind.Func };
                }
                case "dfrac": case "tfrac":
                case "frac":
                    return new MFrac { Num = ParseGroup(s, ref i), Den = ParseGroup(s, ref i) };
                case "sqrt":
                    return new MSqrt { Body = ParseGroup(s, ref i) };
                case "left":
                case "right":
                    while (i < s.Length && s[i] == ' ') i++;
                    if (i < s.Length) { var d = s[i++]; if (d == '.') return null; return new MRun { Text = d.ToString(), Kind = MKind.Punct }; }
                    return null;
            }
            if (Symbols.TryGetValue(cmd, out var sym)) return new MRun { Text = sym, Kind = MKind.Var };
            if (Operators.TryGetValue(cmd, out var op)) return new MRun { Text = op, Kind = MKind.Op };
            if (Functions.Contains(cmd)) return new MRun { Text = cmd, Kind = MKind.Func };
            return new MRun { Text = cmd, Kind = MKind.Func };
        }
        if (char.IsDigit(c))
        {
            var sb = new StringBuilder();
            while (i < s.Length && (char.IsDigit(s[i]) || ((s[i] == ',' || s[i] == '.') && i + 1 < s.Length && char.IsDigit(s[i + 1])))) sb.Append(s[i++]);
            return new MRun { Text = sb.ToString(), Kind = MKind.Num };
        }
        if (char.IsLetter(c)) { i++; return new MRun { Text = c.ToString(), Kind = MKind.Var }; }
        i++;
        return c switch
        {
            '-' => new MRun { Text = "−", Kind = MKind.Op },
            '+' or '=' or '<' or '>' => new MRun { Text = c.ToString(), Kind = MKind.Op },
            _ => new MRun { Text = c.ToString(), Kind = MKind.Punct },
        };
    }
}
