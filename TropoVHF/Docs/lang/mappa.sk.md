LIVETROPOVF · technický dokument 2
# Výstavba mapy

> Strojový preklad z taliančiny: v prípade pochybností platí talianska príručka.

Mapa záložky Mapa a miesto sa zrodí: geografická základňa, projekcie, priemer indexu Hepburn a F5LEN, isobare, škvrny a stanice počúvanie, prelínal s vrstvami. Verzia z 9. novembra 2026.

## Súhrnne

- Mapa je navrhnutá programom v ôsmich vrstvách, každá s [8] box.
- Základňa: Prírodné pobrežie Zeme a hranice na dvoch úrovniach [9] detail; [10] Market a azimut projekcie.
- Fyzika: inverzie, reaktivita N a gradient dN/dh, prah vedenia pri −157 N/km [3] [4] [7] [11].
- Index hrdla: kalibrovaný údaj mapy Hepburn [1] a F5LEN [2], spoločná mriežka a priemer týchto dvoch zdrojov.
- Isobare: GFS [12] tlak cez Open-Meteo [15], čiary každých 4 hPa, H a L centier.
- Spot, počúvanie stanice (tmavé ružové kruhy s vždy viditeľné ružové čiary) a osobné dekódovanie v červenej farbe.
- Čítanie prekrývania: vysoký tlak, vysoké kanály, pokles tlaku, rastúce SNR, [3] [4] [5] [13] [14] horské trasy.

## 1. Princíp: vrstvená mapa

Mapa nie je fixný obrázok: je navrhnutý programom, vrstva cez vrstvu, kedykoľvek zmeníte zoom, projekciu, pásmo, predpoveď času alebo tému. Každá vrstva má svoj box v hornej Layer tyčinke na mape, takže môžete držať len to, čo potrebujete a vidieť všetko dohromady, keď chcete celý obrázok. Poradie zhora hore je toto:

|  #  | Cesta | Obsah | CasellaCity name (optional, probably does not need a translation) |
|  1  | Základné mapy | More, krajiny, pobrežia a hranice | Áno |
|  2  | Index tropo | Media Hepburn + F5LEN alebo len jeden zdroj; nastaviteľná transparentnosť | Áno |
|  3  | Lokátor grilu | Polia, štvorce a so zoomom na druhú | Áno |
|  4  | Izobar | Tlakové vedenia každých 4 hPa, H a L centier | Áno |
|  4a  | Teplota 850 hPa | Izotermická liečba 4 °C | Áno |
|  4b  | Tepelná inverzia | Oranžová vzorka, pri ktorej je vzduch v nadmorskej výške teplejší ako na zemi | Áno |
|  5  | PSKReportér | Prijímacie stanice PSKReporter (tmavo ružové kruhy) a ružové, čo počujú | Áno |
|  6  | Dekódované mnou | Dekódovacie stanice s WSJT-X, JTDX alebo MSHV: Červené čiary z vášho QTH | Áno |
|  6a  | QSO desunti dal live | Uzavreté spojenia ( RR73, 73, RRR) prečítajte si vo svojich dekódovanie: zelená čiara medzi dvoma stanicami | Áno |
|  7  | Spotlight | Prípojky: plná modrá čiara, žltá krytá pre airskatter, dve gule | Áno |
|  7a  | Okrem | CasellaCity name (optional, probably does not need a translation) Vylúčené do 100 km: najkratšia doprava nie je využitá ani uvedená. CasellaCity name (optional, probably does not need a translation) Viac ako 1000 km: iba štvorce (pri štarte): ak je jedna zo staníc vzdialená viac ako 1000 km od vášho QTH, žiadne čiary alebo kruhy, iba lokalizačné štvorce staníc podieľajúcich sa na liečbe lilami na 45° | Áno |
|  8  | QTH a štítky | Vaša stanica, mená vybraného miesta | vždy viditeľné |

Index tropo je bezprostredne nad základňou a pod všetkým ostatným: je farebný odber vzoriek a vyššia umiestnená by pokrývala čiary a symboly. Mriežka prechádza cez ňu, takže vždy viete, kde padá farebná oblasť. Isobare sú tenké čiary a čítajú sa vysoko nad odberom vzoriek.

