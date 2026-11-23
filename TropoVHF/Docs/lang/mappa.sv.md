Lifetropovhf tekniska dokument 2
# Konstruktion av kartan

> Automatisk översättning av italienska: I händelse av tvivel är det värt den italienska guiden.

Som kartan över fliken Karta och plats är född: den geografiska basen, prognoserna, genomsnittet av Hepburn och F5LEN index, isobare, fläckar och stationer som lyssnar, överlappade med lager. Version av den 9 november 2026.

## Sammanfattning

- Kartan är designad av programmet i åtta lager, var och en med sin [8] -box.
- Bas: Natural Earth kust och gränser på två nivåer av [9] detaljer; [10] Market och azimuth prognoser.
- Fysik: inversioner, reaktivitet N och gradient dN/dh, ledning tröskel på −157 N/km [3] [4] [7] [11].
- Throat index: kalibrerad läsning av Hepburn [1] och F5LEN [2] kartor, vanligt rutnät och genomsnitt av de två källorna.
- Isobare: GFS [12] tryck via Open-Meteo [15], linjer var 4 hPa, H och L centra.
- Spot, lyssningsstationer (mörkrosa cirklar med alltid synliga rosa linjer) och personliga avkodningar i rött.
- Läsa överlappningen: högt tryck, höga kanaler, tryckfall, växande SNR, [3] [4] [5] [13] [14] bergslinjer.

## 1. Principen: en skiktad karta

Kartan är inte en fast bild: är utformad av programmet, lager över lager, när du ändrar zoom, projektion, band, prognostid eller tema. Varje lager har sin låda i den övre Layer-baren på kartan, så du kan bara hålla vad du behöver och se allt tillsammans när du vill ha hela bilden. Ordern, underifrån upp, är detta:

|  #  | Vägen | Innehåll | Casella |
|  1  | Grundläggande karta | Hav, land, kuster och gränser | Ja ja ja ja ja |
|  2  | Index tropo | Media Hepburn + F5LEN, eller bara en källa; justerbar transparens | Ja ja ja ja ja |
|  3  | Grill Locator | Fält, kvadrater och, med zoom, kvadrat | Ja ja ja ja ja |
|  4  | Isobare | Trycklinjer var 4 hPa, H och L centra | Ja ja ja ja ja |
|  4a  | Temperatur 850 hPa | Isoterm behandlade varje 4 °C | Ja ja ja ja ja |
|  4b  | Thermal inversion | Orange prov där luften på höjden är varmare än på marken | Ja ja ja ja ja |
|  5  | PSKReporter | Ta emot stationer av PSKReporter (mörkrosa cirklar) och i rosa, vad de hör | Ja ja ja ja ja |
|  6  | Avkodad av mig | Avkodningsstationer med WSJT-X, JTDX eller MSHV: Röda linjer från din QTH | Ja ja ja ja ja |
|  6a  | QSO desunti dal live | Stängda anslutningar ( RR73, 73, RRR) läser i dina avkodningar: grön linje mellan de två stationerna | Ja ja ja ja ja |
|  7  | Spotlight | Anslutningar: fullblå linje, gult täckt för airscatter, två bollar | Ja ja ja ja ja |
|  7a  | Exklusive | Casella Exkluderad under 100 km: den kortaste trafiken dras varken eller listas. Casella Mer än 1000 km: endast rutor (på start): om en av stationerna är över 1000 km från din QTH, inga linjer eller cirklar, endast locatorrutorna på de stationer som är inblandade i lila som behandlas vid 45° | Ja ja ja ja ja |
|  8  | QTH och etiketter | Din station, namnen på den valda platsen | alltid synlig |

Tropoindexet är omedelbart över basen och under allt annat: är en färgad provtagning, och högre placerade skulle täcka linjer och symboler. Nätet passerar över det, så du vet alltid var ett färgat område faller. De isobares är tunna linjer och läser bra över provtagningen.

## 2. Grundkartan

Kostnader, gränser och mark kommer från naturlig jord vektor data, av offentlig domän [9], att skala 1:50 miljoner för övergripande vy och 1:10 miljoner när förstoring. Filerna finns i programmappen: inga onlinekarttjänster. Färger på temat Clear: ljusblå havet # E2ECF8, mörkblå kust, ljusgrå gränser. I teman Svart och Natt färgerna på basen är mörkade, i de andra teman de färgar något.

## 3. De två prognoserna

Alla lager passerar genom samma projektionsfunktion, så de är alltid i linje med varandra. Du väljer mellan två [10] -prognoser (λ longitude, φ latitude, R-ray of the Earth):

