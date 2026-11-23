LIVETROPOVHF · 2. műszaki dokumentum
# A térkép felépítése

> Automatikus fordítás olaszul: kétség esetén érdemes az olasz útmutató.

Ahogy a térkép és a helyszín térképe megszületik: a földrajzi alap, a prognózis, a Hepburn és F5LEN indexek átlaga, az izobare, a foltok és a lehallgató állomások, rétegekkel átfedve. 2026. november 9-i változat.

## Összefoglalva:

- A térképet a program nyolc rétegben tervezte, mindegyik [8] dobozzal.
- Bázis: Természetes Föld partvonala és határai két szinten [9] részlet; [10] piac és azimut előrejelzések.
- Fizika: inverziók, reaktivitás N és gradiens dN / dh, ólomküszöb -157 N / km [3] [4] [7] [11].
- Torokindex: Hepburn [1] és F5LEN [2] térképek kalibrált olvasása, közös rács és a két forrás átlaga.
- Izobare: GFS [12] nyomás keresztül Open- Meteo [15], vonalak minden 4 hPa, H és L központok.
- Spot, lehallgató állomások (sötét rózsaszín körök mindig látható rózsaszín vonalak) és személyes dekódolás piros.
- Olvasás az átfedés: nagy nyomás, nagy csatornák, nyomásesés, növekvő SNR, [3] [4] [13] [14] hegyi útvonalak. [5]

## 1. Az elv: egy rétegelt térkép

A térkép nem rögzített kép: a program által tervezett, réteges, réteges, amikor a zoom, vetítés, sáv, előrejelzési idő vagy téma megváltozik. Minden rétegnek van egy doboza a legfelső réteg sávjában a térképen, így csak azt tudod tartani, amire szükséged van, és mindent együtt látsz, amikor a teljes képet akarod. A parancs, alulról felfelé ez:

|  #  | Út | Tartalom | Casella |
|  1  | Alaptérkép | Tengeri, szárazföldi, tengerparti és határmenti területek | Igen. |
|  2  | Index tropo | Media Hepburn + F5LEN, vagy csak egy forrás; állítható átláthatóság | Igen. |
|  3  | Grilllokátor | Fields, square and, with zoom, square | Igen. |
|  4  | Izofer | Nyomásvonalak 4 hPa-onként, H és L központok | Igen. |
|  4a  | Hőmérséklet 850 hPa | Izotermikus kezelés 4 ° C | Igen. |
|  4b  | Termikus inverzió | Narancssárga minta, ahol a levegő a magasságban melegebb, mint a földön | Igen. |
|  5  | PSKReporter | Fogadó állomások PSKReporter (sötét rózsaszín körök) és, rózsaszín, mit hallanak | Igen. |
|  6  | Dekódoltam | Dekódoló állomások WSJT- X, JTDX vagy MSHV: piros vonalak a QTH | Igen. |
|  6a  | QSO desunti dal live | Zárt kapcsolatok ( RR73, 73, RRR) olvashatók a dekódolásaiban: zöld vonal a két állomás között | Igen. |
|  7  | Spotlight | Kapcsolatok: teljes kék vonal, sárga fedett légszóró, két golyó | Igen. |
|  7a  | Kizárások | Casella 100 km-en kívül: a legrövidebb forgalom nem húzható és nem szerepel a listán. Casella Több mint 1000 km: csak négyzetek (rajt): ha az egyik állomás több mint 1000 km-re van a QTH, vonalak vagy körök nélkül, csak a helymeghatározó négyzetek az állomások érintett orgona kezelt 45 ° | Igen. |
|  8  | QTH és címkék | Az állomás, a kiválasztott hely neve | mindig látható |

A tropo index közvetlenül az alap felett és alatt minden más: egy színes minta, és a magasabb helyezett fedi a vonalakat és szimbólumokat. A rács áthalad rajta, így mindig tudod, hol van egy színes terület. Az izobares vékony vonalak, és jól olvasható a mintavétel.