## 2. Základná mapa

Náklady, hranice a pôda pochádzajú z prírodných vektorových dát Zeme, z verejne prístupnej domény [9], do mierky 1:50 miliónov pre celkový pohľad a 1:10 miliónov pri zväčšení. Súbory sú v programovom priečinku: žiadne online mapové služby. Farby témy Jasné: svetlo modré more # E2ECF8, tmavo modré pobrežie, svetlosivé hranice. V témach Čierny a Night farby základne sú tmavé, v iných témach mierne farbia.

## 3. Tieto dve prognózy

Všetky vrstvy prechádzajú rovnakou projekčnou funkciou, takže sú vždy navzájom prepojené. Vyberiete si medzi dvoma [10] projekciami (λ zemepisnej dĺžky, φ zemepisnej šírky, R lúč Zeme):

Obchod. Uložiť miestne rohy a tvary; Vzdialenosti sa sťahujú k pólom. Prestrihnite na ±85° zemepisnej šírky.

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

Azimutale equidtanti sústredený na vašom námestí. Centrum (φ0, λ0) je stredom vášho námestia WWL. Vzdialenosti a smery merané od stredu sú pravdivé: stanica 1200 km je 1200 km v mierke, a uhol so severom je azimut, na ktorý ukazuje anténu. Keď zmeníte lokalizátor, mapa sa preorientuje.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

V azimute sú čiary na papieri rovné (meridčania, paralely, hrany štvorca) zakrivené. Z tohto dôvodu sú všetky čiary rozdelené do krátkych sekcií, každá sekcia je premietaná a potom sú znaky zjednotené; to isté platí pre okraje trojkolky a izobare. Bod proti pódiám centra nemá jednoznačnú pozíciu a zostáva mimo dohľadu.

## 4. Gril lokátor, zoom a pohyb

Sieť Maidenhead je navrhnutá programom: polia 20° × 10° s dvoma veľkými a vyblednutými písmenami v strede, štvorce 2 × 1st a s dostatočným zoomom 5′ × 2,5′. Linky sú modré a tenké, nie pokryť index tropo.

Zoom s kolesom sústredené na ukazovateľ (bod pod myšou zostáva stabilný), pohybujúce sa ťahanie, dvojitým kliknutím vrátiť sa k celému pohľadu, + a − klávesy, a kľúč s percentom pre celý pohľad. Detaily sa hodia na zoom: jemnejšie pobrežie, podštvorce, hrubšie štítky izobare.

## 5. Prečo inverzie ohýbajú vlny

V nízkej atmosfére teplota zvyčajne klesne okolo 6,5 °C na km; Index lomu závisí od teploty aj vlhkosti [3], a tam, kde sa výrazne mení s nadmorskou výškou vlna ohne k zemi a prichádza ďalej, ako je obvyklé. Najdôležitejšia vrstva je, že až do cca 3 km, kde inverzie, kanály a konvektívne bunky [4] sú tvorené; morské cesty sú zvýhodnené, pretože vodné masy tvoria veľké plochy [4] inverzie. [3]

Veľkosť použitá na meranie je refrakčná N, t. j. (n - 1) · 106 [11]:

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

T je teplota v kelvine, P tlak a tlak pary, ako v hPa.

Počíta sa ako rozmanitý N s podielom (dN/dh). V štandardnej atmosfére je približne −39 N/km; až do −79 N/km sa refrakcia považuje za normálnu okrem super-refrakcie a s kladnými gradientmi sub-refrakcie [7]. Pod −157 N/km je vlna ohnutá viac ako zakrivenie zeme a zostáva zachytená: je to [11] (odvodový) kanál. F5LEN

## 6. Index tropo: z dvoch máp na priemer

Hepburn a F5LEN karty sú kontrolným panelom: zobraziť obrázky stiahnuté zo stránky každého zdroja, s editovateľným predpoveď času, a slúžiť na kalibráciu čítanie kliknutím na referenčné body a farby legendy. Indexová vrstva tropo zostáva prázdna, kým aspoň jedna z dvoch kalibrácií nie je aktívna a úplná.

