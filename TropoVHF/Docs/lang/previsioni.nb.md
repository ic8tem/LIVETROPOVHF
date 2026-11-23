# Prognosmanual

> Automatisk oversettelse av italiensk: i tilfelle tvil det er verdt den italienske guiden.

Fanen Prognose beregner den tropoferiske utbredelsen i seg selv: et indekskart på det synlige området og signalet som er gitt mellom stasjonen og en tilsvarende. Denne manualen beskriver alle data, hvordan det beregnes og hvilken kilde det kommer fra. Tallene i firkantet parentes refererer til kildene i bunnen; i programmet hver data av resultatene har en tegneserie (simbolo i) med samme kilde og referanse til avsnittet.

## 1. Hva han gjør og hva han gjør

Forutsigelsen av GFS [1, 2] -modellens vertikale temperatur- og fuktighetsprofiler stammer fra reaktiviteten til luftlaget etter lag og anslår hvor mye signalgevinstene i forhold til en normal atmosfære. På ruten legger den virkelige bakken (Copernicus GLO-30 [3]), diffraksjon, troposcatter, kanaler og mottakerstøy.

Det er ikke et mål: det er et estimat på en værmodell med fjerne nivåer hundrevis av meter. Det tjener til å sammenligne situasjoner (i dag bedre enn i går, 70 cm bedre enn 2 m, dette behandler bedre enn det), å ikke garantere en forbindelse. Nr. 14 forklarer hvordan man samler inn bevis for å validere det.


> "Forbud kan ikke generaliseres, men studeres i forhold til lokal morfologi. »
> — Volker Grassmann, DF5AI, rapportert av Flavio Egano, IK3XTV (Propagasjon på 2 meter)

Det er prinsippet som styrer punktpunktberegningen: hver rute beregnes på jorden, med sin horisont, havet og dens relieffs; kartet gir det generelle bildet, behandler svaret.

## 2. Språkkommandoer

- Band: 144, 432 eller 1296 MHz. Kart og beregning bruk det valgte bandet.
- Nå: markøren flytter prognosen fra nå til 72 timer fremover, innen 3 timer.
- Oppdateringsprognose (synlig sone): Last ned GFS-profilene på et rutenett på de fleste 240 punktene som dekker det synlige kartet. Rutehøyden (0,5° eller mer) avhenger av forstørrelsen.
- Stopp: avbryte nedlasting eller beregning.
- Detaljer å vise (høyre panel på kartet): tre nivåer, Essential, Normal og Complete, slå på en gruppe bokser; hver boks kan deretter endres for hånd. Boksene er i tre grupper:
  - På kartet: isolineee del Gain, også 3–7 dB (trekking), isobare, termisk inversjon, værtegn S ↓ ↑, korrelasjonsgevinst/trykking, sone med direkte syn (nr. 4, 5, 7 og 9 bis). Fargene blir alltid.
  - I beregningen av signalet: alle linjer i beregningen, flat jordprofil, beregningslogg og detaljert fillogg.
  - Korreksjoner og live data: værkorreksjoner (Misme, subsidens), luft-sjø kontrast, gjennomsnittlig med flekker (corridoi), APRS 2 m hopper og LoRa, over fjellet uten tap (nr. 4 bis, 9 bis, 9 quater og 10 bis).
  - Viktige holder bare isolines, vær og media rettelser med flekker; Normal legger til isobar, inversjon, vær tegn, direkte utsikt, flat jord, air-sea og APRS; Komplett slår det på. Nivået endrer kun det du ser og hvilke rettelser som inntastes: beregningene forblir de samme.
- Kilde til data og historiske tilfelle: fra hvor profilene kommer og hvilken dag du ser (nr. 3 bis og 3 ter).
- Min stasjon og korrespondent: Locator, antenner, strøm, kabler, støy (nr. 7 og 12).
- Beregn signal og dekning i optikken: de to beregningene på bakken (nr. 8-13).
- Beregningsregister: hvert trinn med tallene som brukes, inkludert i filen Data/ prognose-calcoli.log.

## 3. Fra GFS-data til profil

For hvert punkt i nettet og hver time leser vi trykket på bakken, temperatur og fuktighet på 2 m, vinden på 10 m, høyden på grenselaget, CAPE og temperatur og fuktighet i forhold til 1000, 975, 950, 925, 900, 850, 800, 700, 600 og 500 hPa. Fra 1.30 kan du også lese vinden til 850 hPa og vertikal hastighet til 850 og 700 hPa (nr 9 bis).

Nivåene beregnes med den ipsometriske formelen og den virtuelle temperatur; nivåene under bakken er knapt. For hvert nivå:

- N = 77,6/T · (P + 4810·e/T), med T i kelvin, P og i hPa, og ved metningstrykket til ITU-R P.453-14, ekv. 9, med EF-korreksjonsfaktoren (omstyrt på den offisielle teksten) [4].
- M = N + 157·t, med time i km: modifisert reaktivitet. Hvor M går opp er det en kanal.

## 3 bis. Datakilde

Menyen Datakilde velger hvor profilene kommer fra:

- Automatisk (anbefalt): GFS først fra NOMADS [14], NOAA-tjenesten som distribuerer modellen: for hvert øyeblikk spør programmet to cutouts av området i G RIB2 -format og leser dem fra seg selv; NOMADS har ingen daglig grense, bare ett minutt som programmet respekterer. Med brede rutenett (fra 0,5° til opp) bruker du filer til 0,5° eller 1. hver 3 timer, mye lettere. De punktene som NOMADS ikke gir, eller alle hvis NOMADS ikke svarer, spør Open-Meteo [2]. De to kildene gir samme modell og samme løp, slik at du kan blande i samme rutenett.
- GFS fra Open-Meteo: bare Open-Meteo, som i 1,2 (ved grensen per minutt er forventet).
- GFS fra NOAA NOMADS: Bare NOMADS. Langsommere å laste ned (to filer i timen, et halvt sekund mellom ett forespørsel og det andre å ikke overstige NOAA-grensen), men bruker ikke Open- Meteo-grensen.
- ICON-EU (DWD) og ECMWF IFS 0,25°, fra Open-Meteo: andre modeller enn GFS, som gjelder i hele rutenettet og for rutene. De blander seg ikke med GFS: Sammenlign to modeller på samme tid er en enkel måte å se hvordan prognosen er usikker på. ICON-EU dekker bare Europa; ECMWF har ikke alle GFS-trykknivå (manglende nivåhopp) og har kanskje ikke grenselaget.

Kilden som brukes er skrevet i statuslinjen og i den første linjen av resultatene (datakilde), hvor mange poeng kom fra hver tjeneste og hvor mange fra cache. Disk cache er separert etter modell.

## 3 ter. Historiske tilfeller

Historisk En dokumentert åpning med Open-Meteo [15] -arkivet GFS-prognoser: kartet beveger seg til sakens område, bandet setter seg selv og tidsmarkøren forlater fra den første dagen i saken. Ved å trykke på Oppdater prognose laster du ned profilene i de dagene; signalberegning fungerer som for gjeldende prognose. Det tjener til å sammenligne modellen med det som virkelig skjedde.

Det første tilfellet er den 14.–15. august 2022 fortalt i QST (20. november 2022, " Verden over 50 MHz" [23]): en åpning under et høyt trykk mellom Manitoba og Kansas/Missouri, fra 144 til 1296 MHz nesten alle i FT8, med den kanadiske rekorden på 10 GHz på 1228,56 km i Q65 og signaler opp til 1941 km. Den andre er åpningen mellom 29. desember 2021 og 2. januar 2022 på Tyrrensk og det vestlige Middelhavet på 144 MHz, fra rapporter fra IC8TEM: første åpninger på 29 (opp til 737 km), bredere på 31 ( IT9GSF – EB5EA 1217 km), stor åpning den 1. januar mellom Italia, Sicilia og Spania. I eldre tilfeller (f.eks. september 2000) har Open-Meteo-arkivet ingen data. I arkivet mangler høyden på grenselaget: der dets rettelse gjelder ikke.

## 4. Indeksen og fargene

For hvert lag mellom to nivåer sammenligner programmet N-fallet med terskelverdiene i tabellen 6,5 i Bean og Dutton [5]: f.eks. mellom 1000 og 850 hPa er en reduksjon opp til 60 N/km normal, over 100 N/km er super-fraksjon. Et super-refraktivt lag er verdt 10 til 15 dB gevinst; en ledning som fanger båndet fra 20 til 25 dB i henhold til underskuddet til M. Sterk vind til bakken, høygrenselag og høy CAPE reduserer gevinsten, fordi de fjerner eller bryter lagene [5, § 4.4].

Fargen er den estimerte gevinsten i dB, med en fast skala lik i alle temaer:

| Gain | Farge | Betydning |
| Under 7 dB | Ingen. | Nesten normal atmosfære |
| 7-10 dB | blå | gradient over normal |
| 10–13 dB | grønn | super-refraktivt lag |
| 13–16 dB | gul | sterk lag, men uten kanal for band |
| 16-8,5 dB | oransje | meget sterkt lag eller for tynn kanal |
| 18,5–21 dB | rød | Conduit Trapping Band |
| fra 21 dB | Lilla | Sterk og dyp kanal |

Med visningsboksen også 3–7 dB-soner mellom 3 og 7 dB, hvor noe blir dannet, men forblir under skalaen, kan du se med en lys grå tilbakeholdenhet; farger endres ikke. En prikket rute avgrenser området som dekkes av sin egen prognose og viser den maksimale synlige gevinsten: hvis det er under 7 dB, forblir kartet uten farger fordi atmosfæren er nesten normal, ikke fordi dataene mangler. Når du laster ned et nytt klikk på oppdateringsvarsel avbryter du ikke; for å stoppe det er det stopp.

Fargene er ugjennomsiktige til 82%, så bakgrunnen for temaet bare endrer dem. Locator rutenettet og kvadratnavnene er designet over fargene. Der din prediksjon ikke er der, men Hepburn eller F5LEN gir 5 eller flere sjakkkort vises med fargen på den kilden.

