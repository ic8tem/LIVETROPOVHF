using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using TropoVHF.Core;
using TropoVHF.Forecast;

namespace TropoVHF.Ui;

// Parti della linguetta Previsioni comuni alla versione normale (Avalonia) e a quella per Windows 7 (WinForms con lo
// strato di compatibilità): testi e fumetti dei risultati, disegno della mappa e del profilo del terreno.

public enum FcTone { Normal, Strong, Accent, Ok, Warn, Bad, Muted }

/// <summary>Testi dei risultati e fumetti con fonte e richiamo al manuale.</summary>
public static class ForecastText
{
    static string L(string it, string en) => Lang.Current == "it" ? it : en;

    /// <summary>Fumetti dei risultati: fonte e richiamo al manuale delle previsioni, per etichetta (anche come prefisso).</summary>
    static Dictionary<string, string> TipsDict
    {
        get
        {
            if (_tips == null || _tipsLang != Lang.Current) { _tips = BuildTips(); _tipsLang = Lang.Current; }
            return _tips;
        }
    }
    static Dictionary<string, string> _tips; static string _tipsLang;
    static Dictionary<string, string> BuildTips() => new Dictionary<string, string>
    {
        [L("Distanza / azimut", "Distance / azimuth")] = L("Distanza e azimut sul cerchio massimo (formula dell'emisenoverso) fra i due locatori; con 8–10 caratteri la posizione è precisa a pochi metri. Manuale delle previsioni, § 7.", "Great-circle distance and azimuth (haversine) between the locators; 8–10 characters give a position to a few metres. Forecast manual, § 7."),
        [L("Antenne sul mare TX / RX", "Antennas above sea TX / RX")] = L("Quota del suolo da SRTM1 (NASA/USGS, 1\", tramite i riquadri skadi di AWS) più l'altezza dal suolo; con locatore a 4–6 caratteri vale la quota sul mare indicata. Manuale delle previsioni, § 7.", "Ground height from SRTM1 (NASA/USGS, 1\", AWS skadi tiles) plus height above ground; with a 4–6 character locator the stated height above sea is used. Forecast manual, § 7."),
        [L("Raggio equivalente k", "Effective radius k")] = L("k = 157 / (157 + ΔN), con ΔN la variazione di rifrattività nel primo km dai profili GFS lungo la tratta (ITU-R P.452 § 4.3, ITU-R P.834). Limitato fra 0,6 e 4. Manuale delle previsioni, § 8.", "k = 157 / (157 + ΔN), ΔN being the refractivity change in the first km from GFS profiles along the path (ITU-R P.452 § 4.3, ITU-R P.834). Limited to 0.6–4. Forecast manual, § 8."),
        [L("In vista ottica", "Line of sight")] = L("Metodo di Bullington sul profilo SRTM1 con la curvatura data da k (ITU-R P.452 § 4.2.1). Manuale delle previsioni, § 8.", "Bullington method on the SRTM1 profile with the curvature given by k (ITU-R P.452 § 4.2.1). Forecast manual, § 8."),
        [L("1ª zona di Fresnel", "1st Fresnel zone")] = L("Raggio F1 = √(λ·d1·d2/d); libera se lo spazio sopra il terreno è almeno 0,6 F1. Ostacoli: parametro ν e perdita J(ν) del bordo a coltello, ITU-R P.526 eq. 31. Manuale delle previsioni, § 8.", "Radius F1 = √(λ·d1·d2/d); clear if the space above ground is at least 0.6 F1. Obstacles: ν and knife-edge loss J(ν), ITU-R P.526 eq. 31. Forecast manual, § 8."),
        [L("Spazio libero", "Free space")] = L("Lfs = 92,44 + 20 log f(GHz) + 20 log d(km), ITU-R P.525. Manuale delle previsioni, § 8.", "Lfs = 92.44 + 20 log f(GHz) + 20 log d(km), ITU-R P.525. Forecast manual, § 8."),
        [L("Diffrazione (Bullington)", "Diffraction (Bullington)")] = L("Bullington sul profilo reale più la correzione di terra sferica «delta-Bullington» (ITU-R P.452-16 § 4.2, eq. 161–164) con la perdita sferica di ITU-R P.526 § 3. Manuale delle previsioni, § 8.", "Bullington on the real profile plus the spherical-earth delta-Bullington correction (ITU-R P.452-16 § 4.2, eq. 161–164) with ITU-R P.526 § 3 spherical loss. Forecast manual, § 8."),
        [L("Troposcatter (P.452)", "Troposcatter (P.452)")] = L("ITU-R P.452-16 eq. 45 al 50% del tempo, con distanza angolare θ dagli orizzonti, N0 dai profili GFS e perdita d'accoppiamento d'apertura dai guadagni DL6WU. Manuale delle previsioni, § 8.", "ITU-R P.452-16 eq. 45 at 50% of time, angular distance θ from the horizons, N0 from GFS profiles and aperture coupling loss from DL6WU gains. Forecast manual, § 8."),
        [L("Troposcatter, altri modelli", "Troposcatter, other models")] = L("ITU-R P.617-5 (mediana) con N0 e ΔN presi ogni ora dai profili GFS invece che dalle mappe climatologiche, e la formula del VHF/UHF Handbook RSGB (DL2FZN). La differenza fra i tre modelli, P.452 compreso, è mostrata come incertezza, come fa TropoLink. Solo confronto. Manuale delle previsioni, § 8 bis.", "ITU-R P.617-5 (median) with N0 and ΔN taken hourly from GFS profiles instead of the climatological maps, and the RSGB VHF/UHF Handbook formula (DL2FZN). The spread of the three models, P.452 included, is shown as uncertainty, as TropoLink does. Check only. Forecast manual, § 8 bis."),
        [L("Meteo lungo la tratta", "Weather along path")] = L("Subsidenza: aria che scende (velocità verticale GFS a 850 e 700 hPa sotto −1 cm/s) con inversione o forte calo di K, +1,5 dB (Moler e Holden, J. Res. NBS 64D, 1960); aria in salita oltre +5 cm/s, −2 dB. Stabilità di Misme: ΔW del primo km rispetto all'atmosfera standard (Bean e Dutton cap. 6), solo senza condotto. Scirocco: vento da SE–S a 850 hPa di almeno 7 m/s con T850 ≥ 16 °C sul Mediterraneo (ARRL Handbook § 19.4). Condotto morbido: strato che non intrappola la banda ma con λmax almeno un terzo della lunghezza d'onda (Kerr, Hepburn). Persistenza: ore delle 24 precedenti sopra 13 dB, fino a +2 dB (VHF/UHF DX Book; Biancomano e Leyshon). Manuale delle previsioni, § 9 bis e 10.", "Subsidence: sinking air (GFS vertical velocity at 850 and 700 hPa below −1 cm/s) with an inversion or a strong K drop, +1.5 dB (Moler & Holden, J. Res. NBS 64D, 1960); rising air above +5 cm/s, −2 dB. Misme stability: first-km ΔW against the standard atmosphere (Bean & Dutton ch. 6), only without a duct. Scirocco: SE–S wind at 850 hPa of at least 7 m/s with T850 ≥ 16 °C over the Mediterranean (ARRL Handbook § 19.4). Forecast manual, § 9 bis."),
        [L("Probabilità per periodo", "Probability per period")] = L("Probabilità che un periodo (una trasmissione FT8, un cambio in SSB) superi la soglia del modo, con l'evanescenza lenta come distribuzione normale: Φ(margine/σ). σ = 5 dB oltre 160 km (Bray, QST novembre 1961: ±7 dB oltre 100 miglia; ARRL Antenna Book; GEKO 69), 3 dB fra 50 e 160 km, 2 dB sotto. Margine 0 dB = 50%, +σ = 84%, +2σ = 98%. Non comprende l'incertezza del modello, che è più grande. Manuale delle previsioni, § 12.", "Probability that a period (one FT8 transmission, one SSB over) exceeds the mode threshold, with slow fading as a normal distribution: Φ(margin/σ). σ = 5 dB beyond 160 km (Bray, QST November 1961: ±7 dB beyond 100 miles; ARRL Antenna Book; GEKO 69), 3 dB between 50 and 160 km, 2 dB below. Margin 0 dB = 50%, +σ = 84%, +2σ = 98%. Model uncertainty, which is larger, is not included. Forecast manual, § 12."),
        [L("Via monte", "Via mountain")] = L("Con il corrispondente chiuso dietro l'orizzonte, si possono puntare le antenne su un rilievo vicino ben visibile: il versante illuminato diffonde il segnale. Tratta 1 stazione → rilievo nello spazio libero; tratta 2 dal rilievo al corrispondente, calcolata come se il rilievo trasmettesse (stessa previsione); conversione con l'equazione del radar bistatico, σ = σ0·A con σ0 = −15 dB e A l'area illuminata dal fascio (al massimo 20 km²). Si provano i tre rilievi più alti oltre 300 m entro 60 km, in azimut fino a ±120° dal corrispondente. Stima grossolana: ±10 dB. Manuale delle previsioni, § 10 bis.", "With the other station closed behind the horizon, the antennas can be aimed at a nearby, clearly visible mountain: the lit slope scatters the signal. Leg 1 station → mountain in free space; leg 2 from the mountain to the other station, computed as if the mountain transmitted (same forecast); conversion with the bistatic radar equation, σ = σ0·A with σ0 = −15 dB and A the area lit by the beam (at most 20 km²). The three highest peaks above 300 m within 60 km, up to ±120° from the other station, are tried. Rough estimate: ±10 dB. Forecast manual, § 10 bis."),
        [L("Ingresso nel condotto", "Duct entry")] = L("Quando il condotto che intrappola la banda comincia lontano, oltre l'orizzonte della stazione: perdita per portare il segnale fino al suo inizio, a metà del condotto, per diffrazione (spigolo principale del terreno SRTM1 con il parametro di Fresnel ν, ITU-R P.526, e terra sferica liscia: il peggiore dei due), più il percorso dentro il condotto (spazio libero, attenuazione residua γd di P.452 e accoppiamento del corrispondente). Se è più bassa della perdita prevista diventa il meccanismo. Manuale delle previsioni, § 10 ter.", "When the duct trapping the band starts far away, beyond the station horizon: loss to bring the signal to its start, at mid-duct, by diffraction (main SRTM1 terrain edge with Fresnel parameter ν, ITU-R P.526, and smooth spherical earth: the worse of the two), plus the path inside the duct (free space, P.452 residual attenuation γd and coupling of the other station). If lower than the predicted loss it becomes the mechanism. Forecast manual, § 10 ter."),
        [L("Modi digitali", "Digital modes")] = L("S/N in 2500 Hz confrontato con le soglie misurate: FT8 −21 dB (−18,6 dB con evanescenza, usata in troposcatter), FT4 −17,5 dB (Franke, Somerville e Taylor, QEX 2020), Q65-30A −24 dB, JT65A −25 dB (W3SZ 2019). ✓ sopra la soglia, ✗ sotto. Manuale delle previsioni, § 12.", "S/N in 2500 Hz against measured thresholds: FT8 −21 dB (−18.6 dB with fading, used for troposcatter), FT4 −17.5 dB (Franke, Somerville & Taylor, QEX 2020), Q65-30A −24 dB, JT65A −25 dB (W3SZ 2019). ✓ above, ✗ below. Forecast manual, § 12."),
        [L("Sorgente dei dati", "Data source")] = L("Modello e servizio che hanno dato i profili: GFS da Open-Meteo o da NOMADS (NOAA, file GRIB2), ICON-EU (DWD) o ECMWF IFS da Open-Meteo, oppure l'archivio delle previsioni per i casi storici. Manuale delle previsioni, § 3 bis.", "Model and service that supplied the profiles: GFS from Open-Meteo or NOMADS (NOAA, GRIB2 files), ICON-EU (DWD) or ECMWF IFS from Open-Meteo, or the forecast archive for historical cases. Forecast manual, § 3 bis."),
        [L("Volume comune", "Common volume")] = L("Base del volume di scatter: dove si incrociano i raggi radenti delle due stazioni, con gli angoli d'orizzonte dal profilo SRTM1 e il raggio equivalente k·6371 km. Con orizzonti piatti vale d²/(8·ae): circa 1,3 km a 300 km, 3,7 km a 500 km, 9,4 km a 800 km; più è alto, più l'aria è secca e rarefatta e lo scatter è debole. Accanto: rifrattività, gradiente e umidità del GFS a quella quota, e la distanza equivalente di Bray (QST, novembre 1961: ogni grado d'orizzonte vale circa 111 km). Manuale delle previsioni, § 8 bis.", "Base of the scatter volume: where the grazing rays of the two stations cross, using SRTM1 horizon angles and the effective radius k·6371 km. With flat horizons it is d²/(8·ae): about 1.3 km at 300 km, 3.7 km at 500 km, 9.4 km at 800 km; the higher, the drier and thinner the air and the weaker the scatter. Also: GFS refractivity, gradient and humidity at that height, and Bray's equivalent distance (QST, November 1961: each degree of horizon is worth about 111 km). Forecast manual, § 8 bis."),
        [L("Equazione parabolica", "Parabolic equation")] = L("Calcolo di campo con l'equazione parabolica (split-step di Fourier, come PETOOL e il framework wave-propagation di M. Lytaev, MIT): l'onda avanza lungo la tratta nel profilo di rifrattività modificata M del GFS, punto per punto, con i salti stimati dei condotti potenziali, sopra il terreno SRTM1 a gradini. Vede da sé condotti, condotti che perdono e diffrazione; il troposcatter si somma a parte. «Guadagno del profilo» = stessa tratta con atmosfera standard meno profilo previsto. Solo confronto. Manuale delle previsioni, § 11 bis.", "Field calculation with the parabolic equation (split-step Fourier, as in PETOOL and M. Lytaev's wave-propagation framework, MIT): the wave advances along the path through the GFS modified-refractivity profile M, point by point, with the estimated jumps of potential ducts, over stepped SRTM1 terrain. It captures ducts, leaky ducts and diffraction by itself; troposcatter is added separately. «Profile gain» = same path in a standard atmosphere minus the forecast profile. Check only. Forecast manual, § 11 bis."),
        [L("Senza tropo", "Without tropo")] = L("Somma delle potenze di diffrazione e troposcatter, mai sotto lo spazio libero: la perdita con atmosfera normale. Manuale delle previsioni, § 8.", "Power sum of diffraction and troposcatter, never below free space: loss in a normal atmosphere. Forecast manual, § 8."),
        [L("Strato fondante", "Governing layer")] = L("Lo strato di pressione che resta continuo più a lungo lungo la tratta, con condotti di almeno 40 m (Indermuehle e Suzuki, PASA 2026). Soglie per strato: Bean e Dutton, NBS Monograph 92, tab. 6.5. Manuale delle previsioni, § 9.", "The pressure layer that stays continuous longest along the path, with ducts at least 40 m thick (Indermuehle & Suzuki, PASA 2026). Layer thresholds: Bean & Dutton, NBS Monograph 92, table 6.5. Forecast manual, § 9."),
        [L("Continuità sulla tratta", "Continuity along path")] = L("Tratto più lungo in cui lo stesso strato è presente su punti GFS consecutivi; la frazione moltiplica il guadagno. Manuale delle previsioni, § 9.", "Longest stretch where the same layer is present at consecutive GFS points; the fraction multiplies the gain. Forecast manual, § 9."),
        [L("Terra coperta dallo strato", "Land covered by layer")] = L("Sul mare il condotto c'è quasi sempre: decide il tratto di terra (Indermuehle e Suzuki 2026, ricezioni AIS a 162 MHz: frazione di terra in condotto +67%). Se la terra supera il 10% si usa il minimo fra continuità e copertura della terra. Manuale delle previsioni, § 9.", "Over sea a duct is almost always there: the land part decides (Indermuehle & Suzuki 2026, AIS at 162 MHz: land duct fraction +67%). With more than 10% land the minimum of continuity and land coverage is used. Forecast manual, § 9."),
        [L("Quota della cima", "Top height")] = L("Cima e base dello strato lungo il tratto continuo, dai livelli GFS 1000–500 hPa con le quote ipsometriche (temperatura virtuale). Manuale delle previsioni, § 9.", "Layer top and base along the continuous stretch, from GFS 1000–500 hPa levels with hypsometric heights (virtual temperature). Forecast manual, § 9."),
        [L("Inclinazione", "Tilt")] = L("Pendenza della cima dello strato lungo la tratta, regressione lineare in m/km. Manuale delle previsioni, § 9.", "Slope of the layer top along the path, linear regression in m/km. Forecast manual, § 9."),
        [L("Deficit / λ massima", "Deficit / max λ")] = L("Deficit di M del condotto e lunghezza d'onda massima intrappolata, Kerr 1951 eq. 17 (λmax ≈ 2√2·(2/3)·√(ΔM·10⁻⁶)·d). Una banda resta nel condotto solo se λ ≤ λmax. Manuale delle previsioni, § 10.", "Duct M deficit and maximum trapped wavelength, Kerr 1951 eq. 17 (λmax ≈ 2√2·(2/3)·√(ΔM·10⁻⁶)·d). A band stays in the duct only if λ ≤ λmax. Forecast manual, § 10."),
        [L("Calcolo A (GFS, salto in 50 m)", "Calc A (GFS, 50 m jump)")] = L("Condotto potenziale: il calo di N oltre il normale fra due livelli GFS concentrato in 50 m, se l'aria è stabile (gradiente < 3 K/km) e più secca sopra (rapporto di umidità < 0,8). Manuale delle previsioni, § 10.", "Potential duct: the excess N drop between two GFS levels concentrated in 50 m, if the air is stable (lapse < 3 K/km) and drier above (humidity ratio < 0.8). Forecast manual, § 10."),
        [L("Calcolo B (P.453, spessore", "Calc B (P.453, thickness")] = L("Stesso salto distribuito sullo spessore climatologico dei condotti di ITU-R P.453 (mappe digitali a 1,5°, © ITU, uso personale e non commerciale). Manuale delle previsioni, § 10.", "Same jump spread over the ITU-R P.453 climatological duct thickness (1.5° digital maps, © ITU, personal non-commercial use). Forecast manual, § 10."),
        [L("Usato il peggiore", "Worst used")] = L("Per deficit e λmax si tiene il valore più basso fra A e B. Anomalia = deficit A / intensità tipica P.453 del punto. Manuale delle previsioni, § 10.", "For deficit and λmax the lower of A and B is kept. Anomaly = deficit A / typical P.453 strength at the point. Forecast manual, § 10."),
        [L("Terreno dentro lo strato", "Terrain inside layer")] = L("Chilometri in cui il terreno SRTM1 sale sopra la base dello strato: lì il condotto è interrotto o deviato. Manuale delle previsioni, § 9.", "Kilometres where SRTM1 terrain rises above the layer base: the duct is broken or deflected there. Forecast manual, § 9."),
        [L("Antenne e condotto", "Antennas vs duct")] = L("Posizione delle antenne rispetto al condotto: dentro 3 dB, fuori 3 dB più 1 dB ogni 10 m, al massimo 40 dB. Stima empirica dell'accoppiamento. Manuale delle previsioni, § 10.", "Antenna position relative to the duct: inside 3 dB, outside 3 dB plus 1 dB per 10 m, at most 40 dB. Empirical coupling estimate. Forecast manual, § 10."),
        [L("Attenuazione residua", "Residual attenuation")] = L("Attenuazione nel condotto γd = 5·10⁻⁵·ae·f^(1/3) dB/mrad sulla distanza angolare, ITU-R P.452-16 eq. 49. Manuale delle previsioni, § 10.", "In-duct attenuation γd = 5·10⁻⁵·ae·f^(1/3) dB/mrad over the angular distance, ITU-R P.452-16 eq. 49. Forecast manual, § 10."),
        [L("Guadagno tropo", "Tropo gain")] = L("Guadagno dello strato (10–25 dB secondo Bean e Dutton e il deficit del condotto) per la frazione usata; −10 dB con sub-rifrazione diffusa. Manuale delle previsioni, § 9.", "Layer gain (10–25 dB from Bean & Dutton and the duct deficit) times the fraction used; −10 dB with widespread sub-refraction. Forecast manual, § 9."),
        [L("Stima prudente / aggressiva", "Cautious / aggressive estimate")] = L("Prudente: soglia di super-rifrazione di Bean e Dutton (100 N/km nello strato 1000–850 hPa). Aggressiva: soglia classica di super-rifrazione (79 N/km, k ≈ 2). Con un condotto che intrappola la banda le due stime coincidono. Manuale delle previsioni, § 9 ter.", "Cautious: Bean and Dutton super-refraction threshold (100 N/km in the 1000–850 hPa layer). Aggressive: classic super-refraction threshold (79 N/km, k ≈ 2). With a duct trapping the band both are equal. Forecast manual, § 9 ter."),
        [L("Correzioni 1.3.2", "1.3.2 corrections")] = L("Peso del mare (μ1 di ITU-R P.452: più lungo il tratto continuo di terra, meno condotti; × 0,8–1,2), quota delle stazioni rispetto al condotto anche quando copre solo parte della tratta, cima dello strato limite del GFS sopra le creste (IT9GSF, +1,5 dB al massimo), alta pressione estesa con vento debole (+1 dB). Manuale delle previsioni, § 9 ter.", "Sea weight (ITU-R P.452 μ1: the longer the continuous land section, the fewer ducts; × 0.8–1.2), station height vs duct also when it covers only part of the path, GFS boundary-layer top above the ridges (IT9GSF, up to +1.5 dB), broad high pressure with light wind (+1 dB). Forecast manual, § 9 ter."),
        [L("Perdita prevista", "Predicted loss")] = L("Perdita senza tropo meno il guadagno tropo, oppure la perdita nel condotto se lo strato copre almeno l'80% della tratta; mai sotto lo spazio libero. Manuale delle previsioni, § 8–10.", "Loss without tropo minus tropo gain, or the in-duct loss if the layer covers at least 80% of the path; never below free space. Forecast manual, § 8–10."),
        [L("Confronto P.452 (p 20%)", "P.452 check (p 20%)")] = L("ITU-R P.452-16 § 4.5 (condotti e riflessione su strati) con il ΔN mediano del GFS e p = 20% come costante di calibrazione, come in Indermuehle e Suzuki 2026. Include Alf, l'accoppiamento empirico sotto 0,5 GHz. Solo confronto. Manuale delle previsioni, § 11.", "ITU-R P.452-16 § 4.5 (ducting and layer reflection) with GFS median ΔN and p = 20% as calibration constant, as in Indermuehle & Suzuki 2026. Includes Alf, the empirical coupling loss below 0.5 GHz. Check only. Forecast manual, § 11."),
        [L("Tempo di risposta", "Response time")] = L("Tempo dal clic al risultato, per tappe: profili GFS, terreno SRTM1, calcolo. Il dettaglio è nel registro alle righe TEMPI. Manuale delle previsioni, § 13.", "Time from click to result, by stage: GFS profiles, SRTM1 terrain, computation. Details in the log lines TEMPI. Forecast manual, § 13."),
        [L("EIRP", "EIRP")] = L("Potenza meno perdita del cavo più guadagno dell'antenna TX (Yagi DL6WU: circa 8,6 + 8 log(boom/λ) dBd, dalle tabelle di DL6WU). Manuale delle previsioni, § 12.", "Power minus cable loss plus TX antenna gain (DL6WU Yagi: about 8.6 + 8 log(boom/λ) dBd, from DL6WU tables). Forecast manual, § 12."),
        [L("Antenne TX / RX", "Antennas TX / RX")] = L("Guadagno delle Yagi DL6WU dalla lunghezza del boom (in dBi = dBd + 2,15). È una stima prudente: le Yagi ottimizzate misurate dal NBS (Viezbicke, TN 688, 1976) danno 1–1,7 dB in più fra 0,8 e 4,2 λ di boom. Manuale delle previsioni, § 12.", "DL6WU Yagi gain from boom length (dBi = dBd + 2.15). A conservative estimate: optimised Yagis measured by NBS (Viezbicke, TN 688, 1976) give 1–1.7 dB more between 0.8 and 4.2 λ of boom. Forecast manual, § 12."),
        [L("Segnale ricevuto", "Received signal")] = L("EIRP − perdita prevista + guadagno RX − cavo RX, all'ingresso del ricevitore. S-meter IARU VHF/UHF: S9 = −93 dBm, 6 dB per unità (IARU R1, raccomandazione sugli S-meter). Manuale delle previsioni, § 12.", "EIRP − predicted loss + RX gain − RX cable, at the receiver input. IARU VHF/UHF S-meter: S9 = −93 dBm, 6 dB per unit (IARU R1 S-meter recommendation). Forecast manual, § 12."),
        [L("Rumore (P.372)", "Noise (P.372)")] = L("Rumore esterno ITU-R P.372: galattico Fa = 52 − 23 log f, artificiale Fa = c − d log f secondo l'ambiente; Tsys = Ta + 290(Lc−1) + 290(F−1)Lc ai morsetti d'antenna. Manuale delle previsioni, § 12.", "External noise ITU-R P.372: galactic Fa = 52 − 23 log f, man-made Fa = c − d log f by environment; Tsys = Ta + 290(Lc−1) + 290(F−1)Lc at the antenna terminals. Forecast manual, § 12."),
        [L("Rumore in SSB", "SSB noise")] = L("k·Tsys·B nella banda SSB impostata (k = 1,38·10⁻²³ J/K). Manuale delle previsioni, § 12.", "k·Tsys·B in the set SSB bandwidth (k = 1.38·10⁻²³ J/K). Forecast manual, § 12."),
        ["S/N"] = L("Rapporto segnale/rumore ai morsetti d'antenna nella banda di ogni modo, confrontato con la soglia impostata: verde sopra, rosso sotto. Soglie tipiche: SSB 6 dB, CW 0 dB in 500 Hz, FT8 −21 dB in 2500 Hz (WSJT-X). Manuale delle previsioni, § 12.",
                    "Signal-to-noise at the antenna terminals in each mode's bandwidth, against the set threshold: green above, red below. Typical: SSB 6 dB, CW 0 dB in 500 Hz, FT8 −21 dB in 2500 Hz (WSJT-X). Forecast manual, § 12."),
    };

