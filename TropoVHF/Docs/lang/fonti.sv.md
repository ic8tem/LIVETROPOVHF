# Källor och formler

> Automatisk översättning av italienska: I händelse av tvivel är det värt den italienska guiden.

Arbetarsamling av källor som konsulterats för LIVETROPOVHFs tropoprognos (20–21 november 2026): Grundläggande fysik, tillgängliga data, publicerade index, formler, amatörradioupplevelser, validering och beslut. Formlarna är numrerade som i LaTeX, (1), (2),... och i texten de minns med numret i fästen. Prognoshandboken beskriver hur programmet använder dem.


## 1. Författarens lokaler

IC8TEM observationer som ramar in arbetet, med den feedback som finns i källorna.

- Italiensk geografi kräver diffraktion. Med dorsal, promontory och öar några dussin kilometer från stationerna beräknas diffraktionsbidrag med Fresnel-parametern på huvudkanten, inte bara med det släta landet. Möte: I en jämförelse på oregelbunden terräng vid 563 MHz (Rio de Janeiro, 27 poäng mellan 5 och 30 km), var de bästa metoderna P.526 med bladkanter, P.1812 och den diffraktiva kilen, med genomsnittliga fel mellan −5 och +3 dB och spridning av ca 9 dB; P.1546 var den mest pessimistiska (från Costa, Rabanal Ramirez och För branta sluttningar nära PETOOL stationen minns att de serverar breda vinkel propagnen och tvåvägarna.
- För att komma in i en avlägsen kanal behöver du Fresnel. Om ledningen börjar utöver horisonten kommer signalen till oss genom diffraktion över hindren fram till dess början; från 1,3,2 beräknar programmet det (Prevision Manual, § 10 ter). Rohde & Schwarz (2023) konstaterar att stationer längs en hög kanal ofta misslyckas med att komma in, medan de nära ändarna ja.
- Villkor förändras snabbt. Teorierna finns där, men villkoren förändras i minuter, och inget prognossystem tar data i realtid: GFS kommer ut var 6: e timme, med tidsspann och 0,25°, och en prognos kan förlora värde inom en kort tid. Möte: Bray (QST, november 1961) mätning av timmedian fluktuationer på cirka ± 7 dB över 100 miles; kanalerna rör sig med atmosfärisk tid (Rohde & Schwarz), vårt QSO-register av 29/12/2021-2/1/2022 visar öppningar som växer och faller inom timmar.
- Den bästa nu-casting förblir DX kluster-PSKReporter nätverk. För att veta vad som händer nu slår klusterplatser, PSKReporter (och WSPR) alla mönster: de är åtgärder. Prognosen säger var man ska titta och hur länge det kan vara; reklam säger om det är där. Programmet för detta sätter plats och prognos på samma karta.

## 2. Syfte

Tanken är ett tredje index, beräknat av LIVETROPOVHF själv från de vertikala profilerna av en vädermodell, att vara tillsammans med Hepburn och F5LEN istället för att ersätta dem. Idag läser programmet sina kartor redan redo: ett index skulle också säga * varför det finns öppning (kvot och tjocklek av ledningen, minsta band fångad) och det kan inträffa med de platser som programmet samlar in redan.

- Vad kan du ge: sannolikhet för ledning per punkt och per timme, andel av basen och toppen, tjocklek, minsta frekvens fångad, och längs en väg bråkdelen av rutten inuti en ledning.
- Vad som inte kan ge: de tunna kanalerna (under några tio meter) som de globala modellerna vid 0,25° inte löser, och avdunstningskanalerna på havet, som tjänar framför allt från GHz upp.
- Första praktiska målet: 144 och 432 MHz i Europa, med verifiering på DX klusterplatser, PSKReporter och lokala avkodningar.

## 3. Grundläggande fysik

Allt börjar från refractive N beräknas på varje profilnivå: ledningen är där den modifierade reaktiviteten M minskar klättring, det vill säga där dN / dh faller under −157 N / km ([ITU-R P.453-13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)).

Refraktivitet (P och i hPa, T in kelvin):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Ångtryck från relativ fuktighet H (%) och temperatur t (°C), över vatten mellan −40 och +50 °C:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

Modifierad reaktivitet (h i km, eller 0,157 · z med z i meter) och kanalkriterium:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN/dh (N/km) | Förordning | Effekt av 144 MHz |
| 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 | sub-refraction | minskad flödeshastighet |
| från 0 till −79 | normalt (standard om −40) | radiohorisont med motsvarande radie 4/3 |
| från −79 till −157 | super-refraction | Långt flöde, "för bra" |
| under −15 | Kondukt (fångst) | Fångade signaler, DX över 1000 km |

Hur man extraherar N från en modell på trycknivåer: för varje nivå du tar T, relativ fuktighet och geopotential höjd, beräknar du N och M, sedan gradienten mellan angränsande nivåer. Nivåerna under marken bör kasseras (vid 1000 hPa på Alperna är under jord).

Minsta fasta frekvens: ledningen guidar bara vågorna ganska kort jämfört med dess tjocklek. Hepburn publicerar en tabelltjocklek → minst användbar frekvens ([DX Info Centre](https://www.dxinfocentre.com/propagation/luf.htm)); Det är en mjuk gräns, under vilken signalen gradvis sjunker.

| Led tjocklek | Minsta betalande frekvens |
|  61 m  | 962 MHz |
|  91 m  | 464 MHz |
|  122 m  | 276 MHz |
|  152 m  | 185 MHz |
|  183 m  | 133 MHz |
|  213 m  | 101 MHz |
|  305 m  | 53 MHz |

I VHF-sortimentet (ofta 120-300 m) följer tabellvärdena ganska bra en lag om konstant; under 100 m underskattar formeln frekvensen upp till 30-40%. Anpassa det själv (inte en formel publicerad av Hepburn), med f i MHz och d i meter:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

I grund och botten, för 2 m, är en ledning ofta minst 170-180 m, för 70 cm det är ungefär hälften. Formeln som cirkulerar på nätet, λmax = 0.014 · d^1.5, kommer från Kerr (1951, kapitel 1, q. 18) och är giltig med λ i centimeter och står, för en ledtråd där M faller linjärt: med dessa enheter ger ca 2 m 180 m och för cirka 70 cm 89 m, det är samma värden av Hepburn-bordet, som kommer från det. Kerr varnar för att det inte är ett nettosnitt: även vågor flera gånger längre lider mycket av ledningen, och ett sätt som "förlust" lite kan förbättra mottagningen även med stationer ur ledningen. En studie från 2019 IEEE Access bekräftar att klassiska metoder (Kerr, Kukushkin) inte beskriver det verkliga snittet ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)): tröskeln ska användas som en indikation, inte som en vägg.

## 4. Data tillgänglig för gratis

Den svaga punkten är den vertikala upplösningen: mellan 1000 och 850 hPa-modeller ger en nivå var 25-50 hPa, det är 200-450 m, så de ser väl inversionerna till 500-2000 m men inte de tunna kanalerna nära marken. LIVETROPOVHF använder redan Open-Meteo för att isobare, temperatur till 850 hPa och inversion: indexet kan börja från samma samtal.

| Källa | Vad ger | Resolution | Använd för index |
| [Open-Meteo, GFS](https://open-meteo.com/en/docs/gfs-api) | T, relativ fuktighet, daggpunkt, geopotential höjd per nivå (1000, 975, 950, 925, 900, 850 hPa...); höjden av gränsskiktet | 0,25° på trycknivåer; 16 dagar, timmar upp till 120 h, uppdateras var 6 h | profil N och M, gradient, bas och toppen av kanalen; redan i programmet |
| [Open-Meteo, ECMWF](https://open-meteo.com/en/docs/ecmwf-api) | samma nivåer från open-data modeller 0,25°; marken ger daggpunkt |  0,25°  | andra åsikten, användbar för samspelet mellan modeller |
| [Open-Meteo, DWD ICON](https://open-meteo.com/en/docs/dwd-api) | T och relativ fuktighet på trycknivåer; ICON- D2 finns 800, 900 och 925 hPa | ICON-EU ca 7 km | tredje yttrande om Europa |
| [Radiosunds, Univ. Wyoming](https://github.com/NCAR/RAOBget) | mätta profiler, två lanseringar per dag (00 och 12 UTC), textformat per station | många betydande nivåer, även varje litet dussin meter | indexverifiering; se de subtila kanalerna som modellen inte ser |
| [GROANS, NOAA NCEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)] | historiska arkiv för sändning, en fil per station, mer härledda parametrar | som ovan | kalibrering på år av data och jämförelse med de senaste platserna |
| ERA5 (Copernicus) | timmars reanalys sedan 1940, 37 nivåer av tryck | ca 28 km | Statistik och kalibrering, inte prognos |

- Med Open-Meteo geopotential höjd är över havet: de nivåer som faller under marken måste kasseras ([Open-Meteo, övre luft](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)).
- På marken läggs T och fuktighet på 2 m: de ger den första gradientsträckan, den där nattytan kanaler föds på slätten och på havet.
- Italienska sändningar på University of Wyoming arkiv (weather.uwyo.edu/upperair/sounding.html): stationer och siffror WMO i tabellen nedan.

| Station | W W W | Latitud | Längd | Dela |
| Udine/Campoformido |  16044  |  46,03  |  13,18  |  94 m  |
| Milan/Linate |  16080  |  45,43  |  9,28  |  103 m  |
| Öva havet |  16245  |  41,65  |  12,43  |  12 m  |
| Brindisi |  16320  |  40,65  |  17,95  |  10 m  |
| Trapani/Birgi |  16429  |  37,92  |  12,50  | ca 14 m |

Textformatet Wyoming ger för varje nivå tryck, höjd, temperatur, daggpunkt och fuktighet: tillräckligt för N och M. Wyoming har inte ett officiellt gränssnitt, skript används som [RAOBget av NCAR](https://github.com/NCAR/RAOBget); College of DuPage håller kopior i text per station (t.ex. 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)).](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. Index och publicerade metoder

Hepburn och F5LEN startar från samma fysiska ingredienser men publicerar inte hela algoritmen; de vetenskapliga studierna 2008–2026 beskriver istället metoden steg för steg, och det är därifrån som det är bäst att kopiera receptet.

| Metod | Starta data | Vad beräknas | Användbara anteckningar för oss |
| [Hepburn Tropo Index](https://www.dxinfocentre.com/propagation/hti.htm)] | råa vädermodeller av Canada Meteorological Service (MSC, som skrivits under sina kartor; inte GFS / NAM), kartor designade med GrADS, egenutvecklad algoritm ([storia](https://www.dxinfocentre.com/propagation/history.htm)) | 0-10 linjär skala av signalkurvatur: 0 normal, 1-2 superrefraction, 3-4 vissa kanaler, 5-6 diffusa kanaler; negativa värden = subrefraction | Skalan som ska imiteras, så det tredje indexet jämförs omedelbart med de två första |
| F5LEN, världsprognos](https://tropo.f5len.org/WW/info.php) | för Europa ARPEGE-modellen av Météo-France, 7,5 km, fler nivåer mellan 2 m och 1500 m (notera av författare](https://tropo.f5len.org/author/f5len/)) | Genomsnittligt index för hyresvärd, 40 steg från 3 timmar, 4 beräkningar per dag, 28-dagars arkiv ([ny version](https://tropo.f5len.org/?p=504)) | bekräfta att den tjänar en modell med många låga nivåer |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)] | 60 ECMWF-modellnivåer, från mark till 3000 m | Minimal dN/dz per kolumn: genomförd under −157 N/km, superrefaktion under −78,7 N/km; bas av fällaskiktet | Klimatologi: Po Valley det europeiska området med flera rörledningar året runt; Medelhavet på sommaren nära 100%, på vintern under 15%; Italien över 30% på sommarnätterna |
| [Indermuehle och Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861) | GFS 0,25°, 21 trycknivåer + fält vid 2 m | M = N + 0,157 z; minst dM / dz under 3 km, bas och toppen av kanalen, djupet av M-underskottet; under 40 m kasserad | verifierad i 7 år på 25 kanaler från 88 MHz till 2,68 GHz; god kapacitet upp till cirka 3 dagar i förväg (AUC ca 0,74), faller sedan; GFS bättre än ERA5 |
| [Hämäläinen et al., IMF 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf) | Harmonie-AROME, 750 m eller 2,5 km, 65 eller 90 nivåer | dM/dh <0 på modellprofilen, jämförelse med en 4 till 59 m stråle | räknar mer vertikal än horisontell upplösning; ingen modell ser kanalerna under 15 m; fukt dominerar på temperaturgradienten |

Tredje amatörradiotjänsten är redan aktiv: [TROPOCAST.EU](https://tropocast.eu/)av Artur SQ3A, för 2 m och 70 cm i Europa. Från prognossidan ([tropocast.eu/forecast](https://tropocast.eu/forecast)):

- Data: DWD ICON-EU (pass 0.0625°, ca 7 km), standardkälla; alternativt kanadensisk GDPS (0.15°), mindre detaljerad.
- Metod: N och M enligt ITU-R P.453 från profilen mellan lagret nära marken och 850 hPa; erkännandet av lagren, förening av kanaler till marken och höga och trösklarna av färgerna är en korrekt kalibrering av författaren, inte publicerad.
- Produkter: "fullständig" vy 0-3000 m, mer separata vyer "till marken" och "hög" (för närvarande endast i kartorna som ska införlivas); poäng 0-100 i nio klasser (under 38 bakgrund, 38-52 marginal, 53-58 svag, 59-65 måttlig, 66-72 bra, 73-80 mycket bra, 81-88 hög, 89-95 stark, 96-100 mycket stark); prognos upp till +72 h vid steg av en timme, med animation och arkiv.
- Områden: Europa, Polen, Storbritannien, Tyskland, Frankrike, Spanien, Italien, Tjeckien, Kroatien; gränssnitt också på italienska.
- Återanvändning: kartor införlivas med en API nyckellös iframe; Det finns också en dokumenterad API (OpenAPI och JSON schema). Licensen anges inte: Innan du använder den som en tredje källa bredvid Hepburn och F5LEN bör du fråga författaren.

Skillnader från vårt projekt: TROPOCAST stannar vid 850 hPa (ca 1,5 km), medan de förhöjda kanalerna anländer till 2000 m och bortom ( LA0BY fall av 22/11/2018), och ger inte ett index för band eller rutt. Klasserna 0–100 kan jämföras med vårt index 0–10 samma dagar.

TROPOCAST bildstruktur (från länken som tillhandahålls av programmets författare): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, där n är prognossteget. Webbplatsen förbjuder automatisk återkallelse till robotar, så jag kunde inte läsa bilden; också för detta, innan jag visar dessa kartor i LIVETROPOVHF tjänar författarens tillstånd.

Anteckningsbok: [DJ5AR, kategori](https://dj5ar.de/?cat=174)(Magonza, 21 artiklar 2017–2023, 2 m–13 cm). Fall beskrivs: genomfördes flera veckor från Irland (23 maj-2 juni 2023), FT8 till Vitryssland (september 2020), Italien-Tyskland över Alperna och Adriatiska havet (augusti 2017). Känner igen fallet med "för hög" kanalen (omvänd till 1000 m) som du inte kan använda från marken: en annan bekräftelse på att det är nödvändigt att jämföra andelen av ledningen med stationerna. Använd radiophares som bländare (upp till 1028 km) och de vertikala profilerna i Paris, Bordeaux och DWD.

Sammanfattningsvis det gemensamma receptet: vertikal profil → N och M per lager → lager med dM / dh negativ → bas, topp, tjocklek och kraft av kanalen → index. Den hemliga delen av Hepburn är den empiriska kalibreringen på verkliga händelser, som vi kan remake med fläckar.

## 6. Användbara formler för programmet

Förutom N och M (grundläggande fysik avsnitt), dessa används för att flytta från en vertikal profil till ett punktnummer och sedan på väg. ITU-formler hade rekonstruerats av en PDF-läs delvis dåligt; den 22 november granskades på officiella texter ([ITU-R P.452-18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453-14, P.526-16, P.372-18) och korrigerades i koden: konstant av troposcatter 190 (inte 190,1) plus absorption av gaser med ρ = 3 g / m3; α inte under −3,4 (ek. 55a); gas av kantorna med = 5= 5= = 5= 5, 2, 2, 1) = 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2,5 = 4) = 4, 2, 2, 2, 2, 2, 2, 2, 2,5 = 4,5 = 4 Rumore P.372-18: tabell 1 och galaktisk korrespondens; Konstgjorda bullerkurvor är upp till 250 MHz och galaktiken upp till cirka 100 MHz, över programmet sträcker sig samma lagar.

1. Gradient mellan två nivåer av modellen (h i km, M i enhet M):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. Konduitstyrka och tjocklek: tjocklek d = topp - bas (för en jordkanal, bas = jord); styrka som M-brist mellan bas och topp.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3. Jordradieekvivalent faktor (P.452-18, q. 5 och 6a), med ΔN = genomsnittlig N-minskning i första km, positiv, i N/km:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

Med ΔN = 40 har du cirka 4/3 k (standardatmosfär); med ΔN närmar sig 157 k gardiner oändlighet, det vill säga jordens radio blir platt: det är början på ledningen.

4. Radiohorisont av en hög antenn h meter, med radie motsvarande \_e i km:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5. Sannolikheten för kanal för latitud (P.452-18, q. 2), tidsprocent; φ = latitud i mitten av rutten, μ1 och μ4 tar hänsyn till hur långt det är till havs:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

Vid 41° N, allt till havs (μ1 = μ4 = 1), ger cirka 11% av tiden: är den klimatologiska basen, användbar som ett referensvärde från vilket det förväntade indexet skiljer sig.

6. Förlust för troposcatter (P.452-18, ekv. 45; f i MHz, d i km, θ vinkel av vägen i mrad, N0 markreaktivitet i mitten av rutten):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

Termen −0,15 · N0 visar varför våt luft också hjälper utan kanal: 50 extra N-enheter är 7,5 dB mindre än förlust. LIVETROPOVHF använder redan G3YGF -versionen av scatterförlusten.

7. Förlust i kanal (P.452-18, q. 46-51): fast förlust av koppling plus en dämpning för vinkel milliradiant, som växer med den kubiska roten av frekvensen (f i GHz, a\_e i km):

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. Från Bean och Dutton (1966) och Kerr (1951)

De två klassiska texterna ger tre saker som behövs omedelbart till indexet: gradient tröskelvärden tar redan på samma nivåer av tryck av GFS, vad som är värt i dB en förändring av reaktivitet, och vilka vädersituationer skapar eller förstöra kanalerna. Radio Meteorology*, NBS Monograph 92.

1. Profilklassificering för tryckskikt (Bean och Dutton, flik 6.5, s. 248; −dN/dh i N/km). Utformad för radiovåglängder gäller det som det är på modellnivåer:

| Vägen | Aktier om | Sub-refraction | Inte laminerad | Super-refraction |
| 1000-850 hPa | 0–1,46 km | −dN/dh <0 | från 20 till 60 | över 100 |
| 850–700 hPa | 1,46–3,01 km | −dN/dh <0 | från 20 till 50 | över 80 |
| 700–600 hPa | 3,01–4,20 km | −dN/dh <0 | från 15 till 40 | över 70 |
| 600–500 hPa | 4,20–5,57 km | −dN/dh <0 | från 10 till 30 | över 50 |

2. Hur mycket är värt i dB (Bean och Dutton, cap. 6, pp. 233-258), åtgärder från 70 till 180 MHz på vägar utöver horisonten:

- Varje lager (på marken eller på höjden) jämfört med "smooth" -profilen höjer signalen på 10-25 dB till alla procentsatser; den större vinsten kommer från de höga lutade lagren, förmodligen på grund av fokuseringseffekt (kurs på 200 km).
- Grundreaktivitet Ns och nedgången i första km ΔN är relaterade till den månatliga mediansignalen med koefficient mellan 0,4 och 0,95, median 0,7 (20 linjer från 130 till 446 km, cirka 100 MHz).
- Årlig cykel: 0,18 dB per enhet N; daglig cykel från 0,2 till 1,1 dB per enhet N, maximalt på vägar på 175-200 km och vinter.
- Den sammansatta parametern för Misme uppskattar fältvariationen som 0,5 dB/(N/km) · (ekvivalent gradient − 40) plus en term av termisk stabilitet; parametern för turbulens D (område mellan den verkliga profilen och den adiabatiska en, 0-3 km) förbättras bara jämfört med den enkla ΔN. Slutsats av författarna: ΔN är fortfarande den bästa parametern för enkelhet.

3. Referensprofil och anomali (Bean och Dutton, § 3.8 och 3.9). Den exponentiella atmosfären av referens CRPL ger den "normala" profilen från Ns; En enhet mäter hur mycket den verkliga profilen skiljer sig åt vid varje aktie. Det är det naturliga sättet att omvandla en GFS-profil till en anomali för att sätta på kartan:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

Med Ns = 313 har du ΔN ca −42 N/km, det är motsvarande radie 4/3. −157 N/km conduit kriterium härrör också från Snell lag för en horisontell radie (eq. 4.23), nästan oberoende från antennhöjden.

4. Var och när ledningarna uppstår (Bean och Dutton § 4.4; Kerr cap. 3):

| Processprocess | Situation | Effekt |
| Varning | varm och torr luft från land som flyter på kallare hav (typiskt sommar ärm efter dagar av bra väder); huvudsakligen räknar fuktighetsunderskottet | intensiv ledning på havet |
| nattlig bestrålning | seren himmel, svag vind; låga moln eller mycket fuktighet förhindrar det | ledde till marken som föds vid solnedgången, växer på kvällen och försvinner snabbt efter soluppgången, särskilt inuti. |
| Subvention | luft som kommer ner från anticyklonen, uppvärmd och torr, över frisk och fuktig luft; frekvent översvämning i bergen och sydöstra sektorer av anticyklon | super-refraktivt lager eller hög kanal; nästan alltid gradient högre än standard i omvänd |
| Hav och land | kust, cirkulation av några hundra meter | lokala pipelines längs kusten |
| mot konduiten | blandning: mark uppvärmd av solen mellan mid morgon och eftermiddag, vind till marken över några m/s på grov mark; Natt dimma | sannolikhet för ledning nästan ingenting |

5. Två nya ingredienser att prova. Den vertikala rörelsen: uppstigande strömmar i konvergenszonerna tar bort och tar bort lagren, subventionen stärker signalen (Moler och Holden: i genomsnitt cirka 2,5 dB mer än med uppförsbacke luft, på havet i band L och X). Open-Meteo ger redan vertikal hastighet på trycknivåer. Och trycktrenden: på en väg på 140 km till 186 MHz nedsteg signalen när trycket sjönk till sändaren och till mitten av vägen och återvände högt och utan evanescens när det gick upp, med bara 2 (Bean och Dutton, fikon. 6.9, s. 241).

6. Från Kerr. Förutom formeln för den maximala våglängden (basfysiksektionen), ger Kerr en ungefärlig formel för M från den enda temperaturen, ångan och höjden (§ 3.9, med de gamla konstanterna 79 och 4800 sedan ersattes med 77,6 och 4810) och observerar att till en omvänd skärning av vinden kan generera vågor som svänger vid kanalens höjd, möjlig orsak till djupa flykter (§ 3.26). Italiensk källa rapporterad av Bean och Dutton och att söka: L. Bonavoglia (december 1958), "Correlation mellan meteorologiska fenomen och förökning..."

## 8. Från amatörradiopresentationer och tekniska rapporter (andra serien)

Sju dokument som tillhandahålls av programmets författare. Tre tjänar direkt till indexet: W3SZ för troposcatter "golv", LA0BY för fall Hepburn hade inte förutspått, Hepburn ( LU7MC) för väderklassificering av kanalerna.

1. Troposcatter: signalen alltid där. R. Rehr W3SZ, "Tropospheric Scatter (Troposcatter) Propagation for VHF, UHF, Microwave and Frequencies", Pack Rats Conference 2014; J. R. Duffey KK6MC, "The Troposcatter Propagation Mode and How to Predict Coverage on the VHF/UHF Bands", CSVHF 2017.

- Användbart från cirka 100 till 700 km, från 144 MHz till 10 GHz; förlusten växer med 30 logga f med frekvensen.
- Framför allt räknas startvinkeln mot horisonten: varje extra grad kostar ca 9-12 dB ( M1CNK, Collins metod).
- På sommaren signalen är i genomsnitt cirka 10 dB högre än på vintern, i mitten av eftermiddagen ca 5 dB lägre än i början av morgonen eller på kvällen. KK6MC indikerar en total variation på 13-15 dB med tid, säsong och tid.
- Yeh metod, det enklaste att programmera (f i MHz, d i miles, θ vinkel av scatter i grader, α geometriskt medelvärde av öppningarna av de två buntarna):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Jämförelse av Larsen (1968) på 15 verkliga anslutningar: genomsnittligt fyrkantigt fel 5,1 dB för Collins, 7,4 för Yeh, 8,1 för NBS och CCIR, 16,8 för Rider. Alla metoder förbättras om Ns håller stadigt (Yeh sjunker till 5,4 dB): för spridningen räknas dagens refraktion lite, så tropoindexet måste titta på lagren, inte bara Ns. Bordet på 15 anslutningar, med uppmätta förluster (inklusive Feldberg-Monte Baldo, 322 km vid 957 MHz, 223,1 dB; Cypern-Israel, 319 km vid 77,5 MHz, 163,3 dB), är en bra testbänk för vår beräkning.
- KK6MC ger förlusten att en typisk station lyckas överstiga 2 m och motsvarande avstånd, från 184 dB (FT-817 och 3 element i SSB, cirka 130 km) till 215 dB (långa bungs i SSB, ca 430 km) och 236 dB (kW och yagi kopplad, ca 640 km). Det är sättet att översätta index dBs till intjänade kilometer.
- Redo kod: W3SZ Yeh och Collins-programmet (nitehawk.com/w3sz/YehCollins.zip) och Bob Atkins KA1GT -kalkylator (bobatkins.com/radio/troposcatter.html).

En annan källa på scatter: I. Roberts ZS6BTE, "Tropospheric Scatter Propagation", *Radio ZS*, december 1984, med not från 2013 ([qsl.net/zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). Huvudsakligen Användbar geometri: vinkel av scatter θ = θ0 − θ1 − θ2, med θ0 = d/R, θ1 = (h1 − h\_ostacolo)/d1 + d1/2 R och samma för den andra stationen, R = 8446 km (4/3 av jordens radie). Det är den bit som används för att beräkna vinklarna från markens profil i troposcatter "golv". Andra data: varje vinkel i mer kostar ca 10 dB; växling från Ns 280 till 300 minskar förlusten av cirka 4 dB; scattervolymen är mellan 300 och 2000 m för 150 km, mellan 600 och 3000 m för 300 km; åskväder i scattervolymen höjer signalen på cirka 20 dB. det numeriska exemplet på artikeln returnerar inte med de givna formlerna (perioden för fritt utrymme på 900 MHz och 872 km är cirka 150 dB, inte 125), så formlerna bör kontrolleras på W3SZ innan de används.

2. Typer av ledning (W. R. Hepburn, "Tropospheric Propagation Modes", dxinfocentre.com/propagation/tr-modes.htm, spansk översättning av LU7MC). Slutför tabellen Bean och Dutton med användbara fall i Italien:

| Typ | När när | Notera för index |
| bestrålning | lugna och fredliga nätter på jorden | Följ marken, försvinner strax efter gryningen |
| Högt tryck (subsidence) | anticyklon | kan hålla hela dagen; på natten kan ledningen från bestrålning nedan blockera avlägsna signaler, så dagtid kan gå bättre |
| Framför | Framför en varm front, bakom en kall front, norr om en stationär front | Kort, det dåliga vädret stör honom |
| Mountain Descent | föhn, bora: luften kommer ner, värmer och torkar | att försöka bora på Adriatiska havet och föhn söder om Alperna |
| Varning | varm och torr luft över kall och våt jord, till exempel efter regn | ledde till marken |
| Valley Valley | varm luft över kall luft stannar i dalen | kan stanna på dagen |
| Marino | varm och torr luft på kallare vatten; anländer på kusten med brisen | topp på eftermiddagen; våren och början av sommaren de bästa årstiderna |

Avdunstningskanalerna på havet (0-30 m) tjänar bara cirka 3 GHz och går inte in i inlandet: för 144 och 432 MHz kan du ignorera. De höga bergen är en barriär; öknen är för torra.

3. Fallen Hepburn hade inte förutspått (S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", Nordic VUSHF 2019, från JO59IX till 500 m höjd):

- 22 november 2018: 70 cm upp till UA6 (ca 2600 km) i 6-8 timmar. Hepburn angav endast de baltiska staterna och Polen. Kanalen sågs i temperaturkartorna på 900 och 1500 m och slutade drygt 2000 m; den var högre på den norska sidan, och endast LA0BY på kullen gick in. På marken verkade inte som en trope: kall och dimma på toppen.
- 11 februari 2015: 2 m och 70 cm mot 9A och S5 (cirka 1500 km) genom Alperna, även detta inte förutses av Hepburn.
- 15 oktober 2011: 2 m med EA1 (2135 km) i hög kanal, på tre sträckor av havet och tre marken.
- Andra händelser: 6–11 november 2003 österut (2050 km), 10 december 2004 mot Frankrike.
- Fältadresser: dimma i vanliga, fria toppar, liten vind, våt mark. Normal temperaturgradient −6,5 °C/km; i ett verkligt fall ( VK3KAQ) +7 °C på 20 m.

Författarens slutsats: Hepburn och F5LEN räcker inte, de tjänar profilerna på höjden längs rutten. För oss: dessa datum är fall av kontroll som ska beräknas med de historiska uppgifterna i modellerna, och indexet på vägen måste jämföra kvoten av kanalen med kvoten på stationerna.

4. italiensk erfarenhet (G. IV3GBO, "Types of VHF propagation SSB/CW", 2017): genomförd mellan 450 och 3000 km, med lager mellan 450 och 3000 m; dimma utsträckt som en ledtråd; öppningar till havs på Adriatiska mot Puglia, Albanien och Grekland. Scatter från 100 till 500 km.

5. Introduktioner (P. Wilton M1CNK, "En introduktion till VHF/UHF Propagation", 2013): 70 cm är ofta bättre än 2 m; med 10 m pol vid 144 MHz är startvinkeln cirka 3 °, med 20 m cirka 1,5 °.

NASA-rapport (E. Hirschmann, "Troposfäriska och jonosfäriska effekter på radiofrekvens (VHF-SHF) kommunikation", Goddard X-731-67-89, 1967): det handlar om distansfel till satelliter, inte DX. Det ger tillbaka samma N-formel och exponentiell profil (t.ex. i Florida: Ns 370, ΔN −55, konstant 0.161/km) och skickar två NBS-källor för att söka: Bean och Thayer, "CRPL Exponential Reference Atmosphere", Monograph 4 (1959), och Bean, Horn och Ozanich, "Climatic Charts and Data of Radio Refractive Index for the United States and the World", Monograph 22 (1960).

## 9. Från VHF/UHF DX Book, kapitel 2

I. White G3SEK (redigerad av), * VHF/UHF DX Book*, DIR Publishing 1995 (ISBN 0 9520468 0 6; digital replika 2017), kapitel 2 "VHF/UHF Propagation", s. 2-13, 2-22-2-29 och 4-11. Det är den närmaste texten till radioamatörupplevelsen bland dem som samlats in, bekräftar de tröskelvärden som redan används och lägger till nya synoptiska regler.

Nummernummer

- Samma formel av N (77,6 P / T + 3,73 · 10 ^ 5 e / T2); exempel: 1000 hPa, 7 ° C och = 10 hPa skada N = 324. I en välformad atmosfär, till exempel efter en åskväder, är N nästan linjär cirka 39 enheter / km i de första 2-3 km.
- Gradienter mellan 0 och −79 N/km är normala, under −79 finns det överrefaktion, över 0 underrefaktion. Boken insisterar på att den långa DX inte kommer från den enkla superrefraktionen, som borde förbli rätt på hela vägen, utan från kanalerna.
- Minsta kanal tjocklek, lite mer försiktig än Kerr och Hepburn:

| Band | Minsta tjocklek |
| 50 MHz |  400 m  |
| 70 MHz |  300 m  |
| 144 MHz |  200 m  |
| 432 MHz |  100 m  |
| 1.3 GHz |  50 m  |

- Förhållandet är inte proportionellt mot våglängden (cirka 65 λ vid 50 MHz, 220 λ vid 1,3 GHz) eftersom kanterna av ledningen är "split". I Storbritannien är rörledningarna frekventa på 144 MHz men nästan aldrig på 70 MHz eller lägre, och ofta går 432 MHz starkt när 144 är svaga.
- I rörledningen sjunker signalen som 1/d istället för 1/d2: upp till 1000 km kan dämpningen likna det fria utrymmet.
- En lägsta stationskapacitet på 2 m (100 W, 15 dBi yagi, 2 dB buller): 217 dB, det är cirka 400-500 km i SSB i genomsnittliga förhållanden; i CW ca 20 dB i mer (p. 4-7). Håller med KK6MC -bordet.
- Scatter: förlusten stiger med cirka 15 dB genom att tredubbla frekvensen och 10 dB för varje grad av scattervinkel ( Yeh formel, i texten extraheras konstanten är oläslig, så det måste tas från versionen av W3SZ). "up" scatter från konvektiva celler täcker rutinmässigt upp till cirka 500 km, 1000 km med bra stationer. Efter kraftiga regn och långa förhållanden är knappa på alla band.

Typer av duct

- På marken: på jorden sprider de sig illa eftersom jorden absorberar och sprider sig; på det lugna havet kommer långt (Scilly-Canarie, 2500 km, ungefär en gång om året på 2 m). Avdunstningskanalerna är ofta bara cirka 15 m långa och serverar mikrovågor.
- Hög: mellan 450 och 2000 m, på grund av den längre DX på marken; signalen "hög" ovanför stationerna nedan. De "förlorade" kanalerna ger mindre starka signaler men tillåter också mellanliggande distansstationer.
- Kanalerna har exakta riktningar och kan hålla timmar eller dagar, i motsats till den sporadiska E.

Nya synoptiska regler för index

- Ålder av anticyklon: luften sjunker till mitten av några meter per timme och det omvända sänks när det höga trycket utvecklas; de större öppningarna anländer efter flera dagar av förökning redan bra. En kortvarig anticyklon räcker inte.
- Högtryckskant: lagret är kupol och vid kanterna är lägre, så stationerna blir bättre.
- Riktning: Det har observerats att de längre rutterna går längs isobares eller genom dorsalerna (Flavell G3LTP, 1984), men det finns utmärkta förbindelser också i radiell riktning från centrum. Exempel på 29–30 augusti 1987: från England roterade öppningen 180° på två dagar (Spanien, Frankrike, Italien, Österrike, till Norge) efter anticyklonskiftet.
- Mot: tjock isobare (stark vind), stratocumuliform moln vid kanten av lagret (gräns den högsta frekvensen).
- Front: en kall front trycker varm luft upp och skapar en omvänd längs framsidan; den bästa riktningen är längs framsidan medan den passerar, men den varar lite.
- Säsong: I Storbritannien är anticykloner oftare i slutet av sommaren och början av hösten.
- Visuella ledtrådar: rök fångad i en mörk linje ovanför horisonten; med högt tryck indikerar lagerkumuler med bas under 1000 m en markkanal, bas över 1000 m hög kanal.

Referenser som citeras för att söka: R. G. Flavell G3LTP, "Studies of an Extensive Anticyclonic Propagation Event...", *Radio Communication *, februari 1984, s. 128; J. Gannaway G3YGF, "Tropospheric Scatter Propagation", *Radio Communication *, september 1981, s. 710; P. W. Sollom G3BGL, "A Little Flutter Propagation", " W6ZGN

## 10. En verifierad amatörradiomodell: Biancomano och Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, "Practical Tropospheric Model for VHF/UHF Propagation Prediction", *National Weather Digest* 22:2, juni 1998, s. 15-24, gratis i [PDF från National Weather Association](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf).

- Data: radiovåg på 00 och 12 UTC av 47 stationer i östra USA, 1995-1997; markanalys till 700 hPa (i början upp till 400), på N-profilen, utan M. Exklusive kuster, öar och berg.
- För varje undersökning: närvaro och andel av kanalen, den maximala våglängden som kan leda (ofta cirka 100 gånger våglängden), den maximala gradienten av den potentiella temperaturen mellan angränsande nivåer och mellan en nivå och den andra ovan (för tjockare lager). Markkanalerna används inte.
- Första regeln, uthållighet: stabilitetsindex (lyftat index, K, totaltal, SWEAT) förblir inom 3 enheter från undersökningen 12 timmar före, och trycknivån där den potentiella temperaturen växer mer förblir ungefär densamma. Om så är fallet: förbättringsprognos under de närmaste 12 timmarna.
- Andra regeln, dynamiska kontroller: gränsskikt som inte stiger, CAPE, variation av tjockleken till 500 hPa på tre undersökningar, variation av sluttningen inom gränserna.
- Bandtröskeln: en ledning som kan bära 100 cm eller mer indikerar öppning på 432 MHz, nästan säker på 222 och ofta även på 144.
- Rymdfilter: stationer på ett hexagonalt rutnät på cirka 400-500 km; prognosen är bara värt om minst 5 contigue stationer är positiva (om du kan hoppa över en). Med 5 stationer sannolikheten för en falsk slumpmässig positiv blir försumbar.
- Verifiering mot händelserna i kolumnen "The World Above 50 MHz" av QST: 1996 planerade 48 händelser på 67 (71%); 12 av de 15 stora händelserna. Noggrannhet upp till 70% på 432 MHz och cirka 60% på 144 MHz på 12 timmar; Det ger nästan dubbelt de faktiska händelserna; 9 fel av 19 är förutsägelser anlände sent, eftersom många förbättringar bildar på mindre än 12 timmar.
- Läggs till stationerna längre norrut: vindrutterande klockklättring (varmstart) som extra skick.

Från den kompletta PDF-filen (som tillhandahålls av programmets författare) ytterligare tre detaljer. Deras regel "tjocklek = 100 gånger våglängd" ger 600 m för 6 m, 200 m för 2 m, 125 m för 1,25 m och 70 m för 70 cm (fig. 3): för 70 cm är mindre allvarlig än Kerr och Hepburn (cirka 89 m), för de 2 m det sammanfaller med VHF / UHF DX Book. Vinden som roterar medurs går upp, enligt författarna, särskilt eftersom det ger varmare luft på höjd, vilket är vad som gör N-fallet växa med tryck; fukt det medför inte verkar destabilisera. Bilagan erhåller dN/dh i sluten form för en konstant termisk gradient atmosfär med ett skarpt hopp av värme gradient och fuktighet vid en given kvot: användbar som ett testfall för vår kod. Deras spår av sluttningen av den potentiella temperaturen kommer från K. H. Jehn, "Användningen av potentiellt refraktivt index i synoptisk skala radio meteorologi", * J. Meteorologi * 17, 1960, s. 264.

Vad du ska ta för vårt index: (1) en period av uthållighet: jämföra profilen med den 12 timmar tidigare, att i den timprognos vi har fri; (2) rymdfiltret: en punkt räknas endast om förbättringen täcker ett område på några hundra kilometer, vilket minskar falska larm; (3) stabilitetsindex, beräknat från temperatur och dagg till 850, 700 och 500 hPa (Open-Meteo redan ger upplyst index och CAPE); (4) vinden som roterar med höjden mellan 1000 och 850 hPa; (5) deras metod för verifiering, vilket är samma som vi vill göra med

En annan källa i samma paket, med liten användning för oss: P. K. Rastogi, "Stratosfären och Troposfären (S-T) studier vid Millstone Hill", NASA 1984 (dokument 19840019069): 440 MHz radar som mäter turbulensen av klar luft mellan 4 och 25 km, med tunna lager kopplade till gravitationsvågor och vindskärning. Det bekräftar att turbulensen som producerar scatter är skiktad, men det gäller för höga odds för kanalerna.

## 11. Tredje omgången av källor (20 november)

High duct historiska fall. J. Macoun OK1VR, 2 m från toppen av Snãka (1602 m), hösten 1958, artikel 1959 översatt 2006 ([ok2kw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)). Den 27-28 oktober 1958 första Tjeckoslovakien-England anslutning på 145 MHz ( G5YV, 1220 km) och många fler mellan 1100 och 1250 km, med stabila signaler efter 23. Författaren tillskriver den till två överlägsna inversioner, den övre ca 2200 m, med signalen som återvänder till marken endast där det nedre lagret upplöstes; villkor som gynnas av en långsam anticyklon från Atlanten till kontinenten. Från hemmet (Prag) var max 530 km: endast stationerna på höjden gick in i ledningen. Det är ett annat fall av verifiering (t.ex. LA0BY 2018) och bekräftar att indexet ska leta efter över 2000 m och jämföra det med stationernas andel.

Historiska bibliotek NBS (användbart för att söka efter klassiska verk, inte för formler):

- W. Nupen, *Bibliografi om Tropospheric Propagation of Radio Waves*, NBS Technical Note 304, 1965, 356 sidor, med index för ämnen (även "villkorprognos") och land. Italienska påtryckningar rapporterade: L. Sacco, "Den radioanslutning i atmosfärisk super-refraction regim", * Hög frekvens * 24(6), december 1955 (italiensk forskning: super-refraction under 500 m frekvent dag och natt); S. Silleni, * Annali di Geofisica * 8 (1), 1955; J. M. Clara e A. Antinori, Prova tra Sarde e Minorca da 238 a 297 MHz (390 km), * Electrical Communication * 33(2), 1955.
- R. L. Abbott, *Bibliografi av Tropospheric Radio Wave Scattering*, NBS, material fram till januari 1960, cirka 670 poster beställda per år: användbar för troposcatter delen.

Avslöjande och observationer: F. W. Odenthal DK5EQ, "Ausbreitung 2m - Eine Einführung" (31/05/2024): nästan optisk upp till 200 km, tropo per kanal eller refraction bortom, med högt tryck och på våren och hösten, citerar VHF-sidan av DM2HB och noterna på DK5YA på tropo och Es. A. Johnson VK3DXE på bloggen av EI7GL (december 2021): med WSPR VHF-vägar av tusentals sommar i Australien har upptäckts. Diskussion om VHF marina och AIS ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)): omfattning 1,23h (ft) nautiska miles per antenn, sprid upp till cirka 50 nm, typisk trope förbättring 100-400 nm; ledningarna är erkända eftersom avlägsna stationer kommer lika starka som grannarna. Chelmsford ARS, december 1983: i 2 m-tävlingen är stationerna i östra kusten av England gynnas av havsvägen till kontinenten.

ARRL, * Radioamatörens VHF Manual*, 1972, cap. 2 "Propagation Beyond the Horizon" (s. 17-19). Redan kända operationstips (följ högtrycksområden som rör sig långsamt och framför allt sina bakkanter; tidig höst den bästa perioden; kustområden, stora sjöar och dalar av floderna som gynnas av stratifiering). Två nya data: (1) fikon. 2-4 jämför standardatmosfären ( relativ fuktighet 70% upp till 12000 ft, minsta refraktion) med en typisk radie av stark refraktion, med en tydlig temperaturavbrott och blandningsförhållande på cirka 4000 fot (ca 1200 m): det är den signatur som vår beräkning måste känna igen på GFS-nivåerna; (2) sanna kanaler är sällsynta under 144 MHz och vanligast i UHF, enligt de minsta tjocklekar. Användbar QST-bibliografi för att söka: Hull, "Air-Mass Conditions and the Bending of UHF Waves" (juni 1935, maj 1937); Hoisington, "Painless Prediction of Two-Meter Openings" (oktober 1949); Collier, "Upper-Air Conditions for 2-Meter DX" (september 1955); Botts, "En natt att komma ihåg" (januari 1970, utförd på land).

PA0EHG, sida "Tropo" ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): sammanfattning av troposcatter från G3YGF -artikeln, redan i handboken. Använd två praktiska regler för att styra vår scatter beräkning: ca 10 dB extra förlust för varje vinkel vid horisonten av varje station, och över 100 km cirka 9 dB plus varje 100 km rutt (60 miles); den gemensamma volymen börjar från ca 600 m på 100 km och från ca 9 km på 480 km. Det behandlar inte kanaler, omröstningar eller brytbara trösklar.

PA0EHG, PROPBER kalkylator ([sida](https://pa0ehg.com/propagat1.htm), propber.exe program som tillhandahålls av författaren av programmet). Det är ett 36 kB DOS-program skrivet i BASIC och sammanställt med Microsoft BASIC Compiler 5.60 (1985): det går bara under DOS eller DOSBox. Från de interna texterna: ber om frekvens, avstånd (eller de två lokalisterna, även i det gamla QRA-formatet "E-locator"), för varje antennhöjdstation, bullerfigur, FI-band, kraft, ett hinder (höjd och avstånd) och antennvinst (0 = liknelsediameterberäkning, effektivitet 50%). Returnerar det maximala avståndet i optik, den korrekta optiska förlusten för absorptionen av ånga (0,1 dB/km från 24 GHz), förlusten av troposcatter "korrekt för platserna" (dvs. för de horisontvinklar som ges av hinder) och S/N-förhållandet i de två sinnena, även "med den maximala onormala förökningen". Numeriska konstanter är inte läsbara i den sammanställda verkställbara. Den föreslagna metoden för höga band är också användbar för oss: kalibrera beräkningen på en uppmätt länk (skillnad mellan uppmätt och beräknat S/N) och tillämpa samma skillnad på ett annat band. Det är samma beräkningsmönster, men det använder S RTM1 -profilen istället för bara ett hinder och GFS-prognosen istället för en fast marginal.

PROPBER, konstanter läser i körbara (antal i Microsoft Binary Format-format). πre, i datatabellen bredvid texterna: 8.68589 (= 20/ln 10, dvs 20 loggar beräknade med naturlig logaritm), 32.5 (fritt utrymme: 32.5 + 20 log f\[MHz\] + 20 log d\[km\]), 24000 och 0.1 plus 40000 och 0.3 (ånga: 0.1 dB/km från 24 GHz, 0.3 dB/km från 40 GHz) Sannolikt, eftersom du upprepar men inuti koden: −69,5 (samma konstant term som Yehs sida-scatter formel i VHF / UHF DX Book), 332 (mark refractive), 8733 km (motsvarande radie, k ≈ 1.37). Den exakta scatterformeln är inte rekonstruerad utan att demontera programmet. T0

PROPBER, formler rekonstruerade av koden (demontering av den sammanställda delen; operationerna kallas till BASIC biblioteket med operatörerna i minnet, vars värden i dubbel precision har avkodats). Konstanterna −69,5, 332 och 8733 hittades tidigare var falska positiva inom biblioteket.

- Optiskt avstånd: d = 5 · (√h1 + √h2) km (h i m). Den är mer generös än den klassiska 4,12 (k = 4/3): motsvarar k ≈ 2.
- Förlust i optik: L = 32.5 + 20 log f\[MHz\] + 20 log d\[km\], plus 0,1 dB/km över 24 GHz och 0,3 dB/km över 40 GHz.
- Troposcatter: Lts = L + 23 + 10 log f + 0.0674·d, dvs 55.5 + 30 log f + 20 log d + 0.0674·d (formel av G3YGF med konstant 2 dB högre). Ingen vinkel term eller reaktivitet.
- Korrigering "för webbplatser": programmet frågar hinder och avstånd men blocket som bör använda dem är tomt; korrigeringsvariabeln förblir 0, så den "korrekta" förlusten är lika med scatter.
- Buller: T = 290·(10^(NF/10) − 1), N = −228,6 + 10 log T + 10 log B (dBW), utan antenntemperatur.
- S/N = 10 loggar P + G\_tx − L + G\_rx − N. "Med maximal onormal förökning" använder optisk förlust istället för scatter, dvs. det perfekta ledningsfallet utan förluster bortom ledigt utrymme.

Jämförelse: 300 km och 144 MHz PROPBER ger cirka 189,9 dB, G3YGF 187,9, vår P.452 187,8 (med noll horisont vinklar): scatter sammanfaller inom 2 dB. Vår beräkning lägger till horisontvinklar från profilen, antenntemperaturen och i stället för "maximum" fixade det förväntade lagret med kvarstående parning och dämpning.

Rohde & Schwarz, P. Denisowski, "Understanding VHF Propagation", Educational Note, April 2023 (24 sidor, spridning). Bekräftar de punkter som redan samlats in (genomförs som en tvådimensionell vågguide, signal som sjunker på ett linjärt sätt med avståndet, upp till 1500 km och bortom, varaktighet från timmar till dagar; bra mark på havet och knappt på land; upphöjd från några hundra meter till några kilometer). Två användbara observationer för indexet: (1) evolution i tid: en nybildad kanal bär ofta endast de högsta frekvenserna och förtjockning börjar ta med ännu den lägsta; den timprognosen bör därför visa 432 MHz "i förväg" på 144 MHz när lagret växer; (2) stationer plats: de bästa förhållandena är mellan stationer nära ändarna av ledningen, medan de längs vägen av en hög ledningen ofta misslyckas med att komma in, om inte genomförs "vem förlorar". Kanalerna rör sig med atmosfäriskt väder.

Site PA0EHG, andra sidor. För trope, PathProfile av M. Willis G0MJW ([scheda](https://pa0ehg.com/srtmp.htm), [ladda ner](http://www.mike-willis.com/Software/ASTERPathprofile.zip)): profil av rutten från förväntad total förlust och kvotfiler, tillförlitligt bedöms för troposcatter i mikrovågsugn; behandlar inte scatter från plan eller regn. Användbart som en andra referens för att jämföra våra S RTM1 -profiler och förluster. Resten av webbplatsen täcker EME, mikrovågsugn, luftstrip (AirScout, SM7LCB kartor) och åtgärder på 80 m.

PathProfile by G0MJW (Mike Willis), ASTER och SRTM paket och SiteData (tillhandahålls av författaren av programmet; [](https://www.mike-willis.com/software.html)programsida). Windows-program av 2018: profil från ASTER eller SRTM till 1 "eller 3", förluster i optik, diffraktion, troposcatter (giltigt upp till 1000 km), kanal och gasformig, med andel av tiden, median av rutten, bråkdelen av vägen på havet och avståndet från kusten: strukturen är P.452 / P.1812, men hjälpen (komprimerad.chm-fil) kunde inte läsas här. SiteData konverterar locatorerna och ger andelen från GT OPO30.

Den mest användbara delen är datafilerna som ingår: ITU-R P.453 statistiska kartor på globalt rutnät på 1,5 ° (121 rader från 90 ° N till 90 ° S, 241 kolumner från 0 ° till öst): gradient dN / dz i de första 65 m överskrids för 1, 10, 50, 90 och 99% av tiden; för markkanaler procent av närvaro, tjocklek och intensitet; för den höga närvaron, basandel, tjocklek, intensitet och maximal höjd; plus våtsikt och temperatur. Interpolerade årliga värden:

| Plats | Landpipeline: % / tjocklek / intensitet | Hög rörledning: % / bas / tjocklek / intensitet | dN/dz översteg 10% av tiden |
| Neapel |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | −380 N/km |
| Tyrrhenian (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | −674 N/km |
| Trapani |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | −520 N/km |
| Brindisi |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | 405 N/km |
| Adriatiska (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | −399 N/km |
| Milano |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | −172 N/km |
| Udine |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | −176 N/km |

För vårt index är en klar klimatreferens: tjocklek och intensitet som är typisk för kanalerna istället för 50 m fastställd av den potentiella kanalen och en anomali "idag jämfört med normal" per zon. ITU-data: gratis för icke-kommersiell användning, för att kontrollera innan de ingår i en offentlig version. ZO NES01.DEM-filen (58 MB, 2 rutnät) innehåller klimatzonerna inåt landet / kust / hav; formatet är inte dokumenterat och jag avkodade inte det. The Willis ekvationssida innehåller bara fysiska konstanter, ledigt utrymme och antennvinst.

Fjärde omgången av källor (20 november).

Indermuehle och Suzuki 2026, förutsägelse av GFS rörledningar för CSIRO observatoriet av Murchison (arXiv 2608.12861): det är den närmaste källan till LIVETROPOVHF. Profiler av N från GFS till 0,25 ° (yta till 2 m, sedan isobariska nivåer, de under kasserad mark); för varje minsta kolumn av dM / dz under 3 km, bas och toppen av ledningen, integrerade M underskott; sedan P.452-16 och P.1812-6 med GLO-90 25 längder från 98 till 1005 km, från 88 MHz till 2,68 GHz, varje timme. Resultat: Median AUC 0,72 på 25 kanaler; P.452 p tidsprocenten ska användas som kalibreringskonstant (val p = 20%), eftersom ITU-modellen med ett ögonblick ΔN inte definieras i en ledtråd; färdighet förblir nästan platt till 3 dagar i förväg. Vid 162 MHz (AIS) har de mottagna rutterna den högsta ledningsfraktionen på 67% (0,26 mot 0,16) och det högsta kanaldjupet på 49% (28 vs 19 M) med avseende på kontroller: på havet är ledningen nästan alltid, bestämmer landsträckan. GFS ser hälften av tiden på marken jämfört med ERA5 (4,5% jämfört med 10,4% av cellerna timmar), medan på havet ser mer (73% jämfört med 56%). Ett "korridor" index (kontinuerliga rörledningar längs rutten, lager med en minsta tjocklek på 40 m) förbättrar resultatet: bekräftar att kontinuitet längs rutten är rätt kriterium.

Longley och Rice, ESSA ERL 79-ITS 67 (1968): metod för oregelbunden terräng från 20 MHz till 20 GHz (efter ITM), med mark oegentligheter Δh, Ns från 250 till 400, motsvarande radie 8497 km (4/3), horisonter, diffraktion, scatter framåt och variabilitet i tid och på platser för klimat. Statistisk referens, inte omedelbar.

ITU-R P.834-2: radiekurvatur, Jordradieekvivalent k·a, M = N + h / a (h i m, i tusentals km), exponentiell modell; inget nytt än vad som redan användes.

AGARD CP-70 (1970), troposferisk förökning: tunna lager från 1 till 100 m med starka gradienter är vanliga; reflektionskoefficienten hos ett högt lager är ungefär proportionell mot våglängden, med en slumpmässig spridning av cirka 14 dB. Åtgärder vid 90 MHz: minskning av 9-10 dB bunden till ett N-hopp koncentrerat på cirka 350 m, högst upp i ett lager av moln. Passagen av en kall front eliminerar lagren: Diffraktionen kvarstår.

U.S. URSI Report, Commission 2 (1960): genomsnittlig tjocklek av de höga lagren från 60 till 210 m (med tanke på P.453), minsta gradient för 157-160 N/km.

Buscemi K2OVS, VHF propagation guide: typiska avstånd för reversering och kanal för band, kanaler kopplade till hög stationär tryck, maximalt i gryningen.

Wolinsky-Mancini och 2024, AIS på Kanarieöarna: AIS radiosundationer och mottagningar; kanalerna är mycket vanligare under de varma månaderna (omvandling av aliseanerna).

Nigerianska och malaysiska artiklar (Ekah, Iloke, Mat, Igwe): lokala korrelationer mellan temperatur, fuktighet och uppmätt förlust, svag och motsägelsefull (r −0,09 till 0,27); användbar endast som bekräftelse på att markstorlekarna ensam är inte tillräckligt.

Herben 1984, avhandling TU Eindhoven (microonde): 30 GHz på 8,2 km och OTS satellit på 11,6 GHz; gnista för turbulens, multi-path och regn. För mikrovågor domineras dynamiska förluster av regn och turbulens, inte av kanaler.

Baars, The Paraboloidal Reflector Antenna (microonde): vinsten av en liknelse G = ≤(πD/λ) 2 med typisk effektivitet 0,5-0,65; förlust av ytfel enligt Ruze, ≤s = exp(-(-(4\/λ)2). Det skulle hjälpa om vi lägger till liknelserna som ett alternativ till Yagi DL6WU för 1296 MHz och bortom.

Redan analyserat eller av dåligt innehåll: Hirschmann NASA X-731-67-89 (dubbel), Reids VHF ST radar (vindar och turbulens, oskadda), Skriftlig bild och generiska anteckningar på troposfären.

Femte omgången av källor (20 november, kväll).

Rogers 1997, Radio Science 32(1): Bayesiansk uppskattning av parametrarna för en hög kanal (basfrekvens, M-underskott, tjocklek) från förökningsfaktorer som mäts utöver horisonten, med en parabolisk ekvationsmodell; VOCAR-experiment i Kalifornien vid 143, 263 och 375 MHz, radiovågsjämförelse. Signalen beror mycket på basen och M-underskottet (korrigera 0,81 till 143 MHz för underskottet), nästan alls från tjockleken (−0,17): tjockleken kan inte erhållas från signalen. För oss: beräkning B (klimattjocklek) räknas mindre än underskott och andel; och metoden är grunden för bakverket, det vill säga att få ledningen från de flera frekvenserna.

Moler och Holden 1960, "Tropospheric scatter propagation and atmospheric circulations", J. Res. NBS 64D, pp. 81 och följande (läs i original den 22/11/2026, doi 10.6028/jres.064d.014). Kalifornien sjövägar till Point Loma (San Diego): Santa Barbara 190 miles, Point Mugu 144, San Clemente 78; 1365 MHz och 9380 MHz, inte VHF; januari-april 1958, genomsnitt 1 minut. Fallet av den 26 mars: en förstärkning av cirka 20 dB i cirka 3 timmar mot middag, när en cyklon vortex försvagar och börjar havsbris och subvention; vid 15 konvergensen reformeras och signalen sjunker cirka 22 dB från maximum. Statistik (tabell 1, 500 hPa, 33 fall): med luft som släpper den genomsnittliga signalen är högre än cirka 2,5 dB än med luftökning, i bandet L och X. Signalen följer skillnaden beräknad till 500 hPa i tecken och amplitud av variationerna, utan en korrelationskoefficient. Låga lager (under cirka 600 m) gynnar partiell reflektion på spridning. Anticykloner och fronter behandlas inte, och de "utflykter upp till 60 dB" som nämns ovan är inte: de kom från en begagnad sammanfattning. För oss: den vertikala rörelsen är en bra prediktor, och +1,5 dB för subventionen och −2 dB för uppförsbacke luften är av samma ordning som den genomsnittliga mätta skillnaden (2.5 dB); 20 dB är ett enda fall, inte ett typiskt värde, och på mikrovågsugn.

Banafaa och Muqaibel 2025, IEEE Access: undersökning av rörledningarna (avdunstningsmodeller, parabolisk ekvation, radaruppskattningar, GNSS och lidar) och klassificering med automatisk inlärning om verkliga åtgärder; Support vektormaskiner ger de bästa resultaten. Användbar som bibliografi; data är persiska viken.

ITU-R P.834-7 (2015): uppdaterad version av rekommendationen om troposferal refraction; ingen ny formel än de som redan används.

Abbott, NBS bibliografi på troposferous scatter (GOVPUB- C13): redan i tredje omgången. Slide Scribd på kanalerna: generiskt innehåll. Artikeln ScienceDirect 0021916987900304 kunde inte öppnas.

Sjätte omgången (20 november, natt): Bean och Dutton, ARRL Handbook 2022 och 2023, Balanis.

Bean och Dutton, Radio Meteorology (cap. 6), delar som ännu inte används. Fältkänslighet för Ns: 0,18 dB per enhet N på den årliga cykeln; 0,14-0,24 dB / N i dagtidscykeln (maximal tidigt på morgonen), upp till 0,2-1,1 dB / N på vägarna på 175-200 km på vintern; korrigeringar 0,8-0,95. Parameter bestående av Misme: M = a (g\_e − 40 + b≤W^n) med en = 0,5 dB per N/km, som kombinerar motsvarande gradient och termisk stabilitet ΔW (arbete för att lyfta luften på 1 km): i Sahara på 471 MHz på 160 km är den enda stabiliteten 20 dB mellan januari och juni vid nästan lika gradient. potentiell refraction index K (N rapporterade adiabatically till 1000 hPa): belyser luftmassorna och anticykloniska subventionen (Flavell och Lane).

ARRL Handbook 2022 (99a ed.), 19.4.3–19.4. Användbara inversioner mellan 250 och 2000 m; inversioner av strålning (sera, lugn luft, torr jord) sällan leder sant; långsamma anticykloner ger de mest omfattande kanalerna för subvention, med inversioner mellan 500 och 3000 m, starkare på kvällen och på morgonen; på norra halvklotet är de bästa rutterna söder om det höga centrumet. Varm front: bra förhållanden några hundra kilometer framåt; kall front: stark men instabil vänd bakom fronten, parallellt. Foehn (även i Alperna) och jordbrisen (kylluft på marken, varm vid 200-300 m; sträcker sig över 800 km längs kusten). Scirocco: varm och torr luft från Sahara över kallare marin luft, mycket starka inversioner från östra Medelhavet till Gibraltar, trolig orsak till rekord över 1500 km i mikrovågsugn. Troposcatter: teoretisk gräns ca 800 km, maximal typisk halva. Den 100: e ed. (2023) har en kortare text: inversioner vid soluppgång och solnedgång upp till +20 dB och +20-50% flödeshastighet; 1200 miles på Great Lakes, typiska 400-600; rekommenderar Radio Mobile (ITM modell) för Hepburn täckning och kartor.

Balanis, Antenna Theory (4a ed.), § 10.3.3: Viezbicke-bordet (NBS TN 688, 1976) för Yagi optimerade vid 400 MHz: boom 0.4 / 0.8 / 1.2 / 2.2 / 3.2 / 4.2 λ → 7,1 / 9,2 / 10,2 / 12,25 / 13,4 / 14,2 dBd. Vår DL6WU -formel (8.6 + 8 log10 boom / λ) ger 1-1,7 dB mindre mellan 0,8 och 4.4 Med rätt formel på originalet (9,4 + 7,8 log10 boom/λ, från 22 november) faller skivan under 0,2 dB mellan 1,2 och 4,2 λ (12.1 dBd till 2,2 λ mot 12,25) och förblir 0,6 dB till 0,8 λ. DL6WU

Sjunde rundan (21 november): QST, QEX, W3SZ, GitHub repository.

QST 2000-2025 (14 nummer, 10 bäddar med textigenkänning). Endast rapporter i avsnittet Världen ovanför 50 MHz, användbart som fall för att validera modellen: September 5-9, 2000, genomförd på 144 MHz och bortom den östra halvan av USA; 14-15 augusti 2022, öppna Manitoba-Kansas/Missouri under högt tryck på en varm dag, från 144 till 1296 MHz nästan allt i FT8, kanadensiska rekord på 10 GHz av 1228,56 km ( VE4MA - N0OY) i Q65, signalerar upp till 1941 km; APRS propagation kartan visade samma område. QST November 2022 rekommenderar Hepburn kartor. Ingen teknisk artikel om tropoprognos.

QEX och K1JT, Franke och Taylor (QEX 2020) på FT4/FT8 -lägen: FT8 -avkodande trösklar −20,8 dB i 2500 Hz på kanal utan utveckling, −18,6 dB med evanescence på medellånga breddgrader; FT4 −17,5 dB. Det andra antalet QEX 2023 och 2026 behandlar inte tropo (tre siffror 2023 är olästa skanningar). W3SZ 2019: slow mode trösklar ( JT4A −23, JT9A −27, JT65A −25, QRA64A −26, FT8 −21 dB) och valkriterier mellan känslighet och frekvensskift tolerans.

GitHub. TropoLink (LukiBox, Apache-2.0): design av troposcatter-rutter med P.617-5 (med N0 och ΔN), NBS TN101 och ITM flanked, och deras skillnad som visas som osäkerhet; P.617-5: L = F + 22 log f + 35 log θ + 17 log d + Lc, F = 0.18· N0 ·e ^ (−hs/7,35) − 0.23G, Lc = 0.07e ^ 0. andi-f/troposcatter ( DL2FZN 2013, från VHF/UHF Handbook RSGB): Lts = 24,44 + 4,283 ln f + 0,06656 d ovanför ledigt utrymme, sluttning nära PROPBER (0,0674 dB/km). QRadioPredict: ITM och ITWOM 3.0 med S RTM3, markanvändning Corins och NEC diagram. Inte relevant: Tropos (väderapp), opera-adt/tropo och troposim (Troposferisk försening för InSAR), tgbot\_troposcatter (militär station), sdr-troposcatter-gui (kontrollpanel), Troposcatter-Propagation-Delay ( ERA5 försening), Radio-Communication-Project (didactic simulator), PyLayers (kanal).

Åttonde varv (21 november): NTIA- och ITU-modeller på GitHub. NTIA/itm (Longley-Rice, ITS): 20 MHz-20 GHz, fri rymdförlust, diffraktion och troposcatter med markprofil, 0,5-3000 m terminaler, ytreaktivitet N0 (minimitet reducerad till havsnivå, 250-400), sju klimat, jordkonstanter och olika lägen 1 vy, 2 diffraktion, 3 troposcatter; C + + + med.NET Frame 4.7.2 inhägnad. Den modellerar inte kanalerna: den fungerar som en referens "normal atmosfär" med statistisk variation. NTIA/p528 och eeveetza/p528 (MATLAB): ITU-R P.528-5 för luft-jordförbindelser, 100 MHz-30 GHz, 1,5–20 000 m terminaler, p 1–99%;.NET 4.8 hölje. Möjlig användning: luftspridningsbalans (två markvägar - flygplan plus radarsektion). eeveetza/p2001 (MATLAB): ITU-R P.2001-6, all-field modell med diffraktion, troposcatter, onormal förökning (ledning och lager, Lba) och E sporadisk, andel av tiden 0-100%; ITU digitala kartor (DN\_Median, DN\_SupSlope, DN\_SubSlope, dndz\_01, TropoClim, h0, surfwv, Es) är inte omfördelnings. Möjlig användning som en ITU-jämförelse i stället för P.452, med GFS ΔN i stället för kartor som i CSIRO-studien; kartorna ska laddas ner av användaren enligt P.453. Validering 1.3.0 av 593 QSO (29/12/2021–2/1/2022): händelse/jämförelse separation 0,79 utan korrigeringar, 0,91 med Misme och subvention; GFS ser inte ledningen på havet vid 144 MHz (guadagno utan korrigeringar aldrig mer än 15 dB).

Ninth runda (november 21,): IU3QEZ, NTIA, PyWaveProp, ACE High, validering 1.3.1. IU3QEZ (S. Fabris), presentation "Över horisonten i VHF" (GEKO Magazine n. 69, September 2026): gemensam volym d2 / (8 · R \_eff) med R ≈ 8500 km (1,3 km d 300 km, 3,7 km till 500 varje nivå av horisont värd 69 miles (111 km) 10 dB är ca 100 km; station IQ3QC 5 W med 3×5 element, 120 QSO, maximalt 788 km (Alpe Adria 2026). Bibliografi av presentationen som ska återhämtas: Norton 1959 (J. Res. NBS 63D, System Loss), Bray 1961, ARRL Antenna Book 2023 fig. 4.29, Rice-Longley-Norton-Barsis NBS TN 101, Bean och Thayer 1959, Barsis och Kirby 1961 (diffraktion på ås), Deygout 1966, P.1812, AirScout DL2ALF, P.834 NTIA PropLib (wiki): eHata, P. PyWaveProp (Python): Paraboliska ekvationsmetoder med Padé approximations, multipel edge diffraction, PETOOL och RAM-hämtningar, troposferous och höga kanaler, oregelbunden jord: möjlig bas för en fältberäkning med GFS M(z)-profilen, som ska kontrolleras. APRS PropView (Apache 2.0): VHF-förökningskartor från APRS-paket, timstatistik och öppningslarm; möjlig källa till 144 MHz-data för validering. NATO troposcatter netto i Italien (fortifications.net): 900 MHz, 10 kW, 20 m liknelser; stationer inklusive Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Vulture, Monte Lauro; historiska testfall för P.617. Inte relevant: Gupta 2021 (stratosfärisk transport), lag 2023 (Arktisk ozon), delfin # 590 och opera-adt/tropo (InSAR fördröjning), tropo.ino (Arduino gas sensorer), tropo-webapi (telefoni), molntooler/troposfär (AWS). Validering 1.3.1 på 564 QSO tropo: AUC 0,79 (model 1.2), 0,80 (+ mjuk kanal), 0,90 (+ misme och subsidence), 0,90 (+ persistens); ledningsfärger (≥ 16 dB) på 46% av öppningsrutterna och 0% av jämförelsen.

Läggs till i samma runda. mikelytaev/wave-propagation (MIT, Python 3): Parabolisk ekvation med approximates av Padé och Numerov, oregelbunden jord, troposfäriska kanaler; ladda den modifierade refractive M(kvot, lat, lon) från NOAA GFS och ställa in en evaporation conduit av Monin-Obukhov; ingen dokumenterad funktion som returnerar förlusten på vägen. Han är den bästa kandidaten för en framtida fältberäkning med GFS M-profilen (det skulle tjäna en extern tjänst eller en översättning). RP-Sim (AGPL-3.0, JavaScript): Empiriska modeller för radionät (Hata, He, Ericsson, två strålar), ingen tropo. RIMFading (GPL-3.0, OMNeT++): flödesoberoende för nätverkssimuleringar, Weibull-distribution, ingen tropo. PyLayers: inomhusradiokanal, redan utesluten. qradiopredict (kantooon, GPL v2): ITM och ITWOM v3.0 med S RTM3 och Corine, redan i den sjunde omgången som QRadioPredict.

Tenth round (21 november): Antenna Book, Norton, Barsis, nya datakällor. ARRL Antenna Book (23a ed.), cap. 4 ( K9LA): Bray ca 214 dB), kurvor på 50% och 99% (fig. 4.11): 195 dB värt 100 miles till 99% och över 250 miles till 50%; evanescence ± 7 dB över 100 miles (−3,5 dB till 50 miles), SSB 3 dB sämre än CW, +4 dB markreflektion, +3 dB för koppling av två antenner; höjdökning ± 7 dB Soluppgång och solnedgång upp till +20 dB och +20-50% flöde; varm och torr luft över våt frisk luft på de stora sjöarna upp till 1200 miles, typiska 400-600; regioner påverkas av stora vattenmassor de mest gynnade; sena vinter med uppvärmning som kan starka inversioner. Verifiering av vår troposcatter: P.452 till 50% på 483 km och 144 MHz ger cirka 204 dB, i överensstämmelse med 214 dB Bray till 99%. Norton 1959 (J. Res. NBS 63D, fri på NIST): grundläggande förlust, system och banantenn vinst; Referens atmosfär Ns = 301; motsvarande radie från Ns (250 → 4878 miles, 301 → 5280, 400 → 6997); L(99) tabeller med 30-fots antenner (100 MHz: 163 dB till 100 miles, 208 dB till 500 miles; 1000 MHz: 138 och 188 dB med 28-fots liknelser); långsiktig variabilitet V(p, θ) beroende på vinkelavstånd (fig. 13). Rice, Longley och Norton, NBS TN 15 (1959), gratis på NIST. Barsis och Kirby, NBS Report 6751 (1961): 223 km i Colorado med Pikes Peak (4292 m) som kant, 100 och 751 MHz; konsekvent förlust med den rundade kanten (ungefär 11,5 km radie); långvarig evanescence 11,7-14,3 dB jämfört med 39-42 dB av en scatter av samma längd; ingen dag cykel markerad; fyrray modellen gav 4-5 lober men bara en mäts (maximum till 21 m berget) ARRL VHF Digital Handbook: Pack, APRS, D-STAR, WSJT (Scatter Method and EME), ingen del på tropo. Nya beprövade eller sökta datakällor: ECMWF öppna data (IFS 0,25 °, nivåerna 1000, 925, 850, 700 hPa..., utan 975, 950, 900 och 800; G RIB2 med CCSDS komprimering, att vår läsare inte avkodar; redan tillgänglig via Open-Meteo); DWD ICON-EU öppna data (G RIB2 komprimerad bz2, en fil för variabel och nivå: många förfrågningar); Kanada GDPS på MSC Datamart och GeoMetabRopeoMeteoMet (15 km)

Elfte rundan (21 november): Longley-Rice, GEKO Bean och Thayer, "Models of the Atmospheric Radio Refractive Index", Proc. IRE May 1959 (s. 740 et seq.): modell 4/3 har rätt sluttning i första km men faller för högre; 30% av krökningen av en radie förekommer i de första 100 m och 60% i första km; genomsnittliga förhållandet mellan Ns och minskning i första km: ḲN = 7,32 ^ (0,005577 · Ns = 313 → ΔN ca −42). Bean och Thayer, "C.R.P.L. Exponential Reference Atmosphere", J. Res. NBS 63D n. 3 (1959): N(h) = Ns·e ^ (−c·h) med c fixed by Ns genom samma rapport; Average Ns USA 313; strålspårningsbord (totalkurva 15.2 mrad med exponentiell modell jämfört med 30.3 mrad med Earth 4/3). MATLAB refractionexp: c = 0,143859 km−1 för Ns = 313 (skala ca 6,95 km), giltigt över ca 1 km och 10 mrad. Använd för oss: den "normala" jämförelseprofilen kan vara att CRPL med de lokala Ns istället för M = 315 + 0.118 ·z fixerad, och anomalien av minskningen i första km bör mätas jämfört med −7,32 ^ (0,005577 · Ns) i stället för −40 fast. NTIA Report 82-100 (Hufford, Longley, Kissick 1982), ITM guide i områdesläge: climi och Ns föreslog (ekvatorial 360, subtropisk kontinental 320, subtropisk marin 370, öken 280, tempererad kontinental 301, tempererad marin på land 320, tempererad marin på havet 350); Ns = N0 ·e ^ (−hs/9,46 km); du använder månatliga minimum av Ns eftersom mindre påverkas av super-fraction Rice, Longley och Norton, NBS TN 15 (1959), del I: prognoser formel för distribution över tiden av land och troposfärisk våg, bas av TN 101. SoftWright, Wikipedia, Altair WinProp och MATLAB rfprop. LongleyRice: samma information (version 1.2.2, 20 MHz-20 GHz eller 40 GHz, 1-2000 km, Ns 301, sju klimat, variabilitet av tid, plats och situation); ingen formar ledningarna, bara den statistiska effekten av de marina klimaten ("högre fält för 10% av tiden eller inte"). GEKO Magazine n. 69 ( IU3QEZ): radiohorisont 4/3 av 130 km från 1000 m, 260 km lägga till två stationer; troposcatter med gemensam volym mellan 1-3 km och 10 km; Scatter från flygplan på 10 km med + 10-20 dB i tiotals sekunder eller ett par minuter; teoretiska kurser med 2×10 element per sida: SSB QRP ca 440 km, SSB 50 W ca 510 km, CW QRP ca 600 km, SSB 500 W

## 12. Runda tabell: källor för att validera modellen eller korrigera den (21 november)

Sammanfattning av alla källor, beställd för användning. "State" säger om källan redan finns i programmet (1.3.1), oavsett om det är i test eller om det ska göras.

### Data för att validera (vad som verkligen hände)

| Källa | Vad ger | Användning | Staten |
| MMMonVHF rapporterar från författaren | 593 QSO vid 144 MHz, 29/12/2021–2/1/2022, tid och lokalisering | Event validering mot jämförelse (+14 dagar) | I bruk: AUC 0,79 (1,2) → 0,90 (1,3.1) |
| Världen över 50 MHz | dokumenterade fall (14–15/8/2022 Manitoba–Kansas, 144–10 GHz) | Historiskt fall | i bruk som ett historiskt fall; att lägga till validering QSO |
| wspr.live (WSPR) | komplett arkiv, 2 m och 70 cm, SQL | Många händelser och många "nej" (timmar utan plats) | Försök, det bör filtreras (punkten falsk vid 16–20 tusen km) |
| APRS PropView / APRS-IS | stationer kände, avstånd, timmar | endast validering 144 MHz | att göra |
| Omvänd Beacon (arkiv) | Daglig plats | VHF | Kasserad för nu |
| I GRA2 radiosondaggi (Brindisi 16320, Practice of Mare 16245, Trapani 16429, Udine, Milano) | profiler mätt 00 och 12 UTC | GFS profilkontroll och inversioner | Hitta filer (Brindisi 46 MB), som ska användas |
| Barsis och Kirby 1961, Barsis 1963 | evanescens mätt på vägar med bergigt hinder | validera kategorin "monte" (Vesuvio, Faito) | att göra |
| Norton 1959, NBS TN 15, TN 101 | L(99) och bordsmedieförluster | Scatter kontroll | Delvis (se 300 miles konsekvent) |

### Korrigeringar som föreslagits av källor

| Korrigering | Källor | Effekt som mäts vid validering | Staten |
| Misme Stability Termen | Bean och Dutton cap. 6 6 6 6 6 6 6 | AUC 0,79 → 0,90 tillsammans med stöd | 1.3.1 (Titel 16 dB utan ledning) |
| Subvention från vertikal rörelse | Moler och Holden 1960 (läs i original den 22/11: +2.5 dB-genomsnittet mellan luft som kommer ner och luftstiger, 1,4 och 9,4 GHz på havet) | som ovan | i 1.3.1 (+1.5/−2 dB) |
| Mjuk pipeline upp till 3 ·λ | Kerr, Hepburn, VHF/UHF DX Book, Shi 2019 | ensam +1% av AUC; ger färgerna på 46% av händelsens rutter och 0% av jämförelsen | Från: |
| Persistens 24 hh h h h h h h h h h h h h h h h h h h h h h h h h h h h h h h h h h | VHF/UHF DX Book, Biancomano och Leyshon | AUC oförändrade, mer tagna och mer falska larm | i 1.3.1 |
| Normal profil CRPL med lokala Ns (N = 7,32 ^ (0,005577 · Ns)) i stället för 40 N / km fast | Bean och Thayer 1959, NBS Monograph 4 | Testad i validering | 1.3.2 som ett alternativ, avstängd av defekt |
| Super-refraction tröskel 79 N/km (ITU) i stället för 100 (Bean och Dutton, lager 1000-850) | P.453, Lopez 2008, DX Book | Testad i validering | i 1.3.2: aggressiv aktning tillsammans med försiktighet (kartan använder försiktighet) |
| Klimatologisk tjocklek (kalkel B) mindre viktigt än underskottet | Rogers 1997 | Försök: Använd A för vinst | att göra |
| Parabolisk ekvation i GFS profil | PETOOL, vågpropagation (Lytaev), Levy (bok som inte läser, bara förhandsgranskning), Lindquist | första versionen med fält över horisonten domineras av numeriskt buller (AUC 0,12-0,28): korrekt (domän 8 km, strål 6°); efter korrigering AUC 0,54-0,66 | i bruk som andra beräkningar bestämmer inte prognosen |
| Evanescence: ± 7 dB över 100 miles, marginal 10–20 dB till 90–99% | Bray 1961, Antenna Book, GEKO 69 | tröskelvärden i marginella förhållanden | användning: sannolikhet för period och tröskelvärde FT8 −18,6 dB i troposcatter |
| Gemensam volym och motsvarande avstånd av Bray | IU3QEZ, Bray 1961 | Information om information | Från: |
| Troposcatter P.617 och RSGB som osäkerhet | TropoLink, DL2FZN | Information om information | i 1.3.0 |

### Vad som återstår av modeller

På havet i låg höjd (100-200 m) kan GFS-nivåerna inte se (studie av aktuella datakällor); reflektion och diffraktion på de närliggande bergen (Vesuvio, Faito): förslag på beräkning "via monte". ITM/Longley-Rice är fortfarande en statistisk referens "normal dag" (Ns 301, climi), utan ledningar. Kasseras av författarens beslut: AirScout, P.528), E sporadisk (P.2001) och 50 MHz (servoir EIRP som få har och förväxlar med E sporadiska).

### Förslag till en arbetsorder

1. Utökad validering: lägg till filtrerad WSPR (även timmar utan plats) och QST 2022; upprepa för 432 MHz.
2. Prova den normala CRPL-profilen och ITU-tröskeln på 79 N/km, en i taget, vilket bara håller AUC.
3. Jämförelse av GFS-profilerna med radiovågen I GRA2 i Brindisi och Practice of Sea under QSO:s dagar.
4. Omvärdera den paraboliska ekvationen efter numerisk korrigering.

### Beslut av den 21 november (version 1.3.2)

- Antagen: försiktig uppskattning (Bean och Dutton, 100 N/km) och aggressiv (ITU, 79 N/km) flanked; kanaler sökte endast under 2500 m (först 1500 m, upphöjd efter validering); bonus med högt utsträckt tryck och svag vind; havsvikt med μ1 av P.452; andel av stationer med avseende på ledningen också på partiell ledt; gränsskikt över åsarna ( IT9GSF).
- Antas som ett alternativ, avstängd av fel: normal CRPL-profil med lokala Ns.
- Korrekt parabolisk ekvation numeriskt: AUC från 0,12 till 0,54 (30 m antenn) och från 0,28 till 0,63 (300 m).
- Land: Copernicus DEM GLO-30 i stället för Terrain Tiles "skadi", som nära kusten har spöklindringar på 80-100 m i havet (signation av IC8TEM, verifierad punkt till Capri); skadi förblir som en reserv.
- Vädret tecknar på kartan med sin effekt: ❤ gynnsamma subventioner (Moler och Holden 1960, i genomsnitt cirka 2,5 dB mellan luft som kommer ner och luft går upp; +1,5 dB), ^ luftuppförsbackning ogynnsamma (djupa fixturer; −2 dB), Strej gynnsamma ledtråd (ARRL Handbook § 19,4, starka inversioner på Medelhavet; ingen korrigering). Dubbel symbol, grön/röd/orange färger och serietidning på kartan.
- Antog ITM 1.2.2 (NTIA/itm, offentlig domän), översatt till C#: enligt beräkning av baslinjen i normal atmosfär bredvid P.452 och slutlig sträcka efter utgången från en kanal som slutar före korrespondenten. Kasserad qradiopredict-kod eftersom under GPL v2.

Utan användbart innehåll för indexet: *Communications Quarterly*, hösten 1991 (ingen tropoartikel; Atkins artikel om Scatter är i vintern 1991).

- Antagen som principen för manualer (21 november): "förökning kan inte generaliseras, men studeras i förhållande till lokal morfologi" ( DF5AI, rapporterad av IK3XTV i "förökning på 2 meter"); citerad i prognoshandboken (§ 1) och i rådet.
- Adopterad (21 november, efter 1.3.3) tolkningen av IK3XTV: s eftermiddagstransalpinöppningar: inte "konvektiva celler som linser", men troposcatter förstärktes av turbulens till toppen av lagret som antogs när den når andelen av den gemensamma volymen (manual av prognoser, § 8 ter). Från 1.3.4 går den in i punktberäkningen som en provisorisk förstärkning av troposcatter, högst 3 dB. Kasseras av samma artiklar: "auto-amplification" av signalerna bortom Alperna (ett passivt medium inte förstärker: de beskrivna länkarna förblir förklaras med diffraktion och troposcatter; de deklarerade krafterna används inte som data), snön som "bättre diffraktion" (mer troligtvis stabil lager över snön, med mindre evanescence).

- Kerr (1951) granskas i boken (22 november, PDF som tillhandahålls av författaren): 1,5, 17 och 18 och bord 1.1. Siffrorna på eq. 18 i skanningen är oläsliga, men tabell 1.1 bekräftar: 20 ft → 1 cm, 80 ft → 10 cm, 400 ft → 100 cm, 2000 ft → 1000 cm, mot 1,25, 10, 112 och 1252 cm av λmax = 0,014 · d ^ 1,5 (λ i cm, d stående; runda tabellen, "indikativa värden". Kerrs hypoteser: inuti kanalen de modifierade indexdropparna av 4 10−8 per fot, typiskt värde av de låga kanalerna; med svagare gradienter behöver en större tjocklek. Kerr varnar också för att λmax inte är ett nettosnitt: våglängder ibland längre kan fortfarande påverkas starkt av kanalen.

- DL6WU granskade på originalet (november 22, PDF av VHF Communications 3/1982 tillhandahålls av författaren): G. Hoch, "Extremely Long Yagi Antennas", s. 130 och senare. Fig. 1: vinna på dipolen mätt vid 432 och 1296 MHz i en professionell polygon av de tyska posterna, med ett provantenn EIA från 7,7 dBd, uppskattad noggrannhet 0,5 dB; 2,35 dB för dubbel längden av boomen, ingen mättnad upp till cirka 20 λ. Programmet använde 8.6 + 8.0.log10 (L/λ) dBd, d.v.s. ca 0,8 dB i mindre än 1-2 λ; nu använder den 9,4 + 7,8 · logg10 (L / λ) dBd (gradienten är den angivna, 1 λ-värdet läses på diagrammet, ± 0,3 dB). Kasserad formel 10 · logg 10 (7,8 · L / λ) cirkulerade på nätet: skulle ge 3 dB för fördubbling, mot 2,35 mätt.
- Booker and Gordon (1950): referens bekräftad av "This Week's Citation Classic" (Current Contents, 8 februari 1982, kommentar av H. G. Booker): Proc. IRE 38, pp. 401-412. Kommentaren bekräftar de två punkterna som används i handböckerna: fältet som mottagits bortom horisonten med Rayleigh-distribution och exponentiell autokorrelation funktion av oegentligheter (icke-gaussiska). Den ursprungliga artikeln har inte lästs.

## 13. Från IK3XTV artiklar

De två artiklarna av Flavio Egano IK3XTV (Radio Rivista: "Propagation på 2 meter" och "Propagation VHF - konvektiva celler och gravitationsvågor") lägger till två saker som ledarmodellerna inte täcker: transalpinöppningarna på eftermiddagen på grund av konvektion och praktiska regler om var och när ledningarna bildas i Italien. De är observationer från stationen, inte åtgärder: de måste behandlas som hypoteser för att kontrollera med fläckar.

| Observation av författaren | Hur det blir en ingrediens i indexet |
| Kanalerna bildas främst mellan 100 och 1000 m, sällan högre; serverar högt tryck på ett brett område, bra insolering och frånvaro av vind. | söka efter kanalen endast under 1500 m; bonus med högt utsträckt tryck och svag vind i de låga lagren |
| Under de varma månaderna som genomfördes återkommande på Adriatiska havet mellan Veneto och Puglia, 700-800 km, ibland upp till Grekland; sjövägarna är favoriter. | större vikt vid bråkdelen av sjövägen (t.ex. μ1 av ITU-R P.452) |
| Motsvarande måste vara i eller nära kanalen; med höga antenner och några meter leder signalen inte in | Jämför andelen av basen av ledningen med andelen av de två stationerna (kommer att tjäna höjden av QTH) |
| Samma ledning: lyssna från Gargano en station 144 och en av 50 MHz | för att kontrollera: Enligt Hepburn tabell 50 MHz be om över 300 m bly, så händelsen skulle indikera en pipeline mycket ofta |
| Anslutningar Veneto - Sydtyskland bortom Alperna, utan visuella, nästan alltid på eftermiddagen och med högt tryck; instabila signaler som försvinner och återkommer efter några minuter | "konvektivt" index separerat från ledningen, för bergslinjer |
| De konvektiva cellerna föds på marken och klättrar ca 1 km var tredje timme: vid 15 de når ca 3 km, den höjd som tjänar till att åsidosätta Alperna. | Använd gränsskiktets höjd (gränsskikt\_layer\_height of Open-Meteo GFS) längs vägen: poäng när överstiger höjden av åsarna |
| Alperna genererar gravitationsvågor som tillsammans med konvektiva rörelser skapar oegentligheter i det refraktiva indexet. | hypoteser: stark vind perpendikulär till kedjan till 700 hPa som signal av guldvågor; att försöka sist |
| Mycket hög inversion (upp till 8 km i undantagsfall) ger ca 800 km, men i praktiken sällan mer än 300 km | andelen inversion utökar omfattningen: användbara data för att uppskatta vänteavståndet |
| På sommaren Jordradie motsvarande cirka 16000 km (2.5 gånger den verkliga), med cirka 60 dB på mindre än 300 km förlust | konsekvent med k50 = 157/(157 − ΔN): k = 2,5 motsvarar ΔN ca 94 N/km, det är super-refraction |
| Mer än 3000 km endast i 20-30°-området norr och söder om ekvatorn, över haven, kopplad till Hadley-cellerna; topp i augusti i Afrika-Europa | utanför Europa: användbart att inte ge höga index på Atlanten utan återkoppling och för Kanarieöarna – Storbritannien |

Andra användbara data från samma artiklar:

- Optiskt intervall mellan två h1 och h2 meter (d i km): d = 4,1 · (√h1 + √h2), upp till cirka 20% för normal refraktion.
- Evanescens: långsam och grunt med inversion; stark och snabb med scatter och konvektiva celler. Programmet kan använda det för att klassificera annonser om det en dag kommer att ha SNR-rapporter över tiden.
- För den sporadiska E i 144 MHz rapporterar författaren DF5AI och Davis och Johnson (Nature 435, 2005) på bandet mellan temporals och Es: av tropo, men en separat indikator i morgon kan använda blixtdata.
- Grundreflektion i dubbelhopp Es: have K = 81 och 5 S / m, landsbygd och skog K = 13 och 0,006 S / m, urbana områden K = 3 och 0,001 S / m.

## 14. LIVETROPOVHF index hypoteser

Startförslag, för att diskuteras: ett index 0-10 på samma skala som Hepburn, beräknat för varje punkt i nätet och var tredje timme från GFS (då ECMWF och ICON för jämförelse) och ett andra nummer per rutt som säger hur mycket del av rutten passerar in i en användbar kanal till det valda bandet. Vikterna nedan är mina antaganden, att kalibreras med fläckarna.

Granska efter Bean och Dutton och Kerr: profiltrösklar kommer från deras tabell 6.5 och skala från deras mätningar, så ett refraktivt lager är värt 10-25 dB över den normala profilen. Indexet beräknas sedan först i dB och tar sedan 0-10; värdena i den saxen återstår att kalibreras med fläckarna.

Beräkning efter punkt, steg för steg

1. T urladdning, relativ fuktighet, geopotential höjd och vertikal hastighet vid 1000, 975, 950, 925, 900, 850, 800, 700, 600 och 500 hPa, plus T och 2 m fuktighet, marktryck, lagerhöjd och vind på 10 m.
2. Kassera nivåerna under marken; beräkning N och M på varje nivå (ITU-R P.453).
3. Beräkning −dN/dh i vart och ett av de fyra lagren av Bean och Dutton (1000–850, 850–700, 700–600, 600–500 hPa) och diagrammet som subrefraktivt, normalt eller superrefraktivt med trösklarna i deras tabell; inuti det första lagret använder också mellanliggande nivåer för att hitta den starkaste gradienten och dess andel.
4. Där dM/dh < 0 finner jag bas och toppen av kanalen, tjockleken d och underskott ΔM.
5. Jämförelse med den minsta tjockleken för bandet från formeln av Kerr (cirka 180 m för 144 MHz, 89 m för 432 MHz, som i Hepburn-bordet); under tröskeln räknas fortfarande kanalen, men mindre.
6. Jag stimulerar vinsten i dB med tabellen nedan (du tar det bästa lagret, inte summan av lagren, plus termen anomali) och konvertera den: index = dB / 2.5, mellan 0 och 10, så 25 dB är värt 10. Ett negativt värde (underrefaktion) visas som 0 men förblir i vägdata.

| Bästa lager situation | Kriterier | Uppskattade intäkter |
| Normal (inte laminerad) | −dN/dh i flikområdet. 6.5 (t.ex. 20–60 N/km mellan 1000 och 850 hPa) | 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 |
| Något över normalt | mellan den övre gränsen för det normala bandet och tröskeln för superrefraction | 0 till 10 dB, i proportion |
| Super-refraction | bortom lagergränsen (100, 80, 70, 50 N/km klättring) men under 157 N/km | från 10 till 15 dB, i proportion upp till 157 N/km |
| För tunn pipeline för band | dM/dh <0, tjocklek nedan Kerr tröskel | 15 15 15 15 |
| Användbar kontakt för band | dM/dh <0, tjocklek över tröskeln Kerr | 20 dB, +1 dB var 5 M-enheter underskott, maximalt 25 |
| Sub-refraction | −dN/dh <0 i det nedre lagret | −10 dB |
| Anomal bakgrund (du lägger till) | Modell Ns minus ITU karta klimatologiska Ns för den månaden | 0,2 dB per enhet N, mellan −5 och +5 dB |

Beräkning genom rutt (mellan två lokaler): prova den maximala cirkeln var 10 km, som redan gör programmet för indexet på vägen, och beräkna bråkdelen av punkter med användbar kanal och den genomsnittliga andelen av basen. CSIRO-studien visade användbar bråkdelen av rutten i rörledningen (på AIS-rutterna vid 162 MHz var 67% högre än i kontrollfallen).

Lägga till beräkningen per rutt (från LA0BY och W3SZ): (1) sjöstationer jämfört med bas och toppen av kanalen, eftersom en hög kanal bara tjänar dem som går in; (2) en troposcatter "golv" med Yeh-metoden, med de startande hörnen som tagits från markprofilen; tropevinsten i dB avlägsnas från den förlusten och resultatet jämförs med förlusten som stationen lyckas övervinna ( KK6MC -tabellen). Således blir indexet också en uppskattning av nåbart avstånd.

Korrigeringar för att prova en i taget (från Bean och Dutton och LA0BY, IK3XTV, G4MCU redan i handboken), för att hålla endast om de förbättrar jämförelsen med fläckar:

- Blandning (viktigt viktigt enligt författaren, genom daglig observation): från dag till dag på jorden, mellan morgon och eftermiddag med klar himmel eller med vind till marken över några m/s, försvinner kanalerna till marken; deras bidrag måste minskas. Inte till sjöss.
- Dagens tid (viktigt viktigt enligt författaren, genom daglig observation): irraggiamento-kanaler uppstår vid solnedgången och faller efter soluppgången; det är en 3-timmars prognoskontroll.
- Vertikal rörelse: luft stiger till 850-700 hPa remixar lagren (negativ korrigering), luft nedstigning under en anticyklon stärker dem (positiv).
- Trycktrender: uppför trycket på vägen under de senaste timmarna, högre och stabil signal; i minskad, lägre signal (bastano 2 hPa).
- Synonym: högt tryck över 1025 hPa, temperatur vid 850 hPa mycket över det på marken, varm och torr luft som rinner på det kallare havet, rutt på havet.
- Bergsvägar: ett andra "konvektivt" index (avsnitt IK3XTV), med ett eftermiddagsgränsskikt högre än åsarna längs vägen och högt tryck.

Från VHF/UHF DX Boka, andra korrigeringar för att försöka: på varandra följande dagar med högt tryck på punkten (mer dagar, fler poäng), läge jämfört med mitten av anticyklonen (bonus vid kanterna), tryckgradient på marken som mått på vinden, starkt regn i 24 timmar före (penalitet), låga moln vid kanten av lagret (endast för höga band). För riktningen av rutterna: jämföra azimuth av rutten med isobare.

Verifiering

- LIVETROPOVHF-arkiv: varje kommersiell över 500 km till 144 eller 432 MHz är ett "event". Det jämför indexet på rutten med frekvensen av händelser, samtidigt på dagen och månaden (samma tillvägagångssätt för CSIRO-studien).
- Jämförelse med Hepburn och F5LEN samma dagar: där de tre indexen kopplar bort ser du vem som var rätt enligt fläckarna.
- Radiosundationer på 00 och 12 UTC: för att kontrollera om modellen ser de inversioner som sonden mäter.
- Historiska fall av LA0BY (6-11/11/2003, 10/12/2004, 15/10/2011, 11/2/2015, 22/11/2018): indexet som beräknas på dessa dagar måste se de kanaler som Hepburn inte visade.
- Gränser som ska deklareras i handboken: genomförs under 100-200 meter nästan osynliga för GFS-trycknivåer; tillförlitlig prognos i 2-3 dagar, då bara trend.

## 15. Källor av den 21 november (andra delen)

- ITU-R P.1812 (MaTLAB/Octave implementation av I. Stevanovic, OFCOM; GitHub eeveetza/p1812, Zenodo 10.5281/zenodo.6141558). VHF/UHF-områdesspecifik metod för rutten, härrör från P.452: "delta-Bullington" diffraktion, troposcatter, kanaler och reflektion på lager, belastning på marken. Användbar som en tredje jämförelse bredvid P.452 i punktberäkning; ΔN- och N0 -data är fulla ITU-produkter, så användaren laddar ner dem (enligt P.453).
- från Costa, Rabanal Ramirez, Dias, IET MAP 2018 (doi 10.1049/iet-map.2017.0992). 563 MHz, 27 poäng mellan 5 och 30 km på oregelbunden terräng med skog: P.526 med 12 m vegetation −4,3 dB och ≤ 8,8 dB; P.1812 med 10 m av encumbrance +2,6 dB och ≤ 8,9 dB; dielectric wedge −4,8 dB och ≤ 9,2 dB; P.1546 +5,0 dB och ≤ 11,7 dB. För programmet: det kvarvarande felet i en bra diffraktionsmetod är cirka 9 dB, och höjden av beståndet på åsarna (10-12 m) räknas.
- PETOOL (Ozgun e altri, Computer Physics Communications 182, 2011). Parabolisk ekvation split-steg till en och två rutter med bred vinkel propagator (vinklar upp till 40-45 °, mot 10-15 ° av den smala en); botten med förluster genom den omvandlade Fourier blandade; mark till steg. Bekräftar valen av 1.3.2 (omfattande vinkel, jordkoefficienter) och anger tvåvägsberäkningen för branta lättnader.
- Mathworks. `tropopl`: förlust av gaser och förlust av "objektiv" i en skiktad atmosfär (P.835, P.676, P.453), utan kanaler: med gradienter under −157 N/km är den geometriska optiska modellen inte giltig. Exemplet på radarförökning använder för refraction ḲN = 7,32 ^ (0,005577 · Ns), samma CRPL-rapport redan i programmet. `propagationModel` Longley-Rice: fördefinierade horisontella polariseringsvärden, konduktivitet 0.005 S/m, tillåtelse 15, reaktivitet 301, tempererar kontinentalt klimat. Ingen MathWorks modell handlar om kanalerna.
- ITU-R, digitala produkter av grupp 3. Data och program av P.453, P.452, P.1812, P.2001, P.528, P.1546; "integrerade" data är en del av rekommendationerna; ingen deklarerad omfördelningslicens: du fortsätter att låta dem ladda ner till användaren.
- ESA Navipedia, troposferisk försening. N = Nidrostatica + Numida; den torra zenitalfördröjningen är cirka 2,3 m och den fuktiga en tiotals cm, som varierar snabbt. För oss är den våta fördröjningen som uppskattas av GNSS-stationerna ett indirekt mått på den integrerade fuktigheten, möjlig framtida källa till nu-casting; Idag är inte användbar.
- Bray, K2LMG, "En metod för att bestämma V.H.F. Station Capabilities", QST November 1961, pp. 36-40 (läs i original; rechecked den 22 november på PDF av antalet QST: alla värden nedan motsvarar). Författaren D. W. Bray (General Electric, Ithaca), säger att han har verifierat prognoserna med månader av schemalagda anslutningar med K2GOI och W4LTU. Evanescens inom 13,5 dB över cirka 100 miles (7 dB under och 7 över det timliga genomsnittet), nästan linjär under (3,5 dB till 50 miles): för en solid anslutning ta bort 7 dB. Korrigering av horisonten: varje höjd av horisonten är värt 69 miles (111 km) av extra avstånd (mindre om negativ). Få en yagi ca 10 × boom/λ (power ratio), mer cirka 4 dB markreflektion; SSB −3 dB och AM −7 dB jämfört med CW; 99% av timmarna: cirka 180 dB till 60 miles, 190 till 90, 200 till 240 (2 m). Från 1,3,2 sannolikheten för perioden använder σ = 3,4 dB (±7 dB som ±2π) istället för 5 dB uppskattad second hand.
- Tvåvägsberäkning (försök med prototypen, 21 november). Stor vinkel parabolisk ekvation med verklig S RTM1 terräng från Capri, 144 MHz, antenn 10 m från marken, standard atmosfär; mått: genomsnittlig effekt mellan 20 och 400 m till 80 km på vägen mot motsvarande. Från centrum av Capri och Marina Grande är västern stängt (hinder till +11-21 °, Monte Solaro): Genom att peka på Faito (nästan bakom EA5 och EA6, 150-160° av axel) stiger signalen mot Spanien med 10-29 dB; pekar Vesuvius (110-125° av axel, sedan ut ur beräkningsplanen, endast vägledande) med 17-25 dB. Från Anacapri är västvärlden fri och direktvinster med 8-22 dB. De absoluta förlusterna på 1000-1300 km av prototypen är dock inte tillförlitliga. EA3 EA5 EA6
- Barsis och Kirby, NBS Report 6751 (1961), Pikes Peak, 223 km, 100 och 751 MHz. Berget är en vanlig terminal på två vägar i optik: fördelningen av de timmedianer är konvolutionen av de två rutterna. Långtidsutflykt (1-99%) av cirka 14 dB, mot 26-40 dB av en troposcatterbana av lika lång eller vinkel; ingen märkbar dagsförändring. Signalen är starkare än troposcatter men svagare än den ideala kanten: toppen ska behandlas som rundad (ungefär ca 11,5 km). Höga signaler observeras alltid bakom dorsalerna.
- Barsis, NBS Report 7652 (1963), tre vägar över ett berg (Colorado, Alaska, Europa), 750-900 MHz. Genomsnittlig förlust 10-20 dB högre än Fresnel-Kirchhoff beräkning utan mark nära terminalerna; evanescensen uppskattas genom att linda de två linjerna i optik; reflektionerna av jorden nära terminalerna förvärrar den långvariga blekningen. Från 1.3.2 lägger ingången i ledningen till rundningen av P.526.
- Barsis och Johnson, NBS TN 88 (1961). Långvarig upplösning av rymdvågen, även på en väg bortom ett berg (§ 5).
- Norton, "Systemförlust i radiovågsförökning", J. Res. NBS 63D (1959). De kurvor från vilka Bray får nomogrammen; definitioner av systemförlust, överföring och grundläggande processantennvinst.
- Rice, Longley, Norton och Barsis, NBS TN 101 (1965–67), volymer 1 och 2. enstaka kant och hinder avrundat med och utan reflektioner (§ 7, med exemplet på Pikes Peak), långsiktig variabilitet V(0,5) och Y(q) för klimat (härdad konkontinental och andra). För oss: metoden för rundade hinder och variabilitetsfunktioner är hänvisningen till kalibrerad osäkerhet.
- ITU-R P.526-16 (11/2025). Formel (31) av J(W), identisk med programmet; rundade hinder (32)-(36), nu används; multi-split metoder (Bullington och full metod § 4.5).
- ITU-R P.1812-8 (2025) och -7. Fullständig text återhämtad: specifik metod för 30 MHz till 6 GHz (delta-Bullington, troposcatter, kanaler och reflektion på lager, belastning). -7 varnar för att dess troposcatter (från P.617-5) är fel under 66 km. Det är fortfarande en möjlig tredje jämförelse.
- Paulus, "Praktisk tillämpning av IREPS-avdunstningsmodellen", NOSC TR 966 (1984) och Radio Science 20 (1985). Rapport läs den 22/11/2026 (DTIC ADA146528, skanna med erkänd text, ibland oläslig). Luftsjöskillnader som mäts med fartyg flyttas mot stabilitet: varmare havsluft i 31-35% av fartygsdata jämfört med 0,7-7% av NOAA-björnar, för psicrometer störda av fartyg och SST som tas med kylvatten (cirka + 1 °C). Jeske-modellen ger sedan 40 m och mer än 15% av året och höjder som hoppar från 10 till 40 m på 24 timmar med stabil tid, vilket inte bekräftas av radioåtgärder. Föreslagen korrigering (testade trösklar 0, −0,5 och −1 °C; −1 °C är bättre överens med radioåtgärderna i juni 1982): om luft-havskillnaden är −1 °C eller mindre den uppmätta används; annars beräknar du höjden med −1 °C och med 0 °C och, om det med −1 °C är lägre, håller du det. Ökningar på den stabila sidan skärs, minskar kvar. I juni Marsden 120 kvadrat: genomsnittlig höjd från 16,8 m (klimatologi) till 7,6 m. Modellens ekvationer finns inte i rapporten (se Jeske och Hitney). Programmet använder den neutrala profilen, som inte har stabil överdrift och begränsar δ till 40 m: det går i samma riktning; med SST-modeller (inte fartyg) mätfel som Paulus korrigerar väger mindre.
- Levy, "Parabolic Equation Methods for Electromagnetic Wave Propagation" (IEE 2000). På Google Libri finns det bara förhandsvisning: Jag läser inte.
- VK3FS (3fs.net.au). Kvalitetsintroduktion: höga kanaler vid 450–2000 m för längre öppningar på marken; inversioner av 300–600 m i lugna och lugna nätter, som försvinner efter soluppgången; starkare kustinversioner på eftermiddagen; 800–1300 km typiska, 1400–1600 km ofta genomförda. Konsekvent med dag / nattvarningar och taket på 2500 m.

## 16. Källor

Regler och formler

- [ITU-R P.453-13 (2017), radiorefraktivt index:](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)-formel och data
- [ITU-R P.452-18 (2023), förutsägelse av störningar mellan markstationer över 100 MHz: motsvarande radie, β0, troposcatter,](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)-kanaler
- [ITU-R P.617-5, förökning bortom horisonten för radiobroar](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)
- [Shi et al.] "En omfattande studie om våglängd av elektromagnetisk förökning i olika förångningsdukter", IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

Studier om förutsägelse av kanaler med vädermodeller

- [P. Lopez, "En 5-årig 40-km resolution global klimatologi av super-refraction för markbaserad meteorologi radar", ECMWF Tech. Memo 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO), "Troposfärisk Ducting Prediction baserat på GFS Model Data på... CSIRO Murchison Radio-astronomi", Observatory arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (FMI), "Effekter av modellupplösning för att förutsäga atmosfäriska kanalförhållanden med Harmonie-AROME", EGUsphere 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

amatörradio prognoser

- [W. R. Hepburn, "The Hepburn Tropo Index (HTI)"](https://www.dxinfocentre.com/propagation/hti.htm)och [](https://www.dxinfocentre.com/propagation/history.htm)metodhistoria
- [W.R. Hepburn, "Lowest Usable Frequency vs. Thickness of Ducting Layer"](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, "Worldwide refractive index prognoser"](https://tropo.f5len.org/WW/info.php), [ny version 2026](https://tropo.f5len.org/?p=504), [observera på modell ARPEGE](https://tropo.f5len.org/author/f5len/)

Datadata Data Data

- [Open-Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api), [observera på trycknivåer](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)
- [NOAA NCEI, Integrated Global Radiosonde Archive (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RAOBget, radio översvämningsmanus för University of Wyoming](https://github.com/NCAR/RAOBget)

Klassiska texter (indikerad av programmets författare, som ska konsulteras för slutliga formler)

- B. R. Bean, E. J. Dutton, *Radio Meteorology*, NBS Monograph 92, 1966, 435 sidor (reprint Dover 1968): vertikala profiler av N och M, gradient av fälla under −157 N/km, jämförelse med radiovåg; kapitel 5 "Synoptisk radio meteorologi". Basera för 2-4 steg beräkning per punkt. Gratis: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [NIST-kort, doi 10.6028/NBS.MONO.92](https://www.nist.gov/node/1770911), [Internet Archive](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf)kopia.
- L. Boithias, *Radio Wave Propagation*, McGraw-Hill 1987 (översatt av D. Beeson från franska, 330 sidor, ISBN 0070064334; även North Oxford Academic): mekanismer bortom horisonten, gemensam volym av spridning och förlust av koppling antenn-half. Enbart papper ([bibliotekskort](https://libcat.library.tamu.edu/Record/in00000014812/Description)).
- M. P. M. Hall, *Effects of the Troposphere on Radio Communication*, IEEE Electromagnetic Waves Series n. 8, Peter Peregrinus 1979, 206 sidor (ISBN 0906048257; brosura 0863410863): synoptiska situationer och förökning VHF/UHF. Bas för synoptiska ingredienser. Handel på IET ([](https://libcat.library.tamu.edu/Record/in00000636385)kort).
- H. G. Booker, W. E. Gordon, "A Theory of Radio Scattering in the Troposphere", *Proceedings of the IRE*, vol. 38, n. 4, April 1950, pp. 401-412: Scapping teori från det refraktiva indexets disogenitet. Central idé: den diffusa kraften beror på intensiteten och omfattningen av turbulenta fluktuationer av det refraktiva indexet (exponentiell korrelation); nära horisonten råder diffraktion, ytterligare spridning; Enligt författarna är omfattningen av turbulens och indexvariationerna som förväntas av meteorologi tillräckliga för att förklara de uppmätta fälten. Citerad av 328 artiklar (Scilit), ingen gratis åtkomstkopia. Kort på Semantic Scholar ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); full text på IEEE Xplore betalas ([doi 10.1109/RPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)).
- C. M. Crain, J. R. Gerhardt (Electrical Engineering Research Laboratory, University of Texas), "Measurements of the Parameters Involved in the Theory of Radio Scattering in the Troposphere", *Proceedings of the IRE*, vol.40, n.1 januari 1952, pp. 50-54, doi 10.1109/JRPROC.1952.2739 ger de verkliga värdena som Booker och Gordon hade bara uppskattat ([Skilit X0 tab]).](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)
- "The Constants in the Equation for Atmospheric Refractive Index at Radio Frequencies", *Proceedings of the IRE*, vol. 41, n. 8, August 1953, s. 1035-1037: föreslår att granska strömmarna sedan i bruk (79 K/mb och 4800 K) i formeln N; det är härifrån som värdena 77,6 och 3,73 · 10 ^ 5 i formeln ITU (Q](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E.C. S. Megaw, "Fundamental radio scatter propagation theory", *Proceedings of the IEE Part C: Monographs*, vol. 104, n. 6, september 1957, pp. 441-455, doi 10.1049/pi-c.1957.0052: spektrum av indexfluktuationer från turbulensteori, fullständig spårning av scatter utöver den gemensamma horisonten och effekten av geometri som betalas vid IET ([Shilit](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)kort).
- Proceedings of the IRE*, oktober 1955, specialnummer på scatter propagation (av K. A. Norton och J. B. Wiesner), med Bullington, Staras och andra: gratis i PDF på [World Radio History](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf).
- D. E. Kerr (redigerad av), *Propagation of Short Radio Waves *, MIT Radiation Laboratory Series vol. 13, McGraw-Hill 1951 (rapporterar Dover 1965 och IEEE 1987): teori om avdunstning och ytkanaler på havet. Digital kopia: [World Radio History](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); IET-utgåva i [IET Digital Library](https://shop.theiet.org/propag-short-radio-waves).

Nya åtgärder av troposferisk diffusion i VHF: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Australien), "En studie av VHF Troposcatter Communication Channels i South Australia", i * Digital Signal Processing for Communication Systems*, Springer 1997, pp. 103-112 ([Springer](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)). Text för en avgift; från abstrakt det gäller diffusion, inte ledningarna. Användbart endast om du vill kalibrera termen troposcatter med VHF-kanaldata.

Numeriska modeller och sjövägar (indikerad av programmets författare)

- M. Levy, *Parabolic Equation Methods for Electromagnetic Wave Propagation*, IEE Electromagnetic Waves Series #45, 2000, 336 sidor (ISBN 085296767640): parabolisk ekvation för refraktiva profiler som förändras längs vägen och för orografi; kapitel om tropoferisk förökning, strålar och sätt, förökning på havet. Det skulle tjäna för en fältberäkning längs vägen, bortom indexet. För betalning, även i elektronisk form från IET ([IET-kort](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)).
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (University of Leicester): 2 GHz mätningar på Kanalöarna och kanalen. Många verk och avhandlingar är gratis i universitetsarkivet: [långsiktig statistik om avdunstningskanalen, Radio Science 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434), [signalförändringar på tre havsvägar, Radio Science 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710), [Titlar av N. Mufti 2012 med rutter på 50 och 140 km också i VHF](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854), [tesis av I. Alam 2002 om reaktivitet beräknad från väderdata, 240 MHz och 2 GHz X3 UQ, [tesi.](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus, "Praktisk tillämpning av en förångningsmodell", Radio Science vol. 20 n. 4, 1985, s. 887 (inte IEEE Transactions on Antennas and Propagation), doi 10.1029/RS020i004p00887 ([scheda](https://zendy.io/title/10.1029/rs020i004p00887)): höjden av förångningskanalen från ytväderdata. Frigör den tekniska rapport som föregår den: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf).

T. Lindquist, *Wave Propagation Models in the Troposphere for Long-Range UHF/SHF Radio Connections*, masters avhandling, University of Karlstad, 2 juni 2020, 106 sidor (tillhandahållen av programmets författare). Det är en steg-för-steg-guide, gratis, att skriva en beräkning med den paraboliska ekvationen: split-step-lösning (även vid breda vinklar), gränsvillkor för mark och hav, steg mark, strålspårning på linjära profiler ibland, med numeriska kontroller; citerar PETOOL-fritt verktyg för MATLAB. Två användbara data samtidigt: (1) N känslighet för vädervariabler, δN ≈ 0,26 δP + 4,3 δ − 1,4 δT (hPa och K, cirka 20 ° C), dvs ett fel på 1 hPa på ångtrycket väger tre gånger ett 1 ° C fel på temperaturen: är fukt av modellen som måste kontrolleras först; (2) Genomsnittlig höjd av avdunstningskanalen: 5-6 m i Nordsjön, 13-14 m i Medelhavet, över 20 m i Persian Gulfen, alltid för låg Författarens slutsats (frekvenser 1 till 10 GHz): utöver horisonten kräver de tillförlitliga anslutningarna nästan alltid en kanal.

Propagationsprogramvara på markprofilen (för horisont och hinder, inte för den förväntade tropo)

- [Radio Mobile](https://ve2dbe.com/rme.html)av Roger Coudé VE2DBE: gratis, Windows (även Linux och Mac med vin), Longley-Rice / ITM modell, terrängprofil automatiskt extraherad från kvotdata (nedladdning](https://ve2dbe.com/download/download.html)).
- SPLAT!](https://qsl.net/kd2bd/splat.html)av John Magliacane KD2BD: GPL, 20 MHz-20 GHz, Longley-Rice och ITWOM v3.0 modeller, hinder och förlust rapporter i text; senaste version 1.4.2 av 2014.
- [ITM, NTIA/ITS](https://its.ntia.gov/software/itm)officiella kod: Longley-Rice modellen (20 MHz-20 GHz) med källa och dokumentation, bas av Radio Mobile och SPLAT!. Koden är gratis och du kan lägga till beräkningen på LIVETROPOVHFs väg (förlust på grund av marken, för att läggas till i tropoindexet). Ursprunglig rapport: A. G. Longley, P. L. Rice, "Prediction of Tropospheric Radio Transmission Loss Over Irregular Terrain: A Computer Method - 1968", ESSA Technical Report ERL 79-ITS 67, juli 1968 (NTIA-kortet på historisk programvara: X1 https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITU-R, programvara och digital data](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): gratis kartor över ΔN och N0 årliga medianer på rutnät 1,5 ° som används av P.452 och P.617 - användbar som referens klimatvärden för indexet.
- Pathloss (Contract Telecommunication Engineering): kommersiellt program för radiobroar; Jag har inte hittat en offentlig källa som bekräftar vilka ITU-rekommendationer som genomförs, så att kontrollera.

Återanvändbar GitHub-kod i LIVETROPOVHF

| Projektprojekt | Vad han gör | Språkspråk | Använd möjlig för oss |
| [NTIA/ITM](https://github.com/NTIA/itm)] | modell Longley-Rice tjänsteman i NTIA, med paket för. NET |  C++, C#  | markförlust på vägen: integreras direkt i programmet, som är i C# |
| [fuzzzeee/itm](https://github.com/fuzzzeee/itm)] | ITM C++ kod C# översättning |  C#  | alternativ utan inhemska bibliotek, även användbar för Windows 7-versionen |
| [eveetza/Py452](https://github.com/eeveetza/py452) | ITU-R P.452, identisk med MATLAB/Octave-versionen som godkänts av ITU-R WP 3M; MATLAB-versionen publicerad av Swiss Federal Communications Office | Python, MATLAB | referens till översätta till C# troposcatter formler och genomförs, med ITU validering exempel |
| [bwinkel/pycraf](https://github.com/bwinkel/pycraf) | P.452 komplett, P.676, SRTM-profiler; GPL v3, men ITU-datafilerna som ingår är inte gratis för kommersiell användning | << Tidigare - << | kontroll över våra resultat |
| (ITU-Rpy](https://github.com/inigodelportillo/ITU-Rpy)) | många ITU-R-rekommendationer, inklusive P.453-13, validerade med ITU-exempel | << Tidigare - << | verifiering av N-formeln och klimatkartor av P.453 |
| [NCAR/RAOBget](https://github.com/NCAR/RAOBget)] | Ladda ner radiovågen vid University of Wyoming i textformat | << Tidigare - << | diagram att ladda ner i C# profilerna för de italienska stationerna |

Notera: den aktuella versionen av P.453 är [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), nedladdningsbar kostnadsfritt; i dokumentet använde jag 13, för att förverkligas.

Redan i programhandböckerna och återanvändbara här: LA0BY (Nordic VUSHF 2019), IK3XTV ("Propagation på 2 meter" och "Propagation VHF - konvektiva celler och gravitationsvågor", Radio Magazine, som tillhandahålls av programförfattaren), DF5AI/DK5YA (2003), G3YGF (QST 1983), VHF / UHF DX Book cap. 2, G4MCU (2003) KP4MD
