LIVETROPOVHF · document technique 2
# Construction de la carte

> Traduction automatique de l'italien : en cas de doute, le guide italien fait foi.

Comme la carte de l'onglet Carte et spot est née: la base géographique, les projections, la moyenne des indices Hepburn et F5LEN, l'isobare, les spots et les stations qui écoutent, chevauchent les couches. Version du 9 novembre 2026.

## En résumé

- La carte est conçue par le programme en huit couches, chacune avec sa boîte [8].
- Base: Côte naturelle de la Terre et frontières à deux niveaux de détail [9]; projections [10] Market et Azimuth.
- Physique: inversions, réactivité N et gradient dN/dh, seuil de conduite à −157 N/km [3] [4] [7] [11].
- Indice Trumpet: lecture calibrée des cartes Hepburn [1] et F5LEN [2], grille commune et moyenne des deux sources.
- Isobare: pression GFS [12] via Open-Meteo [15], lignes toutes les 4 hPa, H et L centres.
- Spot, stations d'écoute (cercles rose foncé avec des lignes rose toujours visibles) et décodages personnels en rouge.
- Lecture du chevauchement: haute pression, gaines élevées, chute de pression, croissance du SNR, [3] [4] [5] [13] [14] routes de montagne.

## 1. Le principe: une carte stratifiée

La carte n'est pas une image fixe: elle est conçue par le programme, couche sur couche, chaque fois que vous changez de zoom, de projection, de bande, de temps de prévision ou de thème. Chaque couche a sa boîte dans la barre de calques supérieure sur la carte, de sorte que vous ne pouvez tenir ce dont vous avez besoin et voir tout ensemble lorsque vous voulez l'image complète. L'ordre, d'en bas, est celui-ci:

|  #  | Route | Contenu | Casella |
|  1  | Carte de base | Mer, terres, côtes et frontières | Oui |
|  2  | Tropo index | Media Hepburn + F5LEN, ou une seule source; transparence réglable | Oui |
|  3  | Localisateur de grille | Champs, carrés et, avec zoom, carré | Oui |
|  4  | Isobare | Lignes de pression toutes les 4 hPa, centres H et L | Oui |
|  4a  | Température 850 hPa | Isothermie traitée chaque 4 °C | Oui |
|  4b  | Inversion thermique | Échantillon orange où l'air à l'altitude est plus chaud que sur le sol | Oui |
|  5  | PSKReporter | Stations de réception de PSKReporter (cercles roses foncés) et, en rose, ce qu'ils entendent | Oui |
|  6  | Décoré par moi | Stations de décodage avec WSJT-X, JTDX ou MSHV: lignes rouges de votre QTH | Oui |
|  6a  | QSO desunti dal en direct | Liaisons fermées ( RR73, 73, RRR) lire dans vos décodages: ligne verte entre les deux stations | Oui |
|  7  | Pleins feux | Connexions: ligne bleue complète, couverture jaune pour la diffusion d'air, deux boules | Oui |
|  7a  | Exclusions | Casella Exclusion de moins de 100 km: le trafic le plus court n'est ni tiré ni énuméré. Casella Plus de 1000 km: seulement carrés (départ): si l'une des stations est à plus de 1000 km de votre QTH, pas de lignes ou de cercles, seulement la localisation des places des stations impliquées dans le lilas tenue à 45° | Oui |
|  8  | QTH et étiquettes | Votre station, les noms de l'endroit sélectionné | toujours visible |

L'indice de tropo est immédiatement au-dessus de la base et au-dessous de tout: est un échantillonnage coloré, et placé plus haut couvrirait les lignes et les symboles. La grille passe dessus, donc vous savez toujours où tombe une zone colorée. Les isobares sont des lignes minces et sont bien lus sur l'échantillonnage.

## 2. La carte de base

