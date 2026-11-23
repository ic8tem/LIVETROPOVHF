LIVETROPOVHF · techninis dokumentas 1
# Programos vykdymas

> Mašininis vertimas iš italų kalbos: kilus abejonių, galioja itališkas vadovas.

Ką programa daro, iš kur ji imasi duomenis ir kaip ji paverčia juos į tai, ką matote žemėlapyje ir sąraše. 2026 m. lapkričio 9 d. versija, susijusi su C # (.NET 10 + Avalonia) projektu.

## Apibendrinant

- Paskirtis: tropo prognozė (Hepburn [3], F5LEN [4], isobare GFS [18]) ir realus juostų peržiūrėjimas tame pačiame lange pagal LA0BY [1] kontrolės sąrašą.
- Duomenų rinkimas: du DX klasteris, Atvirkštinis Beacon Network [9], dm4x klasteris ir PSSKReporter [5] MQTT srautas, UDP dekodavimas iš WSJT-X, JTDX ir MSHV [8].
- Spot: juostos segmentai, neigiamas filtras, oro sklaida brūkšneliu geltona, padėtis pagal lokatorius arba CALL3. TXT [6] [7].
- Kas klausosi: tamsiai rožinės spalvos apskritimai su rožinėmis linijomis visada matomas link stočių jautėsi [5].
- Vietinė kortelė: atstumas, azimutas, tropo indeksas kelyje ir troposcatter nuostolių su G3YGF [2] metodu.
- Kalbos Hepburn ir F5LEN: [3] vaizdas ir kalibravimo stendas [4].
- Žiūrėti: NTP valdymas 10 serverių kas 3 minutes [10] [11].
- Įtariamos ataskaitos: EME Q65/JT65 atpažįstama su Mėnulio pozicija, nupjautais lokatoriais, skriejikliais, karantine ir klausytojais, su saulės [20] išmoktomis taisyklėmis.
- Spalvų, apibendrintų dviejose lentelėse (11 ter ir 11 quater sekcijose), išmetimo ir reikšmės kriterijai.
- Temos, 36 [17] kalbos, eksporto dokumentai su redaguojamas formules [16], nešiojama programa ir multiplatform [12] [14] [15]. [13]

## 1. Tikslas

Programa sujungia į vieną langą du dalykus, kurie paprastai atrodo skirtingose svetainėse: Tropofer sklidimo prognozavimas (Tropo indeksas Hepburn ir F5LEN, izobare) ir stebėjimas, kas iš tikrųjų vyksta juostose (vietoje iš DX klasteris, Atvirkštinio Beacon Network ir PSKReporter, plius stočių, kurios klausosi sąrašas). Atskaitos juosta yra 144 MHz; reklamų srautas - 50 MHz.

Nustatymas atitinka LA0BY pasiūlytą kontrolės sąrašą ekstremalių tropo įvykių atveju: praėjusiomis dienomis jūs žiūrite į Hepburn ir F5LEN prognozes ir temperatūros profilius aukštyje, o renginys vyksta jums ieškoti DX klasteris ir vietoje žemėlapiuose įrodymus, kad atidarymas prasideda [1]. LA0BY taip pat pažymi, kad vienintelių Hepburn / F5LEN žemėlapių nepakanka ilgiausioms jungtims prognozuoti. [1]

## 2. Langas ir skirtukai

Langas yra vienas ir nepasiskirsto į mobilųjį langą. Nuo viršaus iki apačios:

- Bendrieji nustatymai eilutė: Skambučiai ir lokatorius kairėje; dešinėje UTC laikrodis (pvz 08 / 11 / 2026 20: 33: 38 UTC, gintaro tekstas) po NTP: OK arba NTP: PASKIRTIS, tema meniu ir kalbos.
- Lentelių skaičius penkiose grupėse: Žemėlapis ir vieta; prognozės (Propria β, prognozę, apskaičiuotą pagal programą ir aprašytą Prognozės vadovas, tada Hepburn ir F5LEN); GRAVES; duomenys ir šaltiniai (DX klasteris, PSKReporter, Atvirkštinis beacon, UDP, Paskyrimai ir įtartini pranešimai); gidai (Programos operacija, Žemėlapių kūrimas, Kaip formuoti tropo, Rankinė prognozė, Patarimai operatoriui, su paieškos langeliu, kuriame rodomas tekstas). Po spinduliu juostoje rodoma grupės pokalbė; kiekviena grupė prisimena paskutinį naudotą.
- Kortelės turinys ir apačioje - būsenos juosta su šaltinių būkle, vardų duomenų bazė ir paskutinio atnaujinimo laikas.

Viršutinės eilutės nustatymai taikomi visoms kortelėms: pasirinkta tema taip pat dažo duomenų rinkimą ir dokumentų fonas; Dokumento puslapiai lieka balti kaip lapas, todėl jie spausdinti gerai.

## 3. Skambutis ir nuomotojas

Maidenhead savininkas gali turėti 4 arba 6 simbolius (pvz., JN70 arba JN70CN). Programa konvertuoja jį į kvadrato centro koordinates: laukas 20 ° × 10 °, kvadratas 2 ° × 1st, subquartrate 5 ′ × 2,5 ′. Jei tekstas nėra tinkamas lokatorius, laukelio kraštas tampa raudonas ir stotis žemėlapyje nerodoma. Lokatorius taip pat tarnauja kaip azimuto projekcijos centras ir kaip atskaitos taškas atstumų ir azimuto skelbimų lentoje.

Skambutis yra pavadinimas naudojamas kaip klasterio prisijungimo. Iš pradžių, kol nėra tinkamos kandidatūros, klasteriai nejungia: langas prašo pavadinimo (ir, pasirinktinai, nuomotojas); patvirtinta, kad pavadinimas kopijuojamas kaip vartotojo vardas kiekvienos klasterio, kuris turi jį tuščias ir klasteriai palikti. Su "Vėliau" lieka vis dar, ir kiekvienos klasterio mygtuką Prisijungti reikia dar kartą nominacija. MTTT, UDP ir NTP nenaudoja prisijungimo ir palikti vistiek.

## 4. Duomenų rinkimas: šaltiniai

Kiekvienas šaltinis turi savo skyrių į duomenų rinkimo skirtuką, su prijungtas / atjungtas, Atpažintas ir prijungtas langeliai paleidžiant, statusas, skaitikliai ir registruotis gautų eilučių. Į registrą neįrašytos neįsišaknijusios ar atmestos filtro linijos: tik skaičiuojami sekcijos skaitikliai. Skyriai yra vienas šalia kito, du - du.

| Šaltinis | Prisijungimas | Kas atneša |
| 1. DX klasterio pasirinkimas | on0dxk.dyndns.org: 8000 komandų rinkinys / filtras dxbm / pass 6 | Spot mėgėjų radijo, taip pat SSB ir CW. ON0DXK atveju VHF filtruojamas ir įjungiamas nurodyta komanda (poi pass 4, 2...). |
| 2. Antrasis šaltinis | dxmaps.com: 7300 (DXMAPS, EA6VQ) ir s50clx.si: 41112 ( S50CLX, Slovėnija) | DXMAPS surenka VHF dėmes ir taip pat suteikia RBN šlako dėmes (nustatytas / jau nustatytas); S50CLX yra DX Spider klasteris. Prisijungti su savo vardu; redaguoti adresus. |
| 3. Atvirkštinio švyturio tinklas | telnet.reversebeacon.net 7000 CW / RTTY · 7001 FT8 | Automatinis nuleidimas. Prisijungti tik su pavadinimu [9]. |
| 4. Klasteris su PSKReporter | dm4x.ddns.net: 8500 | Skaitmeniniai Spots iš PSKReporter klasterio formatu. |
| 5. PSKReporter MQTT | mqtt.pskreporter.info 1883 · 1884 su TLS | Kiekvienas realiu laiku priėmimo ataskaita: kas perduoda, kas klauso ir jų [5] Vietos. 144, 222 ir 432 MHz dažnių juostos. |
| 6. Orų ir tropų žemėlapiai | dxinfocentre.com / tropo _ eur.html tropo.f5len.org / prognozė- per-europinis modelis | Hepburn [3] ir F5LEN [4] tropo indeksų žemėlapiai, jūros lygio slėgis izobare (GFS per Open- Meteo [18]). Austrams turi būti leista vartoti Hepburn ir F5LEN. |

