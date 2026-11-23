# Voorspellingshandboek

> Automatische vertaling door het Italiaans: bij twijfel is het de Italiaanse gids waard.

Het tabblad Voorspelling berekent de tropoferische voortplanting zelf: een indexkaart op het zichtbare gebied en het signaal tussen uw station en een overeenkomstige. Deze handleiding beschrijft alle gegevens, hoe het berekend wordt en uit welke bron het komt. De getallen tussen vierkante haakjes verwijzen naar de bronnen onderaan; in het programma hebben elke gegevens van de resultaten een strip (simbolo i) met dezelfde bron en de verwijzing naar de alinea.

## 1. Wat hij doet en wat hij doet

De voorspelling van de verticale temperatuur- en vochtigheidsprofielen van het GFS [1, 2] -model vloeit voort uit de reactiviteit van de luchtlaag per laag en schat in hoeveel het signaal toeneemt in vergelijking met een normale atmosfeer. Op de route voegt de echte grond (Copernicus GLO-30 [3]), diffractie, troposcatter, kanalen en ontvanger lawaai.

Het is geen maatregel: het is een schatting gemaakt op een weermodel met verre niveaus honderden meters. Het dient om situaties te vergelijken (vandaag beter dan gisteren, 70 cm beter dan 2 m, deze behandeling beter dan dat), niet om een verbinding te garanderen. In paragraaf 14 wordt uitgelegd hoe bewijsmateriaal moet worden verzameld om het te valideren.


> "Prohibitie kan niet worden veralgemeend, maar bestudeerd in relatie tot lokale morfologie. Wat?
> Volker Grassmann, DF5AI, gerapporteerd door Flavio Egano, IK3XTV (Propagatie op 2 meter)

Het is het principe dat de punt-punt berekening leidt: elke route wordt berekend op zijn bodem, met zijn horizon, zijn zee en zijn reliëfs; de kaart geeft het algemene beeld, behandelt het antwoord.

## 2. Taalopdrachten

- Band: 144, 432 of 1296 MHz. Kaart en berekening maken gebruik van de gekozen band.
- Nu: de cursor verplaatst de prognose van nu tot 72 uur vooruit, binnen 3 uur.
- Bijwerkingsvoorspelling (zichtbare zone): Download de GFS profielen op een raster van maximaal 240 punten over de zichtbare kaart. De rasterhoogte (0,5° of meer) hangt af van de vergroting.
- Stop: onderbreek een download of berekening.
- Details om te tonen (rechter paneel van de kaart): drie niveaus, Essentieel, Normaal en Complete, zet een groep van dozen; elke doos kan dan met de hand veranderen. De dozen zijn in drie groepen:
  - Op de kaart: isolinee del Gain, ook 3 De kleuren blijven altijd.
  - Bij de berekening van het signaal: alle lijnen van de berekening, vlak aardprofiel, rekenlogboek en gedetailleerd bestandslogboek.
  - Correcties en levende gegevens: weerscorrecties (Misme, daling), lucht-zee contrast, gemiddelde met vlekken (corridoi), APRS 2 m sprongen en LoRa, over de berg zonder verliezen (paragrafen 4 bis, 9 bis, 9 quater en 10 bis).
  - Essentieel houdt alleen isolinees, weer en media correcties met vlekken; Normal voegt isobare, inversie, weerstekens, direct uitzicht, platte aarde, lucht-zee en APRS; Complete zet het aan. Het niveau verandert alleen wat je ziet en welke correcties er worden ingevoerd: de berekeningen blijven hetzelfde.
