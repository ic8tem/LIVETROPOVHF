using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using TropoVHF.Core;
using TropoVHF.Forecast;

namespace TropoVHF.Ui;

// Parti della linguetta Previsioni comuni alla versione normale (Avalonia) e a quella per Windows 7 (WinForms con lo
// strato di compatibilità): testi e fumetti dei risultati, disegno della mappa e del profilo del terreno.

public enum FcTone { Normal, Strong, Accent, Ok, Warn, Bad, Muted }

/// <summary>Testi dei risultati e fumetti con fonte e richiamo al manuale.</summary>
/// <summary>
/// Riquadro «Dettagli da mostrare» (bozzetto approvato 22/11/2026): tre livelli che accendono gruppi di caselle,
/// ogni casella resta modificabile a mano. Livello 0 essenziale, 1 normale, 2 completo.
/// </summary>
public static class FcDetails
{
    public record Item(int Group, string Key, string It, string En, int Level);
    public static readonly Item[] All =
    {
        new(0, "fc_ov_curves", "Isolinee del guadagno", "Gain contours", 0),
        new(0, "fc_ov_low", "Anche 3–7 dB (tratteggio)", "Also 3–7 dB (hatched)", 2),
        new(0, "fc_ov_iso", "Isobare (GFS)", "Isobars (GFS)", 1),
        new(0, "fc_ov_inv", "Inversione termica", "Temperature inversion", 1),
        new(0, "fc_ov_wx", "Segni meteo S ↓ ↑", "Weather marks S ↓ ↑", 1),
        new(0, "fc_show_corr", "Correlazione guadagno/pressione", "Gain/pressure correlation", 2),
        new(0, "fc_show_los", "Zona di vista diretta", "Line-of-sight area", 1),
        new(1, "fc_full", "Tutte le righe del calcolo", "All computation lines", 2),
        new(1, "fc_flat", "Profilo a Terra piatta", "Flat-earth profile", 1),
        new(1, "fc_logshow", "Mostra il registro dei calcoli", "Show the computation log", 2),
        new(1, "fc_log", "Registro dettagliato su file", "Detailed log to file", 2),
        new(2, "fc_corr", "Correzioni meteo (Misme, subsidenza)", "Weather corrections (Misme, subsidence)", 0),
        new(2, "fc_seaair", "Contrasto aria-mare", "Air-sea contrast", 1),
        new(2, "fc_nowcast", "Media con gli spot (corridoi)", "Blend with spots (corridors)", 0),
        new(2, "aprs_on", "Salti APRS 2 m e LoRa", "APRS hops 2 m and LoRa", 1),
        new(2, "fc_via_ideal", "Oltre il monte senza perdite", "Beyond the mountain, lossless", 2),
    };
    static string L(string it, string en) => Lang.Tr(it, en);
    public static string Title => L("Dettagli da mostrare", "Details to show");
    public static string Help => L("Il livello accende un gruppo di caselle; ogni casella si può poi cambiare a mano.", "The level turns on a group of boxes; each box can then be changed by hand.");
    public static string[] Levels => new[] { L("Essenziale", "Essential"), L("Normale", "Normal"), L("Completo", "Complete") };
    public static string[] Short => new[] { L("ess.", "ess."), L("norm.", "norm."), L("compl.", "compl.") };
    public static string[] Groups => new[] { L("Sulla mappa", "On the map"), L("Nel calcolo del segnale", "In the signal computation"), L("Correzioni e dati in diretta", "Corrections and live data") };
    public static string LosTip => L("Mostra o nasconde sulla mappa la zona in vista diretta calcolata con «Copertura in ottica». Manuale delle previsioni, § 7.", "Shows or hides on the map the line-of-sight area computed with «Line-of-sight coverage». Forecast manual, § 7.");
    /// <summary>Valori di partenza quando l'impostazione non c'è ancora (gli stessi di prima del riquadro).</summary>
    public static bool Default(string key) => key switch
    {
        "fc_show_corr" or "fc_show_los" or "fc_flat" or "fc_log" or "fc_corr" or "fc_seaair" or "fc_nowcast" or "aprs_on" => true,
        _ => false
    };
}

public static class ForecastText
{
    static string L(string it, string en, params object[] a) => Lang.Tr(it, en, a);
    /// <summary>Segno senza «−0,0» e senza «-+0,0»: valori che arrotondano a zero diventano 0.</summary>
    static double Sg(double x) => Math.Abs(x) < 0.05 || double.IsNaN(x) ? 0.0 : x;
    /// <summary>Perdite enormi (stazione chiusa da un ostacolo vicino) non hanno senso al decimo: sopra 300 dB si scrive «> 300».</summary>
    static string Db(double x) => double.IsNaN(x) ? "—" : x > 300 ? "> 300" : x.ToString("0.0", CultureInfo.CurrentCulture);
    /// <summary>Nome del clima radio e motivo della scelta, tradotti (le chiavi sono le frasi italiane di RadioClimate).</summary>
    static string ClimTr(string it) => it switch
    {
        "equatoriale" => L("equatoriale", "equatorial"),
        "continentale subtropicale" => L("continentale subtropicale", "continental subtropical"),
        "marittimo subtropicale" => L("marittimo subtropicale", "maritime subtropical"),
        "desertico" => L("desertico", "desert"),
        "continentale temperato" => L("continentale temperato", "continental temperate"),
        "marittimo temperato sulla terra" => L("marittimo temperato sulla terra", "maritime temperate overland"),
        "marittimo temperato sul mare" => L("marittimo temperato sul mare", "maritime temperate oversea"),
        "oltre 60° di latitudine" => L("oltre 60° di latitudine", "beyond 60° latitude"),
        "aria secca e poca pioggia" => L("aria secca e poca pioggia", "dry air and little rain"),
        "Ns alta e costante" => L("Ns alta e costante", "high, steady Ns"),
        "forte escursione stagionale" => L("forte escursione stagionale", "large seasonal swing"),
        "Ns alta, fascia subtropicale" => L("Ns alta, fascia subtropicale", "high Ns, subtropical belt"),
        "entrambi gli orizzonti sul mare" => L("entrambi gli orizzonti sul mare", "both horizons on the sea"),
        "escursione piccola, vicino al mare" => L("escursione piccola, vicino al mare", "small swing, near the sea"),
        "escursione stagionale media" => L("escursione stagionale media", "moderate seasonal swing"),
        "senza dati ERA5: latitudine e mare" => L("senza dati ERA5: latitudine e mare", "no ERA5 data: latitude and sea"),
        _ => it
    };

    static string RadioClimateName(LinkResult r) => RadioClimate.Name(r.RadioClim.Climate);

    static string ItmModeEn(string m) => m == "vista" ? "line of sight" : m == "diffrazione" ? "diffraction" : m == "troposcatter" ? "troposcatter" : m;

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
        [L("Antenne sul mare TX / RX", "Antennas above sea TX / RX")] = L("Quota del suolo da Copernicus GLO-30 (ESA/Airbus, 1\", scaricato dal registro aperto di AWS) più l'altezza dal suolo; con locatore a 4–6 caratteri vale la quota sul mare indicata. Manuale delle previsioni, § 7.", "Ground height from Copernicus GLO-30 (ESA/Airbus, 1\", AWS open data) plus height above ground; with a 4–6 character locator the stated height above sea is used. Forecast manual, § 7."),
        [L("Raggio equivalente k", "Effective radius k")] = L("k = 157 / (157 + ΔN), con ΔN la variazione di rifrattività nel primo km dai profili GFS lungo la tratta (ITU-R P.452 § 4.3, ITU-R P.834). Limitato fra 0,6 e 4. Manuale delle previsioni, § 8.", "k = 157 / (157 + ΔN), ΔN being the refractivity change in the first km from GFS profiles along the path (ITU-R P.452 § 4.3, ITU-R P.834). Limited to 0.6–4. Forecast manual, § 8."),
        [L("In vista ottica", "Line of sight")] = L("Metodo di Bullington sul profilo DEM 30 m con la curvatura data da k (ITU-R P.452 § 4.2.1). Manuale delle previsioni, § 8.", "Bullington method on the DEM 30 m profile with the curvature given by k (ITU-R P.452 § 4.2.1). Forecast manual, § 8."),
        [L("1ª zona di Fresnel", "1st Fresnel zone")] = L("Raggio F1 = √(λ·d1·d2/d); libera se lo spazio sopra il terreno è almeno 0,6 F1. Ostacoli: parametro ν e perdita J(ν) del bordo a coltello, ITU-R P.526 eq. 31. Manuale delle previsioni, § 8.", "Radius F1 = √(λ·d1·d2/d); clear if the space above ground is at least 0.6 F1. Obstacles: ν and knife-edge loss J(ν), ITU-R P.526 eq. 31. Forecast manual, § 8."),
        [L("Spazio libero", "Free space")] = L("Lfs = 92,44 + 20 log f(GHz) + 20 log d(km), ITU-R P.525. Manuale delle previsioni, § 8.", "Lfs = 92.44 + 20 log f(GHz) + 20 log d(km), ITU-R P.525. Forecast manual, § 8."),
        [L("Diffrazione (Bullington)", "Diffraction (Bullington)")] = L("Bullington sul profilo reale più la correzione di terra sferica «delta-Bullington» (ITU-R P.452-16 § 4.2, eq. 161–164) con la perdita sferica di ITU-R P.526 § 3. Manuale delle previsioni, § 8.", "Bullington on the real profile plus the spherical-earth delta-Bullington correction (ITU-R P.452-16 § 4.2, eq. 161–164) with ITU-R P.526 § 3 spherical loss. Forecast manual, § 8."),
        [L("Troposcatter (P.452)", "Troposcatter (P.452)")] = L("ITU-R P.452-16 eq. 45 al 50% del tempo, con distanza angolare θ dagli orizzonti, N0 dai profili GFS e perdita d'accoppiamento d'apertura dai guadagni DL6WU. Manuale delle previsioni, § 8.", "ITU-R P.452-16 eq. 45 at 50% of time, angular distance θ from the horizons, N0 from GFS profiles and aperture coupling loss from DL6WU gains. Forecast manual, § 8."),
        [L("Troposcatter, altri modelli", "Troposcatter, other models")] = L("NBS TN101 § 9 (Rice, Longley, Norton e Barsis, 1967: funzione d'attenuazione F(θd), funzione di guadagno in frequenza H0, Ns dei profili GFS, orizzonti dal profilo reale), ITU-R P.617-5 (mediana) con N0 e ΔN presi ogni ora dai profili GFS invece che dalle mappe climatologiche, e la formula del VHF/UHF Handbook RSGB (DL2FZN). La differenza fra i quattro modelli, P.452 compreso, è mostrata come incertezza, come fa TropoLink. Solo confronto. Manuale delle previsioni, § 8 bis.", "NBS TN101 § 9 (Rice, Longley, Norton and Barsis, 1967: attenuation function F(θd), frequency gain function H0, Ns from GFS profiles, horizons from the real profile), ITU-R P.617-5 (median) with N0 and ΔN taken hourly from GFS profiles instead of the climatological maps, and the RSGB VHF/UHF Handbook formula (DL2FZN). The spread of the four models, P.452 included, is shown as uncertainty, as TropoLink does. Check only. Forecast manual, § 8 bis."),
        [L("Meteo lungo la tratta", "Weather along path")] = L("Subsidenza: aria che scende (velocità verticale GFS a 850 e 700 hPa sotto −1 cm/s) con inversione o forte calo di K, +1,5 dB (Moler e Holden, J. Res. NBS 64D, 1960); aria in salita oltre +5 cm/s, −2 dB. Stabilità di Misme: ΔW del primo km rispetto all'atmosfera standard (Bean e Dutton cap. 6), solo senza condotto. Scirocco: vento da SE–S a 850 hPa di almeno 7 m/s con T850 ≥ 16 °C sul Mediterraneo (ARRL Handbook § 19.4). Aria-mare (sul mare): θ a 925 hPa oltre la SST di 4 K +1 dB, di 7 K +2 dB, +1 se il mare si è raffreddato di 1,5 °C nelle 72 ore prima; mare più caldo dell'aria a 2 m di 2 °C −1,5 dB, di 4 °C −3 dB (§ 9 quater). Condotto morbido: strato che non intrappola la banda ma con λmax almeno un terzo della lunghezza d'onda (Kerr, Hepburn). Persistenza: ore delle 24 precedenti sopra 13 dB, fino a +2 dB (VHF/UHF DX Book; Biancomano e Leyshon). Manuale delle previsioni, § 9 bis e 10.", "Subsidence: sinking air (GFS vertical velocity at 850 and 700 hPa below −1 cm/s) with an inversion or a strong K drop, +1.5 dB (Moler & Holden, J. Res. NBS 64D, 1960); rising air above +5 cm/s, −2 dB. Misme stability: first-km ΔW against the standard atmosphere (Bean & Dutton ch. 6), only without a duct. Scirocco: SE–S wind at 850 hPa of at least 7 m/s with T850 ≥ 16 °C over the Mediterranean (ARRL Handbook § 19.4). Forecast manual, § 9 bis."),
        [L("Probabilità per periodo", "Probability per period")] = L("Probabilità che un periodo (una trasmissione FT8, un cambio in SSB) superi la soglia del modo, con l'evanescenza entro l'ora come distribuzione normale: Φ(margine/σ). Bray (QST, novembre 1961, p. 39): oltre circa 100 miglia l'affievolimento entro l'ora ha un'ampiezza di 13,5 dB, cioè circa 7 dB sotto e 7 sopra la media; sotto le 100 miglia cala quasi in linea retta (3,5 dB a 50 miglia). Preso ±7 dB come ±2σ: σ = 3,4 dB oltre 160 km, proporzionale alla distanza sotto. Per un collegamento «solido» Bray toglie 7 dB dal bilancio (qui circa 98%). Non comprende l'incertezza del modello, che è più grande. Manuale delle previsioni, § 12.", "Probability that a period (one FT8 transmission, one SSB over) exceeds the mode threshold, with within-the-hour fading as a normal distribution: Φ(margin/σ). Bray (QST, November 1961, p. 39): beyond about 100 miles the within-the-hour fade amplitude is 13.5 dB, i.e. about 7 dB below and 7 above the average; below 100 miles it drops almost linearly (3.5 dB at 50 miles). Taking ±7 dB as ±2σ: σ = 3.4 dB beyond 160 km, proportional to distance below. For «solid» copy Bray subtracts 7 dB from the budget (about 98% here). Model uncertainty, which is larger, is not included. Forecast manual, § 12."),

