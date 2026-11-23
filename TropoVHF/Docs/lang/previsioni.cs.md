# Předpověď

> Strojový překlad z italštiny: v případě pochybností platí italská příručka.

Na kartě Prognóza počítá tropoferické šíření samo o sobě: indexová mapa na viditelné ploše a signál mezi vaší stanicí a odpovídajícím. Tato příručka popisuje všechna data, jak se vypočítávají a odkud pocházejí. Čísla v hranatých závorkách odkazují na zdroje v dolní části; v programu každé údaje z výsledků mají komiks (simbolo i) se stejným zdrojem a odkazem na odstavec.

## 1. Co dělá a co dělá

Předpověď svislých teplotních a vlhkých profilů modelu GFS [1, 2] odvozuje reaktivitu vrstvy vzduchu po vrstvě a odhaduje, kolik signálu v porovnání s normální atmosférou získá. Na trase se přidává reálná půda (Copernicus GLO-30 [3]), difrakce, troposcatter, kanály a přijímací hluk.

Není to opatření: je to odhad na modelu počasí se vzdálenými úrovněmi stovek metrů. Slouží k porovnání situací (dnes lepší než včera, o 70 cm lepší než 2 m, tato úprava lepší než to), nezaručuje spojení. Odstavec 14 vysvětluje, jak shromáždit důkazy, které by je potvrdily.


> "Zákaz nelze zobecnit, ale studovat ve vztahu k místní morfologii."
> - Volker Grassmann, DF5AI, reported by Flavio Egano, IK3XTV (propagace na 2 metry)

Je to princip, který řídí výpočet bodu: každá cesta se počítá na své půdě, s horizonty, jeho moře a jeho reliéfy; mapa dává obecný obraz, zachází s odpovědí.

## 2. Jazykové příkazy

- Pásmo: 144, 432 nebo 1296 MHz. Mapa a výpočet použijte zvolené pásmo.
- Kurzor posune předpověď od teď do 72 hodin dopředu, do 3 hodin.
- Aktualizace předpovědi (viditelná zóna): stáhnout profily GFS na mřížce nejvýše 240 bodů pokrývající viditelnou mapu. Rozteč mřížky (0,5 ° nebo více) závisí na zvětšení.
- Stop: přerušení stahování nebo výpočtu.
- Podrobnosti pro zobrazení (pravý panel mapy): tři úrovně, Essential, Normal a Complete, zapněte skupinu krabic; každý box se pak může ručně změnit. Krabice jsou ve třech skupinách:
  - Na mapě: isolinee del Gain, také 3-7 dB (vlečení), isobare, tepelná inverze, povětrnostní známky S ↓ ↑, korelace zisk / tlak, zóna přímého pohledu (odstavce 4, 5, 7 a 9 bis). Barvy vždycky zůstanou.
  - Při výpočtu signálu: všechny řádky výpočtu, plochý zemský profil, záznam výpočtu a podrobný záznam souboru.
  - Opravy a živá data: opravy počasí (Misme, subsidence), kontrast vzduchu-moře, průměr s místy (corridoi), APRS 2 m skoky a LoRa, přes horu bez ztrát (odstavce 4 bis, 9 bis, 9 quater a 10 bis).
  - Základní drží pouze izolanty, počasí a opravy médií s místy; Normal přidává isobare, inverze, povětrnostní známky, přímý pohled, ploché země, air- sea a APRS; Kompletní zapne. Úroveň mění jen to, co vidíte a co opravy zadáte: výpočty zůstávají stejné.
- Zdroj dat a historického případu: odkud pocházejí profily a který den hledáte (odstavce 3 a 3 ter).
- Moje stanice a korespondent: Lokátor, antény, napájení, kabely, hluk (odstavce 7 a 12).
- Vypočítejte signál a pokrytí v optice: dva výpočty na zemi (odstavce 8-13).
- Registr výpočtů: každý krok s použitými čísly, včetně souboru Data / contastast- calcoli.log.

## 3. Od GFS dat k profilu

Pro každý bod mřížky a každou hodinu čteme tlak na zemi, teplotu a vlhkost na 2 m, vítr na 10 m, výšku mezní vrstvy, CAPE a teplotu a vlhkost v poměru k 1000, 975, 950, 925, 900, 850, 800, 700, 600 a 500 hPa. Od 1.3.0 můžete také číst vítr na 850 hPa a vertikální rychlost na 850 a 700 hPa (odstavec 9 bis).

Úrovně se počítají s ipsometrickým vzorcem a virtuální teplotou; úrovně pod zemí jsou vzácné. Pro každou úroveň:

- N = 77,6 / T · (P + 4810 · e / T), s T v kelvinu, P a hPa a saturačním tlakem ITU-R P.453-14, eq. 9, s korekčním faktorem EF (recontrolován na úředním textu) [4].
- M = N + 157 · h, s h v km: modifikovaná reaktivita. Tam, kam M jde, je vedení.

## 3 bis. Zdroj dat

Menu Data Source si vybere, odkud profily pocházejí:

- Automatické (doporučené): GFS první z NOMADS [14], NOAA služba, která distribuuje model: za každý okamžik program žádá dva výřezy oblasti ve formátu G RIB2 a čte je ze sebe; NOMADS nemá denní limit, pouze jeden za minutu, že program respektuje. Se širokými mřížkami (od 0,5 ° nahoru) použijte soubory na 0,5 ° nebo 1st každé 3 hodiny, mnohem lehčí. Body, které NOMADS nedává, nebo všechny, pokud NOMADS neodpovídá, zeptejte se Open- Meteo [2]. Oba zdroje dávají stejný model a stejný závod, takže můžete míchat ve stejné síti.
- GFS od Open- Meteo: pouze Open- Meteo, jako v 1.2 (na limit za minutu se očekává).
- GFS od NOAA NOMADS: pouze NOMADS. Pomaleji ke stažení (dva soubory za hodinu, půl sekundy mezi jedním požadavkem a druhým ne překročit limit NOAA), ale nespotřebovává limit Open- Meteo.
- ICON- EU (DWD) a ECMWF IFS 0,25 °, od Open- Meteo: modely jiné než GFS, které platí po celé síti a pro trasy. Nemíchají se s GFS: srovnání dvou modelů ve stejnou dobu je jednoduchý způsob, jak zjistit, jak je předpověď nejistá. ICON- EU pokrývá pouze Evropu; ECMWF nemá všechny tlakové úrovně GFS (chybějící úrovně skoku) a nemusí mít mezní vrstvu.

Použitý zdroj je zapsán do stavové řádky a do první řady výsledků (Zdroj dat), s kolika body přišel z každé služby a kolika z cache. Disková cache je oddělena modelem.

## 3 ter. Historie případů

Historické Case menu recenzí dokumentované otevření s archívem Open- Meteo [15] GFS prognózy: mapa se pohybuje do oblasti případu, kapela nastavuje sám a čas kurzor odchází od prvního dne případu. Stisknutím aktualizace prognózy stáhnete profily těchto dnů; výpočet signálu funguje jako pro aktuální předpověď. Slouží k porovnání modelu s tím, co se opravdu stalo.

První případ je z 14-15 srpna 2022 řekl v QST (listopad 2022, "Svět nad 50 MHz" [23]): otevření pod vysokým tlakem mezi Manitoba a Kansas / Missouri, od 144 do 1296 MHz téměř vše v FT8, s kanadským záznamu na 10 GHz 1228,56 km v Q65 a signály až 1941 km. Druhým je otevření mezi 29. prosincem 2021 a 2. lednem 2022 na Tyrhenian a západní Středomoří na 144 MHz, ze zpráv IC8TEM: první otevření na 29 (až 737 km), širší na 31 ( IT9GSF - EB5EA 1217 km), velký otevření dne 1. ledna mezi Itálií, Sicílií a Španělskem. Pro starší případy (např. září 2000) nemá archív Open- Meteo žádná data. V archivu chybí výška limitní vrstvy: jeho korekce neplatí.

## 4. Index a barvy

Pro každou vrstvu mezi dvěma úrovněmi program srovnává N pokles s prahy tabulky 6.5 Bean a Dutton [5]: například mezi 1000 a 850 hPa pokles až 60 N / km je normální, přes 100 N / km je superrefraction. Nadrefrakční vrstva má hodnotu 10 až 15 dB zisku; vedení, které zachycuje pásmo od 20 do 25 dB, podle deficitu M. Silného větru k zemi, vysoká mezní vrstva a vysoká CAPE snižují zisk, protože odstraňují nebo lámou vrstvy [5, § 4.4].

Barva je odhadovaný zisk v dB, s pevnou stupnicí rovnající se ve všech tématech:

| Zisk | Barva | Význam |
| Pod 7 dB | Nikdo. | téměř normální atmosféra |
| 7- 10 dB | modrá | gradient nad normální |
| 10- 13 dB | zelený | superrefrakční vrstva |
| 13- 16 dB | žlutá | silná vrstva, ale bez pásů |
| 16- 18,5 dB | oranžová | velmi silná vrstva nebo příliš tenké potrubí |
| 18,5- 21 dB | červená | Conduit Trapping Band |
| od 21 dB | Fialová | Silné a hluboké potrubí |

