# Sources et formules

> Traduction automatique par italien: en cas de doute vaut le guide italien.

Collecte de données sur les sources consultées pour la prévision du tropo LIVETROPOVHF (20-21 novembre 2026): Physique de base, données disponibles, indices publiés, formules, expériences radioamateurs, validation et décisions. Les formules sont numérotées comme dans LaTeX, (1), (2),..., et dans le texte elles se rappellent avec le numéro entre parenthèses. Le Manuel des prévisions décrit comment le programme les utilise.


## 1. Locaux de l'auteur

IC8TEM observations qui cadrent le travail, avec la rétroaction trouvée dans les sources.

- La géographie italienne appelle à la diffraction. Avec des crêtes, des promontoires et des îles à quelques dizaines de kilomètres des stations, les contributions de diffraction devraient être calculées avec le paramètre Fresnel sur le bord principal, non seulement avec la terre lisse. Rencontre: dans une comparaison de terrain irrégulier à 563 MHz (Rio de Janeiro, 27 points entre 5 et 30 km), les meilleures méthodes étaient P.526 avec bords de pales, P.1812 et le coin diffractif, avec une erreur moyenne entre -5 et +3 dB et une dispersion d'environ 9 dB; P.1546 était le plus pessimiste (de Costa, Rabanal Ramirez et A Pour les pentes raides près de la station PETOOL se rappeler qu'ils servent des propagateurs à grand angle et le calcul bidirectionnel (onde réfléchie par les reliefs).
- Pour entrer dans un conduit éloigné, vous avez besoin de Fresnel. Si le conduit commence au-delà de l'horizon, le signal vient à nous par diffraction sur les obstacles jusqu'à son début; à partir de 1.3.2 le programme le calcule (Manuel de prévision, § 10 ter). Réunion: Rohde & Schwarz (2023) note que les stations le long d'un canal élevé ne parviennent souvent pas à entrer, tandis que celles proches des extrémités oui.
- Les conditions changent rapidement. Les théories sont là, mais les conditions changent en minutes, et aucun système de prévision ne prend les données en temps réel: le GFS sort toutes les 6 heures, avec une durée et 0,25°, et une prévision peut perdre de la valeur en peu de temps. Réunion: Bray (QST, novembre 1961) mesure des fluctuations horaires médianes d'environ ±7 dB sur 100 milles; les conduits se déplacent avec le temps atmosphérique (Rohde & Schwarz); notre registre QSO du 29/12/2021–2/1/2022 montre des ouvertures de croissance et de chute en quelques heures.
- La meilleure diffusion actuelle reste le réseau DX-PSKReporter. Pour savoir ce qui se passe maintenant, les taches de cluster, PSKReporter (et WSPR) battent n'importe quel motif: ce sont des mesures. La prévision indique où regarder et combien de temps elle peut durer; les publicités disent s'il y en a. Le programme pour cela met spot et prévisions sur la même carte.

## 2. Objet

L'idée est un troisième indice, calculé par LIVETROPOVHF lui-même à partir des profils verticaux d'un modèle météorologique, à ajouter à Hepburn et F5LEN au lieu de les remplacer. Aujourd'hui, le programme lit leurs cartes déjà prêtes: un index dirait aussi *pourquoi* il y a ouverture (quota et épaisseur du conduit, bande minimale piégée) et cela pourrait se produire avec les points que le programme recueille déjà.

- Que pouvez-vous donner: probabilité de conduit par point et par heure, part de la base et du haut, épaisseur, fréquence minimale piégée, et le long d'un chemin la fraction de la route à l'intérieur d'un conduit.
- Qu'est-ce qui ne peut pas donner: les conduits minces (moins de quelques dix mètres) que les modèles globaux à 0,25° ne résolvent pas, et les conduits d'évaporation sur la mer, qui servent avant tout à partir des GHz.
- Premier objectif pratique: 144 et 432 MHz en Europe, avec vérification sur les points de regroupement DX, PSKReporter et décodages locaux.

## 3. Physique de base

Tout commence par la réfraction N calculé à chaque niveau de profil: le conduit est l'endroit où la réactivité modifiée M diminue l'escalade, c'est-à-dire où dN/dh tombe sous −157 N/km ([ITU-R P.453-13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)).

Réfractivité (P et en hPa, T en kelvin):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Pression de vapeur due à l'humidité relative H (%) et température t (°C), au-dessus de l'eau entre −40 et +50 °C:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

Réactivité modifiée (h en km ou 0,157 · z avec z en mètres) et critère du canal:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN/dh (N/km) | Règlement | Effet de 144 MHz |
| 0 | sous-fraction | débit réduit |
| de 0 à −79 | normale (standard environ −40) | horizon radio avec rayon équivalent 4/3 |
| de −79 à −157 | super-réfraction | débit allongé, "trop bon" |
| ci-dessous − 15 | conduit (piège) | signaux piégés, DX sur 1000 km |

Comment extraire N d'un modèle à des niveaux de pression: pour chaque niveau, vous prenez T, humidité relative et hauteur géopotentielle, vous calculez N et M, puis le gradient entre les niveaux voisins. Les niveaux inférieurs au sol doivent être jetés (à 1000 hPa sur les Alpes est souterrain).

Fréquence minimale piégée: le conduit ne guide que les ondes assez courtes par rapport à son épaisseur. Hepburn publie une épaisseur de table → fréquence utile minimale ([DX Info Centre](https://www.dxinfocentre.com/propagation/luf.htm)); c'est une limite souple, sous laquelle le signal tombe progressivement.

| Épaisseur conduite | Fréquence minimale à payer |
|  61 m  | 962 MHz |
|  91 m  | 464 MHz |
|  122 m  | 276 MHz |
|  152 m  | 185 MHz |
|  183 m  | 133 MHz |
|  213 m  | 101 MHz |
|  305 m  | 53 MHz |

Dans la gamme VHF (souvent de 120 à 300 m), les valeurs de la table suivent assez bien une loi sur la constante; en dessous de 100 m, la formule sous-estime la fréquence jusqu'à 30 à 40 %. L'adapter moi-même (pas une formule publiée par Hepburn), avec f en MHz et d en mètres:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

Dans la pratique: pour les 2 m, un conduit est souvent d'au moins 170–180 m, pour les 70 cm il est d'environ la moitié. La formule circulant sur le filet, λmax = 0,014 · d^1,5, provient de Kerr (1951, chapitre 1, eq. 18) et est valable avec λ en centimètres et debout, pour un conduit dans lequel M tombe linéairement: avec ces unités donne pour environ 2 m 180 m et pour environ 70 cm 89 m, c'est-à-dire les mêmes valeurs de la table Hepburn, qui en provient. Kerr avertit qu'il ne s'agit pas d'un filet coupé: même les vagues souffrent plusieurs fois plus du conduit, et une façon que « perte » un peu peut améliorer la réception même avec les stations hors du conduit. Une étude IEEE Access 2019 confirme que les méthodes classiques (Kerr, Kukushkin) ne décrivent pas la vraie coupe ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)): le seuil doit être utilisé comme indication, pas comme mur.

## 4. Données disponibles gratuitement

Le point faible est la résolution verticale: entre 1000 et 850 modèles hPa donnent un niveau tous les 25–50 hPa, soit tous les 200–450 m, donc ils voient bien les inversions à 500–2000 m mais pas les canaux minces près du sol. LIVETROPOVHF utilise déjà Open-Meteo pour isobare, température jusqu'à 850 hPa et inversion: l'index peut démarrer à partir des mêmes appels.

