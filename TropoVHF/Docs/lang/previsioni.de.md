# Prognosehandbuch

> Maschinelle Übersetzung aus dem Italienischen: Im Zweifel gilt die italienische Anleitung.

Der Forecast-Tab berechnet die tropophärische Ausbreitung selbst: eine Indexkarte auf dem sichtbaren Bereich und das Signal zwischen Ihrer Station und einer entsprechenden. Dieses Handbuch beschreibt alle Daten, wie sie berechnet werden und aus welcher Quelle sie stammen. Die Zahlen in eckigen Klammern beziehen sich auf die Quellen unten; im Programm haben alle Daten der Ergebnisse ein Comic (Simbolo i) mit der gleichen Quelle und dem Verweis auf den Absatz.

## 1. Was er tut und was er tut

Die Vorhersage der vertikalen Temperatur- und Feuchtigkeitsprofile des GFS [1, 2] -Modells leitet die Reaktivität der Luft Schicht für Schicht ab und schätzt, wie viel das Signal im Vergleich zu einer normalen Atmosphäre gewinnt. Auf der Route fügt es den realen Boden (Copernicus GLO-30 [3]), Beugung, Troposcatter, Kanäle und Empfängergeräusche hinzu.

Es ist kein Maß: Es ist eine Schätzung, die auf einem Wettermodell mit entfernten Niveaus von Hunderten von Metern gemacht wird. Es dient dazu, Situationen zu vergleichen (heute besser als gestern, 70 cm besser als 2 m, diese behandeln besser als das), nicht um eine Verbindung zu garantieren. Absatz 14 erklärt, wie man Beweise sammelt, um sie zu validieren.


> "Die Ausbreitung kann nicht verallgemeinert, sondern in Bezug auf die lokale Morphologie untersucht werden." „
> — Volker Grassmann, DF5AI, berichtet von Flavio Egano, IK3XTV (Propagation auf 2 Metern)

Es ist das Prinzip, das die Punkt-Punkt-Berechnung leitet: Jede Route wird auf ihrem Boden berechnet, mit ihren Horizonten, ihrem Meer und ihren Reliefs; die Karte gibt das allgemeine Bild, behandelt die Antwort.

## 2. Sprachbefehle

- Band: 144, 432 oder 1296 MHz. Karte und Berechnung verwenden das gewählte Band.
- Jetzt: Der Cursor verschiebt die Vorhersage von jetzt bis 72 Stunden voraus, innerhalb von 3 Stunden.
- Update-Vorhersage (sichtbare Zone): Laden Sie GFS-Profile in einem Raster von bis zu 240 Punkten für die sichtbare Karte herunter. Der Rasterabstand (0,5° oder mehr) hängt von der Vergrößerung ab.
- Stop: Unterbrechen Sie einen Download oder eine Berechnung.
- Details zu zeigen (rechtes Feld der Karte): drei Ebenen, Essential, Normal und Complete, schalten Sie eine Gruppe von Boxen; jede Box kann dann von Hand wechseln. Die Boxen sind in drei Gruppen unterteilt:
  - Auf der Karte: Isolinee del Gain, ebenfalls 3–7 dB (Tracing), isobar, thermische Inversion, Wetterzeichen S ↓ ↑, Korrelationsverstärkung/Druck, Zone der direkten Sicht (Paragraphen 4, 5, 7 und 9 bis). Die Farben bleiben immer.
  - Bei der Berechnung des Signals: alle Zeilen der Berechnung, flaches Erdprofil, Berechnungsprotokoll und detailliertes Dateiprotokoll.
  - Korrekturen und Live-Daten: Wetterkorrekturen (Misme, Absenkungen), Luft-Meer-Kontrast, Durchschnitt mit Flecken (corridoi), APRS 2 m Sprünge und LoRa, jenseits des Berges ohne Verluste (Paragraphen 4 bis, 9 bis, 9 quater und 10 bis).
  - Essential hält nur Isolinien, Wetter- und Medienkorrekturen mit Spots; Normal fügt isobare, Inversion, Wetterzeichen, direkte Sicht, flache Erde, Luft-Meer und APRS hinzu; Complete schaltet es ein. Die Ebene ändert nur, was Sie sehen und was Korrekturen eingeben: Die Berechnungen bleiben gleich.
- Datenquelle und historischer Fall: woher die Profile kommen und welchen Tag Sie suchen (Paragraphen 3 bis und 3 ter).
- Meine Station und der Korrespondent: Locator, Antennen, Strom, Kabel, Lärm (Absätze 7 und 12).
- Berechnen Sie das Signal und die Abdeckung in der Optik: die beiden Berechnungen am Boden (Absätze 8-13).
- Berechnungsregister: jeder Schritt mit den verwendeten Zahlen, auch in der Datei Data/ forecast-calcoli.log.

## 3. Von GFS-Daten zum Profil

Für jeden Punkt des Gitters und jede Stunde lesen wir den Druck auf dem Boden, Temperatur und Feuchtigkeit bei 2 m, den Wind bei 10 m, die Höhe der Grenzschicht, die CAPE und Temperatur und Feuchtigkeit relativ zu 1000, 975, 950, 925, 900, 850, 800, 700, 600 und 500 hPa. Von 1.3.0 können Sie auch den Wind bis 850 hPa und die vertikale Geschwindigkeit bis 850 und 700 hPa lesen (Absatz 9 bis).

Die Werte werden mit der ipsometrischen Formel und der virtuellen Temperatur berechnet; die Werte unter dem Boden sind knapp. Für jede Ebene:

- N = 77,6/T · (P + 4810·e/T), mit T in Kelvin, P und in hPa und durch den Sättigungsdruck von ITU-R P.453-14, Gl. 9, mit dem Korrekturfaktor EF (im offiziellen Text neu geregelt) [4].
- M = N + 157 · h, mit h in km: die modifizierte Reaktivität. Wo M hochgeht, gibt es eine Leitung.

## 3 bis. Datenquelle

Das Menü Datenquelle wählt aus, woher die Profile kommen:

- Automatisch (empfohlen): GFS zuerst von NOMADS [14], dem NOAA-Dienst, der das Modell verteilt: Für jeden Moment fragt das Programm zwei Ausschnitte des Bereichs im G RIB2 -Format und liest sie selbst; NOMADS hat kein Tageslimit, nur eine pro Minute, die das Programm respektiert. Mit breiten Rastern (von 0,5° bis oben) verwendet es 0,5° oder 1. Dateien alle 3 Stunden, viel leichter. Die Punkte, die NOMADS nicht gibt, oder alle, wenn NOMADS nicht antwortet, fragen Sie Open-Meteo [2]. Die beiden Quellen geben das gleiche Modell und das gleiche Rennen, so dass Sie das gleiche Raster mischen können.
- GFS von Open-Meteo: nur Open-Meteo, wie in 1.2 (am Limit pro Minute wird erwartet).
- GFS von NOAA NOMADS: nur NOMADS. Langsamer herunterladen (zwei Dateien pro Stunde, eine halbe Sekunde zwischen einer Anforderung und der anderen, um das NOAA-Limit nicht zu überschreiten), verbraucht jedoch nicht das Open-Meteo-Limit.
- ICON-EU (DWD) und ECMWF IFS 0.25°, von Open-Meteo: andere Modelle als die GFS, die im gesamten Netz und für die Strecken gelten. Sie mischen sich nicht mit dem GFS: Vergleichen Sie zwei Modelle gleichzeitig ist ein einfacher Weg, um zu sehen, wie unsicher die Prognose ist. ICON-EU deckt nur Europa ab; ECMWF hat nicht alle GFS-Druckpegel (der fehlende Pegelsprung) und möglicherweise nicht die Grenzschicht.

Die verwendete Quelle wird in die Statuszeile und in die erste Ergebniszeile (Datenquelle) geschrieben, wobei angegeben ist, wie viele Punkte von jedem Dienst und wie viele aus dem Cache angekommen sind. Der Festplatten-Cache ist nach Modell getrennt.

## 3 ter. Historische Fälle

Die historische Das Fallmenü überprüft eine dokumentierte Eröffnung mit den GFS-Prognosen des Open-Meteo [15] -Archivs: Die Karte bewegt sich in den Bereich des Falls, das Band setzt sich selbst und der Zeitzeiger verlässt den ersten Tag des Falls. Durch Drücken der Update-Prognose laden Sie die Profile dieser Tage herunter; die Signalberechnung funktioniert wie bei der aktuellen Prognose. Es dient dazu, das Modell mit dem zu vergleichen, was wirklich passiert ist.

Der erste Fall ist der vom 14. bis 15. August 2022 in QST (November 2022, "The World Above 50 MHz" [23]): eine Öffnung unter hohem Druck zwischen Manitoba und Kansas / Missouri, von 144 bis 1296 MHz fast alle in FT8, mit dem kanadischen Rekord bei 10 GHz von 1228,56 km in Q65 und Signalen bis 1941 km. Die zweite ist die Eröffnung zwischen dem 29. Dezember 2021 und dem 2. Januar 2022 auf dem Tyrrhenischen und dem westlichen Mittelmeer bei 144 MHz, aus den Beziehungen des IC8TEM: erste Öffnungen auf 29 (bis zu 737 km), breiter auf 31 ( IT9GSF - EB5EA 1217 km), große Öffnung am 1. Januar zwischen Italien, Sizilien und Spanien. Für ältere Fälle (z.B. September 2000) hat das Open-Meteo-Archiv keine Daten. Im Archiv gibt es keine Grenzschichthöhe: dort gilt seine Korrektur nicht.

## 4. Index und Farben

Für jede Schicht zwischen zwei Ebenen vergleicht das Programm den N-Abfall mit den Schwellenwerten der Tabelle 6.5 von Bean und Dutton [5]: zum Beispiel zwischen 1000 und 850 hPa ist eine Abnahme bis zu 60 N/km normal, über 100 N/km ist Superbrechung. Eine superbrechende Schicht ist 10 bis 15 dB Verstärkung wert; eine Leitung, die das Band von 20 bis 25 dB einfängt, entsprechend dem Defizit von M. Starker Wind zum Boden, hohe Grenzschicht und hohe CAPE reduzieren die Verstärkung, weil sie die Schichten entfernen oder brechen [5, § 4.4].

Die Farbe ist die geschätzte Verstärkung in dB, mit einer festen Skala gleich in allen Themen:

| Gewinn | Farbe | Bedeutung |
| 7 dB | Niemand | Fast normale Atmosphäre |
| 7–10 dB | Blau | Gradient über normal |
| 10–13 dB | grün | superbrechende Schicht |
| 13–16 dB | gelb | starke Schicht, aber ohne Kanal für Band |
| 16–18,5 dB | Orange | sehr feste Schicht oder zu dünner Kanal |
| 18,5–21 dB | rot | Conduit Trapping Band |
| ab 21 dB | Purpur | Starker und tiefer Kanal |