## 2. Az alaptérkép

A költségek, határok és földterületek az [9] köztulajdonban lévő természetes Föld-vektor adataiból származnak, az általános nézet 1: 50 millió, nagyítás esetén pedig 1: 10 millió nagyságrenddel. A fájlok a program mappában vannak: nincs online térkép szolgáltatás. Színei a téma Clear: világoskék tenger # E2ECF8, sötétkék part, világos szürke határok. A témák Fekete és éjszaka a színek a bázis elsötétült, a többi téma enyhén festik.

## 3. A két előrejelzés

Minden réteg ugyanazon vetítési funkción megy keresztül, így mindig egymáshoz igazodnak. Két [10] -prognózis közül választhat (λ hosszúság, szélesség, R-sugár a Földön):

Market. Helyi sarkok és formák tárolása; A távolság a pólusok felé nyúlik. Vágás ± 85 ° szélességi fokra.

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

Azimutale equidtanti centred a téren. A központ (φ0, λ0) a központja a WWL tér. A központtól mért távolságok és irányok igazak: egy 1200 km-es állomás 1200 km-es léptékű, és az északi szög az azimut, amelyre az antennát irányítani lehet. Amikor megváltoztatod a lokátort, a térkép újra fókuszál.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

Az azimutáléban a sorok, hogy a papír egyenes (meridiánok, párhuzamok, szögletes szélek) görbülnek. Ezért minden sor rövid jellemvonásokra oszlik, minden szakasz kivetül, majd a vonások egységesek; ugyanez vonatkozik a tropo index széleire és az izobare-ra is. A pont, hogy az antipodák a központ nincs meghatározott helyzetben, és nem látható.

## 4. Grill lokátor, zoom és mozgás

A Maidenhead rácsot a program határozza meg: a 20 ° × 10 ° -os mezők, a két nagy és halványult betűvel a közepén, a négyzetek 2 ° × 1, és elegendő zoom subquadrati 5 ′ × 2,5 ′. A vonalak kék és vékony, nem fedi a tropo index.

Nagyítás a kerék közepén a mutató (a pont alatt az egér állandó marad), mozgó vontatás, dupla kattintás, hogy visszatérjen a teljes nézet, + és − billentyűk, és a kulcs a százalékos az egész nézet. Részletek illeszkednek a zoom: finomabb partszakaszok, szubnégyzetek, vastagabb izobare címkék.

## 5. Miért inverziók hajlítsa a hullámok

Az alacsony légkörben a hőmérséklet általában csökken körülbelül 6,5 ° C / km; A törésmutató függ mind a hőmérséklet és az [3] nedvesség, és ahol jelentősen változik a magasság a hullám hajlik a földre, és érkezik tovább, mint a normál. A legfontosabb réteg az, hogy akár 3 km, ahol inverziók, csatornák és konvektív sejtek [4] jönnek létre; tengeri utak kedvelik, mert a víz tömegek alkotják nagy területek [4] inverzió. [3]

A méréséhez használt méret: N, azaz (n-1) · 106 [11]:

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

T a hőmérséklet Kelvin, P a nyomás és a gőznyomás, mind hPa.

Számolja a változatos N részt (dN / dh). A standard légkörben körülbelül -39 N / km a fénytörést normálisnak tekintik, a szuperrefrakció mellett, és az [7] szubrefrakció pozitív gradienseivel. -157 N / km alatt a hullám jobban görbül, mint a Föld görbülete, és csapdában marad: ez az [11] csatorna. Ezek azok a feltételek, amelyeket a Hepburn és az F5LEN térképek próbálnak megjósolni.

## 6. A tropo index: két térképről az átlagra

