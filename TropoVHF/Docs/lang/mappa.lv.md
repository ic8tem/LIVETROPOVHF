LIVETROPOVHF · tehniskais dokuments 2
# Kartes konstrukcija

> Mašīntulkojums no itāļu valodas: šaubu gadījumā noteicošā ir itāļu rokasgrāmata.

Tā kā kartes kartes Karte un vietas ir dzimis: ģeogrāfiskā bāze, prognozes, vidējais Hepburn un F5LEN indeksu, izobare, vietas un stacijas klausoties, pārklājas ar slāņiem. 2026. gada 9. novembra redakcija.

## Kopumā

- Karte ir izstrādāta ar programmu astoņos slāņos, katrs ar savu [8] kaste.
- Pamatraksts: Zemes dabiskā piekraste un robežas divos [9] detaļas līmeņos; [10] Tirgus un azimuta prognozes.
- Fizika: inversijas, reaģētspēja N un gradients dN/dh, kanāla slieksnis pie −157 N/km [3] [4] [7] [11].
- Trumpet indekss: kalibrēts lasījums Hepburn [1] un F5LEN [2] kartes, kopīgs režģis un vidējais no abiem avotiem.
- Izobārā: GFS [12] spiediens caur Open-Meteo [15], līnijas ik pēc 4 hPa, H un L centriem.
- Spot, klausīšanās stacijas (tumši rozā apļi ar vienmēr redzamām rozā līnijām) un personīgie dekodingi sarkanā krāsā.
- Pārklāšanās nolasīšana: augsts spiediens, augsti cauruļvadi, spiediena kritums, pieaug SNR, [3] [4] [5] [13] [14] kalnu maršruti.

## 1. Princips: slāņaina karte

Karte nav fiksēts attēls: to veido programma, slānis pāri slānim, katru reizi, kad mainās tālummaiņa, projekcija, josla, prognozēšanas laiks vai tēma. Katram slānim ir sava kaste augšējā slāņa joslā kartē, lai jūs varētu turēt tikai to, kas jums nepieciešams, un redzēt visu kopā, kad vēlaties pilnu attēlu. Rīkojums no apakšas uz augšu ir šāds:

|  #  | Autoceļš | Saturs | Casella |
|  1  | Pamatkarte | Jūra, zemes, krasti un robežas | Jā |
|  2  | Tropo indekss | Media Hepburn + F5LEN vai tikai viens avots; regulējama pārredzamība | Jā |
|  3  | Grill vietrādis | Lauki, kvadrāti un, ar tālummaiņu, kvadrātā | Jā |
|  4  | Izobārs | Spiediena līnijas ik pēc 4 hPa, centri H un L | Jā |
|  4a  | Temperatūra 850 hPa | Izotermālā apstrāde 4 °C | Jā |
|  4b  | Termiskā inversija | Oranžais paraugs, kur gaiss augstumā ir siltāks nekā uz zemes | Jā |
|  5  | PSKReporter | PSKReporter saņemšanas stacijas (tumši rozā apļi) un rozā, ko viņi dzird | Jā |
|  6  | Atkodēju es | Dekodēšanas stacijas ar WSJT-X, JTDX vai MSHV: sarkanās līnijas no QTH | Jā |
|  6a  | QSO desunti dalal live | Slēgti savienojumi ( RR73, 73, RRR) lasīt jūsu dekodings: zaļā līnija starp divām stacijām | Jā |
|  7  | Starmeši | Savienojumi: pilna zila līnija, dzeltens pārklāts ar gaisa strūklu, divas bumbas | Jā |
|  7a  | Izņēmumi | Casella Izslēgts zem 100 km: īsākā satiksme nav ne sastādīta, ne uzskaitīta. Casella Vairāk nekā 1000 km: tikai kvadrāti (sākot): ja viena no stacijām atrodas vairāk nekā 1000 km attālumā no jūsu QTH, nav līniju vai apļi, tikai to staciju kvadrātu atrašanās vietu, kas iesaistītas ceriņu tur 45° | Jā |
|  8  | QTH un etiķetes | Jūsu stacija, izvēlētā vietas nosaukumi | vienmēr redzams |

