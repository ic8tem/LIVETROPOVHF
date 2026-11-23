LIVETROPOVHF · tehniskais dokuments 3
# Kā veidot tropu 144 MHz un tālāk

> Mašīntulkojums no itāļu valodas: šaubu gadījumā noteicošā ir itāļu rokasgrāmata.

Kas notiek gaisā, kad 144, 432 un mikroviļņu iet ārpus horizonta: refrakcija, inversijas, kanāliem, izkliede, un kā izlasīt tos programmas kartē. 2026. gada 9. novembra redakcija.

## Kopumā

- VHF viļņi un tālāk ceļot pirmos kilometrus atmosfērā (troposphere) un saliekt, kur refrakcijas indekss mainās ar [3] [7] [11].
- Indeksu mēra ar refrakcijas N, kas ir atkarīgs no spiediena, temperatūras un galvenokārt no [11] mitruma.
- Normālā atmosfērā ir aptuveni 40 vienības uz km; kad tas nokrīt vairāk nekā 157 vienības uz km, vilnis paliek iesprostots [7] [11] caurulē.
- Caurule nāk no siltuma inversija ar sausu gaisu virs un slapjš zemāk: anticiklona, jūras, serēnu naktis, [3] [4] [5] [13] frontes.
- Jo augstāka frekvence, jo plānāks kanāls ir pietiekami: mikroviļņi atveras biežāk nekā 144 [3] [7].
- Bez vada paliek troposphere izkliedētājs: vienmēr klāt, vājš, kas krīt ar attālumu [6].
- Programmā: augsts spiediens + reversais + Hepburn/ F5LEN augsts = iespējamie apstākļi; plankumi apstiprina [1] [2] [13] [14].

## 1. Kur tas notiek: troposfēra

Troposfēra ir zemākais atmosfēras slānis, līdz apmēram 10—12 km. Par trope skaitās galvenokārt pirmie 3 km: ir ūdens tvaiki un inversijas, un tur iet radio maršruti starp stacijām uz zemes [4]. Normālos apstākļos temperatūra pazeminās ar augstumu aptuveni 6,5 °C uz km [3]. Savukārt, kad slānī temperatūra paceļas līdz ar augstumu, mēs runājam par siltuma inversiju: tas ir sākumpunkts gandrīz visām spēcīgajām tropo atverēm.

VHF jonosfēra neatspoguļojas kā HF (salve un sporādiski, kam ir citas stundas un citas distances [5]): ārpus horizonta tikai tad, ja atmosfēra tos noliec uz zemes vai izplešas.

## 2. Refrakcija un refrakcijas koeficients N

Radioviļnis izliecas pa gaisu ar gaisa refrakcijas indeksu ar citu. Indekss n ir ļoti tuvu 1, tāpēc tiek izmantota reaģētspēja N, t.i. n - 1 reizināta ar miljonu [11]:

```math
N = (n - 1) \cdot 10^{6} = \frac{77,6}{T} \left( P + 4810 \, \frac{e}{T} \right)
```

ar P spiedienu hPa, T temperatūru kelvinā un ūdens tvaika spiedienu hPa. N augsne parasti ir 300–400. Otrais termins — tvaika — sver daudz: šī iemesla dēļ mitrums skaitās tikpat daudz kā [3] [11] temperatūra.

Tas, kas liek vilni, ir nevis N vērtība, bet kā tas mainās ar kvotu, gradients dN/dh:

| dN/dh (N/km) | Stāvoklis | Ietekme uz vilni |
| 0 | Subrefrakcija | Vilnis virzās prom no zemes, īsāks horizonts |
| No 0 līdz -79 | Normāls (standarts −39) | Gaismas locīšana zemē: radio horizonts aptuveni 4/3 no optiskā horizonta |
| no −79 līdz −157 | Superrefrakcija | Viļņa saliecas vairāk, palielināts plūsmas ātrums |
| mazāk nekā −157 | Saturs | Vilnis saliecas tikpat stipri kā Zeme un paliek iesprostots |

Robežvērtība −157 N/km ir punkts, kur rādiusa izliekums ir vienāds ar Zemes [7] [11] izliekumu. Tādējādi "ekvivalents zemes stars" 8497 km (4/3 no reālā rādiusa), ko izmanto, aprēķinot [6] izkliedi.

## 3. Apvērses: tropo motors

Ja gradients ir mazāks par −157 N/km, N ir jāsabrūk dažos augstuma metros. Tas notiek, ja virs slāņa svaiga un mitrs gaiss ir siltāks un sauss: temperatūra paaugstinās un tvaiks krīt kopā, un abi sanes N [3] [5]. LA0BY norāda inversijas līdz 10–15 °C uz km pret normālo samazinājumu 6,5 °C [3].

Inversijas rodas dažādos veidos:

- Nogrimst anticiklonos. Augstajā spiedienā gaiss augstuma kritumā lēnām saspiežas un iesilst, savukārt uz zemes paliek svaigs un mitrs gaiss. Tas ir cēlonis garākām un ilgākām atverēm, kas ilgst dienas [3] [4] [13].
- nakts starojums. Ar rāmām debesīm un vāju vēju augsne atdziest naktī un atdzesē kontakta gaisu: Zemā reversā, kas pārtrūkst ar rīta sauli. Vakara un rīta atveres, raksturīgas vasarai un rudenim.
- Jūras brīdinājums. Karsts un sauss gaiss, kas nāk no zemes virs aukstākas jūras: jūras slānis paliek mitrs un atdzist zemāk. No šejienes lielie jūras ceļi (Atlantijas jūra, Vidusjūra, Ziemeļjūra) [4] [13].
- Priekšpuses. Siltas frontes priekšā karstais gaiss plūst pāri aukstajai; atvere ir īsa un seko frontei.
- Iztvaicēšana. Virs jūras, pirmajos metros, mitrums strauji krītas ar augstumu: ļoti plāns kanāls, noderīga tikai mikroviļņu.

Zīme, ka reverss ir redzams vertikālajos profilos (radiosondaggi): temperatūras paaugstināšanās un rasas punkts iet uz leju tajā pašā stiept [5]. Programma parāda pavedienu par to ar Thermal Inversion slāni (gaiss pie 925 hPa siltāks nekā uz zemes) un ar izotermālo pie 850 hPa.

## 4. Virsma un augsti vadi

Caurule ir slānis, kurā vilnis pārvietojas, veselojot starp augšējo malu (inversija) un zemi vai apakšējo malu [7]:

- Virszemes cauruļvads: grunts vai jūras. Tieši ienāk zemā augstuma stacijas, īpaši piekrastē.
- Augsts cauruļvads: stāv augstumā, piemēram, no 500 līdz 2000 m. Stacijas līdzenumos, iespējams, nejūtas, bet tās, kas atrodas pakalnā vai kalnā, ieiet kanālā. Paskaidrojiet saites, kas šķērso Alpus vai citus [3] reljefus [4].

Lai ievadītu vadu vilnis jāsāk gandrīz horizontāli: zemas antenas pie horizonta un nav blakus šķērslis. Cauruļvada iekšienē vājinājums ir daudz mazāks nekā brīvajā telpā, un attālums sasniedz 1000–2000 km un vairāk (piemēram, savienojumi starp Ibērijas pussalu, Kanāriju salām un Angliju vai starp Skandināviju un Krievijas dienvidiem) [3].

## 5. Frekvence: kāpēc mikroviļņi atveras biežāk

Cauruļvads aiztur tikai diezgan īsus viļņus, salīdzinot ar tā biezumu. Garie viļņi “iznākt” no plāna kanāla; Īsie tie nav. Tad [3] [7]:

| Grupa | Viļņa garums | Kas tev vajadzīgs? |
| 144 MHz |  2 m  | Biezie cauruļvadi (metru centrs), platas un labi iezīmētas inversijas: retākas, bet ļoti plašas atveres |
| 432 MHz | 70 cm | plānāki cauruļvadi: bieži atveras, kad 144 ir tikai "preces" |
| 1296 MHz | 23 cm | Nedaudz vāju inversiju; gari ceļi jūrā bieži vasarā |
| 10 GHz | 3 cm | Tikai iztvaikošanas cauruļvads jūrā; arī skaita absorbciju lietus |

Šī iemesla dēļ pieticīgs tropo indekss jau var nozīmēt atveres UHF un mikroviļņu krāsnī, bet 144 aicina uz augstākām vērtībām. No otras puses, tajā pašā caurulē 144 ir mazāk zaudējumu ārpus kanāla un labāk aptver pierobežas zonas.

## 6. Troposfera izkaisījums

Pat bez inversijas troposfēra nav viendabīga: Neliela temperatūra un mitrums neregulāri izplatās nelielu daļu enerģijas ārpus horizonta. Tas ir vienmēr klātesošais troposferiskais izkliede, kas ļauj savienot 300–800 km ar labām [6] stacijām. Signāls ir vājš un svārstīgs; zaudējums aug ar difūzijas leņķi, tas ir, ar attālumu un šķēršļiem pie horizonta:

```math
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
```

ar θs difūzijas leņķi grādos un f MHz [6]. Programma izmanto šo formulu cilnē katra vietas, lai noteiktu, vai saite var izskaidrot ar izkliedi vai nepieciešama kanāls (skatīt dokumentu 1). Augsta N vērtība uz zemes uzlabo [6] izkliedi.

