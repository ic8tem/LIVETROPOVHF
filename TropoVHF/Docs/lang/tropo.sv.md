Lifetropovhf · tekniskt dokument 3
# Hur man bildar tropen i 144 MHz och bortom

> Maskinöversättning från italienska: vid tveksamhet gäller den italienska guiden.

Vad som händer i luften när 144, 432 och mikrovågor går utöver horisonten: refraction, inversioner, kanaler, scatters och hur man läser dem på programkartan. Version av den 9 november 2026.

## Sammanfattning

- VHF-vågor och bortom resan i de första kilometerna av atmosfären (troposfären) och böj där det refraktiva indexet ändras med [3] [7] [11].
- Indexet mäts med refraktiv N, vilket beror på tryck, temperatur och framför allt [11] fukt.
- I normal atmosfär finns det cirka 40 enheter per km; när mer än 157 enheter per km är vågen fångad i en [7] [11] -kanal.
- Kanalen kommer från en termisk inversion med torr luft ovan och våt nedan: anticyklon, hav, lugna nätter, [3] [4] [5] [13] fronter.
- Ju högre frekvensen, tunnare kanalen är tillräckligt: mikrovågorna öppnas oftare än 144 [3] [7].
- Utan ledning förblir troposfären scatter: alltid närvarande, svag, som faller med avståndet [6].
- På programmet: högt tryck + omvänd + Hepburn / F5LEN hög = sannolika förhållanden; fläckarna bekräftar [1] [2] [13] [14].

## 1. Där det händer: Troposfären

Troposfären är det lägsta lagret av atmosfären, upp till ca 10-12 km. För trope räknas främst de första 3 km: det finns vattenånga och inversioner, och det passerar radiovägarna mellan stationer på marken [4]. Under normala förhållanden sjunker temperaturen med höjden på ca 6,5 °C per km [3]. När, å andra sidan, i ett lager, stiger temperaturen med höjden, pratar vi om termisk inversion: det är utgångspunkten för nästan alla starka tropoöppningar.

VHFs reflekteras inte av jonosfären som HF (salva och sporadiska, som har andra timmar och andra avstånd [5]): bortom horisonten kommer bara om atmosfären böjer dem till marken eller sprider dem.

## 2. Refraktion och Refractive N

En radiovåg böjer sig genom luft med ett luftrefraktivt index med en annan. Index n är mycket nära 1, så reaktiviteten N används, dvs n − 1 multipliceras med en miljon [11]:

```math
N = (n - 1) \cdot 10^{6} = \frac{77,6}{T} \left( P + 4810 \, \frac{e}{T} \right)
```

med P trycket i hPa, T temperaturen i kelvin och trycket av vattenånga i hPa. N jord är vanligtvis 300-400. Den andra termen, den av ånga, väger mycket: Av denna anledning räknas luftfuktigheten lika mycket som [3] [11] -temperaturen.

Vad böjer vågen är inte värdet av N men hur det förändras med kvoten, gradienten dN/dh:

| dN/dh (N/km) | Situation | Effekt på vågen |
| 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 | Subrefraction | Vågen rör sig bort från marken, den kortare horisonten |
| från 0 till −79 | Normal (standard −39) | Ljusböjning till marken: radiohorisont ca 4/3 av den optiska |
| från −79 till −157 | Superrefraction | Vågen böjer mer, högre flödeshastigheter |
| mindre än −157 | Innehåll | Vågen böjer så mycket som jorden eller mer och förblir fångad |

Tröskeln på −157 N/km är den punkt där radiens krökning motsvarar jordens [7] [11]. Därför används den "ekvivalenta markstrålen" av 8497 km (4/3 av den verkliga radien) i beräkningen av [6] Scatter.

## 3. Inversioner: Tropo motor

En gradient under −157 N/km kräver att N kollapsar i några meter höjd. Det händer när över ett lager av färsk och fuktig luft hittas varmare och torrt: temperaturen stiger och ångan faller ihop, och båda ta ner N [3] [5]. LA0BY [3]

Inversioner uppstår på olika sätt:

- Subvention i anticyklon. I det höga trycket sjunker luften i höjden långsamt, det komprimerar och värmer upp, medan på marken förblir frisk och fuktig luft. Det är orsaken till de längre och längre öppningarna, som varar [3] [4] [13].
- Nattbestrålning. Med lugn himmel och svag vind svalnar jorden på natten och kyler kontaktluften: Låg omvänd som bryter med morgonsolen. Kvälls- och morgonöppningar, typiska för sommar och höst.
- Sea varning. Varm och torr luft kommer från marken över ett kallare hav: havsskiktet förblir fuktigt och svalt nedanför. Härifrån de stora marina rutterna (Atlantic, Medelhavet, Nordsjön) [4] [13].
- Framför. Framför en varm front flyter den varma luften över den kalla; öppningen är kort och följer pannan.
- Förångning. Ovanför havet sjunker luftfuktigheten snabbt med höjden: mycket tunn kanal, användbar endast för mikrovågor.

Tecknet på att en omvänd ses i vertikala profiler (radiosondaggi): temperaturökning och daggpunkt nedstigande i samma sträcka [5]. Programmet visar en ledtråd av detta med det termiska inversionsskiktet (luft vid 925 hPa varmare än på marken) och med isotermisk vid 850 hPa.

## 4. Yta och höga kanaler

En ledning är ett lager där vågen reser genom att studsa mellan övre kanten (inversion) och marken eller bottenkanten [7]:

- Surface pipeline: mark eller hav. Låghöjdsstationerna, särskilt vid kusten, går direkt.
- Hög rörledning: står på höjd, till exempel mellan 500 och 2000 m. Stationerna i slätterna kanske inte känner, medan de i kullen eller i berget går in i ledningen. Förklara länkarna som korsar Alperna eller andra [3] -lättnader [4].

För att komma in i ledningen bör vågen börja nästan horisontellt: låga antenner på horisonten och inget grannhinder. Inuti ledningen är dämpningen mycket lägre än i det fria utrymmet, och avstånden når 1000-2000 km och bortom (till exempel förbindelserna mellan den iberiska halvön, Kanarieöarna och England, eller mellan Skandinavien och södra Ryssland) [3].

## 5. Frekvens: Varför mikrovågor öppnas oftare

En ledning fäller bara tillräckligt korta vågor än dess tjocklek. De långa vågorna kommer ut från en tunn kanal; De korta gör det inte. [3] [7]:

| Band | Wavelength | Vad behöver du? |
| 144 MHz |  2 m  | Tjocka rörledningar (centinaia av meter), breda och väl markerade inversioner: mindre frekventa men mycket omfattande öppningar |
| 432 MHz | 70 cm | Fler tunnare rörledningar: öppnar ofta när 144 bara är "bra" |
| 1296 MHz | 23 cm | Få svaga inversioner; långa rutter på havet frekvent på sommaren |
| 10 GHz | 3 cm | Bara avdunstningskanalen på havet; räknar också absorptionen av regn |

Därför kan ett blygsamt tropoindex redan innebära öppningar i UHF och mikrovågsugn, medan 144 kräver högre värden. Å andra sidan har 144 mindre förluster utanför ledningen och bättre täcker gränsområdena.

## 6. Troposfer scatter

Även utan inversion är troposfären inte enhetlig: Små temperatur- och fuktighetsproblem sprider en liten del av energin bortom horisonten. Det är troposferisk scatter, alltid närvarande, som tillåter anslutningar på 300-800 km med bra [6] -stationer. Signalen är svag och fluktuerande; förlusten växer med diffusionens vinkel, det vill säga med avståndet och med hindren på horisonten:

```math
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
```

med θs diffusion vinkeln i grader och f i MHz [6]. Programmet använder denna formel i fliken på varje plats för att berätta om en länk kan förklaras med spridaren eller kräver en ledning (se dokument 1). Ett högt N-värde på marken förbättrar [6] scatter.

Ej att förväxla med airscatter (reflektion på flygplan: signaler på några sekunder, Doppler) och scatter meteor (reflektion på meteorer), som programmet utesluter från tropo.

