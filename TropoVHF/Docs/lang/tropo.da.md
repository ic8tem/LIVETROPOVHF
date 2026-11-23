LIVETROPOVHF · teknisk dokument 3
# Hvordan man danner tropen i 144 MHz og videre

> Automatisk oversættelse af italiensk: i tvivlstilfælde er det den italienske guide værd.

Hvad sker der i luften, når 144, 432 og mikrobølger går ud over horisonten: refraktion, inversioner, kanaler, scatters, og hvordan man læser dem på programkortet. Version af 9. november 2026.

## Sammenfattende

- VHF bølger og ud over rejser i de første kilometer af atmosfæren (troposfære) og bøje, hvor brydningsindekset ændres med [3] [7] [11].
- Indekset måles med refraktiv N, som afhænger af tryk, temperatur og frem for alt [11] fugt.
- I normal atmosfære er der omkring 40 enheder pr. km; når mere end 157 enheder pr. km, er bølgen fanget i en [7] [11] ledning.
- Kanalen kommer fra en termisk inversion med tør luft over og våd nedenfor: anticyklon, hav, fredfyldte nætter, [3] [4] [5] [13] fronter.
- Jo højere frekvens, jo tyndere kanalen er nok: mikrobølgerne åbner oftere end 144 [3] [7].
- Uden ledning forbliver tropospher scatter: altid til stede, svag, der falder med afstanden [6].
- På programmet: højt tryk + omvendt + Hepburn / F5LEN høj = sandsynlige forhold; pletterne bekræfter [1] [2] [13] [14].

## 1. Hvor det sker: troposfæren

Troposfæren er det laveste lag af atmosfæren, op til omkring 10- 12 km. For trope tæller primært de første 3 km: der er vanddamp og invertere, og der passerer radiovejene mellem stationer på jorden [4]. Under normale forhold falder temperaturen med højden på ca. 6,5 ° C pr. km [3]. Når temperaturen derimod i et lag stiger med højden, taler vi om termisk inversion: det er udgangspunktet for næsten alle stærke tropoåbninger.

VHF 'erne afspejles ikke af ionosfæren som HF (salve og sporadisk, som har andre timer og andre afstande [5]): ud over horisonten kommer kun, hvis atmosfæren bøjer dem til jorden eller spreder dem.

## 2. Refraktion og refraktiv N

En radiobølge bøjer gennem luften med et luftbrydningsindeks med en anden. Indekset n er meget tæt på 1, så reaktiviteten N anvendes, dvs. n − 1 ganget med en million [11]:

```math
N = (n - 1) \cdot 10^{6} = \frac{77,6}{T} \left( P + 4810 \, \frac{e}{T} \right)
```

med P trykket i hPa, T temperaturen i kelvin og trykket i vanddamp i hPa. N jord er normalt 300- 400. Den anden term, at damp, vejer en masse: af denne grund luftfugtigheden tæller så meget som [3] [11] temperatur.

Hvad bøjer bølgen er ikke værdien af N, men hvordan det ændrer sig med kvoten, gradienten dN / dh:

| dN / dh (N / km) | Situationen | Virkninger på bølgen |
| 0 | Subfraktion | Bølgen bevæger sig væk fra jorden, den kortere horisont |
| fra 0 til - 79 | Normal (standard − 39) | Let bøjning til jorden: radiobølge omkring 4 / 3 af den optiske |
| fra − 79 til − 157 | Superrefraktion | Bølgen bøjer mere, højere strømningshastigheder |
| under − 157 | Indhold | Bølgen bøjer lige så meget som Jorden eller mere og forbliver fanget |

Tærsklen på -157 N / km er det punkt, hvor krumningen af radius er lig med den af Jorden [7] [11]. Derfor den "ækvivalente jordstråle" på 8497 km (4 / 3 af den reelle radius), der anvendes ved beregningen af [6] -scatter.

## 3. Inversioner: Tropo motoren

En gradient under -157 N / km kræver N til at kollapse i et par meter højde. Det sker, når over et lag frisk og fugtig luft findes varmere og tørre: temperaturen stiger og dampen falder sammen, og begge bringe ned N [3] [5]. LA0BY [3]