    /// <summary>Intestazione dei risultati.</summary>
    public static string Header(LinkResult r, DateTime t) => L("Risultato", "Result") + $" · {r.FreqMHz:0.#} MHz · {t:dd/MM HH}:00 UTC";

    /// <summary>Riga della sorgente dei dati (da mettere in testa ai risultati).</summary>
    public static (string Label, string Value) SourceLine(OwnForecast own) =>
        (L("Sorgente dei dati", "Data source"), own == null ? "—" :
            (own.Hist != null ? L("archivio Open-Meteo, caso ", "Open-Meteo archive, case ") + own.Hist.Name + " · " : "") + WxSources.Name(own.UsedSource) + (own.UsedDetail != "" ? $" ({own.UsedDetail})" : ""));

    /// <summary>Righe dei risultati (etichetta, valore, tono, grassetto) uguali per tutte le finestre; le note hanno l'etichetta vuota.</summary>
    public static List<(string Label, string Value, FcTone Tone, bool Bold)> Lines(LinkResult r, double reqSsb, double reqCw, double reqFt8, OwnForecast own = null)
    {
        var res = new List<(string, string, FcTone, bool)>();
        void Line(string a, string b, FcTone c = FcTone.Normal, bool bold = false) => res.Add((a, b, c, bold));
        void Note(string n) => res.Add(("", n, FcTone.Muted, false));
        if (own != null && r.ForecastUsed) { var (sl, sv) = SourceLine(own); Line(sl, sv, own.Hist != null ? FcTone.Accent : FcTone.Normal); }
        Line(L("Distanza / azimut", "Distance / azimuth"), $"{r.DistKm:0.0} km / {r.AzDeg:0}°");
        Line(L("Antenne sul mare TX / RX", "Antennas above sea TX / RX"), $"{r.HtxAsl:0} m / {r.HrxAsl:0} m");
        Line(L("Raggio equivalente k", "Effective radius k"), $"{r.K:0.00}" + (r.ForecastUsed ? " (GFS)" : " (4/3)"));
        Line(L("In vista ottica", "Line of sight"), r.Los ? L("sì", "yes") : L("no (ostacolato)", "no (obstructed)"));
        Line(L("1ª zona di Fresnel", "1st Fresnel zone"), double.IsNaN(r.FresnelMinRatio) ? "—" : (r.FresnelMinRatio >= 0.6 ? L("libera", "clear") : L("invasa", "obstructed")) + $" ({L("minimo", "min")} {r.FresnelMinRatio:0.0} F1)");
        foreach (var ob in r.Obstacles) Line("  " + L("ostacolo", "obstacle") + $" {ob.km:0.0} km", $"{ob.hM:0} m · {ob.ratio:0.0} F1 · ν {ob.nu:0.0}");
        Line(L("Spazio libero", "Free space"), $"{r.LfsDb:0.0} dB");
        Line(L("Diffrazione (Bullington)", "Diffraction (Bullington)"), $"{r.LdiffDb:0.0} dB (+{r.LbullDb:0.0})");
        Line(L("Troposcatter (P.452)", "Troposcatter (P.452)"), $"{r.LscatDb:0.0} dB (θ {r.ThetaMrad:0.0} mrad)");
        if (!double.IsNaN(r.RsgbDb))
            Line(L("Troposcatter, altri modelli", "Troposcatter, other models"), (double.IsNaN(r.P617Db) ? "" : $"P.617 {r.P617Db:0.0} · ") + $"RSGB {r.RsgbDb:0.0} dB · " + L("incertezza", "uncertainty") + $" ±{r.ScatSpreadDb / 2:0.0} dB");
        if (!double.IsNaN(r.CvHeightM))
            Line(L("Volume comune", "Common volume"), $"{r.CvHeightM / 1000:0.0} km " + L("di quota", "high") + $", {r.CvKm:0} km " + L("dal TX", "from TX") +
                 (double.IsNaN(r.CvGrad) ? "" : $" · N {r.CvN:0}, −dN/dh {r.CvGrad:0} N/km, UR {r.CvRH:0}%") + $" · d eq. {r.DeqKm:0} km");
        Line(L("Senza tropo", "Without tropo"), $"{r.LbaseDb:0.0} dB");
        if (r.ForecastUsed)
        {
            if (r.LayerName != "")
            {
                Line(L("Strato fondante", "Governing layer"), $"{r.LayerName} · " + (r.LayerTraps ? L("condotto", "duct") : L("super-rifrattivo", "super-refractive")), FcTone.Strong, true);
                Line(L("Continuità sulla tratta", "Continuity along path"), $"{r.LayerFromKm:0}–{r.LayerToKm:0} km ({r.LayerFrac * 100:0}%)");
                if (!double.IsNaN(r.LandCov)) Line(L("Terra coperta dallo strato", "Land covered by layer"), $"{r.LandInDuctKm:0} / {r.LandKm:0} km ({r.LandCov * 100:0}%) · " + L("frazione usata", "fraction used") + $" {r.FracUsed * 100:0}%");
                Line(L("Quota della cima", "Top height"), $"{r.TopMean:0} m ({r.TopMin:0}–{r.TopMax:0}), " + L("base", "base") + $" {r.BaseMean:0} m");
                Line(L("Inclinazione", "Tilt"), $"{r.TiltMkm:+0.0;-0.0} m/km");
                if (r.LayerTraps) Line(L("Deficit / λ massima", "Deficit / max λ"), $"{r.DeficitMean:0.0} M / {r.LambdaMin:0.00} m");
                if (r.DualN > 0)
                {
                    Line(L("Calcolo A (GFS, salto in 50 m)", "Calc A (GFS, 50 m jump)"), $"{r.DeficitMeanA:0.0} M / {r.LambdaMinA:0.00} m");
                    Line(L("Calcolo B (P.453, spessore", "Calc B (P.453, thickness") + $" {r.ClimThickMean:0} m)", $"{r.DeficitMeanB:0.0} M / {r.LambdaMinB:0.00} m");
                    Line(L("Usato il peggiore", "Worst used"), (r.DualChosenB * 2 >= r.DualN ? "B" : "A") + $" ({r.DualChosenB}/{r.DualN} " + L("punti B", "points B") + ") · " + L("anomalia", "anomaly") + $" {r.AnomalyMean:0.0}×");
                }
                if (r.TerrainInDuctKm > 0) Line(L("Terreno dentro lo strato", "Terrain inside layer"), $"{r.TerrainInDuctKm:0} km");
                if (r.TxVsDuct != "") Line(L("Antenne e condotto", "Antennas vs duct"), $"TX {r.TxVsDuct}, RX {r.RxVsDuct}");
                if (r.ResidualDb > 0) Line(L("Attenuazione residua", "Residual attenuation"), $"{r.ResidualDb:0.0} dB (+ {r.CouplingTxDb + r.CouplingRxDb:0.0} dB " + L("accoppiamento", "coupling") + ")");
            }
            else Line(L("Strato fondante", "Governing layer"), L("nessuno strato continuo", "no continuous layer"));
            Line(L("Guadagno tropo", "Tropo gain"), $"{r.TropoGainDb:+0.0;-0.0} dB");
            var wx = new List<string>();
            if (r.SubsFrac > 0) wx.Add(L("subsidenza", "subsidence") + $" {r.SubsFrac * 100:0}%");
            if (r.AscentFrac > 0) wx.Add(L("aria in salita", "rising air") + $" {r.AscentFrac * 100:0}%");
            if (r.SciroccoFrac > 0) wx.Add(L("scirocco", "scirocco") + $" {r.SciroccoFrac * 100:0}%");
            wx.Add("Misme " + $"{r.StabDbMean:+0.0;-0.0} dB" + (Refractivity.UseCorrections ? "" : L(" (spento)", " (off)")));
            if (r.SoftN > 0) wx.Add(L("condotto morbido", "soft duct") + $" {r.SoftN} " + L("punti", "pts") + $" ({r.SoftMax:0.00})");
            if (!double.IsNaN(r.PersistFrac)) wx.Add(L("persistenza", "persistence") + $" {r.PersistFrac * 100:0}%");
            Line(L("Meteo lungo la tratta", "Weather along path"), string.Join(" · ", wx), r.SciroccoFrac > 0 || r.SubsFrac > 0.3 ? FcTone.Accent : FcTone.Normal);
        }
        Line(L("Perdita prevista", "Predicted loss"), $"{r.LpredDb:0.0} dB · {r.Mechanism}", FcTone.Strong, true);
        if (!double.IsNaN(r.LentryDb))
            Line(L("Ingresso nel condotto", "Duct entry"), L($"inizio a {r.EntryKm:0} km, {r.EntryHeightM:0} m · diffrazione {r.EntryDiffDb:0.0} dB (ν {r.EntryNu:0.0} a {r.EntryNuKm:0} km) · totale {r.LentryDb:0.0} dB",
                                                              $"starts at {r.EntryKm:0} km, {r.EntryHeightM:0} m · diffraction {r.EntryDiffDb:0.0} dB (ν {r.EntryNu:0.0} at {r.EntryNuKm:0} km) · total {r.LentryDb:0.0} dB"));
        if (!double.IsNaN(r.LpredAggrDb))
            Line(L("Stima prudente / aggressiva", "Cautious / aggressive estimate"), $"{r.LpredDb:0.0} / {r.LpredAggrDb:0.0} dB" + (r.LpredAggrDb < r.LpredDb - 0.05 ? $" ({r.LpredAggrDb - r.LpredDb:+0.0;-0.0})" : " " + L("(uguali)", "(same)")));
        var extra = new List<string>();
        if (!double.IsNaN(r.Mu1)) extra.Add(L($"mare μ1 {r.Mu1:0.00} (× {r.SeaWeight:0.00})", $"sea μ1 {r.Mu1:0.00} (× {r.SeaWeight:0.00})"));
        if (!double.IsNaN(r.DuctCouplingF)) extra.Add(L($"stazioni e condotto × {r.DuctCouplingF:0.00}", $"stations vs duct × {r.DuctCouplingF:0.00}"));
        if (r.RidgeN > 0 && r.RidgeMaxM >= 500) extra.Add(L($"strato limite sopra {r.RidgeCovered}/{r.RidgeN} creste (+{r.BlhRidgeDb:0.0} dB)", $"boundary layer above {r.RidgeCovered}/{r.RidgeN} ridges (+{r.BlhRidgeDb:0.0} dB)"));
        if (r.EvapDuctM > 0) extra.Add(L($"condotto d'evaporazione {r.EvapDuctM:0} m", $"evaporation duct {r.EvapDuctM:0} m"));
        if (r.DayMixFrac >= 0.3) extra.Add(L("giorno: rimescolamento su terra", "day: mixing over land"));
        if (r.NightRadFrac >= 0.3) extra.Add(L("notte: condotti da irraggiamento possibili", "night: radiation ducts possible"));
        if (r.HighPFrac > 0) extra.Add(L($"alta pressione estesa {r.HighPFrac * 100:0}%", $"broad high pressure {r.HighPFrac * 100:0}%"));
        if (extra.Count > 0) Line(L("Correzioni 1.3.2", "1.3.2 corrections"), string.Join(" · ", extra));
        if (r.P452Ok) Line(L("Confronto P.452 (p 20%)", "P.452 check (p 20%)"), $"{r.P452Lb:0.0} dB ({r.P452Lb - r.LpredDb:+0.0;-0.0}) · ΔN {r.P452DeltaN:0} · Lba {r.P452Lba:0.0} dB" + (r.P452Alf > 0 ? $" (Alf {r.P452Alf:0.0})" : ""));
        if (!double.IsNaN(r.PeLossDb))
            Line(L("Equazione parabolica", "Parabolic equation"), $"{r.PeTotalDb:0.0} dB ({r.PeTotalDb - r.LpredDb:+0.0;-0.0}) · " + L("guadagno del profilo", "profile gain") + $" {r.PeGainDb:+0.0;-0.0} dB · " + L("solo campo", "field only") + $" {r.PeLossDb:0.0} dB");
        {
            var tot = r.Times.Laps.FirstOrDefault(x => x.Name == "TOTALE").Ms;
            var parts = r.Times.Laps.Where(x => x.Name != "TOTALE" && x.Ms >= 50).Select(x => $"{x.Name} {CalcLog.Dur(x.Ms)}");
            Line(L("Tempo di risposta", "Response time"), CalcLog.Dur(tot) + (parts.Any() ? " · " + string.Join(" · ", parts) : ""));
        }
        Line(L("EIRP", "EIRP"), $"{r.EirpDbm:0.0} dBm ({Math.Pow(10, (r.EirpDbm - 30) / 10):0} W)");
        Line(L("Antenne TX / RX", "Antennas TX / RX"), $"{r.GtxDbi:0.0} / {r.GrxDbi:0.0} dBi");
        Line(L("Segnale ricevuto", "Received signal"), $"{r.PrxDbm:0.0} dBm · {LinkResult.SMeter(r.PrxDbm)}", FcTone.Accent, true);
        Line(L("Rumore (P.372)", "Noise (P.372)"), (r.TgalK > 0 ? $"{L("gal.", "gal.")} {r.TgalK:0} + {L("art.", "m-m")} {r.TmanK:0} = " : "") + $"Ta {r.TaK:0} K · Tsys {r.TsysK:0} K");
        Line(L("Rumore in SSB", "SSB noise"), $"{r.NoiseSsbDbm:0.0} dBm (" + L("ai morsetti d'antenna", "at antenna terminals") + ")");
        FcTone V(double snr, double req) => snr >= req ? FcTone.Ok : snr >= req - 6 ? FcTone.Warn : FcTone.Bad;
        Line("S/N SSB", $"{r.SnrSsb:+0.0;-0.0} dB", V(r.SnrSsb, reqSsb), true);
        Line("S/N CW", $"{r.SnrCw:+0.0;-0.0} dB", V(r.SnrCw, reqCw), true);
        bool scatter = r.Mechanism == "troposcatter";
        double ft8 = scatter ? Math.Max(reqFt8, -18.6) : reqFt8;
        Line("S/N FT8", $"{r.SnrFt8:+0.0;-0.0} dB" + (scatter && ft8 > reqFt8 ? L(" (soglia −18,6 con evanescenza)", " (−18.6 threshold with fading)") : ""), V(r.SnrFt8, ft8), true);
        {
            string M(string n, double thr) => $"{n} {(r.SnrFt8 >= thr ? "✓" : "✗")}";
            bool any = r.SnrFt8 >= -25;
            Line(L("Modi digitali", "Digital modes"), string.Join(" · ", M("FT4", -17.5), M("FT8", ft8), M("Q65", -24), M("JT65", -25)), any ? FcTone.Ok : FcTone.Bad);
        }
        {
            // evanescenza (Bray, QST nov. 1961; ARRL Antenna Book; GEKO 69): oltre 100 miglia le mediane orarie oscillano di circa ±7 dB.
            // σ: 5 dB oltre 160 km, 3 dB fra 50 e 160 km, 2 dB sotto; probabilità che un periodo superi la soglia = Φ(margine/σ)
            double d = r.DistKm, sg = d >= 160 ? 5 : d >= 50 ? 3 : 2;
            static double Phi(double x) { double t = 1 / (1 + 0.2316419 * Math.Abs(x)); double y = 1 - 0.3989423 * Math.Exp(-x * x / 2) * t * (0.3193815 + t * (-0.3565638 + t * (1.781478 + t * (-1.821256 + t * 1.330274)))); return x >= 0 ? y : 1 - y; }
            string P(string n, double snr, double thr) => $"{n} {100 * Phi((snr - thr) / sg):0}%";
            Line(L("Probabilità per periodo", "Probability per period"), string.Join(" · ", P("SSB", r.SnrSsb, reqSsb), P("CW", r.SnrCw, reqCw), P("FT4", r.SnrFt8, -17.5), P("FT8", r.SnrFt8, ft8), P("Q65", r.SnrFt8, -24), P("JT65", r.SnrFt8, -25)) + $" (σ {sg:0} dB)");
        }
        if (r.Via.Count > 0)
        {
            var bestVia = r.Via.OrderBy(v => v.TotalDb).First();
            foreach (var v in r.Via.OrderBy(v => v.TotalDb))
                Line(L("Via monte", "Via mountain"), L($"azimut {v.AzDeg:0}°, {v.DistKm:0.0} km, {v.HeightM:0} m → {v.TotalDb:0.0} dB ({v.TotalDb - r.LpredDb:+0.0;-0.0} sul diretto)",
                                                       $"azimuth {v.AzDeg:0}°, {v.DistKm:0.0} km, {v.HeightM:0} m → {v.TotalDb:0.0} dB ({v.TotalDb - r.LpredDb:+0.0;-0.0} vs direct)"),
                     v == bestVia && v.TotalDb < r.LpredDb ? FcTone.Ok : FcTone.Normal);
        }
        foreach (var n in r.Notes) Note("• " + n);
        return res;
    }

