# Manual de predicciones

> Traducción automática del italiano: en caso de duda vale la guía italiana.

La pestaña Forecast calcula la propagación tropoférica por sí misma: un mapa índice en el área visible y la señal proporcionada entre su estación y la correspondiente. Este manual describe todos los datos, cómo se calcula y de qué fuente proviene. Los números entre corchetes se refieren a las fuentes en la parte inferior; en el programa cada datos de los resultados tiene un cómic (simbolo i) con la misma fuente y la referencia al párrafo.

## 1. Lo que hace y lo que hace

La predicción de los perfiles verticales de temperatura y humedad del modelo GFS [1, 2] deriva la reactividad de la capa de aire por capa y calcula cuánto gana la señal en comparación con una atmósfera normal. En la ruta se añade el terreno real (Copernicus GLO-30 [3]), difracción, troposcatter, conductos y ruido receptor.

No es una medida: es una estimación hecha en un modelo meteorológico con niveles distantes cientos de metros. Sirve para comparar situaciones (hoy mejor que ayer, 70 cm mejor que 2 m, este tratamiento mejor que eso), no para garantizar una conexión. En el párrafo 14 se explica cómo reunir pruebas para validarlo.


> "La propagación no puede generalizarse, sino estudiarse en relación con la morfología local. »
> — Volker Grassmann, DF5AI, reportado por Flavio Egano, IK3XTV (Propagación a 2 metros)

Es el principio que guía el cálculo punto-punto: cada ruta se calcula en su suelo, con sus horizontes, su mar y sus relieves; el mapa da la imagen general, trata la respuesta.

## 2. Idiomas comandos

- Banda: 144, 432 o 1296 MHz. Mapa y cálculo usan la banda elegida.
- Ahora: el cursor mueve el pronóstico de ahora a 72 horas por delante, dentro de 3 horas.
- Previsión de actualización (zona visible): descargar perfiles GFS en una red de hasta 240 puntos que cubren el mapa visible. El campo de rejilla (0,5° o más) depende de la ampliación.
- Parar: interrumpir una descarga o cálculo.
- Detalles para mostrar (panel derecho del mapa): tres niveles, Esencial, Normal y Completa, enciende un grupo de cajas; cada caja puede cambiar a mano. Las cajas están en tres grupos:
  - En el mapa: isolinee del gain, también 3-7 dB (tracting), isobare, inversión térmica, signos meteorológicos S ↓ ↑, ganancia de correlación/presión, zona de vista directa (párrafos 4, 5, 7 y 9 bis). Los colores siempre se quedan.
  - En el cálculo de la señal: todas las líneas del cálculo, perfil plano de la Tierra, registro de cálculo y registro de archivos detallado.
  - Correcciones y datos en vivo: correcciones meteorológicas (Misme, subsidence), contraste de aire-mar, promedio con puntos (corridoi), saltos de 2 m APRS y LoRa, más allá de la montaña sin pérdidas (párrafos 4 bis, 9 bis, 9 quater y 10 bis).
  - Esencial posee sólo isolinas, correcciones del tiempo y de los medios con manchas; Normal añade isobare, inversión, signos meteorológicos, vista directa, tierra plana, aire-mar y APRS; Completo lo enciende. El nivel sólo cambia lo que ves y las correcciones entran: los cálculos siguen siendo los mismos.
- Fuente de datos y caso histórico: desde donde vienen los perfiles y qué día miras (párrafos 3 bis y 3 ter).
- Mi estación y el corresponsal: Localizador, antenas, energía, cables, ruido (párrafos 7 y 12).
- Calcular la señal y la cobertura en la óptica: los dos cálculos sobre el terreno (párrafos 8 a 13).
- Registro de cálculo: cada paso con los números utilizados, también en el archivo Data/ forecast-calcoli.log.

## 3. De datos GFS a perfil

Para cada punto de la cuadrícula y cada hora leemos la presión sobre el terreno, temperatura y humedad a 2 m, el viento a 10 m, la altura de la capa límite, el CAPE y temperatura y humedad relativa a 1000, 975, 950, 925, 900, 850, 800, 700, 600 y 500 hPa. De 1.3.0 también se puede leer el viento a 850 hPa y la velocidad vertical a 850 y 700 hPa (párrafo 9 bis).

Los niveles se calculan con la fórmula ipsométrica y la temperatura virtual; los niveles por debajo del suelo son escasos. Para cada nivel:

- N = 77,6/T · (P + 4810·e/T), con T en kelvin, P y en hPa, y por la presión de saturación de la UIT-R P.453-14, eq. 9, con el factor de corrección EF (recontrolado en el texto oficial) [4].
- M = N + 157·h, con h en km: la reactividad modificada. Donde M sube hay un conducto.

## 3 bis. Fuente de datos

El menú Data Source elige de dónde vienen los perfiles:

- Automático (recomendado): GFS primero de NOMADS [14], el servicio NOAA que distribuye el modelo: por cada momento el programa pide dos recortes del área en formato G RIB2 y los lee por sí mismo; NOMADS no tiene límite diario, sólo uno por minuto que el programa respeta. Con rejillas amplias (de 0,5° a superior) utiliza archivos 0,5° o 1o cada 3 horas, mucho más ligero. Los puntos que NOMADS no da, o todo si NOMADS no responde, pregunte Open-Meteo [2]. Las dos fuentes dan el mismo modelo y la misma raza, para que pueda mezclarse en la misma cuadrícula.
- GFS de Open-Meteo: sólo Open-Meteo, como en 1.2 (en el límite por minuto se espera).
- GFS de NOAA NOMADS: sólo NOMADS. Más lento para descargar (dos archivos por hora, medio segundo entre una solicitud y la otra para no superar el límite NOAA), pero no consume el límite Open-Meteo.
- ICON-EU (DWD) y ECMWF IFS 0.25°, de Open-Meteo: modelos distintos al GFS, que se aplican a través de la red y para las rutas. No se mezclan con el GFS: comparar dos modelos al mismo tiempo es una manera sencilla de ver cómo el pronóstico es incierto. ICON-EU sólo cubre Europa; ECMWF no tiene todos los niveles de presión GFS (los niveles perdidos saltan) y puede no tener la capa límite.

La fuente utilizada está escrita en la línea de estado y en la primera línea de resultados (fuente de datos), con cuántos puntos han llegado de cada servicio y cuántos de la caché. El caché de disco está separado por modelo.

## 3 ter. Casos históricos

El histórico Menú de caso revisa una abertura documentada con el archivo Open-Meteo [15] GFS pronósticos: el mapa se mueve a la zona del caso, el grupo se establece y el cursor de tiempo sale del primer día del caso. Al presionar el pronóstico de actualización se descargan los perfiles de esos días; cálculo de señal funciona como para el pronóstico actual. Sirve para comparar el modelo con lo que realmente sucedió.

El primer caso es el del 14 al 15 de agosto de 2022 dicho en QST (noviembre 2022, "El mundo sobre 50 MHz" [23]): una apertura bajo una alta presión entre Manitoba y Kansas/Missouri, de 144 a 1296 MHz casi todos en FT8, con el récord canadiense a 10 GHz de 1228,56 km en Q65 y señales de hasta 1941 km. La segunda es la apertura entre el 29 de diciembre de 2021 y el 2 de enero de 2022 sobre el Tirreno y el Mediterráneo occidental en 144 MHz, de las relaciones de IC8TEM: las primeras aperturas de 29 (hasta 737 km), más ancho el 31 ( IT9GSF – EB5EA 1217 km), gran apertura el 1 de enero entre Italia, Sicilia y España. Para casos mayores (por ejemplo, septiembre de 2000) el archivo Open-Meteo no tiene datos. En el archivo no hay altura límite de capa: allí no se aplica su corrección.

## 4. El índice y los colores

Para cada capa entre dos niveles el programa compara la gota N con los umbrales de la tabla 6.5 de Bean y Dutton [5]: por ejemplo entre 1000 y 850 hPa a una disminución de hasta 60 N/km es normal, más de 100 N/km es super-refracción. Una capa super-refractiva vale 10 a 15 dB de ganancia; un conducto que atrapa a la banda de 20 a 25 dB, según el déficit de M. viento fuerte al suelo, capa de alto límite y CAPE alto reduce la ganancia, porque eliminan o rompen las capas [5, § 4.4].

El color es la ganancia estimada en dB, con una escala fija igual en todos los temas:

| Gain | Color | Significado |
| 7 dB | Nadie | atmósfera casi normal |
| 7 a 10 dB | azul | gradiente sobre normal |
| 10 a 13 dB | verde | capa superrefractiva |
| 13 a 16 dB | amarillo | capa fuerte, pero sin conducto para banda |
| 16 a 18,5 dB | naranja | capa muy fuerte o conducto demasiado delgado |
| 18,5 a 21 dB | rojo | Banda de Trampa de conducto |
| de 21 dB | Púrpura | Caucho fuerte y profundo |

