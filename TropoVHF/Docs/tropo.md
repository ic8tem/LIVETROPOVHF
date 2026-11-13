LIVETROPOVHF · documento tecnico 3
# Come si forma la tropo in 144 MHz e oltre

Che cosa succede nell'aria quando i 144, i 432 e le microonde vanno oltre l'orizzonte: rifrazione, inversioni, condotti, scatter, e come leggerli sulla mappa del programma. Versione del 9 novembre 2026.

## In sintesi

- Le onde VHF e oltre viaggiano nei primi chilometri di atmosfera (troposfera) e si piegano dove l'indice di rifrazione cambia con la quota [3][7][11].
- L'indice si misura con la rifrattività N, che dipende da pressione, temperatura e soprattutto umidità [11].
- In atmosfera normale N cala di circa 40 unità per km; quando cala più di 157 unità per km l'onda resta intrappolata in un condotto [7][11].
- Il condotto nasce da un'inversione termica con aria secca sopra e umida sotto: anticicloni, mare, notti serene, fronti [3][4][5][13].
- Più alta è la frequenza, più sottile basta il condotto: le microonde aprono più spesso dei 144 [3][7].
- Senza condotto resta lo scatter troposferico: sempre presente, debole, che cala con la distanza [6].
- Sul programma: alta pressione + inversione + indice Hepburn/F5LEN alto = condizioni probabili; gli spot confermano [1][2][13][14].

## 1. Dove avviene: la troposfera

La troposfera è lo strato più basso dell'atmosfera, fino a circa 10–12 km. Per la tropo contano soprattutto i primi 3 km: lì si concentrano vapore acqueo e inversioni, e lì passano i percorsi radio tra stazioni a terra [4]. In condizioni normali la temperatura cala con la quota di circa 6,5 °C per km [3]. Quando invece, in uno strato, la temperatura sale con la quota, si parla di inversione termica: è il punto di partenza di quasi tutte le aperture tropo forti.

Le VHF non vengono riflesse dalla ionosfera come le HF (salvo E sporadico, che ha altre ore e altre distanze [5]): oltre l'orizzonte ci arrivano solo se l'atmosfera le piega verso terra o le diffonde.

## 2. La rifrazione e la rifrattività N

Un'onda radio si piega passando da aria con un indice di rifrazione a aria con un altro. L'indice n è vicinissimo a 1, quindi si usa la rifrattività N, cioè n − 1 moltiplicato per un milione [11]:

```math
N = (n - 1) \cdot 10^{6} = \frac{77,6}{T} \left( P + 4810 \, \frac{e}{T} \right)
```

con P la pressione in hPa, T la temperatura in kelvin ed e la pressione del vapore acqueo in hPa. Al suolo N vale di solito 300–400. Il secondo termine, quello del vapore, pesa molto: per questo l'umidità conta quanto la temperatura [3][11].

Ciò che piega l'onda non è il valore di N ma come cambia con la quota, il gradiente dN/dh:

| dN/dh (N/km) | Situazione | Effetto sull'onda |
| oltre 0 | Subrifrazione | L'onda si allontana da terra, orizzonte più corto |
| da 0 a −79 | Normale (standard −39) | Leggera curvatura verso terra: orizzonte radio circa 4/3 di quello ottico |
| da −79 a −157 | Superrifrazione | L'onda si piega di più, portate maggiori |
| meno di −157 | Condotto | L'onda curva quanto la Terra o di più e resta intrappolata |

La soglia di −157 N/km è il punto in cui la curvatura del raggio eguaglia quella della Terra [7][11]. Da qui viene anche il “raggio terrestre equivalente” di 8497 km (4/3 del raggio reale) usato nel calcolo dello scatter [6].

## 3. Le inversioni: il motore della tropo

Un gradiente sotto −157 N/km richiede che N crolli in pochi metri di quota. Succede quando sopra uno strato di aria fresca e umida si trova aria più calda e secca: la temperatura sale e il vapore scende insieme, e entrambi fanno calare N [3][5]. LA0BY indica inversioni fino a 10–15 °C per km contro il calo normale di 6,5 °C [3].

Le inversioni nascono in modi diversi:

- Subsidenza negli anticicloni. Nell'alta pressione l'aria in quota scende lentamente, si comprime e si scalda, mentre al suolo resta aria fresca e umida. È la causa delle aperture più lunghe ed estese, che durano giorni [3][4][13].
- Irraggiamento notturno. Con cielo sereno e vento debole il suolo si raffredda di notte e raffredda l'aria a contatto: inversione bassa che si rompe col sole del mattino. Aperture serali e mattutine, tipiche di estate e autunno.
- Avvezione sul mare. Aria calda e secca che arriva da terra sopra un mare più freddo: lo strato marino resta umido e fresco sotto. Da qui i grandi percorsi marini (Atlantico, Mediterraneo, Mare del Nord) [4][13].
- Fronti. Davanti a un fronte caldo l'aria calda scorre sopra quella fredda; l'apertura è breve e segue il fronte.
- Evaporazione. Sopra il mare, nei primi metri, l'umidità cala rapidamente con la quota: condotto sottilissimo, utile solo alle microonde.

Il segno che un'inversione c'è si vede nei profili verticali (radiosondaggi): temperatura che sale e punto di rugiada che scende nello stesso tratto [5]. Il programma mostra un indizio di questo con lo strato Inversione termica (aria a 925 hPa più calda che al suolo) e con le isoterme a 850 hPa.

## 4. Condotti superficiali ed elevati

Un condotto è uno strato in cui l'onda viaggia rimbalzando tra il bordo superiore (l'inversione) e il suolo o il bordo inferiore [7]:

- Condotto superficiale: parte dal suolo o dal mare. Le stazioni a quota bassa, specie sulla costa, ci entrano direttamente.
- Condotto elevato: sta in quota, per esempio tra 500 e 2000 m. Le stazioni in pianura possono non sentirsi, mentre chi sta in collina o in montagna entra nel condotto. Spiega i collegamenti che scavalcano le Alpi o altri rilievi [3][4].

Per entrare nel condotto l'onda deve partire quasi orizzontale: antenne basse sull'orizzonte e nessun ostacolo vicino. Dentro il condotto l'attenuazione è molto più bassa che nello spazio libero, e le distanze arrivano a 1000–2000 km e oltre (per esempio i collegamenti tra la Penisola Iberica, le Canarie e l'Inghilterra, o tra la Scandinavia e la Russia meridionale) [3].

## 5. La frequenza: perché le microonde aprono più spesso

Un condotto intrappola solo onde abbastanza corte rispetto al suo spessore. Le onde lunghe “escono” da un condotto sottile; quelle corte no. Quindi [3][7]:

| Banda | Lunghezza d'onda | Cosa serve |
| 144 MHz | 2 m | Condotti spessi (centinaia di metri), inversioni ampie e ben marcate: aperture meno frequenti ma molto estese |
| 432 MHz | 70 cm | Condotti più sottili: spesso apre quando i 144 sono solo “buoni” |
| 1296 MHz | 23 cm | Bastano inversioni deboli; percorsi lunghi sul mare frequenti in estate |
| 10 GHz | 3 cm | Basta il condotto di evaporazione sul mare; conta anche l'assorbimento della pioggia |

Per questo un indice tropo modesto può già voler dire aperture in UHF e microonde, mentre i 144 chiedono valori più alti. In compenso, a parità di condotto, i 144 hanno meno perdite fuori dal condotto e coprono meglio le zone di bordo.

## 6. Lo scatter troposferico

Anche senza inversioni la troposfera non è uniforme: piccole irregolarità di temperatura e umidità diffondono una minima parte dell'energia oltre l'orizzonte. È lo scatter troposferico, sempre presente, che permette collegamenti di 300–800 km con buone stazioni [6]. Il segnale è debole e fluttua; la perdita cresce con l'angolo di diffusione, cioè con la distanza e con gli ostacoli all'orizzonte:

```math
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
```

con θs l'angolo di diffusione in gradi e f in MHz [6]. Il programma usa questa formula nella scheda di ogni spot per dire se un collegamento è spiegabile con lo scatter o richiede un condotto (vedi documento 1). Un valore alto di N al suolo migliora lo scatter [6].

Da non confondere con l'airscatter (riflessione su aerei: segnali di pochi secondi, Doppler) e con il meteor scatter (riflessioni sulle meteore), che il programma esclude dalla tropo.