    /// <summary>Fumetto per un'etichetta (o null).</summary>
    public static string Tip(string label) => TipsDict.FirstOrDefault(kv => label.TrimStart().StartsWith(kv.Key)).Value;
}

/// <summary>Disegno della previsione propria sulla mappa: colori fissi, tratteggio 3–7 dB, zona coperta, copertura in ottica, percorso, linee opzionali.</summary>
public sealed class ForecastPainter
{
    public AppState St; public OwnForecast Own; public MapView Map; public Palette Th;
    public Func<DateTime> Time; public Func<double> Freq;
    public Func<string> TxLoc, RxLoc;
    public Func<double> ReqSsb, ReqCw, ReqFt8;
    public LinkResult Last;
    public (double[] az, double[] first, double[] far, LatLon origin)? Los;
    /// <summary>Testo dell'accordo con pressione e inversione, da mostrare fuori dal disegno.</summary>
    public event Action<string> CorrText;
    string _corrKey = "";
    static string L(string it, string en) => Lang.Current == "it" ? it : en;

    /// <summary>Campo campionato sulla zona visibile, levigato con due passate di media 3×3 (i buchi restano buchi).</summary>
    public static double[,] Sample(Func<double, double, double> f, double lat0, double lon0, double step, int nr, int nc, double fill)
    {
        var g = new double[nr, nc];
        for (int r = 0; r < nr; r++) for (int c = 0; c < nc; c++) { double v = f(lat0 + r * step, lon0 + c * step); g[r, c] = double.IsNaN(v) ? fill : v; }
        for (int pass = 0; pass < 2; pass++)
        {
            var o = new double[nr, nc];
            for (int r = 0; r < nr; r++)
                for (int c = 0; c < nc; c++)
                {
                    double sum = 0; int n = 0;
                    for (int dr = -1; dr <= 1; dr++) for (int dc = -1; dc <= 1; dc++)
                    {
                        int rr = r + dr, cc = c + dc;
                        if (rr < 0 || cc < 0 || rr >= nr || cc >= nc || double.IsNaN(g[rr, cc])) continue;
                        sum += g[rr, cc]; n++;
                    }
                    o[r, c] = double.IsNaN(g[r, c]) || n == 0 ? double.NaN : sum / n;
                }
            g = o;
        }
        return g;
    }

