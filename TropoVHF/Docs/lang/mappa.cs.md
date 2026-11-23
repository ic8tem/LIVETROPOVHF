LIVETROPOVHF · technický dokument 2
# Výstavba mapy

> Automatický překlad italsky: v případě pochybností stojí za italský průvodce.

Jak mapa karty Mapa a místo se zrodí: geografická základna, projekce, průměr Hepburn a F5LEN indexy, isobare, skvrny a stanice poslouchat, překryty vrstvami. Verze dne 9. listopadu2026.

## Shrnuto.

- Mapa je navržena programem v osmi vrstvách, každý s jeho [8] box.
- Základna: Natural Earth pobřeží a hranice na dvou úrovních [9] detail; [10] trh a azimut projekce.
- Fyzika: inverze, reaktivita N a gradient dN / dh, práh potrubí při − 157 N / km [3] [4] [7] [11].
- Index hrdla: kalibrované čtení map Hepburn [1] a F5LEN [2], společná mřížka a průměr obou zdrojů.
- Isobare: GFS [12] tlak přes Open- Meteo [15], linky každé 4 hPa, H a L centra.
- Spot, odposlouchávací stanice (tmavě růžové kruhy s vždy viditelnými růžovými čarami) a osobní dekódování v červené.
- Čtení překrývání: vysoký tlak, vysoké kanály, pokles tlaku, rostoucí SNR, [3] [4] [5] [13] [14] horské trasy.

## 1. Princip: vrstvená mapa

Mapa není pevný obrázek: je navržena programem, vrstva přes vrstvu, kdykoli změníte zoom, projekci, pásmo, čas předpovědi nebo téma. Každá vrstva má svou krabičku v horní vrstvě na mapě, takže můžete držet jen to, co potřebujete a vidět vše společně, když chcete celý obrázek. Pořadí, zdola nahoru, je toto:

|  #  | Silnice | Obsah | Casella |
|  1  | Základní mapa | Moře, pozemky, pobřeží a hranice | Ano. |
|  2  | Index tropo | Media Hepburn + F5LEN, nebo pouze jeden zdroj; nastavitelná průhlednost | Ano. |
|  3  | Lokátor grilu | Pole, čtverce a se zoomem na druhou | Ano. |
|  4  | Isobare | Tlakové vedení každé 4 hPa, H a L centra | Ano. |
|  4a  | Teplota 850 hPa | Isotermální zpracování všech 4 ° C | Ano. |
|  4b  | Tepelná inverze | Oranžový vzorek, kde je vzduch ve výšce teplejší než na zemi | Ano. |
|  5  | PSKReporter | Přijímací stanice PSKReporter (tmavě růžové kruhy) a, v růžové, co slyší | Ano. |
|  6  | Decoded by me | Dekódovací stanice s WSJT-X, JTDX nebo MSHV: červené linky z QTH | Ano. |
|  6a  | QSO desunti dal live | Uzavřené spoje ( RR73, 73, RRR) ve vašich dekodérech: zelená čára mezi oběma stanicemi | Ano. |
|  7  | Spotlight | Připojení: plná modrá čára, žlutý kryt pro airscatter, dvě koule | Ano. |
|  7a  | S výjimkou | Casella Vyloučeno pod 100 km: nejkratší doprava není ani vlečena, ani uvedena v seznamu. Casella Více než 1000 km: pouze čtverce (při startu): je-li jedna ze stanic více než 1000 km od vašeho QTH, žádné linky nebo kruhy, pouze lokalizační čtverce stanic zapojených do šeříku ošetřené při 45 ° | Ano. |
|  8  | QTH a štítky | Vaše stanice, jména vybraného místa | vždy viditelné |

Index tropo je okamžitě nad základnou a pod vším ostatním: je barevný odběr vzorků a vyšší umístění by pokrývalo linie a symboly. Síť prochází přes něj, takže vždy víte, kde barevná oblast padá. Isobares jsou tenké čáry a číst dobře nad vzorkováním.

## 2. Základní mapa

