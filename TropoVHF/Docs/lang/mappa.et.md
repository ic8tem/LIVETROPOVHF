LIVETROPOVH · tehniline dokument nr 2
# Kaardi koostamine

> Automaatne tõlge itaalia keelest: kahtluse korral on see väärt Itaalia juhendit.

Nagu kaardi kaart kaart kaart ja kohapeal on sündinud: geograafiline alus, projektsioonid, keskmine Hepburn ja F5LEN indeksid, isobare, laigud ja jaamad kuulates, kattuvad kihtidega. 9. novembri 2026. aasta versioon.

## Kokkuvõttes

- Kaart on kujundatud programmi poolt kaheksa kihina, millest igaühel on [8] kast.
- Alus: Maa looduslik rannajoon ja piirid [9] detaili kahel tasandil; [10] turg ja asimuudi projektsioonid.
- Füüsika: inversioon, reaktiivsus N ja gradient dN/dh, kanali lävi −157 N/km [3] [4] [7] [11].
- Kurguindeks: Hepburn [1] ja F5LEN [2] kaartide kalibreeritud lugemine, ühine ruutvõrk ja kahe allika keskmine.
- Isobare: GFS [12] rõhk avatud Meteo [15] kaudu, jooned iga 4 hPa, H ja L tsentri kohta.
- Laik, kuulamisjaamad (pimedad roosad ringid alati nähtavate roosade joontega) ja punased isiklikud dekodeerimised.
- Katte lugemine: kõrge rõhk, kõrged kanalid, rõhu langus, kasvav SNR, [3] [4] [5] [13] [14] mägiteed.

## 1. Põhimõte: kihiline kaart

Kaart ei ole fikseeritud pilt: seda kujundab programm, kiht kiht kihis, kui muudate suum, projektsioon, riba, prognoosimisaeg või teema. Igal kihil on kaardil ülemise kihi baaris oma kast, nii et saate hoida ainult seda, mida vajate, ja näha kõike koos, kui soovite täielikku pilti. Järjekord altpoolt on selline:

|  #  | Road | Sisu | Casella |
|  1  | Põhikaart | Meri, maad, rannikud ja piirid | Jah. |
|  2  | Indekstropo | Meedia Hepburn + F5LEN või ainult üks allikas; reguleeritav läbipaistvus | Jah. |
|  3  | Grilli asukoht | Põllud, väljakud ja suurendusega ruudud | Jah. |
|  4  | Isobare | Rõhuliinid iga 4 hPa, H ja L keskpunkti kohta | Jah. |
|  4a  | Temperatuur 850 hPa | Isotermilist ravitakse iga 4 °C | Jah. |
|  4b  | Termiline inversioon | Oranž proov, kus õhu kõrgus on soojem kui maapinnal | Jah. |
|  5  | PSKReporter | PSKReporteri vastuvõtujaamad (tumedad roosad ringid) ja roosa, mida nad kuulevad | Jah. |
|  6  | Minu poolt dekodeeritud | Dekodeerimisjaamad WSJT-X, JTDX või MSHV-ga: punased jooned teie QTH-st | Jah. |
|  6a  | QSO desunti dal live | Suletud ühendused ( RR73, 73, RRR) loetakse dekodeerimisel: roheline joon kahe jaama vahel | Jah. |
|  7  | Spotlight | Ühendused: täis sinine joon, kollane kaetud õhuhajumiseks, kaks palli | Jah. |
|  7a  | Välja arvatud | Casella Välja arvatud alla 100 km: lühim liiklus ei ole loositud ega loetletud. Casella Üle 1000 km: ainult väljakud (käivitamisel): kui üks jaamadest asub QTH-st rohkem kui 1000 km kaugusel, ei ole jooni ega ringe, ainult sireliga seotud jaamade asukoharuududud, mida töödeldakse 45° juures | Jah. |
|  8  | QTH ja märgised | Sinu jaam, valitud koha nimed | nähtav |

