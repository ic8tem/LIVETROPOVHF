LIVETROPOVHF · technical document 3
# How to form the trope in 144 MHz and beyond

> Machine translation from Italian: if in doubt, the Italian guide is the reference.

What happens in the air when the 144, 432 and microwaves go beyond the horizon: refraction, inversions, ducts, scatters, and how to read them on the program map. Version of 9 November 2026.

## In summary

- The VHF waves and beyond travel in the first kilometers of atmosphere (troposphere) and bend where the refractive index changes with the [3] [7] [11].
- The index is measured with refractive N, which depends on pressure, temperature and above all [11] moisture.
- In normal atmosphere there is about 40 units per km; when it drops more than 157 units per km the wave remains trapped in a [7] [11] conduit.
- The conduit comes from a thermal inversion with dry air above and wet below: anticyclone, sea, serene nights, [3] [4] [5] [13] fronts.
- The higher the frequency, the thinner the duct is enough: the microwaves open more often than the 144 [3] [7].
- Without conduit remains the tropospher scatter: always present, weak, that falls with the distance [6].
- On the program: high pressure + reverse + Hepburn/ F5LEN high = probable conditions; the spots confirm [1] [2] [13] [14].

## 1. Where it happens: the troposphere

The troposphere is the lowest layer of the atmosphere, up to about 10–12 km. For the trope count mainly the first 3 km: there are water vapor and inversions, and there pass the radio routes between stations on the ground [4]. Under normal conditions the temperature drops with the altitude of about 6.5 °C per km [3]. When, on the other hand, in a layer, the temperature rises with the altitude, it is the starting point of almost all the strong tropo openings.

The VHFs are not reflected by the ionosphere as the HF (salve and sporadic, which has other hours and other distances [5]): beyond the horizon only come if the atmosphere bends them to the ground or spreads them.

## 2. Refraction and Refractive N

A radio wave bends through air with an air refractive index with another. The index n is very close to 1, so the reactivity N is used, i.e. n − 1 multiplied by a million [11]:

```math
N = (n - 1) \cdot 10^{6} = \frac{77,6}{T} \left( P + 4810 \, \frac{e}{T} \right)
```

with P the pressure in hPa, T the temperature in kelvin and the pressure of the water vapour in hPa. N soil is usually 300–400. The second term, that of steam, weighs a lot: for this reason the humidity counts as much as the [3] [11] temperature.

What bends the wave is not the value of N but how it changes with the quota, the gradient dN/dh:

| dN/dh (N/km) | Situation | Effect on the wave |
| 0 | Subrefraction | The wave moves away from the ground, the shorter horizon |
| from 0 to −79 | Normal (standard −39) | Light bending to the ground: radio horizon about 4/3 of the optical one |
| from −79 to −157 | Superrefraction | The wave bends more, increased flow rates |
| less than −157 | Content | The wave bends as much as the Earth or more and remains trapped |

The threshold of −157 N/km is the point where the curvature of the radius equals that of the Earth [7] [11]. Hence the "equivalent terrestrial beam" of 8497 km (4/3 of the real radius) used in the calculation of the [6] Scatter.

## 3. Inversions: the tropo motor

A gradient under −157 N/km requires N to collapse in a few meters of altitude. It happens when above a layer of fresh and humid air is found warmer and dry: the temperature rises and the steam falls together, and both bring down N [3] [5]. LA0BY indicates inversions up to 10–15 °C per km against the normal decrease of 6.5 °C [3].

Inversions arise in different ways:

- Subsidence in anticyclones. In the high pressure the air at the altitude drops slowly, it compresses and warms up, while on the ground remains fresh and humid air. It is the cause of the longer and longer openings, which last days [3] [4] [13].
- Nocturnal radiation. With serene sky and weak wind the soil cools at night and cools the contact air: Low reverse that breaks with the morning sun. Evening and morning openings, typical of summer and autumn.
- Sea warning. Hot and dry air coming from the ground over a colder sea: the sea layer remains humid and cool below. From here the great marine routes (Atlantic, Mediterranean, North Sea) [4] [13].
- Fronts. In front of a warm front the hot air flows over the cold one; the opening is short and follows the forehead.
- Evaporation. Above the sea, in the first meters, the humidity drops quickly with the altitude: very thin duct, useful only to microwaves.

The sign that a reverse is seen in the vertical profiles (radiosondaggi): temperature rising and dew point going down in the same section [5]. The program shows a clue of this with the Thermal Inversion layer (air at 925 hPa warmer than on the ground) and with the isothermal at 850 hPa.

## 4. Surface and high ducts

A conduit is a layer in which the wave travels by bouncing between the upper edge (inversion) and the ground or bottom edge [7]:

- Surface pipeline: ground or sea. The low-altitude stations, especially on the coast, enter directly.
- High pipeline: stands at altitude, for example between 500 and 2000 m. The stations in the plains may not feel, while those in the hill or in the mountain enter the conduit. Explain the links that cross the Alps or other [3] reliefs [4].

To enter the conduit the wave should start almost horizontal: low antennas on the horizon and no neighboring obstacle. Inside the conduit the attenuation is much lower than in the free space, and the distances reach 1000–2000 km and beyond (for example the connections between the Iberian Peninsula, the Canary Islands and England, or between Scandinavia and southern Russia) [3].