Mit der Anzeigebox auch 3-7 dB Zonen zwischen 3 und 7 dB, in denen etwas gebildet wird, aber unter der Skala bleibt, können Sie mit einer hellgrauen Zurückhaltung sehen; Farben ändern sich nicht. Eine gepunktete Scheibe begrenzt den von der eigenen Prognose abgedeckten Bereich und zeigt den maximalen sichtbaren Gewinn: Liegt sie unter 7 dB, bleibt die Karte farbenlos, weil die Atmosphäre fast normal ist, nicht weil die Daten fehlen. Beim Herunterladen eines neuen Klicks auf Update wird die Prognose nicht unterbrochen; um sie zu stoppen, gibt es Stop.

Die Farben sind zu 82% undurchsichtig, so dass der Hintergrund des Themas sie nur ändert. Das Locator-Raster und die quadratischen Namen sind über den Farben gestaltet. Wo Ihre Vorhersage nicht da ist, aber Hepburn oder F5LEN geben Sie 5 oder mehr Schachkarten mit der Farbe dieser Quelle erscheinen.

Die Karte beurteilt jeden Punkt allein. Die Signalberechnung steuert stattdessen, dass die Ebene entlang der Route kontinuierlich ist (Absatz 9): Eine Route, die sehr gelb kreuzt, kann weniger ergeben, als die Karte Sie denken lässt.

## 4 bis. Prognose vermittelt durch Werbespots: Korridore (ab 1.3.3)

Copyright Policy: Ein Link allein darf niemals einen Kanal ziehen. Ein isolierter und kurzer Empfang kann die Streuung eines Transitflugzeugs sein (das APRS-Paket ist sehr kurz), eine Öffnung von wenigen Minuten oder ein Fehler, und es würde "Drop-Pipelines" geben, die sie täuschen. Das Programm erkennt einen Korridor, wenn im selben Band:

- mindestens 3 Paare von unabhängigen Bahnhöfen (keine gemeinsame Station) arbeiten auf Strecken von mindestens 300 km;
- die Strecken parallel verlaufen: Azimut innerhalb von ±15°, Durchschnittspunkte innerhalb von 150 km um die gemeinsame Achse;
- alle in den letzten 30 Minuten, wobei sich der Strahl im Abstand von mindestens 10 Minuten wiederholt (Persistenz);
- für die ARS zählt ein Sprung nur, wenn derselbe Sprung wiederholt wird;
- Ab 1.3.4 zählen die Routen über 1500 km nur, wenn der Strahl von mindestens zwei verschiedenen Quellen (z. B. PSKReporter und einem Cluster) stammt: Von einer einzigen Quelle bis zu diesen Entfernungen in 2 m sind häufiger EME, Streumeteor oder falsche Ortungsgeräte als Tropo.
- von 1.3.4, zählen Sie nicht die Stationen "zu ray": wer hat in der letzten Stunde mindestens 5 Korrespondenten über 1000 km verteilt auf einen Lüfter von mindestens 150°. In 2 m ist der Tropus gerichtet; ein Empfänger, der zusammen Israel, Portugal, England und Italien von 2000-3700 km "befreit", oft mit positiven Signalen, hat mit ziemlicher Sicherheit die Frequenz schlecht konfiguriert. In der 17-22/11/2026 I5MZY Archiv 72% der Unterkunft in 2 m über 1000 km kam von Stationen so; am 22/11 um 10:53 UTC waren OH6HLH, OZ1ITS, F4LUK, OM8ABC und SP2JLR, und mit dem Filter der längste Korridor steigt von 2175 km (Verzeichnis 1.3.3) auf 615 km.

Quellen: Cluster-Spot (nicht AS/ACS), PSKReporter über MQTT, seine QSO- und WSJT-X/JTDX/MSHV-Dekodierungen und APRS-Sprünge in 2 m und LoRa 433/868 MHz.

APRS: 2 m und LoRa. Das Programm liest APRS-IS in read-only (rotate.aprs2.net, filter auf Europa) und extrahiert Funksprünge zwischen bekannten Ortungsstationen (sorgente → digipeater → IGate), wie die VHF-Karte von DXView und APRS PropView (RF-YVY, Apache 2.0). Drei Bänder sind wert: 2 m, LoRa 433 MHz (Bandkorridore 432) und LoRa 868 MHz (Korridoi, die für 1296 zählen: eine Leitung, die den 868 MHz auch die höchsten Frequenzen bringt). Die Band stammt aus dem LoRa-Tocall (Aprs-Deviceid-Liste) und aus den Frequenzen oder Wörtern im Kommentar; ein Sprung ist nur zwischen zwei Stationen desselben Bandes wert.

Der heikle Punkt sind die aktiven Repeater: Ein bei 500 km empfangenes Signal kann mit zwei oder drei Digipeater-Wiederübertragungen ankommen. Ein Sprung zählt also nur, wenn er zwischen zwei realen und aufeinanderfolgenden Sendern liegt:

- Der Pfad muss verfolgt werden: jeder Digipeater, der erneut gesendet hat, hat seinen Namen angegeben, und der Sprung wird zwischen dem einen und dem anderen gemessen; ein Alias, der ohne Namen verwendet wird (W IDE1 *, W IDE2 *, regionaler Alias), bricht die Kette und diese Strecke zählt nicht.
- versteckte Wiederübertragungen: Das Programm erinnert sich an den Pfad, den jede Quelle normalerweise fragt (z. B. W IDE2 -2). Wenn ein Paket mit einem Sprung in weniger kommt (W IDE2 -1) und kein Name es erklärt, hat ein Digipeater es erneut gesendet, ohne zu verfolgen, und das Paket wird verworfen; die Quellen, die noch nie zuvor gesehen wurden, zählen nicht;
- Digipeater in der Mitte der Straße: Wenn sich zwischen den beiden Stationen, mehr oder weniger auf der Straße, ein Digipeater des gleichen aktiven Bandes in den letzten drei Stunden befindet, wird der Sprung verworfen, weil er erneut übertragen werden könnte (insbesondere für den Digipeater LoRa, der erneut übertragen wird, ohne den Pfad zu ändern);
- die Nicht-Funkpakete (qAC, qAX, TCPIP), die aus dem Internet über das Radio von einem IGate (dritte Teile), HF und verschiedenen Frequenzen, Stationen in einer Höhe von über 1500 m (Pallonen, Flugzeuge) und Sprünge über 1500 km geleitet werden.

Wirkung auf die Karte (ab 1.3.4). Es zeichnet nur den Bereich, in dem das garantierte Minimum gilt: ein Band von ±60 km um den zentralen Teil jeder Route (von 15% bis 85% ihrer Länge), nicht an den Stationen an den Enden, wo die Leitung möglicherweise nicht da ist. Die Bänder aller Korridore sind in einer neutralen Farbe (Text) und halbtransparent gezeichnet: Die Überlappungen addieren sich nicht, und die Farbe wird nicht mit dem Lila der Skala (21 dB und darüber hinaus) verwechselt. Die Etiketten (Eigenschaften, mittlere Entfernung, minimale Verstärkung) werden miteinander gescannt; vorbei mit der Maus auf einem Etikett erscheinen die Abschnitte des Korridors.

Im Bereich und nur in der Nähe der aktuellen Zeit (±90 Minuten) fällt die Prognose nicht unter ein Minimum. Die Argumentation ist: Wenn die Stationen wirklich passieren, ist die Ausbreitung da, was auch immer das Wettermodell sagt.

Denn kurze Routen sind wenig wert (ab 1.3.4). In 2 m, zwischen 300 und 600 km, verbinden sich zwei gute Stationen (etwa hundert Watt, Antennen, FT8 oder CW) fast täglich sogar mit der normalen Atmosphäre, für Troposcatter: das Signal, das durch die kleinen Unregelmäßigkeiten der Luft im gemeinsamen Volumen der beiden Bündel verbreitet wird. Drei oder vier Spots auf 500 km sagen, dass die Band wie gewohnt funktioniert, nicht, dass es eine Eröffnung gibt: GEKO Magazine n. 69 ( IU3QEZ) Schätzungen für den Troposcatter mit 2 x 10 fließenden Elementen von etwa 440 km in SSB QRP, 510 km in SSB mit 50 W, 600 km in CW QRP und 640 km in SSB mit 500 W (Quellen, elf Runden). Nur über 600-700 km reicht der Troposcatter nicht mehr aus und eine Reihe paralleler Verbindungen deutet wirklich auf eine Leitung oder Schicht hin. Bis zu 1,3.3 ein Korridor von 450-600 km erhöhte die Prognose auf 9-12 dB, und die Karte zeigte eine Verbesserung, die nicht da war.

Der Startwert geht daher mit der durchschnittlichen Entfernung der Routen:

- bis 400 km: 3 dB;
- 400 bis 700 km: Salz in linearer Weise von 3 bis 10 dB (500 km → 5,3 dB; 600 km → 7,7 dB);
- von 700 bis 900 km: von 10 bis 16 dB; über 900 km bleiben bei 16 dB.

Unter den 7 dB, also unter dem Anfang der Skala, bleibt das Minimum: Der Korridor wird mit seinem Etikett gezeichnet, es ist zu sehen, dass das Band funktioniert, aber die Farben der Karte ändern sich nicht (mit der Box zeigt auch 3-7 dB den grauen Punkt). Oberhalb von 7 dB wird der Wert halb und halb mit der Anzahl unabhängiger Routen (3 Striche → 0,25; 6 oder mehr → 1) und mit dem Median S/N von Beziehungen (-20 dB → 0,5; 0 dB oder mehr → 1; nicht bekannt, wie in Cluster-Spots, → 0,75) ab 7 dB gewogen. Beispiele: 3 Routen ohne S/N auf 500 km → 5,3 dB; 5 Routen auf 615 km → 7,7 dB; 3 Routen ohne S/N auf 900 km → 11,5 dB; 3 Routen bis -20 dB auf 900 km → 10,4 dB; 6 Routen bis 0 dB auf 900 km → 16 dB.

Der Mindestabstand bleibt 300 km: Eine Anhebung auf 500 km hätte auch die realen Kurzstreckenöffnungen entfernt (z. B. eine Leitung auf der Tyrrhenischen bei 400 km, die durch Wettervorhersage und sehr starke Signale mehr erkannt wird als aus der Entfernung allein). Im I5MZY -Archiv, am 22.11.2026 um 10:53 UTC, waren mit dieser Regel und dem Strahlfilter die längsten Korridore 615 km (7,7 dB) und 540 km (6,3 dB); mit den Regeln von 1,3.3 erreichten sie 2175 km und 16 dB.

