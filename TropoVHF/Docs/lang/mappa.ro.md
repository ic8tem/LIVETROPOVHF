LIVETROPOVHF · document tehnic 2
# Construcția hărții

> Traducere automată din italiană: în caz de îndoială, este valabil ghidul în italiană.

Pe măsură ce harta tab-ului Harta și spotul se nasc: baza geografică, proiecțiile, media indicilor Hepburn și F5LEN, izobrarul, petele și posturile de ascultare, s-au suprapus cu straturile. Versiunea din 9 noiembrie 2026.

## Pe scurt

- Harta este proiectata de program in opt straturi, fiecare cu caseta [8].
- Baza: Coasta naturala a Pamantului si granitele la doua nivele de detaliu [9]; [10] Market si azimut.
- Fizica: inversiuni, reactivitate N și gradient dN/dh, prag de plumb la −157 N/km [3] [4] [7] [11].
- Indexul gâtului: citirea calibrată a hărților Hepburn [1] și F5LEN [2], grila comună și media celor două surse.
- Isomare: GFS [12] presiune prin Open-Meteo [15], linii fiecare 4 hPa, H și L centre.
- Spot, staţii de ascultare (cercuri roz închis cu linii întotdeauna vizibile roz) şi decodări personale în roşu.
- Citind suprapunerea: presiune mare, conducte înalte, scădere de presiune, creștere SNR, [3] [4] [5] [13] trasee montane [14].

## 1. Principiul: o hartă stratificată

Harta nu este o imagine fixă: este proiectat de program, strat peste strat, ori de câte ori vă modificați zoom, proiecție, bandă, ora de prognoză sau temă. Fiecare strat are cutia sa în bara de strat de sus de pe hartă, astfel încât să puteți ține doar ceea ce ai nevoie și să vedeți totul împreună atunci când doriți imaginea completă. Ordinul, de jos în sus, este aceasta:

|  #  | Drum | Conţinut | Casella |
|  1  | Hartă de bază | Marea, terenurile, coastele și frontierele | Da. |
|  2  | Indice tropo | Media Hepburn + F5LEN sau o singură sursă; transparență reglabilă | Da. |
|  3  | Locator pentru grile | Câmpuri, pătrate și, cu zoom, pătrat | Da. |
|  4  | Isobare | Linii de presiune la fiecare 4 hPa, centre H și L | Da. |
|  4a  | Temperatura 850 hPa | Tratament izotermal la fiecare 4 °C | Da. |
|  4b  | Inversiune termică | Mostră portocalie unde aerul la altitudine este mai cald decât la sol | Da. |
|  5  | PSKReporter | Posturi de primire de PSKReporter (cercuri roz închis) și, în roz, ceea ce aud | Da. |
|  6  | Decodat de mine | Posturi de decodare cu WSJT-X, JTDX sau MSHV: Linii roşii din QTH | Da. |
|  6a  | QSO desunti dal live | Conexiuni inchise ( RR73, 73, RRR) citite in decodare: linie verde intre cele doua statii | Da. |
|  7  | Luminat | Conexiuni: linie albastra completa, galben acoperita cu aerisire, doua bile | Da. |
|  7a  | Excluderi | Casella Exclus la mai puțin de 100 km: cel mai scurt trafic nu este nici tras, nici listat. Casella Peste 1000 km: numai pătrate (începând): în cazul în care una dintre stații este de peste 1000 km de QTH, nici linii sau cercuri, numai pătratele locator ale stațiilor implicate în liliac tratate la 45° | Da. |
|  8  | QTH și etichete | Postul tău, numele locului ales | mereu vizibil |

Indicele tropo este imediat deasupra bazei și sub toate celelalte: este o eșantionare colorată, iar mai mare plasat ar acoperi linii și simboluri. Grila trece peste ea, astfel încât să știi întotdeauna unde o zonă colorată cade. Isobarii sunt linii subtiri si sunt bine cititi in esantionare.

## 2. Harta de bază