Kartet dømmer alle poeng alene. Signalberegningen kontrollerer i stedet at laget er kontinuerlig langs ruten (nr. 9): en rute som krysser meget gult kan gi mindre enn kartet får deg til å tenke.

## 4 bis. Prognos mediert av reklamer: korridorer (fra Kinshasa)

Copyright Policy: En link alene må aldri tegne en kanal. En isolert og kort resepsjon kan være spredningen av et transittplan (APRS-pakken er svært kort), en åpning på noen minutter eller en feil, og vil gi - dråpeledende - de bedrager. Programmet gjenkjenner en korridor når, i samme band:

- minst 3 par uavhengige stasjoner (ingen delt stasjon) arbeider på ruter på minst 300 km;
- rutene er parallelle: azimut innenfor ±15°, gjennomsnittlige punkter innenfor 150 km fra fellesaksen;
- alle i løpet av de siste 30 minuttene, med strålen som gjentas i avstand av minst 10 minutter (peristens);
- for ARS, et hopp teller bare hvis samme hopp gjentas;
- Fra Copalia teller rutene over 1500 km kun dersom strålen kommer fra minst to forskjellige kilder (f.eks. PSKReporter og en klynge): fra en enkelt kilde til disse avstandene i 2 m, er oftere EME, scatter meteor eller feil lokatorer enn tropo.
- fra Copernicus, ikke tell stasjonene 'til stråle': som i den siste timen har minst 5 korrespondenter over 1000 km spredt over en vifte på minst 150°. I 2 m er trope retningsbestemt; en mottaker som " fri" sammen Israel, Portugal, England og Italia fra 2000–3700 km, ofte med positive signaler, har nesten sikkert frekvensen konfigurert dårlig. I den 17–22/11/2026 I5MZY arkiv 72% av overnatting i 2 m over 1000 km kom fra stasjoner så; på 22/11 på 10:53 UTC var OH6HLH, OZ1ITS, F4LUK, OM8ABC og SP2JLR, og med filteret den lengste korridoren ned fra 2175 km (regole Zawinski) til 615 km.

Kilder: klyngeflekker (ikke de merket AS/ACS), PSKReporter via MQTT, deres QSO og WSJT-X/JTDX/MSHV dekodinger, og APRS hopper i 2 m og LoRa 433/868 MHz.

APRS: 2 m og LoRa. Programmet leser APRS-IS i skrivebeskyttet (rotate.aprs2.net, filter på Europa) og trekker radio hopper mellom kjente plasseringsstasjoner (kilde → Digipeater → IGate), som VHF-kart over DXView og APRS PropView (RF-YVY, Apache 2.0). Tre band er verdt: 2 m, LoRa 433 MHz (båndkorridorer 432) og LoRa 868 MHz (corridoi som teller for 1296: en kanal som gir 868 MHz også de høyeste frekvensene). Bandet kommer fra LoRa tocall (aprs-deviceid-listen) og fra frekvensene eller ordene i kommentaren; et hopp er bare verdt mellom to stasjoner av samme band.

Det delikate punktet er de aktive repeaters: Et signal som mottas på 500 km kan ankommes med to eller tre gravipeater retransmissions. Et hopp teller derfor bare hvis det er mellom to ekte og påfølgende sendere:

- stien må spores: hvert Digipeater som har gjensendt har satt sitt navn, og hoppet måles mellom den ene og den neste; et alias som brukes uten navn (W IDE1 *, W IDE2 *, regionalt alias) bryter kjeden og som strekker seg ikke teller;
- skjulte tilbakesendinger: Programmet husker banen hver kilde vanligvis spør (for eksempel W IDE2 -2). Hvis en pakke kommer med et sprang i mindre (W IDE2 -1) og ingen navn forklarer det, sendte et gravipeater igjen det uten sporing og pakken kastes; kildene aldri sett før teller;
- Digipeater i midten av veien: Hvis mellom de to stasjonene, mer eller mindre på veien, er det en Digipeater av det samme aktive bandet de siste tre timene, hoppet kastes, fordi det kunne ha blitt sendt på nytt (det er spesielt for Digipeater LoRa som sender tilbake uten å endre banen);
- ikke-radiopakker (qAC, qAX, TCPIP), de som drives av Internett på radio fra en IGate (tredjepart), HF og forskjellige frekvenser, stasjoner i en høyde på mer enn 1500 m (palloni, fly) og hopper over 1500 km.

Effekt på kartet (fra Copalia). Den trekker bare det område hvor den minste garanterte er gyldig: et bånd på ± 60 km rundt den sentrale delen av hver rute (fra 15 til 85% av dens lengde), ikke på stasjoner i endene, hvor kanalen ikke kan være der. Bandene i alle korridorer trekkes sammen, i en nøytral farge (teksten) og halvgjennomsiktig: overlappingene tilsettes ikke, og fargen forveksles ikke med den lilla i skalaen (21 dB og videre). Etikettene (traits, gjennomsnittlig avstand, minste gevinst) skannes med hverandre; passerer med musen på en etikett deler av korridoren vises.

I området, og bare nær gjeldende tid (±90 minutter) faller ikke værvarselet under et minimum. Begrunnelsen er: Hvis stasjonene virkelig passerer, er spredningen der, uansett hva værmodellen sier.

Fordi de korte rutene er verdt lite (fra Swiss). I 2 m, mellom 300 og 600 km, forbinder to gode stasjoner (somme hundre watt, antenner, FT8 eller CW) nesten hver dag selv med den normale atmosfæren, for troposcatter: signalet diffus av de små uregelmessighetene i luften i det felles volumet av de to buntene. Tre eller fire steder på 500 km sier at bandet fungerer som vanlig, ikke at det er en åpning: GEKO Magazine n. 69 ( IU3QEZ) estimerer for troposcatteren med 2×10 flytende elementer på ca. 440 km i SSB QRP, 510 km i SSB med 50 W, 600 km i CW QRP og 640 km i SSB med 500 W (kilder, elleve runde). Bare over 600–700 km er troposcatteren ikke lenger nok og en serie parallelle forbindelser indikerer virkelig en kanal eller lag. Opp til Perjeta en korridor på 450–600 km hevet prognosen til 9–12 dB, og kartet viste en forbedring som ikke var der.

Startverdien går deretter med gjennomsnittlig avstand av rutene:

- opp til 400 km: 3 dB;
- fra 400 til 700 km: salt på lineær måte fra 3 til 10 dB (500 km → 5,3 dB; 600 km → 7,7 dB);
- fra 700 til 900 km: fra 10 til 16 dB; over 900 km forblir på 16 dB.

Under 7 dB, som er under begynnelsen av skalaen, er minimum at: korridoren er tegnet med sin etikett, det ses at bandet fungerer, men fargene på kartet endres ikke (med boksen viser også 3-7 dB den grå prikkete). Over 7 dB veies verdien, halvparten og halvparten, med antall uavhengige ruter (3 slag → 0,25; 6 eller mer → 1) og med medianen S/N av relasjoner (−20 dB → 0,5; 0 dB eller mer → 1; ikke kjent, som i klyngeflekker, → 0,75), fra 7 dB. Eksempler: 3 ruter uten S/N på 500 km → 5,3 dB; 5 ruter på 615 km → 7,7 dB; 3 ruter uten S/N på 900 km → 11,5 dB; 3 ruter til −20 dB på 900 km → 10,4 dB; 6 ruter til 0 dB på 900 km → 16 dB.

Den minste avstand forblir 300 km: å heve den til 500 km ville også ha fjernet de virkelige kortdistanseåpningene (for eksempel en kanal på Tyrrenian på 400 km, som gjenkjennes ved værvarsel og svært sterke signaler mer enn fra avstanden alene). I I5MZY -arkivet var de lengste korridorene 615 km (7,7 dB) og 540 km (6,3 dB); med reglene for Xavier nådde den 2175 km og 16 dB.

Fram til Perjeta var det minste gyldig på alle korridorens rektangel og var bare avhengig av distansen (fra 10 dB til 300 km til 16 dB til 900 km): med mange lange korridorer sammen (f.eks. på 22/11/2026 løftet tolv korridorer opp til 2620 km) nesten hele Europa til 16 dB.

Gvinstkurvene trekker ikke lenger «gocce»: de mindre øyene på 220 km i begge retninger forblir farget, men uten lukket kurve, med mindre en korridor bekrefter dem. Den slås av med " Prevision mediert av punktene".

## 4 ter. Live sensorer: FM-DX webserver og AIS mottaker (fra 1.4)

To ytterligere kilder til reelle observasjoner, som går inn i korridorene til båndet på 2 m og dommen av pålitelighet (§ 10 septies). De slår på og slår av i fanen Detaljer.

Webserver FM-DX. Mange eksterne mottaksentusiaster i FM (88–108 MHz) holder en Internett-tilkoblet mottaker på FM-DX Webserver gratis program (GPL-3.0); Den offentlige listen er servers.fmdx.org [28]. Hver server sier, på siden /api, den avstemmte frekvensen, signalet i dBf og, hvis det dekodet RDS, senderen gjenkjent av databasen med avstand og azimut. Programmet:

- Last ned listen hvert 30. minutt og hold de 60 nærmeste online servere innen 1500 km fra QTH;
- Forhører dem én om gangen, spre seg over 3 minutter, med maksimal tid på 8 sekunder (ingen server mottar mer enn én forespørsel hvert 3. minutt);
- holder en mottakelse når senderen er 150 km eller mer og signalet er minst 10 dBf; posisjonen til senderen er avledet fra serveren, azimuten og avstanden.

