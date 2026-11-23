LIVETROPOVHF · Documento técnico 2
# Construcción del mapa

> Traducción automática por italiano: en caso de duda vale la pena la guía italiana.

Como nace el mapa de la ficha Mapa y lugar: la base geográfica, las proyecciones, el promedio de los índices Hepburn y F5LEN, el isobare, los puntos y las estaciones escuchando, superpuestos con capas. Versión del 9 de noviembre de 2026.

## Resumen

- El mapa está diseñado por el programa en ocho capas, cada una con su caja [8].
- Base: costa natural de la Tierra y fronteras a dos niveles de detalle [9]; proyecciones [10] Market y Azimuth.
- Física: inversiones, reactividad N y gradiente dN/dh, umbral de conducto en −157 N/km [3] [4] [7] [11].
- Índice de Trumpet: lectura calibrada de mapas Hepburn [1] y F5LEN [2], red común y promedio de las dos fuentes.
- Isobare: GFS [12] Presión a través de Open-Meteo [15], líneas cada 4 hPa, Centros H y L.
- Spot, estaciones de escucha (círculos rosa oscuros con líneas rosas siempre visibles) y decodificaciones personales en rojo.
- Leyendo el solapamiento: alta presión, conductos altos, caída de presión, creciente SNR, [3] [4] [5] [13] [14] rutas de montaña.

## 1. El principio: un mapa capa

El mapa no es una imagen fija: está diseñado por el programa, capa sobre capa, cada vez que cambias zoom, proyección, banda, tiempo de pronóstico o tema. Cada capa tiene su caja en la barra superior de capas en el mapa, por lo que sólo puede sostener lo que necesita y ver todo juntos cuando desea la imagen completa. El orden, desde abajo arriba, es éste:

|  #  | Road | Índice | Casella |
|  1  | Mapa básico | Mar, tierras, costas y fronteras | Sí. |
|  2  | Índice tropo | Media Hepburn + F5LEN, o sólo una fuente; transparencia ajustable | Sí. |
|  3  | Localizador de Grill | Campos, cuadrados y, con zoom, cuadrados | Sí. |
|  4  | Isobare | líneas de presión cada 4 hPa, centros H y L | Sí. |
|  4a  | Temperatura 850 hPa | Isothermal treated every 4 °C | Sí. |
|  4b  | Inversión térmica | Muestra de naranja donde el aire a altitud es más cálido que en el suelo | Sí. |
|  5  | PSKReporter | Estaciones receptoras de PSKReporter (círculos rosa oscuros) y, en rosa, lo que escuchan | Sí. |
|  6  | Decodificado por mí | Estaciones de decodificación con WSJT-X, JTDX o MSHV: Líneas rojas de su QTH | Sí. |
|  6a  | QSO desunti dal live | Las conexiones cerradas ( RR73, 73, RRR) leen en sus decodificaciones: línea verde entre las dos estaciones | Sí. |
|  7  | Spotlight | Conexiones: línea azul completa, amarillo cubierto para el airscatter, dos bolas | Sí. |
|  7a  | Exclusiones | Casella Excluido en menos de 100 km: el tráfico más corto no está dibujado ni listado. Casella Más de 1000 km: sólo cuadrados (estrellante): si una de las estaciones está a más de 1000 km de su QTH, sin líneas o círculos, sólo el localizador de plazas de las estaciones involucradas en lila mantenido a 45° | Sí. |
|  8  | QTH y etiquetas | Su estación, los nombres del lugar seleccionado | siempre visible |

El índice tropo está inmediatamente por encima de la base y debajo de todo lo demás: es un muestreo coloreado, y colocado más alto cubriría líneas y símbolos. La cuadrícula pasa sobre ella, así que siempre sabes dónde cae un área colorida. Los isobares son líneas delgadas y son bien leídos sobre el muestreo.

## 2. El mapa básico