Náklady, hranice a půda pocházejí z přírodních dat vektoru Země, z veřejné domény [9], až po 1: 50 milionů pro celkový pohled a 1: 10 milionů pro zvětšení. Soubory jsou ve složce programu: žádné online mapové služby. Barvy tématu Clear: světle modré moře # E2ECF8, tmavě modré pobřeží, světle šedé hranice. V tématech Black a Night barvy základny jsou tmavší, v jiných tématech se barví mírně.

## 3. Obě projekce

Všechny vrstvy procházejí stejnou projekční funkcí, takže jsou vždy vzájemně sladěny. Vyberte si mezi dvěma [10] projekcemi (λ zeměpisná délka, δ zeměpisná šířka, R paprsek Země):

Trh. ukládat místní rohy a tvary; Vzdálenost se táhne k pólům. Úsek na ± 85 ° zeměpisné šířky.

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

Azimutale equidtanti soustředěný na vašem náměstí. Centrum (φ0, λ0) je centrem vašeho WWL náměstí. Vzdálenosti a směry měřené od centra jsou pravdivé: stanice 1200 km je 1200 km v měřítku, a úhel se severním je azimut, do kterého bod antény. Když změníte lokátor, mapa je přesměrována.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

V azimutu linky na papíře jsou rovné (meridiány, paralely, čtvercové hrany) stát zakřivený. Z tohoto důvodu jsou všechny řádky rozděleny do krátkých úseků, každá sekce je promítána a pak jsou vlastnosti sjednoceny; totéž platí pro okraje tropového indexu a pro isobare. Pointa k antipodům centra nemá definitivní pozici a zůstává mimo dohled.

## 4. Lokátor grilu, zoom a pohyb

Maidenhead mřížka je navržena programem: pole 20 ° × 10 ° se dvěma velkými a vybledlými písmeny ve středu, čtverce 2 ° × 1st, a s dostatečným zoomem 5 ′ × 2,5 ′. Čáry jsou modré a tenké, ne aby pokryly index tropa.

Přiblížit kolečkem soustředěným na ukazovátko (bod pod myší zůstává stabilní), pohybující se přetažení, dvakrát klikněte pro návrat do celého pohledu, + a − klávesy, a klíč s procentem pro celý pohled. Podrobnosti jsou vhodné pro zoom: jemnější pobřeží, podčtverce, silnější isobare štítky.

## 5. Proč inverze ohýbat vlny

V nízké atmosféře teplota obvykle klesne o 6,5 ° C na km; index lomu závisí jak na teplotě, tak na vlhkosti [3], a kde se prudce mění s nadmořskou výškou se vlna ohne k zemi a dorazí dále, než je obvyklé. Nejdůležitější vrstva je, že až do cca 3 km, kde jsou tvořeny inverze, kanály a konvekční buňky [4]; mořské cesty jsou upřednostňovány, protože vodní masy tvoří velké oblasti [4] inverze. [3]

Velikost používaná k měření je refrakční N, tj. (n − 1) · 106 [11]:

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

T je teplota v kelvinu, P tlak a tlak páry, a to jak v hPa.

Počítá se jako pestré N s podílem (dN / dh). Ve standardní atmosféře je přibližně − 39 N / km; až − 79 N / km je lom považován za normální, kromě superlomu a s pozitivními sklony lomu [7]. Pod -157 N / km je vlna ohnutá více než zakřivení země a zůstává v pasti: je to [11] (potrubí) potrubí. F5LEN

## 6. Index tropo: od dvou map po průměr

Hepburn a F5LEN karty jsou ovládací stůl: zobrazit obrázky stažené ze stránky každého zdroje, s editovatelnou dobu prognózy, a slouží ke kalibraci čtení kliknutím na referenční body a barvy legendy. Vrstva tropo indexu zůstává prázdná, dokud alespoň jedna ze dvou kalibrací není aktivní a úplná.

