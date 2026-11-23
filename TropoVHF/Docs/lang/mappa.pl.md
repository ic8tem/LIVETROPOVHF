LIVETROPOVHF - dokument techniczny 2
# Budowa mapy

> Automatyczne tłumaczenie przez włoski: w przypadku wątpliwości warto włoskiego przewodnika.

W miarę jak rodzi się mapa zakładki Mapa i spot: podstawa geograficzna, projekcje, średnia indeksów Hepburn i F5LEN, izobare, miejsca i stacje słuchania, pokrywane warstwami. Wersja z dnia 9 listopada 2026 r.

## Podsumowanie

- Mapa jest zaprojektowana przez program w ośmiu warstwach, z których każda posiada swoje pole [8].
- Podstawa: Natural Earth coastal and borders at two levels of [9] detal; [10] Market and azymuth projections.
- Fizyka: inwersja, reaktywność N i gradient dN / dh, próg kanału przy -157 N / km [3] [4] [7] [11].
- Indeks gardłowy: skalibrowane odczyty map Hepburn [1] i F5LEN [2], wspólna siatka i średnia dwóch źródeł.
- Izobute: Ciśnienie GFS [12] przez Open- Meteo [15], linie co 4 hPa, H i L centra.
- Spot, stacji odsłuchowych (ciemny różowy koła z zawsze widoczne różowe linie) i osobiste dekodowania w czerwonym.
- Czytanie nakładania się: wysokie ciśnienie, wysokie kanały, spadek ciśnienia, rosnący SNR, [3] [4] [5] [13] [14] trasy górskie.

## 1. Zasada: mapa warstwowa

Mapa nie jest stałym obrazem: jest zaprojektowana przez program, warstwa nad warstwą, za każdym razem gdy zmieniasz zoom, projekcję, pasmo, prognozowanie czasu lub tematu. Każda warstwa ma swoje pole w górnym pasku warstwy na mapie, więc można trzymać tylko to, czego potrzebujesz i zobaczyć wszystko razem, gdy chcesz pełny obraz. Zamówienie, od dołu, jest następujące:

|  #  | Droga | Zawartość | Casella |
|  1  | Podstawowa mapa | Morze, lądy, wybrzeża i granice | Tak. |
|  2  | Wskaźnik tropo | Media Hepburn + F5LEN lub tylko jedno źródło; regulowana przejrzystość | Tak. |
|  3  | Lokalizator grill | Pola, kwadraty i, z zoomem, do kwadratu | Tak. |
|  4  | Isoboy | Linie ciśnieniowe co 4 hPa, H i L | Tak. |
|  4a  | Temperatura 850 hPa | Izotermalne poddane obróbce co 4 ° C | Tak. |
|  4b  | Inwersja termiczna | Pomarańczowa próbka, w której powietrze na wysokości jest cieplejsze niż na ziemi | Tak. |
|  5  | PSKReporter | Odbieranie stacji PSKReporter (ciemny różowy koła) i, w różowym, co słyszą | Tak. |
|  6  | Dekodowany przeze mnie | Stacje odkodowania z WSJT- X, JTDX lub MSHV: Czerwone linie z QTH | Tak. |
|  6a  | QSO desunti dal live | Zamknięte połączenia ( RR73, 73, RRR) odczytywane w rozkładzie: zielona linia pomiędzy dwoma stacjami | Tak. |
|  7  | Spotlight | Połączenia: pełna niebieska linia, żółty pokryty do rozpraszania powietrza, dwie kulki | Tak. |
|  7a  | Z wyłączeniem | Casella Wyłączone poniżej 100 km: najkrótszy ruch nie jest ciągnięty ani wymieniony. Casella Ponad 1000 km: tylko kwadraty (przy starcie): jeśli jedna ze stacji znajduje się w odległości ponad 1000 km od QTH, bez linii lub okręgów, tylko place lokalizacyjne stacji zaangażowanych w lilac traktowane w 45 ° | Tak. |
|  8  | QTH i etykiety | Twoja stacja, nazwy wybranego miejsca | zawsze widoczne |

