LIVETROPOVHF · teknisk dokument 2
# Opførelse af kortet

> Automatisk oversættelse af italiensk: i tvivlstilfælde er det den italienske guide værd.

Da kortet over fanen Kort og stedet er født: den geografiske base, fremskrivninger, gennemsnittet af Hepburn og F5LEN indeks, isobare, pletter og stationer lytter, overlappet med lag. Version af 9. november 2026.

## Sammenfattende

- Kortet er designet af programmet i otte lag, hver med sin [8] boks.
- Base: Naturlig Jord kystlinje og grænser på to niveauer af [9] detalje; [10] Marked og azimut fremskrivninger.
- Fysik: inversioner, reaktivitet N og gradient dN / dh, kanaltærskel ved − 157 N / km [3] [4] [7] [11].
- Halsindeks: kalibreret aflæsning af Hepburn [1] og F5LEN [2] kort, fælles gitter og gennemsnit af de to kilder.
- Isobare: GFS [12] tryk via Open- Meteo [15], linjer hver 4 hPa, H og L centre.
- Spot, lyttestationer (mørke lyserøde cirkler med altid synlige lyserøde linjer) og personlige dekodninger i rødt.
- Læsning af overlapningen: højt tryk, høje kanaler, trykfald, voksende SNR, [3] [4] [5] [13] [14] bjergruter.

## 1. Princippet: et layout kort

Kortet er ikke et fast billede: er designet af programmet, lag over lag, når du ændrer zoom, projektion, band, prognoser tid eller tema. Hvert lag har sin kasse i den øverste Layer bar på kortet, så du kan kun holde hvad du har brug for og se alt sammen, når du ønsker det fulde billede. Ordren, nedefra og op, er dette:

|  #  | Vej | Indhold | Casella |
|  1  | Grundlæggende kort | Hav, lande, kyster og grænser | Ja |
|  2  | Indekstropo | Media Hepburn + F5LEN, eller kun én kilde; justerbar gennemsigtighed | Ja |
|  3  | Grill locator | Felter, firkanter og, med zoom, kvadrerede | Ja |
|  4  | Isobare | Tryklinjer hver 4 hPa, H og L centre | Ja |
|  4a  | Temperatur 850 hPa | Isotermisk behandlet hver 4 ° C | Ja |
|  4b  | Termisk inversion | Orange prøve, hvor luften i højden er varmere end på jorden | Ja |
|  5  | PSKReporter | Modtagende stationer af PSKReporter (mørke lyserøde cirkler) og, i pink, hvad de hører | Ja |
|  6  | Afslået af mig | Afkodningsstationer med WSJT- X, JTDX eller MSHV: Røde linjer fra din QTH | Ja |
|  6a  | QSO desunti dal live | Lukkede forbindelser ( RR73, 73, RRR) i din dekodning: grøn linje mellem de to stationer | Ja |
|  7  | Spotlight | Forbindelser: fuld blå linje, gul dækket til airscatter, to bolde | Ja |
|  7a  | med undtagelse af | Casella Omfatter ikke under 100 km: den korteste trafik er hverken trukket eller opført. Casella Mere end 1000 km: kun firkanter (ved opstart): hvis en af stationerne er over 1000 km fra din QTH, ingen linjer eller cirkler, kun lokalisatoren kvadrater af de stationer, der er involveret i lilac behandlet ved 45 ° | Ja |
|  8  | QTH og etiketter | Din station, navnene på det valgte sted | altid synlig |

Tropo indekset er umiddelbart over basen og under alt andet: er en farvet prøveudtagning, og højere placeret ville dække linjer og symboler. Gitteret passerer over det, så du altid ved, hvor et farvet område falder. Biobareerne er tynde linjer og læses et godt stykke over prøveudtagningen.

## 2. Grundkortet

Omkostninger, grænser og jord kommer fra naturlige Jord vektor data, af det offentlige domæne [9], til skala 1: 50 millioner for samlet visning og 1: 10 millioner, når forstørrelse. Filerne er i programmappen: ingen online korttjenester. Farver af temaet Klar: lyseblå hav # E2ECF8, mørkeblå kystlinje, lysegrå grænser. I temaerne Black and Night farverne i basen er mørkere, i de andre temaer de farver lidt.

