LIVETROPOVHF · technical document 2
# Construction of the map

> Machine translation from Italian: if in doubt, the Italian guide is the reference.

As the map of the tab Map and spot is born: the geographical base, the projections, the average of the Hepburn and F5LEN indices, the isobare, the spots and the stations listening, overlapped with layers. Version of 9 November 2026.

## In summary

- The map is designed by the program in eight layers, each with its [8] box.
- Base: Natural Earth coastline and borders at two levels of [9] detail; [10] Market and azimuth projections.
- Physics: inversions, reactivity N and gradient dN/dh, lead threshold at −157 N/km [3] [4] [7] [11].
- Throat index: calibrated reading of Hepburn [1] and F5LEN [2] maps, common grid and average of the two sources.
- Isobare: GFS [12] pressure via Open-Meteo [15], lines every 4 hPa, H and L centers.
- Spot, listening stations (dark pink circles with always visible pink lines) and personal decodings in red.
- Reading the overlap: high pressure, high ducts, pressure drop, growing SNR, [3] [4] [5] [13] [14] mountain routes.

## 1. The principle: a layered map

The map is not a fixed image: is designed by the program, layer over layer, whenever you change zoom, projection, band, forecasting time or theme. Each layer has its box in the top Layer bar on the map, so you can only hold what you need and see everything together when you want the full picture. The order, from below up, is this:

|  #  | Road | Content | Casella |
|  1  | Basic map | Sea, lands, coasts and borders | Yes |
|  2  | Index tropo | Media Hepburn + F5LEN, or only one source; adjustable transparency | Yes |
|  3  | Grill locator | Fields, squares and, with zoom, squared | Yes |
|  4  | Isobare | Pressure lines every 4 hPa, centres H and L | Yes |
|  4a  | Temperature 850 hPa | Isothermal treated every 4 °C | Yes |
|  4b  | Thermal inversion | Orange sample where the air at altitude is warmer than on the ground | Yes |
|  5  | PSKReporter | Receiving stations of PSKReporter (dark pink circles) and, in pink, what they hear | Yes |
|  6  | Decoded by me | Decoding stations with WSJT-X, JTDX or MSHV: Red lines from your QTH | Yes |
|  6a  | QSO desunti dal live | Closed connections ( RR73, 73, RRR) read in your decodings: green line between the two stations | Yes |
|  7  | Spotlight | Connections: full blue line, yellow covered for airscatter, two balls | Yes |
|  7a  | Exclusions | Casella Excluded under 100 km: the shortest traffic is neither drawn nor listed. Casella More than 1000 km: only squares (starting): if one of the stations is over 1000 km from your QTH, no lines or circles, only the locator squares of the stations involved in lilac treated at 45° | Yes |
|  8  | QTH and labels | Your station, the names of the selected spot | always visible |

The tropo index is immediately above the base and below all else: is a colored sampling, and higher placed would cover lines and symbols. The grid passes over it, so you always know where a colored area falls. The isobares are thin lines and are well read over the sampling.

## 2. The basic map

Costs, borders and lands come from natural Earth vector data, of public domain [9], to scale 1:50 million for overall view and 1:10 million when magnified. The files are in the program folder: no online map services. Colors of the theme Clear: light blue sea # E2ECF8, dark blue coast, light grey borders. In the themes Black and Night the colors of the base are darkened, in the other themes they dye slightly.

## 3. The two projections

All layers pass through the same projection function, so they are always aligned with each other. You choose between two [10] projections (λ longitude, φ latitude, R ray of the Earth):

Market. Store local corners and shapes; The distances stretch towards the poles. Cut to ±85° latitude.

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

Azimutale equidtanti centered on your square. The center (φ0, λ0) is the center of your WWL square. Distances and directions measured from the center are true: a station at 1200 km is 1200 km in scale, and the angle with the north is the azimuth to which to point the antenna. When you change the locator the map refocuss.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

