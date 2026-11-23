LIVETROPOVHF · műszaki dokumentum 1
# A program működése

> Gépi fordítás olaszból: kétség esetén az olasz útmutató az irányadó.

Mit csinál a program, honnan veszi az adatokat, és hogyan alakítja át őket azzá, amit a térképen és a listán látunk. A C # (.NET 10 + Avalonia) projekthez kapcsolódó, 2026. november 9-i változat.

## Összefoglalva:

- Cél: tropo előrejelzés (Hepburn [3], F5LEN [4], isobare GFS [18]) és a sávok valós megtekintése ugyanabban az ablakban, az LA0BY [1] ellenőrző lista szerint.
- Adatgyűjtés: két DX klaszter, fordított Beacon Network [9], dm4x cluster és PSKReport [5] MQTT áramlás, UDP dekódolás WSJT-X, JTDX és MSHV [8].
- Spot: sávszegmensek, negatív szűrő, repedezett sárga, pozíció lokátorok vagy CALL3.TXT [6] [7].
- Ki figyel: sötét rózsaszín körök rózsaszín vonalak mindig látható felé állomások érezte [5].
- Spot kártya: távolság, azimut, tropo index az útvonalon és troposcatter veszteség az G3YGF [2] módszerrel.
- Nyelvek Hepburn és F5LEN: [3] kép és kalibráló pad [4].
- Figyelem: NTP vezérlés 10 szerveren 3 percenként [10] [11].
- Gyanúsított jelentések: az EME Q65/JT65 -ban a Hold helyzetével, csonkított lokátorokkal, simmerekkel, karanténokkal és hallgatókkal, a Sun [20] szabályaival ismerkedett meg.
- A két táblázatban (11. és 11. szakasz) összegzett színek visszadobására és jelentésére vonatkozó kritériumok.
- Temze, 36 [17] nyelvek, export dokumentumok Word szerkeszthető képletek [16], hordozható program és multiplatform [12] [13] [14] [15].

## 1. Cél

A program egy ablakban két dolgot tartalmaz, amelyek általában különböző helyszíneken jelennek meg: a tropofer terjedésének előrejelzését (Hepburn és F5LEN tropo indexe, izobare), valamint annak megfigyelését, hogy mi történik valójában a sávokban (a DX cluster, a fordított Beacon Network és a PSKReporter pontjai, valamint a lehallgató állomások listája). A referencia-sáv 144 MHz; a reklámok 50 MHz-ről gyűjtenek.

A beállítás az LA0BY által javasolt kontrolllistát követi extrém tropo események esetén: az előző napokban a Hepburn és F5LEN előrejelzéseket és a hőmérséklet profilokat a magasságban, míg az esemény folyamatban van, a DX klaszter és a helyszínen feltérképezi a bizonyítékot, hogy a nyitás indul [1]. LA0BY azt is megjegyzi, hogy az egyetlen Hepburn / F5LEN térképek nem elég, hogy megjósolja a leghosszabb kapcsolatok [1]

## 2. Az ablak és a fülek

Az ablak egy, és nem tört a mobil ablakok. Fentről lefelé:

- Általános beállítások sor: Hívás és helymeghatározó a bal oldalon; a jobb oldalon az UTC óra (pl. 08 / 11 / 2026 20: 33: 38 UTC, borostyán szöveg), amit az NTP: OK vagy NTP: FAILED, a téma menü és a nyelv.
- A fülek száma öt csoportban: Térkép és helyszín; Előrejelzés (Propria β, az előrejelzés által kiszámított és az előrejelzési kézikönyv, majd Hepburn és F5LEN); GRAVES; Adatok és források (DX klaszter, PSKReporter, fordított Beacon, UDP, kinevezések és gyanús jelentések); Útmutató (Program művelet, térkép épület, Hogyan alakítsa a tropo, Kézi előrejelzés, Tippek az üzemeltető, egy keresődoboz, amelyben számít, hogy a szöveg megjelenik). A gerenda alatt egy sáv mutatja a csoport alnyelveit; mindegyik csoport emlékszik az utolsó használt nyelvekre.
- A lap tartalma és az alján az állapotsor a források állapotával, a nevek adatbázisával és az utolsó frissítés időpontjával.

A felső sor beállítások vonatkoznak minden fülre: a kiválasztott téma is színes az adatgyűjtés és a háttér dokumentumok; Dokumentum oldalak maradnak fehér, mint egy lap, így jól nyomtatják.

## 3. Hívás és főbérlő

A főbérlő 4 vagy 6 karakterből állhat (pl. JN70 vagy JN70CN). A program a tér közepének koordinátáira alakítja át: 20 ° × 10 °, 2 ° × 1 ′, 5 ′ × 2,5 ′. Ha a szöveg nem érvényes, a doboz széle piros lesz, és az állomás nem jelenik meg a térképen. A helymeghatározó az azimut kivetítés központjaként és a hirdetőtábla távolságainak és azimutjának kiindulópontjaként is szolgál.

A hívás a név, amit klaszterbejelentkezésként használnak. Az első kezdéskor, amíg nincs érvényes jelölés, a klaszterek nem csatlakoznak: egy ablak a nevet kéri (és, opcionálisan, a főbérlő); megerősítve, a nevet felhasználónévként másolják minden üres klaszter bejelentkezési mezőjében, és a klaszterek elhagyják. A "Később" változatlanul marad, és a Connect gomb minden klaszter szükséges a jelölés újra. Az MQTT, az UDP és az NTP nem használja a bejelentkezést, és így is marad.

## 4. Adatgyűjtés: források

Minden forrásnak megvan a saját szakasza az Adatgyűjtési fülben, a csatlakoztatott / kikapcsolt, a felismert és csatlakoztatott dobozok induláskor, státusz, mérők és regisztráció a kapott sorok. A szűrőről kikötött vagy kidobott vonalak nem jelennek meg a nyilvántartásban: csak a szakaszmérőket számolják. A szakaszok egymás mellett, kettő-kettő.

| Forrás | Kapcsolat | Mi szél hozott? |
| 1. DX klaszterválasztás | on0dxk.dyndns.org: 8000 parancskészlet / szűrő dxbm / pass 6 | Amatőr rádiók, szintén SSB és CW. ON0DXK -n a VHF szűrve van, és a megadott paranccsal van engedélyezve (poi pass 4, 2...). |
| 2. Második forrás | dxmaps.com: 7300 (DXMAPS, EA6VQ) és s50clx.si: 41112 ( S50CLX, Szlovénia) | A DXMAPS VHF-pontokat gyűjt, és RBN-t is ad (set / smissmer control, már be van állítva); az S50CLX egy DX Spider klaszter. Bejelentkezés a neveddel; szerkeszthető címek. |
| 3. Fordított Beacon hálózat | telnet.reversebeacon.net 7000 CW / RTTY · 7001 FT8 | Automatikus spóroló pont. Bejelentkezés [9] névvel. |
| 4. Klaszter a PSKReporterrel | dm4x.ddns.net: 8500 | Digital Spots of PSKReporter cluster formátumban. |
| 5. MQTT PSKReporter | mqtt.pskreporter.info 1883 · 1884 TLS-szel | Minden valós idejű recepciós jelentés: aki továbbítja, aki hallgat és az [5] helymeghatározó. 144, 222 és 432 MHz. |
| 6. Időjárási és tropótérképek | dxinfocentre.com / tropo _ eur.html tropo.f5len.org / előrejelzés - európai GFS modell | Hepburn [3] és F5LEN [4] tropo index térképek, tengerszint nyomás izobare (GFS via Open- Meteo [18]). A Hepburn és az F5LEN -t meg kell engedni a szerzőknek. |