        [L("Puntamento consigliato", "Recommended heading")] = L("Dove puntare l'antenna quando il corrispondente non si raggiunge bene in diretta. Il programma fa il giro dell'orizzonte ogni 5° e prende in ogni direzione il punto del terreno illuminato dall'antenna (sopra 100 m, fra 1 e 60 km); con una stima rapida sceglie i 4 più promettenti e per questi calcola per intero la tratta dal diffusore al corrispondente. Se la via migliore costa meno del diretto, indica quell'azimut. Stima grossolana (±10 dB): da verificare in aria. Manuale delle previsioni, § 10 bis.", "Where to point the antenna when the other station cannot be reached well directly. The program sweeps the horizon every 5° and in each direction takes the terrain point lit by the antenna (above 100 m, 1–60 km); a quick estimate picks the 4 most promising and for these the full path from the scatterer to the other station is computed. If the best way costs less than direct, that azimuth is shown. Rough estimate (±10 dB): check on air. Forecast manual, § 10 bis."),
        [L("Via monte", "Via mountain")] = L("Con il corrispondente chiuso dietro l'orizzonte, si possono puntare le antenne su un rilievo vicino ben visibile: il versante illuminato diffonde il segnale. Tratta 1 stazione → rilievo nello spazio libero; tratta 2 dal rilievo al corrispondente, calcolata come se il rilievo trasmettesse (stessa previsione); conversione con l'equazione del radar bistatico, σ = σ0·A con σ0 = −15 dB e A l'area illuminata dal fascio (al massimo 20 km²). Si prova tutto il giro dell'orizzonte ogni 5°: i 4 punti più promettenti (sopra 100 m, fra 1 e 60 km) si calcolano per intero. Stima grossolana: ±10 dB. Manuale delle previsioni, § 10 bis.", "With the other station closed behind the horizon, the antennas can be aimed at a nearby, clearly visible mountain: the lit slope scatters the signal. Leg 1 station → mountain in free space; leg 2 from the mountain to the other station, computed as if the mountain transmitted (same forecast); conversion with the bistatic radar equation, σ = σ0·A with σ0 = −15 dB and A the area lit by the beam (at most 20 km²). The whole horizon is swept every 5°: the 4 most promising points (above 100 m, 1–60 km) are computed in full. Rough estimate: ±10 dB. Forecast manual, § 10 bis."),
        [L("ITM (Longley-Rice)", "ITM (Longley-Rice)")] = L("Secondo calcolo indipendente della perdita in atmosfera normale, con il modello NTIA Irregular Terrain Model 1.2.2 (codice ufficiale del governo USA tradotto in C#) sullo stesso profilo DEM 30 m: vista con riflessione dal suolo, diffrazione (spigolo e terra sferica pesati sulla rugosità Δh) e troposcatter, con il clima radio del TN101 scelto dai dati ERA5 (riga «Clima radio») e N0 dalla rifrattività media del luogo, polarizzazione orizzontale. Perdita superata nel 50% del tempo, e quella che si ha nel 10% e nell'1% del tempo migliore. Non modella i condotti: la differenza con P.452 dà un'idea dell'incertezza della linea di base. Manuale delle previsioni, § 10 quater.", "Second independent loss calculation in a normal atmosphere, with the NTIA Irregular Terrain Model 1.2.2 (official US government code translated to C#) on the same DEM 30 m profile: line of sight with ground reflection, diffraction (knife edge and spherical earth weighted by roughness Δh) and troposcatter, with the TN101 radio climate chosen from ERA5 data («Radio climate» line) and N0 from the local mean refractivity, horizontal polarisation. Loss for 50% of the time, and the one reached in the best 10% and 1% of the time. It does not model ducts: the difference with P.452 hints at the uncertainty of the baseline. Forecast manual, § 10 quater."),
        [L("Clima radio (TN101)", "Radio climate (TN101)")] = L("Clima radio della tratta secondo NBS TN101 § 10.2, scelto dal punto medio con tre anni di dati giornalieri della rianalisi ERA5 (Open-Meteo): rifrattività al suolo Ns media e sua escursione nell'anno, umidità, pioggia, latitudine, mare sulla tratta e orizzonti sul mare. Y è la variabilità nel tempo del TN101 § 10 rispetto alla mediana (curve dell'ITM): quanti dB si guadagnano nel 10% e nell'1% del tempo migliore e quanti se ne perdono nel 90%. È statistica di lungo periodo: non cambia la perdita prevista, che segue il tempo di oggi. I dati si scaricano una volta per ogni cella di 0,5° (cartella Dati/CLIMA); senza rete si usa una regola di riserva con latitudine e mare. Manuale delle previsioni, § 10 sexies.", "Radio climate of the path according to NBS TN101 § 10.2, chosen at the midpoint from three years of daily ERA5 reanalysis data (Open-Meteo): mean surface refractivity Ns and its yearly swing, humidity, rain, latitude, sea along the path and horizons on the sea. Y is the TN101 § 10 time variability relative to the median (ITM curves): how many dB are gained in the best 10% and 1% of the time and how many are lost in 90%. It is long-term statistics: it does not change the predicted loss, which follows today's weather. Data are downloaded once per 0.5° cell (Dati/CLIMA folder); without network a fallback rule with latitude and sea is used. Forecast manual, § 10 sexies."),
        [L("Dal monte, senza perdita", "From the peak, lossless")] = L("Opzione «Via monte senza perdita fino al monte»: la tua stazione è portata sul punto migliore trovato nel giro d'orizzonte (antenna a 10 m sopra la cima, stessa potenza, antenna e cavo) e l'abbattimento fra te e il punto è considerato nullo. Conta solo la tratta dal punto al corrispondente, calcolata con lo stesso modello (strati, condotti, diffrazione, ITM). È un limite ottimistico: dice se la direzione si apre quando il monte rilancia bene il segnale. Locatore del punto a 10 caratteri. Manuale delle previsioni, § 10 bis.", "«Via mountain lossless to the peak» option: your station is moved to the best point found in the horizon sweep (antenna 10 m above the summit, same power, antenna and cable) and the loss between you and the point is taken as zero. Only the leg from the point to the other station counts, computed with the same model (layers, ducts, diffraction, ITM). It is an optimistic bound: it tells whether the direction opens if the mountain relaunches the signal well. 10-character locator of the point. Forecast manual, § 10 bis."),
        [L("Rilievi vicini (tetto 6 dB)", "Nearby terrain (6 dB cap)")] = L("Criterio dell'autore: i rilievi entro 20 km da ciascuna stazione, visti in ottica, possono aggiungere al troposcatter al massimo 6 dB. L'angolo d'orizzonte dovuto a loro si limita a quello dei rilievi lontani più 10,5 mrad (P.452 conta 0,573 dB per mrad). La riga dice quanti dB si sono tolti rispetto a P.452 puro. Manuale delle previsioni, § 8.", "Author's rule: terrain within 20 km of each station, seen in line of sight, can add at most 6 dB to troposcatter. Its horizon angle is limited to that of distant terrain plus 10.5 mrad (P.452 counts 0.573 dB per mrad). The line shows how many dB were removed versus plain P.452. Forecast manual, § 8."),
        [L("Uscita dal condotto", "Duct exit")] = L("Quando il condotto che intrappola la banda finisce prima del corrispondente: dal punto d'uscita, con un'antenna virtuale a metà del condotto, il tratto finale fino al corrispondente si calcola con l'ITM (eccesso sopra lo spazio libero, 50% del tempo). Totale: spazio libero + aggancio del TX (o diffrazione d'ingresso) + attenuazione residua γd nel condotto + tratto finale. Se è più bassa della perdita prevista diventa il meccanismo. Manuale delle previsioni, § 10 quater.", "When the duct trapping the band ends before the other station: from the exit point, with a virtual antenna at mid-duct, the final leg to the other station is computed with ITM (excess over free space, 50% of the time). Total: free space + TX coupling (or entry diffraction) + residual attenuation γd in the duct + final leg. If lower than the predicted loss it becomes the mechanism. Forecast manual, § 10 quater."),
        [L("Ingresso nel condotto", "Duct entry")] = L("Quando il condotto che intrappola la banda comincia lontano, oltre l'orizzonte della stazione: perdita per portare il segnale fino al suo inizio, a metà del condotto, per diffrazione (spigolo principale del terreno DEM 30 m con il parametro di Fresnel ν, ITU-R P.526, e terra sferica liscia: il peggiore dei due), più il percorso dentro il condotto (spazio libero, attenuazione residua γd di P.452 e accoppiamento del corrispondente). Se è più bassa della perdita prevista diventa il meccanismo. Manuale delle previsioni, § 10 ter.", "When the duct trapping the band starts far away, beyond the station horizon: loss to bring the signal to its start, at mid-duct, by diffraction (main DEM 30 m terrain edge with Fresnel parameter ν, ITU-R P.526, and smooth spherical earth: the worse of the two), plus the path inside the duct (free space, P.452 residual attenuation γd and coupling of the other station). If lower than the predicted loss it becomes the mechanism. Forecast manual, § 10 ter."),
        [L("Modi digitali", "Digital modes")] = L("S/N in 2500 Hz confrontato con le soglie misurate al 50% di decodifica, senza informazione a priori: FT8 −21 dB e FT4 −17,5 dB; in troposcatter, con l'evanescenza, FT8 −18,6 e FT4 −15,2 dB (Franke, Somerville e Taylor, QEX 2020). Q65-15C −22,2, Q65-30B −24,8, Q65-60A −27,6 dB (K1JT e altri, guida rapida di Q65, 2021): le lettere B e C hanno toni 2 e 4 volte più larghi, con la stessa soglia quando l'allargamento Doppler è piccolo; in aircraft scatter, dove il Doppler supera i toni di 60A (1,7 Hz), sono meglio 30B o 15C. JT65A −25 dB (W3SZ 2019). La decodifica AP (nominativo già noto o fine QSO) dà circa 2 dB in più per FT8 e da 1,5 a 2,6 dB per Q65: non è compresa nella soglia. ✓ sopra la soglia, ✗ sotto. Manuale delle previsioni, § 12.", "S/N in 2500 Hz against thresholds measured at 50% decoding, without a priori information: FT8 −21 dB and FT4 −17.5 dB; in troposcatter, with fading, FT8 −18.6 and FT4 −15.2 dB (Franke, Somerville & Taylor, QEX 2020). Q65-15C −22.2, Q65-30B −24.8, Q65-60A −27.6 dB (K1JT et al., Q65 quick-start guide, 2021): letters B and C have tones 2 and 4 times wider, with the same threshold when Doppler spread is small; in aircraft scatter, where Doppler exceeds the 60A tones (1.7 Hz), 30B or 15C are better. JT65A −25 dB (W3SZ 2019). AP decoding (call already known or end of QSO) gives about 2 dB more for FT8 and 1.5 to 2.6 dB for Q65: it is not included in the threshold. ✓ above, ✗ below. Forecast manual, § 12."),
        [L("Sorgente dei dati", "Data source")] = L("Modello e servizio che hanno dato i profili: GFS da Open-Meteo o da NOMADS (NOAA, file GRIB2), ICON-EU (DWD) o ECMWF IFS da Open-Meteo, oppure l'archivio delle previsioni per i casi storici. Manuale delle previsioni, § 3 bis.", "Model and service that supplied the profiles: GFS from Open-Meteo or NOMADS (NOAA, GRIB2 files), ICON-EU (DWD) or ECMWF IFS from Open-Meteo, or the forecast archive for historical cases. Forecast manual, § 3 bis."),
        [L("Troposcatter pomeridiano", "Afternoon troposcatter")] = L("Di giorno sulla terra il sole rimescola l'aria e alla cima dello strato limite l'aria umida incontra quella secca: lì la turbolenza dell'indice di rifrazione è massima. Se quella cima arriva entro 500 m dalla base del volume comune, il troposcatter guadagna fino a 3 dB (a metà fra 500 e 1000 m). Solo con rilievi lungo la tratta, volume comune sulla terra e sole alto. Dalle osservazioni di IK3XTV sulle aperture transalpine pomeridiane; valore provvisorio. Manuale delle previsioni, § 8 ter.", "By day over land the sun mixes the air, and at the top of the boundary layer moist air meets dry air: refractive-index turbulence peaks there. If that top comes within 500 m of the common-volume base, troposcatter gains up to 3 dB (half between 500 and 1000 m). Only with relief along the path, common volume over land and high sun. From IK3XTV's afternoon trans-Alpine observations; provisional value. Forecast manual, § 8 ter."),
        [L("Volume comune", "Common volume")] = L("Base del volume di scatter: dove si incrociano i raggi radenti delle due stazioni, con gli angoli d'orizzonte dal profilo DEM 30 m e il raggio equivalente k·6371 km. Con orizzonti piatti vale d²/(8·ae): circa 1,3 km a 300 km, 3,7 km a 500 km, 9,4 km a 800 km; più è alto, più l'aria è secca e rarefatta e lo scatter è debole. Accanto: rifrattività, gradiente e umidità del GFS a quella quota, e la distanza equivalente di Bray (QST, novembre 1961: ogni grado d'orizzonte vale circa 111 km). Manuale delle previsioni, § 8 bis.", "Base of the scatter volume: where the grazing rays of the two stations cross, using DEM 30 m horizon angles and the effective radius k·6371 km. With flat horizons it is d²/(8·ae): about 1.3 km at 300 km, 3.7 km at 500 km, 9.4 km at 800 km; the higher, the drier and thinner the air and the weaker the scatter. Also: GFS refractivity, gradient and humidity at that height, and Bray's equivalent distance (QST, November 1961: each degree of horizon is worth about 111 km). Forecast manual, § 8 bis."),
        [L("Equazione parabolica", "Parabolic equation")] = L("Calcolo di campo con l'equazione parabolica (split-step di Fourier, come PETOOL e il framework wave-propagation di M. Lytaev, MIT): l'onda avanza lungo la tratta nel profilo di rifrattività modificata M del GFS, punto per punto, con i salti stimati dei condotti potenziali, sopra il terreno DEM 30 m a gradini. Vede da sé condotti, condotti che perdono e diffrazione; il troposcatter si somma a parte. «Guadagno del profilo» = stessa tratta con atmosfera standard meno profilo previsto. Solo confronto. Manuale delle previsioni, § 11 bis.", "Field calculation with the parabolic equation (split-step Fourier, as in PETOOL and M. Lytaev's wave-propagation framework, MIT): the wave advances along the path through the GFS modified-refractivity profile M, point by point, with the estimated jumps of potential ducts, over stepped DEM 30 m terrain. It captures ducts, leaky ducts and diffraction by itself; troposcatter is added separately. «Profile gain» = same path in a standard atmosphere minus the forecast profile. Check only. Forecast manual, § 11 bis."),
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
        [L("Terreno dentro lo strato", "Terrain inside layer")] = L("Chilometri in cui il terreno DEM 30 m sale sopra la base dello strato: lì il condotto è interrotto o deviato. Manuale delle previsioni, § 9.", "Kilometres where DEM 30 m terrain rises above the layer base: the duct is broken or deflected there. Forecast manual, § 9."),
        [L("Antenne e condotto", "Antennas vs duct")] = L("Posizione delle antenne rispetto al condotto: dentro 3 dB, fuori 3 dB più 1 dB ogni 10 m, al massimo 40 dB. Stima empirica dell'accoppiamento. Manuale delle previsioni, § 10.", "Antenna position relative to the duct: inside 3 dB, outside 3 dB plus 1 dB per 10 m, at most 40 dB. Empirical coupling estimate. Forecast manual, § 10."),
        [L("Attenuazione residua", "Residual attenuation")] = L("Attenuazione nel condotto γd = 5·10⁻⁵·ae·f^(1/3) dB/mrad sulla distanza angolare, ITU-R P.452-16 eq. 49. Manuale delle previsioni, § 10.", "In-duct attenuation γd = 5·10⁻⁵·ae·f^(1/3) dB/mrad over the angular distance, ITU-R P.452-16 eq. 49. Forecast manual, § 10."),
        [L("Guadagno tropo", "Tropo gain")] = L("Guadagno dello strato (10–25 dB secondo Bean e Dutton e il deficit del condotto) per la frazione usata; −10 dB con sub-rifrazione diffusa. Manuale delle previsioni, § 9.", "Layer gain (10–25 dB from Bean & Dutton and the duct deficit) times the fraction used; −10 dB with widespread sub-refraction. Forecast manual, § 9."),
        [L("Stima prudente / aggressiva", "Cautious / aggressive estimate")] = L("Prudente: soglia di super-rifrazione di Bean e Dutton (100 N/km nello strato 1000–850 hPa). Aggressiva: soglia classica di super-rifrazione (79 N/km, k ≈ 2). Con un condotto che intrappola la banda le due stime coincidono. Manuale delle previsioni, § 9 ter.", "Cautious: Bean and Dutton super-refraction threshold (100 N/km in the 1000–850 hPa layer). Aggressive: classic super-refraction threshold (79 N/km, k ≈ 2). With a duct trapping the band both are equal. Forecast manual, § 9 ter."),
        [L("Correzioni 1.3.2", "1.3.2 corrections")] = L("Peso del mare (μ1 di ITU-R P.452: più lungo il tratto continuo di terra, meno condotti; × 0,8–1,2), quota delle stazioni rispetto al condotto anche quando copre solo parte della tratta, cima dello strato limite del GFS sopra le creste (IT9GSF, +1,5 dB al massimo), alta pressione estesa con vento debole (+1 dB). Manuale delle previsioni, § 9 ter.", "Sea weight (ITU-R P.452 μ1: the longer the continuous land section, the fewer ducts; × 0.8–1.2), station height vs duct also when it covers only part of the path, GFS boundary-layer top above the ridges (IT9GSF, up to +1.5 dB), broad high pressure with light wind (+1 dB). Forecast manual, § 9 ter."),
        [L("Perdita prevista", "Predicted loss")] = L("Perdita senza tropo meno il guadagno tropo, oppure la perdita nel condotto se lo strato copre almeno l'80% della tratta; mai sotto lo spazio libero. Manuale delle previsioni, § 8–10.", "Loss without tropo minus tropo gain, or the in-duct loss if the layer covers at least 80% of the path; never below free space. Forecast manual, § 8–10."),
        [L("Confronto P.452 (p 20%)", "P.452 check (p 20%)")] = L("ITU-R P.452-16 § 4.5 (condotti e riflessione su strati) con il ΔN mediano del GFS e p = 20% come costante di calibrazione, come in Indermuehle e Suzuki 2026. Include Alf, l'accoppiamento empirico sotto 0,5 GHz. Solo confronto. Manuale delle previsioni, § 11.", "ITU-R P.452-16 § 4.5 (ducting and layer reflection) with GFS median ΔN and p = 20% as calibration constant, as in Indermuehle & Suzuki 2026. Includes Alf, the empirical coupling loss below 0.5 GHz. Check only. Forecast manual, § 11."),
        [L("Tempo di risposta", "Response time")] = L("Tempo dal clic al risultato, per tappe: profili GFS, terreno DEM 30 m, calcolo. Il dettaglio è nel registro alle righe TEMPI. Manuale delle previsioni, § 13.", "Time from click to result, by stage: GFS profiles, DEM 30 m terrain, computation. Details in the log lines TEMPI. Forecast manual, § 13."),
        [L("EIRP", "EIRP")] = L("Potenza meno perdita del cavo più guadagno dell'antenna TX (Yagi DL6WU: circa 9,4 + 7,8 log(boom/λ) dBd, cioè 2,35 dB per raddoppio del boom, VHF Communications 3/1982). Manuale delle previsioni, § 12.", "Power minus cable loss plus TX antenna gain (DL6WU Yagi: about 9.4 + 7.8 log(boom/λ) dBd, i.e. 2.35 dB per boom doubling, VHF Communications 3/1982). Forecast manual, § 12."),
        [L("Antenne TX / RX", "Antennas TX / RX")] = L("Guadagno delle Yagi DL6WU dalla lunghezza del boom (in dBi = dBd + 2,15). È una stima prudente: le Yagi ottimizzate misurate dal NBS (Viezbicke, TN 688, 1976) danno 1–1,7 dB in più fra 0,8 e 4,2 λ di boom. Manuale delle previsioni, § 12.", "DL6WU Yagi gain from boom length (dBi = dBd + 2.15). A conservative estimate: optimised Yagis measured by NBS (Viezbicke, TN 688, 1976) give 1–1.7 dB more between 0.8 and 4.2 λ of boom. Forecast manual, § 12."),
        [L("Segnale ricevuto", "Received signal")] = L("EIRP − perdita prevista + guadagno RX − cavo RX, all'ingresso del ricevitore. S-meter IARU VHF/UHF: S9 = −93 dBm, 6 dB per unità (IARU R1, raccomandazione sugli S-meter). Manuale delle previsioni, § 12.", "EIRP − predicted loss + RX gain − RX cable, at the receiver input. IARU VHF/UHF S-meter: S9 = −93 dBm, 6 dB per unit (IARU R1 S-meter recommendation). Forecast manual, § 12."),
        [L("Rumore (P.372)", "Noise (P.372)")] = L("Rumore esterno ITU-R P.372: galattico Fa = 52 − 23 log f, artificiale Fa = c − d log f secondo l'ambiente; Tsys = Ta + Tpre + [290(Lc−1) + 290(Fr−1)Lc]/Gpre ai morsetti d'antenna, con il preamplificatore in antenna fisso (NF 0,8 dB, guadagno 20 dB) davanti al cavo e alla radio (NF Fr, di base 6 dB). Manuale delle previsioni, § 12.", "External noise ITU-R P.372: galactic Fa = 52 − 23 log f, man-made Fa = c − d log f by environment; Tsys = Ta + Tpre + [290(Lc−1) + 290(Fr−1)Lc]/Gpre at the antenna terminals, with the fixed mast-head preamplifier (NF 0.8 dB, gain 20 dB) ahead of the cable and the radio (NF Fr, 6 dB by default). Forecast manual, § 12."),
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
        foreach (var ob in r.Obstacles)
            Line("  " + (ob.hM < 1 ? L("curvatura (mare)", "earth bulge (sea)") : L("ostacolo", "obstacle")) + $" {ob.km:0.0} km",
                 (ob.hM < 1 ? L("rigonfiamento della terra", "earth bulge") : $"{ob.hM:0} m") + $" · {ob.ratio:0.0} F1 · ν {ob.nu:0.0}");
        Line(L("Spazio libero", "Free space"), $"{r.LfsDb:0.0} dB");
        Line(L("Diffrazione (Bullington)", "Diffraction (Bullington)"), $"{Db(r.LdiffDb)} dB (+{Db(r.LbullDb)})");
        Line(L("Troposcatter (P.452)", "Troposcatter (P.452)"), $"{Db(r.LscatDb)} dB (θ {r.ThetaMrad:0.0} mrad)" + (r.ThetaMrad > 300 ? L(" · orizzonte molto alto: un rilievo vicino chiude la direzione", " · very high horizon: nearby terrain blocks this direction") : ""));
        if (!double.IsNaN(r.NearTxMrad) || !double.IsNaN(r.NearRxMrad))
            Line(L("Rilievi vicini (tetto 6 dB)", "Nearby terrain (6 dB cap)"), string.Join(" · ", new[] {
                double.IsNaN(r.NearTxMrad) ? null : L("TX a {0:0.0} km, {1:0} mrad: −{2:0.0} dB", "TX at {0:0.0} km, {1:0} mrad: −{2:0.0} dB", r.NearTxKm, r.NearTxMrad, r.NearTxSavedDb),
                double.IsNaN(r.NearRxMrad) ? null : L("RX a {0:0.0} km, {1:0} mrad: −{2:0.0} dB", "RX at {0:0.0} km, {1:0} mrad: −{2:0.0} dB", r.NearRxKm, r.NearRxMrad, r.NearRxSavedDb) }.Where(x => x != null)));
        if (!double.IsNaN(r.RsgbDb))
            Line(L("Troposcatter, altri modelli", "Troposcatter, other models"), (double.IsNaN(r.Tn101Db) ? "" : $"TN101 {r.Tn101Db:0.0} · ") + (double.IsNaN(r.P617Db) ? "" : $"P.617 {r.P617Db:0.0} · ") + $"RSGB {r.RsgbDb:0.0} dB · " + L("incertezza", "uncertainty") + $" ±{r.ScatSpreadDb / 2:0.0} dB");
        if (!double.IsNaN(r.CvHeightM))
            Line(L("Volume comune", "Common volume"), $"{r.CvHeightM / 1000:0.0} km " + L("di quota", "high") + $", {r.CvKm:0} km " + L("dal TX", "from TX") +
                 (double.IsNaN(r.CvGrad) ? "" : $" · N {r.CvN:0}, −dN/dh {r.CvGrad:0} N/km, UR {r.CvRH:0}%") + $" · d eq. {r.DeqKm:0} km");
        if (r.MixScatterDb > 0) Line(L("Troposcatter pomeridiano", "Afternoon troposcatter"), L("cima dello strato rimescolato a {0:0} m, volume comune a {1:0} m: −{2:0.0} dB sul troposcatter", "mixed-layer top at {0:0} m, common volume at {1:0} m: −{2:0.0} dB on troposcatter", r.MixTopM, r.CvHeightM, r.MixScatterDb), FcTone.Accent);
        Line(L("Senza tropo", "Without tropo"), $"{Db(r.LbaseDb)} dB");
        if (r.ForecastUsed)
        {
            if (r.LayerName != "")
            {
                Line(L("Strato fondante", "Governing layer"), $"{r.LayerName} · " + (r.LayerTraps ? L("condotto", "duct") : L("super-rifrattivo", "super-refractive")), FcTone.Strong, true);
                Line(L("Continuità sulla tratta", "Continuity along path"), $"{r.LayerFromKm:0}–{r.LayerToKm:0} km ({r.LayerFrac * 100:0}%)");
                if (!double.IsNaN(r.LandCov)) Line(L("Terra coperta dallo strato", "Land covered by layer"), $"{r.LandInDuctKm:0} / {r.LandKm:0} km ({r.LandCov * 100:0}%) · " + L("frazione usata", "fraction used") + $" {r.FracUsed * 100:0}%");
                Line(L("Quota della cima", "Top height"), $"{r.TopMean:0} m ({r.TopMin:0}–{r.TopMax:0}), " + L("base", "base") + $" {r.BaseMean:0} m");
                Line(L("Inclinazione", "Tilt"), $"{Sg(r.TiltMkm):+0.0;-0.0} m/km");
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
            Line(L("Guadagno tropo", "Tropo gain"), $"{Sg(r.TropoGainDb):+0.0;-0.0} dB");
            var wx = new List<string>();
            if (r.SubsFrac > 0) wx.Add(L("subsidenza", "subsidence") + $" {r.SubsFrac * 100:0}%");
            if (r.AscentFrac > 0) wx.Add(L("aria in salita", "rising air") + $" {r.AscentFrac * 100:0}%");
            if (r.SciroccoFrac > 0) wx.Add(L("scirocco", "scirocco") + $" {r.SciroccoFrac * 100:0}%");
            wx.Add("Misme " + $"{Sg(r.StabDbMean):+0.0;-0.0} dB" + (Refractivity.UseCorrections ? "" : L(" (spento)", " (off)")));
            if (Math.Abs(r.SeaAirDbMean) >= 0.05) wx.Add(L("aria-mare", "air-sea") + $" {Sg(r.SeaAirDbMean):+0.0;-0.0} dB");
            if (r.SoftN > 0) wx.Add(L("condotto morbido", "soft duct") + $" {r.SoftN} " + L("punti", "pts") + $" ({r.SoftMax:0.00})");
            if (!double.IsNaN(r.PersistFrac)) wx.Add(L("persistenza", "persistence") + $" {r.PersistFrac * 100:0}%");
            Line(L("Meteo lungo la tratta", "Weather along path"), string.Join(" · ", wx), r.SciroccoFrac > 0 || r.SubsFrac > 0.3 ? FcTone.Accent : FcTone.Normal);
        }
        Line(L("Perdita prevista", "Predicted loss"), $"{Db(r.LpredDb)} dB · " + (r.LpredDb > 300 ? L("direzione chiusa da un ostacolo vicino", "direction blocked by nearby terrain") : r.Mechanism), FcTone.Strong, true);
        if (!double.IsNaN(r.ItmDb))
            Line(L("ITM (Longley-Rice)", "ITM (Longley-Rice)"), L("{0:0.0} dB al 50% del tempo, {1:0.0} al 10%, {2:0.0} all'1% · {3} · Δh {4:0} m · P.452 {5:0.0} dB ({6:+0.0;-0.0})",
                "{0:0.0} dB at 50% of time, {1:0.0} at 10%, {2:0.0} at 1% · {7} · Δh {4:0} m · P.452 {5:0.0} dB ({6:+0.0;-0.0})", r.ItmDb, r.Itm10Db, r.Itm1Db, r.ItmMode, r.ItmDeltaH, r.LbaseDb, Sg(r.LbaseDb - r.ItmDb), ItmModeEn(r.ItmMode)));
        if (r.RadioClim != null && !double.IsNaN(r.Y10Db))
            Line(L("Clima radio (TN101)", "Radio climate (TN101)"), ClimTr(RadioClimateName(r)) + " (" + ClimTr(r.RadioClim.Why) + ")" +
                 (r.RadioClim.FromEra5 ? $" · Ns {r.RadioClim.NsMean:0} ± {r.RadioClim.NsRange / 2:0}" : "") +
                 " · Y " + L("{0:+0.0;-0.0} dB nel 10% del tempo, {1:+0.0;-0.0} nell'1%, −{2:0.0} nel 90%", "{0:+0.0;-0.0} dB in 10% of time, {1:+0.0;-0.0} in 1%, −{2:0.0} in 90%", r.Y10Db, r.Y1Db, r.Y90Db));
        if (!double.IsNaN(r.LexitDb))
            Line(L("Uscita dal condotto", "Duct exit"), L("a {0:0} km, {1:0} m · tratto finale ITM {2:0.0} dB ({3}) · totale {4:0.0} dB",
                "at {0:0} km, {1:0} m · final leg ITM {2:0.0} dB ({5}) · total {4:0.0} dB", r.ExitKm, r.ExitHeightM, r.ItmTailDb, r.ItmTailMode, r.LexitDb, ItmModeEn(r.ItmTailMode)));
        if (!double.IsNaN(r.LentryDb))
            Line(L("Ingresso nel condotto", "Duct entry"), L("inizio a {0:0} km, {1:0} m · diffrazione {2:0.0} dB (ν {3:0.0} a {4:0} km) · totale {5:0.0} dB",
                                                              "starts at {0:0} km, {1:0} m · diffraction {2:0.0} dB (ν {3:0.0} at {4:0} km) · total {5:0.0} dB", r.EntryKm, r.EntryHeightM, r.EntryDiffDb, r.EntryNu, r.EntryNuKm, r.LentryDb));
        if (!double.IsNaN(r.LpredAggrDb))
            Line(L("Stima prudente / aggressiva", "Cautious / aggressive estimate"), $"{Db(r.LpredDb)} / {Db(r.LpredAggrDb)} dB" + (r.LpredAggrDb < r.LpredDb - 0.05 ? $" ({Sg(r.LpredAggrDb - r.LpredDb):+0.0;-0.0})" : " " + L("(uguali)", "(same)")));
        var extra = new List<string>();
        if (!double.IsNaN(r.Mu1)) extra.Add(L("mare μ1 {0:0.00} (× {1:0.00})", "sea μ1 {0:0.00} (× {1:0.00})", r.Mu1, r.SeaWeight));
        if (!double.IsNaN(r.DuctCouplingF)) extra.Add(L("stazioni e condotto × {0:0.00}", "stations vs duct × {0:0.00}", r.DuctCouplingF));
        if (r.RidgeN > 0 && r.RidgeMaxM >= 500) extra.Add(L("strato limite sopra {0}/{1} creste (+{2:0.0} dB)", "boundary layer above {0}/{1} ridges (+{2:0.0} dB)", r.RidgeCovered, r.RidgeN, r.BlhRidgeDb));
        if (r.EvapDuctM > 0) extra.Add(L("condotto d'evaporazione {0:0} m", "evaporation duct {0:0} m", r.EvapDuctM));
        if (r.DayMixFrac >= 0.3) extra.Add(L("giorno: rimescolamento su terra", "day: mixing over land"));
        if (r.NightRadFrac >= 0.3) extra.Add(L("notte: condotti da irraggiamento possibili", "night: radiation ducts possible"));
        if (r.HighPFrac > 0) extra.Add(L("alta pressione estesa {0:0}%", "broad high pressure {0:0}%", r.HighPFrac * 100));
        if (extra.Count > 0) Line(L("Correzioni 1.3.2", "1.3.2 corrections"), string.Join(" · ", extra));
        if (r.P452Ok) Line(L("Confronto P.452 (p 20%)", "P.452 check (p 20%)"), $"{Db(r.P452Lb)} dB ({Sg(r.P452Lb - r.LpredDb):+0.0;-0.0}) · ΔN {r.P452DeltaN:0} · Lba {Db(r.P452Lba)} dB" + (r.P452Alf > 0 ? $" (Alf {r.P452Alf:0.0})" : ""));
        if (!double.IsNaN(r.PeLossDb))
            Line(L("Equazione parabolica", "Parabolic equation"), $"{Db(r.PeTotalDb)} dB ({Sg(r.PeTotalDb - r.LpredDb):+0.0;-0.0}) · " + L("guadagno del profilo", "profile gain") + $" {Sg(r.PeGainDb):+0.0;-0.0} dB · " + L("solo campo", "field only") + $" {r.PeLossDb:0.0} dB");
        {
            var tot = r.Times.Laps.FirstOrDefault(x => x.Name == "TOTALE").Ms;
            var parts = r.Times.Laps.Where(x => x.Name != "TOTALE" && x.Ms >= 50).Select(x => $"{x.Name} {CalcLog.Dur(x.Ms)}");
            Line(L("Tempo di risposta", "Response time"), CalcLog.Dur(tot) + (parts.Any() ? " · " + string.Join(" · ", parts) : ""));
        }
        Line(L("EIRP", "EIRP"), $"{r.EirpDbm:0.0} dBm ({Math.Pow(10, (r.EirpDbm - 30) / 10):0} W)");
        Line(L("Antenne TX / RX", "Antennas TX / RX"), $"{r.GtxDbi:0.0} / {r.GrxDbi:0.0} dBi");
        Line(L("Segnale ricevuto", "Received signal"), (r.PrxDbm < -230 ? "< −230 dBm · S0" : $"{r.PrxDbm:0.0} dBm · {LinkResult.SMeter(r.PrxDbm)}"), FcTone.Accent, true);
        Line(L("Rumore (P.372)", "Noise (P.372)"), (r.TgalK > 0 ? $"{L("gal.", "gal.")} {r.TgalK:0} + {L("art.", "m-m")} {r.TmanK:0} = " : "") + $"Ta {r.TaK:0} K · Tsys {r.TsysK:0} K");
        Line(L("Rumore in SSB", "SSB noise"), $"{r.NoiseSsbDbm:0.0} dBm (" + L("ai morsetti d'antenna", "at antenna terminals") + ")");
        FcTone V(double snr, double req) => snr >= req ? FcTone.Ok : snr >= req - 6 ? FcTone.Warn : FcTone.Bad;
        Line("S/N SSB", (r.SnrSsb < -60 ? "< −60 dB" : $"{Sg(r.SnrSsb):+0.0;-0.0} dB"), V(r.SnrSsb, reqSsb), true);
        Line("S/N CW", (r.SnrCw < -60 ? "< −60 dB" : $"{Sg(r.SnrCw):+0.0;-0.0} dB"), V(r.SnrCw, reqCw), true);
        bool scatter = r.Mechanism == "troposcatter";
        double ft8 = scatter ? Math.Max(reqFt8, -18.6) : reqFt8;
        // soglie al 50% di decodifica in 2500 Hz, senza AP: FT4 e FT8 da QEX 2020 (tabella 6, con evanescenza «media latitudine
        // disturbato» in troposcatter); Q65 dalla Quick Start Guide di K1JT e altri (2021), sottomodo A: B e C hanno toni 2 e 4 volte
        // più larghi e, con l'allargamento Doppler piccolo della troposfera a 144 MHz, la stessa soglia
        double ft4 = scatter ? -15.2 : -17.5;
        Line("S/N FT8", (r.SnrFt8 < -60 ? "< −60 dB" : $"{Sg(r.SnrFt8):+0.0;-0.0} dB") + (scatter && ft8 > reqFt8 ? L(" (soglia −18,6 con evanescenza)", " (−18.6 threshold with fading)") : ""), V(r.SnrFt8, ft8), true);
        {
            string M(string n, double thr) => $"{n} {(r.SnrFt8 >= thr ? "✓" : "✗")}";
            bool any = r.SnrFt8 >= -27.6;
            Line(L("Modi digitali", "Digital modes"), string.Join(" · ", M("FT4", ft4), M("FT8", ft8), M("Q65-15C", -22.2), M("Q65-30B", -24.8), M("Q65-60A", -27.6), M("JT65", -25)), any ? FcTone.Ok : FcTone.Bad);
        }
        {
            // evanescenza (Bray, QST novembre 1961, p. 39): oltre circa 100 miglia l'affievolimento entro l'ora ha un'ampiezza di
            // 13,5 dB (circa 7 dB sotto e 7 sopra la media oraria); sotto cala quasi in linea retta (3,5 dB a 50 miglia).
            // Si prende ±7 dB ≈ ±2σ: σ = 3,4 dB oltre 160 km, proporzionale alla distanza sotto, almeno 0,5 dB
            double d = r.DistKm, sg = Math.Max(0.5, 3.4 * Math.Min(1, d / 160));
            static double Phi(double x) { double t = 1 / (1 + 0.2316419 * Math.Abs(x)); double y = 1 - 0.3989423 * Math.Exp(-x * x / 2) * t * (0.3193815 + t * (-0.3565638 + t * (1.781478 + t * (-1.821256 + t * 1.330274)))); return x >= 0 ? y : 1 - y; }
            string P(string n, double snr, double thr) => $"{n} {100 * Phi((snr - thr) / sg):0}%";
            Line(L("Probabilità per periodo", "Probability per period"), string.Join(" · ", P("SSB", r.SnrSsb, reqSsb), P("CW", r.SnrCw, reqCw), P("FT4", r.SnrFt8, ft4), P("FT8", r.SnrFt8, ft8), P("Q65-15C", r.SnrFt8, -22.2), P("Q65-30B", r.SnrFt8, -24.8), P("Q65-60A", r.SnrFt8, -27.6), P("JT65", r.SnrFt8, -25)) + $" (σ {sg:0.0} dB)");
        }
        if (r.Via.Count > 0)
        {
            bool ideal = Refractivity.ViaIdeal;
            var bestVia = r.Via.OrderBy(v => v.RankDb).First();
            bool viaWins = bestVia.RankDb < r.LpredDb;
            Line(L("Puntamento consigliato", "Recommended heading"), viaWins
                ? L("azimut {0:0}° verso {1} (non sul corrispondente, {2:0}°): {3} dB meglio del diretto", "azimuth {0:0}° towards {1} (not at the station, {2:0}°): {3} dB better than direct", bestVia.AzDeg, bestVia.Loc10, r.AzDeg, Db(r.LpredDb - bestVia.RankDb))
                : L("diretto, azimut {0:0}°", "direct, azimuth {0:0}°", r.AzDeg), viaWins ? FcTone.Ok : FcTone.Normal, true);
            if (ideal)
            {
                // stazione portata sul punto: perdita nulla fino al monte, conta la tratta dal monte al corrispondente
                double gain = r.LpredDb - bestVia.IdealDb;
                double prx = r.PrxDbm + gain, vSsb = r.SnrSsb + gain, vCw = r.SnrCw + gain, vFt8 = r.SnrFt8 + gain;
                Line(L("Dal monte, senza perdita", "From the peak, lossless"),
                    L("{0} ({1:0} m): {2} dB · {3} · {4} dBm · S/N SSB {5:+0.0;-0.0}, CW {6:+0.0;-0.0}, FT8 {7:+0.0;-0.0} dB",
                      "{0} ({1:0} m): {2} dB · {3} · {4} dBm · S/N SSB {5:+0.0;-0.0}, CW {6:+0.0;-0.0}, FT8 {7:+0.0;-0.0} dB", bestVia.Loc10, bestVia.HeightM, Db(bestVia.IdealDb), bestVia.Mechanism2, (prx < -230 ? "< −230" : prx.ToString("0.0")), Sg(vSsb), Sg(vCw), Sg(vFt8)),
                    V(vSsb, reqSsb), true);
            }
            foreach (var v in r.Via.OrderBy(v => v.RankDb))
                Line(L("Via monte", "Via mountain"), ideal
                    ? L("{0}, azimut {1:0}°, {2:0.0} km, {3:0} m → dal monte {4} dB ({5:+0.0;-0.0} sul diretto; con la diffusione {6} dB)",
                        "{0}, azimuth {1:0}°, {2:0.0} km, {3:0} m → from the peak {4} dB ({5:+0.0;-0.0} vs direct; with scattering {6} dB)", v.Loc10, v.AzDeg, v.DistKm, v.HeightM, Db(v.IdealDb), Sg(v.IdealDb - r.LpredDb), Db(v.TotalDb))
                    : L("{0}, azimut {1:0}°, {2:0.0} km, {3:0} m → {4} dB ({5:+0.0;-0.0} sul diretto)",
                        "{0}, azimuth {1:0}°, {2:0.0} km, {3:0} m → {4} dB ({5:+0.0;-0.0} vs direct)", v.Loc10, v.AzDeg, v.DistKm, v.HeightM, Db(v.TotalDb), Sg(v.TotalDb - r.LpredDb)),
                     v == bestVia && viaWins ? FcTone.Ok : FcTone.Normal);
        }
        foreach (var n in r.Notes) Note("• " + n);
        return res;
    }