Hepburn. Hepburn index měří, kolik vln se očekává, že se složí na plochu, na lineární stupnici od 0 do 10, což může také překročit 10. 0 znamená standardní atmosféra a pouze rozptyl; 1-2 mezní nebo slabý lom; 3-4 superlom s vodovodními plochami; Index nebere v úvahu tropo rozptylu a nevidí hory [1]. Mapy pokrývají 6 dní: 3- hodina trvá prvních 36 hodin, pak 6 hodin, aktualizovány každý den 18 UTC [1]; výchozí data (teplota, vlhkost, tlak) pocházejí z globálního modelu kanadské meteorologické služby [1]. [1]

F5LEN. Pro Evropu F5LEN zveřejňuje 31 map od + 3 do + 192 hodin (každé 3 hodiny až do + 48, každých 6 až do + 96, poté každých 12), s datem a časem platným v názvu, čas výroby vpravo dole a třídní legenda: Disturb, Normal, Minor, Weak, Good, Strong, Vy Strong, Duct [2]. Světová předpověď F5LEN se vypočítá pomocí indexu lomu vzduchu v jednotce N na 40 stupních po 3 hodinách (cca 5 dnech), aktualizovaného 4x denně; kliknutím na mapu se zobrazí průměrná hodnota a lokátor tohoto bodu [2].

Pokračujeme. Pro každý predikční krok program provede tyto kroky:

1. Propuštění. Obrázky těchto dvou prognóz jsou staženy v cache vedle spustitelného, jednou aktualizovat zdroj, ne na žádný redesign. Před konečnou verzí je třeba požádat o povolení pro společnosti Hepburn a F5LEN.
2. Automatické georeference. Program najde rám (dvě vodorovné a dvě svislé čáry stejné barvy) a na Windows 10 / 11, čte s OCR integrován do systému štítky nápravy (54N, 10W, 0, 5E...). Prořízněte pásky kolem rámu, zvětšete je, otestujte obě značky, když písmeno N / S / E / W není čitelné a ponechte řádek, na kterém nejvíce štítků padá: nesprávné údaje zůstávají mimo. Odtud dostanete extrémní lat / lon rámu a typ projekce (normální gril nebo Mercator). Extrémy se ušetří a znovu se odeberou, pokud rám změní velikost. Na Mac a Linux, bez OCR, tovární extrémy nebo ručně psané ty jsou platné v záložce. Případně pro mapy, které nemají čitelné osy: Každý typ mapy (Hepburn Europa, F5LEN po celém světě...) má kalibraci: některé kontrolní body se známými souřadnicemi (kříže poledníků a rovnoběžky, rozpoznatelná žebra) spojují pixely s šířkou a délkou, v projekci používané tímto zdrojem. Kalibrace je v souboru vedle programu a můžete předělat, pokud zdroj změní formát.
3. Barevné čtení. Každý pixel se porovná se zdrojovou legendou a převede na nejbližší číselnou hodnotu. Náklady, spisy a mřížky nakreslené na původní mapě jsou uznávány jako "žádná data" a naplněny nedalekými hodnotami.
4. Světová síť. Každý obraz, jakmile je rámeček nalezen a znám jeho extrémy, je umístěn na Zemi; hodnota v jakémkoli bodě na světě je odvozena od obrázků, které jej pokrývají. Pokud více regionů pokrývá stejný bod, který stojí za detail (více pixelů na stupeň), za předpokladu, že jeho čas je do 1,5 h od této volby; pak nejbližší hodina, pak poslední závod. Pokud žádný obrázek nepokryje bod, mapa ukazuje šedý závoj: žádná data, která se liší od "indexu 0". Uvnitř rámu, pixel bez legendy barva je 0 (normální atmosféra); pobřeží, hranice a spisy jsou skokem na nejbližší barvu v rámci 3 pixelů.
5. Stejný žebřík. Hepburn je již na stupnici 0-10 [1]: tmavé pozadí = 0 a deset barev v pořadí (fialová, modrá, voda zelená, vápno, žlutá, Ocher, oranžová, červená, růžová, světle růžová) pro 1... 10, lilac mimo. F5LEN používá třídy (Disturb, Normal, Minor, Weak, Good, Strong, Vy Strong, Duct) [2], ale některé mají přesně Hepburn barvy: Slabá = 1, Strong = 5, VyStrong = 7, Duct = 8; Minor je 0,5 a Good (green) 3.5. Násobitel stupnice v záložce F5LEN opravuje sadu tak, aby byla srovnatelná s Hepburnem.
6. Ve stejnou dobu. Časová lišta se pohybuje od + 0 do + 192 h po 3 hodinách. Pro každý krok, který uděláte z každého zdroje, je předpověď platná v nejbližším čase. Pokud zdroj tento krok nemá, používají média pouze ten druhý a legenda ho označuje.
7. Média. Pomocí tlačítka Media každý bod má hodnotu (Hepburn + F5LEN) / 2; s Hepburn nebo F5LEN vidíte pouze jeden zdroj, užitečné pochopit, kde dvě předpovědi nesouhlasí.
8. Kreslení. Hodnota je barevná s legendou stupnice (0 modrá, 2 zelená, 4 žlutá, 6 oranžová, 8 červená, 10 fialová) a promítá se jako vzorek pod mřížkou. 0 je transparentní, takže tam, kde se nic nestane, vidíte čistou základní mapu; obecná průhlednost se přizpůsobuje kurzoru vedle rámečku.