Klasteris telnet (šaltiniai 1-4). Po prijungimo programa laukia vardo užklausą (tekstas, kuriame yra prisijungimo, skambinti:, šaukinys ar jūsų skambutis); jei ji neatvyksta per 4 sekundes, prašome siųsti prisijungimo. Tada siųsti sekcijos komandas, atskirtas nuo taško ir kablelio, kas 0,6 sekundės. Telneto valdymo ženklai pašalinami prieš skaitant linijas. Jei ryšys nukrenta ir Rikonnetti yra aktyvus, bandykite vėl po 10 sekundžių (15 jei serveris visiškai nereaguoja) [6].

PSKReporter MQTT (šaltinis 5). Programa įsilieja į temą pskr / filter / v2 / {banda} / {modo} / {tx _ call} / {rx _ call} / {tx _ grid} / {rx _ grid} / {tx _ dxcc} / {rx _ dxcc} nustatant tik juostos ir paliekant kitus laukus nemokamai su ženklu + [5]. Tikslus pavadinimas, su kuriuo serveris nurodo juostas (pvz., 2m, 1,25m, 70cm), turi būti patikrintas pirmojo sujungimo metu.

## 5. Nuo klasterio linijos iki vietos

Viena eilutė yra vietoje, kai ji turi formą DX de SPOTTER: dažnis DX komentaras hhhmmZ. Programa gauna spintelė, DX stotis, dažnis kHz, komentaras, o dabar UTC; pavadinimai kyla skriejantis priesagos (- #, -1...). Tokiu atveju taikomos šios [6] taisyklės.

| Grupė | Segmentas priimtas |
| GRAVES (144) | 143,040-143,060 MHz |
| 144 MHz | 144-148 MHz |
| 222 MHz | 220-225 MHz (tik 2 regionas ITU, Amerika) |
| 432 MHz | Pelningumas, pinigų srautas, investicijos ir investicijų grąža MHz |
| 1296 MHz | 1 lentelė MHz |
| 2320 MHz | 2 lentelė MHz |
| 10 GHz | 10,5 GHz |

1. Grupė. Dažniai turi patekti į vieną iš valdomų segmentų; visi kiti, visų pirma visi komerciniai įrenginiai, kurių dažnių diapazonas mažesnis kaip 49 990,0 kHz, nėra rodomi ir skaičiuojami kaip "už dažnių juostos ribų".
2. Taip. Vienas iš labiausiai žinomų būdų komentuoti yra FT8, FT4, Q65, JT65, ISCAT, CW, SSB, FM, RTTY, PSK31, MSK144, FSK441, JT6M); USB ir LSB tampa SSB.
3. Neigiamas filtras. Vietovė atmetama, jei joje yra MSK144, FSK441, JT6M, JAMPIT, ISCAT režimai arba žymos < AUE > < ES > < MS > < FAI > < F2 >, su nejautriais viršutiniais lapais ir net tarpais skliaustuose. Tai yra būdai ir propagacijos, kurie nėra tropas: Scatter meteor, auora, ir sporadic, F2.
4. Oro sklaida. Jei komentare yra < AS > arba < ACS > (taip pat sumaišytas, pvz., < As >) arba vieta deklaruojama kaip oro sklaida (oro sklaida, oro sklaida, orlaivių sklaida), vieta lieka, bet yra nubrėžta geltona taškine linija (tamsiai banguota, kad aiškiai matytųsi žemėlapyje) ir pažymėta sąraše.
5. Stočių vieta. Jei komentare yra du lokatoriai (pvz., JN54 < JN83) pirmasis yra iš tų, kurie padaryti vietoje ir antroji stoties DX; su vienu lokatorius, kad iš DX stoties. Ko trūksta, ieškoma CALL3. TXT, tik su tiksliu pavadinimu (dėl nešiojamųjų kompiuterių žr. toliau). Jei stotis lieka be pozicijos, vietoje yra sąraše su pastaba "nežinoma pozicija", bet ne žemėlapyje.
6. Dagai. Komercija su tuo pačiu DX, tas pats lankytojas, ta pati juosta ir tas pats šaltinis vienas jau esantis jį pakeičia, o ne pridėti.

CALL3.TXT yra pavadinimas duomenų bazė → lokatorius naudojamas JTDX, formatu CALL, LOCATORE,. Projekto byloje yra 1 048 847 pavadinimai, visi su galiojančiu lokatoriumi; failo antraštės vis dar kalba 34 323 pavadinimai, nes ji liko, kad pirmojo leidimo [7]. Jis įkeliamas į foną paleidimo metu, todėl langas yra iš karto panaudojamas. Failas ieškomas šalia programos, duomenų aplanke, aplanke, iš kurio programa buvo pradėta, ir viename ar dviejuose aukščiau esančiuose aplankuose; jei nėra, langas tai pasako, kai pradedate ir būsenos juosta siunčia raudoną. Jo pavadinimas visada naudoja 6 simbolių. Tik lokatorius taisyklė: jei vienas gautas (vietoje, MTQTT, dekodavimo, QSO desunti) turi mažiau nei 6 simbolių ir CALL3. TXT turi vieną į 6, tai visada verta CALL3. TXT; nešiojamieji kompiuteriai (F / IC8TEM, IC8TEM/P) yra tik tikslus balsas: be, stotis lieka be nuomotojo ir nėra sukurta kitur. Ši taisyklė taip pat taikoma piešimo metu, todėl duomenys, gauti prieš įkeliant duomenų bazę, bus panaudojami.

Viena duomenų bazė: CALL3. TXT. Iš 1.3.4 visi lokatoriai yra vienoje byloje, CALL3.TXT, kuris lieka JTDX ir WSJT-X formatu (CALL, LOCATORE,,,) ir gali būti naudojamas, kaip ji taip pat yra WSJT-X, JTDX ir MSHV. Nėra jokių failų. Bylos viršuje, po antraštės, yra du blokai, pažymėti komentarų eilute: / / [LIVETROPOVHF- MANUALI] su įkištais įrašais ranka ir / / [LIVETROPOVHF- CLUSTER] su pataisymais iš klasterio; tada / / [LIVETROPOVHF- FONTI] su visais kitais objektais abėcėlės tvarka. Kiekviena kandidatūra pateikiama vieną kartą. Poliežuvinių duomenų ir šaltinių › Paskyrimai (CALL3) Jūs ieškote vardo, rašote šeimininką ir spaudžiate Pridėti / pakeisti: balsas eina į balso bloką ranka ir tai yra verta visų pirma, net virš vietos, kad pati stotis siunčia. - Pašalina kandidatūrą iš CALL3.TXT (kitas atnaujinimas atnaujina ją iš šaltinių, jei jie turi). Pridėti failą... skaityti sąrašą CALL, LOCATOR (jūs taip pat priimti taškas ir kablelis, tabuliacija ir vietos) ir rašyti jį tarp rankomis renkamų balsų; pasirinktas failas nėra kopijuojamas. Iš naujo perskaityti CALL3.TXT iš disko, po redagavimo jį su redaktoriumi. Visas šaltinių eiliškumas: nuosavas vietos nustatymo įrenginys, rankiniai balsai, klasteriniai pataisymai, likusi CALL3 dalis. TXT (šaltiniai, išmoko gyventi lokatorius ir G7VRD), gavo lokatorius (kuris taikomas per duomenų bazę tik jei ji turi 6 simbolių ir nesibaigia su LL arba MM).

