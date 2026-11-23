# Quellen und Formeln

> Automatische Übersetzung durch Italienisch: im Zweifel ist der italienische Führer wert.

Arbeitssammlung von Quellen, die für die LIVETROPOVHF-Tropoprognose (20.–21. November 2026) eingesehen wurden: Grundlegende Physik, verfügbare Daten, veröffentlichte Indizes, Formeln, Amateurfunkerfahrungen, Validierung und Entscheidungen. Die Formeln sind nummeriert wie in LaTeX, (1), (2),..., und im Text erinnern sie sich mit der Zahl in Klammern. Das Forecast Manual beschreibt, wie das Programm sie verwendet.


## 1. Räumlichkeiten des Autors

IC8TEM -Beobachtungen, die die Arbeit einrahmen, mit Feedback in Quellen.

- Die italienische Geographie fordert Beugung. Mit Graten, Vorgebirge und Inseln ein paar Dutzend Kilometer von den Stationen entfernt, sollten Beugungsbeiträge mit dem Fresnel-Parameter am Hauptrand berechnet werden, nicht nur mit dem glatten Land. Treffen: In einem Vergleich von unregelmäßigem Gelände bei 563 MHz (Rio de Janeiro, 27 Punkte zwischen 5 und 30 km) waren die besten Methoden P.526 mit Klingen, P.1812 und der diffraktive Keil, mit einem durchschnittlichen Fehler zwischen -5 und +3 dB und einer Dispersion von etwa 9 dB; P.1546 war der pessimistischste (von Costa, Rabanal Ramirez und A Für steile Hänge in der Nähe der PETOOL-Station erinnern Sie sich, dass sie Weitwinkelpropagoren dienen und die Zwei-Wege-Berechnung (Welle, die von den Reliefs reflektiert wird).
- Um in einen entfernten Kanal zu gelangen, braucht man Fresnel. Wenn die Leitung jenseits des Horizonts beginnt, kommt das Signal durch Beugung über Hindernisse bis zu ihrem Anfang zu uns; ab 1.3.2 berechnet das Programm es (Prevision Manual, § 10 ter). Treffen: Rohde & Schwarz (2023) stellt fest, dass Stationen entlang eines Hochkanals oft nicht eintreten, während die nahe den Enden ja sind.
- Die Bedingungen ändern sich schnell. Die Theorien sind da, aber die Bedingungen ändern sich in Minuten, und kein Prognosesystem nimmt die Daten in Echtzeit auf: Das GFS kommt alle 6 Stunden mit Zeitspanne und 0,25° heraus, und eine Prognose kann innerhalb kurzer Zeit an Wert verlieren. Treffen: Bray (QST, November 1961) Messung von stündlichen Medianschwankungen von etwa ±7 dB über 100 Meilen; die Kanäle bewegen sich mit der atmosphärischen Zeit (Rohde & Schwarz); unser QSO-Register vom 29.12.2021–2/1/2022 zeigt Öffnungen, die innerhalb von Stunden wachsen und fallen.
- Das beste jetzt-casting bleibt das dx cluster-pskreporter-netzwerk. Um zu wissen, was jetzt passiert, schlagen die Cluster-Spots, PSKReporter (und WSPR) jedes Muster: Sie sind Maßnahmen. Die Prognose sagt, wo zu suchen und wie lange es dauern kann; Werbespots sagen, wenn es ist. Das Programm dafür setzt Spot und Forecast auf die gleiche Karte.

## 2. Zweck

Die Idee ist ein dritter Index, der von LIVETROPOVHF selbst ausgehend von den vertikalen Profilen eines Wettermodells berechnet wird und zu Hepburn und F5LEN hinzugefügt wird, anstatt sie zu ersetzen. Heute liest das Programm ihre Karten bereits fertig: Ein Index würde auch sagen * warum * es Öffnung gibt (Quote und Dicke des Kanals, Mindestband eingeschlossen) und es könnte mit den Flecken auftreten, die das Programm bereits sammelt.

- Was können Sie geben: Wahrscheinlichkeit der Leitung pro Punkt und pro Stunde, Anteil der Basis und Oberseite, Dicke, minimale Frequenz gefangen, und entlang eines Pfades den Bruchteil der Route innerhalb einer Leitung.
- Was nicht geben kann: die dünnen Kanäle (unter wenigen zehn Metern), die die globalen Modelle bei 0,25° nicht lösen, und die Verdunstungskanäle auf dem Meer, die vor allem vom GHz aufwärts dienen.
- Erstes praktisches Ziel: 144 und 432 MHz in Europa, mit Verifizierung an DX-Cluster-Spots, PSKReporter und lokalen Decodierungen.

## 3. Grundlegende Physik

Alles beginnt mit refraktiv N berechnet auf jeder Profilebene: In der Leitung nimmt die modifizierte Reaktivität M ab, d.h. dN/dh fällt unter -157 N/km ([ITU-R P.453-13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)]).

Refraktion (P und in hPa, T in Kelvin):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Dampfdruck aus relativer Luftfeuchtigkeit H (%) und Temperatur t (°C) über Wasser zwischen -40 und +50 °C:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

Geänderte Reaktivität (h in km oder 0,157 · z mit z in Metern) und Kanalkriterium:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN/dh (N/km) | Verordnung | Wirkung von 144 MHz |
| 0 | Teilbrechung | reduzierter Durchfluss |
| von 0 bis -79 | normal (Standard ca. -40) | Funkhorizont mit äquivalentem Radius 4/3 |
| -79 bis -157 | Superbrechung | länglicher Fluss, "zu gut" |
| unter -15 | Leitung (Einfang) | eingeschlossene Signale, DX über 1000 km |

So extrahieren Sie N aus einem Modell bei Druckniveaus: Für jede Ebene, die Sie T, relative Luftfeuchtigkeit und Geopotentialhöhe nehmen, berechnen Sie N und M, dann den Gradienten zwischen benachbarten Ebenen. Die Ebenen unter dem Boden sollten verworfen werden (bei 1000 hPa auf den Alpen ist unterirdisch).

Minimale eingeschlossene Frequenz: Die Leitung führt nur die Wellen relativ kurz im Vergleich zu ihrer Dicke. Hepburn veröffentlicht eine Tischdicke → minimale Nutzfrequenz ([DX Info Center](https://www.dxinfocentre.com/propagation/luf.htm)]); es ist eine weiche Grenze, unter der das Signal allmählich abfällt.

| Led Dicke | Mindestzahlungsfrequenz |
|  61 m  | 962 MHz |
|  91 m  | 464 MHz |
|  122 m  | 276 MHz |
|  152 m  | 185 MHz |
|  183 m  | 133 MHz |
|  213 m  | 101 MHz |
|  305 m  | 53 MHz |

Im VHF-Bereich (oft 120–300 m) folgen die Tabellenwerte recht gut einem Gesetz über Konstante; unter 100 m unterschätzt die Formel die Frequenz bis zu 30–40%. Anpassen selbst (keine von Hepburn veröffentlichte Formel), mit f in MHz und d in Metern:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

In der Praxis: für die 2 m ist eine Leitung oft mindestens 170-180 m, für die 70 cm ist es etwa die Hälfte. Die im Netz zirkulierende Formel λmax = 0,014 · d^1,5 stammt aus Kerr (1951, Kapitel 1, eq. 18) und gilt mit λ in Zentimetern und Stehen für eine Leitung, in die M linear fällt: Mit diesen Einheiten ergeben sich für etwa 2 m 180 m und für etwa 70 cm 89 m, das sind die gleichen Werte der Hepburn-Tabelle, die daraus stammt. Kerr warnt davor, dass es sich nicht um einen Nettoschnitt handelt: Sogar Wellen, die mehrmals länger sind, leiden viel von der Leitung, und ein Weg, der ein wenig "verliert" kann den Empfang sogar mit Stationen aus der Leitung verbessern. Eine IEEE Access-Studie aus dem Jahr 2019 bestätigt, dass klassische Methoden (Kerr, Kukushkin) den tatsächlichen Schnitt nicht beschreiben ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)]): Die Schwelle sollte als Indikation und nicht als Wand verwendet werden.

## 4. Daten kostenlos verfügbar

Der Schwachpunkt ist die vertikale Auflösung: zwischen 1000 und 850 hPa Modelle geben eine Ebene alle 25-50 hPa, das heißt alle 200-450 m, so dass sie die Inversionen zu 500-2000 m gut sehen, aber nicht die dünnen Kanäle in der Nähe des Bodens. LIVETROPOVHF verwendet bereits Open-Meteo bis isobar, Temperatur bis 850 hPa und Inversion: Der Index kann von den gleichen Aufrufen starten.

