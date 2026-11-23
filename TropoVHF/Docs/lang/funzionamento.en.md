LIVETROPOVHF · technical document 1
# Operation of the program

> Machine translation from Italian: if in doubt, the Italian guide is the reference.

What the program does, from where it takes the data and how it turns them into what you see on the map and in the list. Version of 9 November 2026, related to the project in C# (.NET 10 + Avalonia).

## In summary

- Purpose: tropo forecast (Hepburn [3], F5LEN [4], isobare GFS [18]) and real viewing of bands in the same window, according to the LA0BY [1] control list.
- Data collection: two DX cluster, Reverse Beacon Network [9], dm4x cluster and PSKReporter [5] MQTT flow, UDP decoding from WSJT-X, JTDX and MSHV [8].
- Spot: band segments, negative filter, airscatter in dashed yellow, position by locators or CALL3.TXT [6] [7].
- Who is listening: dark pink circles with pink lines always visible towards the stations felt [5].
- Spot card: distance, azimut, tropo index on the path and troposcatter loss with the G3YGF [2] method.
- Languages Hepburn and F5LEN: [3] image and calibration bench [4].
- Watch: NTP control on 10 servers every 3 minutes [10] [11].
- Suspected reports: EME in Q65/JT65 recognized with the position of the Moon, truncated locators, skimmers, quarantine and listeners, with rules learned by sun [20].
- Criteria for the discard and meaning of the colours summarized in two tables (sections 11 ter and 11 quater).
- Themes, 36 [17] languages, export documents to Word with editable formulas [16], portable program and multiplatform [12] [13] [14] [15].

## 1. Purpose

The program puts together in a single window two things that usually look on different sites: the prediction of the tropofer propagation (Tropo index of Hepburn and F5LEN, isobare) and the observation of what really happens on bands (spot from DX cluster, Reverse Beacon Network and PSKReporter, plus the list of stations that are listening). The reference band is 144 MHz; the commercials collect from 50 MHz up.

The setting follows the control list proposed by LA0BY for extreme tropo events: in the previous days you look at the Hepburn and F5LEN forecasts and the temperature profiles at altitude, while the event is underway you search on the DX cluster and on the spot maps the evidence that the opening is starting [1]. LA0BY also observes that the only Hepburn/ F5LEN maps do not suffice to predict the longest connections [1]

## 2. The window and the tabs

The window is one and does not split into mobile windows. From top to bottom:

- General settings row: Call and Locator on the left; on the right the UTC clock (e.g. 08/11/2026 20:33:38 UTC, amber text) followed by NTP:OK or NTP:FAILED, the theme menu and that of the language.
- Number of tabs in five groups: Map and spot; Forecasts (Propria β, the forecast calculated by the program and described in the Forecast Manual, then Hepburn and F5LEN); GRAVES; Data and sources (DX cluster, PSKReporter, Reverse Beacon, UDP, Appointments and suspicious Reports); Guides (Program Operation, Map Building, How to shape the tropo, Manual forecast, Tips to the operator, with a search box that counts in which guide the text appears). Under the beam a bar shows the sub-languages of the group; each group remembers the last used.
- Content of the tab and, at the bottom, the status bar with the state of the sources, the database of names and the time of the last update.

The top line settings apply to all tabs: the chosen theme also color the data collection and background of documents; Document pages remain as white as a sheet, so they print well.

## 3. Call and landlord

The Maidenhead landlord can have 4 or 6 characters (e.g. JN70 or JN70CN). The program converts it to the coordinates of the center of the square: field of 20° × 10°, square of 2° × 1st, subquadrate of 5′ × 2,5′. If the text is not a valid locator the edge of the box becomes red and the station does not appear on the map. The locator also serves as a center of the azimuth projection and as a starting point for distances and azimuth in the ad board.

The Call is the name used as a cluster login. At the first start, until there is a valid nomination, the clusters do not connect: a window asks for the name (and, optionally, the landlord); confirmed, the name is copied as a username in the Login field of each cluster that has it empty and clusters leave. With “Later” remain still, and the Connect button of each cluster requires the nomination again. MQTT, UDP and NTP do not use login and leave anyway.

## 4. Data collection: sources

Each source has its own section in the Data Collection tab, with Connected/Disconnected, the Recognized and Connected boxes at startup, status, meters and register of the received rows. Unbanded or discarded lines from the filter do not appear in the register: only count the section meters. The sections are side by side with two to two.

| Source | Connection | What brings |
| 1. DX cluster choice | on0dxk.dyndns.org:8000 command set/filter dxbm/pass 6 | Spot of amateur radios, also SSB and CW. On ON0DXK the VHF are filtered and are enabled with the indicated command (poi pass 4, 2...). |
| 2. Second source | dxmaps.com:7300 (DXMAPS, EA6VQ) and s50clx.si:41112 ( S50CLX, Slovenia) | DXMAPS collects VHF spots and also gives RBN skimmer spots (set/skimmer control, already set); S50CLX is a DX Spider cluster. Login with your name; editable addresses. |
| 3. Reverse Beacon Network | telnet.reversebeacon.net 7000 CW/RTTY · 7001 FT8 | Automatic skimmer spot. Login with the name only [9]. |
| 4. Cluster with PSKReporter | dm4x.ddns.net:8500 | Digital Spots of PSKReporter in cluster format. |
| 5. PSKReporter MQTT | mqtt.pskreporter.info 1883 · 1884 with TLS | Every real-time reception report: who transmits, who listens and their [5] Locators. Bands 144, 222 and 432 MHz. |
| 6. Weather and tropo maps | dxinfocentre.com/tropo_eur.html tropo.f5len.org/forecasts-for-European model GFS | Hepburn [3] and F5LEN [4] tropo index maps, sea level pressure for isobare (GFS via Open-Meteo [18]). Hepburn and F5LEN must be allowed to the authors. |

