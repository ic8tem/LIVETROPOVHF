# Manual de previziune

> Traducere automată din italiană: în caz de îndoială, este valabil ghidul în italiană.

Fila de prognoză calculează propagarea tropopherică în sine: o hartă index pe zona vizibilă și semnalul furnizat între stația dumneavoastră și una corespunzătoare. Acest manual descrie fiecare date, modul în care sunt calculate și de unde provine. Numerele în paranteze pătrate se referă la sursele din partea de jos; în program fiecare date ale rezultatelor are un comic (simbolo i) cu aceeași sursă și trimiterea la alineatul.

## 1. Ce face şi ce face

Previzionarea profilelor verticale de temperatură și umiditate ale modelului GFS [1, 2] derivă din reactivitatea stratului de aer pe strat și estimează cât de mult câștigă semnalul în comparație cu o atmosferă normală. Pe traseu se adaugă sol real (Copernicus GLO-30 [3]), difracție, Troposcatter, conducte și zgomot receptor.

Nu este o măsură: este o estimare făcută pe un model meteo cu niveluri îndepărtate sute de metri. Acesta servește la compararea situațiilor (mai bine decât ieri, 70 cm mai bine de 2 m, acest tratament mai bine decât atât), pentru a nu garanta o conexiune. Punctul 14 explică modul în care se colectează probe pentru validarea acesteia.


> "Producţia nu poate fi generalizată, ci studiată în raport cu morfologia locală.
> Volker Grassmann, DF5AI, raportat de Flavio Egano, IK3XTV (Propagare pe 2 metri)

Este principiul care ghidează calculul punct-punct: fiecare rută este calculată pe sol, cu orizonturile sale, marea și reliefurile sale; harta oferă imaginea generală, tratează răspunsul.

## 2. Comenzi lingvistice

- Bandă: 144, 432 sau 1296 MHz. Harta și calculul utilizează banda aleasă.
- Acum: cursorul mută prognoza de acum până la 72 de ore înainte, în 3 ore.
- Prognoza actualizată (zona vizibilă): descarcă profilele GFS pe o grilă de până la 240 de puncte care acoperă harta vizibilă. Smoala grilei (0,5° sau mai mult) depinde de mărire.
- Stop: întrerupe o descărcare sau calcul.
- Detalii de arătat (panoul din dreapta al hărții): trei nivele, esențiale, normale și complete, porniți un grup de cutii; fiecare cutie poate apoi schimba manual. Cutiile sunt în trei grupe:
  - Pe hartă: izoline del Gain, de asemenea, 3 Culorile rămân mereu.
  - În calculul semnalului: toate liniile de calcul, profilul plat al Pământului, jurnalul de calcul și jurnalul de fișiere detaliate.
  - Corecții și date live: corecții meteorologice (Misme, subsidence), contrast aerian-mare, medie cu pete (corridoi), APRS 2 m salturi și LoRa, dincolo de munte fără pierderi (punctele 4 bis, 9 bis, 9 quater și 10 bis).
  - Essential deține numai izoline, corecturi meteo și media cu pete; normal adaugă izobare, inversiune, semne meteorologice, vedere directă, pământ plat, aer-mare și APRS; complet-l activați. Nivelul schimbă doar ceea ce vezi și ce corecturi introduc: calculele rămân aceleași.