Na záložce spot, index podél trasy je průměr hodnot ligy mřížky každých 10 km na maximálním kruhu mezi dvěma stanicemi.

## 7. Isobare

Vysoké stabilní tlaky jsou prvním signálem, který je třeba hledat pro tropu [3], a spojení za analyzovanými Alpami IK3XTV jsou také s vysokým tlakem [4].

1. Date. Tlak na úrovni moře od globálního modelu GFS NOAA [12], číst prostřednictvím bezplatné Open- Meteo [15] služby na mřížce 3 ° × 3 ° (od 30 ° do 72 ° N, od 30 ° W do 48 ° E), pro příštích 78 hodin. Program aktualizuje data při spuštění a pak každých 6 hodin, jako modelové závody.
2. Řádky. Isobare pochází z mřížky každé 4 hPa (... 1012, 1016, 1020...) s algoritmem pochodových čtverců (pochodových čtverců), pak jsou mírně zaoblené a promítá tažené traktem.
3. Centres. Nejvýraznější lokální maxima a minima jsou označeny H (modrá) a L (červená) a hodnotou hPa.
4. Štítky. Hodnota se objeví podél každé isobary, na barevném pozadí mapy, aby zůstala čitelná nad vzorkováním.

Zóna a krok. Data se stahují na oblast, na kterou se díváte, rozšířena o 25%, s krokem od 1 do 20 ° zvoleným k udržení pod 400 bodů (Open- Meteo počítá každý bod jako žádost: 600 za minutu, 5000 za hodinu): rozšíření nebo přesunutí mapy z oblasti již stahuje program nejhustší reload, maximálně každých 10 minut, pokud aktualizace selže předchozí data zůstávají a bude se snažit znovu po 3 minutách. Tímto způsobem isobare a teploty pracují kdekoli na světě.

Teplota. Stejný požadavek přináší teplotu na 2 m, na 925 hPa a na 850 hPa. LA0BY ukazuje, že případ 22 / 11 / 2018 až UA6 byl zaznamenán v teplotních mapách (900- 3000 m), ale ne v předpovědích Hepburn [3]; DF5AI a DK5YA používají vertikální teplotní profily k rozpoznání inverzí [5]. K tomu existují dvě vrstvy:

- Teplota 850 hPa: izomerme léčit každé 4 ° C (červená nad nulou, modrá pod, fialová na nule).
- Tepelná inverze: oranžový odběr vzorků, kde je vzduch 925 nebo 850 hPa teplejší než vzduch ve 2 m; čím intenzivnější, tím silnější je inverze. Je to fyzikální stav, který ohýbá vlny na zem [3] [11], a porovnat ji s tropo index a isobare ukazuje, zda předpovědi mají meteorologické základny.

## 8. Spoty a stanice

