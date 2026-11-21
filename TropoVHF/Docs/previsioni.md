# Manuale delle previsioni

La linguetta Previsioni calcola da sé la propagazione troposferica: una mappa dell'indice sulla zona visibile e il segnale previsto fra la propria stazione e un corrispondente. Questo manuale descrive ogni dato, come viene calcolato e da quale fonte viene. I numeri fra parentesi quadre rimandano alle fonti in fondo; nel programma ogni dato dei risultati ha un fumetto (simbolo ⓘ) con la stessa fonte e il richiamo al paragrafo.

## 1. Cosa fa e cosa non fa

La previsione propria parte dai profili verticali di temperatura e umidità del modello GFS [1, 2], ne ricava la rifrattività dell'aria strato per strato e stima quanto il segnale guadagna rispetto a un'atmosfera normale. Sul percorso aggiunge il terreno reale (SRTM1 [3]), la diffrazione, il troposcatter, i condotti e il rumore del ricevitore.

Non è una misura: è una stima fatta su un modello meteorologico con livelli distanti centinaia di metri. Serve a confrontare situazioni (oggi meglio di ieri, 70 cm meglio di 2 m, questa tratta meglio di quella), non a garantire un collegamento. Il paragrafo 14 spiega come raccogliere le prove per validarla.

## 2. Comandi della linguetta

- Banda: 144, 432 o 1296 MHz. Mappa e calcolo usano la banda scelta.
- Ora: il cursore sposta la previsione da adesso fino a 72 ore avanti, a passi di 3 ore.
- Aggiorna previsione (zona visibile): scarica i profili GFS su una griglia di al massimo 240 punti che copre la mappa visibile. Il passo della griglia (0,5° o più) dipende dall'ingrandimento.
- Stop: interrompe uno scaricamento o un calcolo.
- Curve di guadagno, Isobare, Inversione termica, Segni meteo: linee e segni opzionali sopra i colori (paragrafi 5 e 9 bis). I colori restano sempre.
- Sorgente dei dati e Caso storico: da dove arrivano i profili e quale giorno si guarda (paragrafi 3 bis e 3 ter).
- Correzioni meteo: accende o spegne il termine di stabilità di Misme e la correzione per subsidenza (paragrafo 9 bis).
- La mia stazione e il corrispondente: locatore, antenne, potenza, cavi, rumore (paragrafi 7 e 12).
- Calcola il segnale e Copertura in ottica: i due calcoli sul terreno (paragrafi 8–13).
- Registro dei calcoli: ogni passo con i numeri usati, anche nel file Dati/previsioni-calcoli.log.

## 3. Dai dati GFS al profilo

Per ogni punto della griglia e ogni ora si leggono la pressione al suolo, temperatura e umidità a 2 m, il vento a 10 m, l'altezza dello strato limite, il CAPE e temperatura e umidità relativa a 1000, 975, 950, 925, 900, 850, 800, 700, 600 e 500 hPa. Dalla 1.3.0 si leggono anche il vento a 850 hPa e la velocità verticale a 850 e 700 hPa (paragrafo 9 bis).

Le quote dei livelli si calcolano con la formula ipsometrica e la temperatura virtuale; i livelli sotto il suolo si scartano. Per ogni livello:

- N = 77,6/T · (P + 4810·e/T), con T in kelvin, P ed e in hPa, e dalla formula di Magnus (ITU-R P.453 [4]).
- M = N + 157·h, con h in km: la rifrattività modificata. Dove M cala salendo c'è un condotto.

## 3 bis. Sorgente dei dati

Il menu Sorgente dei dati sceglie da dove arrivano i profili:

- Automatica (consigliata): GFS prima da NOMADS [14], il servizio della NOAA che distribuisce il modello: per ogni istante il programma chiede due ritagli della zona in formato GRIB2 e li legge da sé; NOMADS non ha un limite giornaliero, solo uno al minuto che il programma rispetta. Con le griglie larghe (passo da 0,5° in su) usa i file a 0,5° o 1° ogni 3 ore, molto più leggeri. I punti che NOMADS non dà, o tutti se NOMADS non risponde, si chiedono a Open-Meteo [2]. Le due sorgenti danno lo stesso modello e la stessa corsa, quindi si possono mescolare nella stessa griglia.
- GFS da Open-Meteo: solo Open-Meteo, come nella 1.2 (al limite al minuto si attende).
- GFS da NOAA NOMADS: solo NOMADS. Più lento da scaricare (due file per ora, mezzo secondo fra una richiesta e l'altra per non superare il limite della NOAA), ma non consuma il limite di Open-Meteo.
- ICON-EU (DWD) ed ECMWF IFS 0,25°, da Open-Meteo: modelli diversi dal GFS, che valgono per tutta la griglia e per i percorsi. Non si mescolano con il GFS: confrontare due modelli sulla stessa ora è un modo semplice per vedere quanto la previsione è incerta. ICON-EU copre solo l'Europa; ECMWF non ha tutti i livelli di pressione del GFS (i livelli mancanti si saltano) e può non avere lo strato limite.

La sorgente usata è scritta nella riga di stato e nella prima riga dei risultati (Sorgente dei dati), con quanti punti sono arrivati da ciascun servizio e quanti dalla cache. La cache su disco è separata per modello.

## 3 ter. Casi storici

Il menu Caso storico rivede un'apertura documentata con le previsioni GFS d'archivio di Open-Meteo [15]: la mappa si sposta sulla zona del caso, la banda si imposta da sola e il cursore dell'ora parte dal primo giorno del caso. Premendo Aggiorna previsione si scaricano i profili di quei giorni; il calcolo del segnale funziona come per la previsione attuale. Serve a confrontare il modello con quello che è successo davvero.

Il primo caso è quello del 14–15 agosto 2022 raccontato in QST (novembre 2022, «The World Above 50 MHz» [23]): un'apertura sotto un'alta pressione fra Manitoba e Kansas/Missouri, da 144 a 1296 MHz quasi tutta in FT8, con il record canadese a 10 GHz di 1228,56 km in Q65 e segnali fino a 1941 km. Il secondo è l'apertura fra il 29 dicembre 2021 e il 2 gennaio 2022 sul Tirreno e il Mediterraneo occidentale a 144 MHz, dai rapporti di IC8TEM: prime aperture il 29 (fino a 737 km), più ampie il 31 (IT9GSF–EB5EA 1217 km), grande apertura il 1° gennaio fra Italia, Sicilia e Spagna. Per i casi più vecchi (per esempio settembre 2000) l'archivio di Open-Meteo non ha dati. Nell'archivio manca l'altezza dello strato limite: lì la sua correzione non si applica.

## 4. L'indice e i colori

Per ogni strato fra due livelli il programma confronta il calo di N con le soglie della tabella 6.5 di Bean e Dutton [5]: per esempio fra 1000 e 850 hPa un calo fino a 60 N/km è normale, oltre 100 N/km è super-rifrazione. Uno strato super-rifrattivo vale da 10 a 15 dB di guadagno; un condotto che intrappola la banda da 20 a 25 dB, secondo il deficit di M. Vento forte al suolo, strato limite alto e CAPE elevato riducono il guadagno, perché rimescolano o rompono gli strati [5, § 4.4].

Il colore è il guadagno stimato in dB, con una scala fissa uguale in tutti i temi:

| Guadagno | Colore | Significato |
| sotto 7 dB | nessuno | atmosfera quasi normale |
| 7–10 dB | azzurro | gradiente sopra il normale |
| 10–13 dB | verde | strato super-rifrattivo |
| 13–16 dB | giallo | strato forte, ma senza condotto per la banda |
| 16–18,5 dB | arancio | strato molto forte o condotto troppo sottile |
| 18,5–21 dB | rosso | condotto che intrappola la banda |
| da 21 dB | viola | condotto forte e profondo |

Con la casella Mostra anche 3–7 dB le zone fra 3 e 7 dB, dove qualcosa si sta formando ma resta sotto la scala, si vedono con un tratteggio grigio leggero; i colori non cambiano. Un riquadro tratteggiato delimita la zona coperta dalla previsione propria e riporta il guadagno massimo visibile: se è sotto 7 dB la mappa resta senza colori perché l'atmosfera è quasi normale, non perché mancano i dati. Durante lo scaricamento un nuovo clic su Aggiorna previsione non lo interrompe; per fermarlo c'è Stop.

I colori sono opachi all'82%, così lo sfondo del tema li cambia appena. La griglia dei locatori e i nomi dei quadrati sono disegnati sopra i colori. Dove la previsione propria non c'è ma Hepburn o F5LEN danno 5 o più compaiono quadretti a scacchi con il colore di quella fonte.

La mappa giudica ogni punto da solo. Il calcolo del segnale invece controlla che lo strato sia continuo lungo la tratta (paragrafo 9): una tratta che attraversa molto giallo può dare meno di quanto la mappa faccia pensare.

## 5. Linee opzionali e confronto con pressione e inversione

Curve di guadagno: linee a 7, 10, 13, 16, 18,5 e 21 dB, i bordi della scala, tracciate sul campo levigato con il metodo dei quadrati marcianti, con l'etichetta in dB.

Isobare: pressione al livello del mare ogni 4 hPa dallo stesso GFS, all'ora della previsione, con i centri H e L.

Inversione termica: curve tratteggiate di quanto l'aria a 925 o 850 hPa è più calda di quella a 2 m, a +1, +3, +5 e +8 °C.

Sotto i comandi una riga dà l'accordo del guadagno con la pressione e con l'inversione sulla zona visibile: il coefficiente di correlazione di Pearson, da −1 a +1. I condotti nascono spesso negli anticicloni (l'aria che scende si scalda e forma l'inversione), quindi un accordo positivo con la pressione è normale; ma dentro lo stesso anticiclone il guadagno cambia molto fra mare, coste e montagne. L'inversione da sola non basta: piega le onde solo se sopra l'aria è anche più secca [5]. La riga serve a vedere, ora per ora, quale delle due spiega meglio le zone colorate. Lo stesso confronto è scritto nel registro.

## 6. Condotti potenziali: il doppio calcolo

I livelli GFS distano da 200 a 1000 m: un'inversione sottile viene spalmata e il modello non vede il condotto. Se fra due livelli il calo di N supera il normale (40 N/km), l'aria è stabile (gradiente termico sotto 3 K/km) e sopra è più secca (rapporto di umidità specifica sotto 0,8), il programma considera un condotto potenziale e lo calcola in due modi:

- Calcolo A: tutto il calo in eccesso concentrato in 50 m.
- Calcolo B: lo stesso calo distribuito sullo spessore tipico dei condotti di quella zona, preso dalle mappe climatologiche di ITU-R P.453 [4] (al suolo o elevato).

Per il deficit e per la lunghezza d'onda massima intrappolata si tiene il valore più basso: è una stima prudente.

I dati di ITU-R P.453 sono © ITU e si possono usare per scopi personali e non commerciali citando la fonte, ma non si possono ridistribuire: per questo non sono nel pacchetto del programma. Per accendere il calcolo B basta il pulsante Scarica le mappe P.453 dall'ITU nella linguetta: il programma, dopo aver mostrato le condizioni d'uso, scarica l'archivio dei condotti dal sito dell'ITU (elevated_and_surface_duct.zip) per l'uso personale dell'utente, ne estrae i file in Dati/P453 e accende il calcolo senza riavviare. In alternativa si scaricano dalla pagina dei prodotti digitali della Study Group 3 dell'ITU (itu.int, ITU-R Study Group 3, Digital products, voce P.453, Surface duct data ed Elevated duct data) e si mettono i sette file S_OCCUR.TXT, S_THICK.TXT, S_STREN.TXT, E_OCCUR.TXT, E_base.TXT, E_THICK.TXT ed E_STREN.TXT nella cartella Dati/P453 accanto al programma, poi si riavvia. Senza i file il programma usa solo il calcolo A e lo scrive nella linguetta e nel registro. Il rapporto fra il deficit A e l'intensità tipica P.453 è l'anomalia: quante volte il condotto di oggi è più forte del normale.

## 7. Stazioni e locatori

Il locatore può avere 4, 6, 8 o 10 caratteri. Con 8 o 10 caratteri la posizione è precisa e la quota del suolo viene da SRTM1: basta l'altezza dell'antenna dal suolo. Con 4 o 6 caratteri il quadrato è troppo grande: conviene scrivere la quota dell'antenna sul mare; se resta 0 il programma la stima con il suolo SRTM al centro del quadrato più l'altezza dal suolo e lo segnala nei risultati. Per il corrispondente l'altezza predefinita è 3 m dal suolo; con quota sul mare 0 si usa il suolo SRTM più l'altezza.

## 8. Terreno, diffrazione e troposcatter

Il profilo della tratta si campiona su SRTM1 ogni 30 m (più largo oltre 120 km). Il raggio terrestre equivalente vale k = 157/(157 + ΔN), con ΔN il calo di N nel primo km dai profili GFS lungo la tratta [6, 7].

- Spazio libero: Lfs = 92,44 + 20 log f(GHz) + 20 log d(km) [8].
- Diffrazione: metodo di Bullington sul profilo reale più la correzione di terra sferica delta-Bullington di ITU-R P.452-16 § 4.2 [6], con la perdita sferica di ITU-R P.526 [9].
- Prima zona di Fresnel: raggio √(λ·d1·d2/d); libera se il terreno lascia almeno 0,6 del raggio. Per ogni ostacolo si mostrano il parametro ν e la perdita J(ν) del bordo a coltello [9].
- Troposcatter: ITU-R P.452-16 eq. 45 al 50% del tempo [6], con la distanza angolare dagli orizzonti e N0 dai profili.
- Senza tropo: somma delle potenze di diffrazione e troposcatter, mai sotto lo spazio libero.

## 8 bis. Troposcatter: tre modelli e incertezza

Accanto a P.452 il programma calcola il troposcatter con altri due modelli, come fa il programma TropoLink [16], e mostra la loro differenza come incertezza:

- ITU-R P.617-5 [17], mediana: L = F + 22 log f + 35 log θ + 17 log d + Lc, con f in MHz, θ in mrad, d in km, F = 0,18·N0·e^(−hs/7,35) − 0,23·ΔN e Lc = 0,07·e^(0,055(Gt+Gr)). Di solito N0 e ΔN si prendono dalle mappe climatologiche dell'ITU; qui si usano ogni ora i profili GFS (N0·e^(−hs/7,35) è la rifrattività al suolo, ΔN il calo nel primo km), così anche il troposcatter segue il tempo.
- Formula del VHF/UHF Handbook della RSGB, ripresa da DL2FZN [18]: L = Lfs + 24,44 + 4,283 ln f + 0,06656 d. La pendenza con la distanza è quasi uguale a quella di PROPBER (0,067 dB/km).

Volume comune (dalla 1.3.1): è il punto in cui si incrociano i raggi radenti delle due stazioni, ciascuno inclinato del proprio angolo d'orizzonte, su una Terra di raggio equivalente ae = k·6371 km. Dal TX il raggio sale come z = hTX + x·θt + x²/(2·ae); uguagliandolo a quello del RX si ottiene la distanza x = (hRX − hTX + d·θr + d²/(2·ae)) / θ, con θ = d/ae + θt + θr. Con orizzonti piatti e antenne alla stessa quota la base vale d²/(8·ae): circa 1,3 km a 300 km, 3,7 km a 500 km, 9,4 km a 800 km. Più il volume è alto, più l'aria è secca e rarefatta e meno diffonde: per questo oltre 800–1000 km il troposcatter si esaurisce. Nei risultati compaiono quota e posizione della base, la rifrattività, il gradiente e l'umidità del GFS a quella quota, e la distanza equivalente di Bray (QST, novembre 1961): ogni grado d'orizzonte in più o in meno vale circa 111 km di distanza. Riferimento divulgativo: la presentazione «Oltre l'orizzonte in VHF» di IU3QEZ (GEKO Magazine n. 69).

La riga Troposcatter, altri modelli mostra le due perdite e l'incertezza come metà della differenza fra il modello più ottimista e il più pessimista. Sono solo confronti: la perdita senza tropo resta quella di P.452.

## 9. Lo strato fondante lungo la tratta

È il cuore del calcolo del segnale. Lungo la tratta si prendono i profili GFS ogni 25 km circa. Per ogni strato di pressione si cerca il tratto più lungo in cui lo stesso strato è presente su punti consecutivi; contano come condotto solo strati spessi almeno 40 m [10]. Si misurano:

- continuità: da che km a che km, e la frazione della tratta;
- quota della cima e della base, e l'inclinazione in m/km;
- terreno dentro lo strato: i km in cui le montagne salgono sopra la base;
- terra coperta: sul mare il condotto c'è quasi sempre, decide il tratto di terra [10]. Se la terra supera il 10% della tratta, la frazione usata è il minimo fra continuità e copertura della terra.

Il guadagno tropo è il guadagno dello strato per la frazione usata. Se più di metà dei profili è sub-rifrattiva il guadagno è negativo.

## 9 bis. Correzioni meteo: stabilità, subsidenza, scirocco

Dalla 1.3.0 il profilo dà anche tre informazioni meteorologiche, che si vedono nel registro (riga «stabilità (Misme)»), nella riga Meteo lungo la tratta dei risultati e, con la casella Segni meteo, sulla mappa.

- Stabilità di Misme [5, cap. 6]. Misme lega il campo oltre l'orizzonte a un parametro composto: M = a·(ge − 40 + b·ΔW), con a = 0,5 dB per N/km, il gradiente equivalente ge e la stabilità termica ΔW, il lavoro per sollevare l'aria di 1 km. Il programma calcola ΔW = g·∫(θ − θ0)/θ0 dz fra il suolo e 1 km sopra, con θ la temperatura potenziale; l'atmosfera standard vale circa 56 J/kg. Il termine aggiunto al guadagno è 0,07 dB per J/kg oltre lo standard, limitato fra −4 e +6 dB, e conta solo dove nessun condotto intrappola la banda (il gradiente è già nell'indice di Bean e Dutton). La taratura è provvisoria: va verificata con i rapporti (paragrafo 14).
- Subsidenza e aria in salita [19]. Moler e Holden trovarono rinforzi di 20 dB e oltre, per ore, sulle tratte VHF/UHF oltre l'orizzonte quando l'aria scende negli anticicloni, e affievolimenti profondi quando sale. Il GFS dà la velocità verticale a 850 e 700 hPa. Se a entrambi i livelli l'aria scende di più di 1 cm/s e c'è un'inversione o un forte calo dell'indice di rifrazione potenziale K (N riportato adiabaticamente a 1000 hPa, che mette in evidenza l'aria secca e calda scesa dall'alto [5]), si aggiungono 1,5 dB; se l'aria sale di più di 5 cm/s se ne tolgono 2. Sulla mappa: ↓ subsidenza, ↑ aria in salita.
- Scirocco [20]. Aria calda e secca dal Sahara sopra aria marina più fresca dà inversioni fortissime dal Mediterraneo orientale a Gibilterra. Il segno S compare dove a 850 hPa il vento arriva da 100–200° con almeno 7 m/s e la temperatura è almeno 16 °C, sul Mediterraneo. È solo un'indicazione: il guadagno lo decidono gli strati del profilo.

