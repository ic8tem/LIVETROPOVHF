LIVETROPOVHF · technický dokument 3
# Jak vytvořit tropu v 144 MHz a dále

> Strojový překlad z italštiny: v případě pochybností platí italská příručka.

Co se stane ve vzduchu, když144,432 a mikrovlnné vlny jdou za horizont: lom, inverze, kanály, scatters, a jak je číst na mapě programu. Verze dne 9. listopadu2026.

## Shrnuto.

- VHF vlny a mimo cestování v prvních kilometrech atmosféry (troposféra) a ohyb, kde se index lomu mění s [3] [7] [11].
- Index se měří refrakční N, která závisí na tlaku, teplotě a především vlhkosti [11].
- V normální atmosféře je asi 40 jednotek na km; když je více než 157 jednotek na km, je vlna zachycena v potrubí [7] [11].
- Průchod pochází z tepelné inverze se suchým vzduchem nad a mokré níže: anticyclon, moře, klidné noci, [3] [4] [5] [13] fronty.
- Čím vyšší je frekvence, tím tenčí je potrubí dostatečné: mikrovlnné vlny se otevírají častěji než 144 [3] [7].
- Bez vedení zůstává troposferový rozptyl: vždy přítomný, slabý, který padá s vzdáleností [6].
- Na programu: vysoký tlak + reverzní + Hepburn / F5LEN high = pravděpodobné podmínky; skvrny potvrzují [1] [2] [13] [14].

## 1. Kde se to stane: troposféra

Troposféra je nejnižší vrstva atmosféry, až do cca 10-12 km. Pro trope počet převážně prvních 3 km: tam jsou vodní páry a inverze, a tam projít rádiové trasy mezi stanicemi na zemi [4]. Za normálních podmínek teplota klesá s nadmořskou výškou cca 6,5 ° C na km [3]. Když na druhou stranu, ve vrstvě, teplota stoupá s nadmořskou výškou, mluvíme o tepelné inverzi: je to výchozí bod téměř všech silných otvorů tropo.

VHF se neodráží v ionosféře jako HF (solný a sporadický, který má jiné hodiny a jiné vzdálenosti [5]): za horizont přichází pouze tehdy, pokud je atmosféra ohne k zemi nebo je rozšíří.

## 2. Refrakce a refrakce N

Rádiová vlna se ohýbá vzduchem s refrakční indexem s jinou. Index n je velmi blízko 1, takže se použije reaktivita N, tj. n − 1 vynásobená milionem [11]:

```math
N = (n - 1) \cdot 10^{6} = \frac{77,6}{T} \left( P + 4810 \, \frac{e}{T} \right)
```

s P tlak v hPa, T teplota v kelvinu a tlak vodní páry v hPa. N půda je obvykle 300- 400. Druhý výraz, parní, váží hodně: Z tohoto důvodu vlhkost počítá stejně jako [3] [11] teplota.

Co ohýbá vlnu není hodnota N, ale jak se mění s kvótou, gradient dN / dh:

| dN / dh (N / km) | Situace | Účinek na vlnu |
| 0 | Subrefraction | Vlna ustupuje od země, kratší horizont. |
| od 0 do − 79 | Normální (standard − 39) | Lehké ohýbání k zemi: rádiový horizont asi 4 / 3 optického |
| od − 79 do − 157 | Superrefrakce | Vlna se víc ohýbá, větší průtok. |
| méně než − 157 | Obsah | Vlna se ohýbá stejně jako Země a zůstává v pasti. |

Práh -157 N / km je bod, kde zakřivení poloměru odpovídá zakřivení Země [7] [11]. Proto "ekvivalentní zemský paprsek" 8497 km (4 / 3 reálného poloměru) používaný při výpočtu [6] Scatter.

## 3. Verze: motor tropo

Gradient pod -157 N / km vyžaduje, aby N zkolaboval v několika metrech nadmořské výšky. To se stává, když se nad vrstvou čerstvého a vlhkého vzduchu nachází teplejší a suchý: teplota stoupá a páry padá dohromady, a oba snížit N [3] [5]. LA0BY [3]

Inverze vznikají různými způsoby:

- Poddanství v anticyklonu. Ve vysokém tlaku klesá vzduch v nadmořské výšce pomalu, stlačuje a ohřívá, zatímco na zemi zůstává čerstvý a vlhký vzduch. Je to příčina delších a delších otvorů, které poslední dny [3] [4] [13].
- Noční ozařování. S klidnou oblohou a slabým větrem se půda v noci ochlazuje a chladí kontaktní vzduch: Nízký zpětný chod, který se láme s ranním sluncem. Večerní a ranní otevření, typické pro léto a podzim.
- Varování. Horký a suchý vzduch přicházející ze země přes chladnější moře: mořská vrstva zůstává vlhká a chladná níže. Odtud velké námořní trasy (Atlantik, Středozemní moře, Severní moře) [4] [13].
- Fronts. Před teplou frontou proudí horký vzduch přes studenou, otvor je krátký a následuje čelo.
- Únik. Nad mořem, v prvních metrech, vlhkost klesá rychle s nadmořskou výškou: velmi tenké potrubí, užitečné pouze pro mikrovlnné vlny.

Znamení, že je obrácený je vidět ve svislých profilech (radiosondaggi): teplota stoupá a rosný bod klesá ve stejném úseku [5]. Program ukazuje nápovědu s vrstvou Thermal Inversion (vzduch na 925 hPa teplejší než na zemi) a s izotermální na 850 hPa.

## 4. Povrchové a vysoké kanály

Průvodce je vrstva, ve které se vlna pohybuje skákáním mezi horním okrajem (inverze) a spodním okrajem [7]:

- Povrchový plynovod: země nebo moře. Nízkovýškové stanice, zejména na pobřeží, vstupují přímo.
- Vysoký ropovod: stojí v nadmořské výšce, například mezi 500 a 2000 m. Stanice na pláních nemusí cítit, zatímco ti na kopci nebo na hoře vstoupí do potrubí. Vysvětlete odkazy, které překračují Alpy nebo jiné [3] reliéfy [4].

Pro vstup do potrubí by měla vlna začít téměř vodorovně: nízké antény na obzoru a žádná sousední překážka. Uvnitř potrubí je útlum mnohem nižší než ve volném prostoru a vzdálenosti dosahují 1000- 2000 km a dále (například spojení mezi Iberským poloostrovem, Kanárskými ostrovy a Anglií, nebo mezi Skandinávií a jižním Ruskem) [3].

## 5. Frekvence: proč se mikrovlny otevírají častěji

Průchod zachycuje jen krátké vlny než jeho tloušťka. Dlouhé vlny "vyjít" z tenkého kanálu, ty krátké ne. Pak [3] [7]:

| Pásmo | Vavřín | Co potřebuješ? |
| 144 MHz |  2 m  | Silné potrubí (centinia metrů), široké a dobře označené inverze: méně časté, ale velmi rozsáhlé otvory |
| 432 MHz | 70 cm | Více tenčí potrubí: často otevírá, když 144 jsou pouze "zboží" |
| 1296 MHz | 23 cm | Málo slabých inverzí; dlouhé trasy po moři časté v létě |
| 10 GHz | 3 cm | Jen odpařovací potrubí na moři; také počítá absorpci deště |

Z tohoto důvodu může skromný tropový index již znamenat otvory v UHF a mikrovlnné troubě, zatímco 144 volá po vyšších hodnotách. Na druhé straně, na stejném vedení, 144 mají méně ztrát mimo vedení a lépe pokrývají hraniční oblasti.

## 6. Troposfer se rozptýlí.

I bez inverze není troposféra jednotná: Malé nepravidelnosti teploty a vlhkosti šíří malou část energie za horizont. Je to troposferický rozptyl, vždy přítomný, který umožňuje spojení 300-800 km s dobrými stanicemi [6]. Signál je slabý a kolísá; ztráta roste s úhlem difuze, tedy s vzdáleností a s překážkami na obzoru:

```math
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
```

s θs úhlem difuze ve stupních a f v MHz [6]. Program používá tento vzorec v záložce každého bodu k určení, zda odkaz lze vysvětlit rozptylem nebo vyžaduje vedení (viz dokument 1). Vysoká hodnota N na zemi zlepšuje rozptyl [6].