Marknad. Förvara lokala hörn och former; Avstånden sträcker sig mot polerna. Skär till ±85° latitud.

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

Azimutale equidtanti centrerad på din torg. Centret (≤0, λ0) är centrum för din WWL-torget. Avstånd och riktningar som mäts från centrum är sanna: en station på 1200 km är 1200 km i skala, och vinkeln med norr är azimuth till vilken punkt antennen. När du ändrar platsen är kartan omfokuserad.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

I azimuth linjerna på papperet är raka (meridianer, paralleller, kvadratkanter) blir böjda. Av denna anledning är alla linjer uppdelade i korta sektioner, varje sektion projiceras och sedan egenskaperna är enade; samma gäller för kanterna av tropoindexet och isobare. Poängen till antipoderna i centrum har inte en bestämd position och förblir utom synhåll.

## 4. Grill locator, zooma och flytta

Maidenhead-nätet är utformat av programmet: fält av 20 ° × 10 ° med de två stora och blekna bokstäverna i mitten, rutor av 2 ° × 1: a, och med tillräcklig zoom av 5' × 2,5 ". Linjerna är blå och tunna, för att inte täcka tropoindexet.

Zooma med hjulet centrerad på pekaren (punkten nedanför musen förblir stabil), flytta dragning, dubbelklick för att återvända till hela vyn, + och − nycklar, och nyckeln med andelen för hela vyn. Detaljer passar till zoomen: finare kuster, underkvadrater, tjockare isobare etiketter.

## 5. Varför inversioner böjer vågorna

I den låga atmosfären sjunker temperaturen normalt ca 6,5 °C per km; Det refraktiva indexet beror på både temperatur och [3] fukt, och där det förändras kraftigt med den höjd som vågen böjer till marken och kommer längre än normalt. Det viktigaste lagret är att upp till ca 3 km, där inversioner, kanaler och konvektiva celler [4] bildas; havsvägar gynnas eftersom vattenmassorna bildar stora områden av [4] inversion. [3]

Den storlek som används för att mäta den är refraktiv N, dvs. (n − 1) · 106 [11]:

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

T är temperaturen i kelvin, P-tryck och ångtryck, både i hPa.

Räknar som varierad N med andel (dN/dh). I standardatmosfären är cirka −39 N/km; upp till −79 N/km anses refraktionen vara normal, förutom superrefraktion och med positiva gradienter av subrefraction [7]. Under −157 N/km är vågen böjd mer än jordens krökning och förblir fast: det är [11] -kanalen. F5LEN

## 6. Tropoindexet: från två kartor till genomsnittet

Hepburn och F5LEN -flikar är kontrolldisken: visa bilderna hämtade från sidan av varje källa, med redigerbar prognostid och tjäna till att kalibrera läsning genom att klicka på referenspunkterna och färgerna i legenden. Tropoindexskiktet förblir tomt tills åtminstone en av de två kalibreringarna inte är aktiva och fullständiga.

Hepburn. Hepburn-indexet mäter hur mycket vågor som förväntas vikas över ett område, i linjär skala från 0 till 10 som också kan överstiga 10. 0 betyder standard atmosfär och endast scatter; 1-2 marginell eller svag super-refraction; 3-4 super-refraction med kanalområden; indexet tar inte hänsyn till scatter tropo och ser inte [1] bergen. Kartorna täcker 6 dagar: 3-timmars passerar de första 36 timmarna, sedan 6 timmar, uppdaterade varje dag till 18 UTC [1]; startdata (temperatur, luftfuktighet, tryck) kommer från den globala modellen av den kanadensiska vädertjänsten [1]. [1]

F5LEN. För Europa F5LEN publicerar 31 kartor från +3 till +192 timmar (var 3 timmar upp till +48, var 6 upp till +96, sedan var 12), med datum och tid som är giltig i titeln, produktionstiden längst ner till höger och en klasslegend: Stör, Normal, Minor, Weak, Good, Strong, Vy Strong, Duct [2]. Världsprognosen för F5LEN beräknas av luftrefraktionsindexet i enhet N, på 40 steg på 3 timmar (ca 5 dagar), uppdaterad 4 gånger om dagen; ett klick på kartan visar det genomsnittliga värdet och lokaliseringen av den punkten [2].

Proceeding. För varje förutsägelse steg programmet utför dessa steg:

1. Utsläpp. Bilderna av de två prognoserna laddas ner i cache bredvid körbar, en gång för att uppdatera källan, inte till någon omdesign. Innan den slutliga versionen ska tillstånd begäras till Hepburn och F5LEN.
2. Automatisk georeferencing. Programmet finner ramen (två horisontella och två vertikala linjer av samma färg) och, på Windows 10/11, läser med OCR integrerad i systemet axeletiketterna (54N, 10W, 0, 5E...). Skär remsorna runt ramen, förstora dem, testa båda tecknen när bokstaven N/S/E/W inte läser och hålla den linje på vilken de flesta etiketter faller: fel avläsningar förblir ute. Härifrån får du den extrema lat/lon av ramen och typen av projektion (vanlig grill eller Mercator). Extremerna sparar sig själva och läser om ramen ändrar storlek. På Mac och Linux, utan OCR, är fabriksextrem eller handskrivna giltiga i fliken. Alternativt för kartor som inte har läsbara axlar: Varje typ av karta (Hepburn Europa, F5LEN över hela världen...) har en kalibrering: vissa kontrollpunkter med kända koordinater (kors av meridianer och paralleller, igenkännbara revben) ansluter pixlar till latitud och longitud, i den projektion som används av den källan. Kalibreringen finns i en fil bredvid programmet och du kan remake om en källa ändrar format.
3. Färgläsning. Varje pixel jämförs med källlegenden och omvandlas till närmaste numeriska värde. Kostnader, skrifter och rutnät som dras på den ursprungliga kartan är erkända som "ingen data" och fylld med de närliggande värdena.
4. Världsnätet. Varje bild, när ramen hittas och kända dess ytterligheter, placeras på jorden; värdet vid varje tidpunkt i världen härrör från bilderna som täcker den. Om fler regioner täcker samma punkt är det värt detaljer (mer pixlar per grad), förutsatt att tiden ligger inom 1,5 h från det valet; sedan närmaste timme, då den senaste rasen. Där ingen bild täcker punkten visar kartan en grå slöja: inga data, som skiljer sig från "index 0". Inuti ramen är en pixel utan en legendfärg 0 (normal atmosfär); kuster, gränser och skrifter hoppas genom att titta på närmaste färg inom 3 pixlar.
5. Samma stege. Hepburn är redan på skalan 0-10 [1]: mörk bakgrund = 0 och tio färger i ordning (violett, blått, vattengrönt, lime, gult, orange, röd, rosa, ljusrosa) för 1...10, lila bortom. F5LEN använder klasser (Disturb, Normal, Minor, Weak, Good, Strong, Vy Strong, Duct) [2], men vissa har exakt Hepburn färger: Svag = 1, Stark = 5, Vy Strong = 7, Duct = 8; Mindre är 0,5 och bra (grön) 3,5. Skalan multiplikator, i F5LEN -fliken, korrigerar uppsättningen för att göra den jämförbar med Hepburn.
6. Samma tid. Tidsfältet går från +0 till +192 h vid steg på 3 timmar. För varje steg du tar från varje källa prognosen giltig vid närmaste tidpunkt; Om en källa inte har det steget, använder media bara den andra och legenden indikerar det.
7. Media. Med knappen Media är varje punkt värd (Hepburn + F5LEN) / 2; med Hepburn eller F5LEN ser du bara en källa, användbar för att förstå var de två prognoserna inte håller med.
8. Rita. Värdet färgas med legendskalan (0 blå, 2 grön, 4 gult, 6 orange, 8 röd, 10 lila) och projiceras som en provtagning under nätet. 0 är transparent, så där ingenting händer ser du den rena grundkartan; allmän transparens justeras med markören bredvid lådan.

På fliken plats är indexet längs rutten genomsnittet av liganätsvärden var 10 km på den maximala cirkeln mellan de två stationerna.

## 7. Isobare

De höga stabila trycken är den första signalen som söks för [3] -truppen, och anslutningarna bortom IK3XTV -analyserade Alperna har också varit med högt [4] -tryck.

1. Data. Tryck på havsnivå från den globala modellen GFS av NOAA [12], läs genom den fria Open-Meteo [15] -tjänsten på ett rutnät av 3 × 3 ° (från 30 ° till 72 ° N, från 30° W till 48° E), under de närmaste 78 timmarna. Programmet uppdaterar data vid start och sedan var 6: e timme, som modell racing.
2. Lines. Den isobare kommer från rutnätet var 4 hPa (...1012, 1016, 1020...) med algoritmen av de marscherande rutorna (marscherande rutor), sedan är de något rundade och projicerade dras av traktat.
3. Centren. De mest markerade lokala maximierna och minimumen är markerade med H (blå) och L (röd) och värdet i hPa.
4. etiketter. Värdet visas längs varje isobara, på en färg bakgrund av kartan för att förbli läsbar ovanför provtagningen.

