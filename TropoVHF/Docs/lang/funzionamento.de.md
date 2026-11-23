LEBENSMITTEL · Technisches Dokument 1
# Durchführung des Programms

> Maschinelle Übersetzung aus dem Italienischen: Im Zweifel gilt die italienische Anleitung.

Was das Programm macht, von wo es die Daten nimmt und wie es sie in das verwandelt, was Sie auf der Karte und in der Liste sehen. Version vom 9. November 2026, bezogen auf das Projekt in C# (.NET 10 + Avalonia).

## Zusammenfassend

- Zweck: Tropo-Prognose (Hepburn [3], F5LEN [4], isobare GFS [18]) und reale Beobachtung von Bändern im selben Fenster, gemäß der LA0BY [1] -Kontrollliste.
- Datensammlung: zwei DX-Cluster, Reverse Beacon Network [9], dm4x Cluster und PSKReporter [5] MQTT Flow, UDP-Dekodierung von WSJT-X, JTDX und MSHV [8].
- Spot: Bandsegmente, Negativfilter, Airscatter in gestrichelt gelb, Position durch Locatoren oder CALL3.TXT [6] [7].
- Wer zuhört: dunkelrosa Kreise mit rosa Linien immer sichtbar in Richtung der Stationen gefühlt [5].
- Spot-Karte: Entfernung, Azimut, Tropo-Index auf dem Pfad und Troposcatter-Verlust mit der G3YGF [2] -Methode.
- Sprachen Hepburn und F5LEN: [3] Bild- und Kalibrierbank [4].
- Watch: NTP-Steuerung auf 10 Servern alle 3 Minuten [10] [11].
- Verdächtige Berichte: EME in Q65/JT65 erkannt mit der Position des Mondes, verkürzte Locatoren, Skimmer, Quarantäne und Zuhörer, mit Regeln, die von der Sonne [20] gelernt wurden.
- Kriterien für die Verwerfung und Bedeutung der in zwei Tabellen zusammengefassten Farben (Abschnitte 11 ter und 11 quater).
- Themen, 36 [17] -Sprachen, Export von Dokumenten nach Word mit editierbaren Formeln [16], tragbares Programm und Multiplattform [12] [13] [14] [15].

## 1. Zweck

Das Programm stellt in einem einzigen Fenster zwei Dinge zusammen, die normalerweise auf verschiedene Standorte schauen: Vorhersage der Troposferausbreitung (Tropo-Index von Hepburn und F5LEN, isobar) und Beobachtung dessen, was wirklich auf Bands passiert (Spot aus DX-Cluster, Reverse Beacon Network und PSKReporter, plus die Liste der Sender, die zuhören). Das Referenzband ist 144 MHz; die Spots sammeln sich ab 50 MHz.

Die Einstellung folgt der von LA0BY vorgeschlagenen Kontrollliste für extreme Tropo-Ereignisse: In den vergangenen Tagen schaut man sich die Hepburn- und F5LEN -Prognosen und die Temperaturprofile in der Höhe an, während sich das Ereignis auf dem DX-Cluster befindet und vor Ort den Hinweis darauf abbildet, dass die Eröffnung [1] beginnt. LA0BY beobachtet auch, dass die einzigen Hepburn / F5LEN -Karten nicht ausreichen, um die längsten Verbindungen vorherzusagen [1]

## 2. Das Fenster und die Tabs

Das Fenster ist eins und teilt sich nicht in mobile Fenster. Von oben nach unten:

- Allgemeine Einstellungszeile: Call und Locator links; rechts die UTC-Uhr (z. B. 08/11/2026 20:33:38 UTC, Bernsteintext), gefolgt von NTP:OK oder NTP:FAILED, dem Themenmenü und dem der Sprache.
- Anzahl Tabs in fünf Gruppen: Karte und Spot; Prognosen (Propria β, die vom Programm berechnete und im Forecast-Handbuch beschriebene Prognose, dann Hepburn und F5LEN); GRAVES; Daten und Quellen (DX-Cluster, PSKReporter, Reverse Beacon, UDP, Termine und verdächtige Berichte); Anleitungen (Programmbetrieb, Map Building, Wie man das Tropo formt, Manuelle Prognose, Tipps für den Operator, mit einem Suchfeld, in dem der Text angezeigt wird). Unter dem Balken zeigt ein Balken die Untersprachen der Gruppe; jede Gruppe erinnert sich an die zuletzt verwendete.
- Inhalt der Registerkarte und unten die Statusleiste mit dem Status der Quellen, der Namensdatenbank und der Uhrzeit des letzten Updates.

Die oberen Zeileneinstellungen gelten für alle Registerkarten: Das gewählte Thema färbt auch die Datensammlung und den Hintergrund von Dokumenten; Dokumentseiten bleiben so weiß wie ein Blatt, so dass sie gut gedruckt werden.

## 3. Call und Vermieter

Der Maidenhead Vermieter kann 4 oder 6 Zeichen haben (z.B. JN70 oder JN70CN). Das Programm wandelt es in die Koordinaten der Mitte des Quadrats um: Feld von 20° × 10°, Quadrat von 2° × 1., Unterquadrat von 5′ × 2,5′. Wenn der Text kein gültiger Locator ist, wird der Rand der Box rot und die Station erscheint nicht auf der Karte. Der Locator dient auch als Zentrum der Azimutprojektion und als Ausgangspunkt für Abstände und Azimut in der Anzeigentafel.

Der Call ist der Name, der als Cluster-Login verwendet wird. Beim ersten Start, bis es eine gültige Nominierung gibt, verbinden sich die Cluster nicht: Ein Fenster fragt nach dem Namen (und optional nach dem Vermieter); bestätigt, wird der Name als Benutzername im Login-Feld jedes Clusters kopiert, der leer ist, und Cluster verlassen. Mit "Later" bleiben Sie still, und der Connect-Button jedes Clusters erfordert die Nominierung erneut. MQTT, UDP und NTP verwenden kein Login und verlassen sowieso.

## 4. Datenerhebung: Quellen

Jede Quelle hat ihren eigenen Abschnitt in der Registerkarte Datensammlung, mit verbunden / getrennt, die erkannten und verbundenen Boxen beim Start, Status, Meter und Register der empfangenen Zeilen. Unbanded oder verworfene Linien aus dem Filter erscheinen nicht im Register: Zählen Sie nur die Abschnittsmesser. Die Abschnitte sind nebeneinander mit zwei zu zwei.

| Quelle | Anschluss | Was bringt |
| 1. DX Cluster Wahl | on0dxk.dyndns.org:8000 Befehlssatz/Filter dxbm/pass 6 | Spot von Amateurfunkgeräten, auch SSB und CW. Auf ON0DXK werden die VHF gefiltert und mit dem angegebenen Befehl aktiviert (poi pass 4, 2...). |
| 2. zweite Quelle | dxmaps.com:7300 (DXMAPS, EA6VQ) und s50clx.si:41112 ( S50CLX, Slowenien) | DXMAPS sammelt VHF-Spots und gibt auch RBN-Skimmer-Spots (Set / Skimmer-Steuerung, bereits eingestellt); S50CLX ist ein DX Spider-Cluster. Loggen Sie sich mit Ihrem Namen ein; editierbare Adressen. |
| 3. Reverse Beacon Netzwerk | telnet.reversebeacon.net 7000 CW/RTTY · 7001 FT8 | Automatischer Skimmerspot. Login mit dem Namen nur [9]. |
| 4. Cluster mit PSKReporter | dm4x.ddns.net:8500 | Digitale Spots von PSKReporter im Cluster-Format. |
| 5. PSKReporter MQTT | mqtt.pskreporter.info 1883 · 1884 mit TLS | Jeder Echtzeit-Empfangsbericht: Wer sendet, wer hört und seine [5] Locators. Bänder 144, 222 und 432 MHz. |
| 6. Wetter- und Tropokarten | dxinfocentre.com/tropo_eur.html tropo.f5len.org/forecasts-for-European Modell GFS | Hepburn [3] und F5LEN [4] Tropo-Indexkarten, Meeresspiegeldruck für isobar (GFS über Open-Meteo [18]). Hepburn und F5LEN müssen den Autoren erlaubt sein. |

