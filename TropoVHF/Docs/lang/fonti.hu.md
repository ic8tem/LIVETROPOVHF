# Forrás és képletek

> Gépi fordítás olaszból: kétség esetén az olasz útmutató az irányadó.

A LIVETROPOVHF tropo előrejelzéshez megkérdezett források munkagyűjteménye (2026. november 21.): Alapvető fizika, rendelkezésre álló adatok, közzétett indexek, képletek, rádió amatőr tapasztalatok, validálás és döntések. A képletek száma a LaTeX (1), (2),..., és a szövegben a zárójelben lévő számmal emlékeznek. Az előrejelzési kézikönyv leírja, hogyan használja őket a program.


## 1. Szerző telephelye

IC8TEM megfigyelések, amelyek keretbe a munka, visszajelzések találhatók a források.

- Az olasz földrajz diffrakciót igényel. A hegygerincek, hegytornyok és szigetek néhány tucat kilométerre az állomások, diffrakció hozzájárulás kell számítani a Fresnel paraméter a fő szélén, nem csak a sima föld. Találkozás: a szabálytalan terepen 563 MHz-en (Rio de Janeiro, 27 pont 5 és 30 km között) végzett összehasonlításban a legjobb módszer a P526 volt pengeélekkel, a P.1812 és a diffraktív ék, az átlagos hiba -5 és + 3 dB között, és a diszperzió körülbelül 9 dB volt; a P.1546 volt a legpesszimistább (Costa, Rabanal Ramirez és a PETOOL állomás közelében lévő meredek lejtőkön), emlékeztek arra, hogy széles látószögű propagátorokat és a kétirányú számítást (a rezegek által tükrözött hullám) szolgálnak.
- Egy távoli csatornába való bejutáshoz Fresnel kell. Ha a csatorna a horizonton túl kezdődik, a jel az akadályokon keresztüli diffranciával érkezik a kezdetekig; 1.3.2-től a program kiszámítja azt (az előrejelzések kézikönyve, 10. Találkozó: Rohde & Schwarz (2023) megjegyzi, hogy egy magas csatorna mentén lévő állomások gyakran nem lépnek be, míg a végekhez közel állók igen.
- A körülmények gyorsan változnak. Az elméletek ott vannak, de a feltételek percben változnak, és nincs előrejelzési rendszer veszi az adatokat valós időben: a GFS jön ki 6 óránként, az idő és 0,25 °, és az előrejelzés elveszíti értékét rövid időn belül. Találkozó: Bray (QST, 1961. november) az óránkénti középingadozás körülbelül ± 7 dB 100 mérföld; a csatornák mozog a légköri idő (Rohde & Schwarz); a QSO nyilvántartás 29 / 12 / 20212 / 1 / 2022 mutatja a nyílások növekszik és csökken órákon belül.
- A legjobb most casting marad a DX klaszter- PSKReporter hálózat. Hogy tudjuk, mi történik most, a klaszter foltok, PSKReporter (és WSPR) verte a mintát: ezek intézkedések. Az előrejelzés azt mutatja, hogy hol és mennyi ideig tarthat; a reklámok azt mondják, ha van. A program ezt a helyet és előrejelzést ugyanazon a térképen.

## 2. Cél

Az elképzelés egy harmadik mutató, amelyet maga a LIVETROPOVHF számít ki, egy időjárási modell függőleges profiljából kiindulva, amelyet helyette a Hepburn és az F5LEN -hoz kell hozzáadni. Ma a program olvassa el a térképeket már készen áll: egy index is azt mondja * miért * van megnyitása (kvóta és vastagsága a csatorna, minimális sáv csapdába esett), és ez történhet a helyek, hogy a program gyűjt már.

- Mit tud adni: valószínűségét csatorna per pont és óra, részesedése az alap és a felső, vastagság, minimális frekvencia csapdába, és egy út mentén a töredéke az útvonal belsejében egy vezeték.
- Amit nem lehet megadni: a vékony csatornákat (néhány tíz méter alatt), amelyeket a globális modellek 0,25 ° -on nem oldanak meg, és a párolgási csatornákat a tengeren, amelyek elsősorban a GHz-től felfelé.
- Első gyakorlati célkitűzés: 144 és 432 MHz Európában, a DX klaszterhelyek, a PSKReporter és a helyi dekódolás ellenőrzése.

## 3. Alapfizika

Minden fénytöréssel kezdődik. N az egyes profilszinteken kiszámított: a csatorna, ahol a módosított M reaktivitás csökkenti a hegymászás mértékét, ahol a dN / dh -157 N / km alá esik ([ITU- R P.453- 13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)).

Refrakció (P és hPa, T Kelvin):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

A H relatív páratartalom (%) és a t hőmérséklet (° C) nyomása víz felett -40 és + 50 ° C között:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

Módosított reaktivitás (h km-ben, vagy 0,157 · z z a méterben) és csatornakritérium:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN / dh (N / km) | Rendelet | 144 MHz hatása |
| 0 | szubrefrakció | csökkentett áramlási sebesség |
| 0 és -79 között | normális (standard − 40) | Rádióhorizont, ezzel egyenértékű sugárral 4 / 3 |
| -79 - -157 | szuperrefrakció | Elnyújtott áramlás, "túl jó" |
| - 15 | vezeték (csapda) | beszorult jelek, DX több mint 1000 km |

Hogyan lehet kivonni az N egy modell a nyomás szinten: minden szinten vesz T, relatív páratartalom és geopotenciális magasság, kiszámítjuk N és M, majd a gradiens a szomszédos szintek. A talaj alatti szinteket ki kell dobni (az Alpokon 1000 hPa-nál a föld alatt).

Minimális beszorult frekvencia: a vezeték csak a hullámokat elég rövid, mint a vastagság. Hepburn egy asztali vastagság → minimális hasznos frekvencia ([DX Info Centre](https://www.dxinfocentre.com/propagation/luf.htm)); ez egy puha határ, amely alatt a jel fokozatosan csökken.

| Led Vastagság | Legkisebb fizetendő gyakoriság |
|  61 m  | 962 MHz |
|  91 m  | 464 MHz |
|  122 m  | 276 MHz |
|  152 m  | 185 MHz |
|  183 m  | 133 MHz |
|  213 m  | 101 MHz |
|  305 m  | 53 MHz |

A VHF-tartományban (gyakran 120- 300 m) a táblázat értékei meglehetősen jól követik a törvény állandó; 100 m alatt a képlet alábecsüli a frekvenciát akár 30- 40%. Én magam adaptálom (nem Hepburn által közzétett képlet), f MHz-ben és d méterben:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

A gyakorlatban: a 2 m vesz egy vezetéket gyakran legalább 170- 180 m, a 70 cm elég körülbelül fele. A hálón keringő képlet, az λmax = 0,014 · d ^ 1.5, Kerrből származik (1951, 1. fejezet, eq. 18), és λ centiméterben és állva érvényes, egy olyan vezetékre, amelyben M egyenes vonalban esik: ezekkel az egységekkel körülbelül 2 m 180 m-t és körülbelül 70 cm 89 m-t ad, ez ugyanaz az érték, mint a Hepburn táblában, ami onnan jön. Kerr figyelmeztet arra, hogy ez nem egy nettó vágás: még hullámok több alkalommal is szenved egy csomó a vezetéket, és egy módja annak, hogy "veszteség" egy kicsit javíthatja a vétel még az állomások ki a vezetékből. A 2019-es IEEE Access tanulmány megerősíti, hogy a klasszikus módszerek (Kerr, Kukushkin) nem írják le a valós vágást ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)]: a küszöbértéket jelölésként, nem falként kell használni.

## 4. Ingyenes adatok

A gyenge pont a függőleges felbontás: 1000 és 850 hPa között a modellek minden 25- 50 hPa, azaz minden 200- 450 m, így jól látják az inverziók 500- 2000 m, de nem a vékony csatornák közelében a talaj. A LIVETROPOVHF már használ Open- Meteo izobare, hőmérséklet 850 hPa és inverzió: az index kezdheti ugyanazon hívások.

| Forrás | Mi ad | Felbontás | Felhasználás indexhez |
| [Open- Meteo, GFS](https://open-meteo.com/en/docs/gfs-api) | T, relatív páratartalom, harmatpont, geopotenciális magasság szintenként (1000, 975, 950, 925, 900, 850 hPa...); a határréteg magassága | 0,25 ° nyomás mellett; 16 nap, 120 óra, 6 óránként frissítve | N és M profil, gradiens, alap és a csatorna tetején; már a programban |
| [Open- Meteo, ECMWF](https://open-meteo.com/en/docs/ecmwf-api) | azonos szintek az open-data modellekből 0,25 °; a talaj harmatpontot ad |  0,25°  | második vélemény, hasznos a modellek közötti egyeztetés |
| [Open- Meteo, DWD ICON](https://open-meteo.com/en/docs/dwd-api) | T és relatív páratartalom a nyomásszinten; az ICON- D2 -ban 800, 900 és 925 hPa van | ICON-EU körülbelül 7 km | harmadik vélemény: Európa |
| [Radiosunds, Univ. Wyoming](https://github.com/NCAR/RAOBget) | mért profilok, napi két indítás (00 és 12 UTC), szövegformátum állomásonként | sok jelentős szint, még néhány tucat méterenként | indexellenőrzés; lásd azokat a vékony csatornákat, amelyeket a modell nem lát |
| [GROANS, NOAA NCEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive) | történelmi archívum a műsorszolgáltatás, egy fájl egy állomás, több származtatott paraméterek | a fentiek szerint | évenkénti adatok kalibrálása és összehasonlítás a múlt helyeivel |
| ERA5 (Kopernikusz) | 1940 óta, 37 nyomásszint | Körülbelül 28 km | statisztika és kalibrálás, nem előrejelzés |

- Az Open- Meteo geopotenciális magassága a tengerszint felett van: a föld alá eső szinteket ki kell dobni ([Open- Meteo, felső levegő](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)).
- A talajon 2 m-nél T és páratartalom található: ezek adják az első meredekséget, azt, ahol az éjszakai felszíni csatornák a síkságon és a tengeren születnek.
- Olasz adások a Wyoming Egyetem archívumában (weather.uwyo.edu / upperair / sounding.html): állomások és számok WMO az alábbi táblázatban.

| Állomás | W | Földrajzi szélesség | Földrajzi hosszúság | Megosztás |
| Udine / Campoformido |  16044  |  46,03  |  13,18  |  94 m  |
| Milánó / Linate |  16080  |  45,43  |  9,28  |  103 m  |
| A tenger gyakorlata |  16245  |  41,65  |  12,43  |  12 m  |
| Brindisi |  16320  |  40,65  |  17,95  |  10 m  |
| Trapani / Birgi |  16429  |  37,92  |  12,50  | körülbelül 14 m |

A szöveg formátuma Wyoming ad minden szinten nyomás, magasság, hőmérséklet, harmatpont és páratartalom: elegendő az N és M. Wyoming nincs hivatalos interfész, szkripteket használnak, mint [RAOBget of NCAR](https://github.com/NCAR/RAOBget); a College of DuPage tartja szöveges az állomás ([pl., 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)).](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. Indexek és közzétett módszerek

A Hepburn és az F5LEN ugyanazokból a fizikai összetevőkből indul, de nem teszi közzé a teljes algoritmust; a 2008-2026-os tudományos tanulmányok inkább lépésről lépésre írják le a módszert, és onnan érdemes lemásolni a receptet.

| Módszer | Kezdő adatok | A számítás módja | Hasznos jegyzetek számunkra |
| [Hepburn Tropo Index](https://www.dxinfocentre.com/propagation/hti.htm) | a kanadai Meteorológiai Szolgálat nyers időjárási modelljei (MSC, a térképei alapján; nem GFS / NAM), GrADS-szel tervezett térképek, védett algoritmus ([storia](https://www.dxinfocentre.com/propagation/history.htm)]) | 0-10 lineáris jelgörbületi skála: 0 normál, 1-2 szuperrefrakció, 3-4 néhány csatorna, 5-6 diffúz csatorna; negatív értékek = szubrefrakció | az utánzandó skálát, így a harmadik index azonnal összeveti az első kettőt |
| [F5LEN, világelőrejelzés](https://tropo.f5len.org/WW/info.php) | Európa számára az ARPEGE modell Météo- Franciaország, 7,5 km, több szint között 2 m és 1500 m ([jegyzet szerző](https://tropo.f5len.org/author/f5len/)) | Átlagos index főbérlő, 40 lépés 3 óra, 4 számítások naponta, 28 napos archívum ([új verzió](https://tropo.f5len.org/?p=504)) | erősítse meg, hogy szolgál egy modell sok alacsony szinten |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf) | 60 ECMWF modellszint, talajtól 3000 m-ig | minimális dN / dz oszloponként: -157 N / km, szuperrefrakció -78.7 N / km; a csapdaréteg alapja | klimatológia: Padana Plain az egész évben több csővezetékes európai térség; a Földközi-tenger nyáron közel 100%, télen 15% alatt; Olaszország több mint 30% nyári éjszakákon |
| [Indermuehle és Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861) | GFS 0,25 °, 21 nyomásszint + 2 m-es mezők | M = N + 0,157 z; minimum dM / dz 3 km alatt, a csatorna alapja és teteje, az M-hiány mélysége; 40 m alatt kidobott | 7 évre ellenőrizve 88 MHz-ről 2,68 GHz-re 25 csatornán; jó kapacitás 3 nappal előre (AUC körülbelül 0,74), majd csökken; GFS jobb, mint ERA5 |
| [Hämäläinen et al., IMF 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf) | Harmónia- AROME, 750 m vagy 2,5 km, 65 vagy 90 szint | dM / dh < 0 a modellprofilon, összehasonlítás 4-59 m-es gerendával | függőlegesebbek, mint a vízszintes felbontás; egyik modell sem látja a 15 m alatti csatornákat; a nedvesség dominál a hőmérsékleten |

Harmadik amatőr rádiószolgáltatás már aktív: [TROPOCAST.EU](https://tropocast.eu/)of Artur SQ3A, 2 m és 70 cm Európában. Az előrejelzés oldaláról ([tropocart.eu / Progression](https://tropocast.eu/forecast)):

- Adatok: DWD ICON-EU (áthaladása 0,0625 °, körülbelül 7 km), alapértelmezett forrás; vagy kanadai GDPS (0,15 °), kevésbé részletes.
- Módszer: Az ITU- R P.453 szerinti N és M a talajközeli réteg és a 850 hPa közötti profilból; a rétegek felismerése, a földhöz és magashoz vezető csatornák egyesítése és a színek küszöbértékei a szerző megfelelő kalibrálása, nem publikálva.
- Termékek: "teljes" kilátás 0- 3000 m, különállóbb kilátás "a földre" és "magas" (jelenleg csak a beépítendő térképeken); pontszám 0- 100 kilenc osztályban (38 háttér alatt, 38- 52 marginális, 53- 58 gyenge, 59- 65 közepes, 66- 72 jó, 73- 80 nagyon jó, 81- 88 magas, 89- 95 erős, 96- 100 erős); előrejelzés + 72 h egy órán belül, animációval és archívummal.
- Területek: Európa, Lengyelország, Egyesült Királyság, Németország, Franciaország, Spanyolország, Olaszország, Csehország, Horvátország; interfész olaszul is.
- Újrahasználat: a térképek egy API kulcsnélküli iframe-et tartalmaznak; van egy dokumentált API (OpenAPI és JSON smea) is. A licenc nincs feltüntetve: mielőtt harmadik forrásként használja a Hepburn és az F5LEN mellett, kérdezze meg a szerzőt.

Különbségek a projekttől: A TROPOCAST 850 hPa-nál (körülbelül 1,5 km) megáll, míg a megemelt csatornák 2000 m-en és azon túl érkeznek ( LA0BY eset: 22 / 11 / 2018), és nem ad indexet sávra vagy útvonalra. A 0-100-as osztályokat összehasonlíthatjuk a 0-10-es indexünkkel ugyanazon a napon.

TROPOCAST képszerkezet (a program szerzője által megadott linkről): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, ahol n az előrejelzési lépés. Az oldal megtiltja az automatikus visszavonulást a robotokba, így nem tudtam olvasni a képet; ezt is, mielőtt ezeket a térképeket LIVETROPOVHF szolgál a szerző engedélyével.

Notebook: [DJ5AR, kategória](https://dj5ar.de/?cat=174)(Magonza, 21 cikk 2017-2023, 2 m- 13 cm). Írországtól több hétre (2023. június 23.), az FT8 -tól Fehéroroszországig (2020. szeptember), Olaszország- Németországtól az Alpokon és az Adrián keresztül (2017. augusztus). Ismerje el a "túl magas" (1000 m-re fordított) csatorna esetét, amelyet nem használhat a talajból: egy újabb megerősítés arra vonatkozóan, hogy össze kell hasonlítani a csatorna és az állomások arányát. Radioprocesszorokat használ rekeszmérőként (1028 km-ig) és vertikális profilokat Párizsban, Bordeaux-ban és DWD-ben.

Összefoglalva, a közös recept: vertikális profil → N és M szintenként → réteg dM / dh negatív → bázis, felső, vastagság és erő a csatorna → index. Hepburn "titkos" része a valódi események empirikus kalibrálása, amit foltokkal lehet rekonstruálni.

## 6. Hasznos formátumok a programhoz

Az N és M mellett (fizika alapszakasz) ezeket használják a függőlegesről a pontszámra, majd az útvonalra. Az ITU-képleteket részben rosszul olvasott PDF-ekkel rekonstruálták; november 22-én a hivatalos szövegeken felülvizsgálták ([ITU- R P.452- 18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453- 14, P.526- 16, P.372- 18), és a következő kódban helyesbítették: a troposzcatter 190 (nem 190,1) állandója plusz a gázok abszorpciója 3 g / m3-rel; α nem alacsonyabb -3,4 (eq. 55a) -3,4 (eq. 55a) a 3E14 képlettel; a csővezetékek gázai = 7,5 + 2,5 · (eq. 9a); diffreakciója teljes gömbfölddel (eq. 232- 37: -átlag, 2: 20 °, a tengerszint + 20 log, polation, polation); 0,04.04 képlettel; 0,04.04. Rumore P.372-18: táblázat 1. és galaktikus megfelel; Mesterséges zaj görbék akár 250 MHz és a galaktikus akár 100 MHz, a program kiterjeszti ugyanazokat a törvényeket.

1. A modell két szintje közötti gradiens (h km-ben, M az M egységben):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. Konduitszilárdság és vastagság: vastagság d = felső -bázis (talajcsatorna esetében, bázis = talaj); szilárdság M hiányosság a bázis és a felső között.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3. Földsugaras egyenérték (P.452-18, eq. 5 és 6a), ΔN = átlagos N csökkenés az első km-ben, pozitív, N / km-ben:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

A ΔN = 40 van körülbelül 4 / 3 k (standard atmoszféra); a ΔN, amely megközelíti 157 k hajlamos a végtelenségre, azaz, a földi rádió lesz lapos: ez a kezdete a csatorna.

4. Rádióhorizont magas antennah-méter, amelynek sugara km-ben\ _ e:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5. A csatornának a szélességi fok (P.452- 18, eq. 2), az idő százalékos aránya;

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

41 ° N, minden tengeri (μ1 = μ4 = 1), ad körülbelül 11% az idő: a klimatológiai alap, hasznos, mint egy referenciaérték, amelytől a várt index eltér.

6. A troposzcatter vesztesége (P.452- 18, eq. 45; f MHz, d km, θ szög az út mrad, N0 földi reaktivitás az útvonal közepén):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

A -0,15 · N0 kifejezés azt mutatja, miért segít a nedves levegő csatorna nélkül is: 50 extra N egység 7,5 dB-vel kevesebb, mint veszteség. A LIVETROPOVHF már használja a scatter veszteség G3YGF verzióját.

7. Csatorna-veszteség (P.452-18, eq. 46-51): rögzített kapcsolási veszteség, valamint a szögmérő csillapítása, amely a frekvencia köbgyökével növekszik (f GHz, a\ _ e km):

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. Bean és Dutton (1966) és Kerr (1951)

A két klasszikus szöveg három olyan dolgot ad, amire azonnal szükség van az indexhez: a gradiens küszöbértékek már a GFS azonos szintű nyomásán állnak, ami dB-ben a reaktivitás változását jelenti, és hogy milyen időjárási helyzetek hozzák létre vagy pusztítják el a csatornákat. * Radio Meteorology *, NBS Monograph 92.

1. A nyomásrétegek profilosztályozása (Bean és Dutton, 6.5., 248. o.; -dN / dh N / km). Rádiós hullámhosszra tervezték, modellszinten:

| Út | Részvények | Szubrefrakció | Nem laminált | Szuperrefrakció |
| 1000- 850 hPa | 0- 1,46 km | -dN / dh < 0 | 20 és 60 között | 100 felett |
| 850- 700 hPa | 1,46- 3,01 km | -dN / dh < 0 | 20 és 50 között | 80 felett |
| 700- 600 hPa | 3,01- 4,20 km | -dN / dh < 0 | 15 és 40 között | 70 felett |
| 600- 500 | 4,20- 5,57 km | -dN / dh < 0 | 10 és 30 között | 50 felett |

2. Mennyi az érték dB-ben (Bean és Dutton, felső határ 6, 233- 258), 70 MHz és 180 MHz között a horizonton túlnyúló útvonalakon:

- Minden réteg (a földön vagy a magasságban) a "sima" profilhoz képest 10-25 dB-es jelet mutat minden százalékon; a nagyobb nyereség valószínűleg a fókuszhatás (200 km-es út) miatt a magas billenő rétegekből származik.
- A talaj reaktivitása Az N és az első kilométerben mért ΔN csökkenés a havi középérték 0,4 és 0,95 közötti együtthatóval, a középérték 0,7 (20 út 130-446 km, körülbelül 100 MHz).
- Éves ciklus: 0,18 dB N egységenként; napi ciklus 0,2-1,1 dB N egységenként, maximum 175-200 km-es útvonalon és télen.
- A Misme összetett paramétere 0,5 dB / (N / km) · (egyenértékű gradiens -40) plusz egy termikus stabilitási paraméter; a D turbulencia paraméter (a valós profil és az aidiákus 1 közötti terület, 0-3 km) csak az egyszerű ΔN-hez képest javul. A szerzők következtetései: a ΔN az egyszerűség legjobb paramétere.

3. Referenciaprofil és anomália (Bean és Dutton, 3.8. és 3.9.). A CRPL referenciakeretének exponenciális atmoszférája az Ns-től kezdve a "normál" profilt adja; az A egység azt méri, hogy a valós profil mennyiben különbözik minden résznél. Ez a természetes módja annak, hogy átalakítsa a GFS profil egy anomália, hogy a térképen:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

Ns = 313 ΔN értéke -42 N / km, azaz 4 / 3-as sugár. A -157 N / km-es vezetékre vonatkozó kritériumot a Snell-törvény is előírja egy vízszintes sugárra (eq. 4.23), amely szinte független az antenna magasságától.

4. Ahol és amikor a vezetékek születnek (Bean és Dutton § 4.4; Kerr sapka. A "Horizont 2020" kutatási és innovációs keretprogram (2014-2020) végrehajtását szolgáló egyedi program létrehozásáról és a 2006 / 971 / EK, a 2006 / 974 / EK, a 2006 / 974 / EK, a 2006 / 974 / EK, a 2006 / 974 / EK, a 2006 / 974 / EK és a 2006 / 974 / EK határozatok hatályon kívül helyezéséről szóló, 2013. december 3-i 1303 / 2013 / EU európai parlamenti és tanácsi határozat (HL L 347., 2013.12.20., 965. o.).

| Folyamat | Helyzet | Hatás |
| Figyelem | forró és száraz levegő a hidegebb tengeren folyó földből (jellemző a nyári ingujjra a jó idő után); elsősorban a páratartalom hiányát számolja | intenzív csatorna a tengeren |
| Éjszakai besugárzás | nyugodt ég, gyenge szél; alacsony felhők vagy sok nedvesség megakadályozza | vezetett a földre, amely napnyugtakor született, nő az este, és gyorsan eltűnik napfelkelte után, különösen belül |
| Hátrány | az anticiklonból származó, fűtött és száraz levegő a friss és nedves levegő felett; gyakori árvíz az anticiklonok hegyeiben és délkeleti szektoraiban | szuperrefraktív réteg vagy nagy csatorna; szinte mindig a szabványnál magasabb gradiens fordított |
| Tengeri és szárazföldi | part, néhány száz méter forgalom | helyi csővezetékek a part mentén |
| A csatorna ellen | keverés: a nap által melegített talaj reggel és délután között, a szél a földre néhány m / s a durva terepen; Éjszakai köd | valószínűsége vezeték szinte semmi |

5. Két új hozzávaló. A függőleges mozgás: a konvergencia-zónák emelkedő áramlatai eltávolítják és letörlik a rétegeket, a süllyedés erősíti a jelet (Moler és Holden: átlagosan 2,5 dB-rel több, mint a felemelő levegő, a tengeren az L és X sávban). Az Open- Meteo már függőleges sebességet biztosít a nyomásszinten. És a nyomás trend: egy 140 km-es pályán 186 MHz-en a jel csökkent, amikor a nyomás esett az adó és a középpontja az út, és visszatért magas és evanescence nélkül, amikor ment fel, mindössze 2 (Bean és Dutton, fig. 6.9, p. 241).

6. Kerrtől. A maximális hullámhossz (alapfizika szakasz) képletén kívül a Kerr hozzávetőleges képletet ad az M-hez az egyetlen hőmérsékletből, gőzből és magasságból (3.9. §, a régi 79-es és 4800-as állandókkal, majd 77,6-os és 4810-es értékkel helyettesítve), és megjegyzi, hogy a fordított szélvágás hullámokat generálhat, amelyek a csatorna magasságában lengenek, a mély szökések lehetséges oka (3.26. §). Olasz forrás jelentette Bean és Dutton és keresni: L. Bonavoglia (december 1958), "Korreláció a meteorológiai jelenségek és a terjedés"...

## 8. Amatőr rádióműsorokból és technikai jelentésekből (második sorozat)

A program szerzője által benyújtott hét dokumentum. Három szolgál közvetlenül az index: W3SZ a troposcatter "padló", LA0BY olyan esetekben, hogy Hepburn nem előre, Hepburn (fordítás LU7MC) az időjárás osztályozása a csatornák.

1. Troposcatter: a jel, hogy mindig létezik. R. Rehr W3SZ ", Tropospheric Scatter (Troposcatter) propagáció VHF, UHF, mikrohullámú sütő és gyakoriság", Pack Rats Conference 2014; J. R. Duffey KK6MC, "The Troposcatter propagation mode and How to Predicted Coverage on the VHF / UHF szalagok", CSVHF 2017.

- 100 km-től 700 km-ig, 144 MHz-től 10 GHz-ig; a veszteség 30 log f frekvenciával növekszik.
- Mindenekelőtt a horizont felé mutató kiinduló szöget számolja: minden plusz fok költsége körülbelül 9- 12 dB ( M1CNK, Collins módszer).
- Nyáron a jel átlagosan 10 dB-vel magasabb, mint télen; délutánonként körülbelül 5 dB-rel alacsonyabb, mint kora reggel vagy este. Az KK6MC 13- 15 dB teljes variabilitást mutat az idő, idény és idő függvényében.
- Yeh módszer, a legegyszerűbb programozni (f MHz-ben, d méretben, θ szög scatter fok, α geometriai átlaga a nyílások a két köteg):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Larsen (1968) összehasonlítása 15 valós kapcsolaton: átlagos négyzethiba 5,1 dB Collins, 7,4 Yeh, 8.1 NBS és CCIR, 16,8 Rider. Minden módszer javulni fog, ha az Ns állandó marad (Yeh 5,4 dB-re esik): a nap törőereje nem számít túl sokat, így a tropo indexnek a rétegeket kell megnéznie, nem csak az Ns-t. A 15 kapcsolatból álló táblázat a mért veszteségekkel (beleértve a Feldberg- Monte Baldo-t, 322 km-t 957 MHz-en, 223,1 dB-en; az Israel-t, 319 km-t 77,5 MHz-en, 163,3 dB-en) jó próbapad a számításunkhoz.
- Az KK6MC azt a veszteséget adja, hogy egy tipikus állomás képes 2 m-nél nagyobb távolságra maradni, 184 dB (FT- 817 és 3 elem SSB-ben, körülbelül 130 km) és 215 dB (hosszú tömlők SSB-ben, körülbelül 430 km) és 236 dB (kW és yagi kapcsoltan, körülbelül 640 km) között. Így lehet lefordítani az index dB-ket mérföldekre.
- Készenléti kód: W3SZ Yeh és Collins program (nitehawk.com / w3sz / YehCollins.zip) és Bob Atkins KA1GT számológép (bobatkins.com / radio / troposcatter.html).

Egy másik forrás a scatter: I. Roberts ZS6BTE, "Troposferic Scatter propagation", * Radio ZS *, December 1984, a note of 2013 ([qsl.net / zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). Főként Hasznos geometria: scatter θ = θ0 − θ1 − θ2 szöge, a θ0 = d / R, θ1 = (h1 − h\ _ ostacolo) / d1 + d1 / 2 R és ugyanaz a másik állomás, R = 8446 km (4 / 3 a Föld sugarának). Ez az a darab, amelyet a "padlón" a talaj profiljából származó szögek kiszámításához használnak. Egyéb adatok: minden egyes szög több költséggel körülbelül 10 dB; az Ns 280-ról 300-ra csökken a veszteség körülbelül 4 dB; a szórási térfogat 300 és 2000 m között van 150 km-en, 600 és 3000 m között 300 dB-ben; a szórási térfogat ideje körülbelül 20 dB-t emel a megadott képletekkel (a 900 MHz-es és 872 km-es szabad tér időtartama körülbelül 150 dB, nem 125), így a képleteket az W3SZ -n kell ellenőrizni, mielőtt azokat használnák.

2. Csatorna típusok (W. R. Hepburn, "Tropospheric propagation Modes", dxinfocentre.com / propagation / tr- modes.htm, spanyol LU7MC fordítás). Töltse ki a táblázat Bean és Dutton hasznos esetek Olaszországban:

| Típus | Mikor | Megjegyzés az indexhez |
| Sugárzás | nyugodt és békés éjszakák a földön | Kövesd a földet, röviddel hajnal után eltűnik. |
| Nagy nyomás (süllyedés) | anticiklon | egész nap kitart; éjszaka a besugárzásból származó vezeték blokkolja a távoli jeleket, így nappal jobban megy |
| Első | egy meleg front előtt, egy hideg front mögött, egy álló fronttól északra | rövid; a rossz idő zavarja őt |
| Hegysüllyedés | föhn, bora: levegő jön le, felmelegedés és szárítás | az Adriai-tengeren és az Alpoktól délre található föhn |
| Figyelem | meleg és száraz levegő hideg és nedves talaj felett, például eső után | a földre vezetett |
| Völgy | forró levegő hideg levegő megáll a völgyben | nem maradhatunk a napnál. |
| Marino | meleg és száraz levegő hideg víz; érkezik a parton a szellő | csúcs délután; tavasz és nyár elején a legjobb évszakok |

A tengeri párolgási csatornák (0- 30 m) csak 3 GHz-et szolgálnak, és nem lépnek be a hátországba: 144 és 432 MHz esetén figyelmen kívül lehet hagyni. A magas hegyek gát, a sivatagok túl szárazak.

3. Az esetek Hepburn nem előre (S. Heck LA0BY, "Extreme Tropo propagáció 144 MHz és fel", Nordic VUSHF 2019, az JO59IX és 500 m magasságban):

- 2018. november 22.: 70 cm UA6 (körülbelül 2600 km) 6-8 órán át. Hepburn csak a balti államokat és Lengyelországot jelezte. A vezetéket 900 és 1500 m-nél látták a hőmérséklet-térképeken, és 2000 m fölött ért véget; a norvég oldalon magasabb volt, és a dombon csak LA0BY lépett be. A földön az idő nem úgy nézett ki, mint egy trópus: hideg és köd a tetején.
- 2015. február 11.: 2 m és 70 cm a 9A és S5 (kb. 1500 km) felé az Alpokon keresztül, szintén nem előre látható Hepburn.
- 2011. október 15.: 2 m EA1 (2135 km) magas csatornában, három tengerszakaszon és három talajon.
- Egyéb események: 2003. november 6- 11. kelet felé (2050 km), 2004. december 10-ig Franciaországig.
- Helyszíni címek: sima köd, szabad csúcsok, kis szél, nedves talaj. Normál hőmérséklet-gradiens -6,5 ° C / km; valós esetben ( VK3KAQ) + 7 ° C 20 m-ben.

A szerző következtetése: A Hepburn és az F5LEN nem elég, a profilokat magasságban szolgálják az útvonal mentén. Számunkra: ezek az ellenőrzési időpontok a modellek múltbeli adataival újraszámítandó esetek, és a menetvonal indexének össze kell hasonlítania a csatorna kvótáját az állomások kvótájával.

4. Olasz tapasztalat (G. IV3GBO, "Types of VHF propagation SSB / CW", 2017): 450 és 3000 km között, 450 és 3000 m közötti rétegekkel; köd, mint egy nyom; nyílások az Adrián Puglia, Albánia és Görögország felé. Szétszóródás 100-ról 500 km-re.

5. Bevezetések (P. Wilton M1CNK, "An Introduction to VHF / UHF propagation", 2013): 70 cm gyakran jobb, mint 2 m; 10 m pólus 144 MHz a kiindulási szög körülbelül 3 °, 20 m körülbelül 1,5 °.

6. NASA-jelentés (E. Hirschmann, "Tropospheric and Ionospheric Effects on Radio Frequency (VHF- SHF) Communication", Goddard X-731-67-89, 1967): a műholdakkal szembeni távolsági hibákra vonatkozik, nem a DX-re. Ugyanezt az N képletet és exponenciális profilt hozza vissza (például Floridában: N 370, ΔN -55, állandó 0,161 / km), és két NBS-forrást küld keresésre: Bean és Thayer ", CRPL Exponenciális Referenciaatmoszféra", Monograph 4 (1959), and Bean, Horn and Ozanich ", Climatic Charts and Data of Radio Refractive Index for the United States and the World, Monograph 22 (1960).

## 9. A VHF / UHF DX könyv, 2. fejezet

I. White G3SEK (edited by), * The VHF / UHF DX Book *, DIR Publishing 1995 (ISBN 0 9520468 0 6; Digitális replika 2017), chapter 2 "VHF / UHF propagáció", pp. 2-13, 2-22-2-2-29 és 4-11. Ez a legközelebbi szöveg a rádiós amatőr tapasztalatok között az összegyűjtött; megerősíti a már használt küszöbértékeket, és új szineptikus szabályokat ad hozzá.

Szám

- Az N azonos képlete (77,6 P / T + 3,73 · 10 ^ 5 e / T2); példa: 1000 hPa, 7 ° C és = 10 hPa károsodás N = 324. Egy jól kialakított légkörben, például a vihar után, az N majdnem lineáris, körülbelül 39 egység / km az első 2-3 km.
- A 0 és -79 N / km közötti gradiensek normálisak, -79 alatt szuperrefrakció van, több mint 0 subrefrakció. A könyv ragaszkodik ahhoz, hogy a hosszú DX nem az egyszerű szuperrefrakció, amely továbbra is az egész úton, hanem a csatornák.
- Minimális csatornavastagság, egy kicsit óvatosabb, mint Kerr és Hepburn:

| Band | Minimális vastagság |
| 50 MHz |  400 m  |
| 70 MHz |  300 m  |
| 144 MHz |  200 m  |
| 432 MHz |  100 m  |
| 1,3 GHz |  50 m  |

- Az arány nem arányos a hullámhosszal (körülbelül 65 λ 50 MHz-en, 220 λ 1,3 GHz-en), mert a vezeték szélei "elmozdultak". Az Egyesült Királyságban a csővezetékek gyakran 144 MHz-en vannak, de szinte soha nem 70 MHz-en vagy alatta, és gyakran a 432 MHz erőssé válik, ha a 144 gyenge.
- Csővezetéken a jel 1 / d-ben csökken 1 / d2 helyett: legfeljebb 1000 km-re a csillapítás lehet hasonló a szabad tér.
- Minimális állomás kapacitása 2 m-en (100 W, 15 dBi yagi, 2 dB zaj): 217 dB, ami átlagosan 400-500 km SSB-ben; a CW-ben körülbelül 20 dB (4- 7). Egyetértek az KK6MC asztallal.
- Szóródás: a veszteség körülbelül 15 dB-rel emelkedik a frekvencia háromszorosára, és 10 dB-re a szórási szög minden fokára (Yeh képlet; a kivont szövegben az állandó olvashatatlan, ezért az W3SZ változatból kell venni). A konvektív sejtekből származó "fel" szóródás rendszeresen 500 km-ig, 1000 km-ig terjed jó állomásokkal. A nagy esőzések és a meghosszabbított körülmények minden sávon ritkák.

A csatorna típusai

- A földön: a földön szaporodnak rosszul, mert a talaj elnyeli és terjed; a nyugodt tenger érkezik messze (Scilly-Canarie, 2500 km, körülbelül évente egyszer 2 m). A párolgási csatornák gyakran csak körülbelül 15 m magas és mikrohullámokat szolgálnak fel.
- Magas: 450 és 2000 m között, a hosszabb DX a földön; a "magas" jelzés az alábbi állomások felett. A "vesztes" csatornák kevésbé erős jeleket adnak, de lehetővé teszik a közbenső távállomásokat is.
- A vezetékek pontos irányokkal rendelkeznek, és akár órákig vagy napokig is eltarthatnak, ellentétben a szórványos E-vel.

Az indexre vonatkozó új szimbolikus szabályok

- Kora anticiklon: a levegő esik a központba néhány méter / óra, és a fordított csökken, ahogy a nagy nyomás fejlődik; a nagyobb nyílások után több nap terjedése már jó. Egy rövid távú anticiklon nem elég.
- Nagynyomású perem: a réteg kupola és a szélek alacsonyabbak, így az állomások jobbak lesznek.
- Irány: Megfigyelték, hogy a hosszabb útvonalak mentén fut az izobares vagy a dorsals (Flavell G3LTP, 1984), de vannak kiváló kapcsolatok is sugárirányban a központtól. Példa 1987. augusztus 29- 30.: Angliából a nyitás két nap alatt 180 ° -kal fordult (Spanyolország, Franciaország, Olaszország, Ausztria, Norvégia) az anticiklon eltolódását követően.
- Ellene: vastag izobare (erős szél), stratocumuliform felhők szélén a réteg (korlátozza a legmagasabb frekvenciát).
- Határok: a hideg front felnyomja a forró levegőt, és létrehoz egy fordított mentén az elülső; a legjobb irány az eleje mentén halad, miközben halad, de ez tart kevés.
- Szezon: Az Egyesült Királyságban az anticiklonok gyakrabban a nyár végén és kora ősszel.
- Vizuális nyomok: a látóhatár feletti sötét vonalon rekedt füst; nagy nyomással 1000 m alatti bázisú rétegek jelzik az 1000 m fölötti talajcsatornát.

Hivatkozások a keresésre: R. G. Flavell G3LTP, "Studies of an Extensive Anticyclonic propagation Event", * Radio Communication *, February 1984, p. 128; J. Gannaway G3YGF, "Tropospheric Scatter propagation", * Radio Communication *, September 1981, p. 710; P. W. Sollom G3BGL, "A Little Flutter", W6ZGN

## 10. Igazolt amatőr rádió modell: Biancomano és Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, "Practical Tropospheric Model for VHF / UHF propagation Prediction", * National Weather Digest * 22: 2, 1998, pp. 15- 24, free in [PDF from the National Weather Association](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf).

- Adatok: az Egyesült Államok keleti részén, 1995-1997-ben 47 állomás 00 és 12 UTC rádiótávlatai; földi elemzés 700 hPa-ra (az elején 400-ig), N profilon, M. A partszakaszok, szigetek és hegyek kivételével.
- Minden egyes felmérésnél: a csatorna jelenléte és kvótája, maximális hullámhossz, amely vezethet (gyakran a hullámhossz 100-szorosának felel meg), a szomszédos szintek közötti, valamint az egyik szint és a második szint közötti potenciális hőmérséklet legnagyobb gradiense (vastagabb rétegek esetében). A talajcsatornák nem használatosak.
- Első szabály: a stabilitási indexek (emelt index, K, totális- total, SWEAT) a felméréstől számított 12 órán belül 3 egységen belül maradnak, és az a nyomásszint, ahol a potenciális hőmérséklet tovább nő, nagyjából ugyanaz marad. Ha igen: a következő 12 órában várható javulás.
- Második szabály, dinamikus vezérlés: a nem emelkedő határréteg, CAPE, a vastagság változása 500 hPa-ra három felmérésen, a lejtő eltérése a határokon belül.
- A sávküszöb: 100 cm vagy annál nagyobb átmérőjű vezeték 432 MHz-en, 222-en és gyakran 144-en is megnyílik.
- Space filter: állomások egy hatszögletű rácson mintegy 400- 500 km; az előrejelzés csak akkor éri meg, ha legalább 5 Contigue állomások pozitív (ha ki lehet hagyni egyet). 5 állomással a hamis véletlenszerű pozitív valószínűsége elhanyagolható lesz.
- A QST "The World Above 50 MHz" oszlopának eseményei elleni ellenőrzés: 1996-ban 48 rendezvényt terveztek 67-re (71%); a 15 nagyobb eseményből 12-re. A pontosság a 432 MHz-en akár 70%, a 144 MHz-en pedig körülbelül 60%, 12 óra elteltével; a tényleges események majdnem kétszeresét biztosítja; 19-ből 9 hiba késve érkezett, mert 12 órán belül számos javítás történik.
- További északi állomásokhoz hozzáadva: az óramutató járásával megegyező irányban forgó szél (melegindítás) extra feltételként.

A teljes PDF (által biztosított a program szerzője) három további részletet. A szabály "vastagság = 100-szor hullámhossz" ad 600 m 6 m, 200 m 2 m, 125 m 1,25 m és 70 m 70 cm (ábra. 3): a 70 cm kevésbé súlyos, mint Kerr és Hepburn (körülbelül 89 m), a 2 m ez egybeesik a VHF / UHF DX könyv. Az óramutató járásával ellentétes irányban forgó szél a szerzők szerint felmegy, különösen azért, mert melegebb levegőt hoz a magasságban, ami miatt az N esése nyomás hatására nő; a nedvesség, ami ezzel jár, nem tűnik destabilizálódónak. A függelékben található dN / dh zárt formában egy állandó termikus gradiens atmoszféra egy éles ugrás a hő gradiens és páratartalom egy adott kvóta: hasznos, mint egy vizsgálati esetben a kódot. A potenciális hőmérséklet meredekségének nyomát K adja. H. Jehn, "The use of potential refractive index in synoptic scale radio meteorology", * J. Meteorology * 17, 1960, 264. o.

Mit kell venni a index: (1) a kitartás időtartama: hasonlítsuk össze a profilt a megelőző 12 órával, hogy az óránkénti előrejelzés szabad; (2) az űrszűrő: a pont csak akkor számít, ha a javulás néhány száz km-es területet érint, ami csökkenti a hamis riasztásokat; (3) a hőmérséklet és a harmat alapján számított stabilitási indexeket 850, 700 és 500 hPa-ra (Open- Meteo már felemelte az indexet és a CAPE-t); (4) a szél, amely 1000 és 850 hPa közötti magasságban forog; (5) az ellenőrzési módszerük, amely ugyanaz, mint amit a programhirdetésekkel szeretnénk csinálni.

Egy másik forrás ugyanebben a csomagban, kevés hasznunkra: P. K. Rastogi, "Stratosphere and Troposphere (S- T) Studies at Millstone Hill", NASA 1984 (document 19840019069): 440 MHz radar that measures the turbulence of clean air between 4 and 25 km, with thin layers related to gravity waves and wind cucking. Megerősíti, hogy a szóródást okozó turbulencia rétegelt, de túl nagy az esélye a csatornáknak.

## 11. Harmadik forrásforduló (november 20.)

Nagy csatornás történelmi esetek. J. Macoun OK1VR, 2 m-re Sněžka csúcsától (1602 m), 1958 őszén, 1959-es cikk, melyet 2006-ban fordítottak le ([okskw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)]. 1958. október 27- 28-án az első csehszlovák-angliai csatlakozás 145 MHz-en ( G5YV, 1220 km) és sok más között 1100 és 1250 km között, 23 után stabil jelzésekkel. A szerző két felülírt inverzióra, a felső körülbelül 2200 m-re sorolja, a jel csak ott tér vissza a földre, ahol az alsó réteg feloldódott; az állapotot egy lassú anticiklon részesíti előnyben az Atlanti-óceántól a kontinensig. Otthonról (Prága) maximuma 530 km volt: csak a magassági állomások léptek be a csatornába. Ez egy másik ellenőrzési eset (mint például az LA0BY 2018), és megerősíti, hogy az indexnek 2000 méteren keresztül kell keresnie, és össze kell hasonlítania azt az állomás részarányával.

Történelmi könyvtárak NBS (hasznos keresni klasszikus művek, nem képletek):

- W. Nupen, * Bibliography on Tropospheric propagation of Radio Waves *, NBS Technical Note 304, 1965, 356 pages, with index for tematies (also "condition replies") and by country. Olasz bejegyzések: L. Sacco, "The radio connection in atmoszféra super- refraction regulation", * High Frequency * 24 (6), December 1955 (Italian research: super- refraction under 500 m common day and night); S. Silleni, * Annali di Geofisica * 8 (1), 1955; J. M. Clara e A. Antinori, prova tra Sardegna e Minorca da 238 a 297 MHz (390 km), * Electrical Communication * 33 (2), 1956. A. F. Clarke (Fort Huachuca, 1956), naponta kiadott egy előrejelzést a maximális hullámhosszról a következő reggelről, a következő hőmérsékletről, és a szélről, előrejelzésekről.
- A. L. Abbott, * Bibliography of Tropospheric Radio Wave Scattering *, NBS, material until January 1960, about 670 tételes rendelés évente: hasznos a troposcatter rész.

Közzététel és megfigyelések: F. W. Odenthal DK5EQ, "Ausbreitung 2m - Eine Einführung" (31 / 05 / 2024): szinte optikai akár 200 km, tropo egy csatorna vagy fénytörést túl, nagy nyomással, és tavasszal és ősszel; idézi a VHF oldal DM2HB és a jegyzetek DK5YA tropo és Es. A. Johnson VK3DXE a blog EI7GL (december 2021): WSPR VHF utak több ezer km Ausztráliában felfedezték; A trope nem csak nyáron. VHF marina és AIS ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)]): hatókör 1,23 · ° h (ft) tengeri mérföld antennánként, szétszóródva mintegy 50 nm, tipikus trope javulás 100- 400 nm; a csatornák felismerik, mert a távoli állomások olyan erősek, mint a szomszédok. Chelmsford ARS News, December 1983: A 2 m-es versenyeken az angol keleti part állomásait a kontinens felé vezető tengeri útvonal részesíti előnyben.

ARRL, * The Radio Amateur 's VHF Manual *, 1972, cap. 2 "Propagation Beyond the Horizon" (17- 19). Már ismert működési tippek (kövesse a nagy nyomású területek, amelyek lassan és mindenekelőtt azok hátsó szélei; kora ősz a legjobb időszak; tengerparti területek, nagy tavak és völgyek a folyók által támogatott rétegezés). Két új adat: (1) füge. A 2-4 a standard légkört (relatív páratartalom 70% -12000 láb, minimális fénytörési arány) hasonlítja össze a jellegzetes erős fénytörési sugárral, a tiszta hőmérséklet-törés és a keverési arány körülbelül 4000 láb (körülbelül 1200 m): ez az az aláírás, amelyet számításunknak fel kell ismernie a GFS-szinten; (2) az igazi csatornák 144 MHz alatt ritkák, és a legkisebb sűrűség szerint a UHF-ben a leggyakoribb. Hasznos QST bibliográfia kereséshez: Hull, "Air- Mass Conditions and the Bending of UHF Waves" (June 1935, May 1937); Hoisington, "Paintless Prediction of Two-Meter Openings" (October 1949); Collier, "Upper- Air Conditions for 2-Meter DX" (September 1955); Botts, "A Night to Remember" (Január 1970, hajtott szárazföldön).

PA0EHG, page "Tropo" ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): sumber of the troposcatter from the article of G3YGF, already in the manual. Használjon két gyakorlati szabályt a scatter számításunk szabályozására: körülbelül 10 dB extra veszteség minden szögben minden állomás horizontján, és több mint 100 km körülbelül 9 dB plusz minden 100 km útvonal (60 mérföld); a közös térfogat kezdődik körülbelül 600 m 100 km és körülbelül 9 km a 480 km. Nem kezeli a csatornákat, a szavazásokat vagy a törésküszöböket.

PA0EHG, PROPBER számológép ([](https://pa0ehg.com/propagat1.htm)oldal, propber.exe program, a program szerzője által). Ez egy 36 kB DOS programot írt BASIC és összeállított Microsoft BASIC Compiler 5.60 (1985): ez csak fut DOS vagy DOSBox. A belső szövegekből: a frekvencia, távolság (vagy a két helymeghatározó, szintén a régi QRA formátumban "E-locator"), minden antenna magassági állomás, zajérték, FI sáv, teljesítmény, akadály (magasság és távolság) és antennanövekedés (0 = példás átmérő számítás, hatékonyság 50%). Visszaadja az optika maximális távolságát, a gőz elnyelésének megfelelő optikai veszteségét (0, 1 dB / km 24 GHz-től), a troposzcatter elvesztését "a helyszínekre" (azaz az akadályok által megadott látószögre), valamint az S / N arányt a két érzékszervben, "a maximális abnormális terjedéssel". A numerikus állandók nem olvashatók a lefordított futtatható fájlban. A magas sávokra javasolt módszer is hasznos számunkra: kalibrálni a számítást egy mért kapcsolaton (különbség a mért és a számított S / N között), és ugyanazt a különbséget alkalmazni egy másik sávra. Ez ugyanaz a minta a számításunkban, de az S RTM1 profilt használja egyetlen akadály helyett, és a GFS előrejelzést a rögzített árrés helyett.

JAPBER, konstansok olvashatók a végrehajtható (számok Microsoft Binary Format formátumban). (szabad tér: 32,5 + 20 log f\ [MHz\] + 20 log d\ [km\]), 24000 és 0.1 plusz 40000 és 0.3 (gőz: 0,1 dB / km 24 GHz-től, 0,3 dB / km 40 GHz-től) Valószínű, mert megismétli, de belül a kódot: -69,5 (ugyanaz az állandó kifejezés, mint a Yeh oldalsó scatter képlet a VHF / UHF DX könyv), 332 (talajtöréses), 8733 km (egyenértékű sugara, k) 1.37). A pontos scatter formula nem rekonstruálható anélkül, hogy szétszerelné a programot. T0

JAPBER, a kóddal rekonstruált képletek (az összeszerelt rész szétszerelése; a műveletek a BASIC könyvtárba kerülnek a memóriában lévő kezelőkkel, akiknek az értékeit kettős pontossággal dekódolták). A -69,5, 332 és 8733 korábban talált állandók hamis pozitívumok voltak a könyvtárban.

- Optikai távolság: d = 5 · (1 + 2) km (h in m). Nagyvonalúbb, mint a klasszikus 4,12 (k = 4 / 3): egyenértékű a k q2-vel.
- Optikai veszteség: L = 32,5 + 20 log f\ [MHz\] + 20 log d\ [km\], plusz 0,1 dB / km 24 GHz felett és 0,3 dB / km 40 GHz felett.
- Troposcatter: Lts = L + 23 + 10 log f + 0,0674 · d, azaz 55.5 + 30 log f + 20 log d + 0,0674 · d ( G3YGF képlete 2 dB-rel magasabb állandó). Nincs szögkifejezés vagy reaktivitás.
- Helyesbítés "az oldalak": a program kéri akadály és távolság, de a blokk, hogy használja őket üres; a korrekciós változó marad 0, így a "helyes" veszteség egyenlő a scatter.
- Zaj: T = 290 · (10 ^ (NF / 10) − 1), N = -228,6 + 10 log T + 10 log B (dBW), antenna hőmérséklete nélkül.
- S / N = 10 log P + G\ _ tx − L + G\ _ rx -N. "Maximális abnormális terjedéssel" optikai veszteséget használ szóródás helyett, azaz az ideális vezeték nélküli veszteség a szabad téren túl.

Összehasonlítás: 300 km és 144 MHz JUVBER ad körülbelül 189,9 dB, G3YGF 187,9, a mi P.452 187,8 (nulla látószögben): a scatter egybeesik 2 dB. Számításunk a látószögeket a profilból, az antenna hőmérsékletét, és ahelyett, hogy a "maximum" rögzítette a várt réteg maradandó párzás és csillapítás.

Rohde & Schwarz, P. Denisowski, "Understanding VHF propagation", Educational Note, April 2023 (24 pages, distribution). Igazolja a már összegyűjtött pontokat (kétdimenziós hullámként végzett, a távolsággal egyenes irányban, akár 1500 km-re, akár tovább, óráktól napokig terjedő időtartammal, jó talajjal a tengeren és a szárazföldön; néhány száz méterről néhány km-re emelkedett). Két hasznos megfigyelés az indexhez: (1) az idő alakulása: az újonnan létrehozott vezeték gyakran csak a legmagasabb frekvenciákat hordozza, és megvastagodik, még a legalacsonyabbat is. Az óránkénti előrejelzésnek ezért a 144 MHz-en előre kell mutatnia a 432 MHz-et, amikor a réteg nő; (2) állomások elhelyezkedése: a legjobb körülmények a vezeték végéhez közeli állomások között vannak, míg a magas vezeték mentén lévők gyakran nem lépnek be, kivéve, ha "ki veszít". A csatornák légköri időjárással mozognak.

PA0EHG oldal, egyéb oldalak. A tropo, PathProfile of M. Willis G0MJW ([menetrend](https://pa0ehg.com/srtmp.htm), [letöltése](http://www.mike-willis.com/Software/ASTERPathprofile.zip)): profil az útvonal várható teljes veszteség és kvóta fájlokat, úgy ítéli meg, megbízható a troposcatter mikrohullámú; nem kezeli a scatter a repülőgépek vagy az eső. Hasznos, mint egy második hivatkozás összehasonlítani az S RTM1 profilok és veszteségek. Az oldal többi része EME, mikrohullámú sütő, reptér (AirScout, SM7LCB térképek) és intézkedések 80 m.

PathProfile by G0MJW (Mike Willis), ASTER and SRTM packages and SiteData (provided by the authorist of the program; [](https://www.mike-willis.com/software.html)software page]. Windows program 2018: profil ASTER vagy SRTM 1 "vagy 3", veszteség optika, diffrakció, troposcatter (érvényes akár 1000 km), csatorna és gáz, százalékos idő, k mediánja az útvonal, törtrésze út a tengeren és a távolság a tengerparttól: a szerkezet, hogy a P.452 / P.1812, de a segítséget (tömörített.chm fájl) nem lehet olvasni itt. A SiteData konvertálja a lokátorokat, és a részt a GT OPO30 -ból adja.

A leghasznosabb rész a következő adatfájlok: ITU- R P.453 statisztikai térképek a globális rácson 1,5 ° (121 sor 90 ° -tól 90 ° -ig, 241 oszlop 0 ° -ig): gradiens dN / dz az első 65 m-ben az idő 1, 10, 50, 90 és 99% -ában; talajcsatornák százalékos jelenléte, vastagsága és intenzitása; a magas jelenlét, bázisarány, vastagsága, intenzitása és maximális magassága; plusz a nedves kifejezés és hőmérséklet. Interpolált éves értékek:

| Hely | Földvezeték:% / vastagság / intenzitás | Nagy csővezeték:% / bázis / vastagság / intenzitás | dN / dz meghaladja az idő 10% -át |
| Nápoly |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | -380 N / km |
| Tirrén (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | -674 N / km |
| Trapani |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | -520 N / km |
| Brindisi |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | − 405 N / km |
| Adria (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | − 399 N / km |
| Milánó |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | -172 N / km |
| Udine |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | -176 N / km |

Az indexünk kész éghajlati referenciát jelent: a lehetséges csatorna 50 m-es rögzítése helyett a csatornákra jellemző vastagság és intenzitás, és egy anomália "ma a normálishoz képest" zónánként. ITU-adatok: ingyenes nem kereskedelmi használatra, hogy ellenőrizze, mielőtt azokat egy nyilvános változatban. A ZO NES01.DEM fájl (58 MB, 2 'rács) tartalmazza a bel- / partmenti / tengeri éghajlati övezeteket; a formátum nincs dokumentálva, és nem dekódoltam. Willis egyenletei csak fizikai állandókat, szabad teret és antennát tartalmaznak.

A források negyedik fordulója (november 20.).

Indermuehle és Suzuki 2026, a Murchison CSIRO obszervatórium GFS-csővezetékeinek előrejelzése (arXiv 2608.12861): ez a LIVETROPOVHF legközelebbi forrása. Az N profilok a GFS-től 0,25 ° -ig (felülettől 2 m-ig, majd az izobarikus szintig, a kidobott talaj alatt); minden 3 km alatti minimális dM / dz oszlopra, alapra és a vezeték tetejére, integrált M deficit; majd P.452-16 és P.1812- 6 GLO- 90 25 útvonalon 98 és 1005 km között, 88 MHz és 2,68 GHz között, óránként. Eredmények: 25 csatornán a medián AUC 0,72; a P.452 p időszázalékot kalibrációs állandóként kell használni (option p = 20%), mivel az instant ΔN-nel rendelkező ITU-modellt nem határozzák meg a vezetékeken belül; a jártasság 3 nappal előre majdnem lapos marad. A 162 MHz-es (AIS) útvonalon a legmagasabb csatornafrakció 67% (0,26 versus 0,16), és a legnagyobb csatornamélység 49% (28 vs 19 M) a vezérlések tekintetében: a tengeren a csatorna szinte mindig, dönt a földszakasz. A GFS a földön töltött idő felét látja az ERA5 -hoz képest (4,5% szemben a cellaórák 10,4% -ával), míg a tengeren többet (73% versus 56%). A "folyosó" index (folyamatos csővezetékek az útvonal mentén, legalább 40 m vastagságú rétegek) javítja az eredményt: megerősíti, hogy a folyamatosság az útvonal mentén a megfelelő kritérium.

Longley and Rice, ESSA ERL 79- ITS 67 (1968): módszer a 20 MHz-től 20 GHz-ig terjedő szabálytalan terepen (ITM után), talajszabálytalanságokkal Δh, N 250-től 400-ig, ezzel egyenértékű 8497 km (4 / 3), látótávolság, diffrakció, fröccsenés és variabilitás az idő és az éghajlat tekintetében. Statisztikai hivatkozás, nem azonnali.

ITU- R P.8342: sugara görbület, Föld sugara ekvivalens k · a, M = N + h / a (h in m, ezer km), exponenciális modell; semmi új, mint amit már használtak.

AGARD CP-70 (1970), tropoferikus terjedés: 1-100 m-es vékony rétegek, erős lejtőkkel; a magas réteg visszaverési együtthatója körülbelül arányos a hullámhosszal, és a véletlenszerű szóródás körülbelül 14 dB. Méretek 90 MHz-re: 9- 10 dB csökkenés egy körülbelül 350 m-es N ugráshoz kötve, egy felhőréteg tetején. A hideg front áthaladása megszünteti a rétegeket: A diffrakció megmarad.

U.S. URSI Report, Commission 2 (1960): átlagos vastagsága a 60-210 m magas rétegek (adott összhangban P.453), minimális gradiens a 157-160 N / km csatorna.

Buscemi K2OVS, VHF terjedési útmutató: jellegzetes távolságok a visszafordításhoz és csatorna a sávhoz, csatornák kapcsolódnak a nagy álló nyomáshoz, maximum hajnalban.

Wolinsky-Mancini és 2024, AIS a Kanári-szigeteken: AIS rádió árvíz és fogadások; a csatornák sokkal gyakoribb a meleg hónapokban (inverzió az alizeaiak).

Nigériai és malajziai cikkek (Ekah, Iloke, Mat, Igwe): helyi korreláció a hőmérséklet, a páratartalom és a mért veszteség között, gyenge és ellentmondásos (r -0,09-0,27); csak annak megerősítéseként hasznos, hogy a földméretek önmagában nem elegendőek.

Herben 1984, thesis TU Eindhoven (microonde): 30 GHz 8,2 km-en és OTS műhold 11,6 GHz-en; szikra turbulencia, többpályás és eső. A mikrohullámok dinamikus veszteségeit az eső és a turbulencia uralja, nem a csatornák.

Baars, The Paraboloid Reflector Antenna (microonde): egy példabeszéd G = η · (πD / λ) 2, tipikus hatásfokkal 0.5-0,65; a felületi hibák elvesztése Ruze szerint, ηs = exp (-( 4πε / λ) 2). Segítene, ha a példázatokat a Yagi DL6WU alternatívájaként hozzáadnánk 1296 MHz-hez és azon túl.

Már elemezték, vagy gyenge tartalom: Hirschmann NASA X-731-67-89 (dupla), Reid VHF ST radar (szél és turbulencia, kezeletlen), Scribd dia és generikus jegyzetek a troposzférán.

A források ötödik fordulója (november 20., este).

Rogers 1997, Radio Science 32 (1): Bayesian surveillance of the paraméters of a high dray (base rate, M deficit, vastagság) from propagation factors measured above the horizont, with a parabolic equal model; VOCAR experience in California at 143, 263 and 375 MHz, comparison to radio wave. A jel nagyban függ az alaptól és az M-deficittől (0,81-143 MHz a deficitet korrigálva), szinte egyáltalán a vastagságtól (-0,17): a vastagság nem érhető el a jelből. Számunkra: a B (éghajlati vastagság) számítás kevesebb, mint a hiány és a részarány; és a módszer az alapja a hátsó iroda, azaz, hogy a vezetéket a többfrekvenciás.

Moler and Holden 1960, "Tropospheric scatter propagation and atmoszféra circations", J. Res NBS 64D, 81. o. California tengeri útjai Point Loma (San Diego): Santa Barbara 190 mérföld, Point Mugu 144, San Clemente 78; 1365 MHz és 9380 MHz, nem VHF; január -április 1958, átlagosan 1 perc. Március 26-i eset: körülbelül 20 dB megerősödése körülbelül 3 órán át dél felé, amikor a ciklon örvény gyengül, és megkezdődik a tengeri szellő és a süllyedés; 15 évesen a konvergencia megreformálódik, és a jel a maximumtól körülbelül 22 dB-rel csökken. Statisztikák (1. táblázat, 500 hPa, 33 eset): az L és X sávban az átlagos jel 2,5 dB-nél magasabb. A jel a változások jelében és amplitúdójában 500 hPa-ra kiszámított különbséget követi, korrelációs együttható nélkül. Az alacsony rétegek (kb. 600 m alatt) kedveznek a szóródás részleges visszaverődésének. Az anticiklonok és frontok nem kezelhetők, és a fent említett "kirándulások legfeljebb 60 dB" nem: másodkézből származnak. Számunkra: a függőleges mozgás egy jó előrejelző, és + 1,5 dB a süllyedés és -2 dB a felfelé irányuló levegő azonos sorrendben az átlagos mért különbség (2,5 dB); 20 db egy eset, nem egy tipikus érték, és a mikrohullámú sütő.

Banafaa és Muqaibel 2025, IEEE Access: felmérés a csővezetékek (párolgási modellek, parabolikus egyenlet, radar becslések, GNSS és lidar) és besorolás automatikus tanulás a valós intézkedések; Támogató vektor gépek adja a legjobb eredményeket. Az adatok a Perzsa-öbölből származnak.

ITU- R P.834- 7 (2015): a troposzferális fénytörésre vonatkozó ajánlás aktualizált változata; a már használtaknál nincs új képlet.

Abbott, NBS bibliográfia a troposzferikus szóródásról (GOVPUB- C13): már a harmadik körben. Slide Scribd a csatornákon: generikus tartalom. A cikk ScienceDirect 0021916987900304 nem lehetett kinyitni.

6. kör (november 20., éjszaka): Bean és Dutton, ARRL Kézikönyv 2022 és 2023, Balanis.

Bean és Dutton, Radio Meteorology (sapka. 6), alkatrészek még nem használt. N-re való mezei érzékenység: 0,18 dB N egységenként az éves ciklusban; 0,14- 0,24 dB / N a nappali ciklusban (legfeljebb reggel), legfeljebb 0,2- 1,1 dB / N a téli 175- 200 km útvonalon; korrekciók 0,8- 0,95. Az M = a (g\ _ e -40 + b · ΔW ^) Misme-ből álló paraméter = 0,5 dB / N / km-rel, amely az egyenértékű gradiens és a ΔW (1 km-es levegőemelés) hőstabilitását ötvözi: a Szaharában 471 MHz-en 160 km-en az egyetlen stabilitás 20 dB január és június között majdnem azonos gradiens. potenciális K törésmutató (N): kiemeli a légtömegeket és az anticiklon szubsidenciát (Flavell és Lane).

ARRL kézikönyv 2022 (99a ed.), § 19.4.3-19.4.4. Hasznos inverziók 250 és 2000 m között; inverziók sugárzás (szérumok, nyugodt levegő, száraz talaj) ritkán vezet igaz; lassú anticiklonok adja a legkiterjedtebb csatornák egy alámerülés, inverziók között 500 és 3000 m, erősebb este és reggel; az északi féltekén a legjobb útvonalak délre a magas központ. Melegfront: jó feltételek néhány száz km-rel előttünk; Hideg front: erős, de instabil háttal az első, párhuzamos. Foehn (szintén az Alpokban) és a földi szél (hideg levegő a földön, meleg 200-300 m; nyúlik több mint 800 km-re a part mentén). Scirocco: forró és száraz levegő Szaharától a hűvösebb tengeri levegő felett, nagyon erős inverziók a Földközi-tenger keleti részétől Gibraltárig, valószínű oka a rekordok több mint 1500 km mikrohullámú sütő. Troposcatter: elméleti limit körülbelül 800 km, maximum tipikus fele. A 100th ed. (2023) egy rövidebb szöveg: inverziók napkeltekor és naplemente legfeljebb + 20 dB és + 20-50% áramlási sebesség; 1200 mérföld a Nagy-tavak, tipikus 400- 600; ajánlja a Rádió Mobile (ITM modell) Hepburn lefedettség és térképek.

Balanis, Antenna Theory (4a ed.), § 10.3.3: Viezbike asztal (NBS TN 688, 1976) a Yagi 400 MHz-en optimalizált számára: boom 0,4 / 0,8 / 1.2 / 2.2 / 3.2 / 4.2 λ → 7.1 / 9,2 / 12,25 / 13,4 / 14,2 dBd. DL6WU képletünk (8.6 + 8 log10 boom / λ) 11,7 dB-vel kisebb értéket ad 0,8 és 4,2 λ között, és az DL6WU táblázatoknak megfelelően 9,5-re tér vissza. Az eredeti képlettel (9,4 + 7,8 log10 boom / λ, november 22.) a visszadobás 0,2 dB alá csökken 1,2 és 4,2 λ (12,1 dBd-2,2 λ versus 12,25) között, és 0,6 dB marad 0,8 λ.

Hetedik kör (november 21.): QST, QEX, W3SZ, GitHub tároló.

QST 2000-2025 (14 szám, 10 ágy szövegfelismeréssel). Csak a The World Above 50 MHz-ről szóló jelentések, amelyek a modell validálásához szükségesek: 2000. szeptember 5- 9. az Egyesült Államok keleti felén 144 MHz-en és azon túl; 2022. augusztus 14- 15., a Manitoba Kansas / Missouri nagy nyomás alatt egy forró napon, 144 - 1296 MHz-en szinte minden FT8 -ban, a kanadai rekord X328,56 km ( VE4MA - N0OY) 10 GHz-en, a jelek 1941 km-ig; az APRS propagációs térkép ugyanezt a területet mutatta. QST November 2022 ajánlott Hepburn térképek. Nincs technikai cikk a tropo előrejelzésről. Q65

QEX és K1JT, Franke és Taylor (QEX 2020) FT4/FT8 üzemmódokban: FT8 dekódolási küszöbértékek -20,8 dB 2500 Hz-ben a csatornán evaneszcencia nélkül, -18,6 dB evaneszcenciával közepes latitúdoknál; FT4 -17,5 dB. A QEX 2023 és 2026 többi száma nem kezeli a tropót (három szám 2023 olvasatlan). W3SZ 2019: lassú üzemmódbeli küszöbértékek ( JT4A -23, JT9A -27, JT65A -25, QRA64A -26, FT8 -21 dB) és választandó kritériumok az érzékenység és a frekvencia-eltolódási tolerancia között.

GitHub. TropoLink (LukiBox, Apache- 2.0): troposcatter útvonalak tervezése P.617- 5 ( N0 és ΔN), NBS TN101 és ITM flanked, és ezek különbsége bizonytalanságként mutatva ki; P.617- 5: L = F + 22 log f + 35 log θ + 17 log d + Lc, F = 0,18 · N0 · e ^ (- hs / 7,35) − 0,23 · ΔN, Lc = 0,07 · e (0,055 (Gt + II). andi- f / troposcatter ( DL2FZN 2013, a VHF / UHF kézikönyvből RSGB): Lts = 24,44 + 4,283 Ln f + 0,06656 d a szabad tér felett, lejtő JUPBER közelében (0,0674 dB / km). QRadioPredict: ITM és ITWOM 3.0 S RTM3 -val, talajhasználat Corins és NEC diagramokkal. Nem releváns: Tropos (Időjárás-alkalmazás), opera-adt / tropo és troposom (Troposferic delay for InSAR), tgbot\ _ troposcatter (katonai állomás), sdr- troposcatter- gui (vezérlőpanel), Troposcatter- propagation- Delay ( ERA5 késés), Radio-Communication -Project (didaktikus szimulátor), PyLayers (csatorna).

Nyolcadik kör (november 21.): NTIA és ITU modellek a GitHub-on. NTIA / itm (Longley- Rice, ITS): 20 MHz- 20 GHz, szabad tér veszteség, diffrakció és troposzcatter talajprofillal, 0.5- 3000 m terminálok, felületi reaktivitás N0 (minimális tengerszint, 250- 400), hét éghajlat, talajállandók és különböző módok 1 nézet, 2 diffrakció, 3 troposzcatter; C + + a.NET keretszabály 4.7.2 rekesz. Nem modellezi a csatornákat: statisztikai változékonyságú referencia "normál atmoszféraként" szolgál. NTIA / p528 és eeveetza / p528 (MATLAB): ITU- R P.528- 5 légtalajcsatlakozások, 100 MHz- 30 GHz, 1,5- 20 000 m terminálok, p 1- 99%;.NET 4.8 burkolat. Lehetséges használat: a levegő szórási egyensúlya (két földi út- repülőgép plusz radar szakasz). eeveetza / p2001 (MATLAB): ITU- R P.2001- 6, all- field model diffrakció, troposcatter, rendellenes szaporítás (viselkedés és rétegek, Lba) és E sporadic, az idő 0-100% -a; ITU digitális térképek (DN\ _ Media, DN\ _ Supslope, DN\ _ SubSlope, dndz\ _ 01, TropoClim, h0, surfwv, Es) nem helyettesíthetők. Lehetséges használat ITU összehasonlítás helyett P.452, a GFS ΔN helyett térképek, mint a CSIRO tanulmány; A térképeket kell letölteni a felhasználó a P.453. Validation 1.3.0 out of 593 QSO (29 / 12 / 20212 / 1 / 2022): event / comparation separation 0,79 without corrections, 0,91 with Misme and subsidence; the GFS don 't see the channel on the sea 144 MHz (guadagno without corrections never more 15 dB).

9. kör (november 21.): IU3QEZ, NTIA, PyWaveProp, ACE High, validálás 1.3.1. IU3QEZ (S. Fabris), prezentáció "A horizonton a VHF" (GEKO Magazine n. 69, szeptember 2026): közös térfogat d2 / (8 · R\ _ eff) 8500 km (1,3 km-től 300 km-ig, 3.7 km-től 500 km-ig, minden egyes szintnél 69 mérföld (111 km), szélességi határ 7 dB és 4 dB talajtükrözés a talajtükrözés a talaji visszaverődés esetében 10 dB körülbelül 100 km; X5 W (8 · R / (8 · R\ _ eff), maximum 5 km). A visszanyerendő prezentáció bibliográfiája: Norton 1959 (J. Res NBS 63D, System Loss), Bray 1961, ARRL Antenna Book 2023 fig. 4.29, Rice- Longley- Norton- Barsis NBS TN 101, Bean and Thayer 1959, Barsis and Kirby 1961 (diffration on ridge), Deygout 1966, P.1812, AirScout DL2ALF, P.834 NTIA PropLib (wiki): eHata, ITM, LFMF, P.528, P.2108 (loss from Encumance), + MATC, + MATT. PyWaveProp (Python): Parabolikus egyenlet módszerek Padé közelítések, többszörös széldiffrakció, PETOOL és RAM tartási területek, troposzferózus és magas csatornák, szabálytalan talaj: lehetséges bázis egy mező számítás a GFS M (z profil, ellenőrizendő. APRS PropView (Apache 2.0): VHF terjedési térképek APRS csomagokból, óradíjas statisztikákból és nyitóriasztókból; lehetséges 144 MHz-es adatforrás validáláshoz. NATO troposcatter hálózat Olaszországban (fortifications.net): 900 MHz, 10 kW, 20 m-es példák; Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Vulture, Monte Lauro; a P.617- re vonatkozó történelmi vizsgálati eset. Nem releváns: Gupta 2021 (sztratoszférikus szállítás), 2023 (sarkvidéki ózon), delfin # 590 és opera- adt / tropo (inSAR késés), tropo.ino (Arduino gázérzékelők), tropo- webapi (telefónia), felhőszerszámok / troposzféra (AWS). Validation 1.3.1 on 564 QSO tropo: AUC 0,79 (1.2. minta), 0,80 (+ soft channel), 0,90 (+ misme and subsidence), 0,90 (+ perzisztencia); vezetékszínek (≥ 16 dB) a nyitási útvonalak 46% -án és az összehasonlítás 0% -án. IQ3QC

Ugyanaz a kör. mikelytaev / wave- propagáció (MIT, Python 3): Parabolikus egyenlet a Padé és a Numerov közelítésével, szabálytalan talaj, troposzferikus csatornák; töltsük fel a módosított refraktív M (kvóta, lat, lon) a NOAA GFS és állítsa be a párolgási csatorna Monin- Obuhov; nincs dokumentált funkció, amely visszaadja a veszteséget az útvonalon. Ő a legjobb jelölt egy jövőbeli mező számítás a GFS M profil (szolgálna külső szolgáltatás vagy fordítás). RP- Sim (AGPL- 3.0, JavaScript): Empirikus modellek rádióhálózatokhoz (Hata, He, Ericsson, két sugár), nincs tropo. RIMFading (GPL- 3.0, OMNET + +): áramlási szabálytalanságok hálózati szimulációkhoz, Weibull eloszlás, nincs tropo. PyLayers: beltéri rádiócsatorna, már kizárva. qradiopredict (kantooon, GPL v2): ITM és ITWOM v3.0 S RTM3 és Corine-nal, már a hetedik körben QRadioPredict.

10. forduló (november 21.): Antenna Book, Norton, Barsis, új adatforrások. ARRL Antenna Book (23a ed.), cap. 4 ( K9LA): Bray method (QST november 1961) for the VHF range: small Earth loss to 99% (300 mérföld, 144 MHz: 214 dB), curres to 50% and 99% (fig. 4.11): 195 dB worth 100 mérföld 99% és több mint 250 mérföld és 50 mérföld per 100 mérföld (-3,5 dB-50 mérföld); Evanescence ± 7 dB per 100 mérföld (-3,5 dB), SSB 3 dB rosszabb, mint a CW, + 4 dB földi reflexió + 3 dB két antenna csatlakozásánál; height gain (15 m / 3 / 2 dB 10 / 100 m 10 / 4 dB; 24 m; 8 / 4 dB / dB) times; YDB) of the 10 mérföld (10 mérföld); YDW) of the 10 dB (i) of DW) of DW; Naplemente és naplemente legfeljebb + 20 dB és + 20- 50% áramlási sebesség; meleg és száraz levegő nedves friss levegőn a Nagy-tavak akár 1200 mérföld, tipikus 400- 600; régiók befolyásolja a nagy víztömegek a leginkább kedvelt; késő télen fűtés képes erős inverziók. Troposzcatterünk ellenőrzése: P.452-50% a 483 km-en és 144 MHz-en 204 dB-t ad, ami megfelel a 214 dB Bray 99% -nak. Norton 1959 (J. Res NBS 63D, ingyenes NIST): alapvető veszteség, rendszer és útvonal antenna nyereség; Reference atmoszféra Ns = 301; egyenértékű sugár Ns-től (250 → 4878 mérföld, 301 → 5280, 400 → 6997); L (99) asztalok 30 láb antennával (100 MHz: 163 dB-től 100 mérföld-ig, 208 dB-től 500 mérföld-ig; 1000 MHz: 138 és 188 dB 28 lábas példákkal); hosszú távú variabilitás V (p, θ) a szögtávolságtól függően (13. ábra). Rice, Longley és Norton, NBS TN 15 (1959), ingyenes NIST. Barsis és Kirby, NBS Report 6751 (1961): 223 km Colorado Pikes Peak (4292 m) éle, 100 és 751 MHz; konzisztens veszteség a lekerekített éllel (körülbelül 11,5 km sugarú); hosszú távú Evanescence 11,7-14,3 dB versus 39-42 dB egy scatter azonos hosszúságú; nincs napciklus jelölve; a négysugaras modell 4- 5 lebeny, de csak egyet mértek (legfeljebb körülbelül 21 m a talajtól): a Hegyi akadályral rendelkező utak sokkal stabilabbak, mint a "via Vesuvius vagy Faito". ARRL VHF Digital Handbook: csomag, APRS, D- STAR, WSJT (Scatter Method and EME), nincs része a tropo. Új bizonyított vagy kutatott adatforrások: ECMWF nyílt adatok (IFS 0,25 °, szint 1000, 925, 850, 700 hPa..., 975, 950, 900 és 800; G RIB2 CCSDS tömörítéssel, hogy az olvasó nem dekódolja; már elérhető Open- Meteo); DWD ICON- EU nyílt adatok (G RIB2 sűrített bz2, egy fájl változó és szint: sok kérés); Canada GDPS MSC Datamart és GeoMet (körülbelül 15 km, legfeljebb 33 szint; Geomet lehetővé teszi a termés a terület; ez a Hepburn); DMET (GAPR).

Tizenegyedik kör (november 21.): Longley- Rice, GEKO 69. Bean és Thayer, "Models of the Atmospheric Radio Refractive Index", Proc. IRE May 1959 (pp. 740 et seq.): a 4 / 3-as modell jobb lejtéssel rendelkezik az első km-ben, de túl magasabbra esik; a sugár görbületének 30% -a az első 100 m-ben és 60% -a az első km-ben történik; az átlagos összefüggés a Ns és csökkenés az első km: − ΔN = 7,32 · e ^ (0,005577 · N) (N = 313 → ΔN körülbelül -42). Bean and Thayer, "C.R.P.L. Exponential Reference Atmosphere", J. Res NBS 63D n. 3 (1959): N (h) = Ns · e ^ (-c · h) with c fixed by Ns through the same report; Average Ns USA 313; gerenda követő táblák (teljes görbe 15,2 mrad with exponential model versus 30.3 mrad with Earth 4 / 3). MATLAB refrakciós exp: c = 0,143859 km − 1 Ns = 313 (6,95 km-es skálán), amely 1 km és 10 mrad felett érvényes. Használata számunkra: a "normál" összehasonlítási profil lehet az a CRPL a helyi N-ekkel M = 315 + 0,118 · z helyett, és az anomália a csökkenés az első km kell mérni -7,32 · e ^ (0,005577 · N) helyett -40 fix. NTIA Report 82-100 (Hufford, Longley, Kissick 1982), ITM guide in area mode: climi and N 's suggested (equatorial 360, szubtrópusi kontinentális 320, szubtrópusi tengeri 370, desert 280, temperature continental 301, temperature marine 320, temperature sea 350); Ns = N0 · e ^ (-hs / 9,46 km); you use the monthly minimum of Ns because less by super- refraction and sub- refraction; Ns = 8497 km; under 100 km Ns little nums. Rice, Longley és Norton, NBS TN 15 (1959), I. rész: A szárazföldi és troposzferikus hullámok időbeli eloszlásának előrejelzése, TN 101 bázisa. SoftWright, Wikipedia, Altair WinProp és MATLAB rfprop. LongleyRice: ugyanaz az információ (1.2.2., 20. MHz- 20 GHz vagy 40 GHz, 1- 2000 km, Ns 301, hét éghajlat, az idő, a hely és a helyzet változékonysága); senki sem formálja a csatornákat, csak a tengeri éghajlatok statisztikai hatását ("nagyon magasabb mezők az idő 10% -ában vagy nem"). GEKO Magazine n. 69 ( IU3QEZ): Radio horizont 4 / 3 of 130 km from 1000 m, 260 km add két állomás; troposcatter with common volume between 1103 km and 10 km; scatter from air with 10 km with 10 km with + 10- 20 dB for 10 seconds or néhány perc; elméleti tanfolyamok 2 × 10 elemek per side: SSB QRP about 440 km, SSB 50 W about 510 km, CW QRP about 600 km, SSB 500 W about 640 km (500 W in SSB make 40 km more 5 W in CW);

## 12. Kerekasztal: a modell validálásához vagy javításához szükséges források (november 21.)

Az összes forrás összegzése, felhasználásra rendelve. "State" azt mondja, ha a forrás már a programban (1.3.1.), hogy a teszt, vagy meg kell csinálni.

### Igazolandó adatok (mi történt valójában)

| Forrás | Mi ad | Felhasználás | Állam |
| MMMonVHF jelentések a szerzőtől | 593 QSO 144 MHz, 29 / 12 / 20212 / 1 / 2022, idő és helymeghatározó | Az esemény összehasonlítása (+ 14 nap) | használatban: AUC 0,79 (1,2) → 0,90 (1,3.1) |
| A világ 50 MHz felett | dokumentált esetek (14- 15 / 8 / 2022 Manitoba- Kansas, 144- 10 GHz) | korábbi eset | korábbi esetként használják; az érvényesítési QSO-khoz hozzáadandó |
| Wspr.live (WSPR) | teljes archívum, 2 m és 70 cm, SQL | sok esemény és sok "nem" (órákban nincs hely) | kipróbálni, meg kell szűrni (folt hamis 16-20 ezer km) |
| APRS PropView / APRS- IS | Meghatározott állomások, távolság, óra | kizárólag: érvényesítés 144 MHz | Tenni |
| Fordított Beacon (archívum) | napi | VHF | Egyelőre kidobva |
| I GRA2 radiosondaggi (Brindisi 16320, Practice of Mare 16245, Trapani 16429, Udine, Milánó) | a 00 és 12 UTC mért profilok | GFS profilvezérlés és inverziók | talált fájlok (Brindisi 46 MB), felhasználandó |
| Barsis és Kirby 1961, Barsis 1963 | Hegyi akadályral megtett ösvényeken mért evaneszcencia | a "monte" kategória validálása (Vesuvio, Faito) | Tenni |
| Norton 1959, NBS TN 15, TN 101 | L (99) és az asztali média veszteségei | Szórásvezérlés | részben (lásd 300 mérföld következetes) |

### Források által javasolt korrekciók

| Korrekció | Forrás | A validáláskor mért hatás | Állam |
| Hibás stabilitás kifejezés | Bean és Dutton sapka. 6 | AUC 0,79 → 0,90 a süllyedéssel együtt | 1.3.1. pont (16 dB cím csatorna nélkül) |
| A függőleges mozgástól való süllyedés | Moler és Holden 1960 (olvasva az eredeti 22 / 11: + 2,5 dB átlag között jön le és a levegő emelkedik, 1.4 és 9,4 GHz a tenger) | a fentiek szerint | pont (+ 1,5 / -2 dB) |
| Puha csővezeték (λ legfeljebb 3 · λmax) | Kerr, Hepburn, VHF / UHF DX Book, Shi 2019 | önmagában + az AUC 1% -a; a csatorna színét az esemény útvonalainak 46% -ára és az összehasonlítás 0% -ára hozza | in |
| Perzisztencia 24 h | VHF / UHF DX könyv, Biancomano és Leyshon | AUC változatlan, több bevett és több téves riasztás | pont |
| Normál CRPL profil helyi N-ekkel (-ΔN = 7,32 · e ^ (0,005577 · Ns) 40 N / km rögzített helyett | Bean és Thayer 1959, NBS Monograph 4 | validálási vizsgálat | az 1.3.2. pont helyébe a következő szöveg lép: |
| Szupertörési küszöb 79 N / km (ITU) 100 helyett (Bean és Dutton, 1000- 850 réteg) | P.453, Lopez 2008, DX Book | validálási vizsgálat | 1.3.2-ben: agresszív becslés a prudens mellett (a térkép körültekintő) |
| A klimatológiai vastagság (B számtan) kevésbé fontos, mint a hiány | Rogers 1997 | próbálja meg: használja A a nyereség | Tenni |
| Parabolikus egyenlet a GFS profilban | PETOOL, wave- propagation (Lytaev), Levy (könyv nem olvasható, csak előnézet), Lindquist | első változat a látóhatár feletti mezővel, amelyet a számszerű zaj jellemez (AUC 0,12- 0,28): helyes (8 km-es tartomány, 6 ° -os sugár); az AUC 0,54- 0,66 korrekciót követően | második számításként nem határozza meg az előrejelzést |
| Evaneszcencia: ± 7 dB 100 mérföld felett, 10- 20 dB és 90- 99% között | Bray 1961, Antenna Book, GEKO 69 | a határfeltételek szerinti utak küszöbértékei | használat közben: az FT8 -18,6 dB-es értékhatár és időtartam valószínűsége troposzcatterben |
| A Bray közös térfogata és ezzel egyenértékű távolsága | IU3QEZ, Bray 1961 | Tájékoztatás | in |
| Troposcatter P.617 és RSGB mint bizonytalanság | TropoLink, DL2FZN | Tájékoztatás | 1, 3 |

### Mi marad ki modellek

Alacsony magasságon (100- 200 m) a tengeren azt a következtetést vonták le, hogy a GFS-szint nem látható (a jelenlegi adatforrások tanulmányozása); a közeli hegyeken (Vesuvio, Faito) tükröződés és diffrakció: a "monte-on keresztül" történő számítás javaslata. ITM / Longley- Rice továbbra is statisztikai referencia "normál nap" (Ns 301, climi), vezetékek nélkül. A szerző döntése szerint: AirScout, P.528), E sporadic (P.2001) és 50 MHz (servoir EIRP, amely kevesen vannak, és összetéveszti az E sporadikával).

### Javaslat munkamegbízásra

1. Kiterjesztett hitelesítés: a szűrt WSPR-t (még a folt nélküli órákat is) és a QST 2022-et kell hozzáadni; a 432 MHz-es frekvenciára ismételjük meg.
2. Próbálja ki a normál CRPL profilt és a 79 N / km-es ITU küszöbértéket, egyszerre csak azt, ami az AUC-t emeli.
3. Összehasonlítása a GFS profilok az I GRA2 rádióhullám Brindisi és gyakorlat a tenger a QSO napjaiban.
4. Számszerű korrekció után a parabolikus egyenletet újra kell értékelni.

### A november 21-i határozatok (1.3.2. változat)

- Elfogadható: körültekintő becslés (Bean és Dutton, 100 N / km) és agresszív (ITU, 79 N / km), csak 2500 m alatt keresett csatornák (első 1500 m, a hitelesítés után felemelve); magas nyomással és gyenge széllel járó bónusz; P.452 μ1-es tengeri tömeg; az állomások részvezetékhez viszonyított aránya; határréteg ( IT9GSF).
- Elfogadva, mint egy alternatív, kikapcsolta hiba: normál CRPL profil helyi N.
- A helyes parabolikus egyenlet számszerűen: AUC 0,12-től 0,54-ig (30 m antenna) és 0,28-tól 0,66-ig (300 m).
- Land: Copernicus DEM GLO- 30 Terrain csempe helyett "skadi", amely a part közelében van szellem mentések 80-100 m a tengeren (jelzés IC8TEM, ellenőrzött pont pontonként Capri); skadi marad tartalékként.
- Időjárási jelek a térképen hatásukkal: ↓ kedvező süllyedés (Moler és Holden 1960, átlagosan körülbelül 2,5 dB között levegő megy le és levegő megy felfelé; + 1,5 dB), ↑ levegő felfelé kedvezőtlen (attól függően, mély; − 2 dB), S scirocco kedvező nyom (ARRL kézikönyv § 19.4, inversions nagyon erős a Földközi-tenger; nincs korrekció). Dupla szimbólum, zöld / piros / narancssárga színek és képregény a térképen.
- Elfogadta az ITM 1.2.2 (NTIA / itm, nyilvános domain), lefordítva C #: szerint számítása az alapvonal normál légkörben mellett P.452 és az utolsó szakasz után a kilépés egy vezeték, amely véget ér előtt a levelező. Kiürített qradiopredicode kód, mert a GPL v2 alatt.

Az index hasznos tartalma nélkül: * Communications Quarterly *, Auster 1991 (no article on the tropo; Atkins 's article on the Scatter is in Winter 1991).

- Elfogadva, mint a kézikönyvek alapelve (november 21.): "a szaporítást nem lehet általánosítani, hanem a helyi morfológiával kapcsolatban kell tanulmányozni" ( DF5AI, az IK3XTV jelentése a "2 méteren történő szaporítás" című dokumentumban); idézve az előzetes kézikönyvben (1. §) és a tanácsokban.
- Elfogadva (november 21, 1.3.3. után) az IK3XTV transzalpine nyílások értelmezése: nem "konvektív sejtek, mint a lencsék", hanem a troposcatter, amit a turbulencia a réteg tetejére reacted, ha eléri a közös térfogat részét (kézikönyv az előrejelzések, 8 ter). 1.3.4-től a pontszámítást a troposzcatter ideiglenes megerősítéseként éri el, legfeljebb 3 dB. Ugyanazok a cikkek zárják le: az Alpokon túli jelek "auto-erősítése" (a passzív közeg nem erősíti meg: a leírt linkek továbbra is megmagyarázhatók a diffrakciós és troposzcatterrel; a megadott erőket nem használják a megadott módon), a hó, amely "javítja a diffrakciót" (legvalószínűbb, hogy a hó feletti stabil réteget, kevesebb evaneszcenciával).

- Kerr (1951) felülvizsgálta a könyvet (november 22, PDF által biztosított a szerző): § 1.5, eq. 17 és 18 és táblázat 1.1. Az eq. 18 számadatok olvashatatlanok, de az 1.1 táblázat megerősíti: 20 ft → 1 cm, 80 ft → 10 cm, 400 ft → 100 cm, 2000 ft → 1000 cm, szemben 1,25, 10, 112 és 1252 cm az λmax = 0,014 · d ^ 1.5 (λ cm-ben, d állva; a kerekasztal, "indikatív értékek". Kerr hipotézisei: a vezetéken belül a módosított index-leesés 4 · 10 − 8 láb, jellemző értéke az alacsony csatornák; gyengébb gradiensekkel kell nagyobb vastagságot. Kerr azt is figyelmeztet, hogy az λmax nem egy nettó vágás: a hullámhossz néha még mindig erősen befolyásolja a csatorna.

- DL6WU felülvizsgálta az eredeti (November 22, PDF of VHF Communications 3 / 1982 által biztosított a szerző): G. Hoch, "Extremely Long Yagi Antennas", pp. 130 and follow. 1. ábra: a 432-es és 1296 MHz-es dipole-on a német posták hivatásos poligonjában elért nyereség 7,7 dBd értékű EIA-mintás antennával, becsült pontosság 0,5 dB; 2,35 dB a bumm duplájára, telítettség legfeljebb 20 λ. A program 8,6 + 8.0.log10 (L / λ) dBd, azaz körülbelül 0,8 dB kevesebb, mint 1102 λ-ben; most 9,4 + 7,8 · log10 (L / λ) dBd értéket használ (a gradiens a megadott érték, az 1 λ értéket a diagramon ± 0,3 dB). A 10 · log10 (7,8 · L / λ) képletet leválasztották a hálóról: a megduplázáshoz 3 dB lenne, szemben a 2,35-tel.
- Booker és Gordon (1950): hivatkozás a "This Week 's Citation Classic" által megerősítve (Jelenlegi tartalom, 1982. február 8., kommentár: H. G. Booker): Proc. IRE 38, 401- 412. A megjegyzés megerősíti a kézikönyvekben használt két pontot: a Rayleigh eloszlással a horizonton túl kapott mező, valamint a szabálytalanságok exponenciális autokorrelációs funkciója (non- Gaussian). Az eredeti cikket nem olvasták el.

## 13. IK3XTV cikkekből

A két cikk Flavio Egano IK3XTV (Radio Rivista: "Szaporítás 2 méterre" és "Szaporítás VHF - konvektív sejtek és gravitációs hullámok") hozzáad két dolog, hogy a csatorna modellek nem fedi: a transzalpine nyílások a délutáni miatt konvekció, és gyakorlati szabályok, hogy hol és mikor a csővezetékek jönnek létre Olaszországban. Ezek az állomás megfigyelései, nem az intézkedések: ezeket hipotézisként kell kezelni, hogy ellenőrizzék a pontokat.

| A szerző észrevételei | Hogyan válik az index összetevőjévé |
| A csatornák főleg 100 és 1000 m között alakulnak ki, ritkán magasabbak; nagy nyomást gyakorolnak egy hatalmas területre, jó arcátlanság és a szél hiánya | keresés a csatorna csak kevesebb, mint 1500 m; bónusz nagy nyújtott nyomással és gyenge szél az alacsony rétegekben |
| Az Adrián Veneto és Puglia között visszatérő meleg hónapokban 700- 800 km, néha Görögországig; a tengeri útvonalak a kedvencek | nagyobb tömeg a tengeri útvonal töredékéig (mint például az ITU- R P.452 μ1) |
| A megfelelő kell lennie, vagy közel a vezeték; magas antennák és néhány méter vezet a jel nem lép be | hasonlítsa össze a csatorna alapjának arányát a két állomás részével (a QTH magasságát fogja szolgálni) |
| Ugyanabban az időben, ugyanaz a csatorna: hallgasd meg a Gargano egy állomás 144 és egy 50 MHz | hogy ellenőrizze: a Hepburn asztal szerint az 50 MHz több mint 300 m ólmot kér, így az esemény nagyon gyakran jelezne egy csővezetéket |
| Kapcsolatok Veneto - Dél-Németország az Alpokon túl, látás nélkül, szinte mindig délután és nagy nyomással; instabil jelek, amelyek eltűnnek és újra megjelennek néhány perc múlva | "konvektív" index a vezetéktől elválasztva, hegyvidéki útvonalak esetében |
| A konvektív sejtek a földön születnek és 3 óránként 1 km-t emelkednek: 15-nél elérik a 3 km-t, a magasságot, ami az Alpok felülírására szolgál. | az útvonal mentén használja a limit layer magasságát (határ-\ _ layer\ _ height of Open- Meteo GFS): pontszám, ha meghaladja a gerincek magasságát |
| Az Alpok gravitációs hullámokat generál, amelyek konvektív mozgásokkal együtt a törésmutató szabálytalanságait idézik elő. | hipotézisek: erős szél merőleges a lánc 700 hPa jel arany hullámok; hogy próbálja meg az utolsó |
| Nagyon magas inverzió (akár 8 km kivételes esetekben) ad mintegy 800 km, de a gyakorlatban ritkán több, mint 300 km | az inverzió aránya kiterjeszti a hatókört: hasznos adatok a várakozási távolság becsléséhez |
| Nyáron a Föld sugara körülbelül 16000 km (2,5-szer a valódi), körülbelül 60 dB kevesebb mint 300 km | megfelel k50 = 157 / (157 − ΔN): k = 2,5 megfelel a ΔN körülbelül 94 N / km, azaz szuperrefrakció |
| Több mint 3000 km csak a 20- 30 ° -os tartományban az Egyenlítőtől északra és délre, a tengerek felett, a Hadley sejtekhez kapcsolódva; a csúcs augusztusban Afrika-Európában | Európán kívül: hasznos, hogy nem ad magas mutatók az Atlanti-óceánon visszajelzés nélkül, és a Kanári útvonal - Nagy-Britannia |

Ugyanabból a cikkből származó egyéb hasznos adatok:

- Optikai tartomány két h1 és h2 méter között (d km-ben): d = 4,1 · (kilogrammban), legfeljebb 20% a normál fénytörésnél.
- Evaneszcencia: lassú és sekély, inverziós, erős és gyors, szétszórt és konvektív sejtekkel. A program felhasználhatja hirdetések osztályozására, ha egy nap lesz SNR jelentések idővel.
- A 144 MHz-es szórványos E-nél a szerző az DF5AI, Davis és Johnson (Nature 435, 2005) munkáját jelenti az ideiglenes és az Es: out of the tropo, de egy másik mutató holnap is használható villámadatok.
- Földi visszaverődés a kettős ugrás Es: tenger K = 81 és 5 S / m, vidék és erdő K = 13 és 0,006 S / m, városi területek K = 3 és 0,001 S / m.

## 14. LIVETROPOVHF index-feltevések

Kezdő javaslat, megvitatandó: egy index 0-10 ugyanazon a skálán, mint Hepburn, kiszámított minden pont a rács és minden 3 óra GFS (majd ECMWF és ICON összehasonlításra), és egy második szám útvonalanként, amely azt mondja, hogy mennyi az útvonal egy része megy egy hasznos csatorna a kiválasztott sáv. A súlyok az én feltételezéseim, hogy kalibrálni a foltok.

Felülvizsgálat Bean, Dutton és Kerr után: a profilküszöbök a 6.5. táblázatból származnak, és méréseik skálája, így a törésréteg értéke 10- 25 dB a normál profilnál. Az indexet először dB-ben kell kiszámítani, majd 0-10-re kell kerekíteni; az ebben az ollóban lévő értékeket a foltokkal kell kalibrálni.

Számítás pontonként, lépésről lépésre

1. T kisülés, relatív páratartalom, geopotenciális magasság és függőleges sebesség 1000, 975, 950, 925, 900, 850, 800, 700, 600 és 500 hPa, plusz T és 2 m páratartalom, talajnyomás, réteg magasság és szél 10 m.
2. A talajszint alatti értékeket meg kell semmisíteni; az N és M számítást minden szinten (ITU- R P.453).
3. Számítás − dN / dh a négy réteg Bean és Dutton (1000- 850, 850- 700, 700- 600, 600- 500 hPa) és a diagram, mint sub- refraktív, normál vagy szuper- refraktív a küszöbök a táblázat; belül az első réteg is használja közbenső szinteket, hogy megtalálja a legerősebb gradiens és annak részesedése.
4. Ahol dM / dh < 0 I a csatorna alapját és tetejét, vastagságát és hiányát ΔM.
5. Összehasonlítás a Kerr képletből a sáv minimális vastagságával (körülbelül 180 m 144 MHz, 89 m 432 MHz, mint a Hepburn táblázatban); a küszöb alatt a csatorna még mindig számít, de kevesebb.
6. Én stimulálom a dB-ben kifejezett nyereséget az alábbi táblázattal (a legjobb réteget, nem a rétegek összegét, plusz az anomália kifejezést), és átalakítom: index = dB / 2,5, 0 és 10 között, tehát 25 dB értéke 10. A negatív érték (subrefraction) azt mutatja, hogy 0, de marad a menetvonal adatok.

| A legjobb réteg helyzete | Feltételek | Becsült jövedelem |
| Normál (nem laminált) | -dN / dh a fültartományban. 6.5 (pl. 20- 60 N / km 1000 és 850 hPa között) | 0 |
| A normális felett kissé | a normál sáv felső határa és a szuperfénytörési küszöb között | 0-10 dB arányban |
| Szuperrefrakció | a réteg küszöbén túl (100, 80, 70, 50 N / km), de 157 N / km alatt | 10-15 dB között, 157 N / km-ig terjedő arányban |
| Túl vékony csővezeték | dM / dh < 0, vastagság alatta Kerr küszöbérték | 15 |
| Hasznos csatlakozó sávhoz | dM / dh < 0, a kerr küszöbérték feletti vastagság | 20 dB, + 1 dB 5 M-es deficit egységenként, legfeljebb 25 |
| Szubrefrakció | - dN / dh < 0 az alsó rétegben | -10 dB |
| Anomáliás háttér (hozzá) | Ns modell mínusz ITU térkép klimatológiai N az adott hónapra | 0,2 dB N egységenként, -5 és + 5 dB között |

Számítás útvonal szerint (két helymeghatározó között): vessük be a maximális kört 10 km-enként, ahogy azt már a program az index az útvonalon, és számítsuk ki a tört pontok hasznos csatorna és az átlagos részesedése az alap. A CSIRO-tanulmány az útvonal hasznos részét találta a csővezetéken (az AIS 162 MHz-es útvonalán 67% -kal volt magasabb, mint az ellenőrzési esetekben).

Az útvonalonkénti számítás hozzáadása ( LA0BY és W3SZ -tól): (1) tengeri állomások a csatorna alapjához és tetejéhez képest, mivel a magas vezeték csak azokat szolgálja, akik belépnek; (2) egy troposzcatter "padló" a Yeh módszerrel, a kiindulási sarkok a földi profilból kivett; a dB-ben kifejezett trope nyereség eltávolításra kerül ebből a veszteségből, és az eredményt összehasonlítjuk az állomás által elért veszteséggel ( KK6MC táblázat). Így az index az elérhető távolság becslésévé is válik.

Javítások, hogy próbálja ki egyszerre (Bean és Dutton és LA0BY, IK3XTV, G4MCU már a kézikönyvben), csak akkor kell tartani, ha javítja az összehasonlítást foltok:

- Keverés (fontos szerint a szerző, napi megfigyelés): nap a földön, a közép és a délután között tiszta ég, vagy a szél a földön több m / s, a csatornák a földön eltűnik; a hozzájárulást csökkenteni kell. Nem a tengeren.
- A nap ideje (importante fontos szerint a szerző, napi megfigyelés): irracionális csatornák születnek napnyugtakor és a napfelkelte után; ez egy 3 órás előrejelzési ellenőrzés.
- Függőleges mozgás: a levegő 850- 700-ra emelkedik hPa remixeli a rétegeket (negatív korrekció), a levegő alá egy anticiklon erősíti őket (pozitív).
- Nyomástrend: az utolsó órákban felfelé irányuló nyomás, magasabb és stabil jelzés; csökkenő, alacsonyabb jelzés (bastano 2 hPa).
- Synonym: magas nyomás felett 1025 hPa, hőmérséklet 850 hPa sokkal magasabb, mint a földön, meleg és száraz levegő áramlik a hidegebb tengeren, útvonal a tengeren.
- Hegyi utak: egy második "konvektív" index ( IK3XTV szakasz), amelynek délutáni limitrétege magasabb, mint az út mentén lévő gerincek és a nagy nyomás.

VHF / UHF DX Könyv, egyéb korrekciók, hogy megpróbálja: egymást követő nap nagy nyomás a ponton (több nap, több pont), pozíció összehasonlítva a központ az anticiklon (bónusz a széleken), nyomás gradiens a földön mérésével a szél, erős eső az elmúlt 24 órában (büntető), alacsony felhők szélén a réteg (büntető csak a magas sávok). Az útvonalak irányát: hasonlítsuk össze az útvonal azimutját az izobarééval.

Ellenőrzés

- LIVETROPOVHF archívum: minden 500 km és 144 MHz közötti vagy 432 MHz közötti kereskedelmi forgalom "esemény". Összehasonlítja az útvonali indexet az események gyakoriságával, a nap és a hónap azonos időszakában (a CSIRO-vizsgálat azonos megközelítése).
- Összehasonlítás Hepburn és F5LEN ugyanazon a napon: ahol a három indexek szétkapcsolódnak, nézd, kinek volt igaza szerint a foltok.
- Radioszundációk 00 és 12 UTC: ellenőrizni, hogy a modell látja-e a szonda inverzióját.
- Az LA0BY történelmi esetei (6- 11 / 11 / 2003, 10 / 12 / 2004, 15 / 10 / 2011, 11 / 2 / 2015, 22 / 11 / 2018): az említett napokon újraszámított indexnek meg kell mutatnia azokat a csatornákat, amelyeket Hepburn nem mutatott.
- A kézikönyvben feltüntetendő határértékek: 100- 200 m alatt, a GFS nyomásszint szinte láthatatlan; megbízható előrejelzés 2- 3 napra, majd csak trend.

## 15. November 21-i források (második rész)

- ITU- R P.1812 (MatLAB / Octave implementációja I. Stevanovic, OFCOM; GitHub eeveetza / p1812, Zenodo 10.5281 / zenodo.6141558). Az útvonal egyedi VHF / UHF területalapú módszere, a P.452-ből eredően: "delta- Bullington" diffrakció, troposzcatter, csövek és visszaverődések rétegeken, a talajon lévő örvények. Hasznos, mint egy harmadik összehasonlítás mellett P.452 pontszámítási; ΔN és N0 adatok teljes ITU termékek, így a felhasználó letölti őket (a P.453).
- Costa, Rabanal Ramirez, Dias, IET MAP 2018 (doi 10.1049 / iet- map.2017.0992). 563 MHz, 27 pont 5 és 30 km közötti szabálytalan terepen az erdővel: P.526 12 m növényzetű -4,3 dB és -8,8 dB; P.1812 10 m terheléssel + 2,6 dB és -8,9 dB; dielektromos ék -4,8 dB és -9,2 dB; P.1546 + 5,0 dB és -11,7 dB. A program esetében: a jó diffrakciós módszer fennmaradó hibája körülbelül 9 dB, és a bordák terhelésének magassága (10- 12 m) számít.
- PETOOL (Ozgun e altri, Computer Physics Communications 182, 2011). Parabolikus egyenlet split- step egy és két útvonal széles szög propagátor (szögek akár 40- 45 °, szemben 10- 15 ° a keskeny egyik); alján veszteség keresztül átalakított Fourier kevert; föld lépésről lépésre. Igazolja az 1.3.2 (széles szög, talajegyütthatók) választási lehetőséget, és jelzi a meredek mentességek kétirányú kiszámítását.
- MathWorks. `tropopl`: gázveszteség és "lente" veszteség rétegelt atmoszférában (P.835, P.676, P.453), csatornák nélkül: -157 N / km alatti gradiensekkel a geometriai optikai modell nem alkalmazható. A radar szaporítás példája a fénytöréshez - ΔN = 7,32 · e ^ (0,005577 · N), ugyanaz a CRPL jelentés már a programban. `propagationModel` Longley- Rice: előre meghatározott vízszintes polarizációs értékek, vezetőképesség 0,005 S / m, permittivitás 15, reaktivitás 301, mérsékelt kontinentális éghajlat. Egy MathWorks modell sem foglalkozik a csatornákkal.
- ITU- R, a csoport digitális termékei 3. A P.453, P.452, P.1812, P.2001, P.528, P.1546; "integrált" adatok az ajánlások részét képezik; nincs bejelentett újraelosztási engedély: továbbra is hagyja, hogy letöltsék a felhasználónak.
- ESA Navipedia, troposzferikus késés. N = Nidrostatica + Numida; a száraz zenitális késleltetés körülbelül 2,3 m, a párás egy tíz cm, amely gyorsan változik. Számunkra: a GNSS állomások által becsült nedves késés az integrált páratartalom közvetett mértéke, amely a jelenlegi öntés lehetséges jövőbeli forrása; ma nem használható.
- Bray, K2LMG, "A Method for determining V.H.F. Station Capability", QST November 1961, 36- 40 (readed in original; rechected on November 22 on the PDF of the number of QST: all below values compares). A szerző, D. W. Bray (General Electric, Ithaca), azt mondja, ő ellenőrizte az előrejelzéseket hónapokig tervezett kapcsolatok K2GOI és W4LTU. A 13,5 dB-es idő alatt körülbelül 100 mérföld (7 dB alatt és 7 az óra átlaga felett), majdnem lineáris (3,5 dB és 50 mérföld között): szilárd csatlakozásnál távolítsuk el a 7 dB-t. A látóhatár korrekciója: a horizont minden egyes magassága 69 mérföld (111 km) extra távolságot ér (kevesebb, ha negatív). Egy yagi beszerzése körülbelül 10 × boom / λ (teljesítményarány), több mint 4 dB talajtükrözés; SSB -3 dB és AM -7 dB összehasonlítva CW; 99% -a az órák: körülbelül 180 dB-60 mérföld, 190-90, 200-240 (2 m). 1.3.2-től az időszak valószínűsége az 5 dB becsült második hand helyett 3,4 dB-t (± 7 dB ± 2dB) használ.
- Kétirányú számítás (próbálja ki a prototípus, 21 November). Nagy szög parabolikus egyenlet valódi S RTM1 terep Capri, 144 MHz, antenna 10 m a talajtól, standard atmoszféra; mérés: átlagos teljesítmény 20 és 400 m és 80 km között az útvonal felé a megfelelő. Capri és Marina Grande középpontjától nyugatra zárva van (az akadály + 11- 21 °, Monte Solaro): a Faito-ra mutat (majdnem EA5 és EA6 mögött, 150- 160 ° a tengelyen) a jel Spanyolország felé emelkedik 10- 29 dB-re; a Vesuvius-ra (110- 125 ° a tengelytől, majd a számítási tervből, csak indikatív). Anacapri-tól nyugatra a nyugat ingyenes, és a közvetlen nyeremények 8- 22 dB. A prototípus 1000-1300 km-es teljes vesztesége azonban nem megbízható. EA3 EA5 EA6
- Barsis és Kirby, NBS Report 6751 (1961), Pikes Peak, 223 km, 100 és 751 MHz. A hegy az optika két útvonalának közös terminálja: az óramű mediánok eloszlása a két útvonalon lévők konvolúciója. Hosszútávú kirándulás (1- 99%) körülbelül 14 dB, szemben 26- 40 dB azonos hosszúságú vagy szögű troposzcatter-útvonallal; nem figyelhető meg a nap változása. A jel erősebb, mint a troposzcatter, de gyengébb, mint az ideális szélén: a felső kell kezelni kerekített (durva körülbelül 11,5 km). Mindig nagy jeleket figyelünk meg a hátuk mögött.
- Barsis, NBS Report 7652 (1963), three routes over a mountain (Colorado, Alaszka, Europe), 750- 900 MHz. Az átlagos veszteség 10- 20 dB-vel magasabb, mint a Fresnel- Kirchhoff számítása szárazföld nélkül, a terminálok közelében; az evanescence a két vonal optikával való becsomagolásával becsülhető meg; a talaj tükröződései a terminálok közelében súlyosbítják a megnyúlt elhalványulást. 1.3.2-től a vezetékbe való belépés hozzáadódik a P.526 kerekítéséhez.
- Barsis and Johnson, NBS TN 88 (1961). Az űrhullám elhúzódó felbomlása, még a hegy mögötti úton is (5. §).
- Norton, "System loss in radio wave propagation", J. Res NBS 63D (1959). Azok a görbék, amelyekből Bray megkapja a névképeket; a rendszerveszteség, az átvitel és az alapvető folyamatantenna-nyereség definíciói.
- Rice, Longley, Norton és Barsis, NBS TN 101 (1965- 67), Volumes 1 és 2. az egyélű és az akadály tükörképpel és anélkül kerekítve (7. §, a Pikes Peak példájával), a hosszú távú variabilitás V (0,5) és Y (q) az éghajlatra (temperated concontinental és mások). Számunkra: a lekerekített akadály és variabilitási funkciók módszere a kalibrálási bizonytalanságra való hivatkozás.
- ITU- R P.526- 16 (11 / 2025). A J (ν) képlete (31), azonos a programéval; Kerekített akadály (32) - (36), most használt; multi- split methods (Bullington and full method § 4.5).
- ITU- R P.1812- 8 (2025) és -7. Teljes szöveg: a 30 MHz és 6 GHz közötti specifikus módszer (delta-Bullington, troposcatter, csatornák és visszaverődés rétegeken, ingombs). A -7 figyelmeztet arra, hogy a troposzcatter (P.617- 5) 66 km alatt rossz. Ez továbbra is lehetséges harmadik összehasonlítás.
- Paulus, "Plavatical application of the IREPS spangeration model", NOSC TR 966 (1984), and Radio Science 20 (1985). Jelentés a 22 / 11 / 2026-ról (DTIC ADA146528, scan, elismert szöveg, néha olvashatatlan). A hajók által mért légi-tengeri különbségek a stabilitás felé mozdulnak el: melegebb tengeri levegő a hajó adatainak 31-35% -ában, szemben a NOAA vaddisznók 0,7-7% -ával, a hajó által megzavart és a hűtővízből (körülbelül + 1 ° C) vett PST-mérők esetében. A Jeske modell ekkor ad 40 m és több mint 15% az év és magasságok, hogy ugrik 10-40 m 24 óra alatt stabil idő, nem erősítette meg a rádiós intézkedések. Javasolt korrekció (0, -0,5 és -1 ° C-os vizsgálati küszöbértékek; a -1 ° C jobban megfelel az 1982. júniusi rádiós intézkedéseknek): ha a levegő-tenger különbség -1 ° C vagy kisebb a mért különbség; ellenkező esetben a -1 ° C-os és a -0 ° C-os magasságot kell kiszámítani, és ha ez -1 ° C-os, akkor ezt kell tartani. A stabil oldalon a növekedés csökken, a csökkenés megmarad. Júniusban, Marsden 120 négyzet: átlagos magasság 16.8 m (klimatológia) és 7,6 m. A modell egyenletei nincsenek a jelentésben (lásd Jeske és Hitney). A program a semleges profilt használja, amely nem rendelkezik stabil túlzásokkal, és a γ-t 40 m-re korlátozza: ugyanabba az irányba halad; a modellek STS-eivel (nem a hajóval) a Paulus által korrigált mérési hiba kisebb.
- Levy, "Parabolic Equation Methods for Electromágneses Wave propagation" (IEE 2000). A Google Libri-n csak előnézet található: Nem olvasok.
- VK3FS (3fs.net.au). Minőségi bevezetés: magas csatornák 450- 2000 m hosszú nyílások a földön; inverziók 300 - 600 m nyugodt és nyugodt éjszakák, amelyek eltűnnek napfelkelte után; erősebb tengerparti inverziók délután; 800- 1300 km tipikus, 1400- 1600 km gyakran végzett. A napi / éjszakai riasztásokkal és a 2500 m-es plafonnal összhangban.

## 16. Forrás

Szabályok és képletek

- [ITU- R P.453- 13 (2017), rádió refraktív index:](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)képlet és adatok
- [ITU- R P.452- 18 (2023), 100 MHz feletti földi állomások interferenciájának előrejelzése: egyenértékű sugár, β0, troposzcatter,](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)csatornák
- [ITU- R P.617- 5, az](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)rádióhidaknak a horizonton túli terjedése
- [Shi et al., "A Comprehensive Study on Wave Maximumlength of Electricide propagation in different Evolution Duts", IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

Tanulmányok a csatornáknak az időjárási modellekkel való előrejelzéséről

- [P. Lopez, "A 5- year 40- km resolution global clamatology of super- refraction for ground-based radar meteorology", ECMWF Tech. Memo 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO), "Tropospheric Ducting Prediction based on GFS Model Data at... the CSIRO Murchison Radio-astronomy", Observatory arXiv 2608.1861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (FMI), "Effects of model felbontása előrejelzése légköri csapadék feltételek Harmonie- AROME", EGUsphere 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

Amatőr rádióelőrejelzések

- [W. R. Hepburn, "The Hepburn Tropo Index (HTI)"](https://www.dxinfocentre.com/propagation/hti.htm)és [](https://www.dxinfocentre.com/propagation/history.htm)módszer története
- [W. R. Hepburn, "Loest Usable Specification vs.](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, "Worldwide refraktív index előrejelzések"](https://tropo.f5len.org/WW/info.php), [új verzió 2026](https://tropo.f5len.org/?p=504), [megjegyzés az ARPEGE](https://tropo.f5len.org/author/f5len/)modellhez

Adatok

- [Open- Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api), [Megjegyzés az](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)nyomásszintről]
- [NOAA NCEI, Integrált Global Radiosonde Archive (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RAOBget, rádió árvíz forgatókönyv a Wyoming](https://github.com/NCAR/RAOBget)Egyetemen

Klasszikus szövegek (a program szerzője jelzi, a végleges képletek megtekintéséhez)

- B. R. Bean, E. J. Dutton, * Radio Meteorology *, NBS Monograph 92, 1966, 435 pages (reprint Dover 1968): vertikális profiljai N és M, gradiens a csapda alatt -157 N / km, összehasonlítás a rádióhullámmal; 5. fejezet "Synoptic radio meteorology". Bázis pontonként 24,4 számítási lépéshez. Szabad: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [NIST kártya, doi 10.6028 / NBS.MONO.92](https://www.nist.gov/node/1770911), [Internet Archive](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf)másolat.
- L. Boithias, * Radio Wave propagation *, McGraw- Hill 1987 (translated by D. Beeson from French, 330 pages, ISBN 0070064334; also North Oxford Academic): mechanisms within the horizont, common volume of scattering and loss of connection antenna- mezzo. Csak papír ([](https://libcat.library.tamu.edu/Record/in00000014812/Description)könyvtári kártya).
- M. P. M. Hall, * Effects of the Troposphere on Radio Communication *, IEE Electronic Waves Series n. 8, Peter Peregrinus 1979, 206 pages (ISBN 0906048257; brossura 0863410863): synoptic conditions and propagation VHF / UHF. Szinoptikus összetevők alapja. Kereskedelmi IET ([](https://libcat.library.tamu.edu/Record/in00000636385)kártya]).
- H. G. Booker, W. E. Gordon, "A Theory of Radio Scattering in the Troposphere", * Proceedings of the IRE *, vol. 38, n. 4, April 1950, 401- 412: Az elméletet a törésmutató rendellenességéből merítjük. Központi elképzelés: a diffúz energia a törésmutató turbulens ingadozásainak intenzitásától és nagyságától függ (exponenciális korreláció); a horizont közelében a diffrancement, a további szóródás; a szerzők szerint a turbulencia mértéke és a meteorológia által várt indexeltérések elegendőek a mért mezők magyarázatához. 328 cikk (Scilit), ingyenes hozzáférés nélkül. Card on Semantic Scholar ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); teljes szöveg IEEE Xplore fizetett ([doi 10.1109 / JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)).
- C. M. Crain, J. R. Gerhardt (Electrical Engineering Research Laboratory, University of Texas), "Measurements of the Parameters Involved in the Theory of Radio Scattering in the Troposphere", * Proceedings of the IRE *, vol. 40, n. 1, January 1952, pp. 50-54, doi 10.1109 / JRPROC.1952.2739 give the real value values that Booker and Gordon was only reased ([Skilit](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)card].
- "The Constants in the Equation for Atmosphere Refractive Index at Radio Specifications", * Proceedings of the IRE *, vol. 41, n. 8, August 1953, pp. N; innen jön a 77,6 és 3,73 · 10 ^ 5 érték az ITU képletből ([Q]](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E. C. S. Megaw, "Fundamental radio scatter propagation theory", * Proceedings of the IEE Part C: Monographs *, vol. 104, n. 6, September 1957, pp. 441- 455, doi 10.1049 / pi- c.1957.0052: spectrum of index fluktuations from turbulence theory, complete tracing of scatter obound the common horizon and effect of the geometry paid at the IET ([Shilit](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)card]).
- * Proceedings of the IRE *, October 1955, special number on scatter propagation (by K. A. Norton and J. B. Wiesner), with Bullington, Staras and others: free in PDF on [World Radio History](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf).
- D. E. Kerr (szerkesztette), * Prepagation of Short Radio Waves *, MIT Radiation Laboratory Series vol. 13, McGraw- Hill 1951 (Restampe Dover 1965 és IEE 1987): theory of evaporation and surface duts on the sea. Digitális másolat: [World Radio History](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); IET kiadás [IET Digitális Könyvtár](https://shop.theiet.org/propag-short-radio-waves).

A troposzferikus diffúzió legutóbbi lépései a VHF-ben: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Ausztrália), "A Study of VHF Troposcatter Communication Channels in South Australia", in * Digital Signal Processing for Communication Systems *, Springer 1997, 103- 112 ([Springer](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)]. Szöveg díj; absztrakt ez a diffúzió, nem a vezetékek. Csak akkor használható, ha a troposzcatter kifejezést VHF csatorna adatokkal szeretné kalibrálni.

Numerikus modellek és tengeri útvonalak (a program szerzője jelzi)

- M. Levy, * Parabolikus egyenlítési módszerek elektromágneses hullám propagáció *, IEE elektromágneses hullámok sorozat # 45, 2000, 336 pages (ISBN 0852967640): parabolikus egyenlet refraktív profilok, amelyek változnak az út mentén és orográfia; fejezeteket troposzferikus szaporítás, sugarak és módszerek, propagáció a tengeren. Ez a mező kiszámítását szolgálná az útvonal mentén, az indexen túl. Fizetés céljából az IET ([IET kártya](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)) elektronikus formában is.
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (Leicesteri Egyetem): 2 GHz-es mérések a Csatorna-szigeteken és a Csatornán. Az egyetemi archívumban számos mű és tézis ingyenes: [hosszú távú statisztikák a párolgási csatornáról, Radio Science 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434), [aláírások három tengeri útvonalon, Radio Science 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710), [N. Mufti 2012 címei 50 és 140 km-es útvonalakkal VHF](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854)-ban, [I. Alam 2002 tézise az időjárási adatokból számított reaktivitásról, 240 MHz és 2 GHz X3 UQ, [tesi.](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus, "Practical application of an angolation model", Radio Science vol. 20 n. 4, 1985, p. 887 (not IEEE Transactions on Antennas and propagation), doi 10.1029 / RS020i004p00887 ([menetrend](https://zendy.io/title/10.1029/rs020i004p00887)): height of the párolgási csatorna from surface weather data. Szabadítsa fel az azt megelőző műszaki jelentést: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf).

T. Lindquist, * Wave propagation Models in the Troposphere for Long- Range UHF / SHF Radio Connections *, master 's thesis, University of Karlstad, 2 June 2020, 106 pages (provided by the szerző a program). Ez egy step-by-step útmutató, ingyenes, hogy írjon egy számítást a parabolikus egyenlet: split-step megoldás (még a széles szögek), határfeltételek a föld és a tenger, a föld a lépcsőn, gerenda követő lineáris profilok időnként, számszerű ellenőrzések; idézi a PETOOL ingyenes eszköz MATLAB. Két hasznos adat egyszerre: (1) N érzékenység az időjárási változókkal szemben, δN 0,26 δP + 4,3 δe -1,4 δT (hPa és K, 20 ° C körül), azaz 1 hPa hiba a gőznyomáson, 1 ° C-os hiba: a modell nedvességtartalma, amelyet először ellenőrizni kell; (2) A párolgási csatorna átlagos magassága az Északi-tengeren 5- 6 m, a Földközi-tengeren 13- 14 m, a Perzsa-öbölben 20 m felett, mindig alacsony 144 és 432 MHz-nél. A szerző következtetése (1-10 GHz frekvenciák): a horizonton túl a megbízható kapcsolatokhoz szinte mindig kell csatorna.

Szaporítási szoftver a talaj profilján (horizont és akadályok, nem a várható tropo)

- [Radio Mobile](https://ve2dbe.com/rme.html)by Roger Coudé VE2DBE: ingyenes, Windows (valamint Linux és Mac with Wine), Longley- Rice / ITM modell, terepprofil automatikusan kivont kvóta adatok ([letöltés](https://ve2dbe.com/download/download.html)).
- [SPAT!](https://qsl.net/kd2bd/splat.html)by John Magliacane KD2BD: GPL, 20 MHz- 20 GHz, Longley- Rice és ITWOM v3.0 modellek, akadály és veszteség jelentések szöveges; legújabb változat 1.4.2 of 2014.
- [ITM, NTIA / ITS](https://its.ntia.gov/software/itm)hivatalos kód: Longley- Rice modell (20 MHz- 20 GHz) forráskóddal és dokumentációval, Radio Mobile és SPLAT bázissal! A kód ingyenes, és hozzáadhatjuk a számítást a LIVETROPOVHF (veszteség miatt a talaj, hozzá kell adni a tropo index). Eredeti jelentés: A. G. Longley, P. L. Rice, "Prediction of Tropospheric Radio Transmission Loss Over Irregular Terrain: A Computer Method - 1968", ESSA Technical Report ERL 79- ITS 67, July 1968 (NTIA card on historical software: X1 https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITU- R, software and digital data](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): ingyenes térképek ΔN és N0 éves mediánokról a P.452 és P.617 által használt 1,5 ° rácson - hasznos referenciaéghajlati értékként az indexhez.
- Pathloss (Contract Telecommunication Engineering): kereskedelmi program rádióhidakhoz; nem találtam olyan nyilvános forrást, amely megerősítené, hogy az ITU milyen ajánlásokat hajt végre, hogy ellenőrizzék.

Újrahasználható GitHub kód LIVETROPOVHF-ban

| Projekt | Mit csinálsz? | Nyelv | Használata lehetséges számunkra |
| [NTIA / ITM](https://github.com/NTIA/itm) | modell Longley- Rice hivatalos NTIA, csomagokkal. NET |  C++, C#  | föld veszteség az úton: integrálja közvetlenül a program, amely C # |
| [fuzzzeee / itm](https://github.com/fuzzzeee/itm) | ITM C + + kód C # fordítás |  C#  | alternatív nélkül anyanyelvi könyvtárak, is hasznos a Windows 7 verzió |
| [eeveetza / Py452](https://github.com/eeveetza/py452) | ITU- R P.452, azonos az ITU- R WP 3M által jóváhagyott MATLAB / Octave verzióval; a Svájci Szövetségi Kommunikációs Hivatal által közzétett MATLAB változat | Python, MATLAB | hivatkozás a fordításra C # a troposcatter formulák és a végrehajtott, ITU validálási példák |
| [bwinkel / pycraf](https://github.com/bwinkel/pycraf) | P.452 complete, P.676, SRTM profilok; GPL v3, de ITU adatfájlok nem kereskedelmi használatra | ♪ | eredményeink ellenőrzése |
| [ITU- Rpy](https://github.com/inigodelportillo/ITU-Rpy) | számos ITU- R ajánlás, köztük P.453- 13, ITU példákkal hitelesítve | ♪ | a P.453-as N képlet és éghajlati térképek ellenőrzése |
| [NCAR / RAOBget](https://github.com/NCAR/RAOBget) | letöltheti a Wyoming Egyetem rádióhullámát szöveges formátumban | ♪ | Diagram letöltéséhez C # az olasz állomások profiljai |

Megjegyzés: a P.453 jelenlegi változata a [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), ingyenesen letölthető; a dokumentumban a 13-at használtam, hogy újratöltsem.

Már a program kézikönyvek és újra használható itt: LA0BY (Nordic VUSHF 2019), IK3XTV ("Szaporàtás 2 méter" és "Szaporàtás VHF - konvektív sejtek és gravitációs hullámok", Radio Magazine, által biztosított a program szerzője), DF5AI/DK5YA (2003), G3YGF (QST 1983), The VHF / UHF DX Book sapka. 2, G4MCU (2003) KP4MD