    /// <summary>Curve di livello con i quadrati marcianti (interpolazione lineare sui lati), con etichette distanziate.</summary>
    void Contour(DrawingContext ctx, double[,] g, double lat0, double lon0, double step, double level, IPen pen, string label, Color labCol, List<Point> placed)
    {
        var m = Map; int nr = g.GetLength(0), nc = g.GetLength(1);
        Point P(double r, double c) { m.Screen(new LatLon(lat0 + r * step, lon0 + c * step), out var s); return s; }
        for (int r = 0; r < nr - 1; r++)
            for (int c = 0; c < nc - 1; c++)
            {
                double a = g[r, c], b = g[r, c + 1], d = g[r + 1, c], e = g[r + 1, c + 1];
                if (double.IsNaN(a) || double.IsNaN(b) || double.IsNaN(d) || double.IsNaN(e)) continue;
                var pts = new List<Point>(4);
                void Edge(double v1, double v2, double r1, double c1, double r2, double c2)
                {
                    if ((v1 < level) == (v2 < level)) return;
                    double k = (level - v1) / (v2 - v1);
                    pts.Add(P(r1 + (r2 - r1) * k, c1 + (c2 - c1) * k));
                }
                Edge(a, b, r, c, r, c + 1); Edge(b, e, r, c + 1, r + 1, c + 1); Edge(e, d, r + 1, c + 1, r + 1, c); Edge(d, a, r + 1, c, r, c);
                if (pts.Count >= 2) ctx.DrawLine(pen, pts[0], pts[1]);
                if (pts.Count == 4) ctx.DrawLine(pen, pts[2], pts[3]);
                if (pts.Count >= 2 && label != null)
                {
                    var mid = new Point((pts[0].X + pts[1].X) / 2, (pts[0].Y + pts[1].Y) / 2);
                    if (mid.X > 30 && mid.Y > 30 && mid.X < m.Bounds.Width - 30 && mid.Y < m.Bounds.Height - 30 &&
                        placed.All(q => Math.Abs(q.X - mid.X) + Math.Abs(q.Y - mid.Y) > 260))
                    {
                        placed.Add(mid);
                        var ft = new FormattedText(label, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, new Typeface(FontFamily.Default, FontStyle.Normal, FontWeight.Bold), 11, new SolidColorBrush(labCol));
                        var rc = new Rect(mid.X - ft.Width / 2 - 4, mid.Y - ft.Height / 2 - 1, ft.Width + 8, ft.Height + 2);
                        ctx.DrawRectangle(new SolidColorBrush(Th.Panel), new Pen(new SolidColorBrush(labCol), 1), rc, 4, 4);
                        ctx.DrawText(ft, new Point(rc.X + 4, rc.Y + 1));
                    }
                }
            }
    }

