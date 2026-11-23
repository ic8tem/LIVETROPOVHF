LEBENSMITTEL · Technisches Dokument 2
# Aufbau der Karte

> Automatische Übersetzung durch Italienisch: im Zweifel ist der italienische Führer wert.

Wie die Karte der Registerkarte Karte und Spot geboren wird: die geografische Basis, die Projektionen, der Durchschnitt der Hepburn und F5LEN Indizes, die isobare, die Spots und die Stationen hören, überlappt mit Schichten. Version vom 9. November 2026.

## Zusammenfassend

- Die Karte wird vom Programm in acht Schichten gestaltet, jede mit ihrer [8] -Box.
- Basis: Natürliche Erdküste und Grenzen auf zwei Ebenen des [9] -Details; [10] Market und Azimuth-Projektionen.
- Physik: Inversionen, Reaktivität N und Gradient dN/dh, Leitungsschwelle bei -157 N/km [3] [4] [7] [11].
- Trumpet-Index: kalibriertes Lesen von Hepburn [1] und F5LEN [2] Karten, gemeinsames Raster und Durchschnitt der beiden Quellen.
- Isobar: GFS [12] Druck über Open-Meteo [15], Linien alle 4 hPa, H und L Zentren.
- Spot, Abhörstationen (dunkelrosa Kreise mit immer sichtbaren rosa Linien) und persönliche Decodierungen in Rot.
- Lesen Sie die Überlappung: hoher Druck, hohe Kanäle, Druckabfall, wachsende SNR, [3] [4] [5] [13] [14] Bergrouten.

## 1. Das Prinzip: eine mehrschichtige Karte

Die Karte ist kein festes Bild: wird vom Programm Schicht für Schicht entworfen, jedes Mal, wenn Sie Zoom, Projektion, Band, Prognosezeit oder Thema ändern. Jede Ebene hat ihre Box in der oberen Layerleiste auf der Karte, so dass Sie nur halten können, was Sie brauchen und alles zusammen sehen, wenn Sie das vollständige Bild wollen. Die Reihenfolge, von unten nach oben, ist diese:

|  #  | Straße | Inhalt | Casella |
|  1  | Grundkarte | Meer, Land, Küsten und Grenzen | Ja |
|  2  | Indextropo | Medien Hepburn + F5LEN oder nur eine Quelle; einstellbare Transparenz | Ja |
|  3  | Grill-Locator | Felder, Quadrate und, mit Zoom, Quadrat | Ja |
|  4  | Isobar | Druckleitungen alle 4 hPa, Zentralen H und L | Ja |
|  4a  | Temperatur 850 hPa | Jede isotherm behandelte 4 °C | Ja |
|  4b  | thermische Inversion | Orangene Probe, bei der die Luft in der Höhe wärmer ist als auf dem Boden | Ja |
|  5  | PSKReporter | Empfangsstationen von PSKReporter (dunkelrosa Kreise) und, in rosa, was sie hören | Ja |
|  6  | Entschlüsselt von mir | Decodierungsstationen mit WSJT-X, JTDX oder MSHV: Rote Linien von Ihrem QTH | Ja |
|  6a  | QSO desunti dal live | Geschlossene Verbindungen ( RR73, 73, RRR) lesen Sie in Ihren Dekodierungen: grüne Linie zwischen den beiden Stationen | Ja |
|  7  | Spotlight | Anschlüsse: volle blaue Linie, gelb für Airscatter abgedeckt, zwei Kugeln | Ja |
|  7a  | Ausschlüsse | Casella Ausgenommen unter 100 km: Der kürzeste Verkehr ist weder gezeichnet noch aufgeführt. Casella Mehr als 1000 km: nur Quadrate (beginnend): wenn eine der Stationen mehr als 1000 km von Ihrem QTH ist, keine Linien oder Kreise, nur die Quadrate Locator der Stationen in Flieder bei 45° | Ja |
|  8  | QTH und Labels | Ihre Station, die Namen des ausgewählten Spots | immer sichtbar |