Spot. Každé místo s oběma známými pozicemi je čára na maximální kruh mezi dvěma stanicemi, rozdělené do sekcí a promítá; v Mercatore se pak objeví mírně zakřivený, v azimutale je to rovné, pokud to začíná od středu. Modrá čára plná pro normální místo, žlutá pokrytá tmavým okrajem pro airscatter (< AS >, < ACS > nebo "airscatter" v komentáři), koule pro každou stanici, světlá svatozář pod čárou oddělit od odběru vzorků. Vybrané místo je častěji navrženo pro poslední a s oběma jmény.

Poslouchám. Každý reportér PSKReporter [16] přijímací stanice je tmavě růžový prázdný kruh. Od každého startu plné růžové linky směrem k stanicím, které dekódoval a poslal zpět do sítě, vždy viditelné bez kliknutí, s názvy vždy napsané. Zvolením kruhu jsou jeho čáry silnější. Spot a poslech zobrazují pouze pásmo a rozsah zvolený v seznamu: změna šířky pásma mapy vyprázdní a redesign s daty již shromážděnými pro nové pásmo.

Rozhodl jsem se. Každá stanice, která dekóduje s WSJT-X, JTDX nebo MSHV (příjem přes UDP) je červená linka plná od vašeho QTH na stanici, s míčem a nominací. Je to nejpřímější srovnání s předpovědí: ukazuje, kde váš přijímač skutečně přichází právě teď.

## 8 bis. Barvy a symboly

Každá barva má pouze jeden význam, stejný ve všech tématech (změnit pouze pozadí).

| Barva a značka | Význam |
| Kontinuální modrá čára | Spot z DX clusteru, RBN nebo DM4X mezi dvěma stanicemi |
| Žlutá čára s tmavým okrajem | Spot s komentářem airscatter (reflexe letadla) |
| Tmavě růžová prázdný kruh | Poslouchací stanice na PSKReporter (MQTT) |
| Tenké růžové čáry z kruhu, s růžovou kuličkou v dolní části | Stanice, které posluchač slyšel, s názvem vždy psáno; silnější řádky pro vybraného posluchače |
| Červená čára a červené jméno | Vaše dekódování UDP (WSJT-X, JTDX, MSHV): Kdo jsi? |
| Červené čáry blikající tahy | QSO probíhá s touto stanicí: Zavolal vám a zavírání ještě nedorazilo, nebo je DX Call WSJT-X / JTDX s povolenou převodovkou. Pokračujte s RR73, RRR nebo 73 |
| Zelená čára | QSO desunti dal live: closing RR73, RRR nebo 73 UDP view |
| Černá čára s bílým okrajem | Někdo vám volá: trvá až RR73/RRR / 73 nebo až 3 minuty |
| Skvary 45 ° | Doprava s nejméně jednou stanicí za zvolenou korunou: Žádné čáry, pouze čtvercový lokátor |
| Hledat lilly s vzdáleností na severu | 1000, 700 a 500 km kolem vašeho QTH; tato volba je výraznější |
| Kruh a bod v barvě tématu | Vaše QTH, se jménem a domácím |
| Oranžová koule s gravy 143,050 | Radar GRAVES ( JN27SI) |
| Vzorek od modré po červenou | Hepburn trope index (0-10) nebo třída F5LEN, nebo jejich průměr; teplejší barva, lepší podmínky |
| samet šedý | Oblast bez tropo dat pro zvolený čas |
| Šedomodré čáry s hodnotou v hPa | Isoberit každých 4 hPa; H při modrém vysokém tlaku, L při červeném nízkém tlaku |
| Červené, modré a fialové tečkované čáry | Isotermie na 850 hPa každých 4 ° C: červená nad nulou, modrá pod nulou, fialová nula |
| Vzorek oranžový | Termální inverze: vzduch na 925 hPa teplejší než na zemi, intenzivnější s silnější inverze |

V podezíravých zprávách se na každé kartě píše platné pravidlo: zelené Dobré, červené Scarta, šedé žádné pravidlo.

