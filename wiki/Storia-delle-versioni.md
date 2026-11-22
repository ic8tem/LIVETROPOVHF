# LIVETROPOVHF – storia delle versioni

Le versioni 0.x sono beta: compilate dall'autore per prova, non pubblicate come eseguibili. Dalla 1.0.0 il programma si scarica già pronto dalle Release di GitHub e si aggiorna da solo.
Numerazione: la cifra centrale sale con le funzioni nuove, l'ultima con le sole correzioni.
Fino alla 0.3.0 il programma si chiamava "Previsione Tropo VHF" (TropoVHF).

## 1.3.4 — 22/11/2026
- Previsioni: riquadro «Dettagli da mostrare» a destra della mappa con tre livelli (Essenziale, Normale, Completo); linguetta Previsioni tradotta in tutte le lingue; slovacco aggiunto.
- Colori come YA-NEC: i 18 temi comuni ai programmi di IC8TEM (Chiaro, Notte, Nero, otto pastelli, Scia meteorica, i tre di IP-TRX-CTRL, Blu, Viola, Verde), con i nomi in tutte le lingue; il Chiaro con l'aspetto di YA-NEC (fondo #F0F0F0, riquadri bianchi bordati, fascione blu notte #1E293B, accento #1F6FD1). La scala della previsione non cambia con il tema.
- Schermi 1366×768 (anche Windows 7): sotto 1500 pixel di larghezza l'orologio mostra solo l'ora, e i comandi di tema, testi e lingua restano nella riga in alto.
- Fonti: Moler e Holden 1960 e Paulus 1984 letti in originale; corrette le descrizioni di Moler e Holden nei manuali e nei fumetti (2,5 dB medi su microonde sul mare, non «20 dB e oltre negli anticicloni»); stato dei correttivi aggiornato.
- Un solo database dei nominativi: CALL3.TXT. Voci a mano, elenchi importati da file, correzioni dal cluster (DM4X, RBN), locatori imparati dal vivo e risposte di G7VRD stanno tutti dentro CALL3.TXT, che resta nel formato di JTDX/WSJT-X e si può usare anche in WSJT-X, JTDX e MSHV. Niente più Dati/CALL3-MANUALE.TXT, Dati/CALL3-AGGIUNTE, Dati/CALL3-CLUSTER.TXT, Dati/LOC6.TXT e Dati/CALL3-G7VRD.TXT: al primo avvio vengono fusi in CALL3.TXT con le stesse precedenze e rinominati .fuso.
- In cima a CALL3.TXT due blocchi segnati da un commento, // [LIVETROPOVHF-MANUALI] e // [LIVETROPOVHF-CLUSTER]: restano agli aggiornamenti dalle fonti. Ogni nominativo compare una volta sola. Nell'aggiornamento un locatore a 6 caratteri resta dove una fonte ne ha solo 4 dello stesso quadrato.
- Sottolinguetta Nominativi: Aggiungi un file… scrive l'elenco fra le voci a mano; Ricarica rilegge CALL3.TXT dal disco; ✕ Togli toglie il nominativo da CALL3.TXT. Testi aggiornati in tutte le 36 lingue.
- Gli installatori per Linux (installa.sh, AppImage, deb, rpm, Arch, Slackware) non sovrascrivono più un CALL3.TXT già presente; quello per Windows lo installava già solo se mancava.
- Previsione mediata dagli spot, meno ottimista: il minimo garantito (prima fino a 16 dB su tutto il rettangolo del corridoio) vale solo in una fascia di ±60 km attorno alla parte centrale delle tratte, non sulle stazioni; cresce con la distanza, con il numero di tratte indipendenti e con l'S/N mediano (PSKReporter, decodifiche UDP). Le tratte oltre 1500 km contano solo se confermate da due fonti diverse. Manuale delle previsioni, § 4 bis.
- Corridoi: escluse le stazioni «a raggiera» (almeno 5 corrispondenti oltre 1000 km in un'ora su un ventaglio di almeno 150°), quasi sempre ricevitori con la frequenza configurata male; nell'archivio del 17–22/11 erano il 72% delle ricezioni in 2 m oltre 1000 km. Manuale delle previsioni, § 4 bis.
- Corridoi corti meno ottimisti: fra 300 e 600 km il 2 m passa quasi ogni giorno per troposcatter, quindi il minimo garantito parte da 3 dB fino a 400 km, sale a 10 dB a 700 km e a 16 dB da 900 km; sotto i 7 dB il corridoio si disegna ma non colora la mappa. Manuale delle previsioni, § 4 bis.
- Mappa: i corridoi si disegnano in un colore neutro, tutti in una volta (le sovrapposizioni non diventano più un viola pieno, che sembrava la fascia dei 21 dB), con etichette che si scansano e le tratte visibili passando con il mouse sull'etichetta. I nomi dei quadrati (JN70) si scrivono solo quando il quadrato è largo almeno 60 pixel; sotto, le lettere dei campi: nei temi scuri coprivano la mappa come mattoncini.

## 1.3.3 — 21/11/2026
- Seconda fonte di cluster: DXMAPS (dxmaps.com:7300, con gli spot dello skimmer RBN) al posto di 9A0DXC, che non risponde più; l'indirizzo salvato si aggiorna da solo. Cluster PSKReporter (dm4x) connesso all'avvio e CALL3.TXT aggiornato da solo ogni giorno, di default.
- Linguetta Previsioni: caselle per il profilo a terra piatta, tutte le righe del calcolo (di default solo la sintesi) e il registro dei calcoli (di default nascosto); fumetti su tutti i bottoni; il cursore dell'ora segue l'orologio.
- Contrasto aria-mare: sul mare la temperatura potenziale a 925 hPa e l'aria a 2 m si confrontano con la SST (da −3 a +3 dB), con la memoria del calo della SST nelle 72 ore prima dalle analisi Open-Meteo (mari bassi come l'alto Adriatico dopo la bora). Manuale § 9 quater sull'Adriatico.
- Manuale delle previsioni: approfondimento su isole, Bocche di Bonifacio e discontinuità terra-mare (§ 10 quinquies), richiamato nei consigli.
- Previsione mediata dagli spot: corridoi d'apertura da almeno 3 tratte parallele indipendenti (cluster, PSKReporter, QSO, decodifiche e salti APRS in 2 m e LoRa 433/868 MHz da APRS-IS, scartando le ritrasmissioni di digipeater non tracciate), con persistenza di 10 minuti; sotto il corridoio la previsione dell'ora attuale non scende sotto 10–16 dB. Curve di guadagno senza «gocce». Profilo a terra piatta con raggio curvo e fumetto sullo strato. Testi scalabili 80–150% accanto al tema.