Cluster telnet (sources 1–4). After the connection the program waits for the name request (text that contains login, call:, callsign or your call); if it does not arrive within 4 seconds, please send the login. Then send the commands of the section, separated from point and comma, one every 0.6 seconds. The telnet control characters are removed before reading the lines. If the connection falls and Riconnetti is active, try again after 10 seconds (15 if the server does not respond at all) [6].

PSKReporter MQTT (source 5). The program enrolls in the topic pskr/filter/v2/{banda}/{modo}/{tx_call}/{rx_call}/{tx_grid}/{rx_grid}/{tx_dxcc}/{rx_dxcc} by fixing only the band and leaving the other fields free with the character + [5]. The exact name with which the server indicates the bands (for example 2m, 1.25m, 70cm) must be checked at the first connection.

## 5. From the cluster line to the spot

One line is a spot when it has the form DX de SPOTTER: frequency DX comment hhmmZ. The program derives spotter, DX station, frequency in kHz, comment and now UTC; the names take off the skimmer suffixes (-#, -1...). Then apply, in this order, the following rules [6].

| Band | Segment accepted |
| GRAVES (with 144) | 143,040 – 143,060 MHz |
| 144 MHz | 144 – 148 MHz |
| 222 MHz | 220 – 225 MHz (only Region 2 ITU, Americas) |
| 432 MHz | 420 – 450 MHz |
| 1296 MHz | 1240 – 1300 MHz |
| 2320 MHz | 2300 – 2450 MHz |
| 10 GHz | 10,5 GHz |

1. Band. Frequency must fall into one of the managed segments; all the rest, and in particular every commercial under 49 990,0 kHz, is not shown and counts as “out of band”.
2. Way. One of the best known ways of commenting is FT8, FT4, Q65, JT65, ISCAT, CW, SSB, FM, RTTY, PSK31, MSK144, FSK441, JT6M); USB and LSB become SSB.
3. Negative filter. The spot is discarded if it contains the modes MSK144, FSK441, JT6M, JTMS, ISCAT or the tags <AUE> <ES> <MS> <FAI> < F2 >, with non-sensitive uppercases and even spaces within brackets. These are ways and propagations that are not tropo: Scatter meteor, aurora, and sporadic, F2.
4. Airscatter. If the comment contains <AS> or <ACS> (also mixed, e.g. <As>) or declares the spot as airscatter (airscatter, air scatter, aircraft scatter), the spot remains but is drawn in yellow with dotted line (dark wavy to stay readable on the clear map) and highlighted in the list.
5. Location of stations. If in the comment there are two locators (e.g. JN54 < JN83) the first is of those who make the spot and the second of the station DX; with one locator, that's from DX station. What is missing is searched in CALL3.TXT, only with the exact name (for laptops see below). If a station remains without a position, the spot appears in the list with the note “unknown position” but not on the map.
6. Doubles. A commercial with the same DX, same spotter, same band and same source of one already present replaces it, instead of adding.

CALL3.TXT is the name database → locator used by JTDX, in the format CALL,LOCATORE,,,. The project file contains 1 048 847 names, all with valid locator; the header of the file still speaks of 34 323 names because it remained that of the first edition [7]. It is loaded in the background at startup, so the window is immediately usable. The file is searched next to the program, in the Data folder, in the folder from which the program was started and one or two folders above; If there is no, a window tells it when you start and the status bar signals it in red. Its name always uses its 6-character. Only rule for the locators: if the one received (spot, MQTT, decoding, QSO desunti) has less than 6 characters and CALL3.TXT has one to 6, it is always worth the CALL3.TXT; for laptops (F/ IC8TEM, IC8TEM/P) is only an exact voice: without, the station remains without a landlord and is not designed elsewhere. The rule also applies at the time of drawing, so the data arrived before the database was loaded will run itself.

One database: CALL3.TXT. From 1.3.4 all locators are in one file, CALL3.TXT, which remains in the format of JTDX and WSJT-X (CALL,LOCATORE,,,,) and can be used as it is also in WSJT-X, JTDX and MSHV. There are no more files apart. At the top of the file, after the header, there are two blocks marked by a comment line: // [LIVETROPOVHF-MANUALI] with the entries inserted by hand and // [LIVETROPOVHF-CLUSTER] with the corrections from the cluster; then // [LIVETROPOVHF-FONTI] with all other items in alphabetical order. Each nomination appears once. In the sublingual Data and Sources › Appointments (CALL3) you look for a name, you write the landlord and you press Add / change: the voice goes in the block of the voices by hand and it is worth above all, even above the locator that the station itself sends. ∫ Removes the nomination from CALL3.TXT (the next update resumes it from sources, if they have it). Add a file... read a list CALL, LOCATOR (you also accept the point and comma, tabulation and space) and write it among hand-picked voices; the chosen file is not copied. Reload reread CALL3.TXT from the disk, after editing it with an editor. Full order of sources: own locator, hand voices, cluster corrections, the remainder of CALL3.TXT (sources, learnt live locators and G7VRD), received locator (which applies over the database only if it has 6 characters and does not end with LL or MM).

Update of CALL3.TXT. In the same sub-language the Update button now downloads the sources (CALL3.TXT of WSJT-X Improved, CALL3.TXT EME of W7GJ, grid_data.bin of JTDX), joins them (the first that has a nomination win), check that they are at least 50,000 names (otherwise nothing changes), saves the CALL3.TXT The previous CALL3.TXT entries remain, and a 6-character locator remains where a source has only 4 of the same square. Hand voices and corrections from the cluster remain at the top, unchanged. With the Update box on its own every day, the check takes place at the start. The program rewrites CALL3.TXT immediately after each voice by hand and, for learnt locators, at most every 10 minutes and at the closing; the CALL3.TXT of the package is read-only and the attribute takes off itself. Linux installers do not override a CALL3.TXT already present; that for Windows installs it only if it is missing. ALL3