Tropo indeks on kohe aluse kohal ja kõige muu all: on värviline proovivõtt ja kõrgem asend hõlmaks jooni ja sümboleid. Võrk läheb üle selle, nii et sa tead alati, kus värviline ala langeb. Isobaarid on õhukesed jooned ja loevad hästi proovi kohal.

## 2. Põhikaart

Kulud, piirid ja maa tulevad looduslikust Maa vektori andmetest, mis on avalikus omandis [9], skaalal 1:50 miljonit üldvaate ja 1:10 miljonit suurenduse korral. Failid on programmi kaustas: online-kaarditeenused puuduvad. Teema värvid Selge: helesinine meri # E2ECF8, tumesinine rannajoon, helehallid ääred. Teemades Must ja Öö on aluse värvid tumenenud, teistes teemades värvivad nad veidi.

## 3. Kaks prognoosi

Kõik kihid läbivad sama projektsioonifunktsiooni, nii et nad on alati üksteisega joondatud. Valige kahe [10] projektsiooni vahel (λ pikkuskraad, φ laiuskraad, Maa R-kiir):

Turg. Kohalike nurkade ja kujundite säilitamine; Kaugused ulatuvad pooluste suunas. Lõikatakse ±85 laiuskraadini.

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

Azimutale equidtanti keskendus teie väljakule. Keskpunkt (φ0, λ0) on teie WWL-ruudu keskpunkt. Keskusest mõõdetud kaugused ja suunad on tõesed: 1200 km kõrgusel asuv jaam on mõõtkavas 1200 km ja nurk põhjaga on asimuut, milleni antenn jõuab. Lokaatori vahetamisel suunatakse kaart ümber.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

Asimuudis on paberil olevad jooned sirged (meridaanid, paralleelid, ruudukujulised servad) muutuvad kõveraks. Sel põhjusel jagatakse kõik jooned lühikesteks lõikudeks, iga lõik projitseeritakse ja seejärel tunnused ühendatakse; sama kehtib tropo indeksi servade ja isobare kohta. Keskme antipoodide punkt ei ole kindlas asendis ja jääb vaateväljast välja.

## 4. Grillimiskoht, suurenda ja liiguta

Maidenheadi võrk on projekteeritud programmi järgi: väljad 20° × 10°, mille keskel on kaks suurt ja tuhmunud tähte, ruudud 2° × 1st ja piisav suum 5′ × 2,5′. Jooned on sinised ja õhukesed, et mitte katta tropo indeksit.

Suurenda, kui ratas on suunatud osutile (hiire all olev punkt jääb stabiilseks), lohistamine, topeltklõps kogu vaatele naasmiseks, + ja − klahvid ning võti kogu vaate protsendiga. Suumile sobivad detailid: peenemad rannikud, alamruududud, paksemad isobare sildid.

## 5. Miks inversioon painutab laineid

Madalatmosfääris langeb temperatuur tavaliselt umbes 6,5 °C kilomeetri kohta; murdumisnäitaja sõltub nii temperatuurist kui ka [3] niiskusest ning kui see kõrgusega järsult muutub, paindub laine maapinnale ja jõuab tavapärasest kaugemale. Kõige olulisem kiht on see, et kuni umbes 3 km, kus moodustuvad inversioonid, kanalid ja konvektiivsed rakud [4]; mereteed on eelistatud, sest veemassid moodustavad [4] inversiooni suured alad. [3]

Selle mõõtmiseks kasutatav suurus on murduv N, st (n − 1) · 106 [11]:

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

T on temperatuur kelvinites, rõhk P ja aururõhk, mõlemad hPa-des.

Loendatakse erinevaks N-iks osaga (dN/dh). Standardatmosfääris on refraktsioon ligikaudu −39 N/km; kuni −79 N/km peetakse refraktsiooni normaalseks, lisaks superrefraktsioonile ja positiivsete alarefraktsiooni gradiendidega [7]. Alla −157 N/km on laine painutatud rohkem kui Maa kõverus ja jääb lõksu: see on [11] (juhtiv) kanal. F5LEN