S displejem také 3-7 dB zóny mezi 3 a 7 dB, kde se něco tvoří, ale zůstává pod měřítkem, můžete vidět s lehkou šedou zábradlí; barvy se nemění. Teklá tabule vymezuje oblast pokrytou vlastní prognózou a ukazuje maximální viditelný zisk: pokud je pod 7 dB, mapa zůstává bez barev, protože atmosféra je téměř normální, ne proto, že údaje chybí. Při stahování nového kliknutí na Aktualizační předpověď nepřerušuje, pro zastavení je zde Stop.

Barvy jsou neprůhledné na 82%, takže pozadí tématu je jen mění. Lokátor mřížky a čtvercové názvy jsou navrženy přes barvy. Kde vaše předpověď není tam, ale Hepburn nebo F5LEN dát 5 nebo více šachových karet objeví s barvou tohoto zdroje.

Mapa soudí každý bod sám. Výpočet signálu místo toho řídí, že vrstva je kontinuální podél trasy (odstavec 9): cesta, která prochází velmi žlutou může dát méně, než mapa si myslíte.

## 4 bis. Předpověď zprostředkovaná reklamou: chodby (od 1.3.3)

Copyright Policy: Samotné spojení nesmí nikdy čerpat potrubí. Izolovaný a krátký příjem může být rozptyl průchozí roviny (balíček APRS je velmi krátký), otevření několika minut nebo chybu, a by dát "drop- chování" klamou. Program rozpozná chodbu, když ve stejné skupině:

- nejméně 3 páry nezávislých stanic (bez společné stanice) pracují na trasách o délce nejméně 300 km;
- tratě jsou rovnoběžné: azimut v rozmezí ± 15 °, průměrné body v rozmezí 150 km od společné osy;
- všechny za posledních 30 minut, přičemž se paprsek opakuje ve vzdálenosti nejméně 10 minut (perzistence);
- pro ARS se skok počítá pouze v případě, že se stejný skok opakuje;
- od 1.3.4 se trasy nad 1500 km počítají pouze tehdy, pokud paprsek pochází alespoň ze dvou různých zdrojů (např. z PSKReporter a cluster): z jednoho zdroje na vzdálenost 2 m jsou častěji EME, rozptyl meteoru nebo nesprávné lokátory než tropo.
- od 1.3.4, nepočítejte stanice "do paprsku": kdo v poslední hodině má alespoň 5 korespondentů nad 1000 km rozložen na ventilátor nejméně 150 °. Ve 2 m je trope směrový; přijímač, který "volný" společně Izrael, Portugalsko, Anglie a Itálie od 2000-3700 km, často s pozitivní signály, má téměř jistě špatně nastavenou frekvenci. In the 17-22 / 11 / 2026 I5MZY archiv 72% ubytování ve 2 m nad 1000 km pochází ze stanic, takže 22 / 11 v 10: 53 UTC byly OH6HLH, OZ1ITS, F4LUK, OM8ABC a SP2JLR, a s filtrem nejdelší koridor sestupuje z 2175 km (regole 1.3.3) na 615 km.

Zdroje: cluster spoty (ne ty označené AS / ACS), PSKReporter přes MQTT, jejich QSO a WSJT-X / JTDX / MSHV dekodings, a APRS skoky v 2 m a LoRa 433 / 868 MHz.

2 m a LoRa. Program čte APRS-IS pouze v read- only (rotate.aprs2.net, filtr na Evropě) a extrahuje rádiové skoky mezi známými polohovými stanicemi (zdroj → digipeater → Igate), jako je VHF mapa DXView a APRS PropView (RF-YVY, Apache 2.0). Tři pásma mají hodnotu: 2 m, LoRa 433 MHz (koridory pásma 432) a LoRa 868 MHz (corridoi, které se počítají pro 1296: vedení, které přináší 868 MHz také nejvyšší frekvence). Kapela pochází z LoRa tocall (aprs- deviceid list) a z frekvencí nebo slov v komentáři; skok má hodnotu pouze mezi dvěma stanicemi stejné kapely.

Jemným bodem je aktivní opakovače: signál přijatý na 500 km může být doručen se dvěma nebo třemi přenosy digipeater. Skok se tedy počítá pouze v případě, že je mezi dvěma skutečnými a po sobě jdoucích vysílačů:

- cesta musí být vysledována: každý digipeater, který byl předán, dal své jméno a skok se měří mezi jedním a druhým; alias použitý bez názvu (W IDE1 *, W IDE2 *, regionální alias) přeruší řetězec a tento úsek se nepočítá;
- skryté přenosy: program si pamatuje cestu, kterou se každý zdroj obvykle ptá (např. W IDE2 -2). Pokud balíček přichází se skokem v méně (W IDE2 -1) a žádné jméno to vysvětluje, digipeater přeposlal bez sledování a balení je zlikvidován; zdroje nikdy neviděl se nepočítají;
- digipeater uprostřed silnice: je-li mezi dvěma stanicemi, víceméně na silnici, je digipeater stejné aktivní pásmo v posledních třech hodinách, skok je vyřazen, protože by mohl být znovu přenášen (to je zejména pro digipeater LoRa, který předává bez změny cesty);
- non-radio balíčky (qAC, qAX, TCPIP), ty provozované na internetu z rádia (třetí strany), HF a různé frekvence, stanice v nadmořské výšce více než 1500 m (palloni, letadlo) a skoky přes 1500 km.

Účinek na mapu (od 1.3.4). Vytahuje pouze oblast, kde platí minimální záruka: pásmo ± 60 km kolem střední části každé trasy (od 15% do 85% její délky), nikoli na stanicích na koncích, kde potrubí nemusí být. Pásy všech koridorů jsou taženy dohromady, v neutrální barvě (text) a poloprůhledné: překrývání se nepřidává a barva není zaměňována s fialovou stupnice (21 dB a dále). Štítky (rysy, průměrná vzdálenost, minimální zisk) se navzájem skenují; s myší na štítku se objevují části chodby.

V oblasti a pouze v blízkosti aktuálního času (± 90 minut), předpověď nespadá pod minimum. Důvodem je, že pokud stanice opravdu projdou, je tam propagace, ať už říká model počasí cokoliv.

Protože krátké trasy mají malou hodnotu (od 1.3.4). V 2 m, mezi 300 a 600 km, dvě dobré stanice (asi sto wattů, antény, FT8 nebo CW) připojit téměř každý den i s normální atmosférou, pro troposcatter: signál rozptýlený malými nepravidelností vzduchu ve společném objemu obou svazků. Tři nebo čtyři místa na 500 km říkají, že kapela funguje jako obvykle, ne že by tam byl otvor: GEKO Magazine n. 69 ( IU3QEZ) odhaduje pro troposcatter s 2 × 10 proudícími prvky přibližně 440 km v SSB QRP, 510 km v SSB s 50 W, 600 km v CW QRP a 640 km v SSB s 500 W (zdroje, jedenáct kolo). Pouze přes 600- 700 km troposcatter již nestačí a řada paralelních spojení skutečně naznačuje vedení nebo vrstvu. Až 1.3.3 koridor 450- 600 km zvýšil předpověď na 9-12 dB a mapa ukázala zlepšení, které tam nebylo.

Počáteční hodnota pak jde s průměrnou vzdáleností tras:

- až 400 km: 3 dB;
- od 400 do 700 km: sůl lineárně od 3 do 10 dB (500 km → 5,3 dB; 600 km → 7,7 dB);
- od 700 do 900 km: od 10 do 16 dB; přes 900 km zůstává na 16 dB.

Pod 7 dB, to je pod počátkem stupnice, minimální zůstává, že: koridor je nakreslena s jeho štítkem, je vidět, že páska funguje, ale barvy mapy se nemění (s rámečkem také ukazuje 3-7 dB šedé tečkované). nad 7 dB se hodnota zváží, polovina a polovina, s počtem nezávislých tras (3 tahy → 0,25; 6 nebo více → 1) a s mediánem S / N vztahů (− 20 dB → 0.5; 0 dB nebo více → 1; není známo, jako ve skupinových spotech, → 0,75), počínaje 7 dB. Příklady: 3 trasy bez S / N na 500 km → 5,3 dB; 5 tras na 615 km → 7,7 dB; 3 trasy bez S / N na 900 km → 11.5 dB; 3 trasy na -20 dB na 900 km → 10,4 dB; 6 tras na 0 dB na 900 km → 16 dB.

Minimální vzdálenost zůstává 300 km: zvýšení na 500 km by také odstranilo skutečné krátké otvory (například vedení na Tyrhenian na 400 km, který je uznán předpovědí počasí a velmi silné signály více než jen ze vzdálenosti). V archívu I5MZY 22 / 11 / 2026 v 10: 53 UTC, s tímto pravidlem a paprskovým filtrem byly nejdelší chodby 615 km (7,7 dB) a 540 km (6,3 dB); s pravidly 1.3.3 dosáhla 2175 km a 16 dB.