Hepburn. Index Hepburn meria, koľko sa očakáva, že vlny budú zložené na ploche, na lineárnej stupnici od 0 do 10, ktorá môže tiež prekročiť 10. 0 znamená štandardnú atmosféru a iba rozptýlenie; 1 a 2 okrajové alebo slabé super-refrakcie; 3 a 4 super-refrakčné s kanálikmi; Index neberie do úvahy tropo scatter a nevidí [1] hory. Ty pokrývajů 6 dní: 3 hodiny prejde prvých 36 hodín, potom 6 hodín, aktualizovaný každý deň na 18 UTC [1]; počiatočné údaje (teplota, vlhkosť, tlak) pochádzajú z globálneho modelu kanadskej meteorologickej služby [1]. [1]

F5LEN. Pre Európu F5LEN vydáva 31 máp od +3 do +192 hodín (každých 3 hodín do +48, každých 6 do +96, potom každých 12), s dátumom a časom platným v názve, čas výroby v pravom dolnom rohu a triedna legenda: Disturb, Normálny, Malý, Slabý, Dobrý, Silný, Vy Strong, Duct [2]. Celosvetová predpoveď F5LEN sa vypočíta podľa indexu lomu vzduchu v jednotke N, na 40 krokoch po 3 hodinách (asi 5 dní), aktualizované 4 krát denne; kliknutím na mapu sa zobrazí priemerná hodnota a lokátor tohto bodu [2].

Pokračujeme. Pre každý krok predpovede program vykonáva tieto kroky:

1. Prepustenie. Obrázky týchto dvoch predpovedí sú stiahnuté v vyrovnávacej pamäti vedľa spustiteľného, akonáhle aktualizovať zdroj, nie na žiadny redesign. Pred konečnou verziou je potrebné požiadať o povolenie Hepburn a F5LEN.
2. Automatické geomarking. Program nájde rám (dve horizontálne a dve zvislé čiary rovnakej farby) a, na Windows 10/11, číta s OCR integrované do systému návestidlá nápravy (54N, 10W, 0, 5E...). Rozstrihnite pásy okolo rámu, zväčšte ich, otestujte obe značky, keď písmeno N/S/E/W nečíta a udržujte priamku, na ktorej najviac padne: zlé hodnoty zostávajú mimo. Odtiaľ dostanete extrémny lat/lon rámu a typ projekcie (pravidelný gril alebo Mercator). Extremes uložiť a znovu čítať sami, ak rám zmení veľkosť. Na Mac a Linux, bez OCR, továrenské extrémy alebo ručne písané tie sú platné v karte. Alternatívne pre mapy, ktoré nemajú čitateľné osi: Každý typ mapy (Hepburn Europa, F5LEN po celom svete...) má kalibráciu: niektoré kontrolné body so známymi súradnicami (krížy poludníkov a paralely, rozpoznateľné rebrá) spájajú pixely so zemepisnou šírkou a dĺžkou, v projekcii používanej týmto zdrojom. Kalibrácia je v súbore vedľa programu a môžete prerobiť, ak zdroj zmení formát.
3. Čítanie farieb. Každý pixel sa porovnáva so zdrojovou legendou a konvertuje sa na najbližšiu číselnú hodnotu. Náklady, spisy a mriežky nakreslené na pôvodnej mape sú uznané ako
4. Svetová sieť. Každý obrázok, akonáhle je rám nájdený a známy jeho extrémy, je umiestnený na Zemi; hodnota v každom bode na svete je odvodená zo snímok, ktoré ho pokrývajú. Ak viac regiónov pokrýva rovnaký bod, stojí to za detail (viac pixelov na stupeň), za predpokladu, že jeho čas je do 1,5 hodiny od tejto voľby; potom najbližšia hodina, potom najnovšie preteky. Tam, kde nie je obrázok pokrýva bod mapa ukazuje sivý závoj: žiadne údaje, ktoré sa líšia od Vnútri rámu, pixel bez legendy farby je 0 (normálna atmosféra); pobrežia, hranice a spisy sú skákané pohľadom na najbližšiu farbu v rámci 3 pixelov.
5. Rovnaký rebrík. Hepburn je už na stupnici 0 F5LEN používa triedy (Disturb, Normálny, Malý, Slabý, Dobrý, Silný, Vy Silný, Duct) [2], ale niektoré majú presne Hepburn farby: Slabý = 1, silný = 5, vy silný = 7, Duct = 8; Minor je 0,5 a dobrý (zelený) 3,5. Multiplikátor stupnice v záložke F5LEN opravuje sadu tak, aby bola porovnateľná s Hepburnom. [1]
6. Rovnaký čas. Časový panel sa pohybuje od +0 do +192 h v krokoch po 3 hodinách. Pre každý krok urobíte z každého zdroja predpoveď platná v najbližšom čase; Ak zdroj nemá tento krok, médiá používajú len ten druhý a legenda to naznačuje.
7. Médiá. S tlačidlom Media každý bod stojí (Hepburn + F5LEN) / 2; s Hepburn alebo F5LEN vidíte len jeden zdroj, užitočné pochopiť, kde dve predpovede nesúhlasia.
8. Kreslenie. Hodnota je sfarbená legendou stupnice (0 modrá, 2 zelená, 4 žltá, 6 oranžová, 8 červená, 10 fialová) a premieta sa ako odber vzoriek pod mriežkou. 0 je transparentné, takže tam, kde sa nič nestane, vidíte čistú základnú mapu; všeobecná transparentnosť sa nastaví kurzorom vedľa poľa.