In the azimutale the lines that on the paper are straight (meridians, parallels, square edges) become curved. For this reason all lines are subdivided into short traits, each section is projected and then the traits are united; the same applies to the edges of the tropo index and to the isobare. The point to the antipodes of the center does not have a definite position and remains out of sight.

## 4. Grill locator, zoom and move

The Maidenhead grid is designed by the program: fields of 20° × 10° with the two large and faded letters in the center, squares of 2° × 1st, and with sufficient zoom subquadrati of 5′ × 2,5′. The lines are blue and thin, not to cover the tropo index.

Zoom with the wheel centered on the pointer (the point below the mouse remains steady), moving dragging, double click to return to the whole view, + and − keys, and the key with the percentage for the whole view. Details fit the zoom: finer coasts, sub-squares, thicker isobare labels.

## 5. Why inversions bend the waves

In the low atmosphere the temperature normally drops by about 6.5 °C per km; The refractive index depends on both temperature and [3] moisture, and where it changes sharply with the altitude the wave bends to the ground and arrives further than normal. The most important layer is that up to about 3 km, where inversions, ducts and convective cells [4] are formed; sea paths are favored because the water masses form large areas of [4] inversion. [3]

The size used to measure it is refractive N, i.e. (n − 1) · 106 [11]:

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

T is the temperature in kelvin, P the pressure and and steam pressure, both in hPa.

Counts as varied N with share (dN/dh). In the standard atmosphere is approximately −39 N/km; up to −79 N/km the refraction is considered normal, besides super-refraction, and with positive gradients of sub-refraction [7]. Below −157 N/km the wave is bent more than the curvature of the earth and remains trapped: it is the [11] (ducting) duct. These are the conditions that the Hepburn and F5LEN maps try to predict.

## 6. The tropo index: from two maps to the average

Hepburn and F5LEN tabs are the control desk: show the images downloaded from the page of each source, with the changeable forecasting time, and serve to calibrate the reading by clicking on the reference points and colors of the legend. The tropo index layer remains empty until at least one of the two calibrations is not active and complete.

Hepburn. The Hepburn index measures how much waves are expected to be folded over an area, on a linear scale from 0 to 10 which can also exceed 10. 0 means standard atmosphere and only scatter; 1–2 marginal or weak super-refraction; 3–4 super-refraction with duct areas; The index does not take into account the scatter tropo and does not see the [1] mountains. The maps cover 6 days: 3-hour passes for the first 36 hours, then 6 hours, updated every day to 18 UTC [1]; the starting data (temperature, humidity, pressure) come from the global model of the Canadian weather service [1]. [1]

F5LEN. For Europe F5LEN publishes 31 maps from +3 to +192 hours (every 3 hours up to +48, every 6 up to +96, then every 12), with the date and time valid in the title, the production time at the bottom right and a class legend: Disturb, Normal, Minor, Weak, Good, Strong, Vy Strong, Duct [2]. The world prediction of F5LEN is calculated by the air refractive index in Unit N, on 40 steps of 3 hours (about 5 days), updated 4 times a day; a click on the map shows the average value and the locator of that point [2].

Proceeding. For each prediction step the program performs these steps:

1. Discharge. The images of the two forecasts are downloaded in the cache next to the executable, once to update the source, not to any redesign. Before the final version, permission should be requested to Hepburn and F5LEN.
2. Automatic georeferencing. The program finds the frame (two horizontal and two vertical lines of the same color) and, on Windows 10/11, reads with the OCR integrated into the system the axle labels (54N, 10W, 0, 5E...). Cut the strips around the frame, enlarge them, test both signs when the letter N/S/E/W does not read and keep the line on which the most labels fall: the wrong readings remain out. From here you can get the extreme lat/lon of the frame and the type of projection (regular grill or Mercator). The extremes save themselves and reread themselves if the frame changes size. On Mac and Linux, without OCR, factory extremities or handwritten ones are valid in the tab. Alternatively, for maps that have no legible axes: Each type of map (Hepburn Europa, F5LEN worldwide...) has a calibration: some control points with known coordinates (crosses of meridians and parallels, recognizable ribs) connect pixels to latitude and longitude, in the projection used by that source. The calibration is in a file next to the program and you can remake if a source changes format.
3. Color reading. Each pixel is compared to the source legend and converted into the nearest numerical value. Costs, writings and grids drawn on the original map are recognized as “no data” and filled with the nearby values.
4. World grid. Each image, once the frame is found and known its extremes, is placed on Earth; the value at any point in the world is derived from the images that cover it. If more regions cover the same point it is worth detailing (more pixels per degree), provided its time is within 1.5 h from that choice; then the closest hour, then the most recent race. Where no image covers the point the map shows a grey veil: no data, which is different from “index 0”. Inside the frame, a pixel without a legend color is 0 (normal atmosphere); coasts, borders and writings are jumped by looking at the nearest color within 3 pixels.
5. Same ladder. Hepburn is already on the scale 0–10 [1]: dark background = 0 and ten colors in order (violet, blue, water green, lime, yellow, ocher, orange, red, pink, light pink) for 1...10, lilac beyond. F5LEN uses classes (Disturb, Normal, Minor, Weak, Good, Strong, Vy Strong, Duct) [2], but some have exactly Hepburn colors: Weak = 1, Strong = 5, Vy Strong = 7, Duct = 8; Minor is 0.5 and Good (green) 3.5. The scale multiplier, in the F5LEN tab, corrects the set to make it comparable with Hepburn.
6. Same time. The timebar goes from +0 to +192 h at steps of 3 hours. For each step you take from each source the forecast valid at the nearest time; If a source does not have that step, the media only uses the other and the legend indicates it.
7. Media. With the Media button each point is worth (Hepburn + F5LEN) / 2; with Hepburn or F5LEN you see only one source, useful to understand where the two forecasts disagree.
8. Drawing. The value is coloured with the legend scale (0 blue, 2 green, 4 yellow, 6 orange, 8 red, 10 purple) and projected as a sampling under the grid. The 0 is transparent, so where nothing happens you see the clean basic map; general transparency adjusts with the cursor next to the box.

On the spot tab, the index along the route is the average of the grid values championships every 10 km on the maximum circle between the two stations.

## 7. Isobare

The high stable pressures are the first signal to be sought for the [3] trope, and the connections beyond the IK3XTV analyzed Alps have also been with high [4] pressure.

1. Data. Pressure at sea level from the global model GFS of NOAA [12], read through the free Open-Meteo [15] service on a grid of 3° × 3° (from 30° to 72° N, from 30° W to 48° E), for the next 78 hours. The program updates the data on startup and then every 6 hours, like the model racing.
2. Lines. The isobare is obtained from the grid every 4 hPa (...1012, 1016, 1020...) with the algorithm of the marching squares (marching squares), then they are slightly rounded and projected drawn by tract.
3. Centres. The most marked local maximums and minimums are marked with H (blue) and L (red) and the value in hPa.
4. Labels. The value appears along each isobara, on a color background of the map to remain readable above the sampling.

Area and step. The data is downloaded on the area you are looking at, expanded by 25%, with a step from 1st to 20° chosen to remain under 400 points (Open-Meteo counts every point as a request: 600 per minute, 5000 per hour): by enlarging or moving the map out of the area already downloaded the program the most dense reload, at most every 10 minutes; if an update fails the previous data remain and it will try again after 3 minutes. This way isobare and temperatures work anywhere in the world.

Temperatures. The same request brings the temperature to 2 m, to 925 hPa and to 850 hPa. LA0BY shows that the event of 22/11/2018 to UA6 was seen in temperature maps (900–3000 m), but not in Hepburn [3] predictions; DF5AI and DK5YA use vertical temperature profiles to recognize [5] inversions. For this there are two layers:

- Temperature 850 hPa: isoterme treat every 4 °C (red above zero, blue below, purple on zero).
- Thermal inversion: orange sampling where air at 925 or 850 hPa is warmer than that at 2 m; the more intense, the more inversion is strong. It is the physical condition that bends the waves to the ground [3] [11], and compare it with the tropo index and isobare shows if the forecasts have a meteorological base.

