LIVETROPOVHF · techninis dokumentas 2
# Žemėlapio sudarymas

> Mašininis vertimas iš italų kalbos: kilus abejonių, galioja itališkas vadovas.

Kaip iš skirtukų Žemėlapis ir vietoje žemėlapis gimsta: geografinė bazė, projekcijos, Hepburn ir F5LEN indeksų vidurkis, izobare, dėmės ir stotys klausosi, apkrautas sluoksniais. 2026 m. lapkričio 9 d. redakcija.

## Apibendrinant

- Žemėlapis yra sukurtas pagal aštuonių sluoksnių programą, kiekvienas su savo [8] langelį.
- Pagrindas: Gamta Žemės pakrantė ir sienos dviem lygiais [9] išsamiai; [10] Rinkos ir azimuto projekcijos.
- Fizika: inversijos, reaktyvumas N ir gradientas dN / dh, švino slenkstis - 157 N / km [3] [4] [7] [11].
- Gerklės indeksas: kalibruotas Hepburn [1] ir F5LEN [2] žemėlapių, bendro tinklelio ir dviejų šaltinių vidurkio skaitymas.
- Isobare: GFS [12] slėgis per Open- Meteo [15], linijos kas 4 hPa, H ir L centrai.
- Spot, klausymo stotys (tamsiai rožinės spalvos apskritimai su visada matomos rožinės linijos) ir asmeniniai dekoracijos raudona.
- Skaitant sutapimą: aukštas slėgis, aukšti ortakiai, žemas slėgis, augantis SNR, [3] [4] [5] [13] [14] kalnų maršrutai.

## 1. Principas: sluoksniuotas žemėlapis

Žemėlapis nėra fiksuotas vaizdas: yra sukurtas pagal programą, sluoksnis virš sluoksnio, kai jūs pakeisti priartinimą, projekcija, juosta, prognozavimo laikas ar tema. Kiekvienas sluoksnis turi savo dėžutę viršutiniame sluoksnyje žemėlapyje, todėl jūs galite tik laikyti tai, ko jums reikia ir pamatyti viską kartu, kai norite visą vaizdą. Užsakymas, iš apačios, yra toks:

|  #  | Keliai | Kiekis | Casella |
|  1  | Pagrindinis žemėlapis | Jūros, žemės, pakrantės ir sienos | Taip |
|  2  | Rodyklės tropas | Media Hepburn + F5LEN arba tik vienas šaltinis; reguliuojamas skaidrumas | Taip |
|  3  | Gaudyklė | Sritys, kvadratai ir, su priartinimu, kvadratu | Taip |
|  4  | Isobare | Slėgio linijos kas 4 hPa, centrai H ir L | Taip |
|  4a  | Temperatūra 850 hPa | Izoterminis apdorojimas kas 4 ° C | Taip |
|  4b  | Šiluminė inversija | Oranžinis bandinys, kai aukštis yra šiltesnis nei ant žemės | Taip |
|  5  | Comment | Gaunančiosios stotys PSKReporter (tamsiai rausvos spalvos apskritimai) ir, rožinės spalvos, ką jie girdi | Taip |
|  6  | Atsisakiau manęs | Decoding stotys su WSJT-X, JTDX arba MSHV: Raudonos linijos iš savo QTH | Taip |
|  6a  | QSO desunti dal live | Uždari sujungimai ( RR73, 73, RRR) skaityti savo dekoderius: žalia linija tarp dviejų stočių | Taip |
|  7  | Name | Jungtys: visiškai mėlyna linija, geltona padengta airscatter, du kamuoliukai | Taip |
|  7a  | Išimtys | Casella Išskyrus atvejus, kai atstumas mažesnis nei 100 km: trumpiausias eismas nėra nei vykdomas, nei nurodytas sąraše. Casella Daugiau kaip 1000 km: tik kvadratai (pradžia): jei viena iš stočių yra daugiau nei 1000 km nuo jūsų QTH, jokios linijos ar apskritimai, tik lokatoriaus kvadratų stotyse, dalyvaujančių lilac apdorotas 45 ° | Taip |
|  8  | QTH ir etiketės | Jūsų stotis, pasirinktos vietos pavadinimai | visada matomas |

