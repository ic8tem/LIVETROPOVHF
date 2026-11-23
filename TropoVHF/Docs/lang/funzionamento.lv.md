LIVETROPOVHF · tehniskais dokuments 1
# Programmas darbība

> Mašīntulkojums no itāļu valodas: šaubu gadījumā noteicošā ir itāļu rokasgrāmata.

Ko programma dara, no kurienes tā veic datus un kā tā pārvērš tos to, ko redzat kartē un sarakstā. Ar projektu saistītā 2026. gada 9. novembra versija C# (.NET 10 + Avalonia).

## Kopumā

- Nolūks: tropo prognoze ( Hepburn [3], F5LEN [4], izobare GFS [18]) un joslu reāla novērošana vienā logā saskaņā ar LA0BY [1] kontroles sarakstu.
- Datu vākšana: divas DX kopas, Reversās Beacon Network [9], dm4x klaster un PSKReporter [5] MQTT plūsma, UDP dekodēšana no WSJT-X, JTDX un MSHV [8].
- Spot: joslas segmenti, negatīvs filtrs, airscatter brashed dzeltens, atrašanās vietas vai CALL3.TXT [6] [7].
- Kas klausās: tumši rozā apļi ar rozā līnijas vienmēr redzams pret stacijām jutās [5].
- Spot karte: attālums, azimuts, tropo indekss uz ceļa un troposcatter zudumi ar G3YGF [2] metodi.
- Valodas Hepburn un F5LEN: [3] attēls un kalibrēšanas stends [4].
- Skatīties: NTP kontrole uz 10 serveriem ik pēc 3 minūtēm [10] [11].
- Aizdomās turētie ziņojumi: EME Q65/JT65 ar Mēness stāvokli, atdalītiem vietrāžiem, slēpotājiem, karantīnu un klausītājiem, ar saules [20] apgūtajiem noteikumiem.
- Izmešanas kritēriji un divās tabulās apkopoto krāsu nozīme (11. ter un 11. kvatera sadaļa).
- Tēmas, 36 [17] valodās, eksporta dokumentus Word ar rediģējamas formulas [16], portatīvo programmu un multiplatformu [12] [13] [14] [15].

## 1. Mērķis

Programma saliek kopā vienā logā divas lietas, kas parasti aplūko dažādas vietas: Troposfer izplatīšanās prognozēšana (Tropo indekss Hepburn un F5LEN, izobare) un novērošana par to, kas tiešām notiek uz joslām (punkts no DX klastera, Reverse Beacon Network un PSKReporter, plus saraksts ar stacijām, kas klausās). Atsauces josla ir 144 MHz; punkti savācas no 50 MHz uz augšu.

Uzstādījums seko kontroles sarakstu, ko LA0BY piedāvā ekstrēmiem tropo notikumiem: iepriekšējās dienās jūs apskatīt Hepburn un F5LEN prognozes un temperatūras profili augstumā, kamēr notikums ir uz DX klastera un uz vietas kartē pierādījumus, ka atvēršana sākas [1]. LA0BY arī atzīmē, ka vienīgās Hepburn/ F5LEN kartes nav pietiekamas, lai prognozētu garākos savienojumus [1]

## 2. Logs un cilnes

Logs ir viens un nesadalās mobilajos logos. No augšas uz leju:

- Vispārīgie iestatījumi rinda: Zvans un lokators pa kreisi; labajā pusē UTC pulkstenis (piemēram, 08/11/2026 20:33:38 UTC, dzintara teksts), kam seko NTP:OK vai NTP:FAILED, tēmas izvēlne un valodas izvēlne.
- Cilņu skaits piecās grupās: Karte un plankums; Prognozes (Propria β, programmas aprēķinātā prognoze un aprakstīta Prognozes rokasgrāmatā, pēc tam Hepburn un F5LEN); GRAVES; Dati un avoti (DX klasteris, PSKReporter, Reversā Beacon, UDP, Tikšanās un aizdomīgi ziņojumi); Rokasgrāmatas (Programmas operācija, Karšu būve, Kā veidot tropo, Rokas prognoze, Padomi operatoram, ar meklēšanas lodziņu, kurā redzams teksts). Zem stara bārā redzamas grupas apakšvalodas; katra grupa atceras pēdējo lietoto.
- Cilnes saturs un, apakšā, statusa josla ar avotu stāvokli, nosaukumu datubāzi un pēdējās atjaunināšanas laiku.

Augšējās līnijas iestatījumi attiecas uz visām cilnēm: izvēlētā tēma arī krāsu datu vākšanu un dokumentu fonu; Dokumenta lapas ir tikpat baltas kā lapa, tāpēc tās labi izdrukā.

## 3. Zvans un saimnieks

Maidenhead saimniekam var būt 4 vai 6 zīmes (piemēram, JN70 vai JN70CN). Programma pārvērš to kvadrāta centra koordinātās: lauks 20° × 10°, kvadrāts 2° × 1st, apakškvadrāts 5′ × 2,5′. Ja teksts nav derīgs vietrādis, ailes mala kļūst sarkana un stacija kartē neparādās. Vietnieks kalpo arī kā azimuta projekcijas centrs un kā sākumpunkts attālumiem un azimutam reklāmas dēļā.

Zvans ir nosaukums, ko izmanto kā klastera pieteikšanās. Pirmajā startā, līdz ir derīga nominācija, kopas nesavienojas: logs lūdz vārdu (un, pēc izvēles, saimnieks); apstiprināts, ka nosaukums tiek kopēts kā lietotājvārds Login laukā katram klasteram, kam tas ir tukšs un kopas atstāj. Ar “Vāks” paliek, un Connect poga katra klastera prasa nomināciju vēlreiz. MQTT, UDP un NTP neizmanto pieteikšanās un atstāt vienalga.

## 4. Datu vākšana: avoti

Katram avotam ir sava sadaļa datu vākšanas cilnē, ar savienoto/atvienoto, atpazīstamajām un savienotajām kastēm palaišanas brīdī, statusu, skaitītājiem un saņemto rindu reģistru. Neizslēgtas vai izmestas līnijas no filtra reģistrā neparādās: skaitīt tikai sekcijas metrus. Sekcijas ir blakus ar diviem līdz diviem.

| Avots | Savienojums | Kas nes |
| 1. DX klastera izvēle | on0dxk.dyndns.org:8000 komandu komplekts/filtrs dxbm/pass 6 | Skots amatieru radio, arī SSB un CW. ON0DXK VHF tiek filtrēts un ieslēgts ar norādīto komandu (poi caurlaide 4, 2...). |
| 2. Otrais avots | dxmaps.com:7300 (DXMAPS, EA6VQ) un s50clx.si:41112 ( S50CLX, Slovēnija) | DXMAPS savāc VHF plankumus un dod arī RBN skimmera plankumus (iestatīta/skimmera kontrole, jau iestatīta); S50CLX ir DX Spider klasteris. Pieteikties ar savu vārdu; rediģējamas adreses. |
| 3. Reversās signālu pārraides tīkls | Telnet.reversebeacon.net 7000 CW/RTTY · 701 FT8 | Automātiskā skimmera vieta. Pieteikties ar nosaukumu tikai [9]. |
| 4. Kopa ar PSKReporter | dm4x.ddns.net:8500 | Digitālā Spots PSKReporter klastera formātā. |
| 5. PSKReporter MQTT | mqt.pskreporter.info 1883 · 1884 ar TLS | Katrs reāllaika uzņemšanas ziņojums: kurš pārraida, kurš klausās un viņu [5] Locators. 144, 222 un 432 MHz joslas. |
| 6. Laikapstākļi un tropo kartes | dxinfocentre.com/tropo_eur.html tropo.f5len.org/Eiropas GFS modelis | Hepburn [3] un F5LEN [4] tropo indeksa kartes, jūras līmeņa spiediens izobarei (GFS via Open-Meteo [18]). Autoriem jāpieļauj Hepbērna un F5LEN. |