Zon och steg. Uppgifterna laddas ner på det område du tittar på, utökas med 25%, med ett steg från 1st till 20° som valts för att förbli under 400 poäng (Open-Meteo räknas varje punkt som en begäran: 600 per minut, 5000 per timme): förstora eller flytta kartan ur området redan laddar ner programmet den mest täta reload, till maximum var 10: e minut; om en uppdatering misslyckas de tidigare uppgifterna kvar och det kommer att försöka igen efter 3 minuter. Detta sätt isobare och temperaturer fungerar överallt i världen.

Temperaturer. Samma begäran ger temperaturen till 2 m, till 925 hPa och till 850 hPa. LA0BY visar att händelsen 22/11/2018 till UA6 sågs i temperaturkartor (900-3000 m), men inte i Hepburn [3] förutsägelser; DF5AI och DK5YA använder vertikala temperaturprofiler för att känna igen [5] inversioner. För detta finns det två lager:

- Temperatur 850 hPa: isoterme behandlar var 4 °C (röd ovanför noll, blå nedan, lila på noll).
- Termisk inversion: orange provtagning där luften på 925 eller 850 hPa är varmare än den på 2 m; ju mer intensiv, desto mer inversion är stark. Det är det fysiska tillståndet som böjer vågorna till marken [3] [11] och jämför det med tropoindexet och isobare visar om prognoserna har en väderbas.

## 8. Spots och stationer

Varje plats med båda kända positioner är en linje på den maximala cirkeln mellan de två stationerna, uppdelad i sektioner och projicerade; i Mercatore verkar det då något krökt, i azimutale är det rakt om det börjar från centrum. Blå linje full för den normala platsen, gult täckt med mörk kant för airscatter (<AS>, <ACS> eller "airscatter" i kommentaren), en boll för varje station, lätt halo under linjen för att lossa den från provtagningen. Den valda platsen är oftare utformad för sist och med de två namnen.

Lyssna. Varje PSKReporter [16] mottagare station är en mörk rosa tom cirkel. Från varje start full rosa linjer mot de stationer som har avkodat och skickat tillbaka till nätverket, alltid synliga utan att klicka, med namnen alltid skrivna. Genom att välja en cirkel är dess linjer tjockare. Spot och lyssnande visar bara bandet och utbudet som valts i listan: ändra bandbredd kartan tömmer och omdesignar med de data som redan samlats in för det nya bandet.

Beslutad av mig. Varje station som avkodar med WSJT-X, JTDX eller MSHV (mottagning via UDP) är en röd linje full från din QTH till stationen, med boll och nominering. Det är den mest direkta jämförelsen med prognosen: visar var din mottagare verkligen kommer just nu.

## 8 bis. Färger och symboler

Varje färg har bara en mening, lika i alla teman (ändra bara bakgrunden).

| Färg och tecken | Betydelse |
| Kontinuerlig blå linje | Spot från DX kluster, RBN eller DM4X mellan två stationer |
| Gul linje med mörk kant | Spot med airscatter kommentar (reflektion på flygplan) |
| Mörk rosa tom cirkel | Lyssningsstation på PSKReporter (MQTT) |
| Tunna rosa linjer från cirkeln, med en rosa boll längst ner | Stationer som lyssnaren hörde, med namnet alltid skrivet; tjockare linjer för den utvalda lyssnaren |
| Röd linje och rött namn | Din UDP avkodning (WSJT-X, JTDX, MSHV): som är du |
| Röd linje blinkande stroke | QSO pågår med den stationen: Han kallade dig och stängningen har ännu inte kommit, eller är DX Call of WSJT-X/JTDX med den aktiverade överföringen. Gå vidare med RR73, RRR eller 73 |
| Grönt linje | QSO desunti dal live: stängning av RR73, RRR eller 73 UDP-vy |
| Svart linje med vit gräns | Någon ringer dig: det varar upp till RR73/RRR /73 eller upp till 3 minuter |
| Torg på 45° | Trafik med minst en station utöver den valda kronan: Inga linjer, bara rutor locator |
| Sök efter små med avstånd till norr | 1000, 700 och 500 km runt din QTH; det valet är mer markerat |
| Cirkel och peka i temats färg | Din QTH, med namn och hyresvärd |
| Orange boll med GRAVES 143 050 | Radarn GRAVES ( JN27SI) |
| Prov från blå till röd | Hepburn trope index (0-10) eller F5LEN klass, eller deras genomsnitt; varmare färg, bättre villkor |
| Ljus grå sammet | Område utan tropodata för den valda tiden |
| Gråblå linjer med värdet i hPa | Isolera varje 4 hPa; H i blått högt tryck, L i rött lågt tryck |
| Röda, blå och lila prickade linjer | Isothermia till 850 hPa varje 4 °C: röd ovanför noll, blå under noll, violett noll |
| Sampel orange | Termisk inversion: luft till 925 hPa varmare än på marken, mer intensiv med den starkare inversionen |

