LIVETROPOVHF Documento técnico 1
# Funcionamiento del programa

> Traducción automática del italiano: en caso de duda vale la guía italiana.

Lo que hace el programa, desde donde toma los datos y cómo los convierte en lo que ves en el mapa y en la lista. Versión del 9 de noviembre de 2026, relacionada con el proyecto en C# (.NET 10 + Avalonia).

## Resumen

- Pronóstico de tropo (Hepburn [3], F5LEN [4], isobare GFS [18]) y observación real de bandas en la misma ventana, según la lista de control LA0BY [1].
- Recopilación de datos: dos grupos DX, Red Beacon Inversa [9], clúster dm4x y flujo PSKReporter [5] MQTT, decodificación UDP de WSJT-X, JTDX y MSHV [8].
- Spot: segmentos de banda, filtro negativo, airscatter en amarillo desgarrado, posición por localizadores o CALL3.TXT [6] [7].
- Quién escucha: círculos rosa oscuros con líneas rosas siempre visibles hacia las estaciones sentidas [5].
- Tarjeta Spot: distancia, azimut, índice tropo en el camino y pérdida de troposcatter con el método G3YGF [2].
- Idiomas Hepburn y F5LEN: banco [3] de imagen y calibración [4].
- Reloj: Control NTP en 10 servidores cada 3 minutos [10] [11].
- Informes sospechosos: EME en Q65/JT65 reconoció con la posición de la Luna, localizadores truncados, esquiadores, cuarentena y oyentes, con reglas aprendidas por el sol [20].
- Criterios para el descarte y el significado de los colores resumidos en dos tablas (secciones 11 ter y 11 quater).
- Temas, 36 Idiomas [17], documentos de exportación a Word con fórmulas editables [16], programa portátil y multiplataforma [12] [13] [14] [15].

## 1. Propósito

El programa reúne en una sola ventana dos cosas que generalmente miran a diferentes sitios: predicción de la propagación de troposfer (índice de Tropo de Hepburn y F5LEN, isobare) y observación de lo que realmente sucede en bandas (spot de DX cluster, Red de Beacon Inversa y PSKReporter, además de la lista de estaciones que están escuchando). La banda de referencia es 144 MHz; los puntos recogen de 50 MHz arriba.

El ajuste sigue la lista de control propuesta por LA0BY para eventos de tropo extremos: en los días anteriores se examinan las previsiones Hepburn y F5LEN y los perfiles de temperatura a altitud, mientras que el evento está en el clúster DX y en los mapas de spot la evidencia de que la apertura está comenzando [1]. LA0BY también observa que los únicos mapas Hepburn/ F5LEN no bastan para predecir las conexiones más largas [1]

## 2. La ventana y las pestañas

La ventana es una y no se divide en ventanas móviles. De arriba a abajo:

- Ajustes generales fila: Call and Locator a la izquierda; a la derecha el reloj UTC (por ejemplo 08/11/2026 20:33:38 UTC, texto ámbar) seguido por NTP:OK o NTP:FAILED, el menú temático y el del idioma.
- Número de fichas en cinco grupos: Mapa y lugar; Pronósticos (Propria β, el pronóstico calculado por el programa y descrito en el Manual de Predicción, luego Hepburn y F5LEN); GRAVES; Datos y fuentes (grupo DX, PSKReporter, Inversa Beacon, UDP, Nombramientos e informes sospechosos); Guías (Operación de Programas, Edificio de Mapa, Cómo configurar el tropo, Previsión manual, Consejos al operador con un texto de búsqueda que cuenta una guía). Bajo la viga un bar muestra los sub-languagos del grupo; cada grupo recuerda el último utilizado.
- Contenido de la ficha y, en la parte inferior, la barra de estado con el estado de las fuentes, la base de datos de nombres y el tiempo de la última actualización.

Los ajustes de la línea superior se aplican a todas las pestañas: el tema elegido también coloran la colección de datos y el fondo de documentos; Las páginas de documentos siguen siendo tan blancas como una hoja, por lo que imprimen bien.

## 3. Llamada y propietario

El propietario de Maidenhead puede tener 4 o 6 caracteres (por ejemplo, JN70 o JN70CN). El programa lo convierte en las coordenadas del centro de la plaza: campo de 20° × 10°, cuadrado de 2° × 1o, subquadrado de 5′ × 2,5′. Si el texto no es un localizador válido el borde de la caja se vuelve rojo y la estación no aparece en el mapa. El localizador también sirve como centro de la proyección azimuth y como punto de partida para distancias y azimut en el tablero de anuncios.

La llamada es el nombre utilizado como un login de grupo. Al principio, hasta que haya una nominación válida, los racimos no se conectan: una ventana pide el nombre (y, opcionalmente, el propietario); confirmado, el nombre es copiado como un nombre de usuario en el campo Iniciar sesión de cada grupo que lo tiene vacío y los racimos salen. Con “Más tarde” permanece quieto, y el botón Connect de cada grupo requiere la nominación de nuevo. MQTT, UDP y NTP no usan login y salen de todos modos.

## 4. Recopilación de datos: fuentes

Cada fuente tiene su propia sección en la pestaña Data Collection, con Connected/Disconnected, las cajas reconocidas y conectadas al inicio, estado, metros y registro de las filas recibidas. Las líneas desechadas o desechadas del filtro no aparecen en el registro: sólo cuentan los metros de sección. Las secciones están lado a lado con dos a dos.

| Fuente | Conexión | Lo que trae |
| 1. Elección del grupo DX | on0dxk.dyndns.org:8000 comando set/filter dxbm/pass 6 | Lugar de radios amateurs, también SSB y CW. En ON0DXK el VHF se filtra y se habilita con el comando indicado (poi pass 4, 2...). |
| 2. Segunda fuente | dxmaps.com:7300 (DXMAPS, EA6VQ) y s50clx.si:41112 ( S50CLX, Eslovenia) | DXMAPS recoge puntos VHF y también da puntos de skimmer RBN (control de ajuste/desgaste, ya establecido); S50CLX es un clúster DX Spider. Inicie sesión con su nombre; direcciones editables. |
| 3. Red Inversa de Beacon | telnet.reversebeacon.net 7000 CW/RTTY · 7001 FT8 | Punto de esquí automático. Iniciar sesión con el nombre sólo [9]. |
| 4. Grupo con PSKReporter | dm4x.ddns.net:8500 | Digital Spots of PSKReporter in cluster format. |
| 5. PSKReporter MQTT | mqtt.pskreporter.info 1883 · 1884 con TLS | Cada informe de recepción en tiempo real: quién transmite, quién escucha y sus Localizadores [5]. Bandas 144, 222 y 432 MHz. |
| 6. Mapas meteorológicos y tropo | dxinfocentre.com/tropo_eur.html tropo.f5len.org/forecasts-for-European modelo GFS | Mapas de índice de tropo Hepburn [3] y F5LEN [4], presión de nivel del mar para isobare (GFS a través de Open-Meteo [18]). Hepburn y F5LEN deben permitirse a los autores. |