Tropo indeksas yra iš karto virš pagrindo ir žemiau visų kitų: yra spalvotas mėginių ėmimas, ir aukščiau būtų apimti linijas ir simbolius. Tinklelis eina per jį, todėl jūs visada žinote, kur spalvotas plotas patenka. Izobares yra plonos linijos ir yra gerai skaityti per mėginių ėmimą.

## 2. Pagrindinis žemėlapis

Sąnaudos, sienos ir žemės yra iš natūralių Žemės vektoriaus duomenų, viešojo domeno [9], iki mastelio 1: 50 mln bendras vaizdas ir 1: 10 mln, kai padidėjo. Failai yra programos aplanke: nėra internetinių žemėlapių paslaugų. Spalvos tema Clear: šviesiai mėlyna jūra # E2ECF8, tamsiai mėlyna pakrantė, šviesiai pilka sienų. Themes Black and Night pagrindai spalvos yra patamsėjęs, kitų temų jie šiek tiek dažyti.

## 3. Abi prognozės

Visi sluoksniai pereina per tą pačią projekcijos funkciją, todėl jie visada yra suderinti vienas su kitu. Jūs pasirenkate iš dviejų [10] projekcijų (λ ilgumos, Δ platumos, R ray of the Earth):

Rinka. Laikyti vietinius kampus ir formas; Atstumas driekiasi link stulpų. Pjaustoma iki ± 85 ° platumos.

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

Azimutale equidtanti centre ant savo aikštės. Centras (φ0, λ0) yra jūsų WWL aikštės centras. Iš centro matuojami atstumai ir kryptys yra teisingi: 1200 km stotelė yra 1200 km skalėje, o kampas su šiaurėje yra azimuto, į kurį nustatyti anteną. Kai pakeisite lokatorių, žemėlapis perorientuoja.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

Be azimutale linijos, kad ant popieriaus yra tiesus (meridianai, paraleliai, kvadratinių kraštų) tampa lenktas. Dėl šios priežasties visos eilutės yra suskirstytos į trumpus bruožus, kiekviena dalis yra projektuojama ir tada bruožai yra suvienyti; tas pats pasakytina apie tropo indekso kraštus ir izobarus. Taškas į centro antipodes neturi konkrečios pozicijos ir lieka iš akių.

## 4. Grilio lokatorius, didinimas ir judėjimas

Maidenhead tinklelis yra sukurtas pagal programą: laukai 20 ° × 10 ° su dviejų didelių ir išblukusių raidžių centre, kvadratų 2 ° × 1, ir su pakankamu mastelio subquarrati 5 ′ × 2,5 ′. Linijos yra mėlynos ir plonos, ne padengti tropo indeksas.

Mastelis su rato centre ant rodyklės (taškas po pele lieka pastovus), juda tempimas, du kartus spustelėkite grįžti į visą vaizdą, + ir − klavišus, ir raktas su procentais už visą vaizdą. Detalės tinka priartinimui: plonesnės pakrantės, subkvadratai, storesnės izobare etiketės.

## 5. Kodėl inversijos lenkimo bangos

Žemoje atmosferoje temperatūra paprastai krenta apie 6,5 ° C / km; lūžio rodiklis priklauso tiek nuo temperatūros ir [3] drėgmės, ir kur ji smarkiai keičiasi su aukščio bangų lenkia į žemę ir patenka daugiau nei įprasta. Svarbiausias sluoksnis yra tai, kad iki maždaug 3 km, kur yra suformuotos inversijos, kanalai ir konvekcinės ląstelės [4]; jūros takai yra malonus, nes vandens masės sudaro didelius plotus [4] inversija. [3]

Dydis, naudojamas jam matuoti, yra lūžis N, t.y. (n − 1) · 106 [11]:

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

T yra temperatūra kelvino, P slėgis ir garų slėgis, tiek hPa.