Senza un condotto che intrappola la banda il guadagno totale non supera 16 dB, la fine del giallo: le correzioni rendono più evidenti gli strati forti ma non li fanno passare per condotti (rosso e viola restano ai condotti).

- Persistenza (dalla 1.3.1). Le aperture maggiori arrivano dopo giorni di alta pressione già buoni (VHF/UHF DX Book; Biancomano e Leyshon 1998, regola delle 12 ore; i rapporti del 29/12/2021–2/1/2022 crescono dal primo al quarto giorno). Per ogni cella il programma conta le ore delle 24 precedenti con guadagno di almeno 13 dB e aggiunge fino a 2 dB in proporzione, solo dove il guadagno è già almeno 10 dB. Sul calcolo del segnale si usa la media lungo la tratta.

La casella Correzioni meteo spegne queste correzioni per vedere il modello della 1.2.

Validazione (paragrafo 14): sui 593 collegamenti a 144 MHz fra il 29 dicembre 2021 e il 2 gennaio 2022, confrontati con le stesse tratte e ore due settimane dopo, l'indice di separazione (0,5 = nessuna, 1 = perfetta) vale 0,79 senza correzioni e 0,91 con le correzioni (prima del tetto a 16 dB). Lo stesso confronto mostra il limite principale: il condotto sul mare a bassa quota, che in quei giorni portava i segnali fra la Campania e la Liguria, non è visibile ai livelli del GFS.