Bis 1.3.3 galt das Minimum für das gesamte Rechteck des Korridors und hing nur von der Entfernung ab (von 10 dB bis 300 km bis 16 dB bis 900 km): Mit vielen langen Korridoren zusammen (zum Beispiel am 22.11.2026, zwölf Korridore bis 2620 km) hoben sich fast ganz Europa auf 16 dB an.

Die Gewinnkurven zeichnen nicht mehr «gocce»: die kleineren Inseln von 220 km in beiden Richtungen bleiben farbig, aber ohne geschlossene Kurve, es sei denn, ein Korridor bestätigt sie. Es schaltet mit der "Prevision vermittelt durch die Spots" ab.

## 5. Optionale Linien und Vergleich mit Druck und Inversion

Verstärkungskurven: Linien bei 7, 10, 13, 16, 18,5 und 21 dB, die Ränder der Treppe, auf dem Feld mit der Methode der Marschquadrate geglättet, mit dem Etikett in dB.

Isobar: Druck auf Meereshöhe alle 4 vom gleichen GFS bis zur Stunde der Vorhersage, mit den Zentren H und L.

Thermische Inversion: Kurven, die mit der Luftmenge abgedeckt sind, die bei 925 oder 850 hPa wärmer ist als bei 2 m, bei +1, +3, +5 und +8 °C.

Unter den Kontrollen gibt eine Linie die Verstärkung Übereinstimmung mit dem Druck und mit der Rückseite auf dem sichtbaren Bereich: Pearson Korrelationskoeffizient, -1 bis +1. Die Kanäle werden oft in Antizyklonen geboren (die Luft, die sich erhitzt und umgekehrt bildet), daher ist eine positive Übereinstimmung mit dem Druck normal; aber innerhalb desselben Antizyklons ändert sich der Gewinn stark zwischen Meer, Küste und Bergen. Umgekehrt reicht es nicht: Biegen Sie die Wellen nur, wenn über der Luft noch trockener [5] ist. Die Linie dient dazu, jetzt für jetzt zu sehen, welche der beiden am besten die farbigen Bereiche erklärt. Der gleiche Vergleich wird im Register geschrieben.

## 6. Potenzielle Pipelines: Doppelberechnung

GFS-Werte reichen von 200 bis 1000 m: eine dünne Inversion wird verteilt und das Modell sieht die Leitung nicht. Wenn zwischen zwei Ebenen die N-Abnahme die normale (40 N/km) übersteigt, die Luft stabil ist (thermischer Gradient unter 3 K/km) und darüber trocken ist (spezifisches Feuchtigkeitsverhältnis unter 0,8), berücksichtigt das Programm einen potenziellen Kanal und berechnet ihn auf zwei Arten:

- Berechnung A: alle konzentrierten überschüssigen Tropfen in 50 m.
- Berechnung B: die gleiche Abnahme verteilt auf die typische Dicke der Rohrleitungen dieses Gebiets, entnommen aus den Klimakarten von ITU-R P.453 [4] (auf dem Boden oder erhöht).

Für das Defizit und für die maximale eingeschlossene Wellenlänge wird der niedrigste Wert beibehalten: Es handelt sich um eine vorsichtige Schätzung.

ITU-R P.453 Daten sind © ITU und können für persönliche und nicht-kommerzielle Zwecke unter Angabe der Quelle verwendet werden, können aber nicht weiterverbreitet werden: dafür bin ich nicht im Programmpaket. Um die B-Berechnung einzuschalten, laden Sie einfach die P.453-Karten von der ITU im Tab herunter: Das Programm lädt nach dem Zeigen der Nutzungsbedingungen das Archiv der Pipelines von der ITU-Website (elevated_and_surface_duct.zip) für den persönlichen Gebrauch des Benutzers herunter, extrahiert die Dateien in Data/ P453 und schaltet die Berechnung ohne Neustart ein. Alternativ können Sie die digitale Produktseite der ITU-R Study Group 3 herunterladen (itu.int, ITU-R Study Group 3, Digital Products, voice P.453, Surface data and Elevated duct data) und die sieben S_OCCUR einfügen. TXT, S_THICK.TXT, S_STREN TXT, E_OCCUR.TXT, E_base.T Ohne die Dateien verwendet das Programm nur die Berechnung A und schreibt sie in den Tab und in die Registry. Das Verhältnis zwischen Defizit A und der typischen Intensität P.453 ist eine Anomalie: Wie oft ist die heutige Pipeline stärker als normal? P453

## 7. Stationen und Locators

Der Vermieter kann 4, 6, 8 oder 10 Zeichen haben. Mit 8 oder 10 Zeichen ist die Position genau und der Bodenanteil stammt von Copernicus GLO-30: Die Höhe der Antenne reicht vom Boden aus. Mit 4 oder 6 Zeichen ist das Quadrat zu groß: Es ist besser, das Kontingent der Antenne auf das Meer zu schreiben; Wenn 0 das Programm bleibt die Schätzung mit dem SRTM-Boden in der Mitte des Quadrats plus die Höhe vom Boden und signalisiert es in den Ergebnissen. Für die entsprechende ist die Standardhöhe 3 m über dem Boden; mit Sea Share 0 wird SRTM Boden plus Höhe verwendet.

## 8. Land, Beugung und Troposcatter

Das Streckenprofil wird auf Copernicus GLO-30 alle 30 m (mehr als 120 km breit) beprobt. Der äquivalente Erdradius ist k = 157/(157 + ΔN), wobei ΔN der Rückgang von N in den ersten Kilometern von den GFS-Profilen entlang der [6, 7] -Route ist.

Reliefs in der Nähe von Stationen (ab 1.3.2, Kriterium des Autors). P.452 zahlt den Horizontwinkel linear, 0,573 dB pro mrad: ein Relief von einigen zehn Metern ein paar hundert Meter von der Antenne kostet Dutzende dB, viel mehr als beobachtet wird. Aus diesem Grund können sich die Reliefs innerhalb von 20 km von jeder Station bis zu 6 dB addieren: Der ihnen geschuldete Horizontwinkel ist auf den der entferntesten Reliefs plus 10,5 mrad begrenzt. Es gilt für Ihre Station und für den Korrespondenten. Die Zeile «Near Reliefs» sagt, wie viele dBs abgehoben haben.

Landquelle (ab 1.3.2). Die Quoten stammen von Copernicus DEM GLO-30 [3]: 1 Sekunde Bogen, das Meer ist 0 und die Küste ist klar. Bis 1.3.1 verwendete das Programm die Terrain Tiles 'skadi' Boxen von AWS. In Küstennähe vermischen diese Plätze den SSRTM mit niedrig auflösenden Meeresbodendaten und verteilen den Anteil der Insel auf dem Meer: Um Capri herum entsteht ein Geisterring von 80-100 m zwischen 0,5 und 1 km vom Ufer entfernt. Am 21. November 2026 meldete IC8TEM einen nicht existierenden Horizont nach Nordwesten; der Punkt-für-Punkt-Vergleich mit Copernicus bestätigte ihn (94 m auf See nordwestlich von Marina Grande vs. 0). Dieser falsche Horizont von +2,6° kostete etwa 26 dB Troposcatter zu den Küstenstationen. Die Skadi-Scheiben bleiben nur als Reserve, wenn Copernicus nicht reagiert. Copernicus-Rahmen wiegen 15-30 MB und laden einmal herunter; diejenigen, die nur vom Meer stammen, existieren nicht und sind 0 wert.

- Freier Speicherplatz: Lfs = 92,44 + 20 log f(GHz) + 20 log d(km) [8].
- Beugung: Bullington-Methode auf dem realen Profil plus ITU-R P.452-16 § 4.2 [6] delta-Bullington sphärische Landkorrektur, mit ITU-R P.526 [9] sphärischen Verlust.
- Erste Fresnel-Zone: Radius √(λ·d1·d2/d); frei, wenn der Boden mindestens 0,6 des Radius verlässt. Die [9] -Messerkante wird für jedes Hindernis angezeigt.
- In der Gestaltung des Profils ist der direkte Radius grün in optischer und roter Ansicht, wenn er unter dem Boden oder unter der Krümmung der Erde verläuft; rote Punkte sind die Hindernisse, die in den 1. Bereich von Fresnel eindringen (violett gepunktete Linie). Auf dem Meer ist der "rote Punkt" in der Höhe von 0 m kein Berg: Es ist das Meer, das für die Krümmung aufsteigt. Passieren mit der Maus auf dem Radius oder auf einem Punkt erscheint ein Comic mit Erklärung und Werten.
- Wenn ein Relief in der Nähe der Antenne die Richtung schließt (z. B. ein Horizont von zehn Grad), ergeben Beugung und Troposcatter Verluste von Hunderten von dB: Über 300 dB schreibt das Programm "> 300 dB" und "Richtung durch ein benachbartes Hindernis geschlossen", das Signal "< -230 dB" und die S / N-Verhältnisse "-60 dB". In diesem Fall ist es ratsam, sich die «Empfehlte Pflanze» und «Via monte» anzusehen.
- Troposcatter: ITU-R P.452-16 eq. 45 bis 50% der [6] -Zeit, mit Winkelabstand von den Horizonten und N0 von den Profilen.
- Ohne Tropo: Summe der Beugungskräfte und Troposcatter, niemals unter dem freien Raum.

## 8 bis. Troposcatter: vier Modelle und Unsicherheit

Neben P.452 berechnet das Programm den Troposcatter mit drei anderen Modellen, ebenso wie das TropoLink [16] -Programm, und zeigt ihren Unterschied als Unsicherheit:

- NBS TN101 § 9 [25] (ab 1.3.5), Median: L = 30 log f − 20 log d + F(θd) − F0 + H0 + Lgp. F(θd) ist die Dämpfungsfunktion von Anhang III in seinen analytischen Formen (Ns 301) mit der Korrektur für Ns, H0 die Frequenzverstärkungsfunktion von § 9.2: bei 144 MHz, bei niedrigen Antennen in Wellenlängen löscht die Reflexion vom Boden teilweise das Feld im unteren Teil des gemeinsamen Volumens und das Verlustsalz von 3-10 dB. Ns ist das der Stunden GFS-Profile, die effektiven Horizonte und Höhen stammen aus dem Copernicus-Profil, Lgp = 0,07 · e ^ (0,055 (Gt + Gr)) ist der Öffnungskopplungsverlust. Es ist die gleiche Form, die das ITM in seinem Inneren verwendet, aber mit der Reaktivität von heute anstelle des Klimas. Es testet auf flachen Abschnitten bei 144 MHz mit Antennen in 20 m: innerhalb von 2-4 dB von ITM zwischen 150 und 900 km und etwa 2 dB in der vereinfachten Form 9.2b von TN101, die nicht über H0.