Atnaujinti CALL3.TXT. Toje pačioje pokalbėje mygtuką Atnaujinti dabar parsisiunčia šaltinius (CALL3.TXT WSJT Pagerinta, CALL3.TXT EME W7GJ, tinklelis _ data.bin JTDX), prisijungia prie jų (pirmasis, kuris turi nominacija laimėti), patikrinti, kad jie yra bent 50 000 pavadinimų (kitaip nieko nekeičia), taupo CALL3. TXT Ankstesnis CALL3. TXT įrašai lieka, ir 6 simbolių lokatorius lieka ten, kur šaltinis turi tik 4 to paties kvadrato. Rankiniai balsai ir pataisymai iš klasterio lieka viršuje, nepasikeitė. Su Atnaujinti langelį savo kiekvieną dieną, patikrinimas vyksta pradžioje. Programa perrašo CALL3. TXT iš karto po kiekvieno balso ranka ir, išmoktų lokatorių, ne dažniau kaip kas 10 minučių ir uždarymo; CALL3. Paketo TXT yra tik read- ir atributas kyla pats. Linux montuotojai nepaiso CALL3. TXT jau yra; kad Windows įdiegia tik jei ji nėra. ALL3

Automatiniai pataisymai iš klasterio. DM4X ir RBN automatiniuose taškuose komentaras atneša du lokatorius, pavyzdžiui, KN09tp < > KN09: pirmasis yra tai, ką stotis, kad dėmės deklaruoja save, iki 6 simbolių; antrasis yra, kad stoties jaučiamas, dažnai tik į 4. Jei pirmasis turi 6 simbolių (ir nesibaigia su LL arba MM) ir skiriasi nuo duomenų bazės, programa pataiso CALL3. Tik TXT: pataisa patenka į bloką / / [LIVETROPOVHF- CLUSTER], su paskutinio patvirtinimo trečiojoje srityje data, ji galioja virš šaltinių (po ranka balsai) ir atrodo tarp įtartinų signalų atnaujintas kaip "CALL3 ". Taigi jis išlieka CALL3. TXT atnaujinimai. Nepatvirtinta pataisa dvejus metus praranda pirmenybę ir išlieka įprastu balsu.

Paskutinis šaltinis: G7VRD. Jei kandidatas neturi 6 simbolių CALL3. TXT, programa prašo jį į viešą API G7VRD fone (api.g7vrd.co.uk, "lokator by callsign"), kuris renka lokatorius automatiškai iš tinklo srauto. Jūs sutinkate tik 6-simbolių, kad nėra aikštės centras (LL ar MM); prašymai yra sugrupuoti, ne daugiau kaip 40 nominatorių per minutę, ir kiekvienas nominatorius prašo kartą per dieną, nes tai yra hobistinė paslauga be garantijų. Rastas lokatorius patenka į CALL3. TXT, jei pavadinimas nėra arba turi tik 4 simbolius to paties kvadrato; paieška išsijungia su laukeliu pavadinimu poliežuvinis.

Paskyrimai su baru. Be PSKReporter MTQTT temos, baras atskiria lygius, tada sudėtiniai pavadinimai ateina su taškas vietoj baro ( IV3. HB9CAT). Programa parkelia juostą atgal ir, jei pranešime yra visi pavadinimai, naudokite tuos: IV3/HB9CAT lieka IV3/HB9CAT žemėlapyje, sąraše ir duomenų bazėje.

Lokatoriai išmoko gyventi. CALL3. TXT yra ne visi: pavyzdžiui, SP9EMF ir SP9NVY nėra, ir tada lieka 4 simbolių lokatorius siunčia PSKReporter (kortelė sako, kad). Siekiant sumažinti šių atvejų programa rašo CALL3. TXT kas 6-simbolių, kurie ateina iš bet kurio šaltinio (PSKReporter, pavyzdžiui, siųstuvas arba imtuvas, klasteris, UDP dekodavimo) už paskyrimo, kad CALL3. TXT nėra arba turi tik 4 simbolių toje pačioje aikštėje; niekada nekeičia balso 6 simbolių. Iki 1.3.3 šie lokatoriai buvo duomenų / LOC6.TXT

Perėjimas nuo 1.3.3. Pirmąją 1.3.4 pradžioje bylos, išskyrus ankstesnes versijas (Dati / CALL3 -MANUALE.TXT, Data / CALL3 -AGGIUNTE, Data / CALL3 -CLUSTER. TXT, duomenys / LOC6. TXT ir duomenys / CALL3 - G7VRD. TXT) yra sujungti į CALL3. TXT su tais pačiais įrašais. Paleisti raštu, senų failų imtis.fuso plėtinys, jūs nebeskaityti ir galite ištrinti.

Automatiniai imtuvai. RBN, dm4x ir PSKReporter dažnai praneša pavadinimus su priesaga, nurodančia imtuvą: -SDR, / SDR, - # (plever RBN) arba -1, -2... (plius tos pačios stoties imtuvai). Nuomininkui priesaga pašalinama: OK2OZL -SDR paima OK2OZL JN89UE iš CALL3.TXT, jei stotis dar nesiunčia 6 simbolių lokatoriaus. Rodomas pavadinimas lieka su priesaga.

Lokatoriai išjungti. Ne visi šaltiniai gydyti savo šeimininko pratęsimo: daugelis nustatyti jį į 4 simbolių, ir kai kurios automatinio perkėlimo sistemos sąmoningai nupjaukite 4 simbolių lokatorius, net tie, kurie gauti duomenis iš realaus klausymo FT8, FT4, Q65 ir JT65, arba užbaigti jį su aikštės centre (LL arba MM, pvz JN89MM). Dėl šios priežasties 4 arba 6 simbolių, kurie baigiasi su LL arba MM, pakeičiama CALL3. TXT to paties kvadrato. Jei teisinga vieta ir aikštės centras nesutampa su kelių dešimčių km, klaida išlieka maža, palyginti su tropo atstumų.

## 6. Kas klausosi

Kiekvienas MTTT pranešimas iš PSKReporter praneša pavadinimą ir lokatorius tiems, kurie gauna [5], todėl jums nereikia ieškoti jį CALL3.TXT. Pasirinktos juostos selektoriaus programa laikosi, kiekvienai priimančiajai stočiai: nuomotojas, paskutinis priėmimas, dekoduoti būdai, skaičius stočių jaučiamas ir maksimalus atstumas. Stotis palieka sąrašą, kai ji nebegauna nieko pasirinktame intervale (3, 5, 10, 15, 30 arba 60 minučių).

Žemėlapyje kiekvienas imtuvas yra tamsiai rausvas tuščias apskritimas, ir nuo kiekvieno pradžios visiškai rausvos linijos link stočių, kad jausmas ir siunčia atgal į tinklą. Linijos yra visada matyti, be paspaudimų. Paskyrimai klausymo stočių, tie, kurie išgirdo ir du vadovai kiekvienos vietos visada parašyta žemėlapyje, be paspaudimo (tas pats pavadinimas tame pačiame taške yra parašyta tik vieną kartą). Paspaudus apskritimą, jo linijos tampa dar storesnės ir atveria skirtuką su detales. Jei iš MQTT lokatorius turi mažiau nei 6 simbolių, programa naudoja CALL3.TXT (apie vieną milijoną pavadinimų), jei jis atitinka gautas kvadratas.

Scatter meteor. Kaip ir klasterio dėmių atveju, MQTT santykiai MSK144, FSK441, JT6M, JAMParba ISCAT yra išmetami ir suskaičiuojami tik; tas pats pasakytina apie UDP iššifravimą šiais būdais.

