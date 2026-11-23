Lifetropovhf tekniska dokument 1
# Operation av programmet

> Automatisk översättning av italienska: I händelse av tvivel är det värt den italienska guiden.

Vad programmet gör, varifrån det tar data och hur det förvandlar dem till vad du ser på kartan och i listan. Version av den 9 november 2026, relaterad till projektet i C# (.NET 10 + Avalonia).

## Sammanfattning

- Syfte: tropoprognos (Hepburn [3], F5LEN [4], isobare GFS [18]) och verklig observation av band i samma fönster, enligt LA0BY [1] kontrolllistan.
- Datainsamling: två DX kluster, omvänd Beacon Network [9], dm4x kluster och PSKReporter [5] MQTT flöde, UDP avkodning från WSJT-X, JTDX och MSHV [8].
- Spot: bandsegment, negativt filter, airscatter i streckgul, position av locatorer eller C ALL3.TXT [6] [7].
- Vem lyssnar: mörka rosa cirklar med rosa linjer som alltid syns mot stationerna kände [5].
- Spot kort: avstånd, azimut, tropoindex på vägen och troposcatter förlust med G3YGF [2] metoden.
- Språk Hepburn och F5LEN: [3] bild och kalibrering bänk [4].
- Titta på: NTP-kontroll på 10 servrar var tredje minut [10] [11].
- Misstänkta rapporter: EME i Q65/JT65 erkände med månens position, truncated locators, skimmers, karantän och lyssnare, med regler som lärts ensam [20].
- Kriterier för kassering och betydelse av de färger som sammanfattas i två tabeller (avsnitt 11 ter och 11 quater).
- Teman, 36 [17] språk, exportera dokument till Word med redigerbara formler [16], bärbara program och multiplatform [12] [13] [14] [15].

## 1. Syfte

Programmet sätter ihop i ett enda fönster två saker som vanligtvis ser på olika platser: förutsägelse av troposfer propagation (Tropo index av Hepburn och F5LEN, isobare) och observation av vad som verkligen händer på band (fläckar från DX kluster, Reverse Beacon Network och PSKReporter, plus listan över stationer som lyssnar). Referensbandet är 144 MHz; fläckarna samlar in från 50 MHz upp.

Inställningen följer kontrolllistan som föreslagits av LA0BY för extrema tropohändelser: under de föregående dagarna tittar du på Hepburn- och F5LEN -prognoserna och temperaturprofilerna på höjden, medan evenemanget är på DX-klustret och på platsen kartlägger bevisen att öppningen börjar [1]. LA0BY observerar också att de enda Hepburn / F5LEN -kartorna inte räcker för att förutsäga de längsta anslutningarna. [1]

## 2. Fönstret och flikarna

Fönstret är ett och delas inte in i mobila fönster. Från topp till botten:

- Allmänna inställningar rad: Ring och Locator till vänster; till höger UTC-klockan (t.ex. 08/11/2026 20:33:38 UTC, bärnsten) följt av NTP:OK eller NTP:FAILED, temamenyn och språket.
- I fem grupper: Karta och plats; Prognoser (Propria β, prognosen beräknad av programmet och beskrivs i prognoshandboken, sedan Hepburn och F5LEN); GRAVES; Data och källor (DX kluster, PSKReporter, Reverse Beacon, UDP, Utnämningar och misstänkta rapporter); Guider (Program Operation, Map Building, Hur man formar tropo, Manuell prognos, Tips till operatören, med en sökruta som räknas i vilken guide visas). Under balken visar en bar gruppens underspråk; varje grupp minns den sista.
- Innehållet i fliken och, längst ner, statusfältet med källornas tillstånd, databasen med namn och tiden för den senaste uppdateringen.

De bästa linjeinställningarna gäller för alla flikar: det valda temat färgar också datainsamlingen och bakgrunden av dokument; Dokumentsidorna förblir lika vita som ett ark, så de skriver ut bra.

## 3. Ring och hyresvärd

Maidenhead hyresvärden kan ha 4 eller 6 tecken (t.ex. JN70 eller JN70CN). Programmet omvandlar det till koordinaterna i mitten av torget: fältet 20 × 10 °, kvadrat av 2 ° × 1: a, subquadrate av 5' × 2,5'. Om texten inte är en giltig locator blir lådans kant röd och stationen visas inte på kartan. Locatorn fungerar också som ett centrum för azimuth-projektion och som utgångspunkt för avstånd och azimuth i annonskortet.

Call är namnet som används som klusterinloggning. Vid den första starten, tills det finns en giltig nominering, klusterna inte ansluta: ett fönster ber om namnet (och valfritt hyresvärden); bekräftas, namnet kopieras som ett användarnamn i Login-fältet för varje kluster som har det tomt och kluster lämnar. Med "Senare" kvarstår fortfarande, och Connect-knappen för varje kluster kräver nomineringen igen. MQTT, UDP och NTP använder inte inloggning och lämnar ändå.

## 4. Datainsamling: källor

Varje källa har sitt eget avsnitt i fliken Datainsamling, med Connected/Disconnected, de erkända och anslutna lådorna vid start, status, meter och register över de mottagna raderna. Unbanded eller kasserade linjer från filtret visas inte i registret: räkna bara sektionsmätarna. Avsnitten flankeras av två till två.

| Källa | Anslutning | Vad ger |
| 1. DX kluster val | on0dxk.dyndns.org:8000 kommandot set/filter dxbm/pass 6 | Spot av amatörradio, även SSB och CW. På ON0DXK filtreras VHF och aktiveras med det angivna kommandot (poi pass 4, 2...). |
| 2. Andra källan | dxmaps.com:7300 (DXMAPS, EA6VQ) och s50clx.si:41112 ( S50CLX, Slovenien) | DXMAPS samlar VHF-fläckar och ger även RBN-skimmerfläckar (uppsättning / skimmerkontroll, redan inställd); S50CLX är ett DX Spider-kluster. Logga in med ditt namn; redigerbara adresser. |
| 3. Omvänd Beacon Network | telnet.reversebeacon.net 7000 CW/RTTY · 7001 FT8 | Automatisk skimmer spot. Logga in med namnet endast [9]. |
| Cluster med PSKReporter | dm4x.ddns.net:8500 | Digitala Spots av PSKReporter i klusterformat. |
| PSKReporter MQTT | mqtt.pskreporter.info 1883 · 1884 med TLS | Varje mottagningsrapport i realtid: vem överför, vem lyssnar och deras [5] Locators. Band 144, 222 och 432 MHz. |
| 6. Vädret och tropokartor | dxinfocentre.com/tropo_eur.html tropo.f5len.org/prognoser-för-europeiska modell GFS | Hepburn [3] och F5LEN [4] tropo indexkartor, havsnivåtryck för isobare (GFS via Open-Meteo [18]). Hepburn och F5LEN måste tillåtas författarna. |

