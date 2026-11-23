LIVETROPOVHF · technický dokument 1
# Provoz programu

> Automatický překlad italsky: v případě pochybností stojí za italský průvodce.

Co program dělá, odkud bere data a jak je mění v to, co vidíte na mapě a v seznamu. Verze ze dne 9. listopadu 2026 týkající se projektu v C # (.NET 10 + Avalonia).

## Shrnuto.

- Účel: tropo předpověď (Hepburn [3], F5LEN [4], isobare GFS [18]) a skutečné pozorování pásů ve stejném okně, podle seznamu LA0BY [1].
- Sběr dat: dva DX cluster, Reverse Beacon Network [9], dm4x cluster a PSKReporter [5] MQTT flow, UDP dekódování z WSJT-X, JTDX a MSHV [8].
- Spot: pásmové segmenty, negativní filtr, airscatter ve žlutě, pozice lokátory nebo C ALL3. TXT [6] [7].
- Kdo poslouchá: tmavé růžové kruhy s růžovými čarami vždy viditelné směrem ke stanicím cítil [5].
- Spot card: distance, azimut, tropo index na cestě a troposcatter ztráta s metodou G3YGF [2].
- Jazyky Hepburn a F5LEN: [3] obraz a kalibrační lavice [4].
- Sledujte: NTP ovládání na 10 serverech každých 3 minuty [10] [11].
- Podezřelé zprávy: EME v Q65/JT65 uznán s pozicí Měsíce, zkrácené lokátory, Skimmers, karanténa a posluchači, s pravidly se naučil sám [20].
- Kritéria pro výmět a význam barev shrnutých ve dvou tabulkách (oddíly 11 ter a 11 kvater).
- Témata, 36 [17] jazyky, exportovat dokumenty do Wordu s editovatelnými vzorci [16], přenosný program a multiplatformní [12] [13] [14] [15].

## 1. Účel

Program dává dohromady v jednom okně dvě věci, které obvykle vypadají na různých stránkách: předpověď šíření troposfer (Tropo index Hepburn a F5LEN, isobare) a pozorování toho, co se skutečně děje na pásmech (skvrny z DX clusteru, Reverse Beacon Network a PSKReporter, plus seznam stanic, které poslouchají). Referenční pásmo je 144 MHz; body se shromažďují od 50 MHz nahoru.

Nastavení následuje po kontrolním seznamu LA0BY pro extrémní tropo události: v předchozích dnech se podíváte na prognózy Hepburn a F5LEN a teplotní profily v nadmořské výšce, zatímco událost je na DX clusteru a na místě mapuje důkazy, že otevření začíná [1]. LA0BY také podotýká, že jediné mapy Hepburn / F5LEN nestačí k předvídání nejdelších spojení [1]

## 2. Okno a karty

Okno je jedno a nerozděluje se do mobilních oken. Od shora dolů:

- Řádek všeobecných nastavení: Volání a Locator vlevo; vpravo hodiny UTC (např. 08 / 11 / 2026 20: 33: 38 UTC, jantarový text) následované NTP: OK nebo NTP: FAILED, tematické menu a menu jazyka.
- V pěti skupinách: Mapa a místo; Předpovědi (Propria β, předpověď vypočtená programem a popsaná v Příručce předpovědi, pak Hepburn a F5LEN); GRAVES; Data a zdroje (DX cluster, PSKReporter, Reverse Beacon, UDP, Jmenování a podezíravá hlášení); Průvodce (Programová operace, Mapovací budova, Jak tvarovat tropo, manuální předpověď, Tipy pro operátora, s vyhledávacím políčkem, ve kterém se text objeví). Pod paprskem se zobrazí podjazyky skupiny; každá skupina si pamatuje poslední použité.
- Obsah karty a v dolní části stavové lišty se stavem zdrojů, databází jmen a dobou poslední aktualizace.

Nastavení horního řádku platí pro všechny záložky: vybrané téma také vybarví sběr dat a pozadí dokumentů; Stránky dokumentů zůstávají bílé jako list, takže dobře tisknou.

## 3. Volání a domácí

Maidenhead domácí může mít 4 nebo 6 znaků (např. JN70 nebo JN70CN). Program ji převádí na souřadnice středu náměstí: pole 20 ° × 10 °, čtverec 2 ° × 1, subkvadrat 5 ′ × 2,5 ′. Pokud text není platný lokátor, okraj rámečku se zčervená a stanice se na mapě neobjeví. Lokátor také slouží jako centrum azimut projekce a jako výchozí bod pro vzdálenosti a azimut v reklamní tabuli.

Call je jméno používané jako cluster přihlášení. Na prvním startu, dokud není platná nominace, se klastry nepřipojují: okno žádá o jméno (a případně pronajímatele); potvrzuje, že název je zkopírován jako uživatelské jméno v poli přihlášení každého clusteru, který má prázdné a clusters odejít. S "Později" zůstat v klidu, a tlačítko Connect každého clusteru vyžaduje nominaci znovu. MQTT, UDP a NTP se stejně nepřihlašují a neodcházejí.

## 4. Sběr údajů: zdroje

Každý zdroj má svůj vlastní oddíl v záložce Shromažďování dat, s připojením / odpojením, rozpoznávanými a připojenými boxy při spuštění, stavu, metrech a registraci přijatých řádků. Nespoutané nebo vyřazené řádky z filtru se v registru neobjevují: započítávají se pouze sektorové metry. Sekce jsou zkřížené o dvě ku dvěma.

| Zdroj | Připojení | Co přináší |
| 1. Volba DX clusteru | on0dxk.dyndns.org: 8000 příkaz set / filtr dxbm / pass 6 | Spot amatérských rádia, také SSB a CW. Na ON0DXK jsou VHF filtrovány a jsou povoleny s indikovaným povelem (poi pass 4, 2...). |
| 2. Druhý zdroj | dxmaps.com: 7300 (DXMAPS, EA6VQ) a s50clx. si: 41112 ( S50CLX, Slovinsko) | DXMAPS sbírá VKV skvrny a také dává RBN skimmer skvrny (set / skimmer ovládání, již nastaveno); S50CLX je DX Spider cluster. Přihlaste se se svým jménem, editovatelné adresy. |
| 3. Reverse Beacon Network | telnet.reversebeacon.000 CW / RTTY · 7001 FT8 | Automatický skimmer spot. Přihlaste se pouze se jménem [9]. |
| 4. Oblouk s PSKReporter | dm4x.ddns.net: 8500 | Digitální Spoty PSKReporter ve formátu clusteru. |
| 5. PSKReporter MQTT | mqt.pskreporter.info 1883 · 1884 s TLS | Každá zpráva o recepci v reálném čase: kdo vysílá, kdo poslouchá a jejich [5] Locators. Kapela 144, 222 a 432 MHz. |
| 6. Počasí a tropo mapy | dxinfocentre.com / tropo _ eur.html tropo.f5len.org / contasts- for- European model GFS | Hepburn [3] a F5LEN [4] tropo index mapy, tlak hladiny moře pro isobare (GFS přes Open- Meteo [18]). Hepburn a F5LEN musí být povoleny autorům. |