Až do 1.3.3 platilo minimum na všech obdélnících koridoru a záviselo pouze na vzdálenosti (od 10 dB do 300 km do 16 dB do 900 km): s mnoha dlouhými koridory dohromady (např. 22 / 11 / 2026, dvanáct koridorů až do 2620 km) se zvedla téměř celá Evropa na 16 dB.

Křivky zisku již nekreslí "gokce": menší ostrovy 220 km v obou směrech zůstávají barevné, ale bez uzavřené křivky, ledaže koridor potvrzuje. Vypíná se s "Předpovědí zprostředkovanou spoty".

## 5. Volitelné čáry a srovnání s tlakem a inverzí

Křivky zisku: čáry na 7, 10, 13, 16, 18,5 a 21 dB, hrany stupnice, tažené na poli hladké s metodou pochodových čtverců, s štítkem v dB.

Isobare: tlak na úrovni moře každé 4 od stejné GFS, až do hodiny předpovědi, s centry H a L.

Tepelná inverze: křivky pokryté tím, kolik vzduchu je při 925 nebo 850 hPa teplejší než při 2 m, při + 1, + 3, + 5 a + 8 ° C.

Pod kontrolami čára dává zisk souhlas s tlakem a s opakem na viditelné zóně: Pearson korelační koeficient, − 1 až + 1. Kanály se často rodí v anticyklonech (vzduch, který klesá ohřívá a vytváří opak), takže pozitivní dohoda s tlakem je normální, ale uvnitř stejného anticyklonu zisk mění hodně mezi mořem, pobřežím a horami. Pouze inverze nestačí: ohýbat vlny pouze v případě, že nad vzduchem je dokonce sušička [5]. Linie slouží k tomu, aby nyní, který ze dvou nejlepších vysvětluje barevné oblasti. Stejné srovnání je uvedeno v rejstříku.

## 6. Potenciální potrubí: dvojí výpočet

Hladiny GFS jsou od 200 do 1000 m: tenká inverze se šíří a model nevidí potrubí. Pokud pokles N mezi dvěma úrovněmi překročí normální hodnotu (40 N / km), je vzduch stabilní (teplotní gradient pod 3 K / km) a vyšší je suchý (specifický poměr vlhkosti pod 0,8), program zvažuje potenciální kanál a počítá jej dvěma způsoby:

- Výpočet A: všechny koncentrované nadbytečné pokles v 50 m.
- Výpočet B: stejný pokles rozdělený na typickou tloušťku potrubí této oblasti, převzatý z klimatických map ITU-R P.453 [4] (na zemi nebo zvýšený).

Pro deficit a pro maximální zachycenou vlnovou délku platí nejnižší hodnota: je to obezřetný odhad.

ITU- R P.453 data jsou © ITU a mohou být použita pro osobní i nekomerční účely, cituje zdroj, ale nelze je přerozdělit: proto nejsem v balíčku programu. Pro zapnutí výpočtu B stačí stáhnout mapy P.453 z ITU v záložce: program po zobrazení podmínek použití, stáhnout archiv potrubí z ITU stránky (upgrade _ and _ surface _ duct.zip) pro osobní použití uživatele, extrahovat soubory v Data / P453 a zapnout výpočet bez restartování. Alternativně si stáhněte z digitální produktové stránky Studijní skupiny 3 ITU-R (itu.int, ITU-R Studijní skupina 3, Digitální produkty, P.453, Povrchová data a zvýšená data kanálů) a umístěte sedm S _ OCCUR. TXT, S _ THICK.TXT, S _ STREN. TXT, E _ OCCUR.TXT, E _ base.T Bez souborů program používá pouze výpočet A a zapisuje jej v záložce a v registru. Poměr mezi deficitem A a typickou intenzitou P.453 je anomálie: Kolikrát je dnešní ropovod silnější než obvykle. P453

## 7. Stanice a lokátory

Domácí může mít 4, 6, 8 nebo 10 znaků. S 8 nebo 10 znaky pozice je přesná a podíl půdy pochází z Copernicus GLO-30: výška antény je dost ze země. S 4 nebo 6 znaky náměstí je příliš velký: je lepší napsat kvótu antény na moři; pokud 0 program zůstává odhad s půdou SRTM v centru náměstí plus výška ze země a signalizuje to ve výsledcích. Pro odpovídající je výchozí výška 3 m od země; s mořským podílem 0 se používá půda SRTM plus výška.

## 8. Půda, difrakce a troposcatter

Profil trasy je vzorkován na Copernicus GLO-30 každých 30 m (více než 120 km). Ekvivalent poloměru Země je k = 157 / (157 + ΔN), s ΔN pokles N v prvním km od profilů GFS podél trasy [6, 7].

Reliéfy v blízkosti stanic (od 1.3.2, autor kritérium). P.452 platí horizont úhel lineárně, 0,573 dB na mrad: reliéf o několik desítek metrů několik set metrů od antény stojí desítky dB, mnohem více, než je pozorováno. Z tohoto důvodu mohou reliéfy do vzdálenosti 20 km od každé stanice sčítat až 6 dB: úhel horizontu je omezen na úhel nejvzdálenějších reliéfů plus 10,5 mrad. Platí pro vaši stanici a pro korespondenta. Řádek "Blízko reliéfů" říká, kolik DB vzlétlo.

Zdroj půdy (od 1.3.2). Kvóty pocházejí od společnosti Copernicus DEM GLO-30 [3]:1 druhý oblouk, moře je0 a pobřeží je čisté. Až do 1.3.1, program použil Terrain dlaždice "skadi" AWS. Blízko pobřeží tyto čtverce mix SSRTM s nízkým rozlišením mořského dna data a šířit podíl ostrova na moři: kolem Capri vytvořit ducha prsten 80-100 m mezi 0,5 až1 km od pobřeží. Dne 21. listopadu2026 IC8TEM oznámil neexistující horizont na severozápad; bodové srovnání s Copernicus to potvrdil (94 m na moři na severozápad Marina Grande vs.0). Tento falešný horizont + 2,6 ° stál přibližně26 dB troposcattera pobřežních stanic. Skadi tabule zůstávají jen jako rezerva, pokud Copernicus nereaguje. Panely Copernicus váží 15-30 MB a stahují se jednou; ty pouze moře neexistují a mají hodnotu0.

- Volný prostor: Lfs = 92,44 + 20 log f (GHz) + 20 log d (km) [8].
- Difraction: Bullington metoda na reálném profilu plus ITU-R P.452-16 § 4.2 [6] delta-Bullington sférická korekce, s ITU-R P.526 [9] sférická ztráta.
- První Fresnelova zóna: poloměr Ø (λ · d1 · d2 / d); bez, pokud půda zanechává nejméně 0,6 poloměru. Pro každou překážku je zobrazen parametr [9] Knife Board V a J (VV)..
- Při návrhu profilu je přímý poloměr zelený v optickém a červeném pohledu, pokud prochází pod zemí nebo pod zakřivením země; červené tečky jsou překážky, které napadají první oblast Fresnelu (fialová tečkovaná čára). Na moři "červený bod" ve výšce 0 m není hora: je to moře stoupající pro zakřivení. Procházení s myší na poloměru nebo na místě se objeví komiks s vysvětlením a hodnotami.
- Když reliéf v blízkosti antény zavře směr (např. horizont desítek stupňů), difrakce a troposcatter způsobí ztráty stovek dB: nad 300 dB program zapíše "> 300 dB" a "směr uzavřen sousední překážkou", signál "< − 230 dB" a poměry S / N "− 60 dB". V takovém případě je vhodné se podívat na "Doporučené rostliny" a "Via monte".
- Troposcatter: ITU-R P.452-16 eq. 45 až 50% času [6], s úhlovou vzdáleností od horizontů a N0 od profilů.
- Bez tropa: součet pravomocí difrakce a troposcatteru, nikdy pod svobodným prostorem.

## 8 bis. Troposcatter: čtyři modely a nejistota

Vedle P.452 program počítá troposcatter se třemi dalšími modely, stejně jako program TropoLink [16], a ukazuje jejich rozdíl jako nejistotu:

- NBS TN101 § 9 [25] (od 1.3.5), medián: L = 30 log f − 20 log d + F (θd) − F0 + H0 + Lgp. F (θd) je útlumová funkce přílohy III v jejích analytických formách (Ns 301) s korekcí na Ns, H0 na funkci přírůstku kmitočtu § 9.2: při 144 MHz, s nízkými anténami v vlnových délkách, reflexe ze země částečně vymazává pole v dolní části společného objemu a ztráty soli 3-10 dB. Ns je ten z hodin GFS profilů, efektivní horizonty a výšky pocházejí z profilu Copernicus, Lgp = 0,07 · e ^ (0,055 (Gt + Gr)) je otevírací ztráta spoje. Je to stejná forma, jakou ITM používá ve svém interiéru, ale s reaktivitou dneška místo klimatu. Zkouší ploché úseky při 144 MHz s anténami ve vzdálenosti 20 m: v rozmezí 2-4 dB od ITM mezi 150 a 900 km a přibližně 2 dB ve zjednodušené formě 9.2b TN101, která nemá H0.