Costos, límites y tierra provienen de datos vectoriales naturales de la Tierra, de dominio público [9], a escala 1:50 millones para la vista general y 1:10 millones cuando se magnifica. Los archivos están en la carpeta del programa: no hay servicios de mapa en línea. Colores del tema Despejado: mar azul claro # E2ECF8, costa azul oscura, fronteras grises claras. En los temas Negro y Noche los colores de la base se oscurecen, en los otros temas se tiñen ligeramente.

## 3. Las dos proyecciones

Todas las capas pasan por la misma función de proyección, por lo que siempre están alineadas entre sí. Usted elige entre dos proyecciones [10] (λ longitud, φ latitud, R ray de la Tierra):

Mercado. Almacene rincones y formas locales; Las distancias se extienden hacia los polos. Corte a ±85° de latitud.

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

Azimutale equidtanti centrado en su plaza. El centro (φ0, λ0) es el centro de su plaza WWL. Las distancias y direcciones medidos desde el centro son ciertas: una estación a 1200 km es de 1200 km de escala, y el ángulo con el norte es el azimut al que apuntar la antena. Cuando cambia el localizador el mapa se vuelve a centrar.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

En el azimut las líneas que en el papel son rectas (meridianos, paralelos, bordes cuadrados) se curvan. Por esta razón todas las líneas se dividen en secciones cortas, cada sección se proyecta y luego se unen los rasgos; lo mismo se aplica a los bordes del índice tropo y al isobare. El punto de antípodo en el centro no tiene una posición definida y permanece fuera de la vista.

## 4. Localizador de Grill, zoom y movimiento

La cuadrícula Maidenhead está diseñada por el programa: campos de 20° × 10° con las dos letras grandes y decoloradas en el centro, cuadrados de 2° × 1st, y con suficiente zoom subquadrati de 5′ × 2,5. Las líneas son azules y delgadas, no para cubrir el índice tropo.

Zoom con la rueda centrada en el puntero (el punto debajo del ratón permanece estable), arrastre en movimiento, doble clic para volver a toda la vista, + y - teclas, y la clave con el porcentaje para toda la vista. Los detalles se ajustan al zoom: costas más finas, sub-cuadras, etiquetas más gruesas de isobare.

## 5. Por qué las inversiones doblan las ondas

En la atmósfera baja la temperatura normalmente baja alrededor de 6.5 °C por km; El índice refractivo depende tanto de la temperatura como de la humedad [3], y donde cambia afiladamente con la altitud la onda se dobla al suelo y llega más allá de lo normal. La capa más importante es que hasta unos 3 km, donde se forman inversiones, conductos y células convectivas [4]; los caminos en el mar son favorecidos porque las masas de agua forman grandes áreas de inversión [4]. [3]

El tamaño utilizado para medir es refractivo N, es decir, (n −1) · 106 [11]:

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

T es la temperatura en kelvin, P la presión y la presión del vapor, ambos en hPa.

Cuentas tan variadas N con acciones (dN/dh). En la atmósfera estándar es aproximadamente −39 N/km; hasta −79 N/km la refracción se considera normal, además de la superrefracción, y con gradientes positivos de la subrefracción [7]. Bajo −157 N/km la ola está doblada más que la curvatura de la tierra y permanece atrapada: es el conducto [11] (conduciendo). Estas son las condiciones que los mapas Hepburn y F5LEN intentan predecir.

## 6. El índice tropo: de dos mapas a la media

Las pestañas Hepburn y F5LEN son el mostrador de control: mostrar las imágenes descargadas desde la página de cada fuente, con el tiempo de pronóstico cambiante, y servir para calibrar la lectura haciendo clic en los puntos de referencia y los colores de la leyenda. La capa del índice tropo permanece vacía hasta que al menos una de las dos calibraciones no esté activa y completa.