| Source | Ce qui donne | Résolution | Utilisation pour l'index |
| [Metéo ouvert, GFS](https://open-meteo.com/en/docs/gfs-api) | T, humidité relative, point de rosée, hauteur géopotentielle par niveau (1000, 975, 950, 925, 900, 850 hPa...); hauteur de la couche limite | 0,25° sur les niveaux de pression; 16 jours, heures jusqu'à 120 h, mis à jour toutes les 6 h | profil N et M, gradient, base et haut du conduit; déjà dans le programme |
| [Metéo ouvert, ECMWF](https://open-meteo.com/en/docs/ecmwf-api) | mêmes niveaux à partir de modèles de données ouvertes 0,25°; le sol donne un point de rosée |  0,25°  | deuxième avis, utile pour la concordance entre les modèles |
| [Metéo ouvert, DWD ICON](https://open-meteo.com/en/docs/dwd-api) | T et humidité relative sur les niveaux de pression; en ICON- D2 il y a 800, 900 et 925 hPa | ICON-UE à environ 7 km | troisième avis sur l'Europe |
| [Radiosunds, Univ. Wyoming](https://github.com/NCAR/RAOBget) | profils mesurés, deux lancements par jour (00 et 12 UTC), format texte par station | de nombreux niveaux significatifs, même tous les quelques dizaines de mètres | vérification de l'indice; voir les conduits minces que le modèle ne voit pas |
| [GREURS, NOAA NCEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive) | archives historiques de radiodiffusion, un fichier par station, des paramètres plus dérivés | comme ci-dessus | étalonnage sur les années de données et comparaison avec les spots passés |
| ERA5 (Copernicus) | Réanalyse orale depuis 1940, 37 niveaux de pression | environ 28 km | statistiques et calibrage, non prévus |

- Avec la hauteur géopotentielle Open-Meteo est au-dessus du niveau de la mer: les niveaux qui tombent sous le sol doivent être jetés ([Open-Meteo, haut air](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)).
- Au sol on ajoute T et l'humidité à 2 m: ils donnent le premier trait de gradient, celui où les conduits de surface de nuit naissent sur la plaine et sur la mer.
- Émissions italiennes sur les archives de l'Université du Wyoming (weather.uwyo.edu/upperair/sounding.html): stations et numéros de l'OMM dans le tableau ci-dessous.

| Station | W | Latitude | Longitude | Partager |
| Udine/Campoformido |  16044  |  46,03  |  13,18  |  94 m  |
| Milan/Linate |  16080  |  45,43  |  9,28  |  103 m  |
| Pratique de la mer |  16245  |  41,65  |  12,43  |  12 m  |
| Brindisi |  16320  |  40,65  |  17,95  |  10 m  |
| Trapani/Birgi |  16429  |  37,92  |  12,50  | environ 14 m |

Le format texte du Wyoming donne pour chaque niveau de pression, de hauteur, de température, de point de rosée et d'humidité: assez pour N et M. Wyoming n'a pas d'interface officielle, les scripts sont utilisés comme [RAOBget de NCAR](https://github.com/NCAR/RAOBget); le College of DuPage garde des copies texte par station ([par exemple, 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)).](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. Index et méthodes publiées

Hepburn et F5LEN commencent par les mêmes ingrédients physiques mais ne publient pas l'algorithme complet; les études scientifiques de 2008–2026 décrivent plutôt la méthode étape par étape, et c'est de là qu'il vaut la peine de copier la recette.

| Méthode | Données de démarrage | Ce qui est calculé | Notes utiles pour nous |
| [Indice du tropo de Hepburn](https://www.dxinfocentre.com/propagation/hti.htm) | modèles météorologiques bruts du Service météorologique du Canada (SMC), tels qu'écrits sous ses cartes, et non GFS/NAM, cartes conçues avec GrADS, algorithme propriétaire ([storia](https://www.dxinfocentre.com/propagation/history.htm)) | Échelle linéaire 0–10 de courbure du signal: 0 normale, 1–2 super-réfraction, 3–4 certains conduits, 5–6 canaux diffuses; valeurs négatives = sous-réfraction | l'échelle à imiter, donc le troisième indice se compare immédiatement aux deux premiers |
| [F5LEN, prévisions mondiales](https://tropo.f5len.org/WW/info.php) | Réactivité dans l'unité N; pour l'Europe le modèle ARPEGE de Météo-France, 7,5 km, plus de niveaux entre 2 m et 1500 m ([note de l'auteur](https://tropo.f5len.org/author/f5len/)) | Indice moyen du propriétaire, 40 étapes de 3 heures, 4 calculs par jour, archive de 28 jours ([nouvelle version](https://tropo.f5len.org/?p=504)] | confirmer qu'il sert un modèle avec de nombreux bas niveaux |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf) | 60 niveaux du modèle ECMWF, du sol à 3000 m | minimum de dN/dz par colonne: conduit sous −157 N/km, superfraction sous −78.7 N/km; base de la couche de piège | climatologie: Padana Plain la zone européenne avec plus de pipelines toute l'année; Méditerranée en été près de 100%, en hiver moins de 15%; Italie plus de 30% les nuits d'été |
| [Indermuehle et Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861) | GFS 0,25°, 21 niveaux de pression + champs à 2 m | M = N + 0,157 z; dM/dz minimum inférieur à 3 km, base et sommet du conduit, profondeur du déficit en M; moins de 40 m jetés | vérifié pendant 7 ans sur 25 canaux de 88 MHz à 2,68 GHz; bonne capacité jusqu'à environ 3 jours à l'avance (AUC environ 0,74), puis tombe; GFS meilleur que ERA5 |
| [Hämäläinen et al., FMI 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf) | Harmonie-AROME, 750 m ou 2,5 km, 65 ou 90 niveaux | dM/dh < 0 sur le profil du modèle, comparaison avec un faisceau de 4 à 59 m | compte plus de résolution verticale que horizontale; aucun modèle ne voit les conduits de moins de 15 m; l'humidité domine sur le gradient de température |

Troisième service radio amateur déjà actif: [TROPOCAST.EU](https://tropocast.eu/)d'Artur SQ3A, pour 2 m et 70 cm en Europe. À partir de la page de prévision ([tropocast.eu/forcast](https://tropocast.eu/forecast)):

- Données: DWD ICON-EU (passe 0,0625°, environ 7 km), source par défaut; alternativement le PIBS canadien (0,15°), moins détaillé.
- Méthode: N et M selon UIT-R P.453 du profil entre la couche près du sol et 850 hPa; la reconnaissance des couches, l'union des conduits au sol et haut et les seuils des couleurs sont un bon calibrage de l'auteur, non publié.
- Produits: vue "complète" 0–3000 m, vues plus séparées "au sol" et "haute" (pour l'instant seulement dans les cartes à incorporer); score 0–100 dans neuf classes (sous 38 arrière-plan, 38–52 marginal, 53–58 faible, 59–65 modéré, 66–72 bon, 73–80 très bon, 81–88 élevé, 89–95 fort, 96–100 très fort); prévisions jusqu'à +72 h en une heure, avec animation et archives.
- Domaines: Europe, Pologne, Royaume-Uni, Allemagne, France, Espagne, Italie, République tchèque, Croatie; interface également en italien.
- Réutiliser: les cartes intègrent avec une API sans clé iframe; il y a aussi une API documentée (openAPI et schéma JSON). La licence n'est pas indiquée: Avant de l'utiliser comme troisième source à côté de Hepburn et F5LEN, vous devriez demander à l'auteur.

Différences par rapport à notre projet: TROPOCAST s'arrête à 850 hPa (environ 1,5 km), tandis que les conduits surélevés arrivent à 2000 m et au-delà (cas LA0BY du 22/11/2018), et ne donne pas d'indice pour la bande ou la route. Les classes 0–100 peuvent être comparées à notre indice 0–10 les mêmes jours.

Structure de l'image TROPOCAST (à partir du lien fourni par l'auteur du programme): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, où n est l'étape de prévision. Le site interdit la cueillette automatique des robots, donc je ne pouvais pas lire l'image; aussi pour cela, avant de montrer ces cartes dans LIVETROPOVHF sert la permission de l'auteur.

Cahier: [DJ5AR, catégorie](https://dj5ar.de/?cat=174)(Magonza, 21 articles 2017-2023, 2 m-13 cm). Cas décrits: plusieurs semaines depuis l'Irlande (23 mai-2 juin 2023), FT8 jusqu'au Belarus (septembre 2020), l'Italie-Allemagne sur les Alpes et l'Adriatique (août 2017). Reconnaissez le cas du conduit "trop haut" (inverse à 1000 m) que vous ne pouvez pas utiliser du sol: une autre confirmation qu'il est nécessaire de comparer la part du conduit avec celle des stations. Il utilise les radiophares comme mesure d'ouverture (jusqu'à 1028 km) et les profils verticaux à Paris, Bordeaux et DWD.

En résumé, la recette commune: profil vertical → N et M par niveau → couche avec dM/dh négatif → base, haut, épaisseur et force du conduit → index. La partie "secret" de Hepburn est l'étalonnage empirique sur les événements réels, que nous pouvons refaire avec les taches.

## 6. Formules utiles pour le programme

En plus de N et M (section physique de base), ceux-ci servent à passer du numéro vertical au numéro de point, puis par chemin. Les formules de l'UIT ont été reconstruites par un PDF lu en partie mal; le 22 novembre ont été revues sur des textes officiels ([UIT-R P.452-18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453-14, P.526-16, P.372-18) et corrigées dans le code: constante du troposcatter 190 (et non 190,1) plus absorption de gaz avec ρ = 3 g/m3; α pas en dessous de -3,4 (eq. 55a); gaz des conduits avec ρ = 7,5 + 2,5·. Rumore P.372-18: tableau 1 et galactique correspondent; Les courbes sonores artificielles sont jusqu'à 250 MHz et le galactique jusqu'à environ 100 MHz, sur le programme étend les mêmes lois.

1. Gradient entre deux niveaux du modèle (h en km, M en unité M):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. Résistance et épaisseur des conduits: épaisseur d = base supérieure (pour un conduit de sol, base = sol); résistance en M déficit entre la base et la surface.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3. Facteur équivalent du rayon de la Terre (P.452-18, eq. 5 et 6a), avec ΔN = baisse moyenne de N au premier km, positive, en N/km:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

Avec ΔN = 40 vous avez environ 4/3 k (atmosphère standard); avec ΔN qui approche 157 k tend à l'infini, c'est-à-dire que la radio Terre devient plate: c'est le début du canal.

4. horizon radio d'une haute antenne h mètres, avec un rayon équivalent à\_e en km:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5. Probabilité du conduit pour la latitude (P.452-18, eq. 2), pourcentage de temps; λ = latitude du centre de la route, μ1 et μ4 tiennent compte de la distance qu'il fait par mer:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

À 41° N, tous par mer (μ1 = μ4 = 1), donne environ 11 % du temps: est la base climatologique, utile comme valeur de référence dont l'indice attendu diffère.

6. Perte pour le troposcatter (P.452-18, eq. 45; f en MHz, d en km, angle de la trajectoire en mrad, réactivité au sol N0 au centre de la route):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

Le terme -0,15 · N0 montre pourquoi l'air humide aide aussi sans conduit: 50 unités N supplémentaires sont 7,5 dB de moins que la perte. LIVETROPOVHF utilise déjà la version G3YGF de la perte de dispersion.

7. Perte dans le conduit (P.452-18, eq. 46–51): perte fixe de couplage plus atténuation pour le milliradiant d'angle, qui se développe avec la racine cubique de fréquence (f en GHz, a\_e en km):

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. De Bean et Dutton (1966) et Kerr (1951)

Les deux textes classiques donnent trois choses qui sont nécessaires immédiatement à l'indice: les seuils de gradient déjà tarent sur les mêmes niveaux de pression de GFS, ce qui vaut en dB un changement de réactivité, et quelles situations météorologiques créent ou détruisent les conduits. * Radio Météorologie*, NBS Monographie 92.

1. Classification des profils pour les couches de pression (Bean et Dutton, onglet 6.5, p. 248; −dN/dh en N/km). Conçu pour les longueurs d'onde radio, il s'applique comme il est au niveau du modèle:

| Route | Partager sur | Sous-fraction | Non stratifié | Super-réfraction |
| 1000–850 hPa | 0–1,46 km | −dN/dh < 0 | de 20 à 60 ans | plus de 100 |
| 850–700 hPa | 1,46–3,01 km | −dN/dh < 0 | de 20 à 50 ans | plus de 80 ans |
| 700–600 hPa | 3,01–4,20 km | −dN/dh < 0 | de 15 à 40 ans | plus de 70 ans |
| 600–500 hPa | 4,20–5,57 km | −dN/dh < 0 | de 10 à 30 ans | plus de 50 ans |

2. Combien vaut le dB (Bean et Dutton, chapitre 6, pp. 233 à 258), mesure de 70 à 180 MHz sur les routes au-delà de l'horizon:

- Toute couche (au sol ou à l'altitude) par rapport au profil "smooth" élève le signal de 10 à 25 dB à tous les pourcentages; le plus grand gain provient des couches inclinées élevées, probablement en raison de l'effet de focalisation (cours de 200 km).
- Réactivité au sol Les N et le déclin du premier km ΔN sont liés au signal médian mensuel avec un coefficient compris entre 0,4 et 0,95, la médiane 0,7 (20 routes de 130 à 446 km, environ 100 MHz).
- Cycle annuel: 0,18 dB par unité N; cycle quotidien de 0,2 à 1,1 dB par unité N, maximum sur les routes de 175 à 200 km et en hiver.
- Le paramètre composé de Misme estime la variation de champ à 0,5 dB/(N/km) · ( gradient équivalent − 40) plus un terme de stabilité thermique; le paramètre de turbulence D (zone entre le profil réel et le profil adiabatique, 0–3 km) ne fait que s'améliorer par rapport au simple ΔN. Conclusion des auteurs: ΔN reste le meilleur paramètre de simplicité.

3. Profil de référence et anomalie (Bean et Dutton, § 3.8 et 3.9). L'atmosphère exponentielle de CRPL de référence donne le profil "normal" à partir de Ns; l'unité A mesure combien le profil réel diffère à chaque part. C'est la façon naturelle de transformer un profil GFS en une anomalie à mettre sur la carte:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

Avec Ns = 313, vous avez ΔN environ −42 N/km, c'est-à-dire le rayon équivalent 4/3. Le critère de conduite −157 N/km est également dérivé de la loi Snell pour un rayon horizontal (eq 4.23), presque indépendant de la hauteur de l'antenne.

4. Où et quand naissent les conduits (Bean et Dutton § 4.4; Kerr cap. 3:

| Processus | Situation actuelle | Effet |
| Avertissement | l'air chaud et sec de la terre qui coule sur la mer froide (typique de la manche d'été après des jours de beau temps); compte principalement le déficit d'humidité | conduit intense sur la mer |
| irradiation de nuit | ciel serein, vent faible; nuages bas ou beaucoup d'humidité l'empêchent | conduit au sol qui est né au coucher du soleil, pousse le soir et disparaît rapidement après le lever du soleil, surtout à l'intérieur |
| Résidence | air qui descend de l'anticyclone, chauffé et sec, sur l'air frais et humide; fréquentes inondations dans les montagnes et les secteurs sud-est de l'anticyclone | couche super-réfractive ou gaine élevée; presque toujours gradient supérieur à la norme en inverse |
| Mer et terre | côte, circulation de quelques centaines de mètres | pipelines locaux le long de la côte |
| Contre le conduit | mélange: sol chauffé par le soleil entre le milieu matin et l'après-midi, vent au sol sur quelques m/s sur terrain accidenté; brouillard nocturne | probabilité de conduit presque rien |

5. Deux nouveaux ingrédients à essayer. Le mouvement vertical: des courants ascendants dans les zones de convergence enlèvent et effacent les couches, la subsidence renforce le signal (Moler et Holden: en moyenne environ 2,5 dB de plus qu'avec l'air en montée, sur la mer en bande L et X). Open-Meteo fournit déjà une vitesse verticale sur les niveaux de pression. Et la tendance de la pression: sur une trajectoire de 140 km à 186 MHz, le signal descendait lorsque la pression tombait vers l'émetteur et vers le centre de la trajectoire et retournait haut et sans évanescence quand il montait, avec seulement 2 (Bean et Dutton, fig. 6.9, p. 241).

6. De Kerr. En plus de la formule de la longueur d'onde maximale (section physique de base), Kerr donne une formule approximative pour M à partir de la seule température, vapeur et altitude (§ 3.9, avec les anciennes constantes 79 et 4800 puis remplacées par 77,6 et 4810) et observe qu'à l'inverse la coupe du vent peut générer des vagues qui oscillent à l'altitude le canal, cause possible de fuites profondes (§ 3.26). Source italienne rapportée par Bean et Dutton et à la recherche: L. Bonavoglia (décembre 1958), "Correlation entre phénomènes météorologiques et propagation..."

## 8. Présentations radioamateurs et rapports techniques (deuxième série)

Sept documents fournis par l'auteur du programme. Trois servent directement à l'index: W3SZ pour le troposcatter « sol », LA0BY pour les cas que Hepburn n'avait pas prédits, Hepburn (traduction LU7MC) pour la classification météorologique des conduits.

1. Troposcatter: le signal qui existe toujours. R. Rehr W3SZ, «Trapospheric Scatter (Troposcatter) Propagation for VHF, UHF, Microwave and Frequencies», Conférence sur les rats-paquets 2014; J. R. Duffey KK6MC, «The Troposcatter Propagation Mode and How to Predict Coverage on the VHF/UHF Bands», CSVHF 2017.

- Utile de 100 à 700 km, de 144 MHz à 10 GHz; la perte augmente de 30 logs f avec la fréquence.
- Avant tout, il compte l'angle de départ vers l'horizon: chaque degré supplémentaire coûte environ 9-12 dB ( M1CNK, méthode Collins).
- En été, le signal est en moyenne supérieur d'environ 10 dB à celui de l'hiver; au milieu de l'après-midi, il est inférieur d'environ 5 dB à celui du matin ou du soir. KK6MC indique une variabilité totale de 13 à 15 dB avec le temps, la saison et le temps.
- La méthode Yeh, la plus facile à programmer (f en MHz, d en miles, angle de diffusion en degrés, moyenne géométrique α des ouvertures des deux faisceaux):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Comparaison de Larsen (1968) sur 15 connexions réelles: erreur carrée moyenne 5,1 dB pour Collins, 7,4 pour Yeh, 8,1 pour NBS et 16,8 pour CCIR pour Rider. Toutes les méthodes s'améliorent si Ns reste stable (Yeh tombe à 5.4 dB): pour la dispersion la réfraction du jour compte peu, de sorte que l'indice de tropo doit regarder les couches, pas seulement Ns. Le tableau de 15 connexions, avec pertes mesurées (y compris Feldberg-Monte Baldo, 322 km à 957 MHz, 223,1 dB; Chypre-Israël, 319 km à 77,5 MHz, 163,3 dB), est un bon banc d'essai pour notre calcul.
- KK6MC donne la perte qu'une station typique peut dépasser sur 2 m et la distance correspondante, de 184 dB (FT-817 et 3 éléments en SSB, environ 130 km) à 215 dB (longs bungs en SSB, environ 430 km) et 236 dB (kW et yagi couplés, environ 640 km). C'est la façon de traduire l'indice dBs en miles gagnés.
- Code prêt: W3SZ Programme Yeh et Collins (nitehawk.com/w3sz/YehCollins.zip) et calculatrice Bob Atkins KA1GT (bobatkins.com/radio/troposcatter.html).

Une autre source sur la dispersion: I. Roberts ZS6BTE, "Propagation de la dispersion troposphérique", *Radio ZS*, décembre 1984, avec note de 2013 ([qsl.net/zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). Principalement Géométrie utile: angle de diffusion φ = φ0 − φ1 − φ2, avec φ0 = d/R, φ1 = (h1 − h\_ostacolo)/d1 + d1/2 R et la même pour l'autre station, R = 8446 km (4/3 du rayon de la Terre). C'est la pièce qui est utilisée pour calculer les angles à partir du profil du sol dans le troposcatter « sol ». Autres données: chaque angle en plus coûte environ 10 dB; le passage des Ns 280 à 300 réduit la perte d'environ 4 dB; le volume de dispersion est compris entre 300 et 2000 m pour 150 km, entre 600 et 3000 m pour 300 km; Le temps dans le volume de dispersion augmente le signal d'environ 20 dB. l'exemple numérique de l'article ne revient pas avec les formules données (la durée de l'espace libre à 900 MHz et 872 km est d'environ 150 dB, pas 125), de sorte que les formules doivent être vérifiées sur W3SZ avant de les utiliser.

2. Types de conduits (W. R. Hepburn, "Modes de propagation troposphérique", dxinfocentre.com/propagation/tr-modes.htm, traduction espagnole de LU7MC). Remplissez le tableau de Bean et Dutton avec des cas utiles en Italie:

| Type | Quand | Note pour index |
| Irradiation | nuits calmes et paisibles sur la terre | suivre le sol, disparaît peu après l'aube |
| Haute pression (subsidence) | anticyclone | peut durer toute la journée; la nuit le conduit d'irradiation ci-dessous peut bloquer les signaux éloignés, de sorte que la journée peut aller mieux |
| Avant | devant un front chaud, derrière un front froid, au nord d'un front stationnaire | courte; le mauvais temps le dérange |
| Descente en montagne | föhn, bora: air qui descend, qui chauffe et qui sèche | pour essayer de bora sur l'Adriatique et föhn au sud des Alpes |
| Avertissement | air chaud et sec sur sol froid et humide, par exemple après la pluie | conduit au sol |
| Vallée | L'air chaud au-dessus de l'air froid s'arrête dans la vallée | peut rester dans la journée |
| Maroc | air chaud et sec sur l'eau froide; arrive sur la côte avec la brise | pic en après-midi; printemps et début d'été les meilleures saisons |

Les conduits d'évaporation en mer (0 à 30 m) ne servent qu'environ 3 GHz et ne pénètrent pas dans l'arrière-pays: pour 144 et 432 MHz, vous pouvez ignorer. Les hautes montagnes sont une barrière; les déserts sont trop secs.

3. Les cas que Hepburn n'avait pas prédits (S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", Nordic VUSHF 2019, de JO59IX à 500 m d'altitude):

- 22 novembre 2018: 70 cm jusqu'à UA6 (environ 2600 km) pendant 6 à 8 heures. Hepburn a indiqué seulement les États baltes et la Pologne. Le conduit a été vu dans les cartes de température à 900 et 1500 m et s'est terminé juste au-dessus de 2000 m; il était plus haut du côté norvégien, et seulement LA0BY sur la colline est entré. Au sol, le temps ne semblait pas être un trope: froid et brouillard au sommet.
- 11 février 2015: 2 m et 70 cm vers 9A et S5 (environ 1500 km) à travers les Alpes, également ce non prévu par Hepburn.
- 15 octobre 2011: 2 m avec EA1 (2135 km) dans un conduit haut, sur trois tronçons de mer et trois terrains.
- Autres événements: du 6 au 11 novembre 2003 à l'est (2050 km), du 10 décembre 2004 à la France.
- Adresses de terrain: brouillard en plaine, pics libres, peu de vent, sol humide. gradient de température normale − 6,5 °C/km; dans un cas réel ( VK3KAQ) +7 °C en 20 m.

Conclusion de l'auteur: Hepburn et F5LEN ne suffisent pas, ils servent les profils à l'altitude le long de la route. Pour nous, ces dates sont des cas de vérification à recalculer avec les données historiques des modèles, et l'index sur le chemin doit comparer le quota du canal avec le quota des stations.

4. Expérience italienne (G. IV3GBO, "Types de propagation VHF SSB/CW", 2017): conduite entre 450 et 3000 km, avec des couches entre 450 et 3000 m; brouillard étendu comme indice; ouvertures par mer sur l'Adriatique vers les Pouilles, l'Albanie et la Grèce. Éparpillement de 100 à 500 km.

5. Introductions (P. Wilton M1CNK, « Introduction à la propagation VHF/UHF », 2013): 70 cm sont souvent mieux que 2 m; avec un pôle de 10 m à 144 MHz, l'angle de départ est d'environ 3°, avec 20 m environ 1,5°.

6. Rapport de la NASA (E. Hirschmann, "Trapospheric and Ionospheric Effects on Radio Frequency (VHF-SHF) Communication", Goddard X-731-67-89, 1967): il s'agit d'erreurs de distance aux satellites, et non pas au DX. Il ramène la même formule N et le même profil exponentiel (p. ex. en Floride: Ns 370, ΔN -55, constante 0,161/km) et envoie deux sources NBS à la recherche: Bean et Thayer, "CRPL Expontial Reference Atmosphere", Monographie 4 (1959), et Bean, Horn et Ozanich, "Climatic Charts and Data of Radio Refractive Index for the United States and the World", Monographie 22 (1960).

## 9. D'après le livre DX VHF/UHF, chapitre 2

I. White G3SEK (édité par), *The VHF/UHF DX Book*, DIR Publishing 1995 (ISBN 0 9520468 0 6; reproduction numérique 2017), chapitre 2 "VHF/UHF Propagation", pp. 2-13, 2-22-2-29 et 4-11. C'est le texte le plus proche de l'expérience radioamateur parmi ceux recueillis; confirme les seuils déjà utilisés et ajoute de nouvelles règles synoptiques.

Numéro

- La même formule de N (77,6 P/T + 3,73·10^5 e/T2); exemple: 1000 hPa, 7 °C, et = 10 hPa endommagent N = 324. Dans une atmosphère bien en forme, par exemple après un orage, le N est presque linéaire autour de 39 unités/km sur les 2-3 premiers km.
- Les gradients entre 0 et −79 N/km sont normaux, en dessous de −79 il y a super-réfraction, plus de 0 sous-réfraction. Le livre insiste sur le fait que le long DX ne vient pas de la simple super-réfraction, qui devrait rester droite sur tout le chemin, mais des conduits.
- Épaisseur minimale du conduit, un peu plus prudente que Kerr et Hepburn:

| Bande | Épaisseur minimale |
| 50 MHz |  400 m  |
| 70 MHz |  300 m  |
| 144 MHz |  200 m  |
| 432 MHz |  100 m  |
| 1,3 GHz |  50 m  |

- Le rapport n'est pas proportionnel à la longueur d'onde (environ 65 λ à 50 MHz, 220 λ à 1,3 GHz) parce que les bords du conduit sont "split". Au Royaume-Uni, les pipelines sont fréquents sur 144 MHz, mais presque jamais à 70 MHz ou moins, et souvent les 432 MHz deviennent forts lorsque les 144 sont faibles.
- Dans le pipeline, le signal tombe comme 1/d au lieu de 1/d2: jusqu'à 1000 km l'atténuation peut être similaire à celle de l'espace libre.
- Capacité d'une station minimale sur 2 m (100 W, 15 dBi yagi, 2 dB de bruit): 217 dB, soit environ 400 à 500 km en SSB en conditions moyennes; en CW environ 20 dB en plus (p. 4-7). D'accord avec le tableau KK6MC.
- Scatter: la perte augmente d'environ 15 dB par triple la fréquence et 10 dB pour chaque degré d'angle de dispersion ( formule Yeh; dans le texte extrait la constante est illisible, donc il doit être pris de la version de W3SZ). La dispersion « en route » des cellules convectives couvre systématiquement jusqu'à environ 500 km, 1000 km avec de bonnes stations. Après de fortes pluies et des conditions prolongées sont rares sur toutes les bandes.

Types de conduits

- Sur le sol: sur la terre, ils se propagent mal parce que le sol absorbe et s'étend; sur la mer calme arrivent loin (Scilly-Canarie, 2500 km, environ une fois par an sur 2 m). Les conduits d'évaporation sont souvent d'environ 15 m de haut et servent des micro-ondes.
- Haut: entre 450 et 2000 m, en raison de la DX plus longue au sol; le signal "haut" au-dessus des stations ci-dessous. Les conduits "perdus" donnent des signaux moins forts mais permettent aussi des stations de distance intermédiaire.
- Les conduits ont des directions précises et peuvent durer des heures ou des jours, contrairement à l'E sporadique.

Nouvelles règles synoptiques pour l'index

- Âge de l'anticyclone: l'air tombe au centre de quelques mètres par heure et l'inverse est abaissé à mesure que la haute pression se développe; les plus grandes ouvertures arrivent après plusieurs jours de propagation déjà bonnes. Un anticyclone à court terme ne suffit pas.
- Bord haute pression: la couche est dôme et les bords sont plus bas, donc les stations s'améliorent.
- Direction: On a observé que les routes plus longues longent les isobares ou traversent les dorsales (Flavell G3LTP, 1984), mais il y a aussi d'excellentes connexions en direction radiale depuis le centre. Exemple du 29 au 30 août 1987: de l'Angleterre, l'ouverture a fait 180° en deux jours (Espagne, France, Italie, Autriche, Norvège) à la suite du déplacement de l'anticyclone.
- Contre: épais isobare (vent fort), nuages stratocumuliforme au bord de la couche (limiter la fréquence la plus élevée).
- Fronts: un front froid pousse l'air chaud vers le haut et crée un revers le long de l'avant; la meilleure direction est le long de l'avant en passant, mais il dure peu.
- Saison: Au Royaume-Uni, les anticyclones sont plus fréquents à la fin de l'été et au début de l'automne.
- Indices visuels: la fumée piégée dans une ligne sombre au-dessus de l'horizon; avec une pression élevée, les couches de base inférieures à 1000 m indiquent un canal de terre, une base supérieure à 1000 m un canal de haute.

Références citées à la recherche: R. G. Flavell G3LTP, "Études d'un vaste événement de propagation anticyclonique...", *Radio Communication*, février 1984, p. 128; J. Gannaway G3YGF, "Trapospheric Scatter Propagation", *Radio Communication*, septembre 1981, p. 710; P. W. Sollom G3BGL, "A Little Flutter W6ZGN

## 10. Modèle de radio amateur vérifié: Biancomano et Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, « Practical Tropospheric Model for VHF/UHF Propagation Prediction », *National Weather Digest* 22:2, juin 1998, p. 15 à 24, gratuitement en [PDF de la National Weather Association](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf).

- Données: radiosundations des stations 00 et 12 UTC de 47 stations dans l'est des États-Unis, 1995-1997; analyse au sol à 700 hPa (au début jusqu'à 400), sur le profil de N, sans M. Hors côtes, îles et montagnes.
- Pour chaque enquête: présence et quota du canal, longueur d'onde maximale pouvant conduire (souvent environ 100 fois la longueur d'onde), gradient maximal de la température potentielle entre les niveaux voisins et entre un niveau et la seconde ci-dessus (pour les couches plus épaisses). Les conduits de terre ne sont pas utilisés.
- Première règle: la persistance: les indices de stabilité (indice relevé, K, totaux totaux, SWEAT) restent à moins de 3 unités de l'enquête 12 heures avant, et le niveau de pression où la température potentielle augmente reste sensiblement le même. Si oui: prévisions d'amélioration dans les 12 prochaines heures.
- Deuxième règle, contrôles dynamiques: couche limite qui ne monte pas, CAPE, variation de l'épaisseur à 500 hPa sur trois relevés, variation de la pente à l'intérieur des limites.
- Seuil de bande: un conduit capable de transporter 100 cm ou plus indique une ouverture sur 432 MHz, presque certain sur 222 et souvent même sur 144.
- Filtre spatial: stations sur une grille hexagonale d'environ 400 à 500 km; la prévision ne vaut que si au moins 5 stations de contamination sont positives (si vous pouvez en sauter une). Avec 5 stations, la probabilité d'un faux positif aléatoire devient négligeable.
- Vérification par rapport aux événements de la colonne « Le monde au-dessus de 50 MHz » de la TVQ: en 1996, 48 événements prévus sur 67 (71 %); 12 des 15 événements majeurs. L'exactitude jusqu'à 70% sur la 432 MHz et environ 60% sur la 144 MHz à 12 heures; il fournit presque le double des événements réels; 9 erreurs sur 19 sont arrivées tardivement, car de nombreuses améliorations se forment en moins de 12 heures.
- Ajouté aux stations plus au nord: vent tournant dans le sens des aiguilles d'une montre (démarrage chaud) comme condition supplémentaire.

D'après le PDF complet (fourni par l'auteur du programme) trois autres détails. Leur règle "épaisseur = 100 fois longueur d'onde" donne 600 m pour 6 m, 200 m pour 2 m, 125 m pour 1,25 m et 70 m pour 70 cm (fig. 3): pour les 70 cm est moins sévère que Kerr et Hepburn (environ 89 m), pour les 2 m il coïncide avec le livre VHF/UHF DX. Le vent qui tourne dans le sens des aiguilles d'une montre monte, selon les auteurs, surtout parce qu'il apporte de l'air plus chaud à l'altitude, ce qui fait croître la chute de N avec pression; l'humidité qui l'accompagne ne semble pas déstabiliser. L'appendice obtient dN/dh sous forme fermée pour une atmosphère à gradient thermique constant avec un saut important de gradient thermique et d'humidité à un quota donné: utile comme cas d'essai pour notre code. Leur trace de la pente de la température potentielle vient de K. Jehn, "L'utilisation d'un indice de réfraction potentiel dans la radio météorologie à échelle synoptique", *J. Météorologie* 17, 1960, p. 264.

Que prendre pour notre index: (1) un terme de persistance: comparer le profil avec celui de 12 heures avant, que dans la prévision horaire nous avons libre; (2) le filtre d'espace: un point compte seulement si l'amélioration couvre une superficie de quelques centaines de km, ce qui réduit les fausses alarmes; (3) les indices de stabilité, calculés de température et de rosée à 850, 700 et 500 hPa (Open-Meteo donne déjà l'indice relevé et CAPE); (4) le vent qui tourne avec l'altitude entre 1000 et 850 hPa; (5) leur méthode de vérification, qui est la même que nous voulons faire avec les annonces de programme.

Une autre source dans le même paquet, peu utile pour nous: P. K. Rastogi, "Stratosphere and Troposphere (S-T) Studies at Millstone Hill", NASA 1984 (document 19840019069): radar 440 MHz qui mesure la turbulence de l'air libre entre 4 et 25 km, avec des couches minces liées aux ondes de gravité et à la coupe du vent. Il confirme que la turbulence qui produit la dispersion est stratifiée, mais elle concerne des risques trop élevés pour les conduits.

## 11. Troisième série de sources (20 novembre)

Cas historiques à gaine élevée. J. Macoun OK1VR, liens à 2 m du sommet de Sněžka (1602 m), automne 1958, article de 1959 traduit en 2006 ([ok2kkw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)). Les 27 et 28 octobre 1958, première liaison Tchécoslovaquie-Angleterre sur 145 MHz ( G5YV, 1220 km) et beaucoup plus entre 1100 et 1250 km, avec des signaux stables après 23. L'auteur l'attribue à deux inversions superposées, la partie supérieure d'environ 2200 m, le signal revenant au sol seulement là où la couche inférieure s'est dissoute; condition favorisée par un anticyclone lent de l'Atlantique au continent. De la maison (Prague) son maximum était 530 km: seules les stations à altitude sont entrées dans le conduit. Il s'agit d'un autre cas de vérification (comme LA0BY 2018) et confirme que l'indice devrait être réalisé à plus de 2000 m et comparé à la part de la station.

Bibliothèques historiques NBS (utile pour rechercher des oeuvres classiques, pas pour les formules):

- W. Nupen, *Bibliographie sur la propagation troposphérique des ondes radio*, NBS Technical Note 304, 1965, 356 pages, avec index pour les sujets (aussi "prévision des conditions") et par pays. L. Sacco, « The radio connection in atmospheric super-fraction regime », * High Frequency* 24(6), décembre 1955 (recherche italienne: super-fraction de moins de 500 m fréquentes jour et nuit); S. Silleni, *Annali di Geofisica* 8(1), 1955; J. M. Clara e A. Antinori, prova tra Sardegna e Minorca da 238 a 297 MHz (390 km), *Electrical Communication* 33(2), 1956. A. F. Clarke (Fort Huachuca, 1956) a publié quotidiennement la prévision de la longueur d'onde maximale piégée pour le lendemain matin, à partir des prévisions d'humidité, de température, de pression et de vent.
- A. L. Abbott, *Bibliographie de Tropospheric Radio Wave Scattering*, NBS, matériel jusqu'en janvier 1960, environ 670 articles commandés par an: utiles pour la partie troposcatter.

Divulgation et observations: F. W. Odenthal DK5EQ, "Ausbreitung 2m – Eine Einführung" (31/05/24): presque optique jusqu'à 200 km, tropo par conduit ou réfraction au-delà, avec haute pression et au printemps et en automne; cite la page VHF de DM2HB et les notes de DK5YA sur tropo et Es. A. Johnson VK3DXE sur le blog de EI7GL (décembre 2021): avec les chemins VHF WSPR de milliers de km en Australie ont été découverts; Le trope n'est pas seulement l'été. Discussion sur le port de plaisance VHF et l'AIS ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)]: portée 1,23· √h(ft) milles nautiques par antenne, disperser jusqu'à environ 50 nm, amélioration typique du trompe 100–400 nm; les conduits sont reconnus parce que les stations éloignées viennent aussi fort que les voisins. Chelmsford ARS News, décembre 1983: dans les concours de 2 m les stations de la côte est anglaise sont favorisées par la route maritime vers le continent.

ARRL, *The Radio Amateur's VHF Manual*, 1972, cap. 2 « Propagation Beyond the Horizon » (p. 17-19). Conseils d'exploitation déjà connus (suivre les zones à haute pression qui se déplacent lentement et surtout les bords du dos; début de l'automne la meilleure période; zones côtières, grands lacs et vallées des rivières favorisées par la stratification). Deux nouvelles données: (1) fig. 2-4 compare l'atmosphère standard (humidité relative 70 % jusqu'à 12 000 pi, réfraction minimale) avec un rayon typique de forte réfraction, avec un rapport de rupture de température et de mélange clair d'environ 4000 pi (environ 1200 m): c'est la signature que notre calcul doit reconnaître sur les niveaux GFS; (2) les canaux réels sont rares sous 144 MHz et plus fréquents en UHF, selon les épaisseurs minimales. Bibliographie utile de la TVQ à la recherche: Hull, "Air-Mass Conditions and the Bending of UHF Waves" (juin 1935, mai 1937); Hoisington, "Painless Prediction of Two-Meter Openings" (octobre 1949); Collier, "Upper-Air Conditions for 2-Meter DX" (septembre 1955); Botts, "A Night to Remember" (janvier 1970, mené sur terre).

PA0EHG, page "Tropo" ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): résumé du troposcatter de l'article G3YGF, déjà dans le manuel. Utilisez deux règles pratiques pour contrôler notre calcul de dispersion: environ 10 dB de perte supplémentaire pour chaque angle à l'horizon de chaque station, et plus de 100 km environ 9 dB sur plus tous les 100 km de route (60 miles); le volume commun commence à environ 600 m sur 100 km et à environ 9 km sur 480 km. Il ne traite pas les comportements, les sondages ou les seuils de réfraction.

PA0EHG, calculatrice PROPBER ([page](https://pa0ehg.com/propagat1.htm), propber.exe programme fourni par l'auteur du programme). C'est un programme DOS de 36 kB écrit en BASIC et compilé avec le Compilateur Microsoft BASIC 5.60 (1985): il ne fonctionne que sous DOS ou DOSBox. De textes internes: demande la fréquence, la distance (ou les deux localisations, aussi dans l'ancien format QRA "E-locateur"), pour chaque station de hauteur d'antenne, chiffre de bruit, bande FI, puissance, un obstacle (hauteur et distance) et gain d'antenne (0 = calcul du diamètre de parabole, efficacité 50%). Retourne la distance maximale en optique, la perte optique correcte pour l'absorption de la vapeur (0,1 dB/km à partir de 24 GHz), la perte de troposcatter « correcte pour les sites » (c'est-à-dire pour les angles d'horizon donnés par les obstacles) et le rapport S/N dans les deux sens, également « avec la propagation anormale maximale ». Les constantes numériques ne sont pas lisibles dans l'exécutable compilé. La méthode suggérée pour les bandes élevées est également utile pour nous: étalonner le calcul sur un lien mesuré (différence entre le S/N mesuré et calculé) et appliquer la même différence à une autre bande. C'est le même modèle de notre calcul, mais il utilise le profil S RTM1 au lieu d'un seul obstacle et les prévisions GFS au lieu d'une marge fixe.

PROPBER, constantes lues dans l'exécutable (nombres au format Microsoft Binary Format). πr, dans le tableau de données à côté des textes: 8 68589 (= 20/ln 10, soit 20 logarithmes calculés avec le logarithme naturel), 32,5 (espace libre: 32,5 + 20 log f\[MHz\] + 20 log d\[km\]), 24000 et 0,1 plus 40000 et 0,3 (vapeur: 0,1 dB/km de 24 GHz, 0,3 dB/km de 40 GHz) Probable, parce que vous répétez mais à l'intérieur du code: −69.5 (le même terme constant que la formule de diffusion latérale de Yeh dans le livre VHF/UHF DX), 332 (réduction du sol), 8733 km (rayon équivalent, k, 1.37). La formule de dispersion exacte n'est pas reconstructible sans démonter le programme. T0

PROPBER, formules reconstruites par le code (désassemblage de la partie compilée; les opérations sont appelées à la bibliothèque BASIC avec les opérateurs en mémoire, dont les valeurs en double précision ont été décodées). Les constantes −69,5, 332 et 8733 trouvées plus tôt étaient de faux positifs dans la bibliothèque.

- Distance optique: d = 5 · ( √h1 + √h2) km (h en m). Il est plus généreux que le classique 4,12 (k = 4/3): il est équivalent à k 2.
- Perte en optique: L = 32,5 + 20 log f\[MHz\] + 20 log d\[km\], plus 0,1 dB/km au-dessus de 24 GHz et 0,3 dB/km au-dessus de 40 GHz.
- Troposcatter: Lts = L + 23 + 10 log f + 0,0674·d, c'est-à-dire 55,5 + 30 log f + 20 log d + 0,0674·d (formule de G3YGF avec la constante 2 dB plus élevée). Pas de terme d'angle ou de réactivité.
- Correction "pour les sites": le programme demande obstacle et distance mais le bloc qui devrait les utiliser est vide; la variable de correction reste 0, donc la perte "correcte" est égale à celle de la dispersion.
- Bruit: T = 290·(10^(NF/10) − 1), N = − 228,6 + 10 log T + 10 log B (dBW), sans température de l'antenne.
- S/N = 10 logs P + G\_tx − L + G\_rx − N. "Avec une propagation anormale maximale" utilise la perte optique au lieu de la dispersion, c'est-à-dire le boîtier de conduit idéal sans pertes au-delà de l'espace libre.

Comparaison: 300 km et 144 MHz PROPBER donne environ 189,9 dB, G3YGF 187,9, notre P.452 187,8 (avec des angles d'horizon zéro): la diffusion coïncide avec 2 dB. Notre calcul ajoute les angles d'horizon du profil, la température de l'antenne, et au lieu du "maximum" fixe la couche attendue avec accouplement résiduel et atténuation.

Rohde & Schwarz, P. Denisowski, « Comprendre la propagation VHF », Note éducative, avril 2023 (24 pages, diffusion). Confirme les points déjà collectés (conduits comme guide d'onde bidimensionnel, signal qui tombe de manière linéaire avec la distance, jusqu'à 1500 km et au-delà, durée d'heures à jours; bon sol sur la mer et rare sur terre; élevé de quelques centaines de mètres à quelques km). Deux observations utiles pour l'indice: 1) évolution dans le temps: un nouveau conduit ne porte souvent que les fréquences les plus élevées et, épaississant, commence à apporter même le plus bas; la prévision horaire devrait donc montrer le 432 MHz « à l'avance » sur le 144 MHz lorsque la couche grandit; 2) emplacement des stations: les meilleures conditions sont entre les stations proches des extrémités du conduit, tandis que celles le long du chemin d'un conduit élevé ne parviennent souvent pas à entrer, sauf si elles sont conduites « qui perdent ». Les conduits se déplacent par temps atmosphérique.

Site PA0EHG, autres pages. Pour le trope, PathProfile de M. Willis G0MJW ([scheda](https://pa0ehg.com/srtmp.htm), [téléchargez](http://www.mike-willis.com/Software/ASTERPathprofile.zip)): profil de la route à partir des fichiers de perte totale et de quota attendus, jugé fiable pour le troposcatter au micro-ondes; ne traite pas la dispersion des avions ou de la pluie. Utile comme deuxième référence pour comparer nos profils et pertes S RTM1. Le reste du site couvre EME, micro-ondes, piste d'atterrissage (AirScout, cartes SM7LCB) et mesure en 80 m.

PathProfile par G0MJW (Mike Willis), les paquets ASTER et SRTM et SiteData (fournis par l'auteur du programme; [Page logiciel](https://www.mike-willis.com/software.html)]. Programme Windows de 2018: profil de ASTER ou SRTM à 1" ou 3", pertes en optique, diffraction, troposcatter (valable jusqu'à 1000 km), conduit et gazeux, avec pourcentage de temps, k médiane de la route, fraction de chemin sur la mer et distance de la côte: la structure est celle de P.452/P.1812, mais l'aide (fichier.chm compressé) n'a pas pu être lue ici. SiteData convertit les localisateurs et donne la part de GT OPO30.

La partie la plus utile est les fichiers de données inclus: cartes statistiques UIT-R P.453 sur grille globale de 1,5° (121 rangées de 90°N à 90°S, 241 colonnes de 0° à l'est): gradient dN/dz dans les 65 premiers m dépassé pour 1, 10, 50, 90 et 99 % du temps; pour les conduits du sol pourcentage de présence, d'épaisseur et d'intensité; pour les présences élevées, la part de base, l'épaisseur, l'intensité et l'altitude maximale; plus le terme humide et la température. Valeurs annuelles interpolées:

| Lieu | Pipeline terrestre: % / épaisseur / intensité | Pipeline haute: % / base / épaisseur / intensité | dN/dz dépasse 10% du temps |
| Naples |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | −380 N/km |
| Tyrrhénienne (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | −674 N/km |
| Trapani |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | −520 N/km |
| Brindisi |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | −405 N/km |
| Adriatique (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | − 399 N/km |
| Belgique |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | − 172 N/km |
| Udine |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | − 176 N/km |

Pour notre indice sont une référence climatique prête: l'épaisseur et l'intensité typiques des conduits au lieu des 50 m fixés du conduit potentiel, et une anomalie "aujourd'hui par rapport à la normale" par zone. Données de l'UIT: gratuit pour un usage non commercial, pour vérifier avant de les inclure dans une version publique. Le fichier ZO NES01.DEM (58 Mo, grille 2') contient les zones climatiques intérieures/coast/mer; le format n'est pas documenté et je ne l'ai pas décodé. La page des équations de Willis ne contient que des constantes physiques, de l'espace libre et un gain d'antenne.

Quatrième série de sources (20 novembre).

Indermuehle et Suzuki 2026, prédiction des pipelines GFS pour l'observatoire CSIRO de Murchison (arXiv 2608.12861): c'est la source la plus proche de LIVETROPOVHF. Profils de N du GFS à 0,25° (surface jusqu'à 2 m, puis niveaux isobares, ceux sous le sol rejeté); pour chaque colonne minimale de dM/dz inférieure à 3 km, base et sommet du conduit, déficit M intégré; puis P.452-16 et P.1812-6 avec des routes GLO-90 25 de 98 à 1005 km, de 88 MHz à 2 68 GHz, toutes les heures. Résultats: L'ASC médiane 0,72 sur 25 canaux; le pourcentage de temps P.452 p doit être utilisé comme constante d'étalonnage (choix p = 20%), parce que le modèle de l'UIT avec un ΔN instantané n'est pas défini dans un conduit; la compétence reste presque plate jusqu'à 3 jours à l'avance. À 162 MHz (AIS), les voies reçues ont la fraction de conduit la plus élevée de 67 % (0,26 versus 0,16) et la profondeur de conduit la plus élevée de 49 % (28 vs 19 M) en ce qui concerne les contrôles: sur la mer, le conduit est presque toujours, décide de l'étendue de la terre. Le GFS voit la moitié du temps au sol par rapport à ERA5 (4,5 % contre 10,4 % des cellules-heures), alors qu'en mer, il voit plus (73% contre 56%). Un indice de "corridor" (tuyaux continus le long de la route, couches d'une épaisseur minimale de 40 m) améliore le résultat: confirme que la continuité le long de la route est le bon critère.

Longley et Rice, ESSA ERL 79-ITS 67 (1968): méthode pour les terrains irréguliers de 20 MHz à 20 GHz (après ITM), avec des irrégularités de sol Δh, Ns de 250 à 400, rayon équivalent 8497 km (4/3), horizons, diffraction, dispersion vers l'avant et variabilité dans le temps et dans les endroits pour le climat. Référence statistique, non instantanée.

UIT-R P.834-2: courbure de rayon, équivalent de rayon de Terre k·a, M = N + h/a (h en m, en milliers de km), modèle exponentiel; rien de nouveau que ce qui a déjà été utilisé.

AGARD CP-70 (1970), propagation troposphérique: couches minces de 1 à 100 m avec des gradients forts sont communs; le coefficient de réflexion d'une couche élevée est environ proportionnel à la longueur d'onde, avec une dispersion aléatoire d'environ 14 dB. Mesures à 90 MHz: diminution de 9 à 10 dB liée à un saut N concentré à environ 350 m, au sommet d'une couche de nuages. Le passage d'un front froid élimine les couches: reste la diffraction.

U.S. URSI Report, Commission 2 (1960): épaisseur moyenne des couches de 60 à 210 m de hauteur (considérée comme P.453), gradient minimal pour le conduit 157 à 160 N/km.

Buscemi K2OVS, guide de propagation VHF: distances typiques pour inversion et conduit pour bande, conduits liés à haute pression stationnaire, maximum à l'aube.

Wolinsky-Mancini et autres 2024, AIS dans les îles Canaries: radio AIS et réception; les conduits sont beaucoup plus fréquents dans les mois chauds (inversion des aliseans).

Articles nigérians et malaisiens (Ekah, Iloke, Mat, Igwe): corrélations locales entre la température, l'humidité et la perte mesurée, faibles et contradictoires (r -0,99 à 0,27); utiles uniquement pour confirmer que les dimensions du sol ne suffisent pas.

Herben 1984, thèse TU Eindhoven (microonde): 30 GHz sur 8,2 km et satellite OTS à 11,6 GHz; étincelles pour turbulence, multi-voie et pluie. Pour les micro-ondes, les pertes dynamiques sont dominées par la pluie et la turbulence, et non par les conduits.

Baars, l'antenne réflectrice paraboloidale (microonde): gain d'une parabole G = η·(πD/λ)2 avec une efficacité typique 0,5–0,65; perte d'erreurs de surface selon Ruze, ηs = exp(−(4πε/λ)2). Cela aiderait si nous ajoutions les paraboles comme une alternative à Yagi DL6WU pour 1296 MHz et au-delà.

Déjà analysé ou de faible contenu: Hirschmann NASA X-731-67-89 (double), radar VHF ST de Reid (vents et turbulences, non conduits), diapositives et notes génériques sur la troposphère.

Cinquième série de sources (20 novembre, soir).

Rogers 1997, Radio Science 32(1): estimation bayésienne des paramètres d'un canal élevé (taux de base, déficit en M, épaisseur) à partir de facteurs de propagation mesurés au-delà de l'horizon, avec un modèle d'équation parabolique; expérience COVAR en Californie à 143, 263 et 375 MHz, comparaison avec l'onde radio. Le signal dépend beaucoup de la base et du déficit M (corrigant 0,81 à 143 MHz pour le déficit), presque du tout de l'épaisseur (−0,17): l'épaisseur ne peut pas être obtenue du signal. Pour nous: calcul B (épaisseur du climat) compte moins que déficit et part; et la méthode est la base pour le back office, c'est-à-dire pour obtenir le conduit à partir des fréquences multiples.

Moler et Holden 1960, "Prolifération de dispersion troposphérique et circulation atmosphérique", J. Res. NBS 64D, p. 81 et suivants (lire dans l'original le 22/11/2026, doi 10.6028/jres.064d.014). Routes maritimes de Californie jusqu'à Point Loma (San Diego): Santa Barbara 190 milles, Point Mugu 144, San Clemente 78; 1365 MHz et 9380 MHz, non VHF; janvier-avril 1958, moyenne d'une minute. Le cas du 26 mars: un renforcement d'environ 20 dB pendant environ 3 heures vers midi, quand un vortex cyclonique s'affaiblit et commence la brise et la subsidence de la mer; à 15 la convergence est réformée et le signal baisse d'environ 22 dB du maximum. Statistiques (tableau 1, 500 hPa, 33 cas): avec de l'air qui baisse le signal moyen est supérieur à environ 2,5 dB que avec de l'air qui monte, en bande L et X. Le signal suit la différence calculée à 500 hPa dans le signe et l'amplitude des variations, sans coefficient de corrélation. Les couches basses (moins de 600 m) favorisent une réflexion partielle sur la dispersion. Les anticyclones et les fronts ne sont pas traités, et les "excursions jusqu'à 60 dB" mentionnées ci-dessus ne sont pas: ils proviennent d'un résumé d'occasion. Pour nous: le mouvement vertical est un bon prédicteur, et +1,5 dB pour la subsidence et -2 dB pour l'air de montée sont du même ordre que la différence moyenne mesurée (2,5 dB); 20 dBs sont un seul cas, pas une valeur typique, et sur micro-ondes.

Banafaa et Muqaibel 2025, IEEE Access: levé des pipelines (modèles d'évaporation, équation parabolique, estimations radar, GNSS et lidar) et classification avec apprentissage automatique des mesures réelles; Support machines vectorielles donnent les meilleurs résultats. Utile en tant que bibliographie; les données proviennent du golfe Persique.

UIT-R P.834-7 (2015): version mise à jour de la recommandation sur la réfraction troposphérique; aucune nouvelle formule n'est utilisée.

Abbott, NBS bibliographie sur la dispersion troposphérique (GOVPUB- C13): déjà au troisième tour. Slide Scribd sur les conduits: contenu générique. L'article ScienceDirect 002191698790030 ne pouvait pas être ouvert.

Sixième tour (20 novembre, nuit): Bean et Dutton, Manuel ARRL 2022 et 2023, Balanis.

Bean et Dutton, Radio Météorologie (cap. 6), parties non encore utilisées. Sensibilité au champ aux Ns: 0,18 dB par unité N sur le cycle annuel; 0,14–0,24 dB/N dans le cycle diurne (maximum tôt le matin), jusqu'à 0,2–1,1 dB/N sur les routes de 175–200 km en hiver; corrections 0,8–0,95. Paramètre composé de Misme: M = a(g\_e − 40 + b·ΔW^n) avec a = 0,5 dB par N/km, qui combine le gradient équivalent et la stabilité thermique ΔW (travail pour soulever l'air de 1 km): au Sahara à 471 MHz sur 160 km, la seule stabilité est de 20 dB entre janvier et juin à un gradient presque égal. Indice de réfraction potentielle K (N rapporté adiabatiquement à 1000 hPa): met en évidence les masses d'air et la subsidence anticyclonique (Flavell et Lane).

Manuel ARRL 2022 (99a éd.), Article 19.4.3-19.4.4. Les inversions utiles entre 250 et 2000 m; les inversions de rayonnement (sérum, air calme, sol sec) mènent rarement à la réalité; les anticyclones lents donnent les canaux les plus étendus par subsidence, avec des inversions entre 500 et 3000 m, plus forts le soir et le matin; dans l'hémisphère nord, les meilleurs itinéraires sont au sud du centre élevé. Front chaud: bonnes conditions à quelques centaines de km d'avance; Front froid: arrière fort mais instable derrière le front, parallèle. Foehn (aussi dans les Alpes) et brise de terre (air frais au sol, chaud à 200–300 m; s'étend sur 800 km le long de la côte). Scirocco: air chaud et sec du Sahara sur air marin plus frais, très fortes inversions de l'est de la Méditerranée à Gibraltar, cause probable des records sur 1500 km en micro-ondes. Troposcatter: limite théorique d'environ 800 km, moitié typique maximale. Le 100e éd. (2023) a un texte plus court: inversions au lever et au coucher du soleil jusqu'à +20 dB et +20–50% débit; 1200 miles sur les Grands Lacs, typique 400–600; recommande Radio Mobile (modèle ITM) pour la couverture Hepburn et les cartes.

Balanis, Antenna Theory (4a ed.), § 10.3.3: Tableau Viezbicke (NBS TN 688, 1976) pour Yagi optimisé à 400 MHz: boom 0,4 / 0,8 / 1,2 / 2,2 / 3,2 / 4.2 / 4.2 λ → 7.1 / 9,2 / 10.2 / 12.25 / 13,4 / 14.2 dBd. Notre formule DL6WU (8,6 + 8 log10 boom/λ) donne 1–1,7 dB moins entre 0,8 et 4,2 λ et retourne en ligne avec les tableaux DL6WU à 9,5 λ: estimation prudente. Avec la formule correcte sur l'original (9,4 + 7,8 log10 boom/λ, à partir du 22 novembre), le rejet tombe en dessous de 0,2 dB entre 1,2 et 4,2 λ (12,1 dBd à 2,2 λ versus 12,25) et reste de 0,6 dB à 0,8 λ.

Septième tour (21 novembre): QST, QEX, W3SZ, dépôt GitHub.

QST 2000-2025 (14 numéros, 10 lits avec reconnaissance textuelle). Seulement des rapports sur The World Au-dessus de 50 MHz, utiles pour valider le modèle: du 5 au 9 septembre 2000, effectués à 144 MHz et au-delà sur la moitié est des États-Unis; du 14 au 15 août 2022, ouvrant Manitoba–Kansas/Missouri sous haute pression par jour chaud, de 144 à 1296 MHz presque tout dans FT8, record canadien à 10 GHz de 1228,56 km ( VE4MA – N0OY) dans Q65, signaux jusqu'à 1941 km; la carte de propagation APRS montrait la même zone. QST novembre 2022 recommande des cartes Hepburn. Pas d'article technique sur la prévision du tropo.

QEX et K1JT, Franke et Taylor (QEX 2020) sur les modes FT4/FT8: seuils de décodage FT8 −20,8 dB sur 2500 Hz sur le chenal sans évanescence, −18,6 dB avec évanescence à latitude moyenne; FT4 −17,5 dB. Les autres nombres de QEX 2023 et 2026 ne traitent pas le tropo (trois nombres 2023 sont des scans non lus). W3SZ 2019: seuils en mode lent ( JT4A −23, JT9A −27, JT65A −25, QRA64A −26, FT8 −21 dB) et critères de choix entre la sensibilité et la tolérance au changement de fréquence.

GitHub. TropoLink (LukiBox, Apache-2.0): conception d'itinéraires de troposcatter avec P.617-5 (avec N0 et ΔN), NBS TN101 et ITM flanqués, et leur différence indiquée en tant qu'incertitude; P.617-5: L = F + 22 log f + 35 log φ + 17 log d + Lc, F = 0,18· N0 ·e^(−hs/7,35) − 0,23·ΔN, Lc = 0,07·e^(0,055(Gt+Gr)). eti-f/troposcatter ( DL2FZN 2013, du manuel RSGB VHF/UHF): Lts = 24,44 + 4 283 ln f + 0,06656 j au-dessus de l'espace libre, pente près du PROPBER (0,0674 dB/km). QRadioPredict: ITM et ITWOM 3.0 avec S RTM3, l'utilisation du sol Corins et diagrammes NEC. Sans objet: Tropos (application météorologique), opéra-adt/tropo et dépôt (délai troposphérique pour Insar), tgbot\_troposcatter (station militaire), sdr-troposcatter-gui (panneau de commande), Troposcatter-Propagation-Delay (délai ERA5), Radio-Communication-Project (simulateur didactique), PyLayers (canal).

Huitième tour (21 novembre): modèles NTIA et UIT sur GitHub. NTIA/itm (Longley-Rice, ITS): 20 MHz-20 GHz, perte d'espace libre, diffraction et troposcatter avec profil de sol, 0,5-3000 m terminaux, réactivité de surface N0 (minimum réduit au niveau de la mer, 250-400), sept climats, constantes de sol et différents modes 1 vue, 2 diffraction, 3 troposcatter; C++ avec cadre.NET 4.7.2 boîtier. Il ne modélise pas les conduits: il sert de référence "atmosphère normale" avec variabilité statistique. NTIA/p528 et eeveetza/p528 (MATLAB): UIT-R P.528-5 pour les connexions air-sol, 100 MHz–30 GHz, 1,5–20 000 m terminaux, p 1–99 %;.NET 4.8 boîtier. Utilisation possible: équilibre de dispersion de l'air (deux routes au sol – avions et section radar). eeveetza/p2001 (MATLAB): UIT-R P.2001-6, modèle plein champ avec diffraction, troposcatter, propagation anormale (conduite et couches, Lba) et E sporadique, pourcentage de temps 0-100%; les cartes numériques de l'UIT (DN\_Medien, DN\_SupSlope, DN\_SubSlope, dndz\_01, TropoClim, h0, surfwv, Es) ne sont pas redistribuables. Utilisation possible comme comparaison UIT au lieu de P.452, avec le GFS ΔN au lieu de cartes comme dans l'étude CSIRO; les cartes devraient être téléchargées par l'utilisateur selon P.453. Validation 1.3.0 sur 593 QSO (29/12/2021–2/1/2022): séparation événement/comparaison 0,79 sans corrections, 0,91 avec misme et subsidence; le GFS ne voit pas le conduit en mer à 144 MHz (guadagno sans corrections jamais plus de 15 dB).

Neuvième tour (21 novembre): IU3QEZ, NTIA, PyWaveProp, ACE High, validation 1.3.1. IU3QEZ (S. Fabris), présentation "Sur l'horizon en VHF" (GEKO Magazine n. 69, septembre 2026): volume commun d2/(8·R\_eff) avec R\_eff 8500 km (1,3 km à 300 km, 3,7 km à 500 km chaque niveau d'horizon d'une valeur de 69 miles (111 km), marge de 7 dB pour l'evanescence et 4 dB pour la réflexion au sol; thèse: 10 dB est d'environ 100 km; station IQ3QC 5 W avec 3×5 éléments, 120 QSO, maximum 788 km (Alpe Adria 2026). Bibliographie de la présentation à récupérer: Norton 1959 (J. Res. NBS 63D, System Loss), Bray 1961, ARRL Antenna Book 2023 fig. 4.29, Rice-Longley-Norton-Barsis NBS TN 101, Bean and Thayer 1959, Barsis and Kirby 1961 (différenciation sur crête), Deygout 1966, P.1812, AirScout DL2ALF, P.834 NTIA PropLib (wiki): eHata, ITM, LFMF, P.528, P.2108 (perte d'encombre), avec C++,.NET, MATLAB et Python; pas de modèle de conduit; page ITS: aussi IF-77 (air–sol) et MPM (1 GHz–1 THz). PyWaveProp (Python): Méthodes d'équations paraboliques avec approximations Padé, diffraction multiple des bords, boîtiers PETOOL et RAM, conduits troposfères et hauts, sol irrégulier: base possible pour un calcul de champ avec le profil GFS M(z, à vérifier. APRS PropView (Apache 2.0): Cartes de propagation VHF de paquets APRS, statistiques horaires et alarmes d'ouverture; source possible de données de 144 MHz pour la validation. Réseau de trotscatter de l'OTAN en Italie (fortifications.net): 900 MHz, 10 kW, 20 m de paraboles; stations dont Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Vulture, Monte Lauro; cas d'essai historique pour P.617. Sans objet: Gupta 2021 (transport stratosphérique), loi 2023 (ozone arctique), dauphin no 590 et opéra-adt/tropo (délai InsAR), tropo.ino (capteurs de gaz Arduino), tropo-webapi (téléfonia), cloudtools/troposphère (AWS). Validation 1.3.1 sur 564 tropo QSO: ASC 0,79 (modèle 1.2), 0,80 (+ canal mou), 0,90 (+ misme et subsidence), 0,90 (+ persistance); couleurs de conduit (≥ 16 dB) sur 46% des voies d'ouverture et 0% des comparaisons.

Ajouté au même tour. mikelytaev/onde-propagation (MIT, Python 3): équation parabolique avec approximation de Padé et Numerov, sol irrégulier, canaux troposphériques; charge la réfraction modifiée M(quota, lat, lon) de la NOAA GFS et pose un conduit d'évaporation de Monin-Obukhov; aucune fonction documentée qui renvoie la perte sur le chemin. Il est le meilleur candidat pour un futur calcul de champ avec le profil GFS M (il servirait un service externe ou une traduction). RP-Sim (AGPL-3.0, JavaScript): Modèles empiriques pour réseaux radio (Hata, He, Ericsson, deux rayons), pas de tropo. RIMFading (GPL-3.0, OMNeT++): irrégularités de débit pour simulations réseau, distribution Weibull, pas de tropo. PyLayers: chaîne radio intérieure, déjà exclue. qradiopredict (kantooon, GPL v2): ITM et ITWOM v3.0 avec S RTM3 et Corine, déjà dans le septième tour comme QRadioPredict.

Dixième tour (21 novembre): Livre d'antenne, Norton, Barsis, nouvelles sources de données. Livre d'antennes ARRL (23a éd.), cap. 4 ( K9LA): Méthode de Bray (QST novembre 1961) pour la gamme VHF: perte de Terre en douceur à 99 % (300 milles, 144 MHz: 214 dB), courbes à 50 % et 99 % (fig. 4.11): 195 dB d'une valeur de 100 milles à 99 % et plus de 250 milles à 50 %; évanescence ±7 dB sur 100 milles (-3,5 dB à 50 milles), SSB 3 dB pire que la CW, +4 dB de réflexion au sol, +3 dB pour l'accouplement de deux antennes; gain de hauteur (15 m: 4/3/2 dB à 10/50/100 milles; 24 m: 8/6/4 dB); Yagi gagne environ 10 fois le boom à λ (dBi); station 50 W avec antenne moyenne: environ 180 dB, 100 milles, +10 dB jusqu'à 250 milles. Lever et coucher du soleil jusqu'à +20 dB et +20–50% débit; air chaud et sec sur air frais humide sur les Grands Lacs jusqu'à 1200 miles, typique 400–600; régions influencées par de grandes masses d'eau les plus favorisées; fin d'hiver avec chauffage capable d'inversions fortes. Vérification de notre troposcatter: P.452 à 50% à 483 km et 144 MHz donne environ 204 dB, ce qui correspond à la 214 dB de Bray à 99 %. Norton 1959 (J. Res. NBS 63D, libre sur NIST): perte de base, gain d'antenne système et chemin; Atmosphère de référence Ns = 301; rayon équivalent de Ns (250 → 4878 milles, 301 → 5280, 400 → 6997); tables L(99) avec antennes de 30 pieds (100 MHz: 163 dB à 100 milles, 208 dB à 500 milles; 1000 MHz: 138 et 188 dB avec paraboles de 28 pieds); variabilité à long terme V(p, Φ) selon la distance d'angle (fig. 13). Rice, Longley et Norton, NBS TN 15 (1959), libre sur NIST. Barsis et Kirby, NBS Report 6751 (1961): 223 km dans le Colorado avec Pikes Peak (4292 m) comme bord, 100 et 751 MHz; perte constante avec le bord arrondi (environ 11,5 km de rayon); évanescence à long terme 11,7–14,3 dB versus 39–42 dB d'une dispersion de la même longueur; aucun cycle de jour marqué; le modèle à quatre rayons fourni 4–5 lobes mais seulement un est mesuré (maximum à environ 21 m du sol): les sentiers avec obstacle de montagne sont beaucoup plus stables que la dispersion, utiles pour les connexions «via Vesuve ou Faito». ARRL VHF Digital Handbook: paquet, APRS, D-STAR, WSJT (Scatter Method et EME), aucune partie sur le tropo. Nouvelles sources de données éprouvées ou recherchées: ECMWF données ouvertes (IFS 0,25°, niveaux 1000, 925, 850, 700 hPa..., sans 975, 950, 900 et 800; G RIB2 avec compression CCSDS, que notre lecteur ne décode pas; déjà disponible via Open-Meteo); DWD ICON-EU données ouvertes (G RIB2 compressé bz2, un fichier pour variable et niveau: de nombreuses demandes); GGPS Canada sur MSC Datamart et GeoMet (environ 15 km, jusqu'à 33 niveaux; GeoMet permet la culture de la région; il est la base des cartes Hepburn); GFS NOAA archive sur AWS à partir de février 2021 (fichiers globaux, utiles pour la validation); APRS PropView seulement pour la validation.

Onzième tour (21 novembre): Longley-Rice, GEKO 69. Bean et Thayer, «Modèles de l'indice de réfractivité radio atmosphérique», Proc. IRE mai 1959 (p. 740 et suiv.): le modèle 4/3 a la pente droite au premier km mais tombe trop haut; 30% de la courbure d'un rayon se produit au premier 100 m et 60% au premier km; rapport moyen entre Ns et diminution au premier km: −ΔN = 7,32·e^(0,00577·Ns) (Ns = 313 → ΔN environ −42). haricot et Thayer, "C.R.P.L. Exponential Reference Atmosphere", J. Res. NBS 63D n. 3 (1959): N(h) = Ns·e^(-c·h) avec c fixés par Ns à travers le même rapport; Moyenne Ns USA 313; tableaux de suivi des faisceaux (courbe totale 15,2 mrad avec modèle exponentiel versus 30,3 mrad avec Terre 4/3). MATLAB réfractionexp: c = 0,143859 km−1 pour Ns = 313 (échelle d'environ 6,95 km), valable au-dessus d'environ 1 km et 10 mrad. Utilisation pour nous: le profil "normal" de comparaison peut être que CRPL avec les N locaux au lieu de M = 315 + 0,118·z fixé, et la diminution anormale au premier km doit être mesurée par rapport à -7,32·e^(0,00577·Ns) au lieu de -40 fixé. NTIA Report 82-100 (Hufford, Longley, Kissick 1982), guide ITM en mode zone: climi et Ns suggérés (équatorial 360, subtropical continental 320, subtropical marin 370, désert 280, tempéré continental 301, tempéré marin sur terre 320, tempéré marin sur mer 350); Ns = N0 ·e^(-hs/9,46 km); vous utilisez le minimum mensuel de Ns parce que moins influencé par la super-fraction et la sous-fraction; Ns = 301 correspond à ae = 8497 km; moins de 100 km Ns compte peu. Rice, Longley et Norton, NBS TN 15 (1959), partie I: formule prévisionnelle de répartition au fil du temps par vague terrestre et troposphérique, base du TN 101. SoftWright, Wikipedia, Altair WinProp et MATLAB rfprop. LongleyRice: même information (version 1.2.2, 20 MHz-20 GHz ou 40 GHz, 1–2000 km, Ns 301, sept climats, variabilité du temps, du lieu et de la situation); personne ne forme les conduits, seulement l'effet statistique des climats marins (« champs plus élevés pour 10 % du temps ou non »). GEKO Magazine n. 69 ( IU3QEZ): horizon radio 4/3 de 130 km à partir de 1000 m, 260 km ajoutant deux stations; troposcatter avec un volume commun entre 1–3 km et 10 km; disperser de l'aéronef à 10 km avec +10–20 dB pendant des dizaines de secondes ou quelques minutes; cours théoriques avec 2×10 éléments par côté: SSB QRP environ 440 km, SSB 50 W environ 510 km, CW QRP environ 600 km, SSB 500 W environ 640 km (500 W en SSB font 40 km plus de 5 W en CW); 90–99 % du temps devrait être enlevé une marge de 10–20 dB; 5–10 dB par degré d'horizon sur de longs tronçons; le quota compte pour libérer l'horizon, non pour approcher le volume commun; Alpe Adria 2026: 99 % du troposcatter QSO, presque personne par air malgré AirScout.

## 12. Table ronde: sources pour valider le modèle ou le corriger (21 novembre)

Résumé de tous les tours de sources, commandé pour utilisation. « État » indique si la source est déjà dans le programme (1.3.1), si elle est à l'essai ou si elle doit être faite.

### Données à valider (ce qui s'est vraiment passé)

| Source | Ce qui donne | Utilisation | État |
| MMMonVHF rend compte de l'auteur | 593 QSO à 144 MHz, 29/12/2021–2/1/2022, heure et localisation | Validation de l'événement par comparaison (+14 jours) | Utilisation: ASC 0,79 (1.2) → 0,90 (1.3.1) |
| Le monde au-dessus de 50 MHz | cas documentés (14–15/8/2022 Manitoba–Kansas, 144–10 GHz) | historique | utilisé comme cas historique; à ajouter aux OQS de validation |
| wspr.live (WSPR) | archive complète, 2 m et 70 cm, SQL | de nombreux événements et beaucoup de "non" (heures sans place) | essayé, il devrait être filtré (spot faux à 16-20 mille km) |
| APRS PropView / APRS-IS | stations feutrées, distances, heures | validation unique 144 MHz | à faire |
| Beacon inversé (archive) | tache quotidienne | VHF | rejeté pour le moment |
| I GRA2 radiosondaggi (Brindisi 16320, Pratique de Mare 16245, Trapani 16429, Udine, Milan) | profils mesurés 00 et 12 UTC | Contrôle du profil GFS et inversions | fichiers trouvés (Brindisi 46 MB), à utiliser |
| Barsis et Kirby 1961, Barsis 1963 | Évanescence mesurée sur des chemins à obstacle montagneux | valider la catégorie "monte" (Vesuvio, Faito) | à faire |
| Norton 1959, NBS AMT 15, AMT 101 | L(99) et pertes sur supports de table | Contrôle de la dispersion | partiellement (voir 300 milles constants) |

### Corrections proposées par les sources

| Correction | Sources | Effet mesuré sur la validation | État |
| Stabilité du misme Durée | Bonnet Bean et Dutton. 6 | ASC 0,79 → 0,90 avec la subsidence | en 1.3.1 (titre 16 dB sans conduit) |
| Subsidence du mouvement vertical | Moler et Holden 1960 (lire dans l'original le 22/11: +2,5 dB moyenne entre l'air descendant et l'air montant, 1,4 et 9,4 GHz par la mer) | comme ci-dessus | en 1.3.1 (+1,5 / −2 dB) |
| Pipeline souple (λ jusqu'à 3·λmax) | Kerr, Hepburn, Livre VHF/UHF DX, Shi 2019 | seul +1% de l'ASC; apporte les couleurs du canal sur 46% des voies de l'événement et 0% de la comparaison | en |
| Persistance 24 h | Livre VHF/UHF DX, Whiteman et Leyshon | ASC inchangée, plus prise et plus de fausses alarmes | au paragraphe 1.3.1 |
| Profil CRPL normal avec Ns locaux (−ΔN = 7,32·e^(0,00577·Ns)) au lieu de 40 N/km fixé | Bean et Thayer 1959, NBS Monographie 4 | testés en validation | au paragraphe 1.3.2 comme solution de rechange, en cas de défaut |
| Seuil de super-réfraction 79 N/km (UIT) au lieu de 100 (Bean et Dutton, couche 1000-850) | P.453, Lopez 2008, Livre DX | testés en validation | en 1.3.2: estimation agressive à côté de la prudence (la carte utilise la prudence) |
| Épaisseur climatologique (calcul B) moins importante que le déficit | Rogers 1997 | pour essayer: utilisez A pour gagner | à faire |
| équation parabolique dans le profil GFS | PETOOL, propagation des vagues (Lytaev), Levy (livre non lu, seulement aperçu), Lindquist | première version avec champ sur l'horizon dominé par le bruit numérique (ASC 0,12–0,28): correct (domaine 8 km, faisceau 6°); après correction ASC 0,54–0,66 | comme deuxième calcul, ne décide pas de la prévision |
| Évanescence: ±7 dB sur 100 milles, marge de 10 à 20 dB à 90 à 99 % | Bray 1961, Livre d'antenne, GEKO 69 | seuils de voies dans des conditions marginales | en usage: probabilité pour la période et le seuil FT8 −18,6 dB dans le troposcatter |
| Volume commun et distance équivalente de Bray | IU3QEZ, Bray 1961 | Informations | en |
| Troposcatter P.617 et RSGB comme incertitude | TropoLink, DL2FZN | Informations | au 1.3.0 |

### Ce qui reste hors des modèles

Conduit à la mer à basse altitude (100–200 m) que les niveaux GFS ne voient pas (étude des sources de données actuelles); réflexion et diffraction sur les montagnes voisines (Vesuvio, Faito): proposition de calcul "via monte". ITM/Longley-Rice reste une référence statistique "jour normal" (Ns 301, climi), sans conduits. Rejeté par décision de l'auteur: AirScout, P.528), E sporadique (P.2001) et 50 MHz (terroir EIRP que peu ont et confondent avec l'E sporadique).

### Proposition d'ordre de travail

1. Validation étendue: ajouter WSPR filtré (même heures sans tache) et QST 2022; répéter pour 432 MHz.
2. Essayez le profil CRPL normal et le seuil de l'UIT de 79 N/km, un à la fois, ne conservant que ce qui augmente l'ASC.
3. Comparaison des profils GFS avec l'onde radio I GRA2 de Brindisi et la pratique de la mer à l'époque du QSO.
4. Réévaluer l'équation parabolique après correction numérique.

### Décisions du 21 novembre (version 1.3.2)

- Adopté: estimation prudente (Bean et Dutton, 100 N/km) et agressivité (UIT, 79 N/km) flanquée; conduits recherchés seulement moins de 2500 m (premiers 1500 m, relevés après validation); bonus avec pression prolongée élevée et vent faible; poids en mer avec le μ1 de P.452; part des stations par rapport au conduit également sur le conduit partiel; couche limite au-dessus des crêtes ( IT9GSF).
- Adopté comme alternative, désactivé par défaut: profil CRPL normal avec Ns local.
- équation parabolique correcte numériquement: ASC de 0,12 à 0,54 antenne (30 m) et de 0,28 à 0,66 (300 m).
- Terrain: Copernicus DEM GLO-30 au lieu de Tuiles Terrain «skadi», qui près de la côte ont des reliefs fantômes de 80–100 m en mer (signation de IC8TEM, vérifié point par point à Capri); skadi reste comme réserve.
- Signes météorologiques sur la carte avec leurs effets: subsidence favorable (Moler et Holden 1960, en moyenne environ 2,5 dB entre l'air descendant et l'air montant; +1,5 dB), ↑ montée de l'air défavorable (correspondants profonds; -2 dB), indice favorable S scirocco (Manuel ARRL § 19.4, fortes inversions sur la Méditerranée; aucune correction). Double symbole, couleur verte/rouge/orange et bande dessinée sur la carte.
- Adopté l'IMT 1.2.2 (NTIA/itm, domaine public), traduit en C#: selon le calcul de la base en atmosphère normale à côté de P.452 et l'étirement final après la sortie d'un conduit qui se termine avant le correspondant. Refusé le code qradiopredict parce que sous GPL v2.

Sans contenu utile pour l'index: *Communications trimestrielles*, automne 1991 (pas d'article sur le tropo; l'article d'Atkins sur le Scatter est en hiver 1991).

- Adopté comme principe des manuels (21 novembre): «la propagation ne peut pas être généralisée, mais étudiée en relation avec la morphologie locale» ( DF5AI, rapportée par IK3XTV dans «Propagation sur 2 mètres»); cité dans le manuel de prévision (§ 1) et dans les conseils.
- Adopté le 21 novembre, après le 1.3.3, l'interprétation des ouvertures transalpines de IK3XTV: non pas "cellules convectifs telles que les lentilles", mais troposcatter renforcé par la turbulence jusqu'au sommet de la couche de refonte lorsqu'elle atteint la part du volume commun (manuel des prévisions, § 8 ter). À partir de 1.3.4 il entre dans le calcul point-point comme un renforcement provisoire du troposcatter, au plus 3 dB. Disparus par les mêmes articles: l'"auto-amplification" des signaux au-delà des Alpes (un milieu passif ne s'amplifie pas: les liens décrits restent explicables avec la diffraction et le troposcatter; les pouvoirs déclarés ne sont pas utilisés comme donnés), la neige qui "mène la diffraction" (probablement la couche stable sur la neige, avec moins d'evanescence).

- Kerr (1951) a examiné le livre (22 novembre, PDF fourni par l'auteur): § 1.5, eq. 17 et 18 et tableau 1.1. Les chiffres de l'éq. 18 dans le scan sont illisibles, mais le tableau 1.1 confirme: 20 pi → 1 cm, 80 pi → 10 cm, 400 pi → 100 cm, 2000 pi → 1000 cm, contre 1,25, 10, 112 et 1252 cm de λmax = 0,014·d^1,5 (λ en cm, d debout; la table ronde, 'valeur indicative'. Les hypothèses de Kerr: à l'intérieur du conduit les gouttes d'indice modifiées de 4·10−8 par pied, valeur typique des conduits bas; avec des gradients plus faibles ont besoin d'une plus grande épaisseur. Kerr avertit également que λmax n'est pas une coupe nette: des longueurs d'onde parfois plus longues peuvent encore être fortement influencées par le canal.

- DL6WU examiné sur l'original (22 novembre, PDF de VHF Communications 3/1982 fourni par l'auteur): G. Hoch, «Extremely Long Yagi Antennas», p. 130 et suivants. Figure 1: gain sur le dipôle mesuré à 432 et 1296 MHz dans un polygone professionnel des Postes allemands, avec une antenne échantillon EIA de 7,7 dBd, précision estimée 0,5 dB; 2,35 dB pour doubler la longueur de la flèche, pas de saturation jusqu'à environ 20 λ. Le programme utilisé 8,6 + 8.0.log10(L/λ) dBd, c'est-à-dire environ 0,8 dB dans moins de 1–2 λ; il utilise maintenant 9,4 + 7,8·log10(L/λ) dBd (le gradient est celui indiqué, la valeur à 1 λ est lue sur le graphique, ± 0,3 dB). Distribué la formule 10·log10(7,8·L/λ) circulée sur le net: donnerait 3 dB pour le doublement, contre 2,35 mesuré.
- Booker et Gordon (1950): référence confirmée par "This Week's Citation Classic" (Contenu actuel, 8 février 1982, commentaire de H. G. Booker): Proc. IRE 38, p. 401 à 412. Le commentaire confirme les deux points utilisés dans les manuels: champ reçu au-delà de l'horizon avec distribution Rayleigh, et fonction d'autocorrélation exponentielle des irrégularités (non-Gaussian). L'article original n'a pas été lu.

## 13. À partir des articles IK3XTV

Les deux articles de Flavio Egano IK3XTV (Radio Magazine: "Propagation sur 2 mètres" et "Propagation VHF – cellules convectifs et ondes gravitationnelles") ajoutent deux choses que les modèles de conduit ne couvrent pas: les ouvertures transalpines de l'après-midi dues à la convection, et les règles pratiques sur l'endroit et quand les conduits sont formés en Italie. Ce sont des observations de station, pas des mesures: elles doivent être traitées comme une hypothèse pour vérifier avec les taches.

| Commentaires de l ' auteur | Comment cela devient un ingrédient de l'indice |
| Les conduits sont principalement formés entre 100 et 1000 m, rarement plus haut; servir à haute pression sur une zone large, bonne insolation et absence de vent | recherche du conduit seulement moins de 1500 m; bonus avec haute pression étendue et vent faible dans les couches basses |
| Dans les mois chauds conduits récurrents sur l'Adriatique entre la Vénétie et les Pouilles, 700–800 km, parfois jusqu'à la Grèce; les itinéraires par mer sont favorisés | poids supérieur à la fraction de la route maritime (comme le μ1 de l'UIT-R P.452) |
| Le signal correspondant doit être à l'intérieur ou à proximité du conduit; avec des antennes élevées et quelques mètres de plomb, le signal n'entre pas. | comparer la part de la base du conduit avec la part des deux stations (servera l'altitude du QTH) |
| Même heure, même conduit: écoutez depuis le Gargano une station en 144 et une sur 50 MHz | vérifier: selon le tableau Hepburn les 50 MHz demandent plus de 300 m de plomb, de sorte que l'événement indiquerait un pipeline très souvent |
| Connexions Veneto – Allemagne du Sud au-delà des Alpes, sans visuel, presque toujours dans l'après-midi et à haute pression; signaux instables qui disparaissent et réapparaissent après quelques minutes | Indice "convectif" séparé du conduit, pour les routes de montagne |
| Les cellules convectifs naissent au sol et s'élèvent environ 1 km toutes les 3 heures: à 15, elles atteignent environ 3 km, l'altitude qui sert à dépasser les Alpes | utiliser la hauteur limite de la couche (boundary\_layer\_height of Open-Meteo GFS) le long du chemin: score quand il dépasse l'altitude des crêtes |
| Les Alpes génèrent des ondes gravitationnelles qui, avec des mouvements convectifs, créent des irrégularités de l'indice de réfraction | hypothèses: vent fort perpendiculaire à la chaîne à 700 hPa comme signal d'ondes or; pour essayer en dernier |
| Très haute inversion (jusqu'à 8 km dans des cas exceptionnels) donne environ 800 km, mais en pratique rarement plus de 300 km | la part de l'inversion étend le champ d'application: données utiles pour estimer la distance d'attente |
| En été Rayon de la Terre équivalent à environ 16000 km (2,5 fois le réel), avec environ 60 dB en moins de 300 km de perte | compatible avec k50 = 157/(157 − ΔN): k = 2,5 correspond à ΔN environ 94 N/km, c ' est-à-dire à la superfraction |
| Plus de 3000 km seulement au nord et au sud de l'équateur, au-dessus des mers, reliés aux cellules Hadley; pic en août en Afrique-Europe | en dehors de l'Europe: utile de ne pas donner des indices élevés sur l'Atlantique sans retour d'information, et pour la route des Canaries – Grande-Bretagne |

Autres données utiles des mêmes articles:

- Distance optique entre deux h1 et h2 mètres (d en km): d = 4,1 · ( √h1 + √h2), jusqu'à environ 20 % pour la réfraction normale.
- Évanescence: lente et peu profonde avec inversion; forte et rapide avec des cellules dispersées et convectifs. Le programme pourrait l'utiliser pour classer les annonces si un jour il aura des rapports SNR au fil du temps.
- Pour l'E sporadique dans 144 MHz, l'auteur rapporte le travail de DF5AI et Davis et Johnson (Nature 435, 2005) sur la liaison entre temporal et Es: hors du tropo, mais un indicateur distinct demain pourrait utiliser des données de foudre.
- Réflexion au sol dans le double saut Es: mer K = 81 et 5 S/m, campagne et bois K = 13 et 0,006 S/m, zones urbaines K = 3 et 0,001 S/m.

## 14. Hypothèses de l'indice LIVETROPOVHF

Proposition de départ, à discuter: un indice 0-10 sur la même échelle que Hepburn, calculé pour chaque point de la grille et toutes les 3 heures de GFS (alors ECMWF et ICON pour la comparaison), et un second nombre par route qui indique combien de partie de la route passe dans un canal utile à la bande choisie. Les poids ci-dessous sont mes hypothèses, à étalonner avec les taches.

Revue après Bean et Dutton et Kerr: les seuils de profil proviennent de leur tableau 6.5 et s'échellent à partir de leurs mesures, de sorte qu'une couche réfractaire vaut 10 à 25 dB sur le profil normal. L'indice est ensuite calculé d'abord en dB puis prend 0–10; les valeurs dans ce ciseau restent à étalonner avec les taches.

Calcul par point, étape par étape

1. Décharge T, humidité relative, hauteur géopotentielle et vitesse verticale à 1000, 975, 950, 925, 900, 850, 800, 700, 600 et 500 hPa, plus humidité T et 2 m, pression au sol, hauteur de la couche et vent à 10 m.
2. Éliminer les niveaux sous le sol; calculer N et M à chaque niveau (UIT-R P.453).
3. Calcul -dN/dh dans chacune des quatre couches de Bean et Dutton (1000-850, 850-700, 700-600, 600-500 hPa) et le graphique comme sous-réfractive, normale ou super-réfractive avec les seuils de leur table; à l'intérieur de la première couche également utiliser des niveaux intermédiaires pour trouver le gradient le plus fort et sa part.
4. Où dM/dh < 0 je trouve la base et le haut du conduit, l'épaisseur d et le déficit ΔM.
5. Comparaison de l'épaisseur minimale de la bande à partir de la formule de Kerr (environ 180 m pour 144 MHz, 89 m pour 432 MHz, comme dans le tableau Hepburn); sous le seuil, le conduit compte encore, mais moins.
6. Je stimule le gain en dB avec le tableau ci-dessous (vous prenez la meilleure couche, pas la somme des couches, plus le terme d'anomalie) et le convertir: Index = dB / 2.5, entre 0 et 10, donc 25 dB vaut 10. Une valeur négative (sous-fraction) est montrée comme 0 mais reste dans les données de chemin.

| Situation de la meilleure couche | Critères | Salaire estimatif |
| Normal (non stratifié) | −dN/dh dans la plage de tabulation. 6.5 (par exemple 20 à 60 N/km entre 1000 et 850 hPa) | 0 |
| Un peu au-dessus de la normale | entre la limite supérieure de la bande normale et le seuil de surfraction | 0 à 10 dB, en proportion |
| Super-réfraction | au-delà du seuil de couche (100, 80, 70, 50 N/km) mais moins de 157 N/km | de 10 à 15 dB, en proportion jusqu'à 157 N/km |
| Ligne trop fine pour bande | dM/dh < 0, épaisseur inférieure Seuil Kerr | 15 |
| Connecteur utile pour bande | dM/dh < 0, épaisseur au-dessus du seuil Kerr | 20 dB, +1 dB chacune des 5 M unités de déficit, maximum 25 |
| Sous-fraction | −dN/dh < 0 dans la couche inférieure | −10 dB |
| Fond anomal (vous ajoutez) | Modèle Ns moins carte de l'UIT climatologique Ns pour ce mois | 0,2 dB par unité N, entre −5 et +5 dB |

Calcul par itinéraire (entre deux localisateurs): échantillonner le cercle maximum tous les 10 km, comme le fait déjà le programme pour l'indice sur le chemin, et calculer la fraction de points avec canal utile et la part moyenne de la base. L'étude CSIRO a trouvé utile la fraction de la route dans le pipeline (sur les routes AIS à 162 MHz était 67 % plus élevée que dans les cas de contrôle).

Ajouter le calcul par itinéraire (à partir de LA0BY et W3SZ): (1) les stations maritimes par rapport à la base et au sommet du conduit, parce qu'un conduit haut ne sert qu'à ceux qui entrent; (2) un troposcatter « sol » avec la méthode Yeh, avec les angles de départ tirés du profil du sol; le gain de trompe en dB est retiré de cette perte et le résultat est comparé à la perte que la station parvient à surmonter (tableau KK6MC). Ainsi, l'indice devient aussi une estimation de la distance accessible.

Corrections à essayer une à la fois (de Bean et Dutton et LA0BY, IK3XTV, G4MCU déjà dans le manuel), à ne retenir que si elles améliorent la comparaison avec les spots:

- Mélange (important selon l'auteur, par observation quotidienne): jour sur terre, entre mi-matin et après-midi avec ciel clair, ou avec le vent au sol sur quelques m/s, les conduits au sol disparaissent; leur contribution doit être réduite. Pas en mer.
- Heure de la journée (importante selon l'auteur, par observation quotidienne): les canaux irrationnels naissent au coucher du soleil et tombent après le lever du soleil; c'est un contrôle de prévision de 3 heures.
- Mouvement vertical: l'air monte à 850–700 hPa remixe les couches (correction négative), l'air descendant sous un anticyclone les renforce (positif).
- Tendance de la pression: pression de montée sur la trajectoire dans les dernières heures, signal plus élevé et stable; en diminution, signal plus bas (bastano 2 hPa).
- Synonyme: haute pression au-dessus de 1025 hPa, température à 850 hPa beaucoup plus haut que sur le sol, air chaud et sec qui coule sur la mer plus froide, route par mer.
- Chemins de montagne: un deuxième indice "convectif" (section IK3XTV), avec une couche limite de l'après-midi supérieure aux crêtes le long du chemin et à haute pression.

À partir de VHF/UHF DX Livre, autres corrections à essayer: jours consécutifs de haute pression sur le point (plus de jours, plus de points), position par rapport au centre de l'anticyclone (bonus aux bords), gradient de pression sur le sol comme mesure du vent, forte pluie dans les 24 heures précédentes (pénalité), nuages bas au bord de la couche (pénalité seulement pour les bandes élevées). Pour la direction des itinéraires: comparez l'azimut de la route avec celui de l'isobare.

Vérification

- archive LIVETROPOVHF: chaque commercial de plus de 500 km à 144 ou 432 MHz est un "événement". Il compare l'indice sur la route avec la fréquence des événements, au même moment de jour et de mois (la même approche de l'étude CSIRO).
- Comparaison avec Hepburn et F5LEN les mêmes jours: où les trois index se déconnectent, vous regardez qui était juste selon les spots.
- Radiosundations de 00 et 12 UTC: pour vérifier si le modèle voit les inversions que la sonde mesure.
- Cas historiques de LA0BY (6-11/11/2003, 10/12/2004, 15/10/2011, 11/2/2015, 22/11/2018): l'indice recalculé à ces jours doit voir les conduits que Hepburn n'a pas montrés.
- Limites à déclarer dans le manuel: effectuées de moins de 100 à 200 m presque invisibles aux niveaux de pression GFS; prédiction fiable pendant 2 à 3 jours, puis seulement tendance.

## 15. Sources du 21 novembre (deuxième partie)

- UIT-R P.1812 (MATLAB/Octave mise en œuvre de I. Stevanovic, OFCOM; GitHub eeveetza/p1812, Zenodo 10.5281/zenodo.6141558). Méthode VHF/UHF spécifique à la zone de l'itinéraire, dérivée de P.452: diffraction "delta-Bullington", troposcatter, conduits et réflexion sur les couches, encombrement au sol. Utile comme troisième comparaison à côté de P.452 dans le calcul point-point; les données ΔN et N0 sont des produits complets de l'UIT, de sorte que l'utilisateur les télécharge (selon P.453).
- de Costa, Rabanal Ramirez, Dias, IET MAP 2018 (doi 10.1049/iet-map.2017.0992). 563 MHz, 27 points entre 5 et 30 km sur terrain irrégulier avec forêt: P.526 avec 12 m de végétation −4,3 dB et φ 8,8 dB; P.1812 avec 10 m de charges +2,6 dB et φ 8,9 dB; coin diélectrique −4,8 dB et φ 9,2 dB; P.1546 +5,0 dB et φ 11,7 dB. Pour le programme: l'erreur résiduelle d'une bonne méthode de diffraction est d'environ 9 dB, et la hauteur des charges sur les crêtes (10-12 m) compte.
- PETOOL (Ozgun e altri, Communications en physique informatique 182, 2011). équation parabolique scindé en une et deux voies avec propagateur grand angle (angles jusqu'à 40–45°, contre 10–15° de l'étroit); fond avec des pertes à travers le Fourier transformé mixte; sol en marches. Confirme les choix de 1.3.2 (angle large, coefficients de sol) et indique le calcul bidirectionnel pour les reliefs abrupts.
- MathWorks. `tropopl`: perte de gaz et perte de «lente» dans une atmosphère stratifiée (P.835, P.676, P.453), sans conduits: avec des gradients inférieurs à 157 N/km, le modèle optique géométrique ne s'applique pas. L'exemple de propagation radar utilise pour la réfraction −ΔN = 7,32·e^(0,00557·Ns), le même rapport CRPL déjà dans le programme. `propagationModel` Longley-Rice: valeurs de polarisation horizontale prédéfinies, conductivité 0,005 S/m, permistivité 15, réactivité 301, climat continental tempéré. Aucun modèle MathWorks ne traite des conduits.
- UIT-R, produits numériques du groupe 3. Les données et programmes de P.453, P.452, P.1812, P.2001, P.528, P.1546; les données 'intégrales' font partie des recommandations; aucune licence de redistribution déclarée: vous continuez à les laisser télécharger à l'utilisateur.
- ESA Navipedia, retard troposphérique. N = Nidrostatica + Numida; le retard zénital sec est d'environ 2,3 m et l'humidité d'une dizaine de cm, qui varie rapidement. Pour nous: le délai humide estimé par les stations GNSS est une mesure indirecte de l'humidité intégrée, source future possible de la diffusion actuelle; Aujourd'hui n'est pas utilisable.
- Bray, K2LMG, «A Method for Determining V.H.F. Station Capabilities», QST novembre 1961, pp. 36–40 (lire dans l'original; revérifié le 22 novembre sur le PDF du nombre de QST: toutes les valeurs ci-dessous correspondent). L'auteur, D. W. Bray (General Electric, Ithaque), dit avoir vérifié les prévisions avec des mois de connexions programmées avec K2GOI et W4LTU. Evanescence dans le temps de 13,5 dB sur environ 100 milles (7 dB en dessous et 7 au-dessus de la moyenne horaire), presque linéaire en dessous (3,5 dB à 50 milles): pour une connexion solide enlever 7 dB. Correction de l'horizon: chaque élévation de l'horizon vaut 69 milles (111 km) de distance supplémentaire (moins si négatif). Gain d'un yagi d'environ 10 × boom/λ (rapport puissance), plus environ 4 dB de réflexion du sol; SSB -3 dB et AM -7 dB par rapport à CW; 99% des heures: environ 180 dB à 60 miles, 190 à 90, 200 à 240 (2 m). A partir de 1.3.2, la probabilité pour la période utilise la méthode ε = 3,4 dB (±7 dB en ±2τ) au lieu de la méthode d'évaluation de 5 dB.
- Calcul bidirectionnel (essai avec prototype, 21 novembre). équation parabolique grand angle avec terrain réel S RTM1 de Capri, 144 MHz, antenne à 10 m du sol, atmosphère standard; mesure: puissance moyenne entre 20 et 400 m à 80 km sur la route à celle correspondante. Du centre de Capri et de Marina Grande à l'ouest est fermé (obstacle au +11-21°, Monte Solaro): pointant le Faito (presque derrière EA5 et EA6, 150-160° hors axe) le signal lancé vers l'Espagne monte 10-29 dB; pointant le Vésuve (110-125° hors axe, puis hors du plan de calcul, seulement indicatif) de 17-25 dB. De Anacapri l'ouest est libre et les victoires directes de 8–22 dB. Cependant, les pertes absolues sur 1000–1300 km du prototype ne sont pas fiables. EA3 EA5 EA6
- Barsis et Kirby, NBS Report 6751 (1961), Pikes Peak, 223 km, 100 et 751 MHz. La montagne est un terminal commun de deux routes en optique: la distribution des médianes horaires est la convolution de celles des deux routes. Excursion à long terme (1-99%) d'environ 14 dB, contre 26-40 dB d'un sentier de troposcatter d'égale longueur ou angle; aucun changement de jour appréciable. Le signal est plus fort que le troposcatter, mais plus faible que le bord idéal: le sommet doit être traité comme arrondi (environ 11,5 km). Les signaux élevés sont toujours observés derrière les dorsales.
- Barsis, NBS Report 7652 (1963), trois routes au-dessus d'une montagne (Colorado, Alaska, Europe), 750–900 MHz. Perte moyenne de 10 à 20 dB supérieure au calcul de Fresnel-Kirchhoff sans terre près des bornes; l'évanescence est estimée en enveloppant les deux lignes en optique; les reflets du sol près des bornes aggravent le fade prolongé. À partir de 1.3.2, l'entrée dans le conduit ajoute l'arrondi de P.526.
- Barsis et Johnson, NBS AMT 88 (1961). Dissolution prolongée de la vague spatiale, même sur un sentier au-delà d'une montagne (§ 5).
- Norton, « Perte du système de propagation des ondes radio », J. Res. NBS 63D (1959). Les courbes à partir desquelles Bray obtient les nomogrammes; définitions de perte du système, transmission et base, gain d'antenne de processus.
- Riz, Longley, Norton et Barsis, NBS AMT 101 (1965-1967), volumes 1 et 2. simple bord et obstacle arrondi avec et sans réflexions (§ 7, avec l'exemple de Pikes Peak), variabilité à long terme V(0,5) et Y(q) pour les climats (concontinentaux tempérés et autres). Pour nous: la méthode des fonctions arrondies d'obstacle et de variabilité est la référence à l'incertitude de calibrage.
- UIT-R P.526-16 (11/2025). Formule (31) de J(ν), identique à celle du programme; Obstacle arrondi (32)–(36), maintenant utilisé; méthodes à partage multiple (Bullington et méthode complète § 4.5).
- UIT-R P.1812-8 (2025) et -7. Texte complet récupéré: méthode spécifique de 30 MHz à 6 GHz (delta-Bullington, troposcatter, conduits et réflexion sur les couches, ingombs). Le -7 avertit que son troposcatter (de P.617-5) est mal situé sous 66 km. Il reste une troisième comparaison possible.
- Paulus, «Application du modèle d'évaporation de l'IREPS», NOSC TR 966 (1984) et Radio Science 20 (1985). Rapport lu le 22/11/2026 (DTIC ADA146528, scan avec du texte reconnu, parfois illisible). Les différences air-mer mesurées par les navires sont décalées vers la stabilité: l'air marin plus chaud dans 31 à 35 % des données des navires contre 0,7 à 7 % des sangliers NOAA, pour les psicromètres perturbés par les navires et les SST prélevés sur l'eau de refroidissement (environ +1 °C). Le modèle Jeske donne alors 40 m et plus de 15% de l'année et des hauteurs qui sautent de 10 à 40 m en 24 heures avec un temps stable, non confirmé par des mesures radio. Correction proposée (seuils testés 0, −0,5 et −1 °C; la valeur de −1 °C est mieux en accord avec les mesures radio de juin 1982): si la différence air-mer est de −1 °C ou moins, on utilise la valeur mesurée; sinon, on calcule la hauteur avec −1 °C et avec 0 °C et, si celle avec −1 °C est inférieure, on tient cela. Les augmentations du côté stable sont coupées, des diminutions subsistent. En juin, Marsden 120 carré: hauteur moyenne de 16,8 m (climatologie) à 7,6 m. Les équations du modèle ne figurent pas dans le rapport (voir Jeske et Hitney). Le programme utilise le profil neutre, qui n'a pas d'exagération stable, et limite à 40 m: il va dans la même direction; avec les SST des modèles (pas de bateau) la faute de mesure que Paulus corrige moins pèse.
- Levy, «Méthodes d'équation parabolique pour la propagation des ondes électromagnétiques» (IEE 2000). Sur Google Books il n'y a qu'un aperçu: Je ne lis pas.
- VK3FS (3fs.net.au). Introduction à la qualité: conduits hauts de 450 à 2000 m pour des ouvertures plus longues au sol; inversions de 300 à 600 m en nuits calmes et calmes, qui disparaissent après le lever du soleil; inversions côtières plus fortes en après-midi; 800 à 1300 km typiques, 1400 à 1600 km fréquemment conduites. Conformément aux alertes jour/nuit et au plafond de 2500 m.

## 16. Sources

Règles et formules

- [UIT-R P.453-13 (2017), indice de réfraction radio: formule et données](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)
- [UIT-R P.452-18 (2023), prédiction du brouillage entre stations au sol sur 100 MHz: rayon équivalent, β0, troposcatter, canaux](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)
- [UIT-R P.617-5, propagation au-delà de l'horizon pour les ponts radio](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)
- [Shi et al., "Une étude approfondie sur la longueur maximale d'onde de propagation électromagnétique dans différents conduits d'évaporation", IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

Etudes sur la prédiction des canaux avec modèles météorologiques

- [P. Lopez, "Une climatologie mondiale de résolution de 40 km sur 5 ans pour les radars de météorologie au sol", ECMWF Tech. Mémoire 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO), "Prédiction de la dérivation troposphérique basée sur les données du modèle GFS à... la radioastronomie CSIRO Murchison", Observatoire arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (MFI), "Effets de la résolution du modèle sur la prédiction des conditions de canalisation atmosphérique avec Harmonie-AROME", EGUsphere 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

Prévisions radio amateur

- [W. R. Hepburn, "The Hepburn Tropo Index (HTI)"](https://www.dxinfocentre.com/propagation/hti.htm)et [](https://www.dxinfocentre.com/propagation/history.htm)method historique
- [W. R. Hepburn, "Lowest Usable Frequency vs. Thickness of Ducting Layer"](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, « Prévisions mondiales de l'indice de réfraction »](https://tropo.f5len.org/WW/info.php), [nouvelle version 2026](https://tropo.f5len.org/?p=504), [note sur le modèle ARPEGE](https://tropo.f5len.org/author/f5len/)]

Données

- [Open-Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api), [note sur les niveaux de pression](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)
- [NOAA RCEI, Archives intégrées de radiosonde mondiale (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RAOBget, script d'inondation radio pour l'Université du Wyoming](https://github.com/NCAR/RAOBget)

Textes classiques (indiqués par l'auteur du programme, à consulter pour les formules finales)

- B. R. Bean, E. J. Dutton, *Radio Météorologie*, NBS Monographie 92, 1966, 435 pages (réimpression Dover 1968): profils verticaux de N et M, gradient de piège sous −157 N/km, comparaison avec les ondes radio; chapitre 5 "Synoptique radio météorologie". Base pour 2–4 étapes de calcul par point. Gratuit: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [carte NIST, doi 10.6028/NBS.MONO.92](https://www.nist.gov/node/1770911), [Archives Internet](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf)copie.
- L. Boithias, *Radio Wave Propagation*, McGraw-Hill 1987 (traduit par D. Beeson en français, 330 pages, ISBN 0070064334; aussi North Oxford Academic): mécanismes au-delà de l'horizon, volume commun de diffusion et perte d'antenne-mezzo de couplage. Papier seulement ([carte de bibliothèque](https://libcat.library.tamu.edu/Record/in00000014812/Description)).
- M. P. M. Hall, *Effets de la troposphère sur la radiocommunication*, Série des ondes électromagnétiques IEE n. 8, Peter Peregrinus 1979, 206 pages (ISBN 0906048257; brossura 0863410863): situations synoptiques et propagation VHF/UHF. Base pour les ingrédients synoptiques. Échangez avec IET ([](https://libcat.library.tamu.edu/Record/in00000636385)card).
- H. G. Booker, W. E. Gordon, "A Theory of Radio Scattering in the Troposphère", *Procédures de l'IRE*, vol. 38, n. 4, avril 1950, p. 401-412: La théorie de l'échographie à partir de la dégénérescence de l'indice de réfraction. Idée centrale: la puissance diffuse dépend de l'intensité et de l'échelle des fluctuations turbulentes de l'indice de réfraction (corrélation exponentielle); près de l'horizon prédomine la diffraction, la diffusion ultérieure; Selon les auteurs, l'échelle de turbulence et les variations d'indice attendues par la météorologie sont suffisantes pour expliquer les champs mesurés. Cité par 328 articles (Scilit), pas de copie gratuite. Carte sur le boursier sémantique ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); texte complet sur l'IEEE Xplore payé ([doi 10.109/JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)).
- C. M. Crain, J. R. Gerhardt (Electrical Engineering Research Laboratory, University of Texas), « Mesures des paramètres impliqués dans la théorie de la diffusion de la radio dans la troposphère », *Procédures de l'IRE*, vol. 40, n. 1, janvier 1952, p. 50 à 54, doi 10.109/JRPROC.1952.2739 donnent les valeurs réelles que Booker et Gordon n'avaient estimées que ([onglet](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)de Skilit]).
- "The Constances in the Equation for Atmospheric Refractive Index at Radio Frequencies", *Procédures de l'IRE*, vol. 41, n. 8, août 1953, p. 1035-1037: propose de revoir les courants alors utilisés (79 K/mb et 4800 K) dans la formule N; c'est d'ici que les valeurs 77,6 et 3,73 · 10^5 de la formule UIT ([Q](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E. C. S. Megaw, "Fundamental radio scatter property theory", *Procédures de l'IEE Partie C: Monographies*, vol. 104, n. 6, septembre 1957, pp. 441–455, doi 10.1049/pi-c.](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)
- *Procédures de l'IRE*, octobre 1955, numéro spécial sur la propagation de la dispersion (par K. A. Norton et J. B. Wiesner), avec Bullington, Staras et d'autres: gratuit en PDF sur [World Radio History](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf).
- D. E. Kerr (édité par), *Propagation de ondes radio courtes*, MIT Radiation Laboratory Series vol. 13, McGraw-Hill 1951 (Restampe Dover 1965 et IEE 1987): théorie de l'évaporation et des conduits de surface sur la mer. Copie numérique: [Histoire radio mondiale](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); édition IET dans [IET Digital Library](https://shop.theiet.org/propag-short-radio-waves).

Mesures récentes de diffusion troposphérique dans le VHF: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Australie), "A Study of VHF Troposcatter Communication Channels in South Australia", dans *Digital Signal Processing for Communication Systems*, Springer 1997, p. 103 à 112 ([Springer](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)]. Texte pour frais; de abstrait il concerne la diffusion, pas les conduits. Utile seulement si vous voulez calibrer le terme de troposcatter avec les données de canal VHF.

Modèles numériques et itinéraires maritimes (indiqués par l'auteur du programme)

- M. Levy, *Méthodes d'équation parabolique pour la propagation des ondes électromagnétiques*, série IEE des ondes électromagnétiques #45, 2000, 336 pages (ISBN 0852967640): équation parabolique pour les profils réfractaires qui changent le long du chemin et pour l'orographie; chapitres sur la propagation troposphérique, les rayons et les voies, la propagation en mer. Il servirait à un calcul de champ le long du chemin, au-delà de l'index. Pour le paiement, également sous forme électronique à partir de IET ([IET card](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)).
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (Université de Leicester): 2 GHz sur les îles Channel et la Manche. De nombreux travaux et thèses sont gratuits dans les archives universitaires: [statistiques à long terme sur le canal d'évaporation, Radio Science 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434), [changements de signal sur trois routes maritimes, Radio Science 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710), [Titres de N. Mufti 2012 avec des routes de 50 et 140 km également en VHF](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854), [tesis de I. Alam 2002 sur la réactivité calculée à partir des données météorologiques, 240 MHz et 2 GHz X3 UQ, [tesi..](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus, "Application pratique d'un modèle d'évaporation", Radio Science vol. 20 n. 4, 1985, p. 887 (pas les transactions IEEE sur les antennes et la propagation), doi 10.1029/RS020i004p00887 ([scheda](https://zendy.io/title/10.1029/rs020i004p00887)]: hauteur du conduit d'évaporation à partir des données météorologiques de surface. Libérez le rapport technique qui le précède: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf).

T. Lindquist, *Modèles de propagation Wave dans la Troposphère pour les connexions radio UHF/SHF à longue distance*, thèse de maîtrise, Université de Karlstad, 2 juin 2020, 106 pages (fournies par l'auteur du programme). C'est un guide étape par étape, libre, pour écrire un calcul avec l'équation parabolique: solution en étape fractionnée (même à grands angles), conditions limites pour la terre et la mer, sol à pas, suivi de faisceau sur des profils linéaires parfois, avec des vérifications numériques; cite l'outil libre PETOOL pour MATLAB. Deux données utiles à la fois: (1) Sensibilité N aux variables météorologiques, γN 0.26 γP + 4,3 γe − 1,4 γT (hPa et K, vers 20 °C), c'est-à-dire une erreur de 1 hPa sur la pression de vapeur pèse trois fois une erreur de 1 °C sur la température: est l'humidité du modèle qui doit être vérifié en premier; (2) Hauteur moyenne du conduit d'évaporation: 5–6 m en mer du Nord, 13–14 m en Méditerranée, plus de 20 m dans le golfe Persique, toujours trop basse pour 144 et 432 MHz. Conclusion de l'auteur (fréquences 1 à 10 GHz): au-delà de l'horizon, les connexions fiables nécessitent presque toujours un conduit.

Logiciel de propagation sur le profil du sol (pour l'horizon et les obstacles, pas pour le tropo attendu)

- [Radio Mobile](https://ve2dbe.com/rme.html)de Roger Coudé VE2DBE: gratuit, Windows (aussi Linux et Mac avec Wine), modèle Longley-Rice/ITM, profil terrestre extrait automatiquement des données de quota ([télécharger](https://ve2dbe.com/download/download.html)).
- C'est quoi?](https://qsl.net/kd2bd/splat.html)de John Magliacane KD2BD: GPL, 20 MHz-20 GHz, Longley-Rice et ITWOM v3.0 modèles, rapports d'obstacles et de pertes en texte; dernière version 1.4.2 de 2014.
- [IMT, NTIA/ITS](https://its.ntia.gov/software/itm)code officiel: le modèle Longley-Rice (20 MHz-20 GHz) avec source et documentation, base de Radio Mobile et SPLAT!. Le code est libre et vous pouvez ajouter au calcul sur le chemin de LIVETROPOVHF (perte due au sol, à ajouter à l'indice de tropo). Rapport original: A. G. Longley, P. L. Rice, "Prediction of Tropospheric Radio Transmission Loss Over Irregular Terrain: A Computer Method – 1968", rapport technique ESSA ERL 79-ITS 67, juillet 1968 (carte NTIA sur les logiciels historiques: X1 https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [UIT-R, logiciels et données numériques](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): cartes libres des médianes annuelles ΔN et N0 sur la grille 1,5° utilisée par P.452 et P.617 — utiles comme valeurs climatiques de référence pour l'indice.
- Pathloss (Contract Telecommunication Engineering): programme commercial pour les ponts radio; Je n'ai pas trouvé de source publique qui confirme quelles recommandations de l'UIT met en œuvre, afin de vérifier.

Code GitHub réutilisable dans LIVETROPOVHF

| Projet | Que faites-vous? | Langue | Utilisation possible pour nous |
| [NTIA/IMT](https://github.com/NTIA/itm) | modèle Longley-Rice officiel de NTIA, avec des paquets pour. NET |  C++, C#  | perte de terres sur le chemin: s'intègre directement au programme, qui est en C# |
| [Fuzzlee/itm](https://github.com/fuzzzeee/itm) | ITM C++ code C# traduction |  C#  | alternative sans bibliothèques natives, également utile pour la version Windows 7 |
| [eeveetza/Py452](https://github.com/eeveetza/py452) | UIT-R P.452, identique à la version MATLAB/Octave approuvée par l'UIT-R WP 3M; version MATLAB publiée par l'Office fédéral suisse des communications | Python, MATLAB | référence à traduire en C# les formules de troposcatter et réalisées avec des exemples de validation de l'UIT |
| [bwinkel/pycraf](https://github.com/bwinkel/pycraf)] | P.452 complet, P.676, profils SRTM; GPL v3, mais les fichiers de données de l'UIT inclus ne sont pas gratuits pour une utilisation commerciale | ♪ | contrôle de nos résultats |
| [UIT-Rpy](https://github.com/inigodelportillo/ITU-Rpy) | de nombreuses recommandations de l'UIT-R, y compris P.453-13, validées avec des exemples de l'UIT | ♪ | vérification de la formule N et des cartes climatiques de P.453 |
| [NCAR/RAOBget](https://github.com/NCAR/RAOBget) | télécharger l'onde radio de l'Université du Wyoming en format texte | ♪ | diagramme à télécharger en C# les profils des stations italiennes |

Note: la version actuelle de P.453 est la [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)]

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), téléchargeable gratuitement; dans le document j'ai utilisé le 13 pour être reallined.

Déjà dans les manuels du programme et réutilisable ici: LA0BY (Nordic VUSHF 2019), IK3XTV (« Propagation on 2 metres » et « Propagation VHF – cellules convectifs et ondes gravitationnelles », Radio Magazine, fourni par l'auteur du programme), DF5AI/DK5YA (2003), G3YGF (QST 1983), La capsule VHF/UHF DX Book. 2, G4MCU (2003) KP4MD