## 9 ter. Novità 1.3.2: due stime, soffitto, alta pressione, mare, stazioni, creste

**Stima prudente e aggressiva.** Il guadagno dal gradiente cresce da 0 dB al limite del normale fino a 10 dB alla soglia di super-rifrazione. Nello strato 1000–850 hPa la soglia prudente è quella di Bean e Dutton (100 N/km); quella aggressiva è la soglia ITU-R P.453 di 79 N/km, che con k50 = 157/(157 − ΔN) vale k ≈ 2 (k = 2,5 corrisponde a circa 94 N/km). Il calcolo punto-punto mostra le due perdite affiancate; con un condotto che intrappola la banda coincidono. La mappa usa la prudente.

**Soffitto di 1500 m.** Strati e condotti si cercano solo fino a 1500 m sopra il suolo: più in alto le antenne dei radioamatori non li sfruttano.

**Alta pressione estesa.** +1 dB dove almeno 7 delle 9 celle intorno hanno pressione ridotta al mare di 1020 hPa o più e il vento è debole nei bassi strati (10 m sotto 5 m/s, 850 hPa sotto 8 m/s), salvo moti ascendenti.

**Peso del mare.** Nel calcolo punto-punto il guadagno degli strati si moltiplica per 0,8 + 0,4·μ1, dove μ1 è il fattore di ITU-R P.452 (eq. 51) sul tratto continuo di terra più lungo: 1 sul mare aperto, piccolo con lunghi tratti di terra.

