LIVETROPOVHF · Teknisk dokument 2
# Bygging av kartet

> Automatisk oversettelse av italiensk: i tilfelle tvil er verdt den italienske guiden.

Som kartet over fanen Kart og sted er født: den geografiske basen, fremspringene, gjennomsnittet av Hepburn og F5LEN indekser, isobar, flekker og stasjoner lytte, overlappet med lag. Versjon av 9. november 2026.

## Sammendrag

- Kartet er designet av programmet i åtte lag, hver med sin [8] boks.
- Base: Natural Earth kystlinje og grenser til to nivåer av [9] detalj; [10] Market og azimuth projeksjoner.
- Fysikk: inversjoner, reaktivitet N og gradient dN/dh, blygrense ved −157 N/km [3] [4] [7] [11].
- Throat-indeks: kalibrert lesing av Hepburn [1] og F5LEN [2] -kart, felles rutenett og gjennomsnitt av de to kildene.
- Isobare: GFS [12] trykk via Open-Meteo [15], linjer hver 4 hPa, H og L sentre.
- Spot, lyttestasjoner (mørk rosa sirkler med alltid synlige rosa linjer) og personlige dekodinger i rødt.
- Lese overlappingen: høytrykk, høykanaler, trykkfall, voksende SNR, [3] [4] [5] [13] [14] fjellruter.

## 1. Prinsippet: Et lagdelt kart

Kartet er ikke et fast bilde: er designet av programmet, lag over lag, når du endrer zoom, projeksjon, band, prognosetid eller tema. Hvert lag har sin boks i den øverste laglinjen på kartet, slik at du kan bare holde det du trenger og se alt sammen når du vil ha det fulle bildet. Bestillingen, fra nedenfor, er dette:

|  #  | Veien | Innhold | Casella |
|  1  | Grunnleggende kart | Hav, land, kyster og grenser | Ja |
|  2  | Index Tropo | Media Hepburn + F5LEN, eller bare én kilde; justerbar gjennomsiktighet | Ja |
|  3  | Grill locator | Felt, firkanter og, med zoom, kvadrat | Ja |
|  4  | Isobare | Trykklinjer hver 4. HPa, senter H og L | Ja |
|  4a  | Temperatur 850 hPa | Isotermisk behandlet hver 4 ° C | Ja |
|  4b  | Termisk inversjon | Orange prøve hvor luften i høyden er varmere enn på bakken | Ja |
|  5  | PSKReporter | Motta stasjoner av PSKReporter (mørk rosa sirkler) og i rosa, hva de hører | Ja |
|  6  | Avkodet av meg | Dekodingsstasjoner med WSJT-X, JTDX eller MSHV: Røde linjer fra QTH | Ja |
|  6a  | QSO desunti dal live | Lukket tilkoblinger ( RR73, 73, RRR) les i dekodingene: grønn linje mellom de to stasjonene | Ja |
|  7  | Spotlight | Tilkoblinger: full blå linje, gul dekket for airscatter, to baller | Ja |
|  7a  | Utelukkelser | Casella Utelatt under 100 km: den korteste trafikken er verken trukket eller oppført. Casella Mer enn 1000 km: bare firkanter (starter): Hvis én av stasjonene er over 1000 km fra din QTH, ingen linjer eller sirkler, bare lokaliseringsplassene til stasjonene som er involvert i lilac behandlet på 45° | Ja |
|  8  | QTH og etiketter | Din stasjon, navnene på det valgte stedet | Alltid synlig |

Tropoindeksen er umiddelbart over basen og under alt annet: er en farget prøvetaking, og høyere plassert ville dekke linjer og symboler. Nettet passerer over det, så du vet alltid hvor et farget område faller. Isobarene er tynne linjer og leses godt over prøvetakingen.

## 2. Det grunnleggende kartet