    public void DrawOverlay(DrawingContext ctx)
    {
        var m = Map;
        if (m == null) return;
        var (la1, la2, lo1, lo2) = m.Box;
        double span = Math.Max(la2 - la1, lo2 - lo1);
        double step = Math.Clamp(span / 110, 0.05, 1.0);
        double lat0 = Math.Floor(la1 / step) * step - step, lon0 = Math.Floor(lo1 / step) * step - step;
        int nr = (int)Math.Ceiling((la2 - lat0) / step) + 2, nc = (int)Math.Ceiling((lo2 - lon0) / step) + 2;
        if (nr * nc > 40000) return;
        var t = Time(); double f = Freq();
        var placed = new List<Point>();
        if (St.S.GetBool("fc_ov_curves", false) && Own.HasData)
        {
            var g = Sample((la, lo) => { double v = Own.Index(la, lo, t, f); return double.IsNaN(v) ? double.NaN : v * 2.5; }, lat0, lon0, step, nr, nc, double.NaN);
            foreach (var bnd in Palette.GainBands)
            {
                var col = Palette.GainLineColor(bnd.Lo);
                Contour(ctx, g, lat0, lon0, step, bnd.Lo, new Pen(new SolidColorBrush(col), 2.2, lineCap: PenLineCap.Round), $"{bnd.Lo:0.#} dB", col, placed);
            }
        }
        if (St.S.GetBool("fc_ov_inv", false))
        {
            var g = Sample((la, lo) => St.Pressure.InversionAt(new LatLon(la, lo), t), lat0, lon0, step, nr, nc, double.NaN);
            var col = Color.Parse("#C0392B");
            foreach (var lv in new[] { 1.0, 3.0, 5.0, 8.0 })
                Contour(ctx, g, lat0, lon0, step, lv, new Pen(new SolidColorBrush(col), lv >= 5 ? 2.0 : 1.4, new DashStyle(new double[] { 5, 3 }, 0)), $"+{lv:0} °C", col, placed);
        }
        if (St.S.GetBool("fc_ov_wx", false) && Own.HasData) DrawWeatherMarks(ctx, t);
        Correlation(lat0, lon0, step, nr, nc, t, f);
    }