## 3. De to fremskrivninger

Alle lag passerer gennem samme projektionsfunktion, så de er altid afstemt med hinanden. Du vælger mellem to [10] fremskrivninger (λ længdegrad, φ breddegrad, R ray af Jorden):

Markedet. Opbevar lokale hjørner og former; Afstandene strækker sig mod polerne. Klip til ± 85 ° breddegrad.

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

Azimutale equidtanti centreret på din plads. Centeret (φ0, λ0) er centrum af din WWL kvadrat. Distancer og retninger målt fra midten er sande: en station på 1200 km er 1200 km i skala, og vinklen med nord er azimuth til hvilket punkt antennen. Når du ændrer lokalisatoren er kortet ombygget.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

I azimut linjerne på papiret er lige (meridianer, paralleller, firkantede kanter) bliver buede. Derfor er alle linjerne opdelt i korte sektioner, hvert afsnit projiceres, og så er egenskaberne forenet; det samme gælder for kanten af tropoindekset og for isoburet. Pointen med antipoderne i centrum ikke har en bestemt position og forbliver ude af syne.

## 4. Grill locator, zoom og flytte

Maidenhead gitter er designet af programmet: felter på 20 ° × 10 ° med de to store og falmede bogstaver i midten, kvadrater på 2 ° × 1st, og med tilstrækkelig zoom på 5 ′ × 2,5 ′. Linjerne er blå og tynde.

Zoom med hjulet centreret på markøren (punktet under musen forbliver støt), bevæger træk, dobbeltklik for at vende tilbage til hele visningen, + og − tasterne, og nøglen med den procentdel for hele visningen. Detaljer passer til zoom: finere kyster, sub- firkanter, tykkere isobare etiketter.

## 5. Hvorfor inversioner bøje bølger

I den lave atmosfære falder temperaturen normalt ca. 6,5 ° C pr. km; brydningsindekset afhænger af både temperatur og [3] fugt, og hvor det ændrer sig kraftigt med højden bølgen bøjer til jorden og ankommer længere end normalt. Det vigtigste lag er, at op til ca. 3 km, hvor inversioner, kanaler og konvektive celler [4] dannes; søstier favoriseres, fordi vandmasserne danner store områder af [4] inversion. [3]

Den størrelse, der anvendes til at måle den, er refraktiv N, dvs. (n − 1) · 106 [11]:

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

T er temperaturen i kelvin, P tryk og damptryk, begge i hPa.

Tæller som varieret N med andel (dN / dh). I standardatmosfæren er ca. - 39 N / km; op til − 79 N / km anses fraktionen for normal, foruden superfraktion, og med positive gradienter af subfraktion [7]. Under − 157 N / km er bølgen bøjet mere end jordens krumning og forbliver fanget: det er [11] (ducking) kanal. F5LEN

## 6. Tropoindekset: fra to kort til gennemsnittet

Hepburn og F5LEN faner er kontrolbordet: vise billeder hentet fra siden af hver kilde, med den redigerbare prognosetid, og tjene til at kalibrere aflæsning ved at klikke på referencepunkter og farver i legenden. Tropoindekslaget forbliver tomt, indtil mindst en af de to kalibreringer ikke er aktiv og komplet.

Hepburn. Hepburn-indekset måler, hvor mange bølger der forventes foldet over et område på en lineær skala fra 0 til 10, som også kan overstige 10. 0 betyder standard atmosfære og kun spredning; 1-2 marginale eller svage superrefraktion; 3-4 superrefraktion med kanal områder; Indekset tager ikke hensyn til den spredte tropo og ser ikke [1] bjergene. Kortene dækker 6 dage: 3-timers passerer de første 36 timer, derefter 6 timer, opdateret hver dag til 18 UTC [1]; startdata (temperatur, fugtighed, tryk) kommer fra den globale model af den canadiske vejrtjeneste [1]. [1]