- ITU-R P.617-5 [17] L = F + 22 log f + 35 log θ + 17 log d + Lc, mit f in MHz, θ in mrad, d in km, F = 0,18· N0 · e^(-hs/7,35) − 0,23·ΔN und Lc = 0,07·e^(0,055(Gt+Gr)). N0 und ΔN werden in der Regel aus den Klimakarten der ITU entnommen; hier verwenden wir stündlich die Profile GFS ( N0 ·e^(-hs/7,35) ist die Reaktivität zum Boden, ΔN die Abnahme im ersten Kilometer), so dass auch der Troposcatter der Zeit folgt.
- RSGB VHF/UHF Handbuch Formel, DL2FZN [18]: L = Lfs + 24,44 + 4,283 ln f + 0.06656 d. Die Steigung mit der Entfernung ist fast gleich der von PROPBER (0,067 dB/km).

Gemeinsames Volumen (ab 1.3.1): ist der Punkt, an dem der Radius der beiden Stationen gekreuzt wird, jeder geneigt zu seinem Horizontwinkel, auf einer Erde mit äquivalentem Radius ae = k · 6371 km. Von der TX steigt der Radius als z = hTX + x·θt + x2/(2·ae); Der Abstand x = (hRX − hTX + d^θr + d2/(2·ae) / θ, mit θ = d/ae + θt + θr). Mit flachen Horizonten und Antennen bei gleicher Quote ist die Basis d2/(8·ae) wert: etwa 1,3 km bis 300 km, 3,7 km bis 500 km, 9,4 km bis 800 km. Je höher das Volumen, desto trockener und dünner ist die Luft und desto weniger breitet sie sich aus: Aus diesem Grund geht der Troposcatter um mehr als 800–1000 km aus. In den Ergebnissen sind der Anteil und die Position der Basis, die Reaktivität, der Gradient und die Feuchtigkeit des GFS in dieser Höhe und die äquivalente Entfernung von Bray (QST, November 1961): Jeder zusätzliche oder weniger Horizont ist etwa 111 km entfernt. Referenz: die Präsentation "Over the horizon in VHF" von IU3QEZ (GEKO Magazine n. 69).

Die Troposcatter-Linie und andere Modelle zeigen die beiden Verluste und Unsicherheiten als die Hälfte des Unterschieds zwischen dem optimistischsten Modell und dem pessimistischsten. Sie sind nur Vergleiche: der Verlust ohne Tropo bleibt der von P.452.

## 8 ter. Nachmittagstroposcatter über den Reliefs (ab 1.3.4)

IK3XTV, aus Thiene ( JN55, mit den Alpen bei 10 km), arbeitet in Sommernachmittagen Süddeutschland bei 400-500 km, ohne Sicht, mit starken Signalen, aber mit schnellen und tiefen Evaneszenz; der Autor schrieb sie zu "konvektiven Zellen ausgerichtet als ein Zug von Linsen". Die Beobachtung wird bestätigt; die im Programm angenommene Erklärung ist eine andere, physisch fester.

Am Nachmittag rührt die Sonne die Luft über die Reliefs und die Grenzschicht steigt auf 2–3 km an. An seiner Spitze trifft die feuchte Luft unten auf die trockene Luft oben: dort ist die Turbulenz des Brechungsindex (die Größe, die den Troposcatter beherrscht) maximal. Wenn diese Spitze das gemeinsame Volumen der Route erreicht, das auf 400-500 km etwa 2-3 km beträgt, wird der Troposcatter verstärkt. Dies erklärt die Nachmittagszeit, hohen Druck (stiller Himmel, starke Erwärmung) und schnelle Evaneszenz, typisch für Diffusion und nicht für die Kanäle. Konvektive Zellen wirken nicht als Linsen: Sie erzeugen Turbulenzen, und es sind die Turbulenzen an der Spitze der Neufassungsschicht, die das Signal verbreiten.

Konsequenzen für das Lesen von Prognosen:

- auf der Erde ist der erste Nachmittag die schlimmste Zeit für die niedrigen Kanäle und Schichten, aber nicht für den Troposcatter jenseits der Berge: die beiden Dinge sollten getrennt gelesen werden;
- Das Programm hat bereits die beiden Daten, die dienen, die Höhe der vom Modell bereitgestellten Grenzschicht und den Anteil des gemeinsamen Volumens der Route (Register "Common Volume" in den Ergebnissen): Wenn sie in der Nähe sind, kann ein Nachmittagsberg besser sein als die Berechnung in normaler Atmosphäre;
- ab 1.3.4 die Punkt-Punkt-Berechnung berücksichtigt: Wenn sich das gemeinsame Volumen auf der Erde befindet, gibt es entlang der Route Reliefs von mindestens 800 m, die Sonne ist über 15° und die Grenzschicht überschreitet 800 m, und die Oberseite der Schichtumformung (Modellbodenrate + Grenzschichthöhe) liegt innerhalb von 500 m von der Basis des gemeinsamen Volumens, der Troposcatter gewinnt 3 dB; zwischen 500 und 1000 m fällt der lineare Gewinn auf Null. Die Linie "Troposcatter Nachmittag". Bei Schnee entlang der Route (mindestens 10 cm auf dem Boden nach dem Modell, snow_depth variable von Open-Meteo oder SNOD des GFS von NOMADS) gilt die Verstärkung nicht: Auf dem Schnee bleibt der Boden kalt und die umgeformte Schicht wächst nicht. Der Wert von 3 dB ist vorläufig und muss mit echten Links kalibriert werden; er schaltet sich mit Wetterkorrekturen ab.

Quellen: Beobachtungen von F. Egano IK3XTV (Radio Rivista e www.qsl.net/ik3xtv, "Propagation auf 2 Metern", "Convective Cells", "Troposcatter over the Alps"), Quellen § 13.

## 9. Die Gründungsschicht entlang der Route

Es ist das Herzstück der Signalberechnung. Entlang der Route werden alle ca. 25 km GFS-Profile genommen. Für jede Druckschicht die längste Strecke, in der die gleiche Schicht an aufeinanderfolgenden Punkten vorhanden ist; zählen als Kanal nur dicke Schichten von mindestens 40 m [10].

- Kontinuität: von welchem Kilometer zu welchem Kilometer und der Teil der Strecke;
- Ober- und Unterteil sowie Neigung in m/km;
- Boden innerhalb der Schicht: der Kilometer, in dem sich die Berge über der Basis erheben;
- Bedecktes Land: Auf dem Meer ist die Leitung fast immer, entscheidet die Strecke des Landes [10]. Wenn das Land 10% der Route überschreitet, ist der verwendete Anteil das Minimum zwischen Kontinuität und Abdeckung der Erde.

Der Tropengewinn ist der Gewinn der Schicht für die verwendete Fraktion. Wenn mehr als die Hälfte der Profile subrefraktiv sind, ist die Verstärkung negativ.

## 9bis. Wetterkorrekturen: Stabilität, Senken, Streik

Ab 1.3.0 gibt das Profil auch drei meteorologische Informationen, die im Register (Regave «Stabilität (Misme)»), in der Meteo-Linie entlang der Route der Ergebnisse und mit der Segni Meteo-Box auf der Karte zu sehen sind.

- Stabilität von Misme [5, Kapitel 6]. Misme bindet das Feld jenseits des Horizonts an einen zusammengesetzten Parameter: M = a · (ge - 40 + b · ΔW), mit a = 0,5 dB pro N / km, dem äquivalenten Gradienten ge und der thermischen Stabilität ΔW, der Arbeit zum Anheben der Luft von 1 km. Das Programm berechnet ΔW = g·∫(θ − θ0)/θ0 dz zwischen Boden und 1 km darüber, mit θ Potentialtemperatur; die Standardatmosphäre beträgt etwa 56 J/kg. Der Term, der zur Verstärkung hinzugefügt wird, beträgt 0,07 dB für J / kg über den Standard hinaus, begrenzt zwischen -4 und +6 dB und zählt nur dort, wo keine Leitung das Band abfängt (der Gradient befindet sich bereits im Bean- und Dutton-Index). Die Kalibrierung ist temporär: Sie muss mit den Verhältnissen überprüft werden (Absatz 14).
- Senken und Aufwärtsluft [19]. Moler und Holden, auf Routen von 125-305 km zum Meer bei 1365 und 9380 MHz, fanden das höchste Signal von etwa 2,5 dB im Durchschnitt mit Luft kommen, dass mit Luft steigen, und in einem Fall eine Verstärkung von etwa 20 dB für 3 Stunden am Anfang der Senkung, gefolgt von einem Rückgang von 22 dB, wenn die Luft wieder konvergieren. Der GFS gibt die vertikale Geschwindigkeit auf 850 und 700 hPa. Wenn auf beiden Ebenen die Luft mehr als 1 cm/s fällt und es einen umgekehrten oder scharfen Abfall des potenziellen Brechungsindex K gibt (N adiabatisch auf 1000 hPa berichtet, was die trockene und warme Luft hervorhebt, die von oben [5] gefallen ist), wird 1,5 dB hinzugefügt; wenn die Luft um mehr als 5 cm/s steigt, wenn sie abheben 2.
- [20] Scirocco. Heiße und trockene Luft aus der Sahara über kühlere Meeresluft gibt sehr starke Inversionen vom östlichen Mittelmeer nach Gibraltar. Das S-Zeichen erscheint, wo bei 850 hPa der Wind von 100-200° mit mindestens 7 m/s kommt und die Temperatur mindestens 16 °C beträgt, auf dem Mittelmeer. Es ist nur ein Hinweis: Der Gewinn entscheidet über die Schichten des Profils.

Ohne einen Kanal, der das Band einfängt, übersteigt der Gesamtgewinn nicht 16 dB, das Ende des Gelbs: Korrekturen machen stärkere Schichten deutlicher, aber passieren sie nicht durch Kanäle (rot und lila bleiben an den Kanälen).

- Persistenz (ab 1.3.1). Die größten Eröffnungen kommen nach Hochdruck-Tagen bereits gut (VHF/UHF DX Book; Biancomano und Leyshon 1998, Regel von 12 Stunden; die Berichte vom 29.12.2021–2/1/2022 wachsen vom ersten bis zum vierten Tag). Für jede Zelle zählt das Programm die Stunden der vorherigen 24 mit einer Verstärkung von mindestens 13 dB und addiert sich proportional zu 2 dB, nur wenn die Verstärkung bereits mindestens 10 dB beträgt.