FM-båndet er lavere enn 2 m, og kanalene som bærer det ikke alltid bringe 144 MHz; men en FM-mottak ved 300–600 km utenfor horisonten er nesten alltid tropo (eller om sommeren E sporadic, som ved 100 MHz når utover 900 km: for dette utover 1500 km spør korridorregelen to forskjellige kilder, § 4 bis. På kartet er resepsjonene lilla linjer " FM n km"; i staten bar tellingen, med listen i tegneserien.

AIS fra mottakeren. Skip overfører sin posisjon ved 161 975 og 162 025 MHz (ITU-R M.1371 [31]), nesten samme frekvens som 2 m. En AIS-mottaker (AIS-fanger, rtl-ais, en dedikert mottaker) som videresender frasene NMEA! AIVDM i UDP på port 10110 forvandler hvert skip bort til en kanalsensor: programmet dekoder posisjonsmeldinger (type 1, 2, 3, 4, 18, 19 og 21) og sammenligner avstanden med den normale radiohorisonten

```math
d_h = 4{,}12\,(\sqrt{h_{nave}} + \sqrt{h_{rx}})\ \text{km}
```

med høyden på antennen til skipet estimert fra typen (20 m for lasteskipene, 30 m for passasjerene, 6 m for fiske og nytelse, 10 m for de andre) og antennen til mottakersettet (grunnleggende 10 m). Et skip mer enn 1,5 ganger horisonten og mer enn 50 km er en mottakelse ved kanal: det vises på kartet som en blå linje fra QTH med et brøl «AIS n km». Det er den samme ideen som CSIRO [10] -studien, som brukte 162 MHz AIS-mottak til å verifisere forutsigelsen av GFS-rørledninger. Det er slått av grunnleggende: det tjener en riktig mottaker.

## 5. Valgfrie linjer og sammenligning med trykk og inversjon

Gain kurver: linjer ved 7, 10, 13, 16, 18,5 og 21 dB, kantene på skalaen, som er trukket på feltet glatt med fremgangsmåten til marsjeringsfirkantene, med etiketten i dB.

Isobare: trykk på havnivå hver 4. fra samme GFS, til timen av prognose, med sentrene H og L.

Termisk inversjon: kurver dekket med hvor mye luft ved 925 eller 850 hPa er varmere enn det ved 2 m, ved +1, +3, +5 og +8 °C.

Under kontrollene gir en linje gevinstavtalen med trykket og med det motsatte på den synlige sonen: Pearson korrelasjonskoeffisient, -1 til +1. Kanalene er ofte født i antisykloner (luften som faller varme og danner omvendt), så en positiv avtale med trykket er normal; men i samme antisyklon endres gevinsten mye mellom sjø, kyst og fjell. Inversjon alene er ikke nok: bøy bølgene bare hvis over luften er selv tørketrommel [5]. Linjen tjener til å se, nå for nå, hvilken av de to beste forklarer de fargede områdene. Samme sammenligning er skrevet i registeret.

## 6. Potensielle rørledninger: dobbel beregning

GFS-nivåer varierer fra 200 til 1000 m: en tynn inversjon spres og modellen ser ikke kanalen. Dersom mellom to nivåer N-reduksjonen overstiger det normale (40 N/km), er luften stabil (termisk gradient under 3 K/km) og over er tørr (spesifikk fuktighetsforhold under 0,8), vurderer programmet en potensiell kanal og beregner den på to måter:

- Beregning A: alle konsentrert overskuddsfall i 50 m.
- Beregning B: den samme reduksjon fordelt på den typiske tykkelsen av rørledningene i det området, tatt fra klimakartene til ITU-R P.453 [4] (på bakken eller forhøyet).

For underskuddet og for den maksimale innestengte bølgelengden holdes den laveste verdien: det er et fornuftig estimat.

ITU-R P.453 data er © ITU og kan brukes til personlige og ikke-kommersielle formål, som refererer til kilden, men kan ikke distribueres. Hvis du vil slå på B-beregningen, laster du bare ned P.453-kartene fra ITU i fanen: programmet, etter å ha vist bruksforholdene, last ned arkivet til rørledningene fra ITU-området (elevated_and_surface_duct.zip) for brukerens personlige bruk, trekk ut filene i Data/ P453 og slå på beregningen uten å omstarte. Alternativt lastes ned fra ITU-R-studiegruppen 3 digitale produktsiden (itu.int, ITU-R Studiegruppe 3, Digitale produkter, P.453, Overflatedata og forhøyede kanaldata) og setter de syv S_OCCUR. TXT, S_THICK.TXT, S_STREN. TXT, E_OCCUR.TXT, E_base.T Uten filene bruker programmet bare beregning A og skriver det i fanen og i registeret. Forholdet mellom underskudd A og typisk intensitet P.453 er anomalisk: Hvor mange ganger dagens rørledning er sterkere enn normalt. P453

## 7. stasjoner og lokatorer

Leieren kan ha 4, 6, 8 eller 10 tegn. Med 8 eller 10 tegn er posisjonen nøyaktig og jordandelen kommer fra Copernicus GLO-30: høyden på antennen er nok fra bakken. Med 4 eller 6 tegn er firkanten for stor: det er bedre å skrive kvoten av antennen på havet; hvis 0 programmet forblir estimatet med SRTM-jorden i midten av kvadratet pluss høyden fra bakken og signalerer det i resultatene. For den tilsvarende standardhøyden er 3 m fra bakken; med havandel 0 brukes SRTM jord pluss høyde.

## 8. Land, diffraksjon og troposcatter

Profilen til ruten prøves på Copernicus GLO-30 hver 30. m (mer enn 120 km bred). Jordens radius ekvivalent er k = 157/(157 + ΔN), med ΔN nedgangen av N i første km fra GFS profilene langs [6, 7] ruten.

Relieff nær stasjoner (fra ICAO, forfatterens kriterium). P.452 betaler horisontvinkelen lineært, 0,573 dB per mrad: en relieff på noen få titalls meter fra antennen koster titalls dB, mye mer enn observert. Av denne grunn kan relieffene innen 20 km fra hver stasjon tilsettes opp til 6 dB: horisontvinkelen på grunn av dem er begrenset til den av de mest fjerne relieffene pluss 10,5 mrad. Det er gyldig for stasjonen og korrespondenten. Linjen \"Nære lindringer\" sier hvor mange dBs har tatt av.

Landekilde (fra jämt). Kvoter kommer fra Copernicus DEM GLO-30 [3]: 1 sekund boge, havet er 0 og kysten er klar. Inntil 1.10.2013 brukte programmet Terrain Flisers "skadi" av AWS. Nær kysten blander de firkantene SSRTM med lav oppløsning havbunnsdata og sprer øyas andel på havet: rundt Capri skaper en spøkelsesring på 80–100 m mellom 0,5 og 1 km fra stranden. 21. november 2026 IC8TEM rapporterte en ueksisterende horisont nordvest; punkt-for-punkt sammenligning med Copernicus bekreftet det (94 m til sjøs til nordvest for Marina Grande vs. 0). Den falske horisonten på +2,6° koster ca. 26 dB troposcatter til kyststasjonene. Skadi-rutene forblir bare som en reserve når Copernicus ikke svarer. Copernicus-rutene veier 15–30 MB og laster ned én gang; de av bare havet eksisterer ikke og er verdt 0.

- Gratis plass: Lfs = 92,44 + 20 log f(GHz) + 20 log d(km) [8].
- Diffraksjon: Bullington-metoden på den virkelige profilen pluss ITU-R P.452-16 § 4.2 [6] delta-Bullington sfærisk landkorreksjon, med ITU-R P.526 [9] sfærisk tap.
- Første Fresnel-sone: radius √(λ·d1·d2/d); fri hvis bakken etterlater seg minst 0,6 av radien. For hver hindring vises [9] Knife Board V og J(VV) parameteren...
- I utformingen av profilen er den direkte radiusen grønn i optisk og rødt syn hvis den passerer under bakken eller under krumningen av jorden; røde prikker er de hindringer som invaderer det 1. område av Fresnel (rense prikket linje). På havet er det "røde punktet" i høyde 0 m ikke et fjell; det er havet som stiger for krumningen. Å passere med musen på radius eller på et punkt vises en tegneserie med forklaringen og verdiene.
- Når en relieff nær antennen lukker retningen (for eksempel en horisont på titalls grader), gir diffraksjon og troposcatter tap på hundrevis av dB: over 300 dB programmet skriver "> 300 dB" ogdirectretning lukket av en nabohindring", signalet "< −230 dB" og S/N-forholdene "−−60 dB". I så fall er det tilrådelig å se på «Recommended anlegg» og «Via monte».
- Troposcatter: ITU-R P.452-16 ekv. 45 til 50% av [6] -tiden, med vinkelavstand fra horisontene og N0 fra profilene.
- Uten tropo: summen av kreftene til diffraksjon og troposcatter, aldri under det frie rom.

## 8 bis. Troposcatter: fire modeller og usikkerhet

Ved siden av P.452 beregner programmet troposcatter med tre andre modeller, som TropoLink [16] -programmet, og viser forskjellen som usikkerhet:

- NBS TN101 § 9 [25] (fra 1.3.5), median: L = 30 log f − 20 log d + F(θd) − F0 + H0 + Lgp. F(θd) er dempningsfunksjonen i vedlegg III i dets analytiske former (Ns 301) med korreksjon for Ns, H0 frekvensgevinstfunksjonen til § 9,2: ved 144 MHz, med lave antenner i bølgelengder, refleksjonen fra bakken delvis sletter feltet i den nedre delen av det vanlige volum og tapssaltet på 3-10 dB. Ns er den av timen GFS-profiler, de effektive horisontene og høydene kommer fra Copernicus-profilen, Lgp = 0,07·e^(0,055(Gt+Gr))) er åpningskoblingstapet. Det er den samme form som ITM bruker i sitt indre, men med reaktivitet i dag i stedet for klimaet. Den tester på flate seksjoner ved 144 MHz med antenner ved 20 m: innen 2–4 dB fra ITM mellom 150 og 900 km og ca. 2 dB i forenklet form 9,2b TN101, som ikke har H0.

- ITU-R P.617-5 [17], median: L = F + 22 log f + 35 log θ + 17 log d + Lc, med f i MHz, θ i mrad, d i km, F = 0,18· N0 ·e^(−hs/7,35) − 0,23·ÐN og Lc = 0,07·e^(0,055(Gt+Gr)). N0 og ΔN er vanligvis tatt fra ITUs klimakart; her bruker vi hver time profilene GFS ( N0 ·e^(−hs/7,35) er reaktiviteten til bakken, ΔN reduksjonen i de første km), så også troposcatter følger tiden.
- RSGB VHF/UHF Håndbok Formel, DL2FZN [18]: L = Lfs + 24,44 + 4,283 NOK f + 0,06656 d. Bakken med avstanden er nesten lik PROPBER (0,067 dB/km).