## 6. Tropo indeks: kahest kaardist keskmiseni

Hepburni ja F5LEN vahekaardid on juhtlaud: näidatakse iga allika leheküljelt allalaaditud pilte koos redigeeritava prognoosimisajaga ning kalibreeritakse lugemist, klõpsates legendi viitepunktidele ja värvidele. Tropo indeksikiht jääb tühjaks, kuni vähemalt üks kahest kalibreerimisest ei ole aktiivne ja täielik.

Hepburn. Hepburni indeks mõõdab, kui palju laineid on oodata, et volditakse üle ala, lineaarsel skaalal 0 kuni 10, mis võib samuti ületada 10. 0 tähendab standardset atmosfääri ja ainult hajumist; 1–2 marginaalset või nõrka ülirefraktsiooni; 3–4 ülirefraktsiooni kanalipindadega; Indeks ei arvesta hajutropoga ega näe [1] mägesid. Kaardid hõlmavad 6 päeva: 3-tunnine läbib esimese 36 tunni, seejärel 6 tundi, uuendatakse iga päev 18 UTC [1]; algandmed (temperatuur, niiskus, rõhk) pärinevad Kanada ilmateenistuse [1] globaalsest mudelist. [1]

F5LEN. Euroopa jaoks avaldab F5LEN 31 kaarti vahemikus +3 kuni +192 tundi (iga 3 tunni järel kuni +48, iga 6 kuni +96, seejärel iga 12), mille kuupäev ja kellaaeg kehtivad pealkirjas, tootmisaeg all paremal ja klassi legend: Häire, Normaalne, Väike, Nõrk, Hea, Tugev, Vy Strong, Duct [2]. F5LEN maailmaprognoos arvutatakse õhu murdumise indeksi abil üksuses N, 40 sammul 3 tundi (umbes 5 päeva), mida ajakohastatakse 4 korda päevas; klõps kaardil näitab selle punkti [2] keskmist väärtust ja lokaatorit.

Jätkan. Iga ennustusetapi puhul teeb programm järgmised sammud:

1. Tühjenda. Kahe prognoosi pildid laaditakse alla vahemälus käivitatava faili kõrval, kui värskendada lähtekoodi, mitte ümber kujundada. Enne lõplikku versiooni tuleb taotleda luba Hepburnile ja F5LEN -le.
2. Automaatne georefereerimine. Programm leiab raami (kaks horisontaalset ja kaks sama värvi vertikaalset joont) ja Windows 10/11-l loeb süsteemi integreeritud OCR-iga telje sildid (54N, 10W, 0, 5E...). Lõigake ribad raami ümber, laiendage neid, testige mõlemat märki, kui täht N / S / E / W ei loe ja hoidke joont, millele langeb kõige rohkem silte: valed näidud jäävad välja. Siit saate raami äärmise lat / loni ja projektsiooni tüübi (tavaline grill või Mercator). Äärmused päästavad ennast ja loevad end uuesti, kui raami suurus muutub. Macil ja Linuxil, ilma OCR-ita, kehtivad vahekaardil tehase äärmused või käsitsi kirjutatud. Teise võimalusena kaartide puhul, millel ei ole loetavaid telgi: Igal kaarditüübil (Hepburn Europa, F5LEN üle maailma...) on kaliibrimine: mõned teadaolevate koordinaatidega kontrollpunktid (meridiaanide ja paralleelide ristid, äratuntavad ribid) ühendavad pikslid laius- ja pikkuskraadidega, selle allika kasutatavas projektsioonis. Kalibreerimine on programmi kõrval asuvas failis ja saate uuesti teha, kui allikas muudab vormingut.
3. Värvinäide. Iga pikslit võrreldakse lähtelegendiga ja teisendatakse lähimaks numbriliseks väärtuseks. Esialgsel kaardil joonistatud kulud, kirjutised ja võrgud tunnistatakse "andmeteta" ja täidetakse lähedal asuvate väärtustega.
4. Maailmavõrk. Iga pilt, kui raam on leitud ja teada selle äärmused, asetatakse Maale; väärtus igas maailma punktis tuleneb seda katvatest piltidest. Kui sama punkti katab rohkem piirkondi, on see väärt üksikasju (rohkem piksleid kraadi kohta), kui selle aeg on 1,5 tunni jooksul sellest valikust; siis lähim tund, siis viimane võistlus. Kui ükski pilt ei kata punkti, näitab kaart halli loori: andmed puuduvad, mis erineb indeksist 0. Kaadri sees on legendi värvita piksel 0 (normaalne atmosfäär); rannikuid, piire ja kirjutisi hüpatakse, vaadates lähimat värvi 3 piksli piires.
5. Sama redel. Hepburn on juba skaalal 0–10 [1]: tume taust = 0 ja kümme värvi järjekorras (violetne, sinine, vesiroheline, lubi, kollane, ocher, oranž, punane, roosa, heleroosa) 1...10, sirel kaugemale. F5LEN kasutab klasse (häiriv, normaalne, väike, nõrk, hea, tugev, tugev, tugev, tugev) [2], kuid mõnedel on täpselt Hepburni värvid: Nõrk = 1, tugev = 5, Vy Tugev = 7, kalts = 8; Väike on 0,5 ja Hea (roheline) 3,5. Skaala kordaja F5LEN kaardil korrigeerib komplekti, et see oleks võrreldav Hepburniga.
6. Samal ajal. Ajavahemik on +0 kuni +192 tundi 3-tunniste sammudega. Iga allika iga sammu kohta prognoositakse lähimat aega; Kui allikal seda sammu ei ole, kasutab meedia ainult teist ja legend näitab seda.
7. Meedia. Meedia nupuga on iga punkt väärt (Hepburn + F5LEN) / 2; Hepburni või F5LEN -ga näete ainult ühte allikat, mis on kasulik mõista, kus kaks prognoosi ei nõustu.
8. Joonistamine. Väärtus on värvitud legendi skaalaga (0 sinine, 2 roheline, 4 kollane, 6 oranž, 8 punane, 10 lilla) ja projitseeritakse võre all. 0 on läbipaistev, nii et kui midagi ei juhtu, näete puhast põhikaarti; üldine läbipaistvus kohandub kursoriga kasti kõrval.

Kohakaardil on marsruudi indeks liiga võrgu väärtuste keskmine iga 10 km järel kahe jaama vahelisel maksimaalsel ringil.

## 7. Isobare

Kõrge stabiilne rõhk on esimene signaal, mida otsitakse [3] trope jaoks, ja IK3XTV analüüsitud Alpidest väljapoole jäävad ühendused on olnud ka kõrge [4] rõhuga.

1. Andmed. Rõhk merepinna tasemel NOAA [12] globaalse mudeli GFS järgi, loetuna vaba Open Meteo [15] teenuse kaudu 3° × 3° (30° kuni 72°) võrgus N, 30° W kuni 48° E, järgmised 78 tundi. Programm uuendab andmeid käivitamisel ja seejärel iga 6 tunni järel, nagu mudeli võidusõit.
2. Liinid. Isobare tuleb võrest iga 4 hPa (...1012, 1016, 1020...) marsivate ruutude algoritmiga (marsruududud), siis on need veidi ümarad ja projitseeritud traktiga.
3. Keskused. Kõige tuntumad kohalikud maksimumid ja miinimumid on tähistatud sümbolitega H (sinine) ja L (punane) ning väärtusega hPa.
4. Sildid. Väärtus kuvatakse piki iga isobarat, kaardi värvitaustal, et see jääks loetavaks valimi kohal.

Tsoon ja samm. Andmed laaditakse alla alale, mida te vaatate, laiendatakse 25% võrra, kusjuures samm 1. kuni 20° on valitud jääma alla 400 punkti (Open-Meteo loeb iga punkti taotlusena: 600 minutis, 5000 tunnis): kaardi suurendamine või liigutamine piirkonnast juba laadib programmi kõige tihedamini alla, maksimaalselt iga 10 minuti järel; kui värskendus ebaõnnestub, jäävad eelmised andmed alles ja proovitakse uuesti 3 minuti pärast. Nii töötavad isobare ja temperatuurid kõikjal maailmas.

