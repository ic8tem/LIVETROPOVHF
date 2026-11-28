using System.Globalization;
using System.Text.Json;
using TropoVHF.Core;

namespace TropoVHF.Forecast;

/// <summary>Un livello del profilo verticale: quota sul mare (m), pressione (hPa), temperatura (°C), umidità (%), N e M.</summary>
public struct Level
{
    public double Z, P, T, RH, N, M;
}

/// <summary>
/// Strato rifrattivo fra due livelli di pressione consecutivi. Con i livelli GFS (passi di 200–1000 m) un'inversione
/// sottile viene "spalmata": se la termodinamica dice che c'è un'inversione (aria più stabile del normale e più secca
/// sopra), il salto di N in eccesso sul normale si considera concentrato in uno strato sottile (circa 50 m) e se ne
/// ricava il condotto potenziale: deficit di M, profondità e lunghezza d'onda massima con la formula di Kerr.
/// </summary>
public sealed class LayerDuct
{
    public int Key;                    // indice del livello di pressione superiore (stessa chiave lungo il percorso)
    public string Name = "";           // es. "850–800 hPa"
    public double ZBot, ZTop;          // quote dei due livelli (m s.l.m.)
    public double ExcessN;             // calo di N oltre il normale (40 N/km), unità N
    public double LapseKkm;            // gradiente termico nello strato (K/km, positivo = la temperatura cala salendo)
    public double QRatio;              // rapporto fra umidità specifica sopra e sotto
    public bool Inversion;             // stabile e più secco sopra: salto probabilmente netto
    public bool Resolved;              // M cala già fra i due livelli (condotto visibile al modello)
    public double Deficit;             // deficit di M del condotto (unità M)
    public double DuctTop, DuctBase;   // m s.l.m.
    public bool Surface;               // la base tocca il suolo
    public double LambdaMax;           // lunghezza d'onda massima intrappolata (m), Kerr eq. 17 con calo lineare
    // doppio calcolo dei condotti potenziali: A = salto in 50 m (GFS), B = spessore climatologico P.453; si tiene il peggiore
    public bool Dual;                  // il doppio calcolo è stato fatto (condotto potenziale con climatologia disponibile)
    public double DeficitA, LambdaA, DeficitB, LambdaB, ClimThick, ClimStren, Anomaly;
    public string Chosen = "";         // "A", "B" o "" (condotto risolto: un solo calcolo)
    public double Depth => DuctTop - DuctBase;
    public bool Traps(double freqMHz) => LambdaMax >= 299.792458 / freqMHz;
}

/// <summary>
/// Analisi di un profilo verticale (un punto, un'ora): strati di Bean e Dutton, condotto più forte, guadagno stimato.
/// Le soglie per strato di pressione sono quelle della tabella 6.5 di Bean e Dutton (NBS Monograph 92, 1966, p. 248).
/// </summary>
public sealed class ProfileResult
{
    public double Elev;            // quota del suolo del punto (m s.l.m., quella del modello)
    public double Ns;              // rifrattività al suolo
    public double DN1km;           // variazione di N nel primo km sopra il suolo (N/km, negativa = normale)
    public double LayerGainDb;     // guadagno dal solo gradiente (0..15 dB, −10 con sub-rifrazione)
    public string LayerName = "";  // strato che dà il guadagno
    public double MaxGrad;         // −dN/dh più forte trovato (N/km)
    public bool Sub;               // sub-rifrazione nello strato più basso
    public bool HasDuct;
    public double DuctBase, DuctTop, DuctDeficit;   // m s.l.m. e unità M
    public bool SurfaceDuct;       // la base del condotto è il suolo
    public double Wind10, Blh, Cape;
    public double Lat = double.NaN, Lon = double.NaN;
    public DuctClimate Clim;                  // climatologia P.453 del punto
    // 1.3.0: moto verticale, vento a 850 hPa, stabilità (Misme), rifrazione potenziale K
    public double W850 = double.NaN, W700 = double.NaN;   // velocità verticale (m/s, positiva verso l'alto)
    public double Wind850 = double.NaN, Dir850 = double.NaN, T850 = double.NaN;
    public double DeltaW = double.NaN;     // lavoro per sollevare adiabaticamente l'aria dal suolo a 1 km (J/kg)
    public double StabDb;                  // termine di stabilità di Misme (dB, rispetto all'atmosfera standard)
    public double KSurf = double.NaN, KDrop;  // rifrazione potenziale K al suolo e calo massimo di K fra due livelli
    public string KDropLayer = "";
    public bool Subsidence, Ascent, Scirocco;
    public double VertDb;                  // correzione per moto verticale (dB)
    public bool Sea;                       // punto sul mare (quota del modello sotto 5 m)
    public int LowLevels;                  // livelli NOMADS vicini al suolo aggiunti (sul mare)
    public double Sst = double.NaN;        // temperatura della superficie del mare (°C)
    public double SnowM = double.NaN;      // 1.3.4: spessore della neve al suolo (m), dal modello
    // 1.3.2: contrasto aria-mare (sul mare): θ a 925 hPa − SST, T a 2 m − SST, umidità a 925 hPa, calo della SST nelle 72 ore prima
    public double SeaAirC = double.NaN, SeaT2 = double.NaN, Rh925 = double.NaN, SstDrop = double.NaN;
    /// <summary>
    /// Correzione aria-mare (dB): aria in quota più calda del mare (inversione d'avvezione) +1 / +2 dB, −0,5 con aria umida,
    /// +1 se il mare si è raffreddato di 1,5 °C o più nelle 72 ore prima (mari bassi dopo la bora); mare più caldo dell'aria
    /// a 2 m di 2 °C o più (rimescolamento convettivo) −1,5 dB, di 4 °C o più −3 dB. Fra −3 e +3 dB. Ipotesi da tarare.
    /// </summary>
    public double SeaAirDb
    {
        get
        {
            if (!Refractivity.UseSeaAir || !Sea || double.IsNaN(Sst)) return 0;
            double d = 0;
            if (!double.IsNaN(SeaT2) && SeaT2 <= -2) d = SeaT2 <= -4 ? -3 : -1.5;
            else if (!double.IsNaN(SeaAirC) && SeaAirC >= Refractivity.SeaAirMinK)
            {
                d = SeaAirC >= Refractivity.SeaAirStrongK ? 2 : 1;
                if (Rh925 > 80) d -= 0.5;
                if (SstDrop >= 1.5) d += 1;
            }
            return Math.Max(-3, Math.Min(3, d));
        }
    }
    public double EvapDuctM;               // altezza del condotto d'evaporazione (m), 0 = assente
    public double SunElev = double.NaN;    // altezza del sole (°)
    /// <summary>Giorno su terra con sole alto e strato limite alto o vento: i condotti al suolo sono rimescolati.</summary>
    public bool DayMix => !Sea && SunElev > 15 && (Blh > 800 || Wind10 > 5);
    /// <summary>Notte su terra con vento debole e strato limite basso: possibili condotti al suolo da irraggiamento.</summary>
    public bool NightRad => !Sea && SunElev < -3 && Wind10 < 3 && Blh < 300;
    public double LayerGainAggrDb;         // come LayerGainDb ma con la soglia classica di super-rifrazione (79 N/km) nello strato basso: stima aggressiva
    public double CrplDN1km = double.NaN;  // calo normale nel primo km del profilo CRPL per l'Ns del punto (N/km)
    public double Pmsl = double.NaN;       // pressione ridotta al livello del mare (hPa)
    public bool HighP;                     // alta pressione estesa (≥ 1020 hPa su quasi tutte le celle vicine) e vento debole nei bassi strati
    public double HighPDb => Refractivity.UseHighPressure && HighP && !Ascent ? Refractivity.HighPDbBonus : 0;
    public Level[] Levels = Array.Empty<Level>();
    public List<LayerDuct> Layers = new();     // strati con calo di N oltre il normale (condotti risolti o potenziali)

    /// <summary>Lo strato che intrappola la frequenza con il deficit maggiore (null se nessuno).</summary>
    public LayerDuct BestDuct(double freqMHz) => Layers.Where(l => l.Traps(freqMHz)).OrderByDescending(l => l.Deficit).FirstOrDefault();

    public double DuctThick => HasDuct ? DuctTop - DuctBase : 0;

    /// <summary>Spessore minimo del condotto per una banda (Kerr 1951, eq. 18: λ[cm] = 0,014·d[ft]^1,5).</summary>
    public static double MinDuctM(double freqMHz)
    {
        double lamCm = 29979.2458 / freqMHz;
        return 0.3048 * Math.Pow(lamCm / 0.014, 2.0 / 3.0);
    }

    /// <summary>
    /// Condotto "morbido" (Kerr 1951; Hepburn; VHF/UHF DX Book): il limite λ ≤ λmax non è un taglio netto, anche onde
    /// fino a circa 3 volte più lunghe sentono molto il condotto. Restituisce lo strato con il fattore migliore
    /// s = (λmax/λ − 1/3)/(2/3) fra 0 e 1 (1 = intrappola davvero) e il fattore stesso.
    /// </summary>
    public (LayerDuct layer, double s) SoftDuct(double freqMHz)
    {
        if (!Refractivity.UseSoftDuct) return (null, 0);
        double lam = 299.792458 / freqMHz;
        LayerDuct best = null; double bs = 0;
        foreach (var l in Layers)
        {
            if (l.Deficit <= 0 || l.LambdaMax <= 0) continue;
            double s = Math.Max(0, Math.Min(1, (l.LambdaMax / lam - 1.0 / 3) / (2.0 / 3)));
            if (s > bs || (s == bs && best != null && l.Deficit > best.Deficit)) { bs = s; best = l; }
        }
        return (best, bs);
    }

    /// <summary>Guadagno di un condotto che intrappola la banda: 20 dB più 1 dB ogni 5 unità M di deficit, al massimo 25.</summary>
    public static double DuctGain(double deficit) => Math.Min(25, 20 + deficit / 5.0);

    /// <summary>C'è un condotto (pieno o morbido) per la banda: sopra 16 dB i colori di condotto sono ammessi.</summary>
    public bool DuctLike(double freqMHz) => BestDuct(freqMHz) != null || SoftDuct(freqMHz).s > 0;

