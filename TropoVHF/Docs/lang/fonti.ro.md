# Surse și formule

> Traducere automată din italiană: în caz de îndoială, este valabil ghidul în italiană.

Colectarea de surse de lucru consultate pentru prognoza tropo LIVETRUPOVHF (20/221 noiembrie 2026): Fizica de baza, date disponibile, indici publicati, formule, experiente radio amatori, validare si decizii. Formularele sunt numerotate ca în LaTeX, (1), (2),... și în textul pe care îl amintesc cu numărul în paranteze. Manualul Prognoza descrie modul în care programul le utilizează.


## 1. Sediul autorului

Observaţii IC8TEM care încadrează munca, cu feedback-ul găsit în surse.

- Geografia italiană cere difracţie. Cu crestele, promontoriul si insulele la cateva zeci de kilometri de statii, contributiile la difractie ar trebui calculate cu parametrul Fresnel pe marginea principala, nu numai cu terenul neted. Întâlnire: într-o comparație pe teren neregulat la 563 MHz (Rio de Janeiro, 27 de puncte între 5 și 30 km), cele mai bune metode au fost P.526 cu marginile lamei, P.1812 și pană difractivă, cu eroare medie între −5 și +3 dB și dispersie de aproximativ 9 dB; P.1546 a fost cea mai pesimistă (din Costa, Rabanal Ramirez și Pentru pante abrupte din apropierea stației PETOOL, amintiți-vă că acestea servesc propagatori cu unghi larg și calculul bidirecțional (unde reflectate de reliefuri).
- Pentru a intra într-o conductă îndepărtată, ai nevoie de Fresnel. În cazul în care conducta începe dincolo de orizont, semnalul vine la noi prin difracție peste obstacole până la începutul său; de la 1.3.2 programul calculează (Manualul prognozelor, § 10 ter). Întâlnire: Rohde & Schwarz (2023) constată că staţiile de-a lungul unei conducte înalte nu reuşesc adesea să intre, în timp ce cele aproape de final da.
- Condiţiile se schimbă rapid. Teoriile sunt acolo, dar condițiile se schimbă în minute, și nici un sistem de prognoză ia datele în timp real: GFS iese la fiecare 6 ore, cu interval de timp și 0,25°, și o prognoză poate pierde valoarea într-un timp scurt. Intalnire: Bray (QST, noiembrie 1961) masuratoarea fluctuatiilor mediane pe ora de aproximativ ±7 dB peste 100 mile; conductele se misca cu timpul atmosferic (Rohde & Schwarz); registrul nostru QSO de 29/12/202/21/2022 arata deschideri in crestere si scadere in cateva ore.
- Cel mai bun acum-casting rămâne rețeaua DX cluster Pentru a ști ce se întâmplă acum, punctele de grup, PSKReporter (și WSPR) bate orice model: acestea sunt măsuri. Prognoza spune unde să se uite și cât de mult poate dura; reclamele spun dacă există. Programul pentru acest lucru pune spot și prognoza pe aceeași hartă.

## 2. Scop

Ideea este un al treilea indice, calculat de LIVETROPOVHF însuși pornind de la profilurile verticale ale unui model meteo, care urmează să fie adăugate la Hepburn și F5LEN în loc să le înlocuiască. Astăzi programul citește hărțile lor deja gata: un index ar spune, de asemenea, * de ce * există deschidere (contabilul și grosimea conductei, banda minimă prinsă) și ar putea apărea cu locurile pe care programul le colectează deja.

- Ce se poate da: probabilitatea de conductă pe punct și pe oră, cota de bază și partea de sus, grosimea, frecvența minimă prinsă, și de-a lungul unei căi fracția de traseu în interiorul unei conducte.
- Ceea ce nu se poate da: conductele subţiri (sub câteva zece metri) pe care modelele globale la 0,25° nu le rezolvă, iar conductele de evaporare de pe mare, care servesc mai presus de toate de la GHz în sus.
- Primul obiectiv practic: 144 și 432 MHz în Europa, cu verificarea pe puncte de grup DX, PSKReporter și decodificări locale.

## 3. Fizică de bază

Totul începe de la refracţie. N calculat la fiecare nivel de profil: conducta este în cazul în care reactivitatea modificată M scade alpinism, adică în cazul în care dN/dh scade sub −157 N/km ([ITU-R P.453-13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)).

Refrenitate (P și în hPa, T în kelvin):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Presiunea aburului de la umiditatea relativă H (%) și temperatura t (°C), deasupra apei între −40 și +50 °C:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

Reactivitate modificată (h în km sau 0,157 · z cu z în metri) și criteriul conductei:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN/dh (N/km) | Regulamentul | Efectul a 144 MHz |
| 0 | subrefracție | debit redus |
| de la 0 la −79 | normal (standard aproximativ − 40) | orizont radio cu raza echivalentă 4/3 |
| de la -79 la -157 | super-refracție | flux alungit, "prea bun" |
| sub −15 | conductă (capcană) | Semnale blocate, DX peste 1000 km |

Cum se extrage N dintr-un model la nivel de presiune: pentru fiecare nivel luați T, umiditate relativă și înălțimea geopotențială, calculați N și M, apoi gradient între nivelurile învecinate. Nivelurile de sub sol trebuie aruncate (la 1000 hPa pe Alpi este subteran).

Frecvenţa minimă prinsă: conducta ghidează doar undele destul de scurte în comparaţie cu grosimea sa. Hepburn publică o grosime a tabelului → frecvență minimă utilă ([DX Info Centre](https://www.dxinfocentre.com/propagation/luf.htm)); este o limită ușoară, sub care semnalul scade treptat.

| Grosime Led | Frecvența minimă de plată |
|  61 m  | 962 MHz |
|  91 m  | 464 MHz |
|  122 m  | 276 MHz |
|  152 m  | 185 MHz |
|  183 m  | 133 MHz |
|  213 m  | 101 MHz |
|  305 m  | 53 MHz |

În gama VHF (adesea 120 Adaptarea eu însumi (nu o formulă publicată de Hepburn), cu f în MHz și d în metri:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

În practică: pentru cei 2 m este nevoie de o conductă de multe ori de cel puţin 170 de metri, pentru cei 70 cm este suficient de aproape jumătate. Formula care circulă pe net, λmax = 0,014 · d^1.5, provine de la Kerr (1951), capitolul 1, eq. 18) și este valabilă cu λ în centimetri și în picioare, pentru o conductă în care M cade liniar: cu aceste unități oferă pentru aproximativ 2 m 180 m și pentru aproximativ 70 cm 89 m, care este aceleași valori ale tabelului Hepburn, care vine din acesta. Kerr avertizează că nu este o tăietură netă: chiar și valurile de mai multe ori mai mult suferă o mulțime de conductă, și un mod în care "pierde" un pic poate îmbunătăți recepție chiar și cu stații din conductă. Un studiu de acces IEEE 2019 confirmă faptul că metodele clasice (Kerr, Kukushkin) nu descriu tăietura reală ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)): pragul ar trebui utilizat ca indicație, nu ca perete.

## 4. Date disponibile gratuit

Punctul slab este rezoluţia verticală: între 1000 şi 850 hPa modele dau un nivel la fiecare 25 de ani50 hPa, adică la fiecare 200 de metri450 m, astfel încât să vadă bine inversiunile la 500 de metri dar nu şi conductele subţiri din apropierea solului. LIVETROPOVHF folosește deja Open-Meteo pentru a izola, temperatura la 850 hPa și inversarea: indicele poate porni de la aceleași apeluri.

| Sursă | Ce dă | Rezoluția | Utilizare pentru index |
| [Deschide-Meteo, GFS](https://open-meteo.com/en/docs/gfs-api) | T, umiditate relativă, punct de rouă, înălțime geopotențială după nivel (1000, 975, 950, 925, 900, 850 hPa...); înălțimea stratului limită | 0,25° la nivelul presiunii; 16 zile, ore până la 120 h, actualizate la fiecare 6 ore | profil N și M, gradient, bază și partea superioară a conductei; deja în program |
| [Deschide-Meteo, ECMWF](https://open-meteo.com/en/docs/ecmwf-api) | Același nivel de la modelele de date deschise 0,25°; sol oferă punct de roua |  0,25°  | al doilea aviz, util pentru concordanţa dintre modele |
| [Deschide-Meteo, DWD ICON](https://open-meteo.com/en/docs/dwd-api) | T şi umiditate relativă la nivelul presiunii; în ICON- D2 sunt 800, 900 şi 925 hPa | ICON-UE aproximativ 7 km | al treilea aviz privind Europa |
| [Radiosunds, Univ. Wyoming](https://github.com/NCAR/RAOBget) | profile măsurate, două lansări pe zi (00 și 12 UTC), format text per stație | multe niveluri semnificative, chiar la fiecare câteva zeci de metri | verificarea indicelui; a se vedea conductele subțiri pe care modelul nu le vede |
| [Gemete, NOAA NBEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive) | arhiva istorica de radiodifuziune, un fisier pe statie, mai multi parametri derivati | vezi mai sus | calibrare pe ani de date și comparație cu petele din trecut |
| ERA5 (Copernicus) | Reanaliză orală din 1940, 37 niveluri de presiune | aproximativ 28 km | statistici și calibrare, nu previziuni |

- Cu înălțimea geopotențială Open-Meteo este peste nivelul mării: nivelurile care cad sub pământ trebuie eliminate ([Deschide-Meteo, aer superior](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)).
- Pe sol se adaugă T și umiditate la 2 m: acestea dau prima întindere de gradient, cea în care conductele de suprafață de noapte se nasc pe câmpie și pe mare.
- Transmisiuni italiene pe arhiva Universităţii Wyoming (weather.uwyo.edu/upperair/sonding.html): staţii şi numere WMO în tabelul de mai jos.

| Stație | W | Latitudine | Longitudine | Partajare |
| Udine/Campoformido |  16044  |  46,03  |  13,18  |  94 m  |
| Milan/Linat |  16080  |  45,43  |  9,28  |  103 m  |
| Practica maritimă |  16245  |  41,65  |  12,43  |  12 m  |
| Brindisi |  16320  |  40,65  |  17,95  |  10 m  |
| Trapani/Birgi |  16429  |  37,92  |  12,50  | aproximativ 14 m |

Formatul text al Wyoming oferă pentru fiecare presiune nivel, înălțime, temperatură, punct de rouă și umiditate: suficient pentru N și M. Wyoming nu are o interfață oficială, scripturile sunt utilizate ca [RAOBget of NANCR](https://github.com/NCAR/RAOBget); Colegiul DuPage păstrează copii în text cu stație (de exemplu, 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)).](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. Indexuri și metode publicate

Hepburn și F5LEN pornesc de la aceleași ingrediente fizice, dar nu publică algoritmul complet; studiile științifice din 2008

| Metoda | Date de pornire | Ce se calculează | Note utile pentru noi |
| [Hepburn Tropo Index](https://www.dxinfocentre.com/propagation/hti.htm) | modele meteorologice brute ale Serviciului Meteorologic Canada (MSC, aşa cum este scris sub hărţile sale; nu GFS/NAM), hărţi concepute cu GRADS, algoritm proprietar ([storia](https://www.dxinfocentre.com/propagation/history.htm)) | Scala liniară de curbură a semnalului: 0 normal, 1 | scara de imitat, astfel încât al treilea index se compară imediat cu primele două |
| [F5LEN, prognoza mondială](https://tropo.f5len.org/WW/info.php) | pentru Europa modelul ARPEGE al Météo-France, 7,5 km, mai multe nivele între 2 m şi 1500 m ([notă de autor](https://tropo.f5len.org/author/f5len/)] | Index mediu pentru proprietar, 40 de pași de la 3 ore, 4 calcule pe zi, 28 de zile arhiva ([noua versiune](https://tropo.f5len.org/?p=504)) | confirma ca acesta servește un model cu multe niveluri scăzute |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf) | 60 nivele de model ECMWF, de la sol la 3000 m | minimum dN/dz per coloană: realizat sub −157 N/km, superrefracție sub −78,7 N/km; baza stratului de capcană | climatologie: Padana Plain zona europeană cu mai multe conducte pe tot parcursul anului; Marea Mediterană în vară aproape 100%, iarna sub 15%; Italia peste 30% în nopţile de vară |
| [Indermuehle și Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861) | GFS 0,25°, 21 niveluri de presiune + câmpuri la 2 m | M = N + 0,157 z; dM/dz minim sub 3 km, baza și partea superioară a conductei, adâncimea deficitului M; sub 40 m aruncat | verificate timp de 7 ani pe 25 de canale de la 88 MHz la 2,68 GHz; capacitate bună de până la aproximativ 3 zile în avans (ASC aproximativ 0,74), apoi picături; GFS mai bună decât ERA5 |
| [Hämäläinen et al., FMI 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf) | Harmonie-AROME, 750 m sau 2,5 km, 65 sau 90 niveluri | dM/dh < 0 pe profilul modelului, comparativ cu un fascicul de 4 până la 59 m | nu conteaza mai mult vertical decat rezolutia orizontala; nici un model nu vede conductele sub 15 m; umiditatea domina gradientul de temperatura |

Al treilea serviciu radio amator deja activ: [TROPOCAST.EU](https://tropocast.eu/)de Artur SQ3A, pentru 2 m și 70 cm în Europa. Din pagina de prognoză ([tropocast.eu/precast](https://tropocast.eu/forecast)):

- Date: DWD ICON-UE (pass 0.0625°, aproximativ 7 km), sursă implicită; alternativ, PIB-ul canadian (0,15°), mai puțin detaliat.
- Metoda: N și M în conformitate cu UIT-R P.453 din profilul dintre stratul din apropierea solului și 850 hPa; recunoașterea straturilor, unirea conductelor la sol și la înălțime și pragurile culorilor sunt o calibrare adecvată a autorului, nepublicată.
- Produse: Vedere "completă" 0
- Zone: Europa, Polonia, Regatul Unit, Germania, Franța, Spania, Italia, Republica Cehă, Croația; interfață și în italiană.
- Reutilizare: hărțile încorporează cu un iframe fără cheie API; există, de asemenea, un API documentat (OpenAPI și JSON schema). Licența nu este indicată: înainte de a o utiliza ca o a treia sursă lângă Hepburn și F5LEN ar trebui să întrebați autorul.

Diferențe față de proiectul nostru: TROPOCAST se oprește la 850 hPa (aproximativ 1,5 km), în timp ce conductele ridicate ajung la 2000 m și dincolo (caz LA0BY de 22/11/2018), și nu oferă un indice pentru banda sau ruta. Clasa 0

Structura de imagine TROPOCAST (din linkul furnizat de autorul programului): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, unde n este etapa de prognoză. Site-ul interzice retragerea automată la roboți, așa că nu am putut citi imaginea; de asemenea, pentru acest lucru, înainte de a arăta aceste hărți în LIIVROPOVHF servește permisiunea autorului.

Notebook: [DJ5AR, categoria](https://dj5ar.de/?cat=174)(Magonza, 21 articole 2017 Caze descrise: s-au desfășurat câteva săptămâni din Irlanda (23 mai; 2 iunie 2023), FT8 până în Belarus (septembrie 2020), Italia (2008) peste Alpi și Marea Adriatică (august 2017). Recunoaşteţi cazul conductei "prea înalte" (reverse la 1000 m) pe care nu o puteţi folosi din sol: o altă confirmare că este necesar să se compare cota conductei cu cea a staţiilor. Acesta utilizează radiofare ca măsură de deschidere (până la 1028 km) și profile verticale în Paris, Bordeaux și DWD.

În rezumat, rețeta comună: profil vertical → N și M per nivel → strat cu dM/dh negativ → bază, partea de sus, grosimea și forța conductei → index. Partea "secret" a Hepburn este calibrarea empirică a evenimentelor reale, pe care le putem reface cu pete.

## 6. Formulare utile pentru program

În plus față de N și M (secțiunea fizica de bază), acestea sunt utilizate pentru a trece de la numărul vertical la numărul punctului și apoi pe calea. Formulele UIT au fost reconstruite printr-un PDF citit în parte prost; la 22 noiembrie au fost revizuite pe texte oficiale ([ITU-R P.452-18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453-14, P.526-16, P.372-18] și corectate în cod: constanta troposcatrului 190 (nu 190,1) plus absorbția gazelor cu ρ = 3 g/m3; α nu sub −3,4 (eq. 55a); gaz al conductelor cu ρ = 7,5 + 2,5·ω (eq. 9a); difracție pe teren sferic complet (în medie 23 de ore37: mediu de mare, înălțime cu limita de 2 + 20 de bușteni, sub fluxul optic); presiune de saturare cu abur a P.453-14 (eq. 9, cu EF). Rumore P.372-18: tabelul 1 și galactic corespund; curbe de zgomot artificial sunt de până la 250 MHz și galactic până la aproximativ 100 MHz, peste program se extinde aceleași legi.

1. Gradient între două niveluri ale modelului (h în km, M în unitatea M):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. Rezistenţă şi grosime: grosime d = bază superioară (pentru o conductă de sol, bază = sol); rezistenţă ca deficienţă M între bază şi vârf.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3. Factorul echivalent al razei terestre (P.452-18, eq. 5 și 6a), cu ΔN = media drop N în primul km, pozitiv, în N/km:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

Cu ΔN = 40 aveţi aproximativ 4/3 k (atmosfera standard); cu ΔN care se apropie 157 k tinde spre infinit, adică, radioul terestru devine plat: este începutul conductei.

4. Orizontul radio al unei antene de mare viteză h, cu raza echivalentă cu\_e în km:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5. Probabilitatea conductei pentru latitudine (P.452-18, eq. 2), procentul de timp; φ = latitudinea centrului traseului, μ1 și μ4 ia în considerare cât de departe este pe mare:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

La 41° N, toate pe mare (μ1 = μ4 = 1), oferă aproximativ 11% din timp: este baza climatologică, utilă ca valoare de referință de la care indicele preconizat diferă.

6. Pierderea pentru troposcatter (P.452-18, eq. 45; f în MHz, d în km, unghiul θ al traseului în MVS, reactivitatea la sol N0 în centrul traseului):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

Termenul −0,15 · N0 arată de ce aerul umed ajută și fără conductă: 50 de unități N suplimentare sunt cu 7,5 dB mai mici decât pierderea. LIVETROPOVHF utilizează deja versiunea G3YGF a pierderii scatter.

7. Pierdere în conductă (P.452-18, eq. 46

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. Din Bean și Dutton (1966) și Kerr (1951)

Cele două texte clasice oferă trei lucruri care sunt necesare imediat la index: pragurile de gradient deja tarează pe aceleași niveluri de presiune ale GFS, ceea ce este în valoare de dB o schimbare de reactivitate, și ce situații meteorologice creează sau distruge conductele. *Radio Meteorology*, NBS Monograph 92.

1. Clasificarea profilului pentru straturile de presiune (Bean și Dutton, tab. 6.5, p. 248; −dN/dh în N/km). Proiectat pentru lungimi de undă radio, se aplică așa cum este la nivele de model:

| Drum | Partări despre | Subrefracție | Nestratificate | Super-refracție |
| 1000 | 0 | − dN/ dh < 0 | de la 20 la 60 | peste 100 |
| 850 | 1, 46 | − dN/ dh < 0 | de la 20 la 50 | peste 80 |
| 700 | 3,01 | − dN/ dh < 0 | de la 15 la 40 | peste 70 |
| 600 | 4,20 | − dN/ dh < 0 | de la 10 la 30 | peste 50 |

2. Cât de mult valorează în dB (Bean and Dutton, cap. 6, pp.

- Orice strat (la sol sau la altitudine) în comparație cu profilul "smooth" ridică semnalul de 102,5 dB la toate procentele; câștigul mai mare vine de la straturile înalte înclinate, probabil din cauza efectului de focalizare (curs de 200 km).
- Reactivitatea solului Ns și declinul primului km ΔN sunt legate de semnalul median lunar cu coeficient între 0,4 și 0,95, median 0,7 (20 rute de la 130 la 446 km, aproximativ 100 MHz).
- Ciclu anual: 0,18 dB per unitate N; ciclu zilnic de la 0,2 la 1,1 dB per unitate N, maxim pe trasee de 175; 200 km și iarnă.
- Parametrul compus al lui Misme estimează variaţia câmpului ca fiind de 0,5 dB/(N/km) · (gradient echivalent − 40) plus un termen de stabilitate termică; parametrul turbulenţelor D (zona dintre profilul real şi cel adiabatic, 0 Concluzia autorilor: ΔN rămâne cel mai bun parametru pentru simplitate.

3. Profilul de referință și anomalia (Bean și Dutton, § 3.8 și 3.9). Atmosfera exponențială de referință CRPL oferă profilul "normal" pornind de la Ns; unitatea A măsoară cât de mult diferă profilul real la fiecare acțiune. Este modul natural de a transforma un profil GFS într-o anomalie pentru a pune pe hartă:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

Cu Ns = 313 aveți ΔN aproximativ −42 N/km, aceasta este raza echivalentă 4/3. Criteriul conductei −157 N/km este obținut și aici prin legea Snell pentru o rază orizontală (eq. 4.23), aproape independentă de înălțimea antenei.

4. Locul și momentul nașterii conductelor (Bean and Dutton § 4.4; Kerr cap. 3):

| Proces | Situația | Efect |
| Avertisment | aer cald și uscat din teren care curge pe mare mai rece (tipic pentru manșon de vară după zile de vreme bună); în principal, contează deficitul de umiditate | conductă intensă pe mare |
| Iradiere nocturnă | Cer senin, vânt slab; nori mici sau multă umiditate îl împiedică | a condus la pamantul care se naste la apusul soarelui, creste seara si dispare repede dupa rasarit, mai ales in interior |
| Subsidence | aer care coboară din anticiclon, încălzit și uscat, peste aer proaspăt și umed; inundații frecvente în munți și în sectoarele de sud-est de anticiclone | strat super-refractiv sau conductă înaltă; aproape întotdeauna gradientul superior standard în sens invers |
| Marea și pământul | coasta, circulaţie de câteva sute de metri | conducte locale de-a lungul coastei |
| Împotriva conductei | amestec: sol încălzit de soare între mijlocul dimineţii şi după-amiaza, vânt la sol peste câţiva m/s pe teren accidentat; Ceaţă de noapte | Probabilitatea de conductă aproape nimic |

5. Două ingrediente noi pentru a încerca. Mişcarea verticală: curenţii ascendenti din zonele de convergenţă îndepărtează şi elimină straturile, subsidenţa întăreşte semnalul (Moler şi Holden: în medie cu 2,5 dB mai mult decât cu aer de sus, pe mare în banda L şi X). Open-Meteo oferă deja viteză verticală pe niveluri de presiune. Iar tendința de presiune: pe un curs de 140 km la 186 MHz semnalul a scăzut când presiunea a scăzut la transmițător și la centrul căii și s-a întors sus și fără evanescență când a urcat, cu doar 2 (Bean și Dutton, fig. 6.9, p. 241).

6. De la Kerr. Pe lângă formula lungimii maxime de undă (secţiunea Fizică de bază), Kerr oferă o formulă aproximativă pentru M de la singura temperatură, abur şi altitudine (§ 3.9, cu vechile constante 79 şi 4800 înlocuite apoi cu 77,6 şi 4810) şi observă că, în sens invers, tăierea vântului poate genera valuri care leagănă la altitudinea conductei, posibila cauză a unor evadări adânci (§ 3.26). Sursa italiana raportata de Bean si Dutton si cautarea: L. Bonavoglia (decembrie 1958), "Correatia intre fenomenele meteorologice si propagare..."

## 8. De la prezentări radio amatori și rapoarte tehnice (a doua serie)

Şapte documente furnizate de autorul programului. Trei servesc direct la index: W3SZ pentru Troposcatter "floor," LA0BY pentru cazurile în care Hepburn nu a prezis, Hepburn (translation LU7MC) pentru clasificarea meteo a conductelor.

1. Troposcatter: semnalul care există întotdeauna. R. Rehr W3SZ, "Tropospheric Scatter (Troposcatter) Propagation for VHF, UHF, Microwave and Frecvences," Pack Rats Conference 2014; J. R. Duffey KK6MC, "The Troposcatter Propagation Mode and How to Predict Coverage on the VHF/UHF Bands," CSVHF 2017.

- Util de la aproximativ 100 la 700 km, de la 144 MHz la 10 GHz; pierderea crește de 30 de busteni f cu frecvență.
- Mai presus de toate, se numără unghiul de pornire spre orizont: fiecare grad suplimentar costă aproximativ 9 M1CNK
- În timpul verii, semnalul este în medie cu aproximativ 10 dB mai mare decât iarna; la mijlocul după-amiezii cu aproximativ 5 dB mai mic decât dimineața devreme sau seara. KK6MC indică o variabilitate totală de 13
- Metoda yeh, cea mai ușor de programat (f în MHz, d în mile, unghiul θ de împrăștiere în grade, media geometrică α a deschiderilor celor două pachete):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Comparație între Larsen (1968) și 15 conexiuni reale: eroare medie pătrată 5,1 dB pentru Collins, 7,4 pentru Yeh, 8.1 pentru BNS și CCIR, 16,8 pentru Rider. Toate metodele se imbunatateste daca Ns se mentine constant (Yeh scade la 5,4 dB): pentru imprastierea refractiva a zilei conteaza putin, astfel incat indicele tropo trebuie sa se uite la straturi, nu numai Ns. Tabelul format din 15 conexiuni, cu pierderi măsurate (inclusiv Baldo Monte, 322 km la 957 MHz, 223,1 dB; Cipru; Peste 319 km la 77,5 MHz, 163,3 dB), este un bun banc de încercare pentru calculul nostru.
- KK6MC dă pierderea că o stație tipică reușește să depășească 2 m și distanța corespunzătoare, de la 184 dB (FT-817 și 3 elemente în SSB, aproximativ 130 km) la 215 dB (bunguri lungi în SSB, aproximativ 430 km) și 236 dB (kW și yagi cuplate, aproximativ 640 km). Este modul de a traduce indexul dBs în mile câștigate.
- Cod gata: W3SZ Programul Yeh și Collins (nitehawk.com/w3sz/YehCollins.zip) și calculatorul Bob Atkins KA1GT (bobatkins.com/radio/troposcatter.html).

O altă sursă pe scatter: I. Roberts ZS6BTE, "Tropospheric Scatter Propagation," *Radio ZS*, decembrie 1984, cu nota de 2013 ([qsl.net/zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). În principal Geometrie utilă: unghiul de împrăștiere θ = θ0 − θ1 − θ2, cu θ0 = d/R, θ1 = (h1 − h\_ostacolo) /d1 + d1/2 R și același lucru pentru cealaltă stație, R = 8446 km (4/3 din raza Pământului). Este piesa care este folosita pentru a calcula unghiurile din profilul solului din Troposcatter "floor." Alte date: fiecare unghi în mai multe costuri de aproximativ 10 dB; trecerea de la Ns 280 la 300 reduce pierderea de aproximativ 4 dB; volumul scatter este între 300 și 2000 m pentru 150 km, între 600 și 3000 m pentru 300 km; timpul în volumul scatter ridică semnalul de aproximativ 20 dB. exemplul numeric al articolului nu revine cu formulele date (durata spațiului liber la 900 MHz și 872 km este de aproximativ 150 dB, nu 125), astfel încât formulele trebuie verificate pe W3SZ înainte de a le utiliza.

2. Tipuri de conducte (W. R. Hepburn, "Mode de Propagare Troposferică," dxinfocentre.com/propagation/tr-modes.htm, traducere spaniolă a LU7MC). Completați tabelul de Bean și Dutton cu cazuri utile în Italia:

| Tip | Când | Notă pentru index |
| Iradierea | nopţi liniştite şi liniştite pe pământ | Urmează pământul, dispare la scurt timp după răsărit |
| Presiune mare (subsidence) | anticiclonă | poate dura toată ziua; pe timp de noapte conducta de la iradiere de mai jos poate bloca semnale îndepărtate, astfel încât ziua poate merge mai bine |
| În faţă | în faţa unui front cald, în spatele unui front rece, la nord de un front staţionar | scurt; vremea rea îl deranjează |
| Coboară pe munte | föhn, bora: aerul coboară, se încălzește și se usucă | pentru a încerca pentru bora pe Marea Adriatică și föhn la sud de Alpi |
| Avertisment | aer cald și uscat peste sol rece și umed, de exemplu după ploaie | a condus la sol |
| Valea | aer cald peste aer rece se oprește în vale | poate sta în ziua |
| Marino | aer cald și uscat pe apă rece; ajunge pe coastă cu briza | vârf în după-amiaza; primăvară și începutul verii cele mai bune anotimpuri |

Conductele de evaporare de pe mare (0 Munţii înalţi sunt o barieră; deşerturile sunt prea uscate.

3. Cazurile pe care Hepburn nu le-a prezis (S. Heck LA0BY, "Extreme Tropo Propagandation on 144 MHz and up," Nordic FUSHF 2019, de la JO59IX la 500 m altitudine):

- 22 Noiembrie 2018: 70 cm până la UA6 (aproximativ 2600 km) timp de 6;8 ore. Hepburn a indicat numai statele baltice și Polonia. Conducta a fost văzută în hărțile de temperatură la 900 și 1500 m și s-a încheiat la puțin peste 2000 m; a fost mai mare pe partea norvegiană, și doar LA0BY pe deal a intrat. Pe teren timpul nu părea ca o tropă: rece și ceață în partea de sus.
- 11 februarie 2015: 2 m și 70 cm spre 9A și S5 (aproximativ 1500 km) prin Alpi, de asemenea, acest lucru nu este prevăzut de Hepburn.
- 15 octombrie 2011: 2 m cu EA1 (2135 km) în conductă înaltă, pe trei porţiuni de mare şi trei sol.
- Alte evenimente: 6
- Adrese de câmp: ceaţă în vârf liber, vânt mic, pământ umed. Gradientul normal al temperaturii −6,5 °C/km; într-un caz real ( VK3KAQ) +7 °C în 20 m.

Concluzia autorului: Hepburn şi F5LEN nu sunt suficiente, servesc profilele la altitudine de-a lungul traseului. Pentru noi: aceste date sunt cazuri de verificare care trebuie recalculate cu datele istorice ale modelelor, iar indicele de pe traseu trebuie să compare cota conductei cu cota staţiilor.

4. Experienta italiana (G. IV3GBO, "Tipuri de propagare VHF SSB/CW," 2017): realizata intre 450 si 3000 km, cu straturi intre 450 si 3000 m; ceata extinsa ca indiciu; deschideri pe mare pe Marea Adriatica spre Puglia, Albania si Grecia. Împrăştiaţi-vă de la 100 la 500 km.

5. Introducere (P. Wilton M1CNK, "An Introduction to VHF/UHF Propagation," 2013): 70 cm sunt adesea mai bune decât 2 m; cu 10 m pol la 144 MHz, unghiul de pornire este de aproximativ 3°, cu 20 m aproximativ 1,5°.

6. Raportul NASA (E. Hirschmann, "Troposferic and Ionospheric Effects on Radio Frecvence (VHF-SHF) Communication," Goddard X-731-67-89, 1967): se referă la erori de distanţă la sateliţi, nu la DX. Aduce înapoi aceeași formulă N și același profil exponențial (exemplu în Florida: Ns 370, ΔN −55, constant 0,161/km) și trimite două surse BNS pentru a căuta: Bean și Thayer, "CRPL Exponențial Reference Atmosphere," Monograph 4 (1959), și Bean, Horn și Ozanich, "Climatic Charts and Data of Radio Refractive Index for the United States and the World," Monograph 22 (1960).

## 9. Din cartea VHF/UHF DX, capitolul 2

I. White G3SEK (edited by), *The VHF/UHF DX Book*, DIR Publishing 1995 (ISBN 0 9520468 0 6; digital replica 2017), capitol 2 "VHF/UHF Propagation," pp. 2-13, 2-22izare2-29 și 4-11. Este cel mai apropiat text de experienţa radio-amatori dintre cei colectaţi; confirmă pragurile deja utilizate şi adaugă noi reguli sintoptice.

Număr

- Aceeași formulă de N (77,6 P/T + 3,73·10^5 e/T2); exemplu: 1000 hPa, 7 °C și = 10 hPa dauna N = 324. Într-o atmosferă bine conturată, de exemplu după o furtună, N este aproape liniară de aproximativ 39 unităţi/km în primii 2/3 km.
- Gradienții între 0 și −79 N/km sunt normali, sub −79 există super-refracție, peste 0 sub-refracție. Cartea insistă că DX-ul lung nu provine din simpla super-refracție, care ar trebui să rămână pe întreaga cale, ci din conducte.
- Grosimea minimă a conductei, puţin mai prudentă decât Kerr şi Hepburn:

| Bandă | Grosimea minimă |
| 50 MHz |  400 m  |
| 70 MHz |  300 m  |
| 144 MHz |  200 m  |
| 432 MHz |  100 m  |
| 1,3 GHz |  50 m  |

- Raportul nu este proporțional cu lungimea de undă (aproximativ 65 λ la 50 MHz, 220 λ la 1,3 GHz), deoarece marginile conductei sunt "deslocate." În Regatul Unit, conductele sunt frecvente pe 144 MHz, dar aproape niciodată la 70 MHz sau mai jos și adesea cele 432 MHz sunt puternice atunci când cele 144 sunt slabe.
- În conductă, semnalul scade ca 1/d în loc de 1/d2: până la 1000 km, atenuarea poate fi similară cu cea a spațiului liber.
- Capacitatea unei staţii minime pe 2 m (100 W, 15 dBi yagi, 2 dB zgomot): 217 dB, adică aproximativ 400 De acord cu tabelul KK6MC.
- Scatter: pierderea crește cu aproximativ 15 dB prin triplarea frecvenței și 10 dB pentru fiecare grad de scatter (formula Yeh; în textul extras constanta este de necitit, astfel încât trebuie luată din versiunea W3SZ). Dispersia "sus" din celulele convective acopera de obicei pana la aproximativ 500 km, 1000 km cu statii bune. După ploi abundente și condiții extinse sunt limitate pe toate benzile.

Tipuri de conducte

- Pe pământ: pe pământ se propagă prost, deoarece solul absoarbe şi se răspândeşte; pe marea calmă ajunge departe (Scilly Conductele de evaporare sunt adesea de aproximativ 15 m inaltime si servesc microunde.
- Ridicat: intre 450 si 2000 m, datorita DX mai lung pe teren; semnalul "mare" deasupra statiilor de mai jos. Conductele "pierde" dau semnale mai puţin puternice, dar permit şi staţii intermediare de distanţă.
- Conductele au direcţii precise şi pot dura ore sau zile, contrar E sporadic.

Noi reguli sinoptice pentru index

- Vârsta de anticiclon: aerul scade la centrul de câțiva metri pe oră și inversul este redus pe măsură ce presiunea mare se dezvoltă; deschiderile mai mari sosesc după câteva zile de propagare deja bune. Un anticiclon pe termen scurt nu este suficient.
- Margine de înaltă presiune: stratul este dom și la marginile este mai mic, astfel încât stațiile să se facă mai bine.
- Direcţie: S-a observat că rutele mai lungi merg de-a lungul izobrarilor sau prin dorsale (Flavell G3LTP, 1984), dar există conexiuni excelente, de asemenea, în direcție radială din centru. Exemplul din 29:30 august 1987: din Anglia, deschiderea a rotit 180° în două zile (Spania, Franța, Italia, Austria, Norvegia) ca urmare a schimbării anticiclonului.
- Împotriva: izobare groase (vânt puternic), nori stratocumuliforme la marginea stratului (limita cea mai mare frecvență).
- Fronturi: un front rece împinge aerul cald în sus și creează un revers de-a lungul frontului; cea mai bună direcție este de-a lungul față în timp ce trece, dar durează puțin.
- Sezon: În Marea Britanie anticiclonii sunt mai frecvente la sfârșitul verii și începutul toamnei.
- Indicii vizuale: fumul prins într-o linie întunecată deasupra orizontului; cu presiune ridicată, straturile cumulează cu baza sub 1000 m indică o conductă terestră, baza peste 1000 m o conductă înaltă.

Referințe citate pentru căutare: R. G. Flavell G3LTP, "Studii ale unui Eveniment de Propagare Anticiclonică Extensivă...", *Radio Communication*, February 1984, p. 128; J. Gannaway G3YGF, "Tropospheric Scatter Propagation," *Radio Communication*, September 1981, p. 710; P. W. Sollom G3BGL, "A Little Flutter, "A Little Flutter W6ZGN

## 10. Un model de radio amator verificat: Biancomano și Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, "Practical Tropospheric Model for VHF/UHF Propagation Prediction," *National Weather Digest* 22:2, June 1998, pp. 15](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf)

- Date: radiosundations of the 00 and 12 UTC of 47 stations in the eastern United States, 1995
- Pentru fiecare sondaj: prezența și cota conductei, lungimea de undă maximă care poate conduce (de multe ori aproximativ 100 de ori lungimea de undă), gradientul maxim al temperaturii potențiale între nivelurile învecinate și între un nivel și al doilea de mai sus (pentru straturi mai groase). Conductele terestre nu sunt utilizate.
- Prima regulă, persistența: indicii de stabilitate ( index ridicat, K, totaluri, SWEAT) rămân în termen de 3 unități din studiul cu 12 ore înainte, iar nivelul de presiune în cazul în care temperatura potențială crește mai mult rămâne aproximativ aceeași. Dacă da: ameliorare prognozată în următoarele 12 ore.
- A doua regulă, controale dinamice: nivel limită care nu crește, CAPE, variație a grosimii la 500 hPa pe trei sondaje, variație a pantei în limite.
- Prag bandă: o conductă capabilă să transporte 100 cm sau mai mult indică deschiderea pe 432 MHz, aproape sigură pe 222 și adesea chiar pe 144.
- Filtru spaţial: staţii pe o reţea hexagonală de aproximativ 400 Cu 5 staţii probabilitatea unui fals pozitiv aleator devine neglijabilă.
- Verificarea evenimentelor din coloana "Lumea peste 50 MHz" a QST: în 1996, 48 de evenimente planificate la 67 (71%); 12 din cele 15 evenimente majore. Precizia de până la 70% pe 432 MHz și aproximativ 60% pe 144 MHz la 12 ore; oferă aproape dublul evenimentelor reale; 9 erori din 19 sunt predicții sosite târziu, deoarece multe îmbunătățiri se formează în mai puțin de 12 ore.
- Adăugat la staţiile de nord mai departe: vânt rotativ în sensul acelor de ceasornic alpinism (start fierbinte) ca condiţie suplimentară.

Din PDF complet (suppliat de autorul programului) încă trei detalii. Regula lor "greață = 100 ori lungimea de undă" oferă 600 m pentru 6 m, 200 m pentru 2 m, 125 m pentru 1,25 m și 70 m pentru 70 cm (fig. 3): pentru 70 cm este mai puțin severă decât Kerr și Hepburn (aproximativ 89 m), pentru 2 m coincide cu VHF/UHF DX Book. Vântul care se rotește în sensul acelor de ceasornic se ridică, potrivit autorilor, mai ales pentru că aduce aer cald la altitudine, ceea ce face căderea N să crească cu presiune; umezeala care aduce cu ea nu pare să se destabilizeze. Apendicele obține dN/dh în formă închisă pentru o atmosferă continuă de gradient termic cu un salt ascuțit de gradient de căldură și umiditate la o anumită cotă: util ca un caz de testare pentru codul nostru. Urma lor de pantă a temperaturii potenţiale vine de la K. H. Jehn, "The use of potential refractive index in synoptic scale radio meteorology," *J. Meteorology* 17, 1960, p. 264.

Ce să ia pentru indicele nostru: (1) un termen de persistenţă: comparaţi profilul cu cel de 12 ore înainte, că în prognoza orară avem liber; (2) filtrul de spaţiu: un punct contează numai dacă îmbunătăţirea acoperă o suprafaţă de câteva sute km, care reduce alarme false; (3) indicii de stabilitate, calculaţi de la temperatura şi roua la 850, 700 şi 500 hPa (Open-Meteo oferă deja index ridicat şi CAPE); (4) vântul care se roteşte cu altitudinea între 1000 şi 850 hPa; (5) metoda lor de verificare, care este aceeaşi cu cea pe care dorim să o facem cu reclamele programului.

O altă sursă în același pachet, de puțin folos pentru noi: P. K. Rastogi, "Statosphere and Troposphere (S-T) Studies at Millstone Hill," NASA 1984 (document 19840019069): 440 MHz radar care măsoară turbulențele aerului limpede între 4 și 25 km, cu straturi subțiri legate de unde gravitaționale și de tăierea vântului. Confirmă faptul că turbulenţele care produc dispersia sunt stratificate, dar se referă la cote prea mari pentru conducte.

## 11. A treia rundă de surse (20 noiembrie)

Cazuri istorice de înaltă conductă. J. Macoun OK1VR, 2 m de la summitul Sněžka (1602 m), toamna 1958, articolul din 1959 tradus în 2006 ([ok2kkw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)). În data de 27/28 octombrie 1958, prima conexiune Cehoslovacia Autorul îi atribuie două inversiuni suprapuse, cele superioare fiind de aproximativ 2200 m, semnalul revenind la sol doar în cazul în care stratul inferior s-a dizolvat; condiţie favorizată de un anticiclon lent de la Atlantic la continent. De acasă (Prague) maximul său a fost de 530 km: numai staţiile la altitudine au intrat în conductă. Este un alt caz de verificare (cum ar fi LA0BY 2018) și confirmă faptul că indicele ar trebui să caute să se desfășoare peste 2000 m și să-l compare cu cota stației. G5YV

Biblioteci istorice BNS (utile pentru a căuta lucrări clasice, nu pentru formule):

- W. Nupen, *Bibliography on Tropospheric Propagation of Radio Waves*, BNS Technical Note 304, 1965, 356 pages, with index for topics (de asemenea, "condition provision") and by country. Înregistrările italiene au raportat: L. Sacco, "Conexiunea radio în regimul superrefracţiei atmosferice," * High Frecvence* 24(6), decembrie 1955 (cercetare italiană: super-refracţie sub 500 m frecvent zi şi noapte); S. Silleni, *Annali di Geofisica* 8(1), 1955; J. M. Clara e A. Antinori, prova tra Sardegna e Minorca da 238 a 297 MHz (390 km), *Electrical Communication* 33(2), 1956. A. F. Clarke (Fort Huachuca, 1956) a emis zilnic prognoza lungimii maxime de undă prinsă pentru dimineaţa următoare, de umiditate, temperatură, presiune şi prognoze eoliene.
- A. L. Abbott, *Bibliography of Tropospheric Radio Wave Scatter*, NBS, material până în ianuarie 1960, aproximativ 670 articole comandate pe an: utile pentru partea Troposcatter.

Dezvăluire și observații: F. W. Odenthal DK5EQ, "Ausbreitung 2m Discuție pe VHF Marina și AIS ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)): domeniul de aplicare 1,23·h(ft) mile nautice pe antenă, împrăștiați până la aproximativ 50 nm, îmbunătățire tipică a tropicelor de 100 Chelmsford ARS News, Decembrie 1983: la concursurile din 2 m staţiile de pe coasta engleză de est sunt favorizate de traseul maritim către continent. DM2HB DK5YA VK3DXE EI7GL

ARRL, *The Radio Amateur's VHF Manual*, 1972, cap. 2 "Propagation Beyond the Horizon" (pp. 17 Sfaturi de operare deja cunoscute (urmează zonele de înaltă presiune care se mişcă încet şi deasupra tuturor marginilor spatelui lor; toamna devreme cea mai bună perioadă; zonele de coastă, lacurile mari şi văile râurilor favorizate de stratificare). Două date noi: (1) fig. 2-4 compară atmosfera standard ( umiditate relativă de 70% până la 12000 ft, refracție minimă) cu o rază tipică de refracție puternică, cu un raport clar de rupere a temperaturii și amestec de aproximativ 4000 ft (aproximativ 1200 m): este semnătura pe care calculul nostru trebuie să o recunoască pe nivelurile GFS; (2) conductele adevărate sunt rare sub 144 MHz și cele mai frecvente în UHF, în funcție de grosimea minimă. Bibliografie QST utilă pentru căutare: Hull, "Air-Mass Conditions and the Bending of UHF Waves" (1935), Hoisington, "Painless Prediction of Two-Meter Opens" (1949); Collier, "Upper-Air Conditions for 2-Meter DX" (1955); Botts, "A Night to Remember" (ianuarie 1970, realizat pe uscat).

PA0EHG, pagina "Tropo" ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): rezumat al Troposcatterului din articolul G3YGF, deja în manual. Utilizaţi două reguli practice pentru a controla calculul nostru de scatter: aproximativ 10 dB pierdere suplimentară pentru fiecare unghi la orizontul fiecărei staţii şi peste 100 km aproximativ 9 dB plus fiecare traseu de 100 km (60 mile); volumul comun începe de la aproximativ 600 m pe 100 km şi de la aproximativ 9 km pe 480 km. Nu tratează conductele, sondajele sau pragurile de refracţie.

PA0EHG, PC PROPBER ([pagina](https://pa0ehg.com/propagat1.htm), programul propber.exe oferit de autorul programului). Este un program de 36 kB DOS scris în BASIC și compilat cu Microsoft Basic Compiler 5.60 (1985): rulează doar sub DOS sau DOSBox. Din textele interne: cere frecvența, distanța (sau cele două locatoare, de asemenea, în vechiul format QRA "E-locator"), pentru fiecare stație de înălțime a antenei, figura de zgomot, banda FI, puterea, un obstacol (înălțime și distanță) și câștigul antenei (0 = calcul diametrul parabolei, eficiență 50%). Description Constantele numerice nu pot fi citite în executabilul compilat. Metoda sugerată pentru benzile înalte este, de asemenea, utilă pentru noi: calibrarea calculului pe o legătură măsurată (diferență între S/N măsurată și calculată) și aplicarea aceleiași diferențe la o altă bandă. Este același model al calculului nostru, dar utilizează profilul S RTM1 în loc de un singur obstacol și prognoza GFS în loc de o marjă fixă.

PROPBER, constante citite în executabil (numere în format Microsoft Binary Format). πr, în tabelul de date de lângă texte: 8.68589 (= 20/ln 10, adică 20 de busteni calculați cu logaritmul natural), 32.5 (spațiu liber: 32.5 + 20 log f\[MHz\] + 20 log d\[km\]), 24000 și 0,1 plus 40000 și 0,3 (vapor: 0,1 dB/km de la 24 GHz, 0,3 dB/km de la 40 GHz) Probabil, pentru că repetați, dar în interiorul codului: −69,5 (același termen constant ca formula Scatter laterală a lui Yeh în VHF/UHF DX Book), 332 (refractiv în sol), 8733 km (rază echivalentă, k Formula de scatter exact nu este reconstructiv fără demontarea programului. T0

PROPBER, formule reconstruite prin cod (dezasamblarea piesei compilate; operațiunile sunt chemate la biblioteca de bază cu operatorii în memorie, ale căror valori cu dublă precizie au fost decodate). Constantele −69,5, 332 și 8733 găsite anterior au fost fals pozitive în bibliotecă.

- Distanța optică: d = 5 · ( Este mai generos decât clasicul 4,12 (k = 4/3): este echivalent cu k
- Pierderea opticii: L = 32,5 + 20 log f\[MHz\] + 20 log d\[km\], plus 0,1 dB/km peste 24 GHz și 0,3 dB/km peste 40 GHz.
- Troposcatter: Lts = L + 23 + 10 log f + 0,0674·d, adică 55,5 + 30 log f + 20 log d + 0,0674·d (formula G3YGF cu constanta 2 dB mai mare). Fără termen unghi sau reactivitate.
- Corecție "pentru site-uri": programul cere obstacol și distanță, dar blocul care ar trebui să le folosească este gol; variabila de corecție rămâne 0, astfel încât "corect" pierderea este egal cu cea a scatter.
- Zgomot: T = 290·(10^(NF/10) − 1), N = − 228,6 + 10 log T + 10 log B (dBW), fără temperatura antenei.
- S/N = 10 busteni P + G\_tx − L + G\_rx − N. "Cu propagarea anormală maximă" utilizează pierderea optică în loc de împrăștiere, adică cazul de conductă ideal fără pierderi dincolo de spațiul liber.

Comparație: 300 km și 144 MHz PROPBER oferă aproximativ 189,9 dB, G3YGF 187,9, P.452 187,8 al nostru (cu unghiuri orizont zero): scatter coincide cu 2 dB. Calculul nostru adaugă unghiurile orizontului din profil, temperatura antenei, și în loc de "maxim" fixat stratul așteptat cu împerechere reziduală și atenuare.

Rohde & Schwarz, P. Denisowski, "Înțelegerea propagandei VHF," Notă educațională, aprilie 2023 (24 de pagini, diseminare). Confirmă punctele deja colectate (conduse ca un ghid de undă bidimensional, semnal care scade în mod liniar cu distanța, până la 1500 km și dincolo, durata de la ore la zile; teren bun pe mare și rar pe uscat; ridicat de la câteva sute de metri la câțiva km). Două observații utile pentru indice: (1) evoluția în timp: o conductă nou formată poartă adesea doar cele mai înalte frecvențe și, îngroșându-se, începe să aducă chiar și cele mai mici; prognoza orară ar trebui, prin urmare, să indice 432 MHz "în avans" pe 144 MHz atunci când stratul crește; (2) amplasarea stațiilor: cele mai bune condiții sunt între stațiile aflate la capătul conductei, în timp ce cele de-a lungul trasei unei conducte înalte nu reușesc să intre, cu excepția cazului în care acestea sunt efectuate "care pierd." Conductele se mişcă cu vreme atmosferică.

Site-ul PA0EHG, alte pagini. Pentru Tropo, PathProfile of M. Willis G0MJW ([scheda](https://pa0ehg.com/srtmp.htm), [download](http://www.mike-willis.com/Software/ASTERPathprofile.zip)): profil al traseului din fișierele totale de pierdere și cotă preconizate, considerate fiabile pentru Troposcatter în cuptorul cu microunde; nu tratează dispersarea din avioane sau ploaie. Util ca o a doua referință pentru a compara profilurile noastre S RTM1 și pierderi. Restul site-ului acoperă EME, cuptor cu microunde, pistă de aterizare (AirScout, hărți SM7LCB) și măsuri în 80 m.

PathProfile by G0MJW (Mike Willis), STER and SRTM Packages and SiteDate (supplied by the author of the program; [](https://www.mike-willis.com/software.html)software page). Programul Windows din 2018: profil de la STER sau SRTM la 1" sau 3," pierderi de optica, difractie, troposcat (variind pana la 1000 km), conducta si gazoasa, cu procent de timp, k mediana traseului, fractiunea de cale pe mare si distanta de coasta: structura este cea a P.452/P.1812, dar ajutorul (fisierul.chm comprimat) nu a putut fi citit aici. SiteData convertește locatorii și oferă cota de la GT OPO30.

Partea cea mai utilă este fişierele de date incluse: UIT-R P.453 hărţi statistice pe reţeaua globală de 1,5° (121 rânduri de la 90°N la 90°S, 241 coloane de la 0° la est): gradient dN/dz în primele 65 m depăşite pentru 1, 10, 50, 90 şi 99% din timp; pentru conductele de sol procentul de prezenţă, grosime şi intensitate; pentru prezenţa ridicată, cota de bază, grosimea, intensitatea şi altitudinea maximă; plus termenul umed şi temperatura. Valori anuale interpolate:

| Locație | Conducta de teren: % / grosime / intensitate | Conductă mare: % / bază / grosime / intensitate | dN/dz depășește 10% din timp |
| Napoli |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | −380 N/km |
| Tirenian (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | −674 N/km |
| Trapani |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | −520 N/km |
| Brindisi |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | − 405 N/km |
| Marea Adriatică (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | −399 N/km |
| Milano |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | −172 N/km |
| Udine |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | −176 N/km |

Pentru indicele nostru sunt o referință climatică gata: grosimea și intensitatea tipice conductelor în loc de 50 m fixat de canalul potențial, și o anomalie "în comparație cu normal" pe zonă. Date UIT: gratuit pentru utilizare necomercială, pentru a verifica înainte de a le include într-o versiune publică. Fișierul ZO NES01.DEM (58 MB, 2' grid) conține zonele climatice interioare/de coastă/de mare; formatul nu este documentat și nu l-am decodat. Pagina ecuaţiilor lui Willis conţine doar constante fizice, spaţiu liber şi câştig de antenă.

A patra rundă de surse (20 noiembrie).

Indermuehle și Suzuki 2026, predicția conductelor GFS pentru Observatorul CSIRO Murchison (arXiv 2608.12861): este cea mai apropiată sursă de LIVETROPOVHF. Profilurile N de la GFS la 0,25° (suprafață la 2 m, apoi nivelurile izobarice, cele de sub solul aruncat); pentru fiecare coloană minimă de dM/dz sub 3 km, baza și partea superioară a conductei, deficitul M integrat; apoi P.452-16 și P.1812-6 cu trasee GLO-90 25 de la 98 la 1005 km, de la 88 MHz la 2,68 GHz, în fiecare oră. Rezultate: ASC mediană 0,72 pe 25 de canale; procentul de timp P.452 p trebuie utilizat ca o constantă de calibrare (alegere p = 20%), deoarece modelul UIT cu ΔN instant nu este definit într-o conductă; abilitatea rămâne aproape plată până la 3 zile în avans. La 162 MHz (AIS), rutele primite au cea mai mare fracțiune de conductă de 67% (0,26 versus 0,16) și cea mai mare adâncime a conductei de 49% (28 față de 19 M) în ceea ce privește controalele: pe mare conducta este aproape întotdeauna, decide întinderea terenului. GFS vede jumătate din timpul petrecut la sol în comparație cu ERA5 (4,5% față de 10,4% din celule-oră), în timp ce pe mare vede mai mult (73% față de 56%). Un indice "coridor" (conducte continue de-a lungul traseului, straturi cu o grosime minimă de 40 m) îmbunătățește rezultatul: confirmă că continuitatea de-a lungul traseului este criteriul corect.

Longley și Rice, ESSA ERL 79-ITS 67 (1968): metoda pentru teren neregulat de la 20 MHz la 20 GHz (după ITM), cu nereguli în sol Δh, Ns de la 250 la 400, raza echivalentă 8497 km (4/3), orizonturi, difracție, împrăștiere înainte și variabilitate în timp și în locuri pentru climă. Referinţă statistică, nu instantanee.

UIT-R P.834-2: rază de curbură, raza Pământului echivalent k·a, M = N + h/a (h în m, în mii de km), model exponențial; nimic nou decât ceea ce a fost deja utilizat.

AGARD CP-70 (1970), propagarea tropopherică: straturi subţiri de la 1 la 100 m cu gradienţi puternici sunt comune; coeficientul de reflexie al unui strat înalt este aproximativ proporţional cu lungimea de undă, cu o dispersie aleatorie de aproximativ 14 dB. Măsuri la 90 MHz: scădere de 9 Trecerea unui front rece elimină straturile: Difracţia rămâne.

Raportul U.S. URSI, Comisia 2 (1960): grosimea medie a straturilor înalte de 60-210 m (având în vedere P.453), gradientul minim pentru conducta de 157-160 N/km.

Buscemi K2OVS, ghid de propagare VHF: distanțe tipice pentru inversare și conductă pentru bandă, conducte legate de presiune stationară ridicată, maxim în zori.

Wolinsky-Mancini și 2024, AIS în Insulele Canare: Inundații și recepții radio AIS; conductele sunt mult mai frecvente în lunile calde (versiunea aliseenilor).

Articole nigeriene și malaysiene (Ekah, Iloke, Mat, Igwe): corelații locale între temperatură, umiditate și pierderea măsurată, slabe și contradictorii (r - 0,09 - 0,27); utile doar ca confirmare că dimensiunile sol singur nu sunt suficiente.

Herben 1984, teza TU Eindhoven (microonde): 30 GHz pe 8,2 km și satelit OTS la 11,6 GHz; strălucire pentru turbulențe, cursuri multiple și ploaie. Pentru microunde pierderile dinamice sunt dominate de ploaie și turbulențe, nu de conducte.

Baars, The Paraboloidal Reflector Antenna (microonde): câștigul unei parabole G = η· (πD/λ) 2 cu eficiență tipică 0,5 Ar ajuta dacă am adăuga parabole ca alternativă la Yagi DL6WU pentru 1296 MHz și dincolo.

Analizat deja sau cu un conţinut slab: Hirschmann NASA X-731-67-89 (dublu), radarul VHF ST al lui Reid (vânturi şi turbulenţe, neconductate), alunecarea Scribd şi notiţele generice ale troposferei.

A cincea rundă de surse (20 noiembrie, seara).

Rogers 1997, Radio Science 32 (1): Estimarea Bayesiană a parametrilor unei conducte înalte (rata de bază, deficitul M, grosimea) din factorii de propagare măsurați dincolo de orizont, cu un model de ecuație parabolică; experiment COVAR în California la 143, 263 și 375 MHz, comparație cu unde radio. Semnalul depinde mult de bază și de deficitul M (corecționând 0,81 până la 143 MHz pentru deficit), aproape deloc de grosimea (−0,17): grosimea nu poate fi obținută din semnal. Pentru noi: calculul B (grosimea climei) contează mai puțin decât deficitul și cota; și metoda este baza pentru biroul din spate, care este de a obține conducta de la frecvențe multiple.

Moler and Holden 1960, "Tropospheric scatter propagation and atmospheric circulations," J. Res. BNS 64D, pp. 81 și următoarele (original on 22/11/2026, doi 10.6028/jres.064d.014). Trasee maritime California la Point Loma (San Diego): Santa Barbara 190 mile, Point Mugu 144, San Clemente 78; 1365 MHz și 9380 MHz, nu VHF; ianuarie Cazul din 26 martie: o întărire de aproximativ 20 dB pentru aproximativ 3 ore spre prânz, atunci când un vortex ciclonic slăbește și începe briza mării și subsidence; la 15 convergența este reformată și semnalul scade cu aproximativ 22 dB de la maxim. Statistici (Tabelul 1, 500 hPa, 33 cazuri): cu aer care scade semnalul mediu este mai mare de aproximativ 2,5 dB decât cu aer în creștere, în banda L și X. Semnalul urmează diferența calculată la 500 hPa în semnul și amplitudinea variațiilor, fără un coeficient de corelare. Straturile joase (sub aproximativ 600 m) favorizează reflecţia parţială asupra împrăştierii. Anticiclonii și fronturile nu sunt tratate, iar "excursiile de până la 60 dB" menționate mai sus nu sunt: provin dintr-un rezumat la mâna a doua. Pentru noi: mișcarea verticală este un predictor bun, și +1,5 dB pentru subsidență și −2 dB pentru aerul de sus sunt de aceeași ordine ca diferența medie măsurată (2,5 dB); 20 dBs sunt un singur caz, nu o valoare tipică, și pe cuptorul cu microunde.

Banafaa și Muqaibel 2025, IEEE Acces: studiu al conductelor (modele de evacuare, ecuație parabolică, estimări radar, GNSS și lidar) și clasificare cu învățare automată pe măsuri reale; Masini vectoriale de sprijin oferă cele mai bune rezultate. Util ca bibliografie; Datele sunt din Golful Persic.

UIT-R P.834-7 (2015): versiunea actualizată a recomandării privind refracția sălbatică tropos; nicio formulă nouă decât cea deja utilizată.

Abbott, BNS bibliografie pe scatter troposferous (GOVPUB- C13): deja în runda a treia. Slide Scribd pe conducte: conținut generic. Articolul ScienceDirect 0021919698790030 nu a putut fi deschis.

A șasea rundă (20 noiembrie, noapte): Bean și Dutton, ARRL Manual 2022 și 2023, Balanis.

Bean și Dutton, Radio Meteorologie (cap. 6), părți neutilizate încă. Sensibilitatea câmpului la Ns: 0,18 dB per unitate N pe ciclul anual; 0,14. Parametrul compus din Misme: M = a(g\_e − 40 + b· Indicele potenţial de refracţie K (N raportat adiabatic la 1000 hPa): evidenţiază masele de aer şi subsidenţa anticiclonică (Flavell şi Lane).

Manualul ARRN 2022 (99a ed.), A se vedea nota de subsol 1. Inversiuni utile între 250 și 2000 m; inversii ale radiațiilor (seră, aer calm, sol uscat) rareori duc la adevăr; anticiclonele lente dau cele mai extinse conducte per subsidență, cu inversii între 500 și 3000 m, mai puternice seara și dimineața; în emisfera nordică cele mai bune rute sunt la sud de centrul înalt. Front fierbinte: condiții bune cu câteva sute de km înainte; front rece: puternic, dar instabil invers în spatele frontului, paralel. Foehn (de asemenea, în Alpi) și briza terestră (aer rece pe sol, la cald la 200 Scirocco: aer cald și uscat din Sahara peste aer marin rece, inversii foarte puternice de la estul Mediteranei la Gibraltar, cauza probabilă a înregistrărilor peste 1500 km în cuptorul cu microunde. Troposcatter: limita teoretica aproximativ 800 km, jumatate maxim tipica. Cea de-a 100-a ediție (2023) are un text mai scurt: inversiuni la răsărit și apus de soare până la +20 dB și +20izare50% debit; 1200 de mile pe Marile Lacuri, tipic 400

Balanis, Antena Theory (4a ed.), § 10.3.3: Masa Viezbicke (NBS TN 688, 1976) pentru Yagi optimizată la 400 MHz: boom-ul 0,4 / 0,8 / 1.2 / 2.2 / 3.2 / 4.2 λ → 7.1 / 9,2 / 10.2 / 12.25 / 13,4 / 14.2 dBd. Formula noastră DL6WU (8.6 + 8 log10 boom/λ) oferă o valoare mai mică între 0,8 și 4.2 λ și revine în conformitate cu tabelele DL6WU la 9.5 λ: estimare prudentă. Cu formula corectă pe original (9,4 + 7,8 log10 boom/λ, de la 22 noiembrie), aruncarea înapoi în mare scade sub 0,2 dB între 1,2 și 4,2 λ (12,1 dBd la 2,2 λ față de 12,25) și rămâne 0,6 dB la 0,8 λ.

Runda a șaptea (21 noiembrie): QST, QEX, W3SZ, GitHub depozit.

QST 2000 Doar rapoartele privind Lumea de peste 50 MHz, utile ca cazuri de validare a modelului: 5 septembrie 2009, 2000, efectuate la 144 MHz și dincolo de jumătatea estică a SUA; 14; 15 august 2022, deschiderea Manitoba QST noiembrie 2022 recomandă hărți Hepburn. Niciun articol tehnic despre prognoza tropo. FT8 VE4MA N0OY Q65

QEX și K1JT, Franke și Taylor (QEX 2020) pe moduri FT4/FT8: Praguri de decodare FT8 −20,8 dB în 2500 Hz pe canal fără evanescență, −18,6 dB cu evascență la latitudinea medie; FT4 −17,5 dB. Celelalte numere de QEX 2023 și 2026 nu tratează Tropo (trei numere 2023 sunt scanări necitite). W3SZ 2019: praguri de mod lent ( JT4A −23, JT9A −27, JT65A −25, QRA64A −26, FT8 −21 dB) și criterii de alegere între sensibilitatea și toleranța la schimbarea frecvenței.

GitHub. TropoLink (LukiBox, Apache-2.0): proiectarea de trasee Troposcatter cu P.617-5 (cu N0 și ΔN), BNS TN101 și ITM flancate, iar diferența lor a fost demonstrată ca incertitudine; P.617-5: L = F + 22 log f + 35 log θ + 17 log d + Lc, F = 0,18· N0 ·e^(−hs/7,35) − 0,23· andi-f/troposcatter ( DL2FZN 2013, de la VHF/UHF Handbook RSGB): Lts = 24,44 + 4,283 ln f + 0,06656 d deasupra spațiului liber, panta lângă PROPBER (0,0674 dB/km). QRadioPredict: ITM și ITWOM 3.0 cu S RTM3, utilizarea solului Corins și diagrame NEC. Nu este relevant: Tropos (Weather app), opera-adt/tropo și troposam (Troposferic tangence for InSAR), tgbot\_troposcatter (military station), sdr-troposcatter-gui (control panel), Troposcatter-Propagation-Delay ( ERA5 delay), radio-comunication-Project (diactic simulator), PyLayers (canal).

Al optulea tur (21 noiembrie): modele NTIA și ITU pe GitHub. NTIA/itm (Longley-Rice, STI): 20 Nu modelează conductele: serveşte ca "atmosfera normală de referinţă" cu variabilitate statistică. NTIA/p528 și eeveetza/p528 (MATLAB): UIT-R P.528-5 pentru conexiunile aer-sol, 100 Posibilă utilizare: echilibru de împrăștiere a aerului (două trasee de la sol; aeronave plus secțiunea radar). eeveetza/p2001 (MTALAB): UIT-R P.2001-6, model de câmp cu difracție, troposcatter, propagare anormală (conductă și straturi, Lba) și E sporadic, procent de timp 0 Posibila utilizare ca comparaţie a UIT în loc de P.452, cu GFS ΔN în loc de hărţi ca în studiul CSIRO; hărţile trebuie descărcate de către utilizator conform P.453. Validarea 1.3.0 din 593 QSO (29/12/202/022/1/2022): separarea de evenimente/comparison 0,79 fără corecții, 0,91 cu Misme și subsidence; GFS nu vede conducta de pe mare la 144 MHz (guadagno fără corecții niciodată mai mult de 15 dB). N0

Runda a noua (21 noiembrie): IU3QEZ, NTIA, PyWaveProp, ACE High, validare 1.3.1. IU3QEZ (S. Fabris), prezentare "Peste orizont în VHF" (GEKO Magazine n. 69, septembrie 2026): volum comun d2/(8·R\_eff) cu R\_eff 8500 km (1,3 km până la 300 km, 3,7 km până la 500 km fiecare nivel de orizont în valoare de 69 mile (111 km), marjă de 7 dB pentru evanescență și 4 dB pentru reflexiea solului; Teza: 10 dB este de aproximativ 100 km; stația IQ3QC 5 W cu 3×5 elemente, 120 QSO, maxim 788 km (Alpe Adria 2026). Bibliografie a prezentării care urmează să fie recuperată: Norton 1959 (J. Res. BNS 63D, System Loss), Bray 1961, ARRL Antena Book 2023 fig. 4.29, Rice-Longley-Norton-Barsis NBS TN 101, Bean și Thayer 1959, Barsis și Kirby 1961 (diffration on ridge), Deygout 1966, P.1812, AirScout DL2ALF, P.834 NTIA PropLib (wiki): eHata, ITM, LFMF, P.528, P.2108 (loss from vulgaris), cu Czz, P.NET, MATLAB și Python; fără model de conductă; pagina STI: IF-77 (airsol) și MPM (1 GHz1 THz). PyWaveProp (Python): Metode de ecuație parabolică cu aproximații Padé, difracție multiplă a marginilor, incinte PETOOL și RAM, conducte troposferoase și înalte, sol neregulat: posibilă bază pentru un calcul de câmp cu profilul GFS M(z, care trebuie verificat. APRS PropView (Apache 2. 0): Hărți de propagare VHF din pachetele APRS, statistici pe oră și alarme de deschidere; posibila sursă de date de 144 MHz pentru validare. Reţeaua Troposcatter NATO în Italia (fortificaţii.net): 900 MHz, 10 kW, 20 m parabole; staţii incluzând Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Vulture, Monte Lauro; caz de test istoric pentru P.617. Nu este relevant: Gupta 2021 (transport stratosferic), Legea 2023 (Zgozon arctic), delfinul #590 și opera-adt/tropo (întârziere InSAR), tropo.ino (senzori de gaz Arduino), tropo-webapi (telefonia), cloudtools/troposphere (AWS). Validarea 1.3.1 pe 564 Tropo QSO: ASC 0,79 (model 1.2), 0,80 (+ conductă moale), 0,90 (+ misme și subsidence), 0,90 (+ persistență); culori de conductă (≥ 16 dB) pe 46% din rutele de deschidere și 0% din comparație.

Adaugat la aceeași rundă. mikelytaev/wave-propagation (MIT, Python 3): Ecuație parabolică cu aproximații de Padé și Numerov, sol neregulat, conducte troposferice; încărcați M refractiv modificat (contingent, lat, ln) din NOAA GFS și setați o conductă de evaporare a Monin-Obukhov; nicio funcție documentată care întoarce pierderea pe cale. El este cel mai bun candidat pentru un viitor calcul de teren cu profilul GFS M (ar servi un serviciu extern sau o traducere). RP-Sim (AGPL-3.0, JavaScript): Modele empirice pentru rețele radio (Hata, He, Ericsson, două raze), fără tropo. RIMFading (GPL-3.0, OMNeT++): nereguli ale fluxului pentru simulările de rețea, distribuția Weibull, fără tropo. PyLayers: canal radio interior, deja exclus. qradiopredict (kantooon, GPL v2): ITM și ITWOM v3.0 cu S RTM3 și Corine, deja în runda a șaptea ca QRadioPredict.

Runda a zecea (21 noiembrie): Antenna Book, Norton, Barsis, noi surse de date. ARRL Antena Book (23a ed.), cap. 4 ( K9LA): Metoda Bray (QST noiembrie 1961) pentru gama VHF: pierdere netedă a Pământului la 99% (300 mile, 144 MHz: 214 dB), curbe la 50% și 99% (fig. 4.11): 195 dB în valoare de 100 mile până la 99% și peste 250 mile până la 50%; evanescență ±7 dB peste 100 mile (−3,5 dB până la 50 mile), SSB 3 dB mai rău decât CW, +4 dB de reflexie la sol, +3 dB pentru cuplarea a două antene; creștere a înălțimii (15 m: 4/3/2 dB la 100 km; 24 m: 8/6/4 dB); Yagi câștigă de aproximativ 10 ori mai mult decât boom-ul în λ (dBi); 50 W stație cu antenă medie: aproximativ 180 dB, 100 mile, +10 dB până la 250 mile. Răsărirea soarelui și apusul de soare până la +20 dB și +20 Verificarea Troposcatter-ului nostru: P.452 până la 50% la 483 km și 144 MHz oferă aproximativ 204 dB, în concordanță cu 214 dB de Bray la 99%. Norton 1959 (J. Res. BNS 63D, liber pe NIST): pierdere de bază, sistem și câștig antenă cale; Atmosfera de referință Ns = 301; raza echivalentă de la Ns (250 → 4878 mile, 301 → 5280, 400 → 6997); L(99) tabele cu antene de 30 de picioare (100 MHz: 163 dB până la 100 mile, 208 dB la 500 mile; 1000 MHz: 138 și 188 dB cu parabole de 28 picioare); variabilitate pe termen lung V (p, θ) în funcție de distanța unghiului (fig. 13). Rice, Longley and Norton, NBS TN 15 (1959), liber pe NIST. Barsis and Kirby, BNS Report 6751 (1961): 223 km în Colorado with Pikes Peak (4292 m) as edge, 100 and 751 MHz; consistent loves with the roundd edge (hum 11,5 km radius); Long-time evanescence 11,7 ARRL VHF Digital Handbook: pachet, APRS, D-STAR, WSJT (Scatter Method and EME), no part on the tropo. Noi surse de date dovedite sau căutate: ECMWF date deschise (IFS 0,25°, nivelurile 1000, 925, 850, 700 hPa..., fără 975, 950, 900 și 800; G RIB2 cu compresie CSCDS, pe care cititorul nostru nu le decodează; deja disponibil prin Open-Meteo); DWD ICON-UE date deschise (G RIB2 comprimate bz2, un fișier pentru variabile și nivel: multe cereri); Canada GDPS pe MSC Datamart și GeoMet (aproximativ 15 km, până la 33 nivele; GeoMet permite cultura zonei; acesta este baza hărților Hepburn); arhiva GFS NOAAA pe AWS din februarie 2021 (fișiere globale, utile pentru validare); APRS PropView numai pentru validare.

Runda a unsprezecea (21 noiembrie): Longley-Rice, GEKO 69. Bean şi Thayer, "Modelele Indexului Atmosferic Radio Refractiv," Proc. IRE mai 1959 (pp. 740 şi următoarele): modelul 4/3 are panta dreaptă în primul km, dar cade prea sus; 30% din curbura unei raze are loc în primele 100 m și 60% în primul km; relația medie dintre Ns și scăderea în primul km: − Bean and Thayer, "C.R.P.L. Exponențial Reference Atmosphere," J. Res. BNS 63D n. 3 (1959): N(h) = Ns·e^(−c·h) cu c fixat de Ns prin același raport; Medie Ns USA 313; tabele de urmărire a fasciculelor (curbe totale 15.2 mrad cu model exponențial față de 30.3 mrad cu Pământ 4/3). MATLAB refractionexp: c = 0,143859 km-1 pentru Ns = 313 (scale cca 6,95 km), valabil peste aproximativ 1 km și 10 msec. Utilizarea pentru noi: profilul "normal" de comparație poate fi că CRPL cu Ns locale în loc de M = 315 + 0,118·z fix, și anomalia scăderii în primul km ar trebui să fie măsurată în comparație cu −7,32·e^(0,005577·Ns) în loc de −40 fix. Raportul NTIA 82-100 (Hufford, Longley, Kissick 1982), ghidul ITM în modul de zonă: climi și Ns sugerate (ecuatorial 360, continental subtropical 320, marin subtropical 370, desert 280, temperat continental 301, marin temperat pe uscat 320, marin temperat pe mare 350); Ns = N0 ·e^ (hs/9,46 km); utilizați minimul lunar de Ns pentru că mai puțin influențat de super-refracție și sub-refracție; Ns = 301 corespunde ae = 8497 km; sub 100 km Ns contează puțin. Rice, Longley şi Norton, BNS TN 15 (1959), partea I: prognozarea formulei de distribuţie în timp pe teren şi val troposferic, baza TN 101. SoftWright, Wikipedia, Altair WinProp și MATLAB rfprop. LongleyRice: aceleași informații (versiunea 1.2.2, 20 GEKO Magazine n. 69 ( IU3QEZ): orizont radio 4/3 de 130 km de la 1000 m, 260 km adăugând două staţii; troposcatre cu volum comun între 1 şi 3 km şi 10 km; împrăştiere de la aeronavă la 10 km cu +10

## 12. Tabel rotund: surse pentru validarea modelului sau corectarea acestuia (21 noiembrie)

Sinteza tuturor surselor, comandate pentru utilizare. "State" spune că sursa este deja în program (1.3.1), fie că este în test, fie că este de făcut.

### Date de validat (ce s-a întâmplat cu adevărat)

| Sursă | Ce dă | Utilizare | Stat |
| MMMonVHF raportează de la autor | 593 QSO la 144 MHz, 29/12/202/022/1/2022, timp și locatori | Validarea evenimentului față de comparație (+14 zile) | în uz: ASC 0,79 (1,2) → 0,90 (1.3.1) |
| Lumea de peste 50 MHz | cazuri documentate Manitoba: | caz istoric | utilizat ca caz istoric; care urmează să fie adăugat la QSO-urile de validare |
| wspr.live (WSPR) | arhiva completa, 2 m si 70 cm, SQL | multe evenimente și multe "nu" (ore fără spot) | încercat, ar trebui să fie filtrat (la fața locului fals la 16 |
| APRS PropView / APRS-IS | staţii simţite, distanţe, ore | numai validare 144 MHz | De făcut |
| Reverse Beacon (arhivă) | loc zilnic | VHF | aruncat deocamdată |
| I GRA2 radiosondaggi (Brindisi 16320, Practica Mare 16245, Trapani 16429, Udine, Milano) | profile măsurate la 00 și 12 UTC | Controlul profilului GFS și inversiuni | fișierele găsite (Brindisi 46 MB), care urmează să fie utilizate |
| Barsis and Kirby 1961, Barsis 1963 | Evanescența măsurată pe căi cu obstacole montane | validează categoria "monte" (Vesuvio, Faito) | De făcut |
| Norton 1959, BNS TN 15, TN 101 | L(99) și pierderi medii de masă | Controlul împrăștierii | parţial (a se vedea 300 mile consistent) |

### Corecții sugerate de surse

| Corecție | Surse | Efectul măsurat la validare | Stat |
| Stabilitate greșită Termen | Bean şi Dutton. 6 | ASC 0,79 → 0,90 împreună cu subsidence | la punctul 1.3.1 (titlul 16 dB fără conductă) |
| Subsidence from vertical motion | Moler și Holden 1960 (citit în original pe 22/11: +2.5 dB medie între coborârea aerului și creșterea aerului, 1,4 și 9,4 GHz pe mare) | vezi mai sus | în 1.3.1 (+1,5 / −2 dB) |
| Conductă moale (λ până la 3·λmax) | Kerr, Hepburn, VHF/UHF DX Book, Shi 2019 | monoterapie +1% din ASC; aduce culorile conductei pe 46% din traseele evenimentului şi 0% din comparaţie | în |
| Persistenţă 24 h | VHF/UHF DX Book, Biancomano și Leyshon | ASC nemodificată, mai luată şi mai multe alarme false | la punctul 1.3.1 |
| Profil normal CRPL cu N-uri locale (− | Bean and Thayer 1959, BNS Monograph 4 | testat în validare | la 1.3.2 ca alternativă, din cauza defectului |
| Prag de superrefracție 79 N/km (ITU) în loc de 100 (Bean și Dutton, strat 1000/850) | P.453, Lopez 2008, DX Book | testat în validare | la punctul 1.3.2: estimare agresivă alături de prudență (pe hartă se folosește prudent) |
| Grosimea climatologică (calcula B) mai puțin importantă decât deficitul | Rogers 1997 | pentru a încerca: utilizaţi A pentru câştig | De făcut |
| Ecuație parabolică în profilul GFS | PETOOL, unde-propagare (Lytaev), Levy (carte nu citit, doar previzualizare), Lindquist | prima versiune cu câmp de-a lungul orizontului dominat de zgomot numeric (ASC 0,12 | utilizat ca al doilea calcul, nu decide prognoza |
| Evanescența: ±7 dB peste 100 de mile, marjă 10 | Bray 1961, Antenna Book, GEKO 69 | pragurile de cale în condiții marginale | în uz: probabilitate pentru perioadă și prag FT8 −18,6 dB în troposcatter |
| Volumul comun și distanța echivalentă de Bray | IU3QEZ, Bray 1961 | Informații | în |
| Troposcatter P.617 și RGB ca incertitudine | TropoLink, DL2FZN | Informații | în 1, 3. 0 |

### Ce rămâne din modele

Desfăşurate pe mare la altitudine mică (100 ITM/Longley-Rice rămâne o referință statistică "zi normală" (Ns 301, climi), fără conducte. Eliminat prin decizia autorului: AirScout, P.528), E sporadic (P.2001) şi 50 MHz (servoir EIRP care puţini au şi confundă cu E sporadic).

### Propunere de ordin de lucru

1. Validare extinsă: se adaugă WSPR filtrat (chiar și ore fără spot) și QST 2022; se repetă pentru 432 MHz.
2. Încercați profilul normal CRPL și pragul UIT de 79 N/km, unul la un moment dat, păstrând doar ceea ce ridică ASC.
3. Compararea profilurilor GFS cu valul radio I GRA2 de Brindisi și Practica Mării în zilele QSO.
4. Reevaluează ecuaţia parabolică după corecţia numerică.

### Deciziile din 21 noiembrie (versiunea 1.3.2)

- Adoptată: estimare prudentă (Bean and Dutton, 100 N/km) și agresivă (ITU, 79 N/km) flancată; conducte căutate numai sub 2500 m (primul 1500 m, ridicat după validare); bonus cu presiune mare extinsă și vânt slab; greutate mare cu μ1 de P.452; cota stațiilor în ceea ce privește conducta, de asemenea, pe conducta parțială; strat limită deasupra crestelor ( IT9GSF).
- Adoptat ca alternativă, oprit prin defect: profilul normal CRPL cu Ns locale.
- Ecuație parabolică corectă numeric: ASC de la 0,12 la 0,54 (30 m antenă) și de la 0,28 la 0,66 (300 m).
- Teren: Copernic DEM GLO-30 în loc de Terrain Tiles IC8TEM
- Semnele meteorologice de pe hartă cu efectul lor: ↓ subsidence favorabil (Moler și Holden 1960, în medie aproximativ 2,5 dB între aer merge în jos și aer merge în sus; +1.5 dB), ↑ aer de sus defavorabil (în funcție de adâncime; −2 dB), S scirocco indiciu favorabil (Manualul ARRL § 19.4, inversii foarte puternic pe Marea Mediterană; nici o corecție). Simbol dublu, culori verzi/roșu/portocaliu și benzi desenate pe hartă.
- A adoptat ITM 1.2.2 (NTIA/itm, domeniu public), tradus în C#: în conformitate cu calculul valorii de referință în atmosferă normală lângă P.452 și după ieșirea dintr-o conductă care se termină înainte de corespondent. Codul previzional qradio aruncat deoarece sub GPL v2.

Fără conținut util pentru index: *Comunications Quarterly*, toamna 1991 (nici un articol despre tropo; articolul lui Atkins despre Scatter este iarna 1991).

- Adoptat ca principiu al manualelor (21 noiembrie): "Producţia nu poate fi generalizată, ci studiată în legătură cu morfologia locală" ( DF5AI, raportată de IK3XTV în "Propagarea pe 2 metri"); citată în manualul de prognoză (§ 1) şi în consilii.
- Adoptată (21 noiembrie, după 1.3.3) interpretarea deschiderilor transalpine ale IK3XTV: nu "celule convective, cum ar fi lentilele," ci troposcată întărită de turbulențe în partea de sus a stratului reactivat atunci când ajunge la cota volumului comun (manual al previziunilor, § 8 ter). De la 1.3.4 intră în calculul punctului ca o consolidare provizorie a troposcatrului, cel mult 3 dB. Abandonat de aceleaşi articole: "auto-amplificarea" semnalelor dincolo de Alpi (un mediu pasiv nu amplifică: legăturile descrise rămân explicabile cu difracţie şi troposcat; puterile declarate nu sunt utilizate ca date), zăpada care "îmbunătăţeşte difracţia" (cel mai probabil stratul stabil peste zăpadă, cu mai puţină evanescenţă).

- Kerr (1951) revăzut pe cartea (22 noiembrie, PDF furnizată de autor): § 1.5, eq. 17 și 18 și tabelul 1.1. Cifrele de Eq. 18 în scanare sunt de necitit, dar tabelul 1.1 confirmă: 20 ft → 1 cm, 80 ft → 10 cm, 400 ft → 100 cm, 2000 ft → 1000 cm, față de 1,25, 10, 112 și 1252 cm λmax = 0,014/d ^1,5 (λ în cm, d în picioare; tabelul rotund, "valori indicative." Ipotezele lui Kerr: în interiorul conductei picăturile de index modificate de 4·10−8 pe picior, valoarea tipică a conductelor joase; cu gradienții mai slabi este nevoie de o grosime mai mare. Kerr avertizează, de asemenea, că λmax nu este o tăietură netă: lungimile de undă uneori mai lungi pot fi influenţate puternic de conductă.

- DL6WU revăzut pe original (22 noiembrie, PDF al VHF Communications 3/1982, furnizate de autor): G. Hoch, Fig. 1: câștig pe dipolul măsurat la 432 și 1296 MHz într-un poligon profesionist al Posturilor Germane, cu o antenă de probă EIA de la 7,7 dBd, precizie estimată 0,5 dB; 2,35 dB pentru lungimea dublă a boom-ului, fără saturație până la aproximativ 20 λ. Programul a folosit 8.6 + 8.0.log10 (L/λ) dBd, adică aproximativ 0,8 dB în mai puțin de 1 A aruncat formula 10·log10 (7,8·L/λ) circulată pe net: ar oferi 3 dB pentru dublarea, față de 2,35 măsurată.
- Booker şi Gordon (1950): referinţă confirmată de "This Week's Citation Classic" (Conţinut curent, 8 februarie 1982, comentariu de H. G. Booker): Proc. IRE 38, pp. [43]412. Comentariul confirmă cele două puncte utilizate în manuale: câmpul primit dincolo de orizont cu distribuția Rayleigh, și funcția exponențială de autocorelație a neregulilor (non-Gaussian). Articolul original nu a fost citit.

## 13. De la articolele IK3XTV

Cele două articole ale lui Flavio Egano IK3XTV (Radio Rivista: "Propagare pe 2 metri" și "Propagation VHF Acestea sunt observații de la stație, nu măsuri: acestea trebuie tratate ca ipoteze pentru a verifica cu pete.

| Comentariile autorului | Cum devine un ingredient al indicelui |
| Conductele sunt formate în principal între 100 și 1000 m, rareori mai mari; servi presiune ridicată pe o zonă vastă, insolație bună și absența vântului | căutați canalul numai sub 1500 m; bonus cu presiune mare și vânt slab în straturile joase |
| În lunile calde efectuate recurente pe Marea Adriatică între Veneto și Puglia, 700 | greutate mai mare pentru fracțiunea de cale maritimă (cum ar fi μ1 de UIT-R P.452) |
| Cel corespunzător trebuie să fie în sau lângă conductă; cu antene înalte și câțiva metri cu plumb semnalul nu intră | Comparați cota bazei conductei cu cota celor două stații (va servi la altitudinea QTH) |
| În același timp, aceeași conductă: ascultați de Gargano o stație în 144 și una în 50 MHz | pentru a verifica: în conformitate cu tabelul Hepburn, 50 MHz solicită peste 300 m plumb, astfel încât evenimentul ar indica o conductă foarte des |
| Conexiuni | Indice "convectiv" separat de conducta, pentru traseele montane |
| Celulele convective se nasc la sol şi se ridică la aproximativ 1 km la fiecare 3 ore: la 15 km ajung la aproximativ 3 km, altitudinea care serveşte la suprascrierea Alpilor | Utilizați înălțimea stratului limită (legatar\_strat\_înălțime de Open-Meteo GFS) de-a lungul traseului: scor atunci când depășește altitudinea crestelor |
| Alpii generează unde gravitaționale care, împreună cu mișcările convective, creează nereguli ale indicelui refractiv | ipoteze: vânt puternic perpendicular pe lanț la 700 hPa ca semnal de valuri de aur; pentru a încerca ultima |
| Inversiune foarte mare (până la 8 km în cazuri excepționale) oferă aproximativ 800 km, dar în practică rar mai mult de 300 km | cota de inversare extinde domeniul de aplicare: date utile pentru estimarea distanței de așteptare |
| În timpul verii, raza Pământului este echivalentă cu aproximativ 16000 km (2,5 ori mai mare decât cea reală), cu aproximativ 60 dB în mai puțin de 300 km | în conformitate cu k50 = 157/(157 − ΔN): k = 2,5 corespunde la ΔN aproximativ 94 N/km, care este superrefracție |
| Mai mult de 3000 km numai în intervalul de 20/30° nord și sud al ecuatorului, deasupra mărilor, legat de celulele Hadley; vârf în august în Africa | în afara Europei: util pentru a nu da indici mari pe Atlantic fără feedback, precum și pentru ruta Canare |

Alte date utile din aceleași articole:

- Gama optică între două h1 și h2 metri (d în km): d = 4,1 · (
- Evanescence: lent și superficial cu inversiune; puternic și rapid cu celule dispersate și convective. Programul ar putea folosi pentru a clasifica anunțurile dacă într-o zi va avea rapoarte SNR în timp.
- Pentru sporadicul E în 144 MHz autorul raportează lucrarea DF5AI și Davis și Johnson (Natura 435, 2005) pe legătura dintre temporale și Es: din tropo, dar un indicator separat mâine ar putea folosi datele fulgerului.
- Reflecţie la sol în salt dublu Es: mare K = 81 şi 5 S/m, rural şi pădure K = 13 şi 0,006 S/m, zonele urbane K = 3 şi 0,001 S/m.

## 14. Ipoteze ale indicelui LIVETROPOVHF

Începând cu propunerea, care urmează să fie discutată: un index 0 Greutatea de mai jos sunt presupunerile mele, pentru a fi calibrate cu petele.

Revizuire după Bean și Dutton și Kerr: pragurile de profil provin din tabelul lor 6.5 și scara de la măsurătorile lor, astfel încât un strat refractiv este în valoare de 10 Indicele este apoi calculat mai întâi în dB și apoi ia la 0

Calculul punct cu punct, pas cu pas

1. Descărcare T, umiditate relativă, înălțime geopotențială și viteză verticală la 1000, 975, 950, 925, 900, 850, 800, 700, 600 și 500 hPa, plus umiditate T și 2 m, presiune la sol, înălțime strat și vânt la 10 m.
2. Se elimină nivelurile sub sol; se calculează N și M la fiecare nivel (ITU-R P.453).
3. Calculul −dN/dh în fiecare dintre cele patru straturi de Bean și Dutton (1000
4. În cazul în care dM/dh < 0 găsesc baza și partea superioară a conductei, grosimea d și deficitul ΔM.
5. Comparație cu grosimea minimă a benzii din formula Kerr (aproximativ 180 m pentru 144 MHz, 89 m pentru 432 MHz, la fel ca în tabelul Hepburn); sub pragul pe care încă îl mai contează conducta, dar mai puțin.
6. Stimulez câștigul în dB cu tabelul de mai jos (luați cel mai bun strat, nu suma straturilor, plus termenul de anomalie) și convertiți-l: index = dB / 2.5, între 0 și 10, deci 25 dB este în valoare de 10. O valoare negativă (subrefracție) arată modul în care 0 dar rămâne în datele traseului.

| Situația celui mai bun strat | Criterii | Venituri estimate |
| Normal (nestratificat) | −dN/dh în intervalul de tab-uri. 6.5 (de exemplu 20 | 0 |
| Puţin peste normal. | între limita superioară a benzii normale și pragul de superrefracție | 0-10 dB, proporțional |
| Super-refracție | peste pragul stratului (100, 80, 70, 50 N/km alpinism), dar sub 157 N/km | de la 10 la 15 dB, proporțional până la 157 N/km |
| Conductă prea subţire pentru bandă | dM/dh < 0, grosime sub Prag Kerr | 15 |
| Conector util pentru bandă | dM/dh < 0, grosime peste pragul Kerr | 20 dB, +1 dB la fiecare 5 M unități de deficit, maxim 25 |
| Subrefracție | −dN/dh < 0 în stratul inferior | −10 dB |
| Fundal anormal (se adaugă) | Model Ns minus ITU harta climatologica Ns pentru acea lună | 0,2 dB pe unitate N, între −5 și +5 dB |

Calcul pe traseu (între două locatoare): eșantionați cercul maxim la fiecare 10 km, așa cum face deja programul pentru indexul de pe calea, și calculați fracția de puncte cu conductă utilă și cota medie a bazei. Studiul CSIRO a constatat o parte utilă a traseului din conductă (pe rutele AIS la 162 MHz a fost cu 67% mai mare decât în cazurile de control).

Adăugând calculul pe rută (de la LA0BY și W3SZ): (1) gările maritime în comparație cu baza și partea superioară a conductei, deoarece o conductă înaltă îi servește doar pe cei care intră; (2) un troposcatter "floor" cu metoda Yeh, cu colțurile inițiale luate din profilul solului; câștigul de trope în dB este eliminat din această pierdere și rezultatul este comparat cu pierderea pe care stația reușește să o depășească (tabelul KK6MC). Astfel, indicele devine, de asemenea, o estimare a distanței accesibile.

Corectări pentru a încerca unul la un moment dat (de la Bean și Dutton și LA0BY, IK3XTV, G4MCU deja în manual), pentru a fi păstrate numai în cazul în care acestea îmbunătăţesc comparaţia cu petele:

- Amestecare (importantă în funcţie de autor, prin observarea zilnică): zi pe pământ, între mijlocul dimineţii şi după-amiaza cu cer senin, sau cu vânt pe pământ peste câteva m/s, conductele de pe sol dispar; contribuţia lor trebuie redusă. Nu pe mare.
- Ora zilei (importantă în funcţie de autor, prin observarea zilnică): conductele iraţionale se nasc la apus şi cad după răsăritul soarelui; este un control al prognozei de 3 ore.
- Mișcare verticală: aer în creștere la 850 hPa remixează straturile (corecţie negativă), aerul coborând sub un anticiclon le întăreşte (pozitiv).
- Tendinţă de presiune: presiune la deal pe calea în ultimele ore, semnal mai mare şi stabil; în scădere, semnal inferior (bastano 2 hPa).
- Sinonim: presiune mare peste 1025 hPa, temperatură la 850 hPa mult mai mare decât cea de pe sol, aer cald și uscat care curge pe marea rece, traseu pe mare.
- Trasee montane: un al doilea indice "convectiv" (sectiunea IK3XTV), cu un strat limita dupa-amiaza mai mare decat crestele de-a lungul traseului si presiune mare.

Din VHF/UHF DX Carte, alte corecturi pentru a încerca: zile consecutive de presiune ridicată pe punctul (mai multe zile, mai multe puncte), poziția în comparație cu centrul de anticiclon (bonus la marginile), gradient de presiune pe sol ca o măsură a vântului, ploaie puternică în ultimele 24 de ore (penalitate), nori mici la marginea stratului (penalitate numai pentru benzile înalte). Pentru direcţia rutelor: comparaţi azimutul traseului cu cel al izobrarului.

Verificarea

- Arhiva LIVETROPOVHF: fiecare comercial de peste 500 km până la 144 sau 432 MHz este un "eveniment." Aceasta compară indicele de pe rută cu frecvența evenimentelor, la aceeași oră a zilei și lunii (aceeași abordare a studiului CSIRO).
- Comparație cu Hepburn și F5LEN în aceleași zile: în cazul în care cele trei indexuri se deconectează, te uiți cine a avut dreptate în funcție de pete.
- Radiosundații de 00 și 12 UTC: pentru a verifica dacă modelul vede inversările pe care le măsoară sonda.
- Cauzele istorice ale LA0BY (6
- Limitele care urmează să fie declarate în manual: efectuate sub 100.200 m aproape invizibile pentru nivelurile de presiune GFS; predicție fiabilă timp de 2 "3 zile, apoi numai tendința.

## 15. Surse din 21 noiembrie (a doua parte)

- UIT-R P.1812 (Implementarea Mattlab/Octave a I. Stevanovic, OFCOM; GitHub eeveetza/p1812, Zenodo 10.5281/zenodo.61411558). Metoda specifică pentru zona VHF/UHF a traseului, derivată din P.452: difracție "delta-Bullington," troposcat, conducte și reflexie pe straturi, înghițituri pe sol. Utile ca o a treia comparație lângă P.452 în calculul punct; datele ΔN și N0 sunt produse complete ITU, astfel încât utilizatorul le descarcă (ca pe P.453).
- din Costa, Rabanal Ramirez, Dias, IET MAP 2018 (doi 10.1049/iet-map.2017.0992). 563 MHz, 27 puncte între 5 și 30 km pe teren neregulat cu pădure: P.526 cu 12 m de vegetație −4,3 dB și σ 8,8 dB; P.1812 cu 10 m de narghilea +2,6 dB și σ 8,9 dB; cuie dielectrică −4,8 dB și σ 9,2 dB; P.1546 +5,0 dB și σ 11,7 dB. Pentru program: eroarea reziduală a unei metode bune de difracție este de aproximativ 9 dB, iar înălțimea de pe creastă (10/12 m) contează.
- PETOOL (Ozgun e altri, Computer Physics Communications 182, 2011). Ecuaţia parabolică se împarte pe una şi două căi cu propagator cu unghi larg (unghiuri până la 40 şi 45°, faţă de 10 şi 15° faţă de cea îngustă); fund cu pierderi prin amestecul Fourier transformat; sol la paşi. Confirmă opțiunile de 1.3.2 (unghi larg, coeficienți de sol) și indică calculul bidirecțional pentru reliefurile abrupte.
- MathWorks. `tropopl`: pierdere de gaze și pierdere de Exemplul de utilizare a propagării radar pentru refracție − N = 7,32·e^(0,005577·Ns), același raport CRPL deja în program. `propagationModel` Longley-Rice: valori de polarizare orizontală predefinite, conductivitate 0,005 S/m, permisivitate 15, reactivitate 301, climat continental temperat. Nu MathWorks model se ocupă cu conductele.
- UIT-R, produse digitale din grup 3. Datele și programele P.453, P.452, P.1812, P.2001, P.528, P.1546; datele "integrate" fac parte din recomandări; fără licență de redistribuire declarată: continuați să le lăsați să descarce către utilizator.
- ESA Navipedia, întârziere troposferică. N = Nidrostatica + Numida; întârzierea zenitală uscată este de aproximativ 2,3 m iar cea umedă de zece cm, care variază rapid. Pentru noi: întârzierea umedă estimată de staţiile GNSS este o măsură indirectă de umiditate integrată, posibilă sursă viitoare de acum-formare; Astăzi nu este utilizabil.
- Bray, K2LMG, Autorul, D. W. Bray (General Electric, Ithaca), spune că a verificat previziunile cu luni de conexiuni programate cu K2GOI și W4LTU. Evanescența în timpul de 13,5 dB peste aproximativ 100 mile (7 dB sub și 7 peste media orară), aproape liniară sub 33,5 dB până la 50 mile): pentru o conexiune solidă elimina 7 dB. Corectarea orizontului: fiecare elevație a orizontului este în valoare de 69 mile (111 km) de distanță suplimentară (mai puțin dacă este negativă). Câștigarea unui yagi aproximativ 10 × boom/λ (raportul puterii), mai mult de 4 dB de reflexie a solului; SSB −3 dB și AM −7 dB comparativ cu CW; 99% din ore: aproximativ 180 dB până la 60 mile, 190 la 90, 200 la 240 (2 m). De la 1.3.2 probabilitatea pentru perioada utilizează σ = 3,4 dB (±7 dB ca ±2σ) în loc de 5 dB la mâna a doua estimată.
- Calculul bidirecţional (încercaţi cu prototipul, 21 noiembrie). Ecuație parabolică cu unghi mare cu teren real S RTM1 de la Capri, 144 MHz, antenă 10 m de la sol, atmosferă standard; măsură: putere medie între 20 și 400 m până la 80 km pe ruta către cea corespunzătoare. De la centrul orașului Capri și Marina Grande, vestul este închis (obstacol la +11 De la Anacapri vestul este liber și victoriile directe de 8 Cu toate acestea, pierderile absolute pe 1300 de kilometri de prototip nu sunt fiabile. EA5 EA6 EA3 EA5 EA6
- Barsis și Kirby, BNS Report 6751 (1961), Pikes Peak, 223 km, 100 și 751 MHz. Muntele este un terminal comun de două rute în optică: distribuția medianelor orare este convoluția celor două rute. Excursie pe termen lung (1 Semnalul este mai puternic decât Troposcatter, dar mai slab decât marginea ideală: partea de sus trebuie tratată ca rotunjită (aproximativ 11,5 km). Semnalele mari sunt mereu observate pe la spate.
- Barsis, BNS Report 7652 (1963), trei trasee peste un munte (Colorado, Alaska, Europa), 750 Pierdere medie 10 De la 1.3.2 intrarea în conductă adaugă rotunjirea P.526.
- Barsis and Johnson, NBS TN 88 (1961). Dizolvarea prelungită a valului spaţial, chiar şi pe o cale dincolo de munte (§ 5).
- Norton, Curbele din care Bray primește nomogramele; definițiile pierderii sistemului, transmisiei și câștigului de bază al antenei de proces.
- Rice, Longley, Norton şi Barsis, BNS TN 101 (1 ian şi 67), Volumele 1 şi 2. o singură margine și obstacol rotunjit cu și fără reflexii (§ 7, cu exemplul Pikes Peak), variabilitatea pe termen lung V [0.5) și Y(q) pentru climă (continental temperat și altele). Pentru noi: metoda funcțiilor de obstacol și variabilitate rotunjite este referirea la incertitudinea calibrată.
- UIT-R P.526-16 (11/2025). Formula (31) a lui J(ν), identică cu cea a programului; Treaptă rotundă (32)
- UIT-R P.1812-8 (2025) și -7. Textul complet recuperat: metoda specifică de 30 MHz - 6 GHz (delta-Bullington, Troposcatter, conducte și reflecție pe straturi, ingombe). -7 avertizează că troposcatrul său (de la P.617-5) este greșit sub 66 km. Rămâne o posibilă a treia comparaţie.
- Paulus, Raport citit pe 22/11/2026 (DTIC ADA146528, scanare cu text recunoscut, uneori necitit). Diferenţele de mare măsurate de nave sunt deplasate către stabilitate: aer de mare mai cald în 31;35% din datele navei faţă de 0,7;7% din mistreţii NOAA, pentru psicrometrii perturbaţi de navă şi SST luaţi din apa de răcire (aproximativ +1 °C). Modelul Jeske oferă apoi 40 m și mai mult de 15% din an și înălțimi care sar de la 10 la 40 m în 24 de ore cu timp stabil, neconfirmat prin măsuri radio. Corecţia propusă (pragurile testate 0, −0,5 şi −1 °C; −1 °C este mai bine convenită cu măsurile radio din iunie 1982): dacă diferenţa de aer şi mare este −1 °C sau mai puţin diferenţa măsurată; în caz contrar, calculaţi înălţimea cu −1 °C şi cu 0°C şi, în cazul în care cu −1 °C este mai mică, ţineţi asta. Creşteri pe partea stabilă sunt tăiate, scade rămâne. În iunie, Marsden 120 pătrat: înălțimea medie de la 16,8 m (climatologie) la 7,6 m. Ecuațiile modelului nu figurează în raport (a se vedea Jeske și Hitney). Programul foloseste profilul neutru, care nu are exagerare stabila, si limite δ la 40 m: merge in aceeasi directie; cu SST-urile modelelor (nu de nava) vina de masurare ca Paulus corecteaza cantareste mai putin.
- În cazul în care se utilizează un sistem de măsurare a emisiilor de CO2 în conformitate cu articolul 3 alineatul (2) din Regulamentul (UE) nr. Pe Google Libri există doar previzualizare: Nu citesc.
- VK3FS (3fs.net.au). Introducere de calitate: conducte de înaltă calitate la 450 În conformitate cu alertele de zi/noapte și plafonul de 2500 m.

## 16. Surse

Reguli și formule

- [ITU-R P.453-13 (2017), indice de refracție radio: formula și datele](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)
- [ITU-R P.452-18 (2023), predicția interferenței între stațiile terestre peste 100 MHz: raza echivalentă, β0, troposcatter, conducte](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)
- [ITU-R P.617-5, propagare după orizont pentru podurile radio](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)
- [Shi et al., "A Comprehensive Study on Wave Maximum Lungime of Electromagnetic Propagation in Different Evaporation Ducts," IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

Studii privind predicția conductelor cu modele meteorologice

- [P. Lopez, "A 5-year 40-km resolution global clamatology of super-refraction for sol-based radar meteorology," ECMWF Tech. Memo 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO), "Tropospheric Ducting Prediction based on GFS Model Data at... the CSIRO Murchison Radio-astronomie," Observator arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (FMI), "Efecte de rezoluție model privind prezicerea condițiilor de conducte atmosferice cu Harmonie-AROME," EGUosphere 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

Previziuni radio pentru amatori

- [W. R. Hepburn, "The Hepburn Tropo Index (HTI) "](https://www.dxinfocentre.com/propagation/hti.htm)și [](https://www.dxinfocentre.com/propagation/history.htm)method history
- [W. R. Hepburn, "Lowest Usable Frecvence vs. Grosimea straturilor de scufundare"](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, "Prognoze ale indicelui de refracție la nivel mondial"](https://tropo.f5len.org/WW/info.php), [noua versiune 2026](https://tropo.f5len.org/?p=504), [notă pe modelul ARPEGE](https://tropo.f5len.org/author/f5len/)

Date

- [Deschide-Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api), [notă pe nivelul de presiune](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)
- [NOAA NCEI, Integrated Global Radiosonde Archive (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RAOBget, radio script de inundații pentru Universitatea din Wyoming](https://github.com/NCAR/RAOBget)

Texte clasice (indicate de autorul programului, care urmează să fie consultate pentru formule finale)

- B. R. Bean, E. J. Dutton, * Radio Meteorology*, BNS Monograph 92, 1966, 435 pagini (reprint Dover 1968): profile verticale ale N și M, gradient de capcană sub −157 N/km, comparație cu unde radio; capitol 5 "Synoptic radio meteorology." Baza de calcul pentru 2;4 etape de calcul per punct. Gratuit: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [carte NIST, doi 10.6.028/NBS.MONO.92](https://www.nist.gov/node/1770911), [Internet Archive](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf)copie.
- L. Boithias, *Radio Wave Propagation*, McGraw-Hill 1987 (tradus de D. Beeson din franceză, 330 pagini, ISBN 0070064334; de asemenea, North Oxford Academic): mecanisme dincolo de orizont, volum comun de împrăștiere și pierderea antenei de cuplare-mezzo. Numai hârtie ([carte de bibliotecă](https://libcat.library.tamu.edu/Record/in00000014812/Description)).
- M. P. M. Hall, *Effects of the Troposphere on Radio Communication*, IEE Electromagnetic Waves Series n. 8, Peter Peregrinus 1979, 206 pages (ISBN 09060448257; brosura 0863410863): sintoptic situations and propagation VHF/UHF. Baza pentru ingrediente sinoptice. Comerț la IET ([carte](https://libcat.library.tamu.edu/Record/in00000636385)).
- H. G. Booker, W. E. Gordon, "A Theory of Radio Scattering in the Troposphere," *Proceedings of the IRE*, vol. 38, n. 4, aprilie 1950, pp. Teoria scapping din disogenitatea indicelui refractiv. Ideea centrală: puterea difuzată depinde de intensitatea şi scara fluctuaţiilor turbulente ale indicelui de refracţie (corelaţie exponenţială); în apropierea orizontului predomină difracţia, dispersarea în continuare; Potrivit autorilor, scara turbulenţelor şi variaţiile indicilor aşteptate de meteorologie sunt suficiente pentru a explica câmpurile măsurate. Citat de 328 articole (Scilit), nici o copie de acces liber. Card pe Semantic Scholar ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); text complet pe IEEE Xplore plătit ([doi 10.1109/JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)).
- C. M. Crain, J. R. Gerhardt (Electrical Engineering Research Laboratory, University of Texas), "Măsurări ale parametrilor implicați în teoria Scattering Radio în Troposphere," *Proceedings of the IRE*, vol. 40, n. 1, January 1952, pp. 50](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)
- "The Constants in the Ecuation for Atmospheric Refractive Index at Radio Frecvences," *Proceedings of the IRE*, vol. 41, n. 8, August 1953, pp. 1035 N; este de aici că valorile 77,6 și 3,73 · 10^5 ale formulei ITU ([Q]](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E. C. S. Megaw, "Teoria de propagare prin împrăștiere prin radioFundamental," *Proceedings of the IEE Part C: Monografs*, vol. 104, n. 6, Septembrie 1957, pp. 441](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)
- *Proceedings of the IRE*, Octombrie 1955, special number on scatter propagation (by K. A. Norton and J. B. Wiesner), with Bullington, Staras and others: free in PDF on [World Radio History](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf).
- D. E. Kerr (editat de), *Propagation of Short Radio Waves*, MIT Radiation Laboratory Series vol. 13, McGraw-Hill 1951 (Restaffe Dover 1965 and IEE 1987): theory of evaporation and surface ducts on the sea. Copie digitală: [World Radio History](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); IET edition in [IET Digital Library](https://shop.theiet.org/propag-short-radio-waves).

Măsuri recente de difuzie troposferică în VHF: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Australia), "A Study of VHF Troposcatter Communication Channels in South Australia," in *Digital Signal Processing for Communication Systems*, Springer 1997, pp. 103 Textul pentru o taxă; de la abstract se referă la difuzie, nu conducte. Utile numai dacă doriți să calibreze termenul de Troposcatter cu date canal VHF.](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)

Modele numerice și rute maritime (indicate de autorul programului)

- Dle Levy, *Parabolic Ecuation Methods for Electromagnetic Wave Propagation*, IEE Electromagnetic Waves Series #45, 2000, 336 pages (ISBN 0852967640): parabolic ecuation for refractive profiles that change along the trail and for orography; capitole on Troposferous propagation, rays and ways, propagation on sea. Aceasta ar servi pentru un calcul de câmp de-a lungul traseului, dincolo de index. Pentru plată, de asemenea, în format electronic de la IET ([Cartea IET](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)).
- S. D. Gunaşekar, D. R. Siddle, E. M. Warrington (Universitatea din Leicester): 2 GHz masuratori pe Insulele Canalului si Canalul. Multe lucrări și teza sunt gratuite în arhiva universitară: [Statistici pe termen lung privind conducta de evaporare, Radio Science 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434), [modificări ale semnăturii pe trei rute maritime, Radio Science 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710), [Titlurile N. Mufti 2012 cu rute de 50 și 140 km, de asemenea, în VHF](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854), [teza I. Alam 2002 privind reactivitatea calculată pe baza datelor meteorologice, 240 MHz și 2 GHz X3 UQ, [tesi.......................................................................](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus, "Aplicarea practică a unui model de evaporare," Radio Science vol. 20 n. 4, 1985, p. 887 (netranzacții IEEE pe antene și propagare), doi 10.1029/RS020i004p00887 ([scheda](https://zendy.io/title/10.1029/rs020i004p00887)): înălțimea conductei de evaporare din datele meteorologice de suprafață. Eliberează raportul tehnic precedent: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf).

T. Lindquist, *Wave Propagandation Models in the Troposphere for Long-Range UHF/SHF Radio Connections*, master's thesis, University of Karlstad, 2 June 2020, 106 pages (supplied by the author of the program). Este un ghid pas cu pas, liber, pentru a scrie un calcul cu ecuaţia parabolică: soluţie cu pas de divizare (chiar şi la unghiuri largi), condiţii limită pentru uscat şi mare, sol la trepte, urmărire fascicule pe profiluri liniare uneori, cu controale numerice; citează instrumentul gratuit PETOOL pentru MATLAB. Două date utile simultan: (1) Sensibilitatea N la variabilele meteorologice, δN Concluzia autorului (frecvențele 1-10 GHz): dincolo de orizont conexiunile fiabile necesită aproape întotdeauna o conductă.

Software de propagare pe profilul solului (pentru orizont și obstacole, nu pentru Tropo preconizate)

- [Radio Mobile](https://ve2dbe.com/rme.html)by Roger Coudé VE2DBE: free, Windows (de asemenea, Linux și Mac cu vin), Longley-Rice/ITM model, profil de teren extras automat din datele cotelor ([download](https://ve2dbe.com/download/download.html)).
- [Splat!](https://qsl.net/kd2bd/splat.html)by John Magliacane KD2BD: GPL, 20
- [ITM, NTIA/ITS](https://its.ntia.gov/software/itm)cod oficial: modelul Longley-Rice (20 Codul este gratuit și ați putea adăuga la calculul pe calea LIVETROPOVHF (pierdere din cauza solului, care urmează să fie adăugate la indicele tropo). Raportul original: A. G. Longley, P. L. Rice, "Predicţia pierderii transmisiilor radio Tropospherice asupra Terrenului neregulat: O metodă computerizată https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITU-R, software și date digitale](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): hărți libere ale valorilor mediane anuale ΔN și N0 pe grila 1,5°, utilizate de P.452 și P.617
- Pathloss (Contract Telecomunications Engineering): program comercial pentru poduri radio; nu am găsit o sursă publică care să confirme ce recomandări UIT implementează, astfel încât să verifice.

Codul GitHub reutilizabil în LIVERTOPOVHF

| Proiect | Ce faci? | Limba | Utilizați posibil pentru noi |
| [NTIA/ITM](https://github.com/NTIA/itm) | model Longley-Rice oficial al NTIA, cu pachete pentru. NET |  C++, C#  | pierderea terenului pe calea: integrează direct în program, care este în C# |
| [Fuzzzee/itm](https://github.com/fuzzzeee/itm) | ITM C++ cod C# traducere |  C#  | alternativa fara biblioteci native, utila si pentru versiunea Windows 7 |
| [eeveetza/Py452](https://github.com/eeveetza/py452) | UIT-R P.452, identică cu versiunea MATLAB/Octave aprobată de UIT-R WP 3M; versiunea MATLAB publicată de Biroul Federal de Comunicații din Elveția | Python, MATLAB | trimiterea la traducerea în C# formulele Troposcatter și efectuate, cu exemple de validare ITU |
| [bwinkel/pycraf](https://github.com/bwinkel/pycraf) | P.452 complet, profile P.676, SRTM; fișiere de date GPL v3, dar ITU incluse nu sunt gratuite pentru utilizare comercială | ♪ | controlul rezultatelor noastre |
| [ITU-Rpy](https://github.com/inigodelportillo/ITU-Rpy)] | multe recomandări UIT-R, inclusiv P.453-13, validate cu exemple UIT | ♪ | verificarea formulei N și a hărților climatice ale P.453 |
| [NCAR/RAOBget](https://github.com/NCAR/RAOBget) | download the radio wave of the University of Wyoming in text format | ♪ | diagramă de descărcat în C# profilele stațiilor italiene |

Notă: versiunea curentă a P.453 este [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), descărcabil gratuit; în documentul am folosit 13, pentru a fi reallined.

Deja în manualele de program și reutilizabile aici: LA0BY (Nordic FUSHF 2019), IK3XTV ("Propagare pe 2 metri" și "Propagation VHF DF5AI/DK5YA G3YGF G4MCU KP4MD
