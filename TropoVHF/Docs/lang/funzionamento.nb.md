LIVETROPOVHF · Teknisk dokument 1
# Operasjon av programmet

> Automatisk oversettelse av italiensk: i tilfelle tvil er verdt den italienske guiden.

Hva programmet gjør, hvorfra det tar dataene og hvordan det gjør dem til det du ser på kartet og på listen. Versjon av 9. november 2026, relatert til prosjektet i C# (.NET 10 + Avalonia).

## Sammendrag

- Formål: tropo prognose (Hepburn [3], F5LEN [4], isobar GFS [18]) og reell visning av band i samme vindu, i henhold til LA0BY [1] kontrolllisten.
- Datainnsamling: to DX-hoper, Reverse Beacon Network [9], dm4x-hoper og PSKReporter [5] MQTT-strøm, UDP dekoding fra WSJT-X, JTDX og MSHV [8].
- Spot: bandsegmenter, negativt filter, airscatter i dashed gul, posisjon ved lokatorer eller C ALL3.TXT [6] [7].
- Hvem lytter: mørke rosa sirkler med rosa linjer alltid synlig mot stasjonene følte [5].
- Spotkort: avstand, azimut, tropoindeks på banen og troposcatter tap med G3YGF [2] -metoden.
- Språk Hepburn og F5LEN: [3] bilde og kalibrering benk [4].
- Se: NTP-kontroll på 10 servere hvert tredje minutt [10] [11].
- Mistenkte rapporter: EME i Q65/JT65 anerkjent med Månens posisjon, avkortet lokatorer, skimmere, karantene og lyttere, med regler lært av sol [20].
- Kriterier for kassering og betydning av fargene som er oppsummert i to tabeller (seksjon 11 ter og 11 kvater).
- Temaer, 36 [17] -språk, eksportere dokumenter til Word med redigerbare formeler [16], bærbare program og multiplattform [12] [13] [14] [15].

## 1. Formål

Programmet setter sammen i ett enkelt vindu to ting som vanligvis ser på forskjellige steder: prediksjonen av tropofer utbredelse (Tropo-indeksen til Hepburn og F5LEN, isobar) og observasjonen av hva som virkelig skjer på band (spot fra DX-hopen, Reverse Beacon Network og PSKReporter, pluss listen over stasjoner som lytter). Referansebåndet er 144 MHz; reklamene samles fra 50 MHz opp.

Innstillingen følger kontrolllisten som foreslås av LA0BY for ekstreme tropo hendelser: i de foregående dagene du ser på Hepburn og F5LEN prognoser og temperaturprofiler i høyden, mens hendelsen er på gang du søker på DX-hopen og på punktkartene bevis på at åpningen starter [1]. LA0BY bemerker også at de eneste Hepburn/ F5LEN -kartene ikke er nok til å forutsi de lengste tilkoblingene [1]

## 2. Vinduet og fanene

Vinduet er ett og ikke delt i mobile vinduer. Fra topp til bunn:

- Generelle innstillinger rad: Ring og Locator til venstre; til høyre UTC-klokken (f.eks. 08/11/2026 20:33:38 UTC, ravtekst) etterfulgt av NTP:OK eller NTP: FAILED, temamenyen og språket.
- Antall faner i fem grupper: Kart og sted; Forecasts (Propria β, prognosen beregnet av programmet og beskrevet i Forecast Manual, deretter Hepburn og F5LEN); GRAVES; Data og kilder (DX-hopen, PSKReporter, Reverse Beacon, UDP, Avtaler og mistenkelige rapporter); Veiledninger (Program Operasjon, Kartbygging, Hvordan forme tropoen, Manuell prognose, Tips til operatøren, med en søkeboks som teller der teksten vises). Under bjelken viser en strek gruppens underspråk; hver gruppe husker det siste som ble brukt.
- innhold i fanen og nederst i statuslinjen med tilstanden til kildene, databasen med navn og tidspunktet for siste oppdatering.

De øverste linjeinnstillingene gjelder for alle faner: det valgte temaet også farger datainnsamling og bakgrunn av dokumenter; Dokumentsider forblir så hvite som et ark, så de skriver ut godt.

## 3. Ring og utleier

Maidenhead utleieren kan ha 4 eller 6 tegn (f.eks. JN70 eller JN70CN). Programmet konverterer det til koordinatene til midten av firkanten: felt på 20° × 10°, kvadrat på 2° × 1st, subkvadrat på 5′ × 2,5′. Hvis teksten ikke er en gyldig lokator, blir kanten av boksen rød og stasjonen ikke vises på kartet. Locatoren tjener også som senter for Azimut-projeksjonen og som utgangspunkt for avstander og azimut i annonsebrettet.

Kallet er navnet som brukes som en klynge pålogging. Ved første start, til det er en gyldig nominasjon, kobler klyngene ikke til: et vindu ber om navnet (og eventuelt utleieren); bekrefter, navnet er kopiert som et brukernavn i innloggingsfeltet til hver klynge som har tom og klynger forlate. Med \"Senere\" forblir fortsatt, og Connect-knappen i hver klynge krever nominasjonen igjen. MQTT, UDP og NTP bruker ikke innlogging og forlater uansett.

## 4. Datainnsamling: kilder

Hver kilde har sin egen del i kategorien Datainnsamling, med tilkoblet/disconnected, de anerkjente og tilkoblede boksene ved oppstart, status, meter og register over de mottatte radene. Unbanded eller kassert linjer fra filteret vises ikke i registeret: bare telle delen meter. Seksjonene er side om side med to til to.

| Kilde | Tilkobling | Hva bringer |
| 1. DX klyngevalg | på0dxk.dyndns.org:8000 kommandosett/filter dxbm/pass 6 | Spot av amatørradioer, også SSB og CW. På ON0DXK filtreres VHF og aktiveres med den angitte kommandoen (poi pass 4, 2...). |
| 2. Andre kilde | dxmaps.com:7300 (DXMAPS, EA6VQ) og s50clx.si:41112 ( S50CLX, Slovenia) | DXMAPS samler inn VHF-flekker og gir også RBN skimmer flekker (sett/skimmer kontroll, allerede satt); S50CLX er en DX Spider-hop. Logg inn med ditt navn; redigerbare adresser. |
| 3. Reverse Beacon Network | Telnet.reversebeacon.net 7000 CW/RTTY · 7001 FT8 | Automatisk skimmer sted. Logg inn med navnet bare [9]. |
| 4. Cluster med PSKReporter | dm4x.ddns.net:8500 | Digitale steder av PSKReporter i klyngeformat. |
| 5. PSKReporter MQTT | mqtt.pskreporter.info 1883 · 1884 med TLS | Hver sanntidsmottaksrapport: som sender, som lytter og deres [5] Locators. Band 144, 222 og 432 MHz. |
| 6. Vær og tropo kart | dxinfocenter.com/tropo_eur.html tropo.f5len.org/forecasts-for-European Modell GFS | Hepburn [3] og F5LEN [4] tropo indekskart, havnivåtrykk for isobar (GFS via Open-Meteo [18]). Hepburn og F5LEN må ha lov til forfatterne. |