Tropo indekss ir tieši virs pamatnes un zem visiem pārējiem: ir krāsaina paraugu ņemšana, un augstāk novietots segtu līnijas un simbolus. Režģis iet pāri, tāpēc jūs vienmēr zināt, kur krāsains laukums krīt. Izobari ir plānas līnijas, un tie ir labi nolasāmi virs paraugu ņemšanas.

## 2. Pamata karte

Izmaksas, robežas un zeme rodas no dabas Zemes vektoru datiem, publiskā sektora [9], mērogā 1:50 miljoni kopskatā un 1:10 miljoni palielinājumā. Faili ir programmas mapē: nav tiešsaistes kartes pakalpojumi. Tēmas krāsas Skaidrs: gaiši zila jūra # E2ECF8, tumši zila piekraste, gaiši pelēkas robežas. Tēmās Melna un Nakts bāzes krāsas ir aptumšotas, pārējās tēmās tās nedaudz iekrāso.

## 3. Abas prognozes

Visi slāņi iziet cauri vienai projekcijas funkcijai, tāpēc tie vienmēr ir saskaņoti savā starpā. Jūs izvēlaties starp divām [10] projekcijām (λ garuma, φ platuma, Zemes R raju):

Tirgus. Saglabāt lokālos stūrus un formas; Attālums stiepjas pret poliem. Izgriezt līdz ±85° platuma grādiem.

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

Azimutale equidtanti centrēts uz jūsu laukuma. Centrs (φ0, λ0) ir jūsu WWL laukuma centrs. Attālumi un virzieni, kas mērīti no centra, ir patiesi: stacija 1200 km platumā ir 1200 km, bet leņķis ar ziemeļiem ir azimuts, uz kuru tēmēt antenu. Kad jūs maināt atrašanās vietu, karte tiek pārorientēta.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

Azimutā līnijas, kas uz papīra ir taisnas (meridieši, paralēles, kvadrātveida malas), kļūst izliektas. Šī iemesla dēļ visas rindas ir sadalītas īsās daļās, katra sadaļa tiek projicēta un tad iezīmes ir vienotas; tas pats attiecas uz tropo indeksa malām un izobari. Antipodu punktam centrā nav noteiktas pozīcijas un paliek ārpus redzesloka.

## 4. Grila lokators, tālummaiņa un gājiens

Maidenhead režģis ir paredzēts ar programmu: lauki 20° × 10° ar diviem lieliem un izbalējušiem burtiem centrā, kvadrāti 2° × 1st, un ar pietiekamu tālummaiņu subquadrati 5′ × 2,5′. Līnijas ir zilas un plānas, nevis, lai segtu tropo indeksu.

Zoom ar riteni, kas centrēts uz rādītāja (punkts zem peles paliek vienmērīgs), pārvietojas velkot, dubultklikšķi, lai atgrieztos visā skatā, + un − taustiņi, un taustiņu ar procentu visam skatam. Detaļas, kas piemērotas tālummaiņai: smalkāki krasti, apakškvadrāti, biezākas izobāru etiķetes.

## 5. Kāpēc inversijas liekties viļņi

Zemajā atmosfērā temperatūra parasti pazeminās par 6,5 °C uz km; Refrakcijas indekss ir atkarīgs gan no temperatūras, gan [3] mitruma, un kur tas strauji mainās līdz ar augstumu vilnis noliecas uz zemi un ierodas tālāk nekā parasti. Svarīgākais slānis ir tas, ka līdz apmēram 3 km, kur veidojas inversijas, kanāli un konvekcijas šūnas [4]; celiņi uz jūras ir labvēlīgi, jo ūdens masas veido lielas vietas inversija [4]. [3]

Izmērs, ko izmanto tā mērīšanai, ir refrakcijas N, t.i. (n - 1) · 106 [11]:

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

T ir temperatūra kelvinā, P spiediens un tvaika spiediens, gan hPa.