Inversion opstår på forskellige måder:

- Subsidence i anticyclone. I det høje tryk falder luften i højden langsomt, det komprimerer og varmer op, mens på jorden forbliver frisk og fugtig luft. Det er årsagen til de længere og længere åbninger, som sidste dage [3] [4] [13].
- Nat bestråling. Med rolig himmel og svag vind køler jorden om natten og køler kontaktluften: Lav omvendt, der bryder med morgensolen. Aften og morgen åbninger, typisk sommer og efterår.
- Søadvarsel. Varm og tør luft fra jorden over et koldere hav: havlaget forbliver fugtigt og køligt nedenfor. Herfra de store marine ruter (Atlanterhavet, Middelhavet, Nordsøen) [4] [13].
- Fronts. Foran en varm front løber den varme luft over den kolde; åbningen er kort og følger panden.
- Fordampning. Over havet, i de første meter, falder fugtigheden hurtigt med højden: meget tynd kanal, nyttigt kun til mikrobølger.

Tegn på, at en omvendt ses i de lodrette profiler (radiosondaggi): temperaturstigning og dugpunkt faldende i samme strækning [5]. Programmet viser et fingerpeg om dette med den termiske Inversion lag (luft på 925 hPa varmere end på jorden) og med den isotermiske på 850 hPa.

## 4. Overflade og høje kanaler

En ledning er et lag, hvor bølgen bevæger sig ved at hoppe mellem den øverste kant (inversion) og jorden eller nederste kant [7]:

- Overfladerørledning: jorden eller havet. De lavhøjdestationer, især på kysten, går direkte ind.
- Høj rørledning: står i højden, for eksempel mellem 500 og 2000 m. Stationerne på sletterne kan ikke føle, mens dem i bakken eller i bjerget ind i kanalen. Forklar de links, der krydser Alperne eller andre [3] relieffer [4].

For at komme ind i kanalen skal bølgen starte næsten vandret: lave antenner i horisonten og ingen tilstødende hindring. Inde i kanalen er dæmpningen meget lavere end i det frie rum, og afstandene når 1000- 2000 km og videre (f.eks. forbindelserne mellem Den Iberiske Halvø, De Kanariske Øer og England eller mellem Skandinavien og det sydlige Rusland) [3].

## 5. Hyppighed: hvorfor mikrobølger åbnes oftere

En ledning fanger kun bølger nok end dens tykkelse. De lange bølger "kommer ud" fra en tynd kanal; de korte ikke. Så [3] [7]:

| Band | Bølgelængde | Hvad har du brug for? |
| 144 MHz |  2 m  | Tykke rørledninger (centinaia af meter), brede og godt markerede inversioner: mindre hyppige, men meget omfattende åbninger |
| 432 MHz | 70 cm | Mere tyndere rørledninger: åbner ofte, når 144 kun er "varer" |
| 1296 MHz | 23 cm | Få svage inversioner; lange ruter på havet hyppig om sommeren |
| 10 GHz | 3 cm | Kun fordampningskanalen på havet; også tæller absorptionen af regn |

Derfor kan et beskedent tropoindeks allerede betyde åbninger i UHF og mikrobølgeovn, mens 144 kræver højere værdier. På den anden side har de 144 ved samme kanal færre tab uden for kanalen og bedre dækning af grænseområderne.

## 6. Troposfer-spredningen

Selv uden inversion er troposfæren ikke ensartet: Små temperatur- og fugtighedsuregelmæssigheder spreder en lille del af energien ud over horisonten. Det er den troposferic scatter, altid til stede, som tillader forbindelser på 300- 800 km med gode [6] stationer. Signalet er svagt og svingende; tabet vokser med diffusionsvinklen, dvs. med afstanden og med forhindringer i horisonten:

```math
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
```

med en diffusionsvinkel i grader og f i MHz [6]. Programmet bruger denne formel i fanen af hvert sted at fortælle, om et link kan forklares med scatter eller kræver en kanal (se dokument 1). En høj N-værdi på jorden forbedrer [6] -spredningen.