- ITU- R P.617- 5 [17], medián: L = F + 22 log f + 35 log θ + 17 log d + Lc, s f in MHz, θ in mrad, d in km, F = 0,18 · N0 · e ^ (− hs / 7,35) − 0,23 · ΔN a Lc = 0,07 · e ^ (0,055 (Gt + Gr)). N0 a ΔN jsou obvykle převzaty z klimatických map ITU; zde používáme každou hodinu profily GFS ( N0 · e ^ (− hs / 7,35) je reaktivita k zemi, ΔN pokles v prvním km), takže také troposcatter následuje čas.
- RSGB VHF / UHF Handbook Formule, DL2FZN [18]: L = Lfs + 24,44 + 4,283 ln f + 0.06656 d. Sklon s vzdáleností je téměř roven sklonu PROPBER (0,067 dB / km).

Společný objem (od 1.3.1): je bod, kde je poloměr obou stanic překročen, každý nakloněn k jeho horizontálnímu úhlu, na Zemi s rovnocenným poloměrem ae = k · 6371 km. Z TX se poloměr zvyšuje jako z = hTX + x · θt + x2 / (2 · ae); Vzdálenost x = (hRX − hTX + d · θr + d2 / (2 · ae) / θ, s θ = d / ae + θt + θr. S plochými horizonty a anténami ve stejné kvótě má základna hodnotu d2 / (8 · ae): asi 1,3 km až 300 km, 3,7 km až 500 km, 9,4 km až 800 km. Čím větší je objem, tím více je vzduch suchý a rozsáhlejší a tím méně se šíří: z tohoto důvodu více než 800- 1000 km troposcatter dochází. Ve výsledcích jsou podíl a pozice základny, reaktivita, gradient a vlhkost GFS v této nadmořské výšce, a ekvivalentní vzdálenost Bray (QST, listopad 1961): každý extra nebo méně horizont má hodnotu asi 111 km. Odkaz: Prezentace "Over the horizont in VKV" by IU3QEZ (GEKO Magazine n. 69).

Přímka Troposcatter, jiné modely ukazují dvě ztráty a nejistotu jako polovinu rozdílu mezi nejoptimističtějším modelem a nejpesimističtějším. Jsou to pouze srovnání: ztráta bez tropo zůstává ztráta P.452.

## 8 ter. odpolední troposkok nad reliéfy (od 1.3.4)

IK3XTV, z Thiene ( JN55, s Alpy na 10 km), pracuje v létě odpoledne jižní Německo na 400-500 km, bez dohledu, se silnými signály, ale s rychlým a hlubokým evanescence; autor je přiřadil "konvekční buňky sladěné jako vlak čoček". Pozorování je potvrzeno; vysvětlení přijaté v programu je další, fyzicky pevnější.

Odpoledne slunce rozdmýchává vzduch nad reliéfy a mezní vrstva se zvyšuje na 2-3 km. Na jeho vrcholu vlhký vzduch níže splňuje suchý vzduch výše: tam turbulence indexu lomu (velikost, která řídí troposcatter) je maximální. Když tento vrchol dosáhne společného objemu trasy, která na 400- 500 km je asi 2-3 km, troposcatter je posílen. To vysvětluje odpolední čas, vysoký tlak (tiché válcové, silné topení) a rychlé vypínání, typické pro difuzi a ne kanálků. Konvexní buňky nepůsobí jako čočky: produkují turbulence a signál šíří turbulence v horní části přepracované vrstvy.

Důsledky pro předpovědi čtení:

- na zemi je první odpoledne nejhorší pro dolní kanály a vrstvy, ale ne pro troposkoka za horami; obě věci by měly být čteny odděleně;
- program již má dvě údaje, které slouží, výšku mezní vrstvy poskytované modelem a podíl společného objemu trasy (společný objem) na výsledcích): pokud jsou blízko, může odpolední horská úprava jít lépe než výpočet v normální atmosféře;
- od 1.3.4 se bere v úvahu výpočet bodu bodu: je-li společný objem na zemi, podél trasy jsou reliéfy nejméně 800 m, slunce je nad 15 ° a mezní vrstva přesahuje 800 m, a horní část vrstvy přetváření (model rychlost půdy + mezní výška vrstvy) je do 500 m od základu společného objemu, troposcatter zvyšuje 3 dB; mezi 500 a 1000 m lineární zisk klesá na nulu. "Odpoledne Troposcattera." Se sněhem podél trati (alespoň 10 cm na zemi podle modelu, s proměnnou sněhu _ hloubka Open- Meteo nebo SNOD GFS od NOMADS) se výztuž nepoužije: na sněhu zůstává půda studená a přetvarovaná vrstva neroste. Hodnota 3 dB je prozatímní a musí být kalibrována reálnými odkazy; vypne se s korekcí počasí.

Zdroje: připomínky F. Egano IK3XTV (Radio Rivista e www.qsl.net / ik3xtv ", Propagazione sulle2 metri", "Celle convettive", "Troposcatter over the Alps"), zdroje §13.

## 9. Zakládací vrstva podél trasy

Je to srdce výpočtu signálu. Podél trasy, GFS profily jsou pořízeny každých asi 25 km. Pro každou vrstvu tlaku je nejdelší úsek, v němž je stejná vrstva přítomna v následujících bodech; počítá se jako potrubí pouze silné vrstvy nejméně 40 m [10].

- kontinuita: od jaké km do jaké km a zlomek trasy;
- horní a spodní část a sklon v m / km;
- půda uvnitř vrstvy: km, kde se hory zvedají nad základnu;
- Pokrytá půda: na moři je vedení téměř vždy, rozhoduje o úseku země [10]. Pokud pozemek překročí 10% trasy, použije se minimální zlomek mezi kontinuitou a pokrytím země.

Trope zisk je zisk vrstvy pro použitý zlomek. Pokud je více než polovina profilů subrefrakterní, zisk je negativní.

## 9 bis. Opravy počasí: stabilita, poddanost, stávka

Od 1.3.0 profil také dává tři meteorologické informace, které lze vidět v registru (regave "stability (Misme)"), v Meteo řádku podél trasy výsledků a, s Segni meteo box, na mapě.

- Stabilita Misme [5, kapitola 6]. Misme váže pole za horizontem na složený parametr: M = a · (ge − 40 + b · ΔW), a = 0,5 dB na N / km, ekvivalentní gradient ge a tepelnou stabilitu ΔW, práci na zvýšení vzduchu o 1 km. Program počítá ΔW = g · К (θ − θ0) / θ0 dz mezi půdou a 1 km nad, s θ potenciální teplotou; standardní atmosféra má hodnotu asi 56 J / kg. Výraz přidaný k zisku je 0,07 dB pro J / kg nad rámec normy, omezený mezi − 4 a + 6 dB, a počítá se pouze v případě, že žádný vodič zachytí pásmo (gradient je již v Bean a Dutton indexu). Kalibrace je dočasná: musí být ověřena poměry (odstavec 14).
- Dolní a horní vzduch [19]. Moler a Holden, na trasách 125-305 km k moři na1365 a9380 MHz, našli nejvyšší signál asi 2,5 dB v průměru se vzduchu stoupající, a v případě posílení asi20 dB po dobu3 hodin na začátku ponoru, následuje pokles o22 dB, když se vzduch vrátil ke konvergenci. GFS dává vertikální rychlost850 a700 hPa. Pokud na obou úrovních klesne vzduch o více než1 cm / s a v potenciálním indexu lomu K dojde ke zpětnému nebo prudkému poklesu (N se hlásilo adiabeticky na1000 hPa, což zvýrazňuje suchý a teplý vzduch spadlý z horní části [5]), přidá se 1,5 dB; pokud se vzduch zvýší o více než5 cm / s, vzlétnou.2.
- [20] Scirocco. Horký a suchý vzduch ze Sahary přes chladnější mořský vzduch dává velmi silné inverze z východního Středomoří na Gibraltar. Značka S se objeví, kde při 850 hPa vítr pochází ze 100- 200 ° s nejméně 7 m / s a teplota je nejméně 16 ° C, ve Středozemním moři. Je to jen náznak: zisk rozhoduje o vrstvách profilu.

Bez vedení, které zachytí pásmo celkový zisk nepřesahuje 16 dB, konec žlutého: korekce zviditelňují silnější vrstvy, ale neprojdou kanálky (červená a fialová zůstávají v kanálcích).

- Perzistence (od 1.3.1). Největší otvory přicházejí po dobrých vysokotlakých dnech (VKV / UHF DX Book; Biancomano a Leyshon 1998, pravidlo 12 hodin; zprávy 29 / 12 / 2021-2 / 1 / 2022 rostou od prvního do čtvrtého dne). Pro každou buňku program počítá hodiny předchozích 24 s přírůstkem nejméně 13 dB a přidává se do poměru 2 dB, pouze pokud je zisk již nejméně 10 dB.

Jak číst značky na mapě (Změnit povětrnostní znaky; od 1.3.2 symbol je velký double a prochází s myší přes značku se objeví komiks s vysvětlením a hodnotami buňky):

