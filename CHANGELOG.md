# LIVETROPOVHF – storia delle versioni

Le versioni 0.x sono beta: compilate dall'autore per prova, non pubblicate come eseguibili. Dalla 1.0.0 il programma si scarica già pronto dalle Release di GitHub e si aggiorna da solo.
Numerazione: la cifra centrale sale con le funzioni nuove, l'ultima con le sole correzioni.
Fino alla 0.3.0 il programma si chiamava "Previsione Tropo VHF" (TropoVHF).

## 1.3.2 — 21/11/2026
- Previsioni: due stime affiancate nel calcolo punto-punto, prudente (soglia di super-rifrazione di Bean e Dutton, 100 N/km nello strato basso) e aggressiva (soglia classica di super-rifrazione, 79 N/km). La mappa usa la prudente.
- Previsioni: strati e condotti cercati solo fino a 1500 m sopra il suolo.
- Previsioni: +1 dB con alta pressione estesa (almeno 7 celle su 9 a 1020 hPa o più) e vento debole nei bassi strati (10 m sotto 5 m/s, 850 hPa sotto 8 m/s).
- Calcolo punto-punto: peso del percorso su mare con il μ1 di ITU-R P.452 (× 0,8–1,2); quota delle due stazioni confrontata con base e cima del condotto anche quando copre solo parte della tratta; +1,5 dB al massimo quando la cima dello strato limite del GFS supera le creste lungo la tratta (osservazione di IT9GSF).
- Alternativa (spenta per difetto): profilo normale CRPL con l'Ns del punto (−ΔN = 7,32·e^(0,005577·Ns), Bean e Thayer 1959) al posto di 40 N/km fissi, anche come riferimento dell'equazione parabolica.
- Equazione parabolica: dominio di almeno 8 km, assorbitore sulla metà alta, fascio di al massimo 6°. Prima il campo oltre l'orizzonte era rumore numerico.
- Validazione: le novità si provano una alla volta sui 593 collegamenti del 29/12/2021–2/1/2022.
- Le voci a mano che il programma inseriva da sé (IV3/HB9CAT, IC8JHY, I8/IZ3CGC, IU5SPA, IZ6FLS, SWL1RUCAS, IZ1ERR, IK0SRL) ora stanno solo nel CALL3.TXT del pacchetto; alla prima apertura si tolgono dal file delle voci a mano, se non sono state cambiate.
- Le caselle (per esempio «Connetti all'avvio») si salvano subito in LIVETROPOVHF.ini, non solo alla chiusura del programma.

## 1.3.1 — 21/11/2026
- Previsioni: senza un condotto che intrappola la banda il guadagno non supera più 16 dB (fine del giallo, «strato forte, nessun condotto»): il termine di stabilità di Misme e la subsidenza non portano più al rosso o al viola le zone senza condotto. Rosso e viola restano solo per i condotti.
- Previsioni: condotto «morbido» (Kerr, Hepburn, VHF/UHF DX Book): uno strato che non intrappola del tutto la banda, con λ fino a 3 volte la lunghezza d'onda massima, dà un guadagno graduale fra 15 dB e quello del condotto pieno, invece di fermarsi a 15 dB.
- Previsioni: persistenza: fino a +2 dB dove nelle 24 ore precedenti la stessa zona era già sopra 13 dB (le aperture crescono dopo giorni di alta pressione).
- Previsioni: la sorgente automatica parte da NOMADS (NOAA, nessun limite giornaliero) e chiede a Open-Meteo solo i punti che mancano, o tutti se NOMADS non risponde.
- Previsioni: con griglie larghe (passo da 0,5° in su) NOMADS scarica i file GFS a 0,5° o 1° ogni 3 ore invece di quelli a 0,25° orari: molti meno dati (prima una griglia a passo 3° chiedeva circa 117 MB). Le ore intermedie prendono il profilo più vicino.
- Previsioni: con la griglia a passo oltre 1° il calcolo del segnale scarica i profili dei punti della tratta (con NOMADS solo l'ora richiesta) invece di usare celle larghe centinaia di km; nel registro un profilo uguale per più punti si scrive una volta sola.
- Calcolo del segnale: volume comune (quota e posizione della base del volume di scatter, aria del GFS a quella quota) e distanza equivalente di Bray.
- Calcolo del segnale: equazione parabolica (split-step di Fourier) nel profilo M del GFS lungo la tratta, sul terreno SRTM1, solo come confronto; nella validazione il guadagno del profilo è confrontato fra i giorni dell'apertura e i giorni normali.
- Registro dei calcoli: ogni calcolo riporta nell'intestazione la versione del programma e le opzioni del modello spente.
- Previsioni: il registro scrive all'inizio di ogni aggiornamento la sorgente dei dati usata; se la sorgente è fissata su Open-Meteo e arriva il limite, il messaggio suggerisce NOMADS.
- Previsioni: nuovo caso storico 29 dicembre 2021 – 2 gennaio 2022 (Tirreno e Mediterraneo occidentale, 144 MHz).
- Validazione del modello su 593 collegamenti reali di quei giorni (solo ore e locatori, dai rapporti MMMonVHF): il modello separa i giorni dell'apertura dagli stessi giorni due settimane dopo (indice 0,79 senza correzioni, 0,91 con Misme e subsidenza). Il condotto sul mare a bassa quota non è visibile ai livelli del GFS.
- DX cluster S50CLX: nuovo indirizzo s50clx.si:41112; l'indirizzo vecchio salvato si corregge da solo.

## 1.3.0 — 21/11/2026
Previsioni (sempre sperimentali):
- Seconda sorgente dei dati: con la scelta automatica, quando Open-Meteo rifiuta per un limite, i punti mancanti arrivano da NOMADS (NOAA), lo stesso GFS in file GRIB2 letti dal programma. Si può anche scegliere solo Open-Meteo o solo NOMADS.
- Altri modelli per tutta la griglia: ICON-EU (DWD) ed ECMWF IFS 0,25° da Open-Meteo, per confrontarli con il GFS. Cache separata per modello; la sorgente usata è scritta nei risultati.
- Casi storici: si rivede un'apertura documentata con le previsioni d'archivio di Open-Meteo. Casi: 14–15 agosto 2022, Manitoba–Kansas/Missouri (QST novembre 2022), da 144 a 1296 MHz.
- Correzioni meteo (si possono spegnere): termine di stabilità di Misme e moto verticale del GFS: +1,5 dB con subsidenza negli anticicloni, −2 dB con aria in salita (Moler e Holden 1960). Indice di rifrazione potenziale K nel registro.
- Scirocco: segnalato sulla mappa e nei risultati quando a 850 hPa arriva aria calda da sud-est sul Mediterraneo.
- Casella Segni meteo: S scirocco, ↓ subsidenza, ↑ aria in salita sulle celle della griglia.
- Troposcatter con tre modelli affiancati: P.452, ITU-R P.617-5 con N0 e ΔN del GFS ora per ora, formula RSGB (DL2FZN); la differenza è mostrata come incertezza.
- Modi digitali nei risultati: FT4, FT8, Q65 e JT65 con le soglie misurate; in troposcatter la soglia FT8 tiene conto dell'evanescenza (−18,6 dB).
- Le ore della griglia ora combaciano anche quando alcuni punti vengono dalla cache scaricata un'ora prima.
- Consigli per l'operatore: situazioni meteo (anticicloni, fronti, brezza di terra, foehn, scirocco) dall'ARRL Handbook. Fumetto delle Yagi con il confronto con le misure NBS.

## 1.2.3 — 20/11/2026
- Linguette riordinate da 12 a 5: Mappa e spot, Previsioni (Propria β · Hepburn · F5LEN), GRAVES, Dati e fonti (DX cluster · PSKReporter · Reverse Beacon · UDP · Nominativi · Segnalazioni sospette), Guide (cinque documenti, con la ricerca). Ogni gruppo ricorda l'ultima sotto-linguetta.
- Windows 7 e 8.1: anche qui la linguetta Previsioni (sperimentale), con lo stesso motore, gli stessi testi, fumetti e disegni della versione normale; manuale delle previsioni e consigli per l'operatore.
- Registro degli errori sempre attivo (Dati/errori.log): versione, sistema, linguetta aperta, errore completo e ultime righe dei calcoli. Gli errori dell'interfaccia non chiudono più il programma: compare «⚠ errore annotato» nella barra di stato. All'avvio successivo il programma propone di inviare il rapporto con una segnalazione GitHub già compilata (nominativo, locatore e nome utente del PC tolti).
- Aggiornamenti: primo controllo pochi secondi dopo l'avvio (subito dopo un errore); clic sulla versione nella barra di stato per controllare adesso; a installazione finita una finestra propone di riavviare. Le Release marcate [obbligatorio] fanno riavviare comunque.
- Barra di stato: i pallini delle fonti si cliccano e portano alla fonte; il fumetto dice perché è spenta (porta rifiutata, nodo del cluster fermo, nessuna risposta, UDP senza pacchetti). Riconosciuto il caso «Error on connect to 127.0.0.1» dei nodi DX cluster fermi.
- Previsioni: griglia GFS salvata sul disco per ogni corsa del modello; con la stessa corsa i punti già scaricati non si chiedono più a Open-Meteo. Il rifiuto per limite si legge: al minuto si attende, all'ora o al giorno ci si ferma subito con l'ora a cui riprovare.
- Previsioni: con un locatore a 4–6 caratteri e quota sul mare 0 il calcolo non si blocca più: la quota si stima dal suolo SRTM al centro del quadrato più l'altezza dal suolo, con un avviso nei risultati.

## 1.2.2 — 20/11/2026
- Previsioni: il pannello di sinistra si allarga o si stringe trascinando il bordo; il registro dei calcoli va a capo da solo e la sua altezza si regola trascinando la barra sotto (misure salvate).
- Previsioni: casella «Mostra anche 3–7 dB» con un tratteggio leggero sotto la scala dei colori; riquadro della zona coperta dalla previsione propria con il guadagno massimo visibile, per distinguere una mappa vuota perché l'atmosfera è normale da una senza dati.
- Previsioni: un secondo clic su Aggiorna previsione non interrompe più lo scaricamento in corso.

## 1.2.1 — 20/11/2026
- Previsioni: con una griglia scaricata il programma poteva chiudersi con l'errore «Visual was invalidated during the render pass» (la riga dell'accordo fra guadagno, pressione e inversione veniva aggiornata mentre si disegnava la mappa). Corretto.
- Previsioni: pulsante per scaricare le mappe ITU-R P.453 dal sito dell'ITU per uso personale (con le condizioni d'uso), estratte in Dati/P453; il calcolo B dei condotti si accende senza riavviare. Riconosciuti anche i nomi dei file dell'archivio ITU (E_OCCURRENCE, E_THICKNESS, E_STRENGTH…).