Nejaukt ar aircatter (atstarošana uz plaknēm: dažu sekunžu signāli, Dopler) un izkliede meteoru (atstarošana uz meteoriem), ko programma izslēdz no tropo.

## 7. Laikapstākļu attēls: kā to izlasīt kartē

Tropo atveres seko meteoroloģijai. Tipiskas situācijas [3] [4] [13]:

- Stabils anticiklons, augsts spiediens (vairāk nekā 1020–1025 hPa), plats izobars, maz vēju: noslīdējums. Labākās atveres seko anticiklona malai, īpaši uz jūras.
- Calo della presione: inversijas pārtraukumi. G4MCU sekoja SK6VHF bākai, līdz spiediens samazinājās līdz 1012 hPa [13].
- Karstais gaiss augstumā (izoterms 850 hPa augstumā) virs svaiga gaisa uz zemes: atpakaļgaitā.
- Konfektīvas šūnas un gravitācijas viļņi (piemēram, zemvēja Alpos) var radīt vietējas un īsas atveres [4].
- Dienas laiks: apstarojuma inversijas ir spēcīgākas vakarā un agri no rīta; noslīdēšana paliek arī dienas.

Hepburn [1] un F5LEN [2] indeksi visu šo apvieno no laikapstākļu modeļiem: Viņi aprēķina N pie dažādām kvotām un pārveido gradientus par skaitli vai klasi. Es neesmu garantija: LA0BY parāda LA– UA6 notikumu, kas ir redzams augstā temperatūrā, nevis Hepburn [3]. Tam programma rāda kopā indeksus, izobari, temperatūras un reālos plankumus: ja plankumi pieskaņojas augstam indeksam zem anticiklona, atvere notiek. KP4MD konstatēja, ka SNR WSPR ziņo 144 MHz pieaug tieši logos, ko nodrošina Hepburn [14].

## 8. Praktiskais saraksts

1. Paskatieties uz tropo indeksu jūsu reģionā un virzienā, kas jūs interesē, nākamās dažas stundas.
2. Pārbaudīt izobari: augsts spiediens un plašs izobare vairāk vai blakus ceļam.
3. Ieslēdziet termālo inversiju un T 850 hPa: karstu gaisu augstumā virs ceļa.
4. Dodiet priekšroku maršrutiem jūrā un vakara un rīta stundās.
5. Ja jūs esat uz kalna, padomājiet par augstajiem kanāliem; Ja tu esi krastā, virspusē.
6. Mēģiniet vispirms augstākās joslas (432, 1296): atvērts pirmais.
7. Pārbaudīt ar vietas un PSKReporter: garas līnijas tajā pašā virzienā apstiprināt vadu.

## Avoti

1. W. R. Hepburn, DX Info Centre, “The Hepburn Tropo Index (HTI)” un “Troposfēriskās ducting Prognozes” lapas (dxinfocure.com).
2. F5LEN, prognoze Eiropai un pasaules prognozes par VHF troposfēras pavairošanu (tropo.f5len.org).
3. S. Heck LA0BY, “Extreme Tropo Propagation on 144 MHz and up”, 41° Nordic VUSHF Meeting, Skjeberg 2019. Normāls gradients un inversijas, temperatūra un mitrums, augsts spiediens, LA– UA6 notikums, savienojumi aiz kalniem.
4. F. Egano IK3XTV, “Propagation VHF – Konvekcijas šūnu un atmosfēras gravitācijas viļņu ieguldījumu Tropo pavairošanā.” Pirmais 3 km, jūras ceļi, konvekcijas šūnas, gravitācijas viļņi, augsts spiediens.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, “Ļoti liela attāluma izplatīšanās 144 MHz joslā”, 24/7/2003. Temperatūras un rasas punktu profili, inversijas, atšķirība no E sporādikas.
6. J. N. Gannaway G3YGF, „Troposfēriskā izkliedēšana", QST 1983. gada novembris. Zemes rādiusa ekvivalents un izkliedes zudums.
7. VHF/UHF DX grāmatas 2. nodaļa “Troposfēras izplatīšanās”. Refrakcijas zonas saskaņā ar dN/dh un virsmas un augstkanāla profiliem (tikai attēliem).
11. Ieteikums ITU-R P.453, radiorefrakcijas indekss. N formula un kanāla slieksnis pie −157 N/km. Vispārīga atsauce, nav pievienota pielikumā.
13. G4MCU, „troposfēriskie" raksti (g4mcu.net), 2018—2025. Kondoti ar augstu spiedienu, īpaši uz jūru; SK6VHF un samazinās līdz 1012 hPa.
14. C. F. Milazzo KP4MD, “VHF/UHF WSPR propagandas pētījums”, 2012.–2014. SNR WSPR 144 MHz un Hepburn prognoze.

Avota numuri ir tādi paši kā dokuments 2, Map Building.
