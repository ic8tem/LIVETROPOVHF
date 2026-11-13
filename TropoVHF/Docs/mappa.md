LIVETROPOVHF · documento tecnico 2
# Costruzione della mappa

Come nasce la mappa della linguetta Mappa e spot: la base geografica, le proiezioni, la media degli indici Hepburn e F5LEN, le isobare, gli spot e le stazioni in ascolto, sovrapposti a strati. Versione del 9 novembre 2026.

## In sintesi

- La mappa è disegnata dal programma in otto strati, ognuno con la sua casella [8].
- Base: coste e confini Natural Earth a due livelli di dettaglio [9]; proiezioni di Mercatore e azimutale equidistante [10].
- Fisica: inversioni, rifrattività N e gradiente dN/dh, soglia di condotto a −157 N/km [3][4][7][11].
- Indice tropo: lettura tarata delle mappe Hepburn [1] e F5LEN [2], griglia comune e media delle due fonti.
- Isobare: pressione GFS [12] tramite Open-Meteo [15], linee ogni 4 hPa, centri H e L.
- Spot, stazioni in ascolto (cerchi rosa scuro con linee rosa sempre visibili) e decodifiche personali in rosso.
- Lettura della sovrapposizione: alta pressione, condotti elevati, calo di pressione, SNR in crescita, percorsi di montagna [3][4][5][13][14].

## 1. Il principio: una mappa a strati

La mappa non è un'immagine fissa: è disegnata dal programma, strato sopra strato, ogni volta che cambi zoom, proiezione, banda, ora di previsione o tema. Ogni strato ha la sua casella nella barra Layer in alto sulla mappa, così puoi tenere solo quello che ti serve e vedere tutto insieme quando vuoi il quadro completo. L'ordine, dal basso verso l'alto, è questo:

| # | Strato | Contenuto | Casella |
| 1 | Mappa di base | Mare, terre, coste e confini | sì |
| 2 | Indice tropo | Media Hepburn + F5LEN, oppure una sola fonte; trasparenza regolabile | sì |
| 3 | Griglia locator | Campi, quadrati e, con lo zoom, sottoquadrati | sì |
| 4 | Isobare | Linee di pressione ogni 4 hPa, centri H e L | sì |
| 4a | Temperatura 850 hPa | Isoterme tratteggiate ogni 4 °C | sì |
| 4b | Inversione termica | Campitura arancio dove l'aria in quota è più calda che al suolo | sì |
| 5 | PSKReporter | Stazioni riceventi di PSKReporter (cerchi rosa scuro) e, in rosa, ciò che ascoltano | sì |
| 6 | Decodificati da me | Stazioni che decodifichi con WSJT-X, JTDX o MSHV: linee rosse dal tuo QTH | sì |
| 6a | QSO desunti dal live | Collegamenti chiusi (RR73, 73, RRR) letti nelle tue decodifiche: linea verde tra le due stazioni | sì |
| 7 | Spot | Collegamenti: linea blu piena, gialla tratteggiata per airscatter, due pallini | sì |
| 7a | Esclusioni | Casella Escludi sotto 100 km: il traffico più corto non si disegna né si elenca. Casella Oltre 1000 km: solo quadrati (accesa all'avvio): se una delle stazioni sta oltre 1000 km dal tuo QTH, niente linee né cerchi, solo i quadrati locator delle stazioni coinvolte in lilla tratteggiato a 45° | sì |
| 8 | QTH e etichette | La tua stazione, i nominativi dello spot selezionato | sempre visibile |

L'indice tropo sta subito sopra la base e sotto tutto il resto: è una campitura colorata, e messa più in alto coprirebbe linee e simboli. La griglia gli passa sopra, così sai sempre in quale quadrato cade un'area colorata. Le isobare sono linee sottili e si leggono bene sopra la campitura.

## 2. La mappa di base

Coste, confini e terre vengono dai dati vettoriali Natural Earth, di pubblico dominio [9], alla scala 1:50 milioni per la vista d'insieme e 1:10 milioni quando si ingrandisce. I file stanno nella cartella del programma: niente servizi di mappe online. Colori del tema Chiaro: mare azzurro chiaro #E2ECF8, coste blu scuro, confini grigio chiaro. Nei temi Nero e Notte i colori della base si scuriscono, negli altri temi si tingono leggermente.

## 3. Le due proiezioni

Tutti gli strati passano per la stessa funzione di proiezione, quindi restano sempre allineati tra loro. Si sceglie fra due proiezioni [10] (λ longitudine, φ latitudine, R raggio della Terra):

Mercatore. Conserva gli angoli e le forme locali; le distanze si allungano andando verso i poli. Si taglia a ±85° di latitudine.

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

Azimutale equidistante centrata sul tuo quadrato. Il centro (φ0, λ0) è il centro del tuo quadrato WWL. Distanze e direzioni misurate dal centro sono vere: una stazione a 1200 km sta a 1200 km in scala, e l'angolo con il nord è l'azimut a cui puntare l'antenna. Quando cambi locatore la mappa si ricentra.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

Nell'azimutale le linee che sulla carta sono dritte (meridiani, paralleli, bordi dei quadrati) diventano curve. Per questo tutte le linee vengono suddivise in tratti corti, ogni tratto viene proiettato e poi i tratti vengono uniti; lo stesso vale per i bordi dell'indice tropo e per le isobare. Il punto agli antipodi del centro non ha una posizione definita e resta fuori dalla vista.

## 4. Griglia locator, zoom e spostamento

La griglia Maidenhead è disegnata dal programma: campi di 20° × 10° con le due lettere grandi e sbiadite al centro, quadrati di 2° × 1°, e con zoom sufficiente sottoquadrati di 5′ × 2,5′. Le linee sono azzurre e sottili, per non coprire l'indice tropo.

Zoom con la rotella centrato sul puntatore (il punto sotto il mouse resta fermo), spostamento trascinando, doppio clic per tornare alla vista intera, tasti + e −, e il tasto con la percentuale per la vista intera. I dettagli si adattano allo zoom: coste più fini, sottoquadrati, etichette delle isobare più fitte.

## 5. Perché le inversioni piegano le onde

Nella bassa atmosfera la temperatura normalmente cala di circa 6,5 °C per km; in un'inversione invece sale con la quota, anche di 10–15 °C per km [3]. L'indice di rifrazione dipende sia dalla temperatura sia dall'umidità [3], e dove cambia bruscamente con la quota l'onda si piega verso terra e arriva più lontano del normale. Lo strato più importante è quello fino a circa 3 km, dove si formano inversioni, condotti e celle convettive [4]; i percorsi sul mare sono favoriti perché le masse d'acqua formano ampie zone di inversione [4].

La grandezza usata per misurarlo è la rifrattività N, cioè (n − 1) · 106 [11]:

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

T è la temperatura in kelvin, P la pressione ed e la pressione di vapore, entrambe in hPa.

Conta come varia N con la quota (dN/dh). In atmosfera standard vale circa −39 N/km; fino a −79 N/km la rifrazione è considerata normale, oltre si parla di super-rifrazione, e con gradienti positivi di sub-rifrazione [7]. Sotto −157 N/km l'onda viene piegata più della curvatura terrestre e resta intrappolata: è il condotto (ducting) [11]. Sono queste le condizioni che le mappe Hepburn e F5LEN cercano di prevedere.

## 6. L'indice tropo: dalle due mappe alla media

Le linguette Hepburn e F5LEN sono il banco di controllo: mostrano le immagini scaricate dalla pagina di ciascuna fonte, con l'ora di previsione modificabile, e servono a tarare la lettura cliccando sui punti di riferimento e sui colori della legenda. Lo strato Indice tropo resta vuoto finché almeno una delle due tarature non è attiva e completa.

Hepburn. L'indice di Hepburn misura quanto si prevede che le onde vengano piegate sopra una zona, su una scala lineare da 0 a 10 che può anche superare 10. 0 vuol dire atmosfera standard e solo scatter; 1–2 super-rifrazione marginale o debole; 3–4 super-rifrazione con zone di condotto; da 5 in su condotti sulla maggior parte dell'area, fino a 9+ “estremo” [1]. L'indice non tiene conto del tropo scatter e non vede le montagne [1]. Le mappe coprono 6 giorni: passi di 3 ore per le prime 36 ore, poi di 6 ore, aggiornate ogni giorno verso le 18 UTC [1]; i dati di partenza (temperatura, umidità, pressione) vengono dal modello globale del servizio meteorologico canadese [1].

F5LEN. Per l'Europa F5LEN pubblica 31 mappe da +3 a +192 ore (ogni 3 ore fino a +48, ogni 6 fino a +96, poi ogni 12), con la data e l'ora valide nel titolo, l'ora di produzione in basso a destra e una legenda a classi: Disturb, Normal, Minor, Weak, Good, Strong, Vy Strong, Duct [2]. La previsione mondiale di F5LEN è calcolata dall'indice di rifrazione atmosferico in unità N, su 40 passi di 3 ore (circa 5 giorni), aggiornata 4 volte al giorno; un clic sulla mappa mostra il valore medio e il locatore di quel punto [2].

Procedimento. Per ogni passo di previsione il programma esegue questi passaggi:

1. Scarico. Le immagini delle due previsioni vengono scaricate nella cache accanto all'eseguibile, una volta per aggiornamento della fonte, non a ogni ridisegno. Prima della versione definitiva va chiesto il permesso a Hepburn e a F5LEN.
2. Georeferenziazione automatica. Il programma trova la cornice (due linee orizzontali e due verticali dello stesso colore) e, su Windows 10/11, legge con l'OCR integrato nel sistema le etichette degli assi (54N, 10W, 0, 5E…). Ritaglia le strisce attorno alla cornice, le ingrandisce, prova entrambi i segni quando la lettera N/S/E/W non si legge e tiene la retta su cui cade il maggior numero di etichette: le letture sbagliate restano fuori. Da qui ricava gli estremi lat/lon della cornice e il tipo di proiezione (griglia regolare o Mercatore). Gli estremi si salvano e si rileggono da soli se la cornice cambia dimensione. Su Mac e Linux, senza OCR, valgono gli estremi di fabbrica o quelli scritti a mano nella linguetta. In alternativa, per le mappe che non hanno assi leggibili: ogni tipo di mappa (Hepburn Europa, F5LEN mondiale…) ha una taratura: alcuni punti di controllo con coordinate note (incroci di meridiani e paralleli, coste riconoscibili) collegano i pixel alla latitudine e longitudine, nella proiezione usata da quella fonte. La taratura sta in un file accanto al programma e si può rifare se una fonte cambia formato.
3. Lettura dei colori. Ogni pixel viene confrontato con la legenda della fonte e convertito nel valore numerico più vicino. Coste, scritte e griglie disegnate sulla mappa originale vengono riconosciute come “nessun dato” e riempite con i valori vicini.
4. Griglia mondiale. Ogni immagine, una volta trovata la cornice e noti i suoi estremi, è collocata sulla Terra; il valore in un punto qualsiasi del mondo si ricava dalle immagini che lo coprono. Se più regioni coprono lo stesso punto vale quella di dettaglio (più pixel per grado), purché la sua ora sia entro 1,5 h da quella scelta; poi l'ora più vicina, poi la corsa più recente. Dove nessuna immagine copre il punto la mappa mostra una velatura grigia: nessun dato, che è diverso da “indice 0”. Dentro la cornice, un pixel senza colore di legenda vale 0 (atmosfera normale); coste, confini e scritte vengono saltati guardando il colore più vicino entro 3 pixel.
5. Stessa scala. Hepburn è già sulla scala 0–10 [1]: sfondo scuro = 0 e dieci colori in ordine (viola, blu, verde acqua, lime, giallo, ocra, arancio, rosso, rosa, rosa chiaro) per 1…10, lilla oltre. F5LEN usa classi (Disturb, Normal, Minor, Weak, Good, Strong, Vy Strong, Duct) [2], ma alcune hanno esattamente i colori di Hepburn: Weak = 1, Strong = 5, Vy Strong = 7, Duct = 8; Minor vale 0,5 e Good (verde) 3,5. Il moltiplicatore di scala, nella linguetta F5LEN, corregge l'insieme per renderlo confrontabile con Hepburn.
6. Stessa ora. La barra temporale va da +0 a +192 h a passi di 3 ore. Per ogni passo si prende da ciascuna fonte la previsione valida all'ora più vicina; se una fonte non ha quel passo, la media usa solo l'altra e la legenda lo segnala.
7. Media. Con il pulsante Media ogni punto vale (Hepburn + F5LEN) / 2; con Hepburn o F5LEN si vede una sola fonte, utile per capire dove le due previsioni non sono d'accordo.
8. Disegno. Il valore viene colorato con la scala della legenda (0 blu, 2 verde, 4 giallo, 6 arancio, 8 rosso, 10 viola) e proiettato come campitura sotto la griglia. Lo 0 è trasparente, così dove non succede niente si vede la mappa di base pulita; la trasparenza generale si regola con il cursore accanto alla casella.

Nella scheda dello spot, l'indice lungo il percorso è la media dei valori della griglia campionati ogni 10 km sul cerchio massimo tra le due stazioni.

## 7. Le isobare

Le alte pressioni stabili sono il primo segnale da cercare per il tropo [3], e anche i collegamenti oltre le Alpi analizzati da IK3XTV sono avvenuti con alta pressione [4]. Per questo la pressione è uno strato a sé.

1. Dati. Pressione al livello del mare dal modello globale GFS della NOAA [12], letta tramite il servizio gratuito Open-Meteo [15] su una griglia di 3° × 3° (da 30° a 72° N, da 30° W a 48° E), per le prossime 78 ore. Il programma aggiorna i dati all'avvio e poi ogni 6 ore, come le corse del modello.
2. Linee. Le isobare si ricavano dalla griglia ogni 4 hPa (…1012, 1016, 1020…) con l'algoritmo dei quadrati marcianti (marching squares), poi vengono leggermente arrotondate e proiettate tratto per tratto.
3. Centri. I massimi e i minimi locali più marcati vengono segnati con H (blu) e L (rosso) e il valore in hPa.
4. Etichette. Il valore compare lungo ogni isobara, su uno sfondo del colore della mappa per restare leggibile sopra la campitura.

Zona e passo. I dati si scaricano sulla zona che si sta guardando, allargata del 25%, con un passo da 1° a 20° scelto per restare sotto 400 punti (Open-Meteo conta ogni punto come una richiesta: 600 al minuto, 5000 all'ora): ingrandendo o spostando la mappa fuori dalla zona già scaricata il programma la riscarica più fitta, al massimo ogni 10 minuti; se un aggiornamento non riesce restano i dati precedenti e si riprova dopo 3 minuti. In questo modo isobare e temperature funzionano in qualsiasi parte del mondo.

Temperature. Nella stessa richiesta arrivano la temperatura a 2 m, a 925 hPa e a 850 hPa. LA0BY mostra che l'evento del 22/11/2018 verso UA6 si vedeva nelle mappe della temperatura in quota (900–3000 m), ma non nelle previsioni Hepburn [3]; DF5AI e DK5YA usano i profili verticali di temperatura per riconoscere le inversioni [5]. Per questo ci sono due strati:

- Temperatura 850 hPa: isoterme tratteggiate ogni 4 °C (rosse sopra lo zero, blu sotto, viola sullo zero).
- Inversione termica: campitura arancio dove l'aria a 925 o 850 hPa è più calda di quella a 2 m; più è intensa, più l'inversione è forte. È la condizione fisica che piega le onde verso terra [3][11], e confrontarla con l'indice tropo e con le isobare mostra se le previsioni hanno una base meteorologica.

## 8. Spot e stazioni in ascolto

Spot. Ogni spot con entrambe le posizioni note è una linea sul cerchio massimo tra le due stazioni, suddivisa in tratti e proiettata; in Mercatore appare quindi leggermente curva, nell'azimutale è dritta se parte dal centro. Linea blu piena per lo spot normale, gialla tratteggiata con bordo scuro per l'airscatter (<AS>, <ACS> o “airscatter” nel commento), un pallino per ciascuna stazione, alone chiaro sotto la linea per staccarla dalla campitura. Lo spot selezionato è più spesso, disegnato per ultimo e con i due nominativi.

In ascolto. Ogni stazione ricevente di PSKReporter [16] è un cerchio vuoto rosa scuro. Da ciascuna partono linee rosa piene verso le stazioni che ha decodificato e rimandato in rete, sempre visibili senza cliccare, con i nominativi sempre scritti. Selezionando un cerchio le sue linee si fanno più spesse. Spot e ascolto mostrano solo la banda e l'intervallo scelti nell'elenco: cambiando banda la mappa si svuota e si ridisegna con i dati già raccolti per la nuova banda.

Decodificati da me. Ogni stazione che decodifichi con WSJT-X, JTDX o MSHV (ricevuta via UDP) è una linea rossa piena dal tuo QTH alla stazione, con pallino e nominativo. È il confronto più diretto con la previsione: mostra dove arriva davvero il tuo ricevitore in questo momento.

## 8 bis. Colori e simboli

Ogni colore ha un solo significato, uguale in tutti i temi (cambia solo lo sfondo).

| Colore e segno | Significato |
| Linea blu continua | Spot da DX cluster, RBN o DM4X tra due stazioni |
| Linea gialla tratteggiata con bordo scuro | Spot con commento di airscatter (riflessione su aerei) |
| Cerchio vuoto rosa scuro | Stazione in ascolto su PSKReporter (MQTT) |
| Linee rosa sottili dal cerchio, con un pallino rosa in fondo | Stazioni che quell'ascoltatore ha sentito, con il nominativo sempre scritto; linee più spesse per l'ascoltatore selezionato |
| Linea rossa e nominativo rosso | Le tue decodifiche UDP (WSJT-X, JTDX, MSHV): chi senti tu |
| Linea rossa a tratti che lampeggia | QSO in corso con quella stazione: ti ha chiamato e la chiusura non è ancora arrivata, oppure è il DX Call di WSJT-X/JTDX con la trasmissione abilitata. Torna continua con RR73, RRR o 73 |
| Linea verde | QSO desunti dal live: chiusura RR73, RRR o 73 vista sull'UDP |
| Linea nera tratteggiata lampeggiante con bordo bianco | Qualcuno ti sta chiamando: dura fino a RR73/RRR/73 o al massimo 3 minuti |
| Quadrati lilla tratteggiati a 45° | Traffico con almeno una stazione oltre la corona scelta: niente linee, solo i quadrati locator |
| Cerchi lilla tratteggiati con la distanza a nord | Corone di 1000, 700 e 500 km attorno al tuo QTH; quella scelta è più marcata |
| Cerchio e punto nel colore del tema | Il tuo QTH, con nominativo e locatore |
| Pallino arancio con la scritta GRAVES 143,050 | Il radar GRAVES (JN27SI) |
| Campitura da blu a rosso | Indice tropo Hepburn (0–10) o classe F5LEN, oppure la loro media; più caldo il colore, migliori le condizioni |
| Velo grigio chiaro | Zona senza dati tropo per l'ora scelta |
| Linee grigio-blu con il valore in hPa | Isobare ogni 4 hPa; H in blu alta pressione, L in rosso bassa pressione |
| Linee tratteggiate rosse, blu e viola | Isoterme a 850 hPa ogni 4 °C: rosse sopra zero, blu sotto zero, viola lo zero |
| Campitura arancio | Inversione termica: aria a 925 hPa più calda che al suolo, più intensa con l'inversione più forte |

Nelle Segnalazioni sospette il bordo di ogni scheda dice la regola in vigore: verde Buono, rosso Scarta, grigio nessuna regola.

A che cosa servono i quadrati lilla tratteggiati a 45°. Il traffico lontano dal tuo QTH ti interessa per capire dove la propagazione è aperta, ma le sue linee, lunghe centinaia di km e spesso a decine, coprono la mappa e nascondono quello che conta per te: le aperture che puoi sfruttare dalla tua stazione. Per questo, oltre la corona scelta, il programma non disegna le linee ma colora i quadrati locator (2° × 1°) delle stazioni coinvolte. Il risultato è una mappa di «zone attive»: dove compaiono molti quadrati lilla vicini, lì in questo momento c'è un condotto o un'apertura, anche se non ti riguarda direttamente; se la zona si allarga verso di te, è il segnale per puntare l'antenna in quella direzione. Il tratteggio a 45° lascia vedere sotto l'indice tropo e la griglia, così si confrontano a colpo d'occhio la previsione (il colore di fondo) e il traffico reale (il lilla). Il quadrato del tuo QTH non si colora mai, perché è il punto di partenza e non una zona lontana. Si colorano solo i quadrati che stanno tutti oltre la corona: un quadrato attraversato dalla corona, o al suo interno, resta libero anche se ci sta una delle due stazioni. Per vedere comunque le linee basta spegnere la casella o scegliere una corona più ampia.

I criteri che decidono che cosa arriva sulla mappa e che cosa viene scartato sono spiegati uno per uno nel documento 1, sezione 11 ter.

## 9. La barra temporale

In basso sulla mappa: data e ora della previsione mostrata, cursore da +0 a +72 h a passi di 6 ore, legenda dell'indice 0–10 con il nome della fonte scelta, e l'ora della corsa del modello per le isobare. Indice tropo e isobare seguono il cursore; spot, stazioni in ascolto e tue decodifiche sono osservazioni del momento e restano sempre quelli “adesso”. Quando il cursore non è su +0 vengono attenuati, per ricordare che non appartengono all'ora di previsione.

## 10. Come leggere la sovrapposizione

- Alta pressione + indice alto + spot lungo l'area colorata: l'apertura c'è ed è quella prevista. È il momento di chiamare in quella direzione.
- Indice alto ma nessuno spot: o nessuno è attivo lì, o la previsione è troppo ottimista. Lo strato In ascolto aiuta a distinguere: se ci sono riceventi nella zona e non sentono niente, la seconda ipotesi è più probabile.
- Spot lunghi fuori dalle aree colorate: possibile condotto elevato, che le previsioni di superficie non vedono, come nell'evento LA–UA6 [3]; oppure un'altra propagazione sfuggita al filtro (per esempio E sporadico senza tag). DF5AI e DK5YA distinguono tropo ed E sporadico anche dall'ora: aperture che nascono e finiscono insieme a una E sporadica diffusa difficilmente sono tropo [5].
- Hepburn e F5LEN in disaccordo: confronta le due fonti con i pulsanti e guarda dove cadono gli spot reali.
- Pressione in calo: G4MCU ha seguito un'apertura sul beacon SK6VHF finché la pressione è scesa; a 1012 hPa ha considerato l'evento finito [13]. Guarda le isobare scorrendo la barra temporale: se l'alta pressione si ritira, l'apertura probabilmente la segue.
- Segnali che salgono: nello studio WSPR a 144 MHz di KP4MD il rapporto segnale/rumore cresceva proprio nelle finestre in cui Hepburn prevedeva un miglioramento, e calava quando la previsione lo toglieva [14]. Anche i rapporti di PSKReporter contengono l'SNR: se sullo stesso percorso cresce, l'apertura si sta formando.
- Percorsi di montagna: l'indice non tiene conto dei rilievi [1], ma i collegamenti oltre le Alpi esistono anche grazie a celle convettive e onde di gravità atmosferiche [4], e LA0BY ha lavorato 9A e S5 attraverso le montagne senza che Hepburn lo prevedesse [3].

## 11. Punti aperti

- Permesso di Hepburn e F5LEN per usare le loro mappe dentro il programma.
- Taratura dei coefficienti di conversione F5LEN → scala 0–10.
- Taratura dei punti di controllo di ogni tipo di mappa, da rifare se le fonti cambiano grafica.
- Strato facoltativo con la temperatura in quota (850 hPa) dal GFS.

## Fonti principali

1. W. R. Hepburn, DX Info Centre, “The Hepburn Tropo Index (HTI)” e pagine “Tropospheric Ducting Forecast” (dxinfocentre.com). Definizione e scala dell'indice, valori negativi e oltre 10, esclusione dello scatter e dei rilievi, passi di 3 e 6 ore su 6 giorni, aggiornamento verso le 18 UTC. Origine dei dati dal modello globale canadese: G. Meisse, lista AMSAT-BB, 27/08/2007.
2. F5LEN, previsioni per l'Europa (tropo.f5len.org/forecasts-for-europe) e previsione mondiale della propagazione troposferica VHF (tropo.f5len.org/WW) e nota “New version of the worldwide VHF tropospheric propagation forecast tool”. Indice in unità N, 40 passi da 3 ore, 4 aggiornamenti al giorno, valore e locatore al clic.
3. S. Heck LA0BY, “Extreme Tropo Propagation on 144 MHz and up”, 41° Nordic VUSHF Meeting, Skjeberg 2019. Gradiente normale 6,5 °C/km e inversioni fino a 10–15 °C/km, ruolo di temperatura e umidità, alte pressioni stabili, evento LA–UA6 visibile nella temperatura in quota e non in Hepburn, collegamenti LA–9A/S5 oltre le montagne.
4. F. Egano IK3XTV, “Propagazione VHF – Il contributo delle celle convettive e delle onde gravitazionali atmosferiche nella propagazione Tropo”. Importanza dei primi 3 km, percorsi marini, celle convettive, onde di gravità sulle Alpi, collegamenti con alta pressione, volume comune fino a 3–4 km.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, “Very Long Distance Propagation in the 144 MHz Band”, 24/7/2003. Profili verticali di temperatura e punto di rugiada per riconoscere le inversioni, uso dell'indice Hepburn, distinzione tra tropo ed E sporadico in base all'ora.
6. J. N. Gannaway G3YGF, “Tropospheric Scatter Propagation”, QST novembre 1983. Raggio terrestre equivalente e perdita di percorso usati per l'indice e la scheda dello spot (vedi documento 1).
7. The VHF/UHF DX Book, capitolo 2 “Tropospheric propagation”, pp. 2-23…2-30. Figura delle zone di rifrazione in funzione di dN/dh (standard −39 N/km, limiti −79 N/km e 0) e profili di condotto superficiale ed elevato. Il testo della copia fornita non è leggibile: sono state usate solo le figure.
8. Riepilogo di progetto dell'8/11/2026, fornito dall'autore del programma. Colori della mappa, proiezioni richieste, strati, barra temporale.
9. Natural Earth, dati vettoriali di coste e confini, pubblico dominio (naturalearthdata.com). Riferimento generale, non allegato.
10. J. P. Snyder, “Map Projections – A Working Manual”, USGS Professional Paper 1395, 1987. Formule delle proiezioni di Mercatore e azimutale equidistante. Riferimento generale, non allegato.
11. Raccomandazione ITU-R P.453, indice di rifrazione radio. Formula della rifrattività N e soglia di condotto a −157 N/km. Riferimento generale, non allegato.
12. NOAA NCEP, Global Forecast System (GFS). Pressione al livello del mare a 0,25°, quattro corse al giorno. Riferimento generale, non allegato.
13. G4MCU, articoli con il tag “tropospheric” (g4mcu.net/tag/tropospheric), 2018–2025. Rifrazione, condotto e scatter; condotti più frequenti con alta pressione, specie sul mare; beacon SK6VHF seguito fino al calo della pressione a 1012 hPa (giugno 2019).
14. C. F. Milazzo KP4MD, “VHF/UHF WSPR Propagation Study” (qsl.net/kp4md/144_mhz_wspr.htm), 2012–2014. SNR dei rapporti WSPR a 144 MHz in crescita nelle finestre di miglioramento previste da Hepburn e in calo quando la previsione scompare.
15. Open-Meteo, API di previsione meteo (open-meteo.com), uso non commerciale gratuito. Pressione al livello del mare del modello GFS su più punti in una sola richiesta.
16. mqtt.pskreporter.info, distribuzione MQTT in tempo reale degli spot di pskreporter.info: nominativo e locatore di chi riceve (vedi documento 1).