Der Tropo-Index befindet sich unmittelbar über der Basis und unter allem anderen: ist eine farbige Abtastung, und höher platziert würden Linien und Symbole abdecken. Das Gitter geht darüber hinweg, so dass Sie immer wissen, wo ein farbiger Bereich fällt. Die Isobaren sind dünne Linien und werden über die Probenahme gut gelesen.

## 2. Die Grundkarte

Kosten, Grenzen und Land stammen aus natürlichen Erdvektordaten, von Public Domain [9], im Maßstab 1:50 Millionen für die Gesamtansicht und 1:10 Millionen bei Vergrößerung. Die Dateien befinden sich im Programmordner: keine Online-Kartendienste. Farben des Themas Klar: hellblaues Meer # E2ECF8, dunkelblaue Küste, hellgraue Grenzen. In den Themen Schwarz und Nacht sind die Farben der Basis abgedunkelt, in den anderen Themen färben sie sich leicht.

## 3. Die beiden Vorsprünge

Alle Schichten durchlaufen die gleiche Projektionsfunktion, so dass sie immer miteinander ausgerichtet sind. Sie wählen zwischen zwei [10] -Projektionen (λ Längengrad, φ Breitengrad, R-Strahl der Erde):

Markt. Lokale Ecken und Formen lagern; Die Abstände erstrecken sich zu den Polen hin. Auf ±85° Breite geschnitten.

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

Azimutale Equidtanti zentriert auf Ihrem Quadrat. Das Zentrum (φ0, λ0) ist das Zentrum Ihres WWL-Quadrats. Entfernungen und Richtungen, die vom Zentrum aus gemessen werden, sind wahr: Eine Station bei 1200 km ist 1200 km im Maßstab, und der Winkel mit dem Norden ist der Azimut, auf den die Antenne gerichtet werden soll. Wenn Sie den Locator ändern, wird die Karte neu ausgerichtet.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

Im Azimut werden die Linien, die auf dem Papier gerade sind (Meridiane, Parallelen, quadratische Kanten), gekrümmt. Aus diesem Grund werden alle Linien in kurze Abschnitte unterteilt, jeder Abschnitt wird projiziert und dann werden die Merkmale vereint; dasselbe gilt für die Ränder des Tropo-Index und für das Isobare. Der Antipod-Punkt in der Mitte hat keine bestimmte Position und bleibt außer Sichtweite.

## 4. Grill Locator, Zoom und Move

Das Maidenhead-Raster wird durch das Programm entworfen: Felder von 20° × 10° mit den beiden großen und verblassten Buchstaben in der Mitte, Quadrate von 2° × 1. und mit ausreichender Zoom-Subquadrati von 5′ × 2,5′. Die Linien sind blau und dünn, um den Tropo-Index nicht abzudecken.

Zoom mit dem Rad zentriert auf dem Zeiger (der Punkt unter der Maus bleibt stabil), ziehen, Doppelklick, um zur gesamten Ansicht zurückzukehren, + und - Tasten und die Taste mit dem Prozentsatz für die gesamte Ansicht. Details passen zum Zoom: feinere Küsten, Unterquadrate, dickere isobare Etiketten.

## 5. Warum Inversionen die Wellen beugen

In der niedrigen Atmosphäre sinkt die Temperatur normalerweise um 6,5 °C pro km; Der Brechungsindex hängt sowohl von der Temperatur als auch von der [3] -Feuchtigkeit ab, und wo er sich mit der Höhe stark ändert, biegt sich die Welle zum Boden und gelangt weiter als normal. Die wichtigste Schicht ist, dass bis zu etwa 3 km, wo Inversionen, Kanäle und konvektiven Zellen [4] gebildet werden; die Wege auf dem Meer sind bevorzugt, weil die Wassermassen große Bereiche der Inversion [4] bilden. [3]

Die Größe, die zur Messung verwendet wird, ist refraktiv N, dh (n - 1) · 106 [11]:

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

T ist die Temperatur in Kelvin, P der Druck und Dampfdruck, beide in hPa.