    /// <summary>Guadagno stimato in dB rispetto al profilo normale, per una frequenza (con le correzioni semplici).</summary>
    public double GainDb(double freqMHz)
    {
        double g = LayerGainDb;
        var bd = BestDuct(freqMHz);
        var (sl, sf) = bd == null ? SoftDuct(freqMHz) : (null, 0.0);
        if (bd != null) g = Math.Max(g, DuctGain(bd.Deficit));
        else if (sf > 0) g = Math.Max(g, 15 + sf * (DuctGain(sl.Deficit) - 15));   // condotto morbido: da 15 verso 20–25 dB
        else if (Layers.Any(l => l.Deficit > 0)) g = Math.Max(g, 15);     // condotto troppo sottile per la banda
        // rimescolamento: vento forte al suolo o strato limite molto alto cancellano i condotti al suolo (Bean e Dutton § 4.4)
        bool surf = bd != null ? bd.Surface : sl != null ? sl.Surface : (HasDuct && SurfaceDuct);
        if (surf && g > 10)
        {
            if (Wind10 > 8) g = 10 + (g - 10) * 0.4;
            else if (Wind10 > 5) g = 10 + (g - 10) * 0.7;
            if (Blh > 1500) g = 10 + (g - 10) * 0.6;
        }
        // convezione forte: strati rotti
        if (Cape > 1500) g -= 3; else if (Cape > 800) g -= 1.5;
        if (Refractivity.UseCorrections)
        {
            // Misme: la stabilità termica conta quando non c'è un condotto che intrappola la banda (in proporzione col morbido)
            if (bd == null) g += StabDb * (1 - sf);
            // Moler e Holden 1960: la subsidenza mantiene gli strati, i moti ascendenti li rompono
            g += VertDb;
        }
        g += HighPDb;
        if (Refractivity.UseCorrections) g += SeaAirDb;
        // senza condotto il guadagno resta nel giallo: rosso e viola solo per i condotti (pieni o morbidi)
        if (bd == null && sf <= 0) g = Math.Min(g, Refractivity.NoDuctCapDb);
        return g;
    }

    public double Index(double freqMHz) => Math.Max(0, Math.Min(10, GainDb(freqMHz) / 2.5));
}

/// <summary>Fisica del profilo: rifrattività, quote dei livelli, strati e condotti.</summary>
public static class Refractivity
{
    public static readonly int[] PLevels = { 1000, 975, 950, 925, 900, 850, 800, 700, 600, 500 };

    /// <summary>Correzioni della 1.3.0 (termine di stabilità di Misme e moto verticale): si possono spegnere per confronto.</summary>
    public static bool UseCorrections = true;
    /// <summary>Peso del termine di stabilità: dB per J/kg di ΔW oltre l'atmosfera standard (taratura provvisoria).</summary>
    public const double MismeDbPerJ = 0.07;
    public const double DeltaWStd = 56;      // ΔW dell'atmosfera standard (Δθ ≈ 3,3 K/km)
    /// <summary>Tetto del guadagno senza condotto (fine del giallo, «strato forte, nessun condotto»): validazione 1.3.0.</summary>
    public const double NoDuctCapDb = 16;
    /// <summary>Condotto morbido (λ fino a 3 volte λmax): 1.3.1, si spegne per confronto nella validazione.</summary>
    public static bool UseSoftDuct = true;
    /// <summary>Persistenza: bonus fino a 2 dB se nelle 24 ore precedenti la stessa cella era già sopra 13 dB.</summary>
    public static bool UsePersistence = true;
    /// <summary>Equazione parabolica nel calcolo del segnale (solo confronto).</summary>
    public static bool UseParabolic = true;
    /// <summary>1.3.2: profilo normale CRPL (Bean e Thayer 1959) con l'Ns del punto al posto di 40 N/km fissi. Alternativa, spenta per difetto.</summary>
    public static bool UseCrplNormal = false;
    /// <summary>1.3.2: mappa con la stima aggressiva (soglia ITU di super-rifrazione 79 N/km nello strato basso) invece della prudente (Bean e Dutton, 100 N/km).</summary>
    public static bool UseItuSuper = false;
    /// <summary>1.3.2: strati e condotti cercati solo fino a 2500 m sopra il suolo (1500 m toglieva le inversioni invernali da subsidenza).</summary>
    public static bool UseDuctCeiling = true;
    public const double DuctCeilingM = 2500;
    /// <summary>1.3.2: bonus con alta pressione estesa e vento debole nei bassi strati.</summary>
    public static bool UseHighPressure = true;
    /// <summary>1.3.2: contrasto aria-mare e memoria della temperatura del mare (§ 9 quater). Soglie in kelvin su θ925 − SST.</summary>
    public static bool UseSeaAir = true;
    /// <summary>1.3.4: rinforzo pomeridiano del troposcatter oltre i rilievi (§ 8 ter), al massimo MixScatterDb.</summary>
    public static bool UseMixScatter = true;
    public const double MixScatterDb = 3;
    public const double SeaAirMinK = 4, SeaAirStrongK = 7;
    public const double HighPDbBonus = 1.0, HighPHpa = 1020, HighPWind10 = 5, HighPWind850 = 8;
    /// <summary>1.3.2: peso del percorso su mare (μ1 di ITU-R P.452) sul guadagno degli strati, fra × 0,8 e × 1,2.</summary>
    public static bool UseSeaWeight = true;
    /// <summary>1.3.2: bonus quando la cima dello strato limite supera le creste lungo la tratta (IT9GSF).</summary>
    public static bool UseBlhRidges = true;
    /// <summary>1.3.2: calcolo «via monte» (rilievi vicini come diffusori) quando il corrispondente è oltre l'orizzonte.</summary>
    public static bool UseViaMountain = true;
    /// <summary>1.3.2: ITM (Longley-Rice, NTIA) come calcolo di confronto in atmosfera normale e per il tratto finale dopo l'uscita dal condotto.</summary>
    public static bool UseItm = true;
    /// <summary>Via monte «senza perdita fino al monte»: la stazione è portata sul punto migliore (perdita nulla fra la stazione e il punto).</summary>
    public static bool ViaIdeal = false;
    /// <summary>Criterio dell'autore: i rilievi entro NearKm da ciascuna stazione costano al massimo NearCapDb nel troposcatter (0 = spento).</summary>
    public static double NearCapDb = 6;
    public const double NearKm = 20;
    public static bool UseItmTail = true;
    public const double BlhRidgeDbBonus = 1.5;
    /// <summary>Calo normale nel primo km del profilo CRPL: −ΔN = 7,32·e^(0,005577·Ns) (NBS Monograph 4).</summary>
    public static double CrplDN(double ns) => 7.32 * Math.Exp(0.005577 * ns);
    /// <summary>Calo normale CRPL fra due quote sopra il suolo (m): N(h) = Ns·e^(−c·h), c = −ln(1 − ΔN/Ns) per km.</summary>
    public static double CrplDrop(double ns, double h1, double h2)
    {
        double c = -Math.Log(1 - Math.Min(0.5, CrplDN(ns) / ns));
        return ns * (Math.Exp(-c * Math.Max(0, h1) / 1000) - Math.Exp(-c * Math.Max(0, h2) / 1000));
    }
    public const double PersistMaxDb = 2, PersistThrDb = 13;
    public const int PersistHours = 24;

    /// <summary>Dati aggiuntivi di un'ora (NaN se la sorgente non li ha).</summary>
    public struct Extra
    {
        public double W850, W700, Wind850, Dir850;
        /// <summary>1.3.2: temperatura della superficie (sul mare = SST), T e umidità a 80 m e nello strato 30–0 hPa sopra il suolo (solo NOMADS).</summary>
        public double Sst, T80, Rh80, TL30, RhL30, Snow;
        public DateTime Utc;
        public static Extra None => new Extra { W850 = double.NaN, W700 = double.NaN, Wind850 = double.NaN, Dir850 = double.NaN,
                                                Sst = double.NaN, Snow = double.NaN, T80 = double.NaN, Rh80 = double.NaN, TL30 = double.NaN, RhL30 = double.NaN };
    }

    /// <summary>Temperatura potenziale (K).</summary>
    public static double Theta(double tC, double p) => (tC + 273.15) * Math.Pow(1000.0 / p, 0.2857);

    /// <summary>
    /// Indice di rifrazione potenziale K (Bean e Dutton § 6; Flavell e Lane): N dell'aria portata adiabaticamente a 1000 hPa,
    /// con T → θ e la pressione del vapore scalata con la pressione (rapporto di mescolanza costante).
    /// </summary>
    public static double K(double p, double tC, double rh)
    {
        double th = Theta(tC, p);
        double e = Math.Max(0, Math.Min(100, rh)) / 100.0 * Es(tC, p) * 1000.0 / p;
        return 77.6 / th * (1000 + 4810.0 * e / th);
    }

    /// <summary>
    /// Pressione di saturazione del vapore (hPa) sull'acqua, ITU-R P.453-14 eq. 9 (ricontrollata il 22/11/2026):
    /// es = EF·a·exp((b − t/d)·t/(t + c)), a 6,1121, b 18,678, c 257,14, d 234,5, EF = 1 + 10⁻⁴(7,2 + P(0,0320 + 5,9·10⁻⁶ t²)).
    /// </summary>
    public static double Es(double tC, double p = 1013.25)
    {
        double ef = 1 + 1e-4 * (7.2 + p * (0.0320 + 5.9e-6 * tC * tC));
        return ef * 6.1121 * Math.Exp((18.678 - tC / 234.5) * tC / (tC + 257.14));
    }

    /// <summary>N = 77,6/T·(P + 4810·e/T) (ITU-R P.453), T in K, P ed e in hPa.</summary>
    public static double N(double pHpa, double tC, double rh)
    {
        double tk = tC + 273.15;
        double e = Math.Max(0, Math.Min(100, rh)) / 100.0 * Es(tC, pHpa);
        return 77.6 / tk * (pHpa + 4810.0 * e / tk);
    }

    /// <summary>Guadagno dal gradiente: 0 fino al limite del normale, 10 dB alla soglia di super-rifrazione, 15 dB a 157 N/km.</summary>
    static double Ramp(double grad, double lo, double hi) => grad <= lo ? 0 : grad < hi ? 10 * (grad - lo) / (hi - lo) : 10 + 5 * Math.Min(1, (grad - hi) / Math.Max(1, 157 - hi));

    /// <summary>Soglie di Bean e Dutton (tab. 6.5): limite alto del profilo normale e soglia di super-rifrazione (N/km).</summary>
    static (string name, double pTop, double normHi, double superThr) LayerFor(double pMid)
    {
        if (pMid >= 850) return ("1000–850 hPa", 850, 60, 100);
        if (pMid >= 700) return ("850–700 hPa", 700, 50, 80);
        if (pMid >= 600) return ("700–600 hPa", 600, 40, 70);
        return ("600–500 hPa", 500, 30, 50);
    }

