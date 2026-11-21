# Fonti e formule

Raccolta di lavoro delle fonti consultate per la previsione tropo propria di LIVETROPOVHF (20–21 novembre 2026): fisica di base, dati disponibili, indici pubblicati, formule, esperienze radioamatoriali, validazione e decisioni. Le formule sono numerate come in LaTeX, (1), (2), …, e nel testo si richiamano con il numero fra parentesi. Il Manuale delle previsioni descrive come il programma le usa.


## 1. Premesse dell'autore

Osservazioni di IC8TEM che fanno da cornice al lavoro, con i riscontri trovati nelle fonti.

- **La geografia italiana chiede la diffrazione.** Con dorsali, promontori e isole a poche decine di km dalle stazioni, i contributi di diffrazione vanno calcolati con il parametro di Fresnel sullo spigolo principale, non solo con la terra liscia. Riscontro: in un confronto su terreno irregolare a 563 MHz (Rio de Janeiro, 27 punti fra 5 e 30 km), i metodi migliori sono stati P.526 con spigoli a lama, P.1812 e il cuneo diffrattivo, con errore medio fra −5 e +3 dB e dispersione di circa 9 dB; P.1546 è stato il più pessimista (da Costa, Rabanal Ramirez e Dias, IET Microwaves, Antennas & Propagation, 2018). Per i versanti ripidi vicini alla stazione PETOOL ricorda che servono propagatori ad angolo largo e il calcolo a due vie (onde riflesse dai rilievi).
- **Per entrare in un condotto lontano serve Fresnel.** Se il condotto comincia oltre l'orizzonte, il segnale ci arriva per diffrazione sopra gli ostacoli fino al suo inizio; dalla 1.3.2 il programma lo calcola (Manuale delle previsioni, § 10 ter). Riscontro: Rohde & Schwarz (2023) nota che le stazioni lungo un condotto elevato spesso non riescono a entrarci, mentre quelle vicine alle estremità sì.
- **Le condizioni cambiano in fretta.** Le teorie ci sono, ma le condizioni mutano in minuti, e nessun sistema di previsione prende i dati in tempo reale: il GFS esce ogni 6 ore, con passo orario e 0,25°, e una previsione può perdere valore nel giro di poco. Riscontro: Bray (QST, novembre 1961) misura oscillazioni delle mediane orarie di circa ±7 dB oltre 100 miglia; i condotti si spostano con il tempo atmosferico (Rohde & Schwarz); il nostro registro dei QSO del 29/12/2021–2/1/2022 mostra aperture che crescono e calano nel giro di ore.
- **Il miglior now-casting resta la rete DX cluster–PSKReporter.** Per sapere che cosa succede adesso, gli spot dei cluster, di PSKReporter (e di WSPR) battono qualunque modello: sono misure. La previsione dice dove guardare e quanto può durare; gli spot dicono se c'è. Il programma per questo mette spot e previsione sulla stessa mappa.

## 2. Scopo

L'idea è un terzo indice, calcolato da LIVETROPOVHF stesso partendo dai profili verticali di un modello meteo, da affiancare a Hepburn e F5LEN invece di sostituirli. Oggi il programma legge le loro mappe già pronte: un indice proprio direbbe anche *perché* c'è apertura (quota e spessore del condotto, banda minima intrappolata) e si potrebbe verificare con gli spot che il programma raccoglie già.

- Cosa può dare: probabilità di condotto per punto e per ora, quota della base e della cima, spessore, frequenza minima intrappolata, e lungo un percorso la frazione di tragitto dentro un condotto.
- Cosa non può dare: i condotti sottili (sotto qualche decina di metri) che i modelli globali a 0,25° non risolvono, e i condotti di evaporazione sul mare, che servono soprattutto dai GHz in su.
- Primo obiettivo pratico: 144 e 432 MHz in Europa, con verifica sugli spot di DX cluster, PSKReporter e decodifiche locali.

## 3. Fisica di base

Tutto parte dalla rifrattività N calcolata a ogni livello del profilo: il condotto c'è dove la rifrattività modificata M diminuisce salendo, cioè dove dN/dh scende sotto −157 N/km ([ITU-R P.453-13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)).

Rifrattività (P e e in hPa, T in kelvin):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Pressione di vapore dall'umidità relativa H (%) e dalla temperatura t (°C), sopra l'acqua tra −40 e +50 °C:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

Rifrattività modificata (h in km, oppure 0,157 · z con z in metri) e criterio di condotto:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN/dh (N/km) | Regime | Effetto a 144 MHz |
| sopra 0 | sub-rifrazione | portata ridotta |
| da 0 a −79 | normale (standard circa −40) | orizzonte radio con raggio equivalente 4/3 |
| da −79 a −157 | super-rifrazione | portata allungata, "tropo buona" |
| sotto −157 | condotto (trapping) | segnali intrappolati, DX oltre 1000 km |

Come ricavare N da un modello a livelli di pressione: per ogni livello si prende T, umidità relativa e altezza geopotenziale, si calcola N e M, poi il gradiente tra livelli vicini. I livelli sotto il suolo vanno scartati (a 1000 hPa sulle Alpi si è sottoterra).

