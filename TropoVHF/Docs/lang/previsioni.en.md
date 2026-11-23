# Forecast manual

> Machine translation from Italian: if in doubt, the Italian guide is the reference.

The Forecast tab calculates the tropopheric propagation by itself: an index map on the visible area and the signal provided between your station and a corresponding one. This manual describes every data, how it is calculated and which source it comes from. The numbers in square brackets refer to the sources at the bottom; in the program each data of the results has a comic (simbolo i) with the same source and the reference to the paragraph.

## 1. What he does and what he does

The prediction of the GFS [1, 2] model's vertical temperature and humidity profiles derives the reactivity of the air layer by layer and estimates how much the signal gains compared to a normal atmosphere. On the route it adds the real ground (Copernicus GLO-30 [3]), diffraction, troposcatter, ducts and receiver noise.

It is not a measure: it is an estimate made on a weather model with distant levels hundreds of meters. It serves to compare situations (today better than yesterday, 70 cm better than 2 m, this treat better than that), not to guarantee a connection. Paragraph 14 explains how to collect evidence to validate it.


> "The propagation cannot be generalized, but studied in relation to local morphology. »
> — Volker Grassmann, DF5AI, reported by Flavio Egano, IK3XTV (Propagation on 2 metres)

It is the principle that guides the point-point calculation: each route is calculated on its soil, with its horizons, its sea and its reliefs; the map gives the general picture, treats the answer.

## 2. Language commands

- Band: 144, 432 or 1296 MHz. Map and calculation use the chosen band.
- Now: the cursor moves the forecast from now until 72 hours ahead, within 3 hours.
- Update forecast (visible zone): download the GFS profiles on a grid of up to 240 points covering the visible map. The grid pitch (0.5° or more) depends on the magnification.
- Stop: interrupt a download or calculation.
- Details to show (right panel of the map): three levels, Essential, Normal and Complete, turn on a group of boxes; each box can then change by hand. The boxes are in three groups:
  - On the map: isolinee del Gain, also 3–7 dB (tracting), isobare, thermal inversion, weather signs S ↓ ↑, correlation gain/pression, zone of direct view (paragraphs 4, 5, 7 and 9 bis). The colors always stay.
  - In the calculation of the signal: all lines of the calculation, flat Earth profile, calculation log and detailed file log.
  - Corrections and live data: weather corrections (Misme, subsidence), air-sea contrast, average with spots (corridoi), APRS 2 m jumps and LoRa, beyond the mountain without losses (paragraphs 4 bis, 9 bis, 9 quater and 10 bis).
  - Essential holds only isolinees, weather and media corrections with spots; Normal adds isobare, inversion, weather signs, direct view, flat earth, air-sea and APRS; Complete turns it on. The level only changes what you see and what corrections enter: the calculations remain the same.
- Source of data and historical case: from where the profiles come and which day you look (paragraphs 3 bis and 3 ter).
- My station and the correspondent: Locator, antennas, power, cables, noise (paragraphs 7 and 12).
- Calculate the signal and coverage in optics: the two calculations on the ground (paragraphs 8–13).
- Calculation register: each step with the numbers used, including in the Data/ forecast-calcoli.log file.

## 3. From GFS data to profile

For each point of the grid and every hour we read the pressure to the ground, temperature and humidity at 2 m, the wind at 10 m, the height of the limit layer, the CAPE and temperature and humidity relative to 1000, 975, 950, 925, 900, 850, 800, 700, 600 and 500 hPa. From 1.3.0 you can also read the wind to 850 hPa and the vertical speed to 850 and 700 hPa (paragraph 9 bis).

The levels are calculated with the ipsometric formula and the virtual temperature; levels below the ground are scarce. For each level:

- N = 77,6/T · (P + 4810·e/T), with T in kelvin, P and in hPa, and by the saturation pressure of ITU-R P.453-14, eq. 9, with the EF correction factor (recontrolled on the official text) [4].
- M = N + 157·h, with h in km: the modified reactivity. Where M goes up there is a conduit.

## 3 bis. Data source

The Data Source menu chooses where the profiles come from:

- Automatic (recommended): GFS first from NOMADS [14], the NOAA service that distributes the model: for every moment the program asks two cutouts of the area in G RIB2 format and reads them by itself; NOMADS has no daily limit, only one per minute that the program respects. With wide grids (from 0.5° to above) it uses 0.5° or 1st files every 3 hours, much lighter. The points that NOMADS does not give, or all if NOMADS does not respond, ask Open-Meteo [2].The two sources give the same model and the same race, so you can mix in the same grid.
- GFS from Open-Meteo: only Open-Meteo, as in 1.2 (at the limit per minute is expected).
- GFS from NOAA NOMADS: only NOMADS. Slower to download (two files per hour, half a second between one request and the other not to exceed the NOAA limit), but does not consume the Open-Meteo limit.
- ICON-EU (DWD) and ECMWF IFS 0.25°, from Open-Meteo: models other than the GFS, which apply throughout the grid and for the routes. They do not mix with the GFS: compare two models on the same time is a simple way to see how the forecast is uncertain. ICON-EU covers only Europe; ECMWF does not have all GFS pressure levels (the missing levels jump) and may not have the limit layer.

The source used is written in the status line and in the first line of the results (Data source), with how many points came from each service and how many from the cache. The disk cache is separated by model.

## 3 ter. Historical cases

The Historical Case menu reviews a documented opening with the Open-Meteo [15] archive GFS forecasts: the map moves to the area of the case, the band sets itself and the time cursor leaves from the first day of the case. By pressing Update forecast you download the profiles of those days; signal calculation works as for current forecast. It serves to compare the model with what really happened.

The first case is that of 14–15 August 2022 told in QST (November 2022, "The World Above 50 MHz" [23]): an opening under a high pressure between Manitoba and Kansas/Missouri, from 144 to 1296 MHz almost all in FT8, with the Canadian record at 10 GHz of 1228,56 km in Q65 and signals up to 1941 km. The second is the opening between 29 December 2021 and 2 January 2022 on the Tyrrhenian and the western Mediterranean at 144 MHz, from the reports of IC8TEM: first openings on 29 (up to 737 km), wider on 31 ( IT9GSF – EB5EA 1217 km), great opening on 1 January between Italy, Sicily and Spain. For older cases (e.g. September 2000) the Open-Meteo archive has no data. In the archive there is no limit layer height: there its correction does not apply.

## 4. The index and colors

For each layer between two levels the program compares the N drop with the thresholds of table 6.5 of Bean and Dutton [5]: for example between 1000 and 850 hPa a decrease up to 60 N/km is normal, over 100 N/km is super-refraction. A super-refractive layer is worth 10 to 15 dB of gain; a conduit that traps the band from 20 to 25 dB, according to the deficit of M. Strong wind to the ground, high limit layer and high CAPE reduce the gain, because they remove or break the layers [5, § 4.4].

The color is the estimated gain in dB, with a fixed scale equal in all themes:

| Gain | Colour | Meaning |
| Under 7 dB | Nobody | almost normal atmosphere |
| 7–10 dB | blue | gradient over normal |
| 10–13 dB | green | super-refractive layer |
| 13–16 dB | yellow | strong layer, but without duct for band |
| 16–18.5 dB | orange | very strong layer or too thin duct |
| 18,5–21 dB | red | Conduit Trapping Band |
| from 21 dB | Purple | Strong and deep duct |