Kopa telnet (avoti 1–4). Pēc savienojuma programma gaida nosaukuma pieprasījumu (teksts, kas satur pieteikšanās, zvans:, zvans vai jūsu zvanu); ja tas neierodas 4 sekunžu laikā, lūdzu, nosūtiet pieteikšanās. Pēc tam nosūta sekcijas komandas, kas atdalītas no punkta un komata, pa vienai ik pēc 0,6 sekundēm. Pirms rindu nolasīšanas tiek noņemtas telnet kontroles rakstzīmes. Ja savienojums krīt un Riconnetti ir aktīvs, mēģiniet vēlreiz pēc 10 sekundēm (15 ja serveris vispār nereaģē) [6].

PSKReporter MQTT (avots 5). Programma ietver tēmu pskr/filter/v2/{banda}/{modo}/{tx_call}/{rx_call}/{tx_grid}/{rx_grid}/{tx_dxcc}/{rx_dxcc} nosakot tikai joslu un atstājot citus laukus bez maksas ar rakstzīmi + [5]. Precīzs nosaukums, ar kuru serveris norāda joslas (piemēram, 2m, 1,25m, 70cm) jāpārbauda pie pirmā savienojuma.

## 5. No klastera līnijas līdz vietai

Viena līnija ir vietas, kad tā ir forma DX de SPOTTER: frekvence DX komentārs hhmmZ. Programma iegūst punktnieku, DX staciju, frekvenci kHz, komentāru un tagad UTC; nosaukumi paceļ skimmeru sufiksus (-#, -1...). Tad šajā secībā piemēro šādus [6] noteikumus.

| Grupa | Segments pieņemts |
| GRAVES (ar 144) | 143,040 – 143,060 MHz |
| 144 MHz | 144–148 MHz |
| 222 MHz | 220–225 MHz (tikai 2. reģions ITU, Amerika) |
| 432 MHz | 420 – 450 MHz |
| 1296 MHz | 1240 – 1300 MHz |
| 2320 MHz | 2300 – 2450 MHz |
| 10 GHz | 10,5 GHz |

1. Band. Frekvencei jāietilpst vienā no pārvaldītajiem segmentiem; visi pārējie, un jo īpaši visi komerciālie, kas nepārsniedz 49 990,0 kHz, nav parādīti un tiek skaitīti kā “no joslas”.
2. Tā. Viens no pazīstamākajiem komentēšanas veidiem ir FT8, FT4, Q65, JT65, ISCAT, CW, SSB, FM, RTTY, PSK31, MSK144, FSK441, JT6M); USB un LSB kļūst par SSB.
3. Negatīvs filtrs. Vietu izmet, ja tajā ir režīmi MSK144, FSK441, JT6M, JTMS, ISCAT vai tagi <AUE> <MS> <FAI> < F2 >, ar jutīgiem augšējiem gadījumiem un pat atstarpēm iekavās. Tie ir veidi un izplatīšanās, kas nav Tropo: Izkaisīt meteoru, aurora, un sporādiski, F2.
4. Gaisa vads. Ja komentārs satur <AS> vai <ACS> (arī jauktu, piem., <As>) vai deklarē vietu kā aircatter (gaisa izkliedi, gaisa izkliedi, gaisa izkliedējumu), plankums paliek, bet ir iezīmēts dzeltenā krāsā ar punktētu līniju (tumšs viļņojums, lai paliktu lasāms uz skaidras kartes) un izcelts sarakstā.
5. Staciju atrašanās vieta. Ja komentārā ir divi vietrāži (piemēram, JN54 < JN83), tad pirmais ir no tiem, kas veic vietas un otrais stacijas DX; ar vienu vietrāži, tas ir no DX stacijas. Kas trūkst tiek meklēts CALL3. TXT, tikai ar precīzu nosaukumu (klēpjdatoriem skatīt zemāk). Ja stacija paliek bez vietas, vieta sarakstā parādās ar piezīmi “nezināms stāvoklis”, bet ne kartē.
6. Dubulti. Komerciāls ar to pašu DX, vienu un to pašu rādītāju, to pašu joslu un vienu un to pašu avotu jau klāt aizstāj to, nevis pievieno.

CALL3.TXT ir vārdu datubāze → vietrādis, ko izmanto JTDX, formātā CALL,LOCATORE,,. Projekta fails satur 1 048 847 nominācijas, visi ar derīgu vietrādi; faila galvene joprojām runā par 34 323 vārdus, jo tas palika, ka no pirmā izdevuma [7]. Tas ir ielādēts fonā pie palaišanas, tāpēc logs ir uzreiz izmantojams. Fails tiek meklēts blakus programmai, datu mapē, mapē, no kuras programma tika palaista, un virs vienas vai divām mapēm; Ja nav, logs to pastāsta, kad sākat, un statusa josla to signalizē sarkanā krāsā. Tās nosaukumā vienmēr tiek izmantota tās 6 rakstzīme. Noteikums tikai lokatoriem: ja saņemtajam (punkts, MQTT, dekodēšana, QSO desunti) ir mazāk nekā 6 zīmes un CALL3. TXT ir viens līdz 6, tas vienmēr ir vērts CALL3. TXT; klēpjdatoriem (F/ IC8TEM, IC8TEM/P) ir tikai precīza balss: bez, stacija paliek bez saimnieka un nav izstrādāta citur. Noteikumu piemēro arī zīmēšanas laikā, tāpēc dati, kas ieradās pirms datubāzes ielādes, tiks palaisti paši.

Viena datubāze: CALL3. TXT. No 1.3.4 visi vietrāži ir vienā failā, CALL3.TXT, kas paliek JTDX un WSJT-X formātā (CALL, LOCATORE,,,) un to var izmantot tāpat kā WSJT-X, JTDX un MSHV. Vairs nav failu. Faila augšpusē pēc galvenes ir divi bloki, kas atzīmēti ar komentāru rindu: // [LIVETROPOVHF-MANUALI] ar ierakstiem, kas ievietoti ar roku, un // [LIVETROPOVHF-CLUSTER] ar labojumiem no klastera; tad // [LIVETROPOVHF-FONTI] ar visiem pārējiem priekšmetiem alfabētiskā secībā. Katra nominācija parādās vienreiz. Apakšvalodā Dati un avoti › Tikšanās ( CALL3) jūs meklējat nosaukumu, jūs rakstīt saimnieks un nospiediet Pievienot / mainīt: balss iet bloka rokas balsis, un tas ir vērts galvenokārt, pat virs vietrādi, ka stacija pati sūta. ∫ Izņem nomināciju no CALL3.TXT (nākamais atjauninājums atsāk to no avotiem, ja viņiem tas ir). Pievienot failu... nolasiet sarakstu CALL, LOCATOR (jūs arī pieņemat punktu un komatu, tabulu un atstarpi) un rakstiet to starp ar roku sasietām balsīm; izvēlētais fails netiek kopēts. Pārielādēt pārlasi CALL3. TXT no diska, pēc tā rediģēšanas ar redaktoru. Pilna avotu secība: paša lokators, rokas balsis, klastera korekcijas, atlikusī CALL3. TXT (avoti, apgūtie dzīvās atrašanās vietas noteicēji un G7VRD), saņemtais vietrādis (kas attiecas uz datu bāzi tikai tad, ja tam ir 6 rakstzīmes un nebeidzas ar LL vai MM).

CALL3 atjaunināšana. TXT. Tajā pašā apakšvalodā Update poga tagad lejupielādē avotus (CALL3.TXT of WSJT-X Uzlabots, CALL3.TXT EME of W7GJ, grid_data.bin JTDX), pievienojas tiem (pirmais, kam ir nominācija win), pārbauda, vai tie ir vismaz 50 000 vārdi (citkārt nekas nemainās), saglabā CALL3. TXT Iepriekšējā CALL3. TXT ieraksti paliek, un 6 rakstzīmju vietrādi paliek, ja avots ir tikai 4 no tā paša kvadrāta. Rokas balsis un korekcijas no klastera paliek augšā, nemainīga. Ar Atjauninājumu lodziņu katru dienu, čeks notiek sākumā. Programma pārraksta CALL3. TXT tūlīt pēc katras balss ar rokām un, lai iemācītos vietrādi, ne vairāk ik pēc 10 minūtēm un beigās; CALL3. Pakotnes TXT ir tikai lasāms un atribūts pats noņem. Linux uzstādītāji nepārrakstīt CALL3. TXT jau ir klāt; ka Windows instalē tikai tad, ja tā trūkst. ALL3