Cluster telnet (fuentes 1–4). Después de la conexión el programa espera la solicitud de nombre (texto que contiene login, llamada:, callsign o tu llamada); si no llega dentro de 4 segundos, por favor envía el login. Luego envía los comandos de la sección, separados de punto y coma, uno cada 0,6 segundos. Los caracteres de control de telnet son eliminados antes de leer las líneas. Si la conexión cae y Riconnetti está activa, prueba de nuevo después de 10 segundos (15 si el servidor no responde en absoluto) [6].

PSKReporter MQTT (fuente 5). El programa se inscribe en el tema pskr/filter/v2/{banda}/{modo}/{tx_call}/{rx_call}/{tx_grid}/{rx_grid}/{tx_dxcc}/{rx_dxcc} arreglar sólo la banda y dejar los otros campos libres con el personaje + [5]. El nombre exacto con el que el servidor indica las bandas (por ejemplo 2m, 1,25m, 70cm) debe ser revisado en la primera conexión.

## 5. Desde la línea de racimo hasta el punto

Una línea es un lugar cuando tiene el formulario DX de SPOTTER: frecuencia DX comentario hhmmZ. El programa deriva spotter, estación DX, frecuencia en kHz, comentario y ahora UTC; los nombres quitan los sufijos de skimmer (-#, -1...). A continuación, aplicar, en este orden, las siguientes reglas [6].

| Banda | Segmento aceptado |
| GRAVES (con 144) | 143.040 – 143.060 MHz |
| 144 MHz | 144 – 148 MHz |
| 222 MHz | 220 – 225 MHz (sólo Región 2 UIT, América) |
| 432 MHz | 420 – 450 MHz |
| 1296 MHz | 1240 – 1300 MHz |
| 2320 MHz | 2300 – 2450 MHz |
| 10 GHz | 10,5 GHz |

1. Banda. La frecuencia debe caer en uno de los segmentos gestionados; todo el resto, y en particular cada comercial bajo 49 990,0 kHz, no se muestra y cuenta como “fuera de banda”.
2. Bien. Una de las maneras más conocidas de comentar es FT8, FT4, Q65, ISCAT, CW, SSB, FM, RTTY, PSK31, FSK441, JT6M); USB y LSB se convierten en SSB. JT65 MSK144
3. Filtro negativo. El lugar es descartado si contiene los modos MSK144, FSK441, JT6M, JTMS, ISCAT o las etiquetas.AUE icono.ES títulos <MS títulos >FAI confidencial > F2 но, con mayúsculas no sensibles e incluso espacios dentro de los soportes. Estas son formas y propagaciones que no son tropo: Meteoro estafador, aurora y esporádico, F2.
4. Airscatter. Si el comentario contiene <AS campus o <ACS título (también mixto, por ejemplo <As confía) o declara el lugar como airscatter (airscatter, dispersión de aire, dispersión de aeronaves), el lugar permanece pero se dibuja en amarillo con línea punteada (la onda oscura para mantenerse legible en el mapa claro) y se destaca en la lista.
5. Ubicación de las estaciones. Si en el comentario hay dos localizadores (por ejemplo, JN54) el primero es de los que hacen el lugar y el segundo de la estación DX; con un localizador, que es de la estación DX. Lo que falta es buscado en CALL3. TXT, sólo con el nombre exacto (para portátiles ver abajo). Si una estación permanece sin una posición, el lugar aparece en la lista con la nota “Posición desconocida” pero no en el mapa. JN83
6. Dobles. Un comercial con el mismo DX, el mismo spotter, la misma banda y la misma fuente de uno ya presente lo reemplaza, en lugar de añadir.

CALL3.TXT es la base de datos de nombres → localizador utilizado por JTDX, en el formato CALL,LOCATORE,,. El archivo del proyecto contiene 1 048 847 nominaciones, todas con localizador válido; el encabezado del archivo todavía habla de 34 323 nombres porque se mantuvo el de la primera edición [7]. Se carga en el fondo al inicio, por lo que la ventana es inmediatamente utilizable. El archivo se registra junto al programa, en la carpeta Datos, en la carpeta desde la que se inició el programa y una o dos carpetas arriba; Si no hay, una ventana le dice cuándo se inicia y la barra de estado lo indica en rojo. Su nombre siempre utiliza su 6 caracteres. Sólo regla para los localizadores: si el recibido (spot, MQTT, decodificación, QSO desunti) tiene menos de 6 caracteres y CALL3. TXT tiene uno a 6, siempre vale la pena CALL3.TXT; para portátiles (F/ IC8TEM, IC8TEM/P) es sólo una voz exacta: sin, la estación permanece sin un propietario y no está diseñado en otro lugar. La regla también se aplica en el momento del dibujo, por lo que los datos llegados antes de la carga de la base de datos se ejecutarán.

Una base de datos: CALL3 TXT. Desde 1.3.4 todos los localizadores están en un archivo, CALL3.TXT, que permanece en el formato de JTDX y WSJT-X (CALL,LOCATORE,,,,,) y se puede utilizar como también en WSJT-X, JTDX y MSHV. No hay más archivos separados. En la parte superior del archivo, después del encabezado, hay dos bloques marcados por una línea de comentarios: // [LIVETROPOVHF-MANUALI] con las entradas insertadas a mano y // [LIVETROPOVHF-CLUSTER] con las correcciones del clúster; luego // [LIVETROPOVHF-FONTI] con todos los demás elementos en orden alfabético. Cada propuesta aparece una vez. En el sub-idioma Datos y Fuentes › Nombramientos (CALL3) usted busca un nombre, usted escribe el propietario y la prensa Añadir / cambio: la voz entra en el bloque de las voces de las manos y vale sobre todo, incluso por encima del localizador que la propia estación envía. ∫ Elimina la nominación de CALL3.TXT (la siguiente actualización lo reanuda de las fuentes, si lo tienen). Añada un archivo... lea una lista CALL, LOCATOR (también acepta el punto y coma, tabulación y espacio) y escríbelo entre voces a mano; el archivo elegido no es copiado. Reload reread CALL3.TXT del disco, después de editarlo con un editor. Orden completa de fuentes: propio localizador, voces de mano, correcciones de racimo, el resto de CALL3. TXT (fuentes, localizadores en vivo aprendidos y G7VRD), recibió localizador (que se aplica sobre la base de datos solamente si tiene 6 caracteres y no termina con LL o MM).