Není třeba zaměňovat s airscatter (reflexe na rovinách: signály několika sekund, Doppler) a rozptyl meteor (reflexe na meteory), které program vylučuje z tropo.

## 7. Počasí: jak si ji přečíst na mapě

Otevírání tropa sleduje meteorologii. Typické situace [3] [4] [13]:

- Stabilní anticyclon, vysoký tlak (více než 1020-1025 hPa), široký isobare, malý vítr: převrácení subsidence. Nejlepší otvory následují po okraji anticyklonu, zejména na moři.
- Calo della pressione: inverze přestávky. G4MCU sledoval maják SK6VHF až do poklesu tlaku na 1012 hPa [13].
- Horký vzduch v nadmořské výšce (hysotermie na 850 hPa stoupající) nad čerstvým vzduchem na zemi: obráceně.
- Konvenční buňky a gravitační vlny (např. pod větrem v Alpách) mohou vytvářet lokální a krátké otvory [4].
- Čas dne: inverze z ozařování jsou silnější večer a brzy ráno; poddanost zůstává i den.

Indexy Hepburn [1] a F5LEN [2] to vše daly dohromady z modelů počasí: Vypočítají N v různých kvótách a přemění gradienty na číslo nebo třídu. Nejsem zárukou: LA0BY ukazuje LA- UA6 událost, která je viditelná při vysoké teplotě, a ne v Hepburn [3]. Pro to program ukazuje společně indexy, isobare, teploty a skutečné skvrny: pokud skvrny sladí s vysokým indexem oblasti pod anticyclonem, otvor je v plném proudu. KP4MD našel SNR WSPR reportů na 144 MHz rostoucí přímo v oknech poskytnutých Hepburn [14].

## 8. Praktický seznam

1. Podívejte se na tropo index na vaší oblasti a podél směru, který vás zajímá, na příštích pár hodin.
2. Zkontrolujte isobare: vysoký tlak a široký isobare přes nebo vedle cesty.
3. Zapněte tepelnou inverzi a T 850 hPa: horký vzduch v nadmořské výšce nad cestou.
4. Preferuji cesty po moři a večer a ráno.
5. Pokud jste na kopci, myslete na vysoké kanály; Pokud jste na pobřeží, na povrchu.
6. Zkuste první nejvyšší pásma (432, 1296): otevřít první.
7. Ověřte pomocí spot a PSKReporter: dlouhé čáry ve stejném směru potvrzují kanál.

## Zdroje

1. W. R. Hepburn, DX Info Centre, "The Hepburn Tropo Index (HTI)" a "Troposferic Ducting Forecure" stránky (dxinfocentre.com).
2. F5LEN, předpověď pro Evropu a světová předpověď VHF tropoferického šíření (tropo.f5len.org).
3. S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", 41 ° Nordic VUSHF Meeting, Skjeberg 2019. Normální gradient a inverze, teplota a vlhkost, vysoký tlak, LA- UA6 událost, spojení za horami.
4. F. Egano IK3XTV, "Propagace VHF - Příspěvek konvektivních buněk a atmosférické gravitační vlny v tropo šíření". První 3 km, mořské cesty, konvekční buňky, gravitační vlny, vysoký tlak.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, "Very Long Distance Propagation in the 144 MHz Band", 24 / 7 / 2003. Profily teploty a rosného bodu, inverze, rozdíl od E sporadic.
6. J. N. Gannaway G3YGF, "Troposféric Scatter Propagation", QST November 1983. Pozemský poloměr je ekvivalentní a rozptyluje ztrátu.
7. VHF / UHF DX Book, kapitola 2 "Troposférické šíření". Refrakční zóny podle dN / dh a povrchových a vysokokanálových profilů (pouze figurky).
11. Doporučení ITU-R P.453, index radiového lomu. Vzorec N a práh potrubí při -157 N / km. Obecný odkaz, není přiložen.
13. G4MCU, "troposférické" články (g4mcu.net), 2018-2025. Condotti s vysokým tlakem, zejména na moři; SK6VHF a snížit na 1012 hPa.
14. C. F. Milazzo KP4MD, "VHF / UHF WSPR Propagation Study", 2012-2014. SNR WSPR na 144 MHz a Hepburn předpověď.

Zdrojová čísla jsou stejná jako dokument 2, Map Building.