Wskaźnik tropo znajduje się bezpośrednio nad bazą i pod wszystkim innym: jest kolorowy próbkowanie, a wyższe umieszczone obejmuje linie i symbole. Siatka przechodzi nad nim, więc zawsze wiesz, gdzie upada kolorowy obszar. Isobares są cienkie linie i odczytywane znacznie powyżej pobierania próbek.

## 2. Podstawowa mapa

Koszty, granice i ziemia pochodzą z naturalnych danych o wektorach Ziemi, domeny publicznej [9], do skali 1: 50 milionów dla ogólnego widoku i 1: 10 milionów przy powiększaniu. Pliki znajdują się w folderze programu: brak map online. Kolory tematu Clear: jasnoniebieskie morze # E2ECF8, ciemnoniebieskie wybrzeże, jasnoszare granice. W tematach Black i Night kolory podstawy są ciemniejsze, w innych tematach są one lekko barwione.

## 3. Oba prognozy

Wszystkie warstwy przechodzą przez tę samą funkcję projekcji, więc zawsze są ze sobą powiązane. Wybierasz między dwoma projekcjami [10] (długość geograficzna λ, srogość, promień R Ziemi):

Rynek. Przechowuj lokalne narożniki i kształty; Odległość ciągnie się w kierunku biegunów. Obciąć do ± 85 ° szerokości geograficznej.

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

Azimutale equidtanti na twoim placu. Centrum (φ0, λ0) jest centrum placu WWL. Odległość i kierunek mierzone od centrum są prawdziwe: stacja na 1200 km jest 1200 km w skali, a kąt z północy jest azymut, do którego punktu anteny. Po zmianie lokalizacji mapa jest przeorientowana.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

W azymucie linie na papierze są proste (południki, równoległe, kwadratowe krawędzie) stają się zakrzywione. Z tego powodu wszystkie linie są podzielone na krótkie sekcje, każda sekcja jest prognozowana, a następnie cechy są zjednoczone; to samo dotyczy krawędzi indeksu tropo i izobare. Punkt do antypodów centrum nie ma określonej pozycji i pozostaje poza zasięgiem wzroku.

## 4. Lokalizator grill, zoom i ruch

Siatka Maidenhead jest zaprojektowana przez program: pola 20 ° × 10 ° z dwoma dużymi i wyblakłymi literami w środku, kwadraty 2 ° × 1 i z wystarczającym zoomem 5 ′ × 2,5 ′. Linie są niebieskie i cienkie, nie pokrywają indeksu tropu.

Zoom z koła w centrum na wskaźnik (punkt poniżej myszki pozostaje stały), przeciągając, dwukrotnie kliknij, aby powrócić do całego widoku, + i − klucze, a klucz z odsetkiem dla całego widoku. Szczegóły pasują do zoomu: lepsze wybrzeża, podkwadraty, grubsze etykiety isobare.

## 5. Dlaczego inwersji zginać fale

W niskiej atmosferze temperatura zwykle spada o 6,5 ° C na km; Wskaźnik refrakcji zależy zarówno od temperatury i wilgoci [3], a gdzie zmienia się gwałtownie z wysokości fala pochyla się do ziemi i dociera dalej niż zwykle. Najważniejszą warstwą jest to, że do około 3 km, gdzie powstają inwersji, kanały i komórki konwekcyjne [4]; ścieżki morskie są uprzywilejowane, ponieważ masy wody tworzą duże obszary inwersji [4]. [3]

Rozmiar używany do pomiaru jest refraktywny N, tj. (n − 1) · 106 [11]:

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

T to temperatura w kelwinie, ciśnieniu P i ciśnieniu pary, zarówno w hPa.