Con la caja de visualización también 3-7 zonas dB entre 3 y 7 dB, donde se está formando algo pero permanece bajo la escala, se puede ver con una ligera moderación gris; los colores no cambian. Un panel dotted delimita el área cubierta por su propio pronóstico y muestra la ganancia máxima visible: si está debajo de 7 dB el mapa permanece sin colores porque la atmósfera es casi normal, no porque los datos están desaparecidos. Al descargar un nuevo clic en el pronóstico de actualización no interrumpe; para detenerlo hay Stop.

Los colores son opacos al 82%, por lo que el fondo del tema sólo los cambia. La red de localización y los nombres cuadrados están diseñados sobre los colores. Donde su predicción no está allí pero Hepburn o F5LEN dan 5 o más tarjetas de ajedrez aparecen con el color de esa fuente.

El mapa juzga cada punto solo. El cálculo de la señal en su lugar controla que la capa es continua a lo largo de la ruta (párrafo 9): una ruta que cruza muy amarilla puede dar menos de lo que el mapa te hace pensar.

## 4 bis. Pronóstico mediado por los comerciales: corredores (de 1.3.3)

Política de Derechos de Autor: Un enlace solo nunca debe dibujar un conducto. Una recepción aislada y corta puede ser la dispersión de un plano transitorio (el paquete APRS es muy corto), una apertura de unos minutos o un error, y daría "doble tuberías" que engañan. El programa reconoce un pasillo cuando, en la misma banda:

- por lo menos 3 pares de estaciones independientes (sin estación compartida) trabajan en rutas de al menos 300 km;
- las rutas son paralelas: azimut dentro de ±15°, puntos promedio dentro de 150 km del eje común;
- todo en los últimos 30 minutos, con el rayo repitiendo a distancia de al menos 10 minutos (persistencia);
- para el ARS, un salto cuenta sólo si se repite el mismo salto;
- desde 1.3.4, las rutas de más de 1500 km cuentan sólo si el rayo viene de al menos dos fuentes diferentes (por ejemplo PSKReporter y un clúster): de una sola fuente, a esas distancias en 2 m, son más a menudo EME, dispersa meteoro o localización incorrecta que tropo.
- desde 1.3.4, no cuenten las estaciones 'a raya': que en la última hora tiene al menos 5 corresponsales de más de 1000 km repartidos sobre un ventilador de al menos 150°. En 2 m el trope es direccional; un receptor que "libre" juntos Israel, Portugal, Inglaterra e Italia de 2000–3700 km, a menudo con señales positivas, tiene casi sin duda la frecuencia configurada mal. En el 17-22/11/2026 I5MZY archivo 72% de alojamiento en 2 m sobre 1000 km provenían de estaciones así; el 22/11 a las 10:53 UTC eran OH6HLH, OZ1ITS, F4LUK, OM8ABC y SP2JLR, y con el filtro el pasillo más largo descendía de 2175 km (directorio 1.3.3) a 615 km.

Fuentes: punto de racimo (no AS/ACS), PSKReporter vía MQTT, sus decodificaciones QSO y WSJT-X/JTDX/MSHV, y APRS salta en 2 m y LoRa 433/868 MHz.

2 m y LoRa. El programa lee APRS-IS en sólo lectura (rotate.aprs2.net, filtro en Europa) y extrae saltos de radio entre estaciones de ubicación conocidas (sorgente → digipeater → IGate), como el mapa VHF de DXView y APRS PropView (RF-YVY, Apache 2.0). Tres bandas valen: 2 m, LoRa 433 MHz (corredores de banda 432) y LoRa 868 MHz (corridoi que cuenta para 1296: un conducto que trae el 868 MHz también las frecuencias más altas). La banda viene de LoRa tocall (lista de aaprs-deviceid) y de las frecuencias o palabras en el comentario; un salto sólo vale la pena entre dos estaciones de la misma banda.

El punto delicado son los repetidores activos: una señal recibida a 500 km se puede llegar con dos o tres retransmisiones de digipeater. Un salto cuenta por lo tanto sólo si es entre dos transmisores reales y consecutivos:

- el camino debe ser trazado: cada excavador que ha retransmitido ha puesto su nombre, y el salto se mide entre uno y el siguiente; un alias utilizado sin nombre (W IDE1 *, W IDE2 *, alias regionales) rompe la cadena y ese tramo no cuenta;
- retransmisiones ocultas: el programa recuerda el camino que cada fuente suele preguntar (por ejemplo W IDE2 -2). Si un paquete viene con un salto en menos (W IDE2 -1) y ningún nombre lo explica, un digipeater lo retransmitió sin seguimiento y el paquete es descartado; las fuentes nunca vistas antes no cuentan;
- digipeater en medio de la carretera: si entre las dos estaciones, más o menos en la calle, hay un excavador de la misma banda activa en las últimas tres horas, el salto es descartado, porque podría haber retransmitido (es especialmente para el digipeater LoRa que retransmite sin cambiar el camino);
- los paquetes no radio (qAC, qAX, TCPIP), los dirigidos desde Internet en la radio de un IGate (terceras partes), HF y diferentes frecuencias, estaciones a altitud más de 1500 m (pallones, aviones) y salta más de 1500 km.

Efecto en el mapa (de 1.3.4). Sólo dibuja el área donde el mínimo garantizado es válido: una banda de ±60 km alrededor de la parte central de cada ruta (de 15% a 85% de su longitud), no en las estaciones de los extremos, donde el conducto puede no estar allí. Las bandas de todos los pasillos se juntan, en un color neutro (el texto) y semitransparente: las solas no se suman, y el color no confunde con el púrpura de la escala (21 dB y más allá). Las etiquetas (traits, distancia media, ganancia mínima) se escanean entre sí; pasando con el ratón en una etiqueta aparecen las secciones del pasillo.

En el área, y sólo cerca del tiempo actual (±90 minutos), el pronóstico no cae por debajo de un mínimo. El razonamiento es: si las estaciones realmente pasan, la propagación está ahí, sea lo que diga el modelo meteorológico.

Porque las rutas cortas valen poco (de 1.3.4). En 2 m, entre 300 y 600 km, dos buenas estaciones (unos cientos de vatios, antenas, FT8 o CW) conectan casi todos los días incluso con la atmósfera normal, para troposcatter: la señal difundida por las pequeñas irregularidades del aire en el volumen común de los dos paquetes. Tres o cuatro puntos a 500 km dicen que la banda funciona como de costumbre, no que hay una abertura: GEKO Magazine n. 69 ( IU3QEZ) estima para el troposcatter con elementos de flujo 2×10 de aproximadamente 440 km en SSB QRP, 510 km en SSB con 50 W, 600 km en CW QRP y 640 km en SSB con 500 W (fuentes, once vueltas). Sólo más de 600–700 km el troposcatter ya no es suficiente y una serie de conexiones paralelas realmente indica un conducto o capa. Hasta 1.3.3 un corredor de 450–600 km levantó la previsión a 9–12 dB, y el mapa mostró una mejora que no estaba allí.

Por lo tanto, el valor inicial va con la distancia media de las rutas:

- hasta 400 km: 3 dB;
- de 400 a 700 km: sal en forma lineal de 3 a 10 dB (500 km → 5,3 dB; 600 km → 7,7 dB);
- de 700 a 900 km: de 10 a 16 dB; más de 900 km permanece en 16 dB.

Bajo el 7 dB, que está bajo el principio de la escala, los restos mínimos que: el pasillo se dibuja con su etiqueta, se ve que la banda funciona, pero los colores del mapa no cambian (con la caja también muestra 3-7 dB el gris dotted). Por encima de 7 dB el valor se pesa, media y media, con el número de rutas independientes (3 trazos → 0.25; 6 o más → 1) y con la mediana S/N de relaciones (−20 dB → 0.5; 0 dB o más → 1; no conocido, como en puntos de racimo, → 0,75), a partir de 7 dB. Ejemplos: 3 rutas sin S/N en 500 km → 5,3 dB; 5 rutas en 615 km → 7,7 dB; 3 rutas sin S/N en 900 km → 11.5 dB; 3 rutas a −20 dB en 900 km → 10,4 dB; 6 rutas a 0 dB en 900 km → 16 dB.

La distancia mínima sigue siendo de 300 km: elevarla a 500 km también habría eliminado las verdaderas aberturas de corto alcance (por ejemplo, un conducto en el Tirreno a 400 km, que es reconocido por el pronóstico del tiempo y señales muy fuertes más que de la distancia sola). En el archivo I5MZY, el 22/11/2026 a las 10:53 UTC, con esta regla y el filtro de rayos los corredores más largos fueron 615 km (7.7 dB) y 540 km (6.3 dB); con las reglas de 1.3.3 alcanzó 2175 km y 16 dB.

Hasta 1.3.3 el mínimo era válido en todo el rectángulo del pasillo y dependía sólo de la distancia (de 10 dB a 300 km a 16 dB a 900 km): con muchos corredores largos juntos (por ejemplo el 22/11/2026, doce pasillos hasta 2620 km) levantaban casi toda Europa a 16 dB.