F5LEN. For Europa F5LEN udgiver 31 kort fra + 3 til + 192 timer (hver 3. time op til + 48, hver 6 op til + 96, derefter hver 12), med den dato og tid gyldig i titlen, produktionstid nederst til højre og en klasse legende: Disturb, Normal, Minor, Week, Good, Strong, Vy Strong, Duct [2]. World prognose for F5LEN er beregnet af luft refraktion indeks i enhed N, på 40 trin af 3 timer (omkring 5 dage), opdateret 4 gange om dagen; et klik på kortet viser den gennemsnitlige værdi og lokalisatoren af dette punkt [2].

Foretrækker. For hver forudsigelse trin programmet udfører disse trin:

1. Decharge. Billederne af de to prognoser downloades i cachen ved siden af den eksekverbare, når at opdatere kilden, ikke til nogen redesign. Før den endelige version, bør der anmodes om tilladelse til Hepburn og F5LEN.
2. Automatisk georefering. Programmet finder rammen (to vandrette og to lodrette linjer af samme farve) og på Windows 10 / 11, læser med OCR integreret i systemet akslen etiketter (54N, 10W, 0, 5E...). Klip striberne rundt om rammen, forstør dem, afprøv begge tegn, når bogstavet N / S / E / W ikke læses og hold linjen hvor de fleste etiketter falder: de forkerte aflæsninger forbliver ude. Herfra får du den ekstreme lat / lon af rammen og typen af projektion (regelmæssig grill eller Mercator). Ekstremerne gemmer sig selv og genlæser sig selv, hvis rammen ændrer størrelse. På Mac og Linux, uden OCR, fabrik ekstremer eller håndskrevne dem er gyldige i fanen. Alternativt for kort, der ikke har læsbare akser: Hver type kort (Hepburn Europa, F5LEN på verdensplan...) har en kalibrering: nogle kontrolpunkter med kendte koordinater (kors af meridianer og paralleller, genkendelige ribben) forbinde pixels til bredde- og længdegrad, i fremskrivningen, der anvendes af denne kilde. Kalibreringen er i en fil ved siden af programmet, og du kan genskabe hvis en kilde ændrer format.
3. Farveaflæsning. Hver pixel sammenlignes med kilden legende og konverteres til den nærmeste numeriske værdi. Omkostninger, skrifter og net på det oprindelige kort anerkendes som "ingen data" og fyldes med de nærliggende værdier.
4. World grid. Hvert billede, når rammen er fundet og kendt sine ekstremer, er placeret på Jorden; værdien på ethvert tidspunkt i verden er afledt af de billeder, der dækker det. Hvis flere regioner dækker det samme punkt, er det værd at detaljer (flere pixels per grad), forudsat at dens tid er inden 1,5 h fra dette valg; så den nærmeste time, så den seneste race. Hvor intet billede dækker det punkt kortet viser et gråt slør: ingen data, som er forskellig fra "indeks 0". Inde i rammen, en pixel uden en legende farve er 0 (normal atmosfære); kyster, grænser og skrifter er sprunget ved at se på den nærmeste farve inden for 3 pixels.
5. Samme stige. Hepburn er allerede på skalaen 0-10 [1]: mørk baggrund = 0 og ti farver i rækkefølge (violet, blå, vandgrøn, lime, gul, okker, orange, rød, pink, lys pink) for 1... 10, lilla videre. F5LEN bruger klasser (Disturb, Normal, Minor, Week, Good, Strong, Vy Strong, Duct) [2], men nogle har præcis Hepburn farver: Svag = 1, stærk = 5, Vy Strong = 7, Duct = 8; Mindre er 0,5 og god (grøn) 3.5. Skalamultiplikatoren, i F5LEN fanen, korrigerer sættet for at gøre det sammenligneligt med Hepburn.
6. Samme tid. Tidslinjen går fra + 0 til + 192 timer i trin på 3 timer. For hvert trin du tager fra hver kilde den prognose, der gælder på nærmeste tidspunkt; Hvis en kilde ikke har det skridt, medierne kun bruger den anden og legenden angiver det.
7. Media. Med Media knappen hvert punkt er værd (Hepburn + F5LEN) / 2; med Hepburn eller F5LEN ser du kun én kilde, nyttigt at forstå, hvor de to prognoser er uenige.
8. Tegning. Værdien er farvet med legenden skala (0 blå, 2 grøn, 4 gul, 6 orange, 8 rød, 10 lilla) og projiceres som en prøveudtagning under gitteret. Den 0 er gennemsigtig, så hvor der ikke sker noget, ser du den rene grundlæggende kort; generel gennemsigtighed justerer med markøren ved siden af boksen.