Liczy jako zróżnicowane N z udziałem (dN / dh). W standardowej atmosferze jest w przybliżeniu -39 N / km; do -79 N / km załamanie jest uważane za normalne, oprócz nadrefrakcji, i z dodatnimi gradientami podrefrakcji [7]. Pod -157 N / km fala jest zgięta bardziej niż krzywizna ziemi i pozostaje uwięziona: jest to kanał kanałowy [11]. F5LEN

## 6. Wskaźnik tropo: od dwóch map do średniej

Hepburn i F5LEN zakładki są biurkiem kontrolnym: pokazać zdjęcia pobrane ze strony każdego źródła, z edytowalnym czasem prognozowania, i służyć do kalibracji czytania klikając na punkty odniesienia i kolory legendy. Warstwa indeksowa tropu pozostaje pusta, aż co najmniej jedna z dwóch kalibracji nie będzie aktywna i kompletna.

Hepburn. Wskaźnik Hepburn mierzy, ile fal ma być złożonych na obszarze, w skali liniowej od 0 do 10, które mogą również przekroczyć 10. 0 oznacza atmosferę standardową i tylko rozproszenie; 1-2 marginalne lub słabe nadrefrakcji; 3-4 nadrefrakcji z powierzchniami kanałów; indeks nie uwzględnia tropo rozproszenia i nie widzi gór [1]. Mapy obejmują 6 dni: 3 godziny przechodzi przez pierwsze 36 godzin, a następnie 6 godzin, aktualizowane codziennie do 18 UTC [1]; dane wyjściowe (temperatura, wilgotność, ciśnienie) pochodzą z globalnego modelu kanadyjskiej usługi pogodowej [1]. [1]

F5LEN. Dla Europy F5LEN publikuje 31 map od + 3 do + 192 godzin (co 3 godziny do + 48, co 6 do + 96, a następnie co 12), z datą i czasem ważności w tytule, czas produkcji w prawym dolnym rogu i legenda klasowa: Disturb, Normal, Minor, Weak, Good, Strong, Vy Strong, Duct [2]. Światowa prognoza F5LEN jest obliczana przez wskaźnik załamania powietrza w Jednostce N, na 40 etapach po 3 godzinach (około 5 dni), aktualizowany 4 razy dziennie; kliknięcie na mapie pokazuje średnią wartość i lokalizator tego punktu [2].

Już się robi. Dla każdego etapu przewidywania program wykonuje te kroki:

1. Wypłata. Obrazy dwóch prognoz są pobierane w pamięci podręcznej obok wykonywalnego, raz do aktualizacji źródła, nie do żadnego przeprojektowania. Przed ostateczną wersją należy zwrócić się o zezwolenie do Hepburn i F5LEN.
2. Automatyczne georeferencje. Program znajduje ramę (dwie poziome i dwie pionowe linie tego samego koloru), a w systemie Windows 10 / 11 odczytuje OCR zintegrowane z systemem etykiety osi (54N, 10W, 0, 5E...). Odciąć taśmy wokół ramy, powiększyć je, przetestować oba znaki, gdy litera N / S / E / W nie czyta i zachować linię, na której spada większość etykiet: złe odczyty pozostają poza. Stąd można uzyskać ekstremalne lat / lon ramy i typ projekcji (regularne grill lub Mercator). Ekstremacje ratują się i ponownie odczytują, jeśli rama zmieni rozmiar. Na Mac i Linux, bez OCR, skrajności fabryczne lub odręczne są ważne w zakładce. Alternatywnie, dla map, które nie mają czytelnych osi: Każdy rodzaj mapy (Hepburn Europa, F5LEN na całym świecie...) posiada kalibrację: niektóre punkty kontrolne ze znanymi współrzędnymi (krzyże południków i równoległych żeber, rozpoznawalne żebra) łączą piksele z szerokością i długością geograficzną, w projekcji używanej przez to źródło. Kalibracja jest w pliku obok programu i można remake jeśli źródło zmienia format.
3. Czytanie kolorów. Każdy piksel jest porównywany z legendą źródłową i przeliczany na najbliższą wartość liczbową. Koszty, pisma i siatki narysowane na pierwotnej mapie są uznawane za "brak danych" i wypełnione pobliskimi wartościami.
4. Sieć światowa. Każdy obraz, po odnalezieniu ramy i poznaniu jej skrajności, jest umieszczony na Ziemi; wartość w dowolnym punkcie świata pochodzi z obrazów, które go pokrywają. Jeśli więcej regionów pokrywa ten sam punkt, warto szczegółowo (więcej pikseli na stopień), pod warunkiem, że jego czas jest w odległości 1,5 h od tego wyboru; następnie najbliższa godzina, a następnie ostatni wyścig. W przypadku gdy żaden obraz nie obejmuje punktu mapa pokazuje szarą zasłonę: brak danych, które różnią się od "indeksu 0". Wewnątrz ramy, piksel bez koloru legendy jest 0 (normalna atmosfera); wybrzeże, granice i pisma są skaczone patrząc na najbliższy kolor w ciągu 3 pikseli.
5. Ta sama drabina. Hepburn jest już w skali 0- 10 [1]: ciemne tło = 0 i dziesięć kolorów w kolejności (fioletowy, niebieski, zielony woda, limonka, żółty, ocher, pomarańczowy, czerwony, różowy, jasnoróżowy) dla 1... 10, liliowy poza. F5LEN używa klas (Disturb, Normal, Minor, Weak, Good, Strong, Vy Strong, Duct) [2], ale niektóre mają dokładnie kolory Hepburn: Słaby = 1, silny = 5, Vy Strong = 7, Duct = 8; Minor to 0.5 i Good (green) 3.5. Mnożnik skali, w zakładce F5LEN, koryguje zestaw, aby uczynić go porównywalnym z Hepburn.
6. O tej samej porze. Pasek czasowy przechodzi od + 0 do + 192 h w odstępach 3 godzin. Dla każdego kroku, który podejmujesz z każdego źródła, prognoza ważna w najbliższym czasie; Jeśli źródło nie ma tego kroku, media używają tylko drugiego i legenda wskazuje go.
7. Media. Z przyciskiem Media każdy punkt jest wart (Hepburn + F5LEN) / 2; z Hepburn lub F5LEN widzisz tylko jedno źródło, przydatne do zrozumienia, gdzie oba prognozy się nie zgadzają.
8. Rysunek. Wartość jest kolorowana w skali legendy (0 niebieski, 2 zielony, 4 żółty, 6 pomarańczowy, 8 czerwony, 10 fioletowy) i wyświetlana jako próbka pod siatką. 0 jest przezroczysty, więc gdzie nic się nie dzieje widzisz czystą podstawową mapę; ogólna przejrzystość dostosowuje się do kursora obok pola.

Na zakładce spot, indeks wzdłuż trasy jest średnią wartości siatki ligi co 10 km na maksymalnym okręgu między dwoma stacjami.

## 7. Isoboy

Wysokie stabilne ciśnienie jest pierwszym sygnałem, którego należy szukać dla tropu [3], a połączenia poza analizowanymi Alpami IK3XTV były również z wysokim ciśnieniem [4].

1. Data. Ciśnienie na poziomie morza z globalnego modelu GFS NOAA [12], odczytywane przez bezpłatną usługę Open- Meteo [15] na siatce 3 ° × 3 ° (od 30 ° do 72 ° N, od 30 ° W do 48 ° E), przez następne 78 godzin. Program aktualizuje dane przy starcie, a następnie co 6 godzin, jak model wyścigowy.
2. Linie. Izobare pochodzi z siatki co 4 hPa (... 1012, 1016, 1020...) z algorytmem kwadratów marszowych (kwadraty marszowe), a następnie są lekko zaokrąglone i wyświetlane rysowane drogą.
3. Centra. Największe lokalne maksimum i minima są oznaczone symbolem H (niebieski) i L (czerwony) oraz wartością hPa.
4. Etykiety. Wartość pojawia się wzdłuż każdej isobara, na tle koloru mapy, aby pozostać czytelne ponad próbkowanie.