Las curvas de ganancia ya no dibujan «gocce»: las islas más pequeñas de 220 km en ambas direcciones permanecen coloreadas pero sin curva cerrada, a menos que un pasillo las confirme. Se apaga con la "Previsión mediada por los puntos".

## 5. Líneas opcionales y comparación con presión e inversión

Curvas de ganancia: líneas a 7, 10, 13, 16, 18,5 y 21 dB, los bordes de la escalera, dibujados en el campo suavizado con el método de las plazas de marcha, con la etiqueta en dB.

Isobare: presión a nivel del mar cada 4 del mismo GFS, a la hora de previsión, con los centros H y L.

Inversión térmica: curvas cubiertas con cuánto aire es 925 o 850 hPa es más cálido que el de 2 m, en +1, +3, +5 y +8 °C.

Bajo los controles una línea da el acuerdo de ganancia con la presión y con el reverso en el área visible: Coeficiente de correlación de Pearson, −1 a +1. Los conductos nacen a menudo en anticiclones (el aire que baja los calores y forma el revés), por lo tanto un acuerdo positivo con la presión es normal; pero dentro del mismo anticiclón la ganancia cambia mucho entre el mar, la costa y las montañas. El reverso por sí solo no es suficiente: dobla las olas sólo si por encima del aire es incluso más seco [5]. La línea sirve para ver, ahora por ahora, cuál de las dos mejores explica las áreas de colores. La misma comparación está escrita en el registro.

## 6. Gasoductos potenciales: doble cálculo

Los niveles de GFS varían de 200 a 1000 m: se disemina una inversión delgada y el modelo no ve el conducto. Si entre dos niveles la disminución N supera la normalidad (40 N/km), el aire es estable (gradiente térmico inferior a 3 K/km) y superior es seco (proporción de humedad específica inferior a 0.8), el programa considera un conducto potencial y lo calcula de dos maneras:

- Cálculo R: todo el exceso concentrado cae en 50 m.
- Cálculo B: la misma disminución distribuida en el espesor típico de los oleoductos de esa zona, tomada de los mapas climáticos de la UIT-R P.453 [4] (en el suelo o elevado).

Para el déficit y para la longitud de onda máxima atrapada se mantiene el valor más bajo: es una estimación prudente.

Los datos ITU-R P.453 son © UIT y pueden utilizarse para fines personales y no comerciales citando la fuente, pero no pueden ser redistribuidos: para esto no estoy en el paquete del programa. Para activar el cálculo B, simplemente descarga los mapas P.453 de la UIT en la pestaña: el programa, después de mostrar las condiciones de uso, descarga el archivo de los oleoductos del sitio de la UIT (elevated_and_surface_duct.zip) para el uso personal del usuario, extrae los archivos en Data/ P453 y encienda el cálculo sin reiniciar. Alternativamente, descarga desde la página de producto digital ITU-R Study Group 3 (itu.int, ITU-R Study Group 3, Productos digitales, voz P.453, datos de superficie y datos de conducto elevados) y coloca los siete S_OCCUR. TXT, S_THICK.TXT, S_STREN. TXT, E_OCCUR.TXT, E_base.T Sin los archivos el programa utiliza sólo el cálculo A y lo escribe en la ficha y en el registro. La relación entre el déficit A y la intensidad típica P.453 es la anomalía: Cuantas veces el oleoducto de hoy es más fuerte de lo normal. P453

## 7. Estaciones y Localizadores

El propietario puede tener 4, 6, 8 o 10 caracteres. Con 8 o 10 caracteres la posición es exacta y la cuota de suelo viene de Copernicus GLO-30: la altura de la antena es suficiente desde el suelo. Con 4 o 6 caracteres la plaza es demasiado grande: es mejor escribir la cuota de la antena en el mar; si 0 el programa sigue siendo la estimación con el suelo SRTM en el centro de la plaza más la altura del suelo y lo indica en los resultados. Para el correspondiente la altura predeterminada es de 3 m del suelo; con la cuota de mar 0 se utiliza suelo SRTM más altura.

## 8. Land, diffraction and troposcatter

El perfil de la ruta se muestra en Copernicus GLO-30 cada 30 m (más de 120 km de ancho). El radio Tierra equivalente es k = 157/(157 + ΔN), con ΔN la disminución de N en el primer km de los perfiles GFS a lo largo de la ruta [6, 7].

Alivios cercanos a estaciones (de 1.3.2, criterio del autor). P.452 paga el ángulo del horizonte linealmente, 0.573 dB por mrad: un alivio de unas pocas decenas de metros a pocos cientos metros de la antena cuesta decenas de dB, mucho más de lo que se observa. Por esta razón los relieves a 20 km de cada estación pueden añadir hasta 6 dB: el ángulo del horizonte debido a ellos se limita a la de los relieves más distantes más 10,5 mrad. Es válido para su estación y para el corresponsal. La línea «Relieves cercanos» dice cuántos dBs han quitado.

Fuente de tierra (de 1.3.2). Las cuotas provienen de Copernicus DEM GLO-30 [3]: 1 segundo arco, el mar es 0 y la costa es clara. Hasta 1.3.1, el programa utilizó las cajas Terrain Tiles 'skadi' de AWS. Cerca de la costa esos cuadrados mezclan el SSRTM con datos de fondo marino de baja resolución y extienden la parte de la isla en el mar: alrededor de Capri crea un anillo fantasma de 80–100 m entre 0,5 y 1 km de la orilla. El 21 de noviembre de 2026 IC8TEM reportó un horizonte inexistente al noroeste; la comparación punto a punto con Copernicus lo confirmó (94 m en el mar noroeste de Marina Grande vs. 0). Ese falso horizonte, de +2,6°, costó alrededor de 26 dB de troposcatter a las estaciones costeras. Los sartenes skadi permanecen sólo como reserva cuando Copernicus no responde. Los marcos de Copérnico pesan entre 15 y 30 MB y descargan una vez; los del único mar no existen y valen 0.

- Espacio libre: Lfs = 92,44 + 20 log f(GHz) + 20 log d(km) [8].
- Diffraction: Método Bullington en el perfil real más ITU-R P.452-16 § 4.2 [6] delta-Bullington corrección esférica de tierra, con pérdida esférica de la UIT-R P.526 [9].
- Primera zona de Fresnel: radio √(λ·d1·d2/d); libre si el suelo deja al menos 0,6 del radio. El filo del cuchillo [9] se muestra por cada obstáculo.
- En el diseño del perfil el radio directo es verde en vista óptica y roja si pasa bajo el suelo o debajo de la curvatura de la tierra; los puntos rojos son los obstáculos que invaden el primer área de Fresnel (línea punteada púrpura). En el mar el "punto rojo" a la altitud 0 m no es una montaña: es el mar subiendo para la curvatura. Pasar con el ratón en el radio o en un punto aparece un cómic con explicación y valores.
- Cuando un relieve cerca de la antena cierra la dirección (por ejemplo, un horizonte de decenas de grados), la difracción y troposcatter da pérdidas de cientos de dB: más de 300 dB el programa escribe "Consejo 300 dB" y "dirección cerrada por un obstáculo vecino", la señal "traducido −230 dB" y las relaciones S/N "−60 dB". En ese caso es aconsejable mirar «planta recomendada» y «Via monte».
- Troposcatter: UIT-R P.452-16 eq. 45 a 50% del tiempo [6], con distancia angular de los horizontes y N0 de los perfiles.
- Sin tropo: suma de los poderes de difracción y troposcatter, nunca bajo el espacio libre.

## 8 bis. Troposcatter: cuatro modelos e incertidumbre

Al lado de P.452 el programa calcula el troposcatter con otros tres modelos, al igual que el programa TropoLink [16], y muestra su diferencia como incertidumbre:

- NBS TN101 § 9 [25] (a partir de 1.3.5), mediana: L = 30 log f − 20 log d + F(θd) − F0 + H0 + Lgp. F(θd) es la función de atenuación del Anexo III en sus formas analíticas (Ns 301) con la corrección para Ns, H0 la función de ganancia de frecuencia de § 9.2: a 144 dBz, con parte de reflexión Ns es el de la hora perfiles GFS, los horizontes y alturas efectivos provienen del perfil de Copernicus, Lgp = 0.07·e^(0,055(Gt+Gr))) es la pérdida de acoplamiento de apertura. Es la misma forma que el ITM utiliza en su interior, pero con la reactividad de hoy en lugar de la del clima. Prueba en secciones planas en 144 MHz con antenas a 20 m: dentro de 2-4 dB de ITM entre 150 y 900 km y alrededor de 2 dB en la forma simplificada 9.2b de TN101, que no tiene H0.

