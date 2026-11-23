# Prognozės vadovas

> Automatinis vertimas italų kalba: jei kyla abejonių, verta Italijos vadovas.

Prognozės skirtukas apskaičiuoja tropošerinis dauginimas pats: rodyklės žemėlapis matomo ploto ir signalas, pateiktas tarp jūsų stoties ir atitinkamos. Šiame vadove aprašomi visi duomenys, kaip jie apskaičiuojami ir iš kurio jie gaunami. Skaičiai laužtiniuose skliaustuose nurodo šaltinius apačioje; programoje kiekvienas rezultatų duomenys turi komiksų (simbolo i) su tuo pačiu šaltiniu ir nuoroda į punktą.

## 1. Ką jis daro ir ką daro

GFS [1, 2] modelio vertikalios temperatūros ir drėgmės profilių prognozė lemia oro sluoksnio reaktyvumą pagal sluoksnį ir įvertina, kiek signalo prieaugis, palyginti su įprasta atmosfera. Maršrute jis prideda realią žemę (Copernicus GLO- 30 [3]), difrakcijos, troposcatter, ortakiai ir imtuvų triukšmo.

Tai nėra priemonė: tai apytikriai apskaičiuotas oro modelis su tolimais lygiais šimtais metrų. Ji tarnauja palyginti situacijas (šiandien geriau nei vakar, 70 cm geriau nei 2 m, tai gydyti geriau nei tai), o ne užtikrinti ryšį. 14 straipsnyje paaiškinama, kaip rinkti įrodymus, kad jis būtų patvirtintas.


> "Dauginimas negali būti apibendrintas, bet tiriamas atsižvelgiant į vietinę morfologiją."
> - Volker Grasmann, DF5AI, pranešė Flavio Egano, IK3XTV (Propagacija 2 metrais)

Tai principas, kuriuo vadovaujamasi apskaičiuojant taškus: kiekvienas maršrutas yra apskaičiuojamas ant jo dirvožemio, su jo horizontais, jūra ir jos reljefais; žemėlapis suteikia bendrą vaizdą, gydo atsakymą.

## 2. Kalbų komandos

- Grupė: 144, 432 arba 1296 MHz. Žemėlapis ir skaičiavimas naudoti pasirinktą juostą.
- Dabar: Žymeklis juda prognozę nuo dabar iki 72 valandos į priekį, per 3 valandas.
- Atnaujinimo prognozė (matoma zona): atsisiųsti GFS profilius tinklelyje iki 240 taškų, apimančiame matomą žemėlapį. Tinklelio žingsnis (ne mažesnis kaip 0,5 °) priklauso nuo didinimo.
- Sustabdyti: pertraukti atsiuntimą arba apskaičiavimą.
- Informacija, kurią reikia parodyti (dešinysis žemėlapio skydelis): trys lygiai, esminiai, normalūs ir pilni, įjunkite dėžučių grupę; kiekvienas langelis gali pasikeisti ranka. Langeliai susideda iš trijų grupių:
  - Žemėlapyje: izolee del Gain, taip pat 3- 7 dB (traukos), izobare, terminė inversija, meteorologiniai ženklai S ↓ ↑, koreliacijos padidėjimas / presavimas, tiesioginio matymo zona (4, 5, 7 ir 9 bis dalys). Spalvos visada išlieka.
  - Skaičiuojant signalą: visos apskaičiavimo eilutės, plokščias Žemės profilis, skaičiavimo žurnalas ir detalus failų žurnalas.
  - Korekcijos ir gyvieji duomenys: oro pataisymai (Misme, subsidence), kontrastas ore ir jūroje, vidutinis su dėmėmis (corridoi), APRS 2 m šuoliai ir LoRa, už kalno be nuostolių (4 bis, 9 bis, 9 quater ir 10 bis dalys).
  - Esminiai turi tik izoliatai, oras ir medija pataisymai su dėmėmis; Normalus prideda izobare, inversija, oro ženklai, tiesioginis vaizdas, plokščia žemė, oro jūra ir APRS; Užbaigti apverčia jį. Lygis keičia tik tai, ką matote ir ką įveda pataisymai: skaičiavimai lieka tokie patys.
- Duomenų šaltinis ir istorinis atvejis: iš kur profiliai ateina ir kurią dieną jums atrodo (3 bis ir 3 ter dalys).
- Mano stotis ir korespondentas: lokatorius, antenos, galia, kabeliai, triukšmas (7 ir 12 dalys).
- Signalas ir aprėptis apskaičiuojami optika: du skaičiavimai ant žemės (8-13 pastraipos).
- Skaičiavimo registras: kiekvienas žingsnis su panaudotais skaičiais, įskaitant duomenų / prognozavimo-calcoli.log failą.

## 3. Nuo GFS duomenų iki profilio

Kiekvieno tinklelio taško ir kiekvieną valandą mes skaitome slėgį į žemę, temperatūra ir drėgmė 2 m, vėjas 10 m, ribinio sluoksnio aukštis, CAPE ir temperatūra ir drėgmė, atsižvelgiant į 1000, 975, 950, 925, 900, 850, 800, 700, 600 ir 500 hPa. Nuo 1.3.0 taip pat galite skaityti vėjas į 850 hPa ir vertikalus greitis į 850 ir 700 hPa (9 bis dalis).

Lygiai yra apskaičiuojami su iptomic formulė ir virtuali temperatūra; lygiai žemiau žemės yra mažai. Kiekvieno lygio:

- N = 77,6 / T · (P + 4810 · e / T), su T kelvinu, P ir hPa, ir ITU- R P.453- 14, eq. 9 soties slėgiu, su AP korekcijos koeficientu (pakartotinai kontroliuojama oficialiame tekste) [4].
- M = N + 157 · h, su h km: modifikuotas reaktyvumas. Ten, kur eina M, yra kanalas.

## 3 bis. Duomenų šaltinis

Duomenų šaltinis meniu pasirenka, kur profiliai yra iš:

- Automatinis (rekomenduojamas): GFS pirmas iš NOMADS [14], NOAA paslauga, kuri platina modelį: kiekvieną akimirką programa prašo du iškarpas į G RIB2 formatu srityje ir juos skaito savaime; NOMADS neturi dienos limito, tik vieną minutę, kad programa gerbia. Su plačiais tinkleliais (nuo 0,5 ° iki daugiau) jis naudoja 0,5 ° arba 1 failus kas 3 valandas, daug lengvesni. Taškai, kad NOMADS nesuteikia, arba visi, jei NOMADS neatsako, paklausti Open- Meteo [2]. Du šaltiniai suteikia tą patį modelį ir tas pačias lenktynes, todėl jūs galite maišyti tame pačiame tinklelyje.
- GFS iš Open- Meteo: tik Open- Meteo, kaip 1.2 (ne per minutę tikimasi).
- NOMADS GFS iš NOAA: tik NOMADS. Sulėtinkite atsisiųsti (du failai per valandą, pusė sekundės tarp vieno prašymo ir kito ne viršyti NOAA ribą), bet ne vartoti Open- Meteo ribą.
- ICONES (DWD) ir ECMWF IFS 0,25 °, iš Open- Meteo: modeliai, išskyrus GFS, kurie taikomi visame tinkle ir maršrutams. Jie nederina su GFS: palyginti du modelius tuo pačiu metu yra paprastas būdas pamatyti, kaip prognozė yra neaiški. ICOES apima tik Europą; ECMWF neturi visų GFS slėgio lygių (trūkstamo lygio šuolis) ir gali būti ne ribinio lygmens.

Šaltinis naudojamas yra parašyta statuso eilutėje ir pirmoje eilutėje rezultatų (Duomenų šaltinis), su kiek taškų atėjo iš kiekvienos paslaugos ir kiek iš talpyklos. Disko talpykla atskiriama pagal modelį.

## 3 ter. Istoriniai atvejai

Istorinis Bylos meniu apžvelgia dokumentais įformintą atidarymą su Open- Meteo [15] archyvo GFS prognozėmis: žemėlapis juda į bylos sritį, juosta nustato save ir laiko žymeklį palieka nuo pirmosios bylos dienos. Paspaudus Atnaujinti prognozę atsisiųsti šių dienų profilius; signalo skaičiavimas veikia kaip dabartinės prognozės. Jis padeda palyginti modelį su tuo, kas iš tikrųjų įvyko.

Pirmasis atvejis yra tas, kad 2022 m. rugpjūčio 14- 15 sakė QST (lapkritis 2022, "Pasaulis virš 50 MHz" [23]): atidaryti aukšto slėgio tarp Manitobos ir Kansas / Missouri, nuo 144 iki 1296 MHz beveik visi FT8, su Kanados įrašo 10 GHz 1228,56 km Q65 ir signalai iki 1941 km. Antra, nuo 2021 m. gruodžio 29 d. iki 2022 m. sausio 2 d. atsiveria 144 MHz tironijos ir vakarinės Viduržemio jūros dalies linija nuo IC8TEM pranešimų: pirmosios angos 29 (iki 737 km), platesnės 31 ( IT9GSF - EB5EA 1217 km), o sausio 1 d. - tarp Italijos, Sicilijos ir Ispanijos. Senesniems atvejams (pvz., 2000 m. rugsėjo mėn.) Open- Meteo archyvas neturi duomenų. Archyve nėra ribinio sluoksnio aukščio: jo pataisa netaikoma.

## 4. Indeksas ir spalvos

Kiekvienam sluoksniui tarp dviejų lygių programa lygina N lašą su 6.5 lentelės ribomis Bean ir Dutton [5]: pavyzdžiui, 1000-850 hPa sumažėjimas iki 60 N / km yra normalus, daugiau nei 100 N / km yra superrefrakcija. Aukštesnio aktyvumo sluoksnis yra vertas 10-15 dB pelno; konduktas, kuris fiksuoja juostą nuo 20-25 dB, pagal M. Stipraus vėjo deficitą į žemę, aukštos ribos sluoksnis ir aukštos CAPE sumažinti pelną, nes jie pašalinti arba sulaužyti sluoksnius [5, 4.4 punktas].

Spalva yra apskaičiuotas pelnas dB, su fiksuoto masto lygias visose temose:

| Gain | Spalva | Reikšmė |
| Mažiau kaip 7 dB | Niekas | beveik normali atmosfera |
| 7-10 dB | mėlyna | gradientas virš normalaus |
| 10-13 dB | Žalieji | superrefrakcijos sluoksnis |
| 13- 16 dB | geltona | stiprus sluoksnis, bet be ortakio juostos |
| 16- 18,5 dB | oranžinis | labai stiprus sluoksnis arba per plonas ortakio |
| 18-21 dB | raudona | Stencils |
| nuo 21 dB | Violetinė | Stiprus ir gilus ortakio |

Be to, su ekranu 3-7 dB zonose 3-7 dB, kur kažkas yra formuojamas, bet lieka po skale, galite matyti su šviesiai pilka suvaržymo; spalvos nesikeičia. Taškuota plokštė demimituoja plotą, kuriam taikoma jos pati prognozė, ir rodo maksimalų matomą prieaugį: jei jis mažesnis nei 7 dB, žemėlapis lieka be spalvų, nes atmosfera yra beveik normali, o ne dėl to, kad trūksta duomenų. Atsisiunčiant naują spragtelėjimą ant Atnaujinimo prognozės nenutrūksta; sustabdyti tai yra Sustabdymas.

Spalvos yra nepermatomos iki 82%, todėl temos fonas tiesiog pakeičia juos. Vietovių tinklelis ir kvadratiniai pavadinimai yra skirti virš spalvų. Kur jūsų prognozė nėra ten, bet Hepburn arba F5LEN duoti 5 ar daugiau šachmatų kortelės atrodo su to šaltinio spalva.

Žemėlapis rodo kiekvieną tašką. Vietoj to signalo apskaičiavimas kontroliuoja, kad sluoksnis yra ištisinis maršrute (9 pastraipa): kelias, kuris kerta labai geltona gali duoti mažiau nei žemėlapis leidžia jums galvoti.

## 4 bis. Prognozuojama iš reklamos: koridoriai (nuo 1.3.3)

Copyright Policy: Nuoroda vien niekada neturi atkreipti tinklo. Atskiras ir trumpas priėmimas gali būti tranzitinės plokštumos sklaida (APRS paketas yra labai trumpas), kelių minučių atidarymas arba klaida, ir būtų suteikti "drop-elgesio" jie apgauti. Programa pripažįsta koridorių, kai, toje pačioje juostoje:

- ne mažiau kaip 3 poros nepriklausomų stočių (be bendros stoties) dirba ne mažiau kaip 300 km maršrutais;
- maršrutai yra lygiagretūs: azimutas - ± 15 °, vidutiniai taškai - 150 km nuo bendrosios ašies;
- per pastarąsias 30 minučių, kai spindulių pluoštas kartojasi bent 10 minučių atstumu (patvarumas);
- ARS atveju šuolis skaičiuojamas tik tuo atveju, jei tas pats šuolis kartojamas;
- nuo 1.3.4 maršrutai virš 1500 km skaičiuojami tik tuo atveju, jei spindulis sklinda iš bent dviejų skirtingų šaltinių (pvz., PSKReporter ir klasterio): nuo vieno šaltinio iki tų atstumų 2 m atstumu dažniau yra EME, sklaidos meteoritai arba klaidingi lokatoriai nei tropas.
- nuo 1.3.4, neskaičiuoti stočių "ray": kurie per paskutinę valandą turi ne mažiau kaip 5 korespondentai per 1000 km pasklido per ventiliatorius ne mažiau kaip 150 °. 2 m tropas yra kryptis; imtuvas, kuris "nemokamai" kartu Izraelyje, Portugalijoje, Anglijoje ir Italijoje nuo 2000- 3700 km, dažnai su teigiamais signalais, beveik neabejotinai dažnis sukonfigūruotas blogai. 17- 22 / 11 / 2026 I5MZY archyvas 72% apgyvendinimo 2 m virš 1000 km atvyko iš stočių, todėl 22 / 11 10: 53 UTC buvo OH6HLH, OZ1ITS, F4LUK, OM8ABC ir SP2JLR, ir su filtru ilgiausias koridorius nusileidžia nuo 2175 km (regole 1.3.3) iki 615 km.

Šaltiniai: klasterio dėklas (ne AS / ACS), PSKReporter per MTQTT, jo QSO ir WSJT-X / JTDX / MSHV dekoderiai ir APRS šuoliai per 2 m ir LORa 433 / 868 MHz.

APRS: 2 m ir LoRa. Programa skaito APRS- IS tik iš naujo (rotate.aprs2.net, filtras Europoje) ir išskiria radijo šuolius tarp žinomų vietos stočių (sorgente → digipeater → IGate), pavyzdžiui, VHF žemėlapį DXView ir APRS PropView (RF- YVY, Apache 2.0). Trys juostos yra vertingos: 2 m, LoRa 433 MHz (432 juostos koridoriai) ir LoRa 868 MHz (1296 MHz skaičiuojant corridoi - tai tinklas, kuris taip pat užtikrina aukščiausią 868 MHz dažnį). Juosta ateina iš LoRa totall (aprs- deviceid sąrašas) ir iš dažnių ar žodžių komentaro; šuolis yra verta tik tarp dviejų stočių tos pačios juostos.

Subtilus taškas yra aktyvus kartotuvas: ne 500 km gaunamas signalas gali būti pateiktas su dviejų ar trijų digipeater retransliacijos. Todėl šuolis skaičiuoja tik tada, jei jis yra tarp dviejų realių ir iš eilės siųstuvų:

- kelias turi būti atsektas: kiekvienas retransliuotas digipeteris įdėjo savo pavadinimą, o šuolis matuojamas tarp vieno ir kito; be pavadinimo naudojamas slapyvardis (W IDE1 *, W IDE2 *, regioninis slapyvardis) nutraukia grandinę ir ta atkarpa neskaičiuojama;
- paslėpta retransliacijos: programa prisimena kelią, kad kiekvienas šaltinis paprastai prašo (pavyzdžiui W IDE2 -2). Jei pakuotė yra su šuoliu į mažiau (W IDE2 -1) ir pavadinimas nepaaiškina, digipeater retransferred it without sequand the package is not found; šaltiniai niekada nematė anksčiau nėra skaičiuojami;
- Digipeater kelio viduryje: jei tarp dviejų stočių, daugiau ar mažiau kelyje, yra tos pačios aktyvios juostos digipeater per pastarąsias tris valandas, šuolis yra išmetama, nes jis galėjo būti retransmisijos (tai ypač digipeater LoRa, kad retransliuoti nekeičiant kelio);
- ne radijo paketai (qAC, qAX, TCPIP), tie, kurie eksploatuojami internetu radijo iš IGate (trečiųjų šalių), HF ir įvairių dažnių, stotys virš 1500 m aukščio (Palloni, orlaivių) ir šokinėja virš 1500 km.

Poveikis žemėlapiui (nuo 1.3.4). Ji nurodo tik plotą, kuriame galioja mažiausias garantuojamas dydis: ± 60 km ruožą aplink kiekvieno maršruto centrinę dalį (nuo 15% iki 85% jo ilgio), o ne stotyse, esančiose galuose, kur gali nebūti vamzdyno. Visų koridorių juostos yra sudarytas kartu, neutralios spalvos (tekstas) ir pusiau skaidrus: dubliavimosi nesudeda, ir spalva nesupainioja su skalės violetinės spalvos (21 dB ir už). Etiketės (savybės, vidutinis atstumas, mažiausias pelnas) skenuojamos viena su kita; einant su pele ant etiketės atsiranda koridoriaus maršrutai.

Teritorijoje ir tik netoli dabartinio laiko (± 90 minučių) prognozė nenukrenta žemiau minimumo. Argumentas yra toks: jei stotys tikrai praeis, sklidimas yra ten, kad ir ką oro modelis sako.

Kadangi trumpi maršrutai yra mažai verti (nuo 1.3.4). 2 m, tarp 300 ir 600 km, dvi geros stotys (apie šimtas vatų, antenos, FT8 arba CW) sujungti beveik kasdien net su normalia atmosfera, už troposcatter: signalas sklinda mažų pažeidimų oro bendro tūrio dviejų ryšulių. Trys ar keturios vietos 500 km aukštyje teigia, kad juosta veikia kaip įprasta, o ne kad yra anga: GEKO Žurnalas Nr. 69 ( IU3QEZ) apskaičiavimai troposcatter su 2 × 10 tekančių elementų apie 440 km SSB QRP, 510 km SSB su 50 W, 600 km CW QRP ir 640 km SSB su 500 W (šaltiniai, vienuolika ratas). Tik daugiau nei 600-700 km troposcatter nebėra pakankamai ir iš lygiagrečių jungčių serija tikrai rodo jungtį ar sluoksnį. Iki 1.3.3 450- 600 km koridorius padidino prognozę iki 9-12 dB, o žemėlapis parodė, kad padėtis pagerėjo taip, kaip nebuvo.

Todėl pradinė vertė atitinka vidutinį maršrutų atstumą:

- iki 400 km: 3 dB;
- 400-700 km: druska tiesiu būdu nuo 3 iki 10 dB (500 km → 5,3 dB; 600 km → 7,7 dB);
- nuo 700 iki 900 km: nuo 10 iki 16 dB; daugiau kaip 900 km lieka iki 16 dB.

Pagal 7 dB, tai yra pagal skalės pradžioje, minimalus lieka, kad: koridorius yra sudarytas su jo etiketė, matyti, kad juosta veikia, bet žemėlapio spalvos nesikeičia (su lange taip pat rodo 3- 7 dB pilka punktyrinė). Viršijus 7 dB, vertė pasveriama pusantro, kai nepriklausomų maršrutų skaičius (3 smūgiai → 0.25; 6 arba daugiau → 1) ir ryšio mediana S / N (− 20 dB → 0.5; 0 dB arba daugiau → 1; nežinoma, kaip klasterinėse vietose, → 0,75), pradedant nuo 7 dB. Pavyzdžiai: 3 maršrutai be S / N 500 km → 5,3 dB; 5 maršrutai 615 km → 7,7 dB; 3 maršrutai be S / N 900 km → 11,5 dB; 3 maršrutai iki − 20 dB 900 km → 10,4 dB; 6 maršrutai iki 0 dB 900 km → 16 dB.