| Quelle | Was gibt | Resolution | Verwendung für Index |
| [Open-Meteo, GFS](https://open-meteo.com/en/docs/gfs-api)] | T, relative Luftfeuchtigkeit, Taupunkt, Geopotentialhöhe nach Ebenen (1000, 975, 950, 925, 900, 850 hPa...); Höhe der Grenzschicht | 0,25° auf Druckniveaus; 16 Tage, Stunden bis 120 h, alle 6 h aktualisiert | Profil N und M, Steigung, Basis und Oberseite des Kanals; bereits im Programm |
| [Open-Meteo, ECMWF](https://open-meteo.com/en/docs/ecmwf-api)] | gleiche Werte von Open-Data-Modellen 0,25°; Boden gibt Taupunkt |  0,25°  | zweite Meinung, nützlich für die Übereinstimmung zwischen Modellen |
| [Open-Meteo, DWD ICON](https://open-meteo.com/en/docs/dwd-api)] | T und relative Luftfeuchtigkeit auf Druckniveaus; in ICON- D2 gibt es 800, 900 und 925 hPa | ICON-EU ca. 7 km | Dritte Meinung zu Europa |
| [Radiosunds, Univ. Wyoming](https://github.com/NCAR/RAOBget)] | gemessene Profile, zwei Starts pro Tag (00 und 12 UTC), Textformat pro Station | viele signifikante Ebenen, sogar alle paar Dutzend Meter | Indexprüfung; siehe die dünnen Kanäle, die das Modell nicht sieht |
| [GROANS, NOAA NCEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)] | Historisches Archiv des Rundfunks, eine Datei pro Station, mehr abgeleitete Parameter | wie oben angegeben | Kalibrierung auf jahrelange Daten und Vergleich mit vergangenen Spots |
| ERA5 (Copernicus) | Orale Reanalyse seit 1940, 37 Druckniveaus | ca. 28 km | Statistik und Kalibrierung, nicht Prognose |

- Bei Open-Meteo liegt das Geopotential über dem Meeresspiegel: Die unter den Boden fallenden Ebenen müssen verworfen werden ([Open-Meteo, Upper Air](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)].
- Auf dem Boden werden T und Luftfeuchtigkeit bei 2 m hinzugefügt: Sie geben die erste Steigung, wo die Nachtoberflächenkanäle auf der Ebene und auf dem Meer geboren werden.
- Italienische Sendungen im Archiv der Universität Wyoming (weather.uwyo.edu/upperair/sounding.html): Stationen und Nummern WMO in der folgenden Tabelle.

| Bahnhof | W | Breitengrad | Längengrad | Anteil |
| Udine/Campoformido |  16044  |  46,03  |  13,18  |  94 m  |
| Mailand/Linate |  16080  |  45,43  |  9,28  |  103 m  |
| Praxis des Meeres |  16245  |  41,65  |  12,43  |  12 m  |
| Brindisi |  16320  |  40,65  |  17,95  |  10 m  |
| Trapani/Birgi |  16429  |  37,92  |  12,50  | ca. 14 m |

Das Textformat von Wyoming gibt für jedes Niveau Druck, Höhe, Temperatur, Taupunkt und Feuchtigkeit: genug für N und M. Wyoming hat keine offizielle Schnittstelle, Skripte werden als verwendet [RAOBget von NCAR](https://github.com/NCAR/RAOBget); das College of DuPage hält Kopien in Text nach Station (zB 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)).](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. Indizes und veröffentlichte Methoden

Hepburn und F5LEN beginnen mit den gleichen physikalischen Zutaten, veröffentlichen aber nicht den vollständigen Algorithmus; Die wissenschaftlichen Studien von 2008-2026 beschreiben stattdessen die Methode Schritt für Schritt, und von dort aus lohnt es sich, das Rezept zu kopieren.

| Methode | Startdaten | Was berechnet wird | Nützliche Notizen für uns |
| [Hepburn Tropo Index](https://www.dxinfocentre.com/propagation/hti.htm)] | Rohwettermodelle des Canada Meteorological Service (MSC, wie unter seinen Karten geschrieben; nicht GFS / NAM), Karten entworfen mit GrADS, proprietärer Algorithmus ([storia](https://www.dxinfocentre.com/propagation/history.htm)]) | 0-10 lineare Skala der Signalkrümmung: 0 normal, 1-2 Superbrechung, 3-4 einige Kanäle, 5-6 diffuse Kanäle; negative Werte = Subbrechung | die zu imitierende Skala, so dass der dritte Index sofort mit den ersten beiden verglichen wird |
| [F5LEN, Weltprognose](https://tropo.f5len.org/WW/info.php)] | Reaktivität in Einheit N; für Europa das ARPEGE-Modell von Météo-France, 7,5 km, mehr Ebenen zwischen 2 m und 1500 m ([Anmerkung des Autors](https://tropo.f5len.org/author/f5len/)]) | Durchschnittlicher Index für Vermieter, 40 Schritte ab 3 Stunden, 4 Berechnungen pro Tag, 28-Tage-Archiv ([neue Version](https://tropo.f5len.org/?p=504)]) | bestätigen, dass es einem Modell mit vielen niedrigen Niveaus dient |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)] | 60 ECMWF-Modellebenen, vom Boden bis 3000 m | Minimum von dN/dz pro Spalte: durchgeführt unter -157 N/km, Superbrechung unter -78,7 N/km; Basis der Fallenschicht | Klimatologie: Padana Plain den europäischen Raum mit mehr Pipelines das ganze Jahr über; Mittelmeer im Sommer fast 100%, im Winter unter 15%; Italien über 30% in Sommernächten |
| [Indermuehle und Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861)] | GFS 0,25°, 21 Druckniveaus + Felder bei 2 m | M = N + 0,157 z; mindestens dM/dz unter 3 km, Basis und Oberseite des Kanals, Tiefe des M-Defizits; unter 40 m verworfen | 7 Jahre lang auf 25 Kanälen von 88 MHz bis 2,68 GHz verifiziert; gute Kapazität bis etwa 3 Tage im Voraus (AUC etwa 0,74), dann fällt; GFS besser als ERA5 |
| [Hämäläinen et al., IMF 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)] | Harmonie-AROME, 750 m oder 2,5 km, 65 oder 90 Levels | dM/dh < 0 auf dem Modellprofil, Vergleich mit einem Lichtstrahl von 4 bis 59 m | zählt vertikaler als horizontale Auflösung; kein Modell sieht die Kanäle unter 15 m; Feuchtigkeit dominiert den Temperaturgradienten |

Dritte Amateurfunkdienst bereits aktiv: [TROPOCAST.EU](https://tropocast.eu/)von Artur SQ3A, für 2 m und 70 cm in Europa]. Von der Prognoseseite ([tropocast.eu/forecast](https://tropocast.eu/forecast)):

- Daten: DWD ICON-EU (Pass 0.0625°, ca. 7 km), Standardquelle; alternativ kanadisches GDPS (0,15°), weniger detailliert.
- Methode: N und M nach ITU-R P.453 aus dem Profil zwischen der bodennahen Schicht und 850 hPa; die Erkennung der Schichten, die Vereinigung der Kanäle zum Boden und hoch und die Schwellenwerte der Farben sind eine ordnungsgemäße Kalibrierung des Autors, nicht veröffentlicht.
- Produkte: "vollständige" Ansicht 0-3000 m, mehr getrennte Ansichten "zu Boden" und "hoch" (vorerst nur in Karten aufgenommen werden); Punktzahl 0-100 in neun Klassen (unter 38 Hintergrund, 38-52 marginal, 53-58 schwach, 59-65 moderat, 66-72 gut, 73-80 sehr gut, 81-88 hoch, 89-95 stark, 96-100 sehr stark); Vorhersage bis zu +72 h innerhalb einer Stunde, mit Animation und Archiv.
- Gebiete: Europa, Polen, Vereinigtes Königreich, Deutschland, Frankreich, Spanien, Italien, Tschechische Republik, Kroatien; Schnittstelle auch auf Italienisch.
- Wiederverwendung: Karten werden mit einem API Keyless iframe integriert; es gibt auch eine dokumentierte API (OpenAPI und JSON Schema). Die Lizenz ist nicht angegeben: Bevor Sie es als dritte Quelle neben Hepburn und F5LEN verwenden, sollten Sie den Autor fragen.

Unterschiede zu unserem Projekt: TROPOCAST hält bei 850 hPa (ca. 1,5 km), während die erhöhten Kanäle bei 2000 m und darüber hinaus ankommen ( LA0BY Fall von 22/11/2018), und gibt keinen Index für Band oder Route. Die Klassen 0–100 können an denselben Tagen mit unserem Index 0–10 verglichen werden.

TROPOCAST Bildstruktur (aus dem Link vom Autor des Programms zur Verfügung gestellt): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, wobei n der Prognoseschritt ist. Die Website verbietet die automatische Kommissionierung von Robotern, so dass ich das Bild nicht lesen konnte; auch dafür dient die Erlaubnis des Autors, bevor diese Karten in LIVETROPOVHF gezeigt werden.

Notizbuch: [DJ5AR, Kategorie](https://dj5ar.de/?cat=174)(Magonza, 21 Artikel 2017-2023, 2 m–13 cm). Beschriebene Fälle: mehrere Wochen von Irland (23. Mai bis 2. Juni 2023), FT8 nach Belarus (September 2020), Italien-Deutschland über die Alpen und die Adria (August 2017). Erkennen Sie den Fall des "zu hohen" Kanals (umgekehrt zu 1000 m), den Sie vom Boden aus nicht verwenden können: eine weitere Bestätigung, dass es notwendig ist, den Anteil des Kanals mit dem der Stationen zu vergleichen. Es verwendet Radiopharen als Blendenmaß (bis zu 1028 km) und vertikale Profile in Paris, Bordeaux und DWD.

Zusammenfassend das gemeinsame Rezept: vertikales Profil → N und M pro Ebene → Schicht mit dM / dh negativ → Basis, Oberseite, Dicke und Kraft des Kanals → Index. Der "geheime" Teil von Hepburn ist die empirische Kalibrierung auf reale Ereignisse, die wir mit den Spots neu machen können.

## 6. Nützliche Formeln für das Programm

Zusätzlich zu N und M (Basisphysik) dienen diese dazu, sich von der Vertikalen zur Punktzahl und dann über den Pfad zu bewegen. ITU-Formeln wurden durch ein PDF rekonstruiert, das teilweise schlecht gelesen wurde; am 22. November wurden offizielle Texte überprüft ([ITU-R P.452-18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453-14, P.526-16, P.372-18) und im Code korrigiert: Konstante des Troposcatters 190 (nicht 190,1) plus die Absorption von Gasen mit ρ = 3 g/m3; α nicht unter -3,4 (Gl. 55a); Gas der Kanäle mit ρ = 7,5 + 2,5 · ω (Gl. 9a); Beugung auf vollem kugelförmigen Land (Gl. 23-37: mittleres Erdmeer, Höhenfunktion mit Grenze 2 + 20 logs K, Interpolation unter dem optischen Fluss); Dampfsättigungsdruck von P.453-14 (Gl. 9, mit EF) anstelle von Magnus 'alte Formel. Rumore P.372-18: Tabelle 1 und galaktische entsprechen; Künstliche Rauschkurven sind bis zu 250 MHz und die galaktische bis zu etwa 100 MHz, über das Programm erstreckt sich die gleichen Gesetze.

1. Steigung zwischen zwei Ebenen des Modells (h in km, M in Einheit M):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. Leitungsstärke und -dicke: Dicke d = top − base (für einen Bodenkanal, base = soil); Stärke als M-Mangel zwischen base und top.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3. Äquivalentfaktor Erdradius (P.452-18, Gruppierungen 5 und 6a), wobei ΔN = durchschnittlicher N-Abfall in den ersten km, positiv, in N/km:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

Mit ΔN = 40 haben Sie ungefähr 4/3 k (Standardatmosphäre); mit ΔN, das sich 157 k nähert, neigt es zur Unendlichkeit, das heißt, das Erdradio wird flach: Es ist der Anfang des Kanals.

4. Funkhorizont einer hohen Antenne h Meter, mit Radius äquivalent zu\_e in km:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5. Wahrscheinlichkeit des Kanals für Breitengrad (P.452-18, eq. 2), Prozentsatz der Zeit; φ = Breitengrad der Mitte der Route, μ1 und μ4 berücksichtigen, wie weit es auf dem Seeweg ist:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

bei 41° N, ganz auf dem Seeweg (μ1 = μ4 = 1), gibt etwa 11% der Zeit an: ist die klimatologische Basis, nützlich als Referenzwert, von dem der erwartete Index abweicht.

6. Verlust für Troposcatter (P.452-18, eq. 45; f in MHz, d in km, θ Winkel der Bahn in mrad, N0 Bodenreaktivität in der Mitte der Route):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

Der Begriff -0,15 · N0 zeigt, warum Nassluft auch ohne Kanal hilft: 50 zusätzliche N Einheiten sind 7,5 dB weniger als Verlust. LIVETROPOVHF verwendet bereits die G3YGF -Version des Streuverlustes.

7. Kanalverlust (P.452-18, eq. 46-51): fester Kopplungsverlust plus eine Dämpfung für den Winkel milliradiant, der mit der Kubikwurzel der Frequenz wächst (f in GHz, a\_e in km):

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. Bean and Dutton (1966) und Kerr (1951)

Die beiden klassischen Texte geben dem Index drei Dinge, die sofort benötigt werden: Gradientenschwellen, die bereits auf dem gleichen Druckniveau von GFS liegen, was in dB eine Änderung der Reaktivität wert ist und welche Wettersituationen die Kanäle erzeugen oder zerstören. *Radio Meteorology*, NBS Monograph 92.

1. Profilklassifizierung für Druckschichten (Bean and Dutton, Tab. 6.5, S. 248; -dN/dh in N/km). Konzipiert für Radiowellenlängen, gilt es so wie es auf Modellebenen ist:

| Straße | Aktien über | Teilbrechung | nicht laminiert | Superbrechung |
| 1000–850 hPa | 0–1,46 km | -dN/dh < 0 | 20 bis 60 | über 100 |
| 850–700 hPa | 1,46–3,01 km | -dN/dh < 0 | 20 bis 50 | über 80 |
| 700-600 hPa | 3,01–4,20 km | -dN/dh < 0 | 15 bis 40 | über 70 |
| 600-500 hPa | 4,20–5,57 km | -dN/dh < 0 | 10 bis 30 | über 50 |

2. Wie viel ist in dB wert (Bean and Dutton, Cap. 6, S. 233–258), misst auf Strecken jenseits des Horizonts von 70 bis 180 MHz:

- Jede Schicht (auf dem Boden oder in der Höhe) im Vergleich zum "glatten" Profil erhöht das Signal von 10-25 dB auf alle Prozentsätze; der größere Gewinn kommt von den hochgekippten Schichten, wahrscheinlich aufgrund des Fokussierungseffekts (Kurs von 200 km).
- Die Bodenreaktivität Ns und der Rückgang in den ersten km ΔN sind auf das monatliche Mediansignal mit Koeffizienten zwischen 0,4 und 0,95, Median 0,7 (20 Routen von 130 bis 446 km, etwa 100 MHz) bezogen.
- Jährlicher Zyklus: 0,18 dB pro Einheit N; täglicher Zyklus von 0,2 bis 1,1 dB pro Einheit N, maximal auf Strecken von 175-200 km und im Winter.
- Der zusammengesetzte Parameter von Misme schätzt die Feldvariation als 0,5 dB / (N / km) · (äquivalenter Gradient - 40) plus einen Begriff der thermischen Stabilität; der Parameter der Turbulenz D (Bereich zwischen dem realen und dem adiabatischen Profil, 0-3 km) verbessert sich nur im Vergleich zum einfachen ΔN. Fazit der Autoren: ΔN bleibt der beste Parameter für Einfachheit.

3. Referenzprofil und Anomalie (Bean and Dutton, § 3.8 und 3.9). Die exponentielle Atmosphäre der Referenz-CRPL gibt das "normale" Profil ab Ns; Die A-Einheit misst, wie sehr sich das reale Profil bei jeder Aktie unterscheidet. Es ist der natürliche Weg, ein GFS-Profil in eine Anomalie zu verwandeln, die auf die Karte gesetzt wird:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

Mit Ns = 313 haben Sie ΔN etwa -42 N/km, das ist der äquivalente Radius 4/3. Das Leitungskriterium -157 N/km ergibt sich auch hier aus dem Snell-Gesetz für einen horizontalen Radius (Gl. 4.23), nahezu unabhängig von der Antennenhöhe.

4. Wo und wann die Leitungen geboren sind (Bean und Dutton § 4.4; Kerr cap). 3:

| Prozess | Lage | Wirkung |
| Warnung | heiße und trockene Luft von Land, das auf kälterem Meer fließt (typisch für Sommerhülse nach Tagen guten Wetters); zählt hauptsächlich das Feuchtigkeitsdefizit | intensive Leitung auf dem Meer |
| Nachtbestrahlung | ruhiger Himmel, schwacher Wind; niedrige Wolken oder viel Feuchtigkeit verhindern es | führte zu dem Boden, der bei Sonnenuntergang geboren wird, am Abend wächst und nach Sonnenaufgang schnell verschwindet, besonders im Inneren |
| Absenkungen | Luft, die aus dem Antizyklon stammt, erhitzt und trocken, über frische und feuchte Luft; häufige Überschwemmungen in den Bergen und südöstlichen Sektoren des Antizyklons | superbrechende Schicht oder hoher Kanal; fast immer Gradient höher als Standard in umgekehrter Richtung |
| Meer und Land | Küste, Zirkulation von einigen hundert Metern | lokale Pipelines entlang der Küste |
| Gegen die Leitung | Mischen: von der Sonne zwischen morgens und nachmittags erhitzter Boden, Wind über einige m/s auf unwegsamem Gelände; Nachtnebel | Wahrscheinlichkeit der Leitung fast nichts |

5. Zwei neue Zutaten zum Probieren. Die vertikale Bewegung: Aufsteigende Ströme in den Konvergenzzonen entfernen und löschen die Schichten, die Absenkung verstärkt das Signal (Moler und Holden: im Durchschnitt etwa 2,5 dB mehr als bei bergauf gelegener Luft, am Meer in Band L und X). Open-Meteo bietet bereits vertikale Geschwindigkeit auf Druckniveaus. Und der Drucktrend: Auf einem Weg von 140 km bis 186 MHz sank das Signal ab, wenn der Druck auf den Sender und in die Mitte des Weges fiel und kehrte hoch und ohne Evaneszenz zurück, wenn es stieg, mit nur 2 (Bean and Dutton, Abb. 6.9, S. 241).

6. Aus Kerr. Zusätzlich zur Formel der maximalen Wellenlänge (Basisphysik-Abschnitt) gibt Kerr eine ungefähre Formel für M aus der einzigen Temperatur, Dampf und Höhe (§ 3.9, mit den alten 79- und 4800-Konstanten, die dann durch 77,6 und 4810 ersetzt wurden) und beobachtet, dass zur Umkehrung der Windschnitt Wellen erzeugen kann, die in der Höhe des Kanals schwingen, mögliche Ursache für tiefe Fluchten (§ 3.26). Italienische Quelle berichtet von Bean und Dutton und zu suchen: L. Bonavoglia (Dezember 1958), "Korrelation zwischen meteorologischen Phänomenen und Ausbreitung..."

## 8. Aus Amateurfunkpräsentationen und technischen Berichten (zweite Serie)

Sieben Dokumente, die vom Autor des Programms zur Verfügung gestellt wurden. Drei dienen direkt dem Index: W3SZ für den Troposcatter "Boden", LA0BY für Fälle, die Hepburn nicht vorhergesagt hatte, Hepburn ( LU7MC -Übersetzung) für die Wetterklassifizierung der Kanäle.

1. Troposcatter: das Signal, das immer existiert. R. Rehr W3SZ, "Tropospheric Scatter (Troposcatter) Propagation for VHF, UHF, Microwave and Frequencies", Pack Rats Conference 2014; J. R. Duffey KK6MC, "The Troposcatter Propagation Mode and How to Predict Coverage on the VHF/UHF Bands", CSVHF 2017.

- Nützlich von etwa 100 bis 700 km, von 144 MHz bis 10 GHz; der Verlust wächst um 30 Logs f mit der Frequenz.
- Vor allem zählt der Startwinkel zum Horizont: Jeder zusätzliche Grad kostet etwa 9-12 dB ( M1CNK, Collins-Methode).
- Im Sommer ist das Signal im Durchschnitt etwa 10 dB höher als im Winter; am Nachmittag etwa 5 dB niedriger als am frühen Morgen oder am Abend. KK6MC zeigt eine Gesamtvariabilität von 13-15 dB mit Zeit, Saison und Zeit an.
- Yeh-Methode, am einfachsten zu programmieren (f in MHz, d in Meilen, θ Streuwinkel in Grad, α geometrischer Durchschnitt der Öffnungen der beiden Bündel):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Vergleich von Larsen (1968) auf 15 realen Verbindungen: Durchschnittlicher Quadratfehler 5,1 dB für Collins, 7,4 für Yeh, 8.1 für NBS und CCIR, 16,8 für Rider. Alle Methoden verbessern sich, wenn Ns stabil bleibt (Yeh fällt auf 5,4 dB): Für die Streuung zählt die Brechung des Tages wenig, daher muss der Tropoindex die Schichten betrachten, nicht nur Ns. Die Tabelle mit 15 Anschlüssen mit gemessenen Verlusten (einschließlich Feldberg-Monte Baldo, 322 km bei 957 MHz, 223,1 dB; Zypern-Israel, 319 km bei 77,5 MHz, 163,3 dB) ist ein guter Prüfstand für unsere Berechnung.
- KK6MC gibt den Verlust, den eine typische Station auf 2 m und der entsprechenden Entfernung überschreiten kann, von 184 dB (FT-817 und 3 Elemente in SSB, etwa 130 km) bis 215 dB (lange Bungen in SSB, etwa 430 km) und 236 dB (kW und yagi gekoppelt, etwa 640 km). Es ist der Weg, den Index dBs in verdiente Meilen zu übersetzen.
- Ready Code: W3SZ Yeh und Collins Programm (nitehawk.com/w3sz/YehCollins.zip) und Bob Atkins KA1GT Rechner (bobatkins.com/radio/troposcatter.html).

Eine andere Quelle auf der Streuung: I. Roberts ZS6BTE, "Tropospheric Scatter Propagation", *Radio ZS*, Dezember 1984, mit Anmerkung von 2013 ([qsl.net/zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)]. Hauptsächlich Nützliche Geometrie: Streuwinkel θ = θ0 − θ1 − θ2, mit θ0 = d/R, θ1 = (h1 − h\_ostacolo)/d1 + d1/2 R und das gleiche für die andere Station, R = 8446 km (4/3 des Radius der Erde). Es ist das Stück, das verwendet wird, um die Winkel aus dem Profil des Bodens im Troposcatter "Boden" zu berechnen. Andere Daten: jeder Winkel in mehr kostet etwa 10 dB; Übergang von Ns 280 auf 300 reduziert den Verlust von etwa 4 dB; das Streuvolumen liegt zwischen 300 und 2000 m für 150 km, zwischen 600 und 3000 m für 300 km; Die Zeit im Streuvolumen erhöht das Signal von etwa 20 dB. Das numerische Beispiel des Artikels kehrt nicht mit den angegebenen Formeln zurück (der Begriff des freien Raums bei 900 MHz und 872 km ist etwa 150 dB, nicht 125), so dass die Formeln auf W3SZ überprüft werden sollten, bevor sie verwendet werden.

2. Typen der Leitung (W. R. Hepburn, "Tropospheric Propagation Modes", dxinfocentre.com/propagation/tr-modes.htm, spanische Übersetzung von LU7MC). Füllen Sie die Tabelle von Bean und Dutton mit nützlichen Fällen in Italien aus:

| Typ | Wann | Anmerkung zum Index |
| Bestrahlung | ruhige und friedliche Nächte auf Erden | dem Boden folgen, verschwindet kurz nach der Morgendämmerung |
| Hochdruck (Senkung) | Antizyklon | kann den ganzen Tag dauern; nachts kann die Leitung von der Bestrahlung unten entfernte Signale blockieren, so dass die Tageszeit besser werden kann |
| vorn | vor einer Warmfront, hinter einer Kaltfront, nördlich einer stationären Front | Kurz; das schlechte Wetter stört ihn |
| Bergabstieg | föhn, bora: Luft kommt herunter, erwärmt und trocknet | Bora an der Adria und Föhn südlich der Alpen |
| Warnung | heiße und trockene Luft über kalten und nassen Böden, beispielsweise nach Regen | führte zum Boden |
| Tal | Heiße Luft über kalte Luft stoppt im Tal | Kann am Tag bleiben |
| Marino | heiße und trockene Luft auf kälterem Wasser; kommt mit der Brise an die Küste | Höhepunkt am Nachmittag; Frühling und Frühsommer die besten Jahreszeiten |

Die Verdunstungskanäle auf dem Meer (0-30 m) dienen nur etwa 3 GHz und gelangen nicht ins Hinterland: für 144 und 432 MHz können Sie ignorieren. Die hohen Berge sind eine Barriere; Wüsten sind zu trocken.

3. Die Fälle, die Hepburn nicht vorhergesagt hatte (S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", Nordic VUSHF 2019, von JO59IX bis 500 m Höhe):

- 22. November 2018: 70 cm bis zum UA6 (ca. 2600 km) für 6-8 Stunden. Hepburn bezeichnete nur die baltischen Staaten und Polen. Die Leitung war in den Temperaturkarten bei 900 und 1500 m zu sehen und endete knapp über 2000 m; sie war auf der norwegischen Seite höher und nur LA0BY auf dem Hügel trat ein. Auf dem Boden schien die Zeit nicht wie ein Trope: Kälte und Nebel an der Spitze.
- 11. Februar 2015: 2 m und 70 cm in Richtung 9A und S5 (ca. 1500 km) durch die Alpen, auch dies nicht von Hepburn vorgesehen.
- 15. Oktober 2011: 2 m mit EA1 (2135 km) im Hochkanal, auf drei Seestrecken und drei Boden.
- Sonstige Ereignisse: 6-11 November 2003 nach Osten (2050 km), 10. Dezember 2004 nach Frankreich.
- Feldadressen: Nebel in der Ebene, freie Spitzen, wenig Wind, nasser Boden. Normaler Temperaturgradient -6,5 °C/km; im realen Fall ( VK3KAQ) +7 °C in 20 m.

Schlussfolgerung des Autors: Hepburn und F5LEN sind nicht genug, sie dienen den Profilen in der Höhe entlang der Route. Für uns: Diese Daten sind Fälle von Überprüfungen, die mit den historischen Daten der Modelle neu berechnet werden müssen, und der Index auf dem Weg muss das Kontingent des Kanals mit dem Kontingent der Stationen vergleichen.

4. Italienische Erfahrung (G. IV3GBO, "Arten der VHF-Ausbreitung SSB / CW", 2017): zwischen 450 und 3000 km, mit Schichten zwischen 450 und 3000 m; Nebel als Hinweis; Öffnungen auf dem Seeweg in Richtung Apulien, Albanien und Griechenland. Streuen Sie von 100 bis 500 km.

5. Einführungen (P. Wilton M1CNK, "Eine Einführung in die VHF / UHF-Propagation", 2013): 70 cm sind oft besser als 2 m; bei 10 m Pol bei 144 MHz beträgt der Startwinkel etwa 3°, bei 20 m etwa 1,5°.

6. NASA-Bericht (E. Hirschmann, "Tropospheric and Ionospheric Effects on Radio Frequency (VHF-SHF) Communication", Goddard X-731-67-89, 1967): Es betrifft Entfernungsfehler zu Satelliten, nicht den DX. Es bringt die gleiche N-Formel und das exponentielle Profil zurück (z. B. in Florida: Ns 370, ΔN -55, konstant 0,161 / km) und sendet zwei NBS-Quellen zur Suche: Bean und Thayer, "CRPL Exponential Reference Atmosphere", Monograph 4 (1959) und Bean, Horn und Ozanich, "Climatic Charts and Data of Radio Refractive Index for the United States and the World", Monograph 22 (1960).

## 9. Aus dem VHF/UHF DX Buch, Kapitel 2

I. White G3SEK (herausgegeben von), *The VHF/UHF DX Book*, DIR Publishing 1995 (ISBN 0 9520468 0 6; digitale Replik 2017), Kapitel 2 "VHF/UHF Propagation", S. 2-13, 2-22–2-29 und 4-11. Es ist der Text, der dem Funkamateurerlebnis am nächsten kommt; bestätigt die bereits verwendeten Schwellenwerte und fügt neue synoptische Regeln hinzu.

Nummer

- Dieselbe Formel für N (77,6 P/T + 3,73·10^5 e/T2); Beispiel: 1000 hPa, 7 °C und = 10 hPa Schaden N = 324. In einer gut geformten Atmosphäre, zum Beispiel nach einem Gewitter, ist N fast linear etwa 39 Einheiten / km in den ersten 2-3 km.
- Gradienten zwischen 0 und -79 N/km sind normal, unter -79 gibt es Super-Refraktion, über 0 Sub-Refraktion. Das Buch besteht darauf, dass die langen DX nicht von der einfachen Superbrechung stammen, die auf dem gesamten Weg rechts bleiben sollte, sondern von den Kanälen.
- Minimale Kanaldicke, etwas vorsichtiger als Kerr und Hepburn:

| Band | Mindestdicke |
| 50 MHz |  400 m  |
| 70 MHz |  300 m  |
| 144 MHz |  200 m  |
| 432 MHz |  100 m  |
| 1,3 GHz |  50 m  |

- Das Verhältnis ist nicht proportional zur Wellenlänge (etwa 65 λ bei 50 MHz, 220 λ bei 1,3 GHz), da die Ränder der Leitung "gesplittet" sind. In Großbritannien sind die Pipelines häufig auf 144 MHz, aber fast nie bei 70 MHz oder darunter, und oft sind die 432 MHz stark, wenn die 144 schwach sind.
- In der Pipeline fällt das Signal als 1/d anstelle von 1/d2 ab: Bis zu 1000 km kann die Dämpfung der des freien Raums ähneln.
- Kapazität einer Mindeststation auf 2 m (100 W, 15 dBi yagi, 2 dB Lärm): 217 dB, das sind etwa 400-500 km in SSB unter durchschnittlichen Bedingungen; in CW etwa 20 dB in mehr (S. 4-7). Stimmen Sie der KK6MC -Tabelle zu.
- Scatter: Der Verlust steigt um etwa 15 dB durch die dreifache Frequenz und 10 dB für jeden Streuwinkelgrad (Yeh-Formel; im extrahierten Text ist die Konstante unlesbar, daher muss sie aus der Version von W3SZ entnommen werden). Die "on-go" Streuung von konvektiven Zellen deckt routinemäßig bis zu etwa 500 km, 1000 km mit guten Stationen. Nach heftigen Regenfällen und ausgedehnten Bedingungen sind auf allen Bands knapp.

Kanaltypen

- Auf dem Boden: auf der Erde verbreiten sie sich schlecht, weil der Boden absorbiert und sich ausbreitet; auf dem ruhigen Meer kommen sie weit an (Scilly-Canarie, 2500 km, etwa einmal im Jahr auf 2 m). Die Verdampfungskanäle sind oft nur etwa 15 m hoch und dienen Mikrowellen.
- Hoch: zwischen 450 und 2000 m, aufgrund der längeren DX auf dem Boden; das Signal "hoch" über den Stationen unten. Die "verlieren" Kanäle geben weniger starke Signale, sondern auch Zwischendistanzstationen ermöglichen.
- Die Leitungen haben genaue Richtungen und können Stunden oder Tage dauern, im Gegensatz zum sporadischen E.

Neue Synoptic Rules für Index

- Alter des Antizyklons: Die Luft fällt in die Mitte von wenigen Metern pro Stunde und umgekehrt wird abgesenkt, wenn sich der Hochdruck entwickelt; die größeren Öffnungen kommen nach mehreren Tagen der Ausbreitung bereits gut an. Ein kurzfristiger Antizyklon reicht nicht aus.
- Hochdruckkante: Die Schicht ist Kuppel und die Kanten sind niedriger, so dass die Stationen besser werden.
- Richtung: Es wurde beobachtet, dass die längeren Routen entlang der Isobaren oder durch die Dorsale verlaufen (Flavell G3LTP, 1984), aber es gibt ausgezeichnete Verbindungen auch in radialer Richtung vom Zentrum. Beispiel vom 29. bis 30. August 1987: Von England wurde die Öffnung in zwei Tagen um 180° gedreht (Spanien, Frankreich, Italien, Österreich, Norwegen) nach der Verschiebung des Antizyklons.
- Gegen: dick isobar (starker Wind), stratocumuliforme Wolken am Rand der Schicht (begrenzen Sie die höchste Frequenz).
- Fronten: Eine Kaltfront drückt heiße Luft nach oben und erzeugt eine Rückseite entlang der Front; Die beste Richtung ist entlang der Front beim Passieren, aber es dauert wenig.
- Saison: In Großbritannien sind Antizyklone am Ende des Sommers und Frühherbst häufiger.
- Visuelle Hinweise: Rauch, der in einer dunklen Linie über dem Horizont gefangen ist; bei hohem Druck zeigen Schichtansammlungen mit einer Basis unter 1000 m einen Erdkanal, eine Basis über 1000 m einen hohen Kanal an.

Zur Suche zitierte Referenzen: R. G. Flavell G3LTP, "Studies of an Extensive Anticyclonic Propagation Event...", *Radio Communication*, Februar 1984, S. 128; J. Gannaway G3YGF, "Tropospheric Scatter Propagation", *Radio Communication*, September 1981, S. 710; P. W. Sollom G3BGL, "A Little Flutter" W6ZGN

## 10. Ein verifiziertes Amateurfunkmodell: Biancomano und Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, "Practical Tropospheric Model for VHF/UHF Propagation Prediction", *National Weather Digest* 22:2, Juni 1998, S. 15-24, kostenlos in [PDF von der National Weather Association](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf)].

- Daten: Radiosundationen der 00 und 12 UTC von 47 Stationen im Osten der Vereinigten Staaten, 1995-1997; Bodenanalyse bis 700 hPa (am Anfang bis 400), auf dem Profil von N, ohne M. Ausgenommen Küsten, Inseln und Berge.
- Für jede Erhebung: Vorhandensein und Quote des Kanals, maximale Wellenlänge, die führen kann (oft etwa 100 mal die Wellenlänge), maximale Gradient der potenziellen Temperatur zwischen benachbarten Ebenen und zwischen einer Ebene und der zweiten oben (für dickere Schichten). Die Erdungskanäle werden nicht verwendet.
- Erste Regel, Persistenz: Stabilitätsindizes (erhöhter Index, K, Gesamtsummen, SWEAT) bleiben innerhalb von 3 Einheiten aus der Erhebung 12 Stunden zuvor, und das Druckniveau, bei dem die potenzielle Temperatur stärker wächst, bleibt ungefähr gleich. Wenn ja: Verbesserungsprognose in den nächsten 12 Stunden.
- Zweite Regel, dynamische Steuerung: Grenzschicht, die nicht ansteigt, CAPE, Variation der Dicke auf 500 hPa bei drei Erhebungen, Variation der Steigung innerhalb von Grenzen.
- Bandschwelle: Eine Leitung, die 100 cm oder mehr transportieren kann, zeigt die Öffnung auf 432 MHz an, fast sicher auf 222 und oft sogar auf 144.
- Raumfilter: Stationen auf einem hexagonalen Raster von etwa 400-500 km; die Vorhersage ist nur dann wert, wenn mindestens 5 Kontigue-Stationen positiv sind (wenn Sie eine überspringen können). Bei 5 Stationen wird die Wahrscheinlichkeit eines falschen Zufalls positiv vernachlässigbar.
- Überprüfung gegen die Ereignisse der Spalte "Die Welt über 50 MHz" von QST: 1996 48 Veranstaltungen auf 67 (71%) geplant; 12 der 15 Hauptveranstaltungen. Genauigkeit bis zu 70% auf dem 432 MHz und etwa 60% auf dem 144 MHz nach 12 Stunden; es bietet fast das Doppelte der tatsächlichen Ereignisse; 9 Fehler von 19 sind Vorhersagen, die verspätet angekommen sind, weil sich viele Verbesserungen in weniger als 12 Stunden bilden.
- Zu den weiter nördlich gelegenen Stationen hinzugefügt: Wind im Uhrzeigersinn drehendes Klettern (Warmstart) als zusätzliche Bedingung.

Aus dem vollständigen PDF (vom Autor des Programms geliefert) drei weitere Details. Ihre Regel "Dicke = 100 mal Wellenlänge" gibt 600 m für 6 m, 200 m für 2 m, 125 m für 1,25 m und 70 m für 70 cm (Bild 3): für die 70 cm ist weniger streng als Kerr und Hepburn (ca. 89 m), für die 2 m fällt es mit dem VHF / UHF DX Book zusammen. Der Wind, der sich im Uhrzeigersinn dreht, steigt nach Ansicht der Autoren, besonders weil er wärmere Luft in die Höhe bringt, was den Fall von N mit dem Druck wachsen lässt; Die Feuchtigkeit, die damit einhergeht, scheint sich nicht zu destabilisieren. Der Anhang erhält dN/dh in geschlossener Form für eine konstante Wärmegradientenatmosphäre mit einem scharfen Sprung von Wärmegradient und Feuchtigkeit bei einem gegebenen Kontingent: nützlich als Testfall für unseren Code. Ihre Spur der Steigung der potentiellen Temperatur kommt von K. H. Jehn, "The use of potential breaking index in synoptic scale radio meteorology", *J. Meteorology* 17, 1960, S. 264.

Was für unseren Index zu nehmen: (1) ein Begriff der Persistenz: Vergleichen Sie das Profil mit dem von 12 Stunden zuvor, den wir in der Stundenvorhersage frei haben; (2) der Raumfilter: ein Punkt zählt nur, wenn die Verbesserung ein Gebiet von einigen hundert km abdeckt, was Fehlalarme reduziert; (3) Stabilitätsindizes, berechnet von Temperatur und Tau auf 850, 700 und 500 hPa (Open-Meteo gibt bereits einen erhöhten Index und CAPE); (4) der Wind, der sich mit der Höhe zwischen 1000 und 850 hPa dreht; (5) ihre Verifizierungsmethode, die die gleiche ist, wie wir es mit den Programmanzeigen tun wollen.

Eine andere Quelle im selben Paket, von geringem Nutzen für uns: P. K. Rastogi, "Stratosphere and Troposphere (S-T) Studies at Millstone Hill", NASA 1984 (Dokument 19840019069): 440 MHz Radar, das die Turbulenzen der klaren Luft zwischen 4 und 25 km misst, mit dünnen Schichten, die mit Gravitationswellen und Windschnitt verbunden sind. Es bestätigt, dass die Turbulenz, die die Streuung erzeugt, geschichtet ist, aber es betrifft zu hohe Chancen für die Kanäle.

## 11. Dritte Quellenrunde (20. November)

Hochkanal historische Fälle. J. Macoun OK1VR, Links in 2 m vom Gipfel von Sněžka (1602 m), Herbst 1958, Artikel von 1959 übersetzt 2006 ([ok2kkw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)]. Am 27. und 28. Oktober 1958 erste Tschechoslowakei-England-Verbindung auf 145 MHz ( G5YV, 1220 km) und viele mehr zwischen 1100 und 1250 km, mit stabilen Signalen nach 23. Der Autor schreibt es zwei überlagerten Inversionen zu, die obere etwa 2200 m, wobei das Signal nur dort zum Boden zurückkehrt, wo sich die untere Schicht auflöste; Zustand, der durch einen langsamen Antizyklon vom Atlantik bis zum Kontinent begünstigt wird. Von zu Hause (Prag) war sein Maximum 530 km: nur die Stationen in der Höhe betraten den Kanal. Es ist ein weiterer Fall der Überprüfung (wie LA0BY 2018) und bestätigt, dass der Index über 2000 m durchgeführt werden sollte und ihn mit dem Stationsanteil vergleichen sollte.

Historische Bibliotheken NBS (nützlich, um nach klassischen Werken zu suchen, nicht nach Formeln):

- W. Nupen, *Bibliography on Tropospheric Propagation of Radio Waves*, NBS Technical Note 304, 1965, 356 Seiten, mit Index für Themen (auch "Bedingungsprognose") und nach Ländern. Italienische Einträge berichteten: L. Sacco, "The radio connection in atmospheric super-refraction regime", *High Frequency* 24(6), Dezember 1955 (italienische Forschung: super-refraction under 500 m frequent day and night); S. Silleni, *Annali di Geofisica* 8(1), 1955; J. M. Clara e A. Antinori, prova tra Sardegna e Minorca da 238 a 297 MHz (390 km), *Electrical Communication* 33(2), 1956. A. F. Clarke (Fort Huachuca, 1956) gab täglich die Vorhersage der maximalen Wellenlänge, die für den nächsten Morgen gefangen ist, aus Feuchtigkeit, Temperatur, Druck und Windvorhersagen heraus.
- A. L. Abbott, *Bibliography of Tropospheric Radio Wave Scattering*, NBS, Material bis Januar 1960, etwa 670 bestellte Artikel pro Jahr: nützlich für den Troposcatter-Teil.

Offenlegung und Beobachtungen: F.W. Odenthal DK5EQ, "Ausbreitung 2m – Eine Einführung" (31/05/2024): fast optisch bis 200 km, Tropo pro Kanal oder Refraktion darüber hinaus, mit hohem Druck und im Frühjahr und Herbst; zitiert die VHF-Seite von DM2HB und die Notizen von DK5YA auf tropo und Es. A. Johnson VK3DXE auf dem Blog von EI7GL (Dezember 2021): Mit WSPR wurden VHF-Pfade von Tausenden von Kilometern in Australien entdeckt; Die Trope ist nicht nur Sommer. Diskussion über VHF Marina und AIS ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)): Umfang 1,23 · √h (ft) nautische Meilen pro Antenne, Streuung bis zu etwa 50 nm, typische Tropenverbesserung 100-400 nm; die Leitungen werden erkannt, weil die entfernten Stationen so stark sind wie die Nachbarn. Chelmsford ARS News, Dezember 1983: Bei den Wettbewerben in 2 m werden die Stationen der englischen Ostküste durch den Seeweg zum Kontinent bevorzugt.

ARRL, *The Radio Amateur's VHF Manual*, 1972, Cap. 2 "Propagation Beyond the Horizon" (S. 17-19). Bereits bekannte Betriebsspitzen (Folge Hochdruckgebieten, die sich langsam bewegen und vor allem deren Hinterkanten; Frühherbst die beste Zeit; Küstengebiete, große Seen und Täler der durch Schichtung begünstigten Flüsse). Zwei neue Daten: (1) fig. 2-4 vergleicht die Standardatmosphäre (relative Luftfeuchtigkeit 70% bis zu 12000 ft, minimale Refraktion) mit einem typischen Radius der starken Refraktion, mit einem klaren Temperaturbruch und Mischungsverhältnis von etwa 4000 ft (etwa 1200 m): Es ist die Signatur, die unsere Berechnung auf den GFS-Niveaus erkennen muss; (2) echte Kanäle sind selten unter 144 MHz und am häufigsten in UHF, entsprechend minimalen Dicken. Nützliche QST-Bibliographie zur Suche: Hull, "Air-Mass-Bedingungen und das Biegen von UHF-Wellen" (Juni 1935, Mai 1937); Hoisington, "Painless Prediction of Two-Meter Openings" (Oktober 1949); Collier, "Upper-Air Conditions for 2-Meter DX" (September 1955); Botts, "A Night to Remember" (Januar 1970, an Land durchgeführt).

PA0EHG, Seite "Tropo" ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): Zusammenfassung des Troposcatters aus dem G3YGF -Artikel, bereits im Handbuch. Verwenden Sie zwei praktische Regeln, um unsere Streuberechnung zu steuern: etwa 10 dB zusätzlicher Verlust für jeden Winkel am Horizont jeder Station und über 100 km etwa 9 dB in mehr alle 100 km Route (60 Meilen); Das gemeinsame Volumen beginnt bei etwa 600 m auf 100 km und von etwa 9 km auf 480 km. Es behandelt nicht Verhalten, Umfragen oder Brechungsschwellen.

PA0EHG, PROPBER Rechner ([Seite](https://pa0ehg.com/propagat1.htm), propber.exe Programm vom Autor des Programms zur Verfügung gestellt). Es ist ein 36 kB DOS-Programm, das in BASIC geschrieben und mit dem Microsoft BASIC Compiler 5.60 (1985) kompiliert wurde: Es läuft nur unter DOS oder DOSBox. Von den internen Texten: fragt nach Frequenz, Abstand (oder die beiden Locatoren, auch im alten QRA-Format "E-Locator"), für jede Antennenhöhenstation, Rauschzahl, FI-Band, Leistung, ein Hindernis (Höhe und Abstand) und Antennengewinn (0 = gleichbedeutende Durchmesserberechnung, Effizienz 50%). Gibt den maximalen Abstand in der Optik zurück, den korrekten optischen Verlust für die Absorption von Dampf (0,1 dB/km von 24 GHz), den Verlust von Troposcatter "richtig für die Standorte" (dh für die Horizontwinkel, die durch Hindernisse gegeben werden) und das S / N-Verhältnis in den beiden Sinnen, auch "mit der maximalen abnormalen Ausbreitung". Numerische Konstanten sind in der kompilierten ausführbaren Datei nicht lesbar. Die vorgeschlagene Methode für hohe Bänder ist auch für uns nützlich: Kalibrierung der Berechnung auf einem gemessenen Link (Differenz zwischen gemessenen und berechneten S / N) und Anwendung der gleichen Differenz auf ein anderes Band. Es ist das gleiche Muster unserer Berechnung, aber es verwendet das S RTM1 -Profil anstelle von nur einem Hindernis und die GFS-Prognose anstelle einer festen Marge.

PROPBER, Konstanten, die in der ausführbaren Datei gelesen werden (Zahlen im Microsoft Binary Format). πr, in der Datentabelle neben den Texten: 8,68589 (= 20/ln 10, d. h. 20 logs berechnet mit dem natürlichen Logarithmus), 32,5 (freier Speicherplatz: 32,5 + 20 log f\[MHz\] + 20 log d\[km\]), 24000 und 0,1 plus 40000 und 0,3 (Dampf: 0,1 dB/km von 24 GHz, 0,3 dB/km von 40 GHz) Wahrscheinlich, weil Sie es aber im Code wiederholen: -69,5 (derselbe konstante Begriff wie Yehs Seitenstreuformel in VHF / UHF DX Book), 332 (Bodenbrechung), 8733 km (äquivalenter Radius, k ≈ 1,37). Die genaue Streuformel ist nicht rekonstruierbar, ohne das Programm zu zerlegen. T0

PROPBER, durch den Code rekonstruierte Formeln (Demontage des kompilierten Teils; die Operationen werden in die BASIC-Bibliothek mit den Operatoren im Speicher aufgerufen, deren Werte in doppelter Präzision decodiert wurden). Die früher gefundenen Konstanten -69,5, 332 und 8733 waren innerhalb der Bibliothek falsch positiv.

- Optischer Abstand: d = 5 · (√h1 + √h2) km (h in m). Es ist großzügiger als das klassische 4,12 (k = 4/3): entspricht k ≈ 2.
- Optikverlust: L = 32,5 + 20 log f\[MHz\] + 20 log d\[km\] plus 0,1 dB/km über 24 GHz und 0,3 dB/km über 40 GHz.
- Troposcatter: Lts = L + 23 + 10 log f + 0.0674·d, d. h. 55,5 + 30 log f + 20 log d + 0.0674·d (Formel von G3YGF mit der Konstante 2 dB höher). Kein Winkelterm oder Reaktivität.
- Korrektur "für die Sites": Das Programm fragt Hindernis und Entfernung, aber der Block, der sie verwenden soll, ist leer; Die Korrekturvariable bleibt 0, so dass der "richtige" Verlust gleich dem der Streuung ist.
- Lärm: T = 290·(10^(NF/10) − 1), N = -228,6 + 10 log T + 10 log B (dBW), ohne Antennentemperatur.
- S/N = 10 logs P + G\_tx − L + G\_rx − N. "Mit maximaler anomaler Ausbreitung" verwendet optischen Verlust anstelle von Streuung, d.h. das ideale Leitungsgehäuse ohne Verluste über den freien Raum hinaus.

Vergleich: 300 km und 144 MHz PROPBER ergeben ca. 189,9 dB, G3YGF 187,9, unseren P.452 187,8 (mit Null-Horizontwinkeln): Die Streuung fällt innerhalb von 2 dB zusammen. Unsere Berechnung addiert die Horizontwinkel aus dem Profil, die Antennentemperatur, und anstelle des "Maximums" fixiert die erwartete Schicht mit Restpaarung und Dämpfung.

Rohde & Schwarz, P. Denisowski, "Understanding VHF Propagation", Educational Note, April 2023 (24 Seiten, Verbreitung). Bestätigt die bereits gesammelten Punkte (durchgeführt als zweidimensionaler Wellenleiter, Signal, das linear mit der Entfernung abfällt, bis zu 1500 km und darüber hinaus, Dauer von Stunden bis Tagen; guter Boden am Meer und knapp an Land; erhöht von einigen hundert Metern auf einige Kilometer). Zwei nützliche Beobachtungen für den Index: (1) Entwicklung in der Zeit: Eine neu gebildete Leitung trägt oft nur die höchsten Frequenzen und, Verdickung, beginnt, sogar die niedrigsten zu bringen; die stündliche Vorhersage sollte daher die 432 MHz "im Voraus" auf den 144 MHz zeigen, wenn die Schicht wächst; (2) Stationen Lage: die besten Bedingungen sind zwischen Stationen in der Nähe der Enden der Leitung, während diejenigen, die auf dem Weg einer hohen Leitung oft nicht eintreten, es sei denn, sie werden "verlieren" geführt. Die Kanäle bewegen sich mit atmosphärischem Wetter.

Site PA0EHG, andere Seiten. Für den Tropus PathProfile von M. Willis G0MJW ([Scheda](https://pa0ehg.com/srtmp.htm), [Download](http://www.mike-willis.com/Software/ASTERPathprofile.zip)]): Profil der Route aus erwarteten Totalverlust- und Quotendateien, die für den Troposcatter in der Mikrowelle als zuverlässig beurteilt werden; behandelt nicht die Streuung von Flugzeugen oder Regen. Nützlich als zweite Referenz, um unsere S RTM1 Profile und Verluste zu vergleichen. Der Rest der Website umfasst EME, Mikrowelle, Landebahn (AirScout, SM7LCB -Karten) und Maßnahmen in 80 m.

PathProfile von G0MJW (Mike Willis), ASTER- und SRTM-Pakete und SiteData (vom Autor des Programms bereitgestellt; [](https://www.mike-willis.com/software.html)-Softwareseite ]. Windows-Programm von 2018: Profil von ASTER oder SRTM bis 1" oder 3", Verluste in Optik, Beugung, Troposcatter (gültig bis 1000 km), Kanal und Gas, mit Prozentsatz der Zeit, k Median der Route, Bruchteil des Weges auf dem Meer und Entfernung von der Küste: Die Struktur ist die von P.452/P.1812, aber die Hilfe (komprimierte.chm-Datei) konnte hier nicht gelesen werden. SiteData konvertiert die Locatoren und gibt den Anteil von GT OPO30.

Der nützlichste Teil sind die Datendateien enthalten: ITU-R P.453 statistische Karten auf dem globalen Raster von 1,5° (121 Zeilen von 90°N bis 90°S, 241 Spalten von 0° nach Osten): Gradient dN / dz in den ersten 65 m für 1, 10, 50, 90 und 99% der Zeit überschritten; für Bodenkanäle Prozentsatz des Vorhandenseins, Dicke und Intensität; für diese hohe Anwesenheit, Basenanteil, Dicke, Intensität und maximale Höhe; plus der nasse Begriff und Temperatur. Interpolierte Jahreswerte:

| Standort | Landpipeline: % / Dicke / Intensität | Hohe Pipeline: % / Basis / Dicke / Intensität | dN/dz überschritten 10% der Zeit |
| Neapel |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | -380 N/km |
| Tyrrhenisch (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | -674 N/km |
| Trapani |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | -520 N/km |
| Brindisi |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | -405 N/km |
| Adria (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | -399 N/km |
| Mailand |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | -172 N/km |
| Udin |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | -176 N/km |

Für unseren Index gibt es eine fertige klimatische Referenz: Dicke und Intensität typisch für die Kanäle anstelle der 50 m des potenziellen Kanals, und eine Anomalie "heute im Vergleich zu normal" pro Zone. ITU-Daten: kostenlos für nicht-kommerzielle Zwecke, um sie zu überprüfen, bevor sie in eine öffentliche Version aufgenommen werden. Die ZO NES01.DEM-Datei (58 MB, 2'-Raster) enthält die Klimazonen Binnen/Küste/Meer; das Format ist nicht dokumentiert und ich habe es nicht entschlüsselt. Willis Gleichungsseite enthält nur physikalische Konstanten, freien Raum und Antennengewinn.

Vierte Quellenrunde (20. November).

Indermuehle und Suzuki 2026, Vorhersage der GFS-Pipelines für das CSIRO-Observatorium von Murchison (arXiv 2608.12861): Es ist die nächste Quelle von LIVETROPOVHF. Profile von N vom GFS bis 0,25° (Oberfläche bis 2 m, dann die isobaren Ebenen, diejenigen unter dem weggeworfenen Boden); für jede minimale Spalte von dM/dz unter 3 km, Basis und Oberseite der Leitung, integriertes M-Defizit; dann P.452-16 und P.1812-6 mit GLO-90 25 Routen von 98 bis 1005 km, von 88 MHz bis 2,68 GHz jede Stunde. Ergebnisse: Median AUC 0,72 auf 25 Kanälen; der P.452 p-Zeitprozentsatz sollte als Kalibrationskonstante verwendet werden (Wahl p = 20%), da das ITU-Modell mit einem Zeitpunkt ΔN nicht innerhalb einer Leitung definiert ist; Geschick bleibt bis 3 Tage im Voraus fast flach. Bei 162 MHz (AIS) haben die empfangenen Routen den höchsten Kanalanteil von 67% (0,26 gegenüber 0,16) und die höchste Kanaltiefe von 49% (28 vs. 19 M) in Bezug auf Kontrollen: Auf dem Meer ist die Leitung fast immer, entscheidet die Landstrecke. Die GFS sieht die Hälfte der Zeit am Boden im Vergleich zu ERA5 (4,5% gegenüber 10,4% der Zellstunden), während auf dem Meer mehr sieht (73% gegenüber 56%). Ein "Korridor"-Index (kontinuierliche Rohrleitungen entlang der Route, Schichten mit einer Mindestdicke von 40 m) verbessert das Ergebnis: bestätigt, dass die Kontinuität entlang der Route das richtige Kriterium ist.

Longley and Rice, ESSA ERL 79-ITS 67 (1968): Methode für unregelmäßiges Gelände von 20 MHz bis 20 GHz (nach ITM) mit Bodenunregelmäßigkeiten Δh, Ns von 250 bis 400, äquivalenter Radius 8497 km (4/3), Horizonte, Beugung, Streuung nach vorne und Variabilität in der Zeit und an Orten für das Klima. Statistische Referenz, nicht sofort.

ITU-R P.834-2: Radiuskrümmung, Erdradiusäquivalent k·a, M = N + h/a (h in m, in Tausenden von km), Exponentialmodell; nichts Neues als das bereits verwendete.

AGARD CP-70 (1970), troposferische Ausbreitung: Dünne Schichten von 1 bis 100 m mit starken Gradienten sind üblich; der Reflexionskoeffizient einer hohen Schicht ist etwa proportional zur Wellenlänge, mit einer zufälligen Dispersion von etwa 14 dB. Maßnahmen bis 90 MHz: Abnahme von 9-10 dB, die an einen N-Sprung gebunden sind, der auf etwa 350 m an der Spitze einer Wolkenschicht konzentriert ist. Der Durchgang einer Kaltfront eliminiert die Schichten: bleibt die Beugung.

U.S. URSI Report, Commission 2 (1960): Average thickness of the 60 to 210 m high layers (geben in Übereinstimmung mit P.453), minimum gradient for the 157-160 N/km duct.

Buscemi K2OVS, VHF-Ausbreitungsführung: typische Abstände für Umkehr und Kanal für Band, Kanäle verbunden mit hohem stationären Druck, maximal bei Tagesanbruch.

Wolinsky-Mancini und andere 2024, AIS auf den Kanarischen Inseln: AIS-Radio und -Empfang; Die Kanäle sind in den warmen Monaten viel häufiger (Inversion der Alisäer).

Nigerianische und malaysische Artikel (Ekah, Iloke, Mat, Igwe): lokale Korrelationen zwischen Temperatur, Feuchtigkeit und gemessenem Verlust, schwach und widersprüchlich (r -0,09 bis 0,27); nützlich nur als Bestätigung, dass die Bodengrößen allein nicht ausreichen.

Herben 1984, These TU Eindhoven (Mikroonde): 30 GHz auf 8,2 km und OTS Satellit auf 11,6 GHz; Funkeln für Turbulenzen, Mehrwege und Regen. Für Mikrowellen werden dynamische Verluste von Regen und Turbulenzen dominiert, nicht von Kanälen.

Baars, The Paraboloidal Reflector Antenna (microonde): Verstärkung einer Parabel G = η·(πD/λ)2 mit typischem Wirkungsgrad 0,5–0,65; Verlust von Oberflächenfehlern nach Ruze, ηs = exp(-4πε/λ)2). Es würde helfen, wenn wir die Gleichnisse als Alternative zu Yagi DL6WU für 1296 MHz und darüber hinaus hinzufügen würden.

Bereits analysiert oder von schlechtem Inhalt: Hirschmann NASA X-731-67-89 (doppelt), Reids VHF ST Radar (Winde und Turbulenzen, nicht durchgeführt), Scribd Dia und generische Notizen auf der Troposphäre.

Fünfte Runde der Quellen (20. November, Abend).

Rogers 1997, Radio Science 32(1): Bayessche Schätzung der Parameter eines hohen Kanals (Basisrate, M-Defizit, Dicke) aus Ausbreitungsfaktoren, die jenseits des Horizonts mit einem parabolischen Gleichungsmodell gemessen wurden; VOCAR-Experiment in Kalifornien bei 143, 263 und 375 MHz, Vergleich mit Radiowellen. Das Signal hängt stark von der Basis und dem M-Defizit (korrigiert 0,81 bis 143 MHz für das Defizit), fast überhaupt von der Dicke (-0,17): Die Dicke kann aus dem Signal nicht gewonnen werden. Für uns: Berechnung B (Klimadicke) zählt weniger als Defizit und Anteil; und die Methode ist die Grundlage für das Back Office, das heißt, die Leitung aus den mehreren Frequenzen zu erhalten.

Moler and Holden 1960, "Tropospheric scatter propagation and atmospheric circulations", J. Res. NBS 64D, S. 81 und folgende (lesen Sie im Original am 22/11/2026, doi 10.6028/jres.064d.014). Kalifornien Seerouten nach Point Loma (San Diego): Santa Barbara 190 Meilen, Point Mugu 144, San Clemente 78; 1365 MHz und 9380 MHz, nicht VHF; Januar-April 1958, durchschnittlich 1 Minute. Der Fall vom 26. März: eine Verstärkung von etwa 20 dB für etwa 3 Stunden gegen Mittag, wenn ein zyklonischer Wirbel schwächer wird und Seebrise und Absenkung beginnt; Bei 15 wird die Konvergenz reformiert und das Signal fällt etwa 22 dB vom Maximum ab. Statistik (Tabelle 1, 500 hPa, 33 Fälle): Bei fallender Luft ist das durchschnittliche Signal höher als etwa 2,5 dB als bei steigender Luft im L- und X-Band. Das Signal folgt der auf 500 hPa berechneten Differenz in Vorzeichen und Amplitude der Variationen, ohne Korrelationskoeffizient. Niedrige Schichten (unter etwa 600 m) begünstigen die Teilreflexion bei der Streuung. Die Antizyklone und Fronten werden nicht behandelt, und die oben genannten "Exkursionen bis 60 dB" sind nicht: Sie stammen aus einer Zusammenfassung aus zweiter Hand. Für uns ist die vertikale Bewegung ein guter Prädiktor, und +1,5 dB für die Senkung und -2 dB für die bergauf gelegene Luft sind in der gleichen Reihenfolge wie die durchschnittliche gemessene Differenz (2,5 dB); 20 dB sind ein Einzelfall, kein typischer Wert, und auf Mikrowelle.

Banafaa und Muqaibel 2025, IEEE Access: Vermessung der Pipelines (Verdunstungsmodelle, Parabolgleichung, Radarschätzungen, GNSS und Lidar) und Klassifikation mit automatischem Lernen zu realen Maßnahmen; Supportvektormaschinen liefern die besten Ergebnisse. Nützlich als Bibliographie; die Daten sind vom Persischen Golf.

ITU-R P.834-7 (2015): aktualisierte Fassung der Empfehlung zur troposferalen Refraktion; keine neue Formel als die bereits verwendeten.

Abbott, NBS-Bibliographie zur troposferösen Streuung (GOVPUB- C13): bereits in der dritten Runde. Slide Scribd auf den Kanälen: generische Inhalte. Der Artikel ScienceDirect 0021916987900304 konnte nicht geöffnet werden.

Sechste Runde (20. November, Nacht): Bean and Dutton, ARRL Handbook 2022 and 2023, Balanis.

Bean and Dutton, Radio Meteorology (Cap. 6), Teile noch nicht verwendet. Feldempfindlichkeit gegenüber Ns: 0,18 dB pro Einheit N im Jahreszyklus; 0,14–0,24 dB/N im Tageszyklus (maximal am frühen Morgen), bis zu 0,2–1,1 dB/N auf den Strecken von 175–200 km im Winter; Korrekturen 0,8–0,95. Parameter bestehend aus Misme: M = a(g\_e − 40 + b·ΔW^n) mit a = 0,5 dB pro N/km, der die äquivalente Steigung und die thermische Stabilität ΔW kombiniert (Arbeit, um die Luft von 1 km anzuheben): In der Sahara bei 471 MHz auf 160 km beträgt die einzige Stabilität 20 dB zwischen Januar und Juni bei fast gleicher Steigung. Potentialrefraktionsindex K (N adiabatisch auf 1000 hPa angegeben): hebt die Luftmassen und die antizyklonische Absenkung (Flavell und Lane) hervor.

ARRL Handbuch 2022 (99a ed.), § 19.4.3–19.4.4. Nützliche Inversionen zwischen 250 und 2000 m; Inversionen der Strahlung (Sera, ruhige Luft, trockener Boden) führen selten wahr; langsame Antizyklone geben die umfangreichsten Kanäle pro Absenkung, mit Inversionen zwischen 500 und 3000 m, stärker am Abend und am Morgen; in der nördlichen Hemisphäre sind die besten Routen südlich des hohen Zentrums. Heiße Front: gute Bedingungen ein paar hundert km voraus; Kalte Front: starke, aber instabile Rückseite hinter der Front, parallel. Foehn (auch in den Alpen) und Erdbrise (kühle Luft auf dem Boden, heiß auf 200-300 m; erstreckt sich über 800 km entlang der Küste). Scirocco: heiße und trockene Luft aus der Sahara über kühlere Meeresluft, sehr starke Inversionen vom östlichen Mittelmeer nach Gibraltar, wahrscheinliche Ursache für Aufzeichnungen über 1500 km in der Mikrowelle. Troposcatter: theoretische Grenze etwa 800 km, maximale typische Hälfte. Die 100. Aufl. (2023) hat einen kürzeren Text: Inversionen bei Sonnenaufgang und Sonnenuntergang bis zu +20 dB und +20-50% Flussrate; 1200 Meilen auf den Großen Seen, typisch 400-600; empfiehlt Radio Mobile (ITM-Modell) für Hepburn Abdeckung und Karten.

Balanis, Antennentheorie (4a ed.), § 10.3.3: Viezbicke-Tabelle (NBS TN 688, 1976) für Yagi optimiert bei 400 MHz: Ausleger 0.4 / 0.8 / 1.2 / 2.2 / 3.2 / 4.2 / 4.2 λ → 7.1 / 9,2 / 10.2 / 12.25 / 13,4 / 14.2 dBd. Unsere DL6WU -Formel (8.6 + 8 log10 Ausleger / λ) gibt 1-1.7 dB weniger zwischen 0.8 und 4.2 λ und kehrt im Einklang mit den DL6WU -Tabellen zu 9.5 λ zurück: vorsichtige Schätzung. Mit der richtigen Formel auf dem Original (9,4 + 7,8 log10 boom/λ, ab 22. November) fällt der Abwurf unter 0,2 dB zwischen 1,2 und 4,2 λ (12,1 dBd bis 2,2 λ gegenüber 12,25) und bleibt 0,6 dB bis 0,8 λ.

Siebte Runde (21. November): QST, QEX, W3SZ, GitHub Repository.

QST 2000-2025 (14 Zahlen, 10 Betten mit Texterkennung). Nur Berichte über die Welt über 50 MHz, nützlich als Fälle, um das Modell zu validieren: September 5-9, 2000, durchgeführt bei 144 MHz und darüber hinaus auf der östlichen Hälfte der USA; 14-15 August 2022, Eröffnung Manitoba-Kansas / Missouri unter hohem Druck an einem heißen Tag, von 144 bis 1296 MHz fast alles in FT8, kanadische Aufzeichnung bei 10 GHz von 1228,56 km ( VE4MA - N0OY) in Q65, Signale bis 1941 km; APRS Ausbreitungskarte zeigte den gleichen Bereich. QST November 2022 empfiehlt Hepburn Karten. Kein technischer Artikel über die Tropo-Prognose.

QEX und K1JT, Franke und Taylor (QEX 2020) auf FT4/FT8 -Modi: FT8 -Dekodierungsschwellen -20,8 dB in 2500 Hz auf Kanal ohne Evaneszenz, -18,6 dB mit Evaneszenz in mittleren Breiten; FT4 -17,5 dB. Die anderen Zahlen von QEX 2023 und 2026 behandeln das Tropo nicht (drei Zahlen 2023 sind ungelesene Scans). W3SZ 2019: Schwellen für den langsamen Modus ( JT4A -23, JT9A -27, JT65A -25, QRA64A -26, FT8 -21 dB) und Auswahlkriterien zwischen Empfindlichkeit und Frequenzverschiebungstoleranz.

GitHub. TropoLink (LukiBox, Apache-2.0): Design von Troposcatter-Routen mit P.617-5 (mit N0 und ΔN), NBS TN101 und ITM flankiert, und ihre Differenz als Unsicherheit dargestellt; P.617-5: L = F + 22 log f + 35 log θ + 17 log d + Lc, F = 0,18 · N0 · e^(-hs/7,35) − 0,23 · ΔN, Lc = 0,07 · e^(0,055(Gt+Gr)). Andi-f/troposcatter ( DL2FZN 2013, aus dem VHF/UHF-Handbuch RSGB): Lts = 24,44 + 4,283 ln f + 0.06656 d über freiem Raum, Steigung in der Nähe von PROPBER (0,0674 dB/km). QRadioPredict: ITM und ITWOM 3.0 mit S RTM3, Bodennutzung Corins und NEC Diagramme. Nicht relevant: Tropos (Wetter-App), opera-adt/tropo und troposim (Troposferic delay for InSAR), tgbot\_troposcatter (Militärstation), sdr-troposcatter-gui (Kontrollfeld), Troposcatter-Propagation-Delay ( ERA5 -Verzögerung), Radio-Communication-Project (didaktischer Simulator), PyLayers (Kanal).

Achte Runde (21. November): NTIA und ITU Modelle auf GitHub. NTIA/itm (Longley-Rice, ITS): 20 MHz–20 GHz, freier Raumverlust, Beugung und Troposcatter mit Bodenprofil, 0,5–3000 m Terminals, Oberflächenreaktivität N0 (Minimität reduziert auf Meeresspiegel, 250–400), sieben Klimazonen, Bodenkonstanten und verschiedene Modi 1 Ansicht, 2 Beugung, 3 Troposcatter; C++ mit.NET Framework 4.7.2 Gehäuse. Es modelliert die Kanäle nicht: Es dient als Referenz "normale Atmosphäre" mit statistischer Variabilität. NTIA/p528 und eeveetza/p528 (MATLAB): ITU-R P.528-5 für Luft-Boden-Verbindungen, 100 MHz–30 GHz, 1,5–20 000 m Terminals, 1–99 %;.NET 4.8 Gehäuse. Mögliche Verwendung: Luftstreuung (zwei Bodenstrecken – Luftfahrzeuge plus Radarstrecke). eeveetza/p2001 (MATLAB): ITU-R P.2001-6, Vollfeldmodell mit Beugung, Troposcatter, abnormer Ausbreitung (Leiter und Schichten, Lba) und E sporadisch, prozentualer Zeitanteil 0–100%; digitale ITU-Karten (DN\_Median, DN\_SupSlope, DN\_SubSlope, dndz\_01, TropoClim, h0, surfwv, Es) sind nicht weiterverteilbar. Mögliche Verwendung als ITU-Vergleich anstelle von P.452, mit dem GFS ΔN anstelle von Karten wie in der CSIRO-Studie; die Karten sollten vom Benutzer gemäß P.453 heruntergeladen werden. Validierung 1.3.0 von 593 QSO (29/12/2021–2/1/2022): Ereignis-Vergleichs-Trennung 0,79 ohne Korrekturen, 0,91 mit Misme und Absenkungen; die GFS sieht die Leitung auf dem Meer bei 144 MHz nicht (guadagno ohne Korrekturen nie mehr als 15 dB).

Neunte Runde (21. November): IU3QEZ, NTIA, PyWaveProp, ACE High, Validierung 1.3.1. IU3QEZ (S. Fabris), Präsentation "Über den Horizont in VHF" (GEKO Magazine n. 69, September 2026): gemeinsames Volumen d2/(8·R\_eff) mit R\_eff ≈ 8500 km (1,3 km bis 300 km, 3,7 km bis 500 km jede Ebene des Horizonts im Wert von 69 Meilen (111 km), Marge von 7 dB für Evaneszenz und 4 dB für Bodenreflexion; These: 10 dB ist etwa 100 km; Station IQ3QC 5 W mit 3 × 5 Elementen, 120 QSO, maximal 788 km (Alpe Adria 2026). Bibliographie der Präsentation, die wiederhergestellt werden soll: Norton 1959 (J. Res. NBS 63D, System Loss), Bray 1961, ARRL Antenna Book 2023 Abb. 4.29, Rice-Longley-Norton-Barsis NBS TN 101, Bean and Thayer 1959, Barsis and Kirby 1961 (Diffration on ridge), Deygout 1966, P.1812, AirScout DL2ALF, P.834 NTIA PropLib (wiki): eHata, ITM, LFMF, P.528, P.2108 (Verlust durch Belastung), mit C++,.NET, MATLAB und Python; kein Leitungsmodell; ITS-Seite: auch IF-77 (Luft-Boden) und MPM (1 GHz-1 THz). PyWaveProp (Python): Parabolische Gleichungsmethoden mit Padé-Approximationen, multiple Kantenbeugung, PETOOL- und RAM-Gehäuse, troposferöse und hohe Kanäle, unregelmäßiger Boden: mögliche Basis für eine Feldberechnung mit dem zu prüfenden GFS M(z-Profil). APRS PropView (Apache 2.0): VHF-Ausbreitungskarten aus APRS-Paketen, stündliche Statistiken und Öffnungsalarme; mögliche Quelle von 144 MHz-Daten für die Validierung. NATO-Troposcatternetz in Italien (fortifications.net): 900 MHz, 10 kW, 20 m Parabeln; Stationen einschließlich Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Vulture, Monte Lauro; historischer Testfall für P.617. Nicht relevant: Gupta 2021 (Stratosphärischer Transport), Gesetz 2023 (Arktisches Ozon), Delfin #590 und Opera-adt/tropo (InSAR-Verzögerung), tropo.ino (Arduino-Gassensoren), tropo-webapi (Telefonie), Cloudtools/Troposphäre (AWS). Validierung 1.3.1 auf 564 QSO tropo: AUC 0,79 (Modell 1.2), 0,80 (+ weicher Kanal), 0,90 (+ misme und Absenkung), 0,90 (+ Persistenz); Kanalfarben (≥ 16 dB) auf 46% der Öffnungswege und 0% des Vergleichs.

Zu derselben Runde hinzugefügt. Mikelytaev/Wellenausbreitung (MIT, Python 3): Parabolische Gleichung mit Näherungswerten von Padé und Numerov, unregelmäßigem Boden, troposphärischen Kanälen; Ladung des modifizierten refraktiven M(quota, lat, lon) aus dem NOAA GFS und Einstellung eines Verdampfungskanals von Monin-Obukhov; keine dokumentierte Funktion, die den Verlust auf dem Weg zurückgibt. Er ist der beste Kandidat für eine zukünftige Feldberechnung mit dem GFS M-Profil (es würde einem externen Dienst oder einer Übersetzung dienen). RP-Sim (AGPL-3.0, JavaScript): Empirische Modelle für Funknetze (Hata, He, Ericsson, zwei Strahlen), kein Tropo. RIMFading (GPL-3.0, OMNeT++): Flussunregelmäßigkeiten für Netzwerksimulationen, Weibull-Verteilung, kein Tropo. PyLayers: Indoor-Radiokanal, bereits ausgeschlossen. qradiopredict (kantooon, GPL v2): ITM und ITWOM v3.0 mit S RTM3 und Corine, bereits in der siebten Runde als QRadioPredict.

Zehnte Runde (21. November): Antennenbuch, Norton, Barsis, neue Datenquellen. ARRL Antennenbuch (23a ed.), Kap. 4 ( K9LA): Bray-Methode (QST November 1961) für den VHF-Bereich: glatter Erdverlust zu 99% (300 Meilen, 144 MHz: 214 dB), Kurven zu 50% und 99% (Fig. 4.11): 195 dB im Wert von 100 Meilen bis 99% und über 250 Meilen bis 50%; Evaneszenz ±7 dB über 100 Meilen (-3,5 dB bis 50 Meilen), SSB 3 dB schlechter als die CW, +4 dB Bodenreflexion, +3 dB für die Kopplung von zwei Antennen; Höhenzuwachs (15 m: 4/3/2 dB bei 10/50/100 Meilen; 24 m: 8/6/4 dB); Yagi gewinnen etwa 10 mal den Ausleger in λ (dBi); 50 W-Station mit durchschnittlicher Antenne: etwa 180 dB, 100 Meilen, +10 dB bis 250 Meilen. Sonnenaufgang und Sonnenuntergang bis zu +20 dB und +20-50% Durchflussrate; heiße und trockene Luft über nasser Frischluft auf den Großen Seen bis zu 1200 Meilen, typisch 400-600; Regionen, die von großen Wassermassen am meisten begünstigt werden; Spätwinter mit Heizung, die zu starken Inversionen fähig ist. Überprüfung unseres Troposcatters: P.452 bis 50% bei 483 km und 144 MHz ergibt etwa 204 dB, was den 214 dB von Bray bis 99% entspricht. Norton 1959 (J. Res. NBS 63D, frei auf NIST): Grundverlust, System- und Pfadantennengewinn; Referenzatmosphäre Ns = 301; äquivalenter Radius von Ns (250 → 4878 Meilen, 301 → 5280, 400 → 6997); L(99)-Tabellen mit 30-Fuß-Antennen (100 MHz: 163 dB bis 100 Meilen, 208 dB bis 500 Meilen; 1000 MHz: 138 und 188 dB mit 28-Fuß-Parabeln); langfristige Variabilität V(p, θ) abhängig vom Winkelabstand (Bild 13). Reis, Longley und Norton, NBS TN 15 (1959), frei auf NIST. Barsis und Kirby, NBS Report 6751 (1961): 223 km in Colorado mit Pikes Peak (4292 m) als Rand, 100 und 751 MHz; konsistenter Verlust mit der abgerundeten Kante (ca. 11,5 km Radius); langfristige Evaneszenz 11,7-14,3 dB gegenüber 39-42 dB einer Streuung der gleichen Länge; kein Tageszyklus markiert; das Vier-Strahlen-Modell 4-5 Lappen zur Verfügung gestellt, aber nur einer wird gemessen (maximal bis etwa 21 m vom Boden): die Pfade mit Berghindernis sind viel stabiler als die Streuung, nützlich für die Verbindungen "via Vesuv oder Faito". ARRL VHF Digital Handbook: Paket, APRS, D-STAR, WSJT (Scatter Method und EME), kein Teil auf dem Tropo. Neue erprobte oder gesuchte Datenquellen: ECMWF Open Data (IFS 0.25°, Level 1000, 925, 850, 700 hPa..., ohne 975, 950, 900 und 800; G RIB2 mit CCSDS-Komprimierung, die unser Leser nicht dekodiert; bereits über Open-Meteo verfügbar); DWD ICON-EU Open Data (G RIB2 komprimiert bz2, eine Datei für Variable und Level: viele Anfragen); Canada GDPS auf MSC Datamart und GeoMet (ca. 15 km, bis zu 33 Levels; GeoMet ermöglicht die Ernte des Gebiets; es ist die Grundlage von Hepburn-Karten); GFS NOAA-Archiv auf AWS ab Februar 2021 (globale Dateien, nützlich für die Validierung); APRS PropView nur für die Validierung.

Elfte Runde (21. November): Longley-Rice, GEKO Bean and Thayer, "Models of the Atmospheric Radio Refractive Index", Proc. IRE May 1959 (S. 740 ff.): das Modell 4/3 hat die rechte Steigung im ersten Kilometer, fällt aber zu hoch; 30% der Krümmung eines Radius tritt in den ersten 100 m und 60% im ersten Kilometer auf; durchschnittliche Beziehung zwischen Ns und Abnahme im ersten Kilometer: −ΔN = 7,32·e^(0,005577·Ns) (Ns = 313 → ΔN etwa -42). Bean and Thayer, "C.R.P.L. Exponential Reference Atmosphere", J. Res. NBS 63D n. 3 (1959): N(h) = Ns·e^(-c·h) mit c durch Ns durch denselben Bericht fixiert; Average Ns USA 313; beam tracking tables (Gesamtkurve 15,2 mrad mit exponentiellem Modell versus 30,3 mrad mit Erde 4/3). MATLAB-Refraktionexp: c = 0,143859 km−1 für Ns = 313 (Skala etwa 6,95 km), gültig über etwa 1 km und 10 mrad. Verwenden Sie für uns: das "normale" Vergleichsprofil kann sein, dass CRPL mit den lokalen Ns anstelle von M = 315 + 0,118 · z festgelegt ist, und die anomale Abnahme im ersten Kilometer sollte im Vergleich zu -7,32 · e ^ (0,005577 · Ns) anstelle von -40 festgelegt werden. NTIA-Bericht 82-100 (Hufford, Longley, Kissick 1982), ITM-Leitfaden im Bereichsmodus: climi und Ns vorgeschlagen (äquatorial 360, subtropisch kontinental 320, subtropisch marine 370, Wüste 280, gemäßigt kontinental 301, gemäßigt marine an Land 320, gemäßigt marine auf See 350); Ns = N0 ·e^(-hs/9,46 km); Sie verwenden das monatliche Minimum von Ns, weil weniger von Super-Refraktion und Sub-Refraktion beeinflusst; Ns = 301 entspricht ae = 8497 km; unter 100 km Ns zählt wenig. Rice, Longley and Norton, NBS TN 15 (1959), part I: forecasting formula of distribution over time by land and tropospheric wave, base of TN 101. SoftWright, Wikipedia, Altair WinProp und MATLAB rfprop. LongleyRice: gleiche Informationen (Version 1.2.2, 20 MHz–20 GHz oder 40 GHz, 1–2000 km, Ns 301, sieben Klimazonen, Variabilität von Zeit, Ort und Situation); niemand formt die Leitungen, nur die statistische Wirkung des Meeresklimas ("höhere Felder für 10% der Zeit oder nicht"). GEKO Magazine n. 69 ( IU3QEZ): Radiohorizont 4/3 von 130 km von 1000 m, 260 km Hinzufügen von zwei Stationen; Troposcatter mit gemeinsamem Volumen zwischen 1-3 km und 10 km; Streuung von Flugzeugen auf 10 km mit +10-20 dB für Dutzende von Sekunden oder ein paar Minuten; theoretische Kurse mit 2 × 10 Elementen pro Seite: SSB QRP etwa 440 km, SSB 50 W etwa 510 km, CW QRP etwa 600 km, SSB 500 W etwa 640 km (500 W in SSB machen 40 km mehr als 5 W in CW); 90-99% der Zeit sollte entfernt werden ein Rand von 10-20 dB; 5-10 dB pro Horizont Grad auf langen Strecken; die Quote zählt, um den Horizont zu befreien, nicht das gemeinsame Volumen zu nähern; Alpe Adria 2026: 99% des QSO Troposcatter, fast niemand von der Luft trotz AirScout.

## 12. Runder Tisch: Quellen, um das Modell zu validieren oder zu korrigieren (21. November)

Zusammenfassung aller Quellenwechsel, bestellt zur Verwendung. "Zustand" sagt, ob die Quelle bereits im Programm (1.3.1) ist, ob sie vor Gericht steht oder ob sie durchgeführt werden soll.

### Daten zu validieren (was wirklich passiert ist)

| Quelle | Was gibt | Verwendung | Staat |
| MMMonVHF berichtet vom Autor | 593 QSO bei 144 MHz, 29/12/2021–2/1/2022, Zeit- und Ortungsgeräte | Ereignisvalidierung gegen Vergleich (+14 Tage) | im Einsatz: AUC 0,79 (1.2) → 0,90 (1.3.1) |
| Die Welt über 50 MHz | dokumentierte Fälle (14–15/8/2022 Manitoba–Kansas, 144–10 GHz) | historischer Fall | als historischer Fall verwendet; den Validierungs-QuSOs hinzuzufügen |
| wspr.live (WSPR) | komplettes Archiv, 2 m und 70 cm, SQL | viele Veranstaltungen und viele "Nein" (Stunden ohne Spot) | versucht, es sollte gefiltert werden (Spot false bei 16-20 Tausend km) |
| APRS PropView / APRS-IS | Stationen gefühlt, Entfernungen, Stunden | Nur Validierung 144 MHz | zu tun |
| Reverse Beacon (Archiv) | täglicher Spot | VHF | jetzt verworfen |
| I GRA2 radiosondaggi (Brindisi 16320, Praxis der Mare 16245, Trapani 16429, Udine, Mailand) | Gemessene Profile 00 und 12 UTC | GFS Profilkontrolle und Inversionen | gefundene Dateien (Brindisi 46 MB), zu verwenden |
| Barsis und Kirby 1961, Barsis 1963 | Evaneszenz gemessen auf Pfaden mit gebirgigem Hindernis | validieren Sie die Kategorie "monte" (Vesuvio, Faito) | zu tun |
| Norton 1959, NBS TN 15, TN 101 | L(99) und Tabellenmedienverluste | Streukontrolle | teilweise (siehe 300 Meilen konsistent) |

### Von Quellen vorgeschlagene Korrekturen

| Berichtigung | Quellen | gemessene Auswirkungen auf die Validierung | Staat |
| Mismenstabilität Laufzeit | Bean und Dutton Cap. 6 | AUC 0,79 → 0,90 zusammen mit der Absenkung | in 1.3.1 (Titel 16 dB ohne Leitung) |
| Senken durch vertikale Bewegung | Moler und Holden 1960 (Original gelesen am 22/11: +2,5 dB Durchschnitt zwischen herunterkommender und aufsteigender Luft, 1,4 und 9,4 GHz am Meer) | wie oben angegeben | in 1.3.1 (+1,5 / -2 dB) |
| Weiche Rohrleitung (λ bis 3·λmax) | Kerr, Hepburn, VHF/UHF DX Book, Shi 2019 | allein +1% der AUC; bringt die Farben des Kanals auf 46% der Routen der Veranstaltung und 0% des Vergleichs | in |
| Beharrlichkeit 24 h | VHF/UHF DX Book, Whiteman und Leyshon | AUC unverändert, mehr genommen und mehr Fehlalarme | in 1.3.1 |
| Normales CRPL-Profil mit lokalen Ns (-ΔN = 7,32·e^(0,005577·Ns)) statt 40 N/km festgelegt | Bean und Thayer 1959, NBS Monograph 4 | getestet in der Validierung | in 1.3.2 alternativ, aus wegen Defekt |
| Superbrechungsschwelle 79 N/km (ITU) statt 100 (Bean und Dutton, Schicht 1000–850) | P.453, Lopez 2008, DX Book | getestet in der Validierung | in 1.3.2: aggressive Schätzung neben vorsichtig (die Karte verwendet vorsichtig) |
| Klimatologische Dicke (Kalk B) weniger wichtig als das Defizit | Rogers 1997 | zu versuchen: Verwenden Sie A für Gewinn | zu tun |
| Parabolische Gleichung im GFS-Profil | PETOOL, Wellenpropagation (Lytaev), Levy (Buch nicht gelesen, nur Vorschau), Lindquist | erste Version mit einem vom numerischen Rauschen dominierten Feld über dem Horizont (AUC 0,12–0,28): korrekt (Bereich 8 km, Strahl 6°); nach Korrektur AUC 0,54–0,66 | in der Verwendung als zweite Berechnung, entscheidet nicht über die Prognose |
| Evaneszenz: ±7 dB über 100 Meilen, Marge 10-20 dB bis 90-99% | Bray 1961, Antennenbuch, GEKO 69 | Schwellen von Wegen in Randbedingungen | im Einsatz: Wahrscheinlichkeit für Periode und Schwellenwert FT8 -18,6 dB im Troposcatter |
| Gemeinsames Volumen und äquivalenter Abstand von Bray | IU3QEZ, Bray 1961 | Information | in |
| Troposcatter P.617 und RSGB als Unsicherheit | TropoLink, DL2FZN | Information | in 1.3.0 |

### Was bleibt aus Modellen

Leitung zum Meer in geringer Höhe (100-200 m), die die GFS-Niveaus nicht sehen (Studie der aktuellen Datenquellen); Reflexion und Beugung auf den nahe gelegenen Bergen (Vesuvio, Faito): Vorschlag für die Berechnung "via monte". ITM / Longley-Reis bleibt ein statistischer Bezug "normaler Tag" (Ns 301, climi), ohne Leitungen. Verworfen durch Entscheidung des Autors: AirScout, P.528), E sporadisch (P.2001) und 50 MHz (Servoir EIRP, das nur wenige haben und mit dem E sporadisch verwechseln).

### Vorschlag für einen Arbeitsauftrag

1. Extended Validation: Hinzufügen gefilterter WSPR (auch Stunden ohne Spot) und QST 2022; Wiederholen für 432 MHz.
2. Probieren Sie das normale CRPL-Profil und den ITU-Schwellenwert von 79 N / km einzeln aus, wobei nur das beibehalten wird, was die AUC erhöht.
3. Vergleich der GFS-Profile mit der I GRA2 -Radiowelle von Brindisi und Praxis des Meeres in den Tagen des QSO.
4. Bewerten Sie die parabolische Gleichung nach numerischer Korrektur neu.

### Entscheidungen vom 21. November (Version 1.3.2)

- Angenommen: vorsichtige Schätzung (Bean und Dutton, 100 N/km) und aggressiv (ITU, 79 N/km) flankiert; Kanäle gesucht nur unter 2500 m (erste 1500 m, erhöht nach Validierung); Bonus mit hohem ausgedehnten Druck und schwachem Wind; Seegewicht mit dem μ1 von P.452; Anteil der Stationen in Bezug auf die Leitung auch auf Teilleitung; Grenzschicht über den Grate ( IT9GSF).
- Angenommen als Alternative, defekt ausgeschaltet: normales CRPL-Profil mit lokalen Ns.
- Korrekte parabolische Gleichung numerisch: AUC von 0,12 bis 0,54 (30 m Antenne) und von 0,28 bis 0,66 (300 m).
- Land: Copernicus DEM GLO-30 statt Terrain Fliesen «skadi», die in Küstennähe Geisterreliefs von 80-100 m auf See haben (Unterschrift des IC8TEM, Punkt für Punkt nach Capri überprüft); skadi bleibt als Reserve.
- Wetterzeichen auf der Karte mit ihrer Wirkung: ↓ günstige Absenkungen (Moler und Holden 1960, im Durchschnitt etwa 2,5 dB zwischen absteigender und absteigender Luft; +1,5 dB), ↑ Luft bergauf ungünstig (tiefe Armaturen; -2 dB), S scirocco günstiger Hinweis (ARRL Handbuch § 19.4, starke Inversionen auf dem Mittelmeer; keine Korrektur). Doppelsymbol, grün/rot/orange Farben und Comicbuch auf der Karte.
- Angenommen die ITM 1.2.2 (NTIA/itm, public domain), übersetzt in C#: nach Berechnung der Basislinie in normaler Atmosphäre neben P.452 und Endstrecke nach dem Ausgang aus einer Leitung, die vor dem Korrespondenten endet. Verworfen qradiopredict-code, weil unter GPL v2.

Ohne nützlichen Inhalt für den Index: *Communications Quarterly*, Herbst 1991 (kein Artikel zum Tropo; Atkins' Artikel zum Scatter ist im Winter 1991).

- Angenommen als Prinzip der Handbücher (21. November): "Verbreitung kann nicht verallgemeinert, sondern in Bezug auf die lokale Morphologie untersucht werden" ( DF5AI, berichtet von IK3XTV in "Propagation auf 2 Metern"); zitiert im Prognosehandbuch (§ 1) und in den Räten.
- Angenommen (21. November, nach 1.3.3) die Interpretation der transalpinen Öffnungen von IK3XTV: nicht "konvektive Zellen wie Linsen", sondern Troposcatter, der durch Turbulenzen an die Spitze der Neufassungsschicht verstärkt wird, wenn er den Anteil des gemeinsamen Volumens erreicht (Handbuch der Vorhersagen, § 8 ter). Ab 1.3.4 geht sie als vorläufige Verstärkung des Troposcatters in die Punkt-Punkt-Berechnung ein, höchstens 3 dB. Verworfen durch die gleichen Artikel: die "Auto-Verstärkung" der Signale jenseits der Alpen (ein passives Medium verstärkt nicht: die beschriebenen Verbindungen bleiben mit Beugung und Troposcatter erklärbar; die deklarierten Kräfte werden nicht wie angegeben verwendet), der Schnee, der "besser beugt" (höchstwahrscheinlich die stabile Schicht über dem Schnee, mit weniger Evaneszenz).

- Kerr (1951) überprüft auf dem Buch (22. November, PDF zur Verfügung gestellt durch den Autor): § 1.5, eq. 17 und 18 und Tabelle 1.1. Die Zahlen von Gl. 18 im Scan sind unlesbar, aber Tabelle 1.1 bestätigt: 20 ft → 1 cm, 80 ft → 10 cm, 400 ft → 100 cm, 2000 ft → 1000 cm, gegenüber 1,25, 10, 112 und 1252 cm von λmax = 0,014 · d ^ 1,5 (λ in cm, d stehend; der runde Tisch, 'Richtwerte'). Kerrs Hypothesen: Innerhalb der Leitung fällt der modifizierte Index von 4 · 10−8 pro Fuß, typischer Wert der niedrigen Kanäle; mit schwächeren Steigungen muss eine größere Dicke. Kerr warnt auch, dass λmax kein Nettoschnitt ist: Wellenlängen, die manchmal länger sind, können immer noch stark vom Kanal beeinflusst werden.

- DL6WU überprüfte das Original (22. November, PDF von VHF Communications 3/1982 vom Autor zur Verfügung gestellt): G. Hoch, «Extremely Long Yagi Antennas», S. 130 ff. 1: Verstärkung auf dem Dipol gemessen bei 432 und 1296 MHz in einem professionellen Polygon der Deutschen Posten, mit einer Probenantenne EIA von 7,7 dBd, geschätzte Genauigkeit 0,5 dB; 2,35 dB für die doppelte Länge des Auslegers, keine Sättigung bis etwa 20 λ. Das Programm verwendete 8.6 + 8.0.log10(L/λ) dBd, d. h. etwa 0,8 dB in weniger als 1–2 λ; jetzt verwendet es 9,4 + 7,8·log10(L/λ) dBd (der Gradient ist der angegebene, der Wert zu 1 λ wird im Diagramm gelesen, ± 0,3 dB). Verworfen die Formel 10 · log10 (7,8 · L / λ) im Netz zirkuliert: würde 3 dB für die Verdoppelung geben, gegen 2,35 gemessen.
- Booker und Gordon (1950): Referenz bestätigt durch "This Week's Citation Classic" (Current Contents, 8. Februar 1982, Kommentar von H. G. Booker): Proc. IRE 38, pp. 401-412. Der Kommentar bestätigt die beiden in den Handbüchern verwendeten Punkte: Feld, das über den Horizont mit Rayleigh-Verteilung empfangen wird, und exponentielle Autokorrelationsfunktion von Unregelmäßigkeiten (nicht-gaußisch). Der ursprüngliche Artikel wurde nicht gelesen.

## 13. Aus IK3XTV Artikeln

Die beiden Artikel von Flavio Egano IK3XTV (Radio Magazine: "Propagation on 2 Meter" und "Propagation VHF - konvektive Zellen und Gravitationswellen") fügen zwei Dinge hinzu, die die Leitungsmodelle nicht abdecken: die transalpinen Öffnungen des Nachmittags aufgrund von Konvektion und praktische Regeln, wo und wann die Leitungen in Italien gebildet werden. Es sind Beobachtungen von der Station, keine Maßnahmen: Sie müssen als Hypothese behandelt werden, um mit den Flecken zu überprüfen.

| Bemerkungen des Verfassers | Wie es zu einem Bestandteil des Index wird |
| Die Kanäle werden hauptsächlich zwischen 100 und 1000 m gebildet, selten höher; dienen Hochdruck auf einer breiten Fläche, gute Sonneneinstrahlung und Abwesenheit von Wind | Suche nach dem Kanal nur unter 1500 m; Bonus mit hohem Überdruck und schwachem Wind in den niedrigen Schichten |
| In den warmen Monaten auf der Adria zwischen Venetien und Apulien, 700-800 km, manchmal bis nach Griechenland; die Routen auf dem Seeweg werden bevorzugt | größeres Gewicht zum Bruchteil der Seeroute (wie das μ1 von ITU-R P.452) |
| Die entsprechende muss in oder in der Nähe der Leitung sein; mit hohen Antennen und ein paar Meter führen das Signal nicht eintritt | Vergleichen Sie den Anteil der Basis der Leitung mit dem Anteil der beiden Stationen (wird die Höhe der QTH dienen) |
| Gleiche Zeit, gleiche Leitung: Hören Sie vom Gargano eine Station in 144 und eine in 50 MHz | zu überprüfen: Laut Hepburn-Tabelle verlangen die 50 MHz über 300 m Vorsprung, so dass das Ereignis sehr oft auf eine Pipeline hindeutet |
| Verbindungen Venetien – Süddeutschland jenseits der Alpen, ohne Sicht, fast immer nachmittags und mit hohem Druck; instabile Signale, die nach wenigen Minuten verschwinden und wieder auftauchen | „konvektiver Index, getrennt vom Kanal, für Bergrouten |
| Die konvektiven Zellen werden auf dem Boden geboren und steigen etwa 1 km alle 3 Stunden auf: Mit 15 erreichen sie etwa 3 km, die Höhe, die dazu dient, die Alpen zu übersteuern | Verwenden Sie die Grenzschichthöhe (boundary\_layer\_height von Open-Meteo GFS) entlang des Pfades: Punktzahl, wenn sie die Höhe der Grate überschreitet |
| Die Alpen erzeugen Gravitationswellen, die zusammen mit konvektiven Bewegungen Unregelmäßigkeiten des Brechungsindex erzeugen. | Hypothesen: Starker Wind senkrecht zur Kette bis 700 hPa als Signal von Goldwellen; um es zuletzt zu versuchen |
| Sehr hohe Inversion (bis zu 8 km in Ausnahmefällen) gibt etwa 800 km, aber in der Praxis selten mehr als 300 km | Der Anteil der Inversion erweitert den Umfang: nützliche Daten zur Schätzung der Wartezeit |
| Im Sommer entspricht der Radius der Erde etwa 16000 km (2,5-mal die reale), mit etwa 60 dB in weniger als 300 km Verlust | im Einklang mit k50 = 157/(157 − ΔN): k = 2,5 entspricht ΔN etwa 94 N/km, also Superbrechung |
| Mehr als 3000 km nur im 20-30°-Bereich nördlich und südlich des Äquators, über den Meeren, verbunden mit den Hadley-Zellen; Höhepunkt im August in Afrika-Europa | außerhalb Europas: nützlich, um keine hohen Indizes auf dem Atlantik ohne Feedback zu geben, und für die Kanarische Route – Großbritannien |

Weitere nützliche Daten aus den gleichen Artikeln:

- Optischer Bereich zwischen zwei h1 und h2 Metern (d in km): d = 4,1 · (√h1 + √h2), bis zu etwa 20% für die normale Brechung.
- Evaneszenz: langsam und flach mit Inversion; stark und schnell mit Streu- und Konvektionszellen. Das Programm könnte es verwenden, um Anzeigen zu klassifizieren, wenn es eines Tages SNR-Berichte im Laufe der Zeit haben wird.
- Für das sporadische E in 144 MHz berichtet der Autor über die Arbeit von DF5AI und Davis und Johnson (Nature 435, 2005) über die Bindung zwischen Zeiträumen und Es: Aus dem Tropo, aber ein separater Indikator könnte morgen Blitzdaten verwenden.
- Bodenreflexion im Doppelsprung Es: Meer K = 81 und 5 S/m, Landschaft und Wald K = 13 und 0,006 S/m, Stadtgebiete K = 3 und 0,001 S/m.

## 14. LIVETROPOVHF-Indexhypothesen

Abfahrtsvorschlag, zu diskutieren: ein Index 0-10 auf der gleichen Skala wie Hepburn, berechnet für jeden Punkt des Gitters und alle 3 Stunden von GFS (dann ECMWF und ICON zum Vergleich) und eine zweite Zahl pro Route, die angibt, wie viel Teil der Route in einen nützlichen Kanal zum gewählten Band gelangt. Die Gewichte unten sind meine Annahmen, mit den Flecken kalibriert werden.

Rezension nach Bean und Dutton und Kerr: Profilschwellen kommen aus ihrer Tabelle 6.5 und skaliert aus ihren Messungen, so dass eine refraktive Schicht 10-25 dB über dem normalen Profil wert ist. Der Index wird dann zuerst in dB berechnet und nimmt dann 0-10 an; die Werte in dieser Schere müssen noch mit den Spots kalibriert werden.

Berechnung nach Punkt, Schritt für Schritt

1. T Entladung, relative Luftfeuchtigkeit, Höhe des Geopotentials und vertikale Geschwindigkeit bei 1000, 975, 950, 925, 900, 850, 800, 700, 600 und 500 hPa plus T und 2 m Luftfeuchtigkeit, Bodendruck, Schichthöhe und Wind bei 10 m.
2. Verwerfen Sie die Ebenen unter dem Boden; Berechnung N und M auf jeder Ebene (ITU-R P.453).
3. Berechnung -dN/dh in jeder der vier Schichten von Bean und Dutton (1000-850, 850-700, 700-600, 600-500 hPa) und das Diagramm als subrefraktiv, normal oder superrefraktiv mit den Schwellenwerten ihrer Tabelle; Innerhalb der ersten Schicht verwenden Sie auch Zwischenebenen, um den stärksten Gradienten und seinen Anteil zu finden.
4. Wo dM/dh < 0 ist, finde ich Basis und Oberseite des Rohrs, Dicke d und Defizit ΔM.
5. Vergleich der Mindestdicke für das Band aus der Formel von Kerr (etwa 180 m für 144 MHz, 89 m für 432 MHz, wie in der Hepburn-Tabelle); unter der Schwelle zählt die Leitung noch, aber weniger.
6. Ich stimulieren den Gewinn in dB mit der Tabelle unten (Sie nehmen die beste Schicht, nicht die Summe der Schichten, plus den Begriff der Anomalie) und konvertieren es: Index = dB / 2,5, zwischen 0 und 10, also 25 dB ist 10 wert. Ein negativer Wert (Subrefraktion) wird als 0 angezeigt, bleibt aber in den Pfaddaten.

| Beste Schichtsituation | Kriterien | Geschätzte Einnahmen |
| Normal (nicht laminiert) | -dN/dh im Tab-Bereich 6,5 (z. B. 20-60 N/km zwischen 1000 und 850 hPa) | 0 |
| Etwas über normal | zwischen der oberen Grenze des Normalbandes und der Überbrechungsschwelle | 0 bis 10 dB im Verhältnis |
| Superbrechung | jenseits der Schichtschwelle (100, 80, 70, 50 N/km Klettern), aber unter 157 N/km | von 10 bis 15 dB, Anteil bis 157 N/km |
| Zu dünne Pipeline für Band | dM/dh < 0, Dicke darunter Kerr-Grenzwert | 15 |
| Nützlicher Connector für Band | dM/dh < 0, Dicke oberhalb der Kerr-Schwelle | 20 dB, +1 dB je 5 M Defiziteinheiten, maximal 25 |
| Teilbrechung | -dN/dh < 0 in der unteren Schicht | -10 dB |
| Anomaler Hintergrund (Sie fügen hinzu) | Modell Ns minus ITU Karte klimatologische Ns für diesen Monat | 0,2 dB pro Einheit N, zwischen -5 und +5 dB |

Berechnung nach Route (zwischen zwei Locatoren): Abtasten Sie den maximalen Kreis alle 10 km, wie bereits das Programm für den Index auf dem Weg, und berechnen Sie den Bruchteil der Punkte mit Nutzkanal und den durchschnittlichen Anteil der Basis. Die CSIRO-Studie fand den Bruchteil der Route in der Pipeline nützlich (auf den AIS-Routen mit 162 MHz war 67% höher als in den Kontrollfällen).

Addieren der Berechnung pro Route (von LA0BY und W3SZ): (1) Seestationen im Vergleich zu Basis und Spitze der Leitung, weil eine hohe Leitung nur denen dient, die eintreten; (2) ein Troposcatter "Boden" mit der Yeh-Methode, mit den Startecken aus dem Bodenprofil; der Tropengewinn in dB wird von diesem Verlust entfernt und das Ergebnis wird mit dem Verlust verglichen, den die Station überwinden kann ( KK6MC -Tabelle). Somit wird der Index auch zu einer Schätzung der erreichbaren Entfernung.

Korrekturen, um eine nach der anderen zu versuchen (von Bean und Dutton und LA0BY, IK3XTV, G4MCU bereits im Handbuch), nur zu behalten, wenn sie den Vergleich mit Spots verbessern:

- Mischen (wichtig wichtig nach dem Autor, durch tägliche Beobachtung): Tag auf der Erde, zwischen morgens und nachmittags bei klarem Himmel oder mit Wind auf dem Boden über ein paar m / s, die Kanäle auf dem Boden verschwinden; ihr Beitrag muss reduziert werden. Nicht auf See.
- Tageszeit (wichtig wichtig nach dem Autor, durch tägliche Beobachtung): irrationale Kanäle werden bei Sonnenuntergang geboren und fallen nach Sonnenaufgang; es ist eine 3-stündige Vorhersagekontrolle.
- Vertikale Bewegung: Luft steigt auf 850-700 hPa remixt die Schichten (Negativkorrektur), Luft, die unter einen Antizyklon fällt, stärkt sie (positiv).
- Drucktrend: Aufwärtsdruck auf dem Weg in den letzten Stunden, höheres und stabiles Signal; in Abnahme, niedrigeres Signal (bastano 2 hPa).
- Synonym: Hochdruck über 1025 hPa, Temperatur bei 850 hPa weit über dem Boden, heiße und trockene Luft, die auf dem kälteren Meer fließt, Route auf dem Seeweg.
- Gebirgspfade: ein zweiter "konvektiver" Index (Abschnitt IK3XTV), mit einer Nachmittagsgrenzschicht, die höher ist als die Kämme entlang des Pfades und Hochdruck.

Von VHF/UHF DX Buch, andere Korrekturen zum Ausprobieren: aufeinanderfolgende Tage mit hohem Druck auf dem Punkt (mehr Tage, mehr Punkte), Position im Vergleich zum Zentrum des Antizyklons (Bonus an den Rändern), Druckgradient auf dem Boden als Maß für den Wind, starker Regen in den letzten 24 Stunden (Strafe), niedrige Wolken am Rand der Schicht (Strafe nur für die hohen Bänder). Für die Richtung der Routen: Vergleichen Sie den Azimut der Route mit dem der isobaren.

Überprüfung

- LIVETROPOVHF-Archiv: Jeder Werbespot über 500 km bis 144 oder 432 MHz ist ein "Event". Es vergleicht den Index auf der Route mit der Häufigkeit von Ereignissen zur gleichen Zeit von Tag und Monat (der gleiche Ansatz der CSIRO-Studie).
- Vergleich mit Hepburn und F5LEN an den gleichen Tagen: Wo sich die drei Indizes trennen, schaut man, wer nach den Spots richtig lag.
- Radiosundationen von 00 und 12 UTC: um zu überprüfen, ob das Modell die Inversionen sieht, die die Sonde misst.
- Historische Fälle von LA0BY (6-11/11/2003, 10/12/2004, 15/10/2011, 11/2/2015, 22/11/2018): Der an diesen Tagen neu berechnete Index muss die Kanäle sehen, die Hepburn nicht gezeigt hat.
- Grenzwerte, die im Handbuch angegeben werden: unter 100-200 m fast unsichtbar für GFS-Druckniveaus; zuverlässige Vorhersage für 2-3 Tage, dann nur Trend.

## 15. Quellen vom 21. November (zweiter Teil)

- ITU-R P.1812 (MaTLAB/Octave Implementation of I. Stevanovic, OFCOM; GitHub eeveetza/p1812, Zenodo 10.5281/zenodo.6141558). VHF/UHF-flächenspezifische Methode der Route, abgeleitet von P.452: "Delta-Bullington" Beugung, Troposcatter, Kanäle und Reflexion auf Schichten, Belastung auf dem Boden. Nützlich als dritter Vergleich neben P.452 in der Punkt-Punkt-Berechnung; ΔN- und N0 -Daten sind vollständige ITU-Produkte, so dass der Benutzer sie herunterlädt (gemäß P.453).
- aus Costa, Rabanal Ramirez, Dias, IET MAP 2018 (doi 10.1049/iet-map.2017.0992). 563 MHz, 27 Punkte zwischen 5 und 30 km auf unregelmäßigem Gelände mit Wald: P.526 mit 12 m Vegetation -4,3 dB und σ 8,8 dB; P.1812 mit 10 m Belastung +2,6 dB und σ 8,9 dB; dielektrischer Keil -4,8 dB und σ 9,2 dB; P.1546 +5,0 dB und σ 11,7 dB. Für das Programm: Der Restfehler einer guten Beugungsmethode beträgt etwa 9 dB, und die Höhe der Belastungen auf den Stegen (10-12 m) zählt.
- PETOOL (Ozgun e altri, Computer Physics Communications 182, 2011). Parabolische Gleichung Split-Schritt zu einem und zwei Routen mit Weitwinkel-Propagtor (Winkel bis zu 40-45°, gegen 10-15° des schmalen); unten mit Verlusten durch den transformierten Fourier gemischt; Boden zu Schritten. Bestätigt die Auswahl von 1.3.2 (Weitwinkel, Bodenkoeffizienten) und zeigt die Zwei-Wege-Berechnung für steile Reliefs an.
- MathWorks. `tropopl`: Verlust von Gasen und Verlust von «lente» in einer geschichteten Atmosphäre (P.835, P.676, P.453), ohne Kanäle: mit Gradienten unter -157 N / km das geometrische optische Modell gilt nicht. Das Beispiel der Radarausbreitung verwendet für die Brechung −ΔN = 7,32·e^(0,005577·Ns) den gleichen CRPL-Bericht bereits im Programm. `propagationModel` Longley-Rice: vorgegebene horizontale Polarisationswerte, Leitfähigkeit 0,005 S/m, Permittivität 15, Reaktivität 301, gemäßigtes Kontinentalklima. Kein MathWorks-Modell befasst sich mit den Kanälen.
- ITU-R, digitale Produkte der Gruppe 3. Daten und Programme von P.453, P.452, P.1812, P.2001, P.528, P.1546; "integrelle" Daten sind Teil der Empfehlungen; keine deklarierte Weiterverbreitungslizenz: Sie lassen sie weiterhin für den Benutzer herunterladen.
- ESA Navipedia, troposferical delay. N = Nidrostatica + Numida; die trockene zenitale Verzögerung beträgt etwa 2,3 m und die feuchte ein Dutzend cm, die schnell variiert. Für uns: Die von den GNSS-Stationen geschätzte Nassverzögerung ist ein indirektes Maß für die integrierte Feuchtigkeit, mögliche zukünftige Quelle des Jetzt-Gießens; Heute ist nicht nutzbar.
- Bray, K2LMG, «A Method for Determining V.H.F. Station Capabilities», QST November 1961, S. 36-40 (im Original gelesen; erneut überprüft am 22. November auf der PDF-Nummer der QST: alle Werte unten entsprechen). Der Autor, D. W. Bray (General Electric, Ithaca), sagt, er habe die Prognosen mit monatelangen geplanten Verbindungen mit K2GOI und W4LTU verifiziert. Evaneszenz innerhalb der Zeit von 13,5 dB über etwa 100 Meilen (7 dB unter und 7 über dem stündlichen Durchschnitt), fast linear darunter (3,5 dB bis 50 Meilen): für eine feste Verbindung 7 dB entfernen. Korrektur des Horizonts: Jede Höhe des Horizonts ist 69 Meilen (111 km) zusätzlicher Entfernung wert (weniger, wenn negativ). Gewinn eines yagi etwa 10 × Ausleger / λ (Leistungsverhältnis), mehr etwa 4 dB Bodenreflexion; SSB -3 dB und AM -7 dB im Vergleich zu CW; 99% der Stunden: etwa 180 dB bis 60 Meilen, 190 bis 90, 200 bis 240 (2 m). Ab 1.3.2 verwendet die Wahrscheinlichkeit für die Periode σ = 3,4 dB (±7 dB als ±2σ) anstelle der geschätzten 5 dB aus zweiter Hand.
- Zwei-Wege-Berechnung (Versuch mit Prototyp, 21. November). Große Winkelparabelgleichung mit realem S RTM1 Gelände von Capri, 144 MHz, Antenne 10 m vom Boden, Standardatmosphäre; Maß: durchschnittliche Leistung zwischen 20 und 400 m bei 80 km auf der Route zum entsprechenden. Vom Zentrum von Capri und Marina Grande ist der Westen geschlossen (Hindernis auf +11–21°, Monte Solaro): mit dem Faito (fast hinter EA5 und EA6, 150–160° außerhalb der Achse) steigt das Signal in Richtung Spanien 10–29 dB; mit dem Vesuv (110–125° außerhalb der Achse, dann aus dem Berechnungsplan, nur indikativ) von 17–25 dB. Von Anacapri ist der Westen frei und die direkten Gewinne von 8-22 dB. Die absoluten Verluste auf 1000-1300 km des Prototyps sind jedoch nicht zuverlässig. EA3 EA5 EA6
- Barsis und Kirby, NBS Report 6751 (1961), Pikes Peak, 223 km, 100 und 751 MHz. Der Berg ist ein gemeinsames Terminal von zwei Routen in der Optik: Die Verteilung der stündlichen Mediane ist die Faltung der beiden Routen. Langfristige Exkursion (1-99%) von etwa 14 dB, gegenüber 26-40 dB eines Troposcatter-Pfades gleicher Länge oder Winkel; keine nennenswerte Tagesänderung. Das Signal ist stärker als der Troposcatter, aber schwächer als der ideale Rand: Die Oberseite sollte als abgerundet behandelt werden (ungefähr 11,5 km). Hohe Signale werden immer hinter den Dorsalen beobachtet.
- Barsis, NBS Report 7652 (1963), drei Routen über einen Berg (Colorado, Alaska, Europa), 750–900 MHz. Durchschnittlicher Verlust 10-20 dB höher als Fresnel-Kirchhoff Berechnung ohne Land in der Nähe der Terminals; die Evaneszenz wird durch Einwickeln der beiden Linien in die Optik geschätzt; die Reflexionen des Bodens in der Nähe der Terminals verschlechtern die verlängerte verblassen. Ab 1.3.2 fügt der Eintritt in die Leitung die Rundung von P.526 hinzu.
- Barsis und Johnson, NBS TN 88 (1961). Längere Auflösung der Raumwelle, auch auf einem Weg jenseits eines Berges (§ 5).
- Norton, «Systemverlust in Radiowellenausbreitung», J. Res. NBS 63D (1959). Die Kurven, aus denen Bray die Nomogramme erhält; Definitionen von Systemverlust, Übertragung und Basis, Prozessantennengewinn.
- Reis, Longley, Norton und Barsis, NBS TN 101 (1965-67), Band 1 und 2. einzelne Kante und Hindernis abgerundet mit und ohne Reflexionen (§ 7, am Beispiel des Pikes Peak), langfristige Variabilität V(0,5) und Y(q) für Klimazonen (temperierte kontinentale und andere). Für uns: Die Methode der gerundeten Hindernis- und Variabilitätsfunktionen ist der Bezug zur Kalibrierung der Unsicherheit.
- ITU-R P.526-16 (11/2025): Formel (31) von J(ν), identisch mit der des Programms; abgerundete Hürde (32)–36, jetzt verwendet; Multi-Split-Methoden (Bullington und vollständige Methode § 4.5).
- ITU-R P.1812-8 (2025) und -7. Vollständiger Text wiederhergestellt: spezifische Methode der 30 MHz bis 6 GHz (Delta-Bullington, Troposcatter, Kanäle und Reflexion auf Schichten, Inomben). Die -7 warnt, dass ihr Troposcatter (von P.617-5) unter 66 km falsch ist. Es bleibt ein möglicher dritter Vergleich.
- Paulus, «Plavatische Anwendung des IREPS-Verdampfungsmodells», NOSC TR 966 (1984) und Radio Science 20 (1985). Bericht gelesen am 22/11/2026 (DTIC ADA146528, Scan mit erkanntem Text, manchmal unlesbar). Luft-See-Unterschiede, die von Schiffen gemessen werden, werden in Richtung Stabilität verschoben: wärmere Seeluft in 31-35% der Schiffsdaten im Vergleich zu 0,7-7% der NOAA-Eber, für durch Schiffe gestörte Psikrometer und SST aus Kühlwasser (etwa +1 ° C). Das Jeske-Modell gibt dann 40 m und mehr als 15% des Jahres und Höhen, die von 10 auf 40 m in 24 Stunden mit stabiler Zeit springen, nicht durch Funkmaßnahmen bestätigt. Vorgeschlagene Korrektur (geprüfte Schwellenwerte 0, -0,5 und -1 °C; die -1 °C stimmt besser mit den Radiomaßen vom Juni 1982 überein): Wenn die Luft-Meer-Differenz -1 °C oder weniger beträgt, wird die gemessene verwendet; ansonsten berechnen Sie die Höhe mit -1 °C und mit 0 °C und wenn diese mit -1 °C niedriger ist, halten Sie das fest. Erhöhungen auf der stabilen Seite werden geschnitten, Abnahmen bleiben. Im Juni, Marsden 120 Quadrat: durchschnittliche Höhe von 16,8 m (Klimatologie) bis 7,6 m. Die Gleichungen des Modells sind nicht im Bericht enthalten (siehe Jeske und Hitney). Das Programm verwendet das neutrale Profil, das keine stabile Übertreibung hat und δ auf 40 m begrenzt: Es geht in die gleiche Richtung; mit den SSTs der Modelle (nicht des Schiffes) wiegt der Fehler der Messung, den Paulus korrigiert, weniger.
- Levy, «Parabolic Equation Methods for Electromagnetic Wave Propagation» (IEE 2000). Bei Google Books gibt es nur eine Vorschau: Ich lese nicht.
- VK3FS (3fs.net.au). Qualitätseinführung: hohe Kanäle bei 450-2000 m für längere Öffnungen auf dem Boden; Inversionen von 300-600 m in ruhigen und ruhigen Nächten, die nach Sonnenaufgang verschwinden; stärkere Küsteninversionen am Nachmittag; 800-1300 km typisch, 1400-1600 km häufig durchgeführt. Konsistent mit Tag/Nacht-Warnungen und einer Obergrenze von 2500 m.

## 16. Quellen

Regeln und Formeln

- [ITU-R P.453-13 (2017), Radio Brechungsindex:](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)Formel und Daten
- [ITU-R P.452-18 (2023), Vorhersage von Störungen zwischen Bodenstationen über 100 MHz: äquivalenter Radius, β0, Troposcatter,](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)-Kanäle
- [ITU-R P.617-5, Ausbreitung jenseits des Horizonts für Funkbrücken](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)]
- [Shi et al.] "A Comprehensive Study on Wave Maximumlength of Electromagnetic Propagation in Different Evaporation Ducts", IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

Studien zur Vorhersage von Kanälen mit Wettermodellen

- [P. Lopez, "A 5-year 40-km resolution global climateology of super-refraction for ground-based meteorology radar", ECMWF Tech.] Memo 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO), "Tropospheric Ducting Prediction based on GFS Model Data at... the CSIRO Murchison Radio-astronomy", Observatory arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (FMI), "Effects of model resolution on predicting atmospheric ducting conditions with Harmonie-AROME", EGUsphere 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

Prognosen für Amateurfunk

- [W. R. Hepburn, "The Hepburn Tropo Index (HTI)"](https://www.dxinfocentre.com/propagation/hti.htm)und [](https://www.dxinfocentre.com/propagation/history.htm)-Methodengeschichte]
- [W. R. Hepburn, "Lowest Usable Frequency vs. Thickness of Ducting Layer"]](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, "Weltweite Brechungsindexprognosen"](https://tropo.f5len.org/WW/info.php), [neue Version 2026](https://tropo.f5len.org/?p=504), [Anmerkung zum Modell ARPEGE](https://tropo.f5len.org/author/f5len/)]

Daten

- [Open-Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api)], [Hinweis auf Druckniveaus](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)]
- [NOAA NCEI, Integrated Global Radiosonde Archive (IGRA)]](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RAOBget, Radio Flut Skript für die University of Wyoming](https://github.com/NCAR/RAOBget)]

Klassische Texte (vom Autor des Programms angegeben, für endgültige Formeln zu konsultieren)

- B. R. Bean, E. J. Dutton, *Radio Meteorology*, NBS Monograph 92, 1966, 435 Seiten (Nachdruck Dover 1968): Vertikalprofile von N und M, Steigung der Falle unter -157 N/km, Vergleich mit Radiowelle; Kapitel 5 "Synoptische Radiometeorologie". Basis für 2-4 Berechnungsschritte pro Punkt. Kostenlos: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [NIST-Karte, doi] 10.6028/NBS.MONO.92](https://www.nist.gov/node/1770911), [Internet Archive](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf)copy].
- L. Boithias, *Radio Wave Propagation*, McGraw-Hill 1987 (übersetzt von D. Beeson aus dem Französischen, 330 Seiten, ISBN 0070064334; auch North Oxford Academic): Mechanismen jenseits des Horizonts, gemeinsames Volumen der Streuung und Verlust der Kopplung Antennen-Mezzo. Nur Papier ([Bibliothekskarte](https://libcat.library.tamu.edu/Record/in00000014812/Description)).
- M.P.M. Hall, *Effects of the Troposphere on Radio Communication*, IEE Electromagnetic Waves Series n. 8, Peter Peregrinus 1979, 206 Seiten (ISBN 0906048257; brossura 0863410863): synoptische Situationen und Ausbreitung VHF/UHF. Basis für synoptische Zutaten. Handel bei IET ([](https://libcat.library.tamu.edu/Record/in00000636385)-Karte].
- H. G. Booker, W. E. Gordon, "A Theory of Radio Scattering in the Troposphere", *Proceedings of the IRE*, vol. 38, n. 4, April 1950, pp. 401-412: Scapping Theorie aus der Disogenität des Brechungsindex. Zentrale Idee: Die diffuse Kraft hängt von der Intensität und dem Maßstab der turbulenten Schwankungen des Brechungsindex (exponentielle Korrelation) ab; in der Nähe des Horizonts herrscht Beugung, weitere Streuung; Nach Ansicht der Autoren reichen die Turbulenzskala und die von der Meteorologie erwarteten Indexschwankungen aus, um die gemessenen Felder zu erklären. Zitiert von 328 Artikeln (Scilit), keine kostenlose Kopie. Karte auf Semantic Scholar ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); Volltext auf IEEE Xplore bezahlt ([doi 10.1109/JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)]).
- C. M. Crain, J. R. Gerhardt (Electrical Engineering Research Laboratory, University of Texas), "Measurements of the Parameters Involved in the Theory of Radio Scattering in the Troposphere", *Proceedings of the IRE*, Vol. 40, n. 1, Januar 1952, pp. 50-54, doi 10.1109/JRPROC.1952.2739 geben die realen Werte an, die Booker und Gordon nur geschätzt hatten ([Skilit](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)tab]).
- "The Constants in the Equation for Atmospheric Refractive Index at Radio Frequencies", *Proceedings of the IRE*, vol. 41, n. 8, August 1953, pp. 1035-1037: schlägt vor, die damals verwendeten Ströme (79 K/mb und 4800 K) in der Formel zu überprüfen. N; es ist von hier, dass die Werte 77,6 und 3,73 · 10^5 der Formel ITU ([Q]](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E.C.S. Megaw, "Fundamental radio scatter propagation theory", *Proceedings of the IEE Part C: Monographs*, vol. 104, n. 6, September 1957, pp. 441-455, doi 10.1049/pi-c.1957.0052: spectrum of index fluctuations from turbulence theory, completetracing of scatter beyond the common horizon and effect of the geometry paid at the IET ([Skilit](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)card].
- *Proceedings of the IRE*, Oktober 1955, Sondernummer zur Streuausbreitung (von K. A. Norton und J. B. Wiesner), mit Bullington, Staras und anderen: kostenlos in PDF auf [World Radio History](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf)].
- D. E. Kerr (herausgegeben von), *Propagation of Short Radio Waves*, MIT Radiation Laboratory Series vol. 13, McGraw-Hill 1951 (Restampe Dover 1965 und IEE 1987): Theorie der Verdunstung und Oberflächenkanäle auf dem Meer. Digitale Kopie: [World Radio History](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); IET-Ausgabe in [IET Digital Library](https://shop.theiet.org/propag-short-radio-waves)].

Jüngste Messungen der troposferischen Diffusion in VHF: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Australien), "A Study of VHF Troposcatter Communication Channels in South Australia", in * Digital Signal Processing for Communication Systems*, Springer 1997, pp. 103-112 ([Springer](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)]. Text gegen eine gebühr; abstrakt betrifft es diffusion, nicht die kanäle. Nützlich nur, wenn Sie den Begriff Troposcatter mit VHF-Kanaldaten kalibrieren möchten.

Numerische Modelle und Seewege (vom Autor des Programms angegeben)

- M. Levy, *Parabolic Equation Methods for Electromagnetic Wave Propagation*, IEE Electromagnetic Waves Series #45, 2000, 336 Seiten (ISBN 0852967640): parabolische Gleichung für refraktive Profile, die sich entlang des Pfades und für Orographie ändern; Kapitel über troposferous Ausbreitung, Strahlen und Wege, Ausbreitung auf See. Es würde für eine Feldberechnung entlang des Pfades, jenseits des Index, dienen. Für die Zahlung, auch in elektronischer Form von IET ([IET-Karte](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)].
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (Universität von Leicester): 2 GHz Messungen auf den Kanalinseln und dem Kanal. Viele Arbeiten und Dissertationen sind im Universitätsarchiv kostenlos: [Langzeitstatistiken über den Verdunstungskanal, Radio Science 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434), [Signalwechsel auf drei Seewegen, Radio Science 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710), [Titel von N. Mufti 2012 mit Routen von 50 und 140 km auch in VHF](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854), [Tesis von I. Alam 2002 über die aus den Wetterdaten berechnete Reaktivität, 240 MHz und 2 GHz X3 UQ, [tesi. ].](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus, "Practical application of an vapor model", Radio Science vol. 20 n. 4, 1985, S. 887 (not IEEE Transactions on Antennas and Propagation), doi 10.1029/RS020i004p00887 ([Scheda](https://zendy.io/title/10.1029/rs020i004p00887)): height of the vapor duct from surface weather data. Befreien Sie den technischen Bericht davor: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf)].

T. Lindquist, *Wave Propagation Models in the Troposphere for Long-Range UHF/SHF Radio Connections*, Masterarbeit, Universität Karlstad, 2. Juni 2020, 106 Seiten (geliefert vom Autor des Programms). Es ist eine Schritt-für-Schritt-Anleitung, kostenlos, um eine Berechnung mit der parabolischen Gleichung zu schreiben: Split-Step-Lösung (auch in weiten Winkeln), Randbedingungen für Land und Meer, Boden zu Schritten, Strahlverfolgung auf linearen Profilen zeitweise, mit numerischen Überprüfungen; zitiert das PETOOL-freie Tool für MATLAB. Zwei nützliche Daten gleichzeitig: (1) N Empfindlichkeit gegenüber Wettervariablen, δN ≈ 0,26 δP + 4,3 δe - 1,4 δT (hPa und K, etwa 20 °C), dh ein Fehler von 1 hPa beim Dampfdruck wiegt dreimal so viel wie ein Fehler von 1 °C bei der Temperatur: ist die Feuchtigkeit des Modells, die zuerst überprüft werden muss; (2) Durchschnittliche Höhe des Verdampfungskanals: 5-6 m in der Nordsee, 13-14 m im Mittelmeer, über 20 m im Persischen Golf, immer zu niedrig für 144 und 432 MHz. Fazit des Autors (Frequenzen 1 bis 10 GHz): Über den Horizont hinaus erfordern die zuverlässigen Verbindungen fast immer eine Leitung.

Vermehrungssoftware auf dem Bodenprofil (für Horizont und Hindernisse, nicht für den erwarteten Tropo)

- [Radio Mobile](https://ve2dbe.com/rme.html)von Roger Coudé VE2DBE: kostenlos, Windows (auch Linux und Mac mit Wein), Longley-Rice/ITM-Modell, Landprofil automatisch aus Quotendaten extrahiert ([Download](https://ve2dbe.com/download/download.html)].
- [SPLAT!]](https://qsl.net/kd2bd/splat.html)von John Magliacane KD2BD: GPL, 20 MHz–20 GHz, Longley-Rice und ITWOM v3.0 Modelle, Hindernis- und Verlustberichte im Text; neueste Version 1.4.2 von 2014.
- [ITM, NTIA/ITS](https://its.ntia.gov/software/itm)offizieller Code: das Longley-Rice-Modell (20 MHz–20 GHz) mit Quelle und Dokumentation, Basis von Radio Mobile und SPLAT!]. Der Code ist kostenlos und Sie können die Berechnung auf dem Weg von LIVETROPOVHF (Verlust durch den Boden, der zum Tropo-Index hinzugefügt wird) ergänzen. Originalbericht: A. G. Longley, P. L. Rice, "Vorhersage von troposphärischem Funkübertragungsverlust über unregelmäßigem Terrain: Eine Computermethode - 1968", ESSA Technischer Bericht ERL 79-ITS 67, Juli 1968 (NTIA-Karte auf historischer Software: X1) https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITU-R, Software und digitale Daten](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): kostenlose Karten von ΔN- und N0 -Jahresmedianen auf Raster 1,5°, die von P.452 und P.617 verwendet werden — nützlich als Referenzklimawerte für den Index.]
- Pathloss (Contract Telecommunication Engineering): kommerzielles Programm für Funkbrücken; Ich habe keine öffentliche Quelle gefunden, die bestätigt, welche ITU-Empfehlungen umgesetzt werden, also zu überprüfen.

Wiederverwendbarer GitHub-Code in LIVETROPOVHF

| Projekt | Was tun Sie | Sprache | Nutzung für uns möglich |
| [NTIA/ITM](https://github.com/NTIA/itm)] | Model longley rice offiziell von ntia, mit paketen für. NET |  C++, C#  | Landverlust auf dem Weg: integriert sich direkt in das Programm, das in C# ist |
| [fuzzzeee/itm](https://github.com/fuzzzeee/itm)] | ITM C++ Code C# Übersetzung |  C#  | Alternative ohne native Bibliotheken, auch nützlich für die Windows 7 Version |
| [eeveetza/Py452](https://github.com/eeveetza/py452)] | ITU-R P.452, identisch mit der von ITU-R WP 3M genehmigten MATLAB/Octave-Version; MATLAB-Version veröffentlicht vom Schweizerischen Bundeskommunikationsamt | Python, MATLAB | Referenz zu übersetzen in C# die Troposcatter-Formeln und durchgeführt, mit ITU-Validierungsbeispielen |
| [bwinkel/pycraf](https://github.com/bwinkel/pycraf)] | P.452 vollständige, P.676 SRTM-Profile; GPL v3, aber ITU-Dateien enthalten sind nicht kostenlos für den kommerziellen Gebrauch | ♪ | Kontrolle unserer Ergebnisse |
| [ITU-Rpy](https://github.com/inigodelportillo/ITU-Rpy)] | viele ITU-R-Empfehlungen, einschließlich P.453-13, validiert mit ITU-Beispielen | ♪ | Überprüfung der N-Formel und Klimakarten von P.453 |
| [NCAR/RAOBget](https://github.com/NCAR/RAOBget)] | Download der Radiowelle der University of Wyoming im Textformat | ♪ | Diagramm zum Download in C# die Profile der italienischen Stationen |

Hinweis: Die aktuelle Version von P.453 ist die [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)]

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), kostenlos herunterladbar; in dem Dokument habe ich die 13 verwendet, um sie neu auszurichten.

Bereits in den Programmhandbüchern und hier wiederverwendbar: LA0BY (Nordic VUSHF 2019), IK3XTV ("Propagation auf 2 Metern" und "Propagation VHF – konvektive Zellen und Gravitationswellen", Radio Magazine, bereitgestellt vom Programmautor), DF5AI/DK5YA (2003), G3YGF (QST 1983), The VHF/UHF DX Book cap. 2, G4MCU (2003) KP4MD