Skaičiavimai kaip įvairi N su akcija (dN / dh). Standartinėje atmosferoje yra apytiksliai -39 N / km; iki -79 N / km lūžis laikomas normaliu, be to, lūžis yra labai refrakcinis, ir su teigiamais sublūžio [7] nuolydžiais. Žemiau -157 N / km banga yra sulenkta daugiau nei žemės kreivumas ir lieka įstrigę: tai yra [11] (vamzdžių) ortakio. Tai sąlygos, kurias Hepburn ir F5LEN žemėlapiai bando prognozuoti.

## 6. Tropo indeksas: nuo dviejų žemėlapių iki vidurkio

Hepburn ir F5LEN skirtukai yra valdymo stalas: parodyti iš kiekvieno šaltinio puslapyje atsisiųstus vaizdus, su keičiamu prognozavimo metu, ir tarnauti kalibruoti skaityti paspaudę ant atskaitos taškų ir spalvų legenda. Tropo kontrolinis sluoksnis lieka tuščias, kol bent vienas iš dviejų kalibravimų nėra aktyvus ir užbaigtas.

Hepburnas. Hepburn indeksas matuoja, kiek bangų galima nulenkti tam tikrame plote tiesine skale nuo 0 iki 10, kuri taip pat gali viršyti 10. 0 reiškia standartinę atmosferą ir tik sklaida; 1-2 ribinis arba silpnas superrefrakcijos; 3-4 superrefrakcijos su ortakių srityse; Indekse neatsižvelgiama į sklaidos tropo ir nemato [1] kalnai. Žemėlapis apima 6 dienas: 3 valandos pravažiavimai per pirmąsias 36 valandas, tada 6 valandos, atnaujinama kiekvieną dieną 18 UTC [1]; pradiniai duomenys (temperatūra, drėgmė, slėgis) ateina iš pasaulinio modelio Kanados oro paslaugų [1]. [1]

F5LEN. Europai F5LEN leidžia 31 žemėlapį nuo + 3 iki + 192 valandų (kas 3 valandas iki + 48, kas 6 iki + 96, tada kas 12), su data ir laiku, galiojančiu pavadinime, gamybos laikas apačioje dešinėje ir klasės legenda: Disturb, Normalus, Mažas, Silpnas, Geras, Stiprus, Vy Stiprus, Duct [2]. Pasaulinė prognozė F5LEN yra apskaičiuojama pagal oro refrakcijos indeksą N vienete, 40 žingsnių 3 valandas (apie 5 dienas), atnaujintas 4 kartus per dieną; ant žemėlapio paspaudimas rodo vidutinę vertę ir to taško [2] lokatorius.

Procesas. Kiekvienam prognozės žingsniui programa atlieka šiuos žingsnius:

1. Išleidimas. Šių dviejų prognozių atvaizdai yra atsisiųsti talpyklos šalia vykdomojo failo, kartą atnaujinti šaltinį, o ne bet perprojektuoti. Prieš galutinę versiją reikia paprašyti leidimo Hepburn ir F5LEN.
2. Automatinis geografinių nuorodų susiejimas. Programa nustato rėmelį (dvi horizontalios ir dvi vertikalios tos pačios spalvos linijos) ir, Windows 10 / 11, skaito su OCR integruota į sistemą ašių etiketes (54N, 10W, 0, 5E...). Pjaukite juosteles aplink rėmelį, padidinkite jas, išbandykite abu ženklus, kai raidė N / S / E / W neskaito ir nepalieka linijos, ant kurios patenka dauguma etikečių: neteisingi rodmenys išlieka. Iš čia galite gauti ekstremalių lot / lon rėmo ir projekcijos tipas (reguliariai grilis arba Mercator). Ekstremalus išsaugoti save ir perskaityti save, jei rėmo keičiasi dydis. Mac ir Linux, be OCR, gamyklos galūnes arba ranka rašytus galioja skirtuką. Arba žemėlapiai, kuriuose nėra įskaitomų ašių: Kiekvienas žemėlapio tipas (Hepburn Europa, F5LEN visame pasaulyje...) turi kalibravimą: kai kurie valdymo taškai su žinomomis koordinatėmis (meridianų ir lygiagrečių kryžiai, atpažįstami šonkauliai) prie to šaltinio naudojamos projekcijos jungia pikselius su platuma ir ilguma. Kalibravimas yra failo šalia programos ir jūs galite perdaryti, jei šaltinis keičia formatą.
3. Spalvų skaitymas. Kiekvienas pikselis lyginamas su šaltinio legenda ir paverčiamas į artimiausią skaitinę vertę. Sąnaudos, raštai ir tinkleliai, sudaryti iš pradinio žemėlapio yra pripažįstami kaip "nėra duomenų" ir užpildyti šalia verčių.
4. Pasaulinis tinklas. Kiekvienas vaizdas, kai kadras randamas ir žinomas jo kraštutinumai, yra dedamas į Žemę; vertė bet kurioje pasaulio vietoje yra gaunama iš vaizdų, kurie jį dengia. Jei daugiau regionų užima tą patį tašką, verta smulkiau apibūdinti (daugiau taškų vienam laipsniui), su sąlyga, kad jo laikas yra per 1,5 h nuo to pasirinkimo; tada artimiausia valanda, tada naujausias lenktynes. Jei nėra vaizdo, žvaigždėlapyje yra pilka uždanga: nėra duomenų, kurie skirtųsi nuo rodyklės 0. Viduje rėmo, pikselis be legendos spalvos yra 0 (normali atmosfera); pakrantės, sienos ir rašiniai šoktelėjo žiūri į artimiausią spalvą per 3 pikselių.
5. Tos pačios kopėčios. Hepburn jau ant skalės 0- 10 [1]: tamsus fonas = 0 ir dešimt spalvų tvarka (violetinė, mėlyna, vandens žalia, kalkės, geltona, ocher, oranžinė, raudona, rožinė, šviesiai rožinė) už 1... 10, lilac už. F5LEN naudoja klases (Disturb, Normalus, Mažas, Silpnas, Geras, Stiprus, Vy Stiprus, Duct) [2], bet kai kurie turi tiksliai Hepburn spalvų: Silpnumas = 1, Stiprus = 5, Vy Stiprus = 7, Duct = 8; Mažas yra 0,5 ir geras (žalia) 3.5. Skalės daugiklis, į F5LEN skirtuką, pataiso rinkinį, kad jį būtų galima palyginti su Hepburn.
6. Tuo pačiu metu. Laiko juosta eina nuo + 0 iki + 192 h 3 valandų žingsniais. Kiekvienam žingsniui paimate iš kiekvieno šaltinio artimiausią prognozę; jei šaltinis tokio žingsnio neturi, žiniasklaida naudoja tik kitą, o legenda tai nurodo.
7. Žiniasklaida. Su žiniasklaidos mygtuką kiekvienas taškas yra verta (Hepburn + F5LEN) / 2; su Hepburn arba F5LEN matote tik vieną šaltinį, naudinga suprasti, kur dvi prognozės nesutaria.
8. Braižymas. Vertė yra nuspalvinta legendine skale (0 mėlyna, 2 žalia, 4 geltona, 6 oranžinė, 8 raudona, 10 violetinė) ir projektuojama kaip mėginių ėmimas po tinkleliu. 0 yra skaidrus, todėl ten, kur nieko neįvyksta, matote švarų pagrindinį žemėlapį; bendras skaidrumas reguliuojamas žymekliu šalia dėžutės.

Vietoje skirtukas, indeksas palei maršrutą yra tinklo vertės čempionatų kas 10 km maksimalaus apskritimo tarp dviejų stočių vidurkis.

## 7. Isobare

Aukštas stabilus slėgis yra pirmasis signalas, kurio reikia ieškoti [3] trope, ir jungtys už IK3XTV analizuojamų Alpių taip pat buvo didelis [4] slėgis.

1. Duomenys. Slėgis jūros lygyje iš pasaulinio modelio GFS NOAA [12], skaityti per Free Open- Meteo [15] paslaugą 3 ° × 3 ° tinklelyje (nuo 30 ° iki 72 ° N, nuo 30 ° vakarų ilgumos iki 48 ° rytų ilgumos), per kitas 78 valandas. Programa atnaujina paleidžiant duomenis ir tada kas 6 valandas, kaip modelis lenktynių.
2. Linijos. Izobaras yra gaunamas iš tinklelio kas 4 hPa (... 1012, 1016, 1020...) su žygio kvadratų algoritmas (žygiai kvadratų), tada jie yra šiek tiek suapvalinti ir projektuojamas sudarytas pagal takų.
3. Centrai. Labiausiai pažymėtos vietos maksimumai ir minimumai yra pažymėti H (mėlyna) ir L (raudona) ir vertė hPa.
4. Etiketės. Vertė pasirodo palei kiekvieną isobara, ant spalvoto fono žemėlapyje likti įskaitomas virš bandinio.

