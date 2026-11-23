# Prognos Manual

> Automatisk översättning av italienska: I händelse av tvivel är det värt den italienska guiden.

Fliken Forecast beräknar den tropoferiska förökningen själv: en indexkarta på det synliga området och den signal som tillhandahålls mellan din station och en motsvarande. Den här handboken beskriver varje data, hur den beräknas och vilken källa den kommer från. Siffrorna i fyrkantiga fästen hänvisar till källorna längst ner; i programmet har varje data av resultaten en serie (simbolo i) med samma källa och hänvisningen till punkten.

## 1. Vad han gör och vad han gör

Förutsägelsen av GFS [1, 2] -modellens vertikala temperatur- och fuktprofiler härleder luftskiktets reaktivitet med lager och uppskattar hur mycket signalvinsterna jämfört med en normal atmosfär. På rutten lägger till den verkliga marken (Copernicus GLO-30 [3]), diffraktion, troposcatter, kanaler och mottagare ljud.

Det är inte ett mått: det är en uppskattning gjord på en vädermodell med avlägsna nivåer hundratals meter. Det tjänar till att jämföra situationer (i dag bättre än igår, 70 cm bättre än 2 m, denna behandling bättre än så), för att inte garantera en anslutning. Punkt 14 förklarar hur man samlar in bevis för att validera det.


> Förbudet kan inte generaliseras, men studeras i förhållande till lokal morfologi. » » » »
> Volker Grassmann, DF5AI, rapporterad av Flavio Egano, IK3XTV (Propagation på 2 meter)

Det är principen som styr poängpunkten beräkning: varje rutt beräknas på dess mark, med dess horisonter, dess hav och dess lättnader; kartan ger den allmänna bilden, behandlar svaret.

## 2. Språkkommandon

- Band: 144, 432 eller 1296 MHz. Karta och beräkning använder det valda bandet.
- Nu: markören flyttar prognosen från och med nu till 72 timmar framåt, inom 3 timmar.
- Uppdateringsprognos (synlig zon): Ladda ner GFS-profilerna på ett rutnät på de flesta 240 poäng som täcker den synliga kartan. Gränssnittet (0,5 ° eller mer) beror på förstoringen.
- Sluta: avbryta en nedladdning eller beräkning.
- Detaljer att visa (högerpanel på kartan): tre nivåer, Väsentliga, Normala och Komplett, slå på en grupp lådor; varje ruta kan sedan ändras för hand. Boxarna finns i tre grupper:
  - På kartan: isolinee del Gain, även 3–7 dB (tracting), isobare, termisk inversion, väderskyltar S ^ ^, korrelationsvinst/pression, zon för direktvy (punkterna 4, 5, 7 och 9 bis). Färgerna stannar alltid.
  - I beräkningen av signalen: alla rader av beräkningen, platt jordprofil, beräkningslogg och detaljerad fillogg.
  - Korrigeringar och levande data: väderkorrigeringar (Misme, subsidence), lufthavskontrast, genomsnitt med fläckar (corridoi), APRS 2 m hopp och LoRa, över berget utan förluster (punkterna 4 bis, 9 bis, 9 quater och 10 bis).
  - Essential håller bara isolineer, väder och mediekorrigeringar med fläckar; Normal lägger isobare, inversion, väderskyltar, direktvy, platt jord, lufthav och APRS; Komplett vänder den på. Nivån ändrar bara vad du ser och vilka korrigeringar som kommer in: beräkningarna förblir desamma.
- Källa till data och historiska fall: varifrån profilerna kommer och vilken dag du ser (punkterna 3 bis och 3 ter).
- Min station och korrespondenten: Locator, antenner, ström, kablar, buller (punkterna 7 och 12).
- Beräkna signalen och täckningen i optik: de två beräkningarna på marken (punkterna 8–13).
- Beräkningsregister: varje steg med de nummer som används, inklusive i data/prognos-calcoli.log-filen.

## 3. Från GFS-data till profil

För varje punkt i nätet och varje timme läser vi trycket på marken, temperatur och fukt på 2 m, vinden på 10 m, höjden av gränsskiktet, CAPE och temperatur och fukt i förhållande till 1000, 975, 950, 925, 900, 850, 800, 700, 600 och 500 hPa. Från 1,3.0 kan du också läsa vinden till 850 hPa och den vertikala hastigheten till 850 och 700 hPa (punkt 9 bis).

Nivåerna beräknas med den ipsometriska formeln och den virtuella temperaturen; nivåerna under marken är knappa. För varje nivå:

- N = 77,6/T · (P + 4810·e/T), med T i kelvin, P och i hPa, och genom mättnadstrycket av ITU-R P.453-14, ekv. 9, med EF-korrigeringsfaktorn (recontrolled på den officiella texten) [4].
- M = N + 157, med h i km: den modifierade reaktiviteten. Där M går upp finns en ledning.

## 3 bis. Datakälla

Menyn Datakälla väljer var profilerna kommer ifrån:

- Automatisk (rekommenderad): GFS först från NOMADS [14], NOAA-tjänsten som distribuerar modellen: för varje ögonblick frågar programmet två utskärningar av området i G RIB2 -format och läser dem från sig själv; NOMADS har ingen daglig gräns, bara en per minut som programmet respekterar. Med breda nät (från 0,5 ° till upp) använder filer till 0,5 ° eller 1st var tredje timme, mycket lättare. Poängen som NOMADS inte ger, eller alla om NOMADS inte svarar, fråga Open-Meteo [2]. De två källorna ger samma modell och samma ras, så du kan blanda i samma rutnät.
- GFS från Open-Meteo: endast Open-Meteo, som i 1.2 (vid gränsen per minut förväntas).
- GFS från NOAA NOMADS: Only NOMADS. Långsammare att ladda ner (två filer per timme, en halv sekund mellan en förfrågan och den andra att inte överstiga NOAA-gränsen), men konsumerar inte Open-Meteo-gränsen.
- ICON-EU (DWD) och ECMWF IFS 0,25°, från Open-Meteo: andra modeller än GFS, som gäller i hela nätet och för rutterna. De blandas inte med GFS: jämför två modeller samtidigt är ett enkelt sätt att se hur prognosen är osäker. ICON-EU omfattar endast Europa; ECMWF har inte alla GFS-trycknivåer (de försvunna nivåerna hoppar) och får inte ha gränsskiktet.

Källan som används är skriven i statuslinjen och i den första raden av resultaten (Data Source), med hur många poäng som kom från varje tjänst och hur många från cache. Skivkakan separeras av modell.

## 3 ter. Historiska fall

Historiska Case meny granskar en dokumenterad öppning med Open-Meteo [15] arkivet GFS prognoser: kartan flyttar till området av fallet, bandet sätter sig och tiden markör lämnar från den första dagen i fallet. Genom att trycka på Update-prognosen laddar du ner profilerna för dessa dagar; signalberäkning fungerar som för aktuell prognos. Det tjänar till att jämföra modellen med vad som verkligen hände.

Det första fallet är att den 14-15 augusti 2022 berättade i QST (november 2022, "Världen ovanför 50 MHz" [23]): en öppning under ett högt tryck mellan Manitoba och Kansas / Messouri, från 144 till 1296 MHz nästan alla i FT8, med den kanadensiska rekordet på 10 GHz av 1228,56 km i Q65 och signalerar upp till 1941 km. Den andra är öppningen mellan 29 december 2021 och 2 januari 2022 på Tyrrhenian och västra Medelhavet vid 144 MHz, från rapporterna från IC8TEM: första öppningar på 29 (upp till 737 km), bredare den 31 ( IT9GSF - EB5EA 1217 km), stor öppning den 1 januari mellan Italien, Sicilien och Spanien. För äldre fall (t.ex. september 2000) har Open-Meteo-arkivet inga data. I arkivet saknar gränsskiktets höjd: det gäller inte dess korrigering.

## 4. Index och färger

För varje lager mellan två nivåer jämför programmet N-fallet med trösklarna på tabell 6.5 i Bean och Dutton [5]: till exempel mellan 1000 och 850 hPa en minskning upp till 60 N / km är normalt, över 100 N / km är superrefraktion. Ett super-refraktivt lager är värt 10 till 15 dB vinst; en ledning som fångar bandet från 20 till 25 dB, enligt underskottet av M. Strong vind till marken, hög gräns lager och hög CAPE minskar vinsten, eftersom de tar bort eller bryter lagren [5, § 4.4].

Färgen är den uppskattade vinsten i dB, med en fast skala lika i alla teman:

| Gain | Färg | Betydelse |
| Under 7 dB | Ingen. | Nästan normal atmosfär |
| 7–10 dB | blå blå | gradient över normal |
| 10–13 dB | grönt grönt | super-refraktivt lager |
| 13–16 dB | gult gult gult gult gult | starkt lager, men utan kanal för band |
| 16–18,5 dB | orange | Mycket starkt lager eller för tunn kanal |
| 18,5–21 dB | Rödröd | Conduit Trapping Band |
| Från 21 dB | lila | Stark och djup kanal |