Ikke at forveksle med airscatter (refleksion på fly: signaler af et par sekunder, Doppler) og scatter meteor (refleksion på meteorer), som programmet udelukker fra tropo.

## 7. Vejret billede: hvordan du læser det på kortet

Tropoåbningerne følger meteorologien. Typiske situationer [3] [4] [13]:

- Stabil anticyklon, højt tryk (mere end 1020-1025 hPa), bred isobare, lille vind: subsidence vending. De bedste åbninger følger kanten af anticyklonen, især på havet.
- Calo della pressone: inversion pauser. G4MCU fulgte SK6VHF -signalet indtil trykket falder til 1012 hPa [13].
- Varm luft i højden (hysotermi til 850 hPa stigende) over frisk luft på jorden: omvendt.
- Konvektive celler og tyngdebølger (f.eks. undervind i Alperne) kan skabe lokale og korte åbninger [4].
- Tidspunkt på dagen: Inversion fra bestråling er stærkere om aftenen og tidligt om morgenen; subsidence forbliver også af dagen.

Indekset Hepburn [1] og F5LEN [2] sammenfatter alt dette fra vejrmodeller: De beregner N på de forskellige kvoter og forvandler gradienter til et tal eller en klasse. Jeg er ikke en garanti: LA0BY viser en LA- UA6 begivenhed, der er synlig i høj temperatur og ikke i Hepburn [3]. Til dette viser programmet sammen indeks, isobare, temperaturer og reelle pletter: hvis pletterne passer med et højt indeks område under en anticyklon, åbningen er i gang. KP4MD fandt SNR af WSPR rapporter på 144 MHz vokser lige i vinduerne fra Hepburn [14].

## 8. Praktisk liste

1. Kig på tropo indekset på dit område og i den retning, der interesserer dig, i de næste par timer.
2. Check isobare: højt tryk og bred isobare over eller ved siden af stien.
3. Tænd for termisk inversion og T 850 hPa: varm luft i højden over stien.
4. Foretrækker ruter på havet og aften og morgen timer.
5. Hvis du er på bakken, tænk på de høje kanaler; Hvis du er på kysten, på overfladen.
6. Prøv først de højeste bands (432, 1296): åben først.
7. Verificér med spot og PSKReporter: lange linjer i samme retning bekræfter kanalen.

## Kilder

1. W. R. Hepburn, DX Info Centre, "The Hepburn Tropo Index (HTI)" og "Tropospheric Ducting Prognose" sider (dxinfocentre.com).
2. F5LEN, prognose for Europa og verdens forudsigelse af VHF tropofheric formering (tropo.f5len.org).
3. S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz og op", 41 ° Nordic VUSHF Meeting, Skjeberg 2019. Normal gradient og inversion, temperatur og fugtighed, højt tryk, LA- UA6 begivenhed, forbindelser ud over bjergene.
4. F. Egano IK3XTV, "Formering VHF - Bidraget fra konvektive celler og atmosfæriske gravitationsbølger i tropo formering". Første 3 km, marinebaner, konvektive celler, tyngdebølger, højt tryk.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, "very long distance propagation in the 144 MHz Band", 24 / 7 / 2003. Temperatur og dugpunkt profiler, inversioner, skelnen fra E sporadisk.
6. J. N. Gannaway G3YGF, "Troposfærisk Scatter Formering", QST november 1983. Earth radius ækvivalent og spredt tab.
7. VHF / UHF DX-bogen, kapitel 2 "Troposfærisk formering". Fraktionszoner i henhold til dN / dh og overflade- og højkanalprofiler (kun tallene).
11. Anbefaling ITU-R P.453, radiofoniindeks. N formel og kanal tærskel ved − 157 N / km. Generel henvisning, ikke vedlagt.
13. G4MCU, "troposfæriske" artikler (g4mcu.net), 2018- 2025. Condotti med højt tryk, især på havet; SK6VHF og falde til 1012 hPa.
14. Formand SNR WSPR ved 144 MHz og Hepburn prognose. KP4MD

Kildenumre er de samme som dokument 2, kortbygning.
