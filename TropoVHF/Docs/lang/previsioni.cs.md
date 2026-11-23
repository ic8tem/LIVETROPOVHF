# Předpověď

> Automatický překlad italsky: v případě pochybností stojí za italský průvodce.

Záložka Předpověď počítá tropoferické šíření samo o sobě: indexová mapa na viditelné ploše a signál mezi vaší stanicí a odpovídajícím. Tato příručka popisuje všechna data, jak se vypočítávají a odkud pocházejí. Čísla v hranatých závorkách odkazují na zdroje v dolní části; v programu mají každá data výsledků komiks (simbolo i) se stejným zdrojem a odkazem na odstavec.

## 1. Co dělá a co dělá

Předpověď svislých teplotních a vlhkých profilů modelu GFS [1, 2] odvozuje reaktivitu vzduchové vrstvy po vrstvě a odhaduje, kolik signálu v porovnání s normální atmosférou získá. Na trase se přidává reálná půda (Copernicus GLO-30 [3]), difrakce, troposcatter, kanály a přijímací hluk.

Není to opatření: je to odhad na modelu počasí se vzdálenými úrovněmi stovek metrů. Slouží k porovnání situací (dnes lepší než včera, o 70 cm lepší než 2 m, tato péče lepší než to), nezaručuje připojení. Odstavec 14 vysvětluje, jak shromáždit důkazy, které by je potvrdily.


> "Promnožení nelze zobecnit, ale studovat ve vztahu k místní morfologie."
> - Volker Grassmann, DF5AI, reported by Flavio Egano, IK3XTV (propagace na 2 metry)

Je to princip, který řídí výpočet bodu bodu: každá cesta se počítá na své půdě, s horizonty, jeho moře a jeho reliéfy; mapa dává obecný obraz, zachází s odpovědí.

## 2. Jazykové příkazy

- Pásmo: 144, 432 nebo 1296 MHz. Mapa a výpočet použijte zvolené pásmo.
- Kurzor posune předpověď od teď do 72 hodin dopředu, do 3 hodin.
- Aktualizace předpovědi (viditelná zóna): stáhnout profily GFS na mřížce až 240 bodů pokrývající viditelnou mapu. Rozteč mřížky (0,5 ° nebo více) závisí na zvětšení.
- Stop: přerušení stahování nebo výpočtu.
- Podrobnosti pro zobrazení (pravý panel mapy): tři úrovně, Essential, Normal a Complete, zapněte skupinu krabic; každý box se pak může ručně změnit. Krabice jsou ve třech skupinách:
  - Na mapě: isolinee del Gain, také 3-7 dB (odtahování), isobare, tepelná inverze, povětrnostní známky S ↓ ↑, korelace zisk / tlak, zóna přímého pohledu (odstavce 4, 5, 7 a 9 bis). Barvy vždycky zůstanou.
  - Při výpočtu signálu: všechny řádky výpočtu, plochý zemský profil, záznam výpočtu a podrobný záznam souboru.
  - Opravy a živá data: opravy počasí (Misme, subsidence), kontrast vzduchu-moře, průměr s místy (corridoi), APRS 2 m skoky a LoRa, za horou bez ztrát (odstavce 4 bis, 9 bis, 9 quater a 10 bis).
  - Základní drží pouze izolanty, počasí a opravy médií s místy; Normal dodává isobare, inverze, povětrnostní značky, přímý pohled, ploché země, air- sea a APRS; Kompletní zapne. Úroveň mění pouze to, co vidíte a co opravy zadávat: výpočty zůstávají stejné.
- Zdroj dat a historické případy: odkud pocházejí profily a který den hledáte (odstavce 3 a 3 ter).
- Moje stanice a korespondent: Lokátor, antény, napájení, kabely, hluk (odstavce 7 a 12).
- Vypočítejte signál a pokrytí v optice: dva výpočty na zemi (odstavce 8-13).
- Registr výpočtu: každý krok s použitými čísly, včetně souboru Data / contastast- calkoli.log.

## 3. Od GFS dat k profilu

Pro každý bod mřížky a každou hodinu čteme tlak na zem, teplotu a vlhkost na 2 m, vítr na 10 m, výšku mezní vrstvy, CAPE a teplotu a vlhkost v poměru k 1000, 975, 950, 925, 900, 850, 800, 700, 600 a 500 hPa. Od 1.3.0 můžete také číst vítr na 850 hPa a vertikální rychlost na 850 a 700 hPa (odstavec 9 bis).

Úrovně se počítají s ipsometrickým vzorcem a virtuální teplotou; úrovně pod zemí jsou vzácné. Pro každou úroveň:

- N = 77,6 / T · (P + 4810 · e / T), s T v kelvinu, P a hPa a saturačním tlakem ITU-R P.453-14, eq. 9, s korekčním faktorem EF (recontrolováno na úředním textu) [4].
- M = N + 157 · h, s h v km: modifikovaná reaktivita. Tam, kam M jde, je vedení.

## 3 bis. Zdroj dat

Menu Data Source si vybere, odkud profily pocházejí:

- Automatické (doporučené): GFS první z NOMADS [14], NOAA služba, která distribuuje model: za každý okamžik program žádá dva výřezy oblasti ve formátu G RIB2 a čte je sám; NOMADS nemá denní limit, pouze jeden za minutu, že program respektuje. Se širokými mřížkami (od 0,5 ° nahoru) používá 0,5 ° nebo 1st souborů každé 3 hodiny, mnohem lehčí. Body, které NOMADS nedává, nebo všechny, pokud NOMADS neodpovídá, zeptejte se Open- Meteo [2]. Tyto dva zdroje dávají stejný model a stejný závod, takže můžete míchat ve stejné síti.
- GFS od Open- Meteo: pouze Open- Meteo, jako v 1.2 (na limit za minutu se očekává).
- GFS od NOAA NOMADS: pouze NOMADS. Pomaleji ke stažení (dva soubory za hodinu, půl sekundy mezi jedním požadavkem a druhým ne překročit limit NOAA), ale nespotřebovává limit Open- Meteo.
- ICON- EU (DWD) a ECMWF IFS 0,25 °, od Open- Meteo: modely jiné než GFS, které platí po celé síti a pro trasy. Nemíchají se s GFS: srovnání dvou modelů ve stejnou dobu je jednoduchý způsob, jak zjistit, jak je předpověď nejistá. ICON- EU pokrývá pouze Evropu; ECMWF nemá všechny tlakové úrovně GFS (chybějící úrovně skoku) a nemusí mít mezní vrstvu.

Použitý zdroj je napsán ve stavové řádce a v první řádce výsledků (Zdroj dat), s tím, kolik bodů přišlo z každé služby a kolik z cache. Schránka disku je oddělena modelem.

## 3 ter. Historie případů

Historické Případové menu přezkoumává zdokumentované otevření s archívem Open- Meteo [15] GFS prognózy: mapa se přesune do oblasti případu, kapela se nastaví a čas kurzor odejde od prvního dne případu. Stisknutím aktualizace prognózy si stáhnete profily těchto dnů; výpočet signálu funguje jako pro aktuální předpověď. Slouží k porovnání modelu s tím, co se opravdu stalo.

První případ je případ 14-15 srpna 2022 uvedený v QST (listopad 2022, "Svět nad 50 MHz" [23]): otevření pod vysokým tlakem mezi Manitoba a Kansas / Missouri, od 144 do 1296 MHz téměř vše v FT8, s kanadským záznamu na 10 GHz 1228,56 km v Q65 a signály až 1941 km. Druhým je otevření mezi 29. prosincem 2021 a 2. lednem 2022 na Tyrhenian a západní Středomoří na 144 MHz, ze zpráv IC8TEM: první otevření na 29 (až 737 km), širší na 31 ( IT9GSF - EB5EA 1217 km), velký otevření dne 1. ledna mezi Itálií, Sicílií a Španělskem. Pro starší případy (např. září 2000) nemá archív Open- Meteo žádná data. V archivu není žádná limitní výška vrstvy: jeho korekce neplatí.

## 4. Index a barvy

Pro každou vrstvu mezi dvěma úrovněmi program srovnává N pokles s prahy tabulky 6.5 Bean a Dutton [5]: například mezi 1000 a 850 hPa pokles až 60 N / km je normální, přes 100 N / km je superrefraction. Nadrefrakční vrstva má hodnotu 10 až 15 dB zisku; potrubí, které zachycuje pásmo od 20 do 25 dB, podle deficitu M. Silného větru k zemi, vysoká mezní vrstva a vysoký CAPE snižují zisk, protože odstraňují nebo lámou vrstvy [5, § 4.4].

Barva je odhadovaný zisk v dB, s pevnou stupnicí rovnající se ve všech tématech:

| Zisk | Barva | Význam |
| Pod 7 dB | Nikdo. | téměř normální atmosféra |
| 7- 10 dB | modrá | gradient nad normální |
| 10- 13 dB | zelený | superrefrakční vrstva |
| 13- 16 dB | žlutá | silná vrstva, ale bez pásů |
| 16- 18,5 dB | oranžová | velmi silná vrstva nebo příliš tenké potrubí |
| 18,5- 21 dB | červená | Conduit Trapping Band |
| od 21 dB | Fialová | Silné a hluboké potrubí |

Se zobrazovací skříní také 3-7 dB zóny mezi 3 a 7 dB, kde se něco tvoří, ale zůstává pod měřítkem, můžete vidět s lehkou šedou zdrženlivost; barvy se nemění. tečkovaná tabule vymezuje oblast pokrytou vlastní prognózou a ukazuje maximální viditelný zisk: pokud je pod 7 dB, mapa zůstává bez barev, protože atmosféra je téměř normální, ne proto, že údaje chybí. Při stahování nového kliknutí na Aktualizační předpověď nepřerušuje, pro zastavení je zde Stop.

Barvy jsou neprůhledné na 82%, takže pozadí tématu je jen mění. Lokátory mřížky a čtvercové názvy jsou navrženy nad barvami. Kde vaše předpověď není tam, ale Hepburn nebo F5LEN dát 5 nebo více šachových karet objeví s barvou tohoto zdroje.

