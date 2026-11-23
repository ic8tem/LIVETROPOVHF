LIVETROPOVF · technický dokument 3
# Ako vytvoriť trope v 144 MHz a mimo nej

> Strojový preklad z taliančiny: v prípade pochybností platí talianska príručka.

Čo sa stane vo vzduchu, keď 144, 432 a mikrovlnné rúry idú za horizont: refrakcia, inverzie, kanály, rozptyly, a ako si ich prečítať na mape programu. Verzia z 9. novembra 2026.

## Súhrnne

- VHF vlny a za cestovanie v prvých kilometroch atmosféry (troposféra) a ohnúť, kde index lomu sa mení s [3] [7] [11].
- Index sa meria pomocou lomu N, ktorá závisí od tlaku, teploty a predovšetkým od vlhkosti [11].
- V normálnej atmosfére je asi 40 jednotiek na km; keď viac ako 157 jednotiek na km je vlna zachytená v [7] [11] kanáli.
- Spojka pochádza z tepelnej inverzie so suchým vzduchom nad a mokrým pod: anticyklonický, morský, pokojný večer, [3] [4] [5] [13] fronty.
- Čím vyššia frekvencia, tým tenší kanál stačí: mikrovlnné rúry sa otvárajú častejšie ako 144 [3] [7].
- Bez vedenia zostáva troposférový rozptyl: vždy prítomný, slabý, ktorý padá so vzdialenosťou [6].
- Na programe: vysoký tlak + reverzný + Hepburn / F5LEN vysoký = pravdepodobné podmienky; škvrny potvrdzujú [1] [2] [13] [14].

## 1. Kde sa to stane: troposféra

Troposféra je najnižšia vrstva atmosféry, až do cca 10 a 12 km. Pre tróp počítať najmä prvé 3 km: tam sú vodnej pary a inverzie, a tam prejsť rádiové trasy medzi stanicami na zemi [4]. Za normálnych podmienok teplota klesá s nadmorskou výškou okolo 6,5 °C na km [3]. Na druhej strane, keď vo vrstve teplota stúpa s nadmorskou výškou, hovoríme o tepelnej inverzii: je to východiskový bod takmer všetkých silných tropových otvorov.

VHF sa neodrážajú v ionosfére ako HF (salve a sporadický, ktorý má iné hodiny a iné vzdialenosti [5]): za horizontom prichádza len vtedy, ak ich atmosféra ohýba na zem alebo ich rozprestiera.

## 2. Refrakcia a refrakčná N

Rádiová vlna sa ohýba vzduchom s indexom lomu a iným. Index n je veľmi blízko 1, takže sa používa reaktivita N, t. j. n - 1 vynásobená miliónom [11]:

```math
N = (n - 1) \cdot 10^{6} = \frac{77,6}{T} \left( P + 4810 \, \frac{e}{T} \right)
```

tlak P v hPa, T teplota v kelvine a tlak vodnej pary v hPa. N pôda je zvyčajne 300 / 400. Druhý termín, para, váži veľa: z tohto dôvodu vlhkosť počíta rovnako ako [3] [11] teplota.

Čo ohýba vlnu nie je hodnota N, ale ako sa mení s kvótou, gradient dN/dh:

| dN/dh (N/km) | Situácia | Vplyv na vlnu |
| 0 | Subrefrakcia | Vlna sa pohybuje od zeme, kratší horizont |
| od 0 do − 79 | Normálne (štandard - 39) | Svetlo ohýbanie k zemi: rádiový horizont asi 4/3 optického |
| od − 79 do − 157 | Superrefrakcia | Vlna ohýba viac, vyšší prietok |
| menej ako -157 | Obsah | Vlna sa ohýba rovnako ako Zem alebo viac a zostáva v pasci |

Prah −157 N/km je bod, kde sa zakrivenie polomeru rovná bodu [7] [11] Zeme. Preto "ekvivalentný pozemný lúč" 8497 km (4/3 skutočného polomeru) použitý pri výpočte [6] scatter.

## 3. Inverzie: tropomotor

Sklon pod −157 N/km vyžaduje, aby sa N zrútil v niekoľkých metroch nadmorskej výšky. Stáva sa, keď nad vrstvou čerstvého a vlhkého vzduchu sa nachádza teplejšie a suché: teplota stúpa a para padá dohromady, a obaja znížiť N [3] [5]. LA0BY [3]