Kostnader, grenser og land kommer fra naturlige jordvektordata, av det offentlige domenet [9], til å skalere 1:50 millioner for det totale syn og 1:10 millioner når magnified. Filene er i programmappen: ingen online karttjenester. Klar: lys blå sjø # E2ECF8, mørk blå kyst, lyse grå grenser. I temaene Black and Night er fargene på basen mørket, i de andre temaene de farger litt.

## 3. De to fremspringene

Alle lag passerer gjennom den samme projeksjonsfunksjonen, så de er alltid innrettet med hverandre. Du velger mellom to [10] projeksjoner (λ lengdegrad, φ breddegrad, R stråle av jorden):

Marked. Lagre lokale hjørner og former; Avstandene strekker seg mot polene. Klipp til ± 85° breddegrad.

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

Azimutale equidtanti sentrert på plassen din. Senteret (φ0, λ0) er sentrum av WWL-plassen. Avstander og retninger målt fra sentrum er sant: En stasjon på 1200 km er 1200 km i skala, og vinkelen med nord er azimuten som skal peke antennen. Når du endrer lokatoren, fokuserer kartet på nytt.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

I azimutalen blir linjer som på papiret er rett (meridianere, paralleller, firkantede kanter) buet. Av denne grunn er alle linjer delt inn i korte trekk, hver seksjon er projisert og deretter egenskapene er forent; det samme gjelder kantene av tropoindeksen og isobaren. Punktet til antipoder i sentrum har ikke en bestemt posisjon og forblir ute av syne.

## 4. Grill locator, zoom og flytt

Maidenhead-gitteret er designet av programmet: felt på 20° × 10° med de to store og falmede bokstavene i sentrum, kvadrater på 2° × 1st, og med tilstrekkelig zoom subquadrati på 5′ × 2,5′. Linjene er blå og tynne, for ikke å dekke tropoindeksen.

Zoom med hjulet sentrert på pekeren (punktene under musen forblir stabile), bevegelig dra, dobbeltklikk for å returnere til hele visningen, + og - nøkler, og tasten med prosentandelen for hele visningen. Detaljer passer til zoomen: finere kyster, undersquare, tykkere isobar etiketter.

## 5. Hvorfor inversjoner bøye bølgene

I den lave atmosfære faller temperaturen normalt med ca. 6,5 °C per km; Den brytningsindeksen avhenger av både temperatur og [3] fuktighet, og der den endres kraftig med høyden bølgen bøyer til bakken og kommer lenger enn normalt. Det viktigste laget er at opp til 3 km, hvor inversjoner, kanaler og konvektive celler [4] dannes; sjøveier favoriseres fordi vannmassene danner store områder av [4] -inversjon. [3]

Størrelsen som brukes til å måle den er brytning N, dvs. (n − 1) · 106 [11]:

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

T er temperaturen i kelvin, P trykket og damptrykket, både i hPa.

Teller som varierte N med aksje (dN/dh). I standardatmosfæren er ca. -39 N/km; opp til -79 N/km anses brytningen som normal, i tillegg til super-brytning, og med positive gradienter av under-brytning [7]. Under −157 N/km bøyes bølgen mer enn krumningen av jorden og forblir fanget: den er [11] (dukting) kanalen. Dette er betingelsene som Hepburn og F5LEN -kartene prøver å forutsi.

## 6. Tropoindeksen: fra to kart til gjennomsnitt

Hepburn- og F5LEN -faner er kontrollbordet: vis bildene som er lastet ned fra siden til hver kilde, med den endrelige forutsigbare tiden, og tjener til å kalibrere leset ved å klikke på referansepunktene og fargene i legenden. Tropoindekslaget forblir tomt inntil minst én av de to kalibreringene ikke er aktivt og fullstendig.

Hepburn. Hepburnindeksen måler hvor mye bølger som forventes å bli foldet over et område, på lineær skala fra 0 til 10 som også kan overstige 10. 0 betyr standard atmosfære og bare scatter; 1–2 marginal eller svak super-brytning; 3–4 super-bryting med kanalområder; Indeksen tar ikke hensyn til scatter Tropo og ser ikke [1] -fjellene. Kartene dekker 6 dager: 3-timers passerer i de første 36 timene, deretter 6 timer, oppdatert hver dag til 18 UTC [1]; startdata (temperatur, fuktighet, trykk) kommer fra den globale modellen av den kanadiske værtjenesten [1]. [1]