Zählt als variiertes N mit Anteil (dN/dh). In der Standardatmosphäre beträgt etwa -39 N/km; bis zu -79 N/km wird die Refraktion als normal angesehen, neben der Superrefraktion und mit positiven Gradienten der Subrefraktion [7]. Unter -157 N / km ist die Welle mehr gebogen als die Krümmung der Erde und bleibt gefangen: Es ist der [11] (duktive) Kanal. Dies sind die Bedingungen, die die Hepburn- und F5LEN -Karten vorherzusagen versuchen.

## 6. Der Tropo-Index: von zwei Karten zum Durchschnitt

Hepburn- und F5LEN -Tabs sind der Kontrollschalter: Zeigen Sie die von der Seite jeder Quelle heruntergeladenen Bilder mit der veränderlichen Prognosezeit und dienen zur Kalibrierung des Lesens, indem Sie auf die Referenzpunkte und Farben der Legende klicken. Die Tropoindexschicht bleibt leer, bis mindestens eine der beiden Kalibrierungen nicht aktiv und vollständig ist.

Hepburn. Der Hepburn-Index misst, wie viele Wellen voraussichtlich über einen Bereich gefaltet werden, auf einer linearen Skala von 0 bis 10, die auch 10 überschreiten kann. 0 bedeutet Standardatmosphäre und nur Streuung; 1-2 marginale oder schwache Superbrechung; 3-4 Superbrechung mit Kanalbereichen; Der Index berücksichtigt nicht den Streutropo und sieht die [1] -Berge nicht. Die Karten umfassen 6 Tage: 3-Stunden-Pässe für die ersten 36 Stunden, dann 6 Stunden, täglich aktualisiert auf 18 UTC [1]; Die Startdaten (Temperatur, Luftfeuchtigkeit, Druck) stammen aus dem globalen Modell des kanadischen Wetterdienstes [1]. [1]

F5LEN. Für Europa veröffentlicht F5LEN 31 Karten von +3 bis +192 Stunden (alle 3 Stunden bis +48, alle 6 bis +96, dann alle 12), mit Datum und Uhrzeit im Titel, der Produktionszeit unten rechts und einer Klassenlegende: Disturb, Normal, Minor, Weak, Good, Strong, Vy Strong, Duct [2]. Die Weltvorhersage von F5LEN wird durch den Luftbrechungsindex in Einheit N in 40 Schritten von 3 Stunden (ca. 5 Tage) berechnet, die 4 Mal täglich aktualisiert werden; ein Klick auf die Karte zeigt den Durchschnittswert und den Ort des Punktes [2].

Verfahren. Für jeden Vorhersageschritt führt das Programm diese Schritte aus:

1. Entlastung. Die Bilder der beiden Prognosen werden im Cache neben der ausführbaren Datei heruntergeladen, um die Quelle einmal zu aktualisieren, nicht zu einem Redesign. Vor der endgültigen Version sollte die Erlaubnis für Hepburn und F5LEN angefordert werden.
2. Automatische Georeferenzierung. Das Programm findet den Rahmen (zwei horizontale und zwei vertikale Linien derselben Farbe) und liest unter Windows 10/11 mit dem in das System integrierten OCR die Achsetiketten (54N, 10W, 0, 5E...). Schneiden Sie die Streifen um den Rahmen, vergrößern Sie sie, testen Sie beide Zeichen, wenn der Buchstabe N / S / E / W nicht liest und halten Sie die Linie, auf die die meisten Etiketten fallen: die falschen Werte bleiben aus. Von hier aus können Sie das extreme Lat / Liter des Rahmens und die Art der Projektion (normaler Grill oder Mercator) erhalten. Die Extreme speichern sich und lesen sich neu, wenn der Rahmen die Größe ändert. Auf Mac und Linux, ohne OCR, sind Factory Extreme oder handschriftliche im Tab gültig. Alternativ für Karten, die keine lesbaren Achsen haben: Jede Art von Karte (Hepburn Europa, F5LEN weltweit...) hat eine Kalibrierung: Einige Kontrollpunkte mit bekannten Koordinaten (Kreuzungen von Meridianen und Parallelen, erkennbare Küsten) verbinden Pixel mit Breiten- und Längengrad, in der von dieser Quelle verwendeten Projektion. Die Kalibrierung befindet sich in einer Datei neben dem Programm und Sie können das Format neu erstellen, wenn sich das Format einer Quelle ändert.
3. Farbablesung. Jedes Pixel wird mit der Quellenlegende verglichen und in den nächsten Zahlenwert umgerechnet. Kosten, Schriften und Raster auf der ursprünglichen Karte werden als „keine Daten erkannt und mit den nahe gelegenen Werten gefüllt.
4. Weltnetz. Jedes Bild, sobald Sie den Rahmen finden und seine Extreme bemerken, wird auf der Erde platziert; der Wert an jedem Punkt der Welt wird von den Bildern abgeleitet, die es abdecken. Wenn mehr Regionen den gleichen Punkt abdecken, lohnt es sich, sie zu detaillieren (mehr Pixel pro Grad), solange ihre Zeit innerhalb von 1,5 Stunden von dieser Wahl liegt; dann die nächste Stunde, dann das letzte Rennen. Wo kein Bild den Punkt abdeckt, zeigt die Karte einen grauen Schleier: keine Daten, die sich von "Index 0" unterscheiden. Innerhalb des Rahmens ist ein Pixel ohne Legendenfarbe 0 (normale Atmosphäre); Küsten, Grenzen und Schriften werden durch die Betrachtung der nächsten Farbe innerhalb von 3 Pixeln übersprungen.
5. Gleiche Leiter. Hepburn ist bereits auf der Skala 0-10 [1]: dunkler Hintergrund = 0 und zehn Farben in der Reihenfolge (violett, blau, wassergrün, lind, gelb, ocker, orange, rot, rosa, hellrosa) für 1...10, lila darüber hinaus. F5LEN verwendet Klassen (Disturb, Normal, Minor, Weak, Good, Strong, Vy Strong, Duct) [2], aber einige haben genau Hepburn Farben: Weak = 1, Strong = 5, Vy Strong = 7, Duct = 8; Minor ist 0,5 und Good (grün) 3.5. Der Skalenmultiplikator im F5LEN -Tab korrigiert den Satz, um ihn mit Hepburn vergleichbar zu machen.
6. Gleiche Zeit. Die Zeitleiste geht von +0 bis +192 h in Schritten von 3 Stunden. Für jeden Schritt nehmen Sie von jeder Quelle die Prognose, die zum nächsten Zeitpunkt gültig ist; Wenn eine Quelle diesen Schritt nicht hat, verwendet das Medium nur den anderen und die Legende zeigt es an.
7. Medien. Mit dem Media-Button ist jeder Punkt wert (Hepburn + F5LEN) / 2; mit Hepburn oder F5LEN sehen Sie nur eine Quelle, nützlich, um zu verstehen, wo die beiden Prognosen nicht übereinstimmen.
8. Zeichnung. Der Wert wird mit der Legendenskala (0 blau, 2 grün, 4 gelb, 6 orange, 8 rot, 10 lila) gefärbt und als Stichprobe unter das Raster projiziert. Die 0 ist transparent, wo also nichts passiert, sehen Sie die saubere Grundkarte; Die allgemeine Transparenz passt sich mit dem Cursor neben der Box an.

Auf der Registerkarte vor Ort ist der Index entlang der Route der Durchschnitt der Gitterwerte Meisterschaften alle 10 km auf dem maximalen Kreis zwischen den beiden Stationen.

## 7. Isobar

Die hohen stabilen Drücke sind das erste Signal, nach dem nach dem [3] -Tropus gesucht wird, und die Verbindungen jenseits der IK3XTV -analysierten Alpen waren mit hohem [4] -Druck.

1. Daten. Druck auf Meereshöhe aus dem globalen Modell GFS von NOAA [12], gelesen durch den kostenlosen Open-Meteo [15] -Service auf einem Raster von 3° × 3° (von 30° bis 72°) N, von 30° W bis 48° E, für die nächsten 78 Stunden. Das Programm aktualisiert die Daten beim Start und dann alle 6 Stunden, wie das Model Racing.
2. Strecken. Das Isobare kommt aus dem Raster alle 4 hPa (...1012, 1016, 1020...) mit dem Algorithmus der Marschquadrate (Marschquadrate), dann werden sie leicht gerundet und projiziert nach Traktat gezeichnet.
3. Zentren. Die am meisten markierten lokalen Maximums und Minimums sind mit H (blau) und L (rot) und dem Wert in hPa gekennzeichnet.
4. Etiketten. Der Wert erscheint entlang jeder Isobra, auf einem Farbhintergrund der Karte, um über der Abtastung lesbar zu bleiben.