Mapa soudí každý bod sám. Výpočet signálu místo toho řídí, že vrstva je kontinuální podél trati (odstavec 9): cesta, která prochází velmi žlutou může dát méně, než si myslíte, že mapa.

## 4 bis. Předpověď zprostředkovaná reklamou: chodby (od 1.3.3)

Autorská pravidla: Samotné spojení nesmí nikdy čerpat vedení. Izolovaný a krátký příjem může být rozptyl průchozí roviny (balíček APRS je velmi krátký), otevření několika minut nebo chybu, a by dát "drop- chování" klamou. Program rozpozná chodbu, když ve stejné skupině:

- nejméně 3 páry nezávislých stanic (bez společné stanice) pracují na trasách o délce nejméně 300 km;
- tratě jsou rovnoběžné: azimut v rozmezí ± 15 °, průměrné body v rozmezí 150 km od společné osy;
- všechny za posledních 30 minut, přičemž se paprsek opakuje ve vzdálenosti nejméně 10 minut (perzistence);
- pro ARS se skok počítá pouze v případě, že se stejný skok opakuje;
- od 1.3.4 se trasy nad 1500 km počítají pouze tehdy, pokud paprsek pochází alespoň ze dvou různých zdrojů (např. z PSKReporter a cluster): z jednoho zdroje na vzdálenost 2 m jsou častěji EME, rozptyl meteor nebo nesprávné lokátory než tropo.
- od 1.3.4, nepočítejte stanice "do paprsku": kdo za poslední hodinu má alespoň 5 korespondentů nad 1000 km rozložených na ventilátor nejméně 150 °. Ve 2 m je trope směrový; přijímač, který "volný" společně Izrael, Portugalsko, Anglie a Itálie od 2000-3700 km, často s pozitivní signály, má téměř jistě špatně nastavenou frekvenci. V 17-22 / 11 / 2026 I5MZY archiv 72% ubytování ve 2 m nad 1000 km pochází ze stanic, takže 22 / 11 v 10: 53 UTC byly OH6HLH, OZ1ITS, F4LUK, OM8ABC a SP2JLR, a s filtrem nejdelší koridor sestupuje z 2175 km (regole 1.3.3) na 615 km.

Zdroje: cluster spot (not AS / ACS), PSKReporter via MQTT, its QSO and WSJT-X / JTDX / MSHV decodings, and APRS jumps in 2 m and LoRa 433 / 868 MHz.

2 m a LoRa. Program čte APRS-IS pouze v read- only (rotate.aprs2.net, filtr na Evropě) a extrahuje rádiové skoky mezi známými polohovými stanicemi (sorgente → digipeater → Igate), jako je VHF mapa DXView a APPS PropView (RF-YVY, Apache 2.0). Tři pásma mají hodnotu: 2 m, LoRa 433 MHz (koridory pásma 432) a LoRa 868 MHz (corridoi, které se počítají pro 1296: vedení, které přináší 868 MHz také nejvyšší frekvence). Kapela pochází z LoRa tocall (aprs- deviceid list) a z frekvencí nebo slov v komentáři; skok je jen mezi dvěma stanicemi stejné kapely.

Jemným bodem je aktivní opakovače: signál přijatý na 500 km může být doručen se dvěma nebo třemi přenosy digipeater. Skok se tedy počítá pouze v případě, že je mezi dvěma skutečnými a po sobě jdoucími vysílači:

- cesta musí být vysledována: každý digipeater, který byl předán, dal své jméno, a skok se měří mezi jednou a další; alias použitý bez názvu (W IDE1 *, W IDE2 *, regionální alias) přeruší řetězec a tento úsek se nepočítá;
- skryté přenosy: program si pamatuje cestu, kterou se každý zdroj obvykle ptá (např. W IDE2 -2). Pokud je balíček dodáván se skokem do menší (W IDE2 -1) a žádné jméno to nevysvětluje, digipeater ji přeposlal bez sledování a balíček je vyřazen; zdroje nikdy neviděl předtím se nepočítají;
- digipeater uprostřed silnice: pokud mezi dvěma stanicemi, více či méně na silnici, je digipeater stejného aktivního pásma v posledních třech hodinách, skok je vyřazen, protože by mohl být znovu přenášen (to je zejména pro digipeater LoRa, který předává bez změny cesty);
- non-radio balíčky (qAC, qAX, TCPIP), ty provozované na internetu na rádiu (třetí strany), HF a různé frekvence, stanice ve výšce nad 1500 m (palloni, letadlo) a skoky nad 1500 km.

Účinek na mapu (od 1.3.4). Tahá pouze oblast, kde platí minimální zaručená hodnota: pásmo ± 60 km kolem střední části každé trasy (od 15% do 85% její délky), nikoli na stanicích na koncích, kde potrubí nemusí být. Pásy všech koridorů jsou taženy dohromady, v neutrální barvě (text) a poloprůhledné: překrývání nesedí, a barva neplete s fialovou stupnice (21 dB a dále). Štítky (rysy, průměrná vzdálenost, minimální zisk) se navzájem skenují, s myší na štítku se objevují cesty chodby.

V oblasti a pouze v blízkosti aktuálního času (± 90 minut), předpověď nespadá pod minimum. Důvodem je, že pokud stanice opravdu projdou, je tam propagace, ať už říká model počasí cokoliv.

Protože krátké trasy jsou za málo (od 1.3.4). V 2 m, mezi 300 a 600 km, dvě dobré stanice (asi sto wattů, antény, FT8 nebo CW) připojit téměř každý den i s normální atmosférou, pro troposcatter: signál rozptýlený malými nesrovnalostmi vzduchu ve společném objemu obou svazků. Tři nebo čtyři místa na 500 km říkají, že kapela funguje jako obvykle, ne že by tam byl otvor: GEKO Magazine n. 69 ( IU3QEZ) odhaduje pro troposcatter s 2 × 10 proudícími prvky cca 440 km v SSB QRP, 510 km v SSB s 50 W, 600 km v CW QRP a 640 km v SSB s 500 W (zdroje, jedenáct kolo). Pouze přes 600- 700 km troposcatter již nestačí a řada paralelních spojů skutečně naznačuje vedení nebo vrstvu. Až 1.3.3 koridor 450- 600 km zvýšil předpověď na 9-12 dB a mapa ukázala zlepšení, které tam nebylo.

Počáteční hodnota tedy odpovídá průměrné vzdálenosti tras:

- až 400 km: 3 dB;
- od 400 do 700 km: sůl lineárně od 3 do 10 dB (500 km → 5,3 dB; 600 km → 7,7 dB);
- od 700 do 900 km: od 10 do 16 dB; přes 900 km zbývá do 16 dB.

Pod 7 dB, to je pod začátkem stupnice, minimální zůstává, že: koridor je nakreslena s jeho štítkem, je vidět, že páska funguje, ale barvy mapy se nemění (s rámečkem také ukazuje 3-7 dB šedé tečkované). nad 7 dB se hodnota zváží, půl a půl, s počtem nezávislých tras (3 tahy → 0,25; 6 nebo více → 1) a s mediánem S / N vztahů (− 20 dB → 0.5; 0 dB nebo více → 1; není známo, jako ve shlukových spotech, → 0,75), počínaje 7 dB. Příklady: 3 trasy bez S / N na 500 km → 5,3 dB; 5 tras na 615 km → 7,7 dB; 3 trasy bez S / N na 900 km → 11.5 dB; 3 trasy na − 20 dB na 900 km → 10,4 dB; 6 tras na 0 dB na 900 km → 16 dB.

Minimální vzdálenost zůstává 300 km: zvýšení na 500 km by také odstranilo skutečné krátké otvory (například vedení na Tyrhenian na 400 km, který je uznán předpovědí počasí a velmi silné signály více než jen ze vzdálenosti). V archívu I5MZY, 22 / 11 / 2026 v 10: 53 UTC, s tímto pravidlem a paprskovým filtrem byly delší koridory 615 km (7,7 dB) a 540 km (6,3 dB); s pravidly 1.3.3 dosáhla 2175 km a 16 dB.

Do 1.3.3 platilo minimum na všech obdélnících koridoru a záviselo pouze na vzdálenosti (od 10 dB do 300 km do 16 dB do 900 km): s mnoha dlouhými koridory společně (např. 22 / 11 / 2026, dvanáct koridorů až do 2620 km) pozvedlo téměř celou Evropu na 16 dB.

Výtěžnost křivky již "gocce": menší ostrovy 220 km v obou směrech zůstávají barevné, ale bez uzavřené křivky, ledaže koridor potvrzuje. Vypne se s "Prevision zprostředkovaný spoty."

## 4 ter. Živé senzory: FM-DX webserver a AIS přijímač (od 1.4)

Další dva zdroje skutečných pozorování, které vstupují do koridorů pásma 2 m a do rozsudku o spolehlivosti (§ 10 šedesátých let). Zapnou a vypnou v záložce Detaily.

Webserver FM-DX. Mnoho vzdálených přijímacích nadšenců ve FM (88-108 MHz) udržuje na volném programu FM-DX Webserver internetový přijímač (GPL-3.0); Veřejný seznam je servers.fmdx.org [28]. Každý server říká, na stránce / api, naladěná frekvence, signál v dBf a, pokud dekódoval RDS, vysílač rozpoznán svou databází s vzdáleností a azimutem. Program:

- stáhnout seznam každých 30 minut a mít 60 nejbližších online serverů v rozmezí 1500 km od QTH;
- vyslýchá je postupně po třech minutách s maximální dobou 8 sekund (žádný server neobdrží více než jednu žádost každé 3 minuty);
- udržuje příjem, je-li vysílač 150 km nebo více a signál je nejméně 10 dBf; poloha vysílače je odvozena od polohy serveru, azimutu a vzdálenosti.

FM pásmo je nižší než 2 m, a kanály, které jej nesou, ne vždy přinášejí 144 MHz; ale FM příjem na 300-600 km mimo horizont je téměř vždy tropo (nebo, v létě, E sporadic, že na 100 MHz dosáhne nad 900 km: pro to více než 1500 km pravidlo koridorů žádá dva různé zdroje, § 4 bis). Na mapě jsou recepce fialové linky "FM n km"; v stavové liště se počítá, se seznamem v komiksu.