Plotas ir laiptelis. Duomenys atsisiunčiami apie tai, į ką žiūrite, padidinami 25%, o žingsnis nuo 1 iki 20 ° pasirenkamas, kad liktų iki 400 taškų (Open- Meteo skaičiuoja kiekvieną tašką kaip prašymą: 600 per minutę, 5000 per valandą): padidinant arba perkeliant žemėlapį iš teritorijos, kurioje jau atsisiuntėte programą, tankiausias perkraustymas, ne dažniau kaip kas 10 minučių; jei atnaujinus nepavyksta, ankstesni duomenys lieka ir jis bandys vėl po 3 minučių. Taip izobarai ir temperatūra veikia bet kurioje pasaulio vietoje.

Temperatūra. Tuo pačiu prašymu temperatūra padidėja iki 2 m, iki 925 hPa ir iki 850 hPa. LA0BY rodo, kad nuo 22 / 11 / 2018 iki UA6 įvyko temperatūros žemėlapiuose (900- 3000 m), bet ne Hepburn [3] prognozėse; DF5AI ir DK5YA naudoja vertikalius temperatūros profilius [5] inversijoms atpažinti. Tam yra du sluoksniai:

- Temperatūra 850 hPa: izoterme gydyti kiekvieną 4 ° C (raudona virš nulio, mėlyna žemiau, violetinė ant nulio).
- Terminė inversija: oranžinis mėginių ėmimas, kai oras 925 arba 850 hPa yra šiltesnis nei 2 m; kuo intensyvesnis, tuo labiau inversija stipri. Tai yra fizinė būklė, kuri lenkia bangas į žemę [3] [11], ir palyginti ją su tropo indeksas ir izobare rodo, jei prognozės turi meteorologinį pagrindą.

## 8. Spots ir stotys

Spot. Kiekviena vieta su abiem žinomomis pozicijomis yra linija maksimalaus apskritimo tarp dviejų stočių, padalintas į skyrius ir projektuojamas; Mercatore jis atrodo tada šiek tiek išlenktas, azimutale ji yra tiesiai, jei jis prasideda nuo centro. Mėlyna linija, pilna normaliai vietoje, geltona, padengta tamsiu pakraščiu, skirta airscatter (< AS >, < ACS > arba komentare "airscatter"), kamuolys kiekvienai stočiai, šviesos halo po linija, siekiant atskirti jį nuo mėginio. Pasirinkta vieta yra dažniau sukurta paskutinis ir su dviem pavadinimais.

Klausymas. Kiekvienas PSKReporter [16] priėmimo stotis yra tamsiai rausvas tuščias apskritimas. Nuo kiekvieno pradžios visiškai rausvos linijos link stočių, kurios iškoduoti ir išsiuntė atgal į tinklą, visada matomas be paspaudimų, su vardais visada parašyta. Pasirenkant apskritimą jos linijos yra storesnės. Vietoje ir klausytis tik parodyti juostą ir diapazoną, pasirinktą sąraše: keisti juostos plotį žemėlapis yra tuščias ir pertvarkytas su duomenimis jau surinkti naujos juostos.

Aš nusprendžiau. Kiekviena stotis, kuri decodes su WSJT-X, JTDX arba MSHV (gavimo per UDP) yra raudona linija pilna nuo jūsų QTH stoties, su kamuoliu ir nominacija. Tai yra labiausiai tiesioginis palyginimas su prognoze: rodo, kur jūsų imtuvas tikrai ateina dabar.

## 8 bis. Spalvos ir simboliai

Kiekviena spalva turi tik vieną reikšmę, vienodas visose temose (pakeisti tik fone).

