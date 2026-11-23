LIVETROPOVHF · teknisk dokument 1
# Programmets funktion

> Automatisk oversættelse af italiensk: i tvivlstilfælde er det den italienske guide værd.

Hvad programmet gør, fra hvor det tager data og hvordan det gør dem til, hvad du ser på kortet og i listen. Version af 9 November 2026, relateret til projektet i C # (.NET 10 + Avalonia).

## Sammenfattende

- Formål: Tropo prognose (Hepburn [3], F5LEN [4], isobare GFS [18]) og reel observation af bånd i samme vindue, ifølge LA0BY [1] kontrollisten.
- Dataindsamling: to DX klynge, Reverse Beacon Network [9], dm4x klynge og PSKReporter [5] MQTT flow, UDP dekodning fra WSJT- X, JTDX og MSHV [8].
- Spot: bånd segmenter, negativt filter, luftspredning i stippet gul, position ved locators eller C ALL3. TXT [6] [7].
- Hvem lytter: mørke lyserøde cirkler med lyserøde linjer altid synlige mod stationerne følte [5].
- Spotkort: afstand, azimut, tropo indeks på stien og troposcatter tab med G3YGF [2] metoden.
- Sprog Hepburn og F5LEN: [3] billede og kalibreringsbænk [4].
- Watch: NTP kontrol på 10 servere hver 3 minutter [10] [11].
- Mistænkte rapporter: EME i Q65/JT65 anerkendt med placeringen af Månen, trunkerede locators, skimmere, karantæne og lyttere, med regler lært alene [20].
- Kriterier for udsmid og farvernes betydning sammenfattes i to tabeller (afsnit 11 og 11 quater).
- Temaer, 36 [17] sprog, eksportere dokumenter til Word med redigerbare formler [16], bærbare program og multiplatform [12] [13] [14] [15].

## 1. Formål

Programmet sætter sammen i et enkelt vindue to ting, der normalt ser på forskellige steder: forudsigelse af troposfer formering (Tropo indeks Hepburn og F5LEN, isoBare) og observation af, hvad der virkelig sker på bands (pletter fra DX klynge, Reverse Beacon Network og PSKReporter, plus listen over stationer, der lytter). Referencebåndet er 144 MHz; pletterne samles fra 50 MHz op.

Indstillingen følger kontrollisten foreslået af LA0BY for ekstreme tropo begivenheder: i de foregående dage du ser på Hepburn og F5LEN prognoser og temperaturprofiler i højden, mens begivenheden er på DX klyngen og på stedet kort bevis for, at åbningen starter [1]. LA0BY bemærker også, at de eneste Hepburn / F5LEN kort ikke er tilstrækkelige til at forudsige de længste forbindelser [1]

## 2. Vinduet og fanerne

Vinduet er et og ikke opdelt i mobile vinduer. Fra top til bund:

- Generelle indstillinger række: Ring og Locator til venstre; til højre UTC ur (f.eks. 08 / 11 / 2026 20: 33: 38 UTC, rav tekst) efterfulgt af NTP: OK eller NTP: FAILED, temaet menu og det sprog.
- I fem grupper: Kort og sted; Prorecast (Propria β, prognosen beregnet af programmet og beskrevet i Prognose Manual, derefter Hepburn og F5LEN); GRAVES; Data og kilder (DX klynge, PSKReporter, Reverse Beacon, UDP, Udpegninger og mistænkelige rapporter); Guides (Program Operation, Kort Building, Hvordan man former tropo, Manual prognose, Tips til operatøren, med en søgefelt, der tæller, som guide teksten vises). Under strålen viser en bar gruppens undersprog; hver gruppe husker den sidst anvendte.
- Indholdet af fanen og i bunden statuslinjen med tilstanden af kilderne, databasen af navne og tidspunktet for den seneste opdatering.

Den øverste linje indstillinger gælder for alle faner: det valgte tema også farve dataindsamling og baggrund af dokumenter; Dokument sider forbliver så hvid som et ark, så de udskriver godt.

## 3. Opkald og udlejer

Maidenhead udlejer kan have 4 eller 6 tegn (fx JN70 eller JN70CN). Programmet konverterer det til koordinaterne for centrum af kvadratet: felt 20 ° × 10 °, kvadrat 2 ° × 1st, subquadrate 5 ′ × 2,5 ′. Hvis teksten ikke er en gyldig locator bliver kanten af boksen rød og stationen ikke vises på kortet. Locator også tjener som et centrum for azimut projektion og som et udgangspunkt for afstande og azimut i annonce bord.

The Call er det navn, der bruges som en klynge login. Ved første start, indtil der er en gyldig nominering, forbinder klyngerne ikke: et vindue beder om navnet (og eventuelt udlejeren); bekræftet, navnet kopieres som et brugernavn i loginfeltet for hver klynge, der har det tomt og klynger forlader. Med "Senere" forbliver stille, og Connect knappen i hver klynge kræver nominering igen. MQTT, UDP og NTP bruger ikke login og forlade alligevel.

## 4. Dataindsamling: kilder

Hver kilde har sin egen sektion i fanen Datasamling, med Connected / frakoblet, de Genkendte og Connected bokse ved opstart, status, målere og register over de modtagne rækker. Ubanderede eller kasserede linjer fra filteret vises ikke i registret: kun antal afsnit meter. Sektionerne flankeres af to til to.

| Kilde | Forbindelse | Hvad bringer |
| 1. DX- klyngevalg | on0dxk.dyndns.org: 8000 kommandosæt / filter dxbm / pass 6 | Spot af amatør radioer, også SSB og CW. På ON0DXK filtreres VHF og aktiveres med den angivne kommando (poi pass 4, 2...). |
| 2. Anden kilde | dxmaps.com: 7300 (DXMAPS, EA6VQ) og s50clx.si: 41112 ( S50CLX, Slovenien) | DXMAPS indsamler VHF spots og giver også RBN skimmer spots (sæt / skimmer kontrol, allerede indstillet); S50CLX er en DX Spider klynge. Login med dit navn; redigerbare adresser. |
| 3. Name | telnet.reversebeacon.net 7000 CW / RTTY · 7001 FT8 | Automatisk spot. Login med navnet kun [9]. |
| 4. Klynge med PSKReporter | dm4x.dns.net: 8500 | Digitale spots af PSKReporter i klyngeformat. |
| 5. PSKReporter MQTT | mqtt.pskreporter.info 1883 · 1884 med TLS | Hver realtime modtagelse rapport: hvem sender, hvem lytter og deres [5] Locators. Bands 144, 222 og 432 MHz. |
| 6. Vejr og tropo kort | dxinfocentre.com / tropo _ eur.html tropo.f5len.org / forecast- for- European Model GFS | Hepburn [3] og F5LEN [4] tropoindekskort, havtryk for isobare (GFS via Open- Meteo [18]). Hepburn og F5LEN skal være tilladt for forfatterne. |

Klynge telnet (kilde 1-4). Efter forbindelsen venter programmet på navneanmodningen (tekst der indeholder login, kald:, kaldesignal eller dit opkald); hvis det ikke ankommer inden for 4 sekunder, bedes du sende login. Så send kommandoerne i afsnittet, adskilt fra punkt og komma, en hver 0,6 sekunder. Telnetkontroltegn fjernes, før linierne læses. Hvis forbindelsen falder og Riconnetti er aktiv, prøv igen efter 10 sekunder (15 hvis serveren ikke reagerer på alle) [6].