Costurile, frontierele și terenurile provin din date vectoriale naturale ale Pământului, din domeniul public [9], la scara 1:50 milioane pentru vedere generală și 1:10 milioane atunci când este amplificat. Fișierele sunt în dosarul programului: nu există servicii de hărți online. Culori ale temei Clear: Light Blue Sea # E2ECF8, Dark Blue Coast, Light Grey Borders. În temele Black and Night culorile bazei sunt întunecate, în celelalte teme pe care le colorează ușor.

## 3. Cele două proiecții

Toate straturile trec prin aceeaşi funcţie de proiecţie, aşa că sunt întotdeauna aliniate una cu cealaltă. Alegeți între două proiecții [10] (λ longitudine, φ latitudine, R raza Pământului):

Piaţa. Păstrați colțuri și forme locale; Distanţele se întind spre poli. Taie la ±85° latitudine.

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

Echidtanti Azimumtale centrat pe pătrat. Centrul (φ0, λ0) este centrul pătratului WWL. Distantele si directiile masurate de centru sunt adevarate: o statie la 1200 km este la 1200 km in scara, iar unghiul cu nordul este azimutul spre care se indrepta antena. Când schimbi locatorul harta se refocalizează.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

În azimutale liniile care pe hârtie sunt drepte (americani, paralele, muchii pătrate) devin curbate. Din acest motiv toate liniile sunt împărţite în trăsături scurte, fiecare secţiune este proiectată şi apoi trăsăturile sunt unite; acelaşi lucru se aplică la marginile indicelui tropo şi la izobare. Punctul de antipozi ale centrului nu are o pozitie definita si ramane la vedere.

## 4. Locator grill, zoom și mutare

Grila Maidenhead este proiectata de program: campuri de 20° × 10° cu doua litere mari si decolorate in centru, patrate de 2° × 1st, si cu suficient zoom subquadrati de 5′ × 2,5′. Liniile sunt albastre şi subţiri, nu acoperă indexul tropo.

Zoom cu roata centrat pe pointer (punctul de mai jos mouse-ul rămâne constantă), în mișcare glisare, dublu clic pentru a reveni la întreaga vedere, + și − chei, și cheia cu procentul pentru întreaga vedere. Detalii se potrivesc zoom: coaste fine, sub-pătrate, etichete izobrare mai groase.

## 5. De ce inversiunea îndoi valurile

În atmosferă joasă temperatura scade în mod normal cu aproximativ 6,5 °C pe km; Indicele de refracţie depinde atât de temperatură, cât şi de umiditatea [3], şi unde se schimbă brusc cu altitudinea valul se îndoaie la sol şi ajunge mai departe decât în mod normal. Cel mai important strat este că până la aproximativ 3 km, în cazul în care se formează inversiune, conducte și celule convective [4]; căile maritime sunt favorizate, deoarece masele de apă formează zone mari de inversiune [4]. [3]

Dimensiunea utilizată pentru măsurarea acesteia este refractivă N, adică (n- 1) · 106 [11]:

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

T este temperatura în Kelvin, P presiunea și presiunea de abur, atât în hPa.

Se numără ca N variat cu cota (dN/dh). În atmosfera standard este de aproximativ −39 N/km; până la −79 N/km refracția este considerată normală, pe lângă superrefracție, și cu gradienți pozitivi de subrefracție [7]. Sub −157 N/km valul este îndoit mai mult decât curbura pământului și rămâne blocat: este conducta [11] (ducting). Acestea sunt condiţiile pe care hărţile Hepburn şi F5LEN încearcă să le prezică.

## 6. Indicele tropo: de la două hărți la media

Filele Hepburn și F5LEN sunt biroul de control: arată imaginile descărcate de pe pagina fiecărei surse, cu timpul de prognoză schimbător, și servesc pentru a calibra citirea făcând clic pe punctele de referință și culorile legendei. Stratul de index Tropo rămâne gol până când cel puțin una dintre cele două calibrări nu este activă și completă.

Hepburn. Indexul Hepburn măsoară cât de multe valuri sunt de așteptat să fie pliate pe o zonă, pe o scară liniară de la 0 la 10, care poate depăși, de asemenea, 10. 0 inseamna atmosfera standard si numai scatter; 1 Hărţile acoperă 6 zile: 3 ore trece pentru primele 36 de ore, apoi 6 ore, actualizate în fiecare zi la 18 UTC [1]; datele de pornire (temperatură, umiditate, presiune) provin din modelul global al serviciului meteo canadian [1]. [1] [1]