Cluster telnet (zdroje 1-4). Po připojení program čeká na požadavek na jméno (text, který obsahuje přihlášení, volání:, volací znak nebo váš hovor); pokud nedorazí do 4 sekund, zašlete prosím přihlášení. Poté zašlete příkazy sekce, oddělené od bodu a čárky, jeden každých 0,6 sekundy. Před čtením řádků se odstraní kontrolní znaky telnetu. Pokud spojení spadne a Riconnetti je aktivní, zkuste to znovu po 10 sekundách (15 pokud server vůbec nereaguje) [6].

PSKReporter MQTT (zdroj 5) Program se zapisuje do tématu pskr / filter / v2 / {banda} / {modo} / {tx _ call} / {rx _ call} / {tx _ grid} / {rx _ grid} / {tx _ dxcc} / {rx _ dxcc} upevnění pouze pásma a ponechání ostatních polí bez znaku + [5]. Přesný název, s nímž server označuje pásma (např. 2m, 1.25m, 70cm), musí být zkontrolován při prvním připojení.

## 5. Od shlukové čáry k bodu

Jeden řádek je místo, když má formulář DX de SPOTTER: frekvence DX komentář hhmmZ. Program odvozuje pozorovatel, stanice DX, frekvence v kHz, komentář a nyní UTC; názvy odstranit přípona Skimmers (- #, -1...). V tomto pořadí pak platí následující pravidla [6].

| Pásmo | Sekce přijata |
| Gravy (s 144) | 143,040-143,060 MHz |
| 144 MHz | 144 - 148 MHz |
| 222 MHz | 220 - 225 MHz (pouze oblast 2 ITU, Americas) |
| 432 MHz | 420 - 450 MHz |
| 1296 MHz | 1240 - 1300 MHz |
| 2320 MHz | 2300-2450 MHz |
| 10 GHz | 10,5 GHz |

1. Kapela. Frekvence musí spadat do jednoho z řízených segmentů; všechny ostatní, a zejména všechny komerční pod 49 990,0 kHz, není zobrazen a počítá se jako "mimo pásmo".
2. Tak. Jedním z nejznámějších způsobů komentáře je FT8, FT4, Q65, JT65, ISCAT, CW, SSB, FM, RTTY, PSK31, MSK144, FSK441, JT6M); USB a LSB se stávají SSB.
3. Negativní filtr. Místo je vyřazeno, pokud obsahuje režimy MSK144, FSK441, JT6M, JTMS, ISCAT nebo štítky < AU > < AUE > < ES > < MS > < FAI > < F2 >, s nejadernými zdviháky a dokonce i mezerami v závorkách. To jsou způsoby a šíření, které nejsou tropické: Scatter meteor, aurora, a sporadic, F2.
4. Rozptýlení vzduchu. Pokud komentář obsahuje < AS > nebo < ACS > (také smíšený, např. < As >) nebo označuje místo jako airscatter (airscatter, airscatter, airscatter, airscatter), zůstává místo žlutě, ale je nakresleno s tečkovanou čárou (tmavý skluz, aby zůstalo čitelné na jasné mapě) a zvýrazněno v seznamu.
5. Umístění stanic. Pokud v komentáři existují dva lokátory (např. JN54 < JN83), první je z těch, kteří se místo a druhý ze stanice DX; s jedním lokátorem, že je ze stanice DX. Co chybí, je v C ALL3. TXT, pouze s přesným názvem (pro notebooky viz níže). Pokud stanice zůstává bez pozice, místo se objeví v seznamu s poznámkou "neznámá pozice", ale ne na mapě.
6. Dvojnásobek. Reklama se stejným DX, stejným pozorovatelem, stejným pásmem a stejným zdrojem, který je již přítomen, ji nahrazuje místo přidávání.

C ALL3.TXT je databáze jmen → lokátor používaný JTDX, ve formátu CALL, LOCATORE,,,. Soubor projektu obsahuje 1 048 847 jmen, všechny s platným lokátorem; hlavička souboru stále hovoří o 34 323 nominacích, protože zůstal, že z prvního vydání [7]. Je načtena v pozadí při spuštění, takže okno je okamžitě použitelné. Soubor je vyhledáván vedle programu, ve složce Data, ve složce, ze které byl program spuštěn, a o jednu nebo dvě složky výše; pokud není, okno ho řekne při spuštění a stavová lišta ho signalizuje červeně. Jeho jméno vždy používá svůj 6- znak. Pouze pravidlo pro lokátory: pokud jeden obdržel (spot, MQTT, dekódování, QSO desunti) má méně než 6 znaků a C ALL3. TXT má jeden až 6, to je vždy stojí za C ALL3.TXT; pro notebooky (F / IC8TEM, IC8TEM/P) je pouze přesný hlas: bez, stanice zůstává bez domácího a není navržen jinde. Pravidlo platí i v době čerpání, takže data, která byla doručena před načtením databáze, budou spuštěna sama.

Jedna databáze: C ALL3. TXT. Od 1.3.4 všechny lokátory jsou v jednom souboru, C ALL3.TXT, který zůstává ve formátu JTDX a WSJT-X (CALL, LOCATORE,,,,,) a může být použit, jak je také v WSJT-X, JTDX a MSHV. Žádné další složky nejsou oddělené. V horní části souboru jsou za hlavičkou dva bloky označené poznámkovým řádkem: / / [LIVETROPOVHF- MANUALI] s ručními položkami a / / [LIVETROPOVHF- CLUSTER] s opravami ze clusteru; pak / / [LIVETROPOVHF- FONTI] se všemi ostatními položkami v abecedním pořadí. Každá nominace se objeví jednou. V sublingválních Data a zdroje › Jmenování (C ALL3) hledáte jméno, píšete domácí a stisknete Přidat / změna: hlas jde do bloku hlasů ručně a to je cenná především, i přes lokátor, který stanice sama posílá. {\ cHFFFFFF} Odstraní nominaci z C ALL3.TXT (další aktualizace pokračuje ze zdrojů, pokud ji mají). Přidat soubor... přečtěte si seznam CALL, LOCATOR (také přijímáte bod a čárku, tabulaci a prostor) a zapište jej mezi ručně vybrané; zvolený soubor není zkopírován. Reload readed C ALL3.TXT z disku, po editaci editorem. Kompletní pořadí zdrojů: vlastní lokátor, ruční hlasy, shlukové opravy, zbytek C ALL3. TXT (zdroje, naučené živé lokátory a G7VRD), přijímaný lokátor (který je platný pouze v případě, že má 6 znaků a nekončí LL nebo MM).

Aktualizace C ALL3.TXT Ve stejném podjazyce tlačítko Update nyní stáhne zdroje (C ALL3. TXT WSJT-X Vylepšený, C ALL3. TXT EME W7GJ, grid _ data.bin JTDX), připojí se k nim (první, který má nominační vítězství), zkontrolujte, že jsou alespoň 50,000 jmen (jinak se nic nemění), ukládá C ALL3. TXT Předchozí C ALL3. TXT položky zůstávají, a 6- znak lokátor zůstává, kde zdroj má pouze 4 ze stejného náměstí. Ruční hlasy a korekce z clusteru zůstávají nahoře, beze změny. S políčkem Update každý den, kontrola probíhá na začátku. Program přepisuje C ALL3. TXT bezprostředně po každém hlasu ručně a, pro naučené lokátory, maximálně každých 10 minut a zavírání; C ALL3. TXT balíčku je pouze pro čtení a atribut se odstraňuje sám. Instalátoři Linux nepřepíší C ALL3. TXT již přítomen; že pro Windows instaluje pouze v případě, že chybí. ALL3