Automātiskas korekcijas no klastera. DM4X un RBN automātiskajos punktos komentārs rada divus vietrāžus, piemēram, KN09tp <> KN09: pirmais ir tas, ko stacija, kas planē sevi deklarē, līdz 6 rakstzīmēm; otrais ir tas, kas stacijā jūtams, bieži vien tikai līdz 4. Ja pirmais ir 6 rakstzīmes (un nebeidzas ar LL vai MM) un atšķiras no datubāzes, programma labo CALL3. TXT vien: korekcija iet blokā // [LIVETROPOVHF-CLUSTER], ar datumu pēdējā apstiprinājuma trešajā laukā, tas ir spēkā virs avotiem (zem rokas balsis) un parādās starp aizdomīgajiem signāliem atjaunināta kā "CALL3 ". Tātad tas izdzīvo CALL3. TXT atjaunināšana. Neapstiprināta korekcija divus gadus zaudē prioritāti un paliek kā normāla balss.

Pēdējais avots: G7VRD. Ja kandidātam nav 6 rakstzīmju CALL3. TXT, programma lūdz to fonā sabiedrībai API G7VRD (api.g7vrd.co.uk, “lokators ar zvanu”), kas savāc vietrādi automātiski no tīkla datplūsmas. Jūs pieņemat tikai 6 rakstzīmi, kas nav laukuma centrs (LL vai MM); pieprasījumi ir grupēti, maksimāli 40 nominatīvi minūtē, un katrs nominatīvs jautā reizi dienā, jo tas ir hobisks pakalpojums bez garantijām. Atrastais vietrādis ievada CALL3. TXT, ja nosaukuma nav vai tam ir tikai 4 viena kvadrāta rakstzīmes; meklēšanas izslēdzas ar lodziņu zemlingvālā.

Vārdi ar bāru. Jo PSKReporter MQTT tēmām, bārs atdala līmeņus, tad saliktie nosaukumi nāk ar punktu, nevis bārs ( IV3. HB9CAT). Programma liek joslu atpakaļ un, ja ziņojums satur pilnus vārdus, izmantot tos: IV3/HB9CAT paliek IV3/HB9CAT kartē, sarakstā un datubāzē.

Lokatori iemācījās dzīvot. CALL3. TXT nesatur visus: piemēram, SP9EMF un SP9NVY tur nav, un pēc tam paliek 4-zīmju vietrādis, ko sūtījis PSKReporter (karte tā saka). Lai samazinātu šos gadījumus programma raksta CALL3. TXT ik pēc 6 rakstzīmēm, kas nāk no jebkura avota (PSKReporter, piemēram, raidītājs vai kā uztvērējs, klasteris, UDP dekodēšana) par nomināciju, ka CALL3. TXT nav vai ir tikai 4 rakstzīmes vienā laukumā; nekad mainīt balsi uz 6 rakstzīmes. Līdz 1.3.3. šie lokatori bija Data/ LOC6. TXT.

Pāreja no 1.3.3. punkta. Pirmajā sākumā 1.3.4 failus, izņemot iepriekšējās versijas (Dati/CALL3 -MASNUALE.TXT, Data/CALL3 -AGGIUNTE, Data/CALL3 -CLUSTER. TXT, Data/ LOC6.TXT un Data/CALL3 - G7VRD.TXT) tiek apvienoti CALL3. TXT ar tiem pašiem ierakstiem. Releasing rakstot, vecie faili veikt.fuso paplašinājumu, jūs vairs lasīt un jūs varat dzēst.

automātiskie uztvērēji. RBN, dm4x un PSKReporter bieži ziņo vārdus ar sufiksu, norādot uztvērēju: -SDR, /SDR, -# (skimmer RBN) vai -1, -2... (plus vienas un tās pašas stacijas uztvērēji). Saimniekam sufikss tiek noņemts: OK2OZL -SDR ņem OK2OZL JN89UE no CALL3.TXT, ja stacija jau nenosūta 6 rakstzīmju vietrādi. Rādītais nosaukums paliek tas ar sufiksu.

Locatori grieza. Ne visi avoti izārstēt paplašināšanu to saimnieks: daudzi iestatīts uz 4 rakstzīmes, un dažas automātiskās pārneses sistēmas apzināti truncate 4-zīmju vietrādi, pat tiem, kas iegūst datus no reālā klausīšanās FT8, FT4, Q65 un JT65, vai pabeigt to ar centru laukumā (LL vai MM, piem JN89MM). Šā iemesla dēļ 4 rakstzīme vai 6 rakstzīme, kas beidzas ar LL vai MM, tiek aizstāta ar CALL3. Tā paša laukuma TXT. Ja pareizā vieta un laukuma centrs nesakrīt ar dažiem desmitiem km, kļūda paliek neliela, salīdzinot ar tropo attālumiem.

## 6. Kas klausās

Katrs MQTT ziņojums no PSKReporter ziņo nosaukumu un vietu tiem, kas saņem [5], tāpēc jums nav nepieciešams meklēt to CALL3.TXT. Izvēlētajai joslai selektorā programma notur, katrai saņemošajai stacijai: saimnieks, pēdējā pieņemšana, atšifrētie ceļi, staciju filca skaits un maksimālais attālums. Stacija izstājas no saraksta, kad tā vairs nesaņem neko izvēlētajā diapazonā (3, 5, 10, 15, 30 vai 60 minūtes).

Kartē katrs uztvērējs ir tumši rozā tukšs aplis, un no katra sākuma pilnas rozā līnijas pret stacijām, kas ir sajūta un sūta atpakaļ uz tīklu. Līnijas vienmēr ir redzamas, neklikšķinot. Klausīšanās staciju, dzirdēto un katras vietas divas galvas vienmēr tiek rakstītas kartē, bez klikšķa (vienā un tajā pašā punktā viens un tas pats nosaukums tiek rakstīts tikai vienu reizi). Noklikšķinot apli tā līnijas kļūst vēl biezākas un atver cilni ar detaļām. Ja vietrādis no MQTT ir mazāk nekā 6 rakstzīmes, programma izmanto, ka CALL3.TXT (apmēram viens miljons nosaukumu), ja tas ir saskaņā ar saņemto kvadrātu.

Izkaisīt meteoru. Tāpat kā ar klastera punktiem, MQTT attiecības MSK144, FSK441, JT6M, JTMS vai ISCAT tiek atmestas un ieskaitītas tikai; tas pats attiecas uz UDP dekodingiem šādos veidos.

