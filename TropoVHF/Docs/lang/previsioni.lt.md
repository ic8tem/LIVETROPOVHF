# Prognozės vadovas

> Automatinis vertimas iš italų kalbos: jei kyla abejonių, verta italų kalbos gido.

NAME OF TRANSLATORS Čia aprašomi visi duomenys, kaip jie apskaičiuojami ir iš kurio jie gaunami. Skaičiai laužtiniuose skliaustuose nurodo šaltinius apačioje; programoje kiekvienas rezultatų duomenys turi komiksų (simBolo I) su tuo pačiu šaltiniu ir nuoroda į punktą.

## 1. Ką jis daro ir ką jis daro

BFS [1, 2] modelio vertikalios temperatūros ir drėgmės profilių prognozavimas sukelia oro sluoksnio reaktyvumą pagal sluoksnį ir įvertina, kiek signalo prieaugis, palyginti su normalia atmosferA. Maršrute prideda tikrą žemę (Copernicus GLO-30 [3]), difrakcijos, troposcatter, ortakiai ir imtuvų triukšmo.

Nëra priemonë: tai ávertinimas, atliktas pagal orinio modelio su tolimais dydþiais šimtais metrø. Čia galima palyginti situacijas (šiandien geriau nei vakar, 70 cm geriau nei 2 m, tai geriau nei tai gydyti), o ne užtikrinti ryšį. 14 dalyje aiškinama, kaip rinkti įrodymus, kad juos patvirtintų.


> "Draudimas negali būti apibendrintas, bet buvo susijęs su vietine morfologija."
> - Volker Grasmann, DF5AI, pranešė Flavio Egano, IK3XTV (Propagacija 2 metrais)

Faktas, pagal kurį remiamasi skaičiavimu taškais: kiekvienas maršrutas yra apskaičiuojamas ant jo dirvožemio, su jo horizontais, jūra ir reljefais, žemėlapyje pateikiamas bendras vaizdas, pateikiamas atsakymas.

## 2. Kalbų komandos

- grupė: 144, 432 arba 1296 MHz. Žemėlapį ir skaičiavimą naudoti pasirinktą juostą.
- Dabar: Žymeklis juda prognozę nuo dabar iki 72 valandos į priekį, per 3 valandas.
- Patobulinta prognozė (matoma zona): atsisiųskite GFS profilius ne daugiau kaip 240 taškų tinklelyje, apimančiame matomą žemėlapį. Tinklelio žingsnis (ne mažesnis kaip 0,5 °) priklauso nuo didinimo.
- Stop: pertraukti atsiuntimą arba apskaičiavimą.
- Detaliau parodyti (dešinysis skydelis žemėlapyje): trijų lygių, esminis, normalus ir pilnas, įjungti langelių grupę; kiekvienas langelis gali tada pakeisti ranka. Langeliai yra trijų grupių:
  - Žemėlapyje: izolee del Gain, taip pat 3-7 dB (traukos), izobare, terminė inversija, oro ženklai S ↓ ↑, koreliacijos padidėjimas (presavimas), tiesioginio matymo zona (4, 5, 7 ir 9 bis dalys). Spalvos visada išlieka.
  - Renkant signalą: visos skaičiavimo eilutės, plokščias Žemės profilis, skaičiavimo žurnalas ir išsamus failų žurnalas.
  - Pataisymai ir gyvieji duomenys: oro pataisymai (Misme, subsidence), kontrastas ore, vidurkis su dėmėmis (corridoI), APRS 2 m šuoliai ir LORA, virš kalno be nuostolių (4 bis, 9 bis, 9 quater ir 10 bis).
  - Itin svarbus turi tik izoliatus, oro ir žiniasklaidos korekcijas su dėmėmis; Normalus prideda izobare, apverčiamas, oro ženklai, tiesioginis vaizdas, plokščia žemė, oro jūra ir APRS; Užpildykite jį įjungia. NAME OF TRANSLATORS
- Duomenų šaltinis ir istorijos atvejis: iš ten, iš kur gaunami profiliai ir kuri diena jums atrodo (3 bis ir 3 ter dalys).
- Mano stotis ir korespondentas: Lokatorius, antenos, galia, kabeliai, triukšmas (7 ir 12 dalys).
- Signalas ir aprėptis apskaičiuojami optika: du skaičiavimai ant žemės (8-13 pastraipos).
- skaičiavimo registras: kiekvienas naudojamas žingsnis su skaičiais, įskaitant duomenų / prognozavimo-calcoli.log failą.

## 3. For GFS data to profil

Ant kiekvieno tinklelio taško ir kiekvieną valandą skaitome slėgį ant žemės, temperatūrą ir drėgmę 2 m atstumu, vėją 10 m atstumu, ribinio sluoksnio aukštį, CAPE ir temperatūrą bei drėgmę 1000, 975, 950, 925, 900, 850, 800, 700, 600 ir 500 hPa.

Lenktynių yra apskaičiuojamas su iptomic formulę ir virtualios temperatūros; lygių žemiau žemės yra mažai. kiekvieno lygio:

- N = 77,6 / T · (P + 4810 · E / T), T su kelvinu, P ir hPa, ir ITU- R P.453-14, eq. 9 soties slėgiu, su AP korekcijos koeficientu (pakartotinai kontroliuojama oficialiame tekste) [4].
- M = N + 157 · h, su h km: modifikuotas reaktyvumas. Ten, kur eina M, yra kanalas.

## 3 bis. Duomenų šaltinis

Duomenų šaltinis meniu pasirinkti, iš kur profiliai yra:

- Automatinis (rekomenduojamas): GFS pirmą kartą iš NOMADS [14], NOAA paslauga, kuri platina modelį: kiekvieną akimirką programa prašo dviejų iškirpimas srityje G RIB2 formatu ir skaito juos iš savęs; NOMADS neturi dienos limitą, tik vieną per minutę, kad programa gerbia. Platūs tinklai (nuo 0,5 ° iki daugiau) naudoja failus iki 0,5 ° arba 1-1 kas 3 valandas, daug lengvesni. Požymiai, kad NOMADS neduoda, arba visus, jei NOMADS neatsako, paklauskite Open-Mete- [2]. Abu šaltiniai suteikia tą patį modelį ir tas pačias lenktynes, todėl jūs galite maišyti tame pačiame tinklelyje.
- GFS iš Open-Meteo: tik Open-Meteo, kaip 1.2 (ne per minutę tikimasi).
- GFS iš NOMADS: tik NOMADS. Lėčiau atsisiųsti (du failai per valandą, pusę sekundės tarp vieno prašymo ir kito ne viršyti NOAA ribą), bet ne vartoti Open-Meteo ribą.
- ICONEES (DWD) ir ECMWF IFS 0,25 °, iš Open- Meteo: modeliai, išskyrus BFS, kurie taikomi visame tinkle ir maršrutams. Jais negalima maišyti su GFS: lyginti du modelius tuo pačiu metu yra paprastas būdas pamatyti, kaip prognozė yra neaiški. ICOES apima tik Europą; ECMWF neturi visų GFS slėgio lygių (trūkstamų lygių šuolis) ir gali neturėti ribinio lygmens.

Šaltinis naudojamas yra parašyta statuso eilutėje ir pirmoje eilutėje rezultatų (duomenų šaltinis), su kiek taškų atėjo iš kiekvienos paslaugos ir kiek iš talpyklos. NAME OF TRANSLATORS

## 3 ter. Istoriniai atvejai

Istorinis Bylos meniu apžvelgia dokumentais įformintą atidarymą su Open-Mete- [15] archyvo GFS prognozes: žemėlapis juda į bylos sritį, juosta nustato save ir laiko žymeklį palieka nuo pirmosios bylos dienos. Spaudžiant Update prognozę atsisiųsite tų dienų profilius; signalo skaičiavimas veikia kaip dabartinės prognozės. Čia galima palyginti modelį su tuo, kas iš tikrųjų įvyko.

Pirmasis atvejis yra 2022 m. rugpjūčio 14- 15 dienomis QST (lapkritis 2022, "Pasaulis virš 50 MHz" [23]): atsidarymas aukšto slėgio tarp Manitobos ir Kanzas / Misūrio, nuo 144 iki 1296 MHz FT8, su Kanados rekordu 10 GHz 1228,56 km Q65 ir signalai iki 1941 km. Antrasis - nuo 2021 m. gruodžio 29 d. iki 2022 m. sausio 2 d. atidaroma 144 MHz tironijos ir Vakarų Viduržemio jūros regiono dalis, iš IC8TEM ataskaitų: pirmosios angos 29 (iki 737 km), platesnės 31 ( IT9GSF - EB5EA 1217 km), puiki anga sausio 1 d. tarp Italijos, Sicilijos ir Ispanijos. Senesniems atvejams (pvz., 2000 m. rugsėjis) Open-Meteo archyvas neturi duomenų. Archyvui trūksta ribinio sluoksnio aukščio: ten jo pataisa netaikoma.

## 4. Indeksas ir spalvos

Kiekvienam sluoksniui tarp dviejų lygių programa lygina N lašą su 6.5 lentelės ribomis Bean ir Dutton [5]: pavyzdžiui, nuo 1000 iki 850 hPa yra normalus sumažėjimas iki 60 N / km, daugiau nei 100 N / km yra superrefrakcija. Itin lūžio sluoksnio vertė yra 10-15 dB pelno; konduktas, kuris sutraukia juostą nuo 20-25 dB, pagal M. Stipraus vėjo deficitą į žemę, aukštos ribos sluoksnis ir aukštos CAPE sumažinti pelną, nes jie pašalinti arba nutraukti sluoksniai [5, 4.4 dalis].

Spalva yra apskaičiuotas pelnas dB, su fiksuoto masto visoms temoms:

| Gain | Spalva | Nuoroda |
| mažiau nei 7 dB | Niekas. | beveik normali atmosferA |
| 7-10 dB | mėlyna | gradientas, esant normai |
| 10-13 dB | Žalieji | superrefrakcijos sluoksnis |
| 13- 16 dB | geltona | stiprus sluoksnis, bet be ortakio juostos |
| 16- 18,5 dB | oranžinis | labai stiprus sluoksnis arba per plonas ortakio |
| 18,5- 21 dB | raudonas | Comment |
| nuo 21 dB | Migdolai | Stiprus ir gilus ortakio |

Ekrane taip pat yra 3 - 7 dB zonos nuo 3 iki 7 dB, kur kažkas formuojamas, bet lieka po skale, Jūs galite matyti su šviesiai pilkos apsaugos; spalvos nesikeičia. Nulupta plokštė demimituoja plotą, kurį apima jos paties prognozė, ir rodo maksimalų matomą prieaugį: jei jis yra mažesnis nei 7 dB, žemėlapis lieka be spalvų, nes atmosferA yra beveik normalus, ne dėl to, kad trūksta duomenų. Atsiunčiant naują paspaudimą Update prognozė nenutraukia; sustabdyti tai yra Stop.

Spalvos yra nepermatomos 82%, todėl temos fonas tik pakeičia juos. Lokatoriaus tinklelis ir kvadratiniai pavadinimai yra skirti virš spalvų. Ten, kur jūsų prognozė nėra, bet Hepburn arba F5LEN duoti 5 ar daugiau šachmatų kortelės atrodo su to šaltinio spalva.