Minimalus atstumas išlieka 300 km: pakėlus jį iki 500 km taip pat būtų pašalintos realios trumpojo nuotolio angos (pavyzdžiui, 400 km tiltelio tiltas Tyrhenian, kuris yra pripažintas orų prognoze ir labai stipriais signalais daugiau nei vien tik nuo atstumo). I5MZY archyve, 22 / 11 / 2026 10: 53 UTC, su šia taisykle ir spindulių filtras ilgesni koridoriai buvo 615 km (7,7 dB) ir 540 km (6,3 dB); su 1.3.3 taisyklės pasiekė 2175 km ir 16 dB.

Iki 1.3.3 minimumas galiojo visame koridoriaus stačiakampyje ir priklausė tik nuo atstumo (nuo 10 dB iki 300 dB iki 16 dB iki 900 km): daug ilgų koridorių kartu (pvz., 22 / 11 / 2026, dvylika koridorių iki 2620 km) pakėlė beveik visą Europą iki 16 dB.

Pelno kreivės nebetraukia "gocce": Mažosios salos 220 km abiem kryptimis lieka spalvos, bet be uždara kreivė, nebent koridorius patvirtina juos. Jis išsijungia su "Vaizdas tarpininkauja dėmės".

## 5. Neprivalomos linijos ir palyginimas su slėgiu ir inversija

Įgijimo kreivės: linijos 7, 10, 13, 16, 18,5 ir 21 dB, skalės kraštai ant lauko išlyginti žydėjimo kvadratų metodu, su etikete dB.

Izobaras: slėgis jūros lygyje kas 4 nuo tos pačios GFS, iki prognozės valandos, su centrais H ir L.

Šiluminė apversija: kreivės padengtos kiek oro esant 925 arba 850 hPa yra šiltesnis nei esant 2 m, + 1, + 3, + 5 ir + 8 ° C.

Pagal kontrolės linija suteikia gauti susitarimą su spaudimu ir su atvirkštine matomos srities: Pearson koreliacijos koeficientas, -1 iki + 1. Vamzdžiai dažnai gimsta anticiklonuose (oras, kuris laša kaista ir formuoja priešinga kryptimi), todėl teigiamas susitarimas su spaudimu yra normalus; bet tame pačiame anticiklone pelnas keičia daug tarp jūros, pakrantės ir kalnų. Atvirkštinio vien nepakanka: lenkimo bangos tik jei virš oro taip pat džiovintuvas [5]. Linija tarnauja pamatyti, dabar, kuris iš dviejų geriausių paaiškina spalvos srityse. Tas pats palyginimas įrašytas registre.

## 6. Potencialūs vamzdynai: dvigubas skaičiavimas

GFS lygis svyruoja nuo 200 iki 1000 m: plonas inversija plinta ir modelis nemato vamzdyno. Jei tarp dviejų lygių N sumažėjimas yra didesnis nei įprasta (40 N / km), oras yra stabilus (terminis gradientas mažesnis nei 3 K / km) ir didesnis yra sausas (savitasis drėgmės santykis mažesnis nei 0.8), programa laiko potencialų ortakį ir apskaičiuoja jį dviem būdais:

- Apskaičiavimas A: visi koncentruotas perteklius lašas per 50 m.
- Apskaičiavimas B: tas pats sumažėjimas, paskirstytas ant tipinio tos vietovės vamzdynų storio, paimto iš ITU- R P.453 [4] klimato žemėlapių (ant žemės arba virš jos).

Esant deficitui ir didžiausiam užfiksuotam bangos ilgiui, laikoma mažiausia vertė: tai apdairus įvertinimas.

ITU- R P.453 duomenys yra © ITU ir gali būti naudojami asmeniniais ir nekomerciniais tikslais cituojant šaltinį, bet negali būti perskirstyti: už tai aš nesu programos pakete. Norėdami įjungti skaičiavimą B, tiesiog atsisiųsti P.453 žemėlapius iš ITU skirtuką: programa, po to, kai rodo naudojimo sąlygas, atsisiųsti vamzdynų archyvą iš ITU svetainėje (iškeltas _ and _ surface _ duct.zip) vartotojo asmeniniam naudojimui, išgauti failus į duomenų / P453 ir įjungti skaičiavimo be perkodavimo. Arba atsisiųskite iš ITU- R tyrimo grupės 3 skaitmeninio produkto puslapį (itude, ITU- R tyrimo grupė 3, Skaitmeniniai produktai, balsas P.453, Paviršiaus duomenys ir padidėjęs ortakio duomenys) ir įdėti septynis S _ OCCUR. TXT, S _ THICK.TXT, S _ STREN. TXT, E _ OCCUR.TXT, E _ base.T Be failų programa naudoja tik skaičiavimą A ir rašo jį kortelėje ir registre. A deficito ir tipinio intensyvumo santykis P.453 yra anomalija: Kiek kartų šiandien vamzdynas yra stipresnis nei įprasta. P453

## 7. Stotys ir vietos

Nuomininkas gali turėti 4, 6, 8 arba 10 simbolių. Su 8 arba 10 simbolių padėtis yra tiksli ir dirvožemio dalis ateina iš programos "Copernicus GLO- 30: antenos aukštis yra pakankamai iš žemės. Su 4 arba 6 simbolių kvadratas yra per didelis: tai geriau rašyti antenos kvotą ant jūros; jei 0 programa išlieka su SRTM dirvožemio ties aikštės centre plius aukštis nuo žemės ir signalizuoja rezultatus. Atitinkamas numatytasis aukštis yra 3 m nuo žemės; su jūros dalis 0 naudojamas SRTM dirvožemio plius aukštis.

## 8. Žemė, difrakcijos ir troposekteris

Maršruto profilis nustatomas pagal programą "Copernicus Glo- 30" kas 30 m (daugiau nei 120 km). Lygiavertė Žemės spindulys yra k = 157 / (157 + ΔN), o ΔN - N sumažėjimas pirmaisiais kilometrais nuo GFS profilių [6, 7] maršrutu.

Laidai netoli stočių (nuo 1.3.2, autoriaus kriterijus). P.452 moka horizonto kampą tiesiškai, 0,573 dB vienam mrad: kelių dešimčių metrų atstumas iki kelių šimtų metrų atstumu nuo antenos kainuoja dešimtis dB, daug daugiau nei yra pastebėta. Dėl šios priežasties per 20 km nuo kiekvienos stoties sumažinimai gali sudaryti iki 6 dB: horizonto kampas dėl jų yra apribotas iki tolimiausių atšakų plius 10,5 mrad. Jis galioja jūsų stočiai ir korespondentui. Eilutė "Netoli reljefai" sako, kiek dBs pakilo.

Žemės šaltinis (nuo 1.3.2). Kvotos skiriamos iš programos "Copernicus" GLO- 30 [3]: 1 antras lankas, jūra yra 0 ir pakrantė yra švarus. Iki 1.3.1, programa naudojamas Terrain Tiles 'sadi' dėžutes AWS. Šalia kranto tie kvadratai sumaišoma SSRTM su mažos skiriamosios gebos jūros dugno duomenis ir skleisti salos dalį jūroje: aplink Capri sukurti vaiduoklių žiedas 80-100 m 0.5-1 km nuo kranto. 2026 m. lapkričio 21 d. Tai klaidingas horizontas, + 2,6 °, kainavo apie 26 dB troposcatter į pakrantės stotis. Kabi plokštės lieka rezervu tik tada, kai programa "Copernicus" nereaguoja. "Copernicus" rėmeliai sveria 15- 30 MB ir vieną kartą atsisiunčia; tik jūros rėmeliai neegzistuoja ir jų vertė yra 0. IC8TEM

- Laisva erdvė: Lfs = 92,44 + 20 log f (GHz) + 20 log d (km) [8].
- Differencija: Bullington metodas realiame profilyje plius ITU- R P.452- 16 § 4.2 [6] delta- Bullington sferinis sausumos korekcija, ITU- R P.526 [9] sferinis praradimas.
- Pirmoji Fresnel zona: λ spindulys · d1 · d2 / d); laisva, jei žemė palieka bent 0,6 spindulio. Kiekvienai kliūčiai rodomas [9] peilio stalo V ir J (VV) parametras..
- Į profilio dizainas tiesioginis spindulys yra žalia optiniu ir raudonos vaizdas, jei jis eina po žeme arba pagal žemės kreivumo; raudonieji taškai yra kliūtys, kad įsiveržti į 1 plotas Fresnel (violetinė punktyrinė linija). Ant jūros "raudonas taškas" aukštyje 0 m nėra kalnas: tai jūra kyla kreivumo. Eiti su pele spindulys arba taškas atrodo komiksas su paaiškinimais ir vertėmis.
- Kai prie antenos esantis reljefas uždaro kryptį (pvz., dešimčių laipsnių horizontą), difrakcijos ir troposkaterio nuostoliai sudaro šimtus dB: virš 300 dB programa rašo "> 300 dB" ir "kryptis uždaryta kaimyninės kliūties", signalas "< - 230 dB" ir S / N santykis "-60 dB". Tokiu atveju patartina pažvelgti į "Rekomenduojamą augalą" ir "Via monte".
- Troposcatter: ITU- R P.452-16 eq. 45-50% [6] laiko, su kampiniu atstumu nuo horizontų ir N0 nuo profilių.
- Be tropo: difrakcijos ir troposekterio galių suma, niekada po laisva erdve.

## 8 bis. Troposcatter: keturi modeliai ir neapibrėžtis

Šalia P.452 programa apskaičiuoja su trijų kitų modelių troposcatter, kaip ir TropoLink [16] programa, ir rodo jų skirtumą kaip netikrumą:

- NBS TN101 § 9 [25] (nuo 1.3.5), mediana: L = 30 log f − 20 log d + F (θd) − F0 + H0 + Lgp. F (θd) yra III priedo silpninimo funkcija jo analitinėse formose (Ns 301) su Ns pataisa, H0 dažnio stiprinimo funkcija § 9.2: 144 MHz, su žemomis antenomis bangų ilgiuose, atspindys nuo žemės dalies iš dalies ištrina sritį apatinėje bendro tūrio dalyje ir 3- 10 dB nuostolių druska. Ne, kad valandos GFS profiliai, efektyvūs horizontai ir aukščiai ateina iš "Copernicus" profilio, Lgp = 0.07 · e ^ (0,055 (Gt + Gr)) yra atidarymo sukabinimo nuostoliai. Tai ta pati forma, kad ITM naudoja savo interjerą, bet su šiandien, o ne klimato reaktyvumas. Jis išbando plokščias 144 MHz dalis, kurių antenos yra 20 m atstumu: per 2-4 dB nuo ITM 150-900 km atstumu iki maždaug 2 dB supaprastinta forma 9,2b TN101, kuri neturi H0.

