LIVETROPOVH · tehniline dokument 1
# Programmi toimimine

> Automaatne tõlge itaalia keelest: kahtluse korral on see väärt Itaalia juhendit.

Mida programm teeb, kust see andmed võtab ja kuidas see muudab need kaardil ja loendis nähtavaks. 9. novembri 2026. aasta versioon, mis on seotud projektiga C# (.NET 10 + Avalonia).

## Kokkuvõttes

- Eesmärk: tropoprognoos (Hepburn [3], F5LEN [4], isobare GFS [18]) ja tegelik ribade jälgimine samas aknas vastavalt LA0BY [1] kontrollnimekirjale.
- Andmete kogumine: kaks DX klastrit, Reverse Beacon Network [9], dm4x cluster ja PSKReporter [5] MQTT vood, UDP dekodeerimine WSJT-X-ist, JTDX-ist ja MSHV [8] -st.
- Punkt: ribasegmendid, negatiivne filter, õhu hajumine kriipskollasena, asukoht lokaatorite või C ALL3.TXT [6] [7].
- Kes kuulab: tumedad roosad ringid roosade joontega, mis on alati jaamade poole nähtavad, tundsid [5].
- Täpikaart: vahemaa, asimuut, tropoindeks rajal ja troposkatterkadu G3YGF [2] meetodil.
- Keeled Hepburn ja F5LEN: [3] pilt ja kalibreerimispink [4].
- NTP kontroll 10 serveris iga 3 minuti järel [10] [11].
- Kahtlustatavad teated: EME Q65/JT65 -s on tunnustatud Kuu positsiooniga, kärbitud lokaatorid, skimmerid, karantiin ja kuulajad, kusjuures [20] on õppinud reegleid.
- Vette tagasiviskamise kriteeriumid ja värvide tähendus, mis on kokku võetud kahes tabelis (punktid 11 ter ja 11 quater).
- Teemad, 36 [17] keeled, ekspordi dokumendid Wordi redigeeritavate valemitega [16], kaasaskantav programm ja mitmeplatvormiline [12] [14] [15]. [13]

## 1. Eesmärk

Programm koondab ühte aknasse kaks asja, mis tavaliselt vaatavad erinevaid saite: tropoferi levimise ennustamine (Hepburni ja F5LEN Tropo indeks, isobare) ja selle jälgimine, mis tegelikult juhtub bändides (DX klastri punktid, Reverse Beacon Network ja PSKReporter, pluss kuulavate jaamade nimekiri). Võrdlusriba on 144 MHz; täpid kogunevad 50 MHz-lt üles.

Seade järgib LA0BY poolt ekstreemsete troposündmuste jaoks välja pakutud kontrollnimekirja: eelmistel päevadel vaatate Hepburni ja F5LEN prognoose ja temperatuuriprofiile kõrgusel, samal ajal kui sündmus on DX klastris ja kohapeal kaardistab tõendeid selle kohta, et avamine algab [1]. LA0BY täheldab ka, et ainsatest Hepburn/ F5LEN kaartidest ei piisa kõige pikemate ühenduste ennustamiseks. [1]

## 2. Aken ja vahekaardid

Aken on üks ja ei jagune mobiilseteks akendeks. Ülalt alla:

- Üldseadistuste rida: Helista ja Locator vasakul; paremal UTC kella (nt 08/11/2026 20:33:38 UTC, merevaigu tekst), millele järgneb NTP:OK või NTP:FAILED, teemamenüü ja keele oma.
- Viies rühmas: Kaart ja koht; Prognoosid (Propria β, programmi poolt arvutatud ja Forecast Manualis kirjeldatud prognoos, seejärel Hepburn ja F5LEN); GRAVES; Andmed ja allikad (DX cluster, PSKReporter, Reverse Beacon, UDP, Appointments ja kahtlased aruanded); Juhendid (Program Operation, Map Building, Kuidas kujundada tropo, Käsitsi prognoos, Näpunäited operaatorile, otsingukastiga, milles tekst ilmub). Tala all näitab riba rühma alamkeeli; iga rühm mäletab viimast kasutatud keelt.
- Kaardi sisu ja allosas olekuriba koos allikate oleku, nimede andmebaasi ja viimase uuendamise ajaga.

Ülemise rea seadistused kehtivad kõigile kaartidele: valitud teema värvib ka andmete kogumist ja dokumentide tausta; Dokumendi leheküljed jäävad valgeks kui leht, nii et nad trükivad hästi.

## 3. Helista ja üürileandja

Maidenheadi üürileandjal võib olla 4 või 6 märki (nt JN70 või JN70CN). Programm teisendab selle ruudu keskpunkti koordinaatideks: väli 20° × 10°, ruut 2° × 1st, alamkvadraat 5′ × 2,5′. Kui tekst ei ole korrektne lokaator, muutub kasti serv punaseks ja jaam kaardil ei ilmu. Lokaator toimib ka asimuudi projektsiooni keskusena ning reklaamilaual kauguste ja asimuudi lähtepunktina.

Kõne on nimi, mida kasutatakse klastri sisselogimiseks. Esimesel käivitamisel, kuni kehtib nimetamine, klastrid ei ühendu: aken küsib nime (ja valikuliselt üürileandja); kinnitatud, nimi kopeeritakse kasutajanimena iga klastri väljal, kus see on tühi ja klastrid lahkuvad. "Hiljem" jääb ikka ja iga klastri nupu Connect jaoks on vaja uuesti nominatsiooni. MQTT, UDP ja NTP ei kasuta sisselogimist ja lahkuvad niikuinii.

## 4. Andmete kogumine: allikad

Igal allikal on kaardil Andmete kogumine oma osa, kus on ühendatud / ühendatud, käivitamisel tunnustatud ja ühendatud kastid, staatus, arvestid ja vastuvõetud ridade register. Filtrist eemaldatud või äravisatud ridu registris ei ole: loe ainult lõigu mõõtjad. Sektsioonid on külgnevad kahega.

| Allikas | Ühendus | Mis toob |
| 1. DX klastri valik | on0dxk.dyndns.org:8000 käsukomplekt/filter dxbm/pass 6 | Amatöörraadiote koht, ka SSB ja CW. ON0DXK -l on VHF filtreeritud ja lubatud märgitud käsuga (poi pass 4, 2...). |
| 2. Teine allikas | dxmaps.com:7300 (DXMAPS, EA6VQ) ja s50clx.si:41112 ( S50CLX, Sloveenia) | DXMAPS kogub VHF-punkte ja annab ka RBN-i koorimiskohti (seti/skimmer kontroll, juba seatud); S50CLX on DX Spider klaster. Sisselogimine oma nimega; redigeeritavad aadressid. |
| 3. Tagasipööratud majakavõrk | telnet.reversebeacon.net 7000 CW/RTTY · 7001 FT8 | Automaatne koorimiskoht. Sisselogimine ainult nimega [9]. |
| 4. Klaster PSKReporteriga | dm4x.ddns.net:8500 | PSKReporteri digitaalsed kohad klastrivormingus. |
| 5 PSKReporter MQTT | mqtt.pskreporter.info 1883 · 1884 TLSiga | Iga reaalajas vastuvõtu aruanne: kes edastab, kes kuulab ja nende [5] lokaatorid. Sagedusalad 144, 222 ja 432 MHz. |
| 6. Ilma- ja tropokaardid | dxinfocentre.com/tropo_eur.html tropo.f5len.org/forecasts-for European mudel GFS | Hepburn [3] ja F5LEN [4] tropo indeksi kaardid, merepinna rõhk isobarele (GFS kaudu Open-Meteo [18]). Hepburn ja F5LEN peavad olema autoritele lubatud. |

Klaster telnet (allikad 1–4). Pärast ühenduse loomist ootab programm nimepäringut (tekst, mis sisaldab sisselogimist, kõnet:, kutsungit või teie kõnet); kui see ei jõua 4 sekundi jooksul, palun saatke sisselogimine. Seejärel saada sektsiooni käsud, mis on eraldatud punktist ja komist, üks iga 0,6 sekundi järel. Televõrgu juhtmärgid eemaldatakse enne ridade lugemist. Kui ühendus langeb ja Riconnetti on aktiivne, proovige uuesti 10 sekundi pärast (15, kui server ei reageeri üldse) [6].

