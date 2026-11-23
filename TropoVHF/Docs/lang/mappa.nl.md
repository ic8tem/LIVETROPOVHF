LIVETROPOVHF · technisch document 2
# Bouw van de kaart

> Machinevertaling uit het Italiaans: bij twijfel geldt de Italiaanse handleiding.

Als de kaart van het tabblad Kaart en spot wordt geboren: de geografische basis, de projecties, het gemiddelde van de Hepburn en F5LEN indices, de isobare, de plekken en stations luisteren, overlapt met lagen. Versie van 9 november 2026.

## Samenvatting

- De kaart is ontworpen door het programma in acht lagen, elk met zijn [8] box.
- Basis: Natuurlijke aardkust en grenzen op twee niveaus van [9] detail; [10] Markt en azimut projecties.
- Natuurkunde: inversies, reactiviteit N en gradiënt dN/dh, doorvoerdrempel bij −157 N/km [3] [4] [7] [11].
- Keelindex: gekalibreerde meting van Hepburn [1] en F5LEN [2] kaarten, gemeenschappelijk raster en gemiddelde van de twee bronnen.
- Isobare: GFS [12] druk via Open-Meteo [15], lijnen elke 4 hPa, H en L centra.
- Spot, luisterstations (donker roze cirkels met altijd zichtbare roze lijnen) en persoonlijke decoderingen in het rood.
- Het lezen van de overlapping: hoge druk, hoge kanalen, drukdaling, groeiende SNR, [3] [4] [5] [13] [14] bergroutes.

## 1. Het principe: een gelaagde kaart

De kaart is geen vaste afbeelding: is ontworpen door het programma, laag over laag, wanneer u verandert van zoom, projectie, band, prognosetijd of thema. Elke laag heeft zijn box in de bovenste laagbalk op de kaart, dus je kunt alleen vasthouden wat je nodig hebt en alles samen zien als je de volledige afbeelding wilt. De volgorde, van benedenaf, is dit:

|  #  | Weg | Inhoud | Casella |
|  1  | Basiskaart | Zee, land, kust en grenzen | Ja. |
|  2  | Indextropo | Media Hepburn + F5LEN, of slechts één bron; instelbare transparantie | Ja. |
|  3  | Grilllocator | Velden, vierkanten en, met zoom, vierkant | Ja. |
|  4  | Isobare | Druklijnen om de 4 hPa, H en L centra | Ja. |
|  4a  | Temperatuur 850 hPa | Isothermische behandeld elke 4 °C | Ja. |
|  4b  | Thermische inversie | Oranje monster waarbij de lucht op hoogte warmer is dan op de grond | Ja. |
|  5  | PSKReporter | Ontvangststations van PSKReporter (donkerroze cirkels) en, in roze, wat ze horen | Ja. |
|  6  | Door mij gedecodeerd | Decoderen stations met WSJT-X, JTDX of MSHV: Rode lijnen van uw QTH | Ja. |
|  6a  | QSO desunti dal live | Gesloten verbindingen ( RR73, 73, RRR) gelezen in uw decoderingen: groene lijn tussen de twee stations | Ja. |
|  7  | Spotlight | Verbindingen: volledige blauwe lijn, geel bedekt voor luchtscatter, twee ballen | Ja. |
|  7a  | Exclusief | Casella Uitgesloten tot minder dan 100 km: het kortste verkeer wordt niet getrokken noch genoteerd. Casella Meer dan 1000 km: alleen vierkantjes (bij opstarten): als een van de stations meer dan 1000 km van uw QTH ligt, geen lijnen of cirkels, alleen de locatiepleinen van de stations die betrokken zijn bij lilac behandeld op 45° | Ja. |
|  8  | QTH en etiketten | Uw station, de namen van de geselecteerde plek | altijd zichtbaar |

De tropo index is direct boven de basis en onder al het andere: is een gekleurde steekproef, en hoger geplaatst zou betrekking hebben op lijnen en symbolen. Het raster passeert het, zodat je altijd weet waar een gekleurd gebied valt. De isobares zijn dunne lijnen en lezen ruim boven de bemonstering.

## 2. De basiskaart