    /// <summary>Segni meteo sulle celle della griglia: S scirocco, ↓ subsidenza, ↑ aria in salita (solo se le celle sono abbastanza larghe).</summary>
    void DrawWeatherMarks(DrawingContext ctx, DateTime t)
    {
        var cells = Own.Cells(t);
        if (cells.Count == 0) return;
        Map.Screen(new LatLon(cells[0].lat, cells[0].lon), out var p0);
        Map.Screen(new LatLon(cells[0].lat, cells[0].lon + Own.Step), out var p1);
        if (Math.Abs(p1.X - p0.X) < 16) return;
        var tf = new Typeface(FontFamily.Default, FontStyle.Normal, FontWeight.Bold);
        var halo = new SolidColorBrush(Th.Panel);
        foreach (var (la, lo, pr) in cells)
        {
            string txt = pr.Scirocco ? "S" : pr.Subsidence ? "↓" : pr.Ascent ? "↑" : null;
            if (txt == null) continue;
            var col = pr.Scirocco ? Color.Parse("#D35400") : pr.Subsidence ? Color.Parse("#1F6FB2") : Color.Parse("#7F8C8D");
            Map.Screen(new LatLon(la, lo), out var sp);
            if (sp.X < 0 || sp.Y < 0 || sp.X > Map.Bounds.Width || sp.Y > Map.Bounds.Height) continue;
            var ft = new FormattedText(txt, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, tf, 12, new SolidColorBrush(col));
            var rc = new Rect(sp.X - ft.Width / 2 - 2, sp.Y - ft.Height / 2, ft.Width + 4, ft.Height);
            ctx.DrawRectangle(halo, null, rc, 3, 3);
            ctx.DrawText(ft, new Point(rc.X + 2, rc.Y));
        }
    }

