# Podręcznik prognozowania

> Automatyczne tłumaczenie przez włoski: w przypadku wątpliwości warto włoskiego przewodnika.

Prognoza zakładki oblicza samo propagację tropoferyczną: mapę indeksową na widocznym obszarze oraz sygnał dostarczony pomiędzy Twoją stacją a odpowiadającą jej. Podręcznik ten opisuje wszystkie dane, sposób ich obliczania i źródło, z którego pochodzą. Liczby w nawiasach kwadratowych odnoszą się do źródeł na dole; w programie wszystkie dane wyników mają komiks (simbolo i) z tym samym źródłem i odniesieniem do akapitu.

## 1. Co robi i co robi

Przewidywanie pionowych profili temperatury i wilgotności modelu GFS [1, 2] wynika z reaktywności warstwy powietrza po warstwie i szacuje, jak bardzo sygnał zyskuje w porównaniu z normalną atmosferą. Na trasie dodaje prawdziwy grunt (Copernicus GLO- 30 [3]), dyfrakcji, troposcatter, kanałów i hałasu odbiornika.

Nie jest to środek: jest to oszacowanie wykonane na modelu pogody z odległych poziomów setki metrów. Służy do porównywania sytuacji (dziś lepiej niż wczoraj, 70 cm lepiej niż 2 m, to traktować lepiej niż to), nie do zagwarantowania połączenia. Ustęp 14 wyjaśnia, jak zebrać dowody, aby je potwierdzić.


> - Zakaz nie może być uogólniony, ale studiowany w odniesieniu do miejscowej morfologii. "
> - Volker Grassmann, DF5AI, zgłoszona przez Flavio Egano, IK3XTV (Rozmnażanie na 2 metry)

Jest to zasada, która kieruje obliczeniem punktu punktowego: każda trasa jest obliczana na jej glebie, z jej horyzontów, morza i reliefów; mapa daje ogólny obraz, traktuje odpowiedź.

## 2. Polecenia językowe

- Zakres: 144, 432 lub 1296 MHz. Mapa i obliczenie użyj wybranego pasma.
- Teraz: kursor przesuwa prognozę od teraz do 72 godzin do przodu, w ciągu 3 godzin.
- Prognoza aktualizacji (widoczna strefa): pobrać profile GFS na siatce o wartości co najwyżej 240 punktów obejmujących widoczną mapę. Siatka (0,5 ° lub więcej) zależy od powiększenia.
- Stop: przerwać pobieranie lub obliczenia.
- Szczegóły do pokazania (prawy panel mapy): trzy poziomy, Podstawowe, Normalne i Kompletne, włączyć grupę pól; każde pole może następnie zmienić ręcznie. Pudełka są w trzech grupach:
  - Na mapie: izolacja del Gain, także 3- 7 dB (tracking), izobare, inwersja termiczna, znaki pogodowe S ↓ ↑, przyrost / ucisk korelacji, strefa bezpośredniego widzenia (ust. 4, 5, 7 i 9 bis). Kolory zawsze zostają.
  - Przy obliczaniu sygnału: wszystkie linie obliczeń, płaski profil Ziemi, dziennik obliczeń i szczegółowy dziennik plików.
  - Korekty i dane na żywo: korekty pogody (Misme, subsidence), kontrast powietrzno-morski, średnia z plamami (corridoi), skoki APRS 2 m i LoRa, nad górą bez strat (ust. 4 bis, 9 bis, 9 quater i 10 bis).
  - Niezbędny posiada tylko izolatki, korekcje pogody i mediów z plamami; Normal dodaje izobare, inwersja, znaki pogodowe, bezpośredni widok, płaska ziemia, morze i APRS; Kompletne włącza go. Poziom zmienia tylko to, co widzisz i co korekty wprowadzić: obliczenia pozostają takie same.
- Źródło danych i przypadek historyczny: skąd pochodzą profile i którego dnia szukasz (paragrafy 3 bis i 3 ter).
- Moja stacja i korespondent: Lokalizator, anteny, zasilanie, kable, hałas (pkt 7 i 12).
- Obliczyć sygnał i zasięg w optyce: dwa obliczenia na podłożu (pkt 8- 13).
- Rejestr obliczeń: każdy krok z zastosowanymi numerami, w tym w pliku Data / foprast- calcoli.log.

## 3. Od danych GFS do profilu

Dla każdego punktu siatki i każdej godziny odczytujemy ciśnienie na ziemi, temperaturę i wilgotność na 2 m, wiatr na 10 m, wysokość warstwy granicznej, CAPE oraz temperaturę i wilgotność w stosunku do 1000, 975, 950, 925, 900, 850, 800, 700, 600 i 500 hPa. Od 1.3.0 można również odczytać wiatr do 850 hPa oraz prędkość pionową do 850 i 700 hPa (pkt 9 bis).

Poziomy są obliczane za pomocą wzoru ipsometrycznego i temperatury wirtualnej; poziomy poniżej podłoża są ograniczone. Dla każdego poziomu:

- N = 77,6 / T · (P + 4810 · e / T), z T w kelwinie, P i hPa oraz przez ciśnienie nasycenia ITU- R P.453- 14, eq. 9, z współczynnikiem korekcji śladu środowiskowego (ponownie sterowanym w oficjalnym tekście) [4].
- M = N + 157 · h, z h w km: zmodyfikowana reaktywność. Tam gdzie M idzie na górę jest kanał.

## 3 bis. Źródło danych

Menu Źródło danych wybiera, skąd pochodzą profile:

- Automatyczne (zalecane): GFS najpierw z NOMADS [14], usługi NOAA, która dystrybuuje model: na każdą chwilę program prosi o dwa wycinki obszaru w formacie G RIB2 i czyta je od siebie; NOMADS nie ma dziennego limitu, tylko jedną minutę, że program szanuje. Z szerokimi sieciami (od 0,5 ° do góry) używać plików do 0,5 ° lub 1 co 3 godziny, dużo lżejsze. Punkty, które NOMADS nie daje, lub wszystkie, jeśli NOMADS nie odpowiada, zapytaj Open- Meteo [2]. Te dwa źródła dają ten sam model i ten sam wyścig, więc można mieszać w tej samej siatce.
- GFS z Open- Meteo: tylko Open- Meteo, jak w 1.2 (przy limicie na minutę).
- GFS z NOAA NOMADS: tylko NOMADS. Wolniej pobrać (dwa pliki na godzinę, pół sekundy pomiędzy jednym żądaniem a drugim, aby nie przekroczyć limitu NOAA), ale nie zużywa limitu Open- Meteo.
- ICON- EU (DWD) i ECMWF IFS 0,25 °, z Open- Meteo: modele inne niż GFS, które mają zastosowanie w całej sieci i dla tras. Nie mieszają się one z GFS: porównanie dwóch modeli w tym samym czasie jest prostym sposobem, aby zobaczyć, jak prognoza jest niepewna. ICON- EU obejmuje tylko Europę; ECMWF nie ma wszystkich poziomów ciśnienia GFS (brak poziomu skoku) i może nie mieć warstwy granicznej.

Używane źródło jest zapisane w linii statusu i w pierwszej linii wyników (Źródło danych), z iloma punktami pochodzą z każdej usługi i ile z pamięci podręcznej. Pamięć podręczna dysku jest oddzielana przez model.

## 3 ter. Przypadki historyczne

Historyczne Menu przypadku przegląda udokumentowane otwarcie z prognozami GFS archiwum Open- Meteo [15]: mapa przenosi się na obszar sprawy, zespół ustawia się i kursor czasu odchodzi od pierwszego dnia sprawy. Przez naciśnięcie Prognoza aktualizacji można pobrać profile tych dni; Obliczanie sygnału działa jak dla bieżącej prognozy. Służy do porównania modelu z tym, co naprawdę się stało.

Pierwszy przypadek to to, że 14- 15 sierpnia 2022 powiedział w QST (listopad 2022, "Świat powyżej 50 MHz" [23]): otwarcie pod wysokim ciśnieniem między Manitoba i Kansas / Missouri, od 144 do 1296 MHz prawie wszystkie w FT8, z rekordu kanadyjskiego na 10 GHz 1228,56 km w Q65 i sygnałów do 1941 km. Drugi to otwarcie w okresie od 29 grudnia 2021 r. do 2 stycznia 2022 r. na Tyrrenian i zachodnim wybrzeżu Morza Śródziemnego przy 144 MHz, z raportów IC8TEM: pierwsze otwarcie 29 (do 737 km), szersze 31 ( IT9GSF - EB5EA 1217 km), wielkie otwarcie 1 stycznia między Włochami, Sycylią i Hiszpanią. W przypadku starszych przypadków (np. wrzesień 2000) archiwum Open- Meteo nie posiada żadnych danych. W archiwum brakuje wysokości warstwy limitowej: nie ma zastosowania jej korekcja.

## 4. Indeks i kolory

Dla każdej warstwy pomiędzy dwoma poziomami program porównuje spadek N z progami tabeli 6.5 Fasoli i Dutton [5]: na przykład od 1000 do 850 hPa spadek do 60 N / km jest normalny, ponad 100 N / km jest super-refrakcji. Warstwa superrefraktywna jest warta od 10 do 15 dB zysku; przewód, który usidla pasmo od 20 do 25 dB, zgodnie z deficytem M. Silny wiatr do ziemi, wysoka warstwa graniczna i wysoka CAPE zmniejsza zysk, ponieważ usuwają lub łamią warstwy [5, § 4.4].

Kolor jest szacowanym zyskiem w dB, o stałej skali równej we wszystkich tematach:

| Gain | Kolor | Znaczenie |
| Poniżej 7 dB | Nikt. | niemal normalna atmosfera |
| 7- 10 dB | niebieski | gradient nad normalnym |
| 10- 13 dB | zielone | warstwa superrefrakcyjna |
| 13- 16 dB | żółty | mocna warstwa, ale bez przewodu dla taśmy |
| 16- 18,5 dB | pomarańczowe | bardzo silna warstwa lub zbyt cienki kanał |
| 18,5- 21 dB | czerwony | Zespół pułapkowy |
| od 21 dB | Fioletowy | Mocny i głęboki kanał |

