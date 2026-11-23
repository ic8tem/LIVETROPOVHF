LIVETROPOVHF · 3. műszaki dokumentum
# Hogyan alakítsuk a tropét 144 MHz-ben és azon túl

> Automatikus fordítás olaszul: kétség esetén érdemes az olasz útmutató.

Mi történik a levegőben, amikor a 144, 432 és mikrohullámok megy túl a horizonton: fénytörések, inverziók, csatornák, scatters, és hogyan kell olvasni őket a program térképen. 2026. november 9-i változat.

## Összefoglalva:

- A VHF hullámok és azon túl az utazás az első kilométernyi atmoszféra (troposzféra) és hajlít, ahol a törésmutató változik az [3] [7] [11].
- Az indexet törési N-nel kell mérni, amely a nyomástól, a hőmérséklettől és az összes [11] nedvességtől függ.
- Normál légkörben körülbelül 40 egység / km; ha több mint 157 egység / km, a hullám egy [7] [11] csatornában marad.
- A vezeték egy termikus inverzió száraz levegő felett és nedves alatt: anticiklon, tenger, nyugodt éjszaka, [3] [4] [5] [13] frontok.
- Minél magasabb a frekvencia, annál vékonyabb a csatorna elég: a mikrohullámok gyakrabban nyílnak meg, mint a 144 [3] [7].
- Csatorna nélkül marad a troposter scatter: mindig jelen, gyenge, hogy esik a távolság [6].
- A programban: nagy nyomás + fordított + Hepburn / F5LEN magas = valószínű körülmények; a foltok megerősítik [1] [2] [13] [14].

## 1. Ahol ez történik: a troposzféra

A troposzféra a légkör legalacsonyabb rétege, mintegy 10- 12 km. A trópusszám elsősorban az első 3 km: vannak vízgőz és inverziók, és ott megy át a rádióútvonalak között állomások a földön [4]. Normál körülmények között a hőmérséklet körülbelül 6,5 ° C / km [3] magassággal csökken. Amikor viszont, egy rétegben, a hőmérséklet emelkedik a magassággal, ez a kiindulópontja az összes erős tropo nyílásnak.

A VHF-eket nem az ionoszféra tükrözi, mint a HF-et (salve és sporadic, amely más órákkal és egyéb távolságokkal rendelkezik [5]): a horizonton túl csak akkor jönnek, ha a légkör a földre hajítja vagy szétteríti őket.

## 2. Reffrakció és refraktív N

Egy rádióhullám áthajlik a levegőn légtörésmutatóval egy másikkal. Az n index nagyon közel van az 1-hez, így az N reaktivitást használjuk, azaz n -1 szorozva egy millió [11] -val:

```math
N = (n - 1) \cdot 10^{6} = \frac{77,6}{T} \left( P + 4810 \, \frac{e}{T} \right)
```

P a hPa, T a kelvin hőmérséklete és a hPa a vízgőz nyomása. N talaj általában 300-400. A második kifejezés, hogy a gőz, sokat nyom: ezért a páratartalom számít, mint az [3] [11] hőmérséklet.

Ami meghajlítja a hullámot, az nem N értéke, hanem hogyan változik a kvótával, a gradiens dN / dh:

| dN / dh (N / km) | Helyzet | A hullámra gyakorolt hatás |
| 0 | Subrefraction | A hullám távolodik a földtől, a rövidebb horizonttól. |
| 0 és -79 között | Normál (standard - 39) | Fényhajlás a földre: rádió horizont körülbelül 4 / 3 az optikai egy |
| -79 - -157 | Szuperrefrakció | A hullám egyre jobban kanyarodik, növekszik az áramlási sebesség. |
| kevesebb mint -157 | Tartalom | A hullám úgy hajlik, mint a Föld vagy több, és csapdában marad. |

A -157 N / km-es küszöb az a pont, ahol a sugár görbülete megegyezik a Föld [7] [11] görbületével. Ezért az [6] Scatter kiszámításához használt 8497 km (a valós sugár 4 / 3-a) "egyenértékű földi sugár".

## 3. Inverziók: a tropo motor

A -157 N / km alatti gradiens megköveteli, hogy az N néhány méter magasságban összeomoljon. Ez akkor történik meg, ha egy réteg friss és párás levegő melegebb és száraz: a hőmérséklet emelkedik, és a gőz esik össze, és mindkét lehozza N [3] [5]. Az LA0BY legfeljebb 10-15 ° C / km inverziót jelez a 6,5 ° C-os [3] normál csökkenés ellen.

Az inverziók különböző módokon jelennek meg:

- Az anticiklonokban. A magas nyomáson a levegő a magasságban lassan csökken, összenyomódik és felmelegszik, míg a földön friss és nedves levegő marad. Ez az oka a hosszabb és hosszabb nyílások, amelyek az utolsó napokban [3] [4] [13].
- Éjszakai sugárzás. A nyugodt ég és a gyenge szél a talaj hűl éjszaka, és hűti a kontakt levegő: Alacsony fordított, hogy törik a reggeli nap. Esti és reggeli megnyitók, tipikus nyár és ősz.
- Tengeri figyelmeztetés. Meleg és száraz levegő jön a földről egy hidegebb tenger: a tengerszint marad nedves és hideg alatt. Innen a nagy tengeri útvonalak (Atlanti-óceán, Földközi-tenger, Északi-tenger) [4] [13].
- Határok. A meleg front előtt a meleg levegő átfolyik a hideg felett; a nyílás rövid, és követi a homlok.
- Elpárologtatás. A tenger fölött, az első méterekben a páratartalom gyorsan csökken a magassággal: nagyon vékony csatorna, csak mikrohullámoknak hasznos.

A jel, hogy egy fordított látható a függőleges profilok (radiosondaggi): hőmérséklet emelkedik és harmatpont megy le ugyanabban a szakaszban [5]. A program ennek nyomát mutatja a Thermal Inversion réteggel (a levegő 925 hPa melegebb, mint a földön) és az izotermikus 850 hPa.

## 4. Felület- és nagycsatornák

A csatorna olyan réteg, amelyben a hullám a felső széle (inverzió) és a talaj vagy az alsó széle közötti ugrással halad át [7]:

- Felszíni vezeték: föld vagy tenger. Az alacsony magasságú állomások, különösen a parton, közvetlenül lépnek be.
- Nagy vezeték: magasságban áll, például 500 és 2000 m között. A síkságon lévő állomások nem érezhetik magukat, míg a hegyekben vagy a hegyekben lévők belépnek a csatornába. Magyarázza meg a linkeket, amelyek keresztezik az Alpok vagy más [3] mentesít [4].

A csatornába való belépéshez a hullámnak majdnem vízszintesnek kell lennie: alacsony antennák a horizonton, és nincs szomszédos akadály. A csatornában a csillapítás sokkal alacsonyabb, mint a szabad térben, és a távolságok elérik az 1000- 2000 km és azon túl (például az Ibériai-félsziget, a Kanári-szigetek és Anglia, vagy Skandinávia és Dél-Oroszország közötti kapcsolatok) [3].

## 5. Gyakoriság: miért nyílnak a mikrohullámok gyakrabban

Egy csatorna csak a vastagságához képest elég rövid hullámokat képes befogni. A hosszú hullámok "jönnek ki" egy vékony csatorna, A rövidek nem. Aztán [3] [7]:

| Band | Wavelurus | Amire szükséged van |
| 144 MHz |  2 m  | Vastag csővezetékek (centinaia méter), széles és jól jelölt verziók: kevésbé gyakori, de nagyon kiterjedt nyílások |
| 432 MHz | 70 cm | Vékony szélek: gyakran nyílik, amikor 144 csak "áruk" |
| 1296 MHz | 23 cm | Néhány gyenge inverzió; hosszú útvonalak a tengeren gyakori nyáron |
| 10 GHz | 3 cm | Csak a párolgási csatorna a tengeren; szintén számít az elnyelését eső |

Ezért egy szerény tropo index már jelenthet nyílások UHF és mikrohullámú sütő, míg a 144 magasabb értékeket igényel. Másrészről, ugyanebben a csatornában a 144-nek kevesebb vesztesége van a vezetéken kívül, és jobban lefedi a határ menti területeket.

## 6. A Troposfer scatter

Még inverzió nélkül is a troposzféra nem egységes: A kis hőmérséklet és páratartalom szabálytalanságai az energia egy kis részét a horizonton túlra terelték. Ez a troposzferikus scatter, mindig jelen, amely lehetővé teszi a kapcsolatok 300- 800 km jó [6] állomások. A jel gyenge és ingadozó; a veszteség növekszik a diffúziós szöggel, azaz a távolsággal és a horizonton lévő akadályokkal:

```math
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
```

a diffúziós szöget fokban és f-ben MHz-ben rögzítik [6]. A program ezt a képletet használja minden egyes hely fülében, hogy megmondja, hogy egy link magyarázható-e a scatter-rel, vagy szüksége van egy vezetékre (lásd az 1. dokumentumot). A magas N érték a földön javítja az [6] scatter.