På stedet fanen, indekset langs ruten er gennemsnittet af liga gitter værdier hver 10 km på den maksimale cirkel mellem de to stationer.

## 7. Isobare

Den høje stabile tryk er det første signal, der skal søges for [3] trope, og forbindelserne ud over IK3XTV analyserede alper har også været med højt [4] tryk.

1. Data. Tryk på havniveau fra den globale model GFS for NOAA [12], læs den gratis Open- Meteo [15] service på et gitter på 3 ° × 3 ° (fra 30 ° til 72 ° N, fra 30 ° V til 48 ° Ø) i de næste 78 timer. Programmet opdaterer data ved opstart og derefter hver 6 timer, ligesom modellen racing.
2. Linjer. Den isobare kommer fra nettet hver 4 hPa (... 1012, 1016, 1020...) med algoritmen af marchkvadrater (marchkvadrater), så de er lidt afrundet og projiceres trukket af kanalen.
3. Centre. De mest markante lokale maksimum og minima er markeret med H (blå) og L (rød) og værdien i hPa.
4. Etiketter. Værdien vises langs hver isobara, på en farvebaggrund på kortet for at forblive læselig over prøveudtagningen.

Område og trin. Dataene downloades på det område, du kigger på, udvidet med 25%, med et skridt fra 1. til 20 ° valgt at forblive under 400 point (Open- Meteo tæller hvert punkt som en anmodning: 600 per minut, 5000 per time): udvidelse eller flytte kortet ud af området allerede downloader programmet den mest tætte genindlæsning, til det maksimale hver 10 minutter; hvis en opdatering mislykkes de tidligere data forbliver, og det vil prøve igen efter 3 minutter. På denne måde kan isobare og temperaturer fungere overalt i verden.

Temperaturer. Samme anmodning bringer temperaturen til 2 m, til 925 hPa og til 850 hPa. LA0BY viser, at hændelsen af 22 / 11 / 2018 til UA6 blev set i temperaturkort (900- 3000 m), men ikke i Hepburn [3] forudsigelser; DF5AI og DK5YA bruge lodrette temperaturprofiler til at genkende [5] inversioner. Til dette er der to lag:

- Temperatur 850 hPa: isoterme behandle hver 4 ° C (rød over nul, blå nedenfor, lilla på nul).
- Termisk inversion: orange prøveudtagning, hvor luften ved 925 eller 850 hPa er varmere end ved 2 m; jo mere intens, jo mere inversion er stærk. Det er den fysiske tilstand, der bøjer bølgerne til jorden [3] [11], og sammenligne det med tropo indeks og isobare viser, hvis prognoserne har en vejrbase.

## 8. Spotter og stationer

Spot. Hvert sted med begge kendte positioner er en linje på den maksimale cirkel mellem de to stationer, opdelt i sektioner og projicerede; i Mercatore det vises derefter lidt buet, i azimutale det er lige, hvis det starter fra midten. Blå linje fuld til det normale sted, gul dækket med mørk kant til luftscatter (< AS >, < ACS > eller "airscatter" i kommentaren), en bold til hver station, lys glorie under linjen for at frigøre den fra prøvetagningen. Det valgte sted er oftere designet til sidst og med de to navne.

Lytter. Hver PSKReporter [16] modtager station er en mørk pink tom cirkel. Fra hver start fuld lyserøde linjer mod de stationer, der har afkodet og sendt tilbage til netværket, altid synlig uden at klikke, med navnene altid skrevet. Ved at vælge en cirkel dens linjer er tykkere. Spot og lytte viser kun det band og område, der er valgt i listen: ændre båndbredde kortet tømmer og redesigns med de data, der allerede er indsamlet for det nye band.

Besluttet af mig. Hver station, der afkoder med WSJT- X, JTDX eller MSHV (kvittering via UDP) er en rød linje fuld fra din QTH til stationen, med bold og nominering. Det er den mest direkte sammenligning med prognosen: viser, hvor din modtager virkelig kommer lige nu.

