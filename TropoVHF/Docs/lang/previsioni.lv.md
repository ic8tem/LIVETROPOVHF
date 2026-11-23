# Prognozes rokasgrāmata

> Automātiskā tulkošana itāļu: šaubu gadījumā ir vērts Itālijas ceļvedis.

Prognozes tab aprēķina tropoferisko pavairošanu pati par sevi: indeksa karti redzamajā zonā un signālu, kas tiek sniegts starp jūsu staciju un attiecīgo. Šajā rokasgrāmatā ir aprakstīti visi dati, kā tie tiek aprēķināti un no kāda avota tie ir iegūti. Skaitļi kvadrātiekavās attiecas uz avotiem apakšā; programmā katru datu rezultātu ir komikss (simbolo i) ar to pašu avotu un atsauci uz punktu.

## 1. Ko viņš dara un ko dara

GFS [1, 2] modeļa vertikālās temperatūras un mitruma profilu prognozēšana iegūst gaisa slāņa reaģētspēju ar slāņa palīdzību un aprēķina signāla guvumu, salīdzinot ar normālu atmosfēru. Maršrutā tas pievieno reālo zemi (Copernicus GLO-30 [3]), difrakciju, troposcatter, vadus un uztvērēja troksni.

Tas nav pasākums: tas ir aprēķins, kas veikts uz laika modeli ar tālu līmeni simtiem metru. Tas kalpo, lai salīdzinātu situācijas (šodien labāk nekā vakar, 70 cm labāk nekā 2 m, šis ārstēt labāk nekā), nevis, lai garantētu savienojumu. 14. punktā paskaidrots, kā vākt pierādījumus, lai tos apstiprinātu.


> "Pavairošanu nevar vispārināt, bet pētīt saistībā ar vietējo morfoloģiju. »
> — Volker Grassmann, DF5AI, ziņo Flavio Egano, IK3XTV (Propagācija 2 m attālumā)

Tas ir princips, kas vada punktu aprēķinu: katrs maršruts tiek aprēķināts uz tās augsnes, ar tās apvāršņiem, tās jūru un tās reljefiem; karte sniedz vispārēju priekšstatu, apstrādā atbildi.

## 2. Valodas komandas

- Josla: 144, 432 vai 1296 MHz. Karte un aprēķini izmanto izvēlēto joslu.
- Tagad: kursors pārvieto prognozi no šī brīža līdz 72 stundām uz priekšu, 3 stundu laikā.
- Atjauninātā prognoze (redzamā zona): lejupielādēt GFS profilus līdz pat 240 punktiem, kas aptver redzamo karti. Režģa solis (0,5° vai vairāk) ir atkarīgs no palielinājuma.
- Apturēt: pārtrauc lejupielādi vai aprēķinus.
- Informācija, kas jāparāda (kartes labais panelis): trīs līmeņi, Essential, Normal un Complete, ieslēdziet lodziņu grupu; pēc tam katra kaste var mainīties ar roku. Ailes ir trīs grupās:
  - Kartē: izolīna del pieaugums, arī 3–7 dB (pievilkšana), izobars, siltuma inversija, laikapstākļu pazīmes S ↓ ↑, korelācijas pieaugums/nospiešana, tiešā redzes zona (4., 5., 7. un 9. bis punkts). Krāsas vienmēr paliek.
  - Signāla aprēķināšanā: visas aprēķina līnijas, plakanais Zemes profils, aprēķinu žurnāls un detalizēts faila žurnāls.
  - Labojumi un dzīvie dati: laikapstākļu korekcijas (Misme, iegrime), gaisa-jūras kontrasts, vidējais ar plankumiem (corridoi), APRS 2 m lēcieniem un Lora, aiz kalna bez zaudējumiem (4.bis., 9.bis., 9.kvatera un 10.bis. punkts).
  - Būtiski ir tikai izolīnu, laikapstākļu un mediju korekcijas ar plankumiem; Normāls piebilst izobare, inversija, laika zīmes, tiešais skats, plakana zeme, gaisa jūra un APRS; Pabeigt ieslēdz to. Līmenis maina tikai to, ko jūs redzat un kādus labojumus ievadiet: aprēķini paliek nemainīgi.
- Datu avots un vēsturisks gadījums: no kurienes nāk profili un kurā dienā jūs skatāties (3 bis un 3 ter punkti).
- Mana stacija un korespondents: lokators, antenas, jauda, kabeļi, troksnis (7. un 12. punkts).
- Aprēķina signālu un pārklājumu optikā: abi aprēķini uz zemes (8.–13. punkts).
- Aprēķina reģistrs: katrs solis ar izmantotajiem skaitļiem, arī datu / prognozē-calcoli.log failu.

## 3. No VFS datiem uz profilu

Katram punktam tīkla un katru stundu mēs lasām spiedienu uz zemes, temperatūru un mitrumu pie 2 m, vējš pie 10 m, augstums robežslāņa, CAPE un temperatūra un mitrums attiecībā pret 1000, 975, 950, 925, 925, 900, 850, 800, 700, 600 un 500 hPa. No 1.3.0 jūs varat arī izlasīt vēju līdz 850 hPa un vertikālo ātrumu līdz 850 un 700 hPa (9.bis punkts).

Līmeņus aprēķina ar ipsometrisko formulu un virtuālo temperatūru; līmeņi zem zemes ir ierobežoti. Katrā līmenī:

- N = 77,6/T · (P + 4810·e/T), ar T kelvinos, P un hPa un ar ITU-R P.453-14 piesātinājuma spiedienu, 9. ekv., ar korekcijas koeficientu EF (ko atkārtoti kontrolē ar oficiālo tekstu) [4].
- M = N + 157·h, ar h km: modificētā reaģētspēja. Kur M iet augšā, tur ir kanāls.

## 3 bis. Datu avots

Datu avota izvēlne izvēlas, no kurienes nāk profili:

- Automātisks (ieteicams): GFS first from NOMADS [14], NOAA pakalpojums, kas izplata modeli: uz katru brīdi programma lūdz divas cutouts no apgabala G RIB2 formātā un lasa tos pats; NOMADS nav dienas limitu, tikai vienu minūtē, ka programma ievēro. Ar platiem režģiem (no 0,5° līdz augšai) tas izmanto 0,5° vai 1. failus ik pēc 3 stundām, daudz vieglākus. Punkti, ka NOMADS nedod, vai visi, ja NOMADS nav atbildēt, jautājiet Open-Meteo [2]. Abi avoti dod vienu un to pašu modeli un vienu un to pašu sacensību, lai jūs varētu sajaukt vienā režģī.
- VFS no Open-Meteo: tikai Open-Meteo, tāpat kā 1.2 (ar limitu minūtē ir gaidāms).
- VFS no NOAA NOMADS: tikai NOMADS. Lejupielādēt (divi faili stundā, pussekundē starp vienu pieprasījumu un otru nepārsniedz NOAA robežu), bet nelieto Open-Meteo ierobežojumu.
- ICON-EU (DWD) un ECMWF IFS 0,25° no Open-Meteo: modeļi, kas nav VFS un ko piemēro visā tīklā un maršrutos. Tie nesajaucas ar VFS: salīdzināt divus modeļus vienā laikā ir vienkāršs veids, kā redzēt, kā prognoze ir neskaidra. ICON-ES aptver tikai Eiropu; ECMWF nav visu VFS spiediena līmeņu (trūkstošo līmeņu lēciens), un tai var nebūt robežlīmeņa.

Izmantotais avots tiek ierakstīts statusa rindā un rezultātu pirmajā rindā (Datu avots), ar cik punktu ir no katra servisa un cik daudz no kešatmiņas. Diska kešatmiņu atdala modelis.

## 3 ter. Vēsturiski gadījumi

Vēsturiskais Gadījumu izvēlne pārskata dokumentētu atvēršanu ar Open-Meteo [15] arhīvu GFS prognozes: karte pārceļas uz lietas apgabalu, josla uzstāda sevi un laiku kursors atstāj no pirmās lietas dienas. Nospiežot Update prognozi jūs lejupielādēt profilus šajās dienās; signālu aprēķināšana darbojas kā pašreizējo prognozi. Tas kalpo, lai salīdzinātu modeli ar to, kas tiešām notika.

Pirmais gadījums ir tas, ka no 14-15 augusts 2022 teica QST (2022. gada novembrī, "Pasaule virs 50 MHz" [23]): atvēršana zem augsta spiediena starp Manitoba un Kansas/Missouri, no 144 līdz 1296 MHz gandrīz visi FT8, ar Kanādas rekordu 10 GHz 1228,56 km Q65 un signālu līdz 1941 km. Otrā ir atklāšana no 2021. gada 29. decembra līdz 2022. gada 2. janvārim Tirēnu un Vidusjūras rietumu daļā 144 MHz frekvencē, no IC8TEM attiecībām: pirmās atveres 29 (līdz 737 km), platākas 31 ( IT9GSF – EB5EA 1217 km), lieliska atklāšana 1. janvārī starp Itāliju, Sicīliju un Spāniju. Vecākiem gadījumiem (piemēram, 2000. gada septembris) Open-Meteo arhīvam nav datu. Arhīvā nav ierobežojuma slāņa augstums: tur tā korekcija neattiecas.

## 4. Rādītājs un krāsas

Katram slānim starp diviem līmeņiem programma salīdzina N kritumu ar sliekšņiem tabulā 6.5 Bean un Dutton [5]: piemēram starp 1000 un 850 hPa samazinājums līdz 60 N/km ir normāls, virs 100 N/km ir superrefrakcija. Superrefrakcijas slānis ir 10 līdz 15 dB vērts; cauruļvads, kas aiztur joslu no 20 līdz 25 dB, saskaņā ar M deficītu. Spēcīgs vējš uz zemes, augsts limits slānis un augsts CAPE samazina ieguvumu, jo tie noņem vai lauž slāņus [5, § 4.4].

Krāsa ir aplēstais ieguvums dB, ar fiksētu skalu vienāds visās tēmās:

| Iegūt | Krāsa | Mozīme |
| 7 dB | Neviens | gandrīz normāla atmosfēra |
| 7–10 dB | zils | gradients virs normas |
| 10–13 dB | zaļa | superrefraktīvais slānis |
| 13–16 dB | dzeltena | spēcīgs slānis, bet bez kanāla joslai |
| 16–18,5 dB | apelsīni | ļoti spēcīgs slānis vai pārāk plāna caurule |
| 18,5–21 dB | sarkana | Ķēdes slīpēšanas josla |
| no 21 dB | Purpura | Spēcīgs un dziļš kanāls |

Ar displeja kastē arī 3–7 dB zonas no 3 līdz 7 dB, kur kaut kas tiek veidots, bet paliek zem skalas, jūs varat redzēt ar gaiši pelēku ierobežojumu; krāsas nemainās. Punktētā rūts norobežo teritoriju, uz kuru attiecas pati tās prognoze, un parāda maksimālo redzamo ieguvumu: ja tā ir zem 7 dB, karte paliek bez krāsām, jo atmosfēra ir gandrīz normāla, nevis tāpēc, ka trūkst datu. Lejupielādējot jaunu klikšķi uz Update prognozes nepārtrauc; apturēt to ir Stop.

Krāsas ir necaurspīdīgas līdz 82%, tāpēc tēmas fons tās vienkārši maina. Vietrāža režģis un kvadrātveida nosaukumi ir veidoti virs krāsām. Ja jūsu prognozes nav tur, bet Hepburn vai F5LEN dod 5 vai vairāk šaha kārtis parādās ar šī avota krāsu.

Karte vērtē katru punktu atsevišķi. Signāla aprēķins tā vietā kontrolē, ka slānis ir nepārtraukts pa maršrutu (9. punkts): maršruts, kas šķērso ļoti dzeltenu var dot mazāk nekā karte liek domāt.

## 4 bis. Prognoze, ko veido komerciālie koridori (no 1.3.3. punkta)

Autortiesību politika: saite vien nekad nedrīkst zīmēt vadu. Izolēta un īsa uztveršana var būt caurbraucošās plaknes izkliede (APRS pakete ir ļoti īsa), dažu minūšu atvēršanās vai kļūda, un tas dotu "nomet cauruļvadus" viņi maldināt. Programma atzīst koridoru, kad, tajā pašā joslā:

- vismaz trīs neatkarīgu staciju pāri (nav kopīgu staciju) darbu maršrutos, kuru garums ir vismaz 300 km;
- maršruti ir paralēli: azimuta ±15° robežās, vidējie punkti 150 km attālumā no kopējās ass;
- visi pēdējo 30 minūšu laikā, staru kūļam atkārtojoties vismaz 10 minūšu attālumā (pastāvība);
- attiecībā uz ARS lēcienu skaita tikai tad, ja to pašu lēcienu atkārto;
- no 1.3.4. punkta maršrutus, kas pārsniedz 1500 km, skaita tikai tad, ja staru kūlis nāk no vismaz diviem dažādiem avotiem (piemēram, PSKReporter un klaster): no viena avota līdz šiem attālumiem 2 m, biežāk ir EME, izkliedētāji vai nepareizi vietrāži nekā tropo.
- no 1.3.4., neskaita stacijas, kas “staro”: kurām pēdējā stundā ir vismaz 5 korespondenti 1000 km garumā, kas izvietoti uz vismaz 150° vēdekļa. 2 m trope ir virziena; uztvērējs, kas "brīvi" kopā Izraēla, Portugāle, Anglija un Itālija no 2000–3700 km, bieži ar pozitīviem signāliem, ir gandrīz noteikti ir konfigurēta slikti. 17–22/11/2026 I5MZY arhīvs 72% izmitināšanas 2 m virs 1000 km nāca no stacijām tā; 22/11 10:53 UTC bija OH6HLH, OZ1ITS, F4LUK, OM8ABC un SP2JLR, un ar filtru garākais koridors nolaižas no 2175 km (direktorija 1.3.3.) līdz 615 km.

Avoti: klastera vietas (ne AS/ACS), PSKReporter caur MQTT, tā QSO un WSJT-X/JTDX/MSHV dekodēšanas un APRS lēcieni 2 m un Lora 433/868 MHz.

APRS: 2 m un Lora. Programma skan APRS-IS tikai lasāms (rotate.aprs2.net, filtrs uz Eiropu) un izvilk radio lēcienus starp zināmām atrašanās vietām (sorgente → digipeater → IGate), piemēram, VHF karti DXView un APRS PropView (RF-YVY, Apache 2.0). Ir trīs joslas: 2 m, Lora 433 MHz (joslas koridori 432) un Lora 868 MHz (koridori, kas skaita 1296: kanāls, kas nodrošina 868 MHz arī augstākās frekvences). Grupa nāk no LoRa tocall (aprs-deviceid saraksts) un no frekvencēm vai vārdiem komentārā; lēciens ir vērts tikai starp divām vienas joslas stacijām.

Delikātais punkts ir aktīvie atkārtotāji: signālu, kas saņemts 500 km attālumā, var nogādāt ar divām vai trim digipeater retranslācijām. Tāpēc lēcienu skaita tikai tad, ja tas ir starp diviem reāliem un secīgiem raidītājiem:

- ceļš ir izsekojams: katrs retranslētais digipeaters ir nolicis savu nosaukumu, un lēciens tiek mērīts starp vienu un otru; segvārds, ko izmanto bez nosaukuma (W IDE1 *, W IDE2 *, regional nastics), salauž ķēdi un tas neskaitās;
- slēptās retranslācijas: programma atceras ceļu, ko katrs avots parasti lūdz (piemēram, W IDE2 -2). Ja pakotne nāk ar lēcienu mazāk (W IDE2 - 1) un neviens nosaukums to neizskaidro, digipeater retransmitted to bez izsekošanas un pakotne tiek izmesta; avoti, kas nekad nav redzējuši, neskaitās;
- digipeater vidū ceļa: ja starp divām stacijām, vairāk vai mazāk uz ielas, ir tās pašas aktīvās joslas digipeater pēdējās trīs stundās, lēciens tiek izmests, jo tas varētu būt retranslēts (it īpaši digipeater LoRa, kas retranslē, nemainot ceļu);
- neradioiepakojumi (qAC, qAX, TCPIP), kas no interneta pa radio novirzīti no IGate (trešās daļas), HF un dažādām frekvencēm, stacijas augstumā virs 1500 m (paloni, lidmašīnas) un lec pāri 1500 km.

Ietekme uz karti (no 1.3.4.). Tas piesaista tikai teritoriju, kurā ir spēkā garantētais minimums: ±60 km josla ap katra maršruta centrālo daļu (no 15% līdz 85% no tā garuma), nevis stacijās galos, kur var nebūt savienojuma. Visu koridoru joslas ir saliktas kopā, neitrālā krāsā (tekstā) un puscaurspīdīgā: pārklājumi nepievienojas, un krāsa nesajaucas ar skalas purpuru (21 dB un tālāk). Etiķetes (traiti, vidējais attālums, minimālais guvums) tiek skenētas savā starpā; ar peli uz etiķetes parādās koridora posmi.

Apgabalā un tikai tuvu pašreizējam laikam (±90 minūtes) prognoze nav mazāka par minimumu. Spriedums ir: ja stacijas tiešām iet, izplatīšanās ir tur, neatkarīgi no laika modelis saka.

Tā kā īsie maršruti ir maz vērti (no 1.3.4.). 2 m attālumā no 300 līdz 600 km attālumā divas labas stacijas (aptuveni simts vatu, antenas, FT8 vai CW) savieno gandrīz katru dienu pat ar normālu atmosfēru, troposkoteram: signāls, ko difundē nelielās gaisa neprecizitātes kopējā abu kūļu tilpumā. Trīs vai četri punkti pie 500 km norāda, ka josla darbojas kā parasti, nevis ka ir atvērums: GEKO Magazine n. 69 ( IU3QEZ) aplēses par troposcatter ar 2×10 plūstošiem elementiem aptuveni 440 km SSB QRP, 510 km SSB ar 50 W, 600 km CW QRP un 640 km SSB ar 500 W (avoti, vienpadsmit apļi). Tikai 600–700 km garumā ar troposcatter vairs nepietiek, un virkne paralēlu savienojumu patiešām norāda uz kanālu vai slāni. Līdz 1.3.3. punktam 450–600 km garš koridors paaugstināja prognozi līdz 9–12 dB, un karte liecināja par uzlabojumu, kāda tur nebija.

Tādējādi sākuma vērtība attiecas uz vidējo maršrutu attālumu:

- līdz 400 km: 3 dB;
- no 400 līdz 700 km: sāls lineārā veidā no 3 līdz 10 dB (500 km → 5,3 dB; 600 km → 7,7 dB);
- no 700 līdz 900 km: no 10 līdz 16 dB; vairāk nekā 900 km paliek 16 dB.

Zem 7 dB, kas ir zem skalas sākuma, minimums paliek, ka: koridors tiek zīmēts ar tā etiķeti, ir redzams, ka josla darbojas, bet kartes krāsas nemainās (ar kastē arī redzams 3-7 dB pelēkā punktētā). Virs 7 dB vērtību nosver uz pusi, ar neatkarīgu maršrutu skaitu (3 gājieni → 0,25; 6 vai vairāk → 1) un ar attiecību mediānu S/N (−20 dB → 0,5; 0 dB vai vairāk → 1; nav zināms, piemēram, klastera punktos → 0,75), sākot no 7 dB. Piemēri: 3 maršruti bez S/N uz 500 km → 5,3 dB; 5 maršruti uz 615 km → 7,7 dB; 3 maršruti bez S/N uz 900 km → 11,5 dB; 3 maršruti uz −20 dB uz 900 km → 10,4 dB; 6 maršruti uz 0 dB uz 900 km → 16 dB.

Minimālais attālums paliek 300 km: paceļot to līdz 500 km, tiktu likvidētas arī reālās tuvdarbības atveres (piemēram, cauruļvads Tirēnu pie 400 km, kas tiek atpazīts ar laika prognozi un ļoti spēcīgiem signāliem vairāk nekā no attāluma vien). I5MZY arhīvā 22/11/2026 10:53 UTC, ar šo noteikumu un staru filtru garākie koridori bija 615 km (7,7 dB) un 540 km (6,3 dB); ar noteikumiem 1.3.3. tas sasniedza 2175 km un 16 dB.