Skaitļi pēc N daļas (dN/dh). Standarta atmosfērā ir aptuveni −39 N/km; līdz −79 N/km refrakcija tiek uzskatīta par normālu, bez superrefrakcijas un ar pozitīviem apakšrefrakcijas [7] gradientiem. Zem −157 N/km vilnis ir saliekts vairāk nekā zemes izliekums un paliek iesprostots: tas ir [11] (vadošais) kanāls. Šie ir apstākļi, ko Hepburn un F5LEN kartes mēģina paredzēt.

## 6. Tropo indekss: no divām kartēm līdz vidējam

Hepburn un F5LEN cilnes ir vadības galds: parādīt attēlus lejupielādēti no lapas katra avota, ar maināmu prognozēšanas laiku, un kalpo, lai kalibrētu lasījumu, noklikšķinot uz atsauces punktiem un krāsas leģendas. Tropo indeksa slānis paliek tukšs, līdz vismaz viena no abām kalibrēšanām nav aktīva un pabeigta.

Hepbern. Hepburn indekss mēra, cik daudz viļņus ir paredzēts salocīt pa platību, lineārā mērogā no 0 līdz 10, kas var arī pārsniegt 10. 0 ir standarta atmosfēra un tikai izkliede; 1–2 margināla vai vāja superrefrakcija; 3–4 superrefrakcija ar kanāla zonām; indeksā neņem vērā izkliedes tropo un neredz [1] kalnus. Kartes aptver 6 dienas: 3 stundu caurlaides pirmās 36 stundas, tad 6 stundas, atjaunotas katru dienu līdz 18 UTC [1]; sākuma dati (temperatūra, mitrums, spiediens) nāk no Kanādas meteoroloģiskā dienesta globālā modeļa [1]. [1]

F5LEN. Eiropai F5LEN publicē 31 karti no +3 līdz +192 stundām (ik pēc 3 stundām līdz +48, ik pēc 6 līdz +96, tad ik pēc 12), ar datumu un laiku, kas ir spēkā nosaukumā, ražošanas laiku apakšā pa labi un klases leģenda: Disturb, Normal, Minor, Vāja, Laba, Strong, Vy Strong, Duct [2]. Pasaules prognozi F5LEN aprēķina pēc gaisa refrakcijas koeficienta N vienībā, pa 40 soļiem 3 stundas (apmēram 5 dienas), kas atjaunināts 4 reizes dienā; klikšķis uz kartes parāda vidējo vērtību un šā punkta [2] vietu.

Turpinu. Katram prognozes solim programma veic šos soļus:

1. Izkraušana. Abu prognožu attēli tiek lejupielādēti kešatmiņā blakus izpildfailam, vienreiz, lai atjauninātu avotu, nevis jebkādā pārprojektēšanā. Pirms galīgās versijas būtu jāpieprasa atļauja Hepburn un F5LEN.
2. Automātiska ģeoreferencēšana. Programma atrod rāmi (divas horizontālas un divas vertikālas līnijas vienā krāsā) un uz Windows 10/11, lasa ar OCR integrēta sistēmā ass etiķetes (54N, 10W, 0, 5E...). Nogrieziet sloksnes ap rāmi, palieliniet tās, pārbaudiet abas zīmes, kad burts N/S/E/W nelasa un saglabājiet līniju, uz kuras visvairāk etiķetes krīt: nepareizie rādījumi paliek. No šejienes jūs varat iegūt galējo lat/lon rāmi un projekcijas veidu (regulāro grilu vai Mercator). Galējības ietaupīt sevi un pārlasīt sevi, ja kadrs maina izmēru. Uz Mac un Linux, bez OCR, rūpnīcas galējās vai ar roku rakstītās ir derīga tab. Alternatīvi kartēm, kurām nav salasāmu asu: Katram kartes veidam (Hepburn Europa, F5LEN visā pasaulē...) ir kalibrēšana: daži kontroles punkti ar zināmām koordinātām ( meridiānu un paralēļu krustojumi, atpazīstami krasti) savieno pikseļus ar platumu un garumu, šā avota izmantotajā projekcijā. Kalibrēšana ir failā blakus programmai, un jūs varat pārtaisīt, ja avots maina formātu.
3. Krāsu lasīšana. Katrs pikselis tiek salīdzināts ar avota leģendu un pārvērsts tuvākajā skaitliskajā vērtībā. Sākotnējā kartē attēlotās izmaksas, raksti un tīkli tiek atzīti par „bez datiem" un aizpildīti ar blakus vērtībām.
4. Pasaules tīkls. Katrs attēls, kad atrodat rāmi un pamanāt tā galējības, tiek novietots uz Zemes; vērtība jebkurā pasaules punktā ir atvasināta no attēliem, kas to aptver. Ja vairāk reģionu aptver to pašu punktu, tas ir vērts detalizēti (vairāk pikseļu uz grādu), kamēr tā laiks ir 1,5 h no šīs izvēles; tad tuvāko stundu, tad jaunāko rasi. Ja neviens attēls neaizsedz punktu, kartē redzams pelēks plīvurs: nav datu, kas atšķiras no “indeksa 0”. Iekšā kadrā pikseļs bez leģendas krāsas ir 0 (normāla atmosfēra); krasti, robežas un raksti tiek lēkt, apskatot tuvāko krāsu 3 pikseļos.
5. Tās pašas kāpnes. Hepburn jau ir uz skalas 0-10 [1]: tumšs fons = 0 un desmit krāsas secībā (violets, zils, ūdenszaļš, laims, dzeltens, očers, oranžs, sarkans, rozā, gaiši rozā) par 1..10., ceriņu aiz. F5LEN izmanto klases (Disturb, Normal, Minor, Vāja, Laba, Spēcīga, Vy Strong, Duct) [2], bet daži ir tieši Hepburn krāsas: Vāja = 1, Strong = 5, Vy Strong = 7, Duct = 8; Maza ir 0,5 un Laba (zaļa) 3,5. Mēroga reizinātājs F5LEN cilnē labo komplektu, lai to varētu salīdzināt ar Hepburn.
6. Tajā pašā laikā. Laika josla iet no +0 līdz +192 h ar 3 stundu soli. Par katru soli jūs veikt no katra avota prognozes spēkā tuvākajā laikā; Ja avots nav, ka solis, mediji izmanto tikai otru un leģenda norāda to.
7. Mediji. Ar Media pogu katrs punkts ir vērts (Hepburn + F5LEN) / 2; ar Hepburn vai F5LEN jūs redzat tikai vienu avotu, noderīga, lai saprastu, kur abas prognozes atšķiras.
8. Zīmējums. Vērtība ir iekrāsota ar leģendu skalu (0 zila, 2 zaļa, 4 dzeltena, 6 oranža, 8 sarkana, 10 violeta) un projicēta kā paraugu ņemšana zem režģa. 0 ir caurspīdīgs, tāpēc, ja nekas nenotiek, jūs redzat tīru pamata karti; vispārējā caurspīdīgums pielāgojas ar kursoru blakus kastei.

Uz vietas tab, indekss pa maršrutu ir vidējais no režģa vērtības čempionātu ik 10 km maksimālā aplī starp abām stacijām.

## 7. Izobārs

Augstais stabilais spiediens ir pirmais signāls, ko meklē [3] tropam, un savienojumi ārpus analizētajiem IK3XTV Alpiem ir bijuši ar augstu [4] spiedienu.

1. Dati. Spiediens jūras līmenī no NOAA [12] globālā modeļa GFS, lasīt caur bezmaksas Open-Meteo [15] pakalpojumu 3° × 3° tīklā (no 30° līdz 72° N, no 30° W līdz 48° E), nākamās 78 stundas. Programma atjaunina datus par starta un pēc tam ik pēc 6 stundām, piemēram, modelis sacīkšu.
2. Līnijas. Izobare nāk no tīkla ik pēc 4 hPa (..1012, 1016, 1020...) ar algoritmu maršēšanas kvadrātu (marking kvadrātu), tad tie ir nedaudz noapaļoti un projicēti ar traktātu.
3. Centri. Visiezīmētākās vietējās maksimālās un minimālās vērtības ir apzīmētas ar H (zilā) un L (sarkanā) un vērtību hPa.
4. Etiķetes. Vērtība parādās gar katru izobara, uz krāsainas kartes fona, lai saglabātu lasāmu virs paraugu ņemšanas.

