LEBENSMITTEL · Technisches Dokument 3
# Wie man den Trope in 144 MHz und darüber hinaus bildet

> Automatische Übersetzung durch Italienisch: im Zweifel ist der italienische Führer wert.

Was passiert in der Luft, wenn die 144, 432 und Mikrowellen über den Horizont hinausgehen: Brechung, Inversionen, Kanäle, Streuungen und wie man sie auf der Programmkarte liest. Version vom 9. November 2026.

## Zusammenfassend

- Die VHF-Wellen und darüber hinaus bewegen sich in den ersten Kilometern der Atmosphäre (Troposphäre) und biegen sich, wo sich der Brechungsindex mit dem [3] [7] [11] ändert.
- Der Index wird mit refraktivem N gemessen, das von Druck, Temperatur und vor allem [11] Feuchtigkeit abhängt.
- In normaler Atmosphäre gibt es etwa 40 Einheiten pro km; Wenn es mehr als 157 Einheiten pro km fällt, bleibt die Welle in einem [7] [11] -Kanal gefangen.
- Die Leitung kommt von einer thermischen Inversion mit trockener Luft oben und unten nass: Antizyklon, Meer, ruhige Nächte, [3] [4] [5] [13] Fronten.
- Je höher die Frequenz, desto dünner ist der Kanal: Die Mikrowellen öffnen sich häufiger als die 144 [3] [7].
- Ohne Leitung bleibt die Troposphäre Streuung: immer vorhanden, schwach, die mit dem Abstand [6] fällt.
- Auf dem Programm: Hochdruck + Reverse + Hepburn / F5LEN high = wahrscheinliche Bedingungen; die Spots bestätigen [1] [2] [13] [14].

## 1. Wo es passiert: die Troposphäre

Die Troposphäre ist die unterste Schicht der Atmosphäre, bis zu etwa 10-12 km. Für die Trope zählen vor allem die ersten 3 km: es gibt Wasserdampf und Inversionen, und es passieren die Funkstrecken zwischen Stationen auf dem Boden [4]. Unter normalen Bedingungen sinkt die Temperatur mit einer Höhe von etwa 6,5 °C pro km [3]. Wenn hingegen in einer Schicht die Temperatur mit der Höhe ansteigt, sprechen wir von thermischer Inversion: Sie ist der Ausgangspunkt fast aller starken Tropoöffnungen.

Die VHFs werden von der Ionosphäre nicht als HF reflektiert (Salb und sporadisch, was andere Stunden und andere Entfernungen [5] hat): Hinter dem Horizont kommt es nur, wenn die Atmosphäre sie zum Boden biegt oder ausbreitet.

## 2. Refraktion und refraktives N

Eine Radiowelle biegt sich durch Luft mit einem Luftbrechungsindex mit einer anderen. Der Index n ist sehr nahe an 1, so dass die Reaktivität N verwendet wird, dh n - 1 multipliziert mit einer Million [11]:

```math
N = (n - 1) \cdot 10^{6} = \frac{77,6}{T} \left( P + 4810 \, \frac{e}{T} \right)
```

mit P dem Druck im hPa, T der Temperatur in Kelvin und dem Druck des Wasserdampfes im hPa. N Boden ist normalerweise 300-400. Der zweite Begriff, der von Dampf, wiegt viel: Aus diesem Grund zählt die Feuchtigkeit so viel wie die [3] [11] Temperatur.

Was die Welle biegt, ist nicht der Wert von N, sondern wie sie sich mit dem Quotienten, dem Gradienten dN/dh, ändert:

| dN/dh (N/km) | Lage | Wirkung auf die Welle |
| 0 | Subbrechung | Die Welle bewegt sich vom Boden weg, der kürzere Horizont |
| von 0 bis -79 | Normal (Standard -39) | Lichtbeugung zum Boden: Radiohorizont etwa 4/3 des optischen |
| -79 bis -157 | Überbrechung | Die Welle biegt mehr, erhöhte Durchflussraten |
| weniger als -157 | Inhalt | Die Welle biegt sich so viel wie die Erde oder mehr und bleibt gefangen |

Die Schwelle von -157 N / km ist der Punkt, an dem die Krümmung des Radius der Erde [7] [11] entspricht. Daher der "äquivalente terrestrische Strahl" von 8497 km (4/3 des realen Radius), der bei der Berechnung des [6] Scatters verwendet wurde.

## 3. Inversionen: Der Tropo-Motor

Ein Gefälle unter -157 N/km erfordert, dass N in wenigen Höhenmetern zusammenbricht. Es passiert, wenn über einer Schicht frischer und feuchter Luft wärmer und trockener gefunden wird: Die Temperatur steigt und der Dampf fällt zusammen, und beide bringen N [3] [5] herunter. LA0BY zeigt Inversionen bis zu 10-15 °C pro km gegen die normale Abnahme von 6,5 °C [3].

Inversionen entstehen auf unterschiedliche Weise:

- Senken bei Antizyklonen. Im hohen Druck fällt die Luft in der Höhe langsam, komprimiert und erwärmt sich, während am Boden frische und feuchte Luft bleibt. Es ist die Ursache für die längeren und längeren Öffnungen, die Tage [3] [4] [13] dauern.
- Nächtliche Strahlung. Bei ruhigem Himmel und schwachem Wind kühlt sich der Boden nachts ab und kühlt die Kontaktluft: Low Reverse, die mit der Morgensonne bricht. Abend- und Morgenöffnungen, typisch für Sommer und Herbst.
- Meereswarnung. Heiße und trockene Luft aus dem Boden über einem kälteren Meer: Die Meeresschicht bleibt unten feucht und kühl. Von hier aus die großen Seerouten (Atlantik, Mittelmeer, Nordsee) [4] [13].
- Fronten. Vor einer warmen Front strömt die heiße Luft über die kalte; die Öffnung ist kurz und folgt der Front.
- Verdunstung. Über dem Meer, in den ersten Metern, sinkt die Luftfeuchtigkeit schnell mit der Höhe: sehr dünner Kanal, nur für Mikrowellen nützlich.

Das Zeichen, dass eine Umkehrung in den vertikalen Profilen (Radiosondaggi) zu sehen ist: Temperaturanstieg und Taupunkt, der in der gleichen Strecke [5] abfällt. Einen Hinweis darauf zeigt das Programm mit der Thermischen Inversionsschicht (Luft mit 925 hPa wärmer als am Boden) und mit der Isothermen mit 850 hPa.

## 4. Oberflächen- und Hochkanäle

Ein Kanal ist eine Schicht, in der sich die Welle zwischen der oberen Kante (Inversion) und der Boden- oder Bodenkante bewegt [7]:

- Oberflächenfernleitung: Boden oder Meer. Die niedrig gelegenen Stationen, insbesondere an der Küste, gelangen direkt.
- Hohe Rohrleitung: steht in der Höhe, zum Beispiel zwischen 500 und 2000 m. Die Stationen in den Ebenen können sich nicht fühlen, während die auf dem Hügel oder in den Bergen in den Kanal eintreten. Erklären Sie die Verbindungen, die die Alpen oder andere [3] Reliefs [4] überqueren.

Um die Leitung zu betreten, sollte die Welle fast horizontal beginnen: niedrige Antennen am Horizont und kein benachbartes Hindernis. Innerhalb der Leitung ist die Dämpfung viel geringer als im freien Raum, und die Entfernungen erreichen 1000-2000 km und darüber hinaus (zum Beispiel die Verbindungen zwischen der Iberischen Halbinsel, den Kanarischen Inseln und England oder zwischen Skandinavien und Südrussland) [3].

## 5. Frequenz: Warum sich Mikrowellen häufiger öffnen

Ein Kanal fängt im Vergleich zu seiner Dicke nur recht kurze Wellen ein. Die langen Wellen "kommen" aus einem dünnen Kanal; Die kurzen nicht. Dann [3] [7]:

| Band | Wellenlänge | Was brauchen Sie? |
| 144 MHz |  2 m  | Dicke Rohrleitungen (Centimenaia von Metern), breite und gut markierte Inversionen: weniger häufige, aber sehr umfangreiche Öffnungen |
| 432 MHz | 70 cm3 | Dünnere Rohrleitungen: Öffnet sich oft, wenn 144 nur „Waren sind |
| 1296 MHz | 23 cm | Wenige schwache Inversionen; lange Routen auf dem Meer im Sommer häufig |
| 10 GHz | 3 cm | Nur der Verdunstungskanal auf dem Meer; zählt auch die Absorption von Regen |

Aus diesem Grund kann ein bescheidener Tropo-Index bereits Öffnungen in UHF und Mikrowelle bedeuten, während die 144 höhere Werte fordern. Andererseits haben die 144 an der gleichen Leitung weniger Verluste außerhalb der Leitung und decken die Randbereiche besser ab.

## 6. Troposfer Scatter

Auch ohne Inversion ist die Troposphäre nicht einheitlich: Kleine Temperatur- und Feuchtigkeitsunregelmäßigkeiten verbreiten einen kleinen Teil der Energie über den Horizont hinaus. Es ist die stets vorhandene troposferische Streuung, die Verbindungen von 300-800 km mit guten [6] -Stationen ermöglicht. Das Signal ist schwach und schwankend; der Verlust wächst mit dem Diffusionswinkel, also mit der Entfernung und den Hindernissen am Horizont:

```math
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
```

mit θs der Diffusionswinkel in Grad und f in MHz [6]. Das Programm verwendet diese Formel in der Registerkarte jeder Stelle, um zu sagen, ob eine Verbindung mit der Streuung erklärt werden kann oder eine Leitung erfordert (siehe Dokument 1). Ein hoher N-Wert am Boden verbessert die [6] -Streuung.

