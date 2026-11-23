# Prognosevejledning

> Maskinoversættelse fra italiensk: er du i tvivl, gælder den italienske vejledning.

Fanebladet Prognose beregner den tropiske formering af sig selv: et indeks kort over det synlige område og signalet mellem din station og en tilsvarende. Denne håndbog beskriver alle data, hvordan de beregnes, og hvilken kilde de kommer fra. Tallene i firkantede parenteser refererer til kilderne i bunden; i programmet hver data af resultaterne har en tegneserie (simbolo i) med samme kilde og henvisningen til afsnittet.

## 1. Hvad han gør og hvad han gør

Forudsigelsen af GFS [1, 2] -modellens lodrette temperatur- og fugtighedsprofiler udleder luftlagets reaktivitet efter lag og anslår, hvor meget signalet vinder i forhold til en normal atmosfære. På ruten tilføjer den virkelige jord (Copernicus GLO- 30 [3]), diffraktion, troposcatter, kanaler og modtager støj.

Det er ikke en foranstaltning: det er et skøn lavet på en vejrmodel med fjerne niveauer hundredvis af meter. Det tjener til at sammenligne situationer (i dag bedre end i går, 70 cm bedre end 2 m, dette behandler bedre end det), ikke at garantere en forbindelse. Punkt 14 forklarer, hvordan der skal indsamles dokumentation for at validere den.


> "Forbud kan ikke generaliseres, men undersøges i forhold til lokal morfologi."
> - Volker Grassmann, DF5AI, rapporteret af Flavio Egano, IK3XTV (Formering på 2 meter)

Det er princippet, der styrer pointpunktsberegningen: Hver rute beregnes på sin jord med sin horisont, sit hav og sine relieffer; kortet giver det generelle billede, behandler svaret.

## 2. Sprogkommandoer

- Band: 144, 432 eller 1296 MHz. Kort og beregning bruger det valgte bånd.
- Nu: markøren flytter prognosen fra nu til 72 timer foran, inden for 3 timer.
- Opdateringsprognose (synlig zone): download GFS-profiler på et gitter på højst 240 punkter, der dækker det synlige kort. Gitterpitch (0,5 ° eller mere) afhænger af forstørrelse.
- Stop: Afbryd download eller beregning.
- Detaljer at vise (højre panel af kortet): tre niveauer, Essential, Normal og Complete, tænde en gruppe af kasser; hver boks kan derefter ændre sig i hånden. Kasserne er i tre grupper:
  - På kortet: isolinee del Gain, også 3-7 dB (træk), isobare, termisk inversion, vejrtegn S ↓ ↑, korrelationsgevinst / tryk, zone med direkte visning (punkt 4, 5, 7 og 9 bis). Farverne bliver altid.
  - Ved beregning af signalet: alle linjer i beregningen, flad Jord profil, beregning log og detaljeret fil log.
  - Rettelser og levende data: vejrkorrektion (Misme, subsidence), luft-hav kontrast, gennemsnit med pletter (corridoi), APRS 2 m spring og LoRa, over bjerget uden tab (punkt 4 bis, 9 bis, 9 quater og 10 bis).
  - Essentiel indeholder kun isolines, vejr- og medierettelser med pletter; Normal tilføjer isobare, inversion, vejr tegn, direkte visning, flad jord, luft-hav og APRS; Komplet tænder det. Niveauet ændrer kun, hvad du ser, og hvilke rettelser der indtastes: beregningerne forbliver de samme.
- Kilde til data og historiske tilfælde: fra hvor profilerne kommer og hvilken dag du ser (punkt 3 bis og 3 ter).
- Min station og korrespondenten: Locator, antenner, kraft, kabler, støj (afsnit 7 og 12).
- Beregn signal og dækning i optik: de to beregninger på jorden (afsnit 8-13).
- Beregningsregister: hvert trin med de anvendte numre, herunder i Data / proguast- calcoli.log-filen.

## 3. Fra GFS-data til profil

For hvert punkt på gitteret og hver time læser vi trykket på jorden, temperaturen og fugtigheden på 2 m, vinden ved 10 m, højden af grænsen lag, CAPE og temperatur og fugtighed i forhold til 1000, 975, 950, 925, 900, 850, 800, 700, 600 og 500 hPa. Fra 1.3.0 kan du også læse vinden til 850 hPa og den lodrette hastighed til 850 og 700 hPa (afsnit 9 bis).

Niveauerne er beregnet med ipsometric formel og den virtuelle temperatur; niveauer under jorden er knappe. For hvert niveau:

- N = 77,6 / T · (P + 4810 · e / T), med T i kelvin, P og hPa, og med mætningstrykket af ITU-R P.45314, eq. 9, med EF-korrektionsfaktoren (omkontrolleret på den officielle tekst) [4].
- M = N + 157 · h, med h i km: den modificerede reaktivitet. Hvor M går derop er en ledning.

## 3 bis. Datakilde

Menuen Datakilde vælger hvor profilerne kommer fra:

- Automatisk (anbefales): GFS først fra NOMADIS [14], NOAA service, der distribuerer modellen: for hvert øjeblik programmet beder to udskæringer af området i G RIB2 format og læser dem fra sig selv; NOMADIS har ingen daglig grænse, kun en per minut, at programmet respekterer. Med brede net (fra 0,5 ° til op) bruge filer til 0,5 ° eller 1. hver 3. time, meget lettere. De punkter, som NOMADS ikke giver, eller alle, hvis NOMADS ikke reagerer, spørge Open- Meteo [2]. De to kilder giver samme model og samme race, så du kan blande i samme gitter.
- GFS fra Open- Meteo: kun Open- Meteo, som i 1.2 (ved grænsen pr. minut forventes).
- GFS fra NOAA NOMADS: kun NOMADS. Langsommere at downloade (to filer i timen, et halvt sekund mellem den ene anmodning og den anden ikke at overskride NOAA grænse), men ikke forbruge Open- Meteo grænse.
- ICON- EU (DWD) og ECMWF IFS 0,25 ° fra Open- Meteo: andre modeller end GFS, som gælder i hele nettet og for ruterne. De blandes ikke med GFS: sammenligne to modeller på samme tid er en enkel måde at se, hvordan prognosen er usikker. ICON- EU dækker kun Europa; ECMWF har ikke alle GFS-trykniveauer (de manglende niveauer hoppe) og kan ikke have grænsen lag.

Den anvendte kilde er skrevet i statuslinjen og i den første linje af resultaterne (Datakilde), med hvor mange point der kom fra hver tjeneste og hvor mange fra cachen. Diskcachen er adskilt af model.

## 3 ter. Historiske sager

Den historiske Case menu gennemgår en dokumenteret åbning med Open- Meteo [15] arkiv GFS prognoser: kortet flytter til det område af sagen, bandet sætter sig selv og tidsmarkøren går fra den første dag i sagen. Ved at trykke på Opdatering prognose du downloade profiler af disse dage; signal beregning fungerer som for den aktuelle prognose. Det tjener til at sammenligne modellen med, hvad der virkelig skete.

Det første tilfælde er den 14- 15 August 2022 fortalte i QST (November 2022, "The World over 50 MHz" [23]): en åbning under et højt tryk mellem Manitoba og Kansas / Missouri, fra 144 til 1296 MHz næsten alle i FT8, med den canadiske rekord på 10 GHz på 1228,56 km i Q65 og signaler op til 1941 km. Den anden er åbningen mellem den 29. december 2021 og den 2. januar 2022 på det tyrrhenske og det vestlige Middelhav ved 144 MHz fra rapporterne fra IC8TEM: første åbninger den 29 (op til 737 km), bredere 31 ( IT9GSF - EB5EA 1217 km), stor åbning den 1. januar mellem Italien, Sicilien og Spanien. For ældre sager (f.eks. september 2000) har Open- Meteo-arkivet ingen data. I arkivet mangler højden af grænsen lag: der er dens korrektion ikke finder anvendelse.

## 4. Indeks og farver

For hvert lag mellem to niveauer programmet sammenligner N fald med tærskelværdierne i tabel 6.5 i Bean og Dutton [5]: for eksempel mellem 1000 og 850 hPa et fald op til 60 N / km er normalt, over 100 N / km er super- refraktion. Et super- refraktionslag er 10-15 dB gevinst værd; en ledning, der fanger båndet fra 20 til 25 dB, ifølge underskuddet af M. stærk vind til jorden, høj grænse lag og høj CAPE reducere gevinst, fordi de fjerner eller bryde lagene [5, § 4.4].

Farven er den anslåede gevinst i dB, med en fast skala lige i alle temaer:

| Gain | Farve | Betydning |
| under 7 dB | Ingen. | næsten normal atmosfære |
| 7-10 dB | blå | hældning over normal |
| 10- 13 dB | Grønne | superrefraktionslag |
| 13- 16 dB | gul | fast lag, men uden kanaler til bånd |
| 16- 18,5 dB | orange | meget stærkt lag eller for tynd kanal |
| 18,5-21 dB | rød | Conduit Trapping Band |
| fra 21 dB | Lilla | Stærk og dyb kanal |