Z wyświetlaczem również 3- 7 dB strefy między 3 a 7 dB, gdzie coś jest formowane, ale pozostaje pod skalą, można zobaczyć z jasnoszarym zaciskiem; kolory nie zmieniają się. Zkropkowana tafla wyznacza obszar objęty własną prognozą i pokazuje maksymalne widzialne korzyści: jeśli jest poniżej 7 dB, mapa pozostaje bez kolorów, ponieważ atmosfera jest prawie normalna, nie dlatego, że brakuje danych. Podczas pobierania nowego kliknięcia na prognozę aktualizacji nie przerywa; aby go zatrzymać jest Stop.

Kolory są nieprzezroczyste do 82%, więc tło tematu po prostu je zmienia. Siatka lokalizacyjna i kwadratowe nazwy są projektowane na kolorach. Gdzie nie ma prognozy, ale Hepburn lub F5LEN dać 5 lub więcej kart szachowych pojawiają się z kolorem tego źródła.

Mapa ocenia każdy punkt sam. Obliczanie sygnału zamiast tego kontroluje, że warstwa jest ciągła wzdłuż drogi (pkt 9): droga, która przekracza bardzo żółty może dać mniej niż mapa sprawia, że myślisz.

## 4 bis. Prognoza za pośrednictwem reklam: korytarze (z 1.3.3)

Polityka praw autorskich: Sam link nie może nigdy rysować przewodu. Odizolowana i krótka recepcja może być rozproszeniem samolotu tranzytowego (pakiet APRS jest bardzo krótki), otwarciem kilku minut lub błąd, i daje "drop-zachowanie" oni oszukują. Program rozpoznaje korytarz, kiedy w tym samym zespole:

- co najmniej 3 pary niezależnych stacji (bez wspólnej stacji) pracują na trasach o długości co najmniej 300 km;
- drogi są równoległe: azymut w zakresie ± 15 °, średnie punkty w zakresie 150 km od wspólnej osi;
- wszystkie w ciągu ostatnich 30 minut, przy czym wiązka powtarza się w odległości co najmniej 10 minut (wytrwałość);
- dla ARS, skok liczy się tylko, jeśli ten sam skok jest powtarzany;
- od 1.3.4 trasy o długości ponad 1500 km liczą się tylko wtedy, gdy wiązka pochodzi z co najmniej dwóch różnych źródeł (np. PSKReporter i klaster): od jednego źródła, do tych odległości w odległości 2 m, są częściej EME, meteoryt rozproszony lub niewłaściwe lokalizatory niż tropo.
- od 1.3.4 nie należy liczyć stacji "do ray": którzy w ciągu ostatniej godziny mają co najmniej 5 korespondentów na 1000 km rozłożonych na wentylator o co najmniej 150 °. W 2 m trop jest kierunkowy; odbiornik, który "wolny" razem Izrael, Portugalia, Anglia i Włochy od 2000- 3700 km, często z pozytywnymi sygnałami, ma prawie na pewno częstotliwość skonfigurowana źle. W 17- 22 / 11 / 2026 I5MZY archiwum 72% zakwaterowania w 2 m ponad 1000 km pochodzi ze stacji, więc; 22 / 11 o 10: 53 UTC były OH6HLH, OZ1ITS, F4LUK, OM8ABC i SP2JLR, a filtr najdłuższy korytarz schodzi z 2175 km (regole 1.3.3) do 615 km.

Źródła: spoty klastrowe (nie oznaczone jako AS / ACS), PSKReporter za pośrednictwem MQTT, ich QSOS i WSJT- X / JTDX / MSHV odkodowania i APRS skacze w 2 m i LRa 433 / 868 MHz.

APRS: 2 m i LoRa. Program odczytuje APRS- IS tylko w trybie read- (rotate.aprs2.net, filtr na Europie) i wyciąga skoki radiowe pomiędzy znanymi stacjami lokalizacyjnymi (źródło → digipeater → IGATE), takie jak mapa VHF DXView i APRS PropView (RF- YVY, Apache 2.0). Trzy pasma są warte: 2 m, LoRa 433 MHz (korytarze pasmowe 432) i LoRa 868 MHz (corridoi liczące 1296: kanał, który przynosi 868 MHz również najwyższe częstotliwości). Zespół pochodzi z LoRa tocall (lista aprs- deviceid) oraz z częstotliwości lub słów w komentarzu; skok jest wart tylko pomiędzy dwoma stacjami tego samego zespołu.

Delikatnym punktem są aktywne powtarzacze: sygnał odbierany z prędkością 500 km może być dostarczony z dwoma lub trzema retransmisjami digipeater. Skok liczy się zatem tylko wtedy, gdy jest między dwoma realnymi i kolejnymi przekaźnikami:

- Ścieżka musi być wyśledzona: każdy digipeater, który się retransmitował, umieścił swoją nazwę, a skok jest mierzony między jednym a następnym; alias używany bez nazwy (W IDE1 *, W IDE2 *, alias regionalny) przerywa łańcuch i ten odcinek się nie liczy;
- ukryte retransmisje: program pamięta ścieżkę, o którą zwykle pyta każde źródło (na przykład W IDE2 -2). Jeśli pakiet jest wyposażony w skok w mniej (W IDE2 -1) i żadna nazwa go nie wyjaśnia, digipeater retransmitował go bez śledzenia i pakiet jest odrzucany; źródła nigdy wcześniej nie widziałem nie liczą;
- digipeater na środku drogi: jeżeli pomiędzy dwoma stacjami, mniej więcej na drodze, w ciągu ostatnich trzech godzin istnieje digipeater tego samego aktywnego pasma, skok zostaje odrzucony, ponieważ mógł być retransmitowany (zwłaszcza dla digipeater LoRa, który retransmituje bez zmiany ścieżki);
- Pakiety nieradiowe (qAC, qAX, TCPIP), obsługiwane przez internet przez radio z IGATE (osoby trzecie), HF i różnych częstotliwości, stacje na wysokości ponad 1500 m (palloni, samoloty) i skacze ponad 1500 km.

Wpływ na mapę (z 1.3.4). Wyciąga tylko obszar, na którym obowiązuje gwarantowane minimum: pas ± 60 km wokół centralnej części każdej trasy (od 15% do 85% jej długości), nie na stacjach na końcach, gdzie przewód może nie być. Opaski wszystkich korytarzy są narysowane razem, w neutralnym kolorze (tekst) i półprzezroczystym: nie dodaje się pokrywania, a kolor nie jest mylony z purpurą skali (21 dB i dalej). Etykiety (cechy, średnia odległość, minimalny przyrost) są skanowane ze sobą; przechodząc z myszą na etykiecie pojawiają się sekcje korytarza.

W obszarze i tylko w pobliżu obecnego czasu (± 90 minut) prognoza nie mieści się w minimalnym zakresie. Rozumowanie jest następujące: jeśli stacje naprawdę przejść, propagacja jest tam, cokolwiek model pogody mówi.

Ponieważ krótkie trasy są niewiele warte (z 1.3.4). W odległości 2 m, od 300 do 600 km, dwie dobre stacje (około stu watów, anteny, FT8 lub CW) łączą się prawie codziennie nawet z normalną atmosferą, dla troposcattera: sygnał rozproszony przez małe nieprawidłowości powietrza w wspólnej objętości dwóch wiązek. Trzy lub cztery miejsca na 500 km mówią, że zespół działa jak zwykle, nie, że istnieje otwarcie: GEKO Magazine n. 69 ( IU3QEZ) szacunkowe dla troposkattera z 2 × 10 pływającymi elementami o długości około 440 km w SSB QRP, 510 km w SSB o długości 50 W, 600 km w CW QRP i 640 km w SSB o długości 500 W (źródła, jedenaście okrążeń). Tylko ponad 600- 700 km troposkatter nie jest już wystarczające i seria równoległych połączeń naprawdę wskazuje przewód lub warstwa. Do 1.3.3 korytarz 450- 600 km podniósł prognozę do 9- 12 dB, a mapa wykazała poprawę, której nie było.

Wartość wyjściowa następnie idzie ze średnią odległością tras:

- do 400 km: 3 dB;
- od 400 do 700 km: sól w sposób liniowy od 3 do 10 dB (500 km → 5,3 dB; 600 km → 7,7 dB);
- od 700 do 900 km: od 10 do 16 dB; ponad 900 km pozostaje na 16 dB.

Pod 7 dB, czyli pod początkiem skali, minimalne pozostaje to, że: korytarz jest rysowany z etykietą, widać, że zespół działa, ale kolory mapy nie zmieniają się (z pola pokazuje również 3- 7 dB szare kropkowane). Ponad 7 dB wartość jest ważona, pół i pół, z liczbą niezależnych tras (3 uderzenia → 0.25; 6 lub więcej → 1) i z medianą S / N relacji (-20 dB → 0.5; 0 dB lub więcej → 1; nieznana, jak w miejscach klastra, → 0,75), począwszy od 7 dB. Przykłady: 3 trasy bez S / N na 500 km → 5,3 dB; 5 tras na 615 km → 7,7 dB; 3 trasy bez S / N na 900 km → 11.5 dB; 3 trasy do -20 dB na 900 km → 10,4 dB; 6 tras do 0 dB na 900 km → 16 dB.

Minimalna odległość pozostaje 300 km: podniesienie jej do 500 km również usunęłoby rzeczywiste otwory krótkiego zasięgu (na przykład przewód na Tyrrhenian 400 km, który jest rozpoznawany przez prognozę pogody i bardzo silne sygnały więcej niż z samej odległości). W archiwum I5MZY, 22 / 11 / 2026 o 10: 53 UTC, z tą zasadą i filtrem promieni najdłuższe korytarze wynosiły 615 km (7,7 dB) i 540 km (6,3 dB); z regułami 1.3.3 osiągnął 2175 km i 16 dB.

