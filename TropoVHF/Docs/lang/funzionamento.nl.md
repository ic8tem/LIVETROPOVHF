LIVETROPOVHF · technisch document 1
# Werking van het programma

> Automatische vertaling door het Italiaans: bij twijfel is het de Italiaanse gids waard.

Wat het programma doet, van waar het de gegevens neemt en hoe het ze verandert in wat je ziet op de kaart en in de lijst. Versie van 9 november 2026, gerelateerd aan het project in C# (.NET 10 + Avalonia).

## Samenvatting

- Doel: Tropo-voorspelling (Hepburn [3], F5LEN [4], isobare GFS [18]) en echte observatie van banden in hetzelfde venster, volgens de LA0BY [1] controlelijst.
- Gegevensverzameling: twee DX cluster, Reverse Beacon Network [9], dm4x cluster en PSKReporter [5] MQTT flow, UDP decodering van WSJT-X, JTDX en MSHV [8].
- Spot: bandsegmenten, negatief filter, luchtscatter in gestreept geel, positie door locators of C ALL3.TXT [6] [7].
- Wie luistert: donker roze cirkels met roze lijnen altijd zichtbaar naar de stations voelde [5].
- Spot card: afstand, azimut, tropo index op het pad en troposcatter verlies met de G3YGF [2] methode.
- Languages Hepburn en F5LEN: [3] afbeelding en kalibratiebank [4].
- Kijk: NTP-besturing op 10 servers elke 3 minuten [10] [11].
- Verdachte rapporten: EME in Q65/JT65 erkend met de positie van de Maan, afgeknotte locators, skimmers, quarantaine en luisteraars, met regels alleen geleerd [20].
- Criteria voor teruggooi en betekenis van de kleuren samengevat in twee tabellen (secties 11 ter en 11 quater).
- Thema's 36 [17] talen, export documenten naar Word met bewerkbare formules [16], draagbare programma en multiplatform [12] [13] [14] [15].

## 1. Doel

Het programma plaatst twee dingen in een enkel venster die meestal op verschillende sites kijken: voorspelling van de troposfer propagatie (Tropo index van Hepburn en F5LEN, isobare) en observatie van wat er echt gebeurt op bands (spots van DX cluster, Reverse Beacon Network en PSKReporter, plus de lijst van stations die luisteren). De referentieband is 144 MHz; de spots verzamelen zich vanaf 50 MHz.

De instelling volgt de controlelijst voorgesteld door LA0BY voor extreme tropo gebeurtenissen: in de voorgaande dagen kijk je naar de Hepburn en F5LEN voorspellingen en de temperatuur profielen op hoogte, terwijl het evenement is op de DX cluster en ter plaatse kaarten het bewijs dat de opening begint [1]. LA0BY merkt ook op dat de enige Hepburn/ F5LEN kaarten niet volstaan om de langste verbindingen te voorspellen [1]

## 2. Het venster en de tabbladen

Het venster is één en wordt niet opgesplitst in mobiele vensters. Van boven naar beneden:

- Algemene instellingen rij: Call en Locator aan de linkerkant; aan de rechterkant de UTC klok (bijv. 08/11/2026 20:33:38 UTC, amber tekst) gevolgd door NTP:OK of NTP:FAILED, het thema menu en dat van de taal.
- In vijf groepen: Kaart en plaats; Voorspellingen (Propria β, de prognose berekend door het programma en beschreven in de Forecast Manual, dan Hepburn en F5LEN); GRAVES; Gegevens en bronnen (DX cluster, PSKReporter, Reverse Beacon, UDP, Afspraken en verdachte rapporten); Gidsen (Programma Operatie, Kaart Gebouw, Hoe vorm te geven aan de tropo, Handleiding voorspelling, Tips aan de operator, met een zoekvak dat telt waarin de tekst verschijnt). Onder de balk toont een balk de subtalen van de groep; elke groep herinnert zich de laatst gebruikte.
- Inhoud van het tabblad en, onderaan, de statusbalk met de status van de bronnen, de database met namen en het tijdstip van de laatste update.

De bovenste regel instellingen gelden voor alle tabbladen: het gekozen thema kleurt ook de gegevensverzameling en achtergrond van documenten; Documentpagina's blijven zo wit als een vel, zodat ze goed afdrukken.

## 3. Telefoon en verhuurder

De Maidenhead verhuurder kan 4 of 6 karakters hebben (bijv. JN70 of JN70CN). Het programma zet het om in de coördinaten van het midden van het vierkant: veld van 20° × 10°, vierkant van 2° × 1e, subquadrate van 5′ × 2,5′. Als de tekst geen geldige locator is, wordt de rand van het vak rood en verschijnt het station niet op de kaart. De locator dient ook als een centrum van azimut projectie en als uitgangspunt voor afstanden en azimut in het reclamebord.

De Call is de naam die gebruikt wordt als cluster login. Bij de eerste start, totdat er een geldige nominatie, de clusters niet verbinden: een venster vraagt om de naam (en optioneel de verhuurder); bevestigd, de naam wordt gekopieerd als een gebruikersnaam in het Login veld van elke cluster die het leeg heeft en clusters verlaten. Met MQTT, UDP en NTP maken geen gebruik van login en vertrekken toch.

## 4. Verzameling van gegevens: bronnen

Elke bron heeft een eigen sectie in het tabblad Gegevensverzameling, met Connected/Disconnected, de Herkenbare en Verbonden dozen bij het opstarten, status, meters en registratie van de ontvangen rijen. Ongebonden of afgedankte lijnen uit het filter verschijnen niet in het register: tel alleen de sectiemeters. De secties worden met twee tegen twee geflankeerd.

| Bron | Verbinding | Wat brengt |
| 1. DX clusterkeuze | on0dxk.dyndns.org:8000 commando set/filter dxbm/pass 6 | Spot van amateur radio's, ook SSB en CW. Op ON0DXK worden de VHF gefilterd en ingeschakeld met het aangegeven commando (poi pass 4, 2....). |
| 2. Tweede bron | dxmaps.com:7300 (DXMAPS, EA6VQ) en s50clx.si:41112 ( S50CLX, Slovenië) | DXMAPS verzamelt VHF-spots en geeft ook RBN skimmerspots (set/skimmer control, al ingesteld); S50CLX is een DX Spider cluster. Log in met uw naam; bewerkbare adressen. |
| 3. Omgekeerd Beaconnetwerk | telnet.reversebeacon.net 7000 CW/RTTY · 7001 FT8 | Automatische skimmer spot. Log in met de naam alleen [9]. |
| 4. Cluster met PSKReporter | dm4x.ddns.net:8500 | Digitale spots van PSKReporter in clusterformaat. |
| 5. PSKReporter MQTT | mqtt.pskreporter.info 1883 · 1884 met TLS | Elk real-time ontvangstrapport: wie zendt, wie luistert en hun [5] Locators. Band 144, 222 en 432 MHz. |
| 6. Weer- en tropokaarten | dxinfocentre.com/tropo_eur.html tropo.f5len.org/forecasts-for-European model GFS | Hepburn [3] en F5LEN [4] tropo-indexkaarten, zeeniveaudruk voor isobare (GFS via Open-Meteo [18]). Hepburn en F5LEN moeten aan de auteurs worden toegestaan. |

Cluster telnet (bron 1:4). Na de verbinding wacht het programma op het naamverzoek (tekst die login bevat, bel:, oproepteken of uw oproep); als het niet binnen 4 seconden aankomt, stuur dan de login. Stuur dan de commando's van de sectie, gescheiden van punt en komma, één per 0,6 seconden. De telnet controle karakters worden verwijderd voor het lezen van de regels. Als de verbinding valt en Riconnetti actief is, probeer dan opnieuw na 10 seconden (15 als de server helemaal niet reageert) [6].

