# Előrejelző kézikönyv

> Gépi fordítás olaszból: kétség esetén az olasz útmutató az irányadó.

Az előrejelzési lap magától számítja ki a tropoferikus terjedést: egy indextérképet a látható területen, valamint a jel, amit az állomás és a megfelelő. Ez a kézikönyv leírja az összes adatot, hogyan számítják ki, és melyik forrásból származik. A négyszögletes zárójelben lévő számok az alján lévő forrásokra vonatkoznak; a programban az eredmények minden adatában van egy komikus (szimbolo i) ugyanazzal a forrással és a bekezdésre való hivatkozással.

## 1. Mit csinál és mit csinál

A GFS [1, 2] modell függőleges hőmérsékletének és páratartalmának előrejelzése a levegőréteg rétegenkénti reaktivitását eredményezi, és a normál légkörhöz képest megbecsüli a jelnövekedés mértékét. Az útvonalon adja hozzá a valós talaj (Copernicus GLO- 30 [3]), diffrakció, troposzcatter, csatornák és vevő zaj.

Ez nem intézkedés: ez egy becslés egy időjárási modellről, amely több száz méter távolsággal rendelkezik. Ez arra szolgál, hogy összehasonlítsa a helyzeteket (ma jobb, mint tegnap, 70 cm jobb, mint 2 m, ez a kezelés jobb, mint ez), nem garantálni a kapcsolatot. A 14. bekezdés kifejti, hogy miként lehet bizonyítékot gyűjteni annak érvényesítéséhez.


> "A szaporítást nem lehet általánossá tenni, hanem a helyi morfológiával kapcsolatban tanulmányozni."
> - Volker Grassmann, DF5AI, jelentése: Flavio Egano, IK3XTV (2 méter)

Ez az elv vezérli a pont-pont számítást: minden útvonalat a talajon számítanak ki, látókörével, tengerével és könnyítéseivel; a térkép megadja az általános képet, kezeli a választ.

## 2. Nyelvi parancsok

- Band: 144, 432 vagy 1296 MHz. A térkép és a számítás a kiválasztott sávot használja.
- Most: a kurzor mozgatja az előrejelzést mostantól 72 órával előre, 3 órán belül.
- Frissítés előrejelzése (látható zóna): töltse le a GFS profilokat egy 240 pontos rácson, amely lefedi a látható térképet. A rácsszög (0,5 ° vagy nagyobb) a nagyítástól függ.
- Stop: megszakítani a letöltés vagy a számítás.
- Részletek megjelenítése (jobb panel a térkép): három szint, Essential, Normal és Complete, kapcsolja be a csoport dobozok; minden doboz akkor változik kézzel. A dobozok három csoportba tartoznak:
  - A térképen: izolinee del Gain, szintén 3-7 dB (tracting), izobare, termikus inverzió, időjárási jelek S ↓ ↑, korrelációs nyereség / nyomás, közvetlen látótér (4., 5., 7. és 9. bekezdés). A színek mindig maradnak.
  - A jel kiszámításakor: a számítás minden sorát, a sík földi profilt, a számítási naplót és a részletes fájlnaplót.
  - Korrekciók és élő adatok: időjárás-korrekció (Misme, subsidence), air- sea kontraszt, átlagos foltok (corridoi), APRS 2 m ugrások és LoRa, veszteség nélkül a hegyen túl (4 bis, 9 bis, 9 quater és 10 bis bekezdés).
  - Alapvető birtokol csak izolének, időjárás és média korrekciók foltok; Normál hozzáadott izobare, inverzió, időjárási jelek, közvetlen kilátás, sík föld, levegő-tenger és APRS; Teljes bekapcsolja. A szint csak azt változtatja meg, amit látsz, és mit javít be: a számítások ugyanazok maradnak.