Kosten, grenzen en land zijn afkomstig van natuurlijke aardvectorgegevens, van openbaar domein [9], tot schaal 1:50 miljoen voor totaalbeeld en 1:10 miljoen bij vergroting. De bestanden bevinden zich in de programmamap: geen online kaartdiensten. Kleuren van het thema Helder: lichtblauwe zee # E2ECF8, donkerblauwe kustlijn, lichtgrijze randen. In de thema's Zwart en Nacht worden de kleuren van de basis verduisterd, in de andere thema's kleuren ze lichtjes.

## 3. De twee projecties

Alle lagen gaan door dezelfde projectiefunctie, zodat ze altijd op elkaar afgestemd zijn. Je kiest uit twee [10] projecties (λ lengtegraad, φ breedtegraad, R straal van de Aarde):

Markt. lokale hoeken en vormen opslaan; De afstanden strekken zich uit naar de palen. Knip tot ±85° breedtegraad.

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

Azimutale equidtanti gecentreerd op uw plein. Het centrum (φ0, λ0) is het centrum van uw WWL-vierkant. Afstanden en richtingen gemeten vanuit het centrum zijn waar: een station op 1200 km is 1200 km in schaal, en de hoek met het noorden is de azimut waarop de antenne wijst. Wanneer u de locator wijzigt wordt de kaart opnieuw gericht.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

In de azimut worden de lijnen op het papier recht (meridianen, parallellen, vierkante randen) gebogen. Om deze reden zijn alle lijnen verdeeld in korte secties, wordt elke sectie geprojecteerd en dan worden de eigenschappen verenigd; hetzelfde geldt voor de randen van de tropo-index en voor de isobare. Het punt naar de antipoden van het centrum heeft geen duidelijke positie en blijft uit het zicht.

## 4. Grill locator, zoom en beweeg

Het Maidenhead-raster is ontworpen door het programma: velden van 20° × 10° met de twee grote en vervaagde letters in het midden, vierkanten van 2° × 1e, en met voldoende zoom van 5′ × 2,5′. De lijnen zijn blauw en dun, niet om de tropo index te bedekken.

Zoom met het wiel gecentreerd op de pointer (het punt onder de muis blijft stabiel), bewegende slepen, dubbele klik om terug te keren naar het hele uitzicht, + en − toetsen, en de sleutel met het percentage voor het hele uitzicht. Details passen bij de zoom: fijnere kusten, sub-vierkanten, dikkere isobare etiketten.

## 5. Waarom inversies de golven buigen

In de lage atmosfeer daalt de temperatuur normaal gesproken ongeveer 6,5 °C per km; De brekingsindex is afhankelijk van zowel temperatuur als [3] vocht, en waar het sterk verandert met de hoogte de golf buigt naar de grond en komt verder dan normaal. De belangrijkste laag is dat tot ongeveer 3 km, waar inversies, kanalen en convectieve cellen [4] worden gevormd; zeepaden worden begunstigd omdat de watermassa's vormen grote gebieden van [4] inversie. [3]

De grootte die wordt gebruikt om het te meten is brekings-N, d.w.z. (n − 1) · 106 [11]:

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

T is de temperatuur in kelvin, P druk en stoomdruk, beide in hPa.

Telt als gevarieerd N met aandeel (dN/dh). In de standaardatmosfeer is ongeveer −39 N/km; tot −79 N/km wordt de refractie als normaal beschouwd, naast superfractie, en met positieve gradiënten van subfractie [7]. Onder −157 N/km is de golf meer gebogen dan de kromming van de aarde en blijft gevangen: het is de [11] (afgeleide) kanaal. F5LEN

## 6. De tropo-index: van twee kaarten tot het gemiddelde

Hepburn en F5LEN tabs zijn de control desk: toon de afbeeldingen gedownload van de pagina van elke bron, met de bewerkbare prognosetijd, en dienen om de lezing te kalibreren door te klikken op de referentiepunten en kleuren van de legende. De tropo-indexlaag blijft leeg totdat ten minste één van de twee kalibraties niet actief en voltooid is.

Hepburn. De Hepburn-index meet hoeveel golven naar verwachting over een gebied worden gevouwen, op een lineaire schaal van 0 tot 10, die ook meer dan 10 kan bedragen. 0 betekent standaard atmosfeer en alleen scatter; 1 De kaarten hebben betrekking op 6 dagen: 3 uur passen voor de eerste 36 uur, dan 6 uur, elke dag bijgewerkt tot 18 UTC [1]; de startgegevens (temperatuur, vochtigheid, druk) komen van het wereldwijde model van de Canadese weerdienst [1]. [1] [1]