Hepburn és F5LEN fülek a vezérlő asztal: mutassa meg a képeket letöltött oldaláról minden forrás, a változó előrejelzési idő, és szolgálja kalibrálni az olvasást kattintva a referenciapontok és színek a legenda. A tropo index réteg addig üres marad, amíg a két kalibrálás közül legalább az egyik nem aktív és nem teljes.

Hepburn. A Hepburn index azt méri, hogy mennyi hullámot kell egy területen hajtogatni, 0-tól 10-ig terjedő lineáris skálán, ami szintén meghaladhatja a 10-et. 0 a standard légkör és csak szórt; 12- marginális vagy gyenge szuperrefrakció; 3-4 szuperrefrakció csatorna területek; Az index nem veszi figyelembe a scatter tropo és nem látja az [1] hegyek. A térképek 6 napra szólnak: az első 36 órában 3 óra telik el, majd 6 óra, naponta 18 UTC [1] -ra frissítve; a kiindulási adatok (hőmérséklet, páratartalom, nyomás) a kanadai [1] időjárási szolgáltatás globális modelljéből származnak. [1]

F5LEN. Az Európa F5LEN 31 térképet tesz közzé + 3-tól + 192 óra (3 óránként legfeljebb + 48, 6 óránként legfeljebb + 96, majd 12-enként), a címben érvényes dátummal és idővel, a jobb alsó gyártási idővel és egy osztálylegendával: Disturb, Normal, Minor, Weak, Good, Strong, Vy Strong, Duct [2]. Az F5LEN világelőrejelzését az N egység légtörési indexe alapján számítják ki, 40, 3 órás (körülbelül 5 nap) lépésben, naponta 4 alkalommal frissítve; a térképen egy kattintás mutatja az [2] pont átlagértékét és lokátorát.

Folytatom. Minden előrejelzéshez a program ezeket a lépéseket hajtja végre:

1. Kibocsátó. A két előrejelzés képeit a végrehajtható melletti gyorsítótárba töltjük le, egyszer frissítjük a forrást, nem pedig áttervezésre. A végleges változat előtt engedélyt kell kérni Hepburn és F5LEN.
2. Automatikus georeferencia. A program megtalálja a keretet (két vízszintes és két függőleges vonal azonos színű), és a Windows 10 / 11-en, az OCR integrálásával a tengelycímkék (54N, 10W, 0, 5E...). Vágjuk el a szalagokat a keret körül, nagyítsuk ki őket, vizsgáljuk meg mindkét jelet, ha az N / S / E / W betű nem olvas, és tartsuk meg azt a vonalat, amelyen a legtöbb címke esik: a rossz értékek kimaradnak. Innen lehet kapni az extrém late / lon a keret és a kivetítés típusa (normál grill vagy Mercator). A szélsőségek megmentik magukat, és újraolvassák magukat, ha a keret megváltoztatja a méretét. A Mac és Linux, nélkül OCR, gyári végtagok vagy kézzel írott is érvényes a lapon. Alternatívaként olyan térképek esetében, amelyek nem rendelkeznek olvasható tengelyekkel: Minden térképtípus (Hepburn Europa, F5LEN világszerte...) kalibrációval rendelkezik: néhány, ismert koordinátákkal rendelkező vezérlőpont (meridiánok és párhuzamosok keresztezése, felismerhető bordák) csatlakozik a pixelekhez a szélességi és hosszúsági fokhoz, a forrás által használt kivetítésben. A kalibráció a program melletti fájlban van, és újra lehet készíteni, ha a forrás megváltoztatja a formátumot.
3. Színolvasás. Minden pixelt össze kell hasonlítani a forráslegendával, és át kell számítani a legközelebbi numerikus értékre. Az eredeti térképre rajzolt költségeket, írásokat és rácsokat "nincs adat" -ként ismerik el, és a közeli értékekkel töltik meg.
4. Világháló. Minden kép, amint a keretet megtalálják és megismerik a szélsőségeit, a Földön helyezkedik el; a világ bármely pontján az érték az azt fedő képekből származik. Ha több régió lefedi ugyanazt a pontot, érdemes részletezni (több pixel per fok), feltéve, hogy az idő 1,5 órán belül, hogy a választás; majd a legközelebbi óra, majd a legújabb verseny. Ahol nincs kép, ahol a térkép szürke fátylat mutat: nincs adat, ami eltér a 0 indextől. A kereten belül egy pixel legenda nélkül 0 (normál atmoszféra); partszakaszok, határok és írások ugrottak a legközelebbi szín a 3 pixel.
5. Ugyanaz a létra. Hepburn már a skálán 0-10 [1]: sötét háttér = 0 és tíz szín sorrendben (lila, kék, vízzöld, mész, sárga, ocher, narancs, piros, rózsaszín, világos rózsaszín) 1... 10, az orgona mögött. F5LEN osztályokat használ (Disturb, Normal, Minor, Weak, Good, Strong, Vy Strong, Duct) [2], de néhány pontosan Hepburn színek: Gyenge = 1, erős = 5, Vy Strong = 7, Duct = 8; Minor 0,5 és jó (zöld) 3.5. A skála szorzó, az F5LEN lapon, korrigálja a szettet, hogy hasonlítható Hepburn.
6. Ugyanakkor. Az idősáv + 0 óráról + 192 órára megy 3 órás lépésekben. Minden egyes lépésnél minden forrásból a legközelebbi időpontban érvényes az előrejelzés; Ha egy forrás nem rendelkezik ezzel a lépéssel, a média csak a másikat használja, és a legenda ezt jelzi.
7. Média. A Média gomb minden pont megéri (Hepburn + F5LEN) / 2; Hepburn vagy F5LEN látod csak egy forrás, hasznos megérteni, ha a két előrejelzés nem ért egyet.
8. Rajzolok. Az érték a legendaskálával van színezve (0 kék, 2 zöld, 4 sárga, 6 narancs, 8 piros, 10 lila), és a rács alatti mintavételre vetítve. A 0 átlátszó, így ha nem történik semmi, akkor látod a tiszta alaptérképet; az általános átláthatóság a doboz melletti kurzorral igazodik.