| Podpis | Barva | Co to znamená? | Vliv na tropu |
|---|---|---|---|
|  ↓  | zelený | podsidence: vzduchové kapky (anticyclon), ohřívání pro kompresi a sucho, posiluje obrátky a udržuje je pevné | příznivý, + 1,5 dB |
|  ↑  | červená | do kopce vzduch (přední, nízký tlak, konvekce): míchá nízké vrstvy a přerušuje inverze a kanály | nepříznivé, − 2 dB |
|  S  | oranžová | Sirup: horký a suchý vzduch ze Sahary nad čerstvým mořským vzduchem | příznivé vodítko, žádná korekce: rozhodnout vrstvy |

Oblast plná zelené ↓ pod anticyclone je klasické znamení tvrdého otevření; Red ↑ šipky podél linie říkají, že vrstvy se lámou, i když barva buňky je stále dobrá.

Počasí Opravy vypne tyto opravy vidět model 1.2.

Validace (odstavec 14): na spojích 593 až 144 MHz mezi 29. prosincem 2021 a 2. lednem 2022 ve srovnání se stejnými trasami a o dva týdny později je index oddělování (0,5 = žádný, 1 = perfektní) 0,79 bez oprav a 0,91 s opravami (před střechou při 16 dB). Stejné srovnání ukazuje hlavní limit: vedení na moři v nízké nadmořské výšce, které v těchto dnech přinesl znamení mezi Kampánií a Ligurií, není vidět na úrovni GFS.

## 9 ter. Novinky 1.3.2: dva odhady, strop, vysoký tlak, moře, stanice, hřebeny

Rozumný a agresivní odhad. Přínos z gradientu roste z 0 dB na limit normálu až na 10 dB na práh lomu. Ve vrstvě 1000- 850 je obezřetným prahem Bean a Dutton (100 N / km); Agresivní prahová hodnota je klasická prahová hodnota Super- Refraction 79 N / km (k ³ 2; P.453 používá − 100 N / km v prvních 100 m), která s k50 = 157 / (157 − ΔN) je k doposud 2 (k = 2,5 odpovídá asi 94 N / km). Point- point kalkulace ukazuje dvě ztráty převrácené; s vodičem, který zachycuje gang shoduje. Mapa používá opatrnost.

Podkroví 2500 m. Silnice a kanály jsou hledány pouze do 2500 m nad zemí: vyšší antény amatérských rádií je nevyužívají. S 1500 m došlo ke ztrátě validačního bodu a 4% spojů nad 16 dB, protože v zimě dochází často k převrácení subsidence mezi 1500 a 2500 m.

Vysoký tlak se zvýšil. + 1 dB, kde alespoň 7 z 9 buněk v okolí snížilo tlak na moře o 1020 hPa nebo více a vítr je slabý v nízkých vrstvách (10 m pod 5 m / s, 850 hPa pod 8 m / s), s výjimkou vzestupných pohybů.

Mořská váha. Při výpočtu point-to-point se zisk vrstev vynásobí 0,8 + 0,4 · μ1, kde μ1 je faktor ITU-R P.452 (eq. 51) na delším kontinuálním úseku země: 1 na otevřeném moři, malý s dlouhými úseky půdy.

Stanice a vedení. Je-li vodič, který zachycuje pásmo, pokrývá pouze část vedení, je podíl každé antény srovnáván se základnou a horní částí vodiče na začátku a na konci krytého úseku; zisk se násobí 0,5 + 0,5 · · · (e ^ (− ΔhTX /300) · e ^ (− ΔhRX /300)), s Δh vzdálenost v metrech z kanálu. S potrubím na téměř celé trase je spojení §10.

Vrstvy a hřebeny. Překročí-li horní část okrajové vrstvy GFS (půdní rychlost modelu nejvíce ohraničené _ vrstvy _ výšky) vrcholy trasy (maximální místní výška větší než 300 m, pokud nejvyšší přesáhne 500 m), přechází vrstva přes hory bez přerušení: + 1,5 dB pro zlomek krytých hřebenů. Je to pozorování IT9GSF na otvory ze Sicílie do Německa s příznivým Tyrhenian.

Úrovně blízko k zemi na moři. Se zdrojem NOMADS program také stahuje teplotu povrchu (na moři je SST), teplotu a vlhkost na 80 m a ve vrstvě 30-0 hPa nad zemí. Na mořských bodech (rychlost modelu pod 5 m) jsou tyto úrovně přidány do profilu, pod prvním tlakem, vidět potrubí na 100- 200 m, že tlakové úrovně neřeší. Na zemi se nepoužívají: vrstvy noci a dne ještě nejsou platné. Open- Meteo jen dává povrchové teploty.

Odpařovací potrubí. Od SST, od teploty a vlhkosti do 2 m je odhadnuto δ výška odpařovacího potrubí s neutrálním logaritmickým profilem (jako v modelu Paulus a Jeske, bez korekce stability): vzduch do vlasů vody je nasycen do SST (98% pro sůl), proto

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

omezený mezi 0 a 40 m. K zachycení 144 MHz by to trvalo asi 180 m (Kerr), pak na 144 MHz odpařovací potrubí posiluje málo; od 432 MHz nahoru, s nízkými anténami na moři, hodně (odstavec 11 bis). Zadejte parabolickou rovnici a v řádku Opravy 1.3.2.

Čas dne (návštěva). Program počítá výšku slunce na každém bodě. Ze dne na den na zemi, se sluncem nad 15 ° a limitní vrstvou nad 800 m nebo větrem nad 5 m / s, varuje, že půdní kanály jsou reformovány; v noci, se sluncem pod − 3 °, vítr pod 3 m / s a mezní vrstva pod 300 m, varuje, že s klidovou oblohou je možné vést ozařování, které padá po východu slunce. Ne na moři. Pro teď jsou vidět, ne čísla: Prosinec validace nemá dostatek letních dnů kalibrovat. To jsou dva body, které autor považuje za důležité, z každodenní pozorování.

Normální profil CRPL (alternativa). Místo 40 N / km upevnil normální pokles následuje exponenciální referenční atmosféra CRPL: N (h) = Ns · e ^ (− c · h), s − ΔN v prvním km = 7,32 · e ^ (0,005577 · Ns) (Bean a Thayer 1959). S vlhkým vzduchem (Ns high) je normální strmější a slouží silnější gradient, aby se zisk; ve výšce je sladší. Ukázalo se, že špatné, dokud potvrzení říká, že to pomáhá.

## 9 kvater. kontrastní vzduch-moře a nízké moře: Jadran (od 1.3.3)

Rádiové vlny necítí mořské dno: cítí kontrast vzduchu a vodní plochy. Hloubka se počítá, protože rozhoduje o tom, jak rychle se teplota moře blíží teplotě vzduchu.

Jadran je nízké moře. Průměrná hloubka je asi 250 m, ale severní povodí málokdy přesáhne 100 m a záliv Trieste zůstává pod 25 m; pouze na jih klesá nad 1200 m. Vrstva vody tak tenká, že se za pár dní zahřeje a ochladí, Tyrhenian za týdny. Existují tři typické situace:

1. Jaro a časné léto, příznivé. Moře je stále studené, dokonce i pro sladkou vodu řek Alpine a Po; to přichází horký a suchý vzduch z pláně nebo Balkánu. Horký vzduch nad studenou vodou vytváří zvrat na moři (poradenský plynovod): je klasickým stavem otvorů mezi Itálií, Slovinskem a Chorvatskem.
2. Podzim a zima s Bora, nepříznivé. Studený vzduch nad teplejším mořem: nízká vrstva se stává nestabilní, mísí a potrubí se zlomí.
3. Po Bora, šance. Jen proto, že je moře nízko, Bora ochladí za pár dní. Pokud vstoupíte do anticyklonu s horkým vzduchem, kontrast vzduch-moře je silný a vedení je robustnější než obvykle.

Jadran je také uzavřený kanál mezi Apeniny a Dinari, méně než 200 km široký: mořská vrstva zůstává omezena, a trasy podél osy (Puglia- Venezia- Trieste, asi 700 km) zůstávají na moři, nejlepší podmínkou pro dlouhé potrubí.

Co program počítá. Na mořích, s opravami počasí a krabičkou "Contrasto aria- mare":

| Stav | Oprava |
| --- | --- |
| potenciální teplota 925 hPa (nebo 950) nad SST 4 K | _ |
| nad SST 7 K | + 2 dB |
| s vlhkostí vzduchu na 925 hPa (nad 80%) | − 0,5 dB u těchto bonusů |
| chlazené moře 1,5 ° C nebo více během 72 hodin před, s příznivým kontrastem | _ |
| teplejší moře vzduchu při 2 m 2 ° C (míchání) | − 1, 5 dB |
| teplejší moře 4 ° C | − 3 dB |

Celková korekce zůstává mezi − 3 a + 3 dB a bez vedení zůstává zisk pod žlutou střechou. Prahové hodnoty jsou dočasné: ve standardní atmosféře potenciální teplota 925 hPa přesahuje teplotu moře cca 2-3 K, pak 4 K již indikuje opak.

Vzpomínka na SST. Modely udržují teplotu moře téměř stabilní během předpovědi. Nedávný pokles je odvozen z analýzy tří dnů před (Open- Meteo, pouze povrchová teplota, pouze mořské buňky): maximálně 72 hodin před minus aktuální hodnotu. Pokud služba nereaguje, bonus za paměť se nepoužije.