| Spalva ir ženklas | Reikšmė |
| Nenutrūkstama mėlyna linija | Spot iš DX klasterio, RBN arba DM4X tarp dviejų stočių |
| Geltona linija su tamsiu kraštu | Spot su airscatter komentaras (atspindys orlaivių) |
| Tamsiai rausvas tuščias apskritimas | Klausymo stotis PSKReporter (MTQTT) |
| Plonas rausvos linijos nuo apskritimo, su rausvos kamuolys apačioje | Teiginiai, kad klausytojas išgirdo, su pavadinimu visada parašyta; storesnės linijos pasirinkto klausytojo |
| Raudonoji linija ir raudonasis pavadinimas | Jūsų UDP dekodings (WSJT-X, JTDX, MSHV): kas tu? |
| Raudona linija mirksi smūgių | QSO vykdo šią stotį: Jis paskambino jums ir uždarymo dar neatėjo, arba yra DX Call WSJT-X / JTDX su įjungta perdavimo. Tęskite su RR73, RRR arba 73 |
| Žalioji linija | QSO desunti dal live: uždarymo RR73, RRR arba 73 UDP vaizdas |
| Juoda linija su balta riba | Kažkas jus vadina: tai trunka iki RR73/RRR / 73 arba iki 3 minučių |
| 45 ° kvadratai | Eismas su bent viena stotimi už pasirinktos lajos: Nėra eilučių, tik kvadratų lokatorius |
| Ieškoti LILŲ su atstumu į šiaurę | 1000, 700 ir 500 km aplink savo QTH; šis pasirinkimas yra labiau pažymėtas |
| Apskritimas ir taškas temos spalvos | Jūsų QTH, su vardu ir šeimininku |
| Oranžinis kamuoliukas su gravES 143,050 | Radaras GRAVES ( JN27SI) |
| Pavyzdys nuo mėlynos iki raudonos | Hepburn trope indeksas (0- 10) arba F5LEN klasė, arba jų vidurkis; šiltesnė spalva, geresnės sąlygos |
| Šviesiai pilka aksoma | Plotas be tropo duomenų už pasirinktą laiką |
| Pilkai mėlynos linijos su vertė hPa | Izobratas kas 4 hPa; H mėlynu aukštu slėgiu, L raudonu žemu slėgiu |
| Raudonos, mėlynos ir violetinės taškinės linijos | Izotermija iki 850 hPa kas 4 ° C: raudona virš nulio, mėlyna po nuliu, violetinė nulis |
| Bandinys oranžinis | Šiluminis atstatymas: oras 925 hPa šiltesnis nei ant žemės, intensyvesnis su stipresne inversija |

Įtariamose ataskaitose kiekvienos kortelės kraštas sako galiojančią taisyklę: žalia Geras, raudona Skarta, pilka be taisyklės.

Kas yra lilac kvadratų punktyrinės 45 °. Eismas toli nuo savo QTH interesų jums suprasti, kur dauginimas yra atvira, bet jos linijos, šimtai kilometrų ilgio ilgai ir dažnai dešimtys, padengti žemėlapį ir paslėpti, kas jums svarbu: angos galite išnaudoti iš savo stoties. Dėl šios priežasties, už pasirinktą karūną, programa nėra atkreipti linijas, bet spalvos lokatoriaus kvadratų (2 ° × 1 °) stotys dalyvauja. Rezultatas yra "aktyvių zonų" žemėlapis: kur šalia Lilac kvadratų atsiranda, yra konduit arba atidarymo, net jei jis nėra susijęs su jumis tiesiogiai; Jei plotas išsiplečia į jus, tai yra signalas nukreipti anteną ta kryptimi. 45 ° prietaisų skydelis leidžia jums pamatyti pagal tropo indeksą ir tinklelį, todėl palyginti iš pirmo žvilgsnio prognozę (apatinė spalva) ir nekilnojamojo eismo (APM). Jūsų QTH aikštė niekada nespalva, nes tai yra išeities taškas, o ne tolima zona. Spalvoti tik tie kvadratai, kurie yra už karūnos ribų: karūną kertantis arba viduje esantis kvadratas lieka laisvas, net jei yra viena iš dviejų stočių. Norėdami pamatyti linijas, tiesiog išjunkite langelį arba pasirinkite didesnę karūną.