Temperatuur. Sama palve toob temperatuuri 2 m, 925 hPa ja 850 hPa. LA0BY näitab, et sündmus 22/11/2018 kuni UA6 ilmnes temperatuurikaartidel (900–3000 m), kuid mitte Hepburn [3] ennustustes; DF5AI ja DK5YA kasutavad vertikaalseid temperatuuriprofiile [5] inversioonide tuvastamiseks. Selleks on kaks kihti:

- Temperatuur 850 hPa: isotermtöötlus iga 4 °C järel (punane üle nulli, sinine alla, lilla nulli peal).
- Termiline inversioon: oranž proovivõtt, kus õhk on 925 või 850 hPa juures soojem kui 2 m juures; mida intensiivsem, seda tugevam on inversioon. See on füüsiline seisund, mis painutab laineid maapinnale [3] [11] ja võrdleb seda tropo indeksiga ja isobare näitab, kas prognoosidel on ilmabaas.

## 8. Täpid ja jaamad

Punkt. Iga mõlema teadaoleva asukohaga täpp on joon kahe jaama vahelisel maksimaalsel ringjoonel, mis on jagatud lõikudeks ja projitseeritud; Mercatore'is on see siis veidi kõverdunud, asimuutalis on see sirge, kui see algab keskelt. Sinine joon täis tavalise koha jaoks, kollane kaetud õhuhajumise tumeda servaga (<AS>, <ACS> või "õhuhajumine" kommentaaris), pall iga jaama jaoks, hele halo joone all, et eemaldada see proovivõtust. Valitud koht on sagedamini mõeldud viimasele ja kahe nimega.

Kuulan. Iga PSKReporter [16] vastuvõtjajaam on tumeroosa tühi ring. Igast stardist täielikud roosad jooned jaamade suunas, mis on dekodeeritud ja saadetud tagasi võrku, alati nähtavad ilma klõpsamata, kusjuures nimed on alati kirjutatud. Valides ringi, on selle jooned paksemad. Spot ja kuulamine näitavad ainult loendis valitud riba ja vahemikku: ribalaiuse muutmine kaardi tühjeneb ja uuendab uue bändi jaoks juba kogutud andmeid.

Otsustasin ise. Iga jaam, mis dekodeerib WSJT-X-i, JTDX-i või MSHV-ga (vastuvõtt UDP kaudu), on teie QTH-st jaama täis punane joon, kus on pall ja nominatsioon. See on kõige otsesem võrdlus prognoosiga: näitab, kuhu teie vastuvõtja tegelikult praegu tuleb.

## 8 bis. Värvid ja sümbolid

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

Millised on sireliruududud, mis on kaetud 45° juures. Teie QTH-st eemal olev liiklus huvitab teid, et mõistaksite, kus levik on avatud, kuid selle read, mis on sadu kilomeetreid pikad ja sageli kümned, katavad kaardi ja peidavad, mis teie jaoks loeb: avad, mida saate oma jaamast ära kasutada. Sel põhjusel ei joonista programm valitud kroonist kaugemale jooni, vaid värvib kaasatud jaamade lokaatoriruudu (2° × 1°). Tulemuseks on "aktiivsete tsoonide" kaart: kus ilmuvad paljud lähedal asuvad sireliruududud, on kanal või ava, isegi kui see teid otseselt ei puuduta; Kui ala laieneb teie suunas, on see signaal antenni suunamiseks selles suunas. 45° armatuurlaud võimaldab näha tropoindeksi ja võrgu all, nii et nad võrdlevad lühidalt prognoosi (alumine värv) ja tegelikku liiklust (lilla). Teie QTH ruut ei värvi kunagi, sest see on lähtepunkt, mitte kauge ala. Värvilised on ainult kroonitagused ruudud: krooniga ristuv või sees olev ruut jääb vabaks isegi siis, kui on üks kahest jaamast. Ridade nägemiseks lülitage lihtsalt kasti välja või valige suurem kroon.