F5LEN. For Europa F5LEN publiserer 31 kart fra +3 til +192 timer (hver 3 timer opp til +48, hver 6 opp til +96, deretter hver 12), med dato og klokkeslett gyldig i tittelen, produksjonstid nederst til høyre og en klasselegende: Disturb, Normal, Minor, Svak, God, Sterk, Vy Strong, Duct [2]. Verdensprediksjon av F5LEN beregnes av luftbrytningsindeksen i enhet N, på 40 trinn på 3 timer (ca. 5 dager), oppdatert 4 ganger om dagen; et klikk på kartet viser gjennomsnittlig verdi og lokatoren av det punktet [2].

Fortsetter. For hvert forutsigelsestrinn utfører programmet disse trinnene:

1. Frigjøring. Bildene av de to prognoserne lastes ned i cache ved siden av kjørbare, en gang for å oppdatere kilden, ikke til noen redesign. Før den endelige versjonen bør det be om tillatelse til Hepburn og F5LEN.
2. Automatisk georeferencing. Programmet finner rammen (to horisontale og to vertikale linjer med samme farge) og, på Windows 10/11, leser med OCR integrert i systemet akseletikettene (54N, 10W, 0, 5E...). Kutt stripene rundt rammen, forstørre dem, test begge tegnene når bokstaven N/S/E/W ikke leser og hold linjen som de fleste etikettene faller på: feil avlesning forblir ute. Herfra kan du få den ekstreme lat/lon av rammen og typen projeksjon (vanlig grill eller Mercator). Ekstremene sparer seg selv og releser seg selv hvis rammen endrer størrelse. På Mac og Linux, uten OCR, fabrikk ekstremiteter eller håndskrevne er gyldige i fanen. Alternativt, for kart som ikke har lesbare akser: Hver type kart (Hepburn Europa, F5LEN over hele verden...) har en kalibrering: noen kontrollpunkter med kjente koordinater (over meridianer og paralleller, gjenkjennelige ribben) koble piksler til breddegrad og lengdegrad, i projeksjonen som brukes av den kilden. Kalibrering er i en fil ved siden av programmet, og du kan ta om hvis en kilde endrer format.
3. Fargelesning. Hver piksel sammenlignes med kildelegenden og omdannes til den nærmeste numeriske verdien. Kostnader, skrifter og rutenett som tegnes på det opprinnelige kartet er anerkjent som \"ingen data\" og fylt med de nærliggende verdiene.
4. Verdensnettet. Hvert bilde, når rammen er funnet og kjent som dens ekstremer, er plassert på jorden; verdien når som helst i verden er avledet fra bildene som dekker den. Hvis flere regioner dekker samme punkt, er det verdt å detaljere (mer piksler per grad), forutsatt at tiden er innen 1,5 timer fra det valget; så den nærmeste timen, så den siste løp. Der ingen bilder dekker punktet kartet viser et grått slør: ingen data, som er forskjellig fra \"index 0\". Inne i rammen er en piksel uten legendefarge 0 (normal atmosfære); kyster, grenser og skrifter hoppes ved å se på nærmeste farge innen 3 piksler.
5. Same stige. Hepburn er allerede på skala 0–10 [1]: mørk bakgrunn = 0 og ti farger i rekkefølge (violett, blå, vanngrønn, lime, gul, oker, oransje, rød, rosa, lys rosa) for 1...10, lila utover. F5LEN bruker klasser (Disturb, Normal, Minor, Svak, God, Sterk, Vy Strong, Duct) [2], men noen har nøyaktig Hepburn farger: Svak = 1, Sterk = 5, Vy Strong = 7, Duct = 8; Minor er 0,5 og god (grønn) 3,5. Skala multiplikatoren i F5LEN -fanen korrigerer settet for å gjøre det sammenlignbart med Hepburn.
6. Same tid. Tidslinjen går fra +0 til +192 t ved trinn 3 timer. For hvert trinn du tar fra hver kilde prognosen gyldig på nærmeste tidspunkt; Hvis en kilde ikke har det trinnet, media bruker bare den andre og legenden indikerer det.
7. Media. Med Media-knappen er hvert punkt verdt (Hepburn + F5LEN) / 2; med Hepburn eller F5LEN du ser bare én kilde, nyttig å forstå hvor de to prognoser er uenige.
8. Tegning. Verdien er farget med legendeskalaen (0 blå, 2 grønn, 4 gul, 6 oransje, 8 rød, 10 lilla) og projisert som en prøvetaking under rutenettet. 0 er gjennomsiktig, så der ingenting skjer ser du det rene grunnleggende kartet; generell åpenhet justeres med markøren ved siden av boksen.