Līdz 1.3.3. gadam minimums bija spēkā visos koridora taisnstūros un bija atkarīgs tikai no attāluma (no 10 dB līdz 300 km līdz 16 dB līdz 900 km): kopā ar daudziem gariem koridoriem (piemēram, 22/11/2026, divpadsmit koridori līdz 2620 km) gandrīz visā Eiropā tika pacelti līdz 16 dB.

Ieguvuma līknes vairs neizvelk «gocce»: mazākās 220 km salas abos virzienos paliek krāsainas, bet bez slēgta līkuma, ja vien koridors tās neapstiprina. Tā izslēdzas ar "Revīziju mediētu plankumiem".

## 5. Izvēles līnijas un salīdzinājums ar spiedienu un inversiju

Iegūt līknes: līnijas pie 7, 10, 13, 16, 18,5 un 21 dB, kāpnes malas, kas vilkta uz lauka nogludināts ar metodi maršut kvadrātu, ar etiķeti dB.

Izobārs: spiediens jūras līmenī ik pēc 4 no viena un tā paša VFS, līdz stundu prognozes, ar centriem H un L.

Termiskā inversija: līknes, kas nosegtas ar gaisa daudzumu pie 925 vai 850 hPa ir siltākas nekā pie 2 m, pie +1, +3, +5 un +8 °C.

Zem kontrolēm līnija dod guvumu vienošanos ar spiedienu un ar reversu redzamajā zonā: Pearson korelācijas koeficients, −1 līdz +1. Cauruļvadi bieži piedzimst anticiklonos (gaiss, kas nomet siltumu un veido reversu), tādēļ pozitīva vienošanās ar spiedienu ir normāla; bet tajā pašā anticiklonā ieguvums daudz mainās starp jūru, piekrasti un kalniem. Reversā vien nepietiek: saliekt viļņi tikai tad, ja virs gaisa ir pat žāvētājs [5]. Līnija kalpo, lai redzētu, tagad, kas no diviem vislabāk izskaidro krāsainus apgabalus. Tas pats salīdzinājums ir ierakstīts reģistrā.

## 6. Potenciālie cauruļvadi: divkāršs aprēķins

VFS līmenis svārstās no 200 līdz 1000 m: tiek izkliedēta plāna inversija un modelis nesaskata kanālu. Ja starp diviem līmeņiem N samazinājums pārsniedz normu (40 N/km), gaiss ir stabils (termiskais gradients zem 3 K/km) un lielāks ir sauss (īpašā mitruma attiecība zem 0,8), programma apsver iespējamo kanālu un aprēķina to divos veidos:

- Aprēķināšana A: visu koncentrēto lieko kritumu 50 m.
- Aprēķināšana B: tāds pats samazinājums, kas izplatīts tipiskā šā apgabala cauruļvadu biezumā, iegūts no ITU-R P.453 [4] klimatiskajām kartēm (uz zemes vai paceltām).

Attiecībā uz deficītu un maksimālo pieļaujamo viļņu garumu tiek saglabāta zemākā vērtība: tā ir piesardzīga aplēse.

ITU-R P.453 dati ir © ITU un tos var izmantot personīgiem un nekomerciāliem mērķiem, citējot avotu, bet tos nevar pārdalīt: šim nolūkam es neesmu programmas paketē. Lai ieslēgtu B aprēķinu, vienkārši lejupielādējiet P.453 kartes no ITU cilnē: programma, parādot lietošanas nosacījumus, lejupielādējiet cauruļvadu arhīvu no ITU vietnes (levated_ and_surface_duct.zip) lietotāja personīgai lietošanai, izņemiet failus Data/ P453 un ieslēdziet aprēķinu bez pārstartēšanas. Alternatīvi lejupielādējiet no ITU-R pētījuma grupas 3 digitālo produktu lapas (itu.int, ITU-R pētījuma grupas 3, Digitālie produkti, balss P.453, virsmas dati un paaugstinātie kanalizācijas dati) un ievietojiet septiņus S_OCCUR. TXT, S_THICK. TXT, S_STREN. TXT, E_OCCUR.TXT, E_base.T Bez failiem programma izmanto tikai aprēķinu A un raksta to cilnē un reģistrā. Deficīta A un tipiskās intensitātes attiecība P.453 ir anomālija: Cik reižu šodien cauruļvads ir spēcīgāks nekā parasti. P453

## 7. Stacijas un lokatori

Saimnieks var būt ar 4, 6, 8 vai 10 rakstu zīmēm. Ar 8 vai 10 rakstzīmēm novietojums ir precīzs un augsnes daļa nāk no Copernicus GLO-30: antenas augstums ir pietiekami no zemes. Ar 4 vai 6 rakstzīmēm laukums ir pārāk liels: labāk uzrakstīt antenas kvotu uz jūras; ja 0 programma paliek aplēse ar SRTM augsnes centrā kvadrātā plus augstums no zemes un signālus to rezultātu. Atbilstošajam standarta augstumam ir 3 m no zemes; ar jūras daļu 0 izmanto SRTM augsni plus augstumu.

## 8. Zeme, difrakcija un troposktārs

Maršruta profils tiek ņemts Copernicus GLO-30 ik pēc 30 m (vairāk nekā 120 km platumā). Ekvivalentais Zemes rādiuss ir k = 157(157 + ΔN) ar ΔN N samazināšanos pirmajos km no VFS profiliem pa [6, 7] maršrutu.

Reljefs tuvu stacijām (no 1.3.2., autora kritērijs). P.452 maksā horizonta leņķi lineāri, 0,573 dB uz mrad: dažu desmitu metru reljefs dažu simtu metru attālumā no antenas maksā desmitiem dB, daudz vairāk, nekā tiek novērots. Šā iemesla dēļ 20 km attālumā no katras stacijas reljefi var saskaitīt līdz 6 dB: to dēļ horizonta leņķis ir ierobežots līdz vistālākajiem reljefiem plus 10,5 mrad. Tas ir derīgs jūsu stacijai un korespondentam. Līnija «Near reljefs» stāsta, cik daudz dB ir noņēmis.

Zemes avots (no 1.3.2.). Kvotas nāk no Copernicus DEM GLO-30 [3]: 1 sekundārs loks, jūra ir 0 un krasts ir dzidrs. Līdz 1.3.1. programmai tika izmantotas Terrain Tiles 'skadi' AWS ailes. Piekrastes tuvumā šie laukumi sajauc SSRTM ar zemas izšķirtspējas jūras gultnes datiem un izplata salas daļu jūrā: ap Capri izveido 80–100 m spoku gredzenu 0,5–1 km attālumā no krasta. 2026. gada 21. novembrī IC8TEM ziņoja par neesošu horizontu uz ziemeļrietumiem; salīdzinājums pa punktiem ar Copernicus to apstiprināja (94 m jūrā uz ziemeļrietumiem no Marinas Grandes pret 0). Šis viltus horizonts +2,6° piekrastes stacijām izmaksāja aptuveni 26 dB troposcatter. Skadi rūtis paliek kā rezerve tikai tad, kad Kopernikam nav atbildes. Copernicus kadri sver 15–30 MB un lejupielādē vienreiz; tikai jūras kadri neeksistē un to vērtība ir 0.

- Brīva vieta: Lfs = 92,44 + 20 log f(GHz) + 20 log d(km) [8].
- Difrakcija: Bullingtona metode reālajā profilā plus ITU-R P.452-16 § 4.2 [6] delta-Bullingtona sfēriskā zemes korekcija ar ITU-R P.526 [9] sfērisko zudumu.
- Pirmā Frenela zona: rādiuss (λ·d1·d2/d); brīvs, ja zeme atstāj vismaz 0,6 rādiusa. [9] naža mala ir redzama katram šķērslim.
- Profila konstrukcijā tiešais rādiuss ir zaļš optiskajā un sarkanajā skatā, ja tas iet zem zemes vai zem zemes izliekuma; sarkanie punkti ir šķēršļi, kas iebrūk Frenela 1. zonā (purpura punktētā līnija). Jūrā "sarkanais punkts" 0 m augstumā nav kalns: tā ir jūra, kas paceļas par izliekumu. Pārejot ar peli uz rādiusa vai uz punkta, parādās komikss ar paskaidrojumu un vērtībām.
- Kad reljefs antenas tuvumā aizver virzienu (piemēram, horizontu desmitiem grādu), difrakcija un troposcatter rada zudumus simtiem dB: virs 300 dB, programma raksta "> 300 dB" un "virzienu, ko noslēdz blakus šķērslis", signāls "< −230 dB" un S/N attiecības "−60 dB". Tādā gadījumā ieteicams apskatīt «Ieteicamo augu» un «Via monte».
- Troposcatter: ITU-R P.452-16 ekv. 45 līdz 50% [6] laika ar leņķisko attālumu no horizontiem un N0 no profiliem.
- Bez tropo: summa pilnvaras difrakcijas un troposcatter, nekad zem brīvās telpas.

## 8 bis. Troposcatter: četri modeļi un nenoteiktība

Blakus P.452 programma aprēķina troposcatter ar trim citiem modeļiem, tāpat kā TropoLink [16] programmu, un parāda to atšķirību kā nenoteiktību:

- NBS TN101 § 9 [25] (no 1.3.5.), mediāna: L = 30 log f − 20 log d + F(θd) − F0 + H0 + Lgp. F(θd) ir III pielikuma analītiskās formas (Ns 301) vājinājuma funkcija ar korekciju attiecībā uz Ns, H0 frekvences pieauguma funkcija 9.2. punktā: 144 MHz, ar zemu antenu viļņu garumā, atstarojums no zemes daļēji dzēš lauku kopējā tilpuma apakšējā daļā un 3–10 dB zaudējumu sāls. Ns ir stundas VFS profili, efektīvais horizonts un augstums nāk no Copernicus profila, Lgp = 0,07·e^(0,055(Gt+Gr)) ir atvēršanas savienojuma zudums. Tā ir tā pati forma, ko ITM izmanto savā interjerā, bet ar šodienas, nevis klimata reaģētspēju. Tā testē plakanos posmus 144 MHz frekvencē ar antenām 20 m attālumā: 2–4 dB robežās no ITM starp 150 un 900 km un aptuveni 2 dB vienkāršotā formā no TN101 9.2b, kam nav H0.