A spot tab, az index mentén az átlagos rács értékek bajnokságok 10 km-enként a maximális kör között a két állomás.

## 7. Izofer

A magas stabil nyomás az első jel, amit keresni kell az [3] trópushoz, és a csatlakozások az IK3XTV elemzett Alpokon túl szintén magas [4] nyomás.

1. Data. Tengerszint feletti nyomás a NOAA [12] globális GFS modelljétől, az ingyenes Open- Meteo [15] szolgáltatáson keresztül 3 ° × 3 ° (30 ° -tól 72 ° -ig) rácson N, a ny. h. 30 ° -tól a k. h. 48 ° -ig) a következő 78 órában. A program frissíti az adatokat a startup, majd 6 óránként, mint a modell verseny.
2. Vonalak. Az izobarét 4 hPa-onként (... 1012, 1016, 1020...) nyerik a rácsból a menetes négyzetek (menetes négyzetek) algoritmusával, majd enyhén lekerekítik és kivetítik a vonattal.
3. Centrum. A legjelöltebb helyi maximumok és minimumok H (kék) és L (piros) jelöléssel és hPa-ban kifejezett értékkel vannak ellátva.
4. Címkék. Az érték jelenik meg az egyes izobara, a színes háttér a térkép, hogy olvasható felett a mintavétel.

Terület és lépés. Az adatokat az Ön által megtekintett területen töltik le, 25% -kal bővítve, az 1-től 20 ° -ig terjedő lépésben 400 pont alatt maradnak (az Open- Meteo minden pontot kérésnek számít: 600 / perc, 5000 / óra): a térkép bővítésével vagy kitolásával a program már letöltötte a legsűrűbb újratöltést, legfeljebb 10 percenként; ha a frissítés nem sikerül, az előző adatok maradnak, és 3 perc múlva újra próbálkozni fog. Így az izobare és a hőmérséklet bárhol működik a világon.