Zona un solis. Dati tiek lejupielādēti apgabalā, uz kuru jūs skatāties, paplašināti par 25%, ar soli no 1st līdz 20° izvēlēta palikt zem 400 punktiem (Open-Meteo skaita katru punktu kā pieprasījumu: 600 minūtē, 5000 stundā): paplašinot vai pārvietojot karti no zonas jau lejupielādēt programmu visvairāk blīvs pārlādēt, ne vairāk ik pēc 10 minūtēm; ja atjauninājums neizdodas iepriekšējo datu paliek un tas mēģinās vēlreiz pēc 3 minūtēm. Šādi izobari un temperatūras darbojas jebkur pasaulē.

Temperatūra Tāds pats pieprasījums rada temperatūru līdz 2 m, līdz 925 hPa un līdz 850 hPa. LA0BY parāda, ka 22/11/2018 līdz UA6 notikums bija redzams temperatūras kartēs (900–3000 m), bet ne Hepburn [3] prognozēs; DF5AI un DK5YA izmanto vertikālos temperatūras profilus, lai atpazītu [5] inversijas. Tam ir divi slāņi:

- Temperatūra 850 hPa: izotermu apstrādā ik pēc 4 °C (sarkans virs nulles, zils zem, violets uz nulles).
- Termiskā inversija: oranža paraugu ņemšana, kur gaiss pie 925 vai 850 hPa ir siltāks nekā pie 2 m; jo intensīvāka, jo inversija ir spēcīga. Tas ir fiziskais stāvoklis, kas liek viļņus uz zemes [3] [11], un salīdzināt to ar tropo indeksu un izobare rāda, vai prognozēm ir meteoroloģiskā bāze.

## 8. Vietas un stacijas

Spot. Katrs punkts ar abām zināmajām pozīcijām ir līnija uz maksimālā apļa starp abām stacijām, sadalīts posmos un projicēts; Mercatore tas parādās tad nedaudz izliekts, azimutalē tas ir taisns, ja tas sākas no centra. Parasta plankuma zilā līnija, dzeltena, pārklāta ar tumšu malu gaisa stabam (<AS>, <ACS> vai “Airscatter” komentārā), bumba katrai stacijai, gaismas halo zem līnijas, lai atdalītu to no paraugu ņemšanas. Izvēlētais punkts biežāk ir paredzēts pēdējam un abiem nosaukumiem.

Klausos. Katrs PSKReporter [16] uztveršanas stacija ir tumši rozā tukšs aplis. No katra sākuma pilnas rozā līnijas pret stacijām, kas ir atšifrējušas un nosūtījušas atpakaļ uz tīklu, vienmēr redzams bez klikšķiem, ar vārdiem vienmēr rakstīts. Izvēloties apli, tā līnijas ir biezākas. Planēt un klausīties tikai parādīt joslu un diapazonu, kas izvēlēts sarakstā: mainot joslas platumu karte ir tukša un pārveidots ar datiem, kas jau savākti par jauno joslu.

Es izlēmu. Katra stacija, kas atšifrē ar WSJT-X, JTDX vai MSHV (iegūt caur UDP) ir sarkana līnija pilna no jūsu QTH uz staciju, ar bumbu un nomināciju. Tas ir vistiešākais salīdzinājums ar prognozi: parāda, kur jūsu uztvērējs tiešām nāk tieši tagad.

## 8 bis. Krāsas un simboli

Katrai krāsai ir tikai viena nozīme, vienāda visās tēmās (mainīt tikai fonu).