Cluster telnet (forrás 1104). A kapcsolat után a program a névkérelemre vár (a bejelentkezést tartalmazó szöveg, hívás:, hívójel vagy hívás); ha 4 másodpercen belül nem érkezik meg, kérjük, küldje el a bejelentkezést. Akkor küldje el a parancsokat a szakasz, elkülönítve a pont és vessző, egyet minden 0.6 másodperc. A telnet vezérlő karaktereket a sorok elolvasása előtt el kell távolítani. Ha a kapcsolat leesik, és a Riconnetti aktív, próbálja újra 10 másodperc után (15, ha a szerver egyáltalán nem reagál) [6].

PSKReporter MQTT (forrás 5). A program a pskr / filter / v2 / {banda} / {modo} / {tx _ call} / {rx _ call} / {tx _ grid} / {rx _ grid} / {tx _ dxcc} / {rx _ dxcc} csak a zenekar rögzítésével és a többi mező szabadon hagyásával a + [5] karakterrel. Az első csatlakozáskor ellenőrizni kell azt a pontos nevet, amellyel a kiszolgáló jelzi a sávokat (például 2m, 1.25m, 70cm).

## 5. A csomóponttól a helyig

Egy sor egy hely, amikor a forma DX de SPOTTER: frekvencia DX megjegyzés hhmmZ. A program a spotter, DX állomás, frekvencia kHz, komment és most UTC; a nevek leveszik a succer succixes (- #, -1...). Ezután ebben a sorrendben az alábbi [6] szabályokat kell alkalmazni.

| Band | Elfogadott szegmens |
| GRAVES (144-gyel) | 143,040 - 143,060 MHz |
| 144 MHz | 144-148 MHz |
| 222 MHz | 220-225 MHz (csak 2 ITU régió, Amerika) |
| 432 MHz | 420-450 MHz |
| 1296 MHz | 1240 - 1300 MHz |
| 2320 MHz | 2300 - 2450 MHz |
| 10 GHz | 10,5 GHz |

1. Zenekar. A frekvenciának az egyik kezelt szegmensbe kell esnie; a többi, különösen a 49 990,0 kHz alatti kereskedelmi forgalom nem jelenik meg, és "sávon kívüli" -nek számít.
2. Way. A magyarázat egyik legismertebb módja az FT8, FT4, Q65, JT65, ISCAT, CW, SSB, FM, RTTY, PSK31, MSK144, FSK441, JT6M); USB és LSB SSB.
3. Negatív szűrő. A pontot ki kell dobni, ha MSK144, FSK441, JT6M, JTMS, ISCAT > < AUE > < ES > < MS > < FAI > < F2 >, nem érzékeny felső tartókkal és zárójelben lévő szóközökkel rendelkezik. Ezek olyan módszerek és szaporítások, amelyek nem tropo: Szórja szét a meteort, az aurorát és az F2 -t.
4. Levegőztető. Ha a megjegyzés < AS > vagy < ACS > (keverve is, pl. < As >) -t tartalmaz, vagy a helyet légoszlatónak (légoszlatónak, levegőszórónak, repülőgépszórónak) nyilvánítja, a folt marad, de sárgán, a pontozott vonallal (sötét hullámmal, hogy olvasható maradjon az átlátszó térképen), és a listán kiemelik.
5. Az állomások elhelyezkedése. Ha a megjegyzésben két helymeghatározó van (pl. JN54 < JN83), az első azok közül való, akik a helyet és a második a DX állomás; egy helymeghatározó, hogy DX állomás. Ami hiányzik, az a CALL3 -ban van. TXT, csak a pontos névvel (laptopok lásd alább). Ha egy állomás álláshely nélkül marad, a hely megjelenik a listán az "ismeretlen pozíció" megjegyzéssel, de nem a térképen.
6. Dupla. Az azonos DX-szel, azonos felderítővel, azonos sávval és azonos, már jelen lévő forrással rendelkező reklámok helyettesítik a reklámot.

CALL3. TXT a név adatbázis → lokátor által használt JTDX, a formátum CALL, LOCATORE,,. A projekt állománya 1 048 847 nevet tartalmaz, mind érvényes helymeghatározóval; a fájl fejléce még mindig 34 323 nevet tartalmaz, mert az [7] első kiadásában maradt meg. Az indításkor a háttérben van betöltve, így az ablak azonnal használható. A fájl a program mellett, az Adatmappában, abban a mappában található, ahonnan a program indult, és egy vagy két mappa van fent; Ha nincs, egy ablak jelzi, mikor indul, és az állapotsor jelzi, hogy piros. A neve mindig a 6- karakterét használja. Csak a lokátorokra vonatkozó szabály: ha a kapott példány (spot, MQTT, dekódolás, QSO desunti) kevesebb, mint 6 karakter és CALL3. A TXT-nek van egy a 6-hoz, mindig megér egy CALL3.TXT-t; a laptopok (F / IC8TEM, IC8TEM/P) csak egy pontos hang: anélkül, hogy az állomás megmaradna egy főbérlő nélkül, és nem máshol tervezték. A szabály a rajzoláskor is érvényes, így az adatbázis betöltése előtt érkezett adatok maguktól futnak.

Egy adatbázis: CALL3. TXT 1.3.4-től minden helymeghatározó egy fájlban, a CALL3. TXT-ben található, amely a JTDX és a WSJT- X (CALL, LOCATORE,,,,) formátumú, és a WSJT- X, JTDX és MSHV formátumban is használható. Nincs több külön akta. A fájl tetején, a fejléc után, két tömb található, egy megjegyzési sorral megjelölve: / / [LIVETROPOVHF-MANUALI], kézzel beillesztett bejegyzésekkel és / / [LIVETROPOVHF-CLUSTER] a klaszterből származó korrekciókkal, majd / [LIVETROPOVHF-FONTI] az összes többi tétellel, betűrendben. Minden jelölés egyszer jelenik meg. A sublingual Data and Sources › Kinevezések (CALL3) keres egy nevet, megírja a főbérlő, és nyomja meg a Hozzáadás / változás: a hang megy a blokkba a hangok kézzel, és ez megéri mindenek felett, még a helymeghatározó, hogy az állomás küld. * Eltávolítja a jelölést a CALL3.TXT-ről (a következő frissítés a forrásoktól, ha van). Adjon hozzá egy fájlt... olvasson el egy listát CALL, LOCATOR (akkor is elfogadja a pont és vessző, táblázatos és tér) és írja be a kézzel kiválasztott hangok között; a kiválasztott fájl nem másolódik. Újratölti a CALL3.TXT-t a lemezről, miután szerkesztővel szerkesztette. A források teljes sorrendje: saját lokátor, kézi hangok, klaszter korrekciók, a maradék CALL3. TXT (források, tanult élő lokátorok és G7VRD), kapott lokátor (amely csak akkor alkalmazandó az adatbázisban, ha 6 karakter, és nem végződik LL vagy MM).

A CALL3.TXT frissítése Ugyanebben az alnyelven a Frissítés gomb most letölti a forrásokat (CALL3. TXT of WSJT- X Javított, CALL3.TXT EME of W7GJ, grid _ data.bin of JTDX), csatlakozik hozzájuk (az első, hogy van egy jelölési nyeremény), ellenőrizze, hogy legalább 50.000 nevet (egyébként semmi változás), mentse a CALL3. TXT Az előző CALL3. TXT bejegyzések maradnak, és egy 6- karakter lokátor marad, ahol a forrás csak 4 azonos négyzet. A klaszter hangjai és korrekciói a tetején maradnak, változatlan. A Frissítő doboz minden nap, a csekk kerül sor az elején. A program újraírja a CALL3 -t. TXT közvetlenül után minden hang kézzel, és a tanult helymeghatározó, legfeljebb 10 percenként és a záró; a CALL3. A csomag XT-je csak olvasható, és az attribútum magától elszáll. Linux telepítők nem felülbírálják a CALL3. TXT már jelen van; hogy a Windows telepíti csak akkor, ha hiányzik. ALL3