**Stazioni e condotto.** Quando un condotto che intrappola la banda copre solo parte della tratta, la quota di ciascuna antenna si confronta con base e cima del condotto all'inizio e alla fine del tratto coperto; il guadagno si moltiplica per 0,5 + 0,5·√(e^(−ΔhTX/300)·e^(−ΔhRX/300)), con Δh la distanza in metri fuori dal condotto. Con il condotto su quasi tutta la tratta vale l'accoppiamento del § 10.

**Strato limite e creste.** Se la cima dello strato limite del GFS (quota del suolo del modello più boundary_layer_height) supera le creste della tratta (massimi locali oltre 300 m, se la più alta supera 500 m), lo strato passa sopra i monti senza spezzarsi: +1,5 dB per la frazione di creste coperte. È l'osservazione di IT9GSF sulle aperture dalla Sicilia alla Germania con Tirreno favorevole.

**Profilo normale CRPL (alternativa).** Invece di 40 N/km fissi il calo normale segue l'atmosfera di riferimento esponenziale CRPL: N(h) = Ns·e^(−c·h), con −ΔN nel primo km = 7,32·e^(0,005577·Ns) (Bean e Thayer 1959). Con aria umida (Ns alto) il normale è più ripido e serve un gradiente più forte per dare guadagno; in quota è più dolce. Spento per difetto finché la validazione non dice che aiuta.