## 1.2.0 — 20/11/2026
Nuova linguetta **Previsioni (sperimentale)**: previsione troposferica calcolata dal programma stesso, oltre alle mappe Hepburn e F5LEN. I calcoli sono in fase di verifica: servono a confrontare situazioni, non garantiscono un collegamento.
- Mappa del guadagno stimato sulla zona visibile, dai profili verticali GFS (Open-Meteo, 1000–500 hPa) con le soglie di Bean e Dutton e i condotti con la formula di Kerr; bande 144, 432 e 1296 MHz; fino a 72 ore avanti.
- Scala dei colori fissa in dB, uguale con tutti i temi: sotto 7 dB nessun colore, poi azzurro, verde, giallo, arancio, rosso e viola da 21 dB. Griglia dei locatori più scura, sopra i colori.
- Linee opzionali: curve di guadagno, isobare GFS, curve dell'inversione termica; riga con l'accordo numerico fra guadagno, pressione e inversione.
- Calcolo del segnale fra la propria stazione e un corrispondente: locatore a 4–10 caratteri (8–10 attivano SRTM1), terreno SRTM1, diffrazione delta-Bullington, prima zona di Fresnel, troposcatter P.452, strato fondante continuo lungo la tratta (quota, inclinazione, terreno e terra coperti), condotti con accoppiamento e attenuazione residua, confronto con ITU-R P.452 al 20%, Yagi DL6WU, rumore ITU-R P.372, S/N in SSB, CW e FT8.
- Condotti potenziali con doppio calcolo (salto in 50 m e spessore climatologico ITU-R P.453): si usa il peggiore. Le mappe P.453 sono © ITU e non si ridistribuiscono: chi vuole il secondo calcolo le scarica dall'ITU in Dati/P453.
- Copertura in ottica sul terreno SRTM1; tempi di risposta di ogni calcolo; registro dettagliato dei calcoli (Dati/previsioni-calcoli.log).
- Fumetti con la fonte e il richiamo al manuale su ogni dato dei risultati.
- Nuovi documenti: Manuale delle previsioni e Consigli per l'operatore.
- Locatore della propria stazione a 8 o 10 caratteri nella riga in alto.
- Windows 7 e 8.1: il programma resta lo stesso della 1.0.3 con le correzioni comuni; la linguetta Previsioni per ora c'è solo nelle versioni per Windows 10/11, Linux e Mac.