Inverzie vznikajú rôznymi spôsobmi:

- Subsidence in anticyclon. Pri vysokom tlaku vzduch v nadmorskej výške klesá pomaly, obklady a zahrieva sa, zatiaľ čo na zemi zostáva čerstvý a vlhký vzduch. Je príčinou dlhšie a dlhšie otvory, ktoré v posledných dňoch [3] [4] [13].
- Nočné ožarovanie. S pokojnou oblohou a slabým vetrom pôda v noci ochladzuje a ochladzuje kontaktný vzduch: Nízky opačný smer, ktorý prerazí s ranným slnkom. Večerné a ranné otvory, typické pre leto a jeseň.
- Varovanie pred morom. Horúci a suchý vzduch prichádzajúci zo zeme nad chladnejším morom: morská vrstva zostáva vlhká a chladná. Odtiaľ veľké námorné trasy (Atlantické, Stredomoria, Severné more) [4] [13].
- Vpredu. Pred teplým predkom prúdi horúci vzduch cez ten studený; otvor je krátky a nasleduje čelo.
- Odparenie. Nad morom, v prvých metroch, vlhkosť rýchlo klesá s nadmorskou výškou: veľmi tenké potrubie, užitočné len pre mikrovlnné rúry.

Znak, že reverzný je vidieť vo vertikálnych profiloch (radiosondaggi): teplota stúpa a Rosný bod klesá v rovnakom úseku [5]. Program ukazuje vodítko o tom s Thermal Inversion vrstvou (vzduch pri 925 hPa teplejšie ako na zemi) a s izotermickým pri 850 hPa.

## 4. Povrchové a vysoké kanály

Spojka je vrstva, v ktorej vlna prechádza odrazom medzi horným okrajom (inverzia) a spodným okrajom [7]:

- Povrchový plynovod: pozemné alebo námorné. Nízka nadmorská výška stanice, najmä na pobreží, vstúpiť priamo.
- Vysoký plynovod: stojí vo výške, napríklad medzi 500 a 2000 m. Stanice v rovinách nemusia cítiť, zatiaľ čo tí v kopci alebo v horách vstupujú do vedenia. Vysvetlite odkazy, ktoré prechádzajú cez Alpy alebo iné [3] reliéfy [4].

Ak chcete vstúpiť do vedenia by vlna mala začať takmer horizontálne: nízke antény na obzore a žiadne susedné prekážky. Vnútri vedenia je oslabenie je oveľa nižšia ako vo voľnom priestore, a vzdialenosti dosahujú 1000 a 2000 km a ďalej (napríklad spojenie medzi Pyrenejským polostrovom, Kanárske ostrovy a Anglicko, alebo medzi Škandináviou a južným Ruskom) [3].

## 5. Frekvencia: prečo sa mikrovlny otvárajú častejšie

Spojka zachytáva len dostatok krátkych vĺn než ich hrúbka. Dlhé vlny Vychádzajú z tenkého kanálika; Krátke nie. Potom [3] [7]:

| Pásmo | Vlnová dĺžka | Čo potrebuješ? |
| 144 MHz |  2 m  | Hrubé potrubia (centrája metrov), široké a dobre označené inverzie: menej časté, ale veľmi rozsiahle otvory |
| 432 MHz | 70 cm | Tenšie potrubia: často sa otvára, keď 144 sú len |
| 1296 MHz | 23 cm | Len málo slabých inverzií; dlhé trasy na mori časté v lete |
| 10 GHz | 3 cm | Len odparovací kanál na mori; počíta aj absorpciu dažďa |

Z tohto dôvodu môže skromný trojitý index už znamenať otvory v UHF a mikrovlnnej rúre, zatiaľ čo 144 vyžaduje vyššie hodnoty. Na druhej strane 144 členských štátov má na rovnakom kanáli menšie straty mimo vedenia a lepšie pokrýva pohraničné oblasti.

## 6. Troposferský rozptyl

Aj bez inverzie troposféra nie je jednotná: Malé nepravidelnosti teploty a vlhkosti rozšírili malú časť energie za horizont. Jedná sa o troposferické rozptýlenie, vždy prítomný, ktorý umožňuje pripojenie 300800 km s dobrými stanicami [6]. Signál je slabý a kolísavý; strata rastie s uhlom difúzie, to je s vzdialenosťou a s prekážkami na obzore:

```math
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
```

s θs difúznym uhlom v stupňoch a f v MHz [6]. Program používa tento vzorec v záložke každého miesta, aby zistil, či odkaz možno vysvetliť s rozptylom alebo vyžaduje spojku (pozri dokument 1). Vysoká hodnota N na zemi zlepšuje rozptyl [6].

Nesmie sa zamieňať s airskatter (reflexia na rovinách: signály niekoľkých sekúnd, Doppler) a rozptyl meteor (reflexia na meteory), ktoré program vylučuje z tropa.

## 7. Obrázok počasia: ako si ju prečítať na mape

Otvory tropov nasledujú meteorológiu. Typické situácie [3] [4] [13]:

- Stabilný anticyklon, vysoký tlak (viac ako 1020 Najlepšie otvory nasledujú po okraji anticyklónu, najmä na mori.
- Calo della pressione: prestávky inverzie. G4MCU sledoval maják SK6VHF až kým tlak neklesne na 1012 hPa [13].
- Horúci vzduch v nadmorskej výške (hysotermia na 850 hPa stúpa) nad čerstvým vzduchom na zemi: obrátený.
- Konvekčné bunky a gravitačné vlny (napr. pod vetrom v Alpách) môžu vytvoriť lokálne a krátke otvory [4].
- Čas dňa: inverzie z ožarovania sú silnejšie vo večerných hodinách a skorých dopoludňajších hodinách; úbytok zostáva aj deň.

Indexy Hepburn [1] a F5LEN [2] to všetko spojili z modelov počasia: Vypočítajú N pri rôznych kvótach a premenia gradienty na číslo alebo triedu. Ja nie som záruka: LA0BY zobrazuje LA Pre to program ukazuje spolu indexy, isobare, teploty a reálne škvrny: Ak sa škvrny zhodujú s vysokým indexom oblasti pod anticyklopénom, otváranie prebieha. KP4MD našiel SNR WSPR správy pri 144 MHz rastie priamo v oknách poskytovaných Hepburn [14]. UA6 [3]

## 8. Praktický zoznam

1. Pozrite sa na index tropo vo vašej oblasti a podľa smeru, ktorý vás zaujíma, počas nasledujúcich niekoľkých hodín.
2. Kontrola izobare: vysoký tlak a široký izobare cez alebo vedľa cesty.
3. Zapnite tepelnú inverziu a T 850 hPa: horúci vzduch v nadmorskej výške nad cestou.
4. Preferujte trasy na mori a večer a ráno.
5. Ak ste na kopci, myslite na vysoké kanály; Ak ste na pobreží, na povrchu.
6. Skúste najprv najvyššie pásma (432, 1296): otvorte prvý.
7. Overiť spot a PSKReporter: dlhé čiary v rovnakom smere potvrdzujú kanál.

## Zdroje

1. W. R. Hepburn, DX Info Centre,
2. F5LEN, predpoveď pre Európu a svetová predpoveď tropférického šírenia VHF (tropo.f5len.org).
3. S. Heck LA0BY, Normálny gradient a inverzie, teplota a vlhkosť, vysoké tlaky, LA UA6
4. F. Egano IK3XTV, Prvé 3 km, morské cesty, konvekčné bunky, gravitačné vlny, vysoký tlak.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, Teplotné a Rosné profily, inverzie, rozdiel od E sporadické.
6. J. N. Gannaway G3YGF, Ekvivalentný polomer Zeme a strata rozptylu.
7. The VHF/UHF DX Book, kapitola 2 Refraktívnu zónu podľa dN/dh a povrchových a vysokokanálových profilov (len čísla).
11. Odporúčanie ITU-R P.453, rádiofrakčný index. N vzorec a prah kanála pri -157 N/km. Všeobecný odkaz, nie je pripojený.
13. G4MCU, Condotti s vysokým tlakom, najmä na mori; SK6VHF a znížiť na 1012 hPa.
14. C. F. Milazzo KP4MD, SNR WSPR pri predpovedi 144 MHz a Hepburn.

Zdrojové čísla sú rovnaké ako dokument 2, Map Building.
