LIVETROPOVHF · documento tecnico 1
# Funzionamento del programma

Che cosa fa il programma, da dove prende i dati e come li trasforma in quello che vedi sulla mappa e nell'elenco. Versione del 9 novembre 2026, riferita al progetto in C# (.NET 10 + Avalonia).

## In sintesi

- Scopo: previsione tropo (Hepburn [3], F5LEN [4], isobare GFS [18]) e osservazione reale delle bande nella stessa finestra, secondo la lista di controllo di LA0BY [1].
- Raccolta dati: due DX cluster, Reverse Beacon Network [9], cluster dm4x e flusso MQTT di PSKReporter [5], decodifiche UDP da WSJT-X, JTDX e MSHV [8].
- Spot: segmenti di banda, filtro negativo, airscatter in giallo tratteggiato, posizione dai locatori o da CALL3.TXT [6][7].
- Chi è in ascolto: cerchi rosa scuro con le linee rosa sempre visibili verso le stazioni sentite [5].
- Scheda dello spot: distanza, azimut, indice tropo sul percorso e perdita troposcatter col metodo G3YGF [2].
- Linguette Hepburn e F5LEN: scarico delle immagini e banco di taratura [3][4].
- Orologio: controllo NTP su 10 server ogni 3 minuti [10][11].
- Segnalazioni sospette: EME in Q65/JT65 riconosciuto con la posizione della Luna, locatori troncati, skimmer, quarantena e ascoltatori, con regole imparate da sole [20].
- Criteri di scarto e significato dei colori riassunti in due tabelle (sezioni 11 ter e 11 quater).
- Temi, 36 lingue [17], esportazione dei documenti in Word con formule modificabili [16], programma portabile e multipiattaforma [12][13][14][15].

## 1. Scopo