F5LEN. Voor Europa publiceert F5LEN 31 kaarten van +3 tot +192 uur (elke 3 uur tot +48, elke 6 tot +96, dan elke 12), met de datum en tijd geldig in de titel, de productietijd rechtsonder en een klasse legende: Disturb, Normaal, Minor, Zwak, Goed, Sterk, Vy Strong, Duct [2]. De wereldvoorspelling van F5LEN wordt berekend door de luchtbrekingsindex in Eenheid N, op 40 stappen van 3 uur (ongeveer 5 dagen), 4 keer per dag bijgewerkt; een klik op de kaart toont de gemiddelde waarde en de locator van dat punt [2].

Ik ga verder. Voor elke voorspellingstap voert het programma deze stappen uit:

1. Ontslag. De afbeeldingen van de twee voorspellingen worden gedownload in de cache naast het uitvoerbare bestand, eenmaal om de bron te updaten, niet om een nieuw ontwerp. Voor de definitieve versie, moet toestemming worden gevraagd aan Hepburn en F5LEN.
2. Automatisch georefereren. Het programma vindt het frame (twee horizontale en twee verticale lijnen van dezelfde kleur) en, op Windows 10/11, leest met de OCR geïntegreerd in het systeem de as etiketten (54N, 10W, 0, 5E...). Snijd de stroken rond het frame, vergroot ze, test beide tekens wanneer de letter N/S/E/W niet leest en houd de lijn waarop de meeste labels vallen: de verkeerde waarden blijven uit. Vanaf hier krijg je de extreme lat/lon van het frame en het type projectie (regelmatige grill of Mercator). De extremen sparen zichzelf en herlezen zichzelf als het frame van grootte verandert. Op Mac en Linux, zonder OCR, fabrieksextremen of handgeschreven degenen zijn geldig in het tabblad. Als alternatief, voor kaarten zonder leesbare assen: Elk type kaart (Hepburn Europa, F5LEN wereldwijd...) heeft een kalibratie: sommige controlepunten met bekende coördinaten (kruisingen van meridianen en parallellen, herkenbare ribben) verbinden pixels met breedte- en lengtegraad, in de projectie die door die bron wordt gebruikt. De kalibratie bevindt zich in een bestand naast het programma en u kunt opnieuw maken als een bron verandert van formaat.
3. Kleuren lezen. Elke pixel wordt vergeleken met de bronlegende en omgezet in de dichtstbijzijnde numerieke waarde. De kosten, geschriften en roosters die op de oorspronkelijke kaart worden getrokken, worden herkend als geen gegevens en worden gevuld met de nabijgelegen waarden.
4. Wereldnet. Elk beeld, zodra het frame is gevonden en bekend is met zijn extremen, wordt op Aarde geplaatst; de waarde op elk punt in de wereld wordt afgeleid van de beelden die het bedekken. Als meer regio's hetzelfde punt bestrijken is het de moeite waard detail (meer pixels per graad), mits de tijd binnen 1,5 uur van die keuze; dan het dichtstbijzijnde uur, dan de meest recente race. Waar geen afbeelding het punt beslaat toont de kaart een grijze sluier: geen gegevens, die verschilt van de index 0. In het frame is een pixel zonder legendekleur 0 (normale atmosfeer); kusten, randen en geschriften worden gesprongen door te kijken naar de dichtstbijzijnde kleur binnen 3 pixels.
5. Dezelfde ladder. Hepburn is al op de schaal 0 F5LEN gebruikt klassen (Disturb, Normaal, Minor, Zwak, Goed, Sterk, Vy Strong, Duct) [2], maar sommige hebben precies Hepburn kleuren: Zwak = 1, Strong = 5, Vy Strong = 7, Duct = 8; Minor is 0,5 en Good (green) 3.5. De schaal multiplier, in de F5LEN tab, corrigeert de set om het vergelijkbaar te maken met Hepburn. [1]
6. Zelfde tijd. De tijdbalk gaat van +0 tot +192 uur in stappen van 3 uur. Voor elke stap die u neemt van elke bron de prognose geldig op het dichtstbijzijnde moment; Als een bron niet die stap, de media gebruikt alleen de andere en de legende geeft het.
7. Media. Met de Media knop is elk punt de moeite waard (Hepburn + F5LEN) / 2; met Hepburn of F5LEN zie je slechts één bron, nuttig om te begrijpen waar de twee voorspellingen het niet eens zijn.
8. Tekenen. De waarde wordt gekleurd met de legendeschaal (0 blauw, 2 groen, 4 geel, 6 oranje, 8 rood, 10 paars) en geprojecteerd als een steekproef onder het raster. De 0 is transparant, dus waar niets gebeurt zie je de schone basiskaart; algemene transparantie past zich aan met de cursor naast het vakje.

