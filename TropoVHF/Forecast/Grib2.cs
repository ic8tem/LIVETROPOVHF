namespace TropoVHF.Forecast;

/// <summary>
/// Lettore GRIB2 essenziale per i file del filtro NOMADS (GFS 0,25°): griglia latitudine/longitudine regolare
/// (modello di griglia 3.0) e impacchettamento semplice (5.0) o complesso con o senza differenze spaziali (5.2 e 5.3),
/// come in g2clib (comunpack). Gli altri impacchettamenti (JPEG 2000, PNG, CCSDS) non sono gestiti: il campo si salta.
/// </summary>
public sealed class Grib2Field
{
    public int Discipline, Category, Number;   // parametro (es. 0/0/0 temperatura)
    public int LevelType;                      // 1 suolo, 100 isobarico (Pa), 103 quota sul suolo (m)
    public double LevelValue;                  // valore del livello (Pa per 100, m per 103)
    public int Ni, Nj;
    public double La1, Lo1, La2, Lo2, Di, Dj;  // gradi
    public int ScanMode;
    public float[] Values = Array.Empty<float>();   // NaN dove manca il dato

    /// <summary>Valore nel nodo più vicino della griglia (NaN fuori dalla griglia).</summary>
    public double At(double lat, double lon)
    {
        if (Values.Length == 0 || Ni <= 0 || Nj <= 0) return double.NaN;
        bool jPos = (ScanMode & 0x40) != 0;                 // 0 = da nord a sud
        double fj = jPos ? (lat - La1) / Dj : (La1 - lat) / Dj;
        double dl = lon - Lo1; dl = ((dl % 360) + 360) % 360;
        double fi = dl / Di;
        int i = (int)Math.Round(fi), j = (int)Math.Round(fj);
        if (i < 0 || j < 0 || i >= Ni || j >= Nj || Math.Abs(fi - i) > 0.51 || Math.Abs(fj - j) > 0.51) return double.NaN;
        if ((ScanMode & 0x20) != 0) return double.NaN;       // ordine per colonne: non usato da NOMADS
        double v = Values[j * Ni + i];
        return float.IsNaN((float)v) ? double.NaN : v;
    }
}