Strefa i krok. Dane są pobierane na obszarze, na który patrzysz, powiększone o 25%, z krokiem od 1 ° do 20 ° wybranym do pozostania poniżej 400 punktów (Open-Meteo liczy każdy punkt jako żądanie: 600 na minutę, 5000 na godzinę): powiększenie lub przesunięcie mapy z obszaru już pobiera najgęstsze przeładowanie, do maksimum co 10 minut; jeśli aktualizacja zawiedzie poprzednie dane pozostają i spróbuje ponownie po 3 minutach. W ten sposób izobara i temperatury działają wszędzie na świecie.

Temperatura. To samo żądanie przynosi temperaturę do 2 m, 925 hPa i 850 hPa. LA0BY pokazuje, że zdarzenie od 22 / 11 / 2018 do UA6 było widoczne na mapach temperatury (900- 3000 m), ale nie w prognozach Hepburn [3]; DF5AI i DK5YA używają pionowych profili temperatury do rozpoznania inwersji [5]. Do tego są dwie warstwy:

- Temperatura 850 hPa: izotopy traktują co 4 ° C (czerwony powyżej zera, niebieski poniżej, fioletowy na zero).
- Inwersja termiczna: pomarańczowe pobieranie próbek, gdzie powietrze w temperaturze 925 lub 850 hPa jest cieplejsze niż w temperaturze 2 m; im bardziej intensywne, tym silniejsza jest inwersja. Jest to stan fizyczny, który pochyla fale do podłoża [3] [11], i porównać go z indeksem tropo i izobare pokazuje, czy prognozy mają bazę pogodową.

## 8. Plamki i stacje

Spot. Każde miejsce z obiema znanymi pozycjami jest linią na maksymalnym okręgu pomiędzy dwoma stacjami, podzieloną na sekcje i prognozowane; w Mercatore pojawia się następnie lekko zakrzywione, w azymutale jest prosto, jeśli zaczyna się od środka. Niebieska linia pełna dla normalnego miejsca, żółta pokryta ciemną krawędzią dla rozproszenia powietrza (< AS >, < ACS > lub "rozproszenie powietrza" w komentarzu), kula dla każdej stacji, lekka halo pod linią, aby odłączyć ją od pobierania próbek. Wybrane miejsce jest częściej projektowane na ostatnie i z dwoma nazwami.

Słucham. Każdy PSKReporter Stacja odbiornika [16] to ciemny różowy pusty krąg. Od każdego początku pełne różowe linie w kierunku stacji, które dekodowały i odesłane z powrotem do sieci, zawsze widoczne bez kliknięcia, z nazwami zawsze napisane. Wybierając okrąg jego linie są grubsze. Spot i słuchanie pokazuje tylko pasmo i zakres wybrane na liście: zmiana szerokości pasma opróżnienia mapy i przeprojektowanie z danych już zebranych dla nowego pasma.

Podjęłam decyzję. Każda stacja, która dekoduje z WSJT- X, JTDX lub MSHV (paragon za pośrednictwem UDP) jest czerwona linia pełna od QTH do stacji, z piłką i nominacją. Jest to najbardziej bezpośrednie porównanie z prognozą: pokazuje, gdzie odbiornik naprawdę przychodzi teraz.

## 8 bis. Kolory i symbole

Każdy kolor ma tylko jedno znaczenie, równe we wszystkich tematach (zmiana tylko tła).