AIS z vašeho přijímače. Lodě vysílají svou pozici na 161,975 a 162,025 MHz (ITU-R M.1371 [31]), téměř stejnou frekvenci jako 2 m. AIS přijímač (AIS- catcher, rtl- ais, dedikovaný přijímač), který předává fráze NMEA! AIVDM v UDP na portu 10110 transformuje každou loď do čidla vedení: program dekóduje polohové zprávy (typy 1, 2, 3, 4, 18, 19 a 21, dokonce i na několika fragmentech) a porovnává vzdálenost s normálním rádiovým horizontem

```math
d_h = 4{,}12\,(\sqrt{h_{nave}} + \sqrt{h_{rx}})\ \text{km}
```

s výškou antény lodi odhadnutou z typu (20 m pro nákladní lodě, 30 m pro cestující, 6 m pro rybolov a zábavu, 10 m pro ostatní) a anténou přijímače (základní 10 m). Loď více než 1,5 krát horizont a více než 50 km je příjem potrubím: objeví se na mapě jako modrá čára z QTH s řevem "AIS n km". Je to stejná myšlenka jako studie CSIRO [10], která k ověření predikce plynovodů GFS použila recepcí AIS 162 MHz. Je vypnutý základní: slouží správnému přijímači.

## 5. Volitelné čáry a srovnání s tlakem a inverzí

Křivky zisku: čáry na 7, 10, 13, 16, 18,5 a 21 dB, hrany stupnice, tažené na poli hladké s metodou pochodových čtverců, s štítkem v dB.

Isobare: tlak na úrovni moře každé 4 od stejné GFS, až po hodinu předpovědi, s centry H a L.

Tepelná inverze: křivky pokryté tím, kolik vzduchu je při 925 nebo 850 hPa teplejší než při 2 m, při + 1, + 3, + 5 a + 8 ° C.

Pod ovládacími prvky se přímka shoduje s tlakem a s opakem na viditelné ploše: Pearsonův korelační koeficient, -1 až + 1. Kanály se často rodí v anticyklonech (vzduch, který klesá ohřívá a vytváří opak), takže pozitivní dohoda s tlakem je normální, ale uvnitř stejného anticyklonu zisk mění hodně mezi mořem, pobřežím a horami. Samotný opak nestačí: ohýbat vlny pouze v případě, že nad vzduchem je také sušička [5]. Linie slouží k tomu, aby nyní, který ze dvou nejlepších vysvětluje barevné oblasti. Stejné srovnání je uvedeno v rejstříku.

## 6. Potenciální potrubí: dvojí výpočet

Úroveň GFS se pohybuje od 200 do 1000 m: tenká inverze se šíří a model nevidí potrubí. Pokud mezi dvěma úrovněmi překročí pokles N normální (40 N / km), vzduch je stabilní (teplotní gradient pod 3 K / km) a výše je suchý (specifický poměr vlhkosti pod 0,8), program zvažuje potenciální potrubí a počítá jej dvěma způsoby:

- Výpočet A: všechen koncentrovaný nadbytečný pokles za 50 m.
- Výpočet B: stejný pokles rozdělený na typickou tloušťku potrubí této oblasti, převzatý z klimatických map ITU-R P.453 [4] (na zemi nebo ve výškách).

Pro deficit a pro maximální zachycenou vlnovou délku platí nejnižší hodnota: je to obezřetný odhad.

ITU- R P.453 data jsou © ITU a mohou být použita pro osobní i nekomerční účely citací zdroje, ale nelze je přerozdělit: proto nejsem v balíčku programu. Pro zapnutí výpočtu B stačí stáhnout mapy P.453 z ITU v záložce: program po zobrazení podmínek použití, stáhnout archiv potrubí z ITU stránky (upgrade _ and _ surface _ duct.zip) pro osobní použití uživatele, extrahovat soubory v Data / P453 a zapnout výpočet bez restartování. Alternativně si stáhněte z digitální produktové stránky Studijní skupiny ITU-R 3 (itu.int, ITU-R Studijní skupina 3, Digitální produkty, hlas P.453, Povrchová data a nadsazená data) a umístěte sedm S _ OCCUR. TXT, S _ THICK.TXT, S _ STREN. TXT, E _ OCCUR.TXT, E _ base.T Bez souborů program používá pouze výpočet A a zapisuje jej do karty a do registru. Poměr mezi deficitem A a typickou intenzitou P.453 je anomálie: Kolikrát je dnešní ropovod silnější než obvykle. P453

## 7. Stanice a lokátory

Domácí může mít 4, 6, 8 nebo 10 znaků. S 8 nebo 10 znaky pozice je přesná a podíl půdy pochází z Copernicus GLO-30: výška antény je dost ze země. Se 4 nebo 6 znaky náměstí je příliš velký: je lepší napsat kvótu antény na moři; pokud 0 program zůstává odhad s půdou SRTM ve středu náměstí plus výška ze země a signalizuje to ve výsledcích. Pro odpovídající je výchozí výška 3 m od země; s přílivem moře 0 se používá půda SRTM plus výška.

## 8. Půda, difrakce a troposcatter

Profil trasy je vzorkován na Copernicus GLO-30 každých 30 m (více než 120 km). Ekvivalentní poloměr Země je k = 157 / (157 + ΔN), s ΔN pokles N v prvním km od profilů GFS podél trasy [6, 7].

Reliéfy v blízkosti stanic (od 1.3.2, autor kritérium). P.452 platí horizont úhel lineárně, 0,573 dB na mrad: reliéf o několik desítek metrů na několik set metrů od antény stojí desítky dB, mnohem více, než je pozorováno. Z tohoto důvodu mohou reliéfy v okruhu 20 km od každé stanice sčítat až 6 dB: úhel horizontu je omezen na úhel nejvzdálenějších reliéfů plus 10,5 mrad. Platí pro vaši stanici a pro korespondenta. Řádek "Blízko reliéfů" říká, kolik DB vzlétlo.

Zdroj půdy (od 1.3.2). Kvóty pocházejí od společnosti Copernicus DEM GLO-30 [3]:1 druhý oblouk, moře je0 a pobřeží je čisté. Až do 1.3.1, program používal krabičky "Skadi" Terrain Tiles AWS. Blízko pobřeží tyto čtverce mix SSRTM s nízkou rozlišení mořského dna data a šíří podíl ostrova na moři: kolem Capri vytvořit ducha prsten 80-100 m mezi 0,5 až1 km od pobřeží. Dne 21. listopadu2026 IC8TEM oznámil neexistující horizont na severozápad; bodové srovnání s Copernicus potvrdil (94 m na moři na severozápad Marina Grande vs.0). Tento falešný horizont + 2,6 ° stojí asi26 dB troposcatteru pobřežních stanic. Skadi tabule zůstávají jen jako rezerva, pokud Copernicus nereaguje. Rámy Copernicus váží 15-30 MB a stahují se jednou; ty pouze moře neexistují a mají hodnotu0.

- Volný prostor: Lfs = 92,44 + 20 log f (GHz) + 20 log d (km) [8].
- Difraction: Bullington metoda na reálném profilu plus ITU-R P.452-16 § 4.2 [6] delta-Bullington sférická korekce, s ITU-R P.526 [9] sférická ztráta.
- První Fresnelova zóna: poloměr Ø (λ · d1 · d2 / d); volná, pokud půda zanechává nejméně 0,6 poloměru. Pro každou překážku je zobrazen parametr [9] Knife Board V a J (VV)..
- Při návrhu profilu je přímý poloměr zelený v optickém a červeném pohledu, pokud prochází pod zemí nebo pod zakřivení země; červené tečky jsou překážky, které napadají první oblast Fresnelu (fialová tečkovaná čára). Na moři "červený bod" ve výšce 0 m není hora: je to moře stoupající pro zakřivení. Procházení s myší na poloměru nebo na místě se objeví komiks s vysvětlením a hodnotami.
- Když reliéf v blízkosti antény zavře směr (např. horizont desítek stupňů), difrakce a troposcatter způsobí ztráty stovek dB: nad 300 dB program zapíše "> 300 dB" a "směr uzavřen sousední překážkou", signál "< − 230 dB" a poměry S / N "− 60 dB". V takovém případě je vhodné se podívat na "Doporučené rostliny" a "Via monte".
- Troposcatter: ITU-R P.452-16 eq. 45 až 50% času [6], s úhlovou vzdáleností od horizontů a N0 od profilů.
- Bez tropa: součet pravomocí difrakce a troposcatteru, nikdy pod svobodným prostorem.

## 8 bis. Troposcatter: čtyři modely a nejistota

Vedle P.452 program počítá troposcatter se třemi dalšími modely, stejně jako program TropoLink [16], a ukazuje jejich rozdíl jako nejistotu:

- NBS TN101 § 9 [25] (od 1.3.5), medián: L = 30 log f − 20 log d + F (θd) − F0 + H0 + Lgp. F (θd) je útlumová funkce přílohy III v jejích analytických formách (Ns 301) s korekcí na Ns, H0 na funkci přírůstku kmitočtu § 9.2: při 144 MHz, s nízkými anténami v vlnových délkách, reflexe ze země částečně vymazává pole v dolní části společného objemu a ztráty soli 3-10 dB. Ns je ten z hodin GFS profily, efektivní horizonty a výšky pocházejí z profilu Copernicus, Lgp = 0,07 · e ^ (0,055 (Gt + Gr)) je otevírací ztráta spoje. Je to stejná forma, jakou ITM používá ve svém interiéru, ale s reaktivitou dneška místo klimatu. Zkouší ploché úseky při 144 MHz s anténami ve vzdálenosti 20 m: do 2-4 dB od ITM mezi 150 a 900 km a asi 2 dB ve zjednodušené formě 9.2b TN101, která nemá H0.

- ITU- R P.617-5 [17] L = F + 22 log f + 35 log θ + 17 log d + Lc, s f in MHz, θ in mrad, d in km, F = 0,18 · N0 · e ^ (− hs / 7,35) − 0,23 · ΔN a Lc = 0,07 · e ^ (0,055 (Gt + Gr)). N0 a ΔN jsou obvykle převzaty z klimatických map ITU; zde používáme každou hodinu profily GFS ( N0 · e ^ (− hs / 7,35) je reaktivita k zemi, ΔN pokles v prvním km), takže také troposcatter následuje čas.
- RSGB VHF / UHF Handbook Formule, DL2FZN [18]: L = Lfs + 24,44 + 4,283 ln f + 0,06656 d. Sklon se vzdáleností je téměř roven sklonu PROPBER (0,067 dB / km).

