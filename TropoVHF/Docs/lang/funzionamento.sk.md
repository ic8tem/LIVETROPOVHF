LIVETROPOVF · technický dokument 1
# Prevádzka programu

> Automatický preklad do taliančiny: v prípade pochybností stojí za taliansky sprievodca.

Čo program robí, odkiaľ berie dáta a ako ich mení na to, čo vidíte na mape a v zozname. Verzia z 9. novembra 2026 týkajúca sa projektu v C# (.NET 10 + Avalonia).

## Súhrnne

- Účel: predpoveď tropa (Hepburn [3], F5LEN [4], isobare GFS [18]) a reálne pozorovanie pásiem v rovnakom okne, podľa kontrolného zoznamu LA0BY [1].
- Zber údajov: dva DX klastre, Reverse Beacon Network [9], dm4x cluster a PSKReporter [5] MQTT tok, UDP dekódovanie z WSJT-X, JTDX a MSHV [8].
- Spot: pásmo segmenty, negatívny filter, airscatter v prerušovanej žltej, poloha pomocou lokátorov alebo C ALL3.TXT [6] [7].
- Kto počúva: tmavo ružové kruhy s ružovými líniami vždy viditeľné smerom k stanici cítil [5].
- Spot karta: vzdialenosť, azimut, index tropo na ceste a strata troposcatter s G3YGF [2] metódou.
- Jazyky Hepburn a F5LEN: [3] obraz a kalibrácia lavice [4].
- Sledujte: NTP ovládanie na 10 serveroch každé 3 minúty [10] [11].
- Podozrivé správy: EME v Q65/JT65 uznané s pozíciou Mesiaca, skrátené lokátory, skimmery, karanténu a poslucháčov, s pravidlami naučil sám [20].
- Kritériá pre odhadzovanie a význam farbív zhrnuté v dvoch tabuľkách (oddiely 11 ter a 11 quater).
- Témy 36 [17] jazyky, export dokumentov do programu Word s editovateľnými vzorcami [16], prenosný program a multiplatform [12] [13] [14] [15].

## 1. Účel

Program dáva dohromady v jednom okne dve veci, ktoré zvyčajne vyzerajú na rôznych miestach: predpoveď šírenia troposfera (Tropský index Hepburnu a F5LEN, Isobare) a pozorovanie toho, čo sa naozaj deje na pásmach (spoty z DX cluster, Reverse Beacon Network a PSKReporter, plus zoznam staníc, ktoré počúvajú). Referenčné pásmo je 144 MHz; miesta sa zhromažďujú od 50 MHz nahor.

Nastavenie sa riadi kontrolným zoznamom navrhnutým LA0BY pre extrémne tropo udalosti: v predchádzajúcich dňoch sa pozriete na Hepburn a F5LEN a teplotné profily v nadmorskej výške, zatiaľ čo udalosť je na DX klastre a na mieste mapuje dôkaz, že otvorenie začína [1]. LA0BY tiež poznamenáva, že jediné mapy Hepburn/ F5LEN nestačia na predpovedanie najdlhších spojení [1]

## 2. Okno a karty

Okno je jedno a nerozdeľuje sa na mobilné okná. Zhora nadol:

- Všeobecné nastavenie riadok: Volajte a lokalizátor vľavo; vpravo UTC hodiny (napr. 08/11/2026 20:33:38 UTC, jantárový text), za ktorým nasleduje NTP:OK alebo NTP:FAILED, tematické menu a jazyk.
- V piatich skupinách: Mapa a spot; Predpovede (Propria β, predpoveď vypočítaná podľa programu a popísaná v predpoveď Manual, potom Hepburn a F5LEN); GRAVES; Údaje a zdroje (DX cluster, PSKReporter, Reverse Beacon, UDP, Vymenovania a podozrivé správy); Príručky (Program Prevádzka, Map Building, Ako formovať tropo, Manuálna predpoveď, Tipy pre operátora, s vyhľadávacím políčkom, ktorý počíta, v ktorom sa nachádza sprievodca textom). Pod lúčom bar ukazuje podjazyky skupiny; každá skupina si pamätá naposledy použité.
- Obsah karty a v spodnej časti stavový riadok so stavom zdrojov, databázou mien a časom poslednej aktualizácie.

Nastavenia vrchného riadku platia pre všetky karty: vybraná téma tiež vyfarbí zber dát a pozadie dokumentov; Stránky dokumentov zostávajú biele ako list, takže dobre vytlačia.

## 3. Volanie a prenajímateľ

Maidenhead prenajímateľ môže mať 4 alebo 6 znakov (napr. JN70 alebo JN70CN). Program ho konvertuje na súradnice stredu námestia: pole 20° × 10°, štvorec 2° × 1st, subkvadrát 5′ × 2,5′. Ak text nie je platný lokátor, okraj poľa sa zmení na červenú a stanica sa nezobrazí na mape. Lokátor slúži aj ako stred azimutovej projekcie a ako východiskový bod pre vzdialenosti a azimut v reklamnej doske.

Volanie je meno používané ako cluster login. Na prvom začiatku, kým nie je platná nominácia, klastre sa nepripájajú: okno žiada o názov (a voliteľne prenajímateľ); potvrdil, že názov je skopírovaný ako užívateľské meno v prihlasovacom poli každého klastra, ktorý ho má prázdne a klastre odchádzajú. S MQTT, UDP a NTP nepoužívajú prihlásenie a aj tak odchádzajú.

## 4. Zber údajov: zdroje

Každý zdroj má svoju vlastnú sekciu v záložke Data Collection, s pripojeným/rozpojeným, rozpoznaných a pripojených boxoch pri štarte, stave, metroch a registri prijatých riadkov. Neviazané alebo vyradené linky z filtra sa nezobrazia v registri: počítajte len časti metrov. Časti sú po boku dvoch až dvoch.

| Zdroj | Pripojenie | Čo prináša |
| 1. Výber zoskupenia DX | on0dxk.dyndns.org:8000 príkaz sada/filter dxbm/pass 6 | Miesto amatérskych rádií, tiež SSB a CW. Na ON0DXK sú VHF filtrované a sú povolené s uvedeným príkazom (poi pass 4, 2...). |
| 2. Druhý zdroj | dxmaps.com:7300 (DXMAPS, EA6VQ) a s50clx.si:41112 ( S50CLX, Slovinsko) | DXMAPS zbiera VHF škvrny a tiež dáva RBN skimmer škvrny (set/skimmer ovládanie, už nastavený); S50CLX je DX Spider cluster. Prihláste sa s Vaším menom; editovateľné adresy. |
| 3. Reverse Beacon Network | telnet.reversebeacon.net 7000 CW/RTTY · 7001 FT8 | Automatický bod skimmera. Prihlásenie s názvom iba [9]. |
| 4. Zoskupenie s PSKReporter | dm4x.ddns.net:8500 | Digital Spots of PSKReporter vo formáte klastra. |
| 5. PSKReportér MQTT | mqtt.pskriporter.info 1883 · 1884 s TLS | Každá správa o recepcii v reálnom čase: kto vysiela, kto počúva a ich [5] Locátory. Pásma 144, 222 a 432 MHz. |
| 6. Mapy počasia a tropa | dxinfocentre.com/tropo_eur.html tropo.f5len.org/prognózy pre Európu model GFS | Hepburn indexy [3] a F5LEN indexy [4], tlak hladiny mora pre izobare (GFS cez Open-Meteo [18]). Hepburn a F5LEN musia byť povolené autorom. |