| Kolor i znak | Znaczenie |
| Ciągła niebieska linia | Spot z klastra DX, RBN lub DM4X pomiędzy dwoma stacjami |
| Żółta linia z ciemną krawędzią | Spot z komentarzem rozpraszania powietrza (refleksja na temat samolotów) |
| Ciemny różowy pusty krąg | Stacja odsłuchowa na PSKReporter (MQTT) |
| Cienkie różowe linie od koła, z różową kulą na dole | Stacje, które słuchacz słyszał, z nazwą zawsze napisaną; grubsze linie dla wybranego słuchacza |
| Czerwona linia i czerwona nazwa | Twoje dekodowania UDP (WSJT- X, JTDX, MSHV): Kim jesteś? |
| Uderzenia migoczące linii czerwonej | QSO w toku z tą stacją: Zadzwonił do ciebie, a zamknięcie jeszcze nie dotarło, lub jest DX Call WSJT- X / JTDX z włączoną transmisją. Dalej RR73, RRR lub 73 |
| Zielona linia | QSO desunti dal live: zamykanie RR73, RRR lub 73 widoku UDP |
| Czarna linia z białą granicą | Ktoś do ciebie dzwoni: trwa do RR73/RRR / 73 lub do 3 minut |
| Kwadraty 45 ° | Ruch z co najmniej jedną stacją poza wybraną koroną: Brak linii, tylko kwadraty lokalizator |
| Szukaj lilii z odległością na północ | 1000, 700 i 500 km wokół QTH; ten wybór jest bardziej zaznaczony |
| Koło i punkt w kolorze motywu | Twój QTH, z nazwiskiem i właścicielem |
| Piłka pomarańczowa z GRAVES 143,050 | Radary GRAVES ( JN27SI) |
| Próbka od niebieskiego do czerwonego | Wskaźnik tropu Hepburn (0- 10) lub klasy F5LEN, lub ich średnia; cieplejszy kolor, lepsze warunki |
| Ciemnoszary aksamit | Obszar bez danych dotyczących tropu dla wybranego czasu |
| Grey- niebieskie linie o wartości w hPa | Izoberować co 4 hPa; H w niebieskim wysokim ciśnieniu, L w czerwonym niskim ciśnieniu |
| Czerwone, niebieskie i fioletowe linie | Izotermia do 850 hPa co 4 ° C: czerwony powyżej zera, niebieski poniżej zera, fioletowy zero |
| Pomarańcza próbki | Inwersja termiczna: powietrze do 925 hPa cieplej niż na ziemi, bardziej intensywne z silniejszą inwersji |

W raportach podejrzewanych krawędź każdej zakładki mówi o obowiązującej regule: zielony dobry, czerwony Scarta, szary bez reguły.

Jakie są kwadraty liliowe pokryte w 45 °. Ruch z dala od QTH interesuje, aby zrozumieć, gdzie rozmnażanie jest otwarte, ale jego linie, setki kilometrów długości i często dziesiątki, pokrycie mapy i ukryć to, co się liczy dla Ciebie: otwory można wykorzystać ze stacji. Z tego powodu, poza wybraną koroną, program nie rysuje linii, ale kolor kwadratów lokalizacyjnych (2 ° × 1 °) z zaangażowanych stacji. Rezultatem jest mapa "aktywnych stref": gdzie pojawia się wiele pobliskich liliowych kwadratów, istnieje kanał lub otwarcie, nawet jeśli nie dotyczy bezpośrednio; Jeśli obszar rozszerza się w kierunku Ciebie, jest to sygnał, aby skierować antenę w tym kierunku. 45 ° deska rozdzielcza pozwala zobaczyć pod indeksem tropo i siatki, więc porównują one w skrócie prognozę (dolny kolor) i prawdziwy ruch (liliowy). Kwadrat QTH nigdy nie barwi, ponieważ jest punktem wyjścia, a nie odległym obszarem. Tylko kwadraty, które są za koroną, są kolorowe: kwadrat skrzyżowany przez koronę, lub wewnątrz, pozostaje wolny, nawet jeśli istnieje jedna z dwóch stacji. Aby zobaczyć linie, wystarczy wyłączyć pudełko lub wybrać większą koronę.

