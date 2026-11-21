# Statistica delle radiosonde italiane (IGRA2, NOAA NCEI): inversioni e condotti al suolo alle 00 e alle 12 UTC, 2015-2024.
import io, zipfile, urllib.request, math, collections, sys
ST = {"ITM00016080": "Milano Linate", "ITM00016144": "S. Pietro Capofiume", "ITM00016044": "Udine Campoformido",
      "ITM00016245": "Pratica di Mare", "ITM00016320": "Brindisi"}
URL = "https://www.ncei.noaa.gov/data/integrated-global-radiosonde-archive/access/data-por/{}-data.txt.zip"
SEAS = {12: "DGF", 1: "DGF", 2: "DGF", 3: "MAM", 4: "MAM", 5: "MAM", 6: "GLA", 7: "GLA", 8: "GLA", 9: "SON", 10: "SON", 11: "SON"}
def num(s):
    s = s.strip()
    try: v = int(s)
    except: return None
    return None if v in (-9999, -8888) else v
def analyze(levels):
    # levels: list of (z m, p hPa, T C, Td C) ordinata; il primo è il suolo
    s = levels[0]; z0 = s[0]
    def N(p, t, td):
        e = 6.112 * math.exp(17.67 * td / (td + 243.5)); T = t + 273.15
        return 77.6 * p / T + 3.73e5 * e / (T * T)
    ns = N(s[1], s[2], s[3]); ms = ns
    inv = False; duct = False; sup = False
    for (z, p, t, td) in levels[1:]:
        dz = z - z0
        if dz <= 0 or dz > 1000: continue
        if dz <= 500 and t - s[2] >= 1.0: inv = True
        m = N(p, t, td) + 157 * dz / 1000
        if m < ms - 1: duct = True
    # super-rifrazione nel primo strato (fino al primo livello entro 300 m)
    for (z, p, t, td) in levels[1:]:
        dz = z - z0
        if 0 < dz <= 300:
            if (N(p, t, td) - ns) / (dz / 1000) <= -79: sup = True
            break
    return inv, duct, sup
out = []
for sid, name in ST.items():
    try:
        raw = urllib.request.urlopen(URL.format(sid), timeout=300).read()
        txt = zipfile.ZipFile(io.BytesIO(raw)).read(sid + "-data.txt").decode("ascii", "replace").splitlines()
    except Exception as ex:
        out.append(f"{name}: errore {ex}"); continue
    stat = collections.defaultdict(lambda: [0, 0, 0, 0]); cnt = [0]
    i = 0
    while i < len(txt):
        h = txt[i]
        if not h.startswith("#"): i += 1; continue
        y, mo, hr, nl = int(h[13:17]), int(h[18:20]), int(h[24:26]), int(h[32:36])
        body = txt[i + 1:i + 1 + nl]; i += 1 + nl
        if y < 2015 or y > 2024: continue
        hr = 0 if hr in (22, 23, 0, 1, 2) else 12 if hr in (10, 11, 12, 13, 14) else -1
        if hr < 0: continue
        cnt[0] += 1
        lev = []; surf = None
        for L in body:
            if len(L) < 39: continue
            t2 = L[1]; p = num(L[9:15]); z = num(L[16:21]); t = num(L[22:27]); rh = num(L[28:33]); dp = num(L[34:39])
            if p is None or z is None or t is None: continue
            if dp is not None: td = t / 10 - dp / 10
            elif rh is not None and rh > 0:
                tc = t / 10; a = math.log(rh / 1000) + 17.67 * tc / (tc + 243.5); td = 243.5 * a / (17.67 - a)
            else: continue
            rec = (z, p / 100, t / 10, td)
            if t2 == "1": surf = rec
            elif surf is not None: lev.append(rec)
        if surf is None or len(lev) < 2: continue
        lev.sort(key=lambda r: r[0])
        inv, duct, sup = analyze([surf] + lev)
        k = (SEAS[mo], hr); st = stat[k]
        st[0] += 1; st[1] += inv; st[2] += duct; st[3] += sup
    out.append(f"   (sondaggi 2015-2024 alle 00/12: {cnt[0]}; righe del file {len(txt)}; esempio intestazione: {next((x for x in txt if x.startswith('#') and x[13:17]=='2020'), '')[:60]})")
    out.append(f"== {name} ({sid}) 2015-2024: stagione, ora UTC, sondaggi, inversione al suolo (>=1 K entro 500 m) %, condotto al suolo entro 1 km %, super-rifrazione nel primo strato %")
    for se in ("DGF", "MAM", "GLA", "SON"):
        for hr in (0, 12):
            n, a, b, c = stat[(se, hr)]
            if n: out.append(f"   {se} {hr:02d}  n={n:4d}  inversione {100*a/n:5.1f}%  condotto {100*b/n:5.1f}%  super {100*c/n:5.1f}%")
    tot = {hr: [sum(stat[(se, hr)][j] for se in ("DGF","MAM","GLA","SON")) for j in range(4)] for hr in (0, 12)}
    for hr in (0, 12):
        n, a, b, c = tot[hr]
        if n: out.append(f"   ANNO {hr:02d}  n={n:4d}  inversione {100*a/n:5.1f}%  condotto {100*b/n:5.1f}%  super {100*c/n:5.1f}%")
open("esito-monte.txt", "w").write("\n".join(out) + "\n")
print("\n".join(out))