Kriterijai, pagal kuriuos nustatoma, kas yra žemėlapyje ir kas yra išmetama, paaiškinami 1 dokumento 11 ter dalyje.

## 9. Laiko juosta

Žemiau žemėlapyje: data ir laikas prognozavimo parodyta, žymeklis nuo + 0 iki + 72 h per 6 valandas, indeksas legenda 0- 10 su pasirinkto šaltinio pavadinimas, ir modelio lenktynių laiko izobare. Trumpetas ir izobare indeksas sekti kursoriumi; dėmės, stotys klausytis ir jūsų iššifruoti stebėjimai šiuo metu ir visada lieka tie "adeksas". Kai žymeklis nėra ant + 0 jie yra susilpninti, prisiminti, kad jie nepriklauso prognozės laikas.

## 10. Kaip skaityti sutapimą

- Didelis slėgis + didelis indeksas + vietoje palei spalvos srityje: atidarymo yra ir yra vienas planuojamas. Laikas skambinti ta kryptimi.
- Aukštas indeksas, bet ne vietoje: arba niekas yra aktyvus ten, arba prognozė yra pernelyg optimistinis. Klausymo sluoksnis padeda atskirti: jei yra imtuvai srityje ir nieko nejaučia, antra hipotezė yra labiau tikėtina.
- Ilgos dėmės už spalvotų sričių: aukšto ortakio, kurio paviršiaus prognozės nemato, kaip LA- UA6 [3] atveju; arba kitas dauginimas pabėgti iš filtro (pvz E sporadic be žymės). DF5AI ir DK5YA atskirti tropo ir E sporadic taip pat nuo valandos: angos, kurios gimsta ir baigiasi kartu su difuziniu E sporadic vargu ar [5] tropo.
- Hepburn ir F5LEN nesutaria: palyginti du šaltinius su mygtukais ir pamatyti, kur nekilnojamojo dėmės patenka.
- Slėgis žemyn: G4MCU po atidarymo ant SK6VHF švyturys, kol slėgis sumažėjo; 1012 hPa laikoma gatavas įvykis [13]. Žiūrėti izobare slinkimo laiko juostą: jei aukštas slėgis atsiima, atidarymo tikriausiai taip.
- Signalai, kad eiti į viršų: WSPR tyrimas 144 MHz KP4MD signalo / triukšmo santykis išaugo tiesiai languose, kur Hepburn prognozuoja pagerėjimą, ir sumažėjo, kai prognozė paėmė [14]. jeigu tame pačiame kelyje auga, atsiranda anga.
- Kalnų takai: indeksas neatsižvelgia į [1] reljefai, bet jungtys už Alpių taip pat egzistuoja dėka konvekcinių ląstelių ir bangų atmosferos gravitacijos [4], ir LA0BY dirbo 9A ir S5 per kalnus be Hepburn prognozuoja tai [3].

## 11. Neišspręstas klausimas

- Leidimas Hepburn ir F5LEN naudoti savo žemėlapius programoje.
- Konversijos koeficientų F5LEN → skalė 0- 10 kalibravimas.
- Bet kokio žemėlapio valdymo taškų kalibravimas, atliekamas perdaryti, jei šaltiniai keičia grafiką.
- Neprivalomas maršrutas, kurio temperatūra virš jūros lygio (850 hPa) nuo GFS.

## Pagrindiniai šaltiniai