Do 1.3.3 minimum było ważne na wszystkich prostokątach korytarza i zależało tylko od odległości (od 10 dB do 300 dB do 16 dB do 900 km): z wieloma długimi korytarzami razem (na przykład 22 / 11 / 2026, dwanaście korytarzy do 2620 km) uniósł prawie całą Europę do 16 dB.

Krzywe zysku nie rysują już "gocki": mniejsze wyspy o długości 220 km w obu kierunkach pozostają kolorowe, ale bez zamkniętej krzywej, chyba że korytarz je potwierdza. Wyłącza się za pomocą "Prevision intervered by the spots".

## 5. Opcjonalne linie i porównanie z ciśnieniem i inwersja

Krzywe zysku: linie przy 7, 10, 13, 16, 18,5 i 21 dB, krawędzie skali, narysowane na polu wygładzone metodą kwadratów marszowych, z etykietą w dB.

Isobare: ciśnienie na poziomie morza co 4 od tego samego GFS, do godziny prognozy, z centrami H i L.

Inwersja termiczna: krzywe pokryte ilością powietrza w temperaturze 925 lub 850 hPa cieplej niż w temperaturze 2 m, + 1, + 3, + 5 i + 8 ° C

Pod kontrolą linia daje zysk porozumienie z naciskiem i z rewersem na widocznej strefie: Pearson współczynnik korelacji, -1 do + 1. Kanały często rodzą się w antycyklinach (powietrze, które spada podgrzewa i tworzy rewers), więc pozytywne porozumienie z ciśnieniem jest normalne, ale wewnątrz tego samego antycykliny zysk zmienia wiele między morzem, wybrzeżem i górami. Sama inwersja nie wystarczy: zginać fale tylko wtedy, gdy nad powietrzem jest nawet suszarka [5]. Linia służy zobaczyć, teraz, który z dwóch najlepszych wyjaśnia kolorowe obszary. To samo porównanie jest zapisane w rejestrze.

## 6. Potencjalne rurociągi: podwójne obliczenia

Poziom GFS wynosi od 200 do 1000 m: cienka inwersja jest rozprzestrzeniana i model nie widzi przewodu. Jeżeli pomiędzy dwoma poziomami spadek N przekracza wartość normalną (40 N / km), powietrze jest stabilne (gradient termiczny poniżej 3 K / km), a powyżej jest suchy (współczynnik wilgotności właściwy poniżej 0,8), program uwzględnia potencjalny kanał i oblicza go na dwa sposoby:

- Obliczanie A: wszystkie skoncentrowane nadmiar spadają w odległości 50 m.
- Obliczanie B: ten sam spadek rozmieszczony na typowej grubości rurociągów tego obszaru, zaczerpnięty z map klimatycznych ITU- R P.453 [4] (na ziemi lub wzniesionych).

Dla deficytu i dla maksymalnej długości fali uwięzionej najniższa wartość jest utrzymywana: jest to ostrożne oszacowanie.

ITU- R P.453 dane są © ITU i mogą być wykorzystywane do celów osobistych i niekomercyjnych, cytując źródło, ale nie mogą być redystrybuowane: do tego nie jestem w pakiecie programu. Aby włączyć kalkulację B, po prostu pobrać mapy P.453 z ITU w zakładce: program, po pokazaniu warunków użytkowania, pobrać archiwum rurociągów ze strony ITU (upled _ and _ surface _ duct.zip) do osobistego użytku użytkownika, wyodrębnić pliki w Data / P453 i włączyć obliczenia bez ponownego uruchamiania. Alternatywnie, pobrać ze strony ITU- R Study Group 3 digital product (itinu.int, ITU- R Study Group 3, Digital products, P.453, Surface data and Elevated duint data) i umieścić siedem S _ OCCUR. TXT, S _ THICK.TXT, S _ STREN. TXT, E _ OCCUR.TXT, E _ base.T Bez plików program używa tylko obliczeń A i zapisuje je w zakładce i w rejestrze. Stosunek między deficytem A a typową intensywnością P.453 jest anomalią: Ile razy dzisiejszy rurociąg jest silniejszy niż zwykle. P453

## 7. Stacje i lokalizatory

Właściciel może mieć 4, 6, 8 lub 10 znaków. Z 8 lub 10 znaków pozycja jest dokładna, a udział gleby pochodzi z Kopernika GLO- 30: wysokość anteny jest wystarczająca od podłoża. Z 4 lub 6 znaków kwadrat jest zbyt duży: lepiej jest zapisać kwotę anteny na morzu; jeśli 0 program pozostaje szacunkową z glebą SRTM w centrum placu plus wysokość od podłoża i sygnalizuje to w wynikach. Dla odpowiedniej wysokości domyślna jest 3 m od podłoża; z udziałem morza 0 jest używana gleba SRTM plus wysokość.

## 8. Grunty, dyfraktory i troposkatter

Profil trasy jest pobierany na Copernicus GLO- 30 co 30 m (ponad 120 km szerokości). Równoważnik promienia Ziemi wynosi k = 157 / (157 + ΔN), przy ΔN spadku N w pierwszym km od profili GFS wzdłuż trasy [6, 7].

Reliefy w pobliżu stacji (z 1.3.2, kryterium autora). P.452 płaci kąt horyzontu liniowo, 0.573 dB na mrad: ulga kilku dziesiątek metrów kilkaset metrów od anteny kosztuje dziesiątki dB, znacznie więcej niż jest obserwowane. Z tego powodu reliefy w odległości 20 km od każdej stacji mogą się sumować do 6 dB: kąt horyzontu z ich powodu jest ograniczony do najbardziej odległych reliefów plus 10,5 mrad. Jest ważny dla Twojej stacji i dla korespondenta. Linia "Near Reliefs" mówi, jak wiele DB startowało.

Źródło gruntów (z 1.3.2). Kwoty pochodzą z programu Copernicus DEM GLO- 30 [3]: 1 sekunda łuku, morze 0 i wybrzeże czyste. Do 1.3.1 w programie wykorzystano Terrain Tiles "skadi" AWS. W pobliżu wybrzeża place te mieszają SSRTM z danymi o niskiej rozdzielczości dna morskiego i rozprzestrzeniają udział wyspy na morzu: wokół Capri stworzyć pierścień widmo 80- 100 m między 0,5 i 1 km od brzegu. W dniu 21 listopada 2026 r. Ten fałszywy horyzont, + 2,6 °, kosztował około 26 dB troposkattera dla stacji przybrzeżnych. Szyby skadi pozostają rezerwą tylko wtedy, gdy program Copernicus nie odpowiada. Tafle Copernicus ważą 15- 30 MB i pobierają raz; tafle tylko morskie nie istnieją i są warte 0. IC8TEM

- Przestrzeń wolna: Lfs = 92,44 + 20 log f (GHz) + 20 log d (km) [8].
- Rozbieżność: Metoda Bullington na profilu rzeczywistym plus ITU- R P.452-16 § 4.2 [6] delta-Bullington sferyczna korekta ziemi, z ITU- R P.526 [9] sferyczna utrata.
- Pierwsza strefa Fresnel: promień ¶ (λ · d1 · d2 / d); wolny, jeśli ziemia pozostawia co najmniej 0,6 promienia. Dla każdej przeszkody wyświetlany jest parametr [9] Knife Board V i J (VV)..
- W konstrukcji profilu promień bezpośredni jest zielony w widoku optycznym i czerwonym, jeśli przechodzi pod podłożem lub pod krzywizną ziemi; czerwone kropki są przeszkody, które inwazji pierwszego obszaru Fresnel (fioletowy kropki). Na morzu "czerwony punkt" na wysokości 0 m nie jest górą: to morze wznosi się dla krzywizny. Przechodząc z myszą na promień lub punkt pojawia się komiks z wyjaśnieniem i wartościami.
- Kiedy ulga w pobliżu anteny zamyka kierunek (na przykład horyzont dziesiątek stopni), dyfrakcja i troposkatter dają straty setek dB: powyżej 300 dB program pisze "> 300 dB" i "kierunek zamknięty przez sąsiednią przeszkodę", sygnał "< -230 dB" i stosunek S / N "-60 dB". W takim przypadku zaleca się spojrzenie na "Zalecane rośliny" i "Via monte".
- Troposkatter: ITU- R P.452-16 eq. 45 do 50% czasu [6], z kątową odległością od horyzontów i N0 od profili.
- Bez tropu: suma mocy dyfrakcji i troposkattera, nigdy w wolnej przestrzeni.

## 8 bis. Troposcatter: cztery modele i niepewność

Obok P.452 program oblicza troposkatter z trzema innymi modelami, podobnie jak program TropoLink [16], i pokazuje ich różnicę jako niepewność:

- NBS TN101 § 9 [25] (od 1.3.5), mediana: L = 30 log f − 20 log d + F (θd) − F0 + H0 + Lgp. F (θd) jest funkcją tłumienia określoną w załączniku III w jego formach analitycznych (Ns 301) z korektą dla Ns, H0 funkcja przyrostu częstotliwości w zakresie § 9.2: przy 144 MHz, przy niskich antenkach w długości fal, odbicie od podłoża częściowo usuwa pole w dolnej części wspólnej objętości i sól strat 3-10 dB. Ns jest to, że z profili GFS godziny, skuteczne horyzonty i wysokości pochodzą z profilu Copernicus, Lgp = 0,07 · e ^ (0,055 (Gt + Gr)) jest utrata sprzęgu otwarcia. Jest to taka sama forma, jaką ITM używa w swoim wnętrzu, ale z reaktywnością dnia dzisiejszego, a nie klimatu. Testy na odcinkach płaskich przy częstotliwości 144 MHz przy częstotliwości 20 m: w odległości 2- 4 dB od ITM od 150 do 900 km oraz około 2 dB w uproszczonej formie 9.2b TN101, która nie posiada H0.