Hepburn. El índice Hepburn mide cuánto se espera que las ondas se pleguen sobre un área, en una escala lineal de 0 a 10 que también puede superar 10. 0 significa atmósfera estándar y sólo dispersión; 1–2 super-refracción marginal o débil; 3–4 super-refracción con áreas de conducto; El índice no tiene en cuenta el tropo de dispersión y no ve las montañas [1]. Los mapas cubren 6 días: 3 horas pasan por las primeras 36 horas, luego 6 horas, actualizadas todos los días a 18 UTC [1]; los datos de inicio (temperatura, humedad, presión) provienen del modelo global del servicio meteorológico canadiense [1]. [1]

F5LEN. Para Europa F5LEN publica 31 mapas de +3 a +192 horas (cada 3 horas hasta +48, cada 6 hasta +96, luego cada 12), con la fecha y el tiempo válido en el título, el tiempo de producción en la parte inferior derecha y una leyenda de clase: Disturb, Normal, Menor, Weak, Good, Strong, Vy Strong, Duct [2]. La predicción mundial de F5LEN se calcula por el índice de refracción de aire en la unidad N, en 40 pasos de 3 horas (unos 5 días), actualizado 4 veces al día; un clic en el mapa muestra el valor promedio y el localizador de ese punto [2].

Procediendo. Para cada paso de predicción el programa realiza estos pasos:

1. Retírense. Las imágenes de las dos previsiones se descargan en el caché junto al ejecutable, una vez para actualizar la fuente, no para cualquier rediseño. Antes de la versión final, se debe solicitar permiso a Hepburn y F5LEN.
2. Georeferencias automáticas. El programa encuentra el marco (dos líneas horizontales y dos verticales del mismo color) y, en Windows 10/11, lee con el OCR integrado en el sistema las etiquetas de eje (54N, 10W, 0, 5E...). Cortar las tiras alrededor del marco, agrandarlas, probar ambas señales cuando la letra N/S/E/W no lee y mantener la línea en la que caen las etiquetas más: las lecturas erróneas permanecen fuera. Desde aquí se puede obtener el extremo lat/lon del marco y el tipo de proyección (parrilla regular o Mercator). Los extremos se salvan y se releen si el marco cambia de tamaño. En Mac y Linux, sin OCR, los extremos de fábrica o los manuscritos son válidos en la pestaña. Alternativamente, para mapas que no tienen ejes legibles: Cada tipo de mapa (Hepburn Europa, F5LEN en todo el mundo...) tiene una calibración: algunos puntos de control con coordenadas conocidas (crosses de meridianos y paralelos, costas reconocibles) conectan píxeles a latitud y longitud, en la proyección utilizada por esa fuente. La calibración está en un archivo junto al programa y se puede rehacer si una fuente cambia el formato.
3. Lectura de color. Cada pixel se compara con la leyenda fuente y se convierte en el valor numérico más cercano. Los costos, escritos y cuadrículas dibujados en el mapa original son reconocidos como “no hay datos” y llenos de los valores cercanos.
4. Cuadrícula mundial. Cada imagen, una vez que encuentre el marco y note sus extremos, se coloca en la Tierra; el valor en cualquier punto del mundo se deriva de las imágenes que la cubren. Si más regiones cubren el mismo punto vale la pena detallar (más píxeles por grado), siempre y cuando su tiempo sea dentro de 1,5 h de esa elección; luego la hora más cercana, luego la carrera más reciente. Donde ninguna imagen cubre el punto el mapa muestra un velo gris: no hay datos, que es diferente del índice 0. Dentro del marco, un píxel sin un color de leyenda es 0 (ambiente normal); costas, fronteras y escrituras se saltan mirando el color más cercano dentro de 3 píxeles.
5. La misma escalera. Hepburn ya está en la escala 0–10 [1]: fondo oscuro = 0 y diez colores en orden (violeta, azul, verde agua, limón, amarillo, ocre, naranja, rojo, rosa, rosa claro) para 1...10, lila más allá. F5LEN utiliza clases (Disturb, Normal, Menor, Weak, Good, Strong, Vy Strong, Duct) [2], pero algunos tienen exactamente los colores Hepburn: Weak = 1, Strong = 5, Vy Strong = 7, Duct = 8; Menor es 0.5 y bueno (verde) 3.5. El multiplicador de escala, en la pestaña F5LEN, corrige el conjunto para hacerlo comparable con Hepburn.
6. A la misma hora. El timebar va de +0 a +192 h a pasos de 3 horas. Para cada paso tomas de cada fuente el pronóstico válido en el momento más cercano; Si una fuente no tiene ese paso, los medios sólo utilizan el otro y la leyenda lo indica.
7. Medios. Con el botón Media cada punto vale la pena (Hepburn + F5LEN) / 2; con Hepburn o F5LEN sólo ves una fuente, útil para entender donde las dos previsiones discrepan.
8. Dibujo. El valor está coloreado con la escala de leyendas (0 azul, 2 verde, 4 amarillo, 6 naranja, 8 rojo, 10 púrpura) y proyectado como un muestreo debajo de la cuadrícula. El 0 es transparente, por lo que no pasa nada que vea el mapa básico limpio; la transparencia general se ajusta con el cursor junto a la caja.