    /// <summary>Righe della sintesi (sempre visibili); le altre compaiono con la casella «Tutte le righe del calcolo». Le note restano.</summary>
    static readonly HashSet<string> SummaryLabels = new()
    {
        "Distanza / azimut", "Distance / azimuth", "Strato fondante", "Governing layer", "Guadagno tropo", "Tropo gain",
        "Meteo lungo la tratta", "Weather along path", "Perdita prevista", "Predicted loss", "Segnale ricevuto", "Received signal",
        "S/N SSB", "S/N CW", "S/N FT8", "Modi digitali", "Digital modes", "Probabilità per periodo", "Probability per period",
        "Puntamento consigliato", "Recommended heading", "Dal monte, senza perdita", "From the peak, lossless"
    };
    public static bool IsSummary(string label) => label == "" || SummaryLabels.Contains(label);

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
    static string L(string it, string en, params object[] a) => Lang.Tr(it, en, a);

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

    /// <summary>
    /// Niente «gocce»: le isole sopra il livello più piccole di DropKm (in tutte e due le direzioni) non si disegnano
    /// come curva chiusa. Sono celle isolate del GFS, non un'apertura su una tratta; i colori della mappa restano com'erano.
    /// Un corridoio confermato dagli spot (Corridors) le salva.
    /// </summary>
    public const double DropKm = 220;
    public Func<double, double, bool> Confirmed = (_, _) => false;
    double[,] NoDrops(double[,] g, double level, double lat0, double lon0, double step)
    {
        int nr = g.GetLength(0), nc = g.GetLength(1);
        var o = (double[,])g.Clone();
        var seen = new bool[nr, nc];
        var q = new Stack<(int, int)>(); var cells = new List<(int r, int c)>();
        for (int r0 = 0; r0 < nr; r0++)
            for (int c0 = 0; c0 < nc; c0++)
            {
                if (seen[r0, c0] || double.IsNaN(g[r0, c0]) || g[r0, c0] < level) continue;
                cells.Clear(); q.Push((r0, c0)); seen[r0, c0] = true;
                int rmin = r0, rmax = r0, cmin = c0, cmax = c0;
                while (q.Count > 0)
                {
                    var (r, c) = q.Pop(); cells.Add((r, c));
                    rmin = Math.Min(rmin, r); rmax = Math.Max(rmax, r); cmin = Math.Min(cmin, c); cmax = Math.Max(cmax, c);
                    foreach (var (dr, dc) in new[] { (1, 0), (-1, 0), (0, 1), (0, -1) })
                    {
                        int rr = r + dr, cc = c + dc;
                        if (rr < 0 || cc < 0 || rr >= nr || cc >= nc || seen[rr, cc] || double.IsNaN(g[rr, cc]) || g[rr, cc] < level) continue;
                        seen[rr, cc] = true; q.Push((rr, cc));
                    }
                }
                double lat = lat0 + (rmin + rmax) / 2.0 * step;
                double hKm = (rmax - rmin + 1) * step * 111.2, wKm = (cmax - cmin + 1) * step * 111.2 * Math.Cos(lat * Math.PI / 180);
                if (Math.Max(hKm, wKm) >= DropKm) continue;
                if (cells.Any(x => Confirmed(lat0 + x.r * step, lon0 + x.c * step))) continue;
                foreach (var (r, c) in cells) o[r, c] = level - 0.01;
            }
        return o;
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
        Confirmed = (la, lo) => St.NowcastOn && Math.Abs((t - Clock.UtcNow).TotalMinutes) <= 90 && St.Corridors.At(AppState.BandOf(f), Clock.UtcNow, la, lo) != null;
        if (St.S.GetBool("fc_ov_curves", false) && Own.HasData)
        {
            var g = Sample((la, lo) => { double v = Own.Index(la, lo, t, f); return double.IsNaN(v) ? double.NaN : v * 2.5; }, lat0, lon0, step, nr, nc, double.NaN);
            foreach (var bnd in Palette.GainBands)
            {
                var col = Palette.GainLineColor(bnd.Lo);
                Contour(ctx, NoDrops(g, bnd.Lo, lat0, lon0, step), lat0, lon0, step, bnd.Lo, new Pen(new SolidColorBrush(col), 2.2, lineCap: PenLineCap.Round), $"{bnd.Lo:0.#} dB", col, placed);
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
        if (St.NowcastOn) DrawCorridors(ctx, f);
        Correlation(lat0, lon0, step, nr, nc, t, f);
    }

    /// <summary>Segni meteo sulle celle della griglia: S scirocco, ↓ subsidenza, ↑ aria in salita (solo se le celle sono abbastanza larghe).
    /// Colori per effetto sulla tropo: verde = favorevole (subsidenza, +1,5 dB), rosso = sfavorevole (aria in salita, −2 dB),
    /// arancio = indizio favorevole senza correzione (scirocco). Passando con il mouse sopra un segno compare il fumetto.</summary>
    void DrawWeatherMarks(DrawingContext ctx, DateTime t)
    {
        _marks.Clear();
        var cells = Own.Cells(t);
        if (cells.Count == 0) return;
        Map.Screen(new LatLon(cells[0].lat, cells[0].lon), out var p0);
        Map.Screen(new LatLon(cells[0].lat, cells[0].lon + Own.Step), out var p1);
        if (Math.Abs(p1.X - p0.X) < 24) return;
        Map.HoverKey ??= HitKey;
        var tf = new Typeface(FontFamily.Default, FontStyle.Normal, FontWeight.Bold);
        var halo = new SolidColorBrush(Th.Panel);
        foreach (var (la, lo, pr) in cells)
        {
            int kind = pr.Scirocco ? 0 : pr.Subsidence ? 1 : pr.Ascent ? 2 : -1;
            if (kind < 0) continue;
            string txt = kind == 0 ? "S" : kind == 1 ? "↓" : "↑";
            var col = MarkColor(kind);
            Map.Screen(new LatLon(la, lo), out var sp);
            if (sp.X < 0 || sp.Y < 0 || sp.X > Map.Bounds.Width || sp.Y > Map.Bounds.Height) continue;
            var ft = new FormattedText(txt, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, tf, 24, new SolidColorBrush(col));
            var rc = new Rect(sp.X - ft.Width / 2 - 3, sp.Y - ft.Height / 2, ft.Width + 6, ft.Height);
            ctx.DrawRectangle(halo, new Pen(new SolidColorBrush(col), 1.2), rc, 4, 4);
            ctx.DrawText(ft, new Point(rc.X + 3, rc.Y));
            _marks.Add((rc, kind, pr));
        }
        // fumetto del segno sotto il mouse
        if (Map.Hover is Point hp && HitMark(hp) is int hi && hi >= 0)
        {
            var (rc, kind, pr) = _marks[hi];
            var lines = MarkTip(kind, pr);
            var tfn = new Typeface(FontFamily.Default, FontStyle.Normal, FontWeight.Normal);
            var fts = lines.Select((l, i) => new FormattedText(l, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, i == 0 ? tf : tfn, 12, new SolidColorBrush(i == 0 ? MarkColor(kind) : Th.Ink))).ToList();
            double w = fts.Max(f => f.Width) + 16, h = fts.Sum(f => f.Height + 2) + 12;
            double x = rc.X + rc.Width + 8, y = rc.Y - 4;
            if (x + w > Map.Bounds.Width) x = Math.Max(2, rc.X - w - 8);
            if (y + h > Map.Bounds.Height) y = Math.Max(2, Map.Bounds.Height - h - 2);
            ctx.DrawRectangle(new SolidColorBrush(Th.Panel), new Pen(new SolidColorBrush(MarkColor(kind)), 1.5), new Rect(x, y, w, h), 6, 6);
            double yy = y + 6;
            foreach (var f in fts) { ctx.DrawText(f, new Point(x + 8, yy)); yy += f.Height + 2; }
        }
    }

    readonly List<(Rect rc, int kind, ProfileResult pr)> _marks = new();

    readonly List<Rect> _corrRects = new();

    /// <summary>
    /// Corridoi d'apertura confermati dagli spot. Si disegna solo la zona in cui vale il minimo garantito (vicino alla parte
    /// centrale delle tratte), tutta in una volta: le zone sovrapposte non si sommano. Colore neutro (quello del testo), lontano
    /// dal viola della scala (≥ 21 dB). Le etichette si scansano; le tratte del corridoio compaiono passando con il mouse
    /// sulla sua etichetta.
    /// </summary>
    void DrawCorridors(DrawingContext ctx, double f)
    {
        _corrRects.Clear();
        Map.HoverKey ??= HitKey;
        var list = St.Corridors.Active(AppState.BandOf(f), Clock.UtcNow);
        if (list.Count == 0) return;
        var ink = Th.Ink;
        // una sola geometria con tutte le fasce, riempita con la regola «non zero»: le sovrapposizioni non si sommano
        var zones = new StreamGeometry();
        using (var gc = zones.Open())
        {
            gc.SetFillRule(FillRule.NonZero);
            foreach (var c in list)
                foreach (var o in c.Members)
                {
                    var (ma, mb) = St.Corridors.MiddlePart(o);
                    double az = o.Az, w = St.Corridors.NearKm;
                    var poly = new[] { Geo.Destination(ma, az - 90, w), Geo.Destination(mb, az - 90, w), Geo.Destination(mb, az + 90, w), Geo.Destination(ma, az + 90, w) };
                    Map.Screen(poly[0], out var p0); gc.BeginFigure(p0, true);
                    for (int i = 1; i < 4; i++) { Map.Screen(poly[i], out var pi); gc.LineTo(pi); }
                    gc.EndFigure(true);
                }
        }
        ctx.DrawGeometry(new SolidColorBrush(Color.FromArgb(56, ink.R, ink.G, ink.B)), null, zones);

        // etichette: al centro del corridoio, spostate in basso finché non toccano le altre (al massimo 12 tentativi)
        var tf = new Typeface(FontFamily.Default, FontStyle.Normal, FontWeight.Bold);
        var fg = new SolidColorBrush(ink);
        var border = new Pen(new SolidColorBrush(Color.FromArgb(200, ink.R, ink.G, ink.B)), 1);
        int hover = Map.Hover is Point hp0 ? HitCorridor(hp0) : -1;
        var shown = new List<(Rect rc, FormattedText ft, int i)>();
        for (int i = 0; i < list.Count; i++)
        {
            var c = list[i];
            Map.Screen(c.Mid, out var mp);
            var txt = L("corridoio · {0} tratte · {1:0} km · ≥ {2:0} dB", "corridor · {0} paths · {1:0} km · ≥ {2:0} dB", c.Paths, c.MeanKm, c.FloorDb);
            var ft = new FormattedText(txt, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, tf, 11, fg);
            var rc = new Rect(mp.X - ft.Width / 2 - 4, mp.Y - ft.Height / 2 - 1, ft.Width + 8, ft.Height + 2);
            bool ok = false;
            for (int k = 0; k < 12 && !ok; k++)
            {
                double dy = (k % 2 == 0 ? 1 : -1) * ((k + 1) / 2) * (rc.Height + 3);
                var r = new Rect(rc.X, rc.Y + dy, rc.Width, rc.Height);
                if (shown.All(s2 => s2.rc.Right < r.X || r.Right < s2.rc.X || s2.rc.Bottom < r.Y || r.Bottom < s2.rc.Y)) { rc = r; ok = true; }
            }
            _corrRects.Add(ok ? rc : default);
            if (ok) shown.Add((rc, ft, i));
        }
        // tratte del corridoio sotto il mouse
        if (hover >= 0 && hover < list.Count)
        {
            var thin = new Pen(new SolidColorBrush(ink), 1.4);
            foreach (var o in list[hover].Members.Take(60)) { Map.Screen(o.PA, out var a); Map.Screen(o.PB, out var b); ctx.DrawLine(thin, a, b); }
        }
        foreach (var (rc, ft, i) in shown)
        {
            ctx.DrawRectangle(new SolidColorBrush(Th.Panel), i == hover ? new Pen(new SolidColorBrush(ink), 2) : border, rc, 4, 4);
            ctx.DrawText(ft, new Point(rc.X + 4, rc.Y + 1));
        }
    }

    int HitCorridor(Point p)
    {
        for (int i = 0; i < _corrRects.Count; i++)
        {
            var r = _corrRects[i];
            if (r.Width > 0 && r.Contains(p)) return i;
        }
        return -1;
    }

    /// <summary>Chiave del punto sotto il mouse: un segno meteo (0…) o l'etichetta di un corridoio (100000…).</summary>
    int HitKey(Point p)
    {
        int m = HitMark(p);
        if (m >= 0) return m;
        int c = HitCorridor(p);
        return c >= 0 ? 100000 + c : -1;
    }

    int HitMark(Point p)
    {
        for (int i = _marks.Count - 1; i >= 0; i--)
        {
            var r = _marks[i].rc;
            if (p.X >= r.X - 2 && p.X <= r.X + r.Width + 2 && p.Y >= r.Y - 2 && p.Y <= r.Y + r.Height + 2) return i;
        }
        return -1;
    }

    static Color MarkColor(int kind) => kind == 0 ? Color.Parse("#D35400") : kind == 1 ? Color.Parse("#1E8449") : Color.Parse("#C0392B");

    static List<string> MarkTip(int kind, ProfileResult pr)
    {
        var ci = CultureInfo.InvariantCulture;
        string w = string.Format(ci, "w850 {0:+0.0;-0.0} cm/s, w700 {1:+0.0;-0.0} cm/s", Math.Abs(pr.W850) < 0.0005 ? 0 : pr.W850 * 100, Math.Abs(pr.W700) < 0.0005 ? 0 : pr.W700 * 100);
        return kind switch
        {
            1 => new List<string> {
                L("↓ Subsidenza: FAVOREVOLE alla tropo (+1,5 dB)", "↓ Subsidence: GOOD for tropo (+1.5 dB)"),
                L("L'aria scende (anticiclone), si scalda e si secca:", "Air sinks (anticyclone), warms and dries:"),
                L("rinforza l'inversione e la tiene ferma per ore.", "it strengthens the inversion and holds it for hours."),
                L("Moler e Holden 1960: in media +2,5 dB rispetto all'aria in salita (un caso di +20 dB).", "Moler & Holden 1960: on average +2.5 dB versus rising air (one case of +20 dB)."),
                L("Velocità verticale: ", "Vertical speed: ") + w + L(" (negativa = discesa)", " (negative = sinking)"),
                L("Manuale delle previsioni, § 9 bis.", "Forecast manual, § 9 bis.") },
            2 => new List<string> {
                L("↑ Aria in salita: SFAVOREVOLE alla tropo (−2 dB)", "↑ Rising air: BAD for tropo (−2 dB)"),
                L("L'aria sale (fronti, bassa pressione, convezione):", "Air rises (fronts, low pressure, convection):"),
                L("mescola i bassi strati e rompe inversioni e condotti.", "it mixes the low levels and breaks inversions and ducts."),
                L("Moler e Holden 1960: in media −2,5 dB rispetto alla subsidenza (un calo di 22 dB).", "Moler & Holden 1960: on average −2.5 dB versus subsidence (one drop of 22 dB)."),
                L("Velocità verticale: ", "Vertical speed: ") + w + L(" (positiva = salita)", " (positive = rising)"),
                L("Manuale delle previsioni, § 9 bis.", "Forecast manual, § 9 bis.") },
            _ => new List<string> {
                L("S Scirocco: indizio FAVOREVOLE (nessuna correzione)", "S Scirocco: GOOD sign (no correction)"),
                L("Aria calda e secca dal Sahara sopra aria marina fresca:", "Warm dry Saharan air over cool sea air:"),
                L("inversioni fortissime sul Mediterraneo (ARRL Handbook).", "very strong inversions over the Mediterranean (ARRL Handbook)."),
                L("Il guadagno lo decidono gli strati del profilo.", "The gain comes from the profile layers."),
                string.Format(ci, L("Vento 850 hPa {0:0} m/s da {1:0}°, T850 {2:0} °C", "Wind 850 hPa {0:0} m/s from {1:0}°, T850 {2:0} °C"), pr.Wind850, pr.Dir850, pr.T850),
                L("Manuale delle previsioni, § 9 bis.", "Forecast manual, § 9 bis.") },
        };
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
                if (double.IsNaN(p)) p = Own.At(la, lo, t)?.Pmsl ?? double.NaN;   // senza il livello isobare si usa la pressione della previsione propria
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
        var corrText = L("Accordo del guadagno con la pressione {0}, con l'inversione {1} ({2} punti, {3:dd/MM HH}:00 UTC, {4:0} MHz)",
                         "Gain agreement with pressure {0}, with inversion {1} ({2} points, {3:dd/MM HH}:00 UTC, {4:0} MHz)", F(rp), F(ri), gp.Count, t, f);
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
    /// <summary>Profilo a terra piatta (dalla 1.3.2, il raggio curva verso il basso) o a terra curva (il terreno sale con la curvatura, raggio dritto).</summary>
    public static bool Flat = true;
    LinkResult _r;
    Point? _hover; int _hoverKey = -1;
    // elementi colorati del disegno, per il fumetto: (centro o punto del segmento, chiave)
    readonly List<(Point p, int key)> _hits = new();
    readonly List<Point> _ray = new();
    readonly List<(double x, double yTop, double yBase)> _band = new();

    static string L(string it, string en, params object[] a) => Lang.Tr(it, en, a);

    /// <summary>Chiave dell'elemento sotto il mouse: 0..n−1 ostacoli, 1000 raggio diretto, 2000 strato, −1 niente.</summary>
    int KeyAt(Point p)
    {
        for (int i = 0; i < _hits.Count; i++)
            if (Math.Abs(_hits[i].p.X - p.X) <= 7 && Math.Abs(_hits[i].p.Y - p.Y) <= 7) return _hits[i].key;
        for (int i = 1; i < _ray.Count; i++)
        {
            var a = _ray[i - 1]; var b = _ray[i];
            double dx = b.X - a.X, dy = b.Y - a.Y, len2 = dx * dx + dy * dy;
            if (len2 <= 0) continue;
            double t = Math.Max(0, Math.Min(1, ((p.X - a.X) * dx + (p.Y - a.Y) * dy) / len2));
            double qx = a.X + t * dx - p.X, qy = a.Y + t * dy - p.Y;
            if (qx * qx + qy * qy <= 25) return 1000;
        }
        // dentro la fascia arancio dello strato
        for (int i = 0; i < _band.Count; i++)
        {
            var bp = _band[i]; double xa = i > 0 ? (_band[i - 1].x + bp.x) / 2 : bp.x - 6, xb = i + 1 < _band.Count ? (_band[i + 1].x + bp.x) / 2 : bp.x + 6;
            if (p.X >= xa && p.X <= xb && p.Y >= Math.Min(bp.yTop, bp.yBase) - 3 && p.Y <= Math.Max(bp.yTop, bp.yBase) + 3) return 2000;
        }
        return -1;
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        var p = e.GetPosition(this);
        int k = KeyAt(p);
        if (k != _hoverKey) { _hoverKey = k; _hover = k >= 0 ? p : (Point?)null; InvalidateVisual(); }
    }

    List<string> TipLines(int key)
    {
        if (_r == null) return null;
        if (key >= 0 && key < _r.Obstacles.Count)
        {
            var ob = _r.Obstacles[key];
            return new List<string> {
                ob.hM < 1 ? L("● Curvatura della terra (sul mare)", "● Earth bulge (over the sea)") : L("● Ostacolo nella 1ª zona di Fresnel", "● Obstacle in the 1st Fresnel zone"),
                string.Format(CultureInfo.CurrentCulture, L("a {0:0.0} km, quota {1:0} m", "at {0:0.0} km, height {1:0} m"), ob.km, ob.hM),
                string.Format(CultureInfo.CurrentCulture, L("spazio libero / raggio F1: {0:0.0} (libero da 0,6 in su)", "clearance / F1 radius: {0:0.0} (clear from 0.6 up)"), ob.ratio),
                string.Format(CultureInfo.CurrentCulture, L("parametro di Fresnel ν {0:0.0} (ITU-R P.526)", "Fresnel parameter ν {0:0.0} (ITU-R P.526)"), ob.nu),
                ob.hM < 1 ? L("Non è un monte: è il mare che sale per la curvatura", "Not a mountain: the sea rising with the curvature")
                          : L("Il punto rosso invade la zona: toglie segnale per diffrazione", "The red dot invades the zone: it costs signal by diffraction"),
                L("Manuale delle previsioni, § 8.", "Forecast manual, § 8.") };
        }
        if (key == 2000)
            return new List<string> {
                _r.LayerTraps ? L("▬ Strato fondante: CONDOTTO (intrappola la banda)", "▬ Base layer: DUCT (traps the band)") : L("▬ Strato fondante: super-rifrattivo", "▬ Base layer: super-refractive"),
                L("strato {0}, continuo da {1:0} a {2:0} km", "layer {0}, continuous from {1:0} to {2:0} km", _r.LayerName, _r.LayerFromKm, _r.LayerToKm),
                L("cima media {0:0} m, base media {1:0} m sul mare", "mean top {0:0} m, mean base {1:0} m a.s.l.", _r.TopMean, _r.BaseMean),
                _r.LayerTraps ? L("Il segnale resta dentro lo strato: perdita vicina allo spazio libero.", "The signal stays inside the layer: loss close to free space.")
                              : L("Piega il raggio verso il basso (guadagno), ma non lo intrappola.", "Bends the ray downward (gain) but does not trap it."),
                L("Arancio più pieno = condotto; più chiaro = solo super-rifrazione.", "Fuller orange = duct; lighter = super-refraction only."),
                L("Dove manca, lo strato non è continuo lungo la tratta.", "Where missing, the layer is not continuous along the path."),
                L("Manuale delle previsioni, § 9.", "Forecast manual, § 9.") };
        if (key == 1000)
            return _r.Los
                ? new List<string> { L("— Raggio diretto verde: in vista ottica", "— Green direct ray: line of sight"), L("Il corrispondente si vede sopra il terreno", "The other station is visible above the terrain"), L("(terra equivalente con il k del GFS).", "(equivalent earth with the GFS k).") }
                : new List<string> {
                    L("— Raggio diretto rosso: NON in vista ottica", "— Red direct ray: NOT line of sight"),
                    Flat ? L("Il raggio (curvo: il profilo è a terra piatta) passa", "The ray (curved: the profile is drawn on flat earth)")
                         : L("Il raggio (dritto: il terreno sale con la curvatura) passa", "The ray (straight: terrain rises with the curvature)"),
                    L("sotto il terreno o sotto l'orizzonte (terra equivalente k).", "goes under the terrain or the horizon (equivalent earth k)."),
                    L("Il segnale arriva per diffrazione, troposcatter o", "The signal arrives by diffraction, troposcatter or"),
                    L("strati e condotti: vedi «Perdita prevista».", "layers and ducts: see «Predicted loss».") ,
                    L("Linea tratteggiata viola: bordo della 1ª zona di Fresnel.", "Dashed violet line: edge of the 1st Fresnel zone."),
                    L("Manuale delle previsioni, § 8.", "Forecast manual, § 8.") };
        return null;
    }
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
            var e = new FormattedText(Lang.Tr("Il profilo del terreno compare qui a calcolo finito.", "The terrain profile appears here when the computation ends."),
                CultureInfo.InvariantCulture, FlowDirection.LeftToRight, Typeface.Default, 11, new SolidColorBrush(Th?.Muted ?? Colors.Gray));
            ctx.DrawText(e, new Point(10, H / 2 - 8));
            return;
        }
        var dist = _r.DistAlong; var h = _r.Terrain; double D = _r.DistKm, ae = _r.K * LinkCalc.Re;
        int n = h.Length;
        // dalla 1.3.2 il profilo è disegnato a terra piatta (le montagne risaltano): è il raggio a curvare verso il basso,
        // di d1·d2/(2·ae) rispetto alla retta fra le antenne (terra equivalente con il k del GFS)
        // con la casella «terra piatta» spenta si torna al disegno classico: terreno e strato sollevati dalla curvatura, raggio dritto
        double Bulge(double dk) => dk * (D - dk) / (2 * ae) * 1000;
        double Lift(double dk) => Flat ? 0 : Bulge(dk);
        double Drop(double dk) => Flat ? Bulge(dk) : 0;
        var y = new double[n];
        double ymax = Math.Max(_r.HtxAsl, _r.HrxAsl);
        for (int i = 0; i < n; i++) { y[i] = h[i] + Lift(dist[i]); ymax = Math.Max(ymax, y[i]); }
        if (!double.IsNaN(_ductTop)) ymax = Math.Max(ymax, _ductTop + Lift(D / 2));
        ymax = ymax * 1.10 + 20;
        double ymin = Flat ? -0.12 * ymax : 0;
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
                double Rd(int i) => Math.Max(x0, Math.Min(x1, _r.RunDist[i]));
                c.BeginFigure(new Point(X(x0), Y(_r.RunTop[0] + Lift(x0))), true);
                for (int i = 0; i < _r.RunDist.Length; i++) c.LineTo(new Point(X(Rd(i)), Y(_r.RunTop[i] + Lift(Rd(i)))));
                c.LineTo(new Point(X(x1), Y(_r.RunTop[_r.RunTop.Length - 1] + Lift(x1))));
                c.LineTo(new Point(X(x1), Y(Math.Max(0, _r.RunBase[_r.RunBase.Length - 1]) + Lift(x1))));
                for (int i = _r.RunDist.Length - 1; i >= 0; i--) c.LineTo(new Point(X(Rd(i)), Y(Math.Max(0, _r.RunBase[i]) + Lift(Rd(i)))));
                c.LineTo(new Point(X(x0), Y(Math.Max(0, _r.RunBase[0]) + Lift(x0))));
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
                    c.LineTo(new Point(X(d1), Y(Math.Max(ymin, _r.HtxAsl + (_r.HrxAsl - _r.HtxAsl) * d1 / D - Drop(d1) - f1))));
                }
                c.LineTo(new Point(X(D), Y(_r.HrxAsl)));
                c.EndFigure(false);
            }
            ctx.DrawGeometry(null, new Pen(new SolidColorBrush(Color.Parse("#7A5AD8")), 1, new DashStyle(new double[] { 3, 3 }, 0)), fz);
            _hits.Clear();
            for (int oi = 0; oi < _r.Obstacles.Count; oi++)
            {
                var ob = _r.Obstacles[oi];
                var cp = new Point(X(ob.km), Y(ob.hM + Lift(ob.km)));
                ctx.DrawEllipse(new SolidColorBrush(Color.Parse("#D64545")), null, cp, 3, 3);
                _hits.Add((cp, oi));
            }
        }
        var ray = new Pen(new SolidColorBrush(_r.Los ? Color.Parse("#1F9D55") : Color.Parse("#D64545")), 1.6);
        // raggio diretto curvo rispetto alla terra piatta; sotto il fondo del disegno resta sul bordo
        _ray.Clear();
        {
            var rg = new StreamGeometry();
            using (var c = rg.Open())
            {
                int k = 120;
                for (int j = 0; j <= k; j++)
                {
                    double dk = D * j / k;
                    var pt = new Point(X(dk), Y(Math.Max(ymin, _r.HtxAsl + (_r.HrxAsl - _r.HtxAsl) * dk / D - Drop(dk))));
                    if (j == 0) c.BeginFigure(pt, false); else c.LineTo(pt);
                    _ray.Add(pt);
                }
                c.EndFigure(false);
            }
            ctx.DrawGeometry(null, ray, rg);
        }
        // fascia dello strato fondante, per il fumetto
        _band.Clear();
        if (_r.RunDist.Length > 0)
            for (int i = 0; i < _r.RunDist.Length; i++)
                {
                    double bd = Math.Max(_r.LayerFromKm, Math.Min(_r.LayerToKm, _r.RunDist[i]));
                    _band.Add((X(bd), Y(_r.RunTop[i] + Lift(bd)), Y(Math.Max(0, _r.RunBase[i]) + Lift(bd))));
                }
        var ft = new FormattedText($"{_r.DistKm:0} km · k {_r.K:0.00}", CultureInfo.InvariantCulture, FlowDirection.LeftToRight, Typeface.Default, 11, new SolidColorBrush(Th?.Ink ?? Colors.Black));
        ctx.DrawText(ft, new Point(10, 4));
        // fumetto dell'elemento sotto il mouse
        if (_hover is Point hp && TipLines(_hoverKey) is List<string> lines)
        {
            var bad = Color.Parse(_hoverKey == 1000 && _r.Los ? "#1F9D55" : "#D64545");
            var tb = new Typeface(FontFamily.Default, FontStyle.Normal, FontWeight.Bold);
            var fts = lines.Select((l, i) => new FormattedText(l, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, i == 0 ? tb : Typeface.Default, 11, new SolidColorBrush(i == 0 ? bad : Th?.Ink ?? Colors.Black))).ToList();
            double w = fts.Max(f => f.Width) + 14, hh = fts.Sum(f => f.Height + 1) + 10;
            double x = hp.X + 12, yy = hp.Y + 10;
            if (x + w > W) x = Math.Max(2, hp.X - w - 12);
            if (yy + hh > H) yy = Math.Max(2, H - hh - 2);
            ctx.DrawRectangle(new SolidColorBrush(Th?.Panel ?? Colors.White), new Pen(new SolidColorBrush(bad), 1.4), new Rect(x, yy, w, hh), 6, 6);
            double ty = yy + 5;
            foreach (var f in fts) { ctx.DrawText(f, new Point(x + 7, ty)); ty += f.Height + 1; }
        }
    }
}