Frequenza minima intrappolata: il condotto guida solo le onde abbastanza corte rispetto al suo spessore. Hepburn pubblica una tabella spessore → frequenza minima utile ([DX Info Centre](https://www.dxinfocentre.com/propagation/luf.htm)); è un limite morbido, sotto il quale il segnale cala gradualmente.

| Spessore del condotto | Frequenza minima utile |
| 61 m | 962 MHz |
| 91 m | 464 MHz |
| 122 m | 276 MHz |
| 152 m | 185 MHz |
| 183 m | 133 MHz |
| 213 m | 101 MHz |
| 305 m | 53 MHz |

Nella fascia VHF (spessori 120–300 m) i valori della tabella seguono abbastanza bene una legge f · d^1,5 circa costante; sotto i 100 m la formula sottostima la frequenza fino al 30–40%. Adattandola io (non è una formula pubblicata da Hepburn), con f in MHz e d in metri:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

In pratica: per i 2 m serve un condotto spesso almeno 170–180 m, per i 70 cm ne basta circa la metà. La formula che circola in rete, λmax = 0,014 · d^1,5, viene da Kerr (1951, cap. 1, eq. 18) e vale con **λ in centimetri e d in piedi**, per un condotto in cui M cala in modo lineare: con queste unità dà per i 2 m circa 180 m e per i 70 cm circa 89 m, cioè gli stessi valori della tabella di Hepburn, che ne deriva. Kerr avverte che non è un taglio netto: anche onde parecchie volte più lunghe risentono molto del condotto, e un modo che "perde" un po' può migliorare la ricezione anche con le stazioni fuori dal condotto. Uno studio su IEEE Access del 2019 conferma che i metodi classici (Kerr, Kukushkin) non descrivono il taglio reale ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)): la soglia va usata come indicazione, non come muro.

## 4. Dati disponibili gratis

Il punto debole è la risoluzione verticale: tra 1000 e 850 hPa i modelli danno un livello ogni 25–50 hPa, cioè ogni 200–450 m, quindi vedono bene le inversioni a 500–2000 m ma non i condotti sottili vicino al suolo. LIVETROPOVHF usa già Open-Meteo per isobare, temperatura a 850 hPa e inversione: l'indice proprio può partire dalle stesse chiamate.

| Fonte | Cosa dà | Risoluzione | Uso per l'indice |
| [Open-Meteo, GFS](https://open-meteo.com/en/docs/gfs-api) | T, umidità relativa, punto di rugiada, altezza geopotenziale per livello (1000, 975, 950, 925, 900, 850 hPa…); altezza dello strato limite | 0,25° sui livelli di pressione; 16 giorni, orario fino a 120 h, aggiornato ogni 6 h | profilo N e M, gradiente, base e cima del condotto; già nel programma |
| [Open-Meteo, ECMWF](https://open-meteo.com/en/docs/ecmwf-api) | stessi livelli dai modelli open-data 0,25°; al suolo dà il punto di rugiada | 0,25° | seconda opinione, utile per la concordanza tra modelli |
| [Open-Meteo, DWD ICON](https://open-meteo.com/en/docs/dwd-api) | T e umidità relativa sui livelli di pressione; in ICON-D2 mancano 800, 900 e 925 hPa | ICON-EU circa 7 km | terza opinione sull'Europa |
| [Radiosondaggi, Univ. Wyoming](https://github.com/NCAR/RAOBget) | profili misurati, due lanci al giorno (00 e 12 UTC), formato testo per stazione | molti livelli significativi, anche ogni poche decine di metri | verifica dell'indice; vedono i condotti sottili che il modello non vede |
| [IGRA, NOAA NCEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive) | archivio storico dei radiosondaggi, un file per stazione, più parametri derivati | come sopra | taratura su anni di dati e confronto con gli spot del passato |
| ERA5 (Copernicus) | rianalisi oraria dal 1940, 37 livelli di pressione | circa 28 km | statistica e taratura, non previsione |

- Con Open-Meteo l'altezza geopotenziale è sopra il livello del mare: i livelli che cadono sotto il suolo vanno scartati ([Open-Meteo, upper air](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)).
- Al suolo si aggiungono T e umidità a 2 m: danno il primo tratto di gradiente, quello dove nascono i condotti superficiali notturni sulla pianura e sul mare.
- Radiosondaggi italiani sull'archivio della University of Wyoming (weather.uwyo.edu/upperair/sounding.html): stazioni e numeri WMO nella tabella qui sotto.

| Stazione | WMO | Latitudine | Longitudine | Quota |
| Udine/Campoformido | 16044 | 46,03 | 13,18 | 94 m |
| Milano/Linate | 16080 | 45,43 | 9,28 | 103 m |
| Pratica di Mare | 16245 | 41,65 | 12,43 | 12 m |
| Brindisi | 16320 | 40,65 | 17,95 | 10 m |
| Trapani/Birgi | 16429 | 37,92 | 12,50 | circa 14 m |

Numeri e posizioni dall'[elenco stazioni CEDA](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html). Il formato testo di Wyoming dà per ogni livello pressione, altezza, temperatura, punto di rugiada e umidità: basta per N e M. Wyoming non ha un'interfaccia ufficiale, si usano script come [RAOBget di NCAR](https://github.com/NCAR/RAOBget); il College of DuPage tiene copie in testo per stazione ([esempio, 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)).

## 5. Indici e metodi pubblicati

Hepburn e F5LEN partono dagli stessi ingredienti fisici ma non pubblicano l'algoritmo completo; gli studi scientifici del 2008–2026 invece descrivono il metodo passo per passo, ed è da lì che conviene copiare la ricetta.

| Metodo | Dati di partenza | Che cosa calcola | Note utili per noi |
| [Hepburn Tropo Index](https://www.dxinfocentre.com/propagation/hti.htm) | uscite grezze dei modelli meteo del Servizio Meteorologico del Canada (MSC, come scritto sotto le sue mappe; non GFS/NAM), mappe disegnate con GrADS, algoritmo proprietario ([storia](https://www.dxinfocentre.com/propagation/history.htm)) | scala lineare 0–10 della curvatura dei segnali: 0 normale, 1–2 super-rifrazione, 3–4 qualche condotto, 5–6 condotti diffusi; valori negativi = sub-rifrazione | la scala da imitare, così il terzo indice si confronta subito con i primi due |
| [F5LEN, previsione mondiale](https://tropo.f5len.org/WW/info.php) | rifrattività in unità N; per l'Europa il modello ARPEGE di Météo-France, 7,5 km, più livelli tra 2 m e 1500 m ([note dell'autore](https://tropo.f5len.org/author/f5len/)) | indice medio per locatore, 40 passi da 3 ore, 4 calcoli al giorno, archivio di 28 giorni ([nuova versione](https://tropo.f5len.org/?p=504)) | conferma che serve un modello con molti livelli bassi |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf) | 60 livelli di modello ECMWF, dal suolo a 3000 m | minimo di dN/dz per colonna: condotto sotto −157 N/km, super-rifrazione sotto −78,7 N/km; base dello strato intrappolante | climatologia: Pianura Padana la zona europea con più condotti tutto l'anno; Mediterraneo in estate vicino al 100%, in inverno sotto il 15%; Italia oltre il 30% nelle notti estive |
| [Indermuehle e Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861) | GFS 0,25°, 21 livelli di pressione + campi a 2 m | M = N + 0,157 z; minimo dM/dz sotto i 3 km, base e cima del condotto, profondità del deficit di M; condotti sotto 40 m scartati | verificato per 7 anni su 25 canali da 88 MHz a 2,68 GHz; buona capacità fino a circa 3 giorni di anticipo (AUC circa 0,74), poi cala; GFS meglio di ERA5 |
| [Hämäläinen et al., FMI 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf) | Harmonie-AROME, 750 m o 2,5 km, 65 o 90 livelli | dM/dh < 0 sul profilo del modello, confronto con un traliccio da 4 a 59 m | conta più la risoluzione verticale che quella orizzontale; nessun modello vede i condotti sotto 15 m; l'umidità domina sul gradiente di temperatura |

Terzo servizio radioamatoriale già attivo: [TROPOCAST.EU](https://tropocast.eu/) di Artur SQ3A, per 2 m e 70 cm in Europa. Dalla pagina delle previsioni ([tropocast.eu/forecast](https://tropocast.eu/forecast)):

- **Dati**: ICON-EU del DWD (passo 0,0625°, circa 7 km), sorgente predefinita; in alternativa GDPS canadese (0,15°), meno dettagliato.
- **Metodo**: N e M secondo ITU-R P.453 dal profilo fra lo strato vicino al suolo e **850 hPa**; il riconoscimento degli strati, l'unione di condotti al suolo ed elevati e le soglie dei colori sono una taratura propria dell'autore, non pubblicata.
- **Prodotti**: vista "completa" 0–3000 m, più viste separate "al suolo" ed "elevata" (per ora solo nelle mappe da incorporare); punteggio 0–100 in nove classi (sotto 38 sfondo, 38–52 marginale, 53–58 debole, 59–65 moderato, 66–72 buono, 73–80 molto buono, 81–88 alto, 89–95 forte, 96–100 molto forte); previsione fino a +72 h a passi di un'ora, con animazione e archivio.
- **Zone**: Europa, Polonia, Regno Unito, Germania, Francia, Spagna, **Italia**, Repubblica Ceca, Croazia; interfaccia anche in italiano.
- **Riuso**: le mappe si incorporano con un iframe senza chiave API; esiste anche un'API documentata (OpenAPI e schema JSON). La licenza non è indicata: prima di usarlo come terza fonte accanto a Hepburn e F5LEN va chiesto all'autore.

Differenze dal nostro progetto: TROPOCAST si ferma a 850 hPa (circa 1,5 km), mentre i condotti elevati arrivano a 2000 m e oltre (caso LA0BY del 22/11/2018), e non dà un indice per banda né per percorso. Le classi 0–100 si possono confrontare con il nostro indice 0–10 sugli stessi giorni.

Struttura delle immagini di TROPOCAST (dal link fornito dall'autore del programma): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, dove n è il passo di previsione. Il sito vieta il prelievo automatico ai robot, quindi non ho potuto leggere l'immagine; anche per questo, prima di mostrare queste mappe dentro LIVETROPOVHF serve il permesso dell'autore.

Diario di osservazioni: [DJ5AR, categoria Tropo](https://dj5ar.de/?cat=174) (Magonza, 21 articoli 2017–2023, 2 m–13 cm). Casi descritti: condotto di più settimane dall'Irlanda (23 maggio–2 giugno 2023), FT8 verso la Bielorussia (settembre 2020), prova Italia–Germania sopra Alpi e Adriatico (agosto 2017). Ricorre il caso del condotto "troppo alto" (inversione verso 1000 m) che non si riesce a usare dal suolo: altra conferma che serve confrontare la quota del condotto con quella delle stazioni. Usa i radiofari come misura dell'apertura (fino a 1028 km) e i profili verticali di Parigi, Bordeaux e del DWD.

In sintesi, la ricetta comune: profilo verticale → N e M per livello → strato con dM/dh negativo → base, cima, spessore e forza del condotto → indice. La parte "segreta" di Hepburn è la taratura empirica sugli eventi reali, che noi possiamo rifare con gli spot.

## 6. Formule utili per il programma

Oltre a N e M (sezione Fisica di base), queste servono a passare dal profilo verticale a un numero per punto e poi per percorso. Le formule ITU sono ricostruite da un PDF letto in parte male: prima di metterle nel codice vanno ricontrollate sul documento ufficiale ([ITU-R P.452-18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)).

**1. Gradiente fra due livelli del modello** (h in km, M in unità M):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

**2. Forza e spessore del condotto**: spessore d = cima − base (per un condotto al suolo, base = suolo); forza come deficit di M fra base e cima.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

**3. Fattore di raggio terrestre equivalente** (P.452-18, eq. 5 e 6a), con ΔN = calo medio di N nel primo km, positivo, in N/km:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

Con ΔN = 40 si ha k circa 4/3 (atmosfera standard); con ΔN che si avvicina a 157 k tende all'infinito, cioè la Terra radio diventa piatta: è l'inizio del condotto.

**4. Orizzonte radio** di un'antenna alta h metri, con raggio equivalente a\_e in km:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

**5. Probabilità di condotto per la latitudine** (P.452-18, eq. 2), percentuale di tempo; φ = latitudine del centro del percorso, μ1 e μ4 tengono conto di quanto tragitto è su mare:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

A 41° N, tutto su mare (μ1 = μ4 = 1), dà circa 11% del tempo: è la base climatologica, utile come valore di riferimento da cui l'indice previsto si discosta.

**6. Perdita per troposcatter** (P.452-18, eq. 45; f in MHz, d in km, θ angolo del percorso in mrad, N0 rifrattività al suolo a centro percorso):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

Il termine −0,15 · N0 fa capire perché l'aria umida aiuta anche senza condotto: 50 unità N in più valgono 7,5 dB in meno di perdita. LIVETROPOVHF usa già la versione G3YGF della perdita da scatter.

**7. Perdita in condotto** (P.452-18, eq. 46–51): perdita fissa di accoppiamento più un'attenuazione per milliradiante di angolo, che cresce con la radice cubica della frequenza (f in GHz, a\_e in km):

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. Da Bean e Dutton (1966) e da Kerr (1951)

I due testi classici danno tre cose che servono subito all'indice: soglie di gradiente già tarate **sugli stessi livelli di pressione di GFS**, quanto vale in dB un cambio di rifrattività, e quali situazioni meteo creano o distruggono i condotti. Le pagine citate sono quelle di *Radio Meteorology*, NBS Monograph 92.

**1. Classificazione del profilo per strati di pressione** (Bean e Dutton, tab. 6.5, p. 248; −dN/dh in N/km). Pensata per i radiosondaggi, si applica così com'è ai livelli del modello:

| Strato | Quote circa | Sub-rifrazione | Non stratificato | Super-rifrazione |
| 1000–850 hPa | 0–1,46 km | −dN/dh < 0 | da 20 a 60 | oltre 100 |
| 850–700 hPa | 1,46–3,01 km | −dN/dh < 0 | da 20 a 50 | oltre 80 |
| 700–600 hPa | 3,01–4,20 km | −dN/dh < 0 | da 15 a 40 | oltre 70 |
| 600–500 hPa | 4,20–5,57 km | −dN/dh < 0 | da 10 a 30 | oltre 50 |

**2. Quanto vale in dB** (Bean e Dutton, cap. 6, pp. 233–258), misure da 70 a 180 MHz su percorsi oltre l'orizzonte:

- Qualunque strato (al suolo o in quota) rispetto al profilo "liscio" alza il segnale di **10–25 dB** a tutte le percentuali; il guadagno maggiore viene dagli strati elevati inclinati, probabilmente per effetto di focalizzazione (percorso di 200 km).
- La rifrattività al suolo Ns e il calo nel primo km ΔN sono correlati col segnale mediano mensile con coefficiente fra 0,4 e 0,95, mediana 0,7 (20 percorsi da 130 a 446 km, circa 100 MHz).
- Ciclo annuale: **0,18 dB per unità N**; ciclo giornaliero da 0,2 a 1,1 dB per unità N, massimo sui percorsi di 175–200 km e d'inverno.
- Il parametro composto di Misme stima la variazione di campo come 0,5 dB/(N/km) · (gradiente equivalente − 40) più un termine di stabilità termica; il parametro di turbolenza Π (area fra il profilo reale e quello adiabatico, 0–3 km) migliora appena rispetto al semplice ΔN. Conclusione degli autori: ΔN resta il parametro migliore per semplicità.

**3. Profilo di riferimento e anomalia** (Bean e Dutton, § 3.8 e 3.9). L'atmosfera esponenziale di riferimento CRPL dà il profilo "normale" a partire da Ns; l'unità A misura di quanto il profilo reale se ne discosta a ogni quota. È il modo naturale di trasformare un profilo GFS in un'anomalia da mettere sulla mappa:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

Con Ns = 313 si ha ΔN circa −42 N/km, cioè il raggio equivalente 4/3. Il criterio di condotto −157 N/km è ricavato anche qui dalla legge di Snell per un raggio che parte orizzontale (eq. 4.23), quasi indipendente dall'altezza dell'antenna.

**4. Dove e quando nascono i condotti** (Bean e Dutton § 4.4; Kerr cap. 3):

| Processo | Situazione | Effetto |
| Avvezione | aria calda e secca da terra che scorre su mare più freddo (tipico della Manica d'estate dopo giorni di bel tempo); conta soprattutto il deficit di umidità | condotto intenso sul mare |
| Irraggiamento notturno | cielo sereno, vento debole; nubi basse o molta umidità lo impediscono | condotto al suolo che nasce al tramonto, cresce in serata e sparisce rapidamente dopo l'alba, soprattutto all'interno |
| Subsidenza | aria che scende dall'anticiclone, scaldata e secca, sopra aria fresca e umida; frequente sottovento ai monti e nei settori sud-orientali degli anticicloni | strato super-rifrattivo o condotto elevato; quasi sempre gradiente superiore allo standard nell'inversione |
| Brezza di mare e di terra | coste, circolazione di poche centinaia di metri | condotti locali lungo la costa |
| Contro il condotto | rimescolamento: terreno scaldato dal sole fra metà mattina e pomeriggio, vento al suolo oltre pochi m/s su terreno accidentato; nebbia notturna | probabilità di condotto quasi nulla |

**5. Due ingredienti nuovi da provare.** Il moto verticale: correnti ascendenti nelle zone di convergenza rimescolano e cancellano gli strati, la divergenza dagli alti di pressione crea inversioni (Moler e Holden). Open-Meteo fornisce già la velocità verticale sui livelli di pressione. E la tendenza della pressione: su un percorso di 140 km a 186 MHz il segnale scendeva quando la pressione calava al trasmettitore e al centro del percorso e tornava alto e senza evanescenza quando risaliva, con appena 2 hPa di variazione in giornata (Bean e Dutton, fig. 6.9, p. 241).

**6. Da Kerr.** Oltre alla formula della lunghezza d'onda massima (sezione Fisica di base), Kerr dà una formula approssimata per M dalla sola temperatura, vapore e quota (§ 3.9, con le vecchie costanti 79 e 4800 poi sostituite da 77,6 e 4810) e osserva che a un'inversione il taglio del vento può generare onde che fanno oscillare in quota il condotto, causa possibile delle evanescenze profonde (§ 3.26). Fonte italiana segnalata da Bean e Dutton e da cercare: L. Bonavoglia (dicembre 1958), "Correlazione fra fenomeni meteorologici e propagazione…".

## 8. Da presentazioni radioamatoriali e rapporti tecnici (seconda serie)

Sette documenti forniti dall'autore del programma. Tre servono direttamente all'indice: W3SZ per il "pavimento" di troposcatter, LA0BY per i casi che Hepburn non aveva previsto, Hepburn (traduzione LU7MC) per la classificazione meteo dei condotti.

**1. Troposcatter: il segnale che c'è sempre.** R. Rehr W3SZ, "Tropospheric Scatter (Troposcatter) Propagation for VHF, UHF, and Microwave Frequencies", Pack Rats Conference 2014; J. R. Duffey KK6MC, "The Troposcatter Propagation Mode and How to Predict Coverage on the VHF/UHF Bands", CSVHF 2017.

- Utile da circa 100 a 700 km, da 144 MHz a 10 GHz; la perdita cresce di 30 log f con la frequenza.
- Conta soprattutto l'angolo di partenza verso l'orizzonte: ogni grado in più costa circa 9–12 dB (M1CNK, metodo Collins).
- D'estate il segnale è in media circa 10 dB più alto che d'inverno; a metà pomeriggio circa 5 dB più basso che al mattino presto o la sera. KK6MC indica una variabilità complessiva di 13–15 dB con ora, stagione e tempo.
- Metodo di Yeh, il più semplice da programmare (f in MHz, d in miglia, θ angolo di scatter in gradi, α media geometrica delle aperture dei due fasci):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Confronto di Larsen (1968) su 15 collegamenti reali: errore quadratico medio 5,1 dB per Collins, 7,4 per Yeh, 8,1 per NBS e CCIR, 16,8 per Rider. **Tutti i metodi migliorano se Ns si tiene costante** (Yeh scende a 5,4 dB): per lo scatter la rifrattività del giorno conta poco, quindi l'indice tropo deve guardare agli strati, non al solo Ns. La tabella dei 15 collegamenti, con perdite misurate (fra cui Feldberg–Monte Baldo, 322 km a 957 MHz, 223,1 dB; Cipro–Israele, 319 km a 77,5 MHz, 163,3 dB), è un buon banco di prova per un nostro calcolo.
- KK6MC dà la perdita che una stazione tipica riesce a superare sui 2 m e la distanza corrispondente, da 184 dB (FT-817 e 3 elementi in SSB, circa 130 km) a 215 dB (yagi lunghe in SSB, circa 430 km) e 236 dB (kW e yagi accoppiate, circa 640 km). È il modo per tradurre i dB dell'indice in chilometri guadagnati.
- Codice pronto: programma Yeh e Collins di W3SZ (nitehawk.com/w3sz/YehCollins.zip) e calcolatore di Bob Atkins KA1GT (bobatkins.com/radio/troposcatter.html).

Altra fonte sullo scatter: I. Roberts ZS6BTE, "Tropospheric Scatter Propagation", *Radio ZS*, dicembre 1984, con nota del 2013 ([qsl.net/zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). Utile soprattutto la geometria: angolo di scatter θ = θ0 − θ1 − θ2, con θ0 = d/R, θ1 = (h1 − h\_ostacolo)/d1 + d1/2R e lo stesso per l'altra stazione, R = 8446 km (4/3 del raggio terrestre). È il pezzo che serve per calcolare gli angoli dal profilo del terreno nel "pavimento" di troposcatter. Altri dati: ogni grado di angolo in più costa circa 10 dB; passare da Ns 280 a 300 riduce la perdita di circa 4 dB; il volume di scatter sta fra 300 e 2000 m per 150 km, fra 600 e 3000 m per 300 km; temporali nel volume di scatter alzano il segnale di circa 20 dB. Attenzione: l'esempio numerico dell'articolo non torna con le formule date (il termine di spazio libero a 900 MHz e 872 km è circa 150 dB, non 125), quindi le formule vanno controllate su W3SZ prima di usarle.

**2. Tipi meteorologici di condotto** (W. R. Hepburn, "Tropospheric Propagation Modes", dxinfocentre.com/propagation/tr-modes.htm, traduzione spagnola di LU7MC). Completa la tabella di Bean e Dutton con casi utili all'Italia:

| Tipo | Quando | Nota per l'indice |
| Irraggiamento | notti serene e calme su terra | segue il terreno, sparisce poco dopo l'alba |
| Alta pressione (subsidenza) | anticiclone | può durare tutto il giorno; di notte il condotto da irraggiamento sotto può bloccare i segnali lontani, per cui di giorno può andare meglio |
| Frontale | davanti a un fronte caldo, dietro un fronte freddo, a nord di un fronte stazionario | breve; il maltempo lo disturba |
| Discesa da montagna | föhn, bora: aria che scende, si scalda e si secca | da provare per bora sull'Adriatico e föhn a sud delle Alpi |
| Avvezione | aria calda e secca sopra terreno freddo e bagnato, per esempio dopo la pioggia | condotto al suolo |
| Valle | aria calda sopra aria fredda ferma in valle | può restare di giorno |
| Marino | aria calda e secca su acqua più fredda; arriva sulla costa con la brezza | picco nel pomeriggio; primavera e inizio estate le stagioni migliori |

I condotti di evaporazione sul mare (0–30 m) servono solo sopra circa 3 GHz e non entrano nell'entroterra: per 144 e 432 MHz si possono ignorare. Le montagne alte fanno da barriera; i deserti sono troppo secchi.

**3. I casi che Hepburn non aveva previsto** (S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", Nordic VUSHF 2019, da JO59IX a 500 m di quota):

- 22 novembre 2018: 70 cm fino a UA6 (circa 2600 km) per 6–8 ore. Hepburn indicava solo i Paesi baltici e la Polonia. Il condotto si vedeva nelle mappe di temperatura a 900 e 1500 m e finiva poco sopra i 2000 m; era più alto dal lato norvegese, e solo LA0BY in collina ci entrava. Al suolo il tempo non sembrava da tropo: freddo e nebbia in cima.
- 11 febbraio 2015: 2 m e 70 cm verso 9A e S5 (circa 1500 km) attraverso le Alpi, anche questo non previsto da Hepburn.
- 15 ottobre 2011: 2 m con EA1 (2135 km) in condotto elevato, su tre tratti di mare e tre di terra.
- Altri eventi: 6–11 novembre 2003 verso est (2050 km), 10 dicembre 2004 verso la Francia.
- Indizi sul campo: nebbia in pianura, cime libere, poco vento, terreno bagnato. Gradiente di temperatura normale −6,5 °C/km; in un caso reale (VK3KAQ) +7 °C in 20 m.

Conclusione dell'autore: Hepburn e F5LEN non bastano, servono i profili in quota lungo il percorso. Per noi: queste date sono **casi di verifica** da ricalcolare con i dati storici dei modelli, e l'indice sul percorso deve confrontare la quota del condotto con la **quota delle stazioni**.

**4. Esperienza italiana** (G. IV3GBO, "Types of VHF propagation SSB/CW", 2017): condotti fra 450 e 3000 km, con strati fra 450 e 3000 m; nebbia estesa come indizio; aperture via mare sull'Adriatico verso Puglia, Albania e Grecia. Scatter da 100 a 500 km.

**5. Introduzioni** (P. Wilton M1CNK, "An Introduction to VHF/UHF Propagation", 2013): in condotto i 70 cm spesso vanno meglio dei 2 m; con 10 m di palo a 144 MHz l'angolo di partenza è circa 3°, con 20 m circa 1,5°.

**6. Rapporto NASA** (E. Hirschmann, "Tropospheric and Ionospheric Effects upon Radio Frequency (VHF-SHF) Communication", Goddard X-731-67-89, 1967): riguarda gli errori di distanza verso i satelliti, non il DX. Riporta la stessa formula di N e il profilo esponenziale (esempio in Florida: Ns 370, ΔN −55, costante 0,161/km) e rimanda a due fonti NBS da cercare: Bean e Thayer, "CRPL Exponential Reference Atmosphere", Monograph 4 (1959), e Bean, Horn e Ozanich, "Climatic Charts and Data of Radio Refractive Index for the United States and the World", Monograph 22 (1960).

## 9. Da The VHF/UHF DX Book, capitolo 2

I. White G3SEK (a cura di), *The VHF/UHF DX Book*, DIR Publishing 1995 (ISBN 0 9520468 0 6; replica digitale 2017), capitolo 2 "VHF/UHF Propagation", pp. 2-13, 2-22–2-29 e 4-11. È il testo più vicino all'esperienza radioamatoriale fra quelli raccolti; conferma le soglie già usate e aggiunge regole sinottiche nuove.

**Numeri**

- Stessa formula di N (77,6 P/T + 3,73·10^5 e/T²); esempio: 1000 hPa, 7 °C, e = 10 hPa danno N = 324. In un'atmosfera ben rimescolata, per esempio dopo un temporale, N cala quasi lineare di circa 39 unità/km nei primi 2–3 km.
- Gradienti fra 0 e −79 N/km sono normali, sotto −79 c'è super-rifrazione, sopra 0 sub-rifrazione. Il libro insiste che i DX lunghi non vengono dalla semplice super-rifrazione, che dovrebbe restare giusta su tutto il percorso, ma dai condotti.
- Spessori minimi del condotto, un po' più prudenti di Kerr e Hepburn:

| Banda | Spessore minimo |
| 50 MHz | 400 m |
| 70 MHz | 300 m |
| 144 MHz | 200 m |
| 432 MHz | 100 m |
| 1,3 GHz | 50 m |

- Il rapporto non è proporzionale alla lunghezza d'onda (circa 65 λ a 50 MHz, 220 λ a 1,3 GHz) perché i bordi del condotto sono "sfocati". Nel Regno Unito i condotti sono frequenti sui 144 MHz ma quasi mai a 70 MHz o sotto, e spesso i 432 MHz vanno forte quando i 144 sono deboli.
- In condotto il segnale cala come 1/d invece che 1/d²: fino a 1000 km l'attenuazione può essere simile a quella dello spazio libero.
- Capacità di una stazione minima sui 2 m (100 W, yagi da 15 dBi, cifra di rumore 2 dB): 217 dB, cioè circa 400–500 km in SSB in condizioni medie; in CW circa 20 dB in più (p. 4-11). Concorda con la tabella di KK6MC.
- Scatter: la perdita sale di circa 15 dB triplicando la frequenza e di 10 dB per ogni grado di angolo di scatter (formula di Yeh; nel testo estratto la costante è illeggibile, quindi va presa dalla versione di W3SZ). Lo scatter "in avanti" dalle celle convettive copre di routine fino a circa 500 km, 1000 km con buone stazioni. Dopo piogge forti ed estese le condizioni sono scarse su tutte le bande.

**Tipi di condotto**

- Al suolo: sulla terra propagano male perché il terreno assorbe e diffonde; sul mare calmo arrivano lontano (Scilly–Canarie, 2500 km, circa una volta l'anno sui 2 m). I condotti di evaporazione sono spesso alti solo circa 15 m e servono alle microonde.
- Elevati: fra 450 e 2000 m, causa dei DX più lunghi su terra; il segnale "salta" sopra le stazioni che stanno sotto. I condotti "che perdono" danno segnali meno forti ma lasciano entrare anche le stazioni a distanza intermedia.
- I condotti hanno direzioni precise e possono durare ore o giorni, al contrario dell'E sporadico.

**Regole sinottiche nuove per l'indice**

- **Età dell'anticiclone**: l'aria scende al centro di pochi metri all'ora e l'inversione si abbassa man mano che l'alta pressione si sviluppa; le aperture maggiori arrivano dopo diversi giorni di propagazione già buona. Un anticiclone di breve durata non basta.
- **Bordo dell'alta pressione**: lo strato è a cupola e ai bordi è più basso, quindi le stazioni ci entrano meglio.
- **Direzione**: si è osservato che i percorsi più lunghi corrono lungo le isobare o attraverso le dorsali (Flavell G3LTP, 1984), ma ci sono ottimi collegamenti anche in direzione radiale dal centro. Esempio del 29–30 agosto 1987: dall'Inghilterra l'apertura ha ruotato di 180° in due giorni (Spagna, Francia, Italia, Austria, fino alla Norvegia) seguendo lo spostamento dell'anticiclone.
- **Contro**: isobare fitte (vento forte), nubi stratocumuliformi al bordo dello strato (limitano la frequenza più alta).
- **Fronti**: un fronte freddo spinge l'aria calda in alto e crea un'inversione lungo il fronte; la direzione migliore è lungo il fronte mentre passa, ma dura poco.
- **Stagione**: nel Regno Unito gli anticicloni sono più frequenti a fine estate e inizio autunno.
- **Indizi visivi**: fumo intrappolato in una linea scura sopra l'orizzonte; con alta pressione, stratocumuli con base sotto 1000 m indicano un condotto al suolo, base sopra 1000 m un condotto elevato.

Riferimenti citati da cercare: R. G. Flavell G3LTP, "Studies of an Extensive Anticyclonic Propagation Event…", *Radio Communication*, febbraio 1984, p. 128; J. Gannaway G3YGF, "Tropospheric Scatter Propagation", *Radio Communication*, settembre 1981, p. 710; P. W. Sollom G3BGL, "A Little Flutter on VHF", *RSGB Bulletin*, novembre e dicembre 1966; J. Priedigkeit W6ZGN, "Ray tracing and VHF/UHF Radio Propagation", *QST*, novembre 1986, p. 18.

## 10. Un modello radioamatoriale verificato: Biancomano e Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, "Practical Tropospheric Model for VHF/UHF Propagation Prediction", *National Weather Digest* 22:2, giugno 1998, pp. 15–24, **gratis** in [PDF dalla National Weather Association](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf). È l'unico lavoro raccolto che costruisce un previsore per 144, 222 e 432 MHz e lo verifica contro i collegamenti reali.

- **Dati**: radiosondaggi delle 00 e 12 UTC di 47 stazioni negli Stati Uniti orientali, 1995–1997; analisi dal suolo a 700 hPa (all'inizio fino a 400), sul profilo di N, senza M. Esclusi coste, isole e montagne.
- **Per ogni sondaggio**: presenza e quota del condotto, lunghezza d'onda massima che può portare (spessore circa 100 volte la lunghezza d'onda), pendenza massima della temperatura potenziale fra livelli vicini e fra un livello e il secondo sopra (per strati più spessi). I condotti al suolo non si usano.
- **Prima regola, persistenza**: gli indici di stabilità (lifted index, K, totals-totals, SWEAT) restano entro 3 unità dal sondaggio di 12 ore prima, e il livello di pressione dove la temperatura potenziale cresce di più resta circa lo stesso. Se è così: previsione di miglioramento nelle 12 ore successive.
- **Seconda regola, controlli dinamici**: strato limite che non si alza, CAPE, variazione dello spessore a 500 hPa su tre sondaggi, variazione della pendenza entro limiti.
- **Soglia di banda**: un condotto capace di portare 100 cm o più indica apertura sui 432 MHz, quasi certa sui 222 e spesso anche sui 144.
- **Filtro spaziale**: stazioni su una griglia esagonale di circa 400–500 km; la previsione vale solo se almeno **5 stazioni contigue** sono positive (se ne può saltare una). Con 5 stazioni la probabilità di un falso positivo casuale diventa trascurabile.
- **Verifica** contro gli eventi della rubrica "The World Above 50 MHz" di QST: nel 1996, 48 eventi previsti su 67 (71%); 12 dei 15 eventi maggiori. Precisione fino al 70% sui 432 MHz e circa 60% sui 144 MHz a 12 ore; prevede quasi il doppio degli eventi reali; 9 errori su 19 sono previsioni arrivate tardi, perché molti miglioramenti si formano in meno di 12 ore.
- **Aggiunta per le stazioni più a nord**: vento che ruota in senso orario salendo (avvezione calda) come condizione in più.

Dal PDF completo (fornito dall'autore del programma) tre dettagli in più. La loro regola "spessore = 100 volte la lunghezza d'onda" dà 600 m per i 6 m, 200 m per i 2 m, 125 m per 1,25 m e **70 m per i 70 cm** (fig. 3): per i 70 cm è meno severa di Kerr e Hepburn (circa 89 m), per i 2 m coincide col VHF/UHF DX Book. Il vento che ruota in senso orario salendo serve, secondo gli autori, soprattutto perché porta aria più calda in quota, che è ciò che fa crescere il calo di N con la pressione; l'umidità che porta con sé non sembra destabilizzare. L'appendice ricava dN/dh in forma chiusa per un'atmosfera a gradiente termico costante con un salto brusco di gradiente termico e di umidità a una quota data: utile come caso di prova per il nostro codice. Il loro tracciamento della pendenza della temperatura potenziale viene da K. H. Jehn, "The use of potential refractive index in synoptic scale radio meteorology", *J. Meteorology* 17, 1960, p. 264.

**Cosa prendere per il nostro indice**: (1) un termine di **persistenza**: confrontare il profilo con quello di 12 ore prima, che nelle previsioni orarie abbiamo gratis; (2) il **filtro spaziale**: un punto conta solo se il miglioramento copre un'area di qualche centinaio di km, cosa che riduce i falsi allarmi; (3) gli **indici di stabilità**, calcolabili da temperatura e rugiada a 850, 700 e 500 hPa (Open-Meteo dà già lifted index e CAPE); (4) il **vento che ruota con la quota** fra 1000 e 850 hPa; (5) il loro metodo di verifica, che è lo stesso che vogliamo fare con gli spot del programma.

Altra fonte nello stesso pacchetto, di poco uso per noi: P. K. Rastogi, "Stratosphere and Troposphere (S-T) Studies at Millstone Hill", NASA 1984 (documento 19840019069): radar a 440 MHz che misura la turbolenza dell'aria limpida fra 4 e 25 km, con strati sottili legati a onde di gravità e taglio del vento. Conferma che la turbolenza che produce lo scatter è a strati, ma riguarda quote troppo alte per i condotti.

## 11. Terzo giro di fonti (20 novembre)

**Casi storici di condotto elevato.** J. Macoun OK1VR, collegamenti in 2 m dalla vetta dello Sněžka (1602 m), autunno 1958, articolo del 1959 tradotto nel 2006 ([ok2kkw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)). Il 27–28 ottobre 1958 primo collegamento Cecoslovacchia–Inghilterra sui 145 MHz (G5YV, 1220 km) e molti altri fra 1100 e 1250 km, con segnali stabili dopo le 23. L'autore lo attribuisce a due inversioni sovrapposte, la superiore verso 2200 m, con il segnale che tornava a terra solo dove lo strato inferiore si dissolveva; condizione favorita da un anticiclone lento dall'Atlantico verso il continente. Da casa (Praga) il suo massimo era 530 km: solo le stazioni in quota entravano nel condotto. È un altro caso di verifica (come LA0BY 2018) e conferma che l'indice deve cercare condotti **sopra i 2000 m** e confrontarli con la quota delle stazioni.

**Bibliografie storiche NBS** (utili per cercare i lavori classici, non per formule):

- W. Nupen, *Bibliography on Tropospheric Propagation of Radio Waves*, NBS Technical Note 304, 1965, 356 pagine, con indice per argomenti (anche "previsione delle condizioni") e per paese. Voci italiane segnalate: L. Sacco, "Il collegamento radio in regime di superrifrazione atmosferica", *Alta Frequenza* 24(6), dicembre 1955 (ricerche italiane: super-rifrazione sotto 500 m frequente di giorno e di notte); S. Silleni, *Annali di Geofisica* 8(1), 1955; J. M. Clara e A. Antinori, prove fra Sardegna e Minorca da 238 a 297 MHz (390 km), *Electrical Communication* 33(2), 1956. Un precedente diretto del nostro progetto: R. F. Clarke (Fort Huachuca, 1956) emetteva ogni giorno la previsione della lunghezza d'onda massima intrappolata per la mattina seguente, da previsioni di umidità, temperatura, pressione e vento.
- R. L. Abbott, *Bibliography of Tropospheric Radio Wave Scattering*, NBS, materiale fino a gennaio 1960, circa 670 voci ordinate per anno: utile per la parte di troposcatter.

**Divulgazione e osservazioni**: F. W. Odenthal DK5EQ, "Ausbreitung 2m – Eine Einführung" (31/05/2024): quasi ottica fino a 200 km, tropo per condotto o rifrazione oltre, con alta pressione e in primavera e autunno; cita la pagina VHF di DM2HB e le note di DK5YA su tropo ed Es. A. Johnson VK3DXE sul blog di EI7GL (dicembre 2021): con WSPR si sono scoperti percorsi VHF di migliaia di km in Australia; la tropo non è solo estiva. Discussione su VHF marina e AIS ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)): portata in ottica 1,23·√h(ft) miglia nautiche per antenna, scatter fino a circa 50 nm, miglioramento tropo tipico 100–400 nm; i condotti si riconoscono perché le stazioni lontane arrivano forti come le vicine. Notiziario della Chelmsford ARS, dicembre 1983: nei contest in 2 m le stazioni della costa orientale inglese sono favorite dal percorso su mare verso il continente.

**ARRL, *The Radio Amateur's VHF Manual*, 1972, cap. 2 "Propagation Beyond the Horizon" (pp. 17–19).** Consigli operativi già noti (seguire le aree di alta pressione che si muovono lentamente e soprattutto i loro bordi posteriori; inizio autunno il periodo migliore; zone costiere, grandi laghi e valli dei fiumi favorite dalla stratificazione). Due dati nuovi: (1) la fig. 2-4 confronta l'atmosfera standard (umidità relativa 70% fino a 12000 ft, rifrazione minima) con un radiosondaggio tipico di forte rifrazione, con una rottura netta di temperatura e rapporto di mescolanza verso 4000 ft (circa 1200 m): è la firma che il nostro calcolo deve riconoscere sui livelli GFS; (2) i condotti veri sono rari sotto i 144 MHz e più frequenti in UHF, in accordo con gli spessori minimi. Bibliografia QST utile da cercare: Hull, "Air-Mass Conditions and the Bending of UHF Waves" (giugno 1935, maggio 1937); Hoisington, "Painless Prediction of Two-Meter Openings" (ottobre 1949); Collier, "Upper-Air Conditions for 2-Meter DX" (settembre 1955); Botts, "A Night to Remember" (gennaio 1970, condotto su terra).

**PA0EHG, pagina "Tropo"** ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): riassunto del troposcatter dall'articolo di G3YGF, già nel manuale. Utili due regole pratiche per controllare il nostro calcolo di scatter: circa 10 dB di perdita in più per ogni grado di angolo all'orizzonte di ciascuna stazione, e oltre 100 km circa 9 dB in più ogni 100 km di percorso (60 miglia); il volume comune parte da circa 600 m su 100 km e da circa 9 km su 480 km. Non tratta condotti, sondaggi né soglie di rifrattività.

**PA0EHG, calcolatore PROPBER** ([pagina](https://pa0ehg.com/propagat1.htm), programma propber.exe fornito dall'autore del programma). È un programma DOS di 36 kB scritto in BASIC e compilato con il Microsoft BASIC Compiler 5.60 (1985): gira solo sotto DOS o DOSBox. Dai testi interni: chiede frequenza, distanza (oppure i due locatori, anche nel vecchio formato QRA "E-locator"), per ciascuna stazione altezza dell'antenna, cifra di rumore, banda FI, potenza, un ostacolo (altezza e distanza) e guadagno d'antenna (0 = calcolo da diametro della parabola, efficienza 50%). Restituisce la distanza massima in ottica, la perdita in ottica corretta per l'assorbimento del vapore (0,1 dB/km da 24 GHz), la perdita di troposcatter "corretta per i siti" (cioè per gli angoli d'orizzonte dati dagli ostacoli) e il rapporto S/N nei due sensi, anche "con la massima propagazione anomala". Le costanti numeriche non sono leggibili nell'eseguibile compilato. Il metodo suggerito per le bande alte è utile anche per noi: tarare il calcolo su un collegamento misurato (differenza fra S/N misurato e calcolato) e applicare la stessa differenza a un'altra banda. È lo stesso schema del nostro calcolo, che però usa il profilo SRTM1 invece di un solo ostacolo e la previsione GFS invece di un margine fisso.

**PROPBER, costanti lette nell'eseguibile** (numeri in formato Microsoft Binary Format). Sicure, nella tabella dei dati accanto ai testi: 8,68589 (= 20/ln 10, cioè 20 log calcolato con il logaritmo naturale), 32,5 (spazio libero: 32,5 + 20 log f\[MHz\] + 20 log d\[km\]), 24000 e 0,1 più 40000 e 0,3 (vapore: 0,1 dB/km da 24 GHz, 0,3 dB/km da 40 GHz), 290 e −228,6 (rumore: 10 log k = −228,6 dBW/K/Hz con T0 = 290 K), π (guadagno della parabola), e le costanti dei locatori con 20004,5 km di mezza circonferenza terrestre (raggio circa 6368 km). Probabili, perché ripetute ma dentro il codice: −69,5 (lo stesso termine costante della formula di side-scatter di Yeh nel VHF/UHF DX Book), 332 (rifrattività al suolo), 8733 km (raggio equivalente, k ≈ 1,37). La formula esatta dello scatter non è ricostruibile senza disassemblare il programma.

**PROPBER, formule ricostruite dal codice** (disassemblaggio della parte compilata; le operazioni sono chiamate alla libreria del BASIC con gli operandi in memoria, i cui valori in doppia precisione sono stati decodificati). Le costanti −69,5, 332 e 8733 trovate prima erano falsi positivi dentro la libreria.

- Distanza in ottica: d = 5 · (√h1 + √h2) km (h in m). È più generosa del classico 4,12 (k = 4/3): equivale a k ≈ 2.
- Perdita in ottica: L = 32,5 + 20 log f\[MHz\] + 20 log d\[km\], più 0,1 dB/km sopra 24 GHz e altri 0,3 dB/km sopra 40 GHz.
- Troposcatter: Lts = L + 23 + 10 log f + 0,0674·d, cioè **55,5 + 30 log f + 20 log d + 0,0674·d** (formula di G3YGF con la costante 2 dB più alta). Nessun termine d'angolo né di rifrattività.
- Correzione "per i siti": il programma chiede ostacolo e distanza ma il blocco che dovrebbe usarli è vuoto; la variabile della correzione resta 0, quindi la perdita "corretta" è uguale a quella di scatter.
- Rumore: T = 290·(10^(NF/10) − 1), N = −228,6 + 10 log T + 10 log B (dBW), senza temperatura d'antenna.
- S/N = 10 log P + G\_tx − L + G\_rx − N. "Con la massima propagazione anomala" usa la perdita in ottica al posto di quella di scatter, cioè il caso di condotto ideale senza perdite oltre lo spazio libero.

Confronto: a 300 km e 144 MHz PROPBER dà circa 189,9 dB, G3YGF 187,9, la nostra P.452 187,8 (con angoli d'orizzonte nulli): lo scatter coincide entro 2 dB. Il nostro calcolo aggiunge gli angoli d'orizzonte dal profilo, la temperatura d'antenna, e al posto del "massimo" fisso lo strato previsto con accoppiamento e attenuazione residua.

**Rohde & Schwarz, P. Denisowski, "Understanding VHF Propagation", Educational Note, aprile 2023** (24 pagine, divulgativo). Conferma i punti già raccolti (condotto come guida d'onda a due dimensioni, segnale che cala in modo circa lineare con la distanza, fino a 1500 km e oltre, durata da ore a giorni; condotti al suolo buoni sul mare e scarsi su terra; condotti elevati da poche centinaia di metri a qualche km). Due osservazioni utili per l'indice: (1) **evoluzione nel tempo**: un condotto appena formato porta spesso solo le frequenze più alte e, ispessendosi, comincia a portare anche le più basse; la previsione oraria dovrebbe quindi mostrare i 432 MHz "in anticipo" sui 144 MHz quando lo strato cresce; (2) **posizione delle stazioni**: le condizioni migliori sono fra stazioni vicine alle estremità del condotto, mentre quelle lungo il percorso di un condotto elevato spesso non riescono a entrarci, salvo condotti "che perdono". I condotti si spostano con il tempo atmosferico.

**Sito PA0EHG, altre pagine.** Per la tropo interessa soprattutto PathProfile di M. Willis G0MJW ([scheda](https://pa0ehg.com/srtmp.htm), [download](http://www.mike-willis.com/Software/ASTERPathprofile.zip)): profilo del percorso da file di quote e perdita complessiva prevista, giudicata affidabile per il troposcatter in microonde; non tratta lo scatter da aerei né da pioggia. Utile come secondo riferimento per confrontare i nostri profili SRTM1 e le perdite. Il resto del sito riguarda EME, microonde, scatter da aerei (AirScout, mappe di SM7LCB) e misure in 80 m.

**PathProfile di G0MJW (Mike Willis), pacchetti ASTER e SRTM e SiteData** (forniti dall'autore del programma; [pagina del software](https://www.mike-willis.com/software.html)). Programma Windows del 2018: profilo da ASTER o SRTM a 1" o 3", perdite in ottica, di diffrazione, di troposcatter (valido fino a 1000 km), di condotto e gassose, con percentuale di tempo, k mediano del percorso, frazione di percorso sul mare e distanza dalla costa: la struttura è quella di P.452/P.1812, ma l'aiuto (file .chm compresso) non si è potuto leggere qui. SiteData converte i locatori e dà la quota da GTOPO30.

La parte più utile sono i **file di dati inclusi: le mappe statistiche di ITU-R P.453** su griglia globale di 1,5° (121 righe da 90°N a 90°S, 241 colonne da 0° verso est): gradiente dN/dz nei primi 65 m superato per l'1, 10, 50, 90 e 99% del tempo; per i condotti al suolo percentuale di presenza, spessore e intensità; per quelli elevati presenza, quota della base, spessore, intensità e quota massima; più il termine umido e la temperatura. Valori annuali interpolati:

| Luogo | Condotto al suolo: % / spessore / intensità | Condotto elevato: % / base / spessore / intensità | dN/dz superato 10% del tempo |
| Napoli | 15% / 126 m / 15 M | 18% / 786 m / 222 m / 10,7 M | −380 N/km |
| Tirreno (40N 12E) | 20% / 118 m / 15 M | 18% / 763 m / 228 m / 10,9 M | −674 N/km |
| Trapani | 26% / 113 m / 16 M | 19% / 767 m / 234 m / 11,1 M | −520 N/km |
| Brindisi | 13% / 131 m / 15 M | 17% / 832 m / 209 m / 10 M | −405 N/km |
| Adriatico (43N 15E) | 12% / 113 m / 13 M | 13% / 917 m / 203 m / 9,5 M | −399 N/km |
| Milano | 9% / 78 m / 9 M | 6% / 1017 m / 178 m / 7,5 M | −172 N/km |
| Udine | 10% / 78 m / 9 M | 6% / 1079 m / 175 m / 7,6 M | −176 N/km |

Per il nostro indice sono un riferimento climatologico pronto: spessore e intensità tipici dei condotti al posto dei 50 m fissi del condotto potenziale, e un'anomalia "oggi rispetto al normale" per zona. Sono dati ITU: liberi per uso non commerciale, da verificare prima di includerli in una versione pubblica. Il file ZONES01.DEM (58 MB, griglia di 2') contiene le zone climatiche entroterra/costa/mare; il formato non è documentato e non l'ho decodificato. La pagina delle equazioni di Willis riporta solo costanti fisiche, spazio libero e guadagno d'antenna.

**Quarto giro di fonti (20 novembre).**

**Indermuehle e Suzuki 2026, previsione dei condotti dal GFS per l'osservatorio CSIRO di Murchison** (arXiv 2608.12861): è la fonte più vicina a LIVETROPOVHF. Profili di N dal GFS a 0,25° (superficie a 2 m, poi i livelli isobarici, quelli sotto il suolo scartati); per ogni colonna minimo di dM/dz sotto 3 km, base e cima del condotto, deficit di M integrato; poi P.452-16 e P.1812-6 con terreno GLO-90 lungo 25 tratte da 98 a 1005 km, da 88 MHz a 2,68 GHz, ogni ora. Risultati: AUC mediana 0,72 su 25 canali; la percentuale di tempo p di P.452 va usata come costante di calibrazione (scelta p = 20%), perché il modello ITU con un ΔN istantaneo non è definito dentro un condotto; l'abilità resta quasi piatta fino a 3 giorni di anticipo. A 162 MHz (AIS) le tratte ricevute hanno la frazione di percorso su terra in condotto più alta del 67% (0,26 contro 0,16) e la profondità massima del condotto più alta del 49% (28 contro 19 M) rispetto ai controlli: sul mare il condotto c'è quasi sempre, decide il tratto di terra. Il GFS vede condotti su terra la metà delle volte rispetto a ERA5 (4,5% contro 10,4% delle celle-ora), mentre sul mare ne vede di più (73% contro 56%). Un indice di "corridoio" (condotti continui lungo la tratta, strati con spessore minimo 40 m) migliora il risultato: conferma che la continuità lungo la tratta è il criterio giusto.

**Longley e Rice, ESSA ERL 79-ITS 67 (1968)**: metodo per terreno irregolare da 20 MHz a 20 GHz (poi ITM), con irregolarità del terreno Δh, Ns da 250 a 400, raggio equivalente 8497 km (4/3), orizzonti, diffrazione, scatter in avanti e variabilità nel tempo e nei luoghi per clima. Riferimento statistico, non istantaneo.

**ITU-R P.834-2**: curvatura del raggio, raggio terrestre equivalente k·a, M = N + h/a (h in m, a in migliaia di km), modello esponenziale; nulla di nuovo rispetto a quanto già usato.

**AGARD CP-70 (1970), propagazione troposferica**: strati sottili da 1 a 100 m con forti gradienti sono comuni; il coefficiente di riflessione di uno strato elevato è circa proporzionale alla lunghezza d'onda, con una dispersione casuale di circa 14 dB. Misure a 90 MHz: calo di 9–10 dB legato a un salto di N concentrato a circa 350 m, alla sommità di uno strato di nubi. Il passaggio di un fronte freddo elimina gli strati: resta la diffrazione.

**Rapporto URSI USA, Commissione 2 (1960)**: spessori medi degli strati elevati da 60 a 210 m (dato coerente con P.453), gradiente minimo per il condotto 157–160 N/km.

**Buscemi K2OVS, guida alla propagazione VHF**: distanze tipiche per inversione e condotto per banda, condotti legati ad alte pressioni stazionarie, massimi all'alba.

**Wolinsky-Mancini e altri 2024, AIS alle Canarie**: radiosondaggi e ricezioni AIS; i condotti sono molto più frequenti nei mesi caldi (inversione degli alisei).

**Articoli nigeriani e malesi** (Ekah, Iloke, Mat, Igwe): correlazioni locali fra temperatura, umidità e perdita misurata, deboli e contraddittorie (r da −0,09 a 0,27); utili solo come conferma che le grandezze al suolo da sole non bastano.

**Herben 1984, tesi TU Eindhoven** (microonde): tratta in vista a 30 GHz su 8,2 km e satellite OTS a 11,6 GHz; scintillazione per turbolenza, multipercorso e pioggia. Per le microonde le perdite dinamiche sono dominate da pioggia e turbolenza, non dai condotti.

**Baars, The Paraboloidal Reflector Antenna** (microonde): guadagno di una parabola G = η·(πD/λ)² con efficienza tipica 0,5–0,65; perdita per errori della superficie secondo Ruze, ηs = exp(−(4πε/λ)²). Servirebbe se aggiungiamo le parabole come alternativa alla Yagi DL6WU per 1296 MHz e oltre.

Già analizzati o di scarso contenuto: Hirschmann NASA X-731-67-89 (doppione), radar VHF ST di Reid (venti e turbolenza, non condotti), slide Scribd e appunti generici sulla troposfera.

**Quinto giro di fonti (20 novembre, sera).**

**Rogers 1997, Radio Science 32(1)**: stima bayesiana dei parametri di un condotto elevato (quota della base, deficit di M, spessore) dai fattori di propagazione misurati oltre l'orizzonte, con un modello a equazione parabolica; esperimento VOCAR in California a 143, 263 e 375 MHz, confronto con radiosondaggi. Il segnale dipende molto dalla quota della base e dal deficit di M (correlazione 0,81 a 143 MHz per il deficit), quasi per niente dallo spessore (−0,17): lo spessore non si può ricavare dal segnale. Per noi: il calcolo B (spessore climatologico) conta meno del deficit e della quota; e il metodo è la base per il back office, cioè ricavare il condotto dai rapporti a più frequenze.

**Moler e Holden 1960, J. Res. NBS 64D**: rinforzi di 20 dB e oltre per ore sulle tratte VHF/UHF oltre l'orizzonte legati alla subsidenza (aria che scende) negli anticicloni; i moti ascendenti sollevano e rompono gli strati e danno affievolimenti profondi; escursioni fino a 60 dB. Spiega il legame fra alta pressione e inversione osservato da I5MZY: il predittore fisico è il moto verticale dell'aria, disponibile anche nel GFS.

**Banafaa e Muqaibel 2025, IEEE Access**: rassegna sui condotti (modelli di evaporazione, equazione parabolica, stime da radar, GNSS e lidar) e classificazione con apprendimento automatico su misure reali; le macchine a vettori di supporto danno i risultati migliori. Utile come bibliografia; i dati sono del Golfo Persico.

**ITU-R P.834-7 (2015)**: versione aggiornata della raccomandazione sulla rifrazione troposferica; nessuna formula nuova rispetto a quelle già usate.

**Abbott, bibliografia NBS sullo scatter troposferico (GOVPUB-C13)**: già nel terzo giro. Slide Scribd sui condotti: contenuto generico. L'articolo ScienceDirect 0021916987900304 non si è potuto aprire.

**Sesto giro (20 novembre, notte): Bean e Dutton riletto, ARRL Handbook 2022 e 2023, Balanis.**

**Bean e Dutton, Radio Meteorology (cap. 6), parti non ancora usate.** Sensibilità del campo a Ns: 0,18 dB per unità N sul ciclo annuale; 0,14–0,24 dB/N nel ciclo diurno (massima al mattino presto), fino a 0,2–1,1 dB/N sulle tratte di 175–200 km d'inverno; correlazioni 0,8–0,95. Parametro composto di Misme: M = a(g\_e − 40 + b·ΔW^n) con a = 0,5 dB per N/km, che unisce il gradiente equivalente e la stabilità termica ΔW (lavoro per sollevare l'aria di 1 km): nel Sahara a 471 MHz su 160 km la sola stabilità vale 20 dB fra gennaio e giugno a gradiente quasi uguale. Indice di rifrazione potenziale K (N riportato adiabaticamente a 1000 hPa): mette in evidenza le masse d'aria e la subsidenza anticiclonica (Flavell e Lane).

**ARRL Handbook 2022 (99ª ed.), § 19.4.3–19.4.4.** Inversioni utili fra 250 e 2000 m; le inversioni di radiazione (sera, aria calma, terreno asciutto) raramente fanno condotto vero; gli anticicloni lenti danno i condotti più estesi per subsidenza, con inversioni fra 500 e 3000 m, più forti la sera e al mattino; nell'emisfero nord le tratte migliori stanno a sud del centro dell'alta. Fronte caldo: condizioni buone alcune centinaia di km davanti; fronte freddo: inversione forte ma instabile dietro il fronte, parallela. Foehn (anche nelle Alpi) e brezza di terra (aria fresca al suolo, calda a 200–300 m; tratte oltre 800 km lungo le coste). Scirocco: aria calda e secca dal Sahara sopra aria marina più fresca, inversioni fortissime dal Mediterraneo orientale a Gibilterra, probabile causa dei record oltre 1500 km in microonde. Troposcatter: limite teorico circa 800 km, massimi tipici la metà. La 100ª ed. (2023) ha un testo più breve: inversioni all'alba e al tramonto fino a +20 dB e portata +20–50%; tratte di 1200 miglia sui Grandi Laghi, tipiche 400–600; consiglia Radio Mobile (modello ITM) per la copertura e le mappe di Hepburn.

**Balanis, Antenna Theory (4ª ed.), § 10.3.3**: tabella di Viezbicke (NBS TN 688, 1976) per Yagi ottimizzate a 400 MHz: boom 0,4 / 0,8 / 1,2 / 2,2 / 3,2 / 4,2 λ → 7,1 / 9,2 / 10,2 / 12,25 / 13,4 / 14,2 dBd. La nostra formula DL6WU (8,6 + 8 log10 boom/λ) dà 1–1,7 dB in meno fra 0,8 e 4,2 λ e torna in linea con le tabelle DL6WU verso 9,5 λ: stima prudente.

**Settimo giro (21 novembre): QST, QEX, W3SZ, repository GitHub.**

**QST 2000–2025 (14 numeri, 10 letti con riconoscimento del testo).** Solo resoconti nella rubrica The World Above 50 MHz, utili come casi per validare il modello: 5–9 settembre 2000, condotto a 144 MHz e oltre su metà orientale degli USA; 14–15 agosto 2022, apertura Manitoba–Kansas/Missouri sotto un'alta pressione in una giornata calda, da 144 a 1296 MHz quasi tutto in FT8, record canadese a 10 GHz di 1228,56 km (VE4MA–N0OY) in Q65, segnali fino a 1941 km; la mappa di propagazione APRS mostrava la stessa zona. QST novembre 2022 consiglia le mappe di Hepburn. Nessun articolo tecnico sulla previsione tropo.

**QEX e K1JT, Franke e Taylor (QEX 2020) sulle modalità FT4/FT8**: soglie di decodifica FT8 −20,8 dB in 2500 Hz su canale senza evanescenza, −18,6 dB con evanescenza alle medie latitudini; FT4 −17,5 dB. Gli altri numeri di QEX 2023 e 2026 non trattano la tropo (tre numeri 2023 sono scansioni non lette). **W3SZ 2019**: soglie delle modalità lente (JT4A −23, JT9A −27, JT65A −25, QRA64A −26, FT8 −21 dB) e criteri di scelta fra sensibilità e tolleranza allo spostamento di frequenza.

**GitHub.** TropoLink (LukiBox, Apache-2.0): progettazione di tratte troposcatter con P.617-5 (con N0 e ΔN), NBS TN101 e ITM affiancati, e la loro differenza mostrata come incertezza; P.617-5: L = F + 22 log f + 35 log θ + 17 log d + Lc, F = 0,18·N0·e^(−hs/7,35) − 0,23·ΔN, Lc = 0,07·e^(0,055(Gt+Gr)). andi-f/troposcatter (DL2FZN 2013, dal VHF/UHF Handbook RSGB): Lts = 24,44 + 4,283 ln f + 0,06656 d sopra lo spazio libero, pendenza vicina a PROPBER (0,0674 dB/km). QRadioPredict: ITM e ITWOM 3.0 con SRTM3, uso del suolo Corine e diagrammi NEC. Non pertinenti: Tropos (app meteo), opera-adt/tropo e troposim (ritardo troposferico per InSAR), tgbot\_troposcatter (stazione militare), sdr-troposcatter-gui (pannello di controllo), Troposcatter-Propagation-Delay (ritardo da ERA5), Radio-Communication-Project (simulatore didattico), PyLayers (canale radio in interni).

**Ottavo giro (21 novembre): modelli NTIA e ITU su GitHub.** NTIA/itm (Longley-Rice, ITS): 20 MHz–20 GHz, perdita da spazio libero, diffrazione e troposcatter con il profilo del terreno, terminali 0,5–3000 m, rifrattività superficiale N0 (minimo mensile ridotto al livello del mare, 250–400), sette climi, costanti del suolo e variabilità per tempo, luogo e situazione; modi 1 vista, 2 diffrazione, 3 troposcatter; C++ con involucro .NET Framework 4.7.2. Non modella i condotti: serve come riferimento "atmosfera normale" con la variabilità statistica. NTIA/p528 e eeveetza/p528 (MATLAB): ITU-R P.528-5 per collegamenti aria–suolo, 100 MHz–30 GHz, terminali 1,5–20 000 m, p 1–99%; involucro .NET 4.8. Possibile uso: bilancio dello scatter da aerei (due tratte suolo–aereo più la sezione radar). eeveetza/p2001 (MATLAB): ITU-R P.2001-6, modello a tutto campo con diffrazione, troposcatter, propagazione anomala (condotti e strati, Lba) ed E sporadico, percentuale di tempo 0–100%; mappe digitali ITU (DN\_Median, DN\_SupSlope, DN\_SubSlope, dndz\_01, TropoClim, h0, surfwv, Es) non ridistribuibili. Uso possibile come confronto ITU al posto di P.452, con il ΔN del GFS al posto delle mappe come nello studio CSIRO; le mappe andrebbero scaricate dall'utente come per P.453. Validazione 1.3.0 su 593 QSO (29/12/2021–2/1/2022): separazione evento/confronto 0,79 senza correzioni, 0,91 con Misme e subsidenza; il GFS non vede il condotto sul mare a 144 MHz (guadagno senza correzioni mai oltre 15 dB).

**Nono giro (21 novembre): IU3QEZ, NTIA, PyWaveProp, ACE High, validazione 1.3.1.** IU3QEZ (S. Fabris), presentazione "Oltre l'orizzonte in VHF" (GEKO Magazine n. 69, settembre 2026): volume comune d²/(8·R\_eff) con R\_eff ≈ 8500 km (1,3 km a 300 km, 3,7 km a 500 km, 9,4 km a 800 km), angolo di scatter θ = d/R\_eff + α (circa 0,7° ogni 100 km), regola di Bray (QST novembre 1961): ogni grado d'orizzonte vale 69 miglia (111 km), margine di 7 dB per l'evanescenza e 4 dB per la riflessione dal suolo; tesi: 10 dB valgono circa 100 km; stazione IQ3QC 5 W con 3×5 elementi, 120 QSO, massimo 788 km (Alpe Adria 2026). Bibliografia della presentazione da recuperare: Norton 1959 (J. Res. NBS 63D, System Loss), Bray 1961, ARRL Antenna Book 2023 fig. 4.29, Rice-Longley-Norton-Barsis NBS TN 101, Bean e Thayer 1959, Barsis e Kirby 1961 (diffrazione su crinale), Deygout 1966, P.1812, AirScout DL2ALF, P.834 fig. 2 (angolo critico di intrappolamento), GEKO n. 70 (confronto Bullington, Deygout e Longley-Rice su 12 percorsi). NTIA PropLib (wiki): eHata, ITM, LFMF, P.528, P.2108 (perdita da ingombro), con C++, .NET, MATLAB e Python; nessun modello di condotto; pagina ITS: anche IF-77 (aria–suolo) e MPM (1 GHz–1 THz). PyWaveProp (Python): metodi di tipo equazione parabolica con approssimanti di Padé, diffrazione a più spigoli, involucri per PETOOL e RAM, condotti troposferici ed elevati, terreno irregolare: base possibile per un calcolo di campo con il profilo M(z) del GFS, da verificare. APRS PropView (Apache 2.0): mappe di propagazione VHF dai pacchetti APRS, statistiche orarie e allarmi d'apertura; fonte possibile di dati 144 MHz per la validazione. Rete troposcatter NATO in Italia (fortificazioni.net): 900 MHz, 10 kW, parabole da 20 m; stazioni fra cui Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Vulture, Monte Lauro; caso di prova storico per P.617. Non pertinenti: Gupta 2021 (trasporto stratosferico), Law 2023 (ozono artico), dolphin #590 e opera-adt/tropo (ritardo InSAR), tropo.ino (sensori di gas Arduino), tropo-webapi (telefonia), cloudtools/troposphere (AWS). Validazione 1.3.1 sui 564 QSO tropo: AUC 0,79 (modello 1.2), 0,80 (+ condotto morbido), 0,90 (+ Misme e subsidenza), 0,90 (+ persistenza); colori di condotto (≥ 16 dB) sul 46% delle tratte dell'apertura e sullo 0% del confronto.

**Aggiunte allo stesso giro.** mikelytaev/wave-propagation (MIT, Python 3): equazione parabolica con approssimanti di Padé e Numerov, terreno irregolare, condotti troposferici; carica la rifrattività modificata M(quota, lat, lon) dal GFS NOAA e vi innesta un condotto di evaporazione di Monin-Obukhov; nessuna funzione documentata che restituisca la perdita sul percorso. È il candidato migliore per un futuro calcolo di campo con il profilo M del GFS (servirebbe un servizio esterno o una traduzione). RP-Sim (AGPL-3.0, JavaScript): modelli empirici per reti radio (Hata, Egli, Ericsson, due raggi), nessuna tropo. RIMFading (GPL-3.0, OMNeT++): irregolarità della portata per simulazioni di reti, distribuzione di Weibull, nessuna tropo. PyLayers: canale radio in interni, già escluso. qradiopredict (kantooon, GPL v2): ITM e ITWOM v3.0 con SRTM3 e Corine, già nel settimo giro come QRadioPredict.

**Decimo giro (21 novembre): Antenna Book, Norton, Barsis, nuove sorgenti di dati.** ARRL Antenna Book (23ª ed.), cap. 4 (K9LA): metodo di Bray (QST novembre 1961) per la portata VHF: perdita di percorso su Terra liscia al 99% (a 300 miglia, 144 MHz: 214 dB), curve al 50% e al 99% (fig. 4.11): 195 dB valgono 100 miglia al 99% e oltre 250 miglia al 50%; evanescenza ±7 dB oltre 100 miglia (−3,5 dB a 50 miglia), SSB 3 dB peggio del CW, +4 dB di riflessione dal suolo, +3 dB per l'accoppiamento di due antenne; guadagno in altezza (15 m: 4/3/2 dB a 10/50/100 miglia; 24 m: 8/6/4 dB); guadagno delle Yagi circa 10 volte il boom in λ (dBi); stazione da 50 W con antenna media: circa 180 dB, 100 miglia, +10 dB fino a 250 miglia. Inversioni all'alba e al tramonto fino a +20 dB e portata +20–50%; aria calda e secca sopra aria fresca umida sui Grandi Laghi fino a 1200 miglia, tipiche 400–600; regioni influenzate da grandi masse d'acqua le più favorite; tardo inverno con riscaldamento capace di inversioni forti. Verifica del nostro troposcatter: P.452 al 50% a 483 km e 144 MHz dà circa 204 dB, coerente con i 214 dB di Bray al 99%. Norton 1959 (J. Res. NBS 63D, gratis su NIST): perdita di base, di sistema e guadagno d'antenna di percorso; atmosfera di riferimento Ns = 301; raggio equivalente da Ns (250 → 4878 miglia, 301 → 5280, 400 → 6997); tabelle L(99) con antenne a 30 piedi (100 MHz: 163 dB a 100 miglia, 208 dB a 500 miglia; 1000 MHz: 138 e 188 dB con parabole da 28 piedi); variabilità a lungo termine V(p, θ) in funzione della distanza angolare (fig. 13). Rice, Longley e Norton, NBS TN 15 (1959), gratis su NIST. Barsis e Kirby, NBS Report 6751 (1961): 223 km in Colorado con il Pikes Peak (4292 m) come spigolo, 100 e 751 MHz; perdita coerente con lo spigolo arrotondato (raggio circa 11,5 km); evanescenza a lungo termine 11,7–14,3 dB contro 39–42 dB di uno scatter della stessa lunghezza; nessun ciclo diurno marcato; il modello a quattro raggi prevedeva 4–5 lobi ma se ne è misurato uno solo (massimo a circa 21 m dal suolo): i percorsi con ostacolo montuoso sono molto più stabili dello scatter, utile per i collegamenti "via Vesuvio o Faito". ARRL VHF Digital Handbook: pacchetto, APRS, D-STAR, WSJT (meteor scatter e EME), nessuna parte sulla tropo. Nuove sorgenti di dati provate o cercate: ECMWF open data (IFS 0,25°, livelli 1000, 925, 850, 700 hPa…, senza 975, 950, 900 e 800; GRIB2 con compressione CCSDS, che il nostro lettore non decodifica; già disponibile tramite Open-Meteo); DWD ICON-EU open data (GRIB2 compresso bz2, un file per variabile e livello: molte richieste); Canada GDPS su MSC Datamart e GeoMet (circa 15 km, fino a 33 livelli; GeoMet consente il ritaglio della zona; è la base delle mappe di Hepburn); archivio GFS NOAA su AWS dal febbraio 2021 (file globali, utile per la validazione); APRS PropView solo per la validazione.

**Undicesimo giro (21 novembre): atmosfera di riferimento, Longley-Rice, GEKO 69.** Bean e Thayer, "Models of the Atmospheric Radio Refractive Index", Proc. IRE maggio 1959 (pp. 740 e seguenti): il modello 4/3 ha la pendenza giusta nel primo km ma cala troppo più in alto; il 30% della curvatura di un raggio radente avviene nei primi 100 m e il 60% nel primo km; relazione media fra Ns e calo nel primo km: −ΔN = 7,32·e^(0,005577·Ns) (Ns = 313 → ΔN circa −42). Bean e Thayer, "C.R.P.L. Exponential Reference Atmosphere", J. Res. NBS 63D n. 3 (1959): N(h) = Ns·e^(−c·h) con c fissato da Ns tramite la stessa relazione; Ns medio USA 313; tabelle di tracciamento dei raggi (curvatura totale 15,2 mrad con il modello esponenziale contro 30,3 mrad con la Terra 4/3). MATLAB refractionexp: c = 0,143859 km⁻¹ per Ns = 313 (scala circa 6,95 km), valido sopra circa 1 km e 10 mrad. Uso per noi: il profilo "normale" di confronto può essere quello CRPL con l'Ns locale invece di M = 315 + 0,118·z fisso, e l'anomalia del calo nel primo km va misurata rispetto a −7,32·e^(0,005577·Ns) invece che rispetto a −40 fisso. NTIA Report 82-100 (Hufford, Longley, Kissick 1982), guida all'ITM in modalità area: climi e Ns suggeriti (equatoriale 360, continentale subtropicale 320, marittimo subtropicale 370, deserto 280, continentale temperato 301, marittimo temperato su terra 320, marittimo temperato su mare 350); Ns = N0·e^(−hs/9,46 km); si usa il minimo mensile di Ns perché meno influenzato da super-rifrazione e sub-rifrazione; Ns = 301 corrisponde a ae = 8497 km; sotto 100 km Ns conta poco. Rice, Longley e Norton, NBS TN 15 (1959), parte I: formula di previsione della distribuzione nel tempo per onda di terra e troposferica, base di TN 101. SoftWright, Wikipedia, Altair WinProp e MATLAB rfprop.LongleyRice: stesse informazioni (versione 1.2.2, 20 MHz–20 GHz o 40 GHz, 1–2000 km, Ns 301, sette climi, variabilità di tempo, luogo e situazione); nessuno modella i condotti, solo l'effetto statistico dei climi marittimi ("campi molto più alti per il 10% del tempo o meno"). GEKO Magazine n. 69 (IU3QEZ): orizzonte radio 4/3 di 130 km da 1000 m, 260 km sommando due stazioni; troposcatter con volume comune fra 1–3 km e 10 km; scatter da aerei a 10 km con +10–20 dB per decine di secondi o un paio di minuti; portate teoriche con 2×10 elementi per lato: SSB QRP circa 440 km, SSB 50 W circa 510 km, CW QRP circa 600 km, SSB 500 W circa 640 km (500 W in SSB rendono 40 km più di 5 W in CW); al 90–99% del tempo va tolto un margine di 10–20 dB; 5–10 dB per grado d'orizzonte sui tratti lunghi; la quota conta per liberare l'orizzonte, non per avvicinare il volume comune; Alpe Adria 2026: il 99% dei QSO via troposcatter, quasi nessuno via aerei nonostante AirScout.

## 12. Tavola rotonda: fonti per validare il modello o correggerlo (21 novembre)

Riepilogo di tutti i giri di fonti, ordinato per uso. "Stato" dice se la fonte è già nel programma (1.3.1), se è in prova o se è da fare.

### Dati per validare (che cosa è successo davvero)

| Fonte | Che cosa dà | Uso | Stato |
| Rapporti MMMonVHF passati dall'autore | 593 QSO a 144 MHz, 29/12/2021–2/1/2022, ora e locatori | validazione evento contro confronto (+14 giorni) | in uso: AUC 0,79 (1.2) → 0,90 (1.3.1) |
| QST "The World Above 50 MHz" | casi documentati (14–15/8/2022 Manitoba–Kansas, 144–10 GHz) | caso storico | in uso come caso storico; da aggiungere ai QSO di validazione |
| wspr.live (WSPR) | archivio completo, 2 m e 70 cm, SQL | molti eventi e molti "no" (ore senza spot) | provato, va filtrato (spot falsi a 16–20 mila km) |
| APRS PropView / APRS-IS | stazioni sentite, distanze, ore | solo validazione 144 MHz | da fare |
| Reverse Beacon (archivio) | spot giornalieri | poco VHF | scartato per ora |
| IGRA2 radiosondaggi (Brindisi 16320, Pratica di Mare 16245, Trapani 16429, Udine, Milano) | profili misurati 00 e 12 UTC | controllo del profilo GFS e delle inversioni | file trovati (Brindisi 46 MB), da usare |
| Barsis e Kirby 1961, Barsis 1963 | evanescenza misurata su percorsi con ostacolo montuoso | validare la categoria "monte" (Vesuvio, Faito) | da fare |
| Norton 1959, NBS TN 15, TN 101 | perdite L(99) e mediane tabellate | controllo dello scatter | in parte (verifica a 300 miglia coerente) |

### Correttivi suggeriti dalle fonti

| Correttivo | Fonti | Effetto misurato sulla validazione | Stato |
| Termine di stabilità di Misme | Bean e Dutton cap. 6 | AUC 0,80 → 0,90 insieme alla subsidenza | in 1.3.1 (tetto 16 dB senza condotto) |
| Subsidenza dal moto verticale | Moler e Holden 1960 | come sopra | in 1.3.1 |
| Condotto morbido (λ fino a 3·λmax) | Kerr, Hepburn, VHF/UHF DX Book, Shi 2019 | da solo +1% di AUC; porta i colori di condotto sul 46% delle tratte dell'evento e 0% del confronto | in 1.3.1 |
| Persistenza 24 h | VHF/UHF DX Book, Biancomano e Leyshon | AUC invariata, più presi e più falsi allarmi | in 1.3.1, da tarare |
| Profilo normale CRPL con Ns locale (−ΔN = 7,32·e^(0,005577·Ns)) al posto di 40 N/km fisso | Bean e Thayer 1959, NBS Monograph 4 | da provare | da fare |
| Soglia di super-rifrazione 79 N/km (ITU) invece di 100 (Bean e Dutton, strato 1000–850) | P.453, Lopez 2008, DX Book | da provare | da fare |
| Spessore climatologico (calcolo B) meno importante del deficit | Rogers 1997 | da provare: usare A per il guadagno | da fare |
| Equazione parabolica nel profilo M del GFS | PETOOL, wave-propagation (Lytaev), Levy, Lindquist | prima versione con campo oltre l'orizzonte dominato da rumore numerico (AUC 0,12–0,28): corretta (dominio 8 km, fascio 6°) | in prova |
| Evanescenza: ±7 dB oltre 100 miglia, margine 10–20 dB al 90–99% | Bray 1961, Antenna Book, GEKO 69 | soglie dei modi in condizioni marginali | da fare |
| Volume comune e distanza equivalente di Bray | IU3QEZ, Bray 1961 | informativo | in 1.3.1 |
| Troposcatter P.617 e RSGB come incertezza | TropoLink, DL2FZN | informativo | in 1.3.0 |

### Che cosa resta fuori dai modelli

Condotto sul mare a bassa quota (100–200 m) che i livelli GFS non vedono (studio delle fonti di dati in corso); riflessione e diffrazione sulle montagne vicine (Vesuvio, Faito): proposta di calcolo "via monte". ITM/Longley-Rice resta un riferimento statistico "giornata normale" (Ns 301, climi), senza condotti. Scartati per decisione dell'autore: scatter da aerei (AirScout, P.528), E sporadico (P.2001) e condotti a 50 MHz (servono EIRP che pochi hanno e si confondono con l'E sporadico).

### Proposta di ordine di lavoro

1. Validazione allargata: aggiungere WSPR filtrato (anche le ore senza spot) e il caso QST 2022; ripetere per 432 MHz.
2. Provare il profilo normale CRPL e la soglia ITU di 79 N/km, uno alla volta, tenendo solo ciò che alza l'AUC.
3. Confronto dei profili GFS con i radiosondaggi IGRA2 di Brindisi e Pratica di Mare nei giorni dei QSO.
4. Rivalutare l'equazione parabolica dopo la correzione numerica.

### Decisioni del 21 novembre (versione 1.3.2)

- Adottati: stima prudente (Bean e Dutton, 100 N/km) e aggressiva (ITU, 79 N/km) affiancate; condotti cercati solo sotto 2500 m (prima 1500 m, alzato dopo la validazione); bonus con alta pressione estesa e vento debole; peso del mare con il μ1 di P.452; quota delle stazioni rispetto al condotto anche su condotto parziale; strato limite sopra le creste (IT9GSF).
- Adottato come alternativa, spento per difetto: profilo normale CRPL con Ns locale.
- Equazione parabolica corretta numericamente: AUC da 0,12 a 0,54 (antenne a 30 m) e da 0,28 a 0,66 (300 m).
- Terreno: Copernicus DEM GLO-30 al posto dei riquadri Terrain Tiles «skadi», che vicino alla costa hanno rilievi fantasma di 80–100 m in mare (segnalazione di IC8TEM, verificata punto per punto a Capri); skadi resta come riserva.
- Segni meteo sulla mappa con il loro effetto: ↓ subsidenza favorevole (Moler e Holden 1960, rinforzi di 20 dB e oltre per ore negli anticicloni; +1,5 dB), ↑ aria in salita sfavorevole (affievolimenti profondi; −2 dB), S scirocco indizio favorevole (ARRL Handbook § 19.4, inversioni fortissime sul Mediterraneo; nessuna correzione). Simbolo raddoppiato, colori verde/rosso/arancio e fumetto sulla mappa.
- Adottato l'ITM 1.2.2 (NTIA/itm, pubblico dominio), tradotto in C#: secondo calcolo della linea di base in atmosfera normale accanto a P.452 e tratto finale dopo l'uscita da un condotto che finisce prima del corrispondente. Scartato il codice di qradiopredict perché sotto GPL v2.

Senza contenuto utile per l'indice: *Communications Quarterly*, autunno 1991 (nessun articolo sulla tropo; l'articolo di Atkins sullo scatter è nel numero dell'inverno 1991).

- Adottato come principio dei manuali (21 novembre): «la propagazione non può essere generalizzata, ma studiata in relazione alla morfologia locale» (DF5AI, riportato da IK3XTV in «Propagazione sui 2 metri»); citato nel manuale delle previsioni (§ 1) e nei consigli.
- Adottata (21 novembre, dopo la 1.3.3) l'interpretazione delle aperture transalpine pomeridiane di IK3XTV: non «celle convettive come lenti», ma troposcatter rinforzato dalla turbolenza alla cima dello strato rimescolato quando questa raggiunge la quota del volume comune (manuale delle previsioni, § 8 ter). Per ora criterio di lettura, senza correzione in dB. Scartate dagli stessi articoli: l'«auto-amplificazione» dei segnali oltre le Alpi (con la soglia di JT65 e 2 W si tollerano circa 225 dB, compatibili con diffrazione e troposcatter), la neve che «migliora la diffrazione» (più probabile lo strato stabile sopra la neve, con meno evanescenza).

## 13. Dagli articoli di IK3XTV

I due articoli di Flavio Egano IK3XTV (Radio Rivista: "Propagazione sui 2 metri" e "Propagazione VHF – celle convettive e onde gravitazionali") aggiungono due cose che i modelli di condotto non coprono: le aperture transalpine del pomeriggio dovute alla convezione, e regole pratiche su dove e quando si formano i condotti in Italia. Sono osservazioni da stazione, non misure: vanno trattate come ipotesi da verificare con gli spot.

| Osservazione dell'autore | Come diventa un ingrediente dell'indice |
| I condotti si formano soprattutto fra 100 e 1000 m, raramente più in alto; servono alta pressione su un'area vasta, buona insolazione e assenza di vento | cercare il condotto solo sotto 1500 m; bonus con alta pressione estesa e vento debole nei bassi strati |
| Nei mesi caldi condotto ricorrente sull'Adriatico fra Veneto e Puglia, 700–800 km, a volte fino alla Grecia; i percorsi su mare sono favoriti | peso maggiore alla frazione di percorso su mare (come il μ1 di ITU-R P.452) |
| I corrispondenti devono stare dentro o vicino al condotto; con antenne alte e condotto di pochi metri il segnale non entra | confrontare la quota della base del condotto con la quota delle due stazioni (servirà l'altitudine del QTH) |
| Stessa ora, stesso condotto: ascoltate dal Gargano una stazione in 144 e una in 50 MHz | da verificare: secondo la tabella di Hepburn i 50 MHz chiedono condotti oltre 300 m, quindi l'evento indicherebbe un condotto molto spesso |
| Collegamenti Veneto – Germania del sud oltre le Alpi, senza visuale, quasi sempre di pomeriggio e con alta pressione; segnali instabili che spariscono e ricompaiono dopo pochi minuti | indice "convettivo" separato dal condotto, per percorsi su montagna |
| Le celle convettive nascono al suolo e salgono di circa 1 km ogni 3 ore: alle 15 arrivano a circa 3 km, la quota che serve a scavalcare le Alpi | usare l'altezza dello strato limite (boundary\_layer\_height di Open-Meteo GFS) lungo il percorso: punteggio quando supera la quota delle creste |
| Le Alpi generano onde gravitazionali che, unite ai moti convettivi, creano irregolarità dell'indice di rifrazione | ipotesi: vento forte perpendicolare alla catena a 700 hPa come segnale di onde orografiche; da provare per ultimo |
| Inversione molto alta (fino a 8 km in casi eccezionali) dà circa 800 km, ma in pratica di rado oltre 300 km | la quota dell'inversione allunga la portata: dato utile per stimare la distanza attesa |
| D'estate raggio terrestre equivalente fino a circa 16000 km (2,5 volte quello reale), con circa 60 dB in meno di perdita oltre 300 km | coerente con k50 = 157/(157 − ΔN): k = 2,5 corrisponde a ΔN circa 94 N/km, cioè super-rifrazione |
| Oltre 3000 km solo nella fascia 20–30° a nord e a sud dell'equatore, sopra i mari, legati alle celle di Hadley; picco ad agosto nel settore Africa–Europa | fuori dall'Europa: utile per non dare indici alti sull'Atlantico senza riscontro, e per il percorso Canarie – Gran Bretagna |

Altri dati utili dagli stessi articoli:

- Portata ottica fra due antenne alte h1 e h2 metri (d in km): d = 4,1 · (√h1 + √h2), con aumento fino a circa il 20% per la rifrazione normale.
- Evanescenza: lenta e poco profonda con l'inversione; forte e rapida con lo scatter e con le celle convettive. Il programma potrebbe usarla per classificare gli spot se un giorno avrà i rapporti SNR nel tempo.
- Per l'E sporadico in 144 MHz l'autore riporta il lavoro di DF5AI e di Davis e Johnson (Nature 435, 2005) sul legame fra temporali e Es: fuori dalla tropo, ma un domani un indicatore separato potrebbe usare i dati dei fulmini.
- Riflessione al suolo nel doppio salto Es: mare K = 81 e 5 S/m, campagna e boschi K = 13 e 0,006 S/m, aree urbane K = 3 e 0,001 S/m.

## 14. Ipotesi di indice LIVETROPOVHF

Proposta di partenza, da discutere: un indice 0–10 sulla stessa scala di Hepburn, calcolato per ogni punto della griglia e ogni 3 ore da GFS (poi ECMWF e ICON per confronto), e un secondo numero per percorso che dice quanta parte del tragitto passa in un condotto utile alla banda scelta. I pesi qui sotto sono ipotesi mie, da tarare con gli spot.

Revisione dopo Bean e Dutton e Kerr: le soglie del profilo vengono dalla loro tabella 6.5 e la scala dalle loro misure, per cui uno strato rifrattivo vale 10–25 dB sopra il profilo normale. L'indice si calcola quindi prima in dB e poi si porta su 0–10; restano da tarare con gli spot i valori dentro quella forbice.

**Calcolo per punto, passo per passo**

1. Scarico T, umidità relativa, altezza geopotenziale e velocità verticale a 1000, 975, 950, 925, 900, 850, 800, 700, 600 e 500 hPa, più T e umidità a 2 m, pressione al suolo, altezza dello strato limite e vento a 10 m.
2. Scarto i livelli sotto il suolo; calcolo N e M a ogni livello (formule ITU-R P.453).
3. Calcolo −dN/dh in ognuno dei quattro strati di Bean e Dutton (1000–850, 850–700, 700–600, 600–500 hPa) e lo classifico come sub-rifrattivo, normale o super-rifrattivo con le soglie della loro tabella; dentro il primo strato uso anche i livelli intermedi per trovare il gradiente più forte e la sua quota.
4. Dove dM/dh < 0 trovo base e cima del condotto, lo spessore d e il deficit ΔM.
5. Confronto d con lo spessore minimo per la banda dalla formula di Kerr (circa 180 m per 144 MHz, 89 m per 432 MHz, come nella tabella di Hepburn); sotto la soglia il condotto conta ancora, ma meno.
6. Stimo il guadagno in dB con la tabella qui sotto (si prende lo strato migliore, non la somma degli strati, più il termine di anomalia) e lo converto: indice = dB / 2,5, fra 0 e 10, così 25 dB valgono 10. Un valore negativo (sub-rifrazione) si mostra come 0 ma resta nel dato del percorso.

| Situazione dello strato migliore | Criterio | Guadagno stimato |
| Normale (non stratificato) | −dN/dh dentro la fascia della tab. 6.5 (es. 20–60 N/km fra 1000 e 850 hPa) | 0 dB |
| Leggermente sopra il normale | fra il limite superiore della fascia normale e la soglia di super-rifrazione | da 0 a 10 dB, in proporzione |
| Super-rifrazione | oltre la soglia dello strato (100, 80, 70, 50 N/km salendo) ma sotto 157 N/km | da 10 a 15 dB, in proporzione fino a 157 N/km |
| Condotto troppo sottile per la banda | dM/dh < 0, spessore sotto la soglia di Kerr | 15 dB |
| Condotto utile per la banda | dM/dh < 0, spessore sopra la soglia di Kerr | 20 dB, +1 dB ogni 5 unità M di deficit, massimo 25 |
| Sub-rifrazione | −dN/dh < 0 nello strato più basso | −10 dB |
| Anomalia di fondo (si aggiunge) | Ns del modello meno Ns climatologico della mappa ITU per quel mese | 0,2 dB per unità N, fra −5 e +5 dB |

**Calcolo per percorso** (fra due locatori): campiono il cerchio massimo ogni 10 km, come già fa il programma per l'indice sul percorso, e calcolo la frazione di punti con condotto utile e la quota media della base. Lo studio CSIRO ha trovato utile proprio la frazione di tragitto in condotto (sui percorsi AIS a 162 MHz era il 67% più alta che nei casi di controllo).

**Aggiunte al calcolo per percorso** (da LA0BY e W3SZ): (1) quota delle stazioni sul mare confrontata con base e cima del condotto, perché un condotto elevato serve solo a chi ci entra; (2) un "pavimento" di troposcatter col metodo di Yeh, con gli angoli di partenza presi dal profilo del terreno; il guadagno tropo in dB si sottrae da quella perdita e il risultato si confronta con la perdita che la stazione riesce a superare (tabella di KK6MC). Così l'indice diventa anche una stima di distanza raggiungibile.

**Correzioni da provare una alla volta** (da Bean e Dutton e dai lavori di LA0BY, IK3XTV, G4MCU già nel manuale), da tenere solo se migliorano il confronto con gli spot:

- **Rimescolamento** (⚑ importante secondo l'autore, da osservazione quotidiana): di giorno su terra, fra metà mattina e pomeriggio con cielo sereno, o con vento al suolo oltre pochi m/s, i condotti al suolo spariscono; il loro contributo va ridotto. Su mare no.
- **Ora del giorno** (⚑ importante secondo l'autore, da osservazione quotidiana): i condotti da irraggiamento nascono al tramonto e cadono dopo l'alba; è un controllo sulla previsione a 3 ore.
- **Moto verticale**: aria che sale a 850–700 hPa rimescola gli strati (correzione negativa), aria che scende sotto un anticiclone li rinforza (positiva).
- **Tendenza della pressione**: pressione in salita sul percorso nelle ultime ore, segnale più alto e stabile; in calo, segnale più basso (bastano 2 hPa).
- **Sinottica**: alta pressione sopra 1025 hPa, temperatura a 850 hPa molto sopra quella al suolo, aria calda e secca che scorre su mare più freddo, percorso su mare.
- **Percorsi su montagna**: un secondo indice "convettivo" (sezione IK3XTV), con strato limite pomeridiano più alto delle creste lungo il percorso e alta pressione.

Dal VHF/UHF DX Book, altre correzioni da provare: **giorni consecutivi di alta pressione** sul punto (più giorni, più punti), **posizione rispetto al centro** dell'anticiclone (bonus ai bordi), **gradiente di pressione** al suolo come misura del vento, **pioggia forte nelle 24 ore precedenti** (penalità), **nubi basse al bordo dello strato** (penalità solo per le bande alte). Per la direzione dei percorsi: confrontare l'azimut del percorso con quello delle isobare.

**Verifica**

- Archivio di LIVETROPOVHF: ogni spot oltre 500 km a 144 o 432 MHz è un "evento". Si confronta l'indice sul percorso con la frequenza degli eventi, a parità di ora del giorno e mese (stesso approccio dello studio CSIRO).
- Confronto con Hepburn e F5LEN sugli stessi giorni: dove i tre indici discordano, si guarda chi aveva ragione secondo gli spot.
- Radiosondaggi delle 00 e 12 UTC: per controllare se il modello vede le inversioni che la sonda misura.
- Casi storici di LA0BY (6–11/11/2003, 10/12/2004, 15/10/2011, 11/2/2015, 22/11/2018): l'indice ricalcolato su quei giorni deve vedere i condotti che Hepburn non mostrava.
- Limiti da dichiarare nel manuale: condotti sotto 100–200 m quasi invisibili ai livelli di pressione GFS; previsione affidabile per 2–3 giorni, poi solo tendenza.

## 15. Fonti del 21 novembre (seconda parte)

- **ITU-R P.1812** (implementazione MATLAB/Octave di I. Stevanovic, OFCOM; GitHub eeveetza/p1812, Zenodo 10.5281/zenodo.6141558). Metodo punto-area VHF/UHF specifico della tratta, derivato da P.452: diffrazione «delta-Bullington», troposcatter, condotti e riflessione su strati, ingombri al suolo. Utile come terzo confronto accanto a P.452 nel calcolo punto-punto; i dati ΔN e N0 sono prodotti ITU integrali, quindi l'utente li scarica (come per P.453).
- **da Costa, Rabanal Ramirez, Dias, IET MAP 2018 (doi 10.1049/iet-map.2017.0992).** 563 MHz, 27 punti fra 5 e 30 km su terreno irregolare con foresta: P.526 a spigoli con 12 m di vegetazione −4,3 dB e σ 8,8 dB; P.1812 con 10 m di ingombro +2,6 dB e σ 8,9 dB; cuneo dielettrico −4,8 dB e σ 9,2 dB; P.1546 +5,0 dB e σ 11,7 dB. Per il programma: l'errore residuo di un buon metodo di diffrazione è circa 9 dB, e l'altezza degli ingombri sulle creste (10–12 m) conta.
- **PETOOL (Ozgun e altri, Computer Physics Communications 182, 2011).** Equazione parabolica split-step a una e due vie con propagatore ad angolo largo (angoli fino a 40–45°, contro 10–15° di quello stretto); fondo con perdite tramite la trasformata di Fourier mista; terreno a gradini. Conferma le scelte della 1.3.2 (angolo largo, coefficienti del suolo) e indica il calcolo a due vie per i rilievi ripidi.
- **MathWorks.** `tropopl`: perdita dei gas e perdita di «lente» in un'atmosfera a strati (P.835, P.676, P.453), senza condotti: con gradienti sotto −157 N/km il modello a ottica geometrica non vale. L'esempio di propagazione radar usa per la rifrazione −ΔN = 7,32·e^(0,005577·Ns), la stessa relazione CRPL già nel programma. `propagationModel` Longley-Rice: valori predefiniti polarizzazione orizzontale, conducibilità 0,005 S/m, permittività 15, rifrattività 301, clima continentale temperato. Nessun modello MathWorks tratta i condotti.
- **ITU-R, prodotti digitali del gruppo 3.** Dati e programmi di P.453, P.452, P.1812, P.2001, P.528, P.1546; i dati «integrali» sono parte delle raccomandazioni; nessuna licenza di ridistribuzione dichiarata: si continua a farli scaricare all'utente.
- **ESA Navipedia, ritardo troposferico.** N = Nidrostatica + Numida; il ritardo zenitale secco è circa 2,3 m e quello umido decine di cm, che varia in fretta. Per noi: il ritardo umido stimato dalle stazioni GNSS è una misura indiretta dell'umidità integrata, possibile fonte futura di now-casting; oggi non è usabile.
- **Bray, K2LMG, «A Method for Determining V.H.F. Station Capabilities», QST novembre 1961, pp. 36–40 (letto in originale).** Nomogrammi dalle curve di Norton (NBS 1959). Evanescenza entro l'ora di 13,5 dB oltre circa 100 miglia (7 dB sotto e 7 sopra la media oraria), quasi lineare sotto (3,5 dB a 50 miglia): per un collegamento solido si tolgono 7 dB. Correzione dell'orizzonte: ogni grado d'elevazione dell'orizzonte vale 69 miglia (111 km) di distanza in più (in meno se negativo). Guadagno di una yagi circa 10 × boom/λ (rapporto di potenza), più circa 4 dB di riflessione del suolo; SSB −3 dB e AM −7 dB rispetto al CW; perdita di tratta al 99% delle ore: circa 180 dB a 60 miglia, 190 a 90, 200 a 240 (2 m). Dalla 1.3.2 la probabilità per periodo usa σ = 3,4 dB (±7 dB come ±2σ) invece dei 5 dB stimati di seconda mano.
- **Calcolo a due vie (prova con il prototipo, 21 novembre).** Equazione parabolica ad angolo largo con terreno SRTM1 reale da Capri, 144 MHz, antenna 10 m dal suolo, atmosfera standard; misura: potenza media fra 20 e 400 m a 80 km sulla tratta verso il corrispondente. Dal centro di Capri e da Marina Grande l'ovest è chiuso (ostacolo a +11–21°, Monte Solaro): puntando il Faito (quasi alle spalle per EA5 ed EA6, 150–160° fuori asse) il segnale lanciato verso la Spagna sale di 10–29 dB; puntando il Vesuvio (110–125° fuori asse, quindi fuori dal piano del calcolo, solo indicativo) di 17–25 dB. Da Anacapri l'ovest è libero e il diretto vince di 8–25 dB. Coerente con i rapporti dell'autore del 1–2 gennaio 2022 (EA3 con antenna sul Vesuvio, EA5–EA6 sul Faito). Le perdite assolute su 1000–1300 km del prototipo, invece, non sono affidabili.
- **Barsis e Kirby, NBS Report 6751 (1961), Pikes Peak, 223 km, 100 e 751 MHz.** Il monte fa da terminale comune di due tratte in ottica: la distribuzione delle mediane orarie è la convoluzione di quelle delle due tratte. Escursione a lungo termine (1–99%) di circa 14 dB, contro 26–40 dB di un percorso in troposcatter di uguale lunghezza o angolo; nessuna variazione diurna apprezzabile. Il segnale è più forte del troposcatter ma più debole dello spigolo ideale: la cima va trattata come arrotondata (raggio circa 11,5 km). Segnali alti si osservano sempre dietro le dorsali.
- **Barsis, NBS Report 7652 (1963), tre percorsi oltre un monte (Colorado, Alaska, Europa), 750–900 MHz.** Perdite mediane 10–20 dB più alte del calcolo di Fresnel-Kirchhoff senza il terreno vicino ai terminali; l'evanescenza si stima convolvendo le due tratte in ottica; le riflessioni del suolo vicino ai terminali peggiorano le dissolvenze prolungate. Dalla 1.3.2 l'ingresso nel condotto aggiunge l'arrotondamento di P.526.
- **Barsis e Johnson, NBS TN 88 (1961).** Dissolvenze prolungate dell'onda spaziale, anche su un percorso oltre un monte (§ 5).
- **Norton, «System loss in radio wave propagation», J. Res. NBS 63D (1959).** Le curve da cui Bray ricava i nomogrammi; definizioni di perdita di sistema, di trasmissione e di base, guadagno d'antenna di tratta.
- **Rice, Longley, Norton e Barsis, NBS TN 101 (1965–67), volumi 1 e 2.** Base di ITM: spigolo singolo e ostacolo arrotondato con e senza riflessioni (§ 7, con l'esempio di Pikes Peak), variabilità a lungo termine V(0,5) e Y(q) per i climi (continentale temperato e altri). Per noi: il metodo dell'ostacolo arrotondato e le funzioni di variabilità sono il riferimento per tarare l'incertezza.
- **ITU-R P.526-16 (11/2025).** Formula (31) di J(ν), identica a quella del programma; ostacolo arrotondato (32)–(36), ora usato; metodi a più spigoli (Bullington e metodo completo § 4.5).
- **ITU-R P.1812-8 (2025) e -7.** Testo completo recuperato: metodo specifico della tratta da 30 MHz a 6 GHz (delta-Bullington, troposcatter, condotti e riflessione su strati, ingombri). La -7 avverte che il suo troposcatter (da P.617-5) sbaglia sotto i 66 km. Resta un possibile terzo confronto.
- **Paulus, «Practical application of the IREPS evaporation duct model», NOSC TR 966 (1984), e Radio Science 20 (1985).** Il rapporto DTIC non si scarica dalla rete automatica; il riassunto dice: il modello di Jeske dà altezze di 30–40 m irrealistiche in condizioni stabili (aria più calda del mare), perché le temperature misurate dalle navi sono spostate verso la stabilità; Paulus limita la differenza aria–mare a −1 °C. Il programma usa il profilo neutro, che non ha l'esagerazione stabile, e limita δ a 40 m: va nella stessa direzione. Le formule complete restano da controllare sull'articolo (Wiley, a pagamento).
- **Levy, «Parabolic Equation Methods for Electromagnetic Wave Propagation» (IEE 2000).** Su Google Libri c'è solo l'anteprima: non letto.
- **VK3FS (3fs.net.au).** Introduzione qualitativa: condotti elevati a 450–2000 m per le aperture più lunghe su terra; inversioni da irraggiamento di 300–600 m nelle notti serene e calme, che spariscono dopo l'alba; inversioni costiere più forti nel pomeriggio; 800–1300 km tipici, 1400–1600 km frequenti in condotto. Coerente con gli avvisi giorno/notte e con il soffitto di 2500 m.

## 16. Fonti

**Norme e formule**

- [ITU-R P.453-13 (2017), indice di rifrazione radio: formula e dati](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)
- [ITU-R P.452-18 (2023), previsione delle interferenze fra stazioni a terra sopra 100 MHz: raggio equivalente, β0, troposcatter, condotti](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)
- [ITU-R P.617-5, propagazione oltre l'orizzonte per ponti radio](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)
- [Shi et al., "A Comprehensive Study on Maximum Wavelength of Electromagnetic Propagation in Different Evaporation Ducts", IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

**Studi sulla previsione dei condotti con i modelli meteo**

- [P. Lopez, "A 5-year 40-km resolution global climatology of super-refraction for ground-based radar meteorology", ECMWF Tech. Memo 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO), "Tropospheric Ducting Prediction based on GFS Model Data at … the CSIRO Murchison Radio-astronomy Observatory", arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (FMI), "Effects of model resolution on predicting atmospheric ducting conditions with Harmonie-AROME", EGUsphere 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

**Previsioni radioamatoriali**

- [W. R. Hepburn, "The Hepburn Tropo Index (HTI)"](https://www.dxinfocentre.com/propagation/hti.htm) e [storia del metodo](https://www.dxinfocentre.com/propagation/history.htm)
- [W. R. Hepburn, "Lowest Usable Frequency vs. Thickness of Ducting Layer"](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, "Worldwide refractive index forecasts"](https://tropo.f5len.org/WW/info.php), [nuova versione 2026](https://tropo.f5len.org/?p=504), [note sul modello ARPEGE](https://tropo.f5len.org/author/f5len/)

**Dati**

- [Open-Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api), [nota sui livelli di pressione](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)
- [NOAA NCEI, Integrated Global Radiosonde Archive (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RAOBget, script per i radiosondaggi della University of Wyoming](https://github.com/NCAR/RAOBget)

**Testi classici** (indicati dall'autore del programma, da consultare per le formule definitive)

- B. R. Bean, E. J. Dutton, *Radio Meteorology*, NBS Monograph 92, 1966, 435 pagine (ristampa Dover 1968): profili verticali di N e M, gradiente di intrappolamento sotto −157 N/km, confronto con i radiosondaggi; capitolo 5 "Synoptic radio meteorology". Base per i passi 2–4 del calcolo per punto. **Gratis**: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [scheda NIST, doi 10.6028/NBS.MONO.92](https://www.nist.gov/node/1770911), [copia Internet Archive](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf).
- L. Boithias, *Radio Wave Propagation*, McGraw-Hill 1987 (trad. di D. Beeson dal francese, 330 pagine, ISBN 0070064334; anche North Oxford Academic): meccanismi oltre l'orizzonte, volume comune di scattering e perdita di accoppiamento antenna-mezzo. Solo cartaceo ([scheda di biblioteca](https://libcat.library.tamu.edu/Record/in00000014812/Description)).
- M. P. M. Hall, *Effects of the Troposphere on Radio Communication*, IEE Electromagnetic Waves Series n. 8, Peter Peregrinus 1979, 206 pagine (ISBN 0906048257; brossura 0863410863): situazioni sinottiche e propagazione VHF/UHF. Base per gli ingredienti sinottici. In commercio presso l'IET ([scheda](https://libcat.library.tamu.edu/Record/in00000636385)).
- H. G. Booker, W. E. Gordon, "A Theory of Radio Scattering in the Troposphere", *Proceedings of the IRE*, vol. 38, n. 4, aprile 1950, pp. 401–412: teoria dello scattering dalle disomogeneità dell'indice di rifrazione. Idea centrale: la potenza diffusa dipende dall'intensità e dalla scala delle fluttuazioni turbolente dell'indice di rifrazione (correlazione esponenziale); vicino all'orizzonte prevale la diffrazione, più lontano lo scattering; secondo gli autori la scala della turbolenza e le variazioni di indice attese dalla meteorologia bastano a spiegare i campi misurati. Citato da 328 articoli (Scilit), nessuna copia ad accesso libero. Scheda su Semantic Scholar (https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); testo completo su IEEE Xplore a pagamento ([doi 10.1109/JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)).
- C. M. Crain, J. R. Gerhardt (Electrical Engineering Research Laboratory, Università del Texas), "Measurements of the Parameters Involved in the Theory of Radio Scattering in the Troposphere", *Proceedings of the IRE*, vol. 40, n. 1, gennaio 1952, pp. 50–54, doi 10.1109/JRPROC.1952.273929: prime misure con un rifrattometro registratore delle fluttuazioni dell'indice di rifrazione e della temperatura, dal 1948, fra 25 cm e 15 m dal suolo; danno i valori reali che Booker e Gordon avevano solo stimato ([scheda Scilit](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)).
- "The Constants in the Equation for Atmospheric Refractive Index at Radio Frequencies", *Proceedings of the IRE*, vol. 41, n. 8, agosto 1953, pp. 1035–1037: propone di rivedere le costanti allora in uso (79 K/mb e 4800 K) nella formula di N; è da qui che vengono i valori 77,6 e 3,73 · 10^5 della formula ITU ([scheda Scilit](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)).
- E. C. S. Megaw, "Fundamental radio scatter propagation theory", *Proceedings of the IEE Part C: Monographs*, vol. 104, n. 6, settembre 1957, pp. 441–455, doi 10.1049/pi-c.1957.0052: spettro delle fluttuazioni di indice dalla teoria della turbolenza, trattazione completa dello scatter oltre l'orizzonte e dell'effetto della geometria del volume comune sul campo ricevuto; a pagamento presso l'IET ([scheda Scilit](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)).
- *Proceedings of the IRE*, ottobre 1955, numero speciale sulla propagazione per scatter (a cura di K. A. Norton e J. B. Wiesner), con Bullington, Staras e altri: **gratis** in PDF su [World Radio History](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf). Raccoglie gli articoli fondamentali del periodo in un solo fascicolo.
- D. E. Kerr (a cura di), *Propagation of Short Radio Waves*, MIT Radiation Laboratory Series vol. 13, McGraw-Hill 1951 (ristampe Dover 1965 e IEE 1987): teoria dei condotti di evaporazione e di superficie sul mare. Copia digitalizzata: [World Radio History](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); edizione IET in [IET Digital Library](https://shop.theiet.org/propag-short-radio-waves).

Misure recenti di diffusione troposferica in VHF: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Australia), "A Study of VHF Troposcatter Communication Channels in South Australia", in *Digital Signal Processing for Communication Systems*, Springer 1997, pp. 103–112 ([Springer](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)). Testo a pagamento; dall'abstract riguarda la diffusione, non i condotti. Utile solo se si vuole tarare il termine di troposcatter con dati di canale VHF.

**Modelli numerici e tratte su mare** (indicati dall'autore del programma)

- M. Levy, *Parabolic Equation Methods for Electromagnetic Wave Propagation*, IEE Electromagnetic Waves Series n. 45, 2000, 336 pagine (ISBN 0852967640): equazione parabolica per profili di rifrattività che cambiano lungo il percorso e per l'orografia; capitoli su propagazione troposferica, raggi e modi, propagazione su mare. Servirebbe per un calcolo di campo lungo il percorso, oltre l'indice. A pagamento, anche in forma elettronica dall'IET ([scheda IET](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)).
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (Università di Leicester): misure a 2 GHz sulle Isole del Canale e sul Canale della Manica. Molti lavori e tesi sono **gratis** nell'archivio dell'università: [statistiche a lungo termine sul condotto di evaporazione, Radio Science 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434), [variazioni di segnale su tre tratte di mare, Radio Science 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710), [tesi di N. Mufti 2012 con tratte di 50 e 140 km anche in VHF](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854), [tesi di I. Alam 2015 sulla rifrattività calcolata dai dati meteo, 240 MHz e 2 GHz](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959), [tesi di C. Y. D. Sim 2002 su VHF e UHF su mare](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925).
- R. A. Paulus, "Practical application of an evaporation duct model", **Radio Science** vol. 20 n. 4, 1985, p. 887 (non IEEE Transactions on Antennas and Propagation), doi 10.1029/RS020i004p00887 ([scheda](https://zendy.io/title/10.1029/rs020i004p00887)): altezza del condotto di evaporazione dai dati meteo di superficie. Gratis il rapporto tecnico che lo precede: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf).

T. Lindquist, *Wave Propagation Models in the Troposphere for Long-Range UHF/SHF Radio Connections*, tesi di laurea magistrale, Università di Karlstad, 2 giugno 2020, 106 pagine (fornita dall'autore del programma). È una guida passo passo, gratuita, per scrivere un calcolo con l'equazione parabolica: soluzione split-step (anche ad angolo largo), condizioni al contorno per terreno e mare, terreno a gradini, tracciamento dei raggi su profili lineari a tratti, con verifiche numeriche; cita lo strumento libero PETOOL per MATLAB. Due dati utili subito: (1) la sensibilità di N alle variabili meteo, δN ≈ 0,26 δP + 4,3 δe − 1,4 δT (hPa e K, intorno a 20 °C), cioè un errore di 1 hPa sulla pressione di vapore pesa tre volte un errore di 1 °C sulla temperatura: è l'umidità del modello che va controllata per prima; (2) altezza media del condotto di evaporazione: 5–6 m nel Mare del Nord, 13–14 m nel Mediterraneo, oltre 20 m nel Golfo Persico, sempre troppo bassa per 144 e 432 MHz. Conclusione dell'autore (frequenze da 1 a 10 GHz): oltre l'orizzonte i collegamenti affidabili richiedono quasi sempre un condotto.

**Software di propagazione sul profilo del terreno** (per orizzonte e ostacoli, non per la tropo prevista)

- [Radio Mobile](https://ve2dbe.com/rme.html) di Roger Coudé VE2DBE: gratuito, Windows (anche Linux e Mac con Wine), modello Longley-Rice/ITM, profilo del terreno estratto in automatico dai dati di quota ([download](https://ve2dbe.com/download/download.html)).
- [SPLAT!](https://qsl.net/kd2bd/splat.html) di John Magliacane KD2BD: GPL, 20 MHz–20 GHz, modelli Longley-Rice e ITWOM v3.0, rapporti di ostacolo e perdita in testo; ultima versione 1.4.2 del 2014.
- [ITM, codice ufficiale NTIA/ITS](https://its.ntia.gov/software/itm): il modello Longley-Rice (20 MHz–20 GHz) con sorgente e documentazione, base di Radio Mobile e SPLAT!. Il codice è libero e si potrebbe aggiungere al calcolo sul percorso di LIVETROPOVHF (perdita dovuta al terreno, da sommare all'indice tropo). Rapporto originale: A. G. Longley, P. L. Rice, "Prediction of Tropospheric Radio Transmission Loss Over Irregular Terrain: A Computer Method – 1968", ESSA Technical Report ERL 79-ITS 67, luglio 1968 (scheda NTIA sul software storico: https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITU-R, software e dati digitali](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): mappe gratuite di ΔN e N0 mediani annui su griglia 1,5° usate da P.452 e P.617 — utili come valori climatologici di riferimento per l'indice.
- Pathloss (Contract Telecommunication Engineering): programma commerciale per ponti radio; non ho trovato una fonte pubblica che confermi quali raccomandazioni ITU implementa, quindi da verificare.

**Codice su GitHub riusabile in LIVETROPOVHF**

| Progetto | Che cosa fa | Linguaggio | Uso possibile per noi |
| [NTIA/itm](https://github.com/NTIA/itm) | modello Longley-Rice ufficiale dell'NTIA, con pacchetti per .NET | C++, C# | perdita dovuta al terreno sul percorso: si integra direttamente nel programma, che è in C# |
| [fuzzzeee/itm](https://github.com/fuzzzeee/itm) | traduzione in C# del codice C++ di ITM | C# | alternativa senza librerie native, utile anche per la versione Windows 7 |
| [eeveetza/Py452](https://github.com/eeveetza/py452) | ITU-R P.452, identica alla versione MATLAB/Octave di riferimento approvata da ITU-R WP 3M; versione MATLAB pubblicata dall'Ufficio federale svizzero delle comunicazioni | Python, MATLAB | riferimento per tradurre in C# le formule di troposcatter e condotto, con gli esempi di validazione ITU |
| [bwinkel/pycraf](https://github.com/bwinkel/pycraf) | P.452 completa, P.676, profili da SRTM; GPL v3, ma i file dati ITU inclusi non sono liberi per uso commerciale | Python | controllo dei nostri risultati |
| [ITU-Rpy](https://github.com/inigodelportillo/ITU-Rpy) | molte raccomandazioni ITU-R, fra cui P.453-13, validate con gli esempi ITU | Python | verifica della formula di N e delle mappe climatologiche di P.453 |
| [NCAR/RAOBget](https://github.com/NCAR/RAOBget) | scarica i radiosondaggi della University of Wyoming in formato testo | Python | schema per scaricare in C# i profili delle stazioni italiane |

Nota: la versione in vigore di P.453 è la [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), scaricabile gratis; nel documento ho usato la 13, da riallineare.

Già nei manuali del programma e riusabili qui: LA0BY (Nordic VUSHF 2019), IK3XTV ("Propagazione sui 2 metri" e "Propagazione VHF – celle convettive e onde gravitazionali", Radio Rivista, forniti dall'autore del programma), DF5AI/DK5YA (2003), G3YGF (QST 1983), The VHF/UHF DX Book cap. 2, G4MCU, KP4MD.