En la pestaña de puntos, el índice a lo largo de la ruta es el promedio de los campeonatos de valores de rejilla cada 10 km en el círculo máximo entre las dos estaciones.

## 7. Isobare

Las altas presiones estables son la primera señal a buscar para el trope [3], y las conexiones más allá de los Alpes analizados IK3XTV han sido con alta presión [4].

1. Datos. Presión a nivel del mar del modelo global GFS de NOAA [12], leído a través del servicio Open-Meteo [15] gratuito en una rejilla de 3° × 3° (de 30° a 72°) N, de 30° W a 48° E), durante las próximas 78 horas. El programa actualiza los datos de inicio y luego cada 6 horas, como el modelo de carreras.
2. Líneas. El isobare viene de la cuadrícula cada 4 hPa (...1012, 1016, 1020...) con el algoritmo de las casillas de marcha (casas de matrimonio), entonces se redondean ligeramente y se proyectan dibujados por el tracto.
3. Centros. Los máximos y mínimos locales más marcados están marcados con H (azul) y L (red) y el valor en hPa.
4. Etiquetas. El valor aparece a lo largo de cada isobara, en un fondo de color del mapa para permanecer legible por encima del muestreo.

Zona y paso. Los datos se descargan en el área que está mirando, se expande en un 25%, con un paso de 1 a 20° elegido para permanecer bajo 400 puntos (Open-Meteo cuenta cada punto como una solicitud: 600 por minuto, 5000 por hora): ampliando o moviendo el mapa de la zona ya descargado el programa la recarga más densa, a la mayoría de cada 10 minutos; si una actualización falla los datos anteriores 3 minutos después. De esta manera los isobaros y las temperaturas funcionan en cualquier lugar del mundo.

Temperaturas. La misma petición trae la temperatura a 2 m, a 925 hPa y a 850 hPa. LA0BY muestra que el evento de 22/11/2018 a UA6 fue visto en mapas de temperatura (900-3000 m), pero no en las predicciones de Hepburn [3]; DF5AI y DK5YA utilizan perfiles de temperatura vertical para reconocer las inversiones [5]. Para esto hay dos capas:

- Temperatura 850 hPa: isoterme tratar cada 4 °C (red arriba de cero, azul debajo, púrpura en cero).
- Inversión térmica: muestreo de naranja donde el aire at 925 or 850 hPa es más cálido que el de 2 m; el más intenso, el más inversión es fuerte. Es la condición física que dobla las olas al suelo [3] [11], y lo compara con el índice tropo e isobare muestra si las previsiones tienen una base meteorológica.

## 8. Lugares y estaciones