- ITU- R P.617-5 [17] L = F + 22 log f + 35 log θ + 17 log d + Lc, su f MHz, θ mrad, d km, F = 0,18 · N0 · e ^ (− hs / 7,35) − 0,23 · ΔN ir Lc = 0,07 · e ^ (0,055 (Gt + Gr)). N0 ir ΔN paprastai paimami iš ITU klimato žemėlapių; čia mes naudojame kiekvieną valandą profiliai GFS ( N0 · e ^ (− hs / 7,35) yra reaktyvumas į žemę, ΔN sumažėjimas per pirmuosius km), taip pat troposcatter seka laiko.
- RSGB VHF / UHF Handbook Formulės, DL2FZN [18]: L = Lfs + 24,44 + 4,283 ln f + 0,06656 d. Atstumo nuolydis beveik lygus PROSPER (0,067 dB / km) nuolydžiui.

Bendras tūris (nuo 1.3.1): tai taškas, kuriame dviejų stočių spindulys yra kertamas, kiekvienas pasviręs į horizonto kampą, lygiaverčio spindulio ae = k · 6371 km žemėje. nuo TX spindulys kyla z = hTX + x · θt + x2 / (2 · ae); Atstumas x = (hRX − hTX + d ^ θr + d2 / (2 · ae) / θ, su θ = d / ae + θt + θr. Su plokščiais horizontais ir antenomis ta pačia kvota bazė yra verta d2 / (8 · ae): apie 1,3 km iki 300 km, 3,7 km iki 500 km, 9,4 km iki 800 km. Kuo didesnis tūris, tuo daugiau oras yra sausas ir suriebęs, tuo mažiau jis plinta: dėl šios priežasties daugiau nei 800- 1000 km troposcatter baigiasi. Rezultatuose yra pagrindo dalis ir padėtis, reaktyvumas, gradientas ir GFS drėgmė tame aukštyje, ir lygiavertis atstumas Bray (QST, 1961 m. lapkričio mėn.): kiekvienas papildomas ar mažesnis horizontas yra verta apie 111 km. Drausminė nuoroda: IU3QEZ pristatymas "Už horizonto VHF" (GEKO Magazine n. 69).

"Troposcatter" linija, kiti modeliai rodo du nuostolius ir netikrumą kaip pusę optimistiškiausio modelio ir pesimizmo skirtumo. Tai tik palyginimai: be tropo nuostoliai lieka, kad P.452.

## 8 p. popietinė troposcatter per reljefų (nuo 1.3.4)

IK3XTV, iš Thiene ( JN55, su Alpės 10 km), dirba vasaros popietėmis pietų Vokietijoje 400- 500 km, be matymo, su stipriais signalais, bet su greitai ir giliai eganescencija; autorius priskyrė juos "konvekcinės ląstelės sulygiuotas kaip lęšių traukinys". Pastaba yra patvirtinta; į programą priimtas paaiškinimas yra dar vienas, fiziškai tvirtesnis.

Po pietų saulė sukelia orą virš reljefų ir ribinis sluoksnis pakyla iki 2-3 km. Jo viršuje drėgnas oras žemiau atitinka sauso oro aukščiau: ten lūžio indekso turbulencija (dydis, kuris reguliuoja troposcatter) yra didžiausias. Kai šis viršus pasiekia bendrą maršruto, kuris 400- 500 km yra apie 2-3 km, troposcatter yra sustiprintas. Tai paaiškina popietinį laiką, aukštą spaudimą (tylus dangus, stiprus šildymas) ir greitą eganescenciją, būdingą difuzijai, o ne ortakiams. Konvekcinės ląstelės neveikia kaip lęšiai: gamina turbulenciją, ir tai yra nauja redakcija sluoksnio viršuje turbulencija, kuri plinta signalą.

Skaitymo prognozių pasekmės:

- Žemėje pirmoji popietė yra blogiausias metas žemiems ortakiams ir sluoksniams, bet ne troposkatams už kalnų.
- programa jau turi du duomenis, kurie tarnauja, iš ribinio sluoksnio aukštis pagal modelį ir bendro tūrio maršruto dalis (registro "Bendras tūris" rezultatai): kai jie yra arti, popietės kalnų gydyti gali eiti geriau nei normalioje atmosferoje skaičiavimo;
- nuo 1.3.4 punkto apskaičiavimo atsižvelgiama į: jei bendras tūris yra žemėje, maršrute yra bent 800 m reljefo, saulė yra virš 15 °, ribinis sluoksnis viršija 800 m, o sluoksnio performavimas (pavyzdinis dirvožemio greitis + ribinis sluoksnio aukštis) yra ne toliau kaip 500 m nuo bendro tūrio pagrindo, troposcator padidėja 3 dB; nuo 500 iki 1000 m tiesinis prieaugis sumažėja iki nulio. Linija "Troposcatter pomeridiano". Su sniego palei maršrutą (ne mažiau kaip 10 cm ant žemės pagal modelį, sniego _ gylio kintamasis Open- Meteo arba SNOD GFS iš NOMADS) armatūra netaikoma: ant sniego dirvožemis lieka šalta ir performuotas sluoksnis neauga. 3 dB vertė yra laikina ir turi būti sukalibruota realiomis sąsajomis; ji išsijungia esant oro sąlygų pataisoms.

Šaltiniai: F. Egano IK3XTV (Radio Rivista e www.qsl.net / ik3xtv, "Propagazione sulle 2 metri", "Celle conventtive", "Troposcatter over the Alps"), šaltiniai § 13.

## 9. Pagrindinis sluoksnis išilgai maršruto

Tai yra signalo skaičiavimo širdis. GFS profiliai vežami maršrutu kas 25 km. Kiekvienam slėgio sluoksniui ilgiausias to paties sluoksnio ruožas randamas iš eilės einančiuose taškuose; skaičiuojant kaip ortakio storio sluoksnius, mažiausiai 40 m [10].

- tęstinumas: nuo km iki km ir maršruto dalis;
- viršus ir pagrindas bei pokrypis (m / km);
- dirva sluoksnyje: km, kur kalnai kyla virš pagrindo;
- Padengta žemė: ant jūros konduit beveik visada, nusprendžia sausumos ruožas [10]. Jeigu žemė viršija 10% maršruto, naudojama mažiausia žemės tęstinumo ir aprėpties dalis.

Tropo prieaugis yra panaudotos frakcijos sluoksnio prieaugis. Jei daugiau nei pusė profilių yra subreaktyvūs, prieaugis yra neigiamas.

## 9 bis. Oro pataisos: stabilumas, nusėdimas, streikas

Nuo 1.3.0 profilis taip pat suteikia tris meteorologinę informaciją, kuri gali būti matoma registre (regave "stabilumas (Misme)"), Meteo linija palei rezultatų maršrutą ir, su Segni meteo langelį, žemėlapyje.

- Misme stabilumas [5, 6 skyrius]. Misme jungia lauką už horizonto su sudėtiniu parametru: M = a · (ge − 40 + b · ΔW), su a = 0,5 dB / N / km, lygiavertis gradientas ge ir šiluminis stabilumas ΔW, darbas pakelti orą 1 km. Programa apskaičiuoja ΔW = g · τ (θ - θ0) / θ0 dz tarp dirvožemio ir 1 km virš, su θ potenciali temperatūra; standartinė atmosfera yra apie 56 J / kg. Į prieaugį įtrauktas terminas yra 0,07 dB J / kg, viršijantis standartą, apribotas nuo -4 iki + 6 dB, ir skaičiuojamas tik tada, kai juostelė nefiksuojama (gradientas jau nurodytas Bean ir Dutton indekse). Kalibravimas yra laikinas: jis turi būti tikrinamas naudojant santykį (14 skirsnis).
- Subsidence and upill air [19]. Moler ir Holden, 125- 305 km ilgio maršrutuose į jūrą 1365 MHz ir 9380 MHz dažnių juostoje, nustatė didžiausią maždaug 2,5 dB signalą, kai oras sklinda, o tuo atveju apie 20 dB sustiprinimas 3 valandas nuošalumo pradžioje ir 22 dB sumažėjimas, kai oras sukasi. Pagal GFS vertikalus greitis yra 850 ir 700 hPa. Jei abiejuose lygiuose oras nukrenta daugiau kaip 1 cm / s ir galimas lūžio rodiklis K yra atbulinės eigos arba staigus kritimas (N pranešamas nuo 1000 hPa, tai rodo sausą ir šiltą orą, iškritusį iš viršaus [5]), pridedama 1,5 dB; jei oras pakyla daugiau kaip 5 cm / s, jei jis pakyla 2.
- [20] Scirocco. Karštas ir sausas oras iš Sacharos virš aušintuvas jūrų oras suteikia labai stiprus inversija iš rytinės Viduržemio jūros į Gibraltarą. S ženklas rodo, kur 850 hPa vėjo ateina nuo 100- 200 ° su bent 7 m / s ir temperatūra yra bent 16 ° C, Viduržemio jūroje. Tai tik nuoroda: pelnas nuspręsti profilio sluoksnius.

Be tinklo, kuris spėja juostą bendras prieaugis neviršija 16 dB, geltonas galas: korekcijos padaro stipresnius sluoksnius akivaizdesnius, bet ne pro juos ortakiais (raudonos ir violetinės lieka prie kanalų).

- Patvarumas (nuo 1.3.1). Didžiausios angos yra po gerų aukšto slėgio dienų (VHF / UHF DX Book; Biancomano ir Leyshon 1998, taisyklė 12 valandų; ataskaitos 29 / 12 / 2021-2 / 1 / 2022 auga nuo pirmos iki ketvirtos dienos). Kiekvienai ląstelei programa skaičiuoja ankstesnio 24 valandų skaičiaus prieaugį bent 13 dB ir proporcingai padidina iki 2 dB tik tada, kai prieaugis jau yra ne mažesnis kaip 10 dB.

Kaip skaityti ženklus žemėlapyje (Pakeisti orų ženklai; nuo 1.3.2 simbolis yra didelis dvigubai ir eina su pele virš ženklo atrodo komiksas su paaiškinimo ir vertės langelio):

| Pasirašymas | Spalva | Ką tai reiškia? | Poveikis tropiui |
|---|---|---|---|
|  ↓  | Žalieji | subsidence: oro lašai (anticiklonas), įkaista suspaudimo ir sausumo, stiprina atvirkštinę pusę ir išlaiko ją tvirtai | + 1,5 dB |
|  ↑  | raudona | įpūtimo oras (priekinis, žemo slėgio, konvekcija): maišo žemus sluoksnius ir pertraukas inversijos ir ortakiai | nepalankios, - 2 dB |
|  S  | oranžinis | Sirupas: karštas ir sausas oras iš Sacharos virš šviežio jūros oro | palankios clue, be korekcijos: nuspręsti sluoksnius |

Plotas, pilnas žalios ↓ pagal anticiklono yra klasikinis ženklas sunku atidarymo; Raudona ↑ rodyklės išilgai linijos pasakyti, kad sluoksniai yra laužimo, nors ląstelių spalva vis dar gera.

Orų pataisymų dėžutė išjungia šiuos pataisymus, kad matytųsi 1.2 modelis.

Patvirtinimas (14 punktas). Tas pats palyginimas rodo, kad pagrindinė riba yra ta, kad žemame aukštyje esanti jūra, kuri tomis dienomis atnešė ženklus tarp Kampanijos ir Ligūrijos, nėra matoma GFS lygiuose.

## 9 ter. Naujienos 1.3.2: du apskaičiavimai, lubos, aukštas slėgis, jūra, stotys, keteros

apdairus ir agresyvus vertinimas. Nuolydis iš gradiento padidėja nuo 0 dB iki normos ribos iki 10 dB iki superrefrakcijos slenksčio. 1000-850 sluoksnyje apdairi riba yra Bean ir Dutton (100 N / km); agresyvi riba yra klasikinė super- Refrakcijos riba 79 N / km (k maždaug 2; P.453 naudoja − 100 N / km per pirmuosius 100 m), kuri su k50 = 157 / (157 − ΔN) yra k maždaug 2 (k = 2,5 atitinka maždaug 94 N / km). Point-taškas skaičiavimas rodo du nuostoliai flanked; su tinklo, kad spąstai juosta sutampa. Žemėlapis naudojamas atsargiai.

Keliai ir ortakiai ieškomi tik iki 2500 m aukštyje virš žemės: kuo daugiau mėgėjų radijo antenų jų neišnaudoja. 1500 m vertės patvirtinimas prarado atskyrimo tašką ir 4% didesnių nei 16 dB jungčių, nes žiemą inversijos iš nusėdimo dažnai būna 1500-2500 m.

Aukštas slėgis pailgėjo. + 1 dB, kai mažiausiai 7 iš 9 aplinkinių ląstelių slėgis jūroje yra ne mažesnis kaip 1020 hPa ir vėjas yra silpnas žemuose sluoksniuose (10 m žemiau 5 m / s, 850 hPa žemiau 8 m / s), išskyrus didėjančius judesius.

Jūros svoris. Skaičiuojant taškais, sluoksnių prieaugis dauginamas iš 0,8 + 0,4 · μ1, kur μ1 yra ITU- R P.452 (eq. 51) faktorius ilgiausiame tęstiniame sausumos ruože: 1 atviroje jūroje, mažas su ilgais sausumos ruožais.

Stotys ir vykdomas. Kai juosta fiksuojama tik iš dalies, kiekvienos antenos dalis lyginama su jos baze ir viršumi dengiamos dalies pradžioje ir gale; pelnas dauginamas iš 0,5 + 0,5 · ^ (e ^ (− ΔhTX / 300) · e ^ (− ΔhRX / 300)), o Δh atstumas metrais nuo ortakio. Su konduit beveik visame maršrute yra § 10 sujungimas.

Sluoksnis ir keteros. Jei GFS ribinio sluoksnio viršus (didžiausio ribinio _ layer _ height modelio antžeminis greitis) viršija maršruto keteras (maksimalus plotas virš 300 m, jei didžiausias aukštis didesnis kaip 500 m), sluoksnis eina per kalnus nesulaužant: + 1,5 dB už dengiamų keterų dalį. Tai IT9GSF stebėjimas iš Sicilijos angos į Vokietiją su palankiomis Tirėnų.

Lygiai arti jūros. Su NOMADS šaltinis programa taip pat parsisiunčia paviršiaus temperatūrą (ant jūros yra SST), temperatūra ir drėgmė 80 m ir sluoksnis 30-0 hPa virš žemės. Jūros taškuose (modelio greitis mažesnis nei 5 m) šie lygiai pridedami prie profilio, pirmojo slėgio lygio, kad būtų galima matyti 100-200 m skersmens ortakį, kurio slėgio lygiai neišsprendžia. Žemėje nenaudojama: nakties ir dienos sluoksniai dar negalioja. Open- Meteo suteikia tik paviršiaus temperatūrą.

Garavimo vamzdynas. Nuo SST, nuo temperatūros ir drėgmės iki 2 m apskaičiuojamas δ aukštis garinimo konduito su neutraliu logaritminio profilio (kaip Paulus ir Jeske modelis, be stabilumo korekcijos): oras į vandens plaukus yra prisotintas SST (98% druskos), todėl

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

nuo 0 iki 40 m. 144 MHz spąstai užtektų apie 180 m (Kerr), tada 144 MHz garavimo ortakis nedaug sustiprėtų; iš 432 MHz į viršų, su žemomis antenomis jūroje, daug (11 bis punktas). Įveskite parabolinę lygtį ir eilutėje Pataisymai 1.3.2.

Dienos laikas (apsilankymas). Programa apskaičiuoja saulės aukštis kiekviename taške. Dieną nuo dienos žemėje, kai saulė viršija 15 ° ir ribinis sluoksnis viršija 800 m / s arba vėjas viršija 5 m / s, įspėja, kad žemės kanalai yra performuotas; naktį, saulė iki -3 °, vėjas iki 3 m / s ir ribinis sluoksnis iki 300 m, įspėja, kad su erene dangaus galima atlikti švitinimo, kuris patenka po saulėtekio. Tik ne jūroje. Kol kas jie pastebimi, o ne skaičiai: gruodžio mėn. patvirtinimas neturi pakankamai vasaros dienų jiems kalibruoti. Tai yra du dalykai, kuriuos autorius laiko svarbiais, nuo kasdienio stebėjimo.

Normalus profilis CRPL (alternatyva). Vietoj 40 N / km nustatytas normalus sumažėjimas atitinka eksponentinę etaloninę atmosferą CRPL: N (h) = N · e ^ (− c · h), su − ΔN pirmame km = 7,32 · e ^ (0,005577 · Ns) (Bean and Thayer 1959). Su šlapiu oru (Ns aukštas) normalus yra stabilesnis ir tarnauja stipresnis gradientas duoti pelno; aukštyje yra saldesnis. Pasirodo blogai, kol patvirtinimas sako, kad padeda.

## 9 quater. Kontrastas oro ir jūros: Adrijos (nuo 1.3.3)

Radijo bangos nepajunta jūros dugno - juntamas oro ir vandens paviršiaus kontrastas. gylis svarbus, nes jis nusprendžia, kaip greitai jūros temperatūra atitinka oro temperatūrą.

Adrijos jūra yra žema. Vidutinis gylis yra apie 250 m, tačiau šiaurinis baseinas retai viršija 100 m, o Triesto įlanka išlieka žemiau 25 m; tik į pietus nusileidžia daugiau nei 1200 m. Taip plonas vandens sluoksnis per kelias dienas šildo ir aušina, o per kelias savaites - Tirėnų. Yra trys tipiškos situacijos:

1. Pavasario ir vasaros pradžioje, palanki. Jūra vis dar šalta net Alpių ir Po upių gėlo vandens atžvilgiu; ji iš lygumos ar Balkanų pasiekia karštą ir sausą orą. Karštas oras virš šalto vandens sudaro pajūrį (nuotykių vamzdynas): yra klasikinė būklė angų tarp Italijos, Slovėnijos ir Kroatijos.
2. Ruduo ir žiema su bora, nepalanki. Šaltas oras virš šiltesnės jūros: žemas sluoksnis tampa nestabilus, presai ir ortakiai lūžta.
3. Po boro. Tik todėl, kad jūra yra žema, boras per kelias dienas ją šiek tiek išlygina. Jei tada įvesite anticikloną su karštu oru, oro ir jūros kontrastas yra stiprus ir kanalas yra stipresnis nei įprasta.

Adrijos jūra taip pat yra uždaras kanalas tarp Apeninų ir Dinari, mažiau nei 200 km pločio: jūros sluoksnis lieka uždaras, o maršrutai išilgai ašies (Puglia- Venezija- Triestas, apie 700 km) lieka visi ant jūros, geriausia būklė ilgą ortakio.

Ką programa apskaičiuoja. Ant jūros taškų, su oro pataisas ir langelį "Kontrasto aria- mare":

| Sąlygos | Korekcija |
| --- | --- |
| Galima temperatūra 925 hPa (arba 950) virš 4 K SST | Ką? |
| SST 7 K | + 2 dB |
| su oro drėgme 925 hPa (virš 80%) | - 0,5 dB už šias priemokas, |
| atvėsinta jūra 1,5 ° C ar daugiau per 72 valandas prieš, su palankiu kontrastu | Ką? |
| šiltesnis oras 2 m esant 2 ° C (maišymas) | - 1,5 dB |
| 4 ° C šiltesnė jūra | - 3 dB |

Bendra pataisa išlieka nuo -3 iki + 3 dB, o be vamzdyno prieaugis lieka po geltonu stogu. Ribos yra laikinos: standartinėje atmosferoje potenciali temperatūra iki 925 hPa viršija jūros maždaug 2-3 K, tada 4 K jau rodo atvirkštinę.

SST atmintis. Modeliai išlaikyti jūros temperatūrą beveik pastovus prognozavimo metu. Neseniai sumažėjimas yra gautas iš trijų dienų prieš analizę (Open- Meteo, tik paviršiaus temperatūra, tik jūros ląstelių): daugiausia 72 valandos prieš atėmus dabartinę vertę. Jei paslauga neatsako, atminties premija netaikoma.

Ribos. Modelių žemiau kranto ir viršutinėje Adrijos jūros yra šiurkštus SST; šaltas rašiklis po, pavyzdžiui, yra išspręsta blogai. Batiometrija neįeina į apskaičiavimą: šaltiniai visada suteikia SST, kuris jau apibendrina jūros dugno poveikį. Oro ir jūros kontrastas iš dalies sutampa Misme stabilumo pabaigoje (§ 9bis); už tai premijos yra mažos. Ribos bus eiti tarate su vietoje koridorių ir QSO per Adrijos jūros.

## 10. Visas maršrutas

Jei sluoksnis fiksuoja juostą ne mažiau kaip 80% panaudotos frakcijos ir stotys nematomos, taip pat apskaičiuojamas sistemos praradimas: laisvoji erdvė, antenų sujungimas (3 dB, jei jos yra tinklo viduje, daugiau, jei jos yra aukščiau ar žemiau), liekamasis silpnėjimas γd = 5 · 10 − 5 · ae · f ^ (1 / 3) dB / mrad ITU- R P.452 eq. 49 [6]. Jei jis yra mažesnis nei nuostolių be tropo tampa tikėtinas nuostolis.

Juosta išlieka kondute tik tuo atveju, jei jos bangos ilgis neviršija λmax = 2 ^ 2 · (2 / 3) · ^ (ΔM · 10 − 6) · d (Kerr, eq. 17 [11]). Dėl to paties sluoksnio gali gaudyti 23 cm ir 70 cm, bet ne 2 m. Tačiau riba nėra neto: Kerr įspėja, kad net bangos kelis kartus ilgiau kenčia nuo tinklo daug, ir Hepburn ir VHF / UHF DX Knyga kalbėti "sferored" kraštai ir "atleisti" ortakiai. Nuo 1.3.1 programa naudoja minkštą jungtį: jei λmax yra tarp λmax ir 3 · λmax, prieaugis palaipsniui pereina nuo 15 dB (λ = 3 · λmax) iki viso konduit prieaugio (λ = λmax), o koeficientas s = (λmax / λ − 1 / 3) / (2 / 3). Tokiu atveju latako spalvos yra leidžiamos. Maršrute esantis žurnalas ir Meteo linija nurodo taškus su minkštu ortakiu.

## 10 bis. Via Monte

Kai korespondentas yra uždarytas už horizonto, programa siekia geriausia kryptimi nustatyti antenos: apšviestas nuolydis plinta signalą kaip radaro taikinys. Jis daro horizonto apskritimą kas 5 (išskyrus ± 15 ° aplink korespondentą) ir kiekviena kryptimi trunka apšviestą horizonto tašką, virš 100 m ir 1-60 km. Greitas įvertis (laisva erdvė, bitų konversija ir 10 dB kiekvienam horizonto lygiui nuo difuzoriaus iki korespondento) pasirenka 4 perspektyviausius, ne mažiau kaip 15 ° vienas nuo kito; kiekvienam:

- 1, stotis → reljefas, laisvoje erdvėje;
- apdoroja 2, reljefo → korespondentas, apskaičiuojamas taip, tarsi reljefo perduodama iš 10 m virš viršaus, su ta pačia prognoze (be parabolinės lygties);
- konversija į dvistatinę radarų lygtį, kai skirtumas tarp R1 spindulio (ne daugiau kaip 20 km2) yra:

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

"Rekomenduojamo taško" linija nurodo geriausio maršruto azimuto, jei jis kainuoja mažiau nei tiesioginis. 144 MHz, 20 km ir 15 ° gylyje, kalnų kelias kainuoja apie 45 dB daugiau nei vien tik 2 maršrutas: tai vyksta tik tada, kai tiesiogiai uždaroma kaimyninių kliūčių. Apskaičiavimas yra bendrasis (± 10 dB), ypač σ0 atveju.


Be nuostolių kalnų (variantas, nuo 1.3.2). Su "Via monte senza perdite up al monte" langelį, kritimas tarp stoties ir difrakcijos taško yra laikomas nulis: yra kaip vedimas stoties į viršų (antena 10 m virš taško, ta pati galia, antena ir kabelis). Tada horizonto turas pasirenka tašką su labiausiai laisvo horizonto link korespondento (pariteto, aukščiausias) ir rezultatas rodo maršrutą nuo taško iki korespondento, su signalu ir S / N, šalia viso difuzijos. Tai optimistinė riba: sako, jei kryptis atsidaro, kai kalnas kelia signalą gerai, arba kur eiti nešiojamas. Kiekvienas taškas yra pateiktas su ten- simbolių.

## 10 ter. Įėjimas į tolimą jungtinę

Jei tinklas, kuris spėja juosta prasideda toli, už stoties horizontą, signalas turi atvykti. Programa atneša signalą į atspirties tašką, viduryje tinklo, difrakcijos: palei Copernicus profilį GLO- 30 ieško krašto su aukščiausiu Fresnel parametru (ITU- R P.526), su lygiavertės žemės patinimas,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

daugiau silpninimo dėl viršaus kreivio (suapvalinta kliūtis, P.526- 16 § 4.2), kai R spindulys nuo "Copernicus" GLO- 30 dirvožemio iki ± 1 km nuo krašto,

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

(m · n > 4 yra rekomendacijos 34b forma). Barsis (NBS, 1963) matuojamas trijuose ilguose maršrutuose už kalno (Kolorado, Aliaska, Europa) vidutiniai nuostoliai 10-20 dB didesnis nei idealus kraštas Fresnel- Kirchhoff: apvalinimo paaiškina daug (Barsis ir Kirby, 1961: Pikes Lead su maždaug 11,5 km spinduliu). Rezultatas lyginamas su difrakcijos ant lygaus sferinio dirvožemio; tai blogiausia. Tada signalas sklinda vamzdyne: laisva erdvė visame maršrute, liekamasis silpnėjimas (γd) P.452 taške vamzdyno dalyje ir korespondento sujungimas (3 dB viduje, 1 dB kas 10 m išorėje). Jei šis nuostolis yra mažesnis už tikėtiną nuostolį, jis tampa mechanizmu ("elgesys (difrakcijos būdu)"). Tai autoriaus prielaida dėl Italijos geografijos: su šalia esanti kliūtis gerokai žemiau linijos, v yra neigiamas ir įėjimo išlaidos mažai; su nugaros priekinės išlaidos 15- 30 dB.

## 10 kvartalų. ITM (Longley-Rice) ir išėjimas iš ortakio

Nuo 1.3.2 programa yra Nereguliarus Terrain modelis 1.2.2 NTIA / ITS (Longley ir ryžiai, ESSA ERL 79- ITS 67, 1968; Hufford, Longley ir Kishey, NTIA 82-100, 1982), išverstas į C # pagal oficialų JAV vyriausybės kodą, kuris yra laisvas nuo teisių. Vertimas suteikia tuos pačius numerius kaip ir originalus: automatinis kompiliavimas kiekvieną kartą palygina jį su NTIA C + + kodu penkiais bandymų atvejais (žemiau 0,01 dB).

ITM apskaičiuoja normalios atmosferos praradimą realiame profilyje Copernicus GLO- 30: matyti atspindys nuo žemės, difrakcijos (spigolis ir sferinės žemės pasverti dirvožemio šiurkštumo Δh, tai yra tarp 10-90 ° procentilės kvotų aplink mažiausio kvadratų tiesia) ir troposcatter skirtumas. Žemės reaktyvumas sumažinamas iki vidutinės maršruto dalies, $N_s = N_0\,e^{-h/9460}$, ir nulemia atitinkamos žemės išlinkimą,

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

Programa naudoja radijo klimatą pasirinkto maršruto, kaip § 10 sex (iki 1.3.4 visada vidutinio jūros, su N0 320 ant žemės ir 350 ant jūros), su N0 iš vidutinio reaktyvumo vietoje (dirvožemio εr 15, Bendrijoje 0,005 S / m; jūros εr 81, 5 S / m), horizontalus poliarizacija ir kintamumas per tam tikrą laiką be vietos (profilis yra žinomas). Tai rodo, kad nuostoliai viršijo 50% laiko ir vieną, kad jūs turite 10% ir 1% geriausio laiko. ITM nemodeliuoja kanalų: jo kintamumas laikui bėgant yra statistinis, priklausomai nuo klimato. Jis naudojamas kaip antras bazinio lygio apskaičiavimas šalia P.452; skirtumas tarp šių dviejų suteikia netikrumo idėją.

Antrasis naudojimas yra galutinis ruožas po vamzdyno, kuris baigiasi prieš atitinkamą. Išvykimo taške virtuali antena įrengiama įpusėjus vamzdynui, o ITM apskaičiuoja iki atitinkamo tarpo, kaip laisvos erdvės perviršis 50% laiko. Bendras nuostolis yra

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,	heta_{condotto} + A_{ITM}(d_{uscita} 	o RX)
```

čia A _ TX yra antenos sujungimas (3 dB viduje, 1 dB kas 10 m išorėje), jei kanalas prasideda arti, priešingu atveju 10 ter difrakcija. Tai yra riba, kad autorius davė save: tęstinumo sluoksnio plius, daugiausia, likutinis silpninimas. Jei jis yra mažesnis už tikėtiną nuostolį, jis tampa mechanizmu ("elgesys (išėjimas ir galutinis išėjimas ITM)").

## 10 quinquies. Sustiprinimas: salų, sąsiaurių ir jūros nestabilumas

Pavyzdys iš Neapolio įlankos (Anacapri, JN70CN) į vakarinę Viduržemio jūros dalį rodo, kur maršrutai per salas:

| Paskirtis | Azimutas | Kur jis kerta salas | Kliūtys |
| --- | --- | --- | --- |
| Barselona |  279°  | į šiaurę nuo Sardinijos (Gallura) iki 41,05 ° N, nuo 420 iki 490 km | apie 30 km į pietus nuo Bocche di Bonifacio; apie 20 km nuo linijos |
| Bocche di Bonifacio |  282°  | beveik 41,3 ° šiaurės platumos | apie 11 km |
| Marselis |  296°  | Centrinė Korsika 42,2 ° šiaurės platumos | kalnai iki 2700 m: iki 2 km |
| Valensija, Balearų salos |  260–265°  | Centrinė Sardinija | Vidiniai atlaidai |

Barselonoje pakanka nustatyti 282 °, o ne 279 ° tašką, nes ašis eina iš burnos: 3, viduje įprasto jagi spindulių.

Ar burnos neaiškios? Žemiems ortakiams, mažesniems nei keli šimtai metrų, taip: salos užtvindytos, o virš žemės, dieną kaitinimas nutrūksta. Pati straita yra pakankamai plati: apie 11 km, palyginti su pirmuoju Fresnel plotu, mažesniu nei vienas kilometras, per 1000 km maršruto viduryje 144 MHz. Problema yra oras. Mouths yra vėjo įdomus: Apatinis sluoksnis paspartėja ir apatinis sluoksnis pašalinamas. Dažnai ištrauka yra atvira kaip geometrija, bet sutrikdyta kaip atmosfera. Su pastovus anticiklonas ir mažai vėjo gali būti gera ištrauka.

Žemės ir jūros skirtumai. Jie skaičiuoja. Dėl Tirėnų pakrantėje suformuotas konduit nutraukiamas: signalas ateina iš tinklo (po išėjimo, § 10 quater) ir turi grįžti į kitą konduit ant Balearų jūros, mokėti vėl sukabinimo. Virš 1500 m aukštyje esantys vamzdynai kerta Gallura nesuvokdami jos; centrinei Korsikai šis reikalavimas netaikomas.

Praktikoje: jei maršrutas kerta kalnuotą salą, pabandykite tam tikru laipsniu koreguoti link laisvosios jūros ruožo, bet nesitiki keliu su stipriu vėju siauroje.

## 10 sex. TN101 radijo klimatas ir laiko kintamumas (nuo 1.3.5)

TN101 § 10 [25] apibūdina, kaip maršruto praradimas už horizonto skiriasi per ilgą laikotarpį aplink medianą, su skirtingomis kreivėmis pagal klimatą: Y (q) funkcija rodo, kiek dB nuostoliai sumažėja per Q% geriausio laiko. ITM yra tos kreivės septynių klimatų. Klimatas labai svarbus: jūroje vidutinio jūros klimato, 144 MHz, per 10% laiko, kurį uždirbate daug daugiau dB, nei žemėje žemyninio klimato, nes ortakiai yra dažnesni.

Programa nebetaiso klimato: Jis tai pasirenka kiekvienam gydymui iš vidutinio taško, todėl jis veikia visame pasaulyje, o ne tik Europoje. Parsisiųsti iš Open- Meteo [26] trejų metų dienos vertes ERA5 reanalysis (temperatūra, drėgmė ir dirvožemio slėgis, lietus) už 0,5 ° vidutinio taško ląstelių, vieną kartą; vertės lieka duomenų / CLIMA. Dirvožemio atkūrimas

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

kiekvieną dieną, tada metinis vidurkis ir nuokrypis tarp mėnesio vidurkių, vidutinės drėgmės ir metinio lietaus. Šios taisyklės atitinka TN101 10.2 punkto klimatų aprašymus; ribos tarp vieno klimato ir kito yra autoriaus kriterijus:

- daugiau kaip 60 ° platumos: vidutinio dydžio kontinentinis ( TN101 neturi polinio klimato kreivių);
- mažesnis nei 250 mm lietus per metus, o vidutinis drėgnis mažesnis nei 50%: dykuma (N tipinis 280);
- ne toliau kaip 20 ° nuo pusiaujo, N - ne mažiau kaip 350, o nuokrypis iki 30: pusiaujo (N - tipinis 360);
- 32 ° ir ne mažesnis kaip 60 ° nuokrypis: subtropinis kontinentinis, tai yra monsoonai (tipinis nuokrypis 60-100);
- 10-32 °, Ne mažiau kaip 340 vienetų: povandeninis laivas (tipinis - 370 vienetų);
- abu jūroje esantys horizontai, virš 20 °: jūros dugnas jūroje (tai TN101 apibrėžimas);
- Ekskursija iki 30 prie jūros (jūra maršrute arba vidutinė drėgmė ne mažesnė kaip 75%): jūros dugnu;
- kitaip vidutinio žemyno (tipiškas ekskursija 20-40).

N0 ITM yra vidutinis sumažintas iki jūros lygio, N0 = Ns · e ^ (h / 9460). Be tinklo jūs naudojate rezervinę taisyklę su platuma ir jūros frakcija, ir linija sako, kad tai.

Bandymai su duomenimis ERA5 2023- 2025: Capri Ns 339 su ekskursija 45 (kontinentinis grūdintas ant žemės, jūros temperate ant jūros, kai abu horizontai yra ant jūros); Londonas 327 ir 16 Vienna 319 ir 24 Abidžanas 381 ir 11 (pusiaujo); Majamis 365 ir 36 (subtropinis vyras); Delis 337 ir 82 (subtropinis kontinentinis); Riad 278 ir 37, 90 mm lietaus (dykuma). Čilėje: Antofagasta 341 ir 19 (subtropinis vyras: Atakamos pakrantė, beveik nustatyta žema Ramiojo vandenyno reverse), Calama 232 ant žemės, 2257 m, 14 mm lietaus (dykuma), Valparaíso 331 ir 11 (temperatūros siūlės), Punta Arenas 313 ir 5 (temperatūros siūlės).

Linija "Radijo klimatas ( TN101)" rodo klimatą, priežastis, N su puse ekskursija ir Y: kiek dBs uždirbti 10% ir 1% laiko ir kiek prarasti 90%. Tai yra ilgalaikiai statistiniai duomenys ir lieka atskirti nuo prognozės: laukiami nuostoliai po šiandienos laiko su GFS profilius, ir suma du kartus būtų skaičiuojami tas pats nenormalus dauginimas.

## 11. Palyginimas su ITU- R P.452

P.452 linija taiko ITU metodą, skirtą ortakiams ir apmąstymams ant sluoksnių (P.452- 16 § 4.5 [6]), kai ΔN mediana yra GFS, o ne klimatologinė, ir p = 20% kaip kalibravimo konstanta, kaip CSIRO [10] tyrime. Tai tik palyginimas: tikėtinas nuostolis išlieka 9 paragrafe.

## 11 bis. Parabolinė lygtis (nuo 1.3.1)

Parabolinė lyginimo linija yra ketvirtas skaičiavimas, tik palyginimas, pagamintas kaip profesinės programos, skirtos ortakiams (PETOL; bangų sklidimo sistema M. Lytaev, MIT licencija): vietoj apskaičiuoti pelną iš sluoksnių, tai avansas palei maršrutą su split- žingsnis metodas Furier per M modifikuotą refrakcijos profilį GFS, paimtas iš prognozės taško arčiau kiekvieno atstumo. GFS lygiai pridėti apskaičiuotus šuoliai potencialių kanalų (apskaičiuojant A, 50 m per pusę sluoksnis). Dirvožemis "Copernicus GLO- 30" įjungia žingsnius (po žeme laukas yra nulinis), dirvožemis yra laidininkas, o antenos spindulys yra Gaussiškas su numatoma anga nuo pelno. Skaičiavimas mato konduitai, tie, kurie praranda, difrakcijos ir antenų jungtis; Jis nemato troposcatter, kuris pridedamas atskirai. Kitas yra "profilis čekis": tas pats su standartine atmosfera mažiau nei su tikimasi profilis.

Nuo 1.3.2:

- daugiklis yra plačiu kampu, su tikslia laisvosios erdvės faze:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- jūros dugnas ir pakrantės lyguma nulinio aukščio atspindi su Fresnel koeficientą horizontalioje poliarizacijoje, εc = εr − j · 60 · λ · ů (jūros εr = 81 ir ← = 5 S / m; šalis 13 ir 0,006 S / m), aiškiu maršruto kampu:

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- jūros šiurkštumas sumažina nuoseklų atspindį pagal Miller ir Brown, su efektyviu aukščio bangų nuo vėjo iki 10 m (σh = 0,0051 · U2):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- jūroje M profilis apima garavimo ortakį (9 ter pastraipa) su neutraliu logaritminiu profiliu:

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

Jis bandomas su prototipu (10 m antena ant jūros, įgauna standartinę atmosferą 100-300 km): su δ = 10-30 m garinimo laidas užtikrina 1-3-144 MHz, 8-37 dB-432 MHz ir 21-66 dB-1296 MHz. 144 MHz horizontalioje poliarizacijoje jūra atspindi beveik kaip metalas (maždaug 124; η 124; daugiau nei 0,99 iki kelių dešimtųjų laipsnio), o šiurkštumas yra nedidelis: naujasis dugnas keičia keletą dešimtųjų dB; jis daugiau nei 432 MHz.

Ribos: virš žemės išlieka GFS lygių vertikalusis profilis, todėl ploni ortakiai lieka nematomi; žemė iki laiptelių yra apeinama; spindulis lieka 6.

## 12. antenos, signalas ir triukšmas

- Yagi DL6WU: padidėjimas 9, 5 + 7, 8 · log10 (bum / λ) dBd (2,35 dB dvigubam bumui, DL6WU VHF ryšio priemonėms 3 / 1982), pridėjus 2,15 dB [12] dBi.
- EIRP = galia − kabelis + TX padidėjimas.
- Signalas gautas = EIRP - laukiamas nuostolis + pelnas RX - kabelis RX. S- metras IARU: S9 = − 93 dBm, 6 dB vienam vienetui.
- Išorinis triukšmas pagal ITU- R P.372 [13]: galaktinis (Fa = 52-23 log f) labiau dirbtinis pagal aplinką (miestas, gyvenamasis, kaimo, tylus kaimo) arba ranka parašyta antenos temperatūra.
- Tsys = Ta + Tpre + [290 (Lc − 1) + 290 (Fr − 1) · Lc] / Gpre iki antenos terminalų (Friis formulė). Nuo 1.3.5 imtuvo stotyje visada yra antenos stiprintuvas, fiksuotas parametras: triukšmo skaičius 0,8 dB (Tpre = 59 K) ir 20 dB. Už jų yra kabelis (Lc nuostoliai) ir radijas su pagrindiniu triukšmo numeriu Fr 6 dB. Su 1 dB kabeliu kabelis ir radijo įvestis nukrenta nuo maždaug 1160 K iki 12 K: triukšmas nulemia dangų, aplinką ir preampą. 20 dB prieaugis yra tipinė programos pasirinkta vertė.
- S / N SSB, CW ir FT8 juostų kiekvienu būdu, palyginti su nustatyta riba.
- Laikotarpio tikimybė (nuo 1.3.2): per valandą pakilimas yra normalus S / N pasiskirstymas apie numatomą vidutinę valandinę vertę. Bray (QST, 1961 m. lapkričio mėn., p. 39, Norton 1959 m. kreivėse) suteikia 13,5 dB amplitudę maždaug už 100 mylių, t. y. maždaug 7 dB žemiau ir 7 viršija vidurkį, kuris beveik yra žemiau 100 mylių (nuo 3,5 dB iki 50 mylių); "kietas" kanalas iš biudžeto užima 7 dB. Paimtas ± 7 dB, kaip ± 2Ω: Δ = 3,4 dB per 160 km, proporcingas toliau nurodytam atstumui. Tikimybė, kad laikotarpis viršija kelio ribą yra

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  su normalia pasiskirstymo funkcija: marža 0 dB = 50%, + 3,4 dB = 84%, + 7 dB ("kietoji" Bray marža) = 98%. Ji neapima didesnio modelio neapibrėžtumo.
- Skaitmeniniai būdai: S / N 2500 Hz, palyginti su ribinėmis vertėmis, išmatuotomis esant 50% dekodavimui, be išankstinės informacijos (AP). FT8 -21 dB ir FT4 -17,5 dB baltame triukšme; kai mechanizmas yra troposcter naudoti slenksčius su evanescence "sutrikdyta platuma terpė", FT8 -18,6 dB ir FT4 -15,2 dB [21]. Q65 -15C − 22,2 dB, Q65 -30B - 24,8 dB, Q65 -60A - 27,6 dB Lėktuvuose Doppler gali viršyti Q65 -60A (1,7 Hz) tonus: yra geriau 30B arba 15C. JT65A - 25 dB [22]. Kodavimo AP (QSO pavadinimo arba pabaigos) suteikia apie 2 dB papildomą FT8 ir 1,5-2,6 dB Q65; jis neįeina į ribą, nes jis negalioja visiems FT8 skaitytiems "-24 dB" reikšmėms yra WSJT-X santykių arba AP dekodavimo riba, o ne 50% riba. [24]

## 13. Atsako ir duomenų laikas

Kiekvienas skaičiavimas matuoja savo laiką: GFS profiliai (tinklas, analizė, privalomas pertraukas 20 s tarp partijų Open- Meteo riba), Copernicus GLO- 30 reljefas (su atsisiuntimo plokštės, 15- 30 MB kiekvienas, tik pirmą kartą), skaičiavimas. Atsakymo laiko linija juos apibendrina; registre eilutės prasideda TEMPI. Atnaujindamas tinklą, registras taip pat praneša apie naudotas GFS lenktynes, kai jos tapo prieinamos ir kiek metų yra duomenys.

Nuo 1.3.2 taip pat galite pasirinkti ICON- 2I, Agentūros modelis ItaliaMeteo ir Arpae Italijoje apie 2,2 km, per Open- Meteo (modelis ICON _ meteo _ arpae _ icon _ 2i, dvi rasės per dieną, 3 dienas). Jis apima tik Italiją ir aplinkines jūras ir turi mažiau žemą GFS lygį (1000, 925, 850, 700, 500 hPa): už vamzdynai aukštyje nėra geriau nei GFS, bet bauda tinklas apibūdina gerai pakrantes, salas ir jūrą 2 m, tai yra garavimo ortakio. Kintamieji, kurių modelis neturi (strate riba, paviršiaus temperatūra...) pašalinti save iš prašymo. Nuo 1.3.2 automatinis šaltinis naudoja tik NOMADS; Open- Meteo tarnauja, jei NOMADS nereaguoja ir istoriniais atvejais. Su "Atnaujinti savo" langelį (apšviesta pagal nutylėjimą) prognozė bus atsisiųsti be paspaudimų: atidarymo, kiekvieno naujo GFS lenktynės NOMADS (apie 5 valandos po lenktynių metu) ir kai matomas plotas yra už daugiau nei ketvirtį iš tinklelio; ne daugiau kaip bandymas kas 15 minučių, niekada su istoriniu atveju.

Prašymai Open- Meteo turi ribas per minutę, per valandą ir per dieną kiekvieno interneto adresą. Atsisiųsti profiliai yra išsaugomi diske (Dati / cache-gfs) kiekvienam insultui ir modeliui: su ta pačia rase, kuri keičiasi kas 6 valandas, taškai jau atsisiųsta neprašo dar kartą. Su automatiniu šaltiniu jūs pradedate nuo NOMADS ir Open- Meteo tik tarnauti trūkstamų taškų; tik Open- Meteo, metu arba kasdien sustoja iš karto ir sako, kada bandyti dar kartą. Istoriniai atvejai neišbraukti.

## 14. Modelio patvirtinimas

Prognozė turėtų būti lyginama su realiomis jungtimis. Skaičiavimo žurnalas saugo kiekvieną numerį; programos archyvas saugo skelbimus ir dekodacijas. 432 ir 1296 MHz dažnių juostoje, kur taškų yra nedaug, jiems reikia operatorių ataskaitų: kas juos sujungė, kokiu laiku, kokiu signalu. Palyginkite daug valandų laukiamas pelnas su išmatuotu signalu sako, jei ribos turi būti perkeltas.

## Šaltiniai

1. NOAA / NCEP, Global Forecast System (GFS), Global modely at 0.25 °, four rasių per dieną.
2. Open- Meteo, prognozavimo API (open- meteo.com), gfs _ swift modelis, kintamieji slėgio lygių ir lenktynių metaduomenys.
3. "Copernicus" GLO- 30 DEM (ESA; WorldDEM © DLR e.V. 2010- 2014 e © Airbus Defence and Space GmbH 2014- 2018, tiekiama kaip programos "Copernicus" dalis Europos Sąjungos ir EKA), 1-asis lankas, GeoTIFF rėmeliai atvirame "Amazon" interneto paslaugų registre. Rezervas: NASA / USGS Copernicus GLO-30 Terrain Tiles "skadi" of AWS.
4. ITU-R P.453, Radijo refrakcijos indeksas: jo formulė ir refrakcijos data, su skaitmeninių kanalų žemėlapiai (Tyrimo grupė 3, © ITU, asmeninio ir nekomercinio naudojimo).
5. B. R. Bean, E. J. Dutton, Radio Meteorologija, NBS Monograph 92, 1966: Lentelė 6.5 ir § 4.4.
6. ITU- R P.452-16, prognozavimo procedūros, skirtos įvertinti trukdžius tarp stočių Žemės paviršiuje, kai dažniai viršija 0,1 GHz: § 4.2 (difrakcijos), eq. 45 (troposcatter), § 4.5 ir eq. 49 (atlikta).
7. ITU- R P.834, Poveikis troposferos refrakcijos radijo bangų sklidimo.
8. ITU- R P.525, Laisvos erdvės silpninimo apskaičiavimas.
9. ITU- R P.526, Dauginimas difrakcijos būdu: eq. 31 (punčeonas) ir § 3 (sferinė žemė).
10. B. Indermuehle, H. Suzuki, Tropospheric Ducting Prediction based on GFS Model Data at Inyarrimanha Ilgari Bundara, the CSIRO Murchison Radioastronomy Observatory, Publications of the astronomy Society of Australia, 2026 (arXiv 2608.12861).
11. D. E. Kerr, Propagation of Trumpo radijo bangų, MIT Radiation Laboratory Series vol. 13, 1951: eq. 17 e 18.
12. G. Hoch DL6WU, "Extremely Long Yagi Antennas", VHF Communications 3 / 1982, pp. 130 et seq., fig. 1 (matuojama 432 ir 1296 MHz pagal bumo ilgį).
13. ITU- R P.372, Radijo triukšmas: galaktinis ir dirbtinis triukšmas aplinkai.
14. NOAA / NCEP, NOMADS, GRIB filtras GFS 0,25 ° laiko (nomads.ncep.noa.gov); WMO G RIB2 formatas, sudėtingas pakavimas su erdviniais skirtumais.
15. Open- Meteo, Istorinė prognozė API (modelio prognozė archyvas nuo apie 2021).
16. LukiBox, TropoLink, troposcatter maršrutų projektavimas su P.617, NBS TN101 ir ITM flanked (GitHub, Apache 2.0 licencija).
17. ITU- R P.617-5, Prognozės metodai ir duomenys, reikalingi horizonto radijo relės sistemų projektavimui, 2019.
18. RSGB, VHF / UHF Handbook; DL2FZN implementation (andi- f / troposcatter on GitHub, 2013).
19. W. F. Moler, D. B. Holden, "Troposferos sklaidos ir atmosferos apytakos", Journal of Research of the NBS 64D, 1960, doi 10.6028 / jres.064d.014: signalas už horizonto (1365 ir 9380 MHz, ant jūros) ir vertikalus judėjimas oro.
20. ARRL Handbook 2022 (99a ed.), § 19.4.3-19.4.4: oro sąlygų ir troposteriškas dauginimas.
21. S. Franke, B. Somerville, J. Taylor, The FT4 and FT8 Communication Protocol, QEX July / August 2020.
22. W3SZ, lėtų skaitmeninių režimų ribos ( JT65, JT9, QRA64) ir režimo pasirinkimas pagal sklidimą, 2019.
23. QST "Pasaulis virš 50 MHz", 2022 m. lapkričio mėn.: 2022 m. rugpjūčio 14-15 d. atidarymas tarp Manitobos ir Kanzaso / Misūrio.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Quick- Start Guide to Q65, 3 April 2021 (wsjt.sourceforge.io).
25. P. L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis, Perdavimo nuostolių prognozės Tropospheric Communication Circuits, NBS Technical Notice 101, journal 1967: § 9 (troposcatter, function F (θd), H0), § 10 and Annex III (radio campates and variability in time).
26. Open- Meteo, Istorinis Orų API (ECMWF ERA5 apžvalga nuo 1940; Copernicus klimato kaitos paslauga, CC BY 4.0 licencija).