With the display box also 3–7 dB zones between 3 and 7 dB, where something is being formed but remains under the scale, you can see with a light gray restraint; colors don't change. A dotted pane delimites the area covered by its own forecast and shows the maximum visible gain: if it is under 7 dB the map remains without colors because the atmosphere is almost normal, not because the data is missing. When downloading a new click on Update forecast does not interrupt; to stop it there is Stop.

The colors are opaque to 82%, so the background of the theme just changes them. The locators grid and square names are designed over the colors. Where your prediction is not there but Hepburn or F5LEN give 5 or more chess cards appear with the color of that source.

The map judges every point alone. The signal calculation instead controls that the layer is continuous along the route (paragraph 9): a route that crosses very yellow can give less than the map makes you think.

## 4 bis. Forecast mediated by commercials: corridors (from 1.3.3)

Copyright Policy: A link alone must never draw a conduit. An isolated and short reception can be the scatter of a transiting plane (the APRS package is very short), an opening of a few minutes or an error, and would give "drop-conduct" they deceive. The program recognizes a corridor when, in the same band:

- at least 3 pairs of independent stations (no shared station) work on routes of at least 300 km;
- the routes are parallel: azimut within ±15°, average points within 150 km of the common axis;
- all in the last 30 minutes, with the beam repeating at distance of at least 10 minutes (persistence);
- for the ARS, a jump counts only if the same jump is repeated;
- from 1.3.4, the routes over 1500 km count only if the beam comes from at least two different sources (e.g. PSKReporter and a cluster): from a single source, to those distances in 2 m, are more often EME, scatter meteor or wrong locators than tropo.
- from 1.3.4, do not count the stations 'to ray': who in the last hour has at least 5 correspondents over 1000 km spread over a fan of at least 150°. In 2 m the trope is directional; a receiver that "free" together Israel, Portugal, England and Italy from 2000–3700 km, often with positive signals, has almost certainly the frequency configured badly. In the 17–22/11/2026 I5MZY archive 72% of accommodation in 2 m over 1000 km came from stations so; on 22/11 at 10:53 UTC were OH6HLH, OZ1ITS, F4LUK, OM8ABC and SP2JLR, and with the filter the longest corridor descends from 2175 km (regole 1.3.3) to 615 km.

Sources: cluster spot (not AS/ACS), PSKReporter via MQTT, its QSO and WSJT-X/JTDX/MSHV decodings, and APRS jumps in 2 m and LoRa 433/868 MHz.

APRS: 2 m and LoRa. The program reads APRS-IS in read-only (rotate.aprs2.net, filter on Europe) and extracts radio jumps between known location stations (sorgente → digipeater → IGate), such as the VHF map of DXView and APRS PropView (RF-YVY, Apache 2.0). Three bands are worth: 2 m, LoRa 433 MHz (band corridors 432) and LoRa 868 MHz (corridoi that count for 1296: a conduit that brings the 868 MHz also the highest frequencies). The band comes from the LoRa tocall (aprs-deviceid list) and from the frequencies or words in the comment; a jump is only worth between two stations of the same band.

The delicate point is the active repeaters: a signal received at 500 km can be arrived with two or three digipeater retransmissions. A jump counts therefore only if it is between two real and consecutive transmitters:

- the path must be traced: every digipeater that has retransmitted has put its name, and the jump is measured between one and the next; an alias used without name (W IDE1 *, W IDE2 *, regional alias) breaks the chain and that stretch does not count;
- hidden retransmissions: the program remembers the path that each source usually asks (for example W IDE2 -2). If a package comes with a leap in less (W IDE2 -1) and no name explains it, a digipeater retransmitted it without tracking and the package is discarded; the sources never seen before do not count;
- digipeater in the middle of the road: if between the two stations, more or less on the road, there is a digipeater of the same active band in the last three hours, the jump is discarded, because it could have retransmitted (it is especially for the digipeater LoRa that retransmit without changing the path);
- non-radio packages (qAC, qAX, TCPIP), those operated by internet on radio from an IGate (third parties), HF and different frequencies, stations at altitude over 1500 m (palloni, aircraft) and jumps over 1500 km.

Effect on the map (from 1.3.4). It draws only the area where the minimum guaranteed is valid: a band of ±60 km around the central part of each route (from 15% to 85% of its length), not on stations at the ends, where the conduit may not be there. The bands of all corridors are drawn together, in a neutral color (the text) and semi-transparent: the overlaps do not add up, and the color does not confuse with the purple of the scale (21 dB and beyond). The labels (traits, average distance, minimum gain) are scanned with each other; passing with the mouse on a label the routes of the corridor appear.

In the area, and only near the current time (±90 minutes), the forecast does not fall below a minimum. The reasoning is: if the stations really pass, the propagation is there, whatever the weather model says.

Because short routes are worth little (from 1.3.4). In 2 m, between 300 and 600 km, two good stations (some hundred watts, antennas, FT8 or CW) connect almost every day even with the normal atmosphere, for troposcatter: the signal diffused by the small irregularities of the air in the common volume of the two bundles. Three or four spots at 500 km say that the band works as usual, not that there is an opening: GEKO Magazine n. 69 ( IU3QEZ) estimates for the troposcatter with 2×10 flowing elements of about 440 km in SSB QRP, 510 km in SSB with 50 W, 600 km in CW QRP and 640 km in SSB with 500 W (sources, eleven lap). Only over 600–700 km the troposcatter is no longer enough and a series of parallel connections really indicates a conduit or layer. Up to 1.3.3 a corridor of 450–600 km raised the forecast to 9–12 dB, and the map showed an improvement that was not there.

The starting value therefore goes with the average distance of the routes:

- up to 400 km: 3 dB;
- from 400 to 700 km: salt in a linear way from 3 to 10 dB (500 km → 5,3 dB; 600 km → 7,7 dB);
- from 700 to 900 km: from 10 to 16 dB; over 900 km remains to 16 dB.

Under the 7 dB, that is under the beginning of the scale, the minimum remains that: the corridor is drawn with its label, it is seen that the band works, but the colors of the map do not change (with the box also shows 3–7 dB the gray dotted). Above the 7 dB the value is weighed, half and half, with the number of independent routes (3 strokes → 0.25; 6 or more → 1) and with the median S/N of relationships (−20 dB → 0.5; 0 dB or more → 1; not known, as in cluster spots, → 0,75), starting from 7 dB. Examples: 3 routes without S/N on 500 km → 5,3 dB; 5 routes on 615 km → 7,7 dB; 3 routes without S/N on 900 km → 11.5 dB; 3 routes to −20 dB on 900 km → 10,4 dB; 6 routes to 0 dB on 900 km → 16 dB.

The minimum distance remains 300 km: raising it to 500 km would have also removed the real short-range openings (for example a conduit on the Tyrrhenian at 400 km, which is recognized by weather forecast and very strong signals more than from the distance alone). In the I5MZY archive, on 22/11/2026 at 10:53 UTC, with this rule and the ray filter the longer corridors were 615 km (7.7 dB) and 540 km (6.3 dB); with the rules of 1.3.3 it reached 2175 km and 16 dB.

Until 1.3.3 the minimum was valid on all the rectangle of the corridor and depended only on the distance (from 10 dB to 300 km to 16 dB to 900 km): with many long corridors together (for example on 22/11/2026, twelve corridors up to 2620 km) lifted almost all Europe to 16 dB.

The gain curves no longer draw «gocce»: the smaller islands of 220 km in both directions remain coloured but without closed curve, unless a corridor confirms them. It switches off with the "Prevision mediated by the spots".

## 5. Optional lines and comparison with pressure and inversion