Spot. Cada punto con ambas posiciones conocidas es una línea en el círculo máximo entre las dos estaciones, divididas en secciones y proyectadas; En Mercatore aparece entonces ligeramente curvada, en el azimutale es recta si comienza desde el centro. Línea azul para el punto normal, amarillo cubierto con borde oscuro para el airscatter (§ASю, <ACS confianza o “airscatter” en el comentario), una bola para cada estación, halo ligero debajo de la línea para desmontarlo del muestreo. El lugar seleccionado es más a menudo diseñado para el último y con los dos nombres.

Escuchando. Cada PSKReporter La estación de recepción [16] es un círculo vacío rosa oscuro. Desde cada principio líneas rosas completas hacia las estaciones que han decodificado y enviado de nuevo en la red, siempre visible sin hacer clic, con los nombres siempre escritos. Al seleccionar un círculo sus líneas son más gruesas. Spot y escuchar sólo muestran la banda y el rango elegido en la lista: cambiar ancho de banda el mapa está vacío y rediseñado con los datos ya recogidos para la nueva banda.

Decidida por mí. Cada estación que decodifica con WSJT-X, JTDX o MSHV (receipt via UDP) es una línea roja llena de su QTH a la estación, con bola y nominación. Es la comparación más directa con el pronóstico: muestra donde su receptor realmente viene ahora mismo.

## 8 bis. Colores y símbolos

Cada color tiene sólo un significado, igual en todos los temas (cambiar sólo el fondo).

| Color y signo | Significado |
| Línea azul continua | Spot from DX cluster, RBN or DM4X between two stations |
| Línea amarilla con borde oscuro | Spot with airscatter comment (reflection on aircraft) |
| Círculo vacío rosa oscuro | Estación de escucha en PSKReporter (MQTT) |
| Líneas rosas del círculo, con una bola rosa en la parte inferior | Estaciones que el oyente oyó, con el nombre siempre escrito; líneas más gruesas para el oyente seleccionado |
| Línea roja y nombre rojo | Sus decodificaciones UDP (WSJT-X, JTDX, MSHV): que eres tú |
| Derrames cerebrales en línea roja | QSO en progreso con esa estación: Te llamó y el cierre aún no ha llegado, o es la llamada DX de WSJT-X/JTDX con la transmisión activada. Siga con RR73, RRR o 73 |
| Línea verde | QSO desunti dal live: cierre RR73, RRR o 73 UDP view |
| Línea negra con frontera blanca | Alguien te llama: dura hasta RR73/RRR /73 o hasta 3 minutos |
| Plazas de 45° | Tráfico con al menos una estación más allá de la corona elegida: No hay líneas, sólo cuadrados localizador |
| Búsqueda de lillas con distancia al norte | 1000, 700 y 500 km alrededor de su QTH; esa elección es más marcada |
| Círculo y punto en el color del tema | Su QTH, con nombre y propietario |
| Bola naranja con GRAVES 143,050 | El radar GRAVES ( JN27SI) |
| Muestra de azul a rojo | Índice de tropa de hepburn (0-10) o clase F5LEN, o su promedio; color más cálido, mejores condiciones |
| terciopelo gris claro | Área sin tropo datos para el tiempo elegido |
| Grey-blue lines con el valor en hPa | Isoberate every 4 hPa; H en la alta presión azul, L en rojo baja presión |
| Líneas de punto rojo, azul y púrpura | Isothermia a 850 hPa cada 4 °C: rojo sobre cero, azul bajo cero, púrpura el cero |
| Muestra naranja | Reversión térmica: aire a 925 hPa más cálido que en el suelo, más intenso con una inversión más fuerte |

En los informes sospechosos el borde de cada pestaña dice la regla en vigor: verde bueno, rojo Scarta, gris ninguna regla.