F5LEN. Pentru Europa F5LEN publică 31 de hărți de la +3 la +192 ore (la fiecare 3 ore până la +48, la fiecare 6 până la +96, apoi la fiecare 12), cu data și ora valabile în titlu, timpul de producție de la dreapta jos și o legendă de clasă: Disturb, Normal, Minor, Slab, Bun, Puternic, Vy Strong, Duct [2]. Previziunea mondială a F5LEN se calculează prin indicele de refracție aeriană în Unitatea N, pe 40 de pași de 3 ore (aproximativ 5 zile), actualizat de 4 ori pe zi; un click pe hartă arată valoarea medie și localizarea punctului [2] respectiv.

Începem. Pentru fiecare etapă de predicție programul efectuează acești pași:

1. Descărcare. Imaginile celor două prognoze sunt descărcate în cache-ul de lângă executabil, o dată pentru a actualiza sursa, nu pentru a reproiecta. Înainte de versiunea finală, permisiunea ar trebui să fie solicitată către Hepburn și F5LEN.
2. Georeferentare automată. Programul găsește cadrul (două linii orizontale și două verticale de aceeași culoare) și, pe Windows 10/11, citește cu OCR integrat în sistem etichetele osiei (54N, 10W, 0, 5E...). Tăiați benzile din jurul cadrului, măriți-le, testați ambele semne atunci când litera N/S/E/W nu citește și mențineți linia pe care cad cele mai multe etichete: citirile greșite rămân afară. De aici puteti obtine extrema lat / l de cadru și tipul de proiecție (grătar regulat sau Mercator). Extremele se salvează şi se recitesc dacă cadrul îşi schimbă dimensiunea. Pe Mac și Linux, fără OCR, extremitățile fabricii sau cele scrise de mână sunt valabile în filă. Alternativ, pentru hărțile care nu au axe lizibile: Fiecare tip de hartă (Hepburn Europa, F5LEN la nivel mondial...) are o calibrare: unele puncte de control cu coordonate cunoscute (cross-uri de meridiane și paralele, coaste recunoscute) conectează pixeli la latitudine și longitudine, în proiecția utilizată de această sursă. Calibrarea este într-un fișier lângă program și puteți remake în cazul în care o sursă schimbă formatul.
3. Citirea culorilor. Fiecare pixel este comparat cu legenda sursă și transformat în cea mai apropiată valoare numerică. Costurile, scrierile și grilele desenate pe harta originală sunt recunoscute ca nedate și completate cu valorile din apropiere.
4. Reţeaua mondială. Fiecare imagine, odată ce cadrul este găsit şi cunoscut extremele sale, este plasată pe Pământ; valoarea în orice moment al lumii este derivată din imaginile care o acoperă. În cazul în care mai multe regiuni acoperă același punct, merită detalii (mai mulți pixeli per grad), cu condiția ca timpul său să fie mai mic de 1,5 ore de la această alegere; apoi cea mai apropiată oră, apoi cea mai recentă cursă. În cazul în care nici o imagine nu acoperă punctul, harta prezintă un văl gri: nu există date, care sunt diferite de În interiorul cadrului, un pixel fără o culoare legendară este 0 (atmosfera normală); coaste, frontiere și scrieri sunt sărite de privire la cea mai apropiată culoare în termen de 3 pixeli.
5. Aceeaşi scară. Hepburn este deja pe scara 0 F5LEN folosește clase (Distrub, Normal, Minor, Slab, Bun, Puternic, Vy Strong, Duct) [2], dar unele au exact culori Hepburn: Slab = 1, Puternic = 5, Vy Strong = 7, Duct = 8; Minor este 0,5 și bun (verde) 3.5. Multiplicatorul de scară, în fila F5LEN, corectează setul pentru a-l face comparabil cu Hepburn. [1]
6. La aceeaşi oră. Bara de timp trece de la +0 la +192 h la trepte de 3 ore. Pentru fiecare pas faci din fiecare sursa prognoza valabila in cel mai apropiat timp; Daca o sursa nu are acel pas, media il foloseste doar pe celalalt si legenda il indica.
7. Media. Cu butonul Media fiecare punct este în valoare de (Hepburn + F5LEN) / 2; cu Hepburn sau F5LEN vezi doar o singură sursă, util pentru a înțelege în cazul în care cele două prognoze nu sunt de acord.
8. Desen. Valoarea este colorată cu scala legendară (0 albastru, 2 verde, 4 galben, 6 portocaliu, 8 roșu, 10 violet) și proiectată ca o eșantionare sub grilă. 0 este transparent, astfel încât în cazul în care nu se întâmplă nimic vezi harta de bază curată; transparența generală se ajustează cu cursorul lângă cutie.