Les coûts, les limites et les terres proviennent des données de vecteurs naturels de la Terre, du domaine public [9], à l'échelle 1:50 millions pour la vue globale et 1:10 millions quand agrandi. Les fichiers sont dans le dossier du programme: aucun service de carte en ligne. Couleurs du thème Clair: mer bleu clair # E2ECF8, côte bleu foncé, bordures gris clair. Dans les thèmes Noir et Nuit les couleurs de la base sont obscurcies, dans les autres thèmes ils teintent légèrement.

## 3. Les deux projections

Toutes les couches passent par la même fonction de projection, de sorte qu'elles sont toujours alignées entre elles. Vous choisissez entre deux projections [10] (λ longitude, λ latitude, R rayon de la Terre):

Marché. Entreposer les coins et formes locaux; Les distances s'étendent vers les pôles. Couper à ±85° de latitude.

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

Azimutale équidtanti centré sur votre place. Le centre (λ0, λ0) est le centre de votre place WWL. Les distances et les directions mesurées depuis le centre sont vraies: une station à 1200 km est à 1200 km d'échelle, et l'angle avec le nord est l'azimut vers lequel pointer l'antenne. Lorsque vous changez de localisateur, la carte est recentrée.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

Dans l'azimut les lignes qui sur le papier sont droites (méridiens, parallèles, bords carrés) deviennent courbées. Pour cette raison, toutes les lignes sont divisées en sections courtes, chaque section est projetée et ensuite les traits sont unis; il en va de même pour les bords de l'indice tropo et pour l'isobare. Le point antipode au centre n'a pas de position précise et reste hors de vue.

## 4. Localisateur de grille, zoom et déplacement

La grille Maidenhead est conçue par le programme: des champs de 20° × 10° avec les deux grandes lettres fanées au centre, des carrés de 2° × 1er, et avec suffisamment de subquadrati zoom de 5′ × 2,5′. Les lignes sont bleues et minces, pas pour couvrir l'indice de tropo.

Zoom avec la roue centrée sur le pointeur (le point au-dessous de la souris reste stable), mouvement de glisser, double clic pour revenir à l'ensemble de la vue, + et − touches, et la touche avec le pourcentage pour l'ensemble de la vue. Détails adaptés au zoom: côtes plus fines, sous-carrés, étiquettes isobares plus épaisses.

## 5. Pourquoi les inversions plient les vagues

Dans la basse atmosphère, la température diminue normalement d'environ 6,5 °C par km; L'indice de réfraction dépend à la fois de la température et de l'humidité [3], et où il change fortement avec l'altitude, les vagues se plient au sol et arrivent plus loin que la normale. La couche la plus importante est que jusqu'à environ 3 km, où les inversions, canaux et cellules convectifs [4] sont formés; les chemins sur la mer sont favorisés parce que les masses d'eau forment de grandes zones d'inversion [4]. [3]

La taille utilisée pour la mesurer est la N réfractaire, c'est-à-dire (n - 1) · 106 [11]:

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

T est la température en kelvin, P la pression et la pression de vapeur, tous deux en hPa.

Compte comme N varié avec part (dN/dh). Dans l'atmosphère standard, la réfraction est d'environ −39 N/km; jusqu'à −79 N/km, la réfraction est considérée comme normale, en plus de la superréfraction, et avec des gradients positifs de sous-réfraction [7]. Sous -157 N/km, la vague est plus courbée que la courbure de la terre et reste piégée: c'est le conduit [11] (ducting). Ce sont les conditions que les cartes Hepburn et F5LEN tentent de prédire.

## 6. L'indice du tropo: de deux cartes à la moyenne

Les onglets Hepburn et F5LEN sont le bureau de contrôle: montrez les images téléchargées à partir de la page de chaque source, avec le temps de prévision variable, et servez à calibrer la lecture en cliquant sur les points de référence et les couleurs de la légende. La couche d'indice du tropo reste vide jusqu'à ce qu'au moins une des deux calibrations ne soit pas active et complète.