Nav īsti klausītāji. Dažas stacijas paziņo, ka tās saņem 144 MHz, bet patiesībā uztvērējs atrodas citā joslā (nepareizas frekvences norāde): to ziņojumos tiktu veidotas tūkstošiem km garas atveres, kas neeksistē. Programma to pārbauda pati. Katra stacija, kurā klausītājs saka dzirdēt VHF tiek veikta 30 minūtes uz visām joslām, ar īpašu MQTT uzraksts (pskr/filter/v2/+/{nominatīvs}/#) [5]. kalpo kā pierādījums. Ja vismaz divas no klausītāja dzirdētajām stacijām vienās minūtēs (±10) ir aktīvas citās joslās, klausītājs tiek uzskatīts par neesošu un visas tā VHF atskaites tiek atmestas; MQTT reģistrs ziņo par vārdu un stacijām, kas viņu nodeva. Plankumu sarakstā, blakus metriem, MQTT poga parāda plūsmas stāvokli (zaļā lapa, ja tā ir savienota, sarkana, ja nav) un cik klausītāju tika izmesti izvēlētajā joslā un diapazonā; noklikšķinot uz tās atver cilni ar izmetumu sarakstu un katrai no tām citās joslās atrastās stacijas.

Garš savienojums porcelāns. Saite pāri 1000 km (Clumbing Spot, MQTT Klausīšanās, QSO Desunti) nav uzreiz parādīts: paliek tur, un skaitītājs "turēts > 1000 km" saka skaitu. Tas atklājas, kad cits savienojums starp dažādām stacijām un vismaz pusi gar to apstiprina ar vienu no šiem kritērijiem: viena un tā pati stacija pie galvas (piemēram, viens un tas pats klausītājs) un otra stacija tajā pašā virzienā 10° robežās; līdzīgs koridors, ar abām galvām 200 km attālumā no otra savienojuma; paralēls maršruts, ar virzienu 10° un sānu attālumā 200 km attālumā, pārklājās pa maršrutu. Gan parādās, gan MQTT žurnāls parāda iemeslu. Tās UDP dekodēšanas vienmēr ir redzamas un piemērot kā apstiprinājumu citiem. Saglabātie savienojumi paliek atmiņā 60 minūtes.

Nekas zem 144 MHz. Programma darbojas no 144 MHz uz augšu: viss zem 143 MHz tiek atmests. Izņēmums ir GRAVES, Francijas radars, kas pārraida CW 143,050 MHz ar Broye-lès-Pesmes ( JN27SI): komerciāls starp 143,040 un 143,060 MHz vai ar nosaukumu GRAVES, parādās starp 144 MHz ar radaru kā raidītāju. Ad board saka, ja uztvērējs ir iekšā staru (sektori D, C, B, A no 90° līdz 270° no azimuta redzams no radara, 6 soļi no 7,5° katrā) vai ārā; tab GRAVES, tāpat kā YA-MS, parāda debesis redzams no radara: sektori, solis izgaismots tajā laikā (secenza PE1ITR 2021: 0,8 s uz soli, cikls 4,8 s Dati un ģeometrija nāk no YA-MS. Sajūta Trope ir grūti, bet tas var notikt.

Rehabilitācija no cilvēku kopām. Stacija, kas arī parādās citās joslās, netiek uzskatīta par aizdomīgu, ja tajās pašās minūtēs (±30) DX cilvēku kopas ( ON0DXK, GB7RAU; nevis dm4x, RBN vai citi automātiskie avoti) attiecīgajā joslā, vietās, ko veido citi nosaukumi: tas ir gadījums, kad stacijas patiešām strādā uz vairākām joslām kopā. Tādējādi izmests klausītājs atgriežas redzams (MQTT reģistrs raksta "atjaunots") un var atklāt ilgi turētu saiti zemām joslām.

Aktīvās stacijas zem 144 MHz. Garo savienojumu karantīnā stacija, kas tajās pašās minūtēs ir dzirdama joslās zem 144 MHz (no pārējo joslu MQTT ziņojumiem), neapstiprina citus savienojumus un netiek atklāta, lai gan līdzīgs vai paralēls maršruts to apstiprinātu.

## 7. Tirdzniecības vietu saraksts

Tas aizņem 35% pa labi no kartes tab un vietas. Augšā: nosaukums, joslas selektors (50, 70, 144, 222, 432, 1296, 2320 MHz, 10 GHz), diapazona selektors (pēdējais 3, 5, 10, 15, 30, 60 minūtes) un Update taustiņš. Zem četriem metriem: redzami punkti, AS/ACS, izmesti no filtra, stacijas klausās.

Katrā līnijā redzams līnijas tips, laiks UTC, DX → DE, ceļš, attālums km un, otrajā līnijā, avots, frekvence un komentārs. Visnesenākais ir augšā. Viens klikšķis uz līnijas izvēlas vietas: uz kartes līnija aug un abi nosaukumi parādās.

## 8. Spot kartes un troposcatter zudums

Izvēlētajam plankumam cilne rāda vietrāžus, attālumu, azimutu, frekvenci, režīmu, avotu, tagad, komentāru, vidējo tropo indeksu pa ceļu un novērtēt maršruta zudumu izkaisīto tropo.

Attālumu un azimutu aprēķina maksimālajā aplī ar Zemes rādiusu 6371 km (emisenoverso forma attālumam, sākotnējais azimuts no DX stacijas līdz punktētājam).

Zaudējumu Troposcatter. Pamatzaudējumi divām stacijām uz gludas zemes ir brīvās telpas zuduma un izplatīšanas zuduma summa: G3YGF [2]

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

Zaudējumi ir dB, D ir attālums km, f frekvence MHz, θs difūzijas leņķis grādos.

8497 km ir Zemes ekvivalentais rādiuss (4/3 no fiziskā). θA un θB ir atkarīgi no katras stacijas horizonta:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

kur: Δh šķēršļa augstums no horizonta attiecībā uz staciju un tās attālumu. Katrs apvāršņa līmenis izmaksā aptuveni 10 dB [2]. Programma nezina staciju horizontu, tāpēc tas parāda pamata zudumu ar θA = θB = 0 un deklarē to ("smago zemi"). G3YGF pievieno klimatisko apzīmējumu, kur N ir reaģētspēja uz [2] augsni; to izmanto, kad N ir pieejams no laikapstākļu modeļa:

```math
L_{N} = 0,2 \cdot (N - 310)
```

Pārbaude: 110 līdz 144 MHz frekvencē šī formula dod 166,5 dB pret aptuveni 168 dB gultām raksta diagrammā. No 500 km līdz 144 MHz iegūst aptuveni 206 dB. Paredzamā signāla/trokšņa attiecība ir sistēmas jauda (uztvērēja eirps + efektīvā jutība) mīnus zaudējumi: ar G3YGF 144 MHz modeļa sistēmu (198 dB) 500 km ceļš dotu aptuveni −8 dB SSB [2], tas ir, savienojums ir tikai ar labāku tīra izkliedes izplatīšanos.

## 9. Mani dekodings: UDP no WSJT-X, JTDX un MSHV

WSJT-X, JTDX un MSHV var sūtīt UDP katru rindu viņi dekodēt. Programma klausās uz 127.0.0.1:2237 un atkārto to pašu paketi, baitu uz baitu, uz 127.0.1:2238, tāpēc citas programmas, kas izmanto šo portu (GridTracker, JTAlert, malkas cirtējs) turpina strādāt, ja jūs pārvietot tos uz 2238. Adreses un durvju izmaiņas UDP sublingual datu vākšanas; kā klausīšanās adresi jūs varat izmantot arī multicast grupu (piemēram, 224.0.1.), ja dekodēšanas programma ir iestatīta šādi.

Pakotnes seko WSJT-X tīkla protokolam: sākotnējais burvju numurs 0xADBCCBDA, shēmas numurs un ziņojuma tips. Programmā galvenokārt izmantoti trīs [8] veidi:

- Heartbeat: saka, kura programma ir pārraidot datus un ar kādu versiju.
- Statuss: ciparnīcas un režīma biežums; no frekvences jūs saņemat joslu (ar to pašu plankumu tabulu).
- Atkodēt: Tagad, SNR, laiks un frekvences izniekošana, veids un teksts dekodēto ziņojumu.

No teksta jūs saņemsiet staciju dzirdējāt: in CQ 9 A2PQR JN83 ir stacija pēc CQ (ietaupot DX, ES, TEST...), IK0JKL IT9MNO JM68 ir otrā, t.i., kas pārraida. Atrastājs ir ziņojuma, ja ir (4 rakstzīmes, atšķiras no RR73); pretējā gadījumā jūs meklēt CALL3. TXT. Tie ir klausīšanās līnijas: stacijas, ka jūsu uztvērējs dzird tagad. Kartē tie kļūst sarkanas līnijas no jūsu QTH uz staciju, ar nosaukumu; slānis iedegas ar Decoded kastē no manis un prasa derīgu vietrādi. Sarakstā pa labi skaitītājs stāsta, cik daudz stacijas jūs dekodēšanas izvēlētajā joslā.

QSO desunti dal dzīvot. Kad dekodēšana ir noslēguma ziņojums formā C ALL1 C ALL2 RR73 (vai 73 vai RRR), piemēram, IC8TEM IK3FHP RR73 vai IK3FKP SV8PEX 73, abas stacijas ir pabeigušas savienojumu. Programma ieraksta to kā QSO, ar vietrādi katram ņemts no pēdējā ziņojuma, kurā viņš deklarēja to, vai, ja ne, no CALL3.TXT [7], un vērš to uz kartes kā zaļo līniju starp divām stacijām (strate “QSO desunti dal dzīvot”). Stacijas vienu reizi skaita vienā joslā; sarakstā izvēlētā josla un diapazons ir spēkā.

Tajā pašā apakšvalodā Debug rūts parāda katras saņemtās paketes tekstu (veids, laiks, vērtības un ziņojums), pēc pieprasījuma arī heksadecimālie baiti, un līniju pārsūtīšanai. Tas ir, lai pārbaudītu, ka dekodēšanas programma faktiski sūta datus.

## 9 bis. Valodas Hepburn un F5LEN

Katrs rāda, ko programma ņem no avota. Pogu “Lasīt lapu un lejupielādēt visus attēlus” atver norādīto lapu (bāzes dxinfocentre.com/tropo_eur.html Hepburn [3] un tropo.f5len.org/prognoses-for-europe F5LEN [4]) un lejupielādē visus attēlus kešatmiņā blakus izpildāmajam; jūs varat arī ielīmēt viena attēla adresi. Katram attēlam ir lodziņš, kurā to izslēgt, un lauks ar prognozēšanas laiku. 31 Eiropas attēlus F5LEN ir nosaukti laikā modeļa sacīkšu (03.png... 48.png ik pēc 3 stundām, 54... 96 ik pēc 6 stundām, 108... 192 ik pēc 12 stundām), un programma aizņem laiku no nosaukuma [4] failu; Hepburn tas ir vērts secībā lapas. F5LEN cilnē izvēlne izvēlas reģionu: Eiropa (tropo.f5len.org/process-for-europe), Austrālija (tropo.f5len.org/Austrālija, vk03.png... vk192.png) vai Atlantijas okeāns starp Kaboverdi un Īriju (tropo.f5len.org/atlantic-ocean/atlantic-d4-ei, atlantic-png... atlant.png). Ikvienam ir vienādi soļi.

Zemāk ir kalibrēšanas skaitītājs. Viens klikšķis uz attēla pievieno kontroles punktu (lokators vai lat,lon: vismaz 6) vai leģendas krāsu ar tās vērtību. Programma aprēķina pikseļu atbilstību → koordinātas ar otrās pakāpes polinomu interpolāciju mazākajos [19] kvadrātos, nolasa krāsas ar izvēlēto pielaidi un F5LEN pārvērš vienības N 0-10 skalā ar · N + b. “Saglabājiet kalibrēšanu un mēģiniet lasīt” raksta failu Tarature mapē un pārbaudiet, uz kura pikseļa jūsu vietrādis krīt. Pirms kalibrēšanas veikšanas karšu autoram jāpieprasa atļauja.

Reģioni. Katram avotam ir vairāk reģionu, katrs ar saviem fiksēta nosaukuma attēliem: Piedod, piedod. Katra avota cilnē vispirms redzams saplūšanas priekšskatījums: visi lejupielādētie attēli, kas salikti kopā kā galvenajā kartē, ar punktēto rūti un katra reģiona nosaukumu, pelēks plīvurs, kurā nav neviena attēla, kas aptver teritoriju un slīdni, lai ritinātu stundas; pa kreisi reģionu sarakstā, katrs ar lodziņu, lai to izmantotu, lasīto karšu skaits un no kurienes nāk galējības (OCR, ar roku vai rūpnīcu; ja OCR neizdodas lasītais teksts parādās). Manuālais kalibrēšanas stends paliek aiz homonīma pogas. Izvēlnē Bench Reģions izvēlas, kas izskatās, lodziņš “Izmantojiet šo reģionu kartē” ietver to indeksā. Rāmis ir atrodams vienatnē (divas horizontālas līnijas un divas vertikālas līnijas vienā krāsā); lai novietotu attēlu kalpo galējās lat / lons rāmja: Hepburn Europe (10° W–40° E, 27°–56° N) un F5LEN Europe (15° W–40° E, 30°–65° N) jau ir programmā, citiem reģioniem viņi raksta četros O E S N laukos, lasot ass etiķetes. Mēroga reizinātājs padara abus avotus salīdzināmus: galīgā vērtība = reizinātājs × leģenda vērtība. [3] F5LEN [4] F5LEN

## 9 ter. Eksportēt uz vārdu

Abi dokumenti atrodas pogas “Eksportēt uz vārdu” augšpusē. Fails ir rakstīts Office Open XML formātā; formulas kļūst par Word vienādojumiem (OMML), tāpēc tās paliek rediģējamas ar [16] vienādojuma redaktoru. Ja saglabāšanas logs nav pieejams, fails beidzas dokumentu mapē blakus izpildfailam.

## 9 kvateri. Kas mani sauc

Ja ziņojums tiek nosūtīts jums uz vietējo UDP (pirmais vārds ir jūsu, piemēram, IC8FAX IK3FHP JN55 vai IC8FAX IK3FHP -12), programma dziesmas no jūsu QTH uz staciju, kas sauc jums melns punktēta un mirgo līniju, ar nosaukumu un vietrādi blakus jums. Vietrādis ir no CALL3.TXT (ieskaitot rokas balsis) vai ziņojums. Līnija paliek līdz UDP iet slēgšanu QSO ar šo staciju ( RR73, RRR vai 73, vienā no diviem virzieniem), ne vēlāk kā 3 minūtes no pēdējā zvana; jūs izdarīt pār visu un neievēro izslēgšanas kritērijus, tāpēc tiešais zvans nekad izbēg.

PSKReporter, izmantojot MQTT, nepārraida ziņojumu tekstu: saka tikai tas, kurš saņēma, kas, ar frekvenci, veidā un SNR. Tas ir iemesls, kāpēc tiešais zvans tiek atzīta tikai ar savu UDP dekodings.

## 10. Laika kontrole (NTP)

Digitālie režīmi un vietas laiks prasa precīzu pulksteni. Ik pēc 3 minūtēm programma pratina 10 NTP serverus (SNTP uz UDP 123): 0.pool.ntp.org, 1.pool.ntp.org, 2.pool.ntp.org, 3.pool.ntp.org, it.pool.ntp.org, ntp1.inrim.it, ntp2.inrim.it, ptbtime1.ptb.de, time.google.com, time.cloudflare.com [11].

Katrai atbildei aprēķina izmetumu un atgriešanas kavējumu, izmet nenormālās vērtības un iegūst vidējo vērtību, SNTP [10] nodrošinot izmetumu un kavēšanās aprēķinu. Ar vismaz 3 atbildēm vienojas UTC pulkstenis programmas saskaņo un nākamais parādās NTP:OK zaļā; ja atbildes nav pietiekami, parādās NTP:FAILED sarkanā un pulkstenis paliek kā datora. Sistēmas pulkstenis nav pieskāries, jo būtu nepieciešamas administratora atļaujas; Visi iekšējie programmu grafiki (punkti, 5–60 minūšu logi, dekodēšana) izmanto izlīdzinātu laiku. Vadības ierīce vienmēr griežas fonā: interfeisā redzams tikai rezultāts blakus pulkstenim; serveri, intervāli un minimālās atbildes izmaiņas LIVETROPOVHF.ini (ntp_servers, ntp_minutes, ntp_min).

## 10 bis. Vispārīgi izslēgšanas kritēriji

Divas kastes slāņa joslā attiecas uz visu, ko izlozējat (punkts, PSKReporter, dekodēšana, QSO desunti):

- Neietver līdz 100 km. Satiksme starp stacijām, kas ir mazākas par 100 km, var nebūt nozīmīga tropo (zemes vilnis, optiskais diapazons): ar ieslēgto kasti jūs neizvelkat un neparādās sarakstā. Sākas.
- Vairāk nekā 1000 km: tikai kvadrāti. Ap savu QTH (lokatora iestatīts, lai sāktu) ir 1000 km vainags visos virzienos. Ja tikai viena no divām savienojuma stacijām atrodas ārpus šī vainaga, savienojums netiek zīmēts kā līnija: savā vietā tās krāsa ceriņi, dotted 45°, lokatora kvadrātu (4 rakstzīmes) iesaistīto staciju. Tas pats attiecas uz PSKReporter stacijām, kas klausās vairāk nekā 1000 km garumā, kas parādās kā kvadrāts, nevis kā aplis. Līdz šim satiksmes jūs redzat, neaizpildot karti līniju. Bez derīga vietrāža skaita savienojuma garumu. Jūsu QTH kvadrāts nekad krāso ceriņu, pat tad, kad klausāties staciju aiz vainaga: ir norādītais vietrādis, jebkurš nosaukums izmanto, kas darbojas no stacijas. Kroņa rādiuss tiek izvēlēts blakus kastei: 1000, 700 vai 500 km. Kaste Corone 1000/700/500 km zīmē trīs vainagus ap QTH, kas vilkti ceriņās, ar visvairāk atzīmēto izvēli un attālumu rakstīts uz ziemeļiem. Plankumu sarakstā pēc attāluma seko.. Sākas. Kritēriju piemēro pēc karantīnas: joprojām saglabātie garie savienojumi neparādās pat kā kvadrāti.

Kādi ir ceriņu laukumi sedz 45°? Satiksme prom no jūsu QTH interesēs jums saprast, kur pavairošana ir atvērta, bet tās līnijas, simtiem kilometru garumā un bieži desmitiem, aptver karti un slēpt to, kas ir svarīgi, lai jums: atveres jūs varat izmantot no savas stacijas. Šī iemesla dēļ ārpus izvēlētā vainaga programma neizzīmē iesaistīto staciju līnijas, bet iekrāso lokatora laukumus (2° × 1°). Rezultāts ir «aktīvo zonu» karte: kur parādās daudzi tuvumā esošie ceriņu laukumi, ir kanāls vai atvere, pat ja tas tevi neinteresē tieši; Ja laukums paplašinās pret tevi, tas ir signāls, lai rādītu antenu šajā virzienā. 45° paneļa ļauj jums redzēt zem Tropo indeksu un režģi, tāpēc salīdzināt īsumā prognozi (apakšējā krāsā) un reālo satiksmi (illus). Jūsu QTH kvadrāts nekad krāso, jo tas ir sākuma punkts, nevis attāls apgabals. Tikai laukumi, kas visi ir ārpus vainaga ir krāsaini: kvadrāts šķērso vainagu, vai iekšpusē, paliek brīvi, pat ja ir viena no divām stacijām. Lai redzētu līnijas, vienkārši izslēdziet kasti vai izvēlieties lielāku vainagu.

## 11. Tēmas un valodas

Tēmas: 18 kopīgas IC8TEM programmām, tāpat kā YA-NEC un E-LAYER ANALYZER: Dzidrs, nakts, melns, astoņi pasteļi (zils, oranžs, akvamarīns, zaļš, sārts, ceriņi, indigo, brūns), Scia meteorica (YA-MS palete), trīs IP-TRX-CTRL (Chiaro, Notte, Nero) un Blue, Purple, Green. No 1.3.4. punkta Chiaro izskatās kā YA-NEC: gaiši pelēks fons, apmales balti paneļi, zilas nakts cilnes un zils akcents. Tumšās tēmās arī pamatkarte kļūst tumša. Prognozes krāsu skala paliek vienāda visās tēmās. Šauros ekrānos (zem 1500 pikseļiem, piemēram, 1366×768) pulkstenis augšā rāda tikai stundu, tāpēc tēma, tekstu un valodas kursors paliek redzams. Valodas: YA-MS [17] 28. wiki (angļu, Travel, Deutsch, Nederlands, Français, Español, Port-ês, Dansk, Norsk, Svenska, Íslenska, Eesti, Lietuva, Latviešu, Руский,,кра,нська, Türkaı الالالا) Ar arābu, ivrita un Urdu visu logu spoguļiem no labās uz kreiso pusi; karte paliek kā [12]. Tulkojumi attiecas uz saskarni; divi tehniskie dokumenti un ziņas par statusa joslu paliek itāļu valodā. Dienu nosaukumi nāk no starptautiskajiem iestatījumiem. NET izvēlētajai valodai.

Tēma un valoda mainās no divām izvēlnēm augšā pa labi, blakus pulkstenim, un pieteikties nekavējoties, nepārstartējot programmu un nepārtraucot datu vākšanu. Izvēle saglabāta LIVETROPOVHF.ini.

## 11 bis. Aizdomas un pašmācības ziņojumi

Aizdomas Reports tab apkopo visu programmas kontroli atrast dīvaini, grupē pēc tipa un nosaukuma, ar pēdējās 24 stundas uzskaitīti. Programma mācās pati no sevis: katram pārim (tipam, nosaukumam) tiek skaitīti pozitīvie rezultāti (+) un pretēji (-), un pēc 3 saskaņotiem rezultātiem tiek izveidots automātisks noteikums; pretējs rezultāts liek to apšaubīt. Lietotāja lēmumi (Good, Poor) vienmēr pieteikties; pulsante poga atgriež lēmumu programmā. Noteikumi ir iekļauti datos/APPRENDIMENTO. TXT, blakus programmai, tiek aizmirsti automātiskie, kas nav pastiprināti gadu.

| Veids | Kad | Izbraukšana + / | Noteikumu sekas |
| EME ( Q65/JT65) | Q65 vai JT65 1000 km garumā ar Mēnesi virs horizonta uz abām pusēm, vai EME komentārā | – katru reizi, kad Mēness ir izplatīta | Izmest: EME stacija, tās garais Q65/JT65 arī ir izslēgts bez kopēja Mēness. Labi: uzskata par tropo |
| Ilgs savienojums | Vairāk nekā 1000 km, karantīnā | + atklāts, | Labi: stacijas garumi parādās nekavējoties. Izmest: nekad |
| Klausītājs | PSKReporter uztvērējs, kas dzird aktīvās stacijas citās joslās | - izbrāķēti, + cilvēku kopu atjaunoti | Labi: nekad neatmet. Izmest: vienmēr |
| Cits īpašnieks | Square saņēma atšķirīgu no CALL3. TXT | + katru reizi, kad atkārtojat | Labi: stacija ir pārvietots, tas ir vērts laukumā saņēma. Iznīcināt: CALL3. TXT |
| Kvadrāts | 6 zīmes ar LL vai MM |  —  | Tikai informācija |
| Automātisks uztvērējs | Tikšanās ar -SDR, /SDR, -# vai -n |  —  | Tikai informācija |

EME. Q65 un JT65 ir saziņas veidi, izmantojot Luna: Teorētiski tie iekrīt izslēgšanas klauzulā kā izkliedes meteors, bet tos izmanto arī tropo. Programma aprēķina Mēness pozīciju ar zemas precizitātes formulām Almanaks Astronomiskais (ap 0,3°) [20]: ja savienojums Q65 vai JT65 pārsniedz 1000 km un Mēness ir virs horizonta abām pusēm, tas ir gandrīz noteikti EME un ir izslēgts (tas skaitās starp izslēgtajiem). Ja Mēness nav izplatīts, tropē joprojām ir iespējams savienojums un tas iziet cauri garu savienojumu karantīnai, bet signāls paliek redzams. Tas attiecas uz kopām, MQTT un tās UDP dekodingiem.

## 11. ter. Saskares ar izmetumu kritērijiem pa vienam

Programma vēlas parādīt tikai troposfēras pavairošanu 144 MHz un tālāk. Katrs ienākošie dati (Cluster Row, PSKReporter Report via MQTT, UDP dekodēšana) iet caur virkni filtru, vienmēr tādā pašā secībā. Izmestie dati nekur nepazūd: tiek saskaitīti (saskare ar izslēgto augšējā joslā) un, ja iemesls ir apšaubāms, parādās cilnē Apturētie ziņojumi.

|  N.  | Kritēriji | Kur tā ir? | Kas izmests | Vai vari mainīties? |
|  1  | Grupa | grupa, MQTT, UDP | Visi zem 143 MHz (arī 50 un 70 MHz), izņemot GRAVES 143,050 MHz. Kontrolētās joslas ir 144, 222, 432, 1296, 2320 MHz un 10 GHz. | Nē. |
|  2  | Satelīts QO-100 | grupa, MQTT, UDP | 2400,0–2400,5 MHz un 10489,5–10490 MHz: tā ir ģeostacionārā satelītu satiksme, nevis tropo | Nē. |
|  3  | Meteora izkliede | grupa, MQTT, UDP | JT6M un ISCAT (arī ISCAT-A/B), piezīmes ar "&" un netropoxy pavairošanas etiķetēm (<AU>, <AUE>, <ES>, <FAI>, < F2 >) vienmēr. MSK144, FSK441 un JTMS (arī ar <MS>) tikai 250 km attālumā: 250 km attālumā mēs pieņemam, jo šajā attālumā šos režīmus izmanto arī tropo | Nē. |
|  4  | EME (caur Luna) | grupa, MQTT, UDP | komentārā rakstīta EME; Q65 vai JT65 1000 km garumā, kad Mēness atrodas virs horizonta līdz abām galvām; stacijas apgūtas kā EME | Jā: labi noteikumi aizdomīgos signālos |
|  5  | Nepatiess klausītājs | MQ | PSKReporter uztvērējs, kurš saka dzirdēt 144 stacijās, kas tajā laikā darbojas citās joslās (vismaz divās): Viņa attiecības tiek atmestas. Atpakaļ labi, ja DX cilvēku kopas apstiprina tās pašas stacijas | Jā: labi / nabadzīgi |
|  6  | Garo savienojumu karantīna | visas rindas | Vairāk nekā 1000 km posms paliek paslēpts, līdz tas atrod apstiprinājumu: tā pati stacija tajā pašā virzienā, cits maršruts 200 km koridorā, vai paralēls maršruts 10° un 200 km attālumā vismaz uz pusi. Bez apstiprinājuma derīguma termiņš beidzas pēc 60 minūtēm | Jā: labs / izslēgts uz staciju |
|  7  | Mazāk par 100 km | rindas un saraksts | Ar piekļuves kasti, īsākie savienojumi 100 km (zemes vilnis, optiskais diapazons) | Kaste, izslēgts, uzsākot darbu |
|  8  | Ārpus kroņa no QTH | rindas | Ja stacija ir vairāk nekā 1000, 700 vai 500 km (pēc jūsu izvēles) no jūsu QTH, nav līnija: tikai lilla kvadrātu, ko abas stacijas. Jūsu QTH laukums nekad krāsas | Kaste un rādiuss 1000 km |
|  9  | Laika logs | viss | Vecāki plankumi izvēlētajā diapazonā (no 3 līdz 60 minūtēm) | Izvēlne augšā |
|  10  | Dubults | klasteris | Tas pats punkts (tas pats DX, pats rādītājs, tā pati josla, tas pats avots) atkārtoti | Nē. |

Piezīme par 4. un 6. kritēriju. Tropo ir iespējams savienojums Q65 vai JT65, kas pārsniedz 1000 km bez kopējā Mēness: netiek izmests saskaņā ar 4. kritēriju, bet iet uz 6. kritērija karantīnu kā jebkuru citu garu saiti. Tiešie zvani jums (sadaļa 9 quater) ir vienīgais izņēmums 7. un 8. kritērijā: tie vienmēr zīmē.

Kas nav atkritumi. Vietrāža korekcijas ( CALL3. TXT ar roku balsīm, labojumiem no klastera un apgūtajiem lokatoriem, sufiksiem - SDR, laukuma centrs LL/MM) mainās, kur uzzīmē staciju, nevis izmet to. Savukārt staciju bez neviena saimnieka nevar uzzīmēt: paliek sarakstā ar distanci «—».

## 11 kvateri. Krāsas un simboli

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

## 12. Iestatījumi un pārnesamība

Logs. Sākumā logs aizņem 90% no ekrāna lietderīgās vietas (izgriezt aplikāciju joslu), centrēts, tāpēc uz 15-16 collu piezīmjdatora ar 125% vai 150% izmēra maiņu nosaukuma josla paliek redzama. Ja lietderīgā vieta ir zem 1280 × 720 punktiem, loga daļa ir maksimizēta. Izmērs, pozīcija un maksimizēts statuss saglabāts LIVETROPOVHF. Ini pie slēgšanas un beigās nākamajā startā, ja virsraksta josla vēl iekrīt ekrānā (piemēram, pēc ārējā monitora noņemšanas); pretējā gadījumā mēs atgriežamies līdz 90%. Minimālais izmērs ir 900 × 560.

Programma ir pārnēsājama: nekad neraksta AppData mapē vai reģistrā. Viss atrodas izpildāmā mapē: LIVETROPOVHF.ini (komplekti, kas izveidoti pirmajā aizvēršanā), CALL3.TXT (ar roku rakstītas balsis, iemācījušies labojumi un vietrādi), kartes kešatmiņa un laikapstākļu dati, apgūtie noteikumi (Dati/APPRENDIMENTO.TXT) un LIVETROPOVHF.db arhīvs. Jūs varat arī saglabāt to USB stick, ja mape ir rakstāma.

Reklāmas arhīvs (SQLite [14]). Katrs punkts pieņemts un katrs PSKReporter klausīšanās ziņojums tiek saglabāts LIVETROPOVHF.db, SQLite datu bāzē vienā failā. Atmiņā ir palikušas tikai pēdējās 60 minūtes sarakstam un kartei; arhīvs saglabā visu un kalpo vēsturniekam un statistikai (arī UDP dekodings), piemēram, atveres joslai un dienā, maksimālie attālumi, sasniegtie laukumi un aizmugures salīdzinājums starp gaidāmo indeksu un reālajiem plankumiem. No filtra izmestās sloksnes tiek skaitītas, bet ne arhivētas.

## 12 bis. Automātiska uzstādīšana un atjaunināšana

No versijas 1.0 programma jau lejupielādē no Release lapas github.com/ic8tem/LIVETROPOVHF, tajā pašā paketē YA-MS: uzstādītājs Windows (64 bits, 32 bits un ARM, instalēt C:\LIVETROPOVHF bez administratora tiesībām) un portatīvo zip; Windows 7 SP1 un 8.1 paketes atsevišķi (-windows7-x64 un -window.deb,.rpm, Arch, Slackware, AppImage un tar.gz Linux; paketes Aveņu Pi (64 un 32 biti) un Alpu;.dmg un progr.zip Mac (Apple Silicon un Intel). Ar Linux pakotnēm programma tiek kopēta uz pirmo startu ~/LIVETROPOVHF, tāpēc iestatījumi un dati paliek blakus programmai; uz Mac dati ir dzimuši blakus LIVETROPOVHF.app.

Ik pēc 30 minūtēm (pirmā kontrole dažas sekundes pēc starta, nekavējoties, ja pēdējā reizē programma bija slēgta kļūdas dēļ) programma skan viena pati, neprasot neko, pēdējā Atbrīvošana. Ja versija ir jaunāka lejupielādēt portatīvo pakotni, kas piemērota sistēmai un aizvieto programmas failus uz vietas: tie, kas tiek izmantoti tiek pārdēvēti *. vecs-update un dzēsts nākamajā restartēt. Iestatījumi, arhīvs, datu mape un CALL3. TXT nav pieskāries. Jaunā versija ir aktīva no nākamā sākuma: pēc uzstādīšanas pabeigšanas logs piedāvā restartēt nekavējoties; steidzamiem labojumiem, atzīmēti [obligāti] In Release, reboot nevar atlikt. Kontroles statuss un lietošanā esošā versija tiek rakstīta statusa joslas apakšā; lai izslēgtu atjauninājumus=0 LIVETROPOVHF.ini. Uz Mac programma ir parakstīta un nemaina sevi: programma tikai ziņo, ka ir jauna versija.

LIVETROPOVHF attēls ar versijas numuru parādās sākumā; beigās programma lūdz apstiprinājumu un paldies par lietošanu.

## 12 ter. Kļūdas žurnāls un statusa josla

Jebkura neparedzēta kļūda beidzas Data/erri.log, pat ar aprēķinu log off: datums un laiks, versija, sistēma, atvērta cilne, pilnīga kļūda un pēdējās rindas, ko programma darīja. Name Nākamajā startā programma jautā, vai nosūtīt ziņojumu: atver pārlūkprogrammu uz jau kompilēta GitHub signāla, ar noņemtu nomināciju, atrašanās vietu un datora lietotājvārdu, kuru jūs pārbaudīt un nosūtīt ar bezmaksas GitHub kontu. Žurnāls arī beidzas ar katru atjaunināšanas pārbaudi.

Statusa joslā avota bumbiņas (DX klasteris, RBN, PSKReporter, MQTT, UDP) tiek noklikšķinātas un atvestas uz avotu; komikss saka, kāpēc avots ir izslēgts: noraidīts ports, klastera mezgls izvietots servera pusē, nav atbildes, vai UDP klausoties, bet bez paketes no WSJT-X, JTDX vai MSHV. Viens klikšķis uz versijas numuru nekavējoties pārbaudiet atjauninājumus.

## 13. Platformas

Ārējās grāmatnīcas: Avalonia [12] saskarnei, MQTTnet kā MQTT klients reāllaika PSKReporter [13] plūsmai, Microsoft. [14] arhīva dati.Sqlite.

C# uz.NET 10 (LTS) [15] ar Avalonia interfeisu, publicēts kā neatkarīgs izpildfails Windows 64 un 32 bitiem, Mac Intel un Apple Silicon, Linux Debian un Aveņu Pi (64 un 32 biti). Noted Limits: Windows 10/11; macOS 14 vai vēlāk, ar lietotnēm, lai parakstītu vai atvērt pa labi → Open; Aveņu no Pi 2 / Zero 2 W up (ARMv6 modeļi neatbalsta.NET).

## Paldies
Pateicoties I5MZY Massimo Zannoni testēšanai uz Windows 7 un Windows 8.

Hermētiskie līdzekļi: ITU-R P.453, 3. grupas digitālie produkti (© ITU), ko izmanto personīgām un nekomerciālām vajadzībām.

## Galvenie avoti

1. S. Heck LA0BY, “Extreme Tropo Propagation on 144 MHz and up”, prezentācija Ziemeļvalstu VUSHF 41. sanāksmē, Skjeberg 2019. Troposferical Ways, Event Control List (Hepburn/ F5LEN prognoze, augstas temperatūras profili ar Windy, DX klaster kā atvēršanas pierādījums), novērojums, ka tikai prognozes kartes nav pietiekami ekstrēmiem maršrutiem.
2. J. N. Gannaway G3YGF, “Troposfēriskā izkliede”, QST 1983. gada novembris (no eponīmiskā raksta par radiosakariem, RSGB, 1981. gada augusts). Brīvās telpas zuduma un difūzijas formas, ekvivalents rādiuss 8497 km, vietas apzīmējums 10 dB uz horizontu, klimata termins 0,2 · (N – 310), stacijas jauda un signāla/trokšņa attiecība, piemēram, pie 144 MHz.
3. W. R. Hepburn, DX Info Centre, “Troposfēras ducting Prognose” (dxinfocore.com/tropo.html) un “Hepburn Tropo Index (HTI)”. Indeksa skala un vērtības nozīme, prognoze pēc 6 dienām.
4. F5LEN, prognoze Eiropai (tropo.f5len.org/Eiropas prognozes: attēlu saraksts lapas skriptā, klases leģenda Disturb... Attēlā dublikāts) un pasaules prognoze (tropo.f5len.org/WW) un piezīme “Pasaules VHF troposfēras izplatīšanās prognozēšanas rīka jaunā versija”. Indekss balstīts uz reaģētspēju vienībā N, 40 soļi no 3 stundām, atjaunināt 4 reizes dienā.
5. mqt.pskreporter.info, distribūcija MQTT reāllaikā pskreporter.info reklāmas (kursē M0LTE, plūsma P. Gladstone izmanto ar atļauju). Durvis 1883–1886, pskr/filter/v2 tēmu shēma.
6. Projekta kopsavilkums 8/11/2026 un Go tests (core.go, gui.go), ko nodrošina programmas autors. Reklāmas noteikumi, negatīvs filtrs, joslas platuma segmenti, telnet klastera pārvaldība, tēmas, valodas un pārnesamība.
7. CALL3. TXT, nomināciju datubāze → JTDX saimnieks (I. UA3DJY, A. ES1JA skripts, JTAlert/hamspots.net dati). Projekta faila formāts un nosaukumu skaits
8. WSJT-X, pirmkods, NetworkMessage.hpp fails (UDP protokola apraksts: Heartbeat, Statuss, Decode). Vispārīga atsauce nav pievienota; JTDX un MSHV izmanto to pašu formātu.
9. Reverse Beacon Network (reversebeacon.net), telnet mezgla dienesta lapas. 7000 (CW, RTTY) un 7001 ( FT8), pieteikties tikai ar nosaukumu.
10. D. Mills, RFC 4330 „Vienkāršā tīkla laika protokola (SNTP) 4. versija", IETF 2006. Iepakojuma formāts, atkritumu aprēķināšana un atgriešanas aizkavēšana. Vispārīga atsauce, nav pievienota pielikumā.
11. NTP Pool Project (ntppool.org); INRIM, NTP time service (ntp1/ntp2.inrim.it); PTB (ptbtime1.ptb.de). Serveri, ko izmanto laika kontrolei. Vispārīga atsauce, nav pievienota pielikumā.
12. Avalonia UI, oficiālā dokumentācija (docs.avaloniaui.net). Multiplatformu saskarne, tēmas un izkārtojums no labās uz kreiso pusi. Vispārīga atsauce, nav pievienota pielikumā.
13. MQTTnet,.NET bibliotēka MQTT (github.com/dotnet/MQTTnet). Klients izmantots PSKReporter plūsmai. Vispārīga atsauce, nav pievienota pielikumā.
14. SQLite (sqlite.org) un Microsoft. Dati.Sqlite (learn.microsoft.com). Arhivēt vienā failā. Vispārīga atsauce, nav pievienota pielikumā.
15. Microsoft,.NET 10 – atbalstītas operētājsistēmas un autonoma publikācija vienā failā (learn.microsoft.com/dotnet). Peroni un ierobežojumi, kas norādīti 13. iedaļā. Vispārīga atsauce, nav pievienota pielikumā.
16. ECMA-376 “Office Open XML failu formāti”, 1. daļa, 22.1. sadaļa (Office Math Markup Language). Struktūra.docx failu un vienādojumi. Vispārīga atsauce, nav pievienota pielikumā.
17. Programmas autora nodrošināto YA-MS viki 28 valodu saraksts.
18. Open-Meteo, laika prognoze API (open-meteo.com), ar datiem no NOAA GFS modeļa. Izobares spiediens jūras līmenī (skatīt 2. dokumentu).
19. J. P. Snyder, “Kartes projekcijas – Darba rokasgrāmata”, USGS Professional Paper 1395, 1987, un standarta polinomu transformācijas metodes ģeoreferenču attēliem. Vispārīga atsauce, nav pievienota pielikumā.
20. Astronomiskā almanaka sadaļa D, “Zemas precizitātes formulas mēness” (precizitātes pozīcija aptuveni 0,3°). Izmanto, lai atpazītu EME savienojumus. Vispārīga atsauce, nav pievienota pielikumā.