Med displayrutan också 3–7 dB zoner mellan 3 och 7 dB, där något bildas men förblir under skalan, kan du se med en ljus grå återhållsamhet; färger ändras inte. En prickad ruta avgränsar området som täcks av sin egen prognos och visar den maximala synliga vinsten: om det är under 7 dB kartan förblir utan färger eftersom atmosfären är nästan normal, inte för att data saknas. När du laddar ner ett nytt klick på Update-prognosen avbryts inte; för att stoppa det finns Stop.

Färgerna är ogenomskinliga till 82%, så temats bakgrund ändrar dem bara. Locatornät och fyrkantiga namn är utformade över färgerna. Där din förutsägelse inte finns men Hepburn eller F5LEN ger 5 eller fler schackkort visas med färgen på den källan.

Kartan dömer varje punkt ensam. Signalberäkningen kontrollerar istället att lagret är kontinuerligt längs rutten (punkt 9): en rutt som korsar mycket gul kan ge mindre än kartan gör att du tänker.

## 4 bis. Prognos medierad av reklam: korridorer (från 1.3.3)

Upphovsrättspolicy: En länk ensam får aldrig dra en ledning. En isolerad och kort mottagning kan vara spridningen av ett transiteringsplan (APRS-paketet är mycket kort), en öppning av några minuter eller ett fel, och skulle ge "drop-conduct" de lurar. Programmet erkänner en korridor när, i samma band:

- minst 3 par oberoende stationer (ingen gemensam station) arbetar på vägar på minst 300 km.
- Rutterna är parallella: azimut inom ±15°, genomsnittliga punkter inom 150 km av den gemensamma axeln.
- alla under de senaste 30 minuterna, med strålen som upprepas på avstånd av minst 10 minuter (persistens).
- för ARS, ett hopp räknas endast om samma hopp upprepas.
- Från 1,3,4, rutterna över 1500 km räknas endast om strålen kommer från minst två olika källor (t.ex. PSKReporter och ett kluster): från en enda källa, till dessa avstånd i 2 m, är oftare EME, scatter meteor eller fel locatorer än tropo.
- Från 1,3,4, räkna inte stationerna "att stråla": som i sista timmen har minst 5 korrespondenter över 1000 km spridda över ett fan på minst 150°. På 2 m är tropen riktning; en mottagare som "fria" tillsammans Israel, Portugal, England och Italien från 2000-3700 km, ofta med positiva signaler, har nästan säkert frekvensen konfigureras dåligt. I 17-22/11/2026 I5MZY arkiv 72% av boendet i 2 m över 1000 km kom från stationer så; den 22/11 klockan 10:53 UTC var OH6HLH, OZ1ITS, F4LUK, OM8ABC och SP2JLR, och med filtret den längsta korridoren sjunker från 2175 km (regole 1.3.3) till 615 km.

Källor: klusterfläckar (inte de markerade AS / ACS), PSKReporter via MQTT, deras QSO och WSJT-X / JTDX / MSHV avkodningar, och APRS hoppar i 2 m och LoRa 433 / 868 MHz.

APRS: 2 m och LoRa. Programmet läser APRS-IS i read-only (rotate.aprs2.net, filter på Europa) och extraherar radio hoppar mellan kända placeringsstationer (källa → digipeater → IGate), såsom VHF-kartan av DXView och APRS PropView (RF-YVY, Apache 2.0). Tre band är värda: 2 m, LoRa 433 MHz (bandkorridorer 432) och LoRa 868 MHz (korridoi som räknas för 1296: en ledtråd som ger 868 MHz också de högsta frekvenserna). Bandet kommer från LoRa tocall (aprs-deviceid lista) och från frekvenser eller ord i kommentaren; ett hopp är bara värt mellan två stationer i samma band.

Den känsliga punkten är de aktiva repeatrar: en signal som tas emot på 500 km kan anländas med två eller tre digipeatertransmissioner. Ett hopp räknas därför endast om det är mellan två verkliga och på varandra följande sändare:

- Vägen måste spåras: varje digipeater som har skickats har satt sitt namn, och hoppet mäts mellan en och nästa; ett alias som används utan namn (W IDE1 *, W IDE2 *, regionala alias) bryter kedjan och den sträckan räknas inte.
- dolda överföringar: programmet kommer ihåg den väg varje källa brukar fråga (till exempel W IDE2 -2). Om ett paket kommer med ett språng på mindre (W IDE2 -1) och inget namn förklarar det, skickade en digipeater den utan spårning och paketet kasseras; källorna som aldrig setts innan räknas inte.
- digipeater i mitten av vägen: om mellan de två stationerna, mer eller mindre på vägen, finns det en digipeater av samma aktiva band under de senaste tre timmarna, hoppet kasseras, eftersom det kan ha återförsänds (det är särskilt för digipeater LoRa som återförsänds utan att ändra vägen).
- icke-radiopaket (qAC, qAX, TCPIP), de som drivs via internet på radio från en IGate (tredje part), HF och olika frekvenser, stationer på en höjd av mer än 1500 m (palloni, flygplan) och hoppar över 1500 km.

Effekt på kartan (från 1.3.4). Det drar bara det område där det minsta garanterade är giltigt: ett band på ±60 km runt den centrala delen av varje rutt (från 15% till 85% av längden), inte på stationer i ändarna, där ledningen inte kan vara där. Bandet av alla korridorer dras samman, i en neutral färg (texten) och halvtransparent: överlapparna läggs inte till, och färgen är inte förvirrad med lila av skalan (21 dB och bortom). Etiketterna (drag, genomsnittlig avstånd, minimivinst) skannas med varandra; passerar med musen på en etikett visas de delar av korridoren.

I området, och endast nära den nuvarande tiden (±90 minuter), faller prognosen inte under ett minimum. Anledningen är: om stationerna verkligen passerar är förökningen där, oavsett vädermodellen säger.

Eftersom de korta rutterna är värda lite (från 1.3.4). På 2 m, mellan 300 och 600 km, två bra stationer (några hundra watt, antenner, FT8 eller CW) ansluter nästan varje dag även med den normala atmosfären, för troposcatter: signalen diffused av de små oegentligheterna i luften i den gemensamma volymen av de två buntarna. Tre eller fyra platser på 500 km säger att bandet fungerar som vanligt, inte att det finns en öppning: GEKO Magazine n. 69 ( IU3QEZ) uppskattar troposcatter med 2 × 10 flödeselement på cirka 440 km i SSB QRP, 510 km i SSB med 50 W, 600 km i CW QRP och 640 km i SSB med 500 W (källor, elva varv). Endast över 600-700 km är troposcatter inte längre tillräckligt och en serie parallella anslutningar indikerar verkligen en ledning eller ett lager. Upp till 1,3,3 höjde en korridor på 450-600 km prognosen till 9-12 dB, och kartan visade en förbättring som inte fanns där.

Startvärdet går sedan med det genomsnittliga avståndet på rutterna:

- upp till 400 km: 3 dB;
- från 400 till 700 km: salt på ett linjärt sätt från 3 till 10 dB (500 km → 5,3 dB; 600 km → 7,7 dB);
- från 700 till 900 km: från 10 till 16 dB; över 900 km kvar på 16 dB.

Under 7 dB, som är under början av skalan, är det minsta kvar att: korridoren dras med sin etikett, det ses att bandet fungerar, men färgerna på kartan ändras inte (med lådan visar också 3-7 dB gråplatta). Ovanför 7 dB vägs värdet, hälften och hälften, med antalet oberoende rutter (3 slag → 0,25; 6 eller mer → 1) och median S/N av relationer (−20 dB → 0,5; 0 dB eller mer → 1; inte känd, som i klusterfläckar, → 0,75), från 7 dB. Exempel: 3 linjer utan S/N på 500 km → 5,3 dB; 5 rutter på 615 km → 7,7 dB; 3 rutter utan S/N på 900 km → 11,5 dB; 3 rutter till −20 dB på 900 km → 10,4 dB; 6 rutter till 0 dB på 900 km → 16 dB.

Minimiavståndet förblir 300 km: att höja det till 500 km skulle också ha tagit bort de verkliga kortdistansöppningarna (till exempel en ledtråd på Tyrrhenian på 400 km, som erkänns av väderprognos och mycket starka signaler mer än från avståndet ensam). I I5MZY -arkivet, den 22/11/2026 klockan 10:53 UTC, med denna regel och strålfiltret var de längsta korridorerna 615 km (7,7 dB) och 540 km (6,3 dB); med reglerna på 1,3,3 nådde den 2175 km och 16 dB.

Fram till 1.3.3 var det minsta giltigt på alla korridorens rektangel och berodde bara på avståndet (från 10 dB till 300 km till 16 dB till 900 km): med många långa korridorer tillsammans (till exempel den 22/11/2026, tolv korridorer upp till 2620 km) lyfte nästan hela Europa till 16 dB.

