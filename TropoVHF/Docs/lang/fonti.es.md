# Fuentes y fórmulas

> Traducción automática por italiano: en caso de duda vale la pena la guía italiana.

Reunión de trabajo de fuentes consultadas para el pronóstico del tropo LIVETROPOVHF (20–21 de noviembre de 2026): Física básica, datos disponibles, índices publicados, fórmulas, experiencias de radio amateur, validación y decisiones. Las fórmulas están numeradas como en LaTeX, (1), (2),..., y en el texto que recuerdan con el número entre corchetes. El Manual de Predicción describe cómo el programa los utiliza.


## 1. Locales del autor

Observaciones IC8TEM que enmarcan el trabajo, con comentarios encontrados en fuentes.

- La geografía italiana exige la difusión. Con crestas, promontorios e islas a unas docenas de kilómetros de las estaciones, las contribuciones de difracción deben calcularse con el parámetro Fresnel en el borde principal, no sólo con la tierra lisa. Encuentro: en una comparación de terreno irregular en 563 MHz (Rio de Janeiro, 27 puntos entre 5 y 30 km), los mejores métodos fueron P.526 con filos de hoja, P.1812 y la cuña difctiva, con error promedio entre −5 y +3 dB y dispersión de alrededor de 9 dB; P.1546 fue el más pesimista (desde Costa, Rabanal Ramirez y Alope s
- Para entrar en un conducto distante, necesitas Fresnel. Si el conducto comienza más allá del horizonte, la señal viene a nosotros por difracción sobre los obstáculos hasta su comienzo; a partir de 1.3.2 el programa lo calcula (Manual de Previsión, § 10 ter). Reunión: Rohde & Schwarz (2023) señala que las estaciones a lo largo de un conducto alto a menudo no entran, mientras que las cercanas a los extremos sí.
- Las condiciones cambian rápidamente. Las teorías están allí, pero las condiciones cambian en minutos, y ningún sistema de pronóstico toma los datos en tiempo real: el GFS sale cada 6 horas, con el tiempo y 0.25°, y un pronóstico puede perder valor dentro de un corto tiempo. Reunión: Bray (QST, noviembre de 1961) medición de las fluctuaciones medianas por hora de aproximadamente ±7 dB sobre 100 millas; los conductos se mueven con tiempo atmosférico (Rohde & Schwarz); nuestro registro QSO de 29/12/2021–2/1/2022 muestra aperturas creciendo y cayendo en horas.
- La mejor transmisión ahora sigue siendo la red DX cluster–PSKReporter. Para saber lo que pasa ahora, los puntos de racimo, PSKReporter (y WSPR) vencieron cualquier patrón: son medidas. El pronóstico dice dónde mirar y cuánto tiempo puede durar; los comerciales dicen si hay. El programa para esto pone punto y pronóstico en el mismo mapa.

## 2. Propósito

La idea es un tercer índice, calculado por LIVETROPOVHF a partir de los perfiles verticales de un modelo meteorológico, que se añadirá a Hepburn y F5LEN en lugar de reemplazarlos. Hoy en día el programa lee sus mapas ya listos: un índice también diría *por qué* se está abriendo (por ejemplo, el espesor del conducto, la banda mínima atrapada) y podría ocurrir con los puntos que el programa ya recoge.

- Lo que puede dar: probabilidad de conducto por punto y hora, parte de la base y superior, espesor, frecuencia mínima atrapada, y a lo largo de un camino la fracción de la ruta dentro de un conducto.
- Lo que no puede dar: los conductos delgados (bajo unos diez metros) que los modelos globales a 0,25° no resuelven, y los conductos de evaporación en el mar, que sirven sobre todo del GHz arriba.
- Primer objetivo práctico: 144 y 432 MHz en Europa, con verificación en puntos de agrupación DX, PSKReporter y decodificaciones locales.

## 3. Física básica

Todo empieza por refractivo N calculado a cada nivel de perfil: el conducto es donde la reactividad modificada M disminuye la escalada, es decir, donde dN/dh cae por debajo −157 N/km ([ITU-R P.453-13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)).

Refractividad (P y en hPa, T en kelvin):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Presión de vapor por humedad relativa H (%) y temperatura t (°C), por encima del agua entre −40 y +50 °C:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

Reactividad modificada (h en km, o 0.157 · z con z en metros) y criterio del conducto:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN/dh (N/km) | Reglamento | Efecto de 144 MHz |
| 0 | subrefracción | reducción de la tasa de flujo |
| de 0 a 179 | normal (standard about −40) | radio horizonte con radio equivalente 4/3 |
| de 79 a 157 | super-refracción | flujo alargado, "demasiado bueno" |
| −15 | conduit (trapping) | señales atrapadas, DX más de 1000 km |

Cómo extraer N de un modelo a niveles de presión: para cada nivel toma T, humedad relativa y altura geopotencial, calcula N y M, luego el gradiente entre los niveles vecinos. Los niveles debajo del suelo deben ser descartados (a 1000 hPa en los Alpes es subterráneo).

Frecuencia mínima atrapada: el conducto guía sólo las ondas bastante cortas en comparación con su espesor. Hepburn publica un grosor de mesa → frecuencia mínima útil ([DX Info Centre](https://www.dxinfocentre.com/propagation/luf.htm)); es un límite suave, bajo el cual la señal disminuye gradualmente.

| Led Thickness | Frecuencia mínima pagadera |
|  61 m  | 962 MHz |
|  91 m  | 464 MHz |
|  122 m  | 276 MHz |
|  152 m  | 185 MHz |
|  183 m  | 133 MHz |
|  213 m  | 101 MHz |
|  305 m  | 53 MHz |

En la gama VHF (a menudo 120–300 m) los valores de tabla siguen muy bien una ley sobre constante; por debajo de 100 m la fórmula subestima la frecuencia hasta 30–40%. Adaptarlo yo mismo (no una fórmula publicada por Hepburn), con f en MHz y d en metros:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

En la práctica: para los 2 m, un conducto es a menudo por lo menos 170–180 m, para los 70 cm es aproximadamente la mitad. La fórmula que circula sobre la red, λmax = 0.014 · d^1.5, viene de Kerr (1951, Capítulo 1, eq. 18) y es válida con λ en centímetros y de pie, para un conducto en el que M cae linealmente: con estas unidades da alrededor de 2 m 180 m y por unos 70 cm 89 m, que son los mismos valores de la tabla Hepburn, que viene de ella. Kerr advierte que no es un corte neto: incluso las olas varias veces más sufren mucho del conducto, y una manera que "perder" un poco puede mejorar la recepción incluso con estaciones fuera del conducto. Un estudio de IEEE Access 2019 confirma que los métodos clásicos (Kerr, Kukushkin) no describen el corte real ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)): el umbral debe ser utilizado como una indicación, no como una pared.

## 4. Datos disponibles de forma gratuita

El punto débil es resolución vertical: entre 1000 y 850 hPa modelos dar un nivel cada 25–50 hPa, que es cada 200–450 m, por lo que ven bien las inversiones a 500–2000 m pero no los conductos delgados cerca del suelo. LIVETROPOVHF ya utiliza Open-Meteo para isobare, temperatura a 850 hPa e inversión: el índice puede comenzar desde las mismas llamadas.

| Fuente | ¿Qué pasa? | Resolución | Uso del índice |
| [Open-Meteo, GFS](https://open-meteo.com/en/docs/gfs-api) | T, humedad relativa, punto de rocío, altura geopotencial por nivel (1000, 975, 950, 925, 900, 850 hPa...); altura de la capa límite | 0.25° sobre niveles de presión; 16 días, horas hasta 120 h, actualizado cada 6 h | perfil N y M, gradiente, base y parte superior del conducto; ya en el programa |
| [Open-Meteo, ECMWF](https://open-meteo.com/en/docs/ecmwf-api) | los mismos niveles de los modelos de datos abiertos 0.25°; el suelo da punto de rocío |  0,25°  | segunda opinión, útil para la concordancia entre modelos |
| [Open-Meteo, DWD ICON](https://open-meteo.com/en/docs/dwd-api) | T y humedad relativa en los niveles de presión; en ICON- D2 hay 800, 900 y 925 hPa | ICON-EU unos 7 km | tercera opinión sobre Europa |
| [Radiosunds, Univ. Wyoming](https://github.com/NCAR/RAOBget) | perfiles medidos, dos lanzamientos por día (00 y 12 UTC), formato de texto por estación | muchos niveles significativos, incluso cada docena de metros | verificación del índice; ver los conductos delgados que el modelo no ve |
| [GROANS, NOAA NCEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive) | archivo histórico de la radiodifusión, archivo por estación, parámetros más derivados | arriba | calibración en años de datos y comparación con puntos anteriores |
| ERA5 (Copernicus) | Reanálisis oral desde 1940, 37 niveles de presión | unos 28 km | estadística y calibración, no pronóstico |

- Con altura geopotencial Open-Meteo está por encima del nivel del mar: los niveles que caen bajo el suelo deben ser descartados ([Open-Meteo, aire superior](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)).
- En el suelo se añade T y humedad a 2 m: dan el primer tramo de gradiente, que donde nacen los conductos de superficie nocturna en la llanura y en el mar.
- Transmisiones italianas en el archivo de la Universidad de Wyoming (weather.uwyo.edu/upperair/sounding.html): estaciones y números OMM en la tabla de abajo.

| Estación | W | Latitud | Longitud | Compartir |
| Udine/Campoformido |  16044  |  46,03  |  13,18  |  94 m  |
| Milan/Linate |  16080  |  45,43  |  9,28  |  103 m  |
| Práctica del Mar |  16245  |  41,65  |  12,43  |  12 m  |
| Brindisi |  16320  |  40,65  |  17,95  |  10 m  |
| Trapani/Birgi |  16429  |  37,92  |  12,50  | unos 14 m |

El formato de texto de Wyoming da para cada nivel de presión, altura, temperatura, punto de rocío y humedad: suficiente para N y M. Wyoming no tiene una interfaz oficial, los scripts se utilizan como [RAOBget de NCAR](https://github.com/NCAR/RAOBget); el Colegio de DuPage mantiene copias en texto por estación (por ejemplo, 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)).](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. Índices y métodos publicados

Hepburn y F5LEN comienzan desde los mismos ingredientes físicos pero no publican el algoritmo completo; los estudios científicos de 2008–2026 describen el método paso a paso, y es de ahí que vale la pena copiar la receta.

| Método | Datos de inicio | Qué se calcula | Notas útiles para nosotros |
| [Indice Hepburn Tropo](https://www.dxinfocentre.com/propagation/hti.htm) | modelos meteorológicos de Canadá (MSC, como está escrito en sus mapas; no GFS/NAM), mapas diseñados con GrADS, algoritmo propietario ([storia](https://www.dxinfocentre.com/propagation/history.htm)) | 0–10 escala lineal de curvatura de señal: 0 normal, 1–2 super-refracción, 3–4 algunos conductos, 5–6 conductos difundidos; valores negativos = subrefracción | la escala a imitar, por lo que el tercer índice se compara inmediatamente con los dos primeros |
| [F5LEN, pronóstico mundial](https://tropo.f5len.org/WW/info.php) | reactividad en la unidad N; para Europa el modelo ARPEGE de Météo-France, 7,5 km, más niveles entre 2 m y 1500 m ([nota por autor](https://tropo.f5len.org/author/f5len/)) | Índice promedio para el propietario, 40 pasos de 3 horas, 4 cálculos por día, 28 días de archivo ([nueva versión](https://tropo.f5len.org/?p=504)) | confirmar que sirve un modelo con muchos niveles bajos |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf) | 60 niveles de modelo ECMWF, desde tierra hasta 3000 m | mínimo de dN/dz por columna: realizado bajo −157 N/km, super-refracción bajo −78.7 N/km; base de la capa de trampa | climatología: Padana Enmarca el área europea con más oleoductos durante todo el año; Mediterráneo en verano cerca del 100%, en invierno bajo 15%; Italia sobre el 30% en noches de verano |
| [Indermuehle y Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861) | GFS 0,25°, 21 niveles de presión + campos a 2 m | M = N + 0,157 z; mínimo dM/dz inferior a 3 km, base y parte superior del conducto, profundidad del déficit M; menos de 40 m desechado | verificada por 7 años en 25 canales de 88 MHz a 2,68 GHz; buena capacidad hasta cerca de 3 días de antelación (AUC alrededor de 0,74), luego cae; GFS mejor que ERA5 |
| [Hämäläinen et al., FMI 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf) | Armonie-AROME, 750 m o 2,5 km, 65 o 90 niveles | dM/dh 0 en el perfil del modelo, comparación con un haz de 4 a 59 m | cuenta más vertical que resolución horizontal; ningún modelo ve los conductos inferiores a 15 m; la humedad domina en el gradiente de temperatura |

Tercer servicio de radio aficionado ya activo: [TROPOCAST.EU](https://tropocast.eu/)de Artur SQ3A, por 2 m y 70 cm en Europa. De la página de pronóstico ([tropocast.eu/forecast](https://tropocast.eu/forecast)):

- Datos: DWD ICON-EU (pass 0.0625°, aproximadamente 7 km), fuente predeterminada; alternativamente el GDPS canadiense (0.15°), menos detallado.
- Método: N y M de acuerdo con ITU-R P.453 del perfil entre la capa cerca del suelo y 850 hPa; el reconocimiento de las capas, la unión de los conductos al suelo y alto y los umbrales de los colores son una calibración adecuada del autor, no publicado.
- Productos: Vista "completa" 0-3000 m, vistas más separadas "al suelo" y "alto" (por ahora sólo en mapas a incorporar); puntuación 0–100 en nueve clases (bajo 38 fondo, 38–52 marginal, 53–58 débil, 59–65 moderado, 66–72 bueno, 73–80 muy bueno, 81–88 alto, 89–95 fuerte, 96–100 muy fuerte); pronóstico hasta +72 h dentro de un archivo de hora.
- Áreas: Europa, Polonia, Reino Unido, Alemania, Francia, España, Italia, República Checa, Croacia; interfaz también en italiano.
- Reutilización: los mapas incorporan con un iframe sin clave API; también hay una API documentada (Schema OpenAPI y JSON). La licencia no está indicada: Antes de utilizarlo como una tercera fuente junto a Hepburn y F5LEN debe preguntar al autor.

Diferencias de nuestro proyecto: TROPOCAST para a las 850 hPa (aproximadamente 1,5 km), mientras que los conductos elevados llegan a los 2000 m y más allá ( LA0BY caso de 22/11/2018), y no da un índice para banda o ruta. Las clases 0–100 se pueden comparar con nuestro índice 0–10 en los mismos días.

Estructura de imagen TROPOCAST (del enlace proporcionado por el autor del programa): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, donde n es el paso de pronóstico. El sitio prohíbe la selección automática de robots, por lo que no pude leer la imagen; también por esto, antes de mostrar estos mapas en LIVETROPOVHF sirve el permiso del autor.

Cuaderno: [DJ5AR, categoría](https://dj5ar.de/?cat=174)(Magonza, 21 artículos 2017–2023, 2 m–13 cm). Casos descritos: llevado a cabo varias semanas de Irlanda (23 de mayo a 2 de junio de 2023), FT8 a Belarús (septiembre de 2020), Italia–Alemania sobre los Alpes y el Adriático (agosto de 2017). Reconozca el caso del conducto "demasiado alto" (reverso a 1000 m) que no puede utilizar desde el suelo: otra confirmación de que es necesario comparar la parte del conducto con la de las estaciones. Utiliza radiofármacos como medida de apertura (hasta 1028 km) y perfiles verticales en París, Burdeos y DWD.

En resumen, la receta común: perfil vertical → N y M por nivel → capa con dM/dh negativo → base, superior, espesor y fuerza del ducto → índice. La parte "secreta" de Hepburn es la calibración empírica en eventos reales, que podemos rehacer con los puntos.

## 6. Formulas útiles para el programa

Además de N y M (sección Física Básica), estos sirven para pasar de vertical a punto número y luego por camino. 9. Las fórmulas de la UIT habían sido reconstruidas por un PDF leído en parte mal; el 22 de noviembre se revisaron en textos oficiales ([ITU-R P.452-18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453-14, P.526-16, P.372-18) y se corrigieron en el código: constante de la troposcatter 190 (no 190,1) más la absorción de gases con ρ = 3 g/m3,4; Rumore P.372-18: tabla 1 y galáctica corresponden; curvas de ruido artificial son de hasta 250 MHz y la galáctica hasta cerca de 100 MHz, sobre el programa extiende las mismas leyes.

1. Gradiente entre dos niveles del modelo (h en km, M en unidad M):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. Fortaleza y espesor del conducto: espesor d = superior − base (para un conducto de suelo, base = suelo); fuerza como deficiencia M entre base y parte superior.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3. Factor equivalente al radio terrestre (P.452-18, eq. 5 y 6a), con ΔN = descenso promedio N en el primer km, positivo, en N/km:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

Con ΔN = 40 tienes alrededor de 4/3 k (ambiente estándar); con ΔN que se acerca 157 k tiende a la infinidad, es decir, la radio terrestre se vuelve plana: es el comienzo del conducto.

4. horizonte de radio de una alta antena h metros, con radio equivalente a\_e en km:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5. Probability of duct for latitud (P.452-18, eq. 2), percentage of time; φ = latitud del centro de la ruta, μ1 y μ4 tienen en cuenta hasta qué punto está por mar:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

A 41° N, todo por mar (μ1 = μ4 = 1), da alrededor del 11% del tiempo: es la base climática, útil como un valor de referencia del cual el índice esperado difiere.

6. Pérdida para troposcatter (P.452-18, eq. 45; f en MHz, d en km, θ ángulo del camino en mrad, N0 reactividad terrestre en el centro de la ruta):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

El término −0,15 · N0 muestra por qué el aire húmedo también ayuda sin conducto: 50 unidades adicionales N son 7,5 dB menos que la pérdida. LIVETROPOVHF ya utiliza la versión G3YGF de la pérdida de dispersión.

7. Pérdida en conducto (P.452-18, eq. 46–51): pérdida fija de acoplamiento más una atenuación para el milliradiant de ángulo, que crece con la raíz cúbica de frecuencia (f en GHz, a\_e en km):

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. De Bean y Dutton (1966) y Kerr (1951)

Los dos textos clásicos dan tres cosas que se necesitan inmediatamente al índice: los umbrales gradientes ya se asientan en los mismos niveles de presión de GFS, lo que vale en dB un cambio de reactividad, y qué situaciones meteorológicas crean o destruyen los conductos. *Radio Meteorología*, NBS Monograph 92.

1. Clasificación de perfiles para capas de presión (Bean y Dutton, tab. 6.5, p. 248; −dN/dh en N/km). Diseñado para longitudes de onda de radio, se aplica como está en los niveles de modelo:

| Road | Acciones sobre | Subrefracción | No laminado | Superrefracción |
| 1000–850 hPa | 0 a 1,46 km | −d−N/dh | de 20 a 60 | más de 100 |
| 850–700 hPa | 1,46–3,01 km | −d−N/dh | de 20 a 50 | más de 80 |
| 700–600 hPa | 3,01 a 4,20 km | −d−N/dh | de 15 a 40 | más de 70 |
| 600–500 hPa | 4,20-5,57 km | −d−N/dh | de 10 a 30 | más de 50 |

2. ¿Cuánto vale en dB (Bean y Dutton, cap. 6, págs. 233 a 258), medidas de 70 a 180 MHz en rutas más allá del horizonte:

- Cualquier capa (en el suelo o a la altitud) en comparación con el perfil "smooth" eleva la señal de 10–25 dB a todos los porcentajes; la mayor ganancia proviene de las capas inclinadas altas, probablemente debido al efecto focal (curso de 200 km).
- La reactividad terrestre Los Ns y el descenso en el primer km ΔN están relacionados con la señal mediana mensual con coeficiente entre 0.4 y 0,95, mediana 0,7 (20 rutas desde 130 a 446 km, aproximadamente 100 MHz).
- Ciclo anual: 0.18 dB por unidad N; ciclo diario de 0,2 a 1,1 dB por unidad N, máximo en rutas de 175–200 km e invierno.
- El parámetro compuesto de Misme estima la variación de campo como 0.5 dB/(N/km) · (equivallent gradient − 40) más un término de estabilidad térmica; el parámetro de turbulencia D (rea entre el perfil real y el adiabático, 0–3 km) sólo mejora en comparación con el simple ΔN. Conclusión de los autores: ΔN sigue siendo el mejor parámetro para la simplicidad.

3. Perfil de referencia y anomalía (Bean y Dutton, § 3.8 y 3.9). El ambiente exponencial de referencia CRPL da el perfil "normal" a partir de Ns; la unidad A mide cuánto el perfil real difiere en cada parte. Es la forma natural de transformar un perfil GFS en una anomalía para poner en el mapa:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

Con Ns = 313 usted tiene ΔN aproximadamente −42 N/km, que es el radio equivalente 4/3. El criterio del conducto −157 N/km también se deriva aquí de la ley Snell para un radio horizontal (eq. 4.23), casi independiente de la altura de la antena.

4. Donde y cuándo nacen los conductos (Bean y Dutton § 4.4; Kerr cap. 3):

| Proceso | Situación | Efecto |
| Advertencia | aire caliente y seco de la tierra que fluye en el mar más frío (típico de manga de verano después de días de buen tiempo); principalmente cuenta el déficit de humedad | tubo intenso en el mar |
| Radiación nocturna | cielo sereno, viento débil; nubes bajas o mucha humedad lo previene | llevado al suelo que nace al atardecer, crece por la noche y desaparece rápidamente después del amanecer, especialmente dentro |
| Subvenciones | aire que desciende del anticiclón, calentado y seco, sobre aire fresco y húmedo; inundaciones frecuentes en las montañas y sectores sureste de anticiclón | capa super-refractiva o conducto alto; casi siempre gradiente más alto que estándar en inverso |
| Mar y tierra | costa, circulación de unos pocos cientos de metros | tuberías locales a lo largo de la costa |
| Contra el conducto | mezcla: suelo calentado por el sol entre la mañana y la tarde, viento al suelo sobre unos pocos m/s en terreno áspero; niebla nocturna | probabilidad de conducción casi nada |

5. Dos nuevos ingredientes para probar. El movimiento vertical: las corrientes ascendentes en las zonas de convergencia eliminan y borran las capas, la subsistencia fortalece la señal (Moler y Holden: en promedio alrededor de 2,5 dB más que con el aire cuesta arriba, en el mar en la banda L y X). Open-Meteo ya proporciona velocidad vertical en los niveles de presión. Y la tendencia de presión: en un camino de 140 km a 186 MHz la señal descendió cuando la presión cayó al transmisor y al centro del camino y regresó alto y sin evanescencia cuando subió, con sólo 2 (Bean y Dutton, fig. 6.9, p. 241).

6. De Kerr. Además de la fórmula de la longitud máxima de onda (sección física básica), Kerr da una fórmula aproximada para M de la única temperatura, vapor y altitud (§ 3.9, con las viejas 79 y 4800 constantes luego sustituidas por 77,6 y 4810) y observa que al revés el corte del viento puede generar ondas que oscilan a altitud el conducto, posible causa de escapes profundos (§ 3.26). Fuente italiana reportada por Bean y Dutton y buscar: L. Bonavoglia (diciembre de 1958), "Correlación entre fenómenos meteorológicos y propagación..."

## 8. De las presentaciones de radio amateur e informes técnicos (segunda serie)

Siete documentos proporcionados por el autor del programa. Tres sirven directamente al índice: W3SZ para el troposcatter "floor", LA0BY para casos que Hepburn no había predicho, Hepburn (traducción LU7MC) para la clasificación meteorológica de los conductos.

1. Troposcatter: la señal que siempre existe. R. Rehr W3SZ, "Tropospheric Scatter (Troposcatter) Propagation for VHF, UHF, Microwave and Frequencies", Pack Rats Conference 2014; J. R. Duffey KK6MC, "The Troposcatter Propagation Mode and How to Predict Coverage on the VHF/UHF Bands", CSVHF 2017.

- Útil de aproximadamente 100 a 700 km, de 144 MHz a 10 GHz; la pérdida crece de 30 troncos f con frecuencia.
- Sobre todo, cuenta el ángulo de inicio hacia el horizonte: cada grado extra cuesta alrededor de 9–12 dB ( M1CNK, método Collins).
- En verano la señal es en promedio alrededor de 10 dB más alto que en invierno; a media tarde alrededor de 5 dB más baja que en la mañana temprano o en la noche. KK6MC indica una variabilidad total de 13–15 dB con tiempo, temporada y tiempo.
- método Yeh, el más fácil de programar (f en MHz, d en millas, θ ángulo de dispersión en grados, α promedio geométrico de las aberturas de los dos paquetes):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Comparación de Larsen (1968) en 15 conexiones reales: error cuadrado promedio 5,1 dB para Collins, 7,4 para Yeh, 8.1 para NBS y CCIR, 16,8 para Rider. Todos los métodos mejoran si Ns se mantiene estable (Yeh cae a 5.4 dB): para la dispersión el refractivo del día cuenta poco, por lo que el índice tropo debe mirar las capas, no sólo Ns. La tabla de 15 conexiones, con pérdidas medidas (incluyendo Feldberg-Monte Baldo, 322 km a 957 MHz, 223,1 dB; Chipre–Israel, 319 km a 77,5 MHz, 163,3 dB), es un buen banco de pruebas para nuestro cálculo.
- KK6MC da la pérdida de que una estación típica puede exceder en 2 m y la distancia correspondiente, de 184 dB (FT-817 y 3 elementos en SSB, unos 130 km) a 215 dB (angos largos en SSB, unos 430 km) y 236 dB (kW y yagi acoplados, unos 640 km). Es la manera de traducir el índice dBs en millas ganadas.
- Código listo: W3SZ Yeh y Collins programa (nitehawk.com/w3sz/YehCollins.zip) y Bob Atkins KA1GT calculadora (bobatkins.com/radio/troposcatter.html).

Otra fuente en el scatter: I. Roberts ZS6BTE, "Propagación de estafadoras troposféricas", *Radio ZS*, diciembre de 1984, con nota de 2013 ([qsl.net/zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). Principalmente Geometría útil: ángulo de dispersión θ = θ0 − θ1 − θ2, con θ0 = d/R, θ1 = (h1 − h\_ostacolo)/d1 + d1/2 R y lo mismo para la otra estación, R = 8446 km (4/3 del radio de la Tierra). Es la pieza que se utiliza para calcular los ángulos desde el perfil del suelo en el troposcatter "floor". Otros datos: cada ángulo en más costos alrededor de 10 dB; pasando de Ns 280 a 300 reduce la pérdida de alrededor de 4 dB; el volumen de dispersión es de entre 300 y 2000 m por 150 km, entre 600 y 3000 m por 300 km; el tiempo en el volumen de dispersión aumenta la señal de alrededor de 20 dB. el ejemplo numérico del artículo no vuelve con las fórmulas dadas (el término de espacio libre a 900 MHz y 872 km) W3SZ

2. Tipos de conducto (W. R. Hepburn, "Tropospheric Propagation Modes", dxinfocentre.com/propagation/tr-modes.htm, traducción española de LU7MC). Completar la tabla de Bean y Dutton con casos útiles en Italia:

| Tipo | Cuando | Nota para el índice |
| Irradiation | noches tranquilas y pacíficas en la tierra | seguir el suelo, desaparece poco después del amanecer |
| Alta presión (suficiencia) | anticiclón | puede durar todo el día; por la noche el conducto de irradiación abajo puede bloquear señales distantes, por lo que el día puede ir mejor |
| Frente | frente a un frente cálido, detrás de un frente frío, al norte de un frente estacionario | corto; el mal tiempo le perturba |
| Bajada de montaña | Föhn, bora: aire bajando, calentando y secando | para tratar de bora en el Adriático y Föhn al sur de los Alpes |
| Advertencia | aire caliente y seco sobre suelo frío y húmedo, por ejemplo después de la lluvia | llevado al suelo |
| Valle | aire caliente sobre las paradas de aire frío en el valle | puede permanecer en el día |
| Marino | aire caliente y seco en agua más fría; llega a la costa con la brisa | pico por la tarde; primavera y verano temprano las mejores temporadas |

Los conductos de evaporación en el mar (0–30 m) sirven sólo alrededor de 3 GHz y no entran en el interior: para 144 y 432 MHz se puede ignorar. Las montañas altas son una barrera; los desiertos son demasiado secos.

3. Los casos Hepburn no predijeron (S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", Nordic VUSHF 2019, de JO59IX a 500 m de altitud):

- 22 noviembre 2018: 70 cm hasta UA6 (unos 2600 km) durante 6-8 horas. Hepburn indicó sólo los Estados Bálticos y Polonia. El conducto fue visto en los mapas de temperatura a 900 y 1500 m y terminó justo encima de 2000 m; fue más alto en el lado noruego, y sólo LA0BY en la colina entró. En el tiempo de tierra no parecía un trope: frío y niebla en la parte superior.
- 11 febrero 2015: 2 m y 70 cm hacia 9A y S5 (unos 1500 km) a través de los Alpes, también esto no previsto por Hepburn.
- 15 October 2011: 2 m with EA1 (2135 km) in high duct, on three stretches of sea and three ground.
- Otros acontecimientos: 6 a 11 de noviembre de 2003 al este (2050 km), 10 de diciembre de 2004 a Francia.
- Dirección de campo: niebla en picos planos, libres, viento pequeño, suelo húmedo. Gradiente de temperatura normal −6,5 °C/km; en un caso real ( VK3KAQ) +7 °C en 20 m.

Conclusión del autor: Hepburn y F5LEN no son suficientes, sirven los perfiles a altitud a lo largo de la ruta. Para nosotros: estas fechas son casos de verificación que se recalculan con los datos históricos de los modelos, y el índice en el camino debe comparar la cuota del conducto con la cuota de las estaciones.

4. Experiencia italiana (G. IV3GBO, "Tipos de propagación VHF SSB/CW", 2017): realizada entre 450 y 3000 km, con capas entre 450 y 3000 m; niebla extendida como pista; aberturas por mar en el Adriático hacia Puglia, Albania y Grecia. De 100 a 500 km.

5. Introducción (P. Wilton M1CNK, "Introducción a la Propagación VHF/UHF", 2013): 70 cm son a menudo mejores que 2 m; con un poste de 10 m a 144 MHz el ángulo de inicio es de unos 3°, con 20 m aproximadamente 1,5°.

6. Informe de la NASA (E. Hirschmann, "Tropospheric and Ionospheric Effects upon Radio Frequency (VHF-SHF) Communication", Goddard X-731-67-89, 1967): se refiere a errores de distancia a satélites, no al DX. Devuelve la misma fórmula N y el perfil exponencial (por ejemplo, en Florida: Ns 370, ΔN −55, constante 0.161/km) y envía dos fuentes NBS a buscar: Bean y Thayer, "CRPL Exponential Reference Atmosphere", Monograph 4 (1959), y Bean, Horn y Ozanich, "Climatic Charts and Data of Radio Refractive World Index for the United States

## 9. Del Libro VHF/UHF DX, Capítulo 2

I. White G3SEK (editado por), *The VHF/UHF DX Book*, DIR Publishing 1995 (ISBN 0 9520468 0 6; réplica digital 2017), capítulo 2 "VHF/UHF Propagation", pp. 2-13, 2-22-2-29 y 4-11. Es el texto más cercano a la experiencia de radioaficionado entre los recogidos; confirma los umbrales ya utilizados y añade nuevas reglas sinópticas.

Número

- La misma fórmula de N (77.6 P/T + 3.73·10^5 e/T2); ejemplo: 1000 hPa, 7 °C, y = 10 hPa damage N = 324. En un ambiente bien formado, por ejemplo después de una tormenta, N es casi lineal alrededor de 39 unidades/km en los primeros 2-3 km.
- Los gradientes entre 0 y −79 N/km son normales, debajo de −79 hay super-refracción, sobre 0 sub-refracción. El libro insiste en que el largo DX no viene de la simple super-refracción, que debe permanecer justo en todo el camino, sino de los conductos.
- Espesor mínimo del conducto, un poco más prudente que Kerr y Hepburn:

| Banda | Espesor mínimo |
| 50 MHz |  400 m  |
| 70 MHz |  300 m  |
| 144 MHz |  200 m  |
| 432 MHz |  100 m  |
| 1.3 GHz |  50 m  |

- La relación no es proporcional a la longitud de onda (aproximadamente 65 λ a 50 MHz, 220 λ a 1.3 GHz) porque los bordes del conducto son "split". En el Reino Unido los oleoductos son frecuentes en 144 MHz pero casi nunca a 70 MHz o debajo, y a menudo los 432 MHz van fuertes cuando los 144 son débiles.
- En tubería las gotas de señal como 1/d en lugar de 1/d2: hasta 1000 km la atenuación puede ser similar a la del espacio libre.
- Capacidad de una estación mínima de 2 m (100 W, 15 dBi yagi, 2 dB ruido): 217 dB, que es de unos 400 a 500 km en SSB en condiciones promedio; en CW unos 20 dB en más (p. 4-7). De acuerdo con la mesa KK6MC.
- Scatter: la pérdida se eleva alrededor de 15 dB por triple la frecuencia y 10 dB por cada grado de ángulo de dispersión (fórmula de Yeh; en el texto extraído la constante es imposible de leer, por lo que debe ser tomado de la versión de W3SZ). La dispersión "en marcha" de células convectivas cubre rutinariamente hasta unos 500 km, 1000 km con buenas estaciones. Después de lluvias pesadas y condiciones extendidas son escasas en todas las bandas.

Tipos de conducto

- En el suelo: en la tierra se propagan mal porque el suelo absorbe y se extiende; en el mar tranquilo llegan lejos (Scilly-Canarie, 2500 km, aproximadamente una vez al año en 2 m). Los conductos de evaporación son a menudo sólo unos 15 m de altura y sirven microondas.
- Alto: entre 450 y 2000 m, debido al DX más largo en el suelo; la señal "alto" sobre las estaciones de abajo. Los conductos "seos" dan señales menos fuertes pero también permiten estaciones de distancia intermedias.
- Los conductos tienen direcciones precisas y pueden durar horas o días, contrariamente a la E esporádica.

Nuevas reglas sinópticas para el índice

- Edad de anticiclón: el aire cae al centro de unos pocos metros por hora y el reverso se reduce a medida que se desarrolla la alta presión; las mayores aberturas llegan después de varios días de propagación ya bien. Un anticiclón a corto plazo no es suficiente.
- Filo de alta presión: la capa es cúpula y los bordes es inferior, por lo que las estaciones mejoran.
- Dirección: Se ha observado que las rutas más largas corren a lo largo de los isobares o a través de los dorsals (Flavell G3LTP, 1984), pero hay excelentes conexiones también en dirección radial desde el centro. Ejemplo del 29 al 30 de agosto de 1987: desde Inglaterra la apertura giraba 180° en dos días (España, Francia, Italia, Austria, Noruega) tras el cambio del anticiclón.
- Contra: isobare grueso (viento fuerte), nubes estratocumuliformes al borde de la capa (limitar la frecuencia más alta).
- Frentes: un frente frío empuja el aire caliente hacia arriba y crea un reverso a lo largo del frente; la mejor dirección es a lo largo del frente mientras pasa, pero dura poco.
- Temporada: En el Reino Unido los anticiclones son más frecuentes a finales de verano y principios de otoño.
- Pistas visuales: humo atrapado en una línea oscura por encima del horizonte; con alta presión, capas con base por debajo de 1000 m indican un conducto de tierra, base por encima de 1000 m un conducto alto.

Referencias citadas a la búsqueda: R. G. Flavell G3LTP, "Estudios de un evento extensivo de propagación anticiclónica...", *Radio Comunicación*, febrero 1984, p. 128; J. Gannaway G3YGF, "Tropospheric Scatter Propagation", *Radio Communication*, septiembre 1981, p. 710; P. W. Sollom G3BGL W6ZGN

## 10. Un modelo de radio amateur verificado: Biancomano y Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, "Practical Tropospheric Model for VHF/UHF Propagation Prediction", *National Weather Digest* 22:2, junio de 1998, pp. 15–24, free of charge in [PDF from the National Weather Association](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf).

- Data: radiosundations of the 00 and 12 UTC of 47 stations in the eastern United States, 1995–1997; ground analysis to 700 hPa (at the beginning up to 400), on the profile of N, without M. Excluding coasts, islands and mountains.
- Para cada encuesta: presencia y cuota del conducto, longitud máxima de onda que puede conducir (a menudo alrededor de 100 veces la longitud de onda), máximo gradiente de la temperatura potencial entre los niveles vecinos y entre un nivel y el segundo arriba (para capas más gruesas). Los conductos terrestres no se utilizan.
- Primera regla, persistencia: índices de estabilidad (índice elevado, K, totales-totales, SWEAT) permanecen dentro de 3 unidades de la encuesta 12 horas antes, y el nivel de presión donde la temperatura potencial crece más sigue siendo igual. Si es así: pronóstico de mejora en las próximas 12 horas.
- Segunda regla, controles dinámicos: capa límite que no se levanta, CAPE, variación del espesor a 500 hPa en tres encuestas, variación de la pendiente dentro de los límites.
- Umbral de banda: un conducto capaz de transportar 100 cm o más indica la apertura en 432 MHz, casi seguro en 222 y a menudo incluso en 144.
- Filtro espacial: estaciones en una rejilla hexagonal de unos 400–500 km; el pronóstico sólo vale la pena si por lo menos 5 estaciones de contigue son positivas (si se puede saltar uno). Con 5 estaciones la probabilidad de un falso positivo aleatorio se vuelve insignificante.
- Verificación contra los acontecimientos de la columna "El Mundo Sobre 50 MHz" de QST: en 1996, 48 eventos previstos en 67 (71%); 12 de los 15 eventos principales. Precisión hasta un 70% en los 432 MHz y alrededor del 60% en los 144 MHz a las 12 horas; proporciona casi el doble de los eventos reales; 9 errores de 19 son predicciones llegaron tarde, porque muchas mejoras se forman en menos de 12 horas.
- Se agregó a las estaciones más al norte: el viento girando la escalada del reloj (punto caliente) como condición adicional.

Desde el PDF completo (suministrado por el autor del programa) tres detalles más. Su regla "tickness = 100 veces longitud de onda" da 600 m por 6 m, 200 m por 2 m, 125 m por 1,25 m y 70 m por 70 cm (fig. 3): para los 70 cm es menos severo que Kerr y Hepburn (aproximadamente 89 m), por los 2 m coincide con el VHF/UHF DX Book. El viento que gira el reloj gira, según los autores, sobre todo porque trae aire más cálido a la altitud, lo que hace que la caída de N crezca con presión; la humedad que trae consigo no parece desestabilizarse. El apéndice obtiene dN/dh en forma cerrada para una atmósfera de gradiente térmico constante con un salto agudo de gradiente de calor y humedad en una cuota determinada: útil como caso de prueba para nuestro código. Su rastro de la pendiente de la temperatura potencial proviene de K. H. Jehn, "El uso del índice refractivo potencial en la meteorología radio escala sinóptica", *J. Meteorology* 17, 1960, p. 264.

Qué tomar para nuestro índice: (1) a term of persistence: compare the profile with that of 12 hours before, that in the hourly forecast we have free; (2) the space filter: a point counts only if the improvement covers an area of a few hundred km, which reduces false alarms; (3) stability indices, calculated from temperature and dew to 850, 700 and 500 hPa (Open-Meteo already gives lifted index and CAPE); (4) the wind that rotates with the height ads

Otra fuente en el mismo paquete, de poco uso para nosotros: P. K. Rastogi, "Stratosphere and Troposphere (S-T) Studies at Millstone Hill", NASA 1984 (documento 19840019069): radar de 440 MHz que mide la turbulencia del aire claro entre 4 y 25 km, con capas delgadas vinculadas a ondas de gravedad y corte de viento. Confirma que la turbulencia que produce la dispersión es capa, pero se refiere a probabilidades demasiado altas para los conductos.

## 11. Tercera ronda de fuentes (20 de noviembre)

Casos históricos de alto conducto. J. Macoun OK1VR, enlaces en 2 m de la cumbre de Sněžka (1602 m), otoño 1958, artículo de 1959 traducido en 2006 ([ok2kkw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)). Los días 27 a 28 de octubre de 1958 primera conexión checoslovaquia–Inglaterra con 145 MHz ( G5YV, 1220 km) y muchos más entre 1100 y 1250 km, con señales estables después de 23. El autor lo atribuye a dos inversiones superpuestas, la superior de unos 2200 m, con la señal de regreso al suelo sólo donde se disolvió la capa inferior; condición favorecida por un lento anticiclón del Atlántico al continente. Desde casa (Praga) su máximo fue de 530 km: sólo las estaciones a altitud entraron en el conducto. Es otro caso de verificación (como LA0BY 2018) y confirma que el índice debe buscar realizado más de 2000 m y compararlo con la cuota de estación.

Bibliotecas históricas NBS (útil para buscar obras clásicas, no para fórmulas):

- W. Nupen, *Bibliografía sobre Propagación Troposférica de Radio Waves*, NBS Nota Técnica 304, 1965, 356 páginas, con índice para temas (también "pronóstico de condiciones") y por país. Las entradas italianas reportaron: L. Sacco, "La conexión radiofónica en el régimen de super-refracción atmosférica", *High Frequency* 24(6), diciembre de 1955 (Investigación italiana: super-refracción bajo 500 m frecuencia día y noche); S. Silleni, *Annali di Geofisica* 8(1), 1955; J. M. Clara e A. Antinori, pronosp.
- A. L. Abbott, *Bibliografía de Radio Wave Troposférica*, NBS, material hasta enero de 1960, alrededor de 670 artículos ordenados por año: útiles para la parte del troposcatter.

Divulgación y observaciones: F. W. Odenthal DK5EQ, "Ausbreitung 2m – Eine Einführung" (31/05/2024): casi óptico hasta 200 km, tropo por conducto o refracción más allá, con alta presión y en primavera y otoño; cita la página VHF de DM2HB y las notas de DK5YA en tropo y Es. Discusión sobre el puerto deportivo VHF y AIS ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)): alcance 1,23·h(ft) millas náuticas por antena, dispersen hasta unos 50 nm, mejora típica de trope 100–400 nm; los conductos son reconocidos porque las estaciones distantes son tan fuertes como los vecinos. Chelmsford ARS News, diciembre de 1983: en los concursos en 2 m las estaciones de la costa oriental inglesa están favorecidas por la ruta marítima hacia el continente. VK3DXE EI7GL

ARRL, *The Radio Amateur's VHF Manual*, 1972, cap. 2 "Propagation Beyond the Horizon" (pág. 17-19). Extremidades operativas ya conocidas (seguir áreas de alta presión que se mueven lentamente y sobre todo sus bordes traseros; temprano otoño el mejor período; zonas costeras, grandes lagos y valles de los ríos favorecidos por estratificación). Dos datos nuevos: (1) fig. 2-4 compara la atmósfera estándar ( humedad relativa 70% hasta 12000 pies, refracción mínima) con un radio típico de refracción fuerte, con una clara temperatura de rotura y relación de mezcla de alrededor de 4000 pies (unos 1200 m): es la firma que nuestro cálculo debe reconocer en los niveles de GFS; (2) los conductos verdaderos son raros bajo 144 MHz y más frecuentes en UHF, según los espesores mínimos. Bibliografía QST útil para buscar: Hull, "Condiciones de Air-Mass y Bending de UHF Waves" (junio de 1935, mayo de 1937); Hoisington, "Predicción sin dolor de aperturas de dos metros" (octubre de 1949); Collier, "Upper-Air Conditions for 2-Meter DX" (septiembre de 1955); Bots, "Una noche para recordar" (enero de 1970, llevado a cabo en tierra).

PA0EHG, página "Tropo" ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): resumen del troposcatter del artículo G3YGF, ya en el manual. Use dos reglas prácticas para controlar nuestro cálculo de dispersión: alrededor de 10 dB de pérdida extra para cada ángulo en el horizonte de cada estación, y más de 100 km aproximadamente 9 dB en más cada 100 km de ruta (60 millas); el volumen común comienza de unos 600 m en 100 km y de unos 9 km en 480 km. No trata conductas, encuestas o umbrales refractivos.

PA0EHG, Calculadora PROPBER ([página](https://pa0ehg.com/propagat1.htm), propber.exe programa proporcionado por el autor del programa). Es un programa DOS de 36 kB escrito en BASIC y compilado con Microsoft BASIC Compiler 5.60 (1985): solo funciona bajo DOS o DOSBox. De los textos internos: pide frecuencia, distancia (o los dos localizadores, también en el viejo formato QRA "E-locator"), para cada estación de altura de antena, cifra de ruido, banda FI, potencia, un obstáculo (altura y distancia) y ganancia de antena (0 = cálculo de diámetro de parábola, eficiencia 50%). Devuelve la distancia máxima en la óptica, la pérdida óptica correcta para la absorción de vapor (0.1 dB/km desde 24 GHz), la pérdida de troposcatter "correcto para los sitios" (es decir, para los ángulos de horizonte dados por los obstáculos) y la relación S/N en los dos sentidos, también "con la máxima propagación anormal". Las constantes numéricas no se pueden leer en el ejecutable compilado. El método sugerido para bandas altas también es útil para nosotros: calibrar el cálculo en un enlace medido (diferencia entre medido y calculado S/N) y aplicar la misma diferencia a otra banda. Es el mismo patrón de nuestro cálculo, pero utiliza el perfil S RTM1 en lugar de un solo obstáculo y el pronóstico GFS en lugar de un margen fijo.

PROPBER, constantes leídas en el ejecutable (números en formato Microsoft Binary). πr, en la tabla de datos junto a los textos: 8,68589 (= 20/ln 10, es decir, 20 registros calculados con el logaritmo natural), 32.5 (espacio libre: 32.5 + 20 log f\[MHz\] + 20 log d\[km\]), 24000 y 0.1 más 40000 y 0.3 (vapore: 0.1 dB/km d 24 GHz a 0.3 Probable, ya que se repite pero dentro del código: −69,5 (el mismo término constante que la fórmula de Yeh en VHF/UHF DX Book), 332 (refractivo al suelo), 8733 km (equivalente radio, k ♥ 1.37). La fórmula de dispersión exacta no es reconstructible sin desmontar el programa. T0

PROPBER, fórmulas reconstruidas por el código (desmontaje de la parte compilada; las operaciones son llamadas a la biblioteca BASIC con los operadores en memoria, cuyos valores en doble precisión han sido decodificados). Las constantes −69,5, 332 y 8733 encontrados anteriormente eran falsos positivos dentro de la biblioteca.

- Distancia óptica: d = 5 · (√h1 + √h2) km (h en m). Es más generoso que el clásico 4,12 (k = 4/3): es equivalente a k Ω 2.
- Pérdida en óptica: L = 32.5 + 20 log f\[MHz\] + 20 log d\[km\], más 0.1 dB/km sobre 24 GHz y 0.3 dB/km sobre 40 GHz.
- Troposcatter: Lts = L + 23 + 10 log f + 0.0674·d, es decir, 55.5 + 30 log f + 20 log d + 0.0674·d (formula de G3YGF con la constante 2 dB superior). Sin término de ángulo o reactividad.
- Corrección "para los sitios": el programa pide obstáculo y distancia, pero el bloque que debe utilizarlos está vacío; la variable de corrección permanece 0, por lo que la pérdida "correcta" es igual a la de la dispersión.
- Noise: T = 290·(10^(NF/10) −1), N = −228,6 + 10 log T + 10 log B (dBW), sin temperatura de antena.
- S/N = 10 logs P + G\_tx − L + G\_rx − N. "Con la máxima propagación anómala" utiliza la pérdida óptica en lugar de la dispersión, es decir, el caso ideal del conducto sin pérdidas más allá del espacio libre.

Comparación: 300 km y 144 MHz PROPBER da alrededor de 189,9 dB, G3YGF 187,9, nuestro P.452 187,8 (con ángulos de horizonte cero): la dispersión coincide dentro de 2 dB. Nuestro cálculo agrega los ángulos del horizonte desde el perfil, la temperatura de la antena, y en lugar del "maximum" fija la capa esperada con apareamiento y atenuación residual.

Rohde & Schwarz, P. Denisowski, "Understanding VHF Propagation", Nota Educativa, 2023 de abril (24 páginas, difusión). Confirma los puntos ya recogidos (conducidos como guía de onda bidimensional, señal que baja de una manera lineal con la distancia, hasta 1500 km y más allá, duración de horas a días; buena tierra en el mar y escasa en la tierra; elevado de unos pocos cientos de metros a unos pocos km). Dos observaciones útiles para el índice: (1) evolución en el tiempo: un conducto recién formado a menudo lleva sólo las frecuencias más altas y, engrosamiento, comienza a traer incluso el más bajo; el pronóstico por hora debe mostrar el 432 MHz "de antemano" en el 144 MHz cuando la capa crece; (2) estaciones ubicación: las mejores condiciones son entre estaciones cercanas a los extremos del conducto, mientras que los que a lo largo del camino de un conducto alto a menudo fallan". Los conductos se mueven con el clima atmosférico.

Sitio PA0EHG, otras páginas. Para el trope, PathProfile de M. Willis G0MJW ([scheda](https://pa0ehg.com/srtmp.htm), [download](http://www.mike-willis.com/Software/ASTERPathprofile.zip)): perfil de la ruta de los archivos totales esperados de pérdida y cuota, juzgado confiable para el troposcatter en microondas; no trata la dispersión de aviones o lluvia. Útil como segunda referencia para comparar nuestros perfiles y pérdidas S RTM1. El resto del sitio cubre EME, microondas, pista de aterrizaje (AirScout, mapas SM7LCB) y medidas en 80 m.

PathProfile por G0MJW (Mike Willis), paquetes ASTER y SRTM y SiteData (suministrado por el autor del programa; [página de software](https://www.mike-willis.com/software.html)). Programa de Windows de 2018: perfil de ASTER o SRTM a 1" o 3", pérdidas en óptica, difracción, troposcatter (válido hasta 1000 km), conducto y gaseoso, con porcentaje de tiempo, k mediana de la ruta, fracción de camino en el mar y distancia de la costa: la estructura es la de P.452/P.1812, pero la ayuda (archm comprimido) no pudo leer. SiteData convierte los localizadores y da la parte de GT OPO30.

La parte más útil es los archivos de datos incluidos: UIT-R P.453 mapas estadísticos sobre rejilla global de 1,5° (121 hileras de 90°N a 90°S, 241 columnas de 0° a este): gradiente dN/dz en los primeros 65 m excedido por 1, 10, 50, 90 y 99% del tiempo; para los conductos de suelo porcentaje de presencia, espesor e intensidad; para aquellos alta intensidad y espesor de la cuota, Valores anuales interpolados:

| Ubicación | Gasoducto terrestre: % / grosor / intensidad | Alto oleoducto: % / base / grosor / intensidad | dN/dz superó el 10% del tiempo |
| Nápoles |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | −380 N/km |
| Tirreno (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | −674 N/km |
| Trapani |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | 5−20 N/km |
| Brindisi |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | −405 N/km |
| Adriático (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | −399 N/km |
| Milan |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | −172 N/km |
| Udine |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | −176 N/km |

Para nuestro índice es una referencia climática lista: espesor e intensidad típica de los conductos en lugar de los 50 m fijados del conducto potencial, y una anomalía "hoy en comparación con lo normal" por zona. Datos de la UIT: gratuito para uso no comercial, para comprobar antes de incluirlos en una versión pública. El archivo ZO NES01.DEM (58 MB, 2' grid) contiene las zonas climáticas inland/coast/sea; el formato no está documentado y no lo decodifica. La página de las ecuaciones de Willis contiene solamente constantes físicas, espacio libre y ganancia de antena.

Cuarta ronda de fuentes (20 de noviembre).

Indermuehle y Suzuki 2026, predicción de los oleoductos GFS para el observatorio CSIRO de Murchison (arXiv 2608.12861): es la fuente más cercana a LIVETROPOVHF. Perfiles de N desde el GFS a 0,25° (superficie a 2 m, luego los niveles isobaricos, aquellos bajo tierra descartada); para cada columna mínima de dM/dz menores de 3 km, base y parte superior del conducto, déficit M integrado; luego P.452-16 y P.1812-6 con GLO-90 25 rutas de 98 a 1005 km, de 88 MHz a 2,68 GHz, cada hora. Resultados: Mediana AUC 0,72 en 25 canales; el porcentaje de tiempo P.452 p debe ser utilizado como una constante de calibración (elección p = 20%), porque el modelo de la UIT con un ΔN instantáneo no se define dentro de un conducto; la habilidad permanece casi plana hasta 3 días de antelación. A 162 MHz (AIS) las rutas recibidas tienen la fracción más alta del conducto del 67% (0.26 versus 0,116) y la profundidad de conducto más alta del 49% (28 vs 19 M) con respecto a los controles: en el mar el conducto es casi siempre, decide el tramo de tierra. El GFS ve la mitad del tiempo en el suelo en comparación con ERA5 (4,5% frente al 10,4% de las células-horas), mientras que en el mar ve más (73% frente al 56%). Un índice de "corridor" (continua tuberías a lo largo de la ruta, capas con un espesor mínimo de 40 m) mejora el resultado: confirma que la continuidad a lo largo de la ruta es el criterio correcto.

Longley y Rice, ESSA ERL 79-ITS 67 (1968): método para terreno irregular de 20 MHz a 20 GHz (después de ITM), con irregularidades del suelo Δh, Ns de 250 a 400, radio equivalente 8497 km (4/3), horizontes, difracción, dispersar hacia adelante y variabilidad en el tiempo y en lugares para el clima. Referencia estadística, no instantánea.

UIT-R P.834-2: curvatura radial, radio terrestre equivalente k·a, M = N + h/a (h en m, en miles de km), modelo exponencial; nada nuevo que lo que ya se utiliza.

AGARD CP-70 (1970), propagación troposférica: capas delgadas de 1 a 100 m con gradientes fuertes son comunes; el coeficiente de reflexión de una capa alta es proporcional a la longitud de onda, con una dispersión aleatoria de alrededor de 14 dB. Medidas a 90 MHz: disminución de 9-10 dB atado a un salto N concentrado a unos 350 m, en la parte superior de una capa de nubes. El paso de un frente frío elimina las capas: sigue siendo la difracción.

Informe U.S. URSI, Comisión 2 (1960): espesor medio de las capas de 60 a 210 m de altura (de acuerdo con P.453), gradiente mínimo para el conducto de 157 a 160 N/km.

Buscemi K2OVS, guía de propagación VHF: distancias típicas para reversal y conducto para banda, conductos vinculados a alta presión estacionaria, máximo al amanecer.

Wolinsky-Mancini y otros 2024, AIS en Canarias: radio y recepción AIS; los conductos son mucho más frecuentes en los meses cálidos (inversión de los aliseos).

Artículos nigerianos y malasios (Ekah, Iloke, Mat, Igwe): correlaciones locales entre temperatura, humedad y pérdida medida, débil y contradictorio (r −0,09 a 0.27); útiles sólo como confirmación de que los tamaños de suelo por sí solos no son suficientes.

Herben 1984, tesis TU Eindhoven (microonda): 30 GHz en 8,2 km y satélite OTS en 11,6 GHz; espumoso para la turbulencia, el multirretimiento y la lluvia. Para las pérdidas dinámicas de microondas están dominadas por la lluvia y la turbulencia, no por conductos.

Baars, The Paraboloidal Reflector Antenna (microonde): ganancia de una parábola G = ć·(πD/λ)2 con eficiencia típica 0.5–0,65; pérdida de errores superficiales según Ruze, piras = exp(−(4πε/λ)2). Ayudaría si añadimos las parábolas como una alternativa a Yagi DL6WU para 1296 MHz y más allá.

Ya analizado o de mal contenido: Hirschmann NASA X-731-67-89 (doble), el radar VHF ST de Reid (vientos y turbulencias, no conducidos), deslizamiento de garabatos y notas genéricas sobre la troposfera.

Quinta ronda de fuentes (20 de noviembre, tarde).

Rogers 1997, Radio Science 32(1): estimación bayesiana de los parámetros de un conducto alto (base rate, M deficit, grosor) de factores de propagación medidos más allá del horizonte, con un modelo de ecuación parabólica; VOCAR experimento en California a 143, 263 y 375 MHz, comparación con la onda de radio. La señal depende mucho de la base y el déficit M (corregir 0,81 a 143 MHz para el déficit), casi en absoluto del espesor (−0,17): el espesor no se puede obtener de la señal. Para nosotros: el cálculo B (espeso del cliente) cuenta menos que déficit y cuota; y el método es la base para la oficina posterior, es decir para obtener el conducto de las múltiples frecuencias.

Moler and Holden 1960, "Tropospheric scatter propagation and atmospheric circulations", J. Res. NBS 64D, págs. 81 y siguientes (read in original on 22/11/2026, doi 10.6028/jres.064d.014). Rutas marítimas de California a Point Loma (San Diego): Santa Bárbara 190 millas, Point Mugu 144, San Clemente 78; 1365 MHz y 9380 MHz, no VHF; Enero–abril 1958, promedios 1 minuto. El caso del 26 de marzo: un refuerzo de alrededor de 20 dB durante aproximadamente 3 horas hacia el mediodía, cuando un vórtice ciclónico se debilita y comienza la brisa y la subsistencia del mar; a las 15 se reforma la convergencia y la señal baja alrededor de 22 dB desde el máximo. Estadísticas (tabla 1, 500 hPa, 33 casos): con aire que baja la señal promedio es superior a unos 2,5 dB que con el aumento del aire, en L y X banda. La señal sigue la diferencia calculada a 500 hPa en el signo y amplitud de las variaciones, sin un coeficiente de correlación. Las capas bajas (menos de unos 600 m) favorecen la reflexión parcial sobre la dispersión. Los anticiclones y los frentes no se tratan, y las "excursiones hasta 60 dB" mencionadas anteriormente no son: vienen de un resumen de segunda mano. Para nosotros: el movimiento vertical es un buen predictor, y +1.5 dB para la subsistencia y −2 dB para el aire cuesta arriba son del mismo orden que la diferencia media medida (2.5 dB); 20 dBs son un caso único, no un valor típico, y en microondas.

Banafaa y Muqaibel 2025, IEEE Access: encuesta de los oleoductos (modelos de evaporación, ecuación parabólica, estimaciones de radar, GNSS y lidar) y clasificación con aprendizaje automático sobre medidas reales; Las máquinas vectoriales de soporte dan los mejores resultados. Útil como bibliografía; los datos son del Golfo Pérsico.

UIT-R P.834-7 (2015): versión actualizada de la recomendación sobre refracción troposferal; ninguna fórmula nueva que las ya utilizadas.

Abbott, bibliografía NBS sobre la dispersión troposferosa (GOVPUB- C13): ya en la tercera ronda. Diapositiva Scribd en los conductos: contenido genérico. El artículo ScienceDirect 0021916987900304 no se podía abrir.

Sexta ronda (20 noviembre, noche): Bean y Dutton, ARRL Handbook 2022 y 2023, Balanis.

Bean and Dutton, Radio Meteorology (cap. 6), parts not yet used. Sensibilidad de campo a Ns: 0.18 dB por unidad N en el ciclo anual; 0.14–0,24 dB/N en el ciclo diurno (máximo temprano en la mañana), hasta 0.2–1,1 dB/N en las rutas de 175–200 km en invierno; correcciones 0,8–0,95. Parámetro compuesto por Misme: M = a(g\_e − 40 + b·ΔW^n) con un = 0,5 dB por N/km, que combina el gradiente equivalente y la estabilidad térmica ΔW (trabaja para levantar el aire de 1 km): en el Sahara a 471 MHz a 160 km la única estabilidad es 20 dB entre enero y junio en casi igual gradiente. potencial índice de refracción K (N informó adiabatically a 1000 hPa): destaca las masas de aire y la subsidence anticiclónica (Flavell y Lane).

ARRL Handbook 2022 (99a ed.), § 19.4.3–19.4.4. Las inversiones útiles entre 250 y 2000 m; las inversiones de radiación (sera, aire calmado, suelo seco) raramente conducen a la verdad; los anticiclones lentos dan los conductos más extensos por subsistencia, con inversiones entre 500 y 3000 m, más fuertes por la noche y por la mañana; en el hemisferio norte las mejores rutas son al sur del centro alto. Frente caliente: buenas condiciones a unos cientos de km de adelante; frente frío: fuerte pero inestable reverso detrás del frente, paralelo. Foehn (también en los Alpes) y brisa terrestre (aire frío en el suelo, caliente a 200–300 m; se extiende a más de 800 km a lo largo de la costa). Scirocco: aire caliente y seco del Sahara sobre aire marino más fresco, inversiones muy fuertes del Mediterráneo oriental a Gibraltar, causa probable de registros de más de 1500 km en microondas. Troposcatter: límite teórico de unos 800 km, máximo medio típico. La 100a edición (2023) tiene un texto más corto: inversiones al amanecer y al atardecer hasta +20 dB y +20–50% de caudal; 1200 millas en los Grandes Lagos, típicos 400–600; recomienda Radio Mobile (modelo ITM) para cobertura y mapas de Hepburn.

Balanis, Antenna Theory (4a ed.), § 10.3.3: Viezbicke table (NBS TN 688, 1976) para Yagi optimizado a 400 MHz: boom 0.4 / 0.8 / 2.2 / 3.2 / 4.2 / 4.2 λ → 7.1 / 9,2 / 10.2 / 12.25 / 13,4 / 14.2 dBd. Nuestra fórmula DL6WU λ (8.6 + 8 log10 boom1λ) Con la fórmula correcta en el original (9.4 + 7.8 log10 boom/λ, de 22 de noviembre) el descarte cae por debajo de 0.2 dB entre 1.2 y 4.2 λ (12.1 dBd a 2.2 λ versus 12.25) y permanece 0.6 dB a 0.8 λ. DL6WU

Séptimo round (Noviembre 21,): QST, QEX, W3SZ, repositorio GitHub.

QST 2000–2025 (14 números, 10 camas con reconocimiento de texto). Únicamente los informes sobre El Mundo Sobre 50 MHz, útiles como casos para validar el modelo: 5–9 de septiembre de 2000, realizados en 144 MHz y más allá en la mitad oriental de los EE.UU.; 14–15 de agosto de 2022, abriendo Manitoba–Kansas/Missouri bajo alta presión en un día caliente, de 144 a 1296 MHz casi todo en FT8 RS, récord canadiense a 10 GHz de 1228. QST noviembre 2022 recomienda mapas Hepburn. No hay artículo técnico sobre el pronóstico del tropo. VE4MA N0OY Q65

QEX y K1JT, Franke y Taylor (QEX 2020) en modos FT4/FT8: umbrales de decodificación FT8 −20,8 dB en 2500 Hz en canal sin evanescencia, −18,6 dB con evanecencia en latitudes medias; FT4 −17,5 dB. Los otros números de QEX 2023 y 2026 no tratan al tropo (tres números 2023 son escaneos sin leer). W3SZ 2019: umbrales de modo lento ( JT4A −23, JT9A −27, JT65A −25, QRA64A −26, FT8 −21 dB) y criterios de elección entre sensibilidad y tolerancia de cambio de frecuencia.

GitHub. TropoLink (LukiBox, Apache-2.0): diseño de rutas troposcatter con P.617-5 (con N0 y ΔN), NBS TN101 e ITM flanked, y su diferencia se muestra como incertidumbre; P.617-5: L = F + 22 log f + 35 log θ + 17 log d + Lc, F = 0.18· N0 ·e^(35 Δh andi-f/troposcatter ( DL2FZN 2013, de VHF/UHF Handbook RSGB): Lts = 24,44 + 4,283 ln f + 0.06656 d sobre espacio libre, pendiente cerca de PROPBER (0,0674 dB/km). QRadioPredicto: ITM y ITWOM 3.0 con S RTM3, uso del suelo Corins y diagramas NEC. No es relevante: Tropos (Aplicación meteorológica), opera-adt/tropo y troposim (retraso troposférico para InSAR), tgbot\_troposcatter (estación militar), sdr-troposcatter-gui ( panel de control), Troposcatter-Propagation-Delay (retraso ERA5), Radio-Communication-Project ( simulador didactic simulator), Py).

Octavo vuelta (21 de noviembre): modelos NTIA y UIT en GitHub. NTIA/itm (Longley-Rice, ITS): 20 MHz–20 GHz, pérdida de espacio libre, diffracción y troposcatter con perfil de suelo, 0,5–3000 m terminales, reactividad de superficie N0 (mínimo reducido al nivel del mar, 250–400), siete climas, constantes del suelo y varios modos 1 vista, 2 diffracción, 3 troposcatter; Cclo+ No modela los conductos: sirve como referencia "ambiente normal" con variabilidad estadística. NTIA/p528 y eeveetza/p528 (MATLAB): UIT-R P.528-5 para conexiones de aire-suelo, 100 MHz–30 GHz, 1,5–20 000 m terminales, p 1–99%;.NET 4.8 casing. Posible uso: equilibrio de dispersión de aire (dos rutas terrestres– aeronaves más sección de radar). eeveetza/p2001 (MATLAB): ITU-R P.2001-6, modelo de campo completo con difracción, troposcatter, propagación anormal (conductos y capas, Lba) y E esporádica, porcentaje de tiempo 0-100%; UIT mapas digitales (DN\_Median, DN\_SupSlope, DN\_SubSlope, dndolvención Posible uso como comparación de la UIT en lugar de P.452, con el GFS ΔN en lugar de mapas como en el estudio CSIRO; los mapas deben ser descargados por el usuario según P.453. Validación 1.3.0 de 593 QSO (29/12/2021–2/1/2022): evento / comparación separación 0,79 sin correcciones, 0,91 con Misme y subsidence; el GFS no ve el conducto en el mar a 144 MHz (guadagno sin correcciones nunca más de 15 dB).

Noveth round (November 21,): IU3QEZ, NTIA, PyWaveProp, ACE High, validation 1.3.1. IU3QEZ (S. Fabris), presentación "Over the horizon in VHF" (GEKO Magazine n. 69, septiembre 2026): volumen común d2/(8·R\_eff) con R\_eff ♥ valor 7500 km Bibliografía de la presentación a recuperar: Norton 1959 (J. Res. NBS 63D, System Loss), Bray 1961, ARRL Antenna Book 2023 fig. 4.29, Rice-Longley-Norton-Barsis NBS TN 101, Bean and Thayer 1959, Barsis y Kirby 1961 (diffration on ridge), Deygout 1966, P.1812, AirScout PyWaveProp (Python): Métodos de ecuación parabólica con aproximaciones Padé, diffracción de múltiples bordes, recintos PETOOL y RAM, conductos troposferos y altos, suelo irregular: posible base para un cálculo de campo con el perfil GFS M(z, para ser revisado. APRS PropView (Apache 2.0): Mapas de propagación VHF de paquetes APRS, estadísticas por hora y alarmas de apertura; posible fuente de datos de 144 MHz para validación. Red troposcatter de la OTAN en Italia (fortifications.net): 900 MHz, 10 kW, 20 m parábolas; estaciones incluyendo Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Vulture, Monte Lauro; caso de prueba histórico para P.617. No es relevante: Gupta 2021 (transporte estratosférico), Ley 2023 (Ozono Ártico), delfín #590 y ópera-adt/tropo (retraso de la RAE), tropo.ino (sensores de gas Arduino), tropo-webapi (telefonia), nubes/troposfera (AWS). Validación 1.3.1 sobre 564 QSO tropo: AUC 0,79 (modelo 1.2), 0,80 (+ conducto blando), 0,90 (+ error y subsistencia), 0,90 (+ persistencia); colores de conducto (≥ 16 dB) en el 46% de las rutas de apertura y 0% de comparación. IQ3QC DL2ALF

Se agregó a la misma ronda. mikelytaev/wave-propagation (MIT, Python 3): Ecuación parabólica con aproximaciones de Padé y Numerov, suelo irregular, conductos troposféricos; cargar el M refractivo modificado (quota, lat, lon) del GFS NOAA y establecer un conducto de evaporación de Monin-Obukhov; ninguna función documentada que devuelve la pérdida en el camino. Es el mejor candidato para un futuro cálculo de campo con el perfil GFS M (que serviría un servicio externo o una traducción). RP-Sim (AGPL-3.0, JavaScript): Modelos empíricos para las redes de radio (Hata, He, Ericsson, dos rayos), no tropo. RIMFading (GPL-3.0, OMNeT++): irregularidades de flujo para simulaciones de red, distribución Weibull, no tropo. PyLayers: canal de radio interior, ya excluido. qradiopredict (kantooon, GPL v2): ITM e ITWOM v3.0 con S RTM3 y Corine, ya en la séptima ronda como QRadioPredict.

Décima ronda (21 de noviembre): Antenna Book, Norton, Barsis, nuevas fuentes de datos. ARRL Antenna Book (23a ed.), cap. 4 ( K9LA): Método de freno (QST noviembre 1961) para la gama VHF: suave pérdida de la Tierra al 99% (300 millas, 144 MHz: 214 dB), curvas al 50% y 99% (fig. 4.11): 195 dB por 100 millas al 99% y más de 250 millas al 50%; evanescencia ±7 dB por más de 100 millas (−3,5 dB a 50 millas), SSB 3 dB peor que el C/3/ Salida del sol y puesta del sol hasta +20 dB y +20–50% de caudal; aire caliente y seco sobre el aire fresco húmedo en los Grandes Lagos hasta 1200 millas, típico 400–600; regiones influenciadas por grandes masas de agua más favorecidas; invierno tardío con calefacción capaz de fuertes inversiones. Verificación de nuestro troposcatter: P.452 a 50% a 483 km y 144 MHz da alrededor de 204 dB, consistente con el 214 dB de Bray al 99%. Norton 1959 (J. Res. NBS 63D, libre en NIST): pérdida básica, sistema y avance de la antena de ruta; Ambiente de referencia Ns = 301; radio equivalente de Ns (250 → 4878 millas, 301 → 5280, 400 → 6997); L(99) tablas con antenas de 30 pies (100 MHz: 163 dB a 100 millas, 208 dB a 500 millas; 1000 MHz: 138 y 188 dB con parábolas de 28 pies); variabilidad a largo plazo V(p, θ) dependiendo de 13 distancia (Fi). Rice, Longley y Norton, NBS TN 15 (1959), gratis en NIST. Barsis y Kirby, NBS Report 6751 (1961): 223 km en Colorado con Pikes Peak (4292 m) como borde, 100 y 751 MHz; pérdida consistente con el borde redondeado (aproximadamente 11,5 km de radio); evanescencia a largo plazo 11,7–14,3 dB versus 39–42 dB de una dispersión de la misma longitud; ningún obstáculo máximo de montaña ARRL VHF Digital Handbook: paquete, APRS, D-STAR, WSJT (Método de estafa y EME), ninguna parte del tropo. Nuevos datos comprobados o buscados: ECMWF open data (IFS 0.25°, levels 1000, 925, 850, 700 hPa..., sin 975, 950, 900 y 800; G RIB2 con compresión CCSDS, que nuestro lector no decodifica; ya disponible a través de Open-Meteo); DWD ICON-EU open data (G RIB2 comprimido bz2, un archivo para la variable y el nivel: muchas solicitudes)

11a ronda (21 de noviembre): Longley-Rice, GEKO 69. Bean and Thayer, "Models of the Atmospheric Radio Refractive Index", Proc. IRE May 1959 (pp. 740 et seq.): el modelo 4/3 tiene la pendiente derecha en el primer km pero cae demasiado alto; 30% de la curvatura de un radio ocurre en los primeros 100 m y 60% en el primer km; relación media entre Ns y disminución en el primer km: −ΔN = 7,32·e^(0,005577·Ns) (Ns = 313 → ΔN sobre −42). Bean y Thayer, "C.R.P.L. Exponential Reference Atmosphere", J. Res. NBS 63D n. 3 (1959): N(h) = Ns·e^(−c·h) con c fijado por Ns a través del mismo informe; Promedio Ns USA 313; tablas de seguimiento de haz ( curva total 15.2 mrad con modelo exponencial versus 30.3 mrad con la Tierra 4/3). MATLAB refractionexp: c = 0.143859 km−1 para Ns = 313 (escala alrededor de 6.95 km), válido por encima de 1 km y 10 mrad. Use for us: the "normal" profile of comparison can be that CRPL with the local Ns instead of M = 315 + 0.118·z fixed, and the anomalous decrease in the first km should be measure compared to −7,32·e^(0,005577·Ns) instead of −40 fixed. NTIA Report 82-100 (Hufford, Longley, Kissick 1982), ITM guide in area mode: climi and Ns suggested (equatorial 360, subtropical continental 320, subtropical marine 370, desert 280, temperate continental 301, tempate marine on land 320, temperate marine on sea 350); Ns = N0 ·ection^(hs/9,46 km sub); usted utiliza menos Rice, Longley y Norton, NBS TN 15 (1959), parte I: previsión de la fórmula de distribución con el tiempo por tierra y onda troposférica, base de TN 101. SoftWright, Wikipedia, Altair WinProp y MATLAB rfprop. LongleyRice: la misma información (versión 1.2.2, 20 MHz–20 GHz o 40 GHz, 1–2000 km, Ns 301, siete climas, variabilidad del tiempo, lugar y situación); nadie forma los conductos, sólo el efecto estadístico de los climas marinos ("campos más altos para el 10% del tiempo o no"). QKO Magazine n. 69 ( IU3QEZ): horizonte de radio 4/3 de 130 km de 1000 m, 260 km añadiendo dos estaciones; troposcatter con volumen común entre 1–3 km y 10 km; disperso de aeronaves a 10 km con +10–20 dB por diez segundos o un par de minutos; cursos teóricos con 2×10 elementos por lado: SSB QRP cerca de 440 km

## 12. Mesa redonda: fuentes para validar el modelo o corregirlo (21 de noviembre)

Resumen de todos los giros de las fuentes, ordenados para su uso. "Estado" dice que si la fuente ya está en el programa (1.3.1), ya sea en juicio o si debe hacerse.

### Datos para validar (lo que realmente sucedió)

| Fuente | ¿Qué pasa? | Uso | Estado |
| MMMonVHF reporta del autor | 593 QSO a 144 MHz, 29/12/2021–2/1/2022, tiempo y localización | validación de eventos contra comparación (+14 días) | en uso: AUC 0,79 (1.2) → 0,90 (1.3.1) |
| El mundo por encima de 50 MHz | Casos documentados (14–15/8/2022 Manitoba–Kansas, 144–10 GHz) | caso histórico | en uso como caso histórico; a ser añadido a la validación QSOs |
| wspr.live (WSPR) | archivo completo, 2 m y 70 cm, SQL | muchos eventos y muchos "no" (horas sin lugar) | probado, debe ser filtrado (spot falso a 16-20mil km) |
| APRS PropView / APRS-IS | estaciones sentidas, distancias, horas | sólo validación 144 MHz | para hacer |
| Beacon inverso (arquivo) | lugar diario | VHF | descartado por ahora |
| I GRA2 radiosondaggi (Brindisi 16320, Practice of Mare 16245, Trapani 16429, Udine, Milan) | perfiles medidos 00 y 12 UTC | Control e inversiones de perfil GFS | encontrados archivos (Brindisi 46 MB), para ser utilizados |
| Barsis y Kirby 1961, Barsis 1963 | evanescencia medida en caminos con obstáculos montañosos | validar la categoría "monte" (Vesuvio, Faito) | para hacer |
| Norton 1959, NBS TN 15, TN 101 | L(99) and table media losses | Control de estafadoras | en parte (ver 300 millas consistentes) |

### Correcciones sugeridas por fuentes

| Corrección | Fuentes | Efecto medido en validación | Estado |
| Estabilidad errónea Mandato | Gorra de frijol y Dutton. 6 | AUC 0,79 → 0,90 junto con la subsidencia | en 1.3.1 (Título 16 dB sin conducto) |
| Incidencia de movimiento vertical | Moler y Holden 1960 (read in original on 22/11: +2.5 dB promedio entre el aire bajando y el aumento del aire, 1.4 y 9,4 GHz por el mar) | arriba | en 1.3.1 (+1,5 / −2 dB) |
| Soft pipeline (λ hasta 3·λmax) | Kerr, Hepburn, VHF/UHF DX Book, Shi 2019 | solo +1% de AUC; trae los colores del conducto en el 46% de las rutas del evento y el 0% de la comparación | dentro |
| Persistencia 24 h | VHF/UHF DX Book, Whiteman y Leyshon | AUC sin cambios, más tomadas y más falsas alarmas | en 1.3.1 |
| Perfil CRPL normal con Ns locales (−ΔN = 7,32·e^(0,005577·Ns) en lugar de 40 N/km fijo | Bean y Thayer 1959, NBS Monograph 4 | probado en validación | en 1.3.2 como alternativa, fuera debido al defecto |
| Super-refracción umbral 79 N/km (ITU) en lugar de 100 (Bean y Dutton, capa 1000-850) | P.453, López 2008, DX Book | probado en validación | en 1.3.2: estimación agresiva junto con prudente (el mapa utiliza prudente) |
| El espesor climatológico (calculo B) menos importante que el déficit | Rogers 1997 | para probar: utilizar A para ganar | para hacer |
| Ecuación parabólica en el perfil GFS | PETOOL, ola-propagación (Lytaev), Levy (libro no leído, sólo vista previa), Lindquist | primera versión con campo sobre el horizonte dominado por el ruido numérico (AUC 0.12–0,28): correcto (dominio 8 km, viga 6°); después de la corrección AUC 0,54–0,66 | en uso como segundo cálculo, no decide el pronóstico |
| Evanescencia: ±7 dB más de 100 millas, margen 10–20 dB a 90–99% | Bray 1961, Antenna Book, GEKO 69 | umbrales de caminos en condiciones marginales | en uso: probabilidad para período y umbral FT8 -18,6 dB en troposcatter |
| Volumen común y distancia equivalente de Bray | IU3QEZ, Bray 1961 | Información | dentro |
| Troposcatter P.617 y RSGB como incertidumbre | TropoLink, DL2FZN | Información | en 1.3.0 |

### Lo que queda fuera de los modelos

Conduit to the sea at low altitude (100–200 m) that the GFS levels do not see (study of current data sources); reflection and diffraction on the nearby mountains (Vesuvio, Faito): proposal for calculation "via monte". ITM/Longley-Rice sigue siendo una referencia estadística "día normal" (Ns 301, climi), sin conductos. Discutido por decisión del autor: AirScout, P.528), E sporadic (P.2001) y 50 MHz (servoir EIRP que pocos tienen y confunden con el E sporadic).

### Propuesta de orden de trabajo

1. Validación extendida: añadir WSPR filtrado (incluso horas sin mancha) y QST 2022; repetir por 432 MHz.
2. Prueba el perfil normal de CRPL y el umbral de la UIT de 79 N/km, uno a la vez, manteniendo sólo lo que eleva la AUC.
3. Comparación de los perfiles GFS con la onda de radio I GRA2 de Brindisi y práctica del mar en los días de la QSO.
4. Reevaluar la ecuación parabólica después de la corrección numérica.

### Decisiones de 21 de noviembre (versión 1.3.2)

- Adoptado: estimación prudente (Bean y Dutton, 100 N/km) y agresivo (UIT, 79 N/km) flanqueado; conductos buscados sólo por debajo de 2500 m (primero 1500 m, levantado después de la validación); bonificación con alta presión extendida y viento débil; peso del mar con el μ1 de P.452; parte de estaciones con respecto al conducto también en conducto parcial; capa de límite por encima de las cres (X). IT9GSF
- Adoptado como alternativa, apagado por defecto: perfil normal CRPL con Ns locales.
- Ecuación parabólica correcta numéricamente: AUC de 0.12 a 0,54 (30 m de antena) y de 0,28 a 0,66 (300 m).
- Tierra: Copernicus DEM GLO-30 en lugar de Terrain Azulejos «skadi», que cerca de la costa tienen relieves fantasmas de 80–100 m en el mar (firmación de IC8TEM, punto por punto verificado a Capri); skadi restos como reserva.
- Signos meteorológicos en el mapa con su efecto: ↓ favorable subsidence (Moler y Holden 1960, en promedio alrededor de 2,5 dB entre el aire bajando y el aire subiendo; +1.5 dB), ↑ aire cuesta arriba desfavorable (perturas profundas; −2 dB), pista esciroco favorable (ARRL Handbook § 19.4, fuertes inversiones en el Mediterráneo; ninguna corrección). Doble símbolo, colores verde/rojo/orange y cómic en el mapa.
- Adoptó el ITM 1.2.2 (NTIA/itm, dominio público), traducido al C#: según cálculo de la base en atmósfera normal junto a P.452 y tramo final después de la salida de un conducto que termina antes del corresponsal. Código qradiopredict desvelado porque bajo GPL v2.

Sin contenido útil para el índice: *Communicaciones Trimestral*, otoño 1991 (no hay artículo sobre el tropo; el artículo de Atkins sobre el Scatter es en invierno 1991).

- Adoptado como principio de manuales (Noviembre 21,): "la propagación no puede ser generalizada, sino estudiada en relación con la morfología local" ( DF5AI, reportada por IK3XTV en "Propagación de 2 metros"); citado en el manual de pronóstico (§ 1) y en los consejos.
- Adoptado (21 de noviembre, después de 1.3.3) la interpretación de las aberturas transalpinas de IK3XTV: no "células convectivas como lentes", sino troposcatter reforzado por la turbulencia a la parte superior de la capa de recast cuando alcanza la parte del volumen común (manual de pronósticos, § 8 ter). Desde 1.3.4 entra en el cálculo punto-punto como un refuerzo provisional del troposcatter, en la mayoría de 3 dB. Discutido por los mismos artículos: la "auto-amplificación" de las señales más allá de los Alpes (un medio pasivo no amplifica: los enlaces descritos siguen explicables con la difracción y troposcatter; los poderes declarados no se utilizan como dado), la nieve que "entre la difracción" (probablemente la capa estable sobre la nieve, con menos evanescencia).

- Kerr (1951) examinó el libro (22 de noviembre, PDF proporcionado por el autor): § 1.5, eq. 17 y 18 y cuadro 1.1. Las figuras del eq. 18 en el escaneo no son legibles, pero la tabla 1.1 confirma: 20 ft → 1 cm, 80 ft → 10 cm, 400 ft → 100 cm, 2000 ft → 1000 cm, contra 1,25, 10, 112 y 1252 cm de λmax = 0.014·d^1.5 (λ in cm, d standing; la mesa redonda, 'valores indicativos'. Hipótesis de Kerr: dentro del conducto las gotas de índice modificadas de 4·10-8 por pie, valor típico de los conductos bajos; con gradientes más débiles necesitan un mayor espesor. Kerr también advierte que λmax no es un corte neto: longitudes de onda a veces más largo todavía puede ser fuertemente influenciado por el conducto.

- DL6WU revisó sobre el original (22 de noviembre, PDF de VHF Communications 3/1982 proporcionado por el autor): G. Hoch, «Extremely Long Yagi Antennas», pp. 130 y siguientes. Fig. 1: ganancia en el dipolo medido en 432 y 1296 MHz en un polígono profesional de los Posts alemanes, con una antena de muestra EIA de 7,7 dBd, precisión estimada 0.5 dB; 2,35 dB para duplicar la longitud del boom, sin saturación hasta alrededor de 20 λ. El programa utilizó 8.6 + 8.0.log10(L/λ) dBd, es decir, alrededor de 0.8 dB en menos de 1–2 λ; ahora utiliza 9,4 + 7.8·log10(L/λ) dBd (el gradiente es el indicado, el valor a 1 λ se lee en el gráfico, ± 0,3 dB). Descartó la fórmula 10·log10(7,8·L/λ) distribuida en la red: daría 3 dB para duplicar, contra 2,35 medidos.
- Booker and Gordon (1950): reference confirmed by "This Week's Citation Classic" (Current Contents, 8 de febrero de 1982, comentario de H. G. Booker): Proc. IRE 38, pp. 401–412. El comentario confirma los dos puntos utilizados en los manuales: campo recibido más allá del horizonte con distribución de Rayleigh, y función exponencial de autocorrelación de irregularidades (no gay). El artículo original no ha sido leído.

## 13. De IK3XTV artículos

Los dos artículos de Flavio Egano IK3XTV (Radio Magazine: "Propagación a 2 metros" y "Propagación VHF – células convectivas y ondas gravitacionales") añaden dos cosas que los modelos de conducto no cubren: las aberturas transalpinas de la tarde debido a la convección, y reglas prácticas sobre dónde y cuándo se forman los conductos en Italia. Son observaciones de la estación, no medidas: deben ser tratadas como hipótesis para comprobar con los puntos.

| Comentarios del autor | Cómo se convierte en un ingrediente del índice |
| Los conductos se forman principalmente entre 100 y 1000 m, raramente más alto; sirven alta presión en una zona amplia, buena insolación y ausencia de viento | búsqueda del conducto sólo bajo 1500 m; bonificación con alta presión extendida y viento débil en las capas bajas |
| En los meses cálidos realizados recurriendo al Adriático entre Veneto y Puglia, a 700–800 km, a veces hasta Grecia; las rutas por mar son favorecidas | mayor peso a la fracción de la ruta del mar (como el μ1 de la UIT-R P.452) |
| El correspondiente debe estar dentro o cerca del conducto; con antenas altas y unos pocos metros llevan la señal no entra | comparar la parte de la base del conducto con la parte de las dos estaciones ( servirá la altitud de la QTH) |
| La misma vez, el mismo conducto: escuchar desde el Gargano una estación en 144 y una en 50 MHz | para comprobar: según la tabla Hepburn los 50 MHz piden más de 300 m de plomo, por lo que el evento indicaría un oleoducto muy a menudo |
| Conexiones Veneto – Alemania del Sur más allá de los Alpes, sin visual, casi siempre por la tarde y con alta presión; señales inestables que desaparecen y reaparecen después de unos minutos | índice "convectivo" separado del conducto, para rutas de montaña |
| Las células convectivas nacen sobre el suelo y ascienden alrededor de 1 km cada 3 horas: a 15 alcanzan alrededor de 3 km, la altitud que sirve para anular los Alpes | utilizar la altura límite de la capa (boundary\_layer\_height of Open-Meteo GFS) a lo largo del camino: puntuación cuando excede la altitud de las crestas |
| Los Alpes generan ondas gravitacionales que, junto con movimientos convectivos, crean irregularidades del índice refractivo | hipótesis: viento fuerte perpendicular a la cadena a 700 hPa como señal de ondas de oro; para probar el último |
| La inversión muy alta (hasta 8 km en casos excepcionales) da alrededor de 800 km, pero en la práctica raramente más de 300 km | la parte de la inversión extiende el alcance: datos útiles para estimar la distancia de espera |
| En verano el radio terrestre equivalente a unos 16000 km (2.5 veces el real), con unas 60 dB en menos de 300 km de pérdida | consistente con k50 = 157/(157 − ΔN): k = 2,5 corresponde a ΔN alrededor de 94 N/km, que es super-refracción |
| Más de 3000 km sólo en el rango de 20–30° norte y sur del Ecuador, sobre el mar, vinculado a las células de Hadley; pico en agosto en África–Europa | fuera de Europa: útil no dar índices altos en el Atlántico sin retroalimentación, y para la ruta canaria – Gran Bretaña |

Otros datos útiles de los mismos artículos:

- Rango óptico entre dos h1 y h2 metros (d en km): d = 4,1 · (√h1 + √h2), hasta un 20% para la refracción normal.
- Evanescencia: lenta y poco profunda con la inversión; fuerte y rápido con células dispersas y convectivas. El programa podría utilizarlo para clasificar anuncios si un día tendrá informes SNR con el tiempo.
- Para el esporádico E en 144 MHz el autor reporta el trabajo de DF5AI y Davis y Johnson (Naturaleza 435, 2005) sobre el vínculo entre temporales y Es: fuera del tropo, pero un indicador separado mañana podría utilizar datos de relámpago.
- Reflexión terrestre en el doble salto Es: mar K = 81 y 5 S/m, campo y bosque K = 13 y 0.006 S/m, zonas urbanas K = 3 y 0.001 S/m.

## 14. Hipótesis del índice LIVETROPOVHF

Propuesta de salida, a discutir: un índice 0-10 en la misma escala que Hepburn, calculado para cada punto de la red y cada 3 horas de GFS (entonces ECMWF e ICON para comparación), y un segundo número por ruta que dice cuánto parte de la ruta pasa en un conducto útil a la banda elegida. Los pesos a continuación son mis suposiciones, para ser calibrados con los puntos.

Comentario después de Bean y Dutton y Kerr: los umbrales de perfil provienen de su tabla 6.5 y escala de sus mediciones, por lo que una capa refractiva vale 10–25 dB sobre el perfil normal. El índice se calcula primero en dB y luego toma en 0-10; los valores en ese tijera permanecen calibrados con los puntos.

Cálculo por punto, paso a paso

1. T descarga, humedad relativa, altura geopotencial y velocidad vertical a 1000, 975, 950, 925, 900, 850, 800, 700, 600 y 500 hPa, más T y 2 m de humedad, presión de tierra, altura de capa y viento a 10 m.
2. Desvelar los niveles por debajo del suelo; cálculo N y M a cada nivel (ITU-R P.453).
3. Cálculo −dN/dh en cada una de las cuatro capas de Bean y Dutton (1000–850, 850–700, 700–600, 600–500 hPa) y la gráfica como subrefractiva, normal o súper refractiva con los umbrales de su tabla; dentro de la primera capa también utilizan niveles intermedios para encontrar el más fuerte gradiente y su parte.
4. Donde dM/dh se realizó 0 encuentro base y parte superior del conducto, espesor d y déficit ΔM.
5. Comparación del espesor mínimo para la banda de la fórmula de Kerr (aproximadamente 180 m para 144 MHz, 89 m para 432 MHz, como en la tabla Hepburn); bajo el umbral el conducto todavía cuenta, pero menos.
6. Estimulo la ganancia en dB con la tabla de abajo (toma la mejor capa, no la suma de las capas, más el término de la anomalía) y convertirla: Índice = dB / 2.5, entre 0 y 10, por lo que 25 dB vale 10. Un valor negativo (sub-refracción) se muestra como 0 pero permanece en los datos de la ruta.

| Mejor situación de capa | Criterios | Ingresos estimados |
| Normal (no laminado) | −dN/dh en el rango de pestañas. 6.5 (por ejemplo, 20–60 N/km entre 1000 y 850 hPa) | 0 |
| Ligeramente por encima de lo normal | entre el límite superior de la banda normal y el umbral de super-refracción | 0 a 10 dB, en proporción |
| Superrefracción | más allá del umbral de capa (100, 80, 70, 50 N/km escalada) pero menos de 157 N/km | de 10 a 15 dB, en proporción hasta 157 N/km |
| Demasiado delgada tubería para banda | dM/dh umbral de Kerr | 15 |
| Conector útil para banda | dM/dh | 20 dB, +1 dB cada 5 M unidades de déficit, máximo 25 |
| Subrefracción | −dn/dh 0 en la capa inferior | −10 dB |
| Anomalous background (you add) | Modelo Ns minus UIT mapa climatológico Ns para ese mes | 0.2 dB por unidad N, entre −5 y +5 dB |

Calculación por ruta (entre dos localizadores): muestra el círculo máximo cada 10 km, como ya lo hace el programa para el índice en el camino, y calcula la fracción de puntos con conducto útil y la parte media de la base. El estudio CSIRO encontró útil la fracción de la ruta en el oleoducto (en las rutas AIS a 162 MHz fue 67% mayor que en los casos de control).

Añadiendo el cálculo por ruta (desde LA0BY y W3SZ): (1) estaciones de mar en comparación con la base y la parte superior del conducto, porque un conducto alto sólo sirve a los que entran; (2) un troposcatter "floor" con el método Yeh, con las esquinas iniciales tomadas del perfil de tierra; el aumento de trope en dB se elimina de esa pérdida y el resultado se compara con la pérdida que la estaciónX2 maneja para superar tabla. Así el índice también se convierte en una estimación de distancia accesible. KK6MC

Corrección para probar uno a la vez (de Bean y Dutton y LA0BY, IK3XTV, G4MCU ya en el manual), para ser mantenidos solamente si mejoran la comparación con los puntos:

- Mezcla ( importante según el autor, por observación diaria): día sobre la tierra, entre media mañana y tarde con cielo claro, o con viento en tierra sobre unos pocos m/s, los conductos en el suelo desaparecen; su contribución debe ser reducida. No en el mar.
- Tiempo del día ( importante según el autor, por observación diaria): los conductos irracionales nacen al atardecer y caen después del amanecer; es un control pronóstico de 3 horas.
- Movimiento vertical: aire subiendo a 850–700 hPa remixa las capas (corrección negativa), el aire descendiendo bajo un anticiclón las fortalece (positiva).
- Tendencia de presión: presión cuesta arriba en el camino en las últimas horas, señal más alta y estable; en disminución, menor señal (bastano 2 hPa).
- Synonym: alta presión arriba 1025 hPa, temperatura en 850 hPa mucho más arriba que en el suelo, el aire caliente y seco que fluye en el mar más frío, ruta por el mar.
- Rutas de montaña: un segundo índice "convectivo" (sección IK3XTV), con una capa límite de la tarde superior a las crestas a lo largo del camino y alta presión.

De VHF/UHF DX Libro, otras correcciones a tratar: días consecutivos de alta presión en el punto (más días, más puntos), posición en comparación con el centro del anticiclón (bonus en los bordes), gradiente de presión en el suelo como medida del viento, lluvia fuerte en las 24 horas anteriores (penalidad), nubes bajas en el borde de la capa (penalidad sólo para las bandas altas). Para la dirección de las rutas: compare el azimut de la ruta con la del isobare.

Verificación

- Archivo LIVETROPOVHF: cada anuncio comercial de más de 500 km a 144 o 432 MHz es un "evento". Compara el índice en la ruta con la frecuencia de los eventos, al mismo tiempo del día y del mes (el mismo enfoque del estudio CSIRO).
- Comparación con Hepburn y F5LEN en los mismos días: donde los tres índices se desconectan, pareces quién tenía razón según los puntos.
- Radiosundaciones de 00 y 12 UTC: para comprobar si el modelo ve las inversiones que la sonda mide.
- Casos históricos de LA0BY (6–11/11/2003, 10/12/2004, 15/10/2011, 11/2/2015, 22/11/2018): el índice recalculado en aquellos días debe ver los conductos que Hepburn no mostró.
- Limits to be declared in the manual: conducted under 100–200 m almost invisible to GFS pressure levels; reliable prediction for 2–3 days, then only trend.

## 15. Fuentes de 21 de noviembre (segunda parte)

- ITU-R P.1812 (MaTLAB/Octave implementation of I. Stevanovic, OFCOM; GitHub eeveetza/p1812, Zenodo 10.5281/zenodo.6141558). VHF/UHF método específico de la ruta, derivado de P.452: difracción "delta-Bullington", troposcatter, conductos y reflexión sobre capas, encumbrancia sobre el terreno. Útil como tercera comparación junto a P.452 en cálculo punto; los datos ΔN y N0 son productos completos de la UIT, por lo que el usuario los descarga (como por P.453).
- de Costa, Rabanal Ramirez, Dias, IET MAP 2018 (doi 10.1049/iet-map.2017.0992). 563 MHz, 27 puntos entre 5 y 30 km en terreno irregular con bosque: P.526 con 12 m de vegetación −4,3 dB y σ 8,8 dB; P.1812 con 10 m de encumbrancia +2,6 dB y σ 8,9 dB; wedge dielectric −4,8 dB y σ 9,2 dB; P.1546 +5,0 dB y σ7 Para el programa: el error residual de un buen método de difracción es alrededor de 9 dB, y la altura de las gradas en las crestas (10-12 m) cuenta.
- PETOOL (Ozgun e altri, Computer Physics Communications 182, 2011). La ecuación parabólica se divide paso a una y dos rutas con amplio propagador de ángulos (ángulos de hasta 40–45°, frente a 10–15° del estrecho); fondo con pérdidas a través del Fourier transformado mezclado; tierra a pasos. Confirma las opciones de 1.3.2 (ángulo ancho, coeficientes de suelo) e indica el cálculo bidireccional para relieves empinados.
- MathWorks. `tropopl`: pérdida de gases y pérdida de «lente» en un ambiente con capas (P.835, P.676, P.453), sin conductos: con gradientes debajo −157 N/km el modelo óptico geométrico no se aplica. El ejemplo de usos de propagación de radar para la refracción −ΔN = 7,32·e^(0,005577·Ns), el mismo informe CRPL ya en el programa. `propagationModel` Longley-Rice: predefinidos valores de polarización horizontal, conductividad 0.005 S/m, permittividad 15, reactividad 301, clima continental templado. Ningún modelo de MathWorks se ocupa de los conductos.
- UIT-R, productos digitales del grupo 3. Datos y programas de P.453, P.452, P.1812, P.2001, P.528, P.1546; los datos 'integral' forman parte de las recomendaciones; no hay licencia de redistribución declarada: continúa dejándolos descargar al usuario.
- ESA Navipedia, retraso troposférico. N = Nidrostatica + Numida; el retraso zenital seco es de unos 2,3 m y el húmedo de diez cm, que varía rápidamente. Para nosotros: el retraso húmedo estimado por las estaciones del GNSS es una medida indirecta de la humedad integrada, posible fuente futura de transmisión actual; Hoy no es utilizable.
- Bray, K2LMG, «A Method for Determining V.H.F. Station Capabilities», QST November 1961, pp. 36–40 (read in original; rechecked on November 22 on the PDF of the number of QST: all values below correspond). El autor, D. W. Bray (General Electric, Ithaca), dice que ha verificado las previsiones con meses de conexiones programadas con K2GOI y W4LTU. Evanescence dentro del tiempo de 13,5 dB alrededor de 100 millas (7 dB abajo y 7 sobre el promedio por hora), casi lineal debajo (3,5 dB a 50 millas): para una conexión sólida eliminar 7 dB. Corrección del horizonte: cada elevación del horizonte vale 69 millas (111 km) de distancia extra (menos si negativo). Ganancia de un yagi sobre 10 × boom/λ ( ratio de potencia), más sobre 4 dB de reflexión del suelo; SSB -3 dB y AM -7 dB en comparación con CW; 99% de las horas: alrededor de 180 dB a 60 millas, 190 a 90, 200 a 240 (2 m). Desde 1.3.2 la probabilidad para el período utiliza σ = 3,4 dB (±7 dB como ±2σ) en lugar de la 5 dB estimada segunda mano.
- Cálculo bidireccional (entrada con prototipo, 21 de noviembre). Ecuación parabólica de gran ángulo con terreno S RTM1 real desde Capri, 144 MHz, antena 10 m desde el suelo, atmósfera estándar; medida: potencia media entre 20 y 400 m a 80 km en la ruta hacia la correspondiente. Desde el centro de Capri y Marina Grande el oeste está cerrado ( obstáculo a +11–21°, Monte Solaro): señalando el Faito (casi detrás de EA5 y EA6, 150–160° de eje) la señal lanzada hacia España asciende 10–29 dB; señalando el Vesubio (110–125° de eje apagado, luego fuera del plan de cálculo, sólo dB indicativo). De Anacapri el oeste es libre y las victorias directas de 8 a 22 dB. Las pérdidas absolutas de 1000–1300 km del prototipo, sin embargo, no son fiables. EA3 EA5 EA6
- Barsis y Kirby, NBS Report 6751 (1961), Pikes Peak, 223 km, 100 y 751 MHz. La montaña es un terminal común de dos rutas en óptica: la distribución de las medianas por hora es la convolución de las dos rutas. Excursión a largo plazo (1-99%) de aproximadamente 14 dB, frente a 26–40 dB de un camino troposcatter de igual longitud o ángulo; sin un cambio de día apreciable. La señal es más fuerte que el troposcatter pero más débil que el borde ideal: la parte superior debe ser tratada como redondeada (aproximadamente 11,5 km). Las señales altas siempre se observan detrás de los dorsals.
- Barsis, NBS Report 7652 (1963), tres rutas por una montaña (Colorado, Alaska, Europa), 750–900 MHz. Pérdida media 10–20 dB superior al cálculo Fresnel-Kirchhoff sin tierra cerca de los terminales; la evanescencia se calcula envolviendo las dos líneas en óptica; las reflexiones del suelo cerca de los terminales empeoran la desaparición prolongada. Desde 1.3.2 la entrada en el conducto añade el redondeo de P.526.
- Barsis y Johnson, NBS TN 88 (1961). Disolución prolongada de la ola espacial, incluso en un camino más allá de una montaña (§ 5).
- Norton, «System loss in radio wave propagation», J. Res. NBS 63D (1959). Las curvas de las que Bray obtiene los nomogramas; definiciones de pérdida del sistema, transmisión y ganancia básica de antena de proceso.
- Arroz, Longley, Norton y Barsis, NBS TN 101 (1965–67), Volumen 1 y 2. borde único y obstáculo redondeado con y sin reflejos (§ 7, con el ejemplo de Pikes Peak), variabilidad a largo plazo V(0,5) y Y(q) para climas (temperado concontinental y otros). Para nosotros: el método del obstáculo redondeado y las funciones de variabilidad son la referencia a la incertidumbre calibrada.
- ITU-R P.526-16 (11/2025). Fórmula (31) de J(ν), idéntica a la del programa; Hurdle redondeado (32)–(36), utilizado ahora; métodos multi-split (Bullington y método completo § 4.5).
- UIT-R P.1812-8 (2025) y -7. Texto completo recuperado: método específico de 30 MHz a 6 GHz (delta-Bullington, troposcatter, conductos y reflexión sobre capas, ingombs). El -7 advierte que su troposcatter (de P.617-5) está equivocado bajo 66 km. Sigue siendo una posible tercera comparación.
- Paulus, «Plavatical application of the IREPS evaporation model», NOSC TR 966 (1984), y Radio Science 20 (1985). Informe leído el 22/11/2026 (DTIC ADA146528, escaneo con texto reconocido, a veces imposible de leer). Las diferencias entre el aire y el mar medidos por los buques se desplazan hacia la estabilidad: aire marino más cálido en 31–35% de los datos de los buques frente al 0,7–7% de los jalones NOAA, para los psicómetros perturbados por el buque y el SST tomados del agua de refrigeración (aproximadamente +1 °C). El modelo Jeske da 40 m y más del 15% del año y alturas que saltan de 10 a 40 m en 24 horas con tiempo estable, no confirmado por medidas de radio. Corrección propuesta ( umbrales probados 0, −0,5 y −1 °C; el −1 °C está mejor acordado con las medidas de radio de junio de 1982): si la diferencia de aire-mar es −1 °C o menos se utiliza el medido; de lo contrario calcula la altura con −1 °C y con 0 °C y, si eso con −1 °C es menor, sostiene que. Los aumentos en el lado estable se reducen, las disminuciones permanecen. En junio, Marsden 120 cuadrado: altura media de 16,8 m (climatología) a 7,6 m. Las ecuaciones del modelo no están en el informe (véase Jeske y Hitney). El programa utiliza el perfil neutro, que no tiene una exageración estable, y limita δ a 40 m: va en la misma dirección; con los SST de los modelos (no de barco) la falta de medición que Paulus corrige pesa menos.
- Levy, «Metodos de Ecuación Parabólica para la Propagación Electromagnética de Wave» (IEE 2000). En Google Books sólo hay vista previa: No leo.
- VK3FS (3fs.net.au). Introducción de calidad: altos conductos a 450–2000 m para aperturas más largas sobre el suelo; inversiones de 300–600 m en noches tranquilas y tranquilas, que desaparecen después del amanecer; fuertes inversiones costeras por la tarde; 800–1300 km típicos, 1400–1600 km con frecuencia conducido. Consistente con alertas de día/noche y techo de 2500 m.

## 16. Fuentes

Reglas y fórmulas

- [ITU-R P.453-13 (2017), índice radio refractivo: fórmula](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)y datos
- [ITU-R P.452-18 (2023), predicción de interferencia entre estaciones terrestres de más de 100 MHz: radio equivalente, β0, troposcatter, conductos](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)
- [ITU-R P.617-5, propagación más allá del horizonte para los puentes de radio](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)
- [Shi et al., "Un estudio completo sobre la longitud máxima de onda de la propagación electromagnética en diferentes bloques de evaporación", IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

Estudios sobre predicción de conductos con modelos meteorológicos

- [P. López, "A 5-year 40-km resolution global climatology of super-refraction for ground-based meteorology radar", ECMWF Tech. Memo 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO), "Tropospheric Ducting Prediction based on GFS Model Data at... the CSIRO Murchison Radio-astronomy", Observatorio arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (FMI), "Efectos de la resolución modelo sobre la predicción de las condiciones de transmisión atmosférica con Harmonie-AROME", EGUsphere 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

Previsiones de radio de aficionados

- [W. R. Hepburn, "The Hepburn Tropo Index (HTI)"](https://www.dxinfocentre.com/propagation/hti.htm)y [](https://www.dxinfocentre.com/propagation/history.htm)método history
- [W. R. Hepburn, "Lowest Usable Frequency vs. Thickness of Ducting Layer"](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, "Pronóstico de índice refractivo mundial"](https://tropo.f5len.org/WW/info.php), [nueva versión 2026](https://tropo.f5len.org/?p=504), [nota en el modelo ARPEGE](https://tropo.f5len.org/author/f5len/)

Datos

- [Open-Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api), [nota en niveles de presión](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)
- [NOAA NCEI, Integrated Global Radiosonde Archive (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RAOBget, guión de radio inundación para la Universidad de Wyoming](https://github.com/NCAR/RAOBget)

Textos clásicos (indicados por el autor del programa, para ser consultados para fórmulas finales)

- B. R. Bean, E. J. Dutton, *Radio Meteorology*, NBS Monograph 92, 1966, 435 páginas (reprint Dover 1968): perfiles verticales de N y M, gradiente de trampa bajo −157 N/km, comparación con onda de radio; capítulo 5 "Meteología de radio sínptica". Base para 2-4 pasos de cálculo por punto. Gratis: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [NIST card, doi 10.6028/NBS.MONO.92](https://www.nist.gov/node/1770911), [Copia Archivo de Internet](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf).
- L. Boithias, *Radio Wave Propagation*, McGraw-Hill 1987 (translated by D. Beeson from French, 330 pages, ISBN 0070064334; also North Oxford Academic): mechanisms beyond the horizon, common volume of scattering and loss of coupling antenna-mezzo. Sólo papel ([tarjeta de biblioteca](https://libcat.library.tamu.edu/Record/in00000014812/Description)).
- M. P. M. Hall, *Efectos de la troposfera en la comunicación radio*, IEE Electromagnetic Waves Series n. 8, Peter Peregrinus 1979, 206 páginas (ISBN 0906048257; brossura 0863410863): situaciones sinópticas y propagación VHF/UHF. Base para ingredientes sinópticos. Comercio en IET ([tarjeta](https://libcat.library.tamu.edu/Record/in00000636385)).
- H. G. Booker, W. E. Gordon, "A Theory of Radio Scattering in the Troposphere", *Proceedings of the IRE*, vol. 38, n. 4, April 1950, pp. 401–412: Teoría de la disogenidad del índice refractivo. Idea central: el poder difundido depende de la intensidad y escala de las fluctuaciones turbulentas del índice refractivo (correlación exponencial); cerca del horizonte prevalece la difracción, mayor dispersión; Según los autores la escala de turbulencia y las variaciones de índices esperadas por la meteorología son suficientes para explicar los campos medidos. Citado por 328 artículos (Scilit), sin copia de acceso gratuito. Tarjeta en Semántic Scholar ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); texto completo sobre IEEE Xplore pagado ([doi 10.1109/JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)).
- C. M. Crain, J. R. Gerhardt (Electrical Engineering Research Laboratory, University of Texas), "Measurements of the Parameters Involved in the Theory of Radio Scattering in the Troposphere", *Proceedings of the IRE*, vol. 40, n. 1, enero de 1952, pp. 50–54, doi 10.1109/JRPROC.1952].](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)
- "Los Constantes en la Ecuación para el Índice Refractivo Atmosférico en Frecuencias Radio", *Proceedings of the IRE*, vol. 41, n. 8, agosto de 1953, pp. 1035-1037: propone revisar las corrientes entonces en uso (79 K/mb y 4800 K) en la fórmula N; es de aquí que los valores 77,6 y 3,73 · 10^5 de la fórmula UIT ([Q]](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E. C. S. Megaw, "Teoría de propagación de dispersión radial", *Proceedings of the IEE Part C: Monographs*, vol. 104, n. 6, septiembre de 1957, pp. 441–455, doi 10.1049/pi-c.1957.0052: espectro de fluctuaciones de índice de la teoría de la turbulencia, trazado completo de dispersión más allá del horizonte común y el efecto de la geolitkitría](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)
- *Proceedings of the IRE*, octubre de 1955, número especial sobre propagación de dispersión (por K. A. Norton y J. B. Wiesner), con Bullington, Staras y otros: libre en PDF en [World Radio History](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf).
- D. E. Kerr (editado por), *Propagation of Short Radio Waves*, MIT Radiation Laboratory Series vol. 13, McGraw-Hill 1951 (Restampe Dover 1965 e IEE 1987): theory of evaporation and surface ducts on the sea. Copia digital: [World Radio History](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); edición IET en [IET Digital Library](https://shop.theiet.org/propag-short-radio-waves).

Medidas recientes de difusión troposférica en VHF: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Australia), "A Study of VHF Troposcatter Communication Channels in South Australia", en *Digital Signal Processing for Communication Systems*, Springer 1997, pp. 103–112 ([Springer](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)). Texto por una cuota; de resumen se refiere a la difusión, no a los conductos. Útil sólo si desea calibrar el término de troposcatter con datos de canal VHF.

Modelos numéricos y rutas marítimas (indicados por el autor del programa)

- M. Levy, * Métodos de Ecuación Parabólica para Propagación Electromagnética*, Serie IEE Electromagnetic Waves #45, 2000, 336 páginas (ISBN 0852967640): ecuación parabólica para perfiles refractivos que cambian a lo largo del camino y para la orografía; capítulos sobre propagación troposferosa, rayos y caminos, propagación en el mar. Serviría para un cálculo de campo a lo largo del camino, más allá del índice. Para el pago, también en forma electrónica de IET ([IET card](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)).
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (Universidad de Leicester): 2 mediciones de GHz en las Islas del Canal y el Canal. Muchas obras y tesis son gratuitas en el archivo universitario: [Estadísticas a largo plazo sobre el conducto de evaporación, Radio Science 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434), [cambios de señal en tres rutas marítimas, Radio Science 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710), [Titles de N. Mufti 2012 con rutas de 50 y 140 km también en VHF](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854), [tesis de I. Alam 2002 sobre reactividad calculada a partir de los datos meteorológicos, 240 MHz y.](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus, "Practical application of an evaporation model", Radio Science vol. 20 n. 4, 1985, p. 887 (not IEEE Transactions on Antennas and Propagation), doi 10.1029/RS020i004p00887 ([scheda](https://zendy.io/title/10.1029/rs020i004p00887)): height of the evaporation duct from surface weather data. Libre el informe técnico que le precede: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf).

T. Lindquist, *Wave Propagation Models in the Troposphere for Long-Range UHF/SHF Radio Connections*, master's thesis, University of Karlstad, 2 June 2020, 106 pages (supplied by the author of the program). Es una guía paso a paso, libre, para escribir un cálculo con la ecuación parabólica: solución de paso dividido (incluso en ángulos anchos), condiciones límite para tierra y mar, terreno a pasos, seguimiento de haz en perfiles lineales a veces, con cheques numéricos; cita la herramienta libre PETOOL para MATLAB. Dos datos útiles a la vez: (1) N sensibilidad a las variables del tiempo, δN ♥ 0,26 δP + 4,3 δe − 1,4 δT (hPa y K, alrededor de 20 °C), es decir, un error de 1 hPa en la presión del vapor pesa tres veces un error de 1 °C en la temperatura: es la humedad del modelo que debe ser comprobado primero; (2) Altura media del conducto de evaporación: 5 m Conclusión del autor (frecuencias 1 a 10 GHz): más allá del horizonte las conexiones confiables casi siempre requieren un conducto.

Software de propagación en el perfil del suelo (para horizontes y obstáculos, no para el tropo esperado)

- [Radio Mobile](https://ve2dbe.com/rme.html)de Roger Coudé VE2DBE: libre, Windows (también Linux y Mac con vino), modelo Longley-Rice/ITM, perfil de tierra extraído automáticamente de datos de cuota ([descarga](https://ve2dbe.com/download/download.html)).
- [SPLAT!](https://qsl.net/kd2bd/splat.html)de John Magliacane KD2BD: GPL, 20 MHz–20 GHz, Longley-Rice y ITWOM v3.0 modelos, informes de obstáculos y pérdidas en texto; última versión 1.4.2 de 2014.
- [ITM, NTIA/ITS](https://its.ntia.gov/software/itm)código oficial: el modelo Longley-Rice (20 MHz-20 GHz) con fuente y documentación, base de Radio Mobile y SPLAT!. El código es gratuito y se puede añadir al cálculo en el camino de LIVETROPOVHF (pérdida debido al suelo, que se añadirá al índice tropo). Informe original: A. G. Longley, P. L. Rice, "Prediction of Tropospheric Radio Transmission Loss Over Irregular Terrain: A Computer Method – 1968", ESSA Technical Report ERL 79-ITS 67, julio 1968 (NTIA card on historical software: X1 https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITU-R, software y datos digitales](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): mapas gratuitos de medianas anuales ΔN y N0 en la red 1,5° utilizados por P.452 y P.617 — útiles como valores climáticos de referencia para el índice.
- Pathloss (Contract Telecommunication Engineering): programa comercial para puentes de radio; no he encontrado una fuente pública que confirme lo que las recomendaciones de la UIT implementan, para comprobar.

Código GitHub reutilizable en LIVETROPOVHF

| Proyecto | ¿Qué haces? | Idioma | Uso posible para nosotros |
| [NTIA/ITM](https://github.com/NTIA/itm) | modelo Longley-Rice oficial de NTIA, con paquetes para. NET |  C++, C#  | pérdida de tierra en el camino: se integra directamente en el programa, que está en C# |
| [fuzzzeee/itm](https://github.com/fuzzzeee/itm) | ITM C++ código C# traducción |  C#  | alternativa sin bibliotecas nativas, también útil para la versión Windows 7 |
| [eeveetza/Py452](https://github.com/eeveetza/py452) | ITU-R P.452, idéntica a la versión MATLAB/Octave aprobada por la UIT-R WP 3M; versión MATLAB publicada por la Oficina Federal de Comunicaciones de Suiza | Python, MATLAB | referencia a traducir en C# las fórmulas de troposcatter y realizadas, con ejemplos de validación de la UIT |
| [bwinkel/pycraf](https://github.com/bwinkel/pycraf) | P.452 completo, P.676, perfiles SRTM; GPL v3, pero los archivos de datos de la UIT incluidos no son gratuitos para uso comercial | ♪ | control de nuestros resultados |
| [ITU-Rpy](https://github.com/inigodelportillo/ITU-Rpy) | muchas recomendaciones de la UIT-R, incluido el P.453-13, validadas con ejemplos de la UIT | ♪ | verificación de la fórmula N y los mapas climáticos de P.453 |
| [NCAR/RAOBget](https://github.com/NCAR/RAOBget) | descargar la onda de radio de la Universidad de Wyoming en formato de texto | ♪ | diagrama para descargar en C# los perfiles de las estaciones italianas |

Nota: la versión actual de P.453 es el [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), descargable gratuitamente; en el documento que usé el 13, para ser reallined.

Ya en los manuales del programa y reutilizable aquí: LA0BY (Nordic VUSHF 2019), IK3XTV ("Propagación a 2 metros" y "Propagación VHF – células convectivas y ondas gravitacionales", Revista Radio, proporcionada por el autor del programa), DF5AI/DK5YA (2003), G3YGF (QST 1983), The VHF/UHF DX Book cap. 2, G4MCU (2003) KP4MD