Cluster telnet (källor 1–4). Efter anslutningen väntar programmet på namnbegäran (text som innehåller inloggning, samtal:, callsign eller ditt samtal); om det inte kommer inom 4 sekunder, skicka inloggningen. Skicka sedan kommandona i avsnittet, separerade från punkt och komma, en var 0,6 sekunder. Telnet kontroll tecken tas bort innan du läser raderna. Om anslutningen faller och Riconnetti är aktiv, försök igen efter 10 sekunder (15 om servern inte svarar alls) [6].

PSKReporter MQTT (källa 5). Programmet registrerar sig i ämnet pskr/filter/v2/{banda}/{modo}/{tx_call}/{rx_call}/{tx_grid}/{rx_grid}/{tx_dxcc}/{rx_dxcc} fixa bara bandet och lämna de andra fälten gratis med karaktären + [5]. Det exakta namn med vilket servern anger banden (t.ex. 2m, 1,25m, 70cm) måste kontrolleras vid den första anslutningen.

## 5. Från klusterlinjen till platsen

En linje är en plats när den har formuläret DX de SPOTTER: frekvens DX kommentar hhmmZ. Programmet härleder spotter, station DX, frekvens i kHz, kommentar och nu UTC; namnen tar bort suffixet av skimmers (-#, -1...). Ansök sedan i denna ordning följande regler [6].

| Band | Segment accepterat |
| GRAVES (med 144) | 143 040 – 143 060 MHz |
| 144 MHz | 144 – 148 MHz |
| 222 MHz | 220 – 225 MHz (endast Region 2 ITU, Amerika) |
| 432 MHz | 420 – 450 MHz |
| 1296 MHz | 1240 – 1300 MHz |
| 2320 MHz | 2300 – 2450 MHz |
| 10 GHz | 10,5 GHz |

1. Band. Frekvensen måste falla i ett av de förvaltade segmenten; resten, och i synnerhet varje kommersiell under 49 990,0 kHz, visas inte och räknas som "av band".
2. Vägen. Ett av de mest kända sätten att kommentera är FT8, FT4, Q65, JT65, ISCAT, CW, SSB, FM, RTTY, PSK31, MSK144, FSK441, JT6M); USB och LSB blir SSB.
3. Negativt filter. Platsen kasseras om den innehåller lägena MSK144, FSK441, JT6M, JTMS, ISCAT eller taggarna <AU> <AUE> <ES> <MS> <FAI> < F2 >, med icke-nukleära överkroppar och även utrymmen inom parentes. Dessa är sätt och förökningar som inte är tropiska: Scatter meteor, aurora och sporadisk, F2.
4. Airscatter. Om kommentaren innehåller <AS> eller <ACS> (även blandad, t.ex. <As>) eller förklarar platsen som airscatter (airscatter, air scatter, flygplan scatter), återstår platsen men dras i gult med prickad linje (mörk slip för att hålla sig läsbar på klar karta) och markeras i listan.
5. Plats för stationer. Om det i kommentaren finns två lokaler (t.ex. JN54 < JN83) är den första av dem som gör platsen och den andra av stationen DX; med en locator, det är från DX-stationen. Det som saknas är sökt i C ALL3. TXT, endast med exakt namn (för bärbara datorer se nedan). Om en station förblir utan position visas platsen i listan med noten "okänt läge" men inte på kartan.
6. Dubblar. En kommersiell med samma DX, samma spotter, samma band och samma källa till en redan närvarande ersätter det, istället för att lägga till.

C ALL3.TXT är namnet databas → locator som används av JTDX, i formatet CALL,LOCATORE,. Projektfilen innehåller 1 048 847 namn, alla med giltig locator; filens rubrik talar fortfarande om 34 323 nomineringar eftersom den förblev den första upplagan [7]. Den laddas i bakgrunden vid start, så fönstret är omedelbart användbart. Filen söks bredvid programmet, i mappen Data, i mappen från vilken programmet startades och en eller två mappar ovan; Om det inte finns, ett fönster berättar det vid start och statusfältet signalerar det i rött. Namnet använder alltid sin 6-karaktär. Endast regel för lokaliseringarna: om den som fick (punkt, MQTT, avkodning, QSO desunti) har mindre än 6 tecken och C ALL3. TXT har en till 6, det är alltid värt C ALL3.TXT; för bärbara datorer (F / IC8TEM, IC8TEM/P) är bara en exakt röst: utan, stationen förblir utan hyresvärd och är inte utformad någon annanstans. Regeln gäller också vid tidpunkten för ritningen, så data som anlände innan databasen laddades kommer att köra sig själv.

En databas: C ALL3.TXT. Från 1.3.4 alla lokaliseringar finns i en fil, C ALL3.TXT, som förblir i formatet JTDX och WSJT-X (CALL,LOCATORE,,,,), och kan användas som det är också i WSJT-X, JTDX och MSHV. Det finns inga fler filer ifrån varandra. På toppen av filen, efter rubriken, finns det två kvarter markerade med en kommentar linje: // [LIVETROPOVHF-MANUALI] med poster som införts för hand och // [LIVETROPOVHF-CLUSTER] med korrigering från klustret; sedan // [LIVETROPOVHF-FONTI] med alla andra objekt i alfabetisk ordning. Varje nominering visas en gång. I de sublingual Data and Sources › Utnämningar (C ALL3) letar du efter ett namn, du skriver hyresvärden och du trycker på Lägg till / förändring: rösten går i blocket av rösterna för hand och det är värt framför allt, även över lokaliseringen som stationen själv skickar. □ Ta bort nomineringen från C ALL3.TXT (nästföljande uppdatering återupptar den från källor, om de har den). Lägg till en fil... läs en lista CALL, LOCATOR (du accepterar också punkten och komma, tabulation och utrymme) och skriv den bland handplockade; den valda filen kopieras inte. Reload reread C ALL3.TXT från disken, efter redigering med en redaktör. Fullständig ordning av källor: egen locator, handröster, klusterkorrigeringar, resten av C ALL3. TXT (källor, lärda live-lokaler och G7VRD), fick locator (vilket gäller endast i databasen om den har 6 tecken och inte slutar med LL eller MM).

Uppdatering av C ALL3.TXT. I samma underspråk laddar uppdateringsknappen nu ner källorna (C ALL3.TXT av WSJT-X Förbättrad, C ALL3.TXT EME av W7GJ, grid_data.bin av JTDX), går med dem (den första som har en nomineringsvinst), kontrollera att de är minst 50 000 namn ( annars ingenting ändras), sparar C ALL3. TXT Den tidigare C ALL3. TXT poster kvar, och en 6-karaktär locator kvarstår där en källa har bara 4 av samma torg. Handröster och korrigeringar från klustret finns kvar på toppen, oförändrade. Med uppdateringsrutan på egen hand varje dag sker checken i början. Programmet skriver om C ALL3. TXT omedelbart efter varje röst för hand och, för lärda lokaler, högst var 10: e minut och stängning; C ALL3. TXT av paketet är lättläst och attributet tar bort sig själv. Linux installatörer överskriver inte en C ALL3. TXT finns redan; det för Windows installerar det bara om det saknas. ALL3

