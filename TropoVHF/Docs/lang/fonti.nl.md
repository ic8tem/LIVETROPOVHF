# Bronnen en formules

> Machinevertaling uit het Italiaans: bij twijfel geldt de Italiaanse handleiding.

Werkverzameling van bronnen geraadpleegd voor de LIVETROPOVHF tropo-voorspelling (20 Basisfysica, beschikbare gegevens, gepubliceerde indices, formules, amateur radio-ervaringen, validatie en beslissingen. De formules zijn genummerd zoals in LaTeX, (1), (2),..., en in de tekst die ze herinneren met het nummer tussen haakjes. De Forecast Manual beschrijft hoe het programma ze gebruikt.


## 1. Gronden van de auteur

IC8TEM waarnemingen die het werk frame, met de feedback gevonden in de bronnen.

- Italiaanse geografie vraagt om diffractie. Met dorsaal, voorgebergte en eilanden enkele tientallen kilometers van de stations, worden diffractiebijdragen berekend met de Fresnel parameter aan de hoofdrand, niet alleen met het gladde land. Bijeenkomst: in een vergelijking op onregelmatig terrein op 563 MHz (Rio de Janeiro, 27 punten tussen 5 en 30 km), waren de beste methoden P.526 met bladranden, P.1812 en de diffractieve wig, met gemiddelde fout tussen −5 en +3 dB en verspreiding van ongeveer 9 dB; P.1546 was de meest pessimistische (van Costa, Rabanal Ramirez en Voor steile hellingen in de buurt van het PETOOL station onthouden dat ze dienen breedhoek propagatoren en de twee-weg berekening (golf weerspiegeld door de reliëfs).
- Om in een verre leiding te komen, heb je Fresnel nodig. Als de leiding voorbij de horizon begint, komt het signaal tot ons door diffractie over de obstakels tot het begin; vanaf 1.3.2 berekent het programma het (Prevision Manual, § 10 ter). Ontmoeting: Rohde & Schwarz (2023) merkt op dat stations langs een hoog kanaal vaak niet binnenkomen, terwijl die dicht bij de uiteinden ja.
- De omstandigheden veranderen snel. De theorieën zijn er, maar de omstandigheden veranderen in minuten, en geen voorspellingssysteem neemt de gegevens in real time: de GFS komt elke 6 uur uit, met een tijdspanne en 0,25°, en een prognose kan waarde verliezen binnen een korte tijd. Meeting: Bray (QST, november 1961) meting van gemiddelde schommelingen per uur van ongeveer ±7 dB over 100 mijl; de kanalen bewegen met atmosferische tijd (Rohde & Schwarz); ons QSO-register van 29/12/2021
- De beste now-casting blijft het DX cluster. Om te weten wat er nu gebeurt, clustervlekken, PSKReporter (en WSPR) verslaan elk patroon: het zijn maten. De voorspelling zegt waar te kijken en hoe lang het kan duren; commercials zeggen of het er is. Het programma hiervoor plaatst spot en voorspelling op dezelfde kaart.

## 2. Doel

Het idee is een derde index, berekend door LIVETROPOVHF zelf vanaf de verticale profielen van een weermodel, naast Hepburn en F5LEN in plaats van vervangen. Vandaag leest het programma hun kaarten al klaar: een index zou ook zeggen *waarom* er is opening (quota en dikte van de geleider, minimale band gevangen) en het kan optreden met de plekken die het programma verzamelt al.

- Wat kun je geven: waarschijnlijkheid van leiding per punt en per uur, deel van de basis en top, dikte, minimale frequentie gevangen, en langs een pad de fractie van de route in een leiding.
- Wat kan niet geven: de dunne kanalen (onder een paar tien meter) die de globale modellen op 0,25° niet oplossen, en de verdampingskanalen op de zee, die vooral dienen vanaf de GHz omhoog.
- Eerste praktische doelstelling: 144 en 432 MHz in Europa, met verificatie op DX clusterspots, PSKReporter en lokale decoderingen.

## 3. Basisfysica

Alles begint bij breking. N berekend op elk profielniveau: de geleider is waar de gewijzigde reactiviteit M het klimmen vermindert, dat is waar dN/dh daalt onder −157 N/km ([ITU-R P.453-13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)).

Refractiviteit (P en in hPa, T in kelvin):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Stoomdruk vanaf relatieve vochtigheid H (%) en temperatuur t (°C), boven water tussen −40 en +50 °C:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

Gemodificeerde reactiviteit (h in km, of 0,157 · z met z in meter) en kanaalcriterium:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN/dh (N/km) | Verordening | Effect van 144 MHz |
| 0 | subbreking | verlaagd debiet |
| van 0 tot en met −79 | normaal (standaard ongeveer −40) | radiohorizon met gelijkwaardige straal 4/3 |
| van −79 tot −157 | superbreking | langwerpige stroom, "te goed" |
| onder −15 | conduct (trapping) | gevangen signalen, DX meer dan 1000 km |

Hoe je N uit een model haalt bij drukniveaus: voor elk niveau neem je T, relatieve vochtigheid en geopotentiaalhoogte, bereken je N en M, dan de gradiënt tussen naburige niveaus. De niveaus onder de grond moeten worden weggegooid (bij 1000 hPa op de Alpen is ondergronds).

Minimale ingesloten frequentie: de leiding leidt alleen de golven vrij kort in vergelijking met de dikte. Hepburn publiceert een tafeldikte → minimale nuttige frequentie ([DX Info Centre](https://www.dxinfocentre.com/propagation/luf.htm)); het is een zachte limiet, waaronder het signaal geleidelijk daalt.

| Leddikte | Minimum te betalen frequentie |
|  61 m  | 962 MHz |
|  91 m  | 464 MHz |
|  122 m  | 276 MHz |
|  152 m  | 185 MHz |
|  183 m  | 133 MHz |
|  213 m  | 101 MHz |
|  305 m  | 53 MHz |

In het VHF-bereik (vaak 120 Zelf aanpassen (geen formule gepubliceerd door Hepburn), met f in MHz en d in meter:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

Kortom, voor de 2 m, is een buis vaak minimaal 170 De formule die op het net circuleert, λmax = 0,014 · d^1,5, komt van Kerr (1951, hoofdstuk 1, eq. 18) en is geldig met λ in centimeters en staand, voor een leiding waarin M lineair valt: met deze eenheden geeft voor ongeveer 2 m 180 m en voor ongeveer 70 cm 89 m, dat is dezelfde waarden van de Hepburn tabel, die afkomstig is van het. Kerr waarschuwt dat het geen netsnede is: zelfs golven meerdere malen langer lijden onder veel van de leiding, en een manier die "verlies" een beetje kan verbeteren ontvangst zelfs met stations uit de leiding. Een IEEE Access-studie van 2019 bevestigt dat klassieke methoden (Kerr, Kukushkin) de echte snit niet beschrijven ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)): de drempel moet worden gebruikt als indicatie, niet als muur.

## 4. Gratis beschikbare gegevens

Het zwakke punt is de verticale resolutie: tussen 1000 en 850 hPa modellen geven een niveau elke 25 LIVETROPOVHF gebruikt Open-Meteo al voor isobare, temperatuur tot 850 hPa en inversie: de index kan vanaf dezelfde oproepen beginnen.

| Bron | Wat geeft | Resolutie | Gebruik voor index |
| [Open-Meteo, GFS](https://open-meteo.com/en/docs/gfs-api) | T, relatieve vochtigheid, dauwpunt, geopotentiaalhoogte naar niveau (1000, 975, 950, 925, 900, 850 hPa...); hoogte van de limietlaag | 0,25° op drukniveaus; 16 dagen, uren tot 120 uur, bijgewerkt om de 6 uur | profiel N en M, helling, basis en bovenkant van het kanaal; reeds in het programma |
| [Open-Meteo, ECMWF](https://open-meteo.com/en/docs/ecmwf-api) | dezelfde niveaus van open data modellen 0,25°; grond geeft dauwpunt |  0,25°  | tweede advies, nuttig voor de overeenstemming tussen modellen |
| [Open-Meteo, DWD ICON](https://open-meteo.com/en/docs/dwd-api) | T en relatieve vochtigheid op drukniveaus; in ICON- D2 zijn er 800, 900 en 925 hPa | ICON-EU ongeveer 7 km | derde advies over Europa |
| [Radiosunds, Univ. Wyoming](https://github.com/NCAR/RAOBget) | gemeten profielen, twee lanceringen per dag (00 en 12 UTC), tekstformaat per station | vele belangrijke niveaus, zelfs om de paar dozijn meter | index verificatie; zie de subtiele kanalen die het model niet ziet |
| [GROANS, NOAA NCII](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive) | historisch archief van omroep, een bestand per station, meer afgeleide parameters | als boven | kalibratie op datajaren en vergelijking met de plekken uit het verleden |
| ERA5 (Copernicus) | uurheranalyse sinds 1940, 37 drukniveaus | ongeveer 28 km | statistieken en kalibratie, niet voorspeld |

- Met Open-Meteo geopotentiaal hoogte boven zeeniveau: de niveaus die onder de grond vallen moeten worden weggegooid ([Open-Meteo, bovenste lucht](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)).
- Op de grond worden toegevoegd T en vochtigheid op 2 m: ze geven het eerste stuk van gradiënt, degene waar de nachtoppervlak kanalen worden geboren op de vlakte en op de zee.
- Italiaanse uitzendingen op de Universiteit van Wyoming archief (weather.uwyo.edu/upperair/sounding.html): stations en nummers WMO in de tabel hieronder.

| Post | W | Breedtegraad | Lengtegraad | Delen |
| Udine/Campoformido |  16044  |  46,03  |  13,18  |  94 m  |
| Milaan/Linaat |  16080  |  45,43  |  9,28  |  103 m  |
| Zeepraktijken |  16245  |  41,65  |  12,43  |  12 m  |
| Brindisi |  16320  |  40,65  |  17,95  |  10 m  |
| Trapani/Birgi |  16429  |  37,92  |  12,50  | ongeveer 14 m |

Het tekstformaat van Wyoming geeft voor elk niveau druk, hoogte, temperatuur, dauwpunt en vochtigheid: genoeg voor N en M. Wyoming heeft geen officiële interface, scripts worden gebruikt als [RAOBget van NCAR](https://github.com/NCAR/RAOBget); het College van DuPage bewaart kopieën in tekst per station ([bijv., 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)).](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. Indexen en gepubliceerde methoden

Hepburn en F5LEN beginnen met dezelfde fysische ingrediënten maar publiceren niet het volledige algoritme; de wetenschappelijke studies van 2008

| Methode | Begingegevens | Wat wordt berekend | Nuttige notities voor ons |
| [Hepburn Tropo Index](https://www.dxinfocentre.com/propagation/hti.htm) | ruwe weermodellen van de Canada Meteorologische Dienst (MSC, zoals geschreven onder de kaarten; niet GFS/NAM), kaarten ontworpen met Grads, eigen algoritme ([storia](https://www.dxinfocentre.com/propagation/history.htm)) | 0 | de schaal te imiteren, zodat de derde index onmiddellijk vergelijkt met de eerste twee |
| [F5LEN, wereldvoorspelling](https://tropo.f5len.org/WW/info.php) | voor Europa het ARPEGE model van Météo-France, 7,5 km, meer niveaus tussen 2 m en 1500 m ([noot van auteur](https://tropo.f5len.org/author/f5len/)) | Gemiddelde index voor verhuurder, 40 stappen vanaf 3 uur, 4 berekeningen per dag, 28 dagen archief ([nieuwe versie](https://tropo.f5len.org/?p=504)) | bevestigen dat het dient voor een model met veel lage niveaus |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf) | 60 ECMWF-modelniveaus, van grond tot 3000 m | minimum dN/dz per kolom: uitgevoerd onder −157 N/km, superbreking onder −78.7 N/km; basis van de vallaag | climatologie: Po Vallei de Europese regio met verschillende pijpleidingen het hele jaar door; Middellandse Zee in de zomer bijna 100%, in de winter minder dan 15%; Italië meer dan 30% op zomernachten |
| [Indermuehle en Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861) | GFS 0,25°, 21 drukniveaus + velden op 2 m | M = N + 0,157 z; minimum dM/dz onder 3 km, basis en top van het kanaal, diepte van het M-tekort; minder dan 40 m verwijderd | 7 jaar gecontroleerd op 25 kanalen van 88 MHz tot 2,68 GHz; goede capaciteit tot ongeveer 3 dagen van tevoren (AUC ongeveer 0,74), daalt dan; GFS beter dan ERA5 |
| [Hämäläinen et al., IMF 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf) | Harmonie-AROME, 750 m of 2,5 km, 65 of 90 niveaus | dM/dh < 0 op het modelprofiel, vergeleken met een bundel van 4 tot 59 m | telt meer verticaal dan horizontale resolutie; geen model ziet de kanalen onder 15 m; vocht domineert op de temperatuurgradiënt |

Derde amateur radio service al actief: [TROPOCAST.EU](https://tropocast.eu/)van Artur SQ3A, voor 2 m en 70 cm in Europa. Van de prognosepagina ([tropocast.eu/forecast](https://tropocast.eu/forecast)):

- Gegevens: DWD ICON-EU (pass 0,0625°, ongeveer 7 km), standaardbron; alternatief Canadese BBPS (0,15°), minder gedetailleerd.
- Methode: N en M volgens ITU-R P.453 van het profiel tussen de laag bij de grond en 850 hPa; de herkenning van de lagen, de verbinding van kanalen aan de grond en hoog en de drempels van de kleuren zijn een goede kalibratie van de auteur, niet gepubliceerd.
- Producten: "volledige" weergave 0
- Gebieden: Europa, Polen, Verenigd Koninkrijk, Duitsland, Frankrijk, Spanje, Italië, Tsjechië, Kroatië; interface ook in het Italiaans.
- Hergebruik: kaarten zijn opgenomen met een API keyless iframe; er is ook een gedocumenteerde API (OpenAPI en JSON schema). De licentie is niet aangegeven: Voordat je het gebruikt als een derde bron naast Hepburn en F5LEN moet je het de auteur vragen.

Verschillen met ons project: TROPOCAST stopt bij 850 hPa (ongeveer 1,5 km), terwijl de verhoogde kanalen komen op 2000 m en verder ( LA0BY geval van 22/11/2018), en geeft geen index voor band of route. Klassen 0

TROPOCAST beeldstructuur (van de link van de auteur van het programma): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, waar n de prognosestap is. De site verbiedt automatische terugtrekking naar robots, dus ik kon het beeld niet lezen; ook hiervoor, voordat deze kaarten in LIVETROPOVHF dient de auteur toestemming.

Notitieboek: [DJ5AR, categorie](https://dj5ar.de/?cat=174)(Magonza, 21 artikelen 2017 Beschreven zaken: verscheidene weken van Ierland (23 mei Herken het geval van het "te hoge" kanaal (omkeerbaar tot 1000 m) dat u niet vanaf de grond kunt gebruiken: een andere bevestiging dat het nodig is om het aandeel van de leiding te vergelijken met dat van de stations. Gebruik radiofares als openingsmaat (tot 1028 km) en de verticale profielen van Parijs, Bordeaux en DWD. FT8

Samengevat, het gemeenschappelijke recept: verticaal profiel → N en M per laag → laag met dM/dh negatief → basis, boven, dikte en kracht van het kanaal → index. Het "geheime" deel van Hepburn is de empirische kalibratie op echte gebeurtenissen, die we kunnen remaken met vlekken.

## 6. Nuttige formules voor het programma

Naast N en M (basic Physics sectie), deze worden gebruikt om te verplaatsen van een verticaal profiel naar een punt nummer en vervolgens door pad. De ITU-formules waren gedeeltelijk slecht gereconstrueerd door een PDF; op 22 november werden de officiële teksten herzien ([ITU-R P.452-18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453-14, P.526-16, P.372-18) en gecorrigeerd in de code: constante van de troposcatter 190 (niet 190,1) plus de absorptie van gassen met ρ = 3 g/m3; α niet onder −3,4 (eq. 55a); gas van de leidingen met ρ = 7,5 + 2,5·ω (eq. 9a); diffractie op volle grond (eq. 23 Rumore P.372-18: tabel 1 en galactisch corresponderen; Kunstmatige ruiscurves zijn tot 250 MHz en de galactische tot ongeveer 100 MHz, over het programma breidt dezelfde wetten uit.

1. Verloop tussen twee niveaus van het model (h in km, M in eenheid M):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. Conduitsterkte en -dikte: dikte d = boven - basis (voor een bodemkanaal, basis = bodem); sterkte als M-deficiëntie tussen bodem en boven.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3. Earth radius equivalent factor (P.452-18, eq. 5 en 6a), met ΔN = gemiddelde N daling in de eerste km, positief, in N/km:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

Met ΔN = 40 heb je ongeveer 4/3 k (standaardatmosfeer); met ΔN nadert 157 k gordijnen oneindig, dat wil zeggen, de aardradio wordt plat: het is het begin van de geleider.

4. Radiohorizon van een hoge antenne h meters, met een straal gelijk aan\_e in km:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5. Waarschijnlijkheid van kanaal voor breedtegraad (P.452-18, eq. 2), percentage van tijd; φ = breedtegraad van het centrum van de route, μ1 en μ4 rekening houdend met hoe ver het over zee is:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

Bij 41° N, alles over zee (μ1 = μ4 = 1), geeft ongeveer 11% van de tijd: is de klimatologische basis, nuttig als referentiewaarde waarvan de verwachte index verschilt.

6. Verlies voor troposcatter (P.452-18, eq. 45; f in MHz, d in km, θ hoek van het pad in ondoordringbaar, N0 grondreactiviteit in het midden van de route):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

De term −0,15 · N0 laat zien waarom natte lucht ook zonder kanaal helpt: 50 extra N eenheden zijn 7,5 dB minder dan verlies. LIVETROPOVHF maakt al gebruik van de G3YGF versie van het scatter verlies.

7. Verlies in kanaal (P.452-18, eq. 46

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. Uit Bean en Dutton (1966) en Kerr (1951)

De twee klassieke teksten geven drie dingen die onmiddellijk nodig zijn aan de index: gradiëntdrempels tareren al op dezelfde niveaus van druk van GFS, wat is de moeite waard in dB een verandering van reactiviteit, en wat weersituaties creëren of vernietigen van de kanalen. *Radio Meteorology*, NBS Monograph 92.

1. Profielclassificatie voor druklagen (Bean en Dutton, tab. 6.5, p. 248; −dN/dh in N/km). Ontworpen voor radio golflengten, is het van toepassing zoals het is op modelniveaus:

| Weg | Aandelen | Subfractie | niet gelamineerd | Superbreking |
| 1000 | 0,16,46 km | −dN/dh < 0 | van 20 tot en met 60 | meer dan 100 |
| 850 | 1,46 | −dN/dh < 0 | van 20 tot en met 50 | > 80 |
| 700 | 3,01 | −dN/dh < 0 | van 15 tot en met 40 | meer dan 70 |
| 600 | 4,20 | −dN/dh < 0 | van 10 tot en met 30 | meer dan 50 |

2. Hoeveel is er waard in dB (Bean en Dutton, cap. 6, pp. 233

- Elke laag (op de grond of op de hoogte) ten opzichte van het "glad" profiel verhoogt het signaal van 10
- De grondreactiviteit Ns en de daling van de eerste km ΔN zijn gerelateerd aan het maandelijkse mediane signaal met een coëfficiënt tussen 0,4 en 0,95, mediaan 0,7 (20 routes van 130 tot 446 km, ongeveer 100 MHz).
- Jaarlijkse cyclus: 0,18 dB per eenheid N; dagelijkse cyclus van 0,2 tot 1,1 dB per eenheid N, maximaal op routes van 175
- De samengestelde parameter van Misme schat de veldvariatie op 0,5 dB/(N/km) · (equivalente gradiënt − 40) plus een term van thermische stabiliteit; de parameter van turbulentie D (gebied tussen het werkelijke profiel en de adiabatische, 0 Conclusie van de auteurs: ΔN blijft de beste parameter voor eenvoud.

3. Referentieprofiel en anomalie (Bean en Dutton, § 3.8 en 3.9). De exponentiële atmosfeer van referentie CRPL geeft het "normale" profiel vanaf Ns; de A-eenheid meet hoeveel het werkelijke profiel per aandeel verschilt. Het is de natuurlijke manier om een GFS profiel om te zetten in een anomalie om op de kaart te zetten:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

Bij Ns = 313 heb je ΔN ongeveer −42 N/km, dat is de equivalente straal 4/3. Het −157 N/km conduit criterium is hier ook afgeleid van de Snell wet voor een horizontale straal (eq. 4.23), bijna onafhankelijk van de antennehoogte.

4. Waar en wanneer de leidingen ontstaan (Bean en Dutton § 4.4; Kerr cap. 3):

| Proces | Situatie | Effect |
| Waarschuwing | warme en droge lucht afkomstig van land dat op koudere zee stroomt (typisch zomersleeve na dagen van goed weer); telt voornamelijk het vochttekort | intense leiding op zee |
| Doorstraling nachtelijk | serene hemel, zwakke wind; lage wolken of veel vochtigheid voorkomen het | leidde tot de grond die wordt geboren bij zonsondergang, groeit 's avonds en verdwijnt snel na zonsopgang, vooral binnen |
| Subsidence | lucht afkomstig van de anticycline, verwarmd en droog, over frisse en vochtige lucht; frequente overstromingen in de bergen en zuidoostelijke sectoren van anticycline | super-refractieve laag of hoge kanaal; bijna altijd gradiënt hoger dan standaard in omgekeerde |
| Zee en land | kust, circulatie van een paar honderd meter | lokale pijpleidingen langs de kust |
| Tegen de leiding | mengen: grond verwarmd door de zon tussen het midden van de ochtend en de middag, wind op de grond over een paar m/s op ruwe grond; Nachtmist | waarschijnlijkheid van leiding bijna niets |

5. Twee nieuwe ingrediënten om te proberen. De verticale beweging: stijgende stromingen in de convergentiezones verwijderen en verwijderen de lagen, de daling versterkt het signaal (Moler en Holden: gemiddeld ongeveer 2,5 dB meer dan met bergopwaartse lucht, op zee in band L en X). Open-Meteo levert al verticale snelheid op drukniveaus. En de druktrend: op een pad van 140 km tot 186 MHz daalde het signaal toen de druk zakte naar de zender en naar het midden van het pad en kwam hoog en zonder evanescence terug toen het omhoog ging, met slechts 2 (Bean en Dutton, fig. 6.9, p. 241).

6. Van Kerr. Naast de formule van de maximale golflengte (base Physics sectie), geeft Kerr een benadering formule voor M van de enige temperatuur, stoom en hoogte (§ 3.9, met de oude constanten 79 en 4800 vervolgens vervangen door 77,6 en 4810) en merkt op dat omgekeerd het snijden van de wind golven kan genereren die schommelen op de hoogte van het kanaal, mogelijke oorzaak van diepe ontsnappingen (§ 3.26). Italiaanse bron gemeld door Bean en Dutton en te zoeken: L. Bonavoglia (december 1958), "Correlation between meteorologische fenomenen and propagation..."

## 8. Uit amateur radiopresentaties en technische rapporten (tweede reeks)

Zeven documenten verstrekt door de auteur van het programma. Drie dienen direct naar de index: W3SZ voor de troposcatter "vloer," LA0BY voor gevallen Hepburn had niet voorspeld, Hepburn ( LU7MC) voor de weersclassificatie van de kanalen.

1. Troposcatter: het signaal is er altijd. R. Rehr W3SZ, "Tropospheric Scatter (Troposcatter) Propagation for VHF, UHF, Magnetron en Frequenties," Pack Rats Conference 2014; J.R. Duffey KK6MC, "The Troposcatter Propagation Mode and How to Predict Coverage on the VHF/UHF Bands," CSVHF 2017.

- Nuttig van ongeveer 100 tot 700 km, van 144 MHz tot 10 GHz; het verlies groeit van 30 logf met de frequentie.
- Bovenal telt het de starthoek naar de horizon: elke extra graad kost ongeveer 9 M1CNK
- In de zomer is het signaal gemiddeld ongeveer 10 dB hoger dan in de winter; midden in de middag ongeveer 5 dB lager dan in de vroege ochtend of 's avonds. KK6MC wijst op een totale variabiliteit van 13
- Yeh methode, de eenvoudigste te programmeren (f in MHz, d in miles, θ hoek van scatter in graden, α geometrisch gemiddelde van de openingen van de twee bundels):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Vergelijking van Larsen (1968) op 15 reële verbindingen: gemiddelde vierkante fout 5,1 dB voor Collins, 7,4 voor Yeh, 8.1 voor NBS en CCIR, 16,8 voor Rider. Alle methoden verbeteren als Ns stabiel blijft (Yeh daalt tot 5,4 dB): voor de verstrooiing telt de breking van de dag weinig, dus de tropo index moet kijken naar de lagen, niet alleen Ns. De tabel met 15 aansluitingen, met gemeten verliezen (waaronder Feldberg.Monte Baldo, 322 km bij 957 MHz, 223,1 dB; Cyprus. Israël, 319 km bij 77,5 MHz, 163,3 dB), is een goede testbank voor onze berekening.
- KK6MC geeft het verlies dat een typisch station erin slaagt om meer dan 2 m en de bijbehorende afstand, van 184 dB (FT-817 en 3 elementen in SSB, ongeveer 130 km) tot 215 dB (lange bungs in SSB, ongeveer 430 km) en 236 dB (kW en yagi gekoppeld, ongeveer 640 km). Het is de manier om de index dBs te vertalen in verdiende kilometers.
- Klaar voor gebruik: W3SZ Yeh en Collins programma (nitehawk.com/w3sz/YehCollins.zip) en Bob Atkins KA1GT rekenmachine (bobatkins.com/radio/troposcatter.html).

Een andere bron op de scatter: I. Roberts ZS6BTE, "Tropospheric Scatter Propagation," *Radio ZS*, december 1984, met nota van 2013 ([qsl.net/zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). Vooral Nuttige geometrie: scatterhoek θ = θ0 − θ1 − θ2, met θ0 = d/R, θ1 = (h1 − h\_ostacolo)/d1 + d1/2 R en hetzelfde voor het andere station, R = 8446 km (4/3 van de aardradius). Het is het stuk dat wordt gebruikt om de hoeken van het profiel van de grond in de troposcatter "vloer" te berekenen. Andere gegevens: elke hoek in meer kosten ongeveer 10 dB; overstappen van Ns 280 naar 300 vermindert het verlies van ongeveer 4 dB; het scattervolume ligt tussen 300 en 2000 m voor 150 km, tussen 600 en 3000 m voor 300 km; onweersbuien in het scattervolume verhogen het signaal van ongeveer 20 dB. het numerieke voorbeeld van het artikel komt niet terug met de gegeven formules (de term vrije ruimte bij 900 MHz en 872 km is ongeveer 150 dB, niet 125), dus de formules moeten worden gecontroleerd op W3SZ alvorens ze te gebruiken.

2. Soorten geleiders (W. R. Hepburn, "Troposphereric Propagation Modes," dxinfocentre.com/propagation/tr-modes.htm, Spaanse vertaling van LU7MC). Vul de tabel van Bean en Dutton met nuttige gevallen in Italië:

| Type | Wanneer | Inhoudsopgave |
| Doorstraling | rustige en rustige nachten op aarde | volg de grond, verdwijnt kort na zonsopgang |
| Hoge druk (onderdak) | anticyclion | kan de hele dag duren; 's nachts kan de leiding van straling beneden verafgelegen signalen blokkeren, zodat overdag beter kan gaan |
| Voorkant | voor een warm front, achter een koud front, ten noorden van een stilstaand front | kort; het slechte weer verstoort hem |
| Bergafdaling | föhn, bora: lucht naar beneden, verwarmen en drogen | Bora op de Adriatische Zee en Föhn ten zuiden van de Alpen |
| Waarschuwing | warme en droge lucht over koude en natte grond, bijvoorbeeld na regen | leidde naar de grond |
| Valley | hete lucht over koude lucht stopt in de vallei | kan blijven in de dag |
| Slovenië | warme en droge lucht op kouder water; komt met de bries aan op de kust | piek in de middag; lente en vroege zomer de beste seizoenen |

De verdampingskanalen op zee (0 De hoge bergen zijn een barrière; de woestijnen zijn te droog.

3. De gevallen Hepburn hadden niet voorspeld (S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up," Nordic VUSHF 2019, van JO59IX tot 500 m hoogte):

- 22 november 2018: 70 cm tot UA6 (ongeveer 2600 km) gedurende 6-8 uur. Hepburn gaf alleen de Baltische staten en Polen aan. De buis werd gezien in de temperatuurkaarten op 900 en 1500 m en eindigde iets meer dan 2000 m; het was hoger aan de Noorse kant, en alleen LA0BY op de heuvel kwam. Op de grond leek de tijd geen trope: kou en mist aan de top.
- 11 februari 2015: 2 m en 70 cm richting 9A en S5 (ongeveer 1500 km) door de Alpen, ook dit niet voorzien door Hepburn.
- 15 oktober 2011: 2 m met EA1 (2135 km) in hoog kanaal, op drie stukken zee en drie grond.
- Andere evenementen: 6
- Veldadressen: mist in vlakte, vrije pieken, weinig wind, natte grond. Normale temperatuurgradiënt −6,5 °C/km; in een echt geval ( VK3KAQ) +7 °C in 20 m.

Conclusie van de auteur: Hepburn en F5LEN zijn niet genoeg, ze dienen de profielen op hoogte langs de route. Voor ons: deze data zijn gevallen van verificatie te worden herberekend met de historische gegevens van de modellen, en de index op het pad moet de quota van het kanaal vergelijken met de quota van de stations.

4. Italiaanse ervaring (G. IV3GBO, "Types of VHF propagation SSB/CW," 2017): uitgevoerd tussen 450 en 3000 km, met lagen tussen 450 en 3000 m; mist uitgebreid als een aanwijzing; openingen over zee aan de Adriatische Zee richting Puglia, Albanië en Griekenland. Verspreiden van 100 tot 500 km.

5. Inleidingen (P. Wilton M1CNK, "An Introduction to VHF/UHF Propagation," 2013): 70 cm zijn vaak beter dan 2 m; met 10 m paal op 144 MHz is de starthoek ongeveer 3°, met 20 m ongeveer 1,5°.

6. NASA rapport (E. Hirschmann, "Tropospheric and Ionospheric Effects on Radio Frequency (VHF-SHF) Communication," Goddard X-731-67-89, 1967): het betreft afstandsfouten aan satellieten, niet de DX. Het brengt dezelfde N-formule en exponentieel profiel terug (bv. in Florida: Ns 370, ΔN −55, constante 0,161/km) en stuurt twee NBS-bronnen om te zoeken: Bean en Thayer, "CRPL Exponential Reference Atmosphere," Monograph 4 (1959), en Bean, Horn en Ozanich, "Climatic Charts and Data of Radio Refractive Index for the United States and the World," Monograph 22 (1960).

## 9. Uit het VHF/UHF DX Boek, Hoofdstuk 2

I. White G3SEK (uitgegeven door), *The VHF/UHF DX Book*, DIR Publishing 1995 (ISBN 0 9520468 0 6; digital replica 2017), hoofdstuk 2 "VHF/UHF Propagation," blz. 2-13, 2-22 Het is de tekst die het dichtst bij de radioamateur ervaring van de verzamelde; bevestigt de reeds gebruikte drempels en voegt nieuwe synoptische regels.

Aantal

- Dezelfde formule van N (77,6 P/T + 3,73·10^5 e/T2); voorbeeld: 1000 hPa, 7 °C en = 10 hPa schade N = 324. In een goed gevormde atmosfeer, bijvoorbeeld na een onweersbui, is N bijna lineair ongeveer 39 eenheden/km in de eerste 2
- Verlopen tussen 0 en −79 N/km zijn normaal, onder −79 is er super-breking, boven 0 sub-breking. Het boek benadrukt dat de lange DX niet afkomstig is van de eenvoudige super-breking, die op het hele pad moet blijven, maar van de kanalen.
- Minimale kanaaldikte, iets voorzichtiger dan Kerr en Hepburn:

| Band | Minimale dikte |
| 50 MHz |  400 m  |
| 70 MHz |  300 m  |
| 144 MHz |  200 m  |
| 432 MHz |  100 m  |
| 1,3 GHz |  50 m  |

- De verhouding is niet evenredig met de golflengte (ongeveer 65 λ bij 50 MHz, 220 λ bij 1,3 GHz) omdat de randen van de geleider "gesplitst" zijn. In het Verenigd Koninkrijk komen de pijpleidingen vaak voor op 144 MHz, maar bijna nooit op 70 MHz of lager, en vaak gaan de 432 MHz sterk wanneer de 144 zwak zijn.
- In de pijplijn daalt het signaal als 1/d in plaats van 1/d2: tot 1000 km kan de demping vergelijkbaar zijn met die van vrije ruimte.
- Een minimale stationcapaciteit op 2 m (100 W, 15 dBi yagi, 2 dB ruis): 217 dB, dat is ongeveer 400 Akkoord met de KK6MC -tabel.
- Scatter: het verlies stijgt met ongeveer 15 dB door de frequentie en 10 dB voor elke graad van scatterhoek te verdrievoudigen (Yeh formule; in de tekst is de constante onleesbaar, dus het moet worden genomen uit de versie van W3SZ). De "up" verstrooiing van convectieve cellen beslaat routinematig tot ongeveer 500 km, 1000 km met goede stations. Na zware regenval en langdurige omstandigheden zijn op alle bands schaars.

Typen kanaal

- Op de grond: op de aarde verspreiden ze zich slecht omdat de grond zich absorbeert en verspreidt; op de kalme zee komt ver (Scilly De verdampingskanalen zijn vaak slechts ongeveer 15 m hoog en dienen magnetrons.
- Hoog: tussen 450 en 2000 m, door de langere DX op de grond; het signaal "hoog" boven de stations hieronder. De "verlies"-kanalen geven minder sterke signalen maar laten ook tussenstations toe.
- De leidingen hebben precieze aanwijzingen en kunnen uren of dagen duren, in tegenstelling tot de sporadische E.

Nieuwe synoptische regels voor index

- Leeftijd van anticycline: de lucht daalt naar het centrum van een paar meter per uur en het omgekeerde wordt verlaagd naarmate de hoge druk zich ontwikkelt; de grotere openingen komen na enkele dagen van voortplanting al goed. Een korte termijn anticyclon is niet genoeg.
- Hoge druk rand: de laag is koepel en aan de randen is lager, zodat de stations beter worden.
- Richting: Er is opgemerkt dat de langere routes lopen langs de isobares of door de dorsals (Flavell G3LTP, 1984), maar er zijn uitstekende verbindingen ook in radiale richting van het centrum. Voorbeeld van 29 maart 1987: vanuit Engeland draaide de opening 180° in twee dagen (Spanje, Frankrijk, Italië, Oostenrijk, Noorwegen) na de verschuiving van de anti-cycline.
- Tegen: dikke isobare (sterke wind), stratocumuliform wolken aan de rand van de laag (beperk de hoogste frequentie).
- Voorkanten: een koude voorkant duwt hete lucht omhoog en creëert een omgekeerde langs de voorkant; de beste richting is langs de voorkant, maar het duurt weinig.
- Seizoen: In het Verenigd Koninkrijk komen anticyclones vaker voor aan het einde van de zomer en het begin van de herfst.
- Visuele aanwijzingen: rook gevangen in een donkere lijn boven de horizon; met hoge druk, laagcumules met basis onder 1000 m wijzen op een grondkanaal, basis boven 1000 m een hoge kanaal.

Referenties geciteerd om te zoeken: R. G. Flavell G3LTP, "Studies of an Extensive Anticyclonic Propagation Event...", *Radio Communication*, februari 1984, p. 128; J. Gannaway G3YGF, "Tropospheric Scatter Propagation," *Radio Communication*, september 1981, p. 710; P. W. Sollom G3BGL, "A Little Flutter W6ZGN

## 10. Een geverifieerde amateur radio model: Biancomano en Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, "Practical Tropospheric Model for VHF/UHF Propagation Prediction," *National Weather Digest* 22:2, juni 1998, blz.](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf)

- Gegevens: radiogolf van 00 en 12 UTC van 47 stations in het oosten van de Verenigde Staten, 1995
- Voor elke enquête: de aanwezigheid en het aandeel van het kanaal, de maximale golflengte die kan leiden (vaak ongeveer 100 keer de golflengte), de maximale gradiënt van de potentiële temperatuur tussen naburige niveaus en tussen één niveau en het tweede boven (voor dikkere lagen). De grondkanalen worden niet gebruikt.
- Eerste regel, persistentie: stabiliteitsindices (verhoogde index, K, totalen-totalen, SWEAT) blijven binnen 3 eenheden van de enquête 12 uur voor, en het drukniveau waar de potentiële temperatuur groeit meer blijft ongeveer hetzelfde. Zo ja: verbeteringsvoorspelling in de komende 12 uur.
- Tweede regel, dynamische controles: limietlaag die niet stijgt, CAPE, variatie van de dikte tot 500 hPa op drie enquêtes, variatie van de helling binnen grenzen.
- Banddrempel: een geleider die 100 cm of meer kan dragen, duidt op opening op 432 MHz, bijna zeker op 222 en vaak zelfs op 144 MHz.
- Ruimtefilter: stations op een zeshoekig raster van ongeveer 400 Bij 5 stations wordt de kans op een vals willekeurig positief verwaarloosbaar.
- Verificatie van de gebeurtenissen in de kolom "De wereld boven 50 MHz" van QST: in 1996 waren 48 gebeurtenissen gepland op 67 (71%); 12 van de 15 grote gebeurtenissen. Nauwkeurigheid tot 70% op de 432 MHz en ongeveer 60% op de 144 MHz bij 12 uur; het biedt bijna het dubbele van de werkelijke gebeurtenissen; 9 fouten van 19 zijn voorspellingen die te laat zijn gekomen, omdat veel verbeteringen in minder dan 12 uur ontstaan.
- Aan de stations verder naar het noorden toegevoegd: wind draaiend met de klok mee klimmen (hete start) als extra conditie.

Vanuit de volledige PDF (aangeleverd door de auteur van het programma) drie meer details. Hun regel "dikte = 100 keer golflengte" geeft 600 m voor 6 m, 200 m voor 2 m, 125 m voor 1,25 m en 70 m voor 70 cm (vijg. 3): voor de 70 cm is minder ernstig dan Kerr en Hepburn (ongeveer 89 m), voor de 2 m valt het samen met het VHF/UHF DX Boek. De wind die met de klok mee draait gaat volgens de auteurs omhoog, vooral omdat het warmere lucht op hoogte brengt, waardoor de val van N met druk groeit; het vocht dat het met zich meebrengt lijkt niet destabiliserend. De bijlage verkrijgt dN/dh in gesloten vorm voor een constante thermische gradiëntatmosfeer met een scherpe sprong van warmtegradiënt en vochtigheid op een bepaald quotum: nuttig als testcase voor onze code. Hun spoor van de helling van de potentiële temperatuur komt van K. H. Jehn, "The use of potential breking index in synoptic scale radio meteorology," *J. Meteorology* 17, 1960, p. 264.

Wat te nemen voor onze index: (1) een termijn van persistentie: vergelijk het profiel met dat van 12 uur eerder, dat in de uurvoorspelling vrij is; (2) het ruimtefilter: een punt telt alleen als de verbetering een gebied van een paar honderd km bestrijkt, dat vals alarm vermindert; (3) stabiliteitsindices, berekend van temperatuur en dauw tot 850, 700 en 500 hPa (Open-Meteo geeft al verhoogde index en CAPE); (4) de wind die draait met de hoogte tussen 1000 en 850 hPa; (5) hun verificatiemethode, wat hetzelfde is als we willen doen met de programmaadvertenties.

Een andere bron in hetzelfde pakket, van weinig nut voor ons: P. K. Rastogi, "Stratosphere and Troposphere (S-T) Studies at Millstone Hill," NASA 1984 (document 19840019069): 440 MHz radar die de turbulentie van heldere lucht tussen 4 en 25 km meet, met dunne lagen gekoppeld aan zwaartekrachtgolven en windsnijden. Het bevestigt dat de turbulentie die de scatter veroorzaakt gelaagd is, maar het gaat om te hoge kansen voor de kanalen.

## 11. Derde reeks bronnen (20 november)

Hoge kanaal historische gevallen. J. Macoun OK1VR, 2 m van de top van Sněžka (1602 m), herfst 1958, artikel van 1959 vertaald in 2006 ([ok2kkw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)). Op 27 maart 1958 werd de eerste Tsjecho-Slowakije aangesloten op 145 MHz ( G5YV, 1220 km) en nog veel meer tussen 1100 en 1250 km, met stabiele signalen na 23. De auteur schrijft het toe aan twee super opgelegde inversies, de bovenste ongeveer 2200 m, met het signaal terug te keren naar de grond alleen waar de onderste laag opgelost; toestand voorkeur door een langzame anticyclon van de Atlantische Oceaan naar het continent. Vanaf huis (Praag) was het maximum 530 km: alleen de stations op hoogte kwamen de leiding binnen. Het is een ander geval van verificatie (zoals LA0BY 2018) en bevestigt dat de index meer dan 2000 m moet zoeken en vergelijken met het aandeel van de stations.

Historische bibliotheken NBS (handig om te zoeken naar klassieke werken, niet naar formules):

- W. Nupen, *Bibliography on Troposferic Propagation of Radio Waves*, NBS Technical Note 304, 1965, 356 pagina's, met index voor onderwerpen (ook "condition forecast") en per land. Italiaanse inzendingen rapporteerden: L. Sacco, "The radio connection in atmosferic super-refractie regime," *High Frequency* 24(6), december 1955 (Italiaans onderzoek: super-breking onder 500 m frequent dag en nacht); S. Silleni, *Annali di Geofisica* 8(1), 1955; J. M. Clara e A. Antinori, prova tra Sardegna e Minorca da 238 a 297 MHz (390 km), *Electrische communicatie* 33(2), 1956. A. F. Clarke (Fort Huachuca, 1956) gaf dagelijks de prognose van de maximale golflengte gevangen voor de volgende ochtend, van vocht, temperatuur, druk en windvoorspellingen.
- R. L. Abbott, * Bibliografie van Tropospheric Radio Wave Scattering*, NBS, materiaal tot januari 1960, ongeveer 670 items besteld per jaar: nuttig voor het troposcatter gedeelte.

Onthulling en observaties: F. W. Odenthal DK5EQ, "Ausbreitung 2m Discussie over VHF jachthaven en AIS ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)): scope 1,23·√h(ft) nautische mijlen per antenne, verstrooi tot ongeveer 50 nm, typische tre-verbetering 100 Chelmsford ARS, december 1983: in de 2 m wedstrijd worden de stations van de oostkust van Engeland door de zeeroute naar het continent begunstigd. DM2HB DK5YA VK3DXE EI7GL

ARRL, *The Radio Amateur's VHF Manual*, 1972, cap. 2 "Propagation Beyond the Horizon" (pp. 17 Reeds bekende werkpunten (volg hoge drukgebieden die langzaam en vooral hun rugranden bewegen; begin herfst de beste periode; kustgebieden, grote meren en dalen van de rivieren die door stratificatie worden begunstigd). Twee nieuwe gegevens: (1) fig. 2-4 vergelijkt de standaardatmosfeer (relatieve luchtvochtigheid 70% tot 12000 voet, minimale refractie) met een typische straal van sterke refractie, met een duidelijke temperatuurbreuk en mixverhouding van ongeveer 4000 voet (ongeveer 1200 m): het is de handtekening die onze berekening moet herkennen op de GFS-niveaus; (2) echte kanalen zijn zeldzaam onder 144 MHz en het meest frequent in UHF, volgens de minimale diktes. Nuttige QST bibliografie om te zoeken: Hull, "Air-Mass Conditions and the Bending of UHF Waves" (juni 1935, mei 1937); Hoisington, "Painless Prediction of Two-Meter Openings" (oktober 1949); Collier, "Upper-Air Conditions for 2-Meter DX" (september 1955); Botts, "A Night to Remember" (januari 1970, uitgevoerd op land).

PA0EHG, pagina "Tropo" ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): samenvatting van de troposcatter uit het G3YGF artikel, al in de handleiding. Gebruik twee praktische regels om onze scatter berekening te controleren: ongeveer 10 dB extra verlies voor elke hoek aan de horizon van elk station, en meer dan 100 km ongeveer 9 dB plus elke 100 km route (60 mijl); het gemeenschappelijke volume begint vanaf ongeveer 600 m op 100 km en vanaf ongeveer 9 km op 480 km. Het behandelt geen kanalen, polls of brekingsdrempels.

PA0EHG, PROPBER calculator ([page](https://pa0ehg.com/propagat1.htm), propber.exe programma verstrekt door de auteur van het programma). Het is een 36 kB DOS programma geschreven in BASIC en gecompileerd met de Microsoft BASIC Compiler 5.60 (1985): het draait alleen onder DOS of DOSBox. Uit de interne teksten: vraagt om frequentie, afstand (of de twee locators, ook in het oude QRA-formaat "E-locator"), voor elk antennehoogtestation, ruisfiguur, FI-band, macht, een obstakel (hoogte en afstand) en antennewinst (0 = gelijke diameterberekening, efficiëntie 50%). Geeft de maximale afstand in optiek, het juiste optische verlies voor de absorptie van stoom (0,1 dB/km van 24 GHz), het verlies van troposcatter "correct voor de locaties" (d.w.z. voor de horizonhoeken die door obstakels worden gegeven) en de S/N-verhouding in de twee zintuigen, ook "met de maximale abnormale voortplanting." Numerieke constanten zijn niet leesbaar in het gecompileerde uitvoerbare bestand. De voorgestelde methode voor hoge banden is ook nuttig voor ons: het kalibreren van de berekening op een gemeten link (verschil tussen gemeten en berekende S/N) en het toepassen van hetzelfde verschil op een andere band. Het is hetzelfde patroon van onze berekening, maar het gebruikt het S RTM1 -profiel in plaats van slechts één obstakel en de GFS-voorspelling in plaats van een vaste marge.

PROPBER, constanten gelezen in het uitvoerbare bestand (nummers in Microsoft Binary Format formaat). πre, in de gegevenstabel naast de teksten: 8.68589 (= 20/ln 10, d.w.z. 20 logs berekend met natuurlijke logaritme), 32.5 (vrije ruimte: 32.5 + 20 log f\[MHz\] + 20 log d\[km\]), 24000 en 0,1 plus 40000 en 0,3 (vapore: 0,1 dB/km van 24 GHz, 0,3 dB/km van 40 GHz) Waarschijnlijk, omdat je maar in de code herhaalt: −69,5 (dezelfde constante term als Yeh's side-scatter formule in VHF/UHF DX Book), 332 (ground breking), 8733 km (equivalente straal, k ≈ 1.37). De exacte scatter formule is niet reconstructable zonder demonteren van het programma. T0

PROPBER, formules gereconstrueerd door de code (demontage van het gecompileerde deel; de bewerkingen worden opgeroepen naar de BASIC bibliotheek met de operators in het geheugen, waarvan de waarden in dubbele precisie zijn gedecodeerd). De constanten −69,5, 332 en 8733 die eerder werden gevonden waren valse positieven binnen de bibliotheek.

- Optische afstand: d = 5 · (√h1 + √h2) km (h in m). Het is genereuzer dan de klassieke 4,12 (k = 4/3): is gelijk aan k ≈ 2.
- Optiekverlies: L = 32.5 + 20 log f\[MHz\] + 20 log d\[km\], plus 0,1 dB/km boven 24 GHz en 0,3 dB/km boven 40 GHz.
- Troposcatter: Lts = L + 23 + 10 log f + 0,0674·d, d.w.z. 55,5 + 30 log f + 20 log d + 0,0674·d (formule G3YGF met de constante 2 dB hoger). Geen hoekterm of reactiviteit.
- Correctie "voor de sites": het programma vraagt hindernis en afstand, maar het blok dat ze moet gebruiken is leeg; de correctie variabele blijft 0, dus het "correcte" verlies is gelijk aan de scatter.
- Geluid: T = 290·(10^(NF/10) − 1), N = −228,6 + 10 log T + 10 log B (dBW), zonder antennetemperatuur.
- S/N = 10 logs P + G\_tx − L + G\_rx − N. "Met maximale abnormale voortplanting" gebruikt optisch verlies in plaats van scatter, d.w.z. de ideale geleidingskast zonder verliezen buiten vrije ruimte.

Vergelijking: 300 km en 144 MHz PROPBER geeft ongeveer 189,9 dB, G3YGF 187,9, onze P.452 187,8 (met nul horizonhoeken): de scatter valt samen met 2 dB. Onze berekening voegt de horizon hoeken van het profiel, de antenne temperatuur, en in plaats van de "maximum" vast de verwachte laag met restparing en demping.

Rohde & Schwarz, P. Denisowski, "Understanding VHF Propagation," Educational Note, april 2023 (24 pagina's, verspreiding). Bevestigt de reeds verzamelde punten (geleid als een tweedimensionale golfgids, signaal dat lineair daalt met de afstand, tot 1500 km en verder, duur van uren tot dagen; goede grond op de zee en schaars op het land; verhoogd van een paar honderd meter naar een paar km). Twee nuttige waarnemingen voor de index: (1) evolutie in de tijd: een nieuw gevormde leiding draagt vaak alleen de hoogste frequenties en, verdikking, begint zelfs de laagste te brengen; de uurvoorspelling moet daarom de 432 MHz "vooraf" op de 144 MHz tonen wanneer de laag groeit; (2) stations locatie: de beste omstandigheden zijn tussen stations dicht bij de uiteinden van de leiding, terwijl die langs het pad van een hoge leiding vaak niet binnenkomen, tenzij uitgevoerd "die verliezen." De kanalen bewegen met atmosferische weersomstandigheden.

Site PA0EHG, andere pagina's. Voor de trope, PathProfile van M. Willis G0MJW ([scheda](https://pa0ehg.com/srtmp.htm), [download](http://www.mike-willis.com/Software/ASTERPathprofile.zip)): profiel van de route van verwachte totale verlies en quota bestanden, betrouwbaar beoordeeld voor de troposcatter in magnetron; behandelt de verstrooiing van vliegtuigen of regen niet. Nuttig als tweede referentie om onze S RTM1 profielen en verliezen te vergelijken. De rest van de site omvat EME, magnetron, landingsbaan (AirScout, SM7LCB kaarten) en meet 80 m.

PathProfile by G0MJW (Mike Willis), Aster en SRTM pakketten en SiteData (geleverd door de auteur van het programma; [](https://www.mike-willis.com/software.html)software pagina). Windows-programma van 2018: profiel van ASTER of SRTM tot 1" of 3," verlies in optica, diffractie, troposcatter (geldig tot 1000 km), kanaal en gasvormig, met percentage van de tijd, mediaan van de route, fractie van het pad op de zee en afstand van de kust: de structuur is die van P.452/P.1812, maar de hulp (gecomprimeerd.chm bestand) kon hier niet worden gelezen. SiteData converteert de locators en geeft het aandeel van GT OPO30.

Het meest nuttige deel is de gegevensbestanden opgenomen: ITU-R P.453 statistische kaarten op globaal raster van 1,5° (121 rijen van 90°N tot 90°S, 241 kolommen van 0° tot oost): helling dN/dz in de eerste 65 m overschreden voor 1, 10, 50, 90 en 99% van de tijd; voor bodemkanalen percentage van aanwezigheid, dikte en intensiteit; voor die hoge aanwezigheid, basis aandeel, dikte, intensiteit en maximumhoogte; plus de natte termijn en temperatuur. Geïnterpoleerde jaarwaarden:

| Locatie | Landpijpleiding: % / dikte / intensiteit | Hoge pijpleiding: % / basis / dikte / intensiteit | dN/dz meer dan 10% van de tijd |
| Napels |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | −380 N/km |
| Tyrreens (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | −674 N/km |
| Trapani |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | −520 N/km |
| Brindisi |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | −405 N/km |
| Adriatische Zee (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | −399 N/km |
| Milaan |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | −172 N/km |
| Udine |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | −176 N/km |

Voor onze index zijn een kant-en-klare klimatologische referentie: dikte en intensiteit typisch van de kanalen in plaats van de 50 m vaste van de potentiële kanaal, en een anomalie "vandaag in vergelijking met normaal" per zone. ITU-gegevens: gratis voor niet-commercieel gebruik, te controleren alvorens ze in een openbare versie op te nemen. Het ZO NES01.DEM bestand (58 MB, 2' raster) bevat de klimaatzones binnenland/kust/zee; het formaat is niet gedocumenteerd en ik heb het niet gedecodeerd. De Willis vergelijkingen pagina bevat alleen fysieke constanten, vrije ruimte en antenne gain.

Vierde bronronde (20 november).

Indermuehle en Suzuki 2026, voorspelling van de GFS pijpleidingen voor het CSIRO observatorium van Murchison (arXiv 2608.12861): het is de dichtstbijzijnde bron van LIVETROPOVHF. Profielen van N van de GFS tot 0,25° (oppervlakte tot 2 m, dan de isobarische niveaus, die onder de afgedankte grond); voor elke minimumkolom dM/dz onder 3 km, basis en bovenkant van de leiding, geïntegreerd M-tekort; dan P.452-16 en P.1812-6 met GLO-90 25 lengtes van 98 tot 1005 km, van 88 MHz tot 2,68 GHz, elk uur. Resultaten: Mediane AUC 0,72 op 25 kanalen; het P.452 p tijdpercentage moet worden gebruikt als kalibratieconstante (keuze p = 20%), omdat het ITU-model met een instant ΔN niet binnen een geleider wordt gedefinieerd; de vaardigheid blijft bijna vlak tot 3 dagen van tevoren. Bij 162 MHz (AIS) hebben de ontvangen routes de hoogste geleidingsfractie van 67% (0,26 versus 0,16) en de hoogste kanaaldiepte van 49% (28 versus 19 M) met betrekking tot controles: op zee is de leiding vrijwel altijd, beslist het stuk land. De GFS ziet de helft van de tijd op de grond in vergelijking met ERA5 (4,5% versus 10,4% van de cellen-uren), terwijl op zee meer (73% versus 56%). Een "corridor" index (continue pijpleidingen langs de route, lagen met een minimale dikte van 40 m) verbetert het resultaat: bevestigt dat continuïteit langs de route het juiste criterium is.

Longley and Rice, ESSA ERL 79-ITS 67 (1968): methode voor onregelmatig terrein van 20 MHz tot 20 GHz (na ITG), met bodemonregelmatigheden Δh, Ns van 250 tot 400, gelijkwaardige straal 8497 km (4/3), horizonten, diffractie, verstrooiing naar voren en variabiliteit in tijd en op plaatsen voor klimaat. Statistische referentie, niet onmiddellijk.

ITU-R P.834-2: straalkromming, aardradius equivalent k·a, M = N + h/a (h in m, in duizenden km), exponentieel model; niets nieuws dan wat reeds gebruikt.

AGARD CP-70 (1970), troposferische voortplanting: dunne lagen van 1 tot 100 m met sterke gradiënten komen vaak voor; de reflectiecoëfficiënt van een hoge laag is ongeveer evenredig met de golflengte, met een willekeurige spreiding van ongeveer 14 dB. Metingen op 90 MHz: daling van 9 De doorgang van een koude front elimineert de lagen: De diffractie blijft bestaan.

U.S. URSI Report, Commission 2 (1960): gemiddelde dikte van de hoge lagen van 60 tot 210 m (gegeven in overeenstemming met P.453), minimumgradiënt voor 157

Buscemi K2OVS, VHF propagation guide: typische afstanden voor omkering en kanaal voor band, kanalen gekoppeld aan hoge stationaire druk, maximum bij zonsopgang.

Wolinsky-Mancini en 2024, AIS op de Canarische Eilanden: AIS radiosundaties en recepties; de kanalen komen veel vaker voor in de warme maanden (inversie van de aliseanen).

Nigeriaanse en Maleisische artikelen (Ekah, Iloke, Mat, Igwe): lokale correlaties tussen temperatuur, vochtigheid en gemeten verlies, zwak en tegenstrijdig (r −0,09 tot 0,27); alleen nuttig als bevestiging dat de grondgroottes alleen niet voldoende zijn.

Herben 1984, proefschrift TU Eindhoven (microonde): 30 GHz op 8,2 km en OTS satelliet op 11,6 GHz; schitteren voor turbulentie, multi-path en regen. Voor microgolven worden dynamische verliezen gedomineerd door regen en turbulentie, niet door kanalen.

Baars, The Paraboloidal Reflector Antenna (microonde): winst van een gelijkenis G = η·(πD/λ)2 met een typische efficiëntie 0.5 Het zou helpen als we de gelijkenissen toevoegen als alternatief voor Yagi DL6WU voor 1296 MHz en verder.

Reeds geanalyseerd of van slechte inhoud: Hirschmann NASA X-731-67-89 (dubbel), Reid's VHF ST radar (winden en turbulentie, ongeleid), Scribd dia en generieke notities over de troposfeer.

Vijfde bronronde (20 november, avond).

Rogers 1997, Radio Science 32(1): Bayesiaanse schatting van de parameters van een hoge kanaal (basissnelheid, M-tekort, dikte) van voortplantingsfactoren gemeten buiten de horizon, met een parabolische vergelijking model; VOCAR experiment in Californië op 143, 263 en 375 MHz, radiogolf vergelijking. Het signaal is sterk afhankelijk van de basis en het M-tekort (correctie van 0,81 tot 143 MHz voor het tekort), bijna geheel van de dikte (−0,17): de dikte kan niet worden verkregen uit het signaal. Voor ons: berekening B (klimaatdikte) telt minder dan tekort en aandeel; en de methode is de basis voor het backoffice, dat wil zeggen om de leiding van de meerdere frequenties te krijgen.

Moler and Holden 1960, "Troposferic scatter propagation and atmosferic circulations," J. Res. NBS 64D, blz. 81 en volgende (lees in origineel op 22/11/2026, doi 10.6028/jres.064d.014). Californische zeeroutes naar Point Loma (San Diego): Santa Barbara 190 mijl, Point Mugu 144, San Clemente 78; 1365 MHz en 9380 MHz, niet VHF; januari jl. 1958, gemiddeld 1 minuut. Het geval van 26 maart: een versterking van ongeveer 20 dB gedurende ongeveer 3 uur tegen de middag, wanneer een cyclonische vortex verzwakt en begint zeewind en daling; bij 15 wordt de convergentie hervormd en het signaal daalt ongeveer 22 dB van het maximum. Statistieken (tabel 1, 500 hPa, 33 gevallen): met lucht die daalt het gemiddelde signaal is hoger dan ongeveer 2,5 dB dan met lucht stijgen, in band L en X. Het signaal volgt het verschil berekend tot 500 hPa in het teken en de amplitude van de variaties, zonder correlatiecoëfficiënt. Lage lagen (onder ongeveer 600 m) zijn voorstander van gedeeltelijke reflectie bij verstrooiing. De anticyclones en fronten worden niet behandeld, en de "excursies tot 60 dB" hierboven zijn niet: ze kwamen uit een tweedehands samenvatting. Voor ons: de verticale beweging is een goede voorspeller, en +1,5 dB voor de daling en −2 dB voor de stijgende lucht zijn van dezelfde orde als het gemiddelde gemeten verschil (2,5 dB); 20 dBs zijn een enkel geval, niet een typische waarde, en op magnetron.

Banafaa en Muqaibel 2025, IEEE Access: overzicht van de pijpleidingen (verdampingsmodellen, parabolische vergelijking, radarschattingen, GNSS en lidar) en classificatie met automatische leren over echte maatregelen; ondersteuning vector machines geven de beste resultaten. Nuttig als bibliografie; de gegevens zijn van de Perzische Golf.

ITU-R P.834-7 (2015): bijgewerkte versie van de aanbeveling inzake tropos wilde refractie; geen nieuwe formule dan de reeds gebruikte formule.

Abbott, NBS bibliografie over de troposferous scatter (GOVPUB- C13): al in de derde ronde. Slide Scribd op de kanalen: generieke inhoud. Het artikel ScienceDirect 00219169887900304 kon niet worden geopend.

Zesde ronde (20 november, nacht): Bean and Dutton, ARRL Handboek 2022 en 2023, Balanis.

Bean and Dutton, Radio Meteorology (cap. 6) delen nog niet gebruikt. Veldgevoeligheid voor Ns: 0,18 dB per eenheid N op de jaarlijkse cyclus; 0,14 Parameter bestaande uit Misme: M = a(g\_e − 40 + b·ΔW^n) met a = 0,5 dB per N/km, die de equivalente gradiënt en de thermische stabiliteit ΔW combineert (arbeid om de lucht van 1 km te tillen): in de Sahara bij 471 MHz op 160 km is de enige stabiliteit 20 dB tussen januari en juni bij bijna gelijke helling. potentiële refractie-index K (N gemeld adiabatisch tot 1000 hPa): benadrukt de luchtmassa's en de anticyclonische daling (Flavell en Lane).

Handboek ARRL 2022 (99a ed.), § 19.4.3 Nuttige inversies tussen 250 en 2000 m; inversies van straling (sera, kalme lucht, droge grond) leiden zelden waar; langzame anticyclones geven de meest uitgebreide kanalen voor daling, met inversies tussen 500 en 3000 m, sterker 's avonds en 's morgens; in het noordelijke halfrond de beste routes zijn ten zuiden van het hoge centrum. Hete front: goede omstandigheden een paar honderd km vooruit; Koud front: sterk maar onstabiel achteruit achter de voorkant, parallel. Foehn (ook in de Alpen) en aardwind (koele lucht op de grond, warm op 200 Scirocco: warme en droge lucht uit Sahara over koelere zeelucht, zeer sterke inversies van de oostelijke Middellandse Zee naar Gibraltar, waarschijnlijke oorzaak van records over 1500 km in magnetron. Troposcatter: theoretische limiet ongeveer 800 km, maximum typische helft. De 100ste editie (2023) heeft een kortere tekst: inversies bij zonsopgang en zonsondergang tot +20 dB en +20

Balanis, Antenna Theorie (4a ed.), § 10.3.3: Viezbicke tabel (NBS TN 688, 1976) voor Yagi geoptimaliseerd op 400 MHz: boom 0,4 / 0,8 / 1,2 / 2,2 / 3.2 / 4.2 λ → 7,1 / 9,2 / 10,2 / 12,25 / 13,4 / 14,2 dBd. Onze DL6WU formule (8.6 + 8 log10 boom/λ) geeft 1.7 dB minder tussen 0,8 en 4,2 λ en geeft terug in lijn met de DL6WU tabellen tot 9,5 λ: voorzichtige schatting. Met de juiste formule op het origineel (9,4 + 7,8 log10 boom/λ, vanaf 22 november) daalt de teruggooi onder 0,2 dB tussen 1,2 en 4,2 λ (12,1 dBd tot 2,2 λ versus 12,25) en blijft 0,6 dB tot 0,8 λ.

Zevende ronde (21 november): QST, QEX, W3SZ, GitHub repository.

QST 2000 Alleen rapporten in de sectie The World Above 50 MHz, nuttig als gevallen om het model te valideren: 5 QST November 2022 beveelt Hepburn kaarten. Geen technisch artikel over tropo-voorspelling. FT8 VE4MA N0OY Q65

QEX en K1JT, Franke en Taylor (QEX 2020) op FT4/FT8 modi: FT8 decoderen drempels −20,8 dB in 2500 Hz op kanaal zonder evanescence, −18,5 dB met evanescence op gemiddelde breedtegraden; FT4 −17,5 dB. De andere nummers van QEX 2023 en 2026 behandelen de tropo niet (drie nummers 2023 zijn ongelezen scans). W3SZ 2019: slow mode drempels ( JT4A −23, JT9A −27, JT65A −25, QRA64A −26, FT8 −21 dB) en keuzecriteria tussen gevoeligheid en frequentieverschuivingstolerantie.

GitHub. TropoLink (LukiBox, Apache-2.0): ontwerp van troposcatterroutes met P.617-5 (met N0 en ΔN), NBS TN101 en ITG geflankeerd, en hun verschil als onzekerheid aangetoond; P.617-5: L = F + 22 log f + 35 log θ + 17 log d + Lc, F = 0,18· N0 ·e^(−hs/7,35) − 0,23·ΔN, Lc = 0,07·e^(0,055(Gt+Gr)). andi-f/troposcatter ( DL2FZN 2013, from VHF/UHF Handbook RSGB): Lts = 24,44 + 4.283 In f + 0,06656 d boven vrije ruimte, helling bij PROPBER (0,0674 dB/km). QRadioPredict: ITG en ITWOM 3.0 met S RTM3, bodemgebruik Corins en NEC-diagrammen. Niet relevant: Tropos (Weerapp), opera-adt/tropo en troposim (troposferic delay voor InSAR), tgbot\_troposcatter (militair station), sdr-troposcatter-gui (controlepaneel), Troposcatter-Propagation-Delay ( ERA5 delay), Radio-Communicatie-Project (didactische simulator), PyLayers (kanaal).

Achtste ronde (21 november): NTIA en ITU modellen op GitHub. NTIA/itm (Longley-Rice, ITS): 20 MHz Het modeleert de kanalen niet: het dient als referentie "normale atmosfeer" met statistische variabiliteit. NTIA/p528 en eeveetza/p528 (MATLAB): ITU-R P.528-5 voor luchtverbindingen, 100 MHz/30 GHz, 1.5 Mogelijk gebruik: balans tussen luchtscatter (twee grondroutes en vliegtuigen plus radarsectie). Eeveetza/p2001 (MATLAB): ITU-R P.2001-6, all-field model met diffractie, troposcatter, abnormale voortplanting (gedrag en lagen, Lba) en E sporadisch, percentage van de tijd 0 Mogelijk gebruik als ITU-vergelijking in plaats van P.452, met de GFS ΔN in plaats van kaarten zoals in de CSIRO-studie; de kaarten moeten door de gebruiker worden gedownload volgens P.453. Validatie 1.3.0 van 593 QSO (29/12/2021 N0

Negende ronde (21 november): IU3QEZ, NTIA, PyWaveProp, ACE High, validatie 1.3.1. IU3QEZ (S. Fabris), presentatie "Over de horizon in VHF" (GEKO Magazine n. 69, september 2026): gemeenschappelijk volume d2/(8·R\_eff) met R\_eff 8500 km (1,3 km d 300 km, 3,7 km tot 500 km elk niveau van horizon ter waarde van 69 mijl (111 km), marge van 7 dB voor evanescence en 4 dB voor reflectie vanaf de grond; thesis: 10 dB is ongeveer 100 km; station IQ3QC 5 W met 3×5 elementen, 120 QSO, maximaal 788 km (Alpe Adria 2026). Bibliografie van de te recupereren presentatie: Norton 1959 (J. Res. NBS 63D, System Loss), Bray 1961, ARRL Antenna Book 2023 fig. 4.29, Rice-Longley-Norton-Barsis NBS TN 101, Bean and Thayer 1959, Barsis and Kirby 1961 (diffractie op bergkam), Deygout 1966, P.1812, AirScout DL2ALF, P.834 NTIA PropLib (wiki): eHata, ITTM, LFMF, P.528, P.2108 (verlies uit bulk), met C++,.NET, MATLAB en Python; geen doorlopende model; ITS-pagina: ook IF-77 (air PyWaveProp (Python): Paraboolvergelijkingsmethoden met Padé benaderingswijzen, meervoudige randdiffractie, PETOOL en RAM behuizingen, troposfereuze en hoge kanalen, onregelmatige bodem: mogelijke basis voor een veldberekening met het GFS M(z) profiel, te controleren. APRS PropView (Apache 2.0): VHF propagatiekaarten uit APRS-pakketten, uurstatistieken en alarmen openen; mogelijke bron van 144 MHz-gegevens voor validatie. NATO troposcatternet in Italië (fortifications.net): 900 MHz, 10 kW, 20 m parabels; stations waaronder Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Vulture, Monte Lauro; historische testcase voor P.617. Niet relevant: Gupta 2021 (stratosferisch vervoer), wet 2023 (arctisch ozon), dolfijn #590 en opera-adt/tropo (InSAR vertraging), tropo.ino (Arduino gassensoren), tropo-webapi (telefonie), cloudtools/troposfeer (AWS). Validatie 1.3.1 op 564 QSO tropo: AUC 0,79 (model 1.2), 0,80 (+ zacht kanaal), 0,90 (+ misme en daling), 0,90 (+ persistentie); conduitkleuren (≥ 16 dB) op 46% van de openingsroutes en 0% van vergelijking.

Toegevoegd aan dezelfde ronde. mikelytaev/golf-propagatie (MIT, Python 3): Paraboolvergelijking met benadering van Padé en Numerov, onregelmatige bodem, troposferische kanalen; laad de gemodificeerde brekings M(quota, lat, lon) uit de NOAA GFS en stel een verdampingsleiding van Monin-Obukhov in; geen gedocumenteerde functie die het verlies op het pad terugbrengt. Hij is de beste kandidaat voor een toekomstige veldberekening met het GFS M profiel (het zou een externe dienst of een vertaling dienen). RP-Sim (AGPL-3.0, JavaScript): Empirische modellen voor radionetwerken (Hata, He, Ericsson, twee stralen), geen tropo. RIMFading (GPL-3.0, OMNeT++): stroomonregelmatigheden voor netwerksimulaties, Weibull distributie, geen tropo. PyLayers: binnen radiokanaal, al uitgesloten. qradiopredict (kantoon, GPL v2): ITG en ITWOM v3.0 met S RTM3 en Corine, al in de zevende ronde als QRadioPredict.

Tiende ronde (21 november): Antenneboek, Norton, Barsis, nieuwe gegevensbronnen. ARRL Antenna Book (23a ed.), cap. 4 ( K9LA): Bray-methode (QST november 1961) voor het VHF-bereik: glad aardverlies bij 99% (300 mijl, 144 MHz: 214 dB), bochten bij 50% en 99% (vijg. 4.11): 195 dB ter waarde van 100 mijl tot 99% en meer dan 250 mijl tot 50%; evanescence ±7 dB over 100 mijl (−3,5 dB tot 50 mijl), SSB 3 dB slechter dan CW, +4 dB grondreflectie, +3 dB voor koppeling van twee antennes; hoogtewinst (15 m: 4/3/2 dB bij 10/50/100 mijl; 24 m: 8/6/4 dB); Yagi-winst ongeveer 10 keer de boom in λ (dBi); 50 W-station met gemiddelde antenne: ongeveer 180 dB, 100 mijl, +10 dB tot 250 mijl. Zonsopgang en zonsondergang tot +20 dB en +20 Verificatie van onze troposcatter: P.452 tot 50% bij 483 km en 144 MHz geeft ongeveer 204 dB, consistent met de 214 dB van Bray tot 99%. Norton 1959 (J. Res. NBS 63D, vrij op NIST): basisverlies, systeem- en padantenne winst; Referentieatmosfeer Ns = 301; equivalente straal van Ns (250 → 4878 mijl, 301 → 5280, 400 → 6997); L(99) tabellen met antennes van 30 voet (100 MHz: 163 dB tot 100 mijl, 208 dB tot 500 mijl; 1000 MHz: 138 en 188 dB met gelijkenissen van 28 voet); variabiliteit op lange termijn V(p, θ) afhankelijk van hoekafstand (vijg. 13). Rice, Longley and Norton, NBS TN 15 (1959), vrij op NIST. Barsis en Kirby, NBS Report 6751 (1961): 223 km in Colorado met de Pikes Peak (4292 m) als rand, 100 en 751 MHz; consistent verlies met de afgeronde rand (ongeveer 11,5 km radius); lange termijn evanescence 11.714,3 dB versus 39 ARRL VHF Digital Handbook: pakket, APRS, D-STAR, WSJT (Scatter Method en EME), geen onderdeel van de tropo. Nieuwe bewezen of doorzochte gegevensbronnen: ECMWF open data (IFS 0,25°, niveaus 1000, 925, 850, 700 hPa..., zonder 975, 950, 900 en 800; G RIB2 met CCSDS compressie, dat onze lezer niet decodeert; reeds beschikbaar via Open-Meteo); DWD ICON-EU open data (G RIB2 gecomprimeerd bz2, een bestand voor variabele en niveau: vele verzoeken); Canada BBPS op MSC Datamart en GeoMet (ongeveer 15 km, tot 33 niveaus; GeoMet staat het gewas van het gebied; het is de basis van Hepburn kaarten); GFS NOAA archief op AWS vanaf februari 2021 (globale bestanden, nuttig voor validatie); APRS PropView alleen voor validatie.

Elfde ronde (21 november): Longley-Rice, GEKO 69. Bean and Thayer, "Models of the Atmospheric Radio Refractive Index," Proc. IRE May 1959 (blz. 740 e.v.): het model 4/3 heeft de juiste helling in de eerste km maar valt te hoog; 30% van de kromming van een straal komt voor in de eerste 100 m en 60% in de eerste km; gemiddelde verhouding tussen Ns en afname in de eerste km: −ΔN = 7,32·e^(0,005577·Ns) (Ns = 313 → ΔN ongeveer −42). Bean and Thayer, "C.R.P.L. Exponential Reference Atmosphere," J. Res. NBS 63D n. 3 (1959): N(h) = Ns·e^(−c·h) met c gefixeerd door Ns via hetzelfde rapport; Gemiddelde Ns USA 313; beam tracking tables (totale curve 15.2 met exponentieel model versus 30,3 mrad met Aarde 4/3). MATLAB refractieexp: c = 0,143859 km−1 voor Ns = 313 (schaal ongeveer 6,95 km), geldig boven ongeveer 1 km en 10 mrad. Gebruik voor ons: het "normale" vergelijkingsprofiel kan zijn dat CRPL met de lokale Ns in plaats van M = 315 + 0,118·z vast, en de anomalie van de daling in de eerste km moet worden gemeten ten opzichte van −7,32·e^(0,005577·Ns) in plaats van −40 vast. NTIA-rapport 82-100 (Hufford, Longley, Kissick 1982), ITG-gids in gebiedsmodus: Climi en Ns voorgesteld (equatoriaal 360, subtropisch continentaal 320, subtropisch marine 370, woestijn 280, gematigd continentaal 301, gematigde marine op land 320, gematigde marine op zee 350); Ns = N0 ·e^(−hs/9,46 km); u gebruikt het maandelijkse minimum van Ns omdat minder beïnvloed door super-breking en sub-breking; Ns = 301 komt overeen met ae = 8497 km; minder dan 100 km Ns telt weinig. Rice, Longley and Norton, NBS TN 15 (1959), deel I: prognoseformule van verdeling over de tijd over land en troposferische golf, basis van TN 101. SoftWright, Wikipedia, Altair WinProp en MATLAB rfprop. LongleyRice: dezelfde informatie (versie 1.2.2, 20 MHz GEKO Magazine n. 69 ( IU3QEZ): radiohorizon 4/3 van 130 km van 1000 m, 260 km twee stations toevoegen; troposcatter met gemeenschappelijk volume tussen 1

## 12. Ronde tabel: bronnen om het model te valideren of te corrigeren (21 november)

Samenvatting van alle bochten van bronnen, besteld voor gebruik. "Staat" zegt als de bron al in het programma (1.3.1), of het in test of moet worden gedaan.

### Gegevens om te valideren (wat er werkelijk gebeurd is)

| Bron | Wat geeft | Gebruik | Staat |
| MMMonVHF rapporten van de auteur | 593 QSO bij 144 MHz, 29/12/2021 | Eventvalidatie tegen vergelijking (+14 dagen) | in gebruik: AUC 0,79 (1,2) → 0,90 (1.3.1) |
| De wereld boven 50 MHz | gedocumenteerde gevallen (14 | historisch geval | in gebruik als historisch geval; toe te voegen aan de validatie QSO's |
| wspr.live (WSPR) | compleet archief, 2 m en 70 cm, SQL | veel evenementen en vele "nee" (uren zonder plek) | geprobeerd, het moet worden gefilterd (vlek vals op 16 |
| APRS PropView / APRS-IS | stations vilt, afstanden, uren | alleen validatie 144 MHz | te doen |
| Omgekeerde beacon (archief) | dagelijkse spot | VHF | voor nu weggegooid |
| I GRA2 radiosondaggi (Brindisi 16320, Praktijk van Mare 16245, Trapani 16429, Udine, Milaan) | profielen gemeten 00 en 12 UTC | GFS-profielbeheer en inversies | gevonden bestanden (Brindisi 46 MB), te gebruiken |
| Barsis en Kirby 1961, Barsis 1963 | Evanescence gemeten op paden met bergachtige hindernis | de categorie "monte" valideren (Vesuvio, Faito) | te doen |
| Norton 1959, NBS TN 15, TN 101 | L(99) en verlies aan tabelmedia | Scattercontrole | gedeeltelijk (zie 300 mijl consistent) |

### Correcties voorgesteld door bronnen

| Correctie | Bronnen | Effect gemeten op validatie | Staat |
| Misme stabiliteit Termijn | Bean en Dutton pet. 6 | AUC 0,79 → 0,90 samen met de daling | in 1.3.1 (titel 16 dB zonder leiding) |
| Onderzijde vanaf verticale beweging | Moler en Holden 1960 (lees in origineel op 22/11: +2,5 dB gemiddelde tussen lucht die naar beneden komt en lucht stijgt, 1,4 en 9,4 GHz op zee) | als boven | in 1.3.1 (+1,5 / -2 dB) |
| Zachte pijpleiding (λ tot 3·λmax) | Kerr, Hepburn, VHF/UHF DX Boek, Shi 2019 | alleen +1% van de AUC; brengt de kleuren van kanaal op 46% van de routes van de gebeurtenis en 0% van de vergelijking | : |
| Persistentie 24 h | VHF/UHF DX Book, Biancomano en Leyshon | AUC ongewijzigd, meer genomen en meer valse alarmen | in 1.3.1 |
| Normaal profiel CRPL met lokale Ns (−ΔN = 7,32·e^(0,005577·Ns)) in plaats van 40 N/km vast | Bean and Thayer 1959, NBS Monograph 4 | getest in validatie | in 1.3.2 als alternatief, uitgeschakeld door defect |
| Superbrekingsdrempel 79 N/km (ITU) in plaats van 100 (Bean en Dutton, laag 1000 | P.453, Lopez 2008, DX Book | getest in validatie | in 1.3.2: agressieve waardering naast voorzichtigheid (de kaart maakt gebruik van prudente) |
| Klimaatdikte (calcle B) minder belangrijk dan het tekort | Rogers 1997 | om te proberen: gebruik A voor winst | te doen |
| Paraboolvergelijking in het GFS-profiel | PETOOL, golfpropagatie (Lytaev), Levy (boek niet gelezen, alleen voorbeeld), Lindquist | eerste versie met veld over de horizon gedomineerd door numeriek lawaai (AUC 0,12 | in gebruik als tweede berekening, beslist niet over de prognose |
| Evanescence: ±7 dB over 100 mijl, marge 10 | Bray 1961, Antenna Book, GEKO 69 | drempels van manieren in marginale omstandigheden | in gebruik: waarschijnlijkheid voor periode en drempel FT8 −19,6 dB in troposcatter |
| Gemeenschappelijk volume en gelijkwaardige afstand van Bray | IU3QEZ, Bray 1961 | Informatie | : |
| Troposcatter P.617 en RSGB als onzekerheid | TropoLink, DL2FZN | Informatie | in 1.3.0 |

### Wat overblijft uit modellen

Geleid op zee op lage hoogte (100 ITG/Longley-Rice blijft een statistische referentie "normale dag" (Ns 301, Climi), zonder leidingen. Afgewezen bij besluit van de auteur: AirScout, P.528), E sporadic (P.2001) en 50 MHz (servoir EIRP dat weinigen hebben en verwarren met de E sporadic).

### Voorstel voor een werkorder

1. Extended Validation: voeg gefilterde WSPR (zelfs uren zonder spot) en QST 2022 toe; herhaal voor 432 MHz.
2. Probeer het normale CRPL-profiel en de ITU-drempel van 79 N/km, één voor één, waarbij alleen blijft wat de AUC verhoogt.
3. Vergelijking van de GFS profielen met de I GRA2 radiogolf van Brindisi en Praktijk van Zee in de dagen van de QSO.
4. Herevalueer de paraboolvergelijking na numerieke correctie.

### Besluiten van 21 november (versie 1.3.2)

- Goedgekeurd: voorzichtige schatting (Bean en Dutton, 100 N/km) en agressief (ITU, 79 N/km) geflankeerd; kanalen die slechts onder 2500 m (eerste 1500 m, verhoogd na validatie); bonus met hoge uitgebreide druk en zwakke wind; zeegewicht met de μ1 van P.452; aandeel van stations met betrekking tot de leiding ook op gedeeltelijke leiding; grenslaag boven de ribbels ( IT9GSF).
- Geadopteerd als alternatief, uitgeschakeld door defect: normaal CRPL profiel met lokale Ns.
- Correcte paraboolvergelijking numeriek: AUC van 0,12 tot 0,54 (30 m antenne) en van 0,28 tot 0,63 (300 m).
- Land: Copernicus DEM GLO-30 in plaats van Terrain Tegels IC8TEM
- Weerborden op de kaart met hun effect: ↓ gunstige daling (Moler en Holden 1960, gemiddeld ongeveer 2,5 dB tussen lucht naar beneden komen en lucht omhoog gaan; +1,5 dB), ↑ lucht bergopwaarts ongunstig (diepe armaturen; −2 dB), S slaan gunstige aanwijzing (ARLL-Handboek § 19.4, sterke inversies op de Middellandse Zee; geen correctie). Dubbel symbool, groen/rood/oranje kleuren en stripboek op de kaart.
- Aangenomen het ITG 1.2.2 (NTIA/itm, publieke domein), vertaald in C#: volgens berekening van de basislijn in normale atmosfeer naast P.452 en laatste stretch na de uitgang van een geleider die eindigt voor de correspondent. Afgewezen qradiovoorspelling code omdat onder GPL v2.

Zonder nuttige inhoud voor de index: * Mededelingen Driemaandelijks*, najaar 1991 (geen tropo artikel; Atkins' artikel over de Scatter is in de winter 1991 uitgave).

- Aangenomen als het principe van de handboeken (21 november): "de voortplanting kan niet algemeen zijn, maar wordt bestudeerd met betrekking tot de plaatselijke morfologie" ( DF5AI, gerapporteerd door IK3XTV in "Propagatie op 2 meter"), genoemd in het prognosehandboek (§ 1) en in de raden.
- Aangenomen (21 november, na 1.3.3) de interpretatie van IK3XTV 's middags transalpine openingen: niet " convectieve cellen zoals lenzen," maar troposcatter versterkt door turbulentie naar de bovenkant van de laag nagespeeld wanneer het het aandeel van het gemeenschappelijke volume bereikt (handleiding van prognoses, § 8 ter). Van 1.3.4 komt het in de puntberekening als een voorlopige versterking van de troposcatter, ten hoogste 3 dB. Afgedankt door dezelfde artikelen: de "auto-amplificatie" van de signalen buiten de Alpen (een passief medium versterkt niet: de beschreven verbindingen blijven verklaarbaar met diffractie en troposcatter; de aangegeven bevoegdheden worden niet gebruikt als gegevens), de sneeuw die "beter diffractie" (waarschijnlijk de stabiele laag over de sneeuw, met minder evanescence).

- Kerr (1951) besproken op het boek (22 november, PDF verstrekt door de auteur): § 1.5, eq. 17 en 18 en tabel 1.1. De cijfers van eq. 18 in de scan zijn onleesbaar, maar tabel 1.1 bevestigt: 20 ft → 1 cm, 80 ft → 10 cm, 400 ft → 100 cm, 2000 ft → 1000 cm, tegen 1,25, 10, 112 en 1252 cm λmax = 0,014·d^1,5 (λ in cm, d staand; de ronde tabel, 'indicatieve waarden'. De hypothesen van Kerr: binnenin de leiding moeten de aangepaste indexdruppels van 4·10−8 per voet, typische waarde van de lage kanalen; met zwakkere gradiënten een grotere dikte hebben. Kerr waarschuwt ook dat λmax geen netcut is: golflengten soms langer kunnen nog sterk worden beïnvloed door het kanaal.

- DL6WU herzien op het origineel (22 november, PDF van VHF Communications 3/1982 verstrekt door de auteur): G. Hoch, Fig. 1: winst op de dipool gemeten op 432 en 1296 MHz in een professionele veelhoek van de Duitse Posten, met een steekproef antenne EIA van 7,7 dBd, geschatte nauwkeurigheid 0,5 dB; 2,35 dB voor het dubbele van de lengte van de boom, geen verzadiging tot ongeveer 20 λ. Het programma gebruikte 8.6 + 8.0.log10(L/λ) dBd, d.w.z. ongeveer 0,8 dB in minder dan 1 Afgewezen formule 10·log10(7,8·L/λ) circuleerde op het net: 3 dB voor verdubbeling, tegen 2,35 gemeten.
- Booker en Gordon (1950): referentie bevestigd door "This Week's Citation Classic" (Huidige inhoud, 8 februari 1982, commentaar van H. G. Booker): Proc. IRE 38, blz. 401 Het commentaar bevestigt de twee punten die in de handleidingen worden gebruikt: veld ontvangen voorbij de horizon met Rayleigh distributie, en exponentiële autocorrelation functie van onregelmatigheden (niet-Gaussian). Het originele artikel is niet gelezen.

## 13. Uit IK3XTV artikelen

De twee artikelen van Flavio Egano IK3XTV (Radio Rivista: "Propagatie op 2 meter" en "Propagatie VHF Het zijn waarnemingen vanuit het station, geen maatregelen: ze moeten worden behandeld als hypothesen om te controleren met vlekken.

| Opmerkingen van de auteur | Hoe het een ingrediënt van de index wordt |
| De kanalen zijn voornamelijk gevormd tussen 100 en 1000 m, zelden hoger; dienen hoge druk op een breed gebied, goede sprankeling en afwezigheid van wind | zoeken naar het kanaal slechts onder 1500 m; bonus met hoge uitgebreide druk en zwakke wind in de lage lagen |
| In de warme maanden uitgevoerd terugkerende op de Adriatische Zee tussen Veneto en Puglia, 700 | groter gewicht tot de fractie van de zeeroute (zoals de μ1 van ITU-R P.452) |
| De overeenkomstige moet in of in de buurt van de leiding; met hoge antennes en een paar meter leiden het signaal niet binnen | vergelijk het aandeel van de basis van de leiding met het aandeel van de twee stations (zal de hoogte van de QTH dienen) |
| Zelfde tijd, zelfde geleider: luister vanuit de Gargano een station in 144 en een in 50 MHz | om te controleren: volgens de Hepburn tabel vragen de 50 MHz om meer dan 300 m lood, zodat de gebeurtenis zou wijzen op een pijpleiding zeer vaak |
| Verbindingen Veneto | "concrete" index gescheiden van de geleider, voor bergroutes |
| De convectie cellen worden geboren op de grond en klimmen ongeveer 1 km om de 3 uur: op 15 bereiken ze ongeveer 3 km, de hoogte die dient om de Alpen te overschrijven | gebruik de limietlaaghoogte (grens\_laag\_hoogte van Open-Meteo GFS) langs het pad: score wanneer hoger is dan de hoogte van de richels |
| De Alpen genereren gravitatiegolven die samen met convectieve bewegingen onregelmatigheden van de brekingsindex veroorzaken. | hypothesen: sterke wind loodrecht op de ketting tot 700 hPa als signaal van goud golven; om te proberen laatste |
| Zeer hoge inversie (tot 8 km in uitzonderlijke gevallen) geeft ongeveer 800 km, maar in de praktijk zelden meer dan 300 km | het aandeel van de inversie breidt het toepassingsgebied uit: nuttige gegevens om de wachtafstand te schatten |
| In de zomer is de aardradius gelijk aan ongeveer 16000 km (2,5 keer de werkelijke), met ongeveer 60 dB in minder dan 300 km verlies | consistent met k50 = 157/(157 − ΔN): k = 2,5 komt overeen met ΔN ongeveer 94 N/km, dat is superbreking |
| Meer dan 3000 km alleen in het gebied van 20 | buiten Europa: nuttig om geen hoge indices te geven op de Atlantische Oceaan zonder feedback, en voor de Canarische route |

Andere nuttige gegevens uit dezelfde artikelen:

- Optisch bereik tussen twee h1 en h2 meter (d in km): d = 4,1 · (√h1 + √h2), tot ongeveer 20% voor normale refractie.
- Evanescence: langzaam en oppervlakkig met inversie; sterk en snel met scatter en convectieve cellen. Het programma zou het kunnen gebruiken om advertenties te classificeren als het op een dag SNR rapporten na verloop van tijd zal hebben.
- Voor de sporadische E in 144 MHz rapporteert de auteur het werk van DF5AI en Davis en Johnson (Natuur 435, 2005) over de band tussen temporalen en Es: uit de tropo, maar een afzonderlijke indicator kan morgen bliksemgegevens gebruiken.
- Grondreflectie in de dubbele sprong Es: zee K = 81 en 5 S/m, landschap en bossen K = 13 en 0,006 S/m, stedelijke gebieden K = 3 en 0,001 S/m.

## 14. LIVETROPOVHF-indexhypothesen

Startvoorstel, te bespreken: een index 0/0-10 op dezelfde schaal als Hepburn, berekend voor elk punt van het raster en elke 3 uur van GFS (toen ECMWF en ICON voor vergelijking), en een tweede nummer per route dat zegt hoeveel deel van de route overgaat in een nuttig kanaal naar de gekozen band. De gewichten hieronder zijn mijn veronderstellingen, om te worden gekalibreerd met de vlekken.

Beoordeling na Bean en Dutton en Kerr: profieldrempels komen uit hun tabel 6.5 en schaal van hun metingen, dus een brekingslaag is 10 De index wordt dan eerst berekend in dB en neemt dan 0

Berekening per punt, stap voor stap

1. T-ontlading, relatieve vochtigheid, geopotentiaalhoogte en verticale snelheid bij 1000, 975, 950, 925, 900, 850, 800, 700, 600 en 500 hPa, plus T en 2 m vochtigheid, gronddruk, laaghoogte en wind op 10 m.
2. Gooi de niveaus onder de grond weg; berekening N en M op elk niveau (ITU-R P.453).
3. Berekening −dN/dh in elk van de vier lagen van Bean en Dutton (1000
4. Waar dM/dh < 0 vind ik base en top van de leiding, dikte d en tekort ΔM.
5. Vergelijking van de minimumdikte voor de band met de formule van Kerr (ongeveer 180 m voor 144 MHz, 89 m voor 432 MHz, zoals in de Hepburn-tabel); onder de drempel telt de leiding nog steeds, maar minder.
6. Ik stimuleer de toename in dB met onderstaande tabel (u neemt de beste laag, niet de som van de lagen, plus de term anomalie) en zet deze om: index = dB / 2.5, tussen 0 en 10, dus 25 dB is 10 waard. Een negatieve waarde (sub-breking) wordt weergegeven als 0 maar blijft in de paddata.

| Beste laagsituatie | Criteria | Geraamde inkomsten |
| Normaal (niet gelamineerd) | −dN/dh in het tabbladbereik. 6,5 (bv. 20 | 0 |
| Iets boven normaal | tussen de bovengrens van de normale band en de superbrekingsdrempel | 0 tot 10 dB, in verhouding |
| Superbreking | boven de laagdrempel (100, 80, 70, 50 N/km) maar onder 157 N/km | van 10 tot 15 dB, in verhouding tot 157 N/km |
| Te dunne pijpleiding voor band | dM/dh < 0, dikte onder Sleuteldrempel | 15 |
| Nuttige connector voor band | dM/dh < 0, dikte boven de Kerr-drempel | 20 dB, +1 dB om de 5 M-eenheden tekort, maximaal 25 |
| Subfractie | −dN/dh < 0 in de onderste laag | −10 dB |
| Anomalous achtergrond (u voegt toe) | Model Ns minus ITU kaart klimatologisch Ns voor die maand | 0,2 dB per eenheid N, tussen −5 en +5 dB |

Berekening per route (tussen twee locators): monster de maximale cirkel om de 10 km, net als het programma voor de index op het pad, en bereken de fractie van punten met nuttige kanaal en het gemiddelde aandeel van de basis. De CSIRO-studie vond de fractie van de route in de pijpleiding nuttig (op de AIS-routes bij 162 MHz was 67% hoger dan in de controlegevallen).

Het toevoegen van de berekening per route (van LA0BY en W3SZ): (1) zeestations vergeleken met basis en top van de leiding, omdat een hoge leiding alleen dient voor degenen die binnenkomen; (2) een troposcatter "vloer" met de Yeh-methode, met de starthoeken uit het grondprofiel; de trope gain in dB wordt verwijderd van dat verlies en het resultaat wordt vergeleken met het verlies dat het station weet te overwinnen ( KK6MC tabel). Zo wordt de index ook een schatting van de bereikbare afstand.

Correcties om één voor één te proberen (van Bean en Dutton en LA0BY, IK3XTV, G4MCU al in de handleiding), alleen te houden als ze de vergelijking met vlekken verbeteren:

- Mengen (belangrijk volgens de auteur, door dagelijkse observatie): van dag tot dag op aarde, tussen midden in de ochtend en middag met heldere hemel, of met wind op de grond over een paar m/s, verdwijnen de kanalen naar de grond; hun bijdrage moet worden verminderd. Niet op zee.
- Tijd van de dag (belangrijk volgens de auteur, door dagelijkse observatie): irraggiamento kanalen ontstaan bij zonsondergang en vallen na zonsopgang; het is een 3-uurs prognose controle.
- Verticale beweging: lucht stijgt naar 850 hPa remixt de lagen (negatieve correctie), lucht die daalt onder een anticycline versterkt hen (positief).
- Druktrend: opwaartse druk op het pad in de laatste uren, hoger en stabiel signaal; in afname, lager signaal (bastano 2 hPa).
- Synoniem: hoge druk boven 1025 hPa, temperatuur bij 850 hPa veel hoger dan op de grond, warme en droge lucht stromen op de koudere zee, route op zee.
- Bergpaden: een tweede "convectieve" index (sectie IK3XTV), met een middaglimietlaag hoger dan de richels langs het pad en hoge druk.

Van VHF/UHF DX Boek, andere correcties om te proberen: opeenvolgende dagen van hoge druk op het punt (meer dagen, meer punten), positie in vergelijking met het centrum van de anticycline (bonus aan de randen), drukgradiënt op de grond als maat van de wind, sterke regen in de 24 uur voor (penaliteit), lage wolken aan de rand van de laag (penaliteit alleen voor de hoge banden). Voor de richting van de routes: vergelijk de azimut van de route met die van de isobare.

Verificatie

- LIVETROPOVHF archief: elke commerciële meer dan 500 km tot 144 of 432 MHz is een "event." Het vergelijkt de index op de route met de frequentie van gebeurtenissen, op hetzelfde moment van de dag en maand (dezelfde benadering van de CSIRO-studie).
- Vergelijking met Hepburn en F5LEN op dezelfde dagen: waar de drie indexen loskoppelen, kijk je wie precies was volgens de vlekken.
- Radiosundaties van 00 en 12 UTC: om te controleren of het model de inversies ziet die de sonde meet.
- Historische gevallen van LA0BY (6
- In de handleiding aan te geven grenswaarden: uitgevoerd onder 100

## 15. Bronnen van 21 november (tweede deel)

- ITU-R P.1812 (MatLAB/Octave implementatie van I. Stevanovic, OFCOM; GitHub eeveetza/p1812, Zenodo 10.5281/zenodo.6141558). VHF/UHF-gebiedspecifieke methode van de route, afgeleid van P.452: "delta-Bullington"-diffractie, troposcatter, kanalen en reflectie op lagen, ondoordringbaar op de grond. Nuttig als derde vergelijking naast P.452 in puntberekening; ΔN- en N0 -gegevens zijn volledige ITU-producten, zodat de gebruiker ze downloadt (volgens P.453).
- uit Costa, Rabanal Ramirez, Dias, IET MAP 2018 (doi 10.1049/iet-map.2017.0992). 563 MHz, 27 punten tussen 5 en 30 km op onregelmatig terrein met bos: P.526 met 12 m begroeiing −4,3 dB en σ 8,8 dB; P.1812 met 10 m begroeiing +2,6 dB en σ 8,9 dB; diëlektrische wig −4,8 dB en σ 9,2 dB; P.1546 +5,0 dB en σ 17,7 dB. Voor het programma: de restfout van een goede diffractiemethode is ongeveer 9 dB, en de hoogte van de crêpes op de ruggen (10
- PETOOL (Ozgun e altri, Computer Natuurkunde Communicatie 182, 2011). Parabolische vergelijking split-step op een en twee routes met breedhoek propagator (hoeken tot 40 Bevestigt de keuze van 1.3.2 (breedhoek, bodemcoëfficiënten) en geeft de tweewegberekening voor steile reliëfs aan.
- MathWorks. `tropopl`: verlies van gassen en verlies van Lente in een gelaagde atmosfeer (P.835, P.676, P.453), zonder kanalen: met hellingen onder −157 N/km is het geometrische optische model niet geldig. Het voorbeeld van radar propagation uses for refraction −ΔN = 7,32·e^(0,005577·Ns), hetzelfde CRPL rapport al in het programma. `propagationModel` Longley-Rice: vooraf gedefinieerde horizontale polarisatiewaarden, geleidbaarheid 0,005 S/m, vergunning 15, reactiviteit 301, gematigd continentaal klimaat. Geen MathWorks model behandelt de kanalen.
- ITU-R, digitale producten van groep 3. Gegevens en programma's van P.453, P.452, P.1812, P.2001, P.528, P.1546; 'geïntegreerde' gegevens maken deel uit van de aanbevelingen; geen aangegeven herverdelingslicentie: u laat ze verder downloaden naar de gebruiker.
- ESA Navipedia, troposferische vertraging. N = Nidrostatica + Numida; de droge zenitale vertraging is ongeveer 2,3 m en de vochtige een tientallen cm, die snel varieert. Voor ons: de door de GNSS-stations geraamde natte vertraging is een indirecte maat voor de geïntegreerde vochtigheid, mogelijke toekomstige bron van nu-casting; vandaag is niet bruikbaar.
- Bray, K2LMG, De auteur, D. W. Bray (General Electric, Ithaca), zegt dat hij de prognoses heeft geverifieerd met maanden van geplande verbindingen met K2GOI en W4LTU. Evanescence binnen de tijd van 13,5 dB over ongeveer 100 mijl (7 dB onder en 7 boven het uurgemiddelde), bijna lineair onder (3,5 dB tot 50 mijl): voor een vaste verbinding verwijder 7 dB. Correctie van de horizon: elke hoogte van de horizon is 69 mijl (111 km) van extra afstand waard (minder als negatief). Winst van een yagi ongeveer 10 × boom/λ (vermogensverhouding), meer ongeveer 4 dB van de grond reflectie; SSB −3 dB en AM −7 dB in vergelijking met CW; 99% van de uren: ongeveer 180 dB tot 60 mijl, 190 tot 90, 200 tot 240 (2 m). Vanaf 1.3.2 gebruikt de waarschijnlijkheid voor de periode σ = 3,4 dB (±7 dB als ±2σ) in plaats van de 5 dB geschatte tweedehands.
- Tweewegberekening (probeer met het prototype, 21 november). Grote hoek parabolische vergelijking met echte S RTM1 terrein van Capri, 144 MHz, antenne 10 m van de grond, standaard atmosfeer; maat: gemiddeld vermogen tussen 20 en 400 m tot 80 km op de route naar de overeenkomstige. Vanuit het centrum van Capri en Marina Grande is het westen gesloten (obstakel tot +11 Door de Faito te wijzen (bijna achter EA5 en EA6, 150 Van Anacapri het westen is vrij en de directe overwinningen door 8 De absolute verliezen op 1000 km van het prototype zijn echter niet betrouwbaar. EA3 EA5 EA6
- Barsis en Kirby, NBS Report 6751 (1961), Pikes Peak, 223 km, 100 en 751 MHz. De berg is een gemeenschappelijke terminal van twee routes in de optiek: de verdeling van de uurgemiddelden is de convolutie van die van de twee routes. Lange termijn excursie (1 Het signaal is sterker dan de troposcatter maar zwakker dan de ideale rand: de bovenkant moet worden behandeld als afgerond (ongeveer 11,5 km). Er worden altijd hoge signalen waargenomen achter de rugzakken.
- Barsis, NBS Report 7652 (1963), drie routes over een berg (Colorado, Alaska, Europa), 750 Gemiddeld verlies 10 Vanaf 1.3.2 voegt de ingang in de leiding de afronding van P.526 toe.
- Barsis and Johnson, NBS TN 88 (1961). Langdurig oplossen van de ruimtegolf, zelfs op een pad voorbij een berg (§ 5).
- NBS 63D (1959). De curves waaruit Bray de nomograms krijgt; definities van systeemverlies, transmissie en basis, procesantenne krijgen.
- Rice, Longley, Norton and Barsis, NBS TN 101 (1965 enkele rand en obstakel afgerond met en zonder reflecties (§ 7, met het voorbeeld van Pikes Peak), lange termijn variabiliteit V(0,5) en Y(q) voor klimaten (getemperd concontinentaal en andere). Voor ons: de methode van afgeronde hindernis- en variabiliteitsfuncties is de referentie voor het kalibreren van onzekerheid.
- ITU-R P.526-16 (11/2025). Formule (31) van J(ν), identiek aan dat van het programma; afgeronde hindernis (32)
- ITU-R P.1812-8 (2025) en -7. Volledige tekst hersteld: specifieke methode van de 30 MHz tot 6 GHz (delta-Bullington, troposcatter, kanalen en reflectie op lagen, ondoordringbaar). De -7 waarschuwt dat de troposcatter (van P.617-5) onder 66 km verkeerd is. Het blijft een mogelijke derde vergelijking.
- Paulus, een praktische toepassing van het IREPS-verdampingsmodel. Rapport gelezen op 22/11/2026 (DTIC ADA146528, scan met herkende tekst, soms onleesbaar). De door schepen gemeten verschillen tussen lucht en zee worden verschoven naar stabiliteit: warmere zeelucht in 31 Het Jeske model geeft dan 40 m en meer dan 15% van het jaar en hoogtes die springen van 10 naar 40 m in 24 uur met stabiele tijd, niet bevestigd door radiometingen. Voorgestelde correctie (geteste drempels 0, −0,5 en −1 °C; de −1 °C is beter overeen te komen met de radiomaatstaven van juni 1982): als het verschil tussen lucht en zee −1 °C of minder is dan het gemeten verschil wordt gebruikt; anders berekent u de hoogte met −1 °C en met 0 °C en, als dat met −1 °C lager is, houdt u dat vast. Verhogingen aan de stabiele kant worden gesneden, dalingen blijven. In juni, Marsden 120 vierkant: gemiddelde hoogte van 16,8 m (climatologie) tot 7,6 m. De vergelijkingen van het model staan niet in het rapport (zie Jeske en Hitney). Het programma maakt gebruik van het neutrale profiel, dat geen stabiele overdrijving heeft, en beperkt δ tot 40 m: het gaat in dezelfde richting; met SST modellen (niet schip) het meetdefect dat Paulus corrigeert weegt minder.
- Levy, Paraboolvergelijkingsmethoden voor elektromagnetische golfpropagatie (IEE 2000). Op Google Libri staat alleen een voorbeeld: Ik lees niet.
- VK3FS (3fs.net.au). Kwaliteitsintroductie: hoge openingen op 450 In overeenstemming met dag-/nachtwaarschuwingen en een maximum van 2500 m.

## 16. Bronnen

Regels en formules

- [ITU-R P.453-1 (2017), radiobrekingsindex:](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)formule en gegevens
- [ITU-R P.452-18 (2023), voorspelling van interferentie tussen grondstations over 100 MHz: equivalente straal, β0, troposcatter,](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)-kanalen
- [ITU-R P.617-5, voortplanting over de horizon voor radiobruggen](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)
- [Shi et al., "A Comprehensive Study on Wave Maximumlength of Electromagnetic Propagation in Different Verdamping Duties," IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

Studies naar de voorspelling van kanalen met weermodellen

- [P. Lopez, "A 5-year 40-km resolutie globalclimatology of super-refractie for ground-based meteorology radar," ECMWF Tech. Memo 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO), "Tropospheric Ducting Prediction based on GFS Model Data at... the CSIRO Murchison Radio-astronomie," Observatory arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (FMI), "Effecten van modelresolutie over het voorspellen van atmosferische kanaalomstandigheden met Harmonie-AROME," EGUsfeer 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

Amateurradiovoorspellingen

- [W. R. Hepburn, "The Hepburn Tropo Index (HTI) "](https://www.dxinfocentre.com/propagation/hti.htm)en [](https://www.dxinfocentre.com/propagation/history.htm)methodegeschiedenis
- [W. R. Hepburn, "Lowest Usable Frequency vs. Dikte of Ducting Layer"](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, "Wereldwijde brekingsindex forecasts"](https://tropo.f5len.org/WW/info.php), [nieuwe versie 2026](https://tropo.f5len.org/?p=504), [noot op model ARPEGE](https://tropo.f5len.org/author/f5len/)

Gegevens

- [Open-Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api), [noot op drukniveaus](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)
- [NOAA NCII, Integrated Global Radiosonde Archive (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RAOBget, radio overstroming script voor de Universiteit van Wyoming](https://github.com/NCAR/RAOBget)

Klassieke teksten (aangegeven door de auteur van het programma, te raadplegen voor definitieve formules)

- B. R. Bean, E. J. Dutton, *Radio Meteorology*, NBS Monograph 92, 1966, 435 pagina's (herdruk Dover 1968): verticale profielen van N en M, trapgradiënt onder −157 N/km, vergelijking met radiogolf; hoofdstuk 5 "Synoptische radiometeorologie." Basis voor 2 Vrij: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [NIST kaart, doi 10.6028/NBS.MONO.92](https://www.nist.gov/node/1770911), [Internet Archief](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf)kopie.
- L. Boithias, *Radio Wave Propagation*, McGraw-Hill 1987 (vertaald door D. Beeson uit het Frans, 330 pagina's, ISBN 0070064334; ook North Oxford Academic): mechanismen voorbij de horizon, gemeenschappelijk volume van verstrooiing en verlies van koppeling antenne-half. Papier alleen ([bibliotheekkaart](https://libcat.library.tamu.edu/Record/in00000014812/Description)).
- M. P. M. Hall, * Effects of the Troposfeer on Radio Communication*, IEE Electromagnetic Waves Series n. 8, Peter Peregrinus 1979, 206 pagina's (ISBN 0906048257, brossura 0863410863): synoptische situaties en voortplanting VHF/UHF. Basis voor Synoptische ingrediënten. Handel bij IET ([](https://libcat.library.tamu.edu/Record/in00000636385)-kaart).
- H. G. Booker, W. E. Gordon, "A Theory of Radio Scattering in the Troposfeer," *Proceedings of the IRE*, vol. 38, n. 4 April 1950, pp. 401 Verschuiving van de disogeniteit van de brekingsindex. Centraal idee: het diffuse vermogen is afhankelijk van de intensiteit en schaal van de turbulente schommelingen van de brekingsindex (exponentiële correlatie); nabij de horizon heerst diffractie, verder verstrooien; Volgens de auteurs zijn de schaal van turbulentie en de indexvariaties die door meteorologie worden verwacht voldoende om de gemeten velden te verklaren. Aangehaald door 328 artikelen (Scilit), geen gratis toegang kopie. Kaart op Semantic Scholar ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); full text on IEEE Xplore paid ([doi 10.1109/JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)).
- C. M. Crain, J. R. Gerhardt (Electrical Engineering Research Laboratory, University of Texas), "Measurements of the Parameters Involved in the Theory of Radio Scattering in the Troposphere," *Proceedings of the IRE*, vol. 40, n. 1, January 1952, blz. 50](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)
- "The Constants in the Equation for Atmospheric Refractive Index at Radio Frequences," *Proceedings of the IRE*, vol. 41, n. 8, augustus 1953, pp. 1035 N; de waarden 77,6 en 3,73 · 10^5 van de formule ITU (Q)](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E. C. S. Megaw, "Fundamental radio scatter propagation theory," *Proceedings of the IEE Part C: Monographs*, vol. 104, n. 6, september 1957, blz. 441](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)
- *Proceedings of the IRE*, oktober 1955, special number on scatter propagation (door K. A. Norton en J. B. Wiesner), with Bullington, Staras e.a.: free in PDF on [World Radio History](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf).
- D. E. Kerr (uitgegeven door), *Propagation of Short Radio Waves*, MIT Radiation Laboratory Series vol. 13, McGraw-Hill 1951 (reports Dover 1965 and IEE 1987): theory of vaporation and surface ducts on the sea. Digitale kopie: [World Radio History](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); IET editie in [IET Digital Library](https://shop.theiet.org/propag-short-radio-waves).

Recente metingen van troposferische diffusie in VHF: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Australië), "A Study of VHF Troposcatter Communication Channels in South Australia," in *Digital Signal Processing for Communication Systems*, Springer 1997, pp. 103 Tekst voor een vergoeding; van abstracte het betreft verspreiding, niet de geleiders. Alleen bruikbaar als u de term troposcatter met VHF-kanaalgegevens wilt kalibreren.](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)

Numerieke modellen en zeeroutes (aangegeven door de auteur van het programma)

- M. Levy, * Paraboolvergelijkingsmethoden voor elektromagnetische golfpropagatie*, IEE Electromagnetic Waves Series #45, 2000, 336 pagina's (ISBN 0852967640): parabolische vergelijking voor brekingsprofielen die langs het pad en voor de orografie veranderen; hoofdstukken over tropoferische voortplanting, stralen en manieren, voortplanting op zee. Het zou dienen voor een veldberekening langs het pad, voorbij de index. Voor betaling, ook in elektronische vorm van IET ([IETkaart](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)).
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (Universiteit van Leicester): 2 GHz-metingen op de Kanaaleilanden en het Kanaal. Veel werken en thesis zijn vrij in het universiteitsarchief: [langetermijnstatistieken over het verdampingskanaal, Radio Science 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434), [signaalwijzigingen op drie zeeroutes, Radio Science 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710), [Titels van N. Mufti 2012 met routes van 50 en 140 km ook in VHF](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854), [tesis van I. Alam 2002 over reactiviteit berekend uit de weersgegevens, 240 MHz en 2 GHz X3 UQ, [tesi.](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus, "Praktische toepassing van een verdampingsmodel," Radio Science vol. 20 n. 4, 1985, blz. 887 (niet IEEE Transactions on Antennas and Propagation), doi 10.1029/RS020i004p00887 ([scheda](https://zendy.io/title/10.1029/rs020i004p00887)): hoogte van de verdampingskanaal uit gegevens over het weer over het oppervlak. Vrijheid van het daaraan voorafgaande technisch verslag: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf).

T. Lindquist, *Wave Propagation Models in the Troposphere for Long-Range UHF/SHF Radio Connections*, masterscriptie, Universiteit van Karlstad, 2 juni 2020, 106 pagina's (geleverd door de auteur van het programma). Het is een stap-voor-stap handleiding, vrij, om een berekening te schrijven met de parabolische vergelijking: split-step oplossing (zelfs in brede hoeken), grensvoorwaarden voor land en zee, stap grond, beam tracking op lineaire profielen af en toe, met numerieke controles; citeert de PETOOL vrije tool voor MATLAB. Twee nuttige gegevens tegelijk: (1) N-gevoeligheid voor weervariabelen, δN ≈ 0,26 δP + 4,3 δe − 1,4 δT (hPa en K, ongeveer 20 °C), d.w.z. een fout van 1 hPa op de stoomdruk weegt drie keer een 1 °C-fout op de temperatuur: is het vocht van het model dat eerst moet worden gecontroleerd; (2) Gemiddelde hoogte van het verdampingskanaal: 5 De conclusie van de auteur (frequenties 1 tot en met 10 GHz): voorbij de horizon hebben de betrouwbare verbindingen bijna altijd een kanaal nodig.

Propagatie software op het bodemprofiel (voor horizon en obstakels, niet voor de verwachte tropo)

- [Radio Mobile](https://ve2dbe.com/rme.html)van Roger Coudé VE2DBE: gratis, Windows (ook Linux en Mac met Wine), Longley-Rice/ITM model, terreinprofiel automatisch gewonnen uit quotagegevens ([download](https://ve2dbe.com/download/download.html)).
- [SPLAT!](https://qsl.net/kd2bd/splat.html)by John Magliacane KD2BD: GPL, 20 MHz
- [ITM, NTIA/ITS](https://its.ntia.gov/software/itm)officiële code: het Longley-Rice model (20 MHz De code is gratis en u kunt toevoegen aan de berekening op het pad van LIVETROPOVHF (verlies door de bodem, te worden toegevoegd aan de tropo index). Oorspronkelijk rapport: A. G. Longley, P. L. Rice, "Prediction of Tropospheric Radio Transmission Loss Over Irregular Terrain: A Computer Method https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITU-R, software en digitale gegevens](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): vrije kaarten van ΔN en N0 jaarlijkse mediaans op raster 1,5° gebruikt door P.452 en P.617 nuttig als referentieklimaatwaarden voor de index.
- Pathloss (Contract Telecommunication Engineering): commercieel programma voor radiobruggen; Ik heb geen publieke bron gevonden die bevestigt welke ITU aanbevelingen implementeert, dus om te controleren.

Herbruikbare GitHub-code in LIVETROPOVHF

| Project | Wat hij doet | Taal | Gebruik mogelijk voor ons |
| [NTIA/ITM](https://github.com/NTIA/itm) | model Longley-Rice ambtenaar van NTIA, met pakketten voor. NET |  C++, C#  | land verlies op het pad: integreert direct in het programma, dat is in C# |
| [fuzzzeeee/itm](https://github.com/fuzzzeee/itm) | ITG C++ code C# vertaling |  C#  | alternatief zonder native bibliotheken, ook nuttig voor de Windows 7 versie |
| [eeveetza/Py452](https://github.com/eeveetza/py452) | ITU-R P.452, identiek aan de MATLAB/Octave versie goedgekeurd door ITU-R WP 3M; MATLAB versie gepubliceerd door het Zwitserse Federaal Communicatiebureau | Python, MATLAB | verwijzing naar vertaling in C# de troposcatter formules en uitgevoerd, met ITU validatie voorbeelden |
| [bwinkel/pycraf](https://github.com/bwinkel/pycraf) | P.452 compleet, P.676, SRTM profielen; GPL v3, maar de meegeleverde ITU gegevensbestanden zijn niet gratis voor commercieel gebruik | ♪ | controle van onze resultaten |
| [ITU-Rpy](https://github.com/inigodelportillo/ITU-Rpy) | veel ITU-R aanbevelingen, waaronder P.453-13, gevalideerd met ITU voorbeelden | ♪ | verificatie van de N-formule en klimaatkaarten van P.453 |
| [NCAR/RAOBget](https://github.com/NCAR/RAOBget) | download de radiogolf van de Universiteit van Wyoming in tekstformaat | ♪ | diagram te downloaden in C# de profielen van de Italiaanse stations |

Opmerking: de huidige versie van P.453 is de [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), gratis te downloaden; in het document heb ik de 13 gebruikt om te reallineren.

Al in de programma handleidingen en herbruikbaar hier: LA0BY (Nordic VUSHF 2019), IK3XTV ("Propagatie op 2 meter" en "Propagatie VHF DF5AI/DK5YA G3YGF G4MCU KP4MD
