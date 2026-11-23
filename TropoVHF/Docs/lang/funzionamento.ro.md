LIVETROPOVHF · document tehnic 1
# Funcționarea programului

> Traducere automată de italiană: în caz de îndoială merită ghidul italian.

Ce face programul, de unde ia datele şi cum le transformă în ceea ce vezi pe hartă şi pe listă. Versiunea din 9 noiembrie 2026, legată de proiectul din C# (.NET 10 + Avalonia).

## Pe scurt

- Scop: prognoză tropo (Hepburn [3], F5LEN [4], izobrare GFS [18]) și vizualizarea reală a benzilor în aceeași fereastră, în conformitate cu lista de control LA0BY [1].
- Colectarea datelor: două grupuri DX, Reverse Beacon Network [9], dm4x cluster și PSKReporter [5] MQTT flux, UDP decodare de la WSJT-X, JTDX și MSHV [8].
- Spot: segmente de banda, filtru negativ, aerisire in galben zdrobit, pozitie de localizatoare sau C ALL3.TXT [6] [7].
- Cine ascultă: cercuri roz închis cu linii roz întotdeauna vizibile spre stații simțit [5].
- Cartela spot: distanta, azimut, index tropo pe calea si pierderea troposcatrului cu metoda G3YGF [2].
- Limbi Hepburn și F5LEN: [3] imagine și banc de calibrare [4].
- Watch: NTP control pe 10 servere la fiecare 3 minute [10] [11].
- Rapoarte suspecte: EME în Q65/JT65 recunoscut cu poziția Lunii, localizatoare trunchiate, skimmere, carantină și ascultători, cu reguli învățate de soare [20].
- Criterii pentru aruncarea înapoi în mare şi semnificaţia culorilor prezentate în două tabele (secţiunile 11 ter şi 11 quater).
- Teme, 36 Limbi [17], documente de export în Word cu formule editabile [16], program portabil și multiplatform [12] [13] [14] [15].

## 1. Scop

Programul pune împreună într-o singură fereastră două lucruri care de obicei se uită pe site-uri diferite: predicția propagării tropoferului ( indexul Tropo al Hepburn și F5LEN, izobrare) și observarea a ceea ce se întâmplă cu adevărat pe benzi (la fața locului de la grupul DX, Reverse Beacon Network și PSKReporter, plus lista de stații care ascultă). Banda de referință este de 144 MHz; reclamele se colectează de la 50 MHz în sus.

Setarea urmează listei de control propuse de LA0BY pentru evenimente Tropo extreme: în zilele anterioare te uiți la prognozele Hepburn și F5LEN și profilurile de temperatură la altitudine, în timp ce evenimentul este în curs de desfășurare veți căuta pe grupul DX și pe hărțile la fața locului dovezile că deschiderea începe [1]. LA0BY observă, de asemenea, că singurele hărți Hepburn/ F5LEN nu sunt suficiente pentru a prezice cele mai lungi conexiuni [1]

## 2. Fereastra și filele

Fereastra este una și nu se împarte în ferestre mobile. De sus până jos:

- Setări generale rând: Call and Locator pe stânga; pe dreapta ceasul UTC (de exemplu 08/11/2026 20:33:38 UTC, text chihlimbar) urmat de NTP:OK sau NTP:AINED, meniul tematic și cel al limbii.
- Numărul de file în cinci grupuri: Hartă și spot; Previziuni (Propria β, prognoza calculată de program și descrisă în Manualul de Prognoză, apoi Hepburn și F5LEN); GRAVES; Date și surse (Cluster DX, PSKReporter, Reverse Beacon, UDP, Numiri și Rapoarte suspecte); Ghiduri (Operare Program, Clădire Hartă, Cum să modeleze Tropo, Previziuni Manuale, Sfaturi către operator, cu o casetă de căutare care contează în care apare textul). Sub grinda un bar arată sub-limbile grupului; fiecare grup își amintește ultima utilizare.
- Conținutul filei și, în partea de jos, bara de stare cu starea surselor, baza de date a numelor și ora ultimei actualizări.

Setările liniei de sus se aplică tuturor filelor: tema aleasă de asemenea colorează colectarea de date și fundalul documentelor; Paginile documentelor rămân albe ca o foaie, astfel încât să imprime bine.

## 3. Apel și proprietar

Proprietarul Maidenhead poate avea 4 sau 6 caractere (de exemplu JN70 sau JN70CN). Programul îl transformă în coordonatele centrului pătratului: câmp de 20° × 10°, pătrat de 2° × 1st, subquadrat de 5′ × 2,5′. Dacă textul nu este un localizator valid, marginea cutiei devine roșie și stația nu apare pe hartă. Locatorul servește, de asemenea, ca centru al proiecției azimut și ca punct de plecare pentru distanțe și azimut în placa de anunțuri.

Apelul este numele folosit ca un cluster de conectare. La primul început, până când nu există o nominalizare validă, clusterele nu se conectează: o fereastră cere numele (și, opțional, proprietarul); confirmat, numele este copiat ca un nume de utilizator în câmpul de autentificare al fiecărui grup care îl are gol și clusterele pleacă. Cu mai târziu rămâne încă, iar butonul Connect al fiecărui grup necesită din nou nominalizarea. MQTT, UDP și NTP nu folosesc login și pleacă oricum.

## 4. Colectarea datelor: surse

Fiecare sursă are propria secțiune în tab-ul de colectare a datelor, cu Connected/Deconectat, casetele recunoscute și conectate la pornire, starea, metrii și registrul rândurilor primite. Liniile nebandate sau aruncate din filtru nu apar în registru: numai contoarele de secțiune. Secţiunile sunt una lângă alta cu două.

| Sursă | Conexiune | Ce aduce |
| 1. Alegerea grupului DX | On0dxk.dyndns.org:8000 Set de comandă/filtru dxbm/pass 6 | Spot de radiouri amatori, SSB și CW. Pe ON0DXK VHF sunt filtrate și sunt activate cu comanda indicată (poi pass 4, 2...). |
| 2. A doua sursă | dxmaps.com:7300 (DXMAPS, EA6VQ) și s50clx.si:41112 ( S50CLX, Slovenia) | DXMAPS colectează pete VHF și oferă, de asemenea, pete de skimmer RBN (set/skimmer control, deja setat); S50CLX este un cluster DX Spider. Autentifică-te cu numele tău; adrese editabile. |
| 3. Rețea de Beacon inversă | telnet.reversebeacon.net 7000 CW/RTTY · 7001 FT8 | Spot de skimmer automat. Autentificare cu numele numai [9]. |
| 4. Cluster cu PSKReporter | dm4x.ddns.net:8500 | Spoturi digitale ale PSKReporter în format grup. |
| 5. PSKReporter MQTT | mqtt.pskreporter.info 1883 · 1884 cu TLS | Fiecare raport de recepţie în timp real: cine transmite, cine ascultă şi locatorii [5]. Bandele 144, 222 și 432 MHz. |
| 6. Hărți meteo și Tropo | dxinfocentre.com/tropo_eur.html tropo.f5len.org/previziuni pentru Europa modelul GFS | Hepburn [3] și F5LEN [4] hărți ale indicilor tropo, presiunea la nivelul mării pentru izomare (GFS prin Open-Meteo [18]). Hepburn și F5LEN trebuie să fie permise autorilor. |