Žemėlapis teisia kiekvieną tašką. Signalo apskaičiavimas, vietoj to, kontroliuoja, kad sluoksnis yra nuolatinis palei maršrutą (9 pastraipa): kelias, kuris kerta labai geltonos gali duoti mažiau nei žemėlapis leidžia jums galvoti.

## 4 bis. Prognozuojama iš reklamos: koridoriai (nuo 1.3.3)

Copyright Policy: Nuoroda tik niekada turi atkreipti tinklo. Izoliuotas ir trumpas priėmimas gali būti tranzitinės plokštumos sklaida (APRS paketas yra labai trumpas), kelių minučių atidarymas arba klaida, ir būtų "drop-elgesio" jie apgauti. Programa pripažįsta koridorių, kai, toje pačioje juostoje:

- ne mažiau kaip 3 poros nepriklausomų stočių (be bendros stoties) dirba ne mažiau kaip 300 km maršrutais,
- maršrutai yra lygiagretūs: azimutas - ± 15 °, vidutiniai taškai - 150 km nuo bendrosios ašies;
- visas pastarąsias 30 minučių, kai spindulių pluoštas kartojasi bent 10 minučių (patvarumas);
- ARS atveju šuolis skaičiuojamas tik tuo atveju, jei tas pats šuolis kartojamas;
- nuo 1.3.4 maršrutai, kai atstumas yra didesnis nei 1500 km, skaičiuojami tik tuo atveju, jei spindulis gaunamas iš bent dviejų skirtingų šaltinių (pvz., PSKRenerio ir klasterio): nuo vieno šaltinio iki tų atstumų 2 m atstumu dažniau yra EME, sklaidos meteoritai arba klaidingi lokatoriai, nei tropos.
- nuo 1.3.4, neskaičiuoti stočių "ray": kurie per paskutinę valandą turi ne mažiau kaip 5 korespondentai per 1000 km pasklido per ventiliatorius ne mažiau kaip 150 °. 2 m tropas yra kryptis; imtuvas, kuris "nemokamai" kartu Izraelyje, Portugalijoje, Anglijoje ir Italijoje nuo 2000-3700 km, dažnai su teigiamais signalais, beveik tikrai dažnis sukonfigūruotas blogai. 17-22 / 11 / 2026 I5MZY archyvo 72% apgyvendinimo 2 m virš 1000 km atvyko iš stočių, todėl 22 / 11 10: 53 UTC buvo OH6HLH, OZ1ITS, F4LUK, OM8ABC ir SP2JLR, ir su filtru ilgiausias koridorius nusileidžia nuo 2175 km (regole 1.3.3) iki 615 km.

Šaltiniai: klasteriniai intarpai (ne tie, kurie pažymėti AS / ACS), PSKreporter per MQTT, jų QSO ir WSJT-X / JTDX / MSHV dekoracijos, ir APRS šuoliai 2 m atstumu ir LORA 433 / 868 MHz.

APRS: 2 m ir LORA. Programa skaito APRS- IS tik iš naujo (rotate.aprs2.net, filtras Europoje) ir išskiria radijo šuolius tarp žinomų vietos stočių (šaltinis → Digipeater → IGate), pavyzdžiui, VHF žemėlapį DXView ir APRS PropView (RF- YVY, Apache 2.0). Trys juostos yra vertingos: 2 m, LORA 433 MHz (432 juostos koridoriai) ir LORA 868 MHz (1296 MHz skaičiuojant corridoI: 868 MHz taip pat aukščiausiais dažniais). Juosta ateina iš LoRA totall (aprs- deviceid sąrašas) ir iš dažnių ar žodžių komentare; šuolis yra tik verta tarp dviejų stočių tą pačią juostą.

Subtilus taškas yra aktyvus kartotuvas: ne 500 km gaunamas signalas gali būti pateiktas su dviejų arba trijų digipeater retransmisijas. Šuolių skaičius, todėl tik jei jis yra tarp dviejų realių ir vienas po kito einančių siųstuvų:

- kelias turi būti atsektas: kiekvienas retransliuotas digipeteris įdėjo savo pavadinimą, o šuolis matuojamas tarp vieno ir kito; be pavadinimo naudojamas slapyvardis (W IDE1 *, W IDE2 *, regioninis slapyvardis) nutraukia grandinę ir ta atkarpa neskaičiuojama;
- paslėptas retransliacijas: programa prisimena kelią kiekvienas šaltinis paprastai prašo (pavyzdžiui W IDE2 -2). Neigiamas poveikis aplinkai IDE2
- Digipeater kelio viduryje: jei tarp dviejų stočių, daugiau ar mažiau kelyje, yra tos pačios aktyvios juostos digipeater per pastarąsias tris valandas, šuolis yra išmestas, nes jis galėjo būti retransformuoti (tai ypač digipeater LORA, kad retransliuoti nekeičiant kelio);
- ne radijo paketai (qAC, QAX, TCPIP), tie, kurie eksploatuojami internetu radijo iš IGate (trečiosios šalys), HF ir įvairių dažnių, stotys daugiau nei 1500 m (Palloni, orlaivių) ir šokinėja 1500 km.

(nuo 1.3.4). VGN (VGN) Visų koridorių juostos sudedamos, neutralios spalvos (tekstas) ir pusiau skaidrios: dubliavimosi nėra, o spalva nėra painiojama su skalės violetine spalva (21 dB ir už jos). Etiketės (požymiai, vidutinis atstumas, mažiausias pelnas) skenuojamos viena su kita, per pelę ant etiketės pasirodo koridoriaus dalys.

rajone, ir tik netoli dabartinio laiko (± 90 minučių), prognozė nėra minimalus. Argumentai yra: jei stotys tikrai praeis, sklidimas yra ten, kad ir ką oro modelis sako.

Nes trumpi maršrutai yra mažai verti (nuo 1.3.4). 2 m, tarp 300 ir 600 km, dvi geros stotys (apie 100 W, antenos, FT8 arba CW) sujungti beveik kasdien, net su normalia atmosferA, už troposcatter: signalas sklinda mažų pažeidimų oro bendro tūrio dviejų ryšulių. Trijose ar keturiose vietose, esančiose 500 km, sakoma, kad juosta veikia kaip įprasta, bet ne kad yra anga: GEKO Žurnalas n. 69 ( IU3QEZ) apskaičiavimai troposcatter su 2 × 10 tekančių elementų apie 440 km SSB QRP, 510 km SSB su 50 W, 600 km CW QRP ir 640 km SSB su 500 W (šaltiniai, vienuolika ratas). Vos už 600-700 km troposcatter jau nėra pakankamai, ir iš lygiagrečių jungčių serija tikrai rodo jungtį arba sluoksnį. 150-600 km koridorius iki 1.3.3 pakėlė prognozę iki 9-12 dB, o žemėlapis parodė, kad padėtis nepagerėjo.

Pradinė vertė susijusi su vidutiniu maršrutų atstumu:

- iki 400 km: 3 dB;
- 400-700 km: druska tiesia linija nuo 3-10 dB (500 km → 5,3 dB; 600 km → 7,7 dB);
- 700-900 km: 10-16 dB; daugiau nei 900 km išlieka 16 dB.

7 dB, tai yra pagal skalės pradžioje, yra minimalus, kad: koridorius yra sudarytas su savo etikete, matyti, kad juosta veikia, bet žemėlapio spalvos nesikeičia (su lange taip pat rodo 3-7 dB pilka punktyrinė). Viršijus 7 dB, vertė yra pasverta pusantro, kai nepriklausomų maršrutų skaičius (3 smūgiai → 0.25; 6 arba daugiau → 1) ir santykių S / N mediana (− 20 dB → 0.5; 0 dB arba daugiau → 1; nėra žinomas, kaip klasterinėse vietose → 0,75), pradedant nuo 7 dB. 3 maršrutai be S / N - 500 km → 5,3 dB; 5 maršrutai su 615 km → 7,7 dB; 3 maršrutai be S / N - 900 km → 11,5 dB; 3 maršrutai - 20 dB - 900 km → 10,4 dB; 6 maršrutai su 0 dB - 900 km → 16 dB.

Mažiausias atstumas išlieka 300 km: pakėlus jį iki 500 km, taip pat būtų pašalintos realios trumpojo nuotolio angos (pavyzdžiui, 400 km tiltelio tiltas, pripažintas oro prognoze ir labai stipriais signalais, ne vien tik nuo atstumo). I5MZY archyve, 22 / 11 / 2026 10: 53 UTC, su šia taisykle ir spindulių filtras ilgiausi koridoriai buvo 615 km (7.7 dB) ir 540 km (6,3 dB); su 1.3.3 taisyklės pasiekė 2175 km ir 16 dB.

Vos 1.3.3 minimumas galiojo visame koridoriaus stačiakampį ir priklausė tik nuo atstumo (nuo 10 dB iki 300 dB iki 16 dB iki 900 km): su daugeliu ilgų koridorių (pavyzdžiui, 22 / 11 / 2026, 12 koridorių iki 2620 km), pakėlė beveik visą Europą iki 16 dB.

Pelno kreivės nebetraukia "gocce": mažesnės salos 220 km abiem kryptimis lieka spalvos, bet be uždaros kreivės, nebent koridorius jas patvirtina. Ugnis išsijungia su "Vaizdu, kurį lemia dėmės".

## 4 ter. Gyvi jutikliai: FM-DX web serveris ir AIS imtuvas (nuo 1.4)

Dar du realių stebėjimų šaltiniai, įeinantys į 2 m juostos koridorius, ir patikimumo vertinimas (§ 10). NAME OF TRANSLATORS

Webserver FM-DX. daugelio FM (88-108 MHz) nuotolinio priėmimo entuziastų palaikyti per tinklą prijungtą imtuvą FM- DX Webserver free programme (GPL- 3.0); Viešas sąrašas yra servers.fmdx.org [28]. Kiekvienas serveris sako, puslapyje / apI, derintas dažnis, signalas dBf ir, jei jis dekodavo RDS, siųstuvas pripažintas savo duomenų bazę su atstumu ir azimutas. Programa:

- atsisiųskite sąrašą kas 30 minučių ir 60 artimiausių interneto serverių palaikykite ne toliau kaip 1500 km QTH;
- užklausA juos vienu metu, pasklidus per 3 minutes, ne ilgiau kaip 8 sekundes (nė vienas serveris nesulaukia daugiau kaip vieno prašymo kas 3 minutes);
- palaiko priėmimą, kai siųstuvas yra 150 km ar daugiau, ir signalas yra ne mažiau kaip 10 dBf; siųstuvo padėtis yra kilęs iš serverio, azimutas ir atstumas.

FM juosta yra žemiau nei 2 m, o ją nešantys ortakiai ne visada atneša 144 MHz; bet FM priėmimas 300-600 km atstumu už horizonto beveik visada yra tropas (arba, vasarą, E sporadic, kad 100 MHz pasiekia daugiau nei 900 km: už tai daugiau nei 1500 km koridorių taisyklė reikalauja dviejų skirtingų šaltinių, § 4 bis). Žemėlapį priėmimai yra violetinės linijos "FM n km"; valstybės bare skaičius, su komiksų knygoje sąrašą.