## 10. Condotto su tutta la tratta

Se lo strato intrappola la banda su almeno l'80% della frazione usata e le stazioni non sono in vista, si calcola anche la perdita nel condotto: spazio libero, più l'accoppiamento delle antenne (3 dB se sono dentro il condotto, di più se sono sopra o sotto), più l'attenuazione residua γd = 5·10⁻⁵·ae·f^(1/3) dB/mrad di ITU-R P.452 eq. 49 [6]. Se è minore della perdita senza tropo diventa la perdita prevista.

Una banda resta nel condotto solo se la sua lunghezza d'onda non supera λmax = 2√2·(2/3)·√(ΔM·10⁻⁶)·d (Kerr, eq. 17 [11]). Per questo lo stesso strato può intrappolare i 23 cm e i 70 cm ma non i 2 m. Il limite però non è netto: Kerr avverte che anche onde parecchie volte più lunghe risentono molto del condotto, e Hepburn e il VHF/UHF DX Book parlano di bordi «sfocati» e di condotti «che perdono». Dalla 1.3.1 il programma usa quindi un condotto morbido: se λ è fra λmax e 3·λmax, il guadagno passa gradualmente da 15 dB (λ = 3·λmax) al guadagno del condotto pieno (λ = λmax), con il fattore s = (λmax/λ − 1/3)/(2/3). In quel caso sono ammessi i colori di condotto. Il registro e la riga Meteo lungo la tratta indicano i punti con condotto morbido.