Hepburn. L'indice Hepburn mesure la quantité d'ondes qui devraient être repliées sur une zone, sur une échelle linéaire de 0 à 10 qui peut également dépasser 10. 0 signifie atmosphère standard et seulement disperser; 1–2 super-fraction marginale ou faible; 3–4 super-fraction avec des zones de conduit; L'index ne prend pas en compte le tropo dispersif et ne voit pas les montagnes [1]. Les cartes couvrent 6 jours: passes de 3 heures pour les 36 premières heures, puis 6 heures, mis à jour tous les jours à 18 UTC [1]; les données de départ (température, humidité, pression) proviennent du modèle global du service météorologique canadien [1]. [1]

F5LEN. Pour Europe F5LEN publie 31 cartes de +3 à +192 heures (toutes les 3 heures jusqu'à +48, toutes les 6 jusqu'à +96, puis toutes les 12), avec la date et l'heure valables dans le titre, le temps de production en bas à droite et une légende de classe: Perturb, Normal, Mineur, Faible, Bon, Fort, Vy Strong, Duct [2]. La prédiction mondiale de F5LEN est calculée par l'indice de réfraction de l'air dans l'unité N, sur 40 étapes de 3 heures (environ 5 jours), mis à jour 4 fois par jour; un clic sur la carte montre la valeur moyenne et la localisation de ce point [2].

Ça marche. Pour chaque étape de prédiction, le programme effectue ces étapes:

1. Décharge. Les images des deux prévisions sont téléchargées dans le cache à côté de l'exécutable, une fois pour mettre à jour la source, pas à une refonte. Avant la version finale, la permission doit être demandée à Hepburn et F5LEN.
2. Géoréférence automatique. Le programme trouve le cadre (deux lignes horizontales et deux lignes verticales de la même couleur) et, sur Windows 10/11, lit avec l'OCR intégré dans le système les étiquettes d'essieu (54N, 10W, 0, 5E...). Coupez les bandes autour du cadre, agrandissez-les, testez les deux signes lorsque la lettre N/S/E/W ne lit pas et gardez la ligne sur laquelle tombent le plus d'étiquettes: les mauvaises lectures restent en dehors. De là vous pouvez obtenir l'extrême lat/lon du cadre et le type de projection (grille régulière ou Mercator). Les extrêmes se sauvent et se relisent si le cadre change de taille. Sur Mac et Linux, sans OCR, les extrêmes d'usine ou ceux manuscrits sont valides dans l'onglet. Alternativement, pour les cartes n'ayant pas d'axes lisibles: Chaque type de carte (Hepburn Europa, F5LEN dans le monde...) a un calibrage: certains points de contrôle avec des coordonnées connues (croisements de méridiens et parallèles, côtes reconnaissables) relient pixels à latitude et longitude, dans la projection utilisée par cette source. L'étalonnage est dans un fichier à côté du programme et vous pouvez refaire si une source change de format.
3. Lecture des couleurs. Chaque pixel est comparé à la légende source et converti en la valeur numérique la plus proche. Les coûts, les écritures et les grilles tracées sur la carte originale sont reconnus comme des données non connues et remplies des valeurs voisines.
4. Grille mondiale. Chaque image, une fois que vous trouvez le cadre et remarquez ses extrêmes, est placée sur Terre; la valeur à tout moment du monde est dérivée des images qui la couvrent. Si plus de régions couvrent le même point il vaut la peine de détail (plus de pixels par degré), tant que son temps est à moins de 1,5 h de ce choix; puis l'heure la plus proche, puis la course la plus récente. Lorsqu'aucune image ne couvre le point, la carte montre un voile gris: aucune donnée, qui est différente de l'index 0. A l'intérieur du cadre, un pixel sans légende est 0 (atmosphère normale); les côtes, les bordures et les écritures sont sautées en regardant la couleur la plus proche à moins de 3 pixels.
5. Même échelle. Hepburn est déjà sur l'échelle 0–10 [1]: fond foncé = 0 et dix couleurs dans l'ordre (violet, bleu, vert d'eau, citron vert, jaune, ocre, orange, rouge, rose, rose clair) pour 1...10, lilas au-delà. F5LEN utilise des classes (Disturb, Normal, Mineur, Faible, Bon, Fort, Vy Strong, Duct) [2], mais certains ont exactement les couleurs Hepburn: Faible = 1, Fort = 5, Vy Fort = 7, Duct = 8; Mineur est 0,5 et Bon (vert) 3,5. Le multiplicateur d'échelle, dans l'onglet F5LEN, corrige le jeu pour le rendre comparable à Hepburn.
6. Même heure. La barre de temps va de +0 à +192 h à des pas de 3 heures. Pour chaque étape que vous prenez de chaque source la prévision valide au moment le plus proche; Si une source n'a pas cette étape, le média n'utilise que l'autre et la légende l'indique.
7. Les médias. Avec le bouton Media chaque point vaut (Hepburn + F5LEN) / 2; avec Hepburn ou F5LEN vous ne voyez qu'une seule source, utile pour comprendre où les deux prévisions sont en désaccord.
8. Dessin. La valeur est colorée avec l'échelle de légende (0 bleu, 2 vert, 4 jaune, 6 orange, 8 rouge, 10 violet) et projetée comme échantillon sous la grille. Le 0 est transparent, donc là où rien ne se passe vous voyez la carte de base propre; transparence générale s'ajuste avec le curseur à côté de la boîte.

Sur l'onglet spot, l'indice le long de la route est la moyenne des championnats de la grille tous les 10 km sur le cercle maximum entre les deux stations.

## 7. Isobare

Les pressions stables élevées sont le premier signal à être recherché pour le trope [3], et les connexions au-delà des Alpes analysées IK3XTV ont été à haute pression [4].

1. Données. Pression au niveau de la mer du modèle global GFS de NOAA [12], lu par le service Open-Meteo [15] gratuit sur une grille de 3° × 3° (de 30° à 72°) N, de 30° O à 48° E), pour les 78 heures suivantes. Le programme met à jour les données au démarrage puis toutes les 6 heures, comme le modèle de course.
2. Des lignes. L'isobare vient de la grille tous les 4 hPa (...1012, 1016, 1020...) avec l'algorithme des carrés de marche (carrés de marcing), puis ils sont légèrement arrondis et projetés par tract.
3. Centres. Les maximums et minimums locaux les plus marqués sont H (bleu) et L (rouge) et la valeur en hPa.
4. Des étiquettes. La valeur apparaît le long de chaque isobara, sur un fond de couleur de la carte pour rester lisible au-dessus de l'échantillonnage.