Zone und Stufe. Die Daten werden auf den Bereich heruntergeladen, den Sie sich ansehen, um 25% erweitert, wobei ein Schritt von 1. auf 20° gewählt wird, um unter 400 Punkten zu bleiben (Open-Meteo zählt jeden Punkt als Anforderung: 600 pro Minute, 5000 pro Stunde): Durch Vergrößern oder Verschieben der Karte aus dem Bereich, den das Programm bereits am dichtesten heruntergeladen hat, höchstens alle 10 Minuten; Wenn ein Update fehlschlägt, bleiben die vorherigen Daten erhalten und es wird nach 3 Minuten erneut versucht. Auf diese Weise isobar und Temperaturen arbeiten überall auf der Welt.

Temperaturen. Die gleiche Anforderung bringt die Temperatur auf 2 m, auf 925 hPa und auf 850 hPa. LA0BY zeigt, dass das Ereignis von 22/11/2018 bis UA6 in Temperaturkarten (900-3000 m) zu sehen war, aber nicht in Hepburn [3] -Vorhersagen; DF5AI und DK5YA verwenden vertikale Temperaturprofile, um [5] -Inversionen zu erkennen. Dazu gibt es zwei Schichten:

- Temperatur 850 hPa: Isoterm alle 4 °C behandeln (rot über Null, blau unten, lila auf Null).
- Thermische Inversion: orangefarbene Probenahme, bei der die Luft bei 925 oder 850 hPa wärmer ist als bei 2 m; je intensiver, desto stärker ist die Inversion. Es ist die physikalische Bedingung, die die Wellen zum Boden [3] [11] biegt und sie mit dem Tropo-Index und isobaren Shows vergleicht, wenn die Vorhersagen eine meteorologische Basis haben.

## 8. Spots und Stationen

Jeder Punkt mit beiden bekannten Positionen ist eine Linie auf dem maximalen Kreis zwischen den beiden Stationen, in Abschnitte unterteilt und projiziert; In Mercatore erscheint es dann leicht gekrümmt, in der Azimutale ist es gerade, wenn es von der Mitte ausgeht. Blaue Linie für den normalen Punkt, gelb bedeckt mit dunkler Kante für den Airscatter (<AS>, <ACS> oder "Airscatter" im Kommentar), ein Ball für jede Station, heller Halo unter der Linie, um ihn von der Probenahme zu lösen. Der ausgewählte Ort ist häufiger für den letzten und mit den beiden Namen konzipiert.

Zuhören. Jeder PSKReporter Die [16] -Empfangsstation ist ein dunkelrosa leerer Kreis. Von jedem Start voller rosa Linien zu den Stationen, die dekodiert und zurück auf dem Netz gesendet hat, immer sichtbar, ohne zu klicken, mit den Namen immer geschrieben. Durch die Auswahl eines Kreises sind seine Linien dicker. Spot und Listening zeigen nur das in der Liste gewählte Band und den Bereich an: Die Änderung der Bandbreite ist leer und wird mit den bereits für das neue Band gesammelten Daten neu gestaltet.

Von mir entschieden. Jede Station, die mit WSJT-X, JTDX oder MSHV (Empfang über UDP) dekodiert, ist eine rote Linie von Ihrer QTH zur Station, mit Ball und Nominierung. Es ist der direkteste Vergleich mit der Prognose: zeigt, wo Ihr Empfänger wirklich jetzt kommt.

## 8 bis. Farben und Symbole

Jede Farbe hat nur eine Bedeutung, die in allen Themen gleich ist (ändere nur den Hintergrund).