Clustertelnet (Quellen 1–4). Nach der Verbindung wartet das Programm auf die Namensanfrage (Text, der Login, call:, callsign oder Ihren Anruf enthält); wenn es nicht innerhalb von 4 Sekunden ankommt, senden Sie bitte das Login. Senden Sie dann alle 0,6 Sekunden die Befehle des Abschnitts, getrennt von Punkt und Komma. Die Telnet-Steuerzeichen werden vor dem Lesen der Zeilen entfernt. Wenn die Verbindung fällt und Riconnetti aktiv ist, versuchen Sie es nach 10 Sekunden erneut (15 wenn der Server überhaupt nicht reagiert) [6].

PSKReporter MQTT (Quelle 5). Das Programm registriert sich im Thema pskr/filter/v2/{banda}/{modo}/{tx_call}/{rx_call}/{tx_grid}/{rx_grid}/{tx_dxcc}/{rx_dxcc} Fixieren Sie nur das Band und lassen Sie die anderen Felder mit dem Zeichen + [5] frei. Der genaue Name, mit dem der Server die Bänder angibt (z.B. 2m, 1,25m, 70cm), muss bei der ersten Verbindung überprüft werden.

## 5. Von der Clusterlinie zum Spot

Eine Zeile ist ein Punkt, wenn sie die Form DX de SPOTTER hat: Frequenz DX Kommentar hhmmZ. Das Programm leitet Spotter, DX-Station, Frequenz in kHz, Kommentar und jetzt UTC ab; die Namen nehmen die Skimmer-Suffixe ab (-#, -1...). Dann gelten in dieser Reihenfolge die folgenden Regeln [6].

| Band | Zugelassenes Segment |
| GRAVES (mit 144) | 143,040— 143,060 MHz |
| 144 MHz | 144—148 MHz |
| 222 MHz | 220 – 225 MHz (nur Region 2 ITU, Amerika) |
| 432 MHz | 420—450 MHz |
| 1296 MHz | 1240 bis 1300 MHz |
| 2320 MHz | 2300 bis 2450 MHz |
| 10 GHz | 10,5 GHz |

1. Band. Die Frequenz muss in eines der verwalteten Segmente fallen; der Rest, und insbesondere jeder Werbespot unter 49 990,0 kHz, wird nicht gezeigt und zählt als "out of band".
2. Weg. Eine der bekanntesten Arten des Kommentierens ist FT8, FT4, Q65, JT65, ISCAT, CW, SSB, FM, RTTY, PSK31, MSK144, FSK441, JT6M); USB und LSB werden zu SSB.
3. Negativfilter. Der Spot wird verworfen, wenn er die Modi MSK144, FSK441, JT6M, JTMS, ISCAT oder die Tags <AUE> <ES> <MS> <FAI> < F2 > enthält, mit nicht empfindlichen Großbuchstaben und sogar Leerzeichen in Klammern. Dies sind Wege und Ausbreitungen, die nicht tropo sind: Scatter Meteor, Aurora und sporadisch, F2.
4. Airscatter. Enthält der Kommentar <AS> oder <ACS> (auch gemischt, z.B. <As>) oder deklariert den Spot als Airscatter (Airscatter, Air Scatter, Aircraft Scatter), bleibt der Spot aber gelb mit gestrichelter Linie gezeichnet (dunkel gewellt, um auf der klaren Karte lesbar zu bleiben) und in der Liste hervorgehoben.
5. Standort der Stationen. Wenn es im Kommentar zwei Locatoren gibt (z. B. JN54 < JN83), ist der erste von denen, die den Spot machen, und der zweite von der Station DX; mit einem Locator ist das von der DX-Station. Was fehlt, wird in CALL3 gesucht. TXT, nur mit dem genauen Namen (für Laptops siehe unten). Bleibt eine Station ohne Position, erscheint der Spot in der Liste mit dem Hinweis „unbekannte Position, aber nicht auf der Karte.
6. Doppel. Ein Werbespot mit dem gleichen DX, dem gleichen Spotter, dem gleichen Band und der gleichen Quelle eines bereits vorhandenen ersetzt es, anstatt es hinzuzufügen.

CALL3.TXT ist die von JTDX verwendete Namensdatenbank → Locator im Format CALL,LOCATORE,,,. Die Projektdatei enthält 1 048 847 Nominierungen, alle mit gültigem Locator; der Header der Datei spricht immer noch von 34 323 Namen, weil er der der ersten Ausgabe [7] blieb. Es wird beim Start im Hintergrund geladen, so dass das Fenster sofort verwendbar ist. Die datei wird neben dem programm im datenordner, in dem ordner, aus dem das programm gestartet wurde, und in einem oder zwei ordnern darüber gesucht; wenn nein, zeigt ein fenster an, wenn sie starten, und die statusleiste signalisiert es in rot. Sein Name verwendet immer seinen 6-Zeichen. Nur Regel für die Locatoren: wenn der empfangene (Spot, MQTT, Decodierung, QSO desunti) weniger als 6 Zeichen und CALL3 hat. TXT hat eins bis 6, es lohnt sich immer das CALL3.TXT; für Laptops (F/ IC8TEM, IC8TEM/P) ist nur eine exakte Stimme: ohne, bleibt die Station ohne Vermieter und ist nicht anderswo konzipiert. Die Regel gilt auch zum Zeitpunkt der Zeichnung, so dass die Daten, die vor dem Laden der Datenbank angekommen sind, von selbst ausgeführt werden.

Eine Datenbank: CALL3.TXT. Ab 1.3.4 befinden sich alle Locatoren in einer Datei, CALL3.TXT, die im Format von JTDX und WSJT-X (CALL,LOCATORE,,,) verbleibt und verwendet werden kann, da sie auch in WSJT-X, JTDX und MSHV enthalten ist. Es gibt keine Dateien mehr auseinander. Oben in der Datei, nach dem Header, sind zwei Blöcke durch eine Kommentarzeile markiert: // [LIVETROPOVHF-MANUALI] mit den von Hand eingefügten Einträgen und // [LIVETROPOVHF-CLUSTER] mit den Korrekturen aus dem Cluster; dann // [LIVETROPOVHF-FONTI] mit allen anderen Elementen in alphabetischer Reihenfolge. Jede Nominierung erscheint einmal. In der Untersprache Daten und Quellen › Termine (CALL3) suchen Sie nach einem Namen, schreiben den Vermieter und drücken Add / change: Die Stimme geht in den Block der Handstimmen und es lohnt sich vor allem, auch über dem Locator, den die Station selbst schickt. ∫ Entfernt die Nominierung aus CALL3.TXT (das nächste Update nimmt sie aus Quellen wieder auf, wenn sie sie haben). Fügen Sie eine Datei hinzu... lesen Sie eine Liste CALL, LOCATOR (Sie akzeptieren auch Punkt und Komma, Tabellierung und Platz) und schreiben Sie sie zwischen handverlesenen Stimmen; die ausgewählte Datei wird nicht kopiert. Laden Sie CALL3.TXT erneut von der Festplatte nach dem Bearbeiten mit einem Editor. Vollständige Reihenfolge der Quellen: eigener Locator, Handstimmen, Clusterkorrekturen, der Rest von CALL3. TXT (Quellen, gelernte Live-Locatoren und G7VRD), erhaltener Locator (der nur für die Datenbank gilt, wenn er 6 Zeichen hat und nicht mit LL oder MM endet).

Update von CALL3.TXT. In der gleichen Untersprache lädt der Update-Button nun die Quellen (CALL3.TXT von WSJT-X Improved, CALL3.TXT EME von W7GJ, grid_data.bin von JTDX) herunter, schließt sich ihnen an (der erste, der einen Nominierungsgewinn hat), überprüft, ob es sich um mindestens 50.000 Namen handelt (sonst ändert sich nichts), speichert den CALL3. TXT Das bisherige CALL3. TXT-Einträge bleiben, und ein 6-Zeichen-Locator bleibt, wo eine Quelle nur 4 des gleichen Quadrats hat. Handstimmen und Korrekturen aus dem Cluster bleiben unverändert oben. Mit dem Update-Box auf eigene Faust jeden Tag, findet der Check zu Beginn. Das Programm schreibt CALL3 um. TXT unmittelbar nach jeder Stimme von Hand und, für gelernte Locatoren, höchstens alle 10 Minuten und beim Schließen; der CALL3. TXT des Pakets ist schreibgeschützt und das Attribut entfernt sich selbst. Linux-Installateure überschreiben einen CALL3 nicht. TXT bereits vorhanden; dass für Windows installiert es nur, wenn es fehlt. ALL3