PSKReporter MQTT (kilde 5). Programmet indskrives i emnet pskr / filter / v2 / {banda} / {modo} / {tx _ call} / {rx _ call} / {tx _ grid} / {rx _ grid} / {tx _ dxcc} / {rx _ dxcc} fastsættelse kun båndet og forlader de andre felter gratis med tegnet + [5]. Det nøjagtige navn som serveren angiver båndene (for eksempel 2m, 1,25m, 70cm) skal kontrolleres ved den første forbindelse.

## 5. Fra klyngelinjen til stedet

En linje er et sted, når det har form DX de SPOTTER: frekvens DX kommentar hhmmZ. Programmet stammer spotter, station DX, frekvens i kHz, kommentar og nu UTC; navnene fjerne suffiks af skimmere (- #, -1...). Derefter anvende i denne rækkefølge følgende regler [6].

| Band | Segment accepteret |
| GRAVES (144) | 143,040 - 143,060 MHz |
| 144 MHz | 144 - 148 MHz |
| 222 MHz | 220-225 MHz (kun region 2 ITU, Amerika) |
| 432 MHz | EU-erhvervsgrenens økonomiske situation MHz |
| 1296 MHz | EU-forbruget udviklede sig i den betragtede periode som følger: MHz |
| 2320 MHz | EU-erhvervsgrenens økonomiske situation MHz |
| 10 GHz | 10,5 GHz |

1. Band. Frekvensen skal falde ind i et af de administrerede segmenter; alle resten, og især alle kommercielle under 49 990,0 kHz, er ikke vist og tæller som "ud af båndet".
2. Sådan. En af de bedst kendte måder at kommentere er FT8, FT4, Q65, JT65, ISCAT, CW, SSB, FM, RTTY, PSK31, MSK144, FSK441, JT6M); USB og LSB bliver SSB.
3. Negativ filter. Stedet kasseres, hvis det indeholder de tilstande MSK144, FSK441, JT6M, JTMS, ISCAT eller tags < AU > < AUE > < ES > < MS > < FAI > < F2 >, med ikke-nukleare uppercases og endda mellemrum i parentes. Disse er måder og formationer, der ikke er tropisk: Scatter meteor, aurora, og sporadisk, F2.
4. Airscatter. Hvis kommentaren indeholder < AS > eller < ACS > (også blandet, f.eks. < As >) eller erklærer stedet som luftscatter (luftscatter, luftscatter, flyspredning), forbliver stedet, men tegnes i gul med stiplet linje (mørk slip for at forblive læsbar på det klare kort) og fremhævet på listen.
5. Stationernes placering. Hvis der i kommentaren er to lokalisatorer (f.eks. JN54 < JN83) den første er af dem, der gør stedet og den anden af stationen DX; med en lokalisator, det er fra DX station. Hvad der mangler er søgt i C ALL3. TXT, kun med det nøjagtige navn (for bærbare computere se nedenfor). Hvis en station forbliver uden position, vises stedet på listen med noten "ukendt position", men ikke på kortet.
6. Doubles. En reklame med samme DX, samme spotter, samme band og samme kilde til en allerede til stede erstatter det, i stedet for at tilføje.

C ALL3.TXT er navnet database → locator, der anvendes af JTDX, i formatet CALL, LOCATORE,,. Projektfilen indeholder 1 048 847 navne, alle med gyldig locator; hovedet af filen taler stadig om 34 323 nomineringer, fordi det forblev den første udgave [7]. Det er indlæst i baggrunden ved opstart, så vinduet er umiddelbart brugbart. Filen søges ved siden af programmet, i mappen Data, i mappen hvorfra programmet blev startet og en eller to mapper ovenfor; Hvis der ikke er, et vindue fortæller det ved opstart og statuslinjen signalerer det i rødt. Dens navn bruger altid sin 6-karakter. Kun regel for locators: hvis den ene modtaget (spot, MQTT, dekodning, QSO desunti) har mindre end 6 tegn og C ALL3. TXT har en til 6, det er altid værd at C ALL3.TXT; for bærbare computere (F / IC8TEM, IC8TEM/P) er kun en præcis stemme: uden, stationen forbliver uden en udlejer og er ikke designet andetsteds. Reglen gælder også på tidspunktet for tegning, så de data, der ankom, før databasen blev indlæst, vil køre sig selv.

En database: C ALL3.TXT. Fra 1.3.4 alle locators er i én fil, C ALL3.TXT, som forbliver i formatet JTDX og WSJT- X (CALL, LOCATORE,,,,) og kan bruges som det er også i WSJT- X, JTDX og MSHV. Der er ikke flere filer fra hinanden. Øverst i filen, efter overskriften, er der to blokke markeret med en kommentar linje: / / [LIVETROPOVHF- MANUALI] med indgange indsat i hånden og / / [LIVETROPOVHF- CLUSTER] med korrektioner fra klyngen; derefter / / [LIVETROPOVHF- FONTI] med alle andre elementer i alfabetisk rækkefølge. Hver nominering vises én gang. I de sublinguale data og kilder › Udnævnelser (C ALL3) du leder efter et navn, du skriver udlejeren og du trykker på Tilføj / ændre: stemmen går i blokken af stemmerne i hånden, og det er værd frem for alt, selv over den locator, som stationen selv sender. Name Fjerner nomineringen fra C ALL3. TXT (den næste opdatering genoptager det fra kilder, hvis de har det). Tilføj en fil... læs en liste CALL, LOCATOR (du også acceptere punkt og komma, tabulering og plads) og skriv det blandt håndplukket; den valgte fil er ikke kopieret. Genindlæs C ALL3. TXT fra disken efter redigering med en editor. Komplet rækkefølge af kilder: egen locator, håndstemmer, klyngekorrektioner, resten af C ALL3. TXT (kilder, indlærte live locators og G7VRD), modtaget locator (som kun er gyldig i databasen, hvis den har 6 tegn og ikke slutter med LL eller MM).

Opdatering af C ALL3. TXT. I samme undersprog downloader opdateringsknappen nu kilderne (C ALL3. TXT af WSJT- X Forbedret, C ALL3. TXT EME af W7GJ, grid _ data.bin af JTDX), slutter sig til dem (den første, der har en nomineringsgevinst), kontrollere, at de er mindst 50.000 navne (ellers intet ændringer), gemmer C ALL3. TXT Den forrige C ALL3. TXT indgange tilbage, og en 6-tegn locator tilbage, hvor en kilde har kun 4 af samme kvadrat. Håndstemmer og rettelser fra klyngen forbliver øverst, uændret. Med opdateringsfeltet på egen hånd hver dag, foretages kontrollen i starten. Programmet omskriver C ALL3. TXT umiddelbart efter hver stemme i hånden og, for lært locators, højst hver 10 minutter og lukning; C ALL3. TXT af pakken er read- only og attributten fjerner sig selv. Linux installatører overskriver ikke en C ALL3. TXT allerede til stede; at for Windows installerer det kun, hvis det mangler. ALL3