PSKReporter MQTT (allikas 5). Programm registreerib teema pskr/filter/v2/{banda}/{modo}/{tx_call}/{rx_call}/{tx_grid}/{rx_grid}/{tx_dxcc}/{rx_dxcc} fikseerides ainult riba ja jättes teised väljad vabaks märgiga + [5]. Esimesel ühendusel tuleb kontrollida täpset nime, millega server ribasid näitab (näiteks 2m, 1,25m, 70cm).

## 5. Klastrijoonest kohani

Üks rida on koht, kui sellel on vorm DX de SPOTTER: sagedus DX kommentaar hhmmZ. Programm tuletab jälgimisseadme, jaama DX, sageduse kHz-s, kommentaari ja nüüd UTC-d; nimed eemaldavad skimmerite sufiksi (-#, -1...). Seejärel rakendatakse selles järjekorras järgmisi reegleid [6].

| Band | Segment aktsepteeritud |
| GRAVES (koos 144-ga) | 143,040 – 143,060 MHz |
| 144 MHz | 144 – 148 MHz |
| 222 MHz | 220 – 225 MHz (ainult ITU 2. piirkond, Ameerika) |
| 432 MHz | 420 – 450 MHz |
| 1296 MHz | 1240 – 1300 MHz |
| 2320 MHz | 2300 – 2450 MHz |
| 10 GHz | 10,5 GHz |

1. Bänd. Sagedus peab jagunema ühte hallatavatest segmentidest; kõiki ülejäänuid, eriti kõiki alla 49 990,0 kHz sagedusi, ei näidata ja loetakse sagedusribast väljas olevaks.
2. Tee. Üks tuntumaid kommenteerimisviise on FT8, FT4, Q65, JT65, ISCAT, CW, SSB, FM, RTTY, PSK31, MSK144, FSK441, JT6M; USB ja LSB muutuvad SSB-ks.
3. Negatiivne filter. Laik jäetakse kõrvale, kui see sisaldab režiime MSK144, FSK441, JT6M, JTMS, ISCAT või silte <AU> <AUE> <ES> <MS> <FAI> < F2 >, sulgudes on mittetuumalised suurtähed ja paariskohad. Need on viisid ja levik, mis ei ole troopilised: Hajuta meteoor, virmalised ja sporaadilised, F2.
4. Õhusaaste. Kui kommentaar sisaldab <AS> või <ACS> (ka segatud, nt <As>) või deklareerib koha õhuhajujana (õhuhajumine, õhuhajumine, õhusõiduki hajumine), jääb koht, kuid on joonistatud kollaselt punktiirjoonega (tume libisemine, et jääda selgel kaardil loetavaks) ja nimekirjas esile tõstetud.
5. Jaamade asukoht. Kui kommentaaris on kaks lokaatorit (nt JN54 < JN83) on esimene neist, kes teevad koha ja teine jaama DX; ühe lokaatoriga on see DX jaamast. Mis on puudu, otsitakse C ALL3 -st. TXT, ainult täpse nimega (sülearvutite kohta vaata allpool). Kui jaam jääb ilma positsioonita, ilmub koht loendis märkega "tundmatu asend", kuid mitte kaardil.
6. Topelt. Reklaam, millel on sama DX, sama jälgija, sama riba ja sama allikas, mis juba olemas, asendab selle lisamise asemel.

C ALL3.TXT on JTDXi kasutatav nimeandmebaas → lokaator vormingus CALL, LOCATORE,. Projektifail sisaldab 1 048 847 nime, kõik kehtiva lokaatoriga; faili päis räägib endiselt 34 323 kandidaadist, sest see jäi esimese väljaande [7]. See laaditakse käivitamisel taustal, nii et aken on kohe kasutatav. Faili otsitakse programmi kõrval kaustas Andmed, kataloogis, kust programm käivitati, ja ühes või kahes ülalolevas kataloogis. Kui seda ei ole, annab aken selle käivitamisel teada ja olekuriba annab sellest punase signaali. Selle nimi kasutab alati oma 6-märki. Ainult lokaatorite reegel: kui saadud (spot, MQTT, dekodeerimine, QSO desunti) on vähem kui 6 märki ja C ALL3. TXT-l on üks kuni 6, see on alati väärt C ALL3.TXT; sülearvutite jaoks (F/ IC8TEM, IC8TEM/P) on ainult täpne hääl: ilma, jaam jääb ilma üürileandjata ja ei ole mõeldud mujal. Reegel kehtib ka joonistamise ajal, nii et enne andmebaasi laadimist saabunud andmed töötavad ise.

Üks andmebaas: C ALL3.TXT. Alates 1.3.4 on kõik lokaatorid ühes failis, C ALL3.TXT, mis jääb JTDX ja WSJT-X vormingusse (CALL, LOCATORE,,,,,) ja mida saab kasutada ka WSJT-X, JTDX ja MSHV-s. Ei ole rohkem faile eraldi. Faili ülaosas on päise järel kaks plokki, mis on tähistatud kommentaarireaga: // [LIVETROPOVHF-MANUALI] koos kirjetega käsitsi ja // [LIVETROPOVHF-CLUSTER] klastri parandustega; seejärel // [LIVETROPOVHF-FONTI] koos kõigi teiste elementidega tähestikulises järjekorras. Iga nominatsioon ilmub üks kord. Sublingual Data and Sources › Appointments (C ALL3) otsite nime, kirjutate üürileandja ja vajutate Add / change: hääl läheb häälte plokis käsitsi ja see on väärt ennekõike isegi asukoha üle, mida jaam ise saadab. ∫ Eemaldab nominatsiooni C ALL3.TXT-st (järgmine värskendus taastab selle allikatest, kui neil see on). Lisa fail... loe nimekirja KÕIK, LOCATOR (võtad vastu ka punkti ja koma, tabeli ja tühiku) ning kirjuta see käsitsi valitud failide sekka. Valitud faili ei kopeerita. Laadige C ALL3.TXT uuesti kettalt pärast redigeerimist redaktoriga. Allikate täielik järjekord: oma lokaator, käehääled, klastrite parandused, ülejäänud C ALL3. TXT (allikad, õpitud live lokaatorid ja G7VRD), saadud lokaator (mis kehtib andmebaasis ainult siis, kui sellel on 6 märki ja see ei lõpe LL või MM-iga).

C ALL3.TXT uuendamine. Samas alamkeeles laadib nupp Update nüüd alla allikad (C ALL3.TXT of WSJT-X Improved, C ALL3.TXT EME of W7GJ, grid_data.bin of JTDX), liitub nendega (esimene, kellel on nominatsioonivõit), kontrollige, et nad on vähemalt 50 000 nime (muidugi ei muutu midagi), salvestab C ALL3. TXT Eelmine C ALL3. TXT-kirjed jäävad ja jääb 6-kohaline lokaator, kus allikal on ainult 4 sama ruutu. Käte hääled ja korrektuurid klastrist jäävad ülaosas muutumatuks. Kui värskenduskast on iga päev omaette, toimub kontroll alguses. Programm kirjutab ümber C ALL3. TXT kohe pärast iga häält käsitsi ja õpitud lokaatorite puhul kõige rohkem iga 10 minuti järel ja sulgemisel; C ALL3. Paketi TXT on kirjutuskaitstud ja atribuut eemaldab ennast. Linuxi paigaldajad ei kirjuta üle C ALL3. TXT on juba olemas; et Windows installib selle ainult siis, kui see puudub. ALL3

Automaatsed parandused klastrilt. DM4X ja RBN automaatsetes kohtades toob kommentaar kaks lokaatorit, näiteks KN09tp <> KN09: esimene on see, mida jaam, mida te märkate, deklareerib endast, 6 tähemärki; teine on jaama tunne, sageli ainult 4. Kui esimesel on 6 märki (ja ei lõpe LL või MM) ja erineb andmebaasi omast, parandab programm C ALL3. Ainuüksi TXT: parandus läheb plokki // [LIVETROPOVHF-CLUSTER], kolmanda välja viimase kinnituse kuupäevaga kehtib see allikate kohal (käehäälte all) ja ilmub kahtlaste signaalide vahel, mis on uuendatud kui "C ALL3 ". Seega säilib C ALL3. TXT uuendused. Kinnitamata parandus kaheks aastaks kaotab tähtsuse ja jääb normaalseks hääleks.

Viimane allikas: G7VRD. Kui kandidaadil ei ole C ALL3 -s 6-märki. TXT, programm küsib seda taustal G7VRD avalikule API-le (api.g7vrd.co.uk, "kõnede operaator"), mis kogub võrguliiklusest automaatselt lokaatoreid. Nõustute ainult 6-märgiga, mis ei ole ruudu keskpunkt (LL või MM); taotlused rühmitatakse, maksimaalselt 40 kandidaadiga minutis, ja iga nimetaja küsib kord päevas, sest see on hobistlik teenus ilma garantiideta. Leitud lokaator sisestab C ALL3. TXT, kui nime ei ole või kui sellel on ainult 4 märki samast ruudust; otsing lülitub välja nimekaardi kastiga.

Kohtumised baariga. PSKReporteri MQTT teemades eraldab riba tasemed, seejärel on liitnimetustel riba asemel punkt ( IV3. HB9CAT). Programm paneb riba tagasi ja kui sõnum sisaldab täisnimesid, kasutage neid: IV3/HB9CAT jääb IV3/HB9CAT -ks kaardil, loendis ja andmebaasis.

Lokaator õppis otse. C ALL3. TXT ei sisalda kõike: näiteks SP9EMF ja SP9NVY ei ole olemas ja jääb siis PSKReporteri saadetud 4-märgiliseks lokaatoriks (kaart ütleb nii). Nende juhtumite vähendamiseks kirjutab programm C ALL3. TXT iga 6-kohaline, mis pärineb mis tahes allikast (PSKReporter, näiteks saatja või vastuvõtjana, klaster, UDP dekodeerimine), et saada C ALL3. TXT ei ole või on ainult 4 tähemärki samas ruudus; kunagi muudab hääl 6 tähemärki. Kuni punktini 1.3.3 olid need lokaatorid Data/ LOC6.TXT-s.

Üleminek punktist 1.3.3. 1.3.4 alguses failid, välja arvatud eelmised versioonid (Dati/C ALL3 -MANUALE.TXT, Data/C ALL3 -AGGIUNTE, Data/C ALL3 -CLUSTER). TXT, Data/ LOC6.TXT ja Data/C ALL3 - G7VRD.TXT ühendatakse C ALL3. TXT vabastab kirjutamise, vanad failid võtavad.fuso laienduse, te ei loe enam ja saate kustutada.

Automaatvastuvõtjad. RBN, dm4x ja PSKReporter teatavad sageli nimesid järelliitega, mis näitab vastuvõtjat: -SDR, /SDR, -# (skimmer RBN) või -1, -2... (lisaks sama jaama vastuvõtjad). Rendileandja jaoks eemaldatakse järelliide: OK2OZL -SDR võtab OK2OZL JN89UE C ALL3.TXT-lt, kui jaam ei saada juba 6-märki. Näidatud nimi jääb sellele, millel on järelliide.

Lokaatoreid lõigatud. Mitte kõik allikad ei käsitle oma üürileandja laiendust: paljud seavad selle 4 märgile ja mõned automaatsed ülekandesüsteemid kärbivad teadlikult 4-märgilist lokaatorit, isegi neid, mis saavad andmeid FT8, FT4, Q65 ja JT65 tegelikust kuulamisest või täidavad selle ruudu keskmega (LL või MM, nt JN89MM). Sel põhjusel asendatakse 4-märk või 6-märk, mis lõpeb LL või MM-iga, C ALL3 -ga. TXT samast ruudust. Kui õige asukoht ja väljaku keskpunkt ei lange kokku paarikümne kilomeetriga, jääb viga tropo kaugustega võrreldes väikeseks.

## 6. Kes kuulab?

Iga PSKReporteri MQTT sõnum teatab [5] saajate nime ja asukoha, nii et te ei pea seda otsima C ALL3.TXT-st. Valijas valitud riba jaoks on programmil iga vastuvõtujaama jaoks: üürileandja, viimane vastuvõtt, dekodeeritud viisid, jaamade arv ja maksimaalne vahemaa. Jaam lahkub nimekirjast, kui ta ei saa enam midagi valitud vahemikus (3, 5, 10, 15, 30 või 60 minutit).

Kaardil on iga vastuvõtja tumeroosa tühi ring ja igast stardist täisroosad jooned jaamade suunas, mis tunnevad ja saadavad tagasi võrku. Liinid on alati näha, ilma klikkimata. Kuulamisjaamade, kuuldejaamade ja iga koha kahe pea kohtumised kirjutatakse alati kaardile, ilma et peaks klõpsama (sama nimi kirjutatakse üks kord). Klõpsates ringi, muutuvad selle read veelgi paksemaks ja avavad vahekaardi üksikasjadega. Kui MQTT-i lokaatoril on vähem kui 6 märki, kasutab programm C ALL3.TXT (umbes üks miljon nime), kui see on kooskõlas saadud ruuduga.

Hajuta meteoorirežiime. Nagu klastripunktide puhul, jäetakse ka MSK144, FSK441, JT6M, JTMS või ISCAT MQTT suhtarvud kõrvale ja arvestatakse ainult; sama kehtib UDP dekodeerimise kohta sellisel viisil.

Mitte päris kuulajad. Mõned jaamad deklareerivad, et võtavad vastu 144 MHz, kuid tegelikult on vastuvõtja teisel sagedusalal (vale sageduse näitamine): nende aruanded kujundavad tuhandete kilomeetrite pikkused olematud avad. Programm kontrollib seda ise. Iga jaama, mida kuulaja ütleb VHF-is kuulda, jälgitakse 30 minutit kõigis bändides spetsiaalse MQTT-i kirjaga (pskr/filter/v2/+/{nominative}/#) [5]. on tõestuseks. Kui vähemalt kaks kuulaja kuuldud jaama on samal minutil (±10) aktiivsed teistes sagedusribades, ei peeta kuulajat ehtsaks ja kõik tema VHF-teated jäetakse kõrvale; MQTT-registris teatatakse teda reetnud nimi ja jaamad. Täpide loendis näitab mõõdikute kõrval MQTT nupp voolu olekut (roheline leht, kui see on ühendatud, punane, kui mitte) ja seda, kui palju kuulajaid valitud sagedusalas ja vahemikus kõrvale heideti; klõpsates avab see vahekaardi koos vette tagasi laskmiste nimekirjaga ja kõigi teiste ribade kohta leitud jaamadega.

Pikk ühendus Hiinaga. Üle 1000 km pikkust ühendust (Clumbing Spot, MQTT Listening, QSO Desunti) kohe ei näidata: jääb pidama ja loendur "hoitud > 1000 km" ütleb numbri. Ilmneb, kui teine ühendus, erinevate jaamade vahel ja vähemalt pool mööda, kinnitab seda ühega neist kriteeriumidest: sama jaam ees (näiteks sama kuulaja) ja teine jaam samas suunas 10° piires; sarnane koridor, mille mõlemad pead jäävad teise ühenduse omadest 200 km raadiusse; paralleelne marsruut, mille suund jääb 10° piiresse ja külgmine kaugus 200 km piires, kattub piki marsruuti. Mõlemad ilmuvad ja MQTT logi näitab põhjust. Selle UDP dekodeerimised on alati nähtavad ja kehtivad kinnitusena teistele. Säilitatud ühendused jäävad mällu 60 minutiks.

Mitte midagi alla 144 MHz. Programm töötab 144 MHz-st ülespoole: kõik alla 143 MHz jäetakse kõrvale. Erandiks on GRAVES, Prantsuse radar, mis edastab CW-s 143,050 MHz Broye-lès-Pesmes'i ( JN27SI) poolt: reklaam vahemikus 143,040 – 143,060 MHz või koos GRAVES-iga nimes, kuvatakse 144 MHz vahel koos radariga saatjana. Reklaamtahvlil on kirjas, kas vastuvõtja asub valgusvihu sees (sektorid D, C, B, A alates 90° kuni 270° asimuutist radarilt näha, 6 sammu 7,5° kumbki) või väljas; vahekaart GRAVES, nagu YA-MSis, näitab radarilt nähtavat taevast: sektorid, sel ajal valgustatud samm (järjekord PE1ITR 2021: 0,8 s sammu kohta, tsükkel 4,8 s Andmed ja geomeetria tulevad YA-MSist. Selle tunnetamine tropes on raske, kuid see võib juhtuda.

Inimkobarate rehabilitatsioon. Jaama, mis esineb ka teistel sagedusaladel, ei loeta kahtlaseks, kui samade minutite (±30) jooksul DX inimparved ( ON0DXK, GB7RAU; mitte dm4x, RBN või muud automaatsed allikad) kahjustavad kõnealuse bändi aktiivset kahjustust teiste nimedega tehtud laikudes: see on nii jaamade puhul, mis tõesti töötavad mitmel ribal koos. Nii naaseb äravisatud kuulaja nähtavale (MQTT register kirjutab „rehabiliteeritud) ja madalate ribade jaoks on võimalik avada pikaajaline link.

Aktiivsed jaamad sagedustel alla 144 MHz. Pikkade ühenduste karantiinis ei kinnita jaam, mis samal minutil kuuleb alla 144 MHz sagedusaladel (teiste sagedusalade MQTT aruannetest), muid ühendusi ja seda ei avalikustata, kuigi sarnane või paralleelne marsruut kinnitaks seda.

## 7. Reklaamide nimekiri

See võtab 35% paremal kaardi ja koha. Ülemine: pealkiri, riba valija (50, 70, 144, 222, 432, 1296, 2320 MHz, 10 GHz), kauguse valija (viimane 3, 5, 10, 15, 30, 60 minutit) ja värskendusklahv. Allpool neli meetrit: nähtavad laigud, AS/ACS, filtrist välja visatud, kuulamisjaamad.

Iga rida näitab liini tüüpi, aega UTC, DX → DE, teed, vahemaad kilomeetrites ja teisel real allikat, sagedust ja kommentaari. Viimane on kõige tipus. Üks klõps real valib koha: kaardil joon kasvab ja kaks nime ilmuvad.

## 8. Täpikaardi ja troposhatteri kadu

Valitud koha puhul näitab kaart lokaatoreid, kaugust, asimuuti, sagedust, režiimi, allikat, nüüd kommentaari, keskmist tropoindeksit mööda teed ja marsruudikao hinnangut hajutatud tropo puhul.

Kaugus ja asimuut arvutatakse maksimaalsel ringil, mille Maa raadius on 6371 km (emisenoverso valem kauguse kohta, esialgne asimuut DX-jaamast vaatlejani).

Kaotus troposcatter. Kahe siledal pinnasel asuva jaama põhikadu on vaba ruumi kaotuse ja hajumiskao summa: G3YGF [2]

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

Kaod on dB-s, D on kaugus kilomeetrites, f sagedus MHz-des, θs difusiooninurk kraadides.

8497 km on Maa ekvivalentne raadius (4/3 füüsilisest). θA ja θB sõltuvad iga jaama horisondist:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

koos Δh takistuse kõrgus jaama ja selle kauguse suhtes horisondist. Iga horisondi tase maksab umbes 10 dB [2]. Programm ei tunne jaamade horisonti, nii et see näitab põhikadu θA = θB = 0 ja deklareerib selle ("sile maa"). G3YGF lisab kliimatermini, kus N on reageerimisvõime [2] pinnasele; seda kasutatakse siis, kui N on saadaval ilmamudelist:

```math
L_{N} = 0,2 \cdot (N - 310)
```

Kontrollimine: 110 km kuni 144 MHz puhul annab valem 166,5 dB, võrreldes umbes 168 dB vooga artikli diagrammil. 500 km kuni 144 MHz puhul annab see umbes 206 dB. Eeldatav signaali/müra suhe on süsteemi võimsus (eirp + vastuvõtja efektiivne tundlikkus) miinus kadu: G3YGF 144 MHz mudelisüsteemiga (198 dB) annaks 500 km tee umbes −8 dB SSB [2], see tähendab, et ühendus on ainult puhta hajumise parema levikuga.

## 9. Minu dekodeerimised: UDP WSJT-X, JTDX ja MSHV

WSJT-X, JTDX ja MSHV saavad saata UDP ära iga rea, mida nad dekodeerivad. Programm kuulab 127.0.0.1:2237 ja kordab sama paketti, baiti baiti kohta, 127.0.0.1:2238, nii et teised programmid, mis kasutasid seda porti (GridTracker, JTAlert, logija), jätkavad töötamist, kui liigutate neid 2238. aastal. Aadressid ja uksed muutuvad andmekogu UDP alamkeeles; kuulamisaadressina võib kasutada ka multicast gruppi (nt 224.0.0.1), kui dekodeerimisprogramm on selline.

Paketid järgivad WSJT-X võrguprotokolli: algne maagiline number 0xADBCCBDA, skeemi number ja sõnumi tüüp. Programm kasutab peamiselt kolme [8] tüüpi:

- Heartbeat: ütleb, milline programm edastab andmeid ja millise versiooniga.
- Olek: valimise sagedus ja režiim; sagedusest saad riba (sama täppide tabeliga).
- Dekodeerimine: Nüüd, SNR, aja ja sageduse raiskamine, dekodeeritud sõnumi viis ja tekst.

Tekstist saate kuuldud jaama: CQ 9 A2PQR JN83 on jaam pärast CQ (säästes DX, EU, TEST...), IK0JKL IT9MNO JM68 on teine, st kes edastab. Lokaator on sõnumi oma, kui see on (4 märki, erineb RR73 -st); vastasel juhul otsite C ALL3 -st. TXT. Need on kuulamisliinid: jaamad, mida teie vastuvõtja kuuleb nüüd. Kaardil muutuvad need teie QTH-st jaama punaseks jooneks koos nimega; kiht süttib minu dekodeeritud kastiga ja nõuab kehtivat lokaatorit. Paremal asuvas loendis ütleb loendur, mitu jaama te valitud bändil dekodeerite.

QSO desunti dal live. Kui dekodeerimine on lõputeade kujul C ALL1 C ALL2 RR73 (või 73 või RRR), näiteks IC8TEM IK3FHP RR73 või IK3FKP SV8PEX 73, on kaks jaama lõpetanud lingi. Programm salvestab selle QSO-na, kusjuures iga sõnumi lokaator on võetud viimasest sõnumist, milles ta seda deklareeris, või kui seda ei ole, siis C ALL3.TXT [7] -st ja joonistab selle kaardile rohelise joonena kahe jaama vahel (strate „QSO desunti dal live). Jaamade paar loeb kord riba kohta; loendis valitud riba ja vahemik on kehtivad.

Samas alamkeeles näitab silumine iga vastuvõetud paketi teksti (tüüp, aeg, väärtused ja sõnum), soovi korral ka kuueteistkümnendbaiti ja edastamise rida. See on kontrollida, kas dekodeerimisprogramm tegelikult saadab andmeid.

## 9 bis. Keeled Hepburn ja F5LEN

Igaüks näitab, mida programm allikast võtab. Nupp „Lugege lehekülge ja laadige alla kõik pildid avab märgitud lehekülje (base dxinfocentre.com/tropo_eur.html for Hepburn [3] ja tropo.f5len.org/forecasts-for-europe for F5LEN) ja laadib alla kõik vahemälus olevad pildid käivitatava faili kõrval; samuti saate kleepida ühe pildi aadressi. Iga pildi jaoks on olemas kast selle välistamiseks ja väli prognoosimisajaga. F5LEN 31 Euroopa pilti on nime saanud mudelivõistluse aja järgi (03.png... 48.png iga 3 tunni tagant, 54... 96 iga 6 tunni tagant, 108... 192 iga 12 tunni tagant) ja programm võtab aega [4] -faili nimest; Hepburni jaoks on see lehe järjekord. Kaardil F5LEN valib menüü piirkonna: Euroopa (tropo.f5len.org/forecasts-for-europe), Austraalia (tropo.f5len.org/Australia, vk03.png... vk192.png) või Atlandi ookeani Cabo Verde ja Iirimaa vahel (tropo.f5len.org/atlantic-ocean/atlantic-d4-ei, atl03.png... atl192.png). Kõigil on samad sammud. [4]

Allpool on kalibreerimisloendur. Üks klõps pildil lisab kontrollpunkti (lokaator või lat,lon: see võtab vähemalt 6) või legendi värvi selle väärtusega. Programm arvutab pikslite sobitamise → koordinaadid teise astme polünoomi interpolatsiooniga minimaalseteks ruutudeks [19], loeb värvid valitud tolerantsiga ja F5LEN teisendab ühikud N skaalal 0–10 a · N + b. "Salvesta kalibreerimine ja testi lugemine" kirjutab faili Tarature kausta ja kontrollib, millise piksli teie lokaator langeb. Enne kalibreerimise seadistamist tuleks kaartide autorilt küsida luba.

Piirkonnad. Igal allikal on rohkem piirkondi, millest igaühel on fikseeritud nime pildid: Hepburn tr_map/fcst/{code}{ore}.png?v{data} (eur Europe, nwe North-Western Europe, eeu Eastern Europe, ent Kirde-Atlandi, nat North Atlantico, eam North America East, nsa South America North America, sam South America, mid-East Asia, nca Kesk-Septential Asia) Iga allika kaardil on kõigepealt näha fusiooni eelvaatlus: kõik allalaaditud pildid on kokku pandud nagu põhikaardil, punktiirjoonega paneel ja iga piirkonna nimi, hall loor, kus ükski pilt ei kata ala ja liugur tundide kerimiseks; vasakul piirkondade loend, millest igaühel on kast, mida kasutada, loetud kaartide arv ja kust äärmused tulevad (OCR, käsitsi või tehases; kui OCR ei õnnestu, ilmub tekst, mida ta loeb). Manuaalne kalibreerimispink jääb homonüümse nupu taha. Menüüst Benchi piirkond saab valida, milline välimus, kast „Kasutage seda piirkonda kaardil sisaldab seda indeksis. Raami leitakse üksi (kaks horisontaalset joont ja kaks sama värvi vertikaalset joont); pildi paigutamiseks teenige raami äärmisi lat/lon: Hepburn Europe (10° W–40° E, 27°–56° N) ja F5LEN Europe (15° W–40° E, 30°–65° N on juba programmis, teiste piirkondade jaoks kirjutavad nad neljale O E S N väljale, lugedes teljemärgiseid. Skaala kordaja muudab kaks allikat võrreldavaks: lõppväärtus = kordaja × legendi väärtus. [3] F5LEN [4] F5LEN

## 9 ter. Ekspordi sõnadesse

Need kaks dokumenti on nupu „Eksport sõnadesse ülaosas. Fail on kirjutatud Office Open XML-vormingus; valemid muutuvad Wordi võrranditeks (OMML), nii et need jäävad redigeeritavaks [16] võrrandi redaktoriga. Kui salvestamise aken ei ole saadaval, lõpeb fail kaustas Dokumendid käivitatava faili kõrval.

## 9 quater. Kes mind kutsub?

Kui teile saadetakse kohalikus UDP-s sõnum (eesnimi on teie, näiteks IC8FAX IK3FHP JN55 või IC8FAX IK3FHP -12), jälgib programm teie QTH-st jaama, mis kutsub teid musta punktiirjoone ja vilkuva joonega, mille nimi ja lokaator on teie kõrval. Lokaator pärineb C ALL3.TXT-st (sealhulgas käsihääled) või sõnumist. Rida jääb, kuni UDP läbib QSO sulgemise selle jaamaga ( RR73, RRR või 73, ühes kahest suunas), kõige rohkem 3 minutit viimasest kõnest; te võtate kõik üle ja ei järgi välistamiskriteeriume, nii et otsene kõne ei pääse kunagi.

PSKReporter ei edasta MQTT-i kaudu sõnumite teksti: ütleb ainult, kes sai kelle, sageduse, viisi ja SNR-iga. Sellepärast tunnustatakse otsest kõnet ainult oma UDP dekodeerimisega.

## 10. Ajareguleerimine (NTP)

Digitaalsed režiimid ja hetkeaeg nõuavad täpset kella. Iga 3 minuti järel küsitleb programm 10 NTP serverit (SNTP UDP 123-s): 0.pool.ntp.org, 1.pool.ntp.org, 2.pool.ntp.org, 3.pool.ntp.org, it.pool.ntp.org, ntp1.inrim.it, ntp2.inrim.it, ptbtime1.ptb.de, time.google.com, time.cloudflare.com [11].

Iga vastuse puhul arvutatakse välja vette tagasi laskmise ja tagastamise viivitus, kõrvaldatakse ebanormaalsed väärtused ja saadakse mediaan, kusjuures SNTP [10] arvutab vette tagasi laskmise ja viivituse. Vähemalt 3 vastusega nõustute, et programmi UTC kell joondub ja järgmine ilmub NTP:OK rohelisena; kui vastused ei ole piisavad, kuvatakse NTP:FAILED punasena ja kell jääb arvuti omaks. Süsteemi kella ei puudutata, sest vaja oleks administraatori õigusi; kõik sisemised programmikavad (punktid, 5–60-minutilised aknad, dekodeerimised) kasutavad joondatud aega. Juhtseadis lülitub alati taustal: liideses näete ainult tulemust kella kõrval; serverid, intervallid ja minimaalsed vastused muutuvad LIVETROPOVHF.ini (ntp_servers, ntp_minutes, ntp_min).

## 10 bis. Üldised väljajätmise kriteeriumid

Kaks kasti kihiribal kehtivad kõigele, mida joonistad (spot, PSKReporter, dekodeerimine, QSO desunti):

- Välja arvatud alla 100 km. Alla 100 km pikkuste jaamade vaheline liiklus ei pruugi tropo jaoks olla oluline (maapealne laine, optiline kaugus): sisselülitatud kastiga te ei joonista ega ilmu loendisse. Start on väljas.
- Üle 1000 km: ainult ruudud. QTH (algatav lokaator) ümber on igas suunas 1000 km krooni. Kui ainult üks lingi kahest jaamast on väljaspool seda krooni, ei joonistata ühendust joonena: selle asemel värvivad nad sireli, punktiir 45° juures, asjaomaste jaamade lokaatoriruududud (4 märki). Sama kehtib PSKReporteri jaamade kohta, mis kuulavad üle 1000 km, mis ilmuvad ringi asemel ruuduna. Seni näete liiklust ilma joonte kaarti täitmata. Ilma korrektse lokaatorita loetakse ühenduse pikkust. Teie QTH ruut ei värvi kunagi sirelit, isegi kui kuulate krooni taga asuvat jaama: kehtib seatud üürileandja, mis tahes nimi kasutab seda, kes töötab jaamast. Krooniraadius valitakse kasti kõrval: 1000, 700 või 500 km. Kast Corone 1000/700/500 km joonistab kolm krooni ümber QTH, mis on joonistatud sirelis, kõige märgatavam valik ja kaugus kirjutatud põhja. Täpide loetelus järgneb kaugusele... Algus käib. Kriteerium kehtib pärast karantiini: veel säilinud pikad ühendused ei ilmu isegi ruutudena.

Millised on sireliruududud, mis on kaetud 45° juures. Teie QTH-st eemal olev liiklus huvitab teid, et mõistaksite, kus levik on avatud, kuid selle read, mis on sadu kilomeetreid pikad ja sageli kümned, katavad kaardi ja peidavad, mis teie jaoks loeb: avad, mida saate oma jaamast ära kasutada. Sel põhjusel ei joonista programm valitud kroonist kaugemale jooni, vaid värvib kaasatud jaamade lokaatoriruudu (2° × 1°). Tulemuseks on "aktiivsete tsoonide" kaart: kus ilmuvad paljud lähedal asuvad sireliruududud, on kanal või ava, isegi kui see teid otseselt ei puuduta; Kui ala laieneb teie suunas, on see signaal antenni suunamiseks selles suunas. 45° armatuurlaud võimaldab näha tropoindeksi ja võrgu all, nii et nad võrdlevad lühidalt prognoosi (alumine värv) ja tegelikku liiklust (lilla). Teie QTH ruut ei värvi kunagi, sest see on lähtepunkt, mitte kauge ala. Värvilised on ainult kroonitagused ruudud: krooniga ristuv või sees olev ruut jääb vabaks isegi siis, kui on üks kahest jaamast. Ridade nägemiseks lülitage lihtsalt kasti välja või valige suurem kroon.

## 11. Teemad ja keeled

Teemad: 18 ühist IC8TEM programmidele, sama nagu YA-NEC ja E-LAYER ANALÜÜZER: Selge, öö, must, kaheksa pastelli (sinine, oranž, akvamariin, roheline, roosa, sirel, indigo, pruun), Scia meteorica (YA-MS palett), kolm IP-TRX-CTRL (Chiaro, Notte, Nero) ja sinine, lilla, roheline. Alates 1.3.4 näeb Chiaro välja nagu YA-NEC: helehall taust, külgnevad valged paneelid, sinised öökaardid ja sinine aktsent. Tumedatel teemadel muutub ka aluskaart tumedaks. Prognoosi värviskaala jääb kõigis teemades samaks. Kitsatel ekraanidel (alla 1500 piksli, näiteks 1366×768) näitab ülaosas olev kell ainult tundi, nii et tekstide teema, kursor ja keel jäävad nähtavaks. Keeled: YA-MS [17] wiki (inglise, Travel, Deutsch, Nederlands, Français, Español, Português, Dansk, Norsk, Svenska, Íslenska, Eesti, Lietuviע, Latviešu, Руский,,кра,нська, Türkçe,ا Araabia, heebrea ja urdu abil kogu aknapeeglid paremalt vasakule; kaart jääb [12] -ks. Tõlked puudutavad liidest; olekuriba kaks tehnilist dokumenti ja sõnumit jäävad itaalia keelde. Päevade nimed pärinevad rahvusvahelistest keskkondadest. NET valitud keele jaoks.

Teema ja keele muutmine üleval paremal asuvast kahest menüüst kella kõrval ja rakendamine kohe, ilma programmi taaskäivitamata ja andmete kogumist katkestamata. Valiku salvestab LIVETROPOVHF.ini.

## 11 bis. Kahtlustatavad ja iseõppivad aruanded

Vahekaart Kahtlustatavad aruanded kogub kõike, mida programm kontrollib, mis on tüübi ja nime järgi rühmitatud, kusjuures viimased 24 tundi on loetletud. Programm õpib iseenesest: iga paari (tüüp, nimi) loeb positiivseid tulemusi (+) ja vastupidi (−) ning pärast 3 kokkulepitud tulemust loob automaatse reegli; vastupidine tulemus seab selle kahtluse alla. Kasutaja otsused (hea, halb) kehtivad alati; pulsante nupp tagastab otsuse programmile. Eeskirjad on esitatud dokumendis Data/APPRENDIMENTO. TXT, programmi kõrval; automaatsed, mis ei ole aasta jooksul tugevdatud, unustatakse.

| Tüüp | Millal | Väljumine + / − | Reegelefekt |
| EME ( Q65/JT65) | Q65 või JT65 üle 1000 km Kuuga horisondi kohal mõlema peani või EME kommentaariks | - iga kord, kui Kuu on ühine | Visake ära: EME jaam, selle pikad Q65/JT65 -d on samuti välistatud ilma tavalise kuuta. Hea: käsitletakse tropona |
| Pikk ühendus | üle 1000 km, karantiinis | + avalikustatud, − aegunud ilma kinnituseta | Hea: jaama pikkused näitavad kohe. Visake ära: mitte kunagi |
| Kuulaja | PSKReporteri vastuvõtja, kes kuuleb aktiivseid jaamu teistes sagedusribades | - äravisatud, + inimkobarate poolt rehabiliteeritud | Hea: pole kunagi ära visatud. Visake ära: alati |
| Erinevad üürileandjad | Square sai teistsuguse tulemuse kui C ALL3. TXT | + iga kord, kui sa seda kordad | Hea: jaam on liikunud, see on saadud väljakut väärt. Visake ära: vale C ALL3.TXT |
| Square Center | 6 tähemärki LL või MM |  —  | Ainult teave |
| Automaatne vastuvõtja | Kohtumine -SDR, /SDR, -# või -n |  —  | Ainult teave |

EME. Q65 ja JT65 on Luna kaudu suhtlemise viisid: Teoreetiliselt kuuluvad nad väljajätmise klausli alla nagu hajumise meteoriit, kuid neid kasutatakse ka tropos. Programm arvutab Kuu positsiooni Almanah Astronoomia madala täpsusega valemitega (umbes 0,3°). [20]: kui ühendus Q65 või JT65 -s ületab 1000 km ja Kuu on mõlema peaga horisondi kohal, on see peaaegu kindlasti EME ja on välja arvatud (see loeb välja arvatud). Kui Kuu ei ole tavaline, jääb ühendus tropes võimalikuks ja läbib pikkade ühenduste karantiini, kuid signaal jääb nähtavaks. See kehtib klastrite, MQTT ja selle enda UDP dekodeerimise kohta.

## 11 ter. Kokkupuute vette tagasi laskmise kriteeriumid ükshaaval

Programm tahab näidata ainult troposfääri levi 144 MHz ja kaugemalgi. Iga sissetulev teave (Cluster Row, PSKReporter Report via MQTT, UDP dekodeerimine) läbib filtrite seeria, alati samas järjekorras. Kõrvaldatud andmed ei kao kuhugi: loetakse (väljajäetud kontakti ülemine riba) ja kui põhjus on kaheldav, ilmub vahekaart Peatatud aruanded.

|  N.  | Kriteeriumid | Kus see on? | Mis ära visati | Saad sa riideid vahetada? |
|  1  | Band | klaster, MQTT, UDP | Kõik alla 143 MHz (ka 50 ja 70 MHz), v.a GRAVES 143,050 MHz. Sagedusalad on 144, 222, 432, 1296, 2320 MHz ja 10 GHz | Ei. |
|  2  | Satelliit QO-100 | klaster, MQTT, UDP | 2400,0–2400,5 MHz ja 10489,5–10490 MHz: see on geostatsionaarne satelliitliiklus, mitte tropo | Ei. |
|  3  | Meteoori hajumine | klaster, MQTT, UDP | JT6M ja ISCAT (ka ISCAT-A/B), kommentaarid "&" ja mitte-tropoksülevimärgistega (<AU>, <AUE>, <ES>, <FAI>, < F2 >) alati. MSK144, FSK441 ja JTMS (ka <MS>) ainult üle 250 km: 250 km jooksul me aktsepteerime, sest sel kaugusel kasutatakse neid režiime ka tropos | Ei. |
|  4  | EME (Luna kaudu) | klaster, MQTT, UDP | Kommentaaris kirjutatud EME; Q65 või JT65 üle 1000 km, kui Kuu on mõlema peaga horisondi kohal; jaamad õppisid EME-na | Jah: hea reegel kahtlaste signaalide korral |
|  5  | Mitte- tõeline kuulaja | MQ | PSKReporteri vastuvõtja, kes ütleb, et kuuleb 144 jaamas, mis on sel ajal aktiivsed teistes sagedusribades (vähemalt kaks): Tema suhted on kõrvale heidetud. Tagasi hea, kui DX inimparved kinnitavad samu jaamu | Jah: hea/halb |
|  6  | Pikkade ühenduste karantiin | liinid | Üle 1000 km jääb ühenduspeidetuks, kuni leiab kinnituse: sama jaam samas suunas, teine marsruut 200 km koridoris või paralleelne marsruut 10° ja 200 km kaugusel vähemalt pool. Ilma kinnituseta aegub 60 minuti pärast | Jah: hea / väljas jaamas |
|  7  | Alla 100 km | read ja nimekiri | boksis on lühimad 100 km ühendused (maapealne laine, optiline kaugus) | Kast väljas käivitamisel |
|  8  | Krooni taga QTH-st | jooned | Kui jaam on teie QTH-st üle 1000, 700 või 500 km (teie valikul), pole joont: ainult kahe jaama kaetud lilla-ruududud. Teie QTH Square Never Colors | Kast ja raadius 1000 km peal |
|  9  | Ajaaken | kõike | Valitud vahemiku vanemad laigud (3 kuni 60 minutit) | Menüü üleval |
|  10  | Topelt | kobar | Sama koht (sama DX, sama vaatleja, sama riba, sama allikas) kordus | Ei. |

Märkus kriteeriumide 4 ja 6 kohta. Tropos on võimalik üle 1000 km pikkune ühendus Q65 või JT65 -ga ilma ühiskuuta: seda ei jäeta kõrvale 4. kriteeriumi alusel, vaid see läbib 6. kriteeriumi karantiini nagu mis tahes muu pika lüli. Otsesed kõned teile (punkt 9 quater) on ainus erand kriteeriumidest 7 ja 8: nad alati joonistavad.

Mis ei ole raiskamine. Lokaatori parandused (C ALL3). TXT käte häältega, klastri parandused ja õpitud lokaatorid, järelliited -SDR, ruudu keskpunkt LL / MM muudavad jaama, mitte ei viska seda ära. Jaama ilma üürileandjata seevastu ei saa tõmmata: jääb loendisse kaugusega «—».

## 11 quater. Värvid ja sümbolid

Igal värvil on ainult üks tähendus, mis on kõigis teemades võrdne (muuda ainult tausta).

| Värv ja tähis | Tähendab |
| Pidev sinine joon | Punkt DX klastrist, RBNist või DM4X st kahe jaama vahel |
| Tumeda servaga kollane joon | Koht õhuhajumise kommentaariga (peegeldus õhusõidukil) |
| Tumeroosa tühi ring | Kuulamisjaam PSKReporteris (MQTT) |
| Õhukesed roosad jooned ringist, alt roosa pall | Jaamad, mida kuulaja kuulis, mille nimi on alati kirjutatud; valitud kuulajale paksemad read |
| Punane joon ja punane nimi | Teie UDP dekodeerimised (WSJT-X, JTDX, MSHV): kes sa oled |
| Punase joone vilkuvad löögid | Selle jaamaga töötav QSO: Ta helistas sulle ja sulgemine pole veel saabunud või on WSJT-X/JTDX-i DX-kõne koos lubatud edastusega. Jätkake RR73, RRR või 73 |
| Roheline joon | QSO desunti dal live: RR73, RRR või 73 UDP vaade |
| Must joon valge äärisega | Keegi helistab sulle: see kestab kuni RR73/RRR /73 või kuni 3 minutit |
| 45° ruudud | Liiklus vähemalt ühe jaamaga väljaspool valitud võra: Ridu pole, ainult ruutude lokaator |
| Otsige lillasid, mis on kaugel põhjast | 1000, 700 ja 500 km ümber oma QTH; see valik on rohkem märgistatud |
| Ring ja punkt teema värvis | Teie QTH, nimi ja üürileandja |
| Oranž pall GRAVESiga 143 050 | Radar GRAVES ( JN27SI) |
| Proov sinisest punaseni | Hepburn trope indeks (0–10) või F5LEN klass või nende keskmine; soojem värv, paremad tingimused |
| Helehall samet | Ala ilma tropoandmeteta valitud aja jooksul |
| Hallsinised jooned väärtusega hPa | Isobereerub iga 4 hPa järel; H sinise kõrgrõhu korral, L punase madalrõhu korral |
| Punased, sinised ja lillad punktiirjooned | Isotermia kuni 850 hPa iga 4 °C kohta: punane üle nulli, sinine alla nulli, violetne null |
| Oranž näidis | Termiline inversioon: õhk kuni 925 hPa soojem kui maapinnal, intensiivsem tugevama inversiooni korral |

Kahtlustatavates aruannetes ütleb iga vahekaardi serv kehtiva reegli: roheline hea, punane Scarta, hall ei reegel.

## 12. Seadistused ja kaasaskantavus

Aken. Alguses võtab aken 90% ekraani kasulikust ruumist (lõigake rakenduse riba), mis on tsentreeritud, nii et 15-16-tollisel sülearvutil, mille suurus on 125% või 150%, jääb pealkirjariba nähtavaks. Kui kasulik ruum on väiksem kui 1280 × 720 punkti, maksimeeritakse aknaosa. Suurus, asukoht ja maksimaalne staatus salvestatakse LIVETROPOVHF-is. ini sulgemisel ja lõpus järgmisel käivitamisel, eeldusel, et tiitliriba langeb ekraanile (näiteks pärast välise monitori eemaldamist); vastasel juhul naasete 90%-ni. Minimaalne suurus on 900 × 560.

Programm on kaasaskantav: ei kirjuta kunagi AppData kausta ega registrisse. Kõik on käivitatava faili kaustas: LIVETROPOVHF.ini (komplektid, loodud esimesel sulgemisel), C ALL3.TXT (käsitsi kirjutatud häältega, õpitud parandustega ja lokaatoritega), kaardi vahemälu ja ilmaandmed, õpitud reeglid (Dati/APPRENDIMENTO.TXT) ja LIVETROPOVHF.db arhiiv. Saate seda hoida ka USB-pulgal, kui kaust on kirjutatav.

Reklaamiarhiiv (SQLite [14]). Iga aktsepteeritud koht ja iga PSKReporteri kuulamisaruanne salvestatakse ühes failis asuvasse SQLite andmebaasi LIVETROPOVHF.db. Mälestuseks jäävad nimekirja ja kaardi jaoks alles vaid viimased 60 minutit; arhiiv säilitab kõik ja teenib ajaloolase ja statistika (ka UDP dekodeerimised), näiteks avad bändile ja päevale, maksimaalsed vahemaad, saavutatud ruudud ja tagumine võrdlus oodatava indeksi ja tegelike täppide vahel. Filtrist välja visatud read loetakse, kuid neid ei arhiveerita.

## 12 bis. Automaatne paigaldamine ja uuendamine

Alates versioonist 1.0 on programm juba alla laadinud github.com/ic8tem/LIVETROPOVHF vabastamise lehelt samades YA-MSi pakettides: Windowsi installer (64 bitti, 32 bitti ja ARM, installib C:\LIVETROPOVHF ilma administraatoriõigusteta) ja kaasaskantav zip; Windows 7 SP1 ja 8.1 paketid peale (-windows7-x64 ja -wind.deb,.rpm, Arch, Slackware, AppImage ja tar.gz Linuxi jaoks; paketid Raspberry Pi (64 ja 32 bitti) ja Alpine;. Linuxi pakettidega kopeeritakse programm esimest korda ~ / LIVETROPOVHF-is, nii et seaded ja andmed jäävad programmi kõrvale; Maci andmed sünnivad LIVETROPOVHF.app'i kõrval.

Iga 30 minuti järel (esimene kontroll paar sekundit pärast käivitamist, kohe, kui programm oli viimati vea tõttu suletud) loeb programm üksi, ilma midagi küsimata, viimast väljaannet. Kui versioon on uuem, laadige alla süsteemile sobiv kaasaskantav pakett ja asendage kohapeal programmifailid: kasutusel olevad failid nimetatakse ümber *.old-update ja kustutatakse järgmisel taaskäivitamisel. Seadistused, arhiiv, andmekataloog ja C ALL3. TXT-d ei puututa. Uus versioon on aktiivne alates järgmisest käivitamisest: installi lõpetamisel pakub aken kohe taaskäivitamist; kiireloomuliste paranduste korral, mis on märgitud [kohustuslik] vabastamisel, ei saa taaskäivitamist edasi lükata. Kontrolli olek ja kasutatav versioon kirjutatakse olekuriba allosas; värskenduste väljalülitamiseks=0 punktis LIVETROPOVHF.ini. Macis on rakendus allkirjastatud ja ei muutu ise: programm teatab ainult, et on olemas uus versioon.

LIVETROPOVHF-pilt koos versiooninumbriga ilmub alguses; lõpus palub programm kinnitust ja tänu kasutamise eest.

## 12 ter. Vealogi ja olekuriba

Kõik ettenägematud vead lõpevad Data/errori.log'is, isegi arvutuse väljalogimisega: kuupäev ja kellaaeg, versioon, süsteem, avatud vahekaart, täielik viga ja viimased read, mida programm tegi. Liidese viga ei sulge enam programmi: olekuribal ilmub "annoteeritud veata" (klõpsa: ava register). Järgmisel käivitamisel küsib programm, kas saata aruanne: avab brauseri juba kompileeritud GitHubi signaalil, kus on eemaldatud arvuti nimetamine, lokaator ja kasutajanimi, mida kontrollite ja saadate tasuta GitHubi kontoga. Logi lõpeb ka iga uuenduskontrolliga.

Olekuribal klikitakse ja tuuakse lähtepallid (DX cluster, RBN, PSKReporter, MQTT, UDP); koomiks ütleb, miks allikas on välja lülitatud: tagasilükatud port, klastrisõlm serveri poolel, ei reageerita või UDP kuulamine, kuid WSJT-X, JTDX või MSHV pakette ei ole. Üks klõps versiooni numbril kontrollib kohe värskendusi.

## 13. Platvormid

Välised raamatukapid: Avalonia [12] liidese jaoks, MQTTnet kui MQTT klient PSKReporter [13], Microsoft reaalajas voo jaoks. Data.Sqlite [14] arhiivi jaoks.

C# on.NET 10 (LTS) [15] koos Avalonia liidesega, avaldatud iseseisva käivitatavana Windows 64 ja 32 biti, Mac Inteli ja Apple Silicon'i, Linux Debian'i ja Raspberry Pi jaoks (64 ja 32 bitti). Märkimispiirid: Windows 10/11; macOS 14 või uuem, rakendustega allkirjastamiseks või avamiseks paremale → Open; Vaarika alates Pi 2 / Zero 2 W up (ARMv6 mudeleid ei toeta.NET).

## Tänan.
Tänu I5MZY Massimo Zannonile Windows 7 ja Windows 8 testimise eest.

Õhukaitsed: ITU-R P.453, uurimisrühma 3 (© ITU) digitaalsed tooted, mida kasutatakse isiklikel ja mitteärilistel eesmärkidel.

## Peamised allikad

1. S. Heck LA0BY, „Extreme Tropo Propagation on 144 MHz and up, esitlus 41. Nordic VUSHF Meetingil, Skjeberg 2019. Tropoferical režiimid, kontrollnimekiri sündmuste jaoks (Hepburn / F5LEN prognoos, kõrge temperatuuri profiilid Windyga, DX klaster kui avanemise tõend), tähelepanek, et ainult prognoositavad kaardid ei ole äärmuslike marsruutide jaoks piisavad.
2. J. N. Gannaway G3YGF, „Tropospheric Scatter Propagation, QST November 1983 (samanimelisest artiklist raadioside kohta, RSGB, august 1981). Vaba ruumi kao ja hajumise valemid, ekvivalentraadius 8497 km, koha mõiste 10 dB horisondi kohta, kliima mõiste 0,2 · (N − 310), seadme võimsus ja signaali/müra suhe, näiteks 144 MHz juures.
3. W. R. Hepburn, DX Infokeskus, „Tropospheric Ducting Forecast (dxinfocentre.com/tropo.html) ja „The Hepburn Tropo Index (HTI). Indeksi skaala ja väärtuse tähendus, prognoos 6 päeva kohta.
4. F5LEN, Prognoos Euroopa jaoks (tropo.f5len.org/forecasts-for-European: piltide loend lehekülje skriptis, klassilegend Häire...) Kanal pildil) ja maailmaprognoos (tropo.f5len.org/WW) ning märkus „Maailma VHF troposfääri leviku prognoosimise vahendi uus versioon. Reaktiivsuse indeks ühikus N, 40 sammu alates 3 tunnist, värskenda 4 korda päevas.
5. mqtt.pskreporter.info, levitamine MQTT reaalajas reklaamid pskreporter.info (kureerib M0LTE, voo P. Gladstone kasutatakse loal). Uksed 1883–1886, pskr/filter/v2 teemaskeem.
6. Projekti kokkuvõte 8/11/2026 ja Go test (core.go, gui.go), mille on esitanud programmi autor. Reklaamireeglid, negatiivne filter, ribalaiuse segmendid, televõrgu klastrite haldamine, teemad, keeled ja kaasaskantavus.
7. C ALL3.TXT, nimetamisandmebaas → JTDX landlord (I. UA3DJY, A. ES1JA script, JTAlert/hamspots.net data). Projektifailis kontrollitud nimede failivorming ja arv.
8. WSJT-X, lähtekood, NetworkMessage.hpp fail (UDP protokolli kirjeldus: Heartbeat, Status, Decode). Üldine viide, lisamata; JTDX ja MSHV kasutavad sama vormingut.
9. Reverse Beacon Network (reversebeacon.net), telnet-sõlme teenuselehed. Ports 7000 (CW, RTTY) ja 7001 ( FT8), sisselogimine ainult nimega.
10. D. Mills, RFC 4330 „Simple Network Time Protocol (SNTP) Version 4, IETF 2006. Pakendi vorming, jäätmete arvestus ja tagastamise tähtaeg. Üldine viide, ei ole lisatud.
11. NTP Pool Project (ntppool.org); INRIM, NTP time service (ntp1/ntp2.inrim.it); PTB (ptbtime1.ptb.de). Serverid, mida kasutatakse aja juhtimiseks. Üldine viide, ei ole lisatud.
12. Avalonia UI, ametlik dokumentatsioon (docs.avaloniaui.net). Mitmeplatvormiline liides, teemad ja paigutus paremalt vasakule. Üldine viide, ei ole lisatud.
13. MQTTnet,.NET teek MQTT jaoks (github.com/dotnet/MQTTnet). PSKReporteri vooks kasutatud klient. Üldine viide, ei ole lisatud.
14. SQLite (sqlite.org) ja Microsoft. Data.Sqlite (learn.microsoft.com). Arhiiv ühes failis. Üldine viide, ei ole lisatud.
15. Microsoft,.NET 10 – toetatud operatsioonisüsteemid ja autonoomne kirjastamine ühes failis (learn.microsoft.com/dotnet). Punktis 13 nimetatud platvormid ja piirangud. Üldine viide, ei ole lisatud.
16. ECMA-376 “Office Open XML File Formats”, osa 1, lõik 22.1 (Office Math Markup Language)..docx faili struktuur ja võrrandid. Üldine viide, ei ole lisatud.
17. YA-MS viki 28 keele loend, mille on esitanud programmi autor.
18. Open-Meteo, ilmaprognoosi API (open-meteo.com) koos NOAA GFS mudeli andmetega. Rõhk merepinnal isoglükoosi suhtes (vt dokument 2).
19. J. P. Snyder, “Map Projections – A Working Manual”, USGS Professional Paper 1395, 1987, and standard polynomial transform methods for georeferencing images. Üldine viide, ei ole lisatud.
20. Astronoomiline almanahh, D jagu, "Kuu madala täpsusega valemid" (täpsusasend umbes 0,3°). Kasutatakse EME ühenduste äratundmiseks. Üldine viide, ei ole lisatud.