Wie man die Zeichen auf der Karte liest (Wetterzeichen ändern; ab 1.3.2 ist das Symbol groß und das Hinübergehen mit der Maus über ein Zeichen erscheint als Comic mit der Erklärung und den Werten der Zelle):

| Zeichen | Farbe | Was bedeutet das? | Wirkung auf den Tropus |
|---|---|---|---|
|  ↓  | grün | Absenkung: Lufttropfen (Antizyklon), erhitzt sich zur Kompression und trocken, stärkt die Inversion und hält sie fest | günstig, +1,5 dB |
|  ↑  | rot | Bergaufluft (vorne, Niederdruck, Konvektion): Mischt niedrige Schichten und bricht Inversionen und Kanäle | ungünstig, -2 dB |
|  S  | Orange | Sirup: Heiße und trockene Luft aus der Sahara über frischer Meeresluft | günstiger Hinweis, keine Korrektur: Bestimmen Sie die Schichten |

Ein Bereich voller Grün unter einem Antizyklon ist das klassische Zeichen einer harten Öffnung; Rote Pfeile entlang der Linie sagen, dass die Schichten brechen, obwohl die Farbe der Zelle immer noch gut ist.

Das Feld Wetterkorrekturen schaltet diese Korrekturen aus, um das Modell von 1.2 zu sehen.

Validierung (Absatz 14): Auf den 593 Verbindungen zu 144 MHz zwischen dem 29. Dezember 2021 und dem 2. Januar 2022 beträgt der Trennungsindex (0,5 = keine, 1 = perfekt) im Vergleich zu den gleichen Strecken zwei Wochen später 0,79 ohne Korrekturen und 0,91 mit Korrekturen (vor dem Dach bei 16 dB). Der gleiche Vergleich zeigt die Hauptgrenze: Der Kanal auf dem Meer in niedriger Höhe, der damals die Zeichen zwischen Kampanien und Ligurien brachte, ist auf den Ebenen des GFS nicht sichtbar.

## 9 ter. News 1.3.2: zwei Schätzungen, Decke, Hochdruck, Meer, Stationen, Kämme

vorsichtige und aggressive Schätzung. Der Gewinn aus dem Gradienten wächst von 0 dB bis zur Grenze des Normalen bis zu 10 dB bis zur Schwelle der Superbrechung. In der 1000-850 Schicht ist die vorsichtige Schwelle Bean und Dutton (100 N/km); die aggressive Schwelle ist die klassische Super-Refraktionsschwelle von 79 N/km (k ≈ 2; P.453 verwendet -100 N/km in den ersten 100 m), die mit k50 = 157/(157 - ΔN) k ≈ 2 ist (k = 2,5 entspricht etwa 94 N/km). Die Punkt-Punkt-Berechnung zeigt die beiden Verluste flankiert; mit einer Leitung, die das Band fängt zusammenfallen. Die Karte verwendet Vorsicht.

Dachboden von 2500 m. Straßen und Kanäle werden nur bis zu 2500 m über dem Boden durchsucht: Je höher die Antennen von Amateurfunkgeräten sie nicht ausnutzen. Mit 1500 m verlor die Validierung einen Trennpunkt und 4% der Anschlüsse oberhalb von 16 dB, denn im Winter liegen die Absenkungsumkehrungen oft zwischen 1500 und 2500 m.

Hochdruck verlängert. +1 dB, wobei mindestens 7 der 9 umliegenden Zellen einen reduzierten Meeresdruck von 1020 hPa oder mehr haben und der Wind in den niedrigen Schichten (10 m unter 5 m/s, 850 hPa unter 8 m/s) schwach ist, außer bei aufsteigenden Bewegungen.

Meeresgewicht. In der Punkt-Punkt-Berechnung wird der Gewinn der Schichten mit 0,8 + 0,4 μ1 multipliziert, wobei μ1 der Faktor von ITU-R P.452 (Gl. 51) auf dem längsten zusammenhängenden Landabschnitt ist: 1 auf dem offenen Meer, klein mit langen Landabschnitten.

Stationen und durchgeführt. Wenn eine Leitung, die das Band einfängt, nur einen Teil der Route abdeckt, wird der Anteil jeder Antenne mit der Basis und Oberseite der Leitung am Anfang und Ende des abgedeckten Abschnitts verglichen; der Gewinn wird mit 0,5 + 0,5 · √(e^(-ΔhTX/300) · e^(-ΔhRX/300)) multipliziert, wobei Δh die Entfernung in Metern aus dem Kanal ist. Mit der Leitung auf fast allen Routen ist die Kopplung von § 10.

Schichten und Grate. Übersteigt die Spitze der Grenzschicht des GFS (Ground Rate des most boundary_layer_height Modells) die Grate der Route (maximales Gelände über 300 m, wenn der höchste 500 m überschreitet), so überquert die Schicht die Berge ohne zu brechen: +1,5 dB für den Anteil der abgedeckten Grate. Es ist die Beobachtung von IT9GSF auf den Öffnungen von Sizilien nach Deutschland mit günstigen Tyrrhenischen.

Ebenen in der Nähe des Bodens auf dem Meer. Mit der NOMADS-Quelle lädt das Programm auch die Oberflächentemperatur (auf dem Meer ist die SST), Temperatur und Luftfeuchtigkeit bei 80 m und in der Schicht 30–0 hPa über den Boden herunter. An Seepunkten (Modellgeschwindigkeit unter 5 m) werden diese Werte dem Profil unter dem ersten Druckniveau hinzugefügt, um den Kanal auf 100-200 m zu sehen, den die Druckniveaus nicht lösen. Auf dem Boden werden nicht verwendet: die Nacht- und Tagesschichten sind noch nicht gültig. Open-Meteo gibt nur die Oberflächentemperatur an.

Verdampfungsleitung. Von SST, von Temperatur und Luftfeuchtigkeit bis 2 m wird die δ Höhe der Verdampfungsleitung mit dem neutralen logarithmischen Profil geschätzt (wie im Modell von Paulus und Jeske, ohne die Korrektur der Stabilität): Die Luft zum Haar des Wassers ist zum SST gesättigt (98% für das Salz), daher

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

begrenzt zwischen 0 und 40 m. Um 144 MHz einzufangen, würde es etwa 180 m dauern (Kerr), dann bei 144 MHz verstärkt der Verdunstungskanal wenig; von 432 MHz aufwärts, mit niedrigen Antennen auf dem Meer, viel (Absatz 11 bis). Geben Sie die parabolische Gleichung und in die Zeile Korrekturen 1.3.2 ein.

Tageszeit (Besuch). Das Programm berechnet die Höhe der Sonne an jedem Punkt. Von Tag zu Tag auf der Erde, mit der Sonne über 15° und Grenzschicht über 800 m oder Wind über 5 m/s, warnt davor, dass die Bodenkanäle neu geformt werden; bei Nacht, mit Sonne unter -3°, Wind unter 3 m/s und Grenzschicht unter 300 m, warnt davor, dass bei ruhigem Himmel durch Bestrahlung, die nach Sonnenaufgang fällt, geführt werden kann. Nicht auf dem Meer. Im Moment sind sie entdeckt, keine Zahlen: Die Dezember-Validierung hat nicht genug Sommertage, um sie zu kalibrieren. Dies sind zwei Punkte, die der Autor für wichtig hält, aus der täglichen Beobachtung.

Normalprofil CRPL (Alternative). Anstelle von 40 N/km fix folgt die normale Abnahme der exponentiellen Referenzatmosphäre CRPL: N(h) = Ns·e^(-c·h), mit -ΔN in den ersten km = 7,32·e^(0,005577·Ns) (Bean und Thayer 1959). Mit nasser Luft (Ns hoch) ist der Normale steiler und dient einem stärkeren Gradienten, um Gewinn zu geben; in der Höhe ist süßer. Stellt sich als schlecht heraus, bis die Validierung sagt, dass es hilft.

## 9 quater. Kontrast Luft-Meer und Tiefsee: die Adria (ab 1.3.3)

Radiowellen spüren den Meeresboden nicht: spüren Sie den Kontrast zwischen Luft und Wasseroberfläche. Die Tiefe zählt, weil sie entscheidet, wie schnell die Temperatur des Meeres der der Luft folgt.

Die Adria ist eine niedrige See. Die durchschnittliche Tiefe beträgt etwa 250 m, aber das nördliche Becken übersteigt selten 100 m und der Golf von Triest bleibt unter 25 m; nur im Süden steigt über 1200 m ab. Eine so dünne Wasserschicht erwärmt und kühlt sich in wenigen Tagen ab, der Tyrrhenier in Wochen. Es gibt drei typische Situationen:

1. Frühling und Frühsommer, günstig. Das Meer ist noch kalt, selbst für das Süßwasser der Alpen und Po; es kommt heiße und trockene Luft aus der Ebene oder dem Balkan. Heiße Luft über kaltem Wasser bildet eine Umkehrung auf dem Meer (Advektionspipeline): ist der klassische Zustand der Öffnungen zwischen Italien, Slowenien und Kroatien.
2. Herbst und Winter mit Bora, ungünstig. Kalte Luft über einem wärmeren Meer: Die niedrige Schicht wird instabil, drückt und der Kanal bricht.
3. Nach einer Bora, Chance. Nur weil das Meer niedrig ist, kühlt eine Bora es in wenigen Tagen einige Grad ab. Wenn Sie einen Antizyklon mit heißer Luft betreten, ist der Luft-Meer-Kontrast stark und der Kanal robuster als gewöhnlich.

Die Adria ist auch ein geschlossener Kanal zwischen Apenninen und Dinari, weniger als 200 km breit: Die Meeresschicht bleibt begrenzt, und die Routen entlang der Achse (Puglia-Venezia-Triest, etwa 700 km) bleiben alle auf dem Meer, die beste Bedingung für einen langen Kanal.

Was das Programm berechnet. Auf dem Meer Punkte, mit dem Wetter Korrekturen auf und die Box «Contrasto aria-mare»:

| Zustand | Berichtigung |
| --- | --- |
| Potentialtemperatur bei 925 hPa (oder 950) über die SST von 4 K | _ |
| über die SST von 7 K hinaus | +2 dB |
| mit Luftfeuchtigkeit bei 925 hPa (über 80%) | -0,5 dB auf diese Boni |
| gekühltes Meer von 1,5 °C oder mehr in 72 Stunden zuvor, mit günstigem Kontrast | _ |
| wärmeres Luftmeer bei 2 m 2 °C (Mischen) | -1,5 dB |
| wärmeres Meer von 4 °C | -3 dB |

Die Gesamtkorrektur bleibt zwischen -3 und +3 dB und ohne Leitung bleibt die Verstärkung unter dem gelben Dach. Die Schwellen sind temporär: In der Standardatmosphäre übersteigt die potentielle Temperatur mit 925 hPa die des Meeres von etwa 2–3 K, dann zeigen 4 K bereits eine Umkehrung an.

