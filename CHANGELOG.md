# LIVETROPOVHF – storia delle versioni

Le versioni 0.x sono beta: compilate dall'autore per prova, non pubblicate come eseguibili. Dalla 1.0.0 il programma si scarica già pronto dalle Release di GitHub e si aggiorna da solo.
Numerazione: la cifra centrale sale con le funzioni nuove, l'ultima con le sole correzioni.
Fino alla 0.3.0 il programma si chiamava "Previsione Tropo VHF" (TropoVHF).

## In preparazione (ramo armonizzazione, 15/11/2026)
- Armonizzazione con gli altri programmi di IC8TEM: 37 lingue (aggiunto lo slovacco, come YAEC) e 18 temi (quelli di E-LAYER ANALYZER: Chiaro, Notte, Nero, otto pastelli, Scia meteorica, Chiaro/Notte/Nero di IP-TRX-CTRL, Blu, Viola, Verde). Arancio, indaco e rosa restano i colori originali di LIVETROPOVHF.

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