## 1.3.2 — 21/11/2026
- Rilievi vicini alle stazioni (entro 20 km, criterio dell'autore): nel troposcatter costano al massimo 6 dB per lato invece degli 0,573 dB per mrad di P.452.
- Terreno: nuova fonte Copernicus DEM GLO-30 (GeoTIFF letto dal programma). I riquadri Terrain Tiles usati prima avevano vicino alla costa rilievi fantasma di 80–100 m in mare, che davano orizzonti falsi alle stazioni costiere (a Capri +2,6° verso nord-ovest, circa 26 dB di troposcatter in più). I vecchi riquadri restano come riserva.
- Accordo guadagno–pressione: se il livello delle isobare non è caricato si usa la pressione al mare della previsione propria (prima il registro dava «0 punti»).
- Registro: niente più «-+0.0 cm/s»; se il sito dell'ITU non si raggiunge il messaggio dice perché (internet, DNS o firewall) invece di stampare l'errore tecnico; dimensione dei riquadri SRTM1 corretta (7–13 MB).
- Via monte: locatore a 10 caratteri per ogni punto e nuova casella «Via monte senza perdita fino al monte», che porta la stazione sul punto migliore e calcola solo la tratta dal punto al corrispondente (segnale e S/N).
- Risultati: i valori lunghi vanno a capo invece di essere tagliati, spazio per la barra di scorrimento; perdite oltre 300 dB scritte «> 300 dB» con «direzione chiusa da un ostacolo vicino»; niente più «-+0,0»; la curvatura della terra sul mare non è più chiamata «ostacolo 0 m». Profilo: fumetto sul raggio diretto rosso/verde e sui punti rossi della zona di Fresnel.
- Segni meteo sulla mappa grandi il doppio e colorati per effetto (↓ verde subsidenza favorevole, ↑ rosso aria in salita sfavorevole, S arancio scirocco), con un fumetto al passaggio del mouse; tabella nel manuale delle previsioni (§ 9 bis).
- ITM (Longley-Rice) 1.2.2 dell'NTIA tradotto in C#: nella linguetta Previsioni c'è una riga con la perdita in atmosfera normale (50%, 10% e 1% del tempo) accanto a P.452, e una riga «Uscita dal condotto» quando il condotto finisce prima del corrispondente (tratto finale calcolato con l'ITM). La compilazione automatica controlla che i numeri siano uguali a quelli del codice originale.
- Alla prima partenza la lingua si prende dal sistema operativo (Windows, Linux, macOS); se non è fra le 36 disponibili si parte in inglese. Prima si partiva sempre in italiano.
- Nuova sorgente: ICON-2I Italia, 2 km (Agenzia ItaliaMeteo e Arpae) attraverso Open-Meteo; utile per coste, isole e condotto d'evaporazione. Le variabili che un modello non fornisce si tolgono da sole dalla richiesta.
- Avvio: disclaimer sul calcolo semi-probabilistico dei condotti e sui puntamenti consigliati, completo nella finestra di nominativo e locatore al primo avvio, breve a ogni avvio successivo (si chiude da solo dopo 10 s, con il collegamento al manuale «Fonti e formule»); in tutte le 36 lingue. Il locatore accetta 10 caratteri anche su Windows 7.
- Previsioni: due stime affiancate nel calcolo punto-punto, prudente (soglia di super-rifrazione di Bean e Dutton, 100 N/km nello strato basso) e aggressiva (soglia classica di super-rifrazione, 79 N/km). La mappa usa la prudente.
- Previsioni: strati e condotti cercati solo fino a 2500 m sopra il suolo (con 1500 m la validazione perdeva le inversioni invernali da subsidenza).
- Previsioni: +1 dB con alta pressione estesa (almeno 7 celle su 9 a 1020 hPa o più) e vento debole nei bassi strati (10 m sotto 5 m/s, 850 hPa sotto 8 m/s).
- Calcolo punto-punto: peso del percorso su mare con il μ1 di ITU-R P.452 (× 0,8–1,2); quota delle due stazioni confrontata con base e cima del condotto anche quando copre solo parte della tratta; +1,5 dB al massimo quando la cima dello strato limite del GFS supera le creste lungo la tratta (osservazione di IT9GSF).
- Alternativa (spenta per difetto): profilo normale CRPL con l'Ns del punto (−ΔN = 7,32·e^(0,005577·Ns), Bean e Thayer 1959) al posto di 40 N/km fissi, anche come riferimento dell'equazione parabolica.
- Equazione parabolica: dominio di almeno 8 km, assorbitore sulla metà alta, fascio di al massimo 6°. Prima il campo oltre l'orizzonte era rumore numerico.
- Validazione: le novità si provano una alla volta sui 593 collegamenti del 29/12/2021–2/1/2022.
- Le voci a mano che il programma inseriva da sé (IV3/HB9CAT, IC8JHY, I8/IZ3CGC, IU5SPA, IZ6FLS, SWL1RUCAS, IZ1ERR, IK0SRL) ora stanno solo nel CALL3.TXT del pacchetto; alla prima apertura si tolgono dal file delle voci a mano, se non sono state cambiate.
- Le caselle (per esempio «Connetti all'avvio») si salvano subito in LIVETROPOVHF.ini, non solo alla chiusura del programma.
- Nuovo manuale «Fonti e formule»: tutte le fonti consultate, le formule e le decisioni del lavoro sulla previsione propria.
- Manuali: formule numerate come in LaTeX, (1), (2), …, con il numero a destra, anche nell'esportazione in Word; titoli con grazie; **grassetto**, *corsivo* e collegamenti resi nel testo; titoli di terzo livello.
- Calcolo punto-punto: «Ingresso nel condotto». Se il condotto comincia oltre l'orizzonte, perdita per arrivarci per diffrazione (spigolo principale con il parametro di Fresnel, oppure terra sferica) più il percorso nel condotto; se conviene diventa il meccanismo.
- Previsione: la sorgente automatica usa solo NOMADS (Open-Meteo se NOMADS non risponde e per i casi storici); casella «Aggiorna da sola» per scaricare la previsione senza clic all'apertura, a ogni nuova corsa GFS e quando la zona visibile esce dalla griglia.
- Manuale «Fonti e formule»: premesse dell'autore e fonti del 21 novembre (P.1812, IET 2018, PETOOL, MathWorks, ITU, Navipedia, VK3FS).
- Calcolo punto-punto: «Puntamento consigliato» e «Via monte». Con il corrispondente chiuso, fa il giro dell'orizzonte ogni 5° e indica dove puntare: i 4 diffusori più promettenti entro 60 km (spazio libero fino al rilievo, tratta dal rilievo con la stessa previsione, equazione del radar bistatico).
- Condotto sul mare a bassa quota: con NOMADS si scaricano SST, temperatura e umidità a 80 m e nello strato 30–0 hPa sopra il suolo; sui punti di mare si aggiungono al profilo. Condotto d'evaporazione stimato da SST, temperatura e umidità a 2 m (profilo logaritmico neutro), usato nell'equazione parabolica.
- Equazione parabolica: propagatore ad angolo largo; fondo con il coefficiente di Fresnel in polarizzazione orizzontale (mare εr 81 e 5 S/m, campagna 13 e 0,006 S/m) e la rugosità del mare di Miller-Brown dal vento.
- Avvisi per l'ora del giorno: rimescolamento diurno su terra e possibili condotti notturni da irraggiamento.
- Calcolo punto-punto: riga «Probabilità per periodo» per SSB, CW, FT4, FT8, Q65 e JT65, con l'evanescenza entro l'ora come distribuzione normale (σ 3,4 dB oltre 160 km, dall'articolo originale di Bray, QST novembre 1961: 13,5 dB di ampiezza, ±7 dB).

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