- ITU- R P.617- 5 [17], mediana: L = F + 22 log f + 35 log θ + 17 log d + Lc, with f in MHz, θ in mrad, d in km, F = 0,18 · N0 · e ^ (− hs / 7,35) − 0,23 · ΔN i Lc = 0,07 · e ^ (0,055 (Gt + Gr)). N0 i ΔN są zazwyczaj pobierane z map klimatycznych ITU; tutaj używamy co godzinę profile GFS ( N0 · e ^ (− hs / 7,35) jest reaktywność do podłoża, ΔN spadek w pierwszym km), więc również troposcatter następuje po czasie.
- RSGB VHF / UHF Handbook Formula, DL2FZN [18]: L = Lfs + 24,44 + 4,283 ln f + 0,06656 d. Nachylenie z odległością jest prawie równe nachyleniu PROPBER (0,067 dB / km).

Powszechna objętość (z 1.3.1): jest punktem, w którym promień obu stacji jest skrzyżowany, z których każdy jest nachylony do kąta horyzontu, na Ziemi o równoważnym promieniu ae = k · 6371 km. Z TX promień wzrasta jako z = hTX + x · θt + x2 / (2 · ae); Odległość x = (hRX − hTX + d · θr + d2 / (2 · ae) / θ, z θ = d / ae + θt + θr. Z płaskimi horyzontami i antenami w tej samej wysokości podstawa jest warta d2 / (8 · ae): około 1,3 km do 300 km, 3,7 km do 500 km, 9,4 km do 800 km. Im większa objętość, tym więcej powietrza jest suche i rarefied i mniej rozprzestrzenia się: z tego powodu ponad 800- 1000 km troposcatter kończy. W wynikach znajduje się udział i położenie podstawy, reaktywność, gradient i wilgotność GFS na tej wysokości oraz równoważna odległość Bray (QST, listopad 1961): każdy dodatkowy lub mniejszy horyzont jest wart około 111 km. Reference: the presentation "Over the horizon in VHF" by IU3QEZ (GEKO Magazine n. 69).

Linia Troposkatter, inne modele pokazują dwie straty i niepewność jako połowę różnicy między najbardziej optymistycznym modelem a najbardziej pesymistą. Są to tylko porównania: strata bez tropu pozostaje równa P.452.

## 8 ter. popołudniowy troposcater nad reliefs (z 1.3.4)

IK3XTV, z Thiene ( JN55, z Alpami na 10 km), pracuje w letnie popołudnia południowych Niemiec na 400- 500 km, bez wzroku, z silnymi sygnałami, ale z szybką i głęboką evanescence; autor przypisał je do "komórek konwektywnych połączonych jako pociąg soczewek". Obserwacja jest potwierdzona; wyjaśnienie przyjęte w programie jest inny, fizycznie jędrniejszy.

Po południu słońce pobudza powietrze nad reliefami, a warstwa graniczna podnosi się do 2-3 km. Na górze wilgotne powietrze poniżej spełnia suchy powietrze powyżej: tam turbulencje wskaźnika refrakcji (rozmiar, który reguluje troposcatter) jest maksymalny. Kiedy szczyt osiągnie wspólną objętość trasy, która na 400- 500 km jest około 2-3 km, troposcatter jest wzmocniony. Wyjaśnia to czas popołudniowy, wysokie ciśnienie (cichy cylindryczny, silne ogrzewanie) i szybki wydostatek, typowy dla dyfuzji, a nie kanałów. Komórki skuteczne nie działają jak soczewki: produkują turbulencje, a to turbulencje na górze warstwy przekształconej rozprzestrzeniają sygnał.

Konsekwencje dla prognoz odczytu:

- Na ziemi pierwsze popołudnie jest najgorszy czas dla niskich kanałów i warstw, ale nie dla troposcattera za górami: dwie rzeczy powinny być czytane oddzielnie;
- program posiada już dwa dane, które służy, wysokość warstwy granicznej dostarczonej przez model i udział wspólnej objętości trasy (wspólnej objętości) w wynikach): kiedy są one blisko, popołudniowa trasa górska może pójść lepiej niż obliczenia w normalnej atmosferze;
- od 1.3.4 przy obliczaniu punktu punktowego bierze się pod uwagę: jeżeli wspólna objętość znajduje się na ziemi, wzdłuż drogi znajdują się reliefy o długości co najmniej 800 m, słońce o temperaturze powyżej 15 °, a warstwa graniczna przekracza 800 m, a górna część warstwy przetapiania (wzorzec szybkości gleby + wysokość warstwy granicznej) znajduje się w odległości 500 m od podstawy wspólnej objętości, troposcatter zyskuje 3 dB; między 500 a 1000 m przyrost liniowy spada do zera. Linia "Troposcatter po południu". Przy śniegu wzdłuż szlaku (co najmniej 10 cm na ziemi według modelu, zmienna głębokości śniegu _ Open- Meteo lub SNOD GFS z NOMADS) zbrojenie nie ma zastosowania: na śniegu gleba pozostaje zimna, a warstwa przerośnięta nie rośnie. Wartość 3 dB jest tymczasowa i musi być skalibrowana rzeczywistymi linkami; wyłącza się z korektami pogody.

Źródła: obserwacje F. Egano IK3XTV (Radio Rivista e www.qsl.net / ik3xtv, "Propagazione sulle 2 metri", "Celle convettive", "Troposkatter over the Alps"), źródła § 13.

## 9. Warstwa założycielska wzdłuż trasy

To serce obliczeń sygnałów. Na trasie, profile GFS są podejmowane co około 25 km. Dla każdej warstwy ciśnienia, najdłuższy odcinek, w którym ta sama warstwa występuje w kolejnych punktach; liczyć jako rury tylko grube warstwy co najmniej 40 m [10].

- ciągłość: od jakiego km do jakiego km i ułamek trasy;
- wierzchołek i podstawa oraz nachylenie w m / km;
- gleba wewnątrz warstwy: km, w którym góry wznoszą się powyżej podstawy;
- Obszar kryty: na morzu przewód jest prawie zawsze, decyduje o rozciągnięciu ziemi [10]. Jeżeli grunt przekracza 10% trasy, użyta ułamek stanowi minimum między ciągłością a zasięgiem ziemi.

Zysk tropu jest przyrostem warstwy dla używanej frakcji. Jeśli więcej niż połowa profili jest sub- refraktywna, przyrost jest ujemny.

## 9 bis. Korekty pogody: stabilność, subsidence, strajk

Od 1.3.0 profil daje również trzy informacje meteorologiczne, które można zobaczyć w rejestrze (regave "stabilność (Misme)"), w linii Meteo wzdłuż trasy wyników i, z Segni tip box, na mapie.

- Stabilność Misme [5, rozdział 6]. Misme łączy pole poza horyzontem z parametrem złożonym: M = a · (ge -40 + b · ΔW), z a = 0,5 dB na N / km, równoważnym gradientem i stabilnością termiczną ΔW, pracą na podnoszenie powietrza o 1 km. Program oblicza ΔW = g · ∞ (θ − θ0) / θ0 oś między glebą a 1 km powyżej, z θ potencjalną temperaturą; standardowa atmosfera jest warta około 56 J / kg. Termin dodany do przyrostu wynosi 0,07 dB dla J / kg poza standardem, ograniczony między -4 a + 6 dB, i liczy się tylko wtedy, gdy żaden kanał nie zatrzymuje pasma (gradient jest już w indeksie Bean i Dutton). Kalibracja jest tymczasowa: musi być zweryfikowana przy zastosowaniu współczynników (pkt 14).
- Uległość i wzniesienie powietrza [19]. Moler i Holden, na trasach 125- 305 km do morza przy 1365 i 9380 MHz, znaleźli najwyższy sygnał średnio około 2,5 dB z powietrzem spadającym w dół z powietrzem rosnącym, a w przypadku wzmocnienia około 20 dB na 3 godziny na początku subsidencji, a następnie spadek o 22 dB, gdy powietrze powróciło do konwergencji. GFS daje prędkość pionową 850 i 700 hPa. Jeżeli na obu poziomach powietrze spada o więcej niż 1 cm / s i występuje rewers lub ostry spadek potencjalnego wskaźnika refrakcji K (N zgłaszane adiabetycznie do 1000 hPa, co podkreśla suche i ciepłe powietrze spadło z górnej [5]), dodaje się 1,5 dB; jeśli powietrze wzrasta o więcej niż 5 cm / s, startują 2.
- [20] Scirocco. Gorące i suche powietrze z Sahary nad chłodniejszym morskim powietrzem daje bardzo silne inwersji ze wschodniego Morza Śródziemnego do Gibraltaru. Znak S pojawia się, gdy przy 850 hPa wiatr pochodzi od 100- 200 ° z co najmniej 7 m / s i temperatura wynosi co najmniej 16 ° C, na Morzu Śródziemnym. To tylko wskazówka: zysk decyduje o warstwach profilu.

Bez przewodu, który zatrzymuje pasmo, całkowity przyrost nie przekracza 16 dB, koniec żółtego: poprawki czynią silniejsze warstwy bardziej widoczne, ale nie przechodzą przez kanały (czerwony i fioletowy pozostają na kanałach).

- Trwałość (z 1.3.1). Największe otwory są po dobrych dni wysokiego ciśnienia (VHF / UHF DX Book; Biancomano i Leyshon 1998, zasada 12 godzin; raporty z 29 / 12 / 2021-2 / 1 / 2022 rosną od pierwszego do czwartego dnia). Dla każdej komórki program liczy godziny poprzednich 24 z zyskiem wynoszącym co najmniej 13 dB i dodaje do proporcji do 2 dB, tylko wtedy, gdy zysk wynosi już co najmniej 10 dB.

Jak odczytać znaki na mapie (Zmień znaki pogody; z 1.3.2 symbol jest duży podwójny i przechodząc z myszką nad znakiem pojawia się komiks z wyjaśnieniem i wartościami komórki):