Jaké jsou lilové čtverce pokryté 45 °. Provoz mimo QTH vás zajímá, abyste pochopili, kde je šíření otevřené, ale jeho linky, stovky kilometrů dlouhé a často desítky, pokrývají mapu a skrývají to, co pro vás znamená: otvory, které můžete využít z vaší stanice. Z tohoto důvodu, za zvolenou korunou, program nevykresluje čáry, ale vybarví lokátor čtverců (2 ° × 1 °) zúčastněných stanic. Výsledkem je mapa "aktivních zón": kde se objeví mnoho okolních lilových čtverců, je zde vedení nebo otevření, i když se vás to netýká přímo; Pokud se oblast rozšiřuje směrem k vám, je to signál, aby se anténa v tomto směru. Na 45 ° palubní deska umožňuje vidět pod tropo indexu a mřížky, takže porovnají na první pohled předpověď (spodní barva) a skutečný provoz (šeřík). Vaše QTH náměstí nikdy nestuduje, protože to je výchozí bod a ne vzdálená oblast. Pouze čtverce, které jsou všechny za korunou jsou barevné: čtverec křížený korunou, nebo uvnitř, zůstává volný, i když je jedna ze dvou stanic. Chcete-li vidět řádky, stačí vypnout krabici nebo vybrat větší korunu.

Kritéria, která rozhodují o tom, co se objeví na mapě a co je vyřazeno, jsou vysvětlena jedním v dokumentu 1, oddíl 11 ter.

## 9. Časová lišta

Níže na mapě: datum a čas uvedené předpovědi, kurzor od + 0 do + 72 h za 6 hodin, index Legenda 0-10 s názvem vybraného zdroje a čas modelového závodu pro isobare. Trumpet a isobare index sledovat kurzor; skvrny, stanice poslouchat a vaše dekódování jsou pozorování okamžiku a vždy zůstávají ty "adex". Když kurzor není na + 0, jsou oslabeny, aby si uvědomili, že nepatří do doby předpovědi.

## 10. Jak přečíst překrytí

- Vysoký tlak + vysoký index + spot podél barevné oblasti: otvor je tam a je ten, který je naplánován. Je čas zavolat tímto směrem.
- Vysoký index, ale žádné místo: nebo tam nikdo není aktivní, nebo předpověď je příliš optimistická. Poslouchací vrstva pomáhá rozlišovat: pokud jsou v oblasti přijímače a nic necítí, druhá hypotéza je pravděpodobnější.
- Dlouhá místa mimo barevné oblasti: vysoký kanál, který povrchové předpovědi nevidí, jako v případě LA- UA6 [3]; nebo jiné šíření uniklo z filtru (např. E sporadic bez značky). DF5AI a DK5YA rozlišují tropo a E sporadik také od hodiny: otvory, které se zrodí a skončí společně s difuzní E sporadic jsou stěží [5] tropo.
- Hepburn a F5LEN nesouhlasí: porovnejte dva zdroje s tlačítky a uvidíte, kde skutečné skvrny padají.
- Tlak dolů: G4MCU následoval otvor na majáku SK6VHF až do poklesu tlaku; 1012 hPa považoval za dokončenou událost [13]. Podívejte se na isobare rolování časové lišty: je-li vysoký tlak stažen, otvor pravděpodobně následuje.
- Signály, které jdou nahoru: ve WSPR studiu na 144 MHz KP4MD vzrostl poměr signál / hluk přímo v oknech, kde Hepburn předpovídal zlepšení, a klesl, když předpověď odstranila [14]. pokud na stejné cestě roste, otvor se tvoří.
- Horské cesty: index nebere v úvahu [1] reliéfy, ale spojení mimo Alpy existují také díky konvekční buňky a vlny atmosférické gravitace [4], a LA0BY pracoval 9A a S5 přes hory, aniž Hepburn předpovídá to [3].

## 11. Otevřený bod

- Povolení Hepburn a F5LEN používat své mapy v programu.
- Kalibrace převodních koeficientů F5LEN → stupnice 0-10.
- Kalibrace kontrolních bodů libovolného typu mapy, která se přetvoří, pokud zdroje změní grafiku.
- Volitelná trasa s teplotou ve výšce (850 hPa) od GFS.

## Hlavní zdroje