- Sursa de date și caz istoric: de unde apar profilurile și în ce zi vă uitați (punctele 3 bis și 3 ter).
- Stația mea și corespondentul: Locator, antene, putere, cabluri, zgomot (punctele 7 și 12).
- Calculați semnalul și acoperirea în optica: cele două calcule de la sol (punctul 8
- Registrul de calcul: fiecare etapă cu numerele utilizate, inclusiv în fișierul Data/ prognoza-calcoli.log.

## 3. De la datele GFS la profil

Pentru fiecare punct al rețelei și pentru fiecare oră citim presiunea la sol, temperatura și umiditatea la 2 m, vântul la 10 m, înălțimea stratului limită, CAPE și temperatura și umiditatea în raport cu 1000, 975, 950, 925, 900, 850, 800, 700, 600 și 500 hPa. De la 1.3.0 puteți citi și vântul la 850 hPa și viteza verticală la 850 și 700 hPa (punctul 9 bis).

Nivelurile sunt calculate cu formula ipsometrică și temperatura virtuală; nivelurile sub sol sunt limitate. Pentru fiecare nivel:

- N = 77,6/T · (P + 4810·e/T), cu T în kelvin, P și hPa și cu presiunea de saturare a UIT-R P.453-14, eq. 9, cu factorul de corecție EF (controlat în textul oficial) [4].
- M = N + 157·h, cu h în km: reactivitatea modificată. În cazul în care M merge acolo este o conductă.

## 3 bis. Sursa datelor

Meniul Data Source alege de unde provin profilele:

- Automat (recomandat): GFS mai întâi de la NOMADS [14], serviciul NOAA care distribuie modelul: pentru fiecare moment programul cere două decupaje ale zonei în format G RIB2 și le citește de la sine; NOMADS nu are limită zilnică, doar unul pe minut că programul respectă. Cu grile largi (de la 0,5° la mai sus) foloseste 0.5° sau 1st fisiere la fiecare 3 ore, mult mai usor. Punctele pe care NOMADS nu le oferă sau toate dacă NOMADS nu răspunde, întrebați Open-Meteo [2]. Cele două surse oferă același model și aceeași cursă, astfel încât să puteți amesteca în aceeași grilă.
- GFS de la Open-Meteo: numai Open-Meteo, ca în 1.2 (la limita pe minut este de așteptat).
- GFS de la NOAA NOMADS: numai nominalizări. Mai lent de descărcat (două fișiere pe oră, jumătate de secundă între o cerere și cealaltă pentru a nu depăși limita NOAA), dar nu consumă limita Open-Meteo.
- ICON-UE (DWD) și ECMWF IFS 0,25°, de la Open-Meteo: alte modele decât GFS, care se aplică în întreaga rețea și pentru rute. Acestea nu se amestecă cu GFS: compararea a două modele în același timp este o modalitate simplă de a vedea cum prognoza este incertă. ICON-UE acoperă doar Europa; ECMWF nu are toate nivelurile de presiune GFS (sar nivelurile lipsă) și este posibil să nu aibă nivelul limită.

Sursa folosită este scrisă în linia de stare și în prima linie a rezultatelor (Data source), cu câte puncte au venit de la fiecare serviciu și câte din cache. Cache-ul discului este separat prin model.

## 3 ter. Cazuri istorice

Istoricul Meniul de caz analizează o deschidere documentată a arhivei Open-Meteo [15] Prognoze GFS: harta se mută în zona cazului, banda se stabilește pe sine și cursorul de timp pleacă din prima zi a cazului. Apăsând Prognoza Actualizării descărcați profilurile acelor zile; calculul semnalului funcționează ca pentru prognoza curentă. Serveşte la compararea modelului cu ceea ce s-a întâmplat cu adevărat.

Primul caz este cel din 14 și 15 august 2022, anunțat la QST (noiembrie 2022, "Lumea de peste 50 MHz" [23]): o deschidere sub o presiune ridicată între Manitoba și Kansas/Missouri, de la 144 la 1296 MHz aproape toate în FT8, cu recordul canadian la 10 GHz de 1228,56 km în Q65 și semnale până la 1941 km. Cea de-a doua este deschiderea între 29 decembrie 2021 și 2 ianuarie 2022 privind tirenul și vestul Mediteranei la 144 MHz, din rapoartele IC8TEM: primele deschideri la 29 (până la 737 km), mai larg la 31 ( IT9GSF Pentru cazurile mai vechi (de exemplu septembrie 2000), arhiva Open-Meteo nu are date. În arhivă nu există o înălţime limită a stratului: nu se aplică corecţia acestuia. EB5EA

## 4. Indexul și culorile

Pentru fiecare strat între două niveluri, programul compară picurarea N cu pragurile din tabelul 6.5 de Bean și Dutton [5]: de exemplu între 1000 și 850 hPa o scădere de până la 60 N/km este normală, peste 100 N/km este super-refracție. Un strat superrefractiv este în valoare de 10-15 dB de câștig; o conductă care blochează banda de la 20 la 25 dB, în funcție de deficitul de M. Vânt puternic la sol, strat cu limită ridicată și CAPE ridicat reduce câștigul, deoarece îndepărtează sau rup straturile [5, § 4.4].

Culoarea este câștigul estimat în dB, cu o scală fixă egală în toate temele:

| Câştig | Culoare | Semnificație |
| Sub 7 dB | Nimeni | Atmosfera aproape normală |
| 7 | albastru | gradient peste normal |
| 10 | verde | strat super-refractiv |
| 13 | galben | strat puternic, dar fără conductă pentru bandă |
| 16:18.5 dB | portocaliu | strat foarte puternic sau canal prea subțire |
| 1 | roșu | Banda de prindere a conduitului |
| de la 21 dB | Purpuriu | Conductă puternică și profundă |

Cu caseta de afișare, de asemenea, 3 Un geam punctat delimitează zona acoperită de propria sa prognoză și prezintă câștigul maxim vizibil: dacă este sub 7 dB harta rămâne fără culori, deoarece atmosfera este aproape normală, nu pentru că datele lipsesc. Atunci când descărcați un nou clic pe Prognoza Actualizare nu întrerupe; pentru a opri acolo este Stop.

Culorile sunt opace la 82%, astfel încât fundalul temei doar le schimbă. Grila de localizare și numele pătrate sunt proiectate peste culori. În cazul în care predicția dumneavoastră nu este acolo, dar Hepburn sau F5LEN da 5 sau mai multe cărți de șah apar cu culoarea acelei surse.

Harta judecă fiecare punct singur. Calculul semnalului controlează în schimb faptul că stratul este continuu de-a lungul traseului (punctul 9): o rută care traversează foarte galben poate da mai puțin decât harta te face să crezi.

## 4 bis. Previziuni mediate de reclame: coridoare (de la 1.3.3)

Politica de drepturi de autor: Un singur link nu trebuie să tragă o conductă. O recepție izolată și scurtă poate fi dispersarea unui plan de tranzit (pachetul APRS este foarte scurt), o deschidere de câteva minute sau o eroare, și ar da "drop-conduct" ei înșelă. Programul recunoaște un coridor atunci când, în aceeași bandă:

- cel puțin 3 perechi de stații independente (fără stație comună) lucrează pe rute de cel puțin 300 km;
- traseele sunt paralele: azimutul se situează la ±15°, punctele medii la 150 km de axa comună;
- toate în ultimele 30 de minute, iar fasciculul se repetă la o distanță de cel puțin 10 minute (persistență);
- pentru ARS, un salt contează numai dacă se repetă același salt;
- de la 1.3.4, rutele de peste 1500 km se numără numai dacă fasciculul provine din cel puțin două surse diferite (de exemplu, PSKReporter și un grup): de la o singură sursă, până la acele distanțe de 2 m, sunt mai des EME, meteoriți dispersați sau locatori greșiți decât tropo.
- de la 1.3.4, nu număraţi staţiile "la raze": care, în ultima oră, au cel puţin 5 corespondenţi peste 1000 km răspândiţi pe un ventilator de cel puţin 150°. În 2 m tropa este direcţională; un receptor care "free" împreună Israel, Portugalia, Anglia şi Italia, de la 2000;3700 km, de multe ori cu semnale pozitive, are aproape sigur frecvenţa configurat prost. În I5MZY: Arhiva 72% de cazare în 2 m peste 1000 km a venit de la stații astfel; pe 22/11 la 10:53 UTC au fost OH6HLH, OZ1ITS, F4LUK, OM8ABC și SP2JLR, și cu filtru cel mai lung coridor coboară de la 2175 km (regulă 1.3.3) la 615 km.

Surse: cluster spot (nu AS/ACS), PSKReporter prin MQTT, QSO și WSJT-X/JTDX/MSHV decodings și APRS sare în 2 m și LoRa 433/868 MHz.

2 m şi Lora. Programul citeste APRS-IS numai in citire (rotate.aprs2.net, filtru pe Europa) si extrage salturi radio intre statiile de locatie cunoscute (sorgente → digipeater → IGate), cum ar fi harta VHF a DXView si APRS PropView (RF-YVY, Apache 2.0). Trei benzi sunt în valoare de: 2 m, LoRa 433 MHz (coridoarele de bandă 432) și LoRa 868 MHz (corridoi care contează pentru 1296: o conductă care aduce și frecvențele de 868 MHz). Trupa vine de la LoRa pentru a apela (lista de dispozitive) și de la frecvențele sau cuvintele din comentariu; un salt este în valoare doar între două stații ale aceleiași benzi.

Punctul delicat este repetitoarele active: un semnal primit la 500 km poate fi sosit cu două sau trei transmisii digipeater. Prin urmare, un salt contează numai dacă este între două transmițătoare reale și consecutive:

- calea trebuie să fie urmărită: fiecare digipeater care a retransmitat și-a pus numele, iar saltul este măsurat între unul și următorul; un alias folosit fără nume (W IDE1 *, W IDE2 *, alias regional) rupe lanțul și acea întindere nu contează;
- Retransmisii ascunse: programul își amintește calea pe care fiecare sursă o cere de obicei (de exemplu W IDE2 -2). În cazul în care un pachet vine cu un salt în mai puțin (W IDE2 -1) și nici un nume o explică, un digipeater retransmited it without tracking and the colet is dropsed; sursele nemaivăzute înainte nu conta;
- Digipeater în mijlocul drumului: dacă între cele două stații, mai mult sau mai puțin pe drum, există un digipeater din aceeași bandă activă în ultimele trei ore, saltul este aruncat, deoarece ar fi putut retransmița (în special pentru digipeater LoRa care retransmite fără a schimba calea);
- pachete non-radio (qAC, qAX, TCPIP), cele operate prin internet prin radio de la un IGate (a treia parte), HF și frecvențe diferite, stații la altitudine peste 1500 m (paloni, aeronave) și salturi peste 1500 km.

Efectul asupra hărții (de la 1.3.4). Acesta atrage numai zona în care este valabilă zona minimă garantată: o bandă de ±60 km în jurul părții centrale a fiecărei rute (de la 15% la 85% din lungimea sa), nu pe stațiile de la capete, unde conducta nu poate fi prezentă. Benzile tuturor coridoarelor sunt trase împreună, într-o culoare neutră (textul) și semi-transparentă: suprapunerile nu se adaugă, iar culoarea nu confundă cu violetul scării (21 dB și dincolo). Etichetele (trasee, distanţă medie, câştig minim) sunt scanate între ele; trecerea cu mouse-ul pe o etichetă apar rutele coridorului.

În zonă și numai în apropierea timpului curent (±90 minute), prognoza nu scade sub minim. Raţionamentul este: dacă staţiile trec cu adevărat, propagarea este acolo, indiferent de modelul meteo.

Deoarece rutele scurte valorează puțin (de la 1.3.4). În 2 m, între 300 și 600 km, două stații bune (aproximativ sute de wați, antene, FT8 sau CW) se conectează aproape în fiecare zi chiar și cu atmosfera normală, pentru troposcatter: semnalul difuzat de micile nereguli ale aerului în volumul comun al celor două pachete. Trei sau patru locuri la 500 km spun că trupa funcționează ca de obicei, nu că există o deschidere: GEKO Magazine n. 69 ( IU3QEZ) estimează pentru Troposcatter cu 2×10 elemente care curge de aproximativ 440 km în SSB QRP, 510 km în SSB cu 50 W, 600 km în CW QRP şi 640 km în SSB cu 500 W (surse, unsprezece tur). Troposcatterul nu mai este suficient doar peste 600-700 km şi o serie de conexiuni paralele indică într-adevăr o conductă sau un strat. Până la 1.3.3 un coridor de 450 de kilometri a ridicat prognoza la 9/12 dB, iar harta a arătat o îmbunătățire care nu exista.

Prin urmare, valoarea inițială corespunde distanței medii a rutelor:

- până la 400 km: 3 dB;
- de la 400 la 700 km: sare în mod liniar de la 3 la 10 dB (500 km → 5,3 dB; 600 km → 7,7 dB);
- de la 700 la 900 km: de la 10 la 16 dB; peste 900 km rămân la 16 dB.

La 7 dB, care este la începutul scalei, rămâne minimul că: coridorul este desenat cu eticheta sa, se observă că banda funcționează, dar culorile hărții nu se schimbă (cu caseta arată, de asemenea, 3 Deasupra celor 7 dB valoarea este cântărită, jumătate și jumătate, cu numărul de trasee independente (3 accidente vasculare cerebrale → 0,25; 6 sau mai mult → 1) și cu media S/N a relațiilor (−20 dB → 0,5; 0 dB sau mai mult → 1; necunoscută, ca în zonele grupate, → 0,75), începând de la 7 dB. Exemple: 3 rute fără S/N pe 500 km → 5,3 dB; 5 rute pe 615 km → 7,7 dB; 3 rute fără S/N pe 900 km → 11.5 dB; 3 rute până la −20 dB pe 900 km → 10,4 dB; 6 rute la 0 dB pe 900 km → 16 dB.

Distanţa minimă rămâne de 300 km: ridicarea acesteia la 500 km ar fi îndepărtat de asemenea adevăratele deschideri cu rază scurtă de acţiune (de exemplu, o conductă pe Tyrrhenian la 400 km, care este recunoscută prin prognoza meteo şi semnale foarte puternice mai mult decât de la distanţă). În arhiva I5MZY, pe 22/11/2026 la 10:53 UTC, cu această regulă și filtrul de raze coridoarele mai lungi au fost 615 km (7,7 dB) și 540 km (6,3 dB); cu normele de 1.3.3 a ajuns la 2175 km și 16 dB.

Până la 1.3.3 cel puțin era valabil pe întregul dreptunghi al coridorului și depindea numai de distanță (de la 10 dB la 300 km până la 16 dB la 900 km): cu multe coridoare lungi împreună (de exemplu, pe 22/11/2026, douăsprezece coridoare până la 2620 km) au ridicat aproape toată Europa la 16 dB.

Curbele de câştig nu se mai trag de Se opreşte cu "Previziunea mediată de pete."

## 5. Linii opționale și comparație cu presiunea și inversarea

Curbele câștigului: linii la 7, 10, 13, 16, 18,5 și 21 dB, marginile scalei, trasate pe câmp netezite cu metoda pătratelor de marș, cu eticheta în dB.

Isomare: presiune la nivelul mării la fiecare 4 de la același GFS, până la ora de prognoză, cu centrele H și L.

Inversiune termică: curbe acoperite cu cât aer este mai cald la 925 sau 850 hPa decât la 2 m, la +1, +3, +5 și +8 °C.

Sub control o linie oferă acordul câștig cu presiunea și cu reversul pe suprafața vizibilă: coeficientul de corelare Pearson, −1 la +1. Conductele se nasc adesea în anticiclone (aerul care scade căldură și formează reversul), astfel încât un acord pozitiv cu presiunea este normal; dar în interiorul aceluiași anticiclon câștigul se schimbă mult între mare, coastă și munți. Inversul singur nu este suficient: îndoiţi undele numai dacă deasupra aerului este, de asemenea, uscător [5]. Linia servește pentru a vedea, acum, care dintre cele două cele mai bune explică zonele colorate. Aceeași comparație este scrisă în registru.

## 6. Conducte potențiale: dublă calculare

Nivelurile GFS variază între 200 și 1000 m: o inversiune subțire este răspândită și modelul nu vede conducta. Dacă între două niveluri scăderea N depășește normal (40 N/km), aerul este stabil (gradient termic sub 3 K/km) și mai sus este uscat (raportul specific de umiditate sub 0,8), programul consideră o conductă potențială și calculează-l în două moduri:

- Calcul A: toate excesele concentrate scad în 50 m.
- Calcul B: aceeași scădere distribuită pe grosimea tipică a conductelor din acea zonă, luată din hărțile climatice ale UIT-R P.453 [4] (la sol sau ridicată).

Pentru deficit și pentru lungimea maximă de undă prinsă se menține cea mai mică valoare: este o estimare prudentă.

Datele UIT-R P.453 sunt © UIT și pot fi utilizate în scopuri personale și necomerciale, citând sursa, dar nu pot fi redistribuite: pentru aceasta nu sunt în pachetul programului. Pentru a porni calculul B, descărcați doar hărțile P.453 de la UIT în filă: programul, după ce a arătat condițiile de utilizare, descărcați arhiva conductelor de pe site-ul UIT (elevated_and_suprafață_duct.zip) pentru uzul personal al utilizatorului, extrageți fișierele din Data/ P453 și activați calculul fără repornire. Alternativ, descărcați de pe pagina de produs digital ITU-R Group 3 (itu.int, ITU-R Study Group 3, produse digitale, voce P.453, date de suprafață și date de conducte ridicate) și introduceți cele șapte S_OCCUR. TXT, S_thick.TXT, S_stren. TXT, E_OCCUR.TXT, E_base.T Fara fisiere programul foloseste doar calculul A si il scrie in fila si in registru. Raportul dintre deficitul A și intensitatea tipică P.453 este anomalia: De câte ori conducta de azi este mai puternică decât în mod normal. P453

## 7. Stații și locatori

Proprietarul poate avea 4, 6, 8 sau 10 caractere. Cu 8 sau 10 caractere poziția este exactă și cota de sol vine de la Copernicus GLO-30: înălțimea antenei este suficient de la sol. Cu 4 sau 6 caractere pătratul este prea mare: este mai bine să scrie cota de antenă pe mare; în cazul în care 0 programul rămâne estimarea cu sol SRTM la centrul pătratului plus înălțimea de la sol și semnalizează în rezultate. Pentru înălțimea implicită corespunzătoare este 3 m de la sol; cu cota de mare 0 este utilizat sol SRTM plus înălțime.

## 8. Land, difracție și troposcatter

Profilul rutei este eșantionat pe programul Copernicus GLO-30 la fiecare 30 m (mai mult de 120 km lățime). Raza echivalentă a Pământului este k = 157/ (157 + ΔN), cu ΔN declinul N în primii km de la profilurile GFS de-a lungul rutei [6, 7].

Reliefuri aproape de stații (de la 1.3.2, criteriul autorului). P.452 plătește unghiul orizontului liniar, 0,573 dB per mrad: o reliefare de câteva zeci de metri la câteva sute de metri de antenă costă zeci de dB, mult mai mult decât se observă. Din acest motiv, reliefurile la 20 km de fiecare stație pot adăuga până la 6 dB: unghiul orizontului datorat acestora este limitat la cel al celor mai îndepărtate reliefuri plus 10,5 mrad. Este valabil pentru postul dumneavoastră și pentru corespondent. Linia "Reliefs Near," spune câte dBs au decolat.

Sursa de teren (de la 1.3.2). Cotele provin din programul Copernicus DEM GLO-30 [3]: 1 secundă arc, marea este 0 și coasta este clar. Până la 1.3.1, programul a folosit cutiile de AWS "skadi" ale lui Terrain Tiles. În apropierea coastei, aceste pătrate amestecă SSRTM cu date de joasă rezoluție de pe fundul mării și împrăștie cota insulei pe mare: în jurul Capri creează un inel fantomă de 80 La 21 noiembrie 2026 IC8TEM a raportat un orizont neexistent spre nord-vest; compararea punct cu punct a Copernic a confirmat-o (94 m pe mare la nord-vest de Marina Grande vs. 0). Acest orizont fals, de +2,6°, costa aproximativ 26 dB de Troposcatter la staţiile de coastă. Geamurile skadi rămân ca rezervă numai atunci când Copernicus nu răspunde. Macadamele Copernicus au o greutate de 15

- Spaţiu liber: Lfs = 92,44 + 20 log f(GHz) + 20 log d(km) [8].
- Difracție: Metoda Bullington pe profilul real plus UIT-R P.452-16 § 4.2 [6] delta-Bullington corecție sferică a solului, cu ITU-R P.526 [9] pierdere sferică.
- Prima zonă Fresnel: raza Pentru fiecare obstacol, se indică parametrul [9] Knife Board V și J(VV)..
- În proiectarea profilului raza directă este verde în vedere optică și roșie dacă trece sub pământ sau sub curbura pământului; punctele roșii sunt obstacolele care invadează prima zonă de Fresnel (linia punctată purpurie). Pe mare "punct roșu" la altitudine 0 m nu este un munte: este marea care se ridică pentru curbură. Trecerea cu mouse-ul pe raza sau pe un punct apare un comic cu explicație și valori.
- Atunci când o relief în apropierea antenei închide direcția (de exemplu, un orizont de zeci de grade), difracția și troposcatrul dau pierderi de sute de dB: peste 300 dB programul scrie "> 300 dB" și "direcție închisă de un obstacol vecin," semnalul "< − 230 dB" și raportul S/N "−60 dB." În acest caz, este recomandabil să se uite la planta recomandată și la via monta.
- Troposcatter: UIT-R P.452-16 eq. 45 până la 50% din timpul [6], cu distanță unghiulară față de orizonturi și N0 față de profiluri.
- Fără Tropo: suma puterilor de difracție și troposcatter, niciodată sub spațiul liber.

## 8 bis. Troposcatter: patru modele și incertitudine

Lângă P.452 programul calculează Troposcatter cu alte trei modele, așa cum face programul Tropolink [16], și arată diferența lor ca incertitudine:

- BNS TN101 § 9 [25] (de la 1.3.5), mediana: L = 30 log f - 20 log d + F(θd) − F0 + H0 + Lgp. F(θd) este funcția de reducere a emisiilor din anexa III în formele sale analitice (Ns 301) cu corecția Ns, H0 funcția de creștere a frecvenței de § 9.2: la 144 MHz, cu antene mici în lungimi de undă, reflexia din sol șterge parțial câmpul în partea inferioară a volumului comun și pierderea de sare de 3 Ns este cea a profilurilor GFS de oră, orizonturile și înălțimile efective provin din profilul Copernicus, Lgp = 0,07·e^(0,055(Gt+Gr))))) este pierderea de cuplare de deschidere. Este aceeași formă pe care ITM o utilizează în interiorul său, dar cu reactivitatea de astăzi în loc de cea a climei. Se testează pe secțiuni plane la 144 MHz cu antene la 20 m: în intervalul 2 - 4 dB de la ITM între 150 și 900 km și aproximativ 2 dB în forma simplificată 9,2b de TN101, care nu are H0.

