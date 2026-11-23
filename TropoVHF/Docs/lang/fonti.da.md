# Kilder og formler

> Automatisk oversættelse af italiensk: i tvivlstilfælde er det den italienske guide værd.

Arbejdssamling af kilder, der er konsulteret i forbindelse med overslaget over LIVETROPOVHF tropo (20. til 21. november 2026): Grundlæggende fysik, tilgængelige data, offentliggjorte indeks, formler, amatørradiooplevelser, validering og beslutninger. Formlerne er nummereret som i LaTeX, (1), (2),..., og i teksten de husker med nummeret i parentes. Den Prognose Manual beskriver, hvordan programmet bruger dem.


## 1. Forfatterens lokaler

IC8TEM observationer, der danner rammen om arbejdet, med feedback fundet i kilderne.

- Den italienske geografi kræver diffraktion. Med dorsal, promontory og øer et par dusin kilometer fra stationerne, diffraktion bidrag beregnes med Fresnel parameter på hovedkanten, ikke kun med det glatte land. Møde: i en sammenligning på uregelmæssig terræn på 563 MHz (Rio de Janeiro, 27 point mellem 5 og 30 km), de bedste metoder var P.526 med blade kanter, P.1812 og diffraktiv kile, med gennemsnitlig fejl mellem − 5 og + 3 dB og spredning af omkring 9 dB; P.1546 var den mest pessimistiske (fra Costa, Rabanal Ramirez og For stejle skråninger nær PETOOL station huske, at de tjener bred vinkel propagatorer og to- vej beregning (bølge afspejlet ved reliefferne).
- For at komme ind i en fjern kanal, skal du bruge Fresnel. Hvis kanalen begynder ud over horisonten, signalet kommer til os ved diffraktion over forhindringer indtil dens begyndelse; fra 1.3.2 programmet beregner det (Preview Manual, § 10 ter). Møde: Rohde & Schwarz (2023) bemærker, at stationer langs en høj kanal ofte undlader at komme ind, mens dem tæt på enderne ja.
- Forholdene ændres hurtigt. Teorierne er der, men betingelserne ændres i minutter, og intet prognosesystem tager data i realtid: GFS kommer ud hver 6 timer, med tidsspan og 0,25 °, og en prognose kan miste værdi inden for kort tid. Møde: Bray (QST, november 1961) måling af timelønnede udsving på ca. ± 7 dB over 100 miles; kanalerne bevæger sig med atmosfærisk tid (Rohde & Schwarz); vores QSO register på 29 / 12 / 202-2 / 1 / 2022 viser åbninger vokser og falder inden for timer.
- Den bedste nu-casting forbliver DX cluster-PSKReporter netværk. At vide, hvad der sker nu, klyngepletter, PSKReporter (og WSPR) slog ethvert mønster: de er foranstaltninger. Prognosen siger, hvor man skal kigge og hvor længe det kan vare; reklamer siger, hvis det er der. Programmet for dette sætter plads og prognose på samme kort.

## 2. Formål

Ideen er et tredje indeks, beregnet af LIVETROPOVHF selv startende fra de lodrette profiler af en vejrmodel, at være sammen med Hepburn og F5LEN i stedet for at erstatte dem. I dag læser programmet deres kort allerede klar: et indeks ville også sige * hvorfor * der er åbning (kvote og tykkelse af kanalen, minimum band fanget), og det kunne forekomme med de pletter, som programmet indsamler allerede.

- Hvad kan du give: Sandsynligheden for kanal per punkt og per time, andel af base og top, tykkelse, mindste frekvens fanget, og langs en sti brøken af ruten inde i en kanal.
- Hvad kan ikke give: de tynde kanaler (under et par ti meter), som de globale modeller ved 0,25 ° ikke løse, og fordampningskanaler på havet, som først og fremmest tjener fra GHz op.
- Første praktiske mål: 144 og 432 MHz i Europa, med verifikation på DX klyngespots, PSKReporter og lokale dekodninger.

## 3. Grundlæggende fysik

Alt starter fra brydning N beregnet ved hvert profilniveau: det er kanalen, hvor den modificerede reaktivitet M falder klatring, dvs. hvor dN / dh falder under − 157 N / km ([ITU-R P.453- 13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)).

Refraktion (P og i hPa, T i kelvin):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Damptryk fra relativ fugtighed H (%) og temperatur t (° C) over vand mellem − 40 og + 50 ° C:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

Modificeret reaktivitet (h i km eller 0,157 · z med z i meter) og kanalkriterium:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN / dh (N / km) | Forordning | Effekten af 144 MHz |
| 0 | sub- refraktion | reduceret strømningshastighed |
| fra 0 til - 79 | - 40) | radiohorisont med ækvivalent radius 4 / 3 |
| fra − 79 til − 157 | super- refraktion | aflang strømning, "for god" |
| under − 15 | Rørledning (fældning) | indfangede signaler, DX over 1000 km |

Hvordan man udtrækker N fra en model ved tryk niveauer: for hvert niveau du tager T, relativ fugtighed og geopotential højde, du beregne N og M, derefter gradienten mellem tilstødende niveauer. Niveauerne under jorden bør kasseres (ved 1000 hPa på Alperne er under jorden).

Minimum fanget frekvens: ledningen guider kun bølger temmelig kort i forhold til dens tykkelse. Hepburn udgiver en bordtykkelse → mindste brugsfrekvens ([DX Info Centre](https://www.dxinfocentre.com/propagation/luf.htm)); det er en blød grænse, under hvilken signalet gradvist falder.

| Led ansøgninger | Mindste betalingshyppighed |
|  61 m  | 962 MHz |
|  91 m  | 464 MHz |
|  122 m  | 276 MHz |
|  152 m  | 185 MHz |
|  183 m  | 133 MHz |
|  213 m  | 101 MHz |
|  305 m  | 53 MHz |

I VHF-området (ofte 120- 300 m) følger tabelværdierne ganske godt en lov om konstant; under 100 m undervurderer formlen frekvensen op til 30- 40%. Tilpasning det selv (ikke en formel udgivet af Hepburn), med f i MHz og d i meter:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

Dybest set, for 2 m, en ledning er ofte mindst 170- 180 m, for 70 cm er det omkring halvdelen. Den formel, der cirkulerer på nettet, λmax = 0,014 · d ^ 1.5, kommer fra Kerr (1951, kapitel 1, eq. 18) og er gyldig med λ i centimeter og stående, for en kanal, hvor M falder lineært: med disse enheder giver for omkring 2 m 180 m og for omkring 70 cm 89 m, det er de samme værdier af Hepburn bordet, som kommer fra det. Kerr advarer om, at det ikke er et net cut: selv bølger flere gange længere lider en masse af kanalen, og en måde, at "tab" lidt kan forbedre modtagelsen selv med stationer ud af kanalen. En IEEE Access-undersøgelse fra 2019 bekræfter, at de klassiske metoder (Kerr, Kukushkin) ikke beskriver det virkelige snit ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)): Tærsklen bør anvendes som en indikation, ikke som en mur.

## 4. Data tilgængelige gratis

Det svage punkt er den lodrette opløsning: mellem 1000 og 850 hPa modeller giver et niveau hver 25- 50 hPa, det er hver 200- 450 m, så de ser godt de inverterede til 500- 2000 m, men ikke de tynde kanaler nær jorden. LIVETROPOVHF allerede bruger Open- Meteo til isobare, temperatur til 850 hPa og inversion: indekset kan starte fra de samme opkald.