Společný objem (od 1.3.1): je bod, kde je poloměr obou stanic překročen, každý nakloněn k jeho horizontálnímu úhlu, na Zemi s rovnocenným poloměrem ae = k · 6371 km. Z TX poloměr stoupá jako z = hTX + x · θt + x2 / (2 · ae); Vzdálenost x = (hRX − hTX + d ^ θr + d2 / (2 · ae) / θ, s θ = d / ae + θt + θr. S plochými horizonty a anténami ve stejné kvótě má základna hodnotu d2 / (8 · ae): asi 1,3 km až 300 km, 3,7 km až 500 km, 9,4 km až 800 km. Čím větší je objem, tím více je vzduch suchý a regenerovaný a tím méně se šíří: z tohoto důvodu více než 800- 1000 km troposcatter vyčerpá. Ve výsledcích jsou podíl a pozice základny, reaktivita, gradient a vlhkost GFS v této nadmořské výšce, a ekvivalentní vzdálenost Bray (QST, listopad 1961): každý extra nebo méně horizont je stojí asi 111 km daleko. Disciplinární reference: prezentace "Za horizontem VHF" od IU3QEZ (GEKO Magazine n. 69).

Přímka Troposcatter, další modely ukazují dvě ztráty a nejistotu jako polovinu rozdílu mezi nejoptimističtějším modelem a nejpesimističtějším. Jsou to pouze srovnání: ztráta bez tropa zůstává ztráta P.452.

## 8 ter. odpolední troposkok nad reliéfy (od 1.3.4)

IK3XTV, z Thiene ( JN55, s Alpy na 10 km), pracuje v létě odpoledne jižní Německo na 400- 500 km, bez dohledu, se silnými signály, ale s rychlým a hlubokým evanescence; autor je přiřadil "konvekční buňky sladěné jako vlak čoček". Pozorování je potvrzeno; vysvětlení přijaté v programu je další, fyzicky pevnější.

Odpoledne se nad reliéfy vznáší slunce a mezní vrstva se zvyšuje na 2-3 km. Na jeho vrcholu vlhký vzduch níže splňuje suchý vzduch výše: tam turbulence indexu lomu (velikost, která řídí troposcatter) je maximální. Když tento vrchol dosáhne společného objemu trasy, který na 400- 500 km je asi 2-3 km, troposcatter je posílen. To vysvětluje odpolední čas, vysoký tlak (tichá obloha, silné topení) a rychlé vyhřívání, typické pro difuzi, a ne pro kanály. Konvexní buňky nepůsobí jako čočky: vytvářejí turbulence a signál šíří turbulence v horní části přepracované vrstvy.

Důsledky pro předpovědi čtení:

- na zemi je první odpoledne nejhorší pro dolní kanály a vrstvy, ale ne pro troposkoka za horami; tyto dvě věci by měly být čteny odděleně;
- program již má dvě údaje, které slouží, výšku mezní vrstvy poskytované modelem a podíl společného objemu trasy (zaregistrujte "společný objem" ve výsledcích): když jsou blízko, odpolední horská lahůdka může jít lépe než výpočet v normální atmosféře;
- od 1.3.4 se bere v úvahu výpočet bodového bodu: je-li společný objem na zemi, podél trasy jsou reliéfy nejméně 800 m, slunce je nad 15 ° a mezní vrstva přesahuje 800 m, a horní část vrstvy přetváření (model rychlost půdy + mezní výška vrstvy) je do 500 m od základu společného objemu, troposcatter zvyšuje 3 dB; mezi 500 a 1000 m lineární zisk klesá na nulu. Řádek "Troposcatter pomeridiano." Se sněhem podél trati (alespoň 10 cm na zemi podle modelu, s proměnnou sněhu _ hloubka Open- Meteo nebo SNOD GFS od NOMADS) se výztuž nepoužije: na sněhu zůstává půda studená a přetvarovaná vrstva neroste. Hodnota 3 dB je provizorní a musí být kalibrována reálnými linkami; vypne se s korekcí počasí.

Zdroje: připomínky F. Egano IK3XTV (Radio Rivista e www.qsl.net / ik3xtv ", Propagazione sulle2 metri", "Celle convettive", "Troposcatter over the Alps"), zdroje §13.

## 9. Zakládací vrstva podél trasy

Je to srdce výpočtu signálu. Profily GFS jsou přijímány podél trasy asi 25 km. Pro každou vrstvu tlaku, nejdelší úsek stejné vrstvy se nachází na po sobě jdoucích bodech; počítat jako potrubí pouze tlusté vrstvy nejméně 40 m [10].

- kontinuita: od jaké km do jaké km a zlomek trasy;
- horní a spodní část a sklon v m / km;
- půda uvnitř vrstvy: km, kde se hory zvedají nad základnou;
- Pokrytá půda: na moři je vedení téměř vždy, rozhoduje o úseku země [10]. Pokud půda překročí 10% trasy, použije se minimální zlomek mezi kontinuitou a pokrytím země.

Zisk tropu je zisk vrstvy pro použitý zlomek. Pokud je více než polovina profilů subrefrakterní, zisk je negativní.

## 9 bis. Opravy počasí: stabilita, poddanost, stávka

Od 1.3.0 profil také dává tři meteorologické informace, které lze vidět v registru (regave "stabilita (Misme)"), v Meteo řádku podél trasy výsledků a, s Segni meteo box, na mapě.

- Stabilita Misme [5, kapitola 6]. Misme váže pole za horizontem na složený parametr: M = a · (ge − 40 + b · ΔW), a = 0,5 dB na N / km, odpovídající gradient ge a tepelnou stabilitu ΔW, práci na zvýšení vzduchu o 1 km. Program počítá ΔW = g · ∞ (θ − θ0) / θ0 dz mezi půdou a 1 km nad ní, s potenciální teplotou θ; standardní atmosféra je asi 56 J / kg. Výraz přidaný k zisku je 0,07 dB pro J / kg nad rámec normy, omezený mezi − 4 a + 6 dB, a počítá se pouze v případě, že žádný vodič zachytí pásmo (gradient je již v Bean a Dutton indexu). Kalibrace je dočasná: musí být ověřena poměry (odstavec 14).
- Dolní a horní vzduch [19]. Moler a Holden, na trasách 125-305 km k moři na1365 a9380 MHz, našli nejvyšší signál asi 2,5 dB v průměru se vzduchu stoupání, a v případě posílení asi20 dB po dobu3 hodin na začátku ponoru, následuje pokles o22 dB, když se vzduch vrátil ke konvergenci. GFS dává vertikální rychlost850 a700 hPa. Pokud na obou úrovních klesne vzduch o více než1 cm / s a v potenciálním indexu lomu K dojde ke zpětnému nebo prudkému poklesu (N se hlásilo agregativně na1000 hPa, což zvýrazňuje suchý a teplý vzduch klesl z horní části [5]), přidá se 1,5 dB; pokud se vzduch zvýší o více než5 cm / s, pokud vzlétne.2.
- [20] Scirocco. Horký a suchý vzduch ze Sahary přes chladnější mořský vzduch dává velmi silné inverze z východního Středomoří na Gibraltar. Značka S se objevuje tam, kde při 850 hPa vítr pochází ze 100- 200 ° s nejméně 7 m / s a teplota je nejméně 16 ° C, na Středozemním moři. Je to jen náznak: zisk rozhoduje o vrstvách profilu.

Bez vedení, které zachytí pásmo celkový zisk nepřesahuje 16 dB, konec žluté: korekce činí silnější vrstvy jasnější, ale neprojdou je kanály (červená a fialová zůstávají v kanálcích).

- Perzistence (od 1.3.1). Největší otvory přicházejí po dobrých vysokotlakých dnech (VKV / UHF DX Book; Biancomano a Leyshon 1998, pravidlo 12 hodin; zprávy 29 / 12 / 2021-2 / 1 / 2022 rostou od prvního do čtvrtého dne). Pro každou buňku program počítá hodiny předchozích 24 s přírůstkem nejméně 13 dB a přidává se do poměru 2 dB, pouze pokud je zisk již nejméně 10 dB.

Jak číst značky na mapě (Změnit povětrnostní znaky; od 1.3.2 symbol je velký double a prochází s myší přes značku se objeví komiks s vysvětlením a hodnotami buňky):

| Podpis | Barva | Co to znamená? | Vliv na tropu |
|---|---|---|---|
|  ↓  | zelený | podsidence: vzduchové kapky (anticyclon), ohřívá pro kompresi a sucho, posiluje zadní a udržuje pevné | příznivý, + 1,5 dB |
|  ↑  | červená | do kopce vzduch (přední, nízký tlak, konvekce): míchá nízké vrstvy a přestávky a kanály | nepříznivé, − 2 dB |
|  S  | oranžová | Sirup: horký a suchý vzduch ze Sahary nad čerstvým mořským vzduchem | příznivé vodítko, žádná korekce: rozhodnout vrstvy |

Oblast plná zeleného ↓ pod anticyclone je klasické znamení tvrdého otevření; Red ↑ šipky podél linie říkají, že vrstvy se lámou, i když barva buňky je stále dobrá.

Počasí Opravy vypne tyto opravy vidět model 1.2.

Validace (odstavec 14): na spojích 593 na 144 MHz mezi 29. prosincem 2021 a 2. lednem 2022 ve srovnání se stejnými trasami a o dva týdny později je index oddělování (0,5 = žádný, 1 = perfektní) 0,79 bez oprav a 0,91 s opravami (před střechou při 16 dB). Stejné srovnání ukazuje hlavní limit: vedení na moři v nízké nadmořské výšce, které v těchto dnech přinesl znamení mezi Kampánií a Ligurií, není vidět na úrovních GFS.

## 9 ter. Novinky 1.3.2: dva odhady, strop, vysoký tlak, moře, stanice, hřebeny

Rozumný a agresivní odhad. Přínos z gradientu roste z 0 dB na limit normálu až na 10 dB na práh lomu. Ve vrstvě 1000- 850 je obezřetným prahem Bean a Dutton (100 N / km); agresivním prahem je klasický prahová hodnota Super- Refraction 79 N / km (k doposud 2; P.453 používá − 100 N / km v prvních 100 m), který s k50 = 157 / (157 − ΔN) je k doposud 2 (k = 2,5 odpovídá asi 94 N / km). Výpočet bodu bodu ukazuje, že obě ztráty se rovnají, s vodičem, který zachycuje pásmo shoduje. Mapa používá opatrnost.

Podkroví 2500 m. Silnice a kanály jsou hledány až 2500 m nad zemí: čím vyšší antény amatérských rádií je nevyužívají. S 1500 m došlo ke ztrátě validace bodu oddělení a 4% spojů nad 16 dB, protože v zimě se inverze z podsidence často pohybuje mezi 1500 a 2500 m.

Vysoký tlak se zvýšil. + 1 dB, kde alespoň 7 z 9 buněk v okolí snížilo tlak na moře o 1020 hPa nebo více a vítr je slabý v nízkých vrstvách (10 m pod 5 m / s, 850 hPa pod 8 m / s), s výjimkou vzestupných pohybů.

Mořská váha. Při výpočtu bodu je zisk vrstev násoben 0,8 + 0,4 · μ1, kde μ1 je faktorem ITU-R P.452 (eq. 51) na nejdelším kontinuálním úseku země: 1 na otevřeném moři, malý s dlouhými úseky půdy.

Stanice a vedení. Pokud vedení, které zachycuje pásmo, pokrývá pouze část vedení, je podíl každé antény srovnáván se základnou a horní částí vedení na začátku a na konci krytého úseku; zisk se vynásobí 0,5 + 0,5 · · · (e ^ (− ΔhTX /300) · e ^ (− ΔhRX /300)), s Δh vzdálenost v metrech z kanálu. S potrubím na téměř celé trase je spojení §10.

Layer a Ridges. Pokud horní část okrajové vrstvy GFS (pozemní rychlost modelu nejvíce ohraničené _ vrstvy _ výšky) překročí hřebeny trasy (maximální prostory nad 300 m, pokud nejvyšší přesahuje 500 m), vrstva prochází přes hory bez přerušení: + 1,5 dB pro zlomek krytých hřebenů. Je to pozorování IT9GSF na otvory ze Sicílie do Německa s příznivým Tyrhenian.

Úrovně blízko k zemi na moři. Se zdrojem NOMADS program také stahuje teplotu povrchu (na moři je SST), teplotu a vlhkost na 80 m a ve vrstvě 30-0 hPa nad zemí. Na mořích bodů (modelová rychlost pod 5 m) jsou tyto úrovně přidány do profilu, pod prvním tlakem, vidět potrubí na 100- 200 m, že tlakové úrovně neřeší. Na zemi se nepoužívají vrstvy noci a dne. Open- Meteo dává jen teplotu povrchu.

Odpařovací potrubí. Od SST, od teploty a vlhkosti do 2 m je odhadnuta δ výška odpařovacího potrubí s neutrálním logaritmickým profilem (jako v modelu Paulus a Jeske, bez korekce stability): vzduch do vlasů vody je nasycen do SST (98% pro sůl), proto

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

omezený mezi 0 a 40 m. K zachycení 144 MHz by to trvalo asi 180 m (Kerr), pak na 144 MHz odpařovací potrubí posiluje málo; od 432 MHz nahoru, s nízkými anténami na moři, hodně (odstavec 11 bis). Zadejte parabolickou rovnici a v řádku Opravy 1.3.2.

Čas dne (návštěva). Program počítá výšku slunce na každém bodě. Ze dne na den na zemi, se sluncem nad 15 ° a mezní vrstvou nad 800 m nebo větrem nad 5 m / s, varuje, že pozemní kanály jsou přetvarovány; v noci, se sluncem pod − 3 °, vítr pod 3 m / s a mezní vrstva pod 300 m, varuje, že s klidovou oblohou je možné provádět ozařování, které padá po východu slunce. Ne na moři. Pro tuto chvíli jsou vidět, ne čísla: Prosinec validace nemá dostatek letních dnů kalibrovat. To jsou dva body, které autor považuje za důležité, z každodenní pozorování.

Normální profil CRPL (alternativa). Místo 40 N / km stanovil normální pokles následuje exponenciální referenční atmosféra CRPL: N (h) = Ns · e ^ (− c · h), s − ΔN v prvním km = 7,32 · e ^ (0,005577 · Ns) (Bean a Thayer 1959). S vlhkým vzduchem (Ns high) je normální strmější a slouží silnější gradient, aby zisk; ve výšce je sladší. Ukázalo se, že špatné, dokud potvrzení říká, že to pomáhá.

## 9 kvater. kontrastní vzduch-moře a nízké moře: Jadran (od 1.3.3)

Rádiové vlny necítí mořské dno: cítí kontrast vzduchu a vodní plochy. Hloubka se počítá, protože rozhoduje, jak rychle teplota moře následuje po teplotě vzduchu.

Jadran je nízké moře. Průměrná hloubka je asi 250 m, ale severní povodí málokdy přesáhne 100 m a záliv Trieste zůstává pod 25 m; pouze na jih klesá přes 1200 m. Vrstva vody tak tenká, že se za pár dní zahřeje a ochladí, Tyrhéňan za týdny. Existují tři typické situace:

1. Jaro a časné léto, příznivé. Moře je stále studené, dokonce i pro sladkou vodu řek Alpine a Po; to přichází horký a suchý vzduch z pláně nebo Balkánu. Horký vzduch nad studenou vodou vytváří zvrat na moři (poradenský plynovod): je klasickým stavem otevření mezi Itálií, Slovinskem a Chorvatskem.
2. Podzim a zima s Bora, nepříznivé. Studený vzduch nad teplejším mořem: nízká vrstva se stává nestabilní, lisy a potrubí se rozbije.
3. Po Bora, šance. Jen proto, že je moře nízko, Bora ochladí za pár dní. Pokud pak zadáte anticyclone s horkým vzduchem, vzduch-moře kontrast je silný a potrubí je robustnější než obvykle.

Jadran je také uzavřený kanál mezi Apeniny a Dinari, méně než 200 km široký: mořská vrstva zůstává omezena, a trasy podél osy (Puglia- Venezia- Trieste, asi 700 km) zůstávají na moři, nejlepší podmínkou pro dlouhé potrubí.

Co program počítá. Na mořích, s povětrnostními úpravami a krabičkou "Contrasto aria- mare":

| Stav | Oprava |
| --- | --- |
| potenciální teplota 925 hPa (nebo 950) nad SST 4 K | _ |
| nad SST 7 K | + 2 dB |
| s vlhkostí vzduchu na 925 hPa (nad 80%) | − 0,5 dB u těchto bonusů |
| chlazené moře 1,5 ° C nebo více během 72 hodin před, s příznivým kontrastem | _ |
| teplejší vzduchové moře při 2 m 2 ° C (míchání) | − 1, 5 dB |
| teplejší moře 4 ° C | − 3 dB |

Celková korekce zůstává mezi − 3 a + 3 dB a bez vedení zůstává zisk pod žlutou střechou. Prahové hodnoty jsou dočasné: ve standardní atmosféře potenciální teplota 925 hPa převyšuje teplotu moře asi 2-3 K, pak 4 K již indikuje opak.

Vzpomínka na SST. Modelky udržují teplotu moře během předpovědi téměř stálou. Nedávný pokles je odvozen z analýzy tří dnů před (Open- Meteo, pouze povrchová teplota, pouze mořské buňky): maximálně 72 hodin před minus aktuální hodnotu. Pokud služba nereaguje, bonus za paměť se nepoužije.

Meze. SST modelů pod pobřežím a v horním Jadranu je hrubá; například studené pero Po je špatně řešeno. Batimetrie nezadává výpočet: zdroje vždy dávají SST, který již shrnuje účinek mořského dna. Kontrast vzduchu-moře se překrývá částečně na konci stability Misme (§ 9 bis); pro to bonusy jsou malé. Prahové hodnoty půjdou s chodbami místa a QSO přes Jadran.

## 10. Vedení na celé trase

Pokud vrstva zachytí pásmo na nejméně 80% použité frakce a stanice nejsou v dohledu, ztráta v potrubí se také vypočítá: volný prostor, plus spojení antén (3 dB, pokud jsou uvnitř vedení, více pokud jsou nad nebo pod), plus zbytkový útlum γd = 5 · 10 − 5 · ae · f ^ (1 / 3) dB / mrad ITU-R P.452 eq. 49 [6]. Pokud je nižší než ztráta bez tropo se stane očekávanou ztrátou.

Pásmo zůstává v potrubí pouze v případě, že jeho vlnová délka nepřevyšuje λmax = 2 ▼ 2 · (2 / 3) · ↑ (ΔM · 10 − 6) · d (Kerr, eq. 17 [11]). K tomu může stejná vrstva lapit 23 cm a 70 cm, ale ne 2 m. Nicméně, limit není net: Kerr varuje, že i vlny několikrát déle trpí hodně vedení, a Hepburn a VHF / UHF DX Book mluví o "špičaté" hrany a "odpuštění" kanály. Od 1.3.1 program používá měkké vedení: je-li λmax mezi λmax a 3 · λmax, zisk postupně přechází z 15 dB (λ = 3 · λmax) na plné vedení zisk (λ = λmax), s faktorem s = (λmax / λ − 1 / 3) / (2 / 3). V tom případě jsou barvy potrubí povoleny. Logaritmus a Meteo podél trasy ukazují body s měkkým kanálem.

## 10 bis. Via Monte

Když je korespondent za obzorem uzavřen, program hledá nejlepší směr, jak zaměřit antény: osvětlený svah šíří signál jako radarový cíl. To dělá kruh horizontu každé 5. (s výjimkou ± 15 ° kolem korespondenta) a v každém směru se osvětlený horizont bod, přes 100 m a mezi 1 až 60 km. Rychlý odhad (volný prostor, bitaktická konverze a 10 dB pro každou úroveň horizontu z difuzoru na korespondenta) vybere 4 nejslibnější, alespoň 15 ° od sebe; pro každého:

- treat 1, stanice → reliéf, ve volném prostoru;
- zachází s 2, reliéf → korespondent, vypočtený jako kdyby reliéf přenášený z 10 m nad vrcholem, se stejnou předpovědí (bez parabolické rovnice);
- konverze s bistatickou radarovou rovnicí, s ∞ = σ0 · A, σ0 = − 15 dB (hora v VKV / UHF) a A plocha osvětlená paprskem R1 (maximálně 20 km2):

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

"Doporučený bod" linka označuje azimut nejlepší trasy, pokud to stojí méně než přímé. Při 144 MHz, s úlevou na 20 km a yagi z 15 °, stojí horská silnice asi o 45 dB více než jen 2 trasy: to se stane pouze tehdy, když je přímý je uzavřen sousedními překážkami. Odhad je hrubý (± 10 dB), zejména pro σ0.


Bez ztráty na hoře (možnost, od 1.3.2). S "Via monte senza perdite up al monte" box, pád mezi stanicí a difrakční bod je považován za null: je jako uvedení stanice na vrchol (anténa na 10 m nad bodem, stejný výkon, anténa a kabel). Obálka pak vybere bod s nejvíce volným horizontem směrem k korespondentovi (v paritě, nejvyšší) a výsledek ukazuje cestu z bodu do korespondenta, se signálem a S / N, vedle celku s difuzí. Je to optimistický limit: říká, pokud se směr otevře, když hora zvedne signál dobře, nebo kam jít přenosný. Každý bod je uveden s ten- znak.

## 10 ter. Vstup do vzdáleného vedení

Pokud vedení, které chytá kapelu, začíná daleko za horizontem stanice, signál musí dorazit. Program přináší signál do výchozího bodu, uprostřed vedení, pro difrakci: podél profilu Copernicus GLO-30 hledá okraj s nejvyšším Fresnelovým parametrem (ITU-R P.526), s otokem ekvivalentní půdy,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

větší útlum způsobený zakřivením horní části (zaoblená překážka, P.526-16 § 4.2) s poloměrem R odhadovaným z půdy Copernicus GLO-30 na ± 1 km od okraje,

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

(pro m · n > 4 je formulář 34b doporučení). Barsis (NBS, 1963) měřeno na třech dlouhých trasách za horou (Colorado, Aljaška, Evropa) průměrné ztráty 10- 20 dB vyšší než ideální okraj Fresnel- Kirchhoff: zaokrouhlení vysvětluje hodně (Barsis a Kirby, 1961: Pikes Peak s poloměrem asi 11.5 km). Výsledek se srovnává s difrakce na hladké sférické půdě; je nejhorší. Potom se signál pohybuje v potrubí: volný prostor na celé trase, zbytková útlum γd P.452 na úseku v potrubí a spojení korespondenta (3 dB uvnitř, 1 dB každých 10 m venku). Pokud je tato ztráta nižší než očekávaná ztráta, stává se mechanismem ("chování (vstup difrakcí)"). Je to autorův předpoklad o italské geografii: s nedalekou překážkou těsně pod čárou, v je negativní a vstup stojí málo, s hřbetní přední náklady 15-30 dB.

## 10 kvaterů. ITM (dlouhozrnná rýže) a výstup z kanálu

Od 1.3.2 program obsahuje nepravidelný Terrain Model 1.2.2 NTIA / ITS (Longley a Rice, ESSA ERL 79-ITS 67, 1968; Hufford, Longley a Kissid, NTIA 82-100, 1982), přeložen do C # oficiálním kódem americké vlády, který je bez práv. Překlad uvádí stejná čísla jako originál: auto compilation srovnává pokaždé s NTIA C + + kódem v pěti zkušebních případech (pod 0,01 dB).

ITM počítá ztrátu v normální atmosféře na skutečném profilu Copernicus GLO-30: vidět s odrazem od země, difrakce (spigol a sférické půdy vážené na drsnosti půdy Δh, to je rozdíl mezi 10. a 90 ° percentil kvót kolem rovné minimální čtverce) a troposcatter. Reaktivita půdy je snížena na průměrnou část trasy, $N_s = N_0\,e^{-h/9460}$, a dává zakřivení ekvivalentní půdy,

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

Program využívá radiové klima zvolené trasy jako v § 10 pohlaví (do 1.3.4 vždy mírné moře, s N0 320 na zemi a 350 na moři), s N0 z průměrné reaktivity místa (půda εr 15, δ 0,005 S / m; moře εr 81, δ 5 S / m), horizontální polarizace a variabilita v čase bez místa (profil je znám). Ukazuje, že ztráta překročila 50% času a ta, kterou máte za 10% a 1% nejlepšího času. ITM nemodeluje kanály: jeho variabilita v čase je statistická, podle klimatu. Slouží jako druhý výpočet základní linie vedle P.452; rozdíl mezi těmito dvěma dává představu o nejistotě.

Druhým použitím je konečný úsek za spojkou, která končí před příslušnou. Na výstupním místě je umístěna virtuální anténa v polovině cesty potrubím a ITM počítá trakt až do odpovídající, jako překročení volného prostoru v 50% času. Celková ztráta je

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,\theta_{condotto} + A_{ITM}(d_{uscita} \to RX)
```

kde A _ TX je spojení antény (3 dB uvnitř, 1 dB každých 10 m mimo anténu), pokud je vodič nastaven v blízkosti, jinak vstupní difrakce ve výši § 10 ter. Je to limit, který si autor dal: kontinuita vrstvy plus, nanejvýš, zbytkové útlum. Pokud je nižší než očekávaná ztráta, stává se mechanismem ("direct (exit and final tract ITM)").

## 10 quinquies. Prohloubení: ostrovy, úžiny a nekontinuita zemského moře

Příklad z Neapolského zálivu (Anacapri, JN70CN) směrem na západní Středomoří ukazuje, kde trasy přes ostrovy:

| Země určení | Azimut | Kde přechází ostrovy | Překážka |
| --- | --- | --- | --- |
| Barcelona |  279°  | severně od Sardinie (Gallura) do 41,05 ° N, mezi 420 a 490 km | asi 30 km jižně od Bocche di Bonifacio; Limbara (1359 m) asi 20 km od trati |
| Bocche di Bonifacio |  282°  | v blízkosti 41,3 ° s. š. | asi 11 km |
| Marseille |  296°  | Střední Korsika při 42,2 ° s. š. | hory do 2700 m: potrubí pod 2 km přerušuje |
| Valencie, Balearics |  260–265°  | Střední Sardinie | vnitřní reliéfy |

Pro Barcelona stačí ukázat na 282 ° místo 279 °, protože osa prochází z úst: 3., uvnitř paprsku normální yagi.

Jsou ústa úzká? Pro nížké kanály, pod pár set metrů, ano. Ostrovy jsou zazděny, a nad zemí, v den, topení prolomí inverze. Úžina sama o sobě je dostatečně široká: asi 11 km proti první oblasti Fresnelu, méně než jeden kilometr široký uprostřed 1000 km trasy ve 144 MHz. Problém je počasí. Mouths jsou zábava pro vítr: Dolní vrstva se zrychluje a spodní vrstva se odstraní. Často průchod je otevřen jako geometrie, ale narušen jako atmosféra. S stabilním anticyklonem a malým větrem může být místo toho dobrý průchod.

Pozemsko-mořské rozdíly. Počítají se. Na tyrhénském pobřeží je přerušeno vedení: signál vychází z vedení (sekce po výstupu, § 10 kvater) a musí se vrátit do jiného vedení na moři Balearics, platit znovu spojení. Proudy ve výšce nad 1500 m přecházejí přes Galluru, aniž by si to uvědomili; pro centrální Korsiku neplatí.

V praxi: pokud trasa překročí horský ostrov, zkuste nějaký stupeň korekce směrem k úseku volného moře, ale nepočítejte na cestu se silným větrem v úzkém.

## 10 pohlaví. TN101 rádiová odchylka klimatu a času (od 1.3.5)

TN101 § 10 [25] popisuje, jak se ztráta trasy za horizontem mění v dlouhém období kolem mediánu, s různými křivkami podle klimatu: funkce Y (q) říká, kolik dB ztráta klesne v q% nejlepší doby. ITM obsahuje tyto křivky pro sedm podnebí. Podnebí se hodně počítá: na moři v mírném mořském klimatu, při 144 MHz, v 10% času vyděláte mnohem více dB než na zemi v kontinentálním klimatu, protože kanály jsou častější.

Program již neopravuje klima: Vybírá si ji pro každou odměnu z průměrného bodu, takže funguje po celém světě a nejen v Evropě. Stáhněte si z Open- Meteo [26] tři roky denních hodnot reanalýzy ERA5 (teplota, vlhkost a tlak půdy, déšť) pro buňku 0,5 ° průměrného bodu, pouze jednou; hodnoty zůstávají v Data / CLIMA. Obnova půdy

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

den za dnem, pak roční průměr a výlet mezi měsíčními průměry, průměrnou vlhkostí a ročním deštěm. Pravidla se řídí popisem podnebí TN101 § 10.2; hranice mezi klimatem a klimatem jsou kritériem autora:

- nad 60 ° zeměpisné šířky: mírné kontinentální ( TN101 nemá žádné křivky pro polární klima);
- deště menší než 250 mm za rok a průměrná vlhkost pod 50%: poušť (typicky 280 Ns);
- do 20 ° od rovníku, Ns nejméně 350 a exkurze do 30: rovníková (Ns typická 360);
- v rozmezí 32 ° a exkurze nejméně 60: subtropické kontinentální, tj. monzuny (typická exkurze 60- 100);
- mezi 10 ° a 32 ° s Ns nejméně 340: subtropické moře (Ns typický 370);
- obě obzory na moři, přes 20 °: moře temperované na moři (to je definice TN101);
- exkurze do 30 u moře (moře na trase nebo průměrná vlhkost nejméně 75%): námořní temperované na zemi;
- jinak mírné kontinentální (typická exkurze 20-40).

N0 pro ITM je průměr snížený na hladinu moře, N0 = Ns · e ^ (h / 9460). Bez sítě použijete rezervní pravidlo pouze se zeměpisnou šířkou a mořským zlomkem a hranice to říká.

Testy s údaji ERA5 2023- 2025: Capri Ns 339 s exkurzí 45 (kontinentální temperovaný na zemi, moře temperament na moři, když obě horizonty jsou na moři); Londýn 327 a 16 Vídeň 319 a 24 Abidjan 381 a 11 (rovníková); Miami 365 a 36 (subtropický manžel); Delhi 337 a 82 (subtropické conconcontinental); Riad 278 a 37, 90 mm déšť (poušť). V Chile: Antofagasta 341 a 19 (subtropický manžel: pobřeží Atacama, s téměř pevným nízkým opakem Pacifiku), Calama 232 na zemi, 2257 m, 14 mm deště (poušť), Valparaíso 331 a 11 (teplotní šev), Punta Arenas 313 a 5 (teplotní šev).

Řádek "Radio Climate ( TN101)" ukazuje klima, důvod, N s polovinou výletu a Y: kolik dB vydělat 10% a 1% času a kolik ztratit v 90%. Je to dlouhodobá statistika a zůstává oddělena od prognózy: očekávaná ztráta vyplývá z dnešní doby s profily GFS, a shrnout dvě věci by počítat dvakrát stejné abnormální šíření.

## Devadesát. Spolehlivost predikce a ověřování s radiozony (od 1.4)

Kontrola radiace. GFS vidí kanály méně dobře, než se zdá: Jeho vertikální rozlišení v blízkosti země je několik desítek metrů, a vrstva obráceně je často tenčí. Chcete-li vědět, jak moc věřit modelu zde a v těchto dnech, program srovnává ji s reálnými rádiovými vlnami nejbližší stanice, z archivu IGRA v2 NOAA [27] (veřejné domény):

- bere ze seznamu stanic nejbližší aktivní v okruhu 600 km (pro Capri, Practice of Mare, ITM00016245);
- Stáhnout soubor aktuálního roku (aktualizace každých několik dní) a udržet spuštění 00 a 12 UTC za posledních 15 dní;
- pro každý start se odvozuje úroveň reaktivity N po úrovni (s vlhkostí nebo depresí rosného bodu), nadmořské výšky úrovní, které je nemají s ipsometrickou rovnicí od výšky stanice, gradient prvního km a nejsilnější vodič pod 3000 m (M, který spadá s podílem nejméně 2 jednotky);
- stáhnout ze služby historických předpovědí Open- Meteo [15] profily GFS stejného bodu ve stejnou dobu a analyzovat je se stejným kódem předpovědi;
- počítá pozorované kanály, které GFS viděl, chybějící ty a falešné alarmy, a průměrnou a čtvercovou odchylku gradientu.

Ověření probíhá v pozadí až do prvního výpočtu léčby, maximálně každých 12 hodin; soubory zůstávají v databázi Data / IGRA a čísla v registru výpočtů. Radiosonde zprávy pocházející ze světové sítě počasí (TEMP) mají pouze standardní a významné úrovně: velmi tenké vedení také uniknout k nim, a srovnání říká především, pokud GFS je velmi špatně v jednom nebo jiném smyslu.

Spolehlivost. Přímka "Pochybnost" dává vysoký, střední nebo nízký úsudek o očekávaném zisku. Není to kalibrovaná pravděpodobnost, která by vyžadovala roky srovnání s reálnými odkazy (§ 14), ale součet bodů o tom, jak nezávislé stopy (autorův kód):

| Adresa | Body |
|---|---|
| Dohoda mezi modely: průměrná odchylka mezi očekávanou ztrátou a P.452 s kanály, parabolická rovnice a mezi základní hodnotou a ITM | do 3 dB + 1; nad 8 dB − 1 |
| Předběžná předpověď | do 24 hodin + 1; více než 72 hodin − 1 |
| Kontinuita zakládací vrstvy při ošetření | nejméně 70% + 1; méně než 30% − 1 |
| Vytrvalost vrstvy v sousedních hodinách | nejméně 70% + 1 |
| Ověření radiosondy | GFS alespoň třikrát ze 4 + 1; méně než jedna ze dvou − 1; chybný gradient větší než 15 N / km − 1 (maximálně − 1) |
| Živé potvrzení na trase (cluster, PSKReporter, APRS, FM-DX), pouze pro aktuální čas | 1-2 páry stanic + 1; 3 nebo více + 2; otvory s očekávaným ziskem pod 2 dB − 1 |

Vysoký ze 3 bodů, průměr od 0 do 2, nízký pod nulou. Živé potvrzení se počítá jako pár různých stanic, ve směru do 20 ° od směru trasy a s průměrným bodem do 150 km od její trati, v poslední hodině a půl. V případě archivů (§ 3 ter) nejsou žádné radiosondy ani živá potvrzení.

## 11. Srovnání s ITU- R P.452

Přímka P.452 používá metodu ITU pro kanály a reflexe na vrstvách (P.452 § 4.5 [6]) s mediánem ΔN GFS namísto klimatologické a p =20% jako kalibrační konstanta, jako ve studii CSIRO [10]. Zahrnuje ztrátu empirického spoje Alf pod 0,5 GHz (cca28 dB až144 MHz, 3.532 Je to pouze srovnání: očekávaná ztráta zůstává ztráta podle odstavce9.

Od 1.4: P.452-18 kompletní a ověřené. Srovnání nyní využívá všechny základní metody P.452-18 [29] (§ 4.1-4.6): volný prostor s plynem a multipath, difrakce s sférickou zemí a Bullington profil na správném hladkém povrchu pro překážky, troposcatter, kanály a reflexe na vrstvách, a jejich kombinaci. ΔN a N0 pocházejí z GFS a z místa místo ITU mapy. Kód byl ověřen s oficiálními testovacími případy studijní skupiny ITU 3 (shromážděné ve projektu Py452 [29]): na 194 případech do 2 GHz bez pohlcení země je průměrná odchylka celkové ztráty 0,06 dB, maximální 0,54 dB; difrakce 0,10 dB v průměru, troposcatter 0,002 dB. Ověření opravilo dvě chyby předchozích verzí: pojem spojení kanálů používal frekvenci v MHz místo v GHz (asi 60 dB příliš mnoho, kompenzované jinde) a difrakce se odchýlila od již omezených hladkých povrchových kvót, s chybami až 15 dB na určitých profilech.

ITM (§ 10 kvater) byl také srovnán s NTIA [30] (point-point): pět částí oficiálního souboru dává stejné hodnoty dB centu.

## 11 bis. Parabolická rovnice (od 1.3.1)

Parabolická rovnice Line je čtvrtý výpočet, pouze srovnání, vyrobené jako profesionální programy pro kanály (PETOOL; wave- propagace rámec M. Lytaev, MIT licence): místo odhadování zisk z vrstev, to posune vlnu podél trasy s split- step metoda Fourierova v M modifikovaný refrakční profil GFS, převzatý z předpovědi bodu blíže k každé vzdálenosti. Úroveň GFS přidává odhadované skoky potenciálních kanálků (výpočet A, 50 m v polovrstvě). Půda Koperník GLO-30 vstupuje kroky (pod zemí pole je null), půda je vodič a paprsek antény je Gaussian s odhadem otevření od zisku. Výpočet vidí vedení, ti, kteří prohrají, difrakci a spojení antén; On nevidí troposcatter, který se přidává odděleně. Další je "profil šek": stejné se standardní atmosférou méně ten s očekávaným profilem.

Od 1.3.2:

- propagátor je v širokém úhlu, s přesnou fází volného prostoru:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- dno moře a pobřežní pláň v nulové nadmořské výšce se odráží s koeficientem Fresnelu ve vodorovné polarizaci, εc = εr − j · 60 · λ · δ (moře εr = 81 a ∞ = 5 S / m; země 13 a 0.006 S / m), v jasném úhlu trasy:

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- drsnost moře snižuje konzistentní odraz podle Miller a Brown, s efektivní výškou vln z větru na 10 m (σh = 0,0051 · U2):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- na moři profil M zahrnuje odpařovací kanál (odstavec 9 ter) s neutrálním logaritmickým profilem:

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

Testy s prototypem (10 m anténa na moři, zisk na standardní atmosféře na 100-300 km): s δ = 10- 30 m odpařovací kanál poskytuje 1-3 dB až 144 MHz, 8- 37 dB až 432 MHz a 21- 66 dB až 1296 MHz. Při 144 MHz v horizontální polarizaci se moře odráží téměř jako kov (IR 124; η IR 124; více než 0,99 až několik desetin stupně) a drsnost se počítá málo: nové dno mění několik desetin dB; počítá se více od 432 MHz nahoru.

Mezní hodnoty: nad zemí zůstává vertikální profil úrovně GFS, takže tenké kanály zůstávají neviditelné, země ke schodům se přibližuje, paprsek zůstává v rámci6.

## 12. antény, signál a hluk

- Yagi DL6WU: zisk 9.5 + 7.8 · log10 (boom / λ) dBd (2.35 dB pro dvojité boom, DL6WU opatření na VKV komunikace 3 / 1982), plus 2.15 pro [12] dBi.
- EIRP = výkon − kabel + zisk TX.
- Obdržený signál = EIRP − očekávaná ztráta + zisk RX − kabel RX. S- metr IARU: S9 = − 93 dBm, 6 dB na jednotku.
- Vnější hluk podle ITU-R P.372 [13]: galaktický (Fa = 52 − 23 log f) více umělý podle prostředí (město, obytné, venkovské, klidné venkovské) nebo ručně psaná teplota antény.
- Tsys = Ta + Tpre + [290 (Lc − 1) + 290 (Fr − 1) · Lc] / Gpre na terminály antény (Friis vzorec). Od 1.3.5 má přijímací stanice vždy předzesilovač v anténě, pevný parametr: číslo hluku 0,8 dB (Tpre = 59 K) a získává 20 dB. Za nimi je kabel (ztráta Lc) a rádio se základním hlukovým číslem Fr 6 dB. S 1 dB kabelu klesne kabel a rádiový vstup z cca 1160 K na 12 K: hluk určuje oblohu, prostředí a preamp. Zisk 20 dB je typická hodnota zvolená programem.
- S / N v SSB, CW a FT8 v pásmu každého směru ve srovnání s nastaveným prahem.
- Pravděpodobnost pro období (od 1.3.2): evanescence během hodiny je normální rozdělení S / N kolem očekávané průměrné hodinové hodnoty. Bray (QST, listopad 1961, s. 39, na křivce Norton 1959) dává amplitudu 13,5 dB nad 100 mil, tj. asi 7 dB pod a 7 nad průměrem, který padá téměř přímo pod 100 mil (3,5 dB až 50 mil); pro "pevné" spojení trvá 7 dB od rozpočtu. Zabráno ± 7 dB jako ± 2∞: λ = 3,4 dB nad 160 km, úměrné vzdálenosti uvedené níže. Pravděpodobnost, že období překročí práh cesty je

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  s normální distribuční funkcí: marže 0 dB = 50%, + 3,4 dB = 84%, + 7 dB (pevná marže Bray) = 98%. Nezahrnuje nejistotu modelu, který je větší.
- Digitální způsoby: S / N 2500 Hz ve srovnání s prahovými hodnotami naměřenými při 50% dekódování bez předběžných informací (AP). FT8 − 21 dB a FT4 − 17,5 dB na šumu bílé; když je mechanizmem troposcatter, použijte prahy s evanescencí "narušené zeměpisné šířky", FT8 − 18,6 dB a FT4 − 15,2 dB [21]. Q65 -15C − 22,2 dB, Q65 -30B − 24,8 dB, Q65 -60A − 27,6 dB V letadle rozptyl Doppler může překročit tóny Q65 -60A (1,7 Hz): tam jsou lepší 30B nebo 15C. JT65A − 25 dB [22]. Dekódování AP (název již známé nebo konec QSO) dává asi 2 dB navíc pro FT8 a 1,5 až 2.6 dB pro Q65; nevstupuje do prahu, protože to není platné pro všechny hodnoty "− 24 dB", které jsou často čteny pro FT8 jsou limit WSJT-X vztahy nebo AP dekódování, ne práh na 50%. [24]

## 13. Doba odezvy a údajů

Každý výpočet měří svůj vlastní čas: GFS profily (síť, analýza, povinné přestávky 20 s mezi šaržemi pro Open- Meteo limit), Copernicus GLO-30 terén (se stažením tabulek, 15-30 MB každý, pouze poprvé), výpočet. Časová linie odezvy je shrnuje; v registru začínají řádky pomocí TEMPI. Při modernizaci sítě registr také hlásí použitý závod GFS, kdy byl k dispozici a jak staré jsou data.

Od 1.3.2 si můžete také vybrat ICON-2I, model agentury ItaliaMeteo a Arpae na Itálii na asi 2.2 km, přes Open- Meteo (model Italia _ meteo _ arpae _ icon _ 2i, dva závody denně, 3 dny). Pokrývá pouze Itálii a nedaleké moře a má méně nízké úrovně GFS (1000, 925, 850, 700, 500 hPa): pro vedení v nadmořské výšce není lepší než GFS, ale jemné mřížky popisuje dobře pobřeží, ostrovy a moře na 2 m, to je odpařovací potrubí. Proměnné, které model nemá (stranický limit, teplota povrchu...), se z požadavku odstraňují. Od 1.3.2 automatický zdroj používá NOMADS; Open- Meteo slouží pouze v případě, že NOMADS neodpovídá a pro historické případy. S "Aktualizace na vlastní pěst" box (svítí ve výchozím nastavení) bude předpověď stažena bez kliknutí: na otevření, na každém novém GFS závod na NOMADS (asi 5 hodin po době závodu) a když viditelná plocha je více než čtvrtina z mřížky, maximálně pokus každých 15 minut, nikdy s historickým případem.

Žádosti na Open- Meteo mají limity za minutu, hodinu a den pro každou internetovou adresu. Stažené profily jsou uloženy na disku (Dati / cache- gfs) pro každý tah a model: se stejným závodem, který se mění každých 6 hodin, již stažené body se neptají znovu. S automatickým zdrojem začnete od NOMADS a Open- Meteo slouží pouze pro chybějící body; s jediným Open- Meteo, v časovém limitu nebo denní zastaví okamžitě a říká, kdy zkusit znovu. Historické případy nejsou uloženy.

## 14. Potvrdit model

Předpověď by měla být porovnána se skutečnými spoji. Záznamy výpočtů uchovávají každé číslo; archiv programu uchovává reklamy a dekódování. Pro 432 a 1296 MHz, kde je málo míst, potřebují zprávy operátorů: kdo navázal spojení, do jaké doby, s jakým signálem. Po mnoho hodin porovnejte očekávaný zisk s měřeným signálem říká, že prahové hodnoty mají být přesunuty.

Zkušební programy používané pro P.452 a ITM jsou ve složce se zdrojovými kódy; testovací případy ITU musí být staženy samostatně, protože program nedistribuuje data ITU.

## Zdroje

1. NOAA / NCEP, Global Forecast System (GFS), globální model na 0,25 °, čtyři závody denně.
2. Open-Meteo, predikce API (open-meteo.com), gfs _ bezešvý model, proměnné na tlakových úrovních a závodní metadata.
3. Společnost Copernicus DEM Glo-30 (ESA; WorldDEM © DLR e.V. 2010-2014 e © Airbus Defence and Space GmbH 2014-2018, dodávaná v rámci společnosti Copernicus Evropskou unií a ESA), 1 druhý oblouk, rámce GeotiFF na otevřeném registru internetových služeb společnosti Amazon. Rezerva: NASA / USGS Copernicus Glo-30 v Terrain dlaždice "skadi" AWS.
4. ITU- R P.453, Refrakční index rádia: datum jeho vzorce a lomu s mapami digitálních kanálů (Studijní skupina 3, © ITU, osobní i nekomerční použití).
5. B. R. Bean, E. J. Dutton, Radio Meteorology, NBS Monograph 92, 1966: Tabulka 6.5 a § 4.4.
6. ITU-R P.452-16, Predikční postupy pro hodnocení rušení mezi stanicemi na povrchu Země při frekvencích nad 0,1 GHz: § 4.2 (difrakce), eq. 45 (troposcatter), § 4.5 a eq. 49 (vedeno).
7. ITU- R P.834, Účinky troposférického lomu na šíření radiovln.
8. ITU- R P.525, Výpočet útlumu volného prostoru.
9. ITU- R P.526, propagace difrakcí: eq. 31 (cutcheon) a § 3 (sférická půda).
10. B.
11. D. E. Kerr, Propagation of Short Radio Waves, MIT Radiation Laboratory Series vol.13,1951: eq.17 e18.
12. G. Hoch DL6WU, "Extremely Long Yagi Antennas", VHF Communications 3 / 1982, pp. 130 et seq., fig. 1 (měřeno 432 a 1296 MHz podle délky boomu).
13. ITU- R P.372, Rádiový hluk: galaktický a umělý hluk pro životní prostředí.
14. NOAA / NCEP, NOMADS, GRIB filtr GFS 0,25 ° času (nomads.ncep.noa.gov); WMO G RIB2 formát, komplexní balení s prostorovými rozdíly.
15. Open- Meteo, Historická předpověď API (modelový předpokládaný archiv asi od roku 2021).
16. LukiBox, TropoLink, design troposcatter tras s P.617, NBS TN101 a ITM flanked (GitHub, Apache 2.0 licence).
17. ITU- R P.617-5, Propagační predikční techniky a data potřebná pro návrh transhorizontálních radiorelé systémů,2019.
18. RSGB, VKV / UHF Handbook; DL2FZN implementace (andi-f / troposcatter on GitHub, 2013).
19. W. F. Moler, D. B. Holden, "Troposférický rozptyl šíření a atmosférické oběhy", Journal of Research of the NBS 64D, 1960, doi 10.6028 / jres.064d.014: signál za horizontem (1365 a 9380 MHz, na moři) a vertikální pohyb vzduchu.
20. Příručka ARRL 2022 (99a ed.), § 19.4.3-19.4.4: počasí a troposférické šíření.
21. S. Franke, B. Somerville, J. Taylor, The FT4 and FT8 Communication Protocols, QEX July / August2020.
22. W3SZ, prahy pomalých digitálních režimů ( JT65, JT9, QRA64) a volba režimu podle šíření,2019.
23. QST, "The World Above 50 MHz", November 2022: open of 14-15 August 2022 between Manitoba and Kansas / Missouri.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Quick-Start Guide to Q65, 3 April 2021 (wsjt.sourceforge.io).
25. P. L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis, Transmission Loss Predictions for Troposféric Communication Circuits, NBS Technical Note 101, magazín 1967: § 9 (troposcatter, function F (θd), H0), § 10 a příloha III (rádiové klima a variabilita v čase).
26. Open- Meteo, Historické počasí API (ECMWF ERA5 recenze od roku 1940; Copernicus Climate Change Service, CC BY 4.0 licence).
27. I. Durre, X. Yin, R. S. Vose, S. Applequist, J. Arnfield, Integrated Global Radiosonde Archive (IGRA), version 2, NOAA National Centers for Environmental Information, doi 10.7289 / V5X63K0Q (public domain): igra2-data- format.txt datový formát a seznam stanic.
28. FM-DX Webserver (NoobishSVK a spolupracovníci, licence GPL-3.0) a veřejný seznam servers.fmdx.org. Name
29. ITU-R P.452-18, Predikční postupy pro hodnocení rušení mezi stanicemi na povrchu Země při frekvencích nad 0,1 GHz, 2023; zkušební případy studijní skupiny 3 (příklady validace), jak byly shromážděny v I. Stevenovic, Py452 (GitHub).
30. NTIA / ITS, Nepravidelný Terrain Model (ITM) 1.2.2, referenční kód a bodové zkoušky (GitHub NTIA / itm).
31. ITU-R M.1371, Technické vlastnosti automatického identifikačního systému s využitím mnohonásobného časového dělení v námořním mobilním kmitočtovém pásmu VHF.