Hőmérséklet. Ugyanez a kérés 2 m-re, 925 hPa-ra és 850 hPa-ra emeli a hőmérsékletet. LA0BY azt mutatja, hogy az esemény 22 / 11 / 2018 UA6 a hőmérséklet térképek (900- 3000 m), de nem Hepburn [3] előrejelzések; DF5AI és DK5YA használ függőleges hőmérséklet profilok felismerésére [5] inverziók. Ehhez két réteg van:

- Hőmérséklet 850 hPa: az izotermék 4 ° C-on (piros a nulla felett, kék a nulla alatt, lila a nullán).
- Termikus inverzió: narancssárga mintavétel, ahol a levegő 925 vagy 850 hPa melegebb, mint a 2 m; minél intenzívebb, annál erősebb. Ez a fizikai állapot, hogy hajlítja a hullámok a földre [3] [11], és hasonlítsa össze a tropo index és izobare mutatja, ha az előrejelzések meteorológiai bázis.

## 8. Spotok és állomások

Spot. Minden hely mindkét ismert pozícióban egy sor a maximális kör között a két állomás, osztva szakaszok és kivetített; A Mercatore akkor úgy tűnik, akkor kissé görbült, az azimutale ez egyenes, ha kezdődik a központtól. Kék vonal tele a normál helyszínen, sárga boràtott sötét éle a légoszlató (< AS >, < ACS > vagy "airscatter" a komment), labda minden állomás, fény halo a vonal alatt, hogy távolítsa el a mintavétel. A kiválasztott hely sokkal gyakrabban az utolsó és a két nevet.

Hallgatom. Minden PSKReporter Az [16] fogadó állomás egy sötét rózsaszín üres kör. Minden egyes kezdettől kezdve teljes rózsaszín vonalak felé az állomások, amelyek dekódolták, és visszaküldték a hálózatba, mindig látható kattintások nélkül, a nevek mindig meg van írva. A kör kiválasztásával a vonalak vastagabbak. A hely és a hallás csak a listában kiválasztott sávot és tartományt mutatja: a sávszélesség megváltoztatása a térkép üres és újratervezett, az új sávhoz már összegyűjtött adatokkal.

Én döntöttem el. Minden állomás, amely dekódolja a WSJT-X, JTDX vagy MSHV (receive via UDP) egy piros vonal tele a QTH az állomás, golyóval és jelöléssel. Ez a legközvetlenebb összehasonlítás az előrejelzéssel: megmutatja, hogy a vevő valójában hol jön most.

## 8 bis. Színek és szimbólumok

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

Melyek a 45 ° -os orgona négyzetek. A forgalom távol a QTH érdeklik, hogy megértse, hol a terjedés nyitott, de a vonalak, több száz kilométer hosszú és gyakran tízes, fedje le a térképet, és elrejteni, mi számít az Ön számára: a nyílások lehet kihasználni az állomás. Ezért, a kiválasztott koronán túl, a program nem rajzolja a vonalakat, hanem színezze a helymeghatározó négyzetek (2 ° × 1 °) az érintett állomások. Az eredmény az "aktív zónák" térképe: ahol sok közeli lilák tér jelenik meg, van egy csatorna vagy egy nyílás, még akkor is, ha ez nem érint közvetlenül; Ha a terület szélesedik felé, ez a jel, hogy irányítsa az antennát abba az irányba. A 45 ° -os műszerfal lehetővé teszi, hogy a tropo index és a rács alatt látható legyen, tehát egy pillantásra hasonlítsa össze az előrejelzést (alsó szín) és az igazi forgalmat (illus). A QTH tér soha nem színezik, mert ez a kiindulópont és nem egy távoli terület. Csak a koronán túl fekvő négyzetek színesek: a korona által vagy belül keresztezett négyzet akkor is szabad marad, ha a két állomás egyike van. Hogy lásd a vonalakat, csak kapcsolja ki a dobozt, vagy válasszon egy nagyobb koronát.

