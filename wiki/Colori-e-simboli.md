# Colori e simboli

Ogni colore ha un solo significato, uguale in tutti i temi (cambia solo lo sfondo).

| Colore e segno | Significato |
|---|---|
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

I criteri che decidono che cosa arriva sulla mappa e che cosa viene scartato sono spiegati uno per uno nella pagina [[Criteri di scarto|Criteri-di-scarto]].