| Krāsa un zīme | Mozīme |
| Nepārtraukta zila līnija | Spot no DX klastera, RBN vai DM4X starp divām stacijām |
| Dzeltena līnija ar tumšu malu | Spot ar airscatter komentārs (atstarošana uz gaisa kuģa) |
| Tumši rozā tukšs aplis | Klausīšanās stacija uz PSKReporter (MQTT) |
| Plānas rozā līnijas no apļa, ar rozā bumbu apakšā | Stacijas, ko klausītājs dzirdējis, ar vārdu vienmēr rakstīts; biezākas līnijas izvēlētajam klausītājam |
| Sarkana līnija un sarkans nosaukums | Jūsu UDP dekodēšana (WSJT-X, JTDX, MSHV): kas Jūs esat |
| Sarkanās līnijas zibatmiņas | Šajā stacijā notiek QSO: Viņš jums piezvanīja un noslēgums vēl nav pienācis, vai ir DX Call of WSJT-X/JTDX ar iespējotu pārraidi. Turpināt ar RR73, RRR vai 73 |
| Zaļa līnija | QSO desunti dal dzīvot: aizver RR73, RRR vai 73 UDP skats |
| Melna līnija ar baltu apmali | Kāds jums zvana: tas ilgst līdz RR73/RRR /73 vai līdz 3 minūtēm |
| Kvadrāti 45° | Satiksme ar vismaz vienu staciju aiz izvēlētā vainaga: Nav līniju, tikai kvadrāta vietrādis |
| Meklēt lillas ar attālumu uz ziemeļiem | 1000, 700 un 500 km ap QTH; šī izvēle ir vairāk izteikta |
| Riņķis un punkts tēmas krāsā | Jūsu QTH ar vārdu un īpašnieku |
| Oranža bumba ar GRAVES 143 050 | Radiolokators GRAVES ( JN27SI) |
| Zils līdz sarkans paraugs | Hepburn Trope indekss (0–10) vai F5LEN klase, vai to vidējais; siltāka krāsa, labāki apstākļi |
| Gaiši pelēkais samts | Platība bez Tropo datiem izvēlētajā laikā |
| Pelēki zilas līnijas ar vērtību hPa | Izoberēt ik pēc 4 hPa; H zilā augstā spiedienā, L sarkanā zemspiediena |
| Sarkanas, zilas un violetas punktētas līnijas | Izotermija līdz 850 hPa ik pēc 4 °C: sarkana virs nulles, zila zem nulles, violeta nulle |
| Oranžais paraugs | Termiskā reversēšana: gaisa līdz 925 hPa siltāks nekā uz zemes, intensīvāka ar spēcīgāku inversiju |

Pārbaudītajos ziņojumos katras cilnes mala saka spēkā esošo noteikumu: zaļš labs, sarkans Scarta, pelēks nav noteikumu.

Kādi ir ceriņu laukumi sedz 45°? Satiksme prom no jūsu QTH interesēs jums saprast, kur pavairošana ir atvērta, bet tās līnijas, simtiem kilometru garumā un bieži desmitiem, aptver karti un slēpt to, kas ir svarīgi, lai jums: atveres jūs varat izmantot no savas stacijas. Šī iemesla dēļ ārpus izvēlētā vainaga programma neizzīmē iesaistīto staciju līnijas, bet iekrāso lokatora laukumus (2° × 1°). Rezultāts ir «aktīvo zonu» karte: kur parādās daudzi tuvumā esošie ceriņu laukumi, ir kanāls vai atvere, pat ja tas tevi neinteresē tieši; Ja laukums paplašinās pret tevi, tas ir signāls, lai rādītu antenu šajā virzienā. 45° paneļa ļauj jums redzēt zem Tropo indeksu un režģi, tāpēc salīdzināt īsumā prognozi (apakšējā krāsā) un reālo satiksmi (illus). Jūsu QTH kvadrāts nekad krāso, jo tas ir sākuma punkts, nevis attāls apgabals. Tikai laukumi, kas visi ir ārpus vainaga ir krāsaini: kvadrāts šķērso vainagu, vai iekšpusē, paliek brīvi, pat ja ir viena no divām stacijām. Lai redzētu līnijas, vienkārši izslēdziet kasti vai izvēlieties lielāku vainagu.