public static class Grib2
{
    /// <summary>Legge tutti i messaggi di un file GRIB2 (anche più campi di fila).</summary>
    public static List<Grib2Field> Read(byte[] b)
    {
        var res = new List<Grib2Field>();
        int pos = 0;
        while (pos + 16 <= b.Length)
        {
            // cerca "GRIB"
            if (!(b[pos] == 'G' && b[pos + 1] == 'R' && b[pos + 2] == 'I' && b[pos + 3] == 'B')) { pos++; continue; }
            int edition = b[pos + 7];
            long total = (long)U(b, pos + 8, 8);
            if (edition != 2 || total < 16 || pos + total > b.Length) { pos += 4; continue; }
            int discipline = b[pos + 6];
            int end = (int)(pos + total);
            int p = pos + 16;
            Grib2Field grid = null, cur = null;
            byte[] bitmap = null; bool hasBitmap = false;
            int drsTpl = -1, nPoints = 0; byte[] drs = null; int drsOff = 0;
            while (p + 4 <= end)
            {
                if (b[p] == '7' && b[p + 1] == '7' && b[p + 2] == '7' && b[p + 3] == '7') break;
                int len = (int)U(b, p, 4); int num = b[p + 4];
                if (len < 5 || p + len > end) break;
                switch (num)
                {
                    case 3:
                        {
                            int tpl = (int)U(b, p + 12, 2);
                            grid = new Grib2Field();
                            if (tpl == 0)
                            {
                                grid.Ni = (int)U(b, p + 30, 4); grid.Nj = (int)U(b, p + 34, 4);
                                grid.La1 = S(b, p + 46, 4) * 1e-6; grid.Lo1 = S(b, p + 50, 4) * 1e-6;
                                grid.La2 = S(b, p + 55, 4) * 1e-6; grid.Lo2 = S(b, p + 59, 4) * 1e-6;
                                grid.Di = U(b, p + 63, 4) * 1e-6; grid.Dj = U(b, p + 67, 4) * 1e-6;
                                grid.ScanMode = b[p + 71];
                            }
                            break;
                        }
                    case 4:
                        {
                            cur = new Grib2Field { Discipline = discipline };
                            cur.Category = b[p + 9]; cur.Number = b[p + 10];
                            cur.LevelType = b[p + 22];
                            int sc = (b[p + 23] & 0x80) != 0 ? -(b[p + 23] & 0x7F) : b[p + 23];
                            double sv = S(b, p + 24, 4);
                            cur.LevelValue = sv * Math.Pow(10, -sc);
                            break;
                        }
                    case 5:
                        nPoints = (int)U(b, p + 5, 4); drsTpl = (int)U(b, p + 9, 2);
                        drs = b; drsOff = p;
                        break;
                    case 6:
                        {
                            int ind = b[p + 5];
                            if (ind == 0) { hasBitmap = true; bitmap = new byte[len - 6]; Array.Copy(b, p + 6, bitmap, 0, len - 6); }
                            else if (ind == 255) { hasBitmap = false; bitmap = null; }
                            break;   // 254 = bitmap del messaggio precedente: si tiene quella già letta
                        }
                    case 7:
                        if (grid != null && cur != null && drs != null)
                        {
                            try
                            {
                                var vals = Unpack(b, p + 5, len - 5, drs, drsOff, drsTpl, nPoints);
                                if (vals != null)
                                {
                                    int total2 = grid.Ni * grid.Nj;
                                    var full = new float[total2];
                                    if (hasBitmap && bitmap != null)
                                    {
                                        int k = 0;
                                        for (int q = 0; q < total2; q++)
                                        {
                                            bool on = (bitmap[q >> 3] & (0x80 >> (q & 7))) != 0;
                                            full[q] = on && k < vals.Length ? vals[k++] : float.NaN;
                                        }
                                    }
                                    else for (int q = 0; q < total2; q++) full[q] = q < vals.Length ? vals[q] : float.NaN;
                                    res.Add(new Grib2Field
                                    {
                                        Discipline = cur.Discipline, Category = cur.Category, Number = cur.Number,
                                        LevelType = cur.LevelType, LevelValue = cur.LevelValue,
                                        Ni = grid.Ni, Nj = grid.Nj, La1 = grid.La1, Lo1 = grid.Lo1, La2 = grid.La2, Lo2 = grid.Lo2,
                                        Di = grid.Di, Dj = grid.Dj, ScanMode = grid.ScanMode, Values = full
                                    });
                                }
                            }
                            catch (Exception ex) { CalcLog.W("GRIB2: campo non decodificato (" + ex.Message + ")"); }
                        }
                        break;
                }
                p += len;
            }
            pos = end;
        }
        return res;
    }

    static ulong U(byte[] b, int o, int n) { ulong v = 0; for (int i = 0; i < n; i++) v = (v << 8) | b[o + i]; return v; }

    /// <summary>Intero con segno GRIB2 (bit più alto = segno, il resto = modulo).</summary>
    static long S(byte[] b, int o, int n)
    {
        ulong v = U(b, o, n); ulong sign = 1UL << (8 * n - 1);
        return (v & sign) != 0 ? -(long)(v & (sign - 1)) : (long)v;
    }

    static float F32(byte[] b, int o)
    {
        var t = new byte[4]; for (int i = 0; i < 4; i++) t[i] = b[o + 3 - i];
        if (!BitConverter.IsLittleEndian) Array.Reverse(t);
        return BitConverter.ToSingle(t, 0);
    }

    sealed class Bits
    {
        readonly byte[] _b; readonly int _start, _len; public long Pos;
        public Bits(byte[] b, int start, int len) { _b = b; _start = start; _len = len; }
        public long Get(int n)
        {
            if (n == 0) return 0;
            long v = 0; int need = n;
            while (need > 0)
            {
                int byteIx = (int)(Pos >> 3), off = (int)(Pos & 7), avail = 8 - off, take = Math.Min(avail, need);
                int cur = byteIx < _len ? _b[_start + byteIx] : 0;
                v = (v << take) | (long)((cur >> (avail - take)) & ((1 << take) - 1));
                Pos += take; need -= take;
            }
            return v;
        }
        public void Align() { if ((Pos & 7) != 0) Pos += 8 - (Pos & 7); }
    }

