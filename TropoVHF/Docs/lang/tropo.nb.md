LIVETROPOVHF · Teknisk dokument 3
# Hvordan danne trope i 144 MHz og videre

> Maskinoversettelse fra italiensk: ved tvil gjelder den italienske veiledningen.

Hva som skjer i luften når 144, 432 og mikrobølger går utover horisonten: brytning, inversjoner, kanaler, strøer og hvordan du kan lese dem på programkartet. Versjon av 9. november 2026.

## Sammendrag

- VHF bølgene og utover reiser i de første kilometer av atmosfæren (troposfæren) og bøying der brytningsindeksen endres med [3] [7] [11].
- Indeksen måles med brytning N, som avhenger av trykk, temperatur og fremfor alt [11] fuktighet.
- I normal atmosfære er det ca. 40 enheter pr. km; når den faller mer enn 157 enheter per km, forblir bølgen fanget i en [7] [11] -kanal.
- Kanalen kommer fra en termisk inversjon med tørr luft over og våt under: antisyklon, hav, rolige netter, [3] [4] [5] [13] fronter.
- Jo høyere frekvensen, jo tynnere kanalen er nok: mikrobølgeovnene åpner oftere enn 144 [3] [7].
- Uten kanalen forblir troposferspreaden: alltid tilstede, svak, som faller med avstand [6].
- På programmet: høyt trykk + reverse + Hepburn/ F5LEN høy = sannsynlige forhold; punktene bekrefter [1] [2] [13] [14].

## 1. Hvor det skjer: Troposfæren

Troposfæren er det laveste laget av atmosfæren, opp til 10-12 km. For trope teller hovedsakelig de første 3 km: det er vanndamp og inversjoner, og det passerer radiorutene mellom stasjoner på bakken [4]. Under normale forhold faller temperaturen med høyden på ca. 6,5 ° C per km [3]. Når derimot i et lag stiger temperaturen med høyden, er det utgangspunktet for nesten alle de sterke tropoåpningene.

VHF-ene reflekteres ikke av ionosfæren som HF (salve og sporadiske, som har andre timer og andre avstander [5]): forbi horisonten kommer bare hvis atmosfæren bøyer dem til bakken eller sprer dem.

## 2. Refraksjon og refraktiv N

En radiobølge bøyer gjennom luft med en luft brytningsindeks med en annen. Indeksen n er svært nær 1, så reaktiviteten N brukes, dvs. n − 1 multiplisert med en million [11]:

```math
N = (n - 1) \cdot 10^{6} = \frac{77,6}{T} \left( P + 4810 \, \frac{e}{T} \right)
```

med P trykket i hPa, T temperaturen i kelvin og trykket på vanndamp i hPa. N-jord er vanligvis 300-400. Den andre termen, dampens, veier mye: Derfor teller fuktigheten så mye som [3] [11] -temperaturen.

Hva bøyer bølgen er ikke verdien av N, men hvordan den endres med kvoten, gradienten dN/dh:

| dN/dh (N/km) | Situasjon | Effekt på bølgen |
| 0 | Subrefraksjon | Bølgen beveger seg bort fra bakken, den kortere horisonten |
| fra 0 til −79 | Normal (standard-39) | Lysbøyning til bakken: radiohorisont ca 4/3 av den optiske |
| fra −79 til −157 | Superrefraksjon | Bøyningen bøyer mer, økt strømningshastighet |
| mindre enn −157 | Innhold | Vølven bøyer så mye som jorden eller mer og forblir fanget |

Terskelen på −157 N/km er punktet der krumningen av radiusen er lik jorda [7] [11]. Derfor brukes "ekvivalent terrestrisk stråle" på 8497 km (4/3 av den virkelige radius) i beregningen av [6] Scatter.

## 3. Inversjoner: Tropo motor

En gradient under −157 N/km krever N å kollapse i noen meters høyde. Det skjer når over et lag av frisk og fuktig luft er funnet varmere og tørr: temperaturen stiger og dampen faller sammen, og begge bringe ned N [3] [5]. LA0BY indikerer inversjoner opp til 10-15 °C per km mot den normale reduksjonen på 6,5 °C [3].

Inversjoner oppstår på forskjellige måter:

- Fordeler i antisykloner. I det høye trykket faller luften i høyden sakte, den komprimerer og varmes opp, mens på bakken forblir frisk og fuktig luft. Det er årsaken til de lengre og lengre åpningene, som siste dager [3] [4] [13].
- nattlig stråling. Med rolig himmel og svak vind avkjøles jorden om natten og avkjøler kontaktluften: Lav bakside som bryter med morgensolen. Kveld- og morgenåpninger, typisk for sommer og høst.
- Havvarsel. Varm og tørr luft kommer fra bakken over et kaldere hav: havlaget forblir fuktig og kjølig nedenfor. Herfra de store marine rutene (Atlantic, Middelhavet, Nordsjøen) [4] [13].
- Fronter. Foran en varm front flyter den varme luften over den kalde; åpningen er kort og følger pannen.
- Fordamping. Over havet, i de første meter, faller fuktigheten raskt med høyden: svært tynn kanal, nyttig bare for mikrobølgeovner.

Tegnet på at en omvendt vises i vertikale profiler (radiosondaggi): temperaturen stiger og duggpunktet går ned i samme del [5]. Programmet viser et spor av dette med det termiske inversjonslaget (luft ved 925 hPa varmere enn på bakken) og med isotermisk ved 850 hPa.

## 4. Overflate og høye kanaler

En kanal er et lag der bølgen beveger seg ved å hoppe mellom den øvre kant (inversjon) og den nedre eller nedre kant [7]:

- Overflateledning: grunn eller hav. De lavverdige stasjonene, spesielt på kysten, kommer direkte inn.
- Høy rørledning: står i høyde, for eksempel mellom 500 og 2000 m. Stasjonene på slettene kan ikke føle seg, mens de i åsen eller i fjellet går inn i kanalen. Forklar koblingene som krysser Alpene eller andre [3] reliefs [4].

For å komme inn i kanalen bør bølgen starte nesten horisontalt: lave antenner i horisonten og ingen tilstøtende hindring. Inne i kanalen er dempingen mye lavere enn i det frie rommet, og avstandene når 1000–2000 km og lenger (for eksempel sammenhengene mellom Den iberiske halvøya, Kanariøyene og England, eller mellom Skandinavia og Sør-Russland) [3].

## 5. Frekvens: Hvorfor mikrobølger åpnes oftere

En kanal bare fanger ganske korte bølger sammenlignet med tykkelsen. De lange bølgene \"kommer ut\" fra en tynn kanal; De korte ikke. [3] [7]:

| Band | Bølgelengde | Hva du trenger |
| 144 MHz |  2 m  | Tykke rørledninger (centinai på meter), brede og godt merket versjoner: mindre hyppige, men svært omfattende åpninger |
| 432 MHz | 70 cm | Tynne kanter: åpner ofte når 144 bare er \"goder\" |
| 1296 MHz | 23 cm | Få svake inversjoner; lange ruter på sjøen hyppig om sommeren |
| 10 GHz | 3 cm | Bare fordampningskanalen på havet; også teller absorpsjonen av regn |

Derfor kan en beskjeden tropoindeks allerede bety åpninger i UHF og mikrobølgeovn, mens 144 kaller på høyere verdier. På den samme kanalen har 144 mindre tap utenfor kanalen og bedre dekke grenseområdene.

## 6. Troposfer-spread

Selv uten inversjon er troposfæren ikke ensartet: Små temperatur- og fuktighetsuregelmessigheter sprer en liten del av energien utenfor horisonten. Det er den troposferiske scatteren, alltid tilstede, som tillater forbindelser på 300-800 km med gode [6] stasjoner. Signalet er svakt og svingende; tapet vokser med diffusjonsvinkelen, det vil si med avstanden og med hindringer i horisonten:

```math
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
```

med θs diffusjonsvinkel i grader og f i MHz [6]. Programmet bruker denne formelen i fanen til hvert sted for å se om en kobling kan forklares med strøen eller krever en kanal (se dokument 1). En høy N-verdi på bakken forbedrer [6] -spreaden.

Ikke å forveksles med airscatteren (refleksjon på fly: signaler på et par sekunder, Doppler) og scatter meteoren (refleksjon på meteorer), som programmet utelukker fra tropoen.