## 7. Il quadro meteo: come leggerlo sulla mappa

Le aperture tropo seguono la meteorologia. Le situazioni tipiche [3][4][13]:

- Anticiclone stabile, pressione alta (oltre 1020–1025 hPa), isobare larghe, poco vento: inversione di subsidenza. Le aperture migliori seguono il bordo dell'anticiclone, specie sul mare.
- Calo della pressione: l'inversione si rompe. G4MCU ha seguito il beacon SK6VHF fino al calo della pressione a 1012 hPa [13].
- Aria calda in quota (isoterme a 850 hPa che salgono) sopra aria fresca al suolo: inversione.
- Celle convettive e onde di gravità (per esempio sottovento alle Alpi) possono creare aperture locali e brevi [4].
- Ora del giorno: le inversioni da irraggiamento sono più forti la sera e al mattino presto; la subsidenza resta anche di giorno.

Gli indici Hepburn [1] e F5LEN [2] mettono insieme tutto questo partendo dai modelli meteo: calcolano N alle varie quote e trasformano i gradienti in un numero o in una classe. Non sono una garanzia: LA0BY riporta un evento LA–UA6 visibile nella temperatura in quota e non in Hepburn [3]. Per questo il programma mostra insieme indici, isobare, temperature e spot reali: se gli spot si allineano a una zona di indice alto sotto un anticiclone, l'apertura è in corso. KP4MD ha trovato l'SNR dei rapporti WSPR a 144 MHz in crescita proprio nelle finestre previste da Hepburn [14].

## 8. Lista pratica

1. Guarda l'indice tropo sulla tua zona e lungo la direzione che ti interessa, per le prossime ore.
2. Controlla le isobare: alta pressione e isobare larghe sopra o accanto al percorso.
3. Accendi Inversione termica e T 850 hPa: aria calda in quota sopra il percorso.
4. Preferisci i percorsi sul mare e le ore serali e mattutine.
5. Se sei in collina, pensa ai condotti elevati; se sei sulla costa, a quelli superficiali.
6. Prova prima le bande più alte (432, 1296): aprono prima.
7. Verifica con gli spot e con PSKReporter: le linee lunghe nella stessa direzione confermano il condotto.

## Fonti

1. W. R. Hepburn, DX Info Centre, “The Hepburn Tropo Index (HTI)” e pagine “Tropospheric Ducting Forecast” (dxinfocentre.com).
2. F5LEN, previsioni per l'Europa e previsione mondiale della propagazione troposferica VHF (tropo.f5len.org).
3. S. Heck LA0BY, “Extreme Tropo Propagation on 144 MHz and up”, 41° Nordic VUSHF Meeting, Skjeberg 2019. Gradiente normale e inversioni, temperatura e umidità, alte pressioni, evento LA–UA6, collegamenti oltre le montagne.
4. F. Egano IK3XTV, “Propagazione VHF – Il contributo delle celle convettive e delle onde gravitazionali atmosferiche nella propagazione Tropo”. Primi 3 km, percorsi marini, celle convettive, onde di gravità, alta pressione.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, “Very Long Distance Propagation in the 144 MHz Band”, 24/7/2003. Profili di temperatura e punto di rugiada, inversioni, distinzione da E sporadico.
6. J. N. Gannaway G3YGF, “Tropospheric Scatter Propagation”, QST novembre 1983. Raggio terrestre equivalente e perdita di scatter.
7. The VHF/UHF DX Book, capitolo 2 “Tropospheric propagation”. Zone di rifrazione in funzione di dN/dh e profili di condotto superficiale ed elevato (solo le figure).
11. Raccomandazione ITU-R P.453, indice di rifrazione radio. Formula di N e soglia di condotto a −157 N/km. Riferimento generale, non allegato.
13. G4MCU, articoli “tropospheric” (g4mcu.net), 2018–2025. Condotti con alta pressione, specie sul mare; SK6VHF e calo a 1012 hPa.
14. C. F. Milazzo KP4MD, “VHF/UHF WSPR Propagation Study”, 2012–2014. SNR WSPR a 144 MHz e previsioni Hepburn.

I numeri delle fonti sono gli stessi del documento 2, Costruzione della mappa.