Actualización de CALL3.TXT. En el mismo sub-idioma el botón Actualizar descarga ahora las fuentes (CALL3.TXT de WSJT-X Mejorado, CALL3.TXT EME de W7GJ, grid_data.bin de JTDX), se une a ellos (el primero que tiene una victoria de nominación), comprobar que son al menos 50.000 nombres (otros nada cambia), salva el CALL3. TXT El anterior CALL3. Las entradas de TXT permanecen, y un localizador de 6 caracteres permanece donde una fuente tiene sólo 4 de la misma plaza. Las voces y correcciones de mano del clúster permanecen en la parte superior, sin cambios. Con el cuadro de actualización por sí solo todos los días, el cheque se realiza al principio. El programa reescribe CALL3. TXT inmediatamente después de cada voz a mano y, para los localizadores aprendidos, a la mayoría de cada 10 minutos y en el cierre; el CALL3. TXT del paquete es sólo lectura y el atributo se elimina. Los instaladores de Linux no sobrescriben un CALL3. TXT ya está presente; que para Windows lo instala sólo si está desaparecido. ALL3

Correcciones automáticas del grupo. En los puntos automáticos DM4X y RBN el comentario trae a dos localizadores, por ejemplo KN09tp贸cticamente KN09: el primero es lo que la estación que mancha se declara, a 6 caracteres; el segundo es el de la estación sentida, a menudo sólo a 4. Si el primero tiene 6 caracteres (y no termina con LL o MM) y es diferente al de la base de datos, el programa corrige CALL3. TXT sola: la corrección va en el bloque // [LIVETROPOVHF-CLUSTER], con la fecha de la última confirmación en el tercer campo, es válida por encima de las fuentes (bajo las voces de las manos) y aparece entre las señales sospechosas actualizadas como "CALL3 ". Así que sobrevive a CALL3. Actualizaciones TXT. Una corrección no confirmada durante dos años pierde precedencia y permanece como una voz normal.

Última fuente: G7VRD. Si un nominado no tiene un 6 caracteres en CALL3. TXT, el programa lo pide en el fondo a la API pública de G7VRD (api.g7vrd.co.uk, “locator by callsign”), que recoge los localizadores automáticamente del tráfico de red. Sólo acepta un 6 caracteres que no es el centro de la plaza (LL o MM); las solicitudes se agrupan, al máximo 40 nominativos por minuto, y cada nominativo pregunta una vez al día, porque es un servicio hobbístico sin garantías. El localizador encontrado entra en CALL3. TXT si el nombre no está allí o tiene sólo 4 caracteres de la misma plaza; la búsqueda se apaga con la caja en el nombre sublingual.

Nombres con la barra. En los temas del PSKReporter MQTT, la barra separa los niveles, entonces los nombres compuestos vienen con un punto en lugar de la barra ( IV3. HB9CAT). El programa devuelve la barra y, si el mensaje contiene los nombres completos, utilice los siguientes: IV3/HB9CAT permanece IV3/HB9CAT en el mapa, lista y base de datos.

Los locadores aprendieron en vivo. CALL3. TXT no contiene todo: por ejemplo SP9EMF y SP9NVY no están allí, y luego sigue siendo el localizador de 4 caracteres enviado por PSKReporter (la tarjeta lo dice). Para reducir estos casos el programa escribe en CALL3. TXT cada 6 caracteres que proviene de cualquier fuente (PSKReporter, como transmisor o receptor, cluster, UDP decodificación) para una nominación que CALL3. TXT no tiene ni tiene sólo 4 caracteres en la misma plaza; nunca cambia una voz a 6 caracteres. Hasta 1.3.3 estos localizadores estaban en Data/ LOC6.TXT.

Transición de 1.3.3. En el primer inicio de 1.3.4 los archivos aparte de las versiones anteriores (Dati/CALL3 -MANUALE.TXT, Data/CALL3 -AGGIUNTE, Data/CALL3 -CLUSTER. TXT, Data/ LOC6.TXT y Data/CALL3 - G7VRD.TXT) se fusionan en CALL3. TXT con las mismas entradas. Liberando la escritura, los archivos antiguos toman la extensión.fuso, ya no se lee y se puede eliminar.

Receptores automáticos. RBN, dm4x y PSKReporter a menudo reportan nombres con un sufijo indicando el receptor: -SDR, /SDR, -# (skimmer RBN) o -1, -2... (más receptores de la misma estación). Para el propietario se retira el sufijo: OK2OZL -SDR toma OK2OZL JN89UE de CALL3.TXT si la estación no envía ya un localizador de 6 caracteres. El nombre mostrado sigue siendo el del sufijo.

Los localizadores cortaron. No todas las fuentes curan la extensión de su propietario: muchos lo fijan a 4 caracteres, y algunos sistemas automáticos de carga tocan deliberadamente el localizador de 4 caracteres, incluso los que obtienen los datos de la escucha real de FT8, FT4, Q65 y JT65, o lo completan con el centro de la plaza (LL o MM, por ejemplo JN89MM). Por esta razón, un 4 caracteres, o 6 caracteres que termina con LL o MM, es reemplazado por el CALL3. TXT de la misma plaza. Si la ubicación correcta y el centro de la plaza no coinciden con unas docenas de km, el error sigue siendo pequeño en comparación con las distancias tropo.

## 6. Quien escucha

Cada mensaje MQTT de PSKReporter reporta el nombre y localización de los que reciben [5], por lo que no necesita buscarlo en CALL3.TXT. Para el grupo elegido en el selector el programa sostiene, para cada estación receptor: propietario, última recepción, formas descodificadas, número de estaciones sentidas y máxima distancia. Una estación deja la lista cuando ya no recibe nada en el rango elegido (3, 5, 10, 15, 30 o 60 minutos).

En el mapa cada receptor es un círculo vacío rosa oscuro, y desde cada principio líneas rosas completas hacia las estaciones que se sienten y envía de vuelta a la red. Las líneas siempre se ven, sin hacer clic. Las citas de las estaciones de escucha, las escuchas y las dos cabezas de cada lugar están siempre escritas en el mapa, sin tener que hacer clic (el mismo nombre en el mismo punto se escribe una vez solamente). Al hacer clic en un círculo sus líneas se vuelven aún más gruesas y abre la pestaña con detalles. Si el localizador de MQTT tiene menos de 6 caracteres, el programa utiliza el de CALL3.TXT (alrededor de un millón de nombres), siempre que sea consistente con la plaza recibida.

Un meteorito estafador. Al igual que con las manchas de racimo, las ratios MQTT en MSK144, FSK441, JT6M, JTMS o ISCAT se descartan y cuentan solamente; lo mismo ocurre con las decodificaciones UDP de estas maneras.