Pe tab-ul spot, indexul de-a lungul traseului este media valorilor grilei campionate la fiecare 10 km pe cercul maxim dintre cele două stații.

## 7. Isobare

Presiunile stabile mari sunt primul semnal care trebuie căutat pentru tropele [3], iar conexiunile dincolo de Alpii Analizaţi IK3XTV au fost, de asemenea, cu presiune mare [4].

1. Data. Presiunea la nivelul mării din modelul global GFS al NOAA [12], citită prin serviciul gratuit Open-Meteo [15] pe o grilă de 3° × 3° (de la 30° la 72°) N, de la 30° W la 48° E), pentru următoarele 78 de ore. Programul actualizează datele de pornire și apoi la fiecare 6 ore, cum ar fi cursa model.
2. Linii. Isobarul este obţinut din reţea la fiecare 4 hPa (...1012, 1016, 1020...) cu algoritmul pătratelor marş (pătrate marş), apoi sunt uşor rotunjite şi proiectate de tract.
3. Centre. Cele mai marcate maxime și minime locale sunt marcate cu H (albastru) și L (roșu) și valoarea în hPa.
4. Etichete. Valoarea apare de-a lungul fiecărei isobara, pe fundalul colorat al hărții pentru a rămâne lizibilă deasupra eșantionului.

Zona și pas. Datele sunt descărcate pe zona la care vă uitați, extinse cu 25%, cu un pas de la 1 la 20° ales să rămână sub 400 de puncte (Open-Meteo contează fiecare punct ca o cerere: 600 pe minut, 5000 pe oră): prin lărgirea sau mutarea hărții din zonă deja descărcat programul cel mai dens reîncărcat, cel mult la fiecare 10 minute; în cazul în care o actualizare nu reușește datele anterioare rămân și va încerca din nou după 3 minute. În acest fel, isobarul şi temperaturile funcţionează oriunde în lume.