Med displayboksen også 3-7 dB zoner mellem 3 og 7 dB, hvor noget bliver dannet, men forbliver under skalaen, kan du se med en lys grå fastholdelsesanordning; farver ændrer sig ikke. En stiplet rude afgrænser det område, der er dækket af sin egen prognose, og viser den maksimale synlige gevinst: hvis det er under 7 dB kortet forbliver uden farver, fordi atmosfæren er næsten normal, ikke fordi dataene mangler. Når du henter et nyt klik på Opdatering prognose ikke afbryde; at stoppe det der er Stop.

Farverne er uigennemsigtige til 82%, så baggrunden for temaet bare ændrer dem. Locator gitter og firkantede navne er designet over farverne. Hvor din forudsigelse ikke er der, men Hepburn eller F5LEN give 5 eller flere skak kort vises med farven på denne kilde.

Kortet dømmer alle punkter alene. Signalberegningen kontrollerer i stedet, at laget er kontinuerligt langs ruten (afsnit 9): en rute, der krydser meget gul, kan give mindre end kortet får dig til at tænke.

## 4 bis. Prognose medieret af reklamer: korridorer (fra 1.3.3)

Copyright Policy: Et link alene må aldrig tegne en kanal. En isoleret og kort modtagelse kan være spredningen af et transiterende plan (APRS pakken er meget kort), en åbning af et par minutter eller en fejl, og ville give "drop-adfærd" de bedrage. Programmet genkender en korridor, når i samme band:

- mindst 3 par uafhængige stationer (ingen fælles stationer) arbejder på ruter på mindst 300 km
- ruterne er parallelle: azimut inden for ± 15 °, gennemsnitlige punkter inden for 150 km fra den fælles akse
- alle inden for de sidste 30 minutter, idet strålen gentages i mindst 10 minutter (vedholdenhed)
- for ARS tæller et spring kun, hvis det samme spring gentages
- fra 1.3.4 tæller ruterne over 1500 km kun, hvis strålen kommer fra mindst to forskellige kilder (f.eks. PSKReporter og en klynge): fra en enkelt kilde til disse afstande på 2 m er oftere EME, spredt meteor eller forkerte lokatorer end tropo.
- fra 1.3.4, ikke tælle stationer "til ray": som i den sidste time har mindst 5 korrespondenter over 1000 km fordelt over en ventilator på mindst 150 °. I 2 m er tropen retningsbestemt; en modtager, der "gratis" sammen Israel, Portugal, England og Italien fra 2000- 3700 km, ofte med positive signaler, har næsten helt sikkert frekvensen konfigureret dårligt. I 17- 22 / 11 / 2026 I5MZY Arkiv 72% af indkvarteringen i 2 m over 1000 km kom fra stationer, så på 22 / 11 ved 10: 53 UTC var OH6HLH, OZ1ITS, F4LUK, OM8ABC og SP2JLR, og med filteret den længste korridor nedstiger fra 2175 km (regole 1.3.3) til 615 km.

Kilder: klyngepletter (ikke dem, der er mærket AS / ACS), PSKReporter via MQTT, deres QSO og WSJT- X / JTDX / MSHV dekodninger, og APRS hopper i 2 m og LoRa 433 / 868 MHz.

APRS: 2 m og LoRa. Programmet læser APRS-IS i read- only (rotate.aprs2.net, filter på Europa) og udtrækker radiospring mellem kendte placering stationer (kilde → digipeater → IGate), såsom VHF kort over DXView og APRS PropView (RF- YVY, Apache 2.0). Tre bånd er værd: 2 m, LoRa 433 MHz (båndgange 432) og LoRa 868 MHz (corridoi, der tæller for 1296: en ledning, der bringer 868 MHz også de højeste frekvenser). Bandet kommer fra LoRa tocall (aprs- deviceid liste) og fra frekvenserne eller ord i kommentaren; et spring er kun værd mellem to stationer af samme band.

Det delikate punkt er de aktive repeterere: et signal modtaget på 500 km kan nås med to eller tre digitaliseringsretransmissioner. Et spring tæller derfor kun, hvis det er mellem to reelle og fortløbende sendere:

- stien skal spores: enhver digipeater, der har videresendt, har sat sit navn, og springet måles mellem den ene og den næste; et alias, der anvendes uden navn (W IDE1 *, W IDE2 *, regionale alias), bryder kæden, og denne strækning tæller ikke
- skjulte retransmissioner: programmet husker stien hver kilde normalt spørger (for eksempel W IDE2 -2). Hvis en pakke kommer med et spring i mindre (W IDE2 -1) og intet navn forklarer det, en digipeater retransmitteret det uden sporing og pakken kasseres; kilderne aldrig set før tæller ikke;
- digipeater i midten af vejen: hvis der mellem de to stationer, mere eller mindre på vejen, er en digipeater af samme aktive band i de sidste tre timer, springet er kasseret, fordi det kunne have retransmitteret (det er især for digipeater LoRa, der retransmitterer uden at ændre stien);
- ikke-radiopakker (qAC, qAX, TCPIP), som drives af internettet på radio fra en IGate (tredjeparter), HF og forskellige frekvenser, stationer i en højde på mere end 1500 m (palloni, fly) og springer over 1500 km.

Virkning på kortet (fra 1.3.4). Den trækker kun det område, hvor minimumsgarantien er gyldig: et bånd på ± 60 km omkring den centrale del af hver rute (fra 15% til 85% af dens længde), ikke på stationer i enderne, hvor ledningen måske ikke er der. Båndene i alle korridorer er samlet i en neutral farve (teksten) og semitransparent: overlapningerne er ikke tilføjet, og farven er ikke forvekslet med lilla af skalaen (21 dB og derover). Etiketterne (træk, gennemsnitlig afstand, minimal gevinst) scannes med hinanden; passerer med musen på en etiket de sektioner af korridoren vises.

I området og kun i nærheden af den aktuelle tid (± 90 minutter) falder prognosen ikke under et minimum. Begrundelsen er: Hvis stationerne virkelig passerer, er formeringen der, uanset hvad vejrmodellen siger.

Fordi de korte ruter er lidt værd (fra 1.3.4). I 2 m, mellem 300 og 600 km, to gode stationer (ca. 100 watt, antenner, FT8 eller CW) forbinder næsten hver dag selv med den normale atmosfære, for troposcatter: signalet diffused af de små uregelmæssigheder i luften i den fælles volumen af de to bundter. Tre eller fire pletter på 500 km siger, at bandet fungerer som sædvanligt, ikke at der er en åbning: GEKO Magazine n. 69 ( IU3QEZ) estimater for troposcatter med 2 × 10 strømmende elementer på ca. 440 km i SSB QRP, 510 km i SSB med 50 W, 600 km i CW QRP og 640 km i SSB med 500 W (kilder, elleve omgange). Kun over 600- 700 km troposcatter er ikke længere nok og en række parallelle forbindelser virkelig angiver en ledning eller lag. Op til 1.3.3 en korridor på 450- 600 km hævet prognosen til 9- 12 dB, og kortet viste en forbedring, der ikke var der.

Startværdien går derefter med den gennemsnitlige afstand af ruterne:

- op til 400 km: 3 dB
- fra 400 til 700 km: salt på lineær vis fra 3 til 10 dB (500 km → 5,3 dB; 600 km → 7,7 dB)
- fra 700 til 900 km: fra 10 til 16 dB; der er over 900 km tilbage ved 16 dB.

Under 7 dB, dvs. i begyndelsen af skalaen, er der det minimum tilbage, at: korridoren er tegnet med sin etiket, det ses, at båndet virker, men farverne på kortet ikke ændre sig (med boksen viser også 3-7 dB den grå punkteret). over 7 dB vejes værdien, halvdelen og halvdelen, med antallet af uafhængige ruter (3 slag → 0,25; 6 eller mere → 1) og med medianen S / N for relationer (− 20 dB → 0,5; 0 dB eller mere → 1; ikke kendt, som i klyngespots, → 0,75), startende fra 7 dB. Eksempler: 3 ruter uden S / N på 500 km → 5,3 dB; 5 ruter på 615 km → 7,7 dB; 3 ruter uden S / N på 900 km → 11,5 dB; 3 ruter til − 20 dB på 900 km → 10,4 dB; 6 ruter til 0 dB på 900 km → 16 dB.

Den mindste afstand er stadig 300 km: at hæve den til 500 km ville også have fjernet de reelle kortdistanceåbninger (f.eks. en kanal på Tyrrhenian ved 400 km, som er anerkendt af vejrudsigten og meget stærke signaler mere end fra afstanden alene). I I5MZY -arkivet, 22 / 11 / 2026 kl. 10: 53 UTC, med denne regel og strålefilteret var de længste korridorer 615 km (7,7 dB) og 540 km (6,3 dB); med reglerne i 1.3.3 nåede det 2175 km og 16 dB.

Indtil 1.3.3 var det minimum, der var gældende på hele rektanglet af korridoren og afhang kun af afstanden (fra 10 dB til 300 km til 16 dB til 900 km): med mange lange korridorer samlet (f.eks. 22 / 11 / 2026, tolv korridorer op til 2620 km) løftede næsten hele Europa til 16 dB.