## 7. Väderbilden: hur man läser den på kartan

Tropoöppningarna följer meteorologin. Typiska situationer [3] [4] [13]:

- Stabil anticyklon, högt tryck (mer än 1020-1025 hPa), bred isobare, liten vind: subsidence reversal. De bästa öppningarna följer kanten av anticyklonen, särskilt på havet.
- Calo della pressione: inversion raster. G4MCU följde SK6VHF -beacon tills trycket sjunker till 1012 hPa [13].
- Varm luft på höjd (hysothermia till 850 hPa stiger) över frisk luft på marken: omvänd.
- Konvektiva celler och gravitationsvågor (t.ex. undervind i Alperna) kan skapa lokala och korta öppningar [4].
- Dagens tid: inversioner av bestrålning är starkare på kvällen och tidigt på morgonen; subventionen förblir också på dagen.

Hepburn [1] och F5LEN [2] -index sätter allt detta tillsammans från vädermodeller: De beräknar N på de olika kvoterna och omvandlar gradienter till ett nummer eller en klass. Jag är inte en garanti: LA0BY visar en LA- UA6 -händelse som är synlig i hög temperatur och inte i Hepburn [3]. För detta visar programmet tillsammans index, isobare, temperaturer och verkliga platser: om fläckarna är i linje med ett högt indexområde under en anticyklon, är öppningen pågår. KP4MD fann SNR av WSPR-rapporter vid 144 MHz växer direkt i de fönster som tillhandahålls av Hepburn [14].

## 8. Praktisk lista

1. Titta på tropoindexet på ditt område och längs den riktning som intresserar dig, under de närmaste timmarna.
2. Kontrollera isobare: högt tryck och bred isobare över eller bredvid vägen.
3. Slå på termisk inversion och T 850 hPa: varm luft på höjd över vägen.
4. Föredra rutter på havet och kväll och morgontimmar.
5. Om du är på kullen, tänk på de höga kanalerna; Om du är på kusten, på ytan.
6. Försök först de högsta banden (432, 1296): öppna först.
7. Kontrollera med spot och PSKReporter: långa linjer i samma riktning bekräftar kanalen.

## Källor

1. W. R. Hepburn, DX Info Centre, "The Hepburn Tropo Index (HTI)" och "Tropospheric Ducting Forecast" sidor (dxinfocentre.com).
2. F5LEN, prognos för Europa och världsprediktion av VHF tropopheric propagation (tropo.f5len.org).
3. S. Heck LA0BY, ”Extreme Tropo Propagation on 144 MHz and up”, 41° Nordic VUSHF Meeting, Skjeberg 2019. Normal gradient och inversioner, temperatur och fuktighet, högt tryck, LA- UA6 händelse, anslutningar bortom bergen.
4. F. Egano IK3XTV, "Propagation VHF - Bidraget av konvektiva celler och atmosfäriska gravitationsvågor i tropoförökning." Första 3 km, marina vägar, konvektiva celler, gravitationsvågor, högt tryck.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, "Very Long Distance Propagation in the 144 MHz Band", 24/7/2003. Temperatur- och daggpunktsprofiler, inversioner, distinktion från E sporadiska.
6. J. N. Gannaway G3YGF, ”Tropospheric Scatter Propagation”, QST november 1983. Jorden radie motsvarande och scatter förlust.
7. VHF/UHF DX Book, kapitel 2 ”Troposfärisk förökning”. Refraktiva zoner enligt dN/dh och yta och höga kanalprofiler (endast siffrorna).
11. Rekommendation ITU-R P.453, radiorefraktivt index. N formel och kanal tröskel på −157 N/km. Allmän referens, inte bifogad.
13. G4MCU, ”troposfäriska” artiklar (g4mcu.net), 2018–2025. Condotti med högt tryck, särskilt på havet; SK6VHF och minska till 1012 hPa.
14. C. F. Milazzo KP4MD, ”VHF/UHF WSPR Propagation Study”, 2012–2014. SNR WSPR vid 144 MHz och Hepburn prognos.

Källanummer är desamma som dokument 2, Map Building.
