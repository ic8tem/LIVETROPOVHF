# Źródła i wzory

> Automatyczne tłumaczenie przez włoski: w przypadku wątpliwości warto włoskiego przewodnika.

Kolekcja robocza źródeł, z którymi skonsultowano się w związku z prognozą dotyczącą tropu LIVETROPOVHF (20- 21 listopada 2026 r.): Fizyka podstawowa, dostępne dane, opublikowane indeksy, formuły, doświadczenia w radiu amatorskim, walidacja i decyzje. Formuły są numerowane jak w LaTeX, (1), (2),..., a w tekście, które przypominają z liczbą w nawiasach. Podręcznik prognozowania opisuje, w jaki sposób program z nich korzysta.


## 1. Pomieszczenia autorów

IC8TEM obserwacje, które framework pracy, z informacji zwrotnych znalezionych w źródłach.

- Włoska geografia wymaga dyfrakcji. Z grzbietowych, promontory i wyspy kilka tuzinów kilometrów od stacji, udział dyfrakcji są obliczane z parametrem Fresnel na głównej krawędzi, nie tylko z gładkiej ziemi. Spotkanie: w porównaniu z nieregularnym terenem o częstotliwości 563 MHz (Rio de Janeiro, 27 punktów od 5 do 30 km) najlepsze metody to P.526 z krawędziami ostrza, P.1812 i klinem dyfrakcyjnym, ze średnim błędem między -5 a + 3 dB oraz dyspersją około 9 dB; P.1546 był najbardziej pesymistą (z Costa, Rabanal Ramirez i dla stromych zboczy w pobliżu stacji PETOOL pamiętają, że służą one szeroko zakrojonym propagatorom kąta i obliczeniu dwukierunkowym (fala odbita przez reliefs).
- Aby dostać się do odległego kanału, potrzebujesz Fresnela. Jeśli kanał zaczyna się poza horyzontem, sygnał przychodzi do nas przez dyfrakcji nad przeszkodami aż do jego początku; od 1.3.2 program oblicza go (Podręcznik wizji, § 10 ter). Spotkanie: Rohde & Schwarz (2023) zauważa, że stacje wzdłuż wysokiego kanału często nie wchodzą, podczas gdy ci blisko końca tak.
- Warunki szybko się zmieniają. Teorie istnieją, ale warunki zmieniają się w minutach, a żaden system prognozowania nie zajmuje danych w czasie rzeczywistym: GFS wychodzi co 6 godzin, z zakresem czasu i 0,25 °, a prognoza może stracić wartość w krótkim czasie. Spotkanie: Bray (QST, listopad 1961) pomiar godzinowych średnich wahań o około ± 7 dB na 100 mil; kanały poruszają się z czasem atmosferycznym (Rohde & Schwarz); nasz rejestr QSO 29 / 12 / 2021-2 / 1 / 2022 pokazuje otwory rosnące i spadające w ciągu kilku godzin.
- Najlepsza obecnie -casting pozostaje DX cluster- PSKReporter sieci. Aby wiedzieć, co dzieje się teraz, plamy klastra, PSKReporter (i WPPR) pokonać dowolny wzór: są to miary. Prognoza mówi, gdzie szukać i jak długo może trwać; reklamy mówią, czy tam jest. Program dla tego stawia miejsce i prognozę na tej samej mapie.

## 2. Cel

Pomysł ten jest trzecim indeksem, obliczonym przez sam LIVETROPOVHF, zaczynającym się od pionowych profili modelu pogodowego, który ma być obok Hepburn i F5LEN zamiast ich zastępować. Dziś program odczytuje swoje mapy już gotowe: indeks powiedziałby również * dlaczego * jest otwarcie (kwota i grubość przewodu, minimalny pas uwięziony) i może wystąpić z plamami, które program już zbiera.

- Co można podać: prawdopodobieństwo przejazdu na punkt i na godzinę, udział w podstawie i górze, grubość, minimalna częstotliwość uwięziona, a wzdłuż ścieżki ułamek trasy wewnątrz przewodu.
- Co nie może dać: cienkie kanały (poniżej kilku dziesięciu metrów), których nie rozwiązują globalne modele o temperaturze 0,25 °, a kanały parowania na morzu, które służą przede wszystkim z GHz do góry.
- Pierwszy praktyczny cel: 144 i 432 MHz w Europie, z weryfikacją miejsc klastrów DX, PSKReporter i lokalnych dekodowania.

## 3. Fizyka podstawowa

Wszystko zaczyna się od refrakcji N obliczone na każdym poziomie profilu: przewód jest tam, gdzie modyfikowana reaktywność M zmniejsza wspinaczkę, czyli tam dN / dh spada poniżej -157 N / km ([ITU- R P.453- 13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)).

Refrakcja (P i w hPa, T w kelwinie):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Ciśnienie pary z wilgotności względnej H (%) i temperatury t (° C) powyżej wody między -40 a + 50 ° C:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

Zmodyfikowana reaktywność (h w km lub 0,157 · z z w metrach) i kryterium kanałowe:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN / dh (N / km) | Rozporządzenie | Wpływ 144 MHz |
| 0 | sub-refraction | zmniejszenie natężenia przepływu |
| od 0 do -79 | normalny (standardowo około -40) | horyzont radiowy o równoważnym promieniu 4 / 3 |
| od - 79 do - 157 | super- załamanie | wydłużony przepływ, "zbyt dobry" |
| poniżej − 15 | przewód (pułapki) | sygnały uwięzione, DX ponad 1000 km |

Jak wyodrębnić N z modelu na poziomach ciśnienia: dla każdego poziomu bierzesz T, wilgotność względna i geopotencjał wysokości, obliczasz N i M, a następnie gradient między sąsiednimi poziomami. Poziomy poniżej podłoża należy odrzucić (przy 1000 hPa na Alpach jest pod ziemią).

Minimalna uwięziona częstotliwość: przewód prowadzi tylko fale dość krótkie w porównaniu do jego grubości. Hepburn publikuje grubość tabeli → minimalna użyteczna częstotliwość ([DX Info Centre](https://www.dxinfocentre.com/propagation/luf.htm)); jest to miękki limit, pod którym sygnał stopniowo spada.

| Grubość | Minimalna częstotliwość płatna |
|  61 m  | 962 MHz |
|  91 m  | 464 MHz |
|  122 m  | 276 MHz |
|  152 m  | 185 MHz |
|  183 m  | 133 MHz |
|  213 m  | 101 MHz |
|  305 m  | 53 MHz |

W zakresie VHF (często 120- 300 m) wartości tabeli są dość dobrze zgodne z prawem o stałej; poniżej 100 m formuła niedocenia częstotliwość do 30- 40%. Dostosowywanie go sam (nie wzór opublikowany przez Hepburn), z f w MHz i d w metrach:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

Zasadniczo, dla 2 m, przewód jest często co najmniej 170- 180 m, dla 70 cm jest około połowy. Wzór krążący w sieci, λmax = 0,014 · d ^ 1.5, pochodzi z Kerr (1951, Rozdział 1, eq. 18) i jest ważny z λ w centymetrach i stojąc, dla przewodu, w którym M spada liniowo: z tych jednostek daje około 2 m 180 m i na około 70 cm 89 m, to są te same wartości tabeli Hepburn, który pochodzi z niego. Kerr ostrzega, że nie jest to cięcie sieciowe: nawet fale kilka razy dłużej cierpią wiele przewodu, i sposób, że "strata" trochę może poprawić odbiór nawet ze stacji z przewodu. Badanie IEEE Access z 2019 r. potwierdza, że metody klasyczne (Kerr, Kukushkin) nie opisują prawdziwego cięcia ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)): próg powinien być stosowany jako wskazanie, a nie jako ściana.

## 4. Dane dostępne za darmo

Słabym punktem jest pionowa rozdzielczość: od 1000 do 850 modeli hPa daje poziom co 25- 50 hPa, to jest co 200- 450 m, więc widzą dobrze inwersji do 500- 2000 m, ale nie cienkie kanały w pobliżu ziemi. LIVETROPOVHF używa już Open- Meteo do isobare, temperatury do 850 hPa i inwersji: indeks może zacząć się od tych samych połączeń.

| Źródło | Co daje | Rozdzielczość | Wykorzystanie indeksu |
| [Open- Meteo, GFS](https://open-meteo.com/en/docs/gfs-api) | T, wilgotność względna, punkt rosy, wysokość geopotencjalna według poziomu (1000, 975, 950, 925, 900, 850 hPa...); wysokość warstwy dopuszczalnej | 0,25 ° na poziomie ciśnienia; 16 dni, godziny do 120 h, uaktualnione co 6 h | profil N i M, gradient, podstawa i szczyt kanału; już w programie |
| [Open- Meteo, ECMWF](https://open-meteo.com/en/docs/ecmwf-api) | te same poziomy z modeli open- data 0.25 °; grunt daje punkt rosy |  0,25°  | druga opinia, przydatna dla zgodności modeli |
| [Open- Meteo, DWD ICON](https://open-meteo.com/en/docs/dwd-api) | T i wilgotność względna na poziomie ciśnienia; w ICON- D2 jest 800, 900 i 925 hPa | ICON- UE około 7 km | Trzecia opinia w sprawie Europy |
| Wyoming](https://github.com/NCAR/RAOBget) | zmierzone profile, dwa starty dziennie (00 i 12 UTC), format tekstowy na stację | wiele znaczących poziomów, nawet co kilka tuzinów metrów | weryfikacja indeksu; zobacz subtelne kanały, których model nie widzi |
| [GRUANS, NOAA NCEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive) | historyczne archiwum nadawców, plik na stację, bardziej pochodne parametry | jak powyżej | kalibracja na lata danych i porównanie z plamami z przeszłości |
| ERA5 (Copernicus) | godzinna ponowna analiza od 1940 r., 37 poziomów ciśnienia | około 28 km | statystyki i kalibracji, a nie prognozy |

- Z wysokości geopotencjał Open-Meteo jest nad poziomem morza: poziomy, które spadają pod ziemię muszą być odrzucone ([Open-Meteo, górne powietrze](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)).
- Na ziemi dodaje się T i wilgotność na 2 m: dają one pierwszy odcinek gradientu, ten, w którym nocne kanały powierzchniowe rodzą się na równinie i na morzu.
- Włoskie audycje na archiwum Uniwersytetu Wyoming (weather.uwyo.edu / upperair / sounding.html): stacje i numery WMO w tabeli poniżej.

| Stacja | W | Szerokość geograficzna | Długość | Udział |
| Udine / Campoformido |  16044  |  46,03  |  13,18  |  94 m  |
| Mediolan / Linat |  16080  |  45,43  |  9,28  |  103 m  |
| Praktyka morska |  16245  |  41,65  |  12,43  |  12 m  |
| Brindisi |  16320  |  40,65  |  17,95  |  10 m  |
| Trapani / Birgi |  16429  |  37,92  |  12,50  | około 14 m |

Format tekstowy Wyoming daje dla każdego poziomu ciśnienia, wysokości, temperatury, punktu rosy i wilgotności: wystarczy dla N i M. Wyoming nie ma oficjalnego interfejsu, skrypty są używane jako [RAOBget z NCAR](https://github.com/NCAR/RAOBget); Kolegium DuPage przechowuje kopie tekstu według stacji (np. 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)).](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. Wskaźniki i opublikowane metody

Hepburn i F5LEN zaczynają od tych samych składników fizycznych, ale nie publikują pełnego algorytmu; badania naukowe 2008- 2026 zamiast opisać metodę krok po kroku, i to jest stamtąd, że najlepiej skopiować przepis.

| Metoda | Dane wyjściowe | Co oblicza się | Przydatne notatki dla nas |
| [Indeks Hepburn Tropo](https://www.dxinfocentre.com/propagation/hti.htm) | surowe modele pogodowe Kanadyjskiej Służby Meteorologicznej (MSC, zapisane na jej mapach; nie GFS / NAM), mapy zaprojektowane za pomocą GRADS, własny algorytm ([storia](https://www.dxinfocentre.com/propagation/history.htm))) | 0- 10 liniowa skala krzywizny sygnału: 0 normalny, 1-2 super-refrakcji, 3- 4 niektóre kanały, 5- 6 rozproszonych kanałów; wartości ujemne = sub- refrakcji | skala do naśladowania, więc trzeci indeks natychmiast porównuje się z dwoma pierwszymi |
| [F5LEN, światowa prognoza](https://tropo.f5len.org/WW/info.php) | dla Europy model ARPEGE Météo-France, 7,5 km, więcej poziomów od 2 m do 1500 m ([uwaga autora](https://tropo.f5len.org/author/f5len/)) | Średni indeks dla właściciela, 40 kroków od 3 godzin, 4 obliczenia dziennie, 28- dniowe archiwum ([nowa wersja](https://tropo.f5len.org/?p=504)) | potwierdzić, że służy modelowi o wielu niskich poziomach |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf) | 60 poziomów modelu ECMWF, od podłoża do 3000 m | minimum dN / dz na kolumnę: przeprowadzone w ramach -157 N / km, super@-@ załamanie w ramach -78.7 N / km; podstawa warstwy pułapki | klimatologia: Dolina Po obszar europejski z kilkoma rurociągami przez cały rok; Morze Śródziemne latem blisko 100%, zimą poniżej 15%; Włochy ponad 30% w letnie noce |
| [Indermuehle i Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861) | GFS 0,25 °, 21 poziomów ciśnienia + pola przy 2 m | M = N + 0,157 z; minimum dM / dz poniżej 3 km, podstawa i górna część kanału, głębokość deficytu M; poniżej 40 m odrzucona | sprawdzone przez 7 lat na 25 kanałach od 88 MHz do 2,68 GHz; dobra pojemność do około 3 dni wcześniej (AUC około 0,74), następnie spada; GFS lepiej niż ERA5 |
| [Hämäläinen et al., MFW 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf) | Harmonie- AROME, 750 m lub 2,5 km, 65 lub 90 poziomów | dM / dh < 0 na profilu modelu, porównanie z wiązką 4-59 m | liczy się bardziej pionowo niż rozdzielczość pozioma; żaden model nie widzi kanałów poniżej 15 m; wilgotność dominuje na gradiencie temperatury |

Trzeci amatorski serwis radiowy już aktywny: [TROPOCAST.EU](https://tropocast.eu/)z Artur SQ3A, na 2 m i 70 cm w Europie. Od strony prognozy ([tropocast.eu / prognoza](https://tropocast.eu/forecast)):

- Dane: DWD ICON- EU (przechodzą 0,0625 °, około 7 km), domyślne źródło; alternatywnie kanadyjski GDPS (0,15 °), mniej szczegółowe.
- Metoda: N i M zgodnie z ITU- R P.453 z profilu pomiędzy warstwą w pobliżu gruntu i 850 hPa; rozpoznawanie warstw, połączenie kanałów do podłoża i wysokie i progi kolorów są właściwą kalibracją autora, nie opublikowane.
- Produkty: "kompletny" widok 0- 3000 m, więcej oddzielnych widoków "do ziemi" i "wysoki" (na razie tylko na mapach do włączenia); wynik 0- 100 w dziewięciu klasach (pod 38 tło, 38- 52 krańcowy, 53- 58 słaby, 59- 65 umiarkowany, 66- 72 dobry, 73- 80 bardzo dobry, 81- 88 wysoki, 89- 95 silny, 96- 100 bardzo silny); prognoza do + 72 h na etapach godziny, z animacją i archiwum.
- Obszary: Europa, Polska, Wielka Brytania, Niemcy, Francja, Hiszpania, Włochy, Czechy, Chorwacja; interfejs również w języku włoskim.
- Ponowne wykorzystanie: mapy są wbudowane w iframe bez klucza API; istnieje również udokumentowany schemat API (OpenAPI i JSON). Licencja nie jest wskazana: Przed użyciem go jako trzeciego źródła obok Hepburn i F5LEN należy zapytać autora.

Różnice w stosunku do naszego projektu: TROPOCAST zatrzymuje się na 850 hPa (około 1,5 km), podczas gdy wysokie kanały docierają do 2000 m i dalej (przypadek LA0BY 22 / 11 / 2018) i nie podaje indeksu dla pasma lub trasy. Klasy 0- 100 można porównać do naszego indeksu 0- 10 w tych samych dniach.

Struktura obrazu TROPOCAST (z linku dostarczonego przez autora programu): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, gdzie n jest etapem prognozowania. Strona zakazuje automatycznego wycofywania się do robotów, więc nie mogłem odczytać obrazu; również w tym celu, przed pokazaniem tych map w LIVETROPOVHF służy autorowi zezwolenia.

Notatnik: [DJ5AR, kategoria](https://dj5ar.de/?cat=174)(Magonza, 21 artykułów 2017- 2023, 2 m- 13 cm). Opisane przypadki: przeprowadzono kilka tygodni od Irlandii (23 maja 2023 r.), od FT8 do Białorusi (wrzesień 2020 r.), od Włoch do Niemiec nad Alpami i Adriatykiem (sierpień 2017 r.). Uznaj przypadek "zbyt wysokiego" kanału (odwrócenie do 1000 m), którego nie możesz użyć z ziemi: kolejne potwierdzenie, że konieczne jest porównanie udziału przewodu z udziałem stacji. Używać radiostacji jako pomiaru apertury (do 1028 km) oraz pionowych profili Paryża, Bordeaux i DWD.

Podsumowując, wspólny przepis: profil pionowy → N i M na warstwę → warstwa z dM / dh ujemny → podstawa, góra, grubość i siła kanału → indeks. "Tajną" częścią Hepburn jest empiryczna kalibracja realnych wydarzeń, którą możemy odtworzyć plamami.

## 6. Przydatne pliki dla programu

Oprócz N i M (sekcja fizyki podstawowej), są one używane do przejścia z pionowego profilu do numeru punktu, a następnie ścieżką. Formuły ITU zostały zrekonstruowane przez PDF odczytywane w części źle; w dniu 22 listopada zostały poddane przeglądowi na oficjalnych tekstach ([ITU-R P.452-18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453-14, P.526-16, P.372-18) i poprawione w kodzie: stała troposkatter 190 (nie 190,1) plus absorpcja gazów przy ¶ rednim poziomie 2 + 20 logów K, interpolacji pod napięciem optycznym; dyfrakcja na pełnym gruncie sferycznym (eq. 23- 37: średnia funkcja Land- morze, wysokość z limitem 2 + 20 logów K); nasycenia parowego pod ciśnieniem optycznym P.453- 14 (eq. 9) zamiast EF) ze starą formułą Magnus ". Rumore P.372-18: tabela 1 i galaktyczna odpowiadają; krzywe szumów sztucznych wynoszą do 250 MHz, a galaktyczne do około 100 MHz, przez program rozszerza te same prawa.

1. Gradient między dwoma poziomami modelu (h w km, M w jednostce M):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. Wytrzymałość na zanurzenie i grubość: grubość d = górna -podstawa (dla kanału glebowego, podstawa = gleba); wytrzymałość jako niedobór M między podstawą i wierzchołkiem.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3. Współczynnik równoważnika promienia Ziemi (P.452- 18, eq. 5 i 6a), przy ΔN = średni spadek N w pierwszym km, dodatni, w N / km:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

Z ΔN = 40 masz około 4 / 3 k (atmosfera standardowa); z ΔN zbliża się 157 k zasłony nieskończoność, to znaczy, radio ziemskie staje się płaskie: jest to początek przewodu.

4. horyzont radiowy wysokiej anteny h metrów, o promieniu równoważnym\ _ e w km:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5. Prawdopodobieństwo położenia kanału dla szerokości geograficznej (P.452- 18, eq. 2), procent czasu; ∞ = szerokość środka trasy, μ1 i μ4 uwzględniają odległość między morzem:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

Przy 41 ° N, wszystkie drogą morską (μ1 = μ4 = 1), daje około 11% czasu: jest podstawą klimatologiczną, użyteczną jako wartość odniesienia, od której różni się spodziewany indeks.

6. Utrata troposkattera (P.452-18, eq. 45; f w MHz, d w km, θ kąt ścieżki w mrad, N0 reaktywność ziemi na środku trasy):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

Termin -0,15 · N0 pokazuje, dlaczego mokre powietrze pomaga również bez kanału: 50 dodatkowych jednostek N jest o 7,5 dB mniej niż strata. LIVETROPOVHF używa już wersji G3YGF utraty rozproszenia.

7. Utrata w kanale (P.452-18, eq. 46- 51): stała utrata sprzęgu plus tłumienie dla kątów miliradiantu, który rośnie wraz z korzeniem sześciennym częstotliwości (f w GHz, a\ _ e w km):

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. Od Bean i Dutton (1966) i Kerr (1951)

Dwa klasyczne teksty dają trzy rzeczy, które są potrzebne natychmiast do indeksu: gradientowe progi już taranują na tych samych poziomach ciśnienia GFS, co jest warte w dB zmiana reaktywności, i jakie sytuacje pogodowe tworzą lub niszczą kanały. * Radio Meteorology *, NBS Monograph 92.

1. Klasyfikacja profilu dla warstw ciśnieniowych (Fasola i Dutton, zakładka 6.5, str. 248; − dN / dh w N / km). Zaprojektowany dla fal radiowych, ma zastosowanie jako na poziomie modelu:

| Droga | Akcje | Refrakcja cząstkowa | Nielaminowane | Nadmierne załamanie |
| 1000- 850 hPa | 0- 1,46 km | - dN / dh < 0 | od 20 do 60 | ponad 100 |
| 850- 700 hPa | 1,46- 3,01 km | - dN / dh < 0 | od 20 do 50 | ponad 80 |
| 700- 600 hPa | 3,01- 4,20 km | - dN / dh < 0 | od 15 do 40 | ponad 70 |
| 600- 500 hPa | 4,20- 5,57 km | - dN / dh < 0 | od 10 do 30 | ponad 50 |

2. Wartość w dB (Bean and Dutton, cap. 6, pp. 233- 258), mierzona od 70 do 180 MHz na trasach poza horyzontem:

- Każda warstwa (na ziemi lub na wysokości) w porównaniu z profilem "gładkim" podnosi sygnał 10- 25 dB do wszystkich wartości procentowych; większy przyrost pochodzi z wysokich warstw przechylonych, prawdopodobnie z powodu efektu skupienia (przebieg 200 km).
- Reaktywność naziemna N i spadek w pierwszym km ΔN są związane z miesięcznym sygnałem mediana ze współczynnikiem od 0,4 do 0,95, mediana 0,7 (20 tras od 130 do 446 km, około 100 MHz).
- Cykl roczny: 0,18 dB na jednostkę N; cykl dzienny od 0,2 do 1,1 dB na jednostkę N, maksimum na trasach 175- 200 km i zimą.
- Parametr złożony Misme szacuje zmienność pola jako 0,5 dB / (N / km) · (równoważny gradient -40) plus termin stabilności termicznej; parametr turbulencji D (obszar pomiędzy profilem rzeczywistym a adiadiatycznym, 0-3 km) po prostu poprawia się w porównaniu do prostego ΔN. Wnioski autorów: ΔN pozostaje najlepszym parametrem prostoty.

3. Profil referencyjny i anomalia (Bean and Dutton, § 3.8 i 3.9). Atmosfera wykładnicza referencyjnego CRPL daje "normalny" profil zaczynający się od N; jednostka A mierzy, jak bardzo rzeczywisty profil różni się przy każdej akcji. Jest to naturalny sposób przekształcenia profilu GFS w anomalię umieścić na mapie:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

Przy N = 313 mamy ΔN około -42 N / km, czyli równoważny promień 4 / 3. Kryterium -157 N / km przewodnika pochodzi również z prawa Snell dla promienia poziomego (eq. 4.23), prawie niezależnie od wysokości anteny.

4. Gdzie i kiedy powstają przewody (Bean i Dutton § 4.4; Kerr cap. 3):

| Proces | Sytuacja | Wpływ |
| Ostrzeżenie | gorące i suche powietrze z lądu płynącego na zimniejszym morzu (typowy letni rękaw po dniach dobrej pogody); głównie oblicza deficyt wilgotności | intensywny kanał na morzu |
| Naświetlanie nocne | spokojne niebo, słaby wiatr; niskie chmury lub dużo wilgotności temu zapobiec | doprowadziły do ziemi, która rodzi się o zachodzie słońca, rośnie wieczorem i znika szybko po wschodzie słońca, zwłaszcza wewnątrz |
| Subsidence | powietrze spadające z antycykliny, ogrzewane i suche, nad świeżym i wilgotnym powietrzem; częste powodzie w górach i południowo-wschodnich sektorach antycykliny | warstwa superrefrakcyjna lub wysoki kanał; prawie zawsze gradient wyższy niż standardowy w rewersie |
| Morze i ziemia | wybrzeże, obieg kilkuset metrów | lokalne rurociągi wzdłuż wybrzeża |
| Przeciwko przewodowi | mieszanie: ziemia ogrzewana przez słońce między północą a południem, wiatr do ziemi nad kilkoma m / s na szorstkim gruncie; mgła nocna | Prawdopodobieństwo prawie niczego |

5. Dwa nowe składniki. Ruch pionowy: prądy wznoszące się w strefach konwergencji usuwają i usuwają warstwy, a subsidencja wzmacnia sygnał (Moler i Holden: średnio o 2,5 dB więcej niż z powietrza pod górę, na morzu w paśmie L i X). Open- Meteo już zapewnia prędkość pionową na poziomach ciśnienia. I trend ciśnienia: na ścieżce od 140 km do 186 MHz sygnał zstąpił, gdy ciśnienie spadło do nadajnika i do środka ścieżki i wrócił wysoki i bez evanescence, gdy poszedł w górę, z zaledwie 2 (Bean i Dutton, fig. 6.9, p. 241).

6. Od Kerra. Oprócz formuły maksymalnej długości fali (sekcja fizyki bazowej), Kerr podaje przybliżony wzór dla M z jedynej temperatury, pary i wysokości (§ 3.9, ze starymi stałymi 79 i 4800, następnie zastąpionymi przez 77,6 i 4810) i zauważa, że do odwrotu cięcie wiatru może generować fale, które huśtają się na wysokości kanału, możliwe przyczyny głębokich ucieczek (§ 3.26). Włoskie źródło zgłoszone przez Bean i Dutton i do wyszukiwania: L. Bonavoglia (grudzień 1958), "Korelacja między zjawiskami meteorologicznymi i propagacją"...

## 8. Z amatorskich prezentacji radiowych i raportów technicznych (druga seria)

Siedem dokumentów dostarczonych przez autora programu. Trzy służyć bezpośrednio do indeksu: W3SZ dla troposcatter "podłogi", LA0BY dla przypadków Hepburn nie przewidział, Hepburn ( LU7MC) dla klasyfikacji pogody kanałów.

1. Troposcatter: sygnał zawsze tam jest. R. Rehr W3SZ, "Tropospheric Scatter (Troposcatter) Propagation for VHF, UHF, Microwave and Frequenties", Pack Rats Conference 2014; J. R. Duffey KK6MC, "The Troposcatter Propagation Mode and How to Predict Coverage on the VHF / UHF Bands", CSVHF 2017.

- Przydatne od około 100 do 700 km, od 144 MHz do 10 GHz; strata rośnie z częstotliwością 30 log f.
- Przede wszystkim liczy kąt początkowy w kierunku horyzontu: każdy dodatkowy stopień kosztuje około 9- 12 dB ( M1CNK, metoda Collinsa).
- W lecie sygnał jest średnio o 10 dB wyższy niż w zimie; w połowie popołudnia o 5 dB niższy niż we wczesnym ranku lub wieczorem. KK6MC wskazuje całkowitą zmienność 13- 15 dB z czasem, sezonem i czasem.
- Metoda Yeh, najprostsza do zaprogramowania (f w MHz, d w milach, kąt θ rozproszenia w stopniach, średnia geometryczna α otworów obu wiązek):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Porównanie Larsen (1968) na 15 połączeniach rzeczywistych: średni błąd kwadratowy 5,1 dB dla Collins, 7,4 dla Yeh, 8,1 dla NBS i CCIR, 16,8 dla Rider. Wszystkie metody poprawiają się, jeśli N utrzymuje się na stałym poziomie (Yeh spada do 5,4 dB): dla rozproszenia refraktywny dzień liczy się niewiele, więc indeks tropu musi patrzeć na warstwy, nie tylko Ns. Tabela 15 połączeń, z mierzonymi stratami (w tym Feldberg- Monte Baldo, 322 km przy 957 MHz, 223,1 dB; Cyprus-Izrael, 319 km przy 77,5 MHz, 163,3 dB), jest dobrym miejscem do przeprowadzania testów dla naszych obliczeń.
- KK6MC daje straty, że typowa stacja potrafi przekroczyć 2 m i odpowiadającą jej odległość, od 184 dB (FT- 817 i 3 elementy w SSB, około 130 km) do 215 dB (długie zbiorniki w SSB, około 430 km) i 236 dB (kW i yagi sprzężone, około 640 km). Jest to sposób, aby przetłumaczyć indeks dB na zarobione kilometry.
- Kod gotowy: W3SZ Program Yeh i Collins (nitehawk.com / w3sz / YehCollins.zip) i Bob Atkins KA1GT kalkulator (bobatkins.com / radio / troposkatter.html).

Kolejne źródło rozproszonego materiału: I. Roberts ZS6BTE, "Tropospheric Scatter Propagation", * Radio ZS *, grudzień 1984, z uwagą 2013 ([qsl.net / zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). Głównie Przydatna geometria: kąt rozproszenia θ = θ0 − θ1 − θ2, z θ0 = d / R, θ1 = (h1 − h\ _ ostacolo) / d1 + d1 / 2 R i to samo dla drugiej stacji, R = 8446 km (4 / 3 promienia Ziemi). Jest to kawałek, który jest używany do obliczania kątów z profilu podłoża w troposcatter "podłogi". Inne dane: każdy kąt przy wyższych kosztach około 10 dB; zmiana z Ns 280 na 300 zmniejsza utratę około 4 dB; objętość rozproszenia wynosi od 300 do 2000 m przez 150 km, od 600 do 3000 m przez 300 km; burze w objętości rozproszenia podnoszą sygnał o około 20 dB. numeryczny przykład artykułu nie wraca z podanymi wzorami (czas wolnego miejsca przy 900 MHz i 872 km wynosi około 150 dB, a nie 125), więc wzory powinny być sprawdzone na W3SZ przed ich użyciem.

2. Rodzaje kanałów (W. R. Hepburn, "Tropospheric Propagation Modes", dxinfocentre.com / propagation / tr- modes.htm, Tłumaczenie hiszpańskie LU7MC). Ukończ tabelę Bean i Dutton z przydatnymi sprawami we Włoszech:

| Typ | Kiedy | Uwaga dla indeksu |
| Napromieniowanie | spokojne i spokojne noce na ziemi | za ziemią, znika krótko po świcie |
| Wysokie ciśnienie (subsidence) | antycyklina | może trwać cały dzień; w nocy przewód od napromieniowania poniżej może blokować odległe sygnały, więc w dzień może być lepiej |
| Przód | przed ciepłym frontem, za zimnym frontem, na północ od nieruchomego frontu | krótki; zła pogoda niepokoi go |
| Schodzenie z gór | föhn, bora: odpływ powietrza, ogrzewanie i suszenie | spróbować Bora na Adriatyku i Föhn na południe od Alp |
| Ostrzeżenie | gorące i suche powietrze nad zimną i mokrą glebą, na przykład po deszczu | Prowadzone na ziemię |
| Dolina | gorące powietrze nad zimnym powietrzem zatrzymuje się w dolinie | może zostać w dzień |
| Marino | gorące i suche powietrze na zimniejszej wodzie; przybywa na wybrzeże z wiatrem | szczyt po południu; wiosna i wczesne lato najlepsze pory roku |

Kanały parowania na morzu (0- 30 m) służą tylko około 3 GHz i nie wchodzą do lądu: dla 144 i 432 MHz można zignorować. Wysokie góry są barierą; pustynie są zbyt suche.

3. Przypadki, których Hepburn nie przewidział (S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", Nordic VUSHF 2019, od JO59IX do 500 m wysokości):

- 22 listopada 2018 r.: 70 cm do UA6 (około 2600 km) przez 6- 8 godzin. Hepburn wskazał tylko państwa bałtyckie i Polskę. Przewód był widoczny na mapach temperatury 900 i 1500 m i zakończył się nieco ponad 2000 m; był wyższy po stronie norweskiej, a tylko LA0BY na wzgórzu wszedł. Na ziemi czas nie wydawał się tropem: zimno i mgła na górze.
- 11 lutego 2015 r.: 2 m i 70 cm w kierunku 9A i S5 (ok. 1500 km) przez Alpy, również tego nie przewidział Hepburn.
- 15 października 2011 r.: 2 m z EA1 (2135 km) w wysokim kanale, na trzech odcinkach morza i trzy podłoża.
- Inne wydarzenia: 6- 11 listopada 2003 na wschód (2050 km), 10 grudnia 2004 r. w kierunku Francji.
- Adresy pól: mgła na równinie, wolne szczyty, mały wiatr, mokry grunt. Normalny gradient temperatury -6,5 ° C / km; w rzeczywistym przypadku ( VK3KAQ) + 7 ° C w 20 m.

Wniosek autora: Hepburn i F5LEN nie wystarczą, służą profilom na wysokości wzdłuż trasy. Dla nas: daty te są przypadkami weryfikacji, które należy ponownie obliczyć z danymi historycznymi modeli, a wskaźnik na ścieżce musi porównać kwotę kanału z kwotą stacji.

4. Doświadczenie Włoch (G. IV3GBO, "Rodzaje propagacji VHF SSB / CW", 2017): prowadzone od 450 do 3000 km, z warstwami od 450 do 3000 m; mgła rozciągnięta jako wskazówka; otwory morskie nad Adriatykiem w kierunku Puglia, Albania i Grecja. Rozproszyć się od 100 do 500 km.

5. Wprowadzenie (P. Wilton M1CNK, "An Imputation to VHF / UHF Propagation", 2013): 70 cm jest często lepsze niż 2 m; przy 10 m biegunie przy 144 MHz kąt początkowy wynosi około 3 °, przy 20 m około 1,5 °.

6. Raport NASA (E. Hirschmann, "Troposferic and Ionospheric Effects on Radio Frequency (VHF- SHF) Communication", Goddard X- 731- 67- 89, 1967): dotyczy błędów odległości do satelitów, a nie DX. Przywołuje on tę samą formułę N i profil wykładniczy (np. na Florydzie: Ns 370, ΔN − 55, stała 0,161 / km) i wysyła dwa źródła NBS do wyszukiwania: Bean and Thayer, "CRPL Exponal Reference Atmosphere", Monograph 4 (1959), and Bean, Horn and Ozanich, "Climatic Charts and Data of Radio Refractive Index for the United States and the World", Monograph 22 (1960).

## 9. Z książki VHF / UHF DX, rozdział 2

I. White G3SEK (edited by), * The VHF / UHF DX Book *, DIR Publishing 1995 (ISBN 0 9520468 0 6; digital replica 2017), chapter 2 "VHF / UHF Propagation", pp. 2- 13, 2- 22- 2- 29 i 4- 11. Jest to tekst najbliższy doświadczeniu amatora radia wśród zebranych; potwierdza progi już używane i dodaje nowe zasady synoptyczne.

Numer

- Ten sam wzór N (77.6 P / T + 3.73 · 10 ^ 5 e / T2); przykład: 1000 hPa, 7 ° C i = 10 hPa uszkodzenia N = 324. W dobrze ukształtowanej atmosferze, na przykład po burzy, N jest prawie liniowy około 39 jednostek / km w pierwszym 2-3 km.
- Gradienty od 0 do -79 N / km są normalne, poniżej -79 występuje nadrefrakcja, ponad 0 subrefrakcji. Książka nalega, aby długi DX nie pochodził z prostego super-załamania, które powinno pozostać na całej ścieżce, ale z kanałów.
- Minimalna grubość przewodu, nieco bardziej roztropna niż Kerr i Hepburn:

| Zakres | Minimalna grubość |
| 50 MHz |  400 m  |
| 70 MHz |  300 m  |
| 144 MHz |  200 m  |
| 432 MHz |  100 m  |
| 1.3 GHz |  50 m  |

- Stosunek nie jest proporcjonalny do długości fali (około 65 λ przy 50 MHz, 220 λ przy 1,3 GHz), ponieważ krawędzie przewodu są "podzielone". W Wielkiej Brytanii rurociągi są często na 144 MHz, ale prawie nigdy na 70 MHz lub poniżej, i często 432 MHz są silne, gdy 144 są słabe.
- W rurociągu sygnał spada jako 1 / d zamiast 1 / d2: do 1000 km tłumienie może być podobne do wolnego miejsca.
- Minimalna pojemność stacji na 2 m (100 W, 15 dBi yagi, 2 dB szumy): 217 dB, to jest około 400- 500 km w SSB w warunkach średnich; w CW około 20 dB w więcej (s. 4- 7). Zgadzam się ze stołem KK6MC.
- Rozproszenie: strata wzrasta o około 15 dB trzykrotnie i 10 dB dla każdego stopnia kąta rozproszenia (wzór Yeh; w tekście ekstrahowana stała jest nieczytelna, więc musi być pobrana z wersji W3SZ). Rozprysk "up" z komórek konwekcyjnych obejmuje rutynowo do około 500 km, 1000 km z dobrych stacji. Po ciężkich deszczach i wydłużonych warunkach jest mało na wszystkich zespołach.

Rodzaje kanałów

- Na ziemi: na ziemi rozprzestrzeniają się one źle, ponieważ gleba absorbuje i rozprzestrzenia; na spokojnym morzu dotrzeć daleko (Scilly- Canarie, 2500 km, około raz w roku na 2 m). Kanały parowania są często tylko około 15 m wysokości i służyć mikrofale.
- Wysokie: od 450 do 2000 m, ze względu na dłuższy DX na ziemi; sygnał "wysoki" powyżej stacji poniżej. Kanały "przegrywające" dają mniej silne sygnały, ale również pozwalają na stacje pośredniej odległości.
- Przewody mają dokładne kierunki i mogą trwać godziny lub dni, w przeciwieństwie do sporadycznych E.

Nowe zasady synoptyczne dla indeksu

- Wiek antycykliny: powietrze spada do centrum kilka metrów na godzinę, a odwrót jest obniżany w miarę rozwoju wysokiego ciśnienia; większe otwory przybywają po kilku dniach rozmnażania już dobre. Krótkoterminowe antycykliny nie wystarczą.
- Wysoka krawędź ciśnieniowa: warstwa jest kopułą, a na krawędziach jest niższa, więc stacje stają się lepsze.
- Kierunek: Zaobserwowano, że dłuższe trasy biegną wzdłuż isobares lub przez grzbiety (Flavell G3LTP, 1984), ale istnieją doskonałe połączenia również w kierunku radialnym od centrum. Przykład 29- 30 sierpnia 1987: z Anglii otwarcie obróciło się o 180 ° w dwa dni (Hiszpania, Francja, Włochy, Austria, Norwegia) po zmianie antycyklonu.
- Przeciw: gęsty izobare (silny wiatr), strutokumuliformowe chmury na skraju warstwy (ograniczenie najwyższej częstotliwości).
- Granice: zimny front wypycha gorące powietrze i tworzy rewers wzdłuż przodu; najlepszym kierunkiem jest wzdłuż przodu podczas przechodzenia, ale trwa niewiele.
- Sezon: W Wielkiej Brytanii antycykliny są częstsze pod koniec lata i wczesnej jesieni.
- Wskazówki wizualne: dym uwięziony w ciemnej linii nad horyzontem; przy wysokim ciśnieniu, laurkumuły o podstawie poniżej 1000 m wskazują kanał gruntu, podstawa powyżej 1000 m wysokiego kanału.

Referencje cytowane do wyszukiwania: R. G. Flavell G3LTP, "Studies of an Extensive Anticyclonic Propagation Event"..., * Radio Communication *, Luty 1984, p. 128; J. Gannaway G3YGF, "Tropospheric Scatter Propagation", * Radio Communication *, Wrzesień 1981, p. 710; P. W. Sollom G3BGL, "A Little Flutter W6ZGN

## 10. Zweryfikowany amatorski model radiowy: Biancomano i Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, "Practical Tropospheric Model for VHF / UHF Propagation Prediction", * National Weather Digest * 22: 2, June 1998, pp. 15-24, free in [PDF from the National Weather Association](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf).

- Dane: fala radiowa 00 i 12 UTC 47 stacji we wschodnich Stanach Zjednoczonych, 1995- 1997; analiza gruntu do 700 hPa (na początku do 400), na profilu N, bez M. Wyłączając wybrzeża, wyspy i góry.
- Dla każdego badania: obecność i udział kanału, maksymalna długość fali, która może prowadzić (często około 100 razy długość fali), maksymalny gradient potencjalnej temperatury między sąsiednimi poziomami i między jednym poziomem a drugim powyżej (dla grubszych warstw). Przewody naziemne nie są używane.
- Pierwsza zasada, trwałość: wskaźniki stabilności (podwyższony indeks, K, sumy całkowite, SWEAT) pozostają w granicach 3 jednostek od badania 12 godzin przed, a poziom ciśnienia, gdzie potencjalna temperatura rośnie bardziej pozostaje mniej więcej taki sam. Jeśli tak: prognoza poprawy w ciągu najbliższych 12 godzin.
- Druga zasada, kontrola dynamiczna: warstwa graniczna, która nie wzrasta, CAPE, zmienność grubości do 500 hPa na trzech badaniach, zmienność nachylenia w granicach.
- Próg pasma: przewód zdolny do przenoszenia 100 cm lub więcej wskazuje na otwarcie na 432 MHz, prawie pewne na 222, a często nawet na 144.
- Filtr kosmiczny: stacje na sieciach sześciokątnych o długości około 400- 500 km; prognoza jest warta tylko wtedy, gdy co najmniej 5 stacji contigue jest pozytywnych (jeśli można pominąć jeden). Przy 5 stacjach prawdopodobieństwo fałszywego wyniku losowego staje się znikome.
- Weryfikacja wydarzeń w kolumnie "Świat powyżej 50 MHz" QST: w 1996 r. zaplanowano 48 wydarzeń na 67 (71%); 12 z 15 najważniejszych wydarzeń. Dokładność do 70% na 432 MHz i około 60% na 144 MHz w 12 godzin; zapewnia prawie dwa razy więcej rzeczywistych zdarzeń; 9 błędów z 19 są przewidywania przyszedł późno, ponieważ wiele ulepszeń tworzą w mniej niż 12 godzin.
- Dodano do stacji dalej na północ: wiatr obracający się zgodnie z ruchem wskazówek zegara (gorący start) jako dodatkowy warunek.

Z pełnego PDF (dostarczone przez autora programu) trzy więcej szczegółów. Ich reguła "grubość = 100 razy długość fali" daje 600 m na 6 m, 200 m na 2 m, 125 m na 1,25 m i 70 m na 70 cm (rys. 3): dla 70 cm jest mniej dotkliwa niż Kerr i Hepburn (około 89 m), dla 2 m pokrywa się z VHF / UHF DX Book. Wiatr obracający się zgodnie z ruchem wskazówek zegara idzie w górę, według autorów, szczególnie dlatego, że przynosi cieplejsze powietrze na wysokości, co sprawia, że upadek N rośnie z naciskiem; wilgoć, która przynosi z nim nie wydaje się destabilizować. Dodatek uzyskuje dN / dh w postaci zamkniętej dla stałej atmosfery gradientu termicznego z ostrym skokiem gradientu ciepła i wilgotności w określonej ilości: przydatna jako przypadek testowy dla naszego kodu. Ich ślad nachylenia potencjalnej temperatury pochodzi z K. H. Jehn, "The use of potential refractive index in synoptic scale radio meteorology", * J. Meteorology * 17, 1960, p. 264.

Co wziąć za nasz indeks: (1) termin trwałości: porównać profil z profilem 12 godzin przed, że w godzinnej prognozie mamy wolne; (2) filtr przestrzeni: punkt liczy się tylko, jeśli poprawa obejmuje obszar kilkuset km, co zmniejsza fałszywe alarmy; (3) wskaźniki stabilności, obliczone od temperatury i rosa do 850, 700 i 500 hPa (Open-Meteo już daje podniesiony indeks i CAPE); (4) wiatr, który obraca się z wysokości między 1000 i 850 hPa; (5) ich metoda weryfikacji, która jest taka sama, jak w reklamach programu.

Kolejne źródło w tym samym opakowaniu, mało przydatne dla nas: P. K. Rastogi, "Stratosphere and Troposphere (S- T) Studies at Millstone Hill", NASA 1984 (document 19840019069): 440 MHz radar, który mierzy turbulencje czystego powietrza pomiędzy 4 a 25 km, z cienkimi warstwami związanymi z falami grawitacyjnymi i cięciem wiatru. Potwierdza, że turbulencje, które wytwarzają rozproszenie są warstwowe, ale dotyczą zbyt wysokich szans na kanały.

## 11. Trzecia runda źródeł (20 listopada)

Wysokie kanały historyczne przypadki. J. Macoun OK1VR, 2 m od szczytu Sněžki (1602 m), jesień 1958, artykuł z 1959 przetłumaczony w 2006 ([ok2kkw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)). W dniach 27- 28 października 1958 roku pierwsze połączenie Czechosłowacji z Anglią na 145 MHz ( G5YV, 1220 km) i wiele więcej pomiędzy 1100 a 1250 km, ze stabilnymi sygnałami po 23. Autor przypisuje go dwóm narzuconym inwersjom, górnym około 2200 m, z sygnałem powracającym do ziemi tylko wtedy, gdy niższa warstwa się rozpuszcza; stan preferowany przez powolny antycyklin z Atlantyku na kontynent. Z domu (Praga) jej maksimum było 530 km: tylko stacje na wysokości wszedł do kanału. Jest to kolejny przypadek weryfikacji (taki jak LA0BY 2018) i potwierdza, że indeks powinien wyglądać na ponad 2000 m i porównać go z udziałem stacji.

Biblioteki historyczne NBS (przydatne w poszukiwaniu klasycznych dzieł, nie formuł):

- W. Nupen, * Bibliography on Tropospheric Propagation of Radio Waves *, NBS Technical Note 304, 1965, 356 pages, with index for themes (also "condition presence") and by country. Włoskie wpisy zgłosiły: L. Sacco, "The radio connection in sur- refraction regime", * High Frequency * 24 (6), Grudzień 1955 (włoskie badania: super- refraction under 500 m specific day and night); S. Silleni, * Annali di Geofisica * 8 (1), 1955; J. M. Clara e A. Antinori, prova tra Sardegna e Minorca da 238 a 297 MHz (390 km), * Electrical Communication * 33 (2), 1956. A. F. Clarke (Fort Huachuca, 1956) wydał dzienną prognozę maksymalnej długości fali uwięzionej na następny ranek, od wilgotności, temperatury, ciśnienia i prognozy wiatru.
- R. L. Abbott, * Bibliography of Tropospheric Radio Wave Scattering *, NBS, material until January 1960, about 670 pozycji zamówionych rocznie: przydatne dla części troposcatter.

Ujawnienie i obserwacje: F. W. Odenthal DK5EQ, "Ausbreitung 2 m - Eine Einführung" (31 / 05 / 2024): prawie optyczne do 200 km, tropo na kanał lub refrakcji poza, z wysokim ciśnieniem i wiosną i jesienią; cytuje stronę VHF DM2HB i notatki DK5YA na tropo i Es. A. Johnson VK3DXE na blogu EI7GL (grudzień 2021): z WSPR VHF ścieżki tysięcy kilometrów w Australii zostały odkryte; Trope jest nie tylko latem. Dyskusja na temat mariny VHF i AIS ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)): zakres 1,23 mil morskich (ft) na antenę, rozproszyć do około 50 nm, typowa poprawa Trope 100- 400 nm; przewody są rozpoznawane, ponieważ odległe stacje są tak silne jak sąsiedzi. Chelmsford ARS, grudzień 1983: w konkursie 2 m stacje wschodniego wybrzeża Anglii są uprzywilejowane drogą morską na kontynent.

ARRL, * The Radio Amateur 's VHF Manual *, 1972, cap. 2 "Propagation Beyond the Horizon" (s. 17- 19). Już znane wskazówki eksploatacyjne (postępuj w obszarach wysokiego ciśnienia, które poruszają się powoli i przede wszystkim ich tylnych krawędzi; wczesna jesień najlepszy okres; obszary przybrzeżne, duże jeziora i doliny rzek preferowane przez stratyfikacji). Dwa nowe dane: (1) fig. 2-4 porównuje standardową atmosferę (wilgotność względna 70% do 12000 stóp, minimalne załamanie) z typowym promieniem silnego załamania, z wyraźnym współczynnikiem zerwania temperatury i mieszania około 4000 stóp (około 1200 m): jest to podpis, który nasze obliczenia muszą rozpoznać na poziomach GFS; (2) prawdziwe kanały są rzadkie poniżej 144 MHz i najczęściej w UHF, według minimalnych grubości. Przydatna bibliografia QST do wyszukiwania: Hull, "Air- Mass Conditions and the Bending of UHF Waves" (June 1935, May 1937); Hoisington, "Painless Prediction of Two-Meter Open" (October 1949); Collier, "Upper- Air Conditions for 2- Meter DX" (September 1955); Botts, "A Night to Remember" (A Night to Remember) (January 1970, przeprowadzone na lądzie).

PA0EHG, strona "Tropo" ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): podsumowanie troposcattera z artykułu G3YGF, już w podręczniku. W celu kontroli obliczeń rozproszenia należy zastosować dwie praktyczne zasady: około 10 dB dodatkowej straty dla każdego kąta na horyzoncie każdej stacji, a ponad 100 km około 9 dB plus co 100 km trasa (60 mil); wspólna objętość zaczyna się od około 600 m na 100 km i od około 9 km na 480 km. Nie leczy kanałów, sondaży ani progów refrakcji.

PA0EHG, kalkulator PROPBER ([strona](https://pa0ehg.com/propagat1.htm), program propber.exe dostarczony przez autora programu). Jest to 36 KB DOS program napisany w BASIC i skompilowany z Microsoft BASIC Compiler 5.60 (1985): działa tylko pod DOS lub DOSBox. Z tekstów wewnętrznych: prosi o częstotliwość, odległość (lub dwa lokalizatory, również w starym formacie QRA "E- lokator"), dla każdej stacji wysokości anteny, szumu, pasma FI, mocy, przeszkody (wysokość i odległość) i przyrost anteny (0 = obliczenia średnicy przypowieści, wydajność 50%). Zwraca maksymalną odległość w optyce, prawidłową utratę optyczną dla absorpcji pary wodnej (0,1 dB / km od 24 GHz), utratę troposkattera "poprawnego dla miejsc" (tj. dla kątów horyzontu podanych przez przeszkody) oraz stosunek S / N w dwóch zmysłach, również "przy maksymalnym nienormalnym rozmnażaniu". Stałe numeryczne nie są czytelne w skompilowanym wykonaniu. Sugerowana metoda dla dużych pasm jest również przydatna dla nas: kalibracja obliczeń na mierzonym ogniwie (różnica między zmierzonym i obliczonym S / N) i zastosowanie tej samej różnicy do innego pasma. Jest to ten sam wzór naszych obliczeń, ale używa profilu S RTM1 zamiast jednej przeszkody i prognozy GFS zamiast stałego marginesu.

PROPBER, stałe odczytywane w pliku wykonywalnym (numery w formacie Microsoft Binary Format). πre, w tabeli danych obok tekstów: 8,68589 (= 20 / ln 10, tj. 20 logarytmów obliczonych logarytmem naturalnym), 32,5 (wolna przestrzeń: 32,5 + 20 log f\ [MHz\] + 20 log d\ [km\]), 24000 i 0,1 plus 40000 i 0,3 (pary: 0,1 dB / km od 24 GHz, 0,3 dB / km od 40 GHz) Prawdopodobne, ponieważ powtarzasz, ale wewnątrz kodu: − 69,5 (takie samo stałe określenie jak wzór rozproszenia bocznego Yeh w VHF / UHF DX Book), 332 (refraktywny grunt), 8733 km (równoważny promień, k RR1.37). Dokładna formuła rozproszona nie jest rekonstrukcyjna bez demontażu programu. T0

PROPBER, formuły zrekonstruowane przez kod (demontaż skompilowanej części; operacje są wywoływane do biblioteki BASIC z operatorami w pamięci, których wartości w podwójnej precyzji zostały odkodowane). Stałe -69,5, 332 i 8733 znalezione wcześniej były fałszywe pozytywy w bibliotece.

- Odległość optyczna: d = 5 · (Δh1 + Âh2) km (h w m). Jest on bardziej hojny niż klasyczny 4,12 (k = 4 / 3): jest odpowiednikiem k k = 2.
- Utrata optyki: L = 32,5 + 20 log f\ [MHz\] + 20 log d\ [km\], plus 0,1 dB / km powyżej 24 GHz i 0,3 dB / km powyżej 40 GHz.
- Troposcatter: Lts = L + 23 + 10 log f + 0,0674 · d, tj. 55,5 + 30 log f + 20 log d + 0,0674 · d (wzór G3YGF ze stałą 2 dB wyższą). Brak określenia kąta lub reaktywności.
- Korekta "dla stron": Program prosi o przeszkodę i odległość, ale blok, który powinien ich używać jest pusty; zmienna korekcji pozostaje 0, więc "poprawna" utrata jest równa rozproszeniu.
- Hałas: T = 290 · (10 ^ (NF / 10) -1), N = -228,6 + 10 log T + 10 log B (dBW), bez temperatury anteny.
- S / N = 10 logów P + G\ _ tx − L + G\ _ rx − N. "Przy maksymalnym nienormalnym rozmnażaniu" wykorzystuje straty optyczne zamiast rozproszenia, tj. idealny przypadek przewodu bez strat poza wolną przestrzenią.

Porównanie: 300 km i 144 MHz PROPBER daje około 189,9 dB, G3YGF 187,9, nasz P.452 187,8 (z zerem kątów horyzontu): rozproszenie zbiega się w granicach 2 dB. Nasze obliczenia dodają kąty horyzontu z profilu, temperatury anteny, a zamiast "maksimum" ustawił oczekiwaną warstwę z pozostałą krycia i tłumienia.

Rohde & Schwarz, P. Denisowski, "Understanding VHF Propagation", Educational Note, April 2023 (24 strony, rozpowszechnianie). Potwierdza punkty już zebrane (prowadzone jako dwuwymiarowy falownik, sygnał, który spada w sposób liniowy z odległości, do 1500 km i dalej, czas trwania od godzin do dni; dobry grunt na morzu i rzadkie na lądzie; podniesiony z kilkuset metrów do kilku km). Dwa użyteczne obserwacje dla indeksu: (1) ewolucja w czasie: nowo utworzony kanał często ma tylko najwyższe częstotliwości i, zgrubienie, zaczyna przynieść nawet najniższe; godzinowa prognoza powinna zatem pokazać 432 MHz "z wyprzedzeniem" na 144 MHz, gdy warstwa rośnie; (2) lokalizacja stacji: najlepsze warunki są między stacjami blisko końca przewodu, podczas gdy te na ścieżce wysokiego przewodu często nie wchodzą, chyba że prowadzone "którzy przegrywają". Kanały poruszają się z pogodą atmosferyczną.

Strona PA0EHG, inne strony. Dla tropu, PathProfil M. Willisa G0MJW ([scheda](https://pa0ehg.com/srtmp.htm), [download](http://www.mike-willis.com/Software/ASTERPathprofile.zip)): profil trasy z oczekiwanych całkowitych strat i kwot plików, wiarygodnie ocenione dla troposcatter w mikrofalówce; nie traktuje rozproszenia z samolotów lub deszczu. Przydatne jako drugie odniesienie do porównania naszych profili i strat S RTM1. Pozostała część terenu obejmuje EME, mikrofalówkę, lotnisko (AirScout, mapy SM7LCB) i mierzy w 80 m.

PathProfile by G0MJW (Mike Willis), ASTER and SRTM packages and SiteData (support by the author of the programme; [](https://www.mike-willis.com/software.html)software page). Program Windows 2018: profil z ASTER lub SRTM do 1 "lub 3", utrata optyki, dyfrakcji, troposkatter (ważne do 1000 km), kanały i gazowe, z odsetkiem czasu, mediana trasy, ułamek ścieżki na morzu i odległości od wybrzeża: struktura jest z P.452 / P.1812, ale pomoc (skompresowany plik.chm) nie może być tutaj przeczytana. SiteData konwertuje lokalizatory i daje udział z GT OPO30.

Najbardziej przydatne są pliki danych zawarte: mapy statystyczne ITU- R P.453 na globalnej siatce 1,5 ° (121 wierszy od 90 ° N do 90 ° S, 241 kolumn od 0 ° na wschód): gradient dN / dz w pierwszym 65 m przekroczony dla 1, 10, 50, 90 i 99% czasu; dla kanałów glebowych procent obecności, grubości i intensywności; dla tych wysokiej obecności, udział bazowy, grubość, intensywność i maksymalna wysokość; plus wilgotny termin i temperatura. Interpolowane wartości roczne:

| Lokalizacja | Gazociąg lądowy:% / grubość / intensywność | Gazociąg:% / podstawa / grubość / intensywność | dN / dz przekroczył 10% czasu |
| Neapol |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | − 380 N / km |
| Tyrrenian (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | - 674 N / km |
| Trapani |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | − 520 N / km |
| Brindisi |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | − 405 N / km |
| Adriatyk (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | − 399 N / km |
| Mediolan |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | - 172 N / km |
| Udyna |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | - 176 N / km |

Dla naszego indeksu są gotowe odniesienia klimatyczne: grubość i intensywność typowe dla kanałów zamiast 50 m stałe potencjalnego kanału, i anomalia "obecnie w porównaniu do normalnej" w każdej strefie. Dane ITU: bezpłatnie do użytku niekomercyjnego, do sprawdzenia przed włączeniem ich do wersji publicznej. Plik ZO NES01.DEM (58 MB, 2 'siatka) zawiera strefy klimatyczne śródlądowe / wybrzeże / morze; format nie jest udokumentowany i nie dekoduję go. Strona równań Willisa zawiera tylko fizyczne stałe, wolną przestrzeń i przyrost anteny.

Czwarta runda źródeł (20 listopada).

Indermuehle i Suzuki 2026, prognoza rurociągów GFS dla obserwatorium CSIRO Murchison (arXiv 2608.12861): jest najbliżej źródła do LIVETROPOVHF. Profile N od GFS do 0,25 ° (powierzchnia do 2 m, następnie poziomy izobaryczne, te pod odrzuconym podłożem); dla każdej minimalnej kolumny dM / dz poniżej 3 km, podstawa i szczyt przewodu, zintegrowany deficyt M; następnie P.452- 16 i P.1812- 6 z GLO- 90 25 długości od 98 do 1005 km, od 88 MHz do 2,68 GHz, co godzinę. Wyniki: Mediana AUC 0,72 na 25 kanałach; procent czasu P.452 p powinien być stosowany jako stała kalibracyjna (wybór p = 20%), ponieważ model ITU z natychmiastowym ΔN nie jest zdefiniowany w przewodniku; umiejętności pozostają prawie płaskie do 3 dni wcześniej. Przy 162 MHz (AIS) odbierane trasy mają najwyższy ułamek przewodu 67% (0,26 wobec 0,16) i najwyższą głębokość kanału 49% (28 vs 19 M) w odniesieniu do urządzeń sterujących: na morzu przewód jest prawie zawsze, decyduje o rozciągnięciu terenu. GFS widzi połowę czasu na ziemi w porównaniu z ERA5 (4,5% w porównaniu z 10,4% w komórkach - godzin), podczas gdy na morzu widzi więcej (73% w porównaniu z 56%). Wskaźnik "korytarza" (rurociągi ciągłe wzdłuż trasy, warstwy o minimalnej grubości 40 m) poprawia wynik: potwierdza, że ciągłość na trasie jest właściwym kryterium.

Longley i Rice, ESSA ERL 79- ITS 67 (1968): metoda nieregularnego terenu od 20 MHz do 20 GHz (po ITM), z nieprawidłowościami glebowymi Δh, N od 250 do 400, równoważny promień 8497 km (4 / 3), horyzonty, dyfrakcja, rozproszenie do przodu i zmienność w czasie i miejscach dla klimatu. Odniesienie statystyczne, nie natychmiastowe.

ITU- R P.834-2: krzywizna promienia, równoważnik promienia Ziemi k · a, M = N + h / a (h w m, w tysiącach km), model wykładniczy; nic nowego niż to, co już zostało użyte.

AGARD CP- 70 (1970), rozmnażanie troposferyczne: cienka warstwa od 1 do 100 m z silnymi gradientami są wspólne; współczynnik odbicia wysokiej warstwy jest w przybliżeniu proporcjonalny do długości fali, z losową dyspersją około 14 dB. Pomiary przy 90 MHz: spadek o 9- 10 dB związany z skokiem N skoncentrowanym na około 350 m, na szczycie warstwy chmur. Przejście zimnego frontu eliminuje warstwy: Dyfrakcja pozostaje.

Raport URSI USA, Komisja 2 (1960): średnia grubość wysokich warstw od 60 do 210 m (podana zgodnie z P.453), minimalny gradient 157- 160 N / km.

Buscemi K2OVS, prowadnica propagacji VHF: typowe odległości dla odwrócenia i kanału dla pasma, kanały połączone z wysokim ciśnieniem stacjonarnym, maksimum o świcie.

Wolinsky- Mancini i 2024, AIS na Wyspach Kanaryjskich: AIS radiosundacje i przyjęcia; kanały są znacznie częstsze w ciepłych miesiącach (inwersja alizean).

Artykuły nigeryjskie i malezyjskie (Ekah, Iloke, Mat, Igwe): lokalne korelacje między temperaturą, wilgotnością i zmierzoną stratą, słabe i sprzeczne (r − 0,09 do 0,27); przydatne tylko jako potwierdzenie, że same rozmiary gruntu nie wystarczą.

Herben 1984, teza TU Eindhoven (microonde): 30 GHz na 8,2 km i satelita OTS na 11,6 GHz; błysk dla turbulencji, ścieżki wielokierunkowej i deszczu. Dla mikrofale dynamiczne straty są zdominowane przez deszcz i turbulencje, nie przez kanały.

Baars, Paraboloidalny Reflektor Antenna (microonde): uzyskanie przypowieści G = η · (πD / λ) 2 o typowej sprawności 0,5- 0,65; utrata błędów powierzchniowych według Ruze, ηs = exp (− (4πε / λ) 2). Pomogłoby, gdybyśmy dodali przypowieści jako alternatywę dla Yagi DL6WU dla 1296 MHz i dalej.

Już przeanalizowane lub o złej zawartości: Hirschmann NASA X- 731- 67- 89 (podwójne), Reid VHF ST radar (wiatry i turbulencje, nieprowadzone), Scribd slajdów i ogólne uwagi na troposfery.

Piąta runda źródeł (20 listopada, wieczorem).

Rogers 1997, Radio Science 32 (1): Bayesian estimation of the parametrics of a high dual rate (base rate, M defit, grube) from propagation factors measured beyond the horizon, with a parabolic equality model; VOCAR experiment in California at 143, 263 and 375 MHz, radio wave comparison. Sygnał zależy dużo od podstawy i deficytu M (skorygowanie 0,81 do 143 MHz dla deficytu), prawie wcale z grubości (-0,17): grubości nie można uzyskać z sygnału. Dla nas: obliczenie B (grubość klimatu) liczy się mniej niż deficyt i udział; a metoda jest podstawą dla back office, czyli do uzyskania przewodu z wielu częstotliwości.

Moler i Holden 1960, "Troposferic scatter propasation and contromic circulations", J. Res. NBS 64D, pp. 81 and follow (read in original on 22 / 11 / 2026, doi 10.6028 / jres.064d.014). Kalifornijskie trasy morskie do Point Loma (San Diego): Santa Barbara 190 mil, Point Mugu 144, San Clemente 78; 1365 MHz i 9380 MHz, nie VHF; January- kwiecień 1958, średnio 1 minuta. Przypadek z dnia 26 marca: wzmocnienie o około 20 dB przez około 3 godziny w kierunku południa, gdy wir cykloniczny osłabia i rozpoczyna bryzę morską i uległość; przy 15 konwergencja jest zreformowana i sygnał spada o 22 dB od maksimum. Statystyki (tabela 1, 500 hPa, 33 przypadki): z powietrzem, które spada średni sygnał jest wyższy niż około 2,5 dB niż z powietrzem rosnącym, w paśmie L i X. Sygnał odpowiada różnicy obliczonej do 500 hPa w znaku i amplitudzie zmian, bez współczynnika korelacji. Niskie warstwy (poniżej około 600 m) sprzyjają częściowemu odbiciu rozpraszania. Antycykliny i fronty nie są traktowane, a "wycieczki do 60 dB" wymienione powyżej nie są: pochodzą z wtórnego podsumowania. Dla nas: ruch pionowy jest dobrym przewidywaczem, a + 1,5 dB dla subsidencji i -2 dB dla powietrza podwieszanego są tego samego rzędu co średnia zmierzona różnica (2,5 dB); 20 dB to pojedynczy przypadek, nie typowa wartość, i na mikrofalówce.

Banafaa i Muqaibel 2025, IEEE Access: badanie rurociągów (modele parowania, równanie paraboliczne, szacunki radarowe, GNSS i lidar) oraz klasyfikacja z automatycznym uczeniem się o rzeczywistych środkach; Maszyny wektorowe wsparcia dają najlepsze wyniki. Przydatne jako bibliografia; dane pochodzą z Zatoki Perskiej.

ITU- R P.834- 7 (2015): zaktualizowana wersja zalecenia w sprawie refrakcji troposferalnej; brak nowego wzoru niż ten już zastosowany.

Abbott, bibliografia NBS na rozprysku troposferalnym (GOVPUB- C13): już w trzeciej rundzie. Slide Scribd na kanałach: zawartość ogólna. Artykuł ScienceDirect 0021916987900304 Nie można go otworzyć.

Szósta runda (20 listopada, noc): Fasola i Dutton, Podręcznik ARRL 2022 i 2023, Balanis.

Fasola i Dutton, Radio Meteorologia (cap. 6), części jeszcze nie używane. Czułość pola na N: 0,18 dB na jednostkę N w cyklu rocznym; 0,14- 0,24 dB / N w cyklu dziennym (maksymalnie na początku rano), do 0,2- 1,1 dB / N na trasach 175- 200 km w zimie; korekty 0,8- 0,95. Parametr składający się z Misme: M = a (g\ _ e − 40 + b · ΔW ^ n) z a = 0,5 dB na N / km, który łączy równoważny gradient i stabilność termiczną ΔW (praca w celu podniesienia powietrza o 1 km): w Saharze przy 471 MHz na 160 km jedyna stabilność wynosi 20 dB między styczniem a czerwcem przy prawie równym gradiencie. potencjalny wskaźnik załamania K (N zgłaszany adiabetycznie do 1000 hPa): podkreśla masy powietrza i antycykliczne subsidence (Flavell i Lane).

Podręcznik ARRL 2022 (99a ed.), § 19.4.3- 19.4.4. Przydatne inwersji między 250 a 2000 m; inwersji promieniowania (surowica, spokojne powietrze, sucha gleba) rzadko prowadzić prawdziwe; powolne antycykliny dają najbardziej rozległe kanały subsidence, z inwersjami od 500 do 3000 m, silniejsze w godzinach wieczornych i rano; na półkuli północnej najlepsze drogi są na południe od centrum. Hot front: dobre warunki kilkaset km przed; Zimny front: silny, ale niestabilny rewers za frontem, równolegle. Foehn (także w Alpach) i wiatr ziemny (chłodne powietrze na ziemi, gorące na 200- 300 m; rozciąga się ponad 800 km wzdłuż wybrzeża). Scirocco: gorące i suche powietrze z Sahary nad chłodnym morskim powietrzem, bardzo silne inwersji od wschodniego Morza Śródziemnego do Gibraltaru, prawdopodobna przyczyna rekordów ponad 1500 km w mikrofalówce. Troposcatter: teoretyczny limit około 800 km, maksymalna typowa połowa. The 100th ed. (2023) ma krótszy tekst: inwersji o wschodzie słońca i zachodzie słońca do + 20 dB i + 20- 50% natężenia przepływu; 1200 mil na Wielkich Jezior, typowy 400- 600; zaleca Radio Mobile (model ITM) pokrycie i mapy Hepburn.

Balanis, Antenna Theory (4a ed.), § 10.3.3: Viezbicke table (NBS TN 688, 1976) for Yagi optimized at 400 MHz: boom 0.4 / 0.8 / 1.2 / 2.2 / 3.2 / 4.2 λ → 7,1 / 9,2 / 10,2 / 12,25 / 13,4 / 14,2 dBd. Nasza formuła DL6WU (8.6 + 8 log10 boom / λ) daje 1,7 dB mniej od 0,8 do 4,2 λ i powraca zgodnie z tabelami DL6WU do 9,5 λ: ostrożne oszacowanie. Przy prawidłowym wzorze na oryginale (9,4 + 7,8 log10 boom / λ, od 22 listopada) odrzuty spadają poniżej 0,2 dB pomiędzy 1,2 a 4,2 λ (12,1 dBd do 2,2 λ wobec 12,25) i pozostaje 0,6 dB do 0,8 λ.

Siódma runda (21 listopada): QST, QEX, W3SZ, repozytorium GitHub.

QST 2000- 2025 (14 numerów, 10 łóżek z rozpoznawaniem tekstu). Jedynie w sekcji The World above 50 MHz, przydatne w przypadkach walidacji modelu: 5 września -9, 2000, przeprowadzone na 144 MHz i dalej na wschodniej połowie USA; 14- 15 sierpnia 2022, otwierając Manitoba - Kansas / Missouri pod wysokim ciśnieniem w gorący dzień, od 144 do 1296 MHz prawie wszystko w FT8, rekord kanadyjski na 10 GHz 1228.56 km ( VE4MA - N0OY) w Q65, sygnały do 1941 km; mapa APRS propagacji pokazał ten sam obszar. QST listopad 2022 zaleca mapy Hepburn. Brak artykułu technicznego dotyczącego prognozy tropo.

QEX i K1JT, Franke i Taylor (QEX 2020) w trybach FT4/FT8: progi dekodowania FT8 - 20,8 dB w 2500 Hz na kanale bez evanescence, -18,6 dB z evanescence na średnich szerokościach geograficznych; FT4 - 17,5 dB. Pozostałe numery QEX 2023 i 2026 nie leczą tropu (trzy numery 2023 są nieprzeczytane). W3SZ 2019: progi w trybie wolnym ( JT4A − 23, JT9A − 27, JT65A − 25, QRA64A − 26, FT8 − 21 dB) oraz kryteria wyboru pomiędzy tolerancją czułości a tolerancją zmiany częstotliwości.

GitHub. TropoLink (LukiBox, Apache- 2.0): projekt tras troposkatterowych z P.617- 5 (z N0 i ΔN), NBS TN101 i ITM otoczony, a ich różnica wykazana jako niepewność; P.617- 5: L = F + 22 log f + 35 log θ + 17 log d + Lc, F = 0.18 · N0 · e ^ (− hs / 7,35) − 0.23 · ΔN, Lc = 0.07 · e ^ (0,055 (Gt + Gr)). andi- f / troposkatter ( DL2FZN 2013, od VHF / UHF Handbook RSGB): Lts = 24,44 + 4,283 ln f + 0,06656 d powyżej wolnej przestrzeni, nachylenie w pobliżu PROPBER (0,0674 dB / km). QRadioPredict: ITM i ITWOM 3.0 z S RTM3, stosowanie w glebie Corins i schematy NEC. Nie dotyczy: Tropos (aplikacja pogodowa), opera- adt / tropo i troposim (opóźnienie troposferowe dla InSAR), tgbot\ _ troposcatter (stacja wojskowa), sdr- troposcatter- gui (panel sterowania), Troposcatter- Propagnation- Delay (opóźnienie ERA5), Radio- Komunikacja- Projekt (symulator dydaktyczny), PyLayers (kanał).

Ósme okrążenie (21 listopada): modele NTIA i ITU na GitHub. NTIA / itm (Longley- Rice, ITS): 20 MHz- 20 GHz, utrata wolnej przestrzeni, dyfrakcja i troposkatter o profilu glebowym, terminale 0,5- 3000 m, reaktywność powierzchniowa N0 (minimalizacja zredukowana do poziomu morza, 250- 400), siedem klimatów, stałe gleby i różne tryby lokalizacji 1 widok, 2 dyfrakcja, 3 troposcatter; C + + z.NET Framework 4.7.2 obudowa. Nie modeluje kanałów: służy jako referencyjna "normalna atmosfera" ze zmiennością statystyczną. NTIA / p528 i eeveetza / p528 (MATLAB): ITU- R P.528- 5 dla połączeń z glebą powietrzną, 100 MHz- 30 GHz, 1,5- 20 000 m terminali, p 1- 99%;.NET 4.8 obudowa. Możliwe zastosowanie: równowaga rozpraszania powietrza (dwie rutes- samolot plus sekcja radarowa). eeveetza / p2001 (MATLAB): ITU- R P.2001-6, all- field model z dyfrakcji, troposkatter, anormalne rozmnażanie (zachowanie i warstwy, Lba) i E sporadic, procent czasu 0- 100%; ITU mapy cyfrowe (DN\ _ Median, DN\ _ SupSlope, DN\ _ SubSlope, dndz\ _ 01, TropoClem, h0, surfwv, Es) nie podlegają redystrybucji. Możliwe zastosowanie jako porównanie ITU zamiast P.452, z GFS ΔN zamiast map, jak w badaniu CSIRO; mapy powinny być pobierane przez użytkownika zgodnie z P.453. Walidacja 1.3.0 z 593 QSO (29 / 12 / 2021-2 / 1 / 2022): separacja zdarzeń / porównania 0,79 bez korekt, 0,91 z Misme i subsidence; GFS nie widzi przewodu na morzu na 144 MHz (guadagno bez korekt nigdy więcej niż 15 dB).

Dziewiąta runda (21 listopada): IU3QEZ, NTIA, PyWaveProp, ACE High, walidacja 1.3.1. IU3QEZ (S. Fabris), prezentacja "Przez horyzont w VHF" (GECO Magazine n. 69, Wrzesień 2026): wspólny tom d2 / (8 · R\ _ eff) z R\ _ eff '8500 km (1,3 km d 300 km, 3,7 km do 500 km każdy poziom horyzontu o wartości 69 mil (111 km), margines 7 dB dla evanescence i 4 dB dla odbicia od podłoża; teza: 10 dB wynosi około 100 km; stacja IQ3QC 5 W z 3 × 5 elementami, 120 QSO, maksymalnie 788 km (Alpe Adria 2026). Bibliografia prezentacji do odzyskania: Norton 1959 (J. Res. NBS 63D, System Loss), Bray 1961, ARRL Antenna Book 2023 fig. 4.29, Rice- Longley- Norton- Barsis NBS TN 101, Bean and Thayer 1959, Barsis i Kirby 1961 (difraction on ridge), Deygout 1966, P.1812, AirScout DL2ALF, P.834 NTIA PropLib (wiki): eHata, ITM, LFMF, P.528, P.2108 (loss from bulm), z C + +,.NET, MATLAB i Python; no conduit model; strona ITS: również IF- 77 (airgroup) i MPM (1 GHZ-1 THz). PyWaveProp (Python): Paraboliczne metody równania z przybliżeniem Padé, dyfrakcja wielokrawędziowa, obudowy PETOOL i RAM, kanały troposferowe i wysokie, nieregularna gleba: możliwa podstawa do obliczenia pola z profilem GFS M (z), do sprawdzenia. APRS PropView (Apache 2.0): Mapy propagacji VHF z pakietów APRS, statystyki godzinowe i otwierające alarmy; możliwe źródło danych 144 MHz do walidacji. NATO troposkatter net in Italy (fortifications.net): 900 MHz, 10 kW, 20 m przypowieści; stacje takie jak Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Vulture, Monte Lauro; historyczny przypadek testowy dla P.617. Nie dotyczy: Gupta 2021 (transport stratosferyczny), Ustawa 2023 (ozon arktyczny), Delfin # 590 i Opera- adt / tropo (opóźnienie InSAR), tropo.ino (czujniki gazów arduino), tropo- webapi (telefonia), cloudtools / troposphere (AWS). Walidacja 1.3.1 na 564 QSO tropo: AUC 0,79 (model 1.2), 0,80 (+ kanalizacja miękka), 0,90 (+ misme i subsidence), 0,90 (+ trwałość); kolory przewodów (≥ 16 dB) na 46% dróg otwarcia i 0% porównania.

Dodano do tej samej rundy. mikelytaev / propagacja fali (MIT, Python 3): równanie paraboliczne z przybliżonymi analizami Padé i Numerov, nieregularna gleba, kanały troposferyczne; ładować modyfikowany refractive M (kontyngent, lat, lon) z NOAA GFS i ustawić kanał parowania Monin-Obakhov; brak udokumentowanej funkcji, która zwraca stratę na ścieżce. Jest on najlepszym kandydatem do przyszłych obliczeń pola z profilu GFS M (służyłby usługi zewnętrznej lub tłumaczenia). RP- Sim (AGPL- 3.0, JavaScript): Empirical models for radio networks (Hata, He, Ericsson, two rays), no tropo. RIMFading (GPL- 3.0, OMNeT + +): nieprawidłowości w przepływie dla symulacji sieci, dystrybucja Weibulla, brak tropo. PyLayers: wewnętrzny kanał radiowy, już wyłączony. qradiopredict (kantooon, GPL v2): ITM i ITWOM v3.0 z S RTM3 i Corine, już w siódmej rundzie jako QRadioPredict.

Dziesiąta runda (21 listopada): Antenna Book, Norton, Barsis, nowe źródła danych. ARRL Antenna Book (23a.), cap. 4 ( K9LA): Metoda Bray (QST November 1961) dla zakresu VHF: gładka utrata Ziemi na 99% (300 mil, 144 MHz: 214 dB), krzywe na 50% i 99% (rys. 4.11): 195 dB o wartości 100 mil do 99% i ponad 250 mil do 50%; evanescence ± 7 dB na 100 mil (-3,5 dB do 50 mil), SSB 3 dB gorsze niż CW, + 4 dB odbicia ziemi, + 3 dB dla sprzężenia dwóch anten; przyrost wysokości (15 m: 4 / 3 / 2 dB przy 10 / 50 / 100 mil; 24 m: 8 / 6 / 4 dB); Yagi uzyskują około 10 razy boom w λ (dBi); 50 W stacji ze średnią anteną: około 180 dB, 100 mil, + 10 dB do 250 mil. Wschód słońca i zachód słońca do + 20 dB i + 20-50% przepływu; gorące i suche powietrze nad mokrym świeżym powietrzem na Wielkich Jezior do 1200 mil, typowo 400- 600; regiony pod wpływem dużych mas wodnych najbardziej uprzywilejowane; późna zima z ogrzewaniem zdolnym do silnej inwersji. Weryfikacja naszego troposkattera: P.452 do 50% przy 483 km i 144 MHz daje około 204 dB, zgodnie z 214 dB Bray do 99%. Norton 1959 (J. Res. NBS 63D, free on NIST): strata podstawowa, system i antena ścieżki; Atmosfera referencyjna Ns = 301; równoważny promień od Ns (250 → 4878 mil, 301 → 5280, 400 → 6997); L (99) tabele z antenami 30 stóp (100 MHz: 163 dB do 100 mil, 208 dB do 500 mil; 1000 MHz: 138 i 188 dB z przypowieściami 28 stóp); zmienność długoterminowa V (p, θ) w zależności od odległości kątowej (rys. 13). Rice, Longley and Norton, NBS TN 15 (1959), free on NIST. Barsis i Kirby, NBS Report 6751 (1961): 223 km w Kolorado z Pikes Peak (4292 m) jako krawędź, 100 i 751 MHz; konsekwentna utrata z zaokrągloną krawędzią (około 11,5 km promienia); długotrwała evanescencja 11,7- 14,3 dB w porównaniu z 39- 42 dB rozproszenia o tej samej długości; żaden cykl dnia nie jest oznaczony; czteropromienny model zapewnił 4- 5 płatów, ale tylko jeden jest mierzony (maksymalnie do około 21 m od ziemi): ścieżki z przeszkodą górską są znacznie bardziej stabilne niż rozproszenie, przydatne dla połączeń "przez Vesuvius lub Faito". ARRL VHF Digital Handbook: pakiet, APRS, D- STAR, WSJT (Metoda scatter i EME), bez udziału w tropo. Nowe sprawdzone lub przeszukane źródła danych: ECMWF otwarte dane (IFS 0.25 °, poziomy 1000, 925, 850, 700 hPa..., bez 975, 950, 900 i 800; G RIB2 z kompresją CCSDS, że nasz czytelnik nie dekoduje; już dostępne przez Open- Meteo); DWD ICON- UE otwarte dane (G RIB2 skompresowane bz2, plik dla zmiennej i poziomu: wiele wniosków); Kanada GDPS na MSC Datamart i GeoMet (około 15 km, do 33 poziomów; GeoMet pozwala na uprawy obszaru; jest to podstawa Hepburn map); GFS NOAA archiwum na AWS od lutego 2021 (globalnych plików, przydatne dla walidacji); APERS PropView celu tylko zatwierdzenia.

Jedenasta runda (21 listopada): Longley- Rice, GEKO 69. Fasola i Thayer, "Models of the Atmospheric Radio Refractive Index", Proc. IRE May 1959 (pp. 740 et seq.): model 4 / 3 ma prawo nachylenia w pierwszym km, ale spada zbyt wysoko; 30% krzywizny promienia występuje w pierwszym 100 m i 60% w pierwszym km; średnia zależność między N i spadek w pierwszym km: − ΔN = 7,32 · e ^ (0,005577 · N) (N = 313 → ΔN około -42). Bean and Thayer, "C.R.P.L. Expoential Reference Atmosphere", J. Res. NBS 63D n. 3 (1959): N (h) = Ns · e ^ (-c · h) z c ustawionym przez N przez ten sam raport; Average Ns USA 313; belki tracking table (całkowita krzywa 15.2 mrad z wykładniczym modelem versus 30.3 mrad z Ziemią 4 / 3). Refractionexp MATLAB: c = 0,143859 km − 1 dla Ns = 313 (skala około 6,95 km), ważne powyżej około 1 km i 10 mrad. Zastosowanie dla nas: "normalny" profil porównania może być, że CRPL z lokalnymi Ns zamiast M = 315 + 0,118 · z stałe, a anomalia spadku w pierwszym km powinny być mierzone w porównaniu do -7,32 · e ^ (0,005577 · Ns) zamiast -40 stałe. NTIA Report 82-100 (Hufford, Longley, Kissick 1982), przewodnik ITM w trybie przestrzennym: climi i Ns sugerowane (równikowy 360, subtropikalny kontynentalny 320, subtropikalny morski 370, pustynny 280, umiarkowany kontynentalny 301, umiarkowany morski na lądzie 320, umiarkowany morski na morzu 350); Ns = N0 · e ^ (-hs / 9,46 km); używasz miesięcznego minimum Ns ponieważ mniej wpływa na nadrefrakcji i podrefrakcji; Ns = 301 odpowiada ae = 8497 km; poniżej 100 km Ns liczy niewiele. Ryż, Longley i Norton, NBS TN 15 (1959), część I: prognozowanie formuły dystrybucji w czasie drogą lądową i troposferyczną, podstawa TN 101. SoftWright, Wikipedia, Altair WinProp i MATLAB rfprop. LongleyRice: ta sama informacja (wersja 1.2.2, 20 MHz- 20 GHz lub 40 GHz, 1-2000 km, Ns 301, siedem klimatów, zmienność czasu, miejsca i sytuacji); nikt nie kształtuje przewodów, tylko statystyczny efekt klimatów morskich ("wyższe pola dla 10% czasu lub nie"). GEKO Magazine n. 69 ( IU3QEZ): radiowy horyzont 4 / 3 o 130 km od 1000 m, 260 km dodając dwie stacje; troposkatter o wspólnej objętości między 1-3 km i 10 km; rozproszenie z samolotu na 10 km z + 10- 20 dB przez dziesiątki sekund lub kilka minut; kursy teoretyczne z 2 × 10 elementów na bok: SSB QRP około 440 km, SSB 50 W około 510 km, CW QRP około 600 km, SSB 500 W około 640 km (500 W w SSB sprawiają, że 40 km więcej niż 5 W w CW); w 90- 99% czasu marży 10- 20 dB nie powinno być usunięte pomimo lotu; 5- 10 dB na horyzoncie na długich odcinkach; kontyngent liczy się na wolnym horyzoncie; Alpe Adria 2026: 99% z SO Qposkatter, prawie jeden na przestrzeni powietrznej.

## 12. Okrągły stół: źródła walidacji modelu lub jego poprawienia (21 listopada)

Streszczenie wszystkich obrotów źródeł, zamówione do użytku. "Stan" mówi, czy źródło jest już w programie (1.3.1), czy jest testowane, czy ma być wykonane.

### Dane do zatwierdzenia (co naprawdę się stało)

| Źródło | Co daje | Stosowanie | Państwo |
| Raporty MMMonVHF od autora | 593 QSO przy 144 MHz, 29 / 12 / 2021-2 / 1 / 2022, czas i lokalizatory | Walidacja zdarzenia w porównaniu z porównaniem (+ 14 dni) | w użyciu: AUC 0,79 (1.2) → 0,90 (1.3.1) |
| Świat powyżej 50 MHz | udokumentowane przypadki (14- 15 / 8 / 2022 Manitoba-Kansas, 144- 10 GHz) | przypadek historyczny | w użyciu jako przypadek historyczny; w celu dodania do walidacji QSOS |
| wspr.live (WSPR) | kompletne archiwum, 2 m i 70 cm, SQL | wiele wydarzeń i wiele "nie" (godziny bez miejsca) | próbowany, powinien być filtrowany (spot false na 16- 20 tys. km) |
| APRS PropView / APRS- IS | filc stacji, odległości, godziny | Tylko walidacja 144 MHz | do zrobienia |
| Reverse Beacon (archiwum) | Plama dobowa | VHF | na razie wyrzucony |
| I GRA2 radiosondaggi (Brindisi 16320, Practice of Mare 16245, Trapani 16429, Udine, Mediolan) | profile mierzone od 00 do 12 UTC | Kontrola profilu i inwersji GFS | znalezione pliki (Brindisi 46 MB), do wykorzystania |
| Barsis i Kirby 1961, Barsis 1963 | evanescencja mierzona na ścieżkach z przeszkodą górską | zatwierdzenie kategorii "monte" (Vesuvio, Faito) | do zrobienia |
| Norton 1959, NBS TN 15, TN 101 | L (99) i straty mediów stołowych | Kontrola rozproszenia | częściowo (zob. 300 mil) |

### Korekty zaproponowane przez źródła

| Korekta | Źródła | Wpływ zmierzony przy walidacji | Państwo |
| Niestabilność Termin | Bean i Dutton Cap. 6 | AUC 0,79 → 0,90 wraz z subsidencją | w pkt 1.3.1 (tytuł 16 dB bez przewodu) |
| Odchylenie od ruchu pionowego | Moler i Holden 1960 (czytaj oryginalnie 22 / 11: + 2,5 dB średnia między spadającym powietrzem a wznoszeniem się powietrza, 1,4 i 9,4 GHz na morzu) | jak powyżej | w pkt 1.3.1 (+ 1,5 / -2 dB) |
| Miękki rurociąg (λ do 3 · λmax) | Kerr, Hepburn, VHF / UHF DX Book, Shi 2019 | sam + 1% AUC; przynosi kolory kanału na 46% tras zdarzenia i 0% porównania | : |
| Trwałość 24 h | VHF / UHF DX Book, Biancomano i Leyshon | AUC bez zmian, więcej branych i więcej fałszywych alarmów | w pkt 1.3.1 |
| Profil normalny CRPL z lokalnymi Ns (-ΔN = 7,32 · e ^ (0,005577 · Ns)) zamiast 40 N / km stały | Fasola i Thayer 1959, Monografia NBS 4 | Badanie walidacji | w pkt 1.3.2 jako alternatywa, wyłączona przez defekt |
| Próg superrefrakcji 79 N / km (ITU) zamiast 100 (Fasola i Dutton, warstwa 1000- 850) | P.453, Lopez 2008, DX Book | Badanie walidacji | w pkt 1.3.2: agresywny szacunek obok ostrożnego (mapa wykorzystuje ostrożne) |
| Grubość klimatologiczna (calcle B) mniejsza niż deficyt | Rogers 1997 | spróbować: użyj A dla zysku | do zrobienia |
| Równanie paraboliczne w profilu GFS | PETOOL, propagacja fal (Lytaev), Levy (książka nie czytana, tylko podgląd), Lindquist | Pierwsza wersja z polem na horyzoncie zdominowanym przez hałas numeryczny (AUC 0,12- 0,28): poprawna (domena 8 km, belka 6 °); po korekcji AUC 0,54- 0,66 | w użyciu jako drugi obliczenia, nie decyduje prognoza |
| Ewanescencja: ± 7 dB na 100 mil, margines 10- 20 dB do 90- 99% | Bray 1961, Antenna Book, GEKO 69 | progi sposobów w warunkach krańcowych | w użyciu: prawdopodobieństwo okresu i progu FT8 − 18,6 dB w troposkatterze |
| Wspólna objętość i równoważna odległość Bray 'a | IU3QEZ, Bray 1961 | Informacje | : |
| Troposcatter P.617 i RSGB jako niepewność | TropoLink, DL2FZN | Informacje | w 1.3.0 |

### Co pozostaje poza modelami

Przeprowadzona na morzu na niskiej wysokości (100- 200 m), której poziomy GFS nie widzą (badanie aktualnych źródeł danych); refleksja i dyfrakcja na pobliskich górach (Vesuvio, Faito): propozycja obliczenia "za pośrednictwem monte". ITM / Longley- Rice pozostaje statystycznym odniesieniem "normalny dzień" (Ns 301, climi), bez przewodów. Wyrzucone decyzją autora: AirScout, P.528), E sporadic (P.2001) i 50 MHz (serwoir EIRP, którego niewielu ma i myli z E sporadic).

### Wniosek dotyczący nakazu pracy

1. Rozszerzona walidacja: dodać filtrowane WSPR (nawet godziny bez miejsca) i QST 2022; powtórzyć dla 432 MHz.
2. Wypróbuj normalny profil CRPL i próg ITU 79 N / km, jeden po drugim, zachowując tylko to, co podnosi wartość AUC.
3. Porównanie profili GFS z falą radiową I GRA2 Brindisi i Praktyka Morza w dniach QSO.
4. Przelicz równanie paraboliczne po korekcji numerycznej.

### Decyzje z dnia 21 listopada (wersja 1.3.2)

- Przyjęte: ostrożne oszacowanie (Fasola i Dutton, 100 N / km) i agresywne (ITU, 79 N / km); kanały poszukiwane tylko poniżej 2500 m (pierwsze 1500 m, podniesione po walidacji); premia z wysokim rozszerzonym ciśnieniem i słabym wiatrem; waga morza z μ1 z P.452; udział stacji w odniesieniu do przewodu również na kanale częściowym; warstwa graniczna powyżej grzbietów ( IT9GSF).
- Przyjęte jako alternatywa, wyłączone przez defekt: normalny profil CRPL z lokalnym N.
- Prawidłowe równanie paraboliczne liczbowo: AUC od 0,12 do 0,54 (antena 30 m) oraz od 0,28 do 0,63 (300 m).
- Land: Copernicus DEM GLO- 30 zamiast Terrain Tiles "skadi", które w pobliżu wybrzeża mają ulgi duchów 80- 100 m w morzu ( IC8TEM, zweryfikowany punkt po punkcie do Capri); skadi pozostaje jako rezerwa.
- Znaki pogody na mapie z ich efektem: ↓ korzystna subsidence (Moler i Holden 1960, średnio około 2,5 dB między powietrzem schodzącym w dół a powietrzem wznoszącym się w górę; + 1,5 dB), ↑ powietrze podparcie niekorzystne (głębokie mocowanie; -2 dB), S strike korzystna wskazówka (ARRL Handbook § 19.4, silne inwersji na Morzu Śródziemnym; brak korekty). Podwójny symbol, kolor zielony / czerwony / pomarańczowy i komiks na mapie.
- Przyjęte ITM 1.2.2 (NTIA / itm, domena publiczna), przetłumaczone na C #: zgodnie z obliczeniem wartości odniesienia w normalnej atmosferze obok P.452 i ostatni odcinek po wyjściu z przewodu, który kończy się przed korespondentem. Wydzielony kod qradiopredict, ponieważ pod GPL v2.

Bez użytecznej treści dla indeksu: * Communications Quarterly *, jesień 1991 (brak artykułu tropo; artykuł Atkinsa na temat Scatter jest w zimie 1991 wydania).

- Przyjęta jako zasada podręczników (21 listopada): "rozmnażanie nie może być uogólnione, ale studiowane w odniesieniu do morfologii lokalnej" ( DF5AI, zgłoszone przez IK3XTV w "Rozmnażaniu na 2 metry"); cytowana w instrukcji prognozowania (§ 1) i w radach.
- Przyjęto (21 listopada, po 1.3.3) interpretację popołudniowych otworów transalpejskich IK3XTV: nie "komórek konwekcyjnych, takich jak soczewki", ale troposcatter wzmocniony przez turbulencje do góry warstwy ponownie wprowadzone, gdy osiągnie udział wspólnej objętości (instrukcja prognoz, § 8 ter). Od 1.3.4 wprowadza się obliczenia punktowe jako tymczasowe wzmocnienie troposkattera, maksymalnie 3 dB. Odrzucone tymi samymi artykułami: "autoamplifikacja" sygnałów poza Alpami (podłoże pasywne nie wzmacnia: opisane linki pozostają wyjaśnione dyfrakcją i troposcatterem; deklarowane moce nie są wykorzystywane jako dane), śnieg, który "poprawia dyfrakcję" (bardziej prawdopodobne jest, że stabilna warstwa nad śniegiem, z mniejszą kompensacją).

- Kerr (1951) recenzował książkę (22 listopada, PDF dostarczony przez autora): § 1.5, eq. 17 i 18 oraz tabela 1.1. Dane z Eq. 18 w skanowaniu są nieczytelne, ale tabela 1.1 potwierdza: 20 ft → 1 cm, 80 ft → 10 cm, 400 ft → 100 cm, 2000 ft → 1000 cm, przeciw 1,25, 10, 112 i 1252 cm λmax = 0,014 · d ^ 1,5 (λ w cm, d stojąc; okrągły stół, "wartości orientacyjne". Hipotezy Kerra: wewnątrz przewodu modyfikowane krople indeksowe 4 · 10 − 8 na stopę, typowa wartość niskich kanałów; o słabszych gradientach wymagają większej grubości. Kerr ostrzega również, że λmax nie jest cięciem netto: długości fal czasami dłuższe mogą być nadal silnie pod wpływem kanału.

- DL6WU zweryfikowany na oryginale (22 listopada, PDF VHF Communications 3 / 1982 dostarczone przez autora): G. Hoch, "Extremely Long Yagi Antennas", pp. 130 i później. Rys. 1: przyrost na dipolu mierzony na poziomie 432 i 1296 MHz w profesjonalnym poligonie postów niemieckich, z próbkową anteną EIA od 7,7 dBd, oszacowaną dokładność 0,5 dB; 2,35 dB dla podwójnej długości boomu, bez nasycenia do około 20 λ. Używany program 8.6 + 8.0.log10 (L / λ) dBd, tj. około 0,8 dB w mniej niż 1-2 λ; teraz używa 9,4 + 7,8 · log10 (L / λ) dBd (gradient jest podanym, wartość 1 λ jest odczytywana na wykresie, ± 0,3 dB). Oddzielony wzór 10 · log10 (7,8 · L / λ) w obiegu w sieci: daje 3 dB do podwojenia, w porównaniu z 2,35 zmierzone.
- Booker i Gordon (1950): referencje potwierdzone przez "This Week 's Citation Classic" (Obecna zawartość, 8 lutego 1982, komentarz H. G. Booker): Proc. IRE 38, pp. 401-412. Komentarz potwierdza dwa punkty używane w podręcznikach: pole otrzymane poza horyzontem z dystrybucją Rayleigh i wykładniczą funkcję autokorelacji nieprawidłowości (nie-Gaussian). Oryginalny artykuł nie został przeczytany.

## 13. Z artykułów IK3XTV

Dwa artykuły Flavio Egano IK3XTV (Radio Rivista: "Rozmnażanie na 2 metry" i "Rozmnażanie VHF - Komórki konwekcyjne i fale grawitacyjne") dodają dwie rzeczy, których modele przewodów nie obejmują: otwory po południu z powodu konwekcji oraz praktyczne zasady, gdzie i kiedy przewody są tworzone we Włoszech. Są to obserwacje ze stacji, a nie środki: muszą być traktowane jako hipotezy, aby sprawdzić z plamami.

| Obserwacja autora | Jak staje się składnikiem indeksu |
| Kanały są formowane głównie od 100 do 1000 m, rzadko wyższe; służyć wysokie ciśnienie na szerokiej powierzchni, dobra bezczelność i brak wiatru | poszukiwanie kanału tylko poniżej 1500 m; bonus z wysokim ciśnieniem i słabym wiatrem w niskich warstwach |
| W ciepłych miesiącach prowadzone powtarzające się na Adriatyku między Veneto i Puglia, 700- 800 km, czasami do Grecji; szlaki morskie są ulubione | większa masa do frakcji szlaków morskich (np. μ1 ITU- R P.452) |
| Odpowiedni musi być w lub w pobliżu przewodu; z wysokich anten i kilka metrów prowadzić sygnał nie wchodzi | porównać udział podstawy przewodu z udziałem dwóch stacji (będzie służyć wysokości QTH) |
| W tym samym czasie, ten sam kanał: słuchać z Gargano stacji w 144 i jeden w 50 MHz | aby sprawdzić: według tabeli Hepburn 50 MHz prosi o ponad 300 m ołowiu, więc zdarzenie oznaczałoby rurociąg bardzo często |
| Połączenia Veneto - południowe Niemcy poza Alpami, bez wizualizacji, prawie zawsze po południu i z wysokim ciśnieniem; niestabilne sygnały, które znikają i pojawiają się ponownie po kilku minutach | Wskaźnik "konwekcyjny" oddzielony od przewodu, dla tras górskich |
| Komórki konwekcyjne rodzą się na ziemi i wspinają się na około 1 km co 3 godziny: przy 15 osiągają około 3 km, wysokość, która służy do obejścia Alp | użyj wysokości warstwy granicznej (granica\ _ warstwa\ _ wysokość Open- Meteo GFS) wzdłuż ścieżki: wynik, gdy przekracza wysokość grzbietu |
| Alpy generują fale grawitacyjne, które wraz z ruchem konwekcyjnym tworzą nieprawidłowości wskaźnika refrakcji | hipotezy: silny wiatr prostopadły do łańcucha do 700 hPa jako sygnał fal złota; spróbować ostatni |
| Bardzo wysoka inwersja (do 8 km w wyjątkowych przypadkach) daje około 800 km, ale w praktyce rzadko więcej niż 300 km | udział inwersji rozszerza zakres: użyteczne dane do oszacowania odległości oczekiwania |
| W lecie promień Ziemi odpowiada około 16000 km (2,5 razy większy od rzeczywistego), przy około 60 dB przy utracie poniżej 300 km | zgodny z k50 = 157 / (157 − ΔN): k = 2,5 odpowiada ΔN około 94 N / km, czyli nadrefrakcji |
| Ponad 3000 km tylko w zakresie 20- 30 ° na północ i południe od równika, nad morzami, związane z ogniwami Hadley; szczyt w sierpniu w Afryce - Europa | poza Europą: przydatne nie podawać wysokich wskaźników na Atlantyku bez informacji zwrotnej, a dla trasy Canary - Wielka Brytania |

Inne użyteczne dane z tych samych artykułów:

- Zakres optyczny między dwoma h1 a h2 metrami (d w km): d = 4,1 · (Δh1 + Âh2), do około 20% dla normalnej refrakcji.
- Evanescence: powolne i płytkie z inwersji; silne i szybkie z rozproszone i konwekcyjne komórki. Program może go używać do klasyfikacji reklam, jeśli pewnego dnia będzie miał raporty SNR w czasie.
- Dla sporadycznego E w 144 MHz autor informuje o pracy DF5AI i Davis i Johnson (Natura 435, 2005) w sprawie więzi między czasowymi i Es: z tropo, ale oddzielny wskaźnik jutro może użyć danych błyskawicznych.
- Odbicie ziemi w podwójnym skoku Es: morze K = 81 i 5 S / m, wieś i lasy K = 13 i 0,006 S / m, obszary miejskie K = 3 i 0,001 S / m.

## 14. hipotezy indeksowe LIVETROPOVHF

Rozpoczynając wniosek, należy omówić: indeks 0- 10 na tej samej skali co Hepburn, obliczony dla każdego punktu siatki i co 3 godziny od GFS (następnie ECMWF i ICON dla porównania), oraz drugi numer na trasę, który mówi, ile części trasy przechodzi do użytecznego kanału do wybranego pasma. Wagi poniżej są moimi założeniami, do skalibrowania z plamami.

Przegląd po Bean i Dutton i Kerr: progi profilowe pochodzą z tabeli 6.5 i skali z pomiarów, więc warstwa refraktywna jest warta 10- 25 dB nad normalnym profilem. Wskaźnik oblicza się najpierw w dB, a następnie przyjmuje wartość 0- 10; wartości w tym nożycu pozostają do skalibrowania z plamami.

Obliczanie według punktu, krok po kroku

1. T rozładowanie, wilgotność względna, wysokość geopotencjalna i prędkość pionowa przy 1000, 975, 950, 925, 900, 850, 800, 700, 600 i 500 hPa, plus wilgotność T i 2 m, ciśnienie gruntu, wysokość warstwy i wiatr przy 10 m.
2. Wyrzucić poziomy poniżej podłoża; obliczyć N i M na każdym poziomie (ITU- R P.453).
3. Obliczanie -dN / dh w każdej z czterech warstw Fasoli i Duttona (1000- 850, 850- 700, 700- 600, 600- 500 hPa) i wykresie jako sub- refraktywny, normalny lub superrefraktywny z progami tabeli; w pierwszej warstwie również używać poziomów pośrednich, aby znaleźć najsilniejszy gradient i jego udział.
4. Gdzie dM / dh < 0 I znaleźć podstawę i górę przewodu, grubość d i deficyt ΔM.
5. Porównanie z minimalną grubością pasma ze wzoru Kerr (około 180 m dla 144 MHz, 89 m dla 432 MHz, jak w tabeli Hepburn); poniżej progu przewód nadal się liczy, ale mniej.
6. Stymuluję przyrost w dB z poniższą tabelą (bierzesz najlepszą warstwę, nie sumę warstw, plus termin anomalii) i konwertuję ją: indeks = dB / 2.5, pomiędzy 0 a 10, więc 25 dB jest warte 10. Wartość ujemna (sub-refraction) jest pokazana jako 0, ale pozostaje w danych ścieżki.

| Najlepsza sytuacja warstw | Kryteria | Szacunkowe dochody |
| Normalny (nielaminowany) | 6.5 (np. 20- 60 N / km między 1000 a 850 hPa) | 0 |
| Nieco powyżej normy | między górną granicą normalnego pasma a progiem superrefrakcji | 0-10 dB, proporcjonalnie |
| Nadmierne załamanie | powyżej progu warstwy (100, 80, 70, 50 N / km wspinaczki), ale poniżej 157 N / km | od 10 do 15 dB, proporcjonalnie do 157 N / km |
| Zbyt cienki rurociąg dla zespołu | dM / dh < 0, grubość poniżej Próg Kerr | 15 |
| Przydatne złącze dla pasma | dM / dh < 0, grubość powyżej progu Kerr | 20 dB, + 1 dB co 5 M jednostek deficytu, maksymalnie 25 |
| Refrakcja cząstkowa | − dN / dh < 0 w dolnej warstwie | − 10 dB |
| Anomalne tło (dodajesz) | Model Ns minus ITU mapa klimatologiczna Ns dla tego miesiąca | 0,2 dB na jednostkę N, między -5 a + 5 dB |

Obliczanie według trasy (między dwoma lokalizatorami): pobieranie próbek z maksymalnego okręgu co 10 km, jak już program indeksu na ścieżce, i obliczanie ułamka punktów z użytecznym kanałem i średni udział podstawy. Badanie CSIRO uznało za przydatne ułamek trasy w rurociągu (na trasach AIS przy 162 MHz było o 67% wyższe niż w przypadkach kontroli).

Dodawanie obliczeń na trasę (z LA0BY i W3SZ): (1) stacje morskie w porównaniu z bazą i wierzchołkiem przewodu, ponieważ wysoki kanał służy tylko tym, którzy wchodzą; (2) troposkatter "floor" z metodą Yeh, z narożnikami startowymi pobranymi z profilu gruntu; zysk z tropu w dB jest usuwany z tej straty i wynik jest porównywany ze stratą, którą stacja potrafi pokonać (tabela KK6MC). W ten sposób indeks staje się również szacunkiem odległości osiągalnej.

Korekty, aby spróbować jeden na raz (z Bean i Dutton i LA0BY, IK3XTV, G4MCU już w podręczniku), aby zachować tylko, jeśli poprawiają porównanie z plamami:

- Mieszanie (ważne według autora, codzienne obserwacje): od dnia do dnia na ziemi, od połowy rano do południa z czystym niebem, lub z wiatrem do ziemi nad kilkoma m / s, kanały do ziemi znikają; ich wkład musi być zmniejszony. Nie na morzu.
- Czas dnia (ważne ważne według autora, przez codzienną obserwację): kanały irraggiamento pojawiają się o zachodzie słońca i jesienią po wschodzie słońca; jest to 3-godzinna prognoza kontroli.
- Ruch pionowy: powietrze wzrasta do 850- 700 hPa remiksuje warstwy (korekcja ujemna), powietrze zstępujące pod antycykliną wzmacnia je (dodatnie).
- Tendencja ciśnienia: pod ciśnieniem na ścieżce w ciągu ostatnich godzin, wyższy i stabilny sygnał; spadek, niższy sygnał (bastano 2 hPa).
- Synonym: wysokie ciśnienie powyżej 1025 hPa, temperatura 850 hPa znacznie powyżej tego na ziemi, gorące i suche powietrze płynące na zimniejszym morzu, droga na morzu.
- Ścieżki górskie: drugi indeks "konwekcyjny" (sekcja IK3XTV), z popołudniową warstwą graniczną wyższą niż grzbiety wzdłuż ścieżki i wysokim ciśnieniem.

Od VHF / UHF DX Książka, inne poprawki do próby: kolejne dni wysokiego ciśnienia w punkcie (więcej dni, więcej punktów), pozycja w porównaniu do centrum antycykliny (bonus na krawędziach), gradient ciśnienia na ziemi jako miarę wiatru, silny deszcz w 24 godziny przed (penality), niskie chmury na krawędzi warstwy (penality tylko dla wysokich pasm). Kierunek trasy: porównać azymut trasy z azymutem izobare.

Weryfikacja

- Archiwum LIVETROPOVHF: każda reklama o długości od 500 km do 144 MHz lub 432 MHz jest "wydarzeniem". Porównuje on wskaźnik na trasie z częstotliwością zdarzeń, o tej samej porze dnia i miesiąca (to samo podejście w badaniu CSIRO).
- Porównanie z Hepburn i F5LEN w tych samych dniach: gdzie trzy indeksy odłączyć, można spojrzeć, kto miał rację zgodnie z plamami.
- Radiosunditions of 00 and 12 UTC: to check if the model see the inversions that the sonda measures.
- Przypadki historyczne LA0BY (6- 11 / 11 / 2003, 10 / 12 / 2004, 15 / 10 / 2011, 11 / 2 / 2015, 22 / 11 / 2018): wskaźnik ponownie obliczony w tych dniach musi zobaczyć kanały, których Hepburn nie wykazał.
- Limity, które mają być zadeklarowane w podręczniku: prowadzone poniżej 100- 200 m prawie niewidoczne do poziomu ciśnienia GFS; niezawodna prognoza na 2-3 dni, a następnie tylko trend.

## 15. Źródła z dnia 21 listopada (część druga)

- ITU- R P.1812 (MaTLAB / Octave implementation of I. Stevanovic, OFCOM; GitHub eeeveetza / p1812, Zenodo 10.5281 / zenodo.6141558). VHF / UHF specyficzna metoda trasy, pochodząca z P.452: dyfrakcji "delta-Bullington", troposkattera, kanałów i refleksji na warstwach, obciążenia na ziemi. Przydatne jako trzecie porównanie obok obliczeń punktu punktów P.452; dane ΔN i N0 są pełnymi produktami ITU, więc użytkownik pobiera je (jak w P.453).
- od Costa, Rabanal Ramirez, Dias, IET MAP 2018 (doi 10.1049 / iet- map.2017.0992). 563 MHz, 27 punktów między 5 a 30 km na nieregularnym terenie z lasami: P.526 z 12 m roślinnością -4,3 dB i Δ8,8 dB; P.1812 z 10 m obciążeniem + 2,6 dB i Δ8,9 dB; dielektrycznym klinem -4,8 dB i Δ9,2 dB; P.1546 + 5,0 dB i Δ11,7 dB. Dla programu: rezydualny błąd dobrej metody dyfrakcyjnej wynosi około 9 dB, a wysokość obciążeń na grzbietach (10- 12 m) się liczy.
- PETOOL (Ozgun e altri, Computer Fizyki Communications 182, 2011). Paraboliczne równanie split- krok do jednej i dwóch tras z szerokim propagatorem kąta (kąty do 40- 45 °, w stosunku do 10- 15 ° wąskiego); dół z strat przez przekształcone Fourier mieszane; grunt do kroków. Potwierdza wybór 1.3.2 (szeroki kąt, współczynniki gleby) i wskazuje dwukierunkowe obliczenia stromych reliefów.
- MathWorks. `tropopl`: utrata gazów i utrata "lente" w atmosferze warstwowej (P.835, P.676, P.453), bez przewodów: o gradientach poniżej -157 N / km, model geometryczny nie jest ważny. Przykład zastosowania propagacji radarów do refrakcji - ΔN = 7,32 · e ^ (0,005577 · Ns), ten sam raport CRPL już w programie. `propagationModel` Longley- Rice: wstępnie zdefiniowane poziome wartości polaryzacji, przewodność 0,005 S / m, tolerancyjność 15, reaktywność 301, umiarkowany klimat kontynentalny. Żaden model MathWorks nie zajmuje się kanałami.
- ITUR, cyfrowe produkty grupy 3. Dane i programy P.453, P.452, P.1812, P.2001, P.528, P.1546; "zintegrowane" dane są częścią zaleceń; brak deklarowanej licencji redystrybucji: nadal pozwalasz im pobierać je do użytkownika.
- ESA Navipedia, opóźnienie troposferyczne. N = Nidrostatica + Numida; suche opóźnienie zenitu wynosi około 2,3 m, a wilgotne dziesiątki cm, co szybko się zmienia. Dla nas: mokre opóźnienie szacowane przez stacje GNSS jest pośrednią miarą zintegrowanej wilgotności, możliwe przyszłe źródło odlewania teraz; Dziś nie jest przydatna.
- Bray, K2LMG, "A Method for Determination V.H.F. Station Capabilities", QST November 1961, pp. 36- 40 (czytaj w oryginale; ponownie sprawdzone w dniu 22 listopada na PDF liczby QST: wszystkie wartości poniżej odpowiadają). Autor, D. W. Bray (General Electric, Ithaca), twierdzi, że zweryfikował prognozy z miesiącami regularnych połączeń z K2GOI i W4LTU. Ewanescencja w czasie 13,5 dB powyżej około 100 mil (7 dB poniżej i 7 powyżej średniej godzinnej), prawie liniowa poniżej (3,5 dB do 50 mil): dla stałego połączenia usunąć 7 dB. Korekta horyzontu: każda wysokość horyzontu jest warta 69 mil (111 km) dodatkowej odległości (mniejsza jeśli ujemna). Zysk z yagi około 10 × boom / λ (stosunek mocy), więcej o 4 dB odbicia gleby; SSB -3 dB i AM -7 dB w porównaniu do CW; 99% godzin: około 180 dB do 60 mil, 190 do 90, 200 do 240 (2 m). Od 1.3.2 prawdopodobieństwo dla okresu stosuje się Δ= 3,4 dB (± 7 dB jako ± 2δ) zamiast 5 dB szacowanego dla drugiej ręki.
- Obliczanie dwukierunkowe (spróbuj z prototypem, 21 listopada). Duże równanie paraboliczne kątowe z prawdziwym terenem S RTM1 z Capri, 144 MHz, antena 10 m od podłoża, atmosfera standardowa; pomiar: średnia moc od 20 do 400 m do 80 km na trasie w kierunku odpowiadającym. Od centrum Capri i Marina Grande zachód jest zamknięty (przeszkoda do + 11- 21 °, Monte Solaro): Wskazując Faito (prawie za EA5 i EA6, 150- 160 ° poza osią) sygnał wysłany do Hiszpanii wzrasta o 10- 29 dB; wskazując Vesuvius (110- 125 ° poza osią, a następnie poza planem obliczeniowym, jedynie orientacyjnie) o 17- 25 dB. Z Anacapri zachód wolny i bezpośredni zwycięstwo 8- 22 dB. Bezwzględne straty na 1000- 1300 km prototypu, jednak nie są wiarygodne. EA3 EA5 EA6
- Barsis i Kirby, NBS Report 6751 (1961), Pikes Peak, 223 km, 100 i 751 MHz. Góra jest wspólnym terminalem dwóch tras w optyce: dystrybucja godzinowych median jest połączeniem tych dwóch tras. Długoterminowa wycieczka (1- 99%) o długości około 14 dB, w stosunku do 26- 40 dB ścieżki troposkattera o równej długości lub kącie; brak znaczącej zmiany dnia. Sygnał jest silniejszy od troposkattera, ale słabszy od idealnej krawędzi: wierzchołek powinien być traktowany jako zaokrąglony (szorstkie około 11.5 km). Wysokie sygnały są zawsze obserwowane za grzbietami.
- Barsis, NBS Report 7652 (1963), three routs over a mountain (Colorado, Alaska, Europe), 750- 900 MHz. Średnia strata 10- 20 dB wyższa niż obliczenie Fresnel- Kirchhoff bez gruntu w pobliżu terminali; evanescence jest szacowana przez owinięcie dwóch linii optyką; odbicia gleby w pobliżu terminali pogarszają długotrwały blaknięcie. Od 1.3.2 wejście do kanału dodaje zaokrąglenia P.526.
- Barsis i Johnson, NBS TN 88 (1961). Długotrwałe rozpuszczanie fali kosmicznej, nawet na ścieżce poza górą (§ 5).
- Norton, "System loss in radio wave propagation", J. Res. NBS 63D (1959). Krzywe, z których Bray otrzymuje nomoogramy; definicje utraty systemu, transmisji i podstawowego, przyrostu anteny procesu.
- Rice, Longley, Norton and Barsis, NBS TN 101 (1965- 67), Volumes 1 i 2. pojedyncza krawędź i przeszkody zaokrąglone z odbiciami i bez odbicia (§ 7, z przykładem Pikes Peak), zmienność długoterminowa V (0,5) i Y (q) dla klimatów (temperowane konkontynentalne i inne). Dla nas: metoda zaokrąglonych funkcji przeszkód i zmienności jest odniesieniem do kalibracji niepewności.
- ITU- R P.526- 16 (11 / 2025). Wzór (31) J (ν), identyczny z programem; Zaokrąglona przeszkoda (32) - (36), obecnie stosowana; metody wielodzielne (Bullington i pełna metoda § 4.5).
- ITU- R P.1812- 8 (2025) i -7. Kompletny tekst odzyskany: specyficzna metoda 30 MHz do 6 GHz (delta-Bullington, troposcatter, kanały i odbicie na warstwach, obciążenie). The -7 ostrzega, że jego troposcatter (z P.617- 5) jest błędne poniżej 66 km. Pozostaje to możliwe trzecie porównanie.
- Paulus, "Practical application of the IREPS parisation model", NOSC TR 966 (1984), and Radio Science 20 (1985). Raport z 22 / 11 / 2026 (DTIC ADA146528, skanowanie z rozpoznanym tekstem, czasami nieczytelne). Różnice między wodami powietrznymi mierzone przez statki są przesuwane w kierunku stabilności: cieplejsze powietrze morskie w 31- 35% danych dotyczących statków w porównaniu do 0,7- 7% knur NOAA, dla pzykrometrów zakłóconych przez statki i SST pobieranych przez wodę chłodzącą (około + 1 ° C). Następnie model Jeske daje 40 m i więcej niż 15% roku i wysokości, które skaczą z 10 do 40 m w ciągu 24 godzin ze stabilnym czasem, niepotwierdzonym przez środki radiowe. Proponowana korekta (badane progi 0, -0,5 i -1 ° C; -1 ° C jest lepiej uzgodnione ze środkami radioaktywnymi z czerwca 1982 r.): jeśli różnica powietrza w morzu wynosi -1 ° C lub jest mniejsza, stosuje się zmierzoną wartość; w przeciwnym razie oblicza się wysokość z -1 ° C i z 0 ° C, a jeżeli z -1 ° C jest niższe, to się ją trzyma. Wzrost na stabilnej stronie są cięcia, spadki pozostają. W czerwcu, Marsden 120 kwadrat: średnia wysokość od 16,8 m (klimatologia) do 7,6 m. Równania modelu nie znajdują się w sprawozdaniu (odnoszą się do Jeske i Hitney). Program korzysta z neutralnego profilu, który nie ma stabilnej przesady, i ogranicza go do 40 m: idzie w tym samym kierunku; z modeli SST (nie statek) wady pomiaru, że Paulus koryguje waży mniej.
- Levy, "Parabolic Equation Methods for Electromagnetic Wave Propagation" (IEE 2000). W Google Libri jest tylko podgląd: Nie czytam.
- VK3FS (3fs.net.au). Wprowadzenie jakości: wysokie kanały na 450- 2000 m dla dłuższych otworów na ziemi; inwersji 300- 600 m w spokojne i spokojne noce, które znikają po wschodzie słońca; silniejsze inwersji wybrzeża po południu; 800- 1300 km typowe, 1400- 1600 km często prowadzone. Zgodnie z dziennymi / nocnymi alarmami i pułapem 2500 m.

## 16. Źródła

Zasady i formuły

- [ITU- R P.453- 13 (2017), radiowy wskaźnik refrakcji: wzór](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)i dane
- [ITU- R P.452-18 (2023), przewidywanie zakłóceń między stacjami naziemnymi powyżej 100 MHz: równoważny promień, β0, troposkatter, kanały](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)
- [ITU- R P.617- 5, rozprzestrzenianie się poza horyzont dla mostów radiowych](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)
- [Shi et al., "A Complementary Study on Wave Maximumlength of Electromagnetic Propagation in Different Evangeration Ducts", IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

Badania dotyczące przewidywania kanałów z modelami pogodowymi

- [P. Lopez, "A 5- year 40- km resolution global climatology of superrefraction for grounds-based meteorology radar", ECMWF Tech. Memo 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO), "Tropospheric Ducting Prediction based on GFS Model Data at... the CSIRO Murchison Radioastronomy", Observatory arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (FMI), "Effects of model rozdzielczość na przewidywanie warunków kanalizacji atmosferycznej z Harmonie- AROME", EGUsphere 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

Amatorskie prognozy radiowe

- [W. R. Hepburn, "The Hepburn Tropo Index (HTI)"](https://www.dxinfocentre.com/propagation/hti.htm)and [](https://www.dxinfocentre.com/propagation/history.htm)method history
- [W. R. Hepburn, "Najniższa częstotliwość użytkowa vs grubość warstwy klejącej"](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, "Worldwide refractive index preventions"](https://tropo.f5len.org/WW/info.php), [new version 2026](https://tropo.f5len.org/?p=504), [note on model ARPEGE](https://tropo.f5len.org/author/f5len/)

Dane

- [Open- Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api), [uwaga na poziomach ciśnienia](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)
- [NOAA NCEI, Zintegrowany Global Radiosonde Archive (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RAOBget, radiowy scenariusz powodzi dla Uniwersytetu Wyoming](https://github.com/NCAR/RAOBget)

Teksty klasyczne (wskazane przez autora programu, do konsultacji w sprawie ostatecznych formuł)

- B. R. Bean, E. J. Dutton, * Radio Meteorology *, NBS Monograph 92, 1966, 435 stron (reprint Dover 1968): pionowe profile N i M, gradient pułapki pod -157 N / km, porównanie z falą radiową; rozdział 5 "Radiometeorologia synoptyczna". Podstawa dla 2-4 etapów obliczeń na punkt. Darmowe: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [karta NIST, doi 10.6028 / NBS.MONO.92](https://www.nist.gov/node/1770911), [Internet Archive](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf)kopia.
- L. Boithias, * Radio Wave Propagation *, McGraw- Hill 1987 (przetłumaczone przez D. Beesona z francuskiego, 330 stron, ISBN 0070064334; także North Oxford Academic): mechanizmy poza horyzontem, wspólna objętość rozpraszania i utrata anten sprzęgających. Sam papier ([karta biblioteczna](https://libcat.library.tamu.edu/Record/in00000014812/Description)).
- M. P. M. Hall, * Effects of the Troposphere on Radio Communication *, IEE Electromagnetic Waves Series n. 8, Peter Peregrinus 1979, 206 pages (ISBN 0906048257; brossura 0863410863): synoptic situations and propagation VHF / UHF. Podstawa dla składników synoptycznych. Handel w IET ([karta](https://libcat.library.tamu.edu/Record/in00000636385)).
- H. G. Booker, W. E. Gordon, "A Theory of Radio Scattering in the Troposphere", * Proceedings of the IRE *, vol. 38, n. 4, April 1950, pp. 401-412: Teoria skalowania z dezogenetyczności wskaźnika refrakcji. Idea centralna: rozproszona moc zależy od intensywności i skali turbulencyjnych fluktuacji wskaźnika refrakcji (korelacja wykładnicza); w pobliżu horyzontu dominuje dyfrakcja, dalsze rozpraszanie; Według autorów skala turbulencji i zmiany indeksu spodziewane przez meteorologię są wystarczające, aby wyjaśnić zmierzone pola. Cytowane przez 328 artykułów (Scilit), bez bezpłatnej kopii dostępu. Karta na Semantic Scholar ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); pełny tekst na IEEE Xplore zapłacił ([doi 10.1109 / JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)).
- C. M. Crain, J. R. Gerhardt (Electrical Engineering Research Laboratory, University of Texas), "Measurements of the Parameters involved in the Theory of Radio Scattering in the Troposphere", * Proceedings of the IRE *, vol. 40, n. 1, January 1952, pp. 50- 54, doi 10.1109 / JRPROC.1952.2739 dają rzeczywiste wartości, które Booker i Gordon oszacowali tylko ([Skilit](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)tab]).
- "The Constants in the Equation for Atmospheric Refractive Index at Radio Frequenties", * Proceedings of the IRE *, vol. 41, n. 8, August 1953, pp. 1035- 1037: proponuje przegląd prądów wtedy w użyciu (79 K / mb i 4800 K) w formule N; to stąd wartości 77,6 i 3,73 · 10 ^ 5 wzoru ITU (Q](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E. C. S. Megaw, "Fundamental radio scatter promotion theory", * Proceedings of the IEE Part C: Monographs *, vol. 104, n. 6, september 1957, pp. 441-455, doi 10.1049 / pi- c.1957.0052: spectrum of index flucations from turbulence theory, complete tracing of scatter beyond the common horizon and effect of geometry paid paid at the IET ([Shilit](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)card).
- * Proceedings of the IRE *, October 1955, special number on scatter propagation (by K. A. Norton and J. B. Wiesner), with Bullington, Staras and other: free in PDF on [World Radio History](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf).
- D. E. Kerr (edited by), * Propagation of Short Radio Waves *, MIT Radiation Laboratory Series vol. 13, McGraw- Hill 1951 (reports Dover 1965 and IEE 1987): theory of pareation and surface ducts on the sea. Kopia cyfrowa: [World Radio History](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); wydanie IET w [IET Digital Library](https://shop.theiet.org/propag-short-radio-waves).

Niedawne środki dyfuzji troposferycznej w VHF: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Australia), "A Study of VHF Troposkatter Communication Channels in South Australia", in * Digital Signal Processing for Communication Systems *, Springer 1997, pp. 103- 112 ([Springer](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)). Tekst za opłatą; z abstrakcji dotyczy dyfuzji, a nie przewodów. Przydatne tylko wtedy, gdy chcesz skalibrować termin troposkattera danymi kanału VHF.

Modele numeryczne i trasy morskie (wskazane przez autora programu)

- Panie Levy, * Paraboliczne metody równania dla propagacji fal elektromagnetycznych *, IEE Electromagnetic Waves Series # 45, 2000, 336 stron (ISBN 0852967640): równanie paraboliczne dla profili refraktywnych, które zmieniają się na ścieżce i dla orografii; rozdziały dotyczące propagacji tropoferycznej, promieni i dróg, propagacji na morzu. Służyłoby do obliczenia pola wzdłuż ścieżki, poza indeksem. Do zapłaty, również w formie elektronicznej z IET ([karta IET](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)).
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (Uniwersytet w Leicester): Pomiary 2 GHz na Wyspach Normandzkich i na Kanale. Wiele prac i tezy są wolne w archiwum uniwersyteckim: [długoterminowe statystyki dotyczące kanału parowania, Radio Science 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434), [zmiany sygnału na trzech trasach morskich, Radio Science 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710), [Tytuły N. Mufti 2012 z trasami 50 i 140 km również w VHF](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854), [teza I. Alam 2002 o reaktywności obliczonej na podstawie danych pogodowych, 240 MHz i 2 GHz X3 UQ, [tesi.](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus, "Practical application of an pareation model", Radio Science vol. 20 n. 4, 1985, p. 887 (not IEEE Transactions on Antennas and Propagation), doi 10.1029 / RS020i004p00887 ([scheda](https://zendy.io/title/10.1029/rs020i004p00887)): higher of the pareation chain from from surface weather data. [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf).

T. Lindquist, * Wave Propagation Models in the Troposphere for Long- Range UHF / SHF Radio Connections *, master 's these, University of Karlstad, 2 June 2020, 106 stron (dostarczone przez autora programu). Jest to przewodnik krok-by-krok, wolny, do pisania obliczeń z równania parabolicznego: split- krok rozwiązanie (nawet przy szerokich kątach), warunki graniczne dla lądu i morza, grunt krok, śledzenie wiązki na profilach liniowych w czasie, z kontrolami numerycznymi; cytuje PETOOL wolne narzędzie dla MATLAB. Dwie użyteczne dane na raz: (1) N wrażliwość na zmienne pogodowe, δN ↓ 0,26 δP + 4,3 δe − 1,4 δT (hPa i K, około 20 ° C), tj. błąd 1 hPa na ciśnienie pary waży trzy razy 1 ° C błąd na temperaturę: jest wilgotność modelu, który musi być sprawdzony najpierw; (2) Średnia wysokość kanału parowania: 5- 6 m w Morzu Północnym, 13- 14 m w Morzu Śródziemnym, ponad 20 m w Zatoce Perskiej, zawsze zbyt niska dla 144 i 432 MHz. Wniosek autora (częstotliwości od 1 do 10 GHz): poza horyzontem niezawodne połączenia prawie zawsze wymagają kanału.

Oprogramowanie propagacyjne na profilu glebowym (dla horyzontu i przeszkód, nie dla oczekiwanego tropu)

- [Radio Mobile](https://ve2dbe.com/rme.html)by Roger Coudé VE2DBE: free, Windows (także Linux i Mac with Wine), Longley- Rice / ITM model, profil terenu automatycznie ekstrahowany z danych kwot ([pobierz](https://ve2dbe.com/download/download.html)).
- [SPLAT!](https://qsl.net/kd2bd/splat.html)autorstwa Johna Magliacane KD2BD: GPL, 20 MHz- 20 GHz, Longley- Rice i ITWOM v3.0, raporty o przeszkodach i stratach w tekście; najnowsza wersja 1.4.2 2014.
- [ITM, NTIA / ITS](https://its.ntia.gov/software/itm)oficjalny kod: model Longley- Rice (20 MHz- 20 GHz) ze źródłem i dokumentacją, baza Radio Mobile i SPLAT!. Kod jest darmowy i można dodać do obliczeń na ścieżce LIVETROPOVHF (strata spowodowana glebą, która zostanie dodana do indeksu tropo). Oryginalny raport: A. G. Longley, P. L. Rice, "Prediction of Tropospheric Radio Transmission Loss Over Irregular Terrain: A Computer Method - 1968", EESSA Technical Report ERL 79- ITS 67, July 1968 (karta NTIA dotycząca oprogramowania historycznego: X1 https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITU- R, oprogramowanie i dane cyfrowe](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): darmowe mapy median rocznych ΔN i N0 na siatce 1,5 °, wykorzystywanych przez P.452 i P.617 - przydatne jako referencyjne wartości klimatyczne dla indeksu.
- Pathloss (Contract Telecommunication Engineering): program komercyjny dla mostów radiowych; nie znalazłem źródła publicznego, które potwierdza, które zalecenia ITU wdraża, więc sprawdzić.

Kod GitHub wielokrotnego użytku w systemie LIVETROPOVHF

| Projekt | Co robi? | Język | Możliwość użycia dla nas |
| [NTIA / ITM](https://github.com/NTIA/itm) | model Longley- Rice urzędnik NTIA, z paczkami dla. NET |  C++, C#  | utrata ziemi na ścieżce: integruje się bezpośrednio z programem, który jest w C # |
| [fuzzzeee / itm](https://github.com/fuzzzeee/itm) | ITM C + + kod C # tłumaczenie |  C#  | alternatywa bez rodzimych bibliotek, również przydatna dla wersji Windows 7 |
| [eeveetza / Py452](https://github.com/eeveetza/py452) | ITU- R P.452, identyczny z wersją MATLAB / Octave zatwierdzoną przez ITU- R WP 3M; wersja MATLAB opublikowana przez Szwajcarski Federalny Urząd Komunikacji | PYTHON MATLAB | odniesienie do tłumaczenia na C # formuły troposcatter i przeprowadzone, z przykładami walidacji ITU |
| [bwinkel / pycraf](https://github.com/bwinkel/pycraf) | P.452 kompletne, P.676, profile SRTM; GPL v3, ale dołączone pliki danych ITU nie są bezpłatne do użytku komercyjnego | ♪ | kontrola naszych wyników |
| [ITU- Rpy](https://github.com/inigodelportillo/ITU-Rpy) | wiele zaleceń ITU- R, w tym P.453- 13, potwierdzonych przykładami ITU | ♪ | Weryfikacja N formuły i map klimatycznych z pozycji P.453 |
| [NCAR / RAOBget](https://github.com/NCAR/RAOBget) | pobierz fale radiowe Uniwersytetu Wyoming w formacie tekstowym | ♪ | diagram do pobrania w C # profile włoskich stacji |

Uwaga: obecna wersja P.453 to [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), do pobrania nieodpłatnie; w dokumencie użyłem 13, które mają zostać ponownie dodane.

Już w podręcznikach programu i wielokrotnego użytku tutaj: LA0BY (Nordic VUSHF 2019), IK3XTV ("Rozmnażanie na 2 metry" i "Rozmnażanie VHF - Komórki konwekcyjne i fale grawitacyjne", Radio Magazine, dostarczone przez autora programu), DF5AI/DK5YA (2003), G3YGF (QST 1983), VHF / UHF DX Book cap. 2, G4MCU (2003) KP4MD