Automatiska korrigeringar från klustret. I DM4X och RBN automatiska fläckar kommentaren ger två lokaler, till exempel KN09tp <> KN09: den första är vad stationen du ser förklarar av sig själv, till 6 tecken, den andra är att stationen kände, ofta bara vid 4. Om den första har 6 tecken (och inte slutar med LL eller MM) och skiljer sig från databasen, korrigerar programmet C ALL3. TXT ensam: korrigeringen går i blocket // [LIVETROPOVHF-CLUSTER], med datumet för den sista bekräftelsen i det tredje fältet, är det giltigt ovanför källorna (under handrösterna) och visas mellan misstänkta signaler uppdaterade som "C ALL3 ". Den överlever C ALL3. TXT-uppdateringar. En obekräftad korrigering i två år förlorar företräde och förblir som en normal röst.

Sista källan: G7VRD. Om en kandidat inte har en 6-karaktär i C ALL3. TXT, programmet frågar det i bakgrunden till den offentliga API av G7VRD (api.g7vrd.co.uk, "locator by callsign"), som samlar lokaliseringar automatiskt från nätverkstrafiken. Du accepterar bara en 6-karaktär som inte är mitten av torget (LL eller MM); förfrågningarna är grupperade, högst 40 nominativ per minut, och varje nominativ frågar en gång om dagen, eftersom det är en hobbistisk tjänst utan garantier. Platsen hittades in i C ALL3. TXT om namnet inte finns eller bara har 4 tecken på samma torg; sökningen stängs av med rutan i namnfliken.

Besök med baren. I PSKReporter MQTT-ämnen separerar baren nivåerna, sedan kommer föreningsnamnen med en punkt i stället för baren ( IV3. HB9CAT). Programmet sätter ribban tillbaka och om meddelandet innehåller fullständiga namn, använd dem: IV3/HB9CAT förblir IV3/HB9CAT på karta, lista och databas.

Platser lärde sig live. C ALL3. TXT innehåller inte allt: till exempel SP9EMF och SP9NVY finns inte där, och förblir sedan den 4-karaktärslokal som skickas av PSKReporter (kortet säger så). För att minska dessa fall skriver programmet i C ALL3. TXT varje 6-karaktär som kommer från någon källa (PSKReporter, såsom sändare eller som mottagare, kluster, UDP avkodning) för en nominering som C ALL3. TXT har inte eller har bara 4 tecken på samma torg; ändrar aldrig en röst till 6 tecken. Fram till 1.3.3 var dessa lokaler i Data/ LOC6.TXT.

Övergång från 1.3.3. Vid den första starten av 1.3.4 filerna bortsett från de tidigare versionerna (Dati/C ALL3 -MANUALE.TXT, Data/C ALL3 -AGGIUNTE, Data/C ALL3 -CLUSTER. TXT, Data/ LOC6.TXT och Data/C ALL3 - G7VRD.TXT) slås samman till C ALL3. TXT Releasing-skrivning, gamla filer tar.fuso-förlängningen, du läser inte längre och du kan ta bort.

Automatiska mottagare. RBN, dm4x och PSKReporter rapporterar ofta namn med ett suffix som anger mottagaren: -SDR, /SDR, -# (skimmer RBN) eller -1, -2... (plus mottagare av samma station). För hyresvärden tas suffixet bort: OK2OZL -SDR tar OK2OZL JN89UE från C ALL3.TXT om stationen inte redan skickar en 6-karaktär. Namnet som visas förblir det med suffixet.

Locators cut. Inte alla källor behandla förlängningen av sin hyresvärd: många sätter den till 4 tecken, och vissa automatiska överföringssystem medvetet trunkerar 4-karaktärslokalatorn, även de som får data från den verkliga lyssnandet av FT8, FT4, Q65 och JT65, eller slutföra den med mitten av torget (LL eller MM, t.ex. JN89MM). Av denna anledning ersätts en 4-karaktär eller 6-karaktär som slutar med LL eller MM, med C ALL3. TXT av samma torg. Om rätt läge och mitten av torget inte sammanfaller med några dussin km, är felet fortfarande litet jämfört med tropoavstånden.

## 6. Vem lyssnar

Varje MQTT-meddelande från PSKReporter rapporterar namnet och locatorn för dem som tar emot [5], så du behöver inte leta efter det i C ALL3.TXT. För det band som valts i väljaren håller programmet, för varje mottagningsstation: hyresvärd, sista mottagning, avkodade sätt, antal stationer kände och maximalt avstånd. En station lämnar listan när den inte längre får något i det valda intervallet (3, 5, 10, 15, 30 eller 60 minuter).

På kartan är varje mottagare en mörk rosa tom cirkel, och från varje start full rosa linjer mot stationerna som känner och skickar tillbaka till nätet. Linjerna ses alltid utan att klicka. Utnämningar av lyssningsstationer, hörselstationer och de två huvuden på varje plats skrivs alltid på kartan, utan att behöva klicka (samma namn skrivs en gång). Genom att klicka på en cirkel blir dess linjer ännu tjockare och öppnar fliken med detaljer. Om locatorn från MQTT har mindre än 6 tecken använder programmet den av C ALL3.TXT (cirka en miljon namn), förutsatt att den är förenlig med det mottagna torget.

Scatter meteor lägen. Som med klusterfläckar, MQTT-förhållanden i MSK144, FSK441, JT6M, JTMS eller ISCAT kasseras och räknas endast; samma gäller för UDP-avkodningar på dessa sätt.

