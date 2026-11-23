# Avoti un formulas

> Mašīntulkojums no itāļu valodas: šaubu gadījumā noteicošā ir itāļu rokasgrāmata.

LIVETROPOVHF tropo prognozes (20.–21. novembris) Pamatfizika, pieejamie dati, publicētie indeksi, formulas, amatieru radio pieredze, apstiprināšana un lēmumi. Formulas numurē kā LaTeX (1), (2),... un tekstā tās atsauc ar skaitli iekavās. Prognozes rokasgrāmatā aprakstīts, kā programma tos izmanto.


## 1. Autora telpas

IC8TEM novērojumi, kas nosaka darbu, ar atsauksmēm, kas atrodamas avotos.

- Itālijas ģeogrāfijā ir nepieciešama difrakcija. Ar grēdām, promontorijām un salām dažus desmitus kilometru attālumā no stacijām difrakcijas devumi jāaprēķina ar Frenela parametru galvenajā malā, ne tikai ar gludo zemi. Satikšanās: salīdzinot neregulāro reljefu pie 563 MHz (Rio de Janeiro, 27 punkti no 5 līdz 30 km), labākās metodes bija P.526 ar asmens malām, P.1812 un difraktīvo ķīli, ar vidējo kļūdu no −5 līdz +3 dB un dispersiju aptuveni 9 dB; P.1546 bija pesimists (no Costa, Rabanal Ramirez un A Stāvām nogāzēm pie PTOOL stacijas atceras, ka tie apkalpo platleņķa propagandētājus un divvirzienu aprēķinus (viļņa atspoguļo reljefi).
- Lai iekļūtu attālā caurulē, ir vajadzīgs Frenels. Ja kanāls sākas aiz horizonta, signāls nāk pie mums ar difrakcijas pār šķēršļiem līdz tās sākumam; no 1.3.2. programma aprēķina to (Prevision Manual, § 10 ter). Tikšanās: Rohde & Schwarz (2023) atzīmē, ka stacijās pa augstu vadu bieži neizdodas iekļūt, bet tie tuvu galiem jā.
- Apstākļi ātri mainās. Teorijas ir, bet apstākļi mainās minūtēs, un neviena prognozēšanas sistēma ņem datus reālajā laikā: VFS iznāk ik pēc 6 stundām, ar laiku un 0,25°, un prognoze var zaudēt vērtību īsā laikā. Tikšanās: Bray (QST, 1961. gada novembris) stundas vidējo svārstību mērīšana aptuveni ±7 dB virs 100 jūdzēm; vadi pārvietojas ar atmosfēras laiku (Rohde & Schwarz); mūsu QSO reģistrs 29/12/2021–2/1/2022 rāda atveres aug un nokrīt dažu stundu laikā.
- Labākā tagadne ir DX klastera–PSKReporter tīkls. Lai zinātu, kas notiek tagad, klastera vietas, PSKReporter (un WSPR) pārspēja jebkuru modeli: tie ir pasākumi. Prognoze norāda, kur meklēt un cik ilgi tas var ilgt; reklāmas saka, ja ir. Šī programma liek vietas un prognozēt uz to pašu karti.

## 2. Mērķis

Ideja ir trešais indekss, ko aprēķina pati LIVETROPOVHF, sākot no laikapstākļu modeļa vertikālajiem profiliem, ko pievieno Hepburnam un F5LEN, nevis aizstāj tos. Šodien programma lasa savas kartes jau gatavas: indekss varētu teikt arī *kāpēc* ir atvēršanas (kvota un biezums no kanāla, minimālais joslas iesprostots) un tas varētu notikt ar plankumiem, ka programma jau apkopo.

- Ko jūs varat sniegt: varbūtību, ka kanāls uz vienu punktu un stundā, daļu bāzes un top, biezums, minimālā frekvence iesprostots, un pa ceļu daļa maršruta iekšpusē vada.
- Kas nevar dot: tievie vadi (zem dažiem desmit metriem), kurus globālie modeļi ar ātrumu 0,25° neatrisina, un iztvaikošanas vadi uz jūras, kas galvenokārt kalpo no GHz uz augšu.
- Pirmais praktiskais mērķis: 144 un 432 MHz Eiropā ar DX klastera plankumu, PSKReporter un vietējo dekodingu pārbaudi.

## 3. Fizika

Viss sākas no refrakcijas N aprēķina katrā profila līmenī: cauruļvads ir tur, kur modificētā reaģētspēja M samazinās kāpšanā, t. i., kur dN/dh samazinās zem −157 N/km ([ITU-R P.453-13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)).

Refrakcija (P un hPa, T kelvinos):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Tvaika spiediens no relatīvā mitruma H (%) un temperatūras t (°C) virs ūdens no −40 līdz +50 °C:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

Modificēta reaģētspēja (h km, vai 0,157 · z ar z metros) un kanalizācijas kritērijs:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN/dh (N/km) | Regula | 144 MHz ietekme |
| 0 | apakšrefrakcija | samazināts plūsmas ātrums |
| No 0 līdz -79 | Normāls (standarts aptuveni −40) | radiohorizonts ar līdzvērtīgu rādiusu 4/3 |
| no −79 līdz −157 | superrefrakcija | iegarena plūsma, pārāk laba |
| zem −15 | cauruļvads (trapping) | notvertie signāli, DX 1000 km attālumā |

Kā iegūt N no modeļa spiediena līmeņos: katram līmenim jūs lietojat T, relatīvo mitrumu un ģeopotenciālo augstumu, jūs aprēķināt N un M, tad gradients starp kaimiņu līmeņiem. Līmeņi zem zemes ir jāizmet (1000 hPa augstumā Alpos ir pazemē).

Minimālā iesprostotā frekvence: vada tikai viļņus, kas ir diezgan īsi, salīdzinot ar to biezumu. Hepburn publicē tabulas biezumu → minimālā lietderīgā frekvence ([DX Info Centre](https://www.dxinfocentre.com/propagation/luf.htm)); tā ir mīksta robeža, zem kuras signāls pakāpeniski samazinās.

| Led Biezums | Minimālais maksājamo maksājumu biežums |
|  61 m  | 962 MHz |
|  91 m  | 464 MHz |
|  122 m  | 276 MHz |
|  152 m  | 185 MHz |
|  183 m  | 133 MHz |
|  213 m  | 101 MHz |
|  305 m  | 53 MHz |

VHF diapazonā (bieži vien 120–300 m) tabulas vērtības diezgan labi atbilst likumam par konstanti; zem 100 m formula nepietiekami novērtē frekvenci līdz 30–40%. Pielāgošana pats (nav Hepburn publicētā formula), ar f in MHz un d metros:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

Praktiski: 2 m garumā cauruļvads bieži ir vismaz 170–180 m, 70 cm — apmēram puse. Formula, kas cirkulē uz neto, λmax = 0,014 · d^1.5, nāk no Kerr (1951, 1. nodaļa, ekv. 18) un ir derīga ar λ centimetros un stāvot, par cauruļvadu, kurā M krīt lineāri: ar šīm vienībām dod apmēram 2 m 180 m un apmēram 70 cm 89 m, kas ir tās pašas vērtības Hepburn tabulā, kas nāk no tā. Kerrs brīdina, ka tas nav tīkla griezums: pat viļņi vairākas reizes ilgāk cieš daudz no vada, un veids, kā "zaudēt" nedaudz var uzlabot uztveršanu pat ar stacijām no vada. 2019. gada IEEE Access pētījums apstiprina, ka klasiskās metodes (Kerr, Kukushkin) neapraksta īsto griezumu ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)): slieksnis būtu jāizmanto kā norāde, nevis kā siena.

## 4. Bez maksas pieejami dati

Vājais punkts ir vertikāla izšķirtspēja: starp 1000 un 850 hPa modeļiem dod līmeni ik pēc 25–50 hPa, tas ir ik pēc 200–450 m, tāpēc viņi labi redz inversijas līdz 500–2000 m, bet ne tievajiem kanāliem pie zemes. LIVETROPOVHF jau izmanto Open-Meteo, lai izobare, temperatūra līdz 850 hPa un inversija: indekss var sākt no tiem pašiem zvaniem.

| Avots | Kas dod | Rezolūcija | Indeksa izmantošana |
| [Open-Meteo, GFS](https://open-meteo.com/en/docs/gfs-api) | T, relatīvais mitrums, rasas punkts, ģeopotenciālais augstums pēc līmeņa (1000, 975, 950, 925, 900, 850 hPa...); robežslāņa augstums | 0,25° uz spiediena līmeni; 16 dienas, stundas līdz 120 h, atjaunot ik pēc 6 h | profils N un M, gradients, kanāla bāze un virsotne; jau programmā |
| [Open-Meteo, ECMWF](https://open-meteo.com/en/docs/ecmwf-api) | tādi paši līmeņi no atvērto datu modeļiem 0,25°; zeme dod rasas punktu |  0,25°  | otrais atzinums, kas noder modeļu saskaņotībai |
| [Open-Meteo, DWD ICON](https://open-meteo.com/en/docs/dwd-api) | T un relatīvais mitrums spiediena līmeņos; ICON- D2 ir 800, 900 un 925 hPa | ICON-ES aptuveni 7 km | 3. atzinums par tematu “Eiropa” |
| [Radiosunds, Univ. Wyoming](https://github.com/NCAR/RAOBget) | izmērītie profili, divas palaišanas dienā (00 un 12 UTC), teksta formāts katrai stacijai | daudz nozīmīgu līmeņu, pat ik pēc dažiem desmitiem metru | indeksa pārbaude; skatīt plānās caurules, ko modelis neredz |
| [GROANS, NOAA NCEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive) | vēsturiskais arhīvs apraides, fails uz staciju, vairāk atvasinātu parametru | kā iepriekš minēts | datu kalibrēšana un salīdzinājums ar iepriekšējiem punktiem |
| ERA5 (Copernicus) | Orālā atkārtota analīze kopš 1940, 37 līmeņi spiedienu | aptuveni 28 km | statistika un kalibrēšana, nav prognozes |

- Ar Open-Meteo ģeopotenciālo augstumu ir virs jūras līmeņa: līmeņi, kas ietilpst zem zemes ir jāizmet ([Open-Meteo, augšējā gaisa](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)).
- Uz zemes tiek pievienots T un mitrums 2 m: tie dod pirmo gradienta posmu, ka kur nakts virszemes vadi dzimst uz līdzenuma un uz jūras.
- Itālijas raidījumi Vaiomingas Universitātes arhīvā (weather.uwyo.edu/upperair/sounding.html): stacijas un numuri WMO tabulā.

| Stacija | W | Ģeogrāfiskais platums | Ģeogrāfiskais garums | Daļa |
| Udīns/kampoformido |  16044  |  46,03  |  13,18  |  94 m  |
| Milāna/Linate |  16080  |  45,43  |  9,28  |  103 m  |
| Jūras prakse |  16245  |  41,65  |  12,43  |  12 m  |
| Brindizi |  16320  |  40,65  |  17,95  |  10 m  |
| Trapāni/Birgi |  16429  |  37,92  |  12,50  | aptuveni 14 m |

Teksta formāts Wyoming dod katram līmenim spiedienu, augstumu, temperatūru, rasas punktu un mitrumu: pietiekami, lai N un M. Wyoming nav oficiāla saskarne, skripti tiek izmantoti kā [RAOBget of NCAR](https://github.com/NCAR/RAOBget); College of DuPage saglabā kopijas tekstā pēc stacijas ([piem., 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)).](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. Indeksi un publicētās metodes

Hepburn un F5LEN sākas no tām pašām fiziskajām sastāvdaļām, bet nepublicē pilnu algoritmu; 2008-2026 zinātniskie pētījumi tā vietā apraksta metodi soli pa solim, un no turienes ir vērts nokopēt recepti.

| Metode | Sākuma dati | Kas tiek aprēķināts | Noderīgas piezīmes mums |
| [Hepburn Tropo indekss](https://www.dxinfocentre.com/propagation/hti.htm) | Kanādas meteoroloģiskā dienesta neapstrādāti meteoroloģiskie modeļi (MSC, kā rakstīts tā kartēs; nevis GFS/NAM), kartes, kas veidotas ar GRADS, patentētu algoritmu ([storia](https://www.dxinfocentre.com/propagation/history.htm)) | 0–10 signāla izliekuma lineārā skala: 0 normāls, 1–2 superrefrakcija, 3–4 daži cauruļvadi, 5–6 izkliedēti cauruļvadi; negatīvas vērtības = subrefrakcija | atdarināmā skala, tāpēc trešais indekss uzreiz salīdzina ar pirmajiem diviem |
| [F5LEN, pasaules prognoze](https://tropo.f5len.org/WW/info.php) | reaģētspēja N vienībā; Eiropā Météo-France modelis ARPEGE, 7,5 km, vairāk līmeņu no 2 m līdz 1500 m ([autora](https://tropo.f5len.org/author/f5len/)piezīme) | Vidējais indekss saimniekam, 40 soļi no 3 stundas, 4 aprēķini dienā, 28 dienu arhīvs ([jaunā versija](https://tropo.f5len.org/?p=504)) | apstiprināt, ka tas kalpo modeli ar daudz zemu līmeni |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf) | 60 ECMWF modeļu līmeņi, no zemes līdz 3000 m | minimālais dN/dz katrā kolonnā: veikts zem −157 N/km, superrefrakcija zem −78,7 N/km; lamatu slāņa pamatne | klimatoloģija: Padana līdzenums Eiropas teritorijai ar vairāk cauruļvadiem visu gadu; Vidusjūra vasarā gandrīz 100%, ziemā zem 15%; Itālija vairāk nekā 30% vasaras naktīs |
| [Indermuehle un Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861) | VFS 0,25°, 21 spiediena līmenis + lauki pie 2 m | M = N + 0,157 z; minimālais dM/dz zem 3 km, cauruļvada pamatne un augšējā daļa, M deficīta dziļums; mazāk par 40 m | pārbaudīts 7 gadus 25 kanālos no 88 MHz līdz 2,68 GHz; laba jauda līdz apmēram 3 dienām iepriekš (AUC ap 0,74), tad samazinās; VFS ir labāka par ERA5 |
| [Hämäläinen et al., SVF 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf) | Harmonie-AROME, 750 m vai 2,5 km, 65 vai 90 līmeņi | dM/dh < 0 uz modeļa profila, salīdzinājums ar 4 līdz 59 m staru kūli | ir vairāk vertikāla nekā horizontāla izšķirtspēja; neviens modelis nesaskata kanālus zem 15 m; temperatūras gradienta laikā dominē mitrums |

Trešais amatieru radio pakalpojums jau ir aktīvs: [TROPCAST.EU](https://tropocast.eu/)of Artur SQ3A, 2 m un 70 cm Eiropā. No prognozes lapas ([tropocast.eu/prognose](https://tropocast.eu/forecast)):

- Dati: DWD ICON-EU (pase 0,0625°, aptuveni 7 km), noklusējuma avots; alternatīvi Kanādas GDPS (0,15°), mazāk detalizēts.
- Metode: N un M saskaņā ar ITU-R P.453 no profila starp slāni netālu no zemes un 850 hPa; atpazīšana slāņu, kanalizācijas uz zemes un augstu un krāsu robežvērtības ir pienācīga kalibrēšana autora, nav publicēts.
- Produkti: "pilnīgs" skats 0-3000 m, vairāk atsevišķu skatu "līdz zemei" un "augsts" (pašlaik tikai kartēs, kas jāiekļauj); punktu skaits 0–100 deviņās klasēs (zem 38 fona, 38–52 margināls, 53–58 vājš, 59–65 mērens, 66–72 labs, 73–80 ļoti labs, 81–88 augsts, 89–95 spēcīgs, 96–100 ļoti spēcīgs); prognozes līdz +72 h stundas laikā ar animāciju un arhīvu.
- Apgabali: Eiropa, Polija, Apvienotā Karaliste, Vācija, Francija, Spānija, Itālija, Čehija, Horvātija; saskarne arī itāļu valodā.
- Atkārtota izmantošana: kartes ietver ar API bezatslēgas iframe; ir arī dokumentēta API (OpenAPI un JSON shēma). Licence nav norādīta: Pirms izmantot to kā trešo avotu blakus Hepburn un F5LEN jums vajadzētu jautāt autoram.

Atšķirības no mūsu projekta: TROPOCAST apstājas pie 850 hPa (aptuveni 1,5 km), bet paaugstinātie cauruļvadi ierodas 2000 m un tālāk ( LA0BY gadījums 22/11/2018), un nesniedz indeksu joslai vai maršrutam. Nodarbības 0 līdz 100 var salīdzināt ar mūsu indeksu 0 līdz 10 tajās pašās dienās.

TROPOCAST attēla struktūra (no saites, ko nodrošina programmas autors): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, kur n ir prognozēšanas posms. Vietne aizliedz automātiski pacelt uz robotiem, tāpēc es nevarēju izlasīt attēlu; arī par to, pirms parādīt šīs kartes LIVETROPOVHF kalpo autora atļauju.

Notebook: [DJ5AR,](https://dj5ar.de/?cat=174)kategorija (Magonza, 21 raksts 2017–2023, 2 m-13 cm). Aprakstītās lietas: vairākas nedēļas no Īrijas (2023. gada 23. maijs–2. jūnijs), no FT8 līdz Baltkrievijai (2020. gada septembris), Itālija–Vācijai pāri Alpiem un Adrijas jūrai (2017. gada augusts). Atzīstot gadījumu "pārāk augsts" vadu (reversi 1000 m), ka jūs nevarat izmantot no zemes: vēl viens apstiprinājums, ka ir nepieciešams salīdzināt daļu no vada ar staciju. Tā izmanto radiofāras kā apertūras mēru (līdz 1028 km) un vertikālos profilus Parīzē, Bordo un DWD.

Kopumā kopējā recepte: vertikālais profils → N un M uz līmeni → slānis ar negatīvu dM/dh → bāzi, augšējo, biezumu un kanalizācijas → indeksu. Hepbērna "slepenā" daļa ir empīriska kalibrēšana par reāliem notikumiem, ko mēs varam pārtaisīt ar plankumiem.

## 6. Noderīgas formulas programmai

Papildus N un M (pamatfizikas sadaļa), tie kalpo, lai pārvietotos no vertikālā uz punktu skaitu un pēc tam pa ceļu. ITU formulas bija rekonstruētas ar daļēji slikti nolasītu PDF; 22. novembrī tika pārskatīti oficiālie teksti ([ITU-R P.452-18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453-14, P.526-16, P.372-18) un labots kods: Troposcatter 190 konstante (ne 190,1) plus gāzu absorbcija ar ρ = 3 g/m3; α ne zemāk par −34 (eq. 55a); cauruļvadu gāze ar ρ = 7,5 + 2,5·ω (eq. 9a); difrakcija uz pilnas sfēriskas zemes (eq. 23–37: vidējas gruntsūdens, augstuma funkcija ar ierobežojumu 2 + 20 baļķi K, interpolācija zem optiskās plūsmas); tvaika piesātinājuma spiediens P.453-14 (eq. 9, ar EF) vecās formulas vietā. Rumore P.372-18: 1. tabula un galaktikas atbilst; Mākslīgā trokšņa līknes ir līdz 250 MHz un galaktika līdz aptuveni 100 MHz, virs programmas paplašina tos pašus likumus.

1. Gradients starp diviem modeļa līmeņiem (h km, M M vienībā):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. Ķēdes stiprums un biezums: biezums d = augšējais – pamatne (augsnes vadam pamatne = augsne); izturība kā M deficīts starp pamatni un augšējo.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3. Zemes rādiusa ekvivalenta koeficients (P.452-18, 5. un 6.a q), ar ΔN = vidējais N kritums pirmajos km, pozitīvs, N/km:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

Ar ΔN = 40 jums ir apmēram 4/3 k (standarta atmosfēra); ar ΔN, kas tuvojas 157 k tendence bezgalību, tas ir, Zemes radio kļūst plakana: tas ir sākums vadu.

4. Augstas antenas h metru radiohorizonts ar rādiusu, kas vienāds ar\_e km:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5. Cauruļvada varbūtība platuma grādos (P.452-18, 2. q) laika procentos; φ = maršruta centra platums, μ1 un μ4 ņem vērā, cik tālu tas atrodas pa jūru:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

Pie 41° N, visi pa jūru (μ1 = μ4 = 1) dod apmēram 11% no laika: ir klimatiskā bāze, noderīga kā atsauces vērtība, no kuras atšķiras paredzamais indekss.

6. Troposcatter (P.452-18, ekv. 45; f MHz, d km, θ ceļa leņķis mrad, N0 zemes reaģētspēja maršruta centrā):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

Termins −0,15 · N0 parāda, kāpēc slapjais gaiss palīdz arī bez kanalizācijas: 50 papildu N vienības ir par 7,5 dB mazākas nekā zudumi. LIVETROPOVHF jau izmanto izkliedes zuduma G3YGF versiju.

7. Zudums kanālā (P.452-18, ekv. 46–51): fiksēts sakabes zudums plus leņķa miliradianta pavājinājums, kas aug ar kubikveida frekvences sakni (f in GHz, a\_e km):

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. No Bean and Dutton (1966) un Kerr (1951)

Divi klasiskie teksti dod trīs lietas, kas ir vajadzīgas uzreiz uz indeksu: gradienta sliekšņi jau tarate uz vienādiem spiediena līmeņiem VFS, kas ir vērts dB izmaiņas reaģētspēju, un ko laika apstākļi rada vai iznīcināt cauruļvadiem. *Radio meteoroloģija*, NBS 92. monogrāfija.

1. Spiediena slāņu profila klasifikācija (Bean un Dutton, 6.5. tabula, 248. lpp.; −dN/dh N/km). paredzēti radio viļņu garumiem, tos piemēro pēc modeļa principa:

| Autoceļš | Akcijas par | Apakšrefrakcija | nelaminēts | Superrefrakcija |
| 1000–850 hPa | 0–1,46 km | –dN/dh < 0 | no 20 līdz 60 | virs 100 |
| 850–700 hPa | 1,46–3,01 km | –dN/dh < 0 | no 20 līdz 50 | virs 80 |
| 700–600 hPa | 3,01–4,20 km | –dN/dh < 0 | no 15 līdz 40 | virs 70 |
| 600–500 hPa | 4,20–5,57 km | –dN/dh < 0 | no 10 līdz 30 | virs 50 |

2. Cik liela vērtība ir dB (Bean un Dutton, coup. 6, pp. 233–258), mērot no 70 līdz 180 MHz maršrutos aiz horizonta:

- Jebkurš slānis (uz zemes vai augstumā) salīdzinājumā ar "dūmaka" profilu rada signālu 10–25 dB līdz visiem procentiem; lielāks ieguvums rodas no augsti noliektiem slāņiem, iespējams, pateicoties fokusēšanas efektam (virziens 200 km).
- Zemes reaģētspēja Ns un pirmo kilometru samazinājums ΔN ir saistīti ar mēneša vidējo signālu ar koeficientu no 0,4 līdz 0,95, vidējo 0,7 (20 maršruti no 130 līdz 446 km, aptuveni 100 MHz).
- Gada cikls: 0,18 dB uz vienību N; dienas cikls no 0,2 līdz 1,1 dB uz vienību N, maksimālais maršrutos 175–200 km un ziemā.
- Misme kombinētais parametrs nosaka lauka variāciju kā 0,5 dB/(N/km) · (ekvivalents gradients − 40) plus termiskas stabilitātes termins; turbulences D parametrs (platība starp reālo profilu un adiabātisko, 0–3 km) tikai uzlabojas salīdzinājumā ar vienkāršo ΔN. Autoru secinājums: ΔN joprojām ir labākais vienkāršības parametrs.

3. Atsauces profils un anomālija (Bean un Dutton, 3.8. un 3.9. punkts). Atskaites CRPL eksponenciālā atmosfēra dod "normālo" profilu, sākot no Ns; A vienība mēra, cik daudz reālais profils atšķiras katrā daļā. Tas ir dabisks veids, kā pārveidot VFS profilu par anomāliju, ko likt kartē:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

Ar Ns = 313 jums ir ΔN aptuveni −42 N/km, kas ir ekvivalents rādiuss 4/3. Šeit arī atvasināts no Snell likuma par horizontālo rādiusu (4.23. punkts) gandrīz neatkarīgi no antenas augstuma.

4. Kur un kad ir dzimuši cauruļvadi (Bean un Dutton § 4.4.; Kerr cap. 3):

| Process | Stāvoklis | Ietekme |
| Brīdinājums | karsts un sauss gaiss no sauszemes, kas plūst uz aukstāku jūru (parasti vasaras piedurkne pēc laba laika dienām); galvenokārt skaita mitruma deficītu | intensīvs ūdensvads jūrā; |
| Nakts apstarošana | rāmas debesis, vājš vējš; zems mākoņu vai daudz mitruma to novērst | noveda pie zemes, kas dzimst pie saulrieta, aug vakarā un ātri pazūd pēc saullēkta, īpaši iekšpusē |
| Apmešanās vieta | gaiss, kas nāk no anticiklona, apsildāms un sauss, svaigā un mitrā gaisā; bieži plūdi kalnos un dienvidaustrumu nozarēs anticiklona | superrefraktīvais slānis vai augstais kanāls; gandrīz vienmēr slīpums ir lielāks nekā standarta līmenis atpakaļgaitā |
| Jūra un zeme | krasts, cirkulācija dažus simtus metru | vietējie cauruļvadi gar krastu |
| Pret cauruli | sajaukšana: zeme, ko silda saule no rīta vidus līdz pēcpusdienai, vējš uz zemi pār dažiem m/s nelīdzenā apvidū; nakts migla | varbūtība, ka kanāls gandrīz nekas |

5. Divas jaunas sastāvdaļas, lai mēģinātu. Vertikālā kustība: augšupejošās straumes konverģences zonās noņem un dzēš slāņus, iegrime nostiprina signālu (Moler and Holden: vidēji par 2,5 dB vairāk nekā ar kalnu gaisu, jūrā L un X joslā). Open-Meteo jau nodrošina vertikālo ātrumu spiediena līmeņos. Un spiediena tendence: uz ceļa no 140 km līdz 186 MHz signāls nolaidās, kad spiediens samazinājās uz raidītāju un uz ceļa centru un atgriezās augstu un bez evanescence, kad tas gāja uz augšu, tikai ar 2 (Bean un Dutton, attēlā 6.9, p. 241).

6. No Kerra. Papildus maksimālā viļņa garuma formulai (bāzes fizikas sadaļa), Kerr dod aptuvenu formulu M no vienīgās temperatūras, tvaika un augstuma (3.9. punkts, ar vecajām 79 un 4800 konstantēm tad aizstāj ar 77,6 un 4810), un ievēro, ka atpakaļgaitā vēja griezums var radīt viļņus, kas šūpojas augstumā, iespējams iemesls dziļas evakuācijas (§ 3.26). Itālijas avots ziņoja Bean un Dutton un meklēt: L. Bonavoglia (1958. gada decembris), "Korelācija starp meteoroloģiskajām parādībām un pavairošanu..."

## 8. No amatieru radio prezentācijas un tehniskajiem ziņojumiem (otrā sērija)

Programmas autora izsniegti septiņi dokumenti. Trīs kalpo tieši indeksam: W3SZ troposcatter "grīdai", LA0BY gadījumos, kad Hepburn nebija prognozējis, Hepburn ( LU7MC tulkojums) laikapstākļu klasifikācijai kanāliem.

1. Troposcatter: signāls, kas vienmēr pastāv. R. Rehr W3SZ, "Troposfērisko izkaisījumu (troposcatter) propagandēšana VHF, UHF, mikroviļņu un frekvences", Pack Rats Conference 2014; J. R. Duffey KK6MC, "Troposcatter propagandēšanas režīms un kā paredzēt pārklājumu VHF/UHF joslās", CSVHF 2017.

- Noderīga no aptuveni 100 līdz 700 km, no 144 MHz līdz 10 GHz; zudums pieaug par 30 baļķiem f ar frekvenci.
- Pirmkārt, tas skaita sākuma leņķi pret horizontu: katrs papildu grāds maksā aptuveni 9–12 dB ( M1CNK, Collins metode).
- Vasarā signāls ir vidēji par aptuveni 10 dB augstāks nekā ziemā; pēcpusdienā vidū apmēram par 5 dB zemāks nekā agrā rītā vai vakarā. KK6MC norāda, ka kopējais mainīgums ir 13–15 dB ar laiku, sezonu un laiku.
- Yeh metode, visvieglāk programmējamā (f MHz, d jūdzēs, θ izkliedes leņķis grādos, α ģeometriskais vidējais abu kūļu atverēm):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Larsena (1968) salīdzinājums ar 15 reāliem savienojumiem: vidējā kvadrāta kļūda 5,1 dB Collins, 7,4 Yeh, 8,1 NBS un CCIR, 16,8 Rider. Visas metodes uzlabojas, ja Ns uztur vienmērīgu (Yeh pilieni līdz 5,4 dB): dienas refrakcijas lielumam jābūt mazam, tāpēc tropo indeksam jāskatās uz slāņiem, ne tikai uz Ns. 15 savienojumu tabula ar izmērītajiem zudumiem (tostarp Feldberg–Monte Baldo, 322 km pie 957 MHz, 223,1 dB; Kipra–Izraēla, 319 km pie 77,5 MHz, 163,3 dB) ir labs izmēģinājumu stends mūsu aprēķiniem.
- KK6MC rada zudumus, ko tipiska stacija var pārsniegt uz 2 m un atbilstošo attālumu no 184 dB (FT-817 un 3 elementiem SSB, apmēram 130 km) līdz 215 dB (gariem bungām SSB, aptuveni 430 km) un 236 dB (kW un jagi, kas savienoti, apmēram 640 km). Tas ir veids, kā pārvērst indeksu dB uz jūdzēm nopelnīto.
- Gatavs kods: W3SZ Yeh and Collins programma (nitehawk.com/w3sz/YehCollins.zip) un Bob Atkins KA1GT kalkulators (bobatkins.com/radio/troposcatter.html).

Vēl viens avots uz izkliedes: I. Roberts ZS6BTE, "Troposfēriskā izkliedēšana", *Radio ZS*, 1984. gada decembris, ar 2013. gada piezīmi ([qsl.net/zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). Galvenokārt Noderīgā ģeometrija: izkliedes leņķis θ = θ0 – θ1 – θ2, ar θ0 = d/R, θ1 = (h1 – h\_ostacolo)/d1 + d1/2 R un tas pats otrai stacijai, R = 8446 km (4/3 no Zemes rādiusa). Tas ir gabals, kas tiek izmantots, lai aprēķinātu leņķus no profila zemes troposcatter "grīda". Citi dati: katrs leņķis vairāk maksā par 10 dB; pāreja no Ns 280 uz 300 samazina zudumus par aptuveni 4 dB; izkliedes tilpums ir no 300 līdz 2000 m 150 km, no 600 līdz 3000 m 300 km; laiks izkliedes tilpumā palielina signālu par aptuveni 20 dB. ar dotajām formulām (brīvās telpas termiņš 900 MHz un 872 km ir aptuveni 150 dB, nevis 125) netiek atgriezts skaitliskais piemērs, tāpēc formulas jāpārbauda uz W3SZ pirms to izmantošanas.

2. Cauruļvadu tipi (W. R. Hepburn, "Troposfēriskās propagācijas režīmi", dxinfocentre.com/pavairošana/tr-modes.htm, LU7MC tulkojums spāņu valodā). Aizpildiet tabulu ar Bean un Dutton, iesniedzot noderīgus dokumentus Itālijā:

| Veids | Kad | Piezīme par indeksu |
| Apstarošana | mierīgas un mierīgas naktis uz zemes | sekot zemi, pazūd neilgi pēc rītausmas |
| Augsts spiediens (apakšstāvā) | anticiklons | var ilgt visu dienu; naktī no apstarošanas zemāk var bloķēt tālu signālu, lai dienas var iet labāk |
| Priekšpuse | priekšā silta fronte, aiz aukstā fronte, uz ziemeļiem no stacionāra fronte | īss; slikti laika apstākļi traucē viņam |
| Kalnu nolaišanās | föhn, bora: gaiss nāk uz leju, sasilšana un žāvēšana | mēģināt bora Adrijas jūrā un föhn uz dienvidiem no Alpiem |
| Brīdinājums | karsts un sauss gaiss aukstā un mitrā augsnē, piemēram, pēc lietus | noveda pie zemes |
| Ieleja | karsta gaisa pār aukstā gaisa pieturas ielejā | var palikt dienā |
| Sanmarīno | karstu un sausu gaisu aukstākā ūdenī; ierodas krastā ar brīze | maksimums pēcpusdienā; pavasara un vasaras sākumā labākās sezonas |

Iztvaikošanas vadi jūrā (0–30 m) kalpo tikai aptuveni 3 GHz un neiekļūst iekšzemē: 144 un 432 MHz varat ignorēt. Augstie kalni ir barjera; tuksneši ir pārāk sausi.

3. Hepburn gadījumi nebija paredzami (S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", Nordic VUSHF 2019, no JO59IX līdz 500 m augstumā):

- 2018. gada 22. novembris: 70 cm līdz UA6 (apmēram 2600 km) 6–8 stundas. Hepbērna norādīja tikai Baltijas valstis un Poliju. Caurule bija redzama temperatūras kartēs pie 900 un 1500 m un beidzās tieši virs 2000 m; tā bija augstāka Norvēģijas pusē, un tikai LA0BY uz kalna iegāja. Uz zemes laiks nešķita kā trope: auksts un migla augšā.
- 2015. gada 11. februāris: 2 m un 70 cm virzienā uz 9A un S5 (apmēram 1500 km) cauri Alpiem, arī to Hepbērns neparedzēja.
- 2011. gada 15. oktobris: 2 m ar EA1 (2135 km) augstā kanālā, uz trim jūras posmiem un trīs grunts.
- Citi notikumi: 2003. gada 6.–11. novembrī uz austrumiem (2050 km), 2004. gada 10. decembrī uz Franciju.
- Lauku adreses: migla līdzenumā, brīvas virsotnes, maz vēja, mitra zeme. Normālais temperatūras gradients −6,5 °C/km; reālā gadījumā ( VK3KAQ) +7 °C 20 m.

Autora secinājums: Hepburn un F5LEN nepietiek, tie apkalpo profilus augstumā pa maršrutu. Mums: šie datumi ir pārbaudes gadījumi, kas jāpārrēķina ar modeļu vēsturiskajiem datiem, un ceļa indeksā jāsalīdzina cauruļvada kvota ar staciju kvotu.

4. Itālijas pieredze (G. IV3GBO, "VHF pavairošanas veidi SSB/CW", 2017): veikta no 450 līdz 3000 km, ar slāņiem no 450 līdz 3000 m; migla pagarināta kā pavediens; Adrijas jūras atveres virzienā uz Apūliju, Albāniju un Grieķiju. Izkaisīts no 100 līdz 500 km.

5. Ievads (P. Wilton M1CNK, "Introduction to VHF/UHF Propagation", 2013): 70 cm bieži ir labāki par 2 m; ar 10 m polu pie 144 MHz starta leņķis ir apmēram 3°, ar 20 m apmēram 1,5°.

6. NASA ziņojums (E. Hirschmann, "Troposfēriskā un jonosfēras ietekme uz radiofrekvenci (VHF-SHF) Communication", Goddard X-731-67-89, 1967): tas attiecas uz attāluma kļūdām pavadoņiem, nevis DX. Tas atgriež to pašu N formulu un eksponenciālo profilu (piemēram, Floridā: Ns 370, ΔN −55, konstante 0,161/km) un nosūta divus NBS avotus, lai meklētu: Bean un Thayer, "CRPL Exponential Reference Atmosphere", Monograph 4 (1959), un Bean, Horn un Ozanich, "Klimatiskās diagrammas un dati Radio Refractive Index ASV un pasaulē", Monograph 22 (1960).

## 9. No VHF/UHF DX grāmatas 2. nodaļa

I. White G3SEK (rediģēts), *The VHF/UHF DX Book*, DIR Publishing 1995 (ISBN 0 9520468 0 6; digitālā replika 2017), 2. nodaļa "VHF/UHF propaganda", 2.-13., 2.-22.-29. un 4.-11. lpp. Tas ir vistuvākais teksts radio amatieru pieredzei starp savāktajiem; apstiprina jau izmantotās robežvērtības un pievieno jaunus sinoptiskos noteikumus.

Numurs

- Tāda pati formula N (77.6 P/T + 3.73·10^5 e/T2); piemērs: 1000 hPa, 7 °C, un = 10 hPa bojājumi N = 324. Labi veidotā atmosfērā, piemēram, pēc negaisa, N ir gandrīz lineārs ap 39 vienībām/km pirmajos 2–3 km.
- Gradienti no 0 līdz −79 N/km ir normāli, zem −79 ir superrefrakcija, virs 0 subrefrakcijas. Grāmata uzstāj, ka garais DX nenāk no vienkāršās superrefrakcijas, kurai jāpaliek tieši uz visa ceļa, bet no kanāliem.
- Minimālais cauruļvada biezums, nedaudz piesardzīgāks nekā Kerr un Hepburn:

| Grupa | Minimālais biezums |
| 50 MHz |  400 m  |
| 70 MHz |  300 m  |
| 144 MHz |  200 m  |
| 432 MHz |  100 m  |
| 1.3 GHz |  50 m  |

- Attiecība nav proporcionāla viļņa garumam (aptuveni 65 λ pie 50 MHz, 220 λ pie 1.3 GHz), jo kanāla malas ir "šķeltas". Apvienotajā Karalistē cauruļvadi ir bieži 144 MHz frekvencē, bet gandrīz nekad 70 MHz frekvencē vai zemāk, un bieži vien 432 MHz ir spēcīgi, ja 144 MHz ir vāji.
- Cauruļvados signāls samazinās kā 1/d, nevis 1/d2: līdz 1000 km pavājinājums var būt līdzīgs brīvās telpas pavājinājumam.
- Minimālās stacijas jauda uz 2 m (100 W, 15 dBi jagi, 2 dB troksnis): 217 dB, kas ir aptuveni 400–500 km SSB vidējos apstākļos; CW apmēram 20 dB vairāk (4.-7. lpp.). Piekrītu KK6MC tabulai.
- Izkliede: zudumi pieaug par aptuveni 15 dB ar trīskāršu frekvenci un 10 dB katrai izkliedes leņķa pakāpei (Yeh formula; tekstā iegūtā konstante ir nelasāma, tāpēc tas jāņem no W3SZ versijas). "Ienākošais" izkliede no konvekcijas šūnām parasti aizņem aptuveni 500 km, 1000 km ar labām stacijām. Pēc spēcīgām lietusgāzēm un pagarinātiem apstākļiem visās joslās ir maz.

Cauruļvadu veidi

- Uz zemes: uz zemes tie slikti izplatās, jo augsne absorbējas un izplatās; uz mierīgas jūras nonāk tālu (Scilija–Kanārija, 2500 km, apmēram reizi gadā uz 2 m). Iztvaikošanas vadi bieži vien ir tikai apmēram 15 m augsti un kalpo mikroviļņiem.
- Augsts: 450—2000 m, sakarā ar garāko DX uz zemes; signāls "augsts" virs stacijām zemāk. "Loses" vadi dod mazāk spēcīgu signālu, bet pieļauj arī starpdistances stacijas.
- Cauruļvadiem ir precīzi virzieni, un tie var ilgt stundas vai dienas, pretēji sporādis E.

Jauni sinoptiskie noteikumi indeksam

- Anticiklona laikmets: gaiss krīt uz centru dažus metrus stundā, un reverss tiek pazemināts, attīstoties augstam spiedienam; lielākas atveres pienāk pēc vairākām pavairošanas dienām jau labi. Ar īstermiņa anticiklonu nepietiek.
- Augstspiediena mala: slānis ir kupols un malas ir zemākas, tāpēc stacijas kļūst labākas.
- Virziens: Ir novērots, ka garāki maršruti kursē pa izobariem vai pa dorsāliem (Flavell G3LTP, 1984), bet ir lieliski savienojumi arī radiālā virzienā no centra. 1987. gada 29.–30. augusta piemērs: no Anglijas pēc anticiklona maiņas atvere pagriezta par 180° divās dienās (Spānija, Francija, Itālija, Austrija, Norvēģija).
- Pret: biezs izobars (spēcīgs vējš), stratocumuliformi mākoņi slāņa malā (ierobežo augstāko frekvenci).
- Priekšpuses: auksta fronte piespiež karstu gaisu un izveido reversu gar priekšpusi; labākais virziens ir gar priekšpusi garām ejot, bet tas maz ilgst.
- Sezona: Apvienotajā Karalistē anticikloni biežāk ir vasaras beigās un rudens sākumā.
- Vizuālie pavedieni: dūmi, kas iesprostoti tumšā līnijā virs horizonta; augsta spiediena slāņi ar pamatni zem 1000 m norāda uz grunts vadu, pamatne virs 1000 m – uz augstu vadu.

Atsauces uz meklēšanu: R. G. Flavell G3LTP, "Pētījumi par plašu anticiklonisko propagandu...", *Radio Communication*, February 1984, p. 128; J. Gannaway G3YGF, "Troposfēriskā izkliedēšana", *Radio Communication*, September 1981, p. 710; P. W. Sollom G3BGL, "A Little Flutter W6ZGN

## 10. Pārbaudīts amatieru radio modelis: Biancomano un Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, "Praktiskais Troposfēras modelis VHF/UHF propagandēšanas prognozei", *National Weather Digest* 22:2, 1998. gada jūnijs, 15.–24. lpp., bez maksas [PDF no Nacionālās laikapstākļu asociācijas](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf).

- Dati: radiosundācijas 00 un 12 UTC 47 stacijās ASV austrumos, 1995—1997; zemes analīze līdz 700 hPa (sākumā līdz 400), N profilā, bez M. Izņemot krastus, salas un kalnus.
- Katram apsekojumam: cauruļvada atrašanās vieta un kvota, maksimālais viļņa garums, kas var izraisīt (bieži vien apmēram 100 reižu lielāku viļņa garumu), potenciālās temperatūras maksimālais gradients starp blakusesošajiem līmeņiem un starp vienu līmeni un otro iepriekš minēto (biezākajiem slāņiem). Zemes vadus neizmanto.
- Pirmais noteikums, noturība: stabilitātes indeksi (pacelšanas indekss, K, kopsummas, SWEAT) saglabājas 3 vienību robežās no apsekojuma pirms 12 stundām, un spiediena līmenis, kur potenciālā temperatūra pieaug vairāk, paliek apmēram tāds pats. Ja tā: uzlabojumu prognoze nākamajās 12 stundās.
- Otrais noteikums, dinamiskā vadība: robežslānis, kas nepalielinās, CAPE, biezuma variācija līdz 500 hPa trijos apsekojumos, slīpuma variācija robežās.
- Joslas slieksnis: cauruļvads, kas spēj nest 100 cm vai vairāk, norāda uz atvēršanos 432 MHz frekvencē, gandrīz noteikti uz 222 un bieži pat uz 144 MHz.
- Kosmosa filtrs: stacijas uz sešstūra režģa aptuveni 400–500 km garumā; prognozes vērtība ir tikai tad, ja vismaz 5 kontigue stacijas ir pozitīvas (ja jūs varat izlaist vienu). Ar 5 stacijām varbūtība, ka pozitīvs ir viltus nejaušs rezultāts, kļūst niecīga.
- QST slejas "Pasaule virs 50 MHz" notikumu pārbaude: 1996. gadā tika plānoti 48 notikumi 67 (71%); 12 no 15 galvenajiem notikumiem. Precizitāte līdz pat 70 % 432 MHz un aptuveni 60 % 144 MHz frekvencē pēc 12 stundām; tā nodrošina gandrīz dubultu faktisko notikumu skaitu; 9 kļūdas no 19 ir novēlotas, jo daudzi uzlabojumi veido mazāk nekā 12 stundas.
- Pievienots stacijām tālāk uz ziemeļiem: vēja rotējošs pulksteņa rādītāja virzienā kā papildu nosacījums.

No pilna PDF (piedāvā programmas autors) vēl trīs detaļas. To noteikums "biezums = 100 reizes viļņu garums" dod 600 m 6 m, 200 m 2 m, 125 m 1,25 m un 70 m 70 cm (fig. 3): 70 cm ir mazāk smags nekā Kerr un Hepburn (apmēram 89 m), 2 m tas sakrīt ar VHF/UHF DX grāmatu. Vējš, kas rotē pulksteņrādītāja virzienā, pēc autoru domām, ceļas augšup, it īpaši tāpēc, ka tas nes siltāku gaisu augstumā, kas liek N krišanai augt ar spiedienu; mitrums, kas nes līdzi, nešķiet destabilizējošs. Pielikums iegūst dN/dh slēgtā veidā konstanta siltuma gradienta atmosfērā ar strauju siltuma gradienta un mitruma lēcienu pie noteiktas kvotas: noderīgs kā testa gadījums mūsu kodam. To potenciālās temperatūras slīpuma pēdas nāk no K. H. Jehn, "Iespējamā refrakcijas indeksa izmantošana sinoptiskās skalas radio meteoroloģijā", *J. Meteoroloģija* 17, 1960, 264. lpp.

Ko veikt mūsu indeksu: (1) noturības termins: salīdzināt profilu ar profilu 12 stundas pirms, ka stundas prognozes mums ir bezmaksas; 2) telpas filtrs: punkts skaita tikai tad, ja uzlabojums aptver dažu simtu km, kas samazina viltus trauksmes; 3) stabilitātes rādītāji, ko aprēķina no temperatūras un rasas līdz 850, 700 un 500 hPa (Open-Meteo jau dod paceltu indeksu un CAPE); 4) vējš, kas rotē ar augstumu starp 1000 un 850 hPa; 5) to pārbaudes metode, kas ir tāda pati kā mēs vēlamies darīt ar programmas reklāmas.

Vēl viens avots tajā pašā paketē, maz izmanto mums: P. K. Rastogi, "Stratosfēras un troposfēras (S-T) pētījumi Millstone Hill", NASA 1984 (dokuments 1984019069): 440 MHz radars, kas mēra dzidra gaisa turbulenci 4—25 km attālumā, ar plānām kārtām, kas saistītas ar gravitācijas viļņiem un vēja griešana. Tas apstiprina, ka turbulence, kas rada izkliedi, ir noslāņota, bet tas rada pārāk lielas izredzes cauruļvadiem.

## 11. Trešā avotu kārta (20. novembris)

Augsta līmeņa vēsturiski gadījumi. J. Macoun OK1VR, saites 2 m no Sněžka samita (1602 m), 1958. gada rudens, 1959. gada raksts tulkots 2006. gadā ([ok2kkw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)). 1958. gada 27.–28. oktobrī pirmais Čehoslovākijas–Anglijas savienojums 145 MHz ( G5YV, 1220 km) un vēl daudz vairāk starp 1100 un 1250 km, ar stabiliem signāliem pēc 23. Autors to piedēvē divām uzspiestām inversijām, augšējā apmēram 2200 m, signālam atgriežoties uz zemes tikai tur, kur izšķīst apakšējais slānis; stāvoklis, ko veicina lēns anticiklons no Atlantijas okeāna uz kontinentu. No mājām (Prague) tās maksimums bija 530 km: tikai stacijas augstumā iegāja caurulē. Tas ir vēl viens verifikācijas gadījums (piemēram, LA0BY 2018) un apstiprina, ka indeksam būtu jāmeklē dati par vairāk nekā 2000 m un jāsalīdzina ar stacijas daļu.

Vēsturiskās bibliotēkas NBS (izmanto, lai meklētu klasiskos darbus, nevis formulas):

- W. Nupen, *Bibliogrāfija par radioviļņu izplatīšanos troposfērā*, NBS Tehniskā piezīme 304, 1965, 356 lappuses, ar indeksu tematiem (arī "nosacījumu prognoze") un pēc valsts. Itāļu ieraksti: L. Sacco, "Radio savienojums atmosfēras superrefrakcijas režīmā", *High Frequency* 24(6), 1955. gada decembris (itāļu pētniecība: superrefrakcija zem 500 m biežas dienas un nakts); S. Silleni, *Annali di Geofisica* 8(1), 1955; J. M. Clara e A. Antinori, prova tra Sardegna e Minorca da 238 a 297 MHz (390 km), *Electrical Communication*33, 1956. A. F. Clarke (Fort Huachuca, 1956) izdeva dienas prognozi par maksimālo viļņa garumu, kas iesprostojās nākamajā rītā, no mitruma, temperatūras, spiediena un vēja prognozēm.
- A. L. Abbott, *Troposfēras radioviļņu izkliedes bibliogrāfija*, NBS, materiāls līdz 1960. gada janvārim, aptuveni 670 preces, kas pasūtītas gadā: noderīgs troposcatter daļai.

Atklāšana un novērojumi: F. W. Odenthal DK5EQ, "Ausbreitung 2m – Eine Einführung" (31/05/2024): gandrīz optisks līdz 200 km, tropo uz kanālu vai refrakciju aiz, ar augstu spiedienu un pavasarī un rudenī; min VHF lapu DM2HB un piezīmes DK5YA uz tropo un es. A. Johnson VK3DXE blogā EI7GL (2021. gada decembris): ar WSPR VHF ceļu tūkstošiem km Austrālijā ir atklātas; Trope ir ne tikai vasarā. Diskusija par VHF marīnu un AIS ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)): diapazons 1,23· h(ft) jūras jūdzes uz antenu, izkliede līdz aptuveni 50 nm, tipisks tropes uzlabojums 100–400 nm; cauruļvadi ir atzīti, jo attālās stacijas nāk tikpat spēcīgas kā kaimiņi. Chelmsford ARS News, 1983. gada decembris: konkursos 2 m Anglijas austrumu piekrastē stacijas ir priviliģēts jūras ceļš uz kontinentu.

ARRL, *The Radio Amateur's VHF Manual*, 1972, cap. 2 "Propagation Beyond the Horizon" (17.–19. lpp.). Jau zināmie darbības uzgaļi (seko augsta spiediena zonām, kas pārvietojas lēni un pāri visām to aizmugurējām malām; rudens sākumā vislabākais periods; piekrastes zonas, lielie ezeri un upju ielejas, ko veicina stratifikācija). Divi jauni dati: 1) attēls. 2-4 salīdzina standarta atmosfēru (relatīvais mitrums 70% līdz 12000 pēdām, minimālā refrakcija) ar tipisku spēcīgas refrakcijas rādiusu, ar skaidru temperatūras pārrāvuma un sajaukuma attiecību ap 4000 pēdu (apmēram 1200 m): tas ir paraksts, ka mūsu aprēķiniem ir jāatzīst VFS līmeņos; (2) patiesi cauruļvadi ir reti zem 144 MHz un visbiežāk UHF, atbilstoši minimālajam biezumam. Noderīga QST bibliogrāfija meklēt: Hull, "Gaisa-masas apstākļi un UHF viļņu salikšana" (1935. gada jūnijs, 1937. gada maijs); Hoisington, "Divu metālu atveru bezsamaņas prognoze" (1949. gada oktobris); Collier, "Upper-Gaisa apstākļi 2-Meter DX" (1955. gada septembris); Botts, "Nakts atcerēties" (1970. gada janvāris, diriģēts uz zemes).

PA0EHG, lapa "Tropo" ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): kopsavilkums troposcatter no G3YGF rakstu, jau rokasgrāmatā. Izmantot divus praktiskus noteikumus, lai kontrolētu izkliedes aprēķinu: aptuveni 10 dB papildu zudumi katram leņķim pie katras stacijas apvāršņa un vairāk nekā 100 km apmēram 9 dB vairāk ik pēc 100 km maršruta (60 jūdzes); kopējais tilpums sākas no aptuveni 600 m 100 km attālumā un no aptuveni 9 km 480 km attālumā. Tas neattiecas uz uzvedību, aptaujas un refrakcijas slieksni.

PA0EHG, PROPBER kalkulators ([page](https://pa0ehg.com/propagat1.htm), propber.exe programma, ko nodrošina programmas autors). Tā ir 36 kB DOS programma, kas uzrakstīta BASIC un apkopota ar Microsoft BASIC kompilatoru 5.60 (1985): tā darbojas tikai zem DOS vai DOSBox. No iekšējiem tekstiem: prasa frekvenci, attālumu (vai abus vietrāžus, arī vecajā QRA formātā "E-lokators") katrai antenas augstuma stacijai, trokšņa skaitli, FI joslu, jaudu, šķērsli (augstums un attālums) un antenas pieaugumu (0 = līdzības diametra aprēķins, efektivitāte 50%). Atgriež maksimālo attālumu optikā, pareizos optiskos zudumus tvaika absorbēšanai (0,1 dB/km no 24 GHz), troposcatter zudumu "korektu vietām" (t.i., horizonta leņķiem, ko dod šķēršļi) un S/N attiecību divās nozīmēs, arī "ar maksimālo anomālo izplatīšanos". Skaitliskās konstantes nav nolasāmas apkopotajā izpildāmajā dokumentā. Ieteicamā metode augstām joslām ir noderīga arī mums: aprēķina kalibrēšana ar izmērītu saiti (izmērītā un aprēķinātā S/N starpība) un tās pašas starpības piemērošana citai joslai. Tas ir tas pats mūsu aprēķinu modelis, bet tas izmanto S RTM1 profilu, nevis tikai vienu šķērsli un VFS prognozi, nevis fiksētu rezervi.

PROPBER, konstantes lasīt izpildāmā (skaitļi Microsoft Binary Format formātā). πr, datu tabulā blakus tekstiem: 8,68589 (= 20/ln 10, t. i., 20 baļķi aprēķināti ar naturālo logaritmu), 32,5 (brīva telpa: 32,5 + 20 log f\[MHz\] + 20 log d\[km\]), 24000 un 0,1 plus 40000 un 0,3 (vapora: 0,1 dB/km no 24 GHz, 0,3 dB/km no 40 GHz) Iespējama, jo atkārtojat, bet koda iekšpusē: −69,5 (tas pats konstantais termins kā Yeh sānu izlādes formula VHF/UHF DX Book), 332 (augsnes refrakcija), 8733 km (ekvivalents rādiuss, k ≈ 1,37). Precīza izkliedes formula nav rekonstruējama, neizjaucot programmu. T0

PROPBER, formulas rekonstruētas pēc koda (apkopotās daļas demontēšana; operācijas tiek izsauktas uz BASIC bibliotēku ar operatoriem atmiņā, kuru vērtības dubultprecizitātē ir atšifrētas). Bibliotēkā atrastās konstantes −69,5, 332 un 8733 bija viltus pozitīvi.

- Optiskais attālums: d = 5 · ( Tas ir dāsnāks nekā klasiskais 4,12 (k = 4/3): ir līdzvērtīgs k ≈ 2.
- Optikas zudumi: L = 32, 5 + 20 log f\[MHz\] + 20 log d\[km\], plus 0,1 dB/km virs 24 GHz un 0,3 dB/km virs 40 GHz.
- Troposcatter: Lts = L + 23 + 10 log f + 0,0674·d, t.i., 55,5 + 30 log f + 20 log d + 0,0674·d ( G3YGF forma ar konstanti 2 dB augstāku). Nav leņķa termins vai reaģētspēja.
- Labojums "attiecībā uz vietnēm": programma prasa šķērsli un attālumu, bet bloks, kas tos izmanto, ir tukšs; korekcijas mainīgais paliek 0, tāpēc "pareizs" zaudējums ir vienāds ar izkliedes.
- Troksnis: T = 290·(10^(NF/10) − 1), N = −228,6 + 10 log T + 10 log B (dBW), bez antenas temperatūras.
- S/N = 10 baļķi P + G\_tx − L + G\_rx − N. "Ar maksimālu anomālu pavairošanu" izkliedes vietā izmanto optiskos zudumus, t. i., ideālu cauruļvadu korpusu bez zudumiem ārpus brīvās telpas.

Salīdzinājums: 300 km un 144 MHz PROPBER dod aptuveni 189,9 dB, G3YGF 187,9, mūsu P.452 187,8 (ar nulles horizonta leņķiem): izkliede sakrīt 2 dB robežās. Mūsu aprēķins papildina horizonta leņķus no profila, antenas temperatūras un "maksimālā" vietā noteica paredzamo slāni ar paliekošo pārošanos un pavājināšanos.

Rohde & Schwarz, P. Denisowski, "Understanding VHF Propagation", Izglītības piezīme, 2023. gada aprīlis (24 lappuses, izplatīšana). Apstiprina jau savāktos punktus (vadīts kā divdimensionāls viļņvads, signāls, kas lineāri krītas ar attālumu līdz 1500 km un vairāk, ilgumu no stundām līdz dienām; labs grunts uz jūras un maz uz sauszemes; paaugstināts no dažiem simtiem metru līdz dažiem km). Divi derīgi novērojumi indeksam: 1) attīstība laikā: jaunveidotais kanāls bieži nes tikai augstākās frekvences un, sabiezējot, sāk nest pat vismazāko; tāpēc stundas prognozei vajadzētu parādīt 432 MHz "iepriekš" 144 MHz, kad slānis aug; 2) staciju atrašanās vieta: vislabākie apstākļi ir starp stacijām, kas atrodas tuvu kanāla galiem, bet tie, kas gar augsta vada ceļu bieži neizdodas iekļūt, ja vien nav veikts "kas zaudē". Cauruļvadi pārvietojas ar atmosfēras laiku.

Vietne PA0EHG, citas lapas. Attiecībā uz trope, PathProfile M. Willis G0MJW ([scheda](https://pa0ehg.com/srtmp.htm), [download](http://www.mike-willis.com/Software/ASTERPathprofile.zip)): profils maršruta no sagaidāmo kopējo zaudējumu un kvotu failiem, ko uzskata par uzticamu troposcatter mikroviļņu; neārstē izkliedi no lidmašīnām vai lietus. Noderīga kā otrā atsauce, lai salīdzinātu mūsu S RTM1 profilus un zaudējumus. Pārējā objektā ietilpst EME, mikroviļņu krāsns, lidlauks (AirScout, SM7LCB kartes) un pasākumi 80 m.

PathProfile by G0MJW (Mike Willis), ASTER un SRTM pakotnes un SiteData (piegādā programmas autors; [](https://www.mike-willis.com/software.html)programmatūras lapa). Logi programma 2018: profils no ASTER vai SRTM uz 1" vai 3", zaudējumi optikā, difrakcija, troposcatter (derīga līdz 1000 km), kanalizācijas un gāzveida, ar procentu no laika, k mediāna no maršruta, daļa no ceļa uz jūru un attālums no krasta: struktūra ir tā, ka P.452/P.1812, bet palīdzību (kompresēts.chm failu) nevarēja lasīt šeit. SiteData konvertē atrašanās vietas un dod daļu no GT OPO30.

Visnoderīgākā daļa ir datu faili, kas iekļauti: ITU-R P.453 statistiskās kartes uz 1,5° (121 rindas no 90°N līdz 90°S, 241 kolonna no 0° uz austrumiem): gradients dN/dz pirmajā 65 m, kas pārsniegts 1, 10, 50, 90 un 99% no laika; augsnes vadu klātbūtnes, biezuma un intensitātes procentuālā daļa; tiem, kuriem ir liela klātbūtne, bāzes daļa, biezums, intensitāte un maksimālais augstums; plus slapjais termins un temperatūra. Interpolētās gada vērtības:

| Vieta | Sauszemes cauruļvads: % / biezums / intensitāte | Augsts cauruļvads: % / bāzes / biezums / intensitāte | dN/dz pārsniedza 10% no laika |
| Neapole |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | −380 N/km |
| Tirēnu (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | −674 N/km |
| Trapāni |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | −520 N/km |
| Brindizi |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | −405 N/km |
| Adrijas jūra (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | −399 N/km |
| Milāna |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | −172 N/km |
| Udīns |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | −176 N/km |

Mūsu indekss ir gatavs klimata atsauces: biezums un intensitāte tipisks cauruļvadiem, nevis 50 m fiksēto potenciālo vadu, un anomālija "šodien, salīdzinot ar normālu" katrā zonā. ITU dati: bezmaksas nekomerciālai izmantošanai, pirms to iekļaušanas publiskā versijā. ZO NES01.DEM fails (58 MB, 2' koordinātu tīkls) satur klimata zonas iekšzemē/piekrastē/jūrā; formāts nav dokumentēts, un es to neatkodēju. Vilisa vienādojumu lapā ir tikai fiziskas konstantes, brīva telpa un antenu pieaugums.

Ceturtā avotu kārta (20. novembris).

Indermuehle un Suzuki 2026, GFS cauruļvadu prognoze Mērčisona CSIRO observatorijai (arXiv 2608.12861): tas ir vistuvākais avots LIVETROPOVHF. N profili no VFS līdz 0,25° (virsma līdz 2 m, tad izobariskais līmenis, zem nomestās zemes); katrai minimālajai kolonnai dM/dz zem 3 km, bāzes un kanāla augšpuse, integrēts M deficīts; tad P.452-16 un P.1812-6 ar GLO-90 25 maršrutiem no 98 līdz 1005 km, no 88 MHz līdz 2,68 GHz, katru stundu. Rezultāti: vidējais AUC 0,72 uz 25 kanāliem; P.452 p laika procents jāizmanto kā kalibrēšanas konstante (choice p = 20 %), jo ITU modelis ar tūlītēju ΔN nav definēts caurulē; prasme saglabājas gandrīz līdz 3 dienām iepriekš. Uz 162 MHz (AIS) saņemtajiem maršrutiem ir augstākā cauruļvada frakcija 67 % (0,26 pret 0,16) un lielākais cauruļvada dziļums 49 % (28 pret 19 M) attiecībā uz kontroli: uz jūras ūdensvads ir gandrīz vienmēr, pieņem lēmumu par zemes gabalu. VFS saredz pusi laika uz zemes salīdzinājumā ar ERA5 (4,5% pret 10,4% šūnu stundu), bet jūrā redz vairāk (73% pret 56%). "Koridora" indekss (nepārtraukti cauruļvadi maršruta garumā, slāņi ar minimālo biezumu 40 m) uzlabo rezultātu: apstiprina, ka maršruta nepārtrauktība ir pareizais kritērijs.

Longley and Rice, ESSA ERL 79-ITS 67 (1968): metode neregulāram reljefam no 20 MHz līdz 20 GHz (pēc ITM), ar augsnes bojājumiem Δh, Ns no 250 līdz 400, ekvivalents rādiuss 8497 km (4/3), horizonti, difrakcija, izkliede uz priekšu un laika un klimata mainība. Statistiskā atsauce, nevis momentāna.

ITU-R P.834-2: rādiusa izliekums, Zemes rādiusa ekvivalents k·a, M = N + h/a (h m, tūkstošos km), eksponenciālais modelis; nekas jauns, kā jau izmantots.

AGARD CP-70 (1970), troposferiskā izplatīšanās: bieži sastopami plāni slāņi no 1 līdz 100 m ar spēcīgiem gradientiem; augsta slāņa atstarošanas koeficients ir apmēram proporcionāls viļņa garumam, ar nejaušu dispersiju aptuveni 14 dB. Mēra līdz 90 MHz: samazinājums par 9–10 dB, kas saistīts ar N lēcienu, koncentrēts apmēram 350 m augstumā mākoņu slāņa augšdaļā. Aukstās frontes pāreja novērš slāņus: paliek difrakcija.

U.S. URSI ziņojums, Komisija 2 (1960): vidējais biezums 60 līdz 210 m augstiem slāņiem (ņemot vērā P.453), minimālais slīpums 157–160 N/km vadam.

Buscemi K2OVS, VHF izplatīšanās ceļvedis: tipiski attālumi apvērsei un kanālam joslā, cauruļvadiem, kas saistīti ar augstu stacionāro spiedienu, maksimāli rītausmā.

Wolinsky-Mancini un citi 2024, AIS Kanāriju salās: AIS radio un uztveršana; vadi ir daudz biežāk siltos mēnešos (apvērse aliseans).

Nigērijas un Malaizijas raksti (Ekah, Iloke, Mat, Igwe): vietējās korelācijas starp temperatūru, mitrumu un izmērītajiem zudumiem, vāji un pretrunīgi (r −0,09 līdz 0,27); noderīgi tikai kā apstiprinājums tam, ka ar zemes izmēriem vien nepietiek.

Herben 1984, tēze TU Eindhoven (mikroonds): 30 GHz uz 8,2 km un OTS pavadonis uz 11,6 GHz; dzirksteles turbulencei, multi-path un lietus. Mikroviļņu krāsnīs dinamiskos zaudējumos dominē lietus un turbulence, nevis cauruļvadi.

Baars, The Paraboloidal Reflector Antenna (mikroonds): līdzības G = η·(πD/λ)2 pieaugums ar tipisku efektivitāti 0,5–0,65; virsmas kļūdu zudums saskaņā ar Ruze, ηs = exp(−(4πε/λ)2). Tas palīdzētu pievienot līdzības kā alternatīvu Yagi DL6WU 1296 MHz un tālāk.

Jau analizēts vai ar sliktu saturu: Hirschmann NASA X-731-67-89 (double), Reid's VHF ST radars (vēja un turbulences, nevadīts), Scribd slide un vispārīgas piezīmes uz troposfēras.

Piektā avotu kārta (20. novembris, vakars).

Rogers 1997, Radio Science 32(1): Bayesian aplēse par augsta kanāla parametriem (bāzes ātrums, M deficīts, biezums) no pavairošanas faktoriem, kas mērīti ārpus horizonta, ar parabolisko vienādojuma modeli; GOSAR eksperiments Kalifornijā 143, 263 un 375 MHz frekvencē, salīdzinot ar radioviļņiem. Signāls lielā mērā ir atkarīgs no bāzes un M deficīta (deficīta korekcija 0,81 līdz 143 MHz), gandrīz vispār no biezuma (-0,17): biezumu nevar iegūt no signāla. Mums: aprēķins B (klimata biezums) skaitās mazāks nekā deficīts un daļa; un metode ir pamats atbalsta birojs, tas ir, lai iegūtu kanālu no vairākām frekvencēm.

Moler and Holden 1960, "Troposfēriskā izkliede un atmosfēras cirkulācija", J. Res. NBS 64D, 81. lpp. un pēc (lasīt oriģināla 22/11/2026, doi 10.6028/jres.064d.014). Kalifornijas jūras maršruti uz Point Loma (San Diego): Santabarbara 190 jūdzes, Point Mugu 144, San Clemente 78; 1365 MHz un 9380 MHz, nevis VHF; 1958. gada janvāris–aprīlis vidēji 1 minūte. 26. marta gadījums: aptuveni 20 dB pastiprinājums uz apmēram 3 stundām virzienā uz pusdienlaiku, kad ciklonisks virpuļviesulis vājina un sāk jūras vēsumu un noslīd uz leju; pie 15 konverģence tiek reformēta un signāls nokrīt apmēram 22 dB no maksimālā. Statistika (1. tabula, 500 hPa, 33 gadījumi): ar gaisu, kas pazemina vidējo signālu, ir augstāka par aptuveni 2,5 dB, salīdzinot ar gaisa pieaugumu, L un X joslā. Signāls seko starpībai, kas aprēķināta līdz 500 hPa variāciju zīmē un amplitūdā bez korelācijas koeficienta. Zemi slāņi (zem apmēram 600 m) veicina daļēju atstarošanos uz izkliedi. Anticikloni un frontes netiek apstrādātas, un iepriekš minētie "ekskursijas līdz 60 dB" nav: tie nāca no lietotu kopsavilkumu. Mums: vertikālā kustība ir labs prognozētājs, un +1,5 dB pazeminājumam un −2 dB kalnup gaisam ir tādā pašā secībā kā vidējā izmērītā starpība (2,5 dB); 20 dB ir viens gadījums, nevis tipiska vērtība, un mikroviļņu krāsnī.

Banafaa un Muqaibel 2025, IEEE Access: cauruļvadu izpēte (iztvaikošanas modeļi, paraboliskais vienādojums, radaru aprēķini, GNSS un lidors) un klasifikācija ar automātisku mācīšanos par reāliem pasākumiem; Atbalsta vektoru iekārtas sniedz vislabākos rezultātus. Noderīgi kā bibliogrāfija; dati ir par Persijas līci.

ITU-R P.834-7 (2015): rekomendāciju par troposferālo refrakciju atjauninātā versija; neviena jauna formula, kā tikai tā, kas jau izmantota.

Abbott, NBS bibliogrāfija uz Troposferous Scatter (GOVPUB- C13): jau trešajā kārtā. Slaids Scribd uz kanāliem: vispārējs saturs. Raksts ScienceDirect 0021916987900304 neizdevās atvērt.

Sestā kārta (20. novembris, nakts): Bean and Dutton, ARRL Handbook 2022 un 2023, Balanis.

Bean and Dutton, Radio Meteorology (6. kap.), daļas vēl netiek izmantotas. Lauka jutība pret Ns: 0,18 dB uz N vienību gada ciklā; 0,14–0,24 dB/N dienas ciklā (maksimāli agri no rīta), līdz 0,2–1,1 dB/N maršrutos 175–200 km ziemā; korekcijas 0,8–0,95. Parametrs, kas sastāv no Misme: M = a(g\_e − 40 + b·ΔW^n) ar a = 0,5 dB uz N/km, kas apvieno ekvivalentu gradientu un termisko stabilitāti ΔW (darbs, lai paceltu gaisu 1 km attālumā): Sahārā 471 MHz attālumā 160 km, vienīgā stabilitāte ir 20 dB no janvāra līdz jūnijam gandrīz vienādā slīpumā. potenciālais refrakcijas indekss K (N, par kuru ziņots adiabatiski līdz 1000 hPa): uzsver gaisa masas un anticiklonisko noslīdēšanu (Flavell un Lane).

ARRL Handbook 2022 (99a ed.), § 19.4.3–19.4.4. Noderīgas inversijas starp 250 un 2000 m; apstarojuma inversijas (serums, mierīgs gaiss, sausa augsne) reti noved pie taisnības; lēns anticikloni dod visplašākos vadus uz nolaidenumu, ar inversiju starp 500 un 3000 m, spēcīgāku vakarā un no rīta; ziemeļu puslodē labākie maršruti ir uz dienvidiem no augstā centra. Karsta fronte: labi apstākļi dažus simtus km uz priekšu; Auksta fronte: spēcīga, bet nestabila reversa aiz priekšpuses, paralēla. Fūns (arī Alpos) un zemes brīze (auksts gaiss uz zemes, karsts 200—300 m; stiepjas pāri 800 km gar krastu). Scirocco: karsts un sauss gaiss no Sahāras virs vēsāka jūras gaisa, ļoti spēcīgas inversijas no Vidusjūras austrumu uz Gibraltāru, iespējams iemesls ierakstu vairāk 1500 km mikroviļņu. Troposcatter: teorētiskā robeža ap 800 km, maksimālā tipiskā puse. 100 ed. (2023) teksts ir īsāks: inversijas saullēkta un saulrieta laikā līdz +20 dB un +20-50% caurplūdums; 1200 jūdzes Lielajos ezeros, tipiski 400–600; iesaka radio mobilo (ITM modelis) Hepbērna aptvērumam un kartēm.

Balanis, Antenas teorija (4a ed.), § 10.3.3.: Viezbicke tabula (NBS TN 688, 1976) priekš Yagi optimizēta pie 400 MHz: bums 0.4 / 0.8 / 1.2 / 2.2 / 3.2 / 4.2 λ → 7.1 / 9,2 / 10.2 / 12.25 / 13,4 / 14.2 dBd. Mūsu DL6WU formula (8,6 + 8 log10 bums/λ) dod 1–1,7 dB mazāk no 0,8 līdz 4.2 λ un atgriežas saskaņā ar DL6WU tabulām līdz 9,5 λ: piesardzīgs novērtējums. Ar pareizo formulu oriģinālajā (9,4 + 7,8 log10 bums/λ, no 22. novembra) izmetums nokrītas zem 0,2 dB starp 1,2 un 4,2 λ (12,1 dBd līdz 2,2 λ pret 12,25) un paliek 0,6 dB līdz 0,8 λ.

Septītā kārta (21. novembris): QST, QEX, W3SZ, GitHub repozitorijs.

QST 2000–2025 (14 numuri, 10 gultas ar teksta atpazīšanu). Tikai ziņojumi par pasauli virs 50 MHz, kas ir noderīgi kā gadījumi, lai apstiprinātu modeli: 2000. gada 5.–9. septembris, 144 MHz un tālāk ASV austrumu pusē; 2022. gada 14.–15. augusts, Manitoba–Kansas/Missouri ar augstu spiedienu karstā dienā, no 144 līdz 1296 MHz gandrīz viss FT8, Kanādas rekords 10 GHz 1228,56 km ( VE4MA – N0OY) Q65, signāli līdz 1941 km; APRS pavairošanas karte parādīja to pašu teritoriju. QST 2022. gada novembrī iesaka Hepburn kartes. Nav tehniska panta par tropo prognozi.

QEX un K1JT, Franke un Taylor (QEX 2020) FT4/FT8 režīmos: FT8 dekodēšanas robežvērtības −20,8 dB 2500 Hz kanālā bez evanescences, −18,6 dB ar evanscenci vidējos platuma grādos; FT4 −17,5 dB. Pārējie QEX 2023 un 2026 numuri neārstē tropo (trīs cipari 2023 ir nelasīti skenējumi). W3SZ 2019: lēnā režīma robežvērtības ( JT4A −23, JT9A −27, JT65A −25, QRA64A −26, FT8 −21 dB) un izvēles kritēriji starp jutību un frekvences nobīdes pielaidi.

GitHub. TropoLink (LukiBox, Apache-2.0): Troposcatter maršrutu ar P.617-5 (ar N0 un ΔN), NBS TN101 un ITM sānos konstrukcija un to atšķirība parādīta kā nenoteiktība; P.617-5: L = F + 22 log f + 35 log θ + 17 log d + Lc, F = 0,18· N0 ·e^(−hs/7,35) − 0,23·ΔN, Lc = 0,07·e^(0,055(Gt+Gr)). uni-f/troposkāts ( DL2FZN 2013, no VHF/UHF rokasgrāmatas RSGB): Lts = 24,44 + 4,283 ln f + 0,06656 d virs brīvās vietas, slīpums pie PROPBER (0,0674 dB/km). QRadioPredict: ITM un ITWOM 3.0 ar S RTM3, augsnes izmantošana Corins un NEC diagrammas. Neattiecas: Tropos (weather app), opera-adt/tropo un troposim (Troposferic kavēšanās for InSAR), tgbot\_troposcatter (militārā stacija), sdr-troposcatter-gui (vadības panelis), Troposcatter-Propagation-Delay ( ERA5 aizture), Radio-sakaru projekts (didaktiskais simulators), PyLayers (kanāls).

Astotais aplis (21. novembris): NTIA un ITU modeļi GitHub. NTIA/itm (Longley-Rice, ITS): 20 MHz–20 GHz, brīvas telpas zudumi, difrakcija un troposcatter ar augsnes profilu, 0,5–3000 m spailes, virsmas reaģētspēja N0 (minimitāte samazināta līdz jūras līmenim, 250–400), septiņi klimatiskie apstākļi, augsnes konstantes un dažādi režīmi 1 skats, 2 difrakcija, 3 troposcatter; C++ ar.NET Framework 4.7.2 norobežojums. Tas nemodelē kanālus: tas kalpo kā atsauces "normāla atmosfēra" ar statistisko mainību. NTIA/p528 un eeveetza/p528 (MATLAB): ITU-R P.528-5 gaisa un augsnes savienojumiem, 100 MHz–30 GHz, 1,5–20 000 m termināli, p 1–99%;.NET 4.8. korpuss. Iespējamā izmantošana: gaisa izkliede (divi zemes maršruti– lidmašīnas un radara sekcija). eeveetza/p2001 (MATLAB): ITU-R P.2001-6, pilna lauka modelis ar difrakciju, troposcatter, patoloģiska pavairošana (vadīt un slāņi, Lba) un E sporadic, % laika 0–100 %; ITU digitālās kartes (DN\_Median, DN\_SupSlope, DN\_SubSlope, dndz\_01, TropoClim, h0, surfwv, Es) nav pārdalāmas. Iespējams izmantot kā ITU salīdzinājumu P.452 vietā ar VFS ΔN, nevis kartēm, kā tas ir CSIRO pētījumā; lietotājam kartes jālejupielādē saskaņā ar P.453. Apstiprināšana 1,3.0 no 593 QSO (29/12/2021–2/1/2022): notikumu/salīdzinājumu atdalīšanās 0,79 bez korekcijām, 0,91 ar Misme un subsidence; VFS neredz vadu jūrā 144 MHz (guadagno bez korekcijām nekad vairāk kā 15 dB).

Devītā kārta (21. novembris): IU3QEZ, NTIA, PyWaveProp, ACE High, validācija 1.3.1. IU3QEZ (S. Fabris), prezentācija "Pāri apvārsnim VHF" (GEKO Magazine n. 69, 2026. gada septembris): kopapjoms d2/(8·R\_eff) ar R\_eff ≈ 8500 km (1,3 km līdz 300 km, 3,7 km līdz 500 km katrs horizonta līmenis 69 jūdzes (111 km), robeža 7 dB evanscencei un 4 dB zemes atstarošanai; Tēze: 10 dB ir aptuveni 100 km; stacija IQ3QC 5 W ar 3×5 elementiem, 120 QSO, maksimāli 788 km (Alpe Adria 2026). Atgūstamās prezentācijas bibliogrāfija: Norton 1959 (J. Res. NBS 63D, System Loss), Bray 1961, ARRL Antenna Book 2023 vīģe. 4.29, Rice-Longley-Norton-Barsis NBS TN 101, Bean and Thayer 1959, Barsis and Kirby 1961 (differation on grēd), Deygout 1966, P.1812, AirScout DL2ALF, P.834 NTIA PropLib (wiki): eHata, ITM, LFMF, P.528, P.2108 (zaudējums no ieejas), ar C++,.NET, MATLAB un Python; bezkanšu modelis; ITS lapa: arī IF-77 (air–soil) un MPM (1 GHz–1Hz). PyWaveProp (Python): parabolisko vienādojumu metodes ar Padé tuvinājumiem, vairāku malu difrakcija, PETOOL un RAM kameras, troposferozi un augsti vadi, neregulāra augsne: iespējamā bāze lauka aprēķinam ar VFS M(z profils, kas jāpārbauda. APRS propView (Apache 2.0): VHF pavairošanas kartes no APRS paketēm, stundas statistika un atvēršanas trauksmes signalizācija; iespējams 144 MHz datu avots apstiprināšanai. NATO troposcatter tīkls Itālijā (fortifications.net): 900 MHz, 10 kW, 20 m līdzības; stacijas, tostarp Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Vulture, Monte Lauro; vēsturiskais testa gadījums P.617. Nepiemēro: Gupta 2021 (stratosfēriskais transports), Likums 2023 (arktiskā ozons), delfīns #590 un opera-adt/tropo (InSAR kavēšanās), tropo.ino (Arduino gāzes sensori), tropo-webapi (telefonija), cloudtools/trophosphere (AWS). Apstiprināšana 1.3.1. par 564 QSO tropo: AUC 0,79 (modelis 1.2), 0,80 (+ mīksts kanāls), 0,90 (+ neprecizitāte un iegrime), 0,90 (+ noturība); kanāla krāsas (≥ 16 dB) 46 % atvēršanas ceļu un 0% salīdzinājuma.

Pievienota tai pašai kārtai. mikelytaev/wave-pavairošana (MIT, Python 3): parabolisks vienādojums ar aptuvenām Padé un Numerov, neregulāra augsne, troposfēras vadi; uzlādē modificēto refrakcijas M(kvota, lat, lon) no NOAA GFS un iestata iztvaikošanas kanālu Monin-Obukhov; nav dokumentētas funkcijas, kas atgriež zudumu uz ceļa. Viņš ir labākais kandidāts nākotnes lauka aprēķinam ar VFS M profilu (tas kalpotu ārējam dienestam vai tulkojumam). RP-Sim (AGPL-3.0, JavaScript): Empīriskie modeļi radiotīkliem (Hata, He, Ericsson, divi stari), bez tropo. RIMFading (GPL-3.0, OMNeT+++): plūsmas pārkāpumi tīkla simulācijās, Weibull distribution, no tropo. PyLayers: iekštelpu radio kanāls, jau izslēgts. qradioprodict (kantooon, GPL v2): ITM un ITWOM v3.0 ar S RTM3 un Corine, jau septītajā kārtā kā QRadioPredict.

Desmitā kārta (21. novembris): Antenas grāmata, Nortons, Barsis, jauni datu avoti. ARRL Antenna Book (23a ed.), cap. 4 ( K9LA): Staru metode (QST November 1961) VHF diapazonam: izlīdzināts Zemes zudums līdz 99% (300 jūdzes, 144 MHz: 214 dB), līknes līdz 50% un 99% (Fig. 4.11.): 195 dB no 100 jūdzēm līdz 99% un vairāk nekā 250 jūdzēm līdz 50%; evanscence ±7 dB virs 100 jūdzēm (−3,5 dB līdz 50 jūdzēm), SSB 3 dB sliktāks nekā CW, +4 dB zemes atstarojums, +3 dB divu antenu savienošanai; augstuma pieaugums (15 m: 4/3/2 dB pie 10/50/100 jūdzēm; 24 m: 8/6/4 dB); Yagi pieaugums aptuveni 10 reizes lielāks nekā bums λ (dBi); 50 W stacija ar vidējo antenu: aptuveni 180 dB, 100 jūdzes, +10 dB līdz 250 jūdzēm. Saullēkts un saulriets līdz +20 dB un +20–50% caurplūdums; karsts un sauss gaiss pār slapju svaigu gaisu Lielajos ezeros līdz 1200 jūdzēm, tipiski 400–600; reģioni ietekmē lielu ūdens masu visvairāk; vēlu ziema ar apkuri spēj spēcīgas inversijas. Mūsu troposcatter pārbaude: P.452 līdz 50% pie 483 km un 144 MHz dod aptuveni 204 dB, kas atbilst 214 dB Bray līdz 99%. Norton 1959. (J. Res. NBS 63D, bez NIST): pamata zudumi, sistēmas un ceļa antenas pieaugums; Atmosfēra Ns = 301; līdzvērtīgs rādiuss no Ns (250 → 4878 jūdzes, 301 → 5280, 400 → 6997); L(99) tabulas ar 30 pēdu antenām (100 MHz: 163 dB līdz 100 jūdzes, 208 dB līdz 500 jūdzes; 1000 MHz: 138 un 188 dB ar 28 pēdu līdzībām); ilgtermiņa mainība V(p, θ) atkarībā no leņķa attāluma (13. att.). Rice, Longley un Norton, NBS TN 15 (1959), bezmaksas NIST. Barsis un Kirby, NBS Report 6751 (1961): 223 km Kolorādo ar Pikes Peak (4292 m) kā malu, 100 un 751 MHz; konsekventi zudumi ar noapaļotu malu (aptuveni 11,5 km rādiuss); ilgtermiņa evanscence 11,7–1,3 dB pret 39–42 dB tāda paša garuma izkliede; neviens dienas cikls nav atzīmēts; četru staru modelis nodrošināja 4–5 daivas, bet tikai vienu mēra (maksimāli līdz 21 m attālumā no zemes): ceļi ar kalnu šķērsli ir daudz stabilāki par izkliedi, noder savienojumiem "caur Vezuvius vai Faito". ARRL VHF Digital Handbook: pakete, APRS, D-STAR, WSJT (Scatter Method un EME), nav daļa no tropo. Jaunie pārbaudītie vai meklētie datu avoti: ECMWF atvērtie dati (IFS 0.25°, 1000. līmenis, 925, 850, 700 hPa..., bez 975, 950, 900 un 800; G RIB2 ar CCSDS kompresiju, ka mūsu lasītājs neatkodē; jau ir pieejami, izmantojot Open-Meteo); DWD ICON-EU atvērtie dati (G RIB2 saspiests bz2, fails mainīgam un līmenim: daudzi pieprasījumi); Kanādas GDPS uz MSC Datamart un GeoMet (aptuveni 15 km, līdz 33 līmeņiem; GeoMet ļauj audzēt platības; tas ir pamats Hepburn kartēm); GFS NOAA arhīvs uz AWS no 2021. gada februāra (globālie faili, noderīgi apstiprināšanai); APRS PropView tikai apstiprināšanai.

Vienpadsmitā kārta (21. novembris): Longley-Rice, GEKO 69. Bean and Thayer, "Atmosfēras radiorefrakcijas indeksa paraugi", Proc. IRE 1959. gada maijs (740. un turpmākie lpp.): Modelim 4/3 ir pareizs slīpums pirmajos kilometros, bet tas ir pārāk augstāks; 30% no rādiusa izliekuma ir pirmajos 100 m un 60% pirmajos kilometros; vidējā attiecība starp Ns un samazinājums pirmajos km: −ΔN = 7,32·e^ (0,05577·Ns) (Ns = 313 → ΔN aptuveni −42). Bean and Thayer, "C.R.P.L. Exponential Reference Atmosphere", J. Res. NBS 63D n. 3 (1959): N(h) = Ns·e^(−c·h) ar c nosaka Ns caur to pašu ziņojumu; Average Ns USA 313; staru izsekošanas tabulas (kopējā līkne 15,2 mrad ar eksponenciālo modeli, salīdzinot ar 30,3 mrad ar Zemi 4/3). MATLAB refractionexp: c = 0,143859 km–1 Ns = 313 (mēroga ap 6,95 km), derīgs virs apmēram 1 km un 10 mrad. Izmantojiet mums: "normāls" profils salīdzinājums var būt, ka CRPL ar vietējo Ns vietā M = 315 + 0,118·z fiksēta, un anomāls samazinājums pirmajā km būtu jāmēra, salīdzinot ar −7,32·e^(0,005577·Ns) vietā −40 fiksēta. NTIA Report 82-100 (Hufford, Longley, Kissick 1982), ITM ceļvedis teritorijas režīmā: klimi un Ns ieteikts (ekviatoriāls 360, subtropu kontinentāls 320, subtropu jūras 370, tuksnesis 280, mērenā kontinentāls 301, mērenās joslas jūras uz sauszemes 320, mērenās joslas jūras uz jūras 350); Ns = N0 ·e^(−hs/9,46 km); jūs izmantojat minimālo Ns mēnesī, jo to mazāk ietekmē superrefrakcija un subrefrakcija; Ns = 301 atbilst ae = 8497 km; zem 100 km Ns skaita maz. Rice, Longley un Norton, NBS TN 15 (1959), I daļa: prognozējamā formula izplatīšanai laikā pa sauszemi un troposfēras vilni, bāze TN 101. SoftWright, Wikipedia, Altair WinProp un MATLAB rfprop. LongleyRice: viena un tā pati informācija (1.2.2. versija, 20 MHz–20 GHz vai 40 GHz versija, 1–2000 km, Ns 301, septiņi klimatiskie apstākļi, laika, vietas un situācijas mainība); neviens neveido kanālus, tikai jūras klimata statistisko ietekmi ("augstāki lauki 10% laika vai ne). GEKO Magazine n. 69 ( IU3QEZ): radio horizonts 4/3 130 km attālumā no 1000 m, 260 km, pievienojot divas stacijas; troposcatter ar kopējo tilpumu no 1-3 km līdz 10 km ar +10-20 dB desmitiem sekunžu vai pāris minūšu; teorētiskie kursi ar 2×10 elementiem uz sāniem: SSB QRP aptuveni 440 km, SSB 50 W aptuveni 510 km, CW QRP aptuveni 600 km, SSB 500 W aptuveni 640 km (500 W SSB veido 40 km vairāk nekā 5 W CW); 90–99 % laika būtu jānoņem 10–20 dB; 5–10 dB uz horizonta grādu garajos posmos; kvotas skaita, lai atbrīvotu horizontu, nevis tuvojoties kopējam apjomam; Alpe Adria 2026: 99 % QSO troposcatter, gandrīz nevienai gaisa.

## 12. Apaļais galds: avoti modeļa apstiprināšanai vai labošanai (21. novembris)

Visu avotu pagriezienu kopsavilkums, pasūtīts lietošanai. "Valsts" saka, ja avots jau ir programmā (1.3.1.), vai tas ir izmēģinājuma vai tas ir jādara.

### Validējamie dati (kas īsti notika)

| Avots | Kas dod | Lietot | Stāvoklis |
| MMMonVHF ziņojumi no autora | 593 QSO pie 144 MHz, 29/12/2021–2/1/2022, laika un vietas noteicēji | Notikumu validācija ar salīdzinājumu (+14 dienas) | lietošanas laikā: AUC 0,79 (1,2) → 0,90 (1.3.1.) |
| Pasaule virs 50 MHz | dokumentēti gadījumi (14–15/8/2022 Manitoba–Kansas, 144–10 GHz) | vēsturisks gadījums | izmanto kā vēsturisku gadījumu; jāpievieno validācijai QSO |
| wspr.live (WSPR) | pilns arhīvs, 2 m un 70 cm, SQL | daudzi notikumi un daudzi "nē" (stundas bez vietas) | mēģināts, tas būtu jāfiltrē (spot false pie 16-20 tūkstoši km) |
| APRS propView / APRS-IS | stacijas filca, attālumi, stundas | Tikai apstiprināšanai 144 MHz | darīt |
| Reversā bāka (arhīvs) | dienas vietas | VHF | izmests |
| I GRA2 radiosondaggi (Brindisi 16320, Mare 16245, Trapani 16429, Udine, Milan prakse) | profili mērīti 00 un 12 UTC | VFS profila kontrole un inversijas | atrastie faili (Brindisi 46 MB), izmantojami |
| Barsis un Kirby 1961, Barsis 1963. | evanescence, ko mēra ceļos ar kalnu šķēršļiem | Apstiprināt kategoriju "monts" (Vesuvio, Faito) | darīt |
| Norton 1959, NBS TN 15, TN 101 | L(99) un galda datu nesēju zudumi | Izkliedēšanas kontrole | daļēji (skatīt 300 jūdžu atbilstību) |

### Ieteiktās korekcijas pēc avotiem

| Korekcijas | Avoti | Mērītā ietekme uz apstiprināšanu | Stāvoklis |
| Nepareiza stabilitāte Termiņš | Bean un Dutton cap. 6 | AUC 0,79 → 0,90 kopā ar pazeminājumu | 1.3.1. punktā (16 dB sadaļa bez kanāla) |
| Norobežota no vertikālās kustības | Moler and Holden 1960 (sākotnēji lasīta 22/11: +2,5 dB vidējais starp gaisu lejup un gaisa pieaugumu 1,4 un 9,4 GHz pie jūras) | kā iepriekš minēts | 1.3.1. punktā (+1,5 / −2 dB) |
| Mīkstais cauruļvads (λ līdz 3·λmax) | Kerr, Hepburn, VHF/UHF DX Book, Shi 2019. | vieni paši +1% no AUC; rada krāsas vadu par 46% no pasākuma maršrutiem un 0% no salīdzinājuma | skaits |
| Noturība 24 h | VHF/UHF DX grāmata, Whiteman un Leyshon | AUC nemainīts, vairāk veikti un vairāk viltus trauksmes | 1.3.1. punktā |
| Normāls CRPL profils ar lokālo Ns (−ΔN = 7,32·e^ (0,005577·Ns)) 40 N/km fiksētā vietā | Bean and Thayer 1959, NBS 4. monogrāfija | testēts validēšanā | 1.3.2. punktā kā alternatīvu, izslēgts defekta dēļ |
| Virsrefrakcijas slieksnis 79 N/km (ITU), nevis 100 (Bean un Dutton, slānis 1000–850) | P.453, Lopez 2008, DX grāmata | testēts validēšanā | 1.3.2: agresīvs novērtējums līdzās piesardzīgs (kartē izmanto piesardzīgs) |
| Klimatoloģiskais biezums (B cikls) ir mazāk svarīgs nekā deficīts | Rogers 1997.g. | mēģināt: izmantot A guvumam | darīt |
| Paraboliskais vienādojums VFS profilā | PETOOL, viļņu propagandēšana (Lytaev), Levy (grāmata nav lasīta, tikai priekšskatījums), Lindquist | pirmā versija ar lauku virs horizonta, kurā dominē skaitlisks troksnis (AUC 0,12–0,28): pareizs (domēns 8 km, stars 6°); pēc korekcijas AUC 0,54–0,66 | izmanto kā otro aprēķinu, nenosaka prognozi |
| Evanescence: ±7 dB virs 100 jūdzēm, robeža 10–20 dB līdz 90–99% | Bray 1961, Antenna Book, GEKO 69 | marginālos apstākļos esošo ceļu robežvērtības | lietošanā: perioda varbūtība un FT8 −18,6 dB troposcatter |
| Bray kopējais tilpums un ekvivalentais attālums | IU3QEZ, Bray 1961. | Informācija | skaits |
| Troposcatter P.617 un RSGB kā nenoteiktība | TropoLink, DL2FZN | Informācija | 3.3.0. punktā |

### Kas paliek ārpus modeļiem

VFS līmeņu nesaskatīšana (pašreizējo datu avotu izpēte); refleksija un difrakcija uz tuvējiem kalniem (Vesuvio, Faito): priekšlikums veikt aprēķinu "via monte". ITM/Longley-Rice joprojām ir statistikas atsauce “parasta diena” (Ns 301, klimi), bez kanāliem. Izmests ar autora lēmumu: AirScout, P.528), E sporādika (P.2001) un 50 MHz (servoir EIRP, kas dažiem ir un sajaukt ar E sporādiku).

### Darba kārtības priekšlikums

1. Paplašināta validācija: pievieno filtrētu WSPR (pat stundas bez vietas) un QST 2022.; atkārto 432 MHz.
2. Izmēģini parasto CRPL profilu un ITU robežvērtību 79 N/km, pa vienam, saglabājot tikai to, kas palielina AUC.
3. VFS profilu salīdzinājums ar Brindizi I GRA2 radioviļņu un jūras praksi QSO laikā.
4. Pārvērtēt parabolisko vienādojumu pēc skaitliskās korekcijas.

### 21. novembra lēmumi (versija 1.3.2.)

- Pieņemts: piesardzīgs novērtējums (Bean un Dutton, 100 N/km) un agresīvs (ITU, 79 N/km) ar sāniem; cauruļvadi tiek meklēti tikai zem 2500 m (pirmais 1500 m, celts pēc apstiprināšanas); bonuss ar augstu palielinātu spiedienu un vāju vēju; jūras svars ar μ1 no P.452; staciju īpatsvars attiecībā uz cauruļvadu arī uz daļēju cauruļvadu; robežu slānis virs grēdām ( IT9GSF).
- Pieņemts kā alternatīva, izslēgts ar defektu: normāls CRPL profils ar vietējiem Ns.
- Koriģēts paraboliskais vienādojums skaitliski: AUC no 0,12 līdz 0,54 (30 m antenas) un no 0,28 līdz 0,66 (300 m).
- Zeme: Copernicus DEM GLO-30, nevis Terrain Tiles «skadi», kam krasta tuvumā ir spoku reljefi 80–100 m jūrā ( IC8TEM zīme, pārbaudīts punkts pa punktu līdz Capri); skadi paliek kā rezervāts.
- Laikapstākļu zīmes kartē ar to ietekmi: ↓ labvēlīga iegrime (Moler and Holden 1960, vidēji aptuveni 2,5 dB starp gaisa nogāšanos uz leju un gaisa nogāšanos uz augšu; +1,5 dB), ↑ gaisa uz augšu nelabvēlīgas (dziļi ķermeņi; −2 dB), S sciroko labvēlīga pavediens (ARRL rokasgrāmata § 19,4, spēcīga inversija uz Vidusjūras; nav korekcijas). Dubults simbols, zaļas/sarkanas/oranžas krāsas un komiksu grāmata kartē.
- Pieņemts ITM 1.2.2. (NTIA/itm, publiskais īpašums), pārtulkots C#: saskaņā ar bāzes līnijas aprēķinu normālā atmosfērā blakus P.452 un galīgo stiepumu pēc izejas no vada, kas beidzas pirms korespondenta. Izmests qradioprodict kods, jo saskaņā ar GPL v2.

Bez noderīga satura indeksam: *Komunikācija Ceturkšņa *, rudens 1991 (nav raksta par tropo; Atkinsa raksts par izkaisīšanu ir 1991. gada ziemā).

- Pieņemts kā rokasgrāmatu princips (21. novembris): "ekspluatāciju nevar vispārināt, bet pētīt saistībā ar vietējo morfoloģiju" ( DF5AI, par ko ziņo IK3XTV sadaļā "Propagācija 2 metros"); minēts prognožu rokasgrāmatā (1. pants) un padomēs.
- Pieņemts (21. novembrī, pēc 1.3.3.) IK3XTV transalpīnu atveru interpretācija: nevis "konvekcijas šūnas, piemēram, lēcas", bet troposcatter pastiprināta ar turbulenci līdz pārstrādātā slāņa augšējai daļai, kad tas sasniedz kopējā tilpuma daļu (prognožu rokasgrāmata, 8. ter §). No 1.3.4. punkta tas ievada punktu aprēķinu kā troposcatter pagaidu pastiprinājumu, ne vairāk kā 3 dB. Izmesti vieni un tie paši raksti: signālu "autoamplifikācija" ārpus Alpiem (pasīva vide nepastiprina: aprakstītās saites paliek izskaidrojamas ar difrakciju un troposktāru; deklarētās pilnvaras netiek izmantotas kā norādīts), sniegs, kas "labāk difrakcija" (visticamāk stabilais slānis virs sniega, ar mazāku evanescenci).

- Kerrs (1951) aplūkots grāmatā (22. novembris, PDF, ko sniedzis autors): 1.5, 17. un 18. § un 1.1. tabula. 18. q. skaitļi skenējumā ir nelasāmi, bet 1.1. tabulā apstiprināts: 20 ft → 1 cm, 80 ft → 10 cm, 400 ft → 100 cm, 2000 ft → 1000 cm, pret 1,25, 10, 112 un 1252 cm λmax = 0,014·d^1,5 (λ cm, d stāvot; apaļais galds, "indikatīvās vērtības". Kerra hipotēzes: cauruļvada iekšpusē pārveidotais indekss krītas 4·10−8 uz vienu pēdu, kas ir raksturīga zemo kanālu vērtībai; ar vājākiem slīpumiem nepieciešams lielāks biezums. Kerrs arī brīdina, ka λmax nav tīkla griezums: dažkārt viļņu garumus var spēcīgi ietekmēt vads.

- DL6WU pārskatīts uz oriģināla (22. novembris, PDF of VHF Communications 3/1982, autors): G. Hoch, «Extremely Long Yagi Antennas», 130. un sekojošais. 1. attēls: Dipola pieaugums, mērot 432 un 1296 MHz Vācijas pasta profesionālā daudzstūrī, ar parauga antenu EIA no 7,7 dBd, aplēstā precizitāte 0,5 dB; 2,35 dB uz dubultu buma garumu, bez piesātinājuma līdz aptuveni 20 λ. Programma izmantoja 8.6 + 8.0.log10(L/λ) dBd, t. i., aptuveni 0,8 dB mazāk nekā 1–2 λ; tagad tas izmanto 9,4 + 7,8·log10(L/λ) dBd (gradients ir norādītais, vērtību 1 λ nolasa tabulā, ± 0,3 dB). Izmestā formula 10·log10(7,8·L/λ) cirkulē uz neto: dotu 3 dB divkāršošanai, salīdzinot ar 2,35 izmērīto.
- Booker and Gordon (1950): atsauce apstiprināta ar "Šīs nedēļas citēšanas klasiku" (Current Contents, 1982. gada 8. februāris, H. G. Booker komentārs): Proc. IRE 38, 401.–412. lpp. Komentārs apstiprina divus punktus, ko izmanto rokasgrāmatās: lauks saņemts ārpus horizonta ar Rayleigh distribution, un eksponenciāla autokorelācijas funkcija pārkāpumiem (ne-Gaussian). Sākotnējais raksts nav izlasīts.

## 13. No IK3XTV rakstiem

Divi raksti ar Flavio Egano IK3XTV (Radio Magazine: "Propagācija uz 2 metriem" un "Propagation VHF - convective šūnas un gravitācijas viļņi") pievieno divas lietas, ko vadu modeļi neaptver: transalpu atveres pēcpusdienā sakarā ar konvekciju, un praktiskus noteikumus par to, kur un kad cauruļvadi veidojas Itālijā. Tie ir novērojumi no stacijas, nevis pasākumi: tie ir jāuzskata par hipotēzi, lai pārbaudītu ar plankumiem.

| Autora komentāri | Kā tas kļūst par indeksa sastāvdaļu |
| Cauruļvadi galvenokārt veidojas no 100 līdz 1000 m, reti augstāk; tie kalpo augstam spiedienam plašā teritorijā, labai zolācijai un vēja trūkumam | meklēt vadu tikai zem 1500 m; bonuss ar augstu pagarinātu spiedienu un vājš vējš zemos slāņos |
| Siltos mēnešos, kas atkārtojas Adrijas jūrā starp Veneto un Apūliju, 700–800 km, dažreiz līdz pat Grieķijai; jūras maršruti ir labvēlīgi | lielāks svars līdz jūras ceļa daļai (piemēram, ITU-R P.452) |
| Atbilstošajam jābūt caurulē vai tās tuvumā; ar augstām antenām un dažiem metriem vads signāls neiekļūst | salīdzināt vada bāzes daļu ar abu staciju daļu (apkalpos QTH augstumu); |
| Tajā pašā laikā, tas pats kanāls: klausīties no Gargano staciju 144 un vienu no 50 MHz | lai pārbaudītu: saskaņā ar Hepburn tabulā 50 MHz pieprasītu vairāk nekā 300 m svina, tāpēc notikums norādītu cauruļvadu ļoti bieži |
| Savienojumi Veneto – Dienvidvācija aiz Alpiem, bez vizuālā, gandrīz vienmēr pēcpusdienā un ar augstu spiedienu; nestabili signāli, kas izzūd un atkal parādās pēc dažām minūtēm | “Konvekcijas” indekss, kas atdalīts no kanāla, kalnu ceļiem |
| Konvekcijas šūnas dzimst uz zemes un ik pēc 3 stundām paceļas apmēram 1 km: 15 no tiem sasniedz aptuveni 3 km augstumu, kas kalpo Alpu nomaināšanai | izmantot robežslāņa augstumu (robežslānis\_slānis\_augstums Open-Meteo GFS) pa ceļu: punktu skaits, kad tas pārsniedz grēdas augstumu |
| Alpi rada gravitācijas viļņus, kas kopā ar sastingušām kustībām rada refrakcijas indeksa novirzes | hipotēzes: spēcīgs vējš perpendikulāri ķēdei līdz 700 hPa kā signāls zelta viļņu; mēģināt pēdējo |
| Ļoti augsts inversija (līdz 8 km izņēmuma gadījumos) dod aptuveni 800 km, bet praksē reti vairāk nekā 300 km | inversijas īpatsvars paplašina darbības jomu: noderīgi dati gaidīšanas attāluma aprēķināšanai |
| Vasarā Zemes rādiuss līdzvērtīgs aptuveni 16000 km (2,5 reizes reālā), ar aptuveni 60 dB mazāk nekā 300 km zaudējumu | k50 = 157(157 – ΔN): k = 2,5 atbilst ΔN apmēram 94 N/km, t. i., superrefrakcijai, |
| Vairāk nekā 3000 km tikai 20–30° areālā uz ziemeļiem un dienvidiem no ekvatora virs jūras, kas saistīts ar Hadlija šūnām; maksimums augustā Āfrikā–Eiropā | ārpus Eiropas: noderīgs nav sniegt augstu indeksu uz Atlantijas okeāna bez atsauksmes, un Kanāriju maršrutā – Lielbritānija |

Citi noderīgi dati no tiem pašiem pantiem:

- Optiskais diapazons no diviem h1 līdz h2 metriem (d km): d = 4,1 · (
- Evanescence: lēna un sekla ar inversiju; spēcīga un ātra ar izkliedi un konvekcijas šūnām. Programma varētu izmantot to, lai klasificētu reklāmas, ja kādu dienu tas būs SNR ziņojumus laika gaitā.
- Par sporādisko E 144 MHz autors ziņo DF5AI un Deivisa un Džonsona (Nature 435, 2005) darbu uz saites starp temporals un Es: no tropo, bet atsevišķs rādītājs rīt varētu izmantot zibens datus.
- Zemes atstarošanās dubultlēkšanā Es: jūra K = 81 un 5 S/m, lauki un meži K = 13 un 0,006 S/m, pilsētu teritorijas K = 3 un 0,001 S/m.

## 14. LIVETROPOVHF indeksa hipotēzes

Izbraukšanas priekšlikums, kas jāapspriež: indekss 0-10 tajā pašā skalā kā Hepburn, ko aprēķina katram tīkla punktam un ik pēc 3 stundām no VFS (toreiz ECMWF un ICON salīdzināšanai), un otrs numurs maršrutā, kas norāda, cik liela maršruta daļa pāriet lietderīgā kanālā uz izvēlēto joslu. Svari zemāk ir mani pieņēmumi, kalibrēt ar plankumiem.

Recenzija pēc Bean un Dutton un Kerr: profils robežvērtības nāk no to tabulā 6.5 un skala no to mērījumiem, tāpēc refrakcijas slānis ir vērts 10–25 dB virs normālā profila. Pēc tam indeksu aprēķina vispirms dB un pēc tam iegūst 0–10; šajā šķērē norādītās vērtības vēl jākalibrē ar plankumiem.

Aprēķins pa punktiem, soli pa solim

1. T izlāde, relatīvais mitrums, ģeopotenciālais augstums un vertikālais ātrums pie 1000, 975, 950, 925, 900, 850, 800, 700, 600 un 500 hPa, plus T un 2 m mitrums, zemes spiediens, slāņa augstums un vējš pie 10 m.
2. Izmetiet līmeņus zem zemes; aprēķini N un M katrā līmenī (ITU-R P.453).
3. Aprēķins −dN/dh katrā no četriem slāņiem Bean un Dutton (1000-850, 850-700, 700-600, 600-500 hPa) un diagramma kā sub-refractive, normāls vai super-refractive ar robežvērtībām to tabulā; iekšpusē pirmajā kārtā izmanto arī starplīmeņus, lai atrastu spēcīgāko gradientu un tā daļu.
4. Ja dM/dh < 0 I atrod šahtas pamatni un augšējo daļu, d biezums un deficīts ΔM.
5. Minimālā joslas biezuma salīdzinājums no Kerr formulas (apmēram 180 m 144 MHz, 89 m 432 MHz, kā Hepburn tabulā); zem robežvērtības vada joprojām skaita, bet mazāk.
6. Es stimulē pieaugumu dB ar tabulu zemāk (jūs lietojat labāko slāni, nevis summu slāņu, plus termins anomālija) un pārvērst to: Indekss = dB / 2.5, starp 0 un 10, tāpēc 25 dB ir vērts 10. Negatīva vērtība (apakšrefrakcija) ir norādīta kā 0, bet paliek ceļa datos.

| Labākā slāņa situācija | Kritēriji | Paredzamā peļņa |
| Normāls (nav laminēts) | -dN/dh tabulā. 6,5 (piemēram, 20–60 N/km no 1000 līdz 850 hPa) | 0 |
| Nedaudz virs normas | starp normālās joslas augšējo robežu un superrefrakcijas slieksni | 0 līdz 10 dB proporcionāli |
| Superrefrakcija | virs slāņa sliekšņa (100, 80, 70, 50 N/km kāpšana), bet zem 157 N/km | no 10 līdz 15 dB, proporcionāli līdz 157 N/km |
| Pārāk tievs cauruļvads joslai | dM/dh < 0, biezums zem Kerra robežvērtība | 15 |
| Lentei noderīgs savienotājs | dM/dh < 0, biezums virs Kerra sliekšņa | 20 dB, +1 dB uz 5 M deficīta vienībām, ne vairāk kā 25 |
| Apakšrefrakcija | −dN/dh < 0 apakšējā slānī | −10 dB |
| Normāls fons (jūs pievienojat) | Ns modelis mīnus ITU kartes klimatoloģija Ns attiecīgajam mēnesim | 0,2 dB uz vienību N, starp −5 un +5 dB |

Aprēķins pēc maršruta (starp diviem vietrāžiem): parauga maksimālais aplis ik pēc 10 km, tāpat kā programma indeksam uz ceļa, un aprēķina punktu daļu ar noderīgu kanālu un vidējo bāzes daļu. CSIRO pētījumā konstatēja, ka maršruta daļa cauruļvadā ir lietderīga (AIS maršrutos 162 MHz bija par 67 % lielāka nekā kontroles gadījumos).

Pievienojot aprēķinu katram maršrutam (no LA0BY un W3SZ): 1) jūras stacijas, salīdzinot ar kanāla pamatni un augšpusi, jo augsts cauruļvads apkalpo tikai tos, kas ienāk; 2) troposcatter "grīda" ar Yeh metodi, ar sākuma stūriem, kas ņemti no zemes profila; Trope pieaugums dB tiek noņemts no šī zaudējuma un rezultāts tiek salīdzināts ar zaudējumiem, ko stacijai izdodas pārvarēt ( KK6MC tabula). Tādējādi indekss kļūst arī par sasniedzamā attāluma aplēsi.

Labojumi, lai mēģinātu vienu vienā reizē (no Bean un Dutton un LA0BY, IK3XTV, G4MCU jau rokasgrāmatā), jāglabā tikai tad, ja tie uzlabo salīdzinājumu ar plankumiem:

- Sajaukšanās (svarīgi, pēc autora domām, katru dienu novērojot): diena uz zemes, no rīta vidus līdz pēcpusdienai ar skaidrām debesīm, vai ar vēju uz zemes virs dažiem m/s, vadi uz zemes pazūd; to devums ir jāsamazina. Ne jūrā.
- Dienas laiks (svarīgs pēc autora teiktā, pēc ikdienas novērojuma): iracionālie vadi piedzimst saulrietā un krīt pēc saullēkta; tā ir 3 stundu prognožu kontrole.
- Vertikālā kustība: gaiss paceļas līdz 850–700 hPa remiksē slāņus (negatīva korekcija), gaiss, kas krīt zem anticiklona, nostiprina tos (pozitīvi).
- Spiediena tendence: spiediens uz ceļa pēdējo stundu laikā, augstāks un stabils signāls; samazinājums, zemāks signāls (bastano 2 hPa).
- Sinonīmi: augsts spiediens virs 1025 hPa, temperatūra pie 850 hPa daudz virs ka uz zemes, karsts un sauss gaiss plūst uz aukstāku jūru, maršruts pa jūru.
- Kalnu ceļi: otrs "konvekcijas" indekss (sadaļa IK3XTV), ar pēcpusdienas robežslānis augstāks nekā grēdas pa ceļu un augstu spiedienu.

No VHF/UHF DX Rezervēt, citas korekcijas, lai mēģinātu: secīgas dienas augsts spiediens uz punktu (vairāk dienas, vairāk punktu), stāvoklis, salīdzinot ar centru anticiklona (bonus malās), spiediena gradients uz zemes kā mērījums vēja, spēcīgs lietus iepriekšējās 24 stundās (penalitāte), zemi mākoņi malas slāņa (sods tikai augstu joslās). Maršrutu virzienam: salīdzina maršruta azimutu ar izobara azimutu.

Pārbaude

- LIVETROPOVHF arhīvs: katrs komerciāls no 500 km līdz 144 vai 432 MHz ir "events". Tas salīdzina maršruta indeksu ar notikumu biežumu vienā un tajā pašā diennakts un mēneša laikā (tāda pati CSIRO pētījuma pieeja).
- Salīdzinājums ar Hepburn un F5LEN vienās un tajās pašās dienās: kur trīs indeksi atvienojas, jūs izskatāties, kurš bija taisnība atbilstoši plankumiem.
- Radiosundācijas 00 un 12 UTC: lai pārbaudītu, vai modelis redz inversijas, ko mēra zonde.
- LA0BY (6-11/11/2003, 10/12/2004, 15/10/2011, 11/2/2015, 22/11/2018) vēsturiskie gadījumi: šajās dienās pārrēķinātajam indeksam jābūt tādam, lai būtu redzami vadi, kurus Hepburn neuzrādīja.
- Rokasgrāmatā norādāmie ierobežojumi: veikti zem 100–200 m, gandrīz neredzami VFS spiediena līmeņiem; uzticama prognoze 2–3 dienas, tad tikai tendence.

## 15. 21. novembra avoti (otrā daļa)

- ITU-R P.1812 (MaTLAB/Octave implementation of I. Stevanovic, OFCOM; GitHub eeveetza/p1812, Zenodo 10.5281/zenodo.61411558). VHF/UHF apgabalam specifiska maršruta metode, kas atvasināta no P.452: "delta-Bullingtona" difrakcija, troposcatter, vadi un atstarošana uz slāņiem, apgrūtinājums uz zemes. Noderīgs kā trešais salīdzinājums blakus P.452 punktu aprēķinā; ΔN un N0 dati ir pilni ITU produkti, tāpēc lietotājs tos lejupielādē (kā P.453).
- no Costa, Rabanal Ramirez, Dias, IET MAP 2018 (doi 10.1049/iet-map.2017.0992). 563 MHz, 27 punkti no 5 līdz 30 km neregulārā reljefā ar mežu: P.526 ar 12 m veģetācijas −4,3 dB un σ 8,8 dB; P.1812 ar 10 m apgrūtinājumu +2,6 dB un σ 8,9 dB; dielektriskais ķīlis −4,8 dB un σ 9,2 dB; P.1546 +5,0 dB un σ 11,7 dB. Programmai: labas difrakcijas metodes atlikusī kļūda ir aptuveni 9 dB, bet grēdu augstums (10–12 m) skaitās.
- PETOOL (Ozgun e altri, Computer Fizic Communications 182, 2011). Paraboliskais vienādojums sadalīts līdz vienam un diviem maršrutiem ar platleņķa propagandu (leņķis līdz 40–45°, pret 10–15° šaurā); dibens ar zudumiem caur pārveidoto Furjē jaukto; piezemējas līdz pakāpieniem. Apstiprina 1.3.2. punkta izvēli (platleņķis, augsnes koeficienti) un norāda uz stāvu reljefu divvirzienu aprēķinu.
- MathWorks. `tropopl`: gāzu zudumi un «lentes» zudumi slāņveida atmosfērā (P.835, P.676, P.453), bez kanāliem: ar slīpumiem zem −157 N/km ģeometrisko optisko modeli nepiemēro. Radara izplatīšanās piemērs izmanto refrakciju −ΔN = 7,32·e^ (0,05577·Ns), tas pats CRPL ziņojums jau programmā. `propagationModel` Longley-Rice: iepriekš noteiktas horizontālās polarizācijas vērtības, vadītspēja 0,005 S/m, autability 15, reaktivitāte 301, mērens kontinentāls klimats. Nav MathWorks modelis nodarbojas ar kanāliem.
- ITU-R, grupas digitālie produkti 3. P.453, P.452, P.1812, P.2001, P.528, P.1546 dati un programmas; "integrālie" dati ir daļa no rekomendācijām; nav deklarēta pārdales licence: jūs turpināt ļaut tos lejupielādēt lietotājam.
- ESA Navipedia, Troposferical kavēšanās. N = Nidrostatica + Numida; sausā zenīta aizture ir apmēram 2,3 m, bet mitrā viena desmita cm, kas ātri mainās. Mums: GNSS staciju aprēķinātā mitrā aizkavēšanās ir netiešs mēraukla integrētajam mitrumam, iespējamam nākotnes avotam tagadnē; Šodiena nav izmantojama.
- Bray, K2LMG, «A Method for Determining V.H.F. Station Capabilities», QST November 1961, 36.–40. lpp. (read in original; atkārtoti pārbaudīts 22. novembrī uz PDF skaitu QST: visas zemāk norādītās vērtības atbilst). Autors D. W. Bray (General Electric, Ithaca) saka, ka viņš ir pārbaudījis prognozes ar mēnešiem plānotiem savienojumiem ar K2GOI un W4LTU. Evanescence 13,5 dB laikā aptuveni 100 jūdžu garumā (7 dB zem un 7 virs stundas vidējā), gandrīz lineāra zem (3,5 dB līdz 50 jūdžu): stabilam savienojumam noņem 7 dB. Apvāršņa korekcija: katra horizonta pacēluma vērtība ir 69 jūdzes (111 km) no papildu attāluma (mazāk, ja negatīvs). Jagi pieaugums aptuveni 10 × bums/λ (jaudas attiecība), vairāk par 4 dB augsnes atstarošanas; SSB −3 dB un AM −7 dB, salīdzinot ar CW; 99% no stundām: aptuveni 180 dB līdz 60 jūdzes, 190 līdz 90, 200 līdz 240 (2 m). No 1.3.2. perioda varbūtības 5 dB aprēķinātās otrās rokas vietā izmanto σ = 3,4 dB (±7 dB kā ±2σ).
- Divvirzienu aprēķins (mēģinājums ar prototipu, 21. novembris). Lielleņķa paraboliskais vienādojums ar reālu S RTM1 reljefu no Capri, 144 MHz, antena 10 m no zemes, standarta atmosfēra; mērījums: vidējā jauda 20—400 m pie 80 km maršrutā līdz attiecīgajam. No Capri un Marina Grande centra rietumos ir slēgts (šķērslis līdz +11–21°, Monte Solaro): norādot uz Faito (gandrīz aiz EA5 un EA6, 150–160° pie ass), signāls, kas palaists Spānijas virzienā, paceļas 10–29 dB; norādot uz Vezuvu (110–125° piestātnē, tad ārpus aprēķina plāna, tikai orientējoši) 17–25 dB. No Anacapri rietumos ir brīva un tiešās uzvaras 8—22 dB. Absolūtie zaudējumi uz 1000–1300 km prototipa tomēr nav ticami. EA3 EA5 EA6
- Barsis un Kirby, NBS Report 6751 (1961), Pikes Peak, 223 km, 100 un 751 MHz. Kalns ir izplatīts divu maršrutu terminālis optikā: stundas mediānu sadalījums ir abu maršrutu konvolūcija. Ilgtermiņa ekskursija (1–99%) par aptuveni 14 dB, pret 26–40 dB troposcatter ceļam ar vienādu garumu vai leņķi; nav jūtamu dienas izmaiņu. Signāls ir spēcīgāks par troposcatter, bet vājāks par ideālo malu: augšējais jāuztver kā noapaļots (ap 11,5 km). Augsti signāli vienmēr tiek novēroti aiz muguras.
- Barsis, NBS Report 7652 (1963), trīs maršruti virs kalna (Colorado, Aļaska, Eiropa), 750–900 MHz. Vidējie zudumi 10–20 dB lielāki nekā Fresnel-Kirchhoff aprēķins bez zemes termināļu tuvumā; evanescence tiek aprēķināta, ietinot abas līnijas optikā; augsnes atstarošana pie termināliem pasliktina ilgstošo izbalēšanu. No 1.3.2. punkta ieeja caurulē papildina P.526 noapaļošanu.
- Barsis un Džonsons, NBS TN 88 (1961). Ilgstoša kosmiskā viļņa izšķīdināšana pat uz ceļa ārpus kalna (5. pants).
- Norton, «Sistēmas zudums radioviļņu pavairošanā», J. Res. NBS 63D (1959). Līknes, no kurām Bray iegūst nomogrammas; sistēmas zuduma, pārraides un pamata, procesa antenas pieauguma definīcijas.
- Rice, Longley, Norton un Barsis, NBS TN 101 (1965–67), 1. un 2. sējums. viena mala un šķērslis, kas noapaļots ar atspīdumiem un bez tiem (7. pants ar Pikes Peak piemēru), ilgtermiņa mainība V(0,5) un Y(q) klimatam (tempered concontinental un citi). Mums: noapaļotu šķēršļu un mainības funkciju metode ir atsauce uz kalibrēto nenoteiktību.
- ITU-R P.526-16 (11/2025).
- ITU-R P.1812-8 (2025) un -7. Viss atgūtais teksts: specifiska 30 MHz līdz 6 GHz metode (delta-Bullingtona, troposkrita, kanalizācijas un atstarošanas uz slāņiem, ingombs). -7 brīdina, ka tā troposcatter (no P.617-5) ir nepareizi zem 66 km. Tas joprojām ir iespējams trešais salīdzinājums.
- Paulus, «ĪREPS iztvaikošanas modeļa plavais pielietojums», NOSC TR 966 (1984), un Radiozinātne 20 (1985). Ziņojums nolasīts 22/11/2026 (DTIC ADA146528, skenēt ar atzītu tekstu, dažreiz nelasāms). Gaisa un jūras atšķirības, ko mēra ar kuģiem, tiek nobīdītas uz stabilitāti: siltāks jūras gaiss 31–35% kuģu datu, salīdzinot ar 0,7–7% NOAA kuiļu, attiecībā uz psikrometriem, kurus traucē kuģis un SST, kas iegūti no dzesēšanas ūdens (aptuveni +1 °C). Pēc tam Jeske modelis dod 40 m un vairāk nekā 15% no gada un augstumu, kas lec no 10 līdz 40 m 24 stundās ar stabilu laiku, nav apstiprināts ar radio pasākumiem. Piedāvātais labojums (pārbaudītie sliekšņi 0, −0,5 un −1 °C; par −1 °C labāk vienojas ar 1982. gada jūnija radio pasākumiem): ja gaisa un jūras starpība ir −1 °C vai mazāka, tad izmanto izmērīto; pretējā gadījumā augstumu aprēķina ar −1 °C un ar 0 °C, un, ja ar −1 °C ir zemāka, tad turiet to. Palielinājumi no stabilās puses tiek sagriezti, samazinājumi paliek. Jūnijā Marsdens 120 kvadrāts: vidējais augstums no 16,8 m (klimatoloģija) līdz 7,6 m. Parauga vienādojumi ziņojumā nav iekļauti (skatīt Jeske un Hitney). Programma izmanto neitrālo profilu, kuram nav stabila pārspīlējuma, un ierobežo δ līdz 40 m: tas iet vienā virzienā; ar modeļu SST (ne no kuģa) kļūmi mērījuma, ko Paulus koriģē sver mazāk.
- Levy, «Paraboliskās vienādošanas metodes elektromagnētisko viļņu propagandēšanai» (IEE 2000). Google grāmatās ir tikai priekšskatījums: Es nelasu.
- VK3FS (3fs.net.au). Kvalitatīva ievade: augsti vadi 450–2000 m garākām atverēm uz zemes; 300–600 m garas, mierīgas un mierīgas naktis, kas izzūd pēc saullēkta; spēcīgākas piekrastes inversijas pēcpusdienā; 800–1300 km tipiski, 1400–1600 km bieži veikti. Atbilstoši dienas/nakts brīdinājumiem un griestiem 2500 m.

## 16. Avoti

Noteikumi un formulas

- [ITU-R P.453-13 (2017), radiorefrakcijas indekss:](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)formula un dati
- [ITU-R P.452-18 (2023), traucējumu prognozēšana starp zemes stacijām virs 100 MHz: ekvivalents rādiuss, β0, troposcatter,](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)vadi
- [ITU-R P.617-5, izplatīšanās ārpus horizonta radiotiltiem](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)
- [Shi et al. "Visaptverošs pētījums par viļņu maksimālo garumu elektromagnētiskā izplatība dažādos iztvaicēšanas dubļos", IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

Pētījumi par cauruļvadu prognozēšanu ar laikapstākļu modeļiem

- [P. Lopez, "Piecu gadu 40 km izšķirtspējas globālā superrefrakcijas klimatoloģija uz zemes bāzētam meteoroloģijas radaram", ECMWF Tech. Memo 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO), "Troposfēras ducting Prediction based on GFS Model Data at... the CSIRO Murchison Radio-astronomy", Observatory arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (FMI), “Atmosfēras cauruļvada prognozēšanas modeļa izšķirtspējas trūkumi ar Harmonie-AROME”, EGUsphere 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

Amatieru radio prognozes

- [W. R. Hepburn, "Hepburn Tropo Index (HTI)"](https://www.dxinfocentre.com/propagation/hti.htm)un [](https://www.dxinfocentre.com/propagation/history.htm)metodes vēsture
- [W. R. Hepburn, "Lowest Usable Frequency vs. Ducting Layer biezums"](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, "Pasaules refrakcijas indeksa prognozes"](https://tropo.f5len.org/WW/info.php), [jaunā versija 2026](https://tropo.f5len.org/?p=504), [piezīme par modeli ARPEGE](https://tropo.f5len.org/author/f5len/)

Dati

- [Open-Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api), [piezīme par](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)spiediena līmeni
- [NOAA NCEI, integrēts globālo radiozondu arhīvs (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RIOBget, radio plūdu skripts Wioming](https://github.com/NCAR/RAOBget)universitātei

Klasiskie teksti (norāda programmas autors, ar kuriem iepazīstas galīgās formulas)

- B. R. Bean, E. J. Dutton, *Radio Meteoroloģija*, NBS Monograph 92, 1966, 435 lappuses (pārdrukāts Dover 1968): N un M vertikālie profili, lamatu slīpums zem −157 N/km, salīdzinājums ar radioviļņu; 5. nodaļa "Sinoptiskā radio meteoroloģija". Bāze 2–4 aprēķinu posmiem katram punktam. Bezmaksas: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [NIST karte, doi 10.6028/NBS.MONO.92](https://www.nist.gov/node/1770911), [Internet Archive](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf)copy.
- L. Boithias, *Radio Wave Propagation*, McGraw-Hill 1987 (tulkojis D. Beeson no franču valodas, 330 lpp., ISBN 0070064334; arī North Oxford Academic): mehānismi aiz horizonta, kopējs izkliedes apjoms un sakabes antenas-mezzo zudums. Tikai papīra ([bibliotēka karte](https://libcat.library.tamu.edu/Record/in00000014812/Description)).
- M. P. M. Hall, *Troposfēras piesārņojums radiosakaros*, SEE Elektromagnētisko viļņu sērija n. 8, Peter Peregrinus 1979, 206 lpp. (ISBN 0906048257; brossura 0863410863): sinoptiskās situācijas un izplatīšanās VHF/UHF. Sinoptiķu sastāvdaļu bāze. Tirdzniecība ar IET ([](https://libcat.library.tamu.edu/Record/in00000636385)karte).
- H. G. Booker, W. E. Gordon, "Teorija par radio izkliedi troposfērā", *Īres proces*, 38. sēj., Nr. 4, 1950. gada aprīlis, 401.–412. lpp.: Scapping teorija no refrakcijas indeksa disogenitātes. Centrālā ideja: izkliedētā jauda ir atkarīga no refrakcijas indeksa trauksmaino svārstību intensitātes un mēroga (eksponenciālā korelācija); horizonta tuvumā dominē difrakcija, tālāka izkliede; Pēc autoru domām, meteoroloģijā sagaidāmās turbulences un indeksa variācijas ir pietiekamas, lai izskaidrotu izmērītos laukus. Citējis 328 rakstus (Scilit), nav brīvas piekļuves kopiju. Karte uz semantiskā stipendijas ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); pilns teksts par IEEE Xplore samaksāts ([doi 10.1109/JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)).
- C. M. Crain, J. R. Gerhardt (Elektrotehnikas pētniecības laboratorija, Teksasas Universitāte), "Parametru mērījumi, kas iesaistīti teorijā par radio izkliedi Troposfērā", *ĪRE proceedings*, vol. 40, n. 1, January 1952, pp. 50–54, doi 10.1109/JRPROC.1952.2739 dod reālās vērtības, ko Booker un Gordon bija tikai aplēsis ([Skilit](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)tab]).
- "Atmosfēras refrakcijas indeksa vienādojuma konstantes pie radiofrekvencēm", *ĪRE*, 41. sēj., nr. 8, 1953. g. augusts, 1035.–1037. lpp.: ierosina pēc tam izmantot strāvas (79 K/mb un 4800 K) formulā N; tieši no šejienes ITU formulas vērtības 77,6 un 3,73 · 10^5 ([Q](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E. C. S. Megaw, "Fundamentālā radio izkliedes pavairošanas teorija", *SEE C daļas rezultāti: Monogrāfi*, 104. sēj., n. 6, 1957. gada septembris, 441.–455. lpp., doi 10.1049/pi-c.1957.0052: indeksa svārstību spektrs no turbulences teorijas, pilnīga izkliede aiz kopējā horizonta un ģeometrijas ietekmes, kas samaksāta pie IET ([Skilit](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)karte]).
- *IRE proceedings*, 1955. gada oktobris, speciāls numurs par izkliedi (pēc K. A. Norton un J. B. Wiesner), ar Bullington, Staras un citiem: bezmaksas PDF par [World Radio History](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf).
- D. E. Kerr (rediģēts), *Īsu radioviļņu propagācija*, MIT Radiācijas laboratorija Series vol. 13, McGraw-Hill 1951 (Restampe Dover 1965 un IEE 1987): teorija par iztvaikošanu un virszemes kanāliem jūrā. Ciparu kopija: [World Radio History](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); IET edition in [IET Digital Library](https://shop.theiet.org/propag-short-radio-waves).

Nesenie Troposferical difūzijas pasākumi VHF: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Austrālija), "A Study of VHF Troposcatter Communication Channels in South Australia", in *Digital Signal Processing for Communication Systems*, Springer 1997, 103.–112. lpp. ([Springer](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)). Teksts par maksu; no abstrakti tas attiecas uz izplatīšanu, nevis kanāliem. Noderīga tikai tad, ja vēlaties kalibrēt termiņu troposcatter ar VHF kanāla datiem.

Skaitliskie modeļi un jūras maršruti (norāda programmas autors)

- M. Levy, * Paraboliskās vienādošanas metodes elektromagnētisko viļņu propagandēšanai*, IEE Elektromagnētiskie viļņi sērija #45, 2000, 336 lappuses (ISBN 08529640): parabolisks vienādojums refrakcijas profiliem, kas mainās pa ceļu un par orogrāfiju; nodaļas par troposferozu pavairošanu, stariem un veidiem, pavairošanu uz jūras. Tas kalpotu lauka aprēķinam pa ceļu, aiz indeksa. Apmaksai, arī elektroniskā formā no IET ([IET karte](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)).
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (Leicester universitāte): 2 GHz mērījumi Normandijas salās un Lamanšā. Daudzi darbi un tēzes ir bezmaksas universitātes arhīvā: [ilgtermiņa statistika par iztvaikošanas kanalizāciju, Radio Science 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434), [signāla izmaiņas trijos jūras maršrutos, Radio Science 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710), [Nosaukumi N. Mufti 2012 ar maršrutiem 50 un 140 km arī VHF](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854), [I. Alam 2002 par reaktivitāti aprēķināts no laikapstākļu datiem, 240 MHz un 2 GHz X3 UQ, [tesi.](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus, "Iztvaikošanas modeļa praktiska izmantošana", Radio Science vol. 20 n. 4, 1985, 887. lpp. (nav IEEE Darījumi uz antenām un propagandēšanu), doi 10.1029/RS020i004p00887 ([scheda](https://zendy.io/title/10.1029/rs020i004p00887)): iztvaikošanas kanāla augstums no virsmas laika datiem. Atbrīvojiet tehnisko ziņojumu pirms tā: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf).

T. Lindquist, *Wave Propagation Models Troposphere for Long-Range UHF/SHF Radio Connections*, maģistra disertācija, Karlstadas Universitāte, 2020. gada 2. jūnijs, 106 lappuses (papildina programmas autors). Tas ir soli pa solim ceļvedis, brīvs, uzrakstīt aprēķinu ar parabolisko vienādojumu: dalītā posma risinājums (pat platos leņķos), robežas nosacījumi sauszemes un jūras, no zemes līdz soļiem, staru izsekošana uz lineāriem profiliem reizēm, ar skaitliskām pārbaudēm; citē PETOOL bezmaksas rīku MATLAB. Divi noderīgi dati uzreiz: 1) N jutība pret laikapstākļu mainīgajiem lielumiem, δN ≈ 0,26 δP + 4,3 δe − 1,4 δT (hPa un K, ap 20 °C), t.i., 1 hPa kļūda tvaika spiedienā sver trīs reizes 1 °C kļūdu temperatūrā: ir modeļa mitrums, kas jāpārbauda vispirms; 2) vidējais iztvaikošanas kanāla augstums: 5–6 m Ziemeļjūrā, 13–14 m Vidusjūrā, vairāk nekā 20 m Persijas līcī, vienmēr pārāk zems 144 un 432 MHz. Autora slēdziens (frekvence 1 līdz 10 GHz): ārpus horizonta uzticamiem savienojumiem gandrīz vienmēr ir nepieciešams kanāls.

Pavairošanas programmatūra augsnes profilā (horizontam un šķēršļiem, nevis paredzamajam tropo)

- [Radio Mobile](https://ve2dbe.com/rme.html)by Roger Coudé VE2DBE: bezmaksas, Windows (arī Linux un Mac ar Wine), Longley-Rice/ITM modelis, zemes profils automātiski iegūta no kvotas datiem ([lejupielādēt](https://ve2dbe.com/download/download.html)).
- [SPLAT!](https://qsl.net/kd2bd/splat.html): John Magliacane KD2BD: GPL, 20 MHz–20 GHz, Longley-Rice un ITWOM v3.0 modeļi, ziņojumi par šķēršļiem un zudumiem tekstā; jaunākā 2014. gada 1.4.2. versija.
- [ITM, NTIA/ITS](https://its.ntia.gov/software/itm)oficiālais kods: Longley-Rice modelis (20 MHz–20 GHz) ar avotu un dokumentāciju, Radio mobilā un SPLAT bāze!. Kods ir bezmaksas, un jūs varētu pievienot aprēķinu uz ceļa LIVETROPOVHF (zaudējums sakarā ar augsni, kas jāpievieno tropo indekss). Sākotnējais ziņojums: A. G. Longley, P. L. Rice, "Troposfēras radiopārraides zuduma predikācija pār neregulāru Terrain: A Computer Method – 1968", ESSA Tehniskais ziņojums ERL 79-ITS 67, 1968. gada jūlijs (NTIA karte par vēsturisko programmatūru: X1 https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITU-R, programmatūra un digitālie dati](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): bezmaksas kartes ar ΔN un N0 gada vidējo vērtību 1,5° koordinātu tīklā, ko izmanto P.452 un P.617 – noderīgas kā atsauces klimata vērtības indeksam.
- Pathloss (līgums Telekomunikāciju Engineering): komerciāla programma radio tiltiem; Es neesmu atradis publisku avotu, kas apstiprina, kuru ITU rekomendācijas īsteno, tāpēc, lai pārbaudītu.

Atkārtoti izmantojams GitHub kods LIVETROPOVHF

| Projekts | Ko jūs darāt | Valoda | Izmantot iespējamo mums |
| [NTIA/ITM](https://github.com/NTIA/itm) | modelis Longley-Rice amatpersona NTIA, ar iepakojumiem. NET |  C++, C#  | zemes zudums ceļā: integrējas tieši programmā, kas ir C# |
| [fuzzzeee/itm](https://github.com/fuzzzeee/itm) | ITM C++ kods C# tulkojums |  C#  | alternatīva bez dzimtās bibliotēkas, arī noderīga Windows 7 versija |
| [eeveetza/Py452](https://github.com/eeveetza/py452) | ITU-R P.452, identisks MATLAB/Octave versijai, ko apstiprinājis ITU-R WP 3M; MATLAB versija, ko publicējis Šveices Federālais sakaru birojs | Python MATLAB | atsauce uz C# troposcatter formulas un veikta ar ITU validācijas piemēriem |
| [bvinkels/pikrafs](https://github.com/bwinkel/pycraf) | P.452 complete, P.676, SRTM profili; GPL v3, bet iekļauti ITU datu faili nav brīvi komerciālai lietošanai | ♪ | mūsu rezultātu kontrole |
| [ITU-Rpy](https://github.com/inigodelportillo/ITU-Rpy) | daudzi ITU-R ieteikumi, tostarp P.453-13, kas apstiprināti ar ITU piemēriem | ♪ | P.453 N formulas un klimata karšu pārbaude |
| [NCAR/RAOBget](https://github.com/NCAR/RAOBget) | lejupielādēt radio vilnis University of Wyoming teksta formātā | ♪ | diagramma lejupielādēt Itālijas staciju profilus C# |

Piezīme: pašreizējā P.453 versija ir [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), lejupielādējama bez maksas; dokumentā es izmantoju 13, lai reallined.

Jau programmas rokasgrāmatās un atkārtoti šeit: LA0BY (Nordic VUSHF 2019), IK3XTV ("Propagācija uz 2 metriem" un "Propagation VHF – konvekcijas šūnas un gravitācijas viļņi", Radio Magazine, ko nodrošina programmas autors), DF5AI/DK5YA (2003), G3YGF (QST 1983), The VHF/UHF DX Book cap. 2, G4MCU (2003) KP4MD
