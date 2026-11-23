LIVETROPOVHF · dokument techniczny 3
# Jak utworzyć trope w 144 MHz i dalej

> Automatyczne tłumaczenie przez włoski: w przypadku wątpliwości warto włoskiego przewodnika.

Co dzieje się w powietrzu, kiedy 144, 432 i mikrofale wychodzą poza horyzont: załamania, inwersji, kanałów, rozproszenia i jak je przeczytać na mapie programu. Wersja z dnia 9 listopada 2026 r.

## Podsumowanie

- Fale VHF i poza podróżą w pierwszych kilometrach atmosfery (troposfera) i zginanie, gdzie wskaźnik refrakcji zmienia się za pomocą [3] [7] [11].
- Wskaźnik mierzy się za pomocą refraktywnego N, który zależy od ciśnienia, temperatury i przede wszystkim od wilgotności [11].
- W normalnej atmosferze jest około 40 jednostek na km; gdy więcej niż 157 jednostek na km fala jest uwięziona w kanale [7] [11].
- Przewód pochodzi z inwersji termicznej z suchym powietrzem powyżej i na mokro poniżej: antycykliny, morze, spokojne noce, [3] [4] [5] [13] fronty.
- Im większa częstotliwość, tym cieńszy kanał jest wystarczający: mikrofale otwierają się częściej niż 144 [3] [7].
- Bez przewodu pozostaje rozprysk troposfowy: zawsze obecny, słaby, który spada z odległości [6].
- W programie: wysokie ciśnienie + reverse + Hepburn / F5LEN wysokie = prawdopodobne warunki; miejsca potwierdzają [1] [2] [13] [14].

## 1. Gdzie to się dzieje: troposfera

Troposfera jest najniższą warstwą atmosfery, do około 10- 12 km. Dla tropu liczyć przede wszystkim pierwsze 3 km: są pary wodnej i inwersji, a tam przejść trasy radiowe między stacjami na ziemi [4]. W normalnych warunkach temperatura spada o około 6,5 ° C na km [3]. Z drugiej strony, w warstwie, temperatura wzrasta wraz z wysokością, mówimy o odwróceniu termicznym: jest to punkt wyjścia prawie wszystkich silnych otworów tropowych.

Jonosfera nie odzwierciedla VHF, ponieważ HF (maść i sporadycznie, które mają inne godziny i inne odległości [5]): poza horyzontem pojawiają się tylko wtedy, gdy atmosfera pochyla je na ziemię lub je rozprzestrzenia.

## 2. Refrakcja i refrakcja N

Fala radiowa pochyla się w powietrzu z indeksem refrakcji powietrza z innym. Wskaźnik n jest bardzo blisko 1, więc stosuje się reaktywność N, tj. n-1 pomnożone przez milion [11]:

```math
N = (n - 1) \cdot 10^{6} = \frac{77,6}{T} \left( P + 4810 \, \frac{e}{T} \right)
```

P ciśnienie w hPa, T temperatura w kelvin i ciśnienie pary wodnej w hPa. N gleba zazwyczaj 300- 400. Drugi termin, że pary, waży dużo: z tego powodu wilgotność liczy się tak samo jak temperatura [3] [11].

Co wygina falę nie jest wartością N, ale jak zmienia się wraz z kwotą, gradient dN / dh:

| dN / dh (N / km) | Sytuacja | Wpływ na falę |
| 0 | Subrefrakcja | Fala oddala się od ziemi, krótszy horyzont |
| od 0 do -79 | Normalny (norma -39) | Lekkie zginanie do podłoża: horyzont radiowy około 4 / 3 optycznego |
| od - 79 do - 157 | Superrefrakcja | Fala zgina więcej, wyższe natężenie przepływu |
| poniżej -157 | Zawartość | Fala pochyla się jak Ziemia lub więcej i pozostaje uwięziona |

Próg -157 N / km jest punktem, w którym krzywizna promienia jest równa krzywiznie Ziemi [7] [11]. Stąd "równoważna wiązka naziemna" wynosząca 8497 km (4 / 3 rzeczywistego promienia) użyta do obliczenia rozproszenia [6].

## 3. Inwersja: silnik tropowy

Gradient poniżej -157 N / km wymaga zaniku N na kilku metrach wysokości. Dzieje się tak, gdy nad warstwą świeżego i wilgotnego powietrza znajduje się cieplejsza i sucha: temperatura wzrasta i para spada razem, i oba sprowadzają N [3] [5]. LA0BY [3]

Inwersji powstają na różne sposoby:

- Uległość w antycyklinie. W wysokim ciśnieniu powietrze na wysokości spada powoli, ściska się i ogrzewa, podczas gdy na ziemi pozostaje świeże i wilgotne powietrze. Jest to przyczyna dłuższych i dłuższych otworów, które w ostatnich dniach [3] [4] [13].
- Nocne napromieniowanie. Z spokojnym niebem i słabym wiatrem gleba ochłodzi się w nocy i ochłodzi powietrze kontaktowe: Niski rewers, który pęka z porannym słońcem. Wieczór i poranne otwarcie, typowe dla lata i jesieni.
- Ostrzeżenie o morzu. Gorące i suche powietrze z ziemi nad zimniejszym morzem: warstwa morska pozostaje wilgotna i chłodna poniżej. Stąd wielkie szlaki morskie (Atlantyk, Morze Śródziemne, Morze Północne) [4] [13].
- Granice. Przed ciepłym frontem przepływa gorące powietrze nad zimnym; otwór jest krótki i podąża za czołem.
- Wyparowanie. Nad morzem, w pierwszych metrach, wilgotność szybko spada wraz z wysokością: bardzo cienki kanał, przydatny tylko do mikrofal.

Znak, że rewers jest widoczny w pionowych profilach (radiosondaggi): wzrost temperatury i punkt rosy schodzący na tym samym odcinku [5]. Program pokazuje wskazówkę z warstwą termalną Inwersji (powietrze o temperaturze 925 hPa cieplej niż na ziemi) oraz izotermalną o temperaturze 850 hPa.

## 4. Przewody powierzchniowe i wysokie

Przewód jest warstwą, w której fala porusza się poprzez odbijanie się między górną krawędzią (inwersja) a podłożem lub dolną krawędzią [7]:

- Gazociąg powierzchniowy: grunt lub morze. Stacje na niskich wysokościach, szczególnie na wybrzeżu, wchodzą bezpośrednio.
- Wysoki rurociąg: stoi na wysokości 500-2000 m. Stacje na równinach mogą nie czuć, podczas gdy te na wzgórzu lub w górach wchodzą do kanału. Wyjaśnij linki, które przekraczają Alpy lub inne [3] reliefs [4].

Aby wejść do kanału fala powinna rozpocząć się prawie poziomo: niskie anteny na horyzoncie i nie sąsiednich przeszkód. Wewnątrz kanału tłumienie jest znacznie niższe niż w wolnej przestrzeni, a odległości sięgają 1000- 2000 km i dalej (na przykład połączenia między Półwyspem Iberyjskim, Wyspami Kanaryjskimi i Anglią lub między Skandynawią a południową Rosją) [3].

## 5. Częstotliwość: dlaczego częściej otwierają się mikrofale

Przewód zatrzaskuje tylko wystarczająco krótkie fale niż jego grubość. Długie fale "wychodzą" z cienkiego kanału; krótkie nie. Następnie [3] [7]:

| Zakres | Wavellendar | Czego potrzebujesz? |
| 144 MHz |  2 m  | Grube rurociągi (centinaia metrów), szerokie i dobrze oznakowane inwersji: rzadziej, ale bardzo szerokie otwory |
| 432 MHz | 70 cm | Bardziej cieńsze rurociągi: często otwiera się, gdy 144 są tylko "towary" |
| 1296 MHz | 23 cm | Kilka słabych inwersji; długie trasy na morzu często w lecie |
| 10 GHz | 3 cm | Tylko kanał parowania na morzu; również liczy absorpcję deszczu |

Z tego powodu skromny indeks tropu może już oznaczać otwory w UHF i mikrofalówce, podczas gdy 144 wymagają wyższych wartości. Z drugiej strony, przy tym samym kanale 144 mają mniejsze straty poza kanałem i lepiej pokrywają obszary przygraniczne.

## 6. Rozproszenie Troposfera

Nawet bez odwrócenia troposfera nie jest jednolita: Nieprawidłowości w zakresie temperatury i wilgotności rozprzestrzeniają się poza horyzont. Jest to rozprysk troposferowy, zawsze obecny, który umożliwia połączenia 300- 800 km z dobrymi stacjami [6]. Sygnał jest słaby i zmienny; strata rośnie wraz z kątem dyfuzji, czyli z odległością i przeszkodami na horyzoncie:

```math
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
```

z θs kąt dyfuzji w stopniach i f w MHz [6]. Program używa tego wzoru w zakładce każdego miejsca, aby stwierdzić, czy odnośnik może być wyjaśniony za pomocą rozproszenia lub wymaga przewodu (patrz dokument 1). Wysoka wartość N na podłożu poprawia rozproszenie [6].