Automatické opravy ze shluku. V automatických spotech DM4X a RBN přináší komentář dva lokátory, například KN09tp < > KN09: první je to, co stanice, kterou vidíte deklarovat sám o sobě, na 6 znaků; druhý je, že stanice cítil, často pouze na 4. Pokud má první 6 znaků (a nekončí s LL nebo MM) a je odlišný od toho v databázi, program opravuje C ALL3. Pouze TXT: korekce jde do bloku / / [LIVETROPOVHF- CLUSTER], s datem posledního potvrzení ve třetím poli, je platná nad zdroji (pod ručními hlasy) a objevuje se mezi podezřelými signály aktualizované jako "C ALL3 ". Přežije tedy C ALL3. TXT aktualizace. Nepotvrzená oprava po dva roky ztrácí přednost a zůstává jako normální hlas.

Poslední zdroj: G7VRD. Pokud nominovaný nemá 6- znak v C ALL3. TXT, program se ptá v pozadí na veřejné API G7VRD (api.g7vrd.co.uk, "locator by callsign"), který shromažďuje lokátory automaticky z provozu sítě. Přijímáte pouze 6- znak, který není středem náměstí (LL nebo MM); žádosti jsou seskupeny, maximálně 40 nominálních za minutu, a každý nominovaný žádá jednou denně, protože je to hobistická služba bez záruk. Nalezený lokátor vstupuje do C ALL3. TXT, pokud název není nebo má pouze 4 znaky na stejném náměstí; vyhledávání se vypne s políčkem v záložce name.

Jmenování s barem. V PSKReporter MQTT témata, bar odděluje úrovně, pak složené názvy přicházejí s bodem místo baru ( IV3. HB9CAT). Program vrátí bar zpět, a pokud zpráva obsahuje celá jména, použijte tato: IV3/HB9CAT zůstává IV3/HB9CAT na mapě, seznamu a databázi.

Lokátory se učí živě. C ALL3. TXT neobsahuje všechny: například SP9EMF a SP9NVY tam nejsou, a pak zůstává 4- znakový lokátor zaslaný PSKReporter (karta to říká). Pro snížení těchto případů program píše v C ALL3. TXT každý 6- znak, který pochází z jakéhokoliv zdroje (PSKReporter, jako je vysílač nebo jako přijímač, cluster, UDP dekódování) pro nominaci, že C ALL3. TXT nemá nebo má pouze 4 znaky na stejném náměstí; nikdy nemění hlas na 6 znaků. Do 1.3.3 byly tyto lokátory v Data / LOC6.TXT.

Přechod od 1.3.3. Na prvním startu 1.3.4 soubory kromě předchozích verzí (Dati / C ALL3 -MANUALE.TXT, Data / C ALL3 -AGGIUNTE, Data / C ALL3 -CLUSTER. TXT, Data / LOC6. TXT a Data / C ALL3 - G7VRD. TXT) jsou sloučeny do C ALL3. TXT Uvolnění psaní, staré soubory vzít.fuso rozšíření, již číst a můžete smazat.

Automatické přijímače. RBN, dm4x a PSKReporter často vykazují jména s přístrojem označujícím přijímač: -SDR, / SDR, - # (skimmer RBN) nebo -1, -2... (plus přijímače stejné stanice). Pro pronajímatele je soubor odstraněn: OK2OZL -SDR bere OK2OZL JN89UE z C ALL3.TXT, pokud stanice ještě nepošle 6- znak. Zobrazený název zůstává ten s přístrojem.

Lokátory přerušeny. Ne všechny zdroje léčit rozšíření svého domácího: mnoho nastavit na 4 znaky, a některé automatické carry- over systémy záměrně zkrátit 4- znak lokátor, dokonce i ty, které získávají data z reálného poslechu FT8, FT4, Q65 a JT65, nebo dokončit s centrem náměstí (LL nebo MM, např. JN89MM). Z tohoto důvodu se 4-znak, nebo 6-znak, který končí LL nebo MM, nahrazuje C ALL3. TXT stejného náměstí. Pokud se správná poloha a střed náměstí neshodují s několika tucty km, chyba zůstává malý ve srovnání s tropo vzdálenosti.

## 6. Kdo poslouchá

Každá MQTT zpráva z PSKReporter hlásí jméno a lokátor těch, kteří přijímají [5], takže nemusíte hledat v C ALL3. TXT. Pro skupinu zvolenou ve voliči je program držitelem, pro každou přijímací stanici: domácí, poslední recepce, dekódované způsoby, počet stanic a maximální vzdálenost. Stanice opustí seznam, když již neobdrží nic ve zvoleném rozsahu (3, 5, 10, 15, 30 nebo 60 minut).

Na mapě je každý přijímač tmavě růžový prázdný kruh a od každého startu plné růžové linky směrem k stanicím, které cítí a posílá zpět na síť. Řádky jsou vždy vidět, bez kliknutí. Jmenování odposlouchávacích stanic, sluchových stanic a dvou hlav každého místa jsou vždy zapsány na mapě, aniž by bylo nutné kliknout (stejné jméno je zapsáno jednou). Kliknutím na kruh se jeho řádky stávají ještě tlustší a otevře kartu s detaily. Pokud má lokátor z MQTT méně než 6 znaků, program používá ten z C ALL3.TXT (asi jeden milion jmen), za předpokladu, že je v souladu s náměstí obdržel.

Rozptýlit meteor režimy. Stejně jako u cluster spotů, MQTT poměry v MSK144, FSK441, JT6M, JTMS nebo ISCAT jsou vyřazeny a započítány pouze; totéž platí pro dekódování UDP těmito způsoby.