- Adatforrás és történelmi eset: onnan, ahonnan a profilok jönnek, és melyik nap néz (3 bis.
- Állomásom és a tudósítóm: helymeghatározó, antennák, áram, kábelek, zaj (7. és 12. bekezdés).
- Számítsa ki az optikai jelet és lefedettséget: a két földi számítást (8- 13. bekezdés).
- Számítási nyilvántartás: minden lépés az alkalmazott számokkal, beleértve az Adat- calcoli.log fájlt.

## 3. A GFS-től a profilig

Minden egyes ponton a rács és minden órában olvasni a nyomást a földre, hőmérséklet és páratartalom 2 m, a szél 10 m, a magassága a határérték réteg, a CAPE és a hőmérséklet és a páratartalom képest 1000, 975, 950, 925, 900, 850, 800, 700, 600 és 500 hPa.

A szinteket az ipszometrikus képlettel és a virtuális hőmérséklettel számítják ki; a talaj alatti szintek ritkák. Minden szinten:

- N = 77,6 / T · (P + 4810 · e / T), T-vel kelvinben, P-ben és hPa-ban, valamint az ITU- R P.453- 14, eq. 9 telítettségi nyomásával, az EF korrekciós tényezővel (a hivatalos szövegben átvezérelve) [4].
- M = N + 157 · h, h km-ben kifejezve: a módosított reaktivitás. Ahol M felmegy, ott van egy vezeték.

## 3 bis. Adatforrás

Az adatforrás menü kiválasztja, honnan származnak a profilok:

- Automatikus (ajánlott): GFS először a NOMADS [14], a NOAA szolgáltatás, amely forgalmazza a modellt: minden pillanatban a program kér két vágások a terület G RIB2 formátumban, és olvassa el őket önmagában; NOMADS nincs napi limit, csak egy percenként, hogy a program tiszteletben tartja. A széles rácsok (0,5 ° -tól fentebb) használ 0,5 ° vagy 1. fájlokat 3 óránként, sokkal könnyebb. A NOMADS által nem adott pontok, vagy ha a NOMADS nem válaszol, kérdezze meg az Open- Meteo [2] -t. A két forrás ugyanazt a modellt adja, és ugyanazt a versenyt, így össze lehet keverni ugyanabban a rácsban.
- GFS az Open- Meteo: csak Open- Meteo, mint az 1.2 (a határérték per perc várható).
- GFS a NOAA NOMADS-tól: csak NOMADS. Lassabban letölthető (két fájl óránként, fél másodperc az egyik kérés és a másik között, hogy ne haladja meg a NOAA határértéket), de nem használja az Open- Meteo határértéket.
- ICON- EU (DWD) és ECMWF IFS 0, 25 ° az Open- Meteo-tól: a teljes hálózatra és az útvonalakra alkalmazandó, a GFS-től eltérő modellek. Ezek nem keverednek a GFS: összehasonlítani két modell egy időben egy egyszerű módja annak, hogy hogyan az előrejelzés bizonytalan. Az ICON-EU csak Európára terjed ki; az ECMWF nem rendelkezik az összes GFS nyomásszinttel (a hiányzó szintek ugrása), és lehet, hogy nem rendelkezik a határérték réteggel.

A felhasznált forrás van írva az állapot sor és az első sor az eredmények (Adatforrás), hány pont jött minden szolgáltatás és hány a gyorsítótár. A lemez gyorsítótárát modell választja el.

## 3 ter. Történelmi esetek

A történelem Case menü áttekinti a dokumentált nyitás az Open- Meteo [15] archívum GFS előrejelzések: a térkép mozog a terület az ügy, a zenekar beállítja magát, és az idő kurzor elhagyja az első nap az ügy. A Frissítés előrejelzésének megnyomásával töltse le az említett napok profiljait; a jelszámítás úgy működik, mint az aktuális előrejelzésnél. Arra szolgál, hogy összehasonlítsa a modellt a történtekkel.

Az első eset a 2022. augusztus 14-15-i QST (November 2022, "The World Above 50 MHz" [23]): a nyitás nagy nyomás alatt Manitoba és Kansas / Missouri között, 144-1296 MHz szinte minden FT8, a kanadai rekord 10 GHz 1228,56 km Q65 és jelek akár 1941 km. A második a 2021. december 29. és 2022. január 2. közötti nyitás a Tirrhenianról és a Földközi-tenger nyugati részéről 144 MHz-en, az IC8TEM -ról szóló jelentésekből: első nyitás 29-en (737 km-ig), szélesebb 31-en ( IT9GSF - EB5EA 1217 km), nagy nyitás január 1-jén Olaszország, Szicília és Spanyolország között. A régebbi esetekben (pl. 2000. szeptember) az Open- Meteo archívumban nincsenek adatok. Az archívumban nincs limit réteg magasság: ott nincs korrekció.

## 4. Az index és a színek

Minden réteg között két szinten a program összehasonlítja az N csepp a küszöbértékek a táblázat 6.5 Bean és Dutton [5]: például 1000 és 850 hPa között a csökkenés akár 60 N / km normális, több mint 100 N / km szuperrefrakció. A szuperrefraktív réteg értéke 10-15 dB a nyereség; egy csatorna, amely a sávot 20-25 dB-re szorítja, az M. Strong wind deficitjének megfelelően, a magas limit réteg és a magas CAPE csökkenti a nyereséget, mert eltávolítják vagy eltörik a rétegeket [5, 4.4 §].

A szín a becsült nyereség dB-ben, a fix skála egyenlő minden témában:

| Gain | Színes | Jelentés |
| 7 dB alatt | Senki. | szinte normális légkör |
| 7- 10 dB | kék | gradiens a normális felett |
| 10- 13 dB | zöld | szupertörésgátló réteg |
| 13- 16 dB | sárga | erős réteg, de csatorna nélkül |
| 16- 18,5 dB | narancs | nagyon erős réteg vagy túl vékony csatorna |
| 18,5- 21 dB | piros | Conduit Csapda |
| 21 dB | Lila | Erős és mély csatorna |

A kijelző doboz is 3-7 dB zóna között 3 és 7 dB, ahol valami jön létre, de marad a skála alatt, látható egy világos szürke korlátozó; színek nem változnak. A ponyva elhatárolja a saját előrejelzése által lefedett területet, és a maximális látható nyereséget mutatja: ha 7 dB alatt van, a térkép színtelen marad, mert a légkör majdnem normális, nem pedig azért, mert az adatok hiányoznak. Ha letölteni egy új kattintás a Frissítés előrejelzés nem szakítja meg; megállítani van Stop.

A színek átláthatatlanok 82% -ra, így a téma háttere csak megváltoztatja őket. A lokátorok rácsát és a négyzetneveket a színek fölé tervezték. Ha a jóslat nem ott, de Hepburn vagy F5LEN ad 5 vagy több sakk kártya jelenik meg a színével a forrás.

Csak a térkép ítélkezik. A jel számítása helyett azt ellenőrzi, hogy a réteg folyamatos-e az útvonal mentén (9. bekezdés): az az útvonal, amely a nagyon sárgát keresztezi, kevesebbet adhat, mint a térkép, ami azt gondolja.

## 4 bis. A reklámok által közvetített előrejelzés: folyosók (1.3.3-tól)

Copyright Policy: Egy link önmagában soha nem szabad, hogy egy vezetéket. Az elszigetelt és rövid recepció lehet a szórása a tranzit sík (az APRS csomag nagyon rövid), egy nyitás néhány perc, vagy egy hiba, és adna "csepp-magatartás" azok megtévesztése. A program felismer egy folyosót, amikor ugyanabban a bandában:

- legalább 3 pár független állomás (nincs közös állomás) legalább 300 km-es útvonalon;
- az útvonalak párhuzamosak: azimut ± 15 ° -on belül, átlagos pontok 150 km-en belül a közös tengelytől;
- mind az elmúlt 30 percben, a sugár legalább 10 perces távolságban ismétlődik (kitartás);
- az ARS esetében az ugrás csak akkor számít, ha ugyanaz az ugrás ismétlődik;
- 1.3.4-től az 1500 km-t meghaladó távolság csak akkor számítható ki, ha a sugár legalább két különböző forrásból származik (pl. PSKReporter és egy klaszter): egyetlen forrásból, a 2 m-es távolságokra, gyakrabban vannak EME-k, szétszórt meteorok vagy rossz helymeghatározók, mint a tropo.
- 1.3.4-től kezdve ne számolja az állomásokat "a sugarakig": akinek az elmúlt órában legalább 5, 1000 km-nél hosszabb tudósítója volt legalább 150 ° -os ventilátoron. 2 m-ben a trópus irányított; a vevő, hogy "szabad" együtt Izrael, Portugália, Anglia és Olaszország 2000-3700 km, gyakran pozitív jeleket, szinte biztosan a frekvencia konfigurált rosszul. A 17- 22 / 11 / 2026 I5MZY Az archívum 72% -a 2 m több mint 1000 km-re érkezett az állomásokról, így 22 / 11-én 10: 53 UTC volt OH6HLH, OZ1ITS, F4LUK, OM8ABC és SP2JLR, és a szűrő a leghosszabb folyosó ereszkedik 2175 km (regole 1.3.3) és 615 km.

Forrás: klaszterek (nem AS / ACS), PSKReporter az MQTT-n keresztül, QSO és WSJT- X / JTDX / MSHV dekódolás, valamint APRS ugrások 2 m-ben és LoRa 433 / 868 MHz-ben.

2 m és LoRa. A program az APRS-IS-t csak olvasható formában (rotate.aprs2.net, európai szűrő) és rádióugrások kivonata az ismert helymeghatározó állomások között (sorgente → digitpeater → IGate), mint például a DXView és az APRS PropView VHF térképe (RF- YVY, Apache 2.0). Három sáv ér: 2 m, LoRa 433 MHz (432-es sáv) és LoRa 868 MHz (1296-os corridoi: egy vezeték, amely a 868 MHz-es frekvenciát is a legmagasabb frekvenciára hozza). A zenekar a LoRa tocall-ból (aprs- devichid list) és a komment frekvenciáiból vagy szavaiból származik; az ugrás csak két állomás között ér meg ugyanannak a bandának.

A kényes pont az aktív ismétlések: egy 500 km-es jel érkezhet két vagy három digitalizáló ismétléssel. Az ugrás tehát csak akkor számít, ha két valós és egymást követő adó között van:

- az útvonalat nyomon kell követni: minden digitalizáló, aki továbbította a nevét, és az ugrást az egyik és a másik között mérik; egy név nélküli álnév (W IDE1 *, W IDE2 *, regionális alias) megtöri a láncot, és ez a szakasz nem számít;
- rejtett adatátvitel: a program emlékszik az útvonalra, amit minden forrás általában kér (például W IDE2 -2). Ha egy csomag kevesebb ugrással érkezik (W IDE2 -1), és a név nem magyarázza meg, a digitalizátor nyomkövetés nélkül továbbküldte, és a csomagot kidobták; a soha nem látott források nem számítanak;
- digitalizáló az út közepén: ha a két állomás között, többé-kevésbé az úton, van egy digitalizáló ugyanabból az aktív sávból az elmúlt három órában, az ugrást elvetik, mert az továbbterjedhetett volna (különösen a digitalizáló Lora esetében, amely az útvonal megváltoztatása nélkül továbbít);
- nem rádiós csomagok (qAC, qAX, TCPIP), amelyek egy IGate (harmadik fél), HF és különböző frekvenciák, állomások magasságában 1500 m (paloni, repülőgép) és ugrik több mint 1500 km.

A térképre gyakorolt hatás (1.3.4-től). Csak azt a területet vonzza, ahol a minimális garantált érték érvényes: minden útvonal központi része körül ± 60 km-es sáv (hossza 15-85% -a), nem pedig a végállomásokon, ahol előfordulhat, hogy a vezeték nincs jelen. A folyosók szalagjait egy semleges színű (a szöveg) és félig átlátszó: az átfedések nem állnak össze, és a szín nem téveszti össze a skála lila színét (21 dB és azon túl). A címkék (jellemvonások, átlagos távolság, minimális nyereség) egymással vannak szkennelve; az egérrel egy címkén a folyosó útvonalai jelennek meg.

A területen, és csak az aktuális idő közelében (± 90 perc), az előrejelzés nem csökken a minimális. Az érvelés: ha az állomások valóban átmennek, a terjedés ott van, bármit is mond az időjárás modell.

Mivel a rövid útvonalak keveset érnek (1.3.4-től). 2 m-ben, 300 és 600 km között, két jó állomás (mintegy száz wattos, antenna, FT8 vagy CW) csatlakozik szinte minden nap még a normál légkör, troposzcatter: a jel szórja a kis szabálytalanságok a levegő a közös térfogat a két köteg. Három vagy négy hely 500 km-re azt mondja, hogy a zenekar működik, mint mindig, nem azt, hogy van egy nyílás: GEKO Magazine n. 69 ( IU3QEZ) becslései a troposzcatterre vonatkozóan, amelynek 2 × 10 áramlási eleme körülbelül 440 km SSB QRP, 510 km SSB 50 W, 600 km CW QRP és 640 km SSB 500 W (források, tizenegy kör). Csak több mint 600- 700 km a troposcatter már nem elég, és egy sor párhuzamos kapcsolatok valóban jelzi a vezetéket vagy réteget. A 450- 600 km-es folyosó 9- 12 dB-re emelte az előrejelzést, és a térkép olyan javulást mutatott, amely nem volt ott.

A kiindulási érték ezért az útvonalak átlagos távolságával egyezik meg:

- 400 km-ig: 3 dB;
- 400 km-től 700 km-ig: só lineáris módon 3-tól 10 dB-ig (500 km → 5,3 dB; 600 km → 7,7 dB);
- 700-900 km: 10-16 dB; 900 km felett 16 dB marad.

A 7 dB alatt, ami a skála elején van, a minimum az marad, hogy: a folyosót a címkével együtt rajzolják, látható, hogy a sáv működik, de a térkép színei nem változnak (a doboz 3-7 dB-t mutat a szürke ponyvával). A 7 dB felett az értéket a független útvonalak számával (3 stroke → 0,25; 6 vagy több → 1) és a kapcsolatok középértékével (-20 dB → 0,5; 0 dB vagy több → 1; nem ismert, mint a klaszter pontokban, → 0,75) mérik, 7 dB-től kezdve. Példák: 3 S / N nélküli útvonal 500 km → 5,3 dB; 5 útvonal 615 km → 7,7 dB; 3 útvonal S / N nélküli útvonal 900 km → 11,5 dB; 3 útvonal -20 dB-ig 900 km → 10,4 dB; 6 útvonal 0 dB-ig 900 km → 16 dB.

A minimális távolság 300 km marad: 500 km-re emelve az igazi rövidtávú nyílásokat is eltávolította volna (például egy vezeték a tirrheni 400 km-en, amelyet az időjárás előrejelzése és a nagyon erős jelzések jobban felismernek, mint a távolból). Az I5MZY archívumban a 22 / 11 / 2026-on 10: 53 UTC-nél, ezzel a szabállyal és a sugárszűrővel a hosszabb folyosók 615 km (7,7 dB) és 540 km (6,3 dB) voltak; az 1.3.3-as szabályok szerint 2175 km és 16 dB-t értek el.

A minimum 1.3.3-ig a folyosó összes téglalapján érvényes volt, és csak a távolságtól (10 dB-től 300-tól 16 dB-ig és 900 km-ig) függött: sok hosszú folyosóval együtt (például 2026-ban 22 / 11-ig 12 folyosó 2620 km-ig) majdnem egész Európát 16 dB-re emelte.

A nyereség görbék már nem rajzolnak "gocce": a 220 km-es kisebb szigetek mindkét irányban színesek maradnak, de zárt görbe nélkül, hacsak egy folyosó nem erősíti meg őket. Kikapcsol a "Prevision mediálta foltok" -al.

## 5. Választható vonalak és összehasonlítás a nyomással és inverzióval

Gain görbék: 7, 10, 13, 16, 18,5 és 21 dB-es vonalak, a skála szélei, a mezőre merítve, a menetszögek módszerével, a címkével dB-ben.

Izobare: nyomás a tenger szintjén minden 4 azonos GFS, az óra előrejelzés, a központok H és L.

Termikus inverzió: a 925 vagy 850 hPa-nál a levegő mennyisége melegebb, mint a 2 m-nél, + 1, + 3, + 5 és + 8 ° C-on.

A kontrollok alatt egy sor adja meg a megszerzés megállapodás a nyomás és a fordított a látható területen: Pearson korrelációs együttható, -1 + 1. A csatornák gyakran születnek az anticiklonok (a levegő, amely leejti a hŠs és alakítja a fordított), így a pozitív megállapodás a nyomás normális; de belül az anticiklon a nyereség változik sokat között tenger, part és hegyek. A fordított önmagában nem elég: hajlítsa a hullámok csak akkor, ha a levegő felett is szárító [5]. A vonal arra szolgál, hogy most, melyik a két legjobb magyarázza a színes területek. Ugyanez az összehasonlítás szerepel a nyilvántartásban.

## 6. Potenciális csővezetékek: kettős számítás

A GFS-szintek 200 és 1000 m között mozognak: egy vékony inverzió terjed, és a modell nem látja a vezetéket. Ha két szint között az N-csökkenés meghaladja a normál értéket (40 N / km), a levegő stabil (3 K / km alatti termikus gradiens) és száraz (0,8 alatti páratartalom), a program egy lehetséges csatornát vizsgál és kétféleképpen számítja ki:

- Számítás A: az összes koncentrált felesleges csepp 50 m-ben.
- Számítás B: az adott terület csővezetékeinek jellegzetes vastagságán eloszló, az ITU- R P.453 [4] éghajlati térképéről (a talajon vagy a magasságban) vett csökkenés.

A hiány és a legnagyobb beszorult hullámhossz esetében a legalacsonyabb értéket kell tartani: ez egy óvatos becslés.

Az ITU- R P.453 adatok © ITU jellegűek, és személyes és nem kereskedelmi célokra is felhasználhatók a forrás megjelölésével, de nem terjeszthetők: ezért nem vagyok benne a programcsomagban. Ha be szeretné kapcsolni a B számítást, csak töltse le a P.453 térképeket az ITU-ról a lapon: a program, miután megmutatta a használati feltételeket, töltse le a csővezetékek archívumát az ITU oldaláról (emelt _ és _ felületi _ duct.zip) a felhasználó személyes használatára, vegye ki a fájlokat az Data / P453 -ban, és kapcsolja be a számítást anélkül, hogy újraindítaná. Alternatívaként töltsd le az ITU- R 3. tanulmányozócsoportból a digitális termék oldalát (itu.int, ITU- R 3. tanulmányozócsoport, digitális termékek, P.453 hang, felületi adatok és emelt csatornaadatok), és tedd be a hét S _ OCCUR-t. TXT, S _ TICK.TXT, S _ STREN. TXT, E _ OCCUR.TXT, E _ base.T A fájlok nélkül a program csak az A számítást használja, és azt a fülbe és a regiszterbe írja. A hiány A és a P453 jellemző intenzitása közötti arány anomália: A mai csővezeték hányszor erősebb a normálisnál. P453

## 7. Állomások és helymeghatározók

A főbérlőnek 4, 6, 8 vagy 10 karaktere lehet. 8 vagy 10 karakterrel a pozíció pontos, és a talajrész a Copernicus GLO- 30-ból származik: az antenna magassága elég a talajból. A 4 vagy 6 karakter a tér túl nagy: jobb írni a kvóta az antenna a tengeren; ha 0 a program marad a becslés a SRTM talaj közepén a tér plusz a magasság a földről, és jelzi azt az eredményeket. A megfelelő magasságban az alapmagasság 3 m a talajtól; a 0 tengeri résznél az RTM talajt plusz magasságot kell használni.

## 8. Föld, diffrakció és troposzcatter

Az útvonal profilja 30 méterenként (több mint 120 km széles) a Copernicus GLO- 30 útvonalon történik. Az egyenértékű földi sugár k = 157 / (157 + ΔN), ahol a ΔN a GFS profiloktól számított első km-ben az [6, 7] útvonal mentén az N csökkenése.

Az állomásokhoz közeli segélyek (1.3.2-től, a szerző kritériuma). P.452 fizet a horizont szögének lineáris, 0.573 dB / mrad: a megkönnyebbülés néhány tíz méter néhány száz méterre az antenna kerül tíz dB, sokkal több, mint megfigyelhető. Ezért az egyes állomásoktól 20 km-en belül lévő mentességek 6 dB-t tesznek ki: az ezek miatti látószög a legtávolabbi mentések és a 10,5 mrad-ok szögére korlátozódik. Ez érvényes az Ön állomására és a tudósítóra. A "Near Reliefs" sorban az áll, hogy hány DB szállt fel.

Földforrás (1.3.2-től). A kvóták a Copernicus DEM GLO- 30 [3] -ból származnak: 1 második ív, a tenger 0 és a part tiszta. A program 1.3.1-ig az AWS Terrain csempe 'skadi' dobozait használta. A part közelében ezek a terek összekeverik az SSRTM kis felbontású tengerfenék adatok és terjedjen a sziget részesedése a tengeren: körül Capri hozzon létre egy 80-100 m-es szellemgyűrűt a tengerparttól 0,5 és 1 km között. 2026. november 21-én az IC8TEM egy nem létező horizontról számolt be északnyugaton; a Copernicus-szal való pontos összehasonlítás megerősítette (94 m a Marina Grande-tól északnyugatra). Ez a + 2,6 ° -os hamis horizont körülbelül 26 dB troposzcatterbe került a parti állomásokra. A skadi táblák csak akkor maradnak tartalékként, ha Kopernikusz nem válaszol. Kopernikusz keretek súlya 15- 30 MB és töltse le egyszer; azok csak a tenger nem létezik, és értéke 0.

- Szabadtér: Lfs = 92,44 + 20 log f (GHz) + 20 log d (km) [8].
- Diffraction: Bullington method on the real profile plus ITU- R P.452- 16 § 4.2 [6] delta- Bullington gömbgrafikus földkorrekció, ITU- R P.526 [9] gömb veszteség.
- First Fresnel zone: sugar ‡ (λ · d1 · d2 / d); free if the ground leave minimum 0.6 of the radion. Minden akadálynál megjelenik az [9] Knife Board V és J (VV) paraméter..
- A profil kialakításában a közvetlen sugár optikai és piros, ha áthalad a föld alatt vagy a föld görbülete alatt; a piros pontok azok az akadályok, amelyek behatolnak az 1. terület Fresnel (lila pontozott vonal). A tengeren a 0 m magasságban lévő "vörös pont" nem hegy, hanem a görbület felé tartó tenger. Az egérrel az orsónál vagy a pontnál úgy tűnik, egy képregény magyarázattal és értékekkel.
- Amikor az antenna közelében a nyomáscsökkenés bezárja az irányt (például tíz fokos horizontot), a diffrakció és a troposzcatter több száz dB-t veszít: 300 dB felett a program "> 300 dB" -t és "egy szomszédos akadály által zárt irányt" ír, a "< -230 dB" jelet és az S / N arányokat "-60 dB" -t. Ebben az esetben ajánlatos az "Ajánlott növény" és a "Via monte".
- Troposcatter: ITU- R P.452- 16 eq. 45-50% az [6] idő, szögtávolság a horizonttól és N0 a profiloktól.
- Tropo nélkül: a diffrakció és a troposzcatter erejének összege, soha a szabad tér alatt.

## 8 bis. Troposcatter: négy modell és bizonytalanság

P.452 mellett a program három másik modellel számolja ki a troposzcattert, ahogy a TropoLink [16] program is, és bizonytalanságként mutatja a különbséget:

- NBS TN101 § 9 [25] (1.3.5-től), középérték: L = 30 log f -20 log d + F (θd) − F0 + H0 + Lgp. F (θd) a III. melléklet csillapítási funkciója analitikus formáiban (Ns 301) az Ns, H0 a frekvenciasebesség függvénye 9.2: 144 MHz-en, alacsony antennákkal a hullámhosszon, a tükörkép a talajból részben eltörli a mezőt a közös térfogat alsó részében és a veszteségsó 3-10 dB. Ns az óra GFS profilok, a hatékony horizontok és magasságok jönnek a Copernicus profil, Lgp = 0.07 · e ^ (0,055 (Gt + II)) a nyitó csatolási veszteség. Ez ugyanaz a forma, mint amit az ITM a belső, de a reaktivitás ma helyett az éghajlat. Az ITM-től 150 és 900 km között 24,4 dB-en belül, és körülbelül 2 dB-n belül, az TN101 9.2b egyszerűsített formájában, 144 MHz-en, antennákkal 20 m-en teszteli. H0

- ITU- R P.617- 5 [17] L = F + 22 log f + 35 log θ + 17 log d + Lc, f in MHz, θ in mrad, d in km, F = 0,18 · N0 · e ^ (-hs / 7,35) -0,23 · ΔN és Lc = 0,07 · e ^ (0,055 (Gt + II)). Az N0 -t és a ΔN-t általában az ITU éghajlati térképeiből veszik; itt minden órában használjuk a GFS profilokat ( N0 · e ^ (-hs / 7,35) a talajra ható reaktivitás, ΔN az első km csökkenése), így a troposzcatter is követi az időt.
- RSGB VHF / UHF kézikönyv Forma, DL2FZN [18]: L = Lfs + 24,44 + 4,283 Ln f + 0,06656 d. A meredekség a távolsággal majdnem megegyezik a JAPPER-rel (0,067 dB / km).

Közös térfogat (1.3.1-től): az a pont, ahol a két állomás sugarát keresztezik, mindegyik a látószögére hajlik, egy ezzel egyenértékű sugarú Földön, ae = k · 6371 km. A TX-ből a sugár z = hTX + x · θt + x2 / (2 · ae); A távolság x = (hRX − hTX + d ^ θr + d2 / (2 · ae) / θ, a θ = d / ae + θt + θr. Ugyanazon a kvótán sík horizontokkal és antennákkal a bázis értéke d2 / (8 · ae): körülbelül 1,3 km-től 300 km-ig, 3,7 km-től 500 km-ig, 9,4 km-től 800 km-ig. Minél nagyobb a térfogat, annál több a levegő száraz és ritkaság, és annál kevesebb terjed: ezért több mint 800- 1000 km a troposzcatter fut ki. Az eredmények az alap részesedése és helyzete, a reaktivitás, a gradiens és a páratartalom a GFS ezen a magasságban, és az egyenértékű távolság Bray (QST, November 1961): minden extra vagy kevesebb horizont ér körülbelül 111 km-re. Fegyelmi hivatkozás: IU3QEZ (GEKO Magazine n. 69) "A VHF horizontján túl" prezentáció.

A Troposcatter vonal, más modellek azt mutatják, hogy a két veszteség és bizonytalanság fele a különbség a legoptimistább modell és a legpesszimistább. Ezek csak összehasonlítások: a veszteség nélkül tropo marad P.452.

## 8 ter. délutáni troposzcatter a könnyítések felett (1.3.4.)

IK3XTV, Thiene ( JN55, az Alpok 10 km), működik nyáron délutánonként Dél-Németországban 400- 500 km, anélkül, hogy látna, erős jeleket, de gyors és mély evanescence; a szerző tulajdonított őket "konvektív sejtek egy vonat lencsék". A megfigyelés megerősítést nyert; a programban elfogadott magyarázat egy másik, fizikailag határozottabb.

Délután a nap ellepi a levegőt a mentések felett, és a határréteg 2-3 km-re emelkedik. A tetején a nedves levegő a fenti száraz levegő: ott a turbulencia a törésmutató (a méret, amely szabályozza a troposzcatter) maximális. Amikor ez a felső eléri a közös térfogatát az útvonal, amely 400-500 km körülbelül 2-3 km, a troposcatter erősítik. Ez magyarázza a délutáni időt, a nagy nyomást (csendes ég, erős fűtés) és a gyors evanescence, jellemző diffúzió, és nem a csatornák. A hagyományos sejtek nem lencseként működnek: turbulenciát okoznak, és az átdolgozott réteg tetején lévő turbulencia terjeszti a jelet.

Az előrejelzés következményei:

- A földön az első délután a legrosszabb az alacsony csatornáknak és rétegeknek, de nem a hegyeken túli troposzcatternek: a két dolgot külön kell olvasni;
- a program már rendelkezik a két rendelkezésre álló adatokkal, a modell által biztosított határérték-réteg magasságával és az útvonal közös térfogatának arányával (az eredményekben regisztrálja a "Common Volume" -t): amikor közel vannak, a délutáni hegyi falatozás jobb lehet, mint a számítás normál légkörben;
- 1.3.4-től a pontszámítás figyelembe veszi: ha a közös térfogat a földön van, az útvonal mentén legalább 800 m-es, a nap 15 ° -on van, a határréteg pedig meghaladja a 800 m-t, és a réteg felső része (talajszint + határréteg magasság) a közös térfogat alapjától számított 500 m-en belül van, a troposzcatter 3 dB-t nyer; 500 és 1000 m között a lineáris nyereség nullára esik. A "Troposcatter pomeridiano". A hó az útvonal mentén (legalább 10 cm a talajon a modell szerint, hó _ mélység változó Open- Meteo vagy SNOD a GFS a NOMADS) a megerősítés nem vonatkozik: a hó a talaj hideg marad, és az újraformázott réteg nem nő. A 3 dB érték ideiglenes, és valós kapcsolatokkal kell kalibrálni; az időjárás korrekciókkal kikapcsolja.

Forrás: F. Egano IK3XTV (Radio Rivista e www.qsl.net / ik3xtv, "Propagazione sulle 2 metri", "Celle convettive", "Troposcatter over the Alps"), 13.

## 9. Az útvonal mentén

Ez a jelszámítás lényege. A GFS profilokat az útvonal mentén 25 km-enként veszik. Minden nyomásréteg esetében ugyanazon réteg leghosszabb nyúlása egymást követő pontokban található; a csatorna csak vastag rétegeknek számít, legalább 40 m [10].

- folytonosság: a km-től a km-ig és az útvonal töredékéig;
- felülre és alapra, és dőlésszögre m / km-ben;
- talaj a rétegen belül: az a km, ahol a hegyek az alap fölé emelkednek;
- Fedett föld: a tengeren a csatorna szinte mindig, dönt a terület hossza [10]. Ha a földterület meghaladja az útvonal 10% -át, a földterület folytonossága és lefedettsége közötti minimális frakció a felhasznált.

A trope nyereség a használt frakció rétegének nyeresége. Ha a profilok több mint fele subrefraktív, a nyereség negatív.

## 9 bis. Időjárás-korrekció: stabilitás, süllyedés, sztrájk

A profil 1.3.0-tól három meteorológiai információt is tartalmaz, amelyek a nyilvántartásban (regave "stability (Misme)"), a Meteo vonalon az eredmények útvonalán és a Segni meteo dobozzal a térképen láthatók.

- A Misme stabilitása [5, 6. fejezet]. A Misme a látóhatáron túl egy összetett paraméterhez köti a mezőt: M = a · (ge -40 + b · ΔW), ahol a = 0,5 dB / N / km, az egyenértékű gradiens és a ΔW termikus stabilitás, a levegő 1 km-es emelésének munkája. A program a ΔW = g · g (θ − θ0) / θ0 dz talajszint és 1 km feletti magasság között számítja ki, a standard atmoszféra körülbelül 56 J / kg. A nyereséghez hozzáadott érték 0,07 dB J / kg a szabványon túl, -4 és + 6 dB között korlátozva, és csak akkor számol, ha a sávot egyetlen vezeték sem fogja be (a gradiens már a Bean és Dutton indexben van). A kalibrálás ideiglenes: az arányokkal kell ellenőrizni (14. szakasz).
- Alsó és felemelő [19]. Moler és Holden 125- 305 km-es útvonalon 1365 és 9380 MHz-en átlagosan 2,5 dB-es maximális jelet találtak, amikor a levegő a levegőben emelkedik, és ebben az esetben körülbelül 20 dB-es erősítést találtak a süllyedés kezdetén 3 órán át, amit 22 dB-es csökkenés követett, amikor a levegő visszatért a közelítésre. A GFS 850 és 700 hPa-ra adja a függőleges sebességet. Ha mindkét szinten a levegő több mint 1 cm / s-t esik, és a potenciális K törésmutató megfordítja vagy éles leesése következik be (N-t jelentettek 1000 hPa-ra, amely kiemeli a száraz és meleg levegőt, amely a felső [5] -ról esett le), 1,5 dB-t adunk hozzá; ha a levegő több mint 5 cm / s-t emelkedik, ha felszáll 2.
- [20] Scirocco. A szaharai forró és száraz levegő a tengeri levegő hidegebb felszínén nagyon erős inverziót ad a Földközi-tenger keleti részéről Gibraltárig. Az S jel akkor jelenik meg, amikor 850 hPa-nál a szél 100-200 ° -ról jön, legalább 7 m / s sebességgel, és a hőmérséklet legalább 16 ° C-on van a Földközi-tengeren. Ez csak egy jel: a nyereség határozza meg a rétegek a profil.

A sávot a teljes nyereség nem haladja meg a 16 dB-t, a sárga vége: a korrekciók erősebb rétegeket tesznek nyilvánvalóbbá, de nem haladnak át a csatornákon (piros és lila marad a csatornáknál).

- Perzisztencia (1.3.1-től). A legnagyobb nyílások után jó nyomású napok (VHF / UHF DX könyv; Biancomano és Leyshon 1998, szabály 12 óra; a jelentések 29 / 12 / 20212 / 1 / 2022 növekszik az első és a negyedik nap). A program az előző 24 óra óráit legalább 13 dB nyereséggel számolja fel, és 2 dB arányban összeadja, csak akkor, ha a nyereség már legalább 10 dB.

Hogyan kell olvasni a jeleket a térképen (Változás időjárás jelek; 1.3.2 a szimbólum nagy dupla, és halad az egérrel egy jel jelenik meg egy képregény magyarázatát és értékeit a sejt):

| Aláírás | Színes | Ez mit jelent? | A tropára gyakorolt hatás |
|---|---|---|---|
|  ↓  | zöld | süllyedés: légcseppek (anticiklon), sűrítéshez és szárításhoz szükséges hőenergia, erősíti a hátramenetet és szilárdan tartja | kedvező, + 1,5 dB |
|  ↑  | piros | felemelő levegő (elöl, alacsony nyomás, konvekció): összekeveri az alacsony rétegeket, és megszakítja az inverziókat és csatornákat | kedvezőtlen, -2 dB |
|  S  | narancs | Szirup: forró és száraz levegő Szaharából a friss tengeri levegő felett | kedvező nyom, nincs korrekció: határozza meg a rétegeket |

A terület tele zöld ↓ alatt egy anticiklon a klasszikus jele egy kemény nyílás; Red ↑ nyilak mentén a vonal azt mondja, hogy a rétegek törnek, bár a színe a sejt még mindig jó.

Az időjárás-javítók doboza kikapcsolja ezeket a javításokat, hogy lássa az 1.2-es modellt.

Az 593 MHz-es frekvenciasávon 2021. december 29. és 2022. január 2. között, ugyanazon útvonalakhoz képest és két héttel később, a leválasztási index (0,5 = none, 1 = perfect) 0,79 korrekció nélkül és 0,91 korrekcióval (a tető előtt 16 dB). Ugyanez az összehasonlítás mutatja a fő határértéket: a tengeren alacsony magasságban lévő csatorna, amely ezekben az időkben a Campania és Liguria közötti jeleket hozta, nem látható a GFS szintjén.

## 9 ter. 1.3.2 Hírek: két becslés, mennyezet, nagy nyomás, tenger, állomások, gerincek

Óvatos és agresszív becslés. A gradiensből származó nyereség 0 dB-ről 10 dB-ig a normál határig nő a szuperrefrakció küszöbértékéig. Az 1000- 850 réteg esetében az óvatossági küszöb a Bean és a Dutton (100 N / km); az agresszív küszöbérték a klasszikus, 79 N / km-es Super- Reffertion küszöb (k 2; P.453 használ - 100 N / km az első 100 m-ben), amely k50 = 157 / (157 − ΔN) k 2 (k = 2,5 = 94 N / km). A pont-pont számítás azt mutatja, hogy a két veszteség kiesett; egy vezeték, amely csapdába ejti a sáv egybeesik. A térkép óvatos.

Az utak és csatornák csak 2500 m-re vannak a föld felett: minél magasabbak az amatőr rádiók antennái, annál nem használják ki őket. 1500 m-rel a validálás 16 dB fölötti leválasztási pontot veszített, és a csatlakozások 4% -a 16 dB feletti, mivel télen az alámerülés inverziói gyakran 1500 és 2500 m között vannak.

Nagy a nyomás. + 1 dB, ahol a 9 sejt közül legalább 7 1020 hPa vagy annál nagyobb nyomást gyakorolt a tengerre, és a szél gyenge az alacsony rétegekben (10 m 5 m / s alatt, 850 hPa 8 m / s alatt), kivéve a növekvő mozgásokat.

Tengeri súly. A pontpontszámítás során a rétegek nyereségét megszorozzuk 0,8 + 0,4 · μ1-rel, ahol a μ1 az ITU- R P.452 (eq. 51) tényezője a leghosszabb, folyamatos szárazföldi szakaszon: 1 a nyílt tengeren, kis, hosszú szárazföldi szakaszokkal.

Állomások és végrehajtva. Amikor egy olyan csatorna, amely a sávot csak a vonal egy részére fogja fel, az antennák részarányát összehasonlítjuk a csatorna alapjával és tetejével a lefedett szakasz elején és végén; a nyereség szorzata 0,5 + 0,5 · -vel (e ^ (-ΔhTX / 300) · e ^ (-ΔhRX / 300)), Δh a csatornából való távolsággal. A vezeték szinte minden útvonalon a 10. § kapcsolása.

Réteg és gerenda. Ha a GFS határrétegének teteje (a leghatárosabb _ layer _ height modell alapjárata) meghaladja az útvonal borításait (300 m feletti maximális terület, ha a legmagasabb meghaladja az 500 m-t), a réteg törés nélkül áthalad a hegyeken: + 1,5 dB a borításos borítások töredéke esetén. Az IT9GSF megfigyelése Szicíliából Németországba kedvező tirrhénnel.

A tengerhez közeli szintek. A NOMADS forrás a program is letölti a felszíni hőmérséklet (a tenger az SST), hőmérséklet és páratartalom 80 m, és a réteg 30- 0 hPa a talajra. A tengeri pontokon (5 m alatti modellsebesség) ezek a szintek hozzáadódnak a profilhoz, az első nyomásszint alatt, hogy a csatorna 100-200 m-en legyen, amit a nyomásszintek nem oldanak meg. A földön nem használják: az éjszakai és a nappali rétegek még nem érvényesek. Az Open- Meteo csak felszíni hőmérsékletet ad.

Elpárologtató csővezeték. Az SST-től a hőmérséklet és a páratartalom alapján 2 m-re becsülik a párolgási csatorna becsült magasságát a semleges logaritmikus profillal (mint Paulus és Jeske modelljében, a stabilitás korrekciója nélkül): a víz hajának levegője telítődik az SST-vel (98% a só esetében),

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

korlátozottan 0 és 40 m között. A 144 MHz-es hálózat elfogásához körülbelül 180 m-re (Kerr), majd 144 MHz-re a párolgási csatorna alig erősödik; 432 MHz-ről felfelé, alacsony tengeri antennákkal (11 bis bekezdés). Adja meg a parabolikus egyenletet és a sorban helyesbítések 1.3.2.

A nap ideje (látogatás). A program minden ponton kiszámítja a nap magasságát. Napról napra a földön, a nap több mint 15 ° és a határérték réteg több mint 800 m / s, vagy a szél több mint 5 m / s, figyelmeztet, hogy a föld csatornák újraformázott; éjszaka, a nap alatt -3 °, szél alatt 3 m / s és határérték réteg alatt 300 m, figyelmeztet, hogy a derűs égbolt lehet végezni besugárzás, amely leesik után napfelkeltekor. Nem a tengeren. Jelenleg nem számok, hanem észlelik őket: a decemberi validálásnak nincs elég nyári napja, hogy kalibrálja őket. Ez két pont, amit a szerző fontosnak tart, a napi megfigyelésből.

normál profil CRPL (alternatíva). 40 N / km helyett a normál csökkenés a CRPL: N (h) = Ns · e ^ (-c · h), -ΔN az első km = 7,32 · e ^ (0,005577 · N) (Bean és Thayer 1959). Nedves levegővel (Ns high) a normális meredekebb, és erősebb gradiens, hogy nyereség; magasságban édesebb. Kiderült, hogy rossz, amíg a validálás azt nem mondja, hogy segít.

## 9-es. Kontraszt lég- és alacsony tengerek: az Adriai-tenger (1.3.3-tól)

A rádióhullámok nem érzik a tengerfenéket: érzik a kontrasztot a levegő és a víz felszíne között. A mélység azért számít, mert eldönti, milyen gyorsan követi a tenger hőmérséklete a levegőét.

Az Adriai-tenger alacsony. Az átlagos mélység körülbelül 250 m, de az északi medence ritkán haladja meg a 100 m-t, és a Trieszti-öböl 25 m alatt marad; csak délre esik 1200 m fölé. Egy réteg víz olyan vékony, hogy néhány nap alatt felmelegszik és lehűl, a tirreni hetekig. Három tipikus helyzet van:

1. Tavasz és nyár elején, kedvezően. A tenger még mindig hideg, még az Alpok és Po folyók édesvize miatt is; forró és száraz levegőt hoz a síkságból vagy a Balkánról. A hideg víz feletti meleg levegő megfordítja a tengert (advection csővezeték): az Olaszország, Szlovénia és Horvátország közötti nyílások klasszikus feltétele.
2. Ősz és tél borával, kedvezőtlen. Hideg levegő a melegebb tenger felett: az alacsony réteg instabil lesz, megnyomja és a csatorna eltörik.
3. Egy bora után, esély. Csak azért, mert a tenger alacsony, a bora néhány nap alatt lehűti. Ha ezt követően lép egy anticiklon forró levegővel, a levegő-tenger kontraszt erős, és a csatorna erősebb, mint általában.

Az Adriai-tenger szintén zárt csatorna az Appenninek és a Dinari között, kevesebb mint 200 km széles: a tengerszint továbbra is korlátozott, és a tengely mentén (Puglia-Venezia-Trieste, körülbelül 700 km) lévő útvonalak mind a tengeren maradnak, ami a legjobb feltétele egy hosszú csatornának.

Amit a program kiszámít. A tengeri pontok, az időjárás korrekciók és a doboz "Contrasto ária-mare":

| Feltétel | Korrekció |
| --- | --- |
| potenciális hőmérséklet 925 hPa-on (vagy 950) a 4 K-os SST felett | _ |
| 7 K SST-n túl | + 2 dB |
| levegőnedvesség 925 hPa-nál (80% felett) | -0,5 dB ezekre a bónuszokra |
| legalább 1,5 ° C-os hűtött tenger 72 órával korábban, kedvező kontraszttal | _ |
| 2 m 2 ° C-os melegebb légtenger (keverés) | -1,5 dB |
| 4 ° C-os melegebb tenger | -3 dB |

A teljes korrekció -3 és + 3 dB között marad, és vezeték nélkül a nyereség a sárga tető alatt marad. A küszöbértékek átmenetiek: a standard légkörben a potenciális hőmérséklet 925 hPa-ra meghaladja a tengerben mért 2-3 K-t, majd a 4 K máris fordított értéket jelez.

Az SST emléke. A modellek a tengerek hőmérsékletét majdnem állandónak tartják az előrejelzés során. A közelmúltban bekövetkezett csökkenés az előző három nap elemzéséből származik (Open- Meteo, csak felszíni hőmérséklet, csak tengeri sejtek): legfeljebb 72 órával az aktuális érték levonása előtt. Ha a szolgáltatás nem válaszol, a memória bónusz nem érvényes.

Határok. A part alatti és az Adriai-tenger felső részén található modellek SST-je durva; például a Po hideg tolla rosszul oldódik meg. A Batimetria nem adja meg a számítást: a források mindig megadják az SST-t, amely már összegzi a tengerfenék hatását. A légi-tengeri kontraszt részben átfedi egymást a Misme stabilitás végén (9. A küszöbértékek a helyszíni folyosókkal és a QSO-val az Adrián keresztül fognak haladni.

## 10. A teljes útvonalon

Ha a réteg a használt frakció legalább 80% -án csapdába ejti a sávot, és az állomások nem láthatóak, akkor a vezeték veszteségét is kiszámítják: szabad tér, plusz az antennák csatlakoztatása (3 dB, ha azok a vezetéken belül vannak, több, ha felül vagy alul vannak), plusz a maradék csillapítás (γd = 5 · 10 − 5 · ae · f ^ (1 / 3) dB / mrad az ITU- R P.452 eq. 49 [6]. Ha ez kevesebb, mint a veszteség nélkül tropo válik a várt veszteség.

Egy sáv csak akkor marad a vezetékben, ha a hullámhossza nem haladja meg az λmax = 2 × 2 / 3 értéket (ΔM · 10 − 6) · d értéket (Kerr, eq. 17 [11]). Ehhez a réteghez a 23 és 70 cm, de a 2 m nem. A határérték azonban nem nettó: Kerr figyelmeztet, hogy még hullámok több alkalommal is szenvednek egy csomó a vezetéket, és Hepburn és a VHF / UHF DX könyv beszél "sphered" szélek és "force" csatornák. A program 1.3.1-től puha vezetéket használ: ha az λmax az λmax és 3 · λmax között van, a nyereség fokozatosan 15 dB-ről (λ = 3 · λmax) a teljes csatornanyerésre (λ = λmax) jut, s tényezővel (λmax / λ − 1 / 3) / (2 / 3). Ebben az esetben a csatorna színe megengedett. A napló és a Meteo vonal az útvonal mentén jelzi a pontokat puha csatorna.

## 10 bis. Via Monte

Amikor a tudósítót bezárják a horizont mögött, a program a legjobb irányra törekszik, hogy az antennákra mutasson: a megvilágított lejtő radar célként terjed. A látóhatár körforgását minden ötödik alkalommal megcsinálja (kivéve a levelező körül ± 15 ° -ot), és minden irányban a megvilágított horizontpontot veszi át, 100 m felett, 1 és 60 km között. A gyors becslés (szabad tér, bitaktikus konverzió és 10 dB minden egyes horizontszintnél a diffúzortól a levelezőig) a 4 legígéretesebb, egymástól legalább 15 ° -os értéket választja;

- kezelés 1, állomás → megkönnyebbülés, a szabad térben;
- Kezeli 2, megkönnyebbülés → levelező, kiszámítva, mintha a megkönnyebbülés továbbított 10 m felett a tetején, azonos előrejelzés (nélkül parabolikus egyenlet);
- a bistatic radar egyenlet konverziója a következő képlettel: R1

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

Az "Ajánlott pont" sor a legjobb útvonal azimutját jelzi, ha a közvetlen útnál kevesebbe kerül. 144 MHz-en, 20 km-re és 15 ° -tól egy yagi-val, a hegyi út mintegy 45 dB-vel többe kerül, mint csak a 2-es útvonal: csak akkor történik meg, ha a közvetlen a szomszédos akadályok lezárják. A becslés bruttó (± 10 dB), különösen a σ0 esetében.


Anélkül, hogy veszteség a hegy (lehetőség, 1.3.2). A "Via monte senza perdite up al monte" dobozzal az állomás és a diffrakciós pont közötti esést null-nak tekintik: olyan, mint az állomás csúcsra juttatása (antenna 10 m-re a pont felett, ugyanaz az energia, antenna és kábel). A horizont ezt követően a legszabadabb látószögű pontot választja a levelező felé (paritáson, a legmagasabban), és az eredmény mutatja az útvonalat a ponttól a levelezőhöz, a jel és az S / N mellett a teljes szórással. Ez egy optimista limit: azt mondja, ha az irány nyílik, amikor a hegy emel a jel jól, vagy hova kell menni hordozható. Minden pont a tízes karakterrel van megadva.

## 10 ter. Belépés egy távoli csatornába

Ha a csatorna, ami csapdába ejti a sávot, az állomás horizontján túl kezdődik, a jelnek meg kell érkeznie. A program a kiindulóponthoz hozza a jelet, a vezeték közepén, a diffrakció: végig a Copernicus profil GLO- 30 kutat a szélén a legmagasabb fresnel paraméter (ITU- R P.526), a duzzanat az egyenértékű föld,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

a felső görbületnek (lekerekített akadály, P.526- 16 § 4.2) tulajdonítható csillapítás, a Copernicus GLO- 30 talajtól ± 1 km-re mért R sugara mellett,

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

(m · n > 4 esetében az ajánlás 34b formája). Barsis (NBS, 1963) három hosszú útvonalon mérve a hegyeken túl (Colorado, Alaszka, Európa) átlagosan 10-20 dB-vel magasabb, mint a Fresnel- Kirchhoff ideális széle: a kerekítés sok mindent megmagyaráz (Barsis és Kirby, 1961: Pikes Csúcs körülbelül 11,5 km sugarú). Az eredmény a sima gömbös talajon végzett diffrakció; ez a legrosszabb. Ezt követően a jel a vezetékben halad: szabad tér a teljes útvonalon, a p.452 maradék csillapítása a vezetéken lévő szakaszon (3 dB belül, 1 dB 10 méterenként kívül). Ha ez a veszteség alacsonyabb, mint a várt veszteség, akkor a mechanizmus ("magatartás (input by diffraction)") lesz. Ez a szerző feltevése az olasz földrajz: a közeli akadály jóval a vonal alatt, v negatív, és a bejárat költsége kevés; a háti elülső költségek 15- 30 dB.

## 10 quater. ITM (Longley- Rice) és kijárat a csatornából

A program 1.3.2-től tartalmazza az NTIA / ITS (Longley and Rice, ESSA ERL 79- ITS 67, 1968; Hufford, Longley és Kissick, NTIA 82-100, 1982) rendszertelen terep modelljét, amelyet az Egyesült Államok kormányának hivatalos kódexe, a jogoktól mentes C # -ra fordított. A fordítás ugyanazokat a számokat adja meg, mint az eredeti: automatikus összeállítás összehasonlítja azt minden alkalommal NTIA C + + kód öt vizsgálati esetben (lent 0,01 dB).

Az ITM a Copernicus GLO- 30 valódi profilján a normál atmoszféra elvesztését számítja ki: a talajból való visszaverődéssel, a diffrakcióval (a talajban a Δh egyenetlenséget mérlegelve, azaz a legkisebb négyzetek vonala körüli kvóták 10-90% -os különbségével) és a troposzcatterrel. A földi reaktivitás az útvonal ( $N_s = N_0\,e^{-h/9460}$) átlagos részére csökken, és az egyenértékű terület görbületét adja,

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

A program a kiválasztott útvonal rádiós éghajlatát használja, mint a 10. § -ban (1.3.4-ig mindig a mérsékelt tenger, N0 320 a földön és 350 a tengeren), az N0 -val a hely átlagos reaktivitásából (talaj εr 15, a 0,005 S / m; tenger εr 81, az 5 S / m), a vízszintes polarizáció és a változékonyság idővel anélkül, hogy a hely (a profil ismert). Ez azt mutatja, hogy a veszteség meghaladta 50% -ban az idő, és az egyik, hogy van 10% -ban és 1% -ban a legjobb időben. Az ITM nem modellezi a csatornákat: az időbeli változatossága statisztikai, az éghajlat alapján. A P452 melletti alapérték második számítása; a kettő közötti különbség bizonytalanságot ad.

A második használat az utolsó szakasz után egy vezeték, hogy vége előtt a megfelelő. A kilépési ponton egy virtuális antennát helyeznek el a vezeték felénél, és az ITM kiszámítja a traktust egészen a megfelelő szintig, mint a szabad tér fölötti többletet az idő 50% -ánál. Teljes veszteség

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,	heta_{condotto} + A_{ITM}(d_{uscita} 	o RX)
```

ahol A _ TX az antenna csatlakoztatása (3 dB belül, 1 dB 10 méterenként kívül), ha a csatorna a közelben kezdődik, ellenkező esetben a 10. Ez az a határ, amit a szerző megadott magának: a réteg folytonossága és legfeljebb a maradék csillapítás. Ha alacsonyabb, mint a várt veszteség, akkor válik a mechanizmus ("magatartás (kilépési és végső trash ITM)").

## 10 perc. Mélyedés: szigetek, szorosak és a Föld-tenger folytonossága

Egy példa a Nápolyi-öbölből (Anacapri, JN70CN) a Földközi-tenger nyugati része felé a következő útvonalakon:

| Rendeltetési hely | Azimut | Ahol átkel a szigeteken | Akadály |
| --- | --- | --- | --- |
| Barcelona |  279°  | Szardíniától (Gallura) északra 41,05 ° -ig N, 420 és 490 km között | 30 km-re délre a Boche di Bonifacio; a Limbara (1359 m) mintegy 20 km-re a vonaltól |
| Bocche di Bonifacio |  282°  | é. sz. 41,3 ° közelében | Körülbelül 11 km |
| Marseille |  296°  | Közép-Korzika, é. sz. 42,2 ° | hegyek 2700 m-ig: egy vezeték 2 km alatti ott megszakítja |
| Valencia, Balearics |  260–265°  | Közép-Szardínia | Belső mentességek |

Barcelona számára elég, hogy 282 ° -ot mutasson 279 ° helyett, mert a tengely áthalad a szájból: 3., a gerendán belül egy normál yagi.

A száj szoros? Az alacsony csatornáknak néhány száz méter alatt, igen: a szigetek falazva vannak, és a föld felett nappal a fűtés megtöri a fordulatot. A szoros maga is elég széles: körülbelül 11 km, szemben az első terület Fresnel, kevesebb mint egy kilométer széles közepén egy 1000 km-es útvonal 144 MHz. A probléma az időjárás. A Mouths egy móka a szél: Az alsó réteget felgyorsítjuk és eltávolítjuk. Az átjáró gyakran nyitva, mint geometria, de zavart, mint a légkör. A stabil anticiklon és a kis szél helyett lehet a jó átjáró.

Földi-tengeri egyenlőtlenség. Számítanak. A tirreni parton kialakult csatorna megszakad: a jel a csatornából jön ki (a kilépés utáni szakaszból, 10-es kátrány), és vissza kell térnie egy másik csatornába a Balearics tengerén, ismét kifizetve a csatlakozót. Az 1500 m feletti magasságban lévő csatornák anélkül haladnak át a Gallura felett, hogy észrevennék, mert Korzika központi része nem érvényes.

A gyakorlatban: ha az útvonal áthalad egy hegyvidéki szigeten, próbáljon meg némi korrekciót tenni a szabad tenger felé, de ne számítson arra az útra, ahol erős szél fúj.

## 10 szex. TN101 rádiós klíma és időingadozás (1.3.5-től)

Az TN101 § 10 [25] azt írja le, hogy a horizonton túlnyúló útvonal elvesztése hogyan változik a középérték körüli hosszú időszakban, az éghajlat különböző görbéivel: az Y (q) függvény jelzi, hogy a veszteség hány dB-rel csökken a legjobb idő q% -ában. Az ITM ezeket a görbéket tartalmazza hét éghajlatra. Az éghajlat sokat számít: a tengeren a mérsékelt tengeri éghajlat, 144 MHz, 10% -ban az idő kapsz sokkal több dB, mint a szárazföldön a kontinentális éghajlat, mert a csatornák gyakrabban.

A program már nem javítja az éghajlat: Az átlagtól kezdve minden alkalommal ezt választja, így az egész világon működik, és nem csak Európában. Letöltés Open- Meteo [26] három éves napi ERA5 reanalízis (hőmérséklet, páratartalom és talajnyomás, eső) a sejt 0,5 ° az átlagos pont, csak egyszer; értékek maradnak Data / CLIMA. A talaj helyreállítása

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

nap mint nap, majd az éves átlag és kirándulás között havi átlagok, átlagos páratartalom és éves eső. A szabályok az TN101 10.2. § szerinti éghajlatra vonatkozó leírásokat követik; az egyik klíma és a másik közötti határok a szerző kritériumai:

- szélesség 60 ° felett: mérsékelt kontinentális (az TN101 -nak nincs görbéje a sarki éghajlatra);
- évi 250 mm-nél kisebb eső és 50% alatti átlagos páratartalom: sivatag (N jellemző 280);
- az egyenlítőtől 20 ° -ig, Ns legalább 350 és legfeljebb 30 kirándulás: egyenlítői (N jellemző 360);
- 32 ° -on belül és legalább 60: szubtrópusi kontinentális, azaz monszun (tipikus kirándulás 60- 100);
- 10 ° és 32 ° között Ns legalább 340: szubtrópusi tengeri (N jellemző 370);
- mindkét vízkör a tengeren, több mint 20 °: tengeri temperamentum a tengeren (ez az TN101 meghatározása);
- legfeljebb 30 tengeri kirándulás (a tenger az útvonalon vagy legalább 75% -os átlagos páratartalom): a tengeren edzett tengerek;
- egyéb mérsékelt kontinentális (tipikus kirándulás 20- 40).

N0 az ITM-re az átlagos csökkent tengerszint, N0 = Ns · e ^ (h / 9460). Hálózat nélkül tartalékszabályt használsz, csak a szélességi fokot és a tengeri törtet, és a vonal ezt mondja.

ERA5 2023- 2025 adatokkal végzett vizsgálatok: Capri Ns 339 a 45-ös kirándulással (kontinentális temperamentum a szárazföldön, tengeri temperamentum a tengeren, amikor mindkét horizonton a tengeren van); London 327 és 16 Bécs 319 és 24 Abidjan 381 és 11 (egyenlítő); Miami 365 és 36 (szubtrópusi férj); Delhi 337 és 82 (szubtrópusi kontinentális); Riad 278 és 37, 90 mm eső (sivatag). Chilében: Antofagasta 341 és 19 (szubtrópusi férj: az Atacama partja, a Csendes-óceán majdnem rögzített alacsony hátoldalán), Calama 232 a földön, 2257 m, 14 mm eső (sivatag), Valparaíso 331 és 11 (hőmérséklet varrat), Punta Arenas 313 és 5 (hőmérséklet varrat).

A "Radio Climate ( TN101)" vonal az éghajlatot, az okot, az N-eket a kirándulás felével, az Y-t: hány dB-t keres az idő 10% -ában és 1% -ában, és hány veszít 90% -ban. Hosszú távú statisztikákról van szó, és az előrejelzéstől elkülönítve marad: a várható veszteség a GFS-profilokkal együtt következik be, és a két dolgot kétszer ugyanannak a kóros terjedésnek számítanák.

## 11. Összehasonlítás ITU- R P.452-vel

A P452-es vonal az ITU módszert alkalmazza a rétegeken lévő csatornákra és tükörképekre (P.452- 16 § 4.5 [6]), a GFS ΔN mediánjával a klimatológiai egy helyett, és p = 20% kalibrációs állandóként, mint a CSIRO [10] vizsgálatban. Ez csak egy összehasonlítás: a várható veszteség a (9) bekezdésben foglaltak szerint marad.

## 11 bis. Parabolikus egyenlet (1.3.1-től)

A Parabolic Equation Line egy negyedik számítás, csak összehasonlítás, készült, mint a professzionális programok a csatornák (PETOOL; a hullámterjedési keret M. Lytaev, MIT licenc): ahelyett, hogy a nyereség becslésére a rétegek, ez előre a hullám mentén a split-step módszer Fourier az M módosított refraktív profil a GFS, vett az előrejelzés pont közelebb minden távolságra. A GFS szintek hozzáadják a lehetséges csatornák becsült ugrásait (A, 50 m félrétegben). A talaj Copernicus GLO- 30 belép lépéseket (a föld alatt a mező nulla), a talaj vezető és a sugár az antenna egy Gaussian, a becsült nyílás a nyereség. A számítás a vezetékeket, a veszteseket, a diffrakciót és az antennák csatolását látja; ő nem látja a troposzcattert, amely külön van hozzáadva. A következő a "profil csekk": ugyanaz a standard légkör kevesebb, mint az egyik a várt profil.

1.3.2-től:

- a szaporító széles szögben van, a szabad tér pontos fázisával:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- a tenger alja és a part menti sík zéró magasságban a fresnel vízszintes polarizációs együtthatójával, εc = εr -j · 60 · λ · · (a tenger εr = 81 és az 5 S / m; a 13. és a 0,006 S / m), az útvonal tiszta szögében:

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- a tenger egyenetlensége csökkenti a Miller és Brown szerinti következetes visszaverődést, a hullámok tényleges magasságával a széltől 10 m-re (σh = 0,0051 · U2):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- a tengeren az M profil magában foglalja a párolgási csatornát (9. bekezdés ter) a semleges logaritmikus profillal:

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

Teszteli a prototípust (10 m antenna a tengeren, nyereség a standard légkörben 100- 300 km): Az elpárolgási csatorna 1103 dB-t ad 144 MHz-re, 8- 37 dB-t 432 MHz-re és 21- 66 dB-t 1296 MHz-re. A 144 MHz-es vízszintes polarizáció mellett a tenger szinte fémként tükröződik (a 124; a 124; a 124; a több mint 0,99-től néhány tized fokig), és a durvaság alig számít: az új alsó rész néhány tized dB-t változtat; a 432 MHz-es szintről többet számít.

Határok: a föld felett a függőleges profil a GFS szintjeinél marad, így a vékony csatornák láthatatlanok maradnak; a talajt a lépcsőkhöz közelítik; a gerenda a hatodik szinten marad.

## 12. antennák, jelzés és zaj

- Yagi DL6WU: 9, 5 + 7, 8 · log10 (boom / λ) dBd (2,35 dB a dupla bumm, DL6WU intézkedések VHF Communications 3 / 1982), plusz 2.15 az [12] dBi.
- EIRP = teljesítmény - kábel + TX nyereség.
- Jelzett = EIRP - várható veszteség + nyereség RX - kábel RX. S- méter IARU: S9 = -93 dBm, 6 dB egységenként.
- Külső zaj szerint ITU- R P.372 [13]: galaktikus (Fa = 52 − 23 log f) több mesterséges szerint a környezet (város, lakóhely, vidéki, csendes vidéki) vagy kézzel írt antenna hőmérséklet.
- Tsys = Ta + Tpre+ [290 (Lc -1) + 290 (Fr -1) · Lc] / Gpres az antenna termináljaira (Friis formula). 1.3.5-től a vevőállomásnak mindig van egy előerősítő antennájában, rögzített paraméter: 0,8 dB zajszám (Tprep = 59 K), és 20 dB-t nyer. Mögötte van a kábel (Lc veszteség) és a rádió, az alap zajszám Fr 6 dB. 1 dB kábelrel a kábel és a rádió bemenete 1160 K-ról 12 K-ra csökken: a zaj határozza meg az eget, a környezetet és a preamp-et. A 20 dB nyereség a program által választott jellemző érték.
- S / N az SSB-ben, CW-ben és FT8 -ban minden irányban, a küszöbértékhez képest.
- Valószínűségi időszak (1.3.2.): az egy órán belüli evaneszcencia az S / N normál eloszlása a várható átlagos óránkénti érték körül. Bray (QST, 1961. november, 39. o., Norton 1959 görbéjén) 13,5 dB amplitúdót ad körülbelül 100 mérföldön keresztül, azaz körülbelül 7 dB-rel az átlag alatt és 7-tel az átlag felett, ami majdnem 100 mérföld alá esik (3,5 dB-től 50 mérföldig); a "szilárd" kapcsolat 7 dB-t vesz igénybe a költségvetésből. A ± 7 dB értéket 160 km-en belül kell megadni, az alábbi távolsággal arányosan. Annak valószínűsége, hogy egy időszak meghaladja az út küszöbét,

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  normál eloszlási funkcióval: határértéke 0 dB = 50%, + 3,4 dB = 84%, + 7 dB (Bray "szilárd" határértéke) = 98%. Nem foglalja magában a modell nagyobb bizonytalanságát.
- Digitális módszerek: S / N 2500-ban Hz az 50% -os dekódolásnál mért értékekhez képest, előzetes információ nélkül (AP). FT8 -21 dB és FT4 -17,5 dB a fehér zajra; a troposzcatter esetében a "zavarodott szélességi közeg", FT8 -18,6 dB és FT4 -15,2 dB [21] küszöbértékeket kell alkalmazni. Q65 -15C -22,2 dB, Q65 -30B -24,8 dB, Q65 -60A -27,6 dB A repülőgép szórása a Doppler meghaladhatja a hangok Q65 -60A (1,7 Hz): vannak jobb 30B vagy 15C. JT65A -25 dB [22]. A dekódoló AP (a már ismert QSO neve vagy vége) 2 dB-t ad az FT8 -ra és 1,5-2,6 dB-t az Q65 -ra; nem adja meg a küszöbértéket, mert nem érvényes az összes FT8 -ra gyakran olvasható "-24 dB" értékre a WSJT- X kapcsolatok vagy AP dekódolás határa, nem pedig az 50% -os küszöbérték. [24]

## 13. Válaszidő és adatidők

Minden számítás a saját idejét méri: GFS profilok (hálózat, elemzés, kötelező szünetek között a tételek az Open- Meteo limit), Copernicus GLO- 30 terep (a letöltés a táblák, 15- 30 MB minden, csak az első alkalommal), számítás. A válaszidő sor összefoglalja őket; a regiszterben a sorok TEMPI-vel kezdődnek. A hálózat korszerűsítésekor a nyilvántartás az alkalmazott GFS-versenyt is jelenti, amikor elérhetővé vált, és az adatok hány évesek.

1.3.2-től választhat ICON- 2I-t, az olaszországi ItaliaMeteo és Arpae Ügynökség modelljét az Open- Meteo (modell italia _ meteo _ arpae _ icon _ 2i, naponta két verseny, 3 nap) segítségével. Csak Olaszországra és a közeli tengerekre terjed ki, és a GFS (1000, 925, 850, 700, 500 hPa) alacsonyabb szinttel rendelkezik: a magasságban lévő csatornák esetében nem jobb, mint a GFS, de a finomrács a 2 m-es kút, a szigetek és a tenger, azaz a párolgási csatorna. Azok a változók, amelyekkel egy modell nem rendelkezik (korróziós határérték, a felület hőmérséklete...), eltávolodnak a kéréstől. 1.3.2-től az automatikus forrás csak NOMADS-t használ; az Open- Meteo akkor szolgál, ha a NOMADS nem reagál, és történelmi esetekben. A "Frissítés a saját" mezőben (alapértelmezés szerint megvilágítva) az előrejelzést kattintás nélkül töltik le: a nyitáskor, minden új GFS versenyen a NOMADS-on (kb. 5 órával a verseny kezdete után), és amikor a látható terület több mint egy negyedre esik ki a rácsból; legfeljebb 15 percenként, soha egy történelmi esettel.

Kérések Open- Meteo van limit per perc, óra és nap minden internetes cím. A letöltött profilok minden stroke-hoz és modellhez a lemezen (Dati / cache- gfs) maradnak: ugyanazzal a versennyel, amely 6 óránként változik, a már letöltött pontok nem kérnek többet. Az automatikus forrás indul a NOMADS és Open- Meteo csak a hiányzó pontokat szolgálja; az egyetlen Open- Meteo, a határidő vagy a napi megáll azonnal, és azt mondja, mikor próbálja újra. Történelmi ügyek nem kerülnek zárolásra.

## 14. A modell jóváhagyása

Az előrejelzést össze kell hasonlítani a valós kapcsolatokkal. A számolónapló minden számot megtart; a program archívuma hirdetéseket és dekódolást tart. A 432-es és az 1296-os MHz-es frekvenciára, ahol kevés a hely, az üzemeltetőktől kell jelentést kérniük: akik kapcsolatba léptek, mikor, milyen jellel. Hasonlítsa össze sok órán át a várt nyereség a mért jel azt mondja, ha a küszöbértékeket kell mozgatni.

## Forrás

1. NOAA / NCEP, globális előrejelzési rendszer (GFS), globális modell 0,25 °, napi négy futam.
2. Open- Meteo, prediction API (open- meteo.com), gfs _ context model, változók nyomás szinten és versenymetaadatok.
3. Copernicus DEM GLO- 30 (ESA; WorldDEM © DLR e.V. 2010- 2014 e © Airbus Defense and Space GmbH 2014- 2018, amelyet az Európai Unió és az ESA a Copernicus részeként szállít), 1 második ív, GeotifF keretek az Amazon Web Services nyílt nyilvántartásában. Reserve: NASA / USGS Copernicus GLO- 30 in Terrain Tiles "skadi" of AWS.
4. ITU- R P.453, A rádiós refraktív index: formula és fénytörési dátum, digitális csatorna térképekkel (3. tanulmányozócsoport, © ITU, személyes és nem kereskedelmi felhasználás).
5. B. R. Bean, E. J. Dutton, Radio Meteorology, NBS Monograph 92, 1966: Table 6.5 and § 4.4.
6. ITU- R P.452- 16, Prodication procedures for the assessment of interferencia between the stations on the surface of the Earth at condition about 0.1 GHz: § 4.2 (diffrakció), eq. 45 (troposcatter), § 4.5 and eq. 49 (végrehajtott).
7. ITU- R P.834, A troposzferikus fénytörések hatása a rádióhullámok terjedésére.
8. ITU- R P.525, A szabad tér csillapításának kiszámítása.
9. ITU- R P.526, Szaporítás diffrakció: eq. 31 (cutcheon) és § 3 (gömb alakú föld).
10. B. Indermuehle, H. Suzuki, Tropospheric Ducting Prediction based on GFS Model Data at Inyarimanha Ilgari Bundara, the CSIRO Murchison Radio-astronomy Observatory, Publications of the Astronomical Society of Australia, 2026 (arXiv 2608.12861).
11. D. E. Kerr, Prepagation of Short Radio Waves, MIT Radiation Laboratory Series, 13, 1951: eq. 17 e 18.
12. G. Hoch DL6WU, "Extremely Long Yagi Antennas", VHF Communications 3 / 1982, pp. 130 et seq., fig. 1 (432 és 1296 MHz a robbanás hossza szerint).
13. ITU- R P.372, Rádiózaj: galaktikus és mesterséges zaj a környezet számára.
14. NOAA / NCEP, NOMADS, GFS 0,25 ° -os (nomads.ncep.noa.gov) szűrője; WMO G RIB2 formátum, komplex csomagolás térbeli eltérésekkel.
15. Open- Meteo, Történelmi előrejelzés API (modell előrejelzés archívum 2021 óta).
16. LukiBox, TropoLink, design troposcatter útvonalak P.617, NBS TN101 és ITM flanked (GitHub, Apache 2.0 licenc).
17. ITU- R P.617- 5, Szaporítási előrejelzési technikák és adatok szükségesek a transzhorizon rádiórelé rendszerek tervezéséhez, 2019.
18. RSGB, VHF / UHF kézikönyv; DL2FZN implementáció (andi-f / troposcatter on GitHub, 2013).
19. W. F. Moler, D. B. Holden, "Tropospheric scatter propagation and atmoszféra circations", Journal of Research of the NBS 64D, 1960, doi 10.6028 / jres.064d.014: signation within the horizont (1365 and 9380 MHz, on the sea) and vertikal move of the air.
20. ARRL Handbook 2022 (99a ed.), § 19.4.3-19.4.4: időjárás és troposzferikus terjedés.
21. S. Franke, B. Somerville, J. Taylor, The FT4 and FT8 Communication Protocol, QEX July / August 2020.
22. W3SZ, küszöbértékek a lassú digitális üzemmódok ( JT65, JT9, QRA64) és a választott üzemmód szerint szaporítás, 2019.
23. QST, "The World Above 50 MHz", November 2022: opening of 14- 15 August 2022 between Manitoba and Kansas / Missouri.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Quick- Start Guide to Q65, 3 April 2021 (wsjt.sourceforge.io).
25. P. L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis, Transmission Loss Predictions for Tropospheric Communication Circuits, NBS Technical Note 101, magazine 1967: § 9 (troposcatter, function F (θd), H0), § 10 and Annex III (radio climates and variability időrend).
26. Open- Meteo, Történelmi Időjárás API (ECMWF ERA5 felülvizsgálat óta 1940; Copernicus Climate Change Service, CC BY 4.0 licence).