Automatisk korrektion fra klyngen. I DM4X og RBN automatiske pletter kommentaren bringer to locators, for eksempel KN09tp < > KN09: den første er, hvad stationen du spot erklære af sig selv, til 6 tegn; den anden er, at stationen følte, ofte kun på 4. Hvis den første har 6 tegn (og ikke slutter med LL eller MM) og er forskellig fra den i databasen, programmet korrigerer C ALL3. TXT alene: korrektionen går i blokken / / [LIVETROPOVHF- CLUSTER], med datoen for den sidste bekræftelse i det tredje felt, er den gyldig over kilderne (under håndstemmerne) og vises mellem de mistænkelige signaler opdateret som "C ALL3 ". Så det overlever C ALL3. TXT-opdateringer. En ubekræftet korrektion i to år mister forrang og forbliver som en normal stemme.

Sidste kilde: G7VRD. Hvis en nomineret ikke har et 6-tegn i C ALL3. TXT, programmet spørger det i baggrunden for den offentlige API af G7VRD (api.g7vrd.co.uk, "locator by callsign"), som indsamler locators automatisk fra netværkstrafikken. Du accepterer kun en 6-tegn, der ikke er centrum af pladsen (LL eller MM); anmodningerne er grupperet, på maksimalt 40 nominative per minut, og hver nominative spørger en gang om dagen, fordi det er en hobbistisk service uden garantier. Locator fundet ind C ALL3. TXT hvis navnet ikke er der eller kun har 4 tegn i samme firkant; søgningen slukkes med feltet i fanebladet Navn.

Udnævnelser med baren. I PSKReporter MQTT emner, baren adskiller niveauerne, så sammensatte navne kommer med et punkt i stedet for baren ( IV3. HB9CAT). Programmet sætter baren tilbage og, hvis meddelelsen indeholder de fulde navne, bruge dem: IV3/HB9CAT forbliver IV3/HB9CAT på kort, liste og database.

Locators lært live. C ALL3. TXT indeholder ikke alle: for eksempel SP9EMF og SP9NVY er der ikke, og derefter forbliver 4-tegn locator sendt af PSKReporter (kortet siger det). For at reducere disse sager programmet skriver i C ALL3. TXT hver 6- tegn, der kommer fra enhver kilde (PSKReporter, såsom sender eller som modtager, klynge, UDP dekodning) til en nominering, at C ALL3. TXT ikke eller har kun 4 tegn i samme firkant; aldrig ændrer en stemme til 6 tegn. Indtil 1.3.3 disse lokaliseringssteder var i Data / LOC6.TXT.

Overgang fra 1.3.3. Ved første start af 1.3.4 filerne bortset fra de tidligere versioner (Dati / C ALL3 - MANUALE.TXT, Data / C ALL3 - AGGIUNTE, Data / C ALL3 - CLUSTER. TXT, Data / LOC6. TXT og Data / C ALL3 - G7VRD. TXT) fusioneres til C ALL3. TXT Release skrivning, gamle filer tage.fuso udvidelse, du ikke længere læse, og du kan slette.

Automatiske modtagere. RBN, dm4x og PSKReporter rapporterer ofte navne med et suffiks, der angiver modtageren: -SDR, / SDR, - # (skimmer RBN) eller -1, -2... (plus modtagere af samme station). For udlejer suffikset fjernes: OK2OZL -SDR tager OK2OZL JN89UE fra C ALL3.TXT, hvis stationen ikke allerede sender en 6-karakter. Navnet er stadig det med suffikset.

Locators skåret. Ikke alle kilder behandle udvidelsen af deres udlejer: mange indstille det til 4 tegn, og nogle automatiske carry-over systemer bevidst truncate 4-tegn locator, selv dem, der får data fra den reelle lytning af FT8, FT4, Q65 og JT65, eller fuldføre det med midten af pladsen (LL eller MM, f.eks JN89MM). Derfor erstattes et 4- tegn, eller 6- tegn, der slutter med LL eller MM, af C ALL3. TXT af samme kvadrat. Hvis den korrekte placering og centrum af pladsen ikke falder sammen med et par dusin km, fejlen forbliver lille i forhold til tropo afstande.

## 6. Hvem lytter?

Hver MQTT besked fra PSKReporter rapporterer navn og lokalisering af dem, der modtager [5], så du behøver ikke at kigge efter det i C ALL3.TXT. For det valgte band i vælgeren holder programmet, for hver modtagende station: udlejer, sidste modtagelse, afkodede måder, antal stationer følte og maksimal afstand. En station forlader listen, når den ikke længere modtager noget i det valgte område (3, 5, 10, 15, 30 eller 60 minutter).

På kortet hver modtager er en mørk pink tom cirkel, og fra hver start fuld lyserøde linjer mod de stationer, der føler og sender tilbage til nettet. Linjerne ses altid, uden at klikke. Udnævnelser af lyttestationer, hørestationer og de to hoveder af hvert sted er altid skrevet på kortet, uden at skulle klikke (samme navn er skrevet én gang). Ved at klikke på en cirkel sine linjer bliver endnu tykkere og åbner fanen med detaljer. Hvis lokalisatoren fra MQTT har mindre end 6 tegn, programmet bruger, at C ALL3.TXT (omkring en million navne), forudsat at det er i overensstemmelse med pladsen modtaget.

Scatter meteor modes. Som med klyngepletter kasseres og tælles MQTT-forholdet i MSK144, FSK441, JT6M, JTMS eller ISCAT kun; det samme gælder UDP-dekodninger på disse måder.