| Farbe und Zeichen | Bedeutung |
| durchgehende blaue Linie | Spot aus DX Cluster, RBN oder DM4X zwischen zwei Stationen |
| Gelbe Linie mit dunklem Rand | Spot mit Airscatter Kommentar (Reflexion am Flugzeug) |
| Dunkelrosa leerer Kreis | Hörstation auf PSKReporter (MQTT) |
| Dünne rosa Linien aus dem Kreis, mit einem rosa Ball an der Unterseite | Stationen, die der Zuhörer gehört hat, mit dem Namen immer geschrieben; dickere Linien für den ausgewählten Zuhörer |
| Rote Linie und roter Name | Ihre UDP-Dekodierungen (WSJT-X, JTDX, MSHV): Wer bist du |
| Rote Linienblinkstriche | QSO im Gange mit dieser Station: Er hat dich angerufen und das Closing ist noch nicht angekommen, oder ist der DX Call von WSJT-X/JTDX mit der aktivierten Übertragung. Weiter mit RR73, RRR oder 73 |
| Grüne Linie | QSO desunti dal live: schließende RR73, RRR oder 73 UDP Ansicht |
| Schwarze Linie mit weißem Rand | Jemand ruft dich an: es dauert bis zu RR73/RRR /73 oder bis zu 3 Minuten |
| Quadrate von 45° | Verkehr mit mindestens einer Station jenseits der gewählten Krone: Keine Linien, nur Quadrate Locator |
| Suche nach Lillas mit Entfernung nach Norden | 1000, 700 und 500 km um Ihre QTH; diese Wahl ist ausgeprägter |
| Kreis und Punkt in der Farbe des Themas | Ihre QTH, mit Name und Vermieter |
| Orange Ball mit GRAVES 143.050 | Das Radar GRAVES ( JN27SI) |
| Probe von blau nach rot | Hepburn Trope Index (0-10) oder F5LEN Klasse oder ihren Durchschnitt; wärmere Farbe, bessere Bedingungen |
| hellgrauer Samt | Bereich ohne Tropodaten für die gewählte Zeit |
| Graublaue Linien mit dem Wert in hPa | Isoberate alle 4 hPa; H in blauem Hochdruck, L in rotem Niederdruck |
| Rote, blaue und violette gestrichelte Linien | Isothermie auf 850 hPa alle 4 °C: rot über Null, blau unter Null, lila die Null |
| Probe orange | Thermische Umkehrung: Luft bis 925 hPa wärmer als am Boden, intensiver mit stärkerer Inversion |

In den Verdächtigen Berichten sagt der Rand jedes Tabs die geltende Regel: grün Gut, rot Scarta, grau keine Regel.

Was sind die lila Quadrate bei 45° bedeckt? Der Verkehr weg von Ihrem QTH interessiert Sie zu verstehen, wo die Ausbreitung offen ist, aber seine Linien, Hunderte von Kilometern lang und oft zehn, decken die Karte ab und verbergen, was für Sie zählt: die Öffnungen, die Sie von Ihrer Station aus nutzen können. Aus diesem Grund zeichnet das Programm über die gewählte Krone hinaus nicht die Linien, sondern färbt die Ortungsquadrate (2° × 1°) der beteiligten Stationen. Das Ergebnis ist eine Karte von «aktiven Zonen»: wo viele nahe gelegene lila Quadrate erscheinen, gibt es eine Leitung oder eine Öffnung, auch wenn es Sie nicht direkt betrifft; Wenn sich der Bereich zu Ihnen hin erweitert, ist es das Signal, die Antenne in diese Richtung zu richten. Das 45°-Dashboard lässt Sie unter dem Tropo-Index und dem Raster sehen, vergleichen Sie also auf einen Blick die Prognose (untere Farbe) und den realen Verkehr (Illus). Ihr QTH-Quadrat färbt sich nie, weil es der Ausgangspunkt und kein entfernter Bereich ist. Nur die Quadrate, die sich alle jenseits der Krone befinden, sind gefärbt: Ein Quadrat, das von der Krone gekreuzt wird, oder innen, bleibt frei, auch wenn es eine der beiden Stationen gibt. Um die Linien zu sehen, schalten Sie einfach die Box aus oder wählen Sie eine größere Krone.

Die Kriterien, die entscheiden, was auf die Karte kommt und was verworfen wird, werden einzeln in Dokument 1, Abschnitt 11 ter.