PSKReporter MQTT (bron 5). Het programma schrijft zich in in het onderwerp pskr/filter/v2/{banda}/{modo}/{tx_call}/{rx_call}/{tx_grid}/{rx_grid}/{tx_dxcc}/{rx_dxcc} alleen de band bevestigen en de andere velden vrij laten met het karakter + [5]. De exacte naam waarmee de server de banden aangeeft (bijvoorbeeld 2m, 1,25m, 70cm) moet bij de eerste verbinding worden gecontroleerd.

## 5. Van de clusterlijn naar de plek

Een regel is een plek als het de vorm DX de SPOTTER heeft: frequentie DX commentaar hhmmZ. Het programma ontleent spotter, station DX, frequentie in kHz, commentaar en nu UTC; de namen verwijderen het achtervoegsel van skimmers (-#, -1...). Vervolgens gelden in deze volgorde de volgende regels [6].

| Band | Segment geaccepteerd |
| GRAVES (met 144) | 143,040 MHz |
| 144 MHz | 144 MHz |
| 222 MHz | 220 MHz (alleen regio 2 ITU, Amerika) |
| 432 MHz | 420 450 MHz |
| 1296 MHz | 1240 MHz |
| 2320 MHz | 2300 2450 MHz |
| 10 GHz | 10,5 GHz |

1. Band. De frequentie moet vallen in een van de beheerde segmenten; alle rest, en in het bijzonder elke commerciële onder 49 990,0 kHz, wordt niet getoond en telt als "uit band."
2. Echt wel. Een van de bekendste manieren van commentaar is FT8, FT4, Q65, JT65, ISCAT, CW, SSB, FM, RTTY, PSK31, MSK144, FSK441, JT6M); USB en LSB worden SSB.
3. Negatief filter. De spot wordt verwijderd als deze de modi MSK144, FSK441, JT6M, JTMS, ISCAT of de tags <AU> <AUE> <ES> <MS> < FAI> < F2 > bevat, met niet-nucleaire uppercases en zelfs spaties tussen haakjes. Dit zijn manieren en voortplantingen die niet tropisch zijn: Scatter meteoriet, aurora en sporadisch, F2.
4. Luchtscatter. Als het commentaar <AS> of <ACS> bevat (ook gemengd, bv. <As>) of de plek verklaart als luchtscatter (luchtscatter, luchtscatter, vliegtuigscatter), blijft de plek in het geel getekend met stippellijn (donkere slip om leesbaar te blijven op de duidelijke kaart) en gemarkeerd in de lijst.
5. Plaats van de stations. Als er in het commentaar twee locators zijn (bijv. JN54 < JN83) dan is de eerste van degenen die de plek maken en de tweede van het station DX; met één locator, dat is van DX station. Wat ontbreekt wordt gezocht in C ALL3. TXT, alleen met de exacte naam (voor laptops zie hieronder). Als een station zonder positie blijft, verschijnt de plek in de lijst met de note
6. Dubbel. Een commercial met dezelfde DX, dezelfde spotter, dezelfde band en dezelfde bron van een reeds aanwezige vervangt het, in plaats van toe te voegen.

C ALL3.TXT is de naam database → locator gebruikt door JTDX, in het formaat CALL, LOCATORE,,,,. Het projectbestand bevat 1 048 847 namen, allemaal met een geldige locator; de header van het bestand spreekt nog steeds van 34 323 nominaties omdat het die van de eerste editie [7] bleef. Het wordt bij het opstarten in de achtergrond geladen, zodat het venster direct bruikbaar is. Het bestand wordt doorzocht naast het programma, in de Data map, in de map waaruit het programma is gestart en een of twee mappen hierboven; Als er geen, een venster vertelt het bij het opstarten en de statusbalk signalen in rood. Zijn naam gebruikt altijd zijn 6-karakter. Alleen regel voor de locators: als de ontvangen (spot, MQTT, decoderen, QSO desunti) heeft minder dan 6 karakters en C ALL3. TXT heeft een tot 6, het is altijd de moeite waard de C ALL3.TXT; voor laptops (F/ IC8TEM, IC8TEM/P) is slechts een exacte stem: zonder, het station blijft zonder een verhuurder en is niet elders ontworpen. De regel is ook van toepassing op het moment van tekenen, dus de gegevens die zijn aangekomen voordat de database werd geladen zal zelf draaien.

Eén database: C ALL3.TXT. Van 1.3.4 alle locators zijn in één bestand, C ALL3.TXT, die blijft in het formaat van JTDX en WSJT-X (CALL, LOCATORE,,,,) en kan worden gebruikt als het ook in WSJT-X, JTDX en MSHV. Er zijn geen bestanden meer apart. Aan de bovenkant van het bestand, na de header, zijn er twee blokken gemarkeerd door een commentaarregel: // [LIVETROPOVHF-MANUALI] met de vermeldingen ingevoegd met de hand en // [LIVETROPOVHF-CLUSTER] met de correcties uit de cluster; dan // [LIVETROPOVHF-FONTI] met alle andere items in alfabetische volgorde. Elke nominatie verschijnt eenmaal. In de sublingual Data and Sources › Afspraken (C ALL3) zoekt u een naam, schrijft u de verhuurder en drukt u op Toevoegen / wijzigen: de stem gaat in het blok van de stemmen met de hand en het is vooral de moeite waard, zelfs over de locator die het station zelf stuurt.∫ Verwijdert de nominatie van C ALL3.TXT (de volgende update hervat het van bronnen, als ze het hebben). Voeg een bestand toe... lees een lijst CALL, LOCATOR (u accepteert ook het punt en komma, tabulatie en ruimte) en schrijf het tussen de hand gekozen; het gekozen bestand wordt niet gekopieerd. Herlaad C ALL3.TXT van de schijf, na het bewerken ervan met een editor. Volledige volgorde van bronnen: eigen locator, handstemmen, clustercorrecties, de rest van C ALL3. TXT (bronnen, geleerde live locators en G7VRD), ontvangen locator (die alleen geldig is over de database als het 6 tekens heeft en niet eindigt met LL of MM).

Update van C ALL3.TXT. In dezelfde subtaal downloadt de knop Update nu de bronnen (C ALL3.TXT van WSJT-X Verbeterd, C ALL3.TXT EME van W7GJ, grid_data.bin van JTDX), voegt zich bij hen (de eerste die een nominatie win), controleer of ze minstens 50.000 namen (anders verandert niets), slaat de C ALL3. TXT De vorige C ALL3. Er blijven TXT ingangen over, en er blijft een 6-karakter locator over waar een bron slechts 4 van hetzelfde vierkant heeft. Handstemmen en correcties van de cluster blijven aan de top, onveranderd. Met de Update doos op zijn eigen elke dag, de check vindt plaats aan het begin. Het programma herschrijft C ALL3. TXT direct na elke stem met de hand en, voor geleerde locators, maximaal elke 10 minuten en sluiten; de C ALL3. TXT van het pakket is alleen-lezen en het attribuut verwijdert zichzelf. Linux installateurs overschrijven geen C ALL3. TXT al aanwezig; dat voor Windows installeert het alleen als het ontbreekt. ALL3