Ikke ægte lyttere. Nogle stationer erklærer at modtage i 144 MHz, men i virkeligheden modtageren er på et andet band (forkert frekvens indikation): deres rapporter ville designe ueksisterende åbninger på tusindvis af km. Programmet kontrollerer det selv. Hver station, som en lytter siger at høre i VHF følges i 30 minutter på alle bands, med en dedikeret MQTT inskription (pskr / filter / v2 / + / {nominativ} / #) [5]. tjener som bevis. Hvis mindst to af de stationer, der høres af en lytter, i de samme minutter (± 10) er aktive på andre bands, er lytteren ikke ægte, og alle VHF-rapporter kasseres; MQTT-registret rapporterer navnet og de stationer, der forrådte ham. I listen over pletter, ved siden af måleren, viser MQTT-knappen flowets tilstand (grønt blad hvis tilsluttet, rødt hvis ikke) og hvor mange lyttere der blev kasseret i det valgte band og område; ved at klikke på det åbner fanen med listen over udsmid og for hver station findes på andre bands.

Lang forbindelse porcelæn. En forbindelse over 1000 km (Clumbing Spot, MQTT Lytning, QSO Desunti) vises ikke umiddelbart: tilbageværende, og tælleren "holdt > 1000 km" siger antallet. Det afsløres, når en anden forbindelse, mellem forskellige stationer og mindst halvdelen langs, bekræfter det med et af disse kriterier: samme station ved et hoved (for eksempel den samme lytter) og den anden station i samme retning inden for 10 °; lignende korridor, med begge hoveder inden for 200 km fra den anden forbindelse; parallel rute, med retning inden for 10 ° og sideafstand inden for 200 km, overlappede langs ruten. Både dukke op og MQTT log viser årsagen. Dens UDP dekodninger er altid synlige og gælder som bekræftelse for andre. De tilbageholdte forbindelser forbliver i hukommelsen i 60 minutter.

Intet under 144 MHz. Programmet virker fra 144 MHz op: alt under 143 MHz kasseres. Undtagelsen er GRAVES, den franske radar sender i CW på 143,050 MHz af Broye- lès- Pesmes ( JN27SI): en handel mellem 143,040 og 143,060 MHz, eller med GRAVES i navnet, vises mellem 144 MHz med radar som sender. Annoncen siger, at hvis modtageren er inde i strålen (sektor D, C, B, A fra 90 ° til 270 ° af azimut set fra radaren, 6 skridt fra 7,5 ° hver) eller ud; fanen GRAVES, som i YA-MS, viser himlen set fra radaren: sektorer, trin belyst på det tidspunkt (sekvens PE1ITR 2021: 0,8 s per trin, cyklus 4,8 s Data og geometri kommer fra YA-MS. Det er svært at mærke det i tropen, men det kan ske.

Rehabilitering fra menneskelige klynger. En station, der også vises på andre bands tæller ikke som mistænkt, hvis, i de samme minutter (± 30), DX menneskelige klynger ( ON0DXK, GB7RAU; ikke dm4x, RBN eller andre automatiske kilder) den aktive skade på det pågældende band, i pletter lavet af andre navne: det er tilfældet for stationer, der virkelig arbejder på flere bands sammen. Således en kasseret lytter vender tilbage synlige (MQTT register skriver "rehabiliteret") og et langvarigt link til lave bånd kan afsløres.

Aktive stationer under 144 MHz. I karantæne af lange forbindelser, en station, der i samme minutter høres på bånd under 144 MHz (fra MQTT rapporter af de andre bånd) ikke bekræfte andre forbindelser og er ikke afsløret, selv om en lignende eller parallel rute ville bekræfte det.

## 7. Liste over reklamer

Det optager 35% ret af kortet fanen og stedet. Top: titel, båndvælger (50, 70, 144, 222, 432, 1296, 2320 MHz, 10 GHz), intervalvælger (sidste 3, 5, 10, 15, 30, 60 minutter) og opdateringsnøgle. Nedenfor fire meter: synlige pletter, AS / ACS, kasseret fra filteret, stationer lytter.

Hver linje viser typen af linje, tidspunktet UTC, DX → DE, vejen, afstanden i km og, på den anden linje, kilden, frekvens og kommentar. Den seneste er på toppen. Et klik på linjen vælger stedet: på kortet linjen vokser og de to navne vises.

## 8. Spot kort og troposcatter tab

For det valgte sted viser fanebladet lokatorer, afstand, azimut, frekvens, tilstand, kilde, nu, kommentar, den gennemsnitlige tropo indeks langs stien og et estimat af rutetab for spredt tropo.

Afstand og azimut beregnes på den maksimale cirkel med Jordens radius på 6371 km (formlen for emisenoverso for afstanden, oprindelige azimut fra DX-stationen til spotter).

Tabs troposcatter. Det grundlæggende tab, for to stationer på glat jord, er summen af tabet af fri plads og diffusion tab: G3YGF [2]

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

Tabene er i dB, D er afstanden i km, f frekvensen i MHz, θs diffusionsvinklen i grader.

8497 km er Jordens ækvivalente radius (4 / 3 af den fysiske). θA og θB afhænger af hver stations horisont:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

med Δh højde af den hindring, der gør fra horisonten med hensyn til stationen og dens afstand. Hvert horisontniveau koster ca. 10 dB [2]. Programmet ikke kender horisonten af stationerne, så det viser det grundlæggende tab med θA = θB = 0 og erklærer det ("glat jord"). G3YGF tilføjer et klimatisk udtryk, hvor N er reaktiviteten til [2] jorden; det vil blive brugt, når N er tilgængelig fra vejrmodellen:

```math
L_{N} = 0,2 \cdot (N - 310)
```

Verifikation: For 110 km til 144 MHz giver formlen 166,5 dB mod ca. 168 dB senge på artiklens diagram. For 500 km til 144 MHz giver omkring 206 dB. Det forventede signal / støjforhold er systemets kapacitet (eirp + effektiv følsomhed af modtageren) minus tabet: med G3YGF 144 MHz-modelsystemet (198 dB) ville en 500 km sti give omkring - 8 dB i SSB [2], dvs. forbindelsen er kun med bedre spredning af den rene spredning.

## 9. Min dekodning: UDP fra WSJT-X, JTDX og MSHV

WSJT-X, JTDX og MSHV kan sende UDP væk hver linje, de afkoder. Programmet lytter på 127.0.0.1: 2237 og gentager den samme pakke, byte per byte, på 127.0.0.1: 2238, så de andre programmer, der brugte denne port (GridTracker, JTAlert, en logger) fortsætte med at arbejde, hvis du flytter dem på 2238. Adresser og døre ændres i UDP sublingual af datasamlingen; som en lytteadresse kan du også bruge en multicast gruppe (fx 224.0.0.1), hvis afkodningsprogrammet er indstillet på denne måde.

Pakker følger WSJT- X netværksprotokol: en indledende magisk nummer 0xADBCCBDA, skema nummer og meddelelsestype. Programmet anvender hovedsagelig tre [8] typer:

- Heartbeat: siger, hvilket program der sender data og med hvilken version.
- Status: frekvens for opkald og tilstand; fra den frekvens, du får bandet (med den samme tabel over pletter).
- Decode: Nu, SNR, tid og frekvens affald, måde og tekst af den afkodede meddelelse.

Fra teksten får du den station, du hørte: i CQ 9 A2PQR JN83 er stationen efter CQ (gemme DX, EU, TEST...), i IK0JKL IT9MNO JM68 er den anden, dvs., der sender. Locator er den af meddelelsen, hvis der er (4 tegn, forskellig fra RR73); ellers du søge i C ALL3. TXT. De er lyttelinjer: stationer, som din modtager hører nu. På kortet bliver de røde linjer fra din QTH til stationen, med navnet; laget lyser op med den afkodede boks fra mig og kræver en gyldig locator. På listen til højre fortæller en tæller, hvor mange stationer du afkoder på det valgte band.

QSO desunti dal live. Når en afkodning er en afsluttende meddelelse i form af C ALL1 C ALL2 RR73 (eller 73 eller RRR), for eksempel IC8TEM IK3FHP RR73 eller IK3FKP SV8PEX 73, har de to stationer afsluttet et link. Programmet registrerer det som QSO, med locator af hver taget fra den sidste meddelelse, hvor han erklærede det eller, hvis der er nej, fra C ALL3.TXT [7], og trækker det på kortet som en grøn linje mellem de to stationer (strate "QSO desunti dal live"). Et par stationer tæller én gang pr. bånd; det band og område, der er valgt på listen, er gyldigt.

I samme undersprog viser fejlretningsruden teksten til hver modtaget pakke (type, tid, værdier og besked), efter anmodning også hexadecimale bytes, og en linje til videresendelse. Det er for at kontrollere, at afkodningsprogrammet faktisk sender data.

## 9 bis. Sprog Hepburn og F5LEN

Hver viser, hvad programmet tager fra kilden. Knappen "Læs siden og download alle billeder" åbner den angivne side (base dxinfocentre.com / tropo _ eur.html for Hepburn [3] og tropo.f5len.org / provastsfor- europe for F5LEN [4]) og downloader alle billeder i cachen ved siden af den eksekverbare; du kan også indsætte adressen på et billede. For hvert billede er der en boks til at udelukke det og et felt med prognosetiden. De 31 europæiske billeder af F5LEN er opkaldt efter tidspunktet for modellen race (03.png... 48.png hver 3 timer, 54... 96 hver 6 timer, 108... 192 hver 12 timer) og programmet tager tid fra navnet på [4] fil; for Hepburn er det rækkefølgen af siden. I fanebladet F5LEN vælger en menu regionen: Europa (tropo.f5len.org / provast- for- europe), Australien (tropo.f5len.org / Australia, vk03.png... vk192.png) eller Atlanterhavet mellem Kap Verde og Irland (tropo.f5len.org / atlantisk-ocean / atlantisk-d4-ei, atl03.png... atl192.png). Alle har de samme trin.

Nedenfor er kalibreringstælleren. Et klik på billedet tilføjer et kontrolpunkt (locator eller lat, lon: det tager mindst 6) eller en farve af legenden med sin værdi. Programmet beregner pixel matchende → koordinater med en anden grad polynomiel interpolation til mindste kvadrater [19], læser farverne med den valgte tolerance og, for F5LEN, konverterer enhederne N i skalaen 0-10 med en · N + b. "Gem kalibrering og test læsning" skriver filen i Tarature mappe og kontrollerer, hvilken pixel din locator falder. Forud for opstilling af en kalibrering skal kortets forfatter have tilladelse hertil.

Regioner. Hver kilde har flere regioner, hver med sine fixed- navn billeder: Hepburn tr _ map / fcst / {code} {ore}.png? v {data} (eur Europe, nwe North- Western Europe, eeu Østeuropa, ent North- East Atlantic, nat North Atlantico, eam Nordamerika Øst, nsa Sydamerika Nordamerika, Sam Sydamerika, midt-Østasien, nca Central- Septential Asien Fanebladet for hver kilde viser først og fremmest forhåndsvisningen af fusionen: alle downloadede billeder sat sammen som på hovedkortet, med den punkterede rude og navnet på hver region, et gråt slør, hvor intet billede dækker området og en skyderen til at rulle timerne; til venstre listen over regioner, hver med boksen til at bruge det, antallet af kort læst, og hvorfra ekstremerne kommer (OCR, i hånden eller fabrikken; hvis OCR mislykkes teksten, han læste vises). Den manuelle kalibreringsbænk forbliver bag den enslydende knap. Menuen Bench Region vælger hvilket udseende, feltet "Brug denne region på kortet" indeholder det i indekset. Rammen findes alene (to vandrette linjer og to lodrette linjer af samme farve); at placere billedet tjener yderliggående lat / lon af rammen: for Hepburn Europa (10 ° W- 40 ° E, 27 ° -56 ° N) og F5LEN Europa (15 ° W- 40 ° E, 30 ° -65 ° N) er allerede i programmet, for de andre regioner, de skriver i de fire O E S N felter læser aksen etiketter. Skalamultiplikatoren gør de to kilder sammenlignelige: slutværdi = multiplikator × legende værdi. [3] F5LEN [4] F5LEN

## 9 ter. Eksportér til ord

De to dokumenter har på toppen af "Export to Word" knappen. Filen er skrevet i Office Open XML format; formlerne bliver Word ligninger (OMML), så de forbliver redigerbare med [16] ligning editor. Hvis gemmevinduet ikke er tilgængeligt, ender filen i mappen Dokumenter ved siden af den eksekverbare.

## 9 quater. Hvem kalder mig

Hvis en meddelelse sendes til dig på den lokale UDP (fornavnet er dit, for eksempel IC8FAX IK3FHP JN55 eller IC8FAX IK3FHP -12), programmet spor fra din QTH til stationen, der kalder dig en sort stiplet og blinkende linje, med navnet og lokalisatoren ved siden af dig. Locator kommer fra C ALL3.TXT (herunder hånd stemmer) eller besked. Linjen forbliver indtil UDP passerer lukningen af QSO med denne station ( RR73, RRR eller 73, i en af de to retninger), højst 3 minutter fra sidste opkald; du trækker over alt og ikke følger udelukkelseskriterier, så et direkte opkald aldrig undslippe.

PSKReporter, via MQTT, sender ikke teksten til meddelelserne: siger kun, hvem der har modtaget hvem, med hyppighed, måde og SNR. Derfor anerkendes et direkte opkald kun af dets egne UDP-dekodninger.

## 10. Tidsstyring (NTP)

Digitale tilstande og spot tid kræver et præcist ur. Hvert 3. minut programmet afhører 10 NTP servere (SNTP på UDP 123): 0.pool.ntp.org, 1.pool.ntp.org, 2.pool.ntp.org, 3.pool.ntp.org, it.pool.ntp.org, ntp1.inrim.it, ntp2.inrim.it, ptbtime1.ptb.de, time.google.com, time.cloudflor.com [11].

For hvert svar beregner udsmid og returnering forsinkelse, kasserer de unormale værdier og tager medianen, med beregning af udsmid og forsinkelse leveret af SNTP [10]. Med mindst 3 svar accepterer du UTC uret i programmet justerer og næste vises NTP: OK i grøn; hvis svarene ikke er nok vises NTP: FAILED i rød og uret forbliver det af computeren. System ur er ikke rørt, fordi administrator tilladelser ville være nødvendige; alle interne program tidsplaner (pletter, 5-60 minutter vinduer, dekodings) bruge den justerede tid. Styringen vender altid i baggrunden: i grænsefladen ser du kun resultatet ved siden af uret; servere, intervaller og minimal respons ændring i LIVETROPOVHF.ini (ntp _ servers, ntp _ minutes, ntp _ min).

## 10 bis. Generelle udelukkelseskriterier

To kasser i lag bar gælder for alt hvad du trækker (spot, PSKReporter, dekodning, QSO desunti):

- Omfatter ikke under 100 km. Trafikken mellem stationer på under 100 km er måske ikke væsentlig for tropo (jordbølge, optisk rækkevidde): med on- box du ikke tegne og ikke vises på listen. Begyndelsen er aflyst.
- Mere end 1000 km: kun firkanter. Omkring din QTH (lokalisatoren indstillet til at starte) er der en krone på 1000 km i alle retninger. Hvis kun en af de to stationer af et link er uden for denne krone, forbindelsen er ikke trukket som en linje: i stedet de farve i lilla, punkteret på 45 °, lokalisatoren firkanter (4 tegn) af de involverede stationer. Det samme gælder for PSKReporter-stationer, der lytter til over 1000 km, og som fremstår som et kvadrat i stedet for som en cirkel. Så langt trafik kan du se uden at udfylde kortet over linjer. Uden en gyldig lokalisator tæller længden af forbindelsen. Pladsen af din QTH aldrig farve i lilla, selv når du lytter til en station ud over kronen: gælder den indstillede udlejer, ethvert navn bruger, der opererer fra stationen. Kronen radius vælges ved siden af kassen: 1000, 700 eller 500 km. Boksen Corone 1000 / 700 / 500 km trækker de tre kroner rundt om QTH, trukket i lilla, med det mest markerede valg og afstanden skrevet nord. I listen over steder er afstanden efterfulgt af.. Begyndelsen er på. Kriteriet gælder efter karantæne: lange forbindelser, der stadig holdes, forekommer ikke engang som kvadrater.

Hvad er de lilla firkanter dækket ved 45 °. Trafikken væk fra din QTH interesser dig at forstå, hvor formeringen er åben, men dens linjer, hundredvis af kilometer lange og ofte tiere, dækker kortet og skjule, hvad der tæller for dig: åbningerne du kan udnytte fra din station. Af denne grund, ud over den valgte krone, programmet ikke trække linjerne, men farve locator firkanter (2 ° × 1 °) af de involverede stationer. Resultatet er et kort over "aktive zoner": hvor mange nærliggende lilla firkanter vises, er der en kanal eller åbning, selv om det ikke vedrører dig direkte; Hvis området udvider sig mod dig, er det signalet til at pege antennen i den retning. Den 45 ° dashboard lader dig se under tropo indeks og gitter, så de sammenligne med et blik prognosen (nederste farve) og den reelle trafik (lilla). Din QTH firkant aldrig farver, fordi det er udgangspunktet og ikke et fjernt område. Kun de firkanter, der er alle ud over kronen er farvet: et kvadrat krydset af kronen, eller inde, forbliver fri, selv om der er en af de to stationer. For at se linjerne, skal du blot slukke for boksen eller vælge en større krone.

## 11. Emner og sprog

Temaer: de 18 fælles for IC8TEM -programmerne, det samme som YA-NEC og E-LAYER ANALYSER: Klar, nat, sort, otte pasteller (blå, orange, akvamarine, grøn, pink, lilla, indigo, brun), Scia meteorica (YA-MS palette), de tre af IP- TRX- CTRL (Chiaro, Notte, Nero) og Blue, Purple, Green. Fra 1.3.4 ligner Chiaro YA-NEC: lys grå baggrund, omkranset hvide paneler, blå natfaner og blå accent. I mørke temaer også grundkortet bliver mørk. Farveskalaen af prognosen forbliver den samme i alle temaer. På smalle skærme (under 1500 pixels, for eksempel 1366 × 768) uret øverst viser kun time, så tema, markør af tekster og sprog forbliver synlig. Sprog: YA- MS [17] wiki (Engelsk, Rejser, Deutsch, Nederlands, Français, Español, Português, Dansk, Norsk, Svenska, Íslenska, Eesti, Lietuvië, Latviešu, Руский, кра, нська, Türkçe, Med arabisk, hebraisk og Urdu hele vinduespejle fra højre til venstre; kortet forbliver som [12]. Oversættelser vedrører grænsefladen; de to tekniske dokumenter og meddelelser i statuslinjen forbliver på italiensk. Dagenes navne kommer fra de internationale indstillinger. NET for det valgte sprog.

Tema og sprog ændring fra de to menuer øverst til højre, ved siden af uret, og gælder straks, uden at genstarte programmet og uden at afbryde indsamlingen af data. Valget er gemt i LIVETROPOVHF.ini.

## 11 bis. Mistænkte og selvlæringsrapporter

Fanebladet Mistænkte Rapporter indsamler alt, hvad programkontrollen finder mærkeligt, grupperet efter type og navn, med de sidste 24 timer angivet. Programmet lærer af sig selv: for hvert par (type, navn) tæller de positive resultater (+) og modsat (−), og efter 3 aftalte resultater skaber en automatisk regel; et modsat resultat sætter det i tvivl. Brugerbeslutninger (Good, Poor) gælder altid; pulsante knappen returnerer beslutningen til programmet. Reglerne findes i Data / APPRENDIMENTO. TXT, ved siden af programmet; de automatiske dem ikke forstærket i et år er glemt.

| Type | Hvornår | Afslut + / − | Regel effekt |
| EME ( Q65/JT65) | Q65 eller JT65 over 1000 km med Månen over horisonten til begge hoveder, eller EME i kommentar | − hver gang månen er almindelig | Kassér: EME station, dens lange Q65/JT65 s er også udelukket uden fælles måne. Godt: behandles som en tropo |
| Lang forbindelse | over 1000 km i karantæne | + afsløret, − udløbet uden bekræftelse | Godt: Længden af stationen viser straks. Kassér: aldrig |
| Lytter | PSKReporter modtager der hører aktive stationer på andre bands | − kasseret, + rehabiliteret af menneskelige klynger | Godt: aldrig kasseret. Kassér altid: |
| Forskellige udlejer | Square modtaget forskellige fra C ALL3. TXT | + hver gang du gentager det | Godt: stationen er flyttet, er det værd pladsen modtaget. Kasseres: Vale C ALL3. TXT |
| Square Center | 6-tegn med LL eller MM |  —  | Kun oplysninger |
| Automatisk modtager | Udnævnelse med -SDR, / SDR, - # or -n |  —  | Kun oplysninger |

EME. Q65 og JT65 er kommunikationsmåder via Luna: I teorien falder de ind under udelukkelsesklausulen som den spredte meteor, men de bruges også i tropo. Programmet beregner placeringen af Månen med lav-præcision formler af Almanac Astronomisk (omkring 0,3 °) [20]: Hvis en forbindelse i Q65 eller JT65 overstiger 1000 km og Månen er over horisonten til begge hoveder, er det næsten sikkert EME og er udelukket (det tæller blandt de udelukkede). Hvis Månen ikke er almindelig, forbindelsen forbliver mulig i trope og passerer gennem karantæne af lange forbindelser, men signalet forbliver synlig. Den gælder for klynger, for MQTT og dens egne UDP-dekodninger.

## 11 ter. Kontaktkassationskriterier, en efter en

Programmet ønsker kun at vise den tropolske formering i 144 MHz og videre. Hver indkommende data (Cluster Row, PSKReporter Report via MQTT, UDP dekodning) passerer gennem en række filtre, altid i samme rækkefølge. En kasseret data forsvinder ikke i ingenting: tælles (kontakt af den udelukkede i øverste bar) og, når årsagen er tvivlsom, vises i fanen Suspenderet Rapporter.

|  N.  | Kriterier | Hvor er den? | Hvad kasseret | Kan du skifte? |
|  1  | Band | klynge, MQTT, UDP | Alle under 143 MHz (også 50 og 70 MHz), undtagen GRAVES 143,050 MHz. Band holdt er 144, 222, 432, 1296, 2320 MHz og 10 GHz | Nej. |
|  2  | Satellit QO- 100 | klynge, MQTT, UDP | 2400,0-2400,5 MHz og 10489,5-10490 MHz MHz: det er geostationær satellittrafik, ikke tropo | Nej. |
|  3  | Meteorspredning | klynge, MQTT, UDP | JT6M og ISCAT (også ISCATA / B), kommentarer med "&" og ikke-tropoxy formering etiketter (< AU >, < AUE >, < ES >, < FAI >, < F2 >) altid. MSK144, FSK441 og JTMS (også med < MS >) kun over 250 km: inden for 250 km vi accepterer, fordi på denne afstand disse tilstande også anvendes i tropo | Nej. |
|  4  | EME (via Luna) | klynge, MQTT, UDP | EME skrevet i kommentaren; Q65 eller JT65 over 1000 km, når Månen er over horisonten til begge hoveder; stationer lært som EME | Ja: God regel i mistænkelige signaler |
|  5  | Ikke-ægte lytter | MQ | PSKReporter modtager, der siger at høre på 144 stationer aktiv på det tidspunkt på andre bånd (mindst to): Hans forhold er væk. Tilbage godt, hvis DX menneskelige klynger bekræfte de samme stationer | Ja: god / fattig |
|  6  | Karantæne for lange forbindelser | alle linjer | Over 1000 km et link forbliver skjult indtil det finder en bekræftelse: samme station i samme retning, en anden rute i en korridor på 200 km, eller parallel rute inden for 10 ° og 200 km langs mindst halvdelen. Uden bekræftelse udløber efter 60 minutter | Ja: God / Off til station |
|  7  | under 100 km | linier og liste | Med on- box, de korteste forbindelser på 100 km (jordbølge, optisk rækkevidde) | Box, slukket ved opstart |
|  8  | Ud over kronen fra QTH | linier | Hvis en station er over 1000, 700 eller 500 km (efter eget valg) fra din QTH, ingen linje: kun Lilla pladser dækket af de to stationer. Din QTH Square aldrig farver | Box og radius, på 1000 km |
|  9  | Tidsvindue | alt | Ældre steder i det valgte område (fra 3 til 60 minutter) | Menu øverst |
|  10  | Dobbelt | klynge | Samme sted (samme DX, samme spotter, samme band, samme kilde) gentaget | Nej. |

Note vedrørende kriterium 4 og 6. En forbindelse i Q65 eller JT65 over 1000 km uden Common Moon er mulig i tropo: ikke kasseres efter kriterium 4, men passerer for karantæne af kriterium 6 som enhver anden lang forbindelse. Direkte opkald til dig (afsnit 9 quater) er den eneste undtagelse fra kriterium 7 og 8: de altid trække.

Hvad der ikke er spild. Korrektioner af lokalisatoren (C ALL3. TXT med håndstemmer, rettelser fra klyngen og lærte locators, suffikser -SDR, centrum af kvadratet LL / MM) ændre, hvor du tegner en station, ikke kassere det. En station uden udlejer kan derimod ikke tegnes: forbliver på listen med afstanden "-".

## 11 quater. Farver og symboler

Hver farve har kun én betydning, lige i alle temaer (ændre kun baggrunden).

| Farve og tegn | Betydning |
| Kontinuerlig blå linje | Spot fra DX klynge, RBN eller DM4X mellem to stationer |
| Gul linje med mørk kant | Spot med airscatter kommentar (refleksion på fly) |
| Mørk lyserød tom cirkel | Lyttestation på PSKReporter (MQTT) |
| Tynde lyserøde linjer fra cirklen, med en lyserød kugle i bunden | Stationer som lytteren hørte, med navnet altid skrevet; tykkere linjer for den valgte lytter |
| Rød linje og rødt navn | Din UDP dekodning (WSJT- X, JTDX, MSHV): Hvem er du? |
| Rød line blinkende slag | QSO i gang med denne station: Han ringede til dig, og lukningen er endnu ikke ankommet, eller er DX Call of WSJT-X / JTDX med den aktiverede transmission. Fortsæt med RR73, RRR eller 73 |
| Grøn linje | QSO desunti dal live: Luk RR73, RRR eller 73 UDP visning |
| Sort linje med hvid kant | Nogen ringer til dig: det varer op til RR73/RRR / 73 eller op til 3 minutter |
| Squares på 45 ° | Trafik med mindst én station ud over den valgte krone: Ingen linjer, kun firkanter locator |
| Søg efter lillaer med afstand til nord | 1000, 700 og 500 km omkring din QTH; dette valg er mere markant |
| Circle og punkt i farven på temaet | Din QTH, med navn og udlejer |
| Orange bold med GRAVES 143.050 | RadarGRAVES ( JN27SI) |
| Prøve fra blå til rød | Hepburn trope indeks (0-10) eller F5LEN klasse, eller deres gennemsnit; varmere farve, bedre betingelser |
| Lysegrå fløjl | Område uden kontroldata for det valgte tidspunkt |
| Grey- blå linjer med værdien i hPa | Isoberat hver 4 hPa; H ved blåt højtryk, L ved rødt lavtryk |
| Røde, blå og lilla stiplede linjer | Istermi til 850 hPa hver 4 ° C: rød over nul, blå under nul, violet nul |
| Prøve orange | Termisk inversion: luft til 925 hPa varmere end på jorden, mere intens med den stærkere inversion |

I Mistænkte Rapporter kanten af hver fane siger gældende regel: grøn god, rød Scarta, grå ingen regel.

## 12. Indstillinger og portabilitet

Vindue. I starten optager vinduet 90% af skærmens brugbare plads (cut application bar), centreret, så på en 15-16 tommer notesbog med en 125% eller 150% ændre størrelse titellinjen forbliver synlig. Hvis den nyttige plads er under 1280 × 720 point, vinduet del maksimeret. Størrelse, position og maksimal status gemmes i LIVETROPOVHF. ini ved lukning og afslutning ved næste start, forudsat at titellinjen stadig falder ind i en skærm (for eksempel efter at løsne en ekstern skærm); ellers vender du tilbage til 90%. Mindstestørrelsen er 900 × 560.

Programmet er bærbar: aldrig skriver i AppData mappe eller i registret. Alt er i mappen for eksekverbar: LIVETROPOVHF.ini (sæt, skabt ved første lukning), C ALL3.TXT (med håndskrevne stemmer, indlærte rettelser og locators), kort cache og vejrdata, regler lært (Dati / APPRENDIMENTO.TXT) og LIVETROPOVHF.db arkiv. Du kan også holde det på en USB-stick, så længe mappen er skrivbar.

Ad-arkiv (SQLite [14]). Hvert sted accepteret og hver PSKReporter lytte rapport gemmes i LIVETROPOVHF.db, en SQLite database i en fil. Til minde kun de sidste 60 minutter tilbage til listen og kortet; arkivet bevarer alt og tjener til historiker og statistik (også UDP dekodings), for eksempel åbninger for band og per dag, de maksimale afstande, kvadrater nået, og den bageste sammenligning mellem den forventede indeks og de virkelige pletter. De linjer, der kasseres fra filteret, tælles, men arkiveres ikke.

## 12 bis. Automatisk installation og opdateringer

Fra version 1.0 programmet allerede downloads klar fra Release side af github.com / ic8tem / LIVETROPOVHF, i de samme pakker YA-MS: installer til Windows (64 bit, 32 bit og ARM, installerer i C:\ LIVETROPOVHF uden administratorrettigheder) og bærbar zip; til Windows 7 SP1 og 8.1 pakker adskilt (-windows7-x64 og -wind. Deb,.rpm, Arch, Slackware, Appimage og tar.gz for Linux; pakker til Raspberry Pi (64 og 32 bit) og Alpine;.dmg og apps.zip til Mac (Apple Intel og Intel). Med Linux-pakker kopieres programmet til første start i ~ / LIVETROPOVHF, så indstillinger og data forbliver ved siden af programmet; på Mac-data er født ved siden af LIVETROPOVHF.appen.

Hver 30 minutter (den første kontrol et par sekunder efter starten, straks, hvis sidste gang programmet havde lukket for en fejl) programmet læser alene, uden at bede om noget, den sidste udgivelse. Hvis versionen er nyere hente den bærbare pakke egnet til systemet og erstatter programmet filer på stedet: dem, der er i brug omdøbes *.old- update og slettes ved næste genstart. Indstillinger, arkiv, datamappe og C ALL3. TXT er ikke rørt. Den nye version er aktiv fra næste start: på installationen færdig et vindue foreslår at genstarte med det samme; for hastende rettelser, markeret [obligatorisk] i Release, genstart kan ikke udsættes. Kontrolstatus og version i brug er skrevet i bunden af statuslinjen; at slukke opdateringer = 0 i LIVETROPOVHF.ini. På Mac app 'en er underskrevet og ændrer ikke sig selv: programmet kun rapporterer, at der er en ny version.

LIVETROPOVHF billedet med versionsnummeret vises i starten; i slutningen af programmet beder om bekræftelse og tak for brug.

## 12 ter. Fejllog og statuslinje

Enhver uforudset fejl ender i Data / errori.log, selv med beregningen log off: dato og tid, version, system, åbne fanen, komplet fejl og sidste linjer af, hvad programmet gjorde. En grænsefladefejl lukker ikke længere programmet: i statuslinjen vises "anno annotated error" (klik: åbne registreringsdatabasen). Ved næste start spørger programmet, om du skal sende rapporten: åbner browseren på et GitHub signal, der allerede er kompileret, med nominering, locator og brugernavn på den PC fjernet, som du tjekker og sender med en gratis GitHub konto. Loggen slutter også med hver opdateringstjek.

I statuslinjen klikkes kildeboldene (DX-klyngen, RBN, PSKReporter, MQTT, UDP) og bringes til kilden; tegneserien siger, hvorfor en kilde er slukket: afvist port, klyngeknude på serversiden, ingen respons, eller UDP lytter, men ingen pakker fra WSJT- X, JTDX eller MSHV. Et klik på versionsnummeret straks kontrollere opdateringer.

## 13. Platforme

Eksterne bogreoler: Avalonia for [12] interface, MQTTnet som en MQTT klient for real- time stream af PSKReporter [13], Microsoft. Data.Sqlite til [14] -arkivet.

C # on.NET 10 (LTS) [15] med Avalonia interface, udgivet som en uafhængig eksekverbar for Windows 64 og 32 bits, Mac Intel og Apple Silicon, Linux Debian og Raspberry Pi (64 og 32 bits). Noterede grænser: Windows 10 / 11; macOS 14 eller senere, med apps til at underskrive eller åbne højre → Åbn; Hindbær fra Pi 2 / Zero 2 W op (ARMv6 modeller er ikke understøttet af.NET).

## Tak.
Takket være I5MZY Massimo Zannoni til test på Windows 7 og Windows 8.

Luftveje: ITU-R P.453, digitale produkter fra Study Group 3 (© ITU), der anvendes til personlige og ikke-kommercielle formål.

## Vigtigste kilder

1. S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz og op", præsentation på 41. Nordisk VUSHF møde, Skjeberg 2019. Troposferiske tilstande, tjekliste for begivenheder (Hepburn / F5LEN prognose, højtemperaturprofiler med Windy, DX klynge som bevis for åbning), observation af, at kun prognosekort er ikke nok til ekstreme ruter.
2. J. N. Gannaway G3YGF, "Troposfærisk Scatter Formering", QST November 1983 (fra den eponyme artikel om radiokommunikation, RSGB, august 1981). Formler for tab af fri plads og diffusion, tilsvarende radius 8497 km, anlægstid på 10 dB pr. horisont, klimatid 0.2 · (N − 310), anlægskapacitet og signal- / støjforhold, f.eks. ved 144 MHz.
3. W. R. Hepburn, DX Info Centre, "Tropospheric Ducting Prognose" (dxinfocentre.com / tropo.html) og "The Hepburn Tropo Index (HTI)". Indeks skala og værdi betydning, forudsigelse på 6 dage.
4. F5LEN, prognose for Europa (tropo.f5len.org / prognose- for- European: liste over billeder i sidescript, class legende... Duct i billedet) og verdens prognose (tropo.f5len.org / WW) og bemærk "Ny version af den verdensomspændende VHF troposfærisk formering prognose værktøj". Indeks baseret på reaktivitet i enhed N, 40 skridt fra 3 timer, opdatere 4 gange om dagen.
5. mqtt.pskreporter.info, distribution MQTT i realtid af reklamer pskreporter.info (kurateret af M0LTE, flow af P. Gladstone bruges med tilladelse). Dørene 1883- 1886, pskr / filter / v2 topic scheme.
6. Projekt resumé af 8 / 11 / 2026 og Go test (core.go, gui.go) leveret af forfatteren af programmet. Ad Regler, negativt filter, båndbredde segmenter, telnet klyngehåndtering, temaer, sprog og portabilitet.
7. C ALL3. TXT, nomineringsdatabase → JTDX udlejer (I. UA3DJY, A. ES1JA script, JTAlert / hamspots.net data). Filformat og optælling af navne verificeret i projektfilen.
8. WSJT- X, kildekode, NetworkMessage.hpp fil (beskrivelse af UDP protokol: Heartbeat, Status, Decode). Generel reference, ikke vedlagt; JTDX og MSHV anvender samme format.
9. Reverse Beacon Network (reversebeacon.net), service sider af telnet node. Havne 7000 (CW, RTTY) og 7001 ( FT8), login med navnet kun.
10. D. Mills, RFC 4330 "Simple Network Time Protocol (SNTP) Version 4", IETF 2006. Pakkeformat, affaldsberegning og forsinkelse ved returnering. Generel henvisning, ikke vedlagt.
11. NTP Pool Project (ntppool.org); INRIM, NTP time service (ntp1 / ntp2.inrim.it); PTB (ptbtime1.ptb.de). Servere brugt til tidskontrol. Generel henvisning, ikke vedlagt.
12. Avalonia UI, officiel dokumentation (docs.avaloniaui.net). Multiplatform interface, temaer og layout fra højre til venstre. Generel henvisning, ikke vedlagt.
13. MQTTnet,.NET bibliotek for MQTT (github.com / dotnet / MQTTnet). Klient brugt til PSKReporter flow. Generel henvisning, ikke vedlagt.
14. SQLite (sqlite.org) og Microsoft. Data.Sqlite (learn.microsoft.com). Arkiver i en fil. Generel henvisning, ikke vedlagt.
15. Microsoft,.NET 10 - støttede operativsystemer og autonome udgivelse i en fil (learn.microsoft.com / dotnet). Platforme og grænser som angivet i afsnit 13. Generel henvisning, ikke vedlagt.
16. ECMA- 376 "Office Open XML File Formats", del 1, afsnit 22.1 (Office Math Markup Language). Strukturen af.docx-filen og ligninger. Generel henvisning, ikke vedlagt.
17. Liste over de 28 sprog YA- MS wiki, leveret af forfatteren af programmet.
18. Open- Meteo, vejrudsigt API (open- meteo.com), med data fra NOAA GFS model. Tryk på havoverfladen for isobare (se dokument 2).
19. J.P. Snyder, "Kortfremskrivninger - en arbejdsmanual", USGS Professional Paper 1395, 1987, og standard polynomial transformation metoder til georeferencing billeder. Generel henvisning, ikke vedlagt.
20. The Astronomical Almanac, sektion D, "Lave præcisionsformler til Månen" (præcisionsposition på ca. 0,3 °). Bruges til at genkende EME forbindelser. Generel henvisning, ikke vedlagt.