Zoskupenie telnetu (zdroje 1 Po pripojení program čaká na žiadosť o meno (text, ktorý obsahuje prihlásenie, hovor:, volací znak alebo váš hovor); ak nedorazí do 4 sekúnd, pošlite prosím prihlásenie. Potom pošlite príkazy sekcie, oddelené od bodu a čiarky, jeden každých 0,6 sekúnd. Ovládacie znaky telnetu sa odstránia pred čítaním riadkov. Ak spojenie padá a Riconnetti je aktívny, skúste to znova po 10 sekundách (15 ak server vôbec nereaguje) [6].

PSKReporter MQTT (zdroj 5). Program sa zapíše do témy pskr/filter/v2/{banda}/{modo}/{tx_call}/{rx_call}/{tx_grid}/{rx_grid}/{tx_dxcc}/{rx_dxcc} ktorým sa opravuje len pásmo a ostatné polia opúšťajú voľný znak + [5]. Presný názov, s ktorým server označuje pásma (napríklad 2m, 1,25m, 70cm), sa musí skontrolovať pri prvom pripojení.

## 5. Z klastra na miesto

Jeden riadok je miesto, keď má formu DX de SPOTTER: frekvencia DX komentár hhmmZ. Program odvodzuje spotter, stanica DX, frekvencia v kHz, komentár a teraz UTC; názvy odstrániť príponu snímačov (-#, -1...). Potom platí, v tomto poradí, nasledujúce pravidlá [6].

| Pásmo | Segment prijatý |
| GRAVES (so 144) | 143,040 |
| 144 MHz | 144 - 148 MHz |
| 222 MHz | 220 - 225 MHz (iba región 2 ITU, Amerika) |
| 432 MHz | 450 MHz |
| 1296 MHz | 1300 MHz |
| 2320 MHz | 2300 MHz |
| 10 GHz | 10,5 GHz |

1. Kapela. Frekvencia musí spadať do jedného z riadených segmentov; všetky ostatné, a najmä každý obchod pod 49 990,0 kHz, nie je zobrazený a počíta sa ako
2. Way. Jedným z najznámejších spôsobov komentovania je FT8, FT4, Q65, JT65, ICAT, CW, SSB, FM, RTTY, PSK31, MSK144, FSK441, JT6M); USB a LSB stať SSB.
3. Negatívny filter. Bod sa vyradí, ak obsahuje režimy MSK144, FSK441, JT6M, JTMS, ISCAT alebo štítky <AU> <AUE> <ES> <MS > <FAI> < F2 >, s nejadrovými hornými dvierkami a aj priestormi v zátvorkách. To sú spôsoby a šírenie, ktoré nie sú tropické: Scatter meteor, aurora, a sporadické, F2.
4. Airskatter. Ak poznámka obsahuje <AS> alebo <ACS> (tiež zmiešané, napr. <As>) alebo vyhlasuje miesto ako airskatter (airskatter, rozptyl vzduchu, rozptýlenie lietadla), miesto zostáva, ale je vykreslené žltou čiarou bodkovanou (tmavý sklz, aby bolo na prehľadnej mape čitateľné) a zvýraznené v zozname.
5. Umiestnenie staníc. Ak v komentári sú dva lokátory (napr. JN54 < JN83) prvý je z tých, ktorí robia miesto a druhý stanice DX; s jedným lokátorom, to je zo stanice DX. Čo chýba je prehľadané v C ALL3. TXT, len s presným názvom (pre notebooky pozri nižšie). Ak stanica zostáva bez pozície, miesto sa zobrazí v zozname s poznámkou
6. Dvojnásobok. Reklama s rovnakým DX, rovnaký špička, rovnaké pásmo a rovnaký zdroj už prítomný ho nahrádza, namiesto pridania.

C ALL3.TXT je názov databázy → lokátor používaný JTDX, vo formáte CALL, LOCATORE,,,,. Súbor projektu obsahuje 1 048 847 mien, všetky s platným lokátorom; hlavička súboru stále hovorí o 34 323 nomináciách, pretože to zostalo tým z prvého vydania [7]. Je načítaná v pozadí pri štarte, takže okno je okamžite použiteľné. Súbor sa vyhľadáva vedľa programu, v dátovom priečinku, v priečinku, z ktorého bol program spustený a jeden alebo dva priečinky vyššie; Ak nie je, okno to povie pri štarte a stavový riadok signalizuje ho červenou. Jeho meno vždy používa svoju 6-maternicu. Iba pravidlo pre lokátory: ak jeden prijatý (spot, MQTT, dekódovanie, QSO desunti) má menej ako 6 znakov a C ALL3. TXT má jeden až 6, vždy stojí za to C ALL3.TXT; pre notebooky (F/ IC8TEM, IC8TEM/P) je len presný hlas: bez, stanica zostáva bez prenajímateľa a nie je navrhnutý inde. Pravidlo platí aj v čase kreslenia, takže údaje, ktoré prišli pred načítaním databázy, budú bežať samy.

Jedna databáza: C ALL3.TXT. Od 1.3.4 sú všetky lokalizátory v jednom súbore, C ALL3.TXT, ktorý zostáva vo formáte JTDX a WSJT-X (CALL,LOCATORE,,,,) a môže byť použitý ako je to tiež vo WSJT-X, JTDX a MSHV. Už nie sú žiadne zložky od seba. V hornej časti súboru, po hlavičke, sú dva bloky označené komentár riadok: // [LIVETROPOVHF- MANUALI] s záznamami vložený ručne a // [LIVETROPOVHF- CLUSTER] s opravami zo zoskupenia; potom // [LIVETROPOVHF- FONTI] so všetkými ostatnými položkami v abecednom poradí. Každá nominácia sa objaví raz. V sublingválnych dátach a zdrojoch › Vymenovania (C ALL3) hľadáte meno, napíšete prenajímateľa a stlačíte Add / zmena: hlas ide do bloku hlasov ručne a stojí to predovšetkým, aj cez lokátor, ktorý stanica sama vysiela. ∫ Odstráni nomináciu z C ALL3.TXT (ďalšia aktualizácia ju obnoví zo zdrojov, ak ju majú). Pridať súbor... prečítajte si zoznam CALL, LOCATOR (akceptujete aj bod a čiarku, tabuláciu a priestor) a zapíšte ho medzi ručne vyberané; vybraný súbor nie je skopírovaný. Znovu načítať C ALL3.TXT z disku, po úprave s editorom. Kompletné poradie zdrojov: vlastný lokátor, ručné hlasy, korekcie klastrov, zvyšok C ALL3. TXT (zdroje, naučené živé lokátory a G7VRD), prijatý lokátor (ktorý je platný nad databázou, len ak má 6 znakov a nekončí s LL alebo MM).

Aktualizácia C ALL3.TXT. V rovnakom subjazyku tlačidlo Aktualizovať teraz sťahuje zdroje (C ALL3.TXT WSJT-X vylepšené, C ALL3.TXT EME W7GJ, grid_data.bin JTDX), pripojí sa k nim (prvý, ktorý má nomináciu výhru), skontrolujte, či sú aspoň 50 000 mien (inak sa nič nezmení), ukladá C ALL3. TXT Predchádzajúci C ALL3. Záznamy TXT zostávajú a 6-miestny lokátor zostáva tam, kde zdroj má len 4 rovnakého štvorca. Ručné hlasy a korekcie zo zoskupenia zostávajú hore, nezmenené. S Update box na vlastnú päsť každý deň, kontrola prebieha na začiatku. Program prepisuje C ALL3. TXT okamžite po každom hlase ručne a, pre naučené lokátory, nanajvýš každých 10 minút a zatváranie; C ALL3. TXT balíka je len na čítanie a atribút sa odstráni sám. Inštalatéri Linux neprepisujú C ALL3. TXT už prítomný; že pre Windows nainštaluje len v prípade, že chýba. ALL3

Automatické opravy zo zoskupenia. V DM4X a RBN automatické škvrny komentár prináša dva lokátory, napríklad KN09tp<> KN09: prvý je to, čo stanica si spot sám seba deklarovať, 6 znakov; druhý je to, že stanice cítil, často len na 4. Ak prvý má 6 znakov (a nekončí s LL alebo MM) a je iný ako v databáze, program opravuje C ALL3. Samotný TXT: oprava ide do bloku // [LIVETROPOVF- CLUSTRER], s dátumom posledného potvrdenia v treťom poli, je platná nad zdrojmi (pod hlasmi v ruke) a objavuje sa medzi podozrivými signálmi aktualizovanými ako "C ALL3." Takže to prežije C ALL3. Aktualizácia TXT. Nepotvrdená korekcia na dva roky stráca prednosť a zostáva normálnym hlasom.

Posledný zdroj: G7VRD. Ak nominant nemá 6-miestny znak v C ALL3. TXT, program ho žiada v pozadí pre verejnosť API G7VRD (api.g7vrd.co.uk, Akceptujete len 6 znakov, ktoré nie sú stredom námestia (LL alebo MM); požiadavky sú zoskupené, maximálne 40 nominárov za minútu, a každý nominár žiada raz denne, pretože je to hobistická služba bez záruk. Lokátor nájdený vstupuje C ALL3. TXT, ak názov nie je alebo má len 4 znaky rovnakého štvorca; vyhľadávanie sa vypne s políčkom v záložke názvu.

Stretnutia s barom. V PSKReporter MQTT témy, bar oddeľuje úrovne, potom zložené názvy prichádzajú s bodom namiesto baru ( IV3. HB9CAT). Program vráti lištu späť a ak správa obsahuje plné mená, použite tie: IV3/HB9CAT zostáva IV3/HB9CAT na mape, zozname a databáze.

Lokátori sa naučili žiť. C ALL3. TXT neobsahuje všetky: napríklad SP9EMF a SP9NVY tam nie sú, a potom zostáva 4-miestny lokátor odoslaný PSKReporter (karta hovorí, že). Pre zníženie týchto prípadov program píše v C ALL3. TXT každých 6 znakov, ktoré pochádzajú z akéhokoľvek zdroja (PSKReporter, ako vysielač alebo ako prijímač, klaster, UDP dekódovanie) pre nomináciu, že C ALL3. TXT nemá alebo má len 4 znaky v rovnakom štvorci; nikdy nemení hlas na 6 znakov. Až do 1.3.3 boli tieto lokalizátory v údajoch/ LOC6. TXT.

Prechod z 1.3.3. Na prvom začiatku 1.3.4 súbory okrem predchádzajúcich verzií (Dati/C ALL3 -MANUALE.TXT, Data/C ALL3 -AGGIUNTE, Data/C ALL3 -CLUSTER. TXT, Data/ LOC6.TXT a Data/C ALL3 - G7VRD.TXT) sa zlúčia do C ALL3. TXT uvoľňujúce písanie, staré súbory vziať.fuso rozšírenie, už nečítate a môžete odstrániť.

Automatické prijímače. RBN, dm4x a PSKReporter často hlásia názvy s príponou označujúcou prijímač: -SDR, /SDR, -# (skimmer RBN) alebo -1, -2... (plus prijímače rovnakej stanice). Pre prenajímateľa je prípona odstránená: OK2OZL -SDR trvá OK2OZL JN89UE z C ALL3.TXT, ak stanica už nepošle 6-miestny znak. Zobrazený názov zostáva ten s príponou.

Lokátory rezali. Nie všetky zdroje liečiť rozšírenie svojho prenajímateľa: mnohí nastaviť to na 4 znaky, a niektoré automatické prenos-over systémy zámerne skrachovať 4-miestny lokátor, aj tie, ktoré získajú dáta zo skutočného počúvania FT8, FT4, Q65 a JT65, alebo dokončiť s stredom štvorca (LL alebo MM, napr JN89MM). Z tohto dôvodu sa 4-miestny alebo 6-miestny znak, ktorý končí LL alebo MM, nahrádza C ALL3. TXT rovnakého štvorca. Ak správna poloha a stred námestia sa nezhodujú s niekoľkými desiatkami km, chyba zostáva malá v porovnaní s tropo vzdialenosti.

## 6. Kto počúva?

Každá správa MQTT od PSKReporter hlási meno a lokátor tých, ktorí dostávajú [5], takže nemusíte hľadať v C ALL3.TXT. Pre pásmo vybrané vo voliči program drží, pre každú prijímaciu stanicu: prenajímateľ, posledná recepcia, dekódované spôsoby, počet staníc cítil a maximálnu vzdialenosť. Stanica opustí zoznam, keď už nedostane nič vo zvolenom rozsahu (3, 5, 10, 15, 30 alebo 60 minút).

Na mape každý prijímač je tmavo ružový prázdny kruh, a od každého začiatku plné ružové čiary smerom k staniciam, ktoré cítia a posiela späť do siete. Linky sú vždy vidieť, bez kliknutia. Vymenovania počúvacích staníc, sluchových staníc a dvoch hláv každého miesta sú vždy napísané na mape, bez toho aby museli kliknúť (rovnaké meno je napísané raz). Kliknutím na kružnicu sa jeho línie ešte zhustnú a otvorí kartu detailami. Ak lokátor z MQTT má menej ako 6 znakov, program používa to C ALL3.TXT (asi jeden milión mien), za predpokladu, že je to v súlade s prijatým štvorcom.

Režimy scatter meteor. Rovnako ako v prípade klastrových škvŕn, MQTT pomery v MSK144, FSK441, JT6M, JTMS alebo ISCAT sa vyradia a počítajú len; to isté platí pre dekódovanie UDP týmito spôsobmi.

Nie praví poslucháči. Niektoré stanice hlásia, že prijímajú v 144 MHz, ale v skutočnosti je prijímač na inom pásme (nesprávne indikácie frekvencie): ich správy by navrhli existujúce otvory tisícov km. Program to kontroluje sám. Každá stanica, ktorú poslucháč hovorí počuť vo VHF, sa sleduje 30 minút na všetkých pásmach s vyhradeným nápisom MQTT (pskr/filter/v2/+/{nominatívny}/#) [5]. slúži ako dôkaz. Ak aspoň dve stanice, ktoré poslucháč vypočuje, sú v tej istej minúte (±10), aktívne na iných pásmach, poslucháč sa považuje za nepravého a všetky jeho správy o VHF sa vyradia; register MQTT oznamuje meno a stanice, ktoré ho zradili. V zozname miest, vedľa meračov, tlačidlo MQTT ukazuje stav toku (zelený list, ak je pripojený, červený, ak nie) a koľko poslucháčov boli vyradené v pásme a rozsahu zvolený; kliknutím na tlačidlo otvorí kartu so zoznamom odpadu a pre každú stanicu nájdené na iných pásmach.

Long Connect China. Pripojenie cez 1000 km (Clumbing Spot, MQTT počúvanie, QSO Desunti) nie je okamžite zobrazené: zostáva v držbe, a protiľahlý protiklad > 1000 km Odhaľuje sa, keď to iné spojenie medzi rôznymi stanicami a aspoň polovicou pozdĺž potvrdzuje jedným z týchto kritérií: rovnaká stanica na hlave (napríklad ten istý poslucháč) a druhá stanica v rovnakom smere v rozmedzí 10°; podobná chodba s oboma hlavami v rozmedzí 200 km od ostatných spojov; paralelná trasa so smerom v rozmedzí 10° a bočná vzdialenosť v rozmedzí 200 km prekrytá pozdĺž trasy. Oba ukazujú a MQTT log ukazuje dôvod. Jeho UDP dekódovanie sú vždy viditeľné a aplikovať ako potvrdenie pre ostatných. Zachované spojenia zostávajú v pamäti 60 minút.

Nič pod 144 MHz. Program funguje od 144 MHz nahor: všetko pod 143 MHz je vyradené. Výnimkou je GRAVES, francúzsky radar vysielajúci v CW na 143,050 MHz od Broye-lès-Pesmes ( JN27SI): obchodná oblasť medzi 143,040 a 143,060 MHz alebo s GRAVES v názve sa nachádza medzi 144 MHz s radarom ako vysielačom. Adboard hovorí, že ak je prijímač vo vnútri lúča (odvetvia D, C, B, A od 90° do 270° azimutu videný z radaru, 6 krokov od 7,5° každý) alebo von; záložka GRAVES, ako je to v YA-MS, ukazuje oblohu videný z radaru: sektory, krok osvetlený v tom čase (sekvencia PE1ITR 2021: 0,8 s na krok, cyklus 4,8 s Údaje a geometria pochádzajú z YA-MS. Pocit to v mape je ťažké, ale môže sa to stať.

Rehabilitácia z ľudských zoskupení. Stanica, ktorá sa objavuje aj na iných pásmach, sa nepočíta ako podozrivá, ak v tých istých minútach (±30) ľudské klastre DX ( ON0DXK, GB7RAU, nie dm4x, RBN alebo iné automatické zdroje) aktívne poškodenie daného pásma, v miestach vyvolaných inými názvami: je to prípad staníc, ktoré skutočne pracujú na viacerých pásmach spolu. Tak sa vyradený poslucháč vráti viditeľné (MQTT register píše

Aktívne stanice pod 144 MHz. V karanténe dlhých spojení stanica, ktorá sa v tej istej minúte vypočuje na pásmach pod 144 MHz (zo správ MQTT iných pásiem), nepotvrdzuje iné spojenia a nie je odhalená, hoci podobná alebo paralelná trasa by to potvrdila.

## 7. Zoznam reklám

Zaberá 35% právo na záložku Map a miesto. Hore: titulok, volič pásu (50, 70, 144, 222, 432, 1296, 2320 MHz, 10 GHz), volič rozsahu (posledné 3, 5, 10, 15, 30, 60 minút) a kľúč aktualizácie. Nižšie, štyri metre: viditeľné škvrny, AS/ACS, vyradené z filtra, stanice počúvanie.

Každý riadok zobrazuje typ čiary, čas UTC, DX → DE, cestu, vzdialenosť v km a na druhom riadku zdroj, frekvenciu a komentár. Posledná je na vrchole. Jedným kliknutím na riadok vyberiete miesto: na mape narastie čiara a objavia sa dve mená.

## 8. Spot karta a strata troposcatter

Pre vybrané miesto znázorňuje záložka lokátory, vzdialenosť, azimut, frekvenciu, režim, zdroj, teraz, komentár, priemerný index tropo pozdĺž trasy a odhad straty trasy pre rozptýlené tropo.

Vzdialenosť a azimut sú vypočítané na maximálnom kruhu s polomerom Zeme 6371 km (formula emisenoverso na vzdialenosť, počiatočné azimut zo stanice DX na špičku).

Strata troposcatter. Základná strata pre dve stanice na hladkej zemi je súčet straty voľného priestoru a straty difúzie: G3YGF [2]

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

Straty sú v dB, D je vzdialenosť v km, f frekvencia v MHz, θs difúzny uhol v stupňoch.

8497 km je ekvivalentný polomer Zeme (4/3 fyzického). θA a θB závisia od horizontu každej stanice:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

s Δh výška prekážky z horizontu vzhľadom na stanicu a jej vzdialenosť. Každá úroveň horizontu stojí približne 10 dB [2]. Program nepozná horizont staníc, takže ukazuje základnú stratu s θA = θB = 0 a deklaruje ju ("hladké uzemnenie"). G3YGF pridáva podnebie, kde N je reaktivita na pôdu [2]; použije sa, keď je N k dispozícii z modelu počasia:

```math
L_{N} = 0,2 \cdot (N - 310)
```

Overenie: pre 110 km až 144 MHz vzorec dáva 166,5 dB, proti asi 168 dB lôžok na grafe výrobku. Pre 500 km až 144 MHz poskytuje približne 206 dB. Očakávaný pomer signálu/hluku je kapacita systému (eirp + efektívna citlivosť prijímača) mínus strata: s modelovým systémom G3YGF 144 MHz (198 dB) a 500 km cesta by asi −8 dB v SSB [2], to znamená, že spojenie je len s lepším šírením čistého rozptylu.

## 9. Moje dekódovanie: UDP z WSJT-X, JTDX a MSHV

WSJT-X, JTDX a MSHV môžu poslať UDP preč každý riadok, ktorý dekódujú. Program počúva na 127.0.0.1:2237 a opakuje rovnaký balíček, bajte per byte, na 127.0.0.1:2238, takže ostatné programy, ktoré použili tento port (GridTracker, JTAlert, logger) pokračovať v práci, ak budete pohybovať na 2238. Adresy a dvere sa menia v sublingválnom UDP súboru údajov; ako odpočúvacia adresa môžete použiť aj multicastovú skupinu (napr. 240.0.0.1), ak je dekódovací program nastavený takto.

Balíky sa riadia sieťovým protokolom WSJT-X: počiatočné magické číslo 0xADBCCBDA, číslo schémy a typ správy. Program využíva najmä tri typy [8]:

- Srdce: hovorí, ktorý program vysiela dáta a s akou verziou.
- Stav: frekvencia voľby a režimu; z frekvencie dostanete pásmo (s rovnakou tabuľkou bodov).
- Dekód: SNR, čas a frekvencia odpadu, spôsob a text dekódovanej správy.

Z textu dostanete stanicu ste počuli: v CQ 9 A2PQR JN83 je stanica po CQ (úspora DX, EU, TEST...), IK0JKL IT9MNO JM68 je druhý, tj, kto vysiela. Lokátor je ten z správy, ak tam je (4 znaky, odlišné od RR73); inak hľadáte v C ALL3. TXT. Počúvajú linky: stanice, ktoré váš prijímač počuje teraz. Na mape sa stávajú červenými čiarami z vášho QTH na stanicu, s názvom; vrstva sa rozsvieti s dekódovaný box odo mňa a vyžaduje platný lokátor. V zozname na pravom pultu povie, koľko staníc dekódujete na zvolenej kapele.

QSO desunti Dal naživo. Ak je dekódovanie záverečnou správou vo forme C ALL1 C ALL2 RR73 (alebo 73 alebo RRR), napríklad IC8TEM IK3FHP RR73 alebo IK3FKP SV8PEX 73, obidve stanice dokončili odkaz. Program to zaznamenáva ako QSO, s lokátorom každého prevzaté z poslednej správy, v ktorej to vyhlásil, alebo, ak nie je, z C ALL3.TXT [7], a kreslí ho na mape ako zelená čiara medzi dvoma stanicami (strate Pár staníc sa počíta raz za pásmo; pásmo a rozsah zvolený v zozname sú platné.

V tom istom subjazyku Debug pane zobrazuje text každého prijatého balíčka (typ, čas, hodnoty a správa), na požiadanie aj hexadecimálne bajty a riadok pre presmerovanie. Je to skontrolovať, či dekódovací program vlastne posiela údaje.

## 9 bis. Jazyky Hepburn a F5LEN

Každý ukazuje, čo program berie zo zdroja. Tlačidlo Pre každý obrázok je krabica na vylúčenie a pole s predpoveď času. 31 európskych obrázkov F5LEN sú pomenované podľa času modelovej preteky (03.png... 48.png každých 3 hodiny, 54... 96 každých 6 hodín, 108... 192 každých 12 hodín) a program trvá čas od názvu súboru [4]; pre Hepburn je to poradie stránky. V záložke F5LEN si menu vyberie región: Európa (tropo.f5len.org/predpovede-za-európu), Austrália (tropo.f5len.org/Austrália, vk03.png... vk192.png) alebo Atlantik medzi Kapverdami a Írskom (tropo.f5len.org/atlantic-ocean/atlantic-d4-ei, atl03.png... atl192.png). Každý má rovnaké kroky. [3] F5LEN [4]

Nižšie je kalibračné počítadlo. Jedným kliknutím na obrázok sa pridá riadiaci bod (lokátor alebo lat,lon: trvá aspoň 6) alebo farba legendy s jej hodnotou. Program počíta pixel zodpovedajúce → súradníc s polynómnou interpoláciou druhého stupňa na minimálne štvorce [19], číta farby s zvolenou toleranciou a, pre F5LEN, konvertuje jednotky N v mierke 0 Uložiť kalibráciu a skúšobné čítanie a píše súbor v priečinku Tarature a kontroly, na ktoré pixel váš lokátor padá. Pred nastavením kalibrácie by sa malo autorovi máp udeliť povolenie.

Regióny. Každý zdroj má viac regiónov, každý so svojimi fotkami s pevným menom: Hepburn tr_map/fcst/{code} {ore}.png?v {data} (eur Europe, nwe North-Western Europe, eeu Eastern Europe, enter North-East Atlantic, nat North Atlantico, eam North America East, nsa South America North America, sam South America, mid-East Asia, nca Central-Septencial Asia Záložka každého zdroja zobrazuje najprv náhľad fúzie: všetky stiahnuté obrázky spolu ako na hlavnej mape, s bodkovanou tabuľou a názvom každej oblasti, šedý závoj, kde žiadny obrázok nepokrýva oblasť a posuvník na rolovanie hodín; na ľavej strane zoznam oblastí, z ktorých každá obsahuje rámček, počet máp, z ktorých sa čítajú a odkiaľ pochádzajú extrémy (OCR, ručne alebo v továrni; ak OCR zlyhá text, ktorý čítal, sa objaví). Manuálna kalibrácia lavice zostáva za homonymným tlačidlom. Menu Bench Region vyberá, ktorý vzhľad, pole Rám je nájdený samostatne (dve vodorovné čiary a dve zvislé čiary rovnakej farby), aby obraz slúžil extrémom lat/lon rámu: pre Hepburn Europe (10° W N) sú už v programe, pre ostatné regióny, ktoré píšu v štyroch O E S N poliach čítajúce osy etikety. Multiplikátor stupnice umožňuje porovnať tieto dva zdroje: konečná hodnota = multiplikátor × legendná hodnota. [3] F5LEN [4] F5LEN F5LEN

## 9b. Exportovať do slov

Tieto dva dokumenty majú na vrchole Súbor je napísaný vo formáte Office Open XML; vzorce sa stávajú rovnicami Word (OMML), takže zostávajú editovateľné s editorom rovnice [16]. Ak okno uloženia nie je k dispozícii, súbor sa skončí v priečinku Dokumenty vedľa spustiteľného tabuľky.

## 9 quater. Kto ma volá

Ak je na lokálnom UDP odoslaná správa (prvé meno je vaše, napríklad IC8FAX IK3FHP JN55 alebo IC8FAX IK3FHP -12), program sleduje od vášho QTH na stanicu, ktorá vás nazýva čiernou bodkovanou a blikajúcou čiarou, s menom a lokátorom vedľa vás. Lokátor pochádza z C ALL3.TXT (vrátane hlasov rúk) alebo správy. Čiara zostáva, kým UDP prechádza uzavretie QSO s touto stanicou ( RR73, RRR alebo 73, v jednom z dvoch smerov), najviac 3 minúty od posledného hovoru; nakreslíte všetko a nedodržiavajú kritériá vylúčenia, takže priamy hovor nikdy neunikne.

PSKReporter prostredníctvom MQTT neodosiela text správ: hovorí len ten, kto prijal, s frekvenciou, spôsobom a SNR. To je dôvod, prečo je priamy hovor uznaný len jeho vlastné UDP dekódovanie.

## 10. Časová kontrola (NTP)

Digitálne režimy a čas na mieste vyžadujú presné hodiny. Každých 3 minút program vypočúva 10 NTP serverov (SNTP na UDP 123): 0.pool.ntp.org, 1.pool.ntp.org, 2.pool.ntp.org, 3.pool.ntp.org, it.pool.ntp.org, ntp1.inrim.it, ntp2.inrim.it, ptbtime1.ptb.de, time.google.com, time.cloudflare.com [11].

Pri každej odpovedi sa vypočíta odhodenie odpadu a oneskorenie návratu, abnormálne hodnoty sa vyradia a použije sa medián, pričom výpočet odvrhnutia odpadu a oneskorenia poskytne SNTP [10]. Pri minimálne 3 odpovediach súhlasíte s UTC hodín programu zarovnanie a ďalšie sa objaví NTP:OK v zelenej; ak odpovede nie sú dostatočné sa objaví NTP:FAIILED v červenej farbe a hodiny zostávajú v počítači. Systém hodiny nie je dojatý, pretože administrátorské povolenia by boli potrebné; všetky interné programové plány (spoty, 5 a 60 minút okná, dekódovanie) používať zarovnaný čas. Ovládanie sa vždy otočí v pozadí: v rozhraní vidíte len výsledok vedľa hodín; servery, intervaly a minimálne odpovede zmeny v LIVETROPOVHF.ini (ntp_servers, ntp_minutes, ntp_min).

## 10 bis. Všeobecné kritériá vylúčenia

Dve krabice vo vrstve sa vzťahujú na všetko, čo nakreslíte (spot, PSKReporter, dekódovanie, QSO desunti):

- Vylúčené do 100 km. Premávka medzi stanicami s dĺžkou menej ako 100 km nemusí byť významná pre tropo (zemná vlna, optický rozsah): s on-boxom nekreslíte a nezobrazia sa v zozname. Začíname.
- Viac ako 1000 km: iba štvorce. Okolo QTH (lokátor nastavený na začiatok) je koruna 1000 km vo všetkých smeroch. Ak je len jedna z dvoch staníc odkazu mimo tejto koruny, spojenie nie je nakreslené ako čiara: v mieste, kde sú farebné lila, bodkované na 45°, lokátory štvorcov (4 znaky) z príslušných staníc. To isté platí pre stanice PSKReporter počúvajúce viac ako 1000 km, ktoré sa objavujú ako štvorec namiesto kruhu. Zatiaľ vidíte dopravu bez vyplnenia mapy liniek. Bez platného lokalizátora sa počíta dĺžka spojenia. Námestie vášho QTH nikdy farby v lilach, aj keď počúvate stanice za korunou: platí nastavený prenajímateľ, akékoľvek meno používa, kto pracuje zo stanice. Polomer koruny je vybraný vedľa poľa: 1000, 700 alebo 500 km. Krabica Corone 1000/700/500 km kreslí tri koruny okolo QTH, nakreslené v lilach, s najoznačenejšou voľbou a vzdialenosť napísaná na sever. V zozname miest je vzdialenosť nasleduje.. Začíname. Kritérium sa uplatňuje po karanténe: dlhé spojenia, ktoré sú stále zachované, sa dokonca ani nevyzerajú ako štvorce.

Čo sú ľalie štvorce pokryté na 45°. Premávka ďaleko od vášho QTH vás zaujíma pochopiť, kde šírenie je otvorené, ale jeho linky, stovky kilometrov dlhé a často desiatky, pokrytie mapy a skryť, čo sa pre vás počíta: otvory môžete využiť zo svojej stanice. Z tohto dôvodu, mimo zvolenej koruny, program nekreslí čiary, ale farby lokátor štvorcov (2 × 1°) z príslušných staníc. Výsledkom je mapa aktívne zóny: tam, kde sa objaví mnoho blízkych námestí lilac, tam je kanál alebo otvor, aj keď sa vás netýka priamo; Ak sa oblasť rozširuje smerom k vám, je to signál namieriť anténu v tomto smere. Doska 45° vám umožní vidieť pod tropo index a mriežku, tak sa porovnať na pohľad predpoveď (dolná farba) a skutočný prevádzku (lilac). Váš QTH námestie nikdy farby, pretože je východiskovým bodom a nie vzdialenej oblasti. Iba štvorce, ktoré sú všetky za korunou sú farebné: štvorec prekročený korunou alebo vnútri zostáva voľný, aj keď je jedna z dvoch staníc. Ak chcete vidieť čiary, stačí vypnúť krabicu alebo vybrať väčšiu korunu.

## 11. Témy a jazyky

Témy: 18 spoločných programov IC8TEM, rovnaké ako YA-NEC a E-LAYER ANALYZER: Clear, Night, Black, 8 pastely (modrá, oranžová, akvamarínska, zelená, ružová, lilac, indigo, hnedá), Scia meteorica (YA-MS palette), tri IP-TRX-CTRL (Chiaro, Notte, Nero) a Blue, Purple, Green. Od 1.3.4 Chiaro vyzerá ako YA-NEC: svetlosivé pozadie, ohraničené biele panely, modré nočné karty a modrý prízvuk. V temných témach sa aj základná mapa stáva tmavou. Farebná škála predpovede zostáva rovnaká vo všetkých témach. Na úzkych obrazovkách (pod 1500 pixelov, napríklad 1366×768) hodiny hore ukazujú iba hodinu, takže téma, kurzor textov a jazyk zostávajú viditeľné. Jazykové znalosti: YA-MS [17] wiki (anglický, Travel, Deutsch, Nederlands, Français, Español, Português, Dansk, Norsk, Svenska, Íslenska, Eesti, Lietuvo, Latviešu, Руский,,кра,нська, Türkçe, Preklady sa týkajú rozhrania; dva technické dokumenty a správy v stavovom riadku zostávajú v taliančine. Mená dní pochádzajú z medzinárodného prostredia. NET pre zvolený jazyk. [12]

Téma a zmena jazyka z dvoch menu v pravom hornom rohu, vedľa hodín, a aplikovať okamžite, bez reštartovania programu a bez prerušenia zberu dát. Voľba je uložená v LIVETROPOVHF.ini.

## 11 bis. Podozrivé a samovzdelávacie správy

Záložka podozrivých hlásení zhromažďuje všetko, čo je v programe riadené, nájdené zvláštne, zoskupené podľa typu a názvu, s posledných 24 hodín uvedených. Program sa sám učí: pre každý pár (typ, názov) počíta pozitívne výsledky (+) a naopak (−), a po 3 dohodnutých výsledkoch vytvára automatické pravidlo; opačný výsledok ho spochybňuje. Užívateľské rozhodnutia (dobré, chudobné) vždy platia; Pulsante tlačidlo vráti rozhodnutie do programu. Pravidlá sú v dátach/APPENDIMENTO. TXT, vedľa programu; automatické tie nie sú posilnené na rok sú zabudnuté.

| Typ | Kedy | Exit + / − | Účinok pravidla |
| EME ( Q65/JT65) | Q65 alebo JT65 nad 1000 km s Mesiacom nad horizontom do oboch hláv, alebo EME v komentári | - zakaždým, keď je Mesiac obyčajný | Vyhoďte: stanica EME, jej dlhé Q65/JT65 sú tiež vylúčené bez spoločného mesiaca. Dobrý: zaobchádzať ako tropo |
| Dlhé spojenie | Viac ako 1000 km v karanténe | + odhalené, − exspirácia bez potvrdenia | Dobrý: dĺžka stanice ukázať okamžite. Zlikvidovať: nikdy |
| Počúvajte. | PSKReportér prijímač, ktorý počuje aktívne stanice na iných pásmach | − vyradené, + rehabilitované ľudskými klastrami | Dobrý: nikdy vyradené. Zlikvidovať: vždy |
| Iný prenajímateľ | Námestie dostal iný ako C ALL3. TXT | + pri každom opakovaní | Dobrý: stanica sa presunula, to stojí za námestie dostal. Zlikvidujte: komoru C ALL3.TXT |
| Square Center | 6 znakov s LL alebo MM |  —  | Iba informácie |
| Automatický prijímač | Vymenovanie s -SDR, /SDR, -# alebo -n |  —  | Iba informácie |

EME. Q65 a JT65 sú spôsoby komunikácie cez Lunu: Teoreticky spadajú do klauzuly o vylúčení, ako je meteor scatter, ale používajú sa aj v tropo. Program vypočíta polohu Mesiaca s nízkopresnými vzorcami Almanac Astronomical (asi 0,3°) [20]: ak spojenie Q65 alebo JT65 presahuje 1000 km a Mesiac je nad obzorom oboch hláv, je takmer určite EME a je vylúčený (počíta sa medzi vylúčené). Ak Mesiac nie je bežný, spojenie zostáva možné v mape a prechádza karanténou dlhých spojení, ale signál zostáva viditeľný. Vzťahuje sa na zoskupenia, pre MQTT a vlastné dekódovanie UDP.

## 11b. Kontakt na kritériá odhadzovania, jeden po druhom

Program chce ukázať len tropoférické šírenie v 144 MHz a ďalej. Každý prichádzajúci dáta (Cluster Row, PSKReporter Reporting prostredníctvom MQTT, UDP dekódovanie) prechádza radom filtrov, vždy v rovnakom poradí. Vyradené údaje nezmiznú v ničom: započítajú sa (kontakt vylúčený v hornej lište) a ak je dôvod pochybný, objaví sa v záložke Suspended Reports.

|  N.  | Kritériá | Kde je? | Čo bolo vyradené | Môžeš sa prezliecť? |
|  1  | Pásmo | cluster, MQTT, UDP | Všetky pod 143 MHz (tiež 50 a 70 MHz) okrem GRAVES 143,050 MHz. Pásma sú 144, 222, 432, 1296, 2320 MHz a 10 GHz. | Nie. |
|  2  | Satelit QO-100 | cluster, MQTT, UDP | 2400,0 MHz: ide o geostacionárnu satelitnú prevádzku, nie o tropo | Nie. |
|  3  | Rozptyl meteorov | cluster, MQTT, UDP | JT6M a ISCAT (tiež ISCAT-A/B), komentáre k "&" a netropoxy etiketom šírenia (<AU>, <AUE>, <ES>, <FAI>, < F2 >) vždy. MSK144, FSK441 a JTMS (aj s <MS>) iba nad 250 km: do 250 km prijímame, pretože v tejto vzdialenosti sa tieto režimy používajú aj v tropo | Nie. |
|  4  | EME (cez Lunu) | cluster, MQTT, UDP | EME napísané v komentári; Q65 alebo JT65 nad 1000 km, keď Mesiac je nad horizontom oboch hláv; stanice sa naučil ako EME | Áno: Dobré pravidlo v podozrivých signáloch |
|  5  | Nepravý poslucháč | MQ | PSKReporter prijímač, ktorý hovorí, že počuť na 144 staníc aktívnych v tom čase na iných pásmach (najmenej dva): Jeho vzťahy sú vyradené. Späť dobré, ak DX ľudské klastre potvrdiť rovnaké stanice | Áno: Dobrý / chudobný |
|  6  | Karanténa dlhých spojení | všetky riadky | Viac ako 1000 km spojenie zostáva skryté, kým nenájde potvrdenie: rovnaká stanica v rovnakom smere, iná trasa v koridore 200 km alebo paralelná trasa v okruhu 10° a 200 km pozdĺž aspoň polovice. Bez potvrdenia vyprší po 60 minútach | Áno: Dobrý / Off na stanicu |
|  7  | Pod 100 km | riadky a zoznam | S pokladničkou, najkratšie spojenie 100 km (zemná vlna, optický rozsah) | Box, vypnutý pri štarte |
|  8  | Za korunou z QTH | riadky | Ak je stanica viac ako 1000, 700 alebo 500 km (podľa vášho výberu) z vášho QTH, žiadna čiara: len lilla štvorcov pokrytých dve stanice. Váš QTH námestie nikdy farby | Kolónka a polomer, na 1000 km |
|  9  | Časové okno | všetko | Staršie miesta vybraného rozsahu (od 3 do 60 minút) | Menu hore |
|  10  | Dvojité | klaster | Rovnaké miesto (rovnaký DX, rovnaký bod, rovnaké pásmo, rovnaký zdroj) opakované | Nie. |

Poznámka o kritériách 4 a 6. Spojenie Q65 alebo JT65 nad 1000 km bez spoločného Mesiaca je možné v tropo: nie je vyradené kritériom 4, ale prechádza na karanténu kritéria 6 ako akákoľvek iná dlhá väzba. Priame hovory na vás (oddiel 9 quater) sú jedinou výnimkou z kritérií 7 a 8: vždy kreslia.

Čo nie je plytvanie. Opravy lokalizátora (C ALL3. TXT s ručnými hlasmi, opravami z klastra a naučenými lokátormi, príponami -SDR, stred námestia LL/MM) sa menia tam, kde nakreslíte stanicu, nie ju zahodiť. Stanica bez prenajímateľa, na druhej strane, nemôže byť čerpaná: zostáva v zozname so vzdialenosťou

## 11 quater. Farby a symboly

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

## 12. Nastavenie a prenosnosť

Okno. Na začiatku zaberá okno 90% užitočného priestoru obrazovky (odstrihnite aplikačnú tyč), centruje sa tak na 15-16 palcovom notebooku s 125% alebo 150% zmenou veľkosti titulku zostáva viditeľný. Ak je užitočný priestor pod 1280 × 720 bodmi, časť okna sa maximalizuje. Veľkosť, poloha a maximálny stav sú uložené v LIVETROPOVHF. i pri zatváraní a konci pri ďalšom štarte za predpokladu, že titulok stále padá do obrazovky (napríklad po odpojení externého monitora); inak sa vrátite na 90%. Minimálna veľkosť je 900 × 560.

Program je prenosný: nikdy nepíše v priečinku AppData alebo v registri. Všetko je v priečinku spustiteľného: LIVETROPOVHF.ini (nastavené, vytvorené pri prvom zatvorení), C ALL3.TXT (s ručne písanými hlasmi, naučenými korekciami a lokátormi), mapová vyrovnávacia pamäť a meteorologické údaje, naučené pravidlá (Dati/APPENDIMENTO.TXT) a LIVETROPOVHF.db archív. Môžete tiež udržať na USB palicu, pokiaľ je priečinok napísaný.

Ad archive (SQLite [14]). Každé miesto prijaté a každý PSKReporter počúvací report je uložený v LIVETROPOVHF.db, SQLite databáze v jednom súbore. V pamäti zostáva len posledných 60 minút pre zoznam a mapu; archív uchováva všetko a slúži pre historika a štatistiky (tiež UDP dekódovanie), napríklad otvory pre pásmo a za deň, maximálne vzdialenosti, štvorce dosiahnuté, a zadné porovnanie medzi očakávaným indexom a reálnymi škvrnami. Linky vyradené z filtra sa počítajú, ale nearchivujú.

## 12 bis. Automatická inštalácia a aktualizácie

Z verzie 1.0 program už stiahne pripravený zo stránky Vydanie github.com/ic8tem/LIVETROPOVHF, v rovnakých balíkoch YA-MS: inštalátor pre Windows (64 bit, 32 bit a ARM, inštaluje do C:\LIVETROPOVHF bez správcu práva) a prenosný zip; pre Windows 7 SP1 a 8.1 balíčky od seba (-windows7-x64 a -wind.deb,.rpm, Arch, Slackware, AppImage a tar.gz pre Linux; balíky pre Raspberry Pi (64 a 32 bitov) a Alpine;.dmg a aplikácie.zip pre Mac (Apple Silicon and Intel). S balíkmi Linuxu sa program skopíruje na prvý štart v ~/LIVETROPOVHF, takže nastavenia a dáta zostávajú vedľa programu; na dátach Mac sa rodí vedľa LIVETROPOVHF.App.

Každých 30 minút (prvá kontrola pár sekúnd po štarte, okamžite, ak naposledy program bol uzavretý pre chybu) program číta sám, bez opýtania o niečo, posledné vydanie. Ak je verzia novšia, stiahnite prenosný balík vhodný pre systém a nahradí súbory programu na mieste: tie, ktoré sa používajú, sú premenované na *.staré aktualizácie a vymazané pri ďalšom reštarte. Nastavenia, archív, dátový priečinok a C ALL3. TXT sa nedotýka. Nová verzia je aktívna od nasledujúceho začiatku: pri inštalácii dokončené okno navrhuje reštartovať okamžite; pre naliehavé opravy, označené [povinné] v Release, reštart nemožno odložiť. Stav kontroly a používaná verzia sú zapísané v dolnej časti stavového riadku; vypnúť aktualizácie = 0 v LIVETROPOVHF.ini. Na Mace je aplikácia podpísaná a nemení sám: program len hlási, že existuje nová verzia.

Obraz LIVETROPOVHF s číslom verzie sa objaví na začiatku; na konci program žiada o potvrdenie a vďaka za použitie.

## 12b. Záznam chýb a stavový riadok

Akákoľvek neočakávaná chyba sa končí v data/errori.log, dokonca aj s odhlásením výpočtu: dátum a čas, verzia, systém, otvorená karta, úplná chyba a posledné riadky toho, čo program robil. Chyba rozhrania už nezatvorí program: v stavovom riadku sa objaví " ano anotovaná chyba " (kliknutím: otvorte register). Pri ďalšom spustení sa program pýta, či poslať správu: otvorí prehliadač na signáli GitHub už kompilovaný, s nomináciou, lokátorom a užívateľským menom PC odstránené, ktoré skontrolujete a odošlete s bezplatným účtom GitHub. Log tiež končí pri každej kontrole aktualizácie.

V stavovom riadku sú kliknuté a prinesené zdrojové gule (DX cluster, RBN, PSKReporter, MQTT, UDP); komiks hovorí, prečo je zdroj vypnutý: odmietnutý port, uzol na strane servera, žiadna odpoveď, alebo UDP počúvanie, ale žiadne balíky z WSJT-X, JTDX alebo MSHV. Jedným kliknutím na číslo verzie okamžite skontrolujte aktualizácie.

## 13. Platformy

Externé knihy: Avalonia pre [12] rozhranie, MQTTnet ako MQTT klient pre real-time stream PSKReporter [13], Microsoft. Data.Sqlite pre archív [14].

C# on.NET 10 (LTS) [15] with Avalonia interface, publiced as an independent executable for Windows 64 and 32 bits, Mac Intel and Apple Silicon, Linux Debian and Raspberry Pi (64 and 32 bits). Zaznamenané limity: Windows 10/11; macOS 14 alebo neskôr, s aplikáciami podpísať alebo otvoriť vpravo → Otvoriť; Malina z Pi 2 / Zero 2 W hore (ARMv6 modely nie sú podporované.NET).

## Ďakujem.
Vďaka I5MZY Massimo Zannoni za testovanie na Windows 7 a Windows 8.

Vzduchové zariadenia: ITU-R P.453, digitálne produkty študijnej skupiny 3 (© ITU), používané na osobné a nekomerčné účely.

## Hlavné zdroje

1. S. Heck LA0BY, Troposferické režimy, kontrolný zoznam udalostí (predpoveď Hepburn/ F5LEN, vysoké teplotné profily s Windy, DX cluster ako dôkaz otvorenia), pozorovanie, že iba prognózy mapy nie sú dostatočné pre extrémne trasy.
2. J. N. Gannaway G3YGF, november 1983 (z rovnomenného článku o rádiovej komunikácii, RSGB, august 1981). Formuly straty a difúzie voľného priestoru, ekvivalentný polomer 8497 km, termín lokality 10 dB na horizont, klimatické obdobie 0,2 · (N - 310), kapacita zariadenia a pomer signál/hluk, napríklad pri 144 MHz.
3. W. R. Hepburn, DX Info Centre, Indexová stupnica a hodnota význam, predpoveď na 6 dní.
4. F5LEN, predpoveď pre Európu (tropo.f5len.org/predpovede-pre-európsku: zoznam obrázkov v skripte stránky, legenda triedy Disturb... Duct in the image) a svetová predpoveď (tropo.f5len.org/WW) a poznámka Index založený na reaktivite v jednotke N, 40 krokov od 3 hodín, aktualizácia 4 krát denne.
5. mqtt.pskriporter.info, distribúcia MQTT v reálnom čase reklamy pskreporter.info (vypočítaný M0LTE, tok P. Gladstone použitý s povolením). Dvere 18831886, pskr/filter/v2 tematická schéma.
6. Súhrn projektu 8/11/2026 a Go test (core.go, gui.go) poskytol autor programu. Ad Pravidlá, negatívny filter, segmenty šírky pásma, riadenie klastrov telnetu, témy, jazyky a prenosnosť.
7. C ALL3.TXT, nominačná databáza → JTDX prenajímateľ (I. UA3DJY, A. ES1JA skript, JTAlert/hamspots.net dát). Formát súboru a počet mien overených v projektovom súbore.
8. WSJT-X, zdrojový kód, NetworkMessage.hpp súbor (opis UDP protokolu: tep srdca, stav, dekód). Všeobecný odkaz nie je pripojený; JTDX a MSHV používajú rovnaký formát.
9. Reverse Beacon Network (reversebeacon.net), servisné stránky telnetového uzla. Porty 7000 (CW, RTTY) a 7001 ( FT8), prihlásiť sa len s názvom.
10. D. Mills, RFC 4330 Formát balenia, výpočet odpadu a oneskorenie návratu. Všeobecný odkaz, nie je pripojený.
11. NTP Pool Project (ntppool.org); INRIM, NTP time service (ntp1/ntp2.inrim.it); PTB (ptbtime1.ptb.de). Servery používané na kontrolu času. Všeobecný odkaz, nie je pripojený.
12. Avalonia UI, oficiálna dokumentácia (docs.avaloniaui.net). Multiplatformové rozhranie, témy a rozloženie zľava doľava. Všeobecný odkaz, nie je pripojený.
13. MQTTnet,.NET knižnica pre MQTT (gitub.com/dotnet/MQTTnet). Klient používaný pre tok PSKReporter. Všeobecný odkaz, nie je pripojený.
14. SQLite (sqlite.org) a Microsoft. Data.Sqlite (learn.microsoft.com). Archív v jednom spise. Všeobecný odkaz, nie je pripojený.
15. Microsoft,.NET 10 Platformy a limity uvedené v oddiele 13. Všeobecný odkaz, nie je pripojený.
16. ECMA-376 Štruktúra súboru.docx a rovnice. Všeobecný odkaz, nie je pripojený.
17. Zoznam 28 jazykov YA-MS wiki, ktorý poskytol autor programu.
18. Open-Meteo, predpoveď počasia API (open-meteo.com) s údajmi z modelu NOAA GFS. Tlak na úrovni mora pre izobare (pozri dokument 2).
19. J. P. Snyder, Všeobecný odkaz, nie je pripojený.
20. Astronomický Almanac, sekcia D, Zvykol rozpoznať spojenia EME. Všeobecný odkaz, nie je pripojený.