Automatische Korrekturen aus dem Cluster. In den automatischen Spots DM4X und RBN bringt der Kommentar zwei Locatoren, zum Beispiel KN09tp<> KN09: Der erste ist das, was die Station, die Spots erklärt, zu 6 Zeichen; der zweite ist das der Station, die gefühlt wird, oft nur zu 4. Wenn der erste 6 Zeichen hat (und nicht mit LL oder MM endet) und sich von dem der Datenbank unterscheidet, korrigiert das Programm CALL3. TXT allein: die Korrektur geht in den Block // [LIVETROPOVHF-CLUSTER], mit dem Datum der letzten Bestätigung im dritten Feld gilt sie oberhalb der Quellen (unter den Handstimmen) und erscheint zwischen den verdächtigen Signalen aktualisiert als "CALL3 ". So überlebt es CALL3. TXT Updates. Eine unbestätigte Korrektur für zwei Jahre verliert den Vorrang und bleibt als normale Stimme.

Letzte Quelle: G7VRD. Wenn ein Nominierter kein 6-Zeichen in CALL3 hat. TXT fragt das Programm im Hintergrund nach der öffentlichen API von G7VRD (api.g7vrd.co.uk, "Locator by Callsign"), die Locatoren automatisch aus dem Netzwerkverkehr sammelt. Sie akzeptieren nur ein 6-stelliges Zeichen, das nicht die Mitte des Quadrats (LL oder MM) ist; die Anfragen werden gruppiert, maximal 40 Nominative pro Minute, und jeder Nominative fragt einmal am Tag, weil es sich um einen Hobbistic-Service ohne Garantien handelt. Der gefundene Locator tritt in CALL3 ein. TXT, wenn der Name nicht vorhanden ist oder nur 4 Zeichen des gleichen Quadrats hat; die Suche wird mit dem Feld im Namen sublingual deaktiviert.

Namen mit der Bar. In den PSKReporter MQTT-Themen trennt der Balken die Ebenen, dann kommen die zusammengesetzten Namen mit einem Punkt anstelle des Balkens ( IV3. HB9CAT). Das Programm stellt die Leiste zurück und wenn die Nachricht die vollständigen Namen enthält, verwenden Sie diese: IV3/HB9CAT bleibt IV3/HB9CAT auf Karte, Liste und Datenbank.

Locators haben live gelernt. CALL3. TXT enthält nicht alle: zum Beispiel SP9EMF und SP9NVY sind nicht da, und dann bleibt der 4-stellige Locator, der von PSKReporter gesendet wird (die Karte sagt es). Um diese Fälle zu reduzieren, schreibt das Programm in CALL3. TXT alle 6-Zeichen, die aus einer beliebigen Quelle (PSKReporter, wie Sender oder Empfänger, Cluster, UDP-Dekodierung) für eine Nominierung, die CALL3 kommt. TXT hat keine oder nur 4 Zeichen im selben Quadrat; ändert niemals eine Stimme in 6 Zeichen. Bis 1.3.3 befanden sich diese Locatoren in Data/ LOC6.TXT.

Übergang von 1.3.3. Beim ersten Start von 1.3.4 sind die Dateien abgesehen von den vorherigen Versionen (Dati/CALL3 -MANUALE.TXT, Data/CALL3 -AGGIUNTE, Data/CALL3 -CLUSTER). TXT, Data/ LOC6.TXT und Data/CALL3 - G7VRD.TXT werden zu CALL3 zusammengeführt. TXT mit den gleichen Einträgen. Schreiben freigeben, alte Dateien nehmen die.fuso-Erweiterung, Sie lesen nicht mehr und Sie können löschen.

automatische Empfänger. RBN, dm4x und PSKReporter melden oft Namen mit einem Suffix, das den Empfänger anzeigt: -SDR, /SDR, -# (Skimmer RBN) oder -1, -2... (plus Empfänger der gleichen Station). Für den Vermieter wird das Suffix entfernt: OK2OZL -SDR nimmt OK2OZL JN89UE von CALL3.TXT, wenn die Station nicht bereits einen 6-stelligen Locator sendet. Der angezeigte Name bleibt der mit dem Suffix.

Locators geschnitten. Nicht alle Quellen heilen die Erweiterung ihres Vermieters: Viele setzen es auf 4 Zeichen, und einige automatische Übertragssysteme schneiden absichtlich den 4-Zeichen-Locator ab, auch solche, die die Daten aus dem realen Abhören von FT8, FT4, Q65 und JT65 erhalten oder mit der Mitte des Quadrats vervollständigen (LL oder MM, z. B. JN89MM). Aus diesem Grund wird ein 4-Zeichen oder 6-Zeichen, das mit LL oder MM endet, durch das CALL3 ersetzt. TXT des gleichen Quadrats. Wenn die richtige Lage und die Mitte des Platzes nicht mit einigen Dutzend Kilometern übereinstimmen, bleibt der Fehler im Vergleich zu den Tropo-Entfernungen gering.

## 6. Wer zuhört

Jede MQTT-Nachricht von PSKReporter meldet den Namen und den Locator derjenigen, die [5] erhalten, so dass Sie nicht in CALL3.TXT danach suchen müssen. Für das im Selektor gewählte Band gilt das Programm für jede Empfangsstation: Vermieter, letzter Empfang, dekodierte Wege, Anzahl der Stationen gefühlt und maximale Entfernung. Eine Station verlässt die Liste, wenn sie nichts mehr im gewählten Bereich (3, 5, 10, 15, 30 oder 60 Minuten) erhält.

Auf der Karte ist jeder Empfänger ein dunkelrosa leerer Kreis, und von jedem Anfang an volle rosa Linien zu den Stationen, die sich anfühlen und zum Netz zurücksenden. Die Linien sind immer zu sehen, ohne zu klicken. Termine der Abhörstationen, der Gehörten und der beiden Köpfe jedes Spots werden immer auf die Karte geschrieben, ohne klicken zu müssen (der gleiche Name am selben Punkt wird nur einmal geschrieben). Durch Klicken auf einen Kreis werden seine Linien noch dicker und öffnen den Tab mit Details. Wenn der Locator von MQTT weniger als 6 Zeichen hat, verwendet das Programm das von CALL3.TXT (etwa eine Million Namen), vorausgesetzt, es stimmt mit dem empfangenen Quadrat überein.

Scatter Meteor. Wie bei Cluster-Spots werden MQTT-Verhältnisse in MSK144, FSK441, JT6M, JTMS oder ISCAT verworfen und nur gezählt; dasselbe gilt für UDP-Dekodierungen auf diese Weise.