Kriteeriumid, mis otsustavad, mis kaardile tuleb ja mis kõrvale jäetakse, on selgitatud ükshaaval dokumendi 1 jaotises 11 ter.

## 9. Ajariba

Allpool kaardil: ennustuse kuupäev ja kellaaeg, kursor +0 kuni +72 tundi 6 tunni pärast, indeksilegend 0–10 valitud allika nimega ja mudelisõidu aeg isobare jaoks. Trumpet ja isobare indeks järgivad kursorit; kohad, kuulamisjaamad ja teie dekodeerimised on hetke vaatlused ja jäävad alati nendeks "adex". Kui kursor ei ole +0-s, on need nõrgenenud, et meeles pidada, et need ei kuulu prognoositud aja hulka.

## 10. Kuidas lugeda kattuvust

- Kõrge rõhk + kõrge indeks + täpp piki värvilist ala: avamine on olemas ja on planeeritud. On aeg selles suunas helistada.
- Kõrge indeks, kuid mitte koht: või keegi ei ole seal aktiivne või prognoos on liiga optimistlik. Kuulamiskiht aitab eristada: kui piirkonnas on vastuvõtjaid ja ei tunne midagi, on teine hüpotees tõenäolisem.
- Pikad laigud väljaspool värvilisi alasid: kõrge kanalisatsioon, mida pinnaprognoosid ei näe, nagu LA- UA6 [3] sündmusel; või muu levik pääses filtrist välja (nt E sporaadiline ilma sildita). DF5AI ja DK5YA eristavad tropot ja E sporaadilist ka tunnist: avad, mis sünnivad ja lõpevad koos hajusa E sporaadiga, on vaevalt [5] tropo.
- Hepburn ja F5LEN ei nõustu: võrrelda kahte allikat nuppudega ja vaadake, kus tegelikud kohad langevad.
- Rõhk alla: G4MCU järgnes SK6VHF majaka avanemisele, kuni rõhk langes; 1012 hPa pidas lõppsündmuseks [13]. Vaata isobare kerides ajariba: kui kõrge rõhk on välja lülitatud, siis tõenäoliselt järgneb sellele ava.
- Signaalid, mis tõusevad: WSPR stuudios 144 MHz KP4MD signaali / müra suhe kasvas otse akendes, kus Hepburn ennustas paranemist, ja langes, kui prognoos eemaldas [14]. Kui samal teel kasvab, moodustub avaus.
- Mäeteed: indeks ei võta arvesse [1] reljeefi, kuid Alpidest väljapoole jäävad ühendused eksisteerivad ka tänu konvektiivsetele rakkudele ja atmosfääri gravitatsiooniga [4] lainetele ning LA0BY töötas 9A ja S5 läbi mägede, ilma et Hepburn ennustaks seda [3].

## 11. Avatud punkt

- Hepburni ja F5LEN luba oma kaartide kasutamiseks programmis.
- Teisendustegurite kalibreerimine F5LEN → skaala 0–10.
- Mis tahes kaardi kontrollpunktide kalibreerimine, mis tuleb uuesti teha, kui allikad muudavad graafikat.
- Valikuline marsruut temperatuuriga GFSist (850 hPa) kõrgusel.

## Peamised allikad