Gevinstkurverne trækker ikke længere "gocce": De mindre øer på 220 km i begge retninger forbliver farvede, men uden lukket kurve, medmindre en korridor bekræfter dem. Den slukkes med "Preview medieret af pletterne".

## 5. Valgfrie linjer og sammenligning med tryk og inversion

Optag kurver: linjer på 7, 10, 13, 16, 18,5 og 21 dB, kanterne af skalaen, trukket på marken glattet med metoden af marchkvadrater, med etiketten i dB.

Isobare: tryk ved havoverfladen hver fjerde fra samme GFS, til den time af prognosen, med centre H og L.

Termisk inversion: kurver dækket med hvor meget luft ved 925 eller 850 hPa er varmere end ved 2 m, ved + 1, + 3, + 5 og + 8 ° C.

Under kontrol en linje giver gevinst aftale med trykket og med det modsatte på den synlige zone: Pearson korrelationskoefficient, − 1 til + 1. Kanalerne er ofte født i anticykloner (luften, der falder opvarmer og danner det modsatte), så en positiv aftale med trykket er normal; men inde i den samme anticyklon gevinst ændrer sig meget mellem hav, kyst og bjerge. Inversion alene er ikke nok: bøje bølgerne kun, hvis over luften er endda tørretumbler [5]. Linjen tjener til at se, nu for nu, hvilke af de to bedste forklarer de farvede områder. Den samme sammenligning er skrevet i registret.

## 6. Potentielle rørledninger: dobbelt beregning

GFS niveauer spænder fra 200 til 1000 m: en tynd inversion er spredt og modellen ikke se kanalen. Hvis N-reduktionen mellem to niveauer overstiger normalt (40 N / km), er luften stabil (termisk gradient under 3 K / km) og over tør (specifikt fugtighedsforhold under 0,8), betragter programmet en potentiel kanal og beregner den på to måder:

- Beregning A: det koncentrerede fald på 50 m.
- Beregning B: det samme fald fordelt på den typiske tykkelse af rørledninger i dette område, taget fra klimatiske kort af ITU-R P.453 [4] (på jorden eller forhøjet).

For underskuddet og den maksimale indfangede bølgelængde holdes den laveste værdi: det er et forsigtigt skøn.

ITU-R P.453 data er © ITU og kan bruges til personlige og ikke-kommercielle formål, citerer kilden, men kan ikke gendistribueres: for dette er jeg ikke i programpakken. For at tænde for B-beregningen, bare downloade P.453 kort fra ITU i fanen: programmet, efter at have vist betingelserne for brug, hente arkivet af rørledninger fra ITU site (forhøjet _ og _ overflade _ duct.zip) til brugerens personlige brug, udtrække filerne i Data / P453 og tænde for beregningen uden genstart. Alternativt kan du downloade fra ITU- R Study Group 3 digital produkt side (ITU- R Study Group 3, Digitale produkter, P.453, Surface data og Elevated kanal data) og sætte de syv S _ OCCUR. TXT, S _ THICK.TXT, S _ STREN. TXT, E _ OCCUR.TXT, E _ base.T Uden de filer programmet bruger kun beregning A og skriver det i fanen og i registreringsdatabasen. Forholdet mellem underskud A og typisk intensitet P.453 er anomali: Hvor mange gange i dag er rørledningen stærkere end normalt. P453

## 7. Stationer og lokatorer

Udlejeren kan have 4, 6, 8 eller 10 tegn. Med 8 eller 10 tegn er positionen præcis og jordandelen kommer fra Copernicus GLO- 30: højden af antennen er nok fra jorden. Med 4 eller 6 tegn er pladsen for stor: det er bedre at skrive kvoten af antennen på havet; hvis 0 programmet forbliver estimatet med SRTM jord i midten af pladsen plus højden fra jorden og signalerer det i resultaterne. For den tilsvarende standard højde er 3 m fra jorden; med havets andel 0 anvendes SRTM jord plus højde.

## 8. Jord, diffraktion og troposcatter

Profilen af ruten udtages på Copernicus GLO- 30 hver 30 m (mere end 120 km bred). Earth radius ækvivalent er k = 157 / (157 + ΔN), med ΔN faldet af N i den første km fra GFS-profiler langs [6, 7] rute.

Relieffer tæt på stationer (fra 1.3.2, forfatterens kriterium). P.452 betaler horisonten vinkel lineært, 0,573 dB per mrad: en lettelse på et par tiere meter et par hundrede meter fra antennen koster tiere af dB, meget mere end der er observeret. Derfor kan reliefferne inden for 20 km fra hver station tilføje op til 6 dB: horisonten vinkel på grund af dem er begrænset til den af de mest fjerne relieffer plus 10,5 mrad. Den gælder for din station og for korrespondenten. Linjen "Nær relieffer" siger, hvor mange dBs er taget af.

Landkilde (fra 1.3.2). Kvoterne kommer fra Copernicus DEM GLO- 30 [3]: En anden bue, havet er 0 og kysten er klar. Indtil 1.3.1 brugte programmet Terrain Tiles "skadi" af AWS. Nær kysten disse firkanter blande SSRTM med lav opløsning havbunden data og sprede øens andel på havet: omkring Capri skabe en spøgelsesring på 80- 100 m mellem 0,5 og 1 km fra kysten. Den 21. november 2026 IC8TEM rapporterede en ikke eksisterende horisont mod nordvest; point- by-point sammenligning med Copernicus bekræftede det (94 m til søs mod nordvest af Marina Grande vs. 0). Denne falske horisont på + 2,6 ° kostede kyststationerne ca. 26 dB troposcatter. Skadipanelerne forbliver kun som reserve, når Copernicus ikke reagerer. De Copernicus ruder vejer 15-30 MB og download én gang; dem af kun havet ikke findes og er værd at 0.

- Fri plads: Lfs = 92,44 + 20 log f (GHz) + 20 log d (km) [8].
- Diffraction: Bullington metode på den virkelige profil plus ITU-R P.452-16 § 4.2 [6] delta-Bullington sfærisk jord korrektion, med ITU-R P.526 [9] sfærisk tab.
- Første Fresnel zone: radius ≠ (λ · d1 · d2 / d); fri, hvis jorden efterlader mindst 0,6 af radius. For hver hindring vises [9] Knife Board V og J (VV) parameteren..
- I udformningen af profilen er den direkte radius grøn i optisk og rød visning, hvis den passerer under jorden eller under krumning af jorden; røde prikker er de forhindringer, der invaderer det første område af Fresnel (lilla stiplet linje). På havet det "røde punkt" i højden 0 m er ikke et bjerg: det er havet stiger for krumning. Passage med musen på radius eller på et punkt vises en tegneserie med forklaring og værdier.
- Når en relief nær antennen lukker retningen (f.eks. en horisont på ti grader), giver diffraktion og troposcatter tab på hundredvis af dB: over 300 dB skriver programmet "> 300 dB" og "retning lukket af en nabohindring", signalet "< 230 dB" og S / N-forholdet "− 60 dB". I så fald anbefales det at se på "Anbefalet plante" og "Via monte".
- Troposcatter: ITU-R P.452-16 eq. 45 til 50% af [6] tid, med vinkelafstand fra horisonterne og N0 fra profilerne.
- Uden tropo: summen af beføjelser diffraktion og troposcatter, aldrig under det frie rum.

## 8 bis. Troposcatter: fire modeller og usikkerhed

Ved siden af P.452 programmet beregner troposcatter med tre andre modeller, som det gør TropoLink [16] program, og viser deres forskel som usikkerhed:

- NBS TN101 § 9 [25] (fra 1.3.5), median: L = 30 log f − 20 log d + F (θd) − F0 + H0 + Lgp. F (θd) er dæmpningsfunktionen i bilag III i sine analytiske former (Ns 301) med korrektion for Ns, H0 frekvensgevinstfunktionen i § 9.2: ved 144 MHz med lave antenner i bølgelængder, refleksion fra jorden delvis sletter feltet i den nederste del af den fælles volumen og tabssalt på 3-10 dB. Ns er den af timen GFS profiler, de effektive horisonter og højder kommer fra Copernicus profil, Lgp = 0,07 · e ^ (0,055 (Gt + Gr)) er åbningen kobling tab. Det er den samme form, som ITM bruger i sit indre, men med den reaktivitet i dag i stedet for klimaet. Det afprøves på flade sektioner ved 144 MHz med antenner ved 20 m: inden for 234 dB fra ITM mellem 150 og 900 km og omkring 2 dB i forenklet form 9.2b TN101, som ikke har H0.