Il programma mette insieme in un'unica finestra due cose che di solito si guardano su siti diversi: la previsione della propagazione troposferica (indice tropo di Hepburn e di F5LEN, isobare) e l'osservazione di quello che succede davvero sulle bande (spot da DX cluster, Reverse Beacon Network e PSKReporter, più l'elenco delle stazioni che stanno ascoltando). La banda di riferimento è il 144 MHz; gli spot si raccolgono dai 50 MHz in su.

L'impostazione segue la lista di controllo proposta da LA0BY per gli eventi tropo estremi: nei giorni precedenti si guardano le previsioni Hepburn e F5LEN e i profili di temperatura in quota, mentre l'evento è in corso si cercano sul DX cluster e sulle mappe degli spot le prove che l'apertura sta iniziando [1]. LA0BY osserva anche che le sole mappe Hepburn/F5LEN non bastano a prevedere i collegamenti più lunghi [1]: per questo la mappa sovrappone previsione, pressione e spot reali, e il confronto lo fai a colpo d'occhio.

## 2. La finestra e le linguette

La finestra è una sola e non si divide in finestre mobili. Dall'alto verso il basso:

- Riga delle impostazioni generali: Call e Locatore a sinistra; a destra l'orologio UTC (es. 08/11/2026 20:33:38 UTC, testo ambra) seguito da NTP:OK o NTP:FAILED, il menu del tema e quello della lingua.
- Fascione delle linguette, in cinque gruppi: Mappa e spot; Previsioni (Propria β, la previsione calcolata dal programma e descritta nel Manuale delle previsioni, poi Hepburn e F5LEN); GRAVES; Dati e fonti (DX cluster, PSKReporter, Reverse Beacon, UDP, Nominativi e Segnalazioni sospette); Guide (Funzionamento del programma, Costruzione della mappa, Come si forma la tropo, Manuale previsioni, Consigli all'operatore, con una casella di ricerca che conta in quale guida compare il testo). Sotto il fascione una barra mostra le sotto-linguette del gruppo; ogni gruppo ricorda l'ultima usata.
- Contenuto della linguetta e, in fondo, la barra di stato con lo stato delle fonti, il database dei nominativi e l'ora dell'ultimo aggiornamento.

Le impostazioni della riga in alto valgono per tutte le linguette: il tema scelto colora anche la raccolta dati e lo sfondo dei documenti; le pagine dei documenti restano bianche come un foglio, così si stampano bene.

## 3. Call e locatore

Il locatore Maidenhead può avere 4 o 6 caratteri (es. JN70 o JN70CN). Il programma lo converte nelle coordinate del centro del quadrato: campo di 20° × 10°, quadrato di 2° × 1°, sottoquadrato di 5′ × 2,5′. Se il testo non è un locatore valido il bordo della casella diventa rosso e la stazione non compare sulla mappa. Il locatore serve anche come centro della proiezione azimutale e come punto di partenza per distanze e azimut nella scheda degli spot.

Il Call è il nominativo usato come login sui cluster. Al primo avvio, finché non c'è un nominativo valido, i cluster non si collegano: una finestra chiede il nominativo (e, facoltativamente, il locatore); confermato, il nominativo viene ricopiato come nome utente nel campo Login di ogni cluster che lo ha vuoto e i cluster partono. Con “Più tardi” restano fermi, e il pulsante Connetti di ogni cluster richiede di nuovo il nominativo. MQTT, UDP e NTP non usano il login e partono comunque.

## 4. Raccolta dati: le fonti

Ogni fonte ha la sua sezione nella linguetta Raccolta dati, con Connetti/Disconnetti, le caselle Riconnetti e Connetti all'avvio, lo stato, i contatori e il registro delle righe ricevute. Le righe fuori banda o scartate dal filtro non compaiono nel registro: le contano soltanto i contatori della sezione. Le sezioni sono affiancate a due a due.

| Fonte | Collegamento | Che cosa porta |
| 1. DX cluster a scelta | on0dxk.dyndns.org:8000 comando set/filter dxbm/pass 6 | Spot dei radioamatori, anche SSB e CW. Su ON0DXK le VHF sono filtrate e vanno abilitate col comando indicato (poi pass 4, 2…). |
| 2. Seconda fonte | 9a0dxc.hamradio.hr:8000 (9A0DXC, Zagabria) e s50clx.infrax.si:41112 (S50CLX, Slovenia) | Due cluster DX Spider, login con il proprio nominativo; indirizzi modificabili. |
| 3. Reverse Beacon Network | telnet.reversebeacon.net 7000 CW/RTTY · 7001 FT8 | Spot automatici degli skimmer. Login con il solo nominativo [9]. |
| 4. Cluster con PSKReporter | dm4x.ddns.net:8500 | Spot digitali di PSKReporter in formato cluster. |
| 5. PSKReporter MQTT | mqtt.pskreporter.info 1883 · 1884 con TLS | Ogni rapporto di ricezione in tempo reale: chi trasmette, chi ascolta e i rispettivi locatori [5]. Bande 144, 222 e 432 MHz. |
| 6. Meteo e mappe tropo | dxinfocentre.com/tropo_eur.html tropo.f5len.org/forecasts-for-europe modello GFS | Mappe dell'indice tropo Hepburn [3] e F5LEN [4], pressione al livello del mare per le isobare (GFS tramite Open-Meteo [18]). Per Hepburn e F5LEN va chiesto il permesso agli autori. |

Cluster telnet (fonti 1–4). Dopo la connessione il programma aspetta la richiesta del nominativo (testo che contiene login, call:, callsign o your call); se non arriva entro 4 secondi manda comunque il login. Poi invia i comandi della sezione, separati da punto e virgola, uno ogni 0,6 secondi. I caratteri di controllo telnet vengono tolti prima di leggere le righe. Se la connessione cade e Riconnetti è attivo, riprova dopo 10 secondi (15 se il server non risponde affatto) [6].

PSKReporter MQTT (fonte 5). Il programma si iscrive al topic pskr/filter/v2/{banda}/{modo}/{tx_call}/{rx_call}/{tx_grid}/{rx_grid}/{tx_dxcc}/{rx_dxcc} fissando solo la banda e lasciando liberi gli altri campi col carattere + [5]. Non serve autenticazione. Il nome esatto con cui il server indica le bande (per esempio 2m, 1.25m, 70cm) va controllato alla prima connessione.

## 5. Dalla riga del cluster allo spot

Una riga è uno spot quando ha la forma DX de SPOTTER: frequenza DX commento hhmmZ. Il programma ne ricava spotter, stazione DX, frequenza in kHz, commento e ora UTC; ai nominativi toglie i suffissi degli skimmer (-#, -1…). Poi applica, in quest'ordine, le regole seguenti [6].

| Banda | Segmento accettato |
| GRAVES (con i 144) | 143,040 – 143,060 MHz |
| 144 MHz | 144 – 148 MHz |
| 222 MHz | 220 – 225 MHz (solo Regione 2 ITU, Americhe) |
| 432 MHz | 420 – 450 MHz |
| 1296 MHz | 1240 – 1300 MHz |
| 2320 MHz | 2300 – 2450 MHz |
| 10 GHz | 10 – 10,5 GHz |

1. Banda. La frequenza deve cadere in uno dei segmenti gestiti; tutto il resto, e in particolare ogni spot sotto 49 990,0 kHz, non viene mostrato e conta come “fuori banda”.
2. Modo. Si cerca nel commento uno dei modi noti (FT8, FT4, Q65, JT65, ISCAT, CW, SSB, FM, RTTY, PSK31, MSK144, FSK441, JT6M); USB e LSB diventano SSB.
3. Filtro negativo. Lo spot viene scartato se contiene i modi MSK144, FSK441, JT6M, JTMS, ISCAT oppure i tag <AU> <AUE> <ES> <MS> <FAI> <F2>, con maiuscole e minuscole indifferenti e anche con spazi dentro le parentesi. Sono modi e propagazioni che non sono tropo: scatter meteorico, aurora, E sporadico, F2.
4. Airscatter. Se il commento contiene <AS> o <ACS> (anche misti, es. <As>) oppure dichiara lo spot come airscatter (airscatter, air scatter, aircraft scatter), lo spot resta ma viene disegnato in giallo con linea tratteggiata (bordata di scuro per restare leggibile sulla mappa chiara) ed evidenziato nell'elenco.
5. Posizione delle stazioni. Se nel commento ci sono due locatori (es. JN54<>JN83) il primo è di chi fa lo spot e il secondo della stazione DX; con un solo locatore, quello è della stazione DX. Ciò che manca si cerca in CALL3.TXT, solo con il nominativo esatto (per i portatili vedi sotto). Se una stazione resta senza posizione, lo spot compare nell'elenco con la nota “posizione sconosciuta” ma non sulla mappa.
6. Doppioni. Uno spot con stesso DX, stesso spotter, stessa banda e stessa fonte di uno già presente lo sostituisce, invece di aggiungersi.

CALL3.TXT è il database nominativi → locatore usato da JTDX, nel formato CALL,LOCATORE,,,. Il file del progetto contiene 1 048 847 nominativi, tutti con locatore valido; l'intestazione del file parla ancora di 34 323 nominativi perché è rimasta quella della prima edizione [7]. Viene caricato in background all'avvio, così la finestra è subito utilizzabile. Il file si cerca accanto al programma, nella cartella Dati, nella cartella da cui il programma è stato avviato e una o due cartelle sopra; se non c'è, una finestra lo dice all'avvio e la barra di stato lo segnala in rosso. Il proprio nominativo usa sempre il proprio locatore a 6 caratteri. Regola unica per i locatori: se quello ricevuto (spot, MQTT, decodifiche, QSO desunti) ha meno di 6 caratteri e CALL3.TXT ne ha uno a 6, vale sempre quello di CALL3.TXT; per i portatili (F/IC8TEM, IC8TEM/P) vale solo una voce esatta: senza, la stazione resta senza locatore e non si disegna, perché potrebbe trovarsi altrove. La regola si applica anche al momento del disegno, così i dati arrivati prima che il database fosse caricato si correggono da soli.

Modifiche a CALL3.TXT. Il file originale non si tocca: nella sottolinguetta Raccolta dati › Nominativi (CALL3) si aggiungono due strati che gli si sovrappongono. Le voci inserite a mano (nominativo e locatore, salvate in Dati/CALL3-MANUALE.TXT) valgono sopra tutto, anche sopra il locatore che la stazione stessa manda; all'inizio contengono IV3/HB9CAT JN65UQ, IC8JHY JN60WR, I8/IZ3CGC JN71DB, IU5SPA JN53GU, IZ6FLS JN62XS, SWL1RUCAS JN34OS, IZ1ERR JN35UI e IK0SRL JN61IT. I file aggiunti con il pulsante Aggiungi un file… vengono copiati in Dati/CALL3-AGGIUNTE/ e valgono sopra CALL3.TXT; con più file vince l'ultimo in ordine alfabetico. Il formato è quello di CALL3.TXT (CALL,LOCATORE), ma si accettano anche il punto e virgola, la tabulazione e lo spazio. Ordine completo: proprio locatore, voci a mano, correzioni dal cluster, file aggiunti, CALL3.TXT, LOC6.TXT, G7VRD, locatore ricevuto (che vale sopra i database solo se ha 6 caratteri e non finisce con LL o MM).

Aggiornamento di CALL3.TXT. Nella stessa sottolinguetta il pulsante Aggiorna adesso scarica il CALL3.TXT più recente da MMMonVHF (www.mmmonvhf.de), lo controlla (almeno 100.000 righe valide, altrimenti non cambia nulla), salva il precedente come CALL3.bak e lo sostituisce nella cartella da cui era stato caricato, poi lo ricarica. Con la casella Aggiorna da solo ogni 7 giorni il controllo avviene all'avvio, se l'ultimo aggiornamento è più vecchio di una settimana. Le voci a mano e i file aggiunti restano sopra e non vengono toccati.

Correzioni automatiche dal cluster. Negli spot automatici di DM4X e di RBN il commento porta due locatori, per esempio KN09tp<>KN09: il primo è quello che la stazione che spotta dichiara di sé, a 6 caratteri; il secondo è quello della stazione sentita, spesso solo a 4. Se il primo ha 6 caratteri (e non finisce con LL o MM) ed è diverso da quello del database, il programma corregge il database da solo: la correzione sta in Dati/CALL3-CLUSTER.TXT, vale sopra CALL3.TXT e i file aggiunti (sotto le voci a mano) e compare tra le Segnalazioni sospette come «CALL3 aggiornato dal cluster». Così sopravvive anche agli aggiornamenti di CALL3.TXT da MMMonVHF. Le correzioni non confermate per due anni si lasciano cadere.

Ultima fonte: G7VRD. Se un nominativo non ha un locatore a 6 caratteri in nessuna delle fonti precedenti, il programma lo chiede in sottofondo all'API pubblica di G7VRD (api.g7vrd.co.uk, «locator by callsign»), che raccoglie i locatori in automatico dal traffico in rete. Si accetta solo un locatore a 6 caratteri che non sia il centro del quadrato (LL o MM) e che stia nello stesso quadrato di quello ricevuto; le richieste sono raggruppate, al massimo 40 nominativi al minuto, e ogni nominativo si chiede una volta al giorno, perché è un servizio hobbistico senza garanzie. I risultati stanno in Dati/CALL3-G7VRD.TXT e la ricerca si spegne con la casella nella sottolinguetta Nominativi. Ordine completo delle fonti: proprio locatore, voci a mano, correzioni dal cluster, file aggiunti, CALL3.TXT, LOC6.TXT, G7VRD, locatore ricevuto.

Nominativi con la barra. Nei topic MQTT di PSKReporter la barra separa i livelli, quindi i nominativi composti arrivano con un punto al posto della barra (IV3.HB9CAT). Il programma rimette la barra e, se il messaggio contiene i nominativi per intero, usa quelli: IV3/HB9CAT resta IV3/HB9CAT su mappa, elenco e database.

Locatori imparati dal vivo. CALL3.TXT non contiene tutti: per esempio SP9EMF e SP9NVY non ci sono, e allora resta il locatore a 4 caratteri mandato da PSKReporter (la scheda lo dice). Per ridurre questi casi il programma ricorda ogni locatore a 6 caratteri che arriva da una fonte qualsiasi (PSKReporter, come trasmettitore o come ricevitore, cluster, decodifiche UDP) per un nominativo che CALL3.TXT non ha o ha solo a 4 caratteri, e lo usa per gli spot dello stesso nominativo che portano 4 caratteri, purché il quadrato coincida. I locatori imparati stanno in Dati/LOC6.TXT, accanto al programma, nello stesso formato di CALL3.TXT; le voci più vecchie di due anni vengono lasciate cadere. L'ordine è: proprio locatore, CALL3.TXT a 6 caratteri, LOC6.TXT, locatore ricevuto.

Ricevitori automatici. RBN, dm4x e PSKReporter riportano spesso nominativi con un suffisso che indica il ricevitore: -SDR, /SDR, -# (skimmer RBN) o -1, -2… (più ricevitori della stessa stazione). Per il locatore il suffisso si toglie: OK2OZL-SDR prende da CALL3.TXT il JN89UE di OK2OZL, se la stazione non manda già un locatore a 6 caratteri. Il nominativo mostrato resta quello con il suffisso.

Locatori troncati. Non tutte le fonti curano l'estensione del proprio locatore: molti la impostano a 4 caratteri, e alcuni sistemi automatici di riporto troncano volutamente il locatore a 4 caratteri, anche quelli che ricavano i dati dall'ascolto reale di FT8, FT4, Q65 e JT65, oppure lo completano con il centro del quadrato (LL o MM, es. JN89MM). Per questo un locatore a 4 caratteri, o a 6 che finisce con LL o MM, si sostituisce con quello di CALL3.TXT o di LOC6.TXT dello stesso quadrato. Se la posizione corretta e il centro del quadrato non coincidono di qualche decina di km, l'errore resta piccolo rispetto alle distanze tropo.

## 6. Chi è in ascolto

Ogni messaggio MQTT di PSKReporter riporta il nominativo e il locatore di chi riceve [5], quindi non serve cercarlo in CALL3.TXT. Per la banda scelta nel selettore il programma tiene, per ogni stazione ricevente: locatore, ultima ricezione, modi decodificati, numero di stazioni sentite e distanza massima. Una stazione esce dall'elenco quando non riceve più niente nell'intervallo scelto (3, 5, 10, 15, 30 o 60 minuti).

Sulla mappa ogni ricevente è un cerchio vuoto rosa scuro, e da ciascuno partono linee rosa piene verso le stazioni che sta sentendo e rimanda in rete. Le linee si vedono sempre, senza cliccare. Nominativi delle stazioni in ascolto, di quelle sentite e dei due capi di ogni spot sono sempre scritti sulla mappa, senza dover cliccare (lo stesso nominativo nello stesso punto si scrive una volta sola). Cliccando un cerchio le sue linee diventano ancora più spesse e si apre la scheda con i dettagli. Se il locatore che arriva da MQTT ha meno di 6 caratteri, il programma usa quello di CALL3.TXT (circa un milione di nominativi), purché sia coerente con il quadrato ricevuto.

Modi da meteor scatter. Come per gli spot dei cluster, i rapporti MQTT in MSK144, FSK441, JT6M, JTMS o ISCAT vengono scartati e contati soltanto; lo stesso vale per le decodifiche UDP in questi modi.

Ascoltatori non genuini. Alcune stazioni dichiarano di ricevere in 144 MHz ma in realtà il ricevitore è su un'altra banda (indicazione di frequenza sbagliata): i loro rapporti disegnerebbero aperture inesistenti di migliaia di km. Il programma lo verifica da solo. Ogni stazione che un ascoltatore dice di sentire in VHF viene seguita per 30 minuti su tutte le bande, con un'iscrizione MQTT dedicata (pskr/filter/v2/+/+/{nominativo}/#) [5]. I rapporti delle altre bande non vengono più scartati: servono come prova. Se almeno due delle stazioni sentite da un ascoltatore risultano, negli stessi minuti (±10), attive su altre bande, l'ascoltatore viene considerato non genuino e tutti i suoi rapporti VHF vengono scartati; il registro MQTT riporta il nominativo e le stazioni che lo hanno tradito. Nell'elenco degli spot, accanto ai contatori, il pulsante MQTT mostra lo stato del flusso (pallino verde se collegato, rosso se no) e quanti ascoltatori sono stati scartati nella banda e nell'intervallo scelti; cliccandolo si apre la scheda con l'elenco degli scartati e, per ciascuno, le stazioni trovate su altre bande.

Quarantena dei collegamenti lunghi. Un collegamento oltre 1000 km (spot dei cluster, ascolto MQTT, QSO desunti) non si mostra subito: resta trattenuto, e il contatore “trattenuti > 1000 km” ne dice il numero. Si svela quando un altro collegamento, tra stazioni diverse e lungo almeno la metà, lo conferma con uno di questi criteri: stessa stazione a un capo (per esempio lo stesso ascoltatore) e l'altra stazione nella stessa direzione entro 10°; corridoio simile, con entrambi i capi entro 200 km da quelli dell'altro collegamento; percorso parallelo, con direzione entro 10° e distanza laterale entro 200 km, sovrapposto lungo il percorso. Allo svelamento si mostrano entrambi e il registro MQTT riporta il motivo. Le proprie decodifiche UDP sono sempre visibili e valgono come conferma per gli altri. I collegamenti trattenuti restano in memoria per 60 minuti.

Niente sotto i 144 MHz. Il programma lavora dai 144 MHz in su: tutto ciò che sta sotto i 143 MHz viene scartato. L'eccezione è GRAVES, il radar francese che trasmette in CW su 143,050 MHz da Broye-lès-Pesmes (JN27SI): uno spot tra 143,040 e 143,060 MHz, o con GRAVES nel nominativo, compare tra i 144 MHz con il radar come trasmettitore. La scheda dello spot dice se il ricevitore sta dentro il fascio (settori D, C, B, A da 90° a 270° di azimut visti dal radar, 6 passi da 7,5° ciascuno) oppure fuori; la linguetta GRAVES, come in YA-MS, mostra il cielo visto dal radar: settori, passo illuminato in quel momento (sequenza PE1ITR 2021: 0,8 s per passo, ciclo 4,8 s, legata all'UTC), la tua stazione, il conto alla rovescia al prossimo passaggio del fascio sulla tua direzione, le indicazioni d'ascolto (USB 143,049 MHz, nota a circa 1 kHz, antenna verso GRAVES) e chi ha spottato GRAVES. Dati e geometria vengono da YA-MS. Sentirlo in tropo è difficile, ma può succedere.

Riabilitazione dai cluster umani. Una stazione che risulta anche su altre bande non conta come sospetta se, negli stessi minuti (±30), i DX cluster umani (ON0DXK, GB7RAU; non dm4x, RBN o altre fonti automatiche) la danno attiva sulla banda in esame, in spot fatti da altri nominativi: è il caso delle stazioni che lavorano davvero su più bande insieme. Così un ascoltatore scartato torna visibile (il registro MQTT scrive “riabilitato”) e un collegamento lungo trattenuto per le bande basse può essere svelato.

Stazioni attive sotto i 144 MHz. Nella quarantena dei collegamenti lunghi, una stazione che negli stessi minuti risulta sentita su bande sotto i 144 MHz (dai rapporti MQTT delle altre bande) non conferma altri collegamenti e non viene svelata, anche se un percorso simile o parallelo la confermerebbe.

## 7. Elenco degli spot

Occupa il 35% destro della linguetta Mappa e spot. In alto: titolo, selettore della banda (50, 70, 144, 222, 432, 1296, 2320 MHz, 10 GHz), selettore dell'intervallo (ultimi 3, 5, 10, 15, 30, 60 minuti) e tasto Aggiorna. Sotto, quattro contatori: spot visibili, AS/ACS, scartati dal filtro, stazioni in ascolto.

Ogni riga mostra il tipo di linea, l'ora UTC, DX → DE, il modo, la distanza in km e, sulla seconda riga, la fonte, la frequenza e il commento. Il più recente è in cima. Un clic sulla riga seleziona lo spot: sulla mappa la linea si ingrossa e compaiono i due nominativi.

## 8. Scheda dello spot e perdita troposcatter

Per lo spot selezionato la scheda mostra locatori, distanza, azimut, frequenza, modo, fonte, ora, commento, l'indice tropo medio lungo il percorso e una stima della perdita di percorso per tropo scatter.

Distanza e azimut si calcolano sul cerchio massimo con raggio terrestre di 6371 km (formula dell'emisenoverso per la distanza, azimut iniziale dalla stazione DX verso lo spotter).

Perdita troposcatter. Si usa il metodo di G3YGF [2]. La perdita di base, per due stazioni su terra liscia, è la somma della perdita di spazio libero e della perdita di diffusione:

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

Le perdite sono in dB, D è la distanza in km, f la frequenza in MHz, θs l'angolo di diffusione in gradi.

8497 km è il raggio terrestre equivalente (4/3 di quello fisico). θA e θB dipendono dall'orizzonte di ciascuna stazione:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

con Δh altezza dell'ostacolo che fa da orizzonte rispetto alla stazione e d la sua distanza. Ogni grado di orizzonte costa circa 10 dB [2]. Il programma non conosce l'orizzonte delle stazioni, quindi mostra la perdita di base con θA = θB = 0 e lo dichiara (“terra liscia”). G3YGF aggiunge un termine climatico, dove N è la rifrattività al suolo [2]; verrà usato quando sarà disponibile N dal modello meteo:

```math
L_{N} = 0,2 \cdot (N - 310)
```

Verifica: per 110 km a 144 MHz la formula dà 166,5 dB, contro i circa 168 dB letti sul grafico dell'articolo. Per 500 km a 144 MHz dà circa 206 dB. Il rapporto segnale/rumore atteso è la capacità dell'impianto (eirp + sensibilità efficace del ricevitore) meno la perdita: con l'impianto d'esempio di G3YGF a 144 MHz (198 dB) un percorso di 500 km darebbe circa −8 dB in SSB [2], cioè il collegamento ci sta solo con propagazione migliore dello scatter puro.

## 9. Le mie decodifiche: UDP da WSJT-X, JTDX e MSHV

WSJT-X, JTDX e MSHV possono mandare via UDP ogni riga che decodificano. Il programma si mette in ascolto su 127.0.0.1:2237 e ripete lo stesso pacchetto, byte per byte, su 127.0.0.1:2238, così gli altri programmi che usavano quella porta (GridTracker, JTAlert, un logger) continuano a funzionare se li sposti su 2238. Indirizzi e porte si cambiano nella sottolinguetta UDP della Raccolta dati; come indirizzo di ascolto si può usare anche un gruppo multicast (es. 224.0.0.1), se il programma di decodifica è impostato così.

I pacchetti seguono il protocollo di rete di WSJT-X: un numero magico iniziale 0xADBCCBDA, il numero dello schema e il tipo di messaggio. Il programma usa soprattutto tre tipi [8]:

- Heartbeat: dice quale programma sta trasmettendo i dati e con che versione.
- Status: frequenza di dial e modo; dalla frequenza si ricava la banda (con la stessa tabella degli spot).
- Decode: ora, SNR, scarto di tempo e di frequenza, modo e testo del messaggio decodificato.

Dal testo si ricava la stazione che hai sentito: in CQ 9A2PQR JN83 è la stazione dopo CQ (saltando DX, EU, TEST…), in IK0JKL IT9MNO JM68 è la seconda, cioè chi trasmette. Il locatore è quello del messaggio, se c'è (4 caratteri, diverso da RR73); altrimenti si cerca in CALL3.TXT. Sono righe di ascolto: stazioni che il tuo ricevitore sente adesso. Sulla mappa diventano linee rosse dal tuo QTH alla stazione, con il nominativo; lo strato si accende con la casella Decodificati da me e richiede un locatore valido. Nell'elenco a destra un contatore dice quante stazioni stai decodificando sulla banda scelta.

QSO desunti dal live. Quando una decodifica è un messaggio di chiusura nella forma CALL1 CALL2 RR73 (oppure 73 o RRR), per esempio IC8TEM IK3FHP RR73 o IK3FKP SV8PEX 73, le due stazioni hanno completato un collegamento. Il programma lo registra come QSO avvenuto, con il locatore di ciascuna preso dall'ultimo messaggio in cui l'ha dichiarato o, se non c'è, da CALL3.TXT [7], e lo disegna sulla mappa come una linea verde tra le due stazioni (strato “QSO desunti dal live”). Una coppia di stazioni conta una volta per banda; valgono la banda e l'intervallo scelti nell'elenco.

Nella stessa sottolinguetta il riquadro Debug mostra il testo di ogni pacchetto ricevuto (tipo, ora, valori e messaggio), a richiesta anche i byte in esadecimale, e una riga per gli inoltri. Serve a controllare che il programma di decodifica stia davvero mandando i dati.

## 9 bis. Linguette Hepburn e F5LEN

Ognuna mostra ciò che il programma preleva dalla fonte. Il pulsante “Leggi la pagina e scarica tutte le immagini” apre la pagina indicata (di base dxinfocentre.com/tropo_eur.html per Hepburn [3] e tropo.f5len.org/forecasts-for-europe per F5LEN [4]) e scarica tutte le immagini nella cache accanto all'eseguibile; si può anche incollare l'indirizzo di una sola immagine. Per ogni immagine c'è una casella per escluderla e un campo con l'ora di previsione. Le 31 immagini europee di F5LEN si chiamano con l'ora dalla corsa del modello (03.png … 48.png ogni 3 ore, 54 … 96 ogni 6 ore, 108 … 192 ogni 12 ore) e il programma ricava l'ora dal nome del file [4]; per Hepburn vale l'ordine della pagina. Nella linguetta F5LEN un menu sceglie la regione: Europa (tropo.f5len.org/forecasts-for-europe), Australia (tropo.f5len.org/australia, file vk03.png … vk192.png) o Atlantico tra Capo Verde e Irlanda (tropo.f5len.org/atlantic-ocean/atlantic-d4-ei, file atl03.png … atl192.png). Tutte hanno gli stessi passi.

Sotto c'è il banco di taratura. Un clic sull'immagine aggiunge un punto di controllo (locatore o lat,lon: ne servono almeno 6) oppure un colore della legenda con il suo valore. Il programma calcola la corrispondenza pixel → coordinate con un'interpolazione polinomiale di secondo grado ai minimi quadrati [19], legge i colori con la tolleranza scelta e, per F5LEN, converte le unità N nella scala 0–10 con a · N + b. “Salva taratura e prova la lettura” scrive il file nella cartella Tarature e controlla su quale pixel cade il tuo locatore. Prima di attivare una taratura va chiesto il permesso all'autore delle mappe.

Regioni. Ogni fonte ha più regioni, ognuna con le sue immagini a nome fisso: Hepburn tr_map/fcst/{codice}{ore}.png?v{data} (eur Europa, nwe Europa nord-occidentale, eeu Europa orientale, ent Atlantico nord-orientale, nat Nord Atlantico, eam Nord America orientale, nsa Sud America settentrionale, sam Sud America, mid Medio Oriente, nca Asia centro-settentrionale, sea Sud-est asiatico, eas Estremo Oriente; ore da +6 a +42 ogni 3, poi ogni 6 fino a +144; la data è quella della corsa delle 12 UTC) [3]; F5LEN maps/{prefisso}{ore}.png (Europa senza prefisso, atl, atleakp4, atld4fy, vp, naw, nakh6, ja, vk; ore da +3 a +192) [4]. L'ora valida è la corsa più le ore del nome; per F5LEN la corsa si ricava dall'ora di produzione dell'immagine. La linguetta di ogni fonte mostra per prima cosa l'anteprima della fusione: tutte le immagini scaricate messe insieme come sulla mappa principale, con il riquadro tratteggiato e il nome di ogni regione, una velatura grigia dove nessuna immagine copre la zona e un cursore per scorrere le ore; a sinistra l'elenco delle regioni, ciascuna con la casella per usarla, il numero di mappe lette e da dove vengono gli estremi (OCR, a mano o di fabbrica; se l'OCR non riesce compare il testo che ha letto). Il banco di taratura manuale resta dietro il pulsante omonimo. Il menu Regione del banco sceglie quale guardare, la casella “Usa questa regione sulla mappa” la include nell'indice. La cornice viene trovata da sola (due linee orizzontali e due verticali dello stesso colore); per collocare l'immagine servono gli estremi lat/lon della cornice: per Hepburn Europa (10° W–40° E, 27°–56° N) e F5LEN Europa (15° W–40° E, 30°–65° N) sono già nel programma, per le altre regioni si scrivono nei quattro campi O E S N leggendo le etichette degli assi. Il moltiplicatore di scala rende confrontabili le due fonti: valore finale = moltiplicatore × valore della legenda.

## 9 ter. Esportazione in Word

I due documenti hanno in alto il pulsante “Esporta in Word (.docx)”. Il file viene scritto nel formato Office Open XML; le formule diventano equazioni di Word (OMML), quindi restano modificabili con l'editor di equazioni [16]. Se la finestra di salvataggio non è disponibile il file finisce nella cartella Documenti accanto all'eseguibile.

## 10. Controllo dell'orario (NTP)

I modi digitali e l'orario degli spot richiedono un orologio preciso. Ogni 3 minuti il programma interroga 10 server NTP (SNTP su UDP 123): 0.pool.ntp.org, 1.pool.ntp.org, 2.pool.ntp.org, 3.pool.ntp.org, it.pool.ntp.org, ntp1.inrim.it, ntp2.inrim.it, ptbtime1.ptb.de, time.google.com, time.cloudflare.com [11]. L'elenco si può modificare.

Per ogni risposta calcola lo scarto e il ritardo di andata e ritorno, scarta i valori anomali e prende la mediana, con il calcolo di scarto e ritardo previsto da SNTP [10]. Con almeno 3 risposte concordi l'orologio UTC del programma si allinea e accanto compare NTP:OK in verde; se le risposte non bastano compare NTP:FAILED in rosso e l'orologio resta quello del computer. L'orologio di sistema non viene toccato, perché servirebbero i permessi di amministratore; tutti gli orari interni del programma (spot, finestre di 5–60 minuti, decodifiche) usano l'ora allineata. Il controllo gira sempre in sottofondo: nell'interfaccia si vede soltanto l'esito accanto all'orologio; server, intervallo e risposte minime si cambiano in LIVETROPOVHF.ini (ntp_servers, ntp_minutes, ntp_min).

## 11. Temi e lingue

Temi: Chiaro, Nero, Notte, Arancio pastello, Indaco pastello, Rosa pastello. Nei temi Nero e Notte anche la mappa di base diventa scura. Lingue: le 28 del wiki di YA-MS [17] (English, Italiano, Deutsch, Nederlands, Français, Español, Português, Dansk, Norsk, Svenska, Íslenska, Eesti, Lietuvių, Latviešu, Русский, Українська, Türkçe, Afrikaans, العربية, עברית, اردو, हिन्दी, বাংলা, Bahasa Indonesia, Tagalog, 日本語, 한국어, 中文). Con arabo, ebraico e urdu tutta la finestra si specchia da destra a sinistra; la mappa resta com'è [12]. Le traduzioni riguardano l'interfaccia; i due documenti tecnici e i messaggi della barra di stato restano in italiano. I nomi dei giorni vengono dalle impostazioni internazionali di .NET per la lingua scelta.

Tema e lingua si cambiano dai due menu in alto a destra, accanto all'orologio, e si applicano subito, senza riavviare il programma e senza interrompere la raccolta dei dati. La scelta viene salvata in LIVETROPOVHF.ini.

## 9 quater. Chi mi chiama

Se sull'UDP locale passa un messaggio indirizzato a te (il primo nominativo è il tuo, per esempio IC8FAX IK3FHP JN55 oppure IC8FAX IK3FHP -12), il programma traccia dal tuo QTH alla stazione che ti chiama una linea nera tratteggiata e lampeggiante, con il nominativo e il locatore accanto. Il locatore viene da CALL3.TXT (comprese le voci a mano e i file aggiunti) o da quello del messaggio. La linea resta finché sull'UDP passa la chiusura del QSO con quella stazione (RR73, RRR o 73, in una delle due direzioni), al massimo 3 minuti dall'ultima chiamata; si disegna sopra tutto e non segue i criteri di esclusione, così una chiamata diretta non sfugge mai.

PSKReporter, via MQTT, non trasmette il testo dei messaggi: dice solo chi ha ricevuto chi, con frequenza, modo e SNR. Per questo una chiamata diretta si riconosce solo dalle proprie decodifiche UDP.

## 10 bis. Criteri di esclusione generale

Due caselle nella barra degli strati valgono per tutto ciò che si disegna (spot, PSKReporter, decodifiche, QSO desunti):

- Escludi sotto 100 km. Il traffico tra stazioni a meno di 100 km può non essere significativo per la tropo (onda di terra, portata ottica): con la casella accesa non si disegna e non compare nell'elenco degli spot. All'avvio è spenta.
- Oltre 1000 km: solo quadrati. Attorno al tuo QTH (il locatore impostato all'avvio) c'è una corona di 1000 km in tutte le direzioni. Se anche una sola delle due stazioni di un collegamento sta fuori da questa corona, il collegamento non si disegna come linea: al suo posto si colorano in lilla, tratteggiati a 45°, i quadrati locator (4 caratteri) delle stazioni coinvolte. Lo stesso vale per le stazioni PSKReporter in ascolto oltre 1000 km, che compaiono come quadrato invece che come cerchio. Così il traffico lontano si vede senza riempire la mappa di linee. Senza un locatore valido conta la lunghezza del collegamento. Il quadrato del proprio QTH non si colora mai in lilla, anche quando ci ascolta una stazione oltre la corona: vale il locatore impostato, qualunque nominativo usi chi opera dalla stazione. Il raggio della corona si sceglie accanto alla casella: 1000, 700 o 500 km. La casella Corone 1000/700/500 km disegna le tre corone attorno al QTH, tratteggiate in lilla, con quella scelta più marcata e la distanza scritta a nord. Nell'elenco degli spot la distanza è seguita da ▦. All'avvio è accesa. Il criterio si applica dopo la quarantena: i collegamenti lunghi ancora trattenuti non compaiono nemmeno come quadrati.

A che cosa servono i quadrati lilla tratteggiati a 45°. Il traffico lontano dal tuo QTH ti interessa per capire dove la propagazione è aperta, ma le sue linee, lunghe centinaia di km e spesso a decine, coprono la mappa e nascondono quello che conta per te: le aperture che puoi sfruttare dalla tua stazione. Per questo, oltre la corona scelta, il programma non disegna le linee ma colora i quadrati locator (2° × 1°) delle stazioni coinvolte. Il risultato è una mappa di «zone attive»: dove compaiono molti quadrati lilla vicini, lì in questo momento c'è un condotto o un'apertura, anche se non ti riguarda direttamente; se la zona si allarga verso di te, è il segnale per puntare l'antenna in quella direzione. Il tratteggio a 45° lascia vedere sotto l'indice tropo e la griglia, così si confrontano a colpo d'occhio la previsione (il colore di fondo) e il traffico reale (il lilla). Il quadrato del tuo QTH non si colora mai, perché è il punto di partenza e non una zona lontana. Si colorano solo i quadrati che stanno tutti oltre la corona: un quadrato attraversato dalla corona, o al suo interno, resta libero anche se ci sta una delle due stazioni. Per vedere comunque le linee basta spegnere la casella o scegliere una corona più ampia.

## 11 bis. Segnalazioni sospette e auto-apprendimento

La linguetta Segnalazioni sospette raccoglie tutto ciò che i controlli del programma trovano strano, raggruppato per tipo e nominativo, con le ultime 24 ore in elenco. Il programma impara da solo: per ogni coppia (tipo, nominativo) conta gli esiti favorevoli (+) e contrari (−), e dopo 3 esiti concordi crea una regola automatica; un esito opposto la rimette in discussione. Le decisioni dell'utente (Buono, Scarta) valgono sempre; il pulsante ↺ restituisce la decisione al programma. Le regole stanno in Dati/APPRENDIMENTO.TXT, accanto al programma; quelle automatiche non rinforzate da un anno si dimenticano.

| Tipo | Quando | Esito + / − | Effetto della regola |
| EME (Q65/JT65) | Q65 o JT65 oltre 1000 km con la Luna sopra l'orizzonte a entrambi i capi, o EME nel commento | − ogni volta che la Luna è comune | Scarta: stazione EME, i suoi Q65/JT65 lunghi si escludono anche senza Luna comune. Buono: trattati come tropo |
| Collegamento lungo | Oltre 1000 km, in quarantena | + svelato, − scaduto senza conferme | Buono: i lunghi della stazione si mostrano subito. Scarta: mai |
| Ascoltatore | Ricevitore PSKReporter che sente stazioni attive su altre bande | − scartato, + riabilitato dai cluster umani | Buono: mai scartato. Scarta: sempre |
| Locatore diverso | Quadrato ricevuto diverso da quello di CALL3.TXT | + ogni volta che lo ripete | Buono: la stazione si è spostata, vale il quadrato ricevuto. Scarta: vale CALL3.TXT |
| Centro del quadrato | Locatore a 6 caratteri che finisce con LL o MM | — | Solo informativa |
| Ricevitore automatico | Nominativo con -SDR, /SDR, -# o -n | — | Solo informativa |

EME. Q65 e JT65 sono i modi della comunicazione via Luna: in teoria rientrano nella clausola di esclusione come il meteor scatter, ma si usano anche in tropo. Il programma calcola la posizione della Luna con le formule a bassa precisione dell'Astronomical Almanac (circa 0,3°) [20]: se un collegamento in Q65 o JT65 supera 1000 km e la Luna è sopra l'orizzonte a entrambi i capi, quasi certamente è EME e viene escluso (si conta tra gli esclusi). Se la Luna non è comune, il collegamento resta possibile in tropo e passa per la quarantena dei collegamenti lunghi, ma la segnalazione resta visibile. Vale per i cluster, per MQTT e per le proprie decodifiche UDP.

## 11 ter. Criteri di scarto dei contatti, uno per uno

Il programma vuole mostrare solo la propagazione troposferica in 144 MHz e oltre. Ogni dato in arrivo (riga di cluster, rapporto PSKReporter via MQTT, decodifica UDP) passa per una serie di filtri, sempre nello stesso ordine. Un dato scartato non sparisce nel nulla: viene contato (contatore degli esclusi nella barra in alto) e, quando il motivo è dubbio, compare nella linguetta Segnalazioni sospette.

| N. | Criterio | Dove vale | Che cosa scarta | Si può cambiare? |
| 1 | Banda | cluster, MQTT, UDP | Tutto sotto i 143 MHz (anche 50 e 70 MHz), tranne GRAVES 143,050 MHz. Le bande tenute sono 144, 222, 432, 1296, 2320 MHz e 10 GHz | No |
| 2 | Satellite QO-100 | cluster, MQTT, UDP | 2400,0–2400,5 MHz e 10489,5–10490 MHz: è traffico via satellite geostazionario, non tropo | No |
| 3 | Meteor scatter | cluster, MQTT, UDP | JT6M e ISCAT (anche ISCAT-A/B), i commenti con "&" e le etichette di propagazione non troposferica (<AU>, <AUE>, <ES>, <FAI>, <F2>) sempre. MSK144, FSK441 e JTMS (anche con <MS>) solo oltre 250 km: entro 250 km si accettano, perché a quella distanza questi modi si usano anche in tropo | No |
| 4 | EME (via Luna) | cluster, MQTT, UDP | EME scritto nel commento; Q65 o JT65 oltre 1000 km quando la Luna è sopra l'orizzonte a entrambi i capi; stazioni imparate come EME | Sì: regola Buono nelle Segnalazioni sospette |
| 5 | Ascoltatore non genuino | MQTT | Ricevitore PSKReporter che dice di sentire su 144 stazioni attive in quel momento su altre bande (almeno due): i suoi rapporti si scartano. Torna buono se i DX cluster umani confermano in banda le stesse stazioni | Sì: Buono / Scarta |
| 6 | Quarantena dei collegamenti lunghi | tutte le linee | Oltre 1000 km un collegamento resta nascosto finché non trova una conferma: stessa stazione nella stessa direzione, altro percorso in un corridoio di 200 km, o percorso parallelo entro 10° e 200 km lungo almeno la metà. Senza conferma scade dopo 60 minuti | Sì: Buono / Scarta per stazione |
| 7 | Sotto 100 km | linee ed elenco | Con la casella accesa, i collegamenti più corti di 100 km (onda di terra, portata ottica) | Casella, spenta all'avvio |
| 8 | Oltre la corona dal QTH | linee | Se una stazione è oltre 1000, 700 o 500 km (a scelta) dal tuo QTH, niente linea: solo i quadrati lilla tratteggiati delle due stazioni. Il quadrato del tuo QTH non si colora mai | Casella e raggio, accesa a 1000 km |
| 9 | Finestra temporale | tutto | Spot più vecchi dell'intervallo scelto (da 3 a 60 minuti) | Menu in alto |
| 10 | Doppioni | cluster | Lo stesso spot (stesso DX, stesso spotter, stessa banda, stessa fonte) ripetuto | No |

Nota sui criteri 4 e 6. Un collegamento in Q65 o JT65 oltre 1000 km senza Luna comune è possibile in tropo: non viene scartato dal criterio 4, ma passa per la quarantena del criterio 6 come ogni altro collegamento lungo. Le chiamate dirette a te (sezione 9 quater) sono l'unica eccezione ai criteri 7 e 8: si disegnano sempre.

Che cosa non è uno scarto. Le correzioni del locatore (CALL3.TXT, voci a mano, cluster, LOC6, suffissi -SDR, centro del quadrato LL/MM) cambiano dove si disegna una stazione, non la scartano. Una stazione senza alcun locatore, invece, non si può disegnare: resta nell'elenco con la distanza «—».

## 11 quater. Colori e simboli

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

## 12. Impostazioni e portabilità

Finestra. All'avvio la finestra occupa il 90% dello spazio utile dello schermo (tolta la barra delle applicazioni), centrata, così su un notebook da 15-16 pollici con ridimensionamento al 125% o al 150% la barra del titolo resta visibile. Se lo spazio utile è sotto 1280 × 720 punti, la finestra parte massimizzata. Dimensione, posizione e stato massimizzato si salvano in LIVETROPOVHF.ini alla chiusura e si riusano all'avvio successivo, purché la barra del titolo ricada ancora in uno schermo (per esempio dopo aver staccato un monitor esterno); altrimenti si torna al 90%. La dimensione minima è 900 × 560.

Il programma è portabile: non scrive mai nella cartella AppData né nel registro. Tutto sta nella cartella dell'eseguibile: LIVETROPOVHF.ini (impostazioni, creato alla prima chiusura), CALL3.TXT, la cache delle mappe e dei dati meteo, i locatori imparati (Dati/LOC6.TXT), le regole imparate (Dati/APPRENDIMENTO.TXT) e l'archivio LIVETROPOVHF.db. Si può tenere anche su una chiavetta USB, purché la cartella sia scrivibile.

Archivio degli spot (SQLite [14]). Ogni spot accettato e ogni rapporto di ascolto di PSKReporter viene salvato in LIVETROPOVHF.db, un database SQLite in un solo file. In memoria restano solo gli ultimi 60 minuti per l'elenco e la mappa; l'archivio conserva tutto e serve per lo storico e le statistiche (anche le decodifiche UDP), per esempio le aperture per banda e per giorno, le distanze massime, i quadrati raggiunti, e il confronto a posteriori tra l'indice previsto e gli spot reali. Le righe scartate dal filtro si contano ma non si archiviano.

## 12 ter. Registro degli errori e barra di stato

Ogni errore non previsto finisce in Dati/errori.log, anche con il registro dei calcoli spento: data e ora, versione, sistema, linguetta aperta, l'errore completo e le ultime righe di ciò che il programma stava facendo. Un errore dell'interfaccia non chiude più il programma: nella barra di stato compare «⚠ errore annotato» (clic: apre il registro). All'avvio successivo il programma chiede se inviare il rapporto: si apre il browser su una segnalazione GitHub già compilata, con nominativo, locatore e nome utente del PC tolti, che si controlla e si invia con un account GitHub gratuito. Nel registro finisce anche l'esito di ogni controllo degli aggiornamenti.

Nella barra di stato i pallini delle fonti (DX cluster, RBN, PSKReporter, MQTT, UDP) si cliccano e portano alla fonte; il fumetto dice perché una fonte è spenta: porta rifiutata, nodo del cluster fermo dal lato del server, nessuna risposta, oppure UDP in ascolto ma senza pacchetti da WSJT-X, JTDX o MSHV. Un clic sul numero di versione controlla subito gli aggiornamenti.

## 12 bis. Installazione e aggiornamenti automatici

Dalla versione 1.0 il programma si scarica già pronto dalla pagina Release di github.com/ic8tem/LIVETROPOVHF, negli stessi pacchetti di YA-MS: installatore per Windows (64 bit, 32 bit e ARM, installa in C:\LIVETROPOVHF senza diritti di amministratore) e zip portatile; per Windows 7 SP1 e 8.1 pacchetti a parte (-windows7-x64 e -windows7-x86): dalla 1.0.2 sono un programma scritto apposta per .NET Framework 4.8 con finestra WinForms, con lo stesso motore e la stessa mappa ma senza la lettura OCR degli assi e senza il banco di taratura manuale (le versioni recenti di .NET non partono su Windows 7); .deb, .rpm, Arch, Slackware, AppImage e tar.gz per Linux; pacchetti per Raspberry Pi (64 e 32 bit) e Alpine; .dmg e app.zip per il Mac (Apple Silicon e Intel). Con i pacchetti Linux il programma si copia al primo avvio in ~/LIVETROPOVHF, così impostazioni e dati restano accanto al programma; sul Mac i dati nascono accanto a LIVETROPOVHF.app.

Ogni 30 minuti (il primo controllo pochi secondi dopo l'avvio, subito se l'ultima volta il programma si era chiuso per un errore) il programma legge da solo, senza chiedere nulla, l'ultima Release. Se la versione è più nuova scarica il pacchetto portatile adatto al sistema e sostituisce i file del programma sul posto: quelli in uso vengono rinominati *.old-aggiornamento e cancellati al riavvio successivo. Impostazioni, archivio, cartella Dati e CALL3.TXT non vengono toccati. La nuova versione è attiva dal prossimo avvio: a installazione finita una finestra propone di riavviare subito; per le correzioni urgenti, segnate [obbligatorio] nella Release, il riavvio non si può rimandare. Lo stato del controllo e la versione in uso sono scritti in fondo alla barra di stato; per spegnere gli aggiornamenti si scrive updates=0 in LIVETROPOVHF.ini. Sul Mac l'app è firmata e non si modifica da sola: il programma segnala soltanto che c'è una versione nuova.

All'avvio compare l'immagine di LIVETROPOVHF con il numero di versione; alla chiusura il programma chiede conferma e ringrazia per l'uso.

## 13. Piattaforme

Librerie esterne: Avalonia per l'interfaccia [12], MQTTnet come client MQTT per il flusso in tempo reale di PSKReporter [13], Microsoft.Data.Sqlite per l'archivio [14]. Funzionano tutte su ogni piattaforma prevista.

C# su .NET 10 (LTS) [15] con interfaccia Avalonia, pubblicato come eseguibile autonomo per Windows 64 e 32 bit, Mac Intel e Apple Silicon, Linux Debian e Raspberry Pi (64 e 32 bit). Limiti noti: Windows 10/11; macOS 14 o successivo, con app da firmare o da aprire con clic destro → Apri; Raspberry dal Pi 2 / Zero 2 W in su (i modelli ARMv6 non sono supportati da .NET).

## Ringraziamenti
Grazie a **I5MZY Massimo Zannoni** per le prove su Windows 7 e Windows 8.

Climatologia dei condotti: ITU-R P.453, prodotti digitali della Study Group 3 (© ITU), usata per scopi personali e non commerciali.

## Fonti principali

1. S. Heck LA0BY, “Extreme Tropo Propagation on 144 MHz and up”, presentazione al 41° Nordic VUSHF Meeting, Skjeberg 2019. Modi troposferici, lista di controllo per gli eventi (previsioni Hepburn/F5LEN, profili di temperatura in quota con Windy, DX cluster come prova dell'apertura), osservazione che le sole mappe di previsione non bastano per i percorsi estremi.
2. J. N. Gannaway G3YGF, “Tropospheric Scatter Propagation”, QST novembre 1983 (dall'articolo omonimo su Radio Communication, RSGB, agosto 1981). Formule della perdita di spazio libero e di diffusione, raggio equivalente 8497 km, termine di sito di 10 dB per grado di orizzonte, termine climatico 0,2 · (N − 310), capacità dell'impianto e rapporto segnale/rumore, esempio a 144 MHz.
3. W. R. Hepburn, DX Info Centre, “Tropospheric Ducting Forecast” (dxinfocentre.com/tropo.html) e “The Hepburn Tropo Index (HTI)”. Scala dell'indice e significato dei valori, previsione su 6 giorni.
4. F5LEN, previsioni per l'Europa (tropo.f5len.org/forecasts-for-europe: elenco delle immagini nello script della pagina, legenda a classi Disturb … Duct dentro l'immagine) e previsione mondiale (tropo.f5len.org/WW) e nota “New version of the worldwide VHF tropospheric propagation forecast tool”. Indice basato sulla rifrattività in unità N, 40 passi da 3 ore, aggiornamento 4 volte al giorno.
5. mqtt.pskreporter.info, distribuzione MQTT in tempo reale degli spot di pskreporter.info (curata da M0LTE, flusso di P. Gladstone usato con permesso). Porte 1883–1886, schema del topic pskr/filter/v2.
6. Riepilogo di progetto dell'8/11/2026 e prova in Go (core.go, gui.go), forniti dall'autore del programma. Regole sugli spot, filtro negativo, segmenti di banda, gestione dei cluster telnet, temi, lingue e portabilità.
7. CALL3.TXT, database nominativi → locatore di JTDX (I. UA3DJY, script di A. ES1JA, dati JTAlert/hamspots.net). Formato del file e conteggio dei nominativi verificato sul file del progetto.
8. WSJT-X, codice sorgente, file NetworkMessage.hpp (descrizione del protocollo UDP: Heartbeat, Status, Decode). Riferimento generale, non allegato; JTDX e MSHV usano lo stesso formato.
9. Reverse Beacon Network (reversebeacon.net), pagine di servizio del nodo telnet. Porte 7000 (CW, RTTY) e 7001 (FT8), login con il solo nominativo.
10. D. Mills, RFC 4330 “Simple Network Time Protocol (SNTP) Version 4”, IETF 2006. Formato del pacchetto, calcolo di scarto e ritardo di andata e ritorno. Riferimento generale, non allegato.
11. NTP Pool Project (ntppool.org); INRIM, servizio di tempo NTP (ntp1/ntp2.inrim.it); PTB (ptbtime1.ptb.de). Server usati per il controllo dell'orario. Riferimento generale, non allegato.
12. Avalonia UI, documentazione ufficiale (docs.avaloniaui.net). Interfaccia multipiattaforma, temi e disposizione da destra a sinistra. Riferimento generale, non allegato.
13. MQTTnet, libreria .NET per MQTT (github.com/dotnet/MQTTnet). Client usato per il flusso di PSKReporter. Riferimento generale, non allegato.
14. SQLite (sqlite.org) e Microsoft.Data.Sqlite (learn.microsoft.com). Archivio in un solo file. Riferimento generale, non allegato.
15. Microsoft, .NET 10 – sistemi operativi supportati e pubblicazione autonoma in un solo file (learn.microsoft.com/dotnet). Piattaforme e limiti indicati nella sezione 13. Riferimento generale, non allegato.
16. ECMA-376 “Office Open XML File Formats”, parte 1, sezione 22.1 (Office Math Markup Language). Struttura del file .docx e delle equazioni. Riferimento generale, non allegato.
17. Elenco delle 28 lingue del wiki YA-MS, fornito dall'autore del programma.
18. Open-Meteo, API di previsione meteo (open-meteo.com), con i dati del modello GFS della NOAA. Pressione al livello del mare per le isobare (vedi documento 2).
19. J. P. Snyder, “Map Projections – A Working Manual”, USGS Professional Paper 1395, 1987, e metodi standard di trasformazione polinomiale per la georeferenziazione di immagini. Riferimento generale, non allegato.
20. The Astronomical Almanac, sezione D, “Low precision formulae for the Moon” (posizione con precisione di circa 0,3°). Usata per riconoscere i collegamenti EME. Riferimento generale, non allegato.