Automatic corrections from the cluster. In the DM4X and RBN automatic spots the comment brings two locators, for example KN09tp<> KN09: the first is what the station that spots declares itself, to 6 characters; the second is that of the station felt, often only to 4. If the first has 6 characters (and does not end with LL or MM) and is different from that of the database, the program corrects CALL3.TXT alone: the correction goes in the block // [LIVETROPOVHF-CLUSTER], with the date of the last confirmation in the third field, it is valid above the sources (under the hand voices) and appears between the suspicious signals updated as "CALL3 ". So it survives CALL3.TXT updates. An unconfirmed correction for two years loses precedence and remains as a normal voice.

Last source: G7VRD. If a nominee does not have a 6-character in CALL3.TXT, the program asks it in the background to the public API of G7VRD (api.g7vrd.co.uk, "locator by callsign"), which collects locators automatically from network traffic. You only accept a 6-character that is not the center of the square (LL or MM); the requests are grouped, at maximum 40 nominatives per minute, and each nominative asks once a day, because it is a hobbistic service without guarantees. The locator found enters CALL3.TXT if the name is not there or has only 4 characters of the same square; the search turns off with the box in the name sublingual.

Appointments with the bar. In the PSKReporter MQTT topics, the bar separates the levels, then the compound names come with a point instead of the bar ( IV3. HB9CAT). The program puts the bar back and, if the message contains the full names, use those: IV3/HB9CAT remains IV3/HB9CAT on map, list and database.

Locators learned live. CALL3.TXT does not contain all: for example SP9EMF and SP9NVY are not there, and then remains the 4-character locator sent by PSKReporter (the card says so). To reduce these cases the program writes in CALL3.TXT every 6-character that comes from any source (PSKReporter, such as transmitter or as receiver, cluster, UDP decoding) for a nomination that CALL3.TXT does not or has only 4 characters in the same square; never changes a voice to 6 characters. Until 1.3.3 these locators were in Data/ LOC6.TXT.

Transition from 1.3.3. At the first start of 1.3.4 the files apart from the previous versions (Dati/CALL3 -MANUALE.TXT, the Data/CALL3 -AGGIUNTE, Data/CALL3 -CLUSTER.TXT, Data/ LOC6.TXT and Data/CALL3 - G7VRD.TXT) are merged into CALL3.TXT with the same entries. Releasing writing, old files take the.fuso extension, you no longer read and you can delete.

Automatic receivers. RBN, dm4x and PSKReporter often report names with a suffix indicating the receiver: -SDR, /SDR, -# (skimmer RBN) or -1, -2... (plus receivers of the same station). For the landlord the suffix is removed: OK2OZL -SDR takes OK2OZL JN89UE from CALL3.TXT if the station does not already send a 6-character locator. The name shown remains the one with the suffix.

Locators cut. Not all sources treat the extension of their landlord: many set it to 4 characters, and some automatic carry-over systems deliberately truncate the 4-character locator, even those that obtain the data from the real listening of FT8, FT4, Q65 and JT65, or complete it with the center of the square (LL or MM, e.g. JN89MM). For this reason, a 4-character, or 6-character that ends with LL or MM, is replaced by the CALL3.TXT of the same square. If the correct location and the center of the square do not coincide with a few dozen km, the error remains small compared to the tropo distances.

## 6. Who is listening

Each MQTT message from PSKReporter reports the name and locator of those receiving [5], so you don't need to look for it in CALL3.TXT. For the band chosen in the selector the program holds, for each receiving station: landlord, last reception, decoded ways, number of stations felt and maximum distance. A station leaves the list when it no longer receives anything in the chosen range (3, 5, 10, 15, 30 or 60 minutes).

On the map each receiver is a dark pink empty circle, and from each start full pink lines towards the stations that is feeling and sends back to the net. The lines are always seen, without clicking. Appointments of the listening stations, those heard and the two heads of each spot are always written on the map, without having to click (the same name at the same point is written once only). By clicking a circle its lines become even thicker and opens the tab with details. If the locator from MQTT has less than 6 characters, the program uses that of CALL3.TXT (about one million names), provided it is consistent with the square received.

Scatter meteor. As with cluster spots, MQTT ratios in MSK144, FSK441, JT6M, JTMS or ISCAT are discarded and counted only; the same goes for UDP decodings in these ways.