AIS iš jūsų imtuvo. Laivai perduoda savo poziciją 161,975 ir 162,025 MHz dažnių juostoje (ITU- R M.1371 [31]), beveik tokį patį dažnį, kaip 2 m. AIS imtuvas (AIS- catcher, Rtl- ais, skirtas imtuvas), kuris persiunčia frazes NMEA! AIVDM UDP dėl 10110 uosto transformuoja kiekvieną laivą į tinklo jutiklį: programa iššifruoja buvimo vietos pranešimus (1, 2, 3, 4, 18, 19 ir 21 tipai, net dėl kelių fragmentų) ir lygina atstumą su įprastu radijo horizontu

```math
d_h = 4{,}12\,(\sqrt{h_{nave}} + \sqrt{h_{rx}})\ \text{km}
```

su laivo antenos aukštis nuo tipo (20 m krovininiams laivams, 30 m keleiviams, 6 m žvejybai ir malonumui, 10 m kitiems) ir imtuvo antenA (pagrindinis 10 m). Laivas, daugiau nei 1,5 karto didesnis už horizontą ir daugiau nei 50 km, yra priėmimas per vamzdyną: žemėlapyje jis rodomas kaip mėlyna linija nuo QTH su riaumojimo "AIS n km". Toji pati idėja, kaip CSIRO [10] tyrimas, kuris naudojo 162 MHz AIS imtuvus GFS vamzdynų numatymui patikrinti. Brėžinys išjungtas: jis tinkamas imtuvui.

## 5. Nekomplektinės linijos ir palyginimas su slėgiu ir inversija

Gėlinimo kreivės: linijos 7, 10, 13, 16, 18,5 ir 21 dB, skalės kraštai, nubrėžtos ant lauko išlyginti žydėjimo kvadratų metodu, su etikete dB.

Izobara: slėgis jūros lygyje kas 4 nuo tos pačios GFS, iki prognozės valandos, su centrais H ir L.

Šiluminė apversija: kreivės padengtos kiek oro esant 925 arba 850 hPa yra šiltesnis nei esant 2 m, + 1, + 3, + 5 ir + 8 ° C.

Vadovaujant linija suteikia gauti susitarimą su spaudimu ir su atvirkštine matomos zonos: Pearson koreliacijos koeficientas, -1 + 1. Vožtuvai dažnai gimsta anticiklonuose (ore, kuris nukrenta ir formuoja atvirkščiai), todėl teigiamas susitarimas su spaudimu yra normalus, bet tame pačiame anticiklone pelnas labai keičiasi tarp jūros, pakrantės ir kalnų. Inversija vien nepakanka: lenkimo bangos tik jei virš oro yra net džiovintuvas [5]. Linija tarnauja pamatyti, dabar, kuris iš dviejų geriausių paaiškina spalvos srityse. Tuos pačius rezultatus galima palyginti ir su registru.

## 6. Galimi vamzdynai: dvigubas skaičiavimas

GFS lygis svyruoja nuo 200 iki 1000 m: plonas inversija plinta ir modelis nemato vamzdyno. NNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN3 NNNNNNNNNNNNNNNNNNNNN3 NNNNNNNNNNNNNNN3 NNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN3 NNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN@@

- skaičiavimas A: visas koncentruotas perteklius per 50 m.
- skaičiavimas B: tas pats sumažėjimas, pasklidęs ant tipinio tos zonos vamzdynų storio, paimto iš ITU- R P.453 [4] klimato žemėlapių (ant žemės arba virš jos).

Trūkumui ir didžiausiam užfiksuotam bangų ilgiui nustatoma žemiausia vertė: tai apdairus įvertis.

ITU- R P.453 duomenys yra © ITU ir gali būti naudojami asmeniniais ir nekomerciniais tikslais, cituojant šaltinį, bet negali būti perskirstyti: už tai aš nesu programos pakete. Prijungti B skaičiavimą, tiesiog atsisiųsti P.453 žemėlapius iš ITU skirtuką: programa, po to, kai rodo naudojimo sąlygas, atsisiųsti vamzdynų archyvą iš ITU svetainėje (iškeltas _ ir _ surface _ duct.zip) vartotojo asmeniniam naudojimui, išgauti failus duomenų / P453 ir įjungti skaičiavimo be perkodavimo. Arba, atsisiųsti iš ITU- R Study Group 3 skaitmeninio produkto puslapį (ITU- R Study Group 3, skaitmeninių produktų, P.453, paviršiaus duomenų ir padidėjęs ortakio duomenis) ir įdėti septynis S _ OCCUR. TXT, S _ TTICK.TXT, S _ STREN. TXT, E _ OCCUR.TXT, E _ base.T Befailų programa naudoja tik A skaičiavimą ir rašo jį skirtuke ir registre. Trūkumo A ir tipinio intensyvumo P.453 santykis yra nukrypimas: Kiek kartų šiandien vamzdynas yra stipresnis nei įprasta. P453

## 7. Stotys ir vietos

Savininkas gali turėti 4, 6, 8 arba 10 simbolių. 8 arba 10 simbolių pozicija tiksli, o dirvožemio dalis gaunama iš programos "Copernicus GLO- 30": antenos aukštis nuo žemės yra pakankamas. 4 arba 6 simbolių aikštė yra per didelis: tai geriau rašyti antenA kvotą ant jūros; jei 0 programa lieka su SRCM dirvožemio ties aikštės centre plius aukštis nuo žemės ir signalizuoja, kad rezultatai. Tiems, kurių numatytasis aukštis yra 3 m nuo žemės, naudojamas SRCM dirvožemio ir aukščio santykis 0.

## 8. Žemė, difrakcijos ir troposekteris

Maršruto duomenys imami pagal programą Copernicus GLO- 30 kas 30 m (daugiau nei 120 km). Žemės spindulio ekvivalentas yra k = 157 / (157 + ΔN), o ΔN - N sumažėjimas per pirmuosius kilometrus nuo VFS profilių [6, 7] maršrutu.

Laidai netoli stočių (nuo 1.3.2, autoriaus kriterijus). P.452 moka horizonto kampą tiesiškai, 0,573 dB (mrad): kelių dešimčių metrų atstumas nuo antenos kainuoja dešimtis dB (dB), daug daugiau, nei buvo pastebėta. Vos 20 km atstumas nuo stoties gali pasiekti 6 dB: horizonto kampas dėl jų yra apribotas iki tolimiausių atšakų plius 10,5 mrad. Jūsų stoties ir korespondento atžvilgiu jis galioja. Linija "Šalia reljefai" sako, kiek dBs pakilo.

Žemės šaltinis (nuo 1.3.2). Kvotos yra iš programos "Copernicus" GLO- 30 [3]: 1 sekundė, jūra - 0, pakrantė švari. Ligi 1.3.1, programa naudojo "Terrain Tiles" "skadI" AWS. Šalia kranto tie kvadratai maišyti SSRM su mažos skiriamosios gebos jūros dugno duomenis ir skleisti salos dalį jūroje: aplink Capri sukurti vaiduoklių žiedas 80-100 m nuo 0,5-1 km nuo kranto. 2026 m. lapkričio 21 dieną IC8TEM pranešė apie neegzistuojantį horizontą šiaurės vakarų kryptimi; palyginimas taškais su Copernicus jį patvirtino (94 m jūroje į šiaurės vakarus Marina Grande vs 0). Tiems klaidingiems horizontams (+ 2,6 °) pakrančių stotyse teko apie 26 dB troposcatter. Kadi plokštės lieka rezervu, kai programa "Copernicus" nereaguoja. Copernicus plokštės sveria 15-30 MB ir vieną kartą parsisiunčia; tik jūros plokštės neegzistuoja ir yra verti 0.

- Laisva erdvė: Lfs = 92,44 + 20 log f (GHz) + 20 log d (km) [8].
- Differencija: Bullington metodas realiame profilyje plius ITU- R P.452-16 § 4.2 [6] delta- Bullington sferinis sausumos korekcija, ITU- R P.526 [9] rutulinis praradimas.
- pirmoji Fresnel zona: radius ^ (λ · d1 · d2 / d); laisva, jei žemė palieka bent 0,6 spindulio. Rodomas kiekvienos kliūties [9] peilio stalo V ir J (VV) parametrai..
- Profilio projekcijoje tiesioginis spindulys yra žalios spalvos optiniu ir raudonu vaizdu, jei jis eina po žeme arba po žemės kreive; Raudonieji taškai yra kliūtys, kad įsiveržti į pirmąjį plotą Fresnel (violetinė punktyrinė linija). Ant jūros "raudonas taškas" aukštyje 0 m nėra kalnas: tai jūra kyla kreivumo. Pereijimas su pele ant spindulio arba taške, atrodo komiksas su paaiškinimą ir vertes.
- Šalia antenos užvedus kryptį (pavyzdžiui, dešimčių laipsnių horizontą), difrakcijos ir troposcatter nuostoliai sudaro šimtus dB: virš 300 dB programa rašo "> 300 dB" ir "kryptis uždaryta gretimos kliūties", signalas "< -230 dB" ir S / N santykis "-60 dB". Čia patartina pažvelgti "Rekomenduojamas augalas" ir "Via monte".
- Troposcatter: ITU- R P.452-16 eq. 45-50% [6] laiko, su kampiniu atstumu nuo horizontų ir N0 nuo profilių.
- Betropo: difrakcijos ir troposcatter galių suma, niekada po laisva erdve.

## 8 bis. Troposcatter: keturi modeliai ir neapibrėžtumas

Šalia P.452 programa skaičiuoja su trijų kitų modelių troposcatter, kaip ir TropoLink [16] programa, ir rodo savo skirtumą, kaip neapibrėžtumą:

- NBS TN101 § 9 [25] (nuo 1.3.5), mediana: L = 30 log f − 20 log d + F (θd) − F0 + H0 + Lgp. F (θd) yra III priedo silpninimo funkcija jo analitinėse formose (NS 301) su pataisa NS, H0 dažnio stiprinimo funkcija § 9.2: 144 MHz, su žemomis antenomis bangų ilgiuose, atspindys nuo žemės iš dalies ištrina sritį apatinėje bendro tūrio dalyje ir 3 -10 dB nuostolių druska. NS yra valandos GFS profiliai, efektyvūs horizontai ir aukščiai ateina iš Copernicus profilis, Lgp = 0.07 · E ^ (0,055 (Gt + GI)) yra atidarymo sukabinimo praradimas. Toji pati forma, kad ITM naudoja savo interjero, bet su šiandien, o ne klimato reaktyvumas. It tests on flat sections within 144 MHz with antenos 20 m: within 2-4 dB from ITM 's nuo 150 iki 900 km ir apie 2 dB in the simplified form 9.2b of TN101, kuri neturi H0.

