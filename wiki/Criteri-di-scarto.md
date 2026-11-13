# Criteri di scarto dei contatti

Il programma vuole mostrare solo la propagazione troposferica in 144 MHz e oltre. Ogni dato in arrivo (riga di cluster, rapporto PSKReporter via MQTT, decodifica UDP) passa per una serie di filtri, sempre nello stesso ordine. Un dato scartato non sparisce nel nulla: viene contato (contatore degli esclusi nella barra in alto) e, quando il motivo è dubbio, compare nella linguetta Segnalazioni sospette.

| N. | Criterio | Dove vale | Che cosa scarta | Si può cambiare? |
|---|---|---|---|---|
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

Nota sui criteri 4 e 6. Un collegamento in Q65 o JT65 oltre 1000 km senza Luna comune è possibile in tropo: non viene scartato dal criterio 4, ma passa per la quarantena del criterio 6 come ogni altro collegamento lungo. Le chiamate dirette a te (linea nera lampeggiante) sono l'unica eccezione ai criteri 7 e 8: si disegnano sempre.

Che cosa non è uno scarto. Le correzioni del locatore (CALL3.TXT, voci a mano, cluster, LOC6, suffissi -SDR, centro del quadrato LL/MM) cambiano dove si disegna una stazione, non la scartano. Una stazione senza alcun locatore, invece, non si può disegnare: resta nell'elenco con la distanza «—».