## 1.0.3 — 18/11/2026
- Windows 7 e 8.1: al primo avvio, senza nominativo, l'immagine di partenza restava sopra la finestra che chiede il Call e il programma sembrava bloccato (segnalato da un tester su Windows 8). Ora l'immagine si chiude sempre dopo 2,5 secondi (o subito con un clic), non sta più in primo piano e solo dopo compare la richiesta del nominativo.
- Windows 7 e 8.1: con finestre strette le linguette vanno a capo invece di uscire dal bordo.
- Ringraziamenti: I5MZY Massimo Zannoni per le prove su Windows 7 e 8 (manuale, README, wiki).

## 1.0.2 — 16/11/2026
- Windows 7 SP1 e 8.1: versione riscritta apposta, nativa per **Microsoft .NET Framework 4.8** con finestra WinForms (cartella Win7 dei sorgenti). Anche i pacchetti .NET 6 della 1.0.1 non partivano su alcuni Windows 7. Il motore (cluster, PSKReporter, UDP, CALL3, previsione tropo, criteri di scarto, sospetti, aggiornamenti) e il disegno della mappa e di GRAVES sono gli stessi della versione normale; cambiano solo la finestra e il disegno, fatto con GDI+. Stesse linguette e stesse 36 lingue.
- Serve .NET Framework 4.8 (su Windows 10 e 11 c'è già; su Windows 7 SP1 e 8.1 si installa da Microsoft): l'installatore controlla e, se manca, apre la pagina di download.
- Nella versione per Windows 7 manca solo il banco di taratura manuale delle mappe Hepburn/F5LEN (resta l'anteprima con le regioni); l'OCR degli assi già non c'era.
- I pacchetti -windows7-x64 e -windows7-x86 (setup e zip portatile) sono ora questa versione; si aggiornano da soli con i pacchetti windows7.

## 1.0.1 — 14/11/2026
- Windows 7 SP1 e 8.1: la 1.0.0 non partiva («Impossibile trovare il punto di ingresso GetSystemTimePreciseAsFileTime in KERNEL32.dll», funzione che esiste solo da Windows 8). Nuovi pacchetti -windows7-x64 e -windows7-x86 (setup e zip portatile) compilati su .NET 6, l'ultimo che gira su Windows 7; si aggiornano da soli con i pacchetti windows7. Su questi sistemi manca soltanto la lettura OCR degli assi delle mappe (serve Windows 10).

## 1.0.0 — 13/11/2026
Prima versione pubblica.
- Installatori e pacchetti creati su GitHub come per YA-MS: Windows (x64, x86, ARM64; installatore e zip portatile), Linux (.deb, .rpm, Arch, Slackware, AppImage, tar.gz), Raspberry Pi (64 e 32 bit), Alpine, Mac (Apple Silicon e Intel).
- Aggiornamento automatico e silenzioso dalle Release (ogni 30 minuti; nuova versione attiva al riavvio).
- Immagine di partenza con il numero di versione; icona del programma.
- Finestra di chiusura con conferma e ringraziamento (come YAEC).
- Interfaccia completa in 36 lingue.
- Dati accanto all'app anche sul Mac.

## 0.4.0 — 13/11/2026 (beta)
- Nuovo nome: **LIVETROPOVHF** (eseguibile LIVETROPOVHF.exe). Le impostazioni di TropoVHF.ini e l'archivio TropoVHF.db vengono ripresi da soli al primo avvio.
- Interfaccia in 36 lingue, come YA-MS.
- CALL3.TXT aggiornabile da MMMonVHF (pulsante e controllo automatico ogni 7 giorni, con copia di sicurezza CALL3.bak).
- Correzione automatica dei locatori dal cluster: negli spot automatici DM4X/RBN il locatore a 6 caratteri dichiarato da chi spotta corregge il database (Dati/CALL3-CLUSTER.TXT).
- Nuove voci a mano iniziali (I8/IZ3CGC, IU5SPA, IZ6FLS) e CALL3.TXT del pacchetto aggiornato.
- Il quadrato del proprio QTH non si colora mai in lilla.
- Quadrati lilla solo se interamente oltre la corona scelta.
- MSK144, FSK441 e JTMS accettati entro 250 km.
- Stazioni in ascolto come cerchi (più grandi), pallini delle stazioni sentite più grandi, nominativi sempre scritti sulla mappa.
- QSO in corso dall'UDP: linea rossa tratteggiata e lampeggiante.
- Seconda fonte di cluster: 9A0DXC e S50CLX al posto di GB7RAU (credenziali rimosse).
- Locatori: G7VRD come ultima fonte, in sottofondo.
- Aggiornamenti automatici da GitHub ogni 30 minuti.
- Manuali: criteri di scarto e colori spiegati per intero.

## 0.3.1 — 10/11/2026
- Finestra adatta ai notebook: avvio al 90% dello spazio utile, massimizzata sotto 1280 × 720, posizione e dimensione ricordate, barra del titolo sempre visibile, minimo 900 × 560.
- Confini nazionali più spessi ed evidenti.
- prepara-sdk-win64.bat: installa il .NET 10 SDK accanto al progetto se manca; build-win64.bat lo richiama da solo.

## 0.3.0 — 10/11/2026
- Corone di 1000, 700 e 500 km attorno al QTH, con raggio a scelta per il criterio dei quadrati lilla.
- Chiamate dirette dall'UDP locale: linea nera tratteggiata lampeggiante verso chi chiama, fino a RR73/RRR/73 o al massimo 3 minuti.

## 0.2.4 — 09/11/2026
- Sottolinguetta Nominativi: voci a mano e file che si sovrappongono a CALL3.TXT.
- Nominativi con la barra (IV3/HB9CAT) corretti nei dati MQTT di PSKReporter.

## 0.2.3 — 09/11/2026
- Criterio dei 1000 km riferito al proprio QTH: il traffico con una stazione oltre la corona si mostra solo come quadrati lilla tratteggiati.

## 0.2.2 — 09/11/2026
- Criteri di esclusione generale: casella sotto 100 km e quadrati lilla per il traffico lungo.

## 0.2.1 — 09/11/2026
- Linguetta GRAVES (dal progetto YA-MS) e linguetta "Come si forma la tropo".
- Linguetta Segnalazioni sospette con auto-apprendimento: EME in Q65/JT65 riconosciuto con la posizione della Luna, quarantena dei collegamenti lunghi, ascoltatori, locatori diversi, troncati o di skimmer.
- Locatori a 6 caratteri imparati dal vivo (Dati/LOC6.TXT); suffissi -SDR, /SDR, -# per il locatore.

## 0.2.0 — 08/11/2026
- Regioni Hepburn e F5LEN in tutto il mondo, con estensioni di fabbrica, anteprima delle immagini fuse e lettura OCR degli assi.
- Isobare, isoterme a 850 hPa e inversione termica (Open-Meteo), con limiti di richieste rispettati.
- Quarantena dei collegamenti oltre 1000 km e svelamento per similitudine; riabilitazione dai DX cluster umani.
- Esclusione di meteor scatter (MSK144, FSK441, JT6M, JTMS, ISCAT), QO-100 e di tutto sotto i 143 MHz tranne GRAVES.
- QSO desunti dall'UDP (RR73/73/RRR); locatori sempre a 6 caratteri da CALL3.TXT; richiesta del nominativo all'avvio.

## 0.1.x — novembre 2026
- Prime beta del porting in C# .NET 10 + Avalonia: mappa, spot da DX cluster, RBN, DM4X, PSKReporter MQTT, WSJT-X UDP, archivio SQLite, temi, 28 lingue, manuali con fonti, mappe Hepburn e F5LEN tarate.
