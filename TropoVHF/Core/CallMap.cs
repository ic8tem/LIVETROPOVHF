namespace TropoVHF.Core;

/// <summary>
/// 1.4.0: elenco nominativo → locatore compatto per il milione di voci di CALL3.TXT. Un Dictionary&lt;string,string&gt;
/// occupava circa 110 MB; qui nominativo e locatore diventano numeri (8 + 4 byte), in due array ordinati, e la ricerca
/// è binaria: circa 13 MB. Le voci che non si impacchettano (nominativi oltre 12 caratteri o con caratteri insoliti,
/// locatori a 10 caratteri) e quelle aggiunte dopo il caricamento stanno in un dizionario a parte, piccolo.
/// Ogni voce si rilegge identica a com'era: l'impacchettamento si usa solo se andata e ritorno coincidono.
/// Non è thread-safe: il chiamante tiene il lucchetto (come in CallDb).
/// </summary>
public sealed class CallMap
{
    ulong[] _keys = Array.Empty<ulong>();        // nominativi impacchettati, in ordine (lo stesso ordine Ordinal delle stringhe)
    int[] _vals = Array.Empty<int>();            // locatori impacchettati; -1 = voce tolta
    readonly Dictionary<string, string> _extra = new(StringComparer.Ordinal);
    int _baseLive;

    public int Count => _baseLive + _extra.Count;

    // ------------------------------------------------ impacchettamento del nominativo (base 38, fino a 12 caratteri)

    // '/' < cifre < lettere, come nell'ordine Ordinal; 0 = fine della stringa, così i più corti vengono prima
    static int Sym(char c) => c == '/' ? 1 : c >= '0' && c <= '9' ? 2 + (c - '0') : c >= 'A' && c <= 'Z' ? 12 + (c - 'A') : -1;
    const string Syms = "\0/0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    static bool PackCall(string s, out ulong k)
    {
        k = 0;
        if (s.Length == 0 || s.Length > 12) return false;
        for (int i = 0; i < 12; i++)
        {
            int v = 0;
            if (i < s.Length) { v = Sym(s[i]); if (v < 0) return false; }
            k = k * 38 + (ulong)v;
        }
        return true;
    }

    static string UnpackCall(ulong k)
    {
        var c = new char[12];
        for (int i = 11; i >= 0; i--) { c[i] = Syms[(int)(k % 38)]; k /= 38; }
        int n = 12; while (n > 0 && c[n - 1] == '\0') n--;
        return new string(c, 0, n);
    }

    // ------------------------------------------------ impacchettamento del locatore (4, 6 o 8 caratteri)

    const int Off6 = 18 * 18 * 10 * 10;                    // 32 400 locatori a 4 caratteri
    const int Off8 = Off6 + 18 * 18 * 100 * 24 * 24;       // + 18 662 400 a 6 caratteri; poi quelli a 8 (fino a 1,9 miliardi)

    static bool PackLoc(string l, out int v)
    {
        v = 0;
        if (l.Length != 4 && l.Length != 6 && l.Length != 8) return false;
        int a = l[0] - 'A', b = l[1] - 'A', c = l[2] - '0', d = l[3] - '0';
        if (a < 0 || a > 17 || b < 0 || b > 17 || c < 0 || c > 9 || d < 0 || d > 9) return false;
        long x = ((a * 18 + b) * 10 + c) * 10 + d;
        if (l.Length == 4) { v = (int)x; return true; }
        int e = l[4] - 'A', f = l[5] - 'A';
        if (e < 0 || e > 23 || f < 0 || f > 23) return false;
        x = (x * 24 + e) * 24 + f;
        if (l.Length == 6) { v = (int)(Off6 + x); return true; }
        int g = l[6] - '0', h = l[7] - '0';
        if (g < 0 || g > 9 || h < 0 || h > 9) return false;
        v = (int)(Off8 + (x * 10 + g) * 10 + h);
        return true;
    }

    static string UnpackLoc(int v)
    {
        if (v < Off6) return Four(v);
        if (v < Off8) { int x = v - Off6; return Four(x / 576) + (char)('A' + x / 24 % 24) + (char)('A' + x % 24); }
        { long x = v - Off8; int six = (int)(x / 100); return Four(six / 576) + (char)('A' + six / 24 % 24) + (char)('A' + six % 24) + (char)('0' + x / 10 % 10) + (char)('0' + x % 10); }
        static string Four(int x) => new string(new[] { (char)('A' + x / 1800), (char)('A' + x / 100 % 18), (char)('0' + x / 10 % 10), (char)('0' + x % 10) });
    }