Cluster telnet (surse 1 Dupa conexiune programul asteapta cererea de nume (text care contine login, apel:, apel sau apel); daca nu ajunge in 4 secunde, va rugam sa trimiteti login-ul. Apoi trimite comenzile secţiunii, separate de punct şi virgulă, una la fiecare 0,6 secunde. Personajele de control ale telefonului sunt eliminate înainte de a citi liniile. Dacă conexiunea cade și Riconnetti este activă, încercați din nou după 10 secunde (15 dacă serverul nu răspunde deloc) [6].

PSKReporter MQTT (sursa 5). Programul se înscrie în topic pskr/filtru/v2/{banda}/{modo}/{tx_call}/{rx_call}/{tx_grid}/{rx_grid}/{tx_dxcc}/{rx_dxcc} prin fixarea numai a benzii și lăsând celelalte câmpuri libere cu caracterul + [5]. Numele exact cu care serverul indică benzile (de exemplu 2m, 1,25m, 70cm) trebuie verificat la prima conexiune.

## 5. De la linia de grup la locul

O linie este un loc atunci când are forma DX de SPOTTER: frecvența DX comentariu hhmmZ. Programul derivă spotter, stație DX, frecvența în kHz, comentariu și acum UTC; numele scoate sufixele skimmer (-#, -1...). Apoi se aplică, în această ordine, următoarele reguli [6].

| Bandă | Segment acceptat |
| GRAVES (cu 144) | 143,040 |
| 144 MHz | 144 |
| 222 MHz | 220 |
| 432 MHz | 420 MHz |
| 1296 MHz | 1240 MHz |
| 2320 MHz | 2300 MHz |
| 10 GHz | 10,5 GHz |

1. Band. Frecvenţa trebuie să se situeze într-unul dintre segmentele gestionate; toate celelalte, în special toate reclamele sub 49 990,0 kHz, nu sunt prezentate şi sunt considerate ca fiind "out of band."
2. Ba da. Unul dintre cele mai cunoscute moduri de a comenta este FT8, FT4, Q65, JT65, ISCAT, CW, SSB, FM, RTTY, PSK31, MSK144, FSK441, JT6M); USB și LSB devin SSB.
3. Filtru negativ. Locul este eliminat dacă conține modurile MSK144, FSK441, JT6M, JTMS, ISCAT sau etichetele < AUE > <ES > <MS > < FAI > < F2 >, cu fețe nesensibile și chiar spații între paranteze. Acestea sunt căi și propagări care nu sunt Tropo: Scatter meteorit, aurora, și sporadic, F2.
4. Aerscatter. În cazul în care comentariul conține <AS> sau <ACS> (de asemenea, amestecat, de exemplu <As>) sau declară locul ca airscatter (airscatter, aer scatter, avioane scatter), spotul rămâne, dar este desenat în galben cu linie punctată (dark ondulat pentru a rămâne lizibil pe harta clară) și evidențiat în listă.
5. Locaţia staţiilor. Dacă în comentariu există două locatoare (de exemplu JN54 < JN83) primul este dintre cei care fac la fața locului și al doilea din stația DX; cu un locator, care este de la stația DX. Ce lipseşte este căutat în C ALL3. TXT, numai cu numele exact (pentru laptopuri vezi mai jos). În cazul în care o stație rămâne fără o poziție, la fața locului apare în listă, cu nota de necunoscuta pozitie dar nu pe hartă.
6. Dublu. O reclamă cu același DX, același observator, aceeași bandă și aceeași sursă de una deja prezentă o înlocuiește, în loc să o adauge.

C ALL3.TXT este baza de date a numelui → localizator utilizat de JTDX, în formatul CALL, LOCATORE,. Dosarul proiectului contine 1 048 847 de nume, toate cu localizator valabil; antetul fisierei inca vorbeste de 34 323 de nume deoarece a ramas cel al primei editii [7]. Este încărcat în fundal la pornire, astfel încât fereastra este imediat utilizabil. Fișierul este căutat lângă program, în dosarul Data, în dosarul din care a fost pornit programul și unul sau două dosare de mai sus; Dacă nu există, o fereastră îl spune atunci când porniți și bara de stare îl semnalizează în roșu. Numele lui îşi foloseşte întotdeauna 6 caractere. Numai regula pentru localizatori: daca cel primit (spot, MQTT, decodare, QSO desunti) are mai putin de 6 caractere si C ALL3. TXT are unul până la 6, este întotdeauna în valoare de C ALL3.TXT; pentru laptopuri (F/ IC8TEM, IC8TEM/P) este doar o voce exactă: fără, stația rămâne fără un proprietar și nu este proiectat în altă parte. Regula se aplică, de asemenea, în momentul extragerii, astfel încât datele sosite înainte ca baza de date a fost încărcată va rula ea însăși.

O bază de date: C ALL3.TXT. De la 1.3.4 toate localizatoarele sunt intr-un singur fisier, C ALL3.TX, care ramane in format JTDX si WSJT-X (CALL, LOCATORE,,,,,), si pot fi folosite si in WSJT-X, JTDX si MSHV. Nu mai sunt dosare separate. În partea de sus a fișierului, după antet, există două blocuri marcate de o linie de comentariu: // [LIVETROPOVHF-MANUALI] cu intrările introduse de mână și // [LIVETROPOVHF-CLUSTER] cu corecțiile din grup; apoi // [LIVETROPOVHF-FONTI] cu toate celelalte elemente în ordine alfabetică. Fiecare nominalizare apare o dată. In sublingual Data and Sources › Numiri (C ALL3) cautati un nume, scrieti proprietarul si apasati Adauga/schimba: vocea intra in blocul vocilor cu mana si valoreaza mai presus de toate, chiar deasupra locatorului pe care chiar statia il trimite. Elimină nominalizarea din C ALL3.TXT (următoarea actualizare o reia din surse, dacă o au). Adăugaţi un fişier... citiţi o listă CALL, LOCATOR (de asemenea, acceptaţi punctul şi virgulă, tabulaţie şi spaţiu) şi scrieţi-l printre vocile alese manual; fişierul ales nu este copiat. Reîncarcă recidiva C ALL3.TXT de pe disc, după editarea cu un editor. Ordinea completă a surselor: locator propriu, voci de mână, corecții de grup, restul de C ALL3. TXT (surse, localizatoare live învățate și G7VRD), a primit localizator (care se aplică doar în baza de date dacă are 6 caractere și nu se termină cu LL sau MM).

Actualizarea C ALL3.TXT. În aceeași sub-limbă butonul Actualizare descarcă acum sursele (C ALL3.TXTX de WSJT-X Îmbunătățit, C ALL3.TXT EME de W7GJ, grid_data.bin de JTDX), se alătură acestora (primul care are un câștig nominalizare), verificați dacă acestea sunt cel puțin 50.000 de nume (altfel nu se schimbă nimic), salvează C ALL3. TXT Ultimul C ALL3. Mai rămân intrările TXT, iar un localizator de 6 caractere rămâne acolo unde o sursă are doar 4 din același pătrat. Vocile mâinilor şi corecţiile din grup rămân în vârf, neschimbate. Cu caseta Actualizare pe cont propriu în fiecare zi, cecul are loc la început. Programul rescrie C ALL3. TXT imediat dupa fiecare voce de mana si, pentru localizatorii invatati, cel mult la fiecare 10 minute si la inchidere; C ALL3. TXT a pachetului este doar citit și atributul se decolează. Instalatorii Linux nu suprascrie un C ALL3. TXT este deja prezent; pentru Windows se instalează numai dacă lipsește. ALL3

Corecţii automate din grup. În spoturile automate DM4X și RBN comentariul aduce două locatoare, de exemplu KN09tp<> KN09: prima este ceea ce postul care se declară pete, la 6 caractere; a doua este cea a stației simțit, de multe ori doar la 4. Dacă primul are 6 caractere (și nu se termină cu LL sau MM) și este diferit de cel al bazei de date, programul corectează C ALL3. TXT singur: corecția merge în bloc // [LIVERTOPOVHF-CLUSTER], cu data ultimei confirmări în al treilea câmp, este valabilă deasupra surselor (sub vocile manuale) și apare între semnalele suspecte actualizate ca "C ALL3." Deci supravieţuieşte C ALL3. Actualizări TXT. O corecție neconfirmată timp de doi ani pierde prioritatea și rămâne ca o voce normală.

Ultima sursă: G7VRD. Dacă un candidat nu are 6 caractere în C ALL3. TXT, programul îl cere în fundal către API-ul public al G7VRD (api.g7vrd.co.uk, "locator prin callsign"), care colectează locatori automat din traficul de rețea. Acceptaţi doar un 6 caractere care nu este centrul pieţei (LL sau MM); cererile sunt grupate, la maxim 40 nominative pe minut, şi fiecare nominativ întreabă o dată pe zi, pentru că este un serviciu hobbistic fără garanţii. Localizatorul găsit intră în C ALL3. TXT în cazul în care numele nu este acolo sau are doar 4 caractere din același pătrat; căutarea se oprește cu caseta în sublingual nume.

Numiri cu barul. În subiectele PSKReporter MQTT, bara separă nivelurile, apoi numele compuse vin cu un punct în loc de bara ( IV3. HB9CAT). Programul pune bara înapoi și, în cazul în care mesajul conține numele complete, utilizați cele: IV3/HB9CAT rămâne IV3/HB9CAT pe hartă, listă și baza de date.

Locatorii au învăţat să trăiască. C ALL3. TXT nu conține toate: de exemplu SP9EMF și SP9NVY nu sunt acolo, iar apoi rămâne localizatorul de 4 caractere trimis de PSKReporter (cardul spune așa). Pentru a reduce aceste cazuri programul scrie în C ALL3. TXT fiecare 6 caractere care vine de la orice sursă (PSKReporter, cum ar fi transmițător sau ca receptor, grup, decodare UDP) pentru o nominalizare care C ALL3. TXT nu are sau are doar 4 caractere în același pătrat; nu schimbă niciodată o voce la 6 caractere. Până la 1.3.3 aceste locatoare au fost în Data/ LOC6.TXT.

Trecere de la 1.3.3. La primul început de 1.3.4 fișierele, în afară de versiunile anterioare (Dati/C ALL3 -MANUALE.TXT, Data/C ALL3 -AGGIUNTE, Data/C ALL3 -CLUSTER. TXT, Data/ LOC6.TXT și Data/C ALL3 - G7VRD.TXT) sunt fuzionate în C ALL3. TXT cu aceleași intrări. Releasing scris, fișiere vechi ia extensia.fuso, nu mai citiți și puteți șterge.

Receptoare automate. RBN, dm4x și PSKReporter raportează adesea nume cu un sufix indicând receptorul: -SDR, /SDR, -# (skimmer RBN) sau -1, -2... (plus receptoare ale aceleiași stații). Pentru proprietar sufixul este eliminat: OK2OZL -SDR ia OK2OZL JN89UE de la C ALL3.TXT în cazul în care stația nu trimite deja un localizator de 6 caractere. Numele prezentat rămâne cel cu sufixul.

Localizatoarele se taie. Nu toate sursele tratează extinderea proprietarului lor: multe setați-l la 4 caractere, și unele sisteme automate de transfer-over trunchiează deliberat localizatorul de 4 caractere, chiar și cele care obțin datele de la adevărata ascultare a FT8, FT4, Q65 și JT65, sau completați-l cu centrul pieței (LL sau MM, de exemplu JN89MM). Din acest motiv, un 4 caractere sau 6 caractere care se termină cu LL sau MM se înlocuiește cu C ALL3. TXT din acelaşi pătrat. Dacă locaţia corectă şi centrul pătratului nu coincid cu câteva zeci de km, eroarea rămâne mică în comparaţie cu distanţele tropo.

## 6. Cine ascultă?

Fiecare mesaj MQTT de la PSKReporter raportează numele și localizarea celor care primesc [5], astfel încât nu trebuie să-l caute în C ALL3.TXT. Pentru trupa aleasă în selector programul deține, pentru fiecare stație de primire: proprietar, ultima recepție, moduri decodate, numărul de stații simțite și distanța maximă. O stație părăsește lista atunci când nu mai primește nimic în intervalul ales (3, 5, 10, 15, 30 sau 60 minute).

Pe hartă fiecare receptor este un cerc gol roz închis, și de la fiecare start linii roz complete spre stații care se simte și trimite înapoi la net. Liniile sunt întotdeauna văzute, fără clic. Numirile stațiilor de ascultare, cele auzite și cele două capete ale fiecărui loc sunt întotdeauna scrise pe hartă, fără a trebui să faceți clic (același nume în același punct este scris o singură dată). Făcând clic pe un cerc liniile sale devin și mai groase și deschide fila cu detalii. Dacă localizatorul de la MQTT are mai puțin de 6 caractere, programul utilizează cel de C ALL3.TXT (aproximativ un milion de nume), cu condiția să fie în concordanță cu pătratul primit.

Scatter meteorit. Ca și în cazul clusterelor, rapoartele MQTT în MSK144, FSK441, JT6M, JTMS sau ISCAT sunt eliminate și luate în considerare numai; același lucru este valabil și pentru decodările UDP în aceste moduri.

Nu ascultători autentici. Unele stații declară că primesc în 144 MHz, dar, în realitate, receptorul este pe o altă bandă (indicație greșită a frecvenței): rapoartele lor ar proiecta deschideri inexistente de mii de km. Programul verifică de la sine. Fiecare post pe care un ascultător spune să-l audă în VHF este urmat timp de 30 de minute pe toate benzile, cu o inscripţie MQTT dedicată (pskr/filtru/v2/+/{nominative}/#) [5]. servesc ca dovadă. Dacă cel puţin două dintre staţiile auzite de un ascultător sunt, în aceleaşi minute (±10), active pe alte benzi, ascultătorul nu este considerat autentic şi toate rapoartele sale VHF sunt eliminate; registrul MQTT raportează numele şi posturile care l-au trădat. În lista de pete, lângă metri, butonul MQTT arată starea fluxului (frunza verde dacă este conectată, roșu dacă nu) și câți ascultători au fost eliminați în banda și gama aleasă; făcând clic pe acesta deschide fila cu lista de aruncări înapoi în mare și, pentru fiecare, stații găsite pe alte benzi.

O legătură lungă cu China. O legătură de peste 1000 km (Clumbing Spot, MQTT Listening, QSO Desunti) nu este afișată imediat: rămâne ținută, iar contorul se află la o distanță > 1000 km Este dezvăluită atunci când o altă legătură, între diferite staţii şi cel puţin jumătate de-a lungul acesteia, o confirmă cu unul dintre aceste criterii: aceeaşi staţie la un cap (de exemplu acelaşi ascultător) şi cealaltă staţie în aceeaşi direcţie la 10°; coridor similar, cu ambele capete la 200 km de cealaltă conexiune; rută paralelă, cu direcţie la 10° şi distanţă laterală la 200 km, suprapusă de-a lungul traseului. Ambele apar și jurnalul MQTT arată motivul. Decodarea UDP este întotdeauna vizibilă și se aplică ca confirmare pentru alții. Conexiunile păstrate rămân în memorie timp de 60 de minute.

Nimic sub 144 MHz. Programul funcționează de la 144 MHz în sus: totul sub 143 MHz este aruncat. Excepția este GRAVES, radarul francez care transmite în CW pe 143,050 MHz de către Broye-lès-Pesmes ( JN27SI): o reclamă între 143,040 și 143,060 MHz sau cu GRAVES în nume, apare între 144 MHz cu radar ca transmițător. Placa de publicitate spune că dacă receptorul este în interiorul grinzii (sectoarele D, C, B, A de la 90° la 270° de azimut văzut din radar, 6 pași de 7,5° fiecare) sau în afara; fila GRAVES, ca în YA-MS, arată cerul văzut de pe radar: sectoare, pas iluminat la acel moment (sequenza PE1ITR 2021: 0,8 s pe pas, ciclu 4,8 s Datele și geometria provin de la YA-MS. E dificil să-l simţi în tropă, dar se poate întâmpla.

Reabilitarea din grupuri umane. O stație care apare, de asemenea, pe alte benzi nu se pune ca suspect dacă, în aceleași minute (±30), clusterele umane DX ( ON0DXK, GB7RAU; nu dm4x, RBN sau alte surse automate) deteriorarea activă pe banda în cauză, în pete realizate de alte nume: este cazul stațiilor care lucrează într-adevăr pe mai multe benzi împreună. Astfel, un ascultător abandonat se întoarce vizibil (registrul MQTT scrie

Stații active sub 144 MHz. În carantina conexiunilor lungi, o stație care se aude în aceleași minute pe benzi sub 144 MHz (din rapoartele MQTT ale altor benzi) nu confirmă alte conexiuni și nu este dezvelită, deși o rută similară sau paralelă ar confirma-o.

## 7. Lista reclamelor

Acesta ocupă 35% din dreapta tab-ul harta și la fața locului. Sus: titlu, selector de bandă (50, 70, 144, 222, 432, 1296, 2320 MHz, 10 GHz), selector de gamă (ultima 3, 5, 10, 15, 30, 60 minute) și cheie de actualizare. Mai jos, patru metri: spoturi vizibile, AS/ACS, eliminate din filtru, staţii de ascultare.

Fiecare linie arată tipul de linie, timpul UTC, DX → DE, drumul, distanța în km și, pe a doua linie, sursa, frecvența și comentariul. Cel mai recent este în vârf. Un click pe linie selectează locul: pe hartă linia crește și cele două nume apar.

## 8. Card spot și pierdere Troposcatter

Pentru locul selectat tab-ul arată locatoare, distanță, azimut, frecvență, mod, sursă, acum, comentariu, indicele tropo mediu de-a lungul traseului și o estimare a pierderii traseului pentru Tropo dispersate.

Distanta si azimutul sunt calculate pe cercul maxim cu raza Pamantului de 6371 km (formula emisenoverso pentru distanta, azimut initial de la statia DX la spotter).

Troposcatter pierdere. Pierderea de bază, pentru două stații pe teren neted, este suma pierderii spațiului liber și pierderii difuzării: G3YGF [2]

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

Pierderile sunt în dB, D este distanța în km, f frecvența în MHz, θs unghiul de difuzie în grade.

8497 km este raza echivalentă a Pământului (4/3 din cea fizică). θA și θB depind de orizontul fiecărei stații:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

cu Δh înălțimea obstacolului care face din orizont în ceea ce privește stația și distanța acesteia. Fiecare nivel de orizont costă aproximativ 10 dB [2]. Programul nu cunoaște orizontul stațiilor, așa că prezintă pierderea de bază cu θA = θB = 0 și îl declară ("teren moale"). G3YGF adaugă un termen climatic, în cazul în care N este reactivitatea solului [2]; acesta va fi utilizat atunci când N este disponibil din modelul de vreme:

```math
L_{N} = 0,2 \cdot (N - 310)
```

Verificare: pentru 110 km până la 144 MHz formula oferă 166,5 dB, față de aproximativ 168 dB paturi pe graficul articolului. Pentru 500 km până la 144 MHz, aproximativ 206 dB. Raportul de semnal/zgomot preconizat este capacitatea sistemului (sensibilitatea eirp + efectivă a receptorului) minus pierderea: cu sistemul G3YGF de model 144 MHz (198 dB) o cale de 500 km ar da aproximativ −8 dB în SSB [2], adică conexiunea este doar cu o mai bună propagare a scatterului pur.

## 9. Decodificările mele: UDP de la WSJT-X, JTDX și MSHV

WSJT-X, JTDX şi MSHV pot trimite UDP fiecare linie pe care o decodează. Programul ascultă pe 127.0.0.1:2237 și repetă același pachet, octet per byte, pe 127.0.0.1:2238, astfel încât celelalte programe care au folosit acel port (GridTracker, JTAlert, un logger) continua să funcționeze dacă le mutați pe 2238. Adresele și ușile se schimbă în sublingualul UDP al Colecției de date; ca adresă de ascultare puteți utiliza și un grup multicast (de exemplu, 224.0.1), dacă programul de decodare este setat astfel.

Pachetele urmează protocolul de reţea WSJT-X: numărul magic iniţial 0xADBCCBDA, numărul schema şi tipul de mesaj. Programul utilizează în principal trei tipuri de [8]:

- Heartbeat: spune ce program transmite datele și cu ce versiune.
- Stare: frecventa de apelare si mod; din frecventa obtineti banda (cu acelasi tabel al petelor).
- Decod: acum, SNR, timpul și frecvența deșeuri, modul și textul mesajului decodat.

Din textul obţineţi staţia pe care aţi auzit-o: în CQ 9 A2PQR JN83 este stația după CQ (economisind DX, UE, TEST...), în IK0JKL IT9MNO JM68 este al doilea, adică care transmite. Locatorul este cel al mesajului, dacă există (4 caractere, diferite de RR73); altfel veți căuta în C ALL3. TXT. Sunt linii de ascultare: staţii pe care receptorul le aude acum. Pe hartă devin linii roşii de la QTH la staţie, cu numele; stratul se aprinde cu cutia decodată de la mine şi necesită un locator valid. În lista din dreapta un tejghea spune câte posturi de decodare pe banda aleasă.

QSO desunti dal live. Atunci când o decodare este un mesaj de închidere în forma C ALL1 C ALL2 RR73 (sau 73 sau RRR), de exemplu IC8TEM IK3FHP RR73 sau IK3FKP SV8PEX 73, cele două stații au finalizat un link. Programul îl înregistrează ca QSO, cu localizatorul fiecăruia luat din ultimul mesaj în care l-a declarat sau, dacă nu, de la C ALL3.T [7], și îl desenează pe hartă ca o linie verde între cele două stații (strate O pereche de stații contează o dată pe bandă; banda și gama alese în listă sunt valabile.

În aceeași sub-limbă, panoul de depanare prezintă textul fiecărui pachet primit (tip, timp, valori și mesaj), la cerere, de asemenea, octeți hexazecimali și o linie de transmitere. Pentru a verifica dacă programul de decodare trimite datele.

## 9 bis. Limbi Hepburn și F5LEN

Fiecare arată ce ia programul de la sursă. Butonul "Read the page and download all images" deschide pagina indicată (based on dxinfocentre.com/tropo_eur.html for Hepburn [3] and tropo.f5len.org/precasts-for-europe for F5LEN [4]) și descarcă toate imaginile cache de lângă executabil; puteți lipi și adresa unei imagini. Pentru fiecare imagine există o casetă care o exclude și un câmp cu timpul de prognoză. Cele 31 de imagini europene ale F5LEN sunt numite în momentul cursei model (03.png... 48.png la fiecare 3 ore, 54... 96 la fiecare 6 ore, 108... 192 la fiecare 12 ore) și programul ia timp din numele fișierului [4]; pentru Hepburn merită ordinea paginii. În tab-ul F5LEN, un meniu alege regiunea: Europa (tropo.f5len.org/precasts-for-europe), Australia (tropo.f5len.org/Australia, vk03.png... vk192.png) sau Atlantic între Capul Verde și Irlanda (tropo.f5len.org/atlan-ocean/atlantic-d4-ei, atl03.png... atl192.png). Toată lumea are aceiaşi paşi.

Mai jos este contorul de calibrare. Un click pe imagine adaugă un punct de control (locator sau lat,lon: aveți nevoie de cel puțin 6) sau o culoare legendară cu valoarea sa. Programul calculează potrivirea pixelului → coordonate cu o interpolare polinomială de gradul doi la cele mai mici pătrate [19], citește culorile cu toleranța aleasă și, pentru F5LEN, convertește unitățile N în scara de 0 Save calibration and try reading Înainte de a stabili o calibrare, ar trebui să i se solicite permisiunea autorului hărților.

Regiuni. Fiecare sursă are mai multe regiuni, fiecare cu numele său fix: Îmi pare rău, îmi pare rău. Tabula fiecărei surse arată în primul rând previzualizarea fuziunii: toate imaginile descărcate puse împreună ca pe harta principală, cu panoul punctat și numele fiecărei regiuni, un văl gri unde nici o imagine nu acoperă zona și un slider pentru a derula orele; la stânga lista regiunilor, fiecare cu caseta pentru a o utiliza, numărul de hărți citite și de unde vin extremele (OCR, manual sau fabrică; dacă OCR nu reușește textul pe care îl citește apare). Bancul manual de calibrare rămâne în spatele butonului omonim. Meniul din regiunea Bench alege care aspect, căsuţa "Use this region on the map" îl include în index. Cadrul se găseşte singur (două linii orizontale şi două linii verticale de aceeaşi culoare); pentru a plasa imaginea servi extremele lat/lon ale cadrului: pentru Hepburn Europe (10° W N) sunt deja în program, pentru celelalte regiuni pe care le scriu în cele patru câmpuri OE S N care citesc etichetele axei. Multiplicatorul de scară face cele două surse comparabile: valoarea finală = multiplicator × valoarea legendară. [3] F5LEN [4] F5LEN F5LEN

## 9 ter. Exportă în cuvânt

Cele două documente au pe partea de sus a butonului Fișierul este scris în formatul XML Office Open; formulele devin ecuații Word (OMML), astfel încât rămân editabile cu editorul de ecuații [16]. Dacă fereastra de salvare nu este disponibilă fișierul se termină în dosarul Documente lângă executabil.

## 9 quater. Cine ma cheama

În cazul în care un mesaj este trimis la tine pe UDP locale (primul nume este al tău, de exemplu IC8FAX IK3FHP JN55 sau IC8FAX IK3FHP -12), programul urme de la QTH la stația care te numește o linie punctată și intermitentă negru, cu numele și localizator lângă tine. Localizatorul vine de la C ALL3.TXT (inclusiv voci de mână) sau mesaj. Linia rămâne până când UDP trece de închiderea QSO cu acea stație ( RR73, RRR sau 73, într-una din cele două direcții), cel mult 3 minute de la ultimul apel; trageți peste tot și nu urmați criteriile de excludere, astfel încât un apel direct nu scapă niciodată.

PSKReporter, prin MQTT, nu transmite textul mesajelor: spune doar cine a primit, cu frecvenţă, mod şi SNR. Acesta este motivul pentru care un apel direct este recunoscut doar de propriile decodificări UDP.

## 10. Controlul timpului (NTP)

Modurile digitale și timpul la fața locului necesită un ceas precis. La fiecare 3 minute programul interoghează 10 servere NTP (SNTP on UDP 123): 0.pool.ntp.org, 1.pool.ntp.org, 2.pool.ntp.org, 3.pool.ntp.org, it.pool.ntp.org, ntp1.inrim.it, ntp2.inrim.it, ptbtime1.ptb.de, time.google.com, time.cloudflare.com [11].

Pentru fiecare răspuns calculează aruncarea înapoi în mare și întârzierea returnării, aruncă valorile anormale și ia mediana, calculând aruncarea înapoi în mare și întârzierea oferită de SNTP [10]. Cu cel puțin 3 răspunsuri sunt de acord cu ceasul UTC al programului se aliniază și următorul apare NTP:OK în verde; în cazul în care răspunsurile nu sunt suficiente apar NTP:FAINED în roșu și ceasul rămâne cel al calculatorului. Ceasul sistemului nu este atins, deoarece ar fi necesare permisiuni ale administratorului; toate programele interne (puncte, ferestre de 5 Controlul se întoarce întotdeauna în fundal: în interfața pe care o vedeți doar rezultatul de lângă ceas; servere, intervale și răspunsuri minime se schimbă în LIVETROPOVHF.ini (ntp_servers, ntp_minutes, ntp_min).

## 10 bis. Criterii generale de excludere

Două cutii din bara de strat se aplică la tot ce trageți (spot, PSKReporter, decodare, QSO desunti):

- Se exclude sub 100 km. Traficul între stații mai mici de 100 km poate să nu fie semnificativ pentru tropo (valul de sol, gama optică): cu căsuța pe care nu o desenați și nu apare în lista de liste. Începem.
- Mai mult de 1000 km: numai pătrate. În jurul intervalului QTH (locator setat pentru a începe) există o coroană de 1000 km în toate direcțiile. Daca numai una dintre cele doua statii ale unei legaturi este in afara acestei coroane, conexiunea nu este trasa ca o linie: in locul ei se coloreaza in liliac, punctate la 45°, patratele locator (4 caractere) ale statiilor implicate. Acelaşi lucru este valabil şi pentru staţiile PSKReporter care ascultă peste 1000 km, care apar ca un pătrat în loc de cerc. Până în prezent traficul vedeți fără a umple harta liniilor. Fără un locator valid, lungimea conexiunii este egală cu cea a dispozitivului. Pătratul QTH nu colorează niciodată în liliac, chiar și atunci când ascultați o stație dincolo de coroana: este localizatorul specificat, orice nume folosește care operează de la stație. Raza coroanei este aleasă lângă cutie: 1000, 700 sau 500 km. Cutia Corone 1000/700/500 km atrage cele trei coroane din jurul QTH, trase în liliac, cu cea mai marcată alegere și distanța scrisă spre nord. În lista de puncte distanţa este urmată de.. Începem. Criteriul se aplică după carantină: conexiunile lungi păstrate nu apar nici măcar ca pătrate.

Ce sunt pătratele liliac punctat la 45°. Traficul departe de QTH vă interesează să înțelegeți unde propagarea este deschisă, dar liniile sale, sute de kilometri lungime și adesea zeci, acoperă harta și ascunde ceea ce contează pentru tine: deschiderile pe care le puteți exploata de la stația dumneavoastră. Din acest motiv, dincolo de coroana aleasă, programul nu desenează liniile, ci colorează pătratele locator (2° × 1°) ale stațiilor implicate. Rezultatul este o hartă a zonelor active: în cazul în care apar multe pătrate liliac din apropiere, există o conductă sau o deschidere, chiar dacă nu vă privește direct; Dacă zona se extinde spre tine, este semnalul pentru a indica antena în această direcție. Tabloul de bord de 45° vă permite să vedeți sub indexul tropo și grila, astfel încât să comparați dintr-o privire prognoza (colorul de jos) și traficul real (ilustrul). Pătratul QTH nu colorează niciodată, deoarece este punctul de plecare și nu o zonă îndepărtată. Numai pătratele care sunt dincolo de coroană sunt colorate: un pătrat traversat de coroană, sau interior, rămâne liber chiar dacă există una dintre cele două stații. Pentru a vedea liniile, doar opriți cutia sau alegeți o coroană mai mare.

## 11. Teme și limbi

Teme: cele 18 programe comune IC8TEM, la fel ca YA-NEC și E-LAYER ANALIZER: Clear, Night, Black, opt pasteluri (albastru, portocaliu, aquamarine, verde, roz, liliac, indigo, maro), Scia meteorica (YA-MS paleta), trei de IP-TRX-CTRL (Chiaro, Notte, Nero) și albastru, violet, verde. De la 1.3.4 Chiaro arata ca YA-NEC: fundal gri deschis, panouri albe bordurate, file de noapte albastru și accent albastru. În teme întunecate, harta bazei devine întunecată. Scala de culoare a prognozei rămâne aceeași în toate temele. Pe ecrane înguste (sub 1500 pixeli, de exemplu 1366×768) ceasul din partea de sus arată doar ora, astfel încât tema, cursorul de texte și limba rămân vizibile. Limbi: al 28-lea wiki al YA-MS [17] (English, Travel, Deutsch, Nederlands, Français, Español, Português, Dansk, Norsk, Svenska, Íslenska, Eesti, Lietuvi, Latviešu, Руский, кра,нс Traducerile se referă la interfață; cele două documente tehnice și mesaje ale barei de stare rămân în limba italiană. Numele zilelor provin din setările internaționale de. NET pentru limba aleasă. [12]

Tema și schimbarea limbii din cele două meniuri din dreapta sus, lângă ceas, și se aplică imediat, fără repornirea programului și fără a întrerupe colectarea de date. Alegerea este salvată în LIVETROPOVHF.ini.

## 11 bis. Rapoarte suspectate și auto-învățare

The Suspected Reports tab colectează tot ce controleaza programul găsi ciudat, grupate după tip și nume, cu ultimele 24 de ore enumerate. Programul învață de la sine: pentru fiecare cuplu (tip, nume) numără rezultatele pozitive (+) și contrar (−), iar după 3 rezultate convenite creează o regulă automată; un rezultat opus îl pune în discuție. Se aplică întotdeauna deciziile utilizatorului (Good, Poor); butonul pulsante returnează decizia programului. Regulile sunt în Data/APPRENDIMENTO. TXT, lângă program; cele automate care nu sunt consolidate timp de un an sunt uitate.

| Tip | Când | Ieşire + / − | Efectul regulii |
| EME ( Q65/JT65) | Q65 sau JT65 peste 1000 km cu Luna deasupra orizontului la ambele capete, sau EME în comentariu | - de fiecare dată când Luna este comună | A se arunca: stația EME, Q65/JT65 -urile sale lungi sunt, de asemenea, excluse fără lună comună. Bun: tratat ca un tropo |
| Conexiune lungă | Peste 1000 km, în carantină | + dezvelit, − expirat fără confirmare | Bun: lungimea staţiei arată imediat. A se arunca: niciodată |
| Ascultator | Receptor PSKReporter care aude posturi active pe alte benzi | − aruncat, + reabilitat de grupuri umane | Bine: niciodată aruncat. A se arunca: întotdeauna |
| Proprietar diferit | Piata a primit diferit de cea a C ALL3. TXT | + de fiecare dată când o repeţi | Bine: staţia s-a mutat, merită recepţionat. Eliminaţi: vale C ALL3.TXT |
| Centru pătrat | 6 caractere cu LL sau MM |  —  | Numai informații |
| Receptor automat | Numirea cu -SDR, /SDR, -# sau -n |  —  | Numai informații |

EME. Q65 și JT65 sunt căile de comunicare prin Luna: Teoretic ei cad în clauza de excludere ca meteoritul scatter, dar sunt, de asemenea, folosite în tropo. Programul calculează poziţia Lunii cu formule de precizie joasă a Astronomice Almanac (aproximativ 0,3°) [20]: dacă o conexiune în Q65 sau JT65 depășește 1000 km și Luna este deasupra orizontului pentru ambele capete, este aproape sigur EME și este exclusă (se numără printre persoanele excluse). Dacă Luna nu este comună, conexiunea rămâne posibilă în tropă și trece prin carantina conexiunilor lungi, dar semnalul rămâne vizibil. Se aplică clusterelor, MQTT și decodărilor UDP.

## 11 ter. Contactează criteriile de aruncare înapoi în mare, unul câte unul

Programul vrea să arate doar propagarea tropopherică în 144 MHz și dincolo. Fiecare date primite (Cluster Row, PSKReporter via MQTT, UDP decoding) trece printr-o serie de filtre, întotdeauna în aceeași ordine. Datele aruncate nu dispar în nimic: se numără (contactul celor excluşi în bara superioară) şi, atunci când motivul este îndoielnic, apare în fila Rapoarte suspendate.

|  N.  | Criterii | Unde este? | Ce a aruncat | Te poţi schimba? |
|  1  | Bandă | cluster, MQTT, UDP | Toate sub 143 MHz (de asemenea 50 și 70 MHz), cu excepția GRAVES 143,050 MHz. Benzile deținute sunt 144, 222, 432, 1296, 2320 MHz și 10 GHz. | Nu. |
|  2  | Satelit QO-100 | cluster, MQTT, UDP | 2400,0 MHz: traficul geostaționar prin satelit, nu tropo | Nu. |
|  3  | Split meteor | cluster, MQTT, UDP | JT6M și ISCAT (de asemenea, ISCAT-A/B), comentarii cu "&" și etichete de propagare non-tropoxie (<AU>, <AUE>, <ES>, <FAI>, < F2 >) întotdeauna. MSK144, FSK441 și JTMS (de asemenea, cu <MS>) numai peste 250 km: în termen de 250 km acceptăm, deoarece la această distanță aceste moduri sunt, de asemenea, utilizate în tropo | Nu. |
|  4  | EME (prin Luna) | cluster, MQTT, UDP | EME scris în comentariu; Q65 sau JT65 peste 1000 km atunci când Luna este deasupra orizontului la ambele capete; stații învățate ca EME | Da: O regulă bună în semnalele suspecte |
|  5  | Un ascultător neadevărat | MQ | Receptor PSKReporter care spune să auzi pe 144 de stații active la acel moment pe alte benzi (cel puțin două): Relaţiile lui sunt abandonate. Înapoi bine în cazul în care grupurile umane DX confirma aceleași stații | Da: bine / săraci |
|  6  | Carantină de conexiuni lungi | toate liniile | Peste 1000 km o legătură rămâne ascunsă până când găsește o confirmare: aceeași stație în aceeași direcție, o altă rută pe un coridor de 200 km sau o rută paralelă la 10° și 200 km de-a lungul a cel puțin jumătate. Fără confirmare expiră după 60 de minute. | Da: Bine / Off la stație |
|  7  | Sub 100 km | linii și liste | Cu caseta on-box, cele mai scurte conexiuni de 100 km (valul de sol, raza optică) | Box, oprit la pornire |
|  8  | Dincolo de coroană de QTH | linii | Dacă o stație este de peste 1000, 700 sau 500 km (la alegerea dumneavoastră) de la QTH, nici o linie: numai pătratele Lilla acoperite de cele două stații. QTH pătrat niciodată culoare | Cutie și rază, pe 1000 km |
|  9  | Fereastra temporală | totul | Pete mai vechi ale intervalului ales (de la 3 la 60 de minute) | Meniul de sus |
|  10  | Dublu | cluster | Același loc (același DX, același observator, aceeași bandă, aceeași sursă) repetat | Nu. |

Notă privind criteriile 4 și 6. O conexiune în Q65 sau JT65 de peste 1000 km fără Luna Comună este posibilă în tropo: nu este eliminată prin criteriul 4, ci trece în carantină pentru criteriul 6 ca orice altă legătură lungă. Apelurile directe la tine (secţiunea 9 quater) sunt singura excepţie de la criteriile 7 şi 8: ele întotdeauna desenează.

Ce nu este o pierdere. Corecțiile localizatorului (C ALL3. TXT cu voci de mână, corecturi de la cluster și localizatoare învățate, sufixe - SDR, centru al pieței LL/MM) schimba în cazul în care vă trage o stație, nu aruncați-l. O stație fără nici un proprietar, pe de altă parte, nu poate fi tras: rămâne în lista cu distanța.

## 11 quater. Culori și simboluri

Fiecare culoare are un singur sens, egal în toate temele (schimbare numai fundal).

| Culoare și semn | Semnificație |
| Linia albastră continuă | Spot din grupul DX, RBN sau DM4X între două stații |
| Linia galbenă cu margine închisă la culoare | Spot cu comentariu aerisire (reflecție pe aeronave) |
| Cerc gol roz închis | Post de ascultare pe PSKReporter (MQTT) |
| Liniile roz subţiri din cerc, cu o minge roz la fund | Posturi pe care ascultătorul le-a auzit, cu numele scris întotdeauna; linii mai groase pentru ascultătorul selectat |
| Linia roșie și denumirea roșie | Decodarea UDP (WSJT-X, JTDX, MSHV): Cine eşti? |
| Accident vascular cerebral cu linie roșie | QSO în curs de desfăşurare cu staţia: El te-a sunat și închiderea nu a ajuns încă, sau este DX Call of WSJT-X/JTDX cu transmisia activată. Continuă cu RR73, RRR sau 73 |
| Linia verde | QSO desunti dal live: închidere RR73, RRR sau 73 UDP vizualizare |
| Linia neagră cu frontieră albă | Cineva te sună: durează până la RR73/RRR /73 sau până la 3 minute |
| Pătrate de 45° | Trafic cu cel puțin o stație dincolo de coroana aleasă: Nu sunt linii, doar pătrate locator |
| Căutaţi lile cu distanţă spre nord | 1000, 700 şi 500 km în jurul intervalului QTH; această alegere este mai marcată |
| Cerc și punct în culoarea temei | QTH-ul tău, cu numele şi proprietarul |
| Mingea portocalie cu GRAVES 143,050 | Radarul GRAVES ( JN27SI) |
| Eșantion de la albastru la roșu | Indicele trope de Hepburn (0 F5LEN |
| Catifea gri deschis | Zona fără date Tropo pentru timpul ales |
| Linii gri-albastru cu valoarea în hPa | Izolați fiecare 4 hPa; H în albastru de înaltă presiune, L în roșu de joasă presiune |
| Linii punctate roșu, albastru și violet | Isothermia la 850 hPa la fiecare 4 °C: roșu peste zero, albastru sub zero, violet zero |
| Exemplu portocaliu | Inversare termică: aer la 925 hPa mai cald decât pe sol, mai intens cu inversare mai puternică |

În rapoartele suspecte, marginea fiecărei file spune regula în vigoare: verde Bun, roșu Scarta, gri nici o regulă.

## 12. Configurări și portabilitate

Fereastra. La inceput, fereastra ocupa 90% din spatiul util al ecranului (taie bara de aplicatie), centrat, astfel incat pe un notebook de 15-16 inch cu o redimensionare de 125% sau 150% bara de titlu ramane vizibila. Dacă spațiul util este sub 1280 × 720 puncte, partea de fereastră maximizată. Dimensiunea, poziția și statutul maxim sunt salvate în LIVETROPOVHF. ini la inchiderea si sfarsitul la urmatorul inceput, cu conditia ca bara de titlu sa mai cada intr-un ecran (de exemplu dupa detasarea unui monitor extern); altfel sa va intoarceti la 90%. Dimensiunea minimă este 900 × 560.

Programul este portabil: nu scrie niciodată în dosarul AppData sau în registru. Totul este în dosarul executabilului: LIVETROPOVHF.ini (Setări, create la prima închidere), C ALL3.TXT (cu voci scrise de mână, corecturi învățate și locatoare), cache hartă și date meteorologice, reguli învățate (Dati/APPRENDIMENTO.TXT) și arhiva LIVETROPOVHF.db. Puteți păstra, de asemenea, pe un stick USB, atâta timp cât dosarul este scris.

Arhiva Ad (SQLite [14]). Fiecare loc acceptat și fiecare raport de ascultare PSKReporter este salvat în LIVETROPOVHF.db, o bază de date SQLite într-un singur fișier. În memorie rămân doar ultimele 60 de minute pentru listă și hartă; arhiva păstrează totul și servește istoricului și statisticilor (de asemenea, decodificările UDP), de exemplu deschiderile pentru bandă și pe zi, distanțele maxime, pătratele atinse și compararea din spate între indexul așteptat și punctele reale. Benzile aruncate din filtru sunt numărate, dar nu arhivează.

## 12 bis. Instalare și actualizări automate

Din versiunea 1.0 programul deja descarcă gata din pagina de lansare a github.com/ic8tem/LIVETROPOVHF, în aceleași pachete de YA-MS: instalator pentru Windows (64 biți, 32 biți și ARM, instalați în C:\LIVETROPOVHF fără drepturi de administrator) și zip portabil; pentru Windows 7 SP1 și 8.1 pachete separate (-windows7-x64 și -window.deb,.rpm, Arch, Slackware, AppImage și tar.gz pentru Linux; pachete pentru Raspberry Pi (64 și 32 biți) și Alpine;.dmg și apps.zip pentru Mac (Apple Silicon și Intel). Cu pachetele Linux, programul este copiat la primul început în ~ / LIVETROPOVHF, astfel încât setările și datele rămân lângă program; pe datele Mac se naște lângă LIVETROPOVHF.app.

La fiecare 30 de minute (prima verificare la câteva secunde după începerea programului, imediat dacă ultima dată programul s-a închis pentru o greșeală), programul citește singur, fără să ceară nimic, ultima lansare. Dacă versiunea este mai nouă descărcați pachetul portabil potrivit pentru sistem și înlocuiește fișierele programului la fața locului: cele utilizate sunt redenumite *.old-update și șterse la următoarea repornire. Configurări, arhivă, dosar de date și C ALL3. TXT nu sunt atinse. Noua versiune este activa de la urmatorul inceput: la instalare s-a terminat o fereastra propune repornirea imediata; pentru corecturi urgente, marcate [obligator] in Release, repornirea nu poate fi amânata. Starea de control și versiunea în uz sunt scrise în partea de jos a barei de stare; pentru a opri actualizările=0 în LIVETROPOVHF.ini. Pe Mac aplicația este semnată și nu se schimbă singură: programul raportează doar că există o nouă versiune.

Imaginea LIVETROPOVHF cu numărul versiunii apare la început; la sfârşit programul cere confirmare şi mulţumesc pentru utilizare.

## 12 ter. Jurnal de eroare și bară de stare

Orice eroare neprevăzută se termină în Data/erori.log, chiar și cu jurnalul de calcul oprit: data și ora, versiunea, sistemul, fila deschisă, eroare completă și ultimele linii ale ceea ce programul făcea. O eroare de interfață nu mai închide programul: în bara de stare apare " anno eroare adnotată" (click: deschide registrul). La următoarea pornire programul cere dacă să trimită raportul: deschide browser-ul pe un semnal GitHub deja compilat, cu nominalizare, locator și numele de utilizator al PC eliminat, pe care le verificați și trimiteți cu un cont GitHub gratuit. Jurnalul se termină de asemenea cu fiecare verificare de actualizare.

În bara de stare bilele sursă (DX cluster, RBN, PSKReporter, MQTT, UDP) sunt click și aduse la sursă; comic spune de ce o sursă este oprit: port respins, nod de grup staționate pe partea serverului, nici un răspuns, sau UDP ascultare, dar fără pachete de WSJT-X, JTDX sau MSHV. Un singur click pe numărul versiunii verifică imediat actualizările.

## 13. Platforme

Registre externe: Avalonia pentru interfața [12], MQTTnet ca un client MQTT pentru fluxul în timp real de PSKReporter [13], Microsoft. Data.Sqlite pentru arhiva [14].

C# on.NET 10 (LTS) [15] with Avalonia interface, published as an independent executable for Windows 64 and 32 bits, Mac Intel and Apple Silicon, Linux Debian and Raspberry Pi (64 and 32 bits). Limite notate: Windows 10/11; MacOS 14 sau mai târziu, cu aplicații pentru a semna sau pentru a deschide dreapta → Deschis; Raspberry de la Pi 2 / Zero 2 W Up (modelele ARMv6 nu sunt susținute de.NET).

## Mulţumesc.
Datorită I5MZY Massimo Zannoni pentru testarea pe Windows 7 și Windows 8.

Blindate: UIT-R P.453, produse digitale ale Grupului de studiu 3 (© ITU), utilizate în scopuri personale și necomerciale.

## Surse principale

1. S. Heck LA0BY, Troposferical Ways, Event Control List (Hepburn/ F5LEN prognoza, profile de temperatură înaltă cu Windy, DX cluster ca test de deschidere), observaţie că doar hărţile prognozate nu sunt suficiente pentru trasee extreme.
2. J. N. Gannaway G3YGF, Noiembrie 1983 (din articolul eponim privind comunicarea radio, RSGB, august 1981). Formule de pierdere si difuzie spatiu liber, raza echivalenta 8497 km, termen site-ul de 10 dB pe orizont, termen climatic 0,2 · (N- 310), capacitatea instalației și raportul semnal/zgomot, de exemplu la 144 MHz.
3. W. R. Hepburn, DX Info Centre, Scala indexului şi semnificaţia valorii, predicţie în 6 zile.
4. F5LEN, prognoza pentru Europa (tropo.f5len.org/precasts-for-European: lista imaginilor din scenariul paginii, legenda clasei Disturb... Duct în imagine) și prognoza mondială (tropo.f5len.org/WW) și nota Index bazat pe reactivitate în unitatea N, 40 paşi de la 3 ore, actualizare de 4 ori pe zi.
5. mqtt.pskreporter.info, distribuţie MQTT în timp real a reclamelor de pskreporter.info (curat de M0LTE, fluxul de P. Gladstone utilizate cu permisiunea). Usi 1883
6. Rezumat proiect de 8/11/2026 și Go test (core.go, gui.go) furnizate de autorul programului. Ad Reguli, filtru negativ, segmente de lățime de bandă, gestionarea clusterului telnet, teme, limbi și portabilitate.
7. C ALL3.TXT, baza de date de nominalizare → JTDX proprietar (I. UA3DJY, A. ES1JA script, JTAlert/hamspots.net date). Formatul fișierului și numărul de nume verificate în fișierul proiectului.
8. WSJT-X, cod sursă, fișier NetworkMessage.hpp (descrierea protocolului UDP: Heartbeat, Status, Decode). Trimitere generală, neanexă; JTDX și MSHV utilizează același format.
9. Reverse Beacon Network (reversebeacon.net), paginile de serviciu ale nodului telnet. Porturi 7000 (CW, RTTY) și 7001 ( FT8), login numai cu numele.
10. D. Mills, RFC 4330 Formatul pachetului, calculul deșeurilor și întârzierea returnării. Referinţă generală, neanexată.
11. NTP Pool Project (ntpool.org); INRIM, NTP time service (ntp1/ntp2.inrim.it); PTB (ptbtime1.ptb.de). Servere utilizate pentru controlul timpului. Referinţă generală, neanexată.
12. Avalonia UI, documentație oficială (docs.avaloniaui.net). Interfață multiplatform, teme și dispunere de la dreapta la stânga. Referinţă generală, neanexată.
13. MQTTnet,.NET library for MQTT (github.com/dotnet/MQTTnet). Client utilizat pentru fluxul PSKReporter. Referinţă generală, neanexată.
14. SQLite (sqlite.org) și Microsoft. Data.Sqlite (learn.microsoft.com). Archive într-un singur fișier. Referinţă generală, neanexată.
15. Microsoft,.NET 10 Platforme și limite indicate în secțiunea 13. Referinţă generală, neanexată.
16. ECMA-376 Structura fișierului și ecuațiilor.docx. Referinţă generală, neanexată.
17. Lista celor 28 de limbi ale YA-MS wiki, furnizată de autorul programului.
18. Open-Meteo, prognoza meteo API (open-meteo.com), cu date din modelul NOAA GFS. Presiunea la nivelul mării pentru izobare (a se vedea documentul 2).
19. J. P. Snyder, Referinţă generală, neanexată.
20. Almanahul astronomic, secţiunea D, Folosit pentru a recunoaște conexiuni EME. Referinţă generală, neanexată.