Meze. SST modelů pod pobřežím a v horním Jadranu je hrubá; například studené pero Po je špatně řešeno. Batimetrie nezadává výpočet: zdroje vždy dávají SST, který již shrnuje účinek mořského dna. Kontrast vzducholodi se částečně překrývá na konci stability Misme (§ 9 bis); bonusy jsou proto malé. Prahové hodnoty budou tarátové s chodbami reklam a QSO přes Jadran.

## 10. Vedení na celé trase

Pokud vrstva zachytí pásmo na nejméně 80% použité frakce a stanice nejsou v dohledu, vypočítá se také ztráta v potrubí: volný prostor, plus spojení antén (3 dB, pokud jsou uvnitř vedení, více pokud jsou nad nebo pod), plus zbytková útlum γd = 5 · 10 − 5 · ae · f ^ (1 / 3) dB / mrad ITU-R P.452 eq. 49 [6]. Pokud je nižší než ztráta bez tropu, stává se očekávanou ztrátou.

Pásmo zůstává v potrubí pouze v případě, že jeho vlnová délka nepřekračuje λmax = 2 ″ 2 · (2 / 3) · ″ (ΔM · 10 − 6) · d (Kerr, eq. 17 [11]). K tomu může stejná vrstva lapit 23 cm a 70 cm, ale ne 2 m. Nicméně, limit není net: Kerr varuje, že i vlny několikrát déle trpí hodně vedení, a Hepburn a VHF / UHF DX Book mluví o "špičaté" hrany a "odpuštění" kanály. Od 1.3.1 program používá měkké vedení: je-li λmax mezi λmax a 3 · λmax, zisk postupně přechází z 15 dB (λ = 3 · λmax) na plné vedení zisk (λ = λmax), s s = (λmax / λ − 1 / 3) / (2 / 3). V tom případě jsou barvy potrubí povoleny. Logaritmus a Meteo podél trasy ukazují body s měkkým kanálem.

## 10 bis. Via Monte

Když je korespondent za horizontem uzavřen, program hledá nejlepší směr, jak zaměřit antény: osvětlený svah šíří signál jako radarový cíl. Spustí horizont každé páté (s výjimkou ± 15 ° kolem korespondenta) a v každém směru má osvětlený horizont nad 100 m a mezi 1 a 60 km. Rychlý odhad (volný prostor, bitaktická konverze a 10 dB pro každou úroveň horizontu z difuzoru na korespondenta) vybere 4 nejslibnější, alespoň 15 ° od sebe; pro každého:

- treat 1, stanice → reliéf, ve volném prostoru;
- zachází s 2, reliéf → korespondent, vypočtený jako by reliéf přenášený z 10 m nad vrcholem, se stejnou předpovědí (bez parabolické rovnice);
- konverzi pomocí bistatické radarové rovnice, s λ = σ0 · A, σ0 = − 15 dB (hora v VKV / UHF) a A oblast osvětlená paprskem R1 (maximálně 20 km2):

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

Přímka "Doporučený bod" označuje azimut nejlepší trasy, pokud to stojí méně než přímé. Při 144 MHz, s úlevou na 20 km a 15 ° yagi, stojí horská cesta asi o 45 dB více než jen 2 trasy: to se stane pouze v případě, že přímý je uzavřen sousedními překážkami. Odhad je hrubý (± 10 dB), zejména pro σ0.


Bez ztráty na hoře (možnost, od 1.3.2). S "Via monte senza perdite fino al monte" box, je pád mezi stanicí a difrakční bod je považován za null: je jako uvedení stanice na vrchol (anténa na 10 m nad bodem, stejný výkon, anténa a kabel). Obálka pak vybere bod s nejvolnějším horizontem směrem k korespondentovi (v paritě, nejvyšší) a výsledek ukazuje trasu od bodu k korespondentovi, se signálem a S / N, vedle celku s difuzí. Je to optimistický limit: říká, pokud se směr otevře, když hora zvedne signál dobře, nebo kam jít přenosný. Každý bod je uveden s ten- znak.

## 10 ter. Vstup do vzdáleného vedení

Pokud vedení, které chytá kapelu, začíná daleko za horizontem stanice, signál musí dorazit. Program přináší signál do výchozího bodu, uprostřed vedení, pro difrakci: podél profilu Copernicus GLO-30 hledá okraj s nejvyšším parametrem Fresnelu (ITU-R P.526), s otokem ekvivalentní země,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

větší útlum způsobený zakřivením horní části (zaoblená překážka, P.526-16 § 4.2) s poloměrem R odhadovaným z půdy Copernicus GLO-30 na ± 1 km od okraje,

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

(pro m · n > 4 je formulář 34b doporučení). Barsis (NBS, 1963) měřeno na třech dlouhých trasách přes horu (Colorado, Aljaška, Evropa) průměrné ztráty 10- 20 dB vyšší než ideální okraj Fresnel- Kirchhoff: zaokrouhlení vysvětluje hodně (Barsis a Kirby, 1961: Pikes Peak s poloměrem asi 11.5 km). Výsledek se porovnává s difrakce na hladké sférické půdě; je nejhorší. Pak se signál pohybuje v potrubí: volný prostor na celé trase, zbytková útlum γd P.452 na úseku v potrubí a spojení korespondenta (3 dB uvnitř, 1 dB každých 10 m venku). Pokud je tato ztráta nižší než očekávaná ztráta, stává se mechanismem ("chování (vstup difrakcí)"). Je to autorův předpoklad o italské geografii: s nedalekou překážkou hluboko pod čárou, v je negativní a vstup stojí málo, s hřbetní přední náklady 15-30 dB.

## 10 kvaterů. ITM (dlouhozrnná rýže) a výstup z kanálu

Od 1.3.2 program obsahuje nepravidelný Terrain Model 1.2.2 NTIA / ITS (Longley and Rice, ESSA ERL 79-ITS 67, 1968; Hufford, Longley and Kissid, NTIA 82-100, 1982), přeložen do C # oficiálním kódem americké vlády, který je bez práv. V překladu jsou uvedena stejná čísla jako v originále: auto compilation srovnává pokaždé NTIA C + + kód v pěti zkušebních případech (pod 0,01 dB).

ITM počítá ztrátu v normální atmosféře na skutečném profilu Copernicus GLO-30: vidět s odrazem od země, difrakce (spigol a sférická půda vážená na drsnosti půdy Δh, to je rozdíl mezi 10. a 90 ° percentil kvót kolem rovné minimální čtverce) a troposcatter. Reaktivita půdy je snížena na průměrnou část trasy, $N_s = N_0\,e^{-h/9460}$, a dává zakřivení ekvivalentní půdy,

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

Program využívá rádiové klima zvolené trasy jako v § 10 pohlaví (do 1.3.4 vždy mírné moře, s N0 320 na zemi a 350 na moři), s N0 z průměrné reaktivity místa (půda εr 15, δ 0,005 S / m; moře εr 81, δ 5 S / m), horizontální polarizace a variabilita v čase bez tohoto místa (profil je znám). Ukazuje, že ztráta překročila 50% času a ta, kterou máte za 10% a 1% nejlepšího času. ITM nemodeluje kanály: jeho variabilita v čase je statistická, podle klimatu. Slouží jako druhý výpočet základní hodnoty vedle P.452; rozdíl mezi těmito dvěma dává představu o nejistotě.