Na bodovej záložke je index pozdĺž trasy priemer ligových hodnôt mriežky každých 10 km na maximálnom kruhu medzi dvoma stanicami.

## 7. Izobar

Vysoké stabilné tlaky sú prvým signálom, ktorý sa má hľadať pri [3] mape, a spojenia za IK3XTV analyzovanými Alpami boli tiež s vysokým tlakom [4].

1. Dáta. Tlak na úrovni mora z globálneho modelu GFS NOAA [12], prečítaný cez bezplatnú službu Open-Meteo [15] na mriežke 3° × 3° (od 30° do 72° N, od 30° z. d. do 48° v. d.), počas nasledujúcich 78 hodín. Program aktualizuje dáta pri štarte a potom každých 6 hodín, ako model závodné.
2. Linky. Isobare pochádza zo siete každých 4 hPa (...1012, 1016, 1020...) s algoritmom pochodových štvorcov (marching štvorcov), potom sú mierne zaoblené a premietané traktátom.
3. Centrá. Najvýraznejšie lokálne maximá a minimum sú označené H (modrá) a L (červená) a hodnotou v hPa.
4. Štítky. Hodnota sa objaví pozdĺž každej izobary na farebnom pozadí mapy, aby zostala čitateľná nad odberom vzoriek.

Zóna a krok. Údaje sú stiahnuté v oblasti, na ktorú sa pozeráte, rozšírené o 25%, s krok od 1. do 20° vybrané zostať pod 400 bodov (Open-Meteo počíta každý bod ako žiadosť: 600 za minútu, 5000 za hodinu): rozšírenie alebo presunutie mapy z oblasti už stiahne program najhustejšie znovu načítať, na maximum každých 10 minút; ak aktualizácia zlyhá predchádzajúce údaje zostať a bude to skúsiť znova po 3 minútach. Týmto spôsobom isobare a teploty fungujú kdekoľvek na svete.

Teplota. Rovnaká požiadavka prináša teplotu na 2 m, na 925 hPa a na 850 hPa. LA0BY ukazuje, že udalosť 22/11/2018 až UA6 bola videná na teplotných mapách (900 3000 m), ale nie v predpovediach Hepburn [3]; DF5AI a DK5YA používajú vertikálne teplotné profily na rozpoznávanie inverzií [5]. Pre to existujú dve vrstvy:

- Teplota 850 hPa: izoterme liečiť každých 4 °C (červená nad nulou, modrá pod, fialová na nulu).
- Tepelná inverzia: oranžový odber vzoriek, pri ktorom je vzduch pri teplote 925 alebo 850 hPa teplejší ako pri 2 m; čím intenzívnejší, tým silnejší je inverzia. Je to fyzikálny stav, ktorý ohýba vlny na zem [3] [11], a porovnať ho s tropo index a izobare ukazuje, či predpovede majú meteorologickú základňu.

## 8. Miesta a stanice

Spot. Každý bod s oboma známymi pozíciami je čiara na maximálnom kruhu medzi dvoma stanicami, rozdelené do sekcií a premietané; v Mercatore sa objaví potom mierne zakrivené, v azimutale je to priamo, ak začína od stredu. Modrá čiara plná pre normálne miesto, žltá pokrytá tmavou hranou pre airskatter (<AS>, <ACS> alebo Vybrané miesto je častejšie určené pre posledné a s dvoma menami.

Počúvam. Každý PSKReportér Prijímacia stanica [16] je tmavo ružový prázdny kruh. Od každého začiatku plné ružové čiary smerom k staniciam, ktoré dekódoval a poslal späť do siete, vždy viditeľné bez kliknutia, s menami vždy napísané. Výberom kruhu sú jeho čiary hrubšie. Spot a počúvanie len zobraziť kapelu a rozsah zvolený v zozname: zmena šírky pásma vyprázdnenia mapy a redesigny s dátami, ktoré už boli zhromaždené pre nové pásmo.

Rozhodol som sa. Každá stanica, ktorá dekóduje s WSJT-X, JTDX alebo MSHV (prijímanie cez UDP) je červená čiara plná od vášho QTH na stanicu, s loptou a nomináciou. Je to najpriamejšie porovnanie s predpoveďou: ukazuje, kde váš prijímač skutočne prichádza práve teraz.

## 8 bis. Farby a symboly

Každá farba má len jeden význam, rovné vo všetkých témach (zmena iba pozadia).

| Farba a značka | Význam |
| Nepretržitá modrá čiara | Bod zo zoskupenia DX, RBN alebo DM4X medzi dvoma stanicami |
| Žltá čiara s tmavým okrajom | Spot s airskatter komentár (reflexia na lietadlo) |
| Tmavo ružový prázdny kruh | PSKReporter (MQTT) |
| Tenké ružové čiary z kruhu, s ružovou guľou v dolnej časti | Stanice, ktoré poslucháč počul, s názvom vždy napísané; silnejšie čiary pre vybraného poslucháča |
| Červená čiara a červené meno | Vaše dekódovanie UDP (WSJT-X, JTDX, MSHV): Kto ste? |
| Úchyty červenej čiary | QSO prebieha s touto stanicou: Zavolal vám a zatváranie ešte nedorazil, alebo je DX Call of WSJT-X/JTDX s povoleným prenosom. Pokračujte s RR73, RRR alebo 73 |
| Zelená čiara | QSO desunti dal live: zatvorenie RR73, RRR alebo 73 UDP zobrazenie |
| Čierna čiara s bielym okrajom | Niekto vám volá: trvá až RR73/RRR /73 alebo až 3 minúty |
| Námestie 45° | Premávka s najmenej jednou stanicou za zvolenou korunou: Žiadne riadky, len štvorce lokátor |
| Hľadať lilly s vzdialenosťou na sever | 1000, 700 a 500 km okolo vášho QTH; táto voľba je výraznejšia |
| Kruh a bod vo farbe témy | Váš QTH, s menom a prenajímateľom |
| Oranžová guľa s GRAVES 143,050 | Radar GRAVES ( JN27SI) |
| Vzorka z modrej až červenej | Hepburn index trope (0 F5LEN |
| svetlosivá zamatová | Oblasť bez údajov o tropo pre zvolený čas |
| Sivé modré čiary s hodnotou v hPa | Isoberate each 4 hPa; H in blue high pressure, L in red low pressure |
| Červené, modré a purpurové bodkované čiary | Izotermia do 850 hPa každých 4 °C: červená nad nulou, modrá pod nulou, fialová nula |
| Vzorka oranžová | Tepelná inverzia: vzduch do 925 hPa teplejšie ako na zemi, intenzívnejšie s silnejším inverziou |

V podozrivých správach okraj každej karty hovorí platné pravidlo: zelená dobrá, červená Scarta, šedá žiadne pravidlo.

Čo sú ľalie štvorce pokryté na 45°. Premávka ďaleko od vášho QTH vás zaujíma pochopiť, kde šírenie je otvorené, ale jeho linky, stovky kilometrov dlhé a často desiatky, pokrytie mapy a skryť, čo sa pre vás počíta: otvory môžete využiť zo svojej stanice. Z tohto dôvodu, mimo zvolenej koruny, program nekreslí čiary, ale farby lokátor štvorcov (2 × 1°) z príslušných staníc. Výsledkom je mapa aktívne zóny: tam, kde sa objaví mnoho blízkych námestí lilac, tam je kanál alebo otvor, aj keď sa vás netýka priamo; Ak sa oblasť rozširuje smerom k vám, je to signál namieriť anténu v tomto smere. Doska 45° vám umožní vidieť pod tropo index a mriežku, tak sa porovnať na pohľad predpoveď (dolná farba) a skutočný prevádzku (lilac). Váš QTH námestie nikdy farby, pretože je východiskovým bodom a nie vzdialenej oblasti. Iba štvorce, ktoré sú všetky za korunou sú farebné: štvorec prekročený korunou alebo vnútri zostáva voľný, aj keď je jedna z dvoch staníc. Ak chcete vidieť čiary, stačí vypnúť krabicu alebo vybrať väčšiu korunu.

Kritériá, ktoré rozhodujú o tom, čo je na mape a čo je vyradené, sú vysvetlené jedným v dokumente 1, oddiel 11 ter.

## 9. Časový panel

Nižšie na mape: dátum a čas zobrazenej predpovede, kurzor od +0 do +72 h v 6 hodín, index legenda 0 Trumpet a isobare index sledovať kurzor; škvrny, stanice počúvanie a vaše dekódovanie sú pozorovania okamihu a vždy zostávajú tie Keď kurzor nie je na +0 sú oslabené, pamätať, že nepatria do predpoveď času.

## 10. Ako čítať prekrývanie

- Vysoký tlak + vysoký index + miesto pozdĺž farebnej oblasti: otvorenie je tam a je to plánované. Je čas zavolať tým smerom.
- Vysoký index, ale bez miesta: alebo tam nikto nie je aktívny, alebo predpoveď je príliš optimistická. Odpočúvacia vrstva pomáha rozlíšiť: ak sú prijímače v oblasti a necíti nič, druhá hypotéza je pravdepodobnejšia.
- Dlhé škvrny mimo farebných oblastí: vysoké potrubie, ktoré povrchové predpovede nevidia, ako v prípade LA DF5AI a DK5YA rozlíšiť tropo a E sporadické tiež od hodiny: otvory, ktoré sa narodili a skončiť spolu s difúznym E sporadické sú sotva [5] tropo. UA6 [3]
- Hepburn a F5LEN nesúhlasia: porovnajte dva zdroje s tlačidlami a pozrite sa, kde reálne škvrny padajú.
- Tlak nadol: G4MCU nasledovalo po otvorení na majáku SK6VHF až do poklesu tlaku; 1012 hPa považoval dokončenú udalosť [13]. Pozrite sa na izobare posúvaním časovej čiary: ak je vysoký tlak stiahnutý, otvor pravdepodobne nasleduje.
- Signály, ktoré idú hore: v WSPR štúdiu na 144 MHz KP4MD pomer signál/hluk rástol priamo vo oknách, kde Hepburn predpovedal zlepšenie, a klesol, keď sa predpoveď odstránila [14]. ak na rovnakej ceste rastie, otvor sa vytvára.
- Horské cesty: index neberie do úvahy [1] reliéfy, ale prepojenia za Alpy tiež existujú vďaka konvekčné bunky a vlny atmosférickej gravitácie [4], a LA0BY pracoval 9A a S5 cez hory bez Hepburn predpovedá [3].

## 11. Otvorený bod

- Povolenie Hepburnu a F5LEN používať svoje mapy v programe.
- Kalibrácia konverzných koeficientov F5LEN → stupnica 0
- Kalibrácia kontrolných bodov akéhokoľvek druhu mapy, prerobiť, ak zdroje zmeniť grafiku.
- Nepovinná trasa s teplotou vo výške (850 hPa) od GFS.

## Hlavné zdroje

1. W. R. Hepburn, DX Info Centre, Definícia a rozsah indexu, záporné hodnoty a viac ako 10, vylúčenie rozptylov a reliéfov, kroky 3 a 6 hodín na 6 dní, aktualizácia na 18 UTC. Pôvod údajov z globálneho kanadského modelu: G. Meisse, AMSAT-BB, 27/08/2007.
2. F5LEN, predpoveď pre Európu (tropo.f5len.org/predpovede-pre-európu) a svetová predpoveď tropoférického šírenia VHF (tropo.f5len.org/WW) a poznamenali, že nová verzia nástroja troposférického šírenia VHF. Index jednotiek N, 40 krokov od 3 hodín, 4 aktualizácie denne, hodnota a lokátor za kliknutie.
3. S. Heck LA0BY, Normálny gradient 6,5 °C/km a inverzie až do 10 UA6 S5
4. F. Egano IK3XTV, Význam prvých 3 km, morské chodníky, konvekčné bunky, gravitačné vlny na Alpách, vysokotlakové spojenia, spoločný objem až do 3 a 4 km.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, Vertikálne profily teploty a rosy poukazujú na rozpoznávanie inverzií, použitie indexu Hepburn, rozlíšenie medzi tropo a E sporadické podľa hodiny.
6. J. N. Gannaway G3YGF, Ekvivalent polomeru Zeme a strata dráhy používanej pre index a škvrnovú kartu (pozri dokument 1).
7. The VHF/UHF DX Book, chapter 2 Obrázok refrakčných zón podľa dN/dh (štandard −39 N/km, limit −79 N/km a 0) a povrchových a vysokých káblovodových profilov. Text dodanej kópie nie je čitateľný: použili sa len čísla.
8. Zhrnutie projektu 8/11/2026 poskytol autor programu. Mapa farby, požadované projekcie, vrstvy, časový panel.
9. Prírodná Zem, vektorové dáta pobrežia a hraníc, verejná doména (Natural Earthdata.com). Všeobecný odkaz, nie je pripojený.
10. J. P. Snyder, Formuly Mercator projekcie a azimutale equidtanti. Všeobecný odkaz, nie je pripojený.
11. Odporúčanie ITU-R P.453, rádiofrakčný index. Refrakčný vzorec Prah N a kanál pri −157 N/km. Všeobecný odkaz, nie je pripojený.
12. NOAA NCEP, Global Forecast System (GFS). Tlak na hladine mora na 0,25°, štyri preteky denne. Všeobecný odkaz, nie je pripojený.
13. G4MCU, výrobky s tagom (g4mcu.net/tag/troposféric), 2018 Refrácia, spojka a rozptyl; častejšie kanály s vysokým tlakom, najmä na mori; maják SK6VHF sledoval pokles tlaku na 1012 hPa (jún 2019).
14. C. F. Milazzo KP4MD, SNR WSPR hlási, že 144 MHz rastie v vylepšeniach, ktoré predpovedal Hepburn a klesá, keď sa predpovedá zmizne.
15. Open-Meteo, predpoveď počasia API (open-meteo.com), bezplatné nekomerčné použitie. Tlak na úrovni mora modelu GFS na viacerých bodoch v jednej žiadosti.
16. mqtt.pskriporter.info, distribúcia MQTT v reálnom čase reklamy pskreporter. Informácie: nominácia a prenajímateľ príjemcov (pozri dokument 1).