Nem kell összekeverni a légoszlató (tükörkép a síkok: jelek néhány másodperc, Doppler) és a scatter meteor (tükörkép meteorok), amely a program kizárja a tropo.

## 7. Az időjárás kép: hogyan lehet olvasni a térképen

A tropo nyílások követik a meteorológiát. Tipikus helyzetek [3] [4] [13]:

- Stabil anticiklon, nagy nyomás (több mint 1020- 1025 hPa), széles izobare, kis szél: lefelé fordulás. A legjobb nyílások követik a szélén az anticiklon, különösen a tengeren.
- Calo della pressione: inversion break. G4MCU követte az SK6VHF jeladót, amíg a nyomás 1012 hPa [13] -ra csökken.
- Meleglevegő magasságban (850 hPa-ra emelkedik) a friss levegőn a földön: fordított.
- A stabil sejtek és gravitációs hullámok (pl. az Alpokban a széllel) helyi és rövid nyílásokat hozhatnak létre [4].
- A nap ideje: a besugárzásból származó inverziók erősebbek este és kora reggel; a süllyedés a napnak is marad.

A Hepburn [1] és F5LEN [2] indexek mindezt időjárási modellekből állítják össze: Különböző kvótákban számítják ki az N-t, és a gradienseket számokká vagy osztályokká alakítják át. Nem vagyok garancia: az LA0BY LA- UA6 eseményt mutat, ami magas hőmérsékleten látható, nem Hepburn [3] -ban. Ehhez a program együtt mutatja az indexeket, izobare-t, a hőmérsékletet és a valós foltokat: ha a foltok egy anticiklon alatti magas indexterülethez igazodnak, a nyitás folyamatban van. Az KP4MD 144 MHz-es WSPR-jelentéseket talált a Hepburn [14] által biztosított ablakokban.

## 8. Gyakorlati lista

1. Nézd meg a tropo indexet a területeden és azon az úton, ami érdekel, a következő néhány órában.
2. Ellenőrizze izobare: magas nyomás és széles izobare felett vagy az út mellett.
3. Kapcsolja be a Thermal Inversion és T 850 hPa: forró levegő magasságban az út felett.
4. A tengeri útvonalakat, valamint az esti és reggeli órákat részesítik előnyben.
5. Ha a hegyen vagy, gondolj a magas csatornákra; Ha a parton vagy, a felszínen.
6. Először próbálja meg a legmagasabb sávokat (432, 1296): nyissa meg először.
7. Ellenőrizze a helyszínen és PSKReporter: hosszú vonalak ugyanabban az irányban megerősíti a csatorna.

## Forrás

1. W. R. Hepburn, DX Info Centre, "The Hepburn Tropo Index (HTI)" és "Troposferic Ducting Forection" oldalak (dxinfocentre.com).
2. F5LEN, előrejelzés Európára és világelőrejelzés VHF tropopherikus szaporításra (tropo.f5len.org).
3. S. Heck LA0BY, "Extreme Tropo propagation on 144 MHz and up", 41 ° Nordic VUSHF Meeting, Skjeberg 2019. Normál gradiens és inverziók, hőmérséklet és páratartalom, nagy nyomás, LA- UA6 esemény, kapcsolatok a hegyeken túl.
4. F. Egano IK3XTV, "Propagation VHF - The contribution of convective cells and atmoszféra gravitation waves in the Tropo propagation". Első 3 km, tengeri utak, konvektív sejtek, gravitációs hullámok, nagy nyomás.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, "Nagyon hosszú távolság propagáció a 144 MHz sávban", 24 / 7 / 2003. Hőmérséklet és harmatpont profilok, inverziók, különbségtétel az E sporadiktól.
6. J. N. Gannaway G3YGF, "Tropospheric Scatter propagation", QST November 1983. A Föld sugarának megfelelő és szétszórt veszteség.
7. A VHF / UHF DX könyv 2. fejezete "Troposzferikus szaporítás". A dN / dh szerinti refraktív zónák, valamint a felszíni és a magas csatornaprofilok (csak a számok).
11. ITU- R P.453 ajánlás, rádiórefraktív index. N képlet és csatorna küszöbérték -157 N / km. Általános hivatkozás, nincs mellékelve.
13. G4MCU, "troposzferikus" cikkek (g4mcu.net), 2018- 2025. Condotti nagy nyomással, különösen a tengeren; SK6VHF és csökken 1012 hPa.
14. C. F. Milazzo KP4MD ", VHF / UHF WSPR Propagation Study", 2012- 2014. SNR WSPR 144 MHz és Hepburn előrejelzés.

A forrásszámok ugyanazok, mint a 2. dokumentum, a térkép épület.