Op de spot tab, de index langs de route is het gemiddelde van de competitie raster waarden elke 10 km op de maximale cirkel tussen de twee stations.

## 7. Isobare

De hoge stabiele druk is het eerste signaal dat wordt gezocht voor de [3] trope, en de verbindingen buiten de IK3XTV geanalyseerde Alpen zijn ook met hoge [4] druk.

1. Data. Druk op zeeniveau vanaf het wereldwijde model GFS van NOAA [12], lees via de gratis Open-Meteo [15] -dienst op een rooster van 3° × 3° (van 30° tot 72°) NB, van 30° WL tot 48° OL), gedurende de volgende 78 uur. Het programma update de gegevens bij het opstarten en vervolgens elke 6 uur, zoals het model racen.
2. Lijnen. De isobare komt van het raster om de 4 hPa (...1012, 1016, 1020...) met het algoritme van de marcherende vierkanten (Marching vierkanten), dan zijn ze licht afgerond en geprojecteerd getrokken per tract.
3. Centra. De meest gemarkeerde lokale maxima en minimumwaarden zijn gemarkeerd met H (blauw) en L (rood) en de waarde in hPa.
4. Labels. De waarde verschijnt langs elke isobara, op een kleur achtergrond van de kaart om leesbaar boven de bemonstering te blijven.

Zone en stap. De gegevens worden gedownload op het gebied waar u naar kijkt, uitgebreid met 25%, met een stap van 1e naar 20° gekozen om onder 400 punten te blijven (Open-Meteo telt elk punt als een verzoek: 600 per minuut, 5000 per uur): vergroten of verplaatsen van de kaart uit het gebied al downloadt het programma de meest dichte herlading, tot het maximum elke 10 minuten; als een update mislukt de vorige gegevens blijven en het zal opnieuw proberen na 3 minuten. Op deze manier isobare en temperaturen werken overal ter wereld.