## 8. Spots and stations

Spot. Each spot with both known positions is a line on the maximum circle between the two stations, divided into sections and projected; In Mercatore it appears then slightly curved, in the azimutale it is straight if it starts from the center. Blue line full for the normal spot, yellow covered with dark edge for the airscatter (<AS>, <ACS> or “airscatter” in the comment), a ball for each station, light halo under the line to detach it from the sampling. The selected spot is more often designed for last and with the two names.

Listening. Each PSKReporter [16] receiving station is a dark pink empty circle. From each start full pink lines towards the stations that has decoded and sent back to the network, always visible without clicking, with the names always written. By selecting a circle its lines are thicker. Spot and listening only show the band and range chosen in the list: changing bandwidth the map is empty and redesigned with the data already collected for the new band.

Decided by me. Each station that decodes with WSJT-X, JTDX or MSHV (receipt via UDP) is a red line full from your QTH to the station, with ball and nomination. It is the most direct comparison with the forecast: shows where your receiver really comes right now.

## 8 bis. Colours and symbols

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

What are the lilac squares dotted at 45°. The traffic away from your QTH interests you to understand where the propagation is open, but its lines, hundreds of kilometers long and often tens, cover the map and hide what counts for you: the openings you can exploit from your station. For this reason, beyond the chosen crown, the program does not draw the lines but color the locator squares (2° × 1°) of the stations involved. The result is a map of «active zones»: where many nearby lilac squares appear, there is a conduit or an opening, even if it does not concern you directly; If the area widens towards you, it is the signal to point the antenna in that direction. The 45° dashboard lets you see under the tropo index and the grid, so compare at a glance the forecast (bottom color) and the real traffic (illus). Your QTH square never colores, because it is the starting point and not a distant area. Only the squares that are all beyond the crown are coloured: a square crossed by the crown, or inside, remains free even if there is one of the two stations. To see the lines, just turn off the box or choose a larger crown.

The criteria that decide what comes on the map and what is discarded are explained one by one in document 1, section 11 ter.

## 9. The timebar

Below on the map: date and time of the prediction shown, cursor from +0 to +72 h at 6 hours, index legend 0–10 with the name of the chosen source, and time of the model race for isobare. Trumpet and isobare index follow the cursor; spots, stations listening and your decodings are observations of the moment and always remain those “adex”. When the cursor is not on +0 they are attenuated, to remember that they do not belong to the forecast time.

## 10. How to read the overlap

- High pressure + high index + spot along the colored area: the opening is there and is the one planned. It's time to call in that direction.
- High index but no spot: or no one is active there, or forecast is too optimistic. The listening layer helps to distinguish: if there are receivers in the area and feel nothing, the second hypothesis is more likely.
- Long spots outside the colored areas: high duct, which the surface forecasts do not see, as in the LA– UA6 [3] event; or another propagation escaped from the filter (e.g. E sporadic without tag). DF5AI and DK5YA distinguish tropo and E sporadic also from the hour: openings that are born and end up together with a diffuse E sporadic are hardly [5] tropo.
- Hepburn and F5LEN disagree: compare the two sources with the buttons and see where the real spots fall.
- Pressure down: G4MCU followed an opening on the SK6VHF beacon until the pressure dropped; 1012 hPa considered the finished event [13]. Watch the isobare by scrolling the timebar: if the high pressure withdraws, the opening probably follows it.
- Signals that go up: in the WSPR study at 144 MHz of KP4MD the signal/noise ratio grew right in the windows where Hepburn predicted an improvement, and dropped when the forecast took [14]. if on the same path grows, the opening is forming.
- Mountain paths: the index does not take into account the [1] reliefs, but the connections beyond the Alps also exist thanks to convective cells and waves of atmospheric gravity [4], and LA0BY worked 9A and S5 through the mountains without Hepburn predicting it [3].