- ITU-R P.617-5 [17] L = F + 22 log f + 35 log θ + 17 log d + Lc, ar f MHz, θ mrad, d km, F = 0,18· N0 ·e^(−hs/7,35) − 0,23·ΔN un Lc = 0,07·e^(0,055(Gt+Gr)). N0 un ΔN parasti ņem no ITU klimata kartēm; šeit mēs katru stundu izmantojam profilus GFS ( N0 ·e^(−hs/7,35) ir reaģētspēja uz zemi, ΔN samazinājums pirmajā km), tāpēc arī troposcatter seko laiks.
- RSGB VHF/UHF rokasgrāmata Formula, DL2FZN [18]: L = Lfs + 24,44 + 4,283 ln f + 0,06656 d. Slīpums ar attālumu ir gandrīz vienāds ar PROPBER (0,067 dB/km).

Kopējais tilpums (no 1.3.1.): ir punkts, kurā tiek šķērsots abu staciju rādiuss, katra no kurām ir nosliece uz tās horizonta leņķi, uz Zemes ar līdzvērtīgu rādiusu ae = k·6371 km. No TX rādiusa ceļas z = hTX + x·θt + x2/(2·ae); Attālums x = (hRX − hTX + d^θr + d2/(2·ae) / θ, ar θ = d/ae + θt + θr. Ar plakaniem horizontiem un antenām pie vienas un tās pašas kvotas bāzes vērtība ir d2/(8·ae): apmēram 1,3 km līdz 300 km, 3,7 km līdz 500 km, 9,4 km līdz 800 km. Jo lielāks tilpums, jo vairāk gaiss ir sauss un retināts, un jo mazāk tas izplatās: šī iemesla dēļ vairāk nekā 800–1000 km troposcatter iztek. Rezultāti ir bāzes daļa un novietojums, reaģētspēja, GFS gradients un mitrums šajā augstumā, un ekvivalentais Bray attālums (QST, novembris 1961): katrs papildu vai mazāks horizonts ir apmēram 111 km attālumā. Atsauce: IU3QEZ (GEKO Magazine n. 69) prezentācija "Virzoties pāri horizontam VHF".

Troposcatter līnija, citi modeļi parāda abus zaudējumus un nenoteiktību kā pusi no starpības starp optimistiskāko modeli un pesimistiskāko. Tie ir tikai salīdzinājumi: zaudējumi bez tropo paliek P.452 zaudējumi.

## 8 ter. Pēcpusdienas troposcatter pār reljefiem (no 1.3.4)

IK3XTV no Thiene ( JN55, ar Alpiem pie 10 km), darbojas vasaras pēcpusdienās Dienvidvācijā 400–500 km, bez redzesloka, ar spēcīgiem signāliem, bet ar ātru un dziļu evanescenci; autors tos piedēvēja "konvekcijas šūnām, kas izlīdzinātas kā lēcas vilciens". Novērojums ir apstiprināts; programmā pieņemtais skaidrojums ir cits, fiziski stingrāks.

Pēcpusdienā saule samaisa gaisu pāri reljefiem un robežslānis paceļas līdz 2–3 km. Tās augšdaļā mitrais gaiss zem atbilst sausajam gaisam iepriekš: tur refrakcijas indeksa turbulence (lielums, kas regulē troposcatter) ir maksimālais. Kad šī virsotne sasniedz kopējo maršruta tilpumu, kas 400—500 km garumā ir apmēram 2—3 km, troposcatter tiek pastiprināts. Tas izskaidro pēcpusdienas laiku, augstu spiedienu (klusās debesis, spēcīga apkure) un strauju evanescenci, kas raksturīga difūzijai, nevis kanāliem. Konvekcijas šūnas nedarbojas kā lēcas: rada turbulenci, un signālu izplata turbulence pārstrādātā slāņa augšpusē.

Prognozes lasīšanas sekas:

- uz zemes pirmajā pēcpusdienā ir vissliktākais laiks zemajiem kanāliem un slāņiem, bet ne troposkam aiz kalniem: abas lietas jālasa atsevišķi;
- programmai jau ir divi apkalpojošie dati, modeļa nodrošinātā robežslāņa augstums un maršruta kopējā apjoma daļa (rezultātos reģistrē "kopējais apjoms"): ja tie ir tuvu, tad pēcpusdienas kalnu gardums var būt labāks par aprēķinu normālā atmosfērā;
- no 1.3.4. punkta aprēķina ņem vērā: ja kopējais tilpums ir uz zemes, maršruta reljefi ir vismaz 800 m, Saule ir lielāka par 15° un robežslānis pārsniedz 800 m, un slāņa pārbūves virspuse (parauga augsnes ātrums + limita slāņa augstums) atrodas 500 m attālumā no kopējā tilpuma pamatnes, troposcat guvums ir 3 dB; no 500 līdz 1000 m lineārais pieaugums nokrīt līdz nullei. Līnija "Troposcatter pēcpusdienā". Ar sniegu pa maršrutu (vismaz 10 cm uz zemes saskaņā ar modeli, sniega_dziļuma mainīgais Open-Meteo vai SNOD GFS no NOMADS) pastiprinājumu nepiemēro: uz sniega augsne paliek auksta un pārveidotais slānis neaug. Vērtība 3 dB ir provizoriska, un tā jākalibrē ar reālām saitēm; tā izslēdzas ar laika apstākļu korekcijām.

Avoti: F. Egano IK3XTV (Radio Rivista e www.qsl.net/ik3xtv, "Propagācija uz 2 metriem", "Konvekcijas šūnas", "Troposcatter pār Alpiem") novērojumi, avoti § 13.

## 9. Dibināšanas slānis maršrutā

Tā ir sirds signālu aprēķinu. Maršrutā GFS profili tiek veikti ik pēc aptuveni 25 km. Attiecībā uz katru spiediena slāni – garākā stiepe, kurā ir viens un tas pats slānis uz secīgiem punktiem; skaitīt kā vadu tikai biezi slāņi vismaz 40 m [10].

- nepārtrauktība: no kāda km līdz kādam km, un maršruta daļa;
- augšējā un apakšējā daļa, un slīpums m/km;
- augsne slāņa iekšpusē: km, kur kalni paceļas virs pamatnes;
- Aptvertā zeme: uz jūras kanāls ir gandrīz vienmēr, nolemj stiept sauszemes [10]. Ja zeme pārsniedz 10% no maršruta, izmantotā daļa ir minimums starp nepārtrauktību un zemes segumu.

Tropes pieaugums ir slāņa ieguvums izmantotajai frakcijai. Ja vairāk nekā puse profilu ir subrefractive, ieguvums ir negatīvs.

## 9 bis. Laikapstākļu korekcijas: stabilitāte, nosēdums, streiks

No 1,3.0 profils sniedz arī trīs meteoroloģisko informāciju, kas redzama reģistrā (regave «stabilitāte (Misme)»), Meteo līnijā pa rezultātu maršrutu un, ar Segni meteo kasti, kartē.

- Misme stabilitāte [5, 6. nodaļa]. Maldinājums saista lauku aiz horizonta ar saliktu parametru: M = a·(ge − 40 + b·ΔW), ar a = 0,5 dB uz N/km, ekvivalentu gradientu un termisko stabilitāti ΔW, darbu, lai paceltu gaisu 1 km. Programma aprēķina ΔW = g·ï(θ − θ0)/θ0 dz starp augsni un 1 km virs tās ar θ potenciālo temperatūru; standarta atmosfēra ir aptuveni 56 J/kg. Termins, kas pievienots pieaugumam, ir 0,07 dB J/kg virs standarta, ierobežots starp −4 un +6 dB un tiek skaitīts tikai tur, kur nav kanāla lamatas joslu (gradients jau ir Bean un Dutton indeksā). Kalibrēšana ir īslaicīga: tā jāpārbauda ar koeficientiem (14. punkts).
- Pazemināta un kalnu gaisu [19]. Moler and Holden maršrutos no 125 līdz 305 km līdz jūrai 1365 un 9380 MHz, konstatēja augstāko signālu vidēji par aptuveni 2,5 dB, gaisam krītoties lejup, gaisam paceļoties, un gadījumā pastiprinājums aptuveni 20 dB uz 3 stundām iegrimes sākumā, kam seko samazinājums par 22 dB, kad gaiss atkal saplūst. VFS dod vertikālo ātrumu 850 un 700 hPa. Ja abos līmeņos gaiss krītas vairāk nekā 1 cm/s un potenciālais refrakcijas indekss K ir apgriezts vai krasi samazinājies (N ziņoja adiabatiski līdz 1000 hPa, kas izceļ sauso un silto gaisu, kas nokritis no augšas [5]), tad pievieno 1,5 dB; ja gaiss paceļas par vairāk nekā 5 cm/s, ja tas paceļas. 2.
- [20] Scirocco. Karsts un sauss gaiss no Sahāras pāri vēsākam jūras gaisam dod ļoti spēcīgu inversiju no Vidusjūras austrumu daļas līdz Gibraltāram. S zīme parādās vietā, kur 850 hPa vēja nāk no 100–200° ar vismaz 7 m/s un temperatūra ir vismaz 16 °C, uz Vidusjūras. Tā ir tikai norāde: ieguvums izlemj profila slāņus.

Bez vada, kas slazdu joslu kopējais ieguvums nepārsniedz 16 dB, beigās dzeltens: korekcijas padara spēcīgāku slāņus redzamāku, bet ne garām tos cauruļvadiem (sarkanais un violetais paliek pie kanāliem).

- Noturība (no 1.3.1.). Lielākās atveres rodas jau pēc augsta spiediena dienām (VHF/UHF DX Book; Biancomano un Leyshon 1998 likums 12 stundas; 29/12/2021–2/1/2022 ziņojumi aug no pirmās līdz ceturtajai dienai). Katrai šūnai programma skaita iepriekšējās 24 stundas ar pieaugumu vismaz 13 dB un saskaita līdz 2 dB proporcionāli tikai tad, ja ieguvums jau ir vismaz 10 dB.

Kā izlasīt zīmes kartē (Maināt laika zīmes; no 1.3.2. simbols ir liels dubults un iet ar peli virs zīmes parādās komikss ar paskaidrojumu un šūnas vērtībām):

| Parakstīt | Krāsa | Ko tas nozīmē? | Ietekme uz tropu |
|---|---|---|---|
|  ↓  | zaļa | nosusināšana: gaisa pilieni (anticiklons), uzsilda kompresijas un sausa, stiprina inversija un uztur to stingru | labvēlīga, +1,5 dB |
|  ↑  | sarkana | augšupvērsts gaiss (priekšējais, zemais spiediens, konvekcija): sajauc zemus slāņus un pārrāvumus apvērses un cauruļvadus; | nelabvēlīga, −2 dB |
|  S  | apelsīni | Sīrups: karsts un sauss gaiss no Sahāras virs svaiga jūras gaisa | labvēlīgs pavediens, nav korekcijas: izlemt slāņus |

Laukums pilns ar zaļu ↓ zem anticiklona ir klasiskā zīme grūti atveres; Sarkanas ↑ bultas gar līniju teikt slāņi ir pārkāpj, lai gan šūnas krāsa joprojām ir laba.

Laika apstākļu korekcijas lodziņā šie labojumi tiek izslēgti, lai skatītu 1.2. modeli.

Sk. 1. zemsvītras piezīmi. Tas pats salīdzinājums norāda uz galveno robežu: VFS līmenī nav redzams ūdensvads jūrā nelielā augstumā, kas tajos laikos atnesa zīmes starp Kampāniju un Ligūriju.

## 9 ter. Jaunumi 1.3.2: divas aplēses, griesti, augsts spiediens, jūra, stacijas, grēdas

piesardzīgs un agresīvs novērtējums. Gradienta pieaugums pieaug no 0 dB līdz normas robežai līdz pat 10 dB līdz superrefrakcijas slieksnim. 1000–850 slānī piesardzīgs slieksnis ir Bean un Dutton (100 N/km); agresīvs slieksnis ir klasiskais superrefrakcijas slieksnis 79 N/km (k ≈ 2; P.453 izmanto −100 N/km pirmajos 100 m), kas ar k50 = 157(157 − ΔN) ir k ≈ 2 (k = 2,5 atbilst apmēram 94 N/km). Punkta aprēķins parāda abus zudumus sānos; ar kanālu, kas aiztur joslu, sakrīt. Kartē ir jāievēro piesardzība.

2500 m augstumā. Autoceļi un kanāli tiek meklēti tikai līdz 2500 m virs zemes: jo augstākas amatieru radio antenas tos neizmanto. Ar 1500 m validācija zaudēja atdalīšanās punktu un 4% savienojumu virs 16 dB, jo ziemā pazeminājumi bieži ir 1500–2500 m robežās.

Pastiprināts augsts spiediens. +1 dB, kur vismaz 7 no 9 apkārt esošajām šūnām ir samazināts spiediens uz jūru 1020 hPa vai vairāk un vējš ir vājš zemos slāņos (10 m zem 5 m/s, 850 hPa zem 8 m/s), izņemot augšupejošas kustības.

Jūras svars. Punktu aprēķinā slāņu guvumu reizina ar 0,8 + 0,4·μ1, kur μ1 ir koeficients ITU-R P.452 (eq. 51) garākajā nepārtrauktajā sauszemes posmā: 1 atklātā jūrā, neliels ar gariem sauszemes posmiem.

Stacijas un tās vada. Ja cauruļvads, kas aptver tikai daļu no maršruta, katras antenas daļu salīdzina ar kanāla pamatni un augšējo daļu segtās sekcijas sākumā un beigās; iegūto vērtību reizina ar 0,5 + 0,5· (e^(−ΔhTX/300)·e^(−ΔhRX/300)), ar Δh attālumu metros ārpus kanāla. Ar vadu gandrīz visā maršrutā ir sakabināšana § 10.

Slānis un grēdas. Ja VFS robežslāņa augšdaļa (pamata ātrums visvairāk robežas_slāņa_augstuma modelim) pārsniedz maršruta grēdas (maksimālā platība virs 300 m, ja augstākā pārsniedz 500 m), slānis iet pāri kalniem bez pārrāvuma: +1,5 dB segto grēdu daļai. Tas ir IT9GSF novērojums atverēs no Sicīlijas uz Vāciju ar labvēlīgu tirēnu.

Līmeņi tuvu pie grunts jūrā. Ar NOMADS avotu programma arī lejupielādē virsmas temperatūru (uz jūras ir SST), temperatūru un mitrumu pie 80 m un slānī 30—0 hPa virs zemes. Jūras punktos (modelis zem 5 m) šos līmeņus pievieno profilam zem pirmā spiediena līmeņa, lai redzētu vadu 100–200 m augstumā, ko spiediena līmeņi neatrisina. Uz zemes netiek izmantoti: nakts un dienas slāņi vēl nav derīgi. Open-Meteo dod tikai virsmas temperatūru.

Iztvaicēšanas cauruļvads. No SST, no temperatūras un mitruma līdz 2 m aprēķina iztvaikošanas vada δ augstumu ar neitrālo logaritmisko profilu (kā Paulus un Jeske modelī, bez stabilitātes korekcijas): gaiss uz ūdens apmatojumu ir piesātināts ar SST (98% sāls), tāpēc

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

ierobežota no 0 līdz 40 m. 144 MHz slazdos tas aizņemtu aptuveni 180 m (Kerr), tad 144 MHz frekvencē iztvaikošanas vads nostiprinās nedaudz; no 432 MHz uz augšu, ar zemām antenām jūrā daudz (11.a punkts). Ievadiet parabolisko vienādojumu un rindā Labojumi 1.3.2.

Dienas laiks (apmeklējums). Programma aprēķina Saules augstumu katrā punktā. Dienu no dienas uz Zemes ar sauli virs 15° un robežslāni virs 800 m vai vēju virs 5 m/s brīdina, ka zemes vadi ir pārformēti; naktī ar sauli zem −3°, vēju zem 3 m/s un robežslānis zem 300 m, brīdina, ka ar serēnu debesīm iespējams veikt apstarošanu, kas nokrīt pēc saullēkta. Ne jūrā. Tagad tie ir pamanīti, nevis skaitļi: decembra apstiprināšanai nav pietiekami daudz vasaras dienu, lai tos kalibrētu. Tie ir divi punkti, kurus autors uzskata par svarīgiem, no ikdienas novērojuma.

Normālais profils CRPL (alternatīvs). 40 N/km vietā fiksēts normāls samazinājums seko eksponenciālajai atskaites atmosfērai CRPL: N(h) = Ns·e^(−c·h), ar −ΔN pirmajos km = 7,32·e^ (0,05577·Ns) (Bean and Thayer 1959). Ar slapju gaisu (Ns augstu) normāls ir stāvāks un kalpo spēcīgāku gradientu, lai dotu peļņu; augstumā ir saldāks. Izrādās slikti, kamēr validācija saka, ka tas palīdz.

## 9 kvateri. Kontrasts gaisa, jūras un atklātā jūrā: Adrijas jūra (no 1.3.3.)

Radioviļņi nejūt jūras gultni: sajust kontrastu starp gaisu un ūdens virsmu. Dziļums skaitās, jo nolemj, cik ātri jūras temperatūra seko gaisa temperatūrai.

Adrijas jūra ir zema. Vidējais dziļums ir ap 250 m, bet ziemeļu baseins reti pārsniedz 100 m un Triestes līcis paliek zem 25 m; tikai uz dienvidiem nolaižas 1200 m augstumā. Ūdens slānis tik plāns sasilst un pēc dažām dienām atdziest, tirēnu — pēc nedēļām. Pastāv trīs tipiskas situācijas:

1. Pavasaris un vasaras sākumā, labvēlīgs. Jūra joprojām ir auksta, pat Alpu un Po upju saldūdenim; no līdzenuma vai Balkāniem tā ierodas karsts un sauss gaiss. Karstais gaiss virs aukstā ūdens rada reversiju jūrā (ieplūdes cauruļvads): tas ir klasisks nosacījums atverēm starp Itāliju, Slovēniju un Horvātiju.
2. Rudens un ziema ar bora, nelabvēlīga. Auksts gaiss virs siltākas jūras: zemais slānis kļūst nestabils, spiedes un kanāls pārtrūkst.
3. Pēc bora, iespēja. Tā kā jūra ir zema, dažu dienu laikā bora to atvēsina. Ja ievada anticiklonu ar karstu gaisu, gaisa-jūras kontrasts ir spēcīgs un kanāls ir izturīgāks nekā parasti.

Adrijas jūra ir arī slēgts kanāls starp Apenīniem un Dinari, nepilnu 200 km plats: jūras slānis paliek norobežots, un maršruti gar asi (Puglia–Venezia–Trieste, ap 700 km) paliek uz jūras, labākais nosacījums garam kanālam.

Ko programma aprēķina. Uz jūras punktiem, ar laikapstākļu labojumiem un lodziņu «Contrasto aria-mare»:

| Stāvoklis | Korekcijas |
| --- | --- |
| iespējamā temperatūra 925 hPa (vai 950) virs SST 4 K; | _ |
| ārpus SST 7 K | +2 dB |
| ar gaisa mitru temperatūru 925 hPa (virs 80 %) | –0,5 dB par šīm prēmijām |
| atdzesēta jūra 1,5 °C vai vairāk 72 stundas pirms, ar labvēlīgu kontrastu | _ |
| siltāks gaiss pie 2 m 2 °C (sajaukšana) | −1,5 dB |
| Siltāka 4 °C jūra | −3 dB |

Kopējā korekcija paliek robežās no −3 līdz +3 dB, un bez kanāla ieguvums paliek zem dzeltenā jumta. Robežvērtības ir īslaicīgas: standarta atmosfērā potenciālā temperatūra pie 925 hPa pārsniedz jūras temperatūru aptuveni 2–3 K, tad 4 K jau norāda uz pretējo.

SST atmiņa. Modeļi prognozēšanas laikā uztur jūras temperatūru gandrīz vienmērīgu. Nesenais samazinājums ir atvasināts no analīzes trīs dienas pirms (Open-Meteo, tikai virsmas temperatūra, tikai jūras šūnas): maksimāli 72 stundas pirms mīnus pašreizējā vērtība. Ja pakalpojums neatbild, atmiņas bonusu nepiemēro.

Robežvērtības. SST modeļiem zem krasta un Adrijas jūras augšējā daļā ir rupjš; piemēram, Po aukstā pildspalva ir slikti atrisināta. Batimetrijā aprēķinos neiekļauj: avoti vienmēr dod SST, kas jau apkopo jūras dibena efektu. Gaisa un jūras kontrasts daļēji pārklājas Misme stabilitātes beigās (§ 9 bis); par to prēmijas ir mazas. Robežvērtības tiks noteiktas, izmantojot vietas koridorus un QSO caur Adrijas jūru.

## 10. Vada visā maršrutā

Ja slānis slazdo joslu uz vismaz 80% no izmantotās daļas un stacijas nav redzams, tiek aprēķināts arī zaudējums caurulē: brīvā telpa, plus antenu savienojums (3 dB, ja tās atrodas vada iekšpusē, vairāk, ja tās ir virs vai zem), plus atlikuma vājinājums γd = 5·10-5·ae·f^(1/3) dB/mrad ITU-R P.452 eq. 49 [6]. Ja zaudējumi ir mazāki nekā zaudējumi bez tropo, tie kļūst par paredzamajiem zaudējumiem.

Josla paliek caurulē tikai tad, ja tās viļņa garums nepārsniedz λmax = 2 ~2 · (2/3 · (ΔM·10−6) ·d (Kerr, eq. 17 [11]). Šim pašam slānim var iesprostot 23 cm un 70 cm, bet ne 2 m. Tomēr robeža nav neto: Kerr brīdina, ka pat viļņi vairākas reizes ilgāk cieš daudz no vada, un Hepburn un VHF/UHF DX Book runā par "sfēriskām" malām un "piedot" kanāliem. No 1.3.1. programmā izmanto mīkstu kanālu: ja λmax ir starp λmax un 3·λmax, pieaugums pakāpeniski pāriet no 15 dB (λ = 3·λmax) līdz pilnam kanāla guvumam (λ = λmax), ar koeficientu s = (λmax/λ − 1/3)/(2/3). Tādā gadījumā ir atļautas kanāla krāsas. Loga un Meteo līnija maršrutā norāda punktus ar mīkstu vadu.

## 10 bis. Via Monte

Kad korespondents aizveras aiz horizonta, programma meklē labāko virzienu, lai tēmētu uz antenām: izgaismotais slīpums izklāj signālu kā radara mērķi. Tas veido horizonta apli ik pēc 5 (izņemot ±15° ap korespondentu) un katrā virzienā ieņem apgaismoto horizonta punktu, 100 m virs un 1—60 km attālumā. Ātra aplēse (brīva telpa, bitaktiskā konversācija un 10 dB katram horizonta līmenim no difuzora līdz korespondentam) izvēlas 4 daudzsološākos, vismaz 15° katrs no otra; katram:

- apstrādāt 1, stacija → reljefs, brīvajā telpā;
- apstrādā 2, reljefs → korespondents, ko aprēķina tā, it kā reljefs tiktu pārnests no 10 m virs augšas ar tādu pašu prognozi (bez paraboliskā vienādojuma);
- pārveidojums ar bistatiskā radara vienādojumu ar σ = σ0·A, σ0 = −15 dB (kalns VHF/UHF) un A laukumu, ko apgaismo R1 staru kūlis (maksimāli 20 km2):

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

Līnija "Ieteicamais punkts" norāda labākā maršruta azimutu, ja tas maksā mazāk par tiešo. Pie 144 MHz, ar reljefu 20 km un 15° jagi, kalnu ceļš maksā aptuveni par 45 dB vairāk nekā 2 maršruts pats par sevi: tas notiek tikai tad, kad tiešo noslēdz kaimiņu šķēršļi. Aplēse ir bruto (±10 dB), jo īpaši attiecībā uz σ0.


Bez zaudējumiem kalnam (opcija, no 1.3.2.). Ar «Via monte senza perdite fino al monte» kasti nokrišanu starp staciju un difrakcijas punktu uzskata par neesošu: tas ir kā nogādāt staciju augšā (antena 10 m virs punkta, tāda pati jauda, antena un kabelis). Pēc tam horizonts izvēlas punktu ar brīvāko horizontu korespondencšaha virzienā (ar paritāti, augstāko) un rezultāts parāda maršrutu no punkta uz korespondentu, ar signālu un S/N, blakus kopsummā ar difūziju. Tas ir optimistisks ierobežojums: saka, ja virziens paveras, kad kalns labi paceļ signālu, vai kur iet pārnēsājams. Katrs punkts ir dots ar desmit rakstzīmi.

## 10 ter. Ieeja attālā caurulē

Ja kanāls, kas aiztur joslu, sākas tālu, aiz stacijas horizonta, signālam ir jāierodas. Programma raida signālu līdz difrakcijas sākuma punktam cauruļvada vidū: gar Copernicus profilu GLO-30 pārmeklē malu ar augstāko Frenela parametru (ITU-R P.526) ar ekvivalentas zemes pietūkumu,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

vairāk pavājinājumu augšējās daļas izliekuma dēļ (noapaļots šķērslis, P.526-16 § 4.2.), R rādiuss aprēķināts no Copernicus GLO-30 augsnes līdz ±1 km no malas,

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

(m·n > 4 ir ieteikuma 34.b veidlapa). Barsis (NBS, 1963) mēra trijos garos maršrutos aiz kalna (Colorado, Aļaska, Eiropa) vidējie zaudējumi 10–20 dB augstāki nekā Frenela-Kirhhofa ideālajai malai: noapaļošana daudz izskaidro (Barsis un Kirby, 1961: Pikes Peak ar aptuveni 11,5 km rādiusu). Rezultāts tiek salīdzināts ar difrakciju uz gludas lodveida augsnes; tas ir sliktākais. Pēc tam signāls pārvietojas caurulē: brīva vieta visā maršrutā, atlikusī P.452 pavājināšanās γd uz korespondencšaha un savienojuma daļas (3 dB iekšpusē, 1 dB ik pēc 10 m ārpuses). Ja šis zaudējums ir mazāks par sagaidāmo zaudējumu, tas kļūst par mehānismu ("difrakcijas ievade"). Tas ir autora premisa par Itālijas ģeogrāfiju: ar blakus šķērsli krietni zem līnijas, v ir negatīvs un ieeja maksā maz; ar dorsālo fronte izmaksas 15-30 dB.

## 10 kvateri. ITM (Longley-Rice) un izeja no kanāla

No 1.3.2. programmā ir iekļauts neregulārais Terrein modelis 1.2.2. no NTIA/ITS (Longley and Rice, ESSA ERL 79-ITS 67, 1968; Hufford, Longley and Kissick, NTIA 82-100, 1982), kas pārtulkots C# ar ASV valdības oficiālo kodu, kas ir brīvs no tiesībām. Tulkojums dod tos pašus skaitļus kā oriģināls: auto kompilācija to katru reizi salīdzina ar NTIA C++ kodu piecos testa gadījumos (zem 0,01 dB).

ITM aprēķina zudumus normālā atmosfērā reālajā profilā Copernicus GLO-30: redzams, atstarojoties no zemes, difrakcija (spigols un sfēriskā zeme, kas svērta uz augsnes nelīdzenuma Δh, t. i., starpība starp kvotu desmito un 90° procentīli ap minimālo kvadrātu taisni) un troposcatter. Zemes reaģētspēja ir samazināta līdz maršruta vidējai daļai $N_s = N_0\,e^{-h/9460}$ un rada līdzvērtīgas zemes izliekumu,

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

Programma izmanto radio klimatu izvēlētā maršruta kā § 10 seksi (līdz 13.4 vienmēr mērenā jūra, ar N0 320 uz sauszemes un 350 uz jūras), ar N0 no vidējās reaģētspējas vietas (augsnes εr 15, σ 0,005 S/m; jūras εr 81, σ 5 S/m), horizontālā polarizācija un mainība laika gaitā bez vietas (profils ir zināms). Tas parāda zaudējumu pārsniedza 50% no laika un vienu, kas jums ir 10% un 1% no labākā laika. ITM nemodelē kanālus: to mainība laika gaitā ir statistiska pēc klimata. Tas kalpo kā bāzlīnijas otrais aprēķins blakus P.452; atšķirība starp abiem sniedz priekšstatu par nenoteiktību.

Otrā izmantošana ir pēdējais posms pēc kanāla, kas beidzas pirms atbilstošā. Izejas punktā pusceļā caur vadu tiek novietota virtuāla antena, un ITM aprēķina trasi līdz atbilstošajai, jo 50 % laika pārsniedz brīvo telpu. Kopējie zaudējumi ir

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,	heta_{condotto} + A_{ITM}(d_{uscita} 	o RX)
```

kur A_TX ir antenas savienojums (3 dB iekšpusē, 1 dB ik pēc 10 m ārpuses), ja caurule sākas tuvumā, pretējā gadījumā 10.ter punktā noteiktā ieejas difrakcija. Tā ir robeža, ko autors sev devis: slāņa nepārtrauktība plus, lielākais, atlikuma pavājināšanās. Ja tas ir mazāks par sagaidāmo zaudējumu, tas kļūst par mehānismu ("izslēgšana un galīgā stiepes ITM)").

## 10 kvinkiji. Padziļināšana: salas, jūras šaurumi un grunts zvejas pārtraukšana

Piemērs no Neapoles līča (Anacapri, JN70CN) Vidusjūras rietumu virzienā rāda, kur ceļi šķērso salas:

| Galamērķis | Azimuts | Kur tā šķērso salas | Šķēršļi |
| --- | --- | --- | --- |
| Barselona |  279°  | uz ziemeļiem no Sardīnijas (Gallura) līdz 41,05° N, starp 420 un 490 km | ap 30 km uz dienvidiem no Bočes di Bonifacio; Limbara (1359 m) ap 20 km no līnijas |
| Bocche di Bonifacio |  282°  | tuvu 41,3° N | aptuveni 11 km |
| Marseļa |  296°  | Centrālā Korsika 42,2° N platuma | kalni līdz 2700 m: kanāls zem 2 km tur pārtrauc |
| Valensija, Baleāru salas |  260–265°  | Sardīnijas centrālā daļa | iekšējie atbrīvojumi |

Barselonā pietiek norādīt uz 282°, nevis 279°, jo ass iet no mutes: 3rd, iekšā stars normālas jagi.

Vai mute ir šaura? Zemajiem cauruļvadiem zem dažiem simtiem metru, jā: salas ir sienas, un virs zemes, dienā, apkure pārrauj inversiju. Pats šaurums ir pietiekami plats: apmēram 11 km, pret pirmo Frenela teritoriju, mazāk kā viena kilometra plata puse no 1000 km maršruta 144 MHz. Problēma ir laika apstākļi. Mutes ir jautri vējam: Apakšējo slāni paātrina un apakšējo slāni noņem. Bieži eja ir atvērta kā ģeometrija, bet traucēta kā atmosfēra. Ar stabilu anticiklonu un maz vēja tā vietā var būt laba eja.

Zemes un jūras atšķirības. Tās skaitās. Cauruļvads, kas veidojas Tirēnu piekrastē, tiek pārtraukts: signāls nāk ārā no kanāla (sekcija pēc izejas, § 10 kvater) un tam jāatgriežas citā caurulē Baleāru jūrā, atkal maksājot par savienojumu. Cauruļvadi augstumā virs 1500 m šķērso Galluru, to neapzinoties; Korsikas vidiene to nepiemēro.

Praksē: ja maršruts šķērso kalnainu salu, izmēģiniet zināmu korekcijas pakāpi virzienā uz brīvās jūras posmu, bet neskaitās ceļā ar stipru vēju šaurumā.

## 10 seksi. TN101 radio klimats un laika mainīgums (no 1,3.5)

TN101 § 10 [25] apraksta, kā maršruta zudums ārpus horizonta mainās ilgā laika periodā ap vidējo, ar dažādām līknēm pēc klimata: Y(q) funkcija norāda, cik dB samazinās zudums q% no labākā laika. ITM satur šīs līknes septiņiem klimatiskajiem apstākļiem. Klimats skaitās daudz: uz jūras mērenajā jūras klimatā, 144 MHz, 10% no laika jūs nopelnīt daudz vairāk dB nekā uz sauszemes kontinentālajā klimatā, jo vadi ir biežāk.

Programma vairs nenosaka klimatu: Viņš to izvēlas katram gardumam no vidējā punkta, tāpēc tas darbojas visā pasaulē, ne tikai Eiropā. Lejupielādējiet no Open-Meteo [26] trīs gadu dienas vērtības ERA5 reanalīzei (temperatūra, mitrums un augsnes spiediens, lietus) šūnā 0,5° no vidējā punkta tikai vienu reizi; vērtības paliek Data/CLIMA. Augsnes atjaunošana

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

dienu pēc dienas, tad gada vidējais un ekskursija starp mēneša vidējo, vidējo mitrumu un gada lietus. Noteikumi ir saskaņā ar TN101 10.2 § klimata aprakstiem; robežas starp vienu klimatu un otru ir autora kritērijs:

- virs 60° platuma grādiem: mērens kontinentāls ( TN101 nav polārā klimata līkņu);
- mazāk nekā 250 mm lietus gadā un vidējais mitrums zem 50%: tuksnesis (Ns tipisks 280);
- 20° no ekvatora, N, vismaz 350, un ekskursija līdz 30: ekvatoriāls (Ns tipisks 360);
- 32° robežās un vismaz 60: subtropu kontinentāls, t. i., musoni (tipiska ekskursija 60–100);
- starp 10° un 32°, Ns, vismaz 340: subtropu jūras (Ns tipisks 370);
- abi jūras horizonti, virs 20°: jūrā rūdīta jūra (tā ir TN101 definīcija);
- ekskursija līdz 30 jūras tuvumā (jūra maršrutā vai vidējais gaisa mitrums vismaz 75%): jūras rūdījums uz zemes;
- citādi kontinentāls rūdījums (tipiska ekskursija 20–40).

N0 ITM ir vidējais samazinājums līdz jūras līmenim, N0 = Ns·e^(h/9460). Bez tīkla jūs izmantojat rezerves noteikumu ar platumu vien un jūras frakcija, un līnija saka.

Testi ar datiem ERA5 2023–2025: Capri Ns 339 ar ekskursiju 45 (kontinentāla norūdīšanās pa sauszemi, jūras mērenās joslas jūrā, kad abi horizonti ir jūrā); London 327 un 16 Vienna 319 un 24 Abidžana 381 un 11 (ekvatoriāla); Miami 365 un 36 (subtropu vīrs); Deli 337 un 82 (subtropu konkontinentāls); Riad 278 un 37, 90 mm lietus (deserts). Čīlē: Antofagasta 341 un 19 (subtropu vīrs: Atakamas krasts, ar gandrīz fiksētu zemu Klusā okeāna reversu), Kalama 232 uz zemes, 2257 m, 14 mm lietus (deserts), Valparaíso 331 un 11 (temperatūras šuve), Punta Arenas 313 un 5 (temperatūras šuve).

Līnija "Radio klimats ( TN101)" parāda klimatu, iemesls, Ns ar pusi no ekskursijas un Y: cik dB nopelnīt 10% un 1% no laika un cik zaudēt 90%. Tas ir ilgtermiņa statistika un paliek nošķirti no prognozes: gaidāmie zaudējumi seko laiku šodien ar VFS profiliem, un summa abas lietas būtu skaitās divreiz pašu patoloģisku pavairošanu.

## 11. Salīdzinājums ar ITU-R P.452

P.452 līnija izmanto ITU metodi attiecībā uz kanāliem un atstarošanos uz slāņiem (P.452-16 § 4,5 [6]) ar GFS ΔN mediānu, nevis uz Klimatoloģisko vienu un p = 20% kā kalibrēšanas konstanti, kā CSIRO [10] pētījumā. Tas ir tikai salīdzinājums: paredzamie zaudējumi paliek 9. punktā minētie zaudējumi.

## 11 bis. Paraboliskais vienādojums (no 1.3.1.)

Paraboliskā vienādojuma līnija ir ceturtais aprēķins, tikai salīdzinājums, kas veikts kā profesionālas programmas cauruļvadiem (PETOOL; viļņu propagandēšanas ietvars M. Lytaev, MIT licence): tā vietā, lai novērtētu ieguvumu no slāņiem, tas virza vilni pa maršrutu ar Fourier sadalītā posma metodi modificētā GFS M refrakcijas profilā, kas ņemti no prognozes punkta tuvāk katram attālumam. VFS līmeņi pievieno iespējamo kanalizāciju aplēstos lēcienus (aprēķina A, 50 m pusslānī). Augsne Copernicus GLO-30 ievada soļus (zem zemes lauks ir null), augsne ir diriģents un antenas stars ir Gaussian ar paredzamo atveri no guvuma. Aprēķini redz vadus, tos, kuri zaudē, difrakciju un antenu savienošanu; Viņš neredz troposcatter, kas tiek pievienots atsevišķi. Nākamais ir «profila čeks»: tas pats ar standarta atmosfēru, kas ir mazāka par to, kurai ir paredzētais profils.

No 1.3.2.:

- propagators ir platā leņķī ar brīvās telpas precīzu fāzi:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- jūras dibens un piekrastes līdzenums nulles augstumā atspoguļojas ar Frenela koeficientu horizontālā polarizācijā, εc = εr − j·60·λ·σ (jūras εr = 81 un σ = 5 S/m; valsts 13 un 0,006 S/m) maršruta tīrajā leņķī:

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- jūras nelīdzenums samazina konsekventu atstarošanos saskaņā ar Milleru un Braunu, ar faktisko viļņu augstumu no vēja līdz 10 m (σh = 0,0051·U2):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- uz jūras profils M ietver iztvaikošanas kanālu (9. ter punkts) ar neitrālo logaritmisko profilu:

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

Tas pārbauda ar prototipu (10 m antena uz jūras, iegūt uz standarta atmosfērā ar 100–300 km): ar δ = 10–30 m iztvaikošanas vads dod 1–3 dB līdz 144 MHz, 8–37 dB līdz 432 MHz un 21–66 dB līdz 1296 MHz. 144 MHz horizontālā polarizācijā jūra atspoguļojas gandrīz kā metāls (

Robežas: virs zemes saglabājas VFS līmeņu vertikālais profils, tāpēc plānie vadi paliek neredzami; zeme pret pakāpieniem ir aptuveni; stara paliek 6. vietā.

## 12. antenas, signāls un troksnis,

- Yagi DL6WU: pieaugums 9,5 + 7,8·log10(tilpums/λ) dBd (2,35 dB dubultam sprādzienam, DL6WU mērījumi VHF Sakari 3/1982), plus 2.15 [12] dBi.
- EIRP = jauda − kabelis + TX pieaugums.
- Saņemtais signāls = EIRP − paredzamie zaudējumi + ieguvums RX − kabelis RX. S-metrs IARU: S9 = −93 dBm, 6 dB uz vienību.
- Ārējais troksnis saskaņā ar ITU-R P.372 [13]: galaktikas (Fa = 52 − 23 log f) vairāk mākslīgs atbilstoši videi (pilsēta, dzīvojamā, lauku, klusā lauku) vai ar roku rakstītai antenas temperatūrai.
- Tsys = Ta + Tpre + [290(Lc − 1) + 290(Fr − 1)·Lc] / Gpre antenas spailēm (Frīsa formula). No 1.3.5. uztvērējstacijai antenā vienmēr ir priekšpastiprinātājs, fiksēts parametrs: trokšņa skaitlis 0,8 dB (Tpre = 59 K) un pieaugums 20 dB. Aiz tā atrodas kabelis (zaudējums Lc) un radio, ar pamata trokšņa numuru Fr 6 dB. Ar 1 dB kabelis un radio ieejas pilieni no aptuveni 1160 K uz 12 K: troksnis nosaka debesis, vidi un preamp. 20 dB ieguvums ir tipiska programmas izvēlēta vērtība.
- S/N SSB, CW un FT8 katram veidam, salīdzinot ar noteikto robežvērtību.
- Varbūtība periodam (no 1.3.2.): evansivitāte stundas laikā ir normāla S/N sadale ap paredzamo vidējo stundas vērtību. Bray (QST, 1961. gada novembris, 39. lpp., uz Norton 1959. gada līknēm) piešķir amplitūdu 13,5 dB virs apmēram 100 jūdzēm, t.i., apmēram 7 dB zem un 7 virs vidējā, kas nokrīt gandrīz taisni zem 100 jūdzēm (3,5 dB līdz 50 jūdzēm); "cietai" saitei aizņem 7 dB no budžeta. Pieņemtais ±7 dB kā ±2σ: σ = 3,4 dB 160 km attālumā, proporcionāli zemāk norādītajam attālumam. Varbūtība, ka periods pārsniedz ceļa slieksni, ir

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  ar Φ normālu sadalījuma funkciju: robeža 0 dB = 50%, +3,4 dB = 84%, +7 dB ("cietā" robeža Bray) = 98%. Tas neietver lielāku modeļa nenoteiktību.
- Digitālie ceļi: S/N 2500 Hz, salīdzinot ar robežvērtībām, kas mērītas pie 50 % dekodēšanas, bez iepriekšējas informācijas (AP). FT8 −21 dB un FT4 −17,5 dB uz balto troksni; ja mehānisms ir troposcatter izmanto robežvērtības ar evanescence "traucēja platuma vidē", FT8 −18,6 dB un FT4 −15,2 dB [21]. Q65 -15C −22,2 dB, Q65 -30B −24,8 dB, Q65 -60A −27,6 dB Gaisa kuģa izkliedē Doplers var pārsniegt Q65 -60A (1,7 Hz): ir labāki par 30B vai 15C. JT65A −25 dB [22]. AP dekodēšana (QSO jau zināmā vai beigu nosaukuma) dod aptuveni 2 dB papildus FT8 un 1,5 līdz 2,6 dB Q65; tas neiekļūst robežlielumā, jo tas nav derīgs visiem «−24 dB» vērtībām, kas bieži tiek nolasītas FT8 ir WSJT-X attiecību robeža vai AP dekodēšana, nevis 50 %. [24]

## 13. Atbildes un datu laiki

Katrs aprēķins mēra savu laiku: VFS profili (tīkls, analīze, obligāti 20 s pārtraukumi starp partijām Open-Meteo limit), Copernicus GLO-30 reljefs (ar rūtu lejupielādi, 15–30 MB katrs, tikai pirmo reizi), aprēķins. Atbildes laika līnija tos apkopo; reģistrā rindas sākas ar TEMPI. Atjaunojot tīklu, reģistrs ziņo arī par izmantoto VFS sacensību, kad tā kļuva pieejama un cik veci ir dati.

No 1.3.2. Jūs varat izvēlēties arī ICON-2I, aģentūras ItaliaMeteo un Arpae modeli Itālijā, kas atrodas aptuveni 2,2 km attālumā, caur Open-Meteo (modelis italia_meteo_arpae_icon_2i, divas sacīkstes dienā, 3 dienas). Tā aptver tikai Itāliju un tuvējās jūras, un tai ir mazāk zems VFS līmenis (1000, 925, 850, 700, 500 hPa): attiecībā uz ūdensvadiem augstumā nav labāka par VFS, bet smalkajā koordinātu tīklā labi aprakstītas piekrastes, salas un jūra 2 m augstumā, tas ir, iztvaikošanas vads. Mainīgie lielumi, kas modelim nav (stratu robeža, virsmas temperatūra...) izņem sevi no pieprasījuma. No 1.3.2. automātiskajā avotā tiek izmantots tikai NOMADS; Open-Meteo kalpo, ja NOMADS nereaģē un vēsturiskos gadījumos. Ar "Atjaunināt uz savu" lodziņu (izgaismo pēc noklusējuma) prognoze tiks lejupielādēta bez klikšķiem: atklāšanas, katrā jaunā GFS sacīkstē uz NOMADS (apmēram 5 stundas pēc laika sacensību) un, ja redzamais laukums ir ārpus vairāk nekā ceturtdaļa no tīkla; lielākais mēģinājums ik pēc 15 minūtēm, nekad ar vēsturisku gadījumu.

Pieteikumos Open-Meteo ir ierobežojumi minūtē, stundā un dienā katrai interneta adresei. Lejupielādētie profili tiek saglabāti uz diska (Dati/cache-gfs) katram insultam un modelim: ar to pašu sacīksti, kas mainās ik pēc 6 stundām, jau lejupielādētie punkti vairs nejautā. Ar automātisko avotu jūs sākat no NOMADS un Open-Meteo kalpo tikai trūkstošos punktus; ar vienīgo Open-Meteo, pie laika ierobežojuma vai katru dienu apstājas nekavējoties un saka, kad mēģināt vēlreiz. Vēsturiski gadījumi netiek kešoti.

## 14. Pārbaudīt modeli

Prognoze jāsalīdzina ar reāliem savienojumiem. Aprēķinu žurnāls glabā katru skaitli; programmu arhīvs tur reklāmas un dekodēšanas. 432 un 1296 MHz, kur vietu ir maz, tiem ir vajadzīgi ziņojumi no operatoriem: kurš veica savienojumu, ar kādu laiku, ar kādu signālu. Salīdzinot daudzas stundas gaidāmais ieguvums ar izmērīto signālu saka, ja robežvērtības ir jāpārvieto.

## Avoti

1. NOAA/NCEP, Globālā prognožu sistēma (GFS), globālais modelis 0,25°, četras rases dienā.
2. Open-Meteo, prognoze API (open-meteo.com), gfs_seamless modelis, mainīgie spiediena līmeņos un sacīkšu metadati.
3. Copernicus DEM GLO-30 (ESA; WorldDEM © DLR e.V. 2010–2014 e © Airbus Defence and Space GmbH 2014–2018, ko kā daļu no Copernicus piegādā Eiropas Savienība un EKA), 1 otrais loks, GeoTIFF kadri Amazon Web Services atvērtajā reģistrā. Rezervāts: NASA/USGS Copernicus GLO-30 in Terrain Tiles "skadi" of AWS.
4. ITU-R P.453, Radio refrakcijas indekss: tā formula un refraktivitātes datums, ar ciparu kanālu kartēm (Study Group 3, © ITU, personīgā un nekomerciālā izmantošana).
5. B. R. Bean, E. J. Dutton, Radio Meteoroloģija, NBS Monograph 92, 1966: 6.5. tabula un 4.4. punkts.
6. ITU-R P.452-16, Noteikšanas procedūras traucējumu novērtēšanai starp stacijām uz Zemes virsmas frekvencēs virs 0,1 GHz: § 4.2 (difrakcija), q. 45 (troposcatter), § 4.5 un q. 49 (vadīta).
7. ITU-R P.834, Troposfēriskās refrakcijas ietekme uz radioviļņu izplatīšanos.
8. ITU-R P.525, brīvās telpas vājinājuma aprēķināšana.
9. ITU-R P.526, propagandēšana ar difrakciju: 31. ekv. (cutcheon) un 3. § (sfēriskā zeme).
10. B. Indermuehle, H. Suzuki, Troposfēriskā ducting Prediction balstīta uz GFS Model Data inyarrimanha Ilgari Bundara, CSIRO Murchison Radio-astronomijas observatorija, Astronomijas biedrības Austrālijas publikācijas, 2026 (arXiv 2608.12861).
11. D. E. Kerr., Īsu radioviļņu propagandēšana, MIT Radiācijas laboratorijas sērija vol. 13, 1951: ekv. 17 e 18.
12. G. Hoch DL6WU, «Extremely Long Yagi Antennas», VHF Communications 3/1982, pp. 130 et seq., fig. 1 (mērot 432 un 1296 MHz atkarībā no uzplaukuma garuma).
13. ITU-R P.372, Radiotroksnis: galaktiskais un mākslīgais troksnis videi.
14. NOAA/NCEP, NOMADS, GRIB filtrs GFS 0,25° laika (nomads.ncep.noa.gov); WMO G RIB2 formāts, komplekss iepakojums ar telpiskām atšķirībām.
15. Open-Meteo, Historical Forecast API (paraugprognozes arhīvs kopš aptuveni 2021. gada).
16. LukiBox, TropoLink, Troposcatter maršrutu dizains ar P.617, NBS TN101 un ITM flangu (GitHub, Apache 2.0 licence).
17. ITU-R P.617-5, Propagandēšanas prognozēšanas metodes un dati, kas nepieciešami transhorizona radioreleju sistēmu projektēšanai, 2019. gads.
18. RSGB, VHF/UHF rokasgrāmata; DL2FZN ieviešana (andi-f/troposcatter on GitHub, 2013).
19. W. F. Moler, D. B. Holden, «Troposfēriskā izkliede un atmosfēras cirkulācija», The Journal of Research of the NBS 64D, 1960, doi 10.6028/jres.064d.014: signāls aiz horizonta (1365 un 9380 MHz, jūrā) un vertikālā gaisa kustība.
20. ARRL Handbook 2022 (99a ed.), § 19.4.3–19.4.4: laikapstākļi un troposferiskā izplatīšanās.
21. S. Franke, B. Somerville, J. Taylor, The FT4 un FT8 Communication Protocols, QEX 2020. gada jūlijs/augusts.
22. W3SZ, sliekšņi lēno digitālo režīmu ( JT65, JT9, QRA64) un režīma izvēle saskaņā ar pavairošanas, 2019.
23. QST, "Pasaule virs 50 MHz", 2022. gada novembris: atklāšana 2022. gada 14.–15. augustā starp Manitobu un Kanzasu/Misūri.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Quick-Start Guide to Q65, 2021. gada 3. aprīlis (wsjt.sourceforge.io).
25. P. L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis. H0
26. Open-Meteo, Historical Weather API (ECMWF ERA5 pārskats kopš 1940. gada; Copernicus Klimata pārmaiņu pakalpojums, CC BY 4.0 licence).
