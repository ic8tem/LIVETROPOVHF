LIVETROPOVHF · documento técnico 3
# Cómo formar el trope en 144 MHz y más allá

> Traducción automática por italiano: en caso de duda vale la pena la guía italiana.

Lo que sucede en el aire cuando los 144, 432 y microondas van más allá del horizonte: refracción, inversiones, conductos, dispersiones, y cómo leerlos en el mapa del programa. Versión del 9 de noviembre de 2026.

## Resumen

- Las ondas VHF y más allá viajan en los primeros kilómetros de atmósfera (troposfera) y curvan donde el índice refractivo cambia con el [3] [7] [11].
- El índice se mide con N refractivo, que depende de la presión, la temperatura y sobre todo la humedad [11].
- En la atmósfera normal hay alrededor de 40 unidades por km; cuando cae más de 157 unidades por km la onda permanece atrapada en un conducto [7] [11].
- El conducto viene de una inversión térmica con aire seco encima y húmedo debajo: anticiclón, mar, noches serenas, frentes [3] [4] [5] [13] X3Q.
- Cuanto más alta es la frecuencia, más delgada es suficiente el conducto: los microondas se abren más a menudo que el 144 [3] [7].
- Sin conducto sigue siendo el tropospher scatter: siempre presente, débil, que cae con la distancia [6].
- En el programa: alta presión + inverso + Hepburn/ F5LEN alta = condiciones probables; los puntos confirman [1] [2] [13] [14].

## 1. Donde sucede: la troposfera

La troposfera es la capa más baja de la atmósfera, hasta unos 10–12 km. Para el trope cuenta principalmente los primeros 3 km: hay vapor de agua e inversiones, y pasan las rutas de radio entre estaciones en el suelo [4]. En condiciones normales la temperatura baja con la altitud de alrededor de 6.5 °C por km [3]. Cuando, por otro lado, en una capa, la temperatura aumenta con la altitud, hablamos de la inversión térmica: es el punto de partida de casi todas las aberturas tropo fuertes.

Los VHF no se reflejan por la ionosfera como el HF (salva y esporádico, que tiene otras horas y otras distancias [5]): más allá del horizonte sólo viene si la atmósfera los dobla al suelo o los extiende.

## 2. Refraction and Refractive N

Una onda de radio dobla a través del aire con un índice de refracción de aire con otro. El índice n es muy cercano a 1, por lo que la reactividad N se utiliza, es decir, n − 1 multiplicado por un millón [11]:

```math
N = (n - 1) \cdot 10^{6} = \frac{77,6}{T} \left( P + 4810 \, \frac{e}{T} \right)
```

con P la presión en el hPa, T la temperatura en kelvin y la presión del vapor de agua en el hPa. El suelo N suele ser de 300 a 400. El segundo término, el de vapor, pesa mucho: por esta razón la humedad cuenta tanto como la temperatura [3] [11].

Lo que dobla la onda no es el valor de N pero cómo cambia con la cuota, el gradiente dN/dh:

| dN/dh (N/km) | Situación | Efecto en la onda |
| 0 | Subrefracción | La ola se aleja del suelo, el horizonte más corto |
| de 0 a 179 | Normal (estándar −39) | Doblación de luz al suelo: horizonte de radio alrededor de 4/3 de la óptica |
| de 79 a 157 | Superrefracción | La onda se dobla más, aumento de los caudales |
| menos que 1−57 | Índice | La ola se dobla tanto como la Tierra o más y permanece atrapado |

El umbral de −157 N/km es el punto donde la curvatura del radio equivale a la de la Tierra [7] [11]. De ahí el "viga terrestre equivalente" de 8497 km (4/3 del radio real) utilizado en el cálculo del Scatter [6].

## 3. Inversiones: el motor tropo

Un gradiente bajo -157 N/km requiere que N colapse en unos pocos metros de altitud. Sucede cuando por encima de una capa de aire fresco y húmedo se encuentra más caliente y seco: la temperatura aumenta y el vapor cae juntos, y ambos reducen N [3] [5]. LA0BY indica inversiones de hasta 10–15 °C por km frente a la disminución normal de 6.5 °C [3].

Las inversiones surgen de diferentes maneras:

- Confianza en anticiclones. En la alta presión el aire a altitud baja lentamente, se comprime y se calienta, mientras que en el suelo permanece fresco y húmedo aire. Es la causa de las aberturas más largas y más largas, que duran días [3] [4] [13].
- Radiación nominal. Con cielo sereno y viento débil el suelo se enfría por la noche y enfría el aire de contacto: Bajo reverso que rompe con el sol de la mañana. Aperturas nocturnas y matutinas, típicas de verano y otoño.
- Alerta de mar. Aire caliente y seco procedente del suelo sobre un mar más frío: la capa de mar permanece húmeda y fría debajo. Desde aquí las grandes rutas marítimas (Atlántico, Mediterráneo, Mar del Norte) [4] [13].
- Frentes. En frente de un frente cálido el aire caliente fluye sobre el frío; la abertura es corta y sigue el frente.
- Evaporación. Sobre el mar, en los primeros metros, la humedad baja rápidamente con la altitud: conducto muy delgado, útil sólo para microondas.

El signo de que un reverso se ve en los perfiles verticales (radndaiosoggi): aumento de temperatura y punto de rocío bajando en el mismo tramo [5]. El programa muestra una pista de esto con la capa de inversión térmica (aire de 925 hPa más cálido que en el suelo) y con el isotérmico de 850 hPa.

## 4. Superficie y conductos altos

Un conducto es una capa en la que la onda viaja rebotando entre el borde superior (inversión) y el borde inferior o inferior [7]:

- Gasoducto de superficie: tierra o mar. Las estaciones de baja altitud, especialmente en la costa, entran directamente.
- Alto oleoducto: se sitúa a altitud, por ejemplo entre 500 y 2000 m. Las estaciones en las llanuras no pueden sentir, mientras que las de la colina o en la montaña entran en el conducto. Explique los enlaces que cruzan los Alpes u otros relieves [3] [4].

Para entrar en el conducto la onda debe comenzar casi horizontal: antenas bajas en el horizonte y ningún obstáculo vecino. Dentro del conducto la atenuación es mucho menor que en el espacio libre, y las distancias alcanzan los 1000–2000 km y más allá (por ejemplo, las conexiones entre la Península Ibérica, las Islas Canarias e Inglaterra, o entre Escandinavia y el sur de Rusia) [3].

## 5. Frecuencia: por qué las microondas se abren con más frecuencia

Un conducto solo atrapa ondas bastante cortas en comparación con su espesor. Las largas olas “salen” de un conducto delgado; las cortas no. Entonces [3] [7]:

| Banda | Wavelength | ¿Qué necesitas? |
| 144 MHz |  2 m  | Gasoductos gruesos (centinaia de metros), inversiones amplias y bien marcadas: aberturas menos frecuentes pero muy extensas |
| 432 MHz | 70 cm | tuberías más delgadas: a menudo se abre cuando 144 son sólo “bienes” |
| 1296 MHz | 23 cm | Pocas inversiones débiles; rutas largas en el mar frecuentes en verano |
| 10 GHz | 3 cm | Sólo el conducto de evaporación en el mar; también cuenta la absorción de la lluvia |

Por esta razón un índice de tropo modesto ya puede significar aperturas en UHF y microondas, mientras que los 144 requieren valores más altos. Por otra parte, en el mismo conducto, los 144 tienen menos pérdidas fuera del conducto y mejor cubren las zonas fronterizas.

## 6. La dispersión del Troposfer

Incluso sin inversión la troposfera no es uniforme: Las pequeñas irregularidades de temperatura y humedad extienden una pequeña parte de la energía más allá del horizonte. Es la dispersión troposférica, siempre presente, que permite conexiones de 300 a 800 km con buenas estaciones [6]. La señal es débil y fluctuante; la pérdida crece con el ángulo de difusión, que es con la distancia y con los obstáculos en el horizonte:

```math
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
```

con θs el ángulo de difusión en grados y f en MHz [6]. El programa utiliza esta fórmula en la pestaña de cada punto para saber si un enlace se puede explicar con la dispersión o requiere un conducto (ver documento 1). Un alto valor N en el suelo mejora la dispersión [6].