    /// <summary>
    /// Costruisce il profilo dal suolo a 500 hPa: quote con l'equazione ipsometrica (temperatura virtuale) a partire
    /// dalla quota e dalla pressione al suolo, e analizza strati e condotti.
    /// </summary>
    public static ProfileResult Analyze(double elev, double ps, double t2, double rh2, double[] tLev, double[] rhLev,
                                        double wind10, double blh, double cape, double lat = double.NaN, double lon = double.NaN, Extra? extra = null)
    {
        var ex = extra ?? Extra.None;
        var r = new ProfileResult { Elev = elev, Wind10 = wind10, Blh = blh, Cape = cape, Lat = lat, Lon = lon,
                                    W850 = ex.W850, W700 = ex.W700, Wind850 = ex.Wind850, Dir850 = ex.Dir850 };
        r.Clim = P453.At(lat, lon);
        if (double.IsNaN(ps) || double.IsNaN(t2) || double.IsNaN(rh2)) return null;
        var lev = new List<Level>();
        double z0 = elev + 2;
        var s = new Level { Z = z0, P = ps, T = t2, RH = rh2 };
        s.N = N(s.P, s.T, s.RH); s.M = s.N + 157 * s.Z / 1000.0;
        lev.Add(s);
        for (int i = 0; i < PLevels.Length; i++)
        {
            double p = PLevels[i];
            if (p >= ps - 2) continue;                 // livello sotto il suolo (o troppo vicino)
            if (double.IsNaN(tLev[i]) || double.IsNaN(rhLev[i])) continue;
            var prev = lev[lev.Count - 1];
            double tv1 = Tv(prev.T, prev.RH, prev.P), tv2 = Tv(tLev[i], rhLev[i], p);
            double z = prev.Z + 287.05 * (tv1 + tv2) / 2 / 9.80665 * Math.Log(prev.P / p);
            var l = new Level { Z = z, P = p, T = tLev[i], RH = rhLev[i] };
            l.N = N(p, l.T, l.RH); l.M = l.N + 157 * l.Z / 1000.0;
            lev.Add(l);
        }
        // 1.3.2: sul mare (quota del modello sotto 5 m) i livelli NOMADS vicini al suolo (80 m e strato 30–0 hPa) per vedere
        // il condotto a bassa quota che i livelli di pressione non risolvono; sulla terra no (strati notturni e diurni non validati)
        bool sea = elev < 5;
        r.Sea = sea;
        if (sea && lev.Count >= 2)
        {
            double zNext = lev[1].Z;
            void AddLow(double z, double p, double t, double rh)
            {
                if (double.IsNaN(t) || double.IsNaN(rh) || z >= zNext - 10 || z <= z0 + 5) return;
                var l = new Level { Z = z, P = p, T = t, RH = Math.Max(1, Math.Min(100, rh)) };
                l.N = N(p, l.T, l.RH); l.M = l.N + 157 * l.Z / 1000.0;
                lev.Add(l); r.LowLevels++;
            }
            double tv0 = Tv(t2, rh2, ps);
            AddLow(elev + 80, ps * Math.Exp(-9.80665 * 78 / (287.05 * tv0)), ex.T80, ex.Rh80);
            double pl = ps - 15; AddLow(z0 + 287.05 * tv0 / 9.80665 * Math.Log(ps / pl), pl, ex.TL30, ex.RhL30);
            lev.Sort((a, b) => a.Z.CompareTo(b.Z));
        }
        // 1.3.2: condotto d'evaporazione (profilo logaritmico neutro, come nel modello di Paulus e Jeske senza correzione di
        // stabilità): N al pelo dell'acqua saturo alla SST (98% per il sale), N a 2 m dal GFS; δ = (Nmare − N2m)/(0,157·ln(2/z0))
        r.Sst = ex.Sst;
        r.SnowM = ex.Snow;
        if (sea && !double.IsNaN(ex.Sst) && Math.Abs(ex.Sst - t2) < 15)
        {
            double nSea = N(ps, ex.Sst, 98);
            r.EvapDuctM = Math.Max(0, Math.Min(40, (nSea - s.N) / (0.157 * Math.Log(2 / 1.5e-4))));
        }
        // ora del giorno: altezza del sole sul punto (avvisi di rimescolamento diurno e di condotti notturni da irraggiamento)
        if (ex.Utc != default && !double.IsNaN(lat)) r.SunElev = SunElevation(ex.Utc, lat, lon);
        if (lev.Count < 3) return null;
        r.Levels = lev.ToArray();
        r.Ns = lev[0].N;
        r.CrplDN1km = CrplDN(r.Ns);
        r.Pmsl = ps * Math.Pow(1 - 0.0065 * elev / (t2 + 0.0065 * elev + 273.15), -5.257);
        r.DN1km = Interp(lev, z0 + 1000) - lev[0].N;

        // ---- strati di Bean e Dutton: gradiente più forte fra livelli vicini, classificato con le soglie dello strato
        double best = 0, bestAggr = 0; string bestName = "";
        double maxGrad = double.MinValue;
        // con il profilo CRPL le soglie dello strato basso si spostano di quanto il calo normale locale differisce da 40 N/km
        double crplShift = UseCrplNormal ? r.CrplDN1km - 40 : 0;
        for (int i = 1; i < lev.Count; i++)
        {
            double dz = (lev[i].Z - lev[i - 1].Z) / 1000.0;
            if (dz <= 0.01) continue;
            if (UseDuctCeiling && lev[i - 1].Z > z0 + DuctCeilingM) break;
            double g = -(lev[i].N - lev[i - 1].N) / dz;           // −dN/dh in N/km
            if (g > maxGrad) maxGrad = g;
            var (name, pTop, normHi, superThr) = LayerFor((lev[i].P + lev[i - 1].P) / 2);
            if (pTop == 850) { normHi += crplShift; superThr += crplShift; }
            double gain = Ramp(g, normHi, superThr);
            // stima aggressiva: soglia classica di super-rifrazione di 79 N/km (k ≈ 2; P.453 usa invece −100 N/km nei primi 100 m) nello strato basso (k ≈ 2: 157/(157−79)); prudente = Bean e Dutton 100 N/km
            double gainA = pTop == 850 ? Ramp(g, Math.Min(normHi, 79 - 19), 79 + crplShift) : gain;
            if (gain > best) { best = gain; bestName = name; }
            bestAggr = Math.Max(bestAggr, gainA);
        }
        r.MaxGrad = maxGrad;
        // sub-rifrazione: N cresce con la quota nel primo strato sopra il suolo
        if (lev.Count > 1 && lev[1].N > lev[0].N && best < 1) { r.Sub = true; best = -10; bestName = Lang("sub"); }
        r.LayerGainAggrDb = r.Sub ? best : Math.Max(best, bestAggr);
        if (UseItuSuper && !r.Sub) { r.LayerGainDb = r.LayerGainAggrDb; } else r.LayerGainDb = best;
        r.LayerName = bestName;

        // ---- condotto: strato in cui M cala con la quota (dM/dh < 0); si prende quello con il deficit maggiore
        for (int i = 1; i < lev.Count; i++)
        {
            if (lev[i].M >= lev[i - 1].M) continue;
            // cima: fine del tratto con M in calo
            int top = i;
            while (top + 1 < lev.Count && lev[top + 1].M < lev[top].M) top++;
            double mTop = lev[top].M;
            // massimo di M sotto la cima nel tratto in calo
            double mMax = lev[i - 1].M;
            // base: il punto più in basso (sotto la cima) dove M torna uguale a M della cima
            double baseZ = lev[0].Z; bool surf = true;
            for (int k = i - 1; k > 0; k--)
            {
                if (lev[k - 1].M <= mTop)
                {
                    double f = (mTop - lev[k - 1].M) / Math.Max(1e-9, lev[k].M - lev[k - 1].M);
                    baseZ = lev[k - 1].Z + f * (lev[k].Z - lev[k - 1].Z);
                    surf = false;
                    break;
                }
            }
            double deficit = mMax - mTop;
            if (!r.HasDuct || deficit > r.DuctDeficit)
            {
                r.HasDuct = true; r.DuctBase = baseZ; r.DuctTop = lev[top].Z; r.DuctDeficit = deficit; r.SurfaceDuct = surf;
            }
            i = top;
        }

        // ---- strati fra livelli consecutivi: condotti risolti e potenziali (salto netto nascosto dal passo dei livelli)
        for (int i = 1; i < lev.Count; i++)
        {
            var a = lev[i - 1]; var b = lev[i];
            double dz = b.Z - a.Z;
            if (dz < 10) continue;
            if (UseDuctCeiling && a.Z > z0 + DuctCeilingM) break;
            double normal = UseCrplNormal ? CrplDrop(r.Ns, a.Z - z0, b.Z - z0) : 40 * dz / 1000.0;
            double excess = -(b.N - a.N) - normal;
            if (excess <= 0) continue;
            double qa = Q(a), qb = Q(b);
            double lapse = (a.T - b.T) / (dz / 1000.0);
            var ld = new LayerDuct
            {
                Key = Array.IndexOf(PLevels, (int)Math.Round(b.P)) is int kk && kk >= 0 ? kk : 90, Name = (i == 1 ? "suolo" : a.P.ToString("0")) + "–" + b.P.ToString("0") + " hPa",
                ZBot = a.Z, ZTop = b.Z, ExcessN = excess, LapseKkm = lapse, QRatio = qa > 0 ? qb / qa : 1
            };
            ld.Inversion = lapse < 3.0 && ld.QRatio < 0.8;
            ld.Resolved = b.M < a.M;
            double deficit, top = (a.Z + b.Z) / 2;
            if (ld.Resolved) { deficit = a.M - b.M; top = b.Z; }
            else if (ld.Inversion) { deficit = excess - 157 * 0.05; top = (a.Z + b.Z) / 2; }   // salto in circa 50 m a metà strato
            else deficit = 0;
            if (deficit > 0)
            {
                ld.Deficit = deficit; ld.DuctTop = top;
                double depth = deficit / 0.118;                 // sotto il salto M cresce di circa 118 unità/km
                if (ld.Resolved) depth = Math.Max(depth, b.Z - a.Z);
                ld.DuctBase = top - depth;
                if (ld.DuctBase <= lev[0].Z + 10) { ld.DuctBase = lev[0].Z; ld.Surface = true; }
                ld.LambdaMax = Kerr(deficit, ld.DuctTop - ld.DuctBase);
                if (!ld.Resolved && r.Clim.Ok) DualCalc(ld, excess, a, b, lev[0].Z, r.Clim);
            }
            r.Layers.Add(ld);
        }
        Weather(r, lev);
        return r;
    }