Vinstkurvorna drar inte längre "gocka": de mindre öarna på 220 km i båda riktningarna förblir färgade men utan sluten kurva, om inte en korridor bekräftar dem. Det stänger av med "Prevision medierad av fläckarna".

## 5. Valfria linjer och jämförelse med tryck och inversion

Få kurvor: linjer vid 7, 10, 13, 16, 18,5 och 21 dB, kanterna av skalan, som dras på fältet jämnade med metoden för de marscherande rutorna, med etiketten i dB.

Isobare: tryck på havsnivå var fjärde från samma GFS, till prognostimmen, med centrumen H och L.

Termisk inversion: kurvor täckta med hur mycket luft vid 925 eller 850 hPa är varmare än det vid 2 m, vid +1, +3, +5 och +8 °C.

Under kontrollerna ger en linje vinstavtalet med trycket och med omvänd på den synliga zonen: Pearsons korrelationskoefficient, −1 till +1. Kanalerna är ofta födda i anticykloner (luften som släpper värme och bildar omvänd), så ett positivt avtal med trycket är normalt, men inuti samma anticyklon förändras vinsten mycket mellan havet, kusten och bergen. Inversion ensam är inte tillräckligt: böja vågorna endast om ovanför luften är ännu torktumlare [5]. Linjen tjänar till att se, nu för tillfället, vilket av de två bäst förklarar de färgade områdena. Samma jämförelse skrivs i registret.

## 6. Potentiella rörledningar: dubbelberäkning

GFS-nivåerna varierar från 200 till 1000 m: en tunn inversion sprids och modellen ser inte konduiten. Om mellan två nivåer N-minskningen överstiger normalt (40 N/km), är luften stabil (termisk gradient under 3 K/km) och ovan är torr (specifik luftfuktighetsgrad under 0,8), anser programmet en potentiell kanal och beräknar den på två sätt:

- Beräkning A: all koncentrerad överskottsminskning på 50 m.
- Beräkning B: samma minskning som fördelas på den typiska tjockleken på rörledningarna i det området, tagen från de klimatkartor av ITU-R P.453 [4] (på marken eller förhöjd).

För underskottet och för den maximala fångade våglängden hålls det lägsta värdet: det är en försiktig uppskattning.

ITU-R P.453 data är © ITU och kan användas för personliga och icke-kommersiella ändamål, med hänvisning till källan, men kan inte omfördelas: för detta är jag inte i programpaketet. För att aktivera B-beräkningen laddar du bara ner P.453-kartorna från ITU i fliken: programmet, efter att ha visat användningsvillkoren, ladda ner arkivet för rörledningarna från ITU-webbplatsen (elevated_and_surface_duct.zip) för användarens personliga användning, extrahera filerna i data / P453 och slå på beräkningen utan omstart. Alternativt laddas ned från ITU-R Study Group 3 digital produktsida (itu.int, ITU-R Study Group 3, Digitala produkter, P.453, Surface-data och Elevated duct-data) och sätter de sju S_OCCUR. TXT, S_THICK.TXT, S_STREN. TXT, E_OCCUR.TXT, E_base.T Utan filer programmet använder endast beräkning A och skriver det i fliken och i registret. Förhållandet mellan underskott A och typisk intensitet P.453 är anomali: Hur många gånger dagens pipeline är starkare än normalt. P453

## 7. Stationer och lokaler

Hyresvärden kan ha 4, 6, 8 eller 10 tecken. Med 8 eller 10 tecken är positionen korrekt och markandelen kommer från Copernicus GLO-30: antennens höjd är tillräckligt från marken. Med 4 eller 6 tecken är torget för stort: det är bättre att skriva kvoten på antennen på havet; om 0 programmet förblir uppskattningen med SRTM-jorden i mitten av torget plus höjden från marken och signalerar det i resultaten. För motsvarande standardhöjd är 3 m från marken; med havsandel 0 används SRTM-jord plus höjd.

## 8. Land, diffraktion och troposcatter

Profilen på rutten är provtagen på Copernicus GLO-30 var 30 m (mer än 120 km bred). Jordradieekvivalenten är k = 157/(157 + ΔN), med ΔN nedgången av N i första km från GFS-profilerna längs [6, 7] -rutten.

Lättnader nära stationer (från 1.3.2, författarens kriterium). P.452 betalar horisont vinkel linjärt, 0.573 dB per mrad: en lättnad på några tiotals meter några hundra meter från antennen kostar tiotals dB, mycket mer än vad som observeras. Av denna anledning kan lättnaderna inom 20 km från varje station lägga till upp till 6 dB: horisontvinkeln på grund av dem är begränsad till den av de mest avlägsna lättnaderna plus 10,5 mrad. Det är giltigt för din station och för korrespondenten. Linjen "Nära lättnader" säger hur många dBs har tagit av.

Källan till mark (från 1.3.2). Kvoterna kommer från Copernicus DEM GLO-30 [3]: 1 andra båge, havet är 0 och kusten är klar. Fram till 1.3.1 använde programmet Terrain Tiles "skadi" av AWS. Nära kusten blandar dessa torg SSRTM med låg upplösnings havsbottendata och sprider öns andel på havet: runt Capri skapar en spökring på 80-100 m mellan 0,5 och 1 km från stranden. Den 21 november 2026 IC8TEM rapporterade en obefintlig horisont i nordväst, punkt-för-punkt jämförelse med Copernicus bekräftade det (94 m till sjöss till nordvästra Marina Grande vs. 0). Den falska horisonten, på +2,6°, kostar cirka 26 dB troposcatter till kuststationerna. Skidorna är bara en reserv när Copernicus inte svarar. Copernicusrutorna väger 15-30 MB och laddar ner en gång; de av endast hav existerar inte och är värda 0.

- Gratis utrymme: Lfs = 92,44 + 20 log f(GHz) + 20 log d(km) [8].
- Skillnad: Bullington metod på den verkliga profilen plus ITU-R P.452-16 § 4.2 [6] delta-Bullington sfärisk markkorrigering, med ITU-R P.526 [9] sfärisk förlust.
- Första Fresnel-zonen: radie √ (dd1·d2/d); fri om marken lämnar minst 0,6 av radien. För varje hinder visas [9] Knife Board V och J(VV) parametern..
- I utformningen av profilen är den direkta radien grön i optisk och röd syn om den passerar under marken eller under krökningen av jorden; röda prickar är de hinder som invaderar 1: a området av Fresnel (purple prickad linje). På havet är den "röda punkten" på höjd 0 m inte ett berg: det är havet som stiger för krökningen. Passerar med musen på radien eller på en punkt visas en komiker med förklaringen och värdena.
- När en lättnad nära antennen stänger riktningen (till exempel en horisont av tiotals grader), diffraktion och troposcatter ger förluster av hundratals dB: över 300 dB programmet skriver "> 300 dB" och "riktning stängd av ett angränsande hinder", signalen "< −230 dB" och S / N-förhållanden "−60 dB". I så fall är det lämpligt att titta på "Rekommenderad anläggning" och "Via monte".
- Troposcatter: ITU-R P.452-16 eq. 45 till 50% av [6] -tiden, med vinkelavstånd från horisonterna och N0 från profilerna.
- Utan tropo: summan av diffraktion och troposcatter, aldrig under det fria utrymmet.

## 8 bis. Troposcatter: fyra modeller och osäkerhet

Bredvid P.452 beräknar programmet troposcatter med tre andra modeller, liksom TropoLink [16] -programmet, och visar deras osäkerhet:

- NBS TN101 § 9 [25] (från 1.3.5), median: L = 30 log f − 20 log d + F(θd) − F0 + H0 + Lgp. F(θd) är dämpningsfunktionen i bilaga III i dess analytiska former (Ns 301) med korrigering för Ns, H0 frekvensökningsfunktionen av § 9.2: vid 144 MHz, med låga antenner i våglängder, reflektionen från marken raderar delvis fältet i den nedre delen av den gemensamma volymen och salt. Ns är den timme GFS-profiler, de effektiva horisonterna och höjderna kommer från Copernicus-profilen, Lgp = 0,07 ^ (0,055 (Gt + Gr))) är öppningskopplingsförlusten. Det är samma form som ITM använder i sitt inre, men med dagens reaktivitet istället för klimatet. Det testar på platta sektioner vid 144 MHz med antenner på 20 m: inom 2-4 dB från ITM mellan 150 och 900 km och cirka 2 dB i den förenklade formen 9.2b av TN101, som inte har H0.