1. W. R. Hepburn, DX Infokeskus, "Hepburn Tropo Index (HTI)" ja "Tropospheric Ducting Forecast" leheküljed (dxinfocentre.com). Indeksi määratlus ja skaala, negatiivsed väärtused ja rohkem kui 10, hajumise ja reljeefide väljajätmine, 3 ja 6 tunni sammud 6 päeva jooksul, uuendamine 18 UTC-ni. Kanada globaalse mudeli andmete päritolu: G. Meisse, AMSAT-BB, 27/08/2007.
2. F5LEN, Prognoos Euroopa jaoks (tropo.f5len.org/forecasts-for-europe) ja VHF troposfääri levimise maailmaennustus (tropo.f5len.org/WW) ning märkis „VHF troposfääri levimise prognoosimise vahendi uus versioon. Ühiku indeks N, 40 sammu alates 3 tunnist, 4 värskendust päevas, väärtus ja lokaator kliki kohta.
3. S. Heck LA0BY, „Extreme Tropo Propagation on 144 MHz and up, 41° Nordic VUSHF Meeting, Skjeberg 2019. Normaalne gradient 6,5 °C/km ja inversioonid kuni 10–15 °C/km, temperatuur ja niiskus, kõrge stabiilne rõhk, LA– UA6 sündmus, mis on nähtav kõrgel temperatuuril ja mitte Hepburnis, LA–9A/ S5 ühendused mägede taga.
4. F. Egano IK3XTV, „Propagation VHF – The contribution of convective cells and atmospheric gravitational waves in tropo propagation. Esimese 3 km tähtsus, mereteed, konvektiivrakud, gravitatsioonilained Alpidel, kõrgsurveühendused, ühine maht kuni 3–4 km.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, “Very Long Distance Propagation in the 144 MHz Band”, 24/7/2003. Temperatuuri ja kastepunkti vertikaalsed profiilid inversioonide äratundmiseks, Hepburni indeksi kasutamine, tropo ja E sporaadiline eristamine tunni järgi.
6. J. N. Gannaway G3YGF, „Tropospheric Scatter Propagation, QST November 1983. Maaraadiuse ekvivalent ning indeksi- ja kohakaardi puhul kasutatud teekadu (vt dokument 1).
7. The VHF/UHF DX Book, peatükk 2 „Tropospheric propagation, lk 2-23...2-30. Murdumistsoonide arv vastavalt dN/dh (standard −39 N/km, piirid −79 N/km ja 0) ning pinna- ja kõrge kanali profiilid. Esitatud koopia tekst ei ole loetav: kasutati ainult numbreid.
8. Projekti kokkuvõte 8/11/2026, mille on esitanud programmi autor. Kaardi värvid, vajalikud projektsioonid, kihid, ajariba.
9. Looduslik Maa, rannikute ja piiride vektorandmed, avalik omand (naturalearthdata.com). Üldine viide, ei ole lisatud.
10. J. P. Snyder, “Map Projections – A Working Manual”, USGS Professional Paper 1395, 1987. Mercatori projektsioonide ja asimuutale equidtanti valemid. Üldine viide, ei ole lisatud.
11. Soovitus ITU-R P.453, raadio murdumisnäitaja. Murdumisvalem N ja kanali lävi −157 N/km. Üldine viide, ei ole lisatud.
12. NOAA NCEP, Global Forecast System (GFS). Surve merepinnal 0,25°, neli võistlust päevas. Üldine viide, ei ole lisatud.
13. G4MCU, artiklid sildiga „troposfääriline (g4mcu.net/tag/troposfääriline), 2018–2025. Külmutamine, kanal ja hajumine; sagedasemad kõrge rõhuga kanalid, eriti merel; majakas SK6VHF järgnes rõhu langusele 1012 hPa juures (juuni 2019).
14. C. F. Milazzo KP4MD, „VHF/UHF WSPR Propagation Study (qsl.net/kp4md/144_mhz_wspr.htm), 2012–2014. WSPR-i SNR teatab 144 MHz-le, et Hepburni ennustatud parandusaknad kasvavad ja langevad, kui prognoos kaob.
15. Avatud Meteo, ilmaprognoosi API (open-meteo.com), tasuta mitteäriline kasutamine. GFS-mudeli surve merepinna tasandil mitmele punktile ühes taotluses.
16. mqtt.pskreporter.info, levitamine MQTT reaalajas reklaamid pskreporter. teave: abisaajate nimetamine ja üürileandja (vt dokument 1).