## 8 bis. Farver og symboler

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

Hvad er de lilla firkanter dækket ved 45 °. Trafikken væk fra din QTH interesser dig at forstå, hvor formeringen er åben, men dens linjer, hundredvis af kilometer lange og ofte tiere, dækker kortet og skjule, hvad der tæller for dig: åbningerne du kan udnytte fra din station. Af denne grund, ud over den valgte krone, programmet ikke trække linjerne, men farve locator firkanter (2 ° × 1 °) af de involverede stationer. Resultatet er et kort over "aktive zoner": hvor mange nærliggende lilla firkanter vises, er der en kanal eller åbning, selv om det ikke vedrører dig direkte; Hvis området udvider sig mod dig, er det signalet til at pege antennen i den retning. Den 45 ° dashboard lader dig se under tropo indeks og gitter, så de sammenligne med et blik prognosen (nederste farve) og den reelle trafik (lilla). Din QTH firkant aldrig farver, fordi det er udgangspunktet og ikke et fjernt område. Kun de firkanter, der er alle ud over kronen er farvet: et kvadrat krydset af kronen, eller inde, forbliver fri, selv om der er en af de to stationer. For at se linjerne, skal du blot slukke for boksen eller vælge en større krone.

De kriterier, der afgør, hvad der kommer på kortet, og hvad der kasseres, forklares en efter en i dokument 1, afsnit 11 ter.

## 9. Tidslinjen

Nedenfor på kortet: dato og tidspunkt for forudsigelsen vist, markør fra + 0 til + 72 h ved 6 timer, indeks legende 0-10 med navnet på den valgte kilde, og tid af modellen race for isobare. Trompet og isobare indeks følge markøren; spots, stationer lytter og dine dekodninger er observationer af øjeblikket og altid forblive dem "adex". Når markøren ikke er på + 0, svækkes de, for at huske, at de ikke tilhører prognosetiden.

## 10. Hvordan man læser overlapningen

- Højtryk + højt indeks + plet langs det farvede område: åbningen er der og er den planlagte. Det er på tide at ringe i den retning.
- Høj indeks, men ingen plet: eller ingen er aktiv der, eller forudsigelse er for optimistisk. Lyttelaget hjælper med at skelne: Hvis der er modtagere i området og føler ingenting, den anden hypotese er mere sandsynligt.
- Lange pletter uden for de farvede områder: høj kanal, som overfladen prognoser ikke ser, som i LA- UA6 [3] begivenhed; eller en anden formering undslap fra filteret (fx E sporadisk uden tag). DF5AI og DK5YA adskiller tropo og E sporadisk også fra timen: åbninger, der er født og ender sammen med en diffus E sporadisk er næppe [5] tropo.
- Hepburn og F5LEN er uenige: sammenligne de to kilder med knapperne og se, hvor de virkelige pletter falder.
- Tryk ned: G4MCU fulgte en åbning på SK6VHF -signalet indtil trykket faldt; 1012 hPa betragtede den færdige begivenhed [13]. Se isobare ved at rulle tidslinjen: hvis det høje tryk trækkes tilbage, åbningen sandsynligvis følger det.
- Signaler, der går op: i WSPR studiet på 144 MHz af KP4MD signalet / støjforholdet voksede lige i de vinduer, hvor Hepburn forudså en forbedring, og faldt, da prognosen fjernede [14]. hvis på samme vej vokser, åbningen danner.
- Mountain stier: Indekset tager ikke højde for [1] relieffer, men forbindelserne ud over Alperne findes også takket være konvektive celler og bølger af atmosfærisk tyngdekraft [4], og LA0BY arbejdede 9A og S5 gennem bjergene uden Hepburn forudsige det [3].

## 11. Åbent punkt

- Tilladelse af Hepburn og F5LEN til at bruge deres kort i programmet.
- Kalibrering af omregningskoefficienter F5LEN → skala 0-10.
- Kalibrering af kontrolpunkter af enhver form for kort, der skal remakes, hvis kilder ændrer grafik.
- Valgfri rute med temperatur ved højde (850 hPa) fra GFS.

## Vigtigste kilder