- ITU-R P.617-5 [17], median: L = F + 22 log f + 35 log θ + 17 log d + Lc, med f i MHz, θ i mrad, d i km, F = 0.18 · N0 ·e ^ (−hs/7,35) − 0.23 N och Lc = 0.07 ^ (0,055 (Gt + Gr)). N0 och ΔN tas vanligtvis från ITU: s klimatkartor; här använder vi varje timme profilerna GFS ( N0 ·e ^ (−hs/7,35) är reaktiviteten till marken, ΔN minskningen i första km), så även troposcatter följer tiden.
- RSGB VHF/UHF Handbook Formel, DL2FZN [18]: L = Lfs + 24,44 + 4,283 ln f + 0,06656 d. Sluttningen med avståndet är nästan lika med PROPBER (0,067 dB/km).

Vanlig volym (från 1.3.1): är den punkt där radien av de två stationerna korsas, var och en benägen till sin horisontvinkel, på en Jord med motsvarande radie ae = k 6371 km. Från TX stiger radien som z = hTX + x {\t + x2/(2 ·ae); Avståndet x = (hRX − hTX + děr + d2/(2·ae) / θ, med θ = d/ae + θt + θr. Med platta horisonter och antenner på samma kvot är basen värd d2/(8 ·ae): ca 1,3 km till 300 km, 3,7 km till 500 km, 9,4 km till 800 km. Ju högre volymen, desto mer är luften torr och sällsynt och desto mindre sprids den: av denna anledning mer än 800-1000 km truppen går ut. I resultaten är basens andel och position, reaktiviteten, gradienten och fuktigheten hos GFS på den höjden, och motsvarande avstånd från Bray (QST, november 1961): varje extra eller mindre horisont är värt ca 111 km bort. Referens: presentationen "Över horisonten i VHF" av IU3QEZ (GEKO Magazine n. 69).

Troposcatter-linjen, andra modeller visar de två förlusterna och osäkerheten som halva skillnaden mellan den mest optimistiska modellen och den mest pessimistiska. De är bara jämförelser: förlusten utan tropo förblir P.452.

## 8 ter. eftermiddagstroposcatter över lättnader (från 1.3.4)

IK3XTV, från Thiene ( JN55, med Alperna på 10 km), arbetar på sommareftermiddagar södra Tyskland på 400-500 km, utan syn, med starka signaler men med snabb och djup evanescence; författaren tillskrev dem till "konvektiva celler anpassade som ett tåg av linser". Observationen bekräftas; den förklaring som antagits i programmet är en annan, fysiskt fastare.

På eftermiddagen rör solen luften över lättnaderna och gränsskiktet stiger till 2-3 km. På toppen den fuktiga luften nedan möter den torra luften ovan: där turbulensen av det refraktiva indexet (storleken som styr troposcatter) är maximal. När den toppen når den gemensamma volymen av rutten, som på 400-500 km är ca 2-3 km, förstärks troposcatter. Detta förklarar eftermiddagstiden, högt tryck (tyst cylindrisk, stark uppvärmning) och snabb evanescens, typisk för diffusion och inte av kanalerna. Konvektiva celler fungerar inte som linser: producerar turbulens, och det är turbulensen högst upp i omarbetningsskiktet som sprider signalen.

Konsekvenser för läsning av prognoser:

- på jorden den första eftermiddagen är den värsta tiden för de låga kanalerna och lagren, men inte för troposcatter bortom bergen: de två sakerna bör läsas separat.
- Programmet har redan de två uppgifterna som det tjänar, höjden av gränsskiktet som tillhandahålls av modellen och andelen av den gemensamma volymen av rutten (gemensam volym) i resultaten: när de är nära, kan en eftermiddags bergsbehandling gå bättre än beräkningen i normal atmosfär.
- från 1,3,4 punktpunkten beräkning tar hänsyn till: om den gemensamma volymen är på jorden, längs vägen finns det lindringar på minst 800 m, solen är över 15 ° och gränsskiktet överstiger 800 m, och toppen av lagret omformning (modell jordränta + gränsskikthöjd) ligger inom 500 m från basen av den gemensamma volymen, troposcatter får 3 dB; mellan 500 och 1000 m linjärvinsten faller till noll. Linjen "Troposcatter eftermiddag". Med snö längs rutten (minst 10 cm på marken enligt modellen, snödjupvariabeln Open-Meteo eller SNOD av GFS från NOMADS) gäller förstärkningen inte: på snön förblir jorden kallt och det omformade lagret växer inte. Värdet på 3 dB är provisoriskt och måste kalibreras med riktiga länkar; det stängs av med väderkorrigeringar.

Källor: observationer av F. Egano IK3XTV (Radio Rivista e www.qsl.net/ik3xtv, "Propagazione sulle 2 metri", "Celle convettive", "Troposcatter over the Alps"), källor § 13.

## 9. Grundlagret längs vägen

Det är hjärtat av signalberäkningen. Längs vägen tas GFS-profiler var 25 km. För varje lager av tryck, den längsta sträcka där samma lager är närvarande på varandra följande punkter; räkna som kanal endast tjocka lager minst 40 m [10].

- kontinuitet: från vilken km till vilken km, och bråkdelen av rutten;
- topp och bas, och lutning i m/km;
- Jorden inuti lagret: km där bergen stiger över basen;
- Täckt land: På havet är ledningen nästan alltid, bestämmer sträckan av land [10]. Om marken överstiger 10% av rutten är den bråkdel som används minst mellan kontinuitet och täckning av jorden.

Tropevinsten är lagrets vinst för den använda fraktionen. Om mer än hälften av profilerna är subrefraktiva är vinsten negativ.

## 9 bis. Väderkorrigeringar: stabilitet, subvention, strejk

Från 1.3.0 ger profilen också tre meteorologiska uppgifter, som kan ses i registret (återvinna "stabilitet (Misme)"), i Meteo-linjen längs resultatvägen och, med Segni meteo-boxen, på kartan.

- Mismes stabilitet [5, kapitel 6]. Misme binder fältet utöver horisonten till en sammansatt parameter: M = a·(ge − 40 + bW), med en = 0,5 dB per N / km, motsvarande gradient ge och termisk stabilitet ΔW, arbetet för att höja luften på 1 km. Programmet beräknar ΔW = gō (θ − θ0) / θ0 dz mellan jord och 1 km över, med θ potentiell temperatur; standardatmosfären är värd cirka 56 J/kg. Termen som läggs till vinsten är 0,07 dB för J/kg bortom standarden, begränsad mellan −4 och +6 dB, och räknas endast där ingen ledning fäller bandet (gradienten är redan i Bean och Dutton index). Kalibreringen är tillfällig: den måste verifieras med förhållandena (punkt 14).
- Subvention och uppförsbacke luft [19]. Moler och Holden, på vägar av 125-305 km till havet vid 1365 och 9380 MHz, fann den högsta signalen på cirka 2,5 dB i genomsnitt med luft som kommer ner det med luft stigande, och i ett fall en förstärkning av cirka 20 dB i 3 timmar i början av subventionen, följt av en minskning av 22 dB när luften återvände till konvergera. GFS ger vertikal hastighet till 850 och 700 hPa. Om luften på båda nivåerna sjunker mer än 1 cm/s och det finns en omvänd eller en kraftig nedgång i det potentiella refraktiva indexet K (N rapporterade adiabatiskt till 1000 hPa, vilket belyser den torra och varma luften som tappas från topp [5]), 1,5 dB läggs till; om luften stiger med mer än 5 cm/s tar de av sig. 2.
- [20] Scirocco. Varm och torr luft från Sahara över svalare marin luft ger mycket starka inversioner från östra Medelhavet till Gibraltar. S-tecknet visas där 850 hPa vinden kommer från 100-200° med minst 7 m/s och temperaturen är minst 16 °C, på Medelhavet. Det är bara en indikation: vinsten bestämmer lagren av profilen.

Utan en ledtråd som fångar bandet överstiger den totala vinsten inte 16 dB, slutet av det gula: korrigeringar gör starkare lager tydligare men passerar dem inte genom kanaler (röda och lila kvar vid kanalerna).

- Persistens (från 1.3.1). De största öppningarna kommer efter goda högtrycksdagar (VHF/UHF DX Book; Biancomano och Leyshon 1998, regel 12 timmar; rapporterna från 29/12/2021–2/1/2022 växer från första till fjärde dagen). För varje cell räknas programmet timmarna för föregående 24 med vinst på minst 13 dB och lägger till upp till 2 dB i proportion, endast om vinsten redan är minst 10 dB.

Hur man läser tecknen på kartan (Change Weather Signs; från 1,3,2 symbolen är stor dubbel och passerar med musen över ett tecken visas en komiker med förklaring och värderingar av cellen):

| Tecken | Färg | Vad betyder det? | Effekt på trope |
|---|---|---|---|
|  ↓  | grönt grönt | subvention: luftdroppar (antikyklon), värmer för komprimering och torr, stärker omvänt och håller det fast | Förmånlig +1,5 dB |
|  ↑  | Rödröd | uppförluft (front, lågt tryck, konvektion): blandar låga lager och bryter inversioner och kanaler | ogynnsam, −2 dB |
|  S  | orange | Syrup: varm och torr luft från Sahara över frisk havsluft | gynnsam ledtråd, ingen korrigering: bestämma lagren |

Ett område fullt av grönt under en anticyklon är det klassiska tecknet på en hård öppning; Röd ^ pilar längs linjen säger att lagren bryter, även om cellens färg fortfarande är bra.

Boxen Väderkorrigeringar stänger av dessa korrigeringar för att se modellen av 1,2.

Validering (punkt 14): på 593 anslutningar till 144 MHz mellan 29 december 2021 och 2 januari 2022, jämfört med samma rutter och två veckor senare är separationsindexet (0,5 = ingen, 1 = perfekt) 0,79 utan korrigeringar och 0,91 med korrigeringar (före taket 16 dB). Samma jämförelse visar huvudgränsen: ledningen på havet vid låg höjd, som under dessa dagar förde tecknen mellan Kampanien och Ligurien, är inte synlig på nivåerna av GFS.

## 9 ter. Nyheter 1.3.2: två uppskattningar, tak, högt tryck, hav, stationer, åsar

försiktig och aggressiv uppskattning. Vinsten från gradienten växer från 0 dB till gränsen för normal upp till 10 dB till tröskeln för superrefraktion. I 1000-850-skiktet är försiktigt tröskelvärdet Bean och Dutton (100 N/km); Den aggressiva tröskeln är den klassiska Super-Refraction tröskeln på 79 N/km (k ≈ 2; P.453 använder −100 N/km i de första 100 m), som med k50 = 157/(157 − ΔN) är k ≈ 2 (k = 2,5 motsvarar cirka 94 N/km). Poängberäkningen visar de två förlusterna flankerade; med en kanal som fångar gänget sammanfaller. Kartan använder försiktighet.

Attik på 2500 m. Vägar och kanaler är bara sökt efter upp till 2500 m över marken: ju högre antenner av amatörradio utnyttjar dem inte. Med 1500 m förlorade valideringen en separationspunkt och 4% av anslutningarna över 16 dB, eftersom subventionsreverseringarna på vintern ofta är mellan 1500 och 2500 m.

Högt tryck förlängs. +1 dB där minst 7 av de 9 cellerna runt har minskat trycket till havet på 1020 hPa eller mer och vinden är svag i de låga lagren (10 m under 5 m/s, 850 hPa under 8 m/s), förutom stigande rörelser.

Havsvikt. I punkt-till-punkt beräkning vinsten av lagren multipliceras med 0.8 + 0.4 1, där μ1 är faktorn för ITU-R P.452 (eq. 51) på den längre kontinuerliga sträckan av mark: 1 på det öppna havet, liten med långa sträckor mark.

Stationer och genomförda. När en ledning som fångar bandet täcker endast en del av linjen, jämförs andelen av varje antenn med bas och toppen av kanalen i början och slutet av den täckta sektionen; vinsten multipliceras med 0,5 + 0,5 (e ^ (hTX / 300) · e ^ (hRX / 300)), med Δh avståndet i meter ur kanalen. Med ledningen på nästan hela vägen är kopplingen av § 10.

Layer och åsar. Om toppen av gränsskiktet i GFS (jordfrekvensen för den mest gränsöverskridande_layer_height-modellen) överstiger gränsen (maximalt lokalt mer än 300 m, om det högsta överstiger 500 m), passerar lagret över bergen utan att bryta: +1,5 dB för bråkdelen av täckta åsar. Det är observationen av IT9GSF på öppningarna från Sicilien till Tyskland med gynnsamma Tyrrhenian.

Nivåer nära marken på havet. Med NOMADS-källan laddar programmet också ner yttemperaturen (vid havet är SST), temperatur och fukt på 80 m och i lagret 30-0 hPa över marken. På havspunkter (modellhastighet under 5 m) läggs dessa nivåer till profilen, under den första trycknivån, för att se kanalen på 100-200 m som trycknivåerna inte löser. På jorden används inte: natt- och daglagren är ännu inte giltiga. Open-Meteo ger bara yttemperaturen.

Förångning pipeline. Från SST, från temperatur och fukt till 2 m uppskattas δ-höjden av avdunstningsledningen med den neutrala logaritmiska profilen (som i modellen av Paulus och Jeske, utan korrigering av stabilitet): luften till vattnet är mättad till SST (98% för salt), därför

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

begränsat mellan 0 och 40 m. För att fälla 144 MHz skulle det ta cirka 180 m (Kerr), sedan vid 144 MHz förångningskanalen förstärker lite; från 432 MHz upp, med låga antenner på havet, mycket (punkt 11 bis). Ange den paraboliska ekvationen och i linje Korrigeringar 1.3.2.

Dagens tid (besök). Programmet beräknar solens höjd på varje punkt. Från dag till dag på jorden, med solen över 15 ° och gränsskikt över 800 m eller vind över 5 m / s, varnar för att markkanalerna är omformade, på natten, med sol under −3 °, vind under 3 m / s och gränsskikt under 300 m, varnar för att med lugn himmel är möjliga leds av bestrålning, som faller efter soluppgången. Inte på havet. För nu är de upptäckta, inte siffror: december validering har inte tillräckligt med sommardagar för att kalibrera dem. Det är två punkter som författaren anser vara viktiga, från daglig observation.

Normal profil CRPL (alternativ). Istället för 40 N/km fastställs den normala minskningen följer den exponentiella referensatmosfären CRPL: N(h) = Ns·e ^ (−c·h), med N i första km = 7,32 ^ (0,005577 · Ns) (Bean and Thayer 1959). Med våt luft (Ns high) är det normala brantare och tjänar en starkare gradient för att ge vinst; i höjd är sötare. Visas dåligt tills validering säger att det hjälper.

## 9 quater. Kontrast lufthav och låga hav: Adriatiska havet (från 1.3.3)

Radiovågor känner inte havsbotten: känn kontrasten mellan luft och vattenyta. Djupet räknas eftersom det bestämmer hur snabbt temperaturen på havet följer luften.

Adriatiska havet är lågt hav. Det genomsnittliga djupet är cirka 250 m, men den norra bassängen överstiger sällan 100 m och Trieste-viken förblir under 25 m; endast i söder sjunker över 1200 m. Ett lager av vatten så tunt värmer upp och kyler på några dagar, Tyrrhenian på veckor. Det finns tre typiska situationer:

1. Vår och tidig sommar, gynnsam. Havet är fortfarande kallt, även för det färska vattnet i Alpine och Po floder, det kommer varm och torr luft från slätten eller Balkan. Varm luft över kallt vatten bildar en omvändning på havet (advection pipeline): är det klassiska tillståndet för öppningarna mellan Italien, Slovenien och Kroatien.
2. Hösten och vintern med bora, ogynnsam. Kall luft över ett varmare hav: det låga lagret blir instabilt, blandningar och kanalen bryter.
3. Efter en bora, chans. Bara för att havet är lågt, kyler en bora det några grader på några dagar. Om du går in i en anticyklon med varm luft är lufthavskontrasten stark och ledningen är mer robust än vanligt.

Adriatiska havet är också en stängd kanal mellan Apennines och Dinari, mindre än 200 km bred: havsskiktet förblir begränsat, och rutterna längs axeln (Puglia-Venezia-Trieste, cirka 700 km) förblir allt på havet, det bästa villkoret för en lång kanal.

Vad programmet beräknar. På havspunkterna, med väderkorrigeringar och lådan "Contrasto aria-mare":

| Villkor | Korrigering |
| --- | --- |
| potentiell temperatur vid 925 hPa (eller 950) över SST 4 K | _ |
| bortom SST av 7 K | +2 dB |
| med luftduk vid 925 hPa (över 80%) | −0,5 dB på dessa bonusar |
| kylt hav av 1,5 °C eller mer på 72 timmar före, med gynnsam kontrast | _ |
| varmare hav av luft vid 2 m av 2 °C (blandning) | −1,5 dB |
| Varmare hav av 4 °C | −3 dB |

Den totala korrigeringen förblir mellan −3 och +3 dB och utan ledning förblir vinsten under det gula taket. Trösklarna är tillfälliga: i standardatmosfären överstiger den potentiella temperaturen vid 925 hPa havet på cirka 2-3 K, då indikerar 4 K redan en omvänd.

Minnet av SST. Modeller håller havstemperaturen nästan stadig under förutsägelse. Den senaste minskningen härrör från analysen av de tre dagarna innan (Open-Meteo, endast yttemperatur, endast havsceller): maximalt 72 timmar före minus det aktuella värdet. Om tjänsten inte svarar gäller inte minnesbonusen.

Begränsningar. SST av modellerna under kusten och i övre Adriatiska havet är grov; den kalla pennan i Po, till exempel, löses illa. Batimetry går inte in i beräkningen: källorna ger alltid SST, som redan summerar effekten av havsbotten. Lufthavskontrasten överlappar delvis i slutet av Misme stabilitet (§ 9 bis); för detta är bonusarna små. Trösklarna kommer att vara tjära med korridorerna för reklam och QSO genom Adriatiska havet.

## 10. Utförs på hela vägen

Om lagret fäller bandet på minst 80% av den använda fraktionen och stationerna inte är i sikte, är förlusten i ledningen också beräknad: ledigt utrymme, plus kopplingen av antennerna (3 dB om de är inne i ledningen, mer om de är över eller under), plus den kvarvarande dämpningen ≤ 5 · 10−5 ·f ^ (1/3) dB/mrad av ITU-R P.452 eq. 49 [6]. Om det är mindre än förlusten utan trop blir den förväntade förlusten.

Ett band förblir endast i kanalen om dess våglängd inte överstiger λmax = 2√2 ·(2/3) {(ΔM·10−6)·d (Kerr, q 17 [11]). För detta samma lager kan fälla 23 cm och 70 cm men inte 2 m. Men gränsen är inte netto: Kerr varnar för att även vågor flera gånger längre lider mycket av ledningen, och Hepburn och VHF / UHF DX Book talar om "sphered" kanter och "förlåtande" kanaler. Från 1,3.1 programmet använder en mjuk ledning: om λmax är mellan λmax och 3 Ḳmax, går vinsten gradvis från 15 dB (λ = 3 Ḳmax) till full ledningsvinst (λ = λmax), med s = (λmax / λ − 1/3) /(2/3). I så fall är kanalfärgerna tillåtna. Loggen och Meteo linje längs vägen indikerar punkterna med mjuk kanal.

## 10 bis. Via Monte

När korrespondenten är stängd bakom horisonten söker programmet den bästa riktningen för att peka på antennerna: den upplysta sluttningen sprider signalen som ett radarmål. Den kör horisonten var 5: e (förutom ± 15° runt korrespondenten) och i varje riktning tar den upplysta horisonten, över 100 m och mellan 1 och 60 km. En snabb uppskattning (fritt utrymme, bitaktisk omvandling och 10 dB för varje nivå av horisont från diffusorn till korrespondenten) väljer 4 mest lovande, minst 15° vardera från den andra; för varje:

- behandla 1, station → lättnad, i det fria utrymmet;
- behandlar 2, lättnad → korrespondent, beräknad som om lättnaden överförs från 10 m över toppen, med samma förutsägelse (utan parabolisk ekvation).
- omvandling med den bistatiska radarekvationen, med ≤ = ≤0 · A, ≤0 = −15 dB (berg i VHF/UHF) och A området upplyst av R1 -strålen (maximalt 20 km2):

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

"Rekommenderad punkt" -linjen indikerar azimuten på den bästa vägen, om den kostar mindre än den direkta. Vid 144 MHz, med en lättnad på 20 km och en 15° yagi, kostar bergsvägen cirka 45 dB mer än 2-rutten ensam: det händer bara när direkten är stängd av angränsande hinder. Uppskattningen är brutto (±10 dB), särskilt för ≤0.


Utan förlust till berget (option, från 1.3.2). Med rutan "Via monte senza perdite fino al monte" anses fallet mellan stationen och diffraktionspunkten null: är som att föra stationen till toppen (antenn till 10 m över punkten, samma kraft, antenn och kabel). Horisonten rund väljer sedan punkten med den mest fria horisonten mot korrespondenten (vid paritet, den högsta) och resultatet visar vägen från punkten till korrespondenten, med signal och S/N, bredvid den totala med diffusionen. Det är en optimistisk gräns: säger om riktningen öppnas när berget höjer signalen väl, eller vart man ska gå bärbar. Varje punkt ges med tio tecken.

## 10 ter. Inträde till en avlägsen ledning

Om ledningen som fäller bandet börjar långt, bortom stationens horisont, måste signalen anlända. Programmet ger signalen till utgångspunkten, mitt i ledningen, för diffraktion: längs Copernicus-profilen GLO-30 söker kanten med den högsta Fresnel-parametern (ITU-R P.526), med svullnad av motsvarande mark,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

mer dämpning på grund av krökningen av toppen (rundat hinder, P.526-16 § 4.2), med R-radien uppskattad från Copernicus GLO-30-jorden till ± 1 km från kanten,

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

(för m·n > 4 är formuläret 34b i rekommendationen). Barsis (NBS, 1963) mätt på tre långa vägar över ett berg (Colorado, Alaska, Europa) genomsnittliga förluster 10-20 dB högre än den ideala kanten av Fresnel-Kirchhoff: avrundningen förklarar mycket (Barsis och Kirby, 1961: Pikes Peak med en radie på cirka 11,5 km). Resultatet jämförs med diffraktion på slät sfärisk jord; det är det värsta. Sedan reser signalen i ledningen: ledigt utrymme på hela rutten, kvarvarande dämpning av P.452 på sektionen i ledningen och kopplingen av korrespondenten (3 dB inuti, 1 dB var 10 m utanför). Om denna förlust är lägre än den förväntade förlusten blir den mekanismen ("uppförande (inmatning genom diffraktion)"). Det är författarens premiss på italiensk geografi: med ett närliggande hinder långt under linjen är v negativt och ingången kostar lite; med en dorsal front kostar 15-30 dB.

## 10 quater. ITM (Longley-Rice) och exit från kanalen

Från 1.3.2 innehåller programmet Irregular Terrain Model 1.2.2 av NTIA/ITS (Longley och Rice, ESSA ERL 79-ITS 67, 1968; Hufford, Longley och Kissick, NTIA 82-100, 1982), översatt till C# av den officiella kod för den amerikanska regeringen, som är fri från rättigheter. Översättningen ger samma siffror som originalet: auto sammanställning jämför det varje gång med NTIA C + + kod på fem testfall (ner under 0,01 dB).

ITM beräknar förlusten i normal atmosfär på den verkliga profilen Copernicus GLO-30: ses med reflektion från marken, diffraktion (spigol och sfärisk mark som vägs på jordens grovhet Δh, det är skillnaden mellan den 10: e och 90 ° percentilen av kvoterna runt raka av de minsta rutorna) och troposcatter. Jordreaktiviteten reduceras till den genomsnittliga delen av rutten, $N_s = N_0\,e^{-h/9460}$ och ger krökningen av motsvarande mark,

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

Programmet använder radioklimatet på den valda rutten som i § 10 sexies (fram till 1.3.4 alltid tempererat hav, med N0 320 på marken och 350 på havet), med N0 från den genomsnittliga reaktiviteten av platsen (själ εr 15, ÷ 0,005 S / m; havs ≤r 81, ÷ 5 S / m), horisontell polarisering och variabilitet över tiden utan den platsen (profilen är känd). Det visar att förlusten överskrids i 50% av tiden och den du har i 10% och 1% av den bästa tiden. ITM modellerar inte kanalerna: dess variation över tiden är statistisk, genom klimatet. Det fungerar som en andra beräkning av baslinjen bredvid P.452, skillnaden mellan de två ger en uppfattning om osäkerhet.

Den andra användningen är den sista sträckan efter en ledtråd som slutar före motsvarande. Vid utgångspunkten placeras en virtuell antenn halvvägs genom kanalen och ITM beräknar tarmen till motsvarande, som överskott över det fria utrymmet på 50% av tiden. Total förlust är

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,	heta_{condotto} + A_{ITM}(d_{uscita} 	o RX)
```

där A_TX är kopplingen av antennen (3 dB inuti, 1 dB var 10 m utanför) om ledningen börjar nära, annars inträde diffraktion av § 10 ter. Det är den gräns som författaren har gett sig själv: kontinuitet i lagret plus, högst, kvarvarande dämpning. Om det är lägre än den förväntade förlusten blir det mekanismen ("uppförande (utgång och slutlig stretch ITM)").

## 10 quinquies. Fördjupning: öar, straits och jord-hav avhållsamhet

Ett exempel från Neapelviken (Anacapri, JN70CN) mot västra Medelhavet visar var rutter korsar öarna:

| Destination | Azimut | Där den korsar öarna | hinder för hinder |
| --- | --- | --- | --- |
| Barcelona Barcelona Barcelona |  279°  | norr om Sardinien (Gallura) till 41,05 ° N, mellan 420 och 490 km | ca 30 km söder om Bocche di Bonifacio; Limbara (1359 m) ca 20 km från linjen |
| Bocche di Bonifacio |  282°  | Den smala till 41,3° N | ca 11 km |
| Marseille |  296°  | Centralkorsika vid 42,2 ° N | berg upp till 2700 m: en ledning under 2 km stannar |
| Valencia, Balearics |  260–265°  | Central Sardinien | interna lättnader |

För Barcelona är det tillräckligt att peka på 282° istället för 279° eftersom axeln passerar från munnen: 3rd, inuti strålen av en normal yagi.

Är munnen ett strait? För de låga kanalerna, under några hundra meter, ja: öarna är muromgärdade, och över jorden, per dag, bryter uppvärmningen inversionen. Själva sundet är tillräckligt stort: ca 11 km, mot ett första område av Fresnel, mindre än en kilometer bred, hälften av en 1000 km rutt i 144 MHz. Problemet är väder. Munarna är roliga för vinden: Det lägre lagret accelereras och det lägre lagret avlägsnas. Ofta är passagen öppen som geometri men störd som atmosfär. Med en stadig anticyklon och liten vind kan istället vara den goda passagen.

Jord-sjö skillnad. De räknar. En ledning som bildas på den tyrrhenska kusten avbryts: signalen kommer ut från ledningen (avsnittet efter utgången, § 10 quater) och måste återvända till en annan ledtråd på Östersjön, betala igen kopplingen. Kanalerna på höjd, över 1500 m, passerar över Gallura utan att inse det; för centrala Korsika gäller inte.

I praktiken: om rutten korsar en bergig ö, prova någon grad av korrigering mot sträckan av fritt hav, men räkna inte på passagen med stark vind i smal.

## 10 sexiga. TN101 radioklimat och tidsvariabilitet (från 1.3.5)

TN101 § 10 [25] beskriver hur förlusten av en rutt bortom horisonten varierar under lång tid runt medianen, med olika kurvor med klimat: Y(q)-funktionen säger hur många dB förlusten sjunker i q% av den bästa tiden. ITM innehåller dessa kurvor för sju klimat. Klimatet räknas mycket: på havet i det tempererade marina klimatet, vid 144 MHz, i 10% av tiden finns det många fler dB än på landet i kontinentalt klimat, eftersom kanalerna är vanligare.

Programmet fixar inte längre klimatet: Han väljer det för varje rutt från genomsnittet, så det fungerar över hela världen och inte bara i Europa. Ladda ner från Open-Meteo [26] tre år av dagliga värden av ERA5 -reanalys (temperatur, fuktighet och marktryck, regn) för cellen på 0,5° av genomsnittspunkten, en gång bara; värden kvarstår i Data / CLIMA. Återställande av jord

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

dag för dag, då årsgenomsnittet och utflykten mellan månatliga medelvärden, genomsnittlig fuktighet och årligt regn. Reglerna följer beskrivningarna av klimatet i TN101 § 10.2, gränserna mellan ett klimat och det andra är ett kriterium för författaren:

- över 60° latitud: tempererad kontinental ( TN101 har inga kurvor för polarklimatet).
- mindre än 250 mm regn per år och genomsnittlig fuktighet under 50%: öken (Ns typiska 280).
- inom 20° från ekvatorn, Ns på minst 350 och utflykt upp till 30: ekvatorial (Ns typiska 360).
- inom 32° och utflykt av minst 60: subtropisk kontinental, som är monsuner (typisk utflykt 60–100).
- mellan 10° och 32° med Ns på minst 340: subtropisk marin (Ns typiska 370);
- båda horisonterna på havet, över 20°: tempererat hav på havet (det är definitionen av TN101).
- utflykt upp till 30 nära havet (hav på väg eller genomsnittlig fuktighet på minst 75%): sjöfart härdad på jorden.
- tempererad kontinental (typisk utflykt 20–40).

N0 för ITM är den genomsnittliga reducerade till havsnivån, N0 = Ns ^ (h/9460). Utan ett nätverk används en reservregel med enbart latitud och sjöfraktionen, och linjen säger det.

Tester med data ERA5 2023–2025: Capri Ns 339 med utflykt 45 (kontinental härdad på jorden, havet tempererar när båda horisonterna är på havet); London 327 och 16 (härdad stjärna); Wien 319 och 24 Abidjan 381 och 11 (ekvatorial); Miami 365 och 36 (subtropisk make); Delhi 337 och 82 (subtropisk konkontinental); Riad 278 och 37, 90 mm regn (öknen). I Chile: Antofagasta 341 och 19 (subtropisk man: Atacamas kust, med den nästan fasta låga omvända av Stilla havet), Calama 232 på marken, 2257 m, 14 mm regn (öken), Valparaíso 331 och 11 (temperatursöm), Punta Arenas 313 och 5 (temperatursöm).

Linjen "Radio klimat ( TN101)" visar klimatet, orsaken, Ns med hälften av utflykten och Y: hur många dBs tjänar i 10% och 1% av tiden och hur många förlorar i 90%. Det är långsiktig statistik och förblir separerad från prognosen: den förväntade förlusten följer dagens tid med GFS-profilerna, och summan av de två sakerna skulle räkna två gånger samma onormala förökning.

## 11. Jämförelse med ITU-R P.452

Jämförelselinjen P.452 tillämpar ITU-metoden för kanaler och reflektion på lager (P.452-16 § 4.5 [6]) med ΔN-medianen av GFS istället för den klimatologiska och p = 20% som kalibreringskonstant, som i studien CSIRO [10]. Det inkluderar förlusten av empirisk koppling Alf under 0,5 GHz (cirka 28 dB till 144 MHz Det är bara en jämförelse: den förväntade förlusten förblir den i punkt 9.

## 11 bis. Parabolisk ekvation (från 1.3.1)

Parabolisk ekvationslinje är en fjärde beräkning, bara av jämförelse, gjort som professionella program för kanalerna (PETOOL; våg-förökningsramen för M. Lytaev, MIT-licensen): istället för att uppskatta vinsten från lagren, går den fram längs vägen med Fourier-split-steg-metoden inuti M-modifierad GFS-refraktiv profil, tagen från prognospunkten närmare varje avstånd. GFS-nivåerna läggs till de uppskattade hoppen av de potentiella kanalerna (beräkna A, 50 m i halvskikt). Marken Copernicus GLO-30 går in i steg (under marken är fältet null), jorden är ledare och strålen av antennen är en Gaussian med den uppskattade öppningen från vinsten. Beräkningen ser ledningarna, de som förlorar, diffraktionen och kopplingen av antennen; Han ser inte troposcatter, som läggs isär. Nästa är "profilchecken": samma med standard atmosfär mindre den med den förväntade profilen.

Från 1.3.2:

- propagatorn är i stor vinkel, med den exakta fasen av det fria utrymmet:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- botten på havet och på kusten slätten vid nollhöjd återspeglar med koefficienten av Fresnel i horisontell polarisering, εc = εr − j·60° (sea śr = 81 och ÷ = 5 S/m; land 13 och 0,006 S/m), vid den klara vinkeln på rutten:

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- Grovheten i havet minskar den konsekventa reflektionen enligt Miller och Brown, med den effektiva höjden av vågorna från vinden till 10 m (αh = 0,0051 · U2):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- På havet innehåller profilen M förångningskanalen (punkt 9 ter) med den neutrala logaritmiska profilen:

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

Det testar med prototypen (10 m antenn på havet, vinna på standardatmosfären på 100-300 km): med δ = 10-30 m avdunstningskanalen ger 1-3 dB till 144 MHz, 8-37 dB till 432 MHz och 21-66 dB till 1296 MHz. Vid 144 MHz i horisontell polarisering speglar havet nästan som en metall (mer än 0,99 till några tiondelar av graden) och grovhet räknas lite: den nya botten förändras några tionde dB; det räknas mer från 432 MHz upp.

Gränser: över jorden den vertikala profilen förblir GFS nivåer, så de tunna kanalerna förbli osynliga; marken till steg är ungefärlig; strålen förblir inom 6: e.

## 12. Antenner, signal och ljud

- Yagi DL6WU: Få 9,5 + 7,8 · logg10 (boom/λ) dBd (2,35 dB för dubbelboom, DL6WU åtgärder på VHF Communications 3/1982), plus 2,15 för [12] dBi.
- EIRP = ström − kabel + TX vinst.
- Signal fick = EIRP − förväntad förlust + få RX − kabel RX. S-meter IARU: S9 = −93 dBm, 6 dB per enhet.
- Externt buller enligt ITU-R P.372 [13]: galactic (Fa = 52 - 23 log f) mer artificiell enligt miljön (stad, bostad, landsbygd, lugn landsbygd) eller en handskriven antenntemperatur.
- Tsys = Ta + Tpre + [290 (Lc − 1) + 290 (Fr − 1)·Lc] / Gpre till antennens terminaler (Friis formel). Från 1,3,5 den mottagande stationen har alltid en preamp i antenn, fast parameter: buller nummer 0,8 dB (Tpre = 59 K) och få 20 dB. Bakom det finns kabeln (förlust Lc) och radion, med grundläggande Fr buller nummer 6 dB. Med 1 dB kabel sjunker kabel- och radiobidraget från cirka 1160 K till 12 K: bullret bestämmer himlen, miljön och preampen. Vinsten på 20 dB är ett typiskt värde som valts av programmet.
- S/N i SSB, CW och FT8 i bandet på varje sätt, jämfört med tröskeln.
- Sannolikheten för perioden (från 1.3.2): evanescensen inom timmen är en normal fördelning av S/N runt det förväntade genomsnittliga timvärdet. Bray (QST, november 1961, s. 39, på kurvorna i Norton 1959) ger en amplitude på 13,5 dB över cirka 100 miles, dvs cirka 7 dB under och 7 över genomsnittet, som nästan faller strax under 100 miles (3,5 dB till 50 miles); för en "solid" anslutning tar det 7 dB från budgeten. Tas ± 7 dB som ±2π: ≤ = 3,4 dB över 160 km, proportionellt mot avståndet nedan. Sannolikheten att en period överstiger tröskeln på vägen är

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  med ≤ normal distributionsfunktion: marginal 0 dB = 50%, +3,4 dB = 84%, +7 dB ("fast" marginal av Bray) = 98%. Det inkluderar inte osäkerheten i modellen, som är större.
- Digitala sätt: S/N på 2500 Hz jämförde med de tröskelvärden som mättes med 50% avkodning, utan tidigare information (AP). FT8 −21 dB och FT4 −17,5 dB på vitt brus; när mekanismen är troposcatter använder trösklarna med evanescens "störd latitudmedium", FT8 −18,6 dB och FT4 −15,2 dB [21]. Q65 -15C −22,2 dB, Q65 -30B −24,8 dB, Q65 -60A −27,6 dB I flygplan scatter kan Doppler överstiga tonerna av Q65 -60A (1,7 Hz): det finns bättre än 30B eller 15C. JT65A -25 dB [22]. Den avkodande AP (namnet på den redan kända eller änden av QSO) ger ca 2 dB extra för FT8 och 1,5 till 2,6 dB för Q65; det går inte in i tröskeln eftersom det inte är giltigt för alla «−24 dB” värden som ofta läses för FT8 är gränsen för WSJT-X relationer eller AP avkodning, inte tröskeln på 50%. [24]

## 13. Svar och datatider

Varje beräkning mäter sin egen tid: GFS-profiler (nätverk, analys, obligatoriska avbrott på 20 s mellan partierna för Open-Meteo-gränsen), Copernicus GLO-30 terräng (med nedladdning av rutorna, 15-30 MB vardera, bara första gången), beräkning. Svarstidslinjen sammanfattar dem; i registret börjar raderna med TEMPI. Vid uppgradering av rutnätet rapporterar registret också att GFS-kapplöpningen användes, när den blev tillgänglig och hur gammal datan är.

Från 1,3,2 kan du också välja ICON-2I, modellen av byrån ItaliaMeteo och Arpae på Italien på cirka 2,2 km, genom Open-Meteo (modell italia_meteo_arpae_icon_2i, två raser per dag, 3 dagar). Det täcker bara Italien och de närliggande haven och har mindre låga nivåer av GFS (1000, 925, 850, 700, 500 hPa): för kanalerna på höjden är inte bättre än GFS, men det fina nätet beskriver bra kuster, öar och hav på 2 m, det är förångningskanalen. De variabler som en modell inte har (stratgräns, temperatur på ytan...) tar bort sig från begäran. Från 1.3.2 använder den automatiska källan endast NOMADS; Open-Meteo tjänar om NOMADS inte svarar och för historiska fall. Med "Uppdatering av sig själv" -rutan (belyst av defekt) är prognosen oklickad: vid öppningen, vid varje ny GFS-lopp på NOMADS (ca 5 timmar efter loppets tid) och när den synliga zonen kommer ut i mer än en fjärdedel från rutnätet; högst ett försök var 15: e minut, aldrig med ett historiskt fall.

Förfrågningar på Open-Meteo har gränser per minut, per timme och per dag för varje internetadress. De nedladdade profilerna sparas på disken (Dati/cache-gfs) för varje stroke och modell: med samma lopp, som ändras var 6: e timme, de poäng som redan laddats ner frågar inte igen. Med den automatiska källan börjar du från NOMADS och Open-Meteo tjänar bara för saknade punkter; med den enda Open-Meteo, vid tidsfristen eller dagliga stopp omedelbart och säger när du ska försöka igen. Historiska fall är inte cachade.

## 14. Validera modellen

Prognosen bör jämföras med verkliga förbindelser. Beräkningsloggen håller varje nummer; programarkivet håller plats och avkodning. För 432 och 1296 MHz, där fläckarna är få, behöver de rapporter från operatörerna: som gjorde anslutningen, till vilken tid, med vilken signal. Jämför i många timmar den förväntade vinsten med den uppmätta signalen säger om trösklarna ska flyttas.

## Källor

1. NOAA/NCEP, Global Forecast System (GFS), global modell vid 0,25°, fyra tävlingar per dag.
2. Open-Meteo, förutsägelse API (open-meteo.com), gfs_seamless modell, variabler vid trycknivåer och racing metadata.
3. Copernicus DEM GLO-30 (ESA; WorldDEM © DLR e.V. 2010–2014 e © Airbus Defence and Space GmbH 2014–2018, levererad som en del av Copernicus av Europeiska unionen och ESA), 1 andra båge, GeoTIFF-ramar på det öppna registret över Amazon Web Services. Reserv: NASA/USGS Copernicus GLO-30 i Terrain Tiles "skadi" av AWS.
4. ITU-R P.453, Radiorefraktivt index: dess formel och refraktivitetsdatum, med digitala kanalkartor (Study Group 3, © ITU, personlig och icke-kommersiell användning).
5. B. R. Bean, E. J. Dutton, Radio Meteorology, NBS Monograph 92, 1966: Tabell 6.5 och § 4.4.
6. ITU-R P.452-16, Prediction-procedurer för utvärdering av störningar mellan stationer på jordens yta vid frekvenser över cirka 0,1 GHz: § 4.2 (siffraktion), q. 45 (troposcatter), § 4.5 och q. 49 (ledd).
7. ITU-R P.834, effekter av troposfärisk refraktion på radiovågsförökning.
8. ITU-R P.525, Beräkning av fri-rymd dämpning.
9. ITU-R P.526, Förökning genom diffraktion: eq. 31 (cutcheon) och § 3 (sfärisk mark).
10. B. Indermuehle, H. Suzuki, Tropospheric Ducting Prediction baserat på GFS Model Data på Inyarrimanha Ilgari Bundara, CSIRO Murchison Radio-astronomy Observatory, Publications of the Astronomical Society of Australia, 2026 (arXiv 2608.12861).
11. D. E. Kerr, Propagation of Short Radio Waves, MIT Radiation Laboratory Series vol. 13, 1951: eq. 17 e 18.
12. G. Hoch DL6WU, "Extremely Long Yagi Antennas", VHF Communications 3/1982, pp. 130 et seq., fig. 1 (mätt 432 och 1296 MHz enligt bommens längd).
13. ITU-R P.372, Radiobuller: galaktiskt och artificiellt ljud för miljön.
14. NOAA / NCEP, NOMADS, GRIB-filter av GFS 0,25 ° tid (nomads.ncep.noa.gov); WMO G RIB2 -format, komplex packning med rumsliga skillnader.
15. Open-Meteo, Historisk prognos API (modellprognos arkiv från omkring 2021).
16. LukiBox, TropoLink, design av troposcatter-rutter med P.617, NBS TN101 och ITM flanked (GitHub, Apache 2.0 licens).
17. ITU-R P.617-5, Propagationsförutsägelsetekniker och data som krävs för utformningen av transhorisontella radioreläsystem, 2019.
18. RSGB, VHF/UHF Handbook; DL2FZN implementering (andi-f/troposcatter på GitHub, 2013).
19. W. F. Moler, D. B. Holden, "Troposfärisk scatterförökning och atmosfäriska cirkulationer", Journal of Research of the NBS 64D, 1960, doi 10.6028/jres.064d.014: signal bortom horisonten (1365 och 9380 MHz, på havet) och vertikal rörelse av luften.
20. ARRL Handbook 2022 (99a ed.), § 19.4.3–19.4: Troposferiskt väder och förökning.
21. S. Franke, B. Somerville, J. Taylor, FT4 och FT8 Communication Protocols, QEX juli/augusti 2020.
22. W3SZ, digitala slow mode trösklar ( JT65, JT9, QRA64) och modeval enligt propagation, 2019.
23. QST, "Världen ovanför 50 MHz", november 2022: öppning av 14-15 augusti 2022 mellan Manitoba och Kansas/Missouri.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Quick-Start Guide till Q65, 3 April 2021 (wsjt.sourceforge.io).
25. P. L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis, Transmission Loss Predictions for Tropospheric Communication Circuits, NBS Technical Note 101, Magazine 1967: § 9 (troposcatter, funktion F(θd), H0), § 10 och Annex III (radio klimat och variabilitet över tiden).
26. Open-Meteo, Historical Weather API (ECMWF ERA5 recension sedan 1940; Copernicus Climate Change Service, CC BY 4.0 licens).