Zone et pas. Les données sont téléchargées sur la zone que vous recherchez, agrandies de 25%, avec un pas du 1er au 20° choisi pour rester en dessous de 400 points (Open-Meteo compte chaque point comme une demande: 600 par minute, 5000 par heure): en élargissant ou en déplaçant la carte hors de la zone déjà téléchargé le programme le rechargement le plus dense, au plus toutes les 10 minutes; si une mise à jour échoue les données précédentes restent et il réessayera après 3 minutes. De cette façon, les isobares et les températures fonctionnent partout dans le monde.

Températures. La même demande porte la température à 2 m, à 925 hPa et à 850 hPa. LA0BY montre que l'événement du 22/11/2018 à UA6 a été observé dans les cartes de température (900-3000 m), mais pas dans les prédictions de Hepburn [3]; DF5AI et DK5YA utilisent des profils de température verticaux pour reconnaître les inversions [5]. Pour cela il y a deux couches:

- Température 850 hPa: isoterme traiter toutes les 4 °C (rouge au-dessus de zéro, bleu au-dessous, violet sur zéro).
- Inversion thermique: échantillonnage orange où l'air à 925 ou 850 hPa est plus chaud que celui à 2 m; plus intense, plus l'inversion est forte. C'est la condition physique qui incline les ondes au sol [3] [11], et la comparer avec l'indice de tropo et isobare montre si les prévisions ont une base météorologique.

## 8. Points et stations