¿Cuáles son las plazas de lila cubiertas a 45°? El tráfico lejos de su QTH le interesa entender dónde está abierta la propagación, pero sus líneas, cientos de kilómetros de largo y a menudo decenas, cubren el mapa y esconden lo que cuenta para usted: las aberturas que puede explotar de su estación. Por esta razón, más allá de la corona elegida, el programa no dibuja las líneas sino color los cuadrados localizadores (2° × 1°) de las estaciones involucradas. El resultado es un mapa de «zonas activas»: donde aparecen muchas plazas cercanas de lila, hay un conducto o una abertura, incluso si no le concierne directamente; Si el área se ensancha hacia usted, es la señal para señalar la antena en esa dirección. El panel de 45° le permite ver bajo el índice tropo y la cuadrícula, así que compare a un vistazo el pronóstico (color inferior) y el tráfico real (illus). Su cuadrado QTH nunca colores, porque es el punto de partida y no un área distante. Sólo los cuadrados que están más allá de la corona están coloreados: un cuadrado cruzado por la corona, o dentro, permanece libre incluso si hay una de las dos estaciones. Para ver las líneas, apague la caja o elija una corona más grande.

Los criterios que deciden lo que viene en el mapa y lo que se descarta se explican uno por uno en el documento 1, sección 11 ter.

## 9. La barra de tiempo

A continuación en el mapa: fecha y hora de la predicción mostrada, cursor de +0 a +72 h a 6 horas, leyenda índice 0-10 con el nombre de la fuente elegida, y tiempo de la carrera modelo para isobare. El índice de Trumpet e isobare sigue el cursor; puntos, estaciones de escucha y sus decodificaciones son observaciones del momento y siempre permanecen esos “adex”. Cuando el cursor no está en +0 están atenuados, para recordar que no pertenecen al tiempo de pronóstico.

## 10. Cómo leer el solapamiento

- Alta presión + alto índice + punto a lo largo de la zona de color: la abertura está allí y es el planificado. Es hora de llamar en esa dirección.
- Índice alto pero sin lugar: o nadie está activo allí, o la predicción es demasiado optimista. La capa de escucha ayuda a distinguir: si hay receptores en el área y no sienten nada, la segunda hipótesis es más probable.
- Puntos largos fuera de las áreas de color: conducto alto, que las previsiones de superficie no ven, como en el evento LA- UA6 [3]; o otra propagación escapó del filtro (por ejemplo, E esporádico sin etiqueta). DF5AI y DK5YA distinguen tropo y E esporádico también desde la hora: las aberturas que nacen y terminan junto con un difuso E esporádico apenas son [5] tropo.
- Hepburn y F5LEN discrepan: compare las dos fuentes con los botones y vea dónde caen los puntos reales.
- Presión hacia abajo: G4MCU siguió una abertura en el faro SK6VHF hasta que la presión cayó; 1012 hPa considerado el evento terminado [13]. Mira el isobare girando la barra de tiempo: si la presión alta se retira, la abertura probablemente lo sigue.
- Señales que suben: en el estudio WSPR en 144 MHz de KP4MD la relación señal/ruido creció en las ventanas donde Hepburn predijo una mejora, y cayó cuando el pronóstico tomó [14]. si en el mismo camino crece, la apertura se está formando.
- Rutas de montaña: el índice no tiene en cuenta los relieves [1], pero las conexiones más allá de los Alpes también existen gracias a las células convectivas y olas de gravedad atmosférica [4], y LA0BY trabajó 9A y S5 a través de las montañas sin Hepburn predeciéndolo [3].

## 11. Punto abierto

- Permiso de Hepburn y F5LEN para utilizar sus mapas en el programa.
- Calibración de coeficientes de conversión F5LEN → escala 0-10.
- Calibración de puntos de control de cualquier tipo de mapa, para ser remake si las fuentes cambian de gráficos.
- capa opcional con temperatura a la altura (850 hPa) del GFS.

## Fuentes principales