    static bool TryPack(string call, string loc, out ulong k, out int v)
    {
        v = 0;
        return PackCall(call, out k) && PackLoc(loc, out v) && UnpackCall(k) == call && UnpackLoc(v) == loc;
    }

    // ------------------------------------------------ costruzione

    /// <summary>Costruisce l'elenco da coppie qualsiasi (l'ultima voce di un nominativo vince, come in un dizionario).</summary>
    public static CallMap From(IEnumerable<KeyValuePair<string, string>> items)
    {
        var m = new CallMap();
        var ks = new List<ulong>(1_100_000); var vs = new List<int>(1_100_000); var ss = new List<int>(1_100_000);
        var extraAt = new Dictionary<string, int>(StringComparer.Ordinal);   // posizione dell'ultima voce non impacchettata
        int seq = 0;
        foreach (var kv in items)
        {
            if (TryPack(kv.Key, kv.Value, out var k, out var v)) { ks.Add(k); vs.Add(v); ss.Add(seq); }
            else { m._extra[kv.Key] = kv.Value; extraAt[kv.Key] = seq; }
            seq++;
        }
        var ka = ks.ToArray(); var va = vs.ToArray(); var sa = ss.ToArray();
        ks = null; vs = null; ss = null;
        // in ordine di nominativo e, a parità, di lettura: vince l'ultima voce letta, come in un dizionario
        var idx = new int[ka.Length];
        for (int i = 0; i < idx.Length; i++) idx[i] = i;
        Array.Sort(idx, (x, y) => ka[x] != ka[y] ? ka[x].CompareTo(ka[y]) : x.CompareTo(y));
        int n = 0;
        var outK = new ulong[ka.Length]; var outV = new int[ka.Length]; var outS = new int[ka.Length];
        foreach (int j in idx)
        {
            if (n > 0 && outK[n - 1] == ka[j]) { outV[n - 1] = va[j]; outS[n - 1] = sa[j]; }
            else { outK[n] = ka[j]; outV[n] = va[j]; outS[n] = sa[j]; n++; }
        }
        Array.Resize(ref outK, n); Array.Resize(ref outV, n);
        m._keys = outK; m._vals = outV; m._baseLive = n;
        // lo stesso nominativo in tutte e due le parti: resta la voce letta per ultima
        foreach (var kv in extraAt)
        {
            int i = m.Find(kv.Key);
            if (i < 0) continue;
            if (outS[i] > kv.Value) m._extra.Remove(kv.Key);
            else { m._vals[i] = -1; m._baseLive--; }
        }
        return m;
    }

    int Find(string call)
    {
        if (_keys.Length == 0 || !PackCall(call, out var k)) return -1;
        int i = Array.BinarySearch(_keys, k);
        return i >= 0 && UnpackCall(k) == call ? i : -1;
    }

    // ------------------------------------------------ uso

    public bool TryGetValue(string call, out string loc)
    {
        if (_extra.TryGetValue(call, out loc)) return true;
        int i = Find(call);
        if (i >= 0 && _vals[i] >= 0) { loc = UnpackLoc(_vals[i]); return true; }
        loc = null; return false;
    }

    public bool ContainsKey(string call) => TryGetValue(call, out _);

    public bool Remove(string call)
    {
        bool done = _extra.Remove(call);
        int i = Find(call);
        if (i >= 0 && _vals[i] >= 0) { _vals[i] = -1; _baseLive--; done = true; }
        return done;
    }

    public string this[string call]
    {
        set
        {
            int i = Find(call);
            if (i >= 0 && PackLoc(value, out var v) && UnpackLoc(v) == value)
            {
                if (_vals[i] < 0) _baseLive++;
                _vals[i] = v; _extra.Remove(call);
                return;
            }
            if (i >= 0 && _vals[i] >= 0) { _vals[i] = -1; _baseLive--; }
            _extra[call] = value;
        }
    }

    /// <summary>Tutte le voci in ordine Ordinal del nominativo (per la scrittura di CALL3.TXT).</summary>
    public IEnumerable<KeyValuePair<string, string>> Ordered()
    {
        var ex = _extra.OrderBy(kv => kv.Key, StringComparer.Ordinal).ToList();
        int j = 0;
        for (int i = 0; i < _keys.Length; i++)
        {
            if (_vals[i] < 0) continue;
            var call = UnpackCall(_keys[i]);
            while (j < ex.Count && string.CompareOrdinal(ex[j].Key, call) < 0) yield return ex[j++];
            yield return new KeyValuePair<string, string>(call, UnpackLoc(_vals[i]));
        }
        while (j < ex.Count) yield return ex[j++];
    }
}