No confundirse con el airscatter (reflexión en aviones: señales de unos segundos, Doppler) y el meteoro de dispersión (reflexión sobre meteoros), que el programa excluye del tropo.

## 7. El clima: cómo leerlo en el mapa

Las aberturas del tropo siguen la meteorología. Situaciones típicas [3] [4] [13]:

- Anticiclón estable, alta presión (más de 1020-1025 hPa), isobare ancho, viento pequeño: inversión de la subsidence. Las mejores aberturas siguen el borde del anticiclón, especialmente en el mar.
- Calo della presione: rupturas de inversión. G4MCU siguió el faro SK6VHF hasta que la presión cae a 1012 hPa [13].
- Hot air a altitude (isoterme at 850 hPa rise) sobre el aire fresco en el suelo: reverse.
- Las células convectivas y las ondas de gravedad (por ejemplo, bajo viento en los Alpes) pueden crear aberturas locales y cortas [4].
- Tiempo del día: las inversiones de la irradiación son más fuertes por la noche y temprano por la mañana; la subsistencia permanece también del día.

Los índices Hepburn [1] y F5LEN [2] juntan todo esto de los modelos meteorológicos: Calculan N en varias cuotas y transforman los gradientes en un número o una clase. No soy una garantía: LA0BY muestra un evento LA- UA6 que es visible a alta temperatura y no en Hepburn [3]. Para esto el programa muestra índices, isobare, temperaturas y puntos reales: si los puntos se alinean a un área de índice alto bajo un anticiclón, la apertura está en progreso. KP4MD encontró los informes SNR de WSPR a 144 MHz creciendo derecho en las ventanas proporcionadas por Hepburn [14].

## 8. Lista práctica

1. Mire el índice de tropo en su área y a lo largo de la dirección que le interesa, durante las próximas horas.
2. Check isobare: alta presión y isobare ancho sobre o cerca del camino.
3. Gire en la inversión térmica y T 850 hPa: aire caliente a la altura sobre el camino.
4. Preferir rutas en el mar y las horas de la noche y de la mañana.
5. Si estás en la colina, piensa en los conductos altos; Si estás en la costa, en la superficie.
6. Pruebe primero las bandas más altas (432, 1296): abra primero.
7. Verifica con spot y PSKReporter: líneas largas en la misma dirección confirman el conducto.

## Fuentes

1. W. R. Hepburn, DX Info Centre, “The Hepburn Tropo Index (HTI)” y “Tropospheric Ducting Forecast” páginas (dxinfocentre.com).
2. F5LEN, pronóstico para Europa y predicción mundial de la propagación tropoférica de VHF (tropo.f5len.org).
3. S. Heck LA0BY, “Extreme Tropo Propagation on 144 MHz and up”, 41° Nordic VUSHF Meeting, Skjeberg 2019. Gradiente normal e inversiones, temperatura y humedad, altas presiones, evento LA– UA6, conexiones más allá de las montañas.
4. F. Egano IK3XTV, “Propagation VHF – La contribución de células convectivas y ondas gravitacionales atmosféricas en la propagación de Tropo”. Primero 3 km, caminos marinos, células convectivas, ondas de gravedad, alta presión.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, “Very Long Distance Propation in the 144 MHz Band”, 24/7/2003. Perfiles de temperatura y puntos de rocío, inversiones, distinción de E esporádico.
6. J. N. Gannaway G3YGF, “Tropospheric Scatter Propagation”, QST noviembre 1983. Radius de la Tierra equivalente y pérdida de dispersión.
7. El VHF/UHF DX Book, capítulo 2 “Propulsión troposférica”. Zonas refractivas según dN/dh y perfiles de superficie y conductos altos (sólo las cifras).
11. Recomendación UIT-R P.453, índice radio refractivo. N fórmula y umbral de conducto a −157 N/km. Referencia general, no adjunta.
13. G4MCU, artículos “troposféricos” (g4mcu.net), 2018–2025. Condotti con alta presión, especialmente en el mar; SK6VHF y disminuir a 1012 hPa.
14. C. F. Milazzo KP4MD, “VHF/UHF WSPR Propagation Study”, 2012–2014. SNR WSPR a 144 MHz y Hepburn pronóstico.

Los números fuente son los mismos que el documento 2, Map Building.