På spot-fanen er indeksen langs ruten gjennomsnittet av rutenettverdier mesterskap hver 10 km på den maksimale sirkelen mellom de to stasjonene.

## 7. Isobare

Det høye stabile trykket er det første signal som søkes etter [3] -tropen, og forbindelsene utenfor IK3XTV -analyserte alper har også vært med høyt [4] -trykk.

1. Data. Trykk på havnivå fra den globale modellen GFS i NOAA [12], les gjennom den gratis Open-Meteo [15] -tjenesten på et rutenett på 3° × 3° (fra 30° til 72° N, fra 30° V til 48° E), i de neste 78 timer. Programmet oppdaterer data ved oppstart og deretter hver sjette time, som modellen racing.
2. Linjer. Isobaren oppnås fra rutenettet hver 4 hPa (...1012, 1016, 1020...) med algoritmen til marsjeringsfirkantene (marsjingfirkanter), så er de litt avrundet og projisert tegnet ved traktat.
3. Senter. De mest markerte lokale maksimum og minimum er merket med H (blå) og L (rød) og verdien i hPa.
4. Etiketter. Verdien vises langs hver isobara, på en fargebakgrunn på kartet for å forbli lesbar over prøvetakingen.

Område og trinn. Dataene lastes ned på området du ser på, utvides med 25%, med et steg fra 1. til 20° valgt til å forbli under 400 poeng (Åpne-Meteo teller hvert punkt som en forespørsel: 600 i minuttet, 5000 i timen): ved å utvide eller flytte kartet ut av det området som allerede har lastet ned programmet den mest tette reloaden, på det meste hvert 10. minutt; hvis en oppdatering mislykkes de forrige dataene forblir og det vil prøve igjen etter 3 minutter. På denne måten isobar og temperaturer fungerer hvor som helst i verden.

Temperaturer. Den samme forespørselen bringer temperaturen til 2 m, til 925 hPa og til 850 hPa. LA0BY viser at hendelsen av 22/11/2018 til UA6 ble sett i temperaturkart (900–3000 m), men ikke i Hepburn [3] spådommer; DF5AI og DK5YA bruker vertikale temperaturprofiler for å gjenkjenne [5] inversjoner. For dette er det to lag:

- Temperatur 850 hPa: isoterme behandler hver 4 ° C (rød over null, blå under, lilla på null).
- Termisk inversjon: oransje prøvetaking der luft ved 925 eller 850 hPa er varmere enn det ved 2 m; jo mer intens, jo mer inversjon er sterk. Det er den fysiske tilstanden som bøyer bølgene til bakken [3] [11], og sammenligner den med tropoindeksen og isobar viser om prognoserne har en meteorologisk base.

## 8. Spots og stasjoner