1. W. R. Hepburn, DX Info Centre, "The Hepburn Tropo Index (HTI)" a "Troposferic Ducting Forecure" stránky (dxinfocentre.com). Index definice a stupnice, negativní hodnoty a více než10, vyloučení scatters a reliéfy, kroky3 a6 hodin za6 dní, upgrade na18 UTC. Původ údajů z globálního kanadského modelu: G. Meisse, AMSAT-BB,27 /08 /2007.
2. F5LEN, předpověď pro Evropu (tropo.f5len.org / contasts-for-europe) a světová předpověď VHF tropoferické propagace (tropo.f5len.org / WW) a poznamenal "Nová verze VHF troposférické prognózy šíření nástroje". Index N, 40 kroků od 3 hodin, 4 aktualizace za den, hodnota a lokátor za kliknutí.
3. S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", 41 ° Nordic VUSHF Meeting, Skjeberg 2019. Normální gradient 6,5 ° C / km a inverze do 10-15 ° C / km, úloha teploty a vlhkosti, vysoký stabilní tlak, LA- UA6 událost viditelná při vysoké teplotě a ne v Hepburn, LA-9A / S5 spojení za horami.
4. F. Egano IK3XTV, "Propagace VHF - Příspěvek konvektivních buněk a atmosférické gravitační vlny v tropo šíření". Význam prvních 3 km, mořské cesty, konvekční buňky, gravitační vlny na Alpách, vysokotlaké spoje, společný objem až 3-4 km.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, "Very Long Distance Propagation in the 144 MHz Band", 24 / 7 / 2003. Vertikální profily teploty a rosného bodu rozpoznat inverze, použití Hepburn indexu, rozdíl mezi tropo a E sporadik podle hodiny.
6. J. N. Gannaway G3YGF, "Troposféric Scatter Propagation", QST November 1983. Ekvivalent poloměru Země a ztráta dráhy používané pro indexovou a bodovou kartu (viz dokument 1).
7. VHF / UHF DX Book, kapitola 2 "Troposférické šíření", s. 2-23... 2-30. Obrázek refrakční zóny podle dN / dh (standard − 39 N / km, mezní hodnoty − 79 N / km a 0) a povrchové a vysokokanálové profily. Text dodané kopie není čitelný: byly použity pouze údaje.
8. Shrnutí projektu 8 / 11 / 2026, poskytnuté autorem programu. Barvy mapy, požadované projekce, vrstvy, časová lišta.
9. Přírodní Země, vektorová data pobřeží a hranic, veřejná doména (naturalearthdata.com). Obecný odkaz, není přiložen.
10. J. P. Snyder, "Map Projections - A Working Manual", USGS Professional Paper 1395, 1987. Formuly Mercator projekce a azimutale equidtanti. Obecný odkaz, není přiložen.
11. Doporučení ITU-R P.453, index radiového lomu. Refraktivní vzorec Práh N a potrubí při -157 N / km. Obecný odkaz, není přiložen.
12. NOAA NCEP, Global Forecast System (GFS). Tlak na hladině moře 0,25 °, čtyři závody denně. Obecný odkaz, není přiložen.
13. G4MCU, články s tagem "troposferic" (g4mcu.net / tag / troposferic), 2018-2025. Refrakce, vedení a rozptyl; častější kanály s vysokým tlakem, zejména na moři; maják SK6VHF následoval až k poklesu tlaku na 1012 hPa (červen 2019).
14. C. F. Milazzo KP4MD, "VHF / UHF WSPR Propagation Study" (qsl.net / kp4md / 144 _ mhz _ wspr.htm), 2012-2014. SNR WSPR hlásí, že ve zlepšovacích oknech předpovídaných společností Hepburn roste na 144 MHz a při vymizení předpovědi klesá.
15. Open-Meteo, předpověď počasí API (open-meteo.com), zdarma nekomerční použití. Tlak na úrovni moře modelu GFS na více bodů v jedné žádosti.
16. mqt.pskreporter.info, distribuce MQTT v reálném čase reklamy na pskreportér. Informace: Nominace a domácí příjemců (viz dokument 1).
