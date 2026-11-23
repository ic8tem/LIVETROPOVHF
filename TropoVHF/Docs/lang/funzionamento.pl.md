LIVETROPOVHF - dokument techniczny 1
# Działanie programu

> Automatyczne tłumaczenie przez włoski: w przypadku wątpliwości warto włoskiego przewodnika.

Co program robi, skąd bierze dane i jak zmienia je w to, co widać na mapie i na liście. Wersja z dnia 9 listopada 2026 r., związana z projektem C # (.NET 10 + Avalonia).

## Podsumowanie

- Cel: prognoza tropo (Hepburn [3], F5LEN [4], izobare GFS [18]) oraz rzeczywista obserwacja zespołów w tym samym oknie, zgodnie z listą kontrolną LA0BY [1].
- Gromadzenie danych: dwa klastry DX, sieć Reverse Beacon [9], klaster dm4x i przepływ PSKReporter [5] MQTT, dekodowanie UDP z WSJT- X, JTDX i MSHV [8].
- Spot: segmenty pasma, filtr ujemny, rozprysk powietrza w kolorze żółtym, pozycja według lokalizacji lub C ALL3. TXT [6] [7].
- Kto słucha: ciemnoróżowe koła z różowymi liniami zawsze widoczne w kierunku stacji czuł [5].
- Karta punktowa: odległość, azimut, indeks tropo na ścieżce i utrata troposkattera metodą G3YGF [2].
- Języki Hepburn i F5LEN: obraz [3] i ławka kalibracyjna [4].
- Oglądaj: Kontrola NTP na 10 serwerach co 3 minuty [10] [11].
- Podejrzewane raporty: EME w Q65/JT65 rozpoznawany z pozycją Księżyca, okrojone lokalizatory, skimmers, kwarantanna i słuchacze, z zasadami nauczonymi samodzielnie [20].
- Kryteria odrzutów i znaczenie kolorów podsumowanych w dwóch tabelach (sekcje 11 ter i 11 quater).
- Tematy, 36 [17] languages, export documents to Word with edytable formulas [16], portable programme and multiplatform [12] [13] [14] [15].

## 1. Cel

Program składa w jednym oknie dwie rzeczy, które zwykle patrzą na różne strony: przewidywanie propagacji troposfera (indeks Tropo Hepburn i F5LEN, isobare) i obserwacja tego, co naprawdę dzieje się na zespołach (spoty z klastra DX, Reverse Beacon Network i PSKReporter, plus lista stacji, które słuchają). Zakres odniesienia wynosi 144 MHz; spoty zbierają się od 50 MHz.

Ustawienie jest zgodne z listą kontrolną zaproponowaną przez LA0BY dla ekstremalnych zdarzeń tropologicznych: w poprzednich dniach można spojrzeć na prognozy Hepburn i F5LEN i profile temperatury na wysokości, podczas gdy zdarzenie znajduje się na klastrze DX i na miejscu mapuje dowody na to, że otwarcie zaczyna się [1]. LA0BY zauważa również, że tylko mapy Hepburn / F5LEN nie są wystarczające, aby przewidzieć najdłuższe połączenia [1]

## 2. Okno i karty

Okno jest jedno i nie dzieli się na okna mobilne. Od góry do dołu:

- Ustawienia ogólne wiersz: wywołanie i lokalizacja po lewej; po prawej zegar UTC (np. 08 / 11 / 2026 20: 33: 38 UTC, tekst bursztynowy), po którym następuje NTP: OK lub NTP: FAILED, menu tematyczne i język.
- W pięciu grupach: Mapa i miejsce; Prognozy (Propria β, prognoza obliczona przez program i opisana w podręczniku prognozowania, następnie Hepburn i F5LEN); GRAVES; Dane i źródła (klaster DX, PSKReporter, Reverse Beacon, UDP, Mianowania i raporty podejrzane); Przewodniki (Operacja programu, Map Building, Jak kształtować tropo, Ręczna prognoza, Wskazówki dla operatora, z pola wyszukiwania, w którym znajduje się przewodnik tekst). Pod belką pasek pokazuje podjęzyki grupy; każda grupa pamięta ostatni używany.
- Zawartość karty i, na dole, pasek stanu ze stanem źródeł, baza danych nazw i czas ostatniej aktualizacji.

Ustawienia górnej linii odnoszą się do wszystkich zakładek: wybrany motyw również koloruje zbiór danych i tło dokumentów; Strony dokumentu pozostają białe jak arkusz, więc dobrze drukują.

## 3. Telefon i właściciel

Właściciel Maidenhead może mieć 4 lub 6 znaków (np. JN70 lub JN70CN). Program przekształca go na współrzędne środka kwadratu: pole 20 ° × 10 °, kwadrat 2 ° × 1, subkwadrat 5 ′ × 2,5 ′. Jeśli tekst nie jest poprawnym lokalizatorem, krawędź pola staje się czerwona, a stacja nie pojawia się na mapie. Lokalizator służy również jako centrum projekcji azymutu oraz jako punkt wyjścia dla odległości i azymutu na planszy reklamowej.

Call jest nazwą używaną jako logowanie klastrów. Na początku, dopóki nie jest poprawna nominacja, klastry nie łączą się: okno prosi o nazwę (i opcjonalnie właściciela); potwierdzone, nazwa jest kopiowana jako nazwa użytkownika w polu Login każdego klastra, który ma ją pusty i klastry zostawić. Z "Później" pozostają nieruchome, a przycisk Connect każdego klastra wymaga nominacji ponownie. MQTT, UDP i NTP nie używają logowania i nie zostawiają.

## 4. Gromadzenie danych: źródła

Każde źródło posiada własną sekcję w zakładce Zbieranie danych, z Połączonym / Rozłączonym, Rozpoznawane i Połączone pola przy starcie, status, metry i rejestr otrzymanych wierszy. W rejestrze nie pojawiają się nieoznakowane lub wyrzucone linie z filtra: liczy się tylko metry sekcyjne. Sekcje są otoczone przez dwa do dwóch.

| Źródło | Połączenie | Co przynosi |
| 1. Wybór klastra DX | on0dxk.dyndns.org: 8000 zestaw poleceń / filtr dxbm / pass 6 | Spot amatorskich radiotelefonów, także SSB i CW. Na ON0DXK VHF są filtrowane i są włączone z wskazanym poleceniem (poi pass 4, 2...). |
| 2. Drugie źródło | dxopscom: 7300 (DXMAPS, EA6VQ) i s50clx.si: 41112 ( S50CLX, Słowenia) | DXMAPS zbiera plamy VHF, a także daje RBN spoty skimmer (zestaw / kontrola skimmer, już ustawiony); S50CLX jest klaster DX Spider. Zaloguj się swoim nazwiskiem; edytuj adresy. |
| 3. Reverse Beacon Network | telnet.reversebeacon.net 7000 CW / RTTY · 7001 FT8 | Automatyczne miejsce do ściągania. Zaloguj się z nazwą tylko [9]. |
| 4. Klaster z PSKReporter | dm4x.ddns.net: 8500 | Cyfrowe spoty PSKReporter w formacie klastra. |
| 5. PSKReporter MQTT | mqtt.pskreporter.info 1883 · 1884 z TLS | Każdy raport z odbioru w czasie rzeczywistym: kto transmituje, kto słucha i jego lokalizatory [5]. Zespoły 144, 222 i 432 MHz. |
| 6. Mapy pogody i tropo | dxinfocentre.com / tropo _ eur.html tropo.f5len.org / foperasts- for- European model GFS | Hepburn [3] i F5LEN [4] mapy indeksowe tropo, ciśnienie na poziomie morza dla izobare (GFS poprzez Open- Meteo [18]). Hepburn i F5LEN muszą być dopuszczone do autorów. |