    /// <summary>Accordo fra il guadagno stimato e la pressione / l'inversione sulla zona visibile (Pearson), nel log una volta per ora e banda.</summary>
    void Correlation(double lat0, double lon0, double step, int nr, int nc, DateTime t, double f)
    {
        if (!Own.HasData) return;
        string key = $"{t:yyyyMMddHH}|{f}|{lat0:0.0}|{lon0:0.0}|{step:0.00}";
        if (key == _corrKey) return;
        _corrKey = key;
        var gp = new List<(double g, double p)>(); var gi = new List<(double g, double i)>();
        int sk = Math.Max(1, (int)Math.Round(0.5 / step));          // circa mezzo grado
        for (int r = 0; r < nr; r += sk)
            for (int c = 0; c < nc; c += sk)
            {
                double la = lat0 + r * step, lo = lon0 + c * step;
                double v = Own.Index(la, lo, t, f); if (double.IsNaN(v)) continue;
                double p = St.Pressure.ValueAt(new LatLon(la, lo), t), inv = St.Pressure.InversionAt(new LatLon(la, lo), t);
                if (!double.IsNaN(p)) gp.Add((v * 2.5, p));
                if (!double.IsNaN(inv)) gi.Add((v * 2.5, inv));
            }
        static double R(List<(double, double)> xs)
        {
            if (xs.Count < 10) return double.NaN;
            double mx = xs.Average(x => x.Item1), my = xs.Average(x => x.Item2), sxy = 0, sxx = 0, syy = 0;
            foreach (var (x, y) in xs) { sxy += (x - mx) * (y - my); sxx += (x - mx) * (x - mx); syy += (y - my) * (y - my); }
            return sxx > 0 && syy > 0 ? sxy / Math.Sqrt(sxx * syy) : double.NaN;
        }
        double rp = R(gp.Select(x => (x.g, x.p)).ToList()), ri = R(gi.Select(x => (x.g, x.i)).ToList());
        string F(double r) => double.IsNaN(r) ? "—" : r.ToString("+0.00;−0.00", CultureInfo.InvariantCulture);
        // siamo dentro il disegno della mappa: l'interfaccia cambia il testo dopo, fuori dal passaggio di rendering
        var corrText = L($"Accordo del guadagno con la pressione {F(rp)}, con l'inversione {F(ri)} ({gp.Count} punti, {t:dd/MM HH}:00 UTC, {f:0} MHz)",
                         $"Gain agreement with pressure {F(rp)}, with inversion {F(ri)} ({gp.Count} points, {t:dd/MM HH}:00 UTC, {f:0} MHz)");
        CorrText?.Invoke(corrText);
        CalcLog.W($"accordo sulla zona visibile, {t:dd/MM HH}:00 UTC, {f:0} MHz: guadagno–pressione r = {F(rp)} ({gp.Count} punti), guadagno–inversione r = {F(ri)} ({gi.Count} punti)");
    }

    public void DrawLayer(DrawingContext ctx)
    {
        var m = Map;
        if (m == null) return;
        var (la1, la2, lo1, lo2) = m.Box;
        double span = Math.Max(la2 - la1, lo2 - lo1);
        double step = Math.Max(0.1, Math.Min(2.0, Math.Round(span / 90 * 20) / 20));
        var t = Time(); double f = Freq();
        double lat0 = Math.Floor(la1 / step) * step, lon0 = Math.Floor(lo1 / step) * step;
        bool low = St.S.GetBool("fc_ov_low", false);
        var hatchPen = new Pen(new SolidColorBrush(Color.FromArgb(150, 0x6C, 0x7A, 0x89)), 1);
        double maxDb = double.NaN;
        for (double lat = lat0; lat < la2; lat += step)
        {
            if (lat < -85 || lat + step > 85) continue;
            for (double lon = lon0; lon < lo2; lon += step)
            {
                if (!m.Screen(new LatLon(lat, lon), out var p1) || !m.Screen(new LatLon(lat + step, lon + step), out var p3)) continue;
                var rect = new Rect(new Point(Math.Min(p1.X, p3.X), Math.Min(p1.Y, p3.Y)), new Point(Math.Max(p1.X, p3.X), Math.Max(p1.Y, p3.Y)));
                if (rect.Right < 0 || rect.Left > m.Bounds.Width || rect.Bottom < 0 || rect.Top > m.Bounds.Height) continue;
                double v = Own.Index(lat + step / 2, lon + step / 2, t, f);
                if (!double.IsNaN(v))
                {
                    // scala fissa in dB (uguale con tutti i temi): sotto 7 dB nessun colore
                    double db = v * 2.5;
                    if (double.IsNaN(maxDb) || db > maxDb) maxDb = db;
                    if (Palette.GainColor(db) is Color c)
                        ctx.DrawRectangle(new SolidColorBrush(Color.FromArgb((byte)(255 * Palette.GainAlpha), c.R, c.G, c.B)), null, rect);
                    else if (low && db >= 3)
                    {
                        // tratteggio leggero fra 3 e 7 dB: qualcosa si forma, ma resta sotto la scala
                        using (ctx.PushClip(rect))
                            for (double k = -rect.Height; k < rect.Width; k += 6)
                                ctx.DrawLine(hatchPen, new Point(rect.X + k, rect.Bottom), new Point(rect.X + k + rect.Height, rect.Y));
                    }
                    continue;
                }
                double hv = St.Tropo.Value("media", lat + step / 2, lon + step / 2, t);
                if (double.IsNaN(hv) || hv < 5) continue;
                // quadretti: 4×4 caselle alternate col colore della fonte esterna
                var hc = Palette.IndexColor(hv);
                var br = new SolidColorBrush(Color.FromArgb(150, hc.R, hc.G, hc.B));
                double w = rect.Width / 4, h = rect.Height / 4;
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (((i + j) & 1) == 0) ctx.DrawRectangle(br, null, new Rect(rect.X + i * w, rect.Y + j * h, w, h));
            }
        }

        // zona coperta dalla previsione propria, con il massimo visibile: una mappa vuota "perché va tutto normale"
        // si distingue da una vuota "perché mancano i dati"
        if (Own.HasData && m.Screen(new LatLon(Own.LatMin, Own.LonMin), out var c1) && m.Screen(new LatLon(Own.LatMax, Own.LonMax), out var c2))
        {
            var box = new Rect(new Point(Math.Min(c1.X, c2.X), Math.Min(c1.Y, c2.Y)), new Point(Math.Max(c1.X, c2.X), Math.Max(c1.Y, c2.Y)));
            var bc = Th.Dark ? Color.Parse("#C8D2DC") : Color.Parse("#3E4A57");
            ctx.DrawRectangle(null, new Pen(new SolidColorBrush(bc), 1.4, new DashStyle(new double[] { 6, 4 }, 0)), box);
            string txt = L("previsione propria", "own forecast") + (double.IsNaN(maxDb) ? "" : $" · {L("massimo visibile", "max visible")} {maxDb:0.0} dB") +
                         $" · {f:0} MHz · {t:dd/MM HH}:00 UTC" + (!double.IsNaN(maxDb) && maxDb < 7 ? " · " + L("tutto sotto 7 dB: nessun colore", "all below 7 dB: no colour") : "");
            var ft = new FormattedText(txt, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, new Typeface(FontFamily.Default, FontStyle.Normal, FontWeight.Bold), 11, new SolidColorBrush(bc));
            var at = new Point(Math.Max(4, box.X + 6), Math.Max(4, box.Y + 6));
            ctx.DrawRectangle(new SolidColorBrush(Color.FromArgb(220, Th.Panel.R, Th.Panel.G, Th.Panel.B)), null, new Rect(at.X - 4, at.Y - 2, ft.Width + 8, ft.Height + 4), 4, 4);
            ctx.DrawText(ft, at);
        }

        // copertura in ottica
        if (Los is { } los)
        {
            var poly = new List<Point>();
            for (int i = 0; i < los.az.Length; i++)
                if (m.Screen(Geo.Destination(los.origin, los.az[i], los.first[i]), out var s)) poly.Add(s);
            if (poly.Count > 2)
            {
                var geo = new StreamGeometry();
                using (var c = geo.Open()) { c.BeginFigure(poly[0], true); foreach (var p in poly.Skip(1)) c.LineTo(p); c.EndFigure(true); }
                ctx.DrawGeometry(new SolidColorBrush(Color.FromArgb(70, 60, 170, 255)), new Pen(new SolidColorBrush(Color.FromRgb(30, 120, 220)), 1.5), geo);
            }
            var far = new List<Point>();
            for (int i = 0; i < los.az.Length; i++)
                if (m.Screen(Geo.Destination(los.origin, los.az[i], Math.Max(0.1, los.far[i])), out var s)) far.Add(s);
            if (far.Count > 2)
            {
                var geo = new StreamGeometry();
                using (var c = geo.Open()) { c.BeginFigure(far[0], false); foreach (var p in far.Skip(1)) c.LineTo(p); c.LineTo(far[0]); c.EndFigure(false); }
                ctx.DrawGeometry(null, new Pen(new SolidColorBrush(Color.FromRgb(30, 120, 220)), 1.2, new DashStyle(new double[] { 4, 3 }, 0)), geo);
            }
        }

        // percorso verso il corrispondente
        if (LocatorX.TryParse(TxLoc() ?? "", out var a) && LocatorX.TryParse(RxLoc() ?? "", out var b))
        {
            var pts = Geo.GreatCircle(a, b, 60);
            var col = Last != null ? (Last.SnrSsb >= ReqSsb() ? Th.Ok : Last.SnrFt8 >= ReqFt8() ? Color.Parse("#E0A020") : Th.Bad) : Th.Ink;
            var pen = new Pen(new SolidColorBrush(col), 2.4);
            Point? prev = null;
            foreach (var p in pts)
            {
                if (m.Screen(p, out var s)) { if (prev.HasValue && Math.Abs(prev.Value.X - s.X) < m.Bounds.Width / 2) ctx.DrawLine(pen, prev.Value, s); prev = s; }
                else prev = null;
            }
            if (m.Screen(a, out var sa)) ctx.DrawEllipse(new SolidColorBrush(Th.Accent), null, sa, 5, 5);
        }
    }
}