Not genuine listeners. Some stations declare to receive in 144 MHz but in reality the receiver is on another band (wrong frequency indication): their reports would design unexisting openings of thousands of km. The program checks it by itself. Each station that a listener says to hear in VHF is followed for 30 minutes on all bands, with a dedicated MQTT inscription (pskr/filter/v2/+/{nominative}/#) [5]. serve as proof. If at least two of the stations heard by a listener are, in the same minutes (±10), active on other bands, the listener is considered not genuine and all its VHF reports are discarded; the MQTT register reports the name and stations that betrayed him. In the list of spots, next to the meters, the MQTT button shows the state of the flow (green leaf if connected, red if not) and how many listeners were discarded in the band and range chosen; by clicking it opens the tab with the list of discards and, for each, stations found on other bands.

Long connection china. A link over 1000 km (Clumbing Spot, MQTT Listening, QSO Desunti) is not immediately shown: remains held, and the counter “held > 1000 km” says the number. It is revealed when another connection, between different stations and at least half along, confirms it with one of these criteria: same station at a head (for example the same listener) and the other station in the same direction within 10°; similar corridor, with both heads within 200 km from those of the other connection; parallel route, with direction within 10° and side distance within 200 km, overlapped along the route. Both show up and the MQTT log shows the reason. Its UDP decodings are always visible and apply as confirmation for others. The retained connections remain in memory for 60 minutes.

Nothing under 144 MHz. The program works from 144 MHz up: everything under 143 MHz is discarded. The exception is GRAVES, the French radar transmitting in CW on 143,050 MHz by Broye-lès-Pesmes ( JN27SI): a commercial between 143,040 and 143,060 MHz, or with GRAVES in the name, appears between 144 MHz with radar as a transmitter. The ad board says if the receiver is inside the beam (sectors D, C, B, A from 90° to 270° of azimut seen from the radar, 6 steps from 7,5° each) or out; the tab GRAVES, as in YA-MS, shows the sky seen from the radar: sectors, step illuminated at that time (sequenza PE1ITR 2021: 0,8 s per step, cycle 4,8 s Data and geometry come from YA-MS. Feeling it in the trope is difficult, but it can happen.

Rehabilitation from human clusters. A station that also appears on other bands does not count as suspect if, in the same minutes (±30), the DX human clusters ( ON0DXK, GB7RAU; not dm4x, RBN or other automatic sources) the active damage on the band in question, in spots made by other names: it is the case of stations that really work on multiple bands together. Thus a discarded listener returns visible (the MQTT register writes “rehabilitated”) and a long-held link for low bands can be unveiled.

Active stations under 144 MHz. In the quarantine of long connections, a station that in the same minutes is heard on bands under 144 MHz (from the MQTT reports of other bands) does not confirm other connections and is not unveiled, although a similar or parallel route would confirm it.

## 7. List of commercials

It occupies 35% right of the Map tab and spot. Top: title, band selector (50, 70, 144, 222, 432, 1296, 2320 MHz, 10 GHz), range selector (last 3, 5, 10, 15, 30, 60 minutes) and Update key. Below, four meters: visible spots, AS/ACS, discarded from the filter, stations listening.

Each line shows the type of line, the time UTC, DX → DE, the way, the distance in km and, on the second line, the source, frequency and comment. The most recent is on the top. One click on the line selects the spot: on the map the line grows and the two names appear.

## 8. Spot card and troposcatter loss

For the selected spot the tab shows locators, distance, azimut, frequency, mode, source, now, comment, the average tropo index along the path and an estimate of the route loss for scattered tropo.

Distance and azimut are calculated on the maximum circle with Earth radius of 6371 km (formula of the emisenoverso for distance, initial azimut from the DX station to the spotter).

Loss troposcatter. The basic loss, for two stations on smooth ground, is the sum of free space loss and dissemination loss: G3YGF [2]

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

The losses are in dB, D is the distance in km, f the frequency in MHz, θs the diffusion angle in degrees.

8497 km is the Earth's equivalent radius (4/3 of the physical one). θA and θB depend on the horizon of each station:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

with Δh height of the obstacle that makes from horizon regarding the station and its distance. Each level of horizon costs about 10 dB [2]. The program does not know the horizon of the stations, so it shows the basic loss with θA = θB = 0 and declares it ("smooth ground"). G3YGF adds a climatic term, where N is the reactivity to the [2] soil; it will be used when N is available from the weather model:

```math
L_{N} = 0,2 \cdot (N - 310)
```

Verification: for 110 km to 144 MHz the formula gives 166,5 dB, against about 168 dB beds on the chart of the article. For 500 km to 144 MHz gives about 206 dB. The expected signal/noise ratio is the capacity of the system (eirp + effective sensitivity of the receiver) minus the loss: with the G3YGF 144 MHz model system (198 dB) a 500 km path would give about −8 dB in SSB [2], that is the connection is only with better propagation of the pure scatter.

## 9. My decodings: UDP from WSJT-X, JTDX and MSHV

WSJT-X, JTDX and MSHV can send UDP every line they decode. The program listens on 127.0.0.1:2237 and repeats the same package, byte per byte, on 127.0.0.1:2238, so the other programs that used that port (GridTracker, JTAlert, a logger) continue to work if you move them on 2238. Addresses and doors change in the UDP sublingual of the Data Collection; as a listening address you can also use a multicast group (e.g. 224.0.0.1), if the decoding program is set like this.

Packages follow the WSJT-X network protocol: an initial magic number 0xADBCCBDA, schema number and message type. The programme mainly uses three [8] types:

- Heartbeat: says which program is transmitting the data and with what version.
- Status: frequency of dial and mode; from the frequency you get the band (with the same table of the spots).
- Decode: Now, SNR, time and frequency waste, manner and text of the decoded message.

From the text you get the station you heard: in CQ 9 A2PQR JN83 is the station after CQ (saving DX, EU, TEST...), in IK0JKL IT9MNO JM68 is the second, i.e. who transmits. The locator is that of the message, if there is (4 characters, different from RR73); otherwise you search in CALL3.TXT. They are listening lines: stations that your receiver hears now. On the map they become red lines from your QTH to the station, with the name; the layer lights up with the Decoded box from me and requires a valid locator. In the list on the right a counter tells how many stations you are decoding on the chosen band.

QSO desunti dal live. When a decoding is a closing message in the form C ALL1 C ALL2 RR73 (or 73 or RRR), for example IC8TEM IK3FHP RR73 or IK3FKP SV8PEX 73, the two stations have completed a link. The program records it as QSO, with the locator of each taken from the last message in which he declared it or, if not, from CALL3.TXT [7], and draws it on the map as a green line between the two stations (strate “QSO desunti dal live”). A pair of stations counts once per band; the band and range chosen in the list are valid.

In the same sub-language, the Debug pane shows the text of each received package (type, time, values and message), on request also hexadecimal bytes, and a line for forwarding. It's to check that the decoding program is actually sending the data.

## 9 bis. Languages Hepburn and F5LEN

Each shows what the program takes from the source. The button “Read the page and download all images” opens the indicated page (based on dxinfocentre.com/tropo_eur.html for Hepburn [3] and tropo.f5len.org/forecasts-for-europe for F5LEN [4]) and downloads all the cache images next to the executable; you can also paste the address of one image. For each image there is a box to exclude it and a field with the forecasting time. The 31 European images of F5LEN are named by the time of the model race (03.png... 48.png every 3 hours, 54... 96 every 6 hours, 108... 192 every 12 hours) and the program takes the time from the name of the [4] file; for Hepburn it is worth the order of the page. In the F5LEN tab, a menu chooses the region: Europe (tropo.f5len.org/forecasts-for-europe), Australia (tropo.f5len.org/Australia, vk03.png... vk192.png) or Atlantic between Cape Verde and Ireland (tropo.f5len.org/atlantic-ocean/atlantic-d4-ei, atl03.png... atl192.png). Everyone has the same steps.

Below is the calibration counter. One click on the image adds a control point (locator or lat,lon: you need at least 6) or a legend color with its value. The program calculates the pixel matching → coordinates with a second degree polynomial interpolation at the smallest [19] squares, reads the colors with the tolerance chosen and, for F5LEN, converts the units N in the 0–10 scale with a · N + b. “Save calibration and try reading” writes the file in the Tarature folder and check on which pixel your locator falls. Before setting up a calibration, permission should be requested to the author of the maps.

Regions. Each source has more regions, each with its fixed-name images: I'm sorry, I'm sorry. The tab of each source shows first of all the preview of the fusion: all downloaded images put together as on the main map, with the dotted pane and the name of each region, a grey veil where no image covers the area and a slider to scroll the hours; to the left the list of regions, each with the box to use it, the number of maps read and from where the extremes come (OCR, by hand or factory; if the OCR fails the text he read appears). The manual calibration bench remains behind the homonymous button. The Bench Region menu chooses which look, the box “Use this region on the map” includes it in the index. The frame is found alone (two horizontal lines and two vertical lines of the same color); to place the image serve the extremes lat/lon of the frame: for Hepburn Europe (10° W–40° E, 27°–56° N) and F5LEN Europe (15° W–40° E, 30°–65° N) are already in the program, for the other regions they write in the four O E S N fields reading the axis labels. The scale multiplier makes the two sources comparable: final value = multiplier × legend value. [3] F5LEN [4] F5LEN

## 9 ter. Export in Word

The two documents have on the top of the “Export to Word” button. The file is written in the Office Open XML format; the formulas become Word equations (OMML), so they remain editable with the [16] equation editor. If the save window is not available the file ends in the Documents folder next to the executable.

## 9 quater. Who calls me

If a message is sent to you on the local UDP (the first name is yours, for example IC8FAX IK3FHP JN55 or IC8FAX IK3FHP -12), the program tracks from your QTH to the station that calls you a black dotted and flashing line, with the name and the locator next to you. The locator comes from CALL3.TXT (including hand voices) or message. The line remains until the UDP passes the closure of the QSO with that station ( RR73, RRR or 73, in one of the two directions), at most 3 minutes from the last call; you draw over everything and do not follow exclusion criteria, so a direct call never escapes.

PSKReporter, via MQTT, does not transmit the text of the messages: says only who received who, with frequency, way and SNR. This is why a direct call is only recognized by its own UDP decodings.

## 10. Time control (NTP)

Digital modes and spot time require a precise clock. Every 3 minutes the program interrogates 10 NTP servers (SNTP on UDP 123): 0.pool.ntp.org, 1.pool.ntp.org, 2.pool.ntp.org, 3.pool.ntp.org, it.pool.ntp.org, ntp1.inrim.it, ntp2.inrim.it, ptbtime1.ptb.de, time.google.com, time.cloudflare.com [11].

For each answer calculates the discard and delay of return, discards the abnormal values and takes the median, with the calculation of discard and delay provided by SNTP [10]. With at least 3 replies agree the UTC clock of the program aligns and next appears NTP:OK in green; if the answers are not enough appear NTP:FAILED in red and the clock remains that of the computer. The system clock is not touched, because administrator permissions would be needed; all internal program schedules (spots, 5–60 minutes windows, decodings) use aligned time. The control always turns in the background: in the interface you see only the outcome next to the clock; servers, intervals and minimum responses change in LIVETROPOVHF.ini (ntp_servers, ntp_minutes, ntp_min).

## 10 bis. General exclusion criteria

Two boxes in the layer bar apply to everything you draw (spot, PSKReporter, decoding, QSO desunti):

- Exclude under 100 km. Traffic between stations less than 100 km may not be significant for the tropo (ground wave, optical range): with the on-box you do not draw and do not appear in the listing list. The start is off.
- More than 1000 km: only squares. Around your QTH (the locator set to start) there is a crown of 1000 km in all directions. If only one of the two stations of a link is outside this crown, the connection is not drawn as a line: in its place they color in lilac, dotted at 45°, the locator squares (4 characters) of the stations involved. The same applies to PSKReporter stations listening to over 1000 km, which appear as a square instead of as a circle. So far traffic you see without filling the map of lines. Without a valid locator counts the length of the connection. The square of your QTH never colores in lilac, even when you listen to a station beyond the crown: is the specified locator, any name uses who operates from the station. The crown radius is chosen next to the box: 1000, 700 or 500 km. The box Corone 1000/700/500 km draws the three crowns around the QTH, drawn in lilac, with the most marked choice and the distance written north. In the list of spots the distance is followed by.. The start is on. The criterion applies after quarantine: long connections still retained do not even appear as squares.

What are the lilac squares dotted at 45°. The traffic away from your QTH interests you to understand where the propagation is open, but its lines, hundreds of kilometers long and often tens, cover the map and hide what counts for you: the openings you can exploit from your station. For this reason, beyond the chosen crown, the program does not draw the lines but color the locator squares (2° × 1°) of the stations involved. The result is a map of «active zones»: where many nearby lilac squares appear, there is a conduit or an opening, even if it does not concern you directly; If the area widens towards you, it is the signal to point the antenna in that direction. The 45° dashboard lets you see under the tropo index and the grid, so compare at a glance the forecast (bottom color) and the real traffic (illus). Your QTH square never colores, because it is the starting point and not a distant area. Only the squares that are all beyond the crown are coloured: a square crossed by the crown, or inside, remains free even if there is one of the two stations. To see the lines, just turn off the box or choose a larger crown.

## 11. Themes and languages

Themes: the 18 common to IC8TEM programs, the same as YA-NEC and E-LAYER ANALYZER: Clear, Night, Black, eight pastels (blue, orange, aquamarine, green, pink, lilac, indigo, brown), Scia meteorica (YA-MS palette), the three of IP-TRX-CTRL (Chiaro, Notte, Nero) and Blue, Purple, Green. From 1.3.4 the Chiaro looks like YA-NEC: light grey background, bordered white panels, blue night tabs and blue accent. In dark themes also the base map becomes dark. The color scale of the forecast remains the same in all themes. On narrow screens (under 1500 pixels, for example 1366×768) the clock at the top shows only the hour, so theme, cursor of texts and language remain visible. Languages: the 28th wiki of YA-MS [17] (English, Travel, Deutsch, Nederlands, Français, Español, Português, Dansk, Norsk, Svenska, Íslenska, Eesti, Lietuvi, Latviešu, Руский,,кра,нська, Türkaلרبيا With Arabic, Hebrew and Urdu the whole window mirrors from right to left; the map remains as [12]. Translations concern the interface; the two technical documents and messages of the status bar remain in Italian. The names of the days come from the international settings of.NET for the chosen language.

Theme and language change from the two menus on the top right, next to the clock, and apply immediately, without rebooting the program and without interrupting the collection of data. The choice is saved in LIVETROPOVHF.ini.

## 11 bis. Suspected and self-learning reports

The Suspected Reports tab collects everything the program controls find strange, grouped by type and name, with the last 24 hours listed. The program learns by itself: for each couple (type, name) counts the positive results (+) and contrary (−), and after 3 agreed outcomes creates an automatic rule; an opposite outcome puts it in question. User decisions (Good, Poor) always apply; the pulsante button returns the decision to the program. The rules are in Data/APPRENDIMENTO.TXT, next to the program; the automatic ones not reinforced for a year are forgotten.

| Type | When | Exit + / − | Rule Effect |
| EME ( Q65/JT65) | Q65 or JT65 over 1000 km with the Moon above the horizon to both heads, or EME in comment | − every time the Moon is common | Discard: EME station, its long Q65/JT65 s are also excluded without common moon. Good: treated as a tropo |
| Long connection | Over 1000 km, in quarantine | + unveiled, − expired without confirmation | Good: the lengths of the station show immediately. Discard: never |
| Listener | PSKReporter receiver who hears active stations on other bands | − discarded, + rehabilitated by human clusters | Good: never discarded. Discard: always |
| Different landlord | Square received different from that of CALL3.TXT | + every time you repeat it | Good: the station moved, it is worth the square received. Discard: vale CALL3.TXT |
| Square Center | 6-character with LL or MM |  —  | Information only |
| Automatic receiver | Appointment with -SDR, /SDR, -# or -n |  —  | Information only |

EME. Q65 and JT65 are the ways of communication via Luna: In theory they fall into the exclusion clause like the scatter meteor, but they are also used in tropo. The program calculates the position of the Moon with the low-precision formulas of the Almanac Astronomical (about 0.3°) [20]: if a connection in Q65 or JT65 exceeds 1000 km and the Moon is above the horizon to both heads, it is almost certainly EME and is excluded (it counts among the excluded). If the Moon is not common, the connection remains possible in the trope and passes through the quarantine of long connections, but the signal remains visible. It applies to clusters, MQTT and its UDP decodings.

## 11 ter. Contact discard criteria, one by one

The program wants to show only the tropopheric propagation in 144 MHz and beyond. Each incoming data (Cluster Row, PSKReporter Report via MQTT, UDP decoding) passes through a series of filters, always in the same order. A discarded data does not disappear in nothing: is counted (contact of the excluded in the upper bar) and, when the reason is doubtful, appears in the tab Suspended Reports.

|  N.  | Criteria | Where is it? | What Discarded | Can you change? |
|  1  | Band | cluster, MQTT, UDP | All under 143 MHz (also 50 and 70 MHz), except GRAVES 143,050 MHz. The bands held are 144, 222, 432, 1296, 2320 MHz and 10 GHz | No. |
|  2  | Satellite QO-100 | cluster, MQTT, UDP | 2400,0–2400,5 MHz and 10489,5–10490 MHz: it is geostationary satellite traffic, not tropo | No. |
|  3  | Meteor scatter | cluster, MQTT, UDP | JT6M and ISCAT (also ISCAT-A/B), comments with "&" and non-tropoxy propagation labels (<AU>, <AUE>, <ES>, <FAI>, < F2 >) always. MSK144, FSK441 and JTMS (also with <MS>) only over 250 km: within 250 km we accept, because at that distance these modes are also used in tropo | No. |
|  4  | EME (via Luna) | cluster, MQTT, UDP | EME written in the comment; Q65 or JT65 over 1000 km when the Moon is above the horizon to both heads; stations learned as EME | Yes: Good rule in suspicious signals |
|  5  | Non- Genuine Listener | MQ | PSKReporter receiver who says to hear on 144 stations active at that time on other bands (at least two): His relationships are discarded. Back good if the DX human clusters confirm the same stations | Yes: Good / Poor |
|  6  | Quarantine of long connections | all lines | Over 1000 km a link remains hidden until it finds a confirmation: same station in the same direction, another route in a corridor of 200 km, or parallel route within 10° and 200 km along at least half. Without confirmation expires after 60 minutes | Yes: Good / Off to station |
|  7  | Under 100 km | lines and list | With the on-box, the shortest connections of 100 km (ground wave, optical range) | Box, off at startup |
|  8  | Beyond the crown from the QTH | lines | If a station is over 1000, 700 or 500 km (on your choice) from your QTH, no line: only the lilla squares covered by the two stations. Your QTH square never color | Box and radius, on 1000 km |
|  9  | Time window | everything | Older spots of the chosen range (from 3 to 60 minutes) | Menu at the top |
|  10  | Double | cluster | The same spot (same DX, same spotter, same band, same source) repeated | No. |

Note on criteria 4 and 6. A connection in Q65 or JT65 over 1000 km without Common Moon is possible in tropo: is not discarded by criterion 4, but passes for the quarantine of criterion 6 as any other long link. Direct calls to you (section 9 quater) are the only exception to criteria 7 and 8: they always draw.

What is not a waste. The corrections of the locator (CALL3.TXT with hand voices, corrections from the cluster and learned locators, suffixes -SDR, center of the square LL/MM) change where you draw a station, not discard it. A station without any landlord, on the other hand, cannot be drawn: remains in the list with the distance «—».

## 11 quater. Colours and symbols

Each color has only one meaning, equal in all themes (change only the background).

| Color and sign | Meaning |
| Continuous blue line | Spot from DX cluster, RBN or DM4X between two stations |
| Yellow line with dark edge | Spot with airscatter comment (reflection on aircraft) |
| Dark pink empty circle | Listening station on PSKReporter (MQTT) |
| Thin pink lines from the circle, with a pink ball at the bottom | Stations that the listener heard, with the name always written; thicker lines for the selected listener |
| Red line and red name | Your UDP decodings (WSJT-X, JTDX, MSHV): who are you |
| Red line flashing strokes | QSO in progress with that station: He called you and the closing has not yet arrived, or is the DX Call of WSJT-X/JTDX with the enabled transmission. Go on with RR73, RRR or 73 |
| Green line | QSO desunti dal live: closing RR73, RRR or 73 UDP view |
| Black line with white border | Someone is calling you: it lasts up to RR73/RRR /73 or up to 3 minutes |
| Squares of 45° | Traffic with at least one station beyond the chosen crown: No lines, only squares locator |
| Search for lillas with distance to the north | 1000, 700 and 500 km around your QTH; that choice is more marked |
| Circle and point in the color of the theme | Your QTH, with name and landlord |
| Orange ball with GRAVES 143,050 | The radar GRAVES ( JN27SI) |
| Sample from blue to red | Hepburn trope index (0–10) or F5LEN class, or their average; warmer color, better conditions |
| Light grey velvet | Area without tropo data for the chosen time |
| Grey-blue lines with the value in hPa | Isoberate every 4 hPa; H in blue high pressure, L in red low pressure |
| Red, blue and purple dotted lines | Isothermia to 850 hPa every 4 °C: red above zero, blue under zero, purple the zero |
| Sample orange | Thermal reversal: air to 925 hPa warmer than on the ground, more intense with stronger inversion |

In the Suspected Reports the edge of each tab says the rule in force: green Good, red Scarta, grey no rule.

## 12. Settings and portability

Window. At the start, the window occupies 90% of the screen's useful space (cut the application bar), centered, so on a 15-16 inch notebook with a 125% or 150% resize the title bar remains visible. If the useful space is under 1280 × 720 points, the window part maximized. Size, position and maximised status are saved in LIVETROPOVHF.ini at the closing and end at the next start, provided the title bar still falls into a screen (for example after detaching an external monitor); Otherwise you return to 90%. The minimum size is 900 × 560.

The program is portable: never writes in the AppData folder or in the registry. Everything is in the folder of the executable: LIVETROPOVHF.ini (Settings, created at the first closing), CALL3.TXT (with handwritten voices, learned corrections and locators), map cache and weather data, rules learned (Dati/APPRENDIMENTO.TXT) and LIVETROPOVHF.db archive. You can also keep on a USB stick, as long as the folder is writeable.

Ad archive (SQLite [14]). Every spot accepted and every PSKReporter listening report is saved in LIVETROPOVHF.db, a SQLite database in one file. In memory only the last 60 minutes remain for the list and map; the archive retains everything and serves for the historian and statistics (also the UDP decodings), for example the openings for band and per day, the maximum distances, the squares reached, and the rear comparison between the expected index and the real spots. The strips discarded from the filter are counted but do not archive.

## 12 bis. Automatic installation and updates

From version 1.0 the program already downloads ready from the Release page of github.com/ic8tem/LIVETROPOVHF, in the same packages of YA-MS: installer for Windows (64 bit, 32 bit and ARM, install in C:\LIVETROPOVHF without administrator rights) and portable zip; for Windows 7 SP1 and 8.1 packages apart (-windows7-x64 and -window.deb,.rpm, Arch, Slackware, AppImage and tar.gz for Linux; packages for Raspberry Pi (64 and 32 bits) and Alpine;.dmg and apps.zip for Mac (Apple Silicon and Intel). With Linux packages, the program is copied to the first start in ~/LIVETROPOVHF, so settings and data remain next to the program; on the Mac data is born next to LIVETROPOVHF.app.

Every 30 minutes (the first check a few seconds after the start, immediately if the last time the program had closed for a mistake) the program reads alone, without asking for anything, the last Release. If the version is newer download the portable package suitable for the system and replaces the program files on the spot: those in use are renamed *.old-update and deleted at the next restart. Settings, archive, Data folder and CALL3.TXT are not touched. The new version is active from the next start: at installation finished a window proposes to restart immediately; for urgent corrections, marked [obligatory] in Release, reboot cannot be postponed. The control status and the version in use are written at the bottom of the status bar; to turn off updates=0 in LIVETROPOVHF.ini. On the Mac the app is signed and does not change itself: the program only reports that there is a new version.

The LIVETROPOVHF image with the version number appears at the start; at the end the program asks for confirmation and thanks for use.

## 12 ter. Error log and status bar

Any unforeseen error ends in Data/errori.log, even with the calculation log off: date and time, version, system, open tab, complete error and last lines of what the program was doing. An interface error no longer closes the program: in the status bar appears " anno annotated error" (click: open the registry). At the next start the program asks if to send the report: opens the browser on a GitHub signal already compiled, with nomination, locator and username of the PC removed, which you check and send with a free GitHub account. The log also ends with every update check.

In the status bar the source balls (DX cluster, RBN, PSKReporter, MQTT, UDP) are clicked and brought to the source; the comic says why a source is off: rejected port, cluster node stationed on the server side, no response, or UDP listening but without packages from WSJT-X, JTDX or MSHV. One click on the version number immediately check updates.

## 13. Platforms

External bookcases: Avalonia for the [12] interface, MQTTnet as a MQTT client for the real-time stream of PSKReporter [13], Microsoft.Data.Sqlite for the [14] archive.

C# on.NET 10 (LTS) [15] with Avalonia interface, published as an independent executable for Windows 64 and 32 bits, Mac Intel and Apple Silicon, Linux Debian and Raspberry Pi (64 and 32 bits). Noted Limits: Windows 10/11; macOS 14 or later, with apps to sign or to open right → Open; Raspberry from Pi 2 / Zero 2 W up (ARMv6 models are not supported by.NET).

## Thank you
Thanks to I5MZY Massimo Zannoni for testing on Windows 7 and Windows 8.

Airtights: ITU-R P.453, digital products of Study Group 3 (© ITU), used for personal and non-commercial purposes.

## Main sources

1. S. Heck LA0BY, “Extreme Tropo Propagation on 144 MHz and up”, presentation at the 41st Nordic VUSHF Meeting, Skjeberg 2019. Troposferical Ways, Event Control List (Hepburn/ F5LEN forecast, high temperature profiles with Windy, DX cluster as opening test), observation that only forecast maps are not enough for extreme routes.
2. J. N. Gannaway G3YGF, “Tropospheric Scatter Propagation”, QST November 1983 (from the eponymous article on Radio Communication, RSGB, August 1981). Formules of free space loss and diffusion, equivalent radius 8497 km, site term of 10 dB per horizon, climate term 0.2 · (N − 310), plant capacity and signal/noise ratio, example at 144 MHz.
3. W. R. Hepburn, DX Info Centre, “Tropospheric Ducting Forecast” (dxinfocentre.com/tropo.html) and “The Hepburn Tropo Index (HTI)”. Index scale and value meaning, prediction on 6 days.
4. F5LEN, forecast for Europe (tropo.f5len.org/forecasts-for-European: list of images in the page script, class legend Disturb... Duct in the image) and world forecast (tropo.f5len.org/WW) and note “New version of the worldwide VHF tropospheric propagation forecast tool”. Index based on reactivity in unit N, 40 steps from 3 hours, update 4 times a day.
5. mqtt.pskreporter.info, distribution MQTT in real time of the commercials of pskreporter.info (curated by M0LTE, flow of P. Gladstone used with permission). Doors 1883–1886, pskr/filter/v2 topic scheme.
6. Project summary of 8/11/2026 and Go test (core.go, gui.go) provided by the author of the program. Ad Rules, negative filter, bandwidth segments, telnet cluster management, themes, languages and portability.
7. CALL3.TXT, nomination database → JTDX landlord (I. UA3DJY, A. ES1JA script, JTAlert/hamspots.net data). File format and count of the names verified on the project file.
8. WSJT-X, source code, NetworkMessage.hpp file (description of UDP protocol: Heartbeat, Status, Decode). General reference, not annexed; JTDX and MSHV use the same format.
9. Reverse Beacon Network (reversebeacon.net), service pages of the telnet node. Ports 7000 (CW, RTTY) and 7001 ( FT8), login with the name only.
10. D. Mills, RFC 4330 “Simple Network Time Protocol (SNTP) Version 4”, IETF 2006. Package format, waste calculation and return delay. General reference, not annexed.
11. NTP Pool Project (ntppool.org); INRIM, NTP time service (ntp1/ntp2.inrim.it); PTB (ptbtime1.ptb.de). Servers used for time control. General reference, not annexed.
12. Avalonia UI, official documentation (docs.avaloniaui.net). Multiplatform interface, themes and layout from right to left. General reference, not annexed.
13. MQTTnet,.NET library for MQTT (github.com/dotnet/MQTTnet). Client used for the PSKReporter flow. General reference, not annexed.
14. SQLite (sqlite.org) and Microsoft.Data.Sqlite (learn.microsoft.com). Archive in one file. General reference, not annexed.
15. Microsoft,.NET 10 – supported operating systems and autonomous publication in one file (learn.microsoft.com/dotnet). Platforms and limits indicated in section 13. General reference, not annexed.
16. ECMA-376 “Office Open XML File Formats”, part 1, section 22.1 (Office Math Markup Language). Structure of the.docx file and equations. General reference, not annexed.
17. List of the 28 languages of the YA-MS wiki, provided by the author of the program.
18. Open-Meteo, weather forecast API (open-meteo.com), with data from the NOAA GFS model. Pressure at sea level for isobare (see document 2).
19. J. P. Snyder, “Map Projections – A Working Manual”, USGS Professional Paper 1395, 1987, and standard polynomial transformation methods for georeferencing images. General reference, not annexed.
20. The Astronomical Almanac, section D, “Low precision formulae for the Moon” (precision position of about 0.3°). Used to recognize EME connections. General reference, not annexed.