- UIT-R P.617-5 [17] L = F + 22 log f + 35 log θ + 17 log d + Lc, con f en MHz, θ in mrad, d in km, F = 0.18· N0 ·e^(−hs/7,35) − 0.23·ΔN y Lc = 0.07·e^(0,055(Gt+Gr)). N0 y ΔN se suelen tomar de los mapas climáticos de la UIT; aquí utilizamos cada hora los perfiles GFS ( N0 ·e^(−hs/7,35) es la reactividad al suelo, ΔN la disminución en el primer km), así que también el troposcatter sigue el tiempo.
- RSGB VHF/UHF Handbook Fórmula, DL2FZN [18]: L = Lfs + 24,44 + 4,283 ln f + 0.06656 d. La pendiente con la distancia es casi igual a la de PROPBER (0,067 dB/km).

Volumen común (a partir de 1.3.1): es el punto donde se cruza el radio de las dos estaciones, cada una inclinada a su ángulo de horizonte, en una Tierra de radio equivalente ae = k·6371 km. Desde el TX el radio se eleva como z = hTX + x·θt + x2/(2·ae); La distancia x = (hRX − hTX + d^θr + d2/(2·ae) / θ, con θ = d/ae + θt + θr. Con horizontes planos y antenas a la misma cuota la base vale d2/(8·ae): aproximadamente 1.3 km a 300 km, 3.7 km a 500 km, 9,4 km a 800 km. Cuanto más alto es el volumen, más se seca y rara vez se extiende el aire y menos se extiende: por esta razón se agotan más de 800–1000 km el troposcatter. En los resultados se encuentran la parte y posición de la base, la reactividad, el gradiente y la humedad del GFS a esa altura, y la distancia equivalente de Bray (QST, noviembre 1961): cada horizonte adicional o menos vale aproximadamente 111 km de distancia. Referencia: presentación "Más allá del horizonte en VHF" por IU3QEZ (GEKO Magazine n. 69).

La línea Troposcatter, otros modelos muestran las dos pérdidas e incertidumbre como la mitad de la diferencia entre el modelo más optimista y el más pesimista. Son sólo comparaciones: la pérdida sin tropo sigue siendo la de P.452.

## 8 ter. Troposcatter de la tarde sobre los relieves (de 1.3.4)

IK3XTV, de Thiene ( JN55, con los Alpes a 10 km), trabaja en las tardes de verano al sur de Alemania a 400–500 km, sin vista, con señales fuertes pero con evanecencia rápida y profunda; el autor les atribuyó a "células convectivas alineadas como un tren de lentes". La observación se confirma; la explicación adoptada en el programa es otra, físicamente más firme.

Por la tarde el sol mueve el aire sobre los relieves y la capa límite se eleva a 2-3 km. En su parte superior el aire húmedo debajo se encuentra con el aire seco arriba: allí la turbulencia del índice refractivo (el tamaño que gobierna el troposcatter) es máxima. Cuando la parte superior alcanza el volumen común de la ruta, que en 400–500 km es de aproximadamente 2–3 km, se refuerza el troposcatter. Esto explica la hora de la tarde, alta presión ( cielo silencioso, calefacción fuerte) y rápida evanescencia, típica de la difusión y no de los conductos. Las células convectivas no actúan como lentes: producen turbulencia, y es la turbulencia en la parte superior de la capa de recast que difunde la señal.

Consecuencias para las previsiones de lectura:

- en la tierra la primera tarde es el peor tiempo para los bajos conductos y capas, pero no para el troposcatter más allá de las montañas: las dos cosas deben leerse por separado;
- el programa ya tiene los dos datos que sirven, la altura de la capa límite proporcionada por el modelo y la parte del volumen común de la ruta (registrarse "volumen común" en los resultados): cuando están cerca, un regalo de montaña de la tarde puede ir mejor que el cálculo en la atmósfera normal;
- desde 1.3.4 el cálculo punto-punto tiene en cuenta: si el volumen común está en la tierra, a lo largo de la ruta hay relieves de al menos 800 m, el sol es de más de 15° y la capa límite supera los 800 m, y la parte superior de la capa reformada (la tasa de suelo modelo + altura de capa límite) está a 500 m de la base del volumen común, el troposcatter gana 3 dB; entre 500 y 1000 m la ganancia lineal. La línea "Troposcatter afternoon". Con nieve a lo largo de la ruta (al menos 10 cm en el suelo según el modelo, variable de fondo de nieve de Open-Meteo o SNOD del GFS de NOMADS) el refuerzo no se aplica: en la nieve el suelo permanece frío y la capa de re-forma no crece. El valor de 3 dB es provisional y debe calibrarse con enlaces reales; se apaga con correcciones meteorológicas.

Fuentes: observaciones de F. Egano IK3XTV (Radio Rivista e www.qsl.net/ik3xtv, "Propagation on 2 meters", "Convective Cells", "Troposcatter over the Alps"), fuentes § 13.

## 9. La capa de fundición a lo largo de la ruta

Es el corazón del cálculo de señal. A lo largo de la ruta, los perfiles GFS se toman cada 25 km. Para cada capa de presión, el tramo más largo en el que la misma capa está presente en puntos consecutivos; cuenta como conducto sólo capas gruesas al menos 40 m [10].

- continuidad: desde qué km hasta qué km, y la fracción de la ruta;
- arriba y base, e inclinación en m/km;
- suelo dentro de la capa: el km donde las montañas se elevan por encima de la base;
- Terreno cubierto: en el mar el conducto es casi siempre, decide el tramo de tierra [10]. Si la tierra excede el 10% de la ruta, la fracción utilizada es el mínimo entre continuidad y cobertura de la tierra.

La ganancia de trope es la ganancia de la capa para la fracción utilizada. Si más de la mitad de los perfiles son subrefractivos, la ganancia es negativa.

## 9 bis. Correcciones climáticas: estabilidad, seguridad, huelga

A partir de 1.3.0 el perfil también da tres informaciones meteorológicas, que se pueden ver en el registro (regave «estabilidad (Misme)»), en la línea Meteo a lo largo de la ruta de los resultados y, con la caja Segni meteo, en el mapa.

- Estabilidad de la Misma [5, Capítulo 6]. Misme une el campo más allá del horizonte a un parámetro compuesto: M = a·(ge − 40 + b·ΔW), con un = 0,5 dB por N/km, el ge gradiente equivalente y la estabilidad térmica ΔW, el trabajo para elevar el aire de 1 km. El programa calcula ΔW = g·∫(θ − θ0)/θ0 dz entre el suelo y 1 km más arriba, con θ temperatura potencial; la atmósfera estándar es alrededor de 56 J/kg. El término añadido a la ganancia es 0.07 dB para J/kg más allá del estándar, limitado entre −4 y +6 dB, y cuenta sólo donde ningún conducto atrapa a la banda (el gradiente ya está en el índice Bean y Dutton). La calibración es temporal: debe verificarse con las ratios (párrafo 14).
- Tendencia y aire cuesta arriba [19]. Moler y Holden, en rutas de 125–305 km al mar a 1365 y 9380 MHz, encontraron la señal más alta de aproximadamente 2,5 dB en promedio con aire bajando que con el aumento del aire, y en un caso un refuerzo de alrededor de 20 dB durante 3 horas al comienzo de la subsistencia, seguido de una disminución de 22 dB cuando el aire volvió a converger. El GFS da la velocidad vertical a 850 y 700 hPa. Si en ambos niveles el aire cae más de 1 cm/s y hay una caída inversa o afilada en el índice refractivo potencial K (N informó adiabádicamente a 1000 hPa, que destaca el aire seco y caliente caído de la parte superior [5]), 1,5 dB se añade; si el aire se eleva en más de 5 cm/s si se despega 2.
- [20] Scirocco. El aire caliente y seco del Sahara sobre el aire marino más fresco da fuertes inversiones del Mediterráneo oriental a Gibraltar. El signo S aparece donde al 850 hPa el viento viene de 100–200° con al menos 7 m/s y la temperatura es al menos 16 °C, en el Mediterráneo. Es sólo una indicación: la ganancia decide las capas del perfil.

Sin un conducto que atrapa al grupo la ganancia total no excede 16 dB, el final del amarillo: las correcciones hacen más evidentes las capas más fuertes pero no las pasan por conductos (el rojo y el púrpura permanecen en los conductos).

- Persistencia (de 1.3.1). Las mayores aperturas vienen después de días de alta presión ya buenos (VHF/UHF DX Book; Biancomano y Leyshon 1998, regla de 12 horas; los informes de 29/12/2021–2/1/2022 crecen del primero al cuarto día). Para cada celda el programa cuenta las horas de los 24 anteriores con ganancia de al menos 13 dB y suma hasta 2 dB en proporción, sólo donde la ganancia ya es al menos 10 dB.

Cómo leer los signos en el mapa (Cambiar signos meteorológicos; desde 1.3.2 el símbolo es doble grande y pasar con el ratón sobre un signo aparece un cómic con la explicación y los valores de la célula):

| Signatura | Color | ¿Qué significa eso? | Efecto de la tropa |
|---|---|---|---|
|  ↓  | verde | subsistencia: gotas de aire (anticiclón), calienta para la compresión y seca, fortalece la inversión y la mantiene firme | favorable, +1,5 dB |
|  ↑  | rojo | aire cuesta arriba (frontera, baja presión, convección): mezcla capas bajas y rompe inversiones y conductos | desfavorable, -2 dB |
|  S  | naranja | Syrup: aire caliente y seco del Sahara sobre el aire fresco del mar | pista favorable, sin corrección: decidir las capas |

Un área llena de verde ↓ bajo un anticiclón es el signo clásico de una abertura dura; Las flechas rojas ↑ a lo largo de la línea dicen que las capas están rompiendo, aunque el color de la célula sigue siendo bueno.

La caja de correcciones de tiempo apaga estas correcciones para ver el modelo de 1.2.

Validación (párrafo 14): en las conexiones 593 a 144 MHz entre el 29 de diciembre de 2021 y el 2 de enero de 2022, en comparación con las mismas rutas y dos semanas después, el índice de separación (0,5 = ninguno, 1 = perfecto) es 0,79 sin correcciones y 0,91 con correcciones (antes del techo a 16 dB). La misma comparación muestra el límite principal: el conducto sobre el mar a baja altitud, que en aquellos días trajo las señales entre Campania y Liguria, no es visible a los niveles del GFS.

## 9 ter. Noticias 1.3.2: dos estimaciones, techo, alta presión, mar, estaciones, crestas

cálculo prudente y agresivo. La ganancia del gradiente crece de 0 dB al límite de normalidad hasta 10 dB al umbral de super-refracción. En la capa 1000-850, el umbral prudente es Bean y Dutton (100 N/km); el umbral agresivo es el umbral clásico de Super-Refracción de 79 N/km (k Ω 2; P.453 utiliza −100 N/km en los primeros 100 m), que con k50 = 157/(157 − ΔN) es k ♥ 2 (k = 2,5 corresponde a unos 94 N/km). El cálculo punto-punto muestra las dos pérdidas flanqueadas; con un conducto que atrapa la banda coincide. El mapa utiliza precaución.

El ático de 2500 m. Las carreteras y los conductos sólo se buscan hasta 2500 m sobre el suelo: cuanto más alto las antenas de las radios amateurs no las explotan. Con 1500 m la validación perdió un punto de separación y el 4% de las conexiones por encima de 16 dB, ya que en invierno los reversales de la subsidia son a menudo entre 1500 y 2500 m.

Alta presión extendida. +1 dB donde al menos 7 de las 9 células alrededor han reducido la presión al mar de 1020 hPa o más y el viento es débil en las capas bajas (10 m debajo de 5 m/s, 850 hPa debajo de 8 m/s), excepto para los movimientos ascendentes.

Peso del mar. En el cálculo punto-punto, la ganancia de las capas se multiplica por 0.8 + 0.4·μ1, donde μ1 es el factor de UIT-R P.452 (eq. 51) en el tramo continuo más largo de tierra: 1 en el mar abierto, pequeño con largas extensiones de tierra.

Estaciones y realizadas. Cuando un conducto que atrapa la banda cubre sólo parte de la ruta, la parte de cada antena se compara con la base y la parte superior del conducto al principio y final de la sección cubierta; la ganancia se multiplica por 0.5 + 0.5·(e^(−ΔhTX/300)·e^(−ΔhRX/300)), con Δh la distancia en metros fuera del conducto. Con el conducto en casi toda la ruta es el acoplamiento del § 10.

Capa y crestas. Si la parte superior de la capa de límites del GFS (tasa baja del modelo más limit_layer_height) supera las crestas de la ruta (casa máxima de 300 m, si la más alta supera 500 m), la capa pasa sobre las montañas sin romper: +1.5 dB para la fracción de las crestas cubiertas. Es la observación de IT9GSF en las aberturas de Sicilia a Alemania con Tirreno favorable.

Niveles cercanos al suelo en el mar. Con la fuente NOMADS el programa también descarga la temperatura superficial (en el mar es el SST), la temperatura y la humedad a 80 m y en la capa 30–0 hPa sobre el suelo. En los puntos del mar (tasa modelo inferior a 5 m) estos niveles se añaden al perfil, bajo el primer nivel de presión, para ver el conducto a 100–200 m que los niveles de presión no resuelven. En el suelo no se utilizan: las capas de noche y día todavía no son válidas. Open-Meteo sólo da la temperatura superficial.

Gasoducto de evaporación. Desde SST, de temperatura y humedad a 2 m se estima la altura δ del conducto de evaporación con el perfil logarítmico neutro (como en el modelo de Paulus y Jeske, sin la corrección de estabilidad): el aire al cabello del agua se satura al SST (98% para la sal), por lo tanto

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

limitado entre 0 y 40 m. Para atrapar 144 MHz tomaría alrededor de 180 m (Kerr), luego en 144 MHz el conducto de evaporación refuerza poco; de 432 MHz arriba, con bajas antenas en el mar, mucho (párrafo 11 bis). Introduzca la ecuación parabólica y en la línea Correcciones 1.3.2.

Hora del día (visita). El programa calcula la altura del sol en cada punto. De día a día sobre la tierra, con el sol de más de 15° y capa límite de más de 800 m o viento de más de 5 m/s, advierte que los conductos de tierra son re-formados; de noche, con sol bajo −3°, viento bajo 3 m/s y capa límite debajo de 300 m, advierte que con cielo sereno es posible realizado por irradiación, que caen después del amanecer. No en el mar. Por ahora son vistos, no números: la validación de diciembre no tiene suficientes días de verano para calibrarlos. Estos son dos puntos que el autor considera importantes, desde la observación diaria.

Perfil normal CRPL (alternativo). En lugar de 40 N/km fijado la disminución normal sigue el ambiente de referencia exponencial CRPL: N(h) = Ns·e^(−c·h), con −ΔN en el primer km = 7,32·e^(0,005577·Ns) (Bean y Thayer 1959). Con aire húmedo (Ns alta) la normalidad es más empinada y sirve un gradiente más fuerte para dar ganancia; en la altitud es más dulce. Resulta malo hasta que la validación diga que ayuda.

## 9 quater. Aire-mar y mares bajos: el Adriático (de 1.3.3)

Las ondas de radio no sienten el fondo marino: sienten el contraste entre el aire y la superficie del agua. La profundidad cuenta porque decide lo rápido que la temperatura del mar sigue la del aire.

El Adriático es un mar bajo. La profundidad media es de unos 250 m, pero la cuenca septentrional rara vez supera los 100 m y el Golfo de Trieste permanece por debajo de 25 m; sólo al sur desciende más de 1200 m. Una capa de agua tan delgada se calienta y se enfría en unos días, el Tirreno en semanas. Hay tres situaciones típicas:

1. Primavera y verano temprano, favorable. El mar todavía está frío, incluso para el agua fresca de los ríos Alpino y Po; llega aire caliente y seco de la llanura o los Balcanes. El aire caliente sobre el agua fría forma una inversión en el mar (oleoducto de absorción): es la condición clásica de las aberturas entre Italia, Eslovenia y Croacia.
2. Otoño e invierno con bora, desfavorable. Aire frío sobre un mar más cálido: la capa baja se vuelve inestable, prensa y el conducto se rompe.
3. Después de un bora, azar. Sólo porque el mar es bajo, un bora lo enfría en unos pocos días. Si entras en un anticiclón con aire caliente, el contraste de aire-mar es fuerte y el conducto es más robusto de lo habitual.

El Adriático es también un canal cerrado entre Apeninos y Dinari, a menos de 200 km de ancho: la capa de mar permanece confinada, y las rutas a lo largo del eje (Puglia–Venezia–Trieste, unos 700 km) permanecen en el mar, la mejor condición para un largo conducto.

Lo que el programa calcula. En los puntos del mar, con las correcciones del tiempo en y la caja «Contrasto aria-mare»:

| Estado | Corrección |
| --- | --- |
| temperatura potencial en 925 hPa (o 950) sobre el SST de 4 K | ¿Qué? |
| más allá del SST de 7 K | +2 dB |
| con humedad del aire de 925 hPa (más del 80%) | −0,5 dB sobre estos bonos |
| mar refrigerado de 1,5 °C o más en 72 horas antes, con contraste favorable | ¿Qué? |
| mar de aire más cálido a 2 m de 2 °C ( mezcla) | −1.5 dB |
| mar más cálido de 4 °C | −3 dB |

La corrección total permanece entre −3 y +3 dB y, sin conducto, la ganancia permanece bajo el techo amarillo. Los umbrales son temporales: en la atmósfera estándar la temperatura potencial a 925 hPa excede la del mar de unos 2–3 K, entonces 4 K ya indica un revés.

La memoria del SST. Los modelos mantienen la temperatura del mar casi estable durante la predicción. La disminución reciente se deriva del análisis de los tres días antes (Open-Meteo, sólo la temperatura superficial, sólo las células marinas): máximo de 72 horas antes de menos el valor actual. Si el servicio no responde, el bono de memoria no se aplica.

Limites. El SST de los modelos debajo de la costa y en el Adriático superior es grueso; la pluma fría del Po, por ejemplo, se resuelve mal. Batimetry no entra en el cálculo: las fuentes siempre dan el SST, que ya resume el efecto del fondo marino. El contraste de aire-mar se superpone en parte al final de la estabilidad de Misme (§ 9 bis); por esto los bonos son pequeños. Los umbrales irán tarados con los pasillos del lugar y el QSO a través del Adriático.

## 10. Realizado en toda la ruta

Si la capa atrapa la banda en al menos el 80% de la fracción utilizada y las estaciones no están a la vista, la pérdida en el conducto también se calcula: espacio libre, más el acoplamiento de las antenas (3 dB si están dentro del conducto, más si están por encima o por debajo), más la atenuación residual γd = 5·10−5·ae·f^(1/3) dB/mrad de I52. Si es menos que la pérdida sin tropo se convierte en la pérdida esperada. [6]

Una banda permanece en el conducto sólo si su longitud de onda no excede λmax = 2√2·(2/3)·(ΔM·10−6)·d (Kerr, eq. 17 [11]). Para esto la misma capa puede atrapar los 23 cm y 70 cm pero no los 2 m. Sin embargo, el límite no es neto: Kerr advierte que incluso olas varias veces más sufren mucho del conducto, y Hepburn y el VHF/UHF DX Book hablan de los bordes "esféricos" y los conductos "perdonantes". A partir de 1.3.1 el programa utiliza un conducto blando: si λmax es entre λmax y 3·λmax, la ganancia pasa gradualmente de 15 dB (λ = 3·λmax) a la ganancia total del conducto (λ = λmax), con el factor s = (λmax/λ − 1/3)/(2/3). En ese caso se permiten los colores del conducto. El tronco y la línea Meteo a lo largo de la ruta indican los puntos con conducto suave.

## 10 bis. Via Monte

Cuando el corresponsal está cerrado detrás del horizonte, el programa busca la mejor dirección para señalar las antenas: la pendiente iluminada difunde la señal como un objetivo de radar. Hace que el círculo del horizonte cada 5o (excepto ±15° alrededor del corresponsal) y en cada dirección toma el punto de horizonte iluminado, más de 100 m y entre 1 y 60 km. Una estimación rápida (espacio libre, conversión bitáctica y 10 dB para cada nivel de horizonte desde el difusor al corresponsal) elige los 4 más prometedores, al menos 15° cada uno del otro; para cada uno:

- tratar 1, estación → alivio, en el espacio libre;
- tratados 2, relieve → corresponsal, calculado como si el alivio transmitido a partir de 10 m sobre la parte superior, con la misma predicción (sin ecuación parabólica);
- conversión con la ecuación de radar biestática, con σ = σ0·A, σ0 = −15 dB (montaña en VHF/UHF) y A el área iluminada por el haz R1 (máximo 20 km2):

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

La línea "Punto recomendado" indica el azimut de la mejor ruta, si cuesta menos que la directa. A 144 MHz, con un alivio a 20 km y un yagi de 15°, la carretera de montaña cuesta alrededor de 45 dB más que la 2 ruta por sí misma: sólo ocurre cuando el directo está cerrado por obstáculos vecinos. La estimación es bruta (±10 dB), especialmente para σ0.


Sin pérdida a la montaña (opción, de 1.3.2). Con la caja «Via monte senza perdite fino al monte» se considera nula la caída entre la estación y el punto de difracción: es como llevar la estación a la parte superior ( antena a 10 m sobre el punto, la misma potencia, antena y cable). El horizonte elige entonces el punto con el horizonte más libre hacia el corresponsal (en paridad, el más alto) y el resultado muestra la ruta desde el punto al corresponsal, con señal y S/N, junto al total con la difusión. Es un límite optimista: dice si la dirección se abre cuando la montaña levanta la señal bien, o donde ir portable. Cada punto se da con el diez caracteres.

## 10 ter. Entrada a un conducto distante

Si el conducto que atrapa a la banda comienza lejos, más allá del horizonte de la estación, la señal debe llegar. El programa lleva la señal al punto de partida, en medio del conducto, para la difusión: a lo largo del perfil de Copérnico GLO-30 busca el borde con el parámetro más alto de Fresnel (ITU-R P.526), con la inflamación de la tierra equivalente,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

más la atenuación debido a la curvatura de la parte superior ( obstáculo redondeado, P.526-16 § 4.2), con el radio R estimado del suelo Copernicus GLO-30 a ±1 km del borde,

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

(para m·n ≤ 4 es la forma 34b de la recomendación). Barsis (NBS, 1963) medida en tres largas rutas más allá de una montaña (Colorado, Alaska, Europa) pérdidas promedio 10–20 dB superiores al borde ideal de Fresnel-Kirchhoff: el redondeo explica mucho (Barsis y Kirby, 1961: Pikes Peak con un radio de unos 11,5 km). El resultado se compara con la difusión en suelo esférico liso; es el peor. Luego la señal viaja en el conducto: espacio libre en toda la ruta, atenuación residual γd de P.452 en la sección en el conducto y acoplamiento del corresponsal (3 dB dentro, 1 dB cada 10 m fuera). Si esta pérdida es inferior a la pérdida esperada se convierte en el mecanismo ("conduct (input by diffraction)"). Es la premisa del autor sobre la geografía italiana: con un obstáculo cercano muy por debajo de la línea, v es negativo y la entrada cuesta poco; con un frente dorsal cuesta 15–30 dB.

## 10 quater. ITM (Longley-Rice) y salida del conducto

Desde 1.3.2 el programa contiene el modelo de Terrain Irregular 1.2.2 de NTIA/ITS (Longley y Rice, ESSA ERL 79-ITS 67, 1968; Hufford, Longley y Kissick, NTIA 82-100, 1982), traducido al C# por el código oficial del gobierno estadounidense, que está libre de derechos. La traducción da los mismos números que el original: la compilación automática la compara cada vez con el código NTIA C++ en cinco casos de prueba (bajo 0.01 dB).

El ITM calcula la pérdida en atmósfera normal en el perfil real Copernicus GLO-30: visto con reflexión desde el suelo, la diffracción (spigol y tierra esférica pesada sobre la rugosidad del suelo Δh, es decir, la diferencia entre el percentil 10 y 90° de las cuotas alrededor de la recta de los cuadrados mínimos) y troposcatter. La reactividad terrestre se reduce a la parte media de la ruta, $N_s = N_0\,e^{-h/9460}$, y da la curvatura de la tierra equivalente,

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

El programa utiliza el clima radio de la ruta elegida como en § 10 sexies (hasta 1.3.4 siempre el mar templado, con N0 320 en la tierra y 350 en el mar), con N0 de la reactividad media del lugar (suelo εr 15, σ 0,005 S/m; εr de mar 81, σ 5 S/m), polarización horizontal y variabilidad a lo largo del tiempo sin el perfil conocido. Muestra la pérdida excedida en el 50% del tiempo y la que tienes en 10% y 1% del mejor tiempo. ITM no modela los conductos: su variabilidad con el tiempo es estadística, por el clima. Sirve como segundo cálculo de la base de referencia junto a P.452; la diferencia entre ambos da una idea de incertidumbre.

El segundo uso es el tramo final después de un conducto que termina antes del correspondiente. En el punto de salida, se coloca una antena virtual a mitad del conducto y el ITM calcula el tracto al correspondiente, como exceso sobre el espacio libre al 50% del tiempo. La pérdida total es

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,	heta_{condotto} + A_{ITM}(d_{uscita} 	o RX)
```

donde A_TX es el acoplamiento de la antena (3 dB dentro, 1 dB cada 10 m fuera) si el conducto comienza cerca, de lo contrario la difracción de entrada de § 10 ter. Es el límite que el autor se ha dado: continuidad de la capa más, a lo sumo, atenuación residual. Si es inferior a la pérdida esperada se convierte en el mecanismo ("conduct (exit and final stretch ITM)").

## 10 quinquies. Profundización: islas, estrechos y discontinuidad del mar terrestre

Un ejemplo del Golfo de Nápoles (Anacapri, JN70CN) hacia los espectáculos mediterráneos occidentales donde las rutas cruzan las islas:

| Destino | Azimut | Donde cruza las islas | Obstáculo |
| --- | --- | --- | --- |
| Barcelona |  279°  | norte de Cerdeña (Gallura) a 41,05° N, entre 420 y 490 km | a unos 30 km al sur del Bocche di Bonifacio; el Limbara (1359 m) a unos 20 km de la línea |
| Bocche di Bonifacio |  282°  | cerca de 41,3° N | unos 11 km |
| Marsella |  296°  | Córcega central a 42,2° N | montañas hasta 2700 m: un conducto debajo de 2 km interrumpe |
| Valencia, Baleares |  260–265°  | Central Cerdeña | relieves internos |

Para Barcelona es suficiente apuntar a 282° en lugar de 279° porque el eje pasa de las bocas: 3o, dentro del haz de un yagi normal.

¿Las bocas son un estrecho? Para los conductos bajos, bajo unos pocos cientos de metros, sí: las islas están amuralladas, y sobre la tierra, en el día, la calefacción rompe la inversión. El estrecho en sí es lo suficientemente ancho: unos 11 km, frente a una primera zona de Fresnel, menos de un kilómetro de ancho medio de una ruta de 1000 km en 144 MHz. El problema es el tiempo. Los Mouths son una diversión para el viento: La capa inferior se acelera y se retira la capa inferior. A menudo el pasaje está abierto como geometría pero perturbado como atmósfera. Con un anticiclón estable y poco viento puede ser el buen paso.

Disparidad de la Tierra. Cuentan. Se interrumpe un conducto formado en la costa tirorense: la señal sale del conducto (la sección después de la salida, § 10 quater) y debe regresar a otro conducto en el mar de las Baleares, pagando nuevamente el acoplamiento. Los conductos a altitud, por encima de 1500 m, pasan por la Gallura sin darse cuenta; para Córcega central no se aplica.

En la práctica: si la ruta cruza una isla montañosa, pruebe cierto grado de corrección hacia el tramo del mar libre, pero no cuente en el camino con viento fuerte en el estrecho.

## 10 sexies. Clima de radio TN101 y variabilidad del tiempo (a partir de 1.3.5)

El TN101 § 10 [25] describe cómo la pérdida de una ruta más allá del horizonte varía durante el largo período alrededor de la mediana, con diferentes curvas por el clima: la función Y(q) dice cuántos dB la pérdida cae en el q% del mejor tiempo. El ITM contiene esas curvas para siete climas. El clima cuenta mucho: en el mar en el clima marino templado, a 144 MHz, en el 10% del tiempo que gana muchos más dB que en la tierra en el clima continental, porque los conductos son más frecuentes.

El programa ya no fija el clima: Lo elige por cada regalo desde el punto promedio, por lo que funciona por todo el mundo y no sólo en Europa. Descarga desde Open-Meteo [26] tres años de valores diarios de reanálisis ERA5 (temperatura, humedad y presión del suelo, lluvia) para la célula de 0,5° del punto medio, una vez solamente; los valores permanecen en Data/CLIMA. Restauración del suelo

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

día a día, luego el promedio anual y la excursión entre promedios mensuales, humedad promedio y lluvia anual. Las reglas siguen las descripciones de los climas del TN101 § 10.2; los límites entre un clima y el otro son un criterio del autor:

- más de 60° de latitud: continental templado (el TN101 no tiene curvas para el clima polar);
- menos de 250 mm de lluvia por año y humedad media por debajo del 50%: desierto (Ns típico 280);
- dentro de 20° desde el Ecuador, Ns de al menos 350 y excursión hasta 30: ecuatorial (Ns típico 360);
- dentro de 32° y excursión de al menos 60: continental subtropical, que es monzón (excursión típica 60–100);
- entre 10° y 32° con Ns de al menos 340: marina subtropical (Ns típico 370);
- ambos horizontes en el mar, más de 20°: marinos templados en el mar (es la definición del TN101);
- excursión hasta 30 cerca del mar (mar en la ruta o humedad promedio del 75%): marítimo templado en la tierra;
- de otro modo continental templado (excursión típica 20–40).

N0 para ITM es el promedio reducido al nivel del mar, N0 = Ns·e^(h/9460). Sin red utilizas una regla de reserva con la latitud sola y la fracción del mar, y la línea lo dice.

Pruebas con datos ERA5 2023–2025: Capri Ns 339 con excursión 45 (continental temped on land, sea temperate on the sea when both horizons are on the sea); London 327 and 16 Vienna 319 and 24 Abidjan 381 and 11 (equatorial); Miami 365 and 36 (subtropical husband); Delhi 337 and 82 (subtropical concontinental); Riad 278 and 37, 90 mm rain (desert). En Chile: Antofagasta 341 y 19 (marido subtropical: la costa del Atacama, con la casi fija baja inversa del Pacífico), Calama 232 en el suelo, 2257 m, 14 mm de lluvia (deserte), Valparaíso 331 y 11 (estancia de temperatura), Punta Arenas 313 y 5 (mar de temperatura).

La línea "Radio clima ( TN101)" muestra el clima, la razón, Ns con la mitad de la excursión y Y: cuántos dB ganan en 10% y 1% del tiempo y cuántos pierden en 90%. Se trata de estadísticas a largo plazo y se mantiene separado de la previsión: la pérdida esperada sigue el tiempo de hoy con los perfiles del GFS, y suma las dos cosas contarían dos veces la misma propagación anormal.

## 11. Comparación con la UIT-R P.452

La línea P.452 aplica el método ITU para los conductos y la reflexión sobre capas (P.452-16 § 4.5 [6]) con la mediana ΔN del GFS en lugar del climatológico y p = 20% como constante de calibración, como en el estudio CSIRO [10]. Es sólo una comparación: la pérdida prevista sigue siendo la del párrafo 9.

## 11 bis. Ecuación parabólica (a partir de 1.3.1)

La Línea Parabólica de Ecuación es un cuarto cálculo, sólo de comparación, hecho como programas profesionales para los conductos (PETOOL; el marco de la proagación de ondas de M. Lytaev, licencia MIT): en lugar de estimar la ganancia de las capas, avanza la onda a lo largo de la ruta con el método Fourier split-step dentro del perfil refractivo GFS M modificado, tomado desde el punto de pronóstico más cercano a cada distancia. Los niveles GFS agregan los saltos estimados de los conductos potenciales (calculando A, 50 m en la mitad de capa). El suelo Copernicus GLO-30 entra en pasos (bajo el suelo el campo está nulo), el suelo es conductor y el haz de la antena es un Gausiano con la apertura estimada de la ganancia. El cálculo ve los conductos, los que pierden, la difracción y el acoplamiento de la antena; Él no ve el troposcatter, que se añade aparte. Siguiente es el «cheque de perfil»: el mismo con atmósfera estándar menos el que tiene el perfil esperado.

De 1.3.2:

- el propagador está en ángulo ancho, con la fase exacta del espacio libre:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- el fondo sobre el mar y la llanura costera a cero altura refleja con el coeficiente de Fresnel en polarización horizontal, εc = εr − j·60·λ·σ (sea εr = 81 y σ = 5 S/m; país 13 y 0,006 S/m), en el ángulo claro de la ruta:

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- la rugosidad del mar reduce la constante reflexión según Miller y Brown, con la altura efectiva de las olas del viento a 10 m (σh = 0,0051·U2):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- en el mar el perfil M incluye el conducto de evaporación (párrafo 9 ter) con el perfil logarítmico neutro:

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

Prueba con el prototipo de antena (10 m en el mar, ganar en la atmósfera estándar a 100–300 km): con δ = 10–30 m el conducto de evaporación da 1–3 dB a 144 MHz, 8–37 dB a 432 MHz y 21–66 dB a 1296 MHz. En 144 MHz en polarización horizontal, el mar refleja casi como un metal (respuesta sometida más de 0,99 a unas pocas décimas de grado) y la rugosidad cuenta poco: el nuevo fondo cambia unas pocas décimas de dB; cuenta más de 432 MHz arriba.

Límites: por encima de la tierra el perfil vertical sigue siendo el de los niveles GFS, por lo que los conductos delgados permanecen invisibles; el suelo a los pasos se aproxima; el haz permanece dentro de la sexta.

## 12. antenas, señal y ruido

- Yagi DL6WU: ganar 9.5 + 7.8·log10(boom/λ) dBd (2.35 dB para doble auge, medidas DL6WU en comunicaciones VHF 3/1982), más 2.15 para [12] dBi.
- EIRP = potencia - cable + ganancia TX.
- Señal recibida = EIRP - pérdida esperada + ganancia RX - cable RX. S-meter IARU: S9 = −93 dBm, 6 dB por unidad.
- Ruido externo según UIT-R P.372 [13]: galáctico (Fa = 52 − 23 log f) más artificial según el medio ambiente (ciudad, residencial, rural, rural tranquilo) o una temperatura de antena manuscrita.
- Tsys = Ta + Tpre + [290(Lc −1) + 290(Fr −1)·Lc] / Gpre a los terminales de la antena (Friis formula). Desde 1.3.5 la estación receptora siempre tiene un preamplificador en antena, parámetro fijo: ruido número 0.8 dB (Tpre = 59 K) y ganar 20 dB. Detrás hay el cable (pérdida Lc) y la radio, con el ruido básico P. 6 dB. Con 1 dB de cable el cable y las entradas de radio de aproximadamente 1160 K a 12 K: el ruido decide el cielo, el medio ambiente y el preamplificador. La ganancia de 20 dB es un valor típico elegido por el programa.
- S/N en SSB, CW y FT8 en la banda de cada manera, en comparación con el umbral fijado.
- Probabilidad para el período (a partir de 1.3.2): la evanescencia dentro de la hora es una distribución normal del S/N alrededor del valor medio esperado por hora. Bray (QST, noviembre 1961, p. 39, en las curvas de Norton 1959) da una amplitud de 13,5 dB alrededor de 100 millas, es decir, alrededor de 7 dB por debajo y 7 por encima del promedio, que cae casi recto por debajo de 100 millas (3,5 dB a 50 millas); para un enlace 'sólido' toma 7 dB del presupuesto. Tomado ±7 dB como ±2σ: σ = 3,4 dB más de 160 km, proporcional a la distancia de abajo. La probabilidad de que un período exceda el umbral del camino es

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  con Ё normal distribution function: margin 0 dB = 50%, +3,4 dB = 84%, +7 dB (el margen 'solid' de Bray) = 98%. No incluye la incertidumbre del modelo, que es mayor.
- Vías digitales: S/N en 2500 Hz en comparación con los umbrales medidos al 50% de decodificación, sin información previa (AP). FT8 −21 dB y FT4 −17,5 dB sobre el ruido blanco; cuando el mecanismo es troposcatter utilizar los umbrales con evanecencia "medio de latitud perturbada", FT8 -18,6 dB y FT4 −15,2 dB [21]. Q65 -15C −22,2 dB, Q65 -30B −24,8 En la dispersión de aviones el Doppler puede superar los tonos de Q65 -60A (1,7 Hz): hay mejor que 30B o 15C. JT65A −25 dB [22]. Decodificación AP (nombre del ya conocido o final de QSO) da alrededor de 2 dB extra para FT8 y 1,5 a 2.6 dB para Q65; no entra en el umbral porque no es válido para todos Los valores «−24 dB» que a menudo se leen para FT8 son el límite de las relaciones WSJT-X o decodificación AP, no el umbral al 50%. Q65 [24]

## 13. Tiempos de respuesta y datos

Cada cálculo mide su propio tiempo: Perfiles GFS (network, analysis, pausas obligatorias de 20 s entre los lotes para el límite Open-Meteo), terreno Copernicus GLO-30 (con la descarga de los paneles, 15–30 MB cada uno, sólo la primera vez), cálculo. La línea Tiempo de Respuesta los resume; en el registro las filas comienzan con TEMPI. Al actualizar la cuadrícula, el registro también reporta la carrera GFS utilizada, cuando se puso a disposición y la edad de los datos.

Desde 1.3.2 también se puede elegir ICON-2I, el modelo de la Agencia ItaliaMeteo y Arpae en Italia a unos 2.2 km, a través de Open-Meteo (model italia_meteo_arpae_icon_2i, dos carreras por día, 3 días). Sólo cubre Italia y los mares cercanos y tiene menos niveles bajos del GFS (1000, 925, 850, 700, 500 hPa): para los conductos a altitud no es mejor que el GFS, pero la rejilla fina describe bien las costas, islas y el mar a 2 m, es decir, el conducto de evaporación. Las variables que un modelo no tiene (límite de estrategia, temperatura de la superficie...) se eliminan de la solicitud. Desde 1.3.2 la fuente automática sólo utiliza NOMADS; Open-Meteo sirve si NOMADS no responde y para casos históricos. Con el cuadro "Update on its own" (lighted by default) el pronóstico se descargará sin hacer clic: en la apertura, en cada nueva carrera GFS en NOMADS (aproximadamente 5 horas después del tiempo de la carrera) y cuando el área visible está fuera por más de un cuarto de la red; en la mayoría un intento cada 15 minutos, nunca con un caso histórico.

Las solicitudes en Open-Meteo tienen límites por minuto, por hora y por día para cada dirección de Internet. Los perfiles descargados se guardan en el disco (Dati/cache-gfs) para cada golpe y modelo: con la misma carrera, que cambia cada 6 horas, los puntos ya descargados no piden de nuevo. Con la fuente automática comienzas desde NOMADS y Open-Meteo sólo sirven para puntos perdidos; con el único Open-Meteo, en el límite de tiempo o paradas diarias inmediatamente y dice cuándo intentarlo de nuevo. Los casos históricos no están caché.

## 14. Validar el modelo

El pronóstico debe compararse con conexiones reales. El registro de cálculo mantiene cada número; el archivo del programa mantiene anuncios y decodificaciones. Para 432 y 1296 MHz, donde los puntos son pocos, necesitan informes de los operadores: quién hizo la conexión, a qué hora, con qué señal. Compare por muchas horas la ganancia esperada con la señal medida dice si los umbrales deben ser movidos.

## Fuentes

1. NOAA/NCEP, Global Forecast System (GFS), modelo global a 0.25°, cuatro carreras por día.
2. Open-Meteo, API de predicción (open-meteo.com), modelo gfs_seamless, variables a niveles de presión y metadatos de carreras.
3. Copernicus DEM GLO-30 (ESA; WorldDEM © DLR e.V. 2010–2014 e © Airbus Defence and Space GmbH 2014–2018, suministrado como parte de Copernicus por la Unión Europea y ESA), 1 segundo arco, marcos GeoTIFF en el registro abierto de Amazon Web Services. Reserva: NASA/USGS Copernicus GLO-30 en Terrain Tiles "skadi" de AWS.
4. UIT-R P.453, El índice radio refractivo: su fecha de fórmula y refractividad, con mapas de canales digitales (Study Group 3, © UIT, uso personal y no comercial).
5. B. R. Bean, E. J. Dutton, Radio Meteorology, NBS Monograph 92, 1966: Table 6.5 and § 4.4.
6. UIT-R P.452-16, Procedimientos de predicción para la evaluación de interferencia entre estaciones en la superficie de la Tierra en frecuencias superiores a 0.1 GHz: § 4.2 (diffraction), eq. 45 (troposcatter), § 4.5 y eq. 49 (conductado).
7. UIT-R P.834, Efectos de la refracción troposférica en la propagación de ondas radiales.
8. UIT-R P.525, Cálculo de atenuación del espacio libre.
9. ITU-R P.526, Propagation by diffraction: eq. 31 (cutcheon) and § 3 (tierra esférica).
10. B. Indermuehle, H. Suzuki, Tropospheric Ducting Prediction based on GFS Model Data at Inyarrimanha Ilgari Bundara, the CSIRO Murchison Radio-astronomy Observatory, Publications of the Astronomical Society of Australia, 2026 (arXiv 2608.12861).
11. D. E. Kerr, Propagation of Short Radio Waves, MIT Radiation Laboratory Series vol. 13, 1951: eq. 17 e 18.
12. G. Hoch DL6WU, «Extremely Long Yagi Antennas», VHF Communications 3/1982, pp. 130 et seq., fig. 1 (medido a 432 y 1296 MHz según la longitud del boom).
13. UIT-R P.372, Radio ruido: ruido galáctico y artificial para el medio ambiente.
14. NOAA/NCEP, NOMADS, filtro GRIB del tiempo GFS 0.25° (nomads.ncep.noa.gov); formato WMO G RIB2, embalaje complejo con diferencias espaciales.
15. Open-Meteo, API de predicción histórica (archivo de pronóstico modelo desde cerca de 2021).
16. LukiBox, TropoLink, diseño de rutas troposcatter con P.617, NBS TN101 e ITM flanked (GitHub, Apache 2.0 licencia).
17. UIT-R P.617-5, Técnicas de predicción de Propagación y datos necesarios para el diseño de sistemas de radio-relé transhorizon, 2019.
18. RSGB, VHF/UHF Handbook; DL2FZN implementation (andi-f/troposcatter on GitHub, 2013).
19. W. F. Moler, D. B. Holden, «Transparición troposférica y circulaciones atmosféricas», Journal of Research of the NBS 64D, 1960, doi 10.6028/jres.064d.014: señal más allá del horizonte (1365 y 9380 MHz, en el mar) y movimiento vertical del aire.
20. ARRL Handbook 2022 (99a ed.), § 19.4.3–19.4.4: tiempo y propagación troposférica.
21. S. Franke, B. Somerville, J. Taylor, The FT4 and FT8 Communication Protocols, QEX July/August 2020.
22. W3SZ, umbrales de modos digitales lentos ( JT65, JT9, QRA64) y elección de modo según la propagación, 2019.
23. QST, "El Mundo Sobre 50 MHz", noviembre 2022: apertura del 14 al 15 de agosto de 2022 entre Manitoba y Kansas/Missouri.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Quick-Start Guide to Q65, 3 April 2021 (wsjt.sourceforge.io).
25. P. L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis, Transmission Loss Predictions for Tropospheric Communication Circuits, NBS Technical Note 101, magazine 1967: § 9 (troposcatter, function F(θd), H0), § 10 and Annex III (radio climates and variability over time).
26. Open-Meteo, Historical Weather API (ECMWF ERA5 revisión desde 1940; Copernicus Climate Change Service, CC BY 4.0 license).
