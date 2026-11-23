# Sources and formulas

> Machine translation from Italian: if in doubt, the Italian guide is the reference.

Working collection of sources consulted for the LIVETROPOVHF tropo forecast (20–21 November 2026): Basic physics, available data, published indices, formulas, radio amateur experiences, validation and decisions. The formulas are numbered as in LaTeX, (1), (2),..., and in the text they recall with the number in brackets. The Forecast Manual describes how the program uses them.


## 1. Author's premises

IC8TEM observations that frame the work, with feedback found in sources.

- Italian geography calls for diffraction. With ridges, promontory and islands a few dozen kilometers from the stations, diffraction contributions should be calculated with the Fresnel parameter on the main edge, not only with the smooth land. Meeting: in a comparison on irregular terrain at 563 MHz (Rio de Janeiro, 27 points between 5 and 30 km), the best methods were P.526 with blade edges, P.1812 and the diffractive wedge, with average error between −5 and +3 dB and dispersion of about 9 dB; P.1546 was the most pessimist (from Costa, Rabanal Ramirez and For steep slopes near the PETOOL station remember that they serve wide angle propagators and the two-way calculation (wave reflected by the reliefs).
- To get into a distant conduit, you need Fresnel. If the conduit begins beyond the horizon, the signal comes to us by diffraction over obstacles until its beginning; from 1.3.2 the program calculates it (Manual of the forecasts, § 10 ter). Meeting: Rohde & Schwarz (2023) notes that stations along a high duct often fail to enter, while those close to the ends yes.
- Conditions change quickly. The theories are there, but the conditions change in minutes, and no forecasting system takes the data in real time: the GFS comes out every 6 hours, with time span and 0.25°, and a forecast can lose value within a short time. Meeting: Bray (QST, November 1961) measurement of hourly median fluctuations of approximately ±7 dB over 100 miles; the ducts move with atmospheric time (Rohde & Schwarz); our QSO register of 29/12/2021–2/1/2022 shows openings growing and falling within hours.
- The best now-casting remains the DX cluster–PSKReporter network. To know what happens now, the cluster spots, PSKReporter (and WSPR) beat any pattern: they are measures. The forecast says where to look and how long it can last; commercials say if there is. The program for this puts spot and forecast on the same map.

## 2. Purpose

The idea is a third index, calculated by LIVETROPOVHF itself starting from the vertical profiles of a weather model, to be added to Hepburn and F5LEN instead of replacing them. Today the program reads their maps already ready: an index would also say *why* there is opening (quota and thickness of the conduit, minimum band trapped) and it could occur with the spots that the program collects already.

- What can you give: probability of conduit per point and per hour, share of the base and top, thickness, minimum frequency trapped, and along a path the fraction of the route inside a conduit.
- What can not give: the thin ducts (under a few ten meters) that the global models at 0.25° do not solve, and the evaporation ducts on the sea, which serve above all from the GHz up.
- First practical objective: 144 and 432 MHz in Europe, with verification on DX cluster spots, PSKReporter and local decodings.

## 3. Basic Physics

Everything starts from refractive N calculated at each profile level: the conduit is where the modified reactivity M decreases climbing, that is where dN/dh falls below −157 N/km ([ITU-R P.453-13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)).

Refractivity (P and in hPa, T in kelvin):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Steam pressure from relative humidity H (%) and temperature t (°C), above water between −40 and +50 °C:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

Modified reactivity (h in km, or 0.157 · z with z in meters) and duct criterion:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN/dh (N/km) | Regulation | Effect of 144 MHz |
| 0 | sub-refraction | reduced flow rate |
| from 0 to −79 | normal (standard about −40) | radio horizon with equivalent radius 4/3 |
| from −79 to −157 | super-refraction | elongated flow, "too good" |
| below −15 | conduit (trapping) | trapped signals, DX over 1000 km |

How to extract N from a model at pressure levels: for each level you take T, relative humidity and geopotential height, you calculate N and M, then the gradient between neighboring levels. The levels below the ground should be discarded (at 1000 hPa on the Alps is underground).

Minimum trapped frequency: the conduit guides only the waves fairly short compared to its thickness. Hepburn publishes a table thickness → minimum useful frequency ([DX Info Centre](https://www.dxinfocentre.com/propagation/luf.htm)); it is a soft limit, under which the signal gradually drops.

| Led Thickness | Minimum payable frequency |
|  61 m  | 962 MHz |
|  91 m  | 464 MHz |
|  122 m  | 276 MHz |
|  152 m  | 185 MHz |
|  183 m  | 133 MHz |
|  213 m  | 101 MHz |
|  305 m  | 53 MHz |

In the VHF range (often 120–300 m) the table values follow quite well a law about constant; below 100 m the formula underestimates the frequency up to 30–40%. Adapting it myself (not a formula published by Hepburn), with f in MHz and d in meters:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

In practice: for the 2 m it takes a conduit often at least 170–180 m, for the 70 cm it is enough about half. The formula circulating on the net, λmax = 0.014 · d^1.5, comes from Kerr (1951, Chapter 1, eq. 18) and is valid with λ in centimeters and standing, for a conduit in which M falls linearly: with these units gives for about 2 m 180 m and for about 70 cm 89 m, that is the same values of the Hepburn table, which comes from it. Kerr warns that it is not a net cut: even waves several times longer suffer a lot of the conduit, and a way that "loss" a little can improve reception even with stations out of the conduit. A 2019 IEEE Access study confirms that classical methods (Kerr, Kukushkin) do not describe the real cut ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)): the threshold should be used as an indication, not as a wall.

## 4. Data available for free

The weak point is vertical resolution: between 1000 and 850 hPa models give a level every 25–50 hPa, that is every 200–450 m, so they see well the inversions to 500–2000 m but not the thin ducts near the ground. LIVETROPOVHF already uses Open-Meteo to isobare, temperature to 850 hPa and inversion: the index can start from the same calls.

| Source | What gives | Resolution | Use for index |
| [Open-Meteo, GFS](https://open-meteo.com/en/docs/gfs-api) | T, relative humidity, dew point, geopotential height by level (1000, 975, 950, 925, 900, 850 hPa...); height of the limit layer | 0.25° on pressure levels; 16 days, hours up to 120 h, updated every 6 h | profile N and M, gradient, base and top of the duct; already in the programme |
| [Open-Meteo, ECMWF](https://open-meteo.com/en/docs/ecmwf-api) | same levels from open-data models 0.25°; ground gives dew point |  0,25°  | second opinion, useful for the concordance between models |
| [Open-Meteo, DWD ICON](https://open-meteo.com/en/docs/dwd-api) | T and relative humidity on pressure levels; in ICON- D2 there are 800, 900 and 925 hPa | ICON-EU about 7 km | third opinion on Europe |
| [Radiosunds, Univ. Wyoming](https://github.com/NCAR/RAOBget) | measured profiles, two launches per day (00 and 12 UTC), text format per station | many significant levels, even every few dozen meters | index verification; see the thin ducts that the model does not see |
| [GROANS, NOAA NCEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive) | historical archive of broadcasting, a file per station, more derived parameters | as above | calibration on years of data and comparison with the spots of the past |
| ERA5 (Copernicus) | Oral reanalysis since 1940, 37 levels of pressure | about 28 km | statistics and calibration, not forecast |

- With Open-Meteo geopotential height is above sea level: the levels that fall under the ground must be discarded ([Open-Meteo, upper air](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)).
- On the ground are added T and humidity at 2 m: they give the first stretch of gradient, the one where the night surface ducts are born on the plain and on the sea.
- Italian broadcasts on the University of Wyoming archive (weather.uwyo.edu/upperair/sounding.html): stations and numbers WMO in the table below.

| Station | W | Latitude | Longitude | Share |
| Udine/Campoformido |  16044  |  46,03  |  13,18  |  94 m  |
| Milan/Linate |  16080  |  45,43  |  9,28  |  103 m  |
| Practice of Sea |  16245  |  41,65  |  12,43  |  12 m  |
| Brindisi |  16320  |  40,65  |  17,95  |  10 m  |
| Trapani/Birgi |  16429  |  37,92  |  12,50  | about 14 m |

The text format of Wyoming gives for each level pressure, height, temperature, dew point and humidity: enough for N and M. Wyoming does not have an official interface, scripts are used as [RAOBget of NCAR](https://github.com/NCAR/RAOBget); the College of DuPage keeps copies in text by station ([e.g., 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)).](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. Indexes and published methods

Hepburn and F5LEN start from the same physical ingredients but do not publish the complete algorithm; the scientific studies of 2008–2026 instead describe the method step by step, and it is from there that it is worth copying the recipe.

| Method | Starting data | What is calculated | Useful notes for us |
| [Hepburn Tropo Index](https://www.dxinfocentre.com/propagation/hti.htm) | raw weather models of the Canada Meteorological Service (MSC, as written under its maps; not GFS/NAM), maps designed with GrADS, proprietary algorithm ([storia](https://www.dxinfocentre.com/propagation/history.htm)) | 0–10 linear scale of signal curvature: 0 normal, 1–2 super-refraction, 3–4 some ducts, 5–6 diffused ducts; negative values = sub-refraction | the scale to be imitated, so the third index immediately compares with the first two |
| [F5LEN, world forecast](https://tropo.f5len.org/WW/info.php) | for Europe the ARPEGE model of Météo-France, 7.5 km, more levels between 2 m and 1500 m ([note by author](https://tropo.f5len.org/author/f5len/)) | Average index for landlord, 40 steps from 3 hours, 4 calculations per day, 28-day archive ([new version](https://tropo.f5len.org/?p=504)) | confirm that it serves a model with many low levels |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf) | 60 ECMWF model levels, from ground to 3000 m | minimum of dN/dz per column: conducted under −157 N/km, super-refraction under −78.7 N/km; base of the trap layer | climatology: Padana Plain the European area with more pipelines all year round; Mediterranean in summer near 100%, in winter under 15%; Italy over 30% on summer nights |
| [Indermuehle and Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861) | GFS 0.25°, 21 pressure levels + fields at 2 m | M = N + 0.157 z; minimum dM/dz below 3 km, base and top of the duct, depth of the M deficit; under 40 m discarded | verified for 7 years on 25 channels from 88 MHz to 2,68 GHz; good capacity up to about 3 days in advance (AUC approximately 0,74), then drops; GFS better than ERA5 |
| [Hämäläinen et al., IMF 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf) | Harmonie-AROME, 750 m or 2.5 km, 65 or 90 levels | dM/dh < 0 on the model profile, comparison with a 4 to 59 m beam | counts more vertical than horizontal resolution; no model sees the ducts under 15 m; moisture dominates on the temperature gradient |

Third amateur radio service already active: [TROPOCAST.EU](https://tropocast.eu/)of Artur SQ3A, for 2 m and 70 cm in Europe. From the forecast page ([tropocast.eu/forecast](https://tropocast.eu/forecast)):

- Data: DWD ICON-EU (pass 0.0625°, about 7 km), default source; alternatively Canadian GDPS (0.15°), less detailed.
- Method: N and M according to ITU-R P.453 from the profile between the layer near the ground and 850 hPa; the recognition of the layers, the union of ducts to the ground and high and the thresholds of the colors are a proper calibration of the author, not published.
- Products: "complete" view 0–3000 m, more separate views "to the ground" and "high" (for now only in the maps to be incorporated); score 0–100 in nine classes (under 38 background, 38–52 marginal, 53–58 weak, 59–65 moderate, 66–72 good, 73–80 very good, 81–88 high, 89–95 strong, 96–100 very strong); forecast up to +72 h within an hour, with animation and archive.
- Areas: Europe, Poland, United Kingdom, Germany, France, Spain, Italy, Czech Republic, Croatia; interface also in Italian.
- Reuse: maps incorporate with an API keyless iframe; there is also a documented API (OpenAPI and JSON schema). The license is not indicated: before using it as a third source next to Hepburn and F5LEN you should ask the author.

Differences from our project: TROPOCAST stops at 850 hPa (about 1.5 km), while the elevated ducts arrive at 2000 m and beyond ( LA0BY case of 22/11/2018), and does not give an index for band or route. Classes 0–100 can be compared to our index 0–10 on the same days.

TROPOCAST image structure (from the link provided by the author of the program): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, where n is the forecasting step. The site prohibits automatic withdrawal to robots, so I could not read the image; also for this, before showing these maps in LIVETROPOVHF serves the author's permission.

Notebook: [DJ5AR, category](https://dj5ar.de/?cat=174)(Magonza, 21 articles 2017–2023, 2 m–13 cm). Cases described: conducted several weeks from Ireland (23 May–2 June 2023), FT8 to Belarus (September 2020), Italy–Germany over the Alps and Adriatic (August 2017). Recognize the case of the "too high" duct (reverse to 1000 m) that you cannot use from the ground: another confirmation that it is necessary to compare the share of the conduit with that of the stations. It uses radiophares as an aperture measure (up to 1028 km) and vertical profiles in Paris, Bordeaux and DWD.

In summary, the common recipe: vertical profile → N and M per level → layer with dM/dh negative → base, top, thickness and force of the duct → index. The "secret" part of Hepburn is the empirical calibration on real events, which we can remake with spots.

## 6. Useful Formulas for the program

In addition to N and M (basic Physics section), these are used to move from vertical to point number and then to path. ITU formulas had been reconstructed by a PDF read in part badly; on 22 November were reviewed on official texts ([ITU-R P.452-18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453-14, P.526-16, P.372-18) and corrected in the code: constant of the troposcatter 190 (not 190,1) plus the absorption of gases with ρ = 3 g/m3; α not below −3,4 (eq. 55a); gas of the ducts with ρ = 7.5 + 2.5·ω (eq. 9a); diffraction on full spherical land (eq. 23–37: average land-sea, height function with limit 2 + 20 logs K, interpolation under the optical flow); steam saturation pressure of P.453-14 (eq. 9, with EF) instead of Magnus' old formula. Rumore P.372-18: table 1 and galactic correspond; Artificial noise curves are up to 250 MHz and the galactic up to about 100 MHz, over the program extends the same laws.

1. Gradient between two levels of the model (h in km, M in unit M):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. Conduit strength and thickness: thickness d = top − base (for a soil duct, base = soil); strength as M deficiency between base and top.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3. Earth radius equivalent factor (P.452-18, eq. 5 and 6a), with ΔN = average N drop in the first km, positive, in N/km:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

With ΔN = 40 you have about 4/3 k (standard atmosphere); with ΔN that approaches 157 k tends to infinity, that is, the Earth radio becomes flat: it is the beginning of the duct.

4. Radio horizon of a high antenna h meters, with radius equivalent to\_e in km:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5. Probability of duct for latitude (P.452-18, eq. 2), percentage of time; φ = latitude of the center of the route, μ1 and μ4 take into account how far it is by sea:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

At 41° N, all by sea (μ1 = μ4 = 1), gives about 11% of the time: is the climatological base, useful as a reference value from which the expected index differs.

6. Loss for troposcatter (P.452-18, eq. 45; f in MHz, d in km, θ angle of the path in mrad, N0 ground reactivity at the center of the route):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

The term −0,15 · N0 shows why wet air also helps without duct: 50 extra N units are 7.5 dB less than loss. LIVETROPOVHF already uses the G3YGF version of the scatter loss.

7. Loss in duct (P.452-18, eq. 46–51): fixed loss of coupling plus a attenuation for angle milliradiant, which grows with the cubic root of frequency (f in GHz, a\_e in km):

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. From Bean and Dutton (1966) and Kerr (1951)

The two classic texts give three things that are needed immediately to the index: gradient thresholds already tarate on the same levels of pressure of GFS, what is worth in dB a change of reactivity, and what weather situations create or destroy the ducts. *Radio Meteorology*, NBS Monograph 92.

1. Profile classification for pressure layers (Bean and Dutton, tab. 6.5, p. 248; −dN/dh in N/km). Designed for radio wavelengths, it applies as it is at model levels:

| Road | Shares about | Sub-refraction | Not laminated | Super-refraction |
| 1000–850 hPa | 0–1,46 km | −dN/dh < 0 | from 20 to 60 | over 100 |
| 850–700 hPa | 1,46–3,01 km | −dN/dh < 0 | from 20 to 50 | over 80 |
| 700–600 hPa | 3,01–4,20 km | −dN/dh < 0 | from 15 to 40 | over 70 |
| 600–500 | 4,20–5.57 km | −dN/dh < 0 | from 10 to 30 | over 50 |

2. How much is worth in dB (Bean and Dutton, cap. 6, pp. 233–258), measures from 70 to 180 MHz on routes beyond the horizon:

- Any layer (on the ground or at the altitude) compared to the "smooth" profile raises the signal of 10–25 dB to all percentages; the greater gain comes from the high tilted layers, probably due to focusing effect (course of 200 km).
- The ground reactivity Ns and the decline in the first km ΔN are related to the monthly median signal with coefficient between 0.4 and 0,95, median 0,7 (20 routes from 130 to 446 km, about 100 MHz).
- Annual cycle: 0.18 dB per unit N; daily cycle from 0.2 to 1.1 dB per unit N, maximum on routes of 175–200 km and winter.
- The compound parameter of Misme estimates the field variation as 0.5 dB/(N/km) · (equivalent gradient − 40) plus a thermal stability term; the parameter of turbulence D (area between the real profile and the adiabatic one, 0–3 km) just improves compared to the simple ΔN. Conclusion of the authors: ΔN remains the best parameter for simplicity.

3. Reference profile and anomaly (Bean and Dutton, § 3.8 and 3.9). The exponential atmosphere of reference CRPL gives the "normal" profile starting from Ns; the A unit measures how much the real profile differs at each share. It is the natural way to transform a GFS profile into an anomaly to put on the map:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

With Ns = 313 you have ΔN about −42 N/km, that is the equivalent radius 4/3. The −157 N/km conduit criterion is also obtained here by the Snell law for a horizontal radius (eq. 4.23), almost independent from the antenna height.

4. Where and when the conduits are born (Bean and Dutton § 4.4; Kerr cap. 3):

| Process | Situation | Effect |
| Warning | hot and dry air from land flowing on colder sea (typical of summer sleeve after days of good weather); mainly counts the humidity deficit | intense conduit on the sea |
| Nocturnal irradiation | serene sky, weak wind; low clouds or much humidity prevent it | led to the ground that is born at sunset, grows in the evening and disappears quickly after sunrise, especially inside |
| Subsidence | air that descends from the anticyclone, heated and dry, over fresh and humid air; frequent flooding in the mountains and south-eastern sectors of anticyclones | super-refractive layer or high duct; almost always gradient higher than standard in reverse |
| Sea and land | coast, circulation of a few hundred meters | local pipelines along the coast |
| Against the conduit | mixing: ground heated by the sun between mid morning and afternoon, wind to the ground over a few m/s on rough terrain; Night fog | probability of conduit almost nothing |

5. Two new ingredients to try. The vertical motion: ascending currents in the convergence zones remove and erase the layers, the subsidence strengthens the signal (Moler and Holden: on average about 2.5 dB more than with uphill air, on the sea in band L and X). Open-Meteo already provides vertical speed on pressure levels. And the pressure trend: on a 140 km course at 186 MHz the signal went down when the pressure dropped to the transmitter and to the center of the path and returned high and without evanescence when it went up, with just 2 (Bean and Dutton, fig. 6.9, p. 241).

6. From Kerr. In addition to the formula of the maximum wavelength (base Physics section), Kerr gives an approximate formula for M from the only temperature, steam and altitude (§ 3.9, with the old 79 and 4800 constants then replaced by 77,6 and 4810) and observes that to a reverse the wind cut can generate waves that swing at the altitude the duct, possible cause of deep escapes (§ 3.26). Italian source reported by Bean and Dutton and to search: L. Bonavoglia (December 1958), "Correlation between meteorological phenomena and propagation..."

## 8. From amateur radio presentations and technical reports (second series)

Seven documents provided by the author of the program. Three serve directly to the index: W3SZ for the troposcatter "floor", LA0BY for cases that Hepburn had not predicted, Hepburn (translation LU7MC) for the weather classification of the ducts.

1. Troposcatter: the signal that always exists. R. Rehr W3SZ, "Tropospheric Scatter (Troposcatter) Propagation for VHF, UHF, Microwave and Frequencies", Pack Rats Conference 2014; J. R. Duffey KK6MC, "The Troposcatter Propagation Mode and How to Predict Coverage on the VHF/UHF Bands", CSVHF 2017.

- Useful from about 100 to 700 km, from 144 MHz to 10 GHz; the loss grows of 30 logs f with frequency.
- Above all, it counts the starting angle towards the horizon: each extra degree costs about 9–12 dB ( M1CNK, Collins method).
- In summer the signal is on average about 10 dB higher than in winter; in mid afternoon about 5 dB lower than in the early morning or in the evening. KK6MC indicates a total variability of 13–15 dB with time, season and time.
- Yeh method, the easiest to program (f in MHz, d in miles, θ angle of scatter in degrees, α geometric average of the openings of the two bundles):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Comparison of Larsen (1968) on 15 real connections: average square error 5,1 dB for Collins, 7,4 for Yeh, 8.1 for NBS and CCIR, 16,8 for Rider. All methods improve if Ns keeps steady (Yeh drops to 5.4 dB): for the scatter the refractive of the day counts little, so the tropo index must look at the layers, not only Ns. The table of 15 connections, with measured losses (including Feldberg–Monte Baldo, 322 km at 957 MHz, 223,1 dB; Cyprus–Israel, 319 km at 77,5 MHz, 163,3 dB), is a good test bench for our calculation.
- KK6MC gives the loss that a typical station manages to exceed 2 m and the corresponding distance, from 184 dB (FT-817 and 3 elements in SSB, about 130 km) to 215 dB (long bungs in SSB, about 430 km) and 236 dB (kW and yagi coupled, about 640 km). It's the way to translate the index dBs into miles earned.
- Ready code: W3SZ Yeh and Collins program (nitehawk.com/w3sz/YehCollins.zip) and Bob Atkins KA1GT calculator (bobatkins.com/radio/troposcatter.html).

Another source on the scatter: I. Roberts ZS6BTE, "Tropospheric Scatter Propagation", *Radio ZS*, December 1984, with note of 2013 ([qsl.net/zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). Mainly Useful geometry: angle of scatter θ = θ0 − θ1 − θ2, with θ0 = d/R, θ1 = (h1 − h\_ostacolo)/d1 + d1/2R and the same for the other station, R = 8446 km (4/3 of the Earth radius). It is the piece that is used to calculate the angles from the profile of the ground in the troposcatter "floor". Other data: each angle in more costs about 10 dB; passing from Ns 280 to 300 reduces the loss of about 4 dB; the scatter volume is between 300 and 2000 m for 150 km, between 600 and 3000 m for 300 km; Time in the scatter volume raises the signal of about 20 dB. the numerical example of the article does not return with the given formulas (the term of free space at 900 MHz and 872 km is about 150 dB, not 125), so the formulas should be checked on W3SZ before using them.

2. Types of conduit (W. R. Hepburn, "Tropospheric Propagation Modes", dxinfocentre.com/propagation/tr-modes.htm, Spanish translation of LU7MC). Complete the table of Bean and Dutton with useful cases in Italy:

| Type | When | Note for index |
| Irradiation | calm and peaceful nights on earth | follow the ground, disappears shortly after dawn |
| High pressure (subsidence) | anticyclone | can last all day; at night the conduit from irradiation below can block distant signals, so daytime can go better |
| Front | in front of a warm front, behind a cold front, north of a stationary front | short; the bad weather disturbs him |
| Mountain descent | föhn, bora: air coming down, warming and drying | to try for bora on the Adriatic and föhn south of the Alps |
| Warning | hot and dry air over cold and wet soil, for example after rain | led to the ground |
| Valley | hot air over cold air stops in the valley | can stay in the day |
| Marino | hot and dry air on colder water; arrives on the coast with the breeze | peak in the afternoon; spring and early summer the best seasons |

The evaporation ducts on the sea (0–30 m) serve only about 3 GHz and do not enter the hinterland: for 144 and 432 MHz you can ignore. The high mountains are a barrier; The deserts are too dry.

3. The cases Hepburn had not predicted (S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", Nordic VUSHF 2019, from JO59IX to 500 m of altitude):

- 22 November 2018: 70 cm up to UA6 (about 2600 km) for 6–8 hours. Hepburn indicated only the Baltic States and Poland. The conduit was seen in the temperature maps at 900 and 1500 m and ended just above 2000 m; it was higher on the Norwegian side, and only LA0BY on the hill entered. On the ground time did not seem like a trope: cold and fog at the top.
- 11 February 2015: 2 m and 70 cm towards 9A and S5 (about 1500 km) through the Alps, also this not foreseen by Hepburn.
- 15 October 2011: 2 m with EA1 (2135 km) in high duct, on three stretches of sea and three ground.
- Other events: 6–11 November 2003 to the east (2050 km), 10 December 2004 to France.
- Field addresses: fog in plain, free peaks, little wind, wet ground. Normal temperature gradient −6,5 °C/km; in a real case ( VK3KAQ) +7 °C in 20 m.

Author's conclusion: Hepburn and F5LEN are not enough, they serve the profiles at altitude along the route. For us: these dates are cases of verification to be recalculated with the historical data of the models, and the index on the path must compare the quota of the duct with the quota of the stations.

4. Italian experience (G. IV3GBO, "Types of VHF propagation SSB/CW", 2017): conducted between 450 and 3000 km, with layers between 450 and 3000 m; fog extended as a clue; openings by sea on the Adriatic towards Puglia, Albania and Greece. Scatter from 100 to 500 km.

5. Introductions (P. Wilton M1CNK, "An Introduction to VHF/UHF Propagation", 2013): 70 cm are often better than 2 m; with 10 m pole at 144 MHz the starting angle is about 3°, with 20 m approximately 1.5°.

6. NASA report (E. Hirschmann, "Tropospheric and Ionospheric Effects upon Radio Frequency (VHF-SHF) Communication", Goddard X-731-67-89, 1967): it concerns distance errors to satellites, not the DX. It brings back the same N formula and exponential profile (example in Florida: Ns 370, ΔN −55, constant 0.161/km) and sends two NBS sources to search: Bean and Thayer, "CRPL Exponential Reference Atmosphere", Monograph 4 (1959), and Bean, Horn and Ozanich, "Climatic Charts and Data of Radio Refractive Index for the United States and the World", Monograph 22 (1960).

## 9. From The VHF/UHF DX Book, Chapter 2

I. White G3SEK (edited by), *The VHF/UHF DX Book*, DIR Publishing 1995 (ISBN 0 9520468 0 6; digital replica 2017), chapter 2 "VHF/UHF Propagation", pp. 2-13, 2-22–2-29 and 4-11. It is the closest text to the radio amateur experience among those collected; confirms the thresholds already used and adds new synoptic rules.

Number

- The same formula of N (77.6 P/T + 3.73·10^5 e/T2); example: 1000 hPa, 7 °C, and = 10 hPa damage N = 324. In a well-shaped atmosphere, for example after a thunderstorm, N is almost linear of about 39 units/km in the first 2–3 km.
- Gradients between 0 and −79 N/km are normal, below −79 there is super-refraction, over 0 sub-refraction. The book insists that the long DX do not come from the simple super-refraction, which should remain right on the whole path, but from the ducts.
- Minimum duct thickness, a little more prudent than Kerr and Hepburn:

| Band | Minimum thickness |
| 50 MHz |  400 m  |
| 70 MHz |  300 m  |
| 144 MHz |  200 m  |
| 432 MHz |  100 m  |
| 1.3 GHz |  50 m  |

- The ratio is not proportional to the wavelength (about 65 λ at 50 MHz, 220 λ at 1.3 GHz) because the edges of the conduit are "displaced". In the UK the pipelines are frequent on 144 MHz but almost never at 70 MHz or below, and often the 432 MHz go strong when the 144 are weak.
- In pipeline the signal drops as 1/d instead of 1/d2: up to 1000 km the attenuation can be similar to that of free space.
- Capacity of a minimum station on 2 m (100 W, 15 dBi yagi, 2 dB noise): 217 dB, that is about 400–500 km in SSB in average conditions; in CW about 20 dB in more (p. 4-7). Agree with the KK6MC table.
- Scatter: the loss rises by about 15 dB by triple the frequency and 10 dB for each degree of scatter angle ( Yeh formula; in the text extracted the constant is unreadable, so it must be taken from the version of W3SZ). The "up" scatter from convective cells covers routinely up to about 500 km, 1000 km with good stations. After heavy rains and extended conditions are scarce on all bands.

Types of duct

- On the ground: on the earth they propagate badly because the soil absorbs and spreads; on the calm sea arrive far (Scilly–Canarie, 2500 km, about once a year on 2 m). The evaporation ducts are often only about 15 m tall and serve microwaves.
- High: between 450 and 2000 m, due to the longer DX on the ground; the signal "high" above the stations below. The "lose" ducts give less strong signals but also allow intermediate distance stations.
- The conduits have precise directions and can last hours or days, contrary to the sporadic E.

New Synoptic Rules for Index

- Age of anticyclone: the air drops to the center of a few meters per hour and the reverse is lowered as the high pressure develops; the greater openings arrive after several days of propagation already good. A short-term anticyclone is not enough.
- High pressure edge: the layer is dome and at the edges is lower, so the stations get better.
- Direction: It has been observed that the longer routes run along the isobares or through the dorsals (Flavell G3LTP, 1984), but there are excellent connections also in radial direction from the center. Example of 29–30 August 1987: from England the opening rotated 180° in two days (Spain, France, Italy, Austria, to Norway) following the shift of the anti-cyclone.
- Against: thick isobare (strong wind), stratocumuliform clouds at the edge of the layer (limit the highest frequency).
- Fronts: a cold front pushes hot air up and creates a reverse along the front; the best direction is along the front while passing, but it lasts little.
- Season: In the UK anticyclones are more frequent at the end of summer and early autumn.
- Visual clues: smoke trapped in a dark line above the horizon; with high pressure, layercumules with base below 1000 m indicate a ground duct, base above 1000 m a high duct.

References quoted to search: R. G. Flavell G3LTP, "Studies of an Extensive Anticyclonic Propagation Event...", *Radio Communication*, February 1984, p. 128; J. Gannaway G3YGF, "Tropospheric Scatter Propagation", *Radio Communication*, September 1981, p. 710; P. W. Sollom G3BGL, "A Little Flutter W6ZGN

## 10. A verified amateur radio model: Biancomano and Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, "Practical Tropospheric Model for VHF/UHF Propagation Prediction", *National Weather Digest* 22:2, June 1998, pp. 15–24, free in [PDF from the National Weather Association](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf).

- Data: radiosundations of the 00 and 12 UTC of 47 stations in the eastern United States, 1995–1997; ground analysis to 700 hPa (at the beginning up to 400), on the profile of N, without M. Excluding coasts, islands and mountains.
- For each survey: presence and quota of the duct, maximum wavelength that can lead (often about 100 times the wavelength), maximum gradient of the potential temperature between neighboring levels and between one level and the second above (for thicker layers). The ground ducts are not used.
- First rule, persistence: stability indices (lifted index, K, totals-totals, SWEAT) remain within 3 units from the survey 12 hours before, and the pressure level where the potential temperature grows more remains about the same. If so: improvement forecast in the next 12 hours.
- Second rule, dynamic controls: limit layer that does not rise, CAPE, variation of the thickness to 500 hPa on three surveys, variation of the slope within limits.
- Band threshold: a conduit capable of carrying 100 cm or more indicates opening on 432 MHz, almost certain on 222 and often even on 144.
- Space filter: stations on a hexagonal grid of about 400–500 km; the forecast is only worth if at least 5 contigue stations are positive (if you can skip one). With 5 stations the probability of a false random positive becomes negligible.
- Verification against the events of the column "The World Above 50 MHz" of QST: in 1996, 48 events planned on 67 (71%); 12 of the 15 major events. Accuracy up to 70% on the 432 MHz and about 60% on the 144 MHz at 12 hours; it provides almost double the actual events; 9 errors out of 19 are predictions arrived late, because many improvements form in less than 12 hours.
- Added to the stations further north: wind rotating clockwise climbing (hot start) as extra condition.

From the complete PDF (supplied by the author of the program) three more details. Their rule "thickness = 100 times wavelength" gives 600 m for 6 m, 200 m for 2 m, 125 m for 1,25 m and 70 m for 70 cm (fig. 3): for the 70 cm is less severe than Kerr and Hepburn (about 89 m), for the 2 m it coincides with the VHF/UHF DX Book. The wind that rotates clockwise goes up, according to the authors, especially because it brings warmer air at altitude, which is what makes the fall of N grow with pressure; the moisture that brings with it does not seem to destabilize. The appendix obtains dN/dh in closed form for a constant thermal gradient atmosphere with a sharp leap of heat gradient and humidity at a given quota: useful as a test case for our code. Their trace of the slope of the potential temperature comes from K. H. Jehn, "The use of potential refractive index in synoptic scale radio meteorology", *J. Meteorology* 17, 1960, p. 264.

What to take for our index: (1) a term of persistence: compare the profile with that of 12 hours before, that in the hourly forecast we have free; (2) the space filter: a point counts only if the improvement covers an area of a few hundred km, which reduces false alarms; (3) stability indices, calculated from temperature and dew to 850, 700 and 500 hPa (Open-Meteo already gives lifted index and CAPE); (4) the wind that rotates with the altitude between 1000 and 850 hPa; (5) their method of verification, which is the same as we want to do with the program ads.

Another source in the same package, of little use for us: P. K. Rastogi, "Stratosphere and Troposphere (S-T) Studies at Millstone Hill", NASA 1984 (document 19840019069): 440 MHz radar that measures the turbulence of clear air between 4 and 25 km, with thin layers linked to gravity waves and wind cutting. It confirms that the turbulence that produces the scatter is layered, but it concerns too high odds for the ducts.

## 11. Third round of sources (20 November)

High duct historical cases. J. Macoun OK1VR, 2 m from the summit of Sněžka (1602 m), autumn 1958, article of 1959 translated in 2006 ([ok2kkw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)). On 27–28 October 1958 first Czechoslovakia–England connection on 145 MHz ( G5YV, 1220 km) and many more between 1100 and 1250 km, with stable signals after 23. The author attributes it to two superimposed inversions, the upper about 2200 m, with the signal returning to the ground only where the lower layer dissolved; condition favoured by a slow anticyclone from the Atlantic to the continent. From home (Prague) its maximum was 530 km: only the stations at altitude entered the conduit. It is another case of verification (such as LA0BY 2018) and confirms that the index should look for conducted over 2000 m and compare it to the station share.

Historical libraries NBS (useful to look for classic works, not for formulas):

- W. Nupen, *Bibliography on Tropospheric Propagation of Radio Waves*, NBS Technical Note 304, 1965, 356 pages, with index for topics (also "condition forecast") and by country. Italian entries reported: L. Sacco, "The radio connection in atmospheric super-refraction regime", *High Frequency* 24(6), December 1955 (Italian research: super-refraction under 500 m frequent day and night); S. Silleni, *Annali di Geofisica* 8(1), 1955; J. M. Clara e A. Antinori, prova tra Sardegna e Minorca da 238 a 297 MHz (390 km), *Electrical Communication* 33(2), 1956. A. F. Clarke (Fort Huachuca, 1956) issued daily the forecast of the maximum wavelength trapped for the following morning, from humidity, temperature, pressure and wind forecasts.
- A. L. Abbott, *Bibliography of Tropospheric Radio Wave Scattering*, NBS, material until January 1960, about 670 items ordered per year: useful for the troposcatter part.

Disclosure and observations: F. W. Odenthal DK5EQ, "Ausbreitung 2m – Eine Einführung" (31/05/2024): almost optical up to 200 km, tropo per duct or refraction beyond, with high pressure and in spring and autumn; cites the VHF page of DM2HB and the notes of DK5YA on tropo and Es. A. Johnson VK3DXE on the blog of EI7GL (December 2021): with WSPR VHF paths of thousands of km in Australia have been discovered; The trope is not only summer. Discussion on VHF marina and AIS ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)): scope 1,23·√h(ft) nautical miles per antenna, scatter up to about 50 nm, typical trope improvement 100–400 nm; the conduits are recognized because the distant stations arrive as strong as the neighbors. Chelmsford ARS News, December 1983: in the contests in 2 m the stations of the English eastern coast are favoured by the sea route to the continent.

ARRL, *The Radio Amateur's VHF Manual*, 1972, cap. 2 "Propagation Beyond the Horizon" (pp. 17–19). Already known operating tips (follow high pressure areas that move slowly and above all their back edges; early autumn the best period; coastal areas, large lakes and valleys of the rivers favoured by stratification). Two new data: (1) fig. 2-4 compares the standard atmosphere ( relative humidity 70% up to 12000 ft, minimum refraction) with a typical radius of strong refraction, with a clear temperature break and mix ratio of about 4000 ft (about 1200 m): it is the signature that our calculation must recognize on the GFS levels; (2) the true ducts are rare under 144 MHz and most frequent in UHF, according to the minimum thicknesses. Useful QST bibliography to search: Hull, "Air-Mass Conditions and the Bending of UHF Waves" (June 1935, May 1937); Hoisington, "Painless Prediction of Two-Meter Openings" (October 1949); Collier, "Upper-Air Conditions for 2-Meter DX" (September 1955); Botts, "A Night to Remember" (January 1970, conducted on land).

PA0EHG, page "Tropo" ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): summary of the troposcatter from the article of G3YGF, already in the manual. Use two practical rules to control our scatter calculation: about 10 dB extra loss for each angle at the horizon of each station, and over 100 km approximately 9 dB plus every 100 km route (60 miles); the common volume starts from about 600 m on 100 km and from about 9 km on 480 km. It does not treat ducts, polls or refractive thresholds.

PA0EHG, PROPBER calculator ([page](https://pa0ehg.com/propagat1.htm), propber.exe program provided by the author of the program). It is a 36 kB DOS program written in BASIC and compiled with the Microsoft BASIC Compiler 5.60 (1985): it only runs under DOS or DOSBox. From the internal texts: asks for frequency, distance (or the two locators, also in the old QRA format "E-locator"), for each antenna height station, noise figure, FI band, power, an obstacle (height and distance) and antenna gain (0 = parable diameter calculation, efficiency 50%). Returns the maximum distance in optics, the correct optical loss for the absorption of steam (0.1 dB/km from 24 GHz), the loss of troposcatter "correct for the sites" (i.e. for the horizon angles given by obstacles) and the S/N ratio in the two senses, also "with the maximum abnormal propagation". Numerical constants are not readable in the compiled executable. The suggested method for high bands is also useful for us: calibrating the calculation on a measured link (difference between measured and calculated S/N) and applying the same difference to another band. It is the same pattern of our calculation, but it uses the S RTM1 profile instead of just one obstacle and the GFS forecast instead of a fixed margin.

PROPBER, constants read in the executable (numbers in Microsoft Binary Format format). πr, in the data table next to the texts: 8,68589 (= 20/ln 10, i.e. 20 logs calculated with the natural logarithm), 32.5 (free space: 32.5 + 20 log f\[MHz\] + 20 log d\[km\]), 24000 and 0.1 plus 40000 and 0.3 (vapore: 0.1 dB/km from 24 GHz, 0.3 dB/km from 40 GHz) Probable, because you repeat but inside the code: −69,5 (the same constant term as Yeh's side-scatter formula in VHF/UHF DX Book), 332 (soil refractive), 8733 km (equivalent radius, k ≈ 1.37). The exact scatter formula is not reconstructable without disassembling the program. T0

PROPBER, formulas reconstructed by the code (disassembly of the compiled part; the operations are called to the BASIC library with the operators in memory, whose values in double precision have been decoded). The constants −69,5, 332 and 8733 found earlier were false positives within the library.

- Optical distance: d = 5 · (√h1 + √h2) km (h in m). It is more generous than the classic 4,12 (k = 4/3): is equivalent to k ≈ 2.
- Loss in optics: L = 32.5 + 20 log f\[MHz\] + 20 log d\[km\], plus 0.1 dB/km above 24 GHz and 0.3 dB/km above 40 GHz.
- Troposcatter: Lts = L + 23 + 10 log f + 0.0674·d, i.e. 55.5 + 30 log f + 20 log d + 0.0674·d (formula of G3YGF with the constant 2 dB higher). No angle term or reactivity.
- Correction "for the sites": the program asks obstacle and distance but the block that should use them is empty; the correction variable remains 0, so the "correct" loss is equal to that of scatter.
- Noise: T = 290·(10^(NF/10) − 1), N = −228,6 + 10 log T + 10 log B (dBW), without antenna temperature.
- S/N = 10 logs P + G\_tx − L + G\_rx − N. "With maximum abnormal propagation" uses optical loss instead of scatter, i.e. the ideal conduit case without losses beyond free space.

Comparison: 300 km and 144 MHz PROPBER gives about 189,9 dB, G3YGF 187,9, our P.452 187,8 (with zero horizon angles): the scatter coincides within 2 dB. Our calculation adds the horizon angles from the profile, the antenna temperature, and instead of the "maximum" fixed the expected layer with residual mating and attenuation.

Rohde & Schwarz, P. Denisowski, "Understanding VHF Propagation", Educational Note, April 2023 (24 pages, dissemination). Confirms the points already collected (conducted as a two-dimensional waveguide, signal that drops in a linear way with the distance, up to 1500 km and beyond, duration from hours to days; good ground on the sea and scarce on land; elevated from a few hundred meters to a few km). Two useful observations for the index: (1) evolution in time: a newly formed conduit often carries only the highest frequencies and, thickening, begins to bring even the lowest; the hourly forecast should therefore show the 432 MHz "in advance" on the 144 MHz when the layer grows; (2) stations location: the best conditions are between stations close to the ends of the conduit, while those along the path of a high conduit often fail to enter, unless conducted "who lose". The ducts move with atmospheric weather.

Site PA0EHG, other pages. For the tropo, PathProfile of M. Willis G0MJW ([scheda](https://pa0ehg.com/srtmp.htm), [download](http://www.mike-willis.com/Software/ASTERPathprofile.zip)): profile of the route from expected total loss and quota files, judged reliable for the troposcatter in microwave; does not treat the scatter from planes or rain. Useful as a second reference to compare our S RTM1 profiles and losses. The rest of the site covers EME, microwave, airstrip (AirScout, SM7LCB maps) and measures in 80 m.

PathProfile by G0MJW (Mike Willis), ASTER and SRTM packages and SiteData (supplied by the author of the program; [](https://www.mike-willis.com/software.html)software page). Windows program of 2018: profile from ASTER or SRTM to 1" or 3", losses in optics, diffraction, troposcatter (valid up to 1000 km), duct and gaseous, with percentage of time, k median of the route, fraction of path on the sea and distance from the coast: the structure is that of P.452/P.1812, but the help (compressed.chm file) could not be read here. SiteData converts the locators and gives the share from GT OPO30.

The most useful part is the data files included: ITU-R P.453 statistical maps on global grid of 1,5° (121 rows from 90°N to 90°S, 241 columns from 0° to east): gradient dN/dz in the first 65 m exceeded for 1, 10, 50, 90 and 99% of the time; for soil ducts percentage of presence, thickness and intensity; for those high presence, base share, thickness, intensity and maximum altitude; plus the wet term and temperature. Interpolated annual values:

| Location | Land pipeline: % / thickness / intensity | High pipeline: % / base / thickness / intensity | dN/dz exceeded 10% of the time |
| Naples |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | −380 N/km |
| Tyrrhenian (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | −674 N/km |
| Trapani |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | −520 N/km |
| Brindisi |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | −405 N/km |
| Adriatic (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | −399 N/km |
| Milan |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | −172 N/km |
| Udine |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | −176 N/km |

For our index are a ready climatic reference: thickness and intensity typical of the ducts instead of the 50 m fixed of the potential duct, and an anomaly "today compared to normal" per zone. ITU data: free for non-commercial use, to check before including them in a public version. The ZO NES01.DEM file (58 MB, 2' grid) contains the climate zones inland/coast/sea; the format is not documented and I did not decode it. Willis' equations page contains only physical constants, free space and antenna gain.

Fourth round of sources (20 November).

Indermuehle and Suzuki 2026, prediction of the GFS pipelines for the CSIRO observatory of Murchison (arXiv 2608.12861): it is the closest source to LIVETROPOVHF. Profiles of N from the GFS to 0.25° (surface to 2 m, then the isobaric levels, those under the discarded ground); for each minimum column of dM/dz under 3 km, base and top of the conduit, integrated M deficit; then P.452-16 and P.1812-6 with GLO-90 25 routes from 98 to 1005 km, from 88 MHz to 2,68 GHz, every hour. Results: Median AUC 0,72 on 25 channels; the P.452 p time percentage should be used as a calibration constant (choice p = 20%), because the ITU model with an instant ΔN is not defined within a conduit; skill remains almost flat until 3 days in advance. At 162 MHz (AIS) the received routes have the highest conduit fraction of 67% (0.26 versus 0.16) and the highest duct depth of 49% (28 vs 19 M) with respect to controls: on the sea the conduit is almost always, decides the stretch of land. The GFS sees half the time on the ground compared to ERA5 (4.5% versus 10.4% of the cells-hours), while on the sea sees more (73% versus 56%). A "corridor" index (continuous pipelines along the route, layers with a minimum thickness of 40 m) improves the result: confirms that continuity along the route is the right criterion.

Longley and Rice, ESSA ERL 79-ITS 67 (1968): method for irregular terrain from 20 MHz to 20 GHz (after ITM), with soil irregularities Δh, Ns from 250 to 400, equivalent radius 8497 km (4/3), horizons, diffraction, scatter forward and variability in time and in places for climate. Statistical reference, not instantaneous.

ITU-R P.834-2: radius curvature, Earth radius equivalent k·a, M = N + h/a (h in m, in thousands of km), exponential model; nothing new than what already used.

AGARD CP-70 (1970), tropopheric propagation: thin layers from 1 to 100 m with strong gradients are common; the reflection coefficient of a high layer is about proportional to the wavelength, with a random dispersion of about 14 dB. Measures to 90 MHz: decrease of 9–10 dB tied to a N jump concentrated at about 350 m, at the top of a layer of clouds. The passage of a cold front eliminates the layers: The diffraction remains.

U.S. URSI Report, Commission 2 (1960): average thickness of the 60 to 210 m high layers (given consistent with P.453), minimum gradient for the 157–160 N/km duct.

Buscemi K2OVS, VHF propagation guide: typical distances for reversal and duct for band, ducts linked to high stationary pressure, maximum at dawn.

Wolinsky-Mancini and 2024, AIS in the Canary Islands: AIS radio flooding and receptions; the ducts are much more frequent in the warm months (inversion of the aliseans).

Nigerian and Malaysian articles (Ekah, Iloke, Mat, Igwe): local correlations between temperature, humidity and measured loss, weak and contradictory (r −0,09 to 0.27); useful only as confirmation that the ground sizes alone are not enough.

Herben 1984, thesis TU Eindhoven (microonde): 30 GHz on 8.2 km and OTS satellite at 11.6 GHz; sparkle for turbulence, multi-course and rain. For microwaves dynamic losses are dominated by rain and turbulence, not by ducts.

Baars, The Paraboloidal Reflector Antenna (microonde): gain of a parable G = η·(πD/λ)2 with typical efficiency 0.5–0,65; loss of surface errors according to Ruze, ηs = exp(−(4πε/λ)2). It would help if we add the parables as an alternative to Yagi DL6WU for 1296 MHz and beyond.

Already analyzed or of poor content: Hirschmann NASA X-731-67-89 (double), Reid's VHF ST radar (winds and turbulence, unconducted), Scribd slide and generic notes on the troposphere.

Fifth round of sources (20 November, evening).

Rogers 1997, Radio Science 32(1): Bayesian estimate of the parameters of a high duct (base rate, M deficit, thickness) from propagation factors measured beyond the horizon, with a parabolic equation model; VOCAR experiment in California at 143, 263 and 375 MHz, comparison with radio wave. The signal depends a lot on the base and the M deficit (correcting 0,81 to 143 MHz for the deficit), almost at all from the thickness (−0,17): the thickness cannot be obtained from the signal. For us: calculation B (climate thickness) counts less than deficit and share; and the method is the basis for the back office, that is to get the conduit from the multiple frequencies.

Moler and Holden 1960, "Tropospheric scatter propagation and atmospheric circulations", J. Res. NBS 64D, pp. 81 and following (original on 22/11/2026, doi 10.6028/jres.064d.014). California sea routes to Point Loma (San Diego): Santa Barbara 190 miles, Point Mugu 144, San Clemente 78; 1365 MHz and 9380 MHz, not VHF; January–April 1958, averages 1 minute. The case of 26 March: a reinforcement of about 20 dB for about 3 hours towards noon, when a cyclonic vortex weakens and begin sea breeze and subsidence; at 15 the convergence is reformed and the signal decreases by about 22 dB from the maximum. Statistics (Table 1, 500 hPa, 33 cases): with air that drops the average signal is higher than about 2.5 dB than with air rising, in L and X band. The signal follows the difference calculated to 500 hPa in the sign and amplitude of the variations, without a correlation coefficient. Low layers (under about 600 m) favour partial reflection on scattering. The anticyclones and fronts are not treated, and the "excursions up to 60 dB" mentioned above are not: they came from a second-hand summary. For us: the vertical motion is a good predictor, and +1.5 dB for the subsidence and −2 dB for the uphill air are of the same order as the average measured difference (2.5 dB); 20 dBs are a single case, not a typical value, and on microwave.

Banafaa and Muqaibel 2025, IEEE Access: survey of the pipelines (evaporation models, parabolic equation, radar estimates, GNSS and lidar) and classification with automatic learning on real measures; Support vector machines give the best results. Useful as a bibliography; The data are from the Persian Gulf.

ITU-R P.834-7 (2015): updated version of the recommendation on troposferal refraction; no new formula than those already used.

Abbott, NBS bibliography on the troposferous scatter (GOVPUB- C13): already in the third round. Slide Scribd on the ducts: generic content. The article ScienceDirect 0021916987900304 could not be opened.

Sixth round (20 November, night): Bean and Dutton, ARRL Handbook 2022 and 2023, Balanis.

Bean and Dutton, Radio Meteorology (cap. 6), parts not yet used. Field sensitivity to Ns: 0.18 dB per unit N on the annual cycle; 0.14–0,24 dB/N in the daytime cycle (maximum early in the morning), up to 0.2–1,1 dB/N on the routes of 175–200 km in winter; corrections 0.8–0.95. Parameter composed of Misme: M = a(g\_e − 40 + b·ΔW^n) with a = 0,5 dB per N/km, which combines the equivalent gradient and the thermal stability ΔW (work to lift the air of 1 km): in the Sahara at 471 MHz on 160 km the only stability is 20 dB between January and June at almost equal gradient. potential refraction index K (N reported adiabatically to 1000 hPa): highlights the air masses and the anticyclonic subsidence (Flavell and Lane).

ARRL Handbook 2022 (99a ed.), § 19.4.3–19.4.4. Useful inversions between 250 and 2000 m; inversions of radiation (sera, calm air, dry soil) seldom lead true; slow anticyclones give the most extensive ducts per subsidence, with inversions between 500 and 3000 m, stronger in the evening and in the morning; in the northern hemisphere the best routes are south of the high center. Hot front: good conditions a few hundred km ahead; Cold front: strong but unstable reverse behind the front, parallel. Foehn (also in the Alps) and earth breeze (cool air on the ground, hot at 200–300 m; stretches over 800 km along the coast). Scirocco: hot and dry air from Sahara over cooler marine air, very strong inversions from the eastern Mediterranean to Gibraltar, probable cause of records over 1500 km in microwave. Troposcatter: theoretical limit about 800 km, maximum typical half. The 100th ed. (2023) has a shorter text: inversions at sunrise and sunset up to +20 dB and +20–50% flow rate; 1200 miles on the Great Lakes, typical 400–600; recommends Radio Mobile (ITM model) for Hepburn coverage and maps.

Balanis, Antenna Theory (4a ed.), § 10.3.3: Viezbicke table (NBS TN 688, 1976) for Yagi optimized at 400 MHz: boom 0.4 / 0.8 / 1.2 / 2.2 / 3.2 / 4.2 λ → 7.1 / 9,2 / 10.2 / 12.25 / 13,4 / 14.2 dBd. Our DL6WU formula (8.6 + 8 log10 boom/λ) gives 1–1.7 dB less between 0.8 and 4.2 λ and returns in line with the DL6WU tables to 9.5 λ: prudent estimate. With the correct formula on the original (9.4 + 7.8 log10 boom/λ, from 22 November) the discard falls below 0.2 dB between 1.2 and 4.2 λ (12.1 dBd to 2.2 λ versus 12.25) and remains 0.6 dB to 0.8 λ.

Seventh round (November 21,): QST, QEX, W3SZ, GitHub repository.

QST 2000–2025 (14 numbers, 10 beds with text recognition). Only reports on The World Above 50 MHz, useful as cases to validate the model: September 5–9, 2000, conducted at 144 MHz and beyond on the eastern half of the USA; 14–15 August 2022, opening Manitoba–Kansas/Missouri under high pressure on a hot day, from 144 to 1296 MHz almost everything in FT8, Canadian record at 10 GHz of 1228.56 km ( VE4MA – N0OY) in Q65, signals up to 1941 km; APRS propagation map showed the same area. QST November 2022 recommends Hepburn maps. No technical article on the tropo forecast.

QEX and K1JT, Franke and Taylor (QEX 2020) on FT4/FT8 modes: FT8 decoding thresholds −20,8 dB in 2500 Hz on channel without evanescence, −18,6 dB with evanescence at medium latitudes; FT4 −17,5 dB. The other numbers of QEX 2023 and 2026 do not treat the tropo (three numbers 2023 are unread scans). W3SZ 2019: slow mode thresholds ( JT4A −23, JT9A −27, JT65A −25, QRA64A −26, FT8 −21 dB) and choice criteria between sensitivity and frequency shift tolerance.

GitHub. TropoLink (LukiBox, Apache-2.0): design of troposcatter routes with P.617-5 (with N0 and ΔN), NBS TN101 and ITM flanked, and their difference shown as uncertainty; P.617-5: L = F + 22 log f + 35 log θ + 17 log d + Lc, F = 0.18· N0 ·e^(−hs/7,35) − 0.23·ΔN, Lc = 0.07·e^(0,055(Gt+Gr)). andi-f/troposcatter ( DL2FZN 2013, from VHF/UHF Handbook RSGB): Lts = 24,44 + 4,283 ln f + 0.06656 d above free space, slope near PROPBER (0,0674 dB/km). QRadioPredict: ITM and ITWOM 3.0 with S RTM3, soil use Corins and NEC diagrams. Not relevant: Tropos ( Weather app), opera-adt/tropo and troposim (Troposferic delay for InSAR), tgbot\_troposcatter (military station), sdr-troposcatter-gui (control panel), Troposcatter-Propagation-Delay ( ERA5 delay), Radio-Communication-Project (didactic simulator), PyLayers (channel).

Eighth lap (21 November): NTIA and ITU models on GitHub. NTIA/itm (Longley-Rice, ITS): 20 MHz–20 GHz, free space loss, diffraction and troposcatter with soil profile, 0.5–3000 m terminals, surface reactivity N0 (minimity reduced to sea level, 250–400), seven climates, soil constants and various modes 1 view, 2 diffraction, 3 troposcatter; C++ with.NET Framework 4.7.2 enclosure. It does not model the ducts: it serves as a reference "normal atmosphere" with statistical variability. NTIA/p528 and eeveetza/p528 (MATLAB): ITU-R P.528-5 for air-soil connections, 100 MHz–30 GHz, 1.5–20 000 m terminals, p 1–99%;.NET 4.8 casing. Possible use: air scatter balance (two ground routes– aircraft plus radar section). eeveetza/p2001 (MATLAB): ITU-R P.2001-6, all-field model with diffraction, troposcatter, abnormal propagation (conduct and layers, Lba) and E sporadic, percentage of time 0–100%; ITU digital maps (DN\_Median, DN\_SupSlope, DN\_SubSlope, dndz\_01, TropoClim, h0, surfwv, Es) are not redistributable. Possible use as an ITU comparison instead of P.452, with the GFS ΔN instead of maps as in the CSIRO study; the maps should be downloaded by the user as per P.453. Validation 1.3.0 out of 593 QSO (29/12/2021–2/1/2022): event/comparison separation 0,79 without corrections, 0,91 with Misme and subsidence; the GFS does not see the conduit on the sea at 144 MHz (guadagno without corrections never more than 15 dB).

Ninth round (November 21,): IU3QEZ, NTIA, PyWaveProp, ACE High, validation 1.3.1. IU3QEZ (S. Fabris), presentation "Over the horizon in VHF" (GEKO Magazine n. 69, September 2026): common volume d2/(8·R\_eff) with R\_eff ≈ 8500 km (1,3 km to 300 km, 3.7 km to 500 km each level of horizon worth 69 miles (111 km), margin of 7 dB for evanescence and 4 dB for ground reflection; Thesis: 10 dB is about 100 km; station IQ3QC 5 W with 3×5 elements, 120 QSO, maximum 788 km (Alpe Adria 2026). Bibliography of the presentation to be recovered: Norton 1959 (J. Res. NBS 63D, System Loss), Bray 1961, ARRL Antenna Book 2023 fig. 4.29, Rice-Longley-Norton-Barsis NBS TN 101, Bean and Thayer 1959, Barsis and Kirby 1961 (diffration on ridge), Deygout 1966, P.1812, AirScout DL2ALF, P.834 NTIA PropLib (wiki): eHata, ITM, LFMF, P.528, P.2108 (loss from encumbrance), with C++,.NET, MATLAB and Python; no conduit model; ITS page: also IF-77 (air–soil) and MPM (1 GHz–1 THz). PyWaveProp (Python): Parabolic equation methods with Padé approximations, multiple edge diffraction, PETOOL and RAM enclosures, troposferous and high ducts, irregular soil: possible base for a field calculation with the GFS M(z profile, to be checked. APRS PropView (Apache 2.0): VHF propagation maps from APRS packages, hourly statistics and opening alarms; possible source of 144 MHz data for validation. NATO troposcatter network in Italy (fortifications.net): 900 MHz, 10 kW, 20 m parables; stations including Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Vulture, Monte Lauro; historical test case for P.617. Not relevant: Gupta 2021 ( stratospheric transport), Law 2023 (Arctic ozone), dolphin #590 and opera-adt/tropo (InSAR delay), tropo.ino (Arduino gas sensors), tropo-webapi (telefonia), cloudtools/troposphere (AWS). Validation 1.3.1 on 564 QSO tropo: AUC 0,79 (model 1.2), 0,80 (+ soft duct), 0,90 (+ misme and subsidence), 0,90 (+ persistence); conduit colors (≥ 16 dB) on 46% of opening routes and 0% of comparison.

Added to the same round. mikelytaev/wave-propagation (MIT, Python 3): Parabolic equation with approximates of Padé and Numerov, irregular soil, tropospherical ducts; charge the modified refractive M(quota, lat, lon) from the NOAA GFS and set a evaporation conduit of Monin-Obukhov; no documented function that returns the loss on the path. He is the best candidate for a future field calculation with the GFS M profile (it would serve an external service or a translation). RP-Sim (AGPL-3.0, JavaScript): Empirical models for radio networks (Hata, He, Ericsson, two rays), no tropo. RIMFading (GPL-3.0, OMNeT++): flow irregularities for network simulations, Weibull distribution, no tropo. PyLayers: indoor radio channel, already excluded. qradiopredict (kantooon, GPL v2): ITM and ITWOM v3.0 with S RTM3 and Corine, already in the seventh round as QRadioPredict.

Tenth round (November 21,): Antenna Book, Norton, Barsis, new data sources. ARRL Antenna Book (23a ed.), cap. 4 ( K9LA): Bray method (QST November 1961) for the VHF range: smooth Earth loss to 99% (300 miles, 144 MHz: 214 dB), curves to 50% and 99% (fig. 4.11): 195 dB worth 100 miles to 99% and over 250 miles to 50%; evanescence ±7 dB over 100 miles (−3,5 dB to 50 miles), SSB 3 dB worse than the CW, +4 dB of ground reflection, +3 dB for coupling of two antennas; height gain (15 m: 4/3/2 dB at 10/50/100 miles; 24 m: 8/6/4 dB); Yagi gain about 10 times the boom in λ (dBi); 50 W station with average antenna: about 180 dB, 100 miles, +10 dB up to 250 miles. Sunrise and sunset up to +20 dB and +20–50% flow rate; hot and dry air over wet fresh air on the Great Lakes up to 1200 miles, typical 400–600; regions influenced by large water masses the most favored; late winter with heating capable of strong inversions. Verification of our troposcatter: P.452 to 50% at 483 km and 144 MHz gives about 204 dB, consistent with the 214 dB of Bray to 99%. Norton 1959 (J. Res. NBS 63D, free on NIST): basic loss, system and path antenna gain; Reference atmosphere Ns = 301; equivalent radius from Ns (250 → 4878 miles, 301 → 5280, 400 → 6997); L(99) tables with 30-foot antennas (100 MHz: 163 dB to 100 miles, 208 dB to 500 miles; 1000 MHz: 138 and 188 dB with 28-foot parables); long-term variability V(p, θ) depending on angle distance (fig. 13). Rice, Longley and Norton, NBS TN 15 (1959), free on NIST. Barsis and Kirby, NBS Report 6751 (1961): 223 km in Colorado with Pikes Peak (4292 m) as edge, 100 and 751 MHz; consistent loss with the rounded edge (approximately 11.5 km radius); long-term evanescence 11,7–14,3 dB versus 39–42 dB of a scatter of the same length; no day cycle marked; the four-ray model provided 4–5 lobes but only one is measured (maximum to about 21 m from the ground): the paths with mountain obstacle are much more stable than the scatter, useful for the connections "via Vesuvius or Faito". ARRL VHF Digital Handbook: package, APRS, D-STAR, WSJT (Scatter Method and EME), no part on the tropo. New proven or searched data sources: ECMWF open data (IFS 0.25°, levels 1000, 925, 850, 700 hPa..., without 975, 950, 900 and 800; G RIB2 with CCSDS compression, that our reader does not decode; already available through Open-Meteo); DWD ICON-EU open data (G RIB2 compressed bz2, a file for variable and level: many requests); Canada GDPS on MSC Datamart and GeoMet (about 15 km, up to 33 levels; GeoMet allows the crop of the area; it is the basis of Hepburn maps); GFS NOAA archive on AWS from February 2021 (global files, useful for validation); APRS PropView only for validation.

Eleventh round (November 21,): Longley-Rice, GEKO 69. Bean and Thayer, "Models of the Atmospheric Radio Refractive Index", Proc. IRE May 1959 (pp. 740 et seq.): the model 4/3 has the right slope in the first km but falls too higher; 30% of the curvature of a radius occurs in the first 100 m and 60% in the first km; average relationship between Ns and decrease in the first km: −ΔN = 7,32·e^(0,005577·Ns) (Ns = 313 → ΔN about −42). Bean and Thayer, "C.R.P.L. Exponential Reference Atmosphere", J. Res. NBS 63D n. 3 (1959): N(h) = Ns·e^(−c·h) with c fixed by Ns through the same report; Average Ns USA 313; beam tracking tables (total curve 15.2 mrad with exponential model versus 30.3 mrad with Earth 4/3). MATLAB refractionexp: c = 0.143859 km−1 for Ns = 313 (scale about 6.95 km), valid above about 1 km and 10 mrad. Use for us: the "normal" profile of comparison can be that CRPL with the local Ns instead of M = 315 + 0.118·z fixed, and the anomaly of the decrease in the first km should be measured compared to −7,32·e^(0,005577·Ns) instead of −40 fixed. NTIA Report 82-100 (Hufford, Longley, Kissick 1982), ITM guide in area mode: climi and Ns suggested (equatorial 360, subtropical continental 320, subtropical marine 370, desert 280, temperate continental 301, temperate marine on land 320, temperate marine on sea 350); Ns = N0 ·e^(−hs/9,46 km); you use the monthly minimum of Ns because less influenced by super-refraction and sub-refraction; Ns = 301 corresponds to ae = 8497 km; under 100 km Ns counts little. Rice, Longley and Norton, NBS TN 15 (1959), part I: forecasting formula of distribution over time by land and tropospheric wave, base of TN 101. SoftWright, Wikipedia, Altair WinProp and MATLAB rfprop.LongleyRice: same information (version 1.2.2, 20 MHz–20 GHz or 40 GHz, 1–2000 km, Ns 301, seven climates, variability of time, place and situation); no one shapes the ducts, only the statistical effect of the marine climates ("very higher fields for 10% of the time or not"). GEKO Magazine n. 69 ( IU3QEZ): radio horizon 4/3 of 130 km from 1000 m, 260 km adding two stations; troposcatter with common volume between 1–3 km and 10 km; scatter from aircraft to 10 km with +10–20 dB for tens of seconds or a couple of minutes; theoretical courses with 2×10 elements per side: SSB QRP about 440 km, SSB 50 W about 510 km, CW QRP about 600 km, SSB 500 W about 640 km (500 W in SSB make 40 km more than 5 W in CW); at 90–99% of the time a margin of 10–20 dB should be removed; 5–10 dB per horizon degree on long stretches; the quota counts to free the horizon, not to approach the common volume; Alpe Adria 2026: 99% of the QSO troposcatter, almost no one by air despite AirScout.

## 12. Round table: sources to validate the model or correct it (November 21)

Summary of all turns of sources, ordered for use. "State" says if the source is already in the program (1.3.1), whether it is in test or whether it is to be done.

### Data to validate (what really happened)

| Source | What gives | Use | State |
| MMMonVHF reports from the author | 593 QSO at 144 MHz, 29/12/2021–2/1/2022, time and locators | Event validation against comparison (+14 days) | in use: AUC 0,79 (1.2) → 0,90 (1.3.1) |
| The World Above 50 MHz | documented cases (14–15/8/2022 Manitoba–Kansas, 144–10 GHz) | historical case | in use as a historical case; to be added to the validation QSOs |
| wspr.live (WSPR) | complete archive, 2 m and 70 cm, SQL | many events and many "no" (hours without spot) | tried, it should be filtered (spot false at 16–20 thousand km) |
| APRS PropView / APRS-IS | stations felt, distances, hours | only validation 144 MHz | to do |
| Reverse Beacon (archive) | daily spot | VHF | discarded for now |
| I GRA2 radiosondaggi (Brindisi 16320, Practice of Mare 16245, Trapani 16429, Udine, Milan) | profiles measured 00 and 12 UTC | GFS profile control and inversions | found files (Brindisi 46 MB), to be used |
| Barsis and Kirby 1961, Barsis 1963 | evanescence measured on paths with mountainous obstacle | validate the category "monte" (Vesuvio, Faito) | to do |
| Norton 1959, NBS TN 15, TN 101 | L(99) and table media losses | Scatter control | partly (see 300 miles consistent) |

### Corrections suggested by sources

| Correction | Sources | Effect measured on validation | State |
| Misme Stability Term | Bean and Dutton cap. 6 | AUC 0,79 → 0,90 together with the subsidence | in 1.3.1 (Title 16 dB without conduit) |
| Subsidence from vertical motion | Moler and Holden 1960 (read in original on 22/11: +2.5 dB average between air coming down and air rising, 1.4 and 9,4 GHz by the sea) | as above | in 1.3.1 (+1.5 / −2 dB) |
| Soft pipeline (λ up to 3·λmax) | Kerr, Hepburn, VHF/UHF DX Book, Shi 2019 | alone +1% of AUC; brings the colors of duct on 46% of the routes of the event and 0% of the comparison | in |
| Persistence 24 h | VHF/UHF DX Book, Biancomano and Leyshon | AUC unchanged, more taken and more false alarms | in 1.3.1 |
| Normal CRPL profile with local Ns (−ΔN = 7,32·e^(0,005577·Ns)) instead of 40 N/km fixed | Bean and Thayer 1959, NBS Monograph 4 | tested in validation | in 1.3.2 as an alternative, off due to defect |
| Super-refraction threshold 79 N/km (ITU) instead of 100 (Bean and Dutton, layer 1000–850) | P.453, Lopez 2008, DX Book | tested in validation | in 1.3.2: aggressive estimate alongside prudent (the map uses prudent) |
| Climatological thickness (calcula B) less important than the deficit | Rogers 1997 | to try: use A for gain | to do |
| Parabolic equation in the GFS profile | PETOOL, wave-propagation (Lytaev), Levy (book not read, only preview), Lindquist | first version with field over the horizon dominated by numerical noise (AUC 0.12–0,28): correct (domain 8 km, beam 6°); after correction AUC 0.54–0,66 | in use as a second calculation, does not decide the forecast |
| Evanescence: ±7 dB over 100 miles, margin 10–20 dB to 90–99% | Bray 1961, Antenna Book, GEKO 69 | thresholds of ways in marginal conditions | in use: probability for period and threshold FT8 −18,6 dB in troposcatter |
| Common volume and equivalent distance of Bray | IU3QEZ, Bray 1961 | Information | in |
| Troposcatter P.617 and RSGB as uncertainty | TropoLink, DL2FZN | Information | in 1.3.0 |

### What remains out of models

Conducted on the sea at low altitude (100–200 m) that the GFS levels do not see (study of current data sources); reflection and diffraction on the nearby mountains (Vesuvio, Faito): proposal for calculation "via monte". ITM/Longley-Rice remains a statistical reference "normal day" (Ns 301, climi), without conduits. Discarded by decision of the author: AirScout, P.528), E sporadic (P.2001) and 50 MHz (servoir EIRP that few have and confuse with the E sporadic).

### Proposal for a working order

1. Extended Validation: add filtered WSPR (even hours without spot) and QST 2022; repeat for 432 MHz.
2. Try the normal CRPL profile and the ITU threshold of 79 N/km, one at a time, keeping only what raises the AUC.
3. Comparison of the GFS profiles with the I GRA2 radio wave of Brindisi and Practice of Sea in the days of the QSO.
4. Reevaluate the parabolic equation after numerical correction.

### Decisions of 21 November (version 1.3.2)

- Adopted: prudent estimate (Bean and Dutton, 100 N/km) and aggressive (ITU, 79 N/km) flanked; ducts sought only under 2500 m (first 1500 m, raised after validation); bonus with high extended pressure and weak wind; sea weight with μ1 of P.452; share of the stations with respect to the conduit also on partial conduit; boundary layer above the ridges ( IT9GSF).
- Adopted as an alternative, turned off by defect: normal CRPL profile with local Ns.
- Correct parabolic equation numerically: AUC from 0.12 to 0.54 (30 m antenna) and from 0.28 to 0,66 (300 m).
- Land: Copernicus DEM GLO-30 instead of Terrain Tiles «skadi», which near the coast have ghost reliefs of 80–100 m at sea (signation of IC8TEM, verified point by point to Capri); skadi remains as a reserve.
- Weather signs on the map with their effect: ↓ favorable subsidence (Moler and Holden 1960, on average about 2.5 dB between air going down and air going up; +1.5 dB), ↑ air uphill unfavorable (depending deep; −2 dB), S scirocco favourable clue (ARRL Handbook § 19.4, inversions very strong on the Mediterranean; no correction). Double symbol, green/red/orange colors and comic book on the map.
- Adopted the ITM 1.2.2 (NTIA/itm, public domain), translated into C#: according to calculation of the baseline in normal atmosphere next to P.452 and final stretch after the exit from a conduit that ends before the correspondent. Discarded qradiopredict code because under GPL v2.

Without useful content for the index: *Communications Quarterly*, autumn 1991 (no article on the tropo; Atkins' article on the Scatter is in winter 1991).

- Adopted as the principle of manuals (November 21,): ' propagation cannot be generalized, but studied in relation to local morphology' ( DF5AI, reported by IK3XTV in 'Propagation on 2 meters'); cited in the forecast manual (§ 1) and in the councils.
- Adopted (November 21, after 1.3.3) the interpretation of IK3XTV 's transalpine openings: not "convective cells such as lenses", but troposcatter reinforced by turbulence to the top of the layer re-enacted when it reaches the share of the common volume (manual of the forecasts, § 8 ter). From 1.3.4 it enters the point-point calculation as a provisional reinforcement of the troposcatter, at most 3 dB. Discarded by the same articles: the "auto-amplification" of the signals beyond the Alps (a passive medium does not amplify: the described links remain explainable with diffraction and troposcatter; the declared powers are not used as given), the snow that "improves diffraction" (most likely the stable layer over the snow, with less evanescence).

- Kerr (1951) reviewed on the book (November 22, PDF provided by the author): § 1.5, eq. 17 and 18 and table 1.1. The figures of eq. 18 in the scan are unreadable, but table 1.1 confirms: 20 ft → 1 cm, 80 ft → 10 cm, 400 ft → 100 cm, 2000 ft → 1000 cm, against 1,25, 10, 112 and 1252 cm of λmax = 0.014·d^1.5 (λ in cm, d standing; the round table, 'indicative values'. Kerr's hypotheses: inside the conduit the modified index drops of 4·10−8 per foot, typical value of the low ducts; with weaker gradients need a greater thickness. Kerr also warns that λmax is not a net cut: wavelengths sometimes longer can still be strongly influenced by the duct.

- DL6WU reviewed on the original (November 22, PDF of VHF Communications 3/1982 provided by the author): G. Hoch, «Extremely Long Yagi Antennas», pp. 130 and following. Fig. 1: gain on the dipole measured at 432 and 1296 MHz in a professional polygon of the German Posts, with a sample antenna EIA from 7,7 dBd, estimated accuracy 0.5 dB; 2,35 dB for double the length of the boom, no saturation up to about 20 λ. The program used 8.6 + 8.0.log10(L/λ) dBd, i.e. about 0.8 dB in less than 1–2 λ; now it uses 9,4 + 7.8·log10(L/λ) dBd (the gradient is the stated one, the value to 1 λ is read on the chart, ± 0.3 dB). Discarded the formula 10·log10(7,8·L/λ) circulated on the net: would give 3 dB for doubling, against 2,35 measured.
- Booker and Gordon (1950): reference confirmed by "This Week's Citation Classic" (Current Contents, 8 February 1982, commentary by H. G. Booker): Proc. IRE 38, pp. 401–412. The comment confirms the two points used in the manuals: field received beyond the horizon with Rayleigh distribution, and exponential autocorrelation function of irregularities (non-Gaussian). The original article has not been read.

## 13. From IK3XTV articles

The two articles by Flavio Egano IK3XTV (Radio Rivista: "Propagation on 2 meters" and "Propagation VHF – convective cells and gravitational waves") add two things that the duct models do not cover: the transalpine openings of the afternoon due to convection, and practical rules on where and when the pipelines are formed in Italy. They are observations from station, not measures: they must be treated as hypotheses to check with spots.

| Comments of the author | How it becomes an ingredient of the index |
| The ducts are mainly formed between 100 and 1000 m, rarely higher; serve high pressure on a vast area, good insolation and absence of wind | search for the duct only under 1500 m; bonus with high extended pressure and weak wind in the low layers |
| In the warm months conducted recurring on the Adriatic between Veneto and Puglia, 700–800 km, sometimes up to Greece; the sea routes are favorite | greater weight to the fraction of sea route (such as the μ1 of ITU-R P.452) |
| The corresponding must be in or near the conduit; with high antennas and a few meters lead the signal does not enter | compare the share of the base of the conduit with the share of the two stations (will serve the altitude of the QTH) |
| Same time, same conduit: listen from the Gargano a station in 144 and one in 50 MHz | to check: according to the Hepburn table the 50 MHz ask for over 300 m lead, so the event would indicate a pipeline very often |
| Connections Veneto – South Germany beyond the Alps, without visual, almost always in the afternoon and with high pressure; unstable signals that disappear and reappear after a few minutes | "convective" index separated from the conduit, for mountain routes |
| The convective cells are born on the ground and rise about 1 km every 3 hours: at 15 they reach about 3 km, the altitude that serves to override the Alps | use the limit layer height (boundary\_layer\_height of Open-Meteo GFS) along the path: score when it exceeds the altitude of the ridges |
| The Alps generate gravitational waves that, together with convective motions, create irregularities of the refractive index | hypotheses: strong wind perpendicular to the chain to 700 hPa as signal of gold waves; to try last |
| Very high inversion (up to 8 km in exceptional cases) gives about 800 km, but in practice rarely more than 300 km | the share of the inversion extends the scope: useful data to estimate the waiting distance |
| In summer Earth radius equivalent to about 16000 km (2.5 times the real), with about 60 dB in less than 300 km | consistent with k50 = 157/(157 − ΔN): k = 2,5 corresponds to ΔN about 94 N/km, that is super-refraction |
| More than 3000 km only in the 20–30° range north and south of the equator, above the seas, linked to the Hadley cells; peak in August in Africa–Europe | outside Europe: useful not to give high indices on the Atlantic without feedback, and for the Canary route – Great Britain |

Other useful data from the same articles:

- Optical range between two h1 and h2 meters (d in km): d = 4,1 · (√h1 + √h2), up to about 20% for normal refraction.
- Evanescence: slow and shallow with inversion; strong and fast with scatter and convective cells. The program could use it to classify ads if one day it will have SNR reports over time.
- For the sporadic E in 144 MHz the author reports the work of DF5AI and Davis and Johnson (Nature 435, 2005) on the bond between temporals and Es: out of the tropo, but a separate indicator tomorrow could use lightning data.
- Ground reflection in the double jump Es: sea K = 81 and 5 S/m, countryside and woods K = 13 and 0.006 S/m, urban areas K = 3 and 0.001 S/m.

## 14. LIVETROPOVHF index hypotheses

Starting proposal, to be discussed: an index 0–10 on the same scale as Hepburn, calculated for each point of the grid and every 3 hours from GFS (then ECMWF and ICON for comparison), and a second number per route that says how much part of the route passes into a useful duct to the chosen band. The weights below are my assumptions, to be calibrated with the spots.

Review after Bean and Dutton and Kerr: profile thresholds come from their table 6.5 and scale from their measurements, so a refractive layer is worth 10–25 dB over the normal profile. The index is then calculated first in dB and then takes on 0–10; the values in that scissor remain to be calibrated with the spots.

Calculation by point, step by step

1. T discharge, relative humidity, geopotential height and vertical speed at 1000, 975, 950, 925, 900, 850, 800, 700, 600 and 500 hPa, plus T and 2 m humidity, ground pressure, layer height and wind at 10 m.
2. Discard the levels below the ground; calculation N and M at each level (ITU-R P.453).
3. Calculation −dN/dh in each of the four layers of Bean and Dutton (1000–850, 850–700, 700–600, 600–500 hPa) and the chart as sub-refractive, normal or super-refractive with the thresholds of their table; inside the first layer also use intermediate levels to find the strongest gradient and its share.
4. Where dM/dh < 0 I find base and top of the duct, thickness d and deficit ΔM.
5. Comparison of with the minimum thickness for the band from the formula of Kerr (about 180 m for 144 MHz, 89 m for 432 MHz, as in the Hepburn table); under the threshold the conduit still counts, but less.
6. I stimulate the gain in dB with the table below (you take the best layer, not the sum of the layers, plus the term of anomaly) and convert it: index = dB / 2.5, between 0 and 10, so 25 dB is worth 10. A negative value (sub-refraction) shows how 0 but remains in the path data.

| Best layer situation | Criteria | Estimated earnings |
| Normal (not laminated) | −dN/dh in the tab range. 6.5 (e.g. 20–60 N/km between 1000 and 850 hPa) | 0 |
| Slightly above normal | between the upper limit of the normal band and the super-refraction threshold | 0 to 10 dB, in proportion |
| Super-refraction | beyond the layer threshold (100, 80, 70, 50 N/km climbing) but under 157 N/km | from 10 to 15 dB, in proportion up to 157 N/km |
| Too thin pipeline for band | dM/dh < 0, thickness below Kerr threshold | 15 |
| Useful connector for band | dM/dh < 0, thickness above the Kerr threshold | 20 dB, +1 dB every 5 M units of deficit, maximum 25 |
| Sub-refraction | −dN/dh < 0 in the lower layer | −10 dB |
| Anomalous background (you add) | Model Ns minus ITU map climatological Ns for that month | 0.2 dB per unit N, between −5 and +5 dB |

Calculation by route (between two locators): sample the maximum circle every 10 km, as already does the program for the index on the path, and calculate the fraction of points with useful duct and the average share of the base. The CSIRO study found a useful part of the route in the pipeline (on AIS routes at 162 MHz was 67% higher than in control cases).

Adding the calculation per route (from LA0BY and W3SZ): (1) sea stations compared with base and top of the conduit, because a high conduit only serves those who enter; (2) a troposcatter "floor" with the Yeh method, with the starting corners taken from the ground profile; the trope gain in dB is removed from that loss and the result is compared with the loss that the station manages to overcome ( KK6MC table). Thus the index also becomes an estimate of reachable distance.

Corrections to try one at a time (from Bean and Dutton and LA0BY, IK3XTV, G4MCU already in the manual), to be kept only if they improve comparison with spots:

- Mixing ( importante important according to the author, by daily observation): day on earth, between mid morning and afternoon with clear sky, or with wind on the ground over a few m/s, the ducts on the ground disappear; their contribution must be reduced. Not at sea.
- Time of the day ( importante important according to the author, by daily observation): irrational ducts are born at sunset and fall after sunrise; it is a 3-hour forecast control.
- Vertical motion: air rising to 850–700 hPa remixes the layers (negative correction), air descending under an anticyclone strengthens them (positive).
- Pressure trend: uphill pressure on the path in the last hours, higher and stable signal; in decrease, lower signal (bastano 2 hPa).
- Synonym: high pressure above 1025 hPa, temperature at 850 hPa much above that on the ground, hot and dry air flowing on the colder sea, route by sea.
- Mountain paths: a second "convective" index (section IK3XTV), with an afternoon limit layer higher than the ridges along the path and high pressure.

From VHF/UHF DX Book, other corrections to try: consecutive days of high pressure on the point (more days, more points), position compared to the center of the anticyclone (bonus at the edges), pressure gradient on the ground as a measure of the wind, strong rain in the previous 24 hours (penality), low clouds at the edge of the layer (penality only for the high bands). For the direction of the routes: compare the azimuth of the route with that of the isobare.

Verification

- LIVETROPOVHF archive: every commercial over 500 km to 144 or 432 MHz is a "event". It compares the index on the route with the frequency of events, at the same time of day and month (the same approach of the CSIRO study).
- Comparison with Hepburn and F5LEN on the same days: where the three indexes disconnect, you look who was right according to the spots.
- Radiosundations of 00 and 12 UTC: to check if the model sees the inversions that the probe measures.
- Historical cases of LA0BY (6–11/11/2003, 10/12/2004, 15/10/2011, 11/2/2015, 22/11/2018): the index recalculated on those days must see the ducts that Hepburn did not show.
- Limits to be declared in the manual: conducted under 100–200 m almost invisible to GFS pressure levels; reliable prediction for 2–3 days, then only trend.

## 15. Sources of 21 November (second part)

- ITU-R P.1812 (MaTLAB/Octave implementation of I. Stevanovic, OFCOM; GitHub eeveetza/p1812, Zenodo 10.5281/zenodo.6141558). Specific VHF/UHF area method of the route, derived from P.452: "delta-Bullington" diffraction, troposcatter, ducts and reflection on layers, engulfs on the ground. Useful as a third comparison next to P.452 in point-point calculation; ΔN and N0 data are full ITU products, so the user downloads them (as per P.453).
- from Costa, Rabanal Ramirez, Dias, IET MAP 2018 (doi 10.1049/iet-map.2017.0992). 563 MHz, 27 points between 5 and 30 km on irregular terrain with forest: P.526 with 12 m of vegetation −4,3 dB and σ 8,8 dB; P.1812 with 10 m of encumbrance +2,6 dB and σ 8,9 dB; dielectric wedge −4,8 dB and σ 9,2 dB; P.1546 +5,0 dB and σ 11,7 dB. For the program: the residual error of a good diffraction method is about 9 dB, and the height of the encumbrances on the ridges (10–12 m) counts.
- PETOOL (Ozgun e altri, Computer Physics Communications 182, 2011). Parabolic equation split-step to one and two routes with wide angle propagator (angles up to 40–45°, against 10–15° of the narrow one); bottom with losses through the transformed Fourier mixed; ground to steps. Confirms the choices of 1.3.2 (wide angle, soil coefficients) and indicates the two-way calculation for steep reliefs.
- MathWorks. `tropopl`: loss of gases and loss of «lente» in a layered atmosphere (P.835, P.676, P.453), without ducts: with gradients below −157 N/km the geometric optical model does not apply. The example of radar propagation uses for refraction −ΔN = 7,32·e^(0,005577·Ns), the same CRPL report already in the program. `propagationModel` Longley-Rice: predefined horizontal polarization values, conductivity 0.005 S/m, permittivity 15, reactivity 301, temperate continental climate. No MathWorks model deals with the ducts.
- ITU-R, digital products of group 3. Data and programmes of P.453, P.452, P.1812, P.2001, P.528, P.1546; 'integrated' data are part of the recommendations; no declared redistribution license: you continue to let them download to the user.
- ESA Navipedia, troposferical delay. N = Nidrostatica + Numida; the dry zenital delay is about 2.3 m and the humid one tens of cm, which varies quickly. For us: wet delay estimated by GNSS stations is an indirect measure of integrated humidity, possible future source of now-casting; Today is not usable.
- Bray, K2LMG, «A Method for Determining V.H.F. Station Capabilities», QST November 1961, pp. 36–40 (read in original; rechecked on November 22 on the PDF of the number of QST: all values below correspond). The author, D. W. Bray (General Electric, Ithaca), says he has verified the forecasts with months of scheduled connections with K2GOI and W4LTU. Evanescence within the time of 13,5 dB over about 100 miles (7 dB below and 7 above the hourly average), almost linear below (3,5 dB to 50 miles): for a solid connection remove 7 dB. Correction of the horizon: each elevation of the horizon is worth 69 miles (111 km) of extra distance (less if negative). Gain of a yagi about 10 × boom/λ (power ratio), more about 4 dB of soil reflection; SSB −3 dB and AM −7 dB compared to CW; 99% of the hours: about 180 dB to 60 miles, 190 to 90, 200 to 240 (2 m). From 1.3.2 the probability for period uses σ = 3,4 dB (±7 dB as ±2σ) instead of the 5 dB estimated second hand.
- Two-way calculation (try with prototype, 21 November). Large angle parabolic equation with real S RTM1 terrain from Capri, 144 MHz, antenna 10 m from the ground, standard atmosphere; measure: average power between 20 and 400 m to 80 km on the route towards the corresponding. From the centre of Capri and Marina Grande the west is closed ( obstacle to +11–21°, Monte Solaro): pointing the Faito (almost behind EA5 and EA6, 150–160° off axis) the signal launched towards Spain climbs 10–29 dB; pointing the Vesuvius (110–125° off axis, then out of the calculation plan, only indicative) of 17–25 dB. From Anacapri the west is free and the direct wins of 8–22 dB. The absolute losses on 1000–1300 km of the prototype, however, are not reliable. EA3 EA5 EA6
- Barsis and Kirby, NBS Report 6751 (1961), Pikes Peak, 223 km, 100 and 751 MHz. The mountain is a common terminal of two routes in optics: the distribution of the hourly medians is the convolution of those of the two routes. Long-term excursion (1–99%) of about 14 dB, against 26–40 dB of a troposcatter path of equal length or angle; no appreciable day change. The signal is stronger than the troposcatter but weaker than the ideal edge: the top should be treated as rounded (rough about 11.5 km). High signals are always observed behind the backs.
- Barsis, NBS Report 7652 (1963), three routes over a mountain (Colorado, Alaska, Europe), 750–900 MHz. Average loss 10–20 dB higher than Fresnel-Kirchhoff calculation without land near the terminals; the evanescence is estimated by wrapping the two lines in optics; the reflections of the soil near the terminals worsen the prolonged fade. From 1.3.2 the entry into the conduit adds the rounding of P.526.
- Barsis and Johnson, NBS TN 88 (1961). Prolonged dissolving of the space wave, even on a path beyond a mountain (§ 5).
- Norton, «System loss in radio wave propagation», J. Res. NBS 63D (1959). The curves from which Bray gets the nomograms; definitions of system loss, transmission and basic, process antenna gain.
- Rice, Longley, Norton and Barsis, NBS TN 101 (1965–67), Volumes 1 and 2. single edge and obstacle rounded with and without reflections (§ 7, with the example of Pikes Peak), long-term variability V(0,5) and Y(q) for climates (tempered concontinental and others). For us: the method of rounded obstacle and variability functions are the reference to calibrate uncertainty.
- ITU-R P.526-16 (11/2025). Formula (31) of J(ν), identical to that of the program; Rounded hurdle (32)–(36), now used; multi-split methods (Bullington and full method § 4.5).
- ITU-R P.1812-8 (2025) and -7. Complete text recovered: specific method of the 30 MHz to 6 GHz (delta-Bullington, troposcatter, ducts and reflection on layers, ingombs). The -7 warns that its troposcatter (from P.617-5) is wrong under 66 km. It remains a possible third comparison.
- Paulus, «Plavatical application of the IREPS evaporation model», NOSC TR 966 (1984), and Radio Science 20 (1985). Report read on 22/11/2026 (DTIC ADA146528, scan with recognized text, sometimes unreadable). Air–sea differences measured by ships are shifted towards stability: warmer sea air in 31–35% of ship data versus 0.7–7% of NOAA boars, for psicrometers disturbed by ship and SST taken from cooling water (approximately +1 °C). The Jeske model then gives 40 m and more than 15% of the year and heights that jump from 10 to 40 m in 24 hours with stable time, not confirmed by radio measures. Proposed correction (tested thresholds 0, −0,5 and −1 °C; the −1 °C is better agreed with the radio measures of June 1982): if the air–sea difference is −1 °C or less the measured difference; otherwise you calculate the height with −1 °C and with 0 °C and, if that with −1 °C is lower, you hold that. Increases on the stable side are cut, decreases remain. In June, Marsden 120 square: average height from 16.8 m (climatology) to 7.6 m. The equations of the model are not in the report (refer to Jeske and Hitney). The program uses the neutral profile, which does not have stable exaggeration, and limits δ to 40 m: it goes in the same direction; with the SSTs of the models (not of ship) the fault of measurement that Paulus corrects weighs less.
- Levy, «Parabolic Equation Methods for Electromagnetic Wave Propagation» (IEE 2000). On Google Libri there is only preview: I don't read.
- VK3FS (3fs.net.au). Quality introduction: high ducts at 450–2000 m for longer openings on the ground; inversions of 300–600 m in calm and calm nights, which disappear after sunrise; stronger coastal inversions in the afternoon; 800–1300 km typical, 1400–1600 km frequently conducted. Consistent with day/night alerts and ceiling of 2500 m.

## 16. Sources

Rules and formulas

- [ITU-R P.453-13 (2017), radio refractive index:](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)formula and data
- [ITU-R P.452-18 (2023), prediction of interference between ground stations over 100 MHz: equivalent radius, β0, troposcatter,](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)ducts
- [ITU-R P.617-5, propagation beyond the horizon for radio bridges](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)
- [Shi et al., "A Comprehensive Study on Wave Maximumlength of Electromagnetic Propagation in Different Evaporation Ducts", IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

Studies on prediction of ducts with weather models

- [P. Lopez, "A 5-year 40-km resolution global climatology of super-refraction for ground-based radar meteorology", ECMWF Tech. Memo 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO), "Tropospheric Ducting Prediction based on GFS Model Data at... the CSIRO Murchison Radio-astronomy", Observatory arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (FMI), "Effects of model resolution on predicting atmospheric ducting conditions with Harmonie-AROME", EGUsphere 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

Amateur radio forecasts

- [W. R. Hepburn, "The Hepburn Tropo Index (HTI)"](https://www.dxinfocentre.com/propagation/hti.htm)and [](https://www.dxinfocentre.com/propagation/history.htm)method history
- [W. R. Hepburn, "Lowest Usable Frequency vs. Thickness of Ducting Layer"](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, "Worldwide refractive index forecasts"](https://tropo.f5len.org/WW/info.php), [new version 2026](https://tropo.f5len.org/?p=504), [note on model ARPEGE](https://tropo.f5len.org/author/f5len/)

Data

- [Open-Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api), [note on pressure levels](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)
- [NOAA NCEI, Integrated Global Radiosonde Archive (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RAOBget, radio flood script for the University of Wyoming](https://github.com/NCAR/RAOBget)

Classical texts (indicated by the author of the program, to be consulted for final formulas)

- B. R. Bean, E. J. Dutton, *Radio Meteorology*, NBS Monograph 92, 1966, 435 pages (reprint Dover 1968): vertical profiles of N and M, gradient of trap under −157 N/km, comparison with radio wave; chapter 5 "Synoptic radio meteorology". Base for 2–4 steps of calculation per point. Free: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [NIST card, doi 10.6028/NBS.MONO.92](https://www.nist.gov/node/1770911), [Internet Archive](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf)copy.
- L. Boithias, *Radio Wave Propagation*, McGraw-Hill 1987 (translated by D. Beeson from French, 330 pages, ISBN 0070064334; also North Oxford Academic): mechanisms beyond the horizon, common volume of scattering and loss of coupling antenna-mezzo. Paper only ([library card](https://libcat.library.tamu.edu/Record/in00000014812/Description)).
- M. P. M. Hall, *Effects of the Troposphere on Radio Communication*, IEE Electromagnetic Waves Series n. 8, Peter Peregrinus 1979, 206 pages (ISBN 0906048257; brossura 0863410863): synoptic situations and propagation VHF/UHF. Base for Synoptic ingredients. Trade at IET ([](https://libcat.library.tamu.edu/Record/in00000636385)card).
- H. G. Booker, W. E. Gordon, "A Theory of Radio Scattering in the Troposphere", *Proceedings of the IRE*, vol. 38, n. 4, April 1950, pp. 401–412: Scapping theory from the disogenity of the refractive index. Central idea: the diffused power depends on the intensity and scale of the turbulent fluctuations of the refractive index (exponential correlation); near the horizon prevails diffraction, further scattering; According to the authors the scale of turbulence and the index variations expected by meteorology are sufficient to explain the measured fields. Cited by 328 articles (Scilit), no free access copy. Card on Semantic Scholar ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); full text on IEEE Xplore paid ([doi 10.1109/JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)).
- C. M. Crain, J. R. Gerhardt (Electrical Engineering Research Laboratory, University of Texas), "Measurements of the Parameters Involved in the Theory of Radio Scattering in the Troposphere", *Proceedings of the IRE*, vol. 40, n. 1, January 1952, pp. 50–54, doi 10.1109/JRPROC.1952.2739 give the real values that Booker and Gordon had only estimated ([Skilit](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)card).
- "The Constants in the Equation for Atmospheric Refractive Index at Radio Frequencies", *Proceedings of the IRE*, vol. 41, n. 8, August 1953, pp. 1035–1037: proposes to review the currents then in use (79 K/mb and 4800 K) in the formula N; it is from here that the values 77,6 and 3,73 · 10^5 of the formula ITU ([Q](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E. C. S. Megaw, "Fundamental radio scatter propagation theory", *Proceedings of the IEE Part C: Monographs*, vol. 104, n. 6, September 1957, pp. 441–455, doi 10.1049/pi-c.1957.0052: spectrum of index fluctuations from turbulence theory, complete tracing of scatter beyond the common horizon and effect of the geometry paid at the IET ([Shilit](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)card).
- *Proceedings of the IRE*, October 1955, special number on scatter propagation (by K. A. Norton and J. B. Wiesner), with Bullington, Staras and others: free in PDF on [World Radio History](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf).
- D. E. Kerr (edited by), *Propagation of Short Radio Waves*, MIT Radiation Laboratory Series vol. 13, McGraw-Hill 1951 (Restampe Dover 1965 and IEE 1987): theory of evaporation and surface ducts on the sea. Digital copy: [World Radio History](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); IET edition in [IET Digital Library](https://shop.theiet.org/propag-short-radio-waves).

Recent measures of troposferical diffusion in VHF: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Australia), "A Study of VHF Troposcatter Communication Channels in South Australia", in *Digital Signal Processing for Communication Systems*, Springer 1997, pp. 103–112 ([Springer](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)). Text for a fee; from abstract it concerns diffusion, not the conduits. Useful only if you want to calibrate the term of troposcatter with VHF channel data.

Numerical models and sea routes (indicated by the author of the program)

- M. Levy, *Parabolic Equation Methods for Electromagnetic Wave Propagation*, IEE Electromagnetic Waves Series #45, 2000, 336 pages (ISBN 0852967640): parabolic equation for refractive profiles that change along the path and for orography; chapters on troposferous propagation, rays and ways, propagation on sea. It would serve for a field calculation along the path, beyond the index. For payment, also in electronic form from IET ([IET card](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)).
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (University of Leicester): 2 GHz measurements on the Channel Islands and the Channel. Many works and thesis are free in the university archive: [long-term statistics on the evaporation duct, Radio Science 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434), [signature changes on three sea routes, Radio Science 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710), [Titles of N. Mufti 2012 with routes of 50 and 140 km also in VHF](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854), [tesis of I. Alam 2002 on reactivity calculated from the weather data, 240 MHz and 2 GHz X3 UQ, [tesi..](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus, "Practical application of an evaporation model", Radio Science vol. 20 n. 4, 1985, p. 887 (not IEEE Transactions on Antennas and Propagation), doi 10.1029/RS020i004p00887 ([scheda](https://zendy.io/title/10.1029/rs020i004p00887)): height of the evaporation duct from surface weather data. Free the technical report preceding it: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf).

T. Lindquist, *Wave Propagation Models in the Troposphere for Long-Range UHF/SHF Radio Connections*, master's thesis, University of Karlstad, 2 June 2020, 106 pages (supplied by the author of the program). It is a step-by-step guide, free, to write a calculation with the parabolic equation: split-step solution (even at wide angles), boundary conditions for land and sea, ground to steps, beam tracking on linear profiles at times, with numerical checks; cites the PETOOL free tool for MATLAB. Two useful data at once: (1) N sensitivity to weather variables, δN ≈ 0.26 δP + 4,3 δe − 1,4 δT (hPa and K, around 20 °C), i.e. an error of 1 hPa on the steam pressure weighs three times a 1 °C error on the temperature: is the moisture of the model that must be checked first; (2) Average height of the evaporation duct: 5–6 m in the North Sea, 13–14 m in the Mediterranean, over 20 m in the Persian Gulf, always too low for 144 and 432 MHz. Author's conclusion (frequencies 1 to 10 GHz): beyond the horizon the reliable connections almost always require a duct.

Propagation software on the soil profile (for horizon and obstacles, not for the expected tropo)

- [Radio Mobile](https://ve2dbe.com/rme.html)by Roger Coudé VE2DBE: free, Windows (also Linux and Mac with Wine), Longley-Rice/ITM model, terrain profile automatically extracted from quota data ([download](https://ve2dbe.com/download/download.html)).
- [SPLAT!](https://qsl.net/kd2bd/splat.html)by John Magliacane KD2BD: GPL, 20 MHz–20 GHz, Longley-Rice and ITWOM v3.0 models, obstacle and loss reports in text; latest version 1.4.2 of 2014.
- [ITM, NTIA/ITS](https://its.ntia.gov/software/itm)official code: the Longley-Rice model (20 MHz–20 GHz) with source and documentation, base of Radio Mobile and SPLAT!. The code is free and you could add to the calculation on the path of LIVETROPOVHF (loss due to the soil, to be added to the tropo index). Original report: A. G. Longley, P. L. Rice, "Prediction of Tropospheric Radio Transmission Loss Over Irregular Terrain: A Computer Method – 1968", ESSA Technical Report ERL 79-ITS 67, July 1968 (NTIA card on historical software: X1 https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITU-R, software and digital data](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): free maps of ΔN and N0 annual medians on grid 1,5° used by P.452 and P.617 — useful as reference climate values for the index.
- Pathloss (Contract Telecommunication Engineering): commercial program for radio bridges; I have not found a public source that confirms which ITU recommendations implements, so to check.

Reusable GitHub Code in LIVETROPOVHF

| Project | What do you do | Language | Use possible for us |
| [NTIA/ITM](https://github.com/NTIA/itm) | model Longley-Rice official of NTIA, with packages for.NET |  C++, C#  | land loss on the path: integrates directly into the program, which is in C# |
| [fuzzzeee/itm](https://github.com/fuzzzeee/itm) | ITM C++ code C# translation |  C#  | alternative without native libraries, also useful for the Windows 7 version |
| [eeveetza/Py452](https://github.com/eeveetza/py452) | ITU-R P.452, identical to the MATLAB/Octave version approved by ITU-R WP 3M; MATLAB version published by the Swiss Federal Communications Office | Python, MATLAB | reference to translate into C# the troposcatter formulas and conducted, with ITU validation examples |
| [bwinkel/pycraf](https://github.com/bwinkel/pycraf) | P.452 complete, P.676, SRTM profiles; GPL v3, but ITU data files included are not free for commercial use | ♪ | control of our results |
| [ITU-Rpy](https://github.com/inigodelportillo/ITU-Rpy) | many ITU-R recommendations, including P.453-13, validated with ITU examples | ♪ | verification of the N formula and climate maps of P.453 |
| [NCAR/RAOBget](https://github.com/NCAR/RAOBget) | download the radio wave of the University of Wyoming in text format | ♪ | diagram to download in C# the profiles of the Italian stations |

Note: the current version of P.453 is the [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), downloadable free of charge; in the document I used the 13, to be reallined.

Already in the program manuals and reusable here: LA0BY (Nordic VUSHF 2019), IK3XTV ("Propagation on 2 meters" and "Propagation VHF – convective cells and gravitational waves", Radio Magazine, provided by the program author), DF5AI/DK5YA (2003), G3YGF (QST 1983), The VHF/UHF DX Book cap. 2, G4MCU (2003) KP4MD