## 9. Zeitleiste

Unten auf der Karte: Datum und Uhrzeit der gezeigten Vorhersage, Cursor von +0 bis +72 Stunden, Indexlegende 0-10 mit dem Namen der gewählten Quelle und Uhrzeit des Modellrennens für isobar. Trompete und isobarer Index folgen dem Cursor; Spots, hörende Stationen und Ihre Dekodierungen sind Beobachtungen des Augenblicks und bleiben immer diese "Adex". Wenn der Cursor nicht auf +0 ist, werden sie abgeschwächt, um sich daran zu erinnern, dass sie nicht zur Vorhersagezeit gehören.

## 10. Wie man die Überlappung liest

- Hochdruck + hoher Index + Spot entlang der farbigen Fläche: Die Öffnung ist da und ist die geplante. Es ist Zeit, in diese Richtung zu rufen.
- Hoher Index aber kein Punkt: oder niemand ist dort aktiv, oder die Vorhersage ist zu optimistisch. Die Abhörschicht hilft zu unterscheiden: Wenn es Empfänger in der Gegend gibt und nichts fühlt, ist die zweite Hypothese wahrscheinlicher.
- Lange Flecken außerhalb der farbigen Bereiche: hoher Kanal, den die Oberflächenprognosen nicht sehen, wie beim LA- UA6 [3] -Ereignis; oder eine andere Ausbreitung entwich aus dem Filter (z. B. E sporadisch ohne Tag). DF5AI und DK5YA unterscheiden tropo und E sporadisch auch von der Stunde: Öffnungen, die geboren werden und zusammen mit einem diffusen E sporadisch enden, sind kaum [5] tropo.
- Hepburn und F5LEN sind sich nicht einig: Vergleichen Sie die beiden Quellen mit den Tasten und sehen Sie, wo die realen Flecken fallen.
- Druck nach unten: G4MCU folgte einer Öffnung auf der SK6VHF -Bake bis zum Druckabfall; 1012 hPa betrachtete das fertige Ereignis [13]. Beobachten Sie das Isobare, indem Sie die Zeitleiste scrollen: Wenn sich der Hochdruck zurückzieht, folgt ihm wahrscheinlich die Öffnung.
- Signale, die steigen: Im WSPR-Studio bei 144 MHz KP4MD wuchs das Signal/Rausch-Verhältnis direkt in den Fenstern, in denen Hepburn eine Verbesserung vorhergesagt hatte, und sank, als die Vorhersage [14] nahm. Wenn auf dem gleichen Weg wächst, bildet sich die Öffnung.
- Gebirgspfade: Der Index berücksichtigt nicht die [1] -Reliefs, aber die Verbindungen jenseits der Alpen existieren auch dank konvektiver Zellen und Wellen der atmosphärischen Schwerkraft [4], und LA0BY arbeitete 9A und S5 durch die Berge, ohne dass Hepburn es [3] vorhersagte.

## 11. Offener Punkt

- Erlaubnis von Hepburn und F5LEN, ihre Karten im Programm zu verwenden.
- Kalibrierung der Umrechnungskoeffizienten F5LEN → Skala 0-10.
- Kalibrierung von Kontrollpunkten jeder Art von Karte, neu zu erstellen, wenn Quellen Grafiken ändern.
- Optionale Schicht mit Temperatur in der Höhe (850 hPa) von der GFS.

## Hauptquellen