Kryteria decydujące o tym, co jest na mapie i co jest wyrzucane, są wyjaśnione po kolei w dokumencie 1, sekcja 11 ter.

## 9. Pasek czasu

Poniżej na mapie: data i godzina pokazanej prognozy, kursor od + 0 do + 72 h w 6 godzin, legenda indeksu 0- 10 z nazwą wybranego źródła i czasem wyścigu modelu dla isobare. Trąbka i indeks isobare podążają za kursorem; punkty, stacje słuchowe i wasze dekoracje są obserwacją chwili i zawsze pozostają tymi "adex". Kiedy kursor nie jest na + 0 są one atenuowane, aby pamiętać, że nie należą do czasu prognozowania.

## 10. Jak odczytać pokrywanie

- Wysokie ciśnienie + wysoki wskaźnik + miejsce wzdłuż kolorowego obszaru: otwarcie jest tam i jest to planowane. Czas zadzwonić w tym kierunku.
- Wysoki indeks, ale nie ma miejsca: lub nikt nie jest aktywny tam, lub przewidywanie jest zbyt optymistyczne. Warstwa odsłuchowa pomaga odróżnić: jeżeli w okolicy są odbiorniki i nic nie czują, to druga hipoteza jest bardziej prawdopodobna.
- Długie miejsca poza kolorowymi obszarami: wysoki kanał, którego prognozy powierzchni nie widzą, jak w zdarzeniu LA- UA6 [3]; lub inne rozprzestrzenianie uciekł z filtra (np. E sporadyczny bez znacznika). DF5AI i DK5YA odróżnić tropo i E sporadycznie również od godziny: otwory, które rodzą się i kończą razem z rozproszonym sporadykiem E nie są prawie [5] tropo.
- Hepburn i F5LEN nie zgadzają się: porównać dwa źródła z przyciskami i zobaczyć, gdzie prawdziwe miejsca upadku.
- Ciśnienie w dół: G4MCU po otwarciu nadajnika SK6VHF do czasu spadku ciśnienia; 1012 hPa uznało ukończone zdarzenie [13]. Oglądaj isobare przewijając pasek czasu: jeśli wysokie ciśnienie jest wycofane, otwarcie prawdopodobnie następuje.
- Sygnały, które idą w górę: w studiu WSPR przy 144 MHz KP4MD stosunek sygnału do szumu wzrósł w prawo w oknach, gdzie Hepburn przewidywał poprawę i spadł, gdy prognoza usunęła [14]. Jeśli na tej samej ścieżce rośnie, otwiera się.
- Ścieżki górskie: indeks nie bierze pod uwagę ulg [1], ale połączenia poza Alpami istnieją również dzięki konwekcyjnym komórkom i falach grawitacji atmosferycznej [4], a LA0BY pracował 9A i S5 przez góry bez Hepburn przewidywania [3].

## 11. Punkt otwarty

- Zezwolenie na użycie map Hepburn i F5LEN w programie.
- Kalibracja współczynników konwersji F5LEN → skala 0- 10.
- Kalibracja punktów kontrolnych dowolnego rodzaju mapy, aby remake jeśli źródła zmieniają grafikę.
- Opcjonalna trasa z temperaturą na wysokości 850 hPa od GFS.

## Główne źródła