Kritēriji, pēc kuriem nosaka, kas ir kartē un kas ir atmests, ir izskaidroti ar vienu no tiem dokumentā 1, 11. ter.

## 9. Laika josla

Zemāk kartē: attēlotās prognozes datums un laiks, kursors no +0 līdz +72 h pēc 6 stundām, indeksa leģenda 0-10 ar izvēlētā avota nosaukumu, un modeļu sacensību laiks izobarei. Trumpet un izobare indekss seko kursoru; plankumi, stacijas klausās un jūsu dekodings ir novērojumi par brīdi un vienmēr paliek tie “adex”. Kad kursors nav uz +0 tie ir novājināti, lai atcerētos, ka tie nepieder pie prognozētais laiks.

## 10. Kā lasīt par pārklāšanos

- Augsts spiediens + augsts indekss + plankums gar krāsainu zonu: atvere ir tur un ir viens plānots. Pienācis laiks zvanīt šajā virzienā.
- Augsts indekss, bet nav vietas: vai neviens nav aktīvs tur, vai prognoze ir pārāk optimistisks. Klausīšanās slānis palīdz atšķirt: ja rajonā ir uztvērēji un neko nejūt, visticamāk, ir otrā hipotēze.
- Gari plankumi ārpus krāsainajām zonām: augsti vadi, kurus virsmas prognozes neredz, kā LA– UA6 [3] pasākumā; vai cita izplatīšanās izbēga no filtra (piemēram, E sporādika bez birkas). DF5AI un DK5YA atšķir tropo un E sporādiku arī no stundas: atveres, kas ir dzimuši un beidzas kopā ar difūzu E sporādika ir gandrīz [5] tropo.
- Hepburn un F5LEN nepiekrīt: salīdzināt abus avotus ar pogām un redzēt, kur īstie plankumi kritums.
- Spiediens uz leju: G4MCU sekoja SK6VHF bākas atvēršana, līdz spiediens samazinājās; 1012 hPa uzskatīja par pabeigtu notikumu [13]. Skatīties izobari, ritinot laika joslu: ja augstais spiediens atsauc, atvere, iespējams, seko tai.
- Signāli, kas iet uz augšu: WSPR studijā 144 MHz KP4MD signāla/trokšņa attiecība pieauga tieši logos, kur Hepburn prognozēja uzlabojumu, un samazinājās, kad prognoze ieņēma [14]. ja pa to pašu ceļu aug, atvere veido.
- Kalnu celiņi: indekss neņem vērā [1] reljefus, bet savienojumi ārpus Alpiem pastāv arī pateicoties konvekcijas šūnām un viļņiem atmosfēras gravitācijas [4], un LA0BY strādāja 9A un S5 caur kalniem bez Hepburn prognozējot to [3].

## 11. Atklāts punkts

- Atļauja Hepburn un F5LEN izmantot savas kartes programmā.
- Konversijas koeficientu F5LEN kalibrēšana → skala 0–10.
- Jebkura veida kartes kontroles punktu kalibrēšana, kas jāpārtaisa, ja avoti maina grafiku.
- Papildu slānis ar temperatūru augstumā (850 hPa) no VFS.

## Galvenie avoti