Inte äkta lyssnare. Vissa stationer deklarerar att ta emot i 144 MHz men i verkligheten är mottagaren på ett annat band (fel frekvensindikation): deras rapporter skulle utforma obefintliga öppningar på tusentals km. Programmet kontrollerar det själv. Varje station som en lyssnare säger att höra i VHF följs i 30 minuter på alla band, med en dedikerad MQTT-inskription (pskr/filter/v2/+/{nominativ}/#) [5] fungerar som bevis. Om minst två av de stationer som hörs av en lyssnare, på samma minuter (±10), aktiva på andra band, betraktas lyssnaren inte som äkta och alla VHF-rapporter kasseras; MQTT-registret rapporterar namn och stationer som förrådde honom. I listan över fläckar, bredvid mätarna, MQTT-knappen visar flödets tillstånd (grönt blad om det är anslutet, rött om inte) och hur många lyssnare kasserades i bandet och intervallet valt; genom att klicka på det öppnar fliken med listan över kassor och, för varje, stationer som finns på andra band.

Lång anslutning china. En anslutning över 1000 km (Clumbing Spot, MQTT Listening, QSO Desunti) visas inte omedelbart: hålls kvar och räknaren "hålls > 1000 km" säger numret. Det avslöjas när en annan anslutning, mellan olika stationer och minst hälften tillsammans, bekräftar det med ett av dessa kriterier: samma station i huvudet (till exempel samma lyssnare) och den andra stationen i samma riktning inom 10°; liknande korridor, med båda huvuden inom 200 km från de andra anslutningen; parallellrutt, med riktning inom 10° och sidoavstånd inom 200 km, överlappade längs rutten. Båda dyker upp och MQTT-loggen visar anledningen. Dess UDP-avkodning är alltid synliga och gäller som bekräftelse för andra. De behållna anslutningarna förblir i minnet i 60 minuter.

Inget under 144 MHz. Programmet fungerar från 144 MHz upp: allt under 143 MHz kasseras. Undantaget är GRAVES, den franska radarn som överförs i CW på 143 050 MHz av Broye-lès-Pesmes ( JN27SI): en kommersiell mellan 143 040 och 143 060 MHz, eller med GRAVES i namnet, visas mellan 144 MHz med radar som sändare. Annonskortet säger om mottagaren är inne i strålen (sektorerna D, C, B, A från 90° till 270° av azimut sett från radarn, 6 steg från 7,5° vardera) eller ut; fliken GRAVES, som i YA-MS, visar himlen sedd från radarn: sektorer, steg upplyst vid den tiden (sekvens PE1ITR 2021: 0,8 s per steg, cykel 4,8 s Data och geometri kommer från YA-MS. Att känna det i tropen är svårt, men det kan hända.

Rehabilitering från mänskliga kluster. En station som också visas på andra band räknas inte som misstänkt om, i samma minuter (±30), DX mänskliga kluster ( ON0DXK, GB7RAU; inte dm4x, RBN eller andra automatiska källor) den aktiva skadan på bandet i fråga, i fläckar gjorda av andra namn: det är fallet med stationer som verkligen arbetar på flera band tillsammans. Således returnerar en utkastad lyssnare synlig (MQTT-registret skriver "rehabiliterad") och en långvarig länk för låga band kan avslöjas.

Aktiva stationer under 144 MHz. I karantän av långa anslutningar bekräftar en station som under samma minuter hörs på band under 144 MHz (från MQTT-rapporterna från de andra banden) inte andra anslutningar och inte avslöjas, även om en liknande eller parallell väg skulle bekräfta det.

## 7. Lista över reklam

Den upptar 35% rätt i fliken Karta och plats. Top: titel, bandväljare (50, 70, 144, 222, 432, 1296, 2320 MHz, 10 GHz), range selector (sista 3, 5, 10, 15, 30, 60 minuter) och Update key. Nedan, fyra meter: synliga fläckar, AS / ACS, kasseras från filtret, stationer som lyssnar.

Varje rad visar typen av linje, tiden UTC, DX → DE, vägen, avståndet i km och, på andra raden, källan, frekvensen och kommentaren. Den senaste är på toppen. Ett klick på linjen väljer platsen: på kartan växer linjen och de två namnen visas.

## 8. Spot kort och troposcatter förlust

För den valda platsen visar fliken lokaliseringar, avstånd, azimut, frekvens, läge, källa, nu, kommentar, det genomsnittliga tropoindexet längs vägen och en uppskattning av ruttförlusten för spridd tropo.

Avstånd och azimut beräknas på den maximala cirkeln med jordradien på 6371 km (formel av emisenoverso för avståndet, initial azimut från DX-stationen till spotter).

Loss troposcatter. Den grundläggande förlusten, för två stationer på slät mark, är summan av förlusten av ledigt utrymme och diffusionsförlust: G3YGF [2]

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

Förlusterna är i dB, D är avståndet i km, f frekvensen i MHz, θs diffusionsvinkeln i grader.

8497 km är jordens motsvarande radie (4/3 av den fysiska). θA och θB beror på varje stations horisont:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

med Δh höjden av det hinder som gör från horisont avseende stationen och dess avstånd. Varje nivå av horisont kostar ca 10 dB [2]. Programmet känner inte till stationernas horisont, så det visar den grundläggande förlusten med θA = θB = 0 och förklarar den ("smooth ground"). G3YGF lägger till en klimatterm där N är reaktiviteten på [2] -jorden; den kommer att användas när N är tillgänglig från vädermodellen:

```math
L_{N} = 0,2 \cdot (N - 310)
```

Verifiering: för 110 km till 144 MHz ger formeln 166,5 dB, mot cirka 168 dB-sängar på artikelns diagram. För 500 km till 144 MHz ger ca 206 dB. Den förväntade signalen / ljudkvoten är systemets kapacitet (eirp + effektiv känslighet för mottagaren) minus förlusten: med G3YGF 144 MHz modellsystem (198 dB) en 500 km väg skulle ge cirka −8 dB i SSB [2], det vill säga anslutningen är endast med bättre förökning av den rena scatter.

## 9. Mina avkodningar: UDP från WSJT-X, JTDX och MSHV

WSJT-X, JTDX och MSHV kan skicka UDP bort varje rad som de avkodar. Programmet lyssnar på 127.0.0.1:2237 och upprepar samma paket, byte per byte, på 127.0.0.1:2238, så de andra programmen som använde porten (GridTracker, JTAlert, en logger) fortsätter att fungera om du flyttar dem på 2238. Adresser och dörrar ändras i UDP-underspråket för datainsamlingen; som en lyssningsadress kan du också använda en multicastgrupp (t.ex. 224.0.0.1), om avkodningsprogrammet anges så här.

Paket följer WSJT-X-nätverksprotokollet: ett första magiskt nummer 0xADBCCBDA, schemanummer och meddelandetyp. Programmet använder främst tre [8] -typer:

- Heartbeat säger vilket program som överför data och med vilken version.
- Status: frekvens av ratt och läge; från frekvensen får du bandet (med samma tabell på fläckarna).
- Avkodning: Nu, SNR, tid och frekvensavfall, sätt och text avkodade meddelanden.

Från texten får du den station du hörde: i CQ 9 A2PQR JN83 är stationen efter CQ (spara DX, EU, TEST...), i IK0JKL IT9MNO JM68 är den andra, dvs. som överför. Lokalatorn är meddelandets, om det finns (4 tecken, annorlunda än RR73); annars söker du i C ALL3. TXT. De lyssnar linjer: stationer som din mottagare hör nu. På kartan blir de röda linjer från din QTH till stationen, med namnet; lagret lyser upp med den avkodade rutan från mig och kräver en giltig locator. I listan till höger berättar en räknare hur många stationer du avkodar på det valda bandet.

QSO desunti dal lever. När en avkodning är ett stängningsmeddelande i form C ALL1 C ALL2 RR73 (eller 73 eller RRR), till exempel IC8TEM IK3FHP RR73 eller IK3FKP SV8PEX 73, har de två stationerna slutfört en länk. Programmet registrerar det som QSO, med lokaliseringen av varje tas från det sista meddelandet där han förklarade det eller, om det inte finns något, från C ALL3.TXT [7], och drar den på kartan som en grön linje mellan de två stationerna (strate "QSO desunti dal live"). Ett par stationer räknas en gång per band; bandet och utbudet som valts i listan är giltiga.

I samma underspråk visar Debug-rutan texten för varje mottaget paket (typ, tid, värderingar och meddelande), på begäran även hexadecimal byte och en linje för vidarebefordran. Det är att kontrollera att avkodningsprogrammet faktiskt skickar data.

## 9 bis. Språk Hepburn och F5LEN

Varje visar vad programmet tar från källan. Knappen "Läs sidan och ladda ner alla bilder" öppnar den indikerade sidan (bas dxinfocentre.com/tropo_eur.html för Hepburn [3] och tropo.f5len.org/forecasts-for-europe för F5LEN [4]) och hämtar alla bilder i cache bredvid körbar; du kan också klistra in adressen på en bild. För varje bild finns en låda för att utesluta den och ett fält med prognostiden. De 31 europeiska bilderna av F5LEN namnges av tiden för modellloppet (03.png... 48.png var tredje timme, 54... 96 var 6: e timme, 108... 192 var 12: e timme) och programmet tar tiden från namnet på [4] -filen; för Hepburn är ordningen på sidan. I fliken F5LEN väljer en meny regionen: Europa (tropo.f5len.org/forecasts-for-europe), Australien (tropo.f5len.org/Australia, vk03.png... vk192.png) eller Atlantic mellan Cape Verde och Irland (tropo.f5len.org/atlantic-ocean/atlantic-d4-ei, atl03.png... atl192.png). Alla har samma steg.

Nedan är kalibreringsräknaren. Ett klick på bilden lägger till en kontrollpunkt (locator eller lat,lon: det tar minst 6) eller en färg på legenden med sitt värde. Programmet beräknar pixelmatchningen → koordinater med en andra gradens polynominterpolering till minsta rutor [19], läser färgerna med toleransen som valts och, för F5LEN, omvandlar enheterna N i skalan 0-10 med en · N + b. "Spara kalibrering och testläsning" skriver filen i Tarature-mappen och kontrollerar vilken pixel din locator faller. Innan du ställer in en kalibrering bör tillstånd begäras till författaren av kartorna.

Regioner. Varje källa har fler regioner, var och en med sina fasta namnbilder: Hepburn tr_map/fcst/{code|ore}.png?v{data} (eur Europe, nwe North-Western Europe, eeu Eastern Europe, ent North-East Atlantic, nat North Atlantico, eam North America East, nsa South America North America, sam South America, mid-East Asia, nca Central-Septential Asia Fliken för varje källa visar först och främst förhandsgranskning av fusionen: alla nedladdade bilder som sattes ihop som på huvudkartan, med den prickade rutan och namnet på varje region, en grå slöja där ingen bild täcker området och en reglage för att bläddra timmarna; till vänster listan över regioner, var och en med rutan för att använda den, antalet kartor läser och varifrån extremerna kommer (OCR, för hand eller fabrik; om OCR misslyckas med texten han läser visas). Den manuella kalibreringsbänken ligger kvar bakom den homonyma knappen. Menyn Bench Region väljer vilket utseende rutan "Använd denna region på kartan" innehåller den i indexet. Ramen återfinns ensam (två horisontella linjer och två vertikala linjer av samma färg); att placera bilden tjänar extremerna lat/lon i ramen: för Hepburn Europa (10° W-40° E, 27°-56° N) och F5LEN Europa (15° W-40° E, 30°-65° N) finns redan i programmet, för de andra regionerna som de skriver i de fyra O E S N-fälten som läser axeletiketterna. Skalan multiplikator gör de två källorna jämförbara: slutvärde = multiplikator × legend värde. [3] F5LEN [4] F5LEN

## 9 ter. Export till Word

De två dokumenten har på toppen av knappen "Export to Word". Filen är skriven i Office Open XML-formatet; formlerna blir Word-ekvationer (OMML), så de är redigerbara med [16] -ekvationsredigeraren. Om frälsfönstret inte är tillgängligt slutar filen i mappen Dokument bredvid det körbara.

## 9 quater. Vem ringer mig

Om ett meddelande skickas till dig på den lokala UDP (förnamnet är ditt, till exempel IC8FAX IK3FHP JN55 eller IC8FAX IK3FHP -12), spårar programmet från din QTH till stationen som kallar dig en svart prickad och blinkande linje, med namnet och locatorn bredvid dig. Locatorn kommer från C ALL3.TXT (inklusive handröster) eller meddelande. Linjen kvarstår tills UDP passerar nedläggningen av QSO med den stationen ( RR73, RR eller 73, i en av de två riktningarna), högst 3 minuter från sista samtalet; du drar över allt och inte följer uteslutningskriterier, så ett direkt samtal flyr aldrig.

PSKReporter, via MQTT, överför inte texten i meddelandena: säger bara vem som fick vem, med frekvens, sätt och SNR. Det är därför ett direktsamtal endast erkänns av sina egna UDP-avkodningar.

## 10. Tidskontroll (NTP)

Digitala lägen och spottid kräver en exakt klocka. Var tredje minut programmet förhör 10 NTP-servrar (SNTP på UDP 123): 0.pool.ntp.org, 1.pool.ntp.org, 2.pool.ntp.org, 3.pool.ntp.org, it.pool.ntp.org, ntp1.inrim.it, ntp2.inrim.it, ptbtime1.ptb.de, time.google.com, time.cloudflare.com [11].

För varje svar beräknar kassen och returfördröjningen, kastar de onormala värdena och tar medianen, med beräkning av kass och fördröjning som tillhandahålls av SNTP [10]. Med minst 3 svar godkänner du UTC-klockan av programmet anpassar sig och nästa visas NTP:OK i grönt; om svaren inte räcker med NTP:FAILED i rött och klockan förblir datorns. Systemklockan är inte rörd, eftersom administratörsbehörigheter skulle behövas; alla interna programscheman (punkter, 5-60 minuter fönster, avkodningar) använder den anpassade tiden. Kontrollen vänder alltid i bakgrunden: i gränssnittet ser du bara resultatet bredvid klockan; servrar, intervaller och minsta svar ändras i LIVETROPOVHF.ini (ntp_servers, ntp_minutes, ntp_min).

## 10 bis. Allmänna undantagskriterier

Två lådor i lagerfältet gäller allt du ritar (punkt, PSKReporter, avkodning, QSO desunti):

- Undvik under 100 km. Trafik mellan stationer mindre än 100 km kan inte vara betydande för tropo (markvåg, optiskt intervall): med den på-box du inte ritar och visas inte i listan. Början är av.
- Mer än 1000 km: bara rutor. Runt din QTH (locatorn som startar) finns en krona på 1000 km i alla riktningar. Om bara en av de två stationerna i en länk ligger utanför denna krona, är anslutningen inte dras som en linje: i dess ställe färgar de i lila, prickade på 45°, locator rutor (4 tecken) av stationerna som är inblandade. Detsamma gäller för PSKReporter-stationer som lyssnar på över 1000 km, som framstår som torg istället för som cirkel. Hittills ser du trafik utan att fylla kartan över linjer. Utan en giltig locator räknar längden på anslutningen. Torget i din QTH färgar aldrig i lila, även när du lyssnar på en station utanför kronan: tillämpar den fasta hyresvärden, alla namn använder som fungerar från stationen. Kronradien väljs bredvid lådan: 1000, 700 eller 500 km. Boxen Corone 1000/700/500 km drar de tre kronorna runt QTH, som dras i lila, med det mest markerade valet och avståndet skrivet norrut. I listan över fläckar följs av.. Början är på. Kriteriet gäller efter karantän: långa anslutningar kvarstår inte ens visas som torg.

Vad är de lila rutorna täckta vid 45°. Trafiken bort från din QTH intresserar dig att förstå var förökningen är öppen, men dess linjer, hundratals kilometer långa och ofta tiotals, täcker kartan och döljer vad som räknas för dig: öppningarna du kan utnyttja från din station. Av denna anledning, bortom den valda kronan, ritar programmet inte linjerna utan färgar locatorrutorna (2 ° × 1 °) av de berörda stationerna. Resultatet är en karta över "aktiva zoner": där många närliggande lila rutor visas, finns det en ledtråd eller öppning, även om det inte rör dig direkt; Om området breddar mot dig, är det signalen att peka antennen i den riktningen. 45° instrumentbrädan låter dig se under tropoindexet och rutnätet, så de jämför med en blick prognosen (bottomfärg) och den verkliga trafiken (lacken). Din QTH kvadrat färgar aldrig, eftersom det är utgångspunkten och inte ett avlägset område. Endast de rutor som är alla bortom kronan är färgade: en kvadrat som korsas av kronan, eller inuti, förblir fri även om det finns en av de två stationerna. För att se linjerna, stäng av rutan eller välj en större krona.

## 11. Teman och språk

Teman: de 18 gemensamma för IC8TEM -programmen, samma som YA-NEC och E-LAYER ANALYZER: Clear, Night, Black, åtta pasteller (blå, orange, akvamarin, grön, rosa, lila, indigo, brun), Scia meteorica (YA-MS palett), de tre av IP-TRX-CTRL (Chiaro, Notte, Nero) och Blue, Purple, Green. Från 1.3.4 ser Chiaro ut som YA-NEC: ljusgrå bakgrund, gränsade vita paneler, blå nattflikar och blå accent. I mörka teman blir också baskartan mörk. Forecastens färgskala är densamma i alla teman. På smala skärmar (under 1500 pixlar, till exempel 1366×768) visar klockan på toppen bara timmen, så temat, markör för texter och språk är fortfarande synliga. Språk: YA-MS [17] wiki (engelska, resor, Deutsch, Nederlands, Français, Español, Português, Dansk, Norsk, Svenska, Íslenska, Eesti, Lietuvi, Latviešu,,,, Türkçe, med arabiska, hebreiska och Urdu speglar hela fönstret från höger till vänster; kartan förblir som [12]. Översättningar rör gränssnittet; de två tekniska dokumenten och meddelandena i statusfältet finns kvar på italienska. Dagarnas namn kommer från de internationella inställningarna. NET för det valda språket.

Tema och språkbyte från de två menyerna längst upp till höger, bredvid klockan, och tillämpa omedelbart, utan att starta om programmet och utan att avbryta insamlingen av data. Valet sparas i LIVETROPOVHF.ini.

## 11 bis. Misstänkta och självlärande rapporter

Fliken Misstänkta rapporter samlar allt som programmet kontrollerar finner konstigt, grupperat efter typ och namn, med de senaste 24 timmarna listade. Programmet lär sig själv: för varje par (typ, namn) räknar de positiva resultaten (+) och motsatsen (−), och efter 3 överenskomna resultat skapar en automatisk regel; ett motsatt resultat sätter den i fråga. Användarbeslut (God, dålig) gäller alltid; pulsante-knappen returnerar beslutet till programmet. Reglerna finns i Data/APPRENDIMENTO. TXT, bredvid programmet, de automatiska som inte förstärks för ett år glöms bort.

| Typ | När när | Exit + / − | Regeleffekt |
| EME ( Q65/JT65) | Q65 eller JT65 över 1000 km med månen ovanför horisonten till båda huvuden, eller EME i kommentar | Varje gång månen är vanlig | Discard: EME station, dess långa Q65/JT65 s är också uteslutna utan vanlig måne. Bra: behandlas som en tropo |
| Lång anslutning | Över 1000 km, i karantän | • Avtäckt, utgått utan bekräftelse | Bra: Stationens längder visar omedelbart. Discard: Aldrig |
| Lyssna | PSKReporter mottagare som hör aktiva stationer på andra band | − kasserad, + rehabiliterad av mänskliga kluster | Bra: aldrig kasserad. Discard: alltid |
| Olika hyresvärd | Square fick annorlunda än C ALL3. TXT | Varje gång du upprepar det | Bra: stationen har flyttat, det är värt det mottagna torget. Discard: Val C ALL3.TXT |
| Square Center | 6-karaktär med LL eller MM |  —  | Information endast |
| Automatisk mottagare | Utnämning med -SDR, /SDR, -# eller -n |  —  | Information endast |

EME. Q65 och JT65 är kommunikationsvägarna via Luna: I teorin faller de i exkluderingsklausulen som scatter meteor, men de används också i tropo. Programmet beräknar månens position med lågprecisionsformler av Almanac Astronomical (cirka 0,3 °) [20]: Om en anslutning i Q65 eller JT65 överstiger 1000 km och månen är över horisonten till båda huvuden, är det nästan säkert EME och exkluderas (det räknas bland de uteslutna). Om månen inte är vanlig, förblir anslutningen möjlig i tropen och passerar genom karantän långa anslutningar, men signalen förblir synlig. Det gäller för kluster, för MQTT och dess egna UDP-avkodningar.

## 11 ter. Kontaktkasseringskriterier, en efter en

Programmet vill bara visa den tropoferiska förökningen i 144 MHz och bortom. Varje inkommande data (Cluster Row, PSKReporter Report via MQTT, UDP-avkodning) passerar genom en serie filter, alltid i samma ordning. En kasserad data försvinner inte i ingenting: räknas (kontakt av det uteslutna i övre baren) och, när orsaken är tveksam, visas i fliken Suspended Reports.

|  N.  | Kriterier | Var är det? | Vad som förkastas | Kan du ändra? |
|  1  | Band | Kluster, MQTT, UDP | Alla under 143 MHz (även 50 och 70 MHz), utom GRAVES 143 050 MHz. Band som hålls är 144, 222, 432, 1296, 2320 MHz och 10 GHz | Nej. Nej. |
|  2  | Satellit QO-100 | Kluster, MQTT, UDP | 2400,0–2400,5 MHz och 10489,5–10490 MHz: Det är geostationär satellittrafik, inte tropo | Nej. Nej. |
|  3  | Meteor Scatter | Kluster, MQTT, UDP | JT6M och ISCAT (även ISCAT-A/B), kommenterar med "&" och icke-tropoxy propagation etiketter (<AU>, <AUE>, <ES>, <FAI>, < F2 >) alltid. MSK144, FSK441 och JTMS (även med <MS>) endast över 250 km: inom 250 km vi accepterar, eftersom dessa lägen också används i tropopopopo | Nej. Nej. |
|  4  | EME (via Luna) | Kluster, MQTT, UDP | EME skriven i kommentaren; Q65 eller JT65 över 1000 km när månen är över horisonten till båda huvuden; stationer lärde sig som EME | Ja: Bra regel i misstänkta signaler |
|  5  | Icke-äkta lyssnare | MQ | PSKReporter mottagare som säger att höra på 144 stationer aktiva vid den tiden på andra band (minst två): Hans relationer kasseras. Tillbaka bra om DX mänskliga kluster bekräftar samma stationer | Ja: Bra / dåligt |
|  6  | Kvarantin av långa anslutningar | Alla linjer | Över 1000 km förblir en länk dold tills den finner en bekräftelse: samma station i samma riktning, en annan rutt i en korridor på 200 km eller parallellrutt inom 10° och 200 km längs minst hälften. Utan bekräftelse löper ut efter 60 minuter | Ja: Bra / Av till station |
|  7  | Under 100 km | rader och list | Med on-box, de kortaste anslutningarna av 100 km (markvåg, optiskt intervall) | Box, av vid start |
|  8  | Bortom kronan från QTH | linjer | Om en station är över 1000, 700 eller 500 km (på ditt val) från din QTH, ingen linje: bara de små rutorna som täcks av de två stationerna. Din QTH-torget färgar aldrig | Box och radie, på 1000 km |
|  9  | Tid fönster | Allt allt | Äldre platser i det valda intervallet (från 3 till 60 minuter) | Menyn överst |
|  10  | Dubbel | Kluster | Samma plats (samma DX, samma spotter, samma band, samma källa) upprepade | Nej. Nej. |

Notera kriterierna 4 och 6. En anslutning i Q65 eller JT65 över 1000 km utan Common Moon är möjlig i tropo: kasseras inte av kriterium 4, men passerar för karantän av kriterium 6 som någon annan lång länk. Direkta samtal till dig (avsnitt 9 quater) är det enda undantaget till kriterierna 7 och 8: de drar alltid.

Vad är inte ett avfall. Korrigering av locatorn (C ALL3. TXT med handröster, korrigeringar från klustret och lärda locatorer, suffix -SDR, centrum av kvadraten LL / MM) förändring där du ritar en station, inte kassera den. En station utan hyresvärd, å andra sidan, kan inte dras: förblir i listan med avståndet "-".

## 11 quater. Färger och symboler

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

## 12. Inställningar och portabilitet

Fönster. I början upptar fönstret 90% av skärmens användbara utrymme (klipp applikationsfältet), centrerad, så på en 15-16-tums anteckningsbok med en 125% eller 150% resize titelfältet är fortfarande synligt. Om det användbara utrymmet är under 1280 × 720 poäng maximerade fönsterdelen. Storlek, position och maximerad status sparas i LIVETROPOVHF. ini vid stängning och slut vid nästa start, förutsatt att titelfältet fortfarande faller i en skärm (till exempel efter att en extern bildskärm); Annars återvänder du till 90%. Minimistorleken är 900 × 560.

Programmet är bärbart: skriver aldrig i mappen AppData eller i registret. Allt är i mappen av den körbara: LIVETROPOVHF.ini (uppsättningar, skapad vid första stängningen), C ALL3.TXT (med handskrivna röster, lärde sig korrigeringar och locatorer), kartcache och väderdata, regler som lärts (Dati/APPRENDIMENTO.TXT) och LIVETROPOVHF.db arkiv. Du kan också hålla den på en USB-minne, så länge mappen är skrivbar.

Ad arkiv (SQLite [14]). Varje spot som accepteras och varje PSKReporter-lyssningsrapport sparas i LIVETROPOVHF.db, en SQLite-databas i en fil. I minnet finns bara de senaste 60 minuterna kvar för listan och kartan; arkivet behåller allt och tjänar till historikern och statistiken (även UDP-avkodningarna), till exempel öppningarna för band och per dag, de maximala avstånden, de rutor som nåtts och den bakre jämförelsen mellan det förväntade indexet och de verkliga platserna. Linjerna som kasseras från filtret räknas men inte arkiv.

## 12 bis. Automatisk installation och uppdateringar

Från version 1.0 programmet redan nedladdningar redo från Release sida av github.com/ic8tem/LIVETROPOVHF, i samma paket av YA-MS: installatör för Windows (64 bit, 32 bit och ARM, installerar i C: \LIVETROPOVHF utan administratörsrättigheter) och bärbar zip; för Windows 7 SP1 och 8.1 paket isär (-windows7-x64 och -wind.deb,. tjärpm, Arch, Slackware, 32, 32, 32 X0lpmware, 32, 32, 32, 32 X0lpmware, 32. Med Linux-paket kopieras programmet till den första starten i ~/LIVETROPOVHF, så inställningar och data förblir bredvid programmet; på Mac-data föds bredvid LIVETROPOVHF.app.

Var 30: e minut (den första kontrollen några sekunder efter starten, omedelbart om förra gången programmet hade stängt för ett misstag) programmet läser ensam, utan att be om något, den sista utgåvan. Om versionen är nyare ladda ner det bärbara paketet som är lämpligt för systemet och ersätter programfilerna på plats: de som används byta namn *.old-update och raderas vid nästa omstart. Inställningar, arkiv, datamapp och C ALL3. TXT berörs inte. Den nya versionen är aktiv från nästa start: vid installationen avslutas ett fönster föreslår att starta om omedelbart; för brådskande korrigeringar, markerade [obligatoriska] i Release kan omstart inte skjutas upp. Kontrollstatus och versionen i bruk är skriven längst ner i statusfältet; att stänga av uppdateringar = 0 i LIVETROPOVHF.ini. På Mac är appen signerad och ändrar sig inte: programmet rapporterar bara att det finns en ny version.

LIVETROPOVHF-bilden med versionsnumret visas i början; i slutet ber programmet om bekräftelse och tack för användning.

## 12 ter. Fellogg och status bar

Alla oförutsedda fel slutar i Data/errori.log, även med beräkningen logga av: datum och tid, version, system, öppen flik, fullständig fel och sista rader av vad programmet gjorde. Ett gränssnitt fel inte längre stänger programmet: i statusfältet visas "anno annotated error" (klicka på: öppna registret). Vid nästa start frågar programmet om du skickar rapporten: öppnar webbläsaren på en GitHub-signal som redan sammanställts, med nominering, locator och användarnamn på datorn bort, som du kontrollerar och skickar med ett gratis GitHub-konto. Loggen slutar också med varje uppdateringskontroll.

I statusfältet klickas källbollarna (DX-klustret, RBN, PSKReporter, MQTT, UDP) och förs till källan; serien säger varför en källa är avstängd: avvisad port, klusternod på serversidan, inget svar eller UDP-lyssning men inga paket från WSJT-X, JTDX eller MSHV. Ett klick på versionsnumret kontrollerar omedelbart uppdateringar.

## 13. Plattformar

Externa bokhyllor: Avalonia för [12] -gränssnittet, MQTTnet som en MQTT-klient för realtidsströmmen av PSKReporter [13], Microsoft. Data.Sqlite för [14] -arkivet.

C # på.NET 10 (LTS) [15] med Avalonia gränssnitt, publicerad som en oberoende körbar för Windows 64 och 32 bitar, Mac Intel och Apple Silicon, Linux Debian och Raspberry Pi (64 och 32 bitar). Noterade gränser: Windows 10/11; macOS 14 eller senare, med appar för att registrera eller öppna rätt → Öppna; Raspberry från Pi 2 / Zero 2 W upp (ARMv6-modeller stöds inte av.NET).

## Tack tack
Tack vare I5MZY Massimo Zannoni för testning på Windows 7 och Windows 8.

Airtights: ITU-R P.453, digitala produkter från Study Group 3 (© ITU), som används för personliga och icke-kommersiella ändamål.

## Huvudkällor

1. S. Heck LA0BY, ”Extreme Tropo Propagation on 144 MHz and up”, presentation på 41st Nordic VUSHF Meeting, Skjeberg 2019. Troposferiska lägen, checklista för evenemang (Hepburn / F5LEN prognos, högtemperaturprofiler med Windy, DX kluster som bevis på öppning), observation att endast prognoskartor inte räcker för extrema rutter.
2. J. N. Gannaway G3YGF, ”Troposfärisk Scatter Propagation”, QST November 1983 (från den eponymiska artikeln om radiokommunikation, RSGB, augusti 1981). Formler av fri rymdförlust och diffusion, motsvarande radie 8497 km, platsterm på 10 dB per horisont, klimatterm 0,2 · (N − 310), växtkapacitet och signal/buller, till exempel vid 144 MHz.
3. W. R. Hepburn, DX Info Centre, "Tropospheric Ducting Forecast" (dxinfocentre.com/tropo.html) och "The Hepburn Tropo Index (HTI)". Index skala och värde mening, förutsägelse på 6 dagar.
4. F5LEN, prognos för Europa (tropo.f5len.org/forecasts-for-European: lista över bilder i skriften, klasslegenden Disturb... Duct in the image) och världsprognos (tropo.f5len.org/WW) och notera ”New version of the world VHF tropospheric propagation forecast tool”. Index baserat på reaktivitet i enhet N, 40 steg från 3 timmar, uppdatera 4 gånger om dagen.
5. mqtt.pskreporter.info, distribution MQTT i realtid av reklam för pskreporter.info (curated by M0LTE, flow of P. Gladstone som används med tillstånd). Dörrar 1883-1886, pskr/filter/v2 ämnesprogram.
6. Projektsammanfattning av 8/11/2026 och Go-test (core.go, gui.go) som tillhandahålls av programmets författare. Ad Rules, negativt filter, bandbreddssegment, telnet cluster management, teman, språk och portabilitet.
7. C ALL3.TXT, nomineringsdatabas → JTDX hyresvärd (I. UA3DJY, A. ES1JA script, JTAlert/hamspots.net data). Filformat och räkna med namnen verifierade på projektfilen.
8. WSJT-X, källkod, NetworkMessage.hpp-fil (beskrivning av UDP-protokoll: Heartbeat, Status, Decode). Allmän referens, inte bifogad; JTDX och MSHV använder samma format.
9. Omvänd Beacon Network (reversebeacon.net), servicesidor för telnet-noden. Portar 7000 (CW, RTTY) och 7001 ( FT8), logga in med namnet endast.
10. D. Mills, RFC 4330 ”Simple Network Time Protocol (SNTP) Version 4”, IETF 2006. Paketformat, avfallsberäkning och returfördröjning. Allmän referens, inte bifogad.
11. NTP Pool Project (ntppool.org); INRIM, NTP tidstjänst (ntp1/ntp2.inrim.it); PTB (ptbtime1.ptb.de). Servrar som används för tidskontroll. Allmän referens, inte bifogad.
12. Avalonia UI, officiell dokumentation (docs.avaloniaui.net). Multiplatform gränssnitt, teman och layout från höger till vänster. Allmän referens, inte bifogad.
13. MQTTnet,.NET bibliotek för MQTT (github.com/dotnet/MQTTnet). Kund som används för PSKReporter-flödet. Allmän referens, inte bifogad.
14. SQLite (sqlite.org) och Microsoft. Data.Sqlite (learn.microsoft.com). Arkiv i en fil. Allmän referens, inte bifogad.
15. Microsoft,.NET 10 – stödda operativsystem och autonom publicering i en fil (learn.microsoft.com/dotnet). Plattformar och gränser som anges i avsnitt 13. Allmän referens, inte bifogad.
16. ECMA-376 "Office Open XML File Formats", del 1, avsnitt 22.1 (Office Math Markup Language). Struktur av.docx-filen och ekvationerna. Allmän referens, inte bifogad.
17. Lista över de 28 språken i YA-MS wiki, som tillhandahålls av programmets författare.
18. Open-Meteo, väderprognos API (open-meteo.com), med data från NOAA GFS-modellen. Tryck på havsnivå för isobare (se dokument 2).
19. J. P. Snyder, "Map Projections - A Working Manual", USGS Professional Paper 1395, 1987 och standard polynom transformationsmetoder för georeferencing bilder. Allmän referens, inte bifogad.
20. Astronomiska Almanac, avsnitt D, "Låg precisionsformel för månen" (precisionsposition på cirka 0,3 °). Används för att känna igen EME-anslutningar. Allmän referens, inte bifogad.