Die Erinnerung an SST. Modelle halten die Meerestemperatur während der Vorhersage fast konstant. Die jüngste Abnahme ergibt sich aus der Analyse der drei Tage zuvor (Open-Meteo, nur Oberflächentemperatur, nur Meereszellen): maximal 72 Stunden vorher minus dem aktuellen Wert. Wenn der Dienst nicht antwortet, gilt der Memory-Bonus nicht.

Grenzwerte. Die SST der Modelle unterhalb der Küste und in der oberen Adria ist grob; der kalte Stift des Po zum Beispiel ist schlecht gelöst. Die Batimetrie geht nicht in die Berechnung ein: Die Quellen geben immer die SST an, die bereits die Wirkung des Meeresbodens zusammenfasst. Der Luft-Meer-Kontrast überlappt sich zum Teil am Ende der Misme-Stabilität (§ 9 bis); dafür sind die Boni gering. Die Schwellen werden mit den Korridoren von Spot und QSO durch die Adria tarate gehen.

## 10. Durchführung auf der gesamten Strecke

Wenn die Schicht das Band auf mindestens 80% des verwendeten Anteils einfängt und die Stationen nicht in Sicht sind, wird auch der Verlust in der Leitung berechnet: freier Raum plus die Kopplung der Antennen (3 dB, wenn sie sich innerhalb der Leitung befinden, mehr, wenn sie sich über oder unter befinden), plus die Restdämpfung γd = 5 · 10−5 · ae · f ^ (1/3) dB / mrad von ITU-R P.452 eq. 49 [6]. Wenn es weniger als der Verlust ohne Tropo ist, wird der erwartete Verlust.

Ein Band bleibt nur in der Leitung, wenn seine Wellenlänge λmax = 2√2·(2/3)·√(ΔM·10−6)·d nicht überschreitet (Kerr, eq. 17 [11]). Dafür kann die gleiche Schicht die 23 cm und 70 cm einfangen, aber nicht die 2 m. Die Grenze ist jedoch nicht netto: Kerr warnt davor, dass sogar Wellen, die mehrmals länger sind, viel von der Leitung leiden, und Hepburn und das VHF / UHF DX Book sprechen von "sphered" Kanten und "forgiving" Kanäle. Ab 1.3.1 verwendet das Programm eine weiche Leitung: Wenn λmax zwischen λmax und 3 · λmax liegt, geht die Verstärkung allmählich von 15 dB (λ = 3 · λmax) zur vollen Leitungsverstärkung (λ = λmax) über, mit dem Faktor s = (λmax / λ - 1/3) / (2/3). In diesem Fall sind die Kanalfarben zulässig. Das Protokoll und die Meteo-Linie entlang der Route zeigen die Punkte mit weichem Kanal an.

## 10 bis. Via Monte

Wenn der Korrespondent hinter dem Horizont geschlossen ist, sucht das Programm die beste Richtung, um die Antennen zu richten: Die beleuchtete Steigung breitet das Signal als Radarziel aus. Es macht den Kreis des Horizonts jeden 5. (außer ±15° um den Korrespondenten) und nimmt in jeder Richtung den beleuchteten Horizontpunkt, über 100 m und zwischen 1 und 60 km. Eine schnelle Schätzung (freier Speicherplatz, bitaktische Umwandlung und 10 dB für jede Horizontebene vom Diffusor zum Korrespondenten) wählt die 4 vielversprechendsten, mindestens 15° voneinander; für jeden:

- behandeln 1, Station → Relief, im freien Raum;
- behandelt 2, Relief → Korrespondent, berechnet als ob das Relief von 10 m über der Spitze übertragen würde, mit der gleichen Vorhersage (ohne parabolische Gleichung);
- Umrechnung mit der bistatischen Radargleichung mit σ = σ0·A, σ0 = -15 dB (Berg in VHF/UHF) und A der vom R1 -Strahl beleuchteten Fläche (maximal 20 km2):

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

Die Linie "Empfohlener Punkt" zeigt den Azimut der besten Route an, wenn sie weniger kostet als die direkte. Bei 144 MHz, mit einem Relief bei 20 km und einem 15° yagi kostet die Bergstraße ca. 45 dB mehr als die 2-Strecke an sich: Es passiert nur, wenn die Direkte durch benachbarte Hindernisse geschlossen wird. Die Schätzung ist brutto (±10 dB), insbesondere für σ0.


Ohne Verlust für den Berg (Option, ab 1.3.2). Mit der Box «Via monte senza perdite fino al monte» wird der Fall zwischen der Station und dem Beugungspunkt als Null betrachtet: Es ist, als würde man die Station nach oben bringen (Antenne bis 10 m über dem Punkt, gleiche Leistung, Antenne und Kabel). Der Horizont wählt dann den Punkt mit dem freisten Horizont zum Korrespondenten (bei Parität der höchste) und das Ergebnis zeigt die Route vom Punkt zum Korrespondenten, mit Signal und S / N, neben der Summe mit der Diffusion. Es ist eine optimistische Grenze: sagt, ob sich die Richtung öffnet, wenn der Berg das Signal gut anhebt, oder wohin man tragbar geht. Jeder Punkt wird mit dem Zehn-Zeichen angegeben.

## 10 Ter. Eingang zu einer entfernten Leitung

Wenn der Kanal, der das Band fängt, weit über den Horizont der Station hinaus beginnt, muss das Signal eintreffen. Das Programm bringt das Signal zum Startpunkt, in der Mitte der Leitung, für die Beugung: Entlang des Copernicus-Profils sucht GLO-30 die Kante mit dem höchsten Fresnel-Parameter (ITU-R P.526), mit der Quellung des entsprechenden Landes,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

mehr die Dämpfung aufgrund der Krümmung der Oberseite (abgerundetes Hindernis, P.526-16 § 4.2), wobei der Radius R vom Copernicus GLO-30-Boden auf ±1 km vom Rand geschätzt wird;

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

(für m·n > 4 ist die Form 34b der Empfehlung). Barsis (NBS, 1963) gemessen auf drei langen Routen jenseits eines Berges (Colorado, Alaska, Europa) durchschnittliche Verluste 10-20 dB höher als der ideale Rand von Fresnel-Kirchhoff: die Rundung erklärt einiges (Barsis und Kirby, 1961: Pikes Peak mit einem Radius von etwa 11,5 km). Das Ergebnis vergleicht sich mit der Beugung auf glattem kugelförmigen Boden; es ist das Schlimmste. Dann wandert das Signal in der Leitung: Freiraum auf der gesamten Route, Restdämpfung γd von P.452 auf dem Abschnitt in der Leitung und Kopplung des Korrespondenten (3 dB innen, 1 dB alle 10 m außen). Wenn dieser Verlust geringer ist als der erwartete Verlust, wird er zum Mechanismus ("Verhalten (Input durch Beugung)"). Es ist die Prämisse des Autors auf der italienischen Geographie: mit einem nahe gelegenen Hindernis weit unter der Linie, v ist negativ und der Eingang kostet wenig; mit einer Rückenfront kostet 15-30 dB.

## 10 quater. ITM (Longley-Rice) und Austritt aus dem Kanal

Ab 1.3.2 enthält das Programm das Irregular Terrain Model 1.2.2 von NTIA/ITS (Longley and Rice, ESSA ERL 79-ITS 67, 1968; Hufford, Longley and Kissick, NTIA 82-100, 1982), übersetzt in C# durch den offiziellen Code der US-Regierung, der frei von Rechten ist. Die Übersetzung gibt die gleichen Zahlen wie das Original: Auto Compilation vergleicht es jedes Mal mit NTIA C++ Code in fünf Testfällen (unter 0,01 dB).

Die ITM berechnet den Verlust in normaler Atmosphäre auf dem realen Profil Copernicus GLO-30: gesehen mit Reflexion vom Boden, Beugung (Spindel- und Kugelland gewogen auf die Rauheit des Bodens Δh, das ist die Differenz zwischen dem 10. und 90° Perzentil der Quoten um die Gerade der minimalen Quadrate) und Troposcatter. Die Bodenreaktivität wird auf den durchschnittlichen Teil der Route, $N_s = N_0\,e^{-h/9460}$, reduziert und gibt die Krümmung des entsprechenden Landes an.

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

Das Programm verwendet das Radioklima der gewählten Route wie in § 10 sexies (bis 1.3.4 immer das gemäßigte Meer, mit N0 320 auf dem Land und 350 auf dem Meer), mit N0 aus der durchschnittlichen Reaktivität des Ortes (Boden εr 15, σ 0,005 S / m; Meer εr 81, σ 5 S / m), horizontale Polarisation und Variabilität im Laufe der Zeit ohne Ort (das Profil ist bekannt). Es zeigt den Verlust in 50% der Zeit überschritten und die, die Sie in 10% und 1% der besten Zeit haben. ITM modelliert die Kanäle nicht: Seine Variabilität im Laufe der Zeit ist statistisch, durch das Klima. Es dient als zweite Berechnung der Baseline neben P.452; der Unterschied zwischen den beiden gibt eine Vorstellung von Unsicherheit.