Telnet klastrowy (źródła 1- 4). Po połączeniu program czeka na żądanie nazwy (tekst zawierający logowanie, wywołanie:, sygnał wywoławczy lub połączenie); jeśli nie dotrze w ciągu 4 sekund, prosimy o wysłanie logowania. Następnie wyślij polecenia sekcji, oddzielone od punktu i przecinka, co 0,6 sekundy. Znaki sterowania telnet są usuwane przed odczytaniem linii. Jeśli połączenie spada i Riconnetti jest aktywny, spróbuj ponownie po 10 sekundach (15 jeśli serwer w ogóle nie odpowiada) [6].

PSKReporter MQTT (źródło 5). Program zapisuje się do tematu pskr / filter / v2 / {banda} / {modo} / {tx _ call} / {rx _ call} / {tx _ grid} / {rx _ grid} / {tx _ dxcc} / {rx _ dxcc} naprawianie tylko zespołu i pozostawianie innych pól za darmo z znakiem + [5]. Dokładna nazwa, z którą serwer wskazuje taśmy (na przykład 2m, 1,25m, 70cm) musi być sprawdzona przy pierwszym połączeniu.

## 5. Od linii klastra do miejsca

Jedna linia jest miejscem, gdy ma formę DX de SPOTTER: częstotliwość DX komentarz hhmmZ. Program wywodzi się ze spotter, stacji DX, częstotliwości w kHz, komentarz, a teraz UTC; nazwy usuwają przyrostek skimmers (- #, -1...). Następnie stosować w tej kolejności następujące zasady [6].

| Zakres | Segment przyjęty |
| GRAVES (144) | 143,040 - 143,060 MHz |
| 144 MHz | 144 - 148 MHz |
| 222 MHz | 220 - 225 MHz (tylko region 2 ITU, Ameryka) |
| 432 MHz | 420 - 450 MHz |
| 1296 MHz | 1240 - 1300 MHz |
| 2320 MHz | 2300 - 2450 MHz |
| 10 GHz | 10,5 GHz |

1. Zespół. Częstotliwość musi być podzielona na jeden z zarządzanych segmentów; cała reszta, a w szczególności każda komercja poniżej 49 990,0 kHz, nie jest pokazywana i liczy się jako "poza pasmem".
2. Tak. Jednym z najbardziej znanych sposobów komentowania jest FT8, FT4, Q65, JT65, ISCAT, CW, SSB, FM, RTTY, PSK31, MSK144, FSK441, JT6M); USB i LSB stają się SSB.
3. Negatywny filtr. Plamka zostaje odrzucona, jeśli zawiera tryby MSK144, FSK441, JT6M, JTMS, ISCAT lub znaczniki < AU > < AUE > < ES > < MS > < FAI > < F2 >, z nienuklearnymi uppercases, a nawet spacjami w nawiasach. Są to sposoby i propagacje, które nie są tropiczne: Rozproszyć meteor, zorzę postrzałową i sporadycznie, F2.
4. Rozprysk powietrza. Jeżeli komentarz zawiera < AS > lub < ACS > (również zmieszane, np. < As >) lub deklaruje miejsce jako rozproszenie powietrza (rozproszenie powietrza, rozproszenie powietrza, rozproszenie samolotu), miejsce pozostaje, ale jest rysowane na żółto z linią kropkowaną (ciemny poślizg, aby pozostać czytelnym na czystej mapie) i zaznaczone na liście.
5. Lokalizacja stacji. Jeżeli w komentarzu znajdują się dwa lokalizatory (np. JN54 < JN83), to pierwsze z nich to osoby, które zajmują miejsce i drugie miejsce stacji DX; z jednym lokalizatorem jest to stacja DX. To, czego brakuje, jest przeszukiwane w C ALL3. TXT, tylko z dokładną nazwą (dla laptopów patrz poniżej). Jeśli stacja pozostaje bez pozycji, miejsce pojawia się na liście z uwagą "nieznana pozycja", ale nie na mapie.
6. Podwójne. Reklama z tym samym DX, tym samym obserwatorem, tym samym zespołem i tym samym źródłem jednego już obecnego zastępuje go zamiast dodawania.

C ALL3.TXT to baza nazw → lokalizator używany przez JTDX, w formacie CALL, LOCATORE,,,. Plik projektu zawiera 1 048 847 nazw, wszystkie z poprawnym lokalizatorem; nagłówek pliku nadal mówi o 34 323 nominacjach, ponieważ pozostał to samo co w pierwszej edycji [7]. Jest ładowany w tle przy starcie, więc okno jest natychmiast przydatne. Plik jest przeszukiwany obok programu, w folderze Dane, w folderze, z którego program został uruchomiony i jednym lub dwoma folderami powyżej; Jeśli nie ma, okno pokazuje go przy starcie, a pasek stanu sygnalizuje go na czerwono. Jej nazwa zawsze używa 6 znaków. Tylko reguła dla lokalizatorów: jeśli otrzymany (spot, MQTT, dekodowanie, QSO desunti) ma mniej niż 6 znaków i C ALL3. TXT ma jeden do 6, zawsze warto C ALL3.TXT; dla laptopów (F / IC8TEM, IC8TEM/P) jest tylko dokładny głos: bez, stacja pozostaje bez właściciela i nie jest zaprojektowany gdzie indziej. Zasada ta ma zastosowanie również w czasie rysowania, więc dane uzyskane przed załadowaniem bazy danych będą działać same.

Jedna baza danych: C ALL3.TXT. Od 1.3.4 wszystkie lokalizatory są w jednym pliku, C ALL3.TXT, który pozostaje w formacie JTDX i WSJT- X (CALL, LOCATURE,,,,) i może być używany, jak również w WSJT- X, JTDX i MSHV. Nie ma więcej plików osobno. Na górze pliku, po nagłówku, znajdują się dwa bloki oznaczone wierszem komentarza: / / [LIVETROPOVHF- MANUALI] z wpisami włożonymi ręcznie i / / [LIVETROPOVHF- CLUSTER] z korektami z klastra; następnie / / [LIVETROPOVHF- FONTI] ze wszystkimi innymi pozycjami w porządku alfabetycznym. Każda nominacja pojawia się raz. W podjęzykach Dane i Źródła › Mianowania (C ALL3) szukasz nazwy, piszesz właściciela i naciskasz Add / change: głos idzie w bloku głosów ręcznie i jest wart ponad wszystko, nawet nad lokalizatorem, który sama stacja wysyła. RRRR Usuwa nominację z C ALL3.TXT (następna aktualizacja wznawia ją ze źródeł, jeśli ją mają). Dodaj plik... przeczytaj listę CALL, LOCATOR (akceptujesz również punkt i przecinek, tabelację i przestrzeń) i zapisz go pomiędzy ręcznie wybranymi; wybrany plik nie jest kopiowany. Wczytaj ponownie ponownie C ALL3.TXT z dysku po edycji z edytorem. Całkowita kolejność źródeł: własny lokalizator, głosy ręczne, korekty klastrów, reszta C ALL3. TXT (źródła, wyuczone lokalizatory na żywo i G7VRD), otrzymany lokalizator (który jest ważny w bazie danych tylko wtedy, gdy ma 6 znaków i nie kończy się LL lub MM).

Aktualizacja C ALL3.TXT. W tym samym podjęzyku przycisk Aktualizacja pobiera teraz źródła (C ALL3. TXT WSJT- X Ulepszony, C ALL3. TXT EME W7GJ, grid _ data.bin JTDX), łączy je (pierwszy, który ma nominację wygrać), sprawdzić, że są co najmniej 50.000 nazw (inaczej nic się nie zmienia), zapisuje C ALL3. TXT Poprzedni C ALL3. Pozostają wpisy TXT, a lokalizator 6 znaków pozostaje tam, gdzie źródło ma tylko 4 z tego samego kwadratu. Ręczne głosy i poprawki z klastra pozostają na szczycie, bez zmian. Z pola Uaktualnienia samodzielnie każdego dnia, czek odbywa się na początku. Program powtarza C ALL3. TXT natychmiast po każdym głosie ręcznie i, dla uczonych lokalizatorów, co najwyżej co 10 minut i zamknięcia; C ALL3. TXT pakietu jest tylko read- i atrybut się usuwa. Instalatory Linux nie nadpisują C ALL3. TXT jest już obecny; że dla Windows instaluje go tylko jeśli brakuje. ALL3

