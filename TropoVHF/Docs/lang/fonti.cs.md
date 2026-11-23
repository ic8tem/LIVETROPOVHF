# Zdroje a vzorce

> Strojový překlad z italštiny: v případě pochybností platí italská příručka.

Pracovní sbírka zdrojů konzultovaných pro předpověď tropo LIVETROPOVHF (20- 21. listopadu 2026): Základní fyzika, dostupná data, publikované indexy, vzorce, amatérské zkušenosti v rádiu, validace a rozhodnutí. Vzorce jsou číslovány jako v LaTeX, (1), (2),..., a v textu připomínají číslo v závorkách. Předpověď popisuje, jak je program používá.


## 1. Prostory autora

IC8TEM pozorování, které rám práce, se zpětnou vazbou nalezenou ve zdrojích.

- Italská geografie vyžaduje rozptýlení. S dorsal, promontory a ostrovy několik desítek kilometrů od stanic, difrakční příspěvky se počítají s parametrem Fresnel na hlavní hraně, a to nejen s hladkou půdou. Setkání: v porovnání s nepravidelným terénem na 563 MHz (Rio de Janeiro, 27 bodů mezi 5 a 30 km), nejlepší metody byly P.526 s ostří, P.1812 a difrakční klín, s průměrnou chybou mezi − 5 a + 3 dB a rozptyl asi 9 dB; P.1546 byl nejvíce pesimista (z Costa, Rabanal Ramirez a Pro strmé svahy v blízkosti stanice PETOOL pamatovat, že slouží široké úhlové propagátory a dvousměrný výpočet (vlna odráží reliéfy).
- Abyste se dostali do vzdáleného vedení, potřebujete Fresnela. Pokud vedení začíná za horizontem, signál k nám přichází difrakcí přes překážky až do jeho začátku; od 1.3.2 program počítá (Prevision Manual, § 10 ter). Setkání: Rohde & Schwarz (2023) poznamenává, že stanice podél vysokého kanálu často selhávají při vstupu, zatímco stanice blízko konce ano.
- Podmínky se rychle mění. Teorie jsou tam, ale podmínky se mění v minutách, a žádný systém předpovídání bere data v reálném čase: GFS vyjde každých 6 hodin, s časovým rozpětí a 0,25 °, a předpověď může ztratit hodnotu v krátkém čase. Schůze: Bray (QST, listopad 1961) měření hodinových mediánů kolísání přibližně ± 7 dB nad 100 mil; kanály se pohybují s atmosférickým časem (Rohde & Schwarz); náš QSO registr 29 / 12 / 2021-2 / 1 / 2022 ukazuje, že otvory rostou a padají během hodin.
- Nejlepší nyní-casting zůstává DX cluster- PSKReporter síť. Chcete-li vědět, co se stane teď, cluster skvrny, PSKReporter (a WSPR) porazit jakýkoli vzor: jsou opatření. Předpověď říká, kde hledat a jak dlouho to může trvat; reklamy říkají, jestli tam je. Program pro to klade místo a předpověď na stejné mapě.

## 2. Účel

Tato myšlenka je třetím indexem, který počítá samotný LIVETROPOVHF od vertikálních profilů modelu počasí, který má být vedle Hepburnu a F5LEN namísto jejich nahrazení. Dnes program čte své mapy již připravené: index by také řekl * proč * tam je otevření (kvóta a tloušťka potrubí, minimální pásmo v pasti) a to by mohlo nastat s místy, které program sbírá již.

- Co můžete dát: pravděpodobnost vedení za bod a hodinu, podíl základny a vrcholu, tloušťka, minimální frekvence zachycena, a podél cesty zlomek trasy uvnitř vedení.
- Co nemůže dát: tenké kanály (pod pár desítek metrů), které globální modely při 0,25 ° neřeší, a odpařovací kanály na moři, které slouží především od GHz nahoru.
- První praktický cíl: 144 a 432 MHz v Evropě, s ověřením na DX cluster skvrny, PSKReporter a místní dekódování.

## 3. Základní fyzika

Všechno začíná refrakční. N vypočteno na každé úrovni profilu: potrubí je, kde modifikovaná reaktivita M snižuje lezení, to je místo, kde dN / dh klesá pod − 157 N / km ([ITU- R P.453-13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)).

Refraktivita (P a hPa, T kelvin):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Tlak páry z relativní vlhkosti H (%) a teploty t (° C) nad vodou mezi − 40 a + 50 ° C:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

Upravená reaktivita (h v km, nebo 0,157 · z s z v metrech) a kritérium potrubí:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN / dh (N / km) | Nařízení | Účinek 144 MHz |
| 0 | sub- lom | snížený průtok |
| od 0 do − 79 | normální (standard asi − 40) | rádiový horizont s rovnocenným poloměrem 4 / 3 |
| od − 79 do − 157 | superrefrakce | Prodloužený tok, "příliš dobrý" |
| nižší než − 15 | vedení (zachycování) | zachycené signály, DX přes 1000 km |

Jak extrahovat N z modelu při tlakových hladinách: pro každou úroveň si vezmete T, relativní vlhkost a geopotenciální výšku, spočítáte N a M, pak gradient mezi sousedními úrovněmi. Hladiny pod zemí je třeba zlikvidovat (1000 hPa na Alpách je podzemní).

Minimální zachycená frekvence: vedení vede pouze vlny poměrně krátké ve srovnání s jeho tloušťkou. Hepburn zveřejňuje tabulku tloušťky → minimální užitečná frekvence ([DX Info Centre](https://www.dxinfocentre.com/propagation/luf.htm)); je to měkký limit, pod kterým signál postupně klesá.

| Led Tloušťka | Minimální splatná frekvence |
|  61 m  | 962 MHz |
|  91 m  | 464 MHz |
|  122 m  | 276 MHz |
|  152 m  | 185 MHz |
|  183 m  | 133 MHz |
|  213 m  | 101 MHz |
|  305 m  | 53 MHz |

V rozsahu VKV (často 120-300 m) se hodnoty v tabulce řídí docela dobře zákonem o konstantě; pod 100 m vzorec podhodnocuje frekvenci až do 30-40%. Adaptování sám (není vzorec zveřejněn Hepburn), s f v MHz a d v metrech:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

V podstatě, pro 2 m, potrubí je často nejméně 170-180 m, pro 70 cm je asi polovina. Vzorec cirkulující na síti, λmax = 0,014 · d ^ 1.5, pochází z Kerru (1951, kapitola 1, eq. 18) a je platný s λ v centimetrech a stojící, pro vedení, ve kterém M padá lineárně: s těmito jednotkami dává asi 2 m 180 m a asi 70 cm 89 m, to je stejné hodnoty Hepburn tabulky, která pochází z něj. Kerr varuje, že to není čistý řez: i vlny několikrát déle trpí hodně vedení, a způsob, že "ztráta" trochu může zlepšit příjem i se stanicemi mimo vedení. Studie IEEE Access z roku 2019 potvrzuje, že klasické metody (Kerr, Kukuškin) nepopisují skutečný řez ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)): práh by měl být použit jako indikace, nikoli jako zeď.

## 4. Údaje dostupné zdarma

Slabým bodem je vertikální rozlišení: mezi 1000 a 850 hPa modely poskytují úroveň každých 25-50 hPa, to je každý 200-450 m, takže dobře vidí inverze na 500-2000 m, ale ne tenké kanály v blízkosti země. LIVETROPOVHF již používá Open- Meteo do isobare, teplotu do 850 hPa a inverzi: index může začít ze stejných hovorů.

| Zdroj | Co se děje? | Usnesení | Použití pro index |
| [Open- Meteo, GFS](https://open-meteo.com/en/docs/gfs-api) | T, relativní vlhkost, rosný bod, geopotenciální výška podle úrovně (1000, 975, 950, 925, 900, 850 hPa...); výška mezní vrstvy | 0, 25 ° na tlakových hladinách; 16 dní, hodiny do 120 h, aktualizováno každých 6 h | profil N a M, gradient, základna a horní část potrubí; již v programu |
| [Open- Meteo, ECMWF](https://open-meteo.com/en/docs/ecmwf-api) | stejné úrovně od open- datových modelů 0.25 °; zem dává rosný bod |  0,25°  | druhé stanovisko, užitečné pro shodu mezi modely |
| [Open- Meteo, DWD ICON](https://open-meteo.com/en/docs/dwd-api) | T a relativní vlhkost na tlakových hladinách; v ICON- D2 je 800, 900 a 925 hPa | ICON- EU asi 7 km | třetí stanovisko k Evropě |
| [Radiosonds, Univ. Wyoming](https://github.com/NCAR/RAOBget) | měřené profily, dva starty denně (00 a 12 UTC), textový formát na stanici | mnoho významných úrovní, i každých pár desítek metrů | ověření indexu; viz jemné kanály, které model nevidí |
| [Groans, NOAA NCEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive) | historický archiv vysílání, soubor na stanici, odvozené parametry | výše uvedené | kalibrace let dat a porovnání s místy minulosti |
| ERA5 (Copernicus) | hodinová reanalýza od roku 1940, 37 úrovní tlaku | asi 28 km | statistika a kalibrace, nikoli předpověď |

- S geopotenciální výškou Open- Meteo je nad hladinou moře: úrovně, které spadají pod zem musí být zlikvidovány ([Open- Meteo, horní vzduch](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)).
- Na zemi jsou přidány T a vlhkost na 2 m: dávají první úsek gradient, ten, kde se noční povrchové kanály rodí na pláni a na moři.
- Italské vysílání v archivu University of Wyoming (weather.uwyo.edu / upperair / sounding.html): stanice a čísla WMO v následující tabulce.

| Stanice | W | Zeměpisná šířka | Zeměpisná délka | Sdílet |
| Udine / Campoformido |  16044  |  46,03  |  13,18  |  94 m  |
| Milán / Linate |  16080  |  45,43  |  9,28  |  103 m  |
| Praxe moře |  16245  |  41,65  |  12,43  |  12 m  |
| Brindisi |  16320  |  40,65  |  17,95  |  10 m  |
| Trapani / Birgi |  16429  |  37,92  |  12,50  | asi 14 m |

Textový formát Wyoming dává pro každou úroveň tlaku, výšky, teploty, rosného bodu a vlhkosti: dost pro N a M. Wyoming nemá oficiální rozhraní, skripty se používají jako [RAOBget NCAR](https://github.com/NCAR/RAOBget); College of DuPage uchovává kopie v textu stanice ([např. 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)).](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. Indexy a zveřejněné metody

Hepburn a F5LEN začínají ze stejných fyzikálních složek, ale nezveřejňují kompletní algoritmus; vědecké studie 2008-2026 místo popisu metody krok za krokem, a to je to, že je nejlepší kopírovat recept.

| Metoda | Spuštění dat | Co se vypočítá | Užitečné poznámky pro nás |
| [Hepburn Tropo Index](https://www.dxinfocentre.com/propagation/hti.htm) | raw weather models of the Canada Meteorological Service (MSC, as written under its maps; not GFS / NAM), maps designed with GRADS, proprietární algoritmus ([storia](https://www.dxinfocentre.com/propagation/history.htm)) | 0-10 lineární stupnice zakřivení signálu: 0 normální, 1-2 superlom, 3-4 některé kanály, 5-6 rozptýlené kanály; negativní hodnoty = sub- lom | Stupnice, která má být napodobena, takže třetí index se okamžitě srovnává s prvními dvěma |
| [F5LEN, světová předpověď](https://tropo.f5len.org/WW/info.php) | pro Evropu ARPEGE model Météo-France, 7,5 km, více úrovní mezi 2 m a 1500 m ([poznámka autora](https://tropo.f5len.org/author/f5len/)) | Průměrný index pro pronajímatele, 40 kroků od 3 hodin, 4 výpočty za den, 28denní archiv ([nová verze](https://tropo.f5len.org/?p=504)) | potvrdit, že slouží modelu s mnoha nízkými úrovněmi |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf) | 60 úrovní ECMWF modelu, od země do 3000 m | minimum dN / dz na kolonu: prováděno pod − 157 N / km, superrefrakce pod − 78, 7 N / km; základ vrstvy pasti | klimatologie: Po Valley evropský prostor s několika potrubí po celý rok; Středozemní moře v létě kolem 100%, v zimě pod 15%; Itálie přes 30% v letních nocí |
| [Indermuehle a Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861) | GFS 0,25 °, 21 tlakových úrovní + pole při 2 m | M = N + 0,157 z; minimální dM / dz pod 3 km, základna a horní část potrubí, hloubka M deficitu; pod 40 m vyřazen | ověřeno po dobu 7 let na 25 kanálech od 88 MHz do 2,68 GHz; dobrá kapacita do 3 dnů předem (AUC cca 0,74), pak padá; GFS lepší než ERA5 |
| [Hämäläinen et al., MMF 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf) | Harmonie- AROME, 750 m nebo 2,5 km, 65 nebo 90 úrovní | dM / dh < 0 na profilu modelu, srovnání s paprskem 4 až 59 m | je svislější než vodorovné rozlišení; žádný model nevidí kanály pod 15 m; vlhkost dominuje teplotnímu gradientu |

Třetí amatérská rozhlasová služba již aktivní: [TROPOCAST.EU](https://tropocast.eu/)Artur SQ3A, pro 2 m a 70 cm v Evropě. Ze stránky předpovědi ([tropocast.eu / předpověď](https://tropocast.eu/forecast)):

- Údaje: DWD ICON-EU (průchod 0.0625 °, asi 7 km), výchozí zdroj; alternativně kanadské GDPS (0.15 °), méně podrobné.
- Metoda: N a M podle ITU-R P.453 z profilu mezi vrstvou v blízkosti země a 850 hPa; rozpoznání vrstev, spojení potrubí k zemi a vysoké a prahy barev jsou správnou kalibrací autora, nezveřejněno.
- Produkty: "kompletní" pohled 0-3000 m, více oddělené názory "na zem" a "vysoké" (prozatím pouze v mapách, které mají být začleněny); skóre 0-100 v devíti třídách (pod 38 pozadí, 38-52 marginální, 53-58 slabý, 59-65 střední, 66-72 dobrý, 73-80 velmi dobrý, 81-88 vysoký, 89-95 silný, 96-100 velmi silný); předpověď až + 72 h na stupních hodiny, s animací a archivem.
- Revíry: Evropa, Polsko, Spojené království, Německo, Francie, Španělsko, Itálie, Česká republika, Chorvatsko; rozhraní také v italštině.
- Opakované použití: mapy jsou zabudovány do irámu bez klíče API; je zde také zdokumentovaný API (OpenAPI a JSON schéma). Licence není uvedena: Před použitím jako třetí zdroj vedle Hepburn a F5LEN byste se měli zeptat autora.

Rozdíly od našeho projektu: TROPOCAST zastaví na 850 hPa (cca 1,5 km), zatímco zvýšené kanály dorazí na 2000 m a dále ( LA0BY případ 22 / 11 / 2018), a neudává index pro pásmo nebo trasu. Třídy 0-100 lze porovnat s naším indexem 0-10 ve stejný den.

TROPOCAST obrazová struktura (z odkazu autora programu): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, kde n je krok předpovídání. Stránka zakazuje automatické stahování robotů, takže jsem nemohl přečíst obrázek; také pro to, před zobrazením těchto map v LIVETROPOVHF slouží autorovi svolení.

Notebook: [DJ5AR, kategorie](https://dj5ar.de/?cat=174)(Magonza, 21 článků 2017-2023, 2 m-13 cm). Popsané případy: vedeny několik týdnů z Irska (23. května -2 června 2023), FT8 do Běloruska (září 2020), Italsko-Německo přes Alpy a Jadran (srpen 2017). Uznávejte případ "příliš vysokého" kanálu (převrácení na 1000 m), který nemůžete použít ze země: další potvrzení, že je nutné porovnat podíl vedení s podílem stanic. Používejte radiotelefony jako měřítko clony (až 1028 km) a vertikální profily Paříže, Bordeaux a DWD.

V souhrnu, společný recept: vertikální profil → N a M na vrstvu → vrstva s dM / dh negativní → základ, vrchol, tloušťka a síla potrubí → index. "Tajná" část Hepburnu je empirická kalibrace reálných událostí, které můžeme předělat spoty.

## 6. Užitečné předpoklady pro program

Kromě N a M (základní část fyziky) se používají k posunu od vertikálního profilu k bodovému číslu a pak po cestě. Vzorce ITU byly rekonstruovány ve formátu PDF, který byl špatně přečtený, dne 22. listopadu byly přezkoumány na oficiálních textech ([ITU-R P.452-18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453-14, P.526-16, P.372-18) a opraveny v kódu: konstanta troposcatteru 190 (ne 190,1) plus absorpce plynů s DD = 3 g / m3; α není nižší než − 3,4 (eq. 55a); plyn z kanálků s 3A4 = 7,5 + 2.5 · ω (eq. 9a), tlak páry na plné sférické půdě (eq. 23-37: průměrná hodnota moře, funkce výšky s limitem 2 + 20 logs K, interpolace pod optickým průtokem); parní saturace P.453-14 (eq. 9, s EF). Zvěsti P.372-18: tabulka 1 a galaktická odpovídají; křivky umělého hluku jsou až 250 MHz a galaktické až asi 100 MHz, přes program rozšiřuje stejné zákony.

1. Gradient mezi dvěma úrovněmi modelu (h v km, M v jednotce M):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. Konduitní pevnost a tloušťka: tloušťka d = horní − základna (pro půdní kanál, základ = půda); pevnost jako M nedostatek mezi základnou a horní.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3. Ekvivalentní faktor poloměru Země (P.452-18, eq. 5 a 6a), s ΔN = průměrný pokles N v prvním km, pozitivní, v N / km:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

S ΔN = 40 máte asi 4 / 3 k (standardní atmosféra); s ΔN blížící se 157 k závěsy nekonečna, to znamená, že zemské rádio se stane plochý: to je začátek vedení.

4. Rádiový horizont vysokofrekvenčních h metrů s poloměrem odpovídajícím\ _ e v km:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5. Pravděpodobnost, že potrubí pro zeměpisnou šířku (P.452-18, eq. 2), procento času; Ø = zeměpisná šířka středu trasy, μ1 a μ4 vzít v úvahu, jak daleko je po moři:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

Při 41 ° N, všechny po moři (μ1 = μ4 = 1), dává asi 11% času: je klimatologický základ, užitečný jako referenční hodnota, od které se očekávaný index liší.

6. Ztráta troposcatteru (P.452-18, eq. 45; f v MHz, d v km, θ úhel dráhy v mradu, N0 pozemní reaktivita v centru trasy):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

Termín − 0,15 · N0 ukazuje, proč mokrý vzduch pomáhá i bez potrubí: 50 jednotek navíc N je o 7,5 dB méně než ztráta. LIVETROPOVHF již používá G3YGF verzi ztráty rozptylu.

7. Ztráta v potrubí (P.452-18, eq. 46-51): pevná ztráta spoje plus útlum pro úhel miliradiantu, který roste s krychlovým kořenem frekvence (f v GHz, a\ _ e v km):

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. Od Bean a Dutton (1966) a Kerr (1951)

Tyto dva klasické texty dávají tři věci, které jsou potřebné okamžitě k indexu: gradient prahy již tarate na stejné úrovni tlaku GFS, co je cenná v dB změna reaktivity, a jaké povětrnostní situace vytvořit nebo zničit kanály. * Radio Meteorology *, NBS Monograph92.

1. Klasifikace profilu tlakových vrstev (Bean a Dutton, tabulka 6.5, s. 248; − dN / dh v N / km). Navrženo pro rádiové vlnové délky, platí stejně jako na úrovni modelu:

| Silnice | Sdílí o | Sub- lom | Nelaminované | Superlom |
| 1000- 850 hPa | 0- 1,46 km | - dN / dh < 0 | od 20 do 60 | nad 100 |
| 850- 700 hPa | 1,46- 3,01 km | - dN / dh < 0 | od 20 do 50 | nad 80 |
| 700- 600 hPa | 3,01- 4,20 km | - dN / dh < 0 | od 15 do 40 | nad 70 |
| 600- 500 hPa | 4,20- 5,57 km | - dN / dh < 0 | od 10 do 30 | nad 50 |

2. Kolik stojí v dB (Bean a Dutton, cap. 6, pp. 233-258), měří od 70 do 180 MHz na trasách za horizontem:

- Jakákoli vrstva (na zemi nebo v nadmořské výšce) ve srovnání s "hladkým" profilem zvyšuje signál 10- 25 dB na všechny procenta; větší zisk pochází z vysokých nakloněných vrstev, pravděpodobně díky působení zaostřování (směr 200 km).
- Reaktivita půdy Ns a pokles v prvním km ΔN souvisí s měsíčním mediánovým signálem s koeficientem mezi 0,4 a 0,95, mediánem 0,7 (20 tras od 130 do 446 km, cca 100 MHz).
- Roční cyklus: 0,18 dB na jednotku N; denní cyklus od 0,2 do 1,1 dB na jednotku N, maximálně na trasách 175-200 km a v zimě.
- Složený parametr Misme odhaduje odchylku pole jako 0,5 dB / (N / km) · (ekvivalentní gradient − 40) plus termín tepelné stability; parametr turbulence D (plocha mezi skutečným profilem a adiabatický, 0-3 km) se ve srovnání s jednoduchým ΔN jen zlepšuje. Závěr autorů: ΔN zůstává nejlepším parametrem pro jednoduchost.

3. Referenční profil a anomálie (Bean a Dutton, § 3.8 a 3.9). Exponenciální atmosféra referenční CRPL dává "normální" profil od Ns; jednotka A měří, jak moc se skutečný profil liší na každé akcii. Je to přirozený způsob, jak transformovat profil GFS do anomálie dát na mapu:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

S Ns = 313 máte ΔN asi -42 N / km, což je ekvivalentní poloměr 4 / 3. Kritérium vedení − 157 N / km je zde odvozeno také od Snellova zákona pro horizontální poloměr (eq. 4.23), téměř nezávislý na výšce antény.

4. Kde a kdy potrubí vznikají (Bean a Dutton § 4.4; Kerr čepice. 3):

| Postup | Situace | Účinek |
| Varování | teplý a suchý vzduch ze země tekoucí na studeném moři (typický pro letní rukáv po dnech dobrého počasí); především počítá s nedostatkem vlhkosti | intenzivní vedení na moři |
| Nokturnální ozařování | klidné nebe, slabý vítr; nízké mraky nebo hodně vlhkosti zabránit | vedl k zemi, která se rodí při západu slunce, roste ve večerních hodinách a zmizí rychle po východu slunce, zejména uvnitř |
| Poddanství | vzduch přicházející z anticyklonu, vyhřívaný a suchý, nad čerstvým a vlhkým vzduchem; časté záplavy v horách a jihovýchodních sektorech anticyklonu | superrefrakční vrstva nebo vysoká kanalizace; téměř vždy gradient vyšší, než je standardní obráceně |
| Moře a půda | pobřeží, oběh několika stovek metrů | místní potrubí podél pobřeží |
| Proti vedení. | mixování: země vyhřívaná sluncem od poloviny dopoledne do odpoledne, vítr na zemi nad pár m / s na drsné půdě; noční mlha | pravděpodobnost vedení téměř nic. |

5. Dvě nové ingredience. Svislý pohyb: stoupající proudy v konvergenčních zónách odstraňují a odstraňují vrstvy, subsidence posiluje signál (Moler a Holden: v průměru asi 2,5 dB více než s horním vzduchem, na moři v pásmu L a X). Open- Meteo již poskytuje vertikální rychlost na tlakových hladinách. A trend tlaku: na cestě 140 km na 186 MHz signál sestoupil, když tlak klesl na vysílač a do středu cesty a vrátil se vysoký a bez evanescence, když šel nahoru, s pouhými 2 (Bean a Dutton, obr. 6.9, s. 241).

6. Od Kerra. Kromě vzorce maximální vlnové délky (sekce základní fyziky), Kerr dává přibližný vzorec pro M z jediné teploty, páry a nadmořské výšky (§ 3.9, se starými konstantami 79 a 4800 pak nahrazen 77,6 a 4810) a konstatuje, že na reverzní řezání větru může generovat vlny, které houpají ve výšce kanálu, možné příčiny hlubokých úniků (§ 3.26). Italský zdroj hlášený Beanem a Duttonem a k vyhledávání: L. Bonavoglia (prosinec 1958), "Souvztažnost mezi meteorologickými jevy a rozmnožováním"...

## 8. Z amatérských rozhlasových prezentací a technických zpráv (druhá série)

Sedm dokumentů poskytnutých autorem programu. Tři slouží přímo k indexu: W3SZ pro troposcatter "podlaha", LA0BY pro případy Hepburn nebyl předpovídal, Hepburn ( LU7MC) pro klasifikaci počasí kanálů.

1. Troposcatter: signál vždy tam. R. Rehr W3SZ, "Troposférický Scatter (Troposcatter) Propagation for VHF, UHF, Mikrovlnné a Frequences", Pack Rats Conference2014; J. R. Duffey KK6MC ", The Troposcatter Propagation Mode and How to Predict Coveration on the VHF / UHF Bands", CSVHF2017.

- Užitečné od cca 100 do 700 km, od 144 MHz do 10 GHz; ztráta roste o 30 log f s frekvencí.
- Především počítá startovní úhel směrem k horizontu: každý další stupeň stojí cca 9-12 dB (metoda M1CNK, Collins).
- V létě je signál v průměru o 10 dB vyšší než v zimě; v polovině odpoledne asi o 5 dB nižší než v časném ranním nebo večerním hodinách. KK6MC ukazuje celkovou variabilitu 13- 15 dB s časem, sezónou a časem.
- Metoda Yeh, nejjednodušší k programu (f v MHz, d v mílích, θ úhel rozptylu ve stupních, α geometrický průměr otvorů obou svazků):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Porovnání Larsen (1968) na 15 reálných spojů: průměrná čtvercová chyba 5,1 dB pro Collinse, 7,4 pro Yeh, 8.1 pro NBS a CCIR, 16,8 pro Ridera. Všechny metody zlepšit, pokud N udržuje stabilní (Yeh klesne na 5,4 dB): pro rozptyl refrakční dne je málo, takže index tropo musí podívat na vrstvy, nejen N. Tabulka 15 spojů s měřenými ztrátami (včetně Feldberg- Monte Baldo, 322 km při 957 MHz, 223,1 dB; Israeus- Israel, 319 km při 77,5 MHz, 163,3 dB), je pro náš výpočet dobrou zkušební lavičkou.
- KK6MC udává ztrátu, že typická stanice dokáže překročit 2 m a odpovídající vzdálenost, od 184 dB (FT- 817 a 3 prvky v SSB, cca 130 km) až 215 dB (dlouhé bungy v SSB, asi 430 km) a 236 dB (kW a yagi spojené, asi 640 km). Je to způsob, jak přeložit index dB do vydělaných kilometrů.
- Přímý kód: W3SZ Yeh a Collins program (nitehawk.com / w3sz / YehCollins.zip) a Bob Atkins KA1GT kalkulačka (bobatkins.com / rádio / troposcatter.html).

Další zdroj na rozptylu: I. Roberts ZS6BTE, "Troposférický Scatter Propagation", * Radio ZS *, December 1984, with note of 2013 ([qsl.net / zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). Hlavně Užitečná geometrie: úhel rozptylu θ = θ0 − θ1 − θ2, s θ0 = d / R, θ1 = (h1 − h\ _ ostacolo) / d1 + d1 / 2 R a totéž pro druhou stanici R = 8446 km (4 / 3 poloměru Země). Je to kus, který se používá k výpočtu úhlů z profilu země v troposcatteru "podlaha". Další údaje: každý úhel s více náklady asi 10 dB; přechod z Ns 280 na 300 snižuje ztrátu asi 4 dB; objem rozptylu je mezi 300 a 2000 m pro 150 km, mezi 600 a 3000 m pro 300 km; bouře v rozptylu objem zvýšit signál asi 20 dB. Číselný příklad výrobku se nevrátí s daným vzorců (doba volného prostoru 900 MHz a 872 km je asi 150 dB, ne 125), takže vzorce by měly být kontrolovány na W3SZ před jejich použitím.

2. Typy vedení (W. R. Hepburn, "Troposféric Propagation Modes", dxinfocentre.com / propagace / tr-modes.htm, španělský překlad LU7MC). Vyplňte tabulku Bean a Dutton s užitečnými případy v Itálii:

| Typ | Kdy | Poznámka pro index |
| Ozařování | klidné a klidné noci na zemi | sledovat zem, zmizí krátce po úsvitu |
| Vysoký tlak | anticyklon | může trvat celý den; v noci může vedení z ozařování dole blokovat vzdálené signály, takže den může jít lépe |
| Přední strana | před teplou frontou, za studenou frontou, severně od stojící fronty | krátké; špatné počasí ho znepokojuje |
| Horský sestup | föhn, bora: vzduch klesá, topení a sušení | vyzkoušet na Bora na Jadranu a föhn jižně od Alp |
| Varování | teplý a suchý vzduch nad studenou a vlhkou půdou, například po dešti | vedl k zemi |
| Údolí | horký vzduch nad studeným vzduchem zastaví v údolí | může zůstat ve dne |
| Marino | teplý a suchý vzduch na studenější vodě, přichází na pobřeží s vánkem | Vrchol odpoledne; jaro a časné léto nejlepší roční období |

Odpařovací kanály na moři (0-30 m) slouží pouze asi 3 GHz a nevstupují do vnitrozemí: pro 144 a 432 MHz můžete ignorovat. Vysoké hory jsou překážkou, pouště jsou příliš suché.

3. Případy, které Hepburn nepředvídal (S. Heck LA0BY, "Extrémní tropo propagace na 144 MHz a nahoru", Nordic VUSHF 2019, od JO59IX do 500 m nadmořské výšky):

- 22. listopadu 2018: 70 cm až do UA6 (cca 2600 km) po dobu 6- 8 hodin. Společnost Hepburn uvedla pouze pobaltské státy a Polsko. Průchod byl vidět v teplotních mapách 900 a 1500 m a skončil těsně nad 2000 m; to bylo vyšší na norské straně, a pouze LA0BY na kopci vstoupil. Na zemi se nezdálo jako trope: zima a mlha na vrcholu.
- 11. února 2015: 2 m a 70 cm směrem k 9A a S5 (cca 1500 km) přes Alpy, také to nepředpokládá Hepburn.
- 15. října 2011: 2 m s EA1 (2135 km) ve vysokých kanálech, na třech úsecích moře a tři země.
- Další události: 6- 11. listopadu 2003 východně (2050 km), 10. prosince 2004 směrem k Francii.
- Polní adresy: mlha v hladině, volné vrcholky, malý vítr, vlhká půda. Normální teplotní gradient − 6,5 ° C / km; v reálném případě ( VK3KAQ) + 7 ° C za 20 m.

Závěr autora: Hepburn a F5LEN nestačí, obslouží profily v nadmořské výšce podél trasy. Pro nás: tyto dny jsou případy ověření, které mají být přepočítány s historickými údaji o modelech, a index na cestě musí porovnat kvótu potrubí s kvótou stanic.

4. Italské zkušenosti (G. IV3GBO, "Typy VHF šíření SSB / CW", 2017): vedeny mezi 450 a 3000 km, s vrstvami mezi 450 a 3000 m; mlha rozšířena jako vodítko; otvory po moři na Jadranu směrem Puglia, Albánie a Řecko. Scatter od 100 do 500 km.

5. Představení (P. Wilton M1CNK, "Úvod do VKV / UHF propagace", 2013): 70 cm je často lepší než 2 m; s 10 m pólem při 144 MHz je počáteční úhel asi 3 °, s 20 m přibližně 1,5 °.

6. Zpráva NASA (E. Hirschmann, "Troposférické a ionosférické efekty na Radio Frequency (VHF-SHF) Communication," Goddard X-731-67-89, 1967): týká se chyb na dálku satelitů, nikoli DX. To přináší zpět stejný N vzorec a exponenciální profil (např. na Floridě: Ns 370, ΔN − 55, konstantní 0,161 / km) a posílá dva zdroje NBS hledat: Bean a Thayer, "CRPL Exponenciální referenční atmosféra", Monografie 4 (1959), a Bean, Horn a Ozanich, "Klimatické Charts a Data Radio Refractive Index pro Spojené státy a svět", Monografie 22 (1960).

## 9. Z VKV / UHF DX knihy, kapitola 2

I. White G3SEK (edited by), * The VHF / UHF DX Book *, DIR Publishing 1995 (ISBN 0 9520468 0 6; digital replica 2017), capture 2 "VHF / UHF Propagation", pp. 2-13, 2-22-2-29 and 4-11. Jedná se o nejbližší text k radioamatérské zkušenosti mezi těmi, které byly shromážděny; potvrzuje prahové hodnoty, které již byly použity a přidává nová synovská pravidla.

Číslo

- Stejný vzorec N (77.6 P / T + 3.73 · 10 ^ 5 e / T2); příklad: 1000 hPa, 7 ° C a = 10 hPa poškození N = 324. V dobře tvarované atmosféře, například po bouři, je N téměř lineární asi 39 jednotek / km v prvních 2-3 km.
- Gradienty mezi 0 a − 79 N / km jsou normální, pod − 79 je superrefrakce, nad 0 subrefrakcí. Kniha trvá na tom, že dlouhé DX nepochází z jednoduché superlomu, která by měla zůstat přímo na celé cestě, ale z kanálů.
- Minimální tloušťka potrubí, trochu opatrnější než Kerr a Hepburn:

| Pásmo | Minimální tloušťka |
| 50 MHz |  400 m  |
| 70 MHz |  300 m  |
| 144 MHz |  200 m  |
| 432 MHz |  100 m  |
| 1.3 GHz |  50 m  |

- Poměr není úměrný vlnové délce (asi 65 λ při 50 MHz, 220 λ při 1,3 GHz), protože okraje potrubí jsou "rozděleny". Ve Velké Británii jsou potrubí často na 144 MHz, ale téměř nikdy na 70 MHz nebo nižší, a často 432 MHz jít silný, když 144 jsou slabé.
- V potrubí signál klesá jako 1 / d místo 1 / d2: až 1000 km útlum může být podobný jako u volného prostoru.
- Minimální kapacita stanice na 2 m (100 W, 15 dBi yagi, 2 dB hluk): 217 dB, což je asi 400-500 km v SSB v průměrných podmínkách; v CW asi 20 dB v více (p. 4-7). Souhlas se stolem KK6MC.
- Scatter: ztráta se zvyšuje asi o 15 dB o trojnásobek frekvence a 10 dB pro každý stupeň úhlu rozptylu (Yeh vzorec; v textu extrahovaný konstanta je nečitelná, takže musí být převzata z verze W3SZ). Rozptýlení "up" od konvektivních buněk se běžně vztahuje až na cca 500 km, 1000 km s dobrými stanicemi. Po těžkých deštích a prodloužených podmínkách je na všech pásmech málo.

Typy potrubí

- Na zemi: na zemi se špatně množí, protože půda absorbuje a šíří; na klidné moře dorazí daleko (Scilly- Canarie, 2500 km, asi jednou ročně na 2 m). Odpařovací kanály jsou často jen asi 15 m vysoké a slouží mikrovlny.
- Vysoký: 450 až 2000 m, díky delšímu DX na zemi, signál "vysoký" nad stanicemi níže. "Prohra" kanály dávají méně silné signály, ale také umožňují mezistanice vzdálenosti.
- Přívody mají přesné směry a mohou trvat hodiny nebo dny, na rozdíl od sporadického E.

Nová synoptická pravidla pro index

- Věk anticyklonu: vzduch klesne do středu několika metrů za hodinu a zpětný chod se sníží, jak se vyvíjí vysoký tlak; větší otvory dorazí po několika dnech šíření již dobré. Krátkodobý anticyklon nestačí.
- Vysoký tlak: vrstva je kopule a na okrajích je nižší, takže stanice se zlepší.
- Směr: Bylo zjištěno, že delší trasy běží podél izobares nebo přes dorsals (Flavell G3LTP, 1984), ale existují vynikající spojení i v radiálním směru od centra. Příklad ze dne 29. - 30. srpna 1987: od Anglie se po posunu anticyklonu otočilo otevření o 180 ° za dva dny (Španělsko, Francie, Itálie, Rakousko, Norsko).
- Proti: silný isobare (silný vítr), stratocumuliform mraky na okraji vrstvy (omezit nejvyšší frekvenci).
- Fronty: studená fronta tlačí horký vzduch nahoru a vytváří zpětný chod na přední straně; nejlepší směr je podél přední části při průchodu, ale to trvá málo.
- Sezóna: Ve Velké Británii jsou anticyklony častější na konci léta a na začátku podzimu.
- Vizuální stopy: kouř zachycený v tmavé čáře nad obzorem; s vysokým tlakem, laiercumules se základnou pod 1000 m indikují pozemní kanál, základna nad 1000 m vysoký kanál.

Odkazy na vyhledávání: R. G. Flavell G3LTP, "Studies of an Extensive Anticyclonic Propagation Event...", * Radio Communication *, February 1984, p. 128; J. Gannaway G3YGF, "Troposféric Scatter Propagation", * Radio Communication *, September 1981, s. 710; P. W. Sollom G3BGL ", A Little Flutter W6ZGN

## 10. Ověřený amatérský radiový model: Biancomano a Leyshon (1998)

V. J. Biancomano, S. W. Leyshon ", Practical Troposferic Model for VHF / UHF Propagation Prediction", * National Weather Digest * 22: 2, June 1998, pp. 15-24, zdarma v [PDF z National Weather Association](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf).

- Data: rádiová vlna 00 a 12 UTC 47 stanic ve Spojených státech, 1995-1997; pozemní analýza 700 hPa (na začátku až 400), na profilu N, bez M. Vyloučení pobřeží, ostrovy a hory.
- Pro každý průzkum: přítomnost a podíl potrubí, maximální vlnová délka, která může vést (často asi 100 krát vlnová délka), maximální gradient potenciální teploty mezi sousedními úrovněmi a mezi jednou úrovní a druhou výše (pro silnější vrstvy). Pozemní kanály se nepoužívají.
- První pravidlo, vytrvalost: indexy stability (zvýšený index, K, celkové součty, SWEAT) zůstávají v rozmezí 3 jednotek od průzkumu 12 hodin před, a tlaková hladina, kde potenciální teplota roste více zůstává přibližně stejná. Pokud ano: prognóza zlepšení v příštích 12 hodinách.
- Druhé pravidlo, dynamické kontroly: mezní vrstva, která se nezvýší, CAPE, změna tloušťky na 500 hPa při třech průzkumech, změna sklonu v mezích.
- Prahová hodnota pásma: vedení schopné přenášet100 cm nebo více znamená otevření na432 MHz, téměř jisté na222 a často i na144.
- Space filter: stanice na šestihranné mřížce cca 400- 500 km; předpověď má hodnotu pouze v případě, že je alespoň 5 kontiguálních stanic kladných (pokud můžete jednu vynechat). S 5 stanicemi je pravděpodobnost falešného náhodného kladného efektu zanedbatelná.
- Ověření na základě událostí ve sloupci "Svět nad 50 MHz" QST: v roce 1996, 48 události plánované na 67 (71%); 12 z 15 hlavních událostí. Přesnost až 70% na 432 MHz a asi 60% na 144 MHz ve 12 hodinách; poskytuje téměř dvojnásobek skutečných událostí; 9 chyb z 19 jsou předpovědi přišel pozdě, protože mnoho zlepšení tvoří za méně než 12 hodin.
- Přidáno ke stanicím dále na sever: vítr rotující po směru hodinových ručiček (teplý start) jako další podmínka.

Z kompletního PDF (dodaného autorem programu) tři další podrobnosti. Jejich pravidlo "tloušťka =100 krát vlnová délka" dává600 m pro6 m,200 m pro2 m,125 m pro 1,25 m a70 m pro70 cm (obr.3): pro70 cm je méně těžká než Kerr a Hepburn (cca89 m), pro2 m se shoduje s VKV / UHF DX Book. Vítr, který se otáčí po směru hodinových ručiček, podle autorů, zejména proto, že přináší teplejší vzduch ve výšce, což je to, co způsobuje, že pád N roste s tlakem; vlhkost, kterou přináší s ním nezdá destabilizovat. Dodatek získá dN / dh v uzavřené formě pro konstantní teplotní gradientovou atmosféru s prudkým skokem teplotního sklonu a vlhkosti v dané kvótě: užitečné jako zkušební případ pro náš kód. Jejich stopa po sklonu potenciální teploty pochází z K. H. Jehn, "The use of potential refractive index in synoptic scale radio meterology", * J. Meteorology *17,1960, str.264.

Co si máme vzít pro náš index: (1) Termín vytrvalosti: porovnejte profil s profilem 12 hodin před, že v hodinové předpovědi máme volný; (2) vesmírný filtr: bod se počítá pouze v případě, že zlepšení pokrývá oblast několika stovek km, což snižuje falešné alarmy; (3) indexy stability, vypočtené od teploty a rosy na 850, 700 a 500 hPa (Open- Meteo již dává zvednutý index a CAPE); (4) vítr, který se otáčí s nadmořskou výškou mezi 1000 a 850 hPa; (5) jejich ověřovací metoda, která je stejná, jak chceme dělat s programovými reklamami.

Další zdroj ve stejném balíčku, který je pro nás málo použitelný: P. K. Rastogi, "Stratosphere and Troposphere (S-T) Studies at Millstone Hill", NASA 1984 (dokument 19840019069): 440 MHz radar, který měří turbulenci čistého vzduchu mezi 4 a 25 km, s tenkými vrstvami spojenými s gravitačními vlnami a sekáním větru. Potvrzuje, že turbulence, která vytváří rozptyl, jsou vrstvené, ale týkají se příliš vysoké šance pro kanály.

## 11. Třetí kolo zdrojů (20. listopadu)

Vysokokanálové historické případy. J. Macoun OK1VR, 2 m od summitu Sněžka (1602 m), podzim 1958, článek z roku 1959 přeložen v roce 2006 ([ok2kkw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)). Ve dnech 27.-28. října 1958 první československo-anglické spojení na 145 MHz ( G5YV, 1220 km) a mnoho dalších mezi 1100 a 1250 km, se stabilními signály po 23. Autor ji přikládá dvěma supervnuceným inverzím, horním asi 2200 m, přičemž signál se vrací na zem pouze tehdy, když se spodní vrstva rozpustí; podmínka zvýhodněná pomalým anticyklonem z Atlantiku na kontinent. Z domova (Praha) jeho maximum bylo 530 km: pouze stanice v nadmořské výšce vstoupila do potrubí. Jedná se o další případ ověření (např. LA0BY 2018) a potvrzuje, že index by měl hledat více než 2000 m a porovnat jej s podílem stanic.

Historické knihovny NBS (užitečné pro hledání klasických děl, nikoli vzorců):

- W. Nupen, * Bibliografie na troposférické propagace radiových vln *, NBS Technical Note 304, 1965, 356 stran, s indexem pro témata (také "předpověď stavu") a podle země. Hlášené italské položky: L. Sacco, "The radio connection in atmosferic superrefraction regime", * High Frequency * 24 (6), December 1955 (Italský výzkum: superrefraction under 500 m frequent day and night); S. Silleni, * Annali di Geofisica * 8 (1), 1955; J. M. Clara e A. Antinori, prova tra Sardegna e Minorca da 238 a 297 MHz (390 km), * Electrical Communication * 33 (2), 1956. A. F. Clarke (Fort Huachuca, 1956) vydal denně předpověď maximální vlnové délky zachycené pro příští ráno, od vlhkosti, teploty, teploty, tlaku a předpovědi větru.
- R. L. Abbott, * Bibliography of Troposféric Radio Wave Scatting *, NBS, material until January 1960, about 670 objednaných položek per year: utility for the troposcatter part.

Zveřejnění a pozorování: F. W. Odenthal DK5EQ, "Ausbreitung 2m - Eine Einführung" (31 / 05 / 2024): téměř optické až 200 km, tropo na kanál nebo lomu za, s vysokým tlakem a na jaře a na podzim; cituje VKV stránku DM2HB a poznámky DK5YA o tropo a Es. A. Johnson VK3DXE na blogu EI7GL (prosinec 2021): s WSPR VHF cesty tisíce km v Austrálii byly objeveny; Trope není jen v létě. Diskuse o VKV marina a AIS ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)): rozsah 1,23 · ³ h (ft) námořních mil na anténu, rozptyl až asi 50 nm, typické zlepšení trope 100-400 nm; vedení jsou uznávány, protože vzdálené stanice přicházejí stejně silné jako sousedi. Chelmsford ARS, prosinec 1983: v soutěži 2 m jsou stanice východního pobřeží Anglie upřednostněny námořní cestou na kontinent.

ARRL, * The Radio Amateur 's VKV Manual *, 1972, cap. 2 "Propagation Beyond the Horizon" (pp. 17-19). Již známé provozní tipy (sledovat vysokotlaké oblasti, které se pohybují pomalu a především jejich zadní hrany; na počátku podzimu nejlepší období; pobřežní oblasti, velká jezera a údolí řek upřednostňují stratifikace). Dvě nová data: (1) fan. 2-4 srovnává standardní atmosféru (relativní vlhkost 70% až 12000 ft, minimální lom) s typickým poloměrem silné lomu, s jasným poměrem teploty a mix cca 4000 ft (cca 1200 m): je to podpis, který náš výpočet musí rozpoznat na úrovni GFS; (2) skutečné kanály jsou vzácné pod 144 MHz a nejčastější v UHF, podle minimální tloušťky. Užitečná bibliografie QST pro vyhledávání: Hull, "Air- Mass Conditions and the Bending of UHF Waves" (červen 1935, květen 1937); Hoisington, "Pain Prediction of Two- Meter Openings" (říjen 1949); Collier, "Upper- Air Conditions for 2-Meter DX" (září 1955); Botts, "A Night to Remember" (leden 1970, vedena na zemi).

PA0EHG, strana "Tropo" ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): shrnutí troposcatteru z článku G3YGF, již v manuálu. Pro kontrolu našeho výpočtu rozptylu použijte dvě praktická pravidla: asi 10 dB ztráty navíc pro každý úhel na obzoru každé stanice a více než 100 km přibližně 9 dB plus každá 100 km trasa (60 km); společný objem začíná asi 600 m na 100 km a asi 9 km na 480 km. Neléčí kanály, průzkumy veřejného mínění ani refrakční prahy.

PA0EHG, PROPBER kalkulačka ([strana](https://pa0ehg.com/propagat1.htm), propber.exe program poskytnutý autorem programu). Je to 36 kB DOS program napsaný v BASIC a sestavený pomocí Microsoft BASIC Compiler 5.60 (1985): běží pouze pod DOS nebo DOSBox. Z interních textů: žádá o frekvenci, vzdálenost (nebo dva lokátory, také ve starém formátu QRA "E- lokátor"), pro každou výškovou stanici antény, údaj o hluku, pásmo FI, výkon, překážku (výška a vzdálenost) a nárůst antény (0 = výpočet průměrů, účinnost 50%). Vrací maximální vzdálenost v optice, správnou optickou ztrátu pro absorpci páry (0,1 dB / km od 24 GHz), ztrátu troposcatteru "správnou pro místa" (tj. pro úhly horizontu dané překážkami) a poměr S / N v obou směrech, také "s maximálním abnormálním rozmnožováním". Číslicové konstanty nejsou čitelné v sestaveném spustitelném. Navrhovaná metoda pro vysoká pásma je pro nás také užitečná: kalibrace výpočtu na změřeném přímce (rozdíl mezi měřeným a vypočteným S / N) a použití stejného rozdílu na jiné pásmo. Je to stejný vzor našeho výpočtu, ale používá profil S RTM1 místo jedné překážky a GFS předpověď místo pevné marže.

PROPBER, konstanty ve spustitelném formátu (čísla ve formátu Microsoft Binary Format). πre, v tabulce dat vedle textů: 8,68589 (= 20 / ln 10, tj. 20 logů vypočítaných s přírodním logaritmem), 32.5 (volný prostor: 32.5 + 20 log f\ [MHz\] + 20 log d\ [km\]), 24000 a 0.1 plus 40000 a 0,3 (par: 0,1 dB / km od 24 GHz, 0,3 dB / km od 40 GHz) Pravděpodobné, protože opakujete, ale uvnitř kódu: − 69,5 (stejný konstantní termín jako Yehovo boční-Scatter vzorec v VHF / UHF DX Book), 332 (pozemní refrakční), 8733 km (ekvivalentní poloměr, k ³ 1.37). Přesný vzorec rozptylu není rekonstruovatelný bez demontáže programu. T0

PROPBER, formule rekonstruované kódem (demontáž sestavené části; operace jsou volány do BASIC knihovny s operátory v paměti, jejichž hodnoty byly dekódovány ve dvojnásobné přesnosti). Konstanty − 69,5, 332 a 8733, které byly dříve nalezeny, byly v knihovně falešně pozitivní.

- Optická vzdálenost: d = 5 · (Ø h1 + К h2) km (h v m). Je velkorysejší než klasický 4,12 (k = 4 / 3): je ekvivalentní k ņ2.
- Ztráta optiky: L = 32.5 + 20 log f\ [MHz\] + 20 log d\ [km\], plus 0,1 dB / km nad 24 GHz a 0,3 dB / km nad 40 GHz.
- Troposcatter: Lts = L + 23 + 10 log f + 0.0674 · d, tj. 55.5 + 30 log f + 20 log d + 0.0674 · d (vzorec G3YGF s konstantou 2 dB vyšší). Žádný úhel nebo reaktivita.
- Oprava "pro stránky": program požaduje překážku a vzdálenost, ale blok, který by je měl použít, je prázdný; korekční proměnná zůstává 0, takže "správná" ztráta se rovná rozptylu.
- Hluk: T = 290 · (10 ^ (NF / 10) − 1), N = − 228,6 + 10 log T + 10 log B (dBW), bez teploty antény.
- S / N = 10 logů P + G\ _ tx − L + G\ _ rx − N. "S maximálním abnormálním rozmnožováním" používá optickou ztrátu místo rozptylu, tj. ideální spojovací pouzdro bez ztrát mimo volný prostor.

Srovnání: 300 km a 144 MHz PROPBER dává cca 189,9 dB, G3YGF 187,9, naše P.452 187,8 (s nulovým úhlem horizontu): rozptyl se shoduje v rozmezí 2 dB. Náš výpočet přidává úhly horizontu z profilu, teplotu antény a místo "maxima" upevní očekávanou vrstvu zbytkovým pářením a oslabením.

Rohde & Schwarz, P. Denisowski, "Understanding VHF Propagation", Educational Note, April 2023 (24 stran, šíření). Potvrzuje již shromážděné body (vedeno jako dvourozměrný vlnovod, signál, který klesá lineárně s vzdáleností, až 1500 km a dále, trvání od hodin do dnů, dobrá půda na moři a vzácné na zemi, zvýšené z několika set metrů na několik kilometrů). Dvě užitečná pozorování pro index: (1) vývoj v čase: nově vytvořené vedení často nese pouze nejvyšší frekvence a, zahušťování, začíná přinášet i nejnižší; hodinová předpověď by proto měla ukázat 432 MHz "předem" na 144 MHz, když vrstva roste; (2) umístění stanic: nejlepší podmínky jsou mezi stanicemi blízko konce vedení, zatímco ty na cestě vysoké vedení často selhávají, pokud neprovádějí "kdo prohraje". Ventily se pohybují s atmosférickým počasím.

Místo PA0EHG, další stránky. Pro trope, PathProfile of M. Willis G0MJW ([schema](https://pa0ehg.com/srtmp.htm), [download](http://www.mike-willis.com/Software/ASTERPathprofile.zip)): profil trasy z očekávaných celkových ztrátových a kvótových souborů, spolehlivě vyhodnocen pro troposcatter v mikrovlnce; neléčí rozptyl z letadel nebo deště. Užitečné jako druhý odkaz pro porovnání našich S RTM1 profilů a ztrát. Zbytek webu pokrývá EME, mikrovlnná trouba, letiště (AirScout, SM7LCB mapy) a opatření v 80 m.

PathProfile by G0MJW (Mike Willis), ASTER a SRTM balíčky a SiteData (dodávané autorem programu; [](https://www.mike-willis.com/software.html)softwarová stránka). Windows program 2018: profil z ASTER nebo SRTM na 1 "nebo 3", ztráty v optice, difrakce, troposcatter (platné až 1000 km), potrubí a plynné, s procentem času, medián trasy, zlomek cesty na moři a vzdálenost od pobřeží: struktura je, že P.452 / P.1812, ale pomoc (komprimovaný.chm soubor) nemohla být čtena zde. SiteData konvertuje lokátory a dává podíl z GT OPO30.

Nejužitečnější součástí jsou datové soubory: ITU-R P.453 statistické mapy na globální mřížce 1,5 ° (121 řádků od 90 ° N do 90 ° S, 241 sloupců od 0 ° na východ): gradient dN / dz v prvních 65 m překročil po dobu 1, 10, 50, 90 a 99% času; pro půdní kanály procento přítomnosti, tloušťky a intenzity; pro ty vysoké přítomnosti, základní podíl, tloušťka, intenzita a maximální nadmořská výška; plus vlhký termín a teplota. Interpolované roční hodnoty:

| Umístění | Potrubí:% / tloušťka / intenzita | Vysoký ropovod:% / základ / tloušťka / intenzita | dN / dz překročil 10% času |
| Neapol |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | − 380 N / km |
| Tyrhenian (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | - 674 N / km |
| Trapani |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | − 520 N / km |
| Brindisi |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | − 405 N / km |
| Jaderské moře (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | - 399 N / km |
| Milán |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | − 172 N / km |
| Udin |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | - 176 N / km |

Pro náš index jsou připraveny klimatické reference: tloušťka a intenzita typická pro kanály místo 50 m pevné potenciálního potrubí, a anomálie "dnes ve srovnání s normální" na zónu. Údaje ITU: zdarma pro nekomerční použití, před jejich zařazením do veřejné verze. Soubor ZO NES01.DEM (58 MB, 2 'mřížka) obsahuje klimatické zóny vnitrozemské / pobřežní / mořské; formát není zdokumentován a já jsem ho nerozluštil. Záložka Willisových rovnic obsahuje pouze fyzické konstanty, volný prostor a zisk antény.

Čtvrté kolo zdrojů (20. listopadu).

Indermuehle a Suzuki 2026, předpověď plynovodů GFS pro observatoř CSIRO Murchison (arXiv 2608.12861): je to nejbližší zdroj k LIVETROPOVHF. Profily N od GFS do 0,25 ° (povrch do 2 m, pak isobarské úrovně, ty pod vyhořelou zemí); pro každý minimální sloupec dM / dz pod 3 km, základna a horní část potrubí, integrovaný M deficit; pak P.452-16 a P.1812-6 s GLO-90 25 délky od 98 do 1005 km, od 88 MHz do 2,68 GHz, každou hodinu. Výsledky: Střední hodnota AUC 0,72 na 25 kanálech; procento času P.452 p by mělo být použito jako kalibrační konstanta (volba p = 20%), protože model ITU s okamžitým ΔN není definován v potrubí; dovednost zůstává téměř rovná až 3 dny předem. Při 162 MHz (AIS) mají přijaté trasy nejvyšší průtok zlomek 67% (0,26 versus 0,16) a nejvyšší hloubka kanálu 49% (28 vs 19 M), pokud jde o ovládání: na moři je vodič téměř vždy, určuje úsek půdy. GFS vidí polovinu času na zemi ve srovnání s ERA5 (4,5% oproti 10,4% buněčných hodin), zatímco na moři vidí více (73% oproti 56%). Výsledkem je zlepšení indexu "koridoru" (kontinuální potrubí podél trati, vrstvy o minimální tloušťce 40 m): potvrzuje, že kontinuita trasy je správným kritériem.

Longley and Rice, ESSA ERL 79-ITS 67 (1968): metoda pro nepravidelný terén od 20 MHz do 20 GHz (po ITM), s nepravidelností půdy Δh, Ns od 250 do 400, ekvivalentní poloměr 8497 km (4 / 3), horizonty, difrakce, rozptyl vpřed a variabilita v čase a v místech pro klima. Statistická reference, ne okamžitá.

ITU- R P.834-2: poloměr zakřivení, poloměr Země ekvivalent k · a, M = N + h / a (h v m, v tisících km), exponenciální model; nic nového, než to, co již použili.

AGARD CP-70 (1970), troposférické šíření: tenké vrstvy od 1 do 100 m se silnými sklony jsou běžné; odrazivý koeficient vysoké vrstvy je asi úměrný vlnové délce, s náhodnou disperzí asi 14 dB. Měření při 90 MHz: pokles o 9-10 dB vázaný na skok N koncentrovaný na cca 350 m, v horní části vrstvy mraků. Průchod studené fronty eliminuje vrstvy: Difrakce zůstává.

U.S. URSI Report, Commission 2 (1960): průměrná tloušťka vysokých vrstev od 60 do 210 m (vzhledem k tomu, že odpovídá P.453), minimální gradient pro 157-160 N / km.

Buscemi K2OVS, VHF šíření průvodce: typické vzdálenosti pro převrácení a potrubí pro pásmo, kanály spojené s vysokými stacionárními tlaky, maximálně za úsvitu.

Wolinsky- Mancini a 2024, AIS na Kanárských ostrovech: AIS radiozundace a recepce; kanály jsou mnohem častější v teplých měsících (inverze aliseanů).

Nigerijské a malajské články (Ekah, Iloke, Mat, Igwe): místní korelace mezi teplotou, vlhkostí a měřenou ztrátou, slabé a protichůdné (r − 0,09 až 0,27); užitečné pouze jako potvrzení, že samotné pozemní rozměry nestačí.

Herben 1984, práce TU Eindhoven (microonde): 30 GHz na 8.2 km a OTS satelit na 11.6 GHz; jiskra pro turbulenci, multipath a déšť. Dynamické ztráty mikrovln ovládají déšť a turbulence, nikoli kanály.

Baars, The Paraboloidal Reflector Antenna (microonde): zisk z podoblasti G = η · (πD / λ) 2 s typickou účinností 0.5-0,65; ztráta povrchových chyb podle Ruze, ηs = exp (− (4πε / λ) 2). Pomohlo by, kdybychom přidali podobenství jako alternativu k Yagi DL6WU pro 1296 MHz a dále.

Již analyzované nebo špatné obsahy: Hirschmann NASA X-731-67-89 (double), Reidův VHF ST radar (vítr a turbulence, undided), Scribd slide a obecné poznámky na troposféře.

Páté kolo zdrojů (20. listopadu, večer).

Rogers 1997, Radio Science 32 (1): Bayesian odhad parametrů vysokého kanálu (základní rychlost, M deficit, tloušťka) z množitelských faktorů měřených za horizontem, s parabolickou rovnicí modelu; VOCAR experiment v Kalifornii na 143, 263 a 375 MHz, srovnání radiových vln. Signál závisí hodně na základně a M deficitu (oprava 0,81 až 143 MHz pro deficit), téměř vůbec od tloušťky (− 0,17): tloušťku nelze získat z signálu. Pro nás: výpočet B (tloušťka podnebí) je nižší než deficit a podíl; a metoda je základem pro zadní úřad, to znamená získat vedení z více frekvencí.

Moler a Holden 1960, "Troposférické šíření rozptylů a atmosférické oběhy", J. Res. NBS 64D, str. 81 a následující (přečteno v originálu dne 22 / 11 / 2026, doi 10.6028 / jres.064d.014). Kalifornské námořní trasy do Point Loma (San Diego): Santa Barbara 190 mil, Point Mugu 144, San Clemente 78; 1365 MHz a 9380 MHz, ne VKV; leden-duben 1958, průměr 1 minuta. Případ ze dne 26. března: zpevnění přibližně 20 dB po dobu asi 3 hodin směrem k poledne, kdy cyklonický vír slábne a začne mořský vánek a útlum; na 15 je konvergence reformována a signál klesne asi 22 dB od maxima. Statistika (tabulka 1, 500 hPa, 33 případů): při poklesu vzduchu je průměrný signál vyšší než asi 2,5 dB než při vzestupu vzduchu, v pásmu L a X. Signál sleduje rozdíl vypočtený na 500 hPa ve značce a amplitudě změn bez korekčního koeficientu. Nízké vrstvy (do cca 600 m) podporují částečnou reflexi rozptylu. Anticyklony a fronty nejsou ošetřeny a výše uvedené "výlety do 60 dB" nejsou: pocházejí z druhé ruky shrnutí. Pro nás: vertikální pohyb je dobrý prediktor a + 1.5 dB pro subsidenci a − 2 dB pro horský vzduch jsou ve stejném pořadí jako průměrný měřený rozdíl (2,5 dB); 20 dBs je jeden případ, ne typická hodnota, a na mikrovlnce.

Banafaa a Muqaibel 2025, IEEE Access: průzkum potrubí (odpařovací modely, parabolická rovnice, radarové odhady, GNSS a lidar) a klasifikace s automatickým učením o reálných opatřeních; Podpora vektorové stroje dávají nejlepší výsledky. Užitečné jako bibliografie; data jsou z Perského zálivu.

ITU-R P.834-7 (2015): aktualizovaná verze doporučení o troposdivostickém lomu; žádný nový vzorec než ty, které již byly použity.

Abbott, NBS bibliografie na troposypném rozptylu (GOVEPU- C13): již ve třetím kole. Slide Scribd na kanálech: generický obsah. Článek ScienceDirect 0021916987900304 Nelze otevřít.

Šesté kolo (20. listopadu, noc): Bean a Dutton, ARRL Handbook 2022 a 2023, Balanis.

Bean a Dutton, Radio Meteorology (cap. 6), díly dosud nepoužité. Citlivost na Ns: 0,18 dB na jednotku N v ročním cyklu; 0,14-24 dB / N ve denním cyklu (maximálně brzy ráno), až 0,2-1,1 dB / N na trasách o rychlosti 175-200 km v zimě; opravy 0,8- 0,95. Parametr složený z Misme: M = a (g\ _ e − 40 + b · ΔW ^ n) s a = 0,5 dB na N / km, který kombinuje rovnocenný gradient a tepelnou stabilitu ΔW (práce na zdvihání vzduchu 1 km): v Sahaře při 471 MHz na 160 km je jediná stabilita mezi lednem a červnem při téměř stejném gradientu 20 dB. potenciální index lomu K (N hlášen adiabeticky na 1000 hPa): zvýrazňuje hmotnost vzduchu a anticyklonickou subsidenci (Flavell a Lane).

Příručka ARRL 2022 (99a ed.), § 19.4.3- 19.4.4. Užitečné inverze mezi 250 a 2000 m; inverze záření (sera, klidný vzduch, suchá půda) málokdy vedou pravdu; pomalé anticyklony dávají nejrozsáhlejší kanály pro subsidenci, s inverzí mezi 500 a 3000 m, silnější večer a ráno; na severní polokouli jsou nejlepší cesty na jih od vysokého centra. Horká fronta: dobré podmínky pár set km před námi; studená fronta: silná, ale nestabilní zadní strana za přední, paralelní. Foehn (také v Alpách) a zemský vánek (chladný vzduch na zemi, horký na 200-300 m; táhne přes 800 km podél pobřeží). Scirocco: horký a suchý vzduch ze Sahary přes chladnější mořský vzduch, velmi silné inverze z východního Středomoří do Gibraltaru, pravděpodobná příčina záznamu přes 1500 km v mikrovlnné troubě. Troposcatter: teoretický limit cca 800 km, maximální typická polovina. 100. ed. (2023) má kratší text: inverze při východu slunce a západu slunce až do + 20 dB a + 20-50% průtok; 1200 mil na Velká jezera, typický 400-600; doporučuje Radio Mobile (model ITM) pro pokrytí a mapy Hepburn.

Balanis, Antenna Theory (4a ed.), § 10.3.3: Tabulka Viezbicke (NBS TN 688, 1976) pro Yagi optimalizovaná na 400 MHz: boom 0.4 / 0.8 / 1.2 / 2.2 / 3.2 / 4.2 λ → 7,1 / 9,2 / 10,2 / 12,25 / 13,4 / 14,2 dBd. Náš vzorec DL6WU (8.6 + 8 log10 boom / λ) dává 1-1.7 dB méně mezi 0.8 a 4.2 λ a vrací se v souladu s tabulkami DL6WU na 9.5 λ: obezřetný odhad. Se správným vzorcem na originále (9.4 + 7.8 log10 boom / λ, od 22. listopadu) se výmět snižuje pod 0,2 dB mezi 1,2 a 4.2 λ (12.1 dBd až 2.2 λ versus 12.25) a zůstává 0,6 dB až 0.8 λ.

Sedmé kolo (21. listopadu): QST, QEX, W3SZ, úložiště GitHub.

QST 2000-2025 (14 čísel, 10 lůžek s rozpoznáním textu). Pouze zprávy v sekci Svět nad 50 MHz, užitečné jako případy ověření modelu: Září 5-9, 2000, provádí se na 144 MHz a dále na východní polovině USA; 14-15 srpna 2022, otevření Manitoba-Kansas / Missouri pod vysokým tlakem v horkém dni, od 144 do 1296 MHz téměř všechno v FT8, kanadský záznam na 10 GHz 1228.56 km ( VE4MA - N0OY) v Q65, signály až 1941 km; Mapa šíření APPS ukázala stejnou oblast. QST listopad 2022 doporučuje Hepburn mapy. Žádný technický článek o tropo předpovědi.

QEX a K1JT, Franke a Taylor (QEX 2020) v režimech FT4/FT8: FT8 dekódovací prahy − 20,8 dB v 2500 Hz na kanále bez evanescence, − 18,6 dB s evanescencí ve střední výšce; FT4 − 17,5 dB. Ostatní čísla QEX 2023 a 2026 neléčí tropo (tři čísla 2023 jsou nepřečtená skenování). W3SZ 2019: pomalé prahové hodnoty režimu ( JT4A − 23, JT9A − 27, JT65A − 25, QRA64A − 26, FT8 − 21 dB) a kritéria výběru mezi citlivostí a tolerancí kmitočtu.

GitHub. TropoLink (LukiBox, Apache- 2.0): návrh troposcatterových tras s P.617-5 (s N0 a ΔN), NBS TN101 a ITM flanked, a jejich rozdíl ukázal jako nejistotu; P.617-5: L = F + 22 log f + 35 log θ + 17 log d + Lc, F = 0,18 · N0 · e ^ (− hs / 7,35) − 0,23 · ΔN, Lc = 0,07 · e ^ (0,055 (Gt + Gr))). andi-f / troposcatter ( DL2FZN 2013, z VHF / UHF Handbook RSGB): Lts = 24,44 + 4,283 ln f + 0,06656 d nad volným prostorem, sklon u PROPBER (0,0674 dB / km). QRadioPredict: ITM a ITWOM 3.0 s S RTM3, půdní použití Corins a NEC diagramy. Není relevantní: Tropos (Weather app), opera- adt / tropo a troposim (Troposferic delay for InSAR), Tgbot\ _ troposcatter (vojenská stanice), sdr- troposcatter- gui (řídící panel), Troposcatter- Propagation- Lay ( ERA5 delay), Radio- Communication- Project (didaktický simulátor), PyLayers (kanál).

Osmé kolo (21. listopadu): modely NTIA a ITU na GitHubu. NTIA / itm (Longley- Rice, ITS): 20 MHz-20 GHz, ztráta volného prostoru, difrakce a troposcatter s půdním profilem, 0,5-3000 m terminály, reaktivita povrchu N0 (minimalita snížena na hladinu moře, 250-400), sedm klimatů, konstanty půdy a různé způsoby umístění 1 pohled, 2 difrakce, 3 troposcatter; C + + + s.NET rámcem 4.7.2 kryt. Nemodeluje kanály: slouží jako referenční "normální atmosféra" se statistickou variabilitou. NTIA / p528 a eeveetza / p528 (MATLAB): ITU- R P.528-5 pro air- soil contacts, 100 MHz-30 GHz, 1.5-20 000 m terminály, p 1-99%;.NET 4.8 pouzdro. Možné použití: bilance rozptylu vzduchu (dvě pozemní dráhy letadla plus radarový úsek). eeveetza / p2001 (MATLAB): ITU-R P.2001-6, all-field model s difrakcí, troposcatter, abnormální šíření (chování a vrstvy, Lba) a E sporadic, procento času 0-100%; ITU digitální mapy (DN\ _ Median, DN\ _ SupSlope, DN\ _ _ SubSlope, dndz\ _ 01, TropoClim, h0, surfwv, Es) nejsou redistribuovatelné. Možné použití jako porovnání ITU místo P.452, s GFS ΔN místo map jako ve studii CSIRO; mapy by měl uživatel stáhnout podle P.453. Validace 1.3.0 z 593 QSO (29 / 12 / 2021-2 / 1 / 2022): sekvence událostí / porovnání 0,79 bez korekcí, 0,91 s Misme a subsidence; GFS nevidí vedení na moři při 144 MHz (guadagno bez korekcí nikdy více než 15 dB).

Deváté kolo (21. listopadu): IU3QEZ, NTIA, PyWaveProp, ACE High, validace 1.3.1. IU3QEZ (S. Fabris), prezentace "Over the horizont in VKV" (GEKO Magazine n. 69, September 2026): společný objem d2 / (8 · R\ _ eff) s R\ _ eff- 8500 km (1,3 km d 300 km, 3,7 km až 500 km na každou úroveň horizontu v hodnotě 69 mil (111 km), rozpětí 7 dB pro evazenci a 4 dB pro odraz ze země; práce: 10 dB je asi 100 km; stanice IQ3QC 5 W s 3 × 5 prvky, 120 QSO, maximálně 788 km (Alpe Adria 2026). Bibliografie prezentace, která má být navrácena: Norton 1959 (J. Res. NBS 63D, System Loss), Bray 1961, ARRL Antenna Book 2023 obr. 4.29, Rice-Longley-Norton- Barsis NBS TN 101, Bean and Thayer 1959, Barsis and Kirby 1961 (difraction on ridge), Deygout 1966, P.1812, AirScout DL2ALF, P.834 NTIA PropLib (wiki): eHata, ITM, LFMF, P.528, P.2108 (ztráta z hromadné hmotnosti), s C + +,.NET, MATLAB a Python; no conduit model; ITPage: také IF-77 (air- soil) a MPM (1 GHz-1 z). PyWaveProp (Python): Parabolické rovnice metody s Padé aproximací, vícenásobné difrakce hran, PETOOL a RAM kryty, troposné a vysoké kanály, nepravidelná půda: možná základna pro výpočet pole s GFS M (z) profil, které mají být kontrolovány. APRS PropView (Apache 2.0): VHF mapy šíření z APRS balíčků, hodinové statistiky a otevírací alarmy; možný zdroj 144 MHz dat pro validaci. Síť NATO troposcatter net v Itálii (fortifications.net): 900 MHz, 10 kW, 20 m parables; stanice včetně Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Vulture, Monte Lauro; historický zkušební případ pro P.617. Nepoužije se: Gupta 2021 (stratosférická doprava), zákon 2023 (arktický ozon), delfín # 590 a opera-adt / tropo (zpoždění InSAR), tropo.ino (čidla plynu Arduino), tropo- webapi (telefonie), cloudtools / troposphere (AWS). Validace 1.3.1 na 564 QSO tropo: AUC 0,79 (model 1.2), 0,80 (+ měkký kanál), 0,90 (+ misme a subsidence), 0,90 (+ perzistence); barva potrubí (≥ 16 dB) na 46% otevíracích cest a 0% srovnání.

Přidáno do stejného kola. mikelytaev / wave-množení (MIT, Python 3): Parabolická rovnice s aproximaty Padé a Numerov, nepravidelná půda, troposférické kanály; nabít modifikovaný refraktivní M (kvóta, lat, lon) z NOAA GFS a nastavit odpařovací vedení Monin- Obukhov; žádná zdokumentovaná funkce, která vrací ztrátu na cestě. Je nejlepším kandidátem pro budoucí výpočet pole s GFS M profil (to by sloužilo externí služby nebo překlad). RP- Sim (AGPL- 3.0, JavaScript): Empirické modely pro rádiové sítě (Hata, He, Ericsson, dva paprsky), žádné tropo. RIMFading (GPL- 3.0, OMNeT + +): nesrovnalosti toku pro síťové simulace, distribuce Weibull, žádné tropo. PyLayers: vnitřní rádiový kanál, již vyloučen. qradiopredict (kantooon, GPL v2): ITM a ITWOM v3.0 s S RTM3 a Corine, již v sedmém kole jako QRadioPredict.

Desáté kolo (21. listopadu): Antenna Book, Norton, Barsis, nové zdroje dat. ARRL Antenna Book (23a ed.), cap. 4 ( K9LA): Metoda bray (QST listopad 1961) pro rozsah VKV: hladká zemská ztráta 99% (300 mil, 144 MHz: 214 dB), křivky 50% a 99% (obr. 4.11): 195 dB v hodnotě 100 mil až 99% a více než 250 mil až 50%; evanescence ± 7 dB nad 100 mil (− 3,5 dB až 50 mil), SSB 3 dB horší než CW, + 4 dB zemského odrazu, + 3 dB pro spojení dvou antén; zvýšení výšky (15 m: 4 / 3 / 2 dB při 10 / 50 / 100 mil; 24 m: 8 / 4 dB); Yagi asi desetkrát větší (dBi); 50 W stanice s průměrnou anténou: asi 180 dB, 100 mil, + 10 dB až 250 mil. Východ slunce a západ slunce až do + 20 dB a + 20-50% průtoku; horký a suchý vzduch nad vlhkým čerstvým vzduchem na Velkých jezerech až 1200 mil, typický 400- 600; regiony ovlivněné velkými vodními masami nejvíce oblíbené; pozdní zima s vyhříváním schopné silné inverze. Ověření troposcatteru: P.452 až 50% při 483 km a 144 MHz dává cca 204 dB, v souladu s 214 dB Bray na 99%. Norton 1959 (J. Res. NBS 63D, volný na NIST): základní ztráta, systém a cesta antény zisk; Referenční atmosféra Ns = 301; rovnocenný poloměr od Ns (250 → 4878 mil, 301 → 5280, 400 → 6997); L (99) tabulky s 30- foot anténami (100 MHz: 163 dB až 100 mil, 208 dB až 500 mil; 1000 MHz: 138 a 188 dB s 28- foot parably); dlouhodobá variabilita V (p, θ) v závislosti na vzdálenosti úhlu (obr. 13). Rice, Longley a Norton, NBS TN 15 (1959), zdarma na NIST. Barsis a Kirby, NBS Report 6751 (1961): 223 km v Coloradu s Pikes Peak (4292 m) jako hrana, 100 a 751 MHz; konzistentní ztráta se zaoblenou hranou (přibližně 11,5 km poloměr); dlouhodobá evanescence 11,7-14,3 dB proti 39-42 dB rozptylu stejné délky; žádný denní cyklus označen; čtyřpaprskový model poskytl 4-5 laloků, ale pouze jeden je měřen (maximálně na 21 m od země): cesty s horskou překážkou jsou mnohem stabilnější než rozprašovač, užitečné pro spojení "přes Vesuvius nebo Faito". ARRL VHF Digital Handbook: balíček, APRS, D-STAR, WSJT (Scatter Method a EME), žádná část na tropo. Nové ověřené nebo vyhledávané zdroje dat: ECMWF otevřená data (IFS 0,25 °, úrovně 1000, 925, 850, 700 hPa..., bez 975, 950, 900 a 800; G RIB2 s kompresí CCSDS, že náš čtečka nedekóduje, již k dispozici prostřednictvím Open- Meteo); DWD ICON- EU otevřená data (G RIB2 stlačený bz2, soubor pro proměnnou a úroveň: mnoho požadavků); Kanada GDPS na MSC Datamart a GeoMet (cca 15 km, až 33 úrovní; GeoMet umožňuje úrodu oblasti; je základem Hepburn maps); GFFSS NOAA archiv AWS od února 2021 (globální soubory, užitečné pro ověření); APS PropView pouze pro validaci.

11. kolo (21. listopadu): Dlouhá rýže, GEKO 69. Bean and Thayer, "Models of the Atmosphere Radio Refractive Index", Proc. IRE May 1959 (pp. 740 et seq.): model 4 / 3 má správný sklon v prvním km, ale padá příliš vyšší; 30% zakřivení poloměru se vyskytuje v prvním 100 m a 60% v prvním km; průměrný vztah mezi Ns a pokles v prvním km: − ΔN = 7,32 · e ^ (0,005577 · Ns) (Ns = 313 → ΔN asi − 42). Bean and Thayer, "C.R.P.L. Exponenciální referenční atmosféra", J. Res. NBS 63D n. 3 (1959): N (h) = Ns · e ^ (− c · h) s c pevným Ns prostřednictvím stejné zprávy; Průměrné Ns USA 313; sledovací stoly paprsku (celková křivka 15.2 mrad s exponenciálním modelem versus 30.3 mrad se Zemí 4 / 3). MATLAB refractionaxp: c = 0,143859 km − 1 pro Ns = 313 (stupnice cca 6.95 km), platí nad 1 km a 10 mrad. Použití pro nás: "normální" profil srovnání může být, že CRPL s místními Ns místo M = 315 + 0,118 · z pevné, a anomálie poklesu v prvním km by měly být měřeny ve srovnání s − 7,32 · e ^ (0,005577 · Ns) místo -40 pevné. NTIA Report 82-100 (Hufford, Longley, Kissick 1982), ITM guide in area mode: climi and Ns supposed (rovníková 360, subtropická kontinentální 320, subtropická námořní 370, poušť 280, temperate continental 301, temperate marine on land 320, temperate marine on sea 350); Ns = N0 · e ^ (− hs / 9,46 km); používáte měsíční minimum Ns, protože méně ovlivňuje superrefraction a subrefraction; Ns = 301 odpovídá ae = 8497 km; pod 100 km Ns se počítá málo. Rýže, Longley a Norton, NBS TN 15 (1959), část I: předpovídání vzorce distribuce v čase po zemi a troposférické vlny, základ TN 101. SoftWright, Wikipedia, Altair WinProp a MATLAB rfprop. LongleyRice: stejné informace (verze 1.2.2, 20 MHz-20 GHz nebo 40 GHz, 1-2000 km, Ns 301, sedm podnebí, variabilita času, místa a situace); nikdo neformuje vedení, pouze statistický účinek mořského podnebí ("vyšší pole po 10% času nebo ne"). GEKO Magazine n. 69 ( IU3QEZ): radio horizont 4 / 3 130 km od 1000 m, 260 km přidávání dvou stanic; troposcatter se společným objemem mezi 1-3 km a 10 km; Scatter z letadla na 10 km s + 10-20 dB pro desítky sekund nebo několik minut; teoretické kurzy s 2 × 10 prvky na stranu: SSB QRP asi 440 km, SSB 50 W asi 510 km, CW QRP asi 600 km, SSB 500 W asi 640 km (500 W v SSB dělat 40 km více než 5 W v CW); na 90-99% času rozpětí 10-20 dB by měl být odstraněn; 5-10 dB za horizont na dlouhé úseky; kvóta se počítá na volný horizont, nepřibližuje ke společnému objemu; Alpe Adria 2026: 99% z QSO troposcatter, téměř ne po vzduchu; 5-10 dB za horizont; 5-10 dB za horizont za horizont na dlouhé úseky; kvóta; kvóta na volný horizont;

## 12. Kulatý stůl: zdroje pro potvrzení modelu nebo jeho opravu (21. listopadu)

Shrnutí všech závitů zdrojů, objednáno k použití. "Stav" říká, zda je zdroj již v programu (1.3.1), zda je v testu nebo zda má být proveden.

### Data k ověření (co se opravdu stalo)

| Zdroj | Co se děje? | Použití | Stát |
| MMMonVHF zprávy od autora | 593 QSO při 144 MHz, 29 / 12 / 2021-2 / 1 / 2022, čas a lokátory | Ověření události proti porovnání (+ 14 dní) | při použití: AUC 0,79 (1.2) → 0,90 (1.3.1) |
| Svět nad 50 MHz | zdokumentované případy (14-15 / 8 / 2022 Manitoba-Kansas, 144-10 GHz) | historický případ | používá se jako historický případ; přidat k validaci QSO |
| wspr.live (WSPR) | kompletní archiv, 2 m a 70 cm, SQL | mnoho událostí a mnoho "ne" (hodiny bez místa) | vyzkoušel, by měl být filtrován (bod false na 16-20 tisíc km) |
| APRS PropView / APRS-IS | stanice plsti, vzdálenosti, hodiny | pouze validace 144 MHz | dělat |
| Reverse Beacon (archiv) | denní spot | VHF | Vyřazeno pro tuto chvíli |
| I GRA2 radiosondaggi (Brindisi 16320, Practice of Mare 16245, Trapani 16429, Udine, Milán) | Naměřené profily 00 a 12 UTC | Ovládání a inverze profilu GFS | nalezené soubory (Brindisi 46 MB), které mají být použity |
| Barsis a Kirby 1961, Barsis 1963 | evanescence měřená na cestách s horskou překážkou | Potvrdit kategorii "monte" (Vesuvio, Faito) | dělat |
| Norton 1959, NBS TN 15, TN 101 | L (99) a ztráty stolních médií | Ovládání rozptylu | částečně (viz 300 mil konzistentní) |

### Opravy navržené zdroji

| Oprava | Zdroje | Vliv naměřený při validaci | Stát |
| Misme Stability Termín | Bean a Dutton čepice. 6 | AUC 0,79 → 0,90 spolu se subsidencí | v 1.3.1 (hlava 16 dB bez vedení) |
| Poddajnost od svislého pohybu | Moler a Holden 1960 (přečteno v originále 22 / 11: + 2,5 dB průměr mezi příchodem vzduchu dolů a stoupajícím vzduchem, 1.4 a 9,4 GHz na moři) | výše uvedené | (+ 1, 5 / − 2 dB) |
| Měkký ropovod (λ do 3 · λmax) | Kerr, Hepburn, VKV / UHF DX Book, Shi 2019 | samotný + 1% AUC; přináší barvy potrubí na 46% tras události a 0% srovnání | : |
| Vytrvalost 24 h | VHF / UHF DX Book, Biancomano a Leyshon | AUC nezměněna, více přijata a více falešných poplachů | v bodě 1.3.1 |
| Normální profil CRPL s lokálními Ns (− ΔN = 7,32 · e ^ (0,005577 · Ns)) místo 40 N / km pevné | Bean a Thayer 1959, NBS Monograph 4 | testováno ve validaci | v 1.3.2 jako alternativa, vypnuto vadou |
| práh lomu 79 N / km (ITU) místo 100 (Bean a Dutton, vrstva 1000-850) | P.453, Lopez 2008, DX Book | testováno ve validaci | v 1.3.2: agresivní úcta vedle obezřetnosti (mapa používá obezřetné) |
| Klimatologická tloušťka (calcle B) méně důležitá než deficit | Rogers 1997 | zkusit: použít A pro zisk | dělat |
| Parabolická rovnice v profilu GFS | PETOOL, wave- propagace (Lytaev), Levy (kniha není přečtena, pouze náhled), Lindquist | první verze s polem nad horizontem, jemuž dominuje numerický hluk (AUC 0.12-0,28): správná (doména 8 km, paprsek 6 °); po korekci AUC 0.54-0,66 | při použití jako druhý výpočet, nerozhoduje o prognóze |
| Nespavost: ± 7 dB nad 100 mil, rozpětí 10- 20 dB až 90- 99% | Bray 1961, Antenna Book, GEKO 69 | prahové hodnoty způsobů v okrajových podmínkách | v použití: pravděpodobnost periody a prahu FT8 − 18,6 dB v troposcatteru |
| Společný objem a rovnocenná vzdálenost Bray | IU3QEZ, Bray 1961 | Informace | : |
| Troposcatter P.617 a RSGB jako nejistota | TropoLink, DL2FZN | Informace | 3. 0 |

### Co zbylo z modelů

Vedl na moři v nízké nadmořské výšce (100- 200 m), že úrovně GFS nevidí (studie současných zdrojů dat); reflexe a difrakce na nedalekých horách (Vesuvio, Faito): návrh výpočtu "přes Monte". ITM / Longley- Rice zůstává statistickým referenčním "normálním dnem" (Ns 301, climi), bez vedení. Vyřazeno rozhodnutím autora: AirScout, P.528), E sporadic (P.2001) a 50 MHz (servoir EIRP, které málokdo mají a pletou se E sporadic).

### Návrh pracovního řádu

1. Rozšířená validace: přidejte filtrovaný WSPR (i hodiny bez místa) a QST 2022; opakujte pro 432 MHz.
2. Zkuste normální CRPL profil a ITU práh 79 N / km, jeden po druhém, udržet pouze to, co zvyšuje AUC.
3. Porovnání profilů GFS s rádiovou vlnou I GRA2 Brindisi a Practice of Sea ve dnech QSO.
4. Přehodnotit parabolickou rovnici po numerické korekci.

### Rozhodnutí ze dne 21. listopadu (verze 1.3.2)

- Přijato: uvážlivý odhad (Bean a Dutton, 100 N / km) a agresivní (ITU, 79 N / km) překrytý; kanály žádané pouze pod 2500 m (první 1500 m, zvýšené po ověření); bonus s vysokým tlakem a slabým větrem; hmotnost moře s μ1 z P.452; podíl stanic s ohledem na vedení také na částečné vedení; hraniční vrstva nad hřebeny ( IT9GSF).
- Přijato jako alternativa, vypnuto vadou: normální CRPL profil s místními N.
- Správná parabolická rovnice číselně: AUC od 0, 12 do 0, 54 (30 m anténa) a od 0, 28 do 0, 63 (300 m).
- Pozemky: Koperník DEM GLO-30 místo Terrain Tiles "skadi", které v blízkosti pobřeží mají přízračné reliéfy 80-100 m v moři (Opernicus IC8TEM, ověřený bod podle bodu na Capri); Skadi zůstává jako rezerva.
- Znaky počasí na mapě s jejich účinkem: ↓ příznivé podsidence (Moler a Holden 1960, v průměru asi 2,5 dB mezi vzduchu přicházející dolů a vzduchu stoupající; + 1,5 dB), ↑ vzduch do kopce nepříznivé (hluboké upevnění; − 2 dB), S strike příznivé vodítko (ARRL Handbook § 19.4, silné inverze na Středomoří; žádná korekce). Dvojitý symbol, zelené / červené / oranžové barvy a komiks na mapě.
- Přijato ITM 1.2.2 (NTIA / itm, public domain), přeložen do C #: podle výpočtu základní hodnoty v normální atmosféře vedle P.452 a závěrečný úsek po výstupu z potrubí, které končí před korespondentem. Vyřazený kód qradiopredict, protože pod GPL v2.

Bez užitečného obsahu pro index: * Komunikace čtvrtletně *, podzim 1991 (žádný tropo článek; Atkinsův článek o Scatter je v zimním vydání 1991).

- Přijato jako princip manuálů (21. listopadu): "propagace nelze zobecnit, ale studovat ve vztahu k místní morfologii" ( DF5AI, hlášený IK3XTV v "propagace na 2 metry"); citováno v manuálu prognózy (§ 1) a v radách.
- Přijato (21. listopadu, po 1.3.3) interpretace odpoledních transalpinových otvorů IK3XTV: ne "konvekční buňky, jako jsou čočky", ale troposcatter zesílený turbulencí na horní část vrstvy znovu přijat, když dosáhne podílu na společném objemu (manuál předpovědí, § 8 ter). Od 1.3.4 vstupuje do výpočtu bodového bodu jako prozatímní výztuž troposcatteru, maximálně 3 dB. Vyřazeno stejnými články: "autoamplification" signálů za Alpami (pasivní médium se nezesiluje: popsané odkazy zůstávají vysvětlitelné difrakcí a troposcatterem; deklarované pravomoci se nepoužívají jako data), sníh, který "zlepšuje difrakci" (pravděpodobnější je stabilní vrstva nad sněhem, s menší evazencí).

- Kerr (1951) přezkoumán na knize (22. listopadu, PDF poskytnuté autorem): § 1.5, eq. 17 a 18 a tabulka 1.1. Čísla eq. 18 jsou nečitelná, ale tabulka 1.1 potvrzuje: 20 ft → 1 cm, 80 ft → 10 cm, 400 ft → 100 cm, 2000 ft → 1000 cm, proti 1,25, 10, 112 a 1252 cm λmax = 0,014 · d ^ 1.5 (λ v cm, d stojící; kulatý stůl, "orientační hodnoty". Kurrovy hypotézy: uvnitř potrubí je upravený index 4 · 10 − 8 na stopu, typická hodnota nízkých kanálků; se slabšími sklony je potřeba větší tloušťka. Kerr také varuje, že λmax není čistý řez: vlnové délky někdy déle mohou být ještě silně ovlivněny kanálem.

- DL6WU přezkoumáno na originál (22. listopadu, PDF VKV Communications 3 / 1982 poskytnuté autorem): G. Hoch, "Extrémně Long Yagi Antennas", str. 130 a později. Obr. 1: zisk na dipólu měřený na 432 a 1296 MHz v profesionálním polygonu německých příspěvků, se vzorkovou anténou EIA od 7,7 dBd, odhadovaná přesnost 0,5 dB; 2,35 dB pro dvojnásobek délky boomu, bez nasycení do cca 20 λ. Použitý program 8.6 + 8.0.log10 (L / λ) dBd, tj. asi 0,8 dB v méně než 1-2 λ; nyní používá 9,4 + 7,8 · log10 (L / λ) dBd (gradient je zadaný, hodnota 1 λ se čte na grafu, ± 0,3 dB). Vyřazený vzorec 10 · log10 (7,8 · L / λ) cirkulovaný na síti: by dal 3 dB pro zdvojnásobení, oproti 2,35 měřeno.
- Booker a Gordon (1950): odkaz potvrzen "This Week 's Citation Classic" (Aktuální obsah, 8. února 1982, komentář H. G. Booker): Proc. IRE 38, pp. 401-412. Komentář potvrzuje dva body použité v příručkách: pole obdržené za horizontem s Rayleigh distribuce, a exponenciální autokorelační funkce nesrovnalostí (non-Gaussian). Původní článek nebyl přečten.

## 13. Z IK3XTV článků

Dva články od Flavio Egano IK3XTV (Radio Rivista: "Propagace na 2 metry" a "Propagace VKV - konvekční buňky a gravitační vlny") přidávají dvě věci, které modelové vedení nepokrývají: transalpinové otvory odpoledne v důsledku konvekce, a praktická pravidla o tom, kde a kdy jsou vedení tvořena v Itálii. Jsou to pozorování ze stanice, ne opatření: musí se s nimi zacházet jako s hypotézami pro kontrolu spotů.

| Pozorování autora | Jak se stane složkou indexu |
| Kanály jsou tvořeny hlavně mezi 100 a 1000 m, zřídka vyšší; slouží vysoký tlak na široké oblasti, dobré insolace a absence větru | vyhledávání potrubí pouze pod 1500 m; bonus s vysokým tlakem a slabým větrem v nízkých vrstvách |
| V teplých měsících provádí opakující se na Jadranu mezi Veneto a Puglia, 700-800 km, někdy až do Řecka; mořské trasy jsou oblíbené | větší hmotnost na zlomek námořní trasy (např. μ1 ITU-R P.452) |
| Odpovídající musí být v potrubí nebo v jeho blízkosti; s vysokými anténami a několika metry vedení signál nevstupuje | porovnat podíl základny vedení s podílem obou stanic (bude sloužit nadmořské výšce QTH) |
| Stejný čas, stejné vedení: poslouchat z Gargano stanice v 144 a jeden v 50 MHz | zkontrolovat: podle tabulky Hepburn 50 MHz požádat o více než 300 m vedení, takže událost by naznačoval potrubí velmi často |
| Připojení Veneto - Jižní Německo za Alpami, bez vizuálů, téměř vždy odpoledne a s vysokým tlakem; nestabilní signály, které zmizí a znovu objeví po několika minutách | "konvexní" index oddělený od potrubí, pro horské trasy |
| Konvekční buňky se rodí na zemi a stoupat asi 1 km každé 3 hodiny: v 15 oni dosáhnou asi 3 km, nadmořská výška, která slouží k ovládání Alp | použít mezní výšku vrstvy (hranice\ _ layer\ _ výška Open- Meteo GFS) podél cesty: skóre, když překročí nadmořskou výšku hřebenů |
| Alpy vytvářejí gravitační vlny, které spolu s konvektivními pohyby vytvářejí nesrovnalosti indexu lomu. | hypotézy: silný vítr kolmý na řetěz na 700 hPa jako signál zlaté vlny; vyzkoušet poslední |
| Velmi vysoká inverze (až 8 km ve výjimečných případech) dává asi 800 km, ale v praxi zřídka více než 300 km | podíl inverze rozšiřuje rozsah: užitečné údaje pro odhad čekací vzdálenosti |
| V létě poloměr Země odpovídá asi 16000 km (2,5 krát reálný), s asi 60 dB v méně než 300 km ztráty | v souladu s k50 = 157 / (157 − ΔN): k = 2,5 odpovídá ΔN asi 94 N / km, to je superrefrakce |
| Více než 3000 km pouze v rozmezí 20-30 ° severně a jižně od rovníku, nad mořem, spojené s Hadleyovými buňkami; vrchol v srpnu v Africe-Evropa | mimo Evropu: užitečné nedávat vysoké indexy na Atlantik bez zpětné vazby, a pro Kanárské trasy - Velká Británie |

Další užitečné údaje ze stejných článků:

- Optický rozsah mezi dvěma h1 a h2 metry (d v km): d = 4,1 · (Ø h1 + К h2), až do cca 20% pro normální lom.
- Neklid: pomalý a mělký s inverzí; silný a rychlý s rozptylem a konvekční buňky. Program by ho mohl použít k klasifikaci reklam, pokud jednoho dne bude mít SNR zprávy v průběhu času.
- Pro sporadické E v 144 MHz autor uvádí práci DF5AI a Davis a Johnson (Nature 435, 2005) na vazbu mezi temporals a Es: z tropo, ale samostatný indikátor zítra by mohl použít blesky data.
- Pozemní reflexe ve skoku E: moře K = 81 a 5 S / m, krajina a lesy K = 13 a 0.006 S / m, městské oblasti K = 3 a 0.001 S / m.

## 14. Hypotézy indexu LIVETROPOVHF

Počáteční návrh, který je třeba projednat: index 0-10 ve stejném měřítku jako Hepburn, vypočítaný pro každý bod mřížky a každé 3 hodiny od GFS (pak ECMWF a ICON pro srovnání), a druhé číslo na trase, které říká, jak velká část trasy prochází do užitečného potrubí do zvoleného pásma. Váhy níže jsou moje předpoklady, které mají být kalibrovány s skvrny.

Recenze po Bean, Dutton a Kerr: Profilové prahy pocházejí z jejich tabulky 6.5 a stupnice z jejich měření, takže refrakční vrstva má hodnotu 10- 25 dB nad normálním profilem. Index se pak nejprve vypočte v dB a poté se nastaví na 0-10; hodnoty v tomto nůžkovém materiálu se musí kalibrovat spoty.

Výpočet podle bodu, krok za krokem

1. T výboj, relativní vlhkost, geopotenciální výška a vertikální rychlost 1000, 975, 950, 925, 900, 850, 800, 700, 600 a 500 hPa, plus T a 2 m vlhkost, zemní tlak, výška vrstvy a vítr 10 m.
2. Zlikvidujte úrovně pod zemí; výpočet N a M na každé úrovni (ITU-R P.453).
3. Výpočet − dN / dh v každé ze čtyř vrstev Bean a Dutton (1000-850, 850-700, 700-600, 600-500 hPa) a graf jako subrefractive, normální nebo superrefractive s prahy jejich tabulky; uvnitř první vrstvy také použít meziúrovně najít nejsilnější gradient a jeho podíl.
4. Tam, kde dM / dh < 0, najdu základnu a horní část vedení, tloušťku d a deficit ΔM.
5. Porovnání s minimální tloušťkou pásma ze vzorce Kerr (asi 180 m pro 144 MHz, 89 m pro 432 MHz, jako v Hepburn tabulce); pod prahem potrubí stále počítá, ale méně.
6. Podporuji zisk v dB s následující tabulkou (vezmete nejlepší vrstvu, ne součet vrstev, plus termín anomálie) a převedete jej: index = dB / 2.5, mezi 0 a 10, takže 25 dB má hodnotu 10. Negativní hodnota (sub- refraction) je uvedena jako 0, ale zůstává v datech cesty.

| Situace v nejlepší vrstvě | Kritéria | Odhadované výnosy |
| Běžné (nelaminované) | − dN / dh v rozmezí karet. 6.5 (např. 20-60 N / km mezi 1000 a 850 hPa) | 0 |
| Mírně nad normálem | mezi horní hranicí normálního pásma a prahem nadlomu | 0 až 10 dB v poměru |
| Superlom | nad práh vrstvy (100, 80, 70, 50 N / km), ale pod 157 N / km | od 10 do 15 dB, v poměru do 157 N / km |
| Příliš tenký plynovod pro pásmo | dM / dh < 0, tloušťka níže Prahová hodnota jílu | 15 |
| Užitečný konektor pro pásmo | dM / dh < 0, tloušťka nad prahem Kerru | 20 dB, + 1 dB každých 5 M jednotek deficitu, maximálně 25 |
| Sub- lom | − dN / dh < 0 v dolní vrstvě | − 10 dB |
| Anomální pozadí (přidáte) | Model Ns minus ITU mapa klimatologické N pro tento měsíc | 0,2 dB na jednotku N, mezi − 5 a + 5 dB |

Výpočet podle trasy (mezi dvěma lokátory): vzorek maximální kružnice každých 10 km, stejně jako již program pro index na cestě, a vypočítat zlomek bodů s užitečným kanálem a průměrný podíl základny. Studie CSIRO zjistila užitečnost zlomku trasy v potrubí (na trasách AIS bylo o 67% vyšší než v kontrolních případech).

Přidání výpočtu na trasu (od LA0BY a W3SZ): (1) mořské stanice ve srovnání se základnou a horní částí vedení, protože vysoké vedení slouží pouze těm, kteří vstupují; (2) troposcatter "podlaha" s metodou Yeh, s výchozími rohy převzatými z profilu země; trope zisk v dB je odstraněn z této ztráty a výsledek je srovnáván se ztrátou, kterou stanice zvládne překonat (tabulka KK6MC). Index se tak stává také odhadem dosažitelné vzdálenosti.

Opravy vyzkoušet jeden po druhém (od Bean a Dutton a LA0BY, IK3XTV, G4MCU již v manuálu), aby pouze v případě, že zlepší srovnání s místy:

- Míchání (importante důležité podle autora, podle denního pozorování): ze dne na den na zemi, mezi půlnocí a odpolednem s jasnou oblohou, nebo s větrem na zem přes několik m / s, kanály k zemi zmizí; jejich příspěvek musí být snížena. Ne na moři.
- Čas dne (importante důležité podle autora, podle denní pozorování): Irraggiamento kanály vznikají při západu slunce a padají po východu slunce, je to 3hodinová předpověď řízení.
- Svislý pohyb: vzduch stoupající na 850- 700 hPa remixuje vrstvy (negativní korekce), vzduch sestupuje pod anticyclone posiluje je (pozitivní).
- Trend tlaku: tlak do kopce na cestě v posledních hodinách, vyšší a stabilní signál; v poklesu, nižší signál (bastano 2 hPa).
- Synonyma: vysoký tlak nad 1025 hPa, teplota na 850 hPa mnohem vyšší než na zemi, horký a suchý vzduch tekoucí na studeném moři, cesta na moři.
- Horské stezky: druhý "konvexní" index (sekce IK3XTV), s odpolední limitní vrstvou vyšší než hřebeny podél cesty a vysoký tlak.

Z VKV / UHF DX Kniha, další opravy vyzkoušet: po sobě jdoucí dny vysokého tlaku na místě (více dní, více bodů), pozice ve srovnání se středem anticyklonu (bonus na hranách), tlak gradient na zemi jako míra větru, silný déšť v 24 hodin před (penality), nízké mraky na okraji vrstvy (penalita pouze pro vysoké pásky). Pro směr tras: porovnejte azimut trasy s azimutem isobare.

Ověřování

- Archiv LIVETROPOVHF: každá reklama přes 500 km do 144 nebo 432 MHz je "událost". Srovnává index na trase s frekvencí událostí ve stejnou denní a měsíční dobu (stejný přístup studie CSIRO).
- Porovnání s Hepburn a F5LEN ve stejné dny: kde se tři indexy odpojí, vypadáte, kdo měl pravdu podle bodů.
- Radiozundace 00 a 12 UTC: zkontrolovat, zda model vidí inverze, které sonda měří.
- Historické případy LA0BY (6-11 / 11 / 2003, 10 / 12 / 2004, 15 / 10 / 2011, 11 / 2 / 2015, 22 / 11 / 2018): index přepočítaný v těchto dnech musí vidět kanály, které Hepburn neukázala.
- Mezní hodnoty musí být uvedeny v manuálu: vedeny pod 100- 200 m téměř neviditelné pro tlakové úrovně GFS; spolehlivá předpověď pro 2-3 dny, pak pouze trend.

## 15. Zdroje z 21. listopadu (druhá část)

- ITU- R P.1812 (MaTLAB / Octave implementation of I. Stevenovic, OFCOM; GitHub eeveetza / p1812, Zenodo 10.5281 / zenodo.6141558). VHF / UHF area- specifická metoda trasy, odvozená z P.452: difrakce "delta- Bullington", troposcatter, kanály a reflexe na vrstvách, zatížení na zemi. Užitečné jako třetí srovnání vedle P.452 v point-point výpočtu; ΔN a N0 data jsou plné produkty ITU, takže uživatel stáhne je (podle P.453).
- z Costa, Rabanal Ramirez, Dias, IET MAP 2018 (doi 10.1049 / iet-map.2017.0992). 563 MHz, 27 bodů mezi 5 a 30 km na nepravidelném terénu s lesem: P.526 s vegetací 12 m − 4,3 dB a δ 8,8 dB; P.1812 s zatížením 10 m + 2,6 dB a δ 8,9 dB; dielektrický klín − 4,8 dB a δ 9,2 dB; P.1546 + 5,0 dB a δ 11,7 dB. Pro program: zbytková chyba dobré difrakční metody je asi 9 dB a výška zatížení na hřebenech (10-12 m) se počítá.
- PETOOL (Ozgun e altri, Communications Computer Fyzics 182, 2011). Parabolická rovnice split- krok na jednu a dvě cesty s širokým úhlem propagátor (úhly až 40-45 °, proti 10-15 ° úzkého); spodní se ztrátami přes transformované Fourierovy smíšené; zem na kroky. Potvrzuje volbu 1.3.2 (široký úhel, půdní koeficienty) a označuje dvoucestný výpočet pro strmé reliéfy.
- MathWorks. `tropopl`: ztráta plynů a ztráta "lente" v vrstvené atmosféře (P.835, P.676, P.453), bez kanálů: s gradienty pod -157 N / km, geometrický optický model není platný. Příklad radarového šíření používá pro lom − ΔN = 7,32 · e ^ (0,005577 · N), stejný CRPL report již v programu. `propagationModel` Longley- Rice: předem definované horizontální polarizační hodnoty, vodivost 0,005 S / m, propustnost 15, reaktivita 301, mírné kontinentální klima. Žádný model MathWorks se nestará o vedení.
- ITU-R, digitální výrobky skupiny 3. Údaje a programy P.453, P.452, P.1812, P.2001, P.528, P.1546; "integrovaná" data jsou součástí doporučení; žádná deklarovaná licence k redistribuci: nadále je necháváte stáhnout uživateli.
- ESA Navipedia, troposférické zpoždění. N = Nidrostatica + Numida; suché zenitální zpoždění je asi 2,3 m a vlhké desítky cm, které se rychle mění. Pro nás: mokré zpoždění odhadované stanicemi GNSS je nepřímým měřítkem integrované vlhkosti, možného budoucího zdroje nyní-casting; Dnes není použitelné.
- Bray, K2LMG, "A Method for Determining V.H.F. Station Capacities", QST November 1961, pp. 36-40 (read in original; rechecked on 22 November on the PDF of the number of QST: all values below response). Autor, D. W. Bray (General Electric, Ithaca), říká, že ověřil předpovědi s měsíci plánovaných spojení s K2GOI a W4LTU. Úhyn v době 13,5 dB nad 100 mil (7 dB pod a 7 nad hodinovým průměrem), téměř lineární pod (3,5 dB až 50 mil): pro pevné spojení odstraňte 7 dB. Oprava horizontu: každá nadmořská výška horizontu má hodnotu 69 mil (111 km) větší vzdálenosti (menší pokud záporné). Zisk yagi asi 10 × boom / λ (poměr výkonu), více asi 4 dB odrazu půdy; SSB − 3 dB a AM − 7 dB ve srovnání s CW; 99% hodin: asi 180 dB až 60 mil, 190 až 90, 200 až 240 (2 m). Od 1.3.2 je pravděpodobnost, že se po dobu použije λ = 3,4 dB (± 7 dB jako ± 2∞) místo 5 dB odhadnuté druhé ruky.
- Výpočet Two- way (zkuste s prototypem, 21. listopadu). Velký úhel parabolické rovnice s reálným S RTM1 terénem z Capri, 144 MHz, anténa 10 m od země, standardní atmosféra; míra: průměrný výkon mezi 20 a 400 m až 80 km na trase k odpovídající. Od centra Capri a Marina Grande je západ uzavřen (překážka na + 11-21 °, Monte Solaro): Směřováním Faito (téměř za EA5 a EA6, 150-160 ° mimo osu) se signál vyslaný do Španělska zvyšuje o 10-29 dB; ukazuje Vesuvius (110-125 ° mimo osu, pak mimo výpočet, pouze orientační) o 17-25 dB. Od Anacapri je západ volný a přímé vítězství o 8-22 dB. Absolutní ztráty na 1000- 1300 km prototypu však nejsou spolehlivé. EA3 EA5 EA6
- Barsis a Kirby, NBS Report 6751 (1961), Pikes Peak, 223 km, 100 a 751 MHz. Hora je společným terminálem dvou optických tras: distribuce hodinových mediánů je konvolucí těch dvou tras. Dlouhotrvající exkurze (1-99%) cca 14 dB, oproti 26-40 dB troposcatterovy dráhy o stejné délce nebo úhlu; žádná výrazná změna dne. Signál je silnější než troposcatter, ale slabší než ideální hrana: s vrcholem by se mělo zacházet jako se zaoblenými (zhruba 11,5 km). Vysoké signály jsou vždy pozorovány za hřbety.
- Barsis, NBS Report 7652 (1963), tři trasy přes horu (Colorado, Aljaška, Evropa), 750-900 MHz. Průměrná ztráta 10- 20 dB vyšší než Fresnel- Kirchhoff výpočet bez půdy v blízkosti terminálů; evanescence je odhadnuta tak, že obalije dvě čáry optikou; odrazivosti půdy v blízkosti terminálů zhoršují delší úpadek. Od 1.3.2 vstup do potrubí přidává zaokrouhlení P.526.
- Barsis and Johnson, NBS TN 88 (1961). Prodloužené rozpuštění vesmírné vlny, dokonce i na cestě za horou (§ 5).
- Norton, "System loss in radio wave profilation", J. Res. NBS 63D (1959). Křivky, z nichž Bray dostane nomogramy; definice ztráty systému, přenos a základní, proces antény zisk.
- Rice, Longley, Norton a Barsis, NBS TN 101 (1965-67), Volumes 1 a 2. jednohranná a překážka zaoblená a bez odrazek (§ 7, s příkladem Pikes Peak), dlouhodobá variabilita V (0,5) a Y (q) pro klimatické podmínky (temperované kontinentální a jiné). Pro nás: metodou zaoblených překážek a variabilních funkcí je odkaz na kalibraci nejistoty.
- ITU-R P.526-16 (11 / 2025). Vzorec (31) J (ν), stejný jako v programu; zaoblená překážka (32) - (36), nyní použitá; multi- split metody (Bullington a plná metoda § 4.5).
- ITU- R P.1812-8 (2025) a -7. Kompletní obnovený text: specifická metoda 30 MHz až 6 GHz (delta- Bullington, troposcatter, kanály a reflexe na vrstvách, zatížení). -7 varuje, že jeho troposcatter (z P.617-5) je špatně pod 66 km. Je to možné třetí srovnání.
- Paulus ", Practical application of the IREPS waration model", NOSC TR 966 (1984), and Radio Science 20 (1985). Zpráva je přečtena na 22 / 11 / 2026 (DTIC ADA146528, scan s uznávaným textem, někdy nečitelný). Vzduchomořské rozdíly měřené loděmi se posunují směrem ke stabilitě: teplejší mořský vzduch v 31-35% údajů o lodích oproti 0,7-7% kanců NOAA, pro psicrometry narušené lodí a SST zachycené chladicí vodou (přibližně + 1 ° C). Jeske model pak dává 40 m a více než 15% roku a výšky, které skok z 10 na 40 m za 24 hodin se stabilním časem, není potvrzeno radiovými opatřeními. Navrhovaná korekce (testované prahy 0, - 0,5 a - 1 ° C; − 1 ° C je lépe odsouhlasena s radiovými měřeními z června 1982): je-li rozdíl vzduchu − 1 ° C nebo menší, použije se měřená hodnota; jinak vypočítáte výšku s − 1 ° C a s 0 ° C, a pokud je při − 1 ° C nižší, držíte ji. Zvýšení na stabilní straně jsou řezány, klesá zůstává. V červnu, Marsden 120 náměstí: průměrná výška od 16,8 m (klimatologie) do 7,6 m. Rovnice modelu nejsou ve zprávě (viz Jeske a Hitney). Program používá neutrální profil, který nemá stabilní nadsázka, a omezuje δ na 40 m: jde ve stejném směru; s modely SST (ne loď), měření vady, že Paulus opravuje váží méně.
- Levy, "Parabolic Equation Methods for Electromagnetic Wave Propagation" (IEE 2000). Na Google Libri je pouze náhled: Já nečtu.
- VK3FS (3fs.net.au). Zavedení kvality: vysoké kanály na 450- 2000 m pro delší otvory na zemi; inverze 300- 600 m v klidných a klidných nocích, které zmizí po východu slunce; silnější pobřežní inverze v odpoledních hodinách; 800- 1300 km typické, 1400- 1600 km často vedeny. V souladu se záznamy o dni / noci a stropem 2500 m.

## 16. Zdroje

Pravidla a vzorce

- [ITU-R P.453-13 (2017), index radiolomu:](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)vzorec a data
- [ITU-R P.452-18 (2023), předpověď rušení pozemních stanic nad 100 MHz: ekvivalentní poloměr, β0, troposcatter,](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)kanály
- [ITU- R P.617-5, šíření za horizontem pro rádiové mosty](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)
- [Shi et al., "Komplexní studie o vlnové délce elektromagnetické propagace v různých únikových kanálcích", IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

Studie o předpovědi kanalizace s modely počasí

- [P. Lopez, "A 5- year 40- km resolution global climatology of superrefraction for ground-based meterology radar", ECMWF Tech. Memo 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO), "Troposférická indukční predikce založená na modelových datech GFS na... CSIRO Murchison Radioastronomie", Observatory arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (FMI), "Effects of model resolution on predicting atmosferical ducting conditions with Harmonie-AROME", EGUsphere 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

Radiové předpovědi amatérů

- [W. R. Hepburn, "The Hepburn Tropo Index (HTI)"](https://www.dxinfocentre.com/propagation/hti.htm)and [](https://www.dxinfocentre.com/propagation/history.htm)method history
- [W. R.](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, "Celosvětové prognózy indexu lomu"](https://tropo.f5len.org/WW/info.php), [nová verze 2026](https://tropo.f5len.org/?p=504), [poznámka k modelu ARPEGE](https://tropo.f5len.org/author/f5len/)

Údaje

- [Open- Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api), [Poznámka k tlakovým hladinám](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)
- [NOAA NCEI, Integrovaný globální radiosonde Archive (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RAOBget, radio povodní skript pro University of Wyoming](https://github.com/NCAR/RAOBget)

Klasické texty (uvedené autorem programu, bude konzultován pro konečné vzorce)

- B. R. Bean, E. J. Dutton, * Radio Meteorology *, NBS Monograph 92, 1966, 435 stran (reprint Dover 1968): vertikální profily N a M, gradient pasti pod -157 N / km, srovnání s radiovlnou; kapitola 5 "Synoptická radiometeorologie". Základ pro 2-4 kroky výpočtu za bod. Zdarma: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [NIST karta, doi 10.6028 / NBS.MONO.92](https://www.nist.gov/node/1770911), [Internet Archive](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf)kopie.
- L. Boithias, * Radio Wave Propagation *, McGraw-Hill 1987 (přeloženo D. Beesonem z francouzštiny, 330 stran, ISBN 0070064334; také North Oxford Academic): mechanismy za horizontem, společný objem rozptylu a ztráty spojovacích antén - polovina. Samotný papír ([karta knihovny](https://libcat.library.tamu.edu/Record/in00000014812/Description)).
- M. P. M. Hall, * Effects of the Troposfére on Radio Communication *, IEE Electromagnetic Waves Series n. 8, Peter Peregrinus 1979, 206 pages (ISBN 0906048257; brossura 0863410863): synovské situace a šíření VKV / UHF. Základna pro synoptické ingredience. Obchod na IET ([](https://libcat.library.tamu.edu/Record/in00000636385)karta).
- H. G. Booker, W. E. Gordon, "A Theory of Radio Scatting in the Troposphere", * Proceedings of the IRE *, vol. 38, n. 4, April 1950, pp. 401-412: Teorie rozptylu z disogenity indexu lomu. Ústřední myšlenka: rozptýlený výkon závisí na intenzitě a rozsahu turbulentních výkyvů indexu lomu (exponenciální korelace); v blízkosti horizontu převažuje difrakce, další rozptyl; podle autorů je rozsah turbulence a variací indexu očekávaných meteorologií dostatečný k vysvětlení naměřených polí. Citovaný 328 články (Scilit), žádný volný přístup kopie. Card on Semantic Scholar ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); full text on IEEE Xplore paid ([doi 10.1109 / JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)).
- C. M. Crain, J. R. Gerhardt (Electric Engineering Research Laboratory, University of Texas), "Measurements of the Parameters Involved in the Theory of Radio Scatting in the Troposphere", * Proceedings of the IRE *, vol. 40, n. 1, January 1952, pp. 50-54, doi 10.1109 / JRPROC.1952.2739 give the real values that Booker and Gordon had only estimated ([Skilit](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)tab]).
- "The Constants in the Equation for Atmosphere Refractive Index at Radio Frequences", * Proceedings of the IRE *, vol. 41, n. 8, August 1953, pp. 1035-1037: navrhuje přezkoumat proudy, které se pak používají (79 K / mb a 4800 K) ve vzorci N; je to odtud, že hodnoty 77,6 a 3,73 · 10 ^ 5 vzorce ITU (Q](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E. C. S. Megaw, "Fundamental radio scatter distribution theory", * Proceedings of the IEE Part C: Monographs *, vol. 104, n. 6, september 1957, pp. 441-455, doi 10.1049 / pi-c.1957.0052: spektrum indexových fluktuací z teorie turbulence, kompletní sledování rozptylu za společným horizontem a účinek geometrie placené na IET ([Shilit](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)karta).
- * Proceedings of the IRE *, October 1955, special number on scatter propagation (by K. A. Norton and J. B. Wiesner), with Bullington, Staras and other: free in PDF on [World Radio History](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf).
- D. E. Kerr (edited by), * Propagation of Short Radio Waves *, MIT Radiation Laboratory Series vol. 13, McGraw- Hill 1951 (reports Dover 1965 and IEE 1987): theory of waration and surface ducts on the sea. Digitální kopie: [World Radio History](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); IET edition in [IET Digital Library](https://shop.theiet.org/propag-short-radio-waves).

Nedávná měření troposférické difuze v VKV: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Austrálie), "A Study of VHF Troposcatter Communication Channel in South Australia", in * Digital Signal Processing for Communication Systems *, Springer 1997, pp. 103-112 ([Springer](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)). Text za poplatek; z abstraktního se týká difuze, ne vedení. Užitečné pouze v případě, že chcete kalibrovat termín troposcatter s VHF kanály data.

Numerické modely a námořní trasy (uvedené autorem programu)

- M. Levy, * Parabolické rovnice pro elektromagnetické vlnové propagace *, IEE Electromagnetic Waves Series # 45, 2000, 336 stran (ISBN 0852967640): parabolická rovnice pro refrakční profily, které se mění podél cesty a pro orografii; kapitoly o tropoferickém šíření, paprsky a způsoby, šíření na moři. Sloužilo by to pro výpočet pole podél cesty za indexem. Pro platbu, také v elektronické podobě z IET ([IET karta](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)).
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (Univerzita v Leicesteru): 2 GHz měření na Channel Islands a Channel. Mnoho děl a práce jsou zdarma v univerzitním archivu: [dlouhodobé statistiky o odpařovacím potrubí, Radio Science 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434), [změny signálu na třech námořních trasách, Radio Science 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710), [Tituly N. Mufti 2012 s trasami 50 a 140 km také v VKV](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854), [teze I. Alam 2002 o reaktivitě vypočítané z meteorologických dat, 240 MHz a 2 GHz X3 UQ, [tesi.](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus ", Practical application of a waration model", Radio Science vol. 20 n. 4, 1985, s. 887 (not IEEE Transakce on Antennas and Propagation), doi 10.1029 / RS020i004p00887 ([schema](https://zendy.io/title/10.1029/rs020i004p00887)): výška odpařovacího kanálu z údajů o počasí na povrchu. Uvolněte technickou zprávu, která ji předchází: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf).

T. Lindquist, * Wave Propagation Models in the Troposféra for Long- Range UHF / SHF Radio Connections *, master 's disertation, University of Karlstad, 2 June 2020, 106 stran (dodáno autorem programu). Je step- by- step průvodce, volný, psát výpočet s parabolickou rovnicí: split- step řešení (i v širokých úhlech), hraniční podmínky pro zemi a moře, krok země, paprsek sledování na lineárních profilů v době, s numerické kontroly; cituje PETOOL volný nástroj pro MATLAB. Dvě užitečné údaje najednou: (1) N citlivost na proměnné počasí, δN Ø 0,26 δP + 4,3 δe − 1,4 δT (hPa a K, kolem 20 ° C), tj. chyba 1 hPa na tlaku páry váží třikrát 1 ° C chyba na teplotě: je vlhkost modelu, který musí být kontrolován první; (2) Průměrná výška odpařovacího potrubí: 5-6 m v Severním moři, 13-14 m ve Středozemním moři, více než 20 m v Perském zálivu, vždy příliš nízká pro 144 a 432 MHz. Závěr autora (frekvence 1 až 10 GHz): za horizontem je téměř vždy potřeba spolehlivého spojení.

Propagační software na půdním profilu (pro horizont a překážky, ne pro očekávané tropo)

- [Radio Mobile](https://ve2dbe.com/rme.html)by Roger Coudé VE2DBE: free, Windows (také Linux a Mac s vínem), Longley- Rice / ITM model, terénní profil automaticky extrahován z kvót data ([stáhnout](https://ve2dbe.com/download/download.html)).
- [SPLAT!](https://qsl.net/kd2bd/splat.html)od Johna Magliacane KD2BD: GPL,20 MHz-20 GHz, Longley-Rice a ITWOM v3.0 modely, zprávy o překážkách a ztrátách v textu; nejnovější verze 1.4.22014.
- [ITM, NTIA / ITS](https://its.ntia.gov/software/itm)úřední kód: Longley-Rice model (20 MHz-20 GHz) se zdrojem a dokumentací, základna Radio Mobile a SPLAT!. Kód je zdarma a můžete přidat do výpočtu na cestě LIVETROPOVHF (ztráta v důsledku půdy, která má být přidána do indexu tropo). Původní zpráva: A. G. Longley, P. L. Rice, "Prediction of Troposféric Radio Transmission Loss over Irregular Terrain: A Computer Method - 1968", ESSA Technical Report ERL 79-ITS 67, July 1968 (NTIA card on historical software: X1 https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITU-R, software a digitální data](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): zdarma mapy ΔN a N0 ročních mediánů na mřížce 1,5 ° používané v P.452 a P.617 - užitečné jako referenční klimatické hodnoty pro index.
- Pathloss (Contract Telecommunication Engineering): obchodní program pro rádiové mosty; Nenašel jsem veřejný zdroj, který by potvrdil, které doporučení ITU implementují, tak abych to ověřil.

Jednorázový kód GitHub v LIVETROPOVHF

| Projekt | Co dělá | Jazyk | Použití možné pro nás |
| [NTIA / ITM](https://github.com/NTIA/itm) | model Longley- Rice úředník NTIA, s balíčky pro. NET |  C++, C#  | ztráta půdy na cestě: integruje přímo do programu, který je v C # |
| [Fuzzzeee / itm](https://github.com/fuzzzeee/itm) | ITM C + + kód C # překlad |  C#  | alternativa bez nativních knihoven, také užitečná pro verzi Windows 7 |
| [eeveetza / Py452](https://github.com/eeveetza/py452) | ITU-R P.452, identické s verzí MATLAB / Octave schválenou ITU-R WP 3M; MATLAB verze zveřejněná švýcarským federálním komunikačním úřadem | Python, MATLAB | odkaz na překlad do C # troposcatter formulations and directed, with ITU validation exampments |
| [bwinkel / pycraf](https://github.com/bwinkel/pycraf) | P.452 kompletní, P.676, SRTM profily; GPL v3, ale včetně ITU datových souborů nejsou zdarma pro komerční použití | ♪ | kontrola našich výsledků |
| [ITU- Rpy](https://github.com/inigodelportillo/ITU-Rpy) | mnoho ITU- R doporučení, včetně P.453-13, validované s příklady ITU | ♪ | ověření N vzorců a klimatických map P.453 |
| [NCAR / RAOBget](https://github.com/NCAR/RAOBget) | stáhnout radiovou vlnu University of Wyoming v textovém formátu | ♪ | schéma ke stažení v C # profily italských stanic |

Poznámka: Aktuální verze P.453 je [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), ke stažení zdarma; v dokumentu jsem použil 13, aby se znovu spojila.

Již v programových manuálech a opakovaně použitelných zde: LA0BY (Nordic VUSHF 2019), IK3XTV ("Propagace na 2 metry" a "Propagace VHF - konvekční buňky a gravitační vlny", Radio Magazine, poskytnuté autorem programu), DF5AI/DK5YA (2003), G3YGF (QST 1983), The VHF / UHF DX Book Cap. 2, G4MCU (2003) KP4MD