## 11. Confronto con ITU-R P.452

La riga Confronto P.452 applica il metodo ITU per condotti e riflessione su strati (P.452-16 § 4.5 [6]) con il ΔN mediano del GFS al posto di quello climatologico e p = 20% come costante di calibrazione, come nello studio CSIRO [10]. Comprende la perdita d'accoppiamento empirica Alf sotto 0,5 GHz (circa 28 dB a 144 MHz, 3,5 dB a 432 MHz), la schermatura dei siti, l'accoppiamento sul mare e l'assorbimento dei gas. È solo un confronto: la perdita prevista resta quella del paragrafo 9.

## 11 bis. Equazione parabolica (dalla 1.3.1)

La riga Equazione parabolica è un quarto calcolo, solo di confronto, fatto come i programmi professionali per i condotti (PETOOL; il framework wave-propagation di M. Lytaev, licenza MIT): invece di stimare il guadagno dagli strati, si fa avanzare l'onda lungo la tratta con il metodo split-step di Fourier dentro il profilo di rifrattività modificata M del GFS, preso dal punto di previsione più vicino a ogni distanza. Ai livelli del GFS si aggiungono i salti stimati dei condotti potenziali (calcolo A, 50 m a metà strato). Il terreno SRTM1 entra a gradini (sotto il suolo il campo è nullo), il suolo è conduttore e il fascio dell'antenna è una gaussiana con l'apertura stimata dal guadagno. Il calcolo vede da sé i condotti, quelli che perdono, la diffrazione e l'accoppiamento delle antenne; non vede il troposcatter, che si somma a parte. Accanto si mostra il «guadagno del profilo»: la stessa tratta con atmosfera standard meno quella con il profilo previsto.

Limiti: il profilo verticale resta quello dei livelli GFS, quindi i condotti sottili restano invisibili; il terreno a gradini è approssimato; il metodo ad angolo stretto vale per angoli di pochi gradi.

## 12. Antenne, segnale e rumore

