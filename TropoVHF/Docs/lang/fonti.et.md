# Allikad ja valemid

> Masintõlge itaalia keelest: kahtluse korral kehtib itaaliakeelne juhend.

LIVETROPOVHFi tropoprognoosi (20.–21. november 2026) jaoks kasutatud allikate töökogu: Põhifüüsika, olemasolevad andmed, avaldatud indeksid, valemid, amatöörraadiokogemused, valideerimine ja otsused. Valemid on nummerdatud nagu LaTeX, (1), (2),... ja tekstis, mida nad meelde tuletavad, sulgudes oleva numbriga. Prognoosi käsiraamat kirjeldab, kuidas programm neid kasutab.


## 1. Autori ruumid

IC8TEM tähelepanekud, mis raamivad tööd, koos allikates leiduva tagasisidega.

- Itaalia geograafia nõuab difraktsiooni. Dorsali, promontori ja saartega, mis asuvad paarikümne kilomeetri kaugusel jaamadest, arvutatakse difraktsiooni sissemaksed Fresneli parameetriga peaserval, mitte ainult siledal maal. Koosolek: võrdluses ebakorrapärase maastikuga 563 MHz (Rio de Janeiro, 27 punkti vahemikus 5 kuni 30 km) olid parimad meetodid P.526 tera servadega, P.1812 ja difraktiivne kiil, mille keskmine viga oli −5 kuni +3 dB ja dispersioon umbes 9 dB; P.1546 oli kõige pessimistlikum (Costa, Rabanal Ramirez ja järsud nõlvad PETOOLi jaama lähedal meeles pidada, et nad teenivad laia nurga paljundajaid ja kahesuunalist arvutust (laine kajastub reljeefides).
- Et jõuda kaugesse kanalisse, on vaja Fresnelit. Kui kanal algab horisondi taga, tuleb signaal meile difraktsiooni teel üle takistuste kuni selle alguseni; alates 1.3.2 arvutab programm selle (Eelmise käsiraamat, § 10 ter). Koosolek: Rohde & Schwarz (2023) märgib, et kõrge kanaliga jaamad sageli ei sisene, samas kui otste lähedal asuvad jaamad jah.
- Tingimused muutuvad kiiresti. Teooriad on olemas, kuid tingimused muutuvad minutites ja ükski prognoosimissüsteem ei võta andmeid reaalajas: GFS ilmub iga 6 tunni tagant, aja ja 0,25 kraadiga ning prognoos võib lühikese aja jooksul väärtust kaotada. Koosolek: Bray (QST, november 1961) tunni keskmise kõikumise mõõtmine ligikaudu ± 7 dB üle 100 miili; kanalid liiguvad atmosfääri ajaga (Rohde & Schwarz); meie QSO register 29/12/2021–2/1/2022 näitab avasid, mis kasvavad ja langevad tundide jooksul.
- Parimaks nüüdseks on jäänud DX cluster-PSKReporter võrk. Et teada saada, mis nüüd juhtub, löövad klastrikohad, PSKReporter (ja WSPR) mis tahes mustrit: need on meetmed. Prognoos ütleb, kust otsida ja kui kaua see võib kesta; reklaamid ütlevad, kas see on olemas. See programm paneb koha ja prognoosi samale kaardile.

## 2. Eesmärk

Idee on kolmas indeks, mille on välja arvutanud LIVETROPOVHF ise alates ilmamudeli vertikaalsetest profiilidest, et olla Hepburni ja F5LEN kõrval, selle asemel et neid asendada. Täna loeb programm nende kaardid juba valmis: indeks ütleks ka * miks * on avamine (kanali kvoot ja paksus, minimaalne riba lõksus) ja see võib tekkida kohtades, mida programm juba kogub.

- Mida saate anda: kanali tõenäosus punkti ja tunni kohta, aluse ja ülaosa osakaal, paksus, minimaalne sagedus lõksus ja mööda teed marsruudi osa kanali sees.
- Mida ei saa anda: õhukesed kanalid (alla mõnekümne meetri), mida globaalsed mudelid 0,25 ° juures ei lahenda, ja mere aurustumise kanalid, mis teenivad ennekõike GHz-st üles.
- Esimene praktiline eesmärk: 144 ja 432 MHz Euroopas koos DX-klastrikohtade, PSKReporteri ja kohalike dekodeerimiste kontrollimisega.

## 3. Põhifüüsika

Kõik algab murdumisest. N arvutatakse igal profiili tasemel: kanal on see, kus modifitseeritud reaktiivsus M väheneb ronimine, st kus dN/dh langeb alla −157 N/km ([ITU-R P.453-13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)).

Refraktsioon (P ja hPa, T kelvinis):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Suhtelise niiskuse H (%) ja temperatuuri t (°C) aururõhk vee kohal vahemikus −40 kuni +50 °C:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

Modifitseeritud reaktsioonivõime (h kilomeetrites või 0,157 · z meetrites) ja kanali kriteerium:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN/dh (N/km) | Määrus | 144 MHz mõju |
| 0 | alamrefraktsioon | vähendatud voolukiirus |
| 0 kuni −79 | normaalne (standard umbes −40) | raadiohorisont ekvivalentraadiusega 4/3 |
| alates −79 kuni −157 | superrefraktsioon | piklik vool, "liiga hea" |
| allpool −15 | kanal (lõksumine) | lõksus signaalid, DX üle 1000 km |

Kuidas eraldada N mudelist rõhutasemel: iga taseme puhul, mida võtate T, suhtelise niiskuse ja geopotentsiaalse kõrguse, arvutate N ja M, seejärel naabertasemete vaheline gradient. Maapinnast madalamad tasemed tuleks ära visata (Alpides 1000 hPa juures on maa all).

Minimaalne lõksus sagedus: kanal juhib ainult laineid, mis on selle paksusega võrreldes üsna lühikesed. Hepburn avaldab tabeli paksuse → minimaalse kasuliku sageduse ([DX Info Centre](https://www.dxinfocentre.com/propagation/luf.htm)); see on pehme piir, mille all signaal järk-järgult langeb.

| Led Paksus | Minimaalne makstav sagedus |
|  61 m  | 962 MHz |
|  91 m  | 464 MHz |
|  122 m  | 276 MHz |
|  152 m  | 185 MHz |
|  183 m  | 133 MHz |
|  213 m  | 101 MHz |
|  305 m  | 53 MHz |

VHF-vahemikus (sageli 120–300 m) järgivad tabeli väärtused üsna hästi konstantset seadust; alla 100 m valem alahindab sagedust kuni 30–40%. Kohandades seda ise (mitte Hepburni avaldatud valemit), f-ga MHz-des ja d-ga meetrites:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

Põhimõtteliselt on 2 m kanal sageli vähemalt 170–180 m, 70 cm jaoks on see umbes pool. Võrkudel ringlev valem λmax = 0,014 · d^1,5 pärineb Kerrist (1951, peatükk 1, nt 18) ja kehtib λ-ga sentimeetrites ja seisab, kanali jaoks, milles M langeb lineaarselt: nende ühikutega annab umbes 2 m 180 m ja umbes 70 cm 89 m, mis on samad väärtused Hepburni tabelis, mis pärineb sellest. Kerr hoiatab, et tegemist ei ole võrgulõikega: isegi mitu korda pikemad lained kannatavad palju kanalit ja viis, et "kaotus" natuke võib parandada vastuvõtu isegi jaamade kanalist välja. IEEE Accessi 2019. aasta uuring kinnitab, et klassikalised meetodid (Kerr, Kukuškin) ei kirjelda tegelikku lõikust ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)): künnist tuleks kasutada indikaatorina, mitte seinana.

## 4. Tasuta andmed

Nõrgaks punktiks on vertikaalne eraldusvõime: 1000–850 hPa mudelid annavad taseme iga 25–50 hPa, see tähendab iga 200–450 m järel, nii et nad näevad hästi inversioone 500–2000 m, kuid mitte maapinna lähedal asuvaid õhukesi kanaleid. LIVETROPOVHF juba kasutab Open-Meteo isobare, temperatuur 850 hPa ja inversioon: indeks saab alustada samadest kõnedest.

| Allikas | Mis annab | Resolutsiooni | Kasuta indeksit |
| [Open-Meteo, GFS](https://open-meteo.com/en/docs/gfs-api)] | T, suhteline niiskus, kastepunkt, geopotentsiaalkõrgus taseme järgi (1000, 975, 950, 925, 900, 850 hPa...); piirkihi kõrgus | 0,25° rõhutasemel; 16 päeva, kuni 120 tundi, uuendatakse iga 6 tunni järel | profiilid N ja M, kalle, kanali alus ja ülemine osa; juba programmis |
| [Open-Meteo, ECMWF](https://open-meteo.com/en/docs/ecmwf-api)] | sama tase avatud andmemudelitest 0,25°; maapind annab kastepunkti |  0,25°  | teine arvamus, mis on kasulik mudelite kooskõla seisukohalt |
| [Open-Meteo, DWD ICON](https://open-meteo.com/en/docs/dwd-api)] | T ja suhteline niiskus rõhutasemetel; ICON- D2 -s on 800, 900 ja 925 hPa | ICON-EU umbes 7 km | kolmas arvamus teemal „Euroopa |
| [Radiosunds, Univ. Wyoming](https://github.com/NCAR/RAOBget)] | mõõdetud profiilid, kaks starti päevas (00 ja 12 UTC), tekstivorming jaama kohta | palju märkimisväärseid tasemeid, isegi iga paarikümne meetri tagant | indeksi kontroll; vaata peenikesi kanaleid, mida mudel ei näe |
| [GROANS, NOAA NCEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)] | ringhäälingu ajalooline arhiiv, fail jaama kohta, tuletatud parameetrid | ülalt | kalibreerimine aastate andmete põhjal ja võrdlus mineviku täppidega |
| ERA5 (Copernicus) | tunnine kordusanalüüs alates 1940. aastast, 37 rõhutaset | 28 km | statistika ja kalibreerimine, mitte prognoos |

- Avatud meteo geopotentsiaalne kõrgus on merepinnast kõrgem: maapinna alla jäävad tasemed tuleb kõrvaldada ([Open-Meteo, ülemine õhk](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)).
- Maapinnale lisatakse T ja niiskus 2 m juures: nad annavad esimese gradiendi, selle, kus tasandikul ja merel sünnivad öised pinnakanalid.
- Itaalia saated Wyomingi ülikooli arhiivis (weather.uwyo.edu/upperair/sounding.html): jaamad ja numbrid WMO alltoodud tabelis.

| Jaam | W | laiuskraadi | Pikkuskraad | Jaga |
| Udine/Campoformido |  16044  |  46,03  |  13,18  |  94 m  |
| Milano/Linate |  16080  |  45,43  |  9,28  |  103 m  |
| Merepraktika |  16245  |  41,65  |  12,43  |  12 m  |
| Brindisi |  16320  |  40,65  |  17,95  |  10 m  |
| Trapani/Birgi |  16429  |  37,92  |  12,50  | 14 m |

Wyomingi tekstivorming annab iga taseme rõhu, kõrguse, temperatuuri, kastepunkti ja niiskuse kohta: N ja M jaoks piisab. Wyomingil ei ole ametlikku liidest, skripte kasutatakse nagu [RAOBget NCAR](https://github.com/NCAR/RAOBget); DuPage'i kolleegium hoiab koopiaid tekstis jaama kaupa ([nt 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)).](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. Indeksid ja avaldatud meetodid

Hepburn ja F5LEN algavad samadest füüsikalistest koostisosadest, kuid ei avalda täielikku algoritmi; 2008–2026. aasta teaduslikud uuringud kirjeldavad selle asemel meetodit samm-sammult ja sealt on kõige parem retsepti kopeerida.

| Meetod | Andmete käivitamine | Arvutatud summa | Kasulikud märkmed meile |
| [Hepburn Tropo indeks](https://www.dxinfocentre.com/propagation/hti.htm)] | Kanada meteoroloogiateenistuse (MSC, nagu on kirjutatud selle kaartide all; mitte GFS/NAM), GrADS-iga kujundatud kaardid, patenteeritud algoritm ([storia](https://www.dxinfocentre.com/propagation/history.htm)]) | 0–10 signaali kõveruse lineaarskaala: 0 normaalne, 1–2 ülirefraktsioon, 3–4 mõned kanalid, 5–6 hajutatud kanalid; negatiivsed väärtused = alarefraktsioon | imiteeritav skaala, nii et kolmas indeks võrdleb kohe esimest kahte |
| [F5LEN, maailmaprognoos](https://tropo.f5len.org/WW/info.php) | Euroopale Météo-France'i ARPEGE mudel, 7,5 km, rohkem tasandeid 2 kuni 1500 m ([märkus autor](https://tropo.f5len.org/author/f5len/)) | Keskmine üürileandja indeks, 40 sammu 3 tunnist, 4 arvutust päevas, 28-päevane arhiiv ([uus versioon](https://tropo.f5len.org/?p=504)) | Kinnitage, et see teenib mudelit, millel on palju madalaid tasemeid |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)] | 60 ECMWF mudeli taset maapinnast kuni 3000 m-ni | minimaalne dN/dz kolonni kohta: läbi viidud kiirusega −157 N/km, ülirefraktsioon alla −78,7 N/km; püüdurikihi alus | klimatoloogia: Po Valley Euroopa piirkond mitme torujuhtmega aastaringselt; Vahemeri suvel peaaegu 100%, talvel alla 15%; Itaalia üle 30% suveöödel |
| [Indermuehle ja Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861)] | GFS 0,25°, 21 rõhutaset + välja 2 m kaugusel | M = N + 0,157 z; minimaalne dM/dz alla 3 km, kanali põhi ja ülemine osa, M-puudujäägi sügavus; vette tagasi lastud alla 40 m | kontrollitud 7 aastat 25 kanalil 88 MHz kuni 2,68 GHz; hea võimsus kuni umbes 3 päeva ette (AUC umbes 0,74), siis langeb; GFS parem kui ERA5 |
| [Hämäläinen et al., IMF 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)] | Harmonie-AROME, 750 m või 2,5 km, 65 või 90 taset | dM/dh < 0 mudeli profiilil, võrdlus 4–59 m kiirega | loeb rohkem vertikaalset kui horisontaalset lahutust; ükski mudel ei näe kanaleid alla 15 m; temperatuurigradiendil domineerib niiskus |

Kolmas amatöörraadioteenus on juba aktiivne: [TROPOCAST.EU](https://tropocast.eu/)of Artur SQ3A, 2 m ja 70 cm Euroopas. Prognoosi leheküljelt ([tropocast.eu/forecast](https://tropocast.eu/forecast)]):

- Andmed: DWD ICON-EU (pass 0,0625°, umbes 7 km), vaikimisi allikas; alternatiivina Kanada GDPS (0,15°), vähem üksikasjalikud.
- Meetod: N ja M vastavalt ITU-R P.453 profiilile maapinnalähedase kihi ja 850 hPa vahel; kihtide äratundmine, kanalite liitmine maapinnale ja kõrgele ning värvide lävendid on autori nõuetekohane kalibreerimine, avaldamata.
- Tooted: "täielik" vaade 0–3000 m, rohkem eraldi vaateid "maapinnale" ja "kõrgele" (nüüd ainult lisatavatel kaartidel); skoori 0–100 üheksas klassis (alla 38 taustaga, 38–52 marginaalne, 53–58 nõrk, 59–65 mõõdukas, 66–72 hea, 73–80 väga hea, 81–88 kõrge, 89–95 tugev, 96–100 väga tugev); prognoositakse kuni +72 tundi tunni sammul, animatsiooni ja arhiiviga.
- Piirkonnad: Euroopa, Poola, Ühendkuningriik, Saksamaa, Prantsusmaa, Hispaania, Itaalia, Tšehhi Vabariik, Horvaatia; liides ka itaalia keeles.
- Taaskasutamine: kaardid on ühendatud API võtmeta iframe'iga; on olemas ka dokumenteeritud API (OpenAPI ja JSON skeem). Litsentsi ei ole märgitud: Enne selle kasutamist kolmanda allikana Hepburni ja F5LEN kõrval peaksite küsima autorilt.

Erinevused meie projektist: TROPOCAST peatub 850 hPa juures (umbes 1,5 km), samal ajal kui kõrgendatud kanalid jõuavad 2000 m kaugusele ja kaugemale ( LA0BY juhtum 22/11/2018) ega anna bändi või marsruudi indeksit. Klassid 0–100 on võrreldavad meie indeksiga 0–10 samadel päevadel.

TROPOCAST pildistruktuur (programmi autori antud lingilt): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, kus n on prognoosimise etapp. Sait keelab robotite automaatse tagasivõtmise, nii et ma ei saanud pilti lugeda; ka selleks, enne kui neid kaarte LIVETROPOVHF-is näidatakse, teenib autori luba.

Märkus: [DJ5AR, kategooria](https://dj5ar.de/?cat=174)(Magonza, 21 artiklit 2017–2023, 2 m–13 cm). Kirjeldatud juhtumid: viidi läbi mitu nädalat Iirimaalt (23. mai–2. juuni 2023), FT8 Valgevenesse (september 2020), Itaalia–Saksamaa üle Alpide ja Aadria mere (august 2017). Tunnistage "liiga kõrge" kanali (tagurpidi 1000 m) juhtumit, mida ei saa maapinnalt kasutada: veel üks kinnitus, et on vaja võrrelda kanali osakaalu jaamade osakaaluga. Kasuta avamõõduna (kuni 1028 km) radiofaare ning Pariisi, Bordeaux' ja DWD vertikaalprofiile.

Kokkuvõttes on levinud retsept: vertikaalne profiil → N ja M kihi kohta → kiht, millel on dM / dh negatiivne → alus, ülemine, paksus ja kanali tugevus → indeks. Hepburni "salajane" osa on reaalsete sündmuste empiiriline kalibreerimine, mida saame täppidega ümber teha.

## 6. Kasulikud valemid programmi jaoks

Lisaks N-le ja M-le (põhifüüsika osa) kasutatakse neid vertikaalselt profiililt punktnumbrile liikumiseks ja seejärel tee järgi. ITU valemid olid rekonstrueeritud osaliselt halvasti loetud PDF-iga; 22. novembril vaadati ametlikud tekstid üle ([ITU-R P.452-18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453-14, P.526-16, P.372-18) ja korrigeeriti koodis: troposkatteri konstant 190 (mitte 190,1) pluss gaaside neeldumine ρ = 3 g/m3; α mitte alla −3,4 (eq. 55a); kanalite gaas ρ = 7,5 + 2,5·ω (eq. 9a); difraktsioon täissfäärilisel pinnal (eq. 23–37: keskmine rõhk EF-pol), EF-a optilise saturatsiooni asemel tihedusega 2 magnee valemiga 93 (e). Rumore P.372-18: tabel 1 ja galaktika vastavad; Tehismüra kõverad on kuni 250 MHz ja galaktiline kuni umbes 100 MHz, üle programmi laiendab samu seadusi.

1. Mudeli kahe tasandi vaheline kalle (h km, M ühikus M):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. Toitejuhtme tugevus ja paksus: paksus d = ülemine − alus (mullakanali puhul alus = muld); tugevus M-puudulikkus aluse ja ülemise osa vahel.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3. Maa raadiuse ekvivalenttegur (P.452–18, nt 5 ja 6a), kusjuures ΔN = keskmine N-langus esimese kilomeetri jooksul, positiivne, N/km:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

Kui ΔN = 40, on teil umbes 4/3 k (standardne atmosfäär); kui ΔN läheneb 157 k kardinate lõpmatusele, see tähendab, et Maa raadio muutub tasaseks: see on kanali algus.

4. Kõrge antenni raadiohorisont h meetrit, raadiusega\_e kilomeetrites:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5. kanali tõenäosus laiuskraadide jaoks (P.452-18, eq. 2), ajaprotsent; φ = marsruudi keskpunkti laiuskraad, μ1 ja μ4 võtavad arvesse, kui kaugel see meritsi on:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

41° N, kõik meritsi (μ1 = μ4 = 1), annab umbes 11% ajast: on klimatoloogiline alus, kasulik võrdlusväärtusena, millest eeldatav indeks erineb.

6. Tropohajumise kadu (P.452-18, nt 45; f MHz-des, d km-des, θ-nurk mrad-is, N0 maapinna reaktsioonivõime marsruudi keskel):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

Termin −0,15 · N0 näitab, miks märg õhk aitab ka ilma kanalita: 50 täiendavat N ühikut on 7,5 dB väiksem kui kadu. LIVETROPOVHF kasutab juba hajumiskao G3YGF versiooni.

7. Kadu kanalis (P.452-18, eq 46–51): ühenduskoha püsiv kadu pluss nurga milliradiandi sumbumine, mis kasvab sageduse kuupjuurega (f GHz, a\_e km):

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. Bean ja Dutton (1966) ja Kerr (1951)

Kaks klassikalist teksti annavad indeksile kolm asja, mida on kohe vaja: gradiendi lävi on juba samal GFSi rõhutasemel, mis on väärt dB-s reaktsioonivõime muutust ja millised ilmaolud tekitavad või hävitavad kanalid. Raadiometeoroloogia*, NBS Monograph 92.

1. Rõhukihtide profiiliklassifikatsioon (Bean and Dutton, tabel 6.5, lk 248; −dN/dh N/km). Mõeldud raadiolainepikkustele, kehtib see mudeli tasemel:

| Road | Jagab | Alamrefraktsioon | lamineerimata | Ülirefraktsioon |
| 1000–850 hPa | 0–1,46 km | −dN/dh < 0 | 20–60 | 100 |
| 850–700 hPa | 1,46–3,01 km | −dN/dh < 0 | 20–50 | üle 80 |
| 700–600 hPa | 3,01–4,20 km | −dN/dh < 0 | 15–40 | üle 70 |
| 600–500 hPa | 4,20–5,57 km | −dN/dh < 0 | 10–30 | 50 |

2. Kui palju on väärt dB (Bean and Dutton, cap. 6, lk 233–258), mõõdetuna 70–180 MHz horisondivälistel liinidel:

- Iga kiht (maapinnal või kõrgusel) võrreldes "sileda" profiiliga tõstab signaali 10–25 dB kõigile protsentidele; suurem kasu tuleb kõrgetest kallutatud kihtidest, tõenäoliselt fookusefekti tõttu (200 km).
- Maapinna reaktsioonivõime Ns ja esimese km ΔN vähenemine on seotud kuu mediaansignaaliga, mille koefitsient on vahemikus 0,4–0,95, mediaan 0,7 (20 liini vahemikus 130–446 km, umbes 100 MHz).
- Aastane tsükkel: 0,18 dB ühiku N kohta; päevane tsükkel 0,2–1,1 dB ühiku N kohta, maksimum 175–200 km pikkustel marsruutidel ja talvel.
- Misme ühendparameeter hindab välja varieeruvust järgmiselt: 0,5 dB/(N/km) · (ekvivalentne gradient − 40) pluss termiline stabiilsus; turbulentsi parameeter D (reaalse profiili ja adiabaatilise ala vaheline ala, 0–3 km) lihtsalt paraneb võrreldes lihtsa ΔN-ga. Autorite järeldus: ΔN jääb lihtsuse parimaks parameetriks.

3. Võrdlusprofiil ja anomaalia (Bean and Dutton, § 3.8 ja 3.9). CRPL võrdluse eksponentsiaalne atmosfäär annab N-dest algava "normaalse" profiili; A-ühik mõõdab, kui palju tegelik profiil iga aktsia puhul erineb. See on loomulik viis muuta GFS-profiil anomaaliaks, mida kaardile panna:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

Kui Ns = 313, on ΔN umbes −42 N/km, mis on ekvivalentne raadius 4/3. Ka kanali kriteerium −157 N/km tuletatakse siin Snelli seadusest horisontaalse raadiuse (nt 4.23) kohta, mis on antenni kõrgusest peaaegu sõltumatu.

4. Kus ja millal tekivad kanalid (Bean ja Dutton § 4.4; Kerr kork). 3:

| Protsess | Olukord | Effect |
| Hoiatus | kuum ja kuiv õhk maast, mis voolab külmemas meres (tüüpiline suvekärule pärast hea ilma päevi); loeb peamiselt niiskusdefitsiiti | intensiivne kanal merel |
| Öine kiiritus | rahulik taevas, nõrk tuul; madalad pilved või palju niiskust takistavad seda | viinud maapinnale, mis on sündinud päikeseloojangul, kasvab õhtul ja kaob kiiresti pärast päikesetõusu, eriti sees |
| Vajumine | antitsüklonist alla tulev õhk, kuum ja kuiv, värske ja niiske õhu kohal; sagedased üleujutused mägedes ja kaguosas antitsükloni | ülirefraktsiooniline kiht või kõrgjuha; peaaegu alati kalle kõrgem kui standard tagurpidi |
| Meri ja maa | rannik, paarisaja meetri tsirkulatsioon | rannikuäärsed kohalikud torujuhtmed |
| Kanali vastu | segunemine: päikese poolt hommiku keskpaigast õhtuni soojendatud maapind, karedal pinnasel mõne m/s kohal maapinnale paisatud tuul; öine udu | kanali tõenäosus peaaegu mitte midagi |

5. Kaks uut koostisosa proovida. Vertikaalne liikumine: konvergentsitsoonides kasvavad voolud eemaldavad ja kustutavad kihid, vajumine tugevdab signaali (Moler ja Holden: keskmiselt umbes 2,5 dB rohkem kui ülesmäge õhuga, merel sagedusalas L ja X). Avatud Meteo pakub juba vertikaalset kiirust rõhutasemetel. Ja rõhutrend: 140 km kuni 186 MHz rajal laskus signaal, kui rõhk langes saatjale ja tee keskele ning pöördus kõrgele ja ilma evangentsita, kui see tõusis, vaid 2 (Bean ja Dutton, joonis 6.9, lk 241).

6. Kerrilt. Lisaks maksimaalse lainepikkuse valemile (baasfüüsika osa) annab Kerr M-i ligikaudse valemi ainsast temperatuurist, aurust ja kõrgusest (§ 3.9, kus vanad konstandid 79 ja 4800 asendati seejärel 77,6 ja 4810-ga) ning täheldab, et pöördsuunas võib tuule lõikamine tekitada laineid, mis kõiguvad kanali kõrgusel, võimalik põhjus sügavaks pääsemiseks (§ 3.26). Itaalia allikas teatas Bean ja Dutton ja otsida: L. Bonavoglia (detsember 1958), "Korrelatsioon meteoroloogiliste nähtuste ja leviku vahel..."

## 8. amatöörraadio ettekannetest ja tehnilistest aruannetest (teine seeria)

Programmi autori esitatud seitse dokumenti. Kolm serveeritakse otse indeksisse: W3SZ troposhajumise "põrandale", LA0BY juhtudel, kui Hepburn ei olnud ennustanud, Hepburn ( LU7MC) kanalite ilmaklassifikatsioonile.

1. Troposcatter: signaal on alati olemas. R. Rehr W3SZ, "Tropospheric Scatter (Troposcatter) Propagation for VHF, UHF, Microwave and Frequencies", Pack Rats Conference 2014; J. R. Duffey KK6MC, "The Troposcatter Propagation Mode and How to Predict Coverage on VHF / UHF Bands", CSVHF 2017.

- Kasulik umbes 100 kuni 700 km, 144 MHz kuni 10 GHz; kadu kasvab sagedusega 30 log f.
- Ennekõike loeb see lähtenurga horisondi suunas: iga lisakraad maksab umbes 9–12 dB ( M1CNK, Collinsi meetod).
- Suvel on signaal keskmiselt umbes 10 dB kõrgem kui talvel; pärastlõunal umbes 5 dB madalam kui varahommikul või õhtul. KK6MC näitab kogu varieeruvust 13–15 dB aja, hooaja ja ajaga.
- Jah-meetod, kõige lihtsam programmeerida (f MHz-des, d miilides, θ hajumisnurk kraadides, α kahe kimbu avade geomeetriline keskmine):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Larseni (1968) võrdlus 15 tegeliku ühenduse kohta: keskmine ruutviga 5,1 dB Collinsi puhul, 7,4 Yeh’ puhul, 8.1 NBSi ja CCIRi puhul, 16,8 Rideri puhul. Kõik meetodid paranevad, kui Ns püsib stabiilsena (Yeh langeb 5,4 dB-ni): hajumise korral loeb päeva murdumine vähe, nii et tropo indeks peab vaatama kihid, mitte ainult Ns. Tabel 15 ühendusest, kus on mõõdetud kaod (sealhulgas Feldberg-Monte Baldo, 322 km 957 MHz juures, 223,1 dB; Küpros-Iisrael, 319 km 77,5 MHz juures, 163,3 dB), on meie arvutuste jaoks hea katsestend.
- KK6MC annab kaotuse, mida tüüpiline jaam suudab ületada 2 m, ja vastava kauguse 184 dB-st (FT-817 ja 3 elementi SSB-s, umbes 130 km) 215 dB-ni (pikad kopid SSB-s, umbes 430 km) ja 236 dB-ni (kW ja yagi ühendatud, umbes 640 km). See on viis, kuidas tõlkida indeks dBs teenitud kilomeetriteks.
- Valmis kood: W3SZ Yeh ja Collins programm (nitehawk.com/w3sz/YehCollins.zip) ja Bob Atkins KA1GT kalkulaator (bobatkins.com/radio/troposcatter.html).

Teine allikas hajumise kohta: I. Roberts ZS6BTE, "Tropospheric Scatter Propagation", *Radio ZS*, detsember 1984, märkusega 2013 ([qsl.net/zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). Peamiselt Kasulik geomeetria: hajumisnurk θ = θ0 − θ1 − θ2, kus θ0 = d/R, θ1 = (h1 − h\_ostacolo)/d1 + d1/2 R ja sama teise jaama puhul, R = 8446 km (4/3 Maa raadiusest). See on tükk, mida kasutatakse nurkade arvutamiseks maapinna profiilist troposfääri "põrandas". Muud andmed: iga nurk rohkem maksab umbes 10 dB; üleminek Ns 280-lt 300-le vähendab kadu umbes 4 dB; hajumismaht on vahemikus 300–2000 m 150 km, 600–3000 m 300 km puhul; äikesetormid hajumismahus tõstavad signaali umbes 20 dB. artikli numbriline näide ei naase antud valemitega (vaba ruumi termin 900 MHz ja 872 km juures on umbes 150 dB, mitte 125), nii et valemeid tuleks enne kasutamist kontrollida W3SZ -l.

2. Kanali tüübid (W. R. Hepburn, "Tropospheric Propagation Modes", dxinfocentre.com/propagation/tr-modes.htm, LU7MC hispaaniakeelne tõlge). Täitke tabel Bean ja Dutton kasulikke juhtumeid Itaalias:

| Tüüp | Millal | Märkus indeksi kohta |
| Kiiritamine | rahulikud ja rahulikud ööd maa peal | Maa peal, kaob kohe pärast koitu. |
| Kõrge rõhk (vajumine) | antitsüklon | võib kesta kogu päeva; öösel võib allpool oleva kiirguse kanal blokeerida kaugeid signaale, nii et päev võib minna paremini |
| Front | sooja esiosa ees, külma esiosa taga, seisvast esiosast põhja pool | lühike; halb ilm häirib teda |
| Mäest laskumine | föhn, bora: õhk tuleb alla, kuumutamine ja kuivatamine | Boori püüdmine Aadria merel ja föhn Alpidest lõunas |
| Hoiatus | kuum ja kuiv õhk külmal ja märjal pinnasel, näiteks pärast vihma | viis maapinnale |
| Valley | Kuum õhk üle külma õhu peatub orus | päeval |
| Marino | kuum ja kuiv õhk külmemas vees; jõuab rannikule tuulega | tipp pärastlõunal; kevad ja suve algus parimad aastaajad |

Aurustamiskanalid merel (0–30 m) teenindavad ainult umbes 3 GHz ja ei sisene sisemaale: 144 ja 432 MHz puhul saate ignoreerida. Kõrged mäed on takistuseks; kõrbed on liiga kuivad.

3. Juhtumeid Hepburn ei olnud ennustanud (S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", Nordic VUSHF 2019, alates JO59IX kuni 500 m kõrgusel):

- 22. november 2018: 70 cm kuni UA6 (umbes 2600 km) 6–8 tundi. Hepburn viitas ainult Balti riikidele ja Poolale. Kanal oli temperatuurikaartidel näha 900 ja 1500 m kõrgusel ja lõppes veidi üle 2000 m; see oli Norra poolel kõrgem ja sisenes ainult LA0BY mäel. Maapealne aeg ei tundunud tropena: külm ja udu tipus.
- 11. veebruar 2015: 2 m ja 70 cm suunas 9A ja S5 (umbes 1500 km) läbi Alpide, samuti ei näinud seda ette Hepburn.
- 15. oktoober 2011: 2 m kõrge kanaliga EA1 (2135 km) kolmel mere- ja kolmel maapinnal.
- Muud sündmused: 6.–11. november 2003 idas (2050 km), 10. detsember 2004 Prantsusmaa suunas.
- Väliaadressid: udu tasandikul, vabad tipud, väike tuul, märg maapind. Normaaltemperatuuri gradient −6,5 °C/km; reaaljuhul ( VK3KAQ) +7 °C 20 m täpsusega.

Autori järeldus: Hepburn ja F5LEN ei ole piisavad, nad teenindavad profiile marsruudi kõrgusel. Meie jaoks: need kuupäevad on kontrollijuhtumid, mis tuleb mudelite ajalooliste andmetega ümber arvutada, ja tee indeks peab võrdlema kanali kvooti jaamade kvoodiga.

4. Itaalia kogemus (G. IV3GBO, "VHF-i leviku tüübid SSB/CW", 2017): läbitud 450–3000 km, kihtidega 450–3000 m; vihjena laiendatud udu; avaused merel Aadria merel Puglia, Albaania ja Kreeka suunas. Hajuta 100 kuni 500 km kaugusele.

5. Sissejuhatus (P. Wilton M1CNK, "An Introduction to VHF/UHF Propagation", 2013): 70 cm on sageli paremad kui 2 m; 10 m poolusega 144 MHz juures on stardinurk umbes 3°, 20 m ligikaudu 1,5°.

6. NASA aruanne (E. Hirschmann, "Tropospheric and Ionospheric Effects on Radio Frequency (VHF-SHF) Communication", Goddard X-731-67-89, 1967): see puudutab kauguse vigu satelliitidele, mitte DX-ile. See toob tagasi sama N-valemi ja eksponentsiaalse profiili (nt Floridas: Ns 370, ΔN −55, konstantne 0,161/km) ning saadab otsimiseks kaks NBS-i allikat: Bean ja Thayer, "CRPL eksponentsiaalne võrdlusatmosfäär", Monograph 4 (1959) ja Bean, Horn ja Ozanich, "Climatic Charts and Data of Radio Refractive Index for the United States and World", Monograph 22 (1960).

## 9. VHF/UHF DX Book, peatükk 2

I. White G3SEK (toimetanud), *The VHF/UHF DX Book*, DIR Publishing 1995 (ISBN 0 9520468 0 6; Digital replica 2017), peatükk 2 "VHF/UHF Propagation", lk 2-13, 2-22–2-29 ja 4-11. See on kogutud raadioamatööride kogemusele lähim tekst; kinnitab juba kasutatud künniseid ja lisab uusi sünoptilisi reegleid.

Number

- Sama valem N (77,6 P/T + 3,73·10^5 e/T2); näide: 1000 hPa, 7 °C ja = 10 hPa kahjustus N = 324. Hästi kujundatud atmosfääris, näiteks pärast äikesetormi, on N esimese 2–3 km jooksul peaaegu lineaarne umbes 39 ühikut/km.
- Gradientid vahemikus 0 kuni −79 N/km on normaalsed, alla −79 toimub ülirefraktsioon, üle 0 alarefraktsiooni. Raamat rõhutab, et pikk DX ei tule lihtsast ülirefraktsioonist, mis peaks jääma kogu teele, vaid kanalitest.
- Minimaalne kanali paksus, veidi ettevaatlikum kui Kerr ja Hepburn:

| Band | Minimaalne paksus |
| 50 MHz |  400 m  |
| 70 MHz |  300 m  |
| 144 MHz |  200 m  |
| 432 MHz |  100 m  |
| 1,3 GHz |  50 m  |

- Suhe ei ole proportsionaalne lainepikkusega (umbes 65 λ 50 MHz juures, 220 λ 1,3 GHz juures), sest kanali servad on "jagunenud". Ühendkuningriigis on torujuhtmed sagedased 144 MHz-l, kuid peaaegu mitte kunagi 70 MHz või alla selle, ja sageli on 432 MHz tugevad, kui 144 on nõrgad.
- Torujuhtmes langeb signaal 1/d asemel 1/d2: kuni 1000 km kaugusel võib sumbumine olla sarnane vaba ruumi omaga.
- Minimaalne jaama võimsus 2 m (100 W, 15 dBi yagi, 2 dB müra): 217 dB, mis on SSB-l keskmistel tingimustel umbes 400–500 km; CW-s umbes 20 dB rohkem (lk 4–7). Nõustuge KK6MC tabeliga.
- Hajumine: kadu suureneb umbes 15 dB võrra sageduse kolmekordseks ja 10 dB võrra iga hajumisnurga astme kohta (jah valem; väljavõetud tekstis on konstant loetamatu, seega tuleb see võtta W3SZ versioonist). Konvektsioonirakkude "üles" hajumine katab rutiinselt kuni umbes 500 km, 1000 km heade jaamadega. Pärast tugevat vihma ja pikemaid tingimusi on kõigil bändidel vähe.

Juhatuse tüübid

- Maa peal: maa peal levivad nad halvasti, sest pinnas neelab ja levib; rahulikul merel jõuavad kaugele (Scilly-Canarie, 2500 km, umbes kord aastas 2 m). Aurustamiskanalid on sageli vaid umbes 15 m kõrgused ja teenindavad mikrolaineid.
- Kõrge: vahemikus 450 kuni 2000 m, kuna maapinnal on pikem DX; signaal "kõrge" all olevate jaamade kohal. "Kaotavad" kanalid annavad vähem tugevaid signaale, kuid võimaldavad ka vahepealseid kaugusjaamu.
- Kanalitel on täpsed suunad ja need võivad kesta tunde või päevi, vastupidiselt juhuslikule E-le.

Indeksi uued sünoptilised reeglid

- Antitsükloni vanus: õhk langeb keskele paar meetrit tunnis ja vastupidine alaneb kõrgrõhu arenedes; suuremad avaused saabuvad pärast mitu päeva levimist juba hea. Lühiajalisest antitsüklonist ei piisa.
- Kõrge rõhu serv: kiht on kuppel ja servades on madalam, nii et jaamad saavad paremaks.
- Suund: On täheldatud, et pikemad marsruudid kulgevad piki isobare või läbi seljaosade (Flavell G3LTP, 1984), kuid keskusest on suurepärased ühendused ka radiaalses suunas. Näide 29.–30. augustist 1987: Inglismaalt pöörles ava kahe päevaga (Hispaania, Prantsusmaa, Itaalia, Austria, Norra) 180° pärast antitsükloni nihet.
- Vastu: paks isobare (tugev tuul), kihi servas stratocumuliformsed pilved (piiravad kõrgeimat sagedust).
- Esiküljed: külm esiosa surub kuuma õhku üles ja tekitab ees tagurpidi; parim suund on mööda ees, kuid see kestab vähe.
- Hooaeg: Suurbritannias on antitsüklonid sagedasemad suve lõpus ja varasügisel.
- Visuaalsed vihjed: horisondi kohal asuvasse tumedasse joonesse lõksu jäänud suits; kõrge rõhu korral näitavad kihtkumullid, mille alus on alla 1000 m, maapinna kanalit, alus üle 1000 m kõrge kanali.

Otsingutele tsiteeritud viited: R. G. Flavell G3LTP, "Uuringud ulatuslikust antitsüklonilisest levist...", *Radio Communication*, veebruar 1984, lk 128; J. Gannaway G3YGF, "Tropospheric Scatter Propagation", *Radio Communication*, september 1981, lk 710; P. W. Sollom G3BGL, "A Little Flutter" W6ZGN

## 10. Kontrollitud amatöörraadiomudel: Biancomano ja Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, "Practical Tropospheric Model for VHF/UHF Propagation Prediction", *National Weather Digest* 22:2, juuni 1998, lk 15–24, tasuta [PDF from National Weather Association](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf).

- Andmed: raadiolaine 00 ja 12 UTC 47 jaamast Ameerika Ühendriikide idaosas, 1995–1997; maapinna analüüs 700 hPa (alguses kuni 400), N-profiilil, ilma M. Välja arvatud rannikud, saared ja mäed.
- Iga uuringu puhul: kanali olemasolu ja osakaal, maksimaalne lainepikkus, mis võib viia (sageli umbes 100 korda lainepikkusest), potentsiaalse temperatuuri maksimaalne gradient naabertasandite vahel ja ühe ja teise taseme vahel (paksemate kihtide puhul). Maapealseid kanaleid ei kasutata.
- Esimene reegel, püsivus: stabiilsusindeksid (lifted index, K, totals-totals, SWEAT) jäävad 12 tundi enne uuringut 3 ühiku piiresse ja rõhutase, kus potentsiaalne temperatuur kasvab rohkem, jääb umbes samaks. Kui jah, siis prognoositakse paranemist järgmise 12 tunni jooksul.
- Teine reegel, dünaamilised kontrollid: piirkiht, mis ei tõuse, CAPE, paksuse varieerumine 500 hPa-ni kolmel vaatlusel, kalde varieerumine piirides.
- Riba lävi: kanal, mis suudab kanda 100 cm või rohkem, näitab avamist 432 MHz-l, peaaegu kindel 222-l ja sageli isegi 144-l.
- Ruumifilter: jaamad umbes 400–500 km kuusnurksel võrgul; prognoos on väärt ainult siis, kui vähemalt 5 kontiguurjaama on positiivsed (kui saate ühe vahele jätta). 5 jaama puhul muutub vale juhusliku positiivse tõenäosus tühiseks.
- QST veeru "Maailm üle 50 MHz" sündmuste kontrollimine: 1996. aastal oli kavas 48 sündmust 67 (71%); 15 suursündmusest 12. Täpsus kuni 70% 432 MHz-l ja umbes 60% 144 MHz-l 12 tunni jooksul; see tagab peaaegu kahekordse tegeliku sündmuse; 9 viga 19-st on ennustused hilinenud, sest paljud parandused tekivad vähem kui 12 tunni jooksul.
- Lisatakse jaamadele kaugemal põhjas: tuule pöörlemine päripäeva ronides (kuumkäivitus) lisatingimusena.

Täielikust PDF-failist (mida pakub programmi autor) veel kolm detaili. Nende reegel "paksus = 100 korda lainepikkus" annab 600 m 6 m, 200 m 2 m, 125 m 1,25 m ja 70 m 70 cm (joonis 3): 70 cm on vähem raske kui Kerr ja Hepburn (umbes 89 m), 2 m see langeb kokku VHF / UHF DX Book. Päripäeva pöörlev tuul tõuseb autorite sõnul üles, eriti seetõttu, et see toob kõrgusel soojemat õhku, mis paneb N langemise survega kasvama; sellega kaasnev niiskus ei tundu destabiliseeruvat. Lisa saab dN/dh suletud kujul konstantse termilise gradiendiga atmosfääris, kus on antud kvoodi juures järsk hüpe soojusgradient ja niiskus: kasulik meie koodi katsejuhtumina. Nende jälg potentsiaalse temperatuuri tõusust pärineb K-st. H. Jehn, "The use of potential refractive index in synoptic scale radio meteorology", *J. Meteorology* 17, 1960, lk 264.

Mida võtta meie indeksi jaoks: (1) püsivuse tähtaeg: võrrelda profiili 12 tunni varasemaga, et tunniprognoosis on meil vaba; (2) ruumifilter: punkt loeb ainult siis, kui paranemine hõlmab paarisaja kilomeetri suurust ala, mis vähendab valehäireid; (3) stabiilsusindeksid, mis on arvutatud temperatuurist ja kastest kuni 850, 700 ja 500 hPa (Open-Meteo annab juba tõstetud indeksi ja CAPE); (4) tuul, mis pöörleb kõrgusega 1000 kuni 850 hPa; (5) nende kontrollimeetod, mis on sama, mida me tahame teha programmi reklaamidega.

Teine allikas samas pakendis, vähe kasu meile: P. K. Rastogi, "Stratosphere and Troposphere (S-T) Studies at Millstone Hill", NASA 1984 (dokument 19840019069): 440 MHz radar, mis mõõdab puhta õhu turbulentsi vahemikus 4 kuni 25 km, kusjuures õhukesed kihid on seotud gravitatsioonilainete ja tuule lõikamisega. See kinnitab, et hajumist tekitav turbulents on kihiline, kuid see puudutab kanalite liiga suurt tõenäosust.

## 11. Kolmas allikate voor (20. november)

Ajaloolised kõrgkanali juhtumid. J. Macoun OK1VR, 2 m Sněžka tipust (1602 m), sügis 1958, 1959. aasta artikkel, tõlgitud 2006. aastal ([ok2kkw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)). 27.–28. oktoobril 1958 esimene Tšehhoslovakkia–Inglismaa ühendus kiirusel 145 MHz ( G5YV, 1220 km) ja palju rohkem vahemaadel 1100–1250 km, stabiilse signaaliga pärast 23. Autor omistab selle kahele pealiskaudsele inversioonile, ülemisele umbes 2200 m, kusjuures signaal naaseb maapinnale ainult siis, kui alumine kiht lahustus; seisund, mida soosib aeglane antitsüklon Atlandi ookeanilt mandrile. Kodust (Prague) oli selle maksimum 530 km: kanalisse sisenesid ainult kõrgusel olevad jaamad. Tegemist on veel ühe kontrollimise juhtumiga (näiteks LA0BY 2018) ja kinnitab, et indeks peaks otsima üle 2000 m ja võrdlema seda jaamade osakaaluga.

Ajaloolised raamatukogud NBS (kasulik otsida klassikalisi teoseid, mitte valemeid):

- W. Nupen, *Bibliography on Tropospheric Propagation of Radio Waves*, NBS Technical Note 304, 1965, 356 lk, koos teemade indeksiga (ka "seisundi prognoos") ja riikide kaupa. Itaalia kannetes teatati: L. Sacco, "Raadioühendus atmosfääri superrefraktsiooni režiimis", *High Frequency * 24(6), detsember 1955 (Itaalia uuringud: superrefraktsioon alla 500 m sagedasti päeval ja öösel); S. Silleni, *Annali di Geofisica* 8(1), 1955; J. M. Clara e A. Antinori, prova tra Sardegna e Minorca da 238 a 297 MHz (390 km), *Electrical Communication* 33(2), 1956. A. F. Clarke (Fort Huachuca, 1956) avaldas iga päev prognoosi järgmise hommikuse õhurõhu, niiskuse ja tuule eest lõksu jäänud maksimaalse lainepikkuse kohta.
- R. L. Abbott, *Bibliography of Tropospheric Radio Wave Scattering *, NBS, materjal kuni 1960. aasta jaanuarini, umbes 670 eset, mis tellitakse aastas: kasulik troposhajumise osas.

Avalikustamine ja tähelepanekud: F. W. Odenthal DK5EQ, "Ausbreitung 2m – Eine Einführung" (31/05/2024): peaaegu optiline kuni 200 km, tropo kanali kohta või murdumine kaugemal, suure rõhuga ja kevadel ja sügisel; tsiteerib DM2HB VHF-i lehekülge ja DK5YA -i märkusi tropo ja Es. A. Johnson VK3DXE blogis EI7GL (detsember 2021): WSPR VHF-i tuhandete km radadega Austraalias on avastatud; Trope ei ole ainult suvi. Arutelu VHF jahisadama ja AIS-i üle ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)]): ulatus 1,23·√h(ft) meremiili antenni kohta, hajuvus kuni umbes 50 nm, tüüpiline trope paranemine 100–400 nm; kanalid on äratuntavad, sest kauged jaamad on sama tugevad kui naabrid. Chelmsford ARS, detsember 1983: 2 m võistlusel eelistavad Inglismaa idaranniku jaamad mereteed mandrile.

ARRL, *The Radio Amateur's VHF Manual*, 1972, cap. 2 "Propagation Beyond the Horizon" (lk 17–19). Juba teadaolevad kasutusnipid (järgige kõrgsurvealasid, mis liiguvad aeglaselt ja eelkõige nende tagaservad; varasügis on parim periood; rannikualad, suured järved ja jõeorud, mida soosib kihistumine). Kaks uut teavet: (1) viiginumber. 2-4 võrdleb standardatmosfääri (suhteline õhuniiskus 70% kuni 12000 jalga, minimaalne murdumine) tüüpilise tugeva murdumisraadiusega, selge temperatuuripausi ja segu suhtega umbes 4000 jalga (umbes 1200 m): see on allkiri, mida meie arvutus peab GFS tasemetel ära tundma; (2) tõelised kanalid on haruldased alla 144 MHz ja kõige sagedasemad UHF-is vastavalt minimaalsetele paksustele. Kasulik QST bibliograafia otsimiseks: Hull, "Õhumassi tingimused ja UHF-lainete painutamine" (juuni 1935, mai 1937); Hoisington, "Kahe meetri avanemise valutu ennustamine" (oktoober 1949); Collier, "Ülemise õhu tingimused 2-meetrisele DX-le" (september 1955); Botts, "Öö meeles pidada" (jaanuar 1970, läbi viidud maal).

PA0EHG, lehekülg "Tropo" ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): troposcatteri kokkuvõte G3YGF artiklist, juba käsiraamatus. Hajumisarvutuse kontrollimiseks kasutage kahte praktilist reeglit: umbes 10 dB lisakadu iga nurga kohta iga jaama horisondil ja üle 100 km umbes 9 dB pluss iga 100 km marsruut (60 miili); ühine maht algab umbes 600 m 100 km ja umbes 9 km 480 km. See ei käsitle kanaleid, küsitlusi ega murdumislävesid.

PA0EHG, PROPBER kalkulaator ([lk](https://pa0ehg.com/propagat1.htm), propber.exe programm, mille on esitanud programmi autor). See on 36 kB DOS-programm, mis on kirjutatud BASIC-is ja koostatud koos Microsoft BASIC Compiler 5.60-ga (1985): see töötab ainult DOS-i või DOSBoxi all. Sisetekstidest: küsib sagedust, kaugust (või kahte lokaatorit, ka vanas QRA formaadis "E-lokaator"), iga antenni kõrgusjaama kohta müranäitajat, FI riba, võimsust, takistust (kõrgust ja kaugust) ja antenni võimendust (0 = võrdusdiameetri arvutamine, kasutegur 50%). Tagastab maksimaalse kauguse optikas, õige optilise kao auru neeldumisel (0,1 dB/km 24 GHz-st), troposhajumise kao "õige kohtadele" (st takistuste antud horisondi nurkade jaoks) ja S/N suhte kahes mõttes, samuti "maksimaalse ebanormaalse levimisega". Arvkonstandid ei ole kompileeritud käivitatavas failis loetavad. Meie jaoks on kasulik ka kõrgete ribade jaoks soovitatud meetod: arvutuste kalibreerimine mõõdetud lingil (mõõdetud ja arvutatud S/N erinevus) ja sama erinevuse rakendamine teisele ribale. See on meie arvutustes sama muster, kuid see kasutab ühe takistuse asemel S RTM1 profiili ja fikseeritud marginaali asemel GFS prognoosi.

PROPBER, konstandid, mida loetakse käivitatavas failis (numbrid Microsoft Binary Format formaadis). πre, tekstide kõrval olevas andmetabelis: 8,68589 (= 20/ln 10, s.o 20 logs arvutatud naturaallogaritmiga), 32,5 (vaba ruum: 32,5 + 20 log f\[MHz\] + 20 log d\[km\]), 24000 ja 0.1 pluss 40000 ja 0.3 (aurus: 0,1 dB/km 24 GHz juures, 0,3 dB/km 40 GHz juures) Tõenäoline, sest kordate, kuid koodi sees: −69,5 (sama konstantne termin nagu Yeh'i külghajumise valem VHF/UHF DX raamatus), 332 (maapinna murdumine), 8733 km (ekvivalentne raadius, k ≈ 1,37). Täpne hajumisvalem ei ole rekonstrueeritav ilma programmi lahti võtmata. T0

PROPBER, koodiga rekonstrueeritud valemid (koostatud osa demonteerimine; operatsioonid kutsutakse BASIC teeki koos mällu jäävate operaatoritega, kelle väärtused kahekordse täpsusega on dekodeeritud). Varem leitud konstandid −69,5, 332 ja 8733 olid raamatukogus valepositiivsed.

- Optiline kaugus: d = 5 · (√h1 + √h2) km (h meetrites). See on heldem kui klassikaline 4,12 (k = 4/3): võrdub k ≈ 2.
- Kao optikas: L = 32,5 + 20 log f\[MHz\] + 20 log d\[km\], pluss 0,1 dB/km üle 24 GHz ja 0,3 dB/km üle 40 GHz.
- Troposkatter: Lts = L + 23 + 10 log f + 0,0674·d, st 55,5 + 30 log f + 20 log d + 0,0674·d ( G3YGF valem konstandiga 2 dB kõrgem). Nurksõna või reaktsioonivõime puudub.
- Korrektsioon "saitide jaoks": programm küsib takistust ja kaugust, kuid plokk, mis peaks neid kasutama, on tühi; parandusmuutuja jääb 0, nii et "õige" kadu on võrdne hajumisega.
- Müra: T = 290·(10^(NF/10) − 1), N = −228,6 + 10 log T + 10 log B (dBW), antenni temperatuurita.
- S/N = 10 logs P + G\_tx − L + G\_rx − N. "Maksimaalse ebanormaalse leviga" kasutab hajumise asemel optilist kadu, st ideaalset kanalikesta ilma kadudeta väljaspool vaba ruumi.

Võrdlus: 300 km ja 144 MHz PROPBER annab umbes 189,9 dB, G3YGF 187,9, meie P.452 187,8 (nullhorisondi nurkadega): hajumine langeb 2 dB piiresse. Meie arvutus lisab profiililt horisondinurgad, antenni temperatuuri ja "maksimaalse" fikseeritud kihi asemel eeldatava kihi jääkpaarituse ja sumbumisega.

Rohde & Schwarz, P. Denisowski, "Understanding VHF Propagation", Educational Note, aprill 2023 (24 lk, levitamine). Kinnitab juba kogutud punkte (viiakse läbi kahemõõtmelise lainejuhina, signaal, mis langeb lineaarselt kaugusega, kuni 1500 km ja kaugemale, kestvusega tundidest päevadeni; hea pinnas merel ja napp maal; mõnest sajast meetrist mõne kilomeetrini). Indeksi jaoks on kaks kasulikku tähelepanekut: (1) areng ajas: äsja moodustatud kanal kannab sageli ainult kõige kõrgemaid sagedusi ja paksenemine hakkab tootma isegi kõige madalamat; tunniprognoos peaks seetõttu näitama 432 MHz "ettepoole" 144 MHz-l, kui kiht kasvab; (2) jaamade asukoht: parimad tingimused on kanali otste lähedal asuvate jaamade vahel, samas kui kõrge kanali teel olevad jaamad sageli ei sisene, välja arvatud juhul, kui viiakse läbi "kes kaotavad". Kanalid liiguvad atmosfääri ilmaga.

Sait PA0EHG, muud leheküljed. Trope, PathProfile of M. Willis G0MJW ([scheda](https://pa0ehg.com/srtmp.htm), [download](http://www.mike-willis.com/Software/ASTERPathprofile.zip)): marsruudi profiil eeldatavast kogukaotusest ja kvoodifailidest, mis on usaldusväärselt hinnatud troposkatteri jaoks mikrolaineahjus; ei ravi hajutamist lennukitest või vihmast. Kasulik teise viitena meie S RTM1 profiilide ja kadude võrdlemiseks. Ülejäänud ala hõlmab EME-d, mikrolaineahju, lennuvälja (AirScout, SM7LCB kaardid) ja mõõtmeid 80 m.

PathProfile by G0MJW (Mike Willis), ASTER ja SRTM paketid ja SiteData (pakkunud programmi autor; [](https://www.mike-willis.com/software.html)tarkvaraleht). Windowsi 2018. aasta programm: profiil ASTER-ist või SRTM-ist 1" või 3-ni, optikakaod, difraktsioon, tropohajumine (kehtib kuni 1000 km), kanalis ja gaasiline, ajaprotsendiga, marsruudi mediaan, meretee osa ja kaugus rannikust: struktuur on P.452/P.1812, kuid abi (kokkupressitud.chm-fail) ei saanud siin lugeda. SiteData teisendab lokaatorid ja annab osa GT OPO30 -st.

Kõige kasulikum osa on selles sisalduvad andmefailid: ITU-R P.453 statistilised kaardid ülemaailmsel ruutvõrgul 1,5° (121 rida 90° N kuni 90° S, 241 veergu 0° ida suunas): gradient dN/dz esimesel 65 m ületas 1, 10, 50, 90 ja 99% ajast; mullakanalite puhul kohaloleku, paksuse ja intensiivsuse protsent; nende suure kohaloleku puhul baasosa, paksus, intensiivsus ja maksimaalne kõrgus; pluss niiske periood ja temperatuur. Interpoleeritud aastaväärtused:

| Asukoht | Maismaajuhe: % / paksus / intensiivsus | Kõrgtorustik: % / alus / paksus / intensiivsus | dN/dz ületas 10% ajast |
| Napoli |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | −380 N/km |
| Türreeni (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | −674 N/km |
| Trapani |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | −520 N/km |
| Brindisi |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | −405 N/km |
| Aadria meri (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | −399 N/km |
| Milano |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | −172 N/km |
| Udine |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | −176 N/km |

Meie indeksi jaoks on valmis klimaatiline viide: kanalitele tüüpiline paksus ja intensiivsus potentsiaalse kanali 50 m fikseeritud asemel ja anomaalia "tänapäeval võrreldes tavalisega" tsooni kohta. ITU andmed: tasuta kasutamiseks mitteärilisel eesmärgil, enne nende avalikku versiooni lisamist. ZO NES01.DEM fail (58 MB, 2' ruutvõrk) sisaldab kliimavööndeid sisemaal / rannikul / merel; formaat ei ole dokumenteeritud ja ma ei dekodeerinud seda. Willise võrrandite lehekülg sisaldab ainult füüsilisi konstante, vaba ruumi ja antenni võimendust.

Neljas allikate voor (20. november).

Indermuehle ja Suzuki 2026, GFSi torujuhtmete ennustus Murchisoni CSIRO observatooriumile (arXiv 2608.12861): see on LIVETROPOVHFile lähim allikas. N-profiilid GFSist kuni 0,25°-ni (pindala kuni 2 m, seejärel isobariline tase, äravisatud maapinna all); iga minimaalse dM/dz veeru kohta alla 3 km, kanali aluse ja ülemise osa kohta integreeritud M-puudulikkus; seejärel P.452-16 ja P.1812-6 GLO-90 25 pikkusega 98–1005 km, 88 MHz kuni 2,68 GHz iga tund. Tulemused: 25 kanali keskmine AUC 0,72; kalibreerimiskonstandina tuleks kasutada P.452 p ajaprotsenti (valik p = 20%), sest ITU mudel hetkega ΔN ei ole kanalis määratletud; oskus jääb peaaegu tasaseks kuni 3 päeva ette. 162 MHz (AIS) juures on vastuvõetud marsruutidel suurim kanaliosa 67% (0,26 versus 0,16) ja suurim kanalisügavus 49% (28 vs 19 M) juhtimisseadmete osas: merel on kanal peaaegu alati, otsustab maa ulatus. GFS näeb poole ajast maapinnal võrreldes ERA5 -ga (4,5% versus 10,4% rakutundidest), samas kui merel näeb rohkem (73% versus 56%). "koridori" indeks (pidevad torujuhtmed marsruudil, kihid, mille paksus on vähemalt 40 m) parandab tulemust: kinnitab, et järjepidevus marsruudil on õige kriteerium.

Longley and Rice, ESSA ERL 79-ITS 67 (1968): meetod ebakorrapärase maastiku jaoks vahemikus 20 MHz kuni 20 GHz (pärast ITMi) koos mulla ebakorrapärasusega Δh, Ns 250–400, ekvivalentraadius 8497 km (4/3), horisondid, difraktsioon, hajumine ettepoole ning varieeruvus ajas ja kliimaga seotud kohtades. Statistiline viide, mitte hetkeline.

ITU-R P.834-2: raadiuse kõverus, Maa raadiuse ekvivalent k·a, M = N + h/a (h meetrites, tuhandetes kilomeetrites), eksponentsiaalne mudel; pole midagi uut kui juba kasutatud.

AGARD CP-70 (1970), troposfääriline levik: õhukesed kihid 1–100 m tugevate gradientidega on tavalised; kõrge kihi peegeldustegur on umbes proportsionaalne lainepikkusega, juhusliku dispersiooniga umbes 14 dB. Mõõtmed sagedusel 90 MHz: vähenemine 9–10 dB, mis on seotud umbes 350 m kõrgusele koondunud N-hüppega, mis asub pilvekihi ülaosas. Külma esiosa läbimine kõrvaldab kihid: Difraktsioon jääb alles.

USA URSI aruanne, komisjon 2 (1960): kõrgete kihtide keskmine paksus 60–210 m (vastavalt punktile P.453), minimaalne gradient 157–160 N/km.

Buscemi K2OVS, VHF levimise juhend: tüüpilised kaugused ribapöörde ja kanali jaoks, kõrge statsionaarse rõhuga ühendatud kanalid, maksimaalsed koidikul.

Wolinsky-Mancini ja 2024, AIS Kanaari saartel: AIS-i raadiolained ja vastuvõtud; kanalid on soojadel kuudel palju sagedasemad (alisaanide inversioon).

Nigeeria ja Malaisia artiklid (Ekah, Iloke, Mat, Igwe): kohalikud korrelatsioonid temperatuuri, niiskuse ja mõõdetud kao vahel, nõrgad ja vastuolulised (r −0,09 kuni 0,27); kasulikud ainult kinnituseks, et ainult maapinna suurused ei ole piisavad.

Herben 1984, väitekiri TU Eindhoven (mikroonde): 30 GHz 8,2 km ja OTS-satelliit 11,6 GHz; säde turbulentsi, mitme tee ja vihma jaoks. Mikrolainete puhul domineerivad dünaamilised kaod vihma ja turbulentsi, mitte kanalite tõttu.

Baars, The Paraboloidal Reflector Antenna (mikroonde): mõistujutu G = η·(πD/λ)2 suurenemine tüüpilise efektiivsusega 0,5–0,65; pinnavigade kadu Ruze järgi, ηs = exp(−(4πε/λ)2). See aitaks, kui lisaksime tähendamissõnad alternatiivina Yagi DL6WU -le 1296 MHz ja kaugemalgi.

Juba analüüsitud või halva sisuga: Hirschmann NASA X-731-67-89 (kahekordne), Reidi VHF ST radar (tuuled ja turbulents, juhtimata), Scribd slaid ja üldised märkused troposfääril.

Viies allikate voor (20. november, õhtu).

Rogers 1997, Radio Science 32(1): Bayesian estimate of the parameters of a high duct (base rate, M deficit, thickness) from propagation factors measured beyond the horizon, with a parabolic equation model; VOCAR experiment in California at 143, 263 and 375 MHz, radio wave comparison. Signaal sõltub palju alusest ja M-defitsiidist (defitsiidi puhul 0,81 kuni 143 MHz), peaaegu üldse paksusest (−0,17): paksust ei saa signaalist saada. Meie jaoks: arvutus B (kliima paksus) loeb vähem kui puudujääk ja osa; ja meetod on aluseks back office, st saada kanalit mitmest sagedusest.

Moler and Holden 1960, "Tropospheric scatter propagation and atmospheric circulations", J. Res. NBS 64D, lk 81 jj (loe originaalis 22/11/2026, doi 10.6028/jres.064d.014). California mereteed Point Loma (San Diego) juurde: Santa Barbara 190 miili, Point Mugu 144, San Clemente 78; 1365 MHz ja 9380 MHz, mitte VHF; jaanuar–aprill 1958, keskmiselt 1 minut. 26. märtsi juhtum: umbes 20 dB tugevnemine umbes 3 tunniks keskpäeva suunas, kui tsükloni keeris nõrgeneb ning algab meretuul ja vajumine; kell 15 konvergentsi reformitakse ja signaal langeb maksimumist umbes 22 dB. Statistika (tabel 1, 500 hPa, 33 juhul): õhuga, mis langeb keskmine signaal on suurem kui umbes 2,5 dB kui õhu tõus, ribad L ja X. Signaal järgib 500 hPa väärtusele arvutatud erinevust märgis ja variatsioonide amplituudis, ilma korrelatsioonikoefitsiendita. Madalad kihid (alla 600 m) soodustavad hajumisel osalist peegeldust. Antitsükloneid ja rindu ei käsitleta ja ülalmainitud "ekskursioonid kuni 60 dB" ei ole: need tulid kasutatud kokkuvõttest. Meie jaoks: vertikaalne liikumine on hea ennustaja ning vajumise puhul +1,5 dB ja ülesmäge õhu puhul −2 dB on samas suurusjärgus kui keskmine mõõdetud erinevus (2,5 dB); 20 dBs on üksikjuhtum, mitte tüüpiline väärtus ja mikrolaineahjus.

Banafaa ja Muqaibel 2025, IEEE Access: torujuhtmete uuring (aurustusmudelid, paraboolne võrrand, radarihinnangud, GNSS ja lidar) ja klassifitseerimine automaatse õppimisega reaalsete meetmete kohta; Tugivektori masinad annavad parimad tulemused. Kasulik bibliograafiana; andmed on Pärsia lahe kohta.

ITU-R P.834-7 (2015): troposoole murdumist käsitleva soovituse ajakohastatud versioon; ei ole uut valemit kui juba kasutatud valem.

Abbott, NBS bibliograafia troposfääri hajumisest (GOVPUB- C13): juba kolmandas ringis. Slide Scribd kanalitel: üldine sisu. Artikkel ScienceDirect 0021916987900304 avamine nurjus.

Kuues ring (20. november, öö): Bean and Dutton, ARRL Handbook 2022 ja 2023, Balanis.

Bean and Dutton, Radio Meteorology (punkt 6), osad, mida ei ole veel kasutatud. Välja tundlikkus Ns suhtes: 0,18 dB ühiku N kohta aastases tsüklis; 0,14–0,24 dB/N päevases tsüklis (maksimaalselt varahommikul), kuni 0,2–1,1 dB/N marsruutidel 175–200 km talvel; parandused 0,8–0,95. Misme’ist koosnev parameeter: M = a(g\_e − 40 + b·ΔW^n) a = 0,5 dB N/km kohta, milles on ühendatud ekvivalentgradient ja termiline stabiilsus ΔW (töö 1 km õhu tõstmiseks): Saharas 471 MHz juures 160 km peal on ainus stabiilsus 20 dB jaanuarist juunini peaaegu võrdse kaldega. potentsiaalne murdumisnäitaja K (N esitatud adiabaatiliselt 1000 hPa-le): tõstab esile õhumassid ja antitsüklonilise vajumise (Flavell ja Lane).

ARRL Handbook 2022 (99a toim.), § 19.4.3–19.4.4. Kasulikud inversioonid vahemikus 250 kuni 2000 m; kiirguse inversioonid (seerud, rahulik õhk, kuiv pinnas) on harva tõesed; aeglased antitsüklonid annavad kõige ulatuslikumad vajumiskanalid, mille inversioonid jäävad 500 ja 3000 m vahele, tugevamad õhtul ja hommikul; põhjapoolkeral on parimad marsruudid kõrgkeskmest lõuna pool. Kuum front: head tingimused paarsada km ees; Külm front: tugev, kuid ebastabiilne tagurpidi ees, paralleelne. Foehn (ka Alpides) ja maatuul (jahe õhk maapinnal, kuum 200–300 m; ulatub rannikul üle 800 km). Scirocco: kuum ja kuiv õhk Saharast jahedama mereõhu kohal, väga tugev inversioon Vahemere idaosast Gibraltarini, tõenäoline põhjus, miks mikrolaineahjus on üle 1500 km rekordeid. Troposcatter: teoreetiline piir umbes 800 km, maksimaalne tüüpiline pool. 100. trükk (2023) on lühem tekst: inversioonid päikesetõusul ja päikeseloojangul kuni +20 dB ja +20–50% voolukiirus; 1200 miili Suurel järvel, tüüpiline 400–600; soovitab Radio Mobile (ITM mudel) Hepburni leviala ja kaartide jaoks.

Balanis, Antenna Theory (4a ed.), § 10.3.3: Viezbicke tabel (NBS TN 688, 1976) Yagi jaoks optimeeritud 400 MHz juures: buum 0,4 / 0,8 / 1.2 / 2.2 / 3.2 / 4.2 λ → 7,1 / 9,2 / 10,2 / 12,25 / 13,4 / 14,2 dBd. Meie DL6WU valem (8.6 + 8 log10 boom/λ) annab 1–1,7 dB vähem vahemikus 0,8 kuni 4,2 λ ja tagastab kooskõlas DL6WU tabelitega 9,5 λ: usaldusväärne hinnang. Õige valemiga originaalil (9,4 + 7,8 log10 buum/λ, alates 22. novembrist) langeb vette tagasi laskmine alla 0,2 dB vahemikus 1,2–4,2 λ (12,1 dBd kuni 2,2 λ versus 12,25) ja jääb 0,6–0,8 λ.

Seitsmes ring (21. november): QST, QEX, W3SZ, GitHub hoidla.

QST 2000–2025 (14 numbrit, 10 voodit tekstituvastusega). Ainult aruanded jaotises "Maailm üle 50 MHz", mis on kasulik mudeli valideerimiseks: 5.–9. september 2000, läbi viidud 144 MHz ja kaugemal USA idaosas; 14.–15. august 2022, avades Manitoba–Kansas/Missouri kõrge rõhu all kuumal päeval, 144–1296 MHz peaaegu kõik FT8 -s, Kanada rekord 10 GHz 1228,56 km ( VE4MA – N0OY) Q65, signaalid kuni 1941 km; APRS levikaart näitas sama ala. QST november 2022 soovitab Hepburni kaarte. Ei mingit tehnilist artiklit tropo prognoosi kohta.

QEX ja K1JT, Franke ja Taylor (QEX 2020) FT4/FT8 režiimidel: FT8 dekodeerimislävi −20,8 dB 2500 Hz kanalil ilma evanssentsita, −18,6 dB evanssentsiga keskmistel laiuskraadidel; FT4 −17,5 dB. Teised QEX 2023 ja 2026 numbrid tropot ei ravi (kolm numbrit 2023 on lugemata skaneeringud). W3SZ 2019: aeglase režiimi läviväärtused ( JT4A −23, JT9A −27, JT65A −25, QRA64A −26, FT8 −21 dB) ning valikukriteeriumid tundlikkuse ja sagedusnihke tolerantsi vahel.

GitHub. TropoLink (LukiBox, Apache-2.0): troposfäärihajumise marsruutide projekteerimine P.617-5 (koos N0 ja ΔN-ga), NBS TN101 ja ITM külgnevad ning nende erinevus on näidatud määramatusena; P.617-5: L = F + 22 log f + 35 log θ + 17 log d + Lc, F = 0,18· N0 · e^(−hs/7,35) − 0,23·ΔN, Lc = 0,07·e^(0,055(Gt+Gr)). andi-f/troposcatter ( DL2FZN 2013, VHF/UHF Handbook RSGB): Lts = 24,44 + 4,283 ln f + 0,06656 d vaba ruumi kohal, kalle PROPBERi lähedal (0,0674 dB/km). QRadioPredict: ITM ja ITWOM 3.0 S RTM3, mullakasutuse Corins ja NEC diagrammidega. Ei ole asjakohane: Tropos (ilmarakendus), opera-adt/tropo ja troposim (InSAR-i Troposferic viivitus), tgbot\_troposcatter (sõjaline jaam), sdr-troposcatter-gui (juhtimispaneel), Troposcatter-Propagation-Delay ( ERA5 viivitus), Radio-Communication-Project (didactic Simulor), PyLayers (kanal).

Kaheksas ring (21. november): NTIA ja ITU mudelid GitHubil. NTIA/itm (Longley-Rice, ITS): 20 MHz–20 GHz, vaba ruumi kadu, difraktsioon ja troposhajumine mullaprofiiliga, 0,5–3000 m terminalid, pinnareaktiivsus N0 (minimaalsus merepinnani, 250–400), seitse kliimat, mullakonstandid ja erinevad asukoharežiimid 1 vaade, 2 difraktsioon, 3 troposhajumist; C++ koos.NET Framework 4.7.2 korpusega. See ei modelleeri kanaleid: see on statistilise varieeruvusega "normaalse atmosfääri" võrdlus. NTIA/p528 ja eeveetza/p528 (MATLAB): ITU-R P.528-5 õhu ja pinnase ühenduste jaoks, 100 MHz–30 GHz, 1,5–20 000 m terminalid, p 1–99%;.NET 4.8 korpus. Võimalik kasutusotstarve: õhu hajumise tasakaal (kaks maapealset marsruuti – õhusõiduk ja radarilõik). eeveetza/p2001 (MATLAB): ITU-R P.2001-6, difraktsiooni, troposfäärihajumise, ebanormaalse levikuga täisväljamudel (juhtivus ja kihid, Lba) ja E sporaadiline, ajaprotsent 0–100%; ITU digitaalkaardid (DN\_Median, DN\_SupSlope, DN\_SubSlope, dndz\_01, TropoClim, h0, surfwv, Es) ei ole ümberjaotavad. Võimalik kasutamine ITU võrdlusena P.452 asemel GFS ΔN-iga kaartide asemel nagu CSIRO uuringus; kaardid peaks kasutaja alla laadima vastavalt P.453-le. Valideerimine 1.3.0 593 QSOst (29/12/2021–2/1/2022): sündmuse/võrdluse lahutamine 0,79 ilma parandusteta, 0,91 Misme ja vajumisega; GFS ei näe kanalit merel 144 MHz juures (guadagno ilma parandusteta kunagi üle 15 dB).

Üheksas voor (novembris 21,): IU3QEZ, NTIA, PyWaveProp, ACE High, valideerimine 1.3.1. IU3QEZ (S. Fabris), esitlus "Üle horisondi VHF" (GEKO Magazine n. 69, september 2026): ühine köide d2/(8·R\_eff) R\_eff ≈ 8500 km (1,3 km d 300 km, 3,7 km kuni 500 km iga horisondi tase, mis on väärt 69 miili (111 km), 7 dB evaneststsentsi ja 4 dB maapinnalt peegeldumise eest; väitekiri: 10 dB on umbes 100 km; jaam IQ3QC 5 W 3×5 elementidega, 120 QSO, maksimaalselt 788 km (Alpe Adria 2026). Tagastatava esitluse bibliograafia: Norton 1959 (J. Res. NBS 63D, System Loss), Bray 1961, ARRL Antenna Book 2023 fig. 4.29, Rice-Longley-Norton-Barsis NBS TN 101, Bean and Thayer 1959, Barsis and Kirby 1961 (diffraction on ridge), Deygout 1966, P.1812, AirScout DL2ALF, P.834 NTIA PropLib (wiki): eHata, ITM, LFMF, P.528, P.2108 (loss from bulk), koos C+523 fig.29, 4.29, Brton-Barsis NBS NBS TN 101, Bean and Thayer 1959, Bean and Kirby 1959, Barsis, Barsis and Kirby 1959, Barsis and Kirby 1961 (di 1961, Bean and Kirby 1961, Barsis, Barsis, Bean and Kirby 1961, Barsis, Barsis, Barsis, Dey 1961 (d 1961, Dey 1961, Dey 1961, Dey PyWaveProp (Python): Paraboolsed võrrandimeetodid koos Padé lähendustega, mitme serva difraktsiooniga, PETOOLi ja muutmälu korpusega, troposfääri ja kõrgete kanalitega, ebakorrapärase pinnasega: võimalik alus väljaarvutamiseks GFS M(z) profiiliga, mida tuleb kontrollida. APRS PropView (Apache 2.0): VHF levikaardid APRS-pakettidest, tunnistatistika ja häirete avamine; võimalik 144 MHz andmete allikas valideerimiseks. NATO troposfäärihajumise võrk Itaalias (fortifications.net): 900 MHz, 10 kW, 20 m mõistujutud; jaamad, sealhulgas Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Vulture, Monte Lauro; ajalooline katsejuhtum P.617. Ei ole asjakohane: Gupta 2021 ( stratosfääri transport), seadus 2023 (Arktika osoon), delfiin #590 ja ooperi-adt/tropo (InSAR-hilinemine), tropo.ino (Arduino gaasiandurid), tropo-webapi (telefonia), pilvtööriistad/troposfäär (AWS). Valideerimine 1.3.1 564 QSO tropo puhul: AUC 0,79 (mudel 1.2), 0,80 (+ pehme juhe), 0,90 (+ hälve ja vajumine), 0,90 (+ püsivus); kanalite värvid (≥ 16 dB) 46% avamisteedel ja 0% võrdlusest.

Lisatud samasse ringi. Mikelytaev/wave-propagation (MIT, Python 3): Paraboolne võrrand Padé ja Numerovi ligikaudsete väärtustega, ebakorrapärane pinnas, troposfäärilised kanalid; laadida NOAA GFS-ilt modifitseeritud murdumistegur M(kvoot, lat, lon) ja määrata Monin-Obuhhovi aurumiskanal; puudub dokumenteeritud funktsioon, mis tagastab kaotuse teel. Ta on parim kandidaat tulevase väljaarvutuse jaoks GFS M profiiliga (see teenindaks välist teenust või tõlget). RP-Sim (AGPL-3.0, JavaScript): Empiirilised mudelid raadiovõrkudele (Hata, He, Ericsson, kaks kiirt), tropo puudub. RIMFading (GPL-3.0, OMNeT++): võrgusimulatsioonide vooluhäired, Weibulli jaotus, tropo puudumine. PyLayers: siseraadiokanal, juba välja arvatud. qradiopredict (kantooon, GPL v2): ITM ja ITWOM v3.0 S RTM3 ja Corine'iga, juba seitsmendas ringis QRadioPredict.

Kümnes ring (21. november): Antenna Raamat, Norton, Barsis, uued andmeallikad. ARRL Antenniraamat (23a trükk), cap 4 ( K9LA): Bray meetod (QST november 1961) VHF-vahemiku jaoks: sile maakadu 99% (300 miili, 144 MHz: 214 dB), kõverad 50% ja 99% (joonis 4.11): 195 dB väärt 100 miili kuni 99% ja üle 250 miili kuni 50%; evanssents ±7 dB üle 100 miili (–3,5 dB kuni 50 miili), SSB 3 dB halvem kui CW, +4 dB maapinna peegeldust, +3 dB kahe antenni ühendamisel; kõrguse tõus (15 m: 4/3/2 dB 10/50/100 miili; 24 m: 8/6 dB/4); Yagi saavutab umbes 10-kordse buumi λ-s (dBi); 50 W jaam kuni 100 dB, umbes 180 miili. Päikesetõus ja -loojang kuni +20 dB ja +20–50% voolu; kuum ja kuiv õhk märja värske õhu kohal Suurel järvedel kuni 1200 miili, tüüpiline 400–600; suurtest veemassidest mõjutatud piirkonnad kõige soodsamad; hilistalv küttega, mis on võimeline tugevaks inversiooniks. Tropohajumise kontroll: P.452 kuni 50% 483 km ja 144 MHz juures annab umbes 204 dB, mis on kooskõlas Bray 214 dB 99%-ga. Norton 1959 (J. Res. NBS 63D, vaba NIST-is): põhikadu, süsteemi ja raja antenni võimendus; võrdlusatmosfäär Ns = 301; ekvivalentne raadius Ns (250 → 4878 miili, 301 → 5280, 400 → 6997); L(99) tabelid 30-suu antennidega (100 MHz: 163 dB kuni 100 miili, 208 dB kuni 500 miili; 1000 MHz: 138 ja 188 dB 28-suu tähendamissõnadega); pikaajaline varieeruvus V(p, θ) sõltuvalt nurgakaugusest (joonis 13). Rice, Longley ja Norton, NBS TN 15 (1959), vaba NIST-il. Barsis ja Kirby, NBS Report 6751 (1961): 223 km Colorados Pikes Peak (4292 m) servana, 100 ja 751 MHz; järjekindel kadu ümara servaga (umbes 11,5 km raadius); pikaajaline evanestsents 11,7–14,3 dB versus 39–42 dB sama pikkuse hajumisega; ei ole märgitud päevatsüklit; neljakiireline mudel andis 4–5 lobe, kuid ainult üks on mõõdetud (maksimaalselt umbes 21 m kaugusel maapinnast): mäetakistusega teed on palju stabiilsemad kui hajumine, mis on kasulik ühendustele "Vesuvius või Faito". ARRL VHF Digital Handbook: package, APRS, D-STAR, WSJT (Scatter Method and EME), no part on tropo. Uued tõestatud või otsitud andmeallikad: ECMWF avatud andmed (IFS 0.25°, tasemed 1000, 925, 850, 700 hPa..., ilma 975, 950, 900 ja 800; G RIB2 koos CCSDSi tihendamisega, mida meie lugeja ei dekodeeri; juba saadaval Open-Meteo kaudu); DWD ICON-EU avatud andmed (G RIB2 tihendatud bz2, fail muutuja ja taseme jaoks: palju päringuid); Kanada GDPS MSC Datamartil ja GeoMetil (umbes 15 km, kuni 33 taset; GeoMet võimaldab ala kärpimist; see on Hepburni kaartide alus GWPR-failide kinnitamisel ainult APR-failide jaoks, 2021. aasta veebruaris.

Üheteistkümnes ring (21. november): Longley-Rice, GEKO 69. Bean and Thayer, "Models of the Atmospheric Radio Refractive Index", Proc. IRE May 1959 (lk 740 jj): mudelil 4/3 on esimesel kilomeetril õige kalle, kuid langeb liiga kõrgemale; 30 % raadiuse kumerust toimub esimese 100 m ja 60 % esimese km jooksul; Ns ja vähenemine esimesel kilomeetril: −ΔN = 7,32·e^(0,005577·Ns) (Ns = 313 → ΔN umbes −42). Bean and Thayer, "C.R.P.L. Exponential Reference Atmosphere", J. Res. NBS 63D n. 3 (1959): N(h) = Ns·e^(−c·h), kus c on fikseeritud Ns poolt sama aruande kaudu; Keskmine Ns USA 313; kiire jälgimise tabelid (kogukõver 15,2 mrad eksponentsiaalse mudeliga versus 30,3 mrad Maaga 4/3). MATLAB refraktsioonexp: c = 0,14859 km−1 Ns = 313 (skaala umbes 6,95 km), kehtib umbes 1 km ja 10 mrad kohal. Kasuta meie jaoks: "normaalne" võrdlusprofiil võib olla see, et CRPL koos kohalike N-dega M = 315 + 0,118·z fikseeritud asemel ja esimese km vähenemise anomaalia tuleks mõõta võrreldes −7,32·e^(0,005577·Ns) -40 fikseeritud asemel. NTIA aruanne 82-100 (Hufford, Longley, Kissick 1982), ITM juhend piirkonna režiimis: klimi ja Ns soovitatud (ekvatoriaalne 360, subtroopiline mandriline 320, subtroopiline mereline 370, kõrb 280, parasvöötme mandriline 301, parasvöötme mereline maismaal 320, parasvöötme merel 350); Ns = N0 ·e^(−hs/9,46 km); kasutate igakuist minimaalset Ns, sest seda mõjutab vähem superrefraktsioon ja subrefraktsioon; Ns = 301 vastab ae = 8497 km; alla 100 km Ns loeb vähe. Rice, Longley ja Norton, NBS TN 15 (1959), I osa: prognoositava jaotuse valem aja jooksul maa ja troposfääri laine järgi, TN 101 alus. SoftWright, Wikipedia, Altair WinProp ja MATLAB rfprop. LongleyRice: sama teave (versioon 1.2.2, 20 MHz–20 GHz või 40 GHz, 1–2000 km, Ns 301, seitse kliimat, aja, koha ja olukorra varieeruvus); keegi ei kujunda kanalit, ainult merekliima statistilist mõju ("kõrgemad väljad 10% ajast või mitte"). GEKO Magazine n. 69 ( IU3QEZ): raadiohorisont 4/3 130 km kaugusel 1000 m, 260 km, mis lisab kaks jaama; troposhatter, mille ühine maht on 1–3 km kuni 10 km; hajumine õhusõidukist 10 km kaugusel +10–20 dB kümnete sekundite või paari minuti jooksul; teoreetilised kursused 2×10 elemendiga külje kohta: SSB QRP umbes 440 km, SSB 50 W umbes 510 km, CW QRP umbes 600 km, SSB 500 W umbes 640 km (500 W SSB-s teeb 40 km rohkem kui 5 W CW); 90–99% ajast peaks 10–20 dB marginaal olema eemaldatud ühe horisondi võrra, peaaegu mitte ühe horisondi võrra, ühe horisondi võrra, mitte ühe horisondi võrra;

## 12. Ümarlaud: mudeli valideerimise või parandamise allikad (21. november)

Kõigi kasutusse tellitud allikate pöörete kokkuvõte. "Riik" ütleb, kas allikas on juba programmis (1.3.1), kas see on testitud või tuleb seda teha.

### Kontrollimiseks vajalikud andmed (mis tegelikult juhtus)

| Allikas | Mis annab | Kasuta | osariik |
| MMMonVHF raportid autorilt | 593 QSO 144 MHz, 29/12/2021–2/1/2022, aeg ja lokaatorid | Sündmuse valideerimine võrdluse alusel (+14 päeva) | kasutusel: AUC 0,79 (1.2) → 0,90 (1.3.1) |
| Maailm üle 50 MHz | dokumenteeritud juhtumid (14–15/8/2022 Manitoba–Kansas, 144–10 GHz) | ajalooline juhtum | kasutusel varasema juhtumina; valideerimisele lisada QSOd |
| wspr.live (WSPR) | täielik arhiiv, 2 m ja 70 cm, SQL | Paljud üritused ja "ei" (tunnid ilma kohata) | proovitud, tuleks see filtreerida (koha vale 16–20 tuhat km) |
| APRS PropView / APRS-IS | vilditud jaamad, vahemaad, tunnid | ainult valideerimine 144 MHz | teha |
| Tagurpidi tulepaak (arhiiv) | argipäeva | VHF | praeguseks kõrvale heidetud |
| I GRA2 radiosondaggi (Brindisi 16320, Practice of Mare 16245, Trapani 16429, Udine, Milano) | profiilid mõõdetud 00 ja 12 UTC | GFS profiili juhtimine ja inversioonid | Kasutamiseks leitud failid (Brindisi 46 MB) |
| Barsis ja Kirby 1961, Barsis 1963 | Vananemine mõõdetuna mägise takistusega teedel | valideerida kategooria "monte" (Vesuvio, Faito) | teha |
| Norton 1959, NBS TN 15, TN 101 | L(99) ja tabelikandjate kaod | Hajumise kontroll | osaliselt (vaata 300 miili järjest) |

### Allikate soovitatud parandused

| Parandus | Allikad | Valideerimisel mõõdetud mõju | osariik |
| Missi stabiilsus Termin | Oa ja Duttoni kork. 6 | AUC 0,79 → 0,90 koos vajumisega | punktis 1.3.1 (jaotis 16 dB ilma kanalita) |
| Vajumine vertikaalliikumisest | Moler ja Holden 1960 (loe originaalis 22/11: +2,5 dB keskmine õhu saabumise ja õhu tõusu vahel, 1,4 ja 9,4 GHz merel) | ülalt | punktis 1.3.1 (+1,5 / −2 dB) |
| Pehme torujuhe (λ kuni 3·λmax) | Kerr, Hepburn, VHF/UHF DX Book, Shi 2019 | üksi +1% AUC-st; toob kanali värvid 46% sündmuse marsruutidest ja 0% võrdlusest | : |
| Püsivus 24 h | VHF/UHF DX Book, Biancomano ja Leyshon | AUC muutumatu, rohkem võetud ja rohkem valehäireid | 1.3.1 |
| Normaalprofiil CRPL kohalike Ns-idega (−ΔN = 7,32·e^(0,005577·Ns)) 40 N/km fikseeritud asemel | Bean and Thayer 1959, NBSi monograafia 4 | kontrollitud valideerimisel | punktis 1.3.2 alternatiivina välja lülitatud defekti tõttu |
| Ülimurdumislävi 100 asemel 79 N/km (ITU) (uba ja dutton, kiht 1000–850) | P. 453, Lopez 2008, DX Book | kontrollitud valideerimisel | punktis 1.3.2: ettevaatlikkuse kõrval agressiivne lugupidamine (kaart kasutab ettevaatlikkust) |
| Puudujäägist väiksem klimatoloogiline paksus (kalkulatsioon B) | Rogers 1997 | proovida: kasutada A kasu saamiseks | teha |
| Paraboolvõrrand GFS profiilis | PETOOL, lainete levik (Lytaev), Levy (raamatut ei loeta, ainult eelvaade), Lindquist | esimene versioon väljaga üle horisondi, kus domineerib numbriline müra (AUC 0,12–0,28): õige (domeen 8 km, kiire 6°); pärast korrigeerimist AUC 0,54–0,66 | Kasutades teist arvutust, ei otsusta prognoos |
| Evanestsents: ±7 dB üle 100 miili, marginaal 10–20 dB kuni 90–99% | Bray 1961, Antenna Raamat, GEKO 69 | teekünnised piirtingimustes | kasutusel: perioodi ja läve tõenäosus FT8 −18,6 dB tropohajumise korral |
| Bray ühine maht ja ekvivalentne kaugus | IU3QEZ, Bray 1961 | Info | : |
| Troposcatter P.617 ja RSGB kui määramatus | TropoLink, DL2FZN | Info | 1.3.0 |

### Mis jääb mudelitest välja

Teostatud merel madalal kõrgusel (100–200 m), mida GFS tasemed ei näe (praeguste andmeallikate uuring); peegeldus ja difraktsioon lähedalasuvatel mägedel (Vesuvio, Faito): arvutuse ettepanek "via monte". ITM/Longley-Rice jääb statistiliseks viiteks "normaalne päev" (Ns 301, climi), ilma kanaliteta. Autori otsusega visati ära: AirScout, P.528), E sporaadiline (P.2001) ja 50 MHz (servoir EIRP, mida vähesed omavad ja segavad E sporaadilisega).

### Ettepanek töökorralduse kohta

1. Laiendatud valideerimine: lisada filtreeritud WSPR (kaks tundi ilma täpita) ja QST 2022; korrata 432 MHz.
2. Proovige tavalist CRPL-profiili ja ITU läviväärtust 79 N/km, ükshaaval, säilitades ainult selle, mis suurendab AUC-d.
3. GFS-profiilide võrdlus Brindisi ja Mere praktika I GRA2 raadiolainega QSO päevadel.
4. Paraboolvõrrandi uuesti hindamine pärast numbrilist korrigeerimist.

### 21. novembri otsused (versioon 1.3.2)

- Vastu võetud: ettevaatlik hinnang (Bean and Dutton, 100 N/km) ja agressiivne (ITU, 79 N/km) külgnevad kanalid; kanalid, mida otsitakse ainult alla 2500 m (esimesed 1500 m, tõstetud pärast valideerimist); boonus suure laiendatud rõhu ja nõrga tuulega; meremass μ1 P.452ga; jaamade osakaal kanali suhtes ka osalisel kanalil; piirkiht harjade kohal ( IT9GSF).
- Vastu võetud alternatiivina, välja lülitatud defektiga: tavaline CRPL-profiil kohalike N-dega.
- Paraboolvõrrand korrigeeritakse arvuliselt: AUC on 0,12 kuni 0,54 (30 m antenn) ja 0,28 kuni 0,63 (300 m).
- Maa: Copernicus DEM GLO-30 maastikuplaatide "skadi" asemel, mis ranniku lähedal on meres 80–100 m kummitusreljeefid ( IC8TEM allkirjastamine, kontrollitud punkt punktilt Caprile); skadi jääb reserviks.
- Ilmamärgid kaardil koos oma mõjuga: ↓ soodne vajumine (Moler ja Holden 1960, keskmiselt umbes 2,5 dB õhu saabumise ja õhu tõusu vahel; +1,5 dB), ↑ õhk ülesmäge ebasoodne (sügavad seadmed; −2 dB), S lööb soodsa vihje (ARRL käsiraamat § 19.4, tugevad inversioonid Vahemerel; parandus puudub). Kahekordne sümbol, roheline/punane/oranž värv ja koomiks kaardil.
- Vastu võetud ITM 1.2.2 (NTIA/itm, avalik), mis on tõlgitud C#-ks: vastavalt baasjoone arvutusele normaalses atmosfääris P.452 kõrval ja viimasele lõigule pärast väljumist kanalist, mis lõpeb enne korrespondenti. Kradioprediktkood, sest GPL v2 all.

Indeksi jaoks kasuliku sisuta: *Kommunikatsioonid Kvartal*, sügis 1991 (tropo artiklit ei ole; Atkinsi artikkel Scatterist on 1991. aasta talvenumbris).

- Vastu võetud käsiraamatute põhimõttena (21. november): "paljunemist ei saa üldistada, vaid uurida seoses kohaliku morfoloogiaga" ( DF5AI, millest IK3XTV teatas "Propagation on 2 meetrit"; viidatud prognoosi käsiraamatus (§ 1) ja nõukogudes.
- Vastu võetud (21. novembril pärast 1.3.3. aastat) IK3XTV pärastlõunaste Alpide-üleste avade tõlgendus: mitte "konvektiivsed rakud nagu läätsed", vaid kihi ülaosa turbulentsiga tugevdatud troposhajumine, mis aktiveerub uuesti, kui see jõuab ühise mahu osakaaluni (prognooside käsiraamat, § 8 ter). Alates 1.3.4 sisestab see punktarvutuse tropohajumise ajutise tugevdusena, maksimaalselt 3 dB. Kõrvaldatud samade artiklite poolt: Alpide taga olevate signaalide "automaatne võimendus" (passiivne meedium ei võimenda: kirjeldatud lingid jäävad selgitatavaks difraktsiooni ja troposhatteriga; deklareeritud võimsusi ei kasutata andmetena), lumi, mis "parendab difraktsiooni" (tõenäolisemalt stabiilne kiht üle lume, vähem evanssentsi).

- Kerr (1951) arvustas raamatu kohta (22. november, autori esitatud PDF): § 1.5, nt 17 ja 18 ning tabel 1.1. Numbrid eq. 18 skaneerimisel on loetamatud, kuid tabel 1.1 kinnitab: 20 jalga → 1 cm, 80 jalga → 10 cm, 400 jalga → 100 cm, 2000 jalga → 1000 cm, 1,25, 10, 112 ja 1252 cm λmax = 0,014·d^1,5 (λ cm, d seistes; ümmargune tabel, "indikatiivsed väärtused". Kerri hüpoteesid: kanali sees langeb modifitseeritud indeks 4·10−8 jala kohta, madalate kanalite tüüpiline väärtus; nõrgemate gradiendidega on vaja suuremat paksust. Kerr hoiatab ka, et λmax ei ole võrgulõige: kanal võib mõnikord pikemaid lainepikkusi siiski tugevalt mõjutada.

- DL6WU läbi vaadatud originaal (22. november, PDF VHF Communications 3/1982 esitatud autor): G. Hoch, «Extremely Long Yagi Antennas», lk 130 ja uuem. Joonis 1: dipooli võimendus, mõõdetuna sagedusel 432 MHz ja 1296 MHz Saksa Posti professionaalses polügoonis, prooviantenni EIA väärtusel 7,7 dBd, hinnanguline täpsus 0,5 dB; 2,35 dB buumi kahekordse pikkuse korral, ilma küllastuseta kuni umbes 20 λ. Programm kasutas 8.6 + 8.0.log10(L/λ) dBd, st umbes 0,8 dB vähem kui 1–2 λ korral; nüüd kasutab see 9,4 + 7,8·log10(L/λ) dBd (kalle on märgitud, 1 λ väärtus loetakse diagrammil, ± 0,3 dB). Võltsitud valem 10·log10(7,8·L/λ) ringleb võrgus: annab 3 dB kahekordistamiseks, võrreldes mõõdetud 2,35-ga.
- Booker ja Gordon (1950): viidet kinnitab "This Week's Citation Classic" (Current Contents, 8. veebruar 1982, H. G. Bookeri kommentaar): Proc. IRE 38, lk 401–412. Kommentaar kinnitab käsiraamatutes kasutatud kahte punkti: Rayleigh jaotusega horisondi taha saadud väli ja ebakorrapärasuste (mitte Gaussi) eksponentsiaalne autokorrelatsioonifunktsioon. Algset artiklit ei ole veel läbi loetud.

## 13. IK3XTV artiklitelt

Flavio Egano IK3XTV (Radio Rivista: "Propagation on 2 meetrit" ja "Propagation VHF – konvektiivrakud ja gravitatsioonilained") kaks artiklit lisavad kaks asja, mida kanalimudelid ei kata: konvektsioonist tingitud pärastlõunased avaused ning praktilised reeglid selle kohta, kus ja millal kanalid Itaalias moodustuvad. Need on vaatlused jaamast, mitte mõõdud: neid tuleb käsitleda hüpoteesidena, et laikudega kontrollida.

| Autori tähelepanekud | Kuidas see muutub indeksi koostisosaks |
| Kanalid moodustuvad peamiselt vahemikus 100 kuni 1000 m, harva kõrgemal; pakuvad suurt survet laial alal, head insolatsiooni ja tuule puudumist. | kanali otsimine ainult alla 1500 m; boonus suure laiendatud rõhu ja nõrga tuulega madalates kihtides |
| Veneto ja Puglia vahel Aadria merel korduvatel soojadel kuudel 700–800 km, mõnikord kuni Kreekani; mereteed on lemmik | suurem kaal meretee osani (näiteks ITU-R P.452 μ1) |
| Vastav peab olema kanalis või selle lähedal; kõrgete antennide ja mõne meetri kaugusele viiva signaaliga ei sisene signaal | võrrelda kanali baasi osakaalu kahe jaama osakaaluga (teenib QTH kõrgust) |
| Sama aeg, sama kanal: kuulata Gargano jaama 144 ja üks 50 MHz. | Kontrollimiseks: vastavalt Hepburni tabelile nõuavad 50 MHz üle 300 m edumaad, nii et sündmus näitab väga sageli torujuhtme olemasolu |
| Veneto - Lõuna-Saksamaa Alpide taga, ilma visuaalideta, peaaegu alati pärastlõunal ja kõrge rõhuga; ebastabiilsed signaalid, mis mõne minuti pärast kaovad ja ilmuvad uuesti | "konvektiivne" indeks, mis on eraldatud kanalist mägiteede puhul |
| Konvektiivsed rakud sünnivad maapinnal ja ronivad umbes 1 km iga 3 tunni järel: 15-aastaselt jõuavad nad umbes 3 km, kõrgusel, mis teenib Alpide ületamist. | kasutada piirangukihi kõrgust (piir\_layer\_ height of Open- Meteo GFS) mööda teed: skoori, kui see ületab kurrude kõrguse |
| Alpid tekitavad gravitatsioonilaineid, mis koos konvektiivsete liikumistega tekitavad murdumisnäitaja ebakorrapärasusi | Hüpoteesid: tugev tuul, mis on risti ahelaga kuni 700 hPa kuldlainete signaalina; proovida viimast |
| Väga suur inversioon (erandjuhtudel kuni 8 km) annab umbes 800 km, kuid praktikas harva üle 300 km. | inversiooni osakaal laiendab ulatust: kasulikud andmed ootekauguse hindamiseks |
| Suvel võrdub Maa raadius umbes 16000 km (2,5 korda suurem reaalsest), umbes 60 dB vähem kui 300 km kaotusega | kooskõlas k50 = 157/(157 − ΔN): k = 2,5 vastab ΔN-le umbes 94 N/km, st ülirefraktsioon |
| Üle 3000 km ainult 20–30° ulatuses ekvaatorist põhja ja lõuna pool, mere kohal, ühendatud Hadley rakkudega; tipp augustis Aafrikas–Euroopas | väljaspool Euroopat: kasulik mitte anda kõrgeid indekseid Atlandi ookeanil ilma tagasisideta ja Kanaari saarte marsruudil – Suurbritannia |

Muud kasulikud andmed samadest artiklitest:

- Optiline vahemik kahe h1 kuni h2 meetri vahel (d km): d = 4,1 · (√h1 + √h2), kuni umbes 20% normaalse murdumise korral.
- Evansents: aeglane ja madal inversiooniga; tugev ja kiire hajuvate ja konvektiivsete rakkudega. Programm võiks seda kasutada reklaamide klassifitseerimiseks, kui ühel päeval on sellel aja jooksul SNR aruanded.
- Sporaadilise E puhul 144 MHz-s teatab autor DF5AI ja Davise ja Johnsoni (Nature 435, 2005) tööst ajaliste ja Es vahelise sideme kohta: tropost välja, kuid eraldi indikaator homme võiks kasutada välguandmeid.
- Maapinna peegeldus topelthüppes Es: meri K = 81 ja 5 S/m, maastikud ja metsad K = 13 ja 0,006 S/m, linnapiirkonnad K = 3 ja 0,001 S/m.

## 14. LIVETROPOVHF indeksi hüpoteesid

Algne ettepanek, mida tuleb arutada: indeks 0–10 samas mõõtkavas nagu Hepburn, mis arvutatakse võrgu iga punkti ja GFSi iga 3 tunni tagant (siis ECMWF ja ICON võrdluseks) ja teine number marsruudi kohta, mis näitab, kui palju osa marsruudist läheb valitud sagedusalale kasulikusse kanalisse. Allpool toodud kaalud on minu eeldused, mida tuleb kalibreerida täppidega.

Ülevaade pärast Bean ja Dutton ja Kerr: profiili lävendid tulevad nende tabelist 6.5 ja skaala nende mõõtmistest, nii et murdumiskiht on väärt 10–25 dB üle normaalse profiili. Seejärel arvutatakse indeks kõigepealt detsibellides ja seejärel võtab 0–10; selle kääri väärtused tuleb kalibreerida täppidega.

Arvutamine punktide kaupa, samm-sammult

1. T tühjenemine, suhteline niiskus, geopotentsiaalne kõrgus ja vertikaalkiirus 1000, 975, 950, 925, 900, 850, 800, 700, 600 ja 500 hPa juures, pluss T ja 2 m niiskus, maapinna rõhk, kihi kõrgus ja tuul 10 m juures.
2. Maapinnast allpool olevad tasemed kõrvaldatakse; arvutatakse N ja M igal tasandil (ITU-R P.453).
3. Arvutamine −dN/dh igas Beani ja Duttoni neljas kihis (1000–850, 850–700, 700–600, 600–500 hPa) ja diagramm alamrefraktsioonina, normaalsena või ülirefraktsioonina nende tabeli lävenditega; esimese kihi sees kasutatakse ka vahepealseid tasemeid, et leida tugevaim gradient ja selle osa.
4. Kus dM/dh < 0 leian torustiku aluse ja ülemise osa, paksuse d ja puudujäägi ΔM.
5. Kerri valemi põhjal saadud sagedusala miinimumpaksuse võrdlus (umbes 180 m 144 MHz puhul, 89 m 432 MHz puhul, nagu Hepburni tabelis); künnise all loeb kanal veel, kuid vähem.
6. Stimuleerin allpool oleva tabeliga (võtad parima kihi, mitte kihtide summa, pluss anomaalia termin) ja teisendan selle: indeks = dB / 2,5, vahemikus 0 kuni 10, nii et 25 dB on väärt 10. Negatiivset väärtust (alamrefraktsioon) näidatakse 0-na, kuid see jääb teeandmetesse.

| Parima kihi olukord | Kriteeriumid | Eeldatav tulu |
| Normaalne (lamineerimata) | −dN/dh tabeldusvahemikus. 6.5 (nt 20–60 N/km 1000–850 hPa) | 0 |
| Veidi üle normaalse | normaalvahemiku ülemise piiri ja ülimurdumisläve vahel | 0–10 dB võrdeliselt |
| Ülirefraktsioon | üle kihi läve (tõus 100, 80, 70, 50 N/km), kuid alla 157 N/km | 10–15 dB, proportsionaalselt kuni 157 N/km |
| Liiga õhuke torujuhe sagedusala jaoks | dM/dh < 0, paksus allpool Kerri lävi | 15 |
| Kasulik konnektor riba jaoks | dM/dh < 0, paksus Kerri lävest kõrgemal | 20 dB, +1 dB iga 5 M puudujäägiühiku kohta, maksimaalselt 25 |
| Alamrefraktsioon | −dN/dh < 0 alumises kihis | −10 dB |
| Anomaalne taust (lisada) | Mudel Ns miinus ITU kaart klimatoloogiline Selle kuu Ns | 0,2 dB ühiku N kohta, vahemikus −5 kuni +5 dB |

Arvutamine marsruudi järgi (kahe lokaatori vahel): proovi iga 10 km järel maksimaalne ring, nagu juba tee indeksi programm, ja arvuta punktide murdosa kasuliku kanaliga ja aluse keskmine osakaal. CSIRO uuringus leiti, et kasulik on torujuhtmes oleva marsruudi osa (AIS-liinidel 162 MHz oli see 67% kõrgem kui kontrolljuhtudel).

Arvutuste lisamine marsruudi kohta (alates LA0BY ja W3SZ): 1 merejaamad võrreldes kanali aluse ja ülaosaga, sest kõrge kanal teenindab ainult neid, kes sisenevad; 2 troposkatter "põrand" koos Yeh meetodiga, kusjuures lähtenurgad võetakse maapinna profiilist; trope võimendus dB-s eemaldatakse sellest kaotusest ja tulemust võrreldakse kaotusega, mida jaam suudab ületada KK6MC tabel. Seega muutub indeks ka saavutatava vahemaa hinnanguks.

Parandused, et proovida üks korraga (Bean ja Dutton ja LA0BY, IK3XTV, G4MCU juba käsiraamatus), et säilitada ainult siis, kui nad parandavad võrdlust täppidega:

- Segamine (oluline autori sõnul igapäevase vaatlusega): päevast päeva maa peal, keskhommikust õhtuni selge taevaga või tuulega maapinnale üle mõne m/s kaovad kanalid maapinnale; nende panust tuleb vähendada. Mitte merel.
- Päeva kellaaeg (oluline autori sõnul igapäevase vaatluse järgi): irraggiamento kanalid tekivad päikeseloojangul ja langevad pärast päikesetõusu; see on 3-tunnine prognoosikontroll.
- Vertikaalne liikumine: õhk tõuseb 850–700-ni hPa remikseerib kihid (negatiivne korrektsioon), antitsükloni all laskuv õhk tugevdab neid (positiivne).
- Rõhu suundumus: ülesmäge suunatud rõhk rajal viimastel tundidel, kõrgem ja stabiilne signaal; languse korral madalam signaal (bastano 2 hPa).
- Sünonüüm: kõrge rõhk üle 1025 hPa, temperatuur 850 hPa palju kõrgem kui maapinnal, kuum ja kuiv õhk, mis voolab külmemas meres, tee merel.
- Mäeteed: teine "konvektiivne" indeks (osa IK3XTV), mille pärastlõunane piirkiht on kõrgem kui mööda teed ja kõrge rõhk.

VHF/UHF DX Raamat, muud parandused proovida: järjestikused päevad kõrget rõhku punkti (rohkem päevi, rohkem punkte), asend võrreldes antitsükloni keskmega (boonus servades), rõhu gradient maapinnal kui tuule mõõt, tugev vihm 24 tundi enne (penality), madalad pilved kihi servas (penality ainult kõrgete ribade jaoks). Marsruutide suuna jaoks: võrrelda marsruudi asimuuti isobare omaga.

Kontrollimine

- LIVETROPOVHF arhiiv: iga kommertslik üle 500 km kuni 144 või 432 MHz on "sündmus". Selles võrreldakse marsruudi indeksit sündmuste sagedusega samal päeval ja kuul (sama lähenemine CSIRO uuringus).
- Võrdlus Hepburni ja F5LEN -ga samadel päevadel: kui kolm indeksit lahti ühenduvad, näete, kes oli õige vastavalt täppidele.
- Raadiolained 00 ja 12 UTC: kontrollida, kas mudel näeb inversioone, mida sond mõõdab.
- LA0BY ajaloolised juhtumid (6–11/11/2003, 10/12/2004, 15/10/2011, 11/2/2015, 22/11/2018): nendel päevadel ümberarvutatud indeks peab nägema kanaleid, mida Hepburn ei näidanud.
- Manuaalis deklareeritavad piirväärtused: läbitud 100–200 m kõrgusel, peaaegu nähtamatu GFS rõhutasemele; usaldusväärne prognoos 2–3 päeva, siis ainult trend.

## 15. 21. novembri allikad (teine osa)

- ITU-R P.1812 (MaTLAB/Octave implementation of I. Stevanovic, OFCOM; GitHub eeveetza/p1812, Zenodo 10.5281/zenodo.6141558). VHF/UHF marsruudi piirkonnapõhine meetod, mis on tuletatud punktist P.452: delta-Bullingtoni difraktsioon, troposfäärihajumine, kanalid ja peegeldus kihtidel, maapinnal olevad koormatised. Kasulik kolmanda võrdlusena punktiarvutuses P.452 kõrval; ΔN ja N0 andmed on täis ITU tooted, nii et kasutaja laadib need alla (vastavalt P.453-le).
- Costast, Rabanal Ramirez, Dias, IET MAP 2018 (doi 10.1049/iet-map.2017.0992). 563 MHz, 27 punkti vahemikus 5–30 km metsaga ebakorrapärasel maastikul: P.526 12 m taimestikuga −4,3 dB ja σ 8,8 dB; P.1812 10 m koormatusega +2,6 dB ja σ 8,9 dB; dielektriline kiil −4,8 dB ja σ 9,2 dB; P.1546 +5,0 dB ja σ 11,7 dB. Programmi jaoks: hea difraktsioonimeetodi jääkviga on umbes 9 dB ja koormuste kõrgus harjadel (10–12 m) loeb.
- PETOOL (Ozgun e altri, Computer Physics Communications 182, 2011). Paraboolvõrrandi jagunemine-sammuks ühele ja kahele laia nurga levijaga marsruudile (nurgad kuni 40–45°, kitsa 10–15° vastu); põhja kadudega muudetud Fourier' segu kaudu; maast sammudeni. Kinnitab 1.3.2 (lainurk, mulla koefitsiendid) valikud ja näitab järskude reljeefide kahesuunalist arvutust.
- MathWorks. `tropopl`: gaaside kadu ja objektiivi kadu kihilises atmosfääris (P.835, P.676, P.453) ilma kanaliteta: gradiendiga alla −157 N/km geomeetriline optiline mudel ei kehti. Näide radari levimisest murdumiseks −ΔN = 7,32·e^(0,005577·Ns), sama CRPL aruanne juba programmis. `propagationModel` Longley-Rice: eelnevalt määratletud horisontaalse polarisatsiooni väärtused, juhtivus 0,005 S/m, lubatavus 15, reaktiivsus 301, parasvöötme mandriline kliima. Ükski MathWorksi mudel ei tegele kanalitega.
- ITU-R, grupi digitaalsed tooted 3. P.453, P.452, P.1812, P.2001, P.528, P.1546 andmed ja programmid; „integreeritud andmed on osa soovitustest; ei ole deklareeritud ümberjagamislitsentsi: te lubate neid kasutajale alla laadida.
- ESA Navipedia, troposfääriline viivitus. N = Nidrostatica + Numida; kuiv zenitaalne viivitus on umbes 2,3 m ja niiske üks kümneid cm, mis muutub kiiresti. Meie jaoks: GNSSi jaamade hinnanguline märg viivitus on integreeritud niiskuse kaudne mõõdupuu, võimalik tulevane praeguse valamise allikas; Tänapäev ei ole kasutatav.
- Bray, K2LMG, «A Method for Determining V.H.F. Station Capabilities», QST november 1961, lk 36–40 (loetuna originaalis; uuesti kontrollitud 22. novembril PDF-il QST arvu kohta: vastavad kõik allpool esitatud väärtused). Autor D. W. Bray (General Electric, Ithaca) ütleb, et ta on kinnitanud prognoose kuude plaanitud ühendustega K2GOI ja W4LTU -ga. Evanescence jooksul 13,5 dB jooksul umbes 100 miili (7 dB alla ja 7 üle tunni keskmise), peaaegu lineaarne alla (3,5 dB kuni 50 miili): tahke ühenduse eemaldada 7 dB. Horisondi korrigeerimine: horisondi iga kõrgus on 69 miili (111 km) lisakaugusest (vähem, kui negatiivne). Yagi võimsus umbes 10 × buum/λ (võimsuse suhe), rohkem kui 4 dB mullapeegeldust; SSB −3 dB ja AM −7 dB võrreldes CW-ga; 99% tundidest: umbes 180 dB kuni 60 miili, 190 kuni 90, 200 kuni 240 (2 m). Alates 1.3.2 kasutab perioodi tõenäosus 5 dB hinnangulise kasutatud käe asemel σ = 3,4 dB (±7 dB ±2σ).
- Kahesuunaline arvutus (proovi prototüübiga, 21. november). Suure nurga paraboolvõrrand tegeliku S RTM1 maastikuga Caprilt, 144 MHz, antenn 10 m kaugusel maapinnast, standardatmosfäär; mõõt: keskmine võimsus 20–400 m kuni 80 km marsruudil vastava suunas. Capri ja Marina Grande kesklinnast on lääs suletud (takistus +11–21°, Monte Solaro): Faito suunamisega (peaaegu EA5 ja EA6 taga, 150–160° teljest väljas) tõuseb Hispaania suunas käivitatav signaal 10–29 dB võrra; osutades Vesuviusele (110–125° teljest väljas, seejärel arvutusplaanist väljas, ainult soovituslik) 17–25 dB võrra. Anacaprist on lääs vaba ja otsevõit 8:22 dB. Prototüübi 1000–1300 km absoluutsed kaotused ei ole aga usaldusväärsed. EA3 EA5 EA6
- Barsis ja Kirby, NBS Report 6751 (1961), Pikes Peak, 223 km, 100 ja 751 MHz. Mägi on optikas kahe marsruudi ühine terminal: tunni mediaanide jaotus on kahe marsruudi konvolutsioon. Pikaajaline käik (1–99%) umbes 14 dB, võrreldes 26–40 dB troposfäärihajumise teega, millel on võrdne pikkus või nurk; päeva märkimisväärne muutus puudub. Signaal on troposfäärihajumisest tugevam, kuid ideaalsest servast nõrgem: ülaosa tuleks käsitleda ümarana (umbes 11,5 km). Kõrgeid signaale täheldatakse alati selja taga.
- Barsis, NBS Report 7652 (1963), kolm marsruuti üle mäe (Colorado, Alaska, Euroopa), 750–900 MHz. Keskmine kadu 10–20 dB suurem kui Fresnel-Kirchhoffi arvutus ilma terminalide lähedal asuva maata; evanssentsi hinnatakse kahe joone mähkimisega optikasse; pinnase peegeldused terminalide lähedal halvendavad pikaajalist hääbumist. Punkti 1.3.2 kandega kanalisse lisatakse P.526 ümardamine.
- Barsis ja Johnson, NBS TN 88 (1961). Kosmoselaine pikaajaline lahustumine, isegi teel mäest kaugemale (§ 5).
- Norton, «Raadiolaine levimise süsteemikadu», J. Res. NBS 63D (1959). Kõverused, millest Bray saab nomogrammid; süsteemikao, ülekande ja põhilise protsessi antenni võimenduse definitsioonid.
- Riis, Longley, Norton ja Barsis, NBS TN 101 (1965–67), 1. ja 2. köide. üks serv ja takistus, mis on ümardatud peegeldustega ja ilma (punkt 7, Pikes Peak'i näitel), pikaajaline varieeruvus V(0,5) ja Y(q) kliima puhul (kontinentaalne karastus ja muud). Meie jaoks: ümarate takistuste ja varieeruvuse funktsioonide meetod on viide kalibreerimismääramatusele.
- ITU-R P.526-16 (11/2025). J(ν) valem (31), mis on identne programmi omaga; ümardatud takistus (32)–(36), nüüd kasutatud; mitmeosalised meetodid (Bullingtoni ja täismeetodi § 4.5).
- ITU-R P.1812-8 (2025) ja -7. Kokkusaadud terviktekst: spetsiaalne meetod 30 MHz kuni 6 GHz (delta-Bullington, troposcatter, kanalid ja peegeldus kihtidel, koormatis). -7 hoiatab, et tema tropohajuja (alates P.617-5) eksib alla 66 km. See on võimalik kolmas võrdlus.
- Paulus, „Praktical application of the IREPS evaporation model, NOSC TR 966 (1984) ja Radio Science 20 (1985). Aruanne loetud 22/11/2026 (DTIC ADA146528, skaneerimine tunnustatud tekstiga, mõnikord loetamatu). Laevade mõõdetud õhu ja mere vahelised erinevused nihkuvad stabiilsuse suunas: soojem mereõhk 31–35% laevaandmetes võrreldes 0,7–7% NOAA metssigadega, laeva poolt häiritud psikromeeter ja jahutusveega võetud SST (ligikaudu +1 °C). Jeske mudel annab siis 40 m ja rohkem kui 15% aastast ja kõrgused, mis hüppavad stabiilse ajaga 24 tunni jooksul 10-lt 40-le, mida ei kinnita raadioseadmed. Kavandatud korrektsioon (katsetatud künnised 0, −0,5 ja −1 °C; −1 °C on paremini kooskõlas 1982. aasta juuni raadiomõõtudega): kui õhu ja mere erinevus on −1 °C või väiksem, kasutatakse mõõdetud; vastasel juhul arvutate kõrguse −1 °C ja 0 °C juures ning kui see on −1 °C juures madalam, siis hoiate seda. Stabiilse poole tõusud vähenevad, langus püsib. Juunis Marsden 120 ruut: keskmine kõrgus 16,8 m (klimatoloogia) kuni 7,6 m. Mudeli võrrandeid aruandes ei ole (viide Jeskele ja Hitneyle). Programm kasutab neutraalset profiili, millel ei ole stabiilset liialdust, ja piirab δ kuni 40 m: see läheb samas suunas; SST mudelitega (mitte laevaga) kaalub mõõtmisviga, mida Paulus parandab, vähem.
- Levy, «Parabolic Equation Methods for Electromagnetic Wave Propagation» (IEE 2000). Google Libris on ainult eelvaade: Ma ei loe.
- VK3FS (3fs.net.au). Kvaliteetne sissejuhatus: kõrged kanalid 450–2000 m kõrgusel pikemate avade jaoks maapinnal; 300–600 m inversioonid rahulikel ja rahulikel öödel, mis kaovad pärast päikesetõusu; tugevamad ranniku inversioonid pärastlõunal; 800–1300 km tüüpiline, 1400–1600 km sageli läbi viidud. See on kooskõlas päeva/öö hoiatuste ja laega 2500 m.

## 16. Allikad

Eeskirjad ja valemid

- [ITU-R P.453-13 (2017), raadio murdumisnäitaja:](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)valem ja andmed
- [ITU-R P.452-18 (2023), maapealsete jaamade vaheliste häirete prognoos üle 100 MHz: ekvivalentraadius, β0, tropohajumine,](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)kanalid
- [ITU-R P.617-5, raadiosildade levik horisondi taha](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)
- [Shi et al.] Põhjalik uuring laine elektromagnetilise levimise maksimaalse pikkuse kohta erinevates aurustuskanalites, IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

Ilmamudelitega kanalite prognoosimise uuringud

- [P. Lopez, "5-aastane 40-kilomeetrine resolutsioon superrefraktsiooni globaalne klimatoloogia maapealse meteoroloogia radari jaoks", ECMWF Tech. Memo 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO), "Tropospheric Ducting Prediction based on GFS Model Data at... the CSIRO Murchison Radio-astronomy", Observatory arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (FMI), "Effects of model resolution on predicting atmosphericducting conditions with Harmonie-AROME", EGUsphere 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

Amatöörraadio prognoosid

- [W. R. Hepburn, Hepburn Tropo Index (HTI)]](https://www.dxinfocentre.com/propagation/hti.htm)ja [](https://www.dxinfocentre.com/propagation/history.htm)meetod ajalugu
- W. R. Hepburn, "Lowest Kasutatav Sagedus vs. Ducting Layer paksus"](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, "Worldwide Refractive Index forecasts"](https://tropo.f5len.org/WW/info.php), [uus versioon 2026](https://tropo.f5len.org/?p=504), [märkus mudeli ARPEGE](https://tropo.f5len.org/author/f5len/)kohta]

Andmed

- [Open-Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api), [märkus rõhutasemete](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)kohta]
- [NOAA NCEI, Integrated Global Radiosonde Archive (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)]
- [NCAR RAOBget, Wyomingi ülikooli raadio üleujutuste skript](https://github.com/NCAR/RAOBget)]

Klassikalised tekstid (märgitud programmi autori poolt, mida tuleb lõplike valemite jaoks konsulteerida)

- B. R. Bean, E. J. Dutton, *Radio Meteorology*, NBS Monograph 92, 1966, 435 lk (reprint Dover 1968): Vertikaalsed profiilid N ja M, gradient of trap under −157 N/km, võrdlus raadiolainega; peatükk 5 "Sünoptiline raadiometeoroloogia". Alus 2–4 arvutusastmele punkti kohta. Tasuta: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [NIST card, doi] 10.6028/NBS.MONO.92](https://www.nist.gov/node/1770911), [Internetiarhiiv](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf)koopia].
- L. Boithias, *Radio Wave Propagation*, McGraw-Hill 1987 (tõlkinud D. Beeson prantsuse keelest, 330 lk, ISBN 0070064334; ka Põhja-Oxford Academic): mehhanismid väljaspool horisonti, ühine hajumise maht ja ühendusantenni-pooli kadu. Ainuüksi paber ([teegikaart](https://libcat.library.tamu.edu/Record/in00000014812/Description)]).
- M. P. M. Hall, *Effects of the Troposphere on Radio Communication*, IEE Electromagnetic Waves Series n. 8, Peter Peregrinus 1979, 206 lk (ISBN 0906048257; brossura 0863410863): sünoptilised olukorrad ja levik VHF/UHF. Sünoptiliste koostisosade alus. Kauplemine IET-is ([](https://libcat.library.tamu.edu/Record/in00000636385)-kaart]).
- H. G. Booker, W. E. Gordon, "Raadio hajumise teooria troposfääris", *Proceedings of the IRE*, vol 38, n. 4, aprill 1950, lk 401–412: Skapsi teooria murdumisnäitaja disogeensusest. Keskne idee: hajutatud võimsus sõltub murdumisnäitaja turbulentsete kõikumiste intensiivsusest ja ulatusest (eksponentsiaalne korrelatsioon); horisondi lähedal valitseb difraktsioon, edasine hajumine; Autorite sõnul on mõõdetud väljade selgitamiseks piisav turbulentsi ulatus ja meteoroloogia oodatavad indeksi variatsioonid. Viidatud 328 artikli poolt (Scilit), vaba juurdepääsuga koopiat ei ole. Kaart Semantic Scholar ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); täistekst IEEE Xplore makstud ([doi 10.1109/JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78))).
- C. M. Crain, J. R. Gerhardt (Electrical Engineering Research Laboratory, University of Texas), "Measurements of the Parameters In involved in the Theory of Radio Scattering in the Troposphere", *Proceedings of the IRE*, vol. 40, n. 1, jaanuar 1952, lk 50–54, doi 10.1109/JRPROC.1952.2739 annavad tegelikud väärtused, mida Booker ja Gordon olid ainult hinnanud ([Skilit](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)vahekaart]).
- "The Constants in the Equation for Atmospheric Refractive Index at Radio Frequencies", *Proceedings of the IRE*, vol. 41, n. 8, august 1953, lk 1035–1037: teeb ettepaneku vaadata üle tollal kasutusel olevad voolud (79 K/mb ja 4800 K) valemis N; siit pärinevad valemi ITU väärtused 77,6 ja 3,73 · 10^5 (Q)](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E. C. S. Megaw, "Fundamental radio scatter propagation theory", *Proceedings of the IEE Part C: Monographs*, vol. 104, n. 6, september 1957, pp. 441–455, doi 10.1049/pi-c.1957.0052: spekter of index fluctuations from turbulence theory, full track of disperstter beyond the common horizon and effect of geometry paid at IET ([Shilit](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)card).
- * IRE protseduurid *, oktoober 1955, erinumber hajumise leviku kohta (K. A. Norton ja J. B. Wiesner), Bullington, Staras ja teised: tasuta PDF-is [World Radio History](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf).
- D. E. Kerr (toimetanud), *Propagation of Short Radio Waves*, MIT Radiation Laboratory Series vol. 13, McGraw-Hill 1951 (aruanded Dover 1965 ja IEE 1987): aurustumise ja pinnakanalite teooria merel. Digitaalne koopia: [World Radio History](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); IET edition in [IET Digital Library](https://shop.theiet.org/propag-short-radio-waves)].

Hiljutised meetmed tropoferaalse difusiooni kohta VHF-is: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Austraalia), "A Study of VHF Troposcatter Communication Channels in South Australia", in *Digital Signal Processing for Communication Systems*, Springer 1997, lk 103–112 ([Springer](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)). Tekst tasu eest; abstraktselt puudutab see difusiooni, mitte kanaleid. Kasulik ainult siis, kui soovid kalibreerida troposkatteri tähtaega VHF kanaliandmetega.

Arvmudelid ja mereteed (näidatud programmi autori poolt)

- Härra Levy, *Paraboolne võrrand Meetodid elektromagnetilise laine levimiseks*, IEE Elektromagnetilised lained Series #45, 2000, 336 lk (ISBN 0852967640): paraboolne võrrand mööda teed muutuvate murdumisprofiilide ja orograafia jaoks; peatükid troposfääri levimise, kiirte ja viiside kohta, levik merel. See oleks mõeldud väljaarvutamiseks piki teed, väljaspool indeksit. Maksmiseks ka elektrooniliselt IET-ilt ([IET-kaart](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)]).
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (Leicesteri Ülikool): 2 GHz mõõtmised Kanalisaartel ja Kanalisaartel. Paljud tööd ja väitekirjad on ülikooli arhiivis tasuta: [pikaajaline statistika aurustumise kanali kohta, Raadioteadus 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434), [signaali muutused kolmel mereteel, Raadioteadus 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710), [N. Mufti 2012 pealkirjad marsruutidega 50 ja 140 km ka VHF](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854), [tesis of I. Alam 2002 reaktsioonivõime arvutatud ilmaandmetest, 240 MHz ja 2 GHz X3 UQ, [tesi.].](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus, "Practical application of an evaporation model", Radio Science vol. 20 n. 4, 1985, lk 887 (not IEEE Transactions on Antennas and Propagation), doi 10.1029/RS020i004p00887 ([scheda](https://zendy.io/title/10.1029/rs020i004p00887)): evaporatsioonikanali kõrgus pinnailma andmetest. Tasuta sellele eelnev tehniline aruanne: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf)].

T. Lindquist, *Wave Propagation Models in the Troposphere for Long-Range UHF / SF Radio Connections *, magistritöö, Karlstadi Ülikool, 2. juuni 2020, 106 lehekülge (pakkunud programmi autor). See on tasuta samm-sammuline juhend, et kirjutada arvutus paraboolvõrrandiga: split-step lahus (isegi laia nurga all), maa ja mere piirtingimused, astmeline maa, kiire jälgimine lineaarsetel profiilidel kohati numbriliste kontrollidega; tsiteerib PETOOLi vaba tööriista MATLAB. Kaks kasulikku teavet korraga: (1) N tundlikkus ilmastikumuutujatele, δN ≈ 0,26 δP + 4,3 δe − 1,4 δT (hPa ja K, umbes 20 °C), st 1 hPa viga aururõhul kaalub kolm korda 1 °C vea temperatuurile: on mudeli niiskus, mida tuleb kõigepealt kontrollida; (2) aurustamiskanali keskmine kõrgus: 5–6 m Põhjameres, 13–14 m Vahemeres, üle 20 m Pärsia lahes, alati liiga madal 144 ja 432 MHz jaoks. Autori järeldus (sagedused 1 kuni 10 GHz): väljaspool horisonti nõuavad usaldusväärsed ühendused peaaegu alati kanalit.

Paljundustarkvara mullaprofiilil (horisondi ja takistuste jaoks, mitte oodatava tropo jaoks)

- [Radio Mobile](https://ve2dbe.com/rme.html)Roger Coudé VE2DBE: tasuta, Windows (ka Linux ja Mac veiniga), Longley-Rice / ITM mudel, maastikuprofiil, mis on kvoodiandmetest automaatselt välja võetud ([<load](https://ve2dbe.com/download/download.html)]).
- [Splat]](https://qsl.net/kd2bd/splat.html)John Magliacane KD2BD: GPL, 20 MHz–20 GHz, Longley-Rice ja ITWOM v3.0 mudelid, takistuste ja kadude aruanded tekstis; uusim versioon 1.4.2 2014.
- [ITM, NTIA/ITS](https://its.ntia.gov/software/itm)ametlik kood: Longley-Rice mudel (20 MHz–20 GHz) koos allika ja dokumentatsiooniga, Radio Mobile'i ja SPLATi baas!. Kood on tasuta ja võite lisada arvutusele LIVETROPOVHF-i tee (pinnasest tingitud kahju, mis lisatakse tropo indeksile). Algaruanne: A. G. Longley, P. L. Rice, „Prediction of Tropospheric Radio Transmission Loss Over Irregular Terrain: A Computer Method – 1968, ESSA Technical Report ERL 79-ITS 67, juuli 1968 (NTIA kaart ajaloolise tarkvara kohta: X1) https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITU-R, tarkvara ja digitaalsed andmed](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): vabad kaardid aasta mediaanide ΔN ja N0 kohta ruutvõrgus 1,5°, mida kasutavad P.452 ja P.617 – kasulikud indeksi kliima võrdlusväärtustena.
- Pathloss (Lepingu telekommunikatsioonitehnika): raadiosildade kommertsprogramm; Ma ei ole leidnud avalikku allikat, mis kinnitaks, milliseid ITU soovitusi rakendatakse, nii et kontrollida.

Korduvkasutatav GitHubi kood LIVETROPOVHF-is

| Projekt | Mida ta teeb | Keel | Kasutamine on meile võimalik |
| [NTIA/ITM](https://github.com/NTIA/itm)] | NTIA mudel Longley-Rice'i ametnik koos pakenditega. NET |  C++, C#  | Maa kaotus teel: integreerub otse programmi, mis on C # |
| [fuzzzeee/itm](https://github.com/fuzzzeee/itm)] | ITM C++ kood C# tõlge |  C#  | alternatiiv ilma kohalike teekideta, kasulik ka Windows 7 versiooni jaoks |
| [eeveetza/ Py452](https://github.com/eeveetza/py452)] | ITU-R P.452, identne MATLAB/Octave versiooniga, mille on heaks kiitnud ITU-R WP 3M; MATLAB versioon, mille on välja andnud Šveitsi Föderaalne Sideamet | Python, MATLAB | viide tõlkida C# troposfäärihajumise valemid ja läbi viidud ITU valideerimisnäidetega |
| [bwinkel/pycraf](https://github.com/bwinkel/pycraf)] | P.452 täielik, P.676, SRTM profiilid; GPL v3, kuid lisatud ITU andmefailid ei ole kaubanduslikuks kasutamiseks vabad | ♪ | tulemuste kontroll |
| [ITU-Rpy](https://github.com/inigodelportillo/ITU-Rpy)] | paljud ITU-Ri soovitused, sealhulgas P.453-13, kinnitatud ITU näidetega | ♪ | P.453 valemi N ja kliimakaartide kontrollimine |
| [NCAR/RAOBget](https://github.com/NCAR/RAOBget)] | Wyomingi ülikooli raadiolaine allalaadimine tekstivormingus | ♪ | Itaalia jaamade profiilide allalaadimine C#-s |

Märkus: P.453 praegune versioon on [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)]

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), tasuta allalaaditav; dokumendis kasutasin 13, et olla ümberjoonistatud.

Juba programmi käsiraamatutes ja taaskasutatavad siin: LA0BY (Nordic VUSHF 2019), IK3XTV ("Propagation on 2 meetrit" ja "Propagation VHF – konvektiivrakud ja gravitatsioonilained", Raadio Magazine, esitatud programmi autor), DF5AI/DK5YA (2003), G3YGF (QST 1983), The VHF / UHF DX Book cap. 2, G4MCU (2003) KP4MD