## 7. Værbildet: Hvordan lese det på kartet

Tropo-åpningene følger meteorologien. Typiske situasjoner [3] [4] [13]:

- Stabilt antisyklon, høyt trykk (mer enn 1020-1025 hPa), bred isobar, liten vind: subsidence reverse. De beste åpningene følger kanten av antisyklonen, spesielt på havet.
- Calo della trykkione: inversjon bryter. G4MCU fulgte SK6VHF beacon inntil trykk synker til 1012 hPa [13].
- Varm luft i høyde (andre til 850 hPa stiger) over frisk luft på bakken: omvendt.
- Konfektive celler og gravitasjonsbølger (f.eks. undervind i Alpene) kan skape lokale og korte åpninger [4].
- Tid på dagen: inversjoner fra bestråling er sterkere om kvelden og tidlig på morgenen; subsidensen forblir også på dagen.

Hepburn [1] og F5LEN [2] indekser sette alt dette sammen fra værmodeller: De beregner N ved forskjellige kvoter og forvandler gradienter til et tall eller en klasse. Jeg er ikke en garanti: LA0BY viser en LA - UA6 hendelse som er synlig i høy temperatur og ikke i Hepburn [3]. For dette viser programmet sammen indekser, isobar, temperaturer og reelle flekker: Hvis punktene tilpasser seg et høyt indeksområde under en anticyklon, er åpningen i gang. KP4MD fant SNR av WSPR-rapporter til 144 MHz som vokser rett i vinduene fra Hepburn [14].

## 8. Praktisk liste

1. Se på tropoindeksen på området ditt og langs den retningen som interesserer deg, for de neste timene.
2. Sjekk isobar: høyt trykk og bredt isobar over eller ved siden av banen.
3. Slå på termisk inversjon og T 850 hPa: varm luft i høyde over banen.
4. Foretrekker ruter på havet og kveld og morgen timer.
5. Hvis du er på bakken, tenk på de høye kanalene; Hvis du er på kysten, på overflaten.
6. Prøv først de høyeste bandene (432, 1296): åpne først.
7. Bekreft med spot og PSKReporter: lange linjer i samme retning bekrefte kanalen.

## Kilder

1. W. R. Hepburn, DX Info Centre, \"The Hepburn Tropo Index (HTI)\" og \"Tropospheric Ducting Prognos\" sider (dxinfocenter.com).
2. F5LEN, prognoser for Europa og verden prediksjon av VHF tropoferisk utbredelse (tropo.f5len.org).
3. S. Heck LA0BY, “Extreme Tropo Utbreiing på 144 MHz og oppover”, 41° Nordic VUSHF Meeting, Skjeberg 2019. Normal gradient og inversjoner, temperatur og fuktighet, høyt trykk, LA– UA6 hendelse, forbindelser utenfor fjellene.
4. F. Egano IK3XTV, \"Propagasjon VHF - bidraget fra konfektive celler og atmosfæriske gravitasjonsbølger i Tropo utbredelse.\" Først 3 km, marine stier, konfektive celler, gravitasjonsbølger, høyt trykk.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, “Meget langdistanseutbreiing i 144 MHz bandet”, 24/7/2003. Temperatur og duggpunktprofiler, inversjoner, forskjell fra Esporadic.
6. J. N. Gannaway G3YGF, «Tropopheric Scatter Expansion», QST november 1983. Jordradius tilsvarende og strø tap.
7. VHF/UHF DX-boka, kapittel 2 «Troposfærisk utbredelse». Refraktive soner i henhold til dN/dh og overflate- og høykanalprofiler (bare figurene).
11. Anbefaling ITU-R P.453, radio brytningsindeks. N formel og kanalgrense ved −157 N/km. Generell referanse, ikke vedlagt.
13. G4MCU, “troposfæriske” artikler (g4mcu.net), 2018-2025. Kondotti med høyt trykk, spesielt på havet; SK6VHF og reduksjon til 1012 hPa.
14. C. F. Milazzo KP4MD, \"VHF/UHF WSPR-utbredelsesstudie\", 2012-2014. SNR WSPR ved 144 MHz og Hepburn-prognose.

Kildenummer er de samme som dokument 2, kartbygging.