Spot. Hvert sted med begge kjente posisjoner er en linje på den maksimale sirkelen mellom de to stasjonene, fordelt på seksjoner og projisert; I Mercatore det vises så litt buet, i azimutale det er rett hvis det starter fra sentrum. Blå linje full for normalflekken, gul dekket med mørk kant for airscatteren (<AS>, <ACS> eller \"luftskatter\" i kommentaren), en ball for hver stasjon, lys halo under linjen for å løsne den fra prøvetakingen. Det valgte stedet er oftere designet for sist og med de to navnene.

Hører. Hver PSKReporter [16] mottakerstasjon er en mørk rosa tom sirkel. Fra hver start full rosa linjer mot stasjonene som har dekodet og sendt tilbake til nettverket, alltid synlig uten å klikke, med navnene alltid skrevet. Ved å velge en sirkel er dens linjer tykkere. Spot og lytting viser bare bandet og spekteret som er valgt i listen: å endre båndbredde kartet er tomt og redesignet med data som allerede er samlet inn for det nye bandet.

Bestemt av meg. Hver stasjon som dekoder med WSJT-X, JTDX eller MSHV (receipt via UDP) er en rød linje full fra QTH til stasjonen, med ball og nominasjon. Det er den mest direkte sammenligningen med prognosen: viser hvor mottakeren din virkelig kommer akkurat nå.

## 8 bis. Farger og symboler

Hver farge har bare én betydning, lik i alle temaer (endre bare bakgrunnen).

| Farge og tegn | Betydning |
| Kontinuerlig blå linje | Spot fra DX-hopen, RBN eller DM4X mellom to stasjoner |
| Gul linje med mørk kant | Spot med airscatter kommentar (refleksjon på fly) |
| Mørk rosa tom sirkel | Lyttende stasjon på PSKReporter (MQTT) |
| Tynn rosa linjer fra sirkelen, med en rosa ball nederst | Stationer som lytteren hørte, med navnet alltid skrevet; tykkere linjer for den valgte lytteren |
| Rød linje og rødt navn | Dine UDP dekodinger (WSJT-X, JTDX, MSHV): Hvem er du? |
| Rød linje blinkende slag | QSO i gang med denne stasjonen: Han ringte deg og stengingen er ikke kommet ennå, eller er DX Call av WSJT-X/JTDX med aktivert overføring. Fortsett med RR73, RRR eller 73 |
| Grønn linje | QSO desunti dal live: stenger RR73, RRR eller 73 UDP-visning |
| Svart linje med hvit grense | Noen ringer deg: det varer opp til RR73/RRR /73 eller opp til 3 minutter |
| Kvadrater på 45° | Trafikk med minst én stasjon utover den valgte kronen: Ingen linjer, bare firkanter locator |
| Søk etter Lillas med avstand til nord | 1000, 700 og 500 km rundt din QTH; det valget er mer merket |
| Sirkel og punkt i fargen på temaet | Din QTH, med navn og utleier |
| Orange ball med GRAVES 143 050 | Radaren GRAVES ( JN27SI) |
| Prøve fra blå til rød | Hepburn tropeindeks (0,0) eller F5LEN klasse, eller deres gjennomsnittlige; varmere farge, bedre forhold |
| Lys grå fløyel | Område uten tropodata for den valgte tiden |
| Grå-blå linjer med verdien i hPa | Isoberere hver 4 hPa; H i blått høyt trykk, L i rødt lavt trykk |
| Røde, blå og lilla prikkede linjer | Isothermia til 850 hPa hver 4 ° C: rød over null, blå under null, lilla null |
| Prøve oransje | Termisk reversering: luft til 925 hPa varmere enn på bakken, mer intens med sterkere inversjon |

I den mistenkte rapporterer kanten av hver fane sier regelen i kraft: grønn God, rød Scarta, grå ingen regel.

Hva er lilac firkanter perforert på 45°. Trafikken bort fra din QTH interesserer deg å forstå hvor utbredelsen er åpen, men dens linjer, hundrevis av kilometer lang og ofte tiere, dekker kartet og skjule det som teller for deg: åpningene du kan utnytte fra stasjonen. Av denne grunn, utover den valgte krone, trekker programmet ikke linjene, men farger lokatorens kvadrater (2° × 1°) av stasjonene som er involvert. Resultatet er et kart over \"aktive soner\": hvor mange nærliggende lilac firkanter vises, er det en kanal eller en åpning, selv om det ikke bekymrer deg direkte; Hvis området utvider seg mot deg, er det signalet å peke antennen i den retningen. 45° dashboard lar deg se under tropoindeksen og rutenettet, så sammenlign med et blikk prognosen (bunnfarge) og den virkelige trafikken (illus). Din QTH-plass farger aldri, fordi det er utgangspunktet og ikke et fjernt område. Bare firkantene som er alle utenfor kronen er farget: en firkant krysset av kronen, eller inne, forblir fri selv om det er en av de to stasjonene. For å se linjene, bare slå av boksen eller velg en større krone.

Kriteriene som bestemmer hva som kommer på kartet og hva som kastes, forklares én etter én i dokument 1, avsnitt 11 ter.

## 9. Tidslinjen

Nedenfor på kartet: dato og klokkeslett for den viste forutsigelsen, markør fra +0 til +72 t ved 6 timer, indeks legende 0–10 med navnet på den valgte kilden, og tidspunktet for modellløpet for isobar. Trumpet og isobarindeksen følger markøren; flekker, stasjoner som lytter og dekodingene dine er observasjoner av øyeblikket og forblir alltid de \"adex\". Når markøren ikke er på +0 blir de svekket, for å huske at de ikke tilhører prognosetiden.

## 10. Hvordan lese overlappen

- Høytrykk + høy indeks + spot langs det fargede området: åpningen er der og er den som er planlagt. Det er på tide å ringe i den retningen.
- Høy indeks men ingen sted: eller ingen er aktiv der, eller prognose er for optimistisk. Lyttelaget bidrar til å skille mellom: Hvis det er mottakere i området og ikke føler noe, er den andre hypotesen mer sannsynlig.
- Lange flekker utenfor de fargede områdene: høy kanal, som overflateprognosene ikke ser, som i LA- UA6 [3] hendelsen; eller en annen utbredelse rømt fra filteret (f.eks. E sporadisk uten tag). DF5AI og DK5YA skiller tropo og E sporadisk fra timen: åpninger som er født og ender opp sammen med en diffus E sporadisk er knapt [5] tropo.
- Hepburn og F5LEN er uenige: Sammenlign de to kildene med knappene og se hvor de virkelige punktene faller.
- Trykkned: G4MCU fulgte en åpning på SK6VHF -beacon inntil trykket falt; 1012 hPa vurdert den ferdige hendelsen [13]. Se isobaren ved å rulle tidslinjen: Hvis det høye trykket trekker seg ut, vil åpningen sannsynligvis følge den.
- Signaler som går opp: i WSPR-studien ved 144 MHz i KP4MD vokste signal/støyforholdet rett i vinduene hvor Hepburn forutsa en forbedring, og falt når prognosen tok [14]. Hvis på samme vei vokser, dannes åpningen.
- Fjellveier: indeksen tar ikke hensyn til [1] reliefs, men forbindelsene utover Alpene eksisterer også takket være konfektive celler og bølger av atmosfærisk tyngdekraft [4], og LA0BY jobbet 9A og S5 gjennom fjellene uten å forutsi det [3].

## 11. Åpne punkt

- Tilgang til Hepburn og F5LEN til å bruke sine kart i programmet.
- Kalibrering av konverteringskoeffisienter F5LEN → skala 0-10.
- Kalibrering av kontrollpunkter på alle slags kart, å bli remake hvis kilder endrer grafikk.
- Valgfri rute med temperatur i høyde (850 hPa) fra GFS.

## Hovedkilder

1. W. R. Hepburn, DX Info Centre, \"The Hepburn Tropo Index (HTI)\" og \"Tropospheric Ducting Prognos\" sider (dxinfocenter.com). Indexdefinisjon og skala, negative verdier og mer enn 10, utelukkelse av scatters og reliefs, trinn på 3 og 6 timer på 6 dager, oppgradering til 18 UTC. Opprinnelse av data fra den globale kanadiske modellen: G. Meisse, AMSAT-BB, 27/08/2007.
2. F5LEN, værvarsel for Europa (tropo.f5len.org/forecasts-for-European) og verdensprediksjon av VHF tropopheric utbredelse (tropo.f5len.org/WWW) og bemerket \"Ny versjon av VHF tropospheric utbredelsesprognose verktøyet\". Enhetsindeks N, 40 trinn fra 3 timer, 4 oppdateringer per dag, verdi og lokator per klikk.
3. S. Heck LA0BY, “Extreme Tropo Utbreiing på 144 MHz og oppover”, 41° Nordic VUSHF Meeting, Skjeberg 2019. Normal gradient 6,5 ° C/km og inversjoner opp til 10–15 ° C/km, temperatur og fuktighet rolle, høyt stabilt trykk, LA– UA6 hendelse synlig i høy temperatur og ikke i Hepburn, LA–9A/ S5 forbindelser utover fjellene.
4. F. Egano IK3XTV, \"Propagasjon VHF - bidraget fra konfektive celler og atmosfæriske gravitasjonsbølger i Tropo utbredelse.\" Viktigheten av de første 3 km, marine stier, konfektive celler, gravitasjonsbølger på Alpene, høytrykksforbindelser, felles volum opp til 3-4 km.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, “Meget langdistanseutbreiing i 144 MHz bandet”, 24/7/2003. Vertikale profiler av temperatur og duggpunkt for å gjenkjenne inversjoner, bruk av Hepburn-indeksen, skille mellom tropo og E sporadisk i henhold til timen.
6. J. N. Gannaway G3YGF, «Tropopheric Scatter Expansion», QST november 1983. Jordradius ekvivalent og tap av bane som brukes til indeks- og spotkort (se dokument 1).
7. VHF/UHF DX-bok, kapittel 2 \"Troposfærisk utbredelse\", s. 2-23...2-30. Figur av brytningssoner i henhold til dN/dh (standard-39 N/km, grenser-79 N/km og 0) og overflate- og høykanalprofiler. Teksten til den leverte kopien er ikke lesbar; bare tallene ble brukt.
8. Prosjektsammendrag på 8/11/2026, gitt av forfatteren av programmet. Kartfarger, nødvendige projeksjoner, lag, tidslinje.
9. Naturlig jord, vektordata fra kyster og grenser, offentlig domene (naturalearthdata.com). Generell referanse, ikke vedlagt.
10. J. P. Snyder, «Map Projections – En arbeidsmanual», USGS Profesjonell papir 1395, 1987. Formler av Mercator projeksjoner og azimutale equidtanti. Generell referanse, ikke vedlagt.
11. Anbefaling ITU-R P.453, radio brytningsindeks. Refraktiv formel N og kanalgrense ved −157 N/km. Generell referanse, ikke vedlagt.
12. NOAA NCEP, Global Prognose System (GFS). Trykk på havet ved 0,25°, fire løp per dag. Generell referanse, ikke vedlagt.
13. G4MCU, artikler med tag \"tropospheric\" (g4mcu.net/tag/tropospheric), 2018-2025. Refrasjon, kanal og scatter; hyppigere kanaler med høyt trykk, spesielt på sjøen; beacon SK6VHF fulgte opp til dråpen i trykk ved 1012 hPa (juni 2019).
14. C. F. Milazzo KP4MD, “VHF/UHF WSPR utbredelsesstudie” (qsl.net/kp4md/144_mhz_wspr.htm), 2012–2014. SNR i WSPR rapporterer til 144 MHz økende i forbedringsvinduene som Hepburn hadde forutsagt og falt når prognosen forsvinner.
15. Open-Meteo, værvarsel API (open-meteo.com), gratis ikke-kommersiell bruk. Trykk på havnivå i GFS-modellen på flere punkter i én forespørsel.
16. mqtt.pskreporter.info, distribusjon MQTT i sanntid av reklamer av pskreporter. info: nominasjon og utleier av mottakere (se dokument 1).