Automatikus korrekciók a klaszterből. Az DM4X és RBN automatikus foltok a komment hozza két helymeghatározó, például KN09tp < > KN09: Az első az, amit az állomás, amely foltok kijelenti magát, hogy 6 karakter; a második az, hogy az állomás érezte, gyakran csak 4. Ha az első 6 karakter (és nem végződik LL vagy MM) és különbözik az adatbázis, a program korrigálja CALL3. TXT önmagában: a korrekció a / / [LIVETROPOVHF-CLUSTER] blokkba kerül, a harmadik mezőben az utolsó megerősítés időpontjával a források felett (a kézhangok alatt) érvényes, és a "CALL3 " -ként frissített gyanús jelek között jelenik meg. Tehát túléli a CALL3 -t. TXT frissítések. Egy két évig tartó, meg nem erősített korrekció elveszti az elsőbbségét, és normális hangként marad.

Utolsó forrás: G7VRD. Ha a jelöltnek nincs 6- karaktere a CALL3 -ban. TXT, a program kéri, hogy a háttérben a nyilvános API G7VRD (api.g7vrd.co.uk, "helymeghatározó hívójel"), amely összegyűjti a helymeghatározó automatikusan a hálózati forgalom. Csak egy 6- karaktert fogadunk el, amely nem a tér közepe (LL vagy MM); a kéréseket percenként legfeljebb 40 jelöltként csoportosítjuk, és minden jelölt naponta egyszer kéri, mert ez egy hobbistic szolgáltatás garancia nélkül. A megtalált lokátor belép a CALL3 -ba. TXT, ha a név nincs vagy csak 4 karaktere van ugyanannak a négyzetnek; a keresés a nyelvalatti mezőben lévő mezővel kapcsol ki.

Kinevezés a bárba. A PSKReporter MQTT témákban, a bár elválasztja a szinteket, majd az összetett nevek jön egy pont helyett a bar ( IV3. HB9CAT). A program visszateszi a bárt, és ha az üzenet tartalmazza a teljes neveket, használja azokat: IV3/HB9CAT marad IV3/HB9CAT a térképen, listán és adatbázisban.

A lokátorok élőben tanultak. CALL3. A TXT nem tartalmaz mindent: például az SP9EMF és az SP9NVY nincs ott, majd marad a PSKReporter által küldött 4 karakteres lokátor (a kártya ezt mondja). Az esetek csökkentése érdekében a program a CALL3 -ban ír. TXT minden 6- karakter, hogy jön bármely forrásból (PSKReporter, mint például adó vagy mint vevő, cluster, UDP dekódolás) a jelölés, hogy CALL3. A TXT-nek nincs, vagy csak 4 karaktere van egy négyzetben; egy hangot soha nem változtat 6 karakterre. Egészen 1.3.3-ig ezek a lokátorok voltak Data / LOC6.TXT

Átváltozás: 1.3.3. Az első elején 1.3.4 a fájlok eltekintve az előző verziók (Dati / CALL3 -MANUALE.TXT, az Data / CALL3 -AGGIUNTE, Data / CALL3 -CLUSTER. TXT, Data / LOC6. TXT és Data / CALL3 - G7VRD. TXT) összeolvadnak a CALL3 -ba. TXT ugyanazokkal a bejegyzésekkel. Az írás elengedése, a régi fájlok elveszik a.fuso kiterjesztést, már nem olvasod, és törölheted.

Automatikus vevőkészülékek. RBN, dm4x és PSKReporter gyakran jelent neveket egy utótag jelzi a vevő: -SDR, / SDR, - # (kimmer RBN) vagy -1, -2... (plusz ugyanannak az állomásnak a vevői). A főbérlő számára a utótag eltávolítása: OK2OZL -SDR OK2OZL JN89UE -t vesz a CALL3. TXT-ből, ha az állomás még nem küld 6- karakteres lokátort. A feltüntetett név marad az utótag.

A helymeghatározók elvágtak. Nem minden forrás kezeli a tulajdonos kiterjesztését: sok beállított 4 karakter, és néhány automatikus carry- over rendszerek szándékosan csonkítja a 4- karakter lokátor, még azok is, amelyek megszerzik az adatokat a valódi hallgatás FT8, FT4, Q65 és JT65, vagy töltse ki a közepén a tér (LL vagy MM, pl. JN89MM). Ezért az LL-lel vagy MM-mel végződő 4- vagy 6- karakter helyébe a CALL3 lép. Ugyanarról a térről. Ha a helyes hely és a tér közepe nem esik egybe néhány tucat km-rel, a hiba a tropo távolsághoz képest kicsi marad.

## 6. Ki figyel?

Minden MQTT üzenet a PSKReporter jelenti a nevét és lokátorát a fogadó [5], így nem kell keresni, hogy a CALL3. TXT A selectorban kiválasztott zenekar számára a program minden fogadó állomást megtart: főbérlő, utolsó fogadás, dekódolt utak, az érzékelt állomások száma és maximális távolság. Egy állomás akkor hagyja el a listát, ha már nem kap semmit a kiválasztott tartományban (3, 5, 10, 15, 30 vagy 60 perc).

A térképen minden vevő egy sötét rózsaszín üres kör, és minden kezdettől kezdve teljes rózsaszín vonalak felé az állomások, hogy érzi, és küld vissza a hálóra. A sorok mindig láthatók, kattintás nélkül. A meghallgató állomások, a hallottak, és a két fej minden hely mindig fel van írva a térképen, anélkül, hogy kattintani (ugyanaz a név ugyanazon a ponton van írva, csak egyszer). Ha rákattint egy körre, a vonalak még vastagabbá válnak, és részletekkel nyitja meg a lapot. Ha az MQTT lokátorának kevesebb, mint 6 karaktere van, a program a CALL3.TXT-t használja (körülbelül egymillió név), feltéve, hogy megfelel a kapott négyzetnek.

Meteoroszló. Mint a klaszterfoltok esetében, az MQTT-arányokat MSK144, FSK441, JT6M, JTMS vagy ISCAT-ban csak kidobják és megszámolják; ugyanez vonatkozik az UDP dekódolásokra is.