## 5. Frequency: why microwaves open more often

A conduit only traps quite short waves compared to its thickness. The long waves “come out” from a thin duct; The short ones don't. Then [3] [7]:

| Band | Wavelength | What you need |
| 144 MHz |  2 m  | Thick pipelines (centinaia of meters), wide and well marked versions: less frequent but very extensive openings |
| 432 MHz | 70 cm | Thin edges: often opens when 144 are only “goods” |
| 1296 MHz | 23 cm | Few weak inversions; long routes on the sea frequent in summer |
| 10 GHz | 3 cm | Just the evaporation duct on the sea; also counts the absorption of rain |

For this reason a modest tropo index can already mean openings in UHF and microwave, while the 144 call for higher values. On the other hand, at the same conduit, the 144 have less losses outside the conduit and better cover the border areas.

## 6. The Troposfer scatter

Even without inversion the troposphere is not uniform: Small temperature and humidity irregularities spread a small part of the energy beyond the horizon. It is the troposferic scatter, always present, which allows connections of 300–800 km with good [6] stations. The signal is weak and fluctuating; the loss grows with the angle of diffusion, that is with the distance and with the obstacles on the horizon:

```math
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
```

with θs the diffusion angle in degrees and f in MHz [6]. The program uses this formula in the tab of each spot to tell whether a link can be explained with the scatter or requires a conduit (see document 1). A high N value on the ground improves the [6] scatter.

Not to be confused with the airscatter (reflection on planes: signals of a few seconds, Doppler) and the scatter meteor (reflection on meteors), which the program excludes from the tropo.

## 7. The weather picture: how to read it on the map

The tropo openings follow the meteorology. Typical situations [3] [4] [13]:

- Stable anticyclone, high pressure (more than 1020–1025 hPa), wide isobare, little wind: subsidence reversal. The best openings follow the edge of the anticyclone, especially on the sea.
- Calo della pressione: inversion breaks. G4MCU followed the SK6VHF beacon until pressure drops to 1012 hPa [13].
- Hot air at altitude (isother to 850 hPa rising) over fresh air on the ground: reverse.
- Convective cells and gravity waves (e.g. underwind in the Alps) can create local and short openings [4].
- Time of the day: inversions from irradiation are stronger in the evening and early in the morning; the subsidence remains also of day.

The Hepburn [1] and F5LEN [2] indices put all this together from weather models: They calculate N at various quotas and transform gradients into a number or a class. I am not a guarantee: LA0BY shows an LA– UA6 event that is visible in high temperature and not in Hepburn [3]. For this the program shows together indexes, isobare, temperatures and real spots: if the spots align to a high index area under an anticyclone, the opening is in progress. KP4MD found the SNR of WSPR reports to 144 MHz growing right in the windows provided by Hepburn [14].

## 8. Practical list

1. Look at the tropo index on your area and along the direction that interests you, for the next few hours.
2. Check isobare: high pressure and wide isobare over or next to the path.
3. Turn on Thermal Inversion and T 850 hPa: hot air at altitude above the path.
4. Prefer routes on the sea and evening and morning hours.
5. If you are on the hill, think of the high ducts; If you're on the coast, on the surface.
6. Try first the highest bands (432, 1296): open first.
7. Verify with spot and PSKReporter: long lines in the same direction confirm the duct.

## Sources

1. W. R. Hepburn, DX Info Centre, “The Hepburn Tropo Index (HTI)” and “Tropospheric Ducting Forecast” pages (dxinfocentre.com).
2. F5LEN, forecast for Europe and world prediction of VHF tropopheric propagation (tropo.f5len.org).
3. S. Heck LA0BY, “Extreme Tropo Propagation on 144 MHz and up”, 41° Nordic VUSHF Meeting, Skjeberg 2019. Normal gradient and inversions, temperature and humidity, high pressures, LA– UA6 event, connections beyond the mountains.
4. F. Egano IK3XTV, “Propagation VHF – The contribution of convective cells and atmospheric gravitational waves in the Tropo propagation.” First 3 km, marine paths, convective cells, gravity waves, high pressure.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, “Very Long Distance Propagation in the 144 MHz Band”, 24/7/2003. Temperature and dew point profiles, inversions, distinction from E sporadic.
6. J. N. Gannaway G3YGF, “Tropospheric Scatter Propagation”, QST November 1983. Earth radius equivalent and scatter loss.
7. The VHF/UHF DX Book, chapter 2 “Tropospheric propagation”. Refractive zones according to dN/dh and surface and high duct profiles (only the figures).
11. Recommendation ITU-R P.453, radio refractive index. N formula and duct threshold at −157 N/km. General reference, not annexed.
13. G4MCU, “tropospheric” articles (g4mcu.net), 2018–2025. Condotti with high pressure, especially on the sea; SK6VHF and decrease to 1012 hPa.
14. C. F. Milazzo KP4MD, “VHF/UHF WSPR Propagation Study”, 2012–2014. SNR WSPR at 144 MHz and Hepburn forecast.

Source numbers are the same as document 2, Map Building.