Ne skutečné posluchače. Některé stanice prohlašují, že přijímají v 144 MHz, ale ve skutečnosti je přijímač na jiném pásmu (špatná frekvenční indikace): jejich zprávy by navrhly neexistující otvory tisíce km. Program to kontroluje sám. Každá stanice, o které posluchač říká, že má slyšet VHF, je sledována 30 minut na všech pásmech, se zvláštním nápisem MQTT (pskr / filter / v2 / + / {nominative} / #) [5]. slouží jako důkaz. Pokud jsou alespoň dvě stanice slyšené posluchačem ve stejných minutách (± 10) aktivní na jiných pásmech, je posluchač považován za nepravého a všechny jeho zprávy o VKV jsou vyřazeny; registr MQTT uvádí jméno a stanice, které ho zradily. V seznamu bodů, vedle měřičů, tlačítko MQTT ukazuje stav proudění (zelený list, pokud není připojen, červený, pokud ne) a kolik posluchačů bylo v pásmu vyřazeno a zvoleno rozsah; kliknutím na tlačítko otevře kartu se seznamem výmětů a pro každého ze stanic nalezených na jiných pásmech.

Dlouhý porcelán. Spojení přes 1000 km (Clumbing Spot, MQTT Listening, QSO Desunti) není okamžitě zobrazeno: zůstává zachováno a čítač "drží > 1000 km" uvádí číslo. Je odhaleno, když další spojení, mezi různými stanicemi a alespoň polovinou podél, to potvrzuje jedním z těchto kritérií: stejná stanice v hlavě (například stejný posluchač) a druhá stanice ve stejném směru do 10 °; podobná chodba, s oběma hlavami v rozmezí 200 km od hlavy druhého spojení; paralelní trasa se směrem do 10 ° a boční vzdálenost do 200 km, překrytá podél trati. Oba se objeví a MQTT log ukáže důvod. Jeho dekódování UDP je vždy viditelné a platí jako potvrzení pro ostatní. Zadržené spoje zůstávají v paměti po dobu 60 minut.

Nic pod 144 MHz. Program pracuje od 144 MHz nahoru: vše pod 143 MHz je vyřazeno. Výjimkou je GRAVES, francouzský radar vysílající v CW na 143,050 MHz od Broye-lès-Pesmes ( JN27SI): reklama mezi 143,040 a 143,060 MHz, nebo s GRAVES ve jménu, objeví mezi 144 MHz s radarem jako vysílač. Inzerce říká, že pokud je přijímač uvnitř paprsku (sektory D, C, B, A od 90 ° do 270 ° azimutu viděn z radaru, 6 kroků od 7,5 ° každý) nebo ven; tabulka GRAVES, stejně jako v YA-MS, ukazuje oblohu viděnou z radaru: sektory, krok osvětlený v té době (sekvence PE1ITR 2021: 0,8 s na krok, cyklus 4,8 s Data a geometrie pocházejí z YA-MS. Cítit to v tropech je těžké, ale může se to stát.

Rehabilitace z lidských klastrů. Stanice, která se také objeví na jiných pásmech, se nezapočítává jako podezřelá, pokud ve stejných minutách (± 30) DX lidské klastry ( ON0DXK, GB7RAU; ne dm4x, RBN nebo jiné automatické zdroje) aktivní poškození daného pásma, v místech vytvořených jinými jmény: je to případ stanic, které skutečně pracují na více pásmech dohromady. Proto se vyřazený posluchač vrací viditelný (registr MQTT píše "rehabilitovaný") a lze odhalit dlouhotrvající odkaz pro nízká pásma.

Aktivní stanice pod 144 MHz. V karanténě dlouhých spojů stanice, která je ve stejných minutách slyšena na pásmech pod 144 MHz (ze zpráv MQTT ostatních pásem), nepotvrzuje další spojení a není odhalena, i když by to potvrdila podobná nebo paralelní trasa.

## 7. Seznam reklam

To zabírá 35% vpravo od karty Mapa a místo. Top: název, volič pásma (50, 70, 144, 222, 432, 1296, 2320 MHz, 10 GHz), volič rozsahu (poslední 3, 5, 10, 15, 30, 60 minut) a klíč Update. Níže, čtyři metry: viditelné skvrny, AS / ACS, vyřazen z filtru, stanice poslouchat.

Každý řádek ukazuje typ řádku, čas UTC, DX → DE, způsob, vzdálenost v km a na druhém řádku, zdroj, frekvence a komentář. Nejnovější je nahoře. Jedno kliknutí na řádek vybere místo: na mapě řádek roste a dvě jména se objeví.

## 8. Spot card a troposcatter loss

Pro zvolené místo karta zobrazí lokátory, vzdálenost, azimut, frekvence, režim, zdroj, nyní, komentář, průměrný index tropo podél cesty a odhad ztráty trasy pro roztroušené tropo.

Vzdálenost a azimut se počítají v maximálním kruhu s poloměrem Země 6371 km (vzorec emisenoverso pro vzdálenost, počáteční azimut ze stanice DX do pozorovatele).

Ztráta troposcattera. Základní ztráta, pro dvě stanice na hladkém terénu, je součet ztráty volného prostoru a difuzní ztráty: G3YGF [2]

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

Ztráty jsou v dB, D je vzdálenost v km, f frekvence v MHz, θs difuzní úhel ve stupních.

8497 km je ekvivalentní poloměr Země (4 / 3 fyzického). θA a θB závisí na horizontu každé stanice:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

s Δh výška překážky, která činí z horizontu, pokud jde o stanici a její vzdálenost. Každá úroveň horizontu stojí asi 10 dB [2]. Program nezná horizont stanic, takže ukazuje základní ztrátu s θA = θB = 0 a prohlašuje ji ("hladká půda"). G3YGF přidává klimatický termín, kde N je reaktivita na půdu [2]; použije se, když N je k dispozici z modelu počasí:

```math
L_{N} = 0,2 \cdot (N - 310)
```

Ověření: pro 110 km až 144 MHz vzorec dává 166,5 dB, oproti asi 168 dB lůžek na grafu článku. Pro 500 km až 144 MHz dává cca 206 dB. Očekávaný poměr signál / hluk je kapacita systému (eirp + efektivní citlivost přijímače) minus ztráta: s modelovým systémem G3YGF 144 MHz (198 dB) by cesta 500 km dala v SSB [2] asi -8 dB, tedy spojení je pouze s lepším šířením čistého rozptylu.

## 9. Moje dekódování: UDP z WSJT-X, JTDX a MSHV

WSJT-X, JTDX a MSHV mohou poslat UDP pryč každou linii, kterou dekódují. Program poslouchá na 127.0.0.1: 2237 a opakuje stejný balíček, byte per byte, na 127.0.0.1: 2238, takže ostatní programy, které používají tento port (GridTracker, JTAlert, logger) i nadále pracovat, pokud je přesunete na 2238. Adresy a dveře se mění v sublingvální UDP sbírky dat; jako odposlouchávací adresa můžete také použít multicast skupiny (např. 224.0.0.1), pokud je dekódovací program nastaven takto.

Packages follow the WSJT-X network protocol: a initial magic number 0xADBCCBDA, schema number and message type. Program využívá zejména tři typy [8]:

- Heartbeat: říká, který program přenáší data a s jakou verzí.
- Stav: frekvence vytáčení a režimu; z frekvence, kterou dostanete pásmo (se stejnou tabulkou bodů).
- Decode: Nyní, SNR, čas a frekvence odpadu, způsob a text dekódované zprávy.

Z textu dostanete stanici, kterou jste slyšeli: v CQ 9 A2PQR JN83 je stanice po CQ (uložení DX, EU, TEST...), v IK0JKL IT9MNO JM68 je druhá, tj. kdo vysílá. Lokátor je ten ze zprávy, pokud existuje (4 znaky, odlišné od RR73); jinak hledáte v C ALL3. TXT. Poslouchají linky: stanice, které váš přijímač slyší. Na mapě se stávají červenými liniemi od QTH do stanice, s názvem; vrstva se rozsvítí s dekódovanou krabicí ode mě a vyžaduje platný lokátor. V seznamu vpravo čítač řekne, kolik stanic dekódujete na zvolené skupině.

QSO desunti dal live. Pokud dekódování je závěrečná zpráva ve formě C ALL1 C ALL2 RR73 (nebo 73 nebo RRR), například IC8TEM IK3FHP RR73 nebo IK3FKP SV8PEX 73, obě stanice dokončily spojení. Program to zaznamenává jako QSO, s lokátor každého z poslední zprávy, ve které vyhlásil, nebo, pokud neexistuje, z C ALL3.TXT [7], a čerpá ji na mapě jako zelenou čáru mezi oběma stanicemi (na stránkách "QSO desunti dal live"). Pár stanic se počítá jednou za pásmo; pásmo a rozsah zvolený v seznamu jsou platné.

Ve stejném podjazyce zobrazuje Debug panel text každého přijatého balíku (typ, čas, hodnoty a zpráva), na vyžádání také hexadecimální byte a řádek pro odeslání. Je to pro kontrolu, že dekódovací program vlastně posílá data.

## 9 bis. Jazyky Hepburn a F5LEN

Každý ukazuje, co program bere ze zdroje. Tlačítko "Přečtěte si stránku a stáhněte si všechny obrázky" otevře uvedenou stránku (base dxinfocentrec.com / tropo _ eur.html pro Hepburn [3] a tropo.f5len.org / contasts-for-europe pro F5LEN [4]) a stáhne všechny obrázky v cache vedle spustitelného; také můžete vložit adresu jednoho obrázku. Pro každý obrázek je zde políčko k vyloučení a pole s předpokládaným časem. 31 evropských obrázků F5LEN jsou pojmenovány v době modelového závodu (03.png... 48.png každé 3 hodiny, 54... 96 každých 6 hodin, 108... 192 každých 12 hodin) a program trvá od názvu [4] souboru; pro Hepburn je to pořadí stránky. V záložce F5LEN menu vybere region: Evropa (tropo.f5len.org / contasts- for- europe), Austrálie (tropo.f5len.org / Austrálie, vk03.png... vk192.png) nebo Atlantic mezi Kapverdami a Irskem (tropo.f5len.org / atlantic- ocean / atlantic- d4-ei, atl03.png... atl192.png). Každý má stejné kroky.

Níže je kalibrační počítadlo. Jedno kliknutí na obrázek přidává kontrolní bod (lokátor nebo lat, lon: to trvá nejméně 6) nebo barvu legendy s jeho hodnotou. Program počítá pixel odpovídající → souřadnice s druhým stupněm polynomové interpolace na minimální čtverce [19], čte barvy s tolerancí zvolenou a, pro F5LEN, převádí jednotky N ve stupnici 0-10 s · N + b. "Uložit kalibraci a test čtení" píše soubor ve složce Tarature a kontroluje, na který pixel váš lokátor padá. Před nastavením kalibrace je třeba požádat o svolení autora map.

Regiony. Každý zdroj má více regionů, každý s jeho fixed-name obrázky: Hepburn tr _ map / fcst / {code} {ore}. png? v {data} (eur Europe, nwe North- Western Europe, eeu Eastern Europe, ent North- East Atlantico, nat North Atlantico, eam North America East, nsa South America North America, sam South America, mid- East Asia, nca Central- percental Asia Záložka každého zdroje zobrazuje především náhled fúze: všechny stažené obrázky složené jako na hlavní mapě, s tečkovanou tabulí a názvem každého regionu, šedý závoj, kde žádný obraz pokrývá plochu a posuvník posuvné hodiny; vlevo seznam regionů, každý s krabičkou k použití, počet map číst a z nichž extrémy přicházejí (OCR, ručně nebo továrně; pokud OCR selže text se objeví). Ruční kalibrační lavička zůstává za homonymním tlačítkem. Menu Bench Region vybere, který vzhled, rámeček "Použijte tento region na mapě" obsahuje to v indexu. Rámeček se nachází sám (dvě vodorovné čáry a dvě svislé čáry stejné barvy); k umístění obrázku slouží extrémní lat / lon rámu: pro Hepburn Europe (10 ° W-40 ° E, 27 ° -56 ° N) a F5LEN Europe (15 ° W-40 ° E, 30 ° -65 ° N) jsou již v programu, pro ostatní regiony, které píší ve čtyřech oblastech O E S N čtení štítků osy. Násobitel stupnice činí tyto dva zdroje srovnatelnými: konečná hodnota = násobitel × hodnota legendy. [3] F5LEN [4] F5LEN

## 9 ter. Exportovat do Wordu

Tyto dva dokumenty mají na horní části tlačítka "Export to Word". Soubor je napsán ve formátu Office Open XML; vzorce se stávají Word rovnic (OMML), takže zůstávají Editable s editorem [16] rovnice. Pokud není dostupné okno uložení, soubor končí ve složce Dokumenty vedle spustitelného.

## 9 kvater. Kdo mi volá?

Pokud je vám zpráva zaslána na místní UDP (křestní jméno je vaše, například IC8FAX IK3FHP JN55 nebo IC8FAX IK3FHP -12), program sleduje z vašeho QTH do stanice, která vám říká černá tečkovaná a blikající linka, s názvem a lokátor vedle vás. Lokátor pochází z C ALL3.TXT (včetně ručních hlasů) nebo zprávy. Linie zůstává až do UDP projde uzavření QSO s touto stanicí ( RR73, RRR nebo 73, v jednom ze dvou směrů), maximálně 3 minuty od posledního volání; budete čerpat přes všechno a nedodržují kritéria vyloučení, takže přímý hovor nikdy neuteče.

PSKReporter, přes MQTT, nepředává text zpráv: říká pouze ten, kdo obdržel, s frekvencí, způsobem a SNR. Proto je přímá výzva uznávána pouze vlastním dekódováním UDP.

## 10. Kontrola času (NTP)

Digitální režimy a spotový čas vyžadují přesné hodiny. Každé 3 minuty program vyslýchá 10 NTP serverů (SNTP na UDP 123): 0.pool.ntp.org, 1.pool.ntp.org, 2.pool.ntp.org, 3.pool.ntp.org, it.pool.ntp.org, ntp1.inrim.it, ntp2.inrim.it, ptbtime1.ptb.de, time.google.com, time.cloudflare.com [11].

Pro každou odpověď se vypočítá zpoždění výmětu a vrácení, zahodí abnormální hodnoty a vezme medián, s výpočtem výmětu a zpoždění poskytnuté SNTP [10]. S alespoň 3 odpovědi souhlasíte UTC hodiny programu a další se objeví NTP: OK v zeleném; pokud odpovědi nestačí objeví NTP: FAILED v červené a hodiny zůstávají na počítači. Systémové hodiny se nedotýkají, protože by bylo potřeba administrátorských oprávnění; všechny interní programové plány (skvrny, 5-60 minut okna, dekódování) používají zarovnaný čas. Ovládání se vždy otáčí v pozadí: v rozhraní vidíte pouze výsledek vedle hodin; servery, intervaly a minimální odezva změna v LIVETROPOVHF.ini (ntp _ servery, ntp _ minutes, ntp _ min).

## 10 bis. Obecná kritéria vyloučení

Na všechno, co vytáhnete, se vztahují dvě krabice ve vrstvě (spot, PSKReporter, dekódování, QSO desunti):

- Vyloučeno pod 100 km. Provoz mezi stanicemi menší než 100 km nemusí být významný pro tropo (pozemní vlna, optický rozsah): s on- box nebudete čerpat a nejsou uvedeny v seznamu. Start je vypnutý.
- Více než 1000 km: pouze čtverce. Kolem vašeho QTH (lokátor nastavit začít) je koruna 1000 km ve všech směrech. Je-li jen jedna ze dvou stanic na link je mimo tuto korunu, připojení není nakreslena jako linka: na jeho místě mají barvu šeříku, tečkované na 45 °, lokátor čtverců (4 znaky) ze stanic zapojených. Totéž platí pro stanice PSKReporter, které poslouchají více než 1000 km, které se místo kruhu objevují jako čtverec. Dosud provoz vidíte bez vyplnění mapy linek. Bez platného lokátoru se počítá délka spoje. Čtverce vašeho QTH nikdy nestuduje v šeříku, i když posloucháte stanici za korunou: aplikuje sadu domácí, jakékoliv jméno používá, kdo pracuje ze stanice. Poloměr koruny je vybrán vedle krabičky: 1000, 700 nebo 500 km. Krabice Corone 1000 / 700 / 500 km čerpá tři koruny kolem QTH, čerpané v šeříku, s nejvýraznější volbou a vzdálenost napsaná na sever. V seznamu bodů je vzdálenost následována.. Začíná to. Kritérium platí po karanténě: dlouhé spoje, které jsou stále zachovány, se ani nevyskytují jako čtverce.

Jaké jsou lilové čtverce pokryté 45 °. Provoz mimo QTH vás zajímá, abyste pochopili, kde je šíření otevřené, ale jeho linky, stovky kilometrů dlouhé a často desítky, pokrývají mapu a skrývají to, co pro vás znamená: otvory, které můžete využít z vaší stanice. Z tohoto důvodu, za zvolenou korunou, program nevykresluje čáry, ale vybarví lokátor čtverců (2 ° × 1 °) zúčastněných stanic. Výsledkem je mapa "aktivních zón": kde se objeví mnoho okolních lilových čtverců, je zde vedení nebo otevření, i když se vás to netýká přímo; Pokud se oblast rozšiřuje směrem k vám, je to signál, aby se anténa v tomto směru. Na 45 ° palubní deska umožňuje vidět pod tropo indexu a mřížky, takže porovnají na první pohled předpověď (spodní barva) a skutečný provoz (šeřík). Vaše QTH náměstí nikdy nestuduje, protože to je výchozí bod a ne vzdálená oblast. Pouze čtverce, které jsou všechny za korunou jsou barevné: čtverec křížený korunou, nebo uvnitř, zůstává volný, i když je jedna ze dvou stanic. Chcete-li vidět řádky, stačí vypnout krabici nebo vybrat větší korunu.

## 11. Témata a jazyky

Témata: 18 společných pro programy IC8TEM, stejné jako YA-NEC a E-LAYER ANALYZER: Clear, Night, Black, osm pastelů (modrá, oranžová, aquamarine, zelená, růžová, lilac, indigo, hnědá), Scia meteorica (YA-MS palety), tři IP-TRX-CTRL (Chiaro, Notte, Nero) a Blue, Purple, Green. Od 1.3.4 Chiaro vypadá jako YA-NEC: světle šedé pozadí, ohraničené bílé panely, modré noční karty a modrý přízvuk. V temných tématech také základní mapa se stává tmavou. Barevné měřítko předpovědi zůstává ve všech tématech stejné. Na úzkých obrazovkách (pod 1500 pixelů, například 1366 × 768) hodiny nahoře ukazují pouze hodinu, takže téma, kurzor textu a jazyk zůstávají viditelné. Jazyky: YA-MS [17] wiki (Angličtina, Cestování, Deutsch, Nederlands, Français, Español, Português, Dansk, Norsk, Svenska, Íslenska, Eesti, Lietuvia, Latviešu, Руский,, кра, нська, Türkçe, România S arabskými, hebrejskými a Urdu celé okno zrcadla zprava doleva; mapa zůstává jako [12]. Překlady se týkají rozhraní; dva technické dokumenty a zprávy stavové lišty zůstávají v italštině. Jména dnů pocházejí z mezinárodního nastavení. NET pro zvolený jazyk.

Téma a jazyková změna od dvou menu vpravo nahoře, vedle hodin, a použít okamžitě, bez restartování programu a bez přerušení shromažďování dat. Volba je uložena v LIVETROPOVHF.ini.

## 11 bis. Zprávy o podezření a sebeučení

Záložka Podezření Sbírá vše, co program ovládá najít podivné, seskupeno podle typu a jména, s posledních 24 hodin v seznamu. Program se učí sám: pro každý pár (typ, název) počítá pozitivní výsledky (+) a naopak (−) a po 3 dohodnutých výsledcích vytváří automatické pravidlo; opačný výsledek jej zpochybňuje. Uživatelská rozhodnutí (Good, Poor) vždy platí; tlačítko pulsante vrací rozhodnutí do programu. Pravidla jsou v Data / APPRENDIMENTO. TXT, hned vedle programu; ty automatické, které nejsou posíleny na rok, jsou zapomenuty.

| Typ | Kdy | Výstup + / − | Článek 11 |
| EME ( Q65/JT65) | Q65 nebo JT65 nad 1000 km s Měsícem nad horizontem pro obě hlavy, nebo EME v komentáři | − pokaždé, když je Měsíc obyčejný | Zlikvidujte: EME stanice, její dlouhé Q65/JT65 jsou také vyloučeny bez běžného měsíce. Dobře: zacházeno jako tropo |
| Dlouhé připojení | Přes 1000 km, v karanténě | + odhaleno, − vypršelo bez potvrzení | Dobrý: délka stanice ukazují okamžitě. Zlikvidujte: nikdy |
| Poslouchat | PSKReporter přijímač, který slyší aktivní stanice na jiných pásmech | − vyřazen, + regenerován lidskými klastry | Dobře: nikdy se nezbavíš. Zlikvidujte: vždy |
| Jiný domácí | Náměstí se liší od C ALL3. TXT | + pokaždé, když to zopakujete | Dobrý: stanice se pohnula, stojí za to náměstí obdržel. Vyhodit: vale C ALL3. TXT |
| Náměstí | 6- znak s LL nebo MM |  —  | Pouze informace |
| Automatický přijímač | Jmenování s -SDR, / SDR, - # nebo -n |  —  | Pouze informace |

EME. Q65 a JT65 jsou způsoby komunikace přes Lunu: Teoreticky spadají do ustanovení o vyloučení jako meteorit Scatter, ale používají se také v tropu. Program počítá polohu Měsíce s nízkou přesností vzorců almanacké astronomické (cca 0.3 °) [20]: pokud spojení v Q65 nebo JT65 překročí 1000 km a Měsíc je nad horizontem obou hlav, je téměř jistě EME a je vyloučen (počítá se mezi vyloučené). Pokud Měsíc není běžný, spojení zůstává možné v tropu a prochází karanténou dlouhých spojení, ale signál zůstává viditelný. Vztahuje se na klastry pro MQTT a jeho vlastní dekódování UDP.

## 11 ter. Kritéria pro kontaktní výmět, jeden po druhém

Program chce ukázat pouze tropoferické šíření ve 144 MHz a dále. Každá příchozí data (Cluster Row, PSKReporter Report přes MQTT, UDP dekódování) prochází řadou filtrů, vždy ve stejném pořadí. Vyřazená data nezmizí v ničem: započítává se (kontakt vyloučeného v horním panelu) a, pokud je důvod pochybný, je uveden v záložce Suspended Reports.

|  N.  | Kritéria | Kde je? | Co bylo zrušeno | Můžeš se převléct? |
|  1  | Pásmo | cluster, MQTT, UDP | Všechny do 143 MHz (také 50 a 70 MHz), kromě GRAVŮ 143,050 MHz. Držené pásy jsou 144, 222, 432, 1296, 2320 MHz a 10 GHz | Ne. |
|  2  | Satelitní QO- 100 | cluster, MQTT, UDP | 2400,0-2400,5 MHz a 10489,5-10490 MHz: je to geostacionární satelitní provoz, ne tropo | Ne. |
|  3  | Meteor Scatter | cluster, MQTT, UDP | JT6M a ISCAT (také ISCATA / B), komentáře s nápisem "&" a netropoxidovou množitelskou značkou (< AU >, < AUE >, < ES >, < FAI >, < F2 >) vždy. MSK144, FSK441 a JTMS (také s < MS >) pouze přes 250 km: do 250 km přijímáme, protože v této vzdálenosti se tyto režimy používají také v tropo | Ne. |
|  4  | EME (via Luna) | cluster, MQTT, UDP | EME napsaný v komentáři; Q65 nebo JT65 nad 1000 km, když je Měsíc nad horizontem pro obě hlavy; stanice se naučil jako EME | Ano: Dobré pravidlo pro podezřelé signály |
|  5  | Non-Genuine Liener | MQ | PSKReporter přijímač, který říká slyšet na 144 stanic aktivní v té době na jiných pásmech (nejméně dva): Jeho vztahy jsou zrušeny. Zpět dobrý, pokud DX lidské klastry potvrzují stejné stanice | Ano: Dobré / chudé |
|  6  | Kabina dlouhých spojů | všechny řádky | Více než 1000 km je skrytý odkaz, dokud nenajde potvrzení: stejná stanice ve stejném směru, další trasa v koridoru 200 km, nebo paralelní trasa v rozmezí 10 ° a 200 km podél alespoň poloviny. Bez potvrzení vyprší po 60 minutách | Ano: Dobrý / Off na stanici |
|  7  | Pod 100 km | řádky a seznam | S on- box, nejkratší spojení 100 km (pozemní vlna, optický rozsah) | Krabice, vypnuto při startu |
|  8  | Za korunou z QTH | čáry | Pokud je stanice více než 1000, 700 nebo 500 km (dle Vašeho výběru) od vašeho QTH, žádná linka: pouze lilla čtverců pokrytých dvěma stanicemi. Vaše QTH náměstí nikdy barvy | Box a poloměr, na 1000 km |
|  9  | Časové okno | všechno | Starší skvrny zvoleného rozsahu (od 3 do 60 minut) | Nabídka nahoře |
|  10  | Dvojité | cluster | Stejné místo (stejný DX, stejný pozorovatel, stejné pásmo, stejný zdroj) se opakovalo | Ne. |

Poznámka k kritériím 4 a 6. Spojení v Q65 nebo JT65 nad 1000 km bez Common Moon je možné v tropu: není vyřazeno podle kritéria 4, ale projde karanténou kritéria 6 jako jakýkoli jiný dlouhý odkaz. Přímé hovory na vás (oddíl 9 kvater) jsou jedinou výjimkou z kritérií 7 a 8: vždy kreslit.

Co není plýtvání. Opravy lokátoru (C ALL3. TXT s ručními hlasy, opravy z clusteru a naučil lokátory, Suxixy -SDR, centrum náměstí LL / MM) změnit, kde si nakreslit stanici, ne ji vyhodit. Stanice bez žádného domácího, na druhou stranu, nemůže být čerpána: zůstává v seznamu s vzdáleností "-".

## 11 kvater. Barvy a symboly

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

## 12. Nastavení a přenositelnost

Okno. Na začátku, okno zabírá90% užitečného prostoru obrazovky (snížit aplikační lištu), soustředěný, tak na 15-16palcový notebook s125% nebo150% předimenzovat titulní lišta zůstává viditelná. Pokud je užitečný prostor pod1280 ×720 bodů, část okna se maximalizovala. Velikost, poloha a maximální stav jsou uloženy v LIVETROPOVHF. ini při zavírání a konci při příštím startu, pokud titulní lišta stále padá do obrazovky (např. po odpojení externího monitoru); jinak se vrátíte na90%. Minimální velikost je900 ×560.

Program je přenosný: nikdy nezapisuje do složky AppData nebo do registru. Vše je ve složce spustitelného: LIVETROPOVHF.ini (sety, vytvořené při prvním uzavření), C ALL3.TXT (s ručně psanými hlasy, naučenými korekcemi a lokátory), mapovací cache a data o počasí, naučená pravidla (Dati / APPRENDIMENTO.TXT) a LIVETROPOVHF.db archiv. Můžete si ji také nechat na USB tyči, pokud je složka zapisovatelná.

Archiv reklamy (SQLite [14]). Každé akceptované místo a každá zpráva z PSKReporter je uložena v databázi LIVETROPOVHF.db, SQLite v jednom souboru. V paměti zbývá pouze posledních 60 minut pro seznam a mapu; archiv si uchovává vše a slouží pro historika a statistiku (také dekódování UDP), například otvory pro pásmo a za den, maximální vzdálenosti, čtverce dosaženo, a zadní srovnání mezi očekávaným indexem a reálných míst. Řádky vyřazené z filtru se počítají, ale nearchivují.

## 12 bis. Automatická instalace a aktualizace

Z verze 1.0 program již stahuje ze stránky Release github.com / ic8tem / LIVETROPOVHF, ve stejných balíčcích YA-MS: instalátor pro Windows (64 bitů, 32 bitů a ARM, instalace v C:\ LIVETROPOVHF bez práv administrátora) a přenosný zip; pro Windows 7 SP1 a 8.1 balíčky od sebe (-windows7-x64 a -wind.deb,.ot. / min, Arch, Slackware, AppImage a tar.gz pro Linux; balíčky pro Raspberry Pi (64 a 32 bitů) a Alpine;.dmg a appss.zip pro Mac (Apple Silicon a Intel). S Linuxovými balíčky je program zkopírován na první start v # / LIVETROPOVHF, takže nastavení a data zůstávají vedle programu; na Mac se data rodí vedle LIVETROPOVHF.app.

Každých 30 minut (první kontrola několik sekund po startu, okamžitě, pokud byl program naposledy uzavřen pro chybu) program čte sám, aniž by cokoli žádal, poslední vydání. Pokud je verze novější, stahujte přenosný balíček vhodný pro systém a nahrazujte na místě soubory programu: ty, které používáte, se přejmenují *.old- update a vymažou při příštím restartu. Nastavení, archiv, složka dat a C ALL3. TXT se nedotknou. Nová verze je aktivní od příštího startu: při instalaci dokončeno okno navrhuje restartovat okamžitě; pro naléhavé opravy označené [povinné] ve verzi Release nelze restartovat. Kontrolní stav a použitá verze jsou napsány v dolní části stavové lišty; pro vypnutí aktualizací = 0 v LIVETROPOVHF.ini. Na Mac je aplikace podepsána a nemění se: program pouze hlásí, že existuje nová verze.

Na začátku se objeví obrázek LIVETROPOVHF s číslem verze; na konci programu se žádá o potvrzení a díky za použití.

## 12 ter. Chyba log a stavová lišta

Jakákoli nepředvídaná chyba končí v Data / errori.log, a to i s vypnutým výpočtem: datum a čas, verze, systém, otevřená karta, kompletní chyba a poslední řádky toho, co program dělal. Chyba rozhraní již program neuzavírá: ve stavovém řádku se objeví "anno annotated error" (klikněte na tlačítko: otevřete registr). Na dalším startu se program zeptá, zda má zprávu odeslat: otevře prohlížeč na již sestaveném signálu GitHub, s nominací, lokátorem a uživatelským jménem odstraněného PC, které zkontrolujete a odešlete pomocí volného účtu GitHub. Záznamy také končí každou kontrolou aktualizace.

Ve stavové liště jsou zdrojové koule (DX cluster, RBN, PSKReporter, MQTT, UDP) kliknuty a přineseny ke zdroji; komiks říká, proč je zdroj vypnutý: odmítnutý port, cluster nód na straně serveru, žádná odpověď, nebo UDP poslech, ale žádné balíky z WSJT-X, JTDX nebo MSHV. Jedno kliknutí na číslo verze okamžitě zkontrolujte aktualizace.

## 13. Platformy

Externí knihovny: Avalonia pro [12] rozhraní, MQTTnet jako MQTT klient pro real-time proud PSKReporter [13], Microsoft. Data.Sqlite pro archiv [14].

C # on.NET 10 (LTS) [15] s Avalonia rozhraním, publikováno jako nezávislý spustitelný pro Windows 64 a 32 bitů, Mac Intel a Apple Silicon, Linux Debian a Raspberry Pi (64 a 32 bitů). Zaznamenané limity: Windows 10 / 11; macos 14 nebo novější, s aplikacemi k podpisu nebo k otevření vpravo → Otevřít; Malina z Pi 2 / 0 2 W nahoru (ARMv6 modely nejsou podporovány.NET).

## Děkuji.
Díky I5MZY Massimo Zannoni pro testování na Windows7 a Windows8.

Letouny: ITU-R P.453, digitální produkty studijní skupiny 3 (© ITU), používané pro osobní i nekomerční účely.

## Hlavní zdroje

1. S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", prezentace na 41. severském VUSHF setkání, Skjeberg 2019. Troposférické režimy, kontrolní seznam událostí (předpověď Hepburn / F5LEN, vysokoteplotní profily s Windy, DX cluster jako důkaz otevření), pozorování, že pouze předpovědi mapy nestačí pro extrémní trasy.
2. J. N. Gannaway G3YGF, "Troposférická propagace Scatter", QST Listopad 1983 (z eponymního článku o Radio Communication, RSGB, srpen 1981). Formuly ztrát a difuze volného prostoru, odpovídající poloměr 8497 km, doba trvání lokality 10 dB za horizont, klima 0.2 · (N − 310), kapacita zařízení a poměr signál / hluk, například při 144 MHz.
3. W. R. Hepburn, DX Info Centre, "Troposféric Ducting Forecure" (dxinfocentre.com / tropo.html) a "The Hepburn Tropo Index (HTI)". Indexová stupnice a hodnota význam, předpověď na 6 dní.
4. F5LEN, předpověď pro Evropu (tropo.f5len.org / contasts-for-European: seznam obrázků ve skriptu stránky, třídní legenda Disturb... Dukt v obraze) a světové předpovědi (tropo.f5len.org / WW) a poznámka "Nová verze celosvětového nástroje VHF troposférické předpovědi šíření". Index založený na reaktivitě v jednotce N, 40 kroků od 3 hodin, aktualizace 4 krát denně.
5. mqt.pskreporter.info, distribuce MQTT v reálném čase reklam na pskreporter.info (kurátorem M0LTE, tok P. Gladstone použitý se svolením). Dveře 1883-1886, pskr / filter / v2 tematické schéma.
6. Souhrn projektu 8 / 11 / 2026 a Go test (core.go, gui.go) dodaný autorem programu. Pravidla reklamy, negativní filtr, segmenty šířky pásma, řízení telenetu, témata, jazyky a přenositelnost.
7. C ALL3. TXT, databáze nominací → JTDX domácí (I. UA3DJY, A. ES1JA script, JTAlert / hamspots.net data). Formát souboru a počet jmen ověřených v souboru projektu.
8. WSJT-X, zdrojový kód, NetworkMessage.hpp soubor (popis protokolu UDP: Heartbeat, Status, Decode). Obecný odkaz, není přiložen; JTDX a MSHV používají stejný formát.
9. Reverse Beacon Network (reversebeacon.net), servisní stránky telnetového nódu. Přístavy 7000 (CW, RTTY) a 7001 ( FT8), přihlášení pouze se jménem.
10. D. Mills, RFC 4330 "Simple Network Time Protocol (SNTP) Version 4", IETF 2006. Formát balení, výpočet odpadu a zpoždění návratu. Obecný odkaz, není přiložen.
11. NTP Pool Project (ntppool.org); INRIM, NTP time service (ntp1 / ntp2.inrim.it); PTB (ptbtime1.ptb.de). Servery používané pro kontrolu času. Obecný odkaz, není přiložen.
12. Avalonia UI, úřední dokumentace (docs.avaloniaui.net). Multiplatformní rozhraní, témata a rozložení zprava doleva. Obecný odkaz, není přiložen.
13. MQTTnet,.NET knihovna pro MQTT (github.com / dotnet / MQTTnet). Klient používaný pro tok PSKReporter. Obecný odkaz, není přiložen.
14. SQLite (sqlite.org) a Microsoft. Data.Sqlite (learn.microsoft.com). Archiv v jednom souboru. Obecný odkaz, není přiložen.
15. Microsoft,.NET 10 - podporované operační systémy a autonomní publikování v jednom souboru (learn.microsoft.com / dotnet). Platformy a limity uvedené v oddíle 13. Obecný odkaz, není přiložen.
16. ECMA-376 "Office Open XML File Formats", část 1, oddíl 22.1 (Office Math Markup Language). Struktura.docx souboru a rovnic. Obecný odkaz, není přiložen.
17. Seznam 28 jazyků YA-MS wiki, poskytnutých autorem programu.
18. Open-Meteo, předpověď počasí API (open-meteo.com), s daty z modelu NOAA GFS. Tlak na úrovni moře pro isobare (viz dokument 2).
19. J. P. Snyder, "Map Projections - A Working Manual", USGS Professional Paper 1395, 1987 a standardní polynomové transformační metody pro geoodkazování obrázků. Obecný odkaz, není přiložen.
20. Astronomický almanach, sekce D, "Nízká přesnost vzorců pro Měsíc" (přesná poloha cca 0,3 °). Uznával EME spojení. Obecný odkaz, není přiložen.