- UIT-R P.617-5 [17] L = F + 22 log f + 35 log θ + 17 log d + Lc, cu f în MHz, θ în mrad, d în km, F = 0,18· N0 ·e^(−hs/7,35) − 0,23· N0 și ΔN sunt de obicei luate din hărțile climatice ale UIT; aici folosim la fiecare oră profilurile GFS ( N0 ·e^(−hs/7,35) este reactivitatea la sol, ΔN scăderea în primul km), astfel încât, de asemenea, Troposcatter urmează timpul.
- RGB VHF/UHF Manual Formula, DL2FZN [18]: L = Lfs + 24,44 + 4,283 ln f + 0,06656 d. Panta cu distanța este aproape egală cu cea a PROPBER (0,067 dB/km).

Volumul comun (de la 1.3.1): este punctul în care raza celor două stații este traversată, fiecare înclinată spre unghiul orizontului său, pe un Pământ cu raza echivalentă ae = k·6371 km. Din TX, raza crește ca z = hTX + x·θt + x2/(2·ae); Distanța x = (hRX − hTX + d^θr + d2/(2·ae) / θ, cu θ = d/ae + θt + θr. Cu orizonturi plate și antene la aceeași cotă, baza este în valoare de d2/ (8·ae): aproximativ 1,3 km la 300 km, 3,7 km la 500 km, 9,4 km la 800 km. Cu cât volumul este mai mare, cu atât aerul este mai uscat și rarefiat și cu atât mai puțin se răspândește: din acest motiv, mai mult de 800 de kilometri Troposcatter se termină. În rezultate sunt cota și poziția bazei, reactivitatea, gradientul și umiditatea GFS la acea altitudine, precum și distanța echivalentă a Bray (QST, noiembrie 1961): fiecare orizont suplimentar sau mai puțin valorează aproximativ 111 km. Referinţă disciplinară: prezentarea "Dincolo de orizont în VHF" de IU3QEZ (GEKO Magazine n. 69).

Linia Troposcatter, alte modele arată cele două pierderi și incertitudine ca fiind jumătate din diferența dintre modelul cel mai optimist și cel mai pesimist. Acestea sunt doar comparații: pierderea fără tropo rămâne cea a P.452.

## 8 ter. după-amiază Troposcatter peste reliefuri (de la 1.3.4)

IK3XTV, din Thiene ( JN55, cu Alpii la 10 km), lucreaza in dupa-amiezile de vara din sudul Germaniei la 400.00.500 km, fara vedere, cu semnale puternice, dar cu evanescenta rapida si profunda; autorul le-a atribuit "celulelor convective aliniate ca un tren de lentile." Observaţia este confirmată; explicaţia adoptată în program este alta, mai fermă fizic.

După - amiază, soarele stârneşte aerul peste reliefuri, iar stratul limită se ridică la 2 - 3 km. În partea de sus a aerului umed de mai jos se întâlnește aerul uscat de mai sus: acolo turbulența indicelui de refracție (dimensiunea care guvernează troposcatrul) este maximă. Atunci când vârful ajunge la volumul comun al traseului, care la 400 de kilometri și 500 de kilometri este de aproximativ 2 Acest lucru explică ora de după-amiază, presiunea ridicată (cer liniștit, încălzire puternică) și evanescența rapidă, tipică difuziei și nu conductelor. Celulele convective nu acționează ca lentile: produc turbulențe și turbulențele din partea superioară a stratului reformat sunt cele care răspândesc semnalul.

Consecințele prognozelor de citire:

- pe pământ, prima după-amiază este cea mai rea perioadă pentru conductele şi straturile joase, dar nu şi pentru troposcatul de dincolo de munţi: cele două lucruri trebuie citite separat;
- programul dispune deja de cele două date care servesc, înălțimea stratului limită furnizat de model și ponderea volumului comun al traseului (registrul " volum comun" în rezultate): atunci când acestea sunt aproape, un tratament montan după-amiază poate merge mai bine decât calculul în atmosferă normală;
- de la 1.3.4 calculul punctului ia în considerare: în cazul în care volumul comun este pe pământ, de-a lungul traseului există reliefuri de cel puțin 800 m, soarele este peste 15°, iar stratul limită depășește 800 m, iar partea superioară a stratului de remodelare (modelul ratei solului + înălțimea stratului limită) este la 500 m de baza volumului comun, troposcatrul câștigă 3 dB; între 500 și 1000 m câștigul liniar scade la zero. Linia "Troposcatter pomeridiano." Cu zapada de-a lungul traseului (cel putin 10 cm pe pamant conform modelului, variabila de zapada_aprofunda de Open-Meteo sau SNOD a GFS de la NOMADS) armarea nu se aplica: pe zapada solul ramane rece si stratul de re-in forma nu creste. Valoarea de 3 dB este provizorie și trebuie calibrată cu legături reale; se stinge cu corecții meteorologice.

Surse: observații ale F. Egano IK3XTV (Radio Rivista e www.qsl.net/ik3xtv,

## 9. Stratul fondator de-a lungul traseului

Este centrul de calcul al semnalului. Profilurile GFS sunt luate de-a lungul traseului la fiecare aproximativ 25 km. Pentru fiecare strat de presiune, cea mai lungă întindere a aceluiași strat se găsește pe puncte consecutive; conta ca conducte numai straturi groase de cel puțin 40 m [10].

- continuitate: de la ce km la ce km şi fracţiunea de traseu;
- partea superioară și baza și înclinația în m/km;
- sol în interiorul stratului: km unde munţii se ridică deasupra bazei;
- Teren acoperit: pe mare conducta este aproape întotdeauna, decide întinderea de teren [10]. Dacă terenul depășește 10% din traseu, fracția utilizată este minimă între continuitate și acoperire a pământului.

Câştigul trope reprezintă câştigul stratului pentru fracţiunea utilizată. Dacă mai mult de jumătate din profile sunt sub-refractive, câştigul este negativ.

## 9 bis. Corecții meteorologice: stabilitate, subsidență, grevă

De la 1.3.0 profilul oferă, de asemenea, trei informații meteorologice, care pot fi văzute în registru (regave

- Stabilitatea Misme [5, capitolul 6]. Misme leagă câmpul din afara orizontului de un parametru compus: M = a·(ge − 40 + b· Programul calculează ΔW = g · Termenul adăugat la câștig este de 0,07 dB pentru J/kg peste standardul, limitat între −4 și +6 dB, și contează numai în cazul în care nu există capcane de conductă banda (gradibilitatea este deja în indicele Bean și Dutton). Calibrarea este temporară: trebuie verificată cu raportul (punctul 14).
- Subsidence și Uphill Air [19]. Moler și Holden, pe rutele de 125 GFS oferă viteza verticală la 850 și 700 hPa. În cazul în care la ambele niveluri aerul scade mai mult de 1 cm/s și există o inversare sau o scădere bruscă a indicelui de refracție K potențial (N raportat adiabatic la 1000 hPa, care subliniază aerul uscat și cald a scăzut de la [5] top), 1,5 dB se adaugă; în cazul în care aerul crește mai mult de 5 cm/s, dacă decolează 2.
- [20] Scirocco. Aerul cald şi uscat din Sahara, deasupra aerului marin mai rece, oferă inversiune foarte puternică din estul Mediteranei către Gibraltar. Semnul S apare în cazul în care la 850 hPa vântul vine de la 100 E doar o indicaţie: câştigul decide straturile profilului.

Fără o conductă care blochează banda, câștigul total nu depășește 16 dB, sfârșitul galbenului: corecțiile fac straturile mai puternice mai evidente, dar nu le trec prin conducte (roșu și violet rămân la conducte).

- Persistență (de la 1.3.1). Cele mai mari deschideri vin după zile bune de înaltă presiune (VHF/UHF DX Book; Biancomano și Leyshon 1998, regula de 12 ore; rapoartele de 29/12/2O/2/1/2022 cresc de la prima la a patra zi). Pentru fiecare celulă, programul numără orele celor 24 de ore anterioare, cu un câștig de cel puțin 13 dB și adaugă până la 2 dB în proporție, numai în cazul în care câștigul este deja de cel puțin 10 dB.

Cum se citesc semnele de pe hartă (Schimbarea semnelor meteo; de la 1.3.2 simbolul este mare dublu și trece cu mouse-ul peste un semn apare un comic cu explicația și valorile celulei):

| Semnează | Culoare | Ce înseamnă asta? | Efectul asupra trope |
|---|---|---|---|
|  ↓  | verde | subsidence: picaturi de aer (anticiclon), caldura pentru compresie si uscat, intari invers si mentine ferm | favorabil, +1,5 dB |
|  ↑  | roșu | aer în sus (față, joasă presiune, convecție): se amestecă straturi joase și se rupe inversiuni și conducte | defavorabil, −2 dB |
|  S  | portocaliu | Sirop: aer cald și uscat din Sahara peste aerul proaspăt de mare | indiciu favorabil, nici o corecție: decide straturile |

O zonă plină de verde ↓ sub un anticiclon este semnul clasic al unei deschideri dure; Săgeți Red ↑ de-a lungul liniei spun că straturile sunt de rupere, deși culoarea celulei este încă bună.

Cutia de corecţii meteo opreşte aceste corecţii pentru a vedea modelul de 1.2.

Validarea (punctul 14): privind conexiunile 593 la 144 MHz între 29 decembrie 2021 și 2 ianuarie 2022, comparativ cu aceleași rute și două săptămâni mai târziu, indicele de separare (0,5 = zero, 1 = perfect) este 0,79 fără corecții și 0,91 cu corecții (înainte de acoperiș la 16 dB). Aceeași comparație arată limita principală: conducta de pe mare la altitudine scăzută, care în acele zile a adus semnele între Campania și Liguria, nu este vizibilă la nivelurile GFS.

## 9 ter. Ştiri 1.3.2: două estimări, tavan, presiune ridicată, mare, staţii, creste

estimare prudentă şi agresivă. Câștigul de la gradient crește de la 0 dB până la limita normală de până la 10 dB până la pragul superrefracției. În stratul 1000/850, pragul prudent este Bean și Dutton (100 N/km); pragul agresiv este pragul clasic de super-refracție de 79 N/km (k Calculul punct-punct arată cele două pierderi flancate; cu o conductă care blochează banda coincide. Harta foloseşte precauţie.

Mansarda de 2500 m. Drumurile și conductele sunt căutate doar până la 2500 m deasupra solului: cu cât antenele radio de amatori nu le exploatează. Cu 1500 m validarea a pierdut un punct de separare și 4% din conexiunile de peste 16 dB, deoarece în timpul iernii inversările de la subsidence sunt adesea între 1500 și 2500 m.

Presiune mare extinsă. +1 dB în cazul în care cel puțin 7 din cele 9 celule din jur au redus presiunea în mare de 1020 hPa sau mai mult și vântul este slab în straturile joase (10 m sub 5 m/s, 850 hPa sub 8 m/s), cu excepția mișcărilor ascendente.

Greutatea mării. În calculul punctului de referință, câștigul straturilor se înmulțește cu 0,8 + 0,4·μ1, unde μ1 este factorul UIT-R P.452 (eq. 51) pe cea mai lungă întindere continuă de teren: 1 pe mare liberă, mic cu întinderi lungi de teren.

Stații și efectuate. Atunci când o conductă care blochează banda acoperă doar o parte a liniei, ponderea fiecărei antene este comparată cu baza și partea superioară a conductei la începutul și la sfârșitul secțiunii acoperite; câștigul este multiplicat cu 0,5 + 0,5· Cu conducta pe aproape tot traseul este cuplarea § 10.

Strat si creste. În cazul în care partea superioară a stratului de frontieră al GFS (rata de sol a celui mai mare model de margine_strat_înălțime) depășește crestele traseului (spații maxime peste 300 m, dacă cele mai mari depășesc 500 m), stratul trece peste munți fără a se rupe: +1,5 dB pentru fracția de creste acoperite. Este observația IT9GSF privind deschiderile din Sicilia în Germania cu Tyrrhenian favorabil.

Nivele aproape de sol pe mare. Cu sursa NOMADS programul descarcă, de asemenea, temperatura de suprafață (pe mare este SST), temperatura și umiditatea la 80 m și în stratul de 30 Pe puncte de mare (rata de model mai mică de 5 m) aceste niveluri sunt adăugate la profil, sub primul nivel de presiune, pentru a vedea conducta de la 100 Pe pământ nu sunt folosite: straturile de noapte şi de zi nu sunt încă valabile. Open-Meteo oferă doar temperatura de suprafață.

Conducta de evacuare. De la SST, de la temperatura și umiditate la 2 m se estimează înălțimea δ a conductei de evaporare cu profilul logaritmic neutru (ca în modelul Paulus și Jeske, fără corectarea stabilității): aerul la parul apei este saturat la SST (98% pentru sare), prin urmare,

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

limitată între 0 și 40 m. Pentru a prinde 144 MHz ar fi nevoie de aproximativ 180 m (Kerr), apoi la 144 MHz conducta de evaporare se consolidează puțin; de la 432 MHz în sus, cu antene mici pe mare, mult (punctul 11 bis). Introduceţi ecuaţia parabolică şi în linia Corecţii 1.3.2.

Ora zilei (vizita). Programul calculează înălţimea soarelui pe fiecare punct. Din zi în zi pe pământ, cu soarele peste 15° şi cu stratul limită peste 800 m sau vânt peste 5 m/s, avertizează că conductele terestre sunt remodelate; pe timp de noapte, cu soare sub −3°, vânt sub 3 m/s şi strat limită sub 300 m, avertizează că prin cer senină sunt posibile iradieri, care cad după răsărit. Nu pe mare. Pentru moment, acestea sunt reperate, nu numere: validarea decembrie nu are suficiente zile de vară pentru a le calibra. Acestea sunt două puncte pe care autorul le consideră importante, din observarea zilnică.

Profilul normal CRPL (alternativ). În loc de 40 N/km fix, scăderea normală urmează atmosferei de referință exponențiale CRPL: N(h) = Ns·e^(−c·h), cu − Cu aer umed (Ns mare) normal este mai abrupt și servește un gradient mai puternic pentru a da câștig; în altitudine este mai dulce. Se pare rău până validarea spune că ajută.

## 9 quater. Contrastul dintre mare și mare joasă: Marea Adriatică (de la 1.3.3)

Undele radio nu simt fundul mării: simt contrastul dintre suprafaţa aerului şi cea a apei. Adâncimea contează pentru că decide cât de repede temperatura mării urmează cea a aerului.

Marea Adriatică este o mare joasă. Adâncimea medie este de aproximativ 250 m, dar bazinul nordic rareori depășește 100 m iar Golful Trieste rămâne sub 25 m; numai la sud coboară peste 1200 m. Un strat de apă atât de subţire se încălzeşte şi se răceşte în câteva zile, Tyrrhenian în câteva săptămâni. Există trei situații tipice:

1. Primăvara şi vara devreme, favorabile. Marea este încă rece, chiar și pentru apa dulce a râurilor Alpine și Po; ea ajunge la aer cald și uscat din câmpie sau din Balcani. Aerul cald deasupra apei reci formează o inversare pe mare (ducta de advecţie): este condiţia clasică a deschiderilor dintre Italia, Slovenia şi Croaţia.
2. Toamna și iarna cu bora, nefavorabil. Aer rece deasupra unei mări mai calde: stratul scăzut devine instabil, apasă şi conducta se sparge.
3. După un bora, şansă. Doar pentru că marea e joasă, o boră o răceşte în câteva zile. Dacă apoi introduceţi un anticiclon cu aer cald, contrastul aer-mare este puternic şi conducta este mai robustă decât de obicei.

Marea Adriatică este, de asemenea, un canal închis între Apenini și Dinari, mai puțin de 200 km lățime: stratul mării rămâne limitat, iar rutele de-a lungul axei (Puglia

Ce calculează programul. Pe punctele de mare, cu corecțiile meteorologice pe și caseta

| Stare | Corecție |
| --- | --- |
| temperatura potențială la 925 hPa (sau 950) peste SST de 4 K | _ |
| peste SST de 7 K | +2 dB |
| cu aer umed la 925 hPa (peste 80%) | −0,5 dB pentru aceste bonusuri |
| mare răcită cu 1,5 °C sau mai mult în 72 de ore înainte, cu contrast favorabil | _ |
| mare cu aer cald la 2 m de 2 °C (mixare) | −1,5 dB |
| mare mai caldă de 4 °C | −3 dB |

Corecția totală rămâne între −3 și +3 dB și, fără conductă, câștigul rămâne sub acoperișul galben. Pragurile sunt temporare: in atmosfera standard temperatura potentiala la 925 hPa o depaseste pe cea a marii de cca 2

Memoria SST. Modelele menţin temperatura mării aproape constantă în timpul predicţiei. Scăderea recentă rezultă din analiza celor trei zile anterioare (Open-Meteo, numai temperatura suprafeței, numai celulele marine): maxim 72 de ore înainte de a minus valoarea curentă. Dacă serviciul nu răspunde, bonusul de memorie nu se aplică.

Limite. SST-ul modelelor de mai jos și în Marea Adriatică superioară este aspru; stiloul rece al Po, de exemplu, este rezolvat prost. Batimetry nu intră în calcul: sursele oferă întotdeauna SST, care rezumă deja efectul fundului mării. Contrast aer-mare se suprapune în parte la sfârșitul stabilității Misme (§ 9 bis); pentru aceasta bonusurile sunt mici. Pragurile vor fi tarate cu coridoarele de la fața locului și QSO prin Marea Adriatică.

## 10. Condus pe întreaga rută

În cazul în care stratul blochează banda pe cel puțin 80% din fracția utilizată și stațiile nu sunt la vedere, pierderea conductei este, de asemenea, calculată: spațiul liber, plus cuplarea antenelor (3 dB dacă acestea sunt în interiorul conductei, mai mult dacă acestea sunt deasupra sau dedesubt), plus atenuarea reziduală γd = 5·10−5·ae·f^(1/3) dB/mrad de UIT-R P.452 eq. 49 [6]. Dacă este mai mică decât pierderea fără Tropo devine pierderea așteptată.

O bandă rămâne în conductă numai dacă lungimea sa de undă nu depășește λmax = 2 Pentru aceasta acelasi strat poate prinde 23 cm si 70 cm dar nu 2 m. Cu toate acestea, limita nu este netă: Kerr avertizează că chiar valuri de mai multe ori mai mult suferă o mulțime de conducta, și Hepburn și VHF / UHF DX Carte vorbesc de margini "sferte" și conducte "iartă." De la 1.3.1 programul utilizează o conductă moale: dacă λmax este între λmax și 3·λmax, câștigul trece treptat de la 15 dB (λ = 3·λmax) la câștigul total al conductei (λ = λmax), cu factorul s = (λmax/λ − 1/3)/(2/3). În acest caz, culorile conductei sunt permise. Jurnalul și linia Meteo de-a lungul traseului indică punctele cu conductă moale. [11]

## 10 bis. Via Monte

Când corespondentul este închis în spatele orizontului, programul caută cea mai bună direcţie pentru a indica antenele: panta iluminată răspândeşte semnalul ca ţintă radar. Acesta face cercul orizontului la fiecare 5th (cu excepția ±15° în jurul corespondentului) și în fiecare direcție ia punctul orizont iluminat, peste 100 m și între 1 și 60 km. O estimare rapidă (spațiu liber, conversie bitactică și 10 dB pentru fiecare nivel de orizont de la difuzor la corespondent) alege 4 cele mai promițătoare, cel puțin 15° fiecare dintre ele; pentru fiecare:

- trata 1, stație → relief, în spațiul liber;
- tratează 2, relief → corespondent, calculat ca și cum relieful transmis de la 10 m deasupra vârfului, cu aceeași predicție (fără ecuație parabolică);
- conversia cu ecuația radar bistatic, cu σ = σ0 · A, σ0 = − 15 dB (munte în VHF/UHF) și A zona iluminată de fasciculul R1 (maxim 20 km2):

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

Linia "punct recomandat" indică azimutul celei mai bune rute, dacă costă mai puțin decât direct. La 144 MHz, cu o reducere la 20 km și un yagi de la 15°, drumul montan costă cu aproximativ 45 dB mai mult decât ruta 2 numai: se întâmplă doar atunci când direct este închis de obstacole învecinate. Estimarea este brută (±10 dB), în special pentru σ0.


Fără pierderi la munte (opțiune, de la 1.3.2). Cu Runda orizontului alege apoi punctul cu orizontul cel mai liber spre corespondent (la paritate, cea mai mare) iar rezultatul arată traseul de la punct la corespondent, cu semnal și S/N, lângă total cu difuzia. Este o limită optimistă: spune că dacă direcția se deschide atunci când muntele ridică semnalul bine, sau unde să meargă portabil. Fiecare punct este dat cu zece caractere.

## 10 ter. Intrarea într-o conductă îndepărtată

Dacă conducta care prinde banda începe departe, dincolo de orizontul staţiei, semnalul trebuie să sosească. Programul aduce semnalul la punctul de pornire, în mijlocul conductei, pentru difracție: de-a lungul profilului Copernicus GLO-30 caută marginea cu cel mai mare parametru Fresnel (ITU-R P.526), cu umflarea terenului echivalent,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

mai mult reducerea datorată curburii vârfului (obstacol rotund, P.526-16 § 4.2), cu raza R estimată de la solul Copernicus GLO-30 la ±1 km de margine;

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

(pentru m·n > 4 este forma 34b a recomandării). Barsis (NBS, 1963) a măsurat pe trei rute lungi dincolo de un munte (Colorado, Alaska, Europa) pierderi medii cu 10 Rezultatul se compară cu difracția pe sol sferic neted; este cel mai rău. Apoi semnalul circulă în conductă: spațiu liber pe întreaga rută, atenuare reziduală γd de P.452 pe secțiunea din conducta și cuplarea corespondentului (3 dB în interior, 1 dB la fiecare 10 m în exterior). În cazul în care pierderea este mai mică decât pierderea preconizată, aceasta devine mecanismul ("conductă (inpută prin difracție)"). Este premisa autorului asupra geografiei italiene: cu un obstacol în apropiere mult sub linie, v este negativ și costurile de intrare mici; cu o costuri frontale dorsale 15

## 10 quater. ITM (Longley-Rice) și ieșirea din conductă

De la 1.3.2 programul conține modelul neregulat de teren 1.2.2 al NTIA/ITS (Longley and Rice, ESSA ERL 79-ITS 67, 1968; Hufford, Longley și Kissick, NTIA 82-100, 1982), tradus în C# de codul oficial al guvernului american, care este liber de drepturi. Traducerea dă aceleași numere ca și originalul: compilația automată îl compară de fiecare dată cu codul NTIA C++ în cinci cazuri de testare (sub 0,01 dB).

ITM calculează pierderea în atmosferă normală pe profilul real Copernicus GLO-30: văzută cu reflexie din sol, difracție (pigol și teren sferic cântărit pe rugozitatea solului Δh, aceasta este diferența dintre percentila 10 și 90° a cotelor din jurul dreptei pătratelor minime) și troposcatrul. Reactivitatea solului se reduce la partea medie a traseului, $N_s = N_0\,e^{-h/9460}$, și oferă curbura terenului echivalent;

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

Programul utilizează climatul radio al traseului ales ca în § 10 sexies (până la 1.3.4 întotdeauna marea temperată, cu N0 320 pe sol și 350 pe mare), cu N0 de la reactivitatea medie a locului (sol εr 15, σ 0,005 S/m; mare εr 81, σ 5 S/m), polarizare orizontală și variabilitate în timp fără cea a locului (profilul este cunoscut). Acesta arată pierderea depășită în 50% din timp și cel pe care îl aveți în 10% și 1% din timpul cel mai bun. ITM nu modelează conductele: variabilitatea sa în timp este statistică, prin climă. Aceasta servește ca un al doilea calcul al valorii de referință de lângă P.452; diferența dintre cele două oferă o idee de incertitudine.

A doua utilizare este ultima întindere după o conductă care se termină înaintea celei corespunzătoare. La punctul de ieșire, o antenă virtuală este plasată la jumătatea conductei și ITM calculează tractul până la cel corespunzător, ca exces peste spațiul liber la 50% din timp. Pierderea totală este

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,	heta_{condotto} + A_{ITM}(d_{uscita} 	o RX)
```

unde A_TX este cuplarea antenei (3 dB în interior, 1 dB la fiecare 10 m în afara) dacă conducta începe aproape, în caz contrar, difracția de intrare a § 10 ter. Este limita dată de autor: continuitatea stratului plus, cel mult, atenuarea reziduală. Dacă este mai mică decât pierderea preconizată, aceasta devine mecanismul ("conductă (ieșire și tractul final ITM)").

## 10 quinquies. Aprofundare: insule, strâmtori și discontinuitate la mare

Un exemplu din Golful Neapole (Anacapri, JN70CN) spre vestul Mediteranei arată unde pot traversa insulele:

| Destinația | Azimut | Unde traversează insulele. | Obstacol |
| --- | --- | --- | --- |
| Barcelona |  279°  | la nord de Sardinia (Gallura) până la 41,05° N, între 420 și 490 km | aproximativ 30 km sud de Bocche di Bonifacio; Limbara (1359 m) aproximativ 20 km de linie |
| Bocche di Bonifacio |  282°  | aproape de 41,3° N | aproximativ 11 km |
| Marsilia |  296°  | Corsica Centrală la 42,2° N | munţi până la 2700 m: o conductă sub 2 km nu întrerupe |
| Valencia, Balears |  260–265°  | Sardinia Centrală | reliefuri interne |

Pentru Barcelona este suficient pentru a indica la 282° în loc de 279°, deoarece axa trece din gura: 3rd, în interiorul grinzii unui Yagi normal.

Gurile sunt strâmte? Pentru conductele joase, sub câteva sute de metri, da: insulele sunt zidite, și deasupra pământului, în ziua, încălzirea rupe inversiunea. Strâmtoarea în sine este suficient de largă: aproximativ 11 km, față de o primă zonă de Fresnel, mai puțin de un kilometru lățime în mijlocul unei rute de 1000 km în 144 MHz. Problema e vremea. Gurile sunt o distracţie pentru vânt: Stratul inferior este accelerat şi stratul inferior este îndepărtat. Adesea pasajul este deschis ca geometrie, dar perturbat ca atmosfera. Cu un anticiclon stabil și puțin vânt poate fi în schimb pasajul bun.

Diferenţă de mare. Contează. O conductă formată pe coasta tireniană este întreruptă: semnalul iese din conductă (sectiunea după ieșire, § 10 quater) și trebuie să se întoarcă la o altă conductă pe marea Balerics, plătind din nou cuplarea. Conductele la altitudine, peste 1500 m, trec peste Gallura fără să-şi dea seama; pentru Corsica centrală nu se aplică.

În practică: dacă traseul traversează o insulă muntoasă, încercați un anumit grad de corecție spre întinderea mării libere, dar nu conta pe calea cu vânt puternic îngust.

## 10 sexi. TN101 radioclimat și variabilitatea timpului (de la 1.3.5)

TN101 § 10 [25] descrie modul în care pierderea unei rute dincolo de orizont variază pe parcursul perioadei lungi din jurul medianei, cu diferite curbe de climă: funcția Y(q) spune cât de multe dB scade pierderea în q% din timpul cel mai bun. ITM conține aceste curbe pentru șapte climate. Clima contează foarte mult: pe mare în climatul marin temperat, la 144 MHz, în 10% din timp câştigi mult mai mult dB decât pe uscat în climatul continental, deoarece conductele sunt mai frecvente.

Programul nu mai repară climatul: El o alege pentru fiecare tratament din punct de vedere mediu, astfel încât funcționează peste tot în lume și nu numai în Europa. Descarcă din Open-Meteo [26] trei ani de valori zilnice ale reanalizei ERA5 (temperatura, umiditatea și presiunea solului, ploaia) pentru celula de 0,5° din punctul mediu, o singură dată; valorile rămân în Date/CLIMA. Restaurarea solului

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

zi de zi, apoi media anuală și excursii între mediile lunare, umiditate medie și ploaie anuală. Normele urmează descrierile climatelor din TN101 § 10.2; limitele dintre un climat și altul sunt un criteriu al autorului:

- peste 60° latitudine: continental temperat ( TN101 nu are curbe pentru climatul polar);
- ploaie mai mică de 250 mm pe an și umiditate medie sub 50%: desert (Ns tipic 280);
- în termen de 20° de la ecuator; Ns de cel puțin 350 și excursie până la 30: ecuatorial (Ns tipic 360);
- în limita a 32° și a unei excursii de cel puțin 60: continental subtropical, adică musoni (excursie tipică 60
- între 10° și 32° cu Ns de cel puțin 340: marină subtropicală (Ns tipic 370);
- ambele orizonturi pe mare, peste 20°: marine temperate pe mare (este definiția TN101);
- excursii până la 30 în apropierea mării (mare pe traseu sau umiditate medie de cel puțin 75%): temperat maritim pe pământ;
- în caz contrar temperat continental (excursie tipică 20

N0 pentru ITM este media redusă la nivelul mării, N0 = Ns·e^(h/9460). Fără reţea foloseşti o regulă de rezervă doar cu latitudinea şi fracţiunea de mare, şi linia o spune.

Teste cu date ERA5 Ns 339 cu excursie 45 (continental temperat pe uscat, temperat pe mare, atunci când ambele orizonturi sunt pe mare); Londra 327 și 16 Viena 319 și 24 Abidjan 381 și 11 (ecuatorial); Miami 365 și 36 (soț subtropical); Delhi 337 și 82 (concontinental subtropical); Riad 278 și 37, 90 mm ploaie (deșert). În Chile: Antofagasta 341 și 19 (soț subtropical: coasta Atacama, cu reversul aproape fix al Pacificului), Calama 232 pe sol, 2257 m, 14 mm de ploaie (deșert), Valparaíso 331 și 11 (cusătură de temperatură), Punta Arenas 313 și 5 (cusătură de temperatură).

Linia "Climat radio ( TN101) " arată climatul, motivul, Ns cu jumătate din excursie și Y: cât de multe dBs câștigă în 10% și 1% din timp și cât de multe pierd în 90%. Este statistica pe termen lung și rămâne separată de prognoză: pierderea așteptată urmează momentul actual cu profilurile GFS, și suma două lucruri ar conta de două ori aceeași propagare anormală.

## 11. Comparație cu UIT-R P.452

Linia P.452 aplică metoda UIT pentru conducte și reflexie pe straturi (P.452-16 § 4.5 [6]) cu mediana ΔN a GFS în loc de cea climatologică și p = 20% ca constantă de calibrare, ca în studiul CSIRO [10]. Este doar o comparație: pierderea preconizată rămâne cea de la alineatul (9).

## 11 bis. Ecuație parabolică (de la 1.3.1)

Linia de ecuație parabolică este un al patrulea calcul, doar de comparație, realizat ca programe profesionale pentru conducte (PETOOL; cadrul de unda-propagare al licenței M. Lytaev, MIT): în loc de estimarea câștigului din straturi, avansează valul de-a lungul traseului cu metoda de despărțire pas de Fourier în cadrul profilului de refracție modificat M al GFS, luate din punctul de previziune mai aproape de fiecare distanță. Nivelurile GFS adaugă salturile estimate ale conductelor potenţiale (calcularea A, 50 m în jumătate de strat). Solul Copernicus GLO-30 intră în trepte (sub pământ câmpul este nul), solul este conductor și fasciculul antenei este un gaussian cu deschiderea estimată din câștig. Calculul vede conductele, cei care pierd, difracţia şi cuplarea antenelor; El nu vede troposcatrul, care este adăugat separat. Următorul este cecul de profil: același cu atmosfera standard mai puțin cea cu profilul așteptat.

De la 1.3.2:

- propagatorul este în unghi larg, cu faza exactă a spațiului liber:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- fundul mării și câmpia de coastă la altitudine zero reflectă cu coeficientul de Fresnel în polarizare orizontală, εc = εr − j·60·λ·σ (sea εr = 81 și σ = 5 S/m; țara 13 și 0,006 S/m), la unghiul clar al traseului:

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- rugozitatea mării reduce reflexia consecventă în funcție de Miller și Brown, cu înălțimea efectivă a valurilor de la vânt la 10 m (σh = 0,0051·U2):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- pe mare, profilul M include conducta de evaporare (punctul 9 ter) cu profilul logaritmic neutru:

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

Acesta testează cu prototipul (10 m antena pe mare, câștiga pe atmosfera standard la 100 Conducta de evacuare de la 10 la 30 m oferă o valoare cuprinsă între 1 și 3 dB până la 144 MHz, 8 la 37 dB până la 432 MHz și 21 la 1296 MHz. La 144 MHz în polarizare orizontală, marea reflectă aproape ca un metal (

Limite: deasupra pamantului profilul vertical ramane cel al nivelelor GFS, astfel incat conductele subtiri raman invizibile; pamantul la trepte este aproximativ; raza ramane in a 6-a.

## 12. antene, semnal și zgomot

- Yagi DL6WU: creştere 9, 5 + 7, 8·log10(boom/λ) dBd (2,35 dB pentru boom dublu, DL6WU măsuri privind VHF Communications 3/1982), plus 2,15 pentru [12] dBi.
- EIRP = putere − cablu + câştig TX.
- Semnal primit = EIRP − pierdere așteptată + câștig RX − cablu RX. S-metru IARU: S9 = −93 dBm, 6 dB pe unitate.
- Zgomotul exterior conform UIT-R P.372 [13]: galactic (Fa = 52 − 23 log f) mai artificial în funcţie de mediu (oraş, rezidenţial, rural, rural liniştit) sau de o temperatură de antenă scrisă.
- Tsys = Ta + Tpre + [290(Lc - 1) + 290(Fr - 1) ·Lc] / Gpre la terminalele antenei (formula Friis). De la 1.3.5 stația receptorului are întotdeauna un preamplificator în antenă, parametru fix: numărul de zgomot 0,8 dB (Tpre = 59 K) și câștigă 20 dB. În spate se află cablul (pierdere Lc) și radioul, cu numărul de zgomot de bază Fr 6 dB. Cu 1 dB de cablu cablul și radioul de intrare scade de la aproximativ 1160 K la 12 K: zgomotul decide cerul, mediul și preampa. Câştigul de 20 dB este o valoare tipică aleasă de program.
- S/N în SSB, CW și FT8 în banda fiecărui mod, comparativ cu pragul stabilit.
- Probabilitatea pentru perioada (de la 1.3.2): Evanescența în decurs de o oră este o distribuție normală a S/N în jurul valorii medii estimate pe oră. Bray (QST, noiembrie 1961, p. 39, pe curbele Norton 1959) oferă o amplitudine de 13,5 dB pe aproximativ 100 de mile, adică aproximativ 7 dB mai jos și 7 deasupra mediei, care cade aproape drept sub 100 de mile (3,5 dB la 50 mile); pentru o legătură "solidă" ia 7 dB din buget. Luate ±7 dB ca ±2σ: σ = 3,4 dB pe 160 km, proporțional cu distanța de mai jos. Probabilitatea ca o perioadă să depășească pragul drumului este

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  cu funcția de distribuție normală de "%": marjă 0 dB = 50%, +3,4 dB = 84%, +7 dB (marja "solidă") = 98%. Aceasta nu include incertitudinea modelului, care este mai mare.
- Căi digitale: S/N în 2500 Hz comparativ cu pragurile măsurate la 50% decodare, fără informații prealabile (AP). FT8 −21 dB și FT4 −17,5 dB la zgomotul alb; atunci când mecanismul este Troposcatter, utilizați pragurile cu "mediu de latitudine perturbată," FT8 −18,6 dB și FT4 −15,2 dB [21]. Q65 -15C −22,2 dB, Q65 -30B −24,8 dB, Q65 -60A −27,6 dB În cazul avioanelor dispersate, Doppler poate depăși tonurile de Q65 -60A (1,7 Hz): există mai bine 30B sau 15C. JT65A −25 dB [22]. AP de decodare (numele QSO deja cunoscut sau sfârșitul QSO) oferă aproximativ 2 dB suplimentar pentru FT8 și 1,5-2.6 dB pentru Q65; nu intră în prag pentru că nu este valabil pentru toate valorile [24] FT8

## 13. Timpi de răspuns și date

Fiecare calcul măsoară timpul său propriu: profile GFS (rețea, analiza, pauzele obligatorii de 20 de s între loturile pentru limita Open-Meteo), teren Copernicus GLO-30 (cu descărcarea de panele, 15 de 30 MB fiecare, doar prima dată), calcul. Linia timpului de răspuns le rezumă; în registru rândurile încep cu TEMPI. La modernizarea grilei, registrul raportează, de asemenea, cursa GFS utilizată, atunci când a devenit disponibilă și cât de vechi sunt datele.

De la 1.3.2 puteți alege și ICON-2I, modelul Agenției ItaliaMeteo și Arpae pe Italia la aproximativ 2,2 km, prin Open-Meteo (model italian_meteo_arpae_icon_2i, două curse pe zi, 3 zile). Acesta acoperă doar Italia și mările din apropiere și are niveluri mai scăzute ale GFS (1000, 925, 850, 700, 500 hPa): pentru conductele de la altitudine nu este mai bună decât GFS, dar rețeaua fină descrie coastele, insulele și marea la 2 m, care este conducta de evaporare. Variabilele pe care un model nu le are (limita de strat, temperatura suprafeţei...) se elimină din cerere. De la 1.3.2 sursa automata foloseste NOMADS; Open-Meteo se serveste daca NOMADS nu raspunde si pentru cazurile istorice. Cu cutia "Update pe cont propriu" (umplută implicit) prognoza va fi descărcată fără a da click: la deschidere, la fiecare nouă cursă GFS pe NOMADS (la aproximativ 5 ore de la momentul cursei) și atunci când zona vizibilă este afară pentru mai mult de un sfert de la rețea; cel mult o încercare la fiecare 15 minute, niciodată cu un caz istoric.

Cererile la Open-Meteo au limite pe minut, pe oră și pe zi pentru fiecare adresă de internet. Profilele descărcate sunt salvate pe disc (Dati/cache-gfs) pentru fiecare accident vascular cerebral și model: cu aceeași cursă, care se schimbă la fiecare 6 ore, punctele descărcate deja nu cere din nou. Cu sursa automată porniți de la NOMADS și Open-Meteo servi numai pentru puncte lipsă; cu singurul Open-Meteo, la limita de timp sau zilnic se oprește imediat și spune când să încerce din nou. Cazurile istorice nu sunt distruse.

## 14. Validarea modelului

Prognoza ar trebui comparată cu conexiunile reale. Jurnalul de calcul păstrează fiecare număr; arhiva programului păstrează anunțuri și decodări. Pentru 432 și 1296 MHz, unde locurile sunt puține, acestea au nevoie de rapoarte de la operatori: care au făcut conexiunea, la ce oră, cu ce semnal. Comparați timp de multe ore câștigul așteptat cu semnalul măsurat spune dacă pragurile trebuie să fie mutate.

## Surse

1. NOAA/NCEP, Global Prognoze System (GPS), model global la 0,25°, patru curse pe zi.
2. Open-Meteo, prediction API (open-meteo.com), gfs_seamless model, variabile la nivel de presiune și metadate de curse.
3. Copernic DEM GLO-30 (ESA; WorldDEM © DLR e.V. 2010 Rezerva: NASA/USGS Copernicus GLO-30 în Terrain Tiles "skadi" de AWS.
4. UIT-R P.453, Indexul de refracţie radio: data formulării şi refracţiei, cu hărţi ale canalului digital (Studiul Grupului 3, © UIT, uz personal şi non-comercial).
5. B. R. Bean, E. J. Dutton, Radio Meteorologie, Monografia BNS 92, 1966: Tabel 6.5 și § 4.4.
6. UIT-R P.452-16, Proceduri de predicție pentru evaluarea interferențelor dintre stațiile de pe suprafața Pământului la frecvențe de aproximativ 0,1 GHz: § 4.2 (difracție), eq. 45 (troposcatter), § 4.5 și eq. 49 (condus).
7. UIT-R P.834, Efectele refracţiei troposferice asupra propagării undelor radio.
8. UIT-R P.525, Calcularea reducerii spațiului liber.
9. UIT-R P.526, Propagare prin difracție: eq. 31 (cutcheon) și § 3 (teren sferic).
10. B. Indermuehle, H. Suzuki, Tropospheric Ducting Prediction bazat pe modelul GFS Data la Inyarrimanha Ilgari Bundara, Observatorul Radio-astronomic CSIRO Murchison, Publicații ale Societății Astronomice din Australia, 2026 (arXiv 2608.12861).
11. D. E. Kerr, Propagarea undelor radio scurte, MIT Radiation Laboratory Series vol. 13, 1951: eq. 17 e 18.
12. G. Hoch DL6WU, 1 (măsurată 432 și 1296 MHz în funcție de lungimea boom-ului).
13. UIT-R P.372, Zgomot radio: zgomot galactic și artificial pentru mediu.
14. NOAA/NCEP, NOMADS, filtru GRIB al GFS 0,25° timp (nomads.ncep.noa.gov); format WMO G RIB2, ambalaj complex cu diferențe spațiale.
15. Open-Meteo, Prognoza istorica API (model previzional arhiva de la aproximativ 2021).
16. LukiBox, Tropolink, designul traseelor Troposcatter cu P.617, BNS TN101 și ITM flancate (GitHub, Apache 2.0).
17. UIT-R P.617-5, Tehnici de predicție a propagării și date necesare pentru proiectarea sistemelor de radiorelay trans-horizon, 2019.
18. RGB, Manualul VHF/UHF; implementarea DL2FZN (andi-f/troposcatter on GitHub, 2013).
19. W. F. Moler, D. B. Holden,
20. ARRL Handbook 2022 (99a ed.), § 19.4.3
21. S. Franke, B. Somerville, J. Taylor, The FT4 and FT8 Communication Protocols, QEX July/August 2020.
22. W3SZ, praguri de moduri digitale lente ( JT65, JT9, QRA64) și alegerea modului în funcție de propagare, 2019.
23. QST, "Lumea peste 50 MHz," Noiembrie 2022: deschiderea între Manitoba și Kansas/Missouri din 14 și 15 august 2022.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Quick-Start Guide to Q65, 3 aprilie 2021 (wsjt.sourceforge.io).
25. P. L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis, Predicții privind pierderile de transmisie pentru circuitele de comunicare troposferică, Nota tehnică BNS 101, revista 1967: § 9 (troposcatter, funcția F(θd), H0), § 10 și anexa III (climate radio și variabilitate în timp).
26. Open-Meteo, Historical Weather API (ECMWF ERA5 review from 1940; Copernicus Climate Change Service, CC BY 4.0 license).