- ITU- R P.617- 5 [17], median: L = F + 22 log f + 35 log θ + 17 log d + Lc med f i MHz, θ i mrad, d i km, F = 0,18 · N0 · e ^ (− hs / 7,35) − 0,23 · ΔN og Lc = 0,07 · e ^ (0,055 (Gt + Gr)). N0 og ΔN er normalt taget fra ITU 's klimakort; her bruger vi hver time profilerne GFS ( N0 · e ^ (− hs / 7,35) er reaktivitet til jorden, ΔN faldet i den første km), så også troposcatter følger tiden.
- RSGB VHF / UHF Handbook Formel, DL2FZN [18]: L = Lfs + 24,44 + 4,283 ln f + 0,06656 d. Hældningen med afstanden er næsten lig med PROPBER (0,067 dB / km).

Fælles rumfang (fra 1.3.1): er det punkt, hvor radius af de to stationer krydses, hver hældende til sin horisont vinkel, på en Jord med tilsvarende radius ae = k · 6371 km. Fra TX radius stiger som z = hTX + x · θt + x2 / (2 · ae); Afstanden x = (hRX − hTX + d · θr + d2 / (2 · ae) / θ, med θ = d / ae + θt + θr. Med flade horisonter og antenner på samme kvote er basen værd d2 / (8 · ae): omkring 1,3 km til 300 km, 3,7 km til 500 km, 9,4 km til 800 km. Jo højere volumen, jo mere luft er tør og hærdet, og jo mindre spredes den: derfor løber troposcatter mere end 800- 1000 km ud. I resultaterne er andelen og placeringen af basen, reaktiviteten, gradienten og fugtigheden af GFS i denne højde, og den tilsvarende afstand af Bray (QST, november 1961): hver ekstra eller mindre horisont er værd omkring 111 km væk. Reference: præsentationen "Over horisonten i VHF" af IU3QEZ (GEKO Magazine nr. 69).

Troposcatter linjen, andre modeller viser de to tab og usikkerhed som halvdelen af forskellen mellem den mest optimistiske model og den mest pessimistiske. Der er kun tale om sammenligninger: Tabet uden tropo forbliver P.452. tab

## 8 ter. eftermiddag troposcatter over relieffer (fra 1.3.4)

IK3XTV, fra Thiene ( JN55, med alperne på 10 km), arbejder om sommeren eftermiddage Sydtyskland på 400- 500 km, uden at se, med stærke signaler, men med hurtig og dyb evakuering; forfatteren tilskrives dem til "konvektive celler justeret som et tog af linser". Observationen bekræftes; forklaringen i programmet er en anden, fysisk fastere.

Om eftermiddagen sætter solen luft over reliefferne, og grænsen stiger til 233 km. På toppen af den fugtige luft nedenfor opfylder den tørre luft ovenfor: der turbulensen af brydningsindekset (den størrelse, der styrer troposcatter) er maksimal. Når denne top når det fælles volumen af ruten, som på 400- 500 km er omkring 233 km, er troposcatter forstærket. Dette forklarer eftermiddagens tid, højt tryk (stille cylindrisk, stærk opvarmning) og hurtig evakuering, typisk for diffusion og ikke for kanalerne. Konvektive celler fungerer ikke som linser: producere turbulens, og det er turbulensen i toppen af det omarbejdede lag, der spreder signalet.

Konsekvenser for læseprognoser:

- på jorden er den første eftermiddag den værste tid for de lave kanaler og lag, men ikke for troposcatter hinsides bjergene: de to ting bør læses separat;
- programmet har allerede de to data, det tjener, højden af det grænselag, der leveres af modellen, og andelen af det fælles volumen af ruten (fælles volumen) i resultaterne: når de er tæt, kan en eftermiddag bjergbehandling gå bedre end beregningen i normal atmosfære;
- fra 1.3.4 er der taget højde for pointpunktsberegningen: hvis det fælles volumen er på jorden, langs ruten er der relieffer på mindst 800 m, solen er over 15 °, og grænsen lag overstiger 800 m, og toppen af laget omformning (model jordhastighed + grænse lag højde) er inden for 500 m fra bunden af det fælles volumen, troposcatter vinder 3 dB; mellem 500 og 1000 m den lineære gevinst falder til nul. Linjen "Troposcatter eftermiddag". Med sne langs ruten (mindst 10 cm på jorden i henhold til modellen, sne _ dybde variabel af Open- Meteo eller SNOD af GFS fra NOMADIS) forstærkningen ikke gælder: på sneen jorden forbliver kold og omformet lag ikke vokser. Værdien på 3 dB er foreløbig og skal kalibreres med rigtige links; den slukkes med vejrkorrektion.

Kilder: observationer af F. Egano IK3XTV (Radio Rivista e www.qsl.net / ik3xtv, "Propagazione sulle 2 metri", "Celle konvestitive", "Troposcatter over Alperne"), kilder § 13.

## 9. Grundlaget langs ruten

Det er kernen i signalberegningen. Langs ruten tages GFS-profiler hver 25 km. For hvert tryklag regnes den længste strækning, hvor det samme lag er til stede på fortløbende punkter; kun tykke lag regnes for at være mindst 40 m [10].

- kontinuitet: fra hvilken km til hvilken km og fra hvilken del af ruten
- top og base og hældning i m / km
- jord inde i laget: den km, hvor bjergene stiger op over basen
- Overdækkede land: på havet er kanalen næsten altid, bestemmer strækket af land [10]. Hvis jorden overstiger 10% af ruten, er den anvendte fraktion det mindste mellem kontinuitet og dækning af jorden.

Trope gevinst er gevinsten af laget for den anvendte fraktion. Hvis mere end halvdelen af profilerne er sub- refraktiv gevinst er negativ.

## 9 bis. Vejrkorrektion: stabilitet, stilstandsevne, strejke

Fra 1.3.0 giver profilen også tre meteorologiske oplysninger, som kan ses i registret (regave "stabilitet (Misme)"), i Meteo linje langs ruten af resultaterne og med Segni meteo boks, på kortet.

- Mimes stabilitet [5, kapitel 6]. Misme binder feltet ud over horisonten til en sammensat parameter: M = a · (ge − 40 + b · ΔW), med a = 0,5 dB pr N / km, den tilsvarende gradient ge og den termiske stabilitet ΔW, arbejdet for at hæve luften på 1 km. Programmet beregner ΔW = g · Ω (θ − θ0) / θ0 dz mellem jord og 1 km over, med θ potentielle temperatur; standard atmosfære er værd omkring 56 J / kg. Udtrykket tilføjet til gevinsten er 0,07 dB for J / kg ud over standarden, begrænset mellem − 4 og + 6 dB, og tæller kun, hvor ingen ledning fanger båndet (gradienten er allerede i Bean og Dutton indeks). Kalibreringen er midlertidig: den skal kontrolleres med nøgletallene (punkt 14).
- Subsidence og op ad luft [19]. Moler og Holden, på ruter på 125- 305 km til havet ved 1365 og 9380 MHz, fandt det højeste signal på omkring 2,5 dB i gennemsnit med luft kommer ned, at med luft stiger, og i et tilfælde en forstærkning på omkring 20 dB i 3 timer i begyndelsen af subsidence, efterfulgt af et fald på 22 dB, når luften vendte tilbage til konvergere. GFS giver den lodrette hastighed til 850 og 700 hPa. Hvis luften på begge niveauer falder mere end 1 cm / s, og der er et omvendt eller et skarpt fald i det potentielle brydningsindeks K (N rapporteret adiabatisk til 1000 hPa, som fremhæver den tørre og varme luft faldet fra toppen [5]), 1,5 dB tilføjes; hvis luften stiger med mere end 5 cm / s, de tager ud 2.
- [20] Scirocco. Varm og tør luft fra Sahara over køligere marin luft giver meget stærke inversioner fra det østlige Middelhav til Gibraltar. S skiltet vises hvor ved 850 hPa vinden kommer fra 100- 200 ° med mindst 7 m / s og temperaturen er mindst 16 ° C, på Middelhavet. Det er bare en indikation: gevinsten bestemmer lag af profilen.

Uden en ledning, der fanger båndet den samlede gevinst ikke overstiger 16 dB, slutningen af den gule: korrektioner gør stærkere lag mere synlige, men ikke passerer dem gennem kanaler (rød og lilla forbliver ved kanalerne).

- Persistens (fra 1.3.1). De største åbninger kommer efter gode højtryksdage (VHF / UHF DX Book; Biancomano og Leyshon 1998, regel på 12 timer; rapporterne af 29 / 12 / 202-2 / 1 / 2022 vokser fra den første til den fjerde dag). For hver celle tæller programmet timerne af de foregående 24 med gevinst på mindst 13 dB og tilføjer op til 2 dB i forhold, kun hvis gevinsten er allerede mindst 10 dB.

Hvordan man læser tegnene på kortet (Skift vejrtegn; fra 1.3.2 symbolet er stor dobbelt og passerer med musen over et tegn vises en tegneserie med forklaring og værdier af cellen):

| Signér | Farve | Hvad betyder det? | Virkninger på tropen |
|---|---|---|---|
|  ↓  | Grønne | subsidence: luftdråber (anticyklon), varme til komprimering og tør, styrker det modsatte og holder det fast | favorable, + 1,5 dB |
|  ↑  | rød | op ad luften (front, lavt tryk, konvektion): blander lave lag og bryder inversioner og kanaler | ugunstige, − 2 dB |
|  S  | orange | Syrup: varm og tør luft fra Sahara over frisk havluft | positivt spor, ingen korrektion: Beslut lagene |

Et område fuld af grøn ↓ under en anticyklon er det klassiske tegn på en hård åbning; Red ↑ pile langs linjen siger lagene er ved at bryde, selv om farven på cellen er stadig god.

Boksen Vejrkorrektion slukker disse rettelser for at se modellen på 1.2.

Validering (punkt 14): på 593 tilslutninger til 144 MHz mellem den 29. december 2021 og den 2. januar 2022 sammenlignet med de samme ruter og to uger senere er udskillelsesindekset (0,5 = nul, 1 = perfekt) 0,79 uden rettelser og 0,91 med rettelser (før taget ved 16 dB). Den samme sammenligning viser den vigtigste grænse: den ledning på havet i lav højde, som i disse dage bragte skiltene mellem Campania og Liguria, er ikke synlig på niveauer af GFS.

## 9 ter. Nyheder 1.3.2: to skøn, loft, højt tryk, hav, stationer, kamre

Forsigtigt og aggressivt skøn. Gevinsten fra gradienten vokser fra 0 dB til normalgrænsen op til 10 dB til tærsklen for overbrydning. I 1000- 850-laget er den forsigtige tærskel Bean og Dutton (100 N / km). Den aggressive tærskel er den klassiske Super- Refraktionstærskel på 79 N / km (k · 2; P.453 bruger − 100 N / km i de første 100 m), som med k50 = 157 / (157 − ΔN) er k · 2 (k = 2,5 svarer til omkring 94 N / km). Den pointpunkt beregning viser de to tab flankeret; med en kanal, der fælder banden sammenfaldende. Kortet bruger forsigtighed.

Loft på 2500 m. Veje og kanaler søges kun op til 2500 m over jorden: jo højere antenner af amatør radioer ikke udnytte dem. Med 1500 m mistede valideringen et adskillelsespunkt og 4% af tilslutningerne over 16 dB, fordi tilbageførslerne om vinteren ofte er mellem 1500 og 2500 m.

Højtryk forlænget. + 1 dB, hvor mindst 7 af de 9 celler omkring har reduceret trykket til havet på 1020 hPa eller mere, og vinden er svag i de lave lag (10 m under 5 m / s, 850 hPa under 8 m / s), bortset fra stigende bevægelser.

Havvægt. I punkt-til-punkt beregning ganges gevinsten af lagene med 0,8 + 0,4 · μ1, hvor μ1 er faktoren for ITU-R P.452 (eq. 51) på den længere kontinuerlige strækning af jord: 1 på åbent hav, små med lange strækninger af jord.

Stationer og udført. Når en ledning, der fanger båndet, kun dækker en del af linjen, sammenlignes andelen af hver antenne med bunden og toppen af kanalen i begyndelsen og slutningen af det dækkede afsnit; gevinsten ganges med 0,5 + 0,5 · Ω (e ^ (− ΔhTX / 300) · e ^ (− ΔhRX / 300), med Δh afstanden i meter ud af kanalen. Med kanalen på næsten hele ruten er koblingen af § 10.

Lag og kamme. Hvis toppen af GFS-grænselaget (jordhastigheden af den mest grænselag _ højde-model) overstiger vejens sprækker (maksimal lokal over 300 m, hvis den højeste overstiger 500 m), passerer laget over bjergene uden at bryde: + 1,5 dB for den brøkdel af overdækkede kamre. Det er observation af IT9GSF på åbningerne fra Sicilien til Tyskland med favorable Tyrrhenian.

Niveauer tæt på jorden på havet. Med NOMADIS kilde programmet downloader også overfladetemperaturen (på havet er SST), temperatur og fugtighed ved 80 m og i laget 30- 0 hPa over jorden. På søpunkter (modelhastighed under 5 m) tilføjes disse niveauer til profilen under det første tryk for at se kanalen på 100-200 m, som trykniveauerne ikke løser. På jorden er ikke brugt: nat og dag lag er endnu ikke gyldig. Open- Meteo giver kun overfladetemperaturen.

Fordampningsrørledning. Fra SST, fra temperatur og fugtighed til 2 m er estimeret den δ højde af fordampningskanalen med den neutrale logaritmisk profil (som i model Paulus og Jeske, uden korrektion af stabilitet): luften til håret i vandet er mættet til SST (98% for salt), derfor

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

begrænset mellem 0 og 40 m. For at fange 144 MHz ville det tage omkring 180 m (Kerr), derefter ved 144 MHz fordampningskanalen styrker lidt; fra 432 MHz op, med lave antenner på havet, meget (afsnit 11 bis). Indtast parabolske ligning og i linjen Rettelser 1.3.2.

Dagens tidspunkt (besøg). Programmet beregner højden af solen på hvert punkt. Fra dag til dag på jorden, med solen over 15 ° og grænse lag over 800 m eller vind over 5 m / s, advarer om, at jordkanalerne er reformede; om natten, med sol under − 3 °, vind under 3 m / s og grænse lag under 300 m, advarer, at med rolig himmel er muligt ledet af bestråling, som falder efter solopgang. Ikke på havet. For nu bliver de set, ikke numre: December validering ikke har nok sommerdage til at kalibrere dem. Det er to punkter, som forfatteren anser for vigtige, fra daglig observation.

Normal profil CRPL (alternativ). I stedet for 40 N / km fast følger den eksponentielle referenceatmosfære CRPL: N (h) = Ns · e ^ (− c · h), med − ΔN i den første km = 7,32 · e ^ (0,005577 · Ns) (Bean og Thayer 1959). Med våd luft (Ns høj) den normale er stejlere og tjener en stærkere gradient til at give gevinst; i højden er sødere. Det er slemt, indtil valideringen siger, det hjælper.

## 9 quater. Lufttrafik og lave farvande: Adriaterhavet (fra 1.3.3)

Radiobølger mærker ikke havbunden: Føl kontrasten mellem luft og vand overflade. Dybden tæller, fordi den beslutter, hvor hurtigt havets temperatur følger luftens temperatur.

Adriaterhavet er et lavt hav. Den gennemsnitlige dybde er omkring 250 m, men det nordlige bassin sjældent overstiger 100 m og Trieste bugten forbliver under 25 m; kun mod syd nedstiger over 1200 m. Et lag vand så tyndt varmer op og køler i et par dage, Tyrrhenian i uger. Der er tre typiske situationer:

1. Forår og tidlig sommer, gunstig. Havet er stadig koldt, selv for ferskvandet i Alperne og Po floderne; det ankommer varm og tør luft fra sletten eller Balkan. Varm luft over koldt vand danner en vending på havet (advection pipeline): er den klassiske tilstand af åbningerne mellem Italien, Slovenien og Kroatien.
2. Efterår og vinter med bora, ugunstige. Kold luft over et varmere hav: det lave lag bliver ustabil, blander og kanalen bryder sammen.
3. Efter en bora, chance. Bare fordi havet er lavt, en bora køler det nogle grader i et par dage. Hvis du går ind i en anticyklon med varm luft, er luft-hav kontrasten stærk og kanalen er mere robust end normalt.

Adriaterhavet er også en lukket kanal mellem Apennines og Dinari, mindre end 200 km bred: havlaget forbliver begrænset, og ruterne langs aksen (Puglia-Venezia-Trieste, omkring 700 km) forbliver alle på havet, den bedste tilstand for en lang kanal.

Hvad programmet beregner. På havet punkter, med vejrkorrektion og boksen "Contrasto aria- mare":

| Betingelse | Korrektion |
| --- | --- |
| potentiel temperatur ved 925 hPa (eller 950) over SST på 4 K | Det er ikke mig. |
| ud over SST på 7 K | + 2 dB |
| med luftfugtighed på 925 hPa (over 80%) | − 0,5 dB for disse bonusser |
| afkølet hav på 1,5 ° C eller mere i 72 timer før, med gunstig kontrast | Det er ikke mig. |
| varmere hav af luft ved 2 m 2 ° C (blanding) | − 1, 5 dB |
| varmere hav på 4 ° C | − 3 dB |

Den samlede korrektion forbliver mellem − 3 og + 3 dB, og uden ledning forbliver gevinsten under det gule tag. Tærsklerne er midlertidige: i standardatmosfæren er den potentielle temperatur på 925 hPa større end havets på ca. 233- K, hvorefter 4 K allerede indikerer et bakgear.

Mindet om SST. Modellerne holder havtemperaturen næsten stabil under forudsigelsen. Det seneste fald er afledt af analysen af de tre dage før (Open- Meteo, kun overfladetemperatur, kun havceller): højst 72 timer før minus den aktuelle værdi. Hvis tjenesten ikke reagerer, hukommelse bonus gælder ikke.

Grænser. SST modeller under kysten og i den øvre Adriaterhavet er grove; den kolde pen af Po, for eksempel, er løst dårligt. Batimetri indgår ikke i beregningen: kilderne giver altid SST, som allerede opsummerer virkningen af havbunden. Luft- hav kontrast overlapper delvis i slutningen af Misme stabilitet (§ 9 bis); for dette bonusser er små. Tærsklerne vil være tarate med korridorer af reklamer og QSO gennem Adriaterhavet.

## 10. Leveres på hele ruten

Hvis laget fanger båndet på mindst 80% af den anvendte fraktion og stationerne ikke er i syne, beregnes også tabet i kanalen: fri plads, plus koblingen af antennerne (3 dB, hvis de er inde i kanalen, mere, hvis de er over eller under), plus den resterende dæmpning γd = 5 · 10 − 5 · ae · f ^ (1 / 3) dB / mrad af ITU-R P.452 eq. 49 [6]. Hvis det er mindre end tabet uden trope bliver det forventede tab.

Et bånd forbliver kun i kanalen, hvis dets bølgelængde ikke overstiger λmax = 2 ≠ 2 · (2 / 3) · ≠ (ΔM · 10 − 6) · d (Kerr, eq. 17 [11]). Til dette kan det samme lag fælde 23 cm og 70 cm, men ikke 2 m. Men grænsen er ikke netto: Kerr advarer om, at selv bølger flere gange længere lider en masse af ledningen, og Hepburn og VHF / UHF DX bog taler om "spfered" kanter og "tilgive" kanaler. Fra 1.3.1 bruger programmet en blød ledning: hvis λmax er mellem λmax og 3 · λmax, går gevinsten gradvist fra 15 dB (λ = 3 · λmax) til den fulde ledningsgevinst (λ = λmax), med s = (λmax / λ − 1 / 3) / (2 / 3). I så fald tillades kanalfarverne. Log og Meteo linje langs ruten angiver punkterne med blød kanal.

## 10 bis. Via Monte

Når korrespondent er lukket bag horisonten, programmet søger den bedste retning til at pege antennerne: den oplyste hældning spreder signalet som et radarmål. Det kører horisonten hver femte (undtagen ± 15 ° omkring korrespondenten) og i hver retning tager det oplyste horisontpunkt, over 100 m og mellem 1 og 60 km. Et hurtigt skøn (frit rum, bitaktisk konvertering og 10 dB for hvert horisontniveau fra diffusoren til korrespondenten) vælger de 4 mest lovende, mindst 15 ° fra hinanden; for hver:

- behandle 1, station → relief, i det frie rum;
- behandler 2, relief → korrespondent, beregnet som om relief overføres fra 10 m over toppen, med samme forudsigelse (uden parabolsk ligning);
- omregning med den bistatiske radarligning, med ≠ = σ0 · A, σ0 = − 15 dB (bjerg i VHF / UHF) og A området belyst af R1 -strålen (højst 20 km2):

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

Linjen "Anbefalet punkt" angiver azimuten af den bedste rute, hvis den koster mindre end den direkte. Ved 144 MHz med en aflastning på 20 km og en 15 ° yagi koster bjergvejen ca. 45 dB mere end de 2 ruter alene: det sker kun, når den direkte er lukket af naboforhindringer. Skønnet er brutto (± 10 dB), især for σ0.


Uden tab til bjerget (mulighed, fra 1.3.2). Med "Via monte senza perdite fino al monte" -boksen anses faldet mellem stationen og diffraktionspunktet for nul: er som at bringe stationen til toppen (antennen til 10 m over punktet, samme kraft, antenne og kabel). Derefter vælger horisontrunden det punkt med den mest frie horisont mod korrespondenten (ved paritet, den højeste), og resultatet viser ruten fra punktet til korrespondenten, med signal og S / N, ved siden af det samlede med diffusion. Det er en optimistisk grænse: siger, hvis retningen åbner, når bjerget rejser signalet godt, eller hvor at gå bærbare. Hvert punkt er givet med ten- karakter.

## 10 ter. Indgang til en fjern kanal

Hvis den kanal, der fanger bandet begynder langt, ud over horisonten af stationen, signalet skal ankomme. Programmet bringer signalet til udgangspunktet, i midten af kanalen, for diffraktion: langs Copernicus profil GLO- 30 søger kanten med den højeste Fresnel parameter (ITU-R P.526), med hævelse af det tilsvarende land,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

mere dæmpning på grund af krumning af toppen (afrundet hindring, P.526- 16 § 4.2), med R radius estimeret fra Copernicus GLO- 30 jord til ± 1 km fra kanten,

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

(for m · n > 4 er formen 34b i henstillingen). Barsis (NBS, 1963) målt på tre lange ruter over et bjerg (Colorado, Alaska, Europa) gennemsnitlige tab 10-20 dB højere end den ideelle kant af Fresnel- Kirchhoff: afrundingen forklarer en masse (Barsis og Kirby, 1961: Pikes Peak med en radius på omkring 11,5 km). Resultatet sammenlignes med diffraktion på glat sfærisk jord; det er det værste. Derefter går signalet i kanalen: ledig plads på hele ruten, resterende dæmpning γd af P.452 på strækningen i kanalen og kobling af korrespondenten (3 dB indenfor, 1 dB hver 10 m udenfor). Hvis dette tab er lavere end det forventede tab, bliver det mekanismen ("adfærd (input by diffraction)"). Det er forfatterens præmis om italiensk geografi: med en nærliggende hindring langt under linjen, v er negativ og indgangen koster lidt; med en dorsal front koster 15- 30 dB.

## 10 quater. ITM (Longley- Rice) og udgang fra kanalen

Fra 1.3.2 programmet indeholder den uregelmæssige Terrain Model 1.2.2 af NTIA / ITS (Longley og Rice, ESSA ERL 79- ITS 67, 1968; Hufford, Longley og Kispy, NTIA 82100, 1982), oversat til C # af den officielle kode for den amerikanske regering, som er fri for rettigheder. Oversættelsen giver de samme numre som originalen: automatisk kompilering sammenligner det hver gang med NTIA C + + kode på fem test tilfælde (ned under 0,01 dB).

ITM beregner tabet i normal atmosfære på den virkelige profil Copernicus GLO-30: ses med refleksion fra jorden, diffraktion (spigol og sfæriske jord vejet på grovheden af jorden Δh, det er forskellen mellem 10 og 90 ° percentil af kvoterne omkring ret af de mindste kvadrater) og troposcatter. Jordreaktiviteten reduceres til den gennemsnitlige del af ruten, $N_s = N_0\,e^{-h/9460}$, og giver krumningen af den tilsvarende jord,

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

Programmet bruger radioklimaet af den valgte rute som i § 10 sexies (indtil 1.3.4 altid det tempererede hav, med N0 320 på jorden og 350 på havet), med N0 fra den gennemsnitlige reaktivitet af stedet (jord εr 15, ≠ 0,005 S / m; hav εr 81, ≠ 5 S / m), horisontal polarisering og variation over tid uden dette sted (profilen er kendt). Det viser tabet overskredet i 50% af tiden og den, du har i 10% og 1% af den bedste tid. ITM modellerer ikke kanalerne: Variabiliteten over tid er statistisk efter klima. Det tjener som en anden beregning af baseline ved siden af P.452; forskellen mellem de to giver en idé om usikkerhed.

Den anden anvendelse er den sidste strækning efter en kanal, der slutter før den tilsvarende. Ved udgangspunktet placeres en virtuel antenne halvvejs gennem kanalen, og ITM beregner kanalen til den tilsvarende, som overskridelse over det frie rum ved 50% af tiden. Samlet tab

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,	heta_{condotto} + A_{ITM}(d_{uscita} 	o RX)
```

hvor A _ TX er koblingen af antennen (3 dB indvendigt, 1 dB hver 10 m udenfor), hvis ledningen starter tæt på, ellers er indgangen diffraktion af § 10 ter. Det er den grænse, som forfatteren har givet sig selv: kontinuitet i laget plus højst resterende dæmpning. Hvis det er lavere end det forventede tab bliver det mekanismen ("adfærd (exit og sidste strækning ITM)").

## 10 quinquies. Øer, stræder og jordbundsforstyrrelser

Et eksempel fra Napoli-bugten (Anacapri, JN70CN) mod det vestlige Middelhav viser, hvor ruter krydser øerne:

| Bestemmelsessted | Azimut | Hvor det krydser øerne | Hindringer |
| --- | --- | --- | --- |
| Barcelona |  279°  | nord for Sardinien (Gallura) til 41,05 ° N, mellem 420 og 490 km | ca. 30 km syd for Boche di Bonifacio; Limbara (1359 m) ca. 20 km fra linjen |
| Boche di Bonifacio |  282°  | den smalle til 41,3 ° N | ca. 11 km |
| Marseille |  296°  | Centralkorsika ved 42,2 ° N | bjerge op til 2700 m: en ledning under 2 km der stopper |
| Valencia, Balearerne |  260–265°  | CentralSardinien | interne relieffer |

For Barcelona er det nok til at pege til 282 ° i stedet for 279 ° fordi aksen passerer fra munden: 3., inde i strålen på en normal yagi.

Er munden en spænde? For de lave kanaler, under et par hundrede meter, ja: øerne er indhyllet, og over jorden, om dagen, varmen bryder omvendt. Selve strædet er bredt nok: omkring 11 km mod et første område af Fresnel, mindre end en kilometer bred, halvdelen af en 1000 km rute i 144 MHz. Problemet er vejret. The Mouths er en sjov for vinden: Det nederste lag accelereres og det nederste lag fjernes. Ofte passagen er åben som geometri, men forstyrret som atmosfære. Med en stabil anticyklon og lidt vind kan i stedet være den gode passage.

Jord- havforskellen. De tæller. En kanal dannet på den tyrrhenske kyst afbrydes: signalet kommer ud fra kanalen (afsnittet efter udgangen, § 10 quater) og skal vende tilbage til en anden kanal på havet af Balearics, betaler igen koblingen. Kanalerne i højden, over 1500 m, passerer over Gallura uden at indse det; for centrale Korsika gælder ikke.

I praksis: Hvis ruten krydser en bjergø, prøv en vis grad af korrektion mod strækket af frit hav, men ikke regne med passage med stærk vind i det smalle.

## 10 sexede. TN101 radioklima og tidsvariation (fra 1.3.5)

TN101 § 10 [25] beskriver, hvordan tabet af en rute ud over horisonten varierer i den lange periode omkring medianen, med forskellige kurver ved klima: Y (q) funktion siger, hvor mange dB tabet falder i q% af den bedste tid. ITM indeholder kurverne for syv klimaer. Klimaet tæller meget: på havet i det tempererede marine klima, på 144 MHz, i 10% af tiden er der mange flere dB end på jorden i kontinentalt klima, fordi kanalerne er hyppigere.

Programmet ikke længere løser klimaet: Han vælger det for alle ruter fra det gennemsnitlige punkt, så det virker over hele verden og ikke kun i Europa. Download fra Open- Meteo [26] tre års daglige værdier af ERA5 reanalyse (temperatur, fugtighed og jordtryk, regn) for cellen på 0,5 ° af det gennemsnitlige punkt, kun én gang; værdier forbliver i Data / CLIMA. Genoprettelse af jord

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

dag for dag, derefter det årlige gennemsnit og udflugt mellem månedlige gennemsnit, gennemsnitlig fugtighed og årlig regn. Reglerne følger beskrivelsen af klimaerne i TN101 § 10.2; grænserne mellem det ene klima og det andet er et kriterium for forfatteren:

- over 60 ° breddegrad: tempereret kontinentalt ( TN101 har ingen kurver for polært klima)
- mindre end 250 mm regn om året og gennemsnitlig fugtighed under 50%: ørken (Ns typisk 280)
- inden for 20 ° fra ækvator Ns på mindst 350 og udflugt op til 30: ækvator (Ns typisk 360)
- inden for 32 ° og en udflugt på mindst 60: subtropisk kontinentalt, dvs. monsooner (typisk udflugt 60- 100)
- mellem 10 ° og 32 ° med Ns på mindst 340: subtropisk marine (Ns typisk 370)
- begge horisonter på havet, over 20 °: tempereret hav på havet (det er definitionen af TN101);
- udflugt op til 30 i nærheden af havet (hav på ruten eller gennemsnitlig fugtighed på mindst 75%): havhærdet på jorden
- ellers tempereret kontinental (typisk udflugt 20-40).

N0 for ITM er gennemsnittet reduceret til havniveau, N0 = Ns · e ^ (h / 9460). Uden et netværk, en reserve regel anvendes med breddegrad alene og havet fraktion, og linjen siger det.

Test med data ERA5 2023- 2025: Capri Ns 339 med udflugt 45 (kontinentalt hærdet på jorden, hav tempereret, når begge horisonter er på havet); London 327 og 16 (hærdet stjerne); Wien 319 og 24 Abidjan 381 og 11 (ækvatorial); Miami 365 og 36 (subtropisk mand); Delhi 337 og 82 (subtropisk continental); Riad 278 og 37, 90 mm regn (ørken). I Chile: Antopagasta 341 og 19 (subtropisk mand: kysten i Atacama, med den næsten faste lave bagside af Stillehavet), Calama 232 på jorden, 2257 m, 14 mm regn (ørken), Valparaíso 331 og 11 (temperatursøm), Punta Arenas 313 og 5 (temperatursøm).

Linjen "Radioklima ( TN101)" viser klimaet, årsagen, Ns med halvdelen af udflugten og Y: hvor mange dBs tjener i 10% og 1% af tiden og hvor mange taber i 90%. Det er langsigtede statistikker og forbliver adskilt fra prognosen: det forventede tab følger tidspunktet for i dag med GFS profiler, og summen af de to ting ville tælle dobbelt så unormal formering.

## 11. Sammenligning med ITU- R P.452

Sammenligning linje P.452 anvender ITU metode for kanaler og refleksion på lag (P.452- 16 § 4.5 [6]) med ΔN medianen af GFS i stedet for den klimatologiske og p = 20% som en kalibreringskonstant, som i undersøgelsen CSIRO [10]. Det omfatter tab af empirisk kobling Alf under 0,5 GHz (ca. 28 dB til 144 MHz Det er kun en sammenligning: det forventede tab forbliver det, der er anført i punkt 9.

## 11 bis. Parabolsk ligning (fra 1.3.1)

Parabolic Equation Line er en fjerde beregning, kun af sammenligning, lavet som professionelle programmer for kanalerne (PETOOL; den bølge- formering ramme af M. Lytaev, MIT licens): i stedet for at estimere gevinsten fra lagene, det skrider bølgen langs ruten med Fourier split-step metode inde i M modificerede GFS refraktiv profil, taget fra prognosepunktet tættere på hver afstand. GFS-niveauerne lægges til de anslåede spring i de potentielle kanaler (beregning A, 50 m i halve lag). Jorden Copernicus GLO- 30 går ind i trin (under jorden er feltet null), jorden er dirigent og strålen af antennen er en Gaussian med den anslåede åbning fra gevinst. Beregningen ser kanaler, dem, der taber, diffraktion og kobling af antennen; Han kan ikke se troposcatter, som er tilføjet fra hinanden. Næste er "profilchecken": det samme med standard atmosfære mindre den med den forventede profil.

fra 1.3.2:

- Formulatoren er i bred vinkel, med den nøjagtige fase af det frie rum:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- bunden på havet og på kystsletten ved nul højde afspejler med koefficienten af Fresnel i horisontal polarisering, εc = εr − j · 60 · λ (hav εr = 81 og ≠ = 5 S / m; land 13 og 0,006 S / m), i den klare vinkel af ruten:

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- havenes hårdhed reducerer den konsekvente refleksion ifølge Miller og Brown, med den effektive højde af bølgerne fra vinden til 10 m (σh = 0,0051 · U2):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- på havet omfatter profilen M fordampningskanalen (punkt 9 ter) med den neutrale logaritmiske profil:

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

Det tester med prototypen (10 m antenne på havet, gevinst på standard atmosfære på 100- 300 km): med δ = 10-30 m fordampningskanalen giver 1-3 dB til 144 MHz, 8-37 dB til 432 MHz og 21- 66 dB til 1296 MHz. Ved 144 MHz i horisontal polarisering havet afspejler næsten som et metal (= 124; η Note 124; mere end 0,99 til et par tiendedele grad) og ruhed tæller lidt: den nye bund ændrer et par tiendedele af dB; det tæller mere fra 432 MHz op.

Grænser: over jorden forbliver den lodrette profil GFS-niveauet, så de tynde kanaler forbliver usynlige; jorden til trin er tilnærmet; strålen forbliver inden for 6th.

## 12. antenner, signaler og støj

- Yagi DL6WU: gevinst 9,5 + 7,8 · log10 (boom / λ) dBd (2,35 dB for dobbelt boom, DL6WU foranstaltninger på VHF Communications 3 / 1982) plus 2,15 for [12] dBi.
- EIRP = strøm − kabel + TX gevinst.
- Signaler modtaget = EIRP − forventet tab + gevinst RX − kabel RX. S- meter IARU: S9 = − 93 dBm, 6 dB pr. enhed.
- Ekstern støj ifølge ITU-R P.372 [13]: galaktisk (Fa = 52 − 23 log f) mere kunstig i henhold til miljøet (by, bolig, landdistrikter, stille landdistrikter) eller en håndskrevet antenne temperatur.
- Tsys = Ta + Tpre + [290 (Lc − 1) + 290 (Fr − 1) · Lc] / Gpre til antennens terminaler (Friis formel). Fra 1.3.5 har modtagerstationen altid en preamp i antenne, fast parameter: støjnummer 0,8 dB (Tpre = 59 K) og opnår 20 dB. Bag der er kabel (tab Lc) og radio, med grundlæggende Fr støj nummer 6 dB. Med 1 dB kabel falder kabel og radio bidrag fra omkring 1160 K til 12 K: støjen bestemmer himlen, miljøet og preamp. Gevinsten på 20 dB er en typisk værdi valgt af programmet.
- S / N i SSB, CW og FT8 i båndet på hver vej i forhold til den fastsatte tærskel.
- Sandsynligheden for perioden (fra 1.3.2): Forandringen inden for timen er en normal fordeling af S / N omkring den forventede gennemsnitlige timesats. Bray (QST, november 1961, s. 39, på kurverne i Norton 1959) giver en amplitude på 13,5 dB over ca. 100 miles, dvs. omkring 7 dB under og 7 over gennemsnittet, som næsten falder lige under 100 miles (3,5 dB til 50 miles); for en "solid" forbindelse tager det 7 dB fra budgettet. Taget ± 7 dB som ± 2Ω: ≠ = 3,4 dB over 160 km, proportionalt med afstanden nedenfor. Sandsynligheden for, at en periode overskrider tærsklen for vejen er

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  med Φ normal fordelingsfunktion: margin 0 dB = 50%, + 3,4 dB = 84%, + 7 dB (Bray 's' faste 'margin) = 98%. Den omfatter ikke usikkerheden ved modellen, som er større.
- Digitale måder: S / N i 2500 Hz sammenlignet med tærskelværdierne målt ved 50% afkodning uden forudgående oplysninger (AP). FT8 − 21 dB og FT4 − 17,5 dB om hvid støj; når mekanismen er troposcatter bruge tærskelværdierne med evanescens "forstyrret breddegrad medium", FT8 − 18,6 dB og FT4 − 15,2 dB [21]. Q65 - 15C − 22,2 dB, Q65 - 30B − 24,8 dB, Q65 - 60A − 27,6 dB I fly scatter Doppler kan overstige tonerne af Q65 -60A (1,7 Hz): der er bedre end 30B eller 15C. JT65A − 25 dB [22]. Den afkodning AP (navn på den allerede kendte eller slutningen af QSO) giver omkring 2 dB ekstra for FT8 og 1,5 til 2,6 dB for Q65; det indtaster ikke tærsklen, fordi det ikke er gyldigt for alle de "-24 dB" værdier, der ofte læses for FT8 er grænsen for WSJT- X relationer eller AP dekodning, ikke tærsklen på 50%. [24]

## 13. Response og datatider

Hver beregning måler sin egen tid: GFS-profiler (netværk, analyse, obligatoriske pauser på 20 s mellem partierne for Open- Meteo grænse), Copernicus GLO- 30 terræn (med download af ruder, 15- 30 MB hver, kun første gang), beregning. The Response Time linje opsummerer dem; i registret rækkerne begynder med SCI. Ved opgradering af nettet, registret også rapporterer GFS race anvendes, når det blev tilgængelige, og hvor gamle data er.

Fra 1.3.2 kan du også vælge ICON- 2I, modellen af agenturet ItaliaMeteo og Arpae på Italien på omkring 2,2 km, gennem Open- Meteo (model italia _ meteo _ arpae _ icon _ 2i, to løb om dagen, 3 dage). Det dækker kun Italien og de nærliggende have og har mindre lave niveauer af GFS (1000, 925, 850, 700, 500 hPa): for kanalerne i højden er ikke bedre end GFS, men den fine gitter beskriver godt kyster, øer og hav på 2 m, det er fordampningskanalen. De variabler, som en model ikke har (strate grænse, temperatur af overfladen...) fjerne sig fra anmodningen. Fra 1.3.2 bruger den automatiske kilde kun NOMADS; Open- Meteo tjener, hvis NOMADS ikke reagerer, og i historiske tilfælde. Med "Opdatering af sig selv" feltet (oplyst af defekt) prognosen er uklikket: ved åbningen, ved hvert nyt GFS løb på NOMADS (omkring 5 timer efter tidspunktet for løbet) og når den synlige zone kommer ud for mere end en fjerdedel fra nettet; på de fleste et forsøg hver 15 minutter, aldrig med en historisk sag.

Anmodninger hos Open- Meteo har grænser pr. minut, pr. time og pr. dag for hver internetadresse. De downloadede profiler gemmes på disken (Dati / cache- gfs) for hvert slag og model: med samme løb, som ændres hver 6. time, spørger de allerede downloadede punkter ikke igen. Med den automatiske kilde starter du fra NOMADS og Open- Meteo tjener kun for manglende punkter; med den eneste Open- Meteo, på tidsgrænsen eller dagligt stopper med det samme og siger hvornår du skal prøve igen. Historiske sager behandles ikke.

## 14. Validér modellen

Prognosen bør sammenlignes med reelle forbindelser. Beregningsloggen holder hvert nummer; programmet arkiv holder stedet og afkodning. For 432 og 1296 MHz, hvor pletterne er få, har de brug for rapporter fra operatørerne: hvem lavede forbindelsen, til hvilket tidspunkt, med hvilket signal. Sammenlign i mange timer den forventede gevinst med det målte signal siger, hvis tærskelværdierne skal flyttes.

## Kilder

1. NOAA / NCEP, Global Prognose System (GFS), global model ved 0,25 °, fire løb om dagen.
2. Open- Meteo, forudsigelse API (open- meteo.com), gfs _ seamless model, variabler på tryk niveauer og racing metadata.
3. Copernicus DEM GLO-30 (ESA; WorldDEM © DLR e.V. 2010- 2014 e © Airbus Defence and Space GmbH 2014- 2018, leveret som en del af Copernicus af Den Europæiske Union og ESA), 1 anden bue, GeoTIFF rammer på det åbne register over Amazon Web Services. Reserve: NASA / USGS Copernicus GLO- 30 i Terrain fliser "skadi" af AWS.
4. ITU-R P.453, Radiobrydningsindekset: dets formel og brydningsdato med digitale kanalkort (Study Group 3, © ITU, personlig og ikke-kommerciel brug).
5. B. R. Bean, E. J. Dutton, Radio Meteorology, NBS Monograph 92, 1966: Tabel 6.5 og § 4.4.
6. ITU-R P.45216, Prognose procedurer for vurdering af interferens mellem stationer på jordens overflade ved frekvenser over ca. 0,1 GHz: § 4.2 (diffraktion), eq. 45 (troposcatter), § 4.5 og eq. 49 (udført).
7. ITU-R P.834, Effects of troposfæric refraction on radiowave formering.
8. ITU-R P.525, Beregning af free-space dæmpning.
9. ITU-R P.526, Formering ved diffraktion: eq. 31 (cutcheon) og § 3 (sfærisk jord).
10. B. Indermuehle, H. Suzuki, Troposfærisk Ducting forudsigelse baseret på GFS Model Data på Inyarrimanha Ilgari Bundara, CSIRO Murchison Radioastronomi Observatory, Publikationer fra Astronomisk Society of Australia, 2026 (arxiv 2608.12861).
11. D. E. Kerr, Propagation of Short Radio Waves, MIT Radiation Laboratory Series vol. 13, 1951: eq. 17 e 18.
12. G. Hoch DL6WU, "Extremely Long Yagi Antennas", VHF Communications 3 / 1982, s. 130 ff., fig. 1 (målt 432 og 1296 MHz i henhold til boomets længde).
13. ITU-R P.372, Radiostøj: galaktisk og kunstig støj for miljøet.
14. NOAA / NCEP, NOMADS, GRIB-filteret GFS 0,25 ° tid (nomads.ncep.noa.gov); WMO G RIB2 -format, kompleks pakning med rumlige forskelle.
15. Open- Meteo, Historical Prognose API (modelprognose arkiv fra omkring 2021).
16. LukiBox, TropoLink, design af troposcatter ruter med P.617, NBS TN101 og ITM flanked (GitHub, Apache 2.0 licens).
17. ITU-R P.617- 5, Formeringsforudsigelsesteknikker og data, der kræves til udformning af radiorelæsystemer over horisonten, 2019.
18. RSGB, VHF / UHF Handbook; DL2FZN implementering (andi- f / troposcatter on GitHub, 2013).
19. W. F. Moler, D. B. Holden, "troposfærisk spredning og atmosfærisk cirkulation", Journal of Research of the NBS 64D, 1960, doi 10.6028 / jres.064d.014: signal uden for horisonten (1365 og 9380 MHz, på havet) og lodret bevægelse af luften.
20. ARRL Handbook 2022 (99a ed.), § 19.4.3-19.4.4: Troposferisk vejr og formering.
21. S.Franke, B. Somerville, J. Taylor, FT4 og FT8 kommunikationsprotokoller, QEX juli / august 2020.
22. W3SZ, digitale slow mode tærskler ( JT65, JT9, QRA64) og mode valg efter formering, 2019.
23. QST, "Verden over 50 MHz", november 2022: åbning af 14-15 august 2022 mellem Manitoba og Kansas / Missouri.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Quick- Start Guide til Q65, 3. april 2021 (wsjt.sourceforge.io).
25. P.L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis, Transmission Loss Predictions for Troposfæric Communication Circuits, NBS Technical Note 101, magasin 1967: § 9 (troposcatter, funktion F (θd), H0), § 10 og bilag III (radioklima og variation over tid).
26. Open- Meteo, Historical Weather API (ECMWF ERA5 gennemgang siden 1940; Copernicus Climate Change Service, CC BY 4.0 licens).