Druhým použitím je poslední úsek po vodiči, který končí před příslušným. Na výstupním místě je umístěna virtuální anténa v polovině vodiče a ITM vypočítá trakt na odpovídající, jako překročení volného prostoru v 50% času. Celková ztráta je

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,	heta_{condotto} + A_{ITM}(d_{uscita} 	o RX)
```

kde A _ TX je spojení antény (3 dB uvnitř, 1 dB každých 10 m mimo anténu), pokud je vodič v blízkosti, jinak vstupní difrakce o hodnotě § 10 ter. Je to limit, který autor sám dal: kontinuita vrstvy plus, nanejvýš, zbytkové útlum. Je-li nižší než očekávaná ztráta, stává se mechanismem ("chování (výstup a konečné protažení ITM)").

## 10 quinquies. Prohloubení: ostrovy, úžiny a nekontinuita zemského moře

Příklad z Neapolského zálivu (Anacapri, JN70CN) směrem na západní Středomoří ukazuje, kde trasy přes ostrovy:

| Země určení | Azimut | Kde přechází ostrovy | Překážka |
| --- | --- | --- | --- |
| Barcelona |  279°  | severně od Sardinie (Gallura) do 41,05 ° N, mezi 420 a 490 km | asi 30 km jižně od Bocche di Bonifacio; Limbara (1359 m) asi 20 km od trati |
| Bocche di Bonifacio |  282°  | úzká na 41,3 ° s. š. | asi 11 km |
| Marseille |  296°  | Střední Korsika při 42,2 ° s. š. | hory do 2700 m: potrubí pod 2 km tam zastaví |
| Valencie, Balearics |  260–265°  | Střední Sardinie | vnitřní reliéfy |

Pro Barcelonu stačí ukázat na 282 ° místo 279 °, protože osa prochází z úst: 3., uvnitř paprsku normální yagi.

Jsou ústa úzká? Pro dolní kanály, pod pár set metrů, ano. Ostrovy jsou zazděny, a nad zemí, ve dne, topení prolomí inverzi. Úžina sama o sobě je dostatečně široká: asi 11 km, proti první oblasti Fresnelu, méně než jeden kilometr široký, polovina 1000 km trasy ve 144 MHz. Problém je počasí. Mouths jsou zábava pro vítr: Dolní vrstva se zrychlí a spodní vrstva se odstraní. Často průchod je otevřen jako geometrie, ale narušen jako atmosféra. S stabilním anticyklonem a malým větrem může být místo toho dobrý průchod.

Pozemsko-mořské rozdíly. Počítají se. Je přerušeno vedení tvořené na tyrhénském pobřeží: signál vychází z vedení (sekce po výstupu, § 10 kvater) a musí se vrátit k jinému vedení na moři Balearics, platit znovu spojku. Proudy ve výšce nad 1500 m přecházejí přes Galluru, aniž by si je uvědomovali; pro centrální Korsiku neplatí.

V praxi: je-li cesta přes horský ostrov, zkuste nějaký stupeň korekce směrem k úseku volného moře, ale nepočítejte s průchodem se silným větrem v úzkém.

## 10 pohlaví. TN101 rádiová klima a časová variabilita (od 1.3.5)

TN101 § 10 [25] popisuje, jak se ztráta trasy za horizontem liší v dlouhém období kolem mediánu, s různými křivkami podle klimatu: funkce Y (q) říká, kolik dB ztráta klesne v q% nejlepší doby. ITM obsahuje tyto křivky pro sedm podnebí. Podnebí se hodně počítá: na moři v mírném mořském klimatu, při 144 MHz, v 10% času existuje mnohem více dB než na zemi v kontinentálním klimatu, protože kanály jsou častější.

Program již neopravuje klima: Vybírá si ji pro každou cestu z průměrného bodu, takže funguje po celém světě a nejen v Evropě. Stáhněte si z Open- Meteo [26] tři roky denních hodnot reanalýzy ERA5 (teplota, vlhkost a tlak půdy, déšť) pro buňku 0,5 ° průměrného bodu, pouze jednou; hodnoty zůstávají v Data / CLIMA. Obnova půdy

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Den za dnem, pak roční průměr a výlet mezi měsíčními průměry, průměrná vlhkost a roční déšť. Pravidla se řídí popisem podnebí TN101 § 10.2; hranice mezi jednou klimatem a druhou jsou kritériem autora:

- nad 60 ° zeměpisné šířky: mírné kontinentální ( TN101 nemá žádné křivky pro polární klima);
- deště o průměru méně než 250 mm za rok a vlhkosti pod 50%: poušť (typicky 280 Ns);
- do 20 ° od rovníku, Ns nejméně 350 a exkurze do 30: rovníková (Ns typická 360);
- v rozmezí 32 ° a exkurze nejméně 60: subtropické kontinentální, tj. monzuny (typická exkurze 60- 100);
- mezi 10 ° a 32 ° s Ns nejméně 340: subtropické moře (Ns typický 370);
- oba horizonty na moři nad 20 °: mírné moře na moři (je to definice TN101);
- exkurze až do 30 u moře (moře na trase nebo průměrná vlhkost nejméně 75%): námořní temperované na zemi;
- jinak mírné kontinentální (typická exkurze 20-40).

N0 pro ITM je průměr snížený na hladinu moře, N0 = Ns · e ^ (h / 9460). Bez sítě se použije rezervní pravidlo pouze se zeměpisnou šířkou a mořským zlomkem a hranice to říká.

Testy s údaji ERA5 2023- 2025: Capri Ns 339 s exkurzí 45 (kontinentální temperovaný na zemi, moře temperovat, když oba horizonty jsou na moři); Londýn 327 a 16 (temperované hvězdy); Vídeň 319 a 24 Abidjan 381 a 11 (rovníková); Miami 365 a 36 (subtropický manžel); Dillí 337 a 82 (subtropické continental); Riad 278 a 37, 90 mm déšť (poušť). V Chile: Antofagasta 341 a 19 (subtropický manžel: pobřeží Atacama, s téměř pevným nízkým opakem Pacifiku), Calama 232 na zemi, 2257 m, 14 mm deště (poušť), Valparaíso 331 a 11 (teplotní šev), Punta Arenas 313 a 5 (teplotní šev).

Přímka "Radio Climate ( TN101)" ukazuje klima, důvod, N s polovinou výletu a Y: kolik dB vydělat 10% a 1% času a kolik ztratit v 90%. Je to dlouhodobá statistika a zůstává oddělena od prognózy: očekávaná ztráta vyplývá z dnešní doby s profily GFS, a shrnout dvě věci by počítat dvakrát stejné abnormální šíření.

## 11. Srovnání s ITU- R P.452

Porovnávací čára P.452 používá metodu ITU pro kanály a reflexe na vrstvách (P.452-16 § 4.5 [6]) s mediánem ΔN GFS namísto klimatologické a p =20% jako kalibrační konstanta, jak je uvedeno ve studii CSIRO [10]. Zahrnuje ztrátu empirického spoje Alf pod 0,5 GHz (přibližně28 dB až144 MHz). Je to pouze srovnání: očekávaná ztráta zůstává ztráta podle odstavce9.

## 11 bis. Parabolická rovnice (od 1.3.1)

Parabolická rovnice Line je čtvrtý výpočet, pouze srovnání, vyrobené jako profesionální programy pro kanály (PETOOL; wave- propagace rámec M. Lytaev, MIT licence): místo odhadování zisk z vrstev, to posune vlnu podél trasy s Fourierova split- step metodou uvnitř M modifikovaný GFS refrakční profil, převzatý z předpovědi bodu blíže k každé vzdálenosti. Hladiny GFS se přidávají k odhadovaným skokům potenciálních kanálů (výpočet A, 50 m v polovrstvě). Půda Koperník GLO-30 vstupuje kroky (pod zemí pole je null), půda je vodič a paprsek antény je Gaussian s odhadem otevření od zisku. Výpočet vidí vedení, ti, kteří prohrají, difrakci a spojení antén; On nevidí troposcatter, který se přidává odděleně. Dále je "profilový šek": stejný se standardní atmosférou menší než s očekávaným profilem.

Od 1.3.2:

- Programátor je v širokém úhlu s přesnou fází volného prostoru:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- dno na moři a na pobřežní pláni v nulové nadmořské výšce odráží koeficient Fresnelu v horizontální polarizaci, εc = εr − j · 60 · λ · δ (moře εr = 81 a К = 5 S / m; země 13 a 0.006 S / m), v jasném úhlu trasy:

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- drsnost moře snižuje konzistentní odraz podle Millera a Browna, s efektivní výškou vln z větru na 10 m (σh = 0,0051 · U2):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- na moři profil M zahrnuje odpařovací kanál (odstavec 9 ter) s neutrálním logaritmickým profilem:

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

Testy s prototypem (10 m anténa na moři, zisk na standardní atmosféře na 100-300 km): s δ = 10- 30 m odpařovací kanál poskytuje 1-3 dB až 144 MHz, 8- 37 dB až 432 MHz a 21- 66 dB až 1296 MHz. Při 144 MHz v horizontální polarizaci moře odráží téměř jako kov (IR 124; η IR 124; více než 0,99 až několik desetin stupně) a drsnost se počítá málo: nové dno mění několik desetin dB; počítá se více od 432 MHz nahoru.

Mezní hodnoty: nad zemí zůstává vertikální profil úrovně GFS, takže tenké kanály zůstávají neviditelné, země ke krokům se přibližuje, paprsek zůstává v rámci6.

## 12. antény, signál a hluk

- Yagi DL6WU: zisk 9.5 + 7.8 · log10 (boom / λ) dBd (2.35 dB pro dvojité boom, DL6WU opatření na VKV komunikace 3 / 1982), plus 2.15 pro [12] dBi.
- EIRP = výkon − kabel + TX zisk.
- Obdržený signál = EIRP − očekávaná ztráta + zisk RX − kabel RX. S- metr IARU: S9 = − 93 dBm, 6 dB na jednotku.
- Vnější hluk podle ITU-R P.372 [13]: galaktický (Fa = 52 − 23 log f) více umělý podle prostředí (město, obytné, venkovské, klidné venkovské) nebo ručně psaná teplota antény.
- Tsys = Ta + Tpre + [290 (Lc − 1) + 290 (Fr − 1) · Lc] / Gpre na terminály antény (Friis vzorec). Od 1.3.5 má přijímací stanice vždy preamp v anténě, pevný parametr: číslo hluku 0,8 dB (Tpre = 59 K) a získat 20 dB. Za nimi je kabel (ztráta Lc) a rádio se základním hlukem Fr č. 6 dB. S 1 dB kabelu klesne přívod kabelu a rádia z cca 1160 K na 12 K: hluk určuje oblohu, prostředí a preamp. Zisk 20 dB je typická hodnota zvolená programem.
- S / N v SSB, CW a FT8 v pásmu každého směru ve srovnání s nastaveným prahem.
- Pravděpodobnost pro období (od 1.3.2): evanescence během hodiny je normální rozdělení S / N kolem očekávané průměrné hodinové hodnoty. Bray (QST, listopad 1961, s. 39, na křivce Norton 1959) dává amplitudu 13,5 dB nad 100 mil, tj. asi 7 dB pod a 7 nad průměrem, který téměř padá přímo pod 100 mil (3,5 dB až 50 mil); pro "pevné" připojení bere 7 dB z rozpočtu. Zabráno ± 7 dB jako ± 2∞: λ = 3,4 dB nad 160 km, úměrné vzdálenosti uvedené níže. Pravděpodobnost, že období překročí práh cesty je

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  s normální funkcí distribuce: marže 0 dB = 50%, + 3,4 dB = 84%, + 7 dB (pevná marže Bray) = 98%. Nezahrnuje nejistotu modelu, který je větší.
- Digitální způsoby: S / N v 2500 Hz ve srovnání s prahovými hodnotami naměřenými při 50% dekódování bez předběžných informací (AP). FT8 − 21 dB a FT4 − 17,5 dB na šumu bílé; pokud je mechanizmem troposcatter, použijte prahové hodnoty s evanescenční "narušenou zeměpisnou šířkou", FT8 − 18,6 dB a FT4 − 15,2 dB [21]. Q65 -15C − 22,2 dB, Q65 -30B − 24,8 dB, Q65 -60A − 27,6 dB V letadle rozptyl Doppler může překročit tóny Q65 -60A (1,7 Hz): jsou lepší než 30B nebo 15C. JT65A − 25 dB [22]. Dekódování AP (název již známé nebo konec QSO) dává asi 2 dB navíc pro FT8 a 1,5 až 2.6 dB pro Q65; nevstupuje do prahu, protože to není platné pro všechny hodnoty "− 24 dB", které jsou často čteny pro FT8 jsou limit WSJT-X vztahy nebo AP dekódování, ne práh na 50%. [24]

## 13. Doba odezvy a údajů

Každý výpočet měří svůj vlastní čas: GFS profily (síť, analýza, povinné přestávky 20 s mezi šaržemi pro Open- Meteo limit), Copernicus GLO-30 terén (se stažením tabulek, 15-30 MB každý, pouze poprvé), výpočet. Řádka Response Time je shrnuje; v registru začínají řádky pomocí TEMPI. Při modernizaci mřížky, registr také hlásí GFS závod použitý, když se stal k dispozici a jak staré údaje jsou.

Od 1.3.2 si můžete také vybrat ICON-2I, model agentury ItaliaMeteo a Arpae na Itálii na asi 2.2 km, přes Open- Meteo (model Italia _ meteo _ arpae _ icon _ 2i, dva závody denně, 3 dny). Pokrývá pouze Itálii a nedaleké moře a má méně nízké úrovně GFS (1000, 925, 850, 700, 500 hPa): pro kanály v nadmořské výšce není lepší než GFS, ale jemná mřížka popisuje dobře pobřeží, ostrovy a moře na 2 m, to je odpařování potrubí. Proměnné, které model nemá (koncový limit, teplota povrchu...), se z požadavku vyjmou. Od 1.3.2 automatický zdroj používá NOMADS; Open- Meteo slouží pouze v případě, že NOMADS neodpovídá a pro historické případy. S "Aktualizace sama o sobě" box (osvětlené vadou) je předpověď nekliknutá: na otevření, na každém novém GFS závod na NOMADS (asi 5 hodin po době závodu) a když viditelná zóna vyjde na více než čtvrtinu od mřížky, maximálně pokus každých 15 minut, nikdy s historickým případem.

Žádosti na Open- Meteo mají limity za minutu, hodinu a den pro každou internetovou adresu. Stažené profily jsou uloženy na disku (Dati / cache-gfs) pro každý zdvih a model: se stejným závodem, který se mění každých 6 hodin, již stažené body se neptají znovu. S automatickým zdrojem začnete od NOMADS a Open- Meteo slouží pouze pro chybějící body; s jediným Open- Meteo, v časovém limitu nebo denní zastaví okamžitě a říká, kdy zkusit znovu. Historické případy nejsou uloženy.

## 14. Potvrdit model

Předpověď by měla být porovnána se skutečnými spoji. Logaritmus výpočtu uchovává každé číslo; archiv programu udržuje místo a dekódování. Pro 432 a 1296 MHz, kde je málo míst, potřebují zprávy operátorů: kdo navázal spojení, do jaké doby, s jakým signálem. Po mnoho hodin porovnejte očekávaný zisk s měřeným signálem říká, že prahové hodnoty mají být přesunuty.

## Zdroje

1. NOAA / NCEP, Global Forecast System (GFS), globální model při 0,25 °, čtyři závody denně.
2. Open- Meteo, predikce API (open- meteo.com), gfs _ bezešvý model, proměnné na tlakových úrovních a závodní metadata.
3. Společnost Copernicus DEM GLO-30 (ESA; WorldDEM © DLR e.V. 2010-2014 e © Airbus Defence and Space GmbH 2014-2018, dodávaná v rámci programu Copernicus Evropskou unií a ESA), 1 druhý oblouk, rámce GeotiFF na otevřeném registru internetových služeb společnosti Amazon. Rezerva: NASA / USGS Copernicus GLO-30 v Terrain Tiles "skadi" z AWS.
4. ITU- R P.453, Refrakční index rádia: datum jeho vzorce a lomu s mapami digitálních kanálů (Studijní skupina 3, © ITU, osobní i nekomerční použití).
5. B. R. Bean, E. J. Dutton, Radio Meteorology, NBS Monograph 92, 1966: Tabulka 6.5 a § 4.4.
6. ITU-R P.452-16, Predikční postupy pro hodnocení rušení mezi stanicemi na povrchu Země při frekvencích nad 0,1 GHz: § 4.2 (difrakce), eq. 45 (troposcatter), § 4.5 a eq. 49 (vedeno).
7. ITU- R P.834, Účinky troposférického lomu na šíření radiovln.
8. ITU- R P.525, Výpočet útlumu volného prostoru.
9. ITU- R P.526, propagace difrakcí: eq. 31 (cutcheon) a § 3 (sférická půda).
10. B. Indermuehle, H. Suzuki, Troposférická regulační predikce založená na modelových údajích GFS v Inyarrimanha Ilgari Bundara, observatoř radioastronomie CSIRO Murchison, Publikace Astronomické společnosti Austrálie, 2026 (arXiv 2608.12861).
11. D. E. Kerr, Propagation of Short Radio Waves, MIT Radiation Laboratory Series vol.13,1951: eq.17 e18.
12. G. Hoch DL6WU, "Extremely Long Yagi Antennas", VHF Communications 3 / 1982, pp. 130 et seq., fig. 1 (měřeno 432 a 1296 MHz podle délky boomu).
13. ITU- R P.372, Rádiový hluk: galaktický a umělý hluk pro životní prostředí.
14. NOAA / NCEP, NOMADS, GRIB filtr GFS 0,25 ° (nomads.ncep.noa.gov); WMO G RIB2 formát, komplexní balení s prostorovými rozdíly.
15. Open- Meteo, Historická předpověď API (modelový předpokládaný archív asi 2021).
16. LukiBox, TropoLink, návrh troposcatterových tras s P.617, NBS TN101 a ITM flanked (GitHub, Apache 2.0 licence).
17. ITU- R P.617-5, Propagační predikční techniky a data potřebná pro návrh transhorizontálních radiorelé systémů,2019.
18. RSGB, VKV / UHF Handbook; DL2FZN implementace (andi-f / troposcatter on GitHub, 2013).
19. W. F. Moler, D. B. Holden, "Troposférický rozptyl šíření a atmosférické oběhy", Journal of Research of the NBS 64D, 1960, doi 10.6028 / jres.064d.014: signál za horizontem (1365 a 9380 MHz, na moři) a vertikální pohyb vzduchu.
20. Příručka ARRL 2022 (99a ed.), § 19.4.3-19.4.4: Troposférické počasí a šíření.
21. S. Franke, B. Somerville, J. Taylor, The FT4 and FT8 Communication Protocols, QEX July / August2020.
22. W3SZ, digitální prahové hodnoty pomalého režimu ( JT65, JT9, QRA64) a volba režimu podle šíření,2019.
23. QST, "The World Above 50 MHz", November 2022: otvor 14-15 August 2022 mezi Manitobou a Kansas / Missouri.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Quick-Start Guide to Q65, 3 April 2021 (wsjt.sourceforge.io).
25. P. L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis, Transmission Loss Predictions for Troposféric Communication Circuits, NBS Technical Note 101, magazín 1967: § 9 (troposcatter, function F (θd), H0), § 10 a příloha III (rádiové klima a variabilita v čase).
26. Open- Meteo, Historical Weather API (ECMWF ERA5 recenzi od roku 1940; Copernicus Climate Change Service, CC BY 4.0 licence).