Nie należy mylić z rozproszeniem powietrza (refleksja na samolotach: sygnały kilku sekund, Doppler) i meteorem rozproszonym (refleksja na meteorytach), które program wyklucza z tropu.

## 7. Obraz pogody: jak go przeczytać na mapie

Otwarcia tropu podążają za meteorologią. Typowe sytuacje [3] [4] [13]:

- Stabilny antycyklin, wysokie ciśnienie (ponad 1020- 1025 hPa), szeroki izobare, mały wiatr: odwrócenie subsidence. Najlepsze otwory są po krawędzi antycykliny, zwłaszcza na morzu.
- Calo della pressione: przerwy inwersji. G4MCU podążał za sygnalizatorem SK6VHF aż do spadku ciśnienia do 1012 hPa [13].
- Gorące powietrze na wysokości (wznoszące się do 850 hPa) nad świeżym powietrzem na ziemi: odwrót.
- Komórki i fale grawitacyjne (np. wiatr podwietrzny w Alpach) mogą tworzyć lokalne i krótkie otwory [4].
- Godzina dnia: odwrócenie się od napromieniowania jest silniejsze wieczorem i wczesnym rankiem; uległość pozostaje również dniem.

Wskaźniki Hepburn [1] i F5LEN [2] połączyły to wszystko z modelami pogodowymi: Obliczają one N w różnych kwotach i przekształcają gradienty w liczbę lub klasę. Nie jestem gwarancją: LA0BY pokazuje zdarzenie LA- UA6, które jest widoczne w wysokiej temperaturze, a nie w Hepburn [3]. W tym celu program pokazuje razem indeksy, izobare, temperatury i rzeczywiste spoty: jeśli plamy wyrównają się z wysokim obszarem indeksu pod antycykliną, otwarcie jest w toku. KP4MD znalazł SNR raportów WSPR na 144 MHz rośnie w prawo w oknach dostarczonych przez Hepburn [14].

## 8. Wykaz praktyczny

1. Spójrz na indeks tropu na twoim obszarze i wzdłuż kierunku, który cię interesuje, przez kilka godzin.
2. Sprawdź izobare: wysokie ciśnienie i szerokie izobare nad lub obok ścieżki.
3. Włącz termalną inwersję i T 850 hPa: gorące powietrze na wysokości nad ścieżką.
4. Preferuj trasy na morzu, wieczorem i godzinach porannych.
5. Jeśli jesteś na wzgórzu, pomyśl o wysokich kanałach; Jeśli jesteś na wybrzeżu, na powierzchni.
6. Najpierw spróbuj najwyższych zakresów (432, 1296): najpierw otwórz.
7. Sprawdzić za pomocą spot i PSKReporter: długie linie w tym samym kierunku potwierdzają kanał.

## Źródła

1. W. R. Hepburn, DX Info Centre, "The Hepburn Tropo Index (HTI)" oraz "Tropospheric Ducting Reconserve" (dxinfocentre.com).
2. F5LEN, prognoza dla Europy i światowej prognozy propagacji tropoferycznej VHF (tropo.f5len.org).
3. S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", 41 ° Nordic VUSHF Meeting, Skjeberg 2019. Normalny gradient i inwersji, temperatura i wilgotność, wysokie ciśnienie, zdarzenie LA- UA6, połączenia poza górami.
4. F. Egano IK3XTV, "Propagation VHF - The convective cells and consumative gravitation waves in tropo propagation". Pierwsze 3 km, ścieżki morskie, komórki konwekcyjne, fale grawitacyjne, wysokie ciśnienie.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, "Very Long Distance Propagation in the 144 MHz Band", 24 / 7 / 2003. Profile temperatury i punktu rosy, inwersja, różnica od E sporadyc.
6. J. N. Gannaway G3YGF, "Tropospheric Scatter Propagation", QST November 1983. Równoważnik promienia Ziemi i utrata rozproszenia.
7. VHF / UHF DX Book, rozdział 2 "Propagacja troposferyczna". Strefy refrakcji zgodnie z dN / dh oraz profilami powierzchniowymi i wysokimi kanałami (tylko liczby).
11. Zalecenie ITU- R P.453, radiowy wskaźnik refrakcji. N wzór i próg kanału przy -157 N / km. Odniesienie ogólne, niezałączone.
13. G4MCU, "troposferic" articles (g4mcu.net), 2018- 2025. Condotti z wysokim ciśnieniem, zwłaszcza na morzu; SK6VHF i zmniejszyć do 1012 hPa.
14. C. F. Milazzo KP4MD, "VHF / UHF WPR Propagation Study", 2012- 2014. SNR WPR przy 144 MHz i prognozie Hepburn.

Numery źródłowe są takie same jak w dokumencie 2, Map Building.