A kritériumok, amelyek meghatározzák, hogy mi jön a térképen, és mit dobnak el, egyenként magyarázzák az 1. dokumentum 11.

## 9. Az idősáv

Az alábbiakban a térképen: a bemutatott előrejelzés dátuma és ideje, kurzor + 0 és + 72 h 6 óra, index legenda 0-10 a kiválasztott forrás nevével, és ideje a modell verseny izobare. Trumpet és izobare index követi a kurzor; foltok, állomások hallgatás és a dekódolás megfigyelése a pillanat, és mindig azok "adex". Amikor a kurzor nincs a + 0-on, attenuálódnak, hogy ne felejtsük el, hogy nem tartoznak az előrejelzés idejéhez.

## 10. Hogyan kell olvasni az átfedés

- Nagy nyomás + magas index + pont mentén színes terület: a nyitás van, és az egyik tervezett. Ideje ebbe az irányba telefonálni.
- Magas index, de nincs hely: vagy senki nem aktív ott, vagy előrejelzés túl optimista. A hallgatási réteg segít megkülönböztetni: ha vannak vevőkészülékek a területen, és nem érez semmit, a második hipotézis valószínűbb.
- Hosszú foltok a színes területeken kívül: magas csatorna, amit a felületi előrejelzések nem látnak, mint az LA- UA6 [3] esemény; vagy egy másik szaporítás megszökött a szűrőről (pl. E sporadic címke nélkül). DF5AI és DK5YA különböztesse meg a tropo és E sporadic is az órától: a nyílások, amelyek születnek, és végül együtt egy diffúz E sporadic aligha [5] tropo.
- Hepburn és F5LEN nem ért egyet: hasonlítsuk össze a két forrást a gombokkal, és nézzük meg, hol esnek le az igazi foltok.
- Nyomás lefelé: G4MCU követte a nyílást az SK6VHF jelző, amíg a nyomás esett; 1012 hPa figyelembe vette a befejezett esemény [13]. Az idősáv görgetésével figyelje az izobare-t: ha a nagy nyomás visszahúzódik, a nyílás valószínűleg azt követi.
- Felfelé mutató jelek: a 144 MHz-es KP4MD -n végzett WSPR-vizsgálatban a jel / zaj arány közvetlenül az ablakokon nőtt, ahol Hepburn javulást jósolt, és az [14] -ra vonatkozó előrejelzés során csökkent. Ha ugyanazon az úton nő, a nyílás kialakul.
- Hegyi utak: az index nem veszi figyelembe az [1] könnyítéseket, de a csatlakozások az Alpokon túl is létezik köszönhetően konvektív sejtek és hullámok atmoszférikus gravitáció [4], és LA0BY dolgozott 9A és S5 a hegyeken keresztül anélkül, hogy Hepburn megjósolta volna [3].

## 11. Nyitott pont

- A Hepburn és az F5LEN engedélyével használhatják a térképeiket a programban.
- Konverziós együtthatók kalibrálása F5LEN → skála 0-10.
- Bármilyen térkép ellenőrzési pontjainak kalibrálása, ha a forrás grafikát változtat, akkor újra kell készíteni.
- A GFS nem kötelező útvonala a magasságban (850 hPa).

## Fő források

