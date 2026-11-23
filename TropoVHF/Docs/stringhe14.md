## 0

Webserver FM-DX pubblici (servers.fmdx.org, progetto open source di M. Farkaš): si interrogano i 60 più vicini entro 1500 km, ognuno ogni tre minuti circa, sulla loro API pubblica. Se un webserver riceve una stazione FM a più di 150 km con segnale di almeno 10 dBf (trasmettitore riconosciuto dal PI RDS), la ricezione compare sulla mappa come linea viola; quelle oltre 300 km entrano nei corridoi della banda 144, perché la tropo in FM (88–108 MHz) va di pari passo con i 2 m. Manuale delle previsioni, § 4 ter.

## 1

AIS dal tuo ricevitore (AIS-catcher, rtl-ais o un ricevitore con uscita NMEA): il programma ascolta le frasi !AIVDM in UDP sulla porta 10110 (ais_port in LIVETROPOVHF.ini) e confronta la distanza di ogni nave, stazione base o radiofaro con l'orizzonte radio normale a 162 MHz, 4,12·(√h nave + √h antenna) km. Le navi sentite oltre 1,5 volte l'orizzonte e oltre 50 km compaiono come linee azzurre dal tuo QTH: sul mare sono il segno più diretto di un condotto. Spento finché non lo accendi. Manuale delle previsioni, § 4 ter.

## 6

Quanto fidarsi del guadagno previsto: alta, media o bassa. È una somma di punti, non una probabilità: accordo fra i modelli (perdita prevista contro P.452 con i condotti ed equazione parabolica, linea di base contro ITM: fino a 3 dB +1, oltre 8 dB −1), anticipo (entro 24 ore +1, oltre 72 −1), continuità dello strato sulla tratta (70% +1, sotto il 30% −1) e sua persistenza (+1), verifica del GFS con il radiosondaggio più vicino (±1) e conferme dal vivo lungo la tratta nell'ultima ora e mezza (cluster, PSKReporter, APRS, FM-DX: 1–2 +1, 3 o più +2; aperture con un guadagno previsto quasi nullo −1). Alta da 3 punti, media da 0 a 2, bassa sotto lo zero. Manuale delle previsioni, § 10 septies.

## 8

Confronto fra i radiosondaggi reali della stazione più vicina (archivio IGRA v2 della NOAA, lanci delle 00 e 12 UTC degli ultimi 15 giorni; l'archivio si aggiorna ogni pochi giorni) e il GFS d'archivio alla stessa ora e nello stesso punto (Open-Meteo, previsioni storiche), con la stessa analisi del programma: quante volte il GFS ha visto un condotto osservato (M che cala con la quota sotto i 3000 m, almeno 2 unità M), i falsi allarmi e lo scarto medio del gradiente di rifrattività nel primo km. Dice quanto il modello vede bene i condotti qui, in questi giorni. Dati nella cartella Dati/IGRA. Manuale delle previsioni, § 10 septies.