| Podpisz | Kolor | Co to znaczy? | Wpływ na trop |
|---|---|---|---|
|  ↓  | zielone | subsidence: krople powietrza (antycykliny), podgrzewa do kompresji i suchy, wzmacnia wsteczny i utrzymuje go mocno | korzystne, + 1,5 dB |
|  ↑  | czerwony | powietrze podwieszane (przednie, niskociśnieniowe, konwekcyjne): miesza niskie warstwy i przerywa inwersje i kanały | niekorzystne, -2 dB |
|  S  | pomarańczowe | Syrop: gorące i suche powietrze z Sahary nad świeżym powietrzem morskim | korzystna wskazówka, bez korekty: zdecydować warstw |

Obszar pełen zieleni ↓ pod antycykliną jest klasycznym znakiem twardego otwarcia; Red ↑ strzałki wzdłuż linii mówią, że warstwy się łamią, chociaż kolor komórki jest nadal dobry.

Skrzynka Korekcji Pogoda wyłącza te poprawki, aby zobaczyć model 1.2.

Weryfikacja (pkt 14): na 593 połączeniach do 144 MHz w okresie od dnia 29 grudnia 2021 r. do dnia 2 stycznia 2022 r., w porównaniu z tymi samymi trasami i dwa tygodnie później, wskaźnik separacji (0,5 = none, 1 = perfect) wynosi 0,79 bez korekt i 0,91 z korektami (przed dachem przy 16 dB). To samo porównanie pokazuje główną granicę: kanał na morzu na niskiej wysokości, który w tamtych czasach przyniósł znaki między Kampania i Liguria, nie jest widoczny na poziomie GFS.

## 9 ter. Aktualności 1.3.2: dwa szacunki, sufit, wysokie ciśnienie, morze, stacje, grzbiety

Rozsądna i agresywna ocena. Zysk z gradientu rośnie od 0 dB do granicy normy do 10 dB do progu nadrefrakcji. W warstwie 1000- 850 progiem ostrożnym jest Bean i Dutton (100 N / km); Agresywny próg jest klasycznym progiem superrefrakcji 79 N / km (k Ř2; P.453 wykorzystuje -100 N / km w ciągu pierwszych 100 m), który przy k50 = 157 / (157 − ΔN) wynosi k Ř2 (k = 2,5 odpowiada około 94 N / km). Obliczenie punktu punktowego pokazuje dwie straty otoczone; z przewodem, który uwięził gang zbiega. Mapa używa ostrożności.

Poddasze o wysokości 2500 m. Drogi i kanały są poszukiwane tylko do 2500 m nad ziemią: im wyższe anteny radiotelefonów amatorów nie wykorzystują ich. Przy 1500 m walidacja straciła punkt separacji i 4% połączeń powyżej 16 dB, ponieważ w zimie odchylenia od podłoża wynoszą często od 1500 do 2500 m.

Wysokie ciśnienie. + 1 dB, gdzie co najmniej 7 z 9 komórek wokół zmniejszyło ciśnienie do morza 1020 hPa lub więcej, a wiatr jest słaby w niskich warstwach (10 m poniżej 5 m / s, 850 hPa poniżej 8 m / s), z wyjątkiem wznoszących się ruchów.

Waga morza. W obliczeniach punktowo-punktowych przyrost warstw jest mnożony przez 0,8 + 0,4 · μ1, gdzie μ1 jest czynnikiem ITU- R P.452 (eq. 51) na dłuższym ciągłym odcinku lądu: 1 na otwartym morzu, małym z długimi odcinkami lądu.

Stacje i prowadzone. Gdy przewód, który zatrzymuje zespół, pokrywa tylko część linii, udział każdej anteny jest porównywany z bazą i wierzchołkiem przewodu na początku i na końcu sekcji pokrytej; przyrost jest mnożony przez 0,5 + 0,5 · hm (e ^ (− ΔhTX / 300) · e ^ (− ΔhRX / 300), przy Δh odległości w metrach od kanału. Z przewodem na prawie całej trasie jest połączenie z § 10.

Warstwa i grzbiety. Jeżeli górna warstwa graniczna GFS (wskaźnik glebowy modelu najbardziej granicznego _ warstwa _ wysokość) przekracza szczeliny trasy (maksymalna powierzchnia większa niż 300 m, jeśli najwyższa przekracza 500 m), warstwa przechodzi przez góry bez zerwania: + 1,5 dB dla ułamka pokrytych grzbietów. To jest obserwacja IT9GSF na otwarciu od Sycylii do Niemiec z korzystne Tyrrenian.

Poziomy blisko ziemi na morzu. Dzięki źródłowi NOMADS program pobiera również temperaturę powierzchni (na morzu jest SST), temperaturę i wilgotność na 80 m i w warstwie 30- 0 hPa nad podłożem. Na punktach morskich (model poniżej 5 m) poziomy te są dodawane do profilu, pod pierwszym poziomem ciśnienia, aby zobaczyć kanał na 100- 200 m, że poziomy ciśnienia nie rozwiązują. Na ziemi nie są używane: warstwy nocne i dzienne nie są jeszcze ważne. Open- Meteo daje tylko temperaturę powierzchni.

Gazociąg parowania. Od SST, od temperatury i wilgotności do 2 m szacowana jest wysokość γ przewodu parowania z neutralnym profilem logarytmicznym (jak w modelu Paulus i Jeske, bez korekty stabilności): powietrze do włosów wody jest nasycone do SST (98% dla soli), dlatego

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

ograniczona od 0 do 40 m. Aby złapać 144 MHz potrzeba około 180 m (Kerr), a następnie przy 144 MHz kanał parowania wzmacnia niewiele; od 432 MHz do góry, z niskimi antenami na morzu, wiele (pkt 11 bis). Wprowadź równanie paraboliczne i w wierszach Poprawki 1.3.2.

Czas dnia (wizyta). Program oblicza wysokość słońca w każdym punkcie. Od dnia do dnia na ziemi, ze słońcem powyżej 15 ° i warstwą graniczną powyżej 800 m lub wiatrem powyżej 5 m / s, ostrzega, że kanały glebowe są ponownie ukształtowane; w nocy, ze słońcem poniżej -3 °, wiatrem poniżej 3 m / s i warstwą graniczną poniżej 300 m, ostrzega, że z spokojnym niebem jest możliwe prowadzenie przez napromienianie, które spada po wschodzie słońca. Nie na morzu. Na razie są one zauważone, nie numery: Grudnia walidacja nie ma wystarczająco dni letnich, aby je skalibrować. Są to dwa punkty, które autor uważa za ważne, z codziennej obserwacji.

Profil normalny CRPL (alternatywny). Zamiast 40 N / km stały normalny spadek następuje wykładniczej atmosfery odniesienia CRPL: N (h) = Ns · e ^ (-c · h), z -ΔN w pierwszym km = 7,32 · e ^ (0,005577 · Ns) (Bean i Thayer 1959). Z mokrym powietrzem (Ns wysoki) normalny jest bardziej stromy i służy mocniejszy gradient dać zysk; na wysokości jest słodszy. Okazuje się, że źle, dopóki nie potwierdzi, że to pomaga.

## 9 kwater. Kontrast powietrza-morze i niskie morza: Adriatyk (z 1.3.3)

Fale radiowe nie czują dna morskiego: czują kontrast między powietrzem a powierzchnią wody. Głębokość się liczy, ponieważ decyduje jak szybko temperatura morza podąża za powietrzem.

Adriatyk to niskie morze. Średnia głębokość wynosi około 250 m, ale północna dorzecza rzadko przekracza 100 m, a Zatoka Triesta pozostaje poniżej 25 m; tylko na południe schodzi ponad 1200 m. Warstwa wody tak cienki ogrzewa i ochładza w kilka dni, Tyrrhenian w tygodniach. Istnieją trzy typowe sytuacje:

1. Wiosna i wczesne lato, korzystne. Morze nadal jest zimne, nawet dla słodkiej wody rzek alpejskich i po; dociera ono do gorącego i suchego powietrza ze równiny lub Bałkanów. Gorące powietrze nad zimną wodą stanowi odwrócenie na morzu (rurociąg): jest to klasyczny warunek otwarcia między Włochami, Słowenią i Chorwacją.
2. Jesień i zima z bora, niekorzystny. Zimne powietrze nad cieplejszym morzem: niska warstwa staje się niestabilna, miesza się i pęka kanał.
3. Po bora, szansa. Tylko dlatego, że morze jest niskie, Bora ochładza go trochę stopni w ciągu kilku dni. Jeśli wprowadzisz antycyklinę z gorącym powietrzem, kontrast powietrza-morze jest silny i kanał jest bardziej wytrzymały niż zwykle.

Adriatyk jest również zamknięty kanał między Apennines i Dinari, mniej niż 200 km szerokości: warstwa morska pozostaje ograniczona, a szlaki wzdłuż osi (Puglia- Venezia-Trieste, około 700 km) pozostają wszystkie na morzu, najlepszy warunek dla długiego kanału.

Co program oblicza. Na morzach, z korektami pogody i pole "Contrasto ariamare":

| Stan | Korekta |
| --- | --- |
| potencjalna temperatura przy 925 hPa (lub 950) nad SST 4 K | _ |
| poza SST 7 K | + 2 dB |
| z wilgotnym powietrzem przy 925 hPa (ponad 80%) | - 0,5 dB na te premie |
| chłodzone morze 1,5 ° C lub więcej w 72 godziny przed, z korzystnym kontrastem | _ |
| cieplejsze morze powietrza w 2 m 2 ° C (mieszanie) | − 1,5 dB |
| cieplejsze morze 4 ° C | − 3 dB |

Całkowita korekcja pozostaje między -3 a + 3 dB, a bez przewodu zysk pozostaje pod żółtym dachem. Progi są tymczasowe: w atmosferze standardowej potencjalna temperatura w 925 hPa przekracza temperaturę morza około 2-3 K, następnie 4 K już wskazuje odwrotność.

Pamięć SST. Modele utrzymują temperaturę morza prawie stabilną podczas przewidywania. Ostatni spadek wynika z analizy trzech dni przed (Open- Meteo, tylko temperatura powierzchni, tylko komórki morskie): maksymalnie 72 godziny przed minus wartości bieżącej. Jeśli usługa nie odpowiada, bonus pamięci nie ma zastosowania.

Granice. SST modeli poniżej wybrzeża i w górnym Adriatyku jest gruboziarnisty; na przykład zimne pióro po, jest źle rozwiązane. Batemetria nie zawiera obliczeń: źródła zawsze dają SST, który już podsumowuje efekt dna morskiego. Kontrast powietrza-morze pokrywa się częściowo na końcu stabilności Misme (§ 9 bis); w tym celu premie są małe. Progi będą taranowane z korytarzami reklam i QSO przez Adriatyk.

## 10. Przeprowadzona na całej trasie

Jeżeli warstwa zatrzaska pasmo na co najmniej 80% używanej frakcji, a stacje nie są w zasięgu wzroku, to strata w przewodzie jest również obliczana: wolna przestrzeń plus sprzężenie anten (3 dB jeśli są wewnątrz przewodu, więcej jeśli są one powyżej lub poniżej), plus pozostałość tłumienia γd = 5 · 10 − 5 · ae · f ^ (1 / 3) dB / mrad ITU- R P.452 eq. 49 [6]. Jeśli jest mniejsza niż strata bez tropu staje się oczekiwaną stratą.

Pasek pozostaje w przewodzie tylko wtedy, gdy jego długość fali nie przekracza λmax = 2 Ö 2 · (2 / 3) · ΔM · 10 − 6) · d (Kerr, eq. 17 [11]). W tym celu ta sama warstwa może złapać 23 cm i 70 cm, ale nie 2 m. Jednak limit nie jest netto: Kerr ostrzega, że nawet fale kilka razy dłużej cierpią wiele przewodu, a Hepburn i VHF / UHF DX Książka mówi o "wypryskiwanych" krawędzi i "przebaczyć" kanałów. Od 1.3.1 program używa miękkiego przewodu: jeśli λmax jest pomiędzy λmax a 3 · λmax, przyrost stopniowo przechodzi od 15 dB (λ = 3 · λmax) do pełnego przewodu (λ = λmax), przy s = (λmax / λ − 1 / 3) / (2 / 3). W takim przypadku dopuszczalne są kolory kanałów. Logarytm i linia Meteo wzdłuż trasy wskazują punkty z miękkim kanałem.