Temperaturi. Aceeași cerere aduce temperatura la 2 m, la 925 hPa și la 850 hPa. LA0BY arată că evenimentul de 22/11/2018 până la UA6 a fost văzut în hărțile de temperatură (900 Pentru aceasta sunt două straturi: [3] DF5AI DK5YA [5]

- Temperatura 850 hPa: izoterme trata fiecare 4 °C (roșu peste zero, albastru de mai jos, violet la zero).
- Inversiune termică: eșantion portocaliu în cazul în care aerul la 925 sau 850 hPa este mai cald decât cel la 2 m; cu cât mai intensă, cu atât mai puternică este inversarea. Este condiția fizică care îndoaie valurile la sol [3] [11], și să-l compare cu indicele tropo și izobare arată dacă previziunile au o bază meteorologice.

## 8. Locuri și stații

Spot. Fiecare loc cu ambele poziții cunoscute este o linie pe cercul maxim dintre cele două stații, împărțite în secțiuni și proiectate; În Mercatore apare apoi ușor curbat, în azimutale este drept dacă începe din centru. Linia albastră plină pentru spotul normal, galbenă acoperită cu margine închisă la culoare pentru airscatter (<AS>, <ACS> sau Locul selectat este mult mai des conceput pentru ultimul și cu cele două nume.

Ascult. Fiecare reporter PSK Staţia de primire [16] este un cerc gol roz închis. De la fiecare pornire linii roz complete spre staţiile care au decodat şi trimis înapoi la reţea, întotdeauna vizibile fără clic, cu numele întotdeauna scrise. Prin selectarea unui cerc liniile sale sunt mai groase. Spot și ascultare arată doar banda și gama alese în listă: schimbarea benzii de bandă harta este goală și reproiectată cu datele deja colectate pentru noua bandă.

M-am decis. Fiecare stație care decodează cu WSJT-X, JTDX sau MSHV (receipt via UDP) este o linie roșie plină de la QTH la stația, cu minge și nominalizare. Este cea mai directă comparație cu prognoza: arată unde receptorul într-adevăr vine chiar acum.

## 8 bis. Culori și simboluri

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

Ce sunt pătratele liliac punctat la 45°. Traficul departe de QTH vă interesează să înțelegeți unde propagarea este deschisă, dar liniile sale, sute de kilometri lungime și adesea zeci, acoperă harta și ascunde ceea ce contează pentru tine: deschiderile pe care le puteți exploata de la stația dumneavoastră. Din acest motiv, dincolo de coroana aleasă, programul nu desenează liniile, ci colorează pătratele locator (2° × 1°) ale stațiilor implicate. Rezultatul este o hartă a zonelor active: în cazul în care apar multe pătrate liliac din apropiere, există o conductă sau o deschidere, chiar dacă nu vă privește direct; Dacă zona se extinde spre tine, este semnalul pentru a indica antena în această direcție. Tabloul de bord de 45° vă permite să vedeți sub indexul tropo și grila, astfel încât să comparați dintr-o privire prognoza (colorul de jos) și traficul real (ilustrul). Pătratul QTH nu colorează niciodată, deoarece este punctul de plecare și nu o zonă îndepărtată. Numai pătratele care sunt dincolo de coroană sunt colorate: un pătrat traversat de coroană, sau interior, rămâne liber chiar dacă există una dintre cele două stații. Pentru a vedea liniile, doar opriți cutia sau alegeți o coroană mai mare.

Criteriile care decid ce vine pe hartă și ce se elimină sunt explicate unul câte unul în documentul 1, secțiunea 11 ter.

## 9. Bara de timp

Mai jos pe hartă: data și ora predicției afișate, cursor de la +0 la +72 h la 6 ore, index legenda 0 Trumpet și indicele de izomare urmați cursorul; pete, stații de ascultare și decodarea sunt observații ale momentului și să rămână întotdeauna cele Atunci când cursorul nu este pe +0, acestea sunt atenuate, să ne amintim că acestea nu aparțin timpului de prognoză.

## 10. Cum să citiți suprapunerea

- Presiune mare + index ridicat + spot de-a lungul zonei colorate: deschiderea este acolo și este cel planificat. Este timpul pentru a apela în această direcție.
- Index mare, dar nici un loc: sau nimeni nu este activ acolo, sau prognoza este prea optimist. Stratul de ascultare ajută la distingerea: dacă există receptoare în zonă și nu simt nimic, a doua ipoteză este mai probabilă.
- Pete lungi în afara zonelor colorate: conducta înaltă, pe care previziunile de suprafață nu o văd, ca în evenimentul LA DF5AI și DK5YA distinge tropo și E sporadic, de asemenea, de oră: deschideri care se nasc și se încheie împreună cu un sporadic E difuz sunt cu greu [5] Tropo. UA6 [3]
- Hepburn şi F5LEN nu sunt de acord: comparaţi cele două surse cu butoanele şi vedeţi unde cad petele reale.
- Presiunea în jos: G4MCU a urmat o deschidere pe farul SK6VHF până la scăderea presiunii; 1012 hPa a considerat evenimentul terminat [13]. Uita-te la izobare prin parcurgerea barei de timp: în cazul în care presiunea mare se retrage, deschiderea probabil urmează.
- Semnale care cresc: în studiul WSPR la 144 MHz din KP4MD raportul semnal/zgomot a crescut chiar în ferestrele în care Hepburn a prezis o îmbunătățire, și a scăzut atunci când prognoza a luat [14]. dacă pe aceeași cale crește, deschiderea se formează.
- Trasee montane: indexul nu ia in considerare reliefurile [1], dar conexiunile dincolo de Alpi exista, de asemenea, datorita celulelor convective si undelor de gravitatie atmosferica [4], si LA0BY a lucrat 9A si S5 prin munti fara Hepburn prezicendu-l [3].

## 11. Punct deschis

- Permiterea Hepburn și F5LEN pentru a utiliza hărțile lor în program.
- Etalonarea coeficienţilor de conversie F5LEN → scara 0
- Calibrarea punctelor de control de orice fel de hartă, care urmează să fie remake în cazul în care sursele schimba grafica.
- Traseu opțional cu temperatură la altitudine (850 hPa) de la GFS.

## Surse principale

1. W. R. Hepburn, Centrul de Informaţii DX, Definiția și scala indicelui, valorile negative și mai mult de 10, excluderea dispersărilor și a reliefurilor, etapele de 3 și 6 ore în 6 zile, actualizarea la 18 UTC. Sursa datelor din modelul canadian global: G. Meisse, AMSAT-BB, 27/08/2007.
2. F5LEN, prognoza pentru Europa (tropo.f5len.org/previziuni-pentru-europeană) și predicția mondială a propagării tropopherice a VHF (tropo.f5len.org/WW) și notată Index unitate N, 40 pași de la 3 ore, 4 actualizări pe zi, valoare și locator pe clic.
3. S. Heck LA0BY, Declinul normal 6,5 °C/km și inversările până la 10 UA6 S5
4. F. Egano IK3XTV, Importanța primilor 3 km, căi marine, celule convective, unde gravitaționale pe Alpi, conexiuni de înaltă presiune, volum comun până la 3
5. V. Grassmann DF5AI, U. Langenohl DK5YA, Profiluri verticale de temperatură și punctul de rouă pentru a recunoaște inversiune, utilizarea indicelui Hepburn, distincția între tropo și E sporadic în funcție de oră.
6. J. N. Gannaway G3YGF, Echivalentul razei Pământului și pierderea traiectoriei utilizate pentru index și cartela spot (a se vedea documentul 1).
7. Cartea DX VHF/UHF, capitolul 2 Figura zonelor de refracție în conformitate cu dN/dh (standard −39 N/km, limite −79 N/km și 0) și profilurile de suprafață și de înaltă conductă. Textul copiei furnizate nu poate fi citit: doar cifrele au fost utilizate.
8. Rezumatul proiectului 8/11/2026, furnizat de autorul programului. Culori hartă, proiecții necesare, straturi, timebar.
9. Natural Earth, date vectoriale de coaste și frontiere, domeniul public (naturale earthdata.com). Referinţă generală, neanexată.
10. J. P. Snyder, Formule de proiecții Mercator și azimutale echidtanti. Referinţă generală, neanexată.
11. Recomandarea UIT-R P.453, indice de refracție radio. Formula refractivă N și pragul conductei la -157 N/km. Referinţă generală, neanexată.
12. NOAA NCEP, Global Prognoze System (GFS). Presiunea la nivelul mării la 0,25°, patru curse pe zi. Referinţă generală, neanexată.
13. G4MCU, articole cu tag-ul "troposferic" (g4mcu.net/tag/tropospheric), 2018 Reformare, conductă și împrăștiere; conducte mai frecvente cu presiune ridicată, în special pe mare; baliza SK6VHF a urmat până la scăderea presiunii la 1012 hPa (iunie 2019).
14. C. F. Milazzo KP4MD, SNR a rapoartelor WSPR la 144 MHz, crescând în ferestrele de îmbunătățire prevăzute de Hepburn și căzând atunci când prognoza dispare.
15. Open-Meteo, prognoza meteo API (open-meteo.com), utilizare non-comercială gratuită. Presiunea la nivelul mării a modelului GFS pe mai multe puncte într-o singură cerere.
16. mqtt.pskreporter.info, distribuție MQTT în timp real a reclamelor pskreporter. informații: numirea și proprietarul beneficiarilor (a se vedea documentul 1).