Keine echten Zuhörer. Einige Stationen erklären, in 144 MHz zu empfangen, aber in Wirklichkeit befindet sich der Empfänger auf einem anderen Band (falsche Frequenzanzeige): Ihre Berichte würden unexistierende Öffnungen von Tausenden von Kilometern entwerfen. Das Programm überprüft es von selbst. Jede Station, die ein Zuhörer in VHF hört, wird 30 Minuten lang auf allen Bändern mit einer dedizierten MQTT-Inschrift verfolgt (pskr/filter/v2/+/{nominative}/#) [5] dienen als Beweis. Wenn mindestens zwei der von einem Zuhörer gehörten Stationen in den gleichen Minuten (±10) auf anderen Bändern aktiv sind, gilt der Zuhörer als nicht echt und alle seine VHF-Berichte werden verworfen; das MQTT-Register meldet den Namen und die Stationen, die ihn verraten haben. In der Liste der Punkte, neben den Metern, zeigt die MQTT-Taste den Zustand des Flusses an (grünes Blatt, wenn nicht verbunden, rot, wenn nicht) und wie viele Zuhörer in dem ausgewählten Band und Bereich verworfen wurden; Durch Klicken öffnet sich die Registerkarte mit der Liste der Verwerfungen und für jede Station auf anderen Bändern.

Langes Verbindungsporzellan. Ein Link über 1000 km (Clumbing Spot, MQTT Listening, QSO Desunti) wird nicht sofort angezeigt: bleibt gehalten, und der Zähler "gehalten > 1000 km" sagt die Nummer. Es zeigt sich, wenn eine andere Verbindung, zwischen verschiedenen Stationen und mindestens die Hälfte entlang, es mit einem dieser Kriterien bestätigt: gleiche Station an einem Kopf (zum Beispiel der gleiche Zuhörer) und die andere Station in der gleichen Richtung innerhalb von 10°; ähnlicher Korridor, mit beiden Köpfen innerhalb von 200 km von denen der anderen Verbindung; parallele Route, mit Richtung innerhalb von 10° und Seitenabstand innerhalb von 200 km, überlappt entlang der Route. Beide werden angezeigt und das MQTT-Log zeigt den Grund an. Seine UDP-Dekodierungen sind immer sichtbar und gelten als Bestätigung für andere. Die zurückgehaltenen Verbindungen bleiben 60 Minuten im Speicher.

Nichts unter 144 MHz. Das Programm arbeitet ab 144 MHz: Alles unter 143 MHz wird verworfen. Die Ausnahme ist GRAVES, das französische Radar, das in CW auf 143.050 MHz von Broye-lès-Pesmes ( JN27SI) sendet: ein Werbespot zwischen 143.040 und 143.060 MHz oder mit GRAVES im Namen erscheint zwischen 144 MHz mit Radar als Sender. Die Anzeigetafel sagt, ob sich der Empfänger innerhalb des Strahls befindet (Sektoren D, C, B, A von 90° bis 270° Azimut vom Radar aus gesehen, 6 Schritte von jeweils 7,5°) oder auswärts; die Registerkarte GRAVES zeigt wie in YA-MS den vom Radar aus gesehenen Himmel: Sektoren, zu diesem Zeitpunkt beleuchtete Stufe (sequenza PE1ITR 2021: 0,8 s pro Schritt, Zyklus 4,8 s Daten und Geometrie stammen von YA-MS. Es in der Trope zu fühlen ist schwierig, aber es kann passieren.

Rehabilitation von menschlichen Clustern. Eine Station, die auch auf anderen Bändern erscheint, zählt nicht als verdächtig, wenn in den gleichen Minuten (±30) die DX-Mensch-Cluster ( ON0DXK, GB7RAU; nicht dm4x, RBN oder andere automatische Quellen) den aktiven Schaden auf dem betreffenden Band an Stellen, die von anderen Namen gemacht werden, anrichten: Es ist der Fall von Stationen, die wirklich auf mehreren Bändern zusammenarbeiten. So kehrt ein verworfener Zuhörer sichtbar zurück (das MQTT-Register schreibt "rehabilitiert") und ein lange gehaltener Link für niedrige Bänder kann enthüllt werden.

Aktivstationen unter 144 MHz. In der Quarantäne der langen Verbindungen bestätigt eine Station, die in den gleichen Minuten auf Bändern unter 144 MHz zu hören ist (aus den MQTT-Berichten der anderen Bänder), andere Verbindungen nicht und wird nicht enthüllt, obwohl eine ähnliche oder parallele Route dies bestätigen würde.

## 7. Liste der Werbespots

Es nimmt 35% rechts von der Karte Tab und Spot. Oben: Titel, Band-Selektor (50, 70, 144, 222, 432, 1296, 2320 MHz, 10 GHz), Range-Selektor (letzte 3, 5, 10, 15, 30, 60 Minuten) und Update-Taste. Unten, vier Meter: sichtbare Flecken, AS / ACS, aus dem Filter verworfen, Stationen hören.

Jede Zeile zeigt die Art der Zeile, die Zeit UTC, DX → DE, den Weg, die Entfernung in km und auf der zweiten Zeile die Quelle, Häufigkeit und Kommentar. Der jüngste befindet sich oben. Ein Klick auf die Zeile wählt die Stelle aus: Auf der Karte wächst die Linie und die beiden Namen erscheinen.

## 8. Spot Card und Troposcatter Loss

Für den ausgewählten Punkt zeigt die Registerkarte Locatoren, Entfernung, Azimut, Frequenz, Modus, Quelle, jetzt, Kommentar, den durchschnittlichen Tropo-Index entlang des Pfades und eine Schätzung des Routenverlusts für verstreute Tropo.

Entfernung und Azimut werden auf dem maximalen Kreis mit Erdradius von 6371 km berechnet (Formel des emisenoverso für Entfernung, anfängliches Azimut von der DX-Station zum Spotter).

Verlust troposcatter. Der Grundverlust für zwei Stationen auf glattem Boden ist die Summe aus freiem Raumverlust und Verbreitungsverlust: G3YGF [2]

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

Die Verluste sind in dB, D ist der Abstand in km, f die Frequenz in MHz, θs der Diffusionswinkel in Grad.

8497 km ist der äquivalente Radius der Erde (4/3 des physischen). θA und θB hängen vom Horizont jeder Station ab:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

mit Δh Höhe des Hindernisses, das vom Horizont in Bezug auf den Bahnhof und seine Entfernung entsteht. Jede Ebene des Horizonts kostet etwa 10 dB [2]. Das Programm kennt den Horizont der Stationen nicht, so zeigt es den Grundverlust mit θA = θB = 0 und deklariert ihn ("glatter Boden"). G3YGF fügt einen klimatischen Begriff hinzu, wobei N die Reaktivität des [2] -Bodens ist; er wird verwendet, wenn N aus dem Wettermodell verfügbar ist:

```math
L_{N} = 0,2 \cdot (N - 310)
```

Verifizierung: Für 110 km bis 144 MHz ergibt die Formel 166,5 dB, gegenüber etwa 168 dB Betten auf der Karte des Artikels. Für 500 km bis 144 MHz ergibt sich etwa 206 dB. Das erwartete Signal/Rausch-Verhältnis ist die Kapazität des Systems (eirp + effektive Empfindlichkeit des Empfängers) abzüglich des Verlustes: Mit dem G3YGF 144 MHz Modellsystem (198 dB) würde ein 500 km Weg etwa -8 dB im SSB [2] ergeben, d.h. die Verbindung besteht nur bei besserer Ausbreitung der reinen Streuung.

## 9. Meine Dekodierungen: UDP von WSJT-X, JTDX und MSHV

WSJT-X, JTDX und MSHV können UDP jede Zeile senden, die sie dekodieren. Das Programm hört auf 127.0.0.1:2237 und wiederholt dasselbe Paket, Byte pro Byte, auf 127.0.0.1:2238, so dass die anderen Programme, die diesen Port verwendet haben (GridTracker, JTAlert, ein Logger), weiterhin funktionieren, wenn Sie sie auf 2238 verschieben. Adressen und Türen ändern sich in der UDP-Sublingualsprache der Datensammlung; als Abhöradresse können Sie auch eine Multicast-Gruppe (z.B. 224.0.0.1) verwenden, wenn das Decodierungsprogramm so eingestellt ist.

Pakete folgen dem WSJT-X-Netzwerkprotokoll: eine erste magische Nummer 0xADBCCBDA, Schemanummer und Nachrichtentyp. Das Programm verwendet hauptsächlich drei [8] -Typen:

- Heartbeat: sagt, welches Programm die Daten überträgt und mit welcher Version.
- Status: Frequenz von Zifferblatt und Modus; von der Frequenz erhalten Sie das Band (mit der gleichen Tabelle der Spots).
- Dekodierung: Jetzt, SNR, Zeit- und Frequenzverschwendung, Art und Text der dekodierten Nachricht.

Aus dem Text erhalten Sie die Station, die Sie gehört haben: in CQ 9 A2PQR JN83 ist die Station nach CQ (Speichern von DX, EU, TEST...), in IK0JKL IT9MNO JM68 ist die zweite, dh wer sendet. Der Locator ist der der Nachricht, wenn es (4 Zeichen gibt, die sich von RR73 unterscheiden); Andernfalls suchen Sie in CALL3. TXT. Sie hören Linien: Stationen, die Ihr Empfänger jetzt hört. Auf der Karte werden sie rote Linien von Ihrem QTH zur Station, mit dem Namen; die Schicht leuchtet mit der dekodierten Box von mir auf und erfordert einen gültigen Locator. In der Liste rechts zeigt ein Zähler an, wie viele Stationen Sie auf dem gewählten Band dekodieren.

QSO desunti dal live. Wenn eine Decodierung eine Schließnachricht in der Form C ALL1 C ALL2 RR73 (oder 73 oder RRR), beispielsweise IC8TEM IK3FHP RR73 oder IK3FKP SV8PEX 73 ist, haben die beiden Stationen eine Verbindung abgeschlossen. Das Programm zeichnet es als QSO auf, wobei der Locator von jedem aus der letzten Nachricht stammt, in der er es deklariert hat, oder, wenn nicht, aus CALL3.TXT [7] und zeichnet es als grüne Linie zwischen den beiden Stationen auf die Karte (Strate "QSO desunti dal live"). Ein Senderpaar zählt einmal pro Band; das in der Liste gewählte Band und der Bereich sind gültig.

In derselben Untersprache zeigt der Debug-Bereich den Text jedes empfangenen Pakets (Typ, Zeit, Werte und Nachricht), auf Anfrage auch hexadezimale Bytes und eine Zeile für die Weiterleitung. Es soll überprüfen, ob das Decodierungsprogramm tatsächlich die Daten sendet.

## 9bis. Sprachen Hepburn und F5LEN

Jeder zeigt, was das Programm von der Quelle nimmt. Die Schaltfläche „Seite lesen und alle Bilder herunterladen öffnet die angegebene Seite (base dxinfocentre.com/tropo_eur.html für Hepburn [3] und tropo.f5len.org/forecasts-for-europe für F5LEN [4]) und lädt alle Bilder im Cache neben der ausführbaren Datei herunter; Sie können auch die Adresse eines Bildes einfügen. Für jedes Bild gibt es eine Box zum Ausschließen und ein Feld mit der Prognosezeit. Die 31 europäischen Bilder von F5LEN werden nach der Zeit des Modellrennens benannt (03.png... 48.png alle 3 Stunden, 54... 96 alle 6 Stunden, 108... 192 alle 12 Stunden) und das Programm nimmt die Zeit vom Namen der [4] -Datei; für Hepburn ist es die Reihenfolge der Seite wert. Auf der Registerkarte F5LEN wählt ein Menü die Region: Europa (tropo.f5len.org/forecasts-for-europe), Australien (tropo.f5len.org/Australien, vk03.png... vk192.png) oder Atlantik zwischen Kap Verde und Irland (tropo.f5len.org/atlantic-ocean/atlantic-d4-ei, atl03.png... atl192.png). Jeder hat die gleichen Schritte.

Unten ist der Kalibrierzähler. Ein Klick auf das Bild fügt einen Kontrollpunkt (Locator oder lat,lon: mindestens 6) oder eine Farbe der Legende mit ihrem Wert hinzu. Das Programm berechnet die Pixelübereinstimmung → Koordinaten mit einer Polynominterpolation zweiten Grades an den kleinsten [19] -Quadraten, liest die Farben mit der gewählten Toleranz und konvertiert für F5LEN die Einheiten N auf der 0-10-Skala mit a · N + b. "Speichern Sie die Kalibrierung und versuchen Sie zu lesen" schreibt die Datei in den Tarature-Ordner und prüfen Sie, auf welches Pixel Ihr Locator fällt. Vor dem Einrichten einer Kalibrierung sollte der Autor der Karten um Erlaubnis gebeten werden.

Regionen. Jede Quelle hat mehr Regionen, jede mit ihren Fixed-Name-Bildern: Es tut mir leid, es tut mir leid. Die Registerkarte jeder Quelle zeigt zunächst die Vorschau der Fusion: alle heruntergeladenen Bilder, die wie auf der Hauptkarte zusammengestellt wurden, mit dem gepunkteten Bereich und dem Namen jeder Region, einem grauen Schleier, in dem kein Bild den Bereich bedeckt, und einem Schieberegler, um die Stunden zu scrollen; nach links die Liste der Regionen, jeweils mit dem Feld, um sie zu verwenden, die Anzahl der gelesenen Karten und woher die Extreme kommen (OCR, von Hand oder Fabrik; wenn die OCR fehlschlägt, erscheint der gelesene Text). Die manuelle Kalibrierbank bleibt hinter dem homonymen Knopf. Das Menü Bench Region wählt aus, welches Aussehen, das Feld "Diese Region auf der Karte verwenden" enthält es im Index. Der Rahmen wird allein gefunden (zwei horizontale Linien und zwei vertikale Linien derselben Farbe); um das Bild zu platzieren, dienen die Extreme des Rahmens: für Hepburn Europe (10° W–40° E, 27°–56° N) und F5LEN Europe (15° W–40° E, 30°–65°) N) sind bereits im Programm, für die anderen Bereiche schreiben sie in die vier O E S N-Felder, die die Achsenetiketten lesen. Der Skalenmultiplikator macht die beiden Quellen vergleichbar: Endwert = Multiplikator × Legendenwert. [3] F5LEN [4] F5LEN

## 9 ter. Exportieren nach Word

Die beiden Dokumente haben oben den Button "Export nach Word". Die Datei wird im Office Open XML-Format geschrieben; die Formeln werden zu Word-Gleichungen (OMML), so dass sie mit dem [16] -Gleichungseditor editierbar bleiben. Wenn das Speicherfenster nicht verfügbar ist, endet die Datei im Ordner Dokumente neben der ausführbaren Datei.

## 9 quater. Wer ruft mich

Wenn Ihnen auf der lokalen UDP eine Nachricht gesendet wird (der Vorname gehört Ihnen, z. B. IC8FAX IK3FHP JN55 oder IC8FAX IK3FHP -12), verfolgt das Programm von Ihrem QTH zu der Station, die Sie mit einer schwarz gepunkteten und blinkenden Linie aufruft, mit dem Namen und dem Locator neben Ihnen. Der Locator kommt von CALL3.TXT (einschließlich Handstimmen) oder Nachricht. Die Linie bleibt, bis die UDP die Schließung des QSO mit dieser Station ( RR73, RRR oder 73, in einer der beiden Richtungen), höchstens 3 Minuten nach dem letzten Anruf passiert; Sie ziehen über alles und folgen nicht den Ausschlusskriterien, so dass ein direkter Anruf nie entkommt.

PSKReporter, über MQTT, überträgt nicht den Text der Nachrichten: sagt nur, wer wer erhalten hat, mit Häufigkeit, Weg und SNR. Deshalb wird ein Direktaufruf nur durch eigene UDP-Dekodierungen erkannt.

## 10. Zeitsteuerung (NTP)

Digitale Modi und Spot-Zeit erfordern eine präzise Uhr. Alle 3 Minuten fragt das Programm 10 NTP-Server ab (SNTP auf UDP 123): 0.pool.ntp.org, 1.pool.ntp.org, 2.pool.ntp.org, 3.pool.ntp.org, it.pool.ntp.org, ntp1.inrim.it, ntp2.inrim.it, ptbtime1.ptb.de, time.google.com, time.cloudflare.com [11].

Für jede Antwort berechnet die Verwerfung und Verzögerung der Rückkehr, verwirft die abnormalen Werte und nimmt den Median, mit der Berechnung der Verwerfung und Verzögerung von SNTP [10] zur Verfügung gestellt. Mit mindestens 3 Antworten stimmen die UTC-Uhr des Programms ausgerichtet und als nächstes erscheint NTP: OK in grün; Wenn die Antworten nicht ausreichen, erscheint NTP: FAILED in rot und die Uhr bleibt die des Computers. Die Systemuhr wird nicht berührt, da Administratorberechtigungen erforderlich wären; Alle internen Programmpläne (Spots, Fenster von 5 bis 60 Minuten, Decodierungen) verwenden eine ausgerichtete Zeit. Die Steuerung dreht sich immer im Hintergrund: In der Benutzeroberfläche sieht man nur das Ergebnis neben der Uhr; Server, Intervalle und Mindestantworten ändern sich in LIVETROPOVHF.ini (ntp_servers, ntp_minutes, ntp_min).

## 10 bis. Allgemeine Ausschlusskriterien

Zwei Felder in der Layerleiste gelten für alles, was Sie zeichnen (Spot, PSKReporter, Decodierung, QSO desunti):

- Unter 100 km ausschließen. Der Verkehr zwischen Stationen von weniger als 100 km ist möglicherweise für den Tropo (Bodenwelle, optische Reichweite) nicht signifikant: Mit dem On-Box zeichnen Sie nicht und erscheinen nicht in der Liste. Der Start ist aus.
- Mehr als 1000 km: nur Plätze. Um Ihre QTH (der Locator, der gestartet werden soll) befindet sich eine Krone von 1000 km in alle Richtungen. Wenn sich nur eine der beiden Stationen einer Verbindung außerhalb dieser Krone befindet, wird die Verbindung nicht als Linie gezeichnet: An ihrer Stelle färben sie sich in Flieder, punktiert bei 45°, die Ortungsquadrate (4 Zeichen) der beteiligten Stationen. Gleiches gilt für PSKReporter-Stationen, die über 1000 km hören, die als Quadrat statt als Kreis erscheinen. Bisher sehen Sie den Verkehr, ohne die Karte der Linien zu füllen. Ohne einen gültigen Locator zählt die Länge der Verbindung. Das Quadrat Ihres QTH färbt sich nie lila, auch wenn Sie eine Station jenseits der Krone hören: Ist der angegebene Locator, verwendet jeder Name, der von der Station aus operiert. Der Kronenradius wird neben der Box gewählt: 1000, 700 oder 500 km. Die Box Corone 1000/700/500 km zieht die drei Kronen um die QTH, in lila gezeichnet, mit der am meisten markierten Auswahl und der Entfernung nach Norden geschrieben. In der Liste der Spots folgt die Entfernung von.. Der Start geht los. Das Kriterium gilt nach der Quarantäne: Lange noch erhaltene Verbindungen erscheinen nicht einmal als Quadrate.

Was sind die lila Quadrate bei 45° bedeckt? Der Verkehr weg von Ihrem QTH interessiert Sie zu verstehen, wo die Ausbreitung offen ist, aber seine Linien, Hunderte von Kilometern lang und oft zehn, decken die Karte ab und verbergen, was für Sie zählt: die Öffnungen, die Sie von Ihrer Station aus nutzen können. Aus diesem Grund zeichnet das Programm über die gewählte Krone hinaus nicht die Linien, sondern färbt die Ortungsquadrate (2° × 1°) der beteiligten Stationen. Das Ergebnis ist eine Karte von «aktiven Zonen»: wo viele nahe gelegene lila Quadrate erscheinen, gibt es eine Leitung oder eine Öffnung, auch wenn es Sie nicht direkt betrifft; Wenn sich der Bereich zu Ihnen hin erweitert, ist es das Signal, die Antenne in diese Richtung zu richten. Das 45°-Dashboard lässt Sie unter dem Tropo-Index und dem Raster sehen, vergleichen Sie also auf einen Blick die Prognose (untere Farbe) und den realen Verkehr (Illus). Ihr QTH-Quadrat färbt sich nie, weil es der Ausgangspunkt und kein entfernter Bereich ist. Nur die Quadrate, die sich alle jenseits der Krone befinden, sind gefärbt: Ein Quadrat, das von der Krone gekreuzt wird, oder innen, bleibt frei, auch wenn es eine der beiden Stationen gibt. Um die Linien zu sehen, schalten Sie einfach die Box aus oder wählen Sie eine größere Krone.

## 11. Themen und Sprachen

Themen: die 18 gemeinsamen IC8TEM -Programme, die gleichen wie YA-NEC und E-LAYER ANALYZER: Klar, Nacht, Schwarz, acht Pastelle (blau, orange, Aquamarin, grün, rosa, lila, indigo, braun), Scia meteorica (YA-MS Palette), die drei IP-TRX-CTRL (Chiaro, Notte, Nero) und Blau, Purpur, Grün. Ab 1.3.4 sieht der Chiaro aus wie YA-NEC: hellgrauer Hintergrund, eingerahmte weiße Panels, blaue Nacht-Tabs und blauer Akzent. Bei dunklen Themen wird auch die Basiskarte dunkel. Die Farbskala der Prognose bleibt in allen Themen gleich. Auf schmalen Bildschirmen (unter 1500 Pixeln, z.B. 1366x768) zeigt die Uhr oben nur die Stunde an, so dass Thema, Textcursor und Sprache sichtbar bleiben. Sprachen: das 28. Wiki von YA-MS [17] (Englisch, Reisen, Deutsch, Nederlands, Français, Español, Port<0xE6><0x9C><0xAC>ês, Dansk, Norsk, Svenska, Íslenska, Eesti, Lietuvi, Latviešu, Руский,,кра,нська, Türkaلרبيا) Mit Arabisch, Hebräisch und Urdu die gesamten Fensterspiegel von rechts nach links; die Karte bleibt als [12]. Übersetzungen betreffen die Schnittstelle; die beiden technischen Dokumente und Nachrichten der Statusleiste bleiben auf Italienisch. Die Namen der Tage kommen aus den internationalen Einstellungen von. NET für die gewählte Sprache.

Thema und Sprache wechseln von den beiden Menüs oben rechts neben der Uhr und gelten sofort, ohne das Programm neu zu starten und ohne die Datenerfassung zu unterbrechen. Die Auswahl wird in LIVETROPOVHF.ini gespeichert.

## 11 bis. Verdächtige und selbstlernende Berichte

Die Registerkarte Verdachtsberichte sammelt alles, was die Programmsteuerelemente seltsam finden, gruppiert nach Typ und Name, wobei die letzten 24 Stunden aufgeführt sind. Das Programm lernt von selbst: für jedes Paar (Typ, Name) zählt die positiven Ergebnisse (+) und entgegengesetzt (-), und nach 3 vereinbarten Ergebnissen erstellt eine automatische Regel; ein entgegengesetztes Ergebnis stellt es in Frage. Benutzerentscheidungen (gut, schlecht) gelten immer; der pulsante-button gibt die entscheidung an das programm zurück. Die Regeln sind in Data / APPRENDIMENTO. TXT, neben dem Programm; die automatischen, die ein Jahr lang nicht verstärkt wurden, sind vergessen.

| Typ | Wann | Ausfahrt + / − | Regelwirkung |
| EME ( Q65/JT65) | Q65 oder JT65 über 1000 km mit dem Mond über dem Horizont zu beiden Köpfen, oder EME in Kommentar | Jedes Mal, wenn der Mond normal ist | Discard: EME-Station, ihre langen Q65/JT65 s sind auch ohne Mond ausgeschlossen. Gut: als Tropo behandelt |
| Lange Verbindung | Über 1000 km in Quarantäne | + unverschleiert, − abgelaufen ohne Bestätigung | Gut: Die Längen der Station zeigen sich sofort. Rückwurf: niemals |
| Zuhörer | PSKReporter-Empfänger, der aktive Sender auf anderen Bändern hört | − verworfen, + rehabilitiert durch menschliche Cluster | Gut: nie verworfen. Rückwurf: immer |
| Andere Vermieter | Quadrat erhielt anders als das von CALL3. TXT | + jedes Mal, wenn Sie es wiederholen | Gut: Die Station ist umgezogen, es lohnt sich, den Platz zu erhalten. Verwerfen: vale CALL3.TXT |
| Quadratisches Zentrum | 6 Zeichen mit LL oder MM |  —  | Nur Informationen |
| Automatischer Empfänger | Termin mit -SDR, /SDR, -# oder -n |  —  | Nur Informationen |

EME. Q65 und JT65 sind die Kommunikationswege über Luna: Theoretisch fallen sie wie der Streumeteor in die Ausschlussklausel, werden aber auch in Tropo verwendet. Das Programm berechnet die Position des Mondes mit den niedrigpräzisen Formeln des Almanachs (ca. 0,3°). [20]: Wenn eine Verbindung in Q65 oder JT65 1000 km überschreitet und der Mond zu beiden Köpfen über dem Horizont liegt, ist er mit ziemlicher Sicherheit EME und wird ausgeschlossen (er zählt zu den Ausgeschlossenen). Wenn der Mond nicht üblich ist, bleibt die Verbindung in der Trope möglich und durchläuft die Quarantäne langer Verbindungen, aber das Signal bleibt sichtbar. Es gilt für Cluster, MQTT und seine UDP-Dekodierungen.

## 11 ter. Kriterien für Kontaktrückwürfe, eins nach dem anderen

Das Programm will nur die tropophärische Ausbreitung in 144 MHz und darüber hinaus zeigen. Alle eingehenden Daten (Cluster Row, PSKReporter Report via MQTT, UDP-Decodierung) durchlaufen eine Reihe von Filtern, immer in der gleichen Reihenfolge. Eine verworfene Daten verschwindet nicht in nichts: wird gezählt (Kontakt des Ausgeschlossenen in der oberen Leiste) und, wenn der Grund zweifelhaft ist, erscheint in der Registerkarte Suspended Reports.

|  N.  | Kriterien | Wo ist er? | Was verworfen wurde | Können Sie sich ändern? |
|  1  | Band | Cluster, MQTT, UDP | Alle unter 143 MHz (auch 50 und 70 MHz), außer GRAVES 143,050 MHz. Die Frequenzbänder sind 144, 222, 432, 1296, 2320 MHz und 10 GHz | Nein. |
|  2  | Satelliten QO-100 | Cluster, MQTT, UDP | 2400,0–2400,5 MHz und 10489,5–10490 MHz: es ist geostationärer Satellitenverkehr, nicht Tropo | Nein. |
|  3  | Meteorstreuung | Cluster, MQTT, UDP | JT6M und ISCAT (auch ISCAT-A/B), Kommentare mit "&" und Nicht-Tropoxy-Ausbreitungsetiketten (<AU>, <AUE>, <ES>, <FAI>, < F2 >) immer. MSK144, FSK441 und JTMS (auch mit <MS>) nur über 250 km: Innerhalb von 250 km akzeptieren wir, denn in dieser Entfernung werden diese Modi auch in tropo verwendet | Nein. |
|  4  | EME (via Luna) | Cluster, MQTT, UDP | EME im Kommentar geschrieben; Q65 oder JT65 über 1000 km, wenn der Mond über dem Horizont zu beiden Köpfen ist; Stationen als EME gelernt | Ja: Gute Regel in verdächtigen Signalen |
|  5  | Nicht echt Zuhörer | MQ | PSKReporter-Empfänger, der sagt, auf 144 Stationen zu hören, die zu diesem Zeitpunkt auf anderen Bändern aktiv sind (mindestens zwei): Seine Beziehungen werden verworfen. Zurück gut, wenn die DX-menschlichen Cluster die gleichen Stationen bestätigen | Ja: Gut / Arm |
|  6  | Quarantäne langer Verbindungen | alle Linien | Über 1000 km bleibt eine Verbindung verborgen, bis sie eine Bestätigung findet: dieselbe Station in der gleichen Richtung, eine andere Route in einem Korridor von 200 km oder eine parallele Route innerhalb von 10° und 200 km entlang mindestens der Hälfte. Ohne Bestätigung läuft nach 60 Minuten ab | Ja: Gut / Aus zur Station |
|  7  | Unter 100 km | Zeilen und Liste | Mit der Accessbox die kürzesten Verbindungen von 100 km (Bodenwelle, optische Reichweite) | Box, off beim Start |
|  8  | Jenseits der Krone von der QTH | Linien | Wenn eine Station über 1000, 700 oder 500 km (nach Ihrer Wahl) von Ihrem QTH entfernt ist, keine Linie: nur die Lilla-Quadrate, die von den beiden Stationen abgedeckt werden. Ihr QTH-Quadrat färbt sich nie | Kasten und Radius, auf 1000 km |
|  9  | Zeitfenster | Alles | Ältere Stellen des gewählten Bereichs (von 3 bis 60 Minuten) | Menü oben |
|  10  | doppelt | Cluster | Die gleiche Stelle (gleicher DX, gleicher Spotter, gleiches Band, gleiche Quelle) wiederholt | Nein. |

Anmerkung zu den Kriterien 4 und 6. Eine Verbindung in Q65 oder JT65 über 1000 km ohne Common Moon ist in tropo möglich: wird nicht durch Kriterium 4 verworfen, sondern geht wie jede andere lange Verbindung für die Quarantäne von Kriterium 6 über. Direktanrufe an Sie (Abschnitt 9 quater) sind die einzige Ausnahme von den Kriterien 7 und 8: Sie ziehen immer.

Was ist kein Abfall. Die Korrekturen des Locators (CALL3). TXT mit Handstimmen, Korrekturen aus dem Cluster und gelernte Locatoren, Suffixe -SDR, Mitte des Quadrats LL / MM ändern, wo Sie eine Station zeichnen, nicht verwerfen. Eine Station ohne Vermieter hingegen kann nicht gezogen werden: bleibt in der Liste mit der Entfernung «—».

## 11 quater. Farben und Symbole

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

## 12. Einstellungen und Portabilität

Fenster. Zu Beginn nimmt das Fenster 90% des nützlichen Platzes des Bildschirms ein (schneiden Sie die Anwendungsleiste), zentriert, so dass auf einem 15-16-Zoll-Notebook mit einer 125% oder 150% Größenänderung die Titelleiste sichtbar bleibt. Wenn der Nutzraum unter 1280 × 720 Punkten liegt, wird der Fensterteil maximiert. Größe, Position und maximaler Status werden in LIVETROPOVHF gespeichert. ini beim Schließen und Ende beim nächsten Start, sofern die Titelleiste noch in einen Bildschirm fällt (z.B. nach Abnehmen eines externen Monitors); andernfalls kehren wir zu 90% zurück. Die Mindestgröße beträgt 900 × 560.

Das Programm ist portabel: schreibt niemals im AppData-Ordner oder in der Registry. Alles befindet sich im Ordner der ausführbaren Datei: LIVETROPOVHF.ini (Sets, erstellt beim ersten Schließen), CALL3.TXT (mit handschriftlichen Stimmen, gelernten Korrekturen und Locatoren), Karten-Cache- und Wetterdaten, gelernte Regeln (Dati/APPRENDIMENTO.TXT) und LIVETROPOVHF.db-Archiv. Sie können es auch auf einem USB-Stick aufbewahren, solange der Ordner beschreibbar ist.

Anzeigenarchiv (SQLite [14]). Jeder akzeptierte Spot und jeder PSKReporter-Hörbericht wird in LIVETROPOVHF.db, einer SQLite-Datenbank, in einer Datei gespeichert. Im Gedächtnis bleiben nur die letzten 60 Minuten für die Liste und Karte; das Archiv behält alles und dient dem Historiker und der Statistik (auch den UDP-Dekodierungen), zum Beispiel die Öffnungen für Band und pro Tag, die maximalen Entfernungen, die erreichten Quadrate und der rückwärtige Vergleich zwischen dem erwarteten Index und den realen Spots. Die vom Filter verworfenen Streifen werden gezählt, aber nicht archiviert.

## 12 bis. Automatische Installation und Updates

Ab Version 1.0 lädt das Programm bereits bereit von der Release-Seite von github.com/ic8tem/LIVETROPOVHF herunter, in den gleichen Paketen von YA-MS: Installer für Windows (64 Bit, 32 Bit und ARM, install in C:\LIVETROPOVHF ohne Administratorrechte) und portable zip; für Windows 7 SP1 und 8.1 Pakete auseinander (-windows7-x64 und -window.deb,.rpm, Arch, Slackware, AppImage und tar.gz für Linux; Pakete für Raspberry Pi (64 und 32 Bit) und Alpine;.dmg und apps.zip für Mac (Apple Silicon und Intel). Bei Linux-Paketen wird das Programm auf den ersten Start in ~/LIVETROPOVHF kopiert, so dass Einstellungen und Daten neben dem Programm bleiben; auf dem Mac werden Daten neben LIVETROPOVHF.app geboren.

Alle 30 Minuten (die erste Kontrolle wenige Sekunden nach dem Start, sofort, wenn das letzte Mal das Programm wegen eines Fehlers geschlossen wurde) liest das Programm allein, ohne etwas zu verlangen, das letzte Release. Wenn die Version neuer ist, laden Sie das für das System geeignete tragbare Paket herunter und ersetzen Sie die Programmdateien vor Ort: Die verwendeten werden umbenannt *.old-update und beim nächsten Neustart gelöscht. Einstellungen, Archiv, Datenordner und CALL3. TXT werden nicht berührt. Die neue Version ist vom nächsten Start an aktiv: Bei abgeschlossener Installation schlägt ein Fenster vor, sofort neu zu starten; bei dringenden Korrekturen, die im Release markiert sind, kann der Neustart nicht verschoben werden. Der Kontrollstatus und die verwendete Version werden am Ende der Statusleiste geschrieben; um updates=0 in LIVETROPOVHF.ini auszuschalten. Auf dem Mac ist die App signiert und ändert sich nicht von selbst: Das Programm meldet nur, dass es eine neue Version gibt.

Das LIVETROPOVHF-Bild mit der Versionsnummer erscheint am Anfang; am Ende bittet das Programm um Bestätigung und Dank für die Verwendung.

## 12 ter. Fehlerprotokoll und Statusleiste

Jeder unvorhergesehene Fehler endet in Data/errori.log, auch wenn die Berechnung abloggt: Datum und Uhrzeit, Version, System, geöffneter Tab, vollständiger Fehler und letzte Zeilen von dem, was das Programm getan hat. Ein Interface-Fehler schließt das Programm nicht mehr: In der Statusleiste erscheint "anno annotated error" (klicken: Registry öffnen). Beim nächsten Start fragt das Programm, ob der Bericht gesendet werden soll: öffnet den Browser auf einem bereits kompilierten GitHub-Signal, wobei Nominierung, Locator und Benutzername des PCs entfernt werden, die Sie überprüfen und mit einem kostenlosen GitHub-Konto senden. Das Protokoll endet auch mit jedem Update-Check.

In der Statusleiste werden die Quellbälle (DX Cluster, RBN, PSKReporter, MQTT, UDP) angeklickt und zur Quelle gebracht; der Comic sagt, warum eine Quelle ausgeschaltet ist: abgelehnter Port, auf der Serverseite stationierter Clusterknoten, keine Antwort oder UDP Listening aber ohne Pakete von WSJT-X, JTDX oder MSHV. Ein Klick auf die Versionsnummer prüft sofort Updates.

## 13. Bahnsteige

Externe Buchhandlungen: Avalonia für die [12] -Schnittstelle, MQTTnet als MQTT-Client für den Echtzeit-Stream von PSKReporter [13], Microsoft. Data.Sqlite für das [14] Archiv.

C# auf.NET 10 (LTS) [15] mit Avalonia-Schnittstelle, veröffentlicht als unabhängige ausführbare Datei für Windows 64 und 32 Bit, Mac Intel und Apple Silicon, Linux Debian und Raspberry Pi (64 und 32 Bit). Noted Limits: Windows 10/11; macOS 14 oder höher, mit Apps, die rechts signiert oder geöffnet werden → Öffnen; Raspberry von Pi 2 / Zero 2 W nach oben (ARMv6-Modelle werden von.NET nicht unterstützt).

## Vielen Dank
Dank I5MZY Massimo Zannoni für das Testen unter Windows 7 und Windows 8.

Luftdichter: ITU-R P.453, digitale Produkte der Studiengruppe 3 (© ITU), die für persönliche und nicht-kommerzielle Zwecke verwendet werden.

## Hauptquellen

1. S. Heck LA0BY, „Extreme Tropo Propagation on 144 MHz and up, Vortrag auf dem 41. Nordic VUSHF Meeting, Skjeberg 2019. Troposferical Ways, Event Control List (Hepburn/ F5LEN -Prognose, Hochtemperaturprofile mit Windy, DX-Cluster als Öffnungssicher), Beobachtung, dass nur Vorhersagekarten für extreme Routen nicht ausreichen.
2. J. N. Gannaway G3YGF, "Tropospheric Scatter Propagation", QST November 1983 (aus dem gleichnamigen Artikel über Funkkommunikation, RSGB, August 1981). Formeln von freiem Raumverlust und Diffusion, äquivalenter Radius 8497 km, Ortsbegriff von 10 dB pro Horizont, Klimabegriff 0,2 · (N - 310), Anlagenkapazität und Signal/Rausch-Verhältnis, Beispiel 144 MHz.
3. W. R. Hepburn, DX Info Centre, „Tropospheric Ducting Forecast“ (dxinfocentre.com/tropo.html) und „The Hepburn Tropo Index (HTI)“. Indexskala und Wertbedeutung, Vorhersage an 6 Tagen.
4. F5LEN, Prognose für Europa (tropo.f5len.org/forecasts-for-European: Liste der Bilder im Seitenskript, Klassenlegende Disturb...) Duct im Bild) und Weltvorhersage (tropo.f5len.org/WW) und Hinweis "Neue Version des weltweiten VHF troposphärischen Ausbreitungsprognose-Tools". Index basierend auf Reaktivität in Einheit N, 40 Schritte ab 3 Stunden, Aktualisierung 4 mal am Tag.
5. mqtt.pskreporter.info, Verteilung MQTT in Echtzeit der Werbespots von pskreporter.info (kuratiert von M0LTE, Fluss von P. Gladstone mit Genehmigung verwendet). Türen 1883-1886, pskr/filter/v2 Thema Schema.
6. Projektzusammenfassung vom 8/11/2026 und Go-Test (core.go, gui.go), bereitgestellt vom Autor des Programms. Anzeigenregeln, negativer Filter, Bandbreitensegmente, Telnet Cluster Management, Themen, Sprachen und Portabilität.
7. CALL3.TXT, Nominierungsdatenbank → JTDX Vermieter (I. UA3DJY, A. ES1JA Skript, JTAlert/hamspots.net Daten). Dateiformat und Anzahl der in der Projektdatei verifizierten Namen.
8. WSJT-X, Quellcode, NetworkMessage.hpp-Datei (Beschreibung des UDP-Protokolls: Heartbeat, Status, Decode). Allgemeine Referenz, nicht beigefügt; JTDX und MSHV verwenden das gleiche Format.
9. Reverse Beacon Network (reversebeacon.net), Serviceseiten des Telnet-Knotens. Ports 7000 (CW, RTTY) und 7001 ( FT8), Login nur mit dem Namen.
10. D. Mills, RFC 4330 "Simple Network Time Protocol (SNTP) Version 4", IETF 2006. Paketformat, Abfallberechnung und Rückgabeverzögerung. Allgemeine Bezugnahme, nicht beigefügt.
11. NTP Pool Project (ntppool.org); INRIM, NTP time service (ntp1/ntp2.inrim.it); PTB (ptbtime1.ptb.de). Server zur Zeitkontrolle. Allgemeine Bezugnahme, nicht beigefügt.
12. Avalonia UI, offizielle Dokumentation (docs.avaloniaui.net). Multiplattform-Schnittstelle, Themes und Layout von rechts nach links. Allgemeine Bezugnahme, nicht beigefügt.
13. MQTTnet,.NET Bibliothek für MQTT (github.com/dotnet/MQTTnet). Client, der für den PSKReporter-Flow verwendet wird. Allgemeine Bezugnahme, nicht beigefügt.
14. SQLite (sqlite.org) und Microsoft. Data.Sqlite (learn.microsoft.com). Archivieren in einer Datei. Allgemeine Bezugnahme, nicht beigefügt.
15. Microsoft,.NET 10 – unterstützte Betriebssysteme und autonome Veröffentlichung in einer Datei (learn.microsoft.com/dotnet). Plattformen und Grenzen gemäß Abschnitt 13. Allgemeine Bezugnahme, nicht beigefügt.
16. ECMA-376 „Office Open XML File Formats, Teil 1, Abschnitt 22.1 (Office Math Markup Language). Struktur der.docx Datei und der Gleichungen. Allgemeine Bezugnahme, nicht beigefügt.
17. Liste der 28 Sprachen des YA-MS-Wikis, bereitgestellt vom Autor des Programms.
18. Open-Meteo, Wettervorhersage API (open-meteo.com), mit Daten aus dem NOAA GFS-Modell. Isobarer Druck auf Meereshöhe (siehe Dokument 2).
19. J. P. Snyder, "Map Projections - A Working Manual", USGS Professional Paper 1395, 1987, und Standard-Polynomtransformationsverfahren zur Georeferenzierung von Bildern. Allgemeine Bezugnahme, nicht beigefügt.
20. Der astronomische Almanach, Abschnitt D, "Niedrige Präzisionsformeln für den Mond" (Präzisionsposition von etwa 0,3°). Wird verwendet, um EME-Verbindungen zu erkennen. Allgemeine Bezugnahme, nicht beigefügt.