## 10 bis. Via Monte

Gdy korespondent jest zamknięty za horyzontem, program szuka najlepszego kierunku, aby wskazać anteny: podświetlone nachylenie rozszerza sygnał jako cel radarowy. Przeprowadza horyzont co 5. (z wyjątkiem ± 15 ° wokół korespondenta) i w każdym kierunku zajmuje podświetlony punkt horyzontu, powyżej 100 m i między 1 a 60 km. Szybkie oszacowanie (wolna przestrzeń, konwersja bitaktyczna i 10 dB dla każdego poziomu horyzontu od dyfuzora do korespondenta) wybiera 4 najbardziej obiecujące, co najmniej 15 ° od siebie; dla każdego:

- traktować 1, stacja → ulgi, w wolnej przestrzeni;
- traktuje 2, relief → korespondent, obliczony tak, jakby ulga przekazywana od 10 m powyżej góry, z tą samą prognozą (bez równania parabolicznego);
- konwersja za pomocą równania radarowego bistatycznego, przy wklęsłej szerokości fali, z ułamkiem: R1

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

Linia "Recommended Point" wskazuje azymut najlepszej trasy, jeśli kosztuje mniej niż bezpośrednio. Przy 144 MHz, z ulgą na 20 km i 15 ° yagi, droga górska kosztuje około 45 dB więcej niż tylko 2 trasy: dzieje się tylko, gdy bezpośredni jest zamknięty przez sąsiadujące przeszkody. Szacunkowa wartość wynosi brutto (± 10 dB), szczególnie dla σ0.


Bez strat w górach (opcja, z 1.3.2). Z "Via monte senza perdite fino al monte" box, upadek między stacją a punktem dyfrakcji jest uważany za null: jest jak wprowadzenie stacji na szczyt (antena do 10 m nad punktem, ta sama moc, antena i kabel). Następnie okrąg horyzontu wybiera punkt z najbardziej wolnym horyzontem w kierunku korespondenta (przy parytecie, najwyższym), a wynik pokazuje trasę od punktu do korespondenta, z sygnałem i S / N, obok sumy z dyfuzją. Jest to optymistyczny limit: mówi, jeśli kierunek otwiera się, gdy góra podnosi sygnał dobrze, lub gdzie przejść przenośne. Każdy punkt jest podany z ten- znak.

## 10 ter. Wejście do odległego kanału

Jeśli kanał, który uwięził zespół zaczyna się daleko, poza horyzontem stacji, sygnał musi dotrzeć. Program wprowadza sygnał do punktu wyjścia, w środku przewodu, do dyfrakcji: wzdłuż profilu Kopernika GLO- 30 przeszukuje krawędź z najwyższym parametrem Fresnel (ITU- R P.526), z obrzękiem równoważnej ziemi,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

więcej tłumienia z powodu krzywizny górnej (zaokrąglona przeszkoda, P.526- 16 § 4.2), z promieniem R szacowanym od gleby Copernicus GLO- 30 do ± 1 km od krawędzi,

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

(dla m · n > 4 jest formą 34b zalecenia). Barsis (NBS, 1963) mierzony na trzech długich trasach nad górą (Kolorado, Alaska, Europa) średnie straty 10- 20 dB wyższe od idealnej krawędzi Fresnel- Kirchhoff: zaokrąglanie wyjaśnia wiele (Barsis i Kirby, 1961: Pikes Peak o promieniu około 11,5 km). Wynik porównuje się z dyfrakcją na gładkiej sferycznej glebie; jest ona najgorsza. Następnie sygnał porusza się w kanale: wolna przestrzeń na całej trasie, pozostałości tłumienia γd z P.452 na sekcji w kanale i sprzęganie korespondenta (3 dB wewnątrz, 1 dB co 10 m na zewnątrz). Jeżeli strata ta jest niższa od oczekiwanej straty, staje się ona mechanizmem ("zachowanie (wejście przez dyfrakcję)"). Jest to założenie autora na temat włoskiej geografii: z pobliskiej przeszkody znacznie poniżej linii, v jest ujemne, a wejście kosztuje niewiele; z grzbietowych kosztów frontu 15- 30 dB.

## 10 kwater. ITM (Longley- Rice) i wyjście z kanału

Od 1.3.2 program zawiera nieregularny model Terrain 1.2.2 NTIA / ITS (Longley and Rice, ESSA ERL 79- ITS 67, 1968; Hufford, Longley i Kissick, NTIA 82- 100, 1982), przetłumaczony na C # przez oficjalny kod rządu USA, który jest wolny od praw. Tłumaczenie daje te same numery co oryginał: auto compilation porównuje je za każdym razem z kodem NTIA C + + w pięciu testach (poniżej 0,01 dB).

ITM oblicza straty w normalnej atmosferze na rzeczywistym profilu Copernicus GLO- 30: postrzegane z odbicia od ziemi, dyfrakcji (spigol i sferyczny grunt ważony na chropowatości gleby Δh, to jest różnica między 10. i 90 ° percentyl kwot wokół prostej z minimalnych kwadratów) i troposcatter. Reaktywność gleby jest zmniejszona do średniej części trasy, $N_s = N_0\,e^{-h/9460}$, i daje krzywiznę równoważnego terenu,

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

Program wykorzystuje klimat radiowy wybranej trasy, jak w § 10 sexies (aż 1.3.4 zawsze umiarkowane morze, z N0 320 na lądzie i 350 na morzu), z N0 od średniej reaktywności miejsca (gleba εr 15, RR0,005 S / m; morze εr 81, RR5 S / m), polaryzacja pozioma i zmienność w czasie bez tego miejsca (profil jest znany). Pokazuje to straty przekroczone w 50% czasu, a jeden masz w 10% i 1% najlepszego czasu. ITM nie modeluje kanałów: jego zmienność w czasie jest statystyczna, według klimatu. Służy jako drugie obliczenie poziomu odniesienia obok P.452; różnica między nimi daje pojęcie niepewności.