Cluster telnet (kilder 1–4). Etter tilkoblingen venter programmet på navneforespørselen (tekst som inneholder pålogging, anrop:, anrop eller samtalen); hvis det ikke kommer innen 4 sekunder, vennligst send påloggingen. Send deretter kommandoene i seksjonen, adskilt fra punkt og komma, én hvert 0,6 sekund. Telnet kontroll tegnene fjernes før du leser linjene. Hvis tilkoblingen faller og Riconnetti er aktiv, prøv igjen etter 10 sekunder (15 hvis serveren ikke reagerer i det hele tatt) [6].

PSKReporter MQTT (kilde 5). Programmet registrerer seg i emnet pskr/filter/v2/{banda}/{modo}/{tx_call}/{rx_call}/{tx_grid}/{rx_grid}/{tx_dxcc}/{rx_dxcc} ved å fikse bare bandet og forlate de andre feltene gratis med tegn + [5]. Det nøyaktige navnet som serveren indikerer bandene (f.eks. 2m, 1,25m, 70cm) må kontrolleres ved den første tilkoblingen.

## 5. Fra klyngelinjen til stedet

En linje er et sted når den har formen DX de SPOTTER: frekvens DX-kommentar HHMZ. Programmet stammer spotter, DX stasjon, frekvens i kHz, kommentar og nå UTC; navnene tar av skimmer suffixes (-#, -1...). I denne rekkefølgen gjelder følgende regler [6].

| Band | Segment akseptert |
| GRAVES (med 144) | 143.040 – 143.060 MHz |
| 144 MHz | 144 – 148 MHz |
| 222 MHz | 220 – 225 MHz (bare Region 2 ITU, Amerikas forente stater) |
| 432 MHz | 420 – 450 MHz |
| 1296 MHz | 1240 – 1300 MHz |
| 2320 MHz | 2300 – 2450 MHz |
| 10 GHz | 10,5 GHz |

1. Band. Frekvensen må falle i et av de styrede segmentene; alle resten, og spesielt hver kommersiell under 49 990,0 kHz, er ikke vist og teller som \"uten band\".
2. Hvordan. En av de mest kjente måtene å kommentere på er FT8, FT4, Q65, JT65, ISCAT, CW, SSB, FM, RTTY, PSK31, MSK144, FSK441, JT6M; USB og LSB blir SSB.
3. Negativt filter. Spotet kastes hvis det inneholder modusene MSK144, FSK441, JT6M, JTMS, ISCAT eller taggene <AUE> <ES> <MS> <FAI> < F2 >, med ikke-følsomme store bokstaver og til og med mellomrom innenfor parenteser. Dette er måter og former som ikke er tropo: Scatter meteor, aurora, og sporadisk, F2.
4. Airscatter. Hvis kommentaren inneholder <AS> eller <ACS> (også blandet, for eksempel <As>) eller erklærer punktet som airscatter (luftscatter, airscatter, flyspread, flyspread), forblir punktet i gult med perforert linje (mørk bølge for å holde seg lesbar på det klare kartet) og fremhevet på listen.
5. Plassering av stasjoner. Hvis det i kommentaren er to locators (f.eks. JN54 < JN83) den første er av dem som gjør spot og den andre av stasjonen DX; med en locator, det er fra DX stasjon. Det som mangler søkes i C ALL3. TXT, bare med det nøyaktige navnet (for bærbare datamaskiner se nedenfor). Hvis en stasjon forblir uten posisjon, vises punktet på listen med noten \"ukjend posisjon\", men ikke på kartet.
6. Dobbelt. En kommersiell med den samme DX, samme spotter, samme band og samme kilde til en allerede tilstedeværende erstatter den i stedet for å legge til.

C ALL3.TXT er navnedatabasen → locator som brukes av JTDX, i formatet CALL, LOCATORE,,,. Prosjektfilen inneholder 1 048 847 navn, alle med gyldig locator; headeren på filen snakker fortsatt om 34 323 navn fordi det forble den i den første utgaven [7]. Den er lastet i bakgrunnen ved oppstart, så vinduet er umiddelbart brukbart. Filen søkes ved siden av programmet, i datamappen, i mappen hvorfra programmet ble startet og en eller to mapper ovenfor; Hvis det ikke er noe, forteller et vindu det når du starter og statuslinjen signalerer det i rødt. Navnet bruker alltid 6-tegn. Bare regel for lokatorene: hvis den mottatte (spot, MQTT, dekoding, QSO desunti) har mindre enn 6 tegn og C ALL3. TXT har en til 6, det er alltid verdt C ALL3.TXT; for bærbare datamaskiner (F/ IC8TEM, IC8TEM/P) er bare en nøyaktig stemme: uten, stasjonen forblir uten en leiemann og er ikke designet andre steder. Reglene gjelder også på tidspunktet for tegningen, så dataene som ankom før databasen ble lastet vil kjøre seg selv.

En database: C ALL3.TXT. Fra Bloomingville alle lokatorer er i en fil, C ALL3.TXT, som forblir i formatet av JTDX og WSJT-X (CALL, LOCATORE,,,,,) og kan brukes som det er også i WSJT-X, JTDX og MSHV. Det er ikke flere filer fra hverandre. På toppen av filen, etter overskriften, er det to blokker merket med en kommentarlinje: // [LIVETROPOVHF-MANUALI] med oppføringene satt inn for hånd og // [LIVETROPOVHF-CLUSTER] med rettelser fra klyngen; deretter // [LIVETROPOVHF-FONTI] med alle andre elementer i alfabetisk rekkefølge. Hver nominasjon vises én gang. I de sublingual Data and Sources › Avtaler (C ALL3) du leter etter et navn, skriver du leieren og trykker Legg til / endre: stemmen går i blokken av stemmene for hånd og det er verdt fremfor alt, selv over lokatoren som selve stasjonen sender. ∫ Fjerner nominasjonen fra C ALL3.TXT (den neste oppdateringen gjenopptar det fra kilder, hvis de har det). Legg til en fil... les en liste CALL, LOCATOR (du godtar også punkt og komma, tabulering og mellomrom) og skriv den blant håndplukkede stemmer; den valgte filen er ikke kopiert. Last på nytt C ALL3.TXT fra disken, etter å ha redigert den med en redaktør. Full rekkefølge av kilder: egen lokalisering, håndstemmer, klyngekorrigeringer, resten av C ALL3. TXT (kilder, lærde live lokatorer og G7VRD), mottatt lokator (som gjelder over databasen bare hvis den har 6 tegn og ikke slutter med LL eller MM).

Oppdatering av C ALL3.TXT. På samme underspråk lastes Update-knappen nå ned kildene (C ALL3.TXT av WSJT-X Forbedret, C ALL3.TXT EME av W7GJ, grid_data.bin av JTDX), slår seg sammen med dem (den første som har en nominasjonsvinn), sjekk at de er minst 50 000 navn (andre endringer), sparer C ALL3. TXT Tidligere C ALL3. TXT-poster forblir, og en 6-tegner-locator forblir hvor en kilde har bare 4 av samme firkant. Hånd stemmer og rettelser fra klyngen forblir på toppen, uendret. Med oppdateringsboksen på egen hånd hver dag, skjer sjekken i starten. Programmet skriver om C ALL3. TXT umiddelbart etter hver stemme for hånd og for lærde lokatorer, på det meste hvert tiende minutt og ved stengingen; C ALL3. TXT i pakken er skrivebeskyttet og attributten tar av seg selv. Linux installatører overstyrer ikke en C ALL3. TXT allerede tilstede; det for Windows installerer det bare hvis det mangler. ALL3

Automatiske rettelser fra klyngen. I DM4X og RBN automatiske flekker kommentaren bringer to lokatorer, for eksempel KN09tp<> KN09: den første er hva stasjonen som flekker erklærer seg, til 6 tegn; den andre er den av stasjonen følte, ofte bare til 4. Hvis den første har 6 tegn (og ikke slutter med LL eller MM) og er forskjellig fra den i databasen, korrigerer programmet C ALL3. TXT alene: Korreksjonen går i blokken // [LIVETROPOVHF-CLUSTER], med datoen for den siste bekreftelsen i det tredje feltet, er den gyldig over kildene (under håndstemmene) og vises mellom mistenkelige signaler oppdatert som "C ALL3 ". Så det overlever C ALL3. TXT-oppdateringer. En ubekreftet rettelse i to år mister forrang og forblir som en normal stemme.

Siste kilde: G7VRD. Hvis en nominering ikke har en 6-tegn i C ALL3. TXT, programmet spør det i bakgrunnen til det offentlige API av G7VRD (api.g7vrd.co.uk, " lokalisering ved callsign") som samler lokatorer automatisk fra nettverkstrafikk. Du aksepterer bare en 6-tegner som ikke er midten av plassen (LL eller MM); forespørselene er gruppert på maksimalt 40 nominativer i minuttet, og hver nominativ spør en gang om dagen, fordi det er en hobbyistisk tjeneste uten garantier. Locator funnet går inn i C ALL3. TXT hvis navnet ikke er der eller bare har 4 tegn på samme firkant; søket slår av med boksen i navnet sublingual.

Avtaler med baren. I PSKReporter MQTT-emner, skiller linjen nivåene, så kommer forbindelsesnavnene med et punkt i stedet for streken ( IV3. HB9CAT). Programmet legger linjen tilbake og hvis meldingen inneholder alle navnene, bruk disse: IV3/HB9CAT forblir IV3/HB9CAT på kart, liste og database.

Locatorer lærte levende. C ALL3. TXT inneholder ikke alle: for eksempel SP9EMF og SP9NVY er ikke der, og deretter forblir 4-tegner locator sendt av PSKReporter (kortet sier det). For å redusere disse tilfellene skriver programmet i C ALL3. TXT hver 6-tegn som kommer fra enhver kilde (PSKReporter, som sender eller som mottaker, klynge, UDP dekoding) for en nominasjon som C ALL3. TXT har ikke eller har bare 4 tegn i samme firkant; endrer aldri en stemme til 6 tegn. Fram til Kineret var disse lokalisatorene i Data/ LOC6.TXT.

Overgang fra Tarsus. Ved første begynnelse av Copalia filer fra tidligere versjoner (Dati/C ALL3 -MANUALE.TXT, Data/C ALL3 -AGGIUNTE, Data/C ALL3 -CLUSTER. TXT, Data/ LOC6.TXT og Data/C ALL3 - G7VRD.TXT) slås sammen til C ALL3. TXT med de samme postene. Avleser skriving, gamle filer tar.fuso utvidelsen, du ikke lenger lese og du kan slette.

Automatiske mottakere. RBN, dm4x og PSKReporter rapporterer ofte navn med et suffiks som indikerer mottakeren: -SDR, /SDR, -# (skimmer RBN) eller -1, -2... (pluss mottakere av samme stasjon). For utleieren er suffikset fjernet: OK2OZL -SDR tar OK2OZL JN89UE fra C ALL3.TXT hvis stasjonen ikke allerede sender en 6-tegns locator. Navnet som vises forblir den med suffikset.

Locatorer kuttet. Ikke alle kilder behandler utvidelsen av utleieren deres: mange satt den til 4 tegn, og noen automatiske overføringssystemer krysser bevisst 4-tegns lokatoren, selv de som får data fra den reelle lytte av FT8, FT4, Q65 og JT65, eller fullfører det med sentrum av torget (LL eller MM, f.eks. JN89MM). Av denne grunn blir en 4-tegner eller 6-tegner som slutter med LL eller MM, erstattet av C ALL3. TXT på samme plass. Hvis riktig plassering og midten av plassen ikke sammenfaller med et par dusin km, feilen forblir liten i forhold til tropo avstander.

## 6. Hvem lytter

Hver MQTT-melding fra PSKReporter rapporterer navnet og lokatoren til de som mottar [5], så du trenger ikke å se etter den i C ALL3.TXT. For bandet som er valgt i velgeren holder programmet, for hver mottakerstasjon: utleier, siste mottak, dekodede måter, antall stasjoner filt og maksimal avstand. En stasjon forlater listen når den ikke lenger mottar noe i det valgte området (3, 5, 10, 15, 30 eller 60 minutter).

På kartet er hver mottaker en mørk rosa tom sirkel, og fra hver start fulle rosa linjer mot stasjonene som føles og sender tilbake til nettet. Linjene er alltid sett, uten å klikke. Avtaler av lyttestasjonene, de som hører og de to hodene på hvert sted er alltid skrevet på kartet, uten å måtte klikke (det samme navnet på det samme punktet er skrevet en gang bare). Ved å klikke på en sirkel blir linjene enda tykkere og åpner fanen med detaljer. Hvis lokator fra MQTT har mindre enn 6 tegn, bruker programmet det til C ALL3.TXT (ca. én million navn), forutsatt at det er i samsvar med det mottatte kvadratet.

Spretter meteor. Som med klyngeflekker blir MQTT-forholdene i MSK144, FSK441, JT6M, JTMS eller ISCAT bare kassert og talt; det samme gjelder UDP-dekodinger på disse måter.

Ikke ekte lyttere. Noen stasjoner erklærer å motta i 144 MHz, men i virkeligheten er mottakeren på et annet band (feil frekvens indikasjon): deres rapporter vil designe eksisterende åpninger på tusenvis av km. Programmet kontrollerer det selv. Hver stasjon som en lytter sier å høre i VHF følges i 30 minutter på alle band, med en dedikert MQTT inskripsjon (pskr/filter/v2/+/{nominativ}/#) [5]. tjener som bevis. Hvis minst to av stasjonene som høres av en lytter, i samme minutter (±10), er aktive på andre band, anses lytteren ikke ekte og alle dens VHF-rapporter kastes; MQTT-registeret rapporterer navnet og stasjonene som forrådte ham. I listen over flekker, ved siden av målerne, viser MQTT-knappen strømmens tilstand (grønt blad hvis den er koblet til, rødt eller ikke) og hvor mange lyttere som ble kastet i bandet og utvalgt; ved å klikke på den åpner den fanen med listen over kasseringer og for hver stasjon som ble funnet på andre bånd.

Lang forbindelse Kina. En link over 1000 km (Clumming Spot, MQTT Lytting, QSO Desunti) vises ikke umiddelbart: forblir holdt, og telleren \"holdt > 1000 km\" sier nummeret. Det er avslørt når en annen forbindelse, mellom forskjellige stasjoner og minst halvparten langs, bekrefter det med ett av disse kriterier: samme stasjon i et hode (for eksempel den samme lytter) og den andre stasjonen i samme retning innen 10°; lignende korridor, med begge hoder innen 200 km fra den andre forbindelse; parallelle rute, med retning innen 10° og sideavstand innen 200 km, overlappet langs ruten. Både dukker opp og MQTT-loggen viser grunnen. Dens UDP dekodinger er alltid synlige og gjelder som bekreftelse for andre. De beholdte forbindelser forblir i minnet i 60 minutter.

Ingenting under 144 MHz. Programmet fungerer fra 144 MHz opp: alt under 143 MHz kastes. Unntaket er GRAVES, den franske radar som sender i CW på 143 050 MHz ved Broye-lès-Pesmes ( JN27SI): en kommersiell mellom 143 040 og 143 060 MHz eller med GRAVES i navnet, vises mellom 144 MHz med radar som sender. Ad-boardet sier om mottakeren er inne i strålen (sektorene D, C, B, A fra 90° til 270° azimut sett fra radaren, 6 trinn fra 7,5° hver) eller ut; fliken GRAVES, som i YA-MS, viser himmelen sett fra radaren: sektorer, trinn belyst på den tiden (sequenza PE1ITR 2021: 0,8 s per trinn, syklus 4,8 s Data og geometri kommer fra YA-MS. Føle det i tropen er vanskelig, men det kan skje.

Rehabilitering fra menneskelige klynger En stasjon som også vises på andre band, teller ikke som mistenkt hvis DX menneskelige klynger ( ON0DXK, GB7RAU; ikke dm4x, RBN eller andre automatiske kilder) den aktive skaden på det aktuelle bandet i flekker laget av andre navn: det er tilfellet med stasjoner som virkelig fungerer på flere band sammen. Således returnerer en kassert lytter synlig (MQTT-registeret skriver “rehabilitert”) og en langholdt link for lave bånd kan avdekkes.

stasjoner under 144 MHz. I karantene til lange forbindelser bekrefter ikke en stasjon som i de samme minutter høres på bånd under 144 MHz (fra MQTT-rapporter fra andre bånd) andre forbindelser og ikke avdekkes, selv om en lignende eller parallell rute ville bekrefte den.

## 7. Liste over reklamer

Det har 35% høyre for kategorien Kart og sted. Topp: tittel, bandvelger (50, 70, 144, 222, 432, 1296, 2320 MHz, 10 GHz), områdevelger (siste 3, 5, 10, 15, 30, 60 minutter) og oppdateringsnøkkel. Nedenfor, fire meter: synlige steder, AS/ACS, kastet fra filteret, stasjoner lytte.

Hver linje viser typen linje, tiden UTC, DX → DE, veien, avstanden i km og, på den andre linjen, kilde, frekvens og kommentar. Den siste er på toppen. Ett klikk på linjen velger stedet: på kartet vokser linjen og de to navnene vises.

## 8. Spotkort og troposcatter tap

For det valgte stedet viser fanen lokatorer, avstand, azimut, frekvens, modus, kilde, nå, kommentar, gjennomsnittlig tropoindeks langs banen og et estimat av rutetap for spredt tropo.

Avstand og azimut beregnes på den maksimale sirkel med jordradius på 6371 km (formula av emisenoverso for avstand, initial azimut fra DX-stasjonen til spotteren).

Taper troposcatter. Det grunnleggende tapet, for to stasjoner på glatt grunn, er summen av gratis romtap og formidlingstap: G3YGF [2]

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

Tapene er i dB, D er avstanden i km, f frekvensen i MHz, θs diffusjonsvinkel i grader.

8497 km er jordens tilsvarende radius (4/3 av den fysiske). θA og θB er avhengig av horisonten til hver stasjon:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

med Δh høyden på hindringen som gjør fra horisonten om stasjonen og dens avstand. Hvert nivå av horisont koster ca. 10 dB [2]. Programmet kjenner ikke stasjonenes horisont, slik at det viser det grunnleggende tapet med θA = θB = 0 og erklærer det -smooth jord- G3YGF legger til et klimabegrep, hvor N er reaktiviteten til [2] -jorden; det vil bli brukt når N er tilgjengelig fra værmodellen:

```math
L_{N} = 0,2 \cdot (N - 310)
```

Verifisering: for 110 km til 144 MHz formelen gir 166,5 dB, mot ca 168 dB senger på diagrammet til artikkelen. For 500 km til 144 MHz gir ca 206 dB. Det forventede signal/støyforhold er systemets kapasitet (eirp + effektiv følsomhet av mottakeren) minus tapet: med G3YGF 144 MHz-modellsystemet (198 dB) vil en 500 km bane gi ca. 8 dB i SSB [2], det vil si forbindelsen er bare med bedre utbredelse av den rene scatter.

## 9. Mine dekodninger: UDP fra WSJT-X, JTDX og MSHV

WSJT-X, JTDX og MSHV kan sende UDP hver linje dekoder. Programmet lytter på 57.0.0.1:2237 og gjentar den samme pakken, byte per byte, på 127.0.1:2238, så de andre programmene som brukte den porten (GridTracker, JTALERT, en logger) fortsetter å fungere hvis du flytter dem på 2238. Adresser og dører endres i UDP sublingual of the Data Collection; som en lytteadresse kan du også bruke en flercast gruppe (f.eks. 224.0.0.1), hvis dekodingsprogrammet er satt slik.

Pakker følger WSJT-X-nettverksprotokollen: et initialt magisk nummer 0xADBCCBDA, skjemanummer og meldingstype. Programmet bruker hovedsakelig tre [8] -typer:

- Heartbeat: sier hvilket program som sender dataene og med hvilken versjon.
- Status: frekvens av ring og modus; fra frekvensen du får bandet (med samme tabell av flekkene).
- Dekode: Nå, SNR, tid og frekvensavfall, måte og tekst av dekodet melding.

Fra teksten du får stasjonen du hørte: i CQ 9 A2PQR JN83 er stasjonen etter CQ (besparende DX, EU, TEST...) i IK0JKL IT9MNO JM68 er den andre, dvs. som overfører. Locatoren er den av meldingen, hvis det er (4 tegn, forskjellig fra RR73); ellers du søker i C ALL3. TXT. De lytter linjer: stasjoner som mottakeren din hører nå. På kartet blir de røde linjer fra QTH til stasjonen, med navnet; laget lyser opp med dekodet boksen fra meg og krever en gyldig lokator. I listen til høyre forteller en teller hvor mange stasjoner du dekoder på det valgte bandet.

QSO desunti dal live. Når en dekoding er en lukkende melding i formen C ALL1 C ALL2 RR73 (eller 73 eller RRR), for eksempel IC8TEM IK3FHP RR73 eller IK3FKP SV8PEX 73, har de to stasjonene fullført en lenke. Programmet registrerer det som QSO, med lokatoren til hver tatt fra den siste meldingen som han erklærte det eller, om ikke, fra C ALL3.TXT [7], og trekker det på kartet som en grønn linje mellom de to stasjonene (stret \"QSO desunti dal live\"). Et par stasjoner teller en gang per band; bandet og rekkevidden valgt i listen er gyldig.

På samme underspråk viser feilsøkingsruten teksten til hver mottatt pakke (type, tid, verdier og melding), på forespørsel også heksadesimale bytes, og en linje for videresending. Det er å sjekke at dekodingsprogrammet faktisk sender dataene.

## 9 bis. Språk Hepburn og F5LEN

Hver viser hva programmet tar fra kilden. Knappen “Les siden og last ned alle bilder” åpner den angitte siden (basert på dxinfocenter.com/tropo_eur.html for Hepburn [3] og tropo.f5len.org/forecasts-for-europee for F5LEN [4]) og laster ned alle cache bildene ved siden av kjørbar; du kan også lime inn adressen til ett bilde. For hvert bilde er det en boks å ekskludere den og et felt med prognosertid. De 31 europeiske bildene av F5LEN er navngitt på tidspunktet for modellløpet (03.png... 48.png hver tredje time, 54... 96 hver sjette time, 108... 192 hver 12. time) og programmet tar tid fra navnet på [4] -filen; for Hepburn er det verdt rekkefølgen på siden. I F5LEN -fanen velger en meny regionen: Europa (tropo.f5len.org/forcasts-for-europee), Australia (tropo.f5len.org/Australia, vk03.png... vk192.png) eller Atlanterhavet mellom Kapp Verde og Irland (tropo.f5len.org/atlantic-ocean/atlantic-d4-ei, atl03.png... atl192.png). Alle har de samme skrittene.

Nedenfor er kalibreringsdisken. Ett klikk på bildet legger til et kontrollpunkt (lokalisering eller lat,lon: du trenger minst 6) eller en legendefarge med sin verdi. Programmet beregner pikselen som matcher → koordinater med en andregrads polynomiell interpolasjon på de minste [19] -firkanter, leser fargene med den valgte toleransen og, for F5LEN, konverterer enhetene N i 0-10-skalaen med en · N + b. \"Spara kalibrering og prøv å lese\" skriver filen i Tarature-mappen og sjekk hvilken piksel locatoren faller. Før du konfigurerer en kalibrering, bør det be om tillatelse til forfatteren av kartene.

Regioner. Hver kilde har flere regioner, hver med sine faste navn bilder: Jeg beklager. Fanen til hver kilde viser først av alle forhåndsvisningene av fusjonen: alle nedlastede bilder satt sammen som på hovedkartet, med den perforerte ruten og navnet på hver region, et grått slør der ingen bilder dekker området og en glidebryter for å rulle timene; til venstre listen over regioner, hver med boksen å bruke det, antall kart som leses og hvor ytterpunktene kommer (OCR, for hånd eller fabrikk; hvis OCR mislykkes teksten han leser vises). Den manuelle kalibreringsbenken forblir bak den homonyme knappen. Menyen Bench Region velger hvilket utseende som ser ut, boksen \"Bruk denne regionen på kartet\" inkluderer det i indeksen. Rammen er funnet alene (to horisontale linjer og to vertikale linjer med samme farge); å plassere bildet tjener ytterpunktene i rammen: for Hepburn Europa (10° W–40° E, 27°–56° N) og F5LEN Europa (15° W–40° E, 30°–65° N) allerede er i programmet, for de andre regionene de skriver i de fire O E S N-feltene som leser aksen etiketter. Skala multiplikator gjør de to kildene sammenlignbare: sluttverdi = multiplikator × legendeverdi. [3] F5LEN [4] F5LEN

## 9 ter. Eksporter i Word

De to dokumentene har på toppen av “Eksporter til Word”-knappen. Filen er skrevet i Office Open XML-formatet; formlene blir Word-likninger (OMML), slik at de forblir redigerbare med [16] ligningsredigering. Hvis lagrevinduet ikke er tilgjengelig, slutter filen i dokumentmappen ved siden av kjørbar.

## 9 quater. Hvem kaller meg

Hvis en melding sendes til deg på den lokale UDP (fornavnet er ditt, for eksempel IC8FAX IK3FHP JN55 eller IC8FAX IK3FHP -12), sporer programmet fra QTH til stasjonen som kaller deg en svart prikket og blinkende linje, med navnet og lokatoren ved siden av deg. Locator kommer fra C ALL3.TXT (inkludert håndstemmer) eller melding. Linjen forblir til UDP passerer nedleggelsen av QSO med den stasjonen ( RR73, RRR eller 73 i en av de to retningene), i det meste 3 minutter fra det siste samtalen; du trekker over alt og ikke følger utelukkelseskriterier, så et direkte anrop aldri unnslipper.

PSKReporter, via MQTT, sender ikke teksten til meldingene: sier bare hvem som har mottatt hvem som, med frekvens, vei og SNR. Dette er grunnen til at en direkte samtale kun er anerkjent av sine egne UDP dekodinger.

## 10. Tidskontroll (NTP)

Digital modus og spot tid krever en nøyaktig klokke. Hvert 3 minutter programmet forhører 10 NTP-servere (SNTP på UDP 123): 0.pool.ntp.org, 1.pool.ntp.org, 2.pool.ntp.org, 3.pool.ntp.org, it.pool.ntp.org, ntp1.inrim.it, ntp2.inrim.it, ptbtime1.ptb.de, time.google.com, time.cloudflare.com [11].

For hvert svar beregner kassering og forsinkelse av retur, kaster de unormale verdier og tar medianen, med beregning av kassering og forsinkelse som tilveiebrakt av SNTP [10]. Med minst 3 svar er enige i UTC-klokken til programmet justerer seg og neste vises NTP: OK i grønn; hvis svarene ikke er nok vises NTP: FAILED i rødt og klokken forblir datamaskinens. Systemets klokke er ikke rørt, fordi administratorens tillatelser ville være nødvendig; alle interne programplaner (spots, 5–60 minutters vinduer, dekodinger) bruker justert tid. Kontrollen slår alltid i bakgrunnen: i grensesnittet du ser bare resultatet ved siden av klokken; servere, intervaller og minste responsendring i LIVETROPOVHF.ini (ntp_servere, ntp_minutes, ntp_min).

## 10 bis. Generelle utelukkelseskriterier

To bokser i laglinjen gjelder alt du tegner (spot, PSKReporter, dekoding, QSO desunti):

- Utelukk under 100 km. Trafikken mellom stasjonene mindre enn 100 km kan ikke være betydelig for tropo (grunnbølge, optisk område): med den on-box du ikke tegne og ikke vises i listelisten. Begynnelsen er av.
- Over 1000 km: bare kvadrater. Rundt din QTH (Locator satt til å starte) det er en krone på 1000 km i alle retninger. Hvis bare én av de to stasjonene i en kobling er utenfor denne kronen, er forbindelsen ikke tegnet som en linje: i det sted de farger i lilac, prikket på 45°, lokaliseringsfirkantene (4 tegn) av stasjonene involvert. Det samme gjelder PSKReporter stasjoner som lytter til over 1000 km, som vises som et kvadrat i stedet for som en sirkel. Så langt trafikk du ser uten å fylle kartet over linjer. Uten en gyldig locator teller lengden på forbindelsen. Kvadraten på QTH-farger aldri i lilac, selv når du lytter til en stasjon utenfor kronen: er den angitte lokatoren, bruker ethvert navn som opererer fra stasjonen. Kroneradiusen er valgt ved siden av boksen: 1000, 700 eller 500 km. Boksen Corone 1000/700/500 km trekker de tre kronene rundt QTH, tegnet i lilac, med det mest markante valget og avstanden skrevet nordover. I listen over steder blir avstanden fulgt av.. Begynningen er på. Kriteriet gjelder etter karantene: lange forbindelser som fortsatt holdes, vises ikke engang som firkanter.

Hva er lilac firkanter perforert på 45°. Trafikken bort fra din QTH interesserer deg å forstå hvor utbredelsen er åpen, men dens linjer, hundrevis av kilometer lang og ofte tiere, dekker kartet og skjule det som teller for deg: åpningene du kan utnytte fra stasjonen. Av denne grunn, utover den valgte krone, trekker programmet ikke linjene, men farger lokatorens kvadrater (2° × 1°) av stasjonene som er involvert. Resultatet er et kart over \"aktive soner\": hvor mange nærliggende lilac firkanter vises, er det en kanal eller en åpning, selv om det ikke bekymrer deg direkte; Hvis området utvider seg mot deg, er det signalet å peke antennen i den retningen. 45° dashboard lar deg se under tropoindeksen og rutenettet, så sammenlign med et blikk prognosen (bunnfarge) og den virkelige trafikken (illus). Din QTH-plass farger aldri, fordi det er utgangspunktet og ikke et fjernt område. Bare firkantene som er alle utenfor kronen er farget: en firkant krysset av kronen, eller inne, forblir fri selv om det er en av de to stasjonene. For å se linjene, bare slå av boksen eller velg en større krone.

## 11. Temaer og språk

Temaer: de 18 felles for IC8TEM -programmer, det samme som YA-NEC og E-LAYER ANALYZER: Klar, Natt, Svart, åtte pastell (blå, oransje, aquamarine, grønn, rosa, lilac, indigo, brun), Scia meteorica (YA-MS palett), tre av IP-TRX-CTRL (Chiaro, Notte, Nero) og Blå, lilla, grønne. Fra Bloomingville ser Chiaro ut som YA-NEC: lys grå bakgrunn, grenset hvite paneler, blå natt faner og blå aksent. I mørke temaer blir også grunnkartet mørkt. Fargeskalaen på prognosen er den samme i alle temaer. På smale skjermer (under 1500 piksler, for eksempel 1366×768) viser klokken øverst kun timen, så tema, markør av tekster og språk forblir synlig. Språk: den 28. wikien av YA-MS [17] (engelsk, Reise, Deutsch, Nederlands, Français, Español, Português, Dansk, Norsk, Svenska, Islenska, Eesti, Lietuvi, Latviešu, Руский, кра,нська, Türkaalربيا Med arabisk, hebraisk og urdu hele vinduspeilene fra høyre til venstre; kartet forblir som [12]. Oversettelser vedrører grensesnittet; de to tekniske dokumentene og meldingene i statuslinjen forblir på italiensk. Dagenes navn kommer fra de internasjonale innstillingene. NET til det valgte språket.

Tema og språkendring fra de to menyene øverst til høyre, ved siden av klokken, og gjelder umiddelbart, uten å starte programmet på nytt og uten å avbryte innsamling av data. Valget er lagret i LIVETROPOVHF.ini.

## 11 bis. mistenkelige og selvlærende rapporter

Fanen mistenkte rapporter samler alt programmets kontroller finner merkelige, gruppert etter type og navn, med de siste 24 timene. Programmet lærer av seg selv: for hvert par (type, navn) teller de positive resultatene (+) og tvert imot (−), og etter 3 enige utfall skaper en automatisk regel; et motsatt resultat setter det i spørsmål. Brukerbeslutninger (gode, fattige) alltid gjelder; pulsante-knappen returnerer beslutningen til programmet. Reglene er i Data/APRENDIMENTO. TXT, ved siden av programmet; de automatiske ikke forsterkes i et år er glemt.

| Type | Når | Utgang + / − | Regeleffekt |
| EME ( Q65/JT65) | Q65 eller JT65 over 1000 km med Månen over horisonten til begge hoder, eller EME i kommentar | Hver gang månen er vanlig | Kast: EME stasjon, dens lange Q65/JT65 s er også utelukket uten vanlig måne. God: behandlet som tropo |
| Lang tilkobling | Over 1000 km i karantene | + avslørt, − utløpt uten bekreftelse | Godt: lengdene på stasjonen viser umiddelbart. Forkast: aldri |
| Lytter | PSKReporter mottaker som hører aktive stasjoner på andre band | − kassert, + rehabilitert av menneskelige klynger | Bra: aldri kastet. Kast: alltid |
| Ulike utleiere | Square fikk forskjellig fra C ALL3. TXT | + Hver gang du gjentar det | Bra: stasjonen flyttet, det er verdt torget mottatt. Forkast: vale C ALL3.TXT |
| Square Center | 6-tegn med LL eller MM |  —  | Kun informasjon |
| Automatisk mottaker | Avtale med -SDR, /SDR, -# eller -n |  —  | Kun informasjon |

EME. Q65 og JT65 er måten å kommunisere på via Luna: I teorien faller de inn i utelukkelsesklausulen som scatteren meteor, men de brukes også i tropo. Programmet beregner månes posisjon med de lave presisjonsformlene til Almanac-astronomiske (ca. 0,3°) [20]: Hvis en forbindelse i Q65 eller JT65 overstiger 1000 km og Månen er over horisonten til begge hodene, er det nesten sikkert EME og er ekskludert (det teller blant de utelukkede). Hvis Månen ikke er vanlig, er forbindelsen mulig i tropen og passerer gjennom karantene av lange forbindelser, men signalet forblir synlig. Det gjelder klynger, MQTT og dens UDP dekodinger.

## 11 ter. Kontakt kasseringskriterier, én etter én

Programmet ønsker å vise bare den tropoferiske utbredelsen i 144 MHz og videre. Hver innkommende data (Cluster Row, PSKReporter Report via MQTT, UDP dekoding) passerer gjennom en rekke filtre, alltid i samme rekkefølge. En kassert data forsvinner ikke i ingenting: telles (kontakt med den utestengte i den øvre baren) og når grunnen er tvilsom, vises i fanen Suspenderte rapporter.

|  N.  | Kriterier | Hvor er den? | Hva som er slettet | Kan du endre deg? |
|  1  | Band | cluster, MQTT, UDP | Alt under 143 MHz (også 50 og 70 MHz), unntatt GRAVES 143 050 MHz. Bandene som holdes er 144, 222, 432, 1296, 2320 MHz og 10 GHz | Nei. |
|  2  | Satellitt QO-100 | cluster, MQTT, UDP | 2400,0-2400,5 MHz og 10489,5-10490 MHz: det er geostationær satellitttrafikk, ikke tropo | Nei. |
|  3  | Meteor-spread | cluster, MQTT, UDP | JT6M og ISCAT (også ISCAT-A/B), kommentarer med "&" og ikke-tropoksy utbredelsesmerker (<AU>, <AUE>, <ES>, <FAI>, < F2 >) alltid. MSK144, FSK441 og JTMS (også med <MS>) bare over 250 km: innen 250 km aksepter vi, fordi på den distansen disse modusene også brukes i tropo | Nei. |
|  4  | EME (via Luna) | cluster, MQTT, UDP | EME skrevet i kommentaren; Q65 eller JT65 over 1000 km når Månen er over horisonten til begge hodene; stasjoner lært som EME | Ja: God regel i mistenkelige signaler |
|  5  | Ikke-ekte lytter | MQ | PSKReporter mottaker som sier å høre på 144 stasjoner aktive på den tiden på andre band (minst to): Hans relasjoner kasseres. Tilbake bra hvis DX menneskelige klynger bekrefter de samme stasjonene | Ja: God/fattig |
|  6  | Kvarantin av lange forbindelser | alle linjer | Over 1000 km en kobling forblir skjult inntil den finner en bekreftelse: samme stasjon i samme retning, en annen rute i en korridor på 200 km eller parallell rute innen 10° og 200 km langs minst halvparten. Uten bekreftelse utløper etter 60 minutter | Ja: Godt / Av til stasjon |
|  7  | Under 100 km | linjer og liste | Med esken, de korteste tilkoblingene på 100 km (grunnbølge, optisk rekkevidde) | Boks, av ved oppstart |
|  8  | Utenfor kronen fra QTH | linjer | Hvis en stasjon er over 1000, 700 eller 500 km (på ditt valg) fra QTH, ingen linje: bare lilla torg dekket av de to stasjonene. Din QTH-plass aldri farge | Boks og radius, på 1000 km |
|  9  | Tidsvindu | alt | Eldre flekker av det valgte området (fra 3 til 60 minutter) | Meny øverst |
|  10  | Dobbel | klynge | Det samme punktet (same DX, samme spotter, samme band, samme kilde) gjentas | Nei. |

Legg merke til kriterier 4 og 6. En forbindelse i Q65 eller JT65 over 1000 km uten vanlig måne er mulig i tropo: er ikke kassert ved kriteriet 4, men passerer for karantene ifølge kriterium 6 som en annen lang forbindelse. Direkte samtaler til deg (seksjon 9 kvater) er det eneste unntaket fra kriterier 7 og 8: de trekker alltid.

Hva er ikke et avfall. Rettelser av lokatoren (C ALL3. TXT med håndstemmer, rettelser fra klyngen og lærde lokatorer, suffiks -SDR, sentrum av den firkantede LL / MM) endring der du trekker en stasjon, ikke kast den. En stasjon uten utleier, derimot, kan ikke tegnes: forblir på listen med avstanden «—».

## 11 kvater. Farger og symboler

Hver farge har bare én betydning, lik i alle temaer (endre bare bakgrunnen).

| Farge og tegn | Betydning |
| Kontinuerlig blå linje | Spot fra DX-hopen, RBN eller DM4X mellom to stasjoner |
| Gul linje med mørk kant | Spot med airscatter kommentar (refleksjon på fly) |
| Mørk rosa tom sirkel | Lyttende stasjon på PSKReporter (MQTT) |
| Tynn rosa linjer fra sirkelen, med en rosa ball nederst | Stationer som lytteren hørte, med navnet alltid skrevet; tykkere linjer for den valgte lytteren |
| Rød linje og rødt navn | Dine UDP dekodinger (WSJT-X, JTDX, MSHV): Hvem er du? |
| Rød linje blinkende slag | QSO i gang med denne stasjonen: Han ringte deg og stengingen er ikke kommet ennå, eller er DX Call av WSJT-X/JTDX med aktivert overføring. Fortsett med RR73, RRR eller 73 |
| Grønn linje | QSO desunti dal live: stenger RR73, RRR eller 73 UDP-visning |
| Svart linje med hvit grense | Noen ringer deg: det varer opp til RR73/RRR /73 eller opp til 3 minutter |
| Kvadrater på 45° | Trafikk med minst én stasjon utover den valgte kronen: Ingen linjer, bare firkanter locator |
| Søk etter Lillas med avstand til nord | 1000, 700 og 500 km rundt din QTH; det valget er mer merket |
| Sirkel og punkt i fargen på temaet | Din QTH, med navn og utleier |
| Orange ball med GRAVES 143 050 | Radaren GRAVES ( JN27SI) |
| Prøve fra blå til rød | Hepburn tropeindeks (0,0) eller F5LEN klasse, eller deres gjennomsnittlige; varmere farge, bedre forhold |
| Lys grå fløyel | Område uten tropodata for den valgte tiden |
| Grå-blå linjer med verdien i hPa | Isoberere hver 4 hPa; H i blått høyt trykk, L i rødt lavt trykk |
| Røde, blå og lilla prikkede linjer | Isothermia til 850 hPa hver 4 ° C: rød over null, blå under null, lilla null |
| Prøve oransje | Termisk reversering: luft til 925 hPa varmere enn på bakken, mer intens med sterkere inversjon |

I den mistenkte rapporterer kanten av hver fane sier regelen i kraft: grønn God, rød Scarta, grå ingen regel.

## 12. Innstillinger og portabilitet

Vindu. Ved starten opptar vinduet 90% av skjermens nyttige plass (skjære programlinjen), sentrert, så på en 15-16 tommers notatblokk med en 125% eller 150% størrelse på tittellinjen forblir synlig. Hvis det nyttige området er under 1280 × 720 punkter, vindudelen maksimalt. Størrelse, posisjon og maksimal status er lagret i LIVETROPOVHF. ii ved stengingen og enden i neste start, forutsatt at tittellinjen fortsatt faller i en skjerm (for eksempel etter å ha fjernet en ekstern skjerm); Ellers returnerer du til 90%. Minimumsstørrelsen er 900 × 560.

Programmet er bærbart: aldri skriver i AppData-mappen eller i registeret. Alt er i mappen til den kjørbare: LIVETROPOVHF.ini (Innstillinger, opprettet ved den første lukkingen), C ALL3.TXT (med håndskrevne stemmer, lærde rettelser og lokatorer), kart cache og værdata, regler lært (Dati/APRENDIMENTO.TXT) og LIVETROPOVHF.db arkiv. Du kan også holde på en USB-pinne så lenge mappen kan skrives.

Ad arkiv (SQLite [14]). Hvert sted akseptert og hver PSKReporter-lytterapport lagres i LIVETROPOVHF.db, en SQLite-database i én fil. Til minne er det bare de siste 60 minuttene som forblir til listen og kartet; arkivet beholder alt og tjener til historikeren og statistikken (også UDP-dekodingene), for eksempel åpningene for band og per dag, maksimal avstand, kvadratene nådd, og den bakre sammenligning mellom den forventede indeks og de virkelige flekker. Strimlene som kastes fra filteret regnes, men arkiverer ikke.

## 12 bis. Automatisk installasjon og oppdateringer

Fra versjon 1.0 programmet allerede lastes ned fra Utgivelsessiden av github.com/ic8tem/LIVETROPOVHF, i de samme pakkene av YA-MS: installasjonsprogram for Windows (64 bit, 32 bit og ARM, installer i C:\LIVETROPOVHF uten administratorrettigheter) og bærbar zip; for Windows 7 SP1 og 8.1 pakker fra hverandre (-vinduer7-x64 og -window.deb,.rpm, Arch, Slackware, AppImage og tar.gz for Linux; pakker for Raspberry Pi (64 og 32 bits) og Alpine;.dmg og apps.zip for Mac (Apple Silicon og Intel). Med Linux-pakker kopieres programmet til første start i ~/LIVETROPOVHF, så innstillinger og data forblir ved siden av programmet; på Mac data er født ved siden av LIVETROPOVHF.app.

Hvert 30. minutt (det første sjekke et par sekunder etter starten, umiddelbart hvis siste gang programmet hadde stengt for en feil) programmet leser alene, uten å be om noe, den siste utgivelsen. Hvis versjonen er nyere laste ned den bærbare pakken som er egnet for systemet og erstatter programfilene på stedet: de som er i bruk endres om *.old-update og slettes ved neste omstart. Innstillinger, arkiv, datamappe og C ALL3. TXT berører ikke. Den nye versjonen er aktiv fra neste start: ved installasjon fullført et vindu foreslår å starte om umiddelbart; for akutte rettelser, merket [obligatorisk] i frigivelse, kan omstart ikke utsettes. Kontrollstatus og versjon i bruk er skrevet nederst i statuslinjen; å slå av oppdateringer=0 i LIVETROPOVHF.ini. På Mac-programmet er appen signert og endrer seg ikke: programmet bare rapporterer at det er en ny versjon.

LIVETROPOVHF-bildet med versjonsnummeret vises i starten; til slutt ber programmet om bekreftelse og takk for bruk.

## 12 ter. Feillogg og statuslinje

Enhver uforutsett feil slutter i Data/errori.log, selv med beregningsloggen av: dato og klokkeslett, versjon, system, åpen fane, fullstendig feil og siste linjer av hva programmet gjorde. En grensesnittfeil lukker ikke lenger programmet: i statuslinjen vises " anno annotert feil" (klikk: åpne registeret). I neste start spør programmet om å sende rapporten: Åpner nettleseren på et GitHub signal som allerede er kompilert, med nominasjon, lokator og brukernavn til PC fjernet, som du sjekker og sender med en gratis GitHub-konto. Loggen slutter også med hver oppdateringssjekk.

I statuslinjen blir kildeballene (DX klynge, RBN, PSKReporter, MQTT, UDP) klikket og brakt til kilden; tegneserien sier hvorfor en kilde er av: avvist port, klyngeknutepunkt stasjonert på serversiden, ingen respons eller UDP lytte, men uten pakker fra WSJT-X, JTDX eller MSHV. Ett klikk på versjonsnummer sjekk umiddelbart oppdateringer.

## 13. Plattformene

Eksterne bookcases: Avalonia for [12] -grensesnittet, MQTTnet som MQTT-klient for sanntidsstrøm av PSKReporter [13], Microsoft. Data.Sqlite for [14] arkiv.

C# på.NET 10 (LTS) [15] med Avalonia-grensesnitt, publisert som en uavhengig kjørbar for Windows 64 og 32 biter, Mac Intel og Apple Silicon, Linux Debian og Raspberry Pi (64 og 32 biter). Noted Limits: Windows 10/11; macOS 14 eller nyere, med apper til å signere eller for å åpne til høyre → Åpne; Bringebær fra Pi 2 / Zero 2 W opp (ARMv6-modeller støttes ikke av.NET).

## Takk
Takket være I5MZY Massimo Zannoni for testing på Windows 7 og Windows 8.

Airtights: ITU-R P.453, digitale produkter fra Studiegruppe 3 (© ITU), som brukes til personlige og ikke-kommersielle formål.

## Hovedkilder

1. S. Heck LA0BY, «Ekstreme Tropo-utbredelse på 144 MHz og oppover», presentasjon på det 41. nordiske VUSHF-møtet, Skjeberg 2019. Troposferiske måter, hendelseskontrollliste (Hepburn/ F5LEN -prognose, høytemperaturprofiler med Windy, DX-hop som åpningstest), observasjon som bare prognoser kart er ikke nok for ekstreme ruter.
2. J. N. Gannaway G3YGF, “Tropopheric Scatter Expansion”, QST November 1983 (fra den eponytiske artikkelen om radiokommunikasjon, RSGB, august 1981). Formler av fritt rom tap og diffusjon, ekvivalent radius 8497 km, areal sikt på 10 dB per horisont, klima term 0.2 · (N-310), anleggskapasitet og signal/støyforhold, eksempelvis ved 144 MHz.
3. W. R. Hepburn, DX Info Centre, \"Tropopheric Ducting Prognose\" (dxinfocenter.com/tropo.html) og \"Hepburn Tropo Index (HTI)\". Indeksskala og verdibetydning, prediksjon på 6 dager.
4. F5LEN, værvarsel for Europa (tropo.f5len.org/forecasts-for-European: Liste over bilder i sideskriptet, klasselegende... Duct i bildet) og verdensprognosen (tropo.f5len.org/WW) og merk “Ny versjon av den verdensomspennende VHF troposferic utbredelsesprognose verktøyet”. Indeks basert på reaktivitet i enhet N, 40 trinn fra 3 timer, oppdatering 4 ganger om dagen.
5. mqtt.pskreporter.info, distribusjon MQTT i sanntid av reklamer av pskreporter.info (kurrert av M0LTE, flyt av P. Gladstone brukes med tillatelse). Doors 1883–86, pskr/filter/v2-emne.
6. Prosjektsamandrag av 8/11/2026 og Go-test (core.go, gui.go) gitt av forfatteren av programmet. Annonseregler, negative filtre, båndbreddesegmenter, telnethophåndtering, temaer, språk og portabilitet.
7. C ALL3.TXT, nominasjonsdatabase → JTDX utleier (I. UA3DJY, A. ES1JA script, JTALERT/hamspots.net data). Filtypen og tellingen av navnene som er verifisert på prosjektfilen.
8. WSJT-X, kildekode, NetworkMessage.hpp-fil (beskrivelse av UDP-protokoll: Heartbeat, Status, Decode). Generelt referanse, ikke annektert; JTDX og MSHV bruker samme format.
9. Reverse Beacon Network (Reversebeacon.net), tjenestesider av telnet noden. Porter 7000 (CW, RTTY) og 7001 ( FT8), innlogging med navnet kun.
10. D. Mills, RFC 4330 “Simple Network Time Protocol (SNTP) versjon 4”, IETF 2006. Pakkeformat, avfallsberegning og returforsinkelse. Generell referanse, ikke vedlagt.
11. NTP Bassengprosjekt (ntppool.org); INRIM, NTP tidsservice (ntp1/ntp2.inrim.it); PTB (ptbtime1.ptb.de). Servere som brukes til tidskontroll. Generell referanse, ikke vedlagt.
12. Avalonia UI, offisiell dokumentasjon (docs.avaloniaui.net). Multiplattform grensesnitt, temaer og layout fra høyre til venstre. Generell referanse, ikke vedlagt.
13. MQTTnet,.NET bibliotek for MQTT (github.com/dotnet/MQTTnet). Kunde som brukes til PSKReporter-flyten. Generell referanse, ikke vedlagt.
14. SQLite (sqlite.org) og Microsoft. Data.Sqlite (learn.microsoft.com). Arkiv i én fil. Generell referanse, ikke vedlagt.
15. Microsoft,.NET 10 – støttede operativsystemer og autonom publisering i én fil (learn.microsoft.com/dotnet). Plattform og grenser som er angitt i punkt 13. Generell referanse, ikke vedlagt.
16. ECMA-376 “Office Open XML File Formats”, del 1, avsnitt 22.1 (Office Math Markup Language). Struktur av.docx-filen og ligninger. Generell referanse, ikke vedlagt.
17. Liste over 28 språk i YA-MS wiki, gitt av forfatteren av programmet.
18. Open-Meteo, værvarsel API (open-meteo.com), med data fra NOAA GFS-modellen. Trykk på havnivå for isobar (se dokument 2).
19. J. P. Snyder, \"Map Projections - A Working Manual\", USGS Professional Paper 1395, 1987 og standard polynomial transformasjonsmetoder for georeferencing bilder. Generell referanse, ikke vedlagt.
20. Den astronomiske Almanac, avsnitt D, \"Låg presisjonsformel for månen\" (presisjonsposisjon på ca. 0,3°). Brukes til å gjenkjenne EME-forbindelser. Generell referanse, ikke vedlagt.