1. W. R. Hepburn, DX Info Centre, "The Hepburn Tropo Index (HTI)" og "Tropospheric Ducting Prognose" sider (dxinfocentre.com). Indeks definition og skala, negative værdier og mere end 10, udelukkelse af scatters og relieffer, trin på 3 og 6 timer på 6 dage, opgradere til 18 UTC. Oprindelse af data fra den globale canadiske model: G. Meisse, AMSAT- BB, 27 / 08 / 2007.
2. F5LEN, prognose for Europa (tropo.f5len.org / provastsfor- europe) og verdens forudsigelse af VHF tropofærisk formering (tropo.f5len.org / WW) og bemærkede "Ny version af VHF troposfærisk formering prognose værktøj". Unit indeks N, 40 skridt fra 3 timer, 4 opdateringer pr. dag, værdi og lokalisator per klik.
3. S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz og op", 41 ° Nordic VUSHF Meeting, Skjeberg 2019. Normal gradient 6,5 ° C / km og inversioner op til 10-15 ° C / km, temperatur og fugtighed rolle, højt stabilt tryk, LA- UA6 begivenhed synlig i høj temperatur og ikke i Hepburn, LA- 9A / S5 forbindelser uden for bjergene.
4. F. Egano IK3XTV, "Formering VHF - Bidraget fra konvektive celler og atmosfæriske gravitationsbølger i tropo formering". Betydningen af de første 3 km, marine stier, konvektive celler, tyngdebølger på Alperne, højtryksforbindelser, fælles volumen op til 3-4 km.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, "very long distance propagation in the 144 MHz Band", 24 / 7 / 2003. Vertikale profiler af temperatur og dugpunkt til at genkende inversioner, brug af Hepburn indeks, sondring mellem tropo og E sporadisk efter timen.
6. J. N. Gannaway G3YGF, "Troposfærisk Scatter Formering", QST november 1983. Earth radius ækvivalent og tab af sti, der anvendes til indeks og spot card (se dokument 1).
7. VHF / UHF DX-bogen, kapitel 2 "Troposfærisk formering", s. 2-23... 2-30. Figur over brydningszoner i henhold til dN / dh (standard − 39 N / km, grænseværdier − 79 N / km og 0) og overflade- og højkanalprofiler. Teksten til den medfølgende kopi kan ikke læses: kun tallene blev anvendt.
8. Projekt resumé af 8 / 11 / 2026, leveret af forfatteren af programmet. Kort farver, nødvendige fremskrivninger, lag, tidslinje.
9. Naturlig Jord, vektordata for kyster og grænser, offentligt domæne (naturaljorddata.com). Generel henvisning, ikke vedlagt.
10. J.P. Snyder, "Kortfremskrivninger - en arbejdsmanual", USGS Professional Paper 1395, 1987. Formuler af Mercator fremskrivninger og azimutale equidtanti. Generel henvisning, ikke vedlagt.
11. Anbefaling ITU-R P.453, radiofoniindeks. Refraktiv formel N og kanaltærskel ved − 157 N / km. Generel henvisning, ikke vedlagt.
12. NOAA NCEP, Global Prognose System (GFS). Tryk på havoverfladen ved 0,25 °, fire løb om dagen. Generel henvisning, ikke vedlagt.
13. G4MCU, artikler med mærke "troposfærisk" (g4mcu.net / tag / troposfærisk), 2018- 2025. Fraktion, ledning og spredning; hyppigere kanaler med højt tryk, især på havet; beacon SK6VHF fulgt op til faldet i trykket ved 1012 hPa (juni 2019).
14. C. F. Milazzo KP4MD, "VHF / UHF WSPR Formeringsundersøgelse" (qsl.net / kp4md / 144 _ mhz _ wspr.htm), 2012- 2014. SNR af WSPR rapporter til 144 MHz voksende i forbedringsvinduer forudsagt af Hepburn og faldende, når prognosen forsvinder.
15. Open- Meteo, vejrudsigt API (open- meteo.com), gratis ikke-kommerciel brug. Tryk på havoverfladen af GFS-modellen på flere punkter i én anmodning.
16. mqtt.pskreporter.info, distribution MQTT i realtid af reklamer pskreporter. info: nominering og udlejer af modtagere (se dokument 1).