Spot. Chaque endroit avec les deux positions connues est une ligne sur le cercle maximum entre les deux stations, divisé en sections et projeté; Dans Mercatore il apparaît alors légèrement courbé, dans l'azimutale il est droit si il commence du centre. Ligne bleue pour la tache normale, jaune recouvert d'un bord foncé pour la diffusion d'air (<AS>, <ACS> ou "AirScatter" dans le commentaire), une boule pour chaque station, halo léger sous la ligne pour la détacher de l'échantillonnage. Le spot sélectionné est plus souvent conçu pour la dernière et avec les deux noms.

Écouter. Chaque PSKReporter La station de réception [16] est un cercle vide rose foncé. De chaque départ des lignes pleines roses vers les stations qui ont décodé et renvoyé sur le net, toujours visible sans cliquer, avec les noms toujours écrits. En sélectionnant un cercle, ses lignes sont plus épaisses. Spot et écoute montrent seulement la bande et la plage choisies dans la liste: changer la bande passante la carte est vide et redessiné avec les données déjà collectées pour la nouvelle bande.

Décidé par moi. Chaque station qui décode avec WSJT-X, JTDX ou MSHV (réception via UDP) est une ligne rouge pleine de votre QTH à la station, avec balle et nomination. C'est la comparaison la plus directe avec la prévision: montre où votre récepteur vient vraiment en ce moment.

## 8 bis. Couleurs et symboles