Die zweite Verwendung ist die letzte Strecke nach einer Leitung, die vor der entsprechenden endet. Am Austrittspunkt wird eine virtuelle Antenne auf halbem Weg durch die Leitung platziert und das ITM berechnet den Trakt zum entsprechenden, als Überschuss über den freien Raum in 50% der Zeit. Totalverlust ist

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,	heta_{condotto} + A_{ITM}(d_{uscita} 	o RX)
```

wobei A_TX die Kopplung der Antenne (3 dB innen, 1 dB alle 10 m außen) ist, wenn die Leitung in der Nähe beginnt, andernfalls die Eintrittsbeugung von § 10 ter. Es ist die Grenze, die der Autor sich selbst gegeben hat: Kontinuität der Schicht plus höchstens Restdämpfung. Wenn es niedriger als der erwartete Verlust ist, wird es zum Mechanismus ("Verhalten (Exit und Final Stretch ITM)").

## 10 Quinquies. Vertiefung: Inseln, Meerengen und Diskontinuität zwischen Erde und Meer

Ein Beispiel aus dem Golf von Neapel (Anacapri, JN70CN) zum westlichen Mittelmeer zeigt, wo Routen die Inseln überqueren:

| Bestimmungsort | Azimut | Wo es die Inseln überquert | Hindernis |
| --- | --- | --- | --- |
| Barcelona |  279°  | nördlich von Sardinien (Gallura) bis 41,05° N, zwischen 420 und 490 km | ca. 30 km südlich des Bocche di Bonifacio; der Limbara (1359 m) ca. 20 km von der Linie |
| Bocche di Bonifacio |  282°  | nahe 41,3° N | ca. 11 km |
| Marseille |  296°  | Zentralkorsika bei 42,2° N | Berge bis 2700 m: eine Leitung unter 2 km dort unterbricht |
| Valencia, Balearen |  260–265°  | Zentralsardinien | Innenreliefs |

Für Barcelona reicht es aus, auf 282° anstelle von 279° zu zeigen, weil die Achse von den Mündern ausgeht: 3., innerhalb des Strahls eines normalen Yagi.

Ist der Mund eine Meerenge? Für die niedrigen Kanäle, unter ein paar hundert Metern, ja: die Inseln sind ummauert, und über der Erde, am Tag, die Heizung bricht die Inversion. Die Meerenge selbst ist breit genug: etwa 11 km, gegen einen ersten Fresnel-Bereich, weniger als eine Kilometer breite Hälfte einer 1000 km langen Strecke in 144 MHz. Das Problem ist das Wetter. Die Münder sind ein Spaß für den Wind: Die untere Schicht wird beschleunigt und die untere Schicht entfernt. Oft ist der Durchgang als Geometrie offen, aber als Atmosphäre gestört. Mit einem stetigen Antizyklon und wenig Wind kann stattdessen die gute Passage sein.

Disparität zwischen Erde und Meer. Sie zählen. Eine an der tyrrhenischen Küste gebildete Leitung wird unterbrochen: Das Signal kommt aus der Leitung (der Abschnitt nach dem Ausgang, § 10 quater) und muss zu einer anderen Leitung auf dem Meer der Balearen zurückkehren und die Kupplung erneut bezahlen. Die Kanäle in der Höhe, über 1500 m, überqueren die Gallura, ohne es zu merken; für Zentralkorsika gilt nicht.

In der Praxis: Wenn die Route eine bergige Insel überquert, versuchen Sie ein gewisses Maß an Korrektur in Richtung des freien Meeres, aber zählen Sie nicht auf den Weg mit starkem Wind in der Enge.

## 10 Sexies. TN101 Radioklima und Zeitvariabilität (ab 1.3.5)

Der TN101 § 10 [25] beschreibt, wie der Verlust einer Route jenseits des Horizonts über den langen Zeitraum um den Median variiert, mit unterschiedlichen Kurven nach Klima: Die Y(q)-Funktion sagt, wie viele dB der Verlust in den q% der besten Zeit fällt. Die ITM enthält diese Kurven für sieben Klimazonen. Das Klima zählt viel: Auf dem Meer im gemäßigten Meeresklima, bei 144 MHz, verdient man in 10% der Zeit viel mehr dB als auf dem Land im Kontinentalklima, weil die Kanäle häufiger sind.

Das Programm repariert das Klima nicht mehr: Er wählt es für jeden Leckerbissen aus dem Durchschnitt, so dass es auf der ganzen Welt und nicht nur in Europa funktioniert. Download von Open-Meteo [26] drei Jahre täglicher Werte der ERA5 -Reanalyse (Temperatur, Feuchtigkeit und Bodendruck, Regen) für die Zelle von 0,5° des Durchschnittspunktes, nur einmal; Werte bleiben in Data / CLIMA. Wiederherstellung des Bodens

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Tag für Tag, dann der Jahresdurchschnitt und Exkursion zwischen monatlichen Durchschnittswerten, mittlerer Luftfeuchtigkeit und jährlichem Regen. Die Regeln folgen den Beschreibungen der Klimazonen des TN101 § 10.2; die Grenzen zwischen einem Klima und dem anderen sind ein Kriterium des Autors:

- über 60° Breite: gemäßigt kontinental (der TN101 hat keine Kurven für das polare Klima);
- weniger als 250 mm Regen pro Jahr und durchschnittliche Luftfeuchtigkeit unter 50%: Wüste (Ns typisch 280);
- innerhalb von 20° vom Äquator, Ns von mindestens 350 und Auslenkung bis 30: Äquatorial (Ns typisch 360);
- innerhalb von 32° und Exkursion von mindestens 60: subtropisch kontinental, dh Monsun (typische Exkursion 60-100);
- zwischen 10° und 32° mit Ns von mindestens 340: subtropisch marine (Ns typisch 370);
- beide Horizonte auf dem Meer, über 20°: marine temperiert auf dem Meer (es ist die Definition des TN101);
- Exkursion bis zu 30 nahe dem Meer (Meer auf der Route oder durchschnittliche Luftfeuchtigkeit von mindestens 75%): maritime temperiert auf der Erde;
- sonst kontinental temperiert (typische Exkursion 20-40).

N0 für ITM ist der auf den Meeresspiegel reduzierte Durchschnitt, N0 = Ns · e ^ (h/9460). Ohne Netzwerk verwenden Sie eine Reserveregel mit dem Breitengrad allein und dem Meeresanteil, und die Linie sagt es.

Tests mit Daten ERA5 2023-2025: Capri Ns 339 mit Exkursion 45 (kontinental temperiert an Land, Meer gemäßigt auf dem Meer, wenn beide Horizonte auf dem Meer sind); London 327 und 16 Wien 319 und 24 Abidjan 381 und 11 (äquatorial); Miami 365 und 36 (subtropischer Ehemann); Delhi 337 und 82 (subtropisch kontinental); Riad 278 und 37, 90 mm Regen (Wüste). In Chile: Antofagasta 341 und 19 (subtropischer Ehemann: die Küste des Atacama, mit der fast festen niedrigen Rückseite des Pazifiks), Calama 232 am Boden, 2257 m, 14 mm Regen (Wüste), Valparaíso 331 und 11 (Temperaturnaht), Punta Arenas 313 und 5 (Temperaturnaht).

Die Zeile "Radioklima ( TN101)" zeigt das Klima, den Grund, Ns mit der Hälfte der Exkursion und Y: wie viele dBs in 10% und 1% der Zeit verdienen und wie viele in 90% verlieren. Es ist eine langfristige Statistik und bleibt von der Prognose getrennt: Der erwartete Verlust folgt der Zeit von heute mit den GFS-Profilen, und die Summe der beiden Dinge würde zweimal die gleiche abnormale Ausbreitung zählen.

## 11. Vergleich mit ITU-R P.452

Die Linie P.452 wendet die ITU-Methode für Kanäle und Reflexion an Schichten (P.452-16 § 4.5 [6]) mit dem ΔN-Median des GFS anstelle des klimatologischen an und p = 20% als Kalibrationskonstante, wie in der CSIRO [10] -Studie. Es ist nur ein Vergleich: Der erwartete Verlust bleibt der von Ziffer 9.

## 11 bis. Parabolische Gleichung (ab 1.3.1)

Die Parabolische Gleichungslinie ist eine vierte Berechnung, nur des Vergleichs, die als professionelle Programme für die Kanäle (PETOOL; das Wellenverbreitungs-Framework von M. Lytaev, MIT-Lizenz) durchgeführt wird: Anstatt den Gewinn aus den Schichten zu schätzen, treibt sie die Welle entlang der Route mit der Fourier-Split-Step-Methode innerhalb des modifizierten GFS M-Brechungsprofils voran, das vom Prognosepunkt näher an jeder Entfernung genommen wird. Die GFS-Werte addieren die geschätzten Sprünge der Potentialkanäle (Berechnung A, 50 m in Halbschicht). Der Boden Copernicus GLO-30 tritt in Stufen ein (unter der Erde ist das Feld null), der Boden ist Leiter und der Strahl der Antenne ist ein Gauß mit der geschätzten Öffnung aus dem Gewinn. Die Berechnung sieht die Leitungen, diejenigen, die verlieren, die Beugung und die Kopplung der Antennen; Er sieht nicht die Troposcatter, die auseinander addiert wird. Als nächstes kommt der «Profilscheck»: das gleiche mit Standardatmosphäre weniger die mit dem erwarteten Profil.

Ab 1.3.2:

- der Propagator steht im Weitwinkel, mit der genauen Phase des freien Raums:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- der Meeresgrund und die Küstenebene in der Höhe Null spiegelt sich mit dem Koeffizienten von Fresnel in horizontaler Polarisation, εc = εr − j·60·λ·σ (Meer εr = 81 und σ = 5 S/m; Land 13 und 0,006 S/m), im freien Winkel der Route:

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- Die Rauheit des Meeres reduziert die konsistente Reflexion nach Miller und Brown, mit der effektiven Höhe der Wellen vom Wind auf 10 m (σh = 0,0051 · U2):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- im Meer das Profil M den Verdunstungskanal (Absatz 9 ter) mit dem neutralen logarithmischen Profil umfasst:

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

Es testet mit dem Prototyp (10 m Antenne auf dem Meer, Gewinn auf der Standardatmosphäre bei 100-300 km): mit δ = 10-30 m der Verdampfungskanal ergibt 1-3 dB bis 144 MHz, 8-37 dB bis 432 MHz und 21-66 dB bis 1296 MHz. Bei 144 MHz in horizontaler Polarisation spiegelt sich das Meer fast wie ein Metall (|η| mehr als 0,99 bis einige Zehntel Grad) und die Rauheit zählt wenig: Der neue Boden verändert sich um einige Zehntel dB; er zählt ab 432 MHz mehr.

Grenzen: Über der Erde bleibt das vertikale Profil des GFS-Niveaus, so dass die dünnen Kanäle unsichtbar bleiben; der Boden zu den Stufen wird angenähert; der Strahl bleibt innerhalb des 6. Platzes.

## 12. Antennen, Signal und Rauschen

- Yagi DL6WU: Verstärkung 9,5 + 7,8 · log10 (Boom / λ) dBd (2,35 dB für Doppelausleger, DL6WU misst auf VHF Communications 3/1982), plus 2,15 für [12] dBi.
- EIRP = Power − Kabel + TX Gain.
- Empfangenes Signal = EIRP - erwarteter Verlust + Verstärkung RX - Kabel RX. S-Meter IARU: S9 = -93 dBm, 6 dB pro Einheit.
- Externes Rauschen nach ITU-R P.372 [13]: galaktischer (Fa = 52 - 23 log f) künstlicher je nach Umgebung (Stadt, Wohnen, Land, ruhiges Land) oder einer handgeschriebenen Antennentemperatur.
- Tsys = Ta + Tpre + [290(Lc - 1) + 290(Fr - 1) · Lc] / Gpre an die Anschlüsse der Antenne (Friis-Formel). Ab 1.3.5 hat die Empfangsstation immer einen Vorverstärker in der Antenne, festen Parameter: Rauschzahl 0,8 dB (Tpre = 59 K) und Verstärkung 20 dB. Dahinter befinden sich das Kabel (Verlust Lc) und das Radio mit der Grundrauschzahl Fr 6 dB. Mit 1 dB Kabel sinkt der Kabel- und Funkeingang von ca. 1160 K auf 12 K: Das Rauschen entscheidet über Himmel, Umgebung und Vorverstärker. Der Gewinn von 20 dB ist ein typischer Wert, der vom Programm gewählt wird.
- S/N in SSB, CW und FT8 im Band jedes Weges im Vergleich zum gesetzten Schwellenwert.
- Periodenwahrscheinlichkeit (ab 1.3.2): Die Evaneszenz innerhalb der Stunde ist eine Normalverteilung des S/N um den erwarteten Stundenmittelwert. Bray (QST, November 1961, S. 39, auf den Kurven von Norton 1959) gibt eine Amplitude von 13,5 dB über etwa 100 Meilen, dh etwa 7 dB unter und 7 über dem Durchschnitt, der fast gerade unter 100 Meilen fällt (3,5 dB bis 50 Meilen); für eine "feste" Verbindung nimmt 7 dB aus dem Budget. ±7 dB als ±2σ: σ = 3,4 dB über 160 km, proportional zu der darunter liegenden Entfernung. Die Wahrscheinlichkeit, dass eine Periode die Schwelle des Weges überschreitet, ist

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  mit Φ Normalverteilungsfunktion: Marge 0 dB = 50%, +3,4 dB = 84%, +7 dB (die 'feste' Marge von Bray) = 98%. Es beinhaltet nicht die Unsicherheit des Modells, das größer ist.
- Digitale Wege: S/N in 2500 Hz im Vergleich zu den Schwellenwerten gemessen bei 50% Decodierung, ohne vorherige Information (AP). FT8 -21 dB und FT4 -17,5 dB bei weißem Rauschen; wenn der Mechanismus troposcatter ist, verwenden Sie die Schwellenwerte mit Evaneszenz "gestörtem Breitenmedium", FT8 -18,6 dB und FT4 -15,2 dB [21]. Q65 -15C -22,2 dB, Q65 -30B -24,8 dB, Q65 -60A -27,6 dB In Flugzeugstreuung kann der Doppler die Töne von Q65 -60A (1,7 Hz) überschreiten: Es gibt besser als 30B oder 15C. JT65A -25 dB [22]. AP-Dekodierung (Name des bereits bekannten oder Ende von QSO) gibt etwa 2 dB extra für FT8 und 1,5 bis 2,6 dB für Q65; es geht nicht in den Schwellenwert ein, weil es nicht für alle gültig ist Die «-24 dB»-Werte, die oft für FT8 gelesen werden, sind die Grenze der WSJT-X-Beziehungen oder AP-Dekodierung, nicht die Schwelle bei 50%. [24]

## 13. Antwort- und Datenzeiten

Jede Berechnung misst ihre eigene Zeit: GFS-Profile (Netzwerk, Analyse, obligatorische Pausen von 20 s zwischen den Chargen für das Open-Meteo-Limit), Copernicus GLO-30-Gelände (mit dem Download der Scheiben, jeweils 15-30 MB, nur das erste Mal), Berechnung. Die Antwortzeitlinie fasst sie zusammen; im Register beginnen die Zeilen mit TEMPI. Beim Upgrade des Grids meldet das Register auch das verwendete GFS-Rennen, wann es verfügbar wurde und wie alt die Daten sind.

Ab 1.3.2 können Sie auch ICON-2I, das Modell der Agentur ItaliaMeteo und Arpae auf Italien bei ca. 2,2 km, durch Open-Meteo (Modell italia_meteo_arpae_icon_2i, zwei Rennen pro Tag, 3 Tage) wählen. Es deckt nur Italien und die nahe gelegenen Meere ab und hat weniger niedrige GFS-Werte (1000, 925, 850, 700, 500 hPa): Für die Kanäle in der Höhe ist es nicht besser als die GFS, aber das feine Gitter beschreibt Brunnenküsten, Inseln und Meer in 2 m Höhe, das heißt den Verdunstungskanal. Die Variablen, die ein Modell nicht hat (Strate Limit, Temperatur der Oberfläche...), entfernen sich von der Anforderung. Ab 1.3.2 verwendet die automatische Quelle nur NOMADS; Open-Meteo dient, wenn NOMADS nicht reagiert und für historische Fälle. Mit dem Feld "Update auf eigene Faust" (standardmäßig beleuchtet) wird die Prognose ohne Klick heruntergeladen: bei der Eröffnung, bei jedem neuen GFS-Rennen auf NOMADS (ca. 5 Stunden nach dem Zeitpunkt des Rennens) und wenn der sichtbare Bereich mehr als ein Viertel aus dem Netz ist; höchstens ein Versuch alle 15 Minuten, niemals mit einem historischen Fall.

Anfragen bei Open-Meteo haben Limits pro Minute, pro Stunde und pro Tag für jede Internetadresse. Die heruntergeladenen Profile werden für jeden Strich und jedes Modell auf der Festplatte (Dati/cache-gfs) gespeichert: Beim gleichen Rennen, das alle 6 Stunden wechselt, fragen die bereits heruntergeladenen Punkte nicht noch einmal. Mit der automatischen Quelle starten Sie von NOMADS und Open-Meteo dienen nur für fehlende Punkte; mit dem einzigen Open-Meteo stoppt am Zeitlimit oder täglich sofort und sagt, wann Sie es erneut versuchen sollen. Historische Fälle werden nicht zwischengespeichert.

## 14. Validierung des Modells

Die Prognose sollte mit realen Verbindungen verglichen werden. Das Berechnungsprotokoll speichert jede Zahl; das Programmarchiv speichert Anzeigen und Decodierungen. Für 432 und 1296 MHz, wo die Spots nur wenige sind, benötigen sie Berichte von den Betreibern: wer hat die Verbindung hergestellt, zu welcher Zeit, mit welchem Signal. Vergleichen Sie viele Stunden lang den erwarteten Gewinn mit dem gemessenen Signal, wenn die Schwellenwerte verschoben werden sollen.

## Quellen

1. NOAA/NCEP, Global Forecast System (GFS), globales Modell bei 0,25°, vier Rennen pro Tag.
2. Open-Meteo, Prediction API (open-meteo.com), gfs_seamless Modell, Variablen bei Druckpegeln und Rennmetadaten.
3. Copernicus DEM GLO-30 (ESA; WorldDEM © DLR e.V. 2010–2014 e © Airbus Defence and Space GmbH 2014–2018, geliefert im Rahmen von Copernicus durch die Europäische Union und die ESA), 1 zweiter Bogen, GeoTIFF-Rahmen im offenen Register von Amazon Web Services. Reserve: NASA / USGS Copernicus GLO-30 in Terrain Fliesen "skadi" von AWS.
4. ITU-R P.453, Der Radiobrechungsindex: seine Formel und sein Brechungsdatum mit digitalen Kanalkarten (Studiengruppe 3, © ITU, persönliche und nicht-kommerzielle Nutzung).
5. B. R. Bean, E. J. Dutton, Radio Meteorology, NBS Monograph 92, 1966: Tabelle 6.5 und § 4.4.
6. ITU-R P.452-16, Vorhersageverfahren für die Auswertung von Interferenzen zwischen Stationen auf der Erdoberfläche bei Frequenzen oberhalb von etwa 0,1 GHz: § 4.2 (Beugung), eq. 45 (Troposcatter), § 4.5 und eq. 49 (durchgeführt).
7. ITU-R P.834, Auswirkungen der Troposphärischen Refraktion auf die Radiowellenausbreitung.
8. ITU-R P.525, Berechnung der Freiraumdämpfung.
9. ITU-R P.526, Ausbreitung durch Beugung: eq. 31 (cutcheon) und § 3 (Kugelland).
10. B. Indermuehle, H. Suzuki, Tropospheric Ducting Prediction based on GFS Model Data at Inyarrimanha Ilgari Bundara, the CSIRO Murchison Radio-astronomy Observatory, Publications of the Astronomical Society of Australia, 2026 (arXiv 2608.12861).
11. D.E. Kerr, Propagation of Short Radio Waves, MIT Radiation Laboratory Series vol. 13, 1951: eq. 17 e 18.
12. G. Hoch DL6WU, «Extremely Long Yagi Antennas», VHF Communications 3/1982, S. 130 ff., fig. 1 (gemessen zu 432 und 1296 MHz entsprechend der Länge des Auslegers).
13. ITU-R P.372, Funkgeräusche: galaktisches und künstliches Geräusch für die Umwelt.
14. NOAA/NCEP, NOMADS, GRIB Filter der GFS 0,25° Zeit (nomads.ncep.noa.gov); WMO G RIB2 Format, komplexes Packing mit räumlichen Unterschieden.
15. Open-Meteo, Historical Forecast API (Modellprognosearchiv seit etwa 2021).
16. LukiBox, TropoLink, Gestaltung von Troposcatter-Routen mit P.617, NBS TN101 und ITM flankiert (GitHub, Apache 2.0 Lizenz).
17. ITU-R P.617-5, Propagation prediction techniques and data required for the design of trans-horizon radio-relay systems, 2019.
18. RSGB, VHF/UHF Handbook; DL2FZN -Implementierung (andi-f/troposcatter auf GitHub, 2013).
19. W. F. Moler, D. B. Holden, «Tropospheric scatter propagation and atmospheric circulations», Journal of Research of the NBS 64D, 1960, doi 10.6028/jres.064d.014: signal beyond the horizon (1365 and 9380 MHz, on the sea) and vertical movement of the air.
20. ARRL Handbuch 2022 (99a ed.), § 19.4.3-19.4.4: Wetter und troposferical Ausbreitung.
21. S. Franke, B. Somerville, J. Taylor, The FT4 and FT8 Communication Protocols, QEX Juli/August 2020.
22. W3SZ, Schwellenwerte der langsamen digitalen Modi ( JT65, JT9, QRA64) und Wahl des Modus nach Ausbreitung, 2019.
23. QST, "Die Welt über 50 MHz", November 2022: Eröffnung vom 14. bis 15. August 2022 zwischen Manitoba und Kansas / Missouri.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Quick-Start Guide to Q65, 3. April 2021 (wsjt.sourceforge.io).
25. P. L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis, Transmission Loss Predictions for Tropospheric Communication Circuits, NBS Technical Note 101, Magazin 1967: § 9 (troposcatter, function F(θd), H0), § 10 und Anhang III (Radioklima und Variabilität über die Zeit).
26. Open-Meteo, Historical Weather API (ECMWF ERA5 Review seit 1940; Copernicus Climate Change Service, CC BY 4.0 Lizenz).