| Kilde | Hvad giver | Opløsning | Anvendelse til indeks |
| [Open- Meteo, GFS](https://open-meteo.com/en/docs/gfs-api) | T, relativ fugtighed, dugpunkt, geopotentiel højde efter niveau (1000, 975, 950, 925, 900, 850 hPa...); højde af grænselaget | 0,25 ° på trykniveauer; 16 dage, timer op til 120 timer, opdateret hver 6. time | Profil N og M, gradient, base og top af kanalen; allerede i programmet |
| [Open- Meteo, ECMWF](https://open-meteo.com/en/docs/ecmwf-api) | samme niveauer fra open- data modeller 0,25 °; jorden giver dugpunkt |  0,25°  | anden udtalelse, som er nyttig for overensstemmelse mellem modellerne |
| [Open- Meteo, DWD ICON](https://open-meteo.com/en/docs/dwd-api) | T og relativ fugtighed på trykniveauer; i ICN- D2 er der 800, 900 og 925 hPa | ICON- EU ca. 7 km | Tredje udtalelse om Europa |
| [Radiosounds, Univ. Wyoming](https://github.com/NCAR/RAOBget) | opmålte profiler, to starter om dagen (00 og 12 UTC), tekstformat pr. station | mange betydelige niveauer, selv hvert par dusin meter | indeksverifikation; se de subtile kanaler, som modellen ikke kan se |
| [stønner, NOAA NCEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive) | historisk arkiv over udsendelser, en fil pr. station, mere afledte parametre | som ovenfor | kalibrering af års data og sammenligning med fortidens pletter |
| ERA5 (Copernicus) | timegenanalyse siden 1940, 37 niveauer af tryk | ca. 28 km | statistik og kalibrering, ikke prognose |

- Med Open- Meteo geopotential højde er over havets niveau: de niveauer, der falder under jorden skal kasseres ([Open- Meteo, øvre luft](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)).
- På jorden er tilføjet T og fugtighed på 2 m: de giver den første strækning af gradient, den, hvor nattekanalerne er født på sletten og på havet.
- Italienske udsendelser på University of Wyoming arkiv (weather.uwyo.edu / upperair / sounding.html): stationer og numre WMO i tabellen nedenfor.

| Station | W | Breddegrad | Længdegrad | Del |
| Udine / Campoformido |  16044  |  46,03  |  13,18  |  94 m  |
| Milano / Linate |  16080  |  45,43  |  9,28  |  103 m  |
| Havpraksis |  16245  |  41,65  |  12,43  |  12 m  |
| Brindisi |  16320  |  40,65  |  17,95  |  10 m  |
| Trapani / Birgi |  16429  |  37,92  |  12,50  | ca. 14 m |

Tekstformatet for Wyoming giver for hvert niveau tryk, højde, temperatur, dugpunkt og fugtighed: nok til N og M. Wyoming ikke har en officiel grænseflade, scripts bruges som [RAOBget af NCAR](https://github.com/NCAR/RAOBget); College of DuPage holder kopier i tekst ved station ([fx 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)).](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. Indekser og offentliggjorte metoder

Hepburn og F5LEN starter fra de samme fysiske ingredienser, men ikke offentliggøre den komplette algoritme; de videnskabelige undersøgelser af 2008- 2026 i stedet beskrive metoden trin for trin, og det er derfra, at det er bedst at kopiere opskriften.

| Metode | Startdata | Hvad er beregnet | Nyttige noter til os |
| [Hepburn Tropo Index](https://www.dxinfocentre.com/propagation/hti.htm) | rå vejr modeller af Canada Meteorologisk Service (MSC, som skrevet under sine kort; ikke GFS / NAM), kort designet med Gradis, proprietære algoritme ([storia](https://www.dxinfocentre.com/propagation/history.htm)) | 0-10 lineær skala for signalkrumning: 0 normal, 1-2 superrefraktion, 3-4 nogle kanaler, 5-6 diffuse kanaler; negative værdier = subrefraktion | den skala, der skal efterlignes, så det tredje indeks straks sammenligner med de første to |
| [F5LEN, verdens prognose](https://tropo.f5len.org/WW/info.php) | for Europa ARPEGE model af Météo-Frankrig, 7,5 km, flere niveauer mellem 2 m og 1500 m ([note af forfatter](https://tropo.f5len.org/author/f5len/)) | Gennemsnitlig indeks for udlejer, 40 skridt fra 3 timer, 4 beregninger om dagen, 28-dages arkiv ([ny version](https://tropo.f5len.org/?p=504)) | bekræfte, at det tjener en model med mange lave niveauer |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf) | 60 ECMWF-modelniveauer fra jorden til 3000 m | minimum dN / dz pr. kolonne: udført under − 157 N / km, superbrydning under − 78,7 N / km; bundfældningslaget | klimatologi: Po Valley det europæiske område med flere rørledninger året rundt; Middelhavet om sommeren nær 100%, om vinteren under 15%; Italien over 30% på sommernætter |
| [Indermuehle og Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861) | GFS 0,25 °, 21 trykniveauer + felter ved 2 m | M = N + 0,157 z; minimum dM / dz under 3 km, bund og top af kanalen, dybde af M underskud; under 40 m kasseret | verificeret i 7 år på 25 kanaler fra 88 MHz til 2,68 GHz; god kapacitet op til omkring 3 dage i forvejen (AUC omkring 0,74), derefter falder; GFS bedre end ERA5 |
| [Hämäläinen et al., IMF 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf) | Harmonisk-AROME, 750 m eller 2,5 km, 65 eller 90 niveauer | dM / dh < 0 på modelprofilen, sammenlignet med en 4-59 m stråle | tæller mere lodret end vandret opløsning; ingen model ser kanalerne under 15 m; fugt dominerer temperaturgradienten |

[TROPOCAST.EU](https://tropocast.eu/)Artur SQ3A, for 2 m og 70 cm i Europa. Fra prognosesiden ([tropocast.eu / prognose](https://tropocast.eu/forecast)):

- Data: DWD ICON- EU (pas 0.0625 °, ca. 7 km), standardkilde; alternativt canadisk GDPS (0.15 °), mindre detaljeret.
- Metode: N og M ifølge ITU-R P.453 fra profilen mellem laget nær jorden og 850 hPa; anerkendelse af lagene, foreningen af kanaler til jorden og høj og tærskelværdierne for farverne er en ordentlig kalibrering af forfatteren, ikke offentliggjort.
- Produkter: "komplet" visning 0-3000 m, mere separate visninger "til jorden" og "høj" (for nu kun i de kort, der skal indarbejdes); score 0-100 i ni klasser (under 38 baggrund, 38-52 marginale, 53-58 svage, 59-65 moderat, 66- 72 god, 73-80 meget god, 81-88 høj, 89- 95 stærk, 96- 100 meget stærk); prognosen op til + 72 timer på trin af en time, med animation og arkiv.
- Områder: Europa, Polen, Det Forenede Kongerige, Tyskland, Frankrig, Spanien, Italien, Tjekkiet, Kroatien; grænseflade også på italiensk.
- Reuse: kort er indarbejdet med en API keyless iframe; der er også en dokumenteret API (OpenAPI og JSON schema). Licensen er ikke angivet: Før du bruger det som en tredje kilde ved siden af Hepburn og F5LEN bør du spørge forfatteren.

Forskelle fra vores projekt: TROPOCAST stopper ved 850 hPa (omkring 1,5 km), mens de forhøjede kanaler ankommer til 2000 m og derover ( LA0BY tilfælde af 22 / 11 / 2018), og giver ikke et indeks for band eller rute. Klasser 0-100 kan sammenlignes med vores indeks 0-10 på de samme dage.

TROPOCAST billedstruktur (fra linket fra forfatteren af programmet): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, hvor n er prognosetrin. Webstedet forbyder automatisk tilbagetrækning til robotter, så jeg ikke kunne læse billedet; også for dette, før vise disse kort i LIVETROPOVHF tjener forfatterens tilladelse.

Notebook: [DJ5AR, kategori](https://dj5ar.de/?cat=174)(Magonza, 21 artikler 2017- 2023, 2 m- 13 cm). EU-erhvervsgrenens økonomiske situation Anerkendelse af tilfælde af "for høj" kanal (vending til 1000 m), som du ikke kan bruge fra jorden: en anden bekræftelse af, at det er nødvendigt at sammenligne andelen af kanalen med andelen af stationerne. Brug radioer som en blænde foranstaltning (op til 1028 km) og de lodrette profiler i Paris, Bordeaux og DWD. FT8

Sammenfattende, den fælles opskrift: lodret profil → N og M pr. lag → lag med dM / dh negativ → base, top, tykkelse og kraft af kanalen → indeks. Den "hemmelige" del af Hepburn er den empiriske kalibrering af virkelige begivenheder, som vi kan genskabe med pletter.

## 6. Nyttige formler til programmet

Ud over N og M (grundlæggende fysik afsnit), disse bruges til at bevæge sig fra en lodret profil til et punkt nummer og derefter ad sti. ITU-formlerne var blevet rekonstrueret af en PDF-læst til dels dårligt; den 22. november blev der foretaget en gennemgang af de officielle tekster ([ITU-R P.452- 18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453- 14, P.526- 16, P.372- 18) og korrigeret i koden: konstant af troposcatter 190 (ikke 190,1) plus absorption af gasser med ρ = 3 g / m3; α ikke under − 3,4 (eq. 55a); gas af kanalerne med ρ = 7,5 + 2,5 · ω (eq. 9a); diffraktion på fuld sfærisk jord (eq. 23-37: gennemsnitlig land- hav, højdefunktion med grænse 2 + 20 g. Rumore P.372-18: tabel 1 og galaktisk svarer; Kunstig støj kurver er op til 250 MHz og galaktisk op til omkring 100 MHz, over programmet udvider de samme love.

1. Gradient mellem to niveauer af modellen (h i km, M i enhed M):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. Konduit styrke og tykkelse: tykkelse d = top − base (for en jordkanal, base = jord); styrke som M mangel mellem base og top.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3. Earth radius ækvivalent faktor (P.452-18, eq. 5 og 6a), med ΔN = gennemsnitlig N fald i den første km, positiv, i N / km:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

Med ΔN = 40 har du omkring 4 / 3 k (standard atmosfære); med ΔN nærmer 157 k gardiner infinity, det vil sige, Jordens radio bliver flad: det er begyndelsen af kanalen.

4. Radiofjernhorisont for en høj antenne h meter med en radius svarende til\ _ e i km:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5. Sandsynligheden af kanal for breddegrad (P.452-18, eq. 2), procentdel af tid; φ = breddegrad af centrum af ruten, μ1 og μ4 tage hensyn til, hvor langt det er ad søvejen:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

ved 41 ° N, alle ad søvejen (μ1 = μ4 = 1), giver omkring 11% af tiden: er den klimatologiske base, nyttigt som en referenceværdi, hvorfra det forventede indeks adskiller sig.

6. Tab for troposcatter (P.452-18, eq. 45; f i MHz, d i km, θ vinkel af stien i mrad, N0 jorden reaktivitet i midten af ruten):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

Udtrykket − 0,15 · N0 viser, hvorfor våd luft også hjælper uden kanal: 50 ekstra N enheder er 7,5 dB mindre end tab. LIVETROPOVHF allerede bruger G3YGF version af scatter tab.

7. Tab i kanal (P.452-18, eq. 46-51): fast tab af kobling plus en dæmpning for vinkel milliradiant, som vokser med kubikroden af frekvensen (f i GHz, a\ _ e i km):

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. fra Bean og Dutton (1966) og Kerr (1951)

De to klassiske tekster giver tre ting, der er nødvendige straks til indekset: gradient tærskler allerede tarate på samme niveau af tryk GFS, hvad der er værd i dB en ændring af reaktivitet, og hvilke vejrsituationer skaber eller ødelægger kanalerne. * Radio Meteorologi *, NBS Monograph 92.

1. Profilklassificering for tryklag (Bean og Dutton, tab. 6.5, s. 248; − dN / dh i N / km). Designet til radiobølgelængder, det gælder som det er på modelniveauer:

| Vej | Aktier | Subfraktion | Ikke lamineret | Super- refraktion |
| 1000- 850 hPa | 0- 1,46 km | − dN / dh < 0 | fra 20 til 60 | over 100 |
| 850- 700 hPa | 1,46-3,01 km | − dN / dh < 0 | fra 20 til 50 | over 80 |
| 700- 600 hPa | 3,01- 4,20 km | − dN / dh < 0 | fra 15 til 40 | over 70 |
| 600-500 hPa | 4.20 - 5.57 km | − dN / dh < 0 | fra 10 til 30 | over 50 |

2. Hvor meget er værd i dB (Bean og Dutton, cap. 6, pp. 233-258), måler fra 70 til 180 MHz på ruter uden for horisonten:

- Ethvert lag (på jorden eller i højden) sammenlignet med den "glatte" profil øger signalet på 10- 25 dB til alle procent; den større gevinst kommer fra de høje vippelag, sandsynligvis på grund af fokuseringseffekt (løbet på 200 km).
- Reaktiviteten på jorden Ns og faldet i den første km ΔN er relateret til det månedlige median signal med koefficient mellem 0,4 og 0,95, median 0,7 (20 ruter fra 130 til 446 km, omkring 100 MHz).
- Årlig cyklus: 0,18 dB pr. enhed N; daglig cyklus fra 0,2 til 1,1 dB pr. enhed N, maksimalt på ruter på 175- 200 km og vinter.
- Den sammensatte parameter for Misme anslår feltvariationen til 0,5 dB / (N / km) · (ækvivalent gradient − 40) plus et termomfang; parameteren for turbulens D (arealet mellem den virkelige profil og den adiabatiske, 0-3 km) forbedres blot sammenlignet med den simple ΔN. Konklusion af forfatterne: ΔN forbliver den bedste parameter for enkelhed.

3. Referenceprofil og anomali (Bean og Dutton, § 3.8 og 3.9). Den eksponentielle atmosfære af reference CRPL giver den "normale" profil, der starter fra Ns; A-enheden måler hvor meget den virkelige profil er forskellig ved hver aktie. Det er den naturlige måde at omdanne en GFS profil til en anomali at sætte på kortet:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

Med Ns = 313 har du ΔN omkring − 42 N / km, det er den tilsvarende radius 4 / 3. − 157 N / km-ledningskriteriet er her også afledt af Snell-loven for en horisontal radius (eq. 4.23), næsten uafhængig af antennens højde.

4. Hvor og hvornår kredsløbene opstår (Bean og Dutton § 4.4; Kerr cap. 3):

| Process | Situationen | Virkning |
| Advarsel | varm og tør luft fra land, der flyder på koldere hav (typisk sommerærme efter dage med godt vejr); primært tæller fugtighedsunderskuddet | intens ledning på havet |
| Nokturlig bestråling | seene himmel, svag vind; lave skyer eller en masse fugtighed forhindre det | førte til jorden, der er født ved solnedgang, vokser om aftenen og forsvinder hurtigt efter solopgang, især inde |
| Subsidence | luft, der kommer ned fra anticyklonen, opvarmet og tør, over frisk og fugtig luft; hyppige oversvømmelser i bjergene og syd-østlige sektorer af anticyklon | super- brydningslag eller høj kanal; næsten altid gradient højere end standard i omvendt |
| Hav og land | kyst, cirkulation af et par hundrede meter | lokale rørledninger langs kysten |
| Mod kanalen | blanding: jorden opvarmet af solen mellem midt morgen og eftermiddag, vind til jorden over et par m / s på ru jord; Nat tåge | Sandsynligheden for kanal næsten ingenting |

5. To nye ingredienser at prøve. Den lodrette bevægelse: opstigende strømme i konvergenszonerne fjerner og sletter lagene, subsidence styrker signalet (Moler og Holden: i gennemsnit omkring 2,5 dB mere end med op ad luften, på havet i bånd L og X). Open- Meteo giver allerede vertikal hastighed på trykniveauer. Og trykket trend: på en sti på 140 km til 186 MHz signalet nedstrøms når trykket faldt til senderen og til midten af stien og vendte tilbage højt og uden undvigelse, når det gik op, med kun 2 (Bean og Dutton, fig. 6.9, p. 241).

6. Fra Kerr. Ud over formlen for den maksimale bølgelængde (base fysik sektion), Kerr giver en omtrentlig formel for M fra den eneste temperatur, damp og højde (§ 3.9, med de gamle konstanter 79 og 4800 derefter erstattet af 77,6 og 4810) og bemærker, at til en omvendt skæring af vinden kan generere bølger, der gynger i højden af kanalen, mulig årsag til dybe undslipper (§ 3.26). Italiensk kilde rapporteret af Bean og Dutton og for at søge: L. Bonavoglia (december 1958), "korrelation mellem meteorologiske fænomener og formering"...

## 8. Fra amatør radio præsentationer og tekniske rapporter (anden serie)

Syv dokumenter leveret af programmets forfatter. Tre tjener direkte til indekset: W3SZ for troposcatter "gulv", LA0BY for tilfælde Hepburn havde ikke forudsagt, Hepburn ( LU7MC) for vejret klassificering af kanalerne.

1. Troposcatter: signalet altid der. R. Rehr W3SZ, "Troposfærisk Scatter (Troposcatter) Formering for VHF, UHF, Mikrobølgeovn og frekvenser", Pack Rats Conference 2014; J. R. Duffey KK6MC, "The Troposcatter Formeringstilstand og hvordan man forbereder dækning på VHF / UHF Bands", CSVHF 2017.

- Nyttig fra omkring 100 til 700 km, fra 144 MHz til 10 GHz; tabet vokser på 30 log f med frekvensen.
- Frem for alt tæller det startvinklen mod horisonten: hver ekstra grad koster omkring 9-12 dB ( M1CNK, Collins metode).
- Om sommeren er signalet gennemsnitligt ca. 10 dB højere end om vinteren; midt om eftermiddagen omkring 5 dB lavere end om morgenen eller om aftenen. KK6MC viser en total variation på 13- 15 dB med tid, sæson og tid.
- Yeh metode, den enkleste til program (f i MHz, d i miles, θ vinkel spredning i grader, α geometrisk gennemsnit af åbningerne af de to bundter):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Sammenligning af Larsen (1968) på 15 reelle forbindelser: gennemsnitlig kvadratfejl 5,1 dB for Collins, 7,4 for Yeh, 8.1 for NBS og CCIR, 16,8 for Rider. Alle metoder forbedres, hvis N 'er holder sig stabile (Yeh falder til 5,4 dB): for spredning brydningen af dagen tæller lidt, så tropo indeks skal se på lagene, ikke kun Ns. Tabellen med 15 tilslutninger med målte tab (herunder Feldberg- Monte Baldo, 322 km ved 957 MHz, 223,1 dB; Cypris- Israel, 319 km ved 77,5 MHz, 163,3 dB) er en god prøvebænk til vores beregning.
- KK6MC giver det tab, at en typisk station formår at overstige 2 m og den tilsvarende afstand, fra 184 dB (FT- 817 og 3 elementer i SSB, ca. 130 km) til 215 dB (lange bunger i SSB, ca. 430 km) og 236 dB (kW og yagi koblet, ca. 640 km). Det er den måde at oversætte indeks dBs i tjent kilometer.
- Klar kode: W3SZ Yeh og Collins program (nitehawk.com / w3sz / YehCollins.zip) og Bob Atkins KA1GT lommeregner (bobatkins.com / radio / troposcater.html).

En anden kilde på spredningen: I. Roberts ZS6BTE, "Troposfærisk Scatter Formering", * Radio ZS *, december 1984, med note af 2013 ([qsl.net / zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). Hovedsagelig Nyttig geometri: vinkel af scatter θ = θ0 − θ1 − θ2, med θ0 = d / R, θ1 = (h1 − h\ _ ostacolo) / d1 + d1 / 2 R og det samme for den anden station, R = 8446 km (4 / 3 af Jordens radius). Det er det stykke, der bruges til at beregne vinklerne fra profilen af jorden i troposcatter "gulv". Andre data: hver vinkel i flere omkostninger omkring 10 dB; skift fra Ns 280 til 300 reducerer tabet på omkring 4 dB; spredningen volumen er mellem 300 og 2000 m for 150 km, mellem 600 og 3000 m for 300 km; tordenvejr i det spredte volumen hæve signalet på omkring 20 dB. det numeriske eksempel på artiklen vender ikke tilbage med de givne formler (termen af ledig plads på 900 MHz og 872 km er omkring 150 dB, ikke 125), så formlerne bør kontrolleres på W3SZ før brug af dem.

2. Typer af kanal (W. R. Hepburn, "Troposfærisk Formering Modes", dxinfocentre.com / formering / tr- modes.htm, spansk oversættelse af LU7MC). Udfyld tabellen over Bean og Dutton med nyttige tilfælde i Italien:

| Type | Hvornår | Note for indeks |
| Bestråling | rolige og fredelige nætter på jorden | følge jorden, forsvinder kort efter daggry |
| Højtryk (subside) | anticyclon | kan vare hele dagen; om natten ledning fra bestråling nedenfor kan blokere fjerne signaler, så dagtimerne kan gå bedre |
| Front | foran en varm front, bag en kold front, nord for en stationær front | kort; det dårlige vejr forstyrrer ham |
| Bjergnedstigning | föhn, bora: luft kommer ned, opvarmning og tørring | at forsøge for bora på Adriaterhavet og föhn syd for Alperne |
| Advarsel | varm og tør luft over kold og våd jord, f.eks. efter regn | førte til jorden |
| Valley | varm luft over kold luft stopper i dalen | kan blive i dag |
| Marino | varm og tør luft på koldere vand; ankommer på kysten med brisen | top i eftermiddag; forår og tidlig sommer de bedste sæsoner |

Fordampningskanaler på havet (0-30 m) tjener kun omkring 3 GHz og ikke ind i baglandet: for 144 og 432 MHz kan du ignorere. De høje bjerge er en barriere; ørkenerne er for tørre.

3. De tilfælde Hepburn ikke havde forudsagt (S. Heck LA0BY, "Extreme Tropo Propagation på 144 MHz og op", Nordic VUSHF 2019, fra JO59IX til 500 m højde):

- 22. november 2018: 70 cm op til UA6 (ca. 2600 km) i 6-8 timer. Hepburn kun angivet de baltiske stater og Polen. Kanalen blev set i temperaturkortene på 900 og 1500 m og sluttede lidt over 2000 m; den var højere på den norske side, og kun LA0BY på bakken trådte ind. På jorden tid ikke synes som en trope: kulde og tåge på toppen.
- 11. februar 2015: 2 m og 70 cm mod 9A og S5 (ca. 1500 km) gennem Alperne, også dette ikke forudset af Hepburn.
- 15. oktober 2011: 2 m med EA1 (2135 km) i høj kanal, på tre strækninger af havet og tre jord.
- Andre begivenheder: 6.-11. november 2003 øst (2050 km), 10. december 2004 mod Frankrig.
- Feltadresser: tåge i jævne, gratis toppe, lille vind, våd jord. Normal temperaturgradient − 6,5 ° C / km; i real case ( VK3KAQ) + 7 ° C på 20 m.

Forfatterens konklusion: Hepburn og F5LEN er ikke nok, de tjener profilerne i højden langs ruten. For os: Disse datoer er tilfælde af kontrol, der skal omberegnes med de historiske data for modellerne, og indekset på kanalen skal sammenligne kanalens kvote med stationernes kvote.

4. Italiensk erfaring (G. IV3GBO, "Typer af VHF formering SSB / CW", 2017): udføres mellem 450 og 3000 km, med lag mellem 450 og 3000 m; tåge forlænget som et spor; åbninger ved havet på Adriaterhavet mod Puglia, Albanien og Grækenland. Scatter fra 100 til 500 km.

5. Introduktioner (P. Wilton M1CNK, "En introduktion til VHF / UHF Formering", 2013): 70 cm er ofte bedre end 2 m; med 10 m pol ved 144 MHz startvinkel er omkring 3 °, med 20 m ca 1,5 °.

6. NASA rapport (E. Hirschmann, "Troposfæriske og Ionosfæriske effekter på radiofrekvens (VHF- SHF) Kommunikation", Goddard X- 731- 67- 89, 1967): det vedrører afstand fejl til satellitter, ikke DX. Det bringer tilbage den samme N formel og eksponentiel profil (f.eks i Florida: Ns 370, ΔN − 55, konstant 0.161 / km) og sender to NBS kilder til at søge: Bean og Thayer, "CRPL Exponential Reference Atmosphere", Monograph 4 (1959), og Bean, Horn og Ozanich, "Klimatiske Charts og Data of Radio Refractive Index for USA og verden", Monograph 22 (1960).

## 9. Fra VHF / UHF DX bog, kapitel 2

I. White G3SEK (redigeret af), * The VHF / UHF DX Book *, DIR Publishing 1995 (ISBN 0 9520468 0 6; digital replika 2017), kapitel 2 "VHF / UHF Formering", s. 2-13, 22- 22- 29 og 4-11. Det er den nærmeste tekst til radioamatøroplevelsen blandt de indsamlede; bekræfter de tærskler, der allerede anvendes, og tilføjer nye synoptiske regler.

Nummer

- Samme formel som N (77,6 P / T + 3,73 · 10 ^ 5 e / T2); eksempel: 1000 hPa, 7 ° C og = 10 hPa skader N = 324. I en velformet atmosfære, for eksempel efter tordenvejr, N er næsten lineær omkring 39 enheder / km i de første 233 km.
- Gradienter mellem 0 og - 79 N / km er normale, under − 79 er der super- refraktion, over 0 sub- refraktion. Bogen insisterer på, at den lange DX ikke kommer fra den simple superfraktion, som bør forblive lige på hele stien, men fra kanalerne.
- Mindste kanaltykkelse, lidt mere forsigtig end Kerr og Hepburn:

| Band | Mindste tykkelse |
| 50 MHz |  400 m  |
| 70 MHz |  300 m  |
| 144 MHz |  200 m  |
| 432 MHz |  100 m  |
| 1.3 GHz |  50 m  |

- Forholdet er ikke proportionalt med bølgelængden (omkring 65 λ ved 50 MHz, 220 λ ved 1,3 GHz), fordi kanterne af kanalen er "split". I Det Forenede Kongerige er rørledningerne hyppige på 144 MHz, men næsten aldrig på 70 MHz eller derunder, og ofte de 432 MHz går stærkt, når 144 er svage.
- I rørledningen falder signalet som 1 / d i stedet for 1 / d2: op til 1000 km kan dæmpningen svare til det frie rum.
- En mindste stationskapacitet på 2 m (100 W, 15 dBi yagi, 2 dB støj): 217 dB, dvs. ca. 400- 500 km i SSB under gennemsnitlige forhold; i CW ca. 20 dB i mere (p. 4-7). Enig med KK6MC tabellen.
- Scatter: Tabet stiger med omkring 15 dB ved tredobbelt frekvens og 10 dB for hver grad af scatter vinkel (Yeh formel; i den tekst, der udtrækkes konstant er ulæselig, så det skal tages fra versionen af W3SZ). Den "op" spredning fra konvektive celler dækker rutinemæssigt op til omkring 500 km, 1000 km med gode stationer. Efter kraftig regn og udvidede betingelser er knappe på alle bands.

Typer af kanal

- På jorden: på jorden formerer de sig dårligt, fordi jorden absorberer og spreder sig; på det rolige hav kommer langt (Scilly- Canarie, 2500 km, omkring en gang om året på 2 m). Fordampningskanalerne er ofte kun omkring 15 m høje og tjener mikrobølger.
- Høj: mellem 450 og 2000 m på grund af den længere DX på jorden; signalet "høj" over stationerne nedenfor. De "taber" kanaler giver mindre stærke signaler, men også tillader mellemdistancestationer.
- Kanalerne har præcise retninger og kan vare timer eller dage, i modsætning til sporadiske E.

Nye synoptiske regler for indeks

- Alder af anticyklon: luften falder til midten af et par meter i timen og det modsatte sænkes som det høje tryk udvikler; de større åbninger ankommer efter flere dages formering allerede god. En kortsigtet anticyklon er ikke nok.
- Højtryk kant: laget er kuppel og ved kanterne er lavere, så stationerne bliver bedre.
- Retning: Det er blevet bemærket, at de længere ruter kører langs isobares eller gennem dorsals (Flavell G3LTP, 1984), men der er fremragende forbindelser også i radial retning fra centrum. Eksempel af 29.-30. august 1987: fra England svingede åbningen 180 ° på to dage (Spanien, Frankrig, Italien, Østrig, Norge) efter skiftet af anticyklon.
- Imod: tyk isobare (stærk vind), stratocumuliform skyer på kanten af laget (begrænse den højeste frekvens).
- Fronter: en kold front skubber varm luft op og skaber et bakgear langs fronten; den bedste retning er langs fronten, mens passerer, men det varer lidt.
- Sæson: I Det Forenede Kongerige er anticykloner hyppigere i slutningen af sommeren og begyndelsen af efteråret.
- Visuelle spor: røg fanget i en mørk linje over horisonten; med højt tryk, layerkules med base under 1000 m indikerer en landkanal, base over 1000 m en høj kanal.

Reference citeret for søgning: R. G. Flavell G3LTP, "Studies of an Extensive Anticyclonic Formering Event"..., * Radio Communication *, Februar 1984, s. 128; J. Gannaway G3YGF, "Tropospheric Scatter Formering", * Radio Communication *, September 1981, p. 710; P. W. Sollom G3BGL, "En lille flutter W6ZGN

## 10. En verificeret amatørradiomodel: Biancomano og Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, "Practical Troposfæric Model for VHF / UHF Formeringsforudsigelse", * National Weather Digest * 22: 2, juni 1998, pp. 15-24, gratis i [PDF fra National Weather Association](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf).

- Data: radiobølge på 00 og 12 UTC på 47 stationer i det østlige USA, 1995-1997; jordanalyse til 700 hPa (i begyndelsen op til 400), på N-profilen, uden M. Ekskl. kyster, øer og bjerge.
- For hver undersøgelse: kanalens tilstedeværelse og andel, den maksimale bølgelængde, der kan føre (ofte omkring 100 gange bølgelængden), den maksimale gradient af den potentielle temperatur mellem naboniveauer og mellem et niveau og det andet ovenfor (for tykkere lag). Jordkanalerne anvendes ikke.
- Første regel, persistens: stabilitetsindeks (løftet indeks, K, totaler, SWEAT) forbliver inden for 3 enheder fra undersøgelsen 12 timer før, og trykniveauet, hvor den potentielle temperatur vokser mere forbliver omkring det samme. I bekræftende fald: forventet forbedring inden for de næste 12 timer.
- Anden regel, dynamisk styring: grænse lag, der ikke stiger, CAPE, variation af tykkelsen til 500 hPa på tre undersøgelser, variation af hældningen inden for grænser.
- Band tærskel: en ledning i stand til at bære 100 cm eller mere indikerer åbning på 432 MHz, næsten sikker på 222 og ofte endda på 144.
- Rumfilter: stationer på et sekskantet net på omkring 400- 500 km; prognosen er kun værd, hvis mindst 5 contigue stationer er positive (hvis du kan springe en). Med 5 stationer bliver sandsynligheden for en falsk tilfældig positiv ubetydelig.
- Kontrol af begivenhederne i kolonnen "Verden over 50 MHz" i QST: i 1996 var der planlagt 48 begivenheder på 67 (71%); 12 af de 15 større begivenheder. Nøjagtighed op til 70% på 432 MHz og ca. 60% på 144 MHz ved 12 timer; det giver næsten dobbelt de faktiske begivenheder; 9 fejl ud af 19 er forudsigelser ankom sent, fordi mange forbedringer form i mindre end 12 timer.
- Tilføjet til stationerne længere nordpå: vind roterende med uret klatring (hot start) som ekstra tilstand.

Fra den komplette PDF (leveret af forfatteren af programmet) tre flere detaljer. Deres regel "tykkelse = 100 gange bølgelængde" giver 600 m for 6 m, 200 m for 2 m, 125 m for 1,25 m og 70 m for 70 cm (fig. 3): for de 70 cm er mindre alvorlige end Kerr og Hepburn (ca. 89 m), for de 2 m falder det sammen med VHF / UHF DX Book. Den vind, der roterer med uret går op, ifølge forfatterne, især fordi det bringer varmere luft i højden, hvilket er, hvad der gør faldet af N vokse med pres; den fugt, det bringer med det synes ikke destabilisere. Tillægget opnår dN / dh i lukket form til en konstant termisk gradient atmosfære med et skarpt spring af varme gradient og fugtighed ved en given kvote: nyttigt som en test case for vores kode. Deres spor af hældningen af den potentielle temperatur kommer fra K. H. Jehn "Anvendelse af et potentielt brydningsindeks i synteseskalaspektrometri", * J. Meteorologi * 17, 1960, s. 264.

Hvad skal du tage for vores indeks: (1) en term af persistens: sammenligne profilen med den 12 timer før, at i timeprognosen vi har fri; (2) rummet filter: et punkt tæller kun, hvis forbedringen dækker et område på et par hundrede km, hvilket reducerer falske alarmer; (3) stabilitetsindeks, beregnet fra temperatur og dug til 850, 700 og 500 hPa (Open- Meteo allerede giver løftet indeks og CAPE); (4) vinden, der roterer med højden mellem 1000 og 850 hPa; (5) deres metode til verifikation, som er den samme som vi ønsker at gøre med programmet annoncer.

En anden kilde i samme pakke, til ringe nytte for os: P. K. Rastogi, "Stratosfære og Troposfære (S- T) Studies at Millstone Hill", NASA 1984 (dokument 19840019069): 440 MHz radar, der måler turbulensen af klar luft mellem 4 og 25 km, med tynde lag forbundet med tyngdebølger og vindskæring. Det bekræfter, at turbulensen, der producerer spredningen er lagdelt, men det vedrører for høje odds for kanalerne.

## 11. Tredje runde af kilder (20. november)

Høj kanal historiske sager. J. Macoun OK1VR, 2 m fra topmødet i Sněžka (1602 m), efteråret 1958, artikel 1959 oversat i 2006 ([ok2kkw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)). Den 27.-28. oktober 1958 første Tjekkoslovakiet-England forbindelse på 145 MHz ( G5YV, 1220 km) og mange flere mellem 1100 og 1250 km, med stabile signaler efter 23. Forfatteren tilskriver det to overlagte inversioner, den øvre omkring 2200 m, med signalet tilbage til jorden kun hvor det nederste lag opløst; tilstand begunstiget af en langsom anticyklisk fra Atlanterhavet til kontinentet. Fra hjemmet (Prag) sit maksimum var 530 km: kun stationer ved højden ind i kanalen. Det er et andet tilfælde af verifikation (såsom LA0BY 2018) og bekræfter, at indekset bør kigge efter over 2000 m og sammenligne det med stationernes andel.

Historiske biblioteker NBS (nyttigt at lede efter klassiske værker, ikke for formler):

- W. Nupen, * Bibliografi om Troposfærisk Formering af Radio Waves *, NBS Technical Note 304, 1965, 356 sider, med indeks for emner (også "tilstand prognose") og efter land. Italienske indgange rapporterede: L. Sacco, "Radioforbindelsen i atmosfærisk superrefraktion regime", * High Frequency * 24 (6), December 1955 (italiensk forskning: superrefraktion under 500 m hyppig dag og nat); S. Silleni, * Annali di Geofisica * 8 (1), 1955; J. M. Clara e A. Antinori, prova tra Sardegna e Minorca da 238 a 297 MHz (390 km), * Electrical Communication * 33 (2), 1956. A. F. Clarke (Fort Huachuca, 1956) udstedte dagligt prognosen om den maksimale bølgelængde fanget for den næste morgen, fra fugt, temperatur, tryk og vind prognoser.
- R. L. Abbott, * Bibliografi af troposfæriske Radio Wave Scatter *, NBS, materiale indtil januar 1960, omkring 670 genstande bestilt om året: nyttigt for troposcatter del.

Oplysning og bemærkninger: F. W. Odenthal DK5EQ, "Ausbreitung 2m - Eine Einführung" (31 / 05 / 2024): næsten optisk op til 200 km, tropo per kanal eller refraktion ud, med højt tryk og i foråret og efteråret; citerer VHF side af DM2HB og noter af DK5YA på tropo og Es. A. Johnson VK3DXE på bloggen af EI7GL (december 2021): med WSPR VHF stier tusindvis af km i Australien er blevet opdaget; Trope er ikke kun sommer. Diskussioner om VHF marina og AIS ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)): omfang 1,23 · (ft) sømil per antenne, spredt op til omkring 50 nm, typisk trope forbedring 100- 400 nm; rørledninger er anerkendt, fordi de fjerne stationer kommer så stærk som naboerne. Chelmsford ARS, december 1983: i 2 m konkurrencen stationerne på den østlige kyst af England er begunstiget af søruten til kontinentet.

ARRL, * Radio Amateur VHF Manual *, 1972, cap. 2 "Formering ud over Horizon" (s. 17-19). Allerede kendte driftstips (følg højt tryk områder, der bevæger sig langsomt og frem for alt deres bagkanter; tidligt efterår den bedste periode; kystområder, store søer og dale i floderne begunstiget ved stratificering). To nye data: (1) fig. 23- sammenligner standardatmosfæren (relativ fugtighed 70% op til 12000 ft, mindste refraktion) med en typisk radius af stærk refraktion, med et klart temperaturbreak og mix forhold på ca. 4000 ft (ca. 1200 m): det er den signatur, som vores beregning skal genkende på GFS-niveauet; (2) sande kanaler er sjældne under 144 MHz og hyppigst i UHF, i henhold til de mindste tykkelser. Nyttig QST bibliografi til søgning: Hull, "Air- Mass Conditions and the Bending of UHF Waves" (juni 1935, maj 1937); Hoisington, "Painless Prodiction of Two- Meter Openings" (oktober 1949); Collier, "Upper- Air Conditions for 2- Meter DX" (september 1955); Botts, "A Night to Hukommelse" (januar 1970, gennemført på land).

PA0EHG, side "Tropo" ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): resumé af troposcatter fra G3YGF artiklen, allerede i manualen. Brug to praktiske regler til at kontrollere vores scatter beregning: omkring 10 dB ekstra tab for hver vinkel i horisonten af hver station, og over 100 km ca. 9 dB plus hver 100 km rute (60 miles); det fælles volumen starter fra omkring 600 m på 100 km og fra omkring 9 km på 480 km. Det behandler ikke kanaler, meningsmålinger eller brydningstærskler.

PA0EHG, PROPBER lommeregner ([side](https://pa0ehg.com/propagat1.htm), propber.exe program leveret af forfatteren til programmet). Det er et 36 kB DOS program skrevet i BASIC og kompileret med Microsoft BASIC Compiler 5.60 (1985): det kører kun under DOS eller DOSBox. Fra de interne tekster: beder om frekvens, afstand (eller de to lokalisatorer, også i det gamle QRA-format "E-locator"), for hver antenne højdestation, støjtal, FI-bånd, effekt, en hindring (højde og afstand) og antenne gevinst (0 = lignelig diameter beregning, effektivitet 50%). Returnerer den maksimale afstand i optik, det korrekte optiske tab for absorption af damp (0,1 dB / km fra 24 GHz), tab af troposcatter "korrekte for de steder" (dvs. for horisonten vinkler givet af forhindringer) og S / N-forholdet i de to sanser, også "med den maksimale unormale formering". Numeriske konstanter kan ikke læses i den kompilerede eksekverbar. Den foreslåede metode for høje bånd er også nyttig for os: kalibrering af beregningen på et målt link (forskel mellem målt og beregnet S / N) og anvendelse af samme forskel på et andet bånd. Det er det samme mønster i vores beregning, men det bruger S RTM1 profil i stedet for blot én hindring og GFS prognose i stedet for en fast margin.

PROPBER, konstanter læst i eksekverbare (numre i Microsoft Binary Format format). πre, i datatabellen ved siden af teksterne: 8,68589 (= 20 / ln 10, dvs. 20 logs beregnet med naturlig logaritme), 32.5 (ledig plads: 32.5 + 20 log f\ [MHz\] + 20 log d\ [km\]), 24000 og 0,1 plus 40000 og 0,3 (vī: 0,1 dB / km fra 24 GHz, 0,3 dB / km fra 40 GHz) Sandsynligvis, fordi du gentager, men inde i koden: − 69,5 (samme konstante sigt som Yehs side- scatter formel i VHF / UHF DX Book), 332 (jorden refraktiv), 8733 km (tilsvarende radius, k ≠ 1.37). Den nøjagtige scatter formel er ikke rekonstruerbar uden demontering af programmet. T0

PROPBER, formler rekonstrueret af koden (adskillelse af den kompilerede del; operationerne kaldes til BASIC biblioteket med operatørerne i hukommelsen, hvis værdier i dobbelt præcision er blevet afkodet). Konstanterne − 69,5, 332 og 8733 fundet tidligere var falske positiver i biblioteket.

- Optisk afstand: d = 5 · (‡ h1 + ‡ h2) km (h i m). Det er mere generøst end den klassiske 4,12 (k = 4 / 3): svarer til k ≠ 2.
- Tab i optik: L = 32,5 + 20 log f\ [MHz\] + 20 log d\ [km\] plus 0,1 dB / km over 24 GHz og 0,3 dB / km over 40 GHz.
- Troposcatter: Lts = L + 23 + 10 log f + 0,0674 · d, dvs. 55,5 + 30 log f + 20 log d + 0,0674 · d (formel G3YGF med konstant 2 dB højere). Ingen vinkeludtryk eller reaktivitet.
- Korrektion "for de steder": programmet spørger hindring og afstand, men den blok, der skal bruge dem er tom; korrektion variabel forbliver 0, så den "korrekte" tab er lig med spredningen.
- Støj: T = 290 · (10 ^ (NF / 10) − 1), N = − 228,6 + 10 log T + 10 log B (dBW), uden antennetemperatur.
- S / N = 10 logs P + G\ _ tx − L + G\ _ rx − N. "Med maksimal unormal formering" bruger optisk tab i stedet for spredning, dvs. den ideelle kanal tilfælde uden tab ud over ledig plads.

Sammenligning: 300 km og 144 MHz PROPBER giver omkring 189,9 dB, G3YGF 187,9, vores P.452 187,8 (med nul horisont vinkler): spredningen falder inden for 2 dB. Vores beregning tilføjer horisonten vinkler fra profilen, antennen temperatur, og i stedet for "maximum" fast det forventede lag med resterende parring og dæmpning.

Rohde & Schwarz, P. Denisowski, "Forståelse VHF Formering", Undervisningsnotat, april 2023 (24 sider, formidling). Bekræfter de punkter, der allerede er indsamlet (udført som en todimensionel waveguide, signal, der falder lineært med afstanden, op til 1500 km og videre, varighed fra timer til dage; god jord på havet og knappe på land; forhøjet fra et par hundrede meter til et par km). To nyttige observationer for indekset: (1) udvikling i tid: en nydannet kanal ofte kun bærer de højeste frekvenser og, fortykning, begynder at bringe selv den laveste; timeløn prognose bør derfor vise 432 MHz "på forhånd" på 144 MHz, når laget vokser; (2) stationer placering: de bedste betingelser er mellem stationer tæt på enderne af kanalen, mens dem langs stien af en høj kanal ofte undlader at komme ind, medmindre udføres "der taber". Kanalerne bevæger sig med atmosfærisk vejr.

Site PA0EHG, andre sider. For tropen, PathProfile of M. Willis G0MJW ([Scheda](https://pa0ehg.com/srtmp.htm), [download](http://www.mike-willis.com/Software/ASTERPathprofile.zip)): profil af ruten fra forventede samlede tab og kvote filer, pålideligt bedømt for troposcatter i mikroovn; behandler ikke spredningen fra fly eller regn. Nyttig som en anden reference til at sammenligne vores S RTM1 profiler og tab. Resten af webstedet dækker EME, mikroovn, landingsbane (AirScout, SM7LCB kort) og foranstaltninger i 80 m.

PathProfile af G0MJW (Mike Willis), ASTER og SRTM pakker og SiteData (leveres af forfatteren af programmet; [](https://www.mike-willis.com/software.html)software side). Windows-program 2018: profil fra ASTER eller SRTM til 1 "eller 3", tab i optik, diffraktion, troposcatter (gyldig op til 1000 km), kanal og gasformige, med procent af tid, median af ruten, brøkdel af stien på havet og afstand fra kysten: strukturen er, at P.452 / P.1812, men hjælp (komprimeret.chm fil) kunne ikke læses her. SiteData konverterer locators og giver andelen fra GT OPO30.

Den mest nyttige del er datafiler inkluderet: ITU-R P.453 statistiske kort på globalt gitter på 1,5 ° (121 rækker fra 90 ° N til 90 ° S, 241 kolonner fra 0 ° til øst): gradient dN / dz i de første 65 m overskredet for 1, 10, 50, 90 og 99% af tiden; for jordkanaler procentdel af tilstedeværelse, tykkelse og intensitet; for dem høj tilstedeværelse, basisandel, tykkelse, intensitet og maksimal højde; plus vådsigt og temperatur. Interpolerede årlige værdier:

| Sted | Landledning:% / tykkelse / intensitet | Høj rørledning:% / base / tykkelse / intensitet | dN / dz oversteg 10% af tiden |
| Napoli |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | − 380 N / km |
| Tyrrhenian (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | − 674 N / km |
| Trapani |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | − 520 N / km |
| Brindisi |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | − 405 N / km |
| Adriaterhavet (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | − 399 N / km |
| Milano |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | − 172 N / km |
| Udine |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | − 176 N / km |

For vores indeks er en klar klimatisk reference: tykkelse og intensitet typisk for kanalerne i stedet for de 50 m fast af den potentielle kanal, og en anomali "i dag sammenlignet med normal" per zone. ITU-data: gratis til ikke-kommerciel brug, at kontrollere, før de medtages i en offentlig version. ZO NES01.DEM filen (58 MB, 2 'gitter) indeholder klimazonerne indland / kyst / hav; formatet er ikke dokumenteret, og jeg ikke afkode det. Willis ligninger side indeholder kun fysiske konstanter, ledig plads og antenne gevinst.

Fjerde runde af kilder (20. november).

Indermuehle og Suzuki 2026, forudsigelse af GFS-rørledninger til CSIRO observatoriet i Murchison (arxiv 2608.12861): det er den nærmeste kilde til LIVETROPOVHF. Profiler af N fra GFS til 0,25 ° (overflade til 2 m, derefter de isobariske niveauer, dem under den kasserede jord); for hver mindste kolonne af dM / dz under 3 km, base og top af kanalen, integreret M underskud; derefter P.452- 16 og P.1812-6 med GLO- 90 25 længder fra 98 til 1005 km, fra 88 MHz til 2,68 GHz, hver time. Resultater: Median AUC 0,72 på 25 kanaler; P.452 p tid procent bør anvendes som en kalibreringskonstant (valg p = 20%), fordi ITU model med en øjeblikkelig ΔN er ikke defineret inden for en kanal; færdighed forbliver næsten flad indtil 3 dage i forvejen. Ved 162 MHz (AIS) har de modtagne ruter den højeste kanalfraktion på 67% (0,26 versus 0,16) og den højeste kanaldybde på 49% (28 vs 19 M) med hensyn til kontrol: på havet er ledningen næsten altid, bestemmer strækket af jord. GFS ser halvdelen af tiden på jorden sammenlignet med ERA5 (4,5% mod 10,4% af cellernes timer), mens på havet ser mere (73% mod 56%). Et "korridorindeks" (kontinuerlige rørledninger langs ruten, lag med en tykkelse på mindst 40 m) forbedrer resultatet: bekræfter, at kontinuitet langs ruten er det rigtige kriterium.

Longley and Rice, ESSA ERL 79- ITS 67 (1968): metode til uregelmæssig terræn fra 20 MHz til 20 GHz (efter ITM), med jorduregelmæssigheder Δh, Ns fra 250 til 400, tilsvarende radius 8497 km (4 / 3), horisonter, diffraktion, spredning fremad og variation i tid og steder for klima. Statistisk reference, ikke øjeblikkelig.

ITU-R P.834-2: radius krumning, Jordens radius ækvivalent k · a, M = N + h / a (h i m, i tusindvis af km), eksponentiel model; intet nyt end hvad der allerede anvendes.

AGARD CP- 70 (1970), troposferisk formering: tynde lag fra 1 til 100 m med stærke gradienter er almindelige; reflekskoefficienten for et højt lag er omtrent proportional med bølgelængden, med en tilfældig spredning på omkring 14 dB. Foranstaltninger på 90 MHz: et fald på 9-10 dB bundet til et N spring koncentreret på ca. 350 m, øverst på et lag af skyer. Passage af en kold front eliminerer lagene: Spændingen er tilbage.

US URSI Report, Underudvalg 2 (1960): gennemsnitlig tykkelse af de høje lag fra 60 til 210 m (givet i overensstemmelse med P.453), minimum gradient for 157-160 N / km.

Buscemi K2OVS, VHF formering guide: typiske afstande for vending og kanal for band, kanaler forbundet med høje stationære tryk, maksimalt ved daggry.

Wolinsky- Mancini og 2024, AIS på De Kanariske Øer: AIS radiosunctions og receptioner; kanalerne er meget hyppigere i de varme måneder (inversion af alisaerne).

Nigerianske og malaysiske artikler (Ekah, Iloke, Mat, Igwe): lokale korrelationer mellem temperatur, fugtighed og målt tab, svage og modstridende (r - 0,09 til 0,27); kun som bekræftelse på, at jordstørrelserne alene ikke er nok.

Herben 1984, afhandling TU Eindhoven (microonde): 30 GHz på 8,2 km og OTS satellit på 11,6 GHz; gnister for turbulens, multi-sti og regn. For mikrobølger er dynamiske tab domineret af regn og turbulens, ikke af kanaler.

Baars, The Paraboloidal Reflektor Antenna (microonde): gevinst af en lignelse G = η · (πD / λ) 2 med typisk effektivitet 0.5- 0,65; tab af overfladefejl i henhold til Ruze, ηs = exp (− (4πε / λ) 2). Det ville hjælpe, hvis vi tilføjer lignelserne som et alternativ til Yagi DL6WU for 1296 MHz og videre.

Allerede analyseret eller af dårligt indhold: Hirschmann NASA X-731-67-89 (dobbelt), Reid 's VHF ST radar (vind og turbulens, uudført), Scribd dias og generiske noter på troposfæren.

Femte runde af kilder (20. november, aften).

Rogers 1997, Radio Science 32 (1): Bayeser skøn over parametrene for en høj kanal (base rate, M underskud, tykkelse) fra formationsfaktorer målt ud over horisonten, med en parabolsk ligning model; VOCAR eksperiment i Californien på 143, 263 og 375 MHz, radiobølge sammenligning. Signalet afhænger meget af basen og M-underskuddet (der korrigerer 0,81-143 MHz for underskuddet), næsten alt efter tykkelsen (− 0,17): tykkelsen kan ikke opnås fra signalet. For os: beregning B (klimatykkelse) tæller mindre end underskud og andel; og metoden er grundlaget for back office, det vil sige at få kanalen fra de mange frekvenser.

Moler og Holden 1960, "Troposfærisk spredning og atmosfæriske cirkulationer", J. Res. NBS 64D, s. 81 ff. (læses i original den 22 / 11 / 2026, doi 10.6028 / jres.064d.014). Californiens søruter til Point Loma (San Diego): Santa Barbara 190 miles, Point Mugu 144, San Clemente 78; 1365 MHz og 9380 MHz, ikke VHF; Januar-April 1958, gennemsnit 1 minut. Sagen den 26. marts: en forstærkning på ca. 20 dB i ca. 3 timer mod kl. 12.00, hvor en cyklon vortex svækker og begynder havbrise og subsidence; ved 15 reformeres konvergensen, og signalet falder omkring 22 dB fra maksimum. Statistikker (tabel 1, 500 hPa, 33 tilfælde): med luft, der falder det gennemsnitlige signal er højere end omkring 2,5 dB end med luft stigende, i bånd L og X. Signalet følger forskellen beregnet til 500 hPa i variationernes tegn og amplitude uden korrelationskoefficient. Lave lag (under ca. 600 m) fremmer delvis refleksion på spredning. Anticykloner og fronter behandles ikke, og ovennævnte "udflugter op til 60 dB" er ikke: de kom fra et andet resumé. For os: Den lodrette bevægelse er en god forudsigelse, og + 1,5 dB for subsidence og − 2 dB for den polstrede luft er af samme størrelsesorden som den gennemsnitlige målte forskel (2,5 dB); 20 dBs er et enkelt tilfælde, ikke en typisk værdi, og på mikrobølgeovn.

Banafaa og Muqaibel 2025, IEEE Access: undersøgelse af rørledninger (fordampningsmodeller, paraboliske ligning, radarestimater, GNSS og lidar) og klassificering med automatisk læring om reelle foranstaltninger; Support vektor maskiner giver de bedste resultater. Nyttig som bibliografi; dataene er fra Den Persiske Golf.

ITU-R P.834- 7 (2015): opdateret version af henstillingen om troposvildtlevende refraktion; ingen ny formel end dem, der allerede anvendes.

Abbott, NBS bibliografi om troposfæren (GOVPUB - C13): allerede i tredje runde. Slide Scribd på kanalerne: generisk indhold. Artiklen ScienceDirect 0021916987900304 kunne ikke åbnes.

Sjette runde (20. november, nat): Bean og Dutton, ARRL Handbook 2022 og 2023, Balanis.

Bean og Dutton, Radio Meteorologi (cap. 6), dele endnu ikke brugt. Feltfølsomhed over for Ns: 0,18 dB pr. enhed N på den årlige cyklus; 0,14- 0,24 dB / N i dagcyklussen (maksimalt tidligt om morgenen), op til 0,2- 1,1 dB / N på ruter på 175- 200 km om vinteren; korrektioner 0,8- 0,95. Parameter bestående af Misme: M = a (g\ _ e − 40 + b · ΔW ^ n) med en = 0,5 dB pr N / km, som kombinerer den ækvivalente gradient og den termiske stabilitet ΔW (arbejde for at løfte luften på 1 km): i Sahara ved 471 MHz på 160 km den eneste stabilitet er 20 dB mellem januar og juni ved næsten lige gradient. potentielle refraktionsindeks K (N rapporteret adiabatisk til 1000 hPa): fremhæver luftmasserne og den anticykliske subside (Flavell og Lane).

ARRL Handbook 2022 (99a ed.) 19.4.3-19.4.4. Nyttige inversioner mellem 250 og 2000 m; inversioner af stråling (sera, rolig luft, tør jord) fører sjældent sandt; langsomme anticykloner giver de mest omfattende kanaler til subsidence, med inversioner mellem 500 og 3000 m, stærkere om aftenen og om morgenen; i den nordlige halvkugle de bedste ruter er syd for det høje centrum. Hot front: gode forhold et par hundrede km foran; Cold front: stærk, men ustabil baglæns bag fronten, parallel. Foehn (også i Alperne) og jordbrisen (kold luft på jorden, varm på 200- 300 m; strækker sig over 800 km langs kysten). Scirocco: varm og tør luft fra Sahara over køligere marin luft, meget stærke inversioner fra det østlige Middelhav til Gibraltar, sandsynligvis årsag til poster over 1500 km i mikroovn. Troposcatter: teoretisk grænse omkring 800 km, maksimal typisk halvdel. 100th ed. (2023) har en kortere tekst: inversioner ved solopgang og solnedgang op til + 20 dB og + 20-50% flow hastighed; 1200 miles på De Store Søer, typisk 400- 600; anbefaler Radio Mobile (ITM model) for Hepburn dækning og kort.

Balanis, Antenna Theory (4a ed.), § 10.3.3: Viezbicke tabel (NBS TN 688, 1976) for Yagi optimeret på 400 MHz: boom 0,4 / 0,8 / 1.2 / 2.2 / 3.2 / 4.2 λ → 7,1 / 9,2 / 10,2 / 12,25 / 13,4 / 14,2 dBd. Vores DL6WU formel (8,6 + 8 log10 boom / λ) giver 1-1,7 dB mindre mellem 0,8 og 4,2 λ og vender tilbage i linje med DL6WU tabeller til 9,5 λ: forsigtig skøn. Med den korrekte formel på originalen (9,4 + 7,8 log10 boom / λ fra 22. november) falder kassationen under 0,2 dB mellem 1,2 og 4,2 λ (12,1 dBd til 2,2 λ versus 12,25) og forbliver 0,6 dB til 0,8 λ.

Syvende runde (21. november): QST, QEX, W3SZ, GitHub repository.

QST 2000- 2025 (14 numre, 10 senge med tekstgenkendelse). Kun rapporter i afsnittet The World Over 50 MHz, nyttige som tilfælde for at validere modellen: September 5-9, 2000, udført på 144 MHz og ud på den østlige halvdel af USA; 14- 15 August 2022, åbning Manitoba-Kansas / Missouri under højt tryk på en varm dag, fra 144 til 1296 MHz næsten alt i FT8, canadiske rekord på 10 GHz på 1228.56 km ( VE4MA - N0OY) i Q65, signaler op til 1941 km; APRS formering kort viste det samme område. QST November 2022 anbefaler Hepburn kort. Ingen teknisk artikel om tropo prognose.

QEX og K1JT, Franke og Taylor (QEX 2020) på FT4/FT8 -modus: FT8 -afkodningstærskler − 20,8 dB i 2500 Hz på kanal uden evanescens, − 18,6 dB med evanescens på mellembreddegrader; FT4 − 17,5 dB. De andre numre af QEX 2023 og 2026 behandler ikke tropo (tre numre 2023 er ulæste scanninger). W3SZ 2019: slow mode tærskler ( JT4A − 23, JT9A − 27, JT65A − 25, QRA64A − 26, FT8 − 21 dB) og valgkriterier mellem følsomhed og frekvenstolerance.

GitHub. TropoLink (LukiBox, Apache- 2.0): design af troposcatter ruter med P.617- 5 (med N0 og ΔN), NBS TN101 og ITM flanked, og deres forskel vist som usikkerhed; P.617- 5: L = F + 22 log f + 35 log θ + 17 log d + Lc, F = 0.18 · N0 · e ^ (− hs / 7,35) − 0.23 · ΔN, Lc = 0.07 · e ^ (0,055 (Gt + Gr)). andi- f / troposcatter ( DL2FZN 2013, fra VHF / UHF Handbook RSGB): Lts = 24,44 + 4,283 ln f + 0,06656 d over ledig plads, hældning nær PROPBER (0,0674 dB / km). QRadioPredict: ITM og ITWOM 3.0 med S RTM3, jord brug Coriner og NEC diagrammer. Ikke relevant: Tropos (Weather app), opera- adt / tropo og troposim (Troposferic delay for InSAR), tgbot\ _ troposcatter (militær station), sdr- troposcatter-gui (kontrolpanel), Troposcatter-Propagation- Delay ( ERA5 delay), Radio- Communication-Project (didaktisk simulator), PyLayers (kanal).

Ottende omgang (21 November): NTIA og ITU modeller på GitHub. NTIA / itm (Longley- Rice, ITS): 20 MHz- 20 GHz, frit rumtab, diffraktion og troposcatter med jordprofil, 0.5-3000 m terminaler, overfladereaktivitet N0 (minimalt reduceret til havniveau, 250- 400), syv klimaer, jordkonstanter og forskellige lokaliseringstilstande 1 visning, 2 diffraktion, 3 troposcatter; C + + med.NET Framework 4.7.2 kabinet. Det modellerer ikke kanalerne: det tjener som reference "normal atmosfære" med statistisk variation. NTIA / p528 og eeveetza / p528 (MATLAB): ITU-R P.528-5 for luftjordforbindelser, 100 MHz- 30 GHz, 1,5-20 000 m terminaler, p 1-99%;.NET 4.8 indkapsling. Mulig brug: luft scatter balance (to jordruter - fly plus radarsektion). eeveetza / p2001 (MATLAB): ITU-R P.2001-6, all- field model med diffraktion, troposcter, unormal formering (adfærd og lag, Lba) og E sporadisk, procentdel af tiden 0- 100%; ITU digitale kort (DN\ _ Median, DN\ _ SupSlope, DN\ _ SubSlope, dndz\ _ 01, TropoClim, h0, surfwv, Es) er ikke omdistribuerbare. Mulig brug som en ITU sammenligning i stedet for P.452, med GFS ΔN i stedet for kort som i CSIRO-undersøgelsen; kortene bør downloades af brugeren som pr. P.453. Validering 1.3.0 ud af 593 QSO (29 / 12 / 202-2 / 1 / 2022): begivenhed / sammenligning adskillelse 0,79 uden rettelser, 0,91 med Misme og subsidence; GFS ikke se kanalen på havet på 144 MHz (guadagno uden rettelser aldrig mere end 15 dB).

Niende runde (21. november): IU3QEZ, NTIA, PyWaveProp, ACE High, validering 1.3.1. IU3QEZ (S. Fabris), præsentation "Over horisonten i VHF" (GEKO Magazine n. 69, september 2026): fælles volumen d2 / (8 · R\ _ eff) med R\ _ eff 8500 km (1,3 km d 300 km, 3,7 km til 500 km hvert horisontniveau til 69 miles (111 km), margin på 7 dB for evakuering og 4 dB for refleksion fra jorden; afhandling: 10 dB er omkring 100 km; station IQ3QC 5 W med 3 × 5 elementer, 120 QSO, maksimalt 788 km (Alpe Adria 2026). Bibliografi af præsentationen, der skal inddrives: Norton 1959 (J. Res. NBS 63D, System Loss), Bray 1961, ARRL Antenna Book 2023 fig. 4.29, Rice- Longley- Norton- Barsis NBS TN 101, Bean and Thayer 1959, Barsis og Kirby 1961 (diffraktion på ridge), Deygout 1966, P.1812, AirScout DL2ALF, P.834 NTIA Proplib (wiki): eHata, ITM, LFMF, P.528, P.2108 (tab fra bulk), med C + + +,.NET, MATLAB og Python; conduration it; jord (også (1). PyWaveProp (Python): Paraboliske ligningsmetoder med Padé tilnærmelser, multiple kant diffraktion, PETOOL og RAM indkapslinger, troposole- og høje kanaler, uregelmæssig jord: mulig base for en feltberegning med GFS M (z) profil, der skal kontrolleres. APRS PropView (Apache 2.0): Name NATO troposcatter net i Italien (fortifications.net): 900 MHz, 10 kW, 20 m Parables; stationer, herunder Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Vulture, Monte Lauro; historiske test sag for P.617. Ikke relevant: Gupta 2021 (stratosfærisk transport), lov 2023 (arktisk ozon), delfin # 590 og opera- adt / tropo (InSAR forsinkelse), tropo.ino (Arduino gas sensorer), tropo- webapi (telefoni), cloudtools / troposfære (AWS). Validering 1.3.1 på 564 QSO tropo: AUC 0,79 (model 1.2), 0,80 (+ blød kanal), 0,90 (+ misme og subsidence), 0,90 (+ persistens); ledningsfarver (≥ 16 dB) på 46% af åbningsvejene og 0% af sammenligningen.

Tilføjet til samme runde. mikelytaev / wave- formering (MIT, Python 3): Parabolic ligning med tilnærmelsesvis af Padé og Numerov, uregelmæssig jord, troposfæriske kanaler; oplade den modificerede refraktiv M (kvote, lat, lon) fra NOAA GFS og indstille en fordampning kanal Monin-Obukhov; ingen dokumenteret funktion, der returnerer tabet på stien. Han er den bedste kandidat til en fremtidig felt beregning med GFS M profil (det ville tjene en ekstern tjeneste eller en oversættelse). RP- Sim (AGPL- 3, 0, JavaScript): Empiriske modeller for radionetværk (Hata, Han, Ericsson, to stråler), ingen tropo. RIMFading (GPL- 3.0, OMNeT + +): strømuregelmæssigheder for netværkssimuleringer, Weibull-distribution, ingen tropo. PyLayers: indendørs radio kanal, allerede udelukket. qradiopredict (kantooon, GPL v2): ITM og ITWOM v3.0 med S RTM3 og Corine, allerede i syvende runde som QRadioPredict.

Tiende runde (21. november): Antenna Book, Norton, Barsis, nye datakilder. ARRL Antenna Book (23a ed.), cap. 4 ( K9LA): Bray-metoden (QST November 1961) for VHF-området: glatte jordtab ved 99% (300 miles, 144 MHz: 214 dB), kurver ved 50% og 99% (fig. 4.11): 195 dB til en værdi af 100 miles til 99% og over 250 miles til 50%; evanescens ± 7 dB over 100 miles (− 3,5 dB til 50 miles), SSB 3 dB værre end CW, + 4 dB til jordrefleksion, + 3 dB til sammenkoblingen af to antenner; højdeforøgelse (15 m: 4 / 3 / 2 dB ved 10 / 50 / 100 miles; 24 m: 8 / 6 / 4 dB); Yagi gain ca. 10 gange boom 10 gange i λ (Bl; 50 dB, ca. Solopgang og solnedgang op til + 20 dB og + 20 - 50% flow; varm og tør luft over våd frisk luft på De Store Søer op til 1200 miles, typisk 400- 600; regioner påvirket af store vandmasser de mest begunstigede; sen vinter med opvarmning i stand til stærke inverteringer. Verifikation af vores troposcatter: P.452 til 50% ved 483 km og 144 MHz giver omkring 204 dB, i overensstemmelse med 214 dB af Bray til 99%. Norton 1959 (J. Res. NBS 63D, gratis på NIST): grundlæggende tab, system og sti antenne gevinst; Reference atmosfære Ns = 301; ækvivalent radius fra Ns (250 → 4878 miles, 301 → 5280, 400 → 6997); L (99) tabeller med 30-fods antenner (100 MHz: 163 dB til 100 miles, 208 dB til 500 miles; 1000 MHz: 138 og 188 dB med 28-fods lignelser); langtidsvariation V (p, θ) afhængigt af vinkelafstand (fig. 13). Ris, Longley og Norton, NBS TN 15 (1959), gratis på NIST. Barsis og Kirby, NBS Report 6751 (1961): 223 km i Colorado med Pikes Peak (4292 m) som kant, 100 og 751 MHz; konsistent tab med den afrundede kant (ca. 11,5 km radius); lang sigt evanescens 11,7- 14,3 dB versus 39- 42 dB af en spredning af samme længde; ingen dag cyklus markeret; den fire-ray model forudsat 4- 5 lapper, men kun én måles (højst til ca. 21 m fra jorden): stierne med bjerg hindring er meget mere stabile end spredning, nyttigt for forbindelserne "via Vesuvius eller Faito". ARRL VHF Digital Handbook: pakke, APRS, D-STAR, WSJT (Scatter Method og EME), ingen del på tropo. Nye gennemprøvede eller søgte datakilder: ECMWF åbne data (IFS 0,25 °, niveauer 1000, 925, 850, 700 hPa..., uden 975, 950, 900 og 800; G RIB2 med CCSDS komprimering, at vores læser ikke afkode; allerede tilgængelig gennem Open- Meteo); DWD ICON- EU åbne data (G RIB2 komprimeret bz2, en fil for variabel og niveau: mange anmodninger); Canada GDPS på MSC Datamart og GeoMet (omkring 15 km, op til 33 niveauer; GeoMet tillader afgrøden af området; det er grundlaget for Hepburn kort); GFS arkiv fra 2031.

Ellevte runde (21. november): Longley- Rice, GEKO 69. Bean og Thayer, "Modeller af Atmosfærisk Radio Refractive Index", Proc. IRE maj 1959 (s. 740 ff.): Model 4 / 3 har den rette hældning i den første km, men falder for højere; 30% af krumningen af en radius forekommer i de første 100 m og 60% i den første km; gennemsnitligt forhold mellem Ns og fald i de første km: − ΔN = 7,32 · e ^ (0,005577 · Ns) (Ns = 313 → ΔN omkring − 42). Bean and Thayer ", C.R.P.L. Exponential Reference Atmosphere", J. Res. NBS 63D n. 3 (1959): N (h) = Ns · e ^ (− c · h) med c fast af Ns gennem samme rapport; Gennemsnit Ns USA 313; stråle tracking tabeller (total kurve 15,2 mrad med eksponentiel model versus 30,3 mrad med Jorden 4 / 3). MATLAB refraktionexp: c = 0,143859 km − 1 for Ns = 313 (skala ca. 6,95 km), som gælder over ca. 1 km og 10 mrad. Brug for os: den "normale" profil af sammenligning kan være, at CRPL med de lokale Ns i stedet for M = 315 + 0,118 · z fast, og anomali af faldet i den første km skal måles i forhold til − 7,32 · e ^ (0,005577 · Ns) i stedet for − 40 fast. NTIA Rapport 82100 (Hufford, Longley, Kispill 1982), ITM guide i område mode: climi og Ns foreslået (ækvatorial 360, subtropisk kontinental 320, subtropisk marine 370, ørken 280, tempereret kontinental 301, tempereret marine på land 320, tempereret marine på havet 350); Ns = N0 · e ^ (− hs / 9,46 km); du bruger den månedlige minimum af Ns fordi mindre påvirket af super- refraktion og sub- refraktion; Ns = 301 svarer til ae = 8497 km; under 100 km Ns tæller lidt. Ris, Longley og Norton, NBS TN 15 (1959), del I: prognoser formel for fordeling over tid efter land og troposfæriske bølge, base af TN 101. SoftWright, Wikipedia, Altair WinProp og MATLAB rfprop. LongleyRice: samme information (version 1.2.2, 20 MHz- 20 GHz eller 40 GHz, 1-2000 km, Ns 301, syv klimaer, variation i tid, sted og situation); ingen former rørene, kun den statistiske effekt af de marine klimaer ("højere felter for 10% af tiden eller ej"). GEKO Magazine nr. 69 ( IU3QEZ): radiohorisont 4 / 3 af 130 km fra 1000 m, 260 km med to stationer; troposcatter med fælles volumen mellem 1-3 km og 10 km; Scatter fra fly på 10 km med + 10- 20 dB i ti sekunder eller et par minutter; teoretiske kurser med 2 × 10 elementer pr. side: SSB QRP ca. 440 km, SSB 50 W ca. 510 km, CW QRP ca. 600 km, SSB 500 W ca. 640 km (500 W i SSB gøre 40 km mere end 5 W i CW); ved 90- 99% af tiden bør en margen på 10- 20 dB fjernes 5 km pr. bredde på afstand af den samlede længde;

## 12. Runde tabel: kilder til at validere modellen eller korrigere den (November 21)

Oversigt over alle skift af kilder, bestilt til brug. "State" siger, hvis kilden allerede er i programmet (1.3.1), om det er i test, eller om det skal gøres.

### Data der skal valideres (hvad der virkelig skete)

| Kilde | Hvad giver | Anvendelse | Tilstand |
| MMMonVHF rapporter fra forfatteren | 593 QSO ved 144 MHz, 29 / 12 / 202-2 / 1 / 2022, tid og lokalisatorer | Validering af hændelser mod sammenligning (+ 14 dage) | i brug: AUC 0,79 (1.2) → 0,90 (1.3.1) |
| Verden over 50 MHz | dokumenterede tilfælde (14-15 / 8 / 2022 Manitoba-Kansas, 144-10 GHz) | historisk tilfælde | anvendes som et historisk tilfælde for at tilføje QSO til valideringen |
| wspr.live (WSPR) | komplet arkiv, 2 m og 70 cm, SQL | mange begivenheder og mange "nej" (timer uden sted) | prøvet, det skal filtreres (spot false ved 16- 20 tusinde km) |
| APRS PropView / APRS- IS | Stationer filt, afstande, timer | kun validering 144 MHz | at gøre |
| Reverse Beacon (arkiv) Name | dagligt | VHF | kasseret indtil videre |
| I GRA2 radiosondaggi (Brindisi 16320, Practice of Mare 16245, Trapani 16429, Udine, Milano) | profiler målt ved 00 og 12 UTC | GFS profilstyring og -invertering | fundet filer (Brindisi 46 MB), der skal bruges |
| Barsis og Kirby 1961, Barsis 1963 | evolution målt på stier med bjergrige hindring | validere kategorien "monte" (Vesuvio, Faito) | at gøre |
| Norton 1959, NBS TN 15, TN 101 | L (99) og tab i tabelmedier | Scatter-kontrol | delvis (se 300 miles konsekvent) |

### Korrektioner foreslået af kilder

| Korrektion | Kilder | Virkninger målt ved validering | Tilstand |
| Misme Stabilitet Betegnelse | Bean og Dutton cap. 6 | AUC 0,79 → 0,90 sammen med subsidence | i 1.3.1 (afsnit 16 dB uden ledning) |
| Underskrift fra lodret bevægelse | Moler og Holden 1960 (læs originalt 22 / 11: + 2,5 dB gennemsnit mellem luft kommer ned og luft stiger, 1,4 og 9,4 GHz på havet) | som ovenfor | i 1.3.1 (+ 1,5 / − 2 dB) |
| Blød rørledning (λ op til 3 · λmax) | Kerr, Hepburn, VHF / UHF DX Book, Shi 2019 | alene + 1% af AUC; bringer kanalens farver på 46% af begivenhedens ruter og 0% af sammenligningen | : |
| Persistens 24 e | VHF / UHF DX Book, Biancomano og Leyshon | AUC uændret, mere optaget og flere falske alarmer | i 1.3.1 |
| Normal profil CRPL med lokale Ns (− ΔN = 7,32 · e ^ (0,005577 · Ns)) i stedet for 40 N / km fast | Bean og Thayer 1959, NBS Monograph 4 | testet ved validering | i 1.3.2 som alternativ, slukkes af defekt |
| Super- refraktionstærskel 79 N / km (ITU) i stedet for 100 (Bean og Dutton, lag 1000- 850) | P.453, Lopez 2008, DX Book | testet ved validering | i 1.3.2: aggressiv agtelse ved siden af forsigtig (kortet bruger forsigtig) |
| Klimatologisk tykkelse (calcle B) mindre vigtig end underskuddet | Rogers 1997 | at prøve: bruge A til gevinst | at gøre |
| Parabolsk ligning i GFS-profil | PETOOL, wave- formering (Lytaev), Levy (bog ikke læst, kun preview), Lindquist | første version med felt over horisonten domineret af numerisk støj (AUC 0, 12- 0, 28): korrekt (domæne 8 km, stråle 6 °); efter korrektion AUC 0, 54- 0, 66 | er anvendt som en anden beregning, ikke bestemmer prognosen |
| Forværring: ± 7 dB over 100 miles, margin 10-20 dB til 90- 99% | Bray 1961, Antenna Book, GEKO 69 | grænser for veje under marginale forhold | i brug: sandsynlighed for periode og tærskel FT8 − 18,6 dB i troposcatter |
| Fælles volumen og tilsvarende afstand af Bray | IU3QEZ, Bray 1961 | Oplysninger | : |
| Troposcatter P.617 og RSGB som usikkerhed | TropoLink, DL2FZN | Oplysninger | i 1.3.0 |

### Hvad der er tilbage af modellerne

Leveres på havet i lav højde (100- 200 m), at GFS niveauer ikke ser (undersøgelse af aktuelle datakilder); refleksion og diffraktion på de nærliggende bjerge (Vesuvio, Faito): forslag til beregning "via monte". ITM / Longley- Rice forbliver en statistisk reference "normal dag" (Ns 301, climi), uden ledninger. Kasseret ved beslutning af forfatteren: AirScout, P.528), E sporadisk (P.2001) og 50 MHz (servoir EIRP, at få har og forveksler med E sporadisk).

### Forslag til arbejdsplan

1. Udvidet validering: Tilføj filtreret WSPR (selv timer uden spot) og QST 2022; gentag for 432 MHz.
2. Prøv den normale CRPL profil og ITU tærskel på 79 N / km, en ad gangen, holde kun hvad hæver AUC.
3. Sammenligning af GFS profiler med I GRA2 radiobølge af Brindisi og praksis af havet i de dage af QSO.
4. Revaluere parabolske ligning efter numerisk korrektion.

### Beslutninger af 21. november (version 1.3.2)

- Vedtaget: forsigtigt skøn (Bean og Dutton, 100 N / km) og aggressivt (ITU, 79 N / km) flankeret; kanaler søges kun under 2500 m (første 1500 m, hævet efter validering); bonus med højt udvidet tryk og svag vind; havvægt med μ1 af P.452; andel af stationer med hensyn til kanalen også på delvis ledning; grænselag over kamme ( IT9GSF).
- Vedtaget som et alternativ, slukket af defekt: normal CRPL profil med lokale Ns.
- Korrekt parabolligning numerisk: AUC fra 0, 12 til 0, 54 (30 m antenne) og fra 0, 28 til 0, 63 (300 m).
- Land: Copernicus DEM GLO- 30 i stedet for Terrain Tiles "skadi", som nær kysten har spøgelsesrelieffer på 80-100 m i havet (Mezzogiorno IC8TEM, verificeret punkt for punkt til Capri); skadi forbliver som reserve.
- Vejr tegn på kortet med deres virkning: ↓ favorable subsidence (Moler og Holden 1960, i gennemsnit omkring 2,5 dB mellem luft kommer ned og luft går op; + 1,5 dB), ↑ luft op ad bakke ugunstige (dybe inventar; − 2 dB), S strejke gunstige spor (ARRL Handbook § 19.4, stærke inversioner på Middelhavet; ingen korrektion). Dobbelt symbol, grøn / rød / orange farver og tegneserie på kortet.
- Vedtaget ITM 1.2.2 (NTIA / itm, offentlige domæne), oversat til C #: ifølge beregning af baseline i normal atmosfære ved siden af P.452 og sidste strækning efter udgangen fra en kanal, der slutter før korrespondenten. Kasseret qradiopredict kode fordi under GPL v2.

Uden nyttigt indhold til indekset: * Communications Quarterly *, efteråret 1991 (ingen tropo artikel; Atkins 'artikel om Scatter er i vinteren 1991 udgave).

- Vedtaget som manualprincip (21. november): "kan ikke generaliseres, men undersøges i relation til lokal morfologi" ( DF5AI, rapporteret af IK3XTV i "Formering på 2 meter"); citeret i prognosemanualen (§ 1) og i rådene.
- Vedtaget (21. november, efter 1.3.3) fortolkningen af IK3XTV 's eftermiddagstralpine åbninger: ikke "konvektive celler som linser", men troposcatter forstærket af turbulens til toppen af det lag, der re- vedtaget, når det når andelen af den fælles volumen (manual af prognoser, § 8 ter). Fra 1.3.4 går det ind i pointpunktsberegningen som en foreløbig forstærkning af troposcatter på højst 3 dB. Kasseret af de samme artikler: "automatisk forstærkning" af signalerne uden for Alperne (et passivt medium forstærker ikke: de beskrevne links forbliver forklarlige med diffraktion og troposcatter; de erklærede beføjelser bruges ikke som data), sneen, der "better diffraktion" (sandsynligvis det stabile lag over sneen, med mindre evolution).

- Kerr (1951) revideret på bogen (November 22, PDF leveret af forfatteren): § 1.5, eq. 17 og 18 og tabel 1.1. Tallene for eq. 18 i scanningen er ulæselige, men tabel 1.1 bekræfter: 20 ft → 1 cm, 80 ft → 10 cm, 400 ft → 100 cm, 2000 ft → 1000 cm, mod 1,25, 10, 112 og 1252 cm af λmax = 0,014 · d ^ 1,5 (λ i cm, d stående; den runde tabel, 'vejledende værdier'. Kerr hypoteser: inde i kanalen de modificerede indeks dråber på 4 · 10 − 8 per fod, typisk værdi af de lave kanaler; med svagere gradienter har brug for en større tykkelse. Kerr advarer også om, at λmax ikke er en netto snit: bølgelængder undertiden længere kan stadig være stærkt påvirket af kanalen.

- DL6WU gennemgået på den oprindelige (November 22, PDF af VHF Communications 3 / 1982 leveret af forfatteren): G. Hoch, "Ekstremt Long Yagi Antennas", s 130 og senere. Fig. 1: gevinst på dipolen målt ved 432 og 1296 MHz i en professionel polygon af de tyske indlæg, med en prøve antenne EIA fra 7,7 dBd, estimeret nøjagtighed 0,5 dB; 2,35 dB for dobbelt længde af boom, ingen mætning op til omkring 20 λ. Programmet bruges 8.6 + 8.0.log10 (L / λ) dBd, dvs. omkring 0,8 dB i mindre end 1-2 λ; nu bruger det 9,4 + 7,8 · log10 (L / λ) dBd (gradienten er den angivne, 1 λ værdien læses på diagrammet, ± 0,3 dB). Kasseret formel 10 · log10 (7,8 · L / λ) cirkuleret på nettet: ville give 3 dB for fordobling, mod 2,35 målt.
- Booker and Gordon (1950): reference bekræftet af "This Week 's Citation Classic" (Nuværende indhold, 8 februar 1982, kommentar af H. G. Booker): Proc. IRE 38, pp. 401- 412. Kommentaren bekræfter de to punkter, der anvendes i manualerne: felt modtaget ud over horisonten med Rayleigh distribution, og eksponentiel autokorrelationsfunktion af uregelmæssigheder (non-Gaussian). Den oprindelige artikel er ikke blevet læst.

## 13. fra IK3XTV -artikler

De to artikler af Flavio Egano IK3XTV (Radio Rivista: "Formering på 2 meter" og "Formering VHF - konvektive celler og gravitationelle bølger") tilføjer to ting, som ledningsmodellerne ikke dækker: de transalpine åbninger af eftermiddagen på grund af konvektion, og praktiske regler om, hvor og hvornår kredsløbene er dannet i Italien. Det er observationer fra stationen, ikke foranstaltninger: de skal behandles som hypoteser for at kontrollere med pletter.

| Ophavsmandens observation | Hvordan det bliver en ingrediens i indekset |
| Kanalerne er hovedsageligt dannet mellem 100 og 1000 m, sjældent højere; tjene højt tryk på et bredt område, god insolation og fravær af vind | Søg efter kanalen kun under 1500 m; bonus med højt udvidet tryk og svag vind i de lave lag |
| I de varme måneder udføres tilbagevendende på Adriaterhavet mellem Veneto og Puglia, 700- 800 km, undertiden op til Grækenland; de maritime ruter er foretrukne | større vægt til den brøkdel af havruten (såsom μ1 af ITU-R P.452) |
| Den tilsvarende skal være i eller nær kanalen; med høje antenner og et par meter bly signalet ikke indtaster | sammenligne andelen af basen af kanalen med andelen af de to stationer (vil tjene højden af QTH) |
| Samme tid, samme kanal: Lyt fra Gargano en station i 144 og en i 50 MHz | at kontrollere: Ifølge Hepburn bordet 50 MHz bede om over 300 m bly, så begivenheden ville indikere en rørledning meget ofte |
| Connections Veneto - Sydtyskland uden visuals, næsten altid om eftermiddagen og med højt tryk; ustabile signaler, der forsvinder og dukker op igen efter et par minutter | "Konvektiv" indeks adskilt fra kanalen, for bjergruter |
| De konvektive celler er født på jorden og klatre omkring 1 km hver 3. time: ved 15 de når omkring 3 km, den højde, der tjener til at tilsidesætte Alperne | bruge grænse lag højde (grænse\ _ layer\ _ højde af Open- Meteo GFS) langs stien: score, når overstiger højden af kamrene |
| Alperne genererer tyngdebølger, der sammen med konvekse bevægelser skaber uregelmæssigheder i brydningsindekset | hypoteser: stærk vind vinkelret på kæden til 700 hPa som signal om guldbølger; at prøve sidste |
| Meget høj inversion (op til 8 km i særlige tilfælde) giver omkring 800 km, men i praksis sjældent mere end 300 km | andelen af inversion udvider anvendelsesområdet: nyttige data til at estimere venteafstanden |
| Om sommeren Jordens radius svarende til omkring 16000 km (2,5 gange den reelle), med omkring 60 dB i mindre end 300 km tab | er i overensstemmelse med k50 = 157 / (157 − ΔN): k = 2,5 svarer til ΔN omkring 94 N / km, dvs. superbrydning |
| Mere end 3000 km kun i 20- 30 ° området nord og syd for ækvator, over havet, knyttet til Hadley celler; top i august i Africa-Europa | uden for Europa: nyttigt ikke at give høje indeks på Atlanterhavet uden feedback, og for den kanariske rute - Storbritannien |

Andre nyttige data fra samme artikler:

- Optisk område mellem to h1 og h2 meter (d i km): d = 4,1 · (· h1 + · h2), op til ca. 20% for normal brydning.
- Fremgang: langsom og overfladisk med inversion; stærk og hurtig med spredte og konvektive celler. Programmet kunne bruge det til at klassificere annoncer, hvis en dag det vil have SNR rapporter over tid.
- For sporadisk E i 144 MHz forfatteren rapporterer arbejdet i DF5AI og Davis og Johnson (Nature 435, 2005) om båndet mellem temporals og Es: ud af tropo, men en separat indikator i morgen kunne bruge lyn data.
- Jordrefleksion i dobbeltspring Es: hav K = 81 og 5 S / m, landskab og skov K = 13 og 0.006 S / m, byområder K = 3 og 0,001 S / m.

## 14. LIVETROPOVHF indeks hypoteser

Startforslag, der skal drøftes: et indeks 0-10 på samme skala som Hepburn, beregnet for hvert punkt i nettet og hver 3 timer fra GFS (derefter ECMWF og ICON til sammenligning), og et andet nummer pr. rute, der siger, hvor meget del af ruten passerer ind i en nyttig kanal til det valgte band. Vægtene nedenfor er mine antagelser, der skal kalibreres med pletterne.

Gennemgå efter Bean og Dutton og Kerr: profiltærskler kommer fra deres tabel 6.5 og skala fra deres målinger, så et brydningslag er værd 10- 25 dB over den normale profil. Indekset beregnes derefter først i dB og derefter på 0-10; værdierne i den pågældende saks skal stadig kalibreres med pletterne.

Beregning for punkt, trin for trin

1. T-udledning, relativ fugtighed, geopotentiel højde og vertikal hastighed ved 1000, 975, 950, 925, 900, 850, 800, 700, 600 og 500 hPa, plus T og 2 m fugtighed, jordtryk, laghøjde og vind ved 10 m.
2. Kassér niveauerne under jorden; beregning N og M på hvert niveau (ITU-R P.453).
3. Beregning − dN / dh i hvert af de fire lag af Bean og Dutton (1000- 850, 850- 700, 700- 600, 600- 500 hPa) og diagrammet som sub- refraktiv, normal eller super- refraktiv med tærskelværdierne for deres tabel; inde i det første lag også bruge mellemliggende niveauer til at finde den stærkeste gradient og dens andel.
4. Hvor dM / dh < 0 finder jeg base og top af kanalen, tykkelse d og underskud ΔM.
5. Sammenligning med mindste tykkelse for båndet fra formlen for Kerr (ca. 180 m for 144 MHz, 89 m for 432 MHz, som i Hepburn-tabellen); under tærsklen tæller ledningen stadig, men mindre.
6. Jeg stimulerer gevinsten i dB med nedenstående tabel (du tager det bedste lag, ikke summen af lag, plus udtrykket for anomali) og konvertere det: index = dB / 2,5, mellem 0 og 10, så 25 dB er værd 10. En negativ værdi (sub- refraktion) vises som 0, men forbliver i stiens data.

| Bedste lag situation | Kriterier | Anslået indtjening |
| Normal (ikke lamineret) | − dN / dh i fanebladsområdet. 6.5 (f.eks. 20- 60 N / km mellem 1000 og 850 hPa) | 0 |
| Lidt over normalt | mellem den øvre grænse for normalbåndet og overbrydningstærsklen | 0-10 dB i forhold |
| Super- refraktion | over lagtærsklen (100, 80, 70, 50 N / km klatring), men under 157 N / km | fra 10 til 15 dB i forhold til 157 N / km |
| For tynd rørledning til bånd | dM / dh < 0, tykkelse under Kerr- tærskel | 15 |
| Nyttige stik til bånd | dM / dh < 0, tykkelse over Kerr-tærsklen | 20 dB, + 1 dB for hver 5 M-underskudsenhed, højst 25 |
| Subfraktion | − dN / dh < 0 i det nederste lag | − 10 dB |
| Anomal baggrund (du tilføjer) | Model Ns minus ITU kort klimatologisk Ns for den pågældende måned | 0,2 dB pr. enhed N mellem − 5 og + 5 dB |

Beregning efter rute (mellem to lokatorer): prøve den maksimale cirkel hver 10 km, som allerede programmet for indekset på stien, og beregne brøken af punkter med nyttig kanal og den gennemsnitlige andel af basen. CSIRO-undersøgelsen viste, at andelen af ruten under rørledningen var nyttig (på AIS-ruterne ved 162 MHz var 67% højere end i kontroltilfældene).

Tilføjelse af beregningen pr. rute (fra LA0BY og W3SZ): (1) søstationer sammenlignet med bunden og toppen af kanalen, fordi en høj ledning kun tjener dem, der indtaster; (2) en troposcatter "gulv" med Yeh-metoden, med starthjørner taget fra jorden profil; trope gevinst i dB fjernes fra dette tab, og resultatet sammenlignes med det tab, som stationen formår at overvinde ( KK6MC tabel). Således indekset bliver også et skøn over opnåelige afstand.

Rettelser til at prøve en ad gangen (fra Bean og Dutton og LA0BY, IK3XTV, G4MCU allerede i manualen), at holde kun hvis de forbedrer sammenligning med pletter:

- Blanding (vigtig ifølge forfatteren ved daglig observation): fra dag til dag på jorden, mellem morgen og eftermiddag med klar himmel eller med vind til jorden over nogle få m / s forsvinder kanalerne til jorden; deres bidrag skal reduceres. Ikke til søs.
- Time of the day (importante vigtigt ifølge forfatteren, ved daglig observation): irraggiamento kanaler opstår ved solnedgang og falder efter solopgang; det er en 3-timers prognose kontrol.
- Lodret bevægelse: luft stiger til 850- 700 hPa omblander lagene (negativ korrektion), og luft under en anticyklon styrker dem (positiv).
- Tryk trend: op tryk på stien i de sidste timer, højere og stabile signal; i fald lavere signal (bastano 2 hPa).
- Synonym: højt tryk over 1025 hPa, temperatur på 850 hPa meget over det på jorden, varm og tør luft, der flyder på det koldere hav, rute på havet.
- Bjergstier: et andet "konveksivt" indeks (sektion IK3XTV), med et eftermiddagsloft lag højere end rækkerne langs stien og højt tryk.

fra VHF / UHF DX Book, andre rettelser for at prøve: på hinanden følgende dage med højt tryk på det punkt (flere dage, flere point), position i forhold til midten af anticyklin (bonus ved kanterne), tryk gradient på jorden som mål for vinden, stærk regn i de 24 timer før (penalitet), lave skyer i kanten af laget (penalitet kun for de høje bånd). For ruternes retning: sammenlign rutens azimut med isobanens.

Verifikation

- LIVETROPOVHF arkiv: enhver kommerciel over 500 km til 144 eller 432 MHz er en "begivenhed". Den sammenligner indekset på ruten med hyppigheden af begivenheder på samme tidspunkt af dagen og måneden (samme fremgangsmåde i CSIRO-undersøgelsen).
- Sammenligning med Hepburn og F5LEN på de samme dage: hvor de tre indekser frakobles, ser du, hvem der havde ret ifølge pletterne.
- Radiofrekvenser på 00 og 12 UTC: at kontrollere, om modellen ser de inverteringer, sonden måler.
- Historiske tilfælde af LA0BY (6-11 / 11 / 2003, 10 / 12 / 2004, 15 / 10 / 2011, 11 / 2 / 2015, 22 / 11 / 2018): Indekset omregnet på disse dage skal se de kanaler, Hepburn ikke viste.
- Grænser, der skal angives i manualen: udføres under 100- 200 m næsten usynlige for GFS tryk niveauer; pålidelig prognose for 233 dage, derefter kun tendens.

## 15. Kilder af 21. november (anden del)

- ITU-R P.1812 (MaTLAB / Octave implementering af I. Stevanovic, OFCOM; GitHub eeveetza / p1812, Zenodo 10.5281 / zenodo.6141558). VHF / UHF area- specifik metode af ruten, afledt af P.452: "delta-Bullington" diffraktion, troposcatter, kanaler og refleksion på lag, rigiditet på jorden. Nyttig som en tredje sammenligning ved siden af P.452 i pointpunkt beregning; ΔN og N0 data er fulde ITU-produkter, så brugeren downloader dem (som pr. P.453).
- fra Costa, Rabirez, Dias, IET MAP 2018 (doi 10.1049 / iet- map.2017.0992). 563 MHz, 27 point mellem 5 og 30 km på uregelmæssigt terræn med skov: P.526 med 12 m vegetation − 4,3 dB og ↑ 8,8 dB; P.1812 med 10 m rigiditet + 2,6 dB og ↑ 8,9 dB; dielektrisk kile − 4,8 dB og ↑ 9,2 dB; P.1546 + 5,0 dB og ↑ 11,7 dB. For programmet: den resterende fejl i en god diffraktionsmetode er omkring 9 dB, og højden af de behæftede på kamre (10-12 m) tæller.
- PETOOL (Ozgun e altri, Computer Physics Communications 182, 2011). Parabolsk ligning split- trin til en og to ruter med bred vinkel propagator (vinkler op til 40-45 °, mod 10-15 ° af den smalle); bund med tab gennem den transformerede Fourier blandet; jord til trin. Bekræfter valget af 1.3.2 (bred vinkel, jordkoefficienter) og angiver tovejs beregning for stejle relieffer.
- MathWorks. `tropopl`: tab af gasser og tab af "lente" i en lagdelt atmosfære (P.835, P.676, P.453) uden kanaler: med gradienter under − 157 N / km er den geometriske optiske model ikke gyldig. Eksemplet med radarspredning til refraktion − ΔN = 7,32 · e ^ (0,005577 · Ns), den samme CRPL rapport allerede i programmet. `propagationModel` Longley- Rice: foruddefinerede horisontale polariseringsværdier, ledningsevne 0,005 S / m, permitivitet 15, reaktivitet 301, tempereret kontinentalt klima. Ingen MathWorks model tager sig af kanalerne.
- ITU-R, koncernens digitale produkter 3. Data og programmer af P.453, P.452, P.1812, P.2001, P.528, P.1546; 'integrerede' data er en del af anbefalingerne; ingen erklæret omfordeling licens: du fortsætter med at lade dem downloade til brugeren.
- ESA Naviedia, troposferisk forsinkelse. N = Nidrostatica + Numida; den tørre zenital forsinkelse er omkring 2,3 m og den fugtige en tiendedel af cm, som varierer hurtigt. For os: Den vådforsinkelse, som GNSS-stationerne har anslået, er et indirekte mål for den integrerede luftfugtighed, en mulig fremtidig kilde til nu-støbning.
- Bray, K2LMG, "A Method for Determining V.H.F. Station Capacities", QST November 1961, s. 36-40 (læses i original; kontrolleres igen den 22. november på PDF af antallet af QST: alle værdier nedenfor svarer). Forfatteren, D. W. Bray (General Electric, Ithaca), siger, at han har verificeret prognoserne med måneders planlagte forbindelser med K2GOI og W4LTU. Fremgang inden for tiden 13,5 dB over ca. 100 miles (7 dB under og 7 over timegennemsnittet), næsten lineær under (3,5 dB til 50 miles): for en solid tilslutning fjernes 7 dB. Korrektion af horisonten: Hver højde af horisonten er værd 69 miles (111 km) af ekstra afstand (minus hvis negativ). Opnå en yagi omkring 10 × boom / λ (effektforhold), mere omkring 4 dB jordrefleksion; SSB − 3 dB og AM − 7 dB sammenlignet med CW; 99% af timerne: ca. 180 dB til 60 miles, 190 til 90, 200 til 240 (2 m). Fra 1.3.2 anvendes sandsynligheden for perioden til at bruge ‡ = 3,4 dB (± 7 dB som ± 2‡) i stedet for de 5 dB, der skønnes at være brugt på anden hånd.
- To-vejs beregning (prøv med prototypen, 21 November). Stor vinkel parabolsk ligning med ægte S RTM1 terræn fra Capri, 144 MHz, antenne 10 m fra jorden, standard atmosfære; måle: gennemsnitlig effekt mellem 20 og 400 m til 80 km på ruten mod den tilsvarende. Fra centrum af Capri og Marina Grande vest er lukket (hindring til + 11-21 °, Monte Solaro): Ved at pege Faito (næsten bag EA5 og EA6, 150- 160 ° ud af aksen) det signal, der blev sendt til Spanien, stiger med 10- 29 dB; peger Vesuvius (110- 125 ° ud af aksen, derefter ud af beregningsplanen, kun vejledende) med 17- 25 dB. Fra Anacapri er Vesten fri, og den direkte gevinst er 8-22 dB. De absolutte tab på 1000- 1300 km af prototypen er imidlertid ikke pålidelige. EA3 EA5 EA6
- Barsis og Kirby, NBS Report 6751 (1961), Pikes Peak, 223 km, 100 og 751 MHz. Bjerget er en fælles terminal af to ruter i optik: fordelingen af timeløn medians er convolution af de to ruter. Langvarig udflugt (1-99%) på ca. 14 dB mod 26- 40 dB af en troposcatter-vej med samme længde eller vinkel; ingen nævneværdig dagsændring. Signalet er stærkere end troposcatter, men svagere end den ideelle kant: toppen skal behandles som afrundet (ru omkring 11,5 km). Der observeres altid høje signaler bag dorsalerne.
- Barsis, NBS Report 7652 (1963), tre ruter over et bjerg (Colorado, Alaska, Europa), 750- 900 MHz. Gennemsnitligt tab 10- 20 dB højere end Fresnel- Kirchhoff beregning uden jord i nærheden af terminalerne; den evanescens estimeres ved indpakning af de to linjer i optik; refleksionerne af jorden i nærheden af terminalerne forværre den langvarige fade. Fra 1.3.2 indgangen til kanalen tilføjer afrunding af P.526.
- Barsis og Johnson, NBS TN 88 (1961). Langvarig opløsning af rumbølgen, selv på en sti ud over et bjerg (§ 5).
- Norton, "Systemtab i radiobølge formering", J. Res. NBS 63D (1959). Kurverne hvorfra Bray får nomogrammer; definitioner af systemtab, transmission og grundlæggende, proces antenne gevinst.
- Ris, Longley, Norton og Barsis, NBS TN 101 (1965-67), bind 1 og 2. enkelt kant og hindring afrundet med og uden refleksioner (§ 7, med eksemplet med Pikes Peak), langtidsvariation V (0,5) og Y (q) for klimaer (hærdede conkontinentale og andre). For os: Metoden med afrundede hindrings- og variationsfunktioner er referencen til kalibreringsusikkerhed.
- ITU-R P.526- 16 (11 / 2025). Formel (31) af J (v), identisk med programmet; afrundet hindring (32) - (36), nu anvendt; multisplit metoder (Bullington og fuld metode § 4.5).
- ITU-R P.1812- 8 (2025) og -7. Komplet tekst genvundet: specifik metode af 30 MHz til 6 GHz (delta- Bullington, troposcatter, kanaler og refleksion på lag, rigiditet). -7 advarer om, at dens troposcatter (fra P.617-5) er forkert under 66 km. Det er stadig en mulig tredje sammenligning.
- Paulus, "Praktisk anvendelse af IREPS-fordampningsmodellen", NOSC TR 966 (1984) og Radio Science 20 (1985). Rapport læst på 22 / 11 / 2026 (DTIC ADA146528, scanne med anerkendt tekst, undertiden ulæselige). Lufthavs-forskelle målt af skibe flyttes mod stabilitet: varmere havluft i 31- 35% af skibets data versus 0,7- 7% af NOAA-orner, for pecirometre forstyrret af skibet og SST taget af kølevand (ca. + 1 ° C). Jeske modellen giver derefter 40 m og mere end 15% af året og højder, der springer fra 10 til 40 m på 24 timer med stabil tid, ikke bekræftet af radioforanstaltninger. Foreslåede korrektioner (afprøvede tærskelværdier 0, − 0,5 og − 1 ° C; − 1 ° C er bedre aftalt med radioforanstaltningerne fra juni 1982): hvis luft- / havforskellen er − 1 ° C eller mindre anvendes den målte; ellers beregner man højden med − 1 ° C og med 0 ° C, og hvis den med − 1 ° C er lavere, holder man det. Stigninger på den stabile side er skåret, fald tilbage. I juni, Marsden 120 kvadrat: gennemsnitlig højde fra 16,8 m (klimatologi) til 7,6 m. Modellens ligninger er ikke med i rapporten (se Jeske og Hitney). Programmet bruger den neutrale profil, som ikke har stabil overdrivelse, og grænser δ til 40 m: det går i samme retning; med SST modeller (ikke skib) måling defekt, Paulus retter vejer mindre.
- Levy, "Parabolic Equation Methods for Electromagnetic Wave Promapation" (IEE 2000). På Google Libri er der kun forhåndsvisning: Jeg læser ikke.
- VK3FS (3fs.net.au). Kvalitetsintroduktion: høje kanaler på 450- 2000 m for længere åbninger på jorden; inversioner af 300- 600 m i rolige og rolige nætter, som forsvinder efter solopgang; stærkere kystinversioner om eftermiddagen; 800- 1300 km typisk, 1400- 1600 km ofte udføres. I overensstemmelse med dag / nat-alarmer og loft på 2500 m.

## 16. Kilder

Regler og formler

- [ITU- R P.453-13 (2017), radiofoniindeks:](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)formel og data
- [ITU- R P.452-18 (2023), forudsigelse af interferens mellem jordstationer over 100 MHz: ækvivalent radius, β0, troposcatter,](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)kanaler
- [ITU-R P.617-5, spredning ud over horisonten for radiobroer](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)
- [Shi et al., "En omfattende undersøgelse af Wave Maximulength of Electromagnetic Formering i forskellige fordampningsdukker", IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

Undersøgelser af forudsigelse af kanaler med vejrmodeller

- [P. Lopez, "A 5-år 40- km opløsning global klimatologi af super- refraktion for jorden-baserede meteorologi radar", ECMWF Tech. Memo 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO) ", Troposfærisk Ducting forudsigelse baseret på GFS Model Data på... CSIRO Murchison Radio- astronomi", Observatory arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (FMI), "Effects of model resolution on predicting atmosfæric ducking conditions with Harmonie-AROME", EGUsphere 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

Amatør radioprognoser

- [W. R. Hepburn, "The Hepburn Tropo Index (HTI)"](https://www.dxinfocentre.com/propagation/hti.htm)og [](https://www.dxinfocentre.com/propagation/history.htm)metode historie
- [W. R. Hepburn, "Laveste Anvendelig Frekvens vs. Tykkelse af dukkende lag"](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, "Worldwide refraktionsindeks prognoser"](https://tropo.f5len.org/WW/info.php), [ny version 2026](https://tropo.f5len.org/?p=504), [note on model ARPEGE](https://tropo.f5len.org/author/f5len/)

Data

- [Open- Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api), [note om trykniveauer](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)
- [NOAA NCEI, Integrated Global Radiosonde Archive (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RAOBget, radio oversvømmelse script for University of Wyoming](https://github.com/NCAR/RAOBget)

Klassiske tekster (angivet af forfatteren af programmet, skal høres om endelige formler)

- B. R. Bean, E. J. Dutton, * Radio Meteorology *, NBS Monograph 92, 1966, 435 sider (genoptryk Dover 1968): lodrette profiler af N og M, gradient af fælde under − 157 N / km, sammenligning med radiobølge; kapitel 5 "Synoptisk radiometeorologi". Base for 234 trin af beregningen pr. punkt. Gratis: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [NIST kort, doi 10.6028 / NBS.MONO.92](https://www.nist.gov/node/1770911), [Internet Archive](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf)kopi.
- L. Boithias, * Radio Wave Propagation *, McGraw- Hill 1987 (oversat af D. Beeson fra fransk, 330 sider, ISBN 0070064334; også North Oxford Akademic): mekanismer uden for horisonten, fælles volumen af spredning og tab af koblingsantenner-halvdelen. Papir alene ([bibliotekskort](https://libcat.library.tamu.edu/Record/in00000014812/Description)).
- M. P. M. Hall, * Troposfærens indvirkning på radiokommunikation *, IEE Electromagnetic Waves Series nr. 8, Peter Peregrinus 1979, 206 sider (ISBN 0906048257; brossura 0863410863): syntetiske situationer og formering VHF / UHF. Base for synoptiske ingredienser. Handel på IET ([](https://libcat.library.tamu.edu/Record/in00000636385)-kort).
- H.G. Booker, W. E. Gordon, "A Theory of Radio Scatter in the Troposphere", * Proceedings of the IRE *, vol. 38, n. 4, April 1950, pp. 401- 412: Scapping teori fra disogenity af brydningsindekset. Central idé: den diffuse effekt afhænger af intensiteten og omfanget af de turbulente udsving i brydningsindekset (eksponentiel korrelation); nær horisonten dominerer diffraktion, yderligere spredning; Ifølge forfatterne omfanget af turbulens og indeks variationer forventes af meteorologi er tilstrækkelig til at forklare de målte felter. Citeret af 328 artikler (Scilit), ingen gratis adgang kopi. Kort på semantisk Scholar ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); fuld tekst på IEEE Xplore betalt ([doi 10.1109 / JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)).
- C.M. Crain, J. R. Gerhardt (Electrical Engineering Research Laboratory, University of Texas), "Målinger af de parametre, der er involveret i teorien om radiospredning i Troposphere", * Proceedings of IRE *, vol. 40, n. 1, Januar 1952, s. 50-54, doi 10.1109 / JRProcc.1952.2739 give de reelle værdier, som Booker og Gordon kun havde estimeret ([Skilit](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)tab]).
- "The Constants in the Equation for Atmospatial Refractive Index at Radio Frequences", * Proceedings of the IRE *, vol. 41, n. 8, August 1953, pp. 1035- 1037: foreslår at gennemgå strømmen derefter i brug (79 K / mb og 4800 K) i formlen N; det er herfra, at værdierne 77,6 og 3,73 · 10 ^ 5 af formlen ITU (Q](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E. C. S. Megaw, "Grundlæggende radiospredning teori", * Proceedings of the IEE Part C: Monographs *, vol. 104, n. 6, september1957, pp. 441- 455, doi 10.1049 / pi- c.1957.0052: spektret af indeks udsving fra turbulens teori, komplet sporing af spredning ud over den fælles horisont og effekt af geometri betalt på IET ([Shilit](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)kort).
- * Proceedings of the IRE *, Oktober 1955, special number on scatter formering (by K. A. Norton and J. B. Wiesner), with Bullington, Staras m.fl.: free in PDF on [World Radio History](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf).
- D. E. Kerr (redigeret af), * Propagation of Short Radio Waves *, MIT Radiation Laboratory Series vol. 13, McGraw- Hill 1951 (rapporter Dover 1965 og IEE 1987): teori om fordampning og overfladekanaler på havet. Digital kopi: [World Radio History](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); IET udgave i [IET Digital Library](https://shop.theiet.org/propag-short-radio-waves).

Nylige målinger af troposferisk diffusion i VHF: W Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Australien), "A Study of VHF Troposcatter Communication Kanals in South Australia", in * Digital Signal Processing for Communication Systems *, Springer 1997, pp. 103-112 ([Springer](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)). Tekst for et gebyr; fra abstrakt det vedrører diffusion, ikke kanaler. Nyttig kun hvis du ønsker at kalibrere udtrykket for troposcatter med VHF kanal data.

Numeriske modeller og sejlruter (angivet af programmets forfatter)

- M. Levy. * Parabolic Equation Methods for Electromagnetic Wave Promagation *, IEE Electromagnetic Waves Series # 45, 2000, 336 sider (ISBN 0852967640): Parabolic ligningen for brydningsprofiler, der ændrer sig langs stien og for orografi; kapitler om tropofisk formering, stråler og måder, formering på havet. Det ville tjene til en feltberegning langs stien, ud over indekset. Til betaling, også i elektronisk form fra IET ([IET kort](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)).
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (universitetet i Leicester): 2 GHz målinger på Kanaløerne og Kanalen. Mange værker og afhandlinger er gratis i universitetets arkiv: [langsigtet statistik om fordampningskanalen, Radio Science 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434), [signalændringer på tre havruter, Radio Science 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710), [Titler af N. Mufti 2012 med ruter på 50 og 140 km også i VHF](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854), [tesis af I. Alam 2002 om reaktivitet beregnet ud fra vejrdata, 240 MHz og 2 GHz X3 UQ, [tesi.](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus, "Praktisk anvendelse af en fordampningsmodel", Radio Science vol. 20 n. 4, 1985, s. 887 (ikke IEEE Transaktioner på Antennas og Formering), doi 10.1029 / RS020i004p00887 ([Scheda](https://zendy.io/title/10.1029/rs020i004p00887)): højde af fordampningskanalen fra overflade vejrdata. Udfyld den tekniske rapport, der ligger foran den: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf).

T. Lindquist, * Wave Formering Modeller i Troposphere for Long- Range UHF / SHF Radio Connections *, masterafhandling, University of Karlstad, 2 juni 2020, 106 sider (leveres af forfatteren af programmet). Det er en step-by-step guide, gratis, at skrive en beregning med parabolske ligning: split-step løsning (selv ved brede vinkler), grænsebetingelser for land og hav, trin jord, stråle sporing på lineære profiler til tider, med numeriske kontroller; citerer PETOOL gratis værktøj til MATLAB. To nyttige data på én gang: (1) N følsomhed over for vejrvariabler, δN szá0.26 δP + 4,3 δe − 1,4 δT (hPa og K, omkring 20 ° C), dvs. en fejl på 1 hPa på damptrykket vejer tre gange en 1 ° C fejl på temperaturen: er fugten af den model, der skal kontrolleres først; (2) Gennemsnitlig højde af fordampningskanalen: 5-6 m i Nordsøen, 13-14 m i Middelhavet, over 20 m i Den Persiske Golf, altid for lav for 144 og 432 MHz. Forfatterens konklusion (frekvens 1 til 10 GHz): ud over horisonten kræver de pålidelige forbindelser næsten altid en kanal.

Formeringssoftware på jordprofilen (for horisont og forhindringer, ikke for den forventede tropo)

- [Radio Mobile](https://ve2dbe.com/rme.html)af Roger Coudé VE2DBE: gratis, Windows (også Linux og Mac med vin), Longley- Rice / ITM model, terræn profil automatisk udtrukket fra kvotedata ([download](https://ve2dbe.com/download/download.html)).
- [SPLAT!](https://qsl.net/kd2bd/splat.html)af John Magliacane KD2BD: GPL, 20 MHz- 20 GHz, Longley- Rice og ITWOM v3.0 modeller, forhindrings- og tabsrapporter i tekst; seneste version 1.4.2 af 2014.
- [ITM, NTIA / ITS](https://its.ntia.gov/software/itm)officielle kode: Longley- Rice model (20 MHz- 20 GHz) med kilde og dokumentation, base af Radio Mobile og SPLAT!. Koden er gratis, og du kan tilføje til beregningen på stien til LIVETROPOVHF (tab på grund af jorden, der skal tilføjes til tropo indeks). Original report: A. G. Longley, P. L. Rice, "Prodiction of Troposfæric Radio Transmission Loss Over uregelmæssig Terrain: A Computer Method - 1968", ESSA Technical Report ERL 79- ITS 67, juli 1968 (NTIA kort på historisk software: X1 https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITU-R, software og digitale data](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): gratis kort over ΔN og N0 årlige medianer på gitter 1,5 ° anvendes af P.452 og P.617 - nyttige som referenceklimaværdier for indekset.
- Pathloss (Contract Telecommunication Engineering): kommercielt program for radiobroer; Jeg har ikke fundet en offentlig kilde, der bekræfter, hvilke ITU anbefalinger gennemfører, så at kontrollere.

Genanvendelig GitHub kode i LIVETROPOVHF

| Projekt | Hvad han gør | Sprog | Brug muligt for os |
| [NTIA / ITM](https://github.com/NTIA/itm) | model Longley- Rice embedsmand i NTIA, med pakker til. NET |  C++, C#  | Landtab på stien: integrerer direkte i programmet, som er i C # |
| [fuzzzeee / itm](https://github.com/fuzzzeee/itm) | ITM C + + kode C # oversættelse |  C#  | alternativ uden indfødte biblioteker, også nyttigt for Windows 7 version |
| [eeveetza / Py452](https://github.com/eeveetza/py452) | ITU-R P.452, identisk med MATLAB / Octave-versionen godkendt af ITU-R WP 3M; MATLAB-versionen udgivet af det schweiziske føderale kommunikationskontor | Python, MATLAB | henvisning til oversættelse til C # troposcatter formlerne og udført, med ITU valideringseksempler |
| [bwinkel / pycaf](https://github.com/bwinkel/pycraf) | P.452 komplet, P.676, SRTM profiler; GPL v3, men ITU datafiler inkluderet er ikke gratis til kommerciel brug | ♪ | kontrol af vores resultater |
| [ITU- Rpy](https://github.com/inigodelportillo/ITU-Rpy) | mange ITU- R anbefalinger, herunder P.453- 13, valideret med ITU eksempler | ♪ | kontrol af N-formlen og klimakort fra P.453 |
| [NCAR / RAOBget](https://github.com/NCAR/RAOBget) | download radiobølgen af University of Wyoming i tekstformat | ♪ | diagram til at downloade i C # profilerne på de italienske stationer |

Bemærk: den aktuelle version af P.453 er [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), der kan downloades gratis; i dokumentet brugte jeg de 13, der skal genberegnes.

Allerede i programmanualer og genanvendelige her: LA0BY (Nordic VUSHF 2019), IK3XTV ("Formering på 2 meter" og "Formering VHF - konvektive celler og gravitationelle bølger", Radio Magazine, leveret af programmet forfatter), DF5AI/DK5YA (2003), G3YGF (QST 1983), VHF / UHF DX Book cap. 2, G4MCU (2003) KP4MD