Automatyczne korekty z klastra. W automatycznych miejscach DM4X i RBN komentarz przynosi dwa lokalizatory, na przykład KN09tp < > KN09: pierwsza jest to, co stacja deklaruje się, do 6 znaków; druga jest to, że stacji czuł, często tylko w 4. Jeśli pierwszy ma 6 znaków (i nie kończy się na LL lub MM) i jest inny niż w bazie danych, program koryguje C ALL3. Tylko TXT: korekta idzie w bloku / / [LIVETROPOVHF- CLUSTER], z datą ostatniego potwierdzenia w trzecim polu, jest ważna nad źródłami (pod głosami dłoni) i pojawia się między podejrzanymi sygnałami zaktualizowanymi jako "C ALL3 ". Więc przetrwa C ALL3. Aktualizacje TXT. Niepotwierdzona korekta przez dwa lata traci pierwszeństwo i pozostaje normalnym głosem.

Ostatnie źródło: G7VRD. Jeżeli nominowany nie ma znaku 6- w C ALL3. TXT, program pyta go w tle do publicznego API G7VRD (api.g7vrd.co.uk, "lokator przez wywołanie"), który zbiera lokalizatory automatycznie z ruchu sieciowego. Akceptujesz tylko 6-znakowy znak, który nie jest środkiem kwadratu (LL lub MM); żądania są grupowane, maksymalnie 40 nominowanych na minutę, a każdy nominowany prosi raz dziennie, ponieważ jest to usługa hobbistyczna bez gwarancji. Lokalizator wchodzi w C ALL3. TXT jeśli nazwa nie istnieje lub ma tylko 4 znaki tego samego kwadratu; wyszukiwanie wyłącza się z pola w zakładce nazwa.

Mianowanie w barze. W tematach PSKReporter MQTT, pasek oddziela poziomy, następnie nazwy złożone pochodzą z punktu zamiast baru ( IV3. HB9CAT). Program odstawia pasek i, jeśli wiadomość zawiera pełne nazwy, użyj tych: IV3/HB9CAT pozostaje IV3/HB9CAT na mapie, liście i bazie danych.

Lokalizatorzy nauczyli się na żywo. C ALL3. TXT nie zawiera wszystkich: na przykład nie ma tam SP9EMF i SP9NVY, a następnie pozostaje 4-znakowy lokalizator wysłany przez PSKReporter (karta tak mówi). Aby zmniejszyć te przypadki, program pisze w C ALL3. TXT co 6 znaków, które pochodzą z dowolnego źródła (PSKReporter, takich jak nadajnik lub jako odbiornik, klaster, dekodowanie UDP) dla nominacji, że C ALL3. TXT nie ma lub ma tylko 4 znaki w tym samym kwadracie; nigdy nie zmienia głosu na 6 znaków. Do 1.3.3 lokalizatory te były w danych / LOC6.TXT.

Przejście od 1.3.3. Na początku 1.3.4 pliki oprócz poprzednich wersji (Dati / C ALL3 -MANUALE.TXT, Data / C ALL3 -AGGIUNTE, Data / C ALL3 -CLUSTER. TXT, Data / LOC6. TXT i Data / C ALL3 - G7VRD. TXT) są połączone w C ALL3. TXT Zwolnienie pisania, stare pliki podejmują rozszerzenie.fuso, nie czytasz już i można usunąć.

Automatyczne odbiorniki. RBN, dm4x i PSKReporter często zgłaszają nazwy z przyrostkiem wskazującym odbiornik: -SDR, / SDR, - # (skimmer RBN) lub -1, -2... (plus odbiorniki tej samej stacji). Dla właściciela przyrostek jest usuwany: OK2OZL -SDR bierze OK2OZL JN89UE od C ALL3.TXT, jeśli stacja nie wysyła jeszcze 6 znaków. Pokazana nazwa pozostaje z przyrostkiem.

Lokalizatory przecięte. Nie wszystkie źródła leczą rozszerzenie ich właściciela: wiele ustawia go na 4 znaki, a niektóre automatyczne systemy przenoszenie celowo skrócić lokalizator 4 znaków, nawet te, które otrzymują dane z prawdziwego słuchania FT8, FT4, Q65 i JT65, lub zakończyć go z centrum placu (LL lub MM, np. JN89MM). Z tego powodu znak 4 lub 6, który kończy się znakiem LL lub MM, zostaje zastąpiony znakiem C ALL3. TXT tego samego kwadratu. Jeżeli właściwa lokalizacja i środek placu nie zbiega się z kilkunastoma kilometrami, błąd pozostaje niewielki w porównaniu z odległościami tropu.

## 6. Kto słucha?

Każda wiadomość MQTT z PSKReporter zgłasza nazwę i lokalizację osób otrzymujących [5], więc nie trzeba jej szukać w C ALL3.TXT. Dla zespołu wybranego w selektorze program posiada, dla każdej stacji odbiorczej: właściciela, ostatniej recepcji, dekodowanych sposobów, liczby stacji filcu i maksymalnej odległości. Stacja opuszcza listę, gdy nie otrzymuje już nic w wybranym zakresie (3, 5, 10, 15, 30 lub 60 minut).

Na mapie każdy odbiornik jest ciemny różowy pusty krąg, i od każdego zacząć pełne różowe linie do stacji, które czują i wysyła z powrotem do sieci. Linie są zawsze widoczne, bez klikania. Mianowania stacji odsłuchowych, stacji rozsłuchowych i dwóch głów każdego miejsca są zawsze zapisywane na mapie, bez konieczności klikania (ta sama nazwa jest napisana raz). Klikając na okrąg jego linie stają się jeszcze grubsze i otwiera kartę z szczegółami. Jeśli lokalizator z MQTT ma mniej niż 6 znaków, program używa kodu C ALL3.TXT (około miliona nazw), pod warunkiem że jest zgodny z otrzymanym kwadratem.

Rozproszyć tryby meteorytów. Podobnie jak w przypadku plam klastrowych, współczynniki MQTT w MSK144, FSK441, JT6M, JTMS lub ISCAT są odrzucane i liczone tylko; to samo dotyczy odkostniania UDP w ten sposób.

