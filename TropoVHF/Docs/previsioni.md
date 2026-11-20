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
- Curve di guadagno, Isobare, Inversione termica: linee opzionali sopra i colori (paragrafo 5). I colori restano sempre.
- La mia stazione e il corrispondente: locatore, antenne, potenza, cavi, rumore (paragrafi 7 e 12).
- Calcola il segnale e Copertura in ottica: i due calcoli sul terreno (paragrafi 8–13).
- Registro dei calcoli: ogni passo con i numeri usati, anche nel file Dati/previsioni-calcoli.log.

## 3. Dai dati GFS al profilo

Per ogni punto della griglia e ogni ora si leggono da Open-Meteo [2] la pressione al suolo, temperatura e umidità a 2 m, il vento a 10 m, l'altezza dello strato limite, il CAPE e temperatura e umidità relativa a 1000, 975, 950, 925, 900, 850, 800, 700, 600 e 500 hPa.

Le quote dei livelli si calcolano con la formula ipsometrica e la temperatura virtuale; i livelli sotto il suolo si scartano. Per ogni livello:

- N = 77,6/T · (P + 4810·e/T), con T in kelvin, P ed e in hPa, e dalla formula di Magnus (ITU-R P.453 [4]).
- M = N + 157·h, con h in km: la rifrattività modificata. Dove M cala salendo c'è un condotto.

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

Il locatore può avere 4, 6, 8 o 10 caratteri. Con 8 o 10 caratteri la posizione è precisa e la quota del suolo viene da SRTM1: basta l'altezza dell'antenna dal suolo. Con 4 o 6 caratteri il quadrato è troppo grande e serve la quota dell'antenna sul mare. Per il corrispondente l'altezza predefinita è 3 m dal suolo; con quota sul mare 0 si usa il suolo SRTM più l'altezza.

## 8. Terreno, diffrazione e troposcatter

Il profilo della tratta si campiona su SRTM1 ogni 30 m (più largo oltre 120 km). Il raggio terrestre equivalente vale k = 157/(157 + ΔN), con ΔN il calo di N nel primo km dai profili GFS lungo la tratta [6, 7].

- Spazio libero: Lfs = 92,44 + 20 log f(GHz) + 20 log d(km) [8].
- Diffrazione: metodo di Bullington sul profilo reale più la correzione di terra sferica delta-Bullington di ITU-R P.452-16 § 4.2 [6], con la perdita sferica di ITU-R P.526 [9].
- Prima zona di Fresnel: raggio √(λ·d1·d2/d); libera se il terreno lascia almeno 0,6 del raggio. Per ogni ostacolo si mostrano il parametro ν e la perdita J(ν) del bordo a coltello [9].
- Troposcatter: ITU-R P.452-16 eq. 45 al 50% del tempo [6], con la distanza angolare dagli orizzonti e N0 dai profili.
- Senza tropo: somma delle potenze di diffrazione e troposcatter, mai sotto lo spazio libero.

## 9. Lo strato fondante lungo la tratta

È il cuore del calcolo del segnale. Lungo la tratta si prendono i profili GFS ogni 25 km circa. Per ogni strato di pressione si cerca il tratto più lungo in cui lo stesso strato è presente su punti consecutivi; contano come condotto solo strati spessi almeno 40 m [10]. Si misurano:

- continuità: da che km a che km, e la frazione della tratta;
- quota della cima e della base, e l'inclinazione in m/km;
- terreno dentro lo strato: i km in cui le montagne salgono sopra la base;
- terra coperta: sul mare il condotto c'è quasi sempre, decide il tratto di terra [10]. Se la terra supera il 10% della tratta, la frazione usata è il minimo fra continuità e copertura della terra.

Il guadagno tropo è il guadagno dello strato per la frazione usata. Se più di metà dei profili è sub-rifrattiva il guadagno è negativo.

## 10. Condotto su tutta la tratta

Se lo strato intrappola la banda su almeno l'80% della frazione usata e le stazioni non sono in vista, si calcola anche la perdita nel condotto: spazio libero, più l'accoppiamento delle antenne (3 dB se sono dentro il condotto, di più se sono sopra o sotto), più l'attenuazione residua γd = 5·10⁻⁵·ae·f^(1/3) dB/mrad di ITU-R P.452 eq. 49 [6]. Se è minore della perdita senza tropo diventa la perdita prevista.

Una banda resta nel condotto solo se la sua lunghezza d'onda non supera λmax = 2√2·(2/3)·√(ΔM·10⁻⁶)·d (Kerr, eq. 17 [11]). Per questo lo stesso strato può intrappolare i 23 cm e i 70 cm ma non i 2 m.

## 11. Confronto con ITU-R P.452

La riga Confronto P.452 applica il metodo ITU per condotti e riflessione su strati (P.452-16 § 4.5 [6]) con il ΔN mediano del GFS al posto di quello climatologico e p = 20% come costante di calibrazione, come nello studio CSIRO [10]. Comprende la perdita d'accoppiamento empirica Alf sotto 0,5 GHz (circa 28 dB a 144 MHz, 3,5 dB a 432 MHz), la schermatura dei siti, l'accoppiamento sul mare e l'assorbimento dei gas. È solo un confronto: la perdita prevista resta quella del paragrafo 9.

## 12. Antenne, segnale e rumore

- Yagi DL6WU: guadagno ≈ 8,6 + 8·log10(boom/λ) dBd, più 2,15 per i dBi [12].
- EIRP = potenza − cavo + guadagno TX.
- Segnale ricevuto = EIRP − perdita prevista + guadagno RX − cavo RX. S-meter IARU: S9 = −93 dBm, 6 dB per unità.
- Rumore esterno secondo ITU-R P.372 [13]: galattico (Fa = 52 − 23 log f) più artificiale secondo l'ambiente (città, residenziale, rurale, rurale tranquillo) oppure una temperatura d'antenna scritta a mano.
- Tsys = Ta + 290(Lc − 1) + 290(F − 1)·Lc ai morsetti dell'antenna, con Lc la perdita del cavo e F la cifra di rumore.
- S/N in SSB, CW e FT8 nella banda di ciascun modo, confrontato con la soglia impostata.

## 13. Tempi di risposta e dati

Ogni calcolo misura i propri tempi: profili GFS (rete, analisi, pause obbligate di 20 s fra i lotti per il limite di Open-Meteo), terreno SRTM1 (con lo scaricamento dei riquadri, 6–7 MB l'uno, solo la prima volta), calcolo. La riga Tempo di risposta li riassume; nel registro le righe cominciano con TEMPI. All'aggiornamento della griglia il registro riporta anche la corsa GFS usata, quando è diventata disponibile e quanto sono vecchi i dati.

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
