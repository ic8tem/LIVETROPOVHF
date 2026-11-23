# Príručka prognóz

> Strojový preklad z taliančiny: v prípade pochybností platí talianska príručka.

Záložka Predpoveď vypočíta šírenie tropoféry sama o sebe: index mapa na viditeľnej oblasti a signál poskytnutý medzi vašou stanicou a zodpovedajúce jeden. Táto príručka opisuje všetky údaje, ako sa vypočíta a z ktorého zdroja pochádza. Čísla v hranatých zátvorkách sa vzťahujú na zdroje v dolnej časti; v programe majú všetky údaje z výsledkov komický (simbolo i) s rovnakým zdrojom a odkazom na odsek.

## 1. Čo robí a čo robí

Predpoveď vertikálneho profilu teploty a vlhkosti modelu GFS [1, 2] odvodzuje reaktivitu vrstvy vzduchu vrstvou a odhaduje, do akej miery sa signál v porovnaní s normálnou atmosférou zvyšuje. Na trase dodáva skutočnú zem (Copernicus GLO-30 [3]), difrakciu, troposcatter, kanály a zvuk prijímača.

Nie je to opatrenie: je to odhad na modeli počasia so vzdialenými úrovňami stovky metrov. Slúži na porovnanie situácií (dnes lepšie ako včera, o 70 cm lepšie ako 2 m, to je lepšie ako to), nie je zaručiť pripojenie. V odseku 14 sa vysvetľuje, ako zbierať dôkazy na jeho potvrdenie.


> "Zákaz nemožno zovšeobecňovať, ale študovať vo vzťahu k miestnej morfológii.
> DF5AI IK3XTV

Je to princíp, ktorý vedie výpočet bodového bodu: každá trasa sa vypočíta na svojej pôde s obzormi, morom a reliéfmi; mapa poskytuje všeobecný obraz, lieči odpoveď.

## 2. Jazykové príkazy

- Pásmo: 144, 432 alebo 1296 MHz. Mapa a výpočet použite zvolenú kapelu.
- Teraz: kurzor pohybuje predpoveď odteraz až do 72 hodín dopredu, do 3 hodín.
- Predpoveď aktualizácie (viditeľná zóna): stiahnuť GFS profily na mriežke najviac 240 bodov pokrývajúcich viditeľnú mapu. Rozstup mriežky (0,5° alebo viac) závisí od zväčšenia.
- Stop: prerušiť sťahovanie alebo výpočet.
- Detaily na zobrazenie (pravý panel mapy): tri úrovne, Essential, Normal and Complete, zapnite skupinu políčok; každé políčko sa potom môže meniť ručne. Škatule sú v troch skupinách:
  - Na mape: izolinee del Gain, tiež 3 Farby vždy zostanú.
  - Pri výpočte signálu: všetky riadky výpočtu, plochý profil Zeme, výpočtový denník a podrobný protokol súborov.
  - Opravy a živé údaje: korekcie počasia (Mizme, subsidence), air-sea kontrast, priemer s škvrnami (corridoi), APRS 2 m skoky a LoRa, nad horou bez strát (odseky 4 bis, 9 bis, 9 quater a 10 bis).
  - Essential drží len izolíny, počasie a médiá korekcie s škvrnami; Normálne pridáva isobare, inverzie, počasie značky, priamy výhľad, rovná zem, vzduch-more a APRS; Kompletné zapne. Úroveň iba mení to, čo vidíte a aké opravy zadajte: výpočty zostávajú rovnaké.
- Zdroj údajov a historický prípad: odkiaľ pochádzajú profily a ktorý deň sa pozriete (odseky 3 bis a 3 ter).
- Moja stanica a korešpondent: Locator, antény, napájanie, káble, hluk (odseky 7 a 12).
- Vypočítajte signál a pokrytie v optike: dve výpočty na zemi (odseky 8 a 13).
- Register výpočtov: každý krok s použitými číslami, vrátane súboru Data/ prognózy-calcoli.log.

## 3. Od údajov GFS k profilu

Pre každý bod mriežky a každú hodinu sme si prečítať tlak na zemi, teplota a vlhkosť na 2 m, vietor na 10 m, výška hraničnej vrstvy, CAPE a teplota a vlhkosť vzhľadom k 1000, 975, 950, 925, 900, 850, 800, 700, 600 a 500 hPa. Od 1.3.0 môžete tiež prečítať vietor na 850 hPa a vertikálnu rýchlosť na 850 a 700 hPa (odsek 9 bis).

Úrovne sa vypočítajú pomocou ipsometrického vzorca a virtuálnej teploty; úrovne pod zemou sú obmedzené. Pre každú úroveň:

- N = 77,6/T · (P + 4810·e/T), s T v kelvine, P a v hPa a saturačným tlakom ITU-R P.453-14, eq. 9, s korekčným faktorom EF (rekontrolovaným v úradnom texte) [4].
- M = N + 157·h, s h v km: modifikovaná reaktivita. Tam, kde M ide hore, je spojka.

## 3a. Zdroj údajov

Menu Zdroj dát vyberá, odkiaľ profily pochádzajú:

- Automatické (odporúčané): GFS najprv z NOMADS [14], NOAA služba, ktorá distribuuje model: pre každú chvíľu program žiada dva výrezy oblasti vo formáte G RIB2 a číta ich od seba; NOMADS nemá denný limit, len jednu minútu, že program rešpektuje. So širokými mriežkami (od 0,5° do hore) použite súbory na 0,5° alebo 1. každé 3 hodiny, oveľa ľahšie. Body, ktoré NOMADS nedáva, alebo všetko, ak NOMADS nereaguje, spýtajte sa Open-Meteo [2]. Tieto dva zdroje poskytujú rovnaký model a rovnaké preteky, takže môžete miešať v rovnakej mriežke.
- GFS z Open-Meteo: iba Open-Meteo, ako v 1.2 (na hranici za minútu sa očakáva).
- GFS z NOAA NOMADS: iba NOMADS. Pomalšie k stiahnutiu (dve súbory za hodinu, pol sekundy medzi jednou žiadosťou a druhou neprekračovať limit NOAA), ale nespotrebuje limit Open-Meteo.
- ICON-EU (DWD) a ECMWF IFS 0,25°, z Open-Meteo: modely iné ako GFS, ktoré sa uplatňujú v celej sieti a na trasy. Nemiešajú sa s GFS: porovnať dva modely súčasne je jednoduchý spôsob, ako zistiť, ako je predpoveď neistá. ICON-EU sa vzťahuje len na Európu; ECMWF nemá všetky úrovne tlaku GFS (chýbajúce úrovne skoku) a nemusí mať limitnú vrstvu.

Použitý zdroj je napísaný v stavovom riadku a v prvom riadku výsledkov (Data Source), s koľkými bodmi pochádzal z každej služby a koľko z vyrovnávacej pamäte. Disk cache je oddelený modelom.

## 3b. Historické prípady

Historické Menu case hodnotí zdokumentované otvorenie archívu Open-Meteo [15] predpoveď GFS: mapa sa presunie do oblasti prípadu, kapela sa nastaví sama a čas kurzora odchádza od prvého dňa prípadu. Stlačením aktualizácie predpovede stiahnete profily týchto dní; výpočet signálu funguje ako pre aktuálnu predpoveď. Slúži na porovnanie modelu s tým, čo sa naozaj stalo.