1. W. R. Hepburn, DX Info Centre, „The Hepburn Tropo Index (HTI)“ und „Tropospheric Ducting Forecast“ Seiten (dxinfocentre.com). Indexdefinition und -skala, negative Werte und mehr als 10, Ausschluss von Streuungen und Reliefs, Schritte von 3 und 6 Stunden an 6 Tagen, Upgrade auf 18 UTC. Herkunft der Daten aus dem globalen kanadischen Modell: G. Meisse, AMSAT-BB, 27/08/2007.
2. F5LEN, Prognose für Europa (tropo.f5len.org/forecasts-for-European) und Weltvorhersage der tropopherischen VHF-Ausbreitung (tropo.f5len.org/WW) sowie „Neue Version des troposphärischen VHF-Ausbreitungsprognose-Tools. Unit Index N, 40 Schritte ab 3 Stunden, 4 Updates pro Tag, Wert und Locator pro Klick.
3. S. Heck LA0BY, „Extreme Tropo Propagation on 144 MHz and up, 41° Nordic VUSHF Meeting, Skjeberg 2019. Normaler Gradient 6,5 °C / km und Inversionen bis zu 10-15 °C / km, Temperatur und Feuchtigkeit Rolle, hohe stabile Drücke, LA- UA6 -Ereignis sichtbar bei hohen Temperaturen und nicht in Hepburn, LA-9A / S5 -Verbindungen jenseits der Berge.
4. F. Egano IK3XTV, "Propagation VHF - Der Beitrag von konvektiven Zellen und atmosphärischen Gravitationswellen in der Tropo-Ausbreitung." Bedeutung der ersten 3 km, Meereswege, konvektive Zellen, Gravitationswellen auf den Alpen, Hochdruckanschlüsse, gemeinsames Volumen bis zu 3-4 km.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, "Very Long Distance Propagation in the 144 MHz Band", 24/7/2003. Vertikale Profile von Temperatur und Taupunkt zur Erkennung von Inversionen, Verwendung des Hepburn-Index, Unterscheidung zwischen Tropo und E sporadisch nach der Stunde.
6. J. N. Gannaway G3YGF, "Tropospheric Scatter Propagation", QST November 1983. Äquivalent des Erdradius und Verlust der für die Index- und Spotkarte verwendeten Bahn (siehe Dokument 1).
7. Das VHF/UHF DX Buch, Kapitel 2 „Troposphärische Ausbreitung, S. 2-23...2-30. Abbildung der Brechungszonen nach dN/dh (Standard -39 N/km, Grenzen -79 N/km und 0) und Oberflächen- und Hochkanalprofilen. Der Text der gelieferten Kopie ist nicht lesbar: nur die Zahlen wurden verwendet.
8. Projektzusammenfassung vom 8/11/2026, bereitgestellt vom Autor des Programms. Kartenfarben, erforderliche Projektionen, Schichten, Zeitleiste.
9. Natürliche Erde, Vektordaten von Küsten und Grenzen, gemeinfrei (naturalearthdata.com). Allgemeine Bezugnahme, nicht beigefügt.
10. J. P. Snyder, "Map Projections - A Working Manual", USGS Professional Paper 1395, 1987. Formeln von Mercator-Projektionen und azimutalem Equidtanti. Allgemeine Bezugnahme, nicht beigefügt.
11. Empfehlung ITU-R P.453, Radiobrechungsindex. Refraktive Formel N und Kanalschwelle bei -157 N/km. Allgemeine Bezugnahme, nicht beigefügt.
12. NOAA NCEP, Global Forecast System (GFS). Druck auf Meereshöhe bei 0,25°, vier Rennen pro Tag. Allgemeine Bezugnahme, nicht beigefügt.
13. G4MCU, Artikel mit dem Tag „troposphärisch (g4mcu.net/tag/troposphärisch), 2018–2025. Refration, Leitung und Streuung; häufigere Kanäle mit hohem Druck, insbesondere auf dem Meer; Leuchtfeuer SK6VHF folgte dem Druckabfall bei 1012 hPa (Juni 2019).
14. C. F. Milazzo KP4MD, ‚VHF/UHF WSPR Propagation Study‘ (qsl.net/kp4md/144_mhz_wspr.htm), 2012–2014. SNR der WSPR berichtet, dass 144 MHz in den von Hepburn vorhergesagten Verbesserungsfenstern wachsen und abnehmen, wenn die Prognose verschwindet.
15. Open-Meteo, Wettervorhersage API (open-meteo.com), kostenlose nicht-kommerzielle Nutzung. Druck auf Meereshöhe des GFS-Modells an mehreren Punkten in einer Anforderung.
16. mqtt.pskreporter.info, Vertrieb MQTT in Echtzeit der Werbespots von pskreporter. info: Nominierung und Vermieter der Empfänger (siehe Dokument 1).
