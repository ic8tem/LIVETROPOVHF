# Zdroje a vzorce

> Strojový preklad z taliančiny: v prípade pochybností platí talianska príručka.

Pracovná zbierka zdrojov konzultovaných pre LIVETROPOVHF tropo predpoveď (20 Základná fyzika, dostupné údaje, zverejnené indexy, vzorce, amatérske rádiové skúsenosti, validácia a rozhodnutia. Vzorce sú číslované ako v LaTeX, (1), (2),... a v texte, ktorý si pripomínajú s číslom v zátvorkách. Príručka predpovedí opisuje, ako ich program používa.


## 1. Priestory autora

IC8TEM pozorovania, ktoré rám práce, so spätnou väzbou nájdené v zdrojoch.

- Talianska geografia si vyžaduje difrakciu. S dorzálne, promontory a ostrovy niekoľko desiatok kilometrov od staníc, difrakčné príspevky sú vypočítané s Fresnelom parametra na hlavnom okraji, nielen s hladkým pozemkom. Stretnutie: v porovnaní s nepravidelným terénom pri 563 MHz (Rio de Janeiro, 27 bodov medzi 5 a 30 km) boli najlepšie metódy P.526 s ostriami čepele, P.1812 a difraktívnym klinom, s priemernou chybou medzi −5 a +3 dB a rozptylom okolo 9 dB; P.1546 bol najpesimistickejší (z Costa, Rabanal Ramirez a Pre strmé svahy v blízkosti stanice PETOOL si pamätajú, že slúžia širokospektrálnym množiteľom a obojsmerným výpočtom (vlna odrazená reliéfmi).
- Aby ste sa dostali do vzdialeného vedenia, potrebujete Fresnel. Ak spojka začína za horizontom, signál prichádza k nám difrakciou cez prekážky až do jeho začiatku; od 1.3.2 program počíta (Prevision Manual, § 10 ter). Stretnutie: Rohde & Schwarz (2023) poznamenáva, že stanice pozdĺž vysokého kanálu často nevojde, zatiaľ čo tie, ktoré sú blízko koncov áno.
- Podmienky sa rýchlo menia. Teórie sú tam, ale podmienky sa menia v minútach, a žiadny predpovedací systém trvá údaje v reálnom čase: GFS vychádza každých 6 hodín, s časovým rozpätím a 0,25°, a predpoveď môže stratiť hodnotu v krátkom čase. Stretnutie: Bray (QST, november 1961), meranie hodinových mediánových výkyvov približne ±7 dB cez 100 míľ; kanály sa pohybujú s atmosférickým časom (Rohde & Schwarz); náš QSO register 29/12/20212/1/2022 ukazuje, že otvory rastú a padajú v priebehu hodín.
- Najlepšie teraz-casting zostáva DX cluster Ak chcete vedieť, čo sa stane teraz, cluster spoty, PSKReporter (a WSPR) poraziť akýkoľvek model: sú to opatrenia. Prognóza hovorí, kde hľadať a ako dlho to môže trvať; reklamy hovoria, či je tam. Program pre to kladie bod a predpoveď na rovnakú mapu.

## 2. Účel

Myšlienka je tretí index, vypočítaný samotným LIVETROPOVHF od vertikálnych profilov modelu počasia, ktorý má byť po boku Hepburn a F5LEN namiesto ich výmeny. Dnes program číta ich mapy už pripravený: index by tiež povedať *prečo* tam je otvorenie (kvóta a hrúbka vedenia, minimálne pásmo zachytené) a mohlo by dôjsť k spotom, ktoré program zbiera už.

- Čo môžete dať: pravdepodobnosť vedenia za bod a za hodinu, podiel základne a vrcholu, hrúbka, minimálna frekvencia zachytené, a pozdĺž cesty zlomok trasy vnútri vedenia.
- Čo nemôže dať: tenké kanály (pod pár desať metrov), ktoré globálne modely na 0,25° neriešia, a odparovacie kanály na mori, ktoré slúžia predovšetkým od GHz nahor.
- Prvý praktický cieľ: 144 a 432 MHz v Európe, s overovaním na klastrových miestach DX, PSKReporter a miestne dekódovanie.

## 3. Základná fyzika

Všetko začína od lomu N vypočítaná na každej úrovni profilu: spojka je tam, kde modifikovaná reaktivita M znižuje stúpanie, to je miesto, kde dN/dh klesne pod −157 N/km ([ITU-R P.453-13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)).

Refraktivita (P a v hPa, T v kelvine):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Tlak pary z relatívnej vlhkosti H (%) a teplota t (°C) nad vodou medzi -40 a +50 °C:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

Upravená reaktivita (h v km alebo 0,157 · z s z v metroch) a kritérium kanálika:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN/dh (N/km) | Nariadenie | Účinok 144 MHz |
| 0 | subfrakcia | znížený prietok, |
| od 0 do − 79 | normálne (štandard približne - 40) | rádiový horizont s ekvivalentným polomerom 4/3 |
| od − 79 do − 157 | superrefrakcia | predĺžený prietok, "príliš dobrý" |
| pod - 15 | spojka | zachytené signály, DX nad 1000 km, |

Ako extrahovať N z modelu pri úrovni tlaku: pre každú úroveň budete mať T, relatívnu vlhkosť a geopotenciálnu výšku, vypočítate N a M, potom gradient medzi susedné úrovne. Hladiny pod zemou by sa mali vyhodiť (pri 1000 hPa v Alpách je pod zemou).

Minimálna frekvencia zachytenia: vedenie vedie len pomerne krátke vlny v porovnaní s jeho hrúbkou. Hepburn publikuje hrúbku tabuľky → minimálna užitočná frekvencia ([DX Info Centre](https://www.dxinfocentre.com/propagation/luf.htm)); je to mäkký limit, pod ktorým signál postupne klesá.

| Vedenie Hrúbka | Minimálna platená frekvencia |
|  61 m  | 962 MHz |
|  91 m  | 464 MHz |
|  122 m  | 276 MHz |
|  152 m  | 185 MHz |
|  183 m  | 133 MHz |
|  213 m  | 101 MHz |
|  305 m  | 53 MHz |

V rozsahu VHF (často 120 a 300 m) tabuľky hodnoty sa celkom dobre zákona o konštantnej; pod 100 m vzorec podceňuje frekvenciu až 30 a 40%. Prispôsobenie ju sám (nie vzorec publikovaný Hepburn), s f v MHz a d v metroch:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

V podstate, pre 2 m, potrubie je často aspoň 170 Vzorec cirkulujúci na sieti, λmax = 0.014 · d^1.5, pochádza z Kerr (1951, kapitola 1, eq. 18) a je platný s λ v centimetrov a stojí, pre potrubie, v ktorom M padá lineárne: s týmito jednotkami dáva asi 2 m 180 m a asi 70 cm 89 m, to je rovnaké hodnoty Hepburn tabuľky, ktorá pochádza z neho. Kerr varuje, že to nie je čistý rez: aj vlny niekoľkokrát dlhšie trpia veľa vedenia, a spôsob, ktorý "strata" trochu môže zlepšiť príjem aj so stanicami von z vedenia. Štúdia o prístupe IEEE 2019 potvrdzuje, že klasické metódy (Kerr, Kukushkin) neopíšu skutočný strih ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)): prah by sa mal použiť ako ukazovateľ, nie ako stena.

## 4. Údaje dostupné zdarma

Slabý bod je vertikálne rozlíšenie: medzi 1000 a 850 hPa modely dávajú úroveň každých 25 LIVETROPOVHF už používa Open-Meteo na izobare, teplotu na 850 hPa a inverziu: index môže začať z rovnakých hovorov.

| Zdroj | Čo dáva | Rozlíšenie | Použitie pre index |
| [Open-Meteo, GFS](https://open-meteo.com/en/docs/gfs-api) | T, relatívna vlhkosť, Rosný bod, geopotenciálna výška podľa úrovne (1000, 975, 950, 925, 900, 850 hPa...); výška hraničnej vrstvy | 0,25° hladiny tlaku; 16 dní, hodiny až 120 hodín, aktualizované každých 6 hodín | profil N a M, gradient, základňa a vrchol kanála; už v programe |
| [Open-Meteo, ECMWF](https://open-meteo.com/en/docs/ecmwf-api) | rovnaké úrovne z otvorených dátových modelov 0,25°; zem dáva Rosný bod |  0,25°  | druhé stanovisko, užitočné pre súlad medzi modelmi, |
| [Open-Meteo, DWD ICON](https://open-meteo.com/en/docs/dwd-api) | T a relatívna vlhkosť na úrovniach tlaku; v ICON- D2 sú 800, 900 a 925 hPa | ICON-EÚ približne 7 km | Tretie stanovisko na tému Európa |
| [Radiosunds, Univ. Wyoming](https://github.com/NCAR/RAOBget) | namerané profily, dve štarty za deň (00 a 12 UTC), textový formát na stanicu | mnoho významných úrovní, aj každých niekoľko desiatok metrov | overenie indexu; pozri jemné kanály, ktoré model nevidí |
| [Steny, NOAA TEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive) | historický archív vysielania, súbor na stanicu, viac odvodených parametrov | Ako je uvedené vyššie. | kalibrácia na roky údajov a porovnanie s škvrnami minulosti |
| ERA5 (Copernicus) | hodinová reanalýza od roku 1940, 37 úrovní tlaku | cca 28 km | štatistiky a kalibrácia, nepredpovedané |

- S Open-Meteo geopotenciálna výška je nad hladinou mora: úrovne, ktoré spadajú pod zem sa musia vyradiť ([Open-Meteo, horný vzduch](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)).
- Na zemi sú pridané T a vlhkosť v 2 m: dávajú prvý úsek gradientu, ten, kde nočné povrchové kanály sa rodia na rovine a na mori.
- Talianske vysielanie v archíve Wyoming University of Wyoming (weather.uwyo.edu/upperair/sounding.html): stanice a čísla WMO v tabuľke nižšie.

| Stanica | Z | Zemepisná šírka | Zemepisná dĺžka | Podiel |
| Udine/Campoformido |  16044  |  46,03  |  13,18  |  94 m  |
| Miláno/Linate |  16080  |  45,43  |  9,28  |  103 m  |
| Prax na mori |  16245  |  41,65  |  12,43  |  12 m  |
| Brindisi |  16320  |  40,65  |  17,95  |  10 m  |
| Trapani/Birgi |  16429  |  37,92  |  12,50  | asi 14 m |

Textový formát Wyoming poskytuje pre každú úroveň tlaku, výšky, teploty, Rosného bodu a vlhkosti: dosť pre N a M. Wyoming nemá oficiálne rozhranie, skripty sa používajú ako [RAOBget NCAA](https://github.com/NCAR/RAOBget); College of DuPage uchováva kópie v texte na stanici ([napr. 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)].](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. Indexy a uverejnené metódy

Hepburn a F5LEN začať z rovnakých fyzikálnych zložiek, ale neuverejňujú kompletný algoritmus; vedecké štúdie z 2008

| Metóda | Počiatočné údaje | Čo sa vypočíta | Užitočné poznámky pre nás |
| [Hepburn Tropo Index](https://www.dxinfocentre.com/propagation/hti.htm) | surové modely počasia kanadskej meteorologickej služby (MSC, ako je napísané pod jej mapami; nie GFS/NAM), mapy navrhnuté s GRADS, proprietárny algoritmus ([storia](https://www.dxinfocentre.com/propagation/history.htm)) | 010 lineárna stupnica zakrivenia signálu: 0 normal, 1 | stupnica, ktorá sa má napodobniť, takže tretí index sa okamžite porovná s prvými dvoma |
| [F5LEN, svetová predpoveď](https://tropo.f5len.org/WW/info.php) | pre Európu model ARPEGE Météo-Francúzsko, 7,5 km, viac úrovní medzi 2 m a 1500 m ([poznámka autora](https://tropo.f5len.org/author/f5len/)]) | Priemerný index prenajímateľa, 40 krokov od 3 hodín, 4 výpočty za deň, 28-dňový archív ([nová verzia](https://tropo.f5len.org/?p=504)) | potvrdzuje, že slúži modelu s mnohými nízkymi úrovňami |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf) | 60 úrovní modelu ECMWF od zeme do 3000 m | minimálna dN/dz na kolónu: vykonávaná pod −157 N/km, super-refrakcia pod -78,7 N/km; základná vrstva pasce | klimatológia: Po Valley európsky priestor s niekoľkými potrubiami po celý rok; Stredomorie v lete takmer 100%, v zime pod 15%; Taliansko viac ako 30% v letných nocách |
| [Indermuehle a Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861) | GFS 0,25°, 21 úrovní tlaku + polia pri 2 m | M = N + 0,157 z; minimálna dM/dz nižšia ako 3 km, základná a horná časť kanála, hĺbka deficitu M; menej ako 40 m odhodená | overené 7 rokov na 25 kanáloch od 88 MHz do 2,68 GHz; dobrá kapacita do 3 dní vopred (AUC približne 0,74), potom klesá; GFS lepšie ako ERA5 |
| [Hämäläinen et al., MMF 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf) | Harmonie-AROME, 750 m alebo 2,5 km, 65 alebo 90 úrovní | dM/dh < 0 na modelovom profile, porovnanie s lúčom 4 až 59 m | počíta viac vertikálne ako horizontálne rozlíšenie; žiadny model nevidí kanály pod 15 m; vlhkosť dominuje teplotnému gradientu |

Tretia amatérska rozhlasová služba už aktívna: [TROPOCAST.EU](https://tropocast.eu/)of Artur SQ3A, pre 2 m a 70 cm v Európe. Zo stránky prognózy ([tropocast.eu/prognost](https://tropocast.eu/forecast)):

- Údaje: DWD ICON-EU (priechod 0,0625°, asi 7 km), predvolený zdroj; alternatívne kanadský HDPS (0,15°), menej podrobný.
- Metóda: N a M podľa ITU-R P.453 z profilu medzi vrstvou blízko zeme a 850 hPa; rozpoznávanie vrstiev, spojenie kanálov k zemi a vysoké a prahy farieb sú správne kalibrácia autora, nie je publikovaný.
- Produkty: "úplný" pohľad 039000 m, viac oddelených pohľadov "do zeme" a "vysoká" (iba teraz v mapách, ktoré majú byť začlenené); skóre 0 a 100 v deviatich triedach (pod 38 pozadie, 38 a 52 marginálne, 53 a 58 slabý, 59 a65 mierne, 66 a72 dobrý, 73 a80 veľmi dobrý, 81 a88 vysoký, 89 a95 silný, 96 a100 veľmi silný); predpoveď až +72 h v krokoch za hodinu, s animáciou a archívom.
- Oblasti: Európa, Poľsko, Spojené kráľovstvo, Nemecko, Francúzsko, Španielsko, Taliansko, Česká republika, Chorvátsko; rozhranie aj v taliančine.
- Opakované použitie: mapy sú začlenené s bezkľúčovým iframeom API; existuje aj dokumentovaná API (OpenAPI a JSON schéma). Licencia nie je uvedená: Pred použitím ako tretí zdroj vedľa Hepburn a F5LEN by ste sa mali opýtať autora.

Rozdiely od nášho projektu: TROPCAST sa zastaví na 850 hPa (asi 1,5 km), zatiaľ čo zvýšené kanály dorazia na 2000 m a ďalej (prípad LA0BY z 22/11/2018), a neposkytuje index pre pásmo alebo trasu. Referencie 0 a 100 možno porovnať s naším indexom 0 a 10 v rovnaký deň.

TROPOCAST obrazová štruktúra (z odkazu, ktorý poskytol autor programu): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, kde n je predpoveď. Stránka zakazuje automatické odstúpenie robotom, takže som nemohol prečítať obrázok; aj pre toto, pred zobrazením týchto máp v LIVETROPOVHF slúži autorovi povolenie.

Notebook: [DJ5AR, kategória](https://dj5ar.de/?cat=174)(Magonza, 21 článkov 2017 a 2023, 2 m a 13 cm). Opísané prípady: niekoľko týždňov od narodenia Írska (23. mája a 2. júna 2023), FT8 pre Bielorusko (september 2020), Taliansko a Nemecko nad Alpami a Jadranom (august 2017). Rozpoznajte prípad "príliš vysoká" kanál (reverzný na 1000 m), ktorý nemôžete použiť zo zeme: ďalšie potvrdenie, že je potrebné porovnať podiel vedenia s podielom staníc. Na meranie otvoru (do 1028 km) a vertikálnych profilov Paríža, Bordeaux a DWD používajte rádiofarmy.

Stručne povedané, spoločná receptúra: vertikálny profil → N a M na vrstvu → vrstva s dM/dh negatívnym → základ, vrchol, hrúbka a sila kanála → index. "Secret" časť Hepburn je empirická kalibrácia na skutočných udalostiach, ktoré môžeme prerobiť s škvrnami.

## 6. Užitočné vzorce pre program

Okrem N a M (časť základnej fyziky) sa tieto používajú na presun z vertikálneho profilu na bodové číslo a potom cestou. Vzorce ITU boli zrekonštruované PDF čiastočne zle; 22. novembra boli preskúmané na oficiálnych textoch ([ITU-R P.452-18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453-14, P.526-16, P.372-18) a opravené v kóde: konštanta troposcattera 190 (nie 190,1) plus absorpcia plynov s ρ = 3 g/m3; α nie menej ako −3,4 (eq. 55a); plyn z káblovodov s ρ = 7,5 + 2,5·ω (eq. 9a); chôdza na plnej ploche (eq. 23 Rumore P.372-18: tabuľka 1 a galaktický korešpondujú; Krivky umelého hluku sú až 250 MHz a galaktický až do cca 100 MHz, v rámci programu rozširuje rovnaké zákony.

1. Prechod medzi dvoma úrovňami modelu (h v km, M v jednotke M):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. Pevnosť a hrúbka konduitu: hrúbka d = horná − základňa (pre pôdny kanál, základ = pôda); pevnosť ako nedostatok M medzi základom a vrcholom.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3. Ekvivalentný faktor polomeru Zeme (P.452-18, eq. 5 a 6a) s ΔN = pokles N v prvom km, kladný, v N/km:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

S ΔN = 40 máte približne 4/3 k (štandardná atmosféra); s ΔN blížiace sa 157 k závesy nekonečno, to znamená, že Earth rádio sa stáva plochý: je to začiatok vedenia.

4. Rádiový horizont vysokej antény h metrov, s polomerom rovnajúcim sa\_e v km:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5. Pravdepodobnosť kanála pre zemepisnú šírku (P.452-18, eq. 2), percento času; φ = zemepisná šírka stredu trasy, μ1 a μ4 zohľadňujú, ako ďaleko je po mori:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

Pri 41° N, všetky po mori (μ1 = μ4 = 1), udáva približne 11% času: je klimatologická základňa užitočná ako referenčná hodnota, od ktorej sa očakávaný index líši.

6. Strata za troposcatter (P.452-18, eq. 45; f v MHz, d v km, θ uhol dráhy v mrad, N0 pozemná reaktivita v strede trasy):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

Termín −0,15 · N0 ukazuje, prečo mokrý vzduch pomáha aj bez potrubia: 50 ďalších N jednotiek je o 7,5 dB menej ako strata. LIVETROPOVHF už používa G3YGF verziu straty rozptylu.

7. Strata v kanáli (P.452-18, eq. 46

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. Z Bean a Dutton (1966) a Kerr (1951)

Dva klasické texty dávajú tri veci, ktoré sú potrebné okamžite k indexu: gradient prahy už tarát na rovnakej úrovni tlaku GFS, čo stojí za to v dB zmenu reaktivity, a aké poveternostné situácie vytvárajú alebo ničia kanály. *Radio Meteorology*, NBS Monograph 92.

1. Klasifikácia profilov pre tlakové vrstvy (Bean and Dutton, tab. 6.5, s. 248; −dN/dh in N/km). Navrhnuté pre rádiové vlnové dĺžky, platí na úrovni modelov:

| Cesta | Akcie o | Subfrakcia | Nelaminované | Super-refrakcia |
| 1000 | 0-1,46 km | −dN/dh < 0 | od 20 do 60 | nad 100 |
| 850 | 1,46 - 3,01 km | −dN/dh < 0 | od 20 do 50 | nad 80 |
| 700/600 hPa | 4,20 km | −dN/dh < 0 | od 15 do 40 | nad 70 |
| 600 a 500 hPa | 4,20 | −dN/dh < 0 | od 10 do 30 | nad 50 |

2. Aká hodnota je v dB (Bean and Dutton, cap. 6, s. 233

- Každá vrstva (na zemi alebo v nadmorskej výške) v porovnaní s "hladkým" profilom zvyšuje signál 1025 dB na všetky percentá; väčší zisk pochádza z vysokých naklonených vrstiev, pravdepodobne vďaka efektu zaostrovania (chod 200 km).
- Reaktivita pôdy Ns a pokles v prvom km ΔN sa vzťahujú na mesačný medián signálu s koeficientom medzi 0,4 a 0,95, medián 0,7 (20 trás zo 130 na 446 km, približne 100 MHz).
- Ročný cyklus: 0,18 dB na jednotku N; denný cyklus od 0,2 do 1,1 dB na jednotku N, maximálne na trasách 175 200 km a zima.
- Zložený parameter Misme odhaduje zmenu poľa ako 0,5 dB/ (N/km) · (ekvivalentný gradient − 40) plus termín tepelnej stability; parameter turbulencie D (plocha medzi skutočným profilom a adiabatickým profilom, 0 Záver autorov: ΔN zostáva najlepším parametrom jednoduchosti.

3. Referenčný profil a anomália (Bean and Dutton, § 3.8 a 3.9). Exponenciálna atmosféra referenčného CRPL poskytuje "normálny" profil od Ns; jednotka A meria, ako veľmi sa skutočný profil pri každom podiele líši. Je to prirodzený spôsob, ako premeniť GFS profil na anomáliu, aby na mape:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

S Ns = 313 máte ΔN asi −42 N/km, to je ekvivalentný polomer 4/3. Kritérium kanála −157 N/km je tiež odvodené od zákona Snell pre horizontálny polomer (eq. 4,23), takmer nezávislé od výšky antény.

4. Kde a kedy vznikajú vedenia (Bean a Dutton § 4.4; Kerr cap. 3):

| Proces | Situácia | Účinok |
| Varovanie | horúci a suchý vzduch z pevniny tečúcej na chladnejšie more (typické letné rukávy po dňoch dobrého počasia); hlavne sa počíta deficit vlhkosti | intenzívne potrubie na mori |
| Nočné ožarovanie | pokojná obloha, slabý vietor; nízke mraky alebo veľa vlhkosti zabrániť to | viedol k zemi, ktorá sa rodí pri západe slnka, rastie večer a rýchlo zmizne po východe slnka, najmä vo vnútri |
| Ubytovanie | vzduch prichádzajúci z anticyklónu, vyhrievaný a suchý, nad čerstvým a vlhkým vzduchom; časté záplavy v horách a juhovýchodných sektoroch anticyklónu | super-refrakčná vrstva alebo vysoký kanál; takmer vždy gradient vyšší ako štandard v opačnom smere |
| More a pôda | pobrežie, obeh niekoľkých sto metrov | miestne plynovody pozdĺž pobrežia, |
| Proti kanálu | miešanie: zem vyhrievaná slnkom medzi polnocou a poobede, vietor do zeme cez niekoľko m/s na drsnej zemi; nočná hmla | pravdepodobnosť vedenia takmer nič |

5. Dve nové prísady vyskúšať. Vertikálny pohyb: vzostupné prúdy v konvergenčných zónach odstraňujú a odstraňujú vrstvy, subsidence zosilňuje signál (Moler a Holden: v priemere o 2,5 dB viac ako s horským vzduchom, na mori v pásme L a X). Open-Meteo už poskytuje vertikálnu rýchlosť na úrovni tlaku. A tlakový trend: na ceste 140 km až 186 MHz signál zostúpil, keď tlak klesol na vysielač a do stredu cesty a vrátil sa vysoko a bez evanjelií, keď šiel hore, len s 2 (Bean a Dutton, fig. 6.9, s. 241).

6. Od Kerra. Okrem vzorca maximálnej vlnovej dĺžky (základná fyzika), Kerr poskytuje približný vzorec pre M z jedinej teploty, pary a nadmorskej výšky (§ 3.9, so starými konštantami 79 a 4800 potom nahradený 77,6 a 4810) a poznamenáva, že na reverzné rezanie vetra môže generovať vlny, ktoré sa hojdajú vo výške kanála, možná príčina hlbokého úniku (§ 3.26). Taliansky zdroj uvádza Bean a Dutton a vyhľadáva: L. Bonavoglia (december 1958), "Korelácia medzi meteorologickými javmi a šírením..."

## 8. Z amatérskych rozhlasových prezentácií a technických správ (druhá séria)

Sedem dokumentov od autora programu. Tri slúžia priamo k indexu: W3SZ pre troposcatter "podlaha," LA0BY pre prípady Hepburn nepredpokladal, Hepburn ( LU7MC) pre klasifikáciu počasia kanálov.

1. Signál je vždy tam. R. Rehr W3SZ, "Tropospheric Scatter (Troposcatter) Propagationon for VHF, UHF, Microwwave and Frences," Pack Rats Conference 2014; J. R. Duffey KK6MC, "The Troposcatter Productionon Mode and How to predikt Coverage on the VHF/UHF Bands," CSVHF 2017.

- Užitočné zo 100 na 700 km, od 144 MHz do 10 GHz; strata rastie o 30 log f s frekvenciou.
- Predovšetkým sa počíta počiatočný uhol smerom k horizontu: každý ďalší stupeň náklady okolo 9 M1CNK
- V lete je signál v priemere o 10 dB vyšší ako v zime; v polovici popoludnia o 5 dB nižší ako skoro ráno alebo večer. KK6MC označuje celkovú variabilitu 13
- Yeh metóda, najjednoduchší program (f v MHz, d v míľach, θ uhol rozptylu v stupňoch, α geometrický priemer otvorov oboch zväzkov):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Porovnanie Larsenu (1968) s 15 skutočnými pripojeniami: priemerná chyba štvorca 5,1 dB pre Collins, 7,4 pre Yeh, 8.1 pre NBS a CCIR, 16,8 pre Jazdca. Všetky metódy sa zlepšujú, ak Ns udržuje stabilný (Yeh klesne na 5,4 dB): pre rozptyl lomu dňa sa počíta málo, takže index tropo sa musí pozrieť na vrstvy, nielen Ns. Tabuľka 15 spojov s nameranými stratami (vrátane Feldbergu a Monte Balda, 322 km pri 957 MHz, 223,1 dB; Cyprus, 319 km pri 77,5 MHz, 163,3 dB), je dobrým testovacím lavicom pre náš výpočet.
- KK6MC predstavuje stratu, že typická stanica dokáže prekročiť 2 m a zodpovedajúcu vzdialenosť od 184 dB (FT-817 a 3 prvky v SSB, cca 130 km) až po 215 dB (dlhé bungy v SSB, cca 430 km) a 236 dB (kW a jagi spojené, cca 640 km). Je to spôsob, ako preložiť index dBs do zaslúžených kilometrov.
- Kód pripravenosti: W3SZ Yeh and Collins program (nitehawk.com/w3sz/YehCollins.zip) a Bob Atkins KA1GT kalkulačka (bobatkins.com/rádio/troposcatter.html).

Ďalší zdroj na scatter: I. Roberts ZS6BTE, "Troposférický scatter Propagation," *Radio ZS*, december 1984, s poznámkou z roku 2013 ([qsl.net/zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). Hlavne Užitočná geometria: uhol rozptylu θ = θ0 − θ1 − θ2, s θ0 = d/R, θ1 = (h1 − h\_ostakolo) /d1 + d1/2 R a to isté pre druhú stanicu, R = 8446 km (4/3 polomeru Zeme). Je to kus, ktorý sa používa na výpočet uhlov z profilu zeme v troposcatter "podlaha." Ďalšie údaje: každý uhol stojí viac ako 10 dB; prechod z Ns 280 na 300 znižuje stratu asi 4 dB; objem rozptylu sa pohybuje medzi 300 a 2000 m pre 150 km, medzi 600 a 3000 m pre 300 km; búrky v objeme rozptylu zvyšujú signál okolo 20 dB. Numerický príklad výrobku sa nevráti s danými vzorcami (termín voľného priestoru 900 MHz a 872 km je okolo 150 dB, nie 125), takže vzorce by sa mali skontrolovať na W3SZ pred ich použitím.

2. Typy vedenia (W. R. Hepburn, "Troposférické režimy šírenia," dxinfocentre.com/propagation/tr-modes.htm, španielsky preklad LU7MC). Vyplňte tabuľku Bean a Dutton užitočnými prípadmi v Taliansku:

| Typ | Kedy | Poznámka pre index |
| Ožiarenie | pokojné a pokojné noci na zemi | Nasledujte zem, zmizne krátko po svitaní |
| Vysoký tlak (pobyt) | anticyklon | môže trvať celý deň; v noci vedenie z ožarovania nižšie môže blokovať vzdialené signály, takže deň môže ísť lepšie |
| Predná strana | pred teplým frontom, za studeným frontom, severne od stojaceho frontu | krátke; zlé počasie ho znepokojuje |
| Zostup z hôr | föhn, bora: vzduch klesá, vykurovanie a sušenie | vyskúšať Bora na Jadrane a Föhn južne od Álp |
| Varovanie | horúci a suchý vzduch cez studenú a mokrú pôdu, napríklad po daždi | viedol k zemi |
| Údolie | horúci vzduch cez studený vzduch zastávky v údolí | môže zostať v deň |
| Marino | horúci a suchý vzduch na studenejšej vode; prichádza na pobreží s vánku | vrchol popoludní; jar a skoré leto najlepšie sezóny |

Výparné kanály na mori (0-30 m) slúžia len asi 3 GHz a nevstupujú do vnútrozemia: pre 144 a 432 MHz môžete ignorovať. Vysoké hory sú bariérou; púšte sú príliš suché.

3. Prípady Hepburn nepredpokladal (S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up," Nordic VUSHF 2019, od JO59IX do 500 m nadmorskej výšky):

- 22. novembra 2018: 70 cm až UA6 (cca 2600 km) počas 6 a 8 hodín. Hepburn uviedol len pobaltské štáty a Poľsko. Spojka bola videná na teplotných mapách 900 a 1500 m a skončila o niečo viac ako 2000 m, bola vyššia na nórskej strane, a len LA0BY na kopci vstúpil. Na zemi čas nevyzeral ako výstrek: zima a hmla na vrchole.
- 11. februára 2015: 2 m a 70 cm smerom k 9A a S5 (asi 1500 km) cez Alpy, aj to Hepburn nepredpokladal.
- 15. októbra 2011: 2 m s EA1 (2135 km) vo vysokom kanáli, na troch úsekoch mora a troch pozemných.
- Ďalšie podujatia: 6. novembra 2003 východne (2050 km), 10. decembra 2004 smerom k Francúzsku.
- Adresy polí: hmla v rovine, voľné štíty, malý vietor, mokrá pôda. Normálny teplotný gradient -6,5 °C/km; v reálnom prípade ( VK3KAQ) +7 °C v 20 m.

Záver autora: Hepburn a F5LEN nestačia, slúžia profilom v nadmorskej výške pozdĺž trasy. Pre nás: tieto dátumy sú prípady overovania, ktoré sa majú prepočítať s historickými údajmi modelov, a index na ceste musí porovnať kvótu kanála s kvótou staníc.

4. Talianske skúsenosti (G. IV3GBO, "Typy šírenia VHF SSB/CW," 2017): vykonané medzi 450 a 3000 km, s vrstvami medzi 450 a 3000 m, hmla rozšírená ako vodítko, otvory po mori na Jadrane smerom k Puglia, Albánsko a Grécko. Roztrúsené od 100 do 500 km.

5. Introductions (P. Wilton M1CNK, "Industrie to VHF/UHF Productionon," 2013): 70 cm sú často lepšie ako 2 m; s 10 m tyč pri 144 MHz počiatočný uhol je asi 3°, s 20 m približne 1,5°.

6. NASA správa (E. Hirschmann, "Troposférické a ionosférické účinky na rádiofrekvenčné (VHF-SHF) komunikácia," Goddard X-731-67-89, 1967): To sa týka chýb vzdialenosti k satelitom, nie DX. Prináša späť ten istý N vzorec a exponenciálny profil (napr. na Floride: Ns 370, ΔN −55, konštanta 0,161 km) a posiela dva NBS zdroje na vyhľadávanie: Bean a Thayer, "CRPL Exponencial Reference Atmosféra," Monograph 4 (1959), a Bean, Horn a Ozanich, "Klimatické grafy a dáta rádio refraktívne index pre Spojené štáty a svet," Monografia 22 (1960).

## 9. Z knihy VHF/UHF DX, kapitola 2

I. White G3SEK (edited by), *The VHF/UHF DX Book*, DIR Publishing 1995 (ISBN 0 9520468 0 6; digitálna replika 2017), kapitola 2 "VHF/UHF Propagation', s. 2-13, 2-222-29 a 4-11. Je to najbližší text k rádioamatérskemu zážitku medzi tými, ktoré boli zhromaždené; potvrdzuje prahové hodnoty už použité a pridáva nové synoptické pravidlá.

Číslo

- Rovnaký vzorec N (77,6 P/T + 3,73·10^5 e/T2); príklad: 1000 hPa, 7 °C a = 10 hPa poškodenie N = 324. V dobre tvarovanej atmosfére, napríklad po búrke, N je takmer lineárne asi 39 jednotiek/km v prvých 2 a3 km.
- Gradienty medzi 0 a −79 N/km sú normálne, pod −79 je super-refrakcia, viac ako 0 sub-refrakcia. Kniha trvá na tom, že dlhý DX nepochádzajú z jednoduchého super-refrakcie, ktorá by mala zostať priamo na celej ceste, ale z káblovodov.
- Minimálna hrúbka kanála, trochu obozretnejšia ako Kerr a Hepburn:

| Pásmo | Minimálna hrúbka |
| 50 MHz |  400 m  |
| 70 MHz |  300 m  |
| 144 MHz |  200 m  |
| 432 MHz |  100 m  |
| 1,3 GHz |  50 m  |

- Pomer nie je úmerný vlnovej dĺžke (asi 65 λ pri 50 MHz, 220 λ pri 1,3 GHz), pretože okraje vedenia sú "rozdelené." V Spojenom kráľovstve sú potrubia časté na 144 MHz, ale takmer nikdy na 70 MHz alebo nižšie, a často 432 MHz sú silné, keď 144 sú slabé.
- Rovnica signál klesá ako 1/d miesto 1/d2: až 1000 km útlm môže byť podobný ako voľný priestor.
- Minimálna kapacita stanice na 2 m (100 W, 15 dBi yagi, 2 dB hluk): 217 dB, t. j. približne 400 500 km v SSB v priemerných podmienkach; v CW približne 20 dB viac (s. 4-7). Súhlasím s tabuľkou KK6MC.
- Scatter: strata stúpa asi o 15 dB trojnásobne frekvencie a 10 dB pre každý stupeň uhla rozptylu (Yeh vzorec; v texte extrahovaný konštanta je nečitateľná, takže sa musí vziať z verzie W3SZ). "Up" rozptýlenie z konvekčných buniek bežne pokrýva až 500 km, 1000 km s dobrými stanicami. Po silných dažďoch a rozšírených podmienkach je na všetkých pásmach nedostatok.

Typy kanála

- Na zemi: na zemi sa zle šíria, pretože pôda sa pohlcuje a šíri; na pokojnom mori dorazí ďaleko (Scilly Odparovacie kanály sú často vysoké len okolo 15 m a slúžia mikrovlnným rúram.
- Vysoká: medzi 450 a 2000 m, vzhľadom k dlhšej DX na zemi; signál "vysoká" nad stanice nižšie. "Lose" kanály poskytujú menej silné signály, ale tiež umožňujú medziľahlé stanice.
- Spojky majú presné smery a môžu vydržať hodiny alebo dni, na rozdiel od sporadického E.

Nové Synoptické pravidlá pre index

- Vek anticyklónu: vzduch klesá do stredu niekoľkých metrov za hodinu a naopak je znížená ako sa vyvíja vysoký tlak; väčšie otvory prichádzajú po niekoľkých dňoch šírenia už dobré. Krátkodobý anticyklón nestačí.
- Vysoký tlak hrana: vrstva je kupola a na okrajoch je nižšia, takže stanice sa lepšie.
- Smer: Bolo pozorované, že dlhšie trasy beží pozdĺž izobare alebo cez dorzáre (Flavell G3LTP, 1984), ale tam sú vynikajúce spojenia aj v radiálnom smere od centra. Príklad 29.30. augusta 1987: z Anglicka sa po presune anticyklónu otočil o 180° za dva dni (Španielsko, Francúzsko, Taliansko, Rakúsko, Nórsko).
- Proti: silné izobare (silný vietor), stratocumuliformné mraky na okraji vrstvy (obmedzte najvyššiu frekvenciu).
- Predné časti: studená predná časť tlačí hore horúci vzduch a vytvára spätný chod vpredu; najlepší smer je vpredu, zatiaľ čo prechádza, ale trvá málo.
- Sezóna: V Spojenom kráľovstve sú na konci leta a na začiatku jesene častejšie anticyklóny.
- Vizuálne stopy: dym zachytený v tmavej čiare nad horizontom; s vysokým tlakom, vrstvy zhluky so základňou pod 1000 m indikujú pozemný kanál, základ nad 1000 m vysoký kanál.

Referencie uvedené pre vyhľadávanie: R. G. Flavell G3LTP, "Štúdie extenzívnej anticyklonickej promócie...", *Radio Communication*, February 1984, s. 128; J. Gannaway G3YGF, "Troposférický scatter Promótionon," *Radio Communication*, september 1981, s. 710; P. W. Sollom G3BGL, "A Little Flutter W6ZGN

## 10. Overený amatérsky rádiový model: Biancomano a Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, "Practical Troposféric Model for VHF/UHF Propagation Production Prediktion," *National Weather Digest* 22:2, Jún 1998, s. 15](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf)

- Údaje: rádiová vlna 00 a 12 UTC 47 staníc vo východných Spojených štátoch, 1995 a 1997; pozemná analýza 700 hPa (na začiatku až 400), na N profile, bez M. Okrem pobrežia, ostrovov a hôr.
- Pre každý prieskum: prítomnosť a podiel kanála, maximálna vlnová dĺžka, ktorá môže viesť (často asi 100-násobok vlnovej dĺžky), maximálny gradient potenciálnej teploty medzi susediacimi úrovňami a medzi jednou úrovňou a druhou vyššie (pre hrubšie vrstvy). Pozemné kanály sa nepoužívajú.
- Prvé pravidlo, perzistencia: indexy stability (zdvíhaný index, K, celkové hodnoty, SWEAT) zostávajú v rámci 3 jednotiek od prieskumu 12 hodín pred a úroveň tlaku, kde potenciálna teplota rastie, zostáva približne rovnaká. Ak áno: predpoveď zlepšenia v nasledujúcich 12 hodinách.
- Druhé pravidlo, dynamické ovládanie: limitná vrstva, ktorá nestúpa, CAPE, variácia hrúbky na 500 hPa na troch prieskumoch, variácia sklonu v rámci limitov.
- Prah pásma: vedenie schopné prenášať 100 cm alebo viac indikuje otvorenie na 432 MHz, takmer isté na 222 a často aj na 144.
- Vesmírny filter: stanice na šesťuholníkovej mriežke asi 400 a 500 km; predpoveď stojí len za to, ak aspoň 5 kontigue stanice sú pozitívne (ak môžete preskočiť jeden). S piatimi stanicami sa pravdepodobnosť falošne náhodného pozitívneho stane zanedbateľnou.
- Overenie udalostí uvedených v stĺpci "Svet nad 50 MHz" QST: v roku 1996 sa plánovalo 48 podujatí 67 (71%); 12 z 15 významných podujatí. Presnosť až 70% na 432 MHz a približne 60% na 144 MHz pri 12 hodinách; poskytuje takmer dvojnásobok skutočných udalostí; 9 chýb z 19 sú predpovede, ktoré prišli neskoro, pretože mnohé zlepšenia sa tvoria za menej ako 12 hodín.
- Pridané na stanice ďalej na sever: vietor rotujúci v smere hodinových ručičiek lezenie (horúci štart) ako ďalší stav.

Z kompletného PDF (dodal autor programu) ďalšie tri podrobnosti. Ich pravidlo "hrúbka = 100 krát vlnová dĺžka" dáva 600 m pre 6 m, 200 m pre 2 m, 125 m pre 1,25 m a 70 m pre 70 cm (fig. 3): pre 70 cm je menej ťažké ako Kerr a Hepburn (cca 89 m), pre 2 m sa zhoduje s VHF/UHF DX Book. Vietor, ktorý sa otáča v smere hodinových ručičiek, podľa autorov stúpa, najmä preto, že prináša teplejší vzduch v nadmorskej výške, čo spôsobuje, že pád N rastie s tlakom; vlhkosť, ktorú so sebou prináša, sa nezdá destabilizovať. Dodatok získava dN/dh v uzavretej forme pre konštantné tepelné gradientové prostredie s prudkým skokom gradientu tepla a vlhkosti pri danej kvóte: užitočné ako testovací prípad pre náš kód. Ich stopa po sklone potenciálnej teploty pochádza z K. H. Jehn, "Využívanie potenciálneho indexu lomu v synoptickej rádiometeorológii," *J. Meteorológia* 17, 1960, s. 264.

Čo vziať pre náš index: (1) termín pretrvávania: porovnajte profil s profilom 12 hodín pred tým, že v hodinovej prognóze máme voľný; (2) vesmírny filter: bod sa počíta len vtedy, ak zlepšenie pokrýva plochu niekoľkých stoviek km, ktorá znižuje falošné poplachy; (3) indexy stability vypočítané z teploty a rosy na 850, 700 a 500 hPa (Open-Meteo už dáva výťahový index a CAPE); (4) vietor, ktorý sa otáča s nadmorskou výškou medzi 1000 a 850 hPa; (5) ich metóda overovania, ktorá je rovnaká ako my chceme robiť s reklamy programu.

Ďalší zdroj v tom istom balíku, ktorý je pre nás málo užitočný: P. K. Rastogi, "Stratosférické a troposférické štúdie (S-T) na Millstone Hill', NASA 1984 (dokument 19840019069): radar 440 MHz, ktorý meria turbulenciu čistého vzduchu medzi 4 a 25 km, s tenkými vrstvami spojenými s gravitačnými vlnami a rezaním vetra. Potvrdzuje, že turbulencie, ktoré vytvárajú rozptyl, sú vrstvené, ale týkajú sa príliš vysokých šancí na kanály.

## 11. Tretie kolo zdrojov (20. novembra)

Historické prípady vysokých kanálov. J. Macoun OK1VR, 2 m od vrcholu Sněžky (1602 m), jeseň 1958, článok 1959 preložený v roku 2006 ([ok2kkw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)). 27. a 28. októbra 1958 prvé Československo a spojenie na 145 MHz ( G5YV, 1220 km) a mnoho ďalších medzi 1100 a 1250 km, so stabilnými signálmi po 23. Autor ju pripisuje dvom preneseným inverziám, horným asi 2200 m, so signálom vracajúcim sa do zeme len vtedy, keď sa spodná vrstva rozpustí; stav, ktorý uprednostňuje pomalá anticyklopéna z Atlantiku na kontinent. Z domova (Praha) jeho maximum bolo 530 km: iba stanice v nadmorskej výške vstúpil do vedenia. Je to ďalší prípad overovania (napr. LA0BY 2018) a potvrdzuje, že index by mal hľadať viac ako 2000 m a porovnať ho s podielom staníc.

Historické knižnice NBS (užitočné pre hľadanie klasických diel, nie pre vzorce):

- W. Nupen, *Bibliografia na Troposférickej Propagácii rádiových vĺn*, NBS Technická poznámka 304, 1965, 356 strán, s indexom tém (tiež "predpoveď stavu") a podľa krajiny. Talianske záznamy hlásené: L. Sacco, "Rádiové spojenie v režime atmosférickej super-refrakcie," *High Frequency* 24(6), December 1955 (Taliansky výskum: super-refrakcia pod 500 m častý deň a noc); S. Silleni, *Annali di Geofisica* 8(1), 1955; J. M. Clara e A. Antinori, prova tra Sardegna e Minorca da 238 a 297 MHz (390 km), *Elektrická komunikácia* 33(2), 1956. A. F. Clarke (Fort Huachuca, 1956) vydáva denne predpoveď maximálnej vlnovej dĺžky zachytenej na nasledujúce ráno, z vlhkosti, teploty, tlaku a predpovedí vetra.
- R. L. Abbott, *Bibliografia Troposférické rádio Wave Scattering*, NBS, materiál do januára 1960, asi 670 položiek objednaných ročne: užitočné pre troposcatter časti.

Zverejnenie a pozorovania: F. W. Odenthal DK5EQ, "Ausbreitung 2m Diskusia o VHF marina a AIS ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)): rozsah 1,23·√h (ft) námorných míľ na anténu, rozptýlenie až do asi 50 nm, typické vylepšenia trope 100 Chelmsford ARS, december 1983: v 2 m súťaže stanice východného pobrežia Anglicka sú zvýhodnené morskou cestou na kontinent. DM2HB DK5YA VK3DXE EI7GL

ARRL, * Radio Amateur's VHF Manual*, 1972, cap. 2 "Propagácia za horizontom" (spp. 17 Už známe prevádzkové tipy (sledovať vysokotlakové oblasti, ktoré sa pohybujú pomaly a predovšetkým ich zadné hrany; skoro jeseň najlepšie obdobie; pobrežné oblasti, veľké jazerá a údolia riek, ktoré uprednostňujú stratifikácie). Dve nové údaje: (1) fig. 2-4 porovnáva štandardnú atmosféru (relatívna vlhkosť 70% až 12000 ft, minimálna fraktúra) s typickým polomerom silného lomu, s priezračným pomerom teploty a zmesi približne 4000 ft (cca 1200 m): je to podpis, ktorý musí náš výpočet rozpoznať na úrovniach GFS; (2) skutočné kanály sú zriedkavé pod 144 MHz a najčastejšie v UHF, podľa minimálnej hrúbky. Užitočné QST bibliografia hľadať: Hull, "Air-Mass Conditions and the Bending of UHF Waves" (jún 1935, máj 1937); Hoisington, "Painless Prediktion of Two-Meter Openers" (október 1949); Collier, "Upper-Air Conditions for 2-Meter DX" (september 1955); Botts, "A Night to Pamätať" (január 1970, vykonáva na zemi).

PA0EHG, strana "Tropo" ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): zhrnutie troposcatter z článku G3YGF, už v príručke. Použite dva praktické pravidlá na kontrolu nášho výpočtu rozptylu: asi 10 dB extra strata pre každý uhol na obzore každej stanice a viac ako 100 km približne 9 dB plus každá 100 km trasa (60 míľ); spoločný objem začína od približne 600 m na 100 km a od cca 9 km na 480 km. Nelieči kanály, prieskumy ani prahy lomu.

PA0EHG, PROPBER kalkulačka ([page](https://pa0ehg.com/propagat1.htm), propber.exe program poskytuje autor programu). Je to 36 kB DOS program napísaný v BASIC a zostavený s Microsoft BASIC Compiler 5.60 (1985): beží len pod DOS alebo DoSBox. Z interných textov: žiada o frekvenciu, vzdialenosť (alebo dva lokátory, aj v starom formáte QRA "E-lokátor"), pre každú anténnu výškovú stanicu, hlučnosť, FI pásmo, výkon, prekážku (výška a vzdialenosť) a prírastok antény (0 = vyčnievajúci priemer, účinnosť 50%). Description Číselné konštanty nie sú v zostavovanom spustiteľnom údajovom čitateľné. Navrhovaná metóda pre vysoké pásma je tiež pre nás užitočná: kalibrácia výpočtu na namerané spojenie (rozdiel medzi nameranými a vypočítanými S/N) a uplatňovanie rovnakého rozdielu na iné pásmo. Je to rovnaký model nášho výpočtu, ale používa profil S RTM1 namiesto jednej prekážky a predpoveď GFS namiesto pevnej marže.

PROPBER, konštanty prečítané v spustiteľnom stole (čísla vo formáte Microsoft Binary Format). πre, v tabuľke údajov vedľa textov: 8,68589 (= 20/ln 10, t. j. 20 logov vypočítaných s prirodzeným logaritmom), 32,5 (voľný priestor: 32.5 + 20 log f\[MHz\] + 20 log d\[km\]), 24000 a 0,1 plus 40000 a 0,3 (vodo: 0,1 dB/km od 24 GHz, 0,3 dB/km od 40 GHz) Pravdepodobne preto, že opakujete, ale vnútri kódu: −69,5 (rovnaký konštantný termín ako Yeh's side-scatter formula vo VHF/UHF DX Book), 332 (pozemná refrakcia), 8733 km (ekvivalentný polomer, k Presný vzorec scatter nie je zrekonštruovaný bez demontáže programu. T0

PROPBER, formuly rekonštruované kódom (demontáž zostavenej časti; operácie sa nazývajú do knižnice BASIC s operátormi v pamäti, ktorých hodnoty v dvojitej presnosti boli dekódované). Konštanty - 69, 5, 332 a 8733, ktoré sa našli predtým, boli falošné pozitívne v knižnici.

- Optická vzdialenosť: d = 5 · (√h1 + √h2) km (h v m). Je štedrejší ako klasická 4,12 (k = 4/3): rovná sa k
- Strata optiky: L = 32.5 + 20 log f\[MHz\] + 20 log d\[km\] plus 0,1 dB/km nad 24 GHz a 0,3 dB/km nad 40 GHz.
- Troposcatter: Lts = L + 23 + 10 log f + 0.0674·d, t. j. 55.5 + 30 log f + 20 log d + 0.0674·d (formula G3YGF s konštantnou 2 dB vyššou). Žiadny uhlový výraz alebo reaktivita.
- Oprava "pre stránky": program žiada prekážku a vzdialenosť, ale blok, ktorý by mal používať je prázdny; korekčná premenná zostáva 0, takže "správna" strata sa rovná rozptylu.
- Hluk: T = 290·(10^(NF/10) - 1), N = −228,6 + 10 log T + 10 log B (dBW), bez teploty antény.
- S/N = 10 logov P + G\_tx − L + G\_rx − N. "S maximálnym abnormálnym šírením" využíva optickú stratu namiesto rozptylu, t. j. ideálny prípad vedenia bez strát za voľným priestorom.

Porovnanie: 300 km a 144 MHz PROPBER dáva asi 189,9 dB, G3YGF 187,9, naše P.452 187,8 (s nulovým horizontom): rozptyl sa zhoduje s 2 dB. Náš výpočet pridáva uhly horizontu z profilu, teplota antény, a namiesto "maximálneho" pevné očakávané vrstvy so zvyškovým párenie a útlm.

Rohde & Schwarz, P. Denisowski, "Porozumenie VHF Propagation," Vzdelávacia poznámka, apríl 2023 (24 strán, šírenie). Potvrdzuje už získané body (vodené ako dvojrozmerný vlnovod, signál, ktorý klesá lineárnym spôsobom so vzdialenosťou až 1500 km a viac, trvanie od hodín do dní; dobrá pôda na mori a vzácne na súši; vyvýšený z niekoľkých sto metrov na niekoľko kilometrov). Dva užitočné pozorovania pre index: (1) vývoj v čase: novovytvorený kanál často nesie len najvyššie frekvencie a zahusťuje sa, začína prinášať aj najnižšie; hodinová predpoveď by preto mala zobrazovať "predčasný" 432 MHz na 144 MHz, keď vrstva rastie; (2) umiestnenie staníc: najlepšie podmienky sú medzi stanicami blízko konca vedenia, zatiaľ čo tie pozdĺž cesty vysokého kanála často neplynú, pokiaľ sa nevykonáva "kto prehrá." Kanály sa pohybujú s atmosférickým počasím.

Stránka PA0EHG, iné stránky. Pre metrope, PathProfile of M. Willis G0MJW ([scheda](https://pa0ehg.com/srtmp.htm), [download](http://www.mike-willis.com/Software/ASTERPathprofile.zip)): profil trasy z očakávaných celkových strát a kvót súborov, spoľahlivo posúdiť pre troposcatter v mikrovlnnej rúre; nelieči rozptyl z lietadiel alebo dážď. Užitočné ako druhý odkaz na porovnanie našich S RTM1 profilov a strát. Zvyšok lokality pokrýva EME, mikrovlnnú rúru, airstrip (AirScout, SM7LCB mapy) a opatrenia v 80 m.

PathProfile by G0MJW (Mike Willis), Aster a SRTM balíčky a SiteData (dodal autor programu; [](https://www.mike-willis.com/software.html)softvérová stránka ]. Program Windows 2018: profil z Aster alebo SRTM na 1" alebo 3," straty v optike, difrakcii, troposcatter (platí do 1000 km), kanál a plyn, s percentom času, medián trasy, zlomok trasy na mori a vzdialenosť od pobrežia: štruktúra je štruktúra P.452/P.1812, ale pomoc (komprimovaný.chm súbor) nebolo možné prečítať tu. SiteData konvertuje lokátory a dáva podiel z GT OPO30.

Najužitočnejšou časťou sú súbory údajov: štatistické mapy ITU-R P.453 na globálnej mriežke s rozlohou 1,5° (121 riadkov od 90° s. z. š. do 90° s. z. š., 241 stĺpcov od 0° na východ): gradient dN/dz v prvých 65 m prekročený na 1, 10, 50, 90 a 99% času; pre pôdne kanály percento prítomnosti, hrúbky a intenzity; pre tie, ktoré sú vysoké, základný podiel, hrúbka, intenzita a maximálna nadmorská výška; plus mokrý termín a teplota. Interpolované ročné hodnoty:

| Umiestnenie | Pozemok: % / hrúbka / intenzita | Vysoký plynovod: % / základ / hrúbka / intenzita | dN/dz prekročil 10% času |
| Neapol |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | −380 N/km |
| Tyrrhenian (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | 674 N/km |
| Trapani |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | 520 N/km |
| Brindisi |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | - 405 N/km |
| Jadranské more (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | - 399 N/km |
| Miláno |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | − 172 N/km |
| Udín |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | 176 N/km |

Pre náš index sú pripravené klimatické referencie: hrúbka a intenzita typické kanály namiesto 50 m pevné potenciálne kanál, a anomálie "dnes v porovnaní s normálnou" na zónu. Údaje ITU: bezplatné na nekomerčné použitie, aby sa skontrolovali pred ich zaradením do verejnej verzie. Súbor ZO NES01.DEM (58 MB, 2' mriežka) obsahuje klimatické zóny vnútrozemské/pobrežné/morské; formát nie je zdokumentovaný a ja som ho nedekódoval. Stránka Willisových rovníc obsahuje iba fyzikálne konštanty, voľný priestor a anténu.

Štvrté kolo zdrojov (20. novembra).

Indermuehle a Suzuki 2026, predpoveď plynovodov GFS pre observatórium CSIRO Murchison (arXiv 2608.12861): je najbližším zdrojom LIVETROPOVHF. Profily N od GFS do 0,25° (povrchová do 2 m, potom hladina izobárie, hladina pod zemou); pre každý minimálny stĺpec dM/dz pod 3 km, základná a horná časť vedenia, integrovaný deficit M; potom P.452-16 a P.1812-6 s GLO-90 25 dĺžkami od 98 do 1005 km, od 88 MHz do 2,68 GHz každú hodinu. Výsledky: Medián AUC 0,72 na 25 kanáloch; percento času P.452 p by sa malo použiť ako kalibračná konštanta (výber p = 20%), pretože model ITU s instantnou ΔN nie je definovaný v rámci vedenia; schopnosť zostáva takmer rovná až 3 dni vopred. Pri 162 MHz (AIS) majú prijaté trasy najvyššiu časť vedenia 67% (0,26 verzus 0,16) a najvyššiu hĺbku kanála 49% (28 verzus 19 M) pokiaľ ide o kontroly: na mori je spojka takmer vždy, rozhodne o úseku pôdy. GFS vidí polovicu času na zemi v porovnaní s ERA5 (4,5% oproti 10,4% bunkových hodín), zatiaľ čo na mori vidí viac (73% oproti 56%). Index "koridoru" (priebežné potrubia pozdĺž trasy, vrstvy s minimálnou hrúbkou 40 m) zlepšuje výsledok: potvrdzuje, že kontinuita pozdĺž trasy je správnym kritériom.

Longley a ryža, ESSA ERL 79-ITS 67 (1968): metóda nepravidelného terénu od 20 MHz do 20 GHz (po ITM) s nepravidelnosťami pôdy Δh, Ns od 250 do 400, ekvivalentný polomer 8497 km (4/3), horizonty, difrakcia, rozptyl dopredu a variabilita v čase a na miestach pre klímu. Štatistický odkaz nie je okamžitý.

ITU-R P.834-2: zakrivenie polomeru, ekvivalent polomeru Zeme k·a, M = N + h/a (h v m, v tisícoch km), exponenciálny model; nič nové ako to, čo už bolo použité.

AGARD CP-70 (1970), troposferické šírenie: tenké vrstvy od 1 do 100 m so silnými gradientmi sú bežné; koeficient odrazu vysokej vrstvy je približne úmerný vlnovej dĺžke s náhodnou disperziou približne 14 dB. Opatrenia pri 90 MHz: pokles o 910 dB viazaný na N skok koncentrovaný asi 350 m, na vrchole vrstvy oblakov. Prechod studenej prednej eliminuje vrstvy: Difrakcia zostáva.

U.S. URSI správa, Komisia 2 (1960): priemerná hrúbka vysokých vrstiev od 60 do 210 m (v súlade s P.453), minimálny gradient pre 157-160 N/km.

Buscemi K2OVS, VHF návod na šírenie: typické vzdialenosti pre zvrat a kanál pre pásmo, kanály spojené s vysokým stacionárnym tlakom, maximálne za úsvitu.

Wolinsky-Mancini a 2024, AIS na Kanárskych ostrovoch: AIS rádiá a recepcie; kanály sú oveľa častejšie v teplých mesiacoch (inverzia alisejanov).

Nigérijské a malajzijské články (Ekah, Iloke, Mat, Igwe): lokálne korelácie medzi teplotou, vlhkosťou a meranými stratami, slabé a protichodné (r −0,09 až 0,27); užitočné len ako potvrdenie, že samotné veľkosti terénu nestačí.

Herben 1984, tesis TU Eindhoven (mikronóda): 30 GHz na 8,2 km a OTS satelit na 11,6 GHz; iskrenie pre turbulencie, multi-cesty a dážď. V prípade mikrovlnných rúr dominujú dynamické straty dažďa a turbulencie, nie potrubia.

Baars, The Paraboloidal Reflector Antenna (mikronde): zisk z podobenstva G = η· (πD/λ) 2 s typickou účinnosťou 0.5 Pomohlo by, keby sme pridali podobenstvá ako alternatívu k Yagi DL6WU pre 1296 MHz a viac.

Už analyzované alebo zlého obsahu: Hirschmann NASA X-731-67-89 (dvojité), Reidova VHF ST radar (vietor a turbulencie, nevodené), Scribd slide a všeobecné poznámky na troposfére.

Piate kolo zdrojov (20 November, večer).

Rogers 1997, Radio Science 32(1): Bayesov odhad parametrov vysokého kanálu (základná rýchlosť, M deficit, hrúbka) z faktorov šírenia meraných za horizontom, s modelom parabolickej rovnice; VOCAR experiment v Kalifornii pri 143, 263 a 375 MHz, porovnanie rádiových vĺn. Signál veľmi závisí od základne a deficitu M (oprava 0,81 až 143 MHz pre deficit), takmer vôbec od hrúbky (-0,17): hrúbka nie je možné získať zo signálu. Pre nás: Výpočet B (hrúbka podnebia) sa počíta menej ako deficit a podiel; a metóda je základom pre back office, to je získať vedenie z viacerých frekvencií.

Moler and Holden 1960, "Troposférické šírenie rozptylu a atmosférické cirkulácie," J. Res. NBS 64D, pp. 81 a ďalej (čítajte v pôvodnom znení 22. 11. 2026, doi 10.6028/jres.064d.014). Kalifornské námorné trasy do Point Loma (San Diego): Santa Barbara 190 míľ, bod Mugu 144, San Clemente 78; 1365 MHz a 9380 MHz, nie VHF; január 1958, v priemere 1 minútu. Prípad z 26. marca: posilnenie asi 20 dB počas 3 hodín smerom na poludnie, keď cyklický vír oslabí a začne s morským vánokom a svahom; v 15 sa zreformuje konvergencia a signál klesne približne o 22 dB z maxima. Štatistiky (tabuľka 1, 500 hPa, 33 prípadov): so vzduchom, ktorý padá priemerný signál, je vyšší ako 2,5 dB ako so vzduchom stúpajúcim, v pásme L a X. Signál sleduje rozdiel vypočítaný na 500 hPa v znamení a amplitúde zmien bez korelačného koeficientu. Nízke vrstvy (pod asi 600 m) uprednostňujú čiastočné odrazy pri rozptyle. Anticyklóny a fronty sa neošetrujú a vyššie uvedené "exkurzie do 60 dB" nie sú: pochádzajú z ojazdeného súhrnu. Pre nás: vertikálny pohyb je dobrým prediktorom a +1,5 dB pre prízemie a -2 dB pre horný vzduch sú rovnakého rádu ako priemerný meraný rozdiel (2,5 dB); 20 dBs je jeden prípad, nie typická hodnota a mikrovlnná rúra.

Banafaa a Muqaibel 2025, IEEE Access: prieskum potrubí (odparovacie modely, parabolická rovnica, radarové odhady, GNSS a lidar) a klasifikácia s automatickým učením sa o skutočných opatreniach; Podporné vektorové stroje poskytujú najlepšie výsledky. Užitočná ako bibliografia; údaje sú o Perzskom zálive.

ITU-R P.834-7 (2015): aktualizovaná verzia odporúčania o diviačej lokalite tropos; žiadny nový vzorec ako tie, ktoré už boli použité.

Abbott, NBS bibliografia na troposferickom rozptyle (GOVPUB- C13): už v treťom kole. Slide Scribd na kanáloch: všeobecný obsah. Článok ScienceDirect 002191698790304 Nepodarilo sa otvoriť.

Šieste kolo (20. november, noc): Bean a Dutton, ARRL Handbook 2022 and 2023, Balanis.

Bean a Dutton, Radio Meteorology (čap. 6), časti ešte nepoužívané. Citlivosť v teréne na Ns: 0,18 dB na jednotku N na ročnom cykle; 0,14 - 0,24 dB/N v dennom cykle (maximálne zavčas rána), až 0,2 - 1,1 dB/N na trasách 175 - 200 km v zime; opravy 0,8 - 0,95. Parameter zložený z Misme: M = a(g\_e − 40 + b·ΔW^n) s a = 0,5 dB na N/km, ktorý kombinuje ekvivalentný gradient a tepelnú stabilitu ΔW (pracovať na zdvíhaní vzduchu 1 km): v Sahare pri 471 MHz na 160 km je jedinou stabilitou 20 dB medzi januárom a júnom pri takmer rovnakom gradiente. potenciálne refrakčný index K (N hlásené adiabaticky na 1000 hPa): zdôrazňuje vzdušné masy a anticyklonické subsidence (Flavell a Lane).

príručka ARRL 2022 (99a ed.), § 19.4.319.4.4. Užitočné inverzie medzi 250 a 2000 m; inverzie žiarenia (séra, pokojný vzduch, suchá pôda) zriedkakedy viesť pravdivé; pomalé anticyklóny poskytujú najrozsiahlejšie kanály pre subsidence, s inverziami medzi 500 a 3000 m, silnejšie večer a ráno; na severnej pologuli najlepšie trasy sú južne od najvyššieho centra. Horúca predná časť: dobré podmienky niekoľko sto kilometrov dopredu; studená predná časť: silná, ale nestabilná otočná za prednou časťou, paralelne. Foehn (aj v Alpách) a zemný vánok (chladený vzduch na zemi, horúci na 200300 m; tiahne sa cez 800 km pozdĺž pobrežia). Scirocco: horúci a suchý vzduch zo Sahary nad chladnejším morským vzduchom, veľmi silné inverzie z východného Stredomoria na Gibraltár, pravdepodobná príčina rekordov viac ako 1500 km v mikrovlnnej rúre. Troposcatter: teoretický limit cca 800 km, maximálna typická polovica. 100. ed. (2023) má kratší text: inverzie pri východe a západe slnka až do +20 dB a +20

Balanis, Antenna Theory (4a ed.), § 10.3.3: Tabuľka Viezbicke (NBS TN 688, 1976) pre Yagi optimalizovaný pri 400 MHz: boom 0,4 / 0,8 / 1,2 / 2,2 / 3.2 / 4,2 λ → 7,1 / 9,2 / 10,2 / 12,25 / 13,4/14,2 dBd. Naša DL6WU formula (8.6 + 8 log10 boom/λ) dáva o 1 dB menej medzi 0,8 a 4.2 λ a vracia sa v súlade s tabuľkami DL6WU na 9,5 λ: opatrný odhad. So správnym vzorcom na origináli (9, 4 + 7, 8 log10 boom/ λ, od 22. novembra) odvrhnutie klesne pod 0,2 dB medzi 1,2 a 4,2 λ (12,1 dBd až 2,2 λ oproti 12,25) a zostáva 0,6 dB až 0,8 λ.

Siedme kolo (21. novembra): QST, QEX, W3SZ, GitHub úložisko.

QST 20002025 (14 čísiel, 10 lôžok s rozpoznávaním textu). Iba správy v sekcii Svet nad 50 MHz, užitočné ako prípady pre validáciu modelu: September 519, 2000, vykonáva sa na 144 MHz a ďalej na východnej polovici USA; 14 QST November 2022 odporúča Hepburn mapy. Žiadny technický článok o tropo predpoveď. FT8 VE4MA N0OY Q65

QEX a K1JT, Franke a Taylor (QEX 2020) na FT4/FT8 módoch: prahy dekódovania FT8 −20,8 dB v 2500 Hz na kanáli bez evanjelizácie, −18,6 dB s evanscenciou v stredných zemepisných šírkach; FT4 −17,5 dB. Ostatné čísla QEX 2023 a 2026 neliečia tropo (tri čísla 2023 sú neprečítané skeny). W3SZ 2019: prahy pomalého režimu ( JT4A -23, JT9A -27, JT65A -25, QRA64A -26, FT8 -21 dB) a kritériá výberu medzi citlivosťou a toleranciou posunu frekvencie.

GitHub. TropoLink (LukiBox, Apache-2.0): návrh trás troposcatter s P.617-5 (s N0 a ΔN), NBS TN101 a ITM na boku a ich rozdiel zobrazený ako neistota; P.617-5: L = F + 22 log f + 35 log θ + 17 log d + Lc, F = 0,18· N0 ·e^(-hs/7,35) − 0,23·ΔN, Lc = 0,07·e^(0,055(Gt+Gr)). ai-f/troposcatter ( DL2FZN 2013, z VHF/UHF Handbook RSGB): Lts = 24,44 + 4,283 ln f + 0,06656 d nad voľným priestorom, sklon v blízkosti PROPBER (0,0674 dB/km). QRadioPredikt: ITM a ITWOM 3.0 s S RTM3, pôdy použitie Corins a NEC diagramy. Netýka sa: Tropos (poveternostné aplikácie), opera-adt/tropo a troposim (troposferické oneskorenie pre InSAR), tgbot\_troposcatter (vojenská stanica), sdr-troposcatter-gui (ovládací panel), Troposcatter-Propagation-Delay (Oneskorenie ERA5), Radio-Communic-Project (ditaktický simulátor), PyLayers (kanál).

Ôsme kolo (21. novembra): NTIA a ITU modely na GitHub. NTIA/itm (Longley-Rice, ITS): 20 MHz Ne modeluje kanály: slúži ako referenčná "normálna atmosféra" so štatistickou variabilitou. NTIA/p528 a eeveetza/p528 (MATLAB): ITU-R P.528-5 pre letecké spojenia s pôdou, 100 MHz30 GHz, 1,5 a 20 000 m terminály, s 1 Možné použitie: bilancia rozptylu vzduchu (dve pozemné trasy a radarová časť). eeveetza/p2001 (MATLAB): ITU-R P.2001-6, model všetkých polí s difrakciou, troposcatter, abnormálne šírenie (vodivé a vrstvy, Lba) a E sporadické, percento času 0 Možné použitie ako porovnanie ITU namiesto P.452, s GFS ΔN namiesto máp ako v štúdii CSIRO; používateľ by mal tieto mapy stiahnuť podľa P.453. Validácia 1.3.0 z 593 QSO (29/12/2062): oddelenie udalostí/porovnania 0,79 bez opráv, 0,91 s Misme a subsidence; GFS nevidí kanál na mori pri 144 MHz (guadagno bez opráv nikdy viac ako 15 dB). N0

Deviate kolo (21. novembra): IU3QEZ, NTIA, PyWaveProp, ACE High, validation 1.3.1. IU3QEZ (S. Fabrika), prezentácia "Cez horizont vo VHF" (GEKO Magazine n. 69, september 2026): spoločný objem d2/ (8·R\_eff) s R\_eff 10 dB je cca 100 km; stanica IQ3QC 5 W s 3×5 prvkami, 120 QSO, maximálne 788 km (Alpe Adria 2026). Bibliografia prezentácie, ktorá sa má obnoviť: Norton 1959 (J. Res. NBS 63D, System Loss), Bray 1961, ARRL Antenna Book 2023 fig. 4.29, Rice-Longley-Norton-Barsis NBS TN 101, Bean and Thayer 1959, Barsis and Kirby 1961 (difrakcia na hrebeni), Deygout 1966, P.1812, AirScout DL2ALF, P.834 NTIA PropLib (wiki): eHata, ITM, LFMF, P.528, P.208 (strata z veľkoobjemu), s C++,.NET, MATLAB a Python; žiadny recirkulačný model; ITS stránka: tiež IF-77 (vzduchová ropa) a MPM (1 GHz-1 THz). PyWaveProp (Python): Metódy parabolických rovníc s približnými hodnotami Padé, viachrannou difrakciou, PETOOL a RAM komory, tropoferózne a vysoké kanály, nepravidelná pôda: možný základ pre výpočet poľa s profilom GFS M(z). APRS PropView (Apache 2.0): Mapy šírenia VHF z balíkov APRS, hodinová štatistika a otváracie alarmy; možný zdroj 144 MHz dát pre validáciu. Sieť troposcatter NATO v Taliansku (fortifications.net): 900 MHz, 10 kW, 20 m podobenstvá; stanice vrátane Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Vulture, Monte Lauro; historické skúšobné puzdro pre P.617. Netýka sa: Gupta 2021 (tratosférická doprava), zákon 2023 (arktický ozón), delfín #590 a opera-adt/tropo (oneskorenie v SAR), tropo.ino (snímače plynu v Arduino), tropo-webapi (telefonia), cloudtools/troposféra (AWS). Validácia 1.3.1 na 564 QSO tropo: AUC 0,79 (vzor 1.2), 0,80 (+ mäkký kanál), 0,90 (+ misme a subsidence), 0,90 (+ perzistencia); farby vedenia (≥ 16 dB) na 46% otváracích trás a 0% porovnania.

Pridané do rovnakého kola. mikelytaev/porucha vlny (MIT, Python 3): Parabolická rovnica s približnými hodnotami Padé a Numerov, nepravidelná pôda, troposférické kanály; nabitie modifikovaného lomu M(kvóta, lat, lon) z NOAA GFS a nastavenie odparovacieho kanála Monin-Obukhov; žiadna zdokumentovaná funkcia, ktorá vracia stratu na ceste. Je najlepším kandidátom na budúci výpočet poľa s profilom GFS M (slúžil by externej službe alebo prekladu). RP-Sim (AGPL-3.0, JavaScript): Empirické modely pre rádiové siete (Hata, He, Ericsson, dve lúče), žiadne tropo. RIMFading (GPL-3.0, OMNET++): nepravidelnosti toku pre sieťové simulácie, Weibull distribúcia, žiadne tropo. PyLayers: vnútorný rádiový kanál, už vylúčené. qradiopredikt (kantooon, GPL v2): ITM a ITWOM v3.0 s S RTM3 a Corine, už v siedmom kole ako QRadioPredikt.

Desiate kolo (21. novembra): Kniha antény, Norton, Barsis, nové zdroje dát. ARRL Antenna Book (23a ed.), cap. 4 ( K9LA): Brayova metóda (QST November 1961) pre rozsah VHF: hladká strata Zeme pri 99% (300 míľ, 144 MHz: 214 dB), krivky pri 50% a 99% (fig. 4.11): 195 dB v hodnote 100 míľ až 99% a viac ako 250 míľ až 50%; evanscencia ±7 dB cez 100 míľ (−3,5 dB až 50 míľ), SSB 3 dB horšie ako CW, +4 dB odrazu zeme, +3 dB pre spojenie dvoch antén; prírastok výšky (15 m: 4/3/2 dB pri 10/50/100 míľ; 24 m: 8/6/4 dB); Yagi získava približne 10-násobok rozmachu v λ (dBi); 50 W stanica s priemernou anténou: približne 180 dB, 100 míľ, +10 dB až 250 míľ. Východ a západ slnka až do +20 dB a +20 Overenie nášho troposcatteru: P.452 až 50% pri 483 km a 144 MHz dáva asi 204 dB, v súlade s 214 dB Bray 99%. Norton 1959 (J. Res. NBS 63D, zadarmo na NIST): základná strata, systém a cesta antény zisk; Reference atmosféra Ns = 301; ekvivalentný polomer od Ns (250 → 4878 míľ, 301 → 5280, 400 → 6997); L(99) tabuľky s 30 stôpými anténami (100 MHz: 163 dB až 100 míľ, 208 dB až 500 míľ; 1000 MHz: 138 a 188 dB s 28 stôpými podobenstvami); dlhodobá variabilita V(p, θ) v závislosti od uhlovej vzdialenosti (fig. 13). Ryža, Longley a Norton, NBS TN 15 (1959), voľná na NIST. Barsis and Kirby, NBS Report 6751 (1961): 223 km v Colorade s Piekes Peak (4292 m) ako hrana, 100 a 751 MHz; konzistentná strata so zaobleným okrajom (približne 11,5 km polomer); dlhodobá evanscencia 11,7 Digitálna príručka ARRL VHF: balíček, APRS, D-STAR, WSJT (Scatter Method a EME), žiadna časť na tropo. Nové overené alebo vyhľadávané zdroje dát: ECMWF otvorené dáta (IFS 0,25°, úrovne 1000, 925, 850, 700 hPa..., bez 975, 950, 900 a 800; G RIB2 s CCSDS kompresiou, že náš čitateľ nie je dekódovať; už k dispozícii cez Open-Meteo); DWD ICON-EU otvorené dáta (G RIB2 komprimované bz2, súbor pre variabilné a úrovne: mnoho žiadostí); Kanada GDTS na MSC Datamart a GeoMet (asi 15 km, až 33 úrovní; GeoMet umožňuje obilie oblasti; je základom Hepburn máp); GFS NOAA archív na AWS od februára 2021 (globálne súbory, užitočné pre validáciu); APRS PropView len pre validáciu.

Jedenáste kolo (21. novembra): Longley-Rice, GEKO 69. Bean and Thayer, "Models of the Atmospheric Radio Refractive Index," Proc. IRE May 1959 (pp. 740 a nasl.): model 4/3 má správny sklon v prvom km, ale klesá príliš vyššie; 30% zakrivenia polomeru sa vyskytuje v prvom 100 m a 60% v prvom km; priemerný vzťah medzi Ns a pokles v prvom km: −ΔN = 7,32·e^ (0,005577·Ns) (Ns = 313 → ΔN približne −42). Bean and Thayer, "C.R.P.L. Exponencial Reference Atmosféra," J. Res. NBS 63D n. 3 (1959): N(h) = Ns·e^ (−c·h) s c pevnými Ns prostredníctvom tej istej správy; Priemerné Ns USA 313; tabuľky sledovania lúčov (celková krivka 15,2 mrad s exponenciálnym modelom verzus 30,3 mrad so Zemou 4/3). MATLAB refrakciaexp: c = 0,143859 km-1 pre Ns = 313 (rozpätie cca 6,95 km), platí okolo 1 km a 10 mrad. Použitie pre nás: "normálny" profil porovnania môže byť, že CRPL s miestnymi Ns namiesto M = 315 + 0,118·z pevný, a anomália poklesu v prvom km by sa mala merať v porovnaní s −7,32·e^ (0,0055777·Ns) namiesto −40 pevné. Ns = 301 zodpovedá ae = 8497 km; pod 100 km Ns sa počíta málo. Ryža, Longley a Norton, NBS TN 15 (1959), časť I: predpoveď rozdelenia v priebehu času podľa pôdy a troposférickej vlny, základňa TN 101. SoftWright, Wikipedia, Altair WinProp a MATLAB rfprop. LongleyRice: rovnaké informácie (verzia 1.2.2, 20 MHz a 20 GHz alebo 40 GHz, 1 a 2000 km, Ns 301, sedem klimatických podmienok, variabilita času, miesta a situácie); nikto netvaruje kanály, iba štatistický účinok morského podnebia ("vyššie polia po 10% času alebo nie"). GEKO Magazine n. 69 ( IU3QEZ): rádiový horizont 4/3 130 km od 1000 m, 260 km pridaním dvoch staníc; troposcatter so spoločným objemom medzi 1 a3 km a 10 km; SSB 50 W cca 510 km, CW QRP cca 600 km, SSB 500 W cca 640 km (500 W v SSB robiť 40 km viac ako 5 W v CW); na 90 a99% času by sa malo odstrániť rozpätie 10 a20 dB; 5 a 10 dB za horizont na dlhých úsekoch; kvóta sa počíta na voľný horizont, nie na priblíženie spoločného objemu; Alpe Adria 2026: 99% QSO troposcatter, takmer žiadny vzduch napriek AirScout. N0

## 12. Okrúhly stôl: zdroje na overenie modelu alebo jeho opravu (21. novembra)

Zhrnutie všetkých otáčok zdrojov, objednané na použitie. "State" hovorí, či je zdroj už v programe (1.3.1), či je v teste alebo či sa má urobiť.

### Údaje na overenie (čo sa naozaj stalo)

| Zdroj | Čo dáva | Použitie | Stav |
| MMMonVHF správy od autora | 593 QSO pri 144 MHz, 29/12/2022, čas a lokalizátory | Potvrdenie udalosti oproti porovnaniu (+14 dní) | pri použití: AUC 0,79 (1,2) → 0,90 (1.3.1) |
| Svet nad 50 MHz | zdokumentované prípady (14 | historický prípad | používa sa ako historický prípad; pridať do overovacích QSO |
| wspr.live (WSPR) | kompletný archív, 2 m a 70 cm, SQL | mnoho podujatí a mnoho "nie" (hodiny bez miesta) | vyskúšaný, mal by byť filtrovaný (škvrna falošné na 16 a 20 tisíc km) |
| APRS PropView / APRS-IS | stanice plsť, vzdialenosti, hodiny | iba potvrdenie 144 MHz | robiť |
| Reverzný Beacon (archivný) | denné miesto | VHF | vyradené nateraz |
| I GRA2 radiosondaggi (Brindisi 16320, Prax Mare 16245, Trapani 16429, Udine, Miláno) | namerané profily 00 a 12 UTC | Ovládanie a inverzie profilu GFS | nájdené súbory (Brindisi 46 MB), ktoré sa majú použiť |
| Barsis a Kirby 1961, Barsis 1963 | evanjelium merané na cestách s horskými prekážkami | validovať kategóriu "monte" (Vesuvio, Faito) | robiť |
| Norton 1959, NBS TN 15, TN 101 | L(99) a straty v stolových médiách | Ovládanie scatter | čiastočne (pozri zhodných 300 míľ) |

### Opravy navrhnuté zdrojmi

| Oprava | Zdroje | Účinok meraný pri validácii | Stav |
| Nesprávna stabilita Termín | Bean a Dutton Cap. 6 | AUC 0,79 → 0,90 spolu s úbytkom | v 1.3.1 (hlava 16 dB bez vedenia) |
| Sklon od vertikálneho pohybu | Moler a Holden 1960 (v origináli 22/11: +2,5 dB priemer medzi zostupom vzduchu a stúpaním vzduchu, 1,4 a 9,4 GHz na mori) | Ako je uvedené vyššie. | v 1.3.1 (+1,5 / -2 dB) |
| Mäkký plynovod (λ až 3·λmax) | Kerr, Hepburn, VHF/UHF DX Book, Shi 2019 | sám +1% AUC; prináša farby potrubia na 46% ciest udalosti a 0% porovnania | : |
| Perzistencia 24 h | VHF/UHF DX Book, Biancomano a Leyshon | AUC nezmenené, viac prijímané a viac falošných poplachov | v 1.3.1 |
| Normálny profil CRPL s lokálnymi Ns (−ΔN = 7,32·e^ (0,005577·Ns)) namiesto 40 N/km pevné | Bean and Thayer 1959, NBS Monograph 4 | testované pri validácii, | v 1.3.2 ako alternatíva, vypnutý defektom |
| Super-refrakčná prahová hodnota 79 N/km (ITU) namiesto 100 (Bean a Dutton, vrstva 1 000 | P.453, Lopez 2008, DX Book | testované pri validácii, | v 1.3.2: agresívna úcta spolu s obozretnosťou (mapa používa obozretnosť) |
| Klimatická hrúbka (kalc B) menej dôležitá ako deficit | Rogers 1997 | vyskúšať: použiť A pre zisk | robiť |
| Parabolická rovnica v profile GFS | PETOOL, wave-propagation (Lytaev), Levy (kniha nečítaná, len náhľad), Lindquist | prvá verzia s poľom za horizontom, do ktorej dominuje numerický hluk (AUC 0.12.20.28): správna (doména 8 km, lúč 6°); po korekcii AUC 0.54.0,66 | používa sa ako druhý výpočet, nerozhoduje o prognóze |
| Evanscence: ±7 dB nad 100 míľ, rozpätie od 10 a 20 dB do 90 a 99% | Bray 1961, Antenna Book, GEKO 69 | prahové hodnoty v hraničných podmienkach, | pri použití: pravdepodobnosť pre obdobie a prah FT8 −18,6 dB v troposcatteri |
| Spoločný objem a ekvivalentná vzdialenosť Bray | IU3QEZ, Bray 1961 | Informácie | : |
| Troposcatter P.617 a RSGB ako neistota | TropoLink, DL2FZN | Informácie | v 1.3.0 |

### Čo zostalo mimo modelov

Vykonané na mori v nízkej nadmorskej výške (100 ITM/Longley-Rice zostáva štatistickou referenciou "normálny deň" (Ns 301, klimi) bez vedenia. Vyradené rozhodnutím autora: AirScout, P.528), E sporadické (P.2001) a 50 MHz (servoir EIRP, ktoré málokto má a mýli sa so sporadickým E).

### Návrh pracovného poriadku

1. Rozšírená validácia: pridať filtrované WSPR (párne hodiny bez spotu) a QST 2022; zopakovať pre 432 MHz.
2. Skúste normálny profil CRPL a prah ITU 79 N/km, jeden naraz, pričom len to, čo zvyšuje AUC.
3. Porovnanie GFS profilov s I GRA2 rádiovou vlnou Brindisi a praxe mora v dňoch QSO.
4. Prehodnotiť parabolickú rovnicu po numerickej korekcii.

### Rozhodnutia z 21. novembra (verzia 1.3.2)

- Prijaté: opatrný odhad (Bean and Dutton, 100 N/km) a agresívne (ITU, 79 N/km) na boku; kanály vyhľadávané len do 2500 m (prvá 1500 m, zvýšená po validácii); bonus s vysokým predĺženým tlakom a slabým vetrom; hmotnosť mora s μ1 P.452; podiel staníc, pokiaľ ide o potrubie aj na čiastočnom potrubí; hraničná vrstva nad hrebeňmi ( IT9GSF).
- Prijatý ako alternatíva, vypnutý defektom: normálny profil CRPL s miestnymi Ns.
- Správna parabolická rovnica numericky: AUC od 0,12 do 0,54 (30 m anténa) a od 0,28 do 0,63 (300 m).
- Pozemok: Copernicus DEM GLO-30 namiesto Terrain Tiles IC8TEM
- Meteorologické značky na mape s ich účinkom: ↓ priaznivé umiestnenie (Moler a Holden 1960, v priemere asi 2,5 dB medzi vzduchom zostupom a stúpaním vzduchu; +1,5 dB), ↑ vzduch do kopca nepriaznivá (hlboké vybavenie; −2 dB), S strike priaznivá stopa (ARRL príručka § 19.4, silné inverzie na Stredomorí; žiadna korekcia). Dvojitý symbol, zelená/červená/oranžové farby a komiks na mape.
- Prijaté ITM 1.2.2 (NTIA/itm, verejná doména), preložené do C#: podľa výpočtu základnej hodnoty v normálnej atmosfére vedľa P.452 a konečného úseku po výstupe z vedenia, ktoré končí pred korešpondentom. Vyradené qradiopredikt kód, pretože pod GPL v2.

Bez užitočného obsahu indexu: *Komunikácie Štvrťročne*, jeseň 1991 (žiadny tropo článok; Atkinsov článok o scatteri je v zime 1991).

- Prijatý ako princíp manuálov (november 21,): "šírenie nemožno zovšeobecňovať, ale študované vo vzťahu k miestnej morfológie" ( DF5AI, hlásené IK3XTV v "Propagation on 2 meters'); uvedené v predpovednej príručke (§ 1) a v radoch.
- Prijaté (21. novembra, po 1.3.3) interpretácia IK3XTV popoludňajších transalpínskych otvorov: nie "konvektívne bunky, ako sú šošovky," ale troposcatter posilnený turbulenciou až do hornej časti vrstvy reaktivovaná, keď dosiahne podiel spoločného objemu (manuálne predpovede, § 8 ter). Od 1.3.4 vstupuje do výpočtu bodového bodu ako predbežné posilnenie troposcattera nanajvýš 3 dB. Zneškodnené tými istými článkami: "automatické zdokonaľovanie" signálov za Alpami (pasívne médium nezosilňuje: opísané odkazy zostávajú vysvetľovateľné difrakciou a troposcatterom; deklarované schopnosti sa nepoužívajú ako dáta), sneh, ktorý "lepší difrakciu" (pravdepodobne stabilnú vrstvu nad snehom, s menšou evanscenciou).

- Kerr (1951) review on the book (22. novembra, PDF poskytol autor): § 1.5, eq. 17 a 18 a tabuľka 1.1. Čísla eq. 18 v snímke sú nečitateľné, ale tabuľka 1.1 potvrdzuje: 20 ft → 1 cm, 80 ft → 10 cm, 400 ft → 100 cm, 2000 ft → 1000 cm, proti 1,25, 10, 112 a 1252 cm λmax = 0,014·d^1,5 (λ v cm, d stojaci; okrúhly stôl, "indikatívne hodnoty." Kerrove hypotézy: vo vnútri vedenia je upravený index klesá 4,10 - 8 na stopu, typická hodnota nízkych kanálov; so slabšími gradientmi potrebuje väčšiu hrúbku. Kerr tiež varuje, že λmax nie je čistý rez: vlnové dĺžky niekedy dlhšie môžu byť stále silne ovplyvnené kanálom.

- DL6WU review on the original (22. novembra, PDF VHF Communications 3/1982 poskytol autor): G. Hoch, Obr. 1: prírastok na dipole meraný pri 432 a 1296 MHz v profesionálnom polygóne nemeckých Posts, so vzorkovou anténou EIA od 7,7 dBd, odhadovaná presnosť 0,5 dB; 2,35 dB pre dvojnásobok dĺžky boomu, žiadna saturácia až do približne 20 λ. Program použil 8.6 + 8.0.log10 (L/λ) dBd, t. j. približne 0,8 dB v menej ako 1 τ2 λ; teraz používa 9,4 + 7,8·log10 (L/λ) dBd (sklon je uvedený, hodnota 1 λ je uvedená v grafe, ± 0,3 dB). Odhodená formula 10· log10 (7, 8· l/ λ) rozšírená na internete: by 3 dB na zdvojnásobenie, oproti 2, 35 meraným.
- Booker a Gordon (1950): referencie potvrdené "This Week's Citation Classic" (Current Contents, 8 February 1982, commentary by H. G. Booker): Proc. IRE 38, pp. 4011412. Táto pripomienka potvrdzuje dva body použité v príručkách: pole prijaté za horizontom s rozdelením Rayleigh a exponenciálnu funkciu autokorelácie nezrovnalostí (negausskej). Pôvodný článok nebol prečítaný.

## 13. Z IK3XTV článkov

Dva články od Flavio Egano IK3XTV (Radio Rivista: "Propagácia na 2 metre" a "Propagácia VHF Sú to pozorovania zo stanice, nie opatrenia: musia byť považované za hypotézy na kontrolu s škvrnami.

| Pozorovanie autora | Ako sa stane súčasťou indexu |
| Kanály sa tvoria najmä medzi 100 a 1000 m, zriedka vyššie; slúžia vysoký tlak na širokej ploche, dobrá insolácia a absencia vetra | hľadať kanál len pod 1500 m; bonus s vysokým predĺženým tlakom a slabým vetrom v nízkych vrstvách |
| V teplých mesiacoch vykonáva opakujúce sa na Jadrane medzi Veneto a Puglia, 700 a 800 km, niekedy až do Grécka; morské trasy sú obľúbené | väčšia hmotnosť vzhľadom na zlomok morskej dráhy (napr. μ1 ITU-R P.452) |
| Zodpovedajúce musia byť v alebo v blízkosti vedenia; s vysokými anténami a niekoľko metrov vedie signál nevstupuje | porovnať podiel základne vedenia s podielom dvoch staníc (bude slúžiť nadmorskej výške QTH) |
| Rovnaký čas, rovnaké vedenie: počúvať zo stanice Gargano v 144 a jeden z 50 MHz | skontrolovať: podľa Hepburn tabuľky 50 MHz požiadať o viac ako 300 m vedenia, takže udalosť by naznačovať potrubia veľmi často |
| Connections Veneto | "konvekčný" index oddelený od vedenia pre horské trasy |
| Konvektívne bunky sa rodia na zemi a stúpajú okolo 1 km každé 3 hodiny: pri 15 dosahujú asi 3 km, nadmorská výška, ktorá slúži na prepísanie Álp | použiť limitnú výšku vrstvy (hranica\_vrstva\_výška Open-Meteo GFS) pozdĺž cesty: skóre, ak presahuje výšku hrebeňov |
| Alpy vytvárajú gravitačné vlny, ktoré spolu s konvekčnými pohybmi vytvárajú nezrovnalosti indexu lomu | hypotézy: silný vietor kolmý na reťaz na 700 hPa ako signál zlatých vĺn; vyskúšať posledný |
| Veľmi vysoká inverzia (do 8 km vo výnimočných prípadoch) dáva asi 800 km, ale v praxi zriedka viac ako 300 km | podiel inverzie rozširuje rozsah: užitočné údaje na odhad čakacej vzdialenosti |
| V lete polomer Zeme zodpovedajúci približne 16000 km (2,5-krát reálna), s asi 60 dB za menej ako 300 km straty | v súlade s k50 = 157/(157 - ΔN): k = 2,5 zodpovedá ΔN približne 94 N/km, čo je super-refrakcia |
| Viac ako 3000 km len v rozsahu 20 a30° severne a južne od rovníka nad morom, spojené s Hadleyho bunkami; vrchol v auguste v Afrike | mimo Európy: užitočné nedávať vysoké indexy na Atlantiku bez spätnej väzby a pre Kanársku trasu |

Ďalšie užitočné údaje z tých istých článkov:

- Optický rozsah medzi dvoma h1 a h2 metrov (d v km): d = 4,1 · (√h1 + √h2), až do výšky približne 20% pre normálnu lomu.
- Evanscencia: pomalá a plytká s inverziou; silná a rýchla s rozptylovými a konvekčnými bunkami. Program by ho mohol použiť na klasifikáciu reklám, ak jedného dňa bude mať správy SNR v priebehu času.
- Pre sporadické E v 144 MHz autor podáva správu o práci DF5AI a Davis a Johnson (Príroda 435, 2005) na putách medzi časovými intervalmi a Es: z tropa, ale samostatný indikátor zajtra mohol použiť bleskové dáta.
- Pozemný odraz v dvojitom skoku Es: more K = 81 a 5 S/m, krajina a les K = 13 a 0,006 S/m, mestské oblasti K = 3 a 0,001 S/m.

## 14. LIVETROPOVHF indexové hypotézy

Počiatočný návrh, ktorý sa má prediskutovať: index 0 a 10 na rovnakej stupnici ako Hepburn, vypočítaný pre každý bod siete a každé 3 hodiny od GFS (potom ECMWF a ICON na porovnanie), a druhé číslo na trasu, ktorá hovorí, koľko časť trasy prechádza do užitočného kanála do zvoleného pásma. Závažia nižšie sú moje predpoklady, ktoré majú byť kalibrované s škvrnami.

Recenzia po Bean a Dutton a Kerr: profilové prahy pochádzajú z ich tabuľky 6.5 a mierka z ich meraní, takže refrakčná vrstva má hodnotu 10 a 25 dB nad normálny profil. Index sa potom vypočíta ako prvý v dB a potom preberie hodnotu 0

Výpočet podľa bodu, krok za krokom

1. T výboj, relatívna vlhkosť, geopotenciálna výška a vertikálna rýchlosť pri 1000, 975, 950, 925, 900, 850, 800, 700, 600 a 500 hPa, plus T a 2 m vlhkosť, zemný tlak, výška vrstvy a vietor pri 10 m.
2. Zlikvidujte úrovne pod zemou; výpočet N a M na každej úrovni (ITU-R P.453).
3. Výpočet −dN/dh v každej zo štyroch vrstiev Bean a Dutton (1000
4. kde dM/dh < 0 nájdem základňu a vrchol vedenia, hrúbku d a deficit ΔM.
5. Porovnanie s minimálnou hrúbkou pásma podľa vzorca Kerr (asi 180 m pre 144 MHz, 89 m pre 432 MHz, ako v tabuľke Hepburn); pod prahovou hodnotou sa spojnica stále počíta, ale menej.
6. Stimulujem zisk v dB s nižšie uvedenou tabuľkou (beriete najlepšiu vrstvu, nie súčet vrstiev, plus termín anomálie) a konvertuje ju: index = dB / 2.5, medzi 0 a 10, takže 25 dB je v hodnote 10. Negatívna hodnota (sub-refrakcia) je zobrazená ako 0, ale zostáva v dátach trasy.

| Najlepšia situácia vo vrstve | Kritériá | Odhadované príjmy |
| Normálne (nelaminované) | 6,5 (napr. 20 a 60 N/km medzi 1000 a 850 hPa) | 0 |
| Mierne nad normál | medzi hornou hranicou normálneho pásma a superrefrakčnou prahovou hodnotou | 0 až 10 dB, v pomere |
| Super-refrakcia | za prahom vrstvy (100, 80, 70, 50 N/km stúpanie), ale pod 157 N/km | od 10 do 15 dB, v pomere do 157 N/km |
| Príliš tenké potrubie pre pásmo | dM/dh < 0, hrúbka pod Kerrov prah | 15 |
| Užitočný konektor pre pásmo | dM/dh < 0, hrúbka nad prahom Kerr | 20 dB, +1 dB každých 5 M jednotiek deficitu, maximálne 25 |
| Subfrakcia | -dN/dh < 0 v spodnej vrstve | −10 dB |
| Anomálne pozadie (pridávate) | Model Ns mínus ITU klimatologické mapy Ns za ten mesiac | 0,2 dB na jednotku N, medzi -5 a +5 dB |

Výpočet trasou (medzi dvoma lokátormi): odoberte maximálny kruh každých 10 km, ako aj program indexu na dráhe a vypočítajte zlomok bodov s užitočným kanálom a priemerným podielom základne. Štúdia CSIRO považovala za užitočnú časť trasy v potrubí (na trasách AIS pri 162 MHz bola o 67% vyššia ako v kontrolných prípadoch).

Pridanie výpočtu na trasu (z LA0BY a W3SZ): (1) morské stanice v porovnaní so základnou a hornou spojkou, pretože vysoká spojka slúži len tým, ktorí vstúpia; (2) "podlaha" troposcattera s Yeho metódou, so štartovými rohmi vytiahnutými zo zemského profilu; z tejto straty sa odstráni zisk v dB a výsledok sa porovná so stratou, ktorú stanica dokáže prekonať (tabuľka KK6MC). Index sa tak stáva aj odhadom dosiahnuteľnej vzdialenosti.

Opravy vyskúšať jeden naraz (z Bean a Dutton a LA0BY, IK3XTV, G4MCU už v manuáli), aby len v prípade, že zlepšenie porovnania s bodmi:

- Miešanie (dôležité podľa autora, podľa denného pozorovania): zo dňa na deň na zemi, od polovice rána do popoludnia s jasnou oblohou alebo s vetrom do zeme cez niekoľko m/s, kanály k zemi zmiznú; ich príspevok sa musí znížiť. Nie na mori.
- Čas dňa (dôležité podľa autora, denné pozorovanie): irraggiamento kanály vznikajú pri západe a páde po východe slnka; je to 3-hodinová kontrola predpovede.
- Vertikálny pohyb: vzduch stúpa na 850 hPa remixuje vrstvy (negatívna korekcia), vzduch klesá pod anticyklónom ich posilňuje (pozitívne).
- Tlakový trend: tlak na ceste v posledných hodinách, vyšší a stabilný signál; pokles, nižší signál (bastano 2 hPa).
- Synonymum: vysoký tlak nad 1025 hPa, teplota nad 850 hPa, horúci a suchý vzduch tečúci na chladnom mori, cesta na mori.
- Horské chodníky: druhý "konvektívny" index (oddiel IK3XTV), s popoludňajšími limitnými vrstvami vyššími ako hrebene pozdĺž cesty a vysokým tlakom.

Z VHF/UHF DX Kniha, ďalšie opravy vyskúšať: po sebe idúce dni vysokého tlaku v bode (viac dní, viac bodov), poloha v porovnaní so stredom anticyklopánu (bonus na okrajoch), tlakový gradient na zemi ako meranie vetra, silný dážď 24 hodín pred (poškodenie), nízke oblaky na okraji vrstvy (poškodenie len pre vysoké pásma). Pre smer trás: porovnajte azimut trasy s izobarom.

Overovanie

- LIVETROPOVHF archív: každá reklama cez 500 km až 144 alebo 432 MHz je "udalosť." Porovnáva index na trase s frekvenciou udalostí v rovnakom čase dňa a mesiaca (rovnaký prístup štúdie CSIRO).
- Porovnanie s Hepburn a F5LEN v rovnaký deň: kde tri indexy odpojiť, vyzeráte, kto mal pravdu podľa bodov.
- Radiosundations of 00 and 12 UTC: na kontrolu, či model vidí inverzie, ktoré sonda meria.
- Historické prípady LA0BY (611/11/2003, 10/12/2004, 15/10/2011, 11/2/2015, 22/11/2018): index prepočítaný v týchto dňoch musí vidieť kanály, ktoré Hepburn neukázal.
- Limity, ktoré majú byť deklarované v príručke: vykonáva sa pod 100 a 200 m takmer neviditeľné úrovne tlaku GFS; spoľahlivá prognóza na 2 a 3 dni, potom len trend.

## 15. Zdroje z 21. novembra (druhá časť)

- ITU-R P.1812 (MaTLAB/Octave implementácia I. Stevanovic, OFCOM; GitHub eeveetza/p1812, Zenodo 10.5281/zenodo.6141558). VHF/UHF špecifická metóda trasy odvodená z P.452: difrakcia "delta-Bullington," troposcatter, kanály a odraz na vrstvách, zaťaženie na zemi. Užitočné ako tretie porovnanie vedľa P.452 vo výpočte bodového bodu; ΔN a N0 údaje sú úplné produkty ITU, takže si ich užívateľ stiahne (podľa P.453).
- z Costa, Rabanal Ramirez, Dias, IET MAP 2018 (doi 10.1049/iet-map.2017.0992). 563 MHz, 27 bodov medzi 5 a 30 km na nepravidelnom teréne s lesom: P.526 s 12 m vegetácie −4,3 dB a σ 8,8 dB; P.1812 s 10 m zaťaženosti +2,6 dB a σ 8,9 dB; dielektrický klin −4,8 dB a σ 9,2 dB; P.1546 +5,0 dB a σ 11,7 dB. Pre program: Zostatková chyba dobrej difrakcie metóda je asi 9 dB, a výška zaťaženosti na hrebeniach (10-12 m) počíta.
- PETOOL (Ozgun e altri, Počítačová fyzika Communications 182, 2011). Parabolická rovnica deliaca krok na jednu a dve cesty so širokým uhlom propagátora (uhol až 40 Potvrdzuje výber 1.3.2 (široký uhol, pôdne koeficienty) a označuje obojstranný výpočet pre strmé reliéfy.
- MathWorks. `tropopl`: strata plynov a strata Príklad radarového šírenia používa pre refrakciu −ΔN = 7,32·e^ (0,005577·Ns), rovnaký CRPL správa už v programe. `propagationModel` Longley-Rice: preddefinované horizontálne polarizačné hodnoty, vodivosť 0.005 S/m, povoľnosť 15, reaktivita 301, mierne kontinentálne podnebie. Žiadny model MathWorks sa nezaoberá kanálmi.
- ITU-R digitálne výrobky skupiny 3. Údaje a programy P.453, P.452, P.1812, P.2001, P.528, P.1546; "integrované" údaje sú súčasťou odporúčaní; žiadna deklarovaná licencia na redistribúciu: naďalej ich necháte stiahnuť užívateľovi.
- ESA Navipedia, troposferické oneskorenie. N = Nidrostatica + Numida; suché zenitálne oneskorenie je asi 2,3 m a vlhké jeden desiatky cm, ktoré sa rýchlo líšia. Pre nás: mokré oneskorenie odhadované stanicami GNSS je nepriamym meradlom integrovanej vlhkosti, možným budúcim zdrojom teraz odtoku; Dnes nie je použiteľný.
- Bray, K2LMG, Autor, D. W. Bray (General Electric, Ithaca), hovorí, že preveril predpovede s mesiacmi plánovaných spojení s K2GOI a W4LTU. Evanscencia v čase 13,5 dB nad asi 100 míľ (7 dB pod a 7 nad hodinovým priemerom), takmer lineárna pod (3,5 dB až 50 míľ): pre pevné pripojenie odstrániť 7 dB. Oprava horizontu: každá výška horizontu má hodnotu 69 míľ (111 km) ďalšej vzdialenosti (menej, ak je záporná). Zisk z jagi asi 10 × boom/λ (pomer výkonu), viac ako 4 dB odrazu pôdy; SSB −3 dB a AM −7 dB v porovnaní s CW; 99% hodín: asi 180 dB až 60 míľ, 190 až 90, 200 až 240 (2 m). Z 1.3.2 pravdepodobnosť pre obdobie používa σ = 3,4 dB (±7 dB ako ±2σ) namiesto 5 dB odhadovanej druhej ruky.
- Obojsmerný výpočet (skúste s prototypom, 21. novembra). Veľkoplošná parabolická rovnica so skutočným terénom S RTM1 z Capri, 144 MHz, anténou 10 m od zeme, štandardnou atmosférou; meranie: priemerný výkon od 20 do 400 m do 80 km na trati smerom k korešpondujúcej. Z centra Capri a Marina Grande západ je uzavretý (prekážka na +11 Upozorním na Faito (takmer za EA5 a EA6, 150 Z Anacapri západ je zadarmo a priame výhry o 8 a 22 dB. Absolútne straty na 1000-1300 km prototypu však nie sú spoľahlivé. EA3 EA5 EA6
- Barsis a Kirby, NBS Report 6751 (1961), Pikes Peak, 223 km, 100 a 751 MHz. Pohorie je spoločným terminálom dvoch trás v optike: rozdelenie hodinových mediánov je konvolúcia týchto dvoch trás. Dlhodobá exkurzia (1 - 99%) približne 14 dB, proti 26 - 40 dB dráhy troposcatter rovnakej dĺžky alebo uhla; žiadna zmena zreteľného dňa. Signál je silnejší ako troposcatter, ale slabší ako ideálny okraj: s vrchom by sa malo zaobchádzať ako so zaobleným (približne 11,5 km). Za dorzálmi sa vždy pozorujú vysoké signály.
- Barsis, správa NBS 7652 (1963), tri trasy cez horu (Colorado, Aljaška, Európa), 750 Priemerné straty 10 a 20 dB vyššie ako Fresnel-Kirchhoff výpočet bez pozemku v blízkosti terminálov; evanjelizácia sa odhaduje zabalením dvoch liniek v optike; odrazy pôdy v blízkosti terminálov zhoršuje dlhú vyblednutie. Od 1.3.2 vstup do vedenia pridáva zaokrúhľovanie P.526.
- Barsis a Johnson, NBS TN 88 (1961). Predĺžené rozpustenie vesmírnej vlny, dokonca aj na ceste za horou (§ 5).
- Norton, Krivky, z ktorých Bray dostane nomogramy; definície straty systému, prenos a základné, proces antény zisk.
- Ryža, Longley, Norton a Barsis, NBS TN 101 (1965 - 67), zväzky 1 a 2. jedna hrana a prekážka zaoblené s odrazmi a bez nich (§ 7, s príkladom Pikes Peak), dlhodobá variabilita V(0,5) a Y(q) pre podnebie (časové konkontinentálne a iné). Pre nás: metóda zaoblených prekážok a funkcie variability sú odkazom na kalibráciu neistoty.
- ITU-R P.526-16 (11/2025). Vzorec (31) J(ν), ktorý je identický s normou programu; zaoblená prekážka (32) (36), teraz používaná; multiplikačné metódy (Bullington a úplná metóda § 4.5).
- ITU-R P.1812-8 (2025) a -7. Kompletný text obnovený: špecifická metóda 30 MHz až 6 GHz (delta-Bullington, troposcatter, kanály a odraz na vrstvách, zaťaženosť). -7 varuje, že jeho troposcatter (z P.617-5) sa mýli pod 66 km. Je to aj naďalej možné tretie porovnanie.
- Paulus, Správa z 22/11/2026 (DTIC ADA146528, sken s rozpoznaným textom, niekedy nečitateľný). Vzduchové rozdiely merané loďami sú posunuté smerom k stabilite: teplejší morský vzduch v roku 31 Model Jeske potom dáva 40 m a viac ako 15% roka a výšky, ktoré skáču z 10 na 40 m za 24 hodín so stabilným časom, nie je potvrdené rádiom. Navrhovaná korekcia (testované prahové hodnoty 0, −0,5 a −1 °C; −1 °C je lepšie odsúhlasená s rádiovými opatreniami z júna 1982): ak je rozdiel v ovzduší −1 °C alebo menej, použije sa nameraná hodnota; inak vypočítate výšku s −1 °C a s 0°C a ak je táto s −1 °C nižšia, podržíte ju. Zvýšenie na stabilnej strane sú znížené, zníženie zostáva. V júni Marsden 120 štvorcov: priemerná výška od 16,8 m (klimatológia) do 7,6 m. Rovnice modelu nie sú v správe (pozri Jeske a Hitney). Program používa neutrálny profil, ktorý nemá stabilné preháňanie, a limity δ až 40 m: ide rovnakým smerom; s modelmi SST (nie loď) meranie chyba, ktorú Paulus opravuje váži menej.
- Levy, Na Google Libri je len náhľad: Nečítam.
- VK3FS (3fs.net.au). Kvalitné zavedenie: vysokokanálové kanály na 450 a 2000 m pre dlhšie otvory na zemi; inverzie 300 a 600 m v pokojnej a pokojnej noci, ktoré zmiznú po východe slnka; silnejšie pobrežné inverzie v popoludňajších hodinách; 800 a 1300 km typické, 1400 a 1600 km často vykonáva. V súlade s dennými/nocovými upozorneniami a stropom 2500 m.

## 16. Zdroje

Pravidlá a vzorce

- [ITU-R P.453-13 (2017), rádiový index lomu:](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)vzorec a údaje
- [ITU-R P.452-18 (2023), predpoveď rušenia medzi pozemnými stanicami nad 100 MHz: ekvivalentný polomer, β0, troposcatter, kanály](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)
- [ITU-R P.617-5, šírenie za horizontom pre rádiové mosty](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)
- [Shi et al., "Komplexná štúdia o maximálnej dĺžke trvania elektromagnetickej proliferácie v rôznych odparovacích duktoch," IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

Štúdie o predpovedaní kanálov s modelmi počasia

- [P. Lopez, "A 5-year 40-km rozlíšenie globálnej klimatológie super-refrakcie pre pozemné meteorologické radary," ECMWF Tech. Memo 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO), "Tropospheric Ducting Prediktion based on GFS Model Data at... the CSIRO Murchison Radio-astronomy," Observatory arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (FMI), "Efekty rozlíšenia modelu na predpovedanie podmienok atmosférického kanálovania s Harmonie-AROME," EPUsphere 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

Amatérske rádiové predpovede

- [W. R. Hepburn, "The Hepburn Tropo Index (HTI) "](https://www.dxinfocentre.com/propagation/hti.htm)a [](https://www.dxinfocentre.com/propagation/history.htm)História metódy
- [W. R. Hepburn, "Lowest Usable Frequency verzus Thickness of Dutting Layer"](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, "Worldwide refractive index prognózy"](https://tropo.f5len.org/WW/info.php), [nová verzia 2026](https://tropo.f5len.org/?p=504), [poznámka k modelu ARPEGE](https://tropo.f5len.org/author/f5len/)

Údaje

- [Open-Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api), [poznámka k úrovniam tlaku](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)
- [NOAA TEI, Integrated Global Radiosonde Archive (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RAOBget, rádiový povodňový scenár pre univerzitu vo Wyomingu](https://github.com/NCAR/RAOBget)

Klasické texty (uvedené autorom programu, ktoré sa majú konzultovať pre konečné vzorce)

- B. R. Bean, E. J. Dutton, *Radio Meteorology*, NBS Monograph 92, 1966, 435 strán (reprint Dover 1968): vertikálne profily N a M, gradient pasce pod −157 N/km, porovnanie s rádiovou vlnou; kapitola 5 "Synoptická rádiometeorológia." Základ pre 2 a 4 kroky výpočtu za bod. Bez cla: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [NIST card, doi 10.6028/NBS.MONO.92](https://www.nist.gov/node/1770911), [Internet Archive](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf)copy.
- L. Boithias, *Radio Wave Propagationon*, McGraw-Hill 1987 (preložený D. Beesonom z francúzštiny, 330 strán, ISBN 0070064334; tiež North Oxford Academic): mechanizmy za horizontom, spoločný objem rozptýlenia a straty spojovacej antény polovica. Samotný papier ([knižničná karta](https://libcat.library.tamu.edu/Record/in00000014812/Description)).
- M. P. M. Hall, *Vplyv troposféry na rádiovú komunikáciu*, IEE Electromagnetic Waves Series n. 8, Peter Peregrinus 1979, 206 strán (ISBN 090604082557; brossura 0863410863): synoptické situácie a šírenie VHF/UHF. Základňa pre Synoptické zložky. Obchod na IET ([](https://libcat.library.tamu.edu/Record/in00000636385)karta).
- H. G. Booker, W. E. Gordon, "A Theory of Radio Scattering in the Troposféry," *Proceedings of the IRE*, vol. 38, n. 4, April 1950, pp. 4011412: Skenovanie teórie z dezogenity indexu lomu. Centrálna myšlienka: rozptýlený výkon závisí od intenzity a rozsahu turbulentných výkyvov indexu lomu (exponenciálna korelácia); v blízkosti horizontu prevláda difrakcia, ďalšie rozptyl; Podľa autorov rozsah turbulencií a variácie indexu očakávané meteorológiou sú dostatočné na vysvetlenie nameraných polí. Citovaný 328 článkov (Scilit), žiadny voľný prístup kópiu. Karta na Semantic Scholar ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); celý text na IEEE Xplore zaplatené ([doi 10.1109/JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)).
- C. M. Crain, J. R. Gerhardt (Laboratórium výskumu elektrotechniky, Texaská univerzita), "Measurements of the Parametrics Involved in the Theory of Radio Scattering in the Troposphere," *Proceedings of the IRE*, vol. 40, n. 1, January 1952, pp. 50](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)
- "Konštanty v rovnici pre atmosférický refrakčný index pri rádiových frekvenciách," *Proceedings of the IRE*, vol. 41, n. 8, august 1953, pp. 1035 N; odtiaľ sú hodnoty 77,6 a 3,73 · 10^5 vzorca ITU (Q](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E. C. S. Megaw, "Fundamentálna teória šírenia rádioaktívneho rozptylu," *Proceedings of the IEE Part C: Monographs*, vol. 104, n. 6, september 1957, pp. 441](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)
- *Proceedings of the IRE*, Október 1955, špeciálne číslo na šírenie rozptylu (podľa K. A. Norton a J. B. Wiesner), s Bullington, Staras a ďalšie: zadarmo vo PDF na [World Radio History](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf).
- D. E. Kerr (edited by), *Propagation of Short Radio Waves*, MIT Radiation Laboratory Series vol. 13, McGraw-Hill 1951 (reports Dover 1965 and IEE 1987): teória odparovania a povrchových kanálov na mori. Digitálna kópia: [World Radio History](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); IET vydanie v [IET Digital Library](https://shop.theiet.org/propag-short-radio-waves).

Nedávne opatrenia troposferickej difúzie vo VHF: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Austrália), "A Study of VHF Troposcatter Communication Channels in South Australia," in *Digital Signal Processing for Communication Systems*, Springer 1997, s. 103 Text za poplatok; od abstraktu sa týka šírenia, nie vedenia. Užitočné len ak chcete kalibrovať termín troposcatter s VHF dátami kanála.](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)

Numerické modely a námorné trasy (indikované autorom programu)

- Pán Levy, *Parabolické rovnice metód pre elektromagnetické Wave Propagation*, IEE Elektromagnetické vlny Series #45, 2000, 336 strán (ISBN 0852967640): parabolická rovnica pre refrakčné profily, ktoré sa menia pozdĺž cesty a pre orografiu; kapitoly o tropoférnom šírení, lúčoch a spôsoboch, šírenie na mori. Poslúžil by na výpočet poľa pozdĺž cesty za indexom. Pre platbu aj v elektronickej forme od IET ([IET card](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)).
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (Leicesterská univerzita): Meranie 2 GHz na Normanských ostrovoch a Lamanšskom prielive. Mnoho prác a práce sú zadarmo v univerzitnom archíve: [dlhodobé štatistiky o odparovacie potrubie, Radio Science 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434), [zmeny signálu na troch námorných trasách, Radio Science 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710), [Hlavy N. Mufti 2012 s trasami 50 a 140 km aj vo VHF](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854), [tesis of I. Alam 2002 o reaktivite vypočítaná z údajov o počasí, 240 MHz a 2 GHz X3 UQ, [tesi.](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus, "Praktické použitie modelu odparovania," Radio Science vol. 20 n. 4, 1985, s. 887 (IEEE Transactions on Antenas and Propagation), doi 10.1029/RS020i004p00887 ([scheda](https://zendy.io/title/10.1029/rs020i004p00887)): výška odparovacieho kanála z údajov o povrchovom počasí. Osloboďte technickú správu, ktorá jej predchádza: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf).

T. Lindquist, *Wave Propagation Models in the Troposféry for Long-Range UHF/SHF Radio Connections*, magister's disis, University of Karlstad, 2. júna 2020, 106 strán (dodáva autor programu). Je to krok za krokom sprievodca, voľný, napísať výpočet s parabolickou rovnicou: riešenie delenia (aj v širokom uhle), hraničné podmienky pre pevninu a more, krok po zemi, sledovanie lúča na lineárnych profiloch v čase, s numerickou kontrolou; uvádza nástroj bez PETOOL pre MATLAB. Dva užitočné údaje naraz: (1) N citlivosť na počasie premenné, δN Autorský záver (frekvencie 1 až 10 GHz): za horizontom spoľahlivé spojenia takmer vždy vyžadujú potrubie.

Propagačný softvér na pôdnom profile (pre horizont a prekážky, nie pre očakávané tropo)

- [Radio Mobile](https://ve2dbe.com/rme.html)podľa Roger Coudé VE2DBE: zadarmo, Windows (tiež Linux a Mac s vínom), Longley-Rice/ITM model, terén profil automaticky extrahovaný z kvótových dát ([download](https://ve2dbe.com/download/download.html).).
- [SPEL!](https://qsl.net/kd2bd/splat.html)by John Magliacane KD2BD: GPL, 20 MHz20 GHz, Longley-Rice a ITWOM v3.0 modely, správy o prekážkach a strate v texte; najnovšia verzia 1.4.2 z roku 2014.
- [ITM, NTIA/ITS](https://its.ntia.gov/software/itm)oficiálny kód: Longley-Rice model (20 MHz a 20 GHz) so zdrojom a dokumentáciou, základňa Radio Mobile a SPLAT!. Kód je voľný a môžete pridať k výpočtu na ceste LIVETROPOVHF (strata spôsobená pôdou, ktorá sa pridá do indexu tropo). Pôvodná správa: A. G. Longley, P. L. Rice, "Predpoveď straty troposférického rádiového prenosu cez nepravidelnú terénu: Počítačová metóda https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITU-R, softvér a digitálne údaje](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): bezplatné mapy ročných mediánov ΔN a N0 na mriežke 1,5°, ktoré používajú P.452 a P.617
- Strata trasy (zmluvné telekomunikačné inžinierstvo): komerčný program pre rádiové mosty; Nenašiel som verejný zdroj, ktorý potvrdzuje, ktoré ITU odporúčania vykonáva, tak skontrolovať.

Znovu použiteľný GitHub kód v LIVETROPOVHF

| Projekt | Čo robí | Jazyk | Použiť pre nás |
| [NTIA/ITM](https://github.com/NTIA/itm) | model Longley-Rice úradník NTIA, s balíkmi pre. NET |  C++, C#  | strata pôdy na ceste: integruje priamo do programu, ktorý je v C# |
| [fuzzzee/itm](https://github.com/fuzzzeee/itm) | ITM C++ kód C# preklad |  C#  | alternatíva bez natívne knižnice, tiež užitočné pre verziu Windows 7 |
| [eevetza/Py452](https://github.com/eeveetza/py452) | ITU-R P.452, identické s MATLAB/Octave verzia schválená ITU-R WP 3M; MATLAB verzia uverejnená švajčiarskym Federálnym komunikačným úradom | Python MATLAB | odkaz na preklad C# vzorce troposcatter a vykonané, s príkladmi ITU validácie |
| [bwinkel/pycraf](https://github.com/bwinkel/pycraf) | P.452 kompletné, P.676, SRTM profily; GPL v3, ale zahrnuté dátové súbory ITU nie sú zadarmo pre komerčné použitie | ♪ | kontrola našich výsledkov |
| [ITU-Rpy](https://github.com/inigodelportillo/ITU-Rpy) | Mnohé odporúčania ITU-R vrátane P.453-13 potvrdené príkladmi ITU | ♪ | overovanie N vzorca a klimatických máp P.453 |
| [NCAR/RAOBget](https://github.com/NCAR/RAOBget) | stiahnuť rádiovú vlnu Wyomingskej univerzity v textovom formáte | ♪ | diagram na stiahnutie v C# profily talianskych staníc |

Poznámka: aktuálna verzia P.453 je [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)]

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), k stiahnutiu zdarma; v dokumente som použil 13, aby sa preložil.

Už v programových manuáloch a znovu použiteľné tu: LA0BY (Nordic VUSHF 2019), IK3XTV ("Propagácia na 2 metre" a "Propagácia VHF DF5AI/DK5YA G3YGF G4MCU KP4MD