- Yagi DL6WU: guadagno ≈ 8,6 + 8·log10(boom/λ) dBd, più 2,15 per i dBi [12].
- EIRP = potenza − cavo + guadagno TX.
- Segnale ricevuto = EIRP − perdita prevista + guadagno RX − cavo RX. S-meter IARU: S9 = −93 dBm, 6 dB per unità.
- Rumore esterno secondo ITU-R P.372 [13]: galattico (Fa = 52 − 23 log f) più artificiale secondo l'ambiente (città, residenziale, rurale, rurale tranquillo) oppure una temperatura d'antenna scritta a mano.
- Tsys = Ta + 290(Lc − 1) + 290(F − 1)·Lc ai morsetti dell'antenna, con Lc la perdita del cavo e F la cifra di rumore.
- S/N in SSB, CW e FT8 nella banda di ciascun modo, confrontato con la soglia impostata.
- Modi digitali: lo S/N in 2500 Hz confrontato con le soglie misurate di FT4 (−17,5 dB) e FT8 (−21 dB, ma −18,6 dB con l'evanescenza tipica del troposcatter) [21], Q65-30A (−24 dB) e JT65A (−25 dB) [22]. Quando il meccanismo è troposcatter la soglia FT8 usata è −18,6 dB.

## 13. Tempi di risposta e dati

Ogni calcolo misura i propri tempi: profili GFS (rete, analisi, pause obbligate di 20 s fra i lotti per il limite di Open-Meteo), terreno SRTM1 (con lo scaricamento dei riquadri, 6–7 MB l'uno, solo la prima volta), calcolo. La riga Tempo di risposta li riassume; nel registro le righe cominciano con TEMPI. All'aggiornamento della griglia il registro riporta anche la corsa GFS usata, quando è diventata disponibile e quanto sono vecchi i dati.

Le richieste a Open-Meteo hanno limiti al minuto, all'ora e al giorno per ogni indirizzo internet. I profili scaricati si salvano sul disco (Dati/cache-gfs) per ogni corsa e ogni modello: con la stessa corsa, che cambia ogni 6 ore, i punti già scaricati non si chiedono di nuovo. Con la sorgente automatica si parte da NOMADS e Open-Meteo serve solo per i punti mancanti; con la sola Open-Meteo, al limite orario o giornaliero si ferma subito e dice quando riprovare. I casi storici non si salvano nella cache.

## 14. Validare il modello

La previsione va confrontata con i collegamenti reali. Il registro dei calcoli conserva ogni numero; l'archivio del programma conserva spot e decodifiche. Per 432 e 1296 MHz, dove gli spot sono pochi, servono rapporti dagli operatori: chi ha fatto il collegamento, a che ora, con che segnale. Confrontare per molte ore il guadagno previsto con il segnale misurato dice se le soglie vanno spostate.

## Fonti

1. NOAA/NCEP, Global Forecast System (GFS), modello globale a 0,25°, quattro corse al giorno.
2. Open-Meteo, API di previsione (open-meteo.com), modello gfs_seamless, variabili ai livelli di pressione e metadati delle corse.
3. NASA/USGS, Shuttle Radar Topography Mission, 1 secondo d'arco (SRTM1), riquadri skadi su Amazon Web Services.
4. ITU-R P.453, The radio refractive index: its formula and refractivity data, con le mappe digitali dei condotti (Study Group 3, © ITU, uso personale e non commerciale).
5. B. R. Bean, E. J. Dutton, Radio Meteorology, NBS Monograph 92, 1966: tabella 6.5 e § 4.4.
6. ITU-R P.452-16, Prediction procedure for the evaluation of interference between stations on the surface of the Earth at frequencies above about 0.1 GHz: § 4.2 (diffrazione), eq. 45 (troposcatter), § 4.5 ed eq. 49 (condotti).
7. ITU-R P.834, Effects of tropospheric refraction on radiowave propagation.
8. ITU-R P.525, Calculation of free-space attenuation.
9. ITU-R P.526, Propagation by diffraction: eq. 31 (bordo a coltello) e § 3 (terra sferica).
10. B. Indermuehle, H. Suzuki, Tropospheric Ducting Prediction based on GFS Model Data at Inyarrimanha Ilgari Bundara, the CSIRO Murchison Radio-astronomy Observatory, Publications of the Astronomical Society of Australia, 2026 (arXiv 2608.12861).
11. D. E. Kerr, Propagation of Short Radio Waves, MIT Radiation Laboratory Series vol. 13, 1951: eq. 17 e 18.
12. G. Hoch DL6WU, Yagi antennas with extended boom, tabelle di guadagno per lunghezza del boom.
13. ITU-R P.372, Radio noise: rumore galattico e artificiale per ambiente.
14. NOAA/NCEP, NOMADS, filtro GRIB del GFS 0,25° orario (nomads.ncep.noaa.gov); formato WMO GRIB2, impacchettamento complesso con differenze spaziali.
15. Open-Meteo, Historical Forecast API (archivio delle previsioni dei modelli dal 2021 circa).
16. LukiBox, TropoLink, progettazione di tratte troposcatter con P.617, NBS TN101 e ITM affiancati (GitHub, licenza Apache 2.0).
17. ITU-R P.617-5, Propagation prediction techniques and data required for the design of trans-horizon radio-relay systems, 2019.
18. RSGB, VHF/UHF Handbook; implementazione di DL2FZN (andi-f/troposcatter su GitHub, 2013).
19. W. F. Moler, D. B. Holden, Journal of Research of the NBS 64D, 1960: rinforzi sulle tratte oltre l'orizzonte e moto verticale dell'aria negli anticicloni.
20. ARRL Handbook 2022 (99ª ed.), § 19.4.3–19.4.4: situazioni meteo e propagazione troposferica.
21. S. Franke, B. Somerville, J. Taylor, The FT4 and FT8 Communication Protocols, QEX luglio/agosto 2020.
22. W3SZ, soglie dei modi digitali lenti (JT65, JT9, QRA64) e scelta del modo secondo la propagazione, 2019.
23. QST, «The World Above 50 MHz», novembre 2022: apertura del 14–15 agosto 2022 fra Manitoba e Kansas/Missouri.