    /// <summary>
    /// Grandezze meteo della 1.3.0: stabilità di Misme (ΔW nel primo km), rifrazione potenziale K e suo calo più forte,
    /// subsidenza o moti ascendenti (velocità verticale a 850 e 700 hPa) e situazione di scirocco.
    /// </summary>
    static void Weather(ProfileResult r, List<Level> lev)
    {
        // ΔW = g·∫(θ(z) − θ0)/θ0 dz fra il suolo e 1 km sopra (J/kg): lavoro per sollevare l'aria contro la stabilità
        double z0 = lev[0].Z, th0 = Theta(lev[0].T, lev[0].P), w = 0;
        bool reached = false;
        for (int i = 1; i < lev.Count; i++)
        {
            double za = lev[i - 1].Z, zb = lev[i].Z;
            double ta = Theta(lev[i - 1].T, lev[i - 1].P) - th0, tb = Theta(lev[i].T, lev[i].P) - th0;
            if (zb > z0 + 1000)
            {
                double f = (z0 + 1000 - za) / Math.Max(1, zb - za);
                double tm = ta + f * (tb - ta);
                w += (ta + tm) / 2 * (z0 + 1000 - za);
                reached = true; break;
            }
            w += (ta + tb) / 2 * (zb - za);
        }
        if (reached)
        {
            r.DeltaW = 9.80665 * w / th0;
            r.StabDb = Math.Max(-4, Math.Min(6, MismeDbPerJ * (r.DeltaW - DeltaWStd)));
        }
        // K al suolo e calo più forte fra due livelli consecutivi (aria secca e calda sopra: subsidenza)
        r.KSurf = K(lev[0].P, lev[0].T, lev[0].RH);
        for (int i = 1; i < lev.Count; i++)
        {
            double drop = K(lev[i - 1].P, lev[i - 1].T, lev[i - 1].RH) - K(lev[i].P, lev[i].T, lev[i].RH);
            if (drop > r.KDrop) { r.KDrop = drop; r.KDropLayer = (i == 1 ? "suolo" : lev[i - 1].P.ToString("0")) + "–" + lev[i].P.ToString("0") + " hPa"; }
        }
        foreach (var l in lev) if (Math.Abs(l.P - 850) < 0.5) r.T850 = l.T;
        // moto verticale: −1 cm/s = discesa lenta tipica degli anticicloni; +5 cm/s = salita che rompe gli strati
        double wmin = Min(r.W850, r.W700), wmax = Max(r.W850, r.W700);
        bool inv = r.Layers.Any(l => l.Inversion || l.Resolved);
        r.Subsidence = !double.IsNaN(wmin) && wmax < -0.01 && (inv || r.KDrop >= 15);
        r.Ascent = !double.IsNaN(wmax) && wmax > 0.05;
        r.VertDb = r.Subsidence ? 1.5 : r.Ascent ? -2 : 0;
        // scirocco: vento da 100–200° a 850 hPa di almeno 7 m/s con aria calda (T850 ≥ 16 °C) sul Mediterraneo
        r.Scirocco = !double.IsNaN(r.Dir850) && r.Dir850 >= 100 && r.Dir850 <= 200 && r.Wind850 >= 7 && r.T850 >= 16 &&
                     r.Lat >= 28 && r.Lat <= 47 && r.Lon >= -10 && r.Lon <= 37;
        // 1.3.2: contrasto aria-mare sul mare: temperatura potenziale a 925 hPa (o 950) contro la SST, e aria a 2 m contro la SST
        if (r.Sea && !double.IsNaN(r.Sst))
        {
            r.SeaT2 = lev[0].T - r.Sst;
            foreach (var pl in new[] { 925.0, 950.0 })
            {
                int li = lev.FindIndex(x => Math.Abs(x.P - pl) < 0.5);
                if (li < 0) continue;
                var l = lev[li];
                r.SeaAirC = (l.T + 273.15) * Math.Pow(1000 / l.P, 0.2857) - 273.15 - r.Sst;
                r.Rh925 = l.RH; break;
            }
        }
    }

    /// <summary>Altezza del sole in gradi (formule NOAA semplificate, errore sotto 1°).</summary>
    public static double SunElevation(DateTime utc, double lat, double lon)
    {
        double d = (utc - new DateTime(2000, 1, 1, 12, 0, 0, DateTimeKind.Utc)).TotalDays;
        double g = (357.529 + 0.98560028 * d) * Math.PI / 180, q = 280.459 + 0.98564736 * d;
        double lam = (q + 1.915 * Math.Sin(g) + 0.020 * Math.Sin(2 * g)) * Math.PI / 180, eps = (23.439 - 0.00000036 * d) * Math.PI / 180;
        double dec = Math.Asin(Math.Sin(eps) * Math.Sin(lam));
        double ra = Math.Atan2(Math.Cos(eps) * Math.Sin(lam), Math.Cos(lam));
        double gmst = (18.697374558 + 24.06570982441908 * d) % 24 * 15 * Math.PI / 180;
        double ha = gmst + lon * Math.PI / 180 - ra, la = lat * Math.PI / 180;
        return Math.Asin(Math.Sin(la) * Math.Sin(dec) + Math.Cos(la) * Math.Cos(dec) * Math.Cos(ha)) * 180 / Math.PI;
    }

    static double Min(double a, double b) => double.IsNaN(a) ? b : double.IsNaN(b) ? a : Math.Min(a, b);
    static double Max(double a, double b) => double.IsNaN(a) ? b : double.IsNaN(b) ? a : Math.Max(a, b);

    /// <summary>Lunghezza d'onda massima intrappolata (m), Kerr 1951 eq. 17 con calo lineare di M nel condotto.</summary>
    public static double Kerr(double deficitM, double depthM) => deficitM > 0 && depthM > 0 ? 2 * Math.Sqrt(2) * (2.0 / 3.0) * Math.Sqrt(deficitM * 1e-6) * depthM : 0;

    /// <summary>
    /// Doppio calcolo di un condotto potenziale. A (già fatto): il salto di N in eccesso è concentrato in 50 m.
    /// B: lo stesso salto è distribuito sullo spessore climatologico ITU-R P.453 del condotto (al suolo o elevato) del punto,
    /// quindi il deficit è excess − 0,157·h e il condotto comprende lo strato di salto più il tratto sottostante.
    /// Per il deficit e per λmax si tiene il valore più basso dei due (il peggiore).
    /// </summary>
    static void DualCalc(LayerDuct ld, double excess, Level a, Level b, double ground, DuctClimate c)
    {
        ld.Dual = true;
        ld.DeficitA = ld.Deficit; ld.LambdaA = ld.LambdaMax;
        double thick = ld.Surface ? c.SThick : c.EThick, stren = ld.Surface ? c.SStren : c.EStren;
        ld.ClimThick = thick; ld.ClimStren = stren;
        ld.Anomaly = stren > 0 ? ld.DeficitA / stren : 0;
        double h = Math.Max(50, Math.Min(thick, b.Z - a.Z));
        double defB = excess - 0.157 * h;
        double topB = Math.Min(b.Z, (a.Z + b.Z) / 2 + h / 2);
        double baseB = 0; bool surfB = false;
        if (defB > 0)
        {
            baseB = topB - h - defB / 0.118;
            if (baseB <= ground + 10) { baseB = ground; surfB = true; }
        }
        ld.DeficitB = Math.Max(0, defB);
        ld.LambdaB = defB > 0 ? Kerr(defB, topB - baseB) : 0;
        if (ld.LambdaB < ld.LambdaA || ld.DeficitB < ld.DeficitA)
        {
            ld.Chosen = "B";
            ld.Deficit = Math.Min(ld.DeficitA, ld.DeficitB);
            ld.LambdaMax = Math.Min(ld.LambdaA, ld.LambdaB);
            if (defB > 0 && ld.LambdaB <= ld.LambdaA) { ld.DuctTop = topB; ld.DuctBase = baseB; ld.Surface = surfB; }
        }
        else ld.Chosen = "A";
    }

    /// <summary>Umidità specifica (kg/kg) di un livello.</summary>
    static double Q(Level l)
    {
        double e = Math.Max(0, Math.Min(100, l.RH)) / 100.0 * Es(l.T);
        return 0.622 * e / Math.Max(1, l.P - 0.378 * e);
    }

    static string Lang(string k) => k == "sub" ? "sub-rifrazione" : k;

    static double Tv(double tC, double rh, double p)
    {
        double e = Math.Max(0, Math.Min(100, rh)) / 100.0 * Es(tC);
        double q = 0.622 * e / Math.Max(1, p - 0.378 * e);
        return (tC + 273.15) * (1 + 0.61 * q);
    }

    static double Interp(List<Level> lev, double z)
    {
        for (int i = 1; i < lev.Count; i++)
            if (lev[i].Z >= z)
            {
                double f = (z - lev[i - 1].Z) / Math.Max(1e-6, lev[i].Z - lev[i - 1].Z);
                return lev[i - 1].N + f * (lev[i].N - lev[i - 1].N);
            }
        return lev[lev.Count - 1].N;
    }
}

/// <summary>Sorgente dei profili: GFS da Open-Meteo e/o NOMADS (stesso modello), oppure ICON-EU o ECMWF per tutta la griglia.</summary>
public enum WxSource { Auto, OpenMeteo, Nomads, IconEu, Ecmwf, IconItalia }

public static class WxSources
{
    public static readonly WxSource[] All = { WxSource.Auto, WxSource.OpenMeteo, WxSource.Nomads, WxSource.IconEu, WxSource.Ecmwf, WxSource.IconItalia };
    public static string Name(WxSource s) => s switch
    {
        WxSource.Auto => "GFS: NOMADS (NOAA); Open-Meteo solo se NOMADS non risponde",
        WxSource.OpenMeteo => "GFS da Open-Meteo",
        WxSource.Nomads => "GFS da NOMADS (NOAA)",
        WxSource.IconEu => "ICON-EU (DWD) da Open-Meteo",
        WxSource.Ecmwf => "ECMWF IFS 0,25° da Open-Meteo",
        WxSource.IconItalia => "ICON-2I Italia 2 km (ItaliaMeteo-ARPAE) da Open-Meteo",
        _ => s.ToString()
    };
    public static string Short(WxSource s) => s switch { WxSource.IconEu => "ICON-EU", WxSource.Ecmwf => "ECMWF", WxSource.IconItalia => "ICON-2I", _ => "GFS" };
    public static bool IsGfs(WxSource s) => s == WxSource.Auto || s == WxSource.OpenMeteo || s == WxSource.Nomads;
    /// <summary>Parametro models= di Open-Meteo.</summary>
    public static string OmModel(WxSource s) => s == WxSource.IconEu ? "icon_eu" : s == WxSource.Ecmwf ? "ecmwf_ifs025" : s == WxSource.IconItalia ? "italia_meteo_arpae_icon_2i" : "gfs_seamless";
    /// <summary>Metadati della corsa su Open-Meteo.</summary>
    public static string MetaUrl(WxSource s) => "https://api.open-meteo.com/data/" +
        (s == WxSource.IconEu ? "dwd_icon_eu" : s == WxSource.Ecmwf ? "ecmwf_ifs025" : s == WxSource.IconItalia ? "italia_meteo_arpae_icon_2i" : "ncep_gfs025") + "/static/meta.json";
    public static string CacheName(WxSource s) => s == WxSource.IconEu ? "iconeu" : s == WxSource.Ecmwf ? "ecmwf" : s == WxSource.IconItalia ? "icon2i" : "gfs";
    public static WxSource Parse(string v) => Enum.TryParse<WxSource>(v ?? "", out var r) ? r : WxSource.Auto;
}