Prvý prípad je, že zo 14. a 15. augusta 2022 povedal v QST (november 2022, "svet nad 50 MHz" [23]): otvor pod vysokým tlakom medzi Manitoba a Kansas/Missouri, od 144 do 1296 MHz takmer všetko v FT8, s kanadským rekordom 10 GHz 1228,56 km v Q65 a signály až 1941 km. Druhým je otvorenie medzi 29. decembrom 2021 a 2. januárom 2022 o Tyrrhenian a západnom Stredozemí na 144 MHz, zo správ IC8TEM: prvé otvory na 29 (až do 737 km), širšie na 31 ( IT9GSF Pre staršie prípady (napr. september 2000) archív Open-Meteo nemá žiadne údaje. V archíve chýba výška hraničnej vrstvy: tam sa neuplatňuje jej korekcia. EB5EA

## 4. Index a farby

Pre každú vrstvu medzi dvoma úrovňami program porovnáva pokles N s prahmi tabuľky 6.5 Bean a Dutton [5]: napríklad medzi 1000 a 850 hPa pokles až 60 N/km je normálne, viac ako 100 N/km je super-refrakcia. Super-refrakčná vrstva má hodnotu 10 až 15 dB zisku; spojka, ktorá zachytáva pásmo od 20 do 25 dB podľa deficitu M. Silný vietor do zeme, vysoká medzná vrstva a vysoká CAPE znižujú zisk, pretože odstránia alebo zlomia vrstvy [5, § 4.4].

Farba je odhadovaný prírastok v dB, s pevnou stupnicou rovnou vo všetkých témach:

| Zisk | Farba | Význam |
| Menej ako 7 dB | Nikto. | takmer normálna atmosféra |
| 710 dB | modrá | gradient nad normálom |
| 1013 dB | zelená | super-refrakčná vrstva |
| 13 | žltá | Pevná vrstva, ale bez potrubia pre pásy |
| 1618,5 dB | oranžová | veľmi silná vrstva alebo príliš tenký kanál |
| 18,5 - 21 dB | červená | Pásmo na chytanie konduitu |
| od 21 dB | Fialová | Silné a hlboké potrubie |

S displejom tiež 3 Bodkovaná tabuľa vymedzuje oblasť pokrytú vlastnou predpoveďou a zobrazuje maximálny viditeľný zisk: ak je pod 7 dB, mapa zostáva bez farieb, pretože atmosféra je takmer normálna, nie preto, že chýbajú údaje. Pri sťahovaní nového kliknutia na Update predpoveď neprerušuje; zastaviť je Stop.

Farby sú nepriehľadné na 82%, takže pozadie témy ich jednoducho zmení. Lokátor mriežka a názvy štvorcov sú navrhnuté cez farby. Kde vaša predpoveď nie je tam, ale Hepburn alebo F5LEN dať 5 alebo viac šachových kariet sa objaví s farbou tohto zdroja.

Mapa posudzuje každý bod sám. Výpočet signálu namiesto toho riadi, že vrstva je súvislá pozdĺž trasy (odsek 9): trasa, ktorá prechádza veľmi žltej môže dať menej, než mapa si myslíte.

## 4a. Prognóza sprostredkovaná reklamami: koridory (od 1.3.3)

Copyright Policy: Samotný odkaz nesmie nikdy nakresliť kanál. Izolovaným a krátkym príjmom môže byť rozptýlenie prechádzajúceho lietadla (balík APRS je veľmi krátky), otvorenie niekoľkých minút alebo chyba, a dal "drop-vod" by klame. Program rozpozná koridor, keď v rovnakom pásme:

- aspoň 3 páry nezávislých staníc (žiadna spoločná stanica) pracujú na trasách s dĺžkou najmenej 300 km;
- trate sú rovnobežné: azimut v rozmedzí ±15°, priemerné body v rozsahu 150 km od spoločnej osi;
- všetky za posledných 30 minút, pričom lúč sa opakuje vo vzdialenosti najmenej 10 minút (pretrvávanie);
- pre ARS sa skoky počítajú len vtedy, ak sa rovnaký skok zopakuje;
- od 1.3.4 sa trasy na 1500 km počítajú iba vtedy, ak lúč pochádza z najmenej dvoch rôznych zdrojov (napr. PSKReporter a klaster): z jedného zdroja do vzdialenosti 2 m sú častejšie EME, rozptyľujúce meteory alebo nesprávne lokalizátory ako tropo.
- z 1.3.4 nepočítajte stanice "na raju": kto má v poslednej hodine najmenej 5 korešpondentov nad 1000 km rozložených na ventilátor najmenej 150°. V 2 m je metrope smerové; prijímač, ktorý "voľný" spoločne Izrael, Portugalsko, Anglicko a Taliansko od 2000 V 17 Archív 72% ubytovanie v 2 m cez 1000 km prišiel zo staníc tak; na 22/11 pri 10:53 UTC boli OH6HLH, OZ1ITS, F4LUK, OM8ABC a SP2JLR, a s filtrom najdlhší koridor zostupuje z 2175 km (regulácia 1.3.3) na 615 km. I5MZY

Zdroje: klastre (nie tie označené AS/ACS), PSKReporter prostredníctvom MQTT, ich QSO a WSJT-X/JTDX/MSHV dekódovanie a APRS skoky v 2 m a LoRa 433/868 MHz.

2 m a LoRa. Program číta APRS-IS iba na čítanie (rotate.aprs2.net, filter on Europe) a extrahuje rádiové skoky medzi známymi stanicami (zdroj → digipeater → Igate), ako je VHF mapa DXView a APRS PropView (RF-YVY, Apache 2.0). Tri pásma majú hodnotu: 2 m, LoRa 433 MHz (pásmové koridory 432) a LoRa 868 MHz (korridoi, ktoré sa počíta za 1296: vedenie, ktoré prináša 868 MHz aj najvyššie frekvencie). Kapela pochádza z LoRa tocall (apris-deviceid zoznam) a z frekvencií alebo slov v komentári; skok je hodný len medzi dvoma stanicami rovnakej kapely.

Jemným bodom sú aktívne opakovače: signál prijatý na 500 km môže byť dorazil s dvoma alebo tromi digipeater retransmisie. Skok sa preto počíta len vtedy, ak je medzi dvoma skutočnými a po sebe nasledujúcimi vysielačmi:

- trasa musí byť vysledovaná: každý digipeater, ktorý znovu odoslal svoj názov a skok sa meria medzi jedným a druhým; alias používaný bez názvu (W IDE1 *, W IDE2 *, regionálna prezývka) zlomí reťaz a tento úsek sa nepočíta;
- skryté retransmisie: program si pamätá cestu, ktorú si každý zdroj zvyčajne pýta (napríklad W IDE2 -2). Ak balík prichádza s skokom v menšom (W IDE2 -1) a žiadny názov to nevysvetľuje, digipeater ho znovu odoslal bez sledovania a obal sa vyradí; zdroje, ktoré sa predtým nevideli, sa nepočítajú;
- digipeater v strede cesty: ak medzi dvoma stanicami, viac či menej na ceste, tam je digipeater rovnakej aktívnej kapely v posledných troch hodinách, skokom je vyradený, pretože to mohlo byť retransmitovaný (to je najmä pre digipeater LoRa, že retransmit bez zmeny cesty);
- nerádiové balíčky (qAC, qAX, TCPIP), tie, ktoré sa prevádzkujú prostredníctvom internetu prostredníctvom rádia z IGate (tretie strany), HF a rôzne frekvencie, stanice vo výške viac ako 1500 m (palloni, lietadlo) a skoky cez 1500 km.

Vplyv na mapu (od 1.3.4). Vyťahuje len oblasť, v ktorej je garantované minimum platné: pásmo ±60 km okolo strednej časti každej trasy (z 15% na 85% jej dĺžky), nie na staniciach na koncoch, kde vedenie nemusí byť. Pásma všetkých chodieb sú zostavené dohromady, v neutrálnej farbe (text) a semi-transparentné: prekrytia nie sú pridané, a farba nie je zamieňať s fialovou stupnice (21 dB a ďalej). Štítky (položky, priemerná vzdialenosť, minimálny zisk) sa skenujú navzájom; prechádzanie s myšou na štítku sa objavujú časti koridoru.

V oblasti a len v blízkosti aktuálneho času (±90 minút), predpoveď nespadá pod minimum. Dôvodom je: ak stanice naozaj prejsť, šírenie je tam, bez ohľadu na model počasia hovorí.

Pretože krátke trasy stoja málo (od 1.3.4). V 2 m, medzi 300 a 600 km, dve dobré stanice (asi sto wattov, antény, FT8 alebo CW) pripojiť takmer každý deň aj s normálnou atmosférou, pre troposcatter: signál rozptýlený malé nepravidelnosti vzduchu v spoločnom objeme dvoch zväzkov. Tri alebo štyri miesta na 500 km hovoria, že kapela funguje ako obvykle, nie že tam je otvor: GEKO Magazine n. 69 ( IU3QEZ) odhady pre troposcatter s 2×10 tečúcimi prvkami približne 440 km v SSB QRP, 510 km v SSB s 50 W, 600 km v CW QRP a 640 km v SSB s 500 W (zdroje, jedenásť kôl). Len viac ako 600700 km troposcatter už nestačí a rad paralelných spojení skutočne naznačuje vedenie alebo vrstvu. Až 1.3.3 koridor 450600 km zvýšil prognózu na 912 dB, a mapa ukázala zlepšenie, ktoré tam nebolo.

Východisková hodnota potom ide s priemernou vzdialenosťou trás:

- do 400 km: 3 dB;
- od 400 do 700 km: soľ lineárne od 3 do 10 dB (500 km → 5,3 dB; 600 km → 7,7 dB);
- od 700 do 900 km: od 10 do 16 dB; nad 900 km zostáva na 16 dB.

Pod 7 dB, čo je pod začiatkom stupnice, minimum zostáva, že: koridor je kreslený s jeho štítkom, je vidieť, že pásmo funguje, ale farby mapy sa nemenia (s rámčekom tiež ukazuje 3 Nad 7 dB sa hodnota váži, polovica a polovica, s počtom nezávislých trás (3 ťahy → 0,25; 6 alebo viac → 1) a s mediánom S/N vzťahov (−20 dB → 0,5; 0 dB alebo viac → 1; neznáme, ako v strapcoch, → 0,75), od 7 dB. Príklady: 3 trasy bez S/N na 500 km → 5,3 dB; 5 trás na 615 km → 7,7 dB; 3 trasy bez S/N na 900 km → 11,5 dB; 3 trasy na −20 dB na 900 km → 10,4 dB; 6 trás na 0 dB na 900 km → 16 dB.

Minimálna vzdialenosť zostáva 300 km: zvýšenie na 500 km by tiež odstránilo skutočné otvory krátkeho dosahu (napríklad vedenie na Tyrrhenian na 400 km, ktoré je uznávané predpoveďou počasia a veľmi silnými signálmi viac ako len zo samotnej vzdialenosti). V archíve I5MZY, 22/11/2026 pri 10:53 UTC, s týmto pravidlom a lúčovým filtrom najdlhšie koridory boli 615 km (7,7 dB) a 540 km (6,3 dB); s pravidlami 1.3.3 dosiahol 2175 km a 16 dB.

Až do 1.3.3 bolo minimum platné na celom obdĺžniku koridoru a záviselo len od vzdialenosti (od 10 dB do 300 km do 16 dB do 900 km): s mnohými dlhými koridormi spolu (napríklad 22/11/2026, dvanásť koridorov do 2620 km) zdvihol takmer celú Európu na 16 dB.

Výrastkové krivky už nie sú kreslené To vypne s "Prevision sprostredkované škvrnami."

## 5. Voliteľné čiary a porovnanie s tlakom a inverziou

Ziskové krivky: čiary na 7, 10, 13, 16, 18,5 a 21 dB, okraje mierky, nakreslené na poli vyhladené metódou pochodových štvorcov, s označením v dB.

Isobare: tlak na úrovni mora každých 4 od rovnakého GFS, do hodiny predpovede, s centrami H a L.

Tepelná inverzia: krivky pokryté množstvom vzduchu pri 925 alebo 850 hPa je teplejšie ako pri 2 m, pri +1, +3, +5 a +8 °C.

Pod ovládačmi čiara dáva súhlas s tlakom a naopak na viditeľnej zóne: Pearsonov korelačný koeficient, −1 až +1. Kanály sa často rodia v anticyklónoch (vzduch, ktorý padá teplo a tvorí opačnú stranu), takže pozitívna dohoda s tlakom je normálna; ale vnútri tej istej anticyklopédie zisk veľa mení medzi morom, pobrežím a horami. Samotná inverzia nestačí: ohýbať vlny len v prípade, že nad vzduchom je dokonca sušič [5]. Linka slúži vidieť, teraz, ktorý z dvoch najlepších vysvetľuje farebné oblasti. Rovnaké porovnanie je napísané v registri.

## 6. Potenciálne potrubia: dvojitý výpočet

Úroveň GFS sa pohybuje od 200 do 1000 m: tenká inverzia sa šíri a model nevidí spojku. Ak medzi dvoma úrovňami pokles N prekročí normálne (40 N/km), vzduch je stabilný (tepelný gradient pod 3 K/km) a vyššie je suchý (špecifický pomer vlhkosti pod 0,8), program považuje potenciálny kanál a vypočíta ho dvoma spôsobmi:

- Výpočet A: všetok koncentrovaný prepad o 50 m.
- Výpočet B: rovnaký pokles rozložený na typickej hrúbke potrubí tejto oblasti, prevzatých z klimatických máp ITU-R P.453 [4] (na zemi alebo zvýšených).

Pre deficit a pre maximálnu zachytenú vlnovú dĺžku sa drží najnižšia hodnota: je to obozretný odhad.

ITU-R P.453 údaje sú © ITU a môžu byť použité na osobné a nekomerčné účely, citovať zdroj, ale nemôžu byť prerozdelené: pre to nie som v programovom balíku. Ak chcete zapnúť výpočet B, stačí stiahnuť mapy P.453 z ITU v záložke: program, po zobrazení podmienok použitia, stiahnuť archív potrubí zo stránky ITU (výška_ a_ surface_duct.zip) pre osobné použitie užívateľa, extrahovať súbory v dátach/ P453 a zapnúť výpočet bez reštartovania. Prípadne si stiahnite digitálnu produktovú stránku ITU-R Study Group 3 (itu.int, ITU-R Study Group 3, Digital Products, P.453, Povrchové dáta a Výťahové údaje) a položte sedem S_OCCUR. TXT, S_THICK.TXT, S_STREN. TXT, E_OCCUR.TXT, E_base.T Bez súborov program používa iba výpočet A a zapíše ho do karty a do registra. Pomer medzi deficitom A a typickou intenzitou P.453 je anomália: Koľkokrát je dnešné potrubie silnejšie ako normálne. P453

## 7. Stanice a lokalizátory

Majiteľ môže mať 4, 6, 8 alebo 10 znakov. S 8 alebo 10 znakov je pozícia presná a pôdny podiel pochádza z programu Copernicus GLO-30: výška antény stačí zo zeme. So 4 alebo 6 znakmi je námestie príliš veľké: je lepšie napísať kvótu antény na mori; ak 0 program zostáva odhadom s SRTM pôdy v strede námestia plus výška od zeme a signalizuje to vo výsledkoch. Pre zodpovedajúcu štandardnú výšku je 3 m od zeme; s podielom mora 0 sa používa pôda SRTM plus výška.

## 8. Pozemok, difrakcia a troposcatter

Profil trasy sa odoberá z programu Copernicus GLO-30 každých 30 m (šírka viac ako 120 km). Early radius early equivalent is k = 157/(157 + ΔN), with ΔN the drop of N in the first km from the GFS Profils pozdĺž trasy [6, 7].

Úľavy v blízkosti staníc (od 1.3.2, autorské kritérium). P.452 platí uhol horizontu lineárne, 0,573 dB na mrad: úľava niekoľkých desiatok metrov niekoľko sto metrov od antény stojí desiatky dB, oveľa viac, ako je pozorované. Z tohto dôvodu môžu reliéfy do 20 km od každej stanice pridať až 6 dB: uhol horizontu vďaka nim je obmedzený na uhol najvzdialenejších reliéfov plus 10,5 mrad. Platí pre vašu stanicu a pre korešpondenta. Linka

Zdroj pozemku (od 1.3.2). Kvóty pochádzajú z programu Copernicus DEM GLO-30 [3]: 1 druhý oblúk, more je 0 a pobrežie je čisté. Až do 1.3.1 program využíval Terrain Tiles "skadi" AWS. V blízkosti pobrežia tieto námestia mix SSRTM s nízkym rozlíšením morského dna dáta a rozšíriť podiel ostrova na mori: okolo Capri vytvoriť duch kruh 80 a 100 m medzi 0,5 a 1 km od pobrežia. Dňa 21. novembra 2026 IC8TEM oznámil neexistujúci horizont na severozápad; porovnanie bodu za bodom s Copernicusom ho potvrdilo (94 m na mori severozápadne od Marina Grande vs. 0). Tento falošný horizont +2,6° stojí približne 26 dB troposcatteru na pobrežných staniciach. Skadi tares zostávajú len ako rezerva, keď Copernicus nereaguje. Kopernikové tabule vážia 1530 MB a sťahujú sa raz; tabule iba mora neexistujú a majú hodnotu 0.

- Voľný priestor: Lfs = 92,44 + 20 log f(GHz) + 20 log d(km) [8].
- Difrakcia: Bulingtonova metóda v reálnom profile plus ITU-R P.452-16 § 4.2 [6] delta-Bullingtonova guľová korekcia krajiny s ITU-R P.526 [9] sférická strata.
- Prvá Fresnelová zóna: polomer √(λ·d1·d2/d); voľná, ak zem opúšťa aspoň 0,6 polomeru. Pre každú prekážku je zobrazený parameter [9] Knife Board V a J (VV)..
- Pri konštrukcii profilu je priamy polomer zelený v optickom a červenom pohľade, ak prechádza pod zemou alebo pod zakrivením zeme; červené bodky sú prekážky, ktoré napadajú 1. oblasť Fresnelu (fialová bodkovaná čiara). Na mori "červený bod" v nadmorskej výške 0 m nie je hora: je to more, ktoré stúpa na zakrivenie. Prechod s myšou na polomere alebo na mieste sa objaví komik s vysvetlením a hodnotami.
- Keď reliéf v blízkosti antény uzatvára smer (napríklad horizont desiatok stupňov), difrakcia a troposcatter spôsobujú straty stoviek dB: nad 300 dB program píše "> 300 dB" a "smer uzavretý susednou prekážkou," signál "< −230 dB" a pomery S/N " -60 dB." V takom prípade je vhodné pozrieť sa na
- Troposcatter: ITU-R P.452-16 eq. 45 až 50% času [6] s uhlovou vzdialenosťou od horizontov a N0 od profilov.
- Bez tropa: súčet právomocí difrakcie a troposcatter, nikdy pod voľný priestor.

## 8 bis. Troposcatter: štyri modely a neistota

Vedľa P.452 program počíta troposcatter s tromi ďalšími modelmi, rovnako ako TropoLink [16] program, a ukazuje ich rozdiel ako neistotu:

- NBS TN101 § 9 [25] (od 1.3.5), medián: L = 30 log f − 20 log d + F(θd) − F0 + H0 + Lgp. F(θd) je útlmová funkcia prílohy III v jej analytických formách (Ns 301) s korekciou pre Ns, H0 funkcia zvýšenia frekvencie § 9.2: pri 144 MHz, s nízkymi anténami v vlnových dĺžkach, odraz zo zeme čiastočne vymaže pole v dolnej časti spoločného objemu a strata soli 3 Ns je to v hodinových profiloch GFS, účinné horizonty a výšky pochádzajú z profilu Copernicus, Lgp = 0,07·e^(0,055(Gt+Gr))))) je strata otváracieho spojenia. Je to tá istá forma, akú ITM používa vo svojom interiéri, ale s reaktivitou dneška namiesto klímy. Testuje na plochých úsekoch pri 144 MHz s anténami pri 20 m: v rozmedzí 2 TN101 H0