Gain curves: lines at 7, 10, 13, 16, 18,5 and 21 dB, the edges of the scale, drawn on the field smoothed with the method of the marching squares, with the label in dB.

Isobare: pressure at sea level every 4 from the same GFS, to the hour of forecast, with the centers H and L.

Thermal inversion: curves covered with how much air at 925 or 850 hPa is warmer than that at 2 m, at +1, +3, +5 and +8 °C.

Under the controls a line gives the gain agreement with the pressure and with the reverse on the visible area: Pearson correlation coefficient, −1 to +1. The ducts are often born in anticyclones (the air that drops heats and forms the reverse), so a positive agreement with the pressure is normal; but inside the same anticyclone the gain changes a lot between sea, coast and mountains. The reverse alone is not enough: bend the waves only if above the air is also dryer [5]. The line serves to see, now for now, which of the two best explains the colored areas. The same comparison is written in the register.

## 6. Potential pipelines: double calculation

GFS levels range from 200 to 1000 m: a thin inversion is spread and the model does not see the conduit. If between two levels the N decrease exceeds normal (40 N/km), the air is stable (thermal gradient under 3 K/km) and above is dry (specific humidity ratio below 0.8), the program considers a potential duct and calculates it in two ways:

- Calculation A: all the concentrated excess drop in 50 m.
- Calculation B: the same decrease distributed on the typical thickness of the pipelines of that area, taken from the climatic maps of ITU-R P.453 [4] (at the ground or elevated).

For the deficit and for the maximum trapped wavelength the lowest value is held: it is a prudent estimate.

ITU-R P.453 data are © ITU and can be used for personal and non-commercial purposes by citing the source, but cannot be redistributed: for this I am not in the program package. To turn on the calculation B, just download the P.453 maps from the ITU in the tab: the program, after showing the conditions of use, download the archive of the pipelines from the ITU site (elevated_and_surface_duct.zip) for the user's personal use, extract the files in Data/ P453 and turn on the calculation without rebooting. Alternatively, download from the ITU-R Study Group 3 digital product page (itu.int, ITU-R Study Group 3, Digital products, voice P.453, Surface data and Elevated duct data) and put the seven S_OCCUR.TXT, S_THICK.TXT, S_STREN.TXT, E_OCCUR.TXT, E_base.T Without the files the program only uses the calculation A and writes it in the tab and in the registry. The ratio between deficit A and typical intensity P.453 is anomaly: How many times today's pipeline is stronger than normal. P453

## 7. Stations and Locators

The landlord can have 4, 6, 8 or 10 characters. With 8 or 10 characters the position is accurate and the soil share comes from Copernicus GLO-30: the height of the antenna is enough from the ground. With 4 or 6 characters the square is too large: it is better to write the quota of the antenna on the sea; if 0 the program remains the estimate with the SRTM soil at the center of the square plus the height from the ground and signals it in the results. For the corresponding the default height is 3 m from the ground; with sea share 0 is used SRTM soil plus height.

## 8. Land, diffraction and troposcatter

The profile of the route is sampled on Copernicus GLO-30 every 30 m (more than 120 km wide). The equivalent Earth radius is k = 157/(157 + ΔN), with ΔN the decline of N in the first km from the GFS profiles along the [6, 7] route.

Reliefs close to stations (from 1.3.2, author's criterion). P.452 pays the horizon angle linearly, 0.573 dB per mrad: a relief of a few tens of meters to a few hundred meters from the antenna costs tens of dB, much more than is observed. For this reason the reliefs within 20 km from each station can add up to 6 dB: the horizon angle due to them is limited to that of the most distant reliefs plus 10,5 mrad. It is valid for your station and for the correspondent. The line «Near reliefs» says how many dBs have taken off.

Source of land (from 1.3.2). The quotas come from Copernicus DEM GLO-30 [3]: 1 second arc, the sea is 0 and the coast is clear. Until 1.3.1, the program used the Terrain Tiles 'skadi' boxes of AWS. Near the coast those squares mix the SSRTM with low resolution seabed data and spread the island's share on the sea: around Capri create a ghost ring of 80–100 m between 0.5 and 1 km from the shore. On 21 November 2026 IC8TEM reported an unexisting horizon to the northwest; the point-by-point comparison with Copernicus confirmed it (94 m at sea to the northwest of Marina Grande vs. 0). That false horizon, of +2,6°, cost about 26 dB of troposcatter to the coastal stations. The skadi panes remain only as a reserve when Copernicus does not respond. Copernicus frames weigh 15–30 MB and download once; those of only sea do not exist and are worth 0.

- Free space: Lfs = 92,44 + 20 log f(GHz) + 20 log d(km) [8].
- Diffraction: Bullington method on the real profile plus ITU-R P.452-16 § 4.2 [6] delta-Bullington spherical land correction, with ITU-R P.526 [9] spherical loss.
- First Fresnel zone: radius √(λ·d1·d2/d); free if the ground leaves at least 0.6 of the radius. For each obstacle, the [9] Knife Board V and J(VV) parameter is shown..
- In the design of the profile the direct radius is green in optical and red view if it passes under the ground or under the curvature of the earth; red dots are the obstacles that invade the 1st area of Fresnel (purple dotted line). On the sea the "red point" at altitude 0 m is not a mountain: it is the sea rising for the curvature. Passing with the mouse on the radius or on a point appears a comic with explanation and values.
- When a relief near the antenna closes the direction (for example a horizon of tens of degrees), diffraction and troposcatter give losses of hundreds of dB: above 300 dB the program writes "> 300 dB" and "direction closed by a neighboring obstacle", the signal "< −230 dB" and the S/N ratios "−60 dB". In that case it is advisable to look at «Recommended plant» and «Via monte».
- Troposcatter: ITU-R P.452-16 eq. 45 to 50% of the [6] time, with angular distance from the horizons and N0 from the profiles.
- Without tropo: sum of the powers of diffraction and troposcatter, never under the free space.

## 8 bis. Troposcatter: four models and uncertainty

Next to P.452 the program calculates the troposcatter with three other models, as does the TropoLink [16] program, and shows their difference as uncertainty:

- NBS TN101 § 9 [25] (from 1.3.5), median: L = 30 log f − 20 log d + F(θd) − F0 + H0 + Lgp. F(θd) is the attenuation function of Annex III in its analytical forms (Ns 301) with the correction for Ns, H0 the frequency gain function of § 9.2: at 144 MHz, with low antennas in wavelengths, the reflection from the ground partially erases the field in the lower part of the common volume and the loss salt of 3–10 dB. Ns is that of the hour GFS profiles, the effective horizons and heights come from the Copernicus profile, Lgp = 0.07·e^(0,055(Gt+Gr))) is the opening coupling loss. It is the same form that the ITM uses in its interior, but with the reactivity of today instead of that of the climate. It tests on flat sections at 144 MHz with antennas at 20 m: within 2–4 dB from ITM between 150 and 900 km and about 2 dB in the simplified form 9.2b of TN101, which does not have H0.

- ITU-R P.617-5 [17] L = F + 22 log f + 35 log θ + 17 log d + Lc, with f in MHz, θ in mrad, d in km, F = 0.18· N0 ·e^(−hs/7,35) − 0.23·ΔN and Lc = 0.07·e^(0,055(Gt+Gr)). N0 and ΔN are usually taken from ITU's climate maps; here we use every hour the profiles GFS ( N0 ·e^(−hs/7,35) is the reactivity to the ground, ΔN the decrease in the first km), so also the troposcatter follows the time.
- RSGB VHF/UHF Handbook Formula, DL2FZN [18]: L = Lfs + 24,44 + 4,283 ln f + 0.06656 d. The slope with the distance is almost equal to that of PROPBER (0,067 dB/km).