1. W. R. Hepburn, DX Info Centre, "The Hepburn Tropo Index (HTI)" és "Troposferic Ducting Forection" oldalak (dxinfocentre.com). Index meghatározás és skála, negatív értékek és több mint 10, kivéve a szórások és könnyítések, lépéseket 3 és 6 óra 6 nap, frissíteni 18 UTC. A globális kanadai modellből származó adatok eredete: G. Meisse, AMSAT- BB, 27 / 08 / 2007.
2. F5LEN, előrejelzés Európára (tropo.f5len.org / preciasts- for- European) és a VHF tropopheric propagáció világszintű előrejelzése (tropo.f5len.org / WW), és "A VHF tropopheric propagációs előrejelzési eszköz új változata". Unit index N, 40 lépésre 3 óra, 4 frissítések naponta, érték és lokátor egy kattintással.
3. S. Heck LA0BY, "Extreme Tropo propagation on 144 MHz and up", 41 ° Nordic VUSHF Meeting, Skjeberg 2019. Normál gradiens 6,5 ° C / km és inverziók akár 10- 15 ° C / km, hőmérséklet és páratartalom szerepe, magas stabil nyomás, LA- UA6 esemény látható magas hőmérsékleten és nem Hepburn, LA- 9A / S5 kapcsolatok a hegyeken túl.
4. F. Egano IK3XTV, "Propagation VHF - The contribution of convective cells and atmoszféra gravitation waves in the Tropo propagation". Fontos az első 3 km, tengeri utak, konvektív sejtek, gravitációs hullámok az Alpokon, nagy nyomású kapcsolatok, közös térfogat akár 384 km.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, "Nagyon hosszú távolság propagáció a 144 MHz sávban", 24 / 7 / 2003. Függőleges profilok hőmérséklet és harmat pont felismerni inverziók, használata Hepburn index, különbséget tropo és E sporadic az óra szerint.
6. J. N. Gannaway G3YGF, "Tropospheric Scatter propagation", QST November 1983. Földsugár-egyenérték és az indexhez és a spot kártyához használt útvonal elvesztése (lásd az 1. dokumentumot).
7. A VHF / UHF DX könyv 2. fejezete "Troposzferikus szaporítás", 2-23. A fénytörési zónák dN / dh (standard -39 N / km, határértékek -79 N / km és 0), valamint a felszíni és magas csatornaprofilok szerinti ábrája. A benyújtott másolat szövege nem olvasható: csak a számokat használták.
8. A projekt összefoglalója 8 / 11 / 2026, a szerző a program. Térkép színek, szükséges előrejelzések, rétegek, idősáv.
9. Natural Earth, vektoradatok part és határ, közterület (naturaleadata.com). Általános hivatkozás, nincs mellékelve.
10. J. P. Snyder, "Map Projections - A Working Manual", USGS Professional Paper 1395, 1987. Mercator kivetítések és azimutale equidtani képletek. Általános hivatkozás, nincs mellékelve.
11. ITU- R P.453 ajánlás, rádiórefraktív index. Refraktív képlet N és csatorna küszöbértéke -157 N / km. Általános hivatkozás, nincs mellékelve.
12. NOAA NCEP, globális előrejelzési rendszer (GFS). Tengerszint feletti nyomás 0,25 °, napi négy futam. Általános hivatkozás, nincs mellékelve.
13. G4MCU, "troposzferikus" címkével (g4mcu.net / tag / troposzferikus) ellátott cikkek, 2018- 2025. Az SK6VHF jeladó 1012 hPa-nál (2019. június) követte a nyomásesést.
14. C. F. Milazzo KP4MD, "VHF / UHF WSPR Propagation Study" (qsl.net / kp4md / 144 _ mhz _ wspr.htm), 2012- 2014. A Hepburn által előre jelzett javítási ablakokban 144 MHz-re nőtt a WSPR-jelentések SNR-je, és az előrejelzés megszűnésekor csökken.
15. Open- Meteo, időjárás előrejelzés API (open- meteo.com), ingyenes nem kereskedelmi használat. A GFS-modell tengeri szintű nyomása egy kérésben több ponton.
16. mqtt.pskreporter.info, disztribúció MQTT valós időben a reklámok a pskrister. info: a címzettek jelölése és bérlője (lásd az 1. dokumentumot).