Netikri klausytojai. Kai kurios stotys deklaruoja, kad gauna 144 MHz, tačiau iš tikrųjų imtuvas yra kitoje juostoje (klaidingas dažnio nurodymas): jų ataskaitose būtų projektuojamos neegzistuojančios tūkstančių km angos. Programa pati tai tikrina. Kiekviena stotis, kad klausytojas sako, kad išgirsti VHF seka 30 minučių visose juostose, su specialiu MQTT užrašu (pskr / filtras / v2 / + / {nominal} / #) [5]. Dirba kaip įrodymas. Jei bent dvi stotys, kurias išklausė klausytojas, per tą patį laiką (± 10) veikia kitose juostose, klausytojas laikomas netikru ir visos jo VHF ataskaitos atmetamos; MTQTT registras praneša jo pavadinimą ir stotis. Į dėmių sąrašą, šalia skaitiklių, MTQTT mygtukas rodo srauto būklę (žalia lapų, jei prijungtas, raudona, jei ne) ir kiek klausytojai buvo išmesti juostos ir diapazono pasirinkta; spustelėjus ji atveria skirtuką su išmetimo sąrašą ir, kiekvienai, stotys rasti kitose juostose.

Ilgas ryšys su kinu. Nuoroda per 1000 km (Kumbing Spot, MQTT Klausymas, QSO Desunti) nėra iš karto parodyta: lieka laikomi, ir skaitiklis "vyko > 1000 km" sako skaičius. Jis atskleidžiamas, kai kita jungtis, tarp skirtingų stočių ir bent pusės jų, patvirtina ją vienu iš šių kriterijų: ta pati stotis prie galvos (pavyzdžiui, tas pats klausytojas) ir kita stotis ta pačia kryptimi per 10 °; panašus koridorius, kurio abi galvutės yra 200 km atstumu nuo kitos jungties; lygiagretus maršrutas, kurio kryptis yra 10 ° ir šoninis atstumas 200 km, apsuptas išilgai maršruto. Ir parodyti ir MTTV žurnalas rodo priežastį. Jo UDP dekoderiai visada matomi ir taikomi kaip patvirtinimas kitiems. Išlaikyti sujungimai išlieka atmintyje 60 minučių.

Ne mažiau kaip 144 MHz. Programa veikia nuo 144 MHz iki: viskas iki 143 MHz yra išmetama. Išimtis yra GRAVES, Prancūzijos radaras, transliuojantis iš Broye- lès- Pesmes ( JN27SI) į CW 143,050 MHz dažnių juostoje: komercinė 143,040-143,060 MHz dažnių juosta arba su GRAVES pavadinimu, pasirodo tarp 144 MHz su radaru kaip siųstuvu. Skelbimų lenta sako, jei imtuvas yra viduje spindulio (sektoriai D, C, B, A nuo 90 ° iki 270 ° azimuto matyti iš radaro, 6 žingsniai nuo 7,5 °) arba iš; skirtukas GRAVES, kaip ir YA- MS, rodo iš radaro matomas dangus: sektoriai, žingsnis apšviestas tuo metu (sequenza PE1ITR 2021: 0,8 s per pakopą, ciklas 4,8 s duomenys ir geometrija ateina iš YA- MS. Jausmas trope yra sunku, bet tai gali atsitikti.

Žmogaus grupių reabilitacija. Stotis, kuri taip pat pasirodo kitose juostose, nelaikoma įtartina, jei per tas pačias minutes (± 30), DX žmogaus klasteriai ( ON0DXK, GB7RAU; ne dm4x, RBN ar kiti automatiniai šaltiniai) aktyvioji žala aptariamai juostai, dėmėmis, padarytomis kitų pavadinimų: tai yra stočių, kad tikrai dirba keliose juostose kartu. Taigi išmestas klausytojas grįžta matomas (MTQTT registras rašo "reabilituotas") ir galima atskleisti ilgą saitą žemoms juostoms.

Aktyviosios stotys iki 144 MHz. Ilgų jungčių karantine stotis, kuri per tas pačias minutes girdima mažiau kaip 144 MHz dažnių juostose (iš kitų juostų MQTT pranešimų), nepatvirtina kitų jungčių ir nėra atidengiama, nors panašus arba lygiagretus maršrutas tai patvirtintų.

## 7. Reklamų sąrašas

Jis užima 35% teisę į Žemėlapio skirtuką ir vietoje. Viršuje: pavadinimas, juostos selektorius (50, 70, 144, 222, 432, 1296, 2320 MHz, 10 GHz), nuotolio selektorius (paskutinis 3, 5, 10, 15, 30, 60 minučių) ir atnaujinimo raktas. Žemiau, keturių metrų: matomos dėmės, AS / ACS, išmesti iš filtro, stotys klausytis.

Kiekviena linija rodo linijos tipą, laikas UTC, DX → DE, kelias, atstumas km ir, antroje eilutėje, šaltinis, dažnis ir komentaras. Paskutinieji yra viršuje. Vienas paspaudimas ant linijos pasirenka vietoje: žemėlapyje linija auga ir du pavadinimai atsiranda.

## 8. Spot kortelės ir troposcatter praradimas

Pasirinktoje vietoje skirtukas rodo lokatorius, atstumas, azimutas, dažnis, režimas, šaltinis, dabar, komentaras, vidutinis tropo indeksas palei kelią ir maršruto nuostolių už išsklaidytas tropo įvertinimas.

Atstumas ir azimutas yra apskaičiuojami ant didžiausio apskritimo su Žemės spindulys 6371 km (formulė emisenoverso atstumo, pradinis azimutas iš DX stoties į spintelė).

Praradimo troposcatter. Pagrindinis nuostolis, už dvi stotis ant lygaus žemės, yra laisvos erdvės nuostolių ir sklaidos nuostolių suma: G3YGF [2]

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

Nuostoliai dB, D - atstumas km, f - dažnis MHz, θs - difuzijos kampas laipsniais.

8497 km yra lygiavertis Žemės spindulys (4 / 3 fizinio spindulio). θA ir θB priklauso nuo kiekvienos stoties horizonto:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

su Δh stoties ir jos atstumo kliūties aukštis nuo horizonto. Kiekvienas horizonto lygis kainuoja apie 10 dB [2]. Programa nežino stočių horizontas, todėl ji rodo pagrindinį nuostolį su θA = θB = 0 ir deklaruoja ("sklandžiai žemės"). G3YGF prideda klimato terminą, kur N yra reakcija į [2] dirvožemį; jis bus naudojamas, kai N yra pasiekiamas iš oro modelio:

```math
L_{N} = 0,2 \cdot (N - 310)
```

Tikrinimas: 110-144 MHz ilgio formulė suteikia 166,5 dB, palyginti su maždaug 168 dB lovų ant straipsnio diagramos. Nuo 500 km iki 144 MHz gaunama apie 206 dB. Tikėtinas signalo ir triukšmo santykis yra sistemos pajėgumas (eirp + efektyvusis imtuvo jautrumas), atėmus nuostolius: naudojant G3YGF 144 MHz modelio sistemą (198 dB) 500 km kelias SSB [2] duotų apie − 8 dB, t. y. prisijungimas būtų tik geresnis grynos sklaidos skleidimas.

## 9. Mano dekoracijos: UDP iš WSJT-X, JTDX ir MSHV

WSJT-X, JTDX ir MSHV gali siųsti UDP kiekvieną jų iššifruotą liniją. Programa klausosi 127.0.0.1: 2237 ir kartojasi tą patį paketą, baitas per baitas, 127.0.0.1: 2238, todėl kitos programos, kad naudojamas uostas (GridTracker, JTAlert, logger) toliau dirbti, jei jūs perkelti juos 2238. Adresai ir durys pakeisti UDP poliežuvinių duomenų rinkimo; kaip klausymo adresą taip pat galite naudoti multiast grupė (pvz 224.0.0.1), jei dekodavimo programa yra nustatyta kaip tai.

Paketai atitinka WSJT-X tinklo protokolą: pradinis magija numeris 0xADBCCBDA, schemos numeris ir pranešimo tipas. Programoje daugiausia naudojami trys [8] tipai:

- Širdies plakimas: sako, kuri programa perduoda duomenis ir su kokia versija.
- Statusas: telefono dažnis ir režimas; iš dažnio gausite juostą (su ta pačia lentele taškų).
- Decode: Dabar, SNR, laiko ir dažnio švaistymas, būdas ir tekstas iškoduoti pranešimą.

Iš teksto gausite stoties girdėjote: CQ 9 A2PQR JN83 yra stotis po CQ (sutaupyti DX, ES, TEST...), IK0JKL IT9MNO JM68 yra antroji, t. y. kas perduoda. Lokatorius yra pranešimo, jei yra (4 simbolių, skiriasi nuo RR73); kitaip jūs ieškote CALL3. TXT. Jie klausosi linijos: stočių, kad jūsų imtuvas girdi dabar. Žemėlapyje jie tampa raudonos linijos nuo jūsų QTH į stotį, su pavadinimu; sluoksnis šviečia su decoded langelį nuo manęs ir reikalauja galiojantį lokatorius. Dešiniajame sąraše skaitiklis pasakoja, kiek stočių jūs dekodavimo pasirinkta juosta.

QSO desunti dal gyventi. Kai dekodavimo yra uždarymo pranešimas forma C ALL1 C ALL2 RR73 (arba 73 arba RRR), pavyzdžiui, IC8TEM IK3FHP RR73 arba IK3FKP SV8PEX 73, abi stotys baigė nuorodą. Programa įrašo jį kaip QSO, su lokatoriumi iš kiekvieno iš paskutinio pranešimo, kuriame jis jį deklaravo arba, jei ne, iš CALL3.TXT [7], ir atkreipia jį į žemėlapį kaip žalią liniją tarp dviejų stočių (strate "QSO desunti dal live"). Vienai grupei tenka po porą stočių; sąraše pasirinkta juosta ir intervalas galioja.

Toje pačioje pokalbėje derinimo polangis rodo kiekvieno gauto paketo tekstą (tipas, laikas, reikšmės ir pranešimas), paprašius taip pat šešioliktainius baitus ir siuntimo eilutę. Tai patikrinti, kad dekodavimo programa iš tikrųjų siunčia duomenis.

## 9 bis. Kalbos Hepburn ir F5LEN

Kiekvienas rodo, ką programa trunka nuo šaltinio. Mygtukas "Skaityti puslapį ir atsisiųsti visus vaizdus" atidaro nurodytą puslapį (remiantis dxinfocentre.com / tropo _ eur.html Hepburn [3] ir tropo.f5len.org / prognozess- for- europe F5LEN [4]) ir parsisiunčia visus krepšio vaizdus šalia vykdomojo failo; taip pat galite įklijuoti vieno paveikslėlio adresą. Kiekvienam vaizdui yra dėžutė, kad būtų pašalinti jį ir su prognozavimo laiko lauką. 31 Europos vaizdai F5LEN yra pavadintas modelio lenktynių metu (03.png... 48.png kas 3 valandas, 54... 96 kas 6 valandas, 108... 192 kas 12 valandų) ir programa užima laiką nuo [4] failo pavadinimas; Hepburn tai verta puslapio tvarka. Be F5LEN skirtukas, meniu pasirenka regioną: Europa (tropo.f5len.org / prognozės- for- europe), Australija (tropo.f5len.org / Australija, vk03.png... vk192.png) arba Atlanto tarp Žaliojo Kyšulio ir Airijos (tropo.f5len.org / atlanticean / atlantic- ocean / atlantic- d4-ei, atl03.png... atl192.png). Kiekvienas turi tuos pačius žingsnius.

Žemiau yra kalibravimo matuoklis. Vienas paspaudimas ant paveikslėlio prideda valdymo taškas (lokatorius ar lat, ne: jums reikia bent 6) arba legenda spalva su savo verte. Programa apskaičiuoja pikselių atitikimo → koordinates su antrojo laipsnio daugianacionalinės interpoliacijos mažiausiuose [19] kvadratų, skaito spalvas su pasirinkta tolerancija ir, F5LEN, konvertuoja vienetus N į 0- 10 skalėje su · N + b. "Išsaugoti kalibravimo ir bandyti skaityti" rašo failą į Tarature aplanką ir patikrinkite, kuris pikselis jūsų lokatorius patenka. Prieš pradedant kalibravimą, žemėlapių autoriui turėtų būti prašoma leidimo.

Regionai. Kiekvienas šaltinis turi daugiau regionų, kiekvienas su savo fiksuoto pavadinimo vaizdus: Atleisk, atleisk. Kiekvieno šaltinio skirtukas pirmiausia rodo visų branduolių sintezės peržiūrą: visi atsiųsti vaizdai sudėti kaip pagrindiniame žemėlapyje, su punktyrinės plokštės ir kiekvieno regiono pavadinimas, pilka uždanga, kur joks vaizdas apima plotą ir slankiklį slinkti valandas; į kairę regionų sąrašą, kiekvienas su langeliu jį naudoti, skaityti skaičių žemėlapių ir iš kur kraštutinumų atvykti (OCR, ranka arba gamykloje; jei OCR nepavyksta jo skaitomas tekstas atrodo). Rankinis kalibravimo stendas lieka už homoniminio mygtuko. Lentelės regiono meniu pasirenka, kuri išvaizda, langelis "Naudokite šį regioną žemėlapyje" apima jį į indeksą. Rėmas yra randamas vienas (dvi horizontalios linijos ir dvi vertikalios tos pačios spalvos linijos); vaizdas tarnauja kraštutinumui: Hepburn Europe (10 ° W- 40 ° E, 27 ° -56 ° N) ir F5LEN Europe (15 ° W- 40 ° E, 30 ° -65 ° N) jau yra programoje, kitiems regionams jie rašo keturiuose O E S N laukuose skaitant ašių etiketes. Skalės daugiklis leidžia palyginti du šaltinius: galutinė vertė = daugiklis × legenda vertė. [3] F5LEN [4] F5LEN

## 9 ter. Eksportuoti žodžiu

Abu dokumentai yra ant mygtuko "Eksportuoti į žodį" viršuje. Failas parašytas Office Open XML formatu; formulės tampa Word lygtimis (OMML), todėl jie lieka redaguojami su [16] lygčių redaktoriumi. Jei įrašyti langą nėra, failas baigiasi dokumentų aplanke šalia vykdomojo failo.

## 9 quater. Kas mane vadina

Jei pranešimas siunčiamas jums vietos UDP (vardas yra jūsų, pavyzdžiui IC8FAX IK3FHP JN55 arba IC8FAX IK3FHP -12), programos takelius nuo jūsų QTH į stotį, kuri vadina jums juoda punktyrinės ir mirksi linija, su pavadinimu ir lokatorius šalia jūsų. Lokatorius yra iš CALL3.TXT (įskaitant rankinius balsus) arba žinutės. Linija lieka tol, kol UDP eina QSO uždarymą su ta stotimi ( RR73, RRR arba 73, viena iš dviejų krypčių), ne daugiau kaip 3 minutes nuo paskutinio skambučio; jūs atkreipti per viską ir nesilaikyti atmetimo kriterijus, todėl tiesioginis skambutis niekada pabėgti.

PSKReporter, per MQTT, neperduoda žinučių tekstą: sako tik tie, kurie gavo, kas, dažnumu, būdu ir SNR. Štai kodėl tiesioginis skambutis yra pripažįstamas tik savo UDP dekodingų.

## 10. Laiko kontrolė (NTP)

Skaitmeniniai režimai ir vietos laikas reikalauja tikslaus laikrodžio. Kas 3 minutes programa tardo 10 NTP serverių (SNTP UDP 123): 0.pool.ntp.org, 1.pool.ntp.org, 2.pool.ntp.org, 3.pool.ntp.org, it.pool.ntp.org, ntp1.inrim.it, ntp2.inrim.it, ptbtime1.ptb.de, time.google.com, time.powdflare.com [11].

Kiekvieno atsakymo atveju apskaičiuojamas į jūrą išmetamų žuvų kiekis ir grąžinimo vėlavimas, į jūrą išmetamos neįprastos vertės ir atsižvelgiama į medianą, apskaičiuojant į jūrą išmetamų žuvų kiekį ir vėlavimą, kaip nurodyta SNTP [10]. Su bent 3 atsakymai sutinka UTC laikrodis programa suderina ir kitas atrodo NTP: Gerai žalia; jei atsakymai nėra pakankamai atrodo NTP: PADĖTI raudona ir laikrodis lieka, kad kompiuterio. Sistemos laikrodis nėra paliestas, nes administratoriaus leidimai būtų reikalingi; visi vidiniai programos tvarkaraščiai (dėmės, 5-60 minučių langai, dekodavimai) naudoti suderintą laiką. Valdiklis visada pasuka į foną: sąsajoje matote tik rezultatą šalia laikrodžio; serveriai, intervalai ir mažiausi atsakai pasikeičia LIVETROPOVHF.ini (ntp _ serveriai, ntp _ minutes, ntp _ min).

## 10 bis. Bendrieji atmetimo kriterijai

Dvi dėžės sluoksnyje taikomos viską, ką jūs atkreipti (vietoje, PSKReporter, dekodavimo, QSO desunti):

- Išskyrus mažiau kaip 100 km. Eismas tarp stočių, mažesnių nei 100 km, gali būti nereikšmingas tropo (žemės bangų, optinis diapazonas): ant dėžutės jūs neturite atkreipti ir nerodomi sąraše. Pradžia baigta.
- Daugiau kaip 1000 km: tik kvadratai. Aplink savo QTH (lokatorius nustatyti pradėti) yra 1000 km karūna visomis kryptimis. Jei tik viena iš dviejų stočių nuoroda yra už šio lajos, ryšys nėra piešiamas kaip linija: jos vietoje jie spalvos lilac, punktyrinės 45 °, lokatoriaus kvadratų (4 simbolių) dalyvaujančių stočių. Tas pats pasakytina ir apie PSKReporter stočių klausytis daugiau nei 1000 km, kuris atrodo kaip kvadratas, o ne kaip apskritimas. Iki šiol eismo matote be užpildyti linijų žemėlapį. Be veikiančio lokatoriaus skaičiuoja ryšio ilgį. Jūsų QTH kvadratas niekada spalvas lilac, net tada, kai klausytis stotį už karūnos: yra nurodytas lokatorius, bet pavadinimas naudoja, kas veikia iš stoties. Karūnos spindulys pasirenkamas šalia dėžės: 1000, 700 arba 500 km. Dėžutė Corone 1000 / 700 / 500 km atkreipia tris karūnas aplink QTH, sudarytas lilac, su labiausiai pažymėtas pasirinkimas ir į šiaurę parašytas atstumas. Elementų sąraše po atstumo yra... Pradžioje. Šis kriterijus taikomas po karantino: vis dar laikomos ilgos jungtys net neatrodo kaip kvadratai.

Kas yra lilac kvadratų punktyrinės 45 °. Eismas toli nuo savo QTH interesų jums suprasti, kur dauginimas yra atvira, bet jos linijos, šimtai kilometrų ilgio ilgai ir dažnai dešimtys, padengti žemėlapį ir paslėpti, kas jums svarbu: angos galite išnaudoti iš savo stoties. Dėl šios priežasties, už pasirinktą karūną, programa nėra atkreipti linijas, bet spalvos lokatoriaus kvadratų (2 ° × 1 °) stotys dalyvauja. Rezultatas yra "aktyvių zonų" žemėlapis: kur šalia Lilac kvadratų atsiranda, yra konduit arba atidarymo, net jei jis nėra susijęs su jumis tiesiogiai; Jei plotas išsiplečia į jus, tai yra signalas nukreipti anteną ta kryptimi. 45 ° prietaisų skydelis leidžia jums pamatyti pagal tropo indeksą ir tinklelį, todėl palyginti iš pirmo žvilgsnio prognozę (apatinė spalva) ir nekilnojamojo eismo (APM). Jūsų QTH aikštė niekada nespalva, nes tai yra išeities taškas, o ne tolima zona. Spalvoti tik tie kvadratai, kurie yra už karūnos ribų: karūną kertantis arba viduje esantis kvadratas lieka laisvas, net jei yra viena iš dviejų stočių. Norėdami pamatyti linijas, tiesiog išjunkite langelį arba pasirinkite didesnę karūną.

## 11. Temos ir kalbos

Temos: 18 bendra IC8TEM programas, kaip YA- NEC ir E- LAYER ANALIZER: Skaidrus, naktį, juoda, aštuoni pastelės (mėlyna, oranžinė, akvamarinas, žalia, rožinė, lilac, indigo, ruda), Scia meteorica (YA- MS paletė), IP- TRX- CTRL trys (Chiaro, Notte, Nero) ir mėlyna, violetinė, žalia. Nuo 1.3.4 Chiaro atrodo kaip YA- NEC: šviesiai pilkas fonas, sukepinti baltos spalvos plokštės, mėlynos nakties skirtukai ir mėlynas akcentas. Tamsiose temose taip pat tampa tamsus bazinis žemėlapis. Prognozės spalvų skalė išlieka ta pati visose temose. Siauruose ekranuose (žemiau 1500 pikselių, pvz., 1366 × 768) laikrodis viršuje rodo tik valandą, todėl tema, tekstų žymeklis ir kalba išlieka matomi. Kalbos: YA- MS [17] 28-oji wiki (anglų, Travel, Deutsch, Nederlands, Français, Español, Português, Dansk, Norsk, Svenska, Íslenska, Eesti, Lietuviai, Latviešu, Руский, кра, нська, TürkaÅ ¡ch ir Urdu viso lango veidrodžiai iš dešinės į kairę; žemėlapis lieka kaip [12]. Vertimai susiję su sąsaja; du techniniai dokumentai ir pranešimai būsenos juostoje lieka italų kalba. Dienų pavadinimai ateina iš tarptautinių nustatymų. Pasirinktos kalbos tinklas.

Tema ir kalba pakeisti iš dviejų meniu viršuje dešinėje, šalia laikrodžio, ir taikyti iš karto, be perboot programą ir nenutraukiant duomenų rinkimo. Pasirinkimas išsaugomas LIVETROPOVHF.inii.

## 11 bis. Įtariamos ir savarankiško mokymosi ataskaitos

Įtariamų ataskaitų skirtukas surenka viską programos kontrolė rasti keista, sugrupuotas pagal tipą ir pavadinimą, su paskutiniais 24 valandų sąraše. Programa išmoksta pati: kiekvienai porai (tipas, pavadinimas) įvertinami teigiami rezultatai (+) ir priešingai (−), o po 3 sutartų rezultatų sukuria automatišką taisyklę; priešingas rezultatas kelia klausimą. Vartotojo sprendimai (geras, blogas) visada taikomos; pulsante mygtuką grąžina sprendimą į programą. Taisyklės yra duomenų / APGRENDIMENTO. TXT, šalia programos; automatiniai tie, kurie nėra stiprinami metus yra pamiršti.

| Tipas | Kada | Išj. + / - | Taisyklių poveikis |
| EME ( Q65/JT65) | Q65 arba JT65 daugiau nei 1000 km su Mėnulis virš horizonto abiejų galvučių, arba EME komentaras | - kiekvieną kartą, kai mėnulis yra dažnas | Išmesti: EME stotis, jos ilgas Q65/JT65 s taip pat atmesti be paprasto mėnulio. Tinkamas: traktuojamas kaip tropas |
| Ilgas sujungimas | Daugiau nei 1000 km, karantino | + atidengta, - pasibaigęs be patvirtinimo | Gerai: stoties ilgis rodo nedelsiant. Atsisakyti: niekada |
| Klausytojas | Comment | - išmestas, + rekultivuotas žmonių grupių | Gerai: niekada neišmesta. Išmeskite: visada |
| Skirtingas nuomotojas | Kvadratinis gavo skiriasi nuo CALL3. TXT | + kiekvieną kartą, kai kartojate | Gerai: stotis persikėlė, verta kvadratinių gavo. Išmeskite: vale CALL3. TXT |
| Kvadratinis centras | 6 simbolių su LL arba MM |  —  | Tik informacija |
| Automatinis imtuvas | Paskyrimas su -SDR, / SDR, - # arba -n |  —  | Tik informacija |

EME. Q65 ir JT65 yra bendravimo per Luna būdai: Teoriškai jie patenka į pašalinimo straipsnį, pavyzdžiui, sklaidos meteoras, bet jie taip pat naudojami tropo. Programa apskaičiuoja Mėnulio padėtį su žemo tikslumo formules Almanac astronomijos (apie 0,3 °) [20]: jei Q65 arba JT65 jungtis viršija 1000 km ir Mėnulis yra virš horizonto abiejų galvučių, jis yra beveik tikrai EME ir yra pašalintas (jis skaičiuoja tarp pašalinių). Jei Mėnulis nėra dažnas, ryšys išlieka įmanomas trope ir eina per ilgų jungčių karantiną, bet signalas išlieka matomas. Ji taikoma grupėms, MQTT ir jos UDP dekodavimams.

## 11 ter. Kontaktiniai išmetimo kriterijai, po vieną

Programa nori parodyti tik tropošerijos sklidimo 144 MHz ir daugiau. Kiekvienas gaunamas duomenys (Cluster Row, PSKReporter Report via MTQTT, UDP dekodavimo) eina per filtrų serijos, visada ta pačia tvarka. Atmesta duomenys neišnyksta nieko: yra skaičiuojamas (pašalinti viršutinėje juostoje kontaktas) ir, kai priežastis yra abejotina, rodo skirtukas Sustabdytas Ataskaitos.

|  N.  | Kriterijai | Kur jis? | Ką atmesti | Gali pasikeisti? |
|  1  | Grupė | klasteris, MTQTT, UDP | Visi žemiau 143 MHz (taip pat 50 ir 70 MHz), išskyrus Graves 143,050 MHz. Laikomos 144, 222, 432, 1296, 2320 MHz ir 10 GHz juostos | Ne. |
|  2  | Palydovinė QO- 100 | klasteris, MTQTT, UDP | 2400,0- 2400,5 MHz ir 10489,5- 10490 MHz MHz: geostacionarus palydovinis eismas, o ne tropas | Ne. |
|  3  | Meteorio sklaida | klasteris, MTQTT, UDP | JT6M ir ISCAT (taip pat ISCAT- A / B), komentarai su "&" ir non- tropoksi sklidimo etikete (< AU >, < AUE >, < ES >, < FAI >, < F2 >) visada. MSK144, FSK441 ir JACTY (taip pat su < MS >) tik daugiau nei 250 km: 250 km mes sutinkame, nes tokiu atstumu šie režimai taip pat naudojami tropo | Ne. |
|  4  | EME (per Luna) | klasteris, MTQTT, UDP | EME parašyta komentare; Q65 arba JT65 daugiau nei 1000 km, kai Mėnulis yra virš horizonto abiejų galvučių; stotys išmoko kaip EME | Taip: Gera taisyklė įtartinais signalais |
|  5  | Netikras klausytojas | unit description in lists | PSKReporter imtuvas, kuris sako išgirsti 144 stočių, veikiančių tuo metu kitose juostose (bent du): Jo santykiai atmetami. Atgal geras, jei DX žmonių grupės patvirtina tas pačias stotis | Taip: gerai / blogai |
|  6  | Ilgų jungčių karantinas | visos eilutės | Daugiau nei 1000 km linija lieka paslėpta tol, kol suranda patvirtinimą: ta pati stotis ta pačia kryptimi, kitas maršrutas 200 km koridoriuje arba lygiagretus maršrutas 10 ° ir 200 km atstumu bent per pusę. Be patvirtinimo baigiasi po 60 minučių | Taip: gerai / Išjungta į stotį |
|  7  | Mažiau nei 100 km | eilutės ir sąrašas | Su on-box, trumpiausias jungtys 100 km (žemės banga, optinis diapazonas) | Dėžė, išjungta paleidžiant |
|  8  | Už QTH karūnos | eilutės | Jei stotelė yra daugiau nei 1000, 700 arba 500 km (jūsų pasirinkimu) nuo jūsų QTH, nėra linijos: tik illa kvadratų padengti dviejų stočių. Jūsų QTH kvadratas niekada spalva | Dėžutė ir spindulys, 1000 km |
|  9  | Laiko langas | viskas | Senesni taškai pasirinktame intervale (nuo 3 iki 60 minučių) | Meniu viršuje |
|  10  | Dvigubas | klasteris | Ta pati vieta (tas pats DX, tas pats lankytojas, ta pati juosta, tas pats šaltinis) pakartojo | Ne. |

Pastaba dėl 4 ir 6 kriterijų. Daugiau kaip 1000 km Q65 arba JT65 jungtis be bendro Mėnulio galima tropolyje: ji neišmetama pagal 4 kriterijų, bet laikoma karantine pagal 6 kriterijų kaip bet kuri kita ilgoji jungtis. Tiesioginiai skambučiai Jums (9 dalis quater) yra vienintelė 7 ir 8 kriterijų išimtis: jie visada atkreipti.

Kas nėra švaistymas. Vietos rodiklio pataisymai (CALL3. TXT su ranka balsai, pataisymai iš klasterio ir išmoko lokatorių, priesagas -SDR, centras kvadratinių LL / MM) pokytis, kur jūs atkreipti stoties, o ne jį atmesti. Stotis be jokio savininko, kita vertus, negali būti sudarytas: lieka sąraše su atstumu "-".

## 11 kvartalų. Spalvos ir simboliai

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

## 12. Nustatymai ir perkeliamumas

Langas. Pradžioje langas užima 90% ekrano naudingo ploto (iškirpti aplikacijų juostą), centre, taigi ant 15- 16 colių užrašų knygelės su 125% arba 150% dydžio antraštės juosta išlieka matoma. Jei naudingoji erdvė yra žemiau 1280 × 720 taškų, lango dalis maksimizuota. Dydis, padėtis ir maksimizuotas statusas išsaugomi LIVETROPOVHF. ini uždarymo ir pabaigos kitą pradžią, jei pavadinimo juosta vis dar patenka į ekraną (pavyzdžiui, atjungus išorinį monitorių); Priešingu atveju jūs grįžti į 90%. Mažiausias dydis yra 900 × 560.

Programa yra nešiojama: niekada nerašo AppData aplanke ar registre. Viskas yra vykdomojo failo aplanke: Constellation name (optional) (Parametrai, sukurti pirmojo uždarymo), CALL3.TXT (su ranka parašyta balsų, išmoko pataisymai ir lokatoriai), žemėlapių talpyklos ir orų duomenys, taisyklės išmoko (Dati / APPrENDIMENTTO.TXT) ir LIVETROPOVHF.db archyvas. Taip pat galite laikyti USB laikrodis, tol, kol aplankas yra rašomas.

Ad archyvo (SQLite [14]). Kiekviena priimta vieta ir kiekvienas PSSKReporter klausymo ataskaita išsaugoma LIVETROPOVHF.db, SQLite duomenų bazėje viename faile. Atmintyje tik paskutinės 60 minučių lieka sąraše ir žemėlapyje; archyvas išlaiko viską ir tarnauja istorikui ir statistikai (taip pat UDP dekoderiai), pavyzdžiui, juostos ir per dieną angos, maksimalūs atstumai, kvadratų pasiekė, ir galutinis palyginimas tarp numatomo indekso ir realių dėmių. Iš filtro išmestos juostos suskaičiuojamos, bet nearchyvuojamos.

## 12 bis. Automatinis įrengimas ir atnaujinimas

Nuo versija 1.0 programa jau atsisiunčia paruoštas iš Išleidimo puslapyje github.com / ic8tem / LIVETROPOVHF, į tuos pačius paketus YA- MS: montuotojas Windows (64 bit, 32 bit ir ARM, įdiegti C:\ LIVETROPOVHF be administratoriaus teisių) ir nešiojamas zip; Windows 7 SP1 ir 8.1 paketai atskirai (-windows7- x64 ir -wind.deb,.rpm, Arch, Slackware, Appphit ir tar.gz Linux; paketai Aviečių Pi (64 ir 32 bit) ir Alpių;.dd ir apps.zip Mac (Apple Silicon and Intel). Su Linux paketais, programa nukopijuota į pirmą pradžią ~ / LIVETROPOVHF, todėl nustatymai ir duomenys lieka šalia programos; Mac duomenys gimsta šalia LIVETROPOVHF.app.

Kas 30 minučių (pirmoji patikrinti keletą sekundžių po pradžios, iš karto, jei paskutinį kartą programa buvo uždaryta dėl klaidos) programa skaito viena, nieko neprašant, paskutinis leidimas. Jei versija yra naujesnis atsisiųsti nešiojamą paketą, tinkamą sistemai ir pakeičia programos failus vietoje: tie, kurie naudojami yra pervadinti *.old-update ir ištrinti kitą startą. Nustatymai, archyvas, Duomenų aplankas ir CALL3. TXT neliečiamas. Nauja versija veikia nuo kitos pradžios: montuojant baigtą langą siūloma paleisti iš naujo iš karto; skubiems pataisymams, pažymėtiems [privaloma] išleidimo metu, perkrovimas negali būti atidėtas. Kontrolės statusas ir naudojama versija užrašomi būsenos juostos apačioje; išjungti atnaujinimus = 0 LIVETROPOVHF.inii "Mac" programa yra pasirašyta ir nekeičia pati: programa tik praneša, kad yra nauja versija.

LIVETROPOVHF vaizdas su versijos numeriu pasirodo pradžioje; pabaigoje programa prašo patvirtinimo ir ačiū už naudojimą.

## 12 ter. Klaidų žurnalas ir būsenos juosta

Bet kokia nenumatyta klaida baigiasi duomenų / errori.log, net su skaičiavimo žurnalo off: data ir laikas, versija, sistema, atvira kortelė, visiškai klaida ir paskutinės eilutės, ką programa veikė. Sąsajos klaida uždaro programą: būsenos juostoje pasirodo "anno anoted klaida" (spustelėkite: atverti registrą). Kitoje pradžioje programa klausia, ar siųsti ataskaitą: atidaro naršyklę GitHub signalas jau kompiliuotas, su paskyrimu, lokatorius ir vartotojo vardas kompiuterio, kurį jūs patikrinti ir siųsti su nemokamai GitHub sąskaitą. Žurnalas taip pat baigiasi kiekvieną atnaujinimo patikrinimą.

Būsenos juostoje šaltinio rutuliai (DX klasteris, RBN, PSKReporter, MQTT, UDP) yra paspausti ir atnešti į šaltinį; komiksas sako, kodėl šaltinis yra išjungtas: atmestas prievadas, klasterio mazgas, dislokuotas serverio pusėje, neatsako, ar UDP klausytis, bet be paketų iš WSJT-X, JTDX ar MSHV. Vienas paspaudimas ant versijos numerį nedelsiant patikrinti atnaujinimus.

## 13. Platformos

Išorinės knygutės: Avalonia [12] sąsaja, MQTnet kaip MQTT klientas realiu laiku srauto SKReporter [13], Microsoft. Data.Sqlite [14] archyvui.

C # on.NET 10 (LTS) [15] su Avalonia sąsaja, paskelbta kaip nepriklausomas vykdomasis Windows 64 ir 32 bitai, Mac Intel ir Apple Silicio, Linux Debian ir Aviečių Pi (64 ir 32 bitai). Nurodytos ribos: Windows 10 / 11; MacOS 14 arba vėliau, su programėlėmis pasirašyti arba atidaryti teisę → Open; Aviečių nuo Pi 2 / Zero 2 W iki (ARMv6 modeliai nėra palaikomi.NET).

## Ačiū
Dėka I5MZY Massimo Zannoni bandymų Windows 7 ir Windows 8.

3 tyrimo grupės (© ITU) skaitmeniniai produktai, naudojami asmeniniais ir nekomerciniais tikslais.

## Pagrindiniai šaltiniai

1. S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", pristatymas 41-ajame Šiaurės VUSHF susitikime, Skjeberg 2019. Troposterical Būdai, Įvykių valdymo sąrašas (Hepburn / F5LEN prognozė, aukštos temperatūros profiliai su Windy, DX klasteris atidarymo bandymas), pastebėjimas, kad tik prognozės žemėlapiai nėra pakankamai ekstremalių maršrutų.
2. J. N. Gannaway G3YGF, "Tropospheric Scatter Propagation", QST 1983 m. lapkričio mėn. (iš anoniminio straipsnio dėl radijo ryšio, RSGB, 1981 m. rugpjūčio mėn.). Laisvos erdvės praradimo ir sklaidos formatai, lygiavertis spindulys 8497 km, 10 dB per horizontą, klimato terminas 0.2 · (N − 310), įrenginio galingumas ir signalo bei triukšmo santykis, pvz., 144 MHz.
3. W. R. Hepburn, DX Info Centre, "Tropospheric Ducting Forecast" (dxinfocent.com / tropo.html) ir "The Hepburn Tropo Index (HTI)". Rodyklės skalė ir reikšmė, prognozavimas 6 dienas.
4. F5LEN, prognozė Europai (tropo.f5len.org / prognozės- for- European: sąrašas vaizdų puslapio scenarijų, klasės legenda Disturb... Duct vaizdas) ir pasaulio prognozė (tropo.f5len.org / WW) ir dėmesį "Nauja versija pasaulio VHF troposferos dauginimo prognozavimo įrankis". Indeksas pagrįstas reaktyvumu N vienete, 40 žingsnių nuo 3 valandų, atnaujinti 4 kartus per dieną.
5. mqtt.pskreporter.info, distribution MQTT realiu laiku iš pskreporter.info reklamos (kuruojama M0LTE, srautas P. Gladstone naudojamas su leidimu). Durys 1883- 1886, pskr / filtras / v2 temos schema.
6. Projekto santrauka 8 / 11 / 2026 ir Go testas (core.go, gui.go) pateikė programos autorius. Ad Taisyklės, neigiamas filtras, dažnių juostos segmentai, telnet klasterio valdymas, temos, kalbos ir perkeliamumas.
7. CALL3.TXT, paskyrimo duomenų bazė → JTDX nuomotojas (I. UA3DJY, A. ES1JA scenarijus, JTAlert / hamspots.net duomenys). Projekto faile patikrintų pavadinimų formatas ir skaičius.
8. WSJT-X, šaltinio kodas, NetworkMessage.hpp failas (UDP protokolo aprašymas: Heartbeat, Statusas, Decode). Bendroji nuoroda, nepridedama; JTDX ir MSHV naudoja tą patį formatą.
9. Atvirkštinis Beacon Network (reversebeacon.net), paslaugų puslapių telnet mazgo. Uostai 7000 (CW, RTTY) ir 7001 ( FT8), prisijungti tik su pavadinimu.
10. D. Mills, RFC 4330 "Simple Network Time Protocol (SNTP) Version 4", IETF 2006. Paketo formatas, atliekų apskaičiavimas ir grąžinimo vėlavimas. Bendroji nuoroda, nepateikta priede.
11. NTP Pool Project (ntppool.org); INRIM, NTP time service (ntp1 / ntp2.inrim.it); PTB (ptbtime1.ptb.de). Laiko kontrolės serveriai. Bendroji nuoroda, nepateikta priede.
12. Avalonia UI, oficialūs dokumentai (docs.avaloniaui.net). Multiplatform sąsaja, temos ir išdėstymas iš dešinės į kairę. Bendroji nuoroda, nepateikta priede.
13. MQTnet,.NET biblioteka MQTT (github.com / dotnet / MQTnet). Klientas naudojamas PSKReporter srauto. Bendroji nuoroda, nepateikta priede.
14. SQLite (sqlite.org) ir Microsoft. Data.Sqlite (learn.microsoft.com). Archyvas viename faile. Bendroji nuoroda, nepateikta priede.
15. Microsoft,.NET 10 - palaiko operacines sistemas ir savarankišką skelbimą viename faile (learn.microsoft.com / dotnet). 13 skirsnyje nurodytos platformos ir ribos. Bendroji nuoroda, nepateikta priede.
16. ECMA- 376 "Office Open XML Film Formats", 1 dalis, 22.1 skirsnis (Office Math Markup Language). Struktūra.docx failą ir lygtis. Bendroji nuoroda, nepateikta priede.
17. Programos autoriaus pateiktas 28 YA- MS wiki kalbų sąrašas.
18. Open- Meteo, orų prognozė API (open- meteo.com), su duomenimis iš NOAA GFS modelis. Izobarų slėgis jūros lygyje (žr. 2 dokumentą).
19. J. P. Snyder "Žemėlapis projekcijos - darbo vadovas", USGS Professional Paper 1395, 1987, ir standartiniai daugianacionaliniai transformacijos metodai geoorientaciniai vaizdai. Bendroji nuoroda, nepateikta priede.
20. Astronominis almanakas, D skirsnis, "Mažo tikslumo formulės Mėnuliui" (precizinė padėtis apie 0,3 °). Naudojamas EME sujungimams atpažinti. Bendroji nuoroda, nepateikta priede.