Common volume (from 1.3.1): is the point where the radius of the two stations is crossed, each inclined to its horizon angle, on a Earth of equivalent radius ae = k·6371 km. From the TX the radius rises as z = hTX + x·θt + x2/(2·ae); The distance x = (hRX − hTX + d^θr + d2/(2·ae) / θ, with θ = d/ae + θt + θr. With flat horizons and antennas at the same quota the base is worth d2/(8·ae): about 1.3 km to 300 km, 3.7 km to 500 km, 9,4 km to 800 km. The higher the volume, the more the air is dry and rarefied and the less it spreads: for this reason more than 800–1000 km the troposcatter runs out. In the results are the share and position of the base, the reactivity, the gradient and the humidity of the GFS at that altitude, and the equivalent distance of Bray (QST, November 1961): each extra or less horizon is worth about 111 km away. Disciplinary reference: the presentation "Beyond the horizon in VHF" by IU3QEZ (GEKO Magazine n. 69).

The Troposcatter line, other models shows the two losses and uncertainty as half the difference between the most optimistic model and the most pessimist. They are only comparisons: the loss without tropo remains that of P.452.

## 8 ter. afternoon troposcatter over reliefs (from 1.3.4)

IK3XTV, from Thiene ( JN55, with the Alps at 10 km), works in summer afternoons southern Germany at 400–500 km, without sight, with strong signals but with rapid and deep evanescence; the author attributed them to "convective cells aligned as a train of lenses". The observation is confirmed; the explanation adopted in the program is another, physically firmer.

In the afternoon the sun stirs the air over the reliefs and the limit layer rises to 2–3 km. At its top the humid air below meets the dry air above: there the turbulence of the refractive index (the size that governs the troposcatter) is maximum. When that top reaches the common volume of the route, which on 400–500 km is about 2–3 km, the troposcatter is reinforced. This explains the afternoon time, high pressure (silent sky, strong heating) and rapid evanescence, typical of diffusion and not of the ducts. Convective cells do not act as lenses: produce turbulence, and it is the turbulence at the top of the recast layer that spreads the signal.

Consequences for reading forecasts:

- on the earth the first afternoon is the worst time for the low ducts and layers, but not for the troposcatter beyond the mountains: the two things should be read separately;
- the program already has the two data that serve, the height of the limit layer provided by the model and the share of the common volume of the route (register "Common Volume" in the results): when they are close, an afternoon mountain treat can go better than the calculation in normal atmosphere;
- from 1.3.4 the point-point calculation takes into account: if the common volume is on the earth, along the route there are reliefs of at least 800 m, the sun is over 15° and the limit layer exceeds 800 m, and the top of the layer reshaping (model soil rate + limit layer height) is within 500 m from the base of the common volume, the troposcatter gains 3 dB; between 500 and 1000 m the linear gain falls to zero. The line «Troposcatter pomeridiano». With snow along the route (at least 10 cm on the ground according to the model, snow_depth variable of Open-Meteo or SNOD of the GFS from NOMADS) the reinforcement does not apply: on the snow the soil remains cold and the re-shaped layer does not grow. The value of 3 dB is provisional and must be calibrated with real links; it turns off with weather corrections.

Sources: observations by F. Egano IK3XTV (Radio Rivista e www.qsl.net/ik3xtv, «Propagazione sulle 2 metri», «Celle convettive», «Troposcatter over the Alps»), sources § 13.

## 9. The founding layer along the route

It is the heart of signal calculation. The GFS profiles are taken along the route every about 25 km. For each layer of pressure, the longest stretch of the same layer is found on consecutive points; count as duct only thick layers at least 40 m [10].

- continuity: from what km to what km, and the fraction of the route;
- top and base, and inclination in m/km;
- soil inside the layer: the km where the mountains rise above the base;
- Covered land: on the sea the conduit is almost always, decides the stretch of land [10]. If the land exceeds 10% of the route, the fraction used is the minimum between continuity and coverage of the earth.

The trope gain is the gain of the layer for the used fraction. If more than half of the profiles are sub-refractive the gain is negative.

## 9 bis. Weather corrections: stability, subsidence, strike

From 1.3.0 the profile also gives three meteorological information, which can be seen in the register (regave «stability (Misme)»), in the Meteo line along the route of the results and, with the Segni meteo box, on the map.

- Stability of Misme [5, Chapter 6]. Misme binds the field beyond the horizon to a compound parameter: M = a·(ge − 40 + b·ΔW), with a = 0,5 dB per N/km, the equivalent gradient ge and the thermal stability ΔW, the work to raise the air of 1 km. The program calculates ΔW = g·∫(θ − θ0)/θ0 dz between soil and 1 km above, with θ potential temperature; the standard atmosphere is about 56 J/kg. The term added to the gain is 0.07 dB for J/kg beyond the standard, limited between −4 and +6 dB, and counts only where no conduit traps the band (the gradient is already in the Bean and Dutton index). The calibration is temporary: it must be verified with the ratios (paragraph 14).
- Subsidence and uphill air [19]. Moler and Holden, on routes of 125–305 km to the sea at 1365 and 9380 MHz, found the highest signal of about 2.5 dB on average with air coming down that with air rising, and in a case a reinforcement of about 20 dB for 3 hours at the beginning of the subsidence, followed by a decrease of 22 dB when the air returned to converge. The GFS gives the vertical speed to 850 and 700 hPa. If at both levels the air drops more than 1 cm/s and there is a reversal or a sharp drop in the potential refractive index K (N reported adiabatically to 1000 hPa, which highlights the dry and warm air dropped from the top [5]), 1.5 dB is added; if the air rises more than 5 cm/s if they take off 2.
- [20] Scirocco. Hot and dry air from Sahara over cooler marine air gives very strong inversions from the eastern Mediterranean to Gibraltar. The S sign appears where at 850 hPa the wind comes from 100–200° with at least 7 m/s and the temperature is at least 16 °C, on the Mediterranean. It's just an indication: the gain decide the layers of the profile.

Without a conduit that traps the band the total gain does not exceed 16 dB, the end of the yellow: corrections make stronger layers more evident but do not pass them by ducts (red and purple remain at the ducts).

- Persistence (from 1.3.1). The biggest openings come after good high-pressure days (VHF/UHF DX Book; Biancomano and Leyshon 1998, rule of 12 hours; the reports of 29/12/2021–2/1/2022 grow from the first to the fourth day). For each cell the program counts the hours of the previous 24 with gain of at least 13 dB and adds up to 2 dB in proportion, only where the gain is already at least 10 dB.

How to read the signs on the map (Change Weather Signs; from 1.3.2 the symbol is large double and passing with the mouse over a sign appears a comic with the explanation and values of the cell):

| Sign | Colour | What does that mean? | Effect on the trope |
|---|---|---|---|
|  ↓  | green | subsidence: air drops (anticyclone), heats for compression and dry, strengthens the reverse and keeps it firm | favorable, +1.5 dB |
|  ↑  | red | uphill air (front, low pressure, convection): mixes low layers and breaks inversions and ducts | unfavorable, −2 dB |
|  S  | orange | Syrup: hot and dry air from Sahara over fresh sea air | favorable clue, no correction: decide the layers |

An area full of green ↓ under an anticyclone is the classic sign of a hard opening; Red ↑ arrows along the line say that the layers are breaking, although the color of the cell is still good.

The Weather Corrections box turns off these corrections to see the model of 1.2.

Validation (paragraph 14): on the 593 connections to 144 MHz between 29 December 2021 and 2 January 2022, compared with the same routes and two weeks later, the separation index (0,5 = none, 1 = perfect) is 0,79 without corrections and 0,91 with corrections (before the roof at 16 dB). The same comparison shows the main limit: the conduit on the sea at low altitude, which in those days brought the signs between Campania and Liguria, is not visible at the levels of the GFS.

## 9 ter. News 1.3.2: two estimates, ceiling, high pressure, sea, stations, ridges

prudent and aggressive estimate. The gain from the gradient grows from 0 dB to the limit of normal up to 10 dB to the threshold of super-refraction. In the 1000–850 layer, the prudent threshold is Bean and Dutton (100 N/km); the aggressive threshold is the classic Super-Refraction threshold of 79 N/km (k ≈ 2; P.453 uses −100 N/km in the first 100 m), which with k50 = 157/(157 − ΔN) is k ≈ 2 (k = 2,5 corresponds to about 94 N/km). The point-point calculation shows the two losses flanked; with a conduit that traps the band coincide. The map uses caution.

Attic of 2500 m. Roads and ducts are only searched for up to 2500 m above the ground: the higher the antennas of amateur radios do not exploit them. With 1500 m the validation lost a separation point and 4% of the connections above 16 dB, because in winter the inversions from subsidence are often between 1500 and 2500 m.

High pressure extended. +1 dB where at least 7 of the 9 cells around have reduced pressure to the sea of 1020 hPa or more and the wind is weak in the low layers (10 m under 5 m/s, 850 hPa under 8 m/s), except for ascending motions.

Sea weight. In point-point calculation, the gain of the layers is multiplied by 0.8 + 0.4·μ1, where μ1 is the factor of ITU-R P.452 (eq. 51) on the longest continuous stretch of land: 1 on the open sea, small with long stretches of land.

Stations and conducted. When a conduit that traps the band covers only part of the line, the share of each antenna is compared with base and top of the conduit at the beginning and end of the covered section; the gain is multiplied by 0.5 + 0.5·√(e^(−ΔhTX/300)·e^(−ΔhRX/300)), with Δh the distance in meters out of the duct. With the conduit on almost all the route is the coupling of § 10.

Layer and ridges. If the top of the boundary layer of the GFS (ground rate of the most boundary_layer_height model) exceeds the ridges of the route (maximum premises over 300 m, if the highest exceeds 500 m), the layer passes over the mountains without breaking: +1.5 dB for the fraction of covered ridges. It is the observation of IT9GSF on the openings from Sicily to Germany with favorable Tyrrhenian.

Levels close to the ground on the sea. With the NOMADS source the program also downloads the surface temperature (on the sea is the SST), temperature and humidity at 80 m and in the layer 30–0 hPa over the ground. On sea points (model rate under 5 m) these levels are added to the profile, under the first level of pressure, to see the duct at 100–200 m that pressure levels do not solve. On the earth are not used: the night and day layers are not yet valid. Open-Meteo only gives the surface temperature.

Evaporation pipeline. From SST, from temperature and humidity to 2 m is estimated the δ height of the evaporation conduit with the neutral logarithmic profile (as in the model of Paulus and Jeske, without the correction of stability): the air to the hair of the water is saturated to the SST (98% for the salt), therefore

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

limited between 0 and 40 m. To trap 144 MHz it would take about 180 m (Kerr), then at 144 MHz the evaporation duct reinforces little; from 432 MHz up, with low antennas on the sea, much (paragraph 11 bis). Enter the parabolic equation and in the line Corrections 1.3.2.

Time of the day (visit). The program calculates the height of the sun on each point. From day to day on the earth, with the sun over 15° and limit layer over 800 m or wind over 5 m/s, warns that the ground ducts are re-shaped; at night, with sun under −3°, wind under 3 m/s and limit layer under 300 m, warns that with serene sky are possible conducted by irradiation, which fall after sunrise. Not on the sea. For now they are spotted, not numbers: December validation does not have enough summer days to calibrate them. These are two points that the author considers important, from daily observation.

Normal profile CRPL (alternative). Instead of 40 N/km fixed the normal decrease follows the exponential reference atmosphere CRPL: N(h) = Ns·e^(−c·h), with −ΔN in the first km = 7,32·e^(0,005577·Ns) (Bean and Thayer 1959). With wet air (Ns high) the normal is steeper and serves a stronger gradient to give gain; in altitude is sweeter. Turns out bad until validation says it helps.

## 9 quater. Contrast air-sea and low seas: the Adriatic (from 1.3.3)

Radio waves do not feel the seabed: feel the contrast between air and water surface. The depth counts because it decides how quickly the temperature of the sea follows that of the air.

The Adriatic is a low sea. The average depth is about 250 m, but the northern basin rarely exceeds 100 m and the Gulf of Trieste remains below 25 m; only to the south descends over 1200 m. A layer of water so thin warms up and cools in a few days, the Tyrrhenian in weeks. There are three typical situations:

1. Spring and early summer, favorable. The sea is still cold, even for the fresh water of the Alpine and Po rivers; it arrives hot and dry air from the plain or the Balkans. Hot air over cold water forms a reversal on the sea (advection pipeline): is the classic condition of the openings between Italy, Slovenia and Croatia.
2. Autumn and winter with bora, unfavorable. Cold air above a warmer sea: the low layer becomes unstable, presses and the duct breaks.
3. After a bora, chance. Just because the sea is low, a bora cools it some degrees in a few days. If you then enter an anticyclone with hot air, the air-sea contrast is strong and the duct is more robust than usual.

The Adriatic is also a closed channel between Apennines and Dinari, less than 200 km wide: the sea layer remains confined, and the routes along the axis (Puglia–Venezia–Trieste, about 700 km) remain all on the sea, the best condition for a long duct.

What the program calculates. On the sea points, with the weather corrections on and the box «Contrasto aria-mare»:

| Condition | Correction |
| --- | --- |
| potential temperature at 925 hPa (or 950) over the SST of 4 K | _ |
| beyond the SST of 7 K | +2 dB |
| with air damp at 925 hPa (over 80%) | −0,5 dB on these bonuses |
| cooled sea of 1.5 °C or more in 72 hours before, with favorable contrast | _ |
| warmer air sea at 2 m of 2 °C (mixing) | −1.5 dB |
| warmer sea of 4 °C | −3 dB |

The total correction remains between −3 and +3 dB and, without conduit, the gain remains under the yellow roof. The thresholds are temporary: in the standard atmosphere the potential temperature to 925 hPa exceeds that of the sea of about 2–3 K, then 4 K already indicates a reverse.

The memory of SST. Models keep the sea temperature almost steady during prediction. The recent decrease is derived from the analysis of the three days before (Open-Meteo, only surface temperature, only sea cells): maximum of 72 hours before minus the current value. If the service does not respond, the memory bonus does not apply.

Limits. The SST of the models below coast and in the upper Adriatic is coarse; the cold pen of the Po, for example, is solved badly. Batimetry does not enter the calculation: the sources always give the SST, which already sums up the effect of the seabed. The air-sea contrast overlaps in part at the end of Misme stability (§ 9 bis); for this the bonuses are small. The thresholds will go tarate with the corridors of spot and the QSO through the Adriatic.

## 10. Conducted on the whole route

If the layer traps the band on at least 80% of the used fraction and the stations are not in sight, the loss in the conduit is also calculated: free space, plus the coupling of the antennas (3 dB if they are inside the conduit, more if they are above or below), plus the residual attenuation γd = 5·10−5·ae·f^(1/3) dB/mrad of ITU-R P.452 eq. 49 [6]. If it is less than the loss without tropo becomes the expected loss.

A band remains in the conduit only if its wavelength does not exceed λmax = 2√2·(2/3)·√(ΔM·10−6)·d (Kerr, eq. 17 [11]). For this the same layer can trap the 23 cm and 70 cm but not the 2 m. However, the limit is not net: Kerr warns that even waves several times longer suffer a lot of the conduit, and Hepburn and the VHF/UHF DX Book speak of "sphered" edges and "forgive" ducts. From 1.3.1 the programme uses a soft conduit: if λmax is between λmax and 3·λmax, the gain gradually passes from 15 dB (λ = 3·λmax) to the full conduit gain (λ = λmax), with the factor s = (λmax/λ − 1/3)/(2/3). In that case the duct colors are allowed. The log and the Meteo line along the route indicate the points with soft duct.

## 10 bis. Via Monte

When the correspondent is closed behind the horizon, the program seeks the best direction to point the antennas: the illuminated slope spreads the signal as a radar target. It makes the circle of the horizon every 5th (except ±15° around the correspondent) and in each direction takes the illuminated horizon point, over 100 m and between 1 and 60 km. A quick estimate (free space, bitactic conversion and 10 dB for each level of horizon from the diffuser to the correspondent) chooses the 4 most promising, at least 15° each from the other; for each:

- treat 1, station → relief, in the free space;
- treats 2, relief → correspondent, calculated as if the relief transmitted from 10 m above the top, with the same prediction (without parabolic equation);
- conversion with the bistatic radar equation, with σ = σ0·A, σ0 = −15 dB (mountain in VHF/UHF) and A the area illuminated by the R1 beam (maximum 20 km2):

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

The "Recommended Point" line indicates the azimuth of the best route, if it costs less than the direct. At 144 MHz, with a relief at 20 km and a yagi from 15°, the mountain road costs about 45 dB more than the 2 route alone: it only happens when the direct is closed by neighboring obstacles. The estimate is gross (±10 dB), especially for σ0.


Without loss to the mountain (option, from 1.3.2). With the «Via monte senza perdite up al monte» box, the fall between the station and the diffraction point is considered null: is like bringing the station to the top ( antenna to 10 m above the point, same power, antenna and cable). The horizon round then chooses the point with the most free horizon towards the correspondent (at parity, the highest) and the result shows the route from the point to the correspondent, with signal and S/N, next to the total with the diffusion. It is an optimistic limit: says if the direction opens when the mountain raises the signal well, or where to go portable. Each point is given with the ten-character.

## 10 ter. Entrance to a distant conduit

If the conduit that traps the band begins far, beyond the horizon of the station, the signal must arrive. The program brings the signal to the starting point, in the middle of the conduit, for diffraction: along the Copernicus profile GLO-30 searches the edge with the highest Fresnel parameter (ITU-R P.526), with the swelling of the equivalent land,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

more the attenuation due to the curvature of the top (rounded obstacle, P.526-16 § 4.2), with the R radius estimated from the Copernicus GLO-30 soil to ±1 km from the edge,

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

(for m·n > 4 is the form 34b of the recommendation). Barsis (NBS, 1963) measured on three long routes beyond a mountain (Colorado, Alaska, Europe) average losses 10–20 dB higher than the ideal edge of Fresnel-Kirchhoff: the rounding explains a lot (Barsis and Kirby, 1961: Pikes Peak with a radius of about 11.5 km). The result compares with diffraction on smooth spherical soil; it is the worst. Then the signal travels in the conduit: free space on the entire route, residual attenuation γd of P.452 on the section in the conduit and coupling of the correspondent (3 dB inside, 1 dB every 10 m outside). If this loss is lower than the expected loss it becomes the mechanism ("conduct (input by diffraction)"). It is the author's premise on Italian geography: with a nearby obstacle well below the line, v is negative and the entrance costs little; with a dorsal front costs 15–30 dB.

## 10 quater. ITM (Longley-Rice) and exit from the duct

From 1.3.2 the program contains the Irregular Terrain Model 1.2.2 of NTIA/ITS (Longley and Rice, ESSA ERL 79-ITS 67, 1968; Hufford, Longley and Kissick, NTIA 82-100, 1982), translated into C# by the official code of the US government, which is free from rights. The translation gives the same numbers as the original: auto compilation compares it each time with NTIA C++ code on five test cases (down below 0.01 dB).

The ITM calculates the loss in normal atmosphere on the real profile Copernicus GLO-30: seen with reflection from the ground, diffraction (spigol and spherical land weighed on the roughness of the soil Δh, that is the difference between the 10th and 90° percentile of the quotas around the straight of the minimum squares) and troposcatter. Ground reactivity is reduced to the average portion of the route, $N_s = N_0\,e^{-h/9460}$, and gives the curvature of the equivalent land,

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

The program uses the radio climate of the chosen route as in § 10 sexies (until 1.3.4 always the temperate sea, with N0 320 on the ground and 350 on the sea), with N0 from the average reactivity of the place (soil εr 15, σ 0,005 S/m; sea εr 81, σ 5 S/m), horizontal polarization and variability over time without that of place (the profile is known). It shows the loss exceeded in 50% of the time and the one that you have in 10% and 1% of the best time. ITM does not model the ducts: its variability over time is statistical, by climate. It serves as a second calculation of the baseline next to P.452; the difference between the two gives an idea of uncertainty.

The second use is the final stretch after a conduit that ends before the corresponding one. At the exit point, a virtual antenna is placed halfway through the conduit and the ITM calculates the tract up to the corresponding one, as excess over the free space at 50% of the time. Total loss is

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,	heta_{condotto} + A_{ITM}(d_{uscita} 	o RX)
```

where A_TX is the coupling of the antenna (3 dB inside, 1 dB every 10 m outside) if the conduit starts near, otherwise the entry diffraction of § 10 ter. It is the limit that the author has given himself: continuity of the layer plus, at most, residual attenuation. If it is lower than the expected loss it becomes the mechanism ("conduct (exit and final tract ITM)").

## 10 quinquies. Deepening: islands, straits and earth-sea discontinuity

An example from the Gulf of Naples (Anacapri, JN70CN) towards the western Mediterranean shows where routes cross the islands:

| Destination | Azimut | Where it crosses the islands | Obstacle |
| --- | --- | --- | --- |
| Barcelona |  279°  | north of Sardinia (Gallura) to 41,05° N, between 420 and 490 km | about 30 km south of the Bocche di Bonifacio; the Limbara (1359 m) about 20 km from the line |
| Bocche di Bonifacio |  282°  | close to 41,3° N | about 11 km |
| Marseille |  296°  | Central Corsica at 42,2° N | mountains up to 2700 m: a conduit under 2 km there interrupts |
| Valencia, Balearics |  260–265°  | Central Sardinia | internal reliefs |

For Barcelona it is enough to point to 282° instead of 279° because the axis passes from the mouths: 3rd, inside the beam of a normal yagi.

Is the mouths a strait? For the low ducts, under a few hundred meters, yes: the islands are walled, and above the earth, in the day, the heating breaks the inversion. The strait itself is wide enough: about 11 km, against a first area of Fresnel, less than one kilometre wide in the middle of a 1000 km route in 144 MHz. The problem is weather. The Mouths are a fun for the wind: The lower layer is accelerated and the lower layer is removed. Often the passage is open as geometry but disturbed as atmosphere. With a steady anticyclone and little wind can instead be the good passage.

Earth-sea disparity. They count. A conduit formed on the Tyrrhenian coast is interrupted: the signal comes out from the conduit (the section after the exit, § 10 quater) and must return to another conduit on the sea of the Balearics, paying again the coupling. The conduits at altitude, above 1500 m, pass over the Gallura without realizing it; for central Corsica does not apply.

In practice: if the route crosses a mountainous island, try some degree of correction towards the stretch of free sea, but do not count on the path with strong wind in the narrow.

## 10 sexies. TN101 radio climate and time variability (from 1.3.5)

The TN101 § 10 [25] describes how the loss of a route beyond the horizon varies over the long period around the median, with different curves by climate: the Y(q) function says how many dB the loss drops in the q% of the best time. The ITM contains those curves for seven climates. The climate counts a lot: on the sea in the temperate marine climate, at 144 MHz, in 10% of the time you earn many more dB than on the land in continental climate, because the ducts are more frequent.

The program no longer fixes the climate: He chooses it for every treat from the average point, so it works all over the world and not only in Europe. Download from Open-Meteo [26] three years of daily values of ERA5 reanalysis (temperature, humidity and soil pressure, rain) for the cell of 0.5° of the average point, once only; values remain in Data/CLIMA. Restoration of soil

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

day by day, then the annual average and excursion between monthly averages, average humidity and annual rain. The rules follow the descriptions of the climates of the TN101 § 10.2; the boundaries between one climate and the other are a criterion of the author:

- over 60° latitude: temperate continental (the TN101 has no curves for the polar climate);
- less than 250 mm rain per year and average humidity below 50%: desert (Ns typical 280);
- within 20° from the equator, Ns of at least 350 and excursion up to 30: equatorial (Ns typical 360);
- within 32° and excursion of at least 60: subtropical continental, that is monsoons (typical excursion 60–100);
- between 10° and 32° with Ns of at least 340: subtropical marine (Ns typical 370);
- both horizons on the sea, over 20°: marine tempered on the sea (it is the definition of the TN101);
- excursion up to 30 near the sea (sea on the route or average humidity of at least 75%): maritime tempered on earth;
- otherwise temperate continental (typical excursion 20–40).

N0 for ITM is the average reduced to sea level, N0 = Ns·e^(h/9460). Without network you use a reserve rule with the latitude alone and the sea fraction, and the line says it.

Tests with data ERA5 2023–2025: Capri Ns 339 with excursion 45 (continental tempered on land, sea temperate on the sea when both horizons are on the sea); London 327 and 16 Vienna 319 and 24 Abidjan 381 and 11 (equatorial); Miami 365 and 36 (subtropical husband); Delhi 337 and 82 (subtropical concontinental); Riad 278 and 37, 90 mm rain (desert). In Chile: Antofagasta 341 and 19 (subtropical husband: the coast of the Atacama, with the almost fixed low reverse of the Pacific), Calama 232 on the ground, 2257 m, 14 mm of rain (desert), Valparaíso 331 and 11 (temperature seam), Punta Arenas 313 and 5 (temperature seam).

The line "Radio climate ( TN101)" shows the climate, the reason, Ns with half of the excursion and Y: how many dBs earn in 10% and 1% of the time and how many lose in 90%. It is long-term statistics and remains separated from the forecast: the expected loss follows the time of today with the GFS profiles, and sum the two things would count twice the same abnormal propagation.

## 11. Comparison with ITU-R P.452

The P.452 line applies the ITU method for ducts and reflection on layers (P.452-16 § 4.5 [6]) with the ΔN median of the GFS instead of the climatological one and p = 20% as a calibration constant, as in the CSIRO [10] study. It is only a comparison: the expected loss remains that of paragraph 9.

## 11 bis. Parabolic equation (from 1.3.1)

The Parabolic Equation Line is a fourth calculation, only of comparison, made as professional programs for the ducts (PETOOL; the wave-propagation framework of M. Lytaev, MIT license): instead of estimating the gain from the layers, it advances the wave along the route with the split-step method of Fourier within the M modified refractive profile of the GFS, taken from the forecast point closer to each distance. The GFS levels add the estimated jumps of the potential ducts (calculating A, 50 m in half layer). The soil Copernicus GLO-30 enters steps (under the ground the field is null), the soil is conductor and the beam of the antenna is a Gaussian with the estimated opening from the gain. The calculation sees the conduits, those who lose, the diffraction and the coupling of the antennae; He does not see the troposcatter, which is added apart. Next is the «profile cheque»: the same with standard atmosphere less the one with the expected profile.

From 1.3.2:

- the propagator is at wide angle, with the exact phase of the free space:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- the bottom on the sea and the coastal plain at zero altitude reflects with the coefficient of Fresnel in horizontal polarization, εc = εr − j·60·λ·σ (sea εr = 81 and σ = 5 S/m; country 13 and 0.006 S/m), at the clear angle of the route:

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- the roughness of the sea reduces the consistent reflection according to Miller and Brown, with the effective height of the waves from the wind to 10 m (σh = 0,0051·U2):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- on the sea the profile M includes the evaporation duct (paragraph 9 ter) with the neutral logarithmic profile:

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

It tests with the prototype (10 m antenna on the sea, gain on the standard atmosphere at 100–300 km): with δ = 10–30 m the evaporation duct gives 1–3 dB to 144 MHz, 8–37 dB to 432 MHz and 21–66 dB to 1296 MHz. At 144 MHz in horizontal polarization the sea reflects almost as a metal (|η| more than 0,99 to a few tenths of degree) and roughness counts little: the new bottom changes a few tenths of dB; it counts more from 432 MHz up.

Limits: above the earth the vertical profile remains that of the GFS levels, so the thin ducts remain invisible; the ground to steps is approximated; the beam remains within 6th.

## 12. antennas, signal and noise

- Yagi DL6WU: gain 9.5 + 7.8·log10(boom/λ) dBd (2.35 dB for double boom, DL6WU measures on VHF Communications 3/1982), plus 2.15 for [12] dBi.
- EIRP = power − cable + TX gain.
- Signal received = EIRP − expected loss + gain RX − cable RX. S-meter IARU: S9 = −93 dBm, 6 dB per unit.
- External noise according to ITU-R P.372 [13]: galactic (Fa = 52 − 23 log f) more artificial according to the environment (city, residential, rural, quiet rural) or a handwritten antenna temperature.
- Tsys = Ta + Tpre + [290(Lc − 1) + 290(Fr − 1)·Lc] / Gpre to the terminals of the antenna (Friis formula). From 1.3.5 the receiver station always has a preamplifier in antenna, fixed parameter: noise number 0.8 dB (Tpre = 59 K) and gain 20 dB. Behind there are the cable (loss Lc) and the radio, with basic noise number Fr 6 dB. With 1 dB of cable the cable and radio input drops from about 1160 K to 12 K: the noise decides the sky, the environment and the preamp. The gain of 20 dB is a typical value chosen by the program.
- S/N in SSB, CW and FT8 in the band of each way, compared to the threshold set.
- Probability for period (from 1.3.2): the evanescence within the hour is a normal distribution of the S/N around the expected average hourly value. Bray (QST, November 1961, p. 39, on the curves of Norton 1959) gives an amplitude of 13,5 dB over about 100 miles, i.e. about 7 dB below and 7 above the average, which falls almost straight below 100 miles (3,5 dB to 50 miles); for a 'solid' link takes 7 dB from the budget. Taken ±7 dB as ±2σ: σ = 3,4 dB over 160 km, proportional to the distance below. The probability that a period exceeds the threshold of the way is

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  with Φ normal distribution function: margin 0 dB = 50%, +3,4 dB = 84%, +7 dB (the 'solid' margin of Bray) = 98%. It does not include the uncertainty of the model, which is larger.
- Digital Ways: S/N in 2500 Hz compared to the thresholds measured at 50% decoding, without prior information (AP). FT8 −21 dB and FT4 −17,5 dB on white noise; when the mechanism is troposcatter use the thresholds with evanescence "disturbed latitude medium", FT8 −18,6 dB and FT4 −15,2 dB [21]. Q65 -15C −22,2 dB, Q65 -30B −24,8 dB, Q65 -60A −27,6 dB In aircraft scatter the Doppler can exceed the tones of Q65 -60A (1,7 Hz): there are better 30B or 15C. JT65A −25 dB [22]. The decoding AP (name of the already known or end of QSO) gives about 2 dB extra for FT8 and 1.5 to 2.6 dB for Q65; it does not enter the threshold because it is not valid for all The «−24 dB» values that are often read for FT8 are the limit of WSJT-X relationships or AP decoding, not the threshold at 50%. [24]

## 13. Response and data times

Each calculation measures its own time: GFS profiles (network, analysis, mandatory breaks of 20 s between the batches for the Open-Meteo limit), Copernicus GLO-30 terrain (with the download of the panes, 15–30 MB each, only the first time), calculation. The Response Time line summarizes them; in the register the rows begin with TEMPI. When upgrading the grid, the register also reports the GFS race used, when it became available and how old the data are.

From 1.3.2 you can also choose ICON-2I, the model of the Agency ItaliaMeteo and Arpae on Italy at about 2.2 km, through Open-Meteo (model italia_meteo_arpae_icon_2i, two races per day, 3 days). It covers only Italy and the nearby seas and has less low levels of the GFS (1000, 925, 850, 700, 500 hPa): for the conduits at altitude is not better than the GFS, but the fine grid describes well coasts, islands and sea at 2 m, that is the evaporation duct. The variables that a model does not have (strate limit, temperature of the surface...) remove themselves from the request. From 1.3.2 the automatic source only uses NOMADS; Open-Meteo serves if NOMADS does not respond and for historical cases. With the "Update on its own" box (lighted by default) the forecast will be downloaded without clicking: at the opening, at each new GFS race on NOMADS (about 5 hours after the time of the race) and when the visible area is out for more than a quarter from the grid; at most an attempt every 15 minutes, never with a historical case.

Requests at Open-Meteo have limits per minute, per hour and per day for each internet address. The downloaded profiles are saved on the disk (Dati/cache-gfs) for each stroke and model: with the same race, which changes every 6 hours, the points already downloaded do not ask again. With the automatic source you start from NOMADS and Open-Meteo only serve for missing points; with the only Open-Meteo, at the time limit or daily stops immediately and says when to try again. Historical cases are not cached.

## 14. Validate the model

The forecast should be compared with real connections. The calculation log keeps each number; the program archive keeps ads and decodings. For 432 and 1296 MHz, where the spots are few, they need reports from the operators: who made the connection, to what time, with what signal. Compare for many hours the expected gain with the measured signal says if the thresholds are to be moved.

## Sources

1. NOAA/NCEP, Global Forecast System (GFS), global model at 0.25°, four races per day.
2. Open-Meteo, prediction API (open-meteo.com), gfs_seamless model, variables at pressure levels and racing metadata.
3. Copernicus DEM GLO-30 (ESA; WorldDEM © DLR e.V. 2010–2014 e © Airbus Defence and Space GmbH 2014–2018, supplied as part of Copernicus by the European Union and ESA), 1 second arc, GeoTIFF frames on the open register of Amazon Web Services. Reserve: NASA/USGS Copernicus GLO-30 in Terrain Tiles "skadi" of AWS.
4. ITU-R P.453, The radio refractive index: its formula and refractivity date, with digital channel maps (Study Group 3, © ITU, personal and non-commercial use).
5. B. R. Bean, E. J. Dutton, Radio Meteorology, NBS Monograph 92, 1966: Table 6.5 and § 4.4.
6. ITU-R P.452-16, Prediction procedures for the evaluation of interference between stations on the surface of the Earth at frequencies above about 0.1 GHz: § 4.2 (diffraction), eq. 45 (troposcatter), § 4.5 and eq. 49 (conducted).
7. ITU-R P.834, Effects of tropospheric refraction on radiowave propagation.
8. ITU-R P.525, Calculation of free-space attenuation.
9. ITU-R P.526, Propagation by diffraction: eq. 31 (cutcheon) and § 3 (spherical land).
10. B. Indermuehle, H. Suzuki, Tropospheric Ducting Prediction based on GFS Model Data at Inyarrimanha Ilgari Bundara, the CSIRO Murchison Radio-astronomy Observatory, Publications of the Astronomical Society of Australia, 2026 (arXiv 2608.12861).
11. D. E. Kerr, Propagation of Short Radio Waves, MIT Radiation Laboratory Series vol. 13, 1951: eq. 17 e 18.
12. G. Hoch DL6WU, «Extremely Long Yagi Antennas», VHF Communications 3/1982, pp. 130 et seq., fig. 1 (measured 432 and 1296 MHz according to the length of the boom).
13. ITU-R P.372, Radio noise: galactic and artificial noise for the environment.
14. NOAA/NCEP, NOMADS, GRIB filter of the GFS 0.25° time (nomads.ncep.noa.gov); WMO G RIB2 format, complex packing with spatial differences.
15. Open-Meteo, Historical Forecast API (model forecast archive since about 2021).
16. LukiBox, TropoLink, design of troposcatter routes with P.617, NBS TN101 and ITM flanked (GitHub, Apache 2.0 license).
17. ITU-R P.617-5, Propagation prediction techniques and data required for the design of trans-horizon radio-relay systems, 2019.
18. RSGB, VHF/UHF Handbook; DL2FZN implementation (andi-f/troposcatter on GitHub, 2013).
19. W. F. Moler, D. B. Holden, «Tropospheric scatter propagation and atmospheric circulations», Journal of Research of the NBS 64D, 1960, doi 10.6028/jres.064d.014: signal beyond the horizon (1365 and 9380 MHz, on the sea) and vertical motion of the air.
20. ARRL Handbook 2022 (99a ed.), § 19.4.3–19.4.4: weather and troposferical propagation.
21. S. Franke, B. Somerville, J. Taylor, The FT4 and FT8 Communication Protocols, QEX July/August 2020.
22. W3SZ, thresholds of slow digital modes ( JT65, JT9, QRA64) and choice of mode according to propagation, 2019.
23. QST, "The World Above 50 MHz", November 2022: opening of 14–15 August 2022 between Manitoba and Kansas/Missouri.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Quick-Start Guide to Q65, 3 April 2021 (wsjt.sourceforge.io).
25. P. L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis, Transmission Loss Predictions for Tropospheric Communication Circuits, NBS Technical Note 101, magazine 1967: § 9 (troposcatter, function F(θd), H0), § 10 and Annex III (radio climates and variability over time).
26. Open-Meteo, Historical Weather API (ECMWF ERA5 review since 1940; Copernicus Climate Change Service, CC BY 4.0 license).