Nem igazi hallgatóság. Egyes állomások 144 MHz-es fogadást jeleznek, de a valóságban a vevő egy másik sávon van (rossz frekvenciájú jelzés): a jelentéseik több ezer km-es, nem létező nyílásokat terveznek. A program magától ellenőrzi. Minden állomás, amit egy hallgató azt mondja, hogy hallani a VHF követik 30 percig minden sáv, egy külön MQTT felirat (pskr / szűrő / v2 / + / {nominative} / #) [5]. Ha a hallgató által hallott állomások közül legalább kettő ugyanabban a percben (± 10) más sávokban is aktív, a hallgatót nem tekintik hitelesnek, és a VHF-jelentéseit elvetik; az MQTT-nyilvántartás az őt eláruló nevet és állomásokat jelenti. A pontok listájában, a mérők mellett, az MQTT gomb mutatja az áramlás állapotát (zöld levél, ha csatlakoztatva van, piros, ha nem), és azt, hogy hány hallgatót dobtak ki a sávban és a kiválasztott tartományban; rákattint a lapra a visszadobások listájával és minden egyes más sávon található állomásokkal.

Hosszú kapcsolat porcelán. A link több mint 1000 km (Clumbing Spot, MQTT Listening, QSO Desunti) nem jelenik meg azonnal: marad, és a számláló "tartott > 1000 km" mondja a számot. Ez akkor derül ki, ha egy másik kapcsolat, a különböző állomások és legalább a fél mentén, megerősíti, hogy az egyik ilyen kritérium: ugyanaz az állomás a fej (például ugyanaz a hallgató) és a másik állomás ugyanabban az irányban 10 °; hasonló folyosó, mindkét fej a másik kapcsolat; párhuzamos útvonal, iránya 10 ° és oldaltávolság 200 km, átfedés az útvonal mentén. Mind felbukkan, mind az MQTT napló mutatja az okát. UDP dekódolásai mindig láthatóak, és másokra is vonatkoznak. A megtartott kapcsolatok 60 percig maradnak memóriában.

Semmi 144 MHz alatt. A program 144 MHz-ről felfelé működik: minden 143 MHz alatt eldobható. A kivétel a GRAVES, a Broyelès-Pesmes ( JN27SI) által a CW-ben 143,050 MHz-en sugárzott francia radar: a 143,040 és 143,060 MHz közötti, vagy a nevükben GRAVES-szel ellátott kereskedelmi forgalom 144 MHz-es radarral jelenik meg. A hirdetőtábla azt mondja, ha a vevő a gerendán belül van (D, C, B, A szektor 90 ° és 270 ° között azimut látható a radar, 6 lépés minden 7,5 °) vagy ki; a lap GRAVES, mint a YA-MS, mutatja az ég látható a radar: ágazatok, lépés megvilágított abban az időben (sequenza PE1ITR 2021: 0,8 s lépésről, ciklus 4,8 s adatok és geometria jön YAMS. Érezni a trópusban nehéz, de megtörténhet.

Emberi klaszterek rehabilitációja. Egy állomás, amely más sávokban is megjelenik, nem számít gyanúsnak, ha ugyanabban a percben (± 30) a DX emberi klaszterek ( ON0DXK, GB7RAU; nem dm4x, RBN vagy más automatikus források) az aktív károsodás a szóban forgó sávban, más nevek által készített helyeken: azok az állomások, amelyek valóban együtt dolgoznak több sávon. Így egy eldobott hallgatóság látható (az MQTT register "rehabilitált"), és egy hosszú ideig tartó link az alacsony sávok lehet felfedni.

A 144 MHz alatti aktív állomások. A hosszú csatlakozások karanténjában egy 144 MHz alatti sávokon ugyanabban a percben hallható állomás (más sávok MQTT-jelentéseiből) nem erősíti meg más csatlakozásokat, és nem tárja fel azokat, bár egy hasonló vagy párhuzamos útvonal megerősítené azt.

## 7. A reklámok listája

35% -os jogot foglal el a térkép füléről és helyéről. Top: cím, sávválasztó (50, 70, 144, 222, 432, 1296, 2320 MHz, 10 GHz), tartományválasztó (utolsó 3, 5, 10, 15, 30, 60 perc) és frissítő kulcs. Az alábbiakban, négy méter: látható foltok, AS / ACS, kidobott a szűrő, állomások hallgatás.

Minden vonal mutatja a vonal típusát, az UTC időt, DX → DE-t, az utat, a km-ben mért távolságot és a második sorban a forrást, a frekvenciát és a megjegyzést. A legutóbbi a tetején van. Egy kattintás a vonalon kiválasztja a helyet: a térképen a vonal növekszik, és a két név jelenik meg.

## 8. Spot kártya és troposcatter veszteség

A kiválasztott helyen a tab mutatja a helymeghatározók, távolság, azimut, frekvencia, mód, forrás, most, komment, az átlagos tropo index mentén az útvonal és a becsült útvonal veszteség szórt tropo.

Távolság és azimut számítják a maximális kör Föld sugara 6371 km (képlet az emisenoverso távolság, kezdeti azimut a DX állomás a felderítő).

Veszteség troposzcatter. Az alapvető veszteség, két állomás sima terepen, az összeg a szabad tér veszteség és a terjesztés veszteség: G3YGF [2]

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

A veszteség dB-ben van, D a távolság km-ben, f a frekvencia MHz-ben, és a diffúziós szög fokban van rögzítve.

8497 km a Föld egyenértékű sugara (4 / 3 a fizikai). A θA és a θB az egyes állomások horizontjától függ:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

és Δh az akadály magassága, amely az állomás és annak távolsága tekintetében a horizonttól ered. Minden horizontszint körülbelül 10 dB [2] -ba kerül. A program nem ismeri a horizontot az állomások, így mutatja az alapvető veszteség a θA = θB = 0 és kijelenti, hogy ("sima föld"). Az G3YGF hozzáad egy éghajlati kifejezést, ahol N az [2] talaj reaktivitása; azt akkor használjuk, ha N elérhető az időjárási modellből:

```math
L_{N} = 0,2 \cdot (N - 310)
```

Ellenőrzés: 110 km és 144 MHz között a képlet 166,5 dB-t ad, szemben a cikk ábráján szereplő 168 dB ággal. 500 km és 144 MHz között 206 dB. A várható jel / zaj arány a rendszer kapacitása (eirp + a vevő hatékony érzékenysége) mínusz a veszteség: az G3YGF 144 MHz-es modellrendszerrel (198 dB) 500 km-es útvonal adna körülbelül -8 dB SSB [2] -ban, vagyis a kapcsolat csak a tiszta szóródás jobb terjedése.

## 9. A dekódolásaim: UDP a WSJT- X, JTDX és MSHV-ból

WSJT-X, JTDX és MSHV elküldheti UDP minden sor dekódolt. A program 127.0.0.1: 2237-re hallgat, és ugyanazt a csomagot ismétli, 127.0.0.1: 2238-on, így a többi program, amely ezt a portot használta (GridTracker, JTAlert, egy logger), tovább működik, ha a 2238-on mozgatja őket. Címek és ajtók változnak az UDP nyelvalatti az Adatgyűjtemény; mint egy halló cím is használhatja a multicast csoport (pl. 22.0.0.1), ha a dekódoló program be van állítva, mint ez.

A csomagok a WSJT- X hálózati protokollt követik: egy kezdeti mágikus szám 0xADBCCBDA, séma szám és üzenettípus. A program elsősorban három [8] típust használ:

- Heartbeat: mondja, melyik program továbbítja az adatokat és milyen verzióval.
- Állapot: a tárcsázás és üzemmód gyakorisága; a sáv frekvenciájából (a foltok azonos táblázatával).
- Dekódolás: SNR, idő- és frekvenciahulladék, a dekódolt üzenet módja és szövege.

Az üzenet kap az állomás hallott: CQ 9 A2PQR JN83 az állomás után CQ (megmentése DX, EU, TEST...), az IK0JKL IT9MNO JM68 a második, azaz ki továbbítja. A lokátor az üzenet, ha van (4 karakter, különbözik az RR73); különben keres a CALL3. TXT. Meghallgatják az állomásokat, amiket a fogadód hall. A térképen a QTH-tól az állomásig piros vonalak lesznek, a névvel; a réteg megvilágítja a dekódolt dobozt tőlem, és érvényes nyomkövetőt igényel. A jobb oldali listán egy számláló jelzi, hogy hány állomást dekódolsz a kiválasztott sávon.

QSO desunti dal live. Ha a dekódolás egy záró üzenet a C ALL1 C ALL2 RR73 (vagy 73 vagy RRR) formában, például IC8TEM IK3FHP RR73 vagy IK3FKP SV8PEX 73, a két állomás befejezett egy linket. A program QSO-ként rögzíti, mindegyikük lokátorát az utolsó üzenetből, amelyben kijelentette, vagy ha nem, a CALL3.TXT [7] -ból, és zöld vonalként rajzolja a térképen a két állomás között ("QSO desunti dal live"). A listán kiválasztott sáv és tartomány érvényes.

Ugyanazon az alnyelven, a Debug pane megmutatja a szöveget minden kapott csomag (típus, idő, értékek és üzenet), kérésre is hexadecimális bájtok, és egy sor továbbítására. Hogy ellenőrizzük, hogy a dekódoló program valóban elküldi-e az adatokat.

## 9 bis. Nyelvek Hepburn és F5LEN

Mindegyik azt mutatja, amit a program elvesz a forrástól. A "Olvassa el az oldalt, és töltse le az összes képet" gomb megnyitja a megadott oldalt (a dxinfocentre.com / tropo _ eur.html alapján Hepburn [3] és tropo.f5len.org / preciasts- for- europe F5LEN [4] esetén), és letölti az összes cache képet a végrehajtható mellett; egy kép címét is beillesztheti. Minden képre van egy doboz, amely kizárja azt, és egy mező az előrejelzési időt. A 31 európai F5LEN kép neve a modell verseny ideje (03.png... 48.png 3 óránként, 54... 96 6 óránként, 108... 192 12 óránként), és a program veszi az időt a nevét [4] fájl; Hepburn megéri a sorrendben az oldal. Az F5LEN lapon egy menü választja ki a régiót: Európa (tropo.f5len.org / obloasts- for- europe), Ausztrália (tropo.f5len.org / Australia, vk03.png... vk192.png) vagy az Atlanti-óceán a Zöld-foki-szigetek és Írország között (tropo.f5len.org / atlantic- ocean / atlantic- d4ei, atl03.png... atl192.png). Mindenkinek ugyanazok a lépései.

Itt van a kalibráló számláló. Egy kattintás a képre hozzáad egy vezérlőpontot (lokátor vagy lat, lon: kell legalább 6) vagy egy legendás színt annak értékével. A program kiszámítja a pixel egyező → koordinátákat egy másodfokú polinomiális interpoláció a legkisebb [19] négyzetek, olvassa a színek a választott tolerancia, és, F5LEN, átalakítja az egységek N a 0-10 skála egy · N + b. "Mentse kalibráció és próbálja olvasni" írja a fájlt a Tarature mappában, és ellenőrizze, hogy melyik pixel a lokátor esik. A kalibrálás előtt engedélyt kell kérni a térképek szerzőjétől.

Régiók. Minden forrásnak több régiója van, mindegyiknek fix nevű képei: Sajnálom, sajnálom. Minden egyes forrás füle először a fúzió előnézetét mutatja: minden letöltött kép, amely a fő térképen található, a pontozott táblával és az egyes régiók nevével, egy szürke fátyollal, ahol nincs kép, és egy csúszda, amely az órákat görgetné; a bal oldalon a régiók listája, mindegyik a használni kívánt mezővel, az olvasott térképek száma és a szélsőségek (OCR, kézzel vagy gyárral; ha az OCR nem felel meg az olvasott szövegnek). A manuális kalibráló pad az ismeretlen gomb mögött marad. A Bench Region menü választja ki, hogy melyik megjelenés, a "Használja ezt a régiót a térképen" mezőben benne van az indexben. A keret egyedül található (két vízszintes vonal és két függőleges vonal azonos színnel); a kép elhelyezéséhez a keret szélsőségeit / lonját szolgálja: Hepburn Europe (10 ° W- 40 ° K, 27 ° -56 ° N) és F5LEN Europe (15 ° W- 40 ° K, 30 ° - 65 ° N) már szerepel a programban, a többi régióban írnak a négy O E S N mezők olvasó tengely címkék. A skála szorzó teszi a két forrás összehasonlítható: végső érték = szorzó × legenda érték. [3] F5LEN [4] F5LEN

## 9 ter. Exportálás szó szerint

A két dokumentum az "Export to Word" gomb tetején található. A fájl az Office Open XML formátumban íródott, a képletek szóegyenletekké (OMML) válnak, így szerkeszthetők maradnak az [16] egyenlet szerkesztőjével. Ha a mentési ablak nem áll rendelkezésre, akkor a fájl a documents mappában ér véget a futtatható mellett.

## 9-es. Ki hív engem?

Ha üzenetet küldünk Önnek a helyi UDP-n (az első név a magáé, például IC8FAX IK3FHP JN55 vagy IC8FAX IK3FHP -12), a program a QTH-tól a fekete pontozott és villogó vonalnak nevezett állomásig vezet, a neve és a mellette lévő lokátorral. A lokátor a CALL3.TXT-ből (beleértve a kézi hangokat) vagy üzenetből származik. A vonal addig marad, amíg az UDP át nem adja a QSO zárását az állomással ( RR73, RRR vagy 73, az egyik a két irányból), legfeljebb 3 percre az utolsó hívástól; áthúzunk mindent, és nem követjük a kizárási kritériumokat, így a közvetlen hívás soha nem kerülhető meg.

A PSKReporter az MQTT-n keresztül nem továbbítja az üzenetek szövegét: csak azt mondja, hogy ki kapta meg a frekvenciával, módszerrel és SNR-rel rendelkezőket. Ezért a közvetlen hívást csak saját UDP dekódolásával ismerik el.

## 10. Időszabályozás (NTP)

A digitális üzemmódokhoz és a pontos időhöz pontos óra szükséges. Minden 3 perc a program kihallgatja 10 NTP szerverek (SNTP UDP 123): 0.pool.ntp.org, 1.pool.ntp.org, 3.pool.ntp.org, it.pool.ntp.org, ntp1.inrim.it, ntp2.inrim.it, ptbtime.ptb.de, time.google.com, time.cloudflare.com [11].

Minden válasz kiszámítja a visszadobás és a késedelem a visszatérés, visszadobja a rendellenes értékeket, és veszi a medián, a számítás a visszadobás és a késedelem által biztosított SNTP [10]. Legalább 3 válasz egyetért az UTC óra a program beállítások és a következő jelenik NTP: OK a zöld; ha a válaszok nem elég megjelennek NTP: Failed a piros, és az óra marad, mint a számítógép. A rendszeróra érintetlen, mert rendszergazdai engedélyekre lenne szükség; minden belső program ütemezése (helyek, 5- 60 perc ablakok, dekódolás) összehangolt időt használ. A vezérlés mindig a háttérben forog: az interfészben csak az óra melletti eredményt látod; a LIVETROPOVHF.ini szerverek, időközök és minimális válaszok változását (ntp _ servers, ntp _ minutes, ntp _ min).

## 10 bis. Általános kizárási kritériumok

Két doboz a réteg sáv vonatkozik minden rajzol (spot, PSKReporter, dekódolás, QSO desunti):

- 100 km alatti távolság. A 100 km-nél rövidebb állomások közötti forgalom nem biztos, hogy jelentős a tropó számára (földi hullám, optikai tartomány): az on-box használatával nem rajzol és nem jelenik meg a listában. A kezdésnek vége.
- Több mint 1000 km: csak négyzetek. Körül a QTH (a helymeghatározó beállítva) van egy koronát 1000 km minden irányban. Ha csak az egyik a két állomás a kapcsolat kívül van ezen a koronán, a kapcsolat nem húzott vonal: helyén ezek szànek orgona, 45 °, a lokátor négyzetek (4 karakter) az érintett állomások. Ugyanez vonatkozik a több mint 1000 km-es PSKReporter állomásokra, amelyek a kör helyett négyszögként jelennek meg. Eddig a forgalom nem töltötte meg a vonalak térképét. Érvényes helymeghatározó nélkül a kapcsolat hossza számít. A tér a QTH soha nem colores az orgona, még akkor is, ha hallgat egy állomás túl a koronát: a meghatározott lokátor, minden név használ, aki működik az állomáson. A korona sugarát a doboz mellett választják ki: 1000, 700 vagy 500 km. A Corone 1000 / 700 / 500 km-es doboz a QTH körül húzódó három koronát lilában rajzolja, a legjelöltebb választással és az észak felé írt távolsággal. A helyek listájában a távolságot követi.. Kezdődik. A kritérium a karantén után alkalmazandó: a még megtartott hosszú összeköttetések még négyzetként sem jelennek meg.

Melyek a 45 ° -os orgona négyzetek. A forgalom távol a QTH érdeklik, hogy megértse, hol a terjedés nyitott, de a vonalak, több száz kilométer hosszú és gyakran tízes, fedje le a térképet, és elrejteni, mi számít az Ön számára: a nyílások lehet kihasználni az állomás. Ezért, a kiválasztott koronán túl, a program nem rajzolja a vonalakat, hanem színezze a helymeghatározó négyzetek (2 ° × 1 °) az érintett állomások. Az eredmény az "aktív zónák" térképe: ahol sok közeli lilák tér jelenik meg, van egy csatorna vagy egy nyílás, még akkor is, ha ez nem érint közvetlenül; Ha a terület szélesedik felé, ez a jel, hogy irányítsa az antennát abba az irányba. A 45 ° -os műszerfal lehetővé teszi, hogy a tropo index és a rács alatt látható legyen, tehát egy pillantásra hasonlítsa össze az előrejelzést (alsó szín) és az igazi forgalmat (illus). A QTH tér soha nem színezik, mert ez a kiindulópont és nem egy távoli terület. Csak a koronán túl fekvő négyzetek színesek: a korona által vagy belül keresztezett négyzet akkor is szabad marad, ha a két állomás egyike van. Hogy lásd a vonalakat, csak kapcsolja ki a dobozt, vagy válasszon egy nagyobb koronát.

## 11. Témák és nyelvek

Témák: a 18 közös IC8TEM programok, ugyanaz, mint YA-NEC és E-LAYER ANALYZER: Tiszta, éj, fekete, nyolc pasztell (kék, narancs, aquamarine, zöld, rózsaszín, lila, indigó, barna), Scia meteorica (YA-MS paletta), az IP- TRX- CTRL (Chiaro, Notte, Nero) és a Blue, Purple, Green. 1.3.4-től a Chiaro úgy néz ki, mint a YA-NEC: világos szürke háttér, határos fehér panelek, kék éjjeli fülek és kék akcentus. A sötét témák is az alap térkép lesz sötét. Az előrejelzés színskálája ugyanaz marad minden témakörben. A keskeny képernyőn (1500 pixel alatt, például 1366 × 768 alatt) az óra a tetején csak az órát mutatja, így a téma, kurzor szövegek és a nyelv látható marad. Nyelvek: YA-MS [17] 28. Wiki (angol, utazási, Deutsch, Nederlands, Français, Español, Português, Dansk, Norsk, Svenska, Íslenska, Eesti, Lietuvi, Latviešu, Руский,, кра, нська, Türkaśmoldia) Arábiával, Héber és Urdu az egész ablaktükör jobbról balra; a térkép [12] marad. A fordítások az interfészre vonatkoznak; az állapotsor két technikai dokumentuma és üzenete olasz nyelven marad. A napok neve a nemzetközi környezetből származik. NET a választott nyelvre.

Téma és nyelvi változás a két menü a jobb felső, mellett az óra, és azonnal alkalmazni, anélkül, hogy újraindítja a programot, és megszakítás nélkül az adatgyűjtés. A választás a LIVETROPOVHF.iniben található.

## 11 bis. Feltételezett és öntanulási jelentések

A Várható Jelentések lap összegyűjt mindent, amit a program vezérlése furcsának talál, típus és név szerint csoportosítva, az elmúlt 24 órában. A program magától tanul: minden pár esetében (típus, név) a pozitív eredményeket (+) és az ellenkezőjét (−), és miután 3 elfogadott eredmény létrehoz egy automatikus szabályt; ellenkező eredmény teszi ezt kétségbe. A felhasználói döntések (Good, Poor) mindig érvényesek; a pulsante gomb visszaadja a döntést a programnak. A szabályok az adatokban / alkalmazásban találhatók. TXT, a program mellett; az egy évig meg nem erősített automatákat elfelejtik.

| Típus | Mikor | Kilépés + / - | Szabályhatás |
| EME ( Q65/JT65) | Q65 vagy JT65 több mint 1000 km a Hold felett a horizonton mindkét fej, vagy EME komment | ~ minden alkalommal, amikor a Hold gyakori | Dobja el: EME állomás, a hosszú Q65/JT65 is kizárt nélkül közös hold. Jó: tropoként kezelik |
| Hosszú kapcsolat | 1000 km felett, karanténban | + leleplezve, -érvényessége megerősítés nélkül | Jó: az állomás hossza azonnal látható. Dobja ki: soha |
| Hallgató | PSKReporter vevő, aki hallja aktív állomások más sávokban | − emberi klaszterek által kitermelt, + regenerált | Jó: soha ne dobja el. Kidobás: mindig |
| Különböző főbérlő | A tér más, mint a CALL3. TXT | + minden alkalommal, amikor megismétled | Jó: az állomás elmozdult, megéri a teret kapott. Dobja ki: vale CALL3. TXT |
| Square Center | 6- karakter LL vagy MM |  —  | Kizárólag információ |
| Automatikus vevőkészülék | Kinevezés -SDR, / SDR, - # vagy -n |  —  | Kizárólag információ |

EME. Q65 és JT65 a módja a kommunikáció keresztül Luna: Elméletileg beleesnek a kizárási záradékba, mint a szétszórt meteor, de tropóban is használják őket. A program a Hold helyzetét az Almanac csillagászati képleteinek alacsony pontosságával számítja ki (kb. 0,3 °) [20]: ha az Q65 -ban vagy JT65 -ban lévő kapcsolat meghaladja az 1000 km-t, és a Hold mindkét fej felett van, szinte biztos, hogy EME-je van, és kizárt (a kizárottak közé tartozik). Ha a Hold nem gyakori, a kapcsolat továbbra is lehetséges a trópusban, és áthalad a hosszú kapcsolatok karanténján, de a jel látható marad. Ez a klaszterek, az MQTT és az UDP dekódolására vonatkozik.

## 11 ter. Kontakt visszadobási kritériumok, egyenként

A program csak a 144 MHz-es és azon túli tropopherikus terjedést akarja megmutatni. Minden bejövő adat (Cluster Row, PSKReporter Report via MQTT, UDP dekódolás) áthalad egy sor szűrőn, mindig ugyanabban a sorrendben. A kidobott adatok nem tűnnek el semmiben: meg kell számolni (érintkezés a kizárva a felső sáv), és ha az ok kétséges, megjelenik a lap Független Jelentések.

|  N.  | Feltételek | Hol van? | Mi selejtezett | Át tudsz öltözni? |
|  1  | Band | klaszter, MQTT, UDP | Mindegyik 143 MHz alatt van (szintén 50 és 70 MHz), kivéve a GRAVES 143,050 MHz-et. A sávok 144, 222, 432, 1296, 2320 MHz és 10 GHz | Nem. |
|  2  | Műholdas QO- 100 | klaszter, MQTT, UDP | 2400,0- 2400,5 MHz és 10489,5- 10490 MHz: geostacionárius műholdas forgalom, nem tropo | Nem. |
|  3  | Meteor scatter | klaszter, MQTT, UDP | JT6M és ISCAT (is ISCATA / B), megjegyzések "&" és nem-tropoxi propagációs címkék (< AU >, < AUE >, < ES >, < FAI >, < F2 >) mindig. MSK144, FSK441 és JTMS (is < MS >) csak több mint 250 km: 250 km-en belül elfogadjuk, mert ezen a távolságon belül ezeket a módokat a tropóban is használják. | Nem. |
|  4  | EME (via Luna) | klaszter, MQTT, UDP | Az EME a megjegyzésben leírva: Q65 vagy JT65 több mint 1000 km, amikor a Hold a látóhatár felett van mindkét fejre; a állomások EME-ként tanultak | Igen: Jó szabály a gyanús jelek |
|  5  | Nem - Eredeti hallgató | MQ | PSKReporter vevő, aki azt mondja, hogy hallani 144 állomás aktív abban az időben más sávok (legalább két): A kapcsolatait elvetették. Vissza jó, ha a DX emberi klaszterek megerősítik ugyanazt az állomást | Igen: jó / szegény |
|  6  | Hosszú kapcsolatok karanténja | valamennyi vonal | Több mint 1000 km egy link marad elrejtve, amíg nem talál egy megerősítést: ugyanaz az állomás ugyanabban az irányban, egy másik útvonal egy 200 km-es folyosón, vagy párhuzamos útvonal 10 ° és 200 km mentén legalább fele. Megerősítés nélkül 60 perc múlva lejár | Igen: Jó / Off to station |
|  7  | 100 km alatt | sor és lista | Az on-box, a legrövidebb kapcsolatok 100 km (földi hullám, optikai tartomány) | Doboz, induláskor |
|  8  | A QTH koronáján túl | vonalak | Ha egy állomás több mint 1000, 700 vagy 500 km (az Ön választása szerint) a QTH, nincs vonal: csak a lilla négyzetek által lefedett két állomás. A QTH tér soha nem színes | Doboz és sugár 1000 km-en |
|  9  | Időablak | Minden | A kiválasztott tartomány régebbi helyei (3 és 60 perc között) | Menü a tetején |
|  10  | Dupla | klaszter | Ugyanaz a hely (ugyanaz a DX, ugyanaz a felderítő, ugyanaz a sáv, ugyanaz a forrás) ismétlődött | Nem. |

Megjegyzés a 4. és 6. kritériumról. Az Q65 -ban vagy JT65 -ban 1000 km-nél hosszabb, közös hold nélküli kapcsolat lehetséges tropóban: a 4. kritérium nem utasítja el, hanem a 6. kritérium szerinti karanténra, mint bármely más hosszú kapcsolatra vonatkozik. Közvetlen hívások az Ön számára (9. szakasz quater) az egyetlen kivétel a 7. és 8. kritérium alól: mindig rajzolnak.

Ami nem pazarlás. A helymeghatározó korrekciói (CALL3. TXT kézi hangok, korrekciók a klaszter és tanult lokátorok, utótagok -SDR, közepén a négyzet LL / MM) változás, ahol rajzol egy állomás, nem dobja el. Egy olyan állomás azonban, ahol nincs tulajdonos, nem rajzolható le: a "-" távolsággal a listán marad.

## Tizenegy. Színek és szimbólumok

Minden színnek csak egy jelentése van, egyenlő minden témával (csak a háttérrel).

| Szín és aláírás | Jelentés |
| Folyamatos kék vonal | A DX klaszterből, RBN-ből vagy DM4X -ból származó folt két állomás között |
| Sárga vonal sötét éllel | Spot légoszlató megjegyzés (reflexió a repülőgépen) |
| Sötét rózsaszín üres kör | Figyelő állomás a PSKReporter-en (MQTT) |
| Vékony rózsaszín vonalak a kör, egy rózsaszín labda alján | Állományok, amelyeket a hallgató hallott, a név mindig meg van írva; vastagabb vonalak a kiválasztott hallgatóhoz |
| Vörös vonal és piros név | UDP dekódolásai (WSJT- X, JTDX, MSHV): Ki vagy te? |
| Piros vonal villogó stroke | QSO folyamatban az állomással: Felhívott, és a zárás még nem érkezett meg, vagy a DX Call of WSJT- X / JTDX az engedélyezett átvitel. Folytassa RR73, RRR vagy 73 |
| Zöld vonal | QSO desunti dal live: záró RR73, RRR vagy 73 UDP nézet |
| Fekete vonal fehér határ | Valaki hív: akár RR73/RRR / 73-ig, akár 3 percig is eltarthat. |
| 45 ° -os négyzetek | A kiválasztott korona felett legalább egy állomással rendelkező forgalom: Nincsenek vonalak, csak négyzetek. |
| Keresse meg a lillák távolság az északi | 1000, 700 és 500 km körül QTH; ez a választás több jelölt |
| Kör és pont a szín a téma | A QTH, a név és a főbérlő |
| Narancssárga golyó GRAVES 143,050 | A radar GRAVES ( JN27SI) |
| Minta kéktől pirosig | Hepburn trope index (0- 10) vagy F5LEN osztály, vagy azok átlaga; melegebb szín, jobb körülmények |
| Világosszürke bársony | A kiválasztott időre vonatkozó tropo-adatok nélküli terület |
| Grey- blue lines with the value in hPa | Izoberát minden 4 hPa; H kék magas nyomáson, L piros alacsony nyomáson |
| Piros, kék és lila pontozott vonalak | Izotermia 850 hPa-ig 4 ° C-onként: 0 felett piros, nulla alatt kék, a nulla lila |
| minta | Termikus visszafordítása: levegő 925 hPa melegebb, mint a földön, intenzívebb erősebb inverzió |

A Feltételezett Jelentésekben minden lap széle azt mondja, a hatályos szabály: zöld Jó, piros Scarta, szürke nincs szabály.

## 12. Beállítások és hordozhatóság

Ablak. Kezdetben az ablak a képernyő hasznos helyeinek 90% -át foglalja el (vágjuk el az alkalmazás sávját), középen, így egy 15-16 hüvelykes noteszen, 125 vagy 150% -os átméretezéssel a címsáv látható marad. Ha a hasznos hely alatt 1280 × 720 pont, az ablak része maximalizálódott. Méret, pozíció és maximalizált állapot menthető a LIVETROPOVHF. ini a záráskor és a végén a következő indítás, feltéve, hogy a címsor még mindig esik a képernyőn (például leválasztás után egy külső monitor); Ellenkező esetben visszatér 90%. A legkisebb méret 900 × 560.

A program hordozható: soha nem ír az AppData mappában vagy a regiszterben. Minden a programozó mappájában van: LIVETROPOVHF.ini (Az első záráskor létrehozott beállítások), CALL3. TXT (kézzel írt hangokkal, tanult javításokkal és helymeghatározókkal), térkép gyorsítótárral és időjárási adatokkal, tanult szabályokkal (Dati / APPRENDIMENTO.TXT) és LIVETROPOVHF.db archívummal. USB-boton is lehet tartani, amíg a mappa írható.

Ad archívum (SQLite [14]). Minden elfogadott hely és minden PSKReporter hallgatási jelentés a LIVETROPOVHF.db fájlban található, egy SQLite adatbázisban. A memóriában csak az utolsó 60 perc maradt meg a lista és a térkép; az archívum megtart mindent, és szolgál a történész és a statisztika (is az UDP dekódolás), például a nyitások a sáv és naponta, a maximális távolságok, a négyzetek elért, és a hátsó összehasonlítás a várt index és az igazi helyek. A szűrőről kidobott csíkokat számoljuk, de nem archiváljuk.

## 12 bis. Automatikus telepítés és frissítések

Az 1.0 verzióból a program már letölthető a Github.com / ic8tem / LIVETROPOVHF Release oldaláról, a YA-MS azonos csomagjaiban: telepítő Windows-hoz (64 bit, 32 bit és ARM, telepítés rendszergazdai jogok nélkül) és hordozható zip-hez; Windows 7 SP1 és 8.1 csomagok között (-windows7-x64 és -window.deb,.rpm, Arch, Slackware, AppImage és tar.gz Linux; csomag Raspberry Pi-hez (64 és 32 bit), valamint az Alpokhoz és az Apple-hez). Linux csomagokkal a programot ~ / LIVETROPOVHF-ban kezdik, így a beállítások és az adatok a program mellett maradnak; a Mac-adatokon a LIVETROPOVHFF mellett születnek.

30 percenként (az első ellenőrzés néhány másodperccel a kezdés után, azonnal, ha az utolsó alkalommal a program bezárt hiba miatt) a program egyedül olvas, anélkül, hogy bármit kérne, az utolsó kiadás. Ha a verzió újabb letöltése a hordozható csomag alkalmas a rendszer, és helyettesíti a program fájlokat a helyszínen: a használatban lévő új *.old- update és törli a következő újraindítás. Beállítások, archívum, adatmappa és CALL3. A XT-t nem érinti. Az új verzió a következő indítástól aktív: a telepítés befejezésekor az ablak azonnali újraindítást javasol; sürgős javítások esetén [kötelező] a Release-ben nem halasztható el. Az ellenőrzési státusz és a használatban lévő változat az állapotsor aljára van írva; a frissítések kikapcsolása = 0 a LIVETROPOVHF.iniben. A Mac az app van dedikálva, és nem változtatja meg magát: a program csak azt jelenti, hogy van egy új verzió.

A LIVETROPOVHF kép a verziószámmal az elején jelenik meg; a végén a program megerősítést és köszönetet kér a használatért.

## 12 ter. Hiba napló- és állapotsor

Minden előre nem látható hiba az Data / errori.log-ban végződik, még a számítási naplóval is: dátum és idő, verzió, rendszer, nyitott lap, teljes hiba és az utolsó sorok, amit a program csinált. Egy interfész hiba már nem zárja be a programot: az állapotsorban megjelenik az "anno annotated error" (kattintson: a regisztert megnyitni). A következő indításkor a program megkérdezi, hogy elküldi-e a jelentést: megnyitja a böngészőt egy GitHub jel már összeállított, jelöléssel, lokátorral és felhasználónévvel a PC eltávolított, amit ellenőrizni és küldeni egy ingyenes GitHub fiók. A napló minden frissítéssel véget ér.

Az állapotsorban a forráslabdák (DX cluster, RBN, PSKReporter, MQTT, UDP) kattintanak, és a forráshoz kerülnek; a képregény azt mondja, hogy miért van kikapcsolva a forrás: elutasított port, cluster csomópont állomásozott a szerver oldalán, nincs válasz, vagy UDP hallgatás, de csomagok nélkül WSJT- X, JTDX vagy MSHV. Egy kattintás a verziószámra azonnal ellenőrizze a frissítéseket.

## 13. Platformok

Külső könyvespolcok: Avalonia az [12] felület, MQTTnet, mint MQTT kliens a valós idejű PSKReport [13], Microsoft. Data.Sqlite az [14] archívumhoz.

C # on.NET 10 (LTS) [15] Avalonia interfésszel, publikálva, mint független programozó Windows 64 és 32 bit, Mac Intel és Apple Silicon, Linux Debian és Málna Pi (64 és 32 bit). Noted Limits: Windows 10 / 11; macOS 14 or later, with apps to sign or to open right → Nyitott; Málna a Pi 2 / Zero 2 W fel (ARMv6 modellek nem támogatja.NET).

## Köszönöm.
Köszönet az I5MZY Massimo Zannoni a Windows 7 és Windows 8.

Légkondi: ITU- R P.453, a 3. tanulmánycsoport digitális termékei (© ITU), személyes és nem kereskedelmi célokra.

## Fő források

1. S. Heck LA0BY, "Extreme Tropo propagation on 144 MHz and up", prezentáció a 41. Nordic VUSHF Meeting, Skjeberg 2019. Troposzferikus módszerek, eseményvezérlés lista (Hepburn / F5LEN előrejelzés, magas hőmérsékletű profilok Windy, DX klaszter nyitó teszt), megfigyelés, hogy csak az előrejelzés térképek nem elég a szélsőséges útvonalak.
2. J. N. Gannaway G3YGF, "Tropospheric Scatter propagation", QST 1983. november (a rádiókommunikációról szóló, 1981. augusztusi RSGB-cikkből). Szabadtér-veszteség és diffúzió képletei, ezzel egyenértékű 8497 km-es sugár, 10 dB / horizont telephely-időtartam, éghajlati kifejezés 0,2 · (N -310), a növénykapacitás és a jel- / zajarány, például 144 MHz-en.
3. W. R. Hepburn, DX Info Centre, "Tropospheric Dutting Forection" (dxinfocentre.com / tropo.html) és "The Hepburn Tropo Index (HTI)". Index skála és érték jelentése, előrejelzés 6 napon.
4. F5LEN, előrejelzés Európára (tropo.f5len.org / előrejelzés -for- European: list of images in the page script, class Legend Disturub... Duct a kép) és a világ előrejelzése (tropo.f5len.org / WW) és megjegyzés "Új változata a világ VHF troposzferikus szaporítási előrejelzési eszköz". Index alapján reaktivitás N egység, 40 lépés 3 óra, frissíteni 4 alkalommal egy nap.
5. mqtt.pskreporter.info, disztribúció MQTT valós időben a reklámok pskreporter.info (kurátora M0LTE, áramlás P. Gladstone használt engedéllyel). Ajtók 1883-1886, pskr / filter / v2 téma séma.
6. A projekt összefoglaló 8 / 11 / 2026 és Go teszt (core.go, guiigo) által biztosított a program szerzője. Ad Szabályok, negatív szűrő, sávszélesség szegmensek, telnet cluster menedzsment, témák, nyelvek és hordozhatóság.
7. CALL3. TXT, jelölési adatbázis → JTDX főbérlő (I. UA3DJY, A. ES1JA script, JTAIDER / hamspots.net adatok). A projekt fájljában ellenőrzött nevek formátuma és száma.
8. WSJT- X, forráskód, NetworkMessage.hpp fájl (az UDP protokoll leírása: Heartbeat, Status, Decode). Általános hivatkozás, nincs mellékelve; a JTDX és MSHV ugyanazt a formátumot használja.
9. Reverse Beacon Network (reversebeacon.net), service pages of the telnet node. 7000-es (CW, RTTY) és 7001-es ( FT8) kikötő, csak a névvel.
10. D. Mills, RFC 4330 "Simple Network Time Protocol (SNTP) Version 4", IETF 2006. A csomagolás formátuma, a hulladék kiszámítása és a visszafizetés késleltetése. Általános hivatkozás, nincs mellékelve.
11. NTP Pool Project (ntppool.org); INRIM, NTP időszolgáltatás (ntp1 / ntp2.inrim.it); PTB (ptbtim1.ptb.de). Az időszabályozásra használt szerverek. Általános hivatkozás, nincs mellékelve.
12. Avalonia UI, hivatalos dokumentáció (docs.avaloniaui.net). Multiplatform interfész, témák és elrendezés jobbról balra. Általános hivatkozás, nincs mellékelve.
13. MQTTnet,.NET könyvtár MQTT-hez (github.com / dotnet / MQTTnet). A PSKReporter áramlásához használt kliens. Általános hivatkozás, nincs mellékelve.
14. SQLite (sqlite.org) és Microsoft. Data.Sqlite (learn.microsoft.com). Archívum egy fájlban. Általános hivatkozás, nincs mellékelve.
15. Microsoft,.NET 10 - támogatott operációs rendszerek és autonóm publikáció egy fájlban (tanulnin.microsoft.com / dotnet). A 13. szakaszban megjelölt platformok és határértékek. Általános hivatkozás, nincs mellékelve.
16. ECMA- 376 "Office Open XML File Formats", part 1, section 22.1 (Office Math Markup Language). A.docx fájl és egyenletek szerkezete. Általános hivatkozás, nincs mellékelve.
17. A program szerzője által biztosított YA-MS wiki 28 nyelvének listája.
18. Open- Meteo, időjárás előrejelzés API (open- meteo.com), a NOAA GFS modell adataival. A tengerszint feletti nyomás izobare esetében (lásd a 2. dokumentumot).
19. J. P. Snyder, "Map Projections - A Working Manual", USGS Professional Paper 1395, 1987, and standard polinomial transformation methods for georeferencing images. Általános hivatkozás, nincs mellékelve.
20. A Csillagászati Almanac, D szekció, "Alacsony pontosságú képletek a Holdra" (körülbelül 0,3 ° -os pontosságú). Felismerte az EME kapcsolatait. Általános hivatkozás, nincs mellékelve.