1. W. R. Hepburn, DX Info Centre, “The Hepburn Tropo Index (HTI)” y “Tropospheric Ducting Forecast” páginas (dxinfocentre.com). Definición de índice y escala, valores negativos y más de 10, exclusión de dispersiones y relieves, pasos de 3 y 6 horas en 6 días, actualización a 18 UTC. Origen de los datos del modelo canadiense global: G. Meisse, AMSAT-BB, 27/08/2007.
2. F5LEN, pronóstico para Europa (tropo.f5len.org/forecasts-for-European) y predicción mundial de la propagación tropoférica de VHF (tropo.f5len.org/WW) y señaló “Nueva versión de la herramienta de predicción de propagación troposférica de VHF”. Índice de unidad N, 40 pasos de 3 horas, 4 actualizaciones por día, valor y localización por clic.
3. S. Heck LA0BY, “Extreme Tropo Propagation on 144 MHz and up”, 41° Nordic VUSHF Meeting, Skjeberg 2019. Gradiente normal 6.5 °C/km e inversiones de hasta 10–15 °C/km, función de temperatura y humedad, altas presiones estables, evento LA– UA6 visible en alta temperatura y no en Hepburn, conexiones LA–9A/ S5 más allá de las montañas.
4. F. Egano IK3XTV, “Propagation VHF – La contribución de células convectivas y ondas gravitacionales atmosféricas en la propagación de Tropo”. Importancia de los primeros 3 km, caminos marinos, células convectivas, ondas de gravedad en los Alpes, conexiones de alta presión, volumen común hasta 3-4 km.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, “Very Long Distance Propation in the 144 MHz Band”, 24/7/2003. Perfiles verticales de temperatura y punto de rocío para reconocer inversiones, uso del índice Hepburn, distinción entre tropo y E esporádico según la hora.
6. J. N. Gannaway G3YGF, “Tropospheric Scatter Propagation”, QST noviembre 1983. Radio Tierra equivalente y pérdida del camino utilizado para la tarjeta índice y spot (ver documento 1).
7. The VHF/UHF DX Book, chapter 2 “Tropospheric propagation”, pp. 2-23...2-30. Figura de zonas refractivas según dN/dh (estándar −39 N/km, límites −79 N/km y 0) y perfiles de superficie y de conductos altos. El texto de la copia suministrada no es legible: sólo se utilizaron las cifras.
8. Resumen del proyecto del 8/11/2026, proporcionado por el autor del programa. Colores de mapa, proyecciones requeridas, capas, timebar.
9. Tierra natural, datos vectoriales de costas y fronteras, dominio público (naturalearthdata.com). Referencia general, no adjunta.
10. J. P. Snyder, “Map Projections – A Working Manual”, USGS Professional Paper 1395, 1987. Formules of Mercator projections and azimutale equidtanti. Referencia general, no adjunta.
11. Recomendación UIT-R P.453, índice radio refractivo. Fórmula reactiva N y umbral de conducto a -157 N/km. Referencia general, no adjunta.
12. NOAA NCEP, Global Forecast System (GFS). Presión a nivel del mar a 0,25°, cuatro carreras por día. Referencia general, no adjunta.
13. G4MCU, artículos con etiqueta “troposférica” (g4mcu.net/tag/tropospheric), 2018–2025. Refration, conduit and disperse; ducts más frecuentes con alta presión, especialmente en el mar; beacon SK6VHF seguido hasta la caída de la presión en 1012 hPa (junio 2019).
14. C. F. Milazzo KP4MD, “VHF/UHF WSPR Propagation Study” (qsl.net/kp4md/144_mhz_wspr.htm), 2012–2014. SNR del WSPR reporta a 144 MHz creciendo en las ventanas de mejora predichas por Hepburn y disminuyendo cuando el pronóstico desaparece.
15. Open-Meteo, pronóstico del tiempo API (open-meteo.com), uso libre no comercial. Presión al nivel del mar del modelo GFS en múltiples puntos en una sola solicitud.
16. mqtt.pskreporter.info, distribución MQTT en tiempo real de los comerciales de pskreporter. info: nominación y propietario de los destinatarios (véase el documento 1).