/// <summary>Profilo del terreno sul percorso con la curvatura (k), il raggio diretto e la fascia del condotto.</summary>
public sealed class ProfileView : Control
{
    public Palette Th;
    LinkResult _r;
    double _ductBase = double.NaN, _ductTop = double.NaN;

    public void Set(LinkResult r, List<ProfileResult> prof)
    {
        _r = r;
        _ductTop = r.RunTop.Length > 0 ? r.RunTop.Max() : double.NaN;
        _ductBase = r.RunBase.Length > 0 ? r.RunBase.Min() : double.NaN;
        InvalidateVisual();
    }

    public override void Render(DrawingContext ctx)
    {
        double W = Bounds.Width, H = Bounds.Height;
        ctx.DrawRectangle(new SolidColorBrush(Th?.MapSea ?? Colors.AliceBlue), null, new Rect(0, 0, W, H));
        if (_r == null || _r.Terrain.Length < 2)
        {
            var e = new FormattedText(Lang.Current == "it" ? "Il profilo del terreno compare qui a calcolo finito." : "The terrain profile appears here when the computation ends.",
                CultureInfo.InvariantCulture, FlowDirection.LeftToRight, Typeface.Default, 11, new SolidColorBrush(Th?.Muted ?? Colors.Gray));
            ctx.DrawText(e, new Point(10, H / 2 - 8));
            return;
        }
        var dist = _r.DistAlong; var h = _r.Terrain; double D = _r.DistKm, ae = _r.K * LinkCalc.Re;
        int n = h.Length;
        var y = new double[n];
        double ymin = 0, ymax = Math.Max(_r.HtxAsl, _r.HrxAsl);
        for (int i = 0; i < n; i++)
        {
            double bulge = dist[i] * (D - dist[i]) / (2 * ae) * 1000;    // rigonfiamento della terra al centro (m)
            y[i] = h[i] + bulge;
            ymax = Math.Max(ymax, y[i]);
        }
        if (!double.IsNaN(_ductTop)) ymax = Math.Max(ymax, _ductTop);
        ymax = ymax * 1.08 + 20;
        double X(double dk) => 8 + (W - 16) * dk / D;
        double Y(double m) => H - 14 - (H - 24) * (m - ymin) / (ymax - ymin);
        if (_r.RunDist.Length > 0)
        {
            // lo strato fondante solo dove esiste, con la sua quota e inclinazione (cima e base punto per punto)
            var dc = Color.FromArgb(_r.LayerTraps ? (byte)90 : (byte)50, 255, 140, 0);
            var band = new StreamGeometry();
            double x0 = Math.Max(0, _r.LayerFromKm), x1 = Math.Min(D, _r.LayerToKm);
            using (var c = band.Open())
            {
                c.BeginFigure(new Point(X(x0), Y(_r.RunTop[0])), true);
                for (int i = 0; i < _r.RunDist.Length; i++) c.LineTo(new Point(X(Math.Max(x0, Math.Min(x1, _r.RunDist[i]))), Y(_r.RunTop[i])));
                c.LineTo(new Point(X(x1), Y(_r.RunTop[_r.RunTop.Length - 1])));
                c.LineTo(new Point(X(x1), Y(Math.Max(0, _r.RunBase[_r.RunBase.Length - 1]))));
                for (int i = _r.RunDist.Length - 1; i >= 0; i--) c.LineTo(new Point(X(Math.Max(x0, Math.Min(x1, _r.RunDist[i]))), Y(Math.Max(0, _r.RunBase[i]))));
                c.LineTo(new Point(X(x0), Y(Math.Max(0, _r.RunBase[0]))));
                c.EndFigure(true);
            }
            ctx.DrawGeometry(new SolidColorBrush(dc), new Pen(new SolidColorBrush(Color.FromRgb(230, 120, 0)), 1), band);
        }
        var g = new StreamGeometry();
        using (var c = g.Open())
        {
            c.BeginFigure(new Point(X(0), Y(ymin)), true);
            int stepI = Math.Max(1, n / 800);
            for (int i = 0; i < n; i += stepI) c.LineTo(new Point(X(dist[i]), Y(y[i])));
            c.LineTo(new Point(X(D), Y(y[n - 1])));
            c.LineTo(new Point(X(D), Y(ymin)));
            c.EndFigure(true);
        }
        ctx.DrawGeometry(new SolidColorBrush(Th?.MapLand ?? Colors.Beige), new Pen(new SolidColorBrush(Th?.MapCoast ?? Colors.SaddleBrown), 1), g);
        // limite inferiore della 1ª zona di Fresnel attorno al raggio diretto
        {
            double lam = 299.792458 / _r.FreqMHz;
            var fz = new StreamGeometry();
            using (var c = fz.Open())
            {
                int stepI = Math.Max(1, n / 300);
                c.BeginFigure(new Point(X(0), Y(_r.HtxAsl)), false);
                for (int i = 0; i < n; i += stepI)
                {
                    double d1 = dist[i], d2 = D - dist[i];
                    double f1 = d1 > 0 && d2 > 0 ? Math.Sqrt(lam * d1 * d2 * 1000 / D) : 0;
                    c.LineTo(new Point(X(d1), Y(_r.HtxAsl + (_r.HrxAsl - _r.HtxAsl) * d1 / D - f1)));
                }
                c.LineTo(new Point(X(D), Y(_r.HrxAsl)));
                c.EndFigure(false);
            }
            ctx.DrawGeometry(null, new Pen(new SolidColorBrush(Color.Parse("#7A5AD8")), 1, new DashStyle(new double[] { 3, 3 }, 0)), fz);
            foreach (var ob in _r.Obstacles) ctx.DrawEllipse(new SolidColorBrush(Color.Parse("#D64545")), null, new Point(X(ob.km), Y(ob.hM + ob.km * (D - ob.km) / (2 * ae) * 1000)), 3, 3);
        }
        var ray = new Pen(new SolidColorBrush(_r.Los ? Color.Parse("#1F9D55") : Color.Parse("#D64545")), 1.6);
        ctx.DrawLine(ray, new Point(X(0), Y(_r.HtxAsl)), new Point(X(D), Y(_r.HrxAsl)));
        var ft = new FormattedText($"{_r.DistKm:0} km · k {_r.K:0.00}", CultureInfo.InvariantCulture, FlowDirection.LeftToRight, Typeface.Default, 11, new SolidColorBrush(Th?.Ink ?? Colors.Black));
        ctx.DrawText(ft, new Point(10, 4));
    }
}