Nie prawdziwi słuchacze. Niektóre stacje deklarują odbiór w zakresie 144 MHz, ale w rzeczywistości odbiornik jest na innym paśmie (niewłaściwe oznaczenie częstotliwości): ich raporty zaprojektowałyby nieistniejące otwory o tysiącach kilometrów. Program sam to sprawdza. Każda stacja, którą słuchacz chce usłyszeć w VHF jest śledzona przez 30 minut na wszystkich zespołach, z dedykowanym napisem MQTT (pskr / filter / v2 / + / {nominative} / #) [5]. Jeżeli co najmniej dwie ze stacji słyszanych przez słuchacza działają w tych samych minutach (± 10) na innych pasmach, słuchacz uważa się za nieprawdziwego, a wszystkie jego raporty VHF są odrzucane; rejestr MQTT podaje nazwę i stacje, które go zdradziły. Na liście spotów, obok mierników, przycisk MQTT pokazuje stan przepływu (zielony liść, jeśli jest podłączony, czerwony, jeśli nie) i ile słuchaczy odrzucono w wybranym paśmie i zakresie; klikając otwiera kartę z listą odrzutów i, dla każdej stacji znalezionych na innych pasmach.

Długa porcelana. Podłączenie ponad 1000 km (Clumbing Spot, MQTT Słuchanie, QSO Desunti) nie jest natychmiast pokazane: pozostaje trzymane, a licznik "trzymał > 1000 km" podaje numer. Ujawnia się, gdy inne połączenie, pomiędzy różnymi stacjami i co najmniej połową dalej, potwierdza je jednym z tych kryteriów: ta sama stacja na głowie (na przykład ten sam słuchacz) i ta druga stacja w tym samym kierunku w granicach 10 °; podobny korytarz, z obiema głowami w odległości 200 km od tego drugiego połączenia; równoległa trasa, z kierunkiem w granicach 10 ° i odległość boczną w granicach 200 km, pokrywająca się wzdłuż trasy. Pojawia się i dziennik MQTT pokazuje powód. Jego dekodowania UDP są zawsze widoczne i stosowane jako potwierdzenie dla innych. Zatrzymywane połączenia pozostają w pamięci przez 60 minut.

Nic poniżej 144 MHz. Program działa od 144 MHz w górę: wszystko poniżej 143 MHz jest odrzucane. Wyjątkiem jest GRAVES, francuski radar transmitowany w CW na 143,050 MHz przez Broye- lès- Pesmes ( JN27SI): komercyjny między 143,040 i 143,060 MHz, lub z GRAVES w nazwie, pojawia się między 144 MHz z radarem jako nadajnik. Deska reklamowa mówi, czy odbiornik znajduje się wewnątrz wiązki (sektory D, C, B, A od 90 ° do 270 ° azymutu widzianego z radaru, 6 stopni od 7,5 ° każdy) lub poza; zakładka GRAVES, jak w YA- MS, pokazuje niebo widziane z radaru: sektory, stopień oświetlony w tym czasie (sekwencja PE1ITR 2021: 0,8 s na krok, cykl 4,8 s Dane i geometria pochodzą z YAMS. Uczucie tego w tropie jest trudne, ale może się zdarzyć.

Rehabilitacja z ludzkich gromad. Stacja, która pojawia się również na innych pasmach, nie liczy się jako podejrzana, jeśli w tych samych minutach (± 30) gromady ludzkie DX ( ON0DXK, GB7RAU; nie dm4x, RBN lub inne źródła automatyczne) aktywne uszkodzenia na danym zespole, w miejscach tworzonych przez inne nazwy: to jest przypadek stacji, które naprawdę pracują na wielu pasmach razem. W ten sposób wyrzucony słuchacz powraca widoczny (rejestr MQTT pisze "zresocjalizowany") i można odsłonić długo utrzymane połączenie dla niskich pasm.

Aktywne stacje poniżej 144 MHz. W przypadku kwarantanny długich połączeń stacja, która w tym samym czasie jest słyszalna na pasmach poniżej 144 MHz (z raportów MQTT innych pasm), nie potwierdza innych połączeń i nie jest odsłonięta, chociaż podobna lub równoległa trasa by to potwierdziła.

## 7. Wykaz reklam

Zajmuje 35% prawo od karty mapy i miejsca. Top: tytuł, selektor pasma (50, 70, 144, 222, 432, 1296, 2320 MHz, 10 GHz), selektor zakresu (ostatnie 3, 5, 10, 15, 30, 60 minut) i klucz aktualizacji. Poniżej, cztery metry: widoczne plamy, AS / ACS, wyrzucone z filtra, stacje odsłuchowe.

Każda linia pokazuje typ linii, czas UTC, DX → DE, sposób, odległość w km i, na drugiej linii, źródło, częstotliwość i komentarz. Najnowszy jest na górze. Jedno kliknięcie na linię wybiera miejsce: na mapie linia rośnie i pojawiają się dwie nazwy.

## 8. Spot karta i troposcatter utrata

Dla wybranego miejsca karta pokazuje lokalizatory, odległość, azymut, częstotliwość, tryb, źródło, teraz, komentarz, średni indeks tropo wzdłuż ścieżki i oszacowanie utraty trasy dla rozproszonego tropo.

Odległość i azymut są obliczane na maksymalnym okręgu o promieniu Ziemi 6371 km (wzór emisenoverso dla odległości, początkowy azymut od stacji DX do obserwatora).

Strata troposcattera. Podstawową stratą dla dwóch stacji na gładkiej ziemi jest suma utraty wolnej przestrzeni i utraty dyfuzji: G3YGF [2]

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

Straty są w dB, D jest odległość w km, f częstotliwość w MHz, θs kąt dyfuzji w stopniach.

8497 km jest równoważnym promieniem Ziemi (4 / 3 fizycznego). θA i θB zależą od horyzontu każdej stacji:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

z Δh wysokość przeszkody, która tworzy z horyzontu w odniesieniu do stacji i jej odległości. Każdy poziom horyzontu kosztuje około 10 dB [2]. Program nie zna horyzontu stacji, więc pokazuje podstawową stratę z θA = θB = 0 i deklaruje ją ("gładka ziemia"). G3YGF dodaje termin klimatyczny, gdzie N jest reaktywnością gleby [2]; będzie stosowany, gdy N jest dostępny z modelu pogodowego:

```math
L_{N} = 0,2 \cdot (N - 310)
```

Weryfikacja: dla 110 km do 144 MHz wzór daje 166,5 dB, wobec około 168 dB łóżek na wykresie artykułu. Dla 500 km do 144 MHz daje około 206 dB. Oczekiwany stosunek sygnału do szumu to pojemność systemu (eirp + efektywna czułość odbiornika) pomniejszona o stratę: z systemem modelu G3YGF 144 MHz (198 dB) ścieżka 500 km daje około -8 dB w SSB [2], to jest połączenie jest tylko z lepszym rozpowszechnieniem czystego rozproszenia.

## 9. Moje dekodowania: UDP z WSJT- X, JTDX i MSHV

WSJT- X, JTDX i MSHV mogą wysłać UDP z każdej linii, którą zdekodują. Program słucha na 127.0.0.1: 2237 i powtarza ten sam pakiet, bajt na bajt, na 127.0.0.1: 2238, więc inne programy, które używały tego portu (GridTracker, JTAlert, logger) nadal działają, jeśli przenieść je na 2238. Adresy i drzwi zmieniają się w podjęzyku UDP kolekcji danych; jako adres odsłuchowy można również użyć grupy multicast (np. 224.0.0.1), jeśli program dekodowania jest ustawiony w ten sposób.

Pakiety są zgodne z protokołem sieciowym WSJT- X: początkowy numer magiczny 0xADBCCBDA, numer schematu i typ wiadomości. Program wykorzystuje głównie trzy typy [8]:

- Heartbeat: mówi, który program przesyła dane i z jaką wersją.
- Status: częstotliwość wybierania i trybu; z częstotliwości otrzymujesz pasmo (z tą samą tabelą spotów).
- Dekodowanie: Teraz, SNR, czas i częstotliwość odpadów, sposób i tekst dekodowanej wiadomości.

Z tekstu można dostać stację słyszałeś: w CQ 9 A2PQR JN83 jest stacją po CQ (oszczędzanie DX, EU, TEST...), w IK0JKL IT9MNO JM68 jest drugim, tj. który przekazuje. Lokalizator jest tym z wiadomości, jeśli istnieje (4 znaki, różne od RR73); w przeciwnym razie można szukać w C ALL3. TXT. Są to linie podsłuchowe: stacje, które odbiornik słyszy teraz. Na mapie stają się one czerwone linie od QTH do stacji, z nazwą; warstwa świeci się z zdekodowanym polu ode mnie i wymaga ważnego lokalizatora. Na liście po prawej licznik mówi ile stacji dekodujesz na wybranym zespole.

QSO desunti dal na żywo. Jeżeli dekodowanie jest komunikatem zamykającym w formie C ALL1 C ALL2 RR73 (lub 73 lub RRR), na przykład IC8TEM IK3FHP RR73 lub IK3FKP SV8PEX 73, obie stacje zakończyły połączenie. Program rejestruje go jako QSO, z lokalizatorem każdego pobranego z ostatniej wiadomości, w której go zadeklarował lub, jeśli nie ma, z C ALL3.TXT [7], i rysuje go na mapie jako zielona linia między dwoma stacjami (wygiąć "QSO desunti dal live"). Para stacji liczy się raz na pasmo; pasmo i zakres wybrane na liście są ważne.

W tym samym podjęzyku, panel Debug pokazuje tekst każdego otrzymanego pakietu (typ, czas, wartości i wiadomość), na życzenie także szesnastkowe bajty oraz linię do przesyłania. Aby sprawdzić, czy program dekodujący wysyła dane.

## 9 bis. Języki Hepburn i F5LEN

Każdy pokazuje, co program bierze ze źródła. Przycisk "Read the page and download all images" otwiera wskazaną stronę (base dxinfocentres.com / tropo _ eur.html for Hepburn [3] and tropo.f5len.org / fopecasts- for- europe for F5LEN [4]) i pobiera wszystkie obrazy w pamięci podręcznej obok wykonywalnego; można również wkleić adres jednego obrazu. Dla każdego obrazu jest pole do wyłączenia go i pole z przewidywanym czasem. 31 europejskich obrazów F5LEN są nazwane przez czas wyścigu modelu (03.png... 48.png co 3 godziny, 54... 96 co 6 godzin, 108... 192 co 12 godzin) i program zajmuje czas od nazwy pliku [4]; dla Hepburn to kolejność strony. W zakładce F5LEN menu wybiera region: Europa (tropo.f5len.org / contracasts- for- europe), Australia (tropo.f5len.org / Australia, vk03.png... vk192.png) lub Atlantyk pomiędzy Zielcem a Irlandią (tropo.f5len.org / Atlantic- ocean / Atlantic- d4-ei, atl03.png... atl192.png). Każdy ma te same kroki.

Poniżej znajduje się licznik kalibracji. Jedno kliknięcie na obraz dodaje punkt kontrolny (lokalizator lub lat, lon: zajmuje co najmniej 6) lub kolor legendy z jego wartością. Program oblicza dopasowanie pikseli → współrzędnych z interpolacji wielomianowej drugiego stopnia do kwadratów minimalnych [19], odczytuje kolory z wybraną tolerancją i, dla F5LEN, konwertuje jednostki N w skali 0- 10 z · N + b. "Zapisz kalibrację i odczyt testowy" zapisuje plik w folderze Tarature i sprawdza, który piksel twój lokalizator spada. Przed rozpoczęciem kalibracji należy zwrócić się o zezwolenie do autora map.

Regiony. Każde źródło ma więcej regionów, każdy z utrwalonymi obrazami: Hepburn tr _ map / fcst / {code} {ore}.png? v {data} (eur Europe, nwe North- Western Europe, eeu Eastern Europe, ent North- East Atlantic, nat North Atlantico, eam North America East, nsa South America North America, Sam South America, Mid- East Asia, nca Central- Septential Asia Zakładka każdego źródła pokazuje przede wszystkim podgląd fuzji: wszystkie pobrane obrazy zebrane razem jak na mapie głównej, z kropkowaną taflą i nazwą każdego regionu, szarą zasłonę, w której żaden obraz nie pokrywa obszaru i suwak do przewijania godzin; po lewej stronie znajduje się lista regionów, z których każdy ma pole do użycia, liczba odczytanych map i skąd pochodzą skrajności (OCR, ręcznie lub fabrycznie; jeśli OCR zawiedzie tekst, który przeczytał). Ręczna ławka kalibracyjna pozostaje za przyciskiem homonimicznym. Menu regionu Bench wybiera, które spojrzenie, pole "Użyj tego regionu na mapie" zawiera go w indeksie. Rama znajduje się sama (dwie poziome linie i dwie pionowe linie tego samego koloru); umieścić obraz służyć skrajne lat / Lon ramy: dla Hepburn Europe (10 ° W- 40 ° E, 27 ° -56 ° N) i F5LEN Europe (15 ° W- 40 ° E, 30 ° -65 ° N) są już w programie, dla innych regionów zapisują w czterech polach O E S N czytając etykiety osi. Mnożnik skali sprawia, że oba źródła są porównywalne: wartość końcowa = mnożnik × wartość legendy. [3] F5LEN [4] F5LEN

## 9 ter. Eksportuj do słowa

Dwa dokumenty znajdują się na górze przycisku "Eksport do Słowa". Plik jest napisany w formacie Office Open XML; formuły stają się równaniami Word (OMML), więc pozostają edytowalne z edytorem równań [16]. Jeśli okno zapisu nie jest dostępne, plik kończy się w folderze Dokumenty obok wykonywalnego.

## 9 kwater. Who calls me

Jeśli wiadomość jest wysyłana do Ciebie na lokalnym UDP (pierwsze imię jest Twoje, na przykład IC8FAX IK3FHP JN55 lub IC8FAX IK3FHP -12), program śledzi Cię z QTH do stacji, która nazywa Cię czarną linią przerywaną i migającą, z nazwą i lokalizatorem obok Ciebie. Lokalizator pochodzi z C ALL3.TXT (w tym głosy ręczne) lub wiadomości. Linia pozostaje do czasu, aż UDP przejdzie przez zamknięcie QSO z tą stacją ( RR73, RRR lub 73, w jednym z dwóch kierunków), co najwyżej 3 minuty od ostatniego połączenia; można wyciągnąć wszystko i nie przestrzegać kryteriów wykluczenia, więc bezpośrednie połączenie nigdy nie ucieka.

PSKReporter, za pośrednictwem MQTT, nie przekazuje tekstu wiadomości: mówi tylko kto otrzymał, z częstotliwością, sposobem i SNR. Dlatego bezpośrednie połączenie jest rozpoznawane tylko przez własne dekodowania UDP.

## 10. Kontrola czasu (NTP)

Tryby cyfrowe i czas spotu wymagają dokładnego zegara. Co 3 minuty program przesłuchuje 10 serwerów NTP (SNTP na UDP 123): 0.pool.ntp.org, 1.pool.ntp.org, 2.pool.ntp.org, 3.pool.ntp.org, it.pool.ntp.org, ntp1.inrim.it, ntp2.inrim.it, ptbtime1.ptb.de, time.google.com, time.cloudflare.com [11].

Dla każdej odpowiedzi oblicza się opóźnienie odrzutu i powrotu, odrzuca wartości nieprawidłowe i bierze medianę, z obliczeniem odrzutu i opóźnienia dostarczone przez SNTP [10]. Z co najmniej 3 odpowiedzi zgadzasz się z zegarem UTC programu dostosowuje i następny pojawia się NTP: OK w kolorze zielonym; jeśli odpowiedzi nie wystarczą pojawiają się NTP: FAILED w kolorze czerwonym i zegar pozostaje tym z komputera. Zegar systemowy nie jest dotykany, ponieważ potrzebne byłyby uprawnienia administratora; wszystkie wewnętrzne harmonogramy programów (spoty, 5- 60 minut okna, dekodowania) używają ustawionego czasu. Sterowanie zawsze obraca się w tle: w interfejsie widzisz tylko wynik obok zegara; serwery, interwały i minimalne odpowiedzi zmieniają się w LIVETROPOVHF.ini (ntp _ servers, ntp _ minutes, ntp _ min).

## 10 bis. Ogólne kryteria wykluczenia

Dwa pola w pasku warstw odnoszą się do wszystkiego, co narysowałeś (spot, PSKReporter, dekodowanie, QSO desunti):

- Wyłączyć poniżej 100 km. Ruch między stacjami poniżej 100 km może nie być znaczący dla tropu (fala naziemna, zakres optyczny): z skrzynką nie rysuje się i nie pojawia się na liście. Start odwołany.
- Ponad 1000 km: tylko kwadraty. Wokół QTH (lokalizator ustawiony do uruchomienia) jest korona 1000 km we wszystkich kierunkach. Jeśli tylko jedna z dwóch stacji linka znajduje się poza tą koroną, połączenie nie jest rysowane jako linia: na jego miejscu kolorują się liliami, kropkowane na 45 °, place lokalizacyjne (4 znaki) stacji, których to dotyczy. To samo dotyczy stacji PSKReporter słuchających ponad 1000 km, które pojawiają się jako kwadrat zamiast jako okrąg. Jak dotąd widać ruch bez wypełniania mapy linii. Bez poprawnego lokalizatora liczy długość połączenia. Kwadrat twojego QTH nigdy nie barwi się w liliach, nawet jeśli słuchasz stacji za koroną: stosuje ustawionego właściciela, każde nazwisko używa kto działa ze stacji. Promień korony jest wybierany obok pola: 1000, 700 lub 500 km. Pudełko Corone 1000 / 700 / 500 km przyciąga trzy korony wokół QTH, narysowane liliami, z najbardziej zaznaczonym wyborem i odległości napisane na północ. Na liście miejsc odległość jest następnie.. Zaczynamy. Kryterium stosuje się po kwarantannie: długie połączenia nadal nie pojawiają się nawet jako kwadraty.

Jakie są kwadraty liliowe pokryte w 45 °. Ruch z dala od QTH interesuje, aby zrozumieć, gdzie rozmnażanie jest otwarte, ale jego linie, setki kilometrów długości i często dziesiątki, pokrycie mapy i ukryć to, co się liczy dla Ciebie: otwory można wykorzystać ze stacji. Z tego powodu, poza wybraną koroną, program nie rysuje linii, ale kolor kwadratów lokalizacyjnych (2 ° × 1 °) z zaangażowanych stacji. Rezultatem jest mapa "aktywnych stref": gdzie pojawia się wiele pobliskich liliowych kwadratów, istnieje kanał lub otwarcie, nawet jeśli nie dotyczy bezpośrednio; Jeśli obszar rozszerza się w kierunku Ciebie, jest to sygnał, aby skierować antenę w tym kierunku. 45 ° deska rozdzielcza pozwala zobaczyć pod indeksem tropo i siatki, więc porównują one w skrócie prognozę (dolny kolor) i prawdziwy ruch (liliowy). Kwadrat QTH nigdy nie barwi, ponieważ jest punktem wyjścia, a nie odległym obszarem. Tylko kwadraty, które są za koroną, są kolorowe: kwadrat skrzyżowany przez koronę, lub wewnątrz, pozostaje wolny, nawet jeśli istnieje jedna z dwóch stacji. Aby zobaczyć linie, wystarczy wyłączyć pudełko lub wybrać większą koronę.

## 11. Tematy i języki

Tematy: 18 wspólnych dla programów IC8TEM, takich samych jak YA- NEC i E- LAYER ANALYZER: Clear, Night, Black, osiem pasteli (niebieski, pomarańczowy, aquamarine, zielony, różowy, lilac, indygo, brązowy), Scia meteoryt (YA- MS palette), trzy IP-TRX- CTRL (Chiaro, Notte, Nero) i Blue, Purple, Green. Z 1.3.4 Chiaro wygląda jak YA- NEC: jasnoszare tło, graniczy z białymi panelami, niebieskimi nocnymi zakładkami i niebieskim akcentem. W ciemnych tematach również mapa bazowa staje się ciemna. Skala kolorów prognozy pozostaje taka sama we wszystkich tematach. Na wąskich ekranach (poniżej 1500 pikseli, na przykład 1366 × 768) zegar na górze pokazuje tylko godzinę, więc motyw, kursor tekstów i języka pozostają widoczne. Języki: YA- MS [17] wiki (angielski, Travel, Deutsch, Nederlands, Français, Español, Português, Dansk, Norsk, Svenska, Íslenska, Eesti, Lietuvious, Latviešu, Руский,, кра, нська, Türkçe, Egypt Z arabskim, hebrajskim i urdu całe lusterka okienne od prawej do lewej; mapa pozostaje jako [12]. Tłumaczenia dotyczą interfejsu; dwa dokumenty techniczne i komunikaty paska statusu pozostają w języku włoskim. Nazwy dni pochodzą z międzynarodowych ustawień. NET dla wybranego języka.

Motyw i zmiana języka z dwóch menu w prawym górnym rogu, obok zegara, i stosować natychmiast, bez ponownego uruchamiania programu i bez przerywania zbierania danych. Wybór jest zapisany w systemie LIVETROPOVHF.ini. pl

## 11 bis. Sprawozdania z podejrzeń i samouczenia się

Zakładka Podejrzane Raporty zbiera wszystko, co steruje programem znajduje dziwne, pogrupowane według typu i nazwy, z ostatnich 24 godzin w liście. Program sam się uczy: dla każdej pary (typ, nazwa) liczy dodatnie wyniki (+) i przeciwne (−), a po 3 uzgodnionych wynikach tworzy regułę automatyczną; przeciwny wynik stawia go w wątpliwość. Decyzje użytkowników (Good, Poor) zawsze mają zastosowanie; przycisk pulsante zwraca decyzję do programu. Zasady są w Data / Adredimento. TXT, obok programu; automatyczne nie wzmocnione przez rok są zapomniane.

| Typ | Kiedy | Wyjście + / − | Wpływ zasady |
| EME ( Q65/JT65) | Q65 lub JT65 ponad 1000 km z Księżyca nad horyzontem do obu głów, lub EME w komentarzu | − za każdym razem, gdy Księżyc jest wspólny | Wyrzucić: EME station, jego długie Q65/JT65 s są również wyłączone bez wspólnego księżyca. Dobre: traktowane jako tropo |
| Długie połączenie | Ponad 1000 km w kwarantannie | + odsłonięte, − wygasły bez potwierdzenia | Dobrze: długość stacji pokazuje natychmiast. Wyrzucić: nigdy |
| Słuchacz | Odbiornik PSKReporter, który słyszy aktywne stacje na innych zespołach | − wyrzucony, + zresocjalizowany przez klastry ludzkie | Dobrze: nigdy nie wyrzucony. Wyrzucić: zawsze |
| Inny właściciel | Kwadrat otrzymał inny niż C ALL3. TXT | + za każdym razem, gdy to powtarzasz | Dobrze: stacja się przemieściła, to jest warte placu otrzymał. Wyrzucić: vale C ALL3. TXT |
| Centrum kwadratowe | 6 znaków z LL lub MM |  —  | Wyłącznie informacje |
| Automatyczny odbiornik | Mianowanie z -SDR, / SDR, - # or -n |  —  | Wyłącznie informacje |

EME. Q65 i JT65 to sposoby komunikacji przez Lunę: Teoretycznie są one objęte klauzulą wykluczenia jak meteoryt rozproszony, ale są również używane w tropo. Program oblicza położenie Księżyca z niskoprecyzyjnymi formułami Almanac Astronomical (około 0,3 °) [20]: jeśli połączenie w Q65 lub JT65 przekracza 1000 km, a Księżyc znajduje się nad horyzontem do obu głów, jest prawie na pewno EME i jest wykluczone (liczy się między wykluczonymi). Jeśli Księżyc nie jest wspólny, połączenie pozostaje możliwe w tropie i przechodzi przez kwarantannę długich połączeń, ale sygnał pozostaje widoczny. Ma ona zastosowanie do klastrów, do MQTT i jego własnych dezodów UDP.

## 11 ter. Skontaktowane kryteria odrzutów, jeden po drugim

Program chce pokazać tylko propagację tropoferyczną w 144 MHz i poza nią. Każde dane przychodzące (Cluster Row, PSKReporter Report via MQTT, dekodowanie UDP) przechodzą przez serię filtrów, zawsze w tej samej kolejności. Porzucone dane nie znikają w niczym: są liczone (kontakt wyłączonych w górnym pasku), a kiedy przyczyna jest wątpliwa, pojawia się w zakładce Zawieszone raporty.

|  N.  | Kryteria | Gdzie to jest? | Co zostało usunięte | Możesz się przebrać? |
|  1  | Zakres | klaster, MQTT, UDP | Wszystkie poniżej 143 MHz (również 50 i 70 MHz), z wyjątkiem GRAVES 143,050 MHz. Utrzymane zespoły to 144, 222, 432, 1296, 2320 MHz i 10 GHz | Nie. |
|  2  | QO- 100 | klaster, MQTT, UDP | 2400,0- 2400,5 MHz i 10489,5- 10490 MHz: to geostatyczny ruch satelitarny, nie tropo | Nie. |
|  3  | Rozproszenie meteorytu | klaster, MQTT, UDP | JT6M i ISCAT (także ISCAT- / B), komentarze z etykietami "&" i nie-tropoxy propagacji (< AU >, < AUE >, < ES >, < FAI >, < F2 >) zawsze. MSK144, FSK441 i JTMS (także z < MS >) tylko ponad 250 km: w obrębie 250 km akceptujemy, ponieważ na tej odległości tryby te są również stosowane w tropo | Nie. |
|  4  | EME (via Luna) | klaster, MQTT, UDP | EME napisane w komentarzu; Q65 lub JT65 ponad 1000 km, gdy Księżyc znajduje się nad horyzontem dla obu głów; stacje nauczane jako EME | Tak: Dobra zasada w podejrzanych sygnałach |
|  5  | Nie - Autentyczny słuchacz | MQ | Odbiornik PSKReporter, który mówi, aby usłyszeć na 144 stacjach aktywnych w tym czasie na innych zespołach (co najmniej dwóch): Jego związki są odrzucane. Powrót dobry, jeśli klastry ludzkie DX potwierdzają te same stacje | Tak: Dobry / biedny |
|  6  | Kwartan długich połączeń | wszystkie linie | Ponad 1000 km ogniwo pozostaje ukryte, dopóki nie znajdzie potwierdzenia: ta sama stacja w tym samym kierunku, inna trasa w korytarzu 200 km lub równoległa trasa w odległości 10 ° i 200 km wzdłuż co najmniej połowy. Bez potwierdzenia wygasa po 60 minutach | Tak: Dobrze / Off do stacji |
|  7  | Poniżej 100 km | linie i lista | Dzięki skrzynce, najkrótsze połączenia 100 km (fala naziemna, zasięg optyczny) | Skrzynka, wyłączona przy starcie |
|  8  | Poza koroną z QTH | linie | Jeśli stacja znajduje się ponad 1000, 700 lub 500 km (na Państwa wybór) od QTH, nie ma linii: tylko liliowe kwadraty objęte dwoma stacjami. Twój plac QTH nigdy nie ma kolorów | Pole i promień na 1000 km |
|  9  | Okno czasu | wszystko | Starsze plamy wybranego zakresu (od 3 do 60 minut) | Menu na górze |
|  10  | Podwójne | klaster | To samo miejsce (ten sam DX, ten sam obserwator, ten sam pasek, to samo źródło) powtarzane | Nie. |

Uwaga dotycząca kryteriów 4 i 6. Podłączenie w Q65 lub JT65 na 1000 km bez Wspólnego Księżyca jest możliwe w tropo: nie jest odrzucane przez kryterium 4, ale przechodzi do kwarantanny kryterium 6, jak każde inne długie ogniwo. Bezpośrednie rozmowy do Ciebie (sekcja 9 quater) są jedynym wyjątkiem od kryteriów 7 i 8: zawsze rysują.

Co nie jest stratą. Korekty lokalizatora (C ALL3. TXT z ręcznymi głosami, korektami z klastra i uczonymi lokalizatorami, przyrostkami -SDR, centrum placu LL / MM) zmienić, gdzie narysować stację, a nie wyrzucić. Z drugiej strony nie można narysować stacji bez właściciela: pozostaje na liście z odległością "-".

## 11 kwater. Kolory i symbole

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

## 12. Ustawienia i przenośność

Okno. Na początku okno zajmuje 90% powierzchni użytkowej ekranu (przecięcie paska aplikacji), więc na 15- 16-calowym notebooku z 125% lub 150% zmianą rozmiaru paska tytułowego pozostaje widoczny. Jeśli przestrzeń użytkowa jest poniżej 1280 × 720 punktów, część okna zmaksymalizowana. W systemie LIVETROPOVHF zapisany jest rozmiar, położenie i maksymalny status. ini na zamykaniu i kończeniu na następnym starcie, pod warunkiem, że pasek tytułowy nadal spadnie na ekran (na przykład po odłączeniu zewnętrznego monitora); w przeciwnym razie powrócisz do 90%. Minimalny rozmiar wynosi 900 × 560.

Program jest przenośny: nigdy nie pisze w folderze AppData ani w rejestrze. Wszystko znajduje się w folderze wykonywalnego: LIVETROPOVHF.ini (zestawy utworzone przy pierwszym zamknięciu), C ALL3.TXT (z ręcznymi głosami, korektami i lokalizatorami), mapowanie pamięci podręcznej i danych pogodowych, zasady (Dati / APPRENDIMENTO.TXT) i archiwum LIVETROPOVHF.db. Można go również przechowywać na pulpicie USB, o ile folder jest do zapisu.

Ad archive (SQLite [14]). Każdy akceptowany punkt i każdy raport podsłuchowy PSKReporter jest zapisywany w bazie danych LIVETROPOVHF.db, SQLite w jednym pliku. W pamięci tylko ostatnie 60 minut pozostaje dla listy i mapy; archiwum zachowuje wszystko i służy historykowi i statystyce (także dekodowania UDP), na przykład otwory dla pasma i dziennie, maksymalne odległości, kwadraty osiągnięte, i tylne porównanie oczekiwanego indeksu i rzeczywistych spotów. Linie odrzucone z filtra są liczone, ale nie archiwizowane.

## 12 bis. Automatyczna instalacja i aktualizacje

Z wersji 1.0 program już pobiera gotowe z strony Release github.com / ic8tem / LIVETROPOVHF, w tych samych pakietach YA- MS: installer dla Windows (64 bit, 32 bit i ARM, instaluje w C:\ LIVETROPOVHF bez praw administratora) i przenośny zip; dla Windows 7 SP1 i 8.1 pakietów osobno (-windows7- x64 i -wind.deb,.rpm, Arch, Slackware, Appimage i tar.gz dla Linuksa; pakiety dla Raspberry Pi (64 i 32 bity) i Alpine;.dmg i appss.zip dla Mac (Apple Silicon i Intel). Z pakietami Linuksa program jest kopiowany do pierwszego startu w ~ / LIVETROPOVHF, więc ustawienia i dane pozostają obok programu; na danych Mac rodzi się obok LIVETROPOVHF.app.

Co 30 minut (pierwsza kontrola kilka sekund po starcie, natychmiast, jeśli ostatni raz program został zamknięty z powodu błędu) program czyta samodzielnie, nie prosząc o nic, ostatnie wydanie. Jeżeli wersja jest nowsza, pobierz przenośny pakiet odpowiedni dla systemu i zastąp pliki programu na miejscu: używane pliki są zmieniane na nazwę *.old- update i usuwane przy następnym ponownym uruchomieniu. Ustawienia, archiwum, folder danych i C ALL3. TXT nie jest dotykany. Nowa wersja jest aktywna od następnego startu: przy instalacji zakończonej okno proponuje ponowne uruchomienie; w przypadku pilnych poprawek, oznaczonych [obowiązkowo] w wersji Release, restart nie może być odroczony. Status sterowania i używana wersja są zapisywane na dole paska stanu; aby wyłączyć aktualizacje = 0 w systemie LIVETROPOVHF.ini. type Na komputerze Mac aplikacja jest podpisana i nie zmienia się: program informuje tylko, że istnieje nowa wersja.

Na początku pojawia się obraz LIVETROPOVHF z numerem wersji, na końcu program prosi o potwierdzenie i podziękowania za jego użycie.

## 12 ter. Dziennik błędów i pasek stanu

Wszelkie nieprzewidziane błędy kończą się w Data / errori.log, nawet przy wyłączonym dzienniku obliczeniowym: data i godzina, wersja, system, otwarta karta, kompletny błąd i ostatnie linie programu. Błąd interfejsu nie zamyka już programu: w pasku stanu pojawia się "anno zanotowany błąd" (kliknij: otwórz rejestr). Na następnym początku program pyta, czy wysłać raport: otwiera przeglądarkę na GitHub sygnał już skompilowany, z nominacji, lokalizator i nazwa użytkownika PC usunięte, które można sprawdzić i wysłać z wolnego konta GitHub. Logarytm kończy się także każdym sprawdzaniem aktualizacji.

W pasku stanu są kliknięte kulki źródłowe (klaster DX, RBN, PSKReporter, MQTT, UDP) i przyniesione do źródła; komiks mówi, dlaczego źródło jest wyłączone: odrzucony port, węzeł klastra po stronie serwera, brak odpowiedzi, lub UDP słuchania, ale żadnych pakietów z WSJT- X, JTDX lub MSHV. Jedno kliknięcie na numer wersji natychmiast sprawdzić aktualizacje.

## 13. Platformy

Zewnętrzne zakładki: Avalonia dla interfejsu [12], MQTTnet jako klient MQTT dla strumienia PSKReporter [13], Microsoft. Data.Sqlite dla archiwum [14].

C # na.NET 10 (LTS) [15] z interfejsem Avalonia, opublikowany jako niezależny wykonywalny dla Windows 64 i 32 bitów, Mac Intel i Apple Silicon, Linux Debian i Raspberry Pi (64 i 32 bity). Zaznaczone limity: Windows 10 / 11; MacOS 14 lub później, z aplikacjami do podpisania lub otworzyć w prawo → Otwórz; Malina z Pi 2 / Zero 2 W up (modele ARMv6 nie są obsługiwane przez.NET).

## Dziękuję.
Dzięki I5MZY Massimo Zannoni do testowania na Windows 7 i Windows 8.

Rajstopy powietrzne: ITU- R P.453, produkty cyfrowe grupy analitycznej 3 (© ITU), używane do celów osobistych i niekomercyjnych.

## Główne źródła

1. S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", prezentacja na 41. Nordic VUSHF Meeting, Skjeberg 2019. Tryby troposferyczne, lista kontrolna zdarzeń (prognoza Hepburn / F5LEN, profile wysokiej temperatury z Windy, klaster DX jako dowód otwarcia), obserwacja, że tylko prognozy map nie są wystarczające dla ekstremalnych tras.
2. J. N. Gannaway G3YGF, "Tropospheric Scatter Propagation", QST Listopad 1983 (z artykułu "Komunikacja radiowa", RSGB, sierpień 1981). Formularze utraty i dyfuzji wolnej przestrzeni, równoważny promień 8497 km, termin lokalizacji 10 dB na horyzont, termin klimatyczny 0,2 · (N − 310), pojemność instalacji i stosunek sygnału do szumu, np. przy 144 MHz.
3. W. R. Hepburn, DX Info Centre, "Tropospheric Ducting Property" (dxinfocentre.com / tropo.html) oraz "The Hepburn Tropo Index (HTI)". Skala indeksu i znaczenie wartości, przewidywanie na 6 dni.
4. F5LEN, prognoza dla Europy (tropo.f5len.org / foperasts- for- European: lista obrazów w skrypcie strony, legenda klasy Disturb... Duct in the image) i prognoza świata (tropo.f5len.org / WW) oraz uwaga "Nowa wersja globalnego narzędzia VHF prognozowania propagacji troposfery". Indeks oparty na reaktywności w jednostce N, 40 kroków od 3 godzin, aktualizacja 4 razy dziennie.
5. mqtt.pskreporter.info, dystrybucja MQTT w czasie rzeczywistym reklam pskreporter.info (kurated przez M0LTE, przepływ P. Gladstone używany za pozwoleniem). Drzwi 1883- 1886, schemat tematyczny pskr / filter / v2.
6. Podsumowanie projektu 8 / 11 / 2026 i Go test (core.go, gui.go) dostarczone przez autora programu. Ad Rules, filtr ujemny, segmenty przepustowości, zarządzanie klastrami, tematy, języki i przenośność.
7. C ALL3.TXT, baza danych nominacji → Właściciel JTDX (I. UA3DJY, A. ES1JA skrypt, JTAlert / hamspots.net dane). Format pliku i liczba nazw zweryfikowanych w pliku projektu.
8. WSJT- X, kod źródłowy, plik NetworkMessage.hpp (opis protokołu UDP: Heartbeat, Status, Decode). Odniesienie ogólne, niezałączone; JTDX i MSHV stosują ten sam format.
9. Reverse Beacon Network (reversebeacon.net), strony serwisowe węzła telnet. Porty 7000 (CW, RTTY) i 7001 ( FT8), loguj się tylko z nazwą.
10. D. Mills, RFC 4330 "Simple Network Time Protocol (SNTP) Version 4", IETF 2006. Format opakowania, obliczanie odpadów i opóźnienie powrotu. Odniesienie ogólne, niezałączone.
11. NTP Pool Project (ntppool.org); INRIM, NTP time service (ntp1 / ntp2.inrim.it); PTB (ptbtime1.ptb.de). Serwery używane do kontroli czasu. Odniesienie ogólne, niezałączone.
12. Avalonia UI, urzędowa dokumentacja (docs.avaloniai.net). Interfejs multiplatformowy, motywy i układ od prawej do lewej. Odniesienie ogólne, niezałączone.
13. MQTTnet,.NET biblioteka dla MQTT (github.com / dotnet / MQTTnet). Klient używany do przepływu PSKReporter. Odniesienie ogólne, niezałączone.
14. SQLite (sqlite.org) i Microsoft. Data.Sqlite (learn.microsoft.com). Archiwum w jednym pliku. Odniesienie ogólne, niezałączone.
15. Microsoft,.NET 10 - obsługiwane systemy operacyjne i autonomiczne publikacje w jednym pliku (learn.microsoft.com / dotnet). Platformy i limity określone w punkcie 13. Odniesienie ogólne, niezałączone.
16. ECMA- 376 "Office Open XML File Formats", część 1, sekcja 22.1 (Office Math Markup Language). Struktura pliku.docx i równań. Odniesienie ogólne, niezałączone.
17. Lista 28 języków wiki YA- MS, dostarczonych przez autora programu.
18. Open- Meteo, prognoza pogody API (open- meteo.com), z danymi z modelu NOAA GFS. Ciśnienie na poziomie morza dla izobara (zob. dokument 2).
19. J. P. Snyder, "Map Projections - A Working Manual", USGS Professional Paper 1395, 1987, and standard polynomial transformation methods for georeferencing images. Odniesienie ogólne, niezałączone.
20. Astronomiczny almanac, sekcja D, "Formuły niskiej precyzji dla Księżyca" (precyzja położenia około 0,3 °). Rozpoznawał połączenia EME. Odniesienie ogólne, niezałączone.