1. W. R. Hepburn, DX Info Centre, “The Hepburn Tropo Index (HTI)” un “Troposfēriskās ducting Prognozes” lapas (dxinfocure.com). Indeksa noteikšana un skala, negatīvās vērtības un vairāk nekā 10, izkliedes un reljefu izslēgšana, soļi 3 un 6 stundas 6 dienās, atjaunināšana līdz 18 UTC. Datu izcelsme no globālā Kanādas modeļa: G. Meisse, AMSAT-BB, 27/08/2007.
2. F5LEN, prognoze Eiropai (tropo.f5len.org/Eiropas prognozes) un pasaules prognoze par VHF troposfēras izplatīšanās (tropo.f5len.org/WW) un atzīmēta “VHF troposfēras izplatīšanās prognozēšanas rīka jaunā versija”. Vienības indekss N, 40 soļi no 3 stundām, 4 atjauninājumi dienā, vērtība un vietrādis uz vienu klikšķi.
3. S. Heck LA0BY, “Extreme Tropo Propagation on 144 MHz and up”, 41° Nordic VUSHF Meeting, Skjeberg 2019. Normāls gradients 6,5 °C/km un inversija līdz 10–15 °C/km, temperatūras un mitruma nozīme, augsts stabils spiediens, LA– UA6 notikums, kas redzams augstā temperatūrā, nevis Hepbērnā, LA– 9A/ S5 savienojumos aiz kalniem.
4. F. Egano IK3XTV, “Propagation VHF – Konvekcijas šūnu un atmosfēras gravitācijas viļņu ieguldījumu Tropo pavairošanā.” Pirmo 3 km nozīmīgums, jūras ceļi, konvekcijas šūnas, gravitācijas viļņi Alpos, augsta spiediena savienojumi, kopējs tilpums līdz 3–4 km.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, “Ļoti liela attāluma izplatīšanās 144 MHz joslā”, 24/7/2003. Vertikālie temperatūras un rasas profili norāda, lai atpazītu inversijas, Hepburn indeksa izmantošanu, atšķirību starp tropo un E sporādiku atbilstoši stundai.
6. J. N. Gannaway G3YGF, „Troposfēriskā izkliedēšana", QST 1983. gada novembris. Zemes rādiusa ekvivalents un ceļa zudums, ko izmanto indeksa un plankuma kartei (skatīt dokumentu Nr. 1).
7. VHF/UHF DX grāmatas 2. nodaļa “Troposfēras izplatīšanās”, 2.-23..2.-30. lpp. Refrakcijas zonu attēls saskaņā ar dN/dh (standarta −39 N/km, ierobežojumi −79 N/km un 0) un virsmas un augstkanāla profiliem. Iesniegtās kopijas teksts nav lasāms: tika izmantoti tikai skaitļi.
8. Projekta kopsavilkums 8/11/2026, ko nodrošina programmas autors. Karte krāsas, nepieciešamās projekcijas, slāņi, laika josla.
9. Dabiskā Zeme, piekrastes un robežu vektoru dati, publiskais īpašums (dabaszemedata.com). Vispārīga atsauce, nav pievienota pielikumā.
10. J. P. Snyder, „Kartes projekcijas – darba rokasgrāmata", USGS Professional Paper 1395, 1987. Merkatora projekciju un azimutale equidtanti formas. Vispārīga atsauce, nav pievienota pielikumā.
11. Ieteikums ITU-R P.453, radiorefrakcijas indekss. Refrakcijas formula N un kanāla slieksnis pie −157 N/km. Vispārīga atsauce, nav pievienota pielikumā.
12. NOAA NCEP, Globālā prognožu sistēma (GFS). Spiediens jūras līmenī 0,25°, četras rases dienā. Vispārīga atsauce, nav pievienota pielikumā.
13. G4MCU, raksti ar birku “troposfērs” (g4mcu.net/tag/tropsfērs), 2018–2025. Refrastruktūra, cauruļvads un izkliede; biežāki cauruļvadi ar augstu spiedienu, īpaši jūrā; bāka SK6VHF sekoja spiediena kritumam pie 1012 hPa (2019. gada jūnijs).
14. C. F. Milazzo KP4MD, “VHF/UHF WSPR izplatīšanas pētījums” (qsl.net/kp4md/144_mhz_wspr.htm), 2012.–2014. gads. SNR no WSPR ziņo, ka 144 MHz pieaug Hepburn prognozētajos uzlabošanas logos un samazinās, kad prognozes pazūd.
15. Open-Meteo, laika prognoze API (open-meteo.com), brīva nekomerciāla izmantošana. VFS modeļa spiediens jūras līmenī vairākos punktos vienā pieprasījumā.
16. mqt.pskreporter.info, sadalījums MQTT reālajā laikā reklāmas pskreporter. informācija: nominācija un saņēmēju īpašnieks (skatīt dokumentu 1).