Chaque couleur n'a qu'une signification, égale dans tous les thèmes (changez seulement l'arrière-plan).

| Couleur et signe | Signification |
| Ligne bleue continue | Spot du cluster DX, RBN ou DM4X entre deux stations |
| Ligne jaune avec bord foncé | Spot avec commentaire de diffusion d'air (réflexion sur les aéronefs) |
| Cercle vide rose foncé | Station d'écoute sur PSKReporter (MQTT) |
| Des lignes roses fines du cercle, avec une boule rose en bas | Stations que l'auditeur entendait, avec le nom toujours écrit; lignes plus épaisses pour l'auditeur sélectionné |
| Ligne rouge et nom rouge | Vos décodages UDP (WSJT-X, JTDX, MSHV): qui êtes-vous |
| Clignotes de ligne rouge | QSO en cours avec cette station: Il vous a appelé et la fermeture n'est pas encore arrivée, ou est l'appel DX de WSJT-X/JTDX avec la transmission activée. Continuez avec RR73, RRR ou 73 |
| Ligne verte | QSO desunti dal en direct: fermeture RR73, RRR ou 73 UDP |
| Ligne noire avec bordure blanche | Quelqu'un vous appelle: il dure jusqu'à RR73/RRR /73 ou jusqu'à 3 minutes |
| Carré de 45° | Trafic avec au moins une station au-delà de la couronne choisie: Pas de lignes, seulement des carrés |
| Recherche de lillas avec distance au nord | 1000, 700 et 500 km autour de votre QTH; ce choix est plus marqué |
| Cercle et point dans la couleur du thème | Votre QTH, avec nom et propriétaire |
| Balle orange avec GRAVES 143050 | Le radar GRAVES ( JN27SI) |
| Échantillon du bleu au rouge | Indice du trope Hepburn (0–10) ou classe F5LEN, ou leur moyenne; couleur plus chaude, meilleures conditions |
| Velours gris clair | Zone sans données de tropo pour le temps choisi |
| Lignes gris-bleu avec la valeur en hPa | Isobérer tous les 4 hPa; H en haute pression bleue, L en basse pression rouge |
| Lignes pointillées rouges, bleues et violettes | Isothermie à 850 hPa tous les 4 °C: rouge au-dessus de zéro, bleu sous zéro, pourpre le zéro |
| Échantillon orange | Inversion thermique: air à 925 hPa plus chaud que sur le sol, plus intense avec une inversion plus forte |

Dans les rapports suspects le bord de chaque onglet dit la règle en vigueur: vert Bon, rouge Scarta, gris aucune règle.

Quels sont les carrés de lilas couverts à 45°? Le trafic éloigné de votre QTH vous intéresse à comprendre où la propagation est ouverte, mais ses lignes, des centaines de kilomètres de long et souvent des dizaines, couvrent la carte et cachent ce qui compte pour vous: les ouvertures que vous pouvez exploiter de votre station. Pour cette raison, au-delà de la couronne choisie, le programme ne dessine pas les lignes mais colore les carrés de localisation (2° × 1°) des stations concernées. Le résultat est une carte des «zones actives»: où apparaissent de nombreux carrés de lilas proches, il y a un conduit ou une ouverture, même si cela ne vous concerne pas directement; Si la zone s'élargit vers vous, c'est le signal pour pointer l'antenne dans cette direction. Le tableau de bord à 45° vous permet de voir sous l'index du tropo et la grille, alors comparez en un coup d'oeil la prévision (couleur de fond) et le trafic réel (illus). Votre QTH carré ne colore jamais, parce que c'est le point de départ et non une zone éloignée. Seuls les carrés qui sont tous au-delà de la couronne sont colorés: un carré traversé par la couronne, ou à l'intérieur, reste libre même s'il y a une des deux stations. Pour voir les lignes, il suffit d'éteindre la boîte ou de choisir une couronne plus grande.

Les critères qui déterminent ce qui vient sur la carte et ce qui est rejeté sont expliqués un par un dans le document 1, section 11 ter.

## 9. La barre temporelle

Ci-dessous sur la carte: date et heure de la prédiction montrée, curseur de +0 à +72 h à 6 heures, index légende 0–10 avec le nom de la source choisie, et heure de la course modèle pour isobare. L'index Trumpet et isobare suivent le curseur; les spots, les stations écoutant et vos décodages sont des observations du moment et restent toujours ceux de --adex. Lorsque le curseur n'est pas sur +0 ils sont atténués, pour se rappeler qu'ils n'appartiennent pas à l'heure de prévision.

## 10. Comment lire le chevauchement

- Haute pression + indice élevé + tache le long de la zone colorée: l'ouverture est là et est celle prévue. Il est temps d'appeler dans cette direction.
- Indice élevé, mais pas de place: ou personne n'y est actif, ou la prédiction est trop optimiste. La couche d'écoute permet de distinguer: s'il y a des récepteurs dans la zone et ne ressent rien, la seconde hypothèse est plus probable.
- Feux longs à l'extérieur des zones colorées: conduit élevé, que les prévisions de surface ne voient pas, comme dans l'événement LA– UA6 [3]; ou une autre propagation échappée du filtre (par exemple E sporadique sans étiquette). DF5AI et DK5YA distinguent tropo et E sporadique aussi de l'heure: les ouvertures qui sont nées et finissent avec un E sporadique diffuse sont à peine [5] tropo.
- Hepburn et F5LEN ne sont pas d'accord: comparez les deux sources avec les boutons et voyez où les vrais points tombent.
- Pression vers le bas: G4MCU a suivi une ouverture sur la balise SK6VHF jusqu'à ce que la pression baisse; 1012 hPa ont considéré l'événement fini [13]. Regardez l'isobare en faisant défiler la barre de temps: si la haute pression se retire, l'ouverture la suit probablement.
- Signaux qui montent: dans le studio WSPR à 144 MHz de KP4MD, le rapport signal/bruit a augmenté dans les fenêtres où Hepburn a prédit une amélioration, et a chuté lorsque la prévision a pris [14]. si sur le même chemin grandit, l'ouverture se forme.
- Chemins de montagne: l'indice ne prend pas en compte les reliefs [1], mais les connexions au-delà des Alpes existent également grâce à des cellules convectifs et des vagues de gravité atmosphérique [4], et LA0BY a travaillé 9A et S5 à travers les montagnes sans Hepburn prédire [3].

## 11. Point ouvert

- Permission de Hepburn et F5LEN d'utiliser leurs cartes dans le programme.
- Étalonnage des coefficients de conversion F5LEN → échelle 0-10.
- Étalonnage des points de contrôle de tout type de carte, à refaire si les sources changent de graphiques.
- Couche optionnelle avec température à l'altitude (850 hPa) de la GFS.

## Principales sources

1. W. R. Hepburn, Centre d'information DX, Index du tropo Hepburn (HTI) et pages de prévision de la duplication troposphérique (dxinfocentre.com). Définition et échelle de l'indice, valeurs négatives et plus de 10, exclusion des dispersions et des reliefs, étapes de 3 et 6 heures sur 6 jours, passage à 18 UTC. Origine des données du modèle canadien mondial: G. Meisse, AMSAT-BB, 27/08/2007.
2. F5LEN, prévisions pour l'Europe (tropo.f5len.org/precasts-for-European) et prévisions mondiales de la propagation tropophérique VHF (tropo.f5len.org/WW) et a noté -Nouvelle version de l'outil de prévision de propagation troposphérique VHF. Indice de l'unité N, 40 étapes de 3 heures, 4 mises à jour par jour, valeur et localisation par clic.
3. S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", Réunion Nordic VUSHF 41°, Skjeberg 2019. gradient normal 6,5 °C/km et inversions allant jusqu'à 10–15 °C/km, rôle de la température et de l'humidité, pressions stables élevées, événement LA– UA6 visible à haute température et non à Hepburn, connexions LA–9A/ S5 au-delà des montagnes.
4. F. Egano IK3XTV, La propagation VHF – La contribution des cellules convectives et des ondes gravitationnelles atmosphériques dans la propagation du Tropo. Importance des 3 premiers km, chemins marins, cellules convectifs, ondes de gravité sur les Alpes, connexions à haute pression, volume commun jusqu'à 3-4 km.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, "Propagation très longue distance dans la bande 144 MHz", 24/7/2003. Les profils verticaux de température et de rosée permettent de reconnaître les inversions, l'utilisation de l'indice Hepburn, la distinction entre tropo et E sporadique selon l'heure.
6. J. N. Gannaway G3YGF, "Propagation de la dispersion troposphérique", QST Novembre 1983. Équivalent du rayon de la Terre et perte de trajectoire utilisée pour l'indice et la carte spot (voir document 1).
7. Le Livre VHF/UHF DX, chapitre 2 -Production troposphérique, pp. 2-23...2-30. Figure des zones réfractaires selon dN/dh (standard − 39 N/km, limites − 79 N/km et 0) et les profils de surface et de gaine. Le texte de la copie fournie n'est pas lisible: seuls les chiffres ont été utilisés.
8. Résumé du projet du 8/11/2026, fourni par l'auteur du programme. Couleurs de la carte, projections requises, calques, barre de temps.
9. Terre naturelle, données vectorielles des côtes et des frontières, domaine public (naturalearthdata.com). Référence générale, non annexée.
10. J. P. Snyder, Projections Map – A Working Manual, USGS Professional Paper 1395, 1987. Formules de projections Mercator et azimutale équidtanti. Référence générale, non annexée.
11. Recommandation UIT-R P.453, indice de réfraction radio. Formule réfractive N et seuil du conduit à −157 N/km. Référence générale, non annexée.
12. NOAA NCEP, Système mondial de prévisions (SGF). Pression au niveau de la mer à 0,25°, quatre courses par jour. Référence générale, non annexée.
13. G4MCU, articles avec étiquette troposphérique (g4mcu.net/tag/troposphérique), 2018-2025. Refration, conduit et dispersion; conduits plus fréquents à haute pression, surtout sur la mer; phare SK6VHF suivi à la baisse de pression à 1012 hPa (juin 2019).
14. C. F. Milazzo KP4MD, Étude de propagation VHF/UHF WSPR, 2012-2014. Le SNR du WSPR signale une croissance de 144 MHz dans les fenêtres d'amélioration prévues par Hepburn et une diminution lorsque les prévisions disparaissent.
15. Open-Meteo, API de prévisions météorologiques (open-meteo.com), utilisation gratuite non commerciale. Pression au niveau de la mer du modèle GFS sur plusieurs points en une seule demande.
16. mqtt.pskreporter.info, distribution MQTT en temps réel des publicités de pskreporter. info: nomination et propriétaire des bénéficiaires (voir document 1).