    static float[] Unpack(byte[] b, int dataOff, int dataLen, byte[] d, int s5, int tpl, int n)
    {
        float r = F32(d, s5 + 11);
        int e = (int)S(d, s5 + 15, 2), dd = (int)S(d, s5 + 17, 2);
        int nbits = d[s5 + 19];
        double bscale = Math.Pow(2, e), dscale = Math.Pow(10, -dd);
        var outv = new float[n];
        var bits = new Bits(b, dataOff, dataLen);
        if (tpl == 0)
        {
            for (int i = 0; i < n; i++) outv[i] = (float)((r + (nbits > 0 ? bits.Get(nbits) : 0) * bscale) * dscale);
            return outv;
        }
        if (tpl != 2 && tpl != 3) { CalcLog.W($"GRIB2: impacchettamento 5.{tpl} non gestito"); return null; }
        int missMgmt = d[s5 + 22];
        int ng = (int)U(d, s5 + 31, 4);
        int refW = d[s5 + 35], nbW = d[s5 + 36];
        long refL = (long)U(d, s5 + 37, 4); int incL = d[s5 + 41];
        long lastL = (long)U(d, s5 + 42, 4); int nbL = d[s5 + 46];
        int order = tpl == 3 ? d[s5 + 47] : 0, octx = tpl == 3 ? d[s5 + 48] : 0;
        long ival1 = 0, ival2 = 0, minsd = 0;
        if (tpl == 3 && octx > 0)
        {
            int nb = octx * 8;
            long Sg() { long sgn = bits.Get(1); long v = bits.Get(nb - 1); return sgn == 1 ? -v : v; }
            ival1 = Sg(); if (order == 2) ival2 = Sg(); minsd = Sg();
        }
        var gref = new long[ng]; var gw = new int[ng]; var gl = new long[ng];
        if (nbits > 0) { for (int j = 0; j < ng; j++) gref[j] = bits.Get(nbits); bits.Align(); }
        if (nbW > 0) { for (int j = 0; j < ng; j++) gw[j] = (int)bits.Get(nbW); bits.Align(); }
        for (int j = 0; j < ng; j++) gw[j] += refW;
        if (nbL > 0) { for (int j = 0; j < ng; j++) gl[j] = bits.Get(nbL); bits.Align(); }
        for (int j = 0; j < ng; j++) gl[j] = gl[j] * incL + refL;
        if (ng > 0) gl[ng - 1] = lastL;
        var ifld = new long[n]; var miss = new bool[n];
        int k = 0;
        for (int j = 0; j < ng && k < n; j++)
        {
            long allRef = nbits > 0 ? (1L << nbits) - 1 : 0;
            for (long q = 0; q < gl[j] && k < n; q++, k++)
            {
                if (gw[j] > 0)
                {
                    long v = bits.Get(gw[j]);
                    long allW = (1L << gw[j]) - 1;
                    if (missMgmt >= 1 && v == allW) miss[k] = true;
                    else if (missMgmt == 2 && v == allW - 1) miss[k] = true;
                    else ifld[k] = v + gref[j];
                }
                else
                {
                    if (missMgmt >= 1 && nbits > 0 && gref[j] == allRef) miss[k] = true;
                    else if (missMgmt == 2 && nbits > 0 && gref[j] == allRef - 1) miss[k] = true;
                    else ifld[k] = gref[j];
                }
            }
        }
        if (tpl == 3)
        {
            // differenze spaziali solo sui punti validi
            var idx = new List<int>(n);
            for (int i = 0; i < n; i++) if (!miss[i]) idx.Add(i);
            if (order == 1 && idx.Count > 0)
            {
                ifld[idx[0]] = ival1;
                for (int q = 1; q < idx.Count; q++) ifld[idx[q]] = ifld[idx[q]] + minsd + ifld[idx[q - 1]];
            }
            else if (order == 2 && idx.Count > 1)
            {
                ifld[idx[0]] = ival1; ifld[idx[1]] = ival2;
                for (int q = 2; q < idx.Count; q++) ifld[idx[q]] = ifld[idx[q]] + minsd + 2 * ifld[idx[q - 1]] - ifld[idx[q - 2]];
            }
        }
        for (int i = 0; i < n; i++) outv[i] = miss[i] ? float.NaN : (float)((r + ifld[i] * bscale) * dscale);
        return outv;
    }
}
