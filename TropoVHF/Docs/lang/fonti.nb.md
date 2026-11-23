# Kilder og formler

> Automatisk oversettelse av italiensk: i tilfelle tvil er verdt den italienske guiden.

Arbeidsinnsamling av kilder som konsulteres for LIVETROPOVHF tropo-prognosen (20–21. november 2026): Grunnleggende fysikk, tilgjengelige data, publiserte indekser, formler, radioamatøropplevelser, validering og beslutninger. Formlene er nummerert som i LaTeX, (1), (2),... og i teksten de husker med tallet i parentes. Prognosehåndboken beskriver hvordan programmet bruker dem.


## 1. Forfatterens lokaler

IC8TEM observasjoner som rammer arbeidet, med tilbakemeldinger som finnes i kilder.

- Italiensk geografi krever diffraksjon. Med fjellrygger, promontory og øyer et par dusin kilometer fra stasjonene, bør diffraksjonsbidrag beregnes med Fresnel parameter på hovedkanten, ikke bare med det glatte landområdet. Møte: I en sammenligning på uregelmessig terreng ved 563 MHz (Rio de Janeiro, 27 poeng mellom 5 og 30 km) var de beste metodene P.526 med bladkanter, P.1812 og den diffraktive kile, med gjennomsnittlig feil mellom −5 og +3 dB og dispersjon på ca. 9 dB; P.1546 var den mest pessimistiske (fra Costa, Rabanal Ramirez og For bratte skråninger nær PETOOL stasjonen huske at de tjener brede vinkelutbreiere og toveisberegning (bølge reflektertert av relieffene).
- For å komme inn i en fjern kanal, trenger du Fresnel. Hvis kanalen begynner utover horisonten, kommer signalet til oss ved diffraksjon over hindringer til dens begynnelse; fra ICAO beregner programmet det (manual av prognoser, § 10 ter). Møte: Rohde & Schwarz (2023) merker at stasjoner langs en høy kanal ofte ikke klarer å komme inn, mens de som er i nærheten av endene ja.
- Betingelser endres raskt. Teoriene er der, men betingelsene endres i minutter, og ingen prognoser tar dataene i sanntid: GFS kommer ut hver 6. time, med tidsspenn og 0,25°, og en prognoser kan miste verdi innen kort tid. Møte: Bray (QST, November 1961) måling av timelig median svingninger på ca ±7 dB over 100 miles; kanalene beveger seg med atmosfærisk tid (Rohde & Schwarz); vårt QSO register på 29/12/2021–2/2022 viser åpninger voksende og faller innen timer.
- Det beste nå-casting er DX-hopen–PSKReporter-nettverket. For å vite hva som skjer nå slår klyngeflekkene, PSKReporter (og WSPR) ethvert mønster: de er tiltak. Forventningen sier hvor å se ut og hvor lenge det kan vare; reklamer sier om det er. Programmet for dette setter plass og værvarsel på samme kart.

## 2. Formål

Ideen er en tredje indeks, beregnet av LIVETROPOVHF selv som starter fra vertikale profiler av en værmodell, som skal legges til Hepburn og F5LEN i stedet for å erstatte dem. I dag leser programmet sine kart allerede klar: en indeks vil også si * hvorfor* det er åpning (quota og tykkelse av kanalen, minimum band fanget) og det kan forekomme med punktene som programmet samler allerede.

- Hva kan du gi: sannsynlighet for kanal per punkt og i timen, andel av basen og toppen, tykkelse, minimum frekvens fanget, og langs en bane fraksjonen av ruten inne i en kanal.
- Hva kan ikke gi: de tynne kanalene (under noen få ti meter) som de globale modellene ved 0,25° ikke løser, og fordampningskanalene på havet, som tjener fremfor alt fra GHz oppover.
- Første praktiske mål: 144 og 432 MHz i Europa, med verifisering på DX klynge flekker, PSKReporter og lokale dekodinger.

## 3. Grunnleggende fysikk

Alt starter fra brytning N beregnet på hvert profilnivå: kanalen er der den modifiserte reaktivitet M reduseres klatret, det vil si hvor dN/dh faller under −157 N/km ([ITU-R P.453-13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)).

Refraktivitet (P og i HPa, T i kelvin):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Damptrykk fra relativ fuktighet H (%) og temperatur t (°C), over vann mellom −40 og +50 °C:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

Endret reaktivitet (t i km, eller 0.157 · z med z i meter) og kanalkriterium:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN/dh (N/km) | Forskrift | Effekt av 144 MHz |
| 0 | underbrytning | redusert strømningshastighet |
| fra 0 til −79 | normal (standard ca. −40) | radiohorisont med ekvivalent radius 4/3 |
| fra −79 til −157 | super-bryting | Langstrøm, for god |
| under −15 | kanal (trapping) | fanget signaler, DX over 1000 km |

Hvordan ekstrahere N fra en modell på trykknivå: for hvert nivå du tar T, relativ fuktighet og geopotensiell høyde, beregner du N og M, deretter gradienten mellom nabonivåene. Nivåene under bakken bør kastes (ved 1000 hPa på Alpene er under bakken).

Minimum fanget frekvens: kanalen styrer bare bølgene ganske kort i forhold til tykkelsen. Hepburn publiserer en tabelltykkelse → minimal nyttig frekvens ([DX Info Centre](https://www.dxinfocentre.com/propagation/luf.htm); det er en myk grense, under hvilken signalet gradvis faller.

| Led Thickness | Minimum betalt frekvens |
|  61 m  | 962 MHz |
|  91 m  | 464 MHz |
|  122 m  | 276 MHz |
|  152 m  | 185 MHz |
|  183 m  | 133 MHz |
|  213 m  | 101 MHz |
|  305 m  | 53 MHz |

I VHF-området (ofte 120–300 m) følger tabellverdiene ganske godt en lov om konstant; under 100 m undervurderer formelen frekvensen opp til 30–40%. Tilpasse det selv (ikke en formel publisert av Hepburn), med f i MHz og d i meter:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

I praksis: for 2 m tar det en kanal ofte minst 170-80 m, for 70 cm er det nok om lag halvparten. Formlen som sirkulerer på nettet, λmax = 0,014 · d^1,5, kommer fra Kerr (1951, kapittel 1, eq. 18) og er gyldig med λ i centimeter og stående, for en kanal hvor M faller lineært: med disse enhetene gir ca. 2 m 180 m og ca. 70 cm 89 m, det er de samme verdiene i Hepburn-tabellen, som kommer fra det. Kerr advarer om at det ikke er en netto kutt: selv bølger flere ganger lenger lider mye av kanalen, og en måte som - tap - litt kan forbedre mottak selv med stasjoner ut av kanalen. En IEEE Access-studie fra 2019 bekrefter at klassiske metoder (Kerr, Kukusjkin) ikke beskriver det virkelige kuttet ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)): terskelen bør brukes som en indikasjon, ikke som en vegg.

## 4. Data tilgjengelig gratis

Det svake punktet er vertikal oppløsning: mellom 1000 og 850 hPa modeller gir et nivå hver 25-50 hPa, det vil si hver 200-450 m, så de ser godt inversjonene til 500-2000 m, men ikke de tynne kanalene nær bakken. LIVETROPOVHF bruker allerede Open-Meteo til isobar, temperatur til 850 hPa og inversjon: indeksen kan starte fra de samme samtalene.

| Kilde | Hva gir | Oppløsning | Bruk for indeks |
| [Åpne-Meteo, GFS](https://open-meteo.com/en/docs/gfs-api) | T, relativ fuktighet, Duggpunkt, geopotensiell høyde etter nivå (1000, 975, 950, 925, 900, 850...); høyden på grenselaget | 0.25° på trykknivå; 16 dager, timer opp til 120 timer, oppdatert hver 6 timer | profil N og M, gradient, base og toppen av kanalen; allerede i programmet |
| [Open-Meteo, ECMWF](https://open-meteo.com/en/docs/ecmwf-api) | samme nivå fra open-data modeller 0,25°; bakken gir Duggpunkt |  0,25°  | andre mening, nyttig for konkordansen mellom modeller |
| [Open-Meteo, DWD ICON](https://open-meteo.com/en/docs/dwd-api) | T og relativ fuktighet på trykknivå; i ICON- D2 er det 800, 900 og 925 hPa | ICON-EU ca. 7 km | tredje uttalelse om Europa |
| [Radiosund, Univ. Wyoming](https://github.com/NCAR/RAOBget) | målte profiler, to lanseringer per dag (00 og 12 UTC), tekstformat per stasjon | mange betydelige nivåer, selv hvert par dusin meter | indeksverifisering; se de tynne kanalene som modellen ikke ser |
| [GROANS, NOAA NCEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive) | Historisk arkiv av kringkasting, en fil per stasjon, mer avledet parametere | som ovenfor | Kalibrering på års data og sammenligning med tidligere flekker |
| ERA5 (Copernicus) | Oral reanalyse siden 1940, 37 nivå av trykk | ca 28 km | statistikk og kalibrering, ikke prognose |

- Med åpen Meteo geopotentialhøyde er over havet: nivåene som faller under bakken må kastes ([Open-Meteo, øvre luft](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)].
- På bakken tilsettes T og fuktighet på 2 m: de gir den første strekk av gradient, den der nattoverflatekanalene er født på sletten og på havet.
- Italienske sendinger på University of Wyoming arkiv (weather.uwyo.edu/upperair/sounding.html): stasjoner og tall WMO i tabellen nedenfor.

| stasjon | W | Lengdegrad | Longitude | Dele |
| Udine/Campoformido |  16044  |  46,03  |  13,18  |  94 m  |
| Milano/Linate |  16080  |  45,43  |  9,28  |  103 m  |
| Øvelse av havet |  16245  |  41,65  |  12,43  |  12 m  |
| Brindisi |  16320  |  40,65  |  17,95  |  10 m  |
| Trapani/Birgi |  16429  |  37,92  |  12,50  | ca. 14 m |

Tekstformatet av Wyoming gir for hvert nivå trykk, høyde, temperatur, duggpunkt og fuktighet: nok for N og M. Wyoming har ikke et offisielt grensesnitt, skript brukes som [RAOBget of NCAR](https://github.com/NCAR/RAOBget); College of DuPage holder kopier i tekst etter stasjon (for eksempel 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)).](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. Indekser og publiserte metoder

Hepburn og F5LEN starter fra de samme fysiske ingrediensene, men publiserer ikke den komplette algoritmen; de vitenskapelige studiene i 2008-2026 i stedet beskriver metoden trinnvis, og det er derfra at det er verdt å kopiere oppskriften.

| Metode | Starter data | Hva beregnes | Nyttige notater for oss |
| [Hepburn Tropo Index](https://www.dxinfocentre.com/propagation/hti.htm) | rå værmodeller av Canada Meteorological Service (MSC, som skrevet under sine kart; ikke GFS / NAM), kart konstruert med GRADS, proprietære algoritme ([storia](https://www.dxinfocentre.com/propagation/history.htm)) | 0–10 lineær skala av signal krumning: 0 normal, 1–2 super-refrakt, 3–4 enkelte kanaler, 5–6 diffuserte kanaler; negative verdier = sub-refraksjon | den skalaen som skal imiteres, så den tredje indeksen umiddelbart sammenlignes med de to første. |
| [F5LEN, verdensprognose](https://tropo.f5len.org/WW/info.php) | for Europa ARPEGE modell av Météo-France, 7,5 km, flere nivåer mellom 2 m og 1500 m ([note av forfatter](https://tropo.f5len.org/author/f5len/)) | Gjennomsnittlig indeks for utleier, 40 trinn fra 3 timer, 4 beregninger per dag, 28-dagers arkiv ([ny versjon](https://tropo.f5len.org/?p=504)) | bekrefte at det tjener en modell med mange lave nivåer |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf) | 60 ECMWF-modellnivåer, fra bakke til 3000 m | minimum av dN/dz per kolonne: utført under −157 N/km, super-refraksjon under −78,7 N/km; base for fellelaget | klimatologi: Padana Plain det europeiske området med flere rørledninger året rundt; Middelhavet på sommeren nær 100%, om vinteren under 15%; Italia over 30% på sommernetter |
| [Indermuehle og Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861) | GFS 0,25°, 21 trykknivå + felt ved 2 m | M = N + 0,157 z; minimum dM/dz under 3 km, base og toppen av kanalen, dybden av M-underskuddet; under 40 m kassert | verifisert i 7 år på 25 kanaler fra 88 MHz til 2,68 GHz; god kapasitet opp til ca 3 dager på forhånd (AUC ca. 0,74), deretter dråper; GFS bedre enn ERA5 |
| [Hämäläinen et al., IMF 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf) | Harmonie-Arome, 750 m eller 2,5 km, 65 eller 90 nivåer | dM/dh < 0 på modellens profil, sammenligning med en 4 til 59 m stråle | teller mer vertikal enn horisontal oppløsning; ingen modell ser kanalene under 15 m; fuktighet dominerer på temperaturgradienten |

Tredje amatørradiotjeneste allerede aktiv: [TROPOCAST.EU](https://tropocast.eu/)of Artur SQ3A, i 2 m og 70 cm i Europa. Fra prognosesiden ([tropocast.eu/forecast](https://tropocast.eu/forecast)):

- Data: DWD ICON-EU (pass 0.0625°, ca. 7 km), standardkilde; alternativt kanadiske GDPS (0,15°), mindre detaljert.
- Fremgangsmåte: N og M ifølge ITU-R P.453 fra profilen mellom laget nær bakken og 850 hPa; gjenkjenning av lagene, sammenkobling av kanaler til bakken og høye og terskelverdiene til fargene er en riktig kalibrering av forfatteren, ikke publisert.
- Produkter: " komplett" vis 0–3000 m, mer separate visninger " til bakken" og "høy" (for nå bare i kartene som skal integreres); score 0–100 i ni klasser (under 38 bakgrunn, 38–52 marginal, 53–58 svak, 59–65 moderat, 66–72 bra, 73–80 veldig bra, 81–88 høy, 89–95 sterk, 96–100 svært sterk); prognose opp til +72 h i løpet av en time, med animasjon og arkiv.
- Områder: Europa, Polen, Storbritannia, Tyskland, Frankrike, Spania, Italia, Tsjekkia, Kroatia; grensesnitt også på italiensk.
- Ombruk: kart inneholder en API nøkkelløs iframe; det er også et dokumentert API (OpenAPI og JSON skjema). Lisensen er ikke angitt: Før du bruker den som en tredje kilde ved siden av Hepburn og F5LEN, bør du spørre forfatteren.

Forskjellen fra vårt prosjekt: TROPOCAST stopper på 850 hPa (ca 1,5 km), mens de forhøyede kanalene ankommer til 2000 m og videre ( LA0BY case på 22/11/2018), og gir ikke en indeks for band eller rute. Klassene 0–100 kan sammenlignes med indeks 0–10 på de samme dagene.

TROPOCAST-bildestrukturen (fra lenken gitt av forfatteren av programmet): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, hvor n er forutsetningstrinnet. Nettstedet forbyr automatisk uttak til roboter, så jeg kunne ikke lese bildet; også for dette, før du viser disse kartene i LIVETROPOVHF tjener forfatterens tillatelse.

Merkebok: [DJ5AR, kategori](https://dj5ar.de/?cat=174)(Magonza, 21 artikler 2017-2023, 2 m–13 cm). Cases beskrevet: gjennomført flere uker fra Irland (23. mai–2. juni 2023), FT8 til Hviterussland (september 2020), Italia-Tyskland over Alpene og Adriaterhavet (August 2017). Kjenn til tilfelle med "for høy" kanal (omvendt til 1000 m) som du ikke kan bruke fra bakken: en annen bekreftelse på at det er nødvendig å sammenligne andelen av kanalen med stasjonene. Den bruker radiofag som åpningsmål (opptil 1028 km) og vertikale profiler i Paris, Bordeaux og DWD.

Oppsummert er den vanlige oppskriften: vertikal profil → N og M per nivå → lag med dM / dh negativ → base, topp, tykkelse og kraft av kanalen → indeks. Den hemmelige delen av Hepburn er den empiriske kalibreringen på reelle hendelser som vi kan gjøre om med flekker.

## 6. Nyttige formler for programmet

I tillegg til N og M (grunnfysikkseksjonen) brukes disse til å bevege seg fra vertikal til punktnummer og deretter til bane. ITU-formler var blitt rekonstruert av en PDF-lesning delvis dårlig; den 22. november ble gjennomgått på offisielle tekster ([ITU-R P.452-18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453-14, P.526-16, P.372-18) og korrigert i koden: konstant av troposcatter 190 (ikke 190,1) pluss absorpsjon av gasser med ρ = 3 g/m3; α ikke under −3,4 (eq. 55a); gass av kanalene med ρ = 7,5 + 2,5° (eq. 9a); diffraksjon på full sfærisk land (eq. 23–37: gjennomsnittlig land-sea, høydefunksjon med grense 2 + 20 tømmer K, interpolert under den optiske flyt); dampmettasjonstrykk av P.453-14 (eq. 9, i stedet for Magnus' gamle formel. Rumore P.372-18: Tabell 1 og galaktisk tilsvarer; Kunstige støykurver er opp til 250 MHz og galaktikk opp til ca 100 MHz, over programmet forlenger de samme lovene.

1. Gradient mellom to nivåer av modellen (h i km, M i enhet M):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. Konduit styrke og tykkelse: tykkelse d = topp - base (for en jordkanal, base = jord); styrke som M mangel mellom base og topp.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3. Jordradius ekvivalent faktor (P.452-18, eq. 5 og 6a), med ΔN = gjennomsnittlig N dråpe i første km, positiv, i N/km:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

Med ΔN = 40 har du rundt 4/3 k (standard atmosfære); med ΔN som nærmer seg 157 k har en tendens til uendelighet, det vil si at jordradioen blir flat: det er begynnelsen på kanalen.

4. Radiohorisont på høy antenne h meter, med radius som tilsvarer\_e i km:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5. Sannsynlighet for kanalen for breddegrad (P.452-18, ekv. 2), prosentandel av tiden; φ = breddegrad av sentrum av ruten, μ1 og μ4 tar hensyn til hvor langt det er til sjøs:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

Ved 41° N, alle til sjøs (μ1 = μ4 = 1), gir omtrent 11% av tiden: er den klimatologiske base, nyttig som en referanseverdi som den forventede indeksen er forskjellig fra.

6. Tap for troposcatter (P.452-18, ekv. 45; f i MHz, d i km, θ vinkel av banen i mrad, N0 bakke reaktivitet i sentrum av ruten):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

Begrepet 0,15 · N0 viser hvorfor våt luft også hjelper uten kanal: 50 ekstra N-enheter er 7,5 dB mindre enn tap. LIVETROPOVHF bruker allerede G3YGF -versjonen av strø tap.

7. Tap i kanalen (P.452-18, eq. 46–51): Fast tap av kobling pluss en demping for vinkel-milliradiant, som vokser med den kubiske roten av frekvensen (f i GHz, a__e i km):

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. Fra Bean og Dutton (1966) og Kerr (1951)

De to klassiske tekstene gir tre ting som er nødvendig umiddelbart til indeksen: gradientgrenser tar allerede på samme trykknivå av GFS, hva er verdt i dB en endring av reaktivitet, og hvilke værsituasjoner som skaper eller ødelegger kanalene. *Radio Meteorologi*, NBS Monograph 92.

1. Profilklassifikasjon for trykklag (Bean og Dutton, tab. 6.5, s. 248; −dN/dh i N/km). Designet for radiobølgelengder, gjelder det som det er på modellnivå:

| Veien | Deler om | Underbrytning | Ikke laminert | Super-refraksjon |
| 1000-850 hPa | 0–1,46 km | −dN/dh < 0 | fra 20 til 60 | Over 100 |
| 850–700 hPa | 1,46–3,01 km | −dN/dh < 0 | fra 20 til 50 | over 80 |
| 700–600 hPa | 3,01–4,20 km | −dN/dh < 0 | fra 15 til 40 | over 70 |
| 600-500 | 4,20–5.57 km | −dN/dh < 0 | fra 10 til 30 | over 50 |

2. Hvor mye er verdt i dB (Bean og Dutton, cap. 6, s. 233–258), måler fra 70 til 180 MHz på ruter utenfor horisonten:

- Et hvilket som helst lag (på bakken eller i høyden) sammenlignet med "smooth"-profilen øker signalet på 10–25 dB til alle prosenter; den større gevinsten kommer fra de høye vippede lagene, sannsynligvis på grunn av fokuseringseffekt (kurs 200 km).
- Jordens reaktivitet Ns og nedgangen i det første km ΔN er relatert til det månedlige mediansignal med koeffisient mellom 0,4 og 0,95, median 0,8 (20 ruter fra 130 til 446 km, ca. 100 MHz).
- Årssyklus: 0,18 dB per enhet N; daglig syklus fra 0,2 til 1,1 dB per enhet N, maksimalt på ruter på 175–200 km og vinter.
- Forbindelsesparameteren til Misme anslår feltvariasjonen som 0,5 dB/(N/km) · (ekvivalent gradient − 40) pluss et termisk stabilitetsbegrep; parameteren for turbulens D (område mellom den virkelige profil og den adiabatiske en, 0–3 km) forbedres bare sammenlignet med den enkle ΔN. Konklusjon av forfatterne: ΔN er den beste parameteren for enkelhet.

3. Referanseprofil og anomali (Bean og Dutton, § 3.8 og 3.9). Den eksponentielle atmosfæren i referanse CRPL gir den "normale" profilen som starter fra Ns; A-enheten måler hvor mye den virkelige profilen skiller seg på hver aksje. Det er den naturlige måten å forvandle en GFS-profil til en anomali å sette på kartet:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

Med Ns = 313 har du ΔN ca. −42 N/km, det er den tilsvarende radius 4/3. N/km kanalkriteriet er også oppnådd her ved Snell-loven for en horisontal radius (eks. 4,23), nesten uavhengig av antennehøyden.

4. Hvor og når kanalene er født (Bean og Dutton § 4.4; Kerr cap. 3):

| Prosess | Situasjon | Effekt |
| Advarsel | varm og tørr luft fra land som flyter på kaldere sjø (typisk om sommeren erme etter dager med godt vær); hovedsakelig teller fuktighetsunderskuddet | intens kanal på havet |
| Nocturnal befruktning | rolig himmel, svak vind; lave skyer eller mye fuktighet hindre det | leder til bakken som er født ved solnedgang, vokser om kvelden og forsvinner raskt etter soloppgang, spesielt inne |
| Understøttelse | luft som nedstammer fra antisyklonen, oppvarmet og tørt, over frisk og fuktig luft; hyppige oversvømmelser i fjellene og sørøstlige sektorer av antisykloner | super-fraktivt lag eller høy kanal; nesten alltid gradient høyere enn standard i omvendt |
| Hav og land | kyst, sirkulasjon på noen hundre meter | lokale rørledninger langs kysten |
| Mot kanalen | blanding: jord oppvarmet av solen mellom morgen og ettermiddag, vind til bakken over noen få m/s i grov terreng; natt tåke | Sannsynligheten for ledning nesten ingenting |

5. To nye ingredienser å prøve. Den vertikale bevegelse: stigende strøm i konvergenssonene fjerner og sletter lagene, underlaget styrker signalet (Moler og Holden: i snitt ca. 2,5 dB mer enn med oppbakkeluft, på havet i bånd L og X). Open-Meteo gir allerede vertikal hastighet på trykknivå. Og trykktendensen: på en 140 km bane på 186 MHz gikk signalet ned når trykket falt til senderen og til sentrum av banen og returnerte høy og uten avansasjon når det gikk opp, med bare 2 (Bean og Dutton, fig. 6,9, s. 241).

6. fra Kerr. I tillegg til formelen til den maksimale bølgelengde (basefysikk-seksjonen) gir Kerr en omtrentlig formel for M fra den eneste temperatur, damp og høyde (§ 3,9, med de gamle 79 og 4800 konstantene deretter erstattet av 77,6 og 4810) og observerer at til et omvendt vindskjær kan det generere bølger som svinger i høyden kanalen, mulig årsak til dype flukter (§ 3,26). Italiensk kilde rapportert av Bean og Dutton og å søke: L. Bonavoglia (december 1958), "Correlasjon mellom meteorologiske fenomener og utbredelse..."

## 8. Fra amatørradiopresentasjoner og tekniske rapporter (andre serie)

7 dokumenter gitt av forfatteren av programmet. Tre tjener direkte til indeksen: W3SZ for troposcatter-gulv- LA0BY for tilfeller som Hepburn ikke hadde forutsagt, Hepburn (translation LU7MC) for værklassifisering av kanalene.

1. Troposcatter: signalet som alltid eksisterer. R. Rehr W3SZ, "Tropospheric Scatter (Troposcatter) Utbreiing for VHF, UHF, Mikrobølgeovn og Frekvenser", Pack Rats Conference 2014; J. R. Duffey KK6MC, " Troposcatter utbreiningsmodus og hvordan man forutsier dekk på VHF/UHF Bands", CSVHF 2017.

- Nyttig fra ca 100 til 700 km, fra 144 MHz til 10 GHz; tapet vokser på 30 logs f med frekvens.
- Fremfor alt teller det startvinkelen mot horisonten: hver ekstra grad koster ca. 9-12 dB ( M1CNK, Collins metode).
- Om sommeren er signalet i gjennomsnitt ca. 10 dB høyere enn om vinteren; om ettermiddagen ca. 5 dB lavere enn om morgenen eller om kvelden. KK6MC indikerer en total variasjon på 13–15 dB med tid, sesong og tid.
- Yeh-metoden, den letteste å programmere (f i MHz, d i miles, θ-vinkel i grader, α-geometrisk gjennomsnitt av åpningene av de to buntene):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Sammenligning av Larsen (1968) på 15 ekte forbindelser: gjennomsnittlig firkantet feil 5,1 dB for Collins, 7,4 for Yeh, 8.1 for NBS og CCIR, 16,8 for Rider. Alle metoder forbedres hvis Ns holder seg steady (Yeh faller til 5,4 dB): for scatter brytning av dagen teller lite, så tropoindeksen må se på lagene, ikke bare Ns. Tabellen over 15 forbindelser med målte tap (inkludert Feldberg-Monte Baldo, 322 km ved 957 MHz, 223,1 dB; Kypros-Israel, 319 km ved 77,5 MHz, 163,3 dB), er en god testbenk for vår beregning.
- KK6MC gir tapet som en typisk stasjon klarer å overstige 2 m og den tilsvarende avstand, fra 184 dB (FT-817 og 3 elementer i SSB, ca. 130 km) til 215 dB (lange bunker i SSB, ca. 430 km) og 236 dB (kW og yagi koplet, ca. 640 km). Det er måten å oversette indeks dBs til miles tjent.
- Klar kode: W3SZ Yeh og Collins program (nitehawk.com/w3sz/YehCollins.zip) og Bob Atkins KA1GT kalkulator (bobatkins.com/radio/troposcatter.html).

En annen kilde på scatteren: I. Roberts ZS6BTE, "Tropospheric Scatter Expansion", *Radio ZS*, desember 1984, med notat fra 2013 ([qsl.net/zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). Hovedsaklig Nyttig geometri: Scatterens vinkel θ = θ0 − θ1 − θ2, med θ0 = d/R, θ1 = (h1 − h\_ostacolo)/d1 + d1/2 R og det samme for den andre stasjonen, R = 8446 km (4/3 av jordradiusen). Det er stykket som brukes til å beregne vinkler fra profilen til bakken i troposcatter-gulvet - Andre data: hver vinkel i mer kostnader ca. 10 dB; passering fra Ns 280 til 300 reduserer tapet av ca. 4 dB; strøvolumet er mellom 300 og 2000 m i 150 km, mellom 600 og 3000 m i 300 km; Tid i strøvolumet øker signalet på ca. 20 dB. Det numeriske eksemplet på artikkelen returnerer ikke med de angitte formlene (tid for fri rom på 900 MHz og 872 km er ca. 150 dB, ikke 125), så formelene bør kontrolleres på W3SZ før bruk av dem.

2. Typer av kanal (W. R. Hepburn, "Tropopheric utbreiing Modes", dxinfocenter.com/propagation/tr-modes.htm, spansk oversettelse av LU7MC). Fullfør tabellen av Bean og Dutton med nyttige tilfeller i Italia:

| Type | Når | Merknad til indeks |
| Irradisjon | Stille og fredelige netter på jorden | Følg bakken, forsvinner kort tid etter daggry |
| Høyt trykk (subsidens) | anticyklon | kan vare hele dagen; om natten kan kanalen fra bestråling nedenfor blokkere fjerne signaler, så dagtid kan gå bedre |
| Front | foran en varm front, bak en kald front, nord for en stasjonær front | det dårlige været forstyrrer ham |
| Fjellnedstigning | föhn, bora: luft kommer ned, oppvarming og tørking | å prøve å bora på Adriaterhavet og föhn sør for Alpene |
| Advarsel | varm og tørr luft over kald og våt jord, for eksempel etter regn | ført til bakken |
| Valley | varm luft over kaldt luft stopper i dalen | Kan bo på dagen |
| Marino | varm og tørr luft på kaldere vann; kommer på kysten med brisen | topp på ettermiddagen; vår og tidlig sommer de beste sesongene |

Fordampingskanalene på havet (0–30 m) tjener bare ca 3 GHz og ikke går inn i hinterland: for 144 og 432 MHz kan du ignorere. De høye fjellene er en barriere; ørkenene er for tørre.

3. Hepburn hadde ikke forutsagt (S. Heck LA0BY, " Ekstreme Tropo-utbreiing på 144 MHz og oppover", Nordic VUSHF 2019, fra JO59IX til 500 m høyde):

- 22. november 2018: 70 cm opp til UA6 (ca. 2600 km) i 6-8 timer. Hepburn antydet bare de baltiske landene og Polen. Kanalen ble sett i temperaturkartene på 900 og 1500 m og endte like over 2000 m; den var høyere på norsk side, og bare LA0BY på åsen kom inn. På bakken syntes ikke som en trope: kald og tåke på toppen.
- 11. februar 2015: 2 m og 70 cm mot 9A og S5 (ca. 1500 km) gjennom Alpene, også dette ikke forventet av Hepburn.
- 15. oktober 2011: 2 m med EA1 (2135 km) i høykanal, på tre havstrekninger og tre bakker.
- Andre hendelser: 6–11. november 2003 i øst (2050 km), 10 desember 2004 i Frankrike.
- Feltadresser: tåke i slette, gratis topper, lite vind, våt jord. Normal temperatur gradient 6,5 ° C / km; i et ekte tilfelle ( VK3KAQ) +7 ° C i 20 m.

Forfatterens konklusjon: Hepburn og F5LEN er ikke nok, de tjener profilene i høyde langs ruten. For oss: Disse datoene er tilfeller av verifisering som skal reberegnes med de historiske dataene til modellene, og indeksen på banen må sammenligne kvoten av kanalen med kvoten til stasjonene.

4. Italiensk erfaring (G. IV3GBO, " Typer av VHF utbredelse SSB/CW", 2017): utført mellom 450 og 3000 km, med lag mellom 450 og 3000 m; tåke utvidet som et spor; åpninger til sjøs på Adriaterhavet mot Puglia, Albania og Hellas. Spredning fra 100 til 500 km.

5. Introduksjoner (P. Wilton M1CNK, " En introduksjon til VHF/UHF-utbredelse", 2013): 70 cm er ofte bedre enn 2 m; med 10 m pol ved 144 MHz er startvinkelen ca. 3°, med 20 m ca. 1,5°.

6. NASA-rapport (E. Hirschmann, "Tropospheric og Ionospheric Effects on Radio frekvens (VHF-SHF) Kommunikasjon", Goddard X-731-67-89, 1967): det gjelder avstandsfeil til satellitter, ikke DX. Den bringer tilbake den samme N-formelen og eksponentiell profil (eksempel i Florida: Ns 370, ΔN −55, konstant 0.161/km) og sender to NBS-kilder til søk: Bean og Thayer, CRPL Exponential Reference Atmosphere - Monograph 4 (1959) og Bean, Horn og Ozanich, "Klimatiske diagrammer og data fra Radio Refractive Index for USA og verden", Monograph 22 (1960).

## 9. Fra VHF/UHF DX-boken, kapittel 2

I. Hvit G3SEK (redigert av), *VHF/UHF DX-bok*, DIR Publishing 1995 (ISBN 0 9520468 0 6; digital kopi 2017), kapittel 2 "VHF/UHF-utbredelse", pp. 2-13, 2-22–2-29 og 4-11. Det er den nærmeste teksten til radio amatør erfaring blant de som samles; bekrefter terskelverdiene som allerede brukes og legger til nye synoptiske regler.

Antall

- Den samme formel av N (77,6 P/T + 3.73·10^5 e/T2); eksempel: 1000 hPa, 7 °C og = 10 hPa skade N = 324. I en velformet atmosfære, for eksempel etter en tordenstorm, er N nesten lineær på rundt 39 enheter/km i den første 2-3 km.
- Gradienter mellom 0 og −79 N/km er normale, under −79 er det super-brytning, over 0 underbrytning. Boken insisterer på at den lange DX ikke kommer fra den enkle super-brytningen, som bør forbli rett på hele banen, men fra kanalene.
- Minimum kanaltykkelse, litt mer forsiktig enn Kerr og Hepburn:

| Band | Minimum tykkelse |
| 50 MHz |  400 m  |
| 70 MHz |  300 m  |
| 144 MHz |  200 m  |
| 432 MHz |  100 m  |
| 1.3 GHz |  50 m  |

- Forholdet er ikke proporsjonalt med bølgelengden (ca. 65 λ ved 50 MHz, 220 λ ved 1,3 GHz) fordi kanalens kanter er avsatt I Storbritannia er rørledningene hyppige på 144 MHz, men nesten aldri på 70 MHz eller under, og ofte går 432 MHz sterk når 144 er svak.
- I rørledningen faller signalet som 1/d i stedet for 1/d2: opp til 1000 km kan dempingen være lik den fri plass.
- Kapasiteten til en minstestasjon på 2 m (100 W, 15 dBi yagi, 2 dB støy): 217 dB, det vil si ca. 400–500 km i SSB i gjennomsnittsforhold; i CW ca. 20 dB i mer (s. 4-7). Enig med KK6MC -tabellen.
- Scatter: Tapet stiger med ca. 15 dB ved å tredoble frekvensen og 10 dB for hver grad av scattervinkel (Yeh formel; i teksten ekstrahert konstanten er uleselig, så det må tas fra versjonen av W3SZ. Scatter fra konfektive celler dekker rutinemessig opp til 500 km, 1000 km med gode stasjoner. Etter kraftig regn og lengre forhold er knappe på alle bånd.

Typer av kanal

- På jorden sprer de seg dårlig fordi jorden absorberer og sprer seg; på det rolige havet kommer langt (Scilly-Kanarie, 2500 km, om lag en gang i året på 2 m). Fordampningskanalene er ofte bare ca. 15 m høye og serverer mikrobølgeovner.
- Høy: mellom 450 og 2000 m på grunn av den lengre DX på bakken; signalet " høy" over stasjonene nedenfor. Kanalene gir mindre sterke signaler, men tillater også mellomliggende avstandsstasjoner.
- Kanalene har nøyaktige retninger og kan vare timer eller dager, i motsetning til sporaden E.

Nye Synoptiske regler for indeks

- Age av anticyklon: luften faller til sentrum av noen meter i timen og reversen senkes etter hvert som det høye trykket utvikles; de større åpningene kommer etter flere dager med utbreiing allerede god. En kortsiktig anticyklon er ikke nok.
- Høytrykkskant: laget er kuppel og i kantene er lavere, så stasjonene blir bedre.
- Retning: Det har blitt observert at de lengre rutene kjører langs isobarene eller gjennom dorsalene (Flavell G3LTP, 1984), men det er utmerkede forbindelser også i radiell retning fra sentrum. Eksempel på 29–30. august 1987: fra England roterte åpningen 180° på to dager (Spania, Frankrike, Italia, Østerrike, til Norge) etter skiftet av antisyklonen.
- Mot: tykk isobar (sterkt vind), stratocumuliforme skyer i kanten av laget (begrense den høyeste frekvensen).
- Fronter: En kald front skyver varm luft opp og skaper en omvendt langs fronten; den beste retningen er langs fronten mens den passerer, men det varer lite.
- Sesong: I Storbritannia er antisykloner hyppigere i slutten av sommeren og tidlig høst.
- Visuelle ledetråder: røyk fanget i en mørk linje over horisonten; med høyt trykk indikerer lagkumler med base under 1000 m en bakkekanal, base over 1000 m en høy kanal.

Referanser sitert til søk: R. G. Flavell G3LTP, " Studier av en omfattende antisyklonisk utbreiing hendelse...", * Radiokommunikasjon*, februar 1984, s. 128; J. Gannaway G3YGF, "Tropopheric Scatter Utbredelse", * Radiokommunikasjon*, september 1981, s. 710; P. W. Sollom G3BGL, "A Little Flutter W6ZGN

## 10. En verifisert amatørradiomodell: Biancomano and Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, "Praktisk troposfærisk modell for VHF/UHF-utbredelse Predict * National Weather Digest* 22:2, juni 1998, s. 15–24, gratis i [PDF fra National Weather Association](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf).

- Data: Radiosundasjoner på 00 og 12 UTC på 47 stasjoner i østlige USA, 1995–1997; bakkeanalyse til 700 hPa (i begynnelsen til 400), på profilen til N, uten M. Utelukkende kyster, øyer og fjell.
- For hver undersøkelse: tilstedeværelse og kvote av kanalen kan maksimal bølgelengde som kan lede (ofte ca. 100 ganger bølgelengden), maksimal gradient av den potensielle temperaturen mellom nabonivåer og mellom ett nivå og det andre over (for tykkere lag). Bakkekanalene brukes ikke.
- Første regel, utholdenhet: stabilitetsindekser (løftet indeks, K, total totaler, SWEAT) forblir innen 3 enheter fra undersøkelsen 12 timer før, og trykknivået der den potensielle temperaturen vokser mer forblir omtrent det samme. I så fall: forbedringsprognose de neste 12 timene.
- Andre regel, dynamiske kontroller: begrense lag som ikke stiger, CAPE, variasjon av tykkelsen til 500 hPa på tre undersøkelser, variasjon av skråningen innenfor grenser.
- Bandgrense: en kanal som kan bære 100 cm eller mer indikerer åpning på 432 MHz, nesten sikker på 222 og ofte selv på 144.
- Romfilter: stasjoner på et sekskantet rutenett på rundt 400–500 km; prognosen er bare verdt hvis minst 5 contigue stasjoner er positive (hvis du kan hoppe over en). Med 5 stasjoner blir sannsynligheten for et falskt tilfeldig positivt ubetydelig.
- Verifisering mot hendelsene i kolonnen "The World Over 50 MHz" av QST: i 1996, 48 hendelser planlagt på 67 (71 %); 12 av de 15 store hendelsene. Nøyaktighet opp til 70% på 432 MHz og ca. 60% på 144 MHz ved 12 timer; det gir nesten dobbel den faktiske hendelse; 9 feil ut av 19 er spådommer ankommet sent, fordi mange forbedringer dannes på mindre enn 12 timer.
- Lagt til stasjonene lenger nord: vind roterende med klokka klatre (varm start) som ekstra tilstand.

Fra den komplette PDF (oppgis av forfatteren av programmet) tre detaljer til. Deres regel " tykkelse = 100 ganger bølgelengde" gir 600 m i 6 m, 200 m i 2 m, 125 m i 1,25 m og 70 m i 70 cm (fig. 3): for 70 cm er mindre alvorlig enn Kerr og Hepburn (ca. 89 m), for de 2 m det sammenfaller med VHF/UHF DX-boken. Vinden som roterer med klokken går opp, ifølge forfatterne, spesielt fordi den bringer varmere luft i høyden, som er det som får N-fallet til å vokse med trykk; fuktigheten som bringer med det synes ikke å destabilisere. Appendikset oppnår dN/dh i lukket form for en konstant termisk gradientatmosfære med et skarpt sprang av varmegradient og fuktighet ved en gitt kvote: nyttig som et prøvetilfelle for vår kode. Deres spor av skråningen av den potensielle temperaturen kommer fra K. H. Jehn, " Bruken av potensiell brytningsindeks i synoptisk skala radio meteorologi", *J. Meteorologi* 17, 1960, s. 266.

Hva du skal ta for vår indeks: (1) en utholdenhetsperiode: Sammenlign profilen med profilen på 12 timer før, at i timeprognosen har vi gratis; (2) romfilter: et punkt teller bare hvis forbedringen dekker et område på noen hundre km, som reduserer falske alarmer; (3) stabilitetsindekser, beregnet fra temperatur og dugg til 850, 700 og 500. HPa (Open-Meteo allerede gir løftet indeks og CAPE); (4) vinden som roterer med høyden mellom 1000 og 850.

En annen kilde i samme pakke, til liten bruk for oss: P. K. Rastogi, "Stratosfæren og troposfæren (S-T) Studier ved Millstone Hill - NASA 1984 (dokument 19840019069): 440 MHz radar som måler turbulensen til klar luft mellom 4 og 25 km, med tynne lag knyttet til gravitasjonsbølger og vindskjæring. Det bekrefter at turbulensen som produserer scatteren er lagdelt, men det gjelder for høye odds for kanalene.

## 11. Tredje runde av kilder (20. november)

Høy kanal historiske tilfeller. J. Macoun OK1VR, 2 m fra toppen av Sněžka (1602 m), høst 1958, artikkel i 1959 oversatt i 2006 ([ok2kkw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)). 27.–28. oktober 1958 første Tsjekkoslovakia-England-forbindelse på 145 MHz ( G5YV, 1220 km) og mange flere mellom 1100 og 1250 km, med stabile signaler etter 23. Forfatteren tilskriver det til to superimponerte inversjoner, den øvre rundt 2200 m, med signalet tilbake til bakken kun der det nedre laget oppløst; tilstanden favorisert av en langsom anticyklon fra Atlanterhavet til kontinentet. Fra hjemmet (Prague) var maksimum 530 km: bare stasjonene i høyden kom inn i kanalen. Det er et annet tilfelle av verifisering (for eksempel LA0BY 2018) og bekrefter at indeksen bør se etter utført over 2000 m og sammenligne den med stasjonsandelen.

Historiske biblioteker NBS (nyttig å se etter klassiske verker, ikke for formler):

- W. Nupen, *Bibliografi om troposfærisk utbreiing av radiobølger*, NBS Teknisk Note 304, 1965, 356 sider, med indeks for emner (også " betingelsesprognose") og etter land. Italienske oppføringer rapportert: L. Sacco, " Radioforbindelsen i atmosfærisk super-refrakt regime", * Høy frekvens* 24(6), desember 1955 (italiensk forskning: super-fraktion under 500 m hyppig dag og natt); S. Silleni, *Annali di Geofisica* 8.1, 1955; J. M. Clara e A. Antinori, prova tra Sardegna e Minorca da 238 a 297 MHz (390 km), *Elektrisk kommunikasjon* 33(2), 1956. A. F. Clarke (Fort Huachuca, 1956) utstedt daglig prognosen for maksimal bølgelengde fanget for følgende morgen, fra fuktighet, temperatur, trykk og vindprognoser.
- A. L. Abbott, * Bibliografi av Tropospheric Radio Wave Scattering*, NBS, materiale til januar 1960, ca. 670 elementer bestilt per år: nyttig for den troposcatter delen.

Avsløring og observasjoner: F. W. Odenthal DK5EQ, "Ausbreitung 2m – Eine Einführung" (31/05/2024): nesten optisk opp til 200 km, tropo per kanal eller refraksjon utover, med høyt trykk og om våren og høsten; citerer VHF-siden til DM2HB og notatene til DK5YA på tropo og Es. A. Johnson VK3DXE på bloggen til EI7GL (20. desember 2021): med WSPR VHF stier på tusenvis av km i Australia har blitt oppdaget; Trope er ikke bare sommer. Diskusjon om VHF marina og AIS ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)]: omfang 1,23·h(ft) nautiske mil per antenne, scatter opp til ca 50 nm, typisk trope forbedring 100–400 nm; kanalene er anerkjent fordi de fjerne stasjonene kommer så sterk som naboene. Chelmsford ARS News, desember 1983: i konkurransene i 2 m stasjoner på den engelske østkysten favoriseres av sjøveien til kontinentet.

ARRL, * Radio Amatørens VHF-håndbok*, 1972, cap. 2 "Propagation Beyond the Horizon" (pp. 17–19). Allerede kjente driftstips (følger høytrykksområder som beveger seg sakte og fremfor alle sine bakkanter; tidlig høst den beste perioden; kystområder, store innsjøer og daler i elvene favorisert ved stratifisering). To nye data: (1) fig. 2-4 sammenligner standardatmosfæren (relativ fuktighet 70 % opp til 12000 ft, minimum brytning) med en typisk radius av sterk brytning, med en klar temperaturbrudd og blandingsforhold på ca. 4000 ft (ca. 1200 m): det er signaturen som vår beregning må gjenkjenne på GFS-nivåene; (2) de sanne kanaler er sjeldne under 144 MHz og hyppigst i UHF, ifølge de minste tykkelser. Nyttig QST bibliografi til søk: Hull, "Air-Mass Betingelser og Bending av UHF Waves" (juni 1935, mai 1937); Hoisington, "painless prediksjon av to-målers åpninger" (oktober 1949); Collier, "opper-Air Betingelser for 2-meters DX" (september 1955); Botts, "En natt å huske" (januar 1970, utført på land).

PA0EHG, side "Tropo" ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): sammendrag av troposcatter fra artikkelen i G3YGF, allerede i manualen. Bruk to praktiske regler for å styre vår scatter-beregning: ca. 10 dB ekstra tap for hver vinkel i horisonten til hver stasjon, og over 100 km ca. 9 dB pluss hver 100 km rute (60 miles); det vanlige volumet starter fra ca. 600 m på 100 km og fra ca. 9 km på 480 km. Det behandler ikke kanaler, meningsmålinger eller brytningsgrenser.

PA0EHG, PROPBER kalkulator ([side](https://pa0ehg.com/propagat1.htm), propber.exe program gitt av forfatteren av programmet). Det er et 36 kB DOS program skrevet i BASIC og kompilert med Microsoft BASIC Compiler 5.60 (1985): det kjører bare under DOS eller DOSBox. Fra de interne tekster: ber om frekvens, avstand (eller de to lokatorer, også i det gamle QRA-formatet "E-lokalisator") for hver antennehøydestasjon, støyfigur, FI-bånd, kraft, en hindring (høyde og avstand) og antennege (0, = illustrasjonsdiameterberegning, effektivitet 50%). Returnerer maksimal avstand i optikken, det riktige optiske tap for absorpsjon av damp (0,1 dB/km fra 24 GHz), tap av troposcatter " korrekt for stedene" (dvs. for horisontvinkler gitt av hindringer) og S/N-forholdet i de to sanser, også " med maksimal unormal utbredelse". Numeriske konstanter er ikke lesbare i den kompilerte kjørbare. Den foreslåtte fremgangsmåte for høye bånd er også nyttig for oss: å kalibrere beregningen på en målt kobling (differanse mellom målt og beregnet S/N) og påføre den samme forskjell til et annet bånd. Det er det samme mønsteret i vår beregning, men det bruker S RTM1 -profilen i stedet for bare ett hinder og GFS-prognosen i stedet for en fast margin.

PROPBER, konstanter som leses i det kjørbare (nummer i Microsoft Binary Format-format). πr, i datatabellen ved siden av teksten: 8.68589 (= 20/ln 10, dvs. 20 logger beregnet med den naturlige logaritmen), 32,5 (gratis plass: 32,5 + 20 log f\[MHz\] + 20 log d\[km]), 24000 og 0,1 pluss 40000 og 0,3 (damp: 0,1 dB/km fra 24 GHz, 0,3 dB/km fra 40 GHz) Sannsynlig, fordi du gjentar men inne i koden: −69,5 (samme konstant begrep som Yehs sidescatter formel i VHF/UHF DX bok), 332 (olje brytning), 8733 km (ekvivalent radius, k≈ 1,37). Den eksakte scatter formelen er ikke rekonstruerbar uten å demontere programmet. T0

PROPBER, formler rekonstruert ved koden (demontering av den sammensatte delen; operasjonen kalles til BASIC-biblioteket med operatørene i minnet, hvis verdier i dobbelt presisjon er dekodet). Konstantene −69,5, 332 og 8733 som ble funnet tidligere var falske positive i biblioteket.

- Optisk avstand: d = 5 · (√h1 + √h2) km (h i m). Den er mer sjenerøs enn den klassiske 4,12 (k = 4/3): er ekvivalent med k ≈ 2.
- Tap i optikk: L = 32,5 + 20 log f\[MHz] + 20 log d\[km], pluss 0,1 dB/km over 24 GHz og 0,3 dB/km over 40 GHz.
- Troposcatter: Lts = L + 23 + 10 log f + 0,0674·d, dvs. 55,5 + 30 log f + 20 log d + 0,0674·d (formula av G3YGF med konstant 2 dB høyere). Ingen vinkelbegrep eller reaktivitet.
- Korreksjon " for stedene": programmet ber om hinder og avstand, men blokken som skal bruke dem er tom; korreksjonsvariabelen forblir 0, så det "korrigerte" tapet er lik strø.
- Støy: T = 290·(10^(NF/10) − 1), N = 228,6 + 10 log T + 10 log B (dBW), uten antennetemperatur.
- S/N = 10 logger P + G__tx − L + G__rx − N. " Med maksimal unormal utbredelse uses bruker optisk tap i stedet for strøing, dvs. det ideelle kanalfall uten tap utover fri plass.

Sammenligning: 300 km og 144 MHz PROPBER gir ca. 189,9 dB, G3YGF 187.9, vår P.452 187.8 (med null horisontvinkler): scatteren sammenfaller innenfor 2 dB. Vår beregning legger til horisontvinklene fra profilen, antennetemperaturen og i stedet for detmaximummaksimum" fast det forventede laget med rest paring og demping.

Rohde & Schwarz, P. Denisowski, " Forståelig VHF-utbreiing", pedagogisk notat, april 2023 (24 sider, formidling). Bekrefter punktene som allerede er samlet (ledende som en todimensjonal bølgeleder, signal som faller på en lineær måte med avstanden, opp til 1500 km og videre, varighet fra timer til dager; god bakke på havet og knapp på land; hevet fra noen hundre meter til noen få km). To nyttige observasjoner for indeksen: (1) evolusjon i tide: en nydannet kanal bærer ofte bare de høyeste frekvensene og, fortykkelse, begynner å bringe selv den laveste; timevarselet bør derfor vise 432 MHz " på forhånd" på 144 MHz når laget vokser; (2) stasjoner plassering: de beste forholdene er mellom stasjonene nær endene av kanalen, mens de langs banen til en høy kanal ofte mislykkes å komme inn, med mindre utført "som mister". Kanalene beveger seg med atmosfærisk vær.

Site PA0EHG, andre sider. For tropoen, PathProfile av M. Willis G0MJW ([scheda](https://pa0ehg.com/srtmp.htm), [nedlast](http://www.mike-willis.com/Software/ASTERPathprofile.zip)): profil av ruten fra forventet total tap og kvotefiler, bedømt pålitelig for troposcatter i mikrobølgeovn; behandler ikke strø fra fly eller regn. Nyttig som andre referanse til å sammenligne våre S RTM1 -profiler og tap. Resten av stedet dekker EME, mikrobølgeovn, flystripe (AirScout, SM7LCB -kart) og tiltak i 80 m.

PathProfile by G0MJW (Mike Willis), ASTER og SRTM-pakker og SiteData (oppført av forfatteren av programmet; [](https://www.mike-willis.com/software.html)programvareside ]. Windows-programmet 2018: profil fra ASTER eller SRTM til 1" eller 3", tap i optikk, diffraksjon, troposcatter (gyldig opp til 1000 km), kanal og gassformig, med prosentandel av tiden, k-median av ruten, fraksjonen av banen på havet og avstand fra kysten: strukturen er den til P.452/P.1812, men hjelpen (komprimert.chm-fil) kan ikke leses her. SiteData konverterer lokatorer og gir andelen fra GT OPO30.

Den mest nyttige delen er datafilene som er inkludert: ITU-R P.453 statistiske kart på globalt rutenett på 1,5° (121 rader fra 90°N til 90°S, 241 kolonner fra 0° til øst): gradient dN/dz i de første 65 m overskredet i 1, 10, 50, 90 og 99 % av tiden; for jordkanaler prosent av tilstedeværelse, tykkelse og intensitet; for den høye tilstedeværelse, baseandel, tykkelse, intensitet og maksimal høyde; pluss våt sikt og temperatur. Interpolerte årlige verdier:

| Beliggenhet | Land rørledning: % / tykkelse / intensitet | Høy rørledning: % / base / tykkelse / intensitet | dN/dz overstiger 10% av tiden |
| Napoli |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | −380 N/km |
| Tyrrensk (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | −674 N/km |
| Trapani |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | −520 N/km |
| Brindisi |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | −405 N/km |
| Adriatisk (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | −399 N/km |
| Milano |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | −172 N/km |
| Udine |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | −176 N/km |

For vår indeks er en klar klimatisk referanse: tykkelse og intensitet som er typisk for kanalene i stedet for den 50 m faste av den potensielle kanalen, og en anomali " i dag sammenlignet med normal" per sone. ITU-data: gratis for ikke-kommersiell bruk, å sjekke før de inkluderes i en offentlig versjon. ZO NES01.DEM-filen (58 MB, 2' rutenett) inneholder klimasonene i inn-/utsjøen; formatet er ikke dokumentert og jeg dekodet den ikke. Willis’ ligninger inneholder bare fysiske konstanter, fri plass og antenne gevinst.

Fjerde runde av kilder (20. november).

Indermuehle og Suzuki 2026, forutsigelse av GFS-rørledningene for CSIRO-observatoriet i Murchison (arXiv 2608.12861): det er den nærmeste kilden til LIVETROPOVHF. Profiler av N fra GFS til 0,25° (overflate til 2 m, deretter de isobariske nivåene, de under den kasserte bakken); for hver minste kolonne av dM/dz under 3 km, base og toppen av kanalen, integrert M-underskudd; deretter P.452-16 og P.1812-6 med GLO-90 25 ruter fra 98 til 1005 km, fra 88 MHz til 2,68 GHz, hver time. Resultater: Median AUC 0,72 på 25 kanaler; P.452 p tidsprosenten bør brukes som en kalibreringskonstant (valg p = 20%), fordi ITU-modellen med en øyeblikkelig ΔN ikke er definert innenfor en kanal; ferdigheten forblir nesten flat inntil 3 dager i forveien. Ved 162 MHz (AIS) har de mottatte ruter den høyeste kanalfraksjon på 67% (0,26 versus 0,16) og den høyeste kanaldybde på 49% (28 versus 19 M) med hensyn til kontroller: på havet bestemmer kanalen nesten alltid landstrekningen. GFS ser halvparten av tiden på bakken sammenlignet med ERA5 (4,5 % versus 10,4 % av cellene), mens på havet ser mer (7,3 % versus 56 %). En korridoindeks (fortløpende rørledninger langs ruten, lag med en minste tykkelse på 40 m) forbedrer resultatet: bekrefter at kontinuitet langs ruten er det riktige kriterium.

Longley and Rice, ESSA ERL 79-ITS 67 (1968): metode for uregelmessig terreng fra 20 MHz til 20 GHz (etter ITM), med jorduregelmessigheter Δh, Ns fra 250 til 400, ekvivalent radius 8497 km (4/3), horisonter, diffraksjon, scatter fremover og variasjon i tid og på steder for klima. Statistisk referanse, ikke umiddelbart.

ITU-R P.834-2: radius krumning, jordradius tilsvarende k·a, M = N + h/a (h i m, i tusenvis av km), eksponentiell modell; ingenting nytt enn det som allerede brukes.

AGARD CP-70 (1970), tropoferisk utbredelse: tynne lag fra 1 til 100 m med sterke gradienter er vanlige; refleksjonskoeffisienten til et høyt lag er omtrent proporsjonell med bølgelengden, med en tilfeldig dispersjon på ca. 14 dB. Mål til 90 MHz: reduksjon på 9-10 dB bundet til et N-hopp konsentrert på ca 350 m, øverst på et lag av skyer. Passasjen til en kald front eliminerer lagene: Diffraksjonen gjenstår.

US URSI Report, Commission 2 (1960): gjennomsnittlig tykkelse av de 60 til 210 m høye lag (gitt i samsvar med P.453), minste gradient for 157-160 N/km kanalen.

Bussemi K2OVS, VHF utbredelsesguide: typiske avstander for reversering og kanal for band, kanaler knyttet til høyt stasjonært trykk, maksimalt ved daggry.

Wolinsky-Mancini og 2024, AIS på Kanariøyene: AIS radiofloder og mottak; kanalene er mye hyppigere i de varme månedene (inversjon av aliserne).

Nigerianske og malaysiske artikler (Ekah, Iloke, Mat, Igwe): lokale sammenhenger mellom temperatur, fuktighet og målt tap, svake og motstridende (r −0,09 til 0,27); nyttig bare som bekreftelse på at bakkestørrelsene alene ikke er nok.

Herben 1984, avhandlingen TU Eindhoven (mikroonde): 30 GHz på 8,2 km og OTS satellitt på 11,6 GHz; gnist for turbulens, multi-kurs og regn. For mikrobølger dynamiske tap domineres av regn og turbulens, ikke av kanaler.

Baars, Paraboloidal Reflektor Antenna (mikroonde): gevinst av en illustrasjon G = η·(πD/λ)2 med typisk effektivitet 0,5–0,65; tap av overflatefeil i henhold til Ruze, ηs = exp(−(4πε/λ)2)2). Det ville hjelpe hvis vi legger til likhetene som et alternativ til Yagi DL6WU for 1296 MHz og videre.

Allerede analysert eller av dårlig innhold: Hirschmann NASA X-731-67-89 (dobbelt), Reids VHF ST-radar (vinder og turbulens, uledende), Scribed slide og generiske notater på troposfæren.

Femte runde av kilder (20 november, kveld).

Rogers 1997, Radio Science 32 (1): Bayesisk estimat av parametrene til en høy kanal (baserate, M-underskudd, tykkelse) fra utbreiingsfaktorer målt utover horisonten, med en parabolsk ligningsmodell; VOCAR-eksperiment i California ved 143, 263 og 375 MHz, sammenligning med radiobølge. Signalet avhenger mye av basen og M-underskuddet (korrigerer 0,81 til 143 MHz for underskuddet), nesten i det hele tatt fra tykkelsen (−0,77): tykkelsen kan ikke oppnås fra signalet. For oss: beregning B (klimatykkelse) teller mindre enn underskudd og aksje; og metoden er grunnlaget for bakkontoret, det vil si å få kanalen fra flere frekvenser.

Moler og Holden 1960, "Troposfærisk scatter utbredelse og atmosfæriske sirkulasjoner", J. Res. NBS 64D, s. 81 og følgende (opprinnelig på 22/11/2026, doi 10.6028/jres.064d.014). Fly til Point Loma (San Diego): Santa Barbara 190 miles, Point Mugu 144, San Clemente 78; 1365 MHz og 9380 MHz, ikke VHF; Januar–april 1958, gjennomsnitt 1 minutt. Forsterkning av ca. 20 dB i ca. 3 timer mot middag, når en syklonisk virvel svekkes og begynner havbris og subsidens; ved 15 konvergansen reformeres og signalet reduseres med ca. 22 dB fra maksimum. Statistikk (tabell 1, 500 hPa, 33 tilfeller): med luft som dråper det gjennomsnittlige signalet er høyere enn ca. 2,5 dB enn med luftøkende, i L og X-bånd. Signalet følger forskjellen beregnet til 500 hPa i tegn og amplitude av variasjonene, uten korrelasjonskoeffisient. Lave lag (under ca. 600 m) favoriserer delvis reflektering på spredning. Antisyklonene og frontene er ikke behandlet, og de "ekskursioner opp til 60 dB" som er nevnt ovenfor er ikke: de kom fra et begagnet sammendrag. For oss: den vertikale bevegelsen er en god prediktor, og +1,5 dB for subsidensen og -2 dB for oppbakken luft er av samme rekkefølge som den gjennomsnittlige målte forskjellen (2,5 dB); 20 dBs er et enkelt tilfelle, ikke en typisk verdi, og på mikrobølgeovn.

Banafaa og Muqaibel 2025, IEEEE Access: undersøkelse av rørledningene (dampningsmodeller, parabolisk ligning, radarestimater, GNSS og lidar) og klassifisering med automatisk læring på reelle mål; Støtte vektormaskiner gir de beste resultatene. Nyttig som bibliografi; Dataene er fra Persiabukta.

ITU-R P.834-7 (2015): Oppdatert versjon av anbefalingen om troposferal brytning; ingen ny formel enn de som allerede brukes.

Abbott, NBS bibliografi på troposferous scatter (GOVPUB- C13): allerede i tredje runde. Skyv på kanalene: generisk innhold. Artikkelen ScienceDirect 002191698790030304 Kunne ikke åpnes.

6. runde (20. november, natt): Bean og Dutton, ARRL Handbok 2022 og 2023, Balanis.

Bean og Dutton, Radio Meteorologi (cap. 6), deler som ennå ikke brukes. Feltfølsomhet for Ns: 0,18 dB per enhet N på den årlige syklusen; 0,14–0,24 dB/N i dagtidssyklusen (maksimum tidlig om morgenen), opp til 0,2–11,1 dB/N på rutene 175–200 km om vinteren; rettelser 0,8–0.95. Parameter sammensatt av Misme: M = a(g\_e − 40 + b·ΔW^n) med a = 0,5 dB per N/km, som kombinerer ekvivalent gradient og termisk stabilitet ΔW (arbeid for å løfte luften på 1 km): i Sahara ved 471 MHz på 160 km er den eneste stabiliteten 20 dB mellom januar og juni ved nesten lik gradient. potensiell refraksjonsindeks K (N rapporterte adiabatisk til 1000 hPa): fremhever luftmassen og den antisykloniske subsidensen (Flavell og Lane).

ARRL Håndbok 2022 (99a ed.), § 19.4.3–19.4.4. Nyttige inversjoner mellom 250 og 2000 m; inversjoner av stråling (sera, rolig luft, tørr jord) fører sjelden sann; langsomme antisykloner gir de mest omfattende kanalene per subsidens, med inversjoner mellom 500 og 3000 m, sterkere om kvelden og om morgenen; på den nordlige halvkule de beste rutene er sør for det høye sentrum. Hot front: gode forhold noen hundre km foran; kald front: sterk men ustabil bak fronten, parallell. Foehn (også i Alpene) og jordbris (kjølig luft på bakken, varmt på 200–300 m; strekker seg over 800 km langs kysten). Scirocco: varm og tørr luft fra Sahara over kjøligere sjøluft, veldig sterke inversjoner fra det østlige Middelhavet til Gibraltar, sannsynlig årsak til rekorder over 1500 km i mikrobølgeovn. Troposcatter: teoretisk grense rundt 800 km, maksimal typisk halvdel. Den 100. ed. (2023) har en kortere tekst: inversjoner ved soloppgang og solnedgang opp til +20 dB og +20–50% strømningshastighet; 1200 miles på Great Lakes, typisk 400–600; anbefaler Radio Mobile (ITM-modell) for Hepburn dekning og kart.

Balanis, Antenna teori (4a ed.), § 10.3.3: Viezbicke tabellen (NBS TN 688, 1976) for Yagi optimalisert ved 400 MHz: boom 0,4 / 0,8 / 1,2 / 2,2 / 4,2 / 4,2 λ → 7,1 / 9,2 / 10,2 / 12,25 / 13,4 / 14,2 dBd. Vår DL6WU formel (8,6 + 8 log10 boom/λ) gir 1–1,7 dB mindre mellom 0,8 og 4,2 λ og returnerer i tråd med DL6WU tabellene til 9,5 λ: forsiktig estimat. Med riktig formel på den opprinnelige (9,4 + 7,8 log10 boom/λ, fra 22. november) faller kastet under 0,2 dB mellom 1,2 og 4,2 λ (12,1 dBd til 2,2 λ versus 12,25) og forblir 0,6 dB til 0,8 λ.

Syvende runde (21. november): QST, QEX, W3SZ, GitHub lager.

QST 2000-2025 (14 tall, 10 senger med tekstgjenkjenning). Bare rapporter om The World Over 50 MHz, som er nyttige som tilfeller for å validere modellen: 5. september 2000, utført på 144 MHz og videre på den østlige halvdelen av USA; 14.–15. august 2022, åpning Manitoba-Kansas/Missouri under høyt trykk på en varm dag, fra 144 til 1296 MHz nesten alt i FT8, kanadisk rekord på 10 GHz på 1228,56 km ( VE4MA – N0OY), signaler opp til 1941 km; APRS-utbredelseskart viste det samme området. QST November 2022 anbefaler Hepburn kart. Ingen teknisk artikkel om tropo-prognosen. Q65

QEX og K1JT, Franke og Taylor (QEX 2020) på FT4/FT8 -moduser: FT8 dekodingsterskler −20,8 dB i 2500 Hz på kanalen uten avanescens, −18,6 dB med avanescens på medium breddegrader; FT4 −17,5 dB. De andre tallene på QEX 2023 og 2026 behandler ikke tropoen (tre tall 2023 er uleste skanner). W3SZ 2019: langsom modus terskel ( JT4A −23, JT9A −27, JT65A −25, QRA64A −26, FT8 −21 dB) og valgkriterier mellom følsomhet og frekvensbyttetoleranse.

GitHub. TropoLink (LukiBox, Apache-2.0): design av troposcatter ruter med P.617-5 (med N0 og ΔN), NBS TN101 og ITM flankert, og deres forskjell vist som usikkerhet; P.617-5: L = F + 22 log f + 35 log θ + 17 log d + Lc, F = 0,18· N0 ·e^(−hs/7,35) − 0,23·ΔN, Lc = 0,07·e^(0,055(Gt+Gr)). andi-f/troposcatter ( DL2FZN 2013, fra VHF/UHF Handbook RSGB): Lts = 24,44 + 4,283 f + 0,06656 d over ledig plass, skråning nær PROPBER (0,0674 dB/km). QRadioPredict: ITM og ITHOM 3.0 med S RTM3, jordbruk Corins og NEC diagrammer. Ikke relevant: Tropos (vær app), opera-adt/tropo og troposim (troposferisk forsinkelse for InSAR), tgbot-_troposcatter (militær stasjon), sdr-troposcatter-gui (kontrollpanel), Troposcatter-Propagation-delay ( ERA5 forsinkelse), radio-kommunikasjonsprosjekt (didaktisk simulator), PyLayers (kanal).

Åtte runde (21. november): NTIA og ITU modeller på GitHub. NTIA/itm (Longley-Rice, ITS): 20 MHz–20 GHz, gratis romtap, diffraksjon og troposcatter med jordprofil, 0,5–3000 m terminaler, overflate reaktivitet N0 (minimitet redusert til havnivå, 250–400), syv klimaer, jordkonstanter og ulike moduser 1 visning, 2 diffraksjon, 3 troposcatter; C++ med.NET Framework 4.7.2 kabinett. Den modellerer ikke kanalene: den tjener som referanse " normal atmosfære" med statistisk variasjon. NTIA/p528 og eeveetza/p528 (MATLAB): ITU-R P.528-5 for luft-oljeforbindelser, 100 MHz–30 GHz, 1,5-20 000 m terminaler, p 1–99%;.NET 4.8 foringsrør. Mulig bruk: air scatter balanse (to bakke ruter - fly pluss radar seksjon). eeveetza/p2001 (MATLAB): ITU-R P.2001-6, all-field-modell med diffraksjon, tropskatter, unormal utbredelse (leder og lag, Lba) og E sporadisk, prosentandel av tid 0–100%; ITU digitale kart (DN\_Median, DN\_SupSlope, DN\_SubSlope, dndz\_01, TropoClim, h0, surfwv, Es) er ikke omdistributable. Mulig bruk som ITU-sammenlikning i stedet for P.452, med GFS ΔN i stedet for kart som i CSIRO-studien; kartene bør lastes ned av brukeren i henhold til P.453. Validering 1.3.3 av 593 QSO (29/12/2021–2/2022): hendelse/komparasjon separasjon 0,79 uten rettelser, 0,91 med Misme og subsidens; GFS ser ikke kanalen på havet ved 144 MHz (guadagno uten rettelser aldri mer enn 15 dB).

Ninth runde (21. november): IU3QEZ, NTIA, PyWaveProp, ACE Høy, validering 1.10. IU3QEZ (S. Fabris), presentasjon " Over horisonten i VHF" (GEKO Magazine n. 69, september 2026): vanlig volum d2/(8·R\_eff) med R\_eff ≈ 8500 km (1,3 km til 300 km, 3,7 km til 500 km hvert nivå av horisonten verdt 69 miles (111 km), margin på 7 dB for avanscence og 4 dB for bakkerefleksjon; Avhandling: 10 dB er ca. 100 km; stasjonen IQ3QC 5 W med 3×5 elementer, 120 QSO, maksimum 788 km (Alperia 2026). Bibliografi av presentasjonen som skal gjenopprettes: Norton 1959 (J. Res. NBS 63D, System Loss), Bray 1961, ARRL Antenna Book 2023 fig. 4,29, Rice-Longley-Norton-Barsis NBS TN 101, Bean og Thayer 1959, Barsis og Kirby 1961 (diffrasjon på fjellryggen), Deygout 1966, P.1812, AirScout DL2ALF, P.834 NTIA PropLib (wiki): eHata, ITM, LFMF, P.528, P.2108 (tap fra inngrep), med C++,.NET, MATAB og Python; ingen kanalmodell; ITS-side: også IF-77 (luft-solje) og MPM (1 GHz–1 THz). PyWaveProp (Python): Parabole ligningsmetoder med Padé-tilnærminger, flere kantdiffraksjoner, PETOOL- og RAM-innkapslinger, troposferøse og høye kanaler, irregulær jord: mulig base for en feltberegning med GFS M(z-profilen, som skal kontrolleres. APRS PropView (Apache 2.0): VHF-utbreiingskart fra APRS-pakker, timestatistikk og åpningsalarmer; mulig kilde til 144 MHz-data for validering. NATO Troposcatter nettverk i Italia (fortifikasjoner.net): 900 MHz, 10 kW, 20 m illustrasjoner; stasjoner inkludert Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Vulture, Monte Lauro; historisk test case for P.617. Ikke relevant: Gupta 2021 ( stratosfærisk transport), Law 2023 (Arctic ozon), delfin #590 og opera-adt/tropo (InSAR forsinkelse), tropo.ino (Arduino gasssensorer), tropo-webapi (telefonia), skyverktøy/troposfære (AWS). Validering 1./3 på 564 QSO tropo: AUC 0,79 (modell 1,2), 0,80 (+ myk kanal), 0,90 (+ misme og subsidens), 0,90 (+ utholdenhet); kanalfarger (≥ 16 dB) på 46% av åpningsrutene og 0% av sammenligning.

Legg til i samme runde. mikelytaev/wave-propagasjon (MIT, Python 3): Parabolisk ligning med tilnærmet Padé og Numerov, irregulær jord, troposfæriske kanaler; lad den modifiserte brytnings-M(quota, lat, lon) fra NOAA GFS og sett en fordampningskanal av Monin-Obukhov; ingen dokumentert funksjon som returnerer tapet på banen. Han er den beste kandidaten til en fremtidig feltberegning med GFS M-profilen (det vil tjene en ekstern tjeneste eller en oversettelse). RP-Sim (AGPL-3.0, JavaScript): Empiriske modeller for radionettverk (Hata, Han, Ericsson, to stråler), ingen tropo. RIMFading (GPL-3.0, OMNET++): flyturegelmessigheter for nettverkssimuleringer, Weibull-fordeling, ingen tropo. PyLayers: innendørs radiokanal, allerede ekskludert. qradiopredikt (kantooon, GPL v2): ITM og ITHOM v3.0 med S RTM3 og Corine, allerede i den syvende runden som QRadiopredikt.

Tiende runde (21. november): Antenna Book, Norton, Barsis, nye datakilder. ARRL Antenna Book (23a ed.), cap. 4 ( K9LA): Bray-metoden (QST November 1961) for VHF-området: glatt jordtap til 99 % (300 miles, 144 MHz: 214 dB), kurver til 50 % og 99 % (fig. 4.11): 195 dB verdt 100 miles til 99 % og over 250 miles til 50 %; avansering ±7 dB over 100 miles (−3,5 dB til 50 miles), SSB 3 dB verre enn CW, +4 dB bakkerefleksjon, +3 dB for kobling av to antenner; høydeøkning (15 m: 4/3/2 dB ved 10/50/100 miles; 24 m: 8/6/4 dB); Yagi får ca. 10 ganger bommen i λ (dBi); 50 W stasjon med gjennomsnittlig antenne: ca. 180 dB, 100 miles, +10 dB opp til 250 miles. Soloppgang og solnedgang opp til +20 dB og +20–50 % flythastighet; varm og tørr luft over våt frisk luft på de store sjøene opp til 1200 miles, typisk 400–600; regioner som er påvirket av store vannmasser mest favorisert; sen vinter med oppvarming i stand til sterke inversjoner. Verifisering av troposcatteren vår: P.452 til 50% ved 483 km og 144 MHz gir ca. 204 dB, i samsvar med 214 dB Bray til 99%. Norton 1959 (J. Res. NBS 63D, fri på NIST): grunnleggende tap, system og baneantenne gevinst; referanseatmosfære Ns = 301; ekvivalent radius fra Ns (250 → 4878 miles, 301 → 5280, 400 → 6997); L(99) tabeller med 30-fot antenner (100 MHz: 163 dB til 100 miles, 208 dB til 500 miles; 1000 MHz: 138 og 188 dB med 28-fot ligninger); langsiktig variasjon V(p, θ) avhengig av vinkelavstand (fig. 13). Rice, Longley og Norton, NBS TN 15 (1959), gratis på NIST. Barsis og Kirby, NBS Report 6751 (1961): 223 km i Colorado med Pikes Peak (4292 m) som kant, 100 og 751 MHz; konsistent tap med avrundet kant (ca. 11.5 km radius); langsiktig avansement 11,7–14,3 dB versus 39–42 dB av en scatter av samme lengd; ingen dagsyklus merket; den fire-stråle modellen som er gitt 4–5 lober, men bare én er målt (maksimt til ca. 21 m fra bakken): stiene med fjellhindring er mye mer stabile enn spreaden, nyttig for forbindelsene "via Vesuvius eller Faito". ARRL VHF Digital Handbook: pakke, APRS, D-STAR, WSJT (Scatter Method and EME), ingen del på tropoen. Nye dokumenterte eller søkte datakilder: ECMWF åpne data (IFS 0,25°, nivåer 1000, 925, 850, 700 hPa..., uten 975, 950, 900 og 800; G RIB2 med CCSDS kompresjon, at vår leser ikke dekoder; allerede tilgjengelig gjennom Open-Meteo); DWD ICON-EU åpne data (G RIB2 komprimert bz2, en fil for variabel og nivå: mange forespørsler); Canada GDPS på MSC Datamart og GeoMet (ca. 15 km, opp til 33 nivåer; GeoMet tillater avling av området; det er grunnlaget for Hepburn-kart); GFS NOAAA-arkiv på AWS fra februar 2021 (globale filer, nyttig for validering); APRS PropView bare for validering.

Ellevte runde (21. november): Longley-Rice, GEKO 69. Bean og Thayer, - Modeller av Atmosfærisk Radio Refraktiv Index - Proc. IRE May 1959 (pp. 740 ets.): Modell 4/3 har høyre skråning i første km, men faller for høyere; 30% av krumningen til en radius oppstår i den første 100 m og 60% i den første km; gjennomsnittlig forhold mellom Ns og reduksjon i det første km: −ΔN = 7,32·e^(0,005577·Ns) (Ns = 313 → ΔN ca −42). Bean og Thayer, "C.R.P.L. Exponential Reference Atmosfære", J. Res. NBS 63D n. 3 (1959): N(h) = Ns·e^(−c·h) med c fastgjort av Ns gjennom samme rapport; Gjennomsnittlig Ns USA 313; strålesporingstabeller (total kurve 15,2 mrad med eksponentiell modell versus 30,3 mrad med jord 4/3). MATLAB refraktexp: c = 0.143859 km−1 for Ns = 313 (skala ca. 6,95 km), gyldig over ca. 1 km og 10 mrad. Bruk for oss: den "normale" sammenligningsprofilen kan være at CRPL med de lokale Ns i stedet for M = 315 + 0,118·z faste, og anomalien av reduksjonen i det første km bør måles sammenlignet med −7,32·e^(0,005577·Ns) i stedet for −40 faste. NTIA Report 82-100 (Hufford, Longley, Kissick 1982), ITM guide i områdemodus: Clima og Ns foreslått (kvatorial 360, subtropisk kontinental 320, subtropisk marine 370, ørken 280, temperert kontinental 301, temperert marine på land 320, temperert marine på sjø 350); Ns = N0 ·e^(−hs/9,46 km); du bruker det månedlige minimum av Ns fordi mindre påvirket av super-refraksjon og sub-refraksjon; Ns = 301 svarer til ae = 8497 km; under 100 km Ns teller lite. Rice, Longley og Norton, NBS TN 15 (1959), del I: Forutsetningsformel for distribusjon over tid etter land og troposfærisk bølge, base av TN 101. SoftWright, Wikipedia, Altair WinProp og Matlab rfprop. LongleyRice: samme informasjon (versjon 1.2.2, 20 MHz–20 GHz eller 40 GHz, 1–2000 km, Ns 301, syv klimaer, variasjon i tid, sted og situasjon); ingen former kanalene, bare den statistiske effekten av de marine klimaene (" meget høyere felt for 10 % av tiden eller ikke"). GEKO Magazine n. 69 ( IU3QEZ): radiohorisont 4/3 av 130 km fra 1000 m, 260 km tilsette to stasjoner; troposcatter med felles volum mellom 1–3 km og 10 km; strø fra fly til 10 km med +10–20 dB i titalls sekunder eller et par minutter; teoretiske kurs med 2×10 elementer per side: SSB QRP ca. 440 km, SSB 50 W ca. 510 km, CW QRP ca. 600 km, SSB 500 W ca. 640 km (500 W i SSB gjør 40 km mer enn 5 W i CW); 90–99% av tiden en margin på 10–20 dB bør fjernes; 5–10 dB per horisont grad på lange strekninger; kvoten teller til å frigjøre horisonten, ikke å nærme seg det vanlige volumet; Alpe Adria 2026: 99% av QSO trocatter, nesten ingen av luft til tross AirScaut.

## 12. Rundtabell: kilder til å validere modellen eller korrigere den (21. november)

Oppsummering av alle kilder som er bestilt til bruk. "State" sier om kilden allerede er i programmet (1.10.1), om den er i test eller om den skal gjøres.

### Data å validere (det som virkelig skjedde)

| Kilde | Hva gir | Bruk | State |
| MMMonVHF rapporter fra forfatteren | 593 QSO ved 144 MHz, 29/12/2021–2/2022, tid og lokatorer | Validering av hendelse mot sammenligning (+14 dager) | i bruk: AUC 0,79 (1,2) → 0,90) |
| Verden over 50 MHz | dokumenterte tilfeller (14–15/2022 Manitoba-Kansas, 144–10 GHz) | Historisk sak | i bruk som et historisk tilfelle; som skal legges til valideringen QSOs |
| wspr.live (WSPR) | komplett arkiv, 2 m og 70 cm, SQL | mange hendelser og mange " nei" (timer uten sted) | prøvet, det bør filtreres (spot falsk på 16-20 tusen km) |
| APRS PropView / APRS-IS | stasjoner filt, avstander, timer | Bare validering 144 MHz | å gjøre |
| Reverse Beacon (arkiv) | daglig sted | VHF | Kastet for nå |
| I GRA2 radiosondaggi (Brindisi 16320, Practice of Mare 16245, Trapani 16429, Udine, Milano) | profiler målt 00 og 12 UTC | GFS-profilkontroll og inversjoner | funnet filer (Brindisi 46 MB), som skal brukes |
| Barsis og Kirby 1961, Barsis 1963 | avanescens målt på stier med fjellhindring | valider kategorien "monte" (Vesuvio, Faito) | å gjøre |
| Norton 1959, NBS TN 15, TN 101 | L(99) og tap av tabellmedier | Scatter control | delvis (se 300 miles konsistent) |

### Rettelser foreslått av kilder

| Korreksjon | Kilder | Effekt målt på validering | State |
| Misme stabilitet Term | Bean og Dutton cap. 6 | AUC 0,79 → 0,90 sammen med underlaget | i Perjeta (Tittel 16 dB uten kanal) |
| Støtte fra vertikal bevegelse | Moler og Holden 1960 (les i original 22/11: +2,5 dB gjennomsnitt mellom luft som kommer ned og luft stiger, 1,4 og 9,4 GHz ved havet) | som ovenfor | i Perjeta (+1,5 / −2 dB) |
| Mykt rørledning (λ opp til 3·λmax) | Kerr, Hepburn, VHF/UHF DX-bok, Shi 2019 | alene +1 % av AUC; bringer fargene på kanalen på 46 % av rutene av hendelsen og 0% av sammenligningen | i |
| Persistens 24 h | VHF/UHF DX Bestill, Biancomano og Leyshon | AUC uendret, mer tatt og flere falske alarmer | i Perjeta |
| Normal CRPL-profil med lokale Ns (−ΔN = 7,32·e^(0,005577·Ns)) i stedet for 40 N/km fast | Bean og Thayer 1959, NBS Monograph 4 | testet i validering | som et alternativ, off på grunn av defekt |
| Overbrytningsgrense 79 N/km i stedet for 100 (Bean and Dutton, lag 1000-850) | P.453, Lopez 2008, DX bok | testet i validering | (kartet bruker forsiktig) |
| Klimatologisk tykkelse (beregning B) mindre viktig enn underskuddet | Rogers 1997 | å prøve: bruk A for gevinst | å gjøre |
| Parabolisk likning i GFS-profilen | PetoOL, bølgepropagasjon (Lytaev), Levy (bok ikke lest, bare forhåndsvisning), Lindquist | første versjon med felt over horisonten dominert av numerisk støy (AUC 0.12–0,28): korrekt (domene 8 km, bjelke 6°); etter korrigering AUC 0,54–0,66 | i bruk som andre beregning, bestemmer ikke prognosen |
| Evanescens: ± 7 dB over 100 miles, margin 10–20 dB til 90–99% | Bray 1961, Antenna Book, GEKO 69 | Terskelverdier på måter under marginale forhold | i bruk: sannsynlighet for periode og terskel FT8 −18,6 dB i troposcatter |
| Vanlig volum og tilsvarende avstand av Bray | IU3QEZ, Bray 1961 | Informasjon | i |
| Troposcatter P.617 og RSGB som usikkerhet | TropoLink, DL2FZN | Informasjon | i 1.3.3 |

### Hva som forblir ut av modeller

Gjennomført på havet i lav høyde (100–200 m) som GFS-nivåene ikke ser (studier av aktuelle datakilder); refleksjon og diffraksjon på de nærliggende fjellene (Vesuvio, Faito): forslag til beregning "via monte". ITM/Longley-Rice forblir en statistisk referanse " normal dag" (Ns 301, climi), uten kanaler. Forkastet ved beslutning av forfatteren: AirScout, P.528), E sporadisk (P.2001) og 50 MHz (servoir EIRP som få har og forvirrer med E sporadisk).

### Forslag til arbeidsordre

1. Utvidet validering: Legg til filtrert WSPR (selv timer uten spot) og QST 2022; gjenta for 432 MHz.
2. Prøv den normale CRPL-profilen og ITU-grensen på 79 N/km, én om gangen, og hold bare det som hever AUC.
3. Sammenligning av GFS-profiler med I GRA2 -radiobølgen av Brindisi og Øvelse av havet i QSOs dager.
4. Omvurdere parabolisk ligning etter numerisk korrigering.

### Beslutninger av 21. november (redigert)

- Antatt: forsiktig estimat (Bean og Dutton, 100 N/km) og aggressiv (ITU, 79 N/km) flankert; kanaler søkte bare under 2500 m (først 1500 m, hevet etter validering); bonus med høyt forlenget trykk og svak vind; sjøvekt med μ1 av P.452; andel av stasjonene med hensyn til kanalen også på delvis kanal; grenselaget over ryggene ( IT9GSF).
- Anerkjent som et alternativ, slått av ved defekt: normal CRPL profil med lokale Ns.
- Korrekt parabolisk ligning numerisk: AUC fra 0,12 til 0,54 (30 m antenne) og fra 0,28 til 0,66 (300 m).
- Land: Copernicus DEM GLO-30 i stedet for Terrain Fliser «skadi», som nær kysten har spøkelsesrelieff på 80–100 m til sjøs (signal på IC8TEM, verifisert punkt ved punkt til Capri); skadi forblir som en reservat.
- Værtegn på kartet med deres effekt: ↓ gunstig subsidens (Moler og Holden 1960, i gjennomsnitt ca. 2,5 dB mellom luft som går ned og luft går opp; +1,5 dB), ↑ luft oppover baksiden ugunstig (avhengig dyp; −2 dB), S scirocco gunstig ledetråd (ARRL håndbok § 19,4, inversjoner svært sterk på Middelhavet; ingen rettelse). Dobbel symbol, grønn / rød / oransje farger og tegneserie bok på kartet.
- Vedtok ITM 1.2.2 (NTIA/itm, offentlig domene), oversatt til C#: i henhold til beregning av baseline i normal atmosfære ved siden av P.452 og sluttstrekning etter utgangen fra en kanal som slutter før korrespondenten. Fjernet qradioprediktkode fordi under GPL v2.

Uten nyttig innhold for indeksen: *Kommunikasjoner Kvartalt*, høsten 1991 (ingen artikkel om tropo; Atkins' artikkel om Scatter er om vinteren 1991).

- Anerkjent som manualprinsippet (21. november): \"utbredelse kan ikke generaliseres, men studeres i forhold til lokal morfologi\" ( DF5AI, rapportert av IK3XTV i \"Propagasjon på 2 meter\"), sitert i prognosehåndboken (§ 1) og i rådene.
- Adoptert (21. november) tolkningen av IK3XTV s transalpinåpninger: ikke -konfektive celler såsom linser - men troposcatter forsterket ved turbulens til toppen av laget som re-vedtas når det når andelen av det felles volum (manual av prognoserne, § 8 ter). Fra Copalia går det inn i punktberegningen som en midlertidig forsterkning av troposkateren, i det meste 3 dB. De beskrevne koblingene forblir uforklarlige med diffraksjon og troposcatter; de erklærte krefter brukes ikke som gitt), snøen som "improves diffraksjon" (sannsynligvis det stabile laget over snøen, med mindre avansasjon).

- Kerr (1951) gjennomgått boken (22. november PDF gitt av forfatteren): § 1.5, eq. 17 og 18 og tabell 1.1. Figurene til eq. 18 i skanningen er uleselig, men tabell 1.1 bekrefter: 20 ft → 1 cm, 80 ft → 10 cm, 400 ft → 100 cm, 2000 ft → 1000 cm, mot 1,25, 10, 112 og 1252 cm λmax = 0,014 dd^1,5 (λ i cm, d stående; den runde tabellen, 'indikative verdier'. Kerrs hypoteser: inne i kanalen trenger de modifiserte indeksdråper på 4·10−8 per fot, typisk verdi av de lave kanalene; med svakere gradienter behov for en større tykkelse. Kerr advarer også om at λmax ikke er et netto kutt: bølgelengder noen ganger lengre kan fortsatt påvirkes sterkt av kanalen.

- DL6WU revurdert på originalen (22. november PDF av VHF Communications 3/1982 levert av forfatteren): G. Hoch, «Extremely Long Yagi Antennas», s. 130 og følgende. Figur 1: Forsterkning på dipolen målt ved 432 og 1296 MHz i et profesjonelt polygon av de tyske innlegg, med en prøveantenne EIA fra 7,7 dBd, estimert nøyaktighet 0,5 dB; 2,35 dB for doble lengden av bommen, ingen metning opp til ca. 20 λ. Programmet brukte 8.6 + 8.0.log10(L/λ) dBd, dvs. ca. 0,8 dB i mindre enn 1–2 λ; nå bruker den 9,4 + 7,8 ·log10(L/λ) dBd (gradienten er den angitte, verdien til 1 λ leses på diagrammet, ± 0,3 dB). Dessert formelen 10·log10(7,8·L/λ) sirkulert på nettet: ville gi 3 dB for dobling, mot 2,35 målt.
- Booker og Gordon (1950): referanse som er bekreftet av " Denne ukens Citation Classic" (Current Content, 8. februar 1982, kommentar av H. G. Booker): Proc. IRE 38, s. 401–412. Kommentaren bekrefter de to punktene som brukes i manualene: felt mottatt utover horisonten med Rayleigh-fordeling, og eksponentiell autokorrelasjonsfunksjon av uregelmessigheter (ikke-gaussisk). Den opprinnelige artikkelen er ikke lest.

## 13. Fra IK3XTV artikler

De to artiklene fra Flavio Egano IK3XTV (Radio Rivista: "Propagation på 2 meter" og "Propagation VHF – konfektive celler og gravitasjonsbølger") legger til to ting som kanalmodellene ikke dekker: transalpinåpninger på ettermiddagen på grunn av konveksjon, og praktiske regler om hvor og når rørledningene er dannet i Italia. De er observasjoner fra stasjonen, ikke mål: de må behandles som hypoteser for å kontrollere med flekker.

| Kommentarer til forfatteren | Hvordan det blir en ingrediens i indeksen |
| Kanalene er hovedsakelig dannet mellom 100 og 1000 m, sjelden høyere; tjener høyt trykk på et stort område, god isolering og fravær av vind | søke etter kanalen bare under 1500 m; bonus med høyt trykk og svak vind i de lave lagene |
| I de varme månedene som er gjennomført på Adriaterhavet mellom Veneto og Puglia, 700–800 km, noen ganger opp til Hellas; sjørutene er favoritt | større vekt til fraksjonen av sjøveien (f.eks. ITU-R P.452) |
| Det tilsvarende må være i eller i nærheten av kanalen; med høye antenner og noen meter fører signalet ikke inn | Sammenlign andelen av basen av kanalen med andelen av de to stasjonene (vil tjene høyden av QTH) |
| Same tid, samme kanal: lytte fra Gargano en stasjon i 144 og en i 50 MHz | å sjekke: i henhold til Hepburn-tabellen ber 50 MHz om over 300 m bly, så hendelsen vil indikere en rørledning svært ofte |
| Veneto – Sør-Tyskland utenfor Alpene, uten visuelle, nesten alltid på ettermiddagen og med høyt trykk; ustabile signaler som forsvinner og dukker opp igjen etter noen minutter | " konvensjonell" indeks adskilt fra kanalen, for fjellruter |
| De konvektive cellene er født på bakken og stiger rundt 1 km hver tredje time: på 15 når de rundt 3 km, høyden som tjener til å overstyre Alpene | bruk grenselagets høyde (polar\_lag\_høyde på Open-Meteo GFS) langs banen: score når den overstiger høyden på ryggene |
| Alpene genererer gravitasjonsbølger som sammen med konventive bevegelser skaper uregelmessigheter i brytningsindeksen | hypoteser: sterk vind vinkelrett på kjeden til 700 hPa som signal på gullbølger; å prøve sist |
| Veldig høy inversjon (opp til 8 km i eksepsjonelle tilfeller) gir ca. 800 km, men i praksis sjelden mer enn 300 km | andelen av inversjonen utvider omfanget: nyttige data for å estimere venteavstanden |
| Om sommeren jordradius som tilsvarer ca 16000 km (2.5 ganger den virkelige), med ca 60 dB på mindre enn 300 km | konsistent med k50 = 157/(157 − ΔN): k = 2,5 tilsvarer ΔN ca. 94 N/km, det vil si super-refraksjon |
| Over 3000 km bare i 20–30° området nord og sør for ekvator, over havet, knyttet til Hadley-cellene; toppen i august i Afrika–Europa | utenfor Europa: nyttig å ikke gi høye indekser på Atlanterhavet uten tilbakemeldinger, og for Kanariveien - Storbritannia |

Andre nyttige data fra samme artikler:

- Optisk avstand mellom to h1 og h2 meter (d i km): d = 4,1 · (√t1 + √t2), opp til ca 20% for normal brytning.
- Evanescens: langsom og grunn med inversjon; sterk og rask med scatter og konvektive celler. Programmet kan bruke det til å klassifisere annonser hvis det en dag vil ha SNR rapporter over tid.
- For den sporadiske E i 144 MHz rapporterer forfatteren arbeidet til DF5AI og Davis og Johnson (Nature 435, 2005) om båndet mellom timetimer og Es: ut av tropoen, men en egen indikator i morgen kan bruke lyndata.
- Ground refleksjon i doble hopp Es: hav K = 81 og 5 S / m, landskap og skog K = 13 og 0,006 S / m, byområder K = 3 og 0,001 S / m.

## 14. LIVETROPOVHF indeks hypoteser

Startforslag, som skal diskuteres: en indeks 0–10 på samme skala som Hepburn, beregnet for hvert punkt i rutenettet og hver tredje time fra GFS (så ECMWF og ICON for sammenligning) og et andre tall per rute som sier hvor mye del av ruten passerer til en nyttig kanal til det valgte bandet. Vektene nedenfor er mine antagelser, som skal kalibreres med flekker.

Gjennomgang etter Bean og Dutton og Kerr: profilgrenser kommer fra deres tabell 6,5 og skaler fra sine målinger, så et brytningslag er verdt 10-25 dB over den normale profilen. Indeksen beregnes deretter først i dB og tar så på 0-10; verdiene i den saksoren forblir kalibrert med flekkene.

Beregning etter punkt, trinn for steg

1. T utladning, relativ fuktighet, geopotensiell høyde og vertikal hastighet ved 1000, 975, 950, 925, 990, 850, 800, 700, 600 og 500 hPa, pluss T og 2 m fuktighet, bakketrykk, laghøyde og vind ved 10 m.
2. Kast nivåene under bakken; beregning N og M på hvert nivå (ITU-R P.453).
3. Beregning −dN/dh i hvert av de fire lagene av Bean og Dutton (1000-850, 850–700, 700–600, 600–500 hPa) og diagrammet som sub-refraktiv, normal eller super-refraktiv med terskelverdiene til deres tabell; inne i det første laget bruker også mellomliggende nivåer for å finne den sterkeste gradienten og dens andel.
4. Der dM/dh < 0 finner jeg base og topp av kanalen, tykkelse d og underskudd ΔM.
5. Sammenligning av med minste tykkelse for båndet fra formelen til Kerr (ca. 180 m for 144 MHz, 89 m for 432 MHz, som i Hepburn-tabellen); under terskelen teller kanalen fortsatt, men mindre.
6. Jeg stimulerer forsterkningen i dB med tabellen nedenfor (du tar det beste laget, ikke summen av lagene, pluss anomaliperioden) og konverterer den: indeks = dB / 2,5, mellom 0 og 10, så 25 dB er verdt 10. En negativ verdi (sub-refraksjon) viser hvordan 0 men forblir i banedataene.

| Beste lagsituasjon | Kriterier | Beregnet inntekt |
| Normal (ikke laminert) | −dN/dh i flikområdet. 6,5 (f.eks. 20-60 N/km mellom 1000 og 850 hPa) | 0 |
| Litt over normalt | mellom den øvre grensen til det normale bånd og den super-brytningsgrensen | 0 til 10 dB, i forhold |
| Super-refraksjon | utover laggrensen (100, 80, 70, 50 N/km klatre) men under 157 N/km | fra 10 til 15 dB, i forhold til 157 N/km |
| For tynn rørledning for band | dM/dh < 0, tykkelse under Kerr terskel | 15 |
| Nyttig kontakt til band | dM/dh < 0, tykkelse over Kerr terskel | 20 dB, +1 dB hver 5 M underskuddsenhet, maksimum 25 |
| Underbrytning | −dN/dh < 0 i det nedre laget | −10 dB |
| Anomalus bakgrunn (du legger til) | Modell Ns minus ITU-kart klimatologisk Ns for den måneden | 0.2 dB per enhet N, mellom −5 og +5 dB |

Beregning etter rute (mellom to lokatorer): prøve maksimalsirkelen hver 10 km, som allerede gjør programmet for indeksen på banen, og beregne fraksjonen av punktene med nyttig kanal og den gjennomsnittlige andel av basen. CSIRO-studien fant en nyttig del av ruten i rørledningen (på AIS-ruter ved 162 MHz var 67 % høyere enn i kontrolltilfeller).

Tilsetning av beregningen per rute (fra LA0BY og W3SZ): (1) sjøstasjoner sammenlignet med base og toppen av kanalen, fordi en høy kanal kun tjener de som kommer inn; (2) en troposcatter-gulv" med Yeh-metoden, med utgangshjørnene tatt fra grunnprofilen; tropegevinsten i dB fjernes fra dette tapet og resultatet sammenlignes med det tap som stasjonen klarer å overvinne ( KK6MC -tabellen). Således blir indeksen også et estimat av tilgjengelig avstand.

Korreksjoner for å prøve en om gangen (fra Bean og Dutton og LA0BY, IK3XTV, G4MCU allerede i manualen), skal holdes bare hvis de forbedrer sammenligning med flekker:

- Blanding (vigtig i henhold til forfatteren, ved daglig observasjon): dag på jorden, mellom morgen og ettermiddag med klar himmel, eller med vind på bakken over et par m/s, kanalene på bakken forsvinner; deres bidrag må reduseres. Ikke til sjøs.
- Dagens tid (vigtig i henhold til forfatteren, ved daglig observasjon): irrasjonelle kanaler er født ved solnedgang og faller etter soloppgang; det er en 3-timers prognosekontroll.
- Vertikal bevegelse: luft stiger til 850–700 HPa remikserer lagene (negativ korreksjon), luft som synker under et antisyklon styrker dem (positiv).
- Trykktrenden: oppovertrykk på banen i de siste timene, høyere og stabile signal; i reduksjon, lavere signal (basano 2 hPa).
- Synonym: høyt trykk over 1025 hPa, temperatur på 850 hPa mye over det på bakken, varm og tørr luft som flyter på det kalde havet, rute til sjøs.
- Fjellstier: en andrecon konfektiv" indeks (seksjon IK3XTV), med et ettermiddagsgrenselag høyere enn rygger langs banen og høyt trykk.

Fra VHF/UHF DX Bok, andre rettelser å prøve: påfølgende dager med høyt trykk på punktet (mere dager, flere punkter), posisjon i forhold til sentrum av antisyklonen (bonus ved kantene), trykkgradient på bakken som et mål på vinden, kraftig regn i de foregående 24 timer (penalitet), lave skyer ved kanten av laget (penalitet bare for de høye båndene). For retningen av rutene: sammenligne azimuten av ruten med den av isobar.

Verifisering

- LIVETROPOVHF-arkiv: hver kommersiell over 500 km til 144 eller 432 MHz er en "event". Det sammenligner indeksen på ruten med hyppigheten av hendelser, på samme tidspunkt på dag og måned (den samme tilnærmingen til CSIRO-studien).
- Sammenligning med Hepburn og F5LEN på de samme dagene: der de tre indeksene kobler fra, ser du hvem som var riktig i henhold til punktene.
- Radiosundasjoner på 00 og 12 UTC: å sjekke om modellen ser inversjonene som sonden måler.
- Historiske tilfeller av LA0BY (6–11/11/2003, 10/12/2004, 15/10/2011, 11/2/2015, 22/11/2018): indeksen reberegnet på disse dagene må se kanalene som Hepburn ikke viste.
- Grenser som skal erklæres i manualen: utføres under 100–200 m nesten usynlig for GFS-trykknivåer; pålitelig forutsigelse i 2–3 dager, deretter bare trend.

## 15. Kilder fra 21. november (andre del)

- ITU-R P.1812 (MATLAB/Octave implementering av I. Stevanovic, OFCOM; GitHub eeveetza/p1812, Zenodo 10.5281/zenodo.6141558). Spesifik VHF/UHF-arealmetode for ruten, avledet fra P.452: "delta-Bullington" diffraksjon, troposkater, kanaler og refleksjon på lag, oppslukker på bakken. Nyttig som en tredje sammenligning ved siden av P.452 i punktberegning; ΔN og N0 -data er fulle ITU-produkter, så brukeren laster dem ned (i henhold til P.453).
- fra Costa, Rabanal Ramirez, Dias, IET MAP 2018 (doi 10.1 [30]iet-map.2017.0992). 563 MHz, 27 poeng mellom 5 og 30 km i uregelmessig terreng med skog: P.526 med 12 m vegetasjon −4,3 dB og CAS 8,8 dB; P.1812 med 10 m innsnevring +2,6 dB og σ 8,9 dB; dielektrisk kile −4,8 dB og σ 9,2 dB; P.1546 +5,0 dB og σ 11,7 dB. For programmet: den gjenværende feilen ved en god diffraksjonsmetode er ca. 9 dB, og høyden på oppsummeringene på ryggene (10-12 m) teller.
- PETOOL (Ozgun e altri, Computer Physics Communications 182, 2011). Parabolisk ligning split-trinn til en og to ruter med bred vinkel-forplantning (vinkler opp til 40–45°, mot 10–15° av den smale én); bunn med tap gjennom den transformerte Fourier blandet; bakke til trinn. Bekrefter valgene til ICAO (vidvinkel, jordkoeffisienter) og indikerer toveis beregningen for bratte relieff.
- MathWorks. `tropopl`: tap av gasser og tap av «lente» i en lagdelt atmosfære (P.835, P.676, P.453), uten kanaler: med gradienter under −157 N/km den geometriske optiske modellen gjelder ikke. Eksemplet på radarutbreiing benytter seg av refraksjon − ΔN = 7,32·e^(0,005577·Ns), den samme CRPL-rapporten allerede i programmet. `propagationModel` Longley-Rice: forhåndsdefinerte horisontale polariseringsverdier, konduktivitet 0,005 S/m, tillatelse 15, reaktivitet 301, temperert kontinentalt klima. Ingen MathWorks-modell omhandler kanalene.
- ITU-R, digitale produkter fra gruppe 3. Data og programmer til P.453, P.452, P.1812, P.528, P.1546; \"integrerte\" data er en del av anbefalingene; ingen erklært distribusjonslisens: du fortsetter å la dem laste ned til brukeren.
- ESA Navipedia, troposferisk forsinkelse. N = Nidrostatica + Numida; den tørre zenitalforsinkelsen er ca. 2,3 m og den fuktige en titalls cm, som varierer raskt. For oss: våt forsinkelse estimert av Gdańsk stasjoner er et indirekte mål på integrert fuktighet, mulig fremtidig kilde til nå-casting; I dag er ikke brukbart.
- Bray, K2LMG, «A Method for determinering av V.H.F. stasjonskapasiteter», QST November 1961, pp. 36–40 (les i originalen; kontrollert den 22. november på PDF-filen til antall QST: alle verdier nedenfor tilsvarer). Forfatteren, D. W. Bray (General Electric, Ithaca), sier han har bekreftet prognoser med måneder med planlagte forbindelser med K2GOI og W4LTU. Evanscens i løpet av tiden 13,5 dB over ca. 100 miles (7 dB under og 7 over timegjennomsnittet), nesten lineær under (3,5 dB til 50 miles): for en solid tilkobling fjerner 7 dB. Korreksjon av horisonten: hver høyde i horisonten er verdt 111 km ekstra avstand (mindre hvis negativ). Gevinsten av en yagi ca. 10 × boom/λ (kraftforhold), mer ca. 4 dB jordrefleksjon; SSB −3 dB og AM −7 dB sammenlignet med CW; 99% av timene: ca. 180 dB til 60 miles, 190 til 90, 200 til 240 (2 m). Fra ICAO bruker sannsynligheten for perioden CAS = 3,4 dB (±7 dB som ±2 CAS) i stedet for den 5 dB estimerte andre hånd.
- Toveis beregning (prøv med prototype, 21 november). Stor vinkelparabolisk ligning med reell S RTM1 terreng fra Capri, 144 MHz, antenne 10 m fra bakken, standard atmosfære; mål: gjennomsnittlig effekt mellom 20 og 400 m til 80 km på ruten mot tilsvarende. Fra sentrum av Capri og Marina Grande er vesten lukket (hindring til +11–21°, Monte Solaro): peker Faito (næsten bak EA5 og EA6, 150–160° av aksen) signalet lansert mot Spania klatrer 10–29 dB; peker Vesuvius (110–125° av aksen, deretter ut av beregningsplanen, bare indikativ) på 17–25 dB. Fra Anacapri er vesten fri og den direkte seieren på 8–22 dB. De absolutte tapene på 1000–1300 km av prototypen er imidlertid ikke pålitelige. EA3 EA5 EA6
- Barsis og Kirby, NBS Report 6751 (1961), Pikes Peak, 223 km, 100 og 751 MHz. Fjellet er en felles terminal på to ruter i optikk: fordelingen av timemedianene er konvolusjonen av de to rutene. Langtidsutflukt (1–99 %) på ca. 14 dB, mot 26–40 dB av en troposcatter bane av lik lengde eller vinkel; ingen signifikant dagendring. Signalet er sterkere enn troposcatteren men svakere enn den ideelle kanten: toppen bør behandles som avrundet (rundt ca. 11.5 km). Høye signaler blir alltid observert bak ryggen.
- Barsis, NBS Rapport 7652 (1963), tre ruter over et fjell (Colorado, Alaska, Europa), 750-900 MHz. Gjennomsnittlig tap 10-20 dB høyere enn Fresnel-Kirchhoff beregning uten land i nærheten av terminalene; evanescensen er estimert ved å pakke inn de to linjene i optikken; refleksjonene av jorda i nærheten av terminalene forverre den langvarige falmen. Fraansen til kanalen legger til avrundingen av P.526.
- Barsis og Johnson, NBS TN 88 (1961). Forlenget oppløsning av rombølgen, selv på en sti utenfor et fjell (§ 5).
- Norton, «Systemtap i radiobølgeutbredelse», J. Res. NBS 63D (1959). De kurvene som Bray får nomogrammer fra; definisjoner av systemtap, overføring og grunnleggende, prosessantenne gevinst.
- Rice, Longley, Norton og Barsis, NBS TN 101 (1965-67), bind 1 og 2. enkeltkant og hinder avrundet med og uten refleksjoner (§ 7, med eksempel på Pikes Peak), langsiktig variasjon V(0,5) og Y(q) for klima (temperert konkontinental og andre). For oss er metoden for avrundet hindring og variasjonsfunksjoner referansen til kalibrat usikkerhet.
- ITU-R P.526-16 (11/2025). Formula (31) av J(ON), identisk med programmets; Avrundet hinder (32)–(36), som nå brukes; flerdelte metoder (Bullington og full metode § 4.5).
- ITU-R P.1812-8 (2025) og -7. Komplett tekst gjenvunnet: spesifikk metode for 30 MHz til 6 GHz (delta-Bullington, troposcatter, kanaler og refleksjon på lag, indomber). -7 advarer om at troposcatteren (fra P.617-5) er feil under 66 km. Det er fortsatt en mulig tredje sammenligning.
- Paulus, «Plavatisk anvendelse av IREPS-fordampingsmodellen», NOSC TR 966 (1984) og Radio Science 20 (1985). Rapport lest på 22/11/2026 (DTIC ADA146528, skanne med gjenkjent tekst, noen ganger uleselig). Luft-sjø forskjeller som måles med skip, skiftes mot stabilitet: varmere sjøluft i 31–35% av skipsdata mot 0,7–7% av NOAA villsvin, for psicrometer forstyrret av skip og SST tatt fra kjølevann (ca. +1 °C). Jeske-modellen gir deretter 40 m og mer enn 15% av året og høyder som hopper fra 10 til 40 m i 24 timer med stabil tid, ikke bekreftet av radiomål. Foreslått korreksjon (testet terskelverdier 0, −0,5 og −1 °C; −1 °C er bedre avtalt med radiomålene i juni 1982): hvis luft-sjøforskjellen er −1 °C eller mindre den målte forskjellen; ellers beregner du høyden med −1 °C og med 0 °C og, hvis den med −1 °C er lavere, holder du den. Økninger på den stabile siden blir kuttet, reduksjoner forblir. I juni, Marsden 120 kvadrat: gjennomsnittlig høyde fra 16,8 m (klimatologi) til 7,6 m. Likningene til modellen er ikke i rapporten (referert til Jeske og Hitney). Programmet bruker den nøytrale profilen, som ikke har stabil overdrivelse, og begrenser δ til 40 m: det går i samme retning; med SST-ene til modellene (ikke av skip) feilen i måling som Paulus korrigerer veier mindre.
- Levy, «Paraboliske ligningsmetoder for elektromagnetisk bølgeutbreiing» (IEE 2000). På Google Libri er det bare forhåndsvisning: Jeg leser ikke.
- VK3FS (3fs.net.au). Kvalitetsintroduksjon: høye kanaler ved 450–2000 m for lengre åpninger på bakken; inversjoner på 300–600 m i rolige og rolige netter, som forsvinner etter soloppgang; sterkere kystinversjoner på ettermiddagen; 800–1300 km typisk, 1400–1600 km ofte utført. Samtykke med dag/natt varsler og tak på 2500 m.

## 16. Kilder

Regler og formler

- [ITU-R P.453-13 (2017), radio brytningsindeks:](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)formel og data
- [ITU-R P.452-18 (2023), prediksjon av interferens mellom bakkestasjoner over 100 MHz: ekvivalent radius, β0, troposcatter,](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)kanaler
- [ITU-R P.617-5, utbredelse utenfor horisonten for radiobroer](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)
- [Shi et al.,] "En omfattende studie på bølgens maksimale lengde av elektromagnetisk utbreiing i forskjellige fordamperdukter", IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

Studier om prediksjon av kanaler med værmodeller

- [P. Lopez, - En 5-årig 40-km oppløsning global klimatologi for super-brytning for bakkebasert radar meteorologi - ECMWF Tech. Memo 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO), "Tropopheric Ducting Prediksjon basert på GFS Modelldata på... CSIRO Murchison Radio-astronomi", Observatory arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (FMI), "Effekter av modelloppløsning på prediksjon av atmosfæriske kanalforhold med Harmonie-Arome", EGUsfære 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

Amatør radioprognoser

- [W. R. Hepburn, - Hepburn Tropo Index (HTI-](https://www.dxinfocentre.com/propagation/hti.htm)og [](https://www.dxinfocentre.com/propagation/history.htm)metodehistorie
- [W. R. Hepburn, - Laveste brukervennlig frekvens vs. tykkelsen av ducting lag -](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, " Verdensomfattende brytningsindeksprognoser"](https://tropo.f5len.org/WW/info.php), [ny versjon 2026](https://tropo.f5len.org/?p=504), [note på modell ARPEGE](https://tropo.f5len.org/author/f5len/)

Data

- [Åpne-Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api), [note på trykknivå](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api),
- [NOAA NCEI, Integrert Global Radiosonde Archive (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RAOBget, radioflod manus for University of Wyoming](https://github.com/NCAR/RAOBget)

Klassiske tekster (indikert av forfatteren av programmet, som skal høres for endelige formler)

- B. R. Bean, E. J. Dutton, * Radio Meteorologi*, NBS Monograph 92, 1966, 435 sider (reprint Dover 1968): vertikale profiler av N og M, gradient av felle under −157 N/km, sammenligning med radiobølge; kapittel 5 "Synoptisk radio meteorologi". Base for 2-4 trinn for beregning per punkt. Gratis: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [NIST-kort, doi 10.6028/NBS.MONO.92](https://www.nist.gov/node/1770911), [Internet Archive](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf)kopi.
- L. Boithias, *Radio Wave Expansion*, McGraw-Hill 1987 (omsatt av D. Beeson fra fransk, 330 sider, ISBN 0070064334; også North Oxford Academic): mekanismer utenfor horisonten, felles volum av spredning og tap av kobling antenne-mezzo. Kun papir ([bibliotekskort](https://libcat.library.tamu.edu/Record/in00000014812/Description)).
- M. P. M. Hall, *Effekter av Troposfæren på radiokommunikasjon*, IEE Electromagnetic Waves Series n. 8, Peter Peregrinus 1979, 206 sider (ISBN 0906048257; brossura 08634108663): synoptiske situasjoner og utbredelse VHF/UHF. Base for Synoptiske ingredienser. Handel ved IET ([](https://libcat.library.tamu.edu/Record/in00000636385)-kort).
- H. G. Booker, W. E. Gordon, - En teori om radiospreiing i troposfæren - *Proceedings av IRE*, vol. 38, n. 4, april 1950, s. 401-412: Scapping teori fra desogeniteten til brytningsindeksen. Sentral ide: den diffuse kraften avhenger av intensiteten og omfanget av de turbulente svingninger av brytningsindeksen (eksponentiell korrelasjon); i nærheten av horisonten råder diffraksjon, videre spredning; Ifølge forfatterne skalaen av turbulens og indeksvariasjonene som forventes av meteorologien er tilstrekkelig til å forklare de målte feltene. Sitert av 328 artikler (Scilit), ingen gratis tilgang kopi. Kort på Semantic Scholar ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); fulltekst på IEEE Xplore betalt ([doi 10.1109/JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)).
- C. M. Crain, J. R. Gerhardt (Electrical Engineering Research Laboratory, University of Texas), "Measurements of the Parameters Involved in theory of Radio Scattering in the Troposphere", *Proceedings of the IRE*, vol. 40, n. 1, januar 1952, s. 50–54, doi 10.1109/JRPROC.1952.2739 gir de virkelige verdiene som Booker og Gordon hadde bare estimert ([Skilit](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)-kort).
- "Konstantene i ligningen for Atmosfærisk refleksindeks ved radiofrekvenser", *Proceedings av IRE*, vol. 41, n. 8, august 1953, s. 1035–1037: foreslår å gjennomgå strømmene i bruk (79 K/mb og 4800 K) i formelen N; Det er fra her at verdiene 77,6 og 3,73 · 10^5 av formelen ITU ([Q]](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E. C. S. Megaw, "Fundamental radiospread utbredelsesteori", *Proceedings of the IEE Part C: Monographs*, vol. 104, n. 6, september 1957, s. 441–455, doi 10.157.1pi-c.1957.0052: spekter av indekssvingninger fra turbulensteori, fullstendig sporing av spread utover den felles horisonten og effekten av geometrien betalt ved IET ([Shilit](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)-kortet).
- *Proceedings av IRE*, oktober 1955, spesialnummer på spredning av scatter (av K. A. Norton og J. B. Wiesner), med Bullington, Staras og andre: gratis i PDF på [World Radio History](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf).
- D. E. Kerr (redigert av), *Propagasjon av korte radiobølger*, MIT Radiation Laboratory Series vol. 13, McGraw-Hill 1951 (Restampe Dover 1965 og IEE 1987): teori om fordamping og overflatekanaler på havet. Digital kopi: [World Radio History](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); IET utgave i [IET Digital Library](https://shop.theiet.org/propag-short-radio-waves).

Nylige tiltak for troposferisk diffusjon i VHF: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Australia), " En studie av VHF Troposcatter kommunikasjonskanaler i Sør-Australia", i *Digital signalbehandling for kommunikasjonssystemer*, Springer 1997, s. 103–112 ([Springer](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)]. Tekst for et gebyr; fra abstrakt det gjelder diffusjon, ikke kanalene. Nyttig bare hvis du vil kalibrere termen Troposcatter med VHF-kanaldata.

Numeriske modeller og sjøruter (indikert av forfatteren av programmet)

- M. Levy, *Parabolske ligningsmetoder for elektromagnetisk bølgeutbreiing*, IEE Electromagnetiske bølger Series #45, 2000, 336 sider (ISBN 0852967640): parabolisk ligning for brytningsprofiler som endres langs banen og for orografi; kapitler om troposferøs utbreiing, stråler og måter, utbreiing på havet. Det ville tjene til en feltberegning langs banen, utover indeksen. For betaling, også i elektronisk form fra IET ([IET kort](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)).
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (University of Leicester): 2 GHz målinger på Kanaløyene og Kanalen. Mange verk og avhandlinger er gratis i universitetsarkivet: [langsiktig statistikk om fordampningskanalen, Radio Science 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434), [signaturendringer på tre sjøruter, Radio Science 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710), [Tittler av N. Mufti 2012 med ruter på 50 og 140 km også i VHF](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854), [tesis av I. Alam 2002 om reaktivitet beregnet ut fra værdata, 240 MHz og 2 GHz X3 UQ, [tesi.....................................................................](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus, "Praktisk anvendelse av en fordampningsmodell", Radio Science vol. 20 n. 4, 1985, s. 887 (ikke IEEE-transaksjoner på antenner og utbreiing), doi 10.1029/RS020i004p00887 ([scheda](https://zendy.io/title/10.1029/rs020i004p00887)]: høyden på fordampningskanalen fra overflateværdata. Frigjør den tekniske rapporten før den: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf).

T. Lindquist, *Wave-utbredelsesmodeller i Troposfæren for Long-Range UHF/SHF Radio Connections*, masteroppgave, Universitetet i Karlstad, 2. juni 2020, 106 sider (utgitt av forfatteren av programmet). Det er en trinn-for-trinns guide, gratis, å skrive en beregning med parabolisk ligning: split-trinn løsning (selv i brede vinkler), grensebetingelser for land og hav, bakke til trinn, strålesporing på lineære profiler til tider, med numeriske kontroller; citerer PETOOL gratis verktøy for Matlab. To nyttige data på én gang: (1) N følsomhet for værvariabler, δN ≈ 0,26 δP + 4,3 δe − 1,4 δT (hPa og K, ca. 20 °C), dvs. en feil på 1 hPa på damptrykket veier tre ganger en 1 °C-feil på temperaturen: er fuktigheten av modellen som må kontrolleres først; (2) Gjennomsnittlig høyde på fordampningskanalen: 5–6 m i Nordsjøen, 13–14 m i Middelhavet, over 20 m i Persiabukten, alltid for lavt for 144 og 432 MHz. Forfatterens konklusjon (fregnigheter 1 til 10 GHz): Utover horisonten krever de pålitelige tilkoblingene nesten alltid en kanal.

Utbreiing programvare på jordprofil (for horisont og hindringer, ikke for forventet tropo)

- [Radio Mobile](https://ve2dbe.com/rme.html)av Roger Coudé VE2DBE: gratis, Windows (også Linux og Mac med vin), Longley-Rice/ITM-modell, terrengprofil automatisk ekstrahert fra kvotedata ([nedlast](https://ve2dbe.com/download/download.html)].
- [SPLAT!](https://qsl.net/kd2bd/splat.html)av John Magliacane KD2BD: GPL, 20 MHz–20 GHz, Longley-Rice og ITHOM v3.0 modeller, hindring og tap rapporter i tekst; siste versjon 1.4.2 av 2014.
- [ITM, NTIA/ITS](https://its.ntia.gov/software/itm)offisiell kode: Longley-Rice-modellen (20 MHz–20 GHz) med kilde og dokumentasjon, basis for Radio Mobile og SPLAT!. Koden er fri, og du kan legge til beregningen på banen til LIVETROPOVHF (tap på grunn av jorda, som skal legges til tropoindeksen). Opprinnelig rapport: A. G. Longley, P. L. Rice, - Prediction of Tropospheric Radio Transmission Tap Over Uregelmessig Terrain: En datamaskinmetode – 1968", ESSA Teknisk rapport ERL 79-ITS 67, juli 1968 (NTIA kort på historisk programvare: X1 https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITU-R, programvare og digitale data](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): gratis kart over ΔN og N0 årlige medianer på rutenett 1,5° som brukes av P.452 og P.617 — nyttig som referanse klimaverdier for indeksen.
- Pathloss (Contrakt Telecommunication Engineering): kommersielt program for radiobroer; Jeg har ikke funnet en offentlig kilde som bekrefter hvilke ITU anbefalinger implementerer, slik å sjekke.

Reusable GitHub Code i LIVETROPOVHF

| Prosjekt | Hva gjør du? | Språk | Bruk mulig for oss |
| [NTIA/ITM](https://github.com/NTIA/itm) | modell Longley-Rice offisiell fra NTIA, med pakker til. NET |  C++, C#  | land tap på banen: integreres direkte i programmet, som er i C# |
| [fuzzzee/itm](https://github.com/fuzzzeee/itm) | ITM C++ kode C# oversettelse |  C#  | alternativ uten innfødte biblioteker, også nyttig for Windows 7 versjonen |
| [eeveetza/Py452](https://github.com/eeveetza/py452) | ITU-R P.452, identisk med MATLAB/Octave-versjonen godkjent av ITU-R WP 3M; Matlab-versjonen publisert av det sveitsiske føderale kommunikasjonskontoret | Python, Matlab | referanse til å oversette til C# Troposcatter-formler og utført, med ITU-valideringseksempler |
| [bwinkel/pycraf](https://github.com/bwinkel/pycraf) | P.452 komplett, P.676, SRTM-profiler; GPL v3, men ITU-datafiler inkludert er ikke gratis for kommersiell bruk | # | kontroll av resultatene |
| [ITU-Rpy](https://github.com/inigodelportillo/ITU-Rpy) | mange ITU-R anbefalinger, inkludert P.453-13, validert med ITU eksempler | # | verifisering av N-formel og klimakart av P.453 |
| [NCAR/RAOBget](https://github.com/NCAR/RAOBget) | Last ned radiobølgen til University of Wyoming i tekstformat | # | diagram å laste ned i C# profilene til de italienske stasjonene |

Merk: den aktuelle versjonen av P.453 er [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), nedlastingsbar gratis; i dokumentet jeg brukte de 13 til å bli reallined.

Allereie i programmanualene og gjenbrukbar her: LA0BY (Nordic VUSHF 2019), IK3XTV ("Propagasjon på 2 meter" og "Propagation VHF – konfektive celler og gravitasjonsbølger", Radio Magazine, gitt av programforfatteren), DF5AI/DK5YA (2003), G3YGF (QST 1983), VHF/UHF DX Book cap. 2, G4MCU (2003) KP4MD