I misstänkta rapporter kanten av varje flik säger regeln i kraft: grönt gott, rött Scarta, grå ingen regel.

Vad är de lila rutorna täckta vid 45°. Trafiken bort från din QTH intresserar dig att förstå var förökningen är öppen, men dess linjer, hundratals kilometer långa och ofta tiotals, täcker kartan och döljer vad som räknas för dig: öppningarna du kan utnyttja från din station. Av denna anledning, bortom den valda kronan, ritar programmet inte linjerna utan färgar locatorrutorna (2 ° × 1 °) av de berörda stationerna. Resultatet är en karta över "aktiva zoner": där många närliggande lila rutor visas, finns det en ledtråd eller öppning, även om det inte rör dig direkt; Om området breddar mot dig, är det signalen att peka antennen i den riktningen. 45° instrumentbrädan låter dig se under tropoindexet och rutnätet, så de jämför med en blick prognosen (bottomfärg) och den verkliga trafiken (lacken). Din QTH kvadrat färgar aldrig, eftersom det är utgångspunkten och inte ett avlägset område. Endast de rutor som är alla bortom kronan är färgade: en kvadrat som korsas av kronan, eller inuti, förblir fri även om det finns en av de två stationerna. För att se linjerna, stäng av rutan eller välj en större krona.

Kriterierna som bestämmer vad som kommer på kartan och vad som kasseras förklaras en efter en i dokument 1, avsnitt 11 ter.

## 9. Timebar

Nedan på kartan: datum och tid för den förutsägelse som visas, markör från +0 till +72 h på 6 timmar, index legend 0-10 med namnet på den valda källan, och tiden för modell race för isobare. Trumpet och isobare index följer markören; fläckar, stationer som lyssnar och dina avkodningar är observationer av ögonblicket och förblir alltid de "adex". När markören inte är på +0 är de dämpade, för att komma ihåg att de inte hör till prognostiden.

## 10. Hur man läser överlappningen

- Högt tryck + högt index + plats längs det färgade området: öppningen är där och är den planerade. Det är dags att ringa i den riktningen.
- Högt index men ingen plats: eller ingen är aktiv där, eller förutsägelse är för optimistisk. Lyssningsskiktet hjälper till att skilja: om det finns mottagare i området och känner ingenting, är den andra hypotesen mer sannolikt.
- Långa fläckar utanför de färgade områdena: hög kanal, som ytprognoserna inte ser, som i LA- UA6 [3] -händelsen; eller en annan förökning flydde från filtret (t.ex. E sporadisk utan tag). DF5AI och DK5YA skiljer tropo och E sporadiska också från timmen: öppningar som föds och hamnar tillsammans med en diffus E sporadiska är knappast [5] tropo.
- Hepburn och F5LEN är oense: jämföra de två källorna med knapparna och se var de verkliga platserna faller.
- Tryck ner: G4MCU följde en öppning på SK6VHF -beacon tills trycket sjönk; 1012 hPa ansåg den färdiga händelsen [13]. Titta på isobare genom att bläddra i tidsfältet: om det höga trycket dras tillbaka följer öppningen förmodligen det.
- Signaler som går upp: i WSPR-studion vid 144 MHz av KP4MD ökade signal-/bullerförhållandet direkt i fönstren där Hepburn förutspådde en förbättring och sjönk när prognosen tog bort [14]. Om på samma väg växer, bildar öppningen.
- Bergsvägar: indexet tar inte hänsyn till [1] -lättnaderna, men anslutningarna bortom Alperna finns också tack vare konvektiva celler och vågor av atmosfärisk gravitation [4], och LA0BY arbetade 9A och S5 genom bergen utan Hepburn förutsäger det [3].

## 11. Öppen punkt