- ITU- R P.617-5 [17], mediana: L = F + 22 log f + 35 log θ + 17 log d + Lc, su f MHz, θ Mrad, d, km, F = 0,18 · N0 · E ^ (-hs / 7,35) − 0,23 · ΔN ir Lc = 0,07 · E ^ (0,055 (Gt + GR)). N0 ir ΔN paprastai yra paimtos iš ITU klimato žemėlapiuose; čia mes naudojame kiekvieną valandą profiliai GFS ( N0 · E ^ (-hs / 7,35) yra reaktyvumas į žemę, ΔN sumažėjimas per pirmąjį km), taip pat troposcatter seka laiko.
- RSGB VHF / UHF Handbook Formulation, DL2FZN [18]: L = Lfs + 24,44 + 4,283 ln f + 0,06656 d. Šlaitas su atstumu yra beveik lygus PROSPER (0,067 dB / km) nuolydžiui.

bendras tūris (nuo 1.3.1): tai taškas, kuriame dviejų stočių spindulys yra kertamas, kiekvienas pasviręs į horizonto kampą, lygiaverčio spindulio ae = k · 6371 km žemėje. nuo TX spindulys pakyla, kai z = hTX + x · θt + x2 / (2 · ae); X = (hRX − hTX + d · θr + d2 / (2 · ae) / θ, su θ = d / ae + θt + θr. Fiksuotų horizontų ir antenų su ta pačia kvota bazė yra verta d2 / (8 · ae): apie 1,3-300 km, 3,7-500 km, 9,4-800 km. Kuo didesnis tūris, tuo daugiau oro yra sausas ir surūgęs, ir tuo mažiau jis plinta: dėl šios priežasties daugiau nei 800-1000 km troposcatter baigiasi. Tuose rezultatuose yra pagrindo dalis ir vieta, reaktyvumas, gradientas ir GFS drėgmė tame aukštyje, ir lygiavertis atstumas Bray (QST, November 1961): kiekvienas papildomas arba mažesnis horizontas yra verta apie 111 km. Nuoroda: IU3QEZ pristatymas "VHF horizonte" (GEKO žurnalas n. 69).

Troposcatter linija, kiti modeliai rodo du nuostolius ir netikrumą, kaip pusę skirtumo tarp optimistinio modelio ir pesimisto. Jie yra tik palyginimas: be tropo nuostoliai lieka, kad P.452.

## 8 p. popietinė troposcatter per lengvosios (nuo 1.3.4)

IK3XTV, iš Thiene ( JN55, su Alpės 10 km), dirba vasaros popietėmis pietų Vokietijoje 400- 500 km, be matymo, su stipriais signalais, bet su greitai ir giliai eganescencija; autorius priskyrė juos "konvekcinių ląstelių sulygiuotas kaip lęšių traukinys". Pastaba yra patvirtinta; į programą priimtas paaiškinimas yra kitas, fiziškai tvirtesnis.

popietę saulė sukelia orą virš reljefų, o ribinis sluoksnis pakyla iki 2-3 km. Viršuje drėgnas oras atitinka sausą orą: ten lūžio rodiklio turbulencija (dydis, kuris reguliuoja troposcatter) yra maksimalus. Vos pasiekus bendrą maršruto, kuris 400-500 km yra apie 2-3 km, tūrį, troposcatter yra sustiprintas. Čia galima paaiškinti popietę, aukštą slėgį (tylus cilindrinis, stiprus šildymas) ir greitą eganescenciją, būdingą difuzijai, bet ne ortakiams. Konvekcinės ląstelės neveikia kaip lęšiai: gamina turbulenciją, ir tai yra nauja redakcija sluoksnio viršuje turbulencija, kuri plinta signalą.

Skaitymo prognozių pasekmės:

- žemėje pirmoji popietė yra blogiausias metas žemiems ortakiams ir sluoksniams, bet ne troposkatams už kalnų: abu dalykai turi būti skaityti atskirai,
- programa jau turi du jai reikalingus duomenis: modelio nustatyto ribinio sluoksnio aukštį ir bendro maršruto tūrio (bendro tūrio) dalį rezultatuose: kai jie yra arti, popietinio kalno gydymas gali būti geresnis, nei skaičiavimai normalioje aplinkoje;
- nuo 1.3.4 punkto apskaičiavimo atsižvelgiama: jei bendras tūris yra žemėje, maršrute yra bent 800 m nukrypimų, saulė yra didesnė nei 15 °, ribinis sluoksnis yra didesnis nei 800 m, o sluoksnio performavimas (pavyzdinis dirvožemio greitis + ribinis sluoksnio aukštis) nuo bendro tūrio pagrindo yra ne toliau kaip 500 m, troposcatter įgauna 3 dB; nuo 500 iki 1000 m tiesinis pelnas sumažėja iki nulio. Linija "Troposcatter popietę". Sniego išilgai maršruto (ne mažiau kaip 10 cm nuo žemės pagal modelį, sniego _ gylio kintamasis Open-Meteo arba SNOD GFS iš NOMADS) armatūra netaikoma: sniegas dirvožemis lieka šaltas, o performuotas sluoksnis neauga. 3 dB vertė yra laikina, ji turi būti sukalibruota realiomis sąsajomis; ji išsijungia esant oro sąlygoms.

Šaltiniai: F. Egano IK3XTV (Radio Rivista e www.qsl.net / ik3xtv, Propagazione sulle 2 metrI, Celle conventtive, Troposcatter over the Alps), sources § 13.

## 9. Kelio pradžios sluoksnis

Čia yra signalo skaičiavimo širdis. Maršrute GFS profiliai imami kas 25 km. Kiekvienam slėgio sluoksniui ilgiausia atkarpa, kurioje tas pats sluoksnis yra gretimuose taškuose; skaičiuojama kaip ortakio storio sluoksniai, ne mažesni kaip 40 m [10].

- tęstinumas: nuo km iki kurio km, ir maršruto dalis;
- viršus, pagrindas ir pokrypis (m / km);
- dirvožemio sluoksnio viduje: km, kai kalnai kyla virš pagrindo;
- Padengta žemė: jūroje konduit yra beveik visada, nusprendžia sausumos ruožas [10]. Miškininkystės ministro reglamentas P12 / 2014

Tropo padidėjimas yra panaudotos frakcijos sluoksnio padidėjimas. Molekulinė masė (Molekulinė masė)

## 9 bis. Oro pataisos: stabilumas, nusėdimas, streikas

nuo 1.3.0 profilis taip pat suteikia tris meteorolog( regave "stabilumas (Misme)"), Metero linija palei rezultatų maršrutą ir, su Segni Meteo langelį, žemėlapyje.

- Misme stabilumas [5, 6 skyrius]. Misme prijungia lauką už horizonto prie sudėtinio parametro: M = A · (ge -40 + b · ΔW), su A = 0,5 dB / N / km, lygiavertis gradientas ge ir šiluminis stabilumas ΔW, 1 km oro kėlimo darbas. Programa suskaičiuoja ΔW = g · Ω (θ -θ0) / θ0 dz tarp dirvožemio ir 1 km virš, su θ potencinė temperatūra; standartinė atmosferA yra verta apie 56 J / kg. Punktas "J / kg" yra 0,07 dB (J / kg) už standarto ribų, nuo -4 iki + 6 dB, ir skaičiuojamas tik tuo atveju, kai juostelė nefiksuojama (gradientas jau nurodytas Bean ir Dutton indekse). Kalibravimas yra laikinas: jis turi būti patikrintas naudojant santykį (14 punktas).
- Subsidence ir upill air [19]. Molerio ir Holdeno maršrutuose 125-305 km atstumu į jūrą 1365 MHz ir 9380 MHz aukštyje buvo nustatytas didžiausias maždaug 2,5 dB signalas, kai oras sklinda, o tuo atveju apie 20 dB sustiprinimas 3 valandoms paviešinimo pradžioje, po kurio, kai oras sukasi, sumažėjo 22 dB. BFS vertikalusis greitis yra 850 ir 700 hPa. Brėžinys (P) 2. [5]
- [20] Scirocco. Karštas ir sausas oras iš Sacharos virš aušintuvas jūrų oras suteikia labai stiprus inversija iš Rytų Viduržemio jūros į Gibraltarą. S ženklas rodo, kur 850 hPa vėjo ateina iš 100-200 ° su ne mažiau kaip 7 m / s ir temperatūra yra bent 16 ° C, Viduržemio jūros. Čia tik nuoroda: pelnas nulemia profilio sluoksnius.

Trūkstant tinklo, kuris spėja juostą, bendras prieaugis neviršija 16 dB, geltono galo: pataisymai padaro stipresnius sluoksnius akivaizdesnius, bet ne pro juos ortakiais (raudonos ir violetinės lieka prie kanalų).

- Trukmė (nuo 1.3.1). (VHF / UHF DX Book; Biancomano ir Leyshon 1998, taisyklė 12 valandų; ataskaitos 29 / 12 / 2021-2 / 1 / 2022 auga nuo pirmos iki ketvirtos dienos). kiekvienai ląstelei programa skaičiuoja ankstesnio 24 valandų skaičiaus prieaugį, kuris yra ne mažesnis kaip 13 dB, ir proporcingai prideda 2 dB, tik tuo atveju, kai pelnas jau yra ne mažesnis kaip 10 dB.

NAME OF TRANSLATORS

| Pasirašymas | Spalva | Ką tai reiškia? | Poveikis tropams |
|---|---|---|---|
|  ↓  | Žalieji | subsidence: oro lašai (anticiklon-), įkaista kompresijai ir sausai, stiprina atvirkštinę pusę ir palaiko ją tvirtai | palankus, + 1,5 dB |
|  ↑  | raudonas | įpūtimo oras (priekinis, žemo slėgio, konvekcija): maišomi žemi sluoksniai ir pertraukos inversijos ir ortakiai | nepalankios, - 2 dB |
|  S  | oranžinis | Sirupas: karštas ir sausas oras iš Sacharos virš šviežio jūros oro | palankus raktas, be korekcijos: nuspręsti sluoksnius |

Plotas, pilnas žalios ↓ pagal anticiklone yra klasikinis ženklas sunku atidarymo; Raudona ↑ rodyklės išilgai linijos pasakyti sluoksniai laužo, nors ląstelių spalva yra vis dar geras.

Orų pataisos langelis išjungia šiuos pataisymus, kad matytųsi 1.2 modelis.

Lokomotyvų ir keleivinių riedmenų TSS Tuos pačius duomenis galima palyginti su pagrindiniu ribiniu dydžiu: žemame aukštyje esanti jūra, kuri tomis dienomis atnešė ženklus tarp Kampanijos ir Ligūrijos, GFS lygyje nėra matoma.

## 9 ter. Naujienos 1.3.2: du apskaičiavimai, lubos, aukštas slėgis, jūra, stotys, keteros

apdairus ir agresyvus vertinimas. Nuolydis iš gradiento padidėja nuo 0 dB iki normos ribos iki 10 dB iki viršrefrakcijos slenksčio. 1000-850 sluoksnyje apdairi riba yra Bean ir Dutton (100 N / km); Agresyvi riba - tai klasikinė didžiausios refrakcijos riba 79 N / km (k maždaug 2; P.453 naudoja -100 N / km per pirmuosius 100 m), kuri su k50 = 157 / (157 − ΔN) yra K maždaug 2 (k = 2,5 atitinka apie 94 N / km). Point-Taškas skaičiavimas rodo du nuostoliai flanked; su tinklo, kad spėja gauja sutampa. Žemėlapį naudoti atsargiai.

Keliai ir ortakiai ieškomi tik už 2500 metrų nuo žemės: kuo aukščiau mėgėjų radijo antenos jų neišnaudoja. 1500 m vertės patikimumo patvirtinimas prarado atskyrimo tašką ir 4% didesnių nei 16 dB jungčių, nes žiemą nuošalumo reversai dažnai būna 1500-2500 m.

Pratęstas aukštas slėgis. + 1 dB, kai bent 7 iš 9 aplinkinių ląstelių slėgis jūroje yra 1020 hPa arba didesnis, ir vėjas yra silpnas žemuose sluoksniuose (10 m nuo 5 m / s, 850 hPa nuo 8 m / s), išskyrus didėjančius judesius.

Jūros svoris. Point-to-taškas skaičiavimo sluoksnių pelnas dauginamas iš 0.8 + 0.4 · μ1, kur μ1 yra ITU- R P.452 (eq. 51) dėl ilgesnio tęstinio sausumos ruožas: 1 atviroje jūroje, mažas su ilgais ruožais sausumos.

Stotys ir vykdomas. Vožtuvo, kuris fiksuoja dažnių juostą, dalis tarp kiekvienos antenos ir jos viršaus gretimos dalies pradžioje ir gale yra lyginama; pelnas dauginamas iš 0, 5 + 0, 5 · ^ (E ^ (-ΔhTX / 300) · E ^ (-ΔhRX / 300)), o Δh atstumas metrais nuo ortakio. Konduit beveik visas maršrutas yra § 10 sujungimas.

Sluoksnis ir keteros. GFS ribinio sluoksnio viršus (didžiausio ribinio _ layer _ height modelio dirvožemio greitis) yra didesnis už maršruto taškus (ne daugiau kaip vietos aukštis didesnis nei 300 m, jei didžiausias aukštis didesnis nei 500 m), sluoksnis nesulaužomas per kalnus: + 1,5 dB už uždengtų keterų dalį. IT9GSF stebėjimas nuo Sicilijos į Vokietiją su palankiomis Tirėnų.

Šalia jūros. NOMADS šaltiniu programa taip pat parsisiunčia paviršiaus temperatūrą (ant jūros SST), temperatūrą ir drėgmę 80 m ir sluoksnis 30-0 hPa virš žemės. Ant jūros taškų (modelio greitis mažesnis nei 5 m) šie lygiai pridedami prie profilio, pirmojo slėgio lygio, kad būtų galima matyti 100- 200 m skersmens ortakį, kurio slėgio lygiai neišsprendžia. Žemėje nėra naudojami: nakties ir dienos sluoksniai dar negalioja. Open- Mete- suteikia tik paviršiaus temperatūrą.

Garavimo vamzdynas. nuo SST, nuo temperatūros ir drėgmės 2 m yra apskaičiuotas δ aukštis garinimo konduIto su neutraliu logaritminis profilis (kaip Paulus ir Jeske modelio, be stabilumo korekcijos): oras į vandens plaukus yra prisotintas SST (98% druskos), todėl

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

nuo 0 iki 40 m. 144 MHz spąstai užtektų apie 180 m (Kerr), tada 144 MHz garavimo ortakis beveik nesustiprintų; iš 432 MHz į viršų, su žemomis antenomis jūroje (11 bis dalis). @ info: tooltip

Dienos laikas (apsilankymas). Programa apskaičiuoja saulės aukštis kiekviename taške. Kasdien iš dienos žemėje, saulei esant virš 15 °, ribiniam sluoksniui virš 800 m arba vėjui esant virš 5 m / s, įspėja, kad dirvožemio kanalai yra performuoti; naktį, saulei esant iki -3 °, vėjui esant žemiau -3 m / s, ribiniam sluoksniui iki 300 m, įspėja, kad su ramiu dangumi, kurį gali sukelti švitinimas, kuris nukrenta po saulėtekio. Ne jūroje. Nes šiuo metu jie pastebimi, ne skaičiai: gruodžio patvirtinimas neturi pakankamai vasaros dienų juos kalibruoti. Čia du dalykai, kuriuos autorius laiko svarbiais, nuo kasdienio stebėjimo.

Normalus profilis CRPL (alternatyva). Vietoj 40 N / km nustatytas normalus sumažėjimas yra eksponentinė atskaitinė atmosferA CRPL: N (h) = N · E ^ (− c · h), su − ΔN pirmame km = 7,32 · E ^ (0,005577 · Ns) (Bean ir Thayer 1959). Šlapiu oru (NS) normalus yra stabilesnis ir tarnauja stipresnis gradientas duoti pelną; aukštyje yra saldesnis. Pasirodo, blogai, kol patvirtinimo sako, kad tai padeda.

## 9 quater. kontrastas - jūra (nuo 1.3.3)

Radijo bangos nesijaučia jūros dugne: juntate oro ir vandens paviršiaus kontrastą. gylis svarbus, nes jis nusprendžia, kaip greitai jūros temperatūra atitinka oro temperatūrą.

Adrijos jūra yra žema. Vidutinis gylis yra apie 250 m, bet šiaurinis baseinas retai viršija 100 m, ir Triesto įlankA išlieka žemiau 25 m; tik į pietus nusileidžia daugiau nei 1200 m. Vandens sluoksnis taip plonas per kelias dienas, tyrėnų per kelias savaites, šildo ir aušina. Egzistuoja trys tipinės situacijos:

1. Pavasario ir vasaros pradžioje, palanki. Jūra vis dar šalta, net Alpių ir Popo upių gėlame vandenyje, iš lygumos arba Balkanų atkeliauja karštas ir sausas oras. Karštas oras virš šalto vandens - tai jūros atoslūgis (nuotykių vamzdynas): tai klasikinė angų tarp Italijos, Slovėnijos ir Kroatijos būklė.
2. Ruduo ir žiema su Bora, nepalanki. Šaltas oras virš šiltesnės jūros: žemasis sluoksnis tampa nestabilus, maišomas ir nutrūksta ortakiai.
3. Lškart po boros. Vien dėl to, kad jūra yra žema, boras per kelias dienas ją šiek tiek išlygina. Jums įėjus į karšto oro anticikloną, oro ir jūros kontrastas yra stiprus, o vamzdynas - stipresnis nei įprastai.

Adrijos jūra taip pat yra uždaras kanalas tarp Apeninų ir Dinari, mažiau nei 200 km pločio: jūros sluoksnis lieka izoliuotas, ir maršrutai palei ašį (Puglia- Venezija- Triestas, apie 700 km) lieka visi ant jūros, geriausia būklė ilgą ortakio.

Ką programa apskaičiuoja. Ant jūros taškų, su oro pataisas ir langelį "Kontrasto aria- mare":

| Savybė | Korekcija |
| --- | --- |
| galima temperatūra 925 hPa (arba 950) virš 4 K SST | Tu... |
| ne mažesnės kaip 7 K SST, | + 2 dB |
| su oro drėgme 925 hPa (daugiau nei 80%) | • 0,5 dB už šias priemokas |
| atvėsintos jūros 1,5 ° C arba daugiau per 72 valandas prieš, su palankiu kontrastu | Tu... |
| šiltesnė oro jūra 2 m esant 2 ° C (maišymas) | - 1, 5 dB |
| 4 ° C šiltesnė jūra | − 3 dB |

Suminė pataisa išlieka nuo -3 iki + 3 dB, ir, nesant kontūro, pelnas lieka po geltonu stogu. Ribos yra laikinos: standartinėje atmosferoje galima temperatūra 925 hPa viršija jūros temperatūrą apie 2-3 K, tada 4 K jau rodo atvirkštinę.

SST atmintis. Modeliai išlaikyti jūros temperatūrą beveik pastovus prognozavimo metu. Nesenas sumažėjimas yra gautas iš trijų dienų prieš (Open- Meteo, tik paviršiaus temperatūra, tik jūros ląstelių) analizės: ne daugiau 72 valandos prieš atėmus dabartinę vertę. @ info: whatsthis

Ribos. Modelių žemiau kranto ir viršutinėje Adrijos jūros yra šiurkštus SST; šaltas rašiklis po, pavyzdžiui, yra blogai išspręsta. Batiometrija neįeina į apskaičiavimą: šaltiniai visada suteikia SST, kuris jau apibendrina jūros dugno poveikį. Oro ir jūros kontrastas iš dalies sutampa Misme stabilumo pabaigoje (§ 9 bis); už tai premijos yra mažos. Ribos bus tapačios reklamos koridoriams ir QSO per Adrijos jūrą.

## 10. Iškrautas per visą maršrutą

Lankstus sluoksnis fiksuoja juostą ne mažiau kaip 80% panaudotos frakcijos, bet stotys nematomos, taip pat apskaičiuojamas nutrūkimas vamzdyne: laisvoji erdvė, antenų sujungimas (3 dB, jei jos yra vamzdyne, daugiau, jei jos yra aukščiau arba žemiau), liekamasis atėjimas γD = 5 · 10 -5 · ae · f ^ (1 / 3) dB / mrad ITU- R P.452 eq. 49 [6]. Numanomas nuostolis, jei jis yra mažesnis už nuostolius be tropos, tampa tikėtinu nuostoliu.

Juosta lieka kondute tik tuo atveju, jei jos bangos ilgis neviršija λmax = 2 ^ 2 · (2 / 3) · ^ (ΔM · 10 − 6) · d (Kerr, eq. 17 [11]). Tiems patiems sluoksniams galima gaudyti 23 cm ir 70 cm, bet ne 2 m. VHF / UHF DX Knyga kalba "sferored" kraštai ir "reform" ortakiai. nuo 1.3.1 programa naudoja minkštą kondutą: jei λmax yra tarp λmax ir 3 · λmax, pelnas palaipsniui pereina nuo 15 dB (λ = 3 · λmax) iki viso konduto padidėjimo (λ = λmax), kai s = (λmax / λ − 1 / 3) / (2 / 3). tokiu atveju ortakio spalvos yra leidžiamos. Maršruto žurnalas ir Meteo linija nurodo taškus su minkštu ortakiu.

## 10 bis. Via Monte

Kuomet korespondentas yra uždarytas už horizonto, programa siekia geriausia kryptimi nustatyti antenas: apšviestas nuolydis plinta signalą kaip radaro taikinys. Žiemą jis veikia kas penktą (išskyrus ± 15 ° aplink korespondentą), ir kiekviena kryptimi įjungia apšviestą horizonto tašką (virš 100 m, 1-60 km). Trumpas įvertis (laisva erdvė, bitų konversija ir 10 dB kiekvienam horizonto lygiui nuo difuzoriaus iki korespondento) pasirenka 4 perspektyviausius, ne mažiau kaip 15 ° vienas nuo kito; kiekvienam:

- 1, stotis → reljefas, laisvoje erdvėje;
- traktuoja 2, reljefo → korespondentas, apskaičiuojamas taip, tarsi atleidimo perduodama nuo 10 m virš viršaus, su tuo pačiu prognozavimo (be parabolinės lygties);
- konversija su bistatine radaro lygtimi, kai Δ = σ0 · A, σ0 = -15 dB (kalnas VHF / UHF), ir A plotas, apšviestas R1 spinduliu (ne daugiau kaip 20 km2):

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

"Rekomenduojama taškas" linija nurodo geriausio maršruto azimuto, jei tai kainuoja mažiau nei tiesioginis. 144 MHz, kai greitis 20 km ir 15 ° gylis, kalnų kelias kainuoja 45 dB daugiau, nei vien 2 maršrutas: tai nutinka tik tada, kai tiesiogiai uždaroma kaimyninių kliūčių. Brėžinys yra bendrasis (± 10 dB), ypač σ0.


Nemažai kalnų (1.3.2 variantas). Dėkle "Via monte senza perdite fino al monte", kritimas tarp stoties ir difrakcijos taško laikomas nuliu: tai lyg stoties nuvedimas į viršų (antenA 10 m virš taško, ta pati galia, antenA ir kabelis). Horizonto turas tada pasirenka tašką su labiausiai laisvo horizonto link korespondento (pariteto, aukščiausios) ir rezultatas rodo maršrutą nuo taško iki korespondento, su signalu ir S / N, šalia viso difuzijos. Situacija optimistiška: sako, jei kryptis atsidaro, kai kalnas pakelia signalą, arba kur eiti nešiojamas. Kiekvienas taškas yra pateiktas su ten- simbolių.

## 10 ter. Įėjimas į tolimą jungtinę

Lenktynės prasideda toli, už stoties horizonto, signalas turi atvykti. Programa duoda signalą į atspirties tašką, viduryje tinklo, difrakcijos: palei Copernicus profilis GLO- 30 ieško krašto su didžiausiu Fresnel parametru (ITU- R P.526), su lygiavertės žemės patinimas,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

daugiau silpninimo dėl viršaus kreivio (suapvalinta kliūtis, P.526- 16 § 4.2), kai R spindulys nuo Copernicus GLO- 30 dirvožemio iki ± 1 km nuo krašto,

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

(dėl m · n > 4 yra rekomendacijos 34b forma). Barsis (NBS, 1963), matuojamas trimis ilgais maršrutais per kalną (Kolorado, Aliaska, Europa) vidutiniai nuostoliai 10-20 dB didesnis nei idealus kraštas Fresne- Kirchhoff: apvalinimo paaiškina daug (Barsis ir Kirby, 1961: Pikes Lead su maždaug 11,5 km spinduliu). Rezultatą galima palyginti su difrakcijos ant lygaus sferinio dirvožemio; tai blogiausia. Tada signalas sklinda vamzdyne: laisvoji erdvė visame maršrute, liekamasis silpnėjimas (γd) (P.452) grandinės dalyje ir korespondento sujungimas (3 dB viduje, 1 dB kas 10 m išorėje). Neigiamas poveikis (poveikis (difrakcijos būdu)) Autoriaus prielaida dėl Italijos geografijos: su šalia esanti kliūtis, gerokai žemiau linijos, v yra neigiamas, ir įėjimas kainuoja mažai; su nugaros priekinės išlaidos 15-30 dB.

## 10 kvartalų. ITM (Longley-Rice) ir išėjimas iš ortakio

NTIA / ITS 1.2.2 modelis (Longley ir Rice, ESSA ERL 79-ITS 67, 1968; Hufford, Longley ir Kishey, NTIA 82-100, 1982), išverstas į C # pagal oficialų JAV vyriausybės kodą, kuris yra laisvas nuo teisių. Vertimas suteikia tuos pačius numerius, kaip ir originalus: automatinis kompiliavimas kiekvieną kartą palygina su NTIA C + + kodu 5 bandymų atvejais (žemiau 0,01 dB).

ITM apskaičiuoja normalaus atmosferos praradimo realiame profilyje Copernicus GLO- 30: matyti atspindys nuo žemės, difrakcijos (spigolis ir sferinės žemės pasverti dirvožemio šiurkštumą Δh, tai yra tarp 10-90 ° procentilės kvotų aplink mažiausio kvadratų tiesia) ir troposcatter skirtumas. Dirvožemio reaktyvumas sumažinamas iki vidutinės maršruto dalies $N_s = N_0\,e^{-h/9460}$, ir nulemia atitinkamos žemės išlinkį,

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

Programa naudoja radijo klimatą pasirinkto maršruto, kaip § 10 sex (iki 1.3.4 visada vidutinio jūros, su N0 320 ant žemės ir 350 ant jūros), su N0 iš vidutinio reaktyvumo vietoje (dirvožemio εr 15, Bendrijoje 0,005 S / m; jūros εr 81, Bendrijoje 5 S / m), horizontalios poliarizacijos ir kintamumas laikui bėgant be šios vietos (profilis žinomas). Čia matyti, kad nuostoliai viršijo 50% laiko ir vieną turite 10% ir 1% geriausio laiko. ITM nemodeliuoja kanalų: jo kintamumas laikui bėgant yra statistinis, priklausomai nuo klimato. Raktas naudojamas kaip antras atskaitos taško apskaičiavimas šalia P.452; skirtumas tarp šių dviejų suteikia netikrumo idėją.

Antrasis naudojimas yra galutinis ruožas po vamzdyno, kuris baigiasi prieš atitinkamą. Bekorpusyje virtuali antenA įrengiama įpusėjus vamzdynui, ir ITM apskaičiuoja į atitinkamą trasą kaip laisvos erdvės 50% laiko perteklių. Trūkumai

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,\theta_{condotto} + A_{ITM}(d_{uscita} \to RX)
```

Čia A _ TX - antenos sujungimas (3 dB viduje, 1 dB kas 10 m išorėje), jei kanalas prasideda netoli, priešingu atveju 10 ter difrakcijos taškas. Čia yra riba, kurią autorius pats sau davė: sluoksnio tęstinumas plius, ne daugiau, liekamasis silpninimas. Blokavimo mechanizmas ("elgesys (išėjimas ir galutinis ruožas ITM)")

## 10 kvinatų. Gerinimas: salų, sąsiaurių ir jūros nestabilumas

Pavyzdys iš Neapolio įlankos (Anacapri, JN70CN) link Vakarų Viduržemio jūros rodo, kur maršrutai per salas:

| Paskirtis | Azimutas | Ten, kur jis kerta salas | Kliūtys |
| --- | --- | --- | --- |
| Barselona |  279°  | į šiaurę nuo Sardinijos (Gallura) iki 41,05 ° N - 420-490 km | apie 30 km į pietus nuo Bocche di Bonifacio; Limbara (1359 m) apie 20 km nuo linijos |
| Bocche di Bonifacio |  282°  | nuo siauros iki 41,3 ° šiaurės platumos | apie 11 km |
| Marseille |  296°  | Centrinė KorsikA 42,2 ° šiaurės platumos | kalnai iki 2700 m: iki 2 km stotelė |
| Valensija, Balearų salos |  260–265°  | Centrinė Sardinija | Vidiniai atlaidai |

Barselonoje pakanka nustatyti 282 °, vietoj 279 °, nes ašis eina iš žiočių: 3, viduje normalus jagi spindulių.

Burnos neaiškios? Žemiems ortakiams, mažesniems nei keli šimtai metrų, taip: salos užtvindytos, o virš žemės, diena pralaužia apšildymą. Kraštas yra pakankamai platus: apie 11 km, nuo pirmojo Fresnel ploto, mažiau nei 1 km pločio, pusė 1000 km maršruto 144 MHz. Bėda - oras. Moutai yra malonus vėjo: Apatinis sluoksnis pagreitintas, o apatinis - pašalintas. Dažnai ištrauka yra atvira kaip geometrija, bet sutrikdyta kaip atmosferA. Vėjas su pastoviu anticiklonu gali būti geras kelias.

Žemės ir jūros skirtumai. Skaitau. Taurų pakrantėje suformuotas konduItas yra nutraukiamas: signalas išeina iš vamzdyno (po išėjimo, § 10 quater) ir turi grįžti į kitą konduItą Balearų jūroje, mokėti vėl sukabinimo. Virš 1500 m aukštyje esantys vamzdynai pervažiuoja Gallura nesuvokdami jos, o centrinei Korsikai tai netaikoma.

Praktikoje: jei maršrutas kerta kalnuotą salą, pabandykite šiek tiek koreguoti link laisvosios jūros ruožas, bet nesitiki ištrauka su stipriu vėju siauroje.

## 10 sex. TN101 radijo klimatas ir laiko kintamumas (nuo 1.3.5)

TN101 § 10 [25] aprašo, kaip maršruto praradimas už horizonto skiriasi per visą vidutinį laikotarpį, su skirtingomis kreivėmis pagal klimatą: Y (q) funkcija rodo, kiek dB nuostoliai sumažėja per Q% geriausio laiko. ITM yra tos kreivės septynių klimato. klimatas labai svarbus: jūrose esant vidutiniam jūros klimatui (144 MHz), 10% laiko yra daug daugiau dB, nei sausumoje žemyne, nes ortakiai yra dažnesni.

Programa nebetaiso klimato: Jus pasirenka kiekvienam maršrutui iš vidutinio taško, todėl jis veikia ne tik Europoje, bet ir visame pasaulyje. Parsisiųsti iš Open- Mete- [26] trejų metų dienos vertes ERA5 reanalizės (temperatūros, drėgmės ir dirvožemio slėgio, lietaus) už 0,5 ° vidutinio taško ląstelių, vieną kartą; vertės lieka duomenų / CLIMA. Dirvožemio atkūrimas

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

diena iš dienos, tada metinis vidurkis ir nuokrypis tarp mėnesio vidurkių, vidutinio drėgnumo ir metinio lietaus. Taisyklės atitinka TN101 10.2 dalies klimatų aprašymus; ribos tarp vieno klimato ir kito yra autoriaus kriterijus:

- daugiau nei 60 ° platumos: vidutinio dydžio kontinentinis ( TN101 neturi polinio klimato kreivių),
- mažiau nei 250 mm lietaus per metus, vidutinis drėgnumas mažesnis nei 50%: dykuma (N tipinis 280);
- ne toliau kaip 20 ° nuo ekvatoriaus, N - ne mažiau kaip 350, o nuokrypis - ne daugiau kaip 30: vidurinis (N - tipinis 360),
- 32 ° ir ne mažiau kaip 60: subtropinių kontinentinių, tai yra monsoons (tipiškas nuokrypis 60- 100);
- 10-32 °, N - ne mažiau kaip 340 (tipiniai N - 370);
- abu horizontai jūroje, virš 20 °: vidutinio klimato jūra jūroje (tai TN101 apibrėžimas);
- Ekskursija (iki 30) prie jūros (jūra maršrute arba vidutinė drėgmė - ne mažiau kaip 75%): jūros dugnu;
- kitaip vidutinio žemyno (tipiškas ekskursija 20-40).

ITM N0 yra vidurkis, sumažintas iki jūros lygio, N0 = Ns · E ^ (h / 9460). Trūkstant tinklo, naudojama rezervinė taisyklė su platuma ir jūros dalimi, ir linija tai sako.

Bandymai su ERA5 2023- 2025: Capri NS 339 su ekskursija 45 (kontinentinis tempta ant žemės, jūros temperate, kai abu horizontai yra jūroje); Londonas 327 ir 16 (temperated žvaigždė); Vienna 319 ir 24 Abidžanas 381 ir 11 (pusiaujo); Majamis 365 ir 36 (subtropinis vyras); Delis 337 ir 82 (subtropinis continental); Riad 278 ir 37, 90 mm lietaus (dykuma). Čilės: Antifagasta 341 ir 19 (subtropinis vyras: Atakamos pakrantėje, su beveik nustatyta žema reverse Ramiojo vandenyno), Calama 232 ant žemės, 2257 m, 14 mm lietaus (dykuma), Valparaíso 331 ir 11 (temperatūros siūlės), Punta Arenas 313 ir 5 (temperatūros siūlės).

Linija "Radijo klimatas ( TN101)" rodo klimatą, priežastis, N su puse ekskursijos ir Y: kiek dBs uždirbti 10% ir 1% laiko ir kiek prarasti 90%. Čia yra ilgalaikiai statistiniai duomenys ir jie yra atskirti nuo prognozės: laukiami nuostoliai po to, kai šiandien GFS profiliai, ir susumuoti du kartus tas pats nenormalus dauginimas.

## 10 sepijų. Spindulių numatymo ir tikrinimo patikimumas (nuo 1.4)

Remonto patikrinimas. GFS mato ortakius mažiau nei atrodo: Šalia žemės jos vertikali skiriamoji geba - dešimt metrų, o atvirkštinės linijos sluoksnis - dažnai plonesnis. Norintys sužinoti, kiek galima pasitikėti šiuo modeliu čia ir šiomis dienomis, programa lygina jį su realiomis artimiausios stoties radijo bangomis, iš NOAA [27] (viešo domeno) archyvo IGRA v2:

- iš stočių sąrašo paima artimiausią aktyvųjį per 600 km (Capri, Mare praktika, ITM00016245);
- atsisiųskite einamųjų metų failą (jis atnaujinamas kas kelias dienas) ir saugo paleidimą 00 ir 12 UTC per 15 dienų;
- kiekvieno paleidimo metu reaktyvumo N lygis nustatomas pagal lygį (su rasos taško drėgnumu arba nutrūkimu) - lygių, kurie jų neturi, kvotas, pradedant nuo stoties aukščio, pirmųjų kilometrų gradiento ir stipriausio kontūro pagal 3000 m (M, kuris patenka su ne mažiau kaip 2 vienetų kvota);
- parsisiųsdinti iš istorinių prognozių Open-Meteo [15] BFS profilius iš to paties taško tomis pačiomis valandomis ir juos analizuoti su tuo pačiu kodu prognozės;
- skaičiuoja pastebėtus ortakius, kuriuos matė GFS, trūkstamus ir netikrus pavojaus signalus, ir vidutinį ir kvadratinį nuolydį.

Patikra atliekama pirmojo skaičiavimo metu, ne dažniau kaip kas 12 valandų; bylos lieka duomenų / IGRA ir skaičiai skaičiavimo žurnale. Radionosonde pranešimai iš pasaulio oro tinklo (TMP) turi tik standartinius ir svarbius lygius: labai plonas konduitai taip pat pabėgti jiems, ir palyginimas sako, visų pirma, jei GFS yra labai blogas viena ar kita prasme.

Patikimumas. "Prieinamumo" linija suteikia didelį, vidutinį arba mažą sprendimą dėl numatomo pelno. Nekalibruota tikimybė, kuri reikalauja metų palyginti su realiomis nuorodomis (§ 14), bet taškų suma, kaip nepriklausomi clues (autoriaus kodas):

| Adresas | Taškai |
|---|---|
| Modelių susitarimas: vidutinis nuokrypis tarp tikėtino nuostolio ir P.452 su vamzdynais, paraboline lygtimi, ir tarp bazinio lygio ir ITM | iki 3 dB + 1; daugiau nei 8 dB -1 |
| Avansinė prognozė | nuo 24 valandų + 1; nuo 72 valandų - 1 |
| Gydymo pradžios sluoksnio tęstinumas | ne mažiau kaip 70% + 1; mažiau kaip 30% − 1 |
| Sluoksnio patvarumas kaimynystėje valandų | ne mažiau kaip 70% + 1 |
| Radionavigacinė patikra | GFS tik 3 kartus iš 4 + 1; mažiau nei 1 iš 2 − 1; klaidingas gradientas daugiau nei 15 N / km − 1 (iš viso ne daugiau - 1) |
| Live controlling in the route (klaster, PSKReporter, APRS, FM- DX), tik šiuo metu | 1-2 poros stočių + 1; 3 arba daugiau + 2; angos, kurių numatomas prieaugis mažesnis nei 2 dB -1 |

Aukštas nuo 3 balų, vidutinis nuo 0 iki 2, mažas žemiau nulio. Gyvas patvirtinimas skaičiuojamas kaip skirtingų stočių pora 20 ° nuo maršruto kryptimi, o vidutinis taškas - 150 km nuo linijos, paskutinę valandą su puse. Archyvų atveju (§ 3 ter) nėra radijo arba gyvų patvirtinimų.

## 11. Palyginimas su ITU- R P.452

Palyginimo linija P.452 taikomas ITU metodas, taikomas ortakiams ir apmąstymams ant sluoksnių (P.452 § 4.5 [6]), kai ΔN mediana yra GFS, vietoj klimatologinės, ir p = 20%, kaip kalibravimo konstanta, kaip ir CSIRO [10] tyrime. (apie 28 dB - 144 MHz, 3.5)

P. 452-18 baigtas ir patikrintas. Palyginimas dabar naudoja visą pagrindinį metodą P.452-18 [29] (§ 4.1-4.6): laisvos erdvės su dujomis ir daugiapiločiu, difrakcijos su sferinės žemės ir Bullington profilį ant teisingo lygaus paviršiaus kliūčių, troposcatter, ortakiai ir atspindys sluoksnių, ir jų derinys. ΔN ir N0 yra iš GFS ir vietoje ITU žemėlapių vietoje vietos. Kodas buvo patikrintas oficialiais ITU 3 tyrimo grupės (projekto Py452 [29]) bandymų atvejais: 194 atvejais iki 2 GHz be pagrindo vidutinis nuokrypis nuo bendro nuostolio yra 0,06 dB, maksimalus 0,54 dB; vidutinis difrakcijos - 0,10 dB, troposcatter - 0,002 dB. Patikroje buvo ištaisytos dvi ankstesnių versijų klaidos: ortakių sujungimo terminas naudojamas MHz dažniu, vietoj GHz (apie 60 dB per didelio kompensavimo kitur), o difrakcijos nukrypo nuo jau ir taip riboto lygaus paviršiaus kvotų, kai tam tikrų profilių paklaidos siekia 15 dB.

ITM (§ 10 quater) taip pat buvo lyginamas su NTIA [30] (point): penkiuose oficialios bylos skirsniuose dB centui yra nustatytos tokios pačios vertės.

## 11 bis. parabolinė lygtis (nuo 1.3.1)

Parabolinė Lygties linija yra ketvirtas skaičiavimas, tik palyginimas, pagamintas kaip profesinės programos, skirtos ortakiams (PETOL; bangų sklidimo sistema M. Lytaev, MIT licencija): vietoj apskaičiuoti pelną iš sluoksnių, tai avansas išilgai maršruto Furjė split- žingsnis metodas viduje M pakeistas GFS refrakcijos profilis, paimtas iš prognozės taško arčiau kiekvieno atstumo. GFS lygiai pridedami prie apskaičiuotų galimų kanalų šuolių (skaičiuojant A, 50 m per pusę sluoksniu). Dirvožemio Copernicus GLO-30 patenka į žingsnius (po žeme laukas yra nulinis), dirva yra laidininkas, ir antenos spindulis yra Gaussian su apskaičiuoto atidarymo nuo pelno. Skaičius mato konduitai, tie, kurie praranda, difrakcijos ir antenų sukabinimo; jis nemato troposcatter, kuri yra pridėta atskirai. Sekantis yra "profilių čekį": tas pats su standartine atmosferA, mažiau su tikimasi profilis.

nuo 1.3.2:

- daugiklis yra plačiu kampu, su tikslia laisvosios erdvės fazės:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- apačioje, ant jūros ir ant pakrantės lygumos nulinio aukščio atspindi su Fresnel koeficientas horizontalioje poliarizacijos, εc = εr − j · 60 · λ · opharm (jūros εr = 81 ir ← = 5 S / m; šalis 13 ir 0,006 S / m), aiškiu kampu maršruto:

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- jūros šiurkštumas sumažina nuoseklų atspindį pagal Miller ir Brown, su efektyviu aukščio bangų nuo vėjo 10 m (σh = 0,0051 U2):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- jūroje M profilis apima garavimo ortakį (9 ter) su neutraliu logaritminiu profiliu:

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

Testuoja prototipą (10 m antenA ant jūros, įgauna standartinę atmosferą 100 -300 km): su δ = 10-30 m garinimo kanalas užtikrina 1-3-144 MHz, 8-37-432 MHz ir 21- 66-1296 MHz dažnių juostas. 144 MHz horizontalios poliarizacijos metu jūra atspindi beveik kaip metalas (maždaug 124; η 124; nuo 0,99 iki kelių dešimtųjų laipsnio), o grubumas - mažai: naujasis apatinis taškas keičia kelias dešimtąsias dB, jis daugiau nuo 432 MHz aukštyn.

Ribos: virš žemės išlieka GFS lygių vertikalusis profilis, todėl ploni ortakiai išlieka nematomi; žemė iki laiptelių yra apeinama; spindulys yra 6.

## 12. antenos, signalas ir triukšmas

- YagI DL6WU: padidėjimas 9, 5 + 7, 8 · log10 (bum / λ) DBd (2.35 dB dvigubam bumui, DL6WU VHF ryšio priemonėms 3 / 1982), pridėjus 2.15 dBi. [12]
- EIRP = galia − kabelis + TX padidėjimas.
- Signalas gautas = EIRP - laukiamas nuostolis + pelnas RX - kabelis RX. S- metras IARU: S9 = − 93 dBm, 6 dB vienam vienetui.
- Išorinis triukšmas pagal ITU- R P.372 [13]: galaktinis (FA = 52 -23 log f) labiau dirbtinis pagal aplinką (miestas, gyvenamasis, kaimo, tylus kaimo) arba ranka parašyta antenA temperatūra.
- Tsys = TA + Tpre + [290 (Lc − 1) + 290 (Fr − 1) · Lc] / Gpre iki antenos terminalų (Fris formulė). nuo 1.3.5 priėmimo stotyje visada yra antenA, fiksuotas parametras: triukšmo skaičius 0,8 dB (Tpre = 59 K) ir 20 dB. Virš jo yra kabelis (Lc nuostoliai) ir radijas, su pagrindiniu Fr triukšmo 6 dB. 1 dB kabeliu kabelis ir radijo indėlis krenta nuo 1160 K iki 12 K: triukšmas nulemia dangų, aplinką ir preampą. 20 dB prieaugis yra tipiškas programos pasirinktas dydis.
- S / N SSB, CW ir FT8 kiekvieno kelio juostoje, palyginti su nustatyta riba.
- Laikotarpio tikimybė (nuo 1.3.2): per valandą pakilimas yra normalus S / N pasiskirstymas apie numatomą vidutinę valandinę vertę. Brazė (QST, 1961 m. lapkričio mėn., p. 39, Norton 1959 m. kreivėse) maždaug už 100 mylių suteikia 13,5 dB amplitudę, t. y. maždaug 7 dB žemiau ir 7 - aukščiau už vidurkį, kuris beveik nukrenta žemiau 100 mylių (nuo 3,5 dB iki 50 mylių), o "kietam" ryšiui iš biudžeto reikia 7 dB. Turima (± 7 dB) kaip ± 2Ω: (3,4 dB), kai važiuojama 160 km, proporcinga toliau nurodytam atstumui. tikimybė, kad laikotarpis viršys kelio ribą, yra

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  su normalia pasiskirstymo funkcija: 0 dB = 50%, + 3,4 dB = 84%, + 7 dB (kietoji "Bray" marža) = 98%. Teiginys neapima didesnio modelio neapibrėžtumo.
- Skaitmeniniai būdai: S / N 2500 Hz, palyginti su ribinėmis vertėmis, išmatuotomis esant 50% dekodavimo, be išankstinės informacijos (AP). FT8 -21 dB ir FT4 -17,5 dB baltame triukšme, kai mechanizmas yra troposcter, naudoja slenksčius su vanescencija "trikdančia platumo terpe", FT8 -18,6 dB ir FT4 -15,2 dB [21]. Q65 -15C -22,2 dB, Q65 -30B -24,8 dB, Q65 -60A -27,6 dB Orlaivių sklaidos Doplerio gali viršyti Q65 -60A (1,7 Hz) tonų: yra geriau nei 30B arba 15C. JT65A -25 dB [22]. Mobilioji AP (jau žinomo QSO pavadinimo arba jo pabaigos) suteikia apie 2 dB papildomą FT8 vertę, 1,5-2,6 dB - Q65 vertę; ji neįeina į ribą, nes negalioja visoms FT8 "-24 dB" reikšmėms, kurios dažnai perskaitomos X14Q atveju, yra VSJT santykių arba AP dekodavimo riba, ne 50% riba. [24]

## 13. atsako ir duomenų laikas

Kiekvienas skaičiavimas matuoja savo laiką: GFS profiliai (tinklas, analizė, privalomi pertraukos 20 s tarp partijų Open- Meteo riba), Copernicus GLO- 30 reljefas (su atsisiuntimu plokštės, 15-30 MB kiekvienas, tik pirmą kartą), skaičiavimas. @ info: tooltip Modernizuojant tinklą, registre taip pat pateikiamos BFS lenktynių, kai ji tapo prieinama ir kiek metų duomenys yra.

nuo 1.3.2 taip pat galite pasirinkti ICONE-2I, Agentūros modelis ItaliaMeteo ir Arpae Italijoje apie 2.2 km, per Open-Meteo (modelis ICONE-_ ARPEE _ Icon _ 2I, dvi rasės per dieną, 3 dienos). BTS apima tik Italiją ir netoliese esančias jūras, kurių GFS lygis yra žemesnis (1000, 925, 850, 700, 500 hPa): ortakiai aukštyje nėra geresni už GFS, bet smulkusis tinklas apibūdina geras pakrantes, salas ir jūrą 2 m atstumu, t. y. garavimo ortakį. Kintamieji, kurių modelis neturi (statmena riba, paviršiaus temperatūra...), pašalinami iš prašymo. nuo 1.3.2 automatinis šaltinis naudoja tik NOMADS; Open- Meteo tarnauja, jei NOMADS nereaguoja ir istorinių atvejų. 'Update by self' langelyje (apšviesta defekto) prognozė yra nekliudoma: atidarymo metu, kiekvieną naują BFS lenktynes NOMADS (apie 5 valandos po lenktynių) ir kai matoma zona ateina iš tinklo daugiau nei ketvirtį; ne daugiau kaip bandymas kas 15 minučių, niekada su istoriniu atveju.

Prašymai Open-Meteo turi ribas per minutę, per valandą ir per dieną kiekvieno interneto adresas. Atsisiųsti profiliai yra išsaugoti diske (Dati / cache- gfs) kiekvienam insultui ir modeliui: su tomis pačiomis lenktynėmis, kurios keičiasi kas 6 valandas, atsiųsti taškai daugiau neprašo. Automatiniu šaltiniu galite pradėti nuo NOMADS ir Open- Meteo tik tarnauti trūkstamų taškų; tik Open- Meteo, metu arba kasdien sustoja iš karto ir sako, kada bandyti vėl. Istoriniai atvejai neišbraukti.

## 14. Modelis patvirtintas

prognozę reikėtų palyginti su realiomis jungtimis. Skaičių žurnalas saugo kiekvieną skaičių; programos archyvas išlaiko vietoje ir dekodavimo. 432 ir 1296 MHz dažnių juostose, kuriose taškų yra nedaug, jiems reikia operatorių ataskaitų: kas sukūrė ryšį, iki kurio laiko, su kokiu signalu. Palyginkite daug valandų numatomas pelnas su išmatuotu signalu sako, jei slenksčiai turi būti perkeltas.

P.452 ir ITM naudojamos bandymų programos yra pradinio kodo įrankių aplanke; ITU bandymų bylos turi būti atsisiųstos atskirai, nes programa neplatina ITU duomenų.

## Šaltiniai

1. NOAA / NCAP, pasaulinė prognozavimo sistema (GFS), pasaulinis modelis (0,25 °), keturios rasės per dieną.
2. Open-Meteo, prognozavimo API (open- meteo.com), Gfs _ switch modelis, kintamieji slėgio lygio ir lenktynių metaduomenis.
3. Copernicus GLO- 30 DEM (ESA; Pasaulinė DEM © DLR e.V. 2010-2014 © Airbus Defence and Space GmbH 2014 -2018, tiekiama kaip programos "Copernicus" dalis Europos Sąjungos ir EKA), 1-asis lankas, GeoTIFF rėmeliai atvirame "Amazon" interneto paslaugų registre. Rezervas: NASA / USGS Copernicus GLO-30 Terrain Tiles "skadI" AWS.
4. ITU-R P.453, III PRIEDAS
5. B. R. Bean, E. J. Dutton, Radio Meteorologija, NBS Monograph 92, 1966: Lentelė 6.5 ir § 4.4.
6. ITU- R P.452-16, prognozavimo procedūros, skirtos įvertinti trikdžius tarp stočių Žemės paviršiuje, kai dažniai viršija 0,1 GHz: § 4.2 (difrakcijos), eq. 45 (troposcatter), § 4.5 ir eq. 49 (atlikta).
7. ITU- R P.834, Troposferinės refrakcijos poveikis radiacijos sklidimui.
8. ITU- R P.525, Laisvos erdvės silpninimo apskaičiavimas.
9. ITU- R P.526, Propagacija difrakcijos būdu: eq. 31 (punčonas) ir § 3 (sferinės žemės).
10. B. Indermuehle, H. SuzukI, Tropospheric Ducting Prediction based on GFS Model Data at Inyarrimanha IlgarI BundarA, the CSIRO Murchison Radioastronomy Observation, Publications of the Astronomical Society of Australia, 2026 (arXiv 2608.12861).
11. D. E. Kerr, Propagation of Trumpo radijo bangų, MIT Radiation Laboratory Series vol. 13, 1951: eq. 17 e 18.
12. G. Hoch DL6WU, "Extremely Long YagI Antennas", VHF Communications 3 / 1982, pp. 130 et seq., fig. 1 (matuojama 432 ir 1296 MHz pagal bumo ilgį).
13. ITU- R P.372, Radijo triukšmas: galaktinis ir dirbtinis triukšmas aplinkai.
14. NOAA / NECP, NMADS, GRIB filtras GFS 0,25 ° laiku (nomads.ncep.noa.gov); WMO G RIB2 formatas, sudėtingas pakavimas su erdvės skirtumais.
15. Open-Meteo, Istorinė prognozė API (modelio prognozė archyvas nuo maždaug 2021).
16. Luki Box, TropoLink, troposcatter maršrutai su P.617, NBS TN101 ir ITM flanked (GitHub, Apache 2.0 licencija).
17. ITU- R P.617-5, Propagacijos prognozavimo metodai ir duomenys, reikalingi horizonto radijo relės sistemų projektavimui, 2019.
18. RSGB, VHF / UHF Handbook; DL2FZN implementation (andi- f / troposcatter on GitHub, 2013).
19. W. F. Moler, D. B. Holden, "Troposferos sklaidos ir atmosferos cirkuliacijos", Journal of Research of the NBS 64D, 1960, doI 10.6028 / jres.064d.014: Signalas už horizonto (1365 ir 9380 MHz, jūroje) ir vertikalaus oro judėjimo.
20. III PRIEDAS
21. S. Franke, B. Somerville, J. Taylor, The FT4 and FT8 Communication Protocol, QEX July / August 2020.
22. W3SZ, skaitmeninio lėto režimo ribinės vertės ( JT65, JT9, QRA64) ir režimas pagal dauginimą, 2019.
23. QST "Pasaulis virš 50 MHz", 2022 m. lapkritis: 2022 m. rugpjūčio 14 -15 dienomis tarp Manitobos ir Kanzaso / Misūrio.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Quick- Start Guide to Q65, 3 April 2021 (wsjt.sourceforge.IO).
25. P. L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis, Transmission Loss Preditions for Tropospheric Communication Circuits, NBS Technical Note 101, Žurnalas 1967: § 9 (troposcatter, function F (θd), H0), § 10 ir III priedas (radio climate and variability in time).
26. Open- Mete-, Istorinis Orų API (ECMWF ERA5 apžvalga nuo 1940; Copernicus klimato kaitos paslauga, CC BY 4.0 licencija).
27. I. Durre, X. Yin, R. S. Vose, S. applequist, J. Arnfield, Integrated Global Radiosonde Archive (IGRA), version 2, NOAA National Centers for Environmental Information, doI 10.7289 / V5X63K0Q (viešasis domenas): igra2-da- format.txt duomenų formatas ir stočių sąrašas.
28. FM-DX Webserver (NoobishSVK ir bendradarbiai, licencija GPL- 3.0) ir viešas sąrašas serverio servers.fmdx.org.
29. ITU- R P.452-18, prognozavimo procedūros, skirtos įvertinti Žemės paviršiaus stočių trukdžius, kai dažniai viršija 0,1 GHz, 2023; 3 tyrimo grupės (patvirtinimo pavyzdžiai) bandymų atvejai, surinkti I. Stevanovic, Py452 (GitHub).
30. NTIA / ITS, nereguliarus terrain Model (ITM) 1.2.2, referencinis kodas ir taškų bandymo atvejai (GitHub NTIA / Itm).
31. ITU- R M.1371, Techninės automatinio identifikavimo sistemos charakteristikos, naudojant laiko paskirstymo daugkartinę prieigą VHF jūrų mobiliųjų dažnių juostoje.