- Bron van gegevens en historisch geval: van waar de profielen komen en op welke dag je kijkt (paragrafen 3 bis en 3 ter).
- Mijn station en de correspondent: Locator, antennes, stroom, kabels, lawaai (paragrafen 7 en 12).
- Bereken het signaal en de dekking in de optiek: de twee berekeningen op de grond (paragrafen 8
- Berekeningsregister: elke stap met de gebruikte nummers, inclusief in het Data/ forecast-calcoli.log bestand.

## 3. Van GFS-gegevens naar profiel

Voor elk punt van het rooster en elk uur lezen we de druk op de grond, temperatuur en vochtigheid op 2 m, de wind op 10 m, de hoogte van de grenslaag, de CAPE en temperatuur en vochtigheid ten opzichte van 1000, 975, 950, 925, 900, 850, 800, 700, 600 en 500 hPa. Vanaf 1.3.0 kunt u ook de wind lezen tot 850 hPa en de verticale snelheid tot 850 en 700 hPa (punt 9 bis).

De niveaus worden berekend met de ipsometrische formule en de virtuele temperatuur; niveaus onder de grond zijn schaars. Voor elk niveau:

- N = 77,6/T · (P + 4810·e/T), met T in kelvin, P en in hPa, en door de verzadigingsdruk van ITU-R P.453-14, eq. 9, met de EF-correctiefactor (hercontroleerd op de officiële tekst) [4].
- M = N + 157·h, met h in km: de gewijzigde reactiviteit. Waar M naar boven gaat is een leiding.

## 3 bis. Gegevensbron

Het Data Source menu kiest waar de profielen vandaan komen:

- Automatisch (aanbevolen): GFS eerst van NOMADS [14], de NOAA service die het model verspreidt: voor elk moment vraagt het programma twee uitsparingen van het gebied in G RIB2 formaat en leest ze van zichzelf; NOMADS heeft geen dagelijkse limiet, slechts één per minuut die het programma respecteert. Met brede roosters (van 0.5° naar boven) gebruik bestanden tot 0.5° of 1e elke 3 uur, veel lichter. De punten die NOMADS niet geeft, of alle als NOMADS niet reageert, vraag Open-Meteo [2]. De twee bronnen geven hetzelfde model en hetzelfde ras, zodat je in hetzelfde raster kunt mengen.
- GFS van Open-Meteo: alleen Open-Meteo, zoals in 1.2 (op de limiet per minuut wordt verwacht).
- GFS van NOAA NOMADS: alleen NOMADEN. Langzamer om te downloaden (twee bestanden per uur, een halve seconde tussen een verzoek en de andere om de NOAA-limiet niet te overschrijden), maar verbruikt de Open-Meteo-limiet niet.
- ICON-EU (DWD) en ECMWF IFS 0,25°, van Open-Meteo: andere modellen dan de GFS, die van toepassing zijn op het hele net en voor de routes. Ze mengen zich niet met de GFS: vergelijk twee modellen tegelijkertijd is een eenvoudige manier om te zien hoe de prognose onzeker is. ICON-EU heeft alleen betrekking op Europa; ECMWF heeft niet alle GFS-drukniveaus (de ontbrekende niveaus springen) en heeft mogelijk niet de limietlaag.

De gebruikte bron is geschreven in de statusregel en in de eerste regel van de resultaten (Data Source), met hoeveel punten kwamen uit elke dienst en hoeveel uit de cache. De schijfcache wordt gescheiden door model.

## 3 ter. Historische zaken

Het Historisch Case menu bekijkt een gedocumenteerde opening met het Open-Meteo [15] archief GFS voorspellingen: de kaart verhuist naar het gebied van de zaak, de band zet zichzelf en de tijdcursor vertrekt vanaf de eerste dag van de zaak. Door op Update forecast te drukken download je de profielen van die dagen; signaalberekening werkt zoals voor de huidige prognose. Het dient om het model te vergelijken met wat er echt gebeurd is.

Het eerste geval is dat van 14 De tweede is de opening tussen 29 december 2021 en 2 januari 2022 op het Tyrreense en het westelijke Middellandse Zeegebied op 144 MHz, uit de rapporten van IC8TEM: eerste openingen op 29 (tot 737 km), breder op 31 ( IT9GSF EB5EA 1217 km), grote opening op 1 januari tussen Italië, Sicilië en Spanje. Voor oudere gevallen (bijv. september 2000) heeft het Open-Meteo archief geen gegevens. In het archief ontbreekt de hoogte van de limietlaag: daar is de correctie niet van toepassing. [23] FT8 Q65

## 4. De index en kleuren

Voor elke laag tussen twee niveaus vergelijkt het programma de N-val met de drempels van tabel 6.5 van Bean en Dutton [5]: bijvoorbeeld tussen 1000 en 850 hPa is een afname tot 60 N/km normaal, meer dan 100 N/km is super-breking. Een super-refractieve laag is 10 tot 15 dB winst waard; een leiding die de band van 20 tot 25 dB valt, volgens het tekort van M. Sterke wind naar de grond, hoge limietlaag en hoge CAPE verminderen de winst, omdat ze de lagen verwijderen of breken [5, § 4.4].

De kleur is de geschatte winst in dB, met een vaste schaal gelijk in alle thema's:

| Winst | Kleur | Betekenis |
| Minder dan 7 dB | Niemand. | bijna normale atmosfeer |
| 7 | blauw | gradiënt over normaal |
| 10 | Groene | super-refractieve laag |
| 13,16 dB | geel | sterke laag, maar zonder kanaal voor band |
| 18,5 dB | oranje | zeer sterke laag of te dunne buis |
| 18,5 | rood | Conduit Trapband |
| van 21 dB | Paars | Sterk en diep kanaal |

Met de display box ook 3 Een gestippeld paneel maakt het gebied dat door zijn eigen prognose wordt bestreken af en toont de maximale zichtbare winst: bij minder dan 7 dB blijft de kaart zonder kleuren omdat de atmosfeer bijna normaal is, niet omdat de gegevens ontbreken. Bij het downloaden van een nieuwe klik op Update voorspelling niet onderbreken; om te stoppen is er Stop.

De kleuren zijn ondoorzichtig tot 82%, dus de achtergrond van het thema verandert ze gewoon. De locator raster en vierkante namen zijn ontworpen over de kleuren. Waar uw voorspelling er niet is maar Hepburn of F5LEN geven 5 of meer schaakkaarten verschijnen met de kleur van die bron.

De kaart beoordeelt elk punt alleen. De signaalberekening bepaalt in plaats daarvan dat de laag continu is langs de route (punt 9): een route die zeer geel kruist kan minder geven dan de kaart doet denken.

## 4 bis. Prognose gemedieerd door commerciële bedrijven: corridors (vanaf 1.3.3)

Copyright Policy: Een link alleen mag nooit een geleider trekken. Een geïsoleerde en korte ontvangst kan zijn de verstrooiing van een transiting vliegtuig (het APRS pakket is zeer kort), een opening van een paar minuten of een fout, en zou geven "drop-geleiding" ze misleiden. Het programma herkent een corridor wanneer, in dezelfde band:

- ten minste 3 paar onafhankelijke stations (geen gedeeld station) werken op routes van ten minste 300 km;
- de routes lopen parallel: azimut binnen ±15°, gemiddelde punten binnen 150 km van de gemeenschappelijke as;
- alle in de laatste 30 minuten, waarbij de bundel zich op afstand van ten minste 10 minuten herhaalt (persistentie);
- voor de ARS telt een sprong alleen als dezelfde sprong wordt herhaald;
- van 1.3.4, de routes over 1500 km tellen alleen als de balk afkomstig is van ten minste twee verschillende bronnen (bv. PSKReporter en een cluster): van een enkele bron, tot die afstanden in 2 m, zijn vaker EME, scatter meteoor of verkeerde locators dan tropo.
- vanaf 1.3.4: tel de stations 'tot straal' niet op: die in het laatste uur ten minste 5 correspondenten over 1000 km hebben verspreid over een ventilator van ten minste 150°. In 2 m is de trope gericht; een ontvanger die samen Israël, Portugal, Engeland en Italië "vrij" vanaf 2000.3700 km, vaak met positieve signalen, heeft bijna zeker de frequentie slecht geconfigureerd. In de 17 archief 72% van de accommodatie in 2 m meer dan 1000 km kwam van stations dus; op 22/11 om 10:53 UTC waren OH6HLH, OZ1ITS, F4LUK, OM8ABC en SP2JLR, en met het filter de langste gang daalt van 2175 km (regole 1.3.3) tot 615 km. I5MZY

Bronnen: clusterspots (niet die gemarkeerd met AS/ACS), PSKReporter via MQTT, hun QSO's en WSJT-X/JTDX/MSHV decoderingen, en APRS springt in 2 m en LoRa 433/868 MHz.

2 m en LoRa. Het programma leest APRS-IS in alleen-lezen (rotate.aprs2.net, filter op Europa) en haalt radiosprongen tussen bekende locatiestations (bron → digipeater → IGate), zoals de VHF-kaart van DXView en APRS PropView (RF-YVY, Apache 2.0). Drie banden zijn de moeite waard: 2 m, LoRa 433 MHz (bandcorridors 432) en LoRa 868 MHz (corridoi die 1296 tellen: een geleider die de 868 MHz ook de hoogste frequenties brengt). De band komt uit de LoRa tocall (aprs-deviceid lijst) en uit de frequenties of woorden in het commentaar; een sprong is slechts de moeite waard tussen twee stations van dezelfde band.

Het delicate punt is de actieve repeaters: een signaal ontvangen op 500 km kan worden bereikt met twee of drie digipeater doorgiftes. Een sprong telt dus alleen als het tussen twee echte en opeenvolgende zenders zit:

- het pad moet worden getraceerd: elk digipeater dat is doorgestuurd heeft zijn naam gezet, en de sprong wordt gemeten tussen de ene en de volgende; een alias gebruikt zonder naam (W IDE1 *, W IDE2 *, regionale alias) breekt de keten en dat stretch telt niet;
- verborgen doorgiftes: het programma herinnert zich het pad dat elke bron gewoonlijk vraagt (bijvoorbeeld W IDE2 -2). Als een pakket wordt geleverd met een sprong in minder (W IDE2 -1) en geen naam verklaart het, een digipeater opnieuw verzonden zonder tracking en het pakket wordt weggegooid; de bronnen nooit eerder gezien niet tellen;
- digipeater in het midden van de weg: als er tussen de twee stations, min of meer op de weg, een digipeater van dezelfde actieve band in de laatste drie uur is, wordt de sprong weggegooid, omdat het had kunnen worden teruggezet (het is vooral voor de digipeater LoRa die terugzenden zonder het pad te veranderen);
- niet-radiopakketten (qAC, qAX, TCPIP), die welke via internet worden geëxploiteerd via radio vanuit een IGate (derde partijen), HF en verschillende frequenties, stations op een hoogte van meer dan 1500 m (palloni, vliegtuigen) en springen over 1500 km.

Effect op de kaart (van 1.3.4). Het trekt alleen het gebied waar het gegarandeerde minimum geldig is: een band van ±60 km rond het centrale deel van elke route (van 15% tot 85% van de lengte), niet op stations aan de uiteinden, waar de leiding er mogelijk niet is. De banden van alle gangen zijn bij elkaar getekend, in een neutrale kleur (de tekst) en semi-transparant: de overlappingen worden niet toegevoegd, en de kleur wordt niet verward met de paarse van de schaal (21 dB en verder). De labels (sporen, gemiddelde afstand, minimale winst) worden met elkaar gescand; met de muis passerend op een etiket verschijnen de secties van de gang.

In het gebied, en slechts in de buurt van de huidige tijd (±90 minuten), valt de prognose niet onder een minimum. De redenering is: als de stations echt passeren, de voortplanting is er, wat het weermodel zegt.

Omdat de korte routes weinig waard zijn (van 1.3.4). In 2 m, tussen 300 en 600 km, verbinden twee goede stations (sommige honderd watt, antennes, FT8 of CW) bijna elke dag, zelfs met de normale atmosfeer, voor troposcatter: het signaal verspreid door de kleine onregelmatigheden van de lucht in het gemeenschappelijke volume van de twee bundels. Drie of vier plaatsen op 500 km zeggen dat de band werkt zoals gewoonlijk, niet dat er een opening is: GEKO Magazine n. 69 ( IU3QEZ) schattingen voor de troposcatter met 2×10 vloeiende elementen van ongeveer 440 km in SSB QRP, 510 km in SSB met 50 W, 600 km in CW QRP en 640 km in SSB met 500 W (bronnen, elf ronden). Slechts 600 Tot 1.3.3 een corridor van 450

De startwaarde gaat dan samen met de gemiddelde afstand van de routes:

- tot 400 km: 3 dB;
- van 400 tot 700 km: lineair zout van 3 tot 10 dB (500 km → 5,3 dB; 600 km → 7,7 dB);
- van 700 tot 900 km: van 10 tot 16 dB; meer dan 900 km blijft bij 16 dB.

Onder de 7 dB, dat is onder het begin van de schaal, blijft het minimum dat: de gang wordt getekend met het label, wordt gezien dat de band werkt, maar de kleuren van de kaart niet veranderen (met de doos toont ook 3/4 dB de grijze stippel). Boven de 7 dB wordt de waarde, half en half, gewogen met het aantal onafhankelijke routes (3 slagen → 0,25; 6 of meer → 1) en met de mediane S/N van relaties (−20 dB → 0,5; 0 dB of meer → 1; niet bekend, zoals in clustervlekken, → 0,75), vanaf 7 dB. Voorbeelden: 3 routes zonder S/N op 500 km → 5,3 dB; 5 routes op 615 km → 7,7 dB; 3 routes zonder S/N op 900 km → 11,5 dB; 3 routes tot −20 dB op 900 km → 10,4 dB; 6 routes tot 0 dB op 900 km → 16 dB.

De minimumafstand blijft 300 km: het verhogen tot 500 km zou ook de echte korteafstandsopeningen hebben verwijderd (bijvoorbeeld een leiding op het Tyrreense op 400 km, die wordt herkend aan de weersvoorspelling en zeer sterke signalen meer dan van de afstand alleen). In het I5MZY archief, op 22/11/2026 om 10:53 UTC, met deze regel en het straalfilter waren de langste gangen 615 km (7.7 dB) en 540 km (6.3 dB); met de regels van 1.3.3 bereikte het 2175 km en 16 dB.

Tot 1.3.3 was het minimum geldig op alle rechthoeken van de gang en was alleen afhankelijk van de afstand (van 10 dB tot 300 km tot 16 dB tot 900 km): met veel lange gangen samen (bijvoorbeeld op 22/11/2026, twaalf gangen tot 2620 km) tilde bijna heel Europa op tot 16 dB.

De versterkingscurves trekken niet langer Het schakelt uit met het "Voorzicht gemedieerd door de vlekken."

## 5. Optionele lijnen en vergelijking met druk en inversie

Gain curves: lijnen op 7, 10, 13, 16, 18,5 en 21 dB, de randen van de schaal, getekend op het veld gladgemaakt met de methode van de marcherende vierkanten, met het label in dB.

Isobare: druk op zeeniveau om de 4 van dezelfde GFS, tot het uur van voorspelling, met de centra H en L.

Thermische inversie: bochten bedekt met hoeveel lucht bij 925 of 850 hPa warmer is dan bij 2 m, bij +1, +3, +5 en +8 °C.

Onder de controles geeft een lijn de winst overeenkomst met de druk en met de keerzijde op de zichtbare zone: Pearson correlatiecoëfficiënt, −1 tot +1. De kanalen worden vaak geboren in anticyclones (de lucht die verwarmt en vormt het omgekeerde), dus een positieve overeenkomst met de druk is normaal; maar binnen dezelfde anticycline de winst verandert veel tussen zee, kust en bergen. Inversie alleen is niet genoeg: buig de golven alleen als boven de lucht is zelfs droger [5]. De lijn dient om te zien, voor nu, welke van de twee beste verklaart de gekleurde gebieden. Dezelfde vergelijking staat in het register.

## 6. Potentiële pijpleidingen: dubbele berekening

GFS-niveaus variëren van 200 tot 1000 m: een dunne inversie wordt verspreid en het model ziet de leiding niet. Als tussen twee niveaus de N-daling boven de normale waarde (40 N/km) ligt, is de lucht stabiel (thermische gradiënt onder 3 K/km) en is boven droog (specifieke vochtigheidsverhouding onder 0.8), overweegt het programma een potentieel kanaal en berekent het op twee manieren:

- Berekening A: alle geconcentreerde overmaat in 50 m.
- Berekening B: dezelfde daling verdeeld over de typische dikte van de pijpleidingen van dat gebied, afkomstig van de klimaatkaarten van ITU-R P.453 [4] (op de grond of verhoogd).

Voor het tekort en voor de maximaal gevangen golflengte wordt de laagste waarde aangehouden: het is een voorzichtige schatting.

ITU-R P.453 gegevens zijn © ITU en kunnen worden gebruikt voor persoonlijke en niet-commerciële doeleinden, met vermelding van de bron, maar kan niet worden herverdeeld: hiervoor ben ik niet in het programmapakket. Om de B-berekening aan te zetten, downloadt u de P.453 kaarten van de ITU in het tabblad: het programma, na het tonen van de gebruiksvoorwaarden, downloadt u het archief van de pijpleidingen van de ITU-site (elevated_and_surface_duct.zip) voor persoonlijk gebruik van de gebruiker, haalt u de bestanden uit Data/ P453 en zet u de berekening aan zonder opnieuw op te starten. Als alternatief kunt u downloaden van de digitale productpagina van de ITU-R studiegroep 3 (itu.int, ITU-R studiegroep 3, digitale producten, P.453, oppervlaktegegevens en verhoogde kanaalgegevens) en de zeven S_OCCUR plaatsen. TXT, S_THICK.TXT, S_STREN. TXT, E_OCCUR.TXT, E_base.T Zonder de bestanden gebruikt het programma alleen berekening A en schrijft het in het tabblad en in het register. De verhouding tussen tekort A en gemiddelde intensiteit P.453 is anomalie: Hoe vaak de pijpleiding van vandaag sterker is dan normaal. P453

## 7. Stations en locators

De verhuurder kan 4, 6, 8 of 10 karakters hebben. Met 8 of 10 karakters is de positie nauwkeurig en het bodemaandeel komt van Copernicus GLO-30: de hoogte van de antenne is voldoende vanaf de grond. Met 4 of 6 tekens is het vierkant te groot: het is beter om het quotum van de antenne op de zee te schrijven; als 0 het programma de schatting blijft met de SRTM bodem in het midden van het vierkant plus de hoogte van de grond en signalen in de resultaten. Voor de overeenkomstige standaardhoogte is de hoogte 3 m van de grond; met zee aandeel 0 wordt gebruikt SRTM bodem plus hoogte.

## 8. Land, diffractie en troposcatter

Het profiel van de route wordt bemonsterd op Copernicus GLO-30 om de 30 m (meer dan 120 km breed). Het equivalent van de aardstraal is k = 157/(157 + ΔN), waarbij ΔN de daling van N in de eerste km van de GFS-profielen langs de [6, 7] -route.

Reliefs close to stations (vanaf 1.3.2, auteurscriterium). P.452 betaalt de horizonhoek lineair, 0,573 dB per mrad: een reliëf van enkele tientallen meters op een paar honderd meter van de antenne kost tientallen dB, veel meer dan wordt waargenomen. Om deze reden kunnen de reliëfs binnen 20 km van elk station oplopen tot 6 dB: de horizonhoek als gevolg daarvan is beperkt tot die van de meest verre reliëfs plus 10,5 mrad. Het is geldig voor uw post en voor de correspondent. De lijn

Bron van de grond (vanaf 1.3.2). De quota zijn afkomstig van Copernicus DEM GLO-30 [3]: 1 tweede boog, de zee is 0 en de kust is helder. Tot 1.3.1, het programma gebruikt de Terrain Tegels "skadi" van AWS. Bij de kust mengen deze pleinen de SSRTM met lage resolutie zeebodemgegevens en spreiden het aandeel van het eiland op zee: rond Capri creëer een spookring van 80 Op 21 november 2026 meldde IC8TEM een niet-bestaande horizon in het noordwesten; de puntsgewijze vergelijking met Copernicus bevestigde het (94 m op zee ten noordwesten van Marina Grande vs. 0). Die valse horizon, van +2,6°, kostte ongeveer 26 dB troposcatter aan de kuststations. De skadi ruiten blijven alleen als reserve als Copernicus niet reageert. De Copernicus ruiten wegen 15

- Vrije ruimte: Lfs = 92,44 + 20 log f(GHz) + 20 log d(km) [8].
- Diffractie: Bullington methode op het echte profiel plus ITU-R P.452-16 § 4.2 [6] delta-Bullington sferische landcorrectie, met ITU-R P.526 [9] sferisch verlies.
- Eerste Fresnel-zone: straal √(λ·d1·d2/d); vrij indien de grond ten minste 0,6 van de straal verlaat. Voor elk obstakel wordt de [9] Knife Board V en J(VV) parameter getoond..
- In het ontwerp van het profiel is de directe straal groen in optische en rode weergave als het passeert onder de grond of onder de kromming van de aarde; rode stippen zijn de obstakels die binnenvallen het 1e gebied van Fresnel (paarse stippellijn). Op zee is de "rode punt" op hoogte 0 m geen berg: het is de zee stijgend voor de kromming. Passen met de muis op de radius of op een punt verschijnt een strip met de uitleg en waarden.
- Wanneer een reliëf bij de antenne de richting sluit (bijvoorbeeld een horizon van tientallen graden), geven diffractie en troposcatter verliezen van honderden dB: boven 300 dB schrijft het programma "> 300 dB" en "richting gesloten door een naburig obstakel," het signaal "< −230 dB" en de S/N ratio "−60 dB." In dat geval is het aan te raden om te kijken naar de aanbevolen plant.
- Troposcatter: ITU-R P.452-16 eq. 45 tot 50% van de [6] -tijd, met hoekafstand van de horizonten en N0 van de profielen.
- Zonder tropo: som van de krachten van diffractie en troposcatter, nooit onder de vrije ruimte.

## 8 bis. Troposcatter: vier modellen en onzekerheid

Naast P.452 berekent het programma de troposcatter met drie andere modellen, evenals het TropoLink [16] programma, en toont het hun verschil als onzekerheid:

- NBS TN101 § 9 [25] (vanaf 1.3.5), mediaan: L = 30 log f − 20 log d + F(θd) − F0 + H0 + Lgp. F(θd) is de dempingsfunctie van bijlage III in zijn analytische vormen (Ns 301) met correctie voor Ns, H0 de frequentiewinstfunctie van § 9.2: bij 144 MHz, met lage antennes in golflengten, wist de reflectie vanaf de grond het veld in het onderste deel van het gemeenschappelijke volume en het verlieszout van 310 dB gedeeltelijk uit. Ns is die van het uur GFS-profielen, de effectieve horizonten en hoogten komen van het Copernicusprofiel, Lgp = 0,07·e^(0,055(Gt+Gr)))) is het openingskoppelverlies. Het is dezelfde vorm die het ITG gebruikt in zijn interieur, maar met de reactiviteit van vandaag in plaats van die van het klimaat. Het test op platte secties op 144 MHz met antennes op 20 m: binnen 2 TN101 H0