Vanlig volum (fra 1.10.2009): er det punkt der radiusen til de to stasjonene er krysset, hver som er skrått til dens horisontvinkel, på en jord med ekvivalent radius ae = k·6371 km. Fra TX stiger radiusen som z = hTX + x·θt + x2/(2·ae); Avstanden x = (hRX − hTX + d·θr + d2/(2·ae) / θ, med θ = d/ae + θt + θr. Med flate horisonter og antenner til samme kvote er basen verdt d2/(8·ae): ca. 1,3 km til 300 km, 3,7 km til 500 km, 9,4 km til 800 km. Jo høyere volumet er, jo mer er luften tørr og sjeldent spredt og jo mindre sprer den seg; av denne grunn går det ut over 800–1000 km. I resultatene er andelen og posisjonen til basen, reaktiviteten, gradienten og fuktigheten til GFS i den høyden, og tilsvarende avstand av Bray (QST, november 1961): hver ekstra eller mindre horisont er verdt ca 111 km unna. Referanse: presentasjonen " Over horisonten i VHF" av IU3QEZ (GEKO Magazine n. 69).

Troposcatter linje, andre modeller viser de to tap og usikkerhet som halvparten forskjellen mellom den mest optimistiske modellen og den mest pessimistiske. De er bare sammenligninger: tapet uten tropo gjenstår som P.452.

## 8 ter. ettermiddag troposcatter over lindringer (fra Spain)

IK3XTV, fra Thiene ( JN55, med Alpene på 10 km), jobber om sommeren ettermiddager Sør-Tyskland på 400-500 km, uten å se, med sterke signaler men med rask og dyp evanescence; forfatteren tilskrevet dem til " konfektive celler som er rettet som et linstog". Observasjonen bekreftes; forklaringen i programmet er en annen, fysisk fastere.

På ettermiddagen rører solen luften over relieffene og grenselaget stiger til 2-3 km. På toppen av den fuktige luften nedenfor møter den tørre luften ovenfor: der er turbulensen av brytningsindeksen (størrelsen som styrer troposcatteren) er maksimal. Når den toppen når det felles volumet av ruten, som på 400–500 km er ca. 2–3 km, forsterkes troposcatteren. Dette forklarer ettermiddagstiden, høyt trykk (silent sylindrisk, sterk oppvarming) og rask evanescens, typisk for diffusjon og ikke av kanalene. Konvektive celler virker ikke som linser: produsere turbulens, og det er turbulensen på toppen av det omarbeidede laget som sprer signalet.

Konsekvenser for leseprognoser:

- På jorden den første ettermiddagen er det verste tidspunkt for de lave kanaler og lag, men ikke for troposkatteren utenfor fjellene; de to ting skal leses separat.
- programmet allerede har de to dataene som det tjener, høyden på det begrensede lag som er gitt av modellen og andelen av det felles volumet på ruten (vanlig volum) i resultatene): når de er nært, kan en ettermiddagsfjellbehandling gå bedre enn beregningen i normal atmosfære;
- fra NexoBrids punktpunktberegningen tar hensyn til: Hvis det felles volum er på jorden, langs ruten er det relasjoner på minst 800 m, solen er over 15° og grenselaget overstiger 800 m, og toppen av laget omforming (modell jordhastighet + grense laghøyde) er innenfor 500 m fra basen av det felles volum, får troposcatter 3 dB; mellom 500 og 1000 m den lineære gevinsten faller til null. Linjen "Troposcatter ettermiddag". Med snø langs ruten (minst 10 cm på bakken i henhold til modellen, gjelder ikke snø_dybdevariabelen av Open-Meteo eller SNOD av GFS fra NOMADS: på snøen jorda forblir kaldt og det re-formede laget vokser ikke. Verdien på 3 dB er foreløpig og må kalibreres med reelle lenker; den slår av med værkorrigeringer.

Kilder: observasjoner fra F. Egano IK3XTV (Radio Rivista e www.qsl.net/ik3xtv, «Propagazion sulle 2 metri», «Celle konvettive», «Troposcatter over Alpene»), kilder § 13.

## 9. Grunnlaget langs ruten

Det er hjertet av signalberegning. Langs ruten er GFS-profiler tatt hver ca. 25 km. For hvert trykklag er den lengste strekk hvor det samme laget er tilstede på påfølgende punkt; tell som kanal bare tykke lag minst 40 m [10].

- kontinuitet: fra hva km til hvilket km, og fraksjonen av ruten;
- topp og base, og tendens i m/km;
- jord inne i laget: km der fjellene stiger over basen;
- Dekket land: på havet kanalen er nesten alltid, bestemmer strekningen av land [10]. Hvis arealet overstiger 10 % av ruten, er fraksjonen som brukes minimum mellom kontinuitet og dekning av jorden.

Tropeforsterkningen er gevinsten av laget for den brukte fraksjonen. Hvis mer enn halvparten av profilene er underkjølte er gevinsten negativ.

## 9 bis. Værkorreksjoner: stabilitet, støtte, streik

Fra 1,3.0 gir profilen også tre meteorologiske opplysninger, som kan ses i registeret (registrer «stabilitet (Misme)»), i Meteo-linjen langs resultatveien og med Segni meteo-boksen på kartet.

- Stabilitet i Misme [5], kapittel jämt Misme binder feltet utover horisonten til en forbindelsesparameter: M = a(ge - 40 + b·ΔW), med a = 0,5 dB per N/km, ekvivalent gradient ge og termisk stabilitet ΔW, arbeidet med å heve luften på 1 km. Programmet beregner ΔW = g·(θ θ0)/θ0 dz mellom jord og 1 km over, med θ potensiell temperatur; standardatmosfæren er verdt ca. 56 J/kg. Begrepet som tilsettes i gevinsten, er 0,07 dB for J/kg utover standarden, begrenset mellom −4 og +6 dB, og teller kun der ingen kanalfeller båndet (gradienten er allerede i Bean- og Duttonindeksen). Kalibreringen er midlertidig: den må verifiseres med forholdet (nr. 14).
- Subsidence og oppover air [19]. Moler og Holden, på ruter på 125-305 km til havet ved 1365 og 9380 MHz, fant det høyeste signal på ca. 2,5 dB i gjennomsnitt med luft som kommer ned med luftoppgang, og i et tilfelle en forsterkning på ca. 20 dB i 3 timer i begynnelsen av underlaget, etterfulgt av en reduksjon på 22 dB når luften vendte tilbake til konvergasjon. GFS gir vertikal hastighet til 850 og 700 hPa. Hvis luften på begge nivåer faller mer enn 1 cm/s og det er en revers eller en skarp dråpe i den potensielle brytningsindeks K (N rapportert adiabatisk til 1000 hPa, som fremhever den tørre og varme luft som er droppet fra toppen [5]), tilsettes 1,5 dB; dersom luften stiger med mer enn 5 cm/s, tar de av 2.
- [20] Scirocco. Varm og tørr luft fra Sahara over kjøligere marine luft gir svært sterke inversjoner fra det østlige Middelhavet til Gibraltar. S-tegnet vises der vinden kommer fra 100–200° med minst 7 m/s og temperaturen er minst 16 °C på Middelhavet. Det er bare en indikasjon: gevinsten bestemmer lagene i profilen.

Uten en kanal som fanger bandet, overstiger den totale gevinsten ikke 16 dB, enden av det gule: rettelser gjør sterkere lag mer tydelige, men ikke passerer dem via kanaler (rød og lilla forblir ved kanalene).

- Persistens (fra 1.6.1). De største åpningene kommer etter gode høytrykksdager (VHF/UHF DX Book; Biancomano og Leyshon 1998, regel på 12 timer; rapporter av 29/12/2021–2/2022 vokser fra den første til den fjerde dagen). For hver celle teller programmet timene til den forrige 24 med gevinst på minst 13 dB og legger til opptil 2 dB i proporsjon, bare hvor gevinsten allerede er minst 10 dB.

Hvordan lese tegnene på kartet (Byt Weather Signs; fra ICAO symbolet er stor dobbel og passerer med musen over et tegn vises en tegneserie med forklaring og verdier av cellen):

| Signere | Farge | Hva betyr det? | Effekt på trope |
|---|---|---|---|
|  ↓  | grønn | underlaget: luftdråper (antisyklon), varme for kompresjon og tørr, styrker det motsatte og holder det fast | gunstig, +1,5 dB |
|  ↑  | rød | oppover (front, lavt trykk, konveksjon): blander lave lag og bryter inversjoner og kanaler | ufordelaktig, −2 dB |
|  S  | oransje | Syrup: varm og tørr luft fra Sahara over frisk sjøluft | gunstig ledetråd, ingen rettelse: bestemme lagene |

Et område fullt av grønt ↓ under et anticyklon er det klassiske tegn på en hard åpning; Røde ↑ piler langs linjen sier lagene bryter, selv om fargen på cellen fortsatt er god.

Weather Corrections-boksen slår av disse rettelsene for å se modellen på 1.2.

Validering (nr. 14): på 593 tilkoblinger til 144 MHz mellom 29. desember 2021 og 2. januar 2022, sammenlignet med de samme ruter og to uker senere, er separasjonsindeksen (0,5 = ingen, 1 = perfekt) 0,79 uten rettelser og 0,91 med korrigeringer (før taket ved 16 dB). Den samme sammenligningen viser den viktigste grensen: kanalen på havet i lav høyde, som i de dager brakte tegnene mellom Campania og Liguria, er ikke synlig på nivåene i GFS.

## 9 ter. Nyhetsbrev: to estimater, tak, høyt trykk, sjø, stasjoner, fjellrygger

Forsvarlig og aggressiv estimat. Forsterkningen fra gradienten vokser fra 0 dB til normalgrensen opp til 10 dB til terskelen for super-brytning. I 1000-850-laget er den forsiktige terskelen Bean og Dutton (100 N/km); Den aggressive terskel er den klassiske Super-Refrakt terskel på 79 N/km (k ≈ 2; P.453 bruker −100 N/km i den første 100 m), som med k50 = 157/(157 − ΔN) er k ≈ 2 (k = 2,5 tilsvarer ca. 94 N/km). Punktpunktberegningen viser de to tapene flankert; med en kanal som fanger gjengen sammenfaller. Kartet bruker forsiktighet.

Attic på 2500 m. Veier og kanaler er bare søkt etter opptil 2500 m over bakken: jo høyere antenner av amatørradioer ikke utnytter dem. Med 1500 m mistet valideringen et separasjonspunkt og 4% av tilkoblingene over 16 dB, fordi om vinteren er subsidensen reverseringer ofte mellom 1500 og 2500 m.

Høyt trykk utvidet. +1 dB hvor minst 7 av 9 cellene rundt har redusert trykket til havet på 1020 hPa eller mer og vinden er svak i de lave lag (10 m under 5 m/s, 850 hPa under 8 m/s), bortsett fra stigende bevegelser.

Havvekt. I punkt til punktberegning multipliseres gevinsten av lagene med 0,8 + 0,4·μ1, hvor μ1 er faktoren for ITU-R P.452 (eks. 51) på den lengre kontinuerlige landstrekning: 1 på det åpne hav, liten med lange landstrekninger.

Stationer og stasjoner. Når en kanal som fanger båndet kun dekker en del av linjen, sammenlignes andelen av hver antenne med basen og toppen av kanalen i begynnelsen og slutten av den dekkede seksjon; gevinsten multipliseres med 0,5 + 0,5 √(e^(-ΔhTX/300) ·e^(-ΔhRX/300), med Δh avstanden i meter ut av kanalen. Med kanalen på nesten hele ruten er koblingen av § 10.

Lag og rygger. Dersom toppen av grenselaget til GFS (oljehastigheten til den mest grense_layer_høydemodellen) overstiger kløftene i ruten (maksimalt lokalt over 300 m, hvis det høyeste overstiger 500 m), passerer laget over fjellene uten å bryte: +1,5 dB for fraksjonen av dekkede rygger. Det er observasjonen av IT9GSF på åpningene fra Sicilia til Tyskland med gunstig Tyrrensk.

Nivåer i nærheten av bakken på havet. Med NOMADS-kilden laster programmet også ned overflatetemperaturen (på havet er SST), temperatur og fuktighet på 80 m og i laget 30–0 hPa over bakken. På havpunkt (modelhastighet under 5 m) tilsettes disse nivåene til profilen, under det første trykknivået, for å se kanalen på 100–200 m at trykknivåene ikke løses. På jorden brukes ikke; natt og dag lag er ennå ikke gyldig. Open-Meteo gir kun overflatetemperaturen.

Fordampingsrørledningen. Fra SST er fra temperatur og fuktighet til 2 m estimert δ-høyden av fordampningskanalen med den nøytrale logaritmiske profil (som i modellen av Paulus og Jeske uten å korrigere stabiliteten): luften til vannets hår er mettet til SST (98% for saltet), derfor

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

begrenset mellom 0 og 40 m. For å fange 144 MHz vil det ta ca. 180 m (Kerr), deretter ved 144 MHz fordampningskanalen forsterkes lite; fra 432 MHz oppover, med lave antenner på havet, mye (nr. 11 bis). Angi parabolisk ligning og i linjekorreksjonene©.

Dagens tid (besøk). Programmet beregner høyden på solen på hvert punkt. Fra dag til dag på jorden, med solen over 15° og begrense lag over 800 m eller vind over 5 m/s, advarer jordkanalene omformes; om natten, med sol under 3°, vind under 3 m/s og begrenser lag under 300 m, advarer om at med rolig himmel kan ledes av stråling, som faller etter soloppgang. Ikke på havet. For nå er de oppdaget, ikke tall: December validering har ikke nok sommerdager til å kalibrere dem. Dette er to punkter som forfatteren anser som viktige, fra daglig observasjon.

Normal profil CRPL (alternativ). I stedet for 40 N/km fastes den normale reduksjonen etter den eksponentielle referanseatmosfæren CRPL: N(t) = Ns·e^(−c·t), med −ΔN i det første km = 7,32·e^(0,005577·Ns) (Bean og Thayer 1959). Med våt luft (Ns høy) er normalen brattere og tjener en sterkere gradient for å gi gevinst; i høyden er søtere. Det blir dårlig til validering sier det hjelper.

## 9 quater. Kontrast luft- og lavhav: Adriaterhavet

Radiobølger føler ikke havbunnen: føler kontrasten mellom luft og vannoverflate. Dypet teller fordi det bestemmer hvor raskt sjøtemperaturen følger luften.

Adriaterhavet er et lavt hav. Den gjennomsnittlige dybden er ca 250 m, men det nordlige bassenget overstiger sjelden 100 m og Triestebukta forblir under 25 m; bare i sør nedover over 1200 m. Et lag av vann så tynnt varmer opp og avkjøles på noen dager, Tyrrenian i uker. Det er tre typiske situasjoner:

1. Vår og tidlig sommer, gunstig. Havet er fortsatt kaldt, selv for ferskvannet i Alpine og Po-elvene; det kommer varm og tørr luft fra sletten eller Balkan. Varme luft over kaldt vann danner en reversering på havet (adveksjonsrørledning): er den klassiske tilstanden for åpningene mellom Italia, Slovenia og Kroatia.
2. Høst og vinter med bora, ufordelaktig. Kald luft over et varmere hav: det lave laget blir ustabilt, blander og kanalen bryter.
3. Etter en bora, sjanse. Bare fordi havet er lavt, en bora kjøler det noen grader på noen dager. Hvis du går inn i en anticyklon med varm luft, er luft-sjø kontrasten sterk og kanalen er mer robust enn vanlig.

Adriaterhavet er også en lukket kanal mellom Apenninene og Dinari, mindre enn 200 km bred: sjølaget forblir begrenset, og rutene langs aksen (Puglia-Venezia-Trieste, ca 700 km) forblir alle på havet, den beste betingelsen for en lang kanal.

Hva programmet beregner. På sjøpunktene, med værrettinger og boksen «Contrasto aria-mare»:

| Forhold | Korreksjon |
| --- | --- |
| potensiell temperatur ved 925 hPa (eller 950) over SST på 4 K | _ |
| SST fra 7 K | +2 dB |
| med luft fuktig ved 925 hPa (over 80%) | −0,5 dB på disse bonusene |
| avkjølt hav på 1,5 °C eller mer i 72 timer før, med gunstig kontrast | _ |
| varmere hav av luft ved 2 m 2 ° C (blanding) | −1,5 dB |
| varmere hav på 4 ° C | −3 dB |

Den totale korreksjonen forblir mellom -3 og +3 dB og uten kanal, forblir gevinsten under det gule taket. Terskelverdiene er midlertidige: i standardatmosfæren kan den potensielle temperatur ved 925 hPa overskride havet på ca. 2–3 K, deretter indikerer 4 K allerede et omvendt.

Minnet om SST. Modellene holder sjøtemperaturen nesten jevn under forutsigelse. Den nylige reduksjonen er avledet fra analysen av de tre dager før (Open-Meteo, bare overflatetemperatur, bare havceller): maksimalt 72 timer før minus den aktuelle verdien. Hvis tjenesten ikke svarer, gjelder minnebonusen ikke.

Grenser. SST av modellene under kysten og i det øvre Adriaterhavet er grov; den kalde pennen av Po, for eksempel, er løst dårlig. Batimetry går ikke inn i beregningen: kildene gir alltid SST, som allerede utgjør effekten av havbunnen. Lufthavskontrasten overlapper delvis i slutten av Misme stabilitet (§ 9 bis); for dette er bonusene små. Terskelverdiene vil bli slitt med korridorene i reklamer og QSO gjennom Adriaterhavet.

## 10. Oppført på hele ruten

Hvis sjiktet fanger båndet på minst 80 % av den brukte fraksjon og stasjonene ikke er i sikte, beregnes også tapet i kanalen: fri plass, pluss koblingen av antennene (3 dB dersom de er inne i kanalen, mer hvis de er over eller under), pluss den gjenværende demping γd = 5·10−5·ae·f^(1/3) dB/mrad av ITU-R P.452 eq. 49 [6]. Hvis det er mindre enn tap uten trope blir forventet tap.

Et bånd forblir i kanalen bare dersom dens bølgelengde ikke overstiger λmax = 2√2·(2/3)·(ΔM·10−6)·d (Kerr, eq. 17 [11]). For dette kan det samme laget fange 23 cm og 70 cm, men ikke 2 m. Men grensen er ikke netto: Kerr advarer om at selv bølger flere ganger lenger lider mye av kanalen, og Hepburn og VHF/UHF DX Book snakker om -sfærede - kanter og -forfalskende - kanaler. Fra Perjeta bruker programmet en myk kanal: hvis λmax er mellom λmax og 3·λmax, går forsterkningen gradvis fra 15 dB (λ = 3·λmax) til hele kanalforsterkningen (λ = λmax), med s = (λmax/λ − 1/3)/(2/3). I så fall er kanalfargene tillatt. Loggen og Meteo-linjen langs ruten indikerer punktene med myk kanal.

## 10 bis. Via Monte

Når korrespondenten er stengt bak horisonten, søker programmet den beste retningen for å peke antennene: den belyste skråningen sprer signalet som et radarmål. Den kjører horisonten hver 5. (unntatt ± 15° rundt korrespondenten) og i hver retning tar det opplyste horisontpunktet, over 100 m og mellom 1 og 60 km. Et raskt estimat (fri plass, bitaktisk konvertering og 10 dB for hvert nivå av horisont fra diffuseren til korrespondenten) velger den 4 mest lovende, minst 15° fra den andre; for hver:

- behandle 1, stasjon → lettelse, i fri plass;
- behandler 2, relieff → korrespondent, beregnet som om relieffet som overføres fra 10 m over toppen, med samme prediksjon (uten parabol likning);
- omdannelse med den bistatiske radarlikningen, med CAS = σ0·A, σ0 = −15 dB (fjell i VHF/UHF) og A området belyst av R1 -strålen (maks. 20 km2):

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

Den "Recommended Point" linjen indikerer azimuten til den beste ruten, hvis den koster mindre enn den direkte. Ved 144 MHz, med en avlastning på 20 km og en 15° yagi, koster fjellveien ca 45 dB mer enn 2 ruten alene: det skjer bare når direkte er lukket av nabohindringer. Beregningen er brutto (±10 dB), spesielt for σ0.


Uten tap til fjellet (valg, fra Č). Med \"Via monte senza perdit fino al monte\"-boksen anses fallet mellom stasjonen og diffraksjonspunktet som null: er som å bringe stasjonen til toppen (antenne til 10 m over punktet, samme effekt, antenne og kabel). horisontrunden velger deretter punktet med den mest frie horisonten mot korrespondenten (i paritet, det høyeste) og resultatet viser ruten fra punkt til korrespondent, med signal og S/N, ved siden av det totale med diffusjonen. Det er en optimistisk grense: sier om retningen åpner når fjellet hever signalet godt, eller hvor å gå bærbar. Hvert punkt er gitt med ti-tegneren.

## 10 ter. Inngang til en fjern kanal

Hvis kanalen som fanger bandet begynner langt utover stasjonens horisont, må signalet komme. Programmet bringer signalet til utgangspunktet, midt i kanalen, for diffraksjon: langs Copernicus-profilen GLO-30 søker kanten med den høyeste Fresnel-parameteren (ITU-R P.526), med hevelse av det tilsvarende land,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

mer demping på grunn av krumming av toppen (rundt hinder, P.526-16 § 4.2), med R-radius estimert fra Copernicus GLO-30 jord til ±1 km fra kanten,

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

(for m·n > 4 er form 34b i anbefalingen). Barsis (NBS, 1963) målt på tre lange ruter over et fjell (Colorado, Alaska, Europa) gjennomsnittlig tap 10-20 dB høyere enn den ideelle kanten av Fresnel-Kirchhoff: avrundingen forklarer mye (Barsis og Kirby, 1961: Pikes Peak med en radius på ca 11.5 km). Resultatet sammenligner med diffraksjon på glatt sfærisk jord; det er det verste. Deretter beveger signalet seg i kanalen: fri plass på hele ruten, restdempning av P.452 på seksjonen i kanalen og kobling av korrespondenten (3 dB inne, 1 dB hver 10 m utenfor). Hvis dette tapet er lavere enn det forventede tapet, blir det mekanismen (" leder (innført ved diffraksjon)"). Det er forfatterens premiss på italiensk geografi: med et nærliggende hinder godt under linjen, v er negativ og inngangen koster lite; med en dorsal front koster 15–30 dB.

## 10 quater. ITM (Longley-Rice) og ut av kanalen

Fra SpyHunter inneholder programmet irregulære Terrain Model 1.2.2 av NTIA/ITS (Longley and Rice, ESSA ERL 79-ITS 67, 1968; Hufford, Longley and Kissick, NTIA 82-100, 1982), oversatt til C# av den offisielle koden til den amerikanske regjeringen, som er fri fra rettigheter. Oversettelsen gir de samme tallene som originalen: automatisk sammenlikner det hver gang med NTIA C++ kode på fem test tilfeller (ned under 0,01 dB).

ITM beregner tapet i normal atmosfære på den virkelige profilen Copernicus GLO-30: sett med refleksjonen fra bakken, diffraksjonen (spigol og sfærisk land som veies på jordens ruhet Δh, det er forskjellen mellom den 10. og 90° percentilen av kvotene rundt den rette av de minste kvadratene) og troposcatter. Jordens reaktivitet reduseres til den gjennomsnittlige del av ruten, $N_s = N_0\,e^{-h/9460}$, og gir krumning av det tilsvarende land,

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

Programmet bruker radioklimaet på den valgte ruten som i § 10 kjønn (til Swiss alltid temperert hav, med N0 320 på land og 350 på sjøen), med N0 fra den gjennomsnittlige reaktiviteten til stedet (soil εr 15, σ 0,005 S/m; hav εr 81, σ 5 S/m), horisontal polarisering og variasjon over tid uten det stedet (profilen er kjent). Det viser tapet overskredet i 50% av tiden og den du har i 10% og 1% av den beste tiden. ITM modellerer ikke kanalene: dens variasjon over tid er statistisk, etter klima. Det tjener som en andre beregning av grunnlinjen ved siden av P.452; forskjellen mellom de to gir en ide om usikkerhet.

Den andre bruken er den endelige strekning etter en kanal som slutter før den tilsvarende. Ved utgangspunktet blir en virtuell antenne plassert halvveis gjennom kanalen og ITM beregner kanalen til det tilsvarende, som overskudd over det frie rom ved 50 % av tiden. Totalt tap er

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,\theta_{condotto} + A_{ITM}(d_{uscita} \to RX)
```

hvor A_TX er koplingen av antennen (3 dB inne, 1 dB hver 10 m utenfor) dersom kanalen starter nær, ellers inngangsdiffraksjonen av § 10 ter. Det er grensen som forfatteren har gitt seg selv: kontinuiteten i laget pluss, i det meste, gjenværende demping. Hvis det er lavere enn det forventede tapet, blir det mekanismen (eksit og sluttstrekning ITM)").

## 10 quinquies. Fordypning: Øyer, strimler og jordhavsutsettelse

Et eksempel fra Napolibukten (Anacapri, JN70CN) mot det vestlige Middelhavet viser hvor ruter krysser øyene:

| Destinasjon | Azimut | Hvor det krysser øyene | Obstacle |
| --- | --- | --- | --- |
| Barcelona |  279°  | nord for Sardinia (Gallura) til 41,05° N, mellom 420 og 490 km | rundt 30 km sør for Bocche di Bonifacio; Limbara (1359 m) ca 20 km fra linjen |
| Bocche di Bonifacio |  282°  | den smale til 41,3° N | ca 11 km |
| Marseille |  296°  | Sentral Korsika ved 42,2° N | fjell opp til 2700 m: en kanal under 2 km det stopper |
| Valencia, Balearikk |  260–265°  | Sardinien | Interne lettelser |

For Barcelona er det nok å peke på 282° i stedet for 279° fordi aksen passerer fra munnen: 3rd, inne i bjelken av en normal yag.

Er munnen en streng? For de lave kanaler, under et par hundre meter, ja: Øyene er muret, og over jorden, om dagen, varme bryter inversjonen. Selve streken er bred nok: ca. 11 km, mot et første område av Fresnel, mindre enn en kilometer bred, halvparten av en 1000 km rute i 144 MHz. Problemet er været. Munnene er moro for vinden: Det nedre laget akselereres og det nedre laget fjernes. Ofte er passasjen åpen som geometri, men forstyrret som atmosfære. Med en jevn antisyklon og liten vind kan i stedet være den gode passasjen.

Jordvannsforskjell. De teller. En kanal som dannes på Tyrrensk kyst blir avbrutt: signalet kommer ut fra kanalen (seksjonen etter utløpet, § 10 kvater) og må vende tilbake til en annen kanal på sjøen av Balearisk, betale igjen koblingen. Kanalene i høyde, over 1500 m, passerer over Gallura uten å innse det; for sentrale Korsika gjelder ikke.

I praksis: Hvis ruten krysser en fjellet øy, prøv en viss grad av rettelse mot strekket av fritt hav, men ikke stole på passasjen med sterk vind i smal.

## 10 sex. TN101 radio klima og tidsvariasjon (fra 1.3.5)

TN101 § 10 [25] beskriver hvordan tapet av en rute utover horisonten varierer i løpet av den lange perioden rundt medianen, med ulike kurver ved klima: Y(q)-funksjonen sier hvor mange dB tap faller i q% av den beste tiden. ITM inneholder disse kurvene for syv klima. Klimaet teller mye: på havet i det tempererte marine klimaet, på 144 MHz, i 10 % av tiden er det mye mer dB enn på land i kontinentalt klima, fordi kanalene er hyppigere.

Programmet løser ikke klimaet lenger: Han velger det for alle ruter fra gjennomsnittet, så det fungerer over hele verden og ikke bare i Europa. Last ned fra Open-Meteo [26] tre års daglige verdier av ERA5 reanalyse (temperatur, fuktighet og jordtrykk, regn) for cellen på 0,5° av gjennomsnittspunktet, en gang bare; verdier forblir i Data/CLIMA. Restaurering av jord

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

dag for dag, så det årlige gjennomsnittet og utflukt mellom månedlige gjennomsnitt, gjennomsnittlig fuktighet og årlig regn. Reglene følger beskrivelsene av klimaene i TN101 § 10.2; grensene mellom det ene klima og det andre er et kriterium for forfatteren:

- over 60° breddegrad: temperert kontinentalt ( TN101 har ingen kurver for polar klima);
- mindre enn 250 mm regn per år og gjennomsnittlig fuktighet under 50%: ørken (Ns typisk 280);
- innen 20° fra ekvator, Ns på minst 350 og utflukt opp til 30: ekvatorial (Ns typisk 360);
- innen 32° og utflukter av minst 60: subtropiske kontinental, det vil si monsuner (typisk utflukt 60–100);
- mellom 10 og 32° med Ns på minst 340: subtropisk marine (Ns typisk 370);
- begge horisonter på havet, over 20°: temperert hav på havet (det er definisjonen av TN101);
- utflukt opp til 30 i nærheten av havet (sjø på ruten eller gjennomsnittlig fuktighet på minst 75 %): maritim herdet på jorden;
- ellers temperert kontinentalt (vanlig utflukt 20-40).

N0 for ITM er gjennomsnittlig redusert til havnivå, N0 = Ns·e^(t/9460). Uten et nettverk brukes en reserveregel med breddegraden alene og sjøfraksjonen, og linjen sier det.

Tester med data ERA5 2023-2025: Capri Ns 339 med utflukt 45 (kontinental herdet på jorden, havet temperert når begge horisonter er på havet); London 327 og 16 (temprert stjerne); Wien 319 og 24 Abidjan 381 og 11 (ekvatorial); Miami 365 og 36 (subtropisk ektemann); Delhi 337 og 82 (subtropisk kontinental); Riad 278 og 37, 90 mm regn (ødelagt). I Chile: Antofagasta 341 og 19 (subtropisk ektemann: kysten av Atacama, med nesten fast lav bakside av Stillehavet), Calama 232 på bakken, 2257 m, 14 mm regn (desert), Valparaíso 331 og 11 (temperatur søm), Punta Arenas 313 og 5 (temperatur søm).

Linjen " Radio-klima ( TN101)" viser klimaet, grunnen, Ns med halvparten av utflukten og Y: hvor mange dBs tjener på 10% og 1% av tiden og hvor mange tap i 90%. Det er langsiktig statistikk og forblir separert fra prognosen: det forventede tapet følger tiden i dag med GFS-profilene, og summerer de to tingene vil telle to ganger den samme unormale utbredelsen.

## 10 septies. Pålitelighet av prediksjon og verifikasjon med radiosonner (fra 1.4)

Radioson sjekk. GFS ser kanalene mindre godt enn det ser ut til: Dens vertikale oppløsning nær bakken er et par ti meter, og laget på en revers er ofte tynnere. For å vite hvor mye å stole på modellen her og i disse dager, sammenligner programmet det med de virkelige radiobølger på den nærmeste stasjonen, fra arkivet IGRA v2 av NOAA [27] (offentlig domene):

- tar fra listen over stasjoner den nærmeste aktive innenfor 600 km (for Capri, Practice of Mare, ITM00016245);
- Last ned gjeldende årsfil (det oppdateres hver dag) og holder lanseringen av 00 og 12 UTC siste 15 dager;
- for hver lansering avledes reaktivitet N nivå etter nivå (med fuktighet eller depresjon av duggpunkt), kvoter av nivåene som ikke har dem med ipsometrisk ligning som starter fra høyden av stasjonen, gradienten av det første km og den sterkeste kanalen under 3000 m (M som faller med kvoten på minst 2 enheter);
- Last ned fra tjenesten for historiske prognoser for Open-Meteo [15] GFS-profilene i samme punkt på samme time og analyser dem med samme prognosekode;
- teller de observerte kanalene som GFS har sett, de manglende og falske alarmer, og den gjennomsnittlige og firkantede kaste av gradienten.

Verifiseringen kjører i bakgrunnen til den første beregningen av en behandling, i det meste hver 12. time; filene forblir i Data/IGRA og tallene i beregningsloggen. Radiosondemeldinger som kommer fra verdens værnettverk (TMP) har bare standard og signifikante nivåer: de svært tynne kanalene flykter også til dem, og sammenligningen sier fremfor alt om GFS er veldig galt i en eller annen forstand.

Pålitelighet. Tilknyttability linje gir en høy, medium eller lav vurdering av den forventede gevinsten. Det er ikke en kalibrert sannsynlighet, noe som vil kreve års sammenligning med reelle lenker (§ 14), men en sum av punkter om hvordan uavhengige ledetråder (forfatterens kode):

| Adresse | Poeng |
|---|---|
| Avtale mellom modellene: gjennomsnittlig avvik mellom det forventede tap og P.452 med kanalene, den parabolske ligningen og mellom baseline og ITM | opp til 3 dB + 1; over 8 dB −1 |
| Forhåndsprognose | innen 24 timer +1; over 72 timer −1 |
| Kontinuitet av grunnlaget på behandlingen | minst 70% +1; under 30% −1 |
| Holdbarhet av laget i nabotidene | minst 70% +1 |
| Radiosonde verifikasjon | GFS bare minst 3 ganger ut av 4 +1; mindre enn 1 i to −1; feil gradient på mer enn 15 N/km −1 (maksimum −1 i alt) |
| Live-bekreftelse langs ruten (cluster, PSKReporter, APRS, FM-DX), bare for tiden | 1-2 par stasjoner +1; 3 eller mer +2; åpninger med forventet gevinst under 2 dB-1 |

Høy fra 3 poeng, gjennomsnittlig fra 0 til 2, lavt under null. En levende bekreftelse teller som et par ulike stasjoner, i retning innen 20° fra rutens retning og med gjennomsnittspunktet innen 150 km fra linjen, i den siste timen og en halv. Ved arkiver (§ 3 ter) er det ingen radiosonger eller live bekreftelser.

## 11. Sammenligning med ITU-R P.452

Sammenligningslinjen P.452 anvender ITU-metoden for kanaler og refleksjon på lag (P.452 § 4.5 [6]) med ΔN-medianen til GFS i stedet for den klimatologiske og p = 20% som kalibreringskonstant, som i studien CSIRO [10]. Det omfatter tap av empirisk kobling Alf under 0,5 GHz (ca. 28 dB til 144 MHz, 3.5 Det er bare en sammenligning: det forventede tap forblir punkt 9.

Fra 1.4: P.452-18 komplett og verifisert. Sammenligningen bruker nå all den grunnleggende metoden til P.452-18 [29] (§ 4.1–4.6): fri plass med gass og multipath, diffraksjon med det sfæriske land og Bullingtons profil på den riktige glatte overflaten for hindringer, troposcatter, kanaler og refleksjon på lag og deres kombinasjon. ΔN og N0 kommer fra GFS og fra stedet i stedet for ITU-kartene. Koden er blitt verifisert med de offisielle testtilfellene i ITU-studiegruppen 3 (samlet i prosjektet Py452 [29]): på 194 tilfeller opp til 2 GHz uten å oppsluge bakken er gjennomsnittsavviket på det totale tap 0,06 dB, maksimum 0,54 dB; diffraksjon 0,10 dB i gjennomsnitt, troposcatter 0,002 dB. Verifiseringen har korrigert to feil i de tidligere versjoner: koblingstiden for kanalene som brukes frekvensen i MHz i stedet for i GHz (ca. 60 dB for mye, kompensert andre steder), og diffraksjonen gikk fra kvoter av den allerede begrensede glatte overflate, med feil opp til 15 dB på visse profiler.

ITM (§ 10 quater) ble også sammenlignet med NTIA [30] (punktpunkt): de fem delene av den offisielle filen gir de samme verdiene til dB cent.

## 11 bis. Parabolisk ligning (fra 1. 000)

Parabol liknende linje er en fjerde beregning, bare av sammenligning, laget som profesjonelle programmer for kanalene (PETOOL; bølgepropagasjonsrammen til M. Lytaev, MIT lisens): i stedet for å beregne gevinsten fra lagene, den fremskrider bølgen langs ruten med Fourier split-trinn metoden inne i M modifisert GFS brytningsprofil, tatt fra prognosepunktet nærmere hver avstand. GFS-nivåene tilsettes til de estimerte hoppene i de potensielle kanalene (beregning A, 50 m i det halve lag). Jord Copernicus GLO-30 går inn i trinn (under bakken feltet er null), jorda er dirigent og strålen av antennen er en gaussisk med estimert åpning fra gevinsten. Beregningen ser kanalene, de som mister, diffraksjonen og koblingen av antennen; Han ser ikke troposcatteren, som er tilsatt fra hverandre. Neste er «profilkontroll»: det samme med standard atmosfære mindre den med forventet profil.

Fra jämt:

- Forplantaren er i bred vinkel, med den nøyaktige fase av det frie rom:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- bunnen på havet og på kystsletten i nullhøyde reflekterer med koeffisienten til Fresnel i horisontal polarisering, εc = εr − j·60·λ·σ (sjø εr = 81 og σ = 5 S/m; land 13 og 0,006 S/m), i den klare vinkel av ruten:

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- havets grovhet reduserer den konsekvente refleksjon i henhold til Miller og Brown, med den effektive høyden av bølgene fra vinden til 10 m (σh = 0,0051·U2):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- På havet omfatter profilen M fordampningskanalen (nr. 9 ter) med den nøytrale logaritmiske profilen:

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

Det tester med prototypen (10 m antenne på havet, gevinst på standard atmosfære på 100–300 km): med δ = 10–30 m fordampningskanalen gir 1–3 dB til 144 MHz, 8–37 dB til 432 MHz og 21–66 dB til 1296 MHz. Ved 144 MHz i horisontal polarisering reflekterer havet nesten som et metall (cirka mer enn 0,99 til noen få tiendedeler grad) og grovhet teller lite: den nye bunnen endrer noen få tiendedeler av dB; den teller mer fra 432 MHz opp.

Grenser: over jorden er den vertikale profilen fortsatt GFS-nivåene, så de tynne kanalene forblir usynlige; bakken til trinn er tilnærmet; bjelken forblir innenfor 6.

## 12. antenner, signal og støy

- Yagi DL6WU: få 9,5 + 7,8·log10(boom/λ) dBd (2,35 dB for dobbel boom, DL6WU måler på VHF kommunikasjon 3/182), pluss 2,15 for [12] dBi.
- EIRP = effekt − kabel + TX gevinst.
- Signal mottatt = EIRP − forventet tap + gevinst RX − kabel RX. S-meter IARU: S9 = −93 dBm, 6 dB per enhet.
- Ekstern støy i henhold til ITU-R P.372 [13]: galaktisk (Fa = 52 − 23 log f) mer kunstig i henhold til miljøet (by, bolig, landlig, rolig landlig) eller en håndskreven antennetemperatur.
- Tsys = Ta + Tpre + [290(Lc − 1) + 290(Fr − 1)·Lc] / Gpre til terminalene til antennen (Friis formel). Fra 13,5 har mottakerstasjonen alltid en forhåndsforsterkning i antenne, fast parameter: støynummer 0,8 dB (Tpre = 59 K) og gevinst 20 dB. Bak det er kabelen (tap Lc) og radio, med grunnleggende Fr støy nummer 6 dB. Med 1 dB kabel- og radiobidrag faller fra ca. 1160 K til 12 K: støyen bestemmer himmelen, miljøet og preampen. Gevinsten på 20 dB er en typisk verdi valgt av programmet.
- S/N i SSB, CW og FT8 i bandet på hver måte, sammenlignet med terskelsettet.
- Sannsynligheten for perioden (fra ICAO): Evanescensen i løpet av timen er en normal fordeling av S/N rundt forventet gjennomsnittlig timeverdi. Bray (QST, November 1961, s. 39, på kurvene i Norton 1959) gir en amplitude på 13,5 dB over ca. 100 miles, dvs. ca. 7 dB under og 7 over gjennomsnittet, som nesten faller rett under 100 miles (3,5 dB til 50 miles); for en \"fast\" tilkobling det tar 7 dB fra budsjettet. Teke ±7 dB som ±2CAS: CAS = 3,4 dB over 160 km, proporsjonal med avstanden nedenfor. Sannsynligheten for at en periode overstiger grensen for veien er

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  med jämt normal distribusjonsfunksjon: margin 0 dB = 50%, +3,4 dB = 84%, +7 dB (den \"faste\" margin av Bray) = 98%. Det inkluderer ikke usikkerheten i modellen, som er større.
- Digitale måter: S/N i 2500 Hz sammenlignet med terskelverdiene målt ved 50% dekoding, uten tidligere informasjon (AP). FT8 −21 dB og FT4 −17,5 dB på hvit støy; når mekanismen er troposcatter bruker terskelverdier med avanescens " disturbed breddemedium", FT8 −18,6 dB og FT4 −15,2 dB [21]. Q65 -15C −22,2 dB, Q65 -30B −24,8 dB, Q65 -60A −27,6 dB I fly scatter Doppler kan overskride tonene til Q65 -60A (1,7 Hz): det er bedre enn 30B eller 15C. JT65A -25 dB [22]. Dekoderen AP (navnet på den allerede kjente eller slutten av QSO) gir ca. 2 dB ekstra for FT8 og 1,5 til 2,6 dB for Q65; den angir ikke terskelen fordi den ikke er gyldig for alle de «−24 dB» verdier som ofte leses for FT8 er grensen for WSJT-X relasjoner eller AP dekoding, ikke terskelen på 50%. [24]

## 13. Respons- og datatider

Hver beregning måler sin egen tid: GFS-profiler (nettverk, analyse, obligatoriske pauser på 20 s mellom batchene for Open-Meteo-grensen), Copernicus GLO-30 terreng (med nedlasting av rutene, 15–30 MB hver, bare første gang), beregning. Response tidslinjen oppsummerer dem; i registeret begynner radene med TEMPI. Når du oppgraderer nettet, rapporterer registeret også GFS-løpet som brukes, når det ble tilgjengelig og hvor gamle dataene er.

Du kan også velge ICON-2I, modellen til byrå ItaliaMeteo og Arpae på Italia på rundt 2,2 km, gjennom Open-Meteo (model italia_meteo_arpae_icon_2i, to løp per dag, 3 dager). Den dekker bare Italia og de nærliggende havene og har mindre lave nivåer av GFS (1000, 925, 850, 700, 500 hPa): for kanalene i høyden er ikke bedre enn GFS, men det fine rutenettet beskriver brønnkysten, øyene og havet på 2 m, det vil si fordampningskanalen. Variabler som en modell ikke har (stret grense, temperatur på overflaten...) fjerne seg fra forespørselen. Den automatiske kilden bruker kun NOMADS; Open-Meteo tjener hvis NOMADS ikke reagerer og i historiske tilfeller. Med \"Update\" i seg selv - boksen (belyst av defekt) er prognosen uklikket: ved åpningen, på hvert nytt GFS-løp på NOMADS (ca. 5 timer etter løpets tid) og når den synlige sonen kommer ut i mer enn et kvartal fra rutenettet; på det meste et forsøk hvert 15. minutt, aldri med et historisk tilfelle.

Forespørsler på Open-Meteo har grenser per minutt, per time og per dag for hver internettadresse. De nedlastede profilene lagres på disken (Dati/cache-gfs) for hvert slag og modell: med samme løp, som endrer seg hver 6 timer, spør ikke punktene som allerede er nedlastet igjen. Med den automatiske kilden starter du fra NOMADS og Open-Meteo bare for manglende poeng; med den eneste Open-Meteo, på tidsgrensen eller daglig stopper umiddelbart og sier når du skal prøve igjen. Historiske tilfeller er ikke cache.

## 14. Valider modellen

Forventningen skal sammenlignes med reelle forbindelser. Beregningsloggen holder hvert nummer; programmets arkiv holder plass og dekoding. For 432 og 1296 MHz, der punktene er få, trenger de rapporter fra operatørene: som gjorde forbindelsen, til hvilken tid, med hvilket signal. Sammenlign i mange timer den forventede gevinsten med det målte signalet sier om terskelverdiene skal beveges.

Testprogrammer som brukes for P.452 og ITM er i kildekodeverktøymappen; ITU test tilfeller må lastes ned separat, fordi programmet ikke distribuerer ITU-data.

## Kilder

1. NOAA/NCEP, Global Prognose System (GFS), global modell på 0,25°, fire løp per dag.
2. Open-Meteo, prediksjon API (open-meteo.com), gfs_seamless modell, variabler ved trykknivå og race metadata.
3. Copernicus DEM GLO-30 (ESA; WorldDEM © DLR e.V. 2010–2014 e © Airbus Defence and Space GmbH 2014–2018, levert som en del av Copernicus av EU og ESA), 1 andre bue, GeoTIFF-rammer på det åpne registeret over Amazon Web Services. Reserve: NASA/USGS Copernicus GLO-30 i Terrain fliser "skadi" av AWS.
4. ITU-R P.453, Radio brytningsindeks: formel og brytningsdato, med digitale kanalkart (studiegruppe 3, © ITU, personlig og ikke-kommersiell bruk).
5. B. R. Bean, E. J. Dutton, Radio Meteorologi, NBS Monograph 92, 1966: Tabell 6,5 og § 4.4.
6. ITU-R P.452-16, Forutsigelsesprosedyrer for vurdering av interferens mellom stasjonene på jordens overflate ved frekvenser over ca. 0,1 GHz: § 4.2 (diffraksjon), eq. 45 (troposcatter), § 4,5 og eq. 49 (ledende).
7. ITU-R P.834, Effekter av troposferisk brytning på radiobølgeutbredelse.
8. ITU-R P.525, Beregning av fri-space demping.
9. ITU-R P.526, utbreiing ved diffraksjon: ekv. 31 (kutcheon) og § 3 (sfærisk land).
10. B. Indermuehle, H. Suzuki, Tropospheric Ducting Prediction basert på GFS Modelldata på Inyarrimanha Ilgari Bundara, CSIRO Murchison Radio- astronomi Observatory, Publikasjoner i Astronomisk Selskap i Australia, 2026 (arXiv 2608.12861).
11. D. E. Kerr, utbredelse av korte radiobølger, MIT radiasjon Laboratory Series vol. 13, 1951: eq. 17 e 18.
12. G. Hoch DL6WU, «Extremely Long Yagi Antennas», VHF Communications 3/1982, s. 130 et., fig. 1 (målt 432 og 1296 MHz i henhold til lengden på bommen).
13. ITU-R P.372, Radiostøy: galaktisk og kunstig støy for miljøet.
14. NOAA/NCEP, NOMADS, GRIB filter av GFS 0,25° tid (nomads.ncep.noa.gov); WMO G RIB2 format, kompleks pakking med romlige forskjeller.
15. Open-Meteo, Historisk værvarsel API (modellmeldingsarkiv fra rundt 2021).
16. LukiBox, TropoLink, design av troposcatter ruter med P.617, NBS TN101 og ITM flankert (GitHub, Apache 2.0 lisens).
17. ITU-R P.617-5, forutsetningsteknikker og data som kreves for utforming av transhorizon radio-relay systemer, 2019.
18. RSGB, VHF/UHF Handbook; DL2FZN implementering (andi-f/troposcatter på GitHub, 2013).
19. W. F. Moler, D. B. Holden, «Tropopheric scatter utbredelse og atmosfæriske sirkulasjoner», Journal of Research of the NBS 64D, 1960, doi 10.6028/jres.064d.014: signal utover horisonten (1365 og 9380 MHz, på havet) og vertikal bevegelse av luften.
20. ARRL Håndbok 2022 (99a ed.), § 19.4.3–19.4.4: Troposferisk vær og utbredelse.
21. S. Franke, B. Somerville, J. Taylor, FT4 og FT8 kommunikasjonsprotokoller, QEX juli/august 2020.
22. W3SZ, digital langsom modus terskelverdier ( JT65, JT9, QRA64) og modusvalg i henhold til utbredelse, 2019.
23. QST " Verden over 50 MHz", november 2022: åpning av 14–15 august 2022 mellom Manitoba og Kansas/Missouri.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Hurtigstartguide til Q65, 3 april 2021 (wsjt.sourceforge.io).
25. P. L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis, Transmission Loss Forutsigelser for Tropospheric Communication Circuits, NBS Tekniske Note 101, magasin 1967: § 9 (troposcatter, funksjon F(θd), H0), § 10 og vedlegg III (radioklima og variabilitet over tid).
26. Open-Meteo, Historisk vær API (ECMWF ERA5 gjennomgang siden 1940; Copernicus Climate Change Service, CC BY 4.0 lisens).
27. I. Durre, X. Yin, R. S. Vose, S. Applequist, J. Arnfield, Integrert Global Radiosonde Archive (IGRA), versjon 2, NOAA National Centers for Environmental Information, doi 10.7289/ V5X63K0Q (public domain): igra2-data-format.txt dataformat og stasjonsliste.
28. FM-DX Webserver (NoobishSVK og samarbeidspartnere, lisens GPL-3.0) og offentlig liste over serverservere.fmdx.org.
29. ITU-R P.452-18, Forutsigelsesprosedyrer for vurdering av interferensen mellom stasjonene på jordens overflate ved frekvenser over ca. 0,1 GHz, 2023; testtilfeller i studiegruppen 3 (godkjenningseksempler), som samlet i I. Stevanovic, Py452 (GitHub).
30. NTIA/ITS, Uregelmessig Terrain Model (ITM) 1.2.2, referansekode og punktpunktstestsaker (GitHub NTIA/itm).
31. ITU-R M.1371, tekniske egenskaper for et automatisk identifikasjonssystem ved hjelp av tidsdivisjon multippel tilgang i VHF maritime mobilfrekvensbånd.