Temperatuur. Dezelfde aanvraag brengt de temperatuur op 2 m, 925 hPa en 850 hPa. LA0BY toont aan dat de gebeurtenis van 22/11/2018 tot UA6 werd gezien in temperatuurkaarten (900 Hiervoor zijn er twee lagen: [3] DF5AI DK5YA [5]

- Temperatuur 850 hPa: isoterme behandelen elke 4 °C (rood boven nul, blauw onder, paars op nul).
- Thermische inversie: oranje sampling waarbij de lucht bij 925 of 850 hPa warmer is dan die bij 2 m; hoe intenser, hoe meer inversie is sterk. Het is de fysieke conditie die de golven buigt naar de grond [3] [11], en vergelijk het met de tropo index en isobare toont of de voorspellingen een weerbasis hebben.

## 8. Vlekken en stations

Spot. Elke plek met beide bekende posities is een lijn op de maximale cirkel tussen de twee stations, verdeeld in secties en geprojecteerd; in Mercatore lijkt het dan licht gebogen, in de azimutale is het recht als het begint vanuit het centrum. Blauwe lijn vol voor de normale vlek, geel bedekt met donkere rand voor de luchtscatter (<AS>, <ACS> of "airscatter" in het commentaar), een bal voor elk station, licht halo onder de lijn om los te maken van de bemonstering. De geselecteerde plek is vaker ontworpen voor de laatste en met de twee namen.

Luisteren. Elke PSKReporter [16] ontvanger station is een donker roze lege cirkel. Van elke start volledige roze lijnen naar de stations die hebben gedecodeerd en teruggestuurd naar het netwerk, altijd zichtbaar zonder te klikken, met de namen altijd geschreven. Door een cirkel te selecteren zijn de lijnen dikker. Spot en luisteren tonen alleen de band en het bereik gekozen in de lijst: het veranderen van bandbreedte de kaart leegt en herontwerpt met de gegevens die al verzameld voor de nieuwe band.

Besloten door mij. Elk station dat decodeert met WSJT-X, JTDX of MSHV (receipt via UDP) is een rode lijn vol van uw QTH naar het station, met bal en nominatie. Het is de meest directe vergelijking met de voorspelling: toont waar je ontvanger echt nu komt.

## 8 bis. Kleuren en symbolen

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

Wat zijn de lila vierkantjes bedekt op 45°. Het verkeer weg van uw QTH interesseert u om te begrijpen waar de voortplanting is open, maar de lijnen, honderden kilometers lang en vaak tientallen, bedekken de kaart en verbergen wat telt voor u: de openingen die u kunt exploiteren van uw station. Om die reden tekent het programma, buiten de gekozen kroon, niet de lijnen, maar kleurt het de locator vierkanten (2° × 1°) van de betrokken stations. Het resultaat is een kaart van actieve zones: waar veel nabijgelegen lilac-vierkanten verschijnen, is er een geleiding of opening, zelfs als het u niet direct aangaat; Als het gebied zich naar u uitbreidt, is het het signaal om de antenne in die richting te wijzen. Het 45° dashboard laat je zien onder de tropo index en het raster, dus ze vergelijken in een oogopslag de voorspelling (onderkant kleur) en het echte verkeer (de lila). Uw QTH-vierkant kleurt nooit, omdat het het startpunt is en niet een ver gebied. Alleen de vierkanten die allemaal voorbij de kroon zijn gekleurd: een vierkant doorkruist door de kroon, of binnen, blijft vrij, zelfs als er een van de twee stations. Om de lijnen te zien, zet gewoon de doos uit of kies een grotere kroon.

De criteria die bepalen wat er op de kaart staat en wat wordt weggegooid, worden een voor een uitgelegd in document 1, sectie 11 ter.

## 9. De tijdbalk

Hieronder op de kaart: datum en tijd van de getoonde voorspelling, cursor van +0 tot +72 uur om 6 uur, index legende 0 Trompet en isobare index volgen de cursor; vlekken, stations luisteren en uw decoderingen zijn observaties van het moment en blijven altijd die Wanneer de cursor niet op +0 staat, worden ze verzwakt om te onthouden dat ze niet tot de prognosetijd behoren.

## 10. Hoe lees je de overlapping?

- Hoge druk + hoge index + plek langs het gekleurde gebied: de opening is er en is degene gepland. Het is tijd om die kant op te gaan.
- Hoge index maar geen plek: of niemand is daar actief, of voorspelling is te optimistisch. De luisterlaag helpt om onderscheid te maken: als er ontvangers in het gebied zijn en niets voelen, is de tweede hypothese waarschijnlijker.
- Lange vlekken buiten de gekleurde gebieden: hoge kanaal, die het oppervlak voorspelt niet te zien, zoals in de L.A. UA6 [3] gebeurtenis; of een andere voortplanting ontsnapte uit het filter (bijv. E sporadic zonder tag). DF5AI en DK5YA onderscheiden Tropo en E sporadic ook van het uur: openingen die geboren en eindigen samen met een diffuse E sporadic zijn nauwelijks [5] tropo.
- Hepburn en F5LEN zijn het oneens: vergelijk de twee bronnen met de knoppen en zie waar de echte vlekken vallen.
- Druk omlaag: G4MCU volgde een opening op het SK6VHF baken totdat de druk daalde; 1012 hPa beschouwd als de voltooide gebeurtenis [13]. Bekijk de isobare door de tijdbalk te scrollen: als de hoge druk wordt teruggetrokken, volgt de opening waarschijnlijk.
- Signalen die omhoog gaan: in de WSPR studio bij 144 MHz van KP4MD groeide de signaal/ruisverhouding precies in de vensters waar Hepburn een verbetering voorspelde en daalde toen de prognose [14] verwijderde. als op hetzelfde pad groeit, vormt zich de opening.
- Bergpaden: de index houdt geen rekening met de [1] reliëfs, maar de verbindingen buiten de Alpen bestaan ook dankzij convectieve cellen en golven van atmosferische zwaartekracht [4], en LA0BY werkte 9A en S5 door de bergen zonder Hepburn voorspellen [3].

## 11. Punt openen

- Toestemming van Hepburn en F5LEN om hun kaarten in het programma te gebruiken.
- Kalibratie van conversiecoëfficiënten F5LEN → schaal 0
- Kalibratie van controlepunten van elke soort kaart, om opnieuw te maken als bronnen van afbeelding veranderen.
- Facultatief traject met temperatuur op hoogte (850 hPa) van de GFS.

## Belangrijkste bronnen

1. W. R. Hepburn, DX Info Centre, Indexdefinitie en -schaal, negatieve waarden en meer dan 10, uitsluiting van verstrooiingen en reliëfs, stappen van 3 en 6 uur op 6 dagen, upgrade naar 18 UTC. Oorsprong van de gegevens van het wereldwijde Canadese model: G. Meisse, AMSAT-BB, 27/08/2007.
2. F5LEN, prognose voor Europa (tropo.f5len.org/forecasts-for-europe) en wereldvoorspelling van de VHF tropoferische propagatie (tropo.f5len.org/WW) en merkte op: "Nieuwe versie van het VHF troposphere propagation forwarding tool." Unit index N, 40 stappen vanaf 3 uur, 4 updates per dag, waarde en locator per klik.
3. S. Heck LA0BY, Extreme Tropo Propagatie op 144 MHz en hoger, 41° Nordic VUSHF Meeting, Skjeberg 2019. Normale helling 6,5 °C/km en inversies tot 10 UA6 S5
4. F. Egano IK3XTV, Belang van de eerste 3 km, zeepaden, convectieve cellen, zwaartekracht golven op de Alpen, hoge druk verbindingen, gemeenschappelijk volume tot 3
5. V. Grassmann DF5AI, U. Langenohl DK5YA, Very Long Distance Propagation in the 144 MHz Band Verticale profielen van temperatuur en dauw punt te herkennen inversies, gebruik van de Hepburn index, onderscheid tussen tropo en E sporadic volgens het uur.
6. J. N. Gannaway G3YGF, Tropospheric Scatter Propagation Earth radius equivalent en verlies van pad gebruikt voor index en spotkaart (zie document 1).
7. Het VHF/UHF DX-boek, hoofdstuk 2 - Troposferische verspreiding, blz. 2-23...2-30. Figuur van de brekingsgebieden volgens dN/dh (standaard −39 N/km, grenswaarden −79 N/km en 0) en oppervlakte- en hoge kanaalprofielen. De tekst van het bijgeleverde exemplaar is niet leesbaar: alleen de cijfers zijn gebruikt.
8. Projectsamenvatting van 8/11/2026, verstrekt door de auteur van het programma. Kaart kleuren, vereiste projecties, lagen, tijdbalk.
9. Natuurlijke aarde, vectorgegevens van kusten en grenzen, publiek domein (natural earthdata.com). Algemene verwijzing, niet bijgevoegd.
10. J. P. Snyder, Formules van Mercator projecties en azimutale equidtanti. Algemene verwijzing, niet bijgevoegd.
11. Aanbeveling ITU-R P.453, radio-brekingsindex. Refractaire formule N en kanaaldrempel bij −157 N/km. Algemene verwijzing, niet bijgevoegd.
12. NOAA NCEP, Global Forecast System (GFS). Druk op zeeniveau op 0,25°, vier vluchten per dag. Algemene verwijzing, niet bijgevoegd.
13. G4MCU, artikelen met tag "troposferic" (g4mcu.net/tag/troposferic), 2018 Repareren, geleiden en verstrooien; frequentere kanalen met hoge druk, vooral op zee; baken SK6VHF volgde op 1012 hPa (juni 2019).
14. C. F. Milazzo KP4MD, SNR van de WSPR rapporten aan 144 MHz groeien in de verbetering ramen voorspeld door Hepburn en dalen wanneer de prognose verdwijnt.
15. Open-Meteo, weersverwachting API (open-meteo.com), gratis niet-commercieel gebruik. Druk op zeeniveau van het GFS-model op meerdere punten in één verzoek.
16. mqtt.pskreporter.info, distributie MQTT in real time van de commercials van pskreporter. info: nominatie en verhuurder van ontvangers (zie document 1).