- ITU-R P.617-5 [17], mediaan: L = F + 22 log f + 35 log θ + 17 log d + Lc, met f in MHz, θ in mrad, d in km, F = 0,18· N0 ·e^(−hs/7,35) − 0,23·ΔN en Lc = 0,07·e^(0,055(Gt+Gr)). N0 en ΔN worden meestal genomen van ITU's klimaatkaarten; hier gebruiken we elk uur de profielen GFS ( N0 ·e^(−hs/7,35) is de reactiviteit naar de grond, ΔN de daling in de eerste km), dus ook de troposcatter volgt de tijd.
- RSGB VHF/UHF-Handboekformule, DL2FZN [18]: L = Lfs + 24,44 + 4.283 In f + 0,06656 d. De helling met de afstand is bijna gelijk aan die van PROPBER (0,067 dB/km).

Gemeenschappelijk volume (vanaf 1.3.1): is het punt waar de straal van de twee stations wordt overschreden, elk ten opzichte van de horizonhoek, op een aarde met een gelijkwaardige straal ae = k·6371 km. Uit de TX stijgt de straal als z = hTX + x·θt + x2/(2·ae); De afstand x = (hRX − hTX + d·θr + d2/(2·ae) / θ, met θ = d/ae + θt + θr. Met vlakke horizonten en antennes op hetzelfde quotum is de basis de moeite waard d2/(8·ae): ongeveer 1,3 km tot 300 km, 3,7 km tot 500 km, 9,4 km tot 800 km. Hoe hoger het volume, hoe meer de lucht droog en zeldzaam is en hoe minder het zich verspreidt: om deze reden meer dan 800 In de resultaten zijn het aandeel en de positie van de basis, de reactiviteit, de helling en de vochtigheid van de GFS op die hoogte, en de gelijkwaardige afstand van Bray (QST, november 1961): elke extra of minder horizon is ongeveer 111 km waard. Referentie: presentatie "Over de horizon in VHF" door IU3QEZ (GEKO Magazine n. 69).

De Troposcatter lijn, andere modellen toont de twee verliezen en onzekerheid als de helft van het verschil tussen het meest optimistische model en de meest pessimistische. Het zijn slechts vergelijkingen: het verlies zonder tropo blijft dat van P.452.

## 8 uur 's middags troposcatter over reliëfs (van 1.3.4)

IK3XTV, uit Thiene ( JN55, met de Alpen op 10 km), werkt in de zomermiddag Zuid-Duitsland op 400 De observatie wordt bevestigd; de verklaring in het programma is een andere, fysiek steviger.

's Middags roert de zon de lucht over de reliëfs en stijgt de limietlaag tot 2,3 km. Bovenaan komt de vochtige lucht boven de droge lucht: daar is de turbulentie van de brekingsindex (de grootte die de troposcatter regelt) maximaal. Wanneer die top het gemeenschappelijke volume van de route bereikt, die op 400 Dit verklaart de middagtijd, hoge druk (stille cilindrische, sterke verwarming) en snelle evanescence, typisch voor diffusie en niet van de kanalen. Concrete cellen werken niet als lenzen: produceren turbulentie, en het is de turbulentie aan de bovenkant van de herschikte laag die het signaal verspreidt.

Gevolgen voor het lezen van prognoses:

- Op aarde is de eerste namiddag de slechtste tijd voor de lage kanalen en lagen, maar niet voor de troposcatter achter de bergen: de twee dingen moeten afzonderlijk worden gelezen;
- het programma beschikt reeds over de twee gegevens die het dient, de hoogte van de limietlaag die door het model wordt verstrekt en het aandeel van het gemeenschappelijke volume van de route (gemeenschappelijk volume) in de resultaten: wanneer ze dicht zijn, kan een middaggebergte beter gaan dan de berekening in de normale atmosfeer;
- van 1.3.4 de punt-punt berekening houdt rekening met: als het gemeenschappelijke volume op de aarde is, langs de route zijn er reliëfs van ten minste 800 m, de zon is meer dan 15° en de limietlaag meer dan 800 m, en de bovenkant van de laag hervormen (model bodemsnelheid + limietlaag hoogte) is binnen 500 m van de basis van het gemeenschappelijke volume, de troposcatter wint 3 dB; tussen 500 en 1000 m de lineaire winst daalt tot nul. De lijn "Troposcatter namiddag." Met sneeuw langs de route (minstens 10 cm op de grond volgens het model, sneeuw_diepte variabele van Open-Meteo of SNOD van de GFS van NOMADS) is de versterking niet van toepassing: op de sneeuw blijft de bodem koud en groeit de nieuwe laag niet. De waarde van 3 dB is voorlopig en moet worden gekalibreerd met echte links; het wordt uitgeschakeld met weercorrecties.

Bronnen: waarnemingen door F. Egano IK3XTV (Radio Rivista e www.qsl.net/ik3xtv,

## 9. De grondlaag langs de route

Het is het hart van signaalberekening. Langs de route worden GFS profielen elke ongeveer 25 km genomen. Voor elke druklaag, het langste stuk waarin dezelfde laag aanwezig is op opeenvolgende punten; tel als kanaal alleen dikke lagen ten minste 40 m [10].

- continuïteit: van welke km tot welke km en de fractie van de route;
- boven en onderkant, en helling in m/km;
- grond in de laag: de km waar de bergen boven de basis uitstijgen;
- Bedekt land: op zee is de leiding vrijwel altijd, beslist het stuk land [10]. Als het land meer dan 10% van de route overschrijdt, is de gebruikte fractie het minimum tussen continuïteit en dekking van de aarde.

De trope gain is de winst van de laag voor de gebruikte fractie. Als meer dan de helft van de profielen sub-refractive zijn, is de winst negatief.

## 9 bis. Weercorrecties: stabiliteit, daling, staking

Vanaf 1.3.0 geeft het profiel ook drie meteorologische informatie, die te zien is in het register (regave

- Stabiliteit van Misme [5, hoofdstuk 6]. Misme bindt het veld buiten de horizon aan een samengestelde parameter: M = a·(ge − 40 + b·ΔW), met a = 0,5 dB per N/km, de equivalente gradiënt en de thermische stabiliteit ΔW, het werk om de lucht van 1 km te verhogen. Het programma berekent ΔW = g·∫(θ − θ0)/θ0 dz tussen bodem en 1 km boven, met θ potentiële temperatuur; de standaard atmosfeer is ongeveer 56 J/kg waard. De term toegevoegd aan de gain is 0,07 dB voor J/kg verder dan de standaard, beperkt tussen −4 en +6 dB, en telt alleen wanneer geen leiding valt de band (de gradiënt is al in de Bean en Dutton index). De kalibratie is tijdelijk: deze moet worden gecontroleerd met de ratio's (punt 14).
- Subsidence en uphill air [19]. Moler en Holden vonden op routes van 125 ha/l km naar de zee op 1365 en 9380 MHz het hoogste signaal van gemiddeld ongeveer 2,5 dB met lucht naar beneden dat met lucht stijgt, en in een geval een versterking van ongeveer 20 dB gedurende 3 uur aan het begin van de daling, gevolgd door een daling van 22 dB wanneer de lucht terugkeerde om te convergen. De GFS geeft de verticale snelheid aan 850 en 700 hPa. Als bij beide niveaus de lucht meer dan 1 cm/s daalt en er een omgekeerde of een scherpe daling in de potentiële brekingsindex K (N gemeld adiabatisch tot 1000 hPa, die de droge en warme lucht van de bovenkant [5] benadrukt), 1,5 dB wordt toegevoegd; als de lucht stijgt met meer dan 5 cm/s, ze opstijgen 2.
- [20] Scirocco. Hete en droge lucht van Sahara over koelere zeelucht geeft zeer sterke inversies van de oostelijke Middellandse Zee naar Gibraltar. Het S bord verschijnt waar bij 850 hPa de wind van 100.200° met ten minste 7 m/s en de temperatuur is ten minste 16 °C, op de Middellandse Zee. Het is maar een indicatie: de winst bepaalt de lagen van het profiel.

Zonder een leiding die de band vangt is de totale winst niet hoger dan 16 dB, het einde van de gele: correcties maken sterkere lagen duidelijker maar passeren ze niet door kanalen (rood en paars blijven op de kanalen).

- Persistentie (vanaf 1.3.1). De grootste openingen komen na goede hogedrukdagen (VHF/UHF DX Book; Biancomano en Leyshon 1998, regel van 12 uur; de rapporten van 29/12/2021 Voor elke cel telt het programma de uren van de voorgaande 24 met een winst van ten minste 13 dB en telt het op tot 2 dB in verhouding, alleen wanneer de winst al ten minste 10 dB is.

Hoe lees je de borden op de kaart (Wijzig Weertekens; vanaf 1.3.2 is het symbool groot dubbel en passerend met de muis over een teken verschijnt een strip met de uitleg en waarden van de cel):

| Teken | Kleur | Wat betekent dat? | Effect op de trope |
|---|---|---|---|
|  ↓  | Groene | Verzakking: luchtdruppels (anticycline), verwarmt voor compressie en droog, versterkt de achterkant en houdt het stevig | gunstig, +1,5 dB |
|  ↑  | rood | opwaartse lucht (front, lage druk, convectie): mengt lage lagen en breekt inversies en kanalen | ongunstige, −2 dB |
|  S  | oranje | Stroop: warme en droge lucht uit Sahara boven frisse zeelucht | gunstige aanwijzing, geen correctie: bepaal de lagen |

Een gebied vol groen ↓ onder een anticycline is het klassieke teken van een harde opening; Rode ↑ pijlen langs de lijn zeggen dat de lagen breken, hoewel de kleur van de cel is nog steeds goed.

De doos Weercorrecties schakelt deze correcties uit om het model van 1.2. te zien.

Validatie (punt 14): bij de 593 aansluitingen op 144 MHz tussen 29 december 2021 en 2 januari 2022, vergeleken met dezelfde routes en twee weken later, bedraagt de scheidingsindex (0,5 = geen, 1 = perfect) 0,79 zonder correcties en 0,91 met correcties (vóór het dak op 16 dB). Dezelfde vergelijking toont de belangrijkste limiet: de leiding op de zee op lage hoogte, die in die dagen bracht de borden tussen Campania en Liguria, is niet zichtbaar op het niveau van de GFS.

## 9 ter. Nieuws 1.3.2: twee schattingen, plafond, hoge druk, zee, stations, richels

Een voorzichtige en agressieve schatting. De winst van de gradiënt stijgt van 0 dB tot de grens van normaal tot 10 dB tot de drempel van superbreking. In de 1000 De agressieve drempel is de klassieke Super-Refractiedrempel van 79 N/km (k ≈ 2; P.453 gebruikt −100 N/km in de eerste 100 m), die met k50 = 157/(157 − ΔN) k ≈ 2 (k = 2,5 komt overeen met ongeveer 94 N/km). De punt-punt berekening toont de twee verliezen geflankeerd; met een leiding die de bende vallen samenvallen. De kaart is voorzichtig.

Zolder van 2500 m. Wegen en kanalen worden slechts tot 2500 m boven de grond gezocht: hoe hoger de antennes van amateurradio's ze niet exploiteren. Met 1500 m verloor de validatie een scheidingspunt en 4% van de verbindingen boven 16 dB, want in de winter liggen de omkeringen vaak tussen 1500 en 2500 m.

Hoge druk uitgebreid. +1 dB waarbij ten minste 7 van de 9 cellen rond de zee een verminderde druk van 1020 hPa of meer hebben en de wind zwak is in de lage lagen (10 m onder 5 m/s, 850 hPa onder 8 m/s), behalve voor stijgende bewegingen.

Zeegewicht. In punt-tot-punt berekening wordt de winst van de lagen vermenigvuldigd met 0,8 + 0,4·μ1, waarbij μ1 de factor is van ITU-R P.452 (eq. 51) op het langere ononderbroken stuk land: 1 op de open zee, klein met lange stukken land.

Stations en uitgevoerd. Wanneer een geleider die de band slechts een deel van de lijn bedekt, wordt het aandeel van elke antenne vergeleken met de basis en de bovenkant van de leiding aan het begin en einde van het afgedekte gedeelte; de winst wordt vermenigvuldigd met 0,5 + 0,5·√((−ΔhTX/300)·e^(−ΔhRX/300)), met Δh de afstand in meter uit het kanaal. Met de leiding op bijna alle route is de koppeling van § 10.

Lagen en richels. Als de bovenkant van de grenslaag van de GFS (soil rate van het meest border_layer_height model) hoger is dan de hellingen van de route (maximaal lokaal meer dan 300 m, als de hoogste boven de 500 m), gaat de laag over de bergen zonder te breken: +1,5 dB voor de fractie van bedekte ribbels. Het is de observatie van IT9GSF op de openingen van Sicilië naar Duitsland met gunstige Tyrreense.

Niveaus dicht bij de grond op zee. Met de NOMADS bron downloadt het programma ook de oppervlaktetemperatuur (op zee is de SST), temperatuur en vochtigheid op 80 m en in de laag 30 Op zeepunten (modelsnelheid onder 5 m) worden deze niveaus toegevoegd aan het profiel, onder het eerste drukniveau, om het kanaal te zien op 100 Op de aarde worden geen lagen gebruikt: de nacht- en daglagen zijn nog niet geldig. Open-Meteo geeft alleen de oppervlaktetemperatuur.

Verdampingspijpleiding. Van SST, van temperatuur en vochtigheid tot 2 m wordt de δ hoogte van de verdampingsleiding geschat met het neutrale logaritmeprofiel (zoals in het model van Paulus en Jeske, zonder de correctie van stabiliteit): de lucht aan het haar van het water is verzadigd tot de SST (98% voor het zout), dus

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

beperkt tussen 0 en 40 m. Om 144 MHz te vangen zou het ongeveer 180 m (Kerr) kosten, dan versterkt het verdampingskanaal weinig; vanaf 432 MHz omhoog, met lage antennes op zee, veel (punt 11 bis). Voer de parabolische vergelijking en in de lijn Correcties 1.3.2.

Tijd van de dag (bezoek). Het programma berekent de hoogte van de zon op elk punt. Van dag tot dag op de aarde, met de zon boven 15° en limietlaag over 800 m of wind over 5 m/s, waarschuwt dat de bodemkanalen worden hervormd; 's nachts, met zon onder −3°, wind onder 3 m/s en limietlaag onder 300 m, waarschuwt dat met serene lucht worden mogelijk geleid door bestraling, die vallen na zonsopgang. Niet op zee. Voorlopig zijn ze gespot, geen nummers: Decembervalidatie heeft niet genoeg zomerdagen om ze te kalibreren. Dit zijn twee punten die de auteur belangrijk acht, uit dagelijkse observatie.

Normaal profiel CRPL (alternatief). In plaats van 40 N/km werd de normale daling vastgesteld volgens de exponentiële referentieatmosfeer CRPL: N(h) = Ns·e^(−c·h), met −ΔN in de eerste km = 7,32·e^(0,005577·Ns) (Bean en Thayer 1959). Met natte lucht (Ns hoog) is het normaal steiler en dient een sterkere helling om winst te geven; in hoogte is zoeter. Het blijkt slecht te zijn tot de validatie zegt dat het helpt.

## 9 quater. Contrast lucht-zee en lage zee: de Adriatische Zee (vanaf 1.3.3)

Radiogolven voelen de zeebodem niet: voel het contrast tussen lucht en wateroppervlak. De diepte telt omdat het bepaalt hoe snel de temperatuur van de zee die van de lucht volgt.

De Adriatische Zee is een lage zee. De gemiddelde diepte is ongeveer 250 m, maar het noordelijke bekken is zelden meer dan 100 m en de Golf van Triëst blijft onder 25 m; alleen naar het zuiden daalt meer dan 1200 m. Een laag water zo dun warmt op en koelt in een paar dagen, het Tyrreense in weken. Er zijn drie typische situaties:

1. Voorjaar en vroege zomer, gunstig. De zee is nog steeds koud, zelfs voor het zoet water van de Alpine en Po rivieren; het komt warme en droge lucht uit de vlakte of de Balkan. Hete lucht over koud water vormt een omkering op zee (advectieleiding): is de klassieke conditie van de openingen tussen Italië, Slovenië en Kroatië.
2. Herfst en winter met bora, ongunstig. Koude lucht boven een warmere zee: de lage laag wordt onstabiel, mengt en het kanaal breekt.
3. Na een bora, kans. Gewoon omdat de zee laag is, koelt een bora het af in een paar dagen. Als u een anticycline met hete lucht intreedt, is het lucht-zee contrast sterk en de leiding robuuster dan normaal.

De Adriatische Zee is ook een gesloten kanaal tussen Apennines en Dinari, minder dan 200 km breed: de zeelaag blijft beperkt, en de routes langs de as (Puglia... en de Trieste, ongeveer 700 km) blijven allemaal op zee, de beste voorwaarde voor een lange kanaal.

Wat het programma berekent. Op de zeepunten, met de weerscorrecties en de box Contrasto aria-merrie:

| Voorwaarde | Correctie |
| --- | --- |
| potentiële temperatuur bij 925 hPa (of 950) over de SST van 4 K | _ |
| voorbij de SST van 7 K | +2 dB |
| met luchtvochtigheid bij 925 hPa (meer dan 80%) | −0,5 dB op deze bonussen |
| gekoelde zee van 1,5 °C of meer in 72 uur daarvoor, met een gunstig contrast | _ |
| warmere luchtzee bij 2 m van 2 °C (mixing) | −1,5 dB |
| warmere zee van 4 °C | −3 dB |

De totale correctie blijft tussen −3 en +3 dB en zonder leiding blijft de winst onder het gele dak. De drempels zijn tijdelijk: in de standaardatmosfeer is de potentiële temperatuur bij 925 hPa hoger dan die van de zee van ongeveer 2

De herinnering aan SST. Modellen houden de zeetemperatuur bijna stabiel tijdens de voorspelling. De recente daling is afgeleid van de analyse van de drie dagen ervoor (Open-Meteo, alleen oppervlaktetemperatuur, alleen zeecellen): maximaal 72 uur vóór minus de huidige waarde. Als de service niet reageert, is de geheugenbonus niet van toepassing.

Limieten. De SST van de modellen onder de kust en in de bovenste Adriatische Zee is grof; de koude pen van de Po, bijvoorbeeld, is slecht opgelost. Batimetrie komt niet in de berekening: de bronnen geven altijd de SST, die al het effect van de zeebodem weergeeft. Het lucht-zee contrast overlapt gedeeltelijk aan het einde van Misme stabiliteit (§ 9 bis); hiervoor zijn de bonussen klein. De drempels zullen worden aangepast aan de corridors van commercials en de QSO via de Adriatische Zee.

## 10. Geleverd op de hele route

Als de laag de band op ten minste 80% van de gebruikte fractie valt en de stations niet in zicht zijn, wordt ook het verlies in de leiding berekend: vrije ruimte, plus de koppeling van de antennes (3 dB als ze zich in de leiding bevinden, meer indien ze boven of beneden), plus de restdemping γd = 5·10−5·ae·f^(1/3) dB/mrad van ITU-R P.452 eq. 49 [6]. Als het minder is dan het verlies zonder trope wordt het verwachte verlies.

Een band blijft alleen in de geleider zitten als de golflengte niet groter is dan λmax = 2√2·(2/3)·√(ΔM·10−6)·d (Kerr, eq. 17 [11]). Hiervoor kan dezelfde laag de 23 cm en 70 cm maar niet de 2 m vangen. Echter, de limiet is niet netto: Kerr waarschuwt dat zelfs golven meerdere malen langer lijden aan veel van de leiding, en Hepburn en het VHF/UHF DX Boek spreken van "gesferde" randen en "vergeef" kanalen. Vanaf 1.3.1 maakt het programma gebruik van een zachte leiding: als λmax tussen λmax en 3·λmax ligt, gaat de toename geleidelijk over van 15 dB (λ = 3·λmax) naar de volledige leidingwinst (λ = λmax), met de s = (λmax/λ − 1/3)/(2/3). In dat geval zijn de kanaalkleuren toegestaan. De log en de Meteo lijn langs de route geven de punten met zachte kanaal.

## 10 bis. Via Monte

Wanneer de correspondent achter de horizon wordt gesloten, zoekt het programma de beste richting om de antennes te wijzen: de verlichte helling verspreidt het signaal als radardoel. Het loopt de horizon om de 5e (behalve ±15° rond de correspondent) en neemt in elke richting het verlichte horizonpunt, meer dan 100 m en tussen 1 en 60 km. Een snelle schatting (vrije ruimte, bitactische conversie en 10 dB voor elk niveau van de horizon van de diffuser naar de correspondent) kiest de 4 meest veelbelovende, ten minste 15° van elkaar; voor elk:

- behandelen 1, station → reliëf, in de vrije ruimte;
- behandelt 2, relief → correspondent, berekend als de relief overgedragen van 10 m boven de top, met dezelfde voorspelling (zonder paraboolvergelijking);
- conversie met de bistatische radarvergelijking, met σ = σ0·A, σ0 = −15 dB (berg in VHF/UHF) en A het door de R1 -bundel verlichte gebied (maximaal 20 km2):

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

De lijn "Recommended Point" geeft de azimut van de beste route aan, indien deze minder kost dan de directe route. Bij 144 MHz, met een reliëf op 20 km en een 15° yagi, kost de bergweg ongeveer 45 dB meer dan de 2 route alleen: het gebeurt alleen wanneer de directe wordt gesloten door naburige obstakels. De schatting is bruto (±10 dB), vooral voor σ0.


Zonder verlies aan de berg (optie, van 1.3.2). Met de De horizonronde kiest dan het punt met de meest vrije horizon naar de correspondent (in pariteit, het hoogste) en het resultaat toont de route van het punt naar de correspondent, met signaal en S/N, naast het totaal met de diffusie. Het is een optimistische grens: zegt als de richting opent wanneer de berg verhoogt het signaal goed, of waar te gaan draagbaar. Elk punt wordt gegeven met de tien tekens.

## 10 ter. Toegang tot een verre leiding

Als de leiding die de band vangt ver begint, voorbij de horizon van het station, moet het signaal aankomen. Het programma brengt het signaal naar het startpunt, in het midden van de leiding, voor diffractie: langs het Copernicus profiel GLO-30 zoekt de rand met de hoogste Fresnel parameter (ITU-R P.526), met de zwelling van het gelijkwaardige land,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

meer de demping als gevolg van de kromming van de bovenkant (afgerond obstakel, P.526-16 § 4.2), waarbij de R-straal wordt geschat vanaf de grond van Copernicus GLO-30 tot ±1 km van de rand;

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

(voor m·n > 4 is het formulier 34b van de aanbeveling). Barsis (NBS, 1963) gemeten op drie lange routes over een berg (Colorado, Alaska, Europa) gemiddelde verliezen 10 Het resultaat vergelijkt met diffractie op gladde sferische grond; het is het ergste. Vervolgens reist het signaal in de leiding: vrije ruimte op de gehele route, restdemping γd van P.452 op de sectie in de leiding en koppeling van de correspondent (3 dB binnen, 1 dB om de 10 m buiten). Als dit verlies lager is dan het verwachte verlies, wordt het het mechanisme ("geleiding (input door diffractie)"). Het is de premisse van de auteur over de Italiaanse geografie: met een nabijgelegen obstakel ver onder de lijn, v is negatief en de ingang kost weinig; met een dorsale front kost 15

## 10 quater. ITG (Longley-Rice) en uitgang van het kanaal

Vanaf 1.3.2 bevat het programma het Irregular Terrain Model 1.2.2 van NTIA/ITS (Longley and Rice, ESSA ERL 79-ITS 67, 1968; Hufford, Longley and Kissick, NTIA 82-100, 1982), vertaald in C# door de officiële code van de Amerikaanse overheid, die vrij is van rechten. De vertaling geeft dezelfde nummers als het origineel: auto compilatie vergelijkt het elke keer met NTIA C++ code op vijf testcases (onder 0,01 dB).

Het ITG berekent het verlies in de normale atmosfeer op het echte profiel Copernicus GLO-30: gezien met de reflectie van de grond, diffractie (spigol en bolland gewogen op de ruwheid van de bodem Δh, dat is het verschil tussen de 10e en 90° De reactiviteit van de bodem wordt teruggebracht tot het gemiddelde gedeelte van de route, $N_s = N_0\,e^{-h/9460}$, en geeft de kromming van het equivalente land,

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

Het programma maakt gebruik van het radioklimaat van de gekozen route zoals in § 10 sexies (tot 1.3.4 altijd de gematigde zee, met N0 320 op het land en 350 op zee), met N0 van de gemiddelde reactiviteit van de plaats (bodem εr 15, σ 0,005 S/m; zee εr 81, σ 5 S/m), horizontale polarisatie en variabiliteit in de tijd zonder die plaats (het profiel is bekend). Hieruit blijkt dat het verlies in 50% van de tijd en het verlies in 10% en 1% van de beste tijd is overschreden. ITG modelleert de kanalen niet: de variabiliteit in de tijd is statistisch, per klimaat. Het dient als een tweede berekening van de basislijn naast P.452; het verschil tussen beide geeft een idee van onzekerheid.

Het tweede gebruik is het laatste stuk na een leiding die eindigt voor de overeenkomstige. Bij het uitgangspunt wordt een virtuele antenne halverwege de leiding geplaatst en het ITG berekent het traject naar de overeenkomstige antenne, als overmaat over de vrije ruimte bij 50% van de tijd. Totaal verlies is

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,	heta_{condotto} + A_{ITM}(d_{uscita} 	o RX)
```

waarbij A_TX de koppeling is van de antenne (3 dB binnen, 1 dB buiten, 1 dB om de 10 m buiten) indien de leiding dichtbij begint, anders de diffractie van § 10 ter. Het is de limiet die de auteur zichzelf heeft gegeven: continuïteit van de laag plus, hoogstens, resterende demping. Als het lager is dan het verwachte verlies wordt het het mechanisme ("conduct (exit and final stretch ITG)").

## 10 quinquies. Verdieping: eilanden, zee-engte en aardse zee-ontsluiting

Een voorbeeld van de Golf van Napels (Anacapri, JN70CN) naar het westelijke Middellandse Zeegebied toont waar routes over de eilanden:

| Bestemming | Azimut | Waar het de eilanden oversteekt | Obstakel |
| --- | --- | --- | --- |
| Barcelona |  279°  | ten noorden van Sardinië (Gallura) tot 41,05° N, tussen 420 en 490 km | ongeveer 30 km ten zuiden van de Bocche di Bonifacio; de Limbara (1359 m) ongeveer 20 km van de lijn |
| Bocche di Bonifacio |  282°  | de smalle tot 41,3° NB | ongeveer 11 km |
| Marseille |  296°  | Centraal Corsica bij 42,2° NB | bergen tot 2700 m: er stopt een leiding onder 2 km |
| Valencia, Balearen |  260–265°  | Centraal Sardinië | interne reliëfs |

Voor Barcelona is het genoeg om te wijzen naar 282° in plaats van 279/° omdat de as uit de monden gaat: 3e, binnen de straal van een normale yagi.

Zijn de monden lastig? Voor de lage kanalen, onder een paar honderd meter, ja: de eilanden zijn ommuurd, en boven de aarde, overdag, de verwarming breekt de inversie. De straat zelf is breed genoeg: ongeveer 11 km, tegen een eerste oppervlakte van Fresnel, minder dan een kilometer breed, de helft van een 1000 km route in 144 MHz. Het probleem is het weer. De Mouths zijn leuk voor de wind: De onderste laag wordt versneld en de onderste laag wordt verwijderd. Vaak is de passage open als geometrie maar verstoord als atmosfeer. Met een stabiele anticycline en weinig wind kan in plaats daarvan de goede doorgang zijn.

Aardzeeverschillen. Ze tellen. Een leiding gevormd aan de Tyrreense kust wordt onderbroken: het signaal komt uit de leiding (het gedeelte na de uitgang, § 10 quater) en moet terugkeren naar een andere leiding op de zee van de Balearen, betalen opnieuw de koppeling. De leidingen op hoogte, boven 1500 m, passeren over de Gallura zonder het te beseffen; voor centraal Corsica is niet van toepassing.

In de praktijk: als de route een bergachtig eiland oversteekt, probeer dan wat correctie in de richting van het stuk vrije zee, maar reken niet op de passage met sterke wind in de smalle.

## Tien sexies. TN101 radioklimaat en tijdvariabiliteit (vanaf 1.3.5)

De TN101 § 10 [25] beschrijft hoe het verlies van een route voorbij de horizon varieert over de lange periode rond de mediaan, met verschillende curves per klimaat: de functie Y(q) zegt hoeveel dB het verlies daalt in de q% van de beste tijd. Het ITG bevat deze rondingen voor zeven klimaten. Het klimaat telt veel: op zee in het gematigde mariene klimaat, op 144 MHz, in 10% van de tijd zijn er veel meer dB dan op het land in het continentale klimaat, omdat de kanalen vaker voorkomen.

Het programma lost het klimaat niet meer op: Hij kiest het voor elke route vanaf het gemiddelde punt, dus het werkt over de hele wereld en niet alleen in Europa. Download van Open-Meteo [26] drie jaar aan dagelijkse waarden van ERA5 heranalyse (temperatuur, vochtigheid en bodemdruk, regen) voor de cel van 0,5° van het gemiddelde punt, slechts eenmaal; waarden blijven in Data/CLIMA. Herstel van de bodem

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

dag na dag, dan het jaarlijkse gemiddelde en excursie tussen maandelijkse gemiddelden, gemiddelde vochtigheid en jaarlijkse regen. De regels volgen de beschrijvingen van de klimaten van de TN101 § 10.2; de grenzen tussen het ene klimaat en het andere zijn een criterium van de auteur:

- over 60° breedtegraad: gematigd continentaal (de TN101 heeft geen curven voor het poolklimaat);
- minder dan 250 mm regen per jaar en gemiddelde vochtigheid onder 50%: woestijn (Ns typisch 280);
- binnen 20° van de evenaar, Ns van ten minste 350 en excursie tot 30: equatorial (Ns typerend 360);
- binnen 32° en excursie van ten minste 60: subtropisch continentaal, dat wil zeggen moessons (typische excursie 60/100);
- tussen 10° en 32° met Ns van ten minste 340: subtropische marine (Ns typerend 370);
- beide horizonten op zee, meer dan 20°: gematigde zee op zee (het is de definitie van de TN101);
- excursie tot 30 nabij de zee (zee op de route of gemiddelde vochtigheid van ten minste 75%): zeegetemperd op aarde;
- anders gematigd continentaal (typische excursie 20

N0 voor ITG is het gemiddelde gereduceerd tot zeeniveau, N0 = Ns·e^(h/9460). Zonder netwerk wordt een reserveregel gebruikt met alleen de breedtegraad en de zeefractie, en de lijn zegt het.

Tests met gegevens ERA5 ››2025: Capri Ns 339 met excursie 45 (continentaal getemperd op de aarde, zee gematigd wanneer beide horizonten zijn op de zee); London 327 en 16 (getemperde ster); Wenen 319 en 24 Abidjan 381 en 11 (equatoriaal); Miami 365 en 36 (subtropische echtgenoot); Delhi 337 en 82 (subtropisch concontinentaal); Riad 278 en 37, 90 mm regen (woestijn). In Chili: Antofagasta 341 en 19 (subtropische echtgenoot: de kust van Atacama, met de vrijwel vaste lage achterkant van de Stille Oceaan), Calama 232 op de grond, 2257 m, 14 mm regen (woestijn), Valparaíso 331 en 11 (temperatuur naad), Punta Arenas 313 en 5 (temperatuur naad).

De lijn "Radio klimaat ( TN101) " toont het klimaat, de reden, Ns met de helft van de excursie en Y: hoeveel dBs verdienen in 10% en 1% van de tijd en hoeveel verliezen in 90%. Het is lange termijn statistieken en blijft gescheiden van de prognose: het verwachte verlies volgt op de tijd van vandaag met de GFS profielen, en som de twee dingen zou twee keer dezelfde abnormale voortplanting tellen.

## 11. Vergelijking met ITU-R P.452

De vergelijkingslijn P.452 past de ITU-methode voor kanalen en reflectie op lagen (P.452-16 § 4.5 [6]) toe met de ΔN mediaan van de GFS in plaats van de klimatologische en p = 20% als kalibratieconstante, zoals in het onderzoek CSIRO [10]. Het omvat het verlies van empirische koppeling Alf onder 0,5 GHz (ongeveer 28 dB tot 144 MHz) Het is slechts een vergelijking: het verwachte verlies blijft dat van paragraaf 9.

## 11 bis. Parabolische vergelijking (vanaf 1.3.1)

De Parabool Equation Line is een vierde berekening, alleen van vergelijking, gemaakt als professionele programma's voor de kanalen (PETOOL; de golf-propagatie kader van M. Lytaev, MIT licentie): in plaats van het schatten van de winst van de lagen, het beweegt de golf langs de route met de Fourier split-step methode binnen de M gemodificeerde GFS brekingsprofiel, genomen van het prognosepunt dichter bij elke afstand. De GFS niveaus worden toegevoegd aan de geschatte sprongen van de potentiële kanalen (berekenen A, 50 m in halve laag). De grond Copernicus GLO-30 treedt stappen (onder de grond het veld is nul), de grond is geleider en de straal van de antenne is een Gaussian met de geschatte opening van de winst. De berekening ziet de geleiders, degenen die verliezen, de diffractie en de koppeling van de antennes; Hij ziet de troposcatter niet, die apart wordt toegevoegd. Volgende is de "profile cheque": hetzelfde met standaard sfeer minder degene met het verwachte profiel.

van 1.3.2:

- de propagator zich in een brede hoek bevindt, met de exacte fase van de vrije ruimte:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- de bodem op zee en op de kustvlakte op nulhoogte weerspiegelt met de coëfficiënt van Fresnel in horizontale polarisatie, εc = εr − j·60·λ·σ (zee εr = 81 en σ = 5 S/m; land 13 en 0,006 S/m), onder de duidelijke hoek van de route:

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- de ruwheid van de zee vermindert de consistente reflectie volgens Miller en Brown, met de effectieve hoogte van de golven van de wind tot 10 m (σh = 0,0051·U2):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- op zee omvat profiel M het verdampingskanaal (punt 9 ter) met het neutrale logaritmeprofiel:

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

Het test met het prototype (10 m antenne op de zee, winst op de standaard atmosfeer op 100 Het verdampingskanaal van 10/30 m geeft 1/3 dB tot 144 MHz, 8/37 dB tot 432 MHz en 21/66 dB tot 1296 MHz. Bij 144 MHz in horizontale polarisatie reflecteert de zee bijna als metaal (ongeveer 0,99 tot enkele tienden) en ruwheid telt weinig: de nieuwe bodem verandert enkele tienden van dB; het telt meer van 432 MHz tot.

Grenswaarden: boven de aarde blijft het verticale profiel dat van de GFS-niveaus, zodat de dunne kanalen onzichtbaar blijven; de grond tot treden wordt benaderd; de straal blijft binnen de 6e.

## 12. antennes, signaal en geluid

- Yagi DL6WU: winst 9,5 + 7,8·log10(boom/λ) dBd (2.35 dB voor dubbele giek, DL6WU metingen op VHF-communicatie 3/1982), plus 2.15 voor [12] dBi.
- EIRP = power − kabel + TX gain.
- Signaal ontvangen = EIRP − verwacht verlies + winst RX − kabel RX. S-meter IARU: S9 = −93 dBm, 6 dB per eenheid.
- Externe ruis volgens ITU-R P.372 [13]: galactisch (Fa = 52 − 23 log f) meer kunstmatig volgens de omgeving (stad, residentiële, landelijke, rustige landelijke) of een handgeschreven antennetemperatuur.
- Tsys = Ta + Tpre + [290(Lc − 1) + 290(Fr − 1)·Lc] / Gpre naar de terminals van de antenne (Friis formule). Vanaf 1.3.5 heeft het ontvangststation altijd een voorversterker in antenne, vaste parameter: ruisnummer 0,8 dB (Tpre = 59 K) en winst 20 dB. Achter de kabel (loss Lc) en de radio, met basis Fr noise nummer 6 dB. Met 1 dB kabel daalt de kabel en radiobijdrage van ongeveer 1160 K naar 12 K: het geluid bepaalt de lucht, het milieu en de preamp. De winst van 20 dB is een typische waarde gekozen door het programma.
- S/N in SSB, CW en FT8 in de band van elke manier, vergeleken met de drempelwaarde.
- Waarschijnlijkheid voor de periode (vanaf 1.3.2): de evanescence binnen het uur is een normale verdeling van de S/N rond de verwachte gemiddelde uurwaarde. Bray (QST, november 1961, blz. 39, op de bochten van Norton 1959) geeft een amplitude van 13,5 dB over ongeveer 100 mijl, d.w.z. ongeveer 7 dB onder en 7 boven het gemiddelde, die bijna recht onder 100 mijl valt (3,5 dB tot 50 mijl); voor een 'vaste' verbinding neemt het 7 dB uit het budget. Overgenomen ±7 dB als ±2σ: σ = 3,4 dB over 160 km, evenredig met de afstand hieronder. De kans dat een periode de drempel van de weg overschrijdt is

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  met Φ normale distributiefunctie: marge 0 dB = 50%, +3,4 dB = 84%, +7 dB (de 'vaste' marge van Bray) = 98%. Het omvat niet de onzekerheid van het model, dat groter is.
- Digitale manieren: S/N in 2500 Hz vergeleken met de drempels gemeten bij 50% decoderen, zonder voorafgaande informatie (AP). FT8 −21 dB en FT4 −17,5 dB op wit geluid; wanneer het mechanisme troposcatter is, gebruik dan de drempels met evanescence "verstoorde breedtegraad medium," FT8 −18,5 dB en FT4 −15,2 dB [21]. Q65 -15C −22,2 dB, Q65 -30B −24,8 dB, Q65 -60A −27,6 dB In vliegtuigen verstrooit de Doppler kan de tonen van Q65 -60A overschrijden: er zijn beter dan 30B of 15C. JT65A −25 dB [22]. De decodering AP (naam van de reeds bekende of einde van QSO) geeft ongeveer 2 dB extra voor FT8 en 1,5 tot 2,6 dB voor Q65; het komt niet in de drempel omdat het niet geldig is voor alle De.−24 dB [24] FT8

## 13. Respons en datatijden

Elke berekening meet zijn eigen tijd: GFS profielen (netwerk, analyse, verplichte breuken van 20 s tussen de partijen voor de Open-Meteo limiet), Copernicus GLO-30 terrein (met de download van de ruiten, 15 De Response Time regel vat ze samen; in het register beginnen de rijen met TEMPI. Bij het upgraden van het raster, meldt het register ook de gebruikte GFS race, toen het beschikbaar werd en hoe oud de gegevens zijn.

Vanaf 1.3.2 kunt u ook kiezen voor ICON-2I, het model van het Agentschap ItaliaMeteo en Arpae op Italië op ongeveer 2,2 km, door Open-Meteo (model italia_meteo_arpae_icon_2i, twee wedstrijden per dag, 3 dagen). Het bestrijkt alleen Italië en de nabijgelegen zeeën en heeft minder lage niveaus van de GFS (1000, 925, 850, 700, 500 hPa): voor de kanalen op hoogte is niet beter dan de GFS, maar het fijne raster beschrijft goed kusten, eilanden en zee op 2 m, dat is de verdampingskanaal. De variabelen die een model niet heeft (snelheidslimiet, temperatuur van het oppervlak...) verwijderen zich van het verzoek. Vanaf 1.3.2 gebruikt de automatische bron alleen NOMADS; Open-Meteo dient als NOMADS niet reageert en voor historische gevallen. Met de "Update by self" box (opgelicht door defect) wordt de prognose ongeklikt: bij de opening, bij elke nieuwe GFS race op NOMADS (ongeveer 5 uur na de tijd van de wedstrijd) en wanneer de zichtbare zone komt voor meer dan een kwart van het raster; hoogstens een poging om de 15 minuten, nooit met een historisch geval.

Aanvragen bij Open-Meteo hebben limieten per minuut, per uur en per dag voor elk internetadres. De gedownloade profielen worden opgeslagen op de schijf (Dati/cache-gfs) voor elke slag en model: met dezelfde race, die elke 6 uur verandert, vragen de reeds gedownloade punten niet opnieuw. Met de automatische bron start je bij NOMADS en Open-Meteo alleen voor ontbrekende punten; met de enige Open-Meteo, op de tijdslimiet of dagelijks stopt onmiddellijk en zegt wanneer je het opnieuw moet proberen. Historische zaken worden niet gecached.

## 14. Valideren van het model

De prognose moet worden vergeleken met echte verbindingen. De berekening log houdt elk nummer; het programma archief houdt spot en decoderen. Voor 432 en 1296 MHz, waar de spots weinig zijn, hebben ze rapporten nodig van de operators: wie maakte de verbinding, hoe laat, met welk signaal. Vergelijk gedurende vele uren de verwachte winst met het gemeten signaal geeft aan of de drempels moeten worden verplaatst.

## Bronnen

1. NOAA/NCEP, Global Forecast System (GFS), wereldwijd model op 0,25°, vier vluchten per dag.
2. Open-Meteo, voorspelling API (open-meteo.com), gfs_naadloos model, variabelen op drukniveaus en racemetadata.
3. © Airbus Defence and Space GmbH 2014 Reserve: NASA/USGS Copernicus GLO-30 in Terrain Tegels "skadi" van AWS.
4. ITU-R P.453, De radio-brekingsindex: de formule en brekingsdatum, met digitale kanaalkaarten (Studiegroep 3, © ITU, persoonlijk en niet-commercieel gebruik).
5. B. R. Bean, E. J. Dutton, Radio Meteorology, NBS Monograph 92, 1966: Tabel 6.5 en § 4.4.
6. ITU-R P.452-16, Voorspellingsprocedures voor de evaluatie van interferentie tussen stations op het aardoppervlak bij frequenties boven ongeveer 0,1 GHz: § 4.2 (diffractie), eq. 45 (troposcatter), § 4.5 en eq. 49 (geleid).
7. ITU-R P.834, effecten van troposferische refractie op radiogolf voortplanting.
8. ITU-R P.525, Berekening van de demping van de vrije ruimte.
9. ITU-R P.526, Propagation by diffractie: eq. 31 (cutcheon) en § 3 (sferisch land).
10. B. Indermuehle, H. Suzuki, Tropospheric Ducting Prediction gebaseerd op GFS Model Data in Inyarrimanha Ilgari Bundara, de CSIRO Murchison Radio-astronomie Observatory, Publicaties van de Astronomische Vereniging van Australië, 2026 (arXiv 2608.12861).
11. D. E. Kerr, Propagation of Short Radio Waves, MIT Radiation Laboratory Series vol. 13, 1951: eq. 17 e 18.
12. G. Hoch DL6WU, 1 (gemeten 432 en 1296 MHz volgens de lengte van de giek).
13. ITU-R P.372, Radiolawaai: galactisch en kunstmatig lawaai voor het milieu.
14. NOAA/NCEP, NOMADS, GRIB filter van de GFS 0,25° tijd (nomads.ncep.noa.gov); WMO G RIB2 formaat, complexe verpakking met ruimtelijke verschillen.
15. Open-Meteo, Historical Forecast API (modelvoorspelling archief van rond 2021).
16. LukiBox, TropoLink, ontwerp van troposcatter routes met P.617, NBS TN101 en ITG flanked (GitHub, Apache 2.0 licentie).
17. ITU-R P.617-5, Propagatievoorspellingstechnieken en gegevens die nodig zijn voor het ontwerp van trans-hirizon-radio-relaysystemen, 2019.
18. RSGB, VHF/UHF Handboek; DL2FZN implementatie (andi-f/troposcatter op GitHub, 2013).
19. W. F. Moler, D. B. Holden,
20. ARRL Handboek 2022 (99a ed.), § 19.4.3
21. S. Franke, B. Somerville, J. Taylor, The FT4 and FT8 Communication Protocols, QEX juli/augustus 2020.
22. W3SZ, digital slow mode drempels ( JT65, JT9, QRA64) en modus keuze volgens voortplanting, 2019.
23. QST, "The World Above 50 MHz," november 2022: opening van 14 maart 2022 tussen Manitoba en Kansas/Missouri.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Quick-Start Guide to Q65, 3 April 2021 (wsjt.sourceforge.io).
25. P. L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis, Transmission Loss Predictions for Troposferic Communication Circuits, NBS Technical Note 101, magazine 1967: § 9 (troposcatter, functie F(θd), H0), § 10 en bijlage III (radio klimaten en variabiliteit in de tijd).
26. Open-Meteo, Historisch weer API (ECMWF ERA5 review sinds 1940; Copernicus Climate Change Service, CC BY 4.0 licentie).