No oyentes auténticos. Algunas estaciones declaran recibir en 144 MHz pero en realidad el receptor está en otra banda (indicación de frecuencia incorrecta): sus informes diseñarían aberturas inexistentes de miles de kilómetros. El programa lo revisa por sí mismo. Cada estación que un oyente dice escuchar en VHF se sigue durante 30 minutos en todas las bandas, con una inscripción dedicada MQTT (pskr/filter/v2/+/{nominative}/#) [5]. servir como prueba. Si al menos dos de las estaciones escuchadas por un oyente son, en los mismos minutos (±10), activas en otras bandas, el oyente no se considera genuino y todos sus informes VHF son descartados; el registro MQTT reporta el nombre y las estaciones que lo traicionaron. En la lista de puntos, junto a los metros, el botón MQTT muestra el estado del flujo (la hoja verde si está conectada, roja si no) y cuántos oyentes fueron descartados en la banda y el rango elegido; haciendo clic en él abre la pestaña con la lista de descartes y, para cada, estaciones encontradas en otras bandas.

China de conexión larga. Un enlace de más de 1000 km (Clumbing Spot, MQTT Listening, QSO Desunti) no se muestra inmediatamente: permanece sostenido, y el contador “held > 1000 km” dice el número. Se revela cuando otra conexión, entre diferentes estaciones y por lo menos la mitad a lo largo, lo confirma con uno de estos criterios: misma estación en una cabeza (por ejemplo el mismo oyente) y la otra estación en la misma dirección dentro de 10°; corredor similar, con ambas cabezas a 200 km de las de la otra conexión; ruta paralela, con dirección a 10° y distancia lateral a 200 km, superada a lo largo de la ruta. Ambos aparecen y el registro MQTT muestra la razón. Sus decodificaciones UDP son siempre visibles y se aplican como confirmación para otros. Las conexiones retenidas permanecen en memoria durante 60 minutos.

Nada menos de 144 MHz. El programa funciona desde 144 MHz hasta: todo menos de 143 MHz es descartado. La excepción es GRAVES, el radar francés que transmite en CW el 143,050 MHz por Broye-lès-Pesmes ( JN27SI): un comercial entre 143,040 y 143,060 MHz, o con GRAVES en el nombre, aparece entre 144 MHz con radar como transmisor. El tablero de anuncios dice que si el receptor está dentro del haz (sectores D, C, B, A de 90° a 270° de azimut visto desde el radar, 6 pasos de 7.5° cada uno) o hacia fuera; la pestaña GRAVES, como en YA-MS, muestra el cielo visto desde el radar: sectores, paso iluminado en ese momento (sequenza PE1ITR 2021: 0,8 s por paso, ciclo 4,8 s Sentirlo en el trope es difícil, pero puede suceder.

Rehabilitación de grupos humanos. Una estación que también aparece en otras bandas no cuenta como sospechosa si, en los mismos minutos (±30), los racimos humanos DX ( ON0DXK, GB7RAU; no dm4x, RBN u otras fuentes automáticas) el daño activo en la banda en cuestión, en puntos hechos por otros nombres: es el caso de estaciones que realmente trabajan en múltiples bandas juntas. Así un oyente descartado vuelve visible (el registro MQTT escribe “rehabilitado”) y un enlace de larga data para bandas bajas puede ser revelado.

Estaciones activas inferiores a 144 MHz. En la cuarentena de largas conexiones, una estación que en los mismos minutos se escucha en bandas menores de 144 MHz (de los informes de MQTT de las otras bandas) no confirma otras conexiones y no se revela, aunque una ruta similar o paralela lo confirmaría.

## 7. Lista de anuncios

Ocupa el 35% a la derecha de la pestaña Mapa y punto. Top: Título, selector de bandas (50, 70, 144, 222, 432, 1296, 2320 MHz, 10 GHz), selector de rango (último 3, 5, 10, 15, 30, 60 minutos) y llave de actualización. A continuación, cuatro metros: puntos visibles, AS/ACS, descartados del filtro, estaciones de escucha.

Cada línea muestra el tipo de línea, el tiempo UTC, DX → DE, el camino, la distancia en km y, en la segunda línea, la fuente, frecuencia y comentario. El más reciente está arriba. Un clic en la línea selecciona el punto: en el mapa la línea crece y aparecen los dos nombres.

## 8. Tarjeta Spot y pérdida de troposcatter

Para el lugar seleccionado la pestaña muestra localizadores, distancia, azimut, frecuencia, modo, fuente, ahora, comentario, el índice promedio de tropo a lo largo del camino y una estimación de la pérdida de ruta para tropo dispersado.

La distancia y la azimut se calculan en el círculo máximo con radio terrestre de 6371 km (formula del emisenoverso para la distancia, azimut inicial de la estación DX al spotter).

Perder troposcatter. La pérdida básica, para dos estaciones sobre tierra lisa, es la suma de pérdida y pérdida de espacio libre: G3YGF [2]

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

Las pérdidas están en dB, D es la distancia en km, f la frecuencia en MHz, θs el ángulo de difusión en grados.

8497 km es el radio equivalente de la Tierra (4/3 del físico). θA y θB dependen del horizonte de cada estación:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

con Altura Δh del obstáculo que hace desde el horizonte con respecto a la estación y su distancia. Cada nivel de horizonte cuesta aproximadamente 10 dB [2]. El programa no conoce el horizonte de las estaciones, por lo que muestra la pérdida básica con θA = θB = 0 y lo declara ("smooth ground"). G3YGF añade un término climático, donde N es la reactividad al suelo [2]; se utilizará cuando N esté disponible del modelo meteorológico:

```math
L_{N} = 0,2 \cdot (N - 310)
```

Verificación: para 110 km hasta 144 MHz la fórmula da 166,5 dB, contra aproximadamente 168 camas dB en la gráfica del artículo. Por 500 km a 144 MHz da alrededor de 206 dB. La relación señal/noise esperada es la capacidad del sistema (eirp + sensibilidad efectiva del receptor) menos la pérdida: con el sistema modelo G3YGF 144 MHz (198 dB) un camino de 500 km daría alrededor de −8 dB en SSB [2], es decir, la conexión es sólo con una mejor propagación de la dispersión pura.

## 9. Mis decodificaciones: UDP de WSJT-X, JTDX y MSHV

WSJT-X, JTDX y MSHV pueden enviar UDP cada línea que decodifican. El programa escucha en 127.0.0.1:2237 y repite el mismo paquete, byte per byte, en 127.0.0.1:2238, así que los otros programas que utilizaron ese puerto (GridTracker, JTAlert, un logger) continúan trabajando si los mueve en 2238. Las direcciones y puertas cambian en el sublingual UDP de la colección de datos; como dirección de escucha también puede utilizar un grupo multicast (por ejemplo, 224.0.0.1), si el programa de decodificación se establece así.

Los paquetes siguen el protocolo de red WSJT-X: un número de magia inicial 0xADBCCBDA, número de esquema y tipo de mensaje. El programa utiliza principalmente tres tipos de [8]:

- Heartbeat: dice qué programa está transmitiendo los datos y con qué versión.
- Estado: frecuencia de esfera y modo; de la frecuencia que recibe la banda (con la misma tabla de los puntos).
- Decodificación: Ahora, SNR, tiempo y desperdicio de frecuencia, manera y texto del mensaje decodificado.

Desde el texto se obtiene la estación que escuchó: en CQ 9 A2PQR JN83 es la estación después de CQ (salvar DX, UE, TEST...), en IK0JKL IT9MNO JM68 es el segundo, es decir, quien transmite. El localizador es el del mensaje, si hay (4 caracteres, diferentes de RR73); de lo contrario usted busca en CALL3. TXT. Son líneas de escucha: estaciones que su receptor escucha ahora. En el mapa se convierten en líneas rojas de su QTH a la estación, con el nombre; la capa se ilumina con la caja decodificada de mí y requiere un localizador válido. En la lista de la derecha un contador cuenta cuántas estaciones estás decodificando en la banda elegida.

QSO desunti dal vivo. Cuando una decodificación es un mensaje de cierre en el formulario C ALL1 C ALL2 RR73 (o 73 o RRR), por ejemplo IC8TEM IK3FHP RR73 o IK3FKP SV8PEX 73, las dos estaciones han completado un enlace. El programa lo registra como QSO, con el localizador de cada uno tomado del último mensaje en el que lo declaró o, si no, de CALL3.TXT [7], y lo dibuja en el mapa como una línea verde entre las dos estaciones (strate “QSO desunti dal live”). Un par de estaciones cuentan una vez por banda; la banda y el rango elegido en la lista son válidos.

En el mismo sub-idioma, el panel Debug muestra el texto de cada paquete recibido (tipo, tiempo, valores y mensaje), a petición también bytes hexadecimal, y una línea para el reenvío. Es para comprobar que el programa de decodificación está enviando los datos.

## 9 bis. Idiomas Hepburn y F5LEN

Cada uno muestra lo que el programa toma de la fuente. El botón “Leer la página y descargar todas las imágenes” abre la página indicada (base dxinfocentre.com/tropo_eur.html for Hepburn [3] and tropo.f5len.org/forecasts-for-europe for F5LEN [4]) y descarga todas las imágenes en el caché junto al ejecutable; también puede pegar la dirección de una imagen. Para cada imagen hay una caja para excluirla y un campo con el tiempo de pronóstico. Las 31 imágenes europeas de F5LEN son nombradas por el momento de la carrera modelo (03.png... 48.png cada 3 horas, 54... 96 cada 6 horas, 108... 192 cada 12 horas) y el programa toma el tiempo desde el nombre del archivo [4]; para Hepburn vale la pena el orden de la página. En la pestaña F5LEN, un menú elige la región: Europa (tropo.f5len.org/forecasts-for-europe), Australia (tropo.f5len.org/Australia, vk03.png... vk192.png) o Atlántico entre Cabo Verde e Irlanda (tropo.f5len.org/atlantic-ocean/atlantic-d4-ei, atl0319p2. Todos tienen los mismos pasos.

A continuación está el contador de calibración. Un clic en la imagen añade un punto de control (locator o lat,lon: al menos 6) o un color de la leyenda con su valor. El programa calcula la combinación de pixel → coordenadas con una interpolación polinomio de segundo grado en los cuadrados [19] más pequeños, lee los colores con la tolerancia elegida y, para F5LEN, convierte las unidades N en la escala 0-10 con un · N + b. “Guarda la calibración y prueba la lectura” escribe el archivo en la carpeta Tarature y comprueba en qué píxel cae tu localizador. Antes de establecer una calibración, debe solicitarse permiso al autor de los mapas.

Regiones. Cada fuente tiene más regiones, cada una con sus imágenes de nombre fijo: Lo siento, lo siento. La ficha de cada fuente muestra en primer lugar la vista previa de la fusión: todas las imágenes descargadas se juntan como en el mapa principal, con el panel punteado y el nombre de cada región, un velo gris donde ninguna imagen cubre el área y un slider para desplazar las horas; a la izquierda la lista de regiones, cada una con la caja para utilizarla, el número de mapas leídos y desde donde vienen los extremos (OCR, OCR texto o fábrica). El banco de calibración manual permanece detrás del botón homónimo. El menú Región de Bench elige qué aspecto, el cuadro “Utilice esta región en el mapa” lo incluye en el índice. El marco se encuentra solo (dos líneas horizontales y dos líneas verticales del mismo color); colocar la imagen servir a los extremos lat/lon del marco: para Hepburn Europe (10° W–40° E, 27°–56° N) y F5LEN Europe (15° W–40° E, 30°–65° N) ya están en el programa, para las otras regiones que escriben en los cuatro campos O E S N que leen las etiquetas del eje. El multiplicador de escala hace que las dos fuentes sean comparables: valor final = multiplicador × valor leyenda. [3] F5LEN [4] F5LEN

## 9 ter. Exportar a Word

Los dos documentos tienen en la parte superior del botón “Exportar a Word”. El archivo está escrito en el formato Office Open XML; las fórmulas se convierten en ecuaciones de Word (OMML), por lo que siguen editables con el editor de ecuaciones [16]. Si la ventana guarda no está disponible el archivo termina en la carpeta Documentos junto al ejecutable.

## 9 quater. Que me llama

Si un mensaje es enviado a usted en el UDP local (el nombre de pila es suyo, por ejemplo IC8FAX IK3FHP JN55 o IC8FAX IK3FHP -12), el programa rastrea desde su QTH a la estación que le llama una línea negra punteada y flash, con el nombre y el localizador junto a usted. El localizador viene de CALL3.TXT (incluyendo las voces de las manos) o mensaje. La línea permanece hasta que el UDP pasa el cierre de la QSO con esa estación ( RR73, RRR o 73, en una de las dos direcciones), a más de 3 minutos de la última llamada; usted se apodera de todo y no sigue criterios de exclusión, por lo que una llamada directa nunca escapa.

PSKReporter, a través de MQTT, no transmite el texto de los mensajes: dice sólo quién recibió quién, con frecuencia, camino y SNR. Es por ello que una llamada directa sólo es reconocida por sus propias decodificaciones UDP.

## 10. Control de tiempo (NTP)

Los modos digitales y el tiempo del lugar requieren un reloj preciso. Cada 3 minutos el programa interroga 10 servidores NTP (SNTP en UDP 123): 0.pool.ntp.org, 1.pool.ntp.org, 2.pool.ntp.org, 3.pool.ntp.org, it.pool.ntp.org, ntp1.inrim.it, ntp2.inrim.it, ptbtime1.ptb.de, time.google.com, fla. [11]

Para cada respuesta calcula el descarte y el retraso del retorno, descarta los valores anormales y toma la mediana, con el cálculo del descarte y el retraso proporcionado por SNTP [10]. Con al menos 3 respuestas coinciden en que el reloj UTC del programa se alinea y siguiente aparece NTP:OK en verde; si las respuestas no son suficientes aparecen NTP:FAILED en rojo y el reloj sigue siendo el de la computadora. El reloj del sistema no se toca, porque los permisos del administrador serían necesarios; Todos los horarios del programa interno (spots, ventanas de 5 a 60 minutos, decodificaciones) utilizan el tiempo alineado. El control siempre gira en el fondo: en la interfaz sólo ves el resultado junto al reloj; servidores, intervalos y respuestas mínimas cambian en LIVETROPOVHF.ini (ntp_servers, ntp_minutos, ntp_min).

## 10 bis. Criterios generales de exclusión

Dos cajas en la barra de capa aplican a todo lo que dibuja (spot, PSKReporter, decodificación, QSO desunti):

- Excluya menos de 100 km. El tráfico entre estaciones inferiores a 100 km no puede ser significativo para el tropo (onda de tierra, rango óptico): con la bandeja de entrada no dibuja y no aparece en la lista de listados. El comienzo está apagado.
- Más de 1000 km: sólo cuadrados. Alrededor de su QTH (el localizador establecido para comenzar) hay una corona de 1000 km en todas las direcciones. Si sólo una de las dos estaciones de un enlace está fuera de esta corona, la conexión no se dibuja como una línea: en su lugar se coloran en lila, dotada a 45°, las plazas de localización (4 caracteres) de las estaciones implicadas. Lo mismo se aplica a las estaciones PSKReporter que escuchan más de 1000 km, que aparecen como plaza en lugar de como círculo. Hasta ahora el tráfico que ves sin llenar el mapa de líneas. Sin un localizador válido cuenta la longitud de la conexión. El cuadrado de su QTH nunca colores en lilac, incluso cuando escucha una estación más allá de la corona: es el localizador especificado, cualquier nombre utiliza que opera desde la estación. El radio corona es elegido junto a la caja: 1000, 700 o 500 km. La caja Corone 1000/700/500 km dibuja las tres coronas alrededor de la QTH, dibujadas en lila, con la opción más marcada y la distancia escrita norte. En la lista de puntos la distancia es seguida por.. El comienzo está encendido. El criterio se aplica después de la cuarentena: las conexiones largas aún retenidas no aparecen como cuadrados.

¿Cuáles son las plazas de lila cubiertas a 45°? El tráfico lejos de su QTH le interesa entender dónde está abierta la propagación, pero sus líneas, cientos de kilómetros de largo y a menudo decenas, cubren el mapa y esconden lo que cuenta para usted: las aberturas que puede explotar de su estación. Por esta razón, más allá de la corona elegida, el programa no dibuja las líneas sino color los cuadrados localizadores (2° × 1°) de las estaciones involucradas. El resultado es un mapa de «zonas activas»: donde aparecen muchas plazas cercanas de lila, hay un conducto o una abertura, incluso si no le concierne directamente; Si el área se ensancha hacia usted, es la señal para señalar la antena en esa dirección. El panel de 45° le permite ver bajo el índice tropo y la cuadrícula, así que compare a un vistazo el pronóstico (color inferior) y el tráfico real (illus). Su cuadrado QTH nunca colores, porque es el punto de partida y no un área distante. Sólo los cuadrados que están más allá de la corona están coloreados: un cuadrado cruzado por la corona, o dentro, permanece libre incluso si hay una de las dos estaciones. Para ver las líneas, apague la caja o elija una corona más grande.

## 11. Temas e idiomas

Temas: los 18 programas comunes a IC8TEM, los mismos que YA-NEC y E-LAYER ANALYZER: Clear, Night, Black, ocho pasteles (azul, naranja, aquamarine, verde, rosa, lila, índigo, marrón), Scia meteorica (paleta YA-MS), los tres de IP-TRX-CTRL (Chiaro, Notte, Nero) y Azul, Púrpura, Verde. Desde 1.3.4 el Chiaro se parece a YA-NEC: fondo gris claro, paneles blancos bordeados, pestañas de noche azul y acento azul. En temas oscuros también el mapa base se vuelve oscuro. La escala de color del pronóstico sigue siendo la misma en todos los temas. En pantallas estrechas (bajo 1500 píxeles, por ejemplo 1366×768) el reloj en la parte superior muestra sólo la hora, por lo que el tema, cursor de textos y lenguaje siguen siendo visibles. Idiomas: 28 wiki de YA-MS [17] (English, Travel, Deutsch, Nederlands, Français, Español, Port日本ês, Dansk, Norsk, Svenska, Íslenska, Eesti, Lietuvi, Latviešu, Руский,,кра,нська, Türkaلרبيا With Arabic, hebreo and Urdu whole map. Las traducciones se refieren a la interfaz; los dos documentos técnicos y mensajes de la barra de estado permanecen en italiano. Los nombres de los días provienen de la configuración internacional de. NET para el idioma elegido. [12]

El tema y el lenguaje cambian de los dos menús en la parte superior derecha, junto al reloj, y se aplican inmediatamente, sin reiniciar el programa y sin interrumpir la recopilación de datos. La elección se guarda en LIVETROPOVHF.ini.

## 11 bis. Informes sospechosos y de autoaprendizaje

La pestaña Informes Sospechosos recoge todo lo que los controles del programa encuentran extraño, agrupado por tipo y nombre, con las últimas 24 horas enumeradas. El programa aprende por sí mismo: para cada pareja (tipo, nombre) cuenta los resultados positivos (+) y contrario (−), y después de 3 resultados acordados crea una regla automática; un resultado opuesto lo pone en cuestión. Las decisiones de usuario (bien, pobres) siempre se aplican; el botón pulsante devuelve la decisión al programa. Las reglas están en Data/APPRENDIMENTO. TXT, junto al programa; los automáticos no reforzados por un año se olvidan.

| Tipo | Cuando | Exit + / − | Efecto de la regla |
| EME ( Q65/JT65) | Q65 o JT65 sobre 1000 km con la Luna sobre el horizonte a ambas cabezas, o EME en comentario | Cada vez que la Luna es común | Discard: estación EME, sus largos Q65/JT65 s también están excluidos sin luna común. Bien: tratado como tropo |
| Conexión larga | Más de 1000 km, en cuarentena | + revelado, - expirado sin confirmación | Bien: las longitudes de la estación muestran inmediatamente. Discard: nunca |
| Oyente | PSKReporter receptor que escucha estaciones activas en otras bandas | − descartado, + rehabilitado por grupos humanos | Bien: nunca descartado. Discard: siempre |
| Diferente propietario | Plaza recibida diferente de la de CALL3. TXT | + cada vez que lo repites | Bien: la estación se ha movido, vale la pena la plaza recibida. Discard: vale CALL3.TXT |
| Square Center | 6 caracteres con LL o MM |  —  | Información únicamente |
| Receptor automático | Nombramiento con -SDR, /SDR, -# o -n |  —  | Información únicamente |

EME. Q65 y JT65 son las formas de comunicación a través de Luna: En teoría caen en la cláusula de exclusión como el meteoro de dispersión, pero también se utilizan en tropo. El programa calcula la posición de la Luna con las fórmulas de baja precisión del Almanac Astronomical (aproximadamente 0.3°) [20]: si una conexión en Q65 o JT65 supera los 1000 km y la Luna está por encima del horizonte para ambas cabezas, es casi sin duda EME y está excluida (conta entre los excluidos). Si la Luna no es común, la conexión sigue siendo posible en el trope y pasa por la cuarentena de conexiones largas, pero la señal sigue siendo visible. It applies to clusters, MQTT and its UDP decodings.

## 11 ter. Criterios de descarte de contacto, uno por uno

El programa sólo quiere mostrar la propagación tropoférica en 144 MHz y más allá. Cada información entrante (Cluster Row, PSKReporter Report via MQTT, UDP decoding) pasa a través de una serie de filtros, siempre en el mismo orden. Un dato descartado no desaparece en nada: se cuenta (contacto de los excluidos en la barra superior) y, cuando la razón es dudosa, aparece en la pestaña Informes Suspendidos.

|  N.  | Criterios | ¿Dónde está? | Lo que se discute | ¿Puedes cambiar? |
|  1  | Banda | cluster, MQTT, UDP | Todos menos de 143 MHz (también 50 y 70 MHz), excepto GRAVES 143,050 MHz. Las bandas son 144, 222, 432, 1296, 2320 MHz y 10 GHz | No. |
|  2  | satélite QO-100 | cluster, MQTT, UDP | 2400,0–2400,5 MHz y 10489,5–10490 MHz: es tráfico geoestacionario por satélite, no tropo | No. |
|  3  | Meteor scatter | cluster, MQTT, UDP | JT6M y ISCAT (también ISCAT-A/B), comentarios con etiquetas de propagación " estrecho" y no troposoxi («AU título,,AUE título, &gt; &gt;) siempre. MSK144, FSK441 y JTMS (también con יMS") a más de 250 km: dentro de 250 km también aceptamos, porque en esa distancia se utilizan estos modos en esa distancia. F2 | No. |
|  4  | EME (via Luna) | cluster, MQTT, UDP | EME escrito en el comentario; Q65 o JT65 más de 1000 km cuando la Luna está por encima del horizonte para ambas cabezas; estaciones aprendidas como EME | Sí: Buena regla en señales sospechosas |
|  5  | Escuchador no genuino | MQ | PSKReporter receiver who say to hear on 144 stations active at that time on other bands (al menos dos): Sus relaciones son descartadas. Volver bien si los racimos humanos DX confirman las mismas estaciones | Sí: bueno / pobre |
|  6  | Cuarentena de largas conexiones | todas las líneas | Más de 1000 km un enlace permanece oculto hasta encontrar una confirmación: la misma estación en la misma dirección, otra ruta en un pasillo de 200 km, o ruta paralela dentro de 10° y 200 km a lo menos la mitad. Sin confirmación expira después de 60 minutos | Sí: Bien / Off a la estación |
|  7  | Menos de 100 km | líneas y lista | Con la caja de acceso, las conexiones más cortas de 100 km (onda baja, rango óptico) | Recuadro, al inicio |
|  8  | Más allá de la corona de la QTH | líneas | Si una estación es de más de 1000, 700 o 500 km (a su elección) de su QTH, ninguna línea: sólo los cuadrados de lilla cubiertos por las dos estaciones. Su QTH Square Nunca Colores | Caja y radio, a 1000 km |
|  9  | ventana del tiempo | todo | Puntos más antiguos del rango elegido (de 3 a 60 minutos) | Menú arriba |
|  10  | Doble | Grupo temático | El mismo lugar (same DX, mismo spotter, misma banda, misma fuente) repetido | No. |

Nota sobre los criterios 4 y 6. Una conexión en Q65 o JT65 más de 1000 km sin Luna Común es posible en tropo: no es descartada por el criterio 4, pero pasa por la cuarentena del criterio 6 como cualquier otro enlace largo. Las llamadas directas a usted (sección 9 quater) son la única excepción a los criterios 7 y 8: siempre dibujan.

Lo que no es un desperdicio. Las correcciones del localizador (CALL3. TXT con voces de mano, correcciones del clúster y localizadores aprendidos, sufijos -SDR, centro de la plaza LL/MM) cambian donde se dibuja una estación, no se descarta. Una estación sin propietario, por otro lado, no se puede dibujar: permanece en la lista con la distancia «—».

## 11 quater. Colores y símbolos

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

## 12. Ajustes y portabilidad

Ventana. Al principio, la ventana ocupa el 90% del espacio útil de la pantalla (cortar la barra de aplicación), centrado, por lo que en un cuaderno de 15-16 pulgadas con un 125% o 150% redimensionar la barra de título sigue siendo visible. Si el espacio útil es inferior a 1280 × 720 puntos, la parte de la ventana maximizó. Tamaño, posición y estado máximo se guardan en LIVETROPOVHF. ini en el cierre y final en el siguiente inicio, siempre que la barra de títulos todavía cae en una pantalla (por ejemplo después de desmontar un monitor externo); de lo contrario regresamos al 90%. El tamaño mínimo es 900 × 560.

El programa es portátil: nunca escribe en la carpeta AppData o en el registro. Todo está en la carpeta del ejecutable: LIVETROPOVHF.ini (sets, created at the first closing), CALL3.TXT (con voces manuscritas, correcciones y localizadores aprendidos), mapa de caché y datos meteorológicos, reglas aprendidas (Dati/APPRENDIMENTO.TXT) y archivo LIVETROPOVHF.db. También puede mantenerlo en un palo USB, siempre y cuando la carpeta sea escribible.

Archivo de anuncios (SQLite [14]). Cada lugar aceptado y cada informe de escucha de PSKReporter se guarda en LIVETROPOVHF.db, una base de datos SQLite en un archivo. En memoria sólo quedan los últimos 60 minutos para la lista y el mapa; el archivo conserva todo y sirve para el historiador y las estadísticas (también las decodificaciones UDP), por ejemplo las aberturas para banda y por día, las distancias máximas, las plazas alcanzadas, y la comparación trasera entre el índice esperado y los puntos reales. Las tiras desechadas del filtro se cuentan pero no archivan.

## 12 bis. Instalación y actualizaciones automáticas

Desde la versión 1.0, el programa ya descarga listo desde la página de lanzamiento de github.com/ic8tem/LIVETROPOVHF, en los mismos paquetes de YA-MS: instalador para Windows (64 bits, 32 bits y ARM, instalar en C:\LIVETROPOVHF sin derechos de administrador) y cremallera portátil; para Windows 7 XpQ y 8.1 paquetes aparte (-window Con paquetes Linux, el programa se copia al primer comienzo en ~/LIVETROPOVHF, por lo que los ajustes y datos permanecen al lado del programa; en los datos Mac nacen junto a LIVETROPOVHF.app. SP1

Cada 30 minutos (el primer control unos segundos después del inicio, inmediatamente si la última vez que el programa había cerrado por un error) el programa lee solo, sin pedir nada, el último lanzamiento. Si la versión es más reciente descargar el paquete portátil adecuado para el sistema y reemplaza los archivos del programa en el lugar: los que se utilizan se renombran *.old-update y se eliminan en el siguiente reinicio. Ajustes, archivo, carpeta de datos y CALL3. TXT no está tocado. La nueva versión está activa desde el siguiente inicio: en la instalación terminó una ventana propone reiniciar inmediatamente; para correcciones urgentes, marcadas [obligatorias] en Release, el reinicio no puede aplazarse. El estado de control y la versión en uso se escriben en la parte inferior de la barra de estado; para apagar actualizaciones=0 en LIVETROPOVHF.ini. En el Mac la aplicación está firmada y no se cambia: el programa sólo informa que hay una nueva versión.

La imagen LIVETROPOVHF con el número de versión aparece al principio; al final el programa pide confirmación y gracias por su uso.

## 12 ter. Registro de errores y barra de estado

Cualquier error imprevisto termina en Data/errori.log, incluso con el registro de cálculo: fecha y hora, versión, sistema, pestaña abierta, error completo y últimas líneas de lo que el programa estaba haciendo. Un error de interfaz ya no cierra el programa: en la barra de estado aparece "error anotado" (haga clic en: abrir el registro). En el siguiente inicio el programa pregunta si enviar el informe: abre el navegador en una señal GitHub ya compilada, con nominación, localización y nombre de usuario del PC eliminado, que usted comprueba y envía con una cuenta gratuita GitHub. El registro también termina con cada cheque de actualización.

En la barra de estado las bolas de origen (grupo DX, RBN, PSKReporter, MQTT, UDP) se hacen clic y llevan a la fuente; el cómic dice por qué una fuente está apagada: puerto rechazado, nodo de racimo estacionado en el lado servidor, no respuesta, o escucha UDP pero sin paquetes de WSJT-X, JTDX o MSHV. Un clic en el número de la versión revisa las actualizaciones inmediatamente.

## 13. Plataformas

Librerías externas: Avalonia para la interfaz [12], MQTTnet como cliente MQTT para el flujo en tiempo real de PSKReporter [13], Microsoft. Data.Sqlite para el archivo [14].

C# on.NET 10 (LTS) [15] con interfaz de Avalonia, publicado como ejecutable independiente para Windows 64 y 32 bits, Mac Intel y Apple Silicon, Linux Debian y Raspberry Pi (64 y 32 bits). Límites marcados: Windows 10/11; macOS 14 o posterior, con aplicaciones para firmar o abrir a la derecha → Abierto; Raspberry de Pi 2 / Zero 2 W up (Los modelos ARMv6 no son compatibles con.NET).

## Gracias.
Gracias a I5MZY Massimo Zannoni para probar en Windows 7 y Windows 8.

Airtights: UIT-R P.453, productos digitales del Grupo de Estudio 3 (© UIT), utilizados para fines personales y no comerciales.

## Fuentes principales

1. S. Heck LA0BY, “Extreme Tropo Propagation on 144 MHz and up”, presentación en la 41a Reunión Nordic VUSHF, Skjeberg 2019. Vías Troposféricas, Lista de Control de Eventos (pronóstico Hepburn/ F5LEN, perfiles de alta temperatura con Windy, grupo DX como prueba de apertura), observación que sólo mapas de pronóstico no son suficientes para rutas extremas.
2. J. N. Gannaway G3YGF, “Tropospheric Scatter Propagation”, QST Noviembre de 1983 (del eponímico artículo sobre Radio Comunicación, RSGB, agosto de 1981). Formules of free space loss and diffusion, equivalent radius 8497 km, site term of 10 dB per horizon, climate term 0,2 · (N - 310), capacidad de planta y relación de señal/noise, ejemplo a 144 MHz.
3. W. R. Hepburn, DX Info Centre, “Tropospheric Ducting Forecast” (dxinfocentre.com/tropo.html) y “The Hepburn Tropo Index (HTI)”. Escala de índice y significado de valor, predicción en 6 días.
4. F5LEN, pronóstico para Europa (tropo.f5len.org/forecasts-for-European: lista de imágenes en el script de página, leyenda de la clase... Duct in the image) and world predict (tropo.f5len.org/WW) and note “New version of the worldwide VHF tropospheric propagation predict tool”. Índice basado en la reactividad en la unidad N, 40 pasos de 3 horas, actualización 4 veces al día.
5. mqtt.pskreporter.info, distribución MQTT en tiempo real de los comerciales de pskreporter.info (curado por M0LTE, flujo de P. Gladstone utilizado con permiso). Doors 1883-1886, pskr/filter/v2 topic scheme.
6. Resumen del proyecto de 8/11/2026 y prueba Go (core.go, gui.go), proporcionado por el autor del programa. Reglas de Ad, filtro negativo, segmentos ancho de banda, gestión de clústeres de telnet, temas, idiomas y portabilidad.
7. CALL3.TXT, base de datos de nominación → JTDX propietario (I. UA3DJY, A. ES1JA script, JTAlert/hamspots.net data). Formato de archivo y conteo de los nombres verificados en el archivo del proyecto.
8. WSJT-X, código fuente, archivo NetworkMessage.hpp (descripción del protocolo UDP: Heartbeat, Status, Decode). Referencia general, no adjunta; JTDX y MSHV utilizan el mismo formato.
9. Red de Beacon Inversa (reversebeacon.net), páginas de servicio del nodo telnet. Puertos 7000 (CW, RTTY) y 7001 ( FT8), login con el nombre solamente.
10. D. Mills, RFC 4330 “Simple Network Time Protocol (SNTP) Versión 4”, IETF 2006. Formato de paquete, cálculo de residuos y retraso de retorno. Referencia general, no adjunta.
11. NTP Pool Project (ntppool.org); INRIM, NTP time service (ntp1/ntp2.inrim.it); PTB (ptbtime1.ptb.de). Servidores utilizados para el control de tiempo. Referencia general, no adjunta.
12. Avalonia UI, documentación oficial (docs.avaloniaui.net). Interfaz multiplataforma, temas y diseño de derecha a izquierda. Referencia general, no adjunta.
13. MQTTnet, biblioteca.NET para MQTT (github.com/dotnet/MQTTnet). Cliente utilizado para el flujo PSKReporter. Referencia general, no adjunta.
14. SQLite (sqlite.org) y Microsoft. Data.Sqlite (learn.microsoft.com). Archivo en un archivo. Referencia general, no adjunta.
15. Microsoft,.NET 10 – sistemas operativos compatibles y publicación autónoma en un archivo (learn.microsoft.com/dotnet). Plataformas y límites indicados en la sección 13. Referencia general, no adjunta.
16. ECMA-376 “Office Open XML File Formats”, parte 1, sección 22.1 (Office Math Markup Language). Estructura del archivo.docx y ecuaciones. Referencia general, no adjunta.
17. Lista de los 28 idiomas del wiki YA-MS, proporcionado por el autor del programa.
18. Open-Meteo, pronóstico del tiempo API (open-meteo.com), con datos del modelo NOAA GFS. Presión a nivel del mar para isobare (véase el documento 2).
19. J. P. Snyder, “Map Projections – A Working Manual”, USGS Professional Paper 1395, 1987, y los métodos de transformación polinomio estándar para georeferenciar imágenes. Referencia general, no adjunta.
20. El Almanac Astronómico, sección D, “Fórmulas de precisión para la Luna” (Posición de precisión de alrededor de 0.3°). Solía reconocer conexiones EME. Referencia general, no adjunta.