Nicht zu verwechseln mit dem Airscatter (Reflexion auf Flugzeugen: Signale von wenigen Sekunden, Doppler) und dem Scatter-Meteor (Reflexion auf Meteoren), den das Programm aus dem Tropo ausschließt.

## 7. Das Wetterbild: Wie man es auf der Karte liest

Die Tropo-Öffnungen folgen der Meteorologie. Typische Situationen [3] [4] [13]:

- Stabiler Antizyklon, Hochdruck (mehr als 1020-1025 hPa), weit isobar, wenig Wind: Senkungsumkehr. Die besten Öffnungen folgen dem Rand des Antizyklons, besonders am Meer.
- Calo della pressione: Inversionsbrüche. G4MCU folgte dem SK6VHF -Beacon, bis der Druck auf 1012 hPa [13] fiel.
- Heiße Luft in der Höhe (Isoterm bei 850 hPa ansteigend) über Frischluft am Boden: umgekehrt.
- Konvektive Zellen und Gravitationswellen (z.B. Unterwind in den Alpen) können lokale und kurze Öffnungen [4] erzeugen.
- Tageszeit: Inversionen durch Bestrahlung sind am Abend und am frühen Morgen stärker; die Absenkung bleibt auch am Tag.

Die Hepburn [1] und F5LEN [2] Indizes setzen all dies aus Wettermodellen zusammen: Sie berechnen N bei verschiedenen Quoten und transformieren Gradienten in eine Zahl oder eine Klasse. Ich bin keine Garantie: LA0BY zeigt ein LA- UA6 -Ereignis, das bei hohen Temperaturen sichtbar ist und nicht in Hepburn [3]. Dazu zeigt das Programm zusammen Indizes, isobare, Temperaturen und reale Flecken: Wenn sich die Flecken zu einem hohen Indexbereich unter einem Antizyklon ausrichten, ist die Öffnung im Gange. KP4MD fand die SNR von WSPR Berichte auf 144 MHz wachsen direkt in den Fenstern von Hepburn [14] zur Verfügung gestellt.

## 8. Praktische Liste

1. Schauen Sie sich den Tropo-Index auf Ihrer Region und entlang der Richtung an, die Sie für die nächsten Stunden interessiert.
2. Überprüfen Sie isobar: Hochdruck und breite isobar über oder neben dem Weg.
3. Thermische Inversion und T 850 hPa einschalten: Heißluft in Höhe über dem Pfad.
4. Bevorzugt Routen auf dem Meer und Abend- und Morgenstunden.
5. Wenn Sie auf dem Hügel sind, denken Sie an die hohen Kanäle; Wenn Sie an der Küste sind, an der Oberfläche.
6. Versuchen Sie zuerst die höchsten Bänder (432, 1296): zuerst öffnen.
7. Überprüfen Sie mit Spot und PSKReporter: Lange Linien in die gleiche Richtung bestätigen den Kanal.

## Quellen

1. W. R. Hepburn, DX Info Centre, „The Hepburn Tropo Index (HTI)“ und „Tropospheric Ducting Forecast“ Seiten (dxinfocentre.com).
2. F5LEN, Prognose für Europa und Welt Vorhersage der VHF tropophärischen Ausbreitung (tropo.f5len.org).
3. S. Heck LA0BY, „Extreme Tropo Propagation on 144 MHz and up, 41° Nordic VUSHF Meeting, Skjeberg 2019. Normaler Gradient und Inversionen, Temperatur und Feuchtigkeit, hohe Drücke, LA- UA6 -Ereignis, Verbindungen jenseits der Berge.
4. F. Egano IK3XTV, "Propagation VHF - Der Beitrag von konvektiven Zellen und atmosphärischen Gravitationswellen in der Tropo-Ausbreitung." Erste 3 km, Meerespfade, konvektive Zellen, Gravitationswellen, Hochdruck.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, "Very Long Distance Propagation in the 144 MHz Band", 24/7/2003. Temperatur- und Taupunktprofile, Inversionen, Unterscheidung von E sporadisch.
6. J. N. Gannaway G3YGF, "Tropospheric Scatter Propagation", QST November 1983. Erdradiusäquivalent und Streuverlust.
7. Das VHF/UHF DX Buch, Kapitel 2 „Troposphärische Ausbreitung. Brechungszonen nach dN/dh und Oberflächen- und Hochkanalprofilen (nur die Figuren).
11. Empfehlung ITU-R P.453, Radiobrechungsindex. N Formel und Kanalschwelle bei -157 N/km. Allgemeine Bezugnahme, nicht beigefügt.
13. G4MCU, „troposphärische Artikel (g4mcu.net), 2018–2025. Condotti mit hohem Druck, vor allem auf dem Meer; SK6VHF und sinken auf 1012 hPa.
14. C. F. Milazzo KP4MD, „VHF/UHF WSPR Propagation Study, 2012–2014. SNR WSPR bei 144 MHz und Hepburn Prognose.

Die Quellennummern sind identisch mit Dokument 2, Map Building.