Drugim zastosowaniem jest ostatni odcinek po przewodu, który kończy się przed odpowiednim. W punkcie wyjścia, wirtualna antena jest umieszczona w połowie drogi przez przewód, a ITM oblicza przewód do odpowiedniego, jako przekroczenie wolnej przestrzeni w 50% czasu. Całkowita strata

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,	heta_{condotto} + A_{ITM}(d_{uscita} 	o RX)
```

gdzie A _ TX jest sprzęgiem anteny (3 dB wewnątrz, 1 dB co 10 m na zewnątrz), jeżeli kanał zaczyna się w pobliżu, w przeciwnym razie dyfrakcja wejścia § 10 ter. Jest to limit, który autor sam sobie dał: ciągłość warstwy plus, co najwyżej, pozostałą tłumienie. Jeżeli jest niższa od oczekiwanej straty, staje się mechanizmem ("zachowanie (wyjście i ostatni odcinek ITM)").

## 10 quinquies. Pogłębienie: wyspy, cieśniny i nieciągłość wód podziemnych

Przykład z Zatoki Neapolitańskiej (Anacapri, JN70CN) w kierunku zachodniej części Morza Śródziemnego pokazuje, gdzie szlaki przebiegają przez wyspy:

| Miejsce przeznaczenia | Azimut | Gdzie przecina wyspy | Przeszkody |
| --- | --- | --- | --- |
| Barcelona |  279°  | na północ od Sardynii (Gallura) do 41,05 ° N, między 420 a 490 km | około 30 km na południe od Boche di Bonifacio; Limbara (1359 m) około 20 km od linii |
| Bocche di Bonifacio |  282°  | wąskie do 41,3 ° N | około 11 km |
| Marsylia |  296°  | Korsyka Środkowa w 42,2 ° N | góry do 2700 m: kanał poniżej 2 km tam zatrzymuje |
| Valencia, Baleary |  260–265°  | Sardynia Środkowa | Reliefy wewnętrzne |

Dla Barcelony wystarczy wskazać 282 ° zamiast 279 °, ponieważ oś przechodzi z ust: 3., wewnątrz belki normalnego yagi.

Czy usta są cieśniną? Dla niskich kanałów, poniżej kilkuset metrów, tak: wyspy są zamurowane, a nad ziemią, w ciągu dnia ogrzewanie łamie inwersji. Sam cieśnina jest wystarczająco szeroka: około 11 km, w stosunku do pierwszego obszaru Fresnel, mniej niż kilometr szerokości, połowa trasy 1000 km w 144 MHz. Problemem jest pogoda. Mouthowie są zabawą dla wiatru: Dolna warstwa jest przyspieszana, a dolna usuwana. Często przejście jest otwarte jako geometria, ale zakłócone jako atmosfera. Przy stałym antycyklinie i małym wietrze może być dobrym przejściem.

Rozbieżność między morzem a Ziemią. Liczą się. Przewód utworzony na wybrzeżu Tyrrenii jest przerwany: sygnał wychodzi z przewodu (sekcja po wyjściu, § 10 quater) i musi powrócić do innego przewodu na morzu Balearów, płacąc ponownie sprzęgu. Przewody na wysokości powyżej 1500 m przechodzą nad Gallurą, nie zdając sobie z tego sprawy; dla Korsyki Środkowej nie ma zastosowania.

W praktyce: jeśli trasa przebiega przez górzystą wyspę, spróbuj trochę poprawić w kierunku odcinka wolnego morza, ale nie liczyć na przejście z silnym wiatrem w wąskim.

## 10 seksów. TN101 klimat radiowy i zmienność czasu (z 1.3.5)

TN101 § 10 [25] opisuje, w jaki sposób utrata trasy poza horyzontem różni się w długim okresie wokół mediany, przy różnych krzywych według klimatu: funkcja Y (q) określa, ile dB spada utrata w q% najlepszego czasu. ITM zawiera te krzywe dla siedmiu klimatów. Klimat liczy się dużo: na morzu w klimacie morskim umiarkowanym, na poziomie 144 MHz, w 10% przypadków jest o wiele więcej dB niż na lądzie w klimacie kontynentalnym, ponieważ kanały są częstsze.

Program nie naprawia już klimatu: Wybiera go na każdą trasę od przeciętnego punktu, więc działa na całym świecie, a nie tylko w Europie. Pobieranie z Open- Meteo [26] trzech lat codziennych wartości reanalizy ERA5 (temperatura, wilgotność i ciśnienie gleby, deszcz) dla komórki 0,5 ° przeciętnego punktu, tylko raz; wartości pozostają w Data / CLIMA. Przywrócenie gleby

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

dzień po dniu, następnie średnia roczna i wycieczki między średnimi miesięcznymi, średnia wilgotność i roczne opady deszczu. Zasady są zgodne z opisami klimatów TN101 § 10.2; granice między jednym klimatem a drugim są kryterium autora:

- ponad 60 ° szerokości geograficznej: umiarkowany kontynentalny ( TN101 nie ma krzywych dla klimatu polarnego);
- mniej niż 250 mm deszczu rocznie i średnia wilgotność poniżej 50%: pustynia (Ns typowy 280);
- w odległości 20 ° od równika, Ns co najmniej 350 i wycieczka do 30: Równikowy (Ns typowy 360);
- w granicach 32 ° i rejs co najmniej 60: subtropikalny kontynentalny, czyli monsuny (typowa wycieczka 60- 100);
- od 10 ° do 32 ° Ns co najmniej 340: podtropikalny statek morski (Ns typowy 370);
- oba poziomy na morzu, powyżej 20 °: umiarkowane morze na morzu (jest to definicja TN101);
- wycieczka do 30 w pobliżu morza (morze na trasie lub średnia wilgotność wynosząca co najmniej 75%): morze hartowane na ziemi;
- inaczej umiarkowany kontynentalny (typowa wycieczka 20- 40).

N0 dla ITM jest średnią obniżoną do poziomu morza, N0 = Ns · e ^ (h / 9460). Bez sieci stosuje się zasadę rezerwy z samą szerokością i ułamkiem morza, a linia tak mówi.

Badania z danymi ERA5 2023- 2025: Capri Ns 339 z wycieczką 45 (kontynentalny temperowany na ziemi, umiarkowany morze, gdy oba horyzonty są na morzu); Londyn 327 i 16 (gwiazda temperowana); Wiedeń 319 i 24 Abidżan 381 i 11 (równikowy); Miami 365 i 36 (subtropikalny mąż); Delhi 337 i 82 (subtropikalny continental); Riad 278 i 37, 90 mm deszcz (pustynia). W Chile: Antofagasta 341 i 19 (subtropikalny mąż: wybrzeże Atacama, z prawie stałym niskim rewersem Pacyfiku), Calama 232 na ziemi, 2257 m, 14 mm deszczu (pustynia), Valparaíso 331 i 11 (szew temperatury), Punta Arenas 313 i 5 (szew temperatury).

Linia "Radio climate ( TN101)" pokazuje klimat, powód, Ns z połową wycieczki i Y: ile dBs zarabiają w 10% i 1% czasu i ile tracą w 90%. Jest to statystyka długoterminowa i pozostaje oddzielona od prognozy: oczekiwana strata następuje po chwili obecnej z profilami GFS, a suma tych dwóch rzeczy policzyłaby się dwukrotnie tak samo nieprawidłowego rozmnażania.

## 11. Porównanie z ITU- R P.452

Linia porównawcza P.452 stosuje metodę ITU dla kanałów i odbicia na warstwach (P.452- 16 § 4.5 [6]) z medianą ΔN GFS zamiast klimatologicznego i p = 20% jako stałą kalibracyjną, jak w badaniu CSIRO [10]. Obejmuje to utratę sprzężenia empirycznego Alf poniżej 0,5 GHz (około 28 dB do 144 MHz Jest to tylko porównanie: oczekiwana strata pozostaje w rozumieniu pkt 9.

## 11 bis. Równanie paraboliczne (z 1.3.1)

Paraboliczna Linia Równikowa jest czwartym obliczeniem, tylko porównawczym, wykonanym jako profesjonalne programy dla kanałów (PETOOL; ramy propagacji fali M. Lytaev, licencja MIT): zamiast szacowania zysków z warstw, przesuwa falę wzdłuż trasy z metodą Fourier split- step wewnątrz profilu refraktywnego M zmodyfikowanego GFS, pobranego z punktu prognozy bliżej każdej odległości. Poziomy GFS są dodawane do szacowanych skoków potencjalnych kanałów (obliczając A, 50 m w połowie warstwy). Gleba Copernicus GLO- 30 wchodzi w schody (pod ziemią pole jest zerowe), gleba jest przewodnikiem, a belka anteny jest Gaussian z szacowanym otworem od zysku. Obliczenie widzi przewody, tych, którzy przegrywają, dyfrakcję i połączenie anten; On nie widzi troposcatter, który jest dodawany osobno. Następnie jest "czek profilowy": to samo ze standardową atmosferą mniejszą niż ta z oczekiwanym profilem.

Od 1.3.2:

- propagator znajduje się pod szerokim kątem, z dokładną fazą wolnej przestrzeni:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- dno na morzu i na równinie przybrzeżnej na zerowej wysokości odbija się współczynnikiem Fresnela w polaryzacji poziomej, εc = εr − j · 60 · λ · ∞ (morze εr = 81 i ∞ = 5 S / m; kraj 13 i 0,006 S / m), pod kącem prostym trasy:

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- chropowatość morza zmniejsza konsekwentne odbicie według Millera i Browna, przy efektywnej wysokości fal od wiatru do 10 m (σh = 0,0051 · U2):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- na morzu profil M obejmuje kanał parowania (pkt 9 ter) z neutralnym profilem logarytmicznym:

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

Testy z prototypem (10 m anteny na morzu, zyskują na standardowej atmosferze na 100- 300 km): z ∞ = 10-30 m kanał parowania daje 1-3 dB do 144 MHz, 8- 37 dB do 432 MHz i 21- 66 dB do 1296 MHz. Przy 144 MHz w polaryzacji poziomej morze odbija się prawie jak metal (η124; η η124; więcej niż 0,99 do kilku dziesiątych stopni) i szorstkość się nie liczy: nowe dno zmienia kilka dziesiątych dB; liczy się więcej od 432 MHz w górę.

Granice: nad ziemią pozostaje pionowy profil poziomu GFS, więc cienkie kanały pozostają niewidoczne; grunt do stopni jest zbliżony; belka pozostaje w granicach 6.

## 12. anteny, sygnał i hałas

- Yagi DL6WU: przyrost 9,5 + 7,8 · log10 (boom / λ) dBd (2.35 dB dla podwójnego boomu, miary DL6WU dla VHF Communications 3 / 1982) plus 2.15 dla [12] dBi.
- EIRP = power − cable + TX gain.
- Otrzymany sygnał = EIRP -oczekiwana strata + przyrost RX − kabel RX. S- meter IARU: S9 = -93 dBm, 6 dB na jednostkę.
- Hałas zewnętrzny według ITU- R P.372 [13]: bardziej sztuczny w zależności od otoczenia (miasto, mieszkalne, wiejskie, spokojne wiejskie) lub temperatury antenowej.
- Tsys = Ta + Tpre + [290 (Lc − 1) + 290 (Fr − 1) · Lc] / Gpre do terminali anteny (wzór Friis). Od 1.3.5 stacja odbiorcza ma zawsze przedwzmacniacz w antenie, stały parametr: numer hałasu 0.8 dB (Tpre = 59 K) i uzyskuje 20 dB. Za kablem (utrata Lc) i radiem, z podstawowym ks. hałas numer 6 dB. Z 1 dB kabla i wkładu radiowego spada z około 1160 K do 12 K: hałas decyduje o niebie, środowisku i przedwzmacniaczu. Zysk 20 dB jest typową wartością wybraną przez program.
- S / N w SSB, CW i FT8 w paśmie każdej drogi, w porównaniu do ustawionego progu.
- Prawdopodobieństwo dla okresu (z 1.3.2): wyniszczenie w ciągu godziny jest normalnym rozdziałem S / N wokół oczekiwanej średniej wartości godzinowej. Bray (QST, listopad 1961, s. 39, na łukach Norton 1959) daje amplitudę 13,5 dB na około 100 mil, czyli około 7 dB poniżej i 7 dB powyżej średniej, która prawie spada poniżej 100 mil (3,5 dB do 50 mil); dla "stałego" połączenia zajmuje 7 dB z budżetu. Uzyskany ± 7 dB jako ± 2∞ = 3,4 dB przez 160 km, proporcjonalny do odległości poniżej. Prawdopodobieństwo przekroczenia progu drogi

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  z funkcją normalnego rozkładu: margines 0 dB = 50%, + 3,4 dB = 84%, + 7 dB ("stały" margines Bray) = 98%. Nie obejmuje niepewności modelu, który jest większy.
- Drogi cyfrowe: S / N w 2500 Hz w porównaniu z progami mierzonymi przy 50% dekodowaniu, bez uprzednich informacji (AP). FT8 - 21 dB i FT4 - 17,5 dB na szumie białym; gdy mechanizm jest troposcatter używać progów z evanescence "zakłócone medium szerokości geograficznej", FT8 - 18,6 dB i FT4 - 15,2 dB [21]. Q65 - 15C - 22,2 dB, Q65 - 30B - 24,8 dB, Q65 - 60A - 27,6 dB W rozrzucie samolotu Doppler może przekraczać tony Q65 -60A (1,7 Hz): są lepsze niż 30B lub 15C. JT65A − 25 dB [22]. AP dekodujący (nazwa już znanego lub kończącego się QSO) daje około 2 dB ekstra dla FT8 oraz od 1,5 do 2,6 dB dla Q65; nie wchodzi on w próg, ponieważ nie jest ważny dla wszystkich wartości "-24 dB", które są często odczytywane dla FT8, to granica relacji WSJT- X lub dekodowania AP, a nie próg 50%. [24]

## 13. Odpowiedź i czas trwania danych

Każde obliczenie mierzy swój czas: profile GFS (sieć, analiza, obowiązkowe przerwy 20 s pomiędzy partiami dla limitu Open- Meteo), teren Kopernika GLO- 30 (z pobraniem paneli, 15- 30 MB każdy, tylko pierwszy raz), obliczenia. Linia czasu odpowiedzi podsumowuje je; w rejestrze wiersze zaczynają się od TEMPI. Podczas aktualizacji sieci, rejestr informuje również GFS race używane, kiedy stał się dostępny i jak stare są dane.

Od 1.3.2 można również wybrać ICON- 2I, model Agencji ItaliaMeteo i Arpae na Włochy na około 2.2 km, przez Open- Meteo (model italia _ meteo _ arpae _ icon _ 2i, dwa wyścigi dziennie, 3 dni). Obejmuje tylko Włochy i pobliskie morza i ma mniej niski poziom GFS (1000, 925, 850, 700, 500 hPa): dla kanałów na wysokości nie jest lepszy niż GFS, ale drobna siatka opisuje dobrze wybrzeża, wyspy i morze na 2 m, to jest kanał parowania. Zmienne, których model nie posiada (granica wystawienia, temperatura powierzchni...), usuwają się z żądania. Od 1.3.2 źródło automatyczne używa NOMADS; Open- Meteo służy tylko wtedy, gdy NOMADS nie reaguje i w przypadku historycznych przypadków. W polu "Update by self" (oświetlony przez defekt) prognoza jest odblokowana: na otwarciu, na każdym nowym wyścigu GFS na NOMADS (około 5 godzin po czasie wyścigu) i kiedy widoczna strefa wychodzi na więcej niż jedną czwartą z siatki; co najwyżej próba co 15 minut, nigdy w przypadku historycznym.

Wnioski w Open- Meteo mają ograniczenia na minutę, godzinę i dzień dla każdego adresu internetowego. Ściągnięte profile są zapisywane na dysku (Dati / cache- gfs) dla każdego udaru i modelu: z tym samym wyścigu, który zmienia się co 6 godzin, punkty już pobrane nie pytaj ponownie. Z automatycznym źródłem można rozpocząć od NOMADS i Open- Meteo tylko służyć brakujących punktów; z jedynym Open- Meteo, w czasie lub codziennie zatrzymuje się natychmiast i mówi, kiedy spróbować ponownie. Przypadki historyczne nie są buforowane.

## 14. Sprawdzić model

Prognoza powinna być porównana z rzeczywistymi połączeniami. Logarytm obliczeń zatrzymuje każdy numer; archiwum programu zachowuje miejsce i dekodowanie. Dla 432 i 1296 MHz, gdzie spoty są małe, potrzebują raportów od operatorów: którzy wykonali połączenie, do której godziny, z jakim sygnałem. Porównaj przez wiele godzin spodziewany zysk z mierzonym sygnałem mówi, jeśli progi mają być przeniesione.

## Źródła

1. NOAA / NCEP, Globalny System Prognozy (GFS), model globalny na 0,25 °, cztery wyścigi dziennie.
2. Open- Meteo, prediction API (open- meteo.com), gfs _ cheap model, zmienne na poziomach ciśnienia i metadane wyścigowe.
3. Program Copernicus DEM GLO- 30 (ESA; WorldDEM © DLR e.V. 2010- 2014 e © Airbus Defence and Space GmbH 2014- 2018, dostarczany w ramach programu Copernicus przez Unię Europejską i ESA), 1 sekundowy łuk, ramy GeoTIFF w otwartym rejestrze Amazon Web Services. Rezerwa: NASA / USGS Copernicus GLO- 30 in Terrain Tiles "skadi" of AWS.
4. ITU- R P.453, Wskaźnik refrakcji radiowej: jego formuła i data refrakcji, z mapami kanałów cyfrowych (Grupa Badawcza 3, © ITU, użytku osobistego i niekomercyjnego).
5. B. R. Bean, E. J. Dutton, Radio Meteorology, NBS Monograph 92, 1966: Table 6.5 and § 4.4.
6. ITU- R P.452-16, procedury przewidywania dla oceny interferencji między stacjami na powierzchni Ziemi na częstotliwościach powyżej około 0,1 GHz: § 4.2 (dyfrakcja), eq. 45 (troposkatter), § 4.5 i eq. 49 (prowadzona).
7. ITU- R P.834, Wpływ refrakcji troposferycznej na propagację radiową.
8. ITU- R P.525, Obliczanie tłumienia wolnej przestrzeni.
9. ITU- R P.526, Rozpowszechnianie przez dyfrakcję: eq. 31 (cutcheon) i § 3 (ziemia kulista).
10. B. Indermuehle, H. Suzuki, Tropospheric Ducting Prediction based on GFS Model Data at Inyarrimanha Ilgari Bundara, the CSIRO Murchison Radioastronomy Observatory, Publications of the Astronomical Society of Australia, 2026 (arXiv 2608.12861).
11. D. E. Kerr, Rozpowszechnianie krótkofalówek radiowych, MIT Radiation Laboratory Series vol. 13, 1951: eq. 17 e 18.
12. G. Hoch DL6WU, "Extremely Long Yagi Antennas", VHF Communications 3 / 1982, pp. 130 et seq., fig. 1 (zmierzone 432 i 1296 MHz zgodnie z długością boomu).
13. ITU- R P.372, hałas radiowy: galaktyczny i sztuczny hałas dla środowiska.
14. NOAA / NCEP, NOMADS, filtr GRIB czasu GFS 0,25 ° (nomads.ncep.noa.gov); format WMO G RIB2, skomplikowane opakowanie z różnicami przestrzennymi.
15. Open-Meteo, prognoza historyczna API (model prognoza archiwum z około 2021).
16. LukiBox, TropoLink, projekt tras troposcatter z P.617, NBS TN101 i ITM otoczone (GitHub, Apache 2.0 licencji).
17. ITU- R P.617- 5, techniki prognozowania rozprzestrzeniania i dane wymagane do projektowania systemów transhoryzontu radioprzekaźnika, 2019.
18. RSGB, VHF / UHF Handbook; DL2FZN implementation (andi- f / troposkatter on GitHub, 2013).
19. W. F. Moler, D. B. Holden, "Troposferic scatter propagation and contromic circulations", Journal of Research of the NBS 64D, 1960, doi 10.6028 / jres.064d.014: sygnał poza horyzontem (1365 i 9380 MHz, na morzu) i pionowy ruch powietrza.
20. ARRL Handbook 2022 (99a ed.), § 19.4.3- 19.4.4: Troposferical weather and propagation.
21. S. Franke, B. Somerville, J. Taylor, The FT4 and FT8 Communication Protocols, QEX July / August 2020.
22. W3SZ, cyfrowe progi wolnego trybu ( JT65, JT9, QRA64) i wybór trybu zgodnie z propagowaniem, 2019.
23. QST, "The World above 50 MHz", listopad 2022: otwarcie 14- 15 sierpnia 2022 roku pomiędzy Manitobą a Kansas / Missouri.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Quick- Start Guide to Q65, 3 kwietnia 2021 (wsjt.sourceforge.io).
25. P. L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis, Transmission Loss Predictions for Tropospheric Communication Circuits, NBS Technical Note 101, magazine 1967: § 9 (troposcatter, function F (θd), H0), § 10 i załącznik III (radiowy klimat i zmienność w czasie).
26. Open-Meteo, Historyczne API Pogoda (ECMWF ERA5 recenzja od 1940; Copernicus Climate Change Service, CC BY 4.0 licence).