1. W. R. Hepburn, DX Info Centre, "The Hepburn Tropo Index (HTI)" ir "Tropospheric Ducting Forecast" puslapiai (dxinfocentre.com). Rodyklės apibrėžimas ir skalė, neigiamos vertės ir daugiau nei 10, neįtraukimas į skirstytuvų ir atšakų sąrašą, 3 ir 6 valandų žingsniai per 6 dienas, padidinama iki 18 UTC. Duomenys iš pasaulinio Kanados modelio: G. Meisse, AMSAT- BB, 27 / 08 / 2007.
2. F5LEN, prognozė Europai (tropo.f5len.org / prognozės- for- European) ir pasaulinė prognozė VHF tropošerijos sklidimo (tropo.f5len.org / WW) ir pažymėta "Nauja versija VHF troposferos sklidimo prognozės įrankis". Vieneto indeksas N, 40 žingsnių nuo 3 valandų, 4 atnaujinimai per dieną, vertė ir lokatorius per paspaudimą.
3. S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", 41 ° Nordic VUSHF Meeting, Skjeberg 2019. Normalus gradientas 6,5 ° C / km ir iki 10-15 ° C / km, temperatūra ir drėgmė vaidmuo, didelis stabilus slėgis, LA- UA6 įvykis matomas aukštos temperatūros, o ne Hepburn, LA- 9A / S5 jungtys už kalnų.
4. F. Egano IK3XTV, "Paplitimas VHF - Konvekcinių ląstelių ir atmosferos gravitacijos bangų indėlis į Tropo plitimą". Pirmų 3 km reikšmė, jūrų keliai, konvekcinės ląstelės, traukos bangos Alpėse, aukšto slėgio jungtys, bendras tūris iki 3-4 km.
5. V. Grasmann DF5AI, U. Langenohl DK5YA, "Labai ilgas atstumas Propagation in the 144 MHz Band", 24 / 7 / 2003. Vertikalūs profiliai temperatūros ir rasos taško atpažinti inversijos, naudoti Hepburn indeksas, skirtumas tarp tropo ir E sporadic pagal valandą.
6. J. N. Gannaway G3YGF, "Tropospheric Scatter Propagation", QST, 1983 m. lapkričio mėn. Žemės spindulio ekvivalentas ir kelio, naudojamo indeksui ir taškinei kortelei, praradimas (žr. 1 dokumentą).
7. VHF / UHF DX knyga, 2 skyrius "Troposferos sklidimas", p. 2-23... 2-30. DN / dh (standartas − 39 N / km, ribos − 79 N / km ir 0) ir paviršinio bei aukšto ortakio profilių lūžio zonų schema. Pateiktos kopijos tekstas nėra įskaitomas: naudoti tik skaičiai.
8. Projekto santrauka 8 / 11 / 2026, pateikė programos autorius. Žemėlapio spalvos, reikalingos projekcijos, sluoksniai, laiko juosta.
9. Gamta Žemė, vektoriniai duomenys pakrančių ir sienų, visuomenės domeno (naturalfthaldata.com). Bendroji nuoroda, nepateikta priede.
10. J. P. Snyder "Žemėlapis - darbo vadovas", USGS Professional Paper 1395, 1987. Mercator projekcijų ir azimutale equidtanti formatai. Bendroji nuoroda, nepateikta priede.
11. Rekomendacija ITU- R P.453, radijo lūžio rodiklis. Refrakcijos formulė N ir ortakio slenkstis - 157 N / km. Bendroji nuoroda, nepateikta priede.
12. NOAA NCEP, Global Forecast System (GFS). Slėgis jūros lygyje 0,25 °, keturios rasės per dieną. Bendroji nuoroda, nepateikta priede.
13. G4MCU, straipsniai su žymeniu "troposferos" (g4mcu.net / tag / troposferos), 2018- 2025. Refration, konduit ir išsibarstymas; dažniau ortakiai su dideliu slėgiu, ypač ant jūros; švyturys SK6VHF toliau iki slėgio kritimo 1012 hPa (birželio 2019).
14. C. F. Milazzo KP4MD, "VHF / UHF WSPR Propagation Study" (qsl.net / kp4md / 144 _ mhz _ wspr.htm), 2012- 2014. SNR iš WSPR pranešė, kad 144 MHz auga Hepburn prognozuojamų patobulinimų languose ir krenta, kai prognozės išnyksta.
15. Open- Meteo, orų prognozė API (open- meteo.com), nemokamai nekomercinis naudojimas. GFS modelio slėgis jūros lygyje keliuose taškuose pagal vieną prašymą.
16. mqtt.pskreporter.info, distribution MQTT realiu laiku iš pskreporter reklamos. Informacija: gavėjų paskyrimas ir savininkas (žr. 1 dokumentą).