- ITU-R P.617-5 [17], medián: L = F + 22 log f + 35 log θ + 17 log d + Lc, s f v MHz, θ v mrad, d v km, F = 0,18· N0 ·e^ ((-hs/7,35) − 0,23·ΔN a Lc = 0,07·e^(0,055(Gt+Gr)). N0 a ΔN sú zvyčajne prevzaté z klimatických máp ITU; tu používame každú hodinu profily GFS ( N0 ·e^( - hs/7,35) je reaktivita na zem, ΔN pokles v prvom km), takže tiež troposcatter nasleduje čas.
- RSGB VHF/UHF Handbook Formule, DL2FZN [18]: L = Lfs + 24,44 + 4,283 ln f + 0,06656 d. Sklon so vzdialenosťou je takmer rovnaký ako pri PROPBER (0,067 dB/km).

Spoločný objem (od 1.3.1): je bod, v ktorom je prekročený polomer oboch staníc, každá naklonená svojmu uhlu horizontu, na Zemi s ekvivalentným polomerom ae = k·6371 km. z TX polomer stúpa ako z = hTX + x·θt + x2/(2·ae); Vzdialenosť x = (hRX − hTX + d·θr + d2/(2·ae) / θ, s θ = d/ae + θt + θr. S plochými obzormi a anténami s rovnakou kvótou má základňa hodnotu d2/ (8·ae): cca 1,3 km až 300 km, 3,7 km až 500 km, 9,4 km až 800 km. Čím vyšší je objem, tým viac vzduchu je suchý a vzácny a čím menej sa šíri: z tohto dôvodu viac ako 800 a 1000 km troposcatter vyčerpá. Výsledky sú podiel a poloha základne, reaktivita, gradient a vlhkosť GFS v danej nadmorskej výške a ekvivalentná vzdialenosť Bray (QST, november 1961): každý ďalší alebo menší horizont je vzdialený približne 111 km. Ref.: prezentácia "Cez horizont vo VHF" od IU3QEZ (GEKO Magazine n. 69).

Línia Troposcatter, ostatné modely ukazujú dve straty a neistotu ako polovicu rozdielu medzi najoptimistickejším a najpesimistickejším modelom. Sú to len porovnania: strata bez tropa zostáva strata P.452.

## 8 ter. popoludňajší troposcatter nad reliéfmi (od 1.3.4)

IK3XTV, z Thiene ( JN55, s Alpy na 10 km), pracuje v letných popoludňajších hodinách južné Nemecko na 400 a 500 km, bez dohľadu, so silnými signálmi, ale s rýchlym a hlbokým evanscence; autor pripísal im "konvektívne bunky zarovnané ako vlak šošoviek." Pozorovanie je potvrdené; vysvetlenie prijaté v programe je ďalšie, fyzicky pevnejšie.

V popoludňajších hodinách slnko roznieti vzduch nad reliéfmi a limitná vrstva stúpa na 2 a3 km. Na vrchole vlhkého vzduchu sa nachádza suchý vzduch, ktorý je hore: turbulencia indexu lomu (veľkosť, ktorou sa riadi troposcatter) je maximálna. Keď sa tento vrchol dostane k spoločnému objemu trasy, ktorá na 400 a 500 km je asi 2 a3 km, troposcatter je posilnený. To vysvetľuje popoludňajší čas, vysoký tlak (tichý valcový, silný ohrev) a rýchlu evanjeliu, typickú difúziu a nie kanáliky. Konvektívne bunky nepôsobia ako šošovky: vytvárajú turbulencie a signál šíria turbulencie v hornej časti prepracovanej vrstvy.

Dôsledky pre predpovede čítania:

- na zemi prvé popoludnie je najhorší čas pre nízke kanály a vrstvy, ale nie pre troposcatter za horami: dve veci by sa mali čítať samostatne;
- program už má dve údaje, ktoré slúži, výšku limitnej vrstvy poskytovanej modelom a podiel spoločného objemu trasy (spoločného objemu) vo výsledkoch): keď sú blízko, popoludňajšia horská liečba môže ísť lepšie ako výpočet v normálnej atmosfére;
- z 1.3.4 výpočet bodu berie do úvahy: ak je spoločný objem na zemi, pozdĺž trasy sú reliéfy najmenej 800 m, Slnko je nad 15° a limitná vrstva presahuje 800 m a vrchol vrstvy pretváranie (model rýchlosti pôdy + limitná výška vrstvy) je do 500 m od základne spoločného objemu, troposcatter získa 3 dB; medzi 500 a 1000 m lineárny prírastok klesne na nulu. Linka "Troposcatter popoludní". So snehom pozdĺž trasy (aspoň 10 cm na zemi podľa modelu, Snow_hĺbková premenná Open-Meteo alebo SNOD GFS od NOMADS) sa výstuha neuplatňuje: na snehu zostáva pôda studená a opätovne tvarovaná vrstva nerastie. Hodnota 3 dB je predbežná a musí sa kalibrovať so skutočnými prepojeniami; vypne sa s úpravami počasia.

Zdroje: pozorovania F. Egano IK3XTV (Radio Rivista e www.qsl.net/ik3xtv,

## 9. Zakladacia vrstva pozdĺž trasy

Je jadrom výpočtu signálu. Po celej trase sa profily GFS nachádzajú okolo 25 km. Pre každú vrstvu tlaku najdlhší úsek, v ktorom je rovnaká vrstva prítomná v po sebe nasledujúcich bodoch; počíta sa len ako hrubé vrstvy kanálu aspoň 40 m [10].

- kontinuita: z akého km do akého km a zlomok trasy;
- horná a základná a sklon v m/km;
- pôda vnútri vrstvy: km, kde hory stúpajú nad základňou;
- Krytý pozemok: na mori je kanál takmer vždy, rozhodne o úseku pozemku [10]. Ak pozemok presahuje 10% trasy, použitý zlomok je minimálny medzi kontinuitou a pokrytím zeme.

Prírastok trope je prírastok vrstvy pre použitú frakciu. Ak je viac ako polovica profilov subrefraktívna, zisk je negatívny.

## 9 bis. Korekcie počasia: stabilita, úbytok, štrajk

Od 1.3.0 profil tiež poskytuje tri meteorologické informácie, ktoré môžu byť videné v registri (Regave Mistability (Misme)

- Stabilita misu [5, kapitola 6]. Misme viaže pole za horizontom na zložený parameter: M = a· (ge - 40 + b·ΔW), s a = 0,5 dB na N/km, ekvivalentný gradient ge a tepelná stabilita ΔW, práca na zvýšení vzduchu o 1 km. Program počíta ΔW = g∫(θ − θ0/θ0 dz medzi pôdou a 1 km nad, s θ potenciálnou teplotou; štandardná atmosféra má hodnotu okolo 56 J/kg. Termín pridaný k zisku je 0,07 dB pre J/kg nad rámec normy, limitovaný medzi −4 a +6 dB, a počíta sa len vtedy, keď pásmo nezachytí žiadna spojka (sklon je už v Bean a Dutton index). Kalibrácia je dočasná: musí sa overiť pomermi (odsek 14).
- Subsidence a hore kopcom [19]. Moler a Holden, na trasách 125 305 km k moru v rokoch 1365 a 9380 MHz, našiel najvyšší signál asi 2,5 dB v priemere so vzduchom prichádzajúce dole, že so vzduchom stúpa, a v prípade, že posilnenie asi 20 dB na 3 hodiny na začiatku subsidence, nasleduje pokles o 22 dB, keď sa vzduch vrátil do konvergencie. GFS zabezpečuje vertikálnu rýchlosť 850 a 700 hPa. Ak na oboch úrovniach klesne vzduch viac ako 1 cm/s a dôjde k spätnému alebo prudkému poklesu potenciálneho indexu lomu K (N uvádza adiabaticky na 1000 hPa, ktorý zdôrazňuje pokles suchého a teplého vzduchu z hornej časti [5]), pridá sa 1,5 dB; ak vzduch stúpne o viac ako 5 cm/s, vzlietnu 2.
- [20] Scirocco. Horúci a suchý vzduch zo Sahary nad chladnejším morským vzduchom dáva veľmi silné inverzie z východného Stredozemia do Gibraltáru. Značka S sa objavuje tam, kde pri 850 hPa vietor pochádza zo 100200° s najmenej 7 m/s a teplota je najmenej 16 °C, v Stredozemnom mori. Je to len náznak: zisk rozhoduje o vrstvách profilu.

Bez vedenia, ktoré zachytáva pásmo, celkový prírastok nepresahuje 16 dB, koniec žltej: opravy zviditeľňujú silnejšie vrstvy, ale neprechádzajú cez kanály (červené a fialové zostávajú v kanáloch).

- Perzistencia (od 1.3.1). Najväčšie otvory prichádzajú po dobrých vysokotlakových dňoch (VHF/UHF DX Book; Biancomano a Leyshon 1998, pravidlo 12 hodín; správy z 29/12/2/2022 rastú od prvého do štvrtého dňa). Pre každú bunku program počíta hodiny predchádzajúcich 24 so ziskom najmenej 13 dB a v pomere až 2 dB, len ak je zisk už najmenej 10 dB.

Ako čítať znaky na mape (Zmeniť počasie Znaky; od 1.3.2 symbol je veľký dvojitý a prechádza s myšou cez znamenie sa objaví komik s vysvetlením a hodnotami bunky):

| Značka | Farba | Čo to znamená? | Účinok na mape |
|---|---|---|---|
|  ↓  | zelená | subsidence: vzduchové kvapky (anticyklon), teplo pre kompresiu a suchosť, posilňuje spätný a udržuje ho pevný | priaznivé, +1,5 dB |
|  ↑  | červená | horský vzduch (predné, nízkotlakové, konvekčné): mieša nízke vrstvy a prerušuje inverzie a kanály | nepriaznivé, -2 dB |
|  S  | oranžová | Sirup: horúci a suchý vzduch zo Sahary nad čerstvým morským vzduchom | Priaznivá stopa, žiadna korekcia: rozhodnúť vrstvy |

Oblasť plná zelenej ↓ pod anticyklónom je klasickým znakom tvrdého otvoru; Červené ↑ šípky pozdĺž čiary hovoria, že vrstvy sa lámu, aj keď farba bunky je stále dobrá.

Poveternostné opravy box vypne tieto opravy vidieť model 1.2.

Validácia (odsek 14): na 593 spojeniach do 144 MHz medzi 29. decembrom 2021 a 2. januárom 2022 v porovnaní s tými istými trasami a o dva týždne neskôr je koeficient oddelenia (0,5 = žiadny, 1 = dokonalý) 0,79 bez opráv a 0,91 s opravami (pred strechou 16 dB). Rovnaké porovnanie ukazuje hlavný limit: kanál na mori v nízkej nadmorskej výške, ktorý v tých dňoch priniesol znaky medzi Kampániou a Liguriou, nie je viditeľný na úrovniach GFS.

## 9b. Správy 1.3.2: dva odhady, strop, vysoký tlak, more, stanice, hrebene

rozvážny a agresívny odhad. Prírastok zo gradientu narastá z 0 dB na limit normálu do 10 dB na prah superrefrakcie. Vo vrstve 1000 a viac bodov je obozretným prahom Bean a Dutton (100 N/km); Agresívna prahová hodnota je klasický super-refrakčný prah 79 N/km (k Výpočty bodového bodu ukazujú dve straty na boku; s potrubím, ktoré zachytáva gang zhoduje. Mapa používa opatrnosť.

Podkrovie 2500 m. Cesty a kanály sú vyhľadávané len až 2500 m nad zemou: čím vyššie antény amatérskych rádií nevyužívajú. S 1500 m validácia stratila separačný bod a 4% pripojení nad 16 dB, pretože v zime sú zvraty medzi 1500 a 2500 m.

Vysoký tlak zvýšený. +1 dB, ak aspoň 7 z 9 buniek v okolí znížilo tlak na more o 1020 hPa alebo viac a vietor je slabý v nízkych vrstvách (10 m pod 5 m/s, 850 hPa pod 8 m/s), s výnimkou vzostupných pohybov.

Morská hmotnosť. Pri výpočte bodu k bodu sa zisk vrstiev vynásobí 0,8 + 0,4·μ1, kde μ1 je faktor ITU-R P.452 (eq. 51) na dlhšej súvislej ploche pôdy: 1 na otvorenom mori, malé s dlhými úsekmi pôdy.

Stanice a vedené. Ak spojka, ktorá zachytáva pásmo, pokrýva iba časť čiary, podiel každej antény sa porovná so základnou a hornou časťou spojnice na začiatku a na konci krytej časti; zisk sa vynásobí 0,5 + 0,5·√(e^(-ΔhTX/300)·e^(-ΔhRX/300)), pričom Δh je vzdialenosť v metroch mimo kanála. S spojkou na takmer všetkých trasách je spojenie § 10.

Vrstva a hrebene. Ak horná časť hraničnej vrstvy GFS (rýchlosť pôdy modelu s najvyššou hodnotou hranice_vrstva_výška) presahuje hrebene trasy (maximálne lokálne viac ako 300 m, ak je najvyššia hodnota vyššia ako 500 m), vrstva prechádza cez hory bez rozbitia: +1,5 dB pre zlomok pokrytých hrebení. Je to pozorovanie IT9GSF na otvoroch zo Sicílie do Nemecka s priaznivé Tyrrhenian.

Úrovne blízko zeme na mori. So zdrojom NOMADS program tiež sťahuje povrchovú teplotu (na mori je SST), teplotu a vlhkosť vo výške 80 m a vo vrstve 30 a0 hPa nad zemou. Na námorných bodoch (model sadzby pod 5 m) sa tieto úrovne pridávajú do profilu, pod prvou úrovňou tlaku, vidieť potrubie na 100 a 200 m, že tlak úrovne nerieši. Na zemi sa nepoužívajú: nočné a denné vrstvy ešte nie sú platné. Open-Meteo vytvára len povrchovú teplotu.

Odparovacie potrubie. Od SST sa od teploty a vlhkosti do 2 m odhaduje výška odparovacieho kanála s neutrálnym logaritmickým profilom (ako v modeli Paulus a Jeske, bez korekcie stability): vzduch do vlasov vo vode je nasýtený do SST (98% pre soľ), a preto sa v prípade soli

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

limitovaná od 0 do 40 m. Na zachytenie 144 MHz by to trvalo asi 180 m (Kerr), potom 144 MHz odparovací kanál posilňuje málo; zo 432 MHz nahor, s nízkymi anténami na mori, veľa (odsek 11 bis). Zadajte parabolickú rovnicu a v riadku Opravy 1.3.2.

Čas dňa (návšteva). Program počíta výšku slnka na každom bode. Zo dňa na deň na zemi, so slnkom nad 15° a limitnou vrstvou nad 800 m alebo s vetrom nad 5 m/s, varuje, že pôdne kanály sú pretvárané; v noci, s slnkom pod -3°, vietor pod 3 m/s a limitná vrstva pod 300 m, varuje, že s pokojnou oblohou je možné viesť ožarovaním, ktoré padá po východe slnka. Nie na mori. Zatiaľ sú spozorované, nie čísla: December validácia nemá dostatok letných dní na ich kalibráciu. Toto sú dva body, ktoré autor považuje za dôležité z každodenného pozorovania.

Normálny profil CRPL (alternatívny). Namiesto 40 N/km pevne stanovil normálny pokles na základe exponenciálnej referenčnej atmosféry CRPL: N(h) = Ns·e^ (−c·h), s −ΔN v prvom km = 7,32·e^ (0,005577·Ns) (Bean and Thayer 1959). S mokrým vzduchom (Ns vysokým) normálne je strmšie a slúži silnejšie gradient dať zisk; v nadmorskej výške je sladšie. Ukázalo sa, že je to zlé, až kým potvrdenie nepovie, že to pomáha.

## 9 quater. Kontrastné more a šíre more: Jadranské more (od 1.3.3)

Rádiové vlny necítia morské dno: cítia kontrast medzi povrchom vzduchu a vody. Hĺbka sa počíta, pretože rozhoduje, ako rýchlo sa teplota mora riadi teplotou vzduchu.

Jadranské more je nízke more. Priemerná hĺbka je cca 250 m, ale severná povodie zriedka presahuje 100 m a Terstský záliv zostáva pod 25 m; len na juhu zostupuje viac ako 1200 m. Vrstva tak tenkej vody sa zohreje a ochladí za pár dní, Tyrrhenian v týždňoch. Existujú tri typické situácie:

1. Jar a skoré leto, priaznivé. More je stále studené, dokonca aj pre sladkú vodu riek Alpských a Po, prichádza horúci a suchý vzduch z planiny alebo Balkánu. Horúci vzduch nad studenou vodou vytvára zvrat na mori (privádzač): je klasickým stavom otvorov medzi Talianskom, Slovinskom a Chorvátskom.
2. Jeseň a zima s borou, nepriaznivé. Studený vzduch nad teplejším morom: nízka vrstva sa stáva nestabilnou, mieša sa a potrubie sa rozbije.
3. Po bore, šanca. Len preto, že more je nízke, Bora ochladzuje ho niekoľko stupňov za niekoľko dní. Ak vstúpite do anticyklónu s horúcim vzduchom, je vzdušný kontrast silný a spojka je robustnejšia ako zvyčajne.

Jadranské more je tiež uzavretý kanál medzi Apenines a Dinari, necelých 200 km široká: morská vrstva zostáva obmedzená, a trasy pozdĺž osi (Puglia Venezia, cca 700 km) zostávajú všetky na mori, najlepšie podmienky pre dlhé potrubie.

Čo program počíta. Na morských miestach, s počasím korekcie a poľa

| Podmienka | Oprava |
| --- | --- |
| potenciálna teplota pri 925 hPa (alebo 950) nad SST 4 K, | _ |
| mimo SST 7 K | +2 dB |
| s vlhkým vzduchom pri 925 hPa (nad 80%) | −0,5 dB na týchto bonusoch |
| chladené more 1,5 °C alebo viac za 72 hodín pred, s priaznivým kontrastom | _ |
| teplejšie more vzduchu pri 2 m 2 °C (zmiešavanie) | −1,5 dB |
| teplejšie more 4 °C | -3 dB |

Celková korekcia zostáva v rozmedzí od -3 do +3 dB a bez vedenia zostane zisk pod žltou strechou. Prahové hodnoty sú dočasné: v štandardnej atmosfére potenciálna teplota pri 925 hPa presahuje teplotu mora okolo 2 a3 K, potom 4 K už naznačuje opak.

Pamäť SST. Modely udržujú teplotu mora takmer stabilnú počas predpovede. Nedávny pokles je odvodený z analýzy troch dní pred (Open-Meteo, len povrchová teplota, len morské bunky): maximálne 72 hodín pred mínus aktuálna hodnota. Ak služba neodpovedá, pamäťový bonus sa neuplatňuje.

Limity. SST modelov pod pobrežím a v hornej časti Jadranu je hrubý; napríklad studené pero Po je zle riešené. Batimetria nezahŕňa výpočet: zdroje vždy poskytujú SST, čo už zhŕňa účinok morského dna. Kontrast vzduch-more sa čiastočne prekrýva na konci Misme stability (§ 9 bis); pre to bonusy sú malé. Prahové hodnoty budú tarané chodbami reklám a QSO cez Jadranské more.

## 10. Vykonávaná na celej trase

Ak vrstva zachytáva pásmo aspoň na 80% použitej frakcie a stanice nie sú na dohľad, vypočíta sa aj strata v potrubí: voľný priestor plus spojenie antén (3 dB, ak sú vnútri vedenia, viac, ak sú vyššie alebo nižšie), plus zvyškový útlm γd = 5,10-5, ·ae·f^ (1/3) dB/mrad ITU-R P.452 eq. 49 [6]. Ak je nižšia ako strata bez výplachu, očakávaná strata sa stane.

Pásmo zostáva v spojke len vtedy, ak jeho vlnová dĺžka nepresahuje λmax = 2√2·(2/3)·√(ΔM·10-6·d (Kerr, eq. 17 [11]). Pre to môže rovnaká vrstva uväzniť 23 cm a 70 cm, ale nie 2 m. Limit však nie je čistý: Kerr varuje, že aj vlny niekoľkokrát dlhšie trpia veľa vedenia, a Hepburn a VHF / UHF DX Book hovoria o "sférových" hrán a "odpustiť" kanály. Od 1.3.1 program používa mäkkú spojku: ak je λmax medzi λmax a 3·λmax, zisk postupne prechádza z 15 dB (λ = 3·λmax) na plný prírastok spojnice (λ = λmax), so s = (λmax/λ − 1/3)/(2/3). V takom prípade sú povolené farby kanálika. Log a čiara Meteo pozdĺž trasy označujú body mäkkým kanálom.

## 10 bis. Via Monte

Keď sa korešpondent uzavrie za horizontom, program hľadá najlepší smer, ktorým sa ukazujú antény: osvetlený sklon rozprestiera signál ako radarový cieľ. Prebieha každý piaty horizont (okrem ±15° okolo korešpondenta) a v každom smere zaberá osvetlený horizont, cez 100 m a medzi 1 a 60 km. Rýchly odhad (voľný priestor, bitaktická konverzia a 10 dB pre každú úroveň horizontu z difuzéra na korešpondenta) vyberá 4 najsľubnejšie, najmenej 15° od seba; pre každý z nich:

- liečiť 1, stanica → úľava, vo voľnom priestore;
- zaobchádza 2, reliéf → korešpondent, vypočítaný ako v prípade úľavy vysielanej z 10 m nad vrcholom, s rovnakou predpoveďou (bez parabolickej rovnice);
- prepočet s bistatickou radarovou rovnicou, s σ = σ0·A, σ0 = −15 dB (horské vo VHF/UHF) a A plochou osvetlenou R1 lúčom (maximálne 20 km2):

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

Riadok "Odporúčaný bod" označuje azimut najlepšej trasy, ak stojí menej ako priame. Pri 144 MHz, s úľavou na 20 km a 15° Yagi, horská cesta stojí asi 45 dB viac ako len 2 trasy: to sa stane len vtedy, keď je priame je uzavretý susedné prekážky. Odhad je hrubý (±10 dB), najmä pre σ0.


Bez straty na horách (možnosť od 1.3.2). S Obzorové kolo si potom vyberie bod s najvoľnejším horizontom smerom k korešpondentovi (pri parite a najvyššej) a výsledok ukazuje trasu z bodu do korešpondenta, so signálom a S/N, vedľa súčtu s difúziou. Je to optimistický limit: hovorí, či sa otvára smer, keď hora dvíha signál dobre, alebo kam ísť prenosné. Každý bod je daný desiatimi znakmi.

## 10b. Vstup do vzdialeného vedenia

Ak spojka, ktorá zachytáva kapelu, začne ďaleko, za horizont stanice, signál musí doraziť. Program prináša signál do východiskového bodu, uprostred vedenia, pre difrakciu: pozdĺž profilu programu Copernicus GLO-30 hľadá okraj s najvyšším Fresnelom parametrom (ITU-R P.526), s opuchom ekvivalentnej pôdy,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

skôr útlm v dôsledku zakrivenia vrcholu (zaokrúhlená prekážka, P.526-16 § 4.2), s polomerom R odhadovaným z pôdy programu Copernicus GLO-30 na ±1 km od okraja,

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

(pre m·n > 4 je formulár 34b odporúčania). Barsis (NBS, 1963) meraná na troch dlhých trasách ponad horu (Colorado, Aljaška, Európa) priemerné straty 10 a 20 dB vyššie ako ideálny okraj Fresnel-Kirchhoff: zaokrúhľovanie vysvetľuje veľa (Barsis a Kirby, 1961: Pikes Peak s polomerom približne 11,5 km). Výsledok sa porovnáva s difrakciou na hladkej sférickej pôde; je najhorší. Signál potom prechádza v potrubí: voľný priestor na celej trase, zvyškový útlm γd P.452 na úseku v spojke a spojení korešpondenta (3 dB vnútri, 1 dB každých 10 m mimo). Ak je táto strata nižšia ako očakávaná strata, stáva sa mechanizmom ("koniec (vložený difrakciou) "). Je to autorova predpoveď o talianskej geografii: s blízkou prekážkou hlboko pod čiarou, v je negatívny a vstup stojí málo; s dorz predné náklady 15 chees 30 dB.

## 10 quaterov. ITM (Longley-Rice) a výstup z kanála

Od 1.3.2 program obsahuje nepravidelný teroristický model 1.2.2 NTIA/ITS (Longley a ryža, ESSA ERL 79-ITS 67, 1968; Hufford, Longley a Kissick, NTIA 82-100, 1982), preložený do C# oficiálnym kódom americkej vlády, ktorý je bez práv. Preklad dáva rovnaké čísla ako originál: automatické zostavovanie porovnáva zakaždým s kódom NTIA C++ na piatich skúšobných prípadoch (dole pod 0,01 dB).

ITM vypočíta stratu v normálnej atmosfére na skutočnom profile Copernicus GLO-30: videný s odrazom zo zeme, difrakciou (spigol a sférická pôda odvážená na drsnosť pôdy Δh, to je rozdiel medzi 10. a 90. percentilom kvót okolo rovno minimálnych štvorcov) a troposcatter. Reaktivita pôdy sa zníži na priemernú časť trasy $N_s = N_0\,e^{-h/9460}$ a spôsobí zakrivenie ekvivalentnej pôdy,

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

Program využíva rádiové podnebie zvolenej trasy ako v § 10 pohlavia (do 1.3.4 vždy mierne more, s N0 320 na pevnine a 350 na mori), s N0 z priemernej reaktivity miesta (pole εr 15, σ 0,005 S/m; more εr 81, σ 5 S/m), horizontálne polarizáciu a variabilitu v priebehu času bez tohto miesta (profil je známy). Ukazuje stratu prekročenú vo 50% času a ten, ktorý máte v 10% a 1% najlepšieho času. ITM ne modeluje kanály: jeho variabilita v priebehu času je štatistická podľa klímy. Slúži ako druhý výpočet východiskovej hodnoty vedľa P.452; rozdiel medzi nimi dáva predstavu o neistote.

Druhé použitie je posledný úsek po potrubí, ktoré končí pred príslušným. Vo výstupnom bode je virtuálna anténa umiestnená v polovici vedenia a ITM vypočíta traktát k príslušnému bodu ako nadbytok voľného priestoru na 50% času. Celková strata

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,	heta_{condotto} + A_{ITM}(d_{uscita} 	o RX)
```

kde A_TX je spojenie antény (3 dB vo vnútri, 1 dB každých 10 m mimo), ak vedenie začína blízko, inak vstupná difrakcia § 10 ter. Je to limit, ktorý si autor sám dal: kontinuita vrstvy plus nanajvýš zvyškový útlm. Ak je nižšia ako očakávaná strata, stáva sa mechanizmom ("vodivosť (výstup a konečný úsek ITM) ").

## 10 chinquies. Prehlbovanie: ostrovy, úžiny a zemsko-morská diskontinuita

Príklad z Neapolského zálivu (Anacapri, JN70CN) smerom k západnému Stredomoriu ukazuje, kde trasy cez ostrovy:

| Krajina určenia | Azimut | Kde prechádza cez ostrovy | Prekážka |
| --- | --- | --- | --- |
| Barcelona |  279°  | severne od Sardínie (Gallura) do 41,05° N, medzi 420 a 490 km | asi 30 km južne od Bocche di Bonifacio; Limbara (1359 m) asi 20 km od trate |
| Bocche di Bonifacio |  282°  | úzky na 41,3° s. š. | asi 11 km |
| Marseille |  296°  | Stredná Korzika pri 42,2° s. š. | hory až do 2700 m: vedenie pod 2 km zastávky |
| Valencia, Baleárska oblasť |  260–265°  | Stredná Sardínia | vnútorné úľavy |

Pre Barcelona stačí bod 282° namiesto 279°, pretože os prechádza z úst: 3., vnútri lúča normálneho Yagi.

Sú ústa tiesnivé? Lebo nízke kanály, pod niekoľko sto metrov, áno; ostrovy sú murované a nad zemou, cez deň, teplo pretrhne inverziu. Samotná priepasť je dostatočne široká: asi 11 km, oproti prvej ploche Fresnelu, široká menej ako jeden kilometer, polovica 1000 km trasy v 144 MHz. Problémom je počasie. Ústa sú zábava pre vietor: Spodná vrstva sa zrýchli a spodná vrstva sa odstráni. Často je priechod otvorený ako geometria, ale narušený ako atmosféra. S stabilným anticyklónom a malým vetrom môže byť namiesto toho dobrý priechod.

Pozemské rozdiely. Počítajú sa. Spojka vytvorená na tyrrhenskom pobreží je prerušená: signál vychádza z kanála (časť po výstupe, § 10 quater) a musí sa vrátiť do iného kanála na mori Baleárskych, platí opäť spojenie. Spojky vo výške nad 1500 m prejdú cez Galluru bez toho, aby si to uvedomili; pre strednú Korziku neplatí.

V praxi: ak cesta prechádza cez hornatý ostrov, skúste určitý stupeň korekcie smerom k úseku voľného mora, ale nepočítajte s priechodom so silným vetrom v úzkych.

## 10 sexies. TN101 rádiová klíma a časová variabilita (od 1.3.5)

TN101 § 10 [25] opisuje, ako sa strata trasy za horizontom mení počas dlhého obdobia okolo mediánu, s rôznymi krivkami podľa klímy: funkcia Y(q) hovorí, koľko dB strata klesá v q% najlepšieho času. ITM obsahuje tieto krivky pre sedem klimatických podmienok. Klíma sa počíta veľa: na mori v miernom morskom podnebí, pri 144 MHz, v 10% prípadov je oveľa viac dB ako na zemi v kontinentálnom podnebí, pretože kanály sú častejšie.

Program už neopravuje klímu: Vyberá si ju pre každú cestu z priemerného bodu, takže funguje po celom svete a nielen v Európe. Stiahnite si z Open-Meteo [26] 3-ročné denné hodnoty ERA5 reanalýzy (teplota, vlhkosť a pôdny tlak, dážď) pre bunku s hodnotou 0,5° priemerného bodu len raz; hodnoty zostávajú v Data/CLIMA. Obnova pôdy

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

deň za dňom, potom ročný priemer a exkurzie medzi mesačnými priemermi, priemernou vlhkosťou a ročným dažďom. Pravidlá sa riadia opismi podnebia TN101 § 10.2; hranice medzi jedným podnebím a druhým sú kritériom autora:

- viac ako 60° zemepisnej šírky: mierny kontinentálny ( TN101 nemá krivky polárneho podnebia);
- menej ako 250 mm zrážok za rok a priemerná vlhkosť pod 50%: púšť (Ns typických 280);
- do 20° od rovníka, Ns najmenej 350 a exkurzia do 30: rovník (Ns typický 360);
- v rámci 32° a vychádzky najmenej 60: subtropické kontinentálne, t. j. monzúny (typické vychádzky 60
- od 10° do 32° s Ns najmenej 340: subtropické morské (Ns typické 370);
- oba horizonty na mori, viac ako 20°: mierne more na mori (je to definícia TN101);
- exkurzia do 30 v blízkosti mora (more na trase alebo priemerná vlhkosť aspoň 75%): morská mierna na zemi;
- inak mierna kontinentálne (typické exkurzie 20

N0 pre ITM je priemer znížený na úroveň mora, N0 = Ns·e^ (h/9460). Bez siete sa používa pravidlo o rezerve so samotnou zemepisnou šírkou a morskou líniou a hovorí sa v ňom čiara.

Testy s údajmi ERA5 20232025: Capri Ns 339 s exkurziou 45 (kontinentálne mierne na zemi, morská mierna, keď sú oba obzory na mori); Londýn 327 a 16 (temperovaná hviezda); Viedeň 319 a 24 Abidjan 381 a 11 (rovnica); Miami 365 a 36 (subtropický manžel); Delhi 337 a 82 (subtropická konkontinentálna); Riad 278 a 37, 90 mm dážď (desert). V Čile: Antofagasta 341 a 19 (subtropický manžel: pobrežie Atacamy, s takmer pevným nízkym opačným smerom Pacifiku), Calama 232 na zemi, 2257 m, 14 mm dažďa (desert), Valparaíso 331 a 11 (teplotný šev), Punta Arenas 313 a 5 (teplotný šev).

Riadok "Rádiové podnebie ( TN101) " ukazuje klímu, dôvod, Ns s polovicou exkurzie a Y: koľko dB zarobiť v 10% a 1% času a koľko stratiť v 90%. Je to dlhodobá štatistika a zostáva oddelená od prognózy: očakávaná strata nasleduje v dnešnej dobe s GFS profily, a súčet dve veci by počítať dvakrát rovnaké abnormálne šírenie.

## 11. Porovnanie s ITU-R P.452

Porovnávacia čiara P.452 používa metódu ITU pre kanály a odraz na vrstvách (P.452-16 § 4.5 [6]) s mediánom ΔN GFS namiesto klimatologickej a p = 20% ako kalibračnú konštantu, ako v štúdii CSIRO [10]. Zahŕňa stratu empirického spojenia Alfa pod 0,5 GHz (približne 28 dB až 144 MHz). Je to len porovnanie: očakávaná strata zostáva v odseku 9.

## 11 bis. Parabolická rovnica (od 1.3.1)

Parabolická rovnica Line je štvrtý výpočet, iba porovnanie, vykonané ako profesionálne programy pre kanály (PETOOL; wave-propagation framework of M. Lytaev, MIT licencie): namiesto odhadu zisku z vrstiev, postupuje vlna pozdĺž trasy s Fourier split-step metóda vnútri M upravené GFS refrakčný profil, prevzaté z predpovedaného bodu bližšie ku každej vzdialenosti. Úroveň GFS sa pripočíta k odhadovaným skokom potenciálnych potrubí (výpočet A, 50 m v polovici vrstvy). Pôda Copernicus GLO-30 vstupuje do krokov (pod zemou pole je null), pôda je vodič a lúč antény je Gaussian s odhadovaným otvorom z zisku. Výpočet vidí vedenia, tí, ktorí prehrávajú, difrakcia a spojenie antény; On nevidí troposcatter, ktorý je pridaný oddelene. Ďalej je

Od 1.3.2:

- propagátor je v širokom uhle s presnou fázou voľného priestoru:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- dno na mori a na pobrežnej rovine v nulovej nadmorskej výške sa odráža s koeficientom Fresnelu v horizontálnej polarizácii, εc = εr − j·60·λ·σ (morská εr = 81 a σ = 5 S/m; krajina 13 a 0,006 S/m), v jasnom uhle trasy:

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- drsnosť mora znižuje konzistentný odraz podľa Millera a Browna s účinnou výškou vĺn z vetra na 10 m (σh = 0,0051·U2):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- na mori profil M zahŕňa odparovací kanál (odsek 9 ter) s neutrálnym logaritmickým profilom:

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

Testuje s prototypom (10 m antény na mori, získať na štandardnú atmosféru pri 100 a 300 km): s δ = 1030 m odparovací kanál dáva 1 a3 dB až 144 MHz, 8 a37 dB až 432 MHz a 21 a66 dB až 1296 MHz. Pri 144 MHz v horizontálnej polarizácii sa more odráža takmer ako kov (o viac ako 0,99 až niekoľko desiatok stupňov) a drsnosť sa počíta málo: nové dno sa mení niekoľko desiatok dB; počíta viac od 432 MHz nahor.

Limity: nad zemou zostáva vertikálny profil úrovne GFS, takže tenké kanály zostávajú neviditeľné; zem k schodom je približná; lúč zostáva v rámci 6.

## 12. antény, signál a hluk

- Yagi DL6WU: zisk 9,5 + 7,8·log10 (boom/λ) dBd (2,35 dB pre dvojitý boom, DL6WU pre VHF komunikácie 3/1982) plus 2,15 pre [12] dBi.
- EIRP = výkon − kábel + TX zisk.
- Prijímaný signál = EIRP − očakávaná strata + zisk RX − kábel RX. S-meter IARU: S9 = −93 dBm, 6 dB na jednotku.
- Vonkajší hluk podľa ITU-R P.372 [13]: galaktický (Fa = 52 - 23 log f) umelejší podľa prostredia (mesto, obytné, vidiecke, tiché vidiecke) alebo ručne písaných teploty antény.
- Tsys = Ta + Tpre + [290 (Lc - 1) + 290 (Fr - 1) ·Lc] / Gpre do terminálov antény (Friis vzorec). Od 1.3.5 má prijímacia stanica vždy predpripravenú anténu, pevný parameter: číslo hluku 0,8 dB (Tpre = 59 K) a získa 20 dB. Za nimi je kábel (strata Lc) a rádio, so základným Fr hlukom číslo 6 dB. S 1 dB kábla kábel a rádiopríspevok klesá z približne 1160 K na 12 K: hluk rozhoduje o oblohe, životnom prostredí a preamp. Zisk 20 dB je typická hodnota zvolená programom.
- S/N v SSB, CW a FT8 v pásme každého smeru v porovnaní s nastavenou prahovou hodnotou.
- Pravdepodobnosť za obdobie (od 1.3.2): evanjelium v priebehu hodiny je normálne rozdelenie S/N okolo očakávanej priemernej hodinovej hodnoty. Bray (QST, november 1961, s. 39, na krivkách Nortonu 1959) poskytuje amplitúdu 13,5 dB cez asi 100 míľ, t. j. približne 7 dB pod a 7 nad priemerom, ktorý takmer padá priamo pod 100 míľ (3,5 dB až 50 míľ); pre "pevné" pripojenie berie 7 dB z rozpočtu. Zber ±7 dB ako ±2σ: σ = 3,4 dB nad 160 km, úmerná ďalej uvedenej vzdialenosti. Pravdepodobnosť, že obdobie prekročí prah cesty je

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  s normálnou distribučnou funkciou Φ: rozpätie 0 dB = 50%, +3,4 dB = 84%, +7 dB (ďalej len "pevný" rozsah Bray) = 98%. Nezahŕňa neistotu modelu, ktorá je väčšia.
- Digitálne spôsoby: S/N v 2500 Hz v porovnaní s prahovými hodnotami meranými pri 50% dekódovaní bez predchádzajúcich informácií (AP). FT8 −21 dB a FT4 −17,5 dB pri bielom šumení; ak je mechanizmom troposcatter, používajte prahové hodnoty s evanjelikou "rozptýlené médium zemepisnej šírky," FT8 −18,6 dB a FT4 −15,2 dB [21]. Q65 -15C −22,2 dB, Q65 -30B −24,8 dB, Q65 -60A −27,6 dB V lietadle s rozptylom môže Doppler prekročiť tóny Q65 -60A (1,7 Hz): sú lepšie ako 30B alebo 15C. JT65A −25 dB [22]. Dekódovanie AP (názov už známeho alebo konca QSO) dáva asi 2 dB navyše pre FT8 a 1,5 až 2,6 dB pre Q65; to nevstupuje do prahovej hodnoty, pretože to nie je platné pre všetkých [24] FT8

## 13. Časy odpovede a údajov

Každý výpočet meria svoj vlastný čas: GFS profily (sieť, analýza, povinné prestávky 20 s medzi šaržami pre Open-Meteo limit), Copernicus GLO-30 terén (stiahnutím tabúľ, 15 a 30 MB každý, len prvýkrát), výpočet. Časová línia odpovede ich zhŕňa; v registri riadky začínajú s TEMPI. Pri vylepšovaní siete register uvádza aj použité preteky GFS, keď boli k dispozícii a aké staré sú údaje.

Od 1.3.2 si môžete tiež vybrať ICON-2I, model agentúry ItaliaMeteo a Arpae na Taliansku na približne 2,2 km, cez Open-Meteo (model italia_meteo_arpae_icon_2i, dve preteky za deň, 3 dni). Pokrýva len Taliansko a blízke moria a má menej nízke úrovne GFS (1000, 925, 850, 700, 500 hPa): pre kanály v nadmorskej výške nie je lepší ako GFS, ale jemná mriežka opisuje dobre pobrežia, ostrovy a more v 2 m, to je odparovací kanál. Premenné, ktoré model nemá (limit vrstvy, teplota povrchu...) sa z požiadavky odstránia. Od 1.3.2 automatický zdroj používa len NOMADS; Open-Meteo slúži, ak NOMADS nereaguje a pre historické prípady. S "Update by o sebe" box (osvetlený chyba) predpoveď je unclicked: pri otvorení, na každom novom GFS závode na NOMATS (cca 5 hodín po pretekoch) a keď viditeľná zóna vyjde na viac ako štvrtinu zo siete; nanajvýš pokus každých 15 minút, nikdy s historickým prípadom.

Žiadosti v Open-Meteo majú limity za minútu, za hodinu a za deň pre každú internetovú adresu. Stiahnuté profily sú uložené na disku (Dati/cache-gfs) pre každý ťah a model: s rovnakými preteky, ktoré sa menia každých 6 hodín, body už stiahnuté nepýtať znova. Vďaka automatickému zdroju začínate z NOMADS a Open-Meteo slúži len na chýbajúce body; s jediným Open-Meteo, v časovom limite alebo denné zastavenie okamžite a hovorí, kedy to skúsiť znova. Historické prípady nie sú uložené.

## 14. Overiť model

Prognóza by sa mala porovnať so skutočnými prepojeniami. Záznam výpočtu drží každé číslo; archív programu udržiava spot a dekódovanie. V prípade 432 a 1296 MHz, kde je málo miest, potrebujú správy od operátorov: ktorí vytvorili spojenie, kedy, s akým signálom. Porovnajte počas mnohých hodín očakávaný zisk s nameraným signálom, ak sa prahy majú presunúť.

## Zdroje

1. NOAA/NCEP, Global Forecast System (GFS), globálny model pri 0,25°, štyri preteky za deň.
2. Open-Meteo, predpoveď API (open-meteo.com), gfs_seamless model, premenné na úrovni tlaku a pretekárske metaúdaje.
3. Kopernik DEM GLO-30 (ESA; WorldDEM © DLR e.V. 2010 Rezerva: NASA/USGS Copernicus GLO-30 v Terrain Tiles "skadi" AWS.
4. ITU-R P.453, Rádiový index lomu: jeho vzorec a dátum lomu, s digitálnymi mapami kanálov (Study Group 3, © ITU, osobné a nekomerčné použitie).
5. B. R. Bean, E. J. Dutton, Radio Meteorology, NBS Monograph 92, 1966: Tabuľka 6.5 a § 4.4.
6. ITU-R P.452-16, Prediction procedure for the evaluation of interference between stanications on the surface of the Earth at the frency about 0,1 GHz: § 4.2 (diffrakcia), eq. 45 (troposcatter), § 4.5 a eq. 49 (vodené).
7. ITU-R P.834, Účinky troposférického lomu na šírenie rádiových vĺn.
8. ITU-R P.525, Výpočet útlmu voľného priestoru.
9. ITU-R P.526, propagovanie difrakciou: eq. 31 (cutcheon) a § 3 (sférická pôda).
10. B. Indermuehle, H. Suzuki, Troposferic Ducting Prediktion based on GFS Model Data at Inyarrimanha Ilgari Bundara, the CSIRO Murchison Radio-astronomy Observatory, Publications of the Astronomical Society of Australia, 2026 (arXiv 2608.1261).
11. D. E. Kerr, Propagation of Short Radio Waves, MIT Radiation Laboratory Series vol. 13, 1951: eq. 17 e 18.
12. G. Hoch DL6WU, 1 (merané 432 a 1296 MHz podľa dĺžky boomu).
13. ITU-R P.372, Rádiový hluk: galaktický a umelý hluk pre životné prostredie.
14. NOAA/NCEP, NOMADS, GRIB filter času GFS 0,25° (nomads.ncep.noa.gov); formát WMO G RIB2, komplexné balenie s priestorovými rozdielmi.
15. Open-Meteo, Historická predpoveď API (vzorový archív predpovedí z roku 2021).
16. LukiBox, TropoLink, dizajn trás troposcatter s P.617, NBS TN101 a ITM na boku (licencia GitHub, Apache 2.0).
17. IMO MSC.1/obež.
18. RSGB, príručka VHF/UHF; implementácia DL2FZN (andi-f/troposcatter na GitHub, 2013).
19. W. F. Moler, D. B. Holden,
20. Príručka ARRL 2022 (99a ed.), § 19.4.319.4.4: Troposferické počasie a šírenie.
21. S. Franke, B. Somerville, J. Taylor, The FT4 and FT8 Communication Protocols, QEX júl/august 2020.
22. W3SZ, prahy digitálneho pomalého režimu ( JT65, JT9, QRA64) a voľba režimu podľa šírenia, 2019.
23. QST, "The World Nad 50 MHz," November 2022: Otvorenie 14. a 15. augusta 2022 medzi Manitobou a Kansas/Missouri.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Quick-Start Guide to Q65, 3. apríla 2021 (wsjt.sourceforge.io).
25. P. L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis, Transmission Loss Predictions for Troposféric Communication Circuits, NBS Technical Note 101, magazín 1967: § 9 (troposcatter, funkcia F(θd), H0), § 10 a príloha III (rádiové podnebie a variabilita v čase).
26. Open-Meteo, historické počasie API (ECMWF ERA5 recenzia od roku 1940; Copernicus Climate Change Service, CC BY 4.0 licencia).