- Tillstånd av Hepburn och F5LEN för att använda sina kartor i programmet.
- Kalibrering av konverteringskoefficienter F5LEN → skala 0–10.
- Kalibrering av kontrollpunkter av någon form av karta, för att bli remake om källor ändrar grafik.
- Valfri rutt med temperatur på höjd (850 hPa) från GFS.

## Huvudkällor

1. W. R. Hepburn, DX Info Centre, "The Hepburn Tropo Index (HTI)" och "Tropospheric Ducting Forecast" sidor (dxinfocentre.com). Index definition och skala, negativa värden och mer än 10, uteslutning av scatters och reliefs, steg på 3 och 6 timmar på 6 dagar, uppgradera till 18 UTC. Ursprung av data från den globala kanadensiska modellen: G. Meisse, AMSAT-BB, 27/08/2007.
2. F5LEN, prognos för Europa (tropo.f5len.org/forecasts-for-europe) och världsförutsägelse av VHF tropopheric propagation (tropo.f5len.org/WW) och noterade "New version av VHF tropospheric propagation prognosverktyg". Enhetsindex N, 40 steg från 3 timmar, 4 uppdateringar per dag, värde och lokalisering per klick.
3. S. Heck LA0BY, ”Extreme Tropo Propagation on 144 MHz and up”, 41° Nordic VUSHF Meeting, Skjeberg 2019. Normal gradient 6,5 °C/km och inversioner upp till 10–15 °C/km, temperatur och fuktighet roll, höga stabila tryck, LA- UA6 händelse synlig i hög temperatur och inte i Hepburn, LA-9A/ S5 anslutningar utöver bergen.
4. F. Egano IK3XTV, "Propagation VHF - Bidraget av konvektiva celler och atmosfäriska gravitationsvågor i tropoförökning." Vikten av de första 3 km, marina vägar, konvektiva celler, gravitationsvågor på Alperna, högtrycksanslutningar, vanlig volym upp till 3–4 km.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, "Very Long Distance Propagation in the 144 MHz Band", 24/7/2003. Vertikala profiler av temperatur och daggpunkt för att känna igen inversioner, användning av Hepburn index, skillnad mellan tropo och E sporadiska enligt timmen.
6. J. N. Gannaway G3YGF, ”Tropospheric Scatter Propagation”, QST november 1983. Jordradieekvivalent och förlust av väg som används för index och spotkort (se dokument 1).
7. VHF/UHF DX Book, kapitel 2 ”Troposfärisk förökning”, s. 2-23...2-30. Figur av refraktiva zoner enligt dN/dh (standard −39 N/km, gränser −79 N/km och 0) och yta och höga kanalprofiler. Texten till den levererade kopian är inte läsbar: endast siffrorna användes.
8. Projektsammanfattning av 8/11/2026, som tillhandahålls av programmets författare. Karta färger, nödvändiga prognoser, lager, tidsfält.
9. Natural Earth, vektordata från kuster och gränser, offentlig domän (naturalearthdata.com). Allmän referens, inte bifogad.
10. J. P. Snyder, ”Map Projections – A Working Manual”, USGS Professional Paper 1395, 1987. Formler av Mercator prognoser och azimutale equidtanti. Allmän referens, inte bifogad.
11. Rekommendation ITU-R P.453, radiorefraktivt index. Refraktiv formel N och kanal tröskel på −157 N/km. Allmän referens, inte bifogad.
12. NOAA NCEP, Global Forecast System (GFS). Tryck på havsnivå vid 0,25°, fyra raser per dag. Allmän referens, inte bifogad.
13. G4MCU, artiklar med taggen "troposfärisk" (g4mcu.net/tag/troposfär), 2018–2025. Avstämning, ledning och scatter; mer frekventa kanaler med högt tryck, särskilt på havet; beacon SK6VHF följde upp till nedgången i tryck vid 1012 hPa (juni 2019).
14. C. F. Milazzo KP4MD, ”VHF/UHF WSPR Propagation Study” (qsl.net/kp4md/144_mhz_wspr.htm), 2012–2014. SNR i WSPR rapporterar till 144 MHz växer i de förbättringsfönster som förutspås av Hepburn och faller när prognosen försvinner.
15. Open-Meteo, väderprognos API (open-meteo.com), gratis icke-kommersiell användning. Tryck på havsnivå av GFS-modellen på flera punkter i en förfrågan.
16. mqtt.pskreporter.info, distribution MQTT i realtid av reklam för pskreporter. information: nominering och hyresvärd för mottagare (se dokument 1).