/// <summary>Un caso storico da rivedere con l'archivio delle previsioni di Open-Meteo (QST, The World Above 50 MHz).</summary>
public sealed class HistCase
{
    public string Name = "", Note = "";
    public DateTime StartUtc; public int Days;
    public double LatMin, LatMax, LonMin, LonMax;
    public double FreqMHz = 144.3;

    public static readonly HistCase[] Known =
    {
        new HistCase { Name = "14–15 ago 2022 · Manitoba–Kansas/Missouri", StartUtc = new DateTime(2022, 8, 13, 0, 0, 0, DateTimeKind.Utc), Days = 4,
                       LatMin = 34, LatMax = 54, LonMin = -104, LonMax = -86, FreqMHz = 1296,
                       Note = "QST nov. 2022: apertura sotto un'alta pressione da 144 a 1296 MHz quasi tutta in FT8; record canadese a 10 GHz di 1228,56 km in Q65, segnali fino a 1941 km" },
        new HistCase { Name = "29 dic 2021 – 2 gen 2022 · Tirreno e Mediterraneo occidentale", StartUtc = new DateTime(2021, 12, 29, 0, 0, 0, DateTimeKind.Utc), Days = 5,
                       LatMin = 35, LatMax = 47, LonMin = -2, LonMax = 20, FreqMHz = 144.3,
                       Note = "rapporti IC8TEM su MMMonVHF: 29/12 prime aperture (ODX IK1PAG JN35TB 737 km), 30/12 secondo giorno, 31/12 apertura più ampia (IT9GSF–EB5EA 1217 km), 1/1/2022 grande apertura fra Italia, Sicilia e Spagna; quasi tutto in FT8 a 144 MHz" },
    };
}

/// <summary>
/// Previsione propria su una griglia: profili dal suolo a 500 hPa, ogni ora per 78 ore (GFS, ICON-EU o ECMWF).
/// Per contenere le richieste a Open-Meteo la quota dei livelli si ricava con l'equazione ipsometrica invece di scaricarla.
/// </summary>
public sealed class OwnForecast
{
    public double LatMin, LatMax, LonMin, LonMax, Step;
    public int Rows, Cols;
    public DateTime[] Times = Array.Empty<DateTime>();
    /// <summary>[ora][riga, colonna]: profilo analizzato o null.</summary>
    public ProfileResult[][,] Grid = Array.Empty<ProfileResult[,]>();
    public string Status = "";
    public DateTime UpdatedUtc;
    public string LastTiming = "";            // tempi dell'ultimo aggiornamento della griglia
    public DateTime RunUtc, RunAvailableUtc;  // corsa usata e quando Open-Meteo l'ha resa disponibile
    /// <summary>Sorgente scelta (vale per tutta la griglia e per i percorsi).</summary>
    public static WxSource Source = WxSource.Auto;
    /// <summary>Caso storico in corso (null = previsione attuale).</summary>
    public HistCase Hist;
    public WxSource UsedSource = WxSource.Auto;   // sorgente della griglia mostrata
    public string UsedDetail = "";                // es. "Open-Meteo 180 punti, NOMADS 60 punti"