1. W. R. Hepburn, DX Info Centre, "The Hepburn Tropo Index (HTI)" oraz "Tropospheric Ducting Reconserve" (dxinfocentre.com). Definicja indeksu i skala, wartości ujemne i więcej niż 10, wyłączenie rozrzutów i reliefów, etapy 3 i 6 godzin w ciągu 6 dni, uaktualnienie do 18 UTC. Pochodzenie danych z światowego modelu kanadyjskiego: G. Meisse, AMSAT- BB, 27 / 08 / 2007.
2. F5LEN, prognoza dla Europy (tropo.f5len.org / fopecasts- for- europe) oraz prognoza światowa propagacji tropoferycznej VHF (tropo.f5len.org / WW) i zaznaczyła "Nowa wersja narzędzia prognozowania propagacji troposferycznej VHF". Wskaźnik jednostki N, 40 kroków od 3 godzin, 4 aktualizacje dziennie, wartość i lokalizator za kliknięcie.
3. S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", 41 ° Nordic VUSHF Meeting, Skjeberg 2019. Normalny gradient 6,5 ° C / km i inwersji do 10- 15 ° C / km, temperatura i wilgotność rola, wysokie stabilne ciśnienie, LA- UA6 zdarzenie widoczne w wysokiej temperaturze i nie w Hepburn, LA- 9A / S5 połączeń poza górami.
4. F. Egano IK3XTV, "Propagation VHF - The convective cells and consumative gravitation waves in tropo propagation". Znaczenie pierwszych 3 km, szlaki morskie, komórki konwekcyjne, fale grawitacyjne na Alpach, połączenia wysokiego ciśnienia, wspólna objętość do 3- 4 km.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, "Very Long Distance Propagation in the 144 MHz Band", 24 / 7 / 2003. Pionowe profile temperatury i punktu rosy do rozpoznania inwersji, użycie indeksu Hepburn, rozróżnienie pomiędzy tropo i E sporadycznie w zależności od godziny.
6. J. N. Gannaway G3YGF, "Tropospheric Scatter Propagation", QST November 1983. Równoważnik promienia Ziemi i utrata ścieżki używanej w kartach indeksowych i punktowych (zob. dokument 1).
7. VHF / UHF DX Book, rozdział 2 "Propagacja troposferyczna", s. 2-23... 2-30. Rysunek stref refrakcji zgodnie z dN / dh (norma -39 N / km, granice -79 N / km i 0) oraz profile powierzchni i wysokiego kanału. Tekst dostarczonej kopii nie jest czytelny: wykorzystano jedynie dane liczbowe.
8. Podsumowanie projektu 8 / 11 / 2026, dostarczone przez autora programu. Kolory mapy, wymagane projekcje, warstwy, pasek czasowy.
9. Naturalna Ziemia, dane wektorowe wybrzeży i granic, domena publiczna (naturalheartdata.com). Odniesienie ogólne, niezałączone.
10. J. P. Snyder, "Map Projections - A Working Manual", USGS Professional Paper 1395, 1987. Formularze projekcji Mercatora i azymutale equidtanti. Odniesienie ogólne, niezałączone.
11. Zalecenie ITU- R P.453, radiowy wskaźnik refrakcji. Wzór refraktywny N i próg kanału przy -157 N / km. Odniesienie ogólne, niezałączone.
12. NOAA NCEP, Globalny System Prognozowania (GFS). Ciśnienie na poziomie morza 0,25 °, cztery wyścigi dziennie. Odniesienie ogólne, niezałączone.
13. G4MCU, artykuły z tagiem "tropospheric" (g4mcu.net / tag / tropospheric), 2018- 2025. Refrakcja, przewód i rozproszenie; częstsze kanały o wysokim ciśnieniu, zwłaszcza na morzu; promień świetlny SK6VHF szedł do spadku ciśnienia przy 1012 hPa (czerwiec 2019).
14. C. F. Milazzo KP4MD, "VHF / UHF WPR Propagation Study" (qsl.net / kp4md / 144 _ mhz _ wsr.htm), 2012-2014. SNR z WSPR zgłasza 144 MHz rosnące w oknie poprawy przewidywane przez Hepburn i spada, gdy prognoza znika.
15. Open- Meteo, prognoza pogody API (open- meteo.com), darmowe niekomercyjne stosowanie. Ciśnienie na poziomie morza modelu GFS w wielu punktach w jednym wniosku.
16. mqtt.pskreporter.info, dystrybucja MQTT w czasie rzeczywistym reklam pskreporter. info: nominacja i właściciel odbiorcy (zob. dokument 1).