Automatische correcties van de cluster. In de DM4X en RBN automatische spots brengt het commentaar twee locators met zich mee, bijvoorbeeld KN09tp<> KN09: het eerste is wat het station dat je spot van zichzelf verklaart, tot 6 karakters; het tweede is dat van het station gevoeld, vaak alleen op 4. Als de eerste 6 tekens heeft (en niet eindigt met LL of MM) en verschilt van die van de database, corrigeert het programma C ALL3. TXT alleen: de correctie gaat in het blok // [LIVETROPOVHF-CLUSTER], met de datum van de laatste bevestiging in het derde veld, is geldig boven de bronnen (onder de handstemmen) en verschijnt tussen de verdachte signalen bijgewerkt als "C ALL3." Dus het overleeft C ALL3. TXT-updates. Een onbevestigde correctie gedurende twee jaar verliest voorrang en blijft een normale stem.

Laatste bron: G7VRD. Als een kandidaat geen 6-karakter heeft in C ALL3. TXT, het programma vraagt het op de achtergrond aan de publieke API van G7VRD (api.g7vrd.co.uk, Je accepteert alleen een 6-teken dat niet het midden van het vierkant is (LL of MM); de verzoeken worden gegroepeerd, maximaal 40 nominatieven per minuut, en elke nominatieve vraagt eenmaal per dag, omdat het een hobbistische dienst is zonder garanties. De gevonden locator gaat C ALL3 binnen. TXT als de naam er niet is of slechts 4 tekens van hetzelfde vierkant heeft; de zoekopdracht wordt uitgeschakeld met het vakje in het naamtabblad.

Afspraken met de bar. In de PSKReporter MQTT onderwerpen, de balk scheidt de niveaus, dan komen de samengestelde namen met een punt in plaats van de balk ( IV3. HB9CAT). Het programma plaatst de balk terug en, als het bericht de volledige namen bevat, gebruik deze: IV3/HB9CAT blijft IV3/HB9CAT op kaart, lijst en database.

Locators hebben live geleerd. C ALL3. TXT bevat niet alles: bijvoorbeeld SP9EMF en SP9NVY zijn er niet, en dan blijft de 4-karakter locator verzonden door PSKReporter (de kaart zegt dat). Om deze gevallen te verminderen schrijft het programma in C ALL3. TXT elk 6-teken dat afkomstig is van elke bron (PSKReporter, zoals zender of als ontvanger, cluster, UDP decodering) voor een nominatie die C ALL3. TXT heeft geen of slechts 4 tekens in hetzelfde vierkant; verandert nooit een stem in 6 tekens. Tot 1.3.3 waren deze locators in Data/ LOC6.TXT.

Overgang van 1.3.3. Bij het eerste begin van de 1.3.4 de bestanden, afgezien van de vorige versies (Dati/C ALL3 -MANUALE.TXT, de Data/C ALL3 -AGGIUNTE, Data/C ALL3 -CLUSTER. TXT, Data/ LOC6.TXT en Data/C ALL3 - G7VRD.TXT) worden samengevoegd tot C ALL3. TXT Schrijven loslaten, oude bestanden nemen de.fuso extensie, je niet meer lezen en je kunt verwijderen.

Automatische ontvangers. RBN, dm4x en PSKReporter rapporteren vaak namen met een achtervoegsel dat de ontvanger aangeeft: -SDR, /SDR, -# (skimmer RBN) of -1, -2... (plus ontvangers van hetzelfde station). Voor de verhuurder wordt het achtervoegsel verwijderd: OK2OZL -SDR neemt OK2OZL JN89UE van C ALL3.TXT als het station nog geen 6-karakter stuurt. De getoonde naam blijft degene met het achtervoegsel.

Locators gesneden. Niet alle bronnen behandelen de uitbreiding van hun verhuurder: velen stellen het op 4 karakters, en sommige automatische carry-over systemen doelbewust afgekapt de 4-karakter locator, zelfs degenen die de gegevens verkrijgen van het echte luisteren van FT8, FT4, Q65 en JT65, of voltooien het met het centrum van het vierkant (LL of MM, bijvoorbeeld JN89MM). Om deze reden wordt een 4-teken, of 6-teken dat eindigt met LL of MM, vervangen door de C ALL3. TXT van hetzelfde vierkant. Als de juiste locatie en het centrum van het plein niet samenvallen met een paar dozijn km, blijft de fout klein in vergelijking met de tropo afstanden.

## 6. Wie luistert er?

Elk MQTT bericht van PSKReporter rapporteert de naam en locator van degenen die [5] ontvangen, dus je hoeft er niet naar te zoeken in C ALL3.TXT. Voor de band gekozen in de selectie het programma heeft, voor elk ontvangststation: verhuurder, laatste ontvangst, gedecodeerde manieren, aantal stations vilt en maximale afstand. Een station verlaat de lijst wanneer het niets meer ontvangt binnen het gekozen bereik (3, 5, 10, 15, 30 of 60 minuten).

Op de kaart is elke ontvanger een donkerroze lege cirkel, en vanaf elke start volledige roze lijnen richting de stations die voelen en stuurt terug naar het net. De lijnen worden altijd gezien, zonder te klikken. Afspraken van luisterstations, hoorstations en de twee hoofden van elke plek worden altijd op de kaart geschreven, zonder te hoeven klikken (de zelfde naam wordt eenmaal geschreven). Door op een cirkel te klikken worden de lijnen nog dikker en opent het tabblad met details. Als de locator van MQTT minder dan 6 tekens heeft, gebruikt het programma dat van C ALL3.TXT (ongeveer een miljoen namen), mits het overeenkomt met het ontvangen vierkant.

Scatter meteorieten. Net als bij clustervlekken worden MQTT ratio's in MSK144, FSK441, JT6M, JTMS of ISCAT alleen weggegooid en geteld; hetzelfde geldt voor UDP decoderingen op deze manieren.

Geen echte luisteraars. Sommige stations verklaren 144 MHz te ontvangen, maar in werkelijkheid is de ontvanger op een andere band (verkeerde frequentieindicatie): hun rapporten zouden onbestaande openingen van duizenden km ontwerpen. Het programma controleert het zelf. Elk station dat een luisteraar zegt te horen in VHF wordt 30 minuten gevolgd op alle bands, met een speciale MQTT inscriptie (pskr/filter/v2/+/{nominative}/#) [5]. dienen als bewijs. Als ten minste twee van de door een luisteraar gehoorde stations in dezelfde minuten (±10) actief zijn op andere bands, wordt de luisteraar als niet echt beschouwd en worden alle VHF-rapporten genegeerd; de MQTT-register meldt de naam en de stations die hem verraden hebben. In de lijst van plaatsen, naast de meters, toont de MQTT-knop de staat van de stroom (groen blad indien verbonden, rood indien niet) en hoeveel luisteraars werden weggegooid in de band en het gekozen bereik; door te klikken opent het tabblad met de lijst van teruggooien en, voor elk, stations gevonden op andere banden.

Lange verbinding China. Een verbinding van meer dan 1000 km (Clumbing Spot, MQTT Listening, QSO Desunti) wordt niet onmiddellijk getoond: blijft gehouden, en de teller gehouden > 1000 km Het wordt onthuld wanneer een andere verbinding, tussen verschillende stations en ten minste de helft langs, bevestigt met een van deze criteria: hetzelfde station aan een hoofd (bijvoorbeeld dezelfde luisteraar) en het andere station in dezelfde richting binnen 10°; soortgelijke gang, met beide hoofden binnen 200 km van die van de andere verbinding; parallelle route, met richting binnen 10° en zijafstand binnen 200 km, overlappen langs de route. Beide verschijnen en de MQTT log toont de reden. De UDP-decoderingen zijn altijd zichtbaar en gelden als bevestiging voor anderen. De bewaarde verbindingen blijven 60 minuten in het geheugen.

Niets onder 144 MHz. Het programma werkt vanaf 144 MHz omhoog: alles onder 143 MHz wordt weggegooid. De uitzondering is GRAVES, de Franse radar die in CW op 143.050 MHz door Broye-lès-Pesmes ( JN27SI) wordt uitgezonden: een commercial tussen 143.040 en 143.060 MHz, of met GRAVES in de naam, verschijnt tussen 144 MHz met radar als zender. Het reclamebord zegt dat als de ontvanger zich in de straal (sectoren D, C, B, A van 90° tot 270° van azimut gezien vanaf de radar, 6 stappen van 7,5° elk) of uit; het tabblad GRAVES, zoals in YA-MS, toont de lucht gezien vanaf de radar: sectoren, stap verlicht op dat moment (volgorde PE1ITR 2021: 0,8 s per stap, cyclus 4,8 s Data en geometrie komen van YA-MS. Het gevoel in de trope is moeilijk, maar het kan gebeuren.

Rehabilitatie van menselijke clusters. Een station dat ook op andere bands verschijnt telt niet als verdachte als, in dezelfde minuten (±30), de DX menselijke clusters ( ON0DXK, GB7RAU; niet dm4x, RBN of andere automatische bronnen) de actieve schade aan de band in kwestie, op plekken gemaakt door andere namen: het is het geval van stations die echt werken op meerdere bands samen. Zo keert een afgedankte luisteraar zichtbaar terug (het MQTT-register schrijft "gerehabiliteerd" en kan een lange-held link voor lage bands worden onthuld.

Actieve stations onder 144 MHz. In de quarantaine van lange verbindingen, een station dat in dezelfde minuten wordt gehoord op banden onder 144 MHz (uit de MQTT rapporten van de andere banden) bevestigt geen andere verbindingen en wordt niet onthuld, hoewel een soortgelijke of parallelle route zou bevestigen.

## 7. Lijst van commercials

Het beslaat 35% rechts van de kaart tab en spot. Top: titel, band selector (50, 70, 144, 222, 432, 1296, 2320 MHz, 10 GHz), range selector (laatste 3, 5, 10, 15, 30, 60 minuten) en Update toets. Hieronder, vier meter: zichtbare plekken, AS/ACS, verwijderd uit het filter, stations luisteren.

Elke lijn toont het type lijn, de tijd UTC, DX → DE, de weg, de afstand in km en, op de tweede lijn, de bron, frequentie en commentaar. De meest recente staat bovenaan. Een klik op de lijn selecteert de plek: op de kaart groeit de lijn en de twee namen verschijnen.

## 8. Spot kaart en troposcatter verlies

Voor de geselecteerde plek toont het tabblad locators, afstand, azimut, frequentie, modus, bron, nu commentaar, de gemiddelde tropo-index langs het pad en een schatting van het routeverlies voor verspreide tropo.

Afstand en azimut worden berekend op de maximale cirkel met Aarde radius van 6371 km (formule van de emisenoverso voor de afstand, initiële azimut van het DX station tot de spotter).

Verlies troposcatter. Het fundamentele verlies, voor twee stations op gladde grond, is de som van het verlies van vrije ruimte en diffusieverlies: G3YGF [2]

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

De verliezen zijn in dB, D is de afstand in km, f de frequentie in MHz, θs de diffusiehoek in graden.

8497 km is de equivalente straal van de aarde (4/3 van de fysieke straal). θA en θB zijn afhankelijk van de horizon van elk station:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

met Δh hoogte van het obstakel dat vanaf de horizon met betrekking tot het station en de afstand. Elk niveau van horizon kost ongeveer 10 dB [2]. Het programma kent de horizon van de stations niet, dus het toont het fundamentele verlies met θA = θB = 0 en verklaart het ("smooth ground"). G3YGF voegt een klimaatterm toe, waarbij N de reactiviteit is van de [2] -grond; het wordt gebruikt wanneer N beschikbaar is van het weermodel:

```math
L_{N} = 0,2 \cdot (N - 310)
```

Verificatie: voor 110 tot 144 MHz geeft de formule 166,5 dB, tegen ongeveer 168 dB bedden op de kaart van het artikel. Voor 500 km tot 144 MHz geeft ongeveer 206 dB. De verwachte signaal/ruisverhouding is de capaciteit van het systeem (eirp + effectieve gevoeligheid van de ontvanger) minus het verlies: met het G3YGF 144 MHz modelsysteem (198 dB) zou een 500 km-pad ongeveer −8 dB geven in SSB [2], dat is de verbinding is alleen met een betere verspreiding van de zuivere scatter.

## 9. Mijn decoderingen: UDP van WSJT-X, JTDX en MSHV

WSJT-X, JTDX en MSHV kunnen elke regel die ze decoderen wegsturen. Het programma luistert op 127.0.0.1:2237 en herhaalt hetzelfde pakket, byte per byte, op 127.0.0.1:2238, dus de andere programma's die die poort gebruikten (GridTracker, JTAlert, een logger) blijven werken als je ze verplaatst op 2238. Adressen en deuren veranderen in de UDP sublingual van de Data Collection; als luisteradres kun je ook een multicast groep gebruiken (bijv. 224.0.0.1) als het decoderingsprogramma zo ingesteld is.

Pakketten volgen het WSJT-X netwerkprotocol: een eerste magische nummer 0xADBCCBDA, schemanummer en berichttype. Het programma maakt voornamelijk gebruik van drie [8] -types:

- Heartbeat: zegt welk programma de data uitzendt en met welke versie.
- Status: frequentie van de wijzerplaat en -modus; van de frequentie krijg je de band (met dezelfde tabel van de vlekken).
- Decoderen: Nu, SNR, tijd- en frequentieverspilling, manier en tekst van het gedecodeerde bericht.

Uit de tekst krijg je het station dat je hoorde: in CQ 9 A2PQR JN83 is het station na CQ (bespaart DX, EU, TEST...), in IK0JKL IT9MNO JM68 is de tweede, d.w.z. die uitzendt. De locator is die van het bericht, als er (4 tekens, anders dan RR73); anders zoek je in C ALL3. TXT. Het zijn luisterlijnen: stations die je ontvanger nu hoort. Op de kaart worden ze rode lijnen van uw QTH naar het station, met de naam; de laag verlicht met de gedecodeerde doos van mij en vereist een geldige locator. In de lijst rechts staat hoeveel stations u decodeert op de gekozen band.

QSO desunti dal live. Wanneer een decodering een sluitbericht is in het formulier C ALL1 C ALL2 RR73 (of 73 of RRR), bijvoorbeeld IC8TEM IK3FHP RR73 of IK3FKP SV8PEX 73, hebben de twee stations een koppeling voltooid. Het programma registreert het als QSO, met de locator van elk genomen van het laatste bericht waarin hij verklaarde het of, als er geen is, van C ALL3.TXT [7], en tekent het op de kaart als een groene lijn tussen de twee stations (strate.QSO desunti dal live Een paar stations tellen één keer per band; de band en het bereik in de lijst zijn geldig.

In dezelfde subtaal toont het Debug-paneel de tekst van elk ontvangen pakket (type, tijd, waarden en bericht), op verzoek ook hexadecimale bytes, en een regel voor doorsturen. Het is om te controleren of het decoderen programma daadwerkelijk de gegevens verstuurt.

## 9 bis. Taal Hepburn en F5LEN

Elk toont wat het programma uit de bron haalt. De knop Voor elke afbeelding is er een doos om het uit te sluiten en een veld met de verwachte tijd. De 31 Europese beelden van F5LEN worden genoemd door de tijd van de modelrace (03.png... 48.png elke 3 uur, 54... 96 elke 6 uur, 108... 192 elke 12 uur) en het programma neemt de tijd van de naam van het [4] bestand; voor Hepburn is het de volgorde van de pagina. In het F5LEN tabblad kiest een menu de regio: Europa (tropo.f5len.org/forecasts-for-europe), Australië (tropo.f5len.org/Australië, vk03.png... vk192.png) of de Atlantische Oceaan tussen Kaapverdië en Ierland (tropo.f5len.org/Atlantic-ocean/Atlantic-d4-ei, atl03.png... atl192.png). Iedereen heeft dezelfde stappen. [3] F5LEN [4]

Hieronder is de kalibratieteller. Een klik op de afbeelding voegt een controlepunt (locator of lat,lon: het duurt minstens 6) of een kleur van de legende met zijn waarde. Het programma berekent de pixel matching → coördinaten met een tweedegraads polynomiale interpolatie tot de minimale vierkanten [19], leest de kleuren met de tolerantie gekozen en, voor F5LEN, zet de eenheden N in de schaal 0 Opslaan calibratie en test lezing schrijft het bestand in de Tarature map en controleert op welke pixel uw locator valt. Alvorens een kalibratie in te stellen, dient toestemming te worden gevraagd aan de auteur van de kaarten.

Regio's. Elke bron heeft meer regio's, elk met zijn vaste-naam afbeeldingen: Hepburn tr_map/fcst/{code}{ore}.png?v{data} (eur Europa, nwe Noordwest-Europa, eeu Oost-Europa, ent North East Atlantic, nat North Atlantico, eam North America East, nsa South America North America, sam South America, Mid-East Asia, nca Central-Septential Asia Het tabblad van elke bron toont in de eerste plaats het voorbeeld van de fusie: alle gedownloade afbeeldingen die zijn samengesteld als op de hoofdkaart, met het gestippelde paneel en de naam van elke regio, een grijze sluier waar geen afbeelding het gebied beslaat en een schuifregelaar om de uren te scrollen; aan de linkerkant de lijst van regio's, elk met het vak om het te gebruiken, het aantal kaarten gelezen en van waar de extremen komen (OCR, met de hand of fabriek; als de OCR mislukt de tekst die hij leest verschijnt). De handmatige kalibratiebank blijft achter de homonieme knop. Het Bench Region menu kiest welke look, de box Het frame is alleen te vinden (twee horizontale lijnen en twee verticale lijnen van dezelfde kleur); om de afbeelding te serveren de uitersten lat/lon van het frame: voor Hepburn Europa (10° W N) zijn al in het programma, voor de andere regio's ze schrijven in de vier O E S N velden lezen van de as labels. De schaalvermenigvuldiger maakt de twee bronnen vergelijkbaar: eindwaarde = multiplier × legendewaarde. [3] F5LEN [4] F5LEN F5LEN

## 9 ter. Exporteren naar Word

De twee documenten hebben op de top van de Het bestand is geschreven in het Office Open XML formaat; de formules worden Word vergelijkingen (OMML), zodat ze bewerkbaar blijven met de [16] vergelijkingsbewerker. Als het opslaan venster niet beschikbaar is eindigt het bestand in de Documenten map naast het uitvoerbare bestand.

## 9 quater. Wie belt me?

Als er een bericht naar u wordt verzonden op de lokale UDP (de voornaam is van u, bijvoorbeeld IC8FAX IK3FHP JN55 of IC8FAX IK3FHP -12), dan volgt het programma van uw QTH naar het station dat u een zwarte stippellijn noemt, met de naam en de locator naast u. De locator komt van C ALL3.TXT (inclusief handstemmen) of bericht. De lijn blijft tot de UDP voorbij de sluiting van de QSO met dat station ( RR73, RRR of 73, in een van de twee richtingen), ten hoogste 3 minuten van de laatste oproep; je trekt over alles en volgt niet uitsluitingscriteria, zodat een directe oproep nooit ontsnapt.

PSKReporter, via MQTT, stuurt de tekst van de berichten niet: zegt alleen wie, met frequentie, manier en SNR ontvangen heeft. Dit is waarom een directe oproep wordt alleen herkend door zijn eigen UDP-decoderingen.

## 10. Tijdregeling (NTP)

Digitale modi en spottijd vereisen een precieze klok. Elke 3 minuten ondervraagt het programma 10 NTP-servers (SNTP op UDP 123): 0.pool.ntp.org, 1.pool.ntp.org, 2.pool.ntp.org, 3.pool.ntp.org, it.pool.ntp.org, ntp1.inrim.it, ntp2.inrim.it, ptbtime1.ptb.de, time.google.com, time.cloudflare.com [11].

Voor elk antwoord berekent de teruggooi- en retourvertraging, gooit de abnormale waarden weg en neemt de mediaan, met de berekening van teruggooi en vertraging die wordt verstrekt door SNTP [10]. Met ten minste 3 antwoorden ga je akkoord met de UTC klok van het programma uitlijnt en volgende verschijnt NTP:OK in groen; als de antwoorden niet voldoende verschijnen NTP:FAILED in rood en de klok blijft die van de computer. De systeemklok wordt niet aangeraakt, omdat beheerdersmachtigingen nodig zouden zijn; alle interne programmaschema's (spots, 5 De besturing draait altijd in de achtergrond: in de interface zie je alleen het resultaat naast de klok; servers, intervallen en minimale responsen veranderen in LIVETROPOVHF.ini (ntp_servers, ntp_minutes, ntp_min).

## 10 bis. Algemene uitsluitingscriteria

Twee vakjes in de laagbalk zijn van toepassing op alles wat je tekent (spot, PSKReporter, decoderen, QSO desunti):

- Exclusief minder dan 100 km. Het verkeer tussen stations van minder dan 100 km is mogelijk niet significant voor de tropo (grondgolf, optisch bereik): met de on-box die u niet tekent en niet in de lijst voorkomt. Het begin gaat niet door.
- Meer dan 1000 km: alleen vierkantjes. Rondom uw QTH (de locator ingesteld om te beginnen) is er een kroon van 1000 km in alle richtingen. Als slechts één van de twee stations van een link zich buiten deze kroon bevindt, wordt de verbinding niet als een lijn getrokken: op zijn plaats kleuren ze in lila, gestippeld op 45°, de locator vierkanten (4 tekens) van de betrokken stations. Hetzelfde geldt voor PSKReporter stations luisteren naar meer dan 1000 km, die verschijnen als een vierkant in plaats van als een cirkel. Tot nu toe verkeer zie je zonder het invullen van de kaart van lijnen. Zonder geldige locator telt de lengte van de verbinding. Het plein van uw QTH kleurt nooit in lila, zelfs niet wanneer u luistert naar een station voorbij de kroon: past de vaste verhuurder toe, elke naam gebruikt wie werkt vanaf het station. De kroonradius wordt gekozen naast de doos: 1000, 700 of 500 km. De doos Corone 1000/700/500 km trekt de drie kronen rond de QTH, getekend in lila, met de meest gemarkeerde keuze en de afstand naar het noorden geschreven. In de lijst van plaatsen wordt de afstand gevolgd door.. Het begin is begonnen. Het criterium geldt na quarantaine: lange verbindingen die nog steeds behouden blijven, verschijnen niet eens als vierkanten.

Wat zijn de lila vierkantjes bedekt op 45°. Het verkeer weg van uw QTH interesseert u om te begrijpen waar de voortplanting is open, maar de lijnen, honderden kilometers lang en vaak tientallen, bedekken de kaart en verbergen wat telt voor u: de openingen die u kunt exploiteren van uw station. Om die reden tekent het programma, buiten de gekozen kroon, niet de lijnen, maar kleurt het de locator vierkanten (2° × 1°) van de betrokken stations. Het resultaat is een kaart van actieve zones: waar veel nabijgelegen lilac-vierkanten verschijnen, is er een geleiding of opening, zelfs als het u niet direct aangaat; Als het gebied zich naar u uitbreidt, is het het signaal om de antenne in die richting te wijzen. Het 45° dashboard laat je zien onder de tropo index en het raster, dus ze vergelijken in een oogopslag de voorspelling (onderkant kleur) en het echte verkeer (de lila). Uw QTH-vierkant kleurt nooit, omdat het het startpunt is en niet een ver gebied. Alleen de vierkanten die allemaal voorbij de kroon zijn gekleurd: een vierkant doorkruist door de kroon, of binnen, blijft vrij, zelfs als er een van de twee stations. Om de lijnen te zien, zet gewoon de doos uit of kies een grotere kroon.

## 11. Thema's en talen

Thema's: de 18 gemeenschappelijke IC8TEM -programma's, hetzelfde als YA-NEC en E-LAYER ANALYZER: Clear, Night, Black, acht pastels (blauw, oranje, aquamarijn, groen, roze, lila, indigo, bruin), Scia meteorica (YA-MS palet), de drie van IP-TRX-CTRL (Chiaro, Notte, Nero) en Blue, Purple, Green. Vanaf 1.3.4 lijkt de Chiaro op YA-NEC: licht grijze achtergrond, witte randen, blauwe nachttabbladen en blauw accent. In donkere thema's wordt ook de basiskaart donker. De kleurschaal van de prognose blijft hetzelfde in alle thema's. Op smalle schermen (onder 1500 pixels, bijvoorbeeld 1366×768) toont de klok bovenaan alleen het uur, dus thema, cursor van teksten en taal blijven zichtbaar. Talen: YA-MS [17] wiki (English, Travel, Deutsch, English, Français, Español, Português, Dansk, Norsk, Svenska, Íslenska, Eesti, Lietuvi, Latviešu, Руский,,кра,нська, Türkçe, Vertalingen hebben betrekking op de interface; de twee technische documenten en berichten van de statusbalk blijven in het Italiaans. De namen van de dagen komen van de internationale instellingen van. NET voor de gekozen taal. [12]

Thema en taal wijzigen van de twee menu's rechtsboven, naast de klok, en onmiddellijk toepassen, zonder het programma opnieuw op te starten en zonder de verzameling van gegevens te onderbreken. De keuze wordt opgeslagen in LIVETROPOVHF.ini.

## 11 bis. Verdachte en zelflerende rapporten

Het tabblad Verdachte Verslagen verzamelt alles wat het programma controleert vindt vreemd, gegroepeerd op type en naam, met de laatste 24 uur vermeld. Het programma leert op zichzelf: voor elk koppel (type, naam) telt de positieve resultaten (+) en omgekeerd (−), en na 3 overeengekomen resultaten creëert een automatische regel; een tegenovergestelde resultaat stelt het in vraag. Gebruikersbeslissingen (Goed, Slecht) zijn altijd van toepassing; de pulsante knop geeft de beslissing terug aan het programma. De regels staan in Data/APPRENDIMENTO. TXT, naast het programma; de automatische niet versterkt voor een jaar worden vergeten.

| Type | Wanneer | Exit + / − | Regeleffect |
| EME ( Q65/JT65) | Q65 of JT65 meer dan 1000 km met de Maan boven de horizon aan beide hoofden, of Eme in commentaar | − elke keer dat de Maan vaak voorkomt | Weggooien: EME station, de lange Q65/JT65 s zijn ook uitgesloten zonder gewone maan. Goed: behandeld als een tropo |
| Lange verbinding | Meer dan 1000 km, in quarantaine | + onthuld, − verlopen zonder bevestiging | Goed: de lengtes van het station show onmiddellijk. Weggooien: nooit |
| Luisteraar | PSKReporter ontvanger die hoort actieve stations op andere banden | − weggegooid, + gerehabiliteerd door menselijke clusters | Goed: nooit weggegooid. Gooien: altijd |
| Andere verhuurder | Vierkant ontvangen verschillend van C ALL3. TXT | + elke keer dat je het herhaalt | Goed: het station is verplaatst, het is het ontvangen plein waard. Verwijderen: vale C ALL3.TXT |
| Vierkant midden | 6-karakter met LL of MM |  —  | Uitsluitend informatie |
| Automatische ontvanger | Afspraak met -SDR, /SDR, -# of -n |  —  | Uitsluitend informatie |

Em. Q65 en JT65 zijn de manieren van communicatie via Luna: In theorie vallen ze onder de uitsluitingsclausule zoals de scatter meteoriet, maar ze worden ook gebruikt in tropo. Het programma berekent de positie van de Maan met de lage precisie formules van de Almanac Astronomische (ongeveer 0,3°) [20]: als een verbinding in Q65 of JT65 groter is dan 1000 km en de Maan boven de horizon voor beide hoofden ligt, is het vrijwel zeker EME en is uitgesloten (het telt onder de uitgeslotenen). Als de Maan niet gebruikelijk is, blijft de verbinding mogelijk in de trope en gaat door de quarantaine van lange verbindingen, maar het signaal blijft zichtbaar. Het is van toepassing op clusters, voor MQTT en zijn eigen UDP decoderingen.

## 11 ter. criteria voor de teruggooi, één voor één

Het programma wil alleen de tropoferische voortplanting tonen in 144 MHz en verder. Elke binnenkomende gegevens (Cluster Row, PSKReporter Report via MQTT, UDP decoderen) gaat door een reeks filters, altijd in dezelfde volgorde. Een afgedankte gegevens verdwijnen niet in niets: wordt geteld (contact van de uitgesloten in de bovenste balk) en, als de reden twijfelachtig is, verschijnt in het tabblad Geschorst Verslagen.

|  N.  | Criteria | Waar is het? | Wat is er verworpen? | Kun je je omkleden? |
|  1  | Band | cluster, MQTT, UDP | Allen onder 143 MHz (ook 50 en 70 MHz), behalve GRAVES 143,050 MHz. Banden van 144, 222, 432, 1296, 2320 MHz en 10 GHz | Nee. |
|  2  | Satelliet QO-100 | cluster, MQTT, UDP | 2400,0 MHz: het is geostationair satellietverkeer, niet tropo | Nee. |
|  3  | Meteorietenscatter | cluster, MQTT, UDP | JT6M en ISCAT (ook ISCAT-A/B), opmerkingen met "&" en niet-tropoxy voortplantingslabels (<AU>, <AUE>, <ES>, < FAI>, < F2 >) altijd. MSK144, FSK441 en JTMS (ook met <MS>) slechts meer dan 250 km: binnen 250 km accepteren we, omdat deze modi op die afstand ook worden gebruikt in tropo | Nee. |
|  4  | EME (via Luna) | cluster, MQTT, UDP | EME geschreven in het commentaar; Q65 of JT65 meer dan 1000 km wanneer de maan boven de horizon aan beide hoofden; stations geleerd als EME | Ja: Goede regel in verdachte signalen |
|  5  | Niet-echte luisteraar | MQ | PSKReporter ontvanger die zegt te horen op 144 stations actief op dat moment op andere bands (ten minste twee): Zijn relaties worden genegeerd. Terug goed als de DX menselijke clusters bevestigen dezelfde stations | Ja: Goed / Arm |
|  6  | Quarantaine van lange verbindingen | alle regels | Meer dan 1000 km blijft een verbinding verborgen totdat hij een bevestiging vindt: hetzelfde station in dezelfde richting, een andere route in een gang van 200 km, of parallelle route binnen 10° en 200 km langs ten minste de helft. Zonder bevestiging verloopt na 60 minuten | Ja: Goed / Uit naar station |
|  7  | Minder dan 100 km | regels en lijst | Met de on-box, de kortste aansluitingen van 100 km (grondgolf, optische bereik) | Box, uit bij opstarten |
|  8  | Voorbij de kroon van de QTH | regels | Als een station meer dan 1000, 700 of 500 km (naar keuze) van uw QTH, geen lijn: alleen de lilla pleinen bedekt door de twee stations. Uw QTH-plein nooit kleuren | Vak en straal, op 1000 km |
|  9  | Tijdsvenster | alles | Oudere plekken van het gekozen bereik (van 3 tot 60 minuten) | Menu bovenaan |
|  10  | Dubbel | cluster | Dezelfde plek (zelfde DX, dezelfde spotter, dezelfde band, dezelfde bron) herhaald | Nee. |

Opmerking over de criteria 4 en 6. Een verbinding in Q65 of JT65 meer dan 1000 km zonder Common Moon is mogelijk in tropo: wordt niet weggegooid volgens criterium 4, maar gaat voor de quarantaine van criterium 6 als een andere lange schakel. Directe oproepen naar u (sectie 9 quater) zijn de enige uitzondering op criteria 7 en 8: ze altijd tekenen.

Wat geen verspilling is. De correcties van de locator (C ALL3. TXT met handstemmen, correcties uit het cluster en geleerde locators, achtervoegsels -SDR, midden van het vierkant LL/MM) veranderen waar je een station tekent, niet weggooien. Een station zonder verhuurder, aan de andere kant, kan niet worden getrokken: blijft in de lijst met de afstand.

## 11 quater. Kleuren en symbolen

Elke kleur heeft slechts één betekenis, gelijk in alle thema's (wijzig alleen de achtergrond).

| Kleur en teken | Betekenis |
| Doorlopende blauwe lijn | Spot van DX cluster, RBN of DM4X tussen twee stations |
| Gele lijn met donkere rand | Spot met luchtscatter commentaar (reflectie op vliegtuigen) |
| Donker roze lege cirkel | Luisterstation op PSKReporter (MQTT) |
| Dunne roze lijnen uit de cirkel, met een roze bal aan de onderkant | Stations die de luisteraar hoorde, met de naam altijd geschreven; dikkere lijnen voor de geselecteerde luisteraar |
| Rode lijn en rode naam | Uw UDP-decoderingen (WSJT-X, JTDX, MSHV): Wie ben jij? |
| Rode lijn knipperende slagen | QSO in uitvoering met dat station: Hij belde je en de sluiting is nog niet aangekomen, of is de DX Call van WSJT-X/JTDX met de ingeschakelde transmissie. Ga verder met RR73, RRR of 73 |
| Groene lijn | QSO desunti dal live: sluit RR73, RRR of 73 UDP-weergave |
| Zwarte lijn met witte rand | Iemand roept u: het duurt tot RR73/RRR /73 of tot 3 minuten |
| Vierkanten van 45° | Verkeer met ten minste één station buiten de gekozen kroon: Geen lijnen, alleen vierkanten locator |
| Zoek naar lilla's met afstand naar het noorden | 1000, 700 en 500 km rond uw QTH; die keuze is meer gemarkeerd |
| Cirkel en punt in de kleur van het thema | Uw QTH, met naam en verhuurder |
| Oranje bal met GRAVES 143,050 | De radar GRAVES ( JN27SI) |
| Monster van blauw tot rood | Hepburn trope index (0 F5LEN |
| Lichtgrijs fluweel | Gebied zonder tropo-gegevens voor de gekozen tijd |
| Grijsblauwe lijnen met de waarde in hPa | Isobeer elke 4 hPa; H in blauw hoge druk, L in rood lage druk |
| Rode, blauwe en paarse stippellijnen | Isothermie tot 850 hPa elke 4 °C: rood boven nul, blauw onder nul, violet de nul |
| Monster oranje | Thermische inversie: lucht tot 925 hPa warmer dan op de grond, intenser met de sterkere inversie |

In de verdachte rapporten de rand van elk tabblad zegt de geldende regel: groen Goed, rood Scarta, grijs geen regel.

## 12. Instellingen en draagbaarheid

Raam. Aan het begin neemt het venster 90% van de nuttige ruimte van het scherm in beslag (snijd de toepassingsbalk), gecentreerd, dus op een 15-16-inch notebook met een grootte van 125% of 150% blijft de titelbalk zichtbaar. Als de nuttige ruimte onder 1280 × 720 punten ligt, wordt het venstergedeelte gemaximaliseerd. Grootte, positie en gemaximaliseerde status worden opgeslagen in LIVETROPOVHF. Ini bij het sluiten en eindigen bij de volgende start, mits de titelbalk nog steeds in een scherm valt (bijvoorbeeld na het losmaken van een externe monitor); Anders keer je terug naar 90%. De minimumgrootte is 900 × 560.

Het programma is draagbaar: schrijft nooit in de AppData map of in het register. Alles is in de map van het uitvoerbare: LIVETROPOVHF.ini (sets, gemaakt bij de eerste sluiting), C ALL3.TXT (met handgeschreven stemmen, geleerde correcties en locators), kaart cache en weersgegevens, regels geleerd (Dati/APPRENDIMENTO.TXT) en LIVETROPOVHF.db archief. U kunt het ook op een USB-stick houden, zolang de map schrijfbaar is.

Ad-archief (SQLite [14]). Elke plek geaccepteerd en elk PSKReporter luisterverslag wordt opgeslagen in LIVETROPOVHF.db, een SQLite database in één bestand. In het geheugen alleen de laatste 60 minuten blijven voor de lijst en kaart; het archief behoudt alles en dient voor de historicus en statistieken (ook de UDP decoderingen), bijvoorbeeld de openingen voor band en per dag, de maximale afstanden, de pleinen bereikt, en de achterste vergelijking tussen de verwachte index en de echte plekken. De regels die uit het filter worden verwijderd worden geteld maar niet archiveren.

## 12 bis. Automatische installatie en updates

Vanaf versie 1.0 downloadt het programma al klaar vanaf de Release pagina van github.com/ic8tem/LIVETROPOVHF, in dezelfde pakketten van YA-MS: installateur voor Windows (64 bit, 32 bit en ARM, installeert in C:\LIVETROPOVHF zonder beheerdersrechten) en draagbare zip; voor Windows 7 SP1 en 8.1 pakketten apart (-windows7-x64 en -wind.deb,.rpm, Arch, Slackware, AppImage en tar.gz voor Linux; pakketten voor Raspberry Pi (64 en 32 bits) en Alpine;.dmg en apps.zip voor Mac (Apple Silicon en Intel). Met Linux pakketten, het programma wordt gekopieerd naar de eerste start in ~/LIVETROPOVHF, zodat instellingen en gegevens blijven naast het programma; op de Mac gegevens wordt geboren naast LIVETROPOVHF.app.

Elke 30 minuten (de eerste controle een paar seconden na de start, onmiddellijk als de laatste keer dat het programma was gesloten voor een fout) het programma leest alleen, zonder te vragen om iets, de laatste release. Als de versie is nieuwer download het draagbare pakket geschikt voor het systeem en vervangt de programmabestanden ter plaatse: die in gebruik worden hernoemd *.old-update en verwijderd bij de volgende herstart. Instellingen, archief, gegevensmap en C ALL3. TXT is niet aangeraakt. De nieuwe versie is actief vanaf de volgende start: bij de installatie klaar is een venster stelt voor om onmiddellijk opnieuw te starten; voor dringende correcties, gemarkeerd [verplicht] in Release, kan reboot niet worden uitgesteld. De controlestatus en de gebruikte versie zijn geschreven onderaan de statusbalk; om updates=0 in LIVETROPOVHF.ini uit te schakelen. Op de Mac is de app ondertekend en verandert zichzelf niet: het programma meldt alleen dat er een nieuwe versie is.

De LIVETROPOVHF afbeelding met het versienummer verschijnt bij het begin; aan het eind vraagt het programma om bevestiging en bedankt voor het gebruik.

## 12 ter. Foutmelding log en statusbalk

Elke onvoorziene fout eindigt in Data/errori.log, zelfs met de berekening log uit: datum en tijd, versie, systeem, open tabblad, volledige fout en laatste regels van wat het programma deed. Een interfacefout sluit het programma niet langer: in de statusbalk verschijnt " anno geannoteerde fout" (klik: open het register). Bij de volgende start vraagt het programma of het rapport moet worden verzonden: opent de browser op een reeds gecompileerde GitHub-signaal, met nominatie, locator en gebruikersnaam van de PC verwijderd, die u controleert en verstuurt met een gratis GitHub-account. Het logboek eindigt ook bij elke updatecontrole.

In de statusbalk worden de bronballen (DX-cluster, RBN, PSKReporter, MQTT, UDP) geklikt en naar de bron gebracht; de strip zegt waarom een bron uit is: afgewezen poort, clusterknooppunt aan de serverzijde, geen antwoord of UDP-luisteren, maar geen pakketten van WSJT-X, JTDX of MSHV. Een klik op de versie nummer onmiddellijk controleren updates.

## 13. Platforms

Externe boekenkasten: Avalonia voor de [12] interface, MQTTnet als MQTT client voor de real-time stream van PSKReporter [13], Microsoft. Data.Sqlite voor het [14] archief.

C# op.NET 10 (LTS) [15] met Avalonia interface, gepubliceerd als een onafhankelijk uitvoerbaar bestand voor Windows 64 en 32 bits, Mac Intel en Apple Silicon, Linux Debian en Raspberry Pi (64 en 32 bits). Genoteerde limieten: Windows 10/11; macOS 14 of later, met apps om te ondertekenen of te openen rechts → Open; Raspberry van Pi 2 / Zero 2 W up (ARMv6 modellen worden niet ondersteund door.NET).

## Dank je.
Dankzij I5MZY Massimo Zannoni voor het testen op Windows 7 en Windows 8.

Luchtdicht: ITU-R P.453, digitale producten van studiegroep 3 (© ITU), gebruikt voor persoonlijke en niet-commerciële doeleinden.

## Belangrijkste bronnen

1. S. Heck LA0BY, Troposferische modi, checklist voor gebeurtenissen (Hepburn/ F5LEN -voorspelling, hoge temperatuurprofielen met Windy, DX-cluster als bewijs van opening), observatie dat alleen prognosekaarten niet genoeg zijn voor extreme routes.
2. J. N. Gannaway G3YGF, November 1983 (uit het gelijknamige artikel over Radiocommunicatie, RSGB, augustus 1981). Vormen van verlies en verspreiding van vrije ruimte, gelijkwaardige straal 8497 km, plaatsterm van 10 dB per horizon, klimaatterm 0,2 · (N − 310), plantcapaciteit en signaal/ruisverhouding, bijvoorbeeld bij 144 MHz.
3. W. R. Hepburn, DX Info Centre, Index schaal en waarde betekenis, voorspellingen op 6 dagen.
4. F5LEN, prognose voor Europa (tropo.f5len.org/forecasts-for-European: lijst met afbeeldingen in het paginascript, klasse legende Disturb... Duct in het beeld) en de wereldvoorspelling (tropo.f5len.org/WW) en noot Index gebaseerd op reactiviteit in eenheid N, 40 stappen vanaf 3 uur, update 4 keer per dag.
5. mqtt.pskreporter.info, distributie MQTT in real time van de commercials van pskreporter.info (gecurseerd door M0LTE, stroom van P. Gladstone gebruikt met toestemming). Deuren 1883
6. Project samenvatting van 8/11/2026 en Go test (core.go, gui.go) verstrekt door de auteur van het programma. Ad regels, negatieve filter, bandbreedte segmenten, telnet cluster management, thema's, talen en draagbaarheid.
7. C ALL3.TXT, nominatie database → JTDX verhuurder (I. UA3DJY, A. ES1JA script, JTAlert/hamspots.net data). Bestandsformaat en tellen van de namen geverifieerd in het projectbestand.
8. WSJT-X, broncode, NetworkMessage.hpp bestand (beschrijving van UDP protocol: Heartbeat, Status, Decode). Algemene verwijzing, niet bijgevoegd; JTDX en MSHV gebruiken hetzelfde formaat.
9. Reverse Beacon Network (reversebeacon.net), service pagina's van de telnet node. Havens 7000 (CW, RTTY) en 7001 ( FT8), alleen aanmelden met de naam.
10. D. Mills, RFC 4330 Pakketformaat, afvalberekening en retourvertraging. Algemene verwijzing, niet bijgevoegd.
11. NTP Pool Project (ntppool.org); INRIM, NTP time service (ntp1/ntp2.inrim.it); PTB (ptbtime1.ptb.de). Servers gebruikt voor time control. Algemene verwijzing, niet bijgevoegd.
12. Avalonia UI, officiële documentatie (docs.avaloniaui.net). Multiplatform interface, thema's en lay-out van rechts naar links. Algemene verwijzing, niet bijgevoegd.
13. MQTTnet,.NET bibliotheek voor MQTT (github.com/dotnet/MQTTnet). Klant gebruikt voor de PSKReporter stroom. Algemene verwijzing, niet bijgevoegd.
14. SQLite (sqlite.org) en Microsoft. Data.Sqlite (learn.microsoft.com). Archief in één bestand. Algemene verwijzing, niet bijgevoegd.
15. Microsoft,.NET 10 Platformen en limieten vermeld in punt 13. Algemene verwijzing, niet bijgevoegd.
16. ECMA-376 Structuur van het.docx bestand en vergelijkingen. Algemene verwijzing, niet bijgevoegd.
17. Lijst van de 28 talen van de YA-MS wiki, verstrekt door de auteur van het programma.
18. Open-Meteo, weersverwachting API (open-meteo.com), met gegevens van het NOAA GFS model. Druk op zeeniveau voor isobare (zie document 2).
19. J. P. Snyder, Algemene verwijzing, niet bijgevoegd.
20. De Astronomische Almanak, sectie D, Weinig precisieformules voor de maan... (precisiepositie ongeveer 0,3°). Gebruikt om EME-verbindingen te herkennen. Algemene verwijzing, niet bijgevoegd.