    /// <summary>Corsa più recente del modello scelto su Open-Meteo (metadati); non blocca se non risponde.</summary>
    public async Task GfsRunInfo(HttpClient http, CancellationToken ct)
    {
        RunUtc = default; RunAvailableUtc = default;
        if (Hist != null) return;
        var src = Source;
        try
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            cts.CancelAfter(15000);
            using var mresp = await http.GetAsync(WxSources.MetaUrl(src), cts.Token);
            mresp.EnsureSuccessStatusCode();
            var json = await Compat.ReadString(mresp.Content, cts.Token);
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            DateTime U(string k) => root.TryGetProperty(k, out var v) && v.ValueKind == JsonValueKind.Number ? DateTimeOffset.FromUnixTimeSeconds(v.GetInt64()).UtcDateTime : default;
            RunUtc = U("last_run_initialisation_time"); RunAvailableUtc = U("last_run_availability_time");
            if (RunUtc != default)
                CalcLog.W($"{WxSources.Short(src)}: corsa delle {RunUtc:dd/MM HH:mm} UTC, disponibile su Open-Meteo dalle {RunAvailableUtc:HH:mm} UTC " +
                          $"(ritardo dalla corsa {CalcLog.Dur((RunAvailableUtc - RunUtc).TotalMilliseconds)}, età dei dati adesso {CalcLog.Dur((DateTime.UtcNow - RunUtc).TotalMilliseconds)})");
        }
        catch (Exception ex) when (!ct.IsCancellationRequested)
        {
            CalcLog.W($"{WxSources.Short(src)}: metadati della corsa non disponibili (" + ex.Message + ")");
            if (WxSources.IsGfs(src)) RunUtc = Nomads.Floor6(DateTime.UtcNow.AddHours(-5));   // per NOMADS e per la cache
        }
    }
    public bool Busy;
    DateTime _autoLastTry;

    /// <summary>
    /// 1.3.2: aggiornamento senza clic. Vero se non c'è ancora una griglia, se su NOMADS è uscita una corsa più nuova
    /// (circa 5 ore dopo l'ora della corsa) o se la zona visibile esce per più di un quarto dalla griglia scaricata.
    /// Mai con un caso storico o durante uno scaricamento; al massimo un tentativo ogni 15 minuti.
    /// </summary>
    public bool NeedsAutoUpdate(double la1, double la2, double lo1, double lo2)
    {
        if (Busy || Hist != null || DateTime.UtcNow - _autoLastTry < TimeSpan.FromMinutes(15)) return false;
        bool need;
        if (Grid.Length == 0) need = true;
        else if (WxSources.IsGfs(Source) && Nomads.Floor6(DateTime.UtcNow.AddHours(-5)) > RunUtc) need = true;
        else
        {
            double ia = Math.Max(0, Math.Min(la2, LatMax) - Math.Max(la1, LatMin)) * Math.Max(0, Math.Min(lo2, LonMax) - Math.Max(lo1, LonMin));
            double ba = Math.Max(1e-6, (la2 - la1) * (lo2 - lo1));
            need = ia / ba < 0.75;
        }
        if (need) _autoLastTry = DateTime.UtcNow;
        return need;
    }
    readonly object _lock = new();

    public const int Hours = 78;
    static readonly string[] SurfVars = { "surface_pressure", "temperature_2m", "relative_humidity_2m", "wind_speed_10m", "boundary_layer_height", "cape" };
    /// <summary>Variabili aggiunte nella 1.3.0: vento a 850 hPa (scirocco) e velocità verticale (subsidenza).</summary>
    public static readonly string[] ExtraVars = { "wind_speed_850hPa", "wind_direction_850hPa", "vertical_velocity_850hPa", "vertical_velocity_700hPa", "surface_temperature", "snow_depth" };
    /// <summary>Solo da NOMADS: livelli vicini al suolo per il condotto sul mare a bassa quota.</summary>
    public static readonly string[] NomadsLowVars = { "temperature_80m", "relative_humidity_80m", "temperature_pbl30", "relative_humidity_pbl30" };

    public static string HourlyVars()
    {
        var v = new List<string>(SurfVars);
        foreach (var p in Refractivity.PLevels) { v.Add($"temperature_{p}hPa"); v.Add($"relative_humidity_{p}hPa"); }
        v.AddRange(ExtraVars);
        return string.Join(",", v);
    }

    public sealed class PointSeries
    {
        public double Lat, Lon, Elev;
        public DateTime[] Times;
        public ProfileResult[] Profiles;
        public string From = "";
    }

    /// <summary>Limite di Open-Meteo raggiunto: messaggio chiaro (minuto, ora o giorno) per l'utente.</summary>
    public sealed class RateLimitException : Exception
    {
        public bool MinuteOnly;
        public RateLimitException(string m, bool minute = false) : base(m) { MinuteOnly = minute; }
    }

    /// <summary>Cartella della cache dei profili per una corsa (un file per punto della griglia), separata per modello.</summary>
    static string CacheRoot => Path.Combine(Settings.DataDir, "cache-gfs");
    static string Prefix(WxSource s) => "v2-" + WxSources.CacheName(s) + "-";
    static string CacheDir(DateTime run, WxSource s) => Path.Combine(CacheRoot, Prefix(s) + run.ToString("yyyyMMddHH", CultureInfo.InvariantCulture));
    static string CacheKey(LatLon p) => p.Lat.ToString("0.###", CultureInfo.InvariantCulture) + "_" + p.Lon.ToString("0.###", CultureInfo.InvariantCulture) + ".json";

    /// <summary>Tiene solo le cache delle ultime due corse di ogni modello (e cancella quelle del formato vecchio).</summary>
    static void CleanCache(DateTime run, WxSource s)
    {
        try
        {
            if (!Directory.Exists(CacheRoot)) return;
            string pre = Prefix(s);
            var keep = new[] { pre + run.ToString("yyyyMMddHH", CultureInfo.InvariantCulture), pre + run.AddHours(-6).ToString("yyyyMMddHH", CultureInfo.InvariantCulture) };
            foreach (var d in Directory.GetDirectories(CacheRoot))
            {
                var n = Path.GetFileName(d);
                bool old = !n.StartsWith("v2-", StringComparison.Ordinal);
                if (old || (n.StartsWith(pre, StringComparison.Ordinal) && !keep.Contains(n))) try { Directory.Delete(d, true); } catch { }
            }
        }
        catch { }
    }

    /// <summary>
    /// Profili dei punti. Con la corsa nota (run) i punti già scaricati per quella corsa si leggono dal disco e si chiedono
    /// solo quelli mancanti. Open-Meteo a lotti di 60 con 20 s di pausa; con la scelta automatica, al primo rifiuto per
    /// limite (minuto, ora o giorno) i punti mancanti si prendono da NOMADS, che dà lo stesso GFS. Con hist (caso storico)
    /// si usa l'archivio delle previsioni di Open-Meteo, senza cache.
    /// </summary>
    public static async Task<List<PointSeries>> FetchPoints(HttpClient http, IList<LatLon> pts, Action<string> progress, CancellationToken ct, FetchStats st = null,
                                                            DateTime run = default, WxSource? source = null, HistCase hist = null, Action<string> used = null,
                                                            double gridStep = 0.25, DateTime? onlyUtc = null)
    {
        st ??= new FetchStats();
        var src = source ?? Source;
        if (hist != null && src == WxSource.Nomads) src = WxSource.OpenMeteo;     // NOMADS tiene solo gli ultimi giorni
        CalcLog.W($"sorgente dei profili: {WxSources.Name(src)}" + (hist != null ? $" — archivio, caso {hist.Name}" : "") + $", {pts.Count} punti");
        var sw = new System.Diagnostics.Stopwatch();
        var raw = new string[pts.Count];
        var origin = new string[pts.Count];
        string cdir = run != default && hist == null ? CacheDir(run, src) : null;
        if (cdir != null)
        {
            CleanCache(run, src);
            for (int i = 0; i < pts.Count; i++)
            {
                var f = Path.Combine(cdir, CacheKey(pts[i]));
                if (File.Exists(f)) try { raw[i] = File.ReadAllText(f); origin[i] = "cache"; } catch { raw[i] = null; }
            }
        }
        var missing = Enumerable.Range(0, pts.Count).Where(i => raw[i] == null).ToList();
        st.Cached = pts.Count - missing.Count;
        if (st.Cached > 0) CalcLog.W($"{WxSources.Short(src)}: {st.Cached} punti su {pts.Count} già scaricati per la corsa delle {run:dd/MM HH} UTC (cache su disco), da scaricare {missing.Count}");
        void Save(int i)
        {
            if (cdir == null || raw[i] == null) return;
            try { Directory.CreateDirectory(cdir); File.WriteAllText(Path.Combine(cdir, CacheKey(pts[i])), raw[i]); } catch { }
        }

        // GFS: prima NOMADS (nessun limite giornaliero, ritagli GRIB2 della zona), poi Open-Meteo per i punti che mancano.
        // Con l'archivio storico (hist) NOMADS non serve: tiene solo gli ultimi giorni.
        async Task FromNomads()
        {
            var sub = missing.Select(i => pts[i]).ToList();
            var (nr, nrun) = await Nomads.FetchPoints(http, sub, Hours, run, progress, ct, st, Nomads.ResFor(gridStep), onlyUtc);
            bool sameRun = (run == default || nrun == run) && !onlyUtc.HasValue;   // un'ora sola (percorso) non va nella cache
            for (int k = 0; k < sub.Count; k++)
            {
                raw[missing[k]] = nr[k];
                if (nr[k] != null) { origin[missing[k]] = "NOMADS"; if (sameRun) Save(missing[k]); }
            }
            missing = Enumerable.Range(0, pts.Count).Where(i => raw[i] == null).ToList();
        }
        bool nomadsDown = false;
        if ((src == WxSource.Nomads || src == WxSource.Auto) && missing.Count > 0 && hist == null)
        {
            if (src == WxSource.Nomads) await FromNomads();
            else
                try { await FromNomads(); }
                catch (OperationCanceledException) when (ct.IsCancellationRequested) { throw; }
                catch (Exception ex)
                {
                    CalcLog.W("NOMADS non disponibile (" + ex.Message + ") → i punti mancanti si prendono da Open-Meteo (stesso GFS)");
                    progress?.Invoke("NOMADS non risponde: continuo con Open-Meteo…");
                    nomadsDown = true;
                    missing = Enumerable.Range(0, pts.Count).Where(i => raw[i] == null).ToList();
                }
            if (src == WxSource.Auto && missing.Count > 0 && !nomadsDown) CalcLog.W($"NOMADS: {missing.Count} punti mancanti (restano senza profilo: dalla 1.3.2 la sorgente automatica usa Open-Meteo solo se NOMADS non risponde)");
        }
        // 1.3.2: con la sorgente automatica Open-Meteo serve solo se NOMADS non risponde, e per i casi storici
        if (src != WxSource.Nomads && missing.Count > 0 && (src != WxSource.Auto || nomadsDown || hist != null))
        {
            await FromOpenMeteo(http, pts, missing, raw, origin, Save, progress, ct, st, src, hist, false);
            missing = Enumerable.Range(0, pts.Count).Where(i => raw[i] == null).ToList();
        }
        // analisi dei profili (anche di quelli letti dal disco)
        sw.Restart();
        var res = new List<PointSeries>();
        for (int i = 0; i < pts.Count; i++)
        {
            if (raw[i] == null) continue;
            using var d = JsonDocument.Parse(raw[i]);
            var ps = ParsePoint(d.RootElement, pts[i]);
            ps.From = origin[i] ?? "";
            res.Add(ps);
        }
        st.ParseMs += sw.Elapsed.TotalMilliseconds;
        var groups = origin.Where(x => x != null).GroupBy(x => x).Select(g => $"{g.Key} {g.Count()}");
        used?.Invoke(string.Join(", ", groups));
        return res;
    }

    static async Task FromOpenMeteo(HttpClient http, IList<LatLon> pts, List<int> missing, string[] raw, string[] origin, Action<int> save,
                                    Action<string> progress, CancellationToken ct, FetchStats st, WxSource src, HistCase hist, bool switchOnLimit)
    {
        var sw = new System.Diagnostics.Stopwatch();
        const int batch = 60;
        var vars = HourlyVars();
        bool sstDropped = false; int dropped = 0;
        string tail = hist == null
            ? $"&forecast_hours={Hours}"
            : $"&start_date={hist.StartUtc:yyyy-MM-dd}&end_date={hist.StartUtc.AddDays(hist.Days - 1):yyyy-MM-dd}";
        string host = hist == null ? "https://api.open-meteo.com/v1/forecast" : "https://historical-forecast-api.open-meteo.com/v1/forecast";
        string model = WxSources.OmModel(src);
        for (int i = 0; i < missing.Count; i += batch)
        {
            var idx = missing.Skip(i).Take(batch).ToList();
            var part = idx.Select(k => pts[k]).ToList();
            var lat = string.Join(",", part.Select(p => p.Lat.ToString("0.###", CultureInfo.InvariantCulture)));
            var lon = string.Join(",", part.Select(p => p.Lon.ToString("0.###", CultureInfo.InvariantCulture)));
            string Url() => $"{host}?latitude={lat}&longitude={lon}&hourly={vars}&models={model}{tail}&wind_speed_unit=ms&timezone=GMT";
            var url = Url();
            string json = null;
            for (int attempt = 0; attempt < 3 && json == null; attempt++)
            {
                sw.Restart();
                using var resp = await http.GetAsync(url, ct);
                if ((int)resp.StatusCode == 429)
                {
                    st.NetMs += sw.Elapsed.TotalMilliseconds; st.Retries++;
                    string reason = "";
                    try
                    {
                        var body = await Compat.ReadString(resp.Content, ct);
                        using var ed = JsonDocument.Parse(body);
                        reason = ed.RootElement.TryGetProperty("reason", out var rr) ? rr.GetString() ?? "" : body;
                    }
                    catch { }
                    CalcLog.W("Open-Meteo: richiesta rifiutata per limite (HTTP 429): " + (reason == "" ? "nessuna spiegazione" : reason));
                    var low = reason.ToLowerInvariant();
                    if (low.Contains("hour"))
                        throw new RateLimitException($"limite orario di Open-Meteo raggiunto: riprova dopo le {DateTime.Now.AddHours(1):HH}:00 (la griglia già scaricata resta salvata)");
                    if (low.Contains("daily") || low.Contains("day"))
                        throw new RateLimitException("limite giornaliero di Open-Meteo raggiunto: riprova domani (la griglia già scaricata resta salvata)");
                    if (switchOnLimit) throw new RateLimitException("limite al minuto di Open-Meteo", true);
                    progress?.Invoke("Open-Meteo: limite al minuto, attendo 60 s…");
                    sw.Restart(); await Task.Delay(60000, ct); st.WaitMs += sw.Elapsed.TotalMilliseconds; continue;
                }
                if ((int)resp.StatusCode == 400)
                {
                    string body = ""; try { body = await Compat.ReadString(resp.Content, ct); } catch { }
                    // non tutti i modelli hanno tutte le variabili (SST, strato limite…): si toglie quella rifiutata e si riprova
                    var bad = vars.Split(',').FirstOrDefault(v => v.Length > 0 && body.Contains(v));
                    if (bad == null && !sstDropped && vars.Contains(",surface_temperature")) bad = "surface_temperature";
                    if (bad != null && dropped < 8)
                    {
                        dropped++; if (bad == "surface_temperature") sstDropped = true;
                        vars = string.Join(",", vars.Split(',').Where(v => v != bad)); url = Url(); attempt--;
                        CalcLog.W($"Open-Meteo {model}: la variabile {bad} non c'è per questo modello, richiesta ripetuta senza");
                        continue;
                    }
                    throw new HttpRequestException("Open-Meteo ha rifiutato la richiesta (400): " + (body.Length > 300 ? body.Substring(0, 300) : body));
                }
                resp.EnsureSuccessStatusCode();
                json = await Compat.ReadString(resp.Content, ct);
                st.NetMs += sw.Elapsed.TotalMilliseconds; st.Batches++; st.Bytes += json.Length;
            }
            if (json == null) throw new RateLimitException("Open-Meteo continua a rifiutare per il limite al minuto: riprova fra qualche minuto", true);
            using (var doc = JsonDocument.Parse(json))
            {
                var items = doc.RootElement.ValueKind == JsonValueKind.Array ? doc.RootElement.EnumerateArray().ToList() : new List<JsonElement> { doc.RootElement };
                for (int k = 0; k < items.Count && k < idx.Count; k++)
                {
                    raw[idx[k]] = items[k].GetRawText(); origin[idx[k]] = "Open-Meteo";
                    save(idx[k]);
                }
            }
            int done = Math.Min(missing.Count, i + batch);
            progress?.Invoke($"{WxSources.Short(src)}: {done} / {missing.Count} punti da scaricare");
            CalcLog.W($"Open-Meteo {model}: lotto di {part.Count} punti letto ({json.Length / 1024} kB), {(hist == null ? Hours + " ore" : hist.Days + " giorni d'archivio")}, {HourlyVars().Split(',').Length} variabili");
            if (i + batch < missing.Count)
            {
                progress?.Invoke($"{WxSources.Short(src)}: {done} / {missing.Count} punti — pausa di 20 s per il limite di Open-Meteo");
                sw.Restart(); await Task.Delay(20000, ct); st.WaitMs += sw.Elapsed.TotalMilliseconds;
            }
        }
    }

    internal static PointSeries ParsePoint(JsonElement it, LatLon p)
    {
        var h = it.GetProperty("hourly");
        var times = h.GetProperty("time").EnumerateArray()
            .Select(t => DateTime.SpecifyKind(DateTime.Parse(t.GetString(), CultureInfo.InvariantCulture), DateTimeKind.Utc)).ToArray();
        double elev = it.TryGetProperty("elevation", out var ev) && ev.ValueKind == JsonValueKind.Number ? ev.GetDouble() : 0;
        double[] A(string name) => h.TryGetProperty(name, out var a) && a.ValueKind == JsonValueKind.Array
            ? a.EnumerateArray().Select(x => x.ValueKind == JsonValueKind.Number ? x.GetDouble() : double.NaN).ToArray()
            : Enumerable.Repeat(double.NaN, times.Length).ToArray();
        var ps = A("surface_pressure"); var t2 = A("temperature_2m"); var rh2 = A("relative_humidity_2m");
        var w = A("wind_speed_10m"); var blh = A("boundary_layer_height"); var cape = A("cape");
        var tl = Refractivity.PLevels.Select(pl => A($"temperature_{pl}hPa")).ToArray();
        var rl = Refractivity.PLevels.Select(pl => A($"relative_humidity_{pl}hPa")).ToArray();
        var ws85 = A("wind_speed_850hPa"); var wd85 = A("wind_direction_850hPa"); var vv85 = A("vertical_velocity_850hPa"); var vv70 = A("vertical_velocity_700hPa");
        var snow = A("snow_depth");
        var sst = A("surface_temperature"); var t80 = A("temperature_80m"); var rh80 = A("relative_humidity_80m"); var tl30 = A("temperature_pbl30"); var rhl30 = A("relative_humidity_pbl30");
        var prof = new ProfileResult[times.Length];
        for (int t = 0; t < times.Length; t++)
        {
            var tt = new double[tl.Length]; var rr = new double[rl.Length];
            for (int j = 0; j < tl.Length; j++) { tt[j] = t < tl[j].Length ? tl[j][t] : double.NaN; rr[j] = t < rl[j].Length ? rl[j][t] : double.NaN; }
            var ex = new Refractivity.Extra { W850 = At(vv85, t), W700 = At(vv70, t), Wind850 = At(ws85, t), Dir850 = At(wd85, t),
                                              Sst = At(sst, t), Snow = At(snow, t), T80 = At(t80, t), Rh80 = At(rh80, t), TL30 = At(tl30, t), RhL30 = At(rhl30, t), Utc = times[t] };
            prof[t] = Refractivity.Analyze(elev, At(ps, t), At(t2, t), At(rh2, t), tt, rr, At(w, t), At(blh, t), At(cape, t), p.Lat, p.Lon, ex);
        }
        return new PointSeries { Lat = p.Lat, Lon = p.Lon, Elev = elev, Times = times, Profiles = prof };
    }

    static double At(double[] a, int t) => t < a.Length ? a[t] : double.NaN;

    /// <summary>Griglia sul riquadro dato, con un passo scelto per restare sotto circa 240 punti.</summary>
    public async Task<bool> UpdateAsync(HttpClient http, double la1, double la2, double lo1, double lo2, Action changed, CancellationToken ct)
    {
        if (Busy) return false;
        Busy = true;
        try
        {
            la1 = Math.Max(-70, la1); la2 = Math.Min(75, la2); lo1 = Math.Max(-180, lo1); lo2 = Math.Min(180, lo2);
            double step = 0.5;
            foreach (var s in new[] { 0.5, 0.75, 1.0, 1.25, 1.5, 2.0, 2.5, 3.0, 4.0, 5.0 })
            {
                step = s;
                if ((Math.Floor((la2 - la1) / s) + 2) * (Math.Floor((lo2 - lo1) / s) + 2) <= 240) break;
            }
            double latMin = Math.Floor(la1 / step) * step, lonMin = Math.Floor(lo1 / step) * step;
            int rows = (int)Math.Ceiling((la2 - latMin) / step) + 1, cols = (int)Math.Ceiling((lo2 - lonMin) / step) + 1;
            var pts = new List<LatLon>();
            for (int r = 0; r < rows; r++) for (int c = 0; c < cols; c++) pts.Add(new LatLon(latMin + r * step, lonMin + c * step));
            var src = Source; var hist = Hist;
            Status = $"{WxSources.Short(src)}: scarico {pts.Count} punti (passo {step:0.##}°)" + (hist != null ? $" — caso storico {hist.Name}" : "") + "…"; changed?.Invoke();
            var tm = new Timing();
            await GfsRunInfo(http, ct);
            tm.Lap("metadati della corsa");
            var fs = new FetchStats();
            string detail = "";
            var series = await FetchPoints(http, pts, m => { Status = m; changed?.Invoke(); }, ct, fs, RunUtc, src, hist, d => detail = d, step);
            tm.Lap("scaricamento e analisi");
            // ore: quelle della serie più lunga; ogni punto si colloca per ora (i punti della cache possono partire prima)
            var times = series.Where(s => s.Times != null).OrderByDescending(s => s.Times.Length).FirstOrDefault()?.Times ?? Array.Empty<DateTime>();
            var tix = new Dictionary<DateTime, int>();
            for (int t = 0; t < times.Length; t++) tix[times[t]] = t;
            var grid = new ProfileResult[times.Length][,];
            for (int t = 0; t < times.Length; t++) grid[t] = new ProfileResult[rows, cols];
            var key = new Dictionary<(double, double), int>();
            for (int i = 0; i < pts.Count; i++) key[(Math.Round(pts[i].Lat, 3), Math.Round(pts[i].Lon, 3))] = i;
            foreach (var ser in series)
            {
                if (!key.TryGetValue((Math.Round(ser.Lat, 3), Math.Round(ser.Lon, 3)), out int i)) continue;
                int r = i / cols, c = i % cols;
                for (int k = 0; k < ser.Times.Length && k < ser.Profiles.Length; k++)
                    if (tix.TryGetValue(ser.Times[k], out int t)) grid[t][r, c] = ser.Profiles[k];
                // serie ogni 3 ore (NOMADS a 0,5° o 1°) in una griglia oraria: le ore mancanti prendono il profilo più vicino entro 90 minuti
                if (ser.Times.Length > 1 && (ser.Times[1] - ser.Times[0]).TotalHours > 1.5)
                    for (int t = 0; t < times.Length; t++)
                    {
                        if (grid[t][r, c] != null) continue;
                        int bk = -1; double bd = 91;
                        for (int k = 0; k < ser.Times.Length && k < ser.Profiles.Length; k++) { double dm = Math.Abs((ser.Times[k] - times[t]).TotalMinutes); if (dm < bd && ser.Profiles[k] != null) { bd = dm; bk = k; } }
                        if (bk >= 0) grid[t][r, c] = ser.Profiles[bk];
                    }
            }
            MarkHighPressure(grid, rows, cols);
            if (hist == null && Refractivity.UseSeaAir)
            {
                try { await SstMemory(http, grid, rows, cols, latMin, lonMin, step, ct); tm.Lap("memoria della temperatura del mare"); }
                catch (OperationCanceledException) { throw; }
                catch { }
            }
            UsedSource = src; UsedDetail = detail;
            lock (_lock)
            {
                LatMin = latMin; LonMin = lonMin; LatMax = latMin + (rows - 1) * step; LonMax = lonMin + (cols - 1) * step;
                Step = step; Rows = rows; Cols = cols; Times = times; Grid = grid; _persist.Clear(); _cellIdx.Clear();
            }
            UpdatedUtc = DateTime.UtcNow;
            {
                int ti = 0; int nd = 0, nOk = 0; double mx = 0; ProfileResult best = null; double bl = 0, bo = 0;
                if (times.Length > 0)
                    for (int r = 0; r < rows; r++) for (int c = 0; c < cols; c++)
                    {
                        var pr = grid[ti][r, c]; if (pr == null) continue; nOk++;
                        if (pr.HasDuct) nd++;
                        double v = pr.Index(144.3); if (v > mx) { mx = v; best = pr; bl = latMin + r * step; bo = lonMin + c * step; }
                    }
                CalcLog.Section($"Griglia della previsione propria");
                CalcLog.W($"riquadro {latMin:0.##}–{latMin + (rows - 1) * step:0.##} N, {lonMin:0.##}–{lonMin + (cols - 1) * step:0.##} E, passo {step:0.##}°, {rows}×{cols} punti, {times.Length} ore dal {(times.Length > 0 ? times[0].ToString("dd/MM HH:mm") : "—")} UTC");
                CalcLog.W($"prima ora: profili validi {nOk}, con condotto {nd}; indice 144 MHz massimo {mx:0.0} a {bl:0.##}, {bo:0.##}");
                if (best != null) CalcLog.Profile($"profilo con l'indice più alto ({bl:0.##}, {bo:0.##})", best);
            }
            tm.Lap("griglia e riepilogo");
            LastTiming = $"pronta in {CalcLog.Dur(tm.TotalMs)}: {fs.Summary()}";
            CalcLog.W($"TEMPI griglia ({pts.Count} punti): totale {CalcLog.Dur(tm.TotalMs)} — {tm.Summary()}; {fs.Summary()}");
            Status = (hist != null ? $"Caso storico {hist.Name}: {WxSources.Short(src)} dall'archivio di Open-Meteo"
                                   : $"Previsione propria: {WxSources.Short(src)} {UpdatedUtc:dd/MM HH:mm} UTC" + (RunUtc != default ? $" (corsa {RunUtc:HH} UTC)" : "")) +
                     $", {pts.Count} punti, passo {step:0.##}°" + (detail != "" ? $" ({detail})" : "") + $" · {LastTiming}";
            return true;
        }
        catch (OperationCanceledException) when (ct.IsCancellationRequested) { Status = "Previsione propria: interrotta"; return false; }
        catch (OperationCanceledException) { Status = "Previsione propria: tempo scaduto nello scaricamento, riprova"; CalcLog.W(Status); return false; }
        catch (RateLimitException ex)
        {
            Status = "Previsione propria: " + ex.Message + (WxSources.IsGfs(Source) && Source != WxSource.Auto && Hist == null
                ? " — scegli «GFS: NOMADS» o «GFS da NOMADS» in Sorgente dei dati per continuare senza Open-Meteo" : "");
            CalcLog.W(Status); return false;
        }
        catch (Exception ex) { Status = "Previsione propria non riuscita: " + ex.Message; CalcLog.W("ERRORE: " + ex); Core.ErrorLog.Write(ex, "previsione propria (griglia)"); return false; }
        finally { Busy = false; changed?.Invoke(); }
    }

    int TimeIndex(DateTime utc)
    {
        if (Times.Length == 0) return -1;
        int best = 0; double bd = double.MaxValue;
        for (int i = 0; i < Times.Length; i++) { double d = Math.Abs((Times[i] - utc).TotalMinutes); if (d < bd) { bd = d; best = i; } }
        return bd <= 90 ? best : -1;
    }

    /// <summary>Profilo della cella più vicina (null fuori dalla griglia o senza dati).</summary>
    public ProfileResult At(double lat, double lon, DateTime utc)
    {
        lock (_lock)
        {
            int t = TimeIndex(utc);
            if (t < 0 || Rows == 0) return null;
            double fr = (lat - LatMin) / Step, fc = (lon - LonMin) / Step;
            if (fr < -0.5 || fc < -0.5 || fr > Rows - 0.5 || fc > Cols - 0.5) return null;
            int r = (int)Math.Round(fr), c = (int)Math.Round(fc);
            r = Math.Max(0, Math.Min(Rows - 1, r)); c = Math.Max(0, Math.Min(Cols - 1, c));
            return Grid[t][r, c];
        }
    }

    // 1.4.0: indice di ogni cella per ora, banda e interruttori del calcolo, calcolato una volta sola. Prima la mappa
    // ricalcolava GainDb e la persistenza di quattro celle per ogni quadretto a ogni ridisegno (cursore delle ore,
    // zoom). I valori sono gli stessi: cambia solo che non si rifanno.
    readonly Dictionary<(int t, double f, bool corr, bool soft, bool pers, bool hp, bool sea), double[,]> _cellIdx = new();

    double[,] CellIndex(int t, double f)
    {
        var key = (t, f, Refractivity.UseCorrections, Refractivity.UseSoftDuct, Refractivity.UsePersistence, Refractivity.UseHighPressure, Refractivity.UseSeaAir);
        if (_cellIdx.TryGetValue(key, out var a)) return a;
        a = new double[Rows, Cols];
        for (int r = 0; r < Rows; r++)
            for (int c = 0; c < Cols; c++)
            {
                var p = Grid[t][r, c];
                if (p == null) { a[r, c] = double.NaN; continue; }
                double g = p.GainDb(f);
                double pb = PersistBonus(t, r, c, f, g, p);
                a[r, c] = Math.Max(0, Math.Min(10, (g + pb) / 2.5));
            }
        if (_cellIdx.Count > 400) _cellIdx.Clear();
        _cellIdx[key] = a;
        return a;
    }

    /// <summary>Indice 0–10 interpolato fra le quattro celle vicine; NaN dove non ci sono dati.</summary>
    public double Index(double lat, double lon, DateTime utc, double freqMHz)
    {
        lock (_lock)
        {
            int t = TimeIndex(utc);
            if (t < 0 || Rows < 2 || Cols < 2) return double.NaN;
            double fr = (lat - LatMin) / Step, fc = (lon - LonMin) / Step;
            if (fr < 0 || fc < 0 || fr > Rows - 1 || fc > Cols - 1) return double.NaN;
            int r0 = Math.Min(Rows - 2, (int)fr), c0 = Math.Min(Cols - 2, (int)fc);
            double tr = fr - r0, tc = fc - c0, sum = 0, wsum = 0;
            var cell = CellIndex(t, freqMHz);
            void Add(int r, int c, double w)
            {
                double v = cell[r, c];
                if (double.IsNaN(v) || w <= 0) return;
                sum += w * v; wsum += w;
            }
            Add(r0, c0, (1 - tr) * (1 - tc)); Add(r0, c0 + 1, (1 - tr) * tc); Add(r0 + 1, c0, tr * (1 - tc)); Add(r0 + 1, c0 + 1, tr * tc);
            if (wsum < 0.25) return double.NaN;
            double idx = sum / wsum;
            // previsione mediata dagli spot: sotto un corridoio confermato (tratte parallele indipendenti) il guadagno
            // non scende sotto il minimo del corridoio, solo vicino all'ora attuale
            if (Nowcast != null) { double fl = Nowcast(lat, lon, utc, freqMHz); if (!double.IsNaN(fl)) idx = Math.Max(idx, Math.Min(10, fl / 2.5)); }
            return idx;
        }
    }

    public bool HasData { get { lock (_lock) return Rows > 0 && Times.Length > 0; } }

    /// <summary>Correzione dagli spot (corridoi): guadagno minimo in dB nel punto, o NaN. Impostata da AppState.</summary>
    public static Func<double, double, DateTime, double, double> Nowcast;

    // persistenza: frazione delle 24 ore precedenti in cui la cella era sopra 13 dB (cache per ora e frequenza)
    readonly Dictionary<(int t, double f, bool corr, bool soft), double[,]> _persist = new();

    /// <summary>
    /// Bonus di persistenza (VHF/UHF DX Book: le aperture maggiori arrivano dopo giorni di alta pressione; Biancomano e
    /// Leyshon 1998: persistenza a 12 ore): fino a +2 dB in proporzione alle ore delle 24 precedenti con guadagno ≥ 13 dB,
    /// solo se la cella è già sopra 10 dB. Senza condotto il totale resta entro 16 dB.
    /// </summary>
    double PersistBonus(int t, int r, int c, double f, double g, ProfileResult p)
    {
        if (!Refractivity.UsePersistence || !Refractivity.UseCorrections || g < 10 || t <= 0) return 0;
        var key = (t, f, Refractivity.UseCorrections, Refractivity.UseSoftDuct);
        if (!_persist.TryGetValue(key, out var arr))
        {
            arr = new double[Rows, Cols];
            int t0 = Math.Max(0, t - Refractivity.PersistHours);
            for (int rr = 0; rr < Rows; rr++)
                for (int cc = 0; cc < Cols; cc++)
                {
                    int n = 0, hi = 0;
                    for (int k = t0; k < t; k++)
                    {
                        var q = Grid[k][rr, cc]; if (q == null) continue;
                        n++; if (q.GainDb(f) >= Refractivity.PersistThrDb) hi++;
                    }
                    arr[rr, cc] = n > 0 ? (double)hi / Math.Max(n, Math.Min(Refractivity.PersistHours, 12)) : 0;
                }
            if (_persist.Count > 400) _persist.Clear();
            _persist[key] = arr;
        }
        double b = Refractivity.PersistMaxDb * arr[r, c];
        if (!p.DuctLike(f)) b = Math.Max(0, Math.Min(b, Refractivity.NoDuctCapDb - g));
        return b;
    }

    /// <summary>Persistenza della cella più vicina (frazione 0–1 delle 24 ore precedenti sopra 13 dB), per il calcolo sul percorso.</summary>
    public double PersistAt(double lat, double lon, DateTime utc, double f)
    {
        lock (_lock)
        {
            int t = TimeIndex(utc);
            if (t <= 0 || Rows == 0) return 0;
            int r = (int)Math.Round((lat - LatMin) / Step), c = (int)Math.Round((lon - LonMin) / Step);
            if (r < 0 || c < 0 || r >= Rows || c >= Cols || Grid[t][r, c] == null) return 0;
            PersistBonus(t, r, c, f, 99, Grid[t][r, c]);
            return _persist.TryGetValue((t, f, Refractivity.UseCorrections, Refractivity.UseSoftDuct), out var a) ? a[r, c] : 0;
        }
    }

    /// <summary>Celle della griglia all'ora più vicina (per i segni meteo sulla mappa).</summary>
    /// <summary>
    /// Alta pressione estesa: almeno 7 delle 9 celle (la cella e le vicine) con pressione ridotta al mare ≥ 1020 hPa,
    /// e vento debole nei bassi strati nella cella (10 m &lt; 5 m/s, 850 hPa &lt; 8 m/s). Anticiclone stabile: strati che durano.
    /// </summary>
    /// <summary>
    /// 1.3.2: memoria della temperatura del mare. I modelli tengono la SST quasi ferma durante la previsione, quindi il calo
    /// recente si prende dalle analisi delle 72 ore prima (Open-Meteo, past_days=3, solo surface_temperature, solo celle di
    /// mare): SstDrop = massimo delle 72 ore prima − valore attuale. Su un mare basso (alto Adriatico, golfo del Leone) una
    /// bora o un maestrale lo raffreddano di alcuni gradi in pochi giorni. Se il servizio non risponde, nessuna correzione.
    /// </summary>
    static async Task SstMemory(HttpClient http, ProfileResult[][,] grid, int rows, int cols, double latMin, double lonMin, double step, CancellationToken ct)
    {
        if (grid.Length == 0) return;
        var cells = new List<(int r, int c)>();
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
            {
                ProfileResult p = null;
                foreach (var g in grid) { p = g[r, c]; if (p != null) break; }
                if (p != null && p.Sea && !double.IsNaN(p.Sst)) cells.Add((r, c));
            }
        var now = DateTime.UtcNow;
        for (int i = 0; i < cells.Count; i += 80)
        {
            var part = cells.Skip(i).Take(80).ToList();
            var lat = string.Join(",", part.Select(x => (latMin + x.r * step).ToString("0.###", CultureInfo.InvariantCulture)));
            var lon = string.Join(",", part.Select(x => (lonMin + x.c * step).ToString("0.###", CultureInfo.InvariantCulture)));
            var url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&hourly=surface_temperature&past_days=3&forecast_days=1&timezone=GMT";
            using var resp = await http.GetAsync(url, ct);
            if (!resp.IsSuccessStatusCode) return;
            using var d = JsonDocument.Parse(await resp.Content.ReadAsStringAsync());
            var arr = d.RootElement.ValueKind == JsonValueKind.Array ? d.RootElement.EnumerateArray().ToList() : new List<JsonElement> { d.RootElement };
            for (int k = 0; k < arr.Count && k < part.Count; k++)
            {
                if (!arr[k].TryGetProperty("hourly", out var h) || !h.TryGetProperty("time", out var tj) || !h.TryGetProperty("surface_temperature", out var vj)) continue;
                var ts = tj.EnumerateArray().Select(x => DateTime.SpecifyKind(DateTime.Parse(x.GetString() ?? "", CultureInfo.InvariantCulture), DateTimeKind.Utc)).ToList();
                var vs = vj.EnumerateArray().Select(x => x.ValueKind == JsonValueKind.Number ? x.GetDouble() : double.NaN).ToList();
                double cur = double.NaN, mx = double.NaN;
                for (int j = 0; j < ts.Count && j < vs.Count; j++)
                {
                    if (double.IsNaN(vs[j]) || ts[j] > now) continue;
                    if ((now - ts[j]).TotalHours <= 72) mx = double.IsNaN(mx) ? vs[j] : Math.Max(mx, vs[j]);
                    cur = vs[j];
                }
                if (double.IsNaN(cur) || double.IsNaN(mx)) continue;
                double drop = mx - cur;
                foreach (var g in grid) { var p = g[part[k].r, part[k].c]; if (p != null) p.SstDrop = drop; }
            }
        }
    }

    static void MarkHighPressure(ProfileResult[][,] grid, int rows, int cols)
    {
        foreach (var g in grid)
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                {
                    var p = g[r, c]; if (p == null) continue;
                    int hi = 0, n = 0;
                    for (int dr = -1; dr <= 1; dr++)
                        for (int dc = -1; dc <= 1; dc++)
                        {
                            int rr = r + dr, cc = c + dc;
                            if (rr < 0 || cc < 0 || rr >= rows || cc >= cols || g[rr, cc] == null || double.IsNaN(g[rr, cc].Pmsl)) continue;
                            n++; if (g[rr, cc].Pmsl >= Refractivity.HighPHpa) hi++;
                        }
                    bool calm = !(p.Wind10 >= Refractivity.HighPWind10) && !(p.Wind850 >= Refractivity.HighPWind850);
                    p.HighP = n >= 4 && hi >= Math.Ceiling(n * 7.0 / 9) && calm;
                }
    }

    public List<(double lat, double lon, ProfileResult p)> Cells(DateTime utc)
    {
        var res = new List<(double, double, ProfileResult)>();
        lock (_lock)
        {
            int t = TimeIndex(utc);
            if (t < 0) return res;
            for (int r = 0; r < Rows; r++) for (int c = 0; c < Cols; c++)
            {
                var p = Grid[t][r, c];
                if (p != null) res.Add((LatMin + r * Step, LonMin + c * Step, p));
            }
        }
        return res;
    }
}