## 11. Open point

- Permission of Hepburn and F5LEN to use their maps in the program.
- Calibration of conversion coefficients F5LEN → scale 0–10.
- Calibration of control points of any kind of map, to be remake if sources change graphics.
- Optional route with temperature at altitude (850 hPa) from the GFS.

## Main sources

1. W. R. Hepburn, DX Info Centre, “The Hepburn Tropo Index (HTI)” and “Tropospheric Ducting Forecast” pages (dxinfocentre.com). Index definition and scale, negative values and more than 10, exclusion of scatters and reliefs, steps of 3 and 6 hours on 6 days, upgrade to 18 UTC. Origin of data from the global Canadian model: G. Meisse, AMSAT-BB, 27/08/2007.
2. F5LEN, forecast for Europe (tropo.f5len.org/forecasts-for-European) and world prediction of the VHF tropopheric propagation (tropo.f5len.org/WW) and noted “New version of the VHF tropospheric propagation forecast tool”. Unit index N, 40 steps from 3 hours, 4 updates per day, value and locator per click.
3. S. Heck LA0BY, “Extreme Tropo Propagation on 144 MHz and up”, 41° Nordic VUSHF Meeting, Skjeberg 2019. Normal gradient 6.5 °C/km and inversions up to 10–15 °C/km, temperature and humidity role, high stable pressures, LA– UA6 event visible in high temperature and not in Hepburn, LA–9A/ S5 connections beyond the mountains.
4. F. Egano IK3XTV, “Propagation VHF – The contribution of convective cells and atmospheric gravitational waves in the Tropo propagation.” Importance of the first 3 km, marine paths, convective cells, gravity waves on the Alps, high pressure connections, common volume up to 3–4 km.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, “Very Long Distance Propagation in the 144 MHz Band”, 24/7/2003. Vertical profiles of temperature and dew point to recognize inversions, use of the Hepburn index, distinction between tropo and E sporadic according to the hour.
6. J. N. Gannaway G3YGF, “Tropospheric Scatter Propagation”, QST November 1983. Earth radius equivalent and loss of path used for index and spot card (see document 1).
7. The VHF/UHF DX Book, chapter 2 “Tropospheric propagation”, pp. 2-23...2-30. Figure of refractive zones according to dN/dh (standard −39 N/km, limits −79 N/km and 0) and surface and high duct profiles. The text of the supplied copy is not readable: only the figures were used.
8. Project summary of 8/11/2026, provided by the author of the program. Map colors, required projections, layers, timebar.
9. Natural Earth, vector data of coasts and borders, public domain (naturalearthdata.com). General reference, not annexed.
10. J. P. Snyder, “Map Projections – A Working Manual”, USGS Professional Paper 1395, 1987. Formules of Mercator projections and azimutale equidtanti. General reference, not annexed.
11. Recommendation ITU-R P.453, radio refractive index. Refractive formula N and duct threshold at −157 N/km. General reference, not annexed.
12. NOAA NCEP, Global Forecast System (GFS). Pressure at sea level at 0.25°, four races per day. General reference, not annexed.
13. G4MCU, articles with tag “tropospheric” (g4mcu.net/tag/tropospheric), 2018–2025. Refration, conduit and scatter; more frequent ducts with high pressure, especially on the sea; beacon SK6VHF followed up to the drop in pressure at 1012 hPa (June 2019).
14. C. F. Milazzo KP4MD, “VHF/UHF WSPR Propagation Study” (qsl.net/kp4md/144_mhz_wspr.htm), 2012–2014. SNR of the WSPR reports to 144 MHz growing in the improvement windows predicted by Hepburn and falling when the forecast disappears.
15. Open-Meteo, weather forecast API (open-meteo.com), free non-commercial use. Pressure at sea level of the GFS model on multiple points in one request.
16. mqtt.pskreporter.info, distribution MQTT in real time of the commercials of pskreporter.info: nomination and landlord of recipients (see document 1).
