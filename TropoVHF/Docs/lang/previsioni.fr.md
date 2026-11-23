# Manuel des prévisions

> Traduction automatique de l'italien : en cas de doute, le guide italien fait foi.

L'onglet Prévisions calcule la propagation tropophérique par elle-même: une carte d'index sur la zone visible et le signal fourni entre votre station et celle correspondante. Ce manuel décrit toutes les données, comment elles sont calculées et d'où elles proviennent. Les chiffres entre crochets renvoient aux sources en bas; dans le programme, chaque donnée des résultats a une bande dessinée (simbolo i) avec la même source et la référence au paragraphe.

## 1. Ce qu'il fait et ce qu'il fait

La prédiction des profils verticaux de température et d'humidité du modèle GFS [1, 2] dérive la réactivité de la couche d'air par couche et évalue la quantité de gains de signal par rapport à une atmosphère normale. Sur le parcours il ajoute le sol réel (Copernicus GLO-30 [3]), la diffraction, le troposcatter, les conduits et le bruit du récepteur.

Ce n'est pas une mesure: c'est une estimation faite sur un modèle météorologique avec des niveaux éloignés de centaines de mètres. Il sert à comparer les situations (aujourd'hui mieux qu'hier, 70 cm mieux que 2 m, ce traite mieux que cela), pas pour garantir une connexion. Le paragraphe 14 explique comment recueillir des éléments de preuve pour les valider.


> "La propagation ne peut pas être généralisée, mais étudiée par rapport à la morphologie locale. «
> — Volker Grassmann, DF5AI, rapporté par Flavio Egano, IK3XTV (Propagation sur 2 mètres)

C'est le principe qui guide le calcul point-point: chaque itinéraire est calculé sur son sol, avec ses horizons, sa mer et ses reliefs; la carte donne l'image générale, traite la réponse.

## 2. Commandes linguistiques

- Bande: 144, 432 ou 1296 MHz. La carte et le calcul utilisent la bande choisie.
- Maintenant: le curseur déplace la prévision à partir de maintenant jusqu'à 72 heures d'avance, dans les 3 heures.
- Mise à jour des prévisions (zone visible): télécharger des profils GFS sur une grille de 240 points couvrant la carte visible. La hauteur de la grille (0,5° ou plus) dépend du grossissement.
- Arrêt: interrompre un téléchargement ou un calcul.
- Détails à afficher (à droite de la carte): trois niveaux, Essentiel, Normal et Complet, allumez un groupe de boîtes; chaque boîte peut ensuite changer à la main. Les boîtes sont en trois groupes:
  - Sur la carte: isolinee del gain, aussi 3-7 dB (rétractation), isobare, inversion thermique, signes météorologiques S ↑ ↑, gain/pression de corrélation, zone de vue directe (paragraphes 4, 5, 7 et 9 bis). Les couleurs restent toujours.
  - Dans le calcul du signal: toutes les lignes du calcul, profil Terre plat, journal de calcul et journal de fichiers détaillé.
  - Corrections et données en direct: corrections météorologiques (Misme, subsidence), contraste air-mer, moyenne avec des taches (corridoi), sauts APRS 2 m et LoRa, au-delà de la montagne sans pertes (paragraphes 4 bis, 9 bis, 9 quater et 10 bis).
  - La normale ajoute isobare, inversion, signes météorologiques, vue directe, terre plate, air-mer et APRS; complète l'allume. Le niveau ne change que ce que vous voyez et quelles corrections entrent: les calculs restent les mêmes.
- Source de données et cas historique: d'où viennent les profils et quel jour vous regardez (paragraphes 3 bis et 3 ter).
- Ma station et le correspondant: localisateur, antennes, puissance, câbles, bruit (paragraphes 7 et 12).
- Calculer le signal et la couverture en optique: les deux calculs au sol (paragraphes 8-13).
- Registre de calcul: chaque étape avec les nombres utilisés, également dans le fichier Data/ forecast-calcoli.log.

## 3. De données GFS au profil

Pour chaque point de la grille et chaque heure, nous lisons la pression sur le sol, la température et l'humidité à 2 m, le vent à 10 m, la hauteur de la couche limite, la CAPE et la température et l'humidité par rapport à 1000, 975, 950, 925, 900, 850, 800, 700, 600 et 500 hPa. À partir de 1.3.0 vous pouvez également lire le vent à 850 hPa et la vitesse verticale à 850 et 700 hPa (paragraphe 9 bis).

Les niveaux sont calculés avec la formule ipsométrique et la température virtuelle; les niveaux sous le sol sont rares. Pour chaque niveau:

- N = 77,6/T · (P + 4810·e/T), avec T en kelvin, P et en hPa, et par la pression de saturation de l'UIT-R P.453-14, eq. 9, avec le facteur de correction EF (réglage sur le texte officiel) [4].
- M = N + 157·h, avec h en km: la réactivité modifiée. Là où M monte, il y a un conduit.

## 3 bis. Source des données

Le menu Source de données choisit d'où proviennent les profils:

- Automatique (recommandé): GFS d'abord de NOMADS [14], le service NOAA qui distribue le modèle: pour chaque moment le programme demande deux découpes de la zone au format G RIB2 et les lit par lui-même; NOMADS n'a pas de limite quotidienne, seulement une par minute que le programme respecte. Avec des grilles larges (de 0,5° à plus haut), il utilise des fichiers de 0,5° ou 1er toutes les 3 heures, beaucoup plus léger. Les points que NOMADS ne donne pas, ou tous si NOMADS ne répond pas, demandez Open-Meteo [2]. Les deux sources donnent le même modèle et la même race, de sorte que vous pouvez mélanger dans la même grille.
- GFS de Open-Meteo: seulement Open-Meteo, comme dans 1.2 (à la limite par minute est attendue).
- GFS de NOAA NOMADS: seulement NOMADS. Plus lentement à télécharger (deux fichiers par heure, une demi-seconde entre une requête et l'autre pour ne pas dépasser la limite NOAA), mais ne consomme pas la limite Open-Meteo.
- ICON-EU (DWD) et ECMWF IFS 0,25°, d'Open-Meteo: modèles autres que le GFS, qui s'appliquent sur tout le réseau et pour les itinéraires. Ils ne se mélangent pas avec le GFS: comparer deux modèles en même temps est une façon simple de voir comment la prévision est incertaine. ICON-UE couvre uniquement l'Europe; ECMWF n'a pas tous les niveaux de pression GFS (les niveaux manquants sautent) et peut ne pas avoir la couche limite.

La source utilisée est écrite dans la ligne d'état et dans la première ligne de résultats (source de données), avec combien de points sont arrivés de chaque service et combien du cache. Le cache disque est séparé par le modèle.

## 3 ter. Cas historiques

Historique Le menu Case examine une ouverture documentée avec l'archive Open-Meteo [15] Prévisions GFS: la carte se déplace vers la zone du cas, la bande se définit elle-même et le curseur de temps part du premier jour du cas. En appuyant sur Mise à jour des prévisions vous téléchargez les profils de ces jours; le calcul du signal fonctionne comme pour les prévisions actuelles. Il sert à comparer le modèle avec ce qui s'est réellement passé.

Le premier cas est celui du 14 au 15 août 2022 raconté dans QST (novembre 2022, « Le monde au-dessus de 50 MHz » [23]): une ouverture sous haute pression entre le Manitoba et le Kansas/Missouri, de 144 à 1296 MHz presque tous dans FT8, avec le record canadien à 10 GHz de 1228,56 km dans Q65 et des signaux jusqu'à 1941 km. La seconde est l'ouverture entre le 29 décembre 2021 et le 2 janvier 2022 sur le Tyrrhénien et la Méditerranée occidentale à 144 MHz, des relations de IC8TEM: premières ouvertures sur 29 (jusqu'à 737 km), plus larges sur 31 ( IT9GSF – EB5EA 1217 km), grande ouverture le 1er janvier entre l'Italie, la Sicile et l'Espagne. Pour les cas plus anciens (p. ex. septembre 2000), l'archive Open-Meteo ne contient aucune donnée. Dans l'archive il n'y a pas de hauteur de couche limite: là sa correction ne s'applique pas.

## 4. L'index et les couleurs

Pour chaque couche entre deux niveaux, le programme compare la chute de N avec les seuils du tableau 6.5 de Bean et Dutton [5]: par exemple entre 1000 et 850 hPa une diminution jusqu'à 60 N/km est normale, plus de 100 N/km est super-réfraction. Une couche super-réfractive vaut 10 à 15 dB de gain; un conduit qui piège la bande de 20 à 25 dB, selon le déficit de M. Vent fort au sol, couche limite élevée et CAPE élevée réduisent le gain, parce qu'ils enlèvent ou brisent les couches [5, § 4.4].

La couleur est le gain estimé en dB, avec une échelle fixe égale dans tous les thèmes:

| Gain | Couleur | Signification |
| 7 dB | Personne | atmosphère presque normale |
| 7-10 dB | bleu | gradient par rapport à la normale |
| 10-13 dB | vert | couche super-réfractive |
| 13-16 dB | jaune | couche forte, mais sans conduit pour bande |
| 16-18.5 dB | orange | couche très forte ou conduit trop mince |
| 18,5-21 dB | rouge | Bande de piégeage des conduits |
| à partir de 21 dB | Pourpre | Voie forte et profonde |

Avec la boîte d'affichage également 3-7 dB zones entre 3 et 7 dB, où quelque chose se forme mais reste sous l'échelle, vous pouvez voir avec une retenue gris clair; les couleurs ne changent pas. Un panneau pointillé délimite la zone couverte par ses propres prévisions et montre le gain maximum visible: si elle est inférieure à 7 dB, la carte reste sans couleurs parce que l'atmosphère est presque normale, pas parce que les données sont manquantes. Lors du téléchargement d'un nouveau clic sur les prévisions de mise à jour n'interrompt pas; pour l'arrêter il y a Stop.

Les couleurs sont opaques à 82%, donc le fond du thème les modifie. La grille de localisation et les noms carrés sont conçus sur les couleurs. Lorsque votre prédiction n'est pas là mais Hepburn ou F5LEN donner 5 cartes d'échecs ou plus apparaissent avec la couleur de cette source.

La carte juge chaque point seul. Le calcul du signal contrôle plutôt que la couche est continue le long de la route (paragraphe 9): une route qui traverse très jaune peut donner moins que la carte vous fait penser.

## 4 bis. Prévisions véhiculées par les publicités: corridors (à partir du 1.3.3)

Politique sur le droit d'auteur: Un lien à lui seul ne doit jamais tirer un conduit. Une réception isolée et courte peut être la dispersion d'un avion de transit (le paquet APRS est très court), une ouverture de quelques minutes ou une erreur, et il donnerait des « pipelines de goutte » qu'ils trompent. Le programme reconnaît un corridor lorsque, dans la même bande:

- au moins 3 paires de stations indépendantes (pas de station partagée) travaillent sur des routes d'au moins 300 km;
- les itinéraires sont parallèles: azimut à ±15°, points moyens à moins de 150 km de l'axe commun;
- tous au cours des 30 dernières minutes, le faisceau répétant à une distance d'au moins 10 minutes (persistance);
- pour l'ARS, un saut ne compte que si le même saut est répété;
- de 1.3.4, les itinéraires de plus de 1500 km ne sont comptés que si le faisceau provient d'au moins deux sources différentes (p. ex. PSKReporter et un amas): d'une source unique, à ces distances en 2 m, sont plus souvent des EME, dispersent des météorites ou de mauvais localisations que des tropos.
- à partir de 1.3.4, ne comptez pas les stations 'à rayon': qui dans la dernière heure a au moins 5 correspondants sur 1000 km répartis sur un ventilateur d'au moins 150°. En 2 m le trope est directionnel; un récepteur qui "libre" ensemble Israël, Portugal, Angleterre et Italie de 2000 à 3700 km, souvent avec des signaux positifs, a presque certainement la fréquence mal configurée. Dans le 17-22/11/2026 I5MZY archive 72% des logements sur 2 m sur 1000 km provenaient des stations ainsi; le 22/11 à 10:53 UTC étaient OH6HLH, OZ1ITS, F4LUK, OM8ABC et SP2JLR, et avec le filtre le corridor le plus long descend de 2175 km (répertoire 1.3.3) à 615 km.

Sources: Spot de cluster (et non AS/ACS), PSKReporter via MQTT, ses décodages QSO et WSJT-X/JTDX/MSHV, et sauts APRS en 2 m et LoRa 433/868 MHz.

2 m et LoRa. Le programme lit APRS-IS en lecture seule (rotation.aprs2.net, filtre sur l'Europe) et extrait des sauts radio entre stations connues (sorgente → digipeater → IGate), comme la carte VHF de DXView et APRS PropView (RF-YVY, Apache 2.0). Trois bandes valent: 2 m, LoRa 433 MHz (correspondant aux corridors de bande 432) et LoRa 868 MHz (corridoi qui compte pour 1296: un conduit qui apporte les 868 MHz aussi les fréquences les plus élevées). La bande vient de la LoRa tocall (liste aprs-deviceid) et des fréquences ou des mots dans le commentaire; un saut ne vaut qu'entre deux stations de la même bande.

Le point délicat est les répéteurs actifs: un signal reçu à 500 km peut être obtenu avec deux ou trois retransmissions de digipeater. Un saut ne compte donc que s'il se situe entre deux émetteurs réels et consécutifs:

- le chemin doit être tracé: chaque digipeater qui a retransmis a mis son nom, et le saut est mesuré entre un et le suivant; un alias utilisé sans nom (W IDE1 *, W IDE2 *, alias régional) brise la chaîne et ce tronçon ne compte pas;
- retransmissions cachées: le programme se souvient du chemin que chaque source demande habituellement (par exemple W IDE2 -2). Si un paquet est livré avec un saut en moins (W IDE2 -1) et aucun nom ne l'explique, un digipeater l'a retransmis sans suivi et le paquet est jeté; les sources jamais vues auparavant ne comptent pas;
- digipeater au milieu de la route: si entre les deux stations, plus ou moins sur la rue, il y a un digipeater de la même bande active dans les trois dernières heures, le saut est écarté, car il aurait pu être retransmis (c'est surtout pour le digipeater LoRa qui retransmet sans changer le chemin);
- les paquets non radio (qAC, qAX, TCPIP), ceux dirigés depuis l'Internet sur la radio à partir d'un IGate (troisième partie), HF et différentes fréquences, stations à plus de 1500 m d'altitude (pallons, avions) et saute sur 1500 km.

Effet sur la carte (à partir du 1.3.4). Elle n'attire que la zone où le minimum garanti est valide: une bande de ±60 km autour de la partie centrale de chaque itinéraire (de 15 à 85 % de sa longueur), pas sur les stations aux extrémités, où le conduit peut ne pas être là. Les bandes de tous les couloirs sont tracées ensemble, dans une couleur neutre (le texte) et semi-transparente: les chevauchements ne s'additionnent pas, et la couleur ne confond pas avec le pourpre de l'échelle (21 dB et au-delà). Les étiquettes (caractères, distance moyenne, gain minimum) sont scannées l'une avec l'autre; en passant avec la souris sur une étiquette apparaissent les sections du couloir.

Dans la région, et seulement près de l'heure actuelle (± 90 minutes), les prévisions ne tombent pas en dessous d'un minimum. Le raisonnement est le suivant: si les stations passent vraiment, la propagation est là, peu importe le modèle météorologique.

Parce que les routes courtes valent peu (à partir de 1.3.4). En 2 m, entre 300 et 600 km, deux bonnes stations (une centaine de watts, des antennes, FT8 ou CW) relient presque tous les jours même à l'atmosphère normale, pour le troposcatter: le signal diffusé par les petites irrégularités de l'air dans le volume commun des deux faisceaux. Trois ou quatre spots à 500 km disent que le groupe fonctionne comme d'habitude, et non qu'il y a une ouverture: GEKO Magazine n. 69 ( IU3QEZ) estime pour le troposcatter avec 2×10 éléments fluides d'environ 440 km en QRP SSB, 510 km en SSB avec 50 W, 600 km en QRP CW et 640 km en SSB avec 500 W (sources, onze tours). Seulement plus de 600 à 700 km le troposcatter n'est plus suffisant et une série de connexions parallèles indique vraiment un conduit ou une couche. Jusqu'à 1.3.3, un corridor de 450 à 600 km a porté les prévisions à 9–12 dB, et la carte montre une amélioration qui n'est pas là.

La valeur de départ va donc avec la distance moyenne des itinéraires:

- jusqu'à 400 km: 3 dB;
- de 400 à 700 km: sel linéaire de 3 à 10 dB (500 km → 6,3 dB; 600 km → 7,7 dB);
- de 700 à 900 km: de 10 à 16 dB; plus de 900 km restent à 16 dB.

Sous le 7 dB, c'est-à-dire sous le début de l'échelle, le minimum reste que: le couloir est dessiné avec son étiquette, on voit que la bande fonctionne, mais les couleurs de la carte ne changent pas (avec la boîte montre également 3-7 dB le pointillé gris). Au-dessus de 7 dB la valeur est pesée, moitié et moitié, avec le nombre d'itinéraires indépendants (3 coups → 0,25; 6 ou plus → 1) et avec le S/N médian des relations (−20 dB → 0,5; 0 dB ou plus → 1; non connu, comme dans les taches de cluster, → 0,75), à partir de 7 dB. Exemples: 3 routes sans S/N sur 500 km → 5,4 dB; 5 routes sur 615 km → 7,7 dB; 3 routes sans S/N sur 900 km → 11,5 dB; 3 routes vers -20 dB sur 900 km → 10,4 dB; 6 routes vers 0 dB sur 900 km → 16 dB.

La distance minimale reste de 300 km: l'élever à 500 km aurait également enlevé les véritables ouvertures à courte portée (par exemple un conduit sur le Tyrrhénien à 400 km, qui est reconnu par les prévisions météorologiques et des signaux très forts plus que de la seule distance). Dans l'archive I5MZY, le 22/11/2026 à 10:53 UTC, avec cette règle et le filtre à rayons les plus longs couloirs étaient 615 km (7,7 dB) et 540 km (6,3 dB); avec les règles de 1.3.3 il a atteint 2175 km et 16 dB.

Jusqu'à 1.3.3 le minimum était valable sur tout le rectangle du couloir et dépendait uniquement de la distance (de 10 dB à 300 km à 16 dB à 900 km): avec de nombreux longs corridors ensemble (par exemple le 22/11/2026, douze corridors jusqu'à 2620 km) a soulevé presque toute l'Europe à 16 dB.

Les courbes de gain ne dessinent plus «gocce»: les petites îles de 220 km dans les deux sens restent colorées mais sans courbe fermée, à moins qu'un couloir ne les confirme. Il s'éteint avec la "Prévision médiée par les spots".

## 5. Lignes optionnelles et comparaison avec la pression et l'inversion

Courbes de gain: lignes à 7, 10, 13, 16, 18,5 et 21 dB, les bords de l'escalier, tracés sur le champ lissé avec la méthode des carrés de marche, avec l'étiquette en dB.

Isobare: pression au niveau de la mer tous les 4 du même GFS, à l'heure de prévision, avec les centres H et L.

Inversion thermique: courbes recouvertes d'air à 925 ou 850 hPa plus chaud que celui à 2 m, à +1, +3, +5 et +8 °C.

Sous les contrôles, une ligne donne l'accord de gain avec la pression et avec l'inverse sur la zone visible: coefficient de corrélation Pearson, −1 à +1. Les conduits naissent souvent dans les anticyclones (l'air qui fait chuter la chaleur et forme l'inverse), donc un accord positif avec la pression est normal; mais dans le même anticyclone, le gain change beaucoup entre la mer, la côte et les montagnes. L'inverse seul ne suffit pas: plier les vagues seulement si au-dessus de l'air est même sèche [5]. La ligne sert à voir, maintenant pour l'instant, qui des deux meilleurs explique les zones colorées. La même comparaison est écrite dans le registre.

## 6. Pipelines potentielles: double calcul

Les niveaux de GFS varient de 200 à 1000 m: une inversion fine est répandue et le modèle ne voit pas le conduit. Si entre deux niveaux la diminution de N dépasse la normale (40 N/km), l'air est stable ( gradient thermique inférieur à 3 K/km) et supérieur est sec (rapport d'humidité spécifique inférieur à 0,8), le programme considère un conduit potentiel et le calcule de deux façons:

- Calcul A: toute la chute de l'excédent concentré dans 50 m.
- Calcul B: la même diminution répartie sur l'épaisseur typique des pipelines de cette zone, tirée des cartes climatiques de l'UIT-R P.453 [4] (au sol ou surélevée).

Pour le déficit et pour la longueur d'onde maximale piégée, la valeur la plus basse est conservée: c'est une estimation prudente.

Les données de l'UIT-R P.453 sont © UIT et peuvent être utilisées à des fins personnelles et non commerciales en citant la source, mais ne peuvent pas être redistribuées: pour cela je ne suis pas dans le paquet programme. Pour activer le calcul B, il suffit de télécharger les cartes P.453 de l'UIT dans l'onglet: le programme, après avoir montré les conditions d'utilisation, télécharger l'archive des pipelines du site de l'UIT (levated_and_surface_duct.zip) pour l'usage personnel de l'utilisateur, extraire les fichiers dans Data/ P453 et activer le calcul sans redémarrer. Alternativement, télécharger à partir de la page de produit numérique du Groupe d'étude 3 de l'UIT-R (itu.int, Groupe d'étude 3 de l'UIT-R, Produits numériques, voix P.453, Données de surface et données de conduit élevées) et mettre les sept S_OCCUR. TXT, S_THICK.TXT, S_STREN. TXT, E_OCCUR.TXT, E_base.T Sans les fichiers, le programme utilise seulement le calcul A et l'écrit dans l'onglet et dans le registre. Le rapport entre le déficit A et l'intensité type P.453 est anomalie: Combien de fois le pipeline d'aujourd'hui est plus fort que la normale. P453

## 7. Stations et points de repère

Le propriétaire peut avoir 4, 6, 8 ou 10 caractères. Avec 8 ou 10 caractères, la position est précise et la part du sol provient de Copernic GLO-30: la hauteur de l'antenne est suffisante du sol. Avec 4 ou 6 caractères le carré est trop grand: il vaut mieux écrire le quota de l'antenne sur la mer; si 0 le programme reste l'estimation avec le sol SRTM au centre du carré plus la hauteur du sol et le signale dans les résultats. Pour la hauteur correspondante, la hauteur par défaut est de 3 m du sol; avec la part de mer 0 est utilisé sol SRTM plus hauteur.

## 8. Terre, diffraction et troposcatter

Le profil de la route est échantillonné sur Copernicus GLO-30 tous les 30 m (plus de 120 km de large). Le rayon de Terre équivalent est k = 157/(157 + ΔN), avec ΔN le déclin de N au premier km des profils GFS le long de la route [6, 7].

Reliefs proches des stations (à partir de 1.3.2, critère de l'auteur). P.452 paie l'angle d'horizon linéairement, 0.573 dB par mrad: un relief de quelques dizaines de mètres à quelques centaines de mètres de l'antenne coûte des dizaines de dB, beaucoup plus que ce qui est observé. Pour cette raison, les reliefs à moins de 20 km de chaque station peuvent s'additionner jusqu'à 6 dB: l'angle d'horizon qui leur est dû est limité à celui des reliefs les plus éloignés plus 10,5 mrad. Il est valable pour votre station et pour le correspondant. La ligne «Near reliefs» indique combien de dBs ont décollé.

Source des terres (à partir de 1.3.2). Les quotas proviennent de Copernicus DEM GLO-30 [3]: 1 deuxième arc, la mer est 0 et la côte est libre. Jusqu'au 1.3.1, le programme utilisait les boîtes de terreaux 'skadi' de l'AWS. Près de la côte, ces places mélangent la SSRTM avec des données de fonds marins à basse résolution et diffusent la part de l'île sur la mer: autour de Capri créent un anneau fantôme de 80 à 100 m entre 0,5 et 1 km de la côte. Le 21 novembre 2026, IC8TEM a signalé un horizon inexistant au nord-ouest; la comparaison point par point avec Copernicus l'a confirmé (94 m en mer au nord-ouest de Marina Grande vs. 0). Ce faux horizon, de +2,6°, coûte environ 26 dB de troposcatter aux stations côtières. Les panneaux skadi restent seulement une réserve lorsque Copernicus ne répond pas. Les cadres Copernicus pèsent de 15 à 30 Mo et ils sont téléchargés une fois; ceux de la seule mer n'existent pas et valent 0.

- Espace libre: Lfs = 92,44 + 20 log f(GHz) + 20 log d(km) [8].
- Diffraction: méthode de Bullington sur le profil réel plus UIT-R P.452-16 § 4.2 [6] delta-Bullington correction des terres sphériques, avec UIT-R P.526 [9] perte sphérique.
- Première zone Fresnel: rayon √(λ·d1·d2/d); libre si le sol laisse au moins 0,6 du rayon. Le bord du couteau [9] est montré pour chaque obstacle.
- Dans la conception du profil, le rayon direct est vert en vue optique et rouge s'il passe sous le sol ou sous la courbure de la terre; les points rouges sont les obstacles qui envahissent la 1ère zone de Fresnel (ligne pointillée pourpre). Sur la mer, le "point rouge" à 0 m d'altitude n'est pas une montagne: c'est la mer qui monte pour la courbure. Passer avec la souris sur le rayon ou sur un point apparaît une bande dessinée avec explication et valeurs.
- Lorsqu'un relief près de l'antenne ferme la direction (par exemple un horizon de dizaines de degrés), la diffraction et le troposcatter donnent des pertes de centaines de dB: au-dessus de 300 dB le programme écrit "> 300 dB" et "direction fermée par un obstacle voisin", le signal "<−230 dB" et les rapports S/N "−60 dB". Dans ce cas, il est conseillé d'examiner «usine recommandée» et «Via monte».
- Troposcatter: ITU-R P.452-16 eq. 45 à 50% du temps [6], avec une distance angulaire des horizons et N0 des profils.
- Sans tropo: somme des pouvoirs de diffraction et de troposcatter, jamais sous l'espace libre.

## 8 bis. Troposcatter: quatre modèles et incertitude

À côté de P.452, le programme calcule le troposcatter avec trois autres modèles, tout comme le programme TropoLink [16], et montre leur différence comme incertitude:

- NBS TN101 § 9 [25] (à partir de 1.3.5), médiane: L = 30 log f − 20 log d + F(γd) − F0 + H0 + Lgp. F(γd) est la fonction d'atténuation de l'annexe III dans ses formes analytiques (Ns 301) avec la correction pour Ns, H0 la fonction de gain de fréquence de § 9.2: à 144 MHz, avec des antennes basses en longueur d'onde, la réflexion du sol efface partiellement le champ dans la partie inférieure du volume commun et la perte de sel de 3 à 10 dB. Ns est celui des profils GFS de l'heure, les horizons et les hauteurs effectifs proviennent du profil Copernicus, Lgp = 0,07·e^(0,055(Gt+Gr))) est la perte de couplage d'ouverture. C'est la même forme que l'IMT utilise dans son intérieur, mais avec la réactivité d'aujourd'hui au lieu de celle du climat. Il teste sur des sections plates à 144 MHz avec des antennes à 20 m: dans un rayon de 2 à 4 dB de ITM entre 150 et 900 km et environ 2 dB dans la forme simplifiée 9,2b de TN101, qui n'a pas de H0.

- UIT-R P.617-5 [17] L = F + 22 log f + 35 log φ + 17 log d + Lc, avec f en MHz, φ en mrad, d en km, F = 0,18· N0 ·e^(−hs/7,35) − 0,23·ΔN et Lc = 0,07·e^(0,055(Gt+Gr)). N0 et ΔN sont généralement tirés des cartes climatiques de l'UIT; ici nous utilisons toutes les heures les profils GFS ( N0 ·e^(−hs/7,35) est la réactivité au sol, ΔN la diminution dans le premier km), ainsi aussi le troposcatter suit le temps.
- RSGB VHF/UHF Manuel Formule, DL2FZN [18]: L = Lfs + 24,44 + 4 283 ln f + 0,06656 d. La pente avec la distance est presque égale à celle du PROPBER (0,067 dB/km).

Volume commun (à partir de 1.3.1): est le point où le rayon des deux stations est traversé, chacune inclinée à son angle d'horizon, sur une Terre d'un rayon équivalent ae = k·6371 km. À partir du TX, le rayon s'élève en z = hTX + x·φt + x2/(2·ae); La distance x = (hRX − hTX + d^γr + d2/(2·ae) / φ, avec φ = d/ae + φt + φr. Avec des horizons plats et des antennes au même quota, la base vaut d2/(8·ae): environ 1,3 km à 300 km, 3,7 km à 500 km, 9,4 km à 800 km. Plus le volume est élevé, plus l'air est sec et rare et moins il s'étend: pour cette raison plus de 800 à 1000 km le troposcatter s'épuise. Dans les résultats sont la part et la position de la base, la réactivité, le gradient et l'humidité du GFS à cette altitude, et la distance équivalente de Bray (QST, novembre 1961): chaque horizon supplémentaire ou moins vaut environ 111 km. Référence: présentation « Over the horizon in VHF » par IU3QEZ (GEKO Magazine n. 69).

La ligne Troposcatter, d'autres modèles montrent les deux pertes et l'incertitude comme la moitié de la différence entre le modèle le plus optimiste et le plus pessimiste. Ce ne sont que des comparaisons: la perte sans tropo reste celle de P.452.

## 8 ter. Après-midi troposcatter sur les reliefs (à partir de 1.3.4)

IK3XTV, de Thiene ( JN55, avec les Alpes à 10 km), travaille l'après-midi d'été dans le sud de l'Allemagne à 400-500 km, sans vue, avec des signaux forts mais avec une évanescence rapide et profonde; l'auteur les attribuait aux «cellules convectifs alignées comme un train de lentilles». L'observation est confirmée; l'explication adoptée dans le programme est une autre, plus ferme physiquement.

L'après-midi, le soleil remue l'air au-dessus des reliefs et la couche limite atteint 2–3 km. En haut, l'air humide en dessous rencontre l'air sec au-dessus: là, la turbulence de l'indice réfractif (la taille qui gouverne le troposcatter) est maximale. Lorsque ce sommet atteint le volume commun de la route, qui sur 400–500 km est d'environ 2–3 km, le troposcatter est renforcé. Cela explique l'heure de l'après-midi, la haute pression (ciel silencieux, chauffage fort) et l'évanescence rapide, typique de la diffusion et non des conduits. Les cellules convectifs n'agissent pas comme des lentilles: elles produisent des turbulences, et c'est la turbulence en haut de la couche de refond qui propage le signal.

Conséquences pour les prévisions de lecture:

- sur la terre le premier après-midi est le pire moment pour les canaux bas et les couches, mais pas pour le troposcatter au-delà des montagnes: les deux choses devraient être lues séparément;
- le programme a déjà les deux données qui servent, la hauteur de la couche limite fournie par le modèle et la part du volume commun de la route (enregistrer "Volume commun" dans les résultats): lorsqu'ils sont proches, un plaisir de montagne de l'après-midi peut aller mieux que le calcul dans l'atmosphère normale;
- à partir de 1.3.4 le calcul point-point prend en compte: si le volume commun est sur la terre, le long du parcours il y a des reliefs d'au moins 800 m, le soleil est de plus de 15° et la couche limite dépasse 800 m, et le sommet de la couche remodelant (taux de sol modèle + hauteur limite de la couche) est à moins de 500 m de la base du volume commun, le troposcatter gagne 3 dB; entre 500 et 1000 m le gain linéaire tombe à zéro. La ligne "Troposcatter après-midi". Avec la neige le long de la route (au moins 10 cm sur le sol selon le modèle, variable neige_profondeur de Open-Meteo ou SNOD du GFS de NOMADS), le renfort ne s'applique pas: sur la neige, le sol reste froid et la couche reformée ne pousse pas. La valeur de 3 dB est provisoire et doit être calibrée avec des liaisons réelles; elle s'éteint avec des corrections météorologiques.

Sources: observations de F. Egano IK3XTV (Radio Rivista e www.qsl.net/ik3xtv, "Propagation sur 2 mètres", "Cellules de convection", "Troposcatter sur les Alpes"), sources § 13.

## 9. La couche fondatrice le long de la route

C'est le cœur du calcul des signaux. Le long de la route, les profils GFS sont pris tous les environ 25 km. Pour chaque couche de pression, l'étirement le plus long dans lequel la même couche est présente sur des points consécutifs; compter comme conduit seulement les couches épaisses au moins 40 m [10].

- continuité: de quel km à quel km, et la fraction de la route;
- le haut et la base, et l'inclinaison en m/km;
- sol à l'intérieur de la couche: le km où les montagnes s'élèvent au-dessus de la base;
- Terrain couvert: sur la mer le conduit est presque toujours, décide le tronçon de terrain [10]. Si la terre dépasse 10 % de la route, la fraction utilisée est le minimum entre la continuité et la couverture de la terre.

Le gain de trompe est le gain de la couche pour la fraction utilisée. Si plus de la moitié des profils sont sous-réfractifs, le gain est négatif.

## 9 bis. Corrections météorologiques: stabilité, subsidence, grève

A partir de 1.3.0, le profil donne également trois informations météorologiques, qui peuvent être vus dans le registre (récupérer la «stabilité (Misme)»), dans la ligne Meteo le long de la route des résultats et, avec la boîte Segni meteo, sur la carte.

- Stabilité du misme [5, chapitre 6]. Misme lie le champ au-delà de l'horizon à un paramètre composé: M = a·(ge - 40 + b·ΔW), avec a = 0,5 dB par N/km, le gradient équivalent ge et la stabilité thermique ΔW, les travaux pour élever l'air de 1 km. Le programme calcule ΔW = g·γ(-------)/-0 dz entre le sol et 1 km au-dessus, avec une température potentielle de 1; l'atmosphère standard est d'environ 56 J/kg. Le terme ajouté au gain est 0,07 dB pour J/kg au-delà de la norme, limité entre −4 et +6 dB, et ne compte que là où aucun conduit ne piège la bande (le gradient est déjà dans l'indice Bean et Dutton). L'étalonnage est temporaire: il doit être vérifié avec les rapports (par. 14).
- Subsidence et vol en montée [19]. Moler et Holden, sur des routes allant de 125 à 305 km jusqu'à la mer à 1365 et 9380 MHz, ont trouvé le signal le plus élevé d'environ 2,5 dB en moyenne avec l'air qui descend avec l'air qui monte, et dans un cas un renforcement d'environ 20 dB pendant 3 heures au début de la subsidence, suivie d'une diminution de 22 dB lorsque l'air est revenu à converger. Le GFS donne la vitesse verticale à 850 et 700 hPa. Si à ces deux niveaux l'air chute de plus de 1 cm/s et qu'il y a une baisse inverse ou forte de l'indice de réfraction potentiel K (N rapporté adiabatiquement à 1000 hPa, qui met en évidence l'air sec et chaud tombé du haut [5]), 1,5 dB est ajouté; si l'air augmente de plus de 5 cm/s s'ils décollent 2.
- [20] Scirocco. L'air chaud et sec du Sahara sur l'air marin plus frais donne des inversions très fortes de la Méditerranée orientale à Gibraltar. Le panneau S apparaît où à 850 hPa le vent vient de 100 à 200° avec au moins 7 m/s et la température est d'au moins 16 °C, sur la Méditerranée. C'est juste une indication: le gain détermine les couches du profil.

Sans un conduit qui piège la bande le gain total ne dépasse pas 16 dB, l'extrémité du jaune: les corrections rendent les couches plus fortes plus évidentes mais ne les passent pas par les conduits (rouge et violet restent aux conduits).

- Persistance (à partir de 1.3.1). Les plus grandes ouvertures viennent après des jours de haute pression déjà bons (VHF/UHF DX Book; Biancomano et Leyshon 1998, règle de 12 heures; les rapports du 29/12/2021-2/1/2022 croissent du premier au quatrième jour). Pour chaque cellule, le programme compte les heures des 24 précédentes avec un gain d'au moins 13 dB et additionne jusqu'à 2 dB en proportion, seulement lorsque le gain est déjà d'au moins 10 dB.

Comment lire les signes sur la carte (Modifier les signes météorologiques; à partir de 1.3.2 le symbole est grand double et passer avec la souris sur un signe apparaît une bande dessinée avec l'explication et les valeurs de la cellule):

| Signe | Couleur | Qu'est-ce que ça veut dire? | Effet sur la trompe |
|---|---|---|---|
|  ↓  | vert | subsidence: gouttes d'air (anticyclone), chauffe pour la compression et sèche, renforce l'inversion et la maintient ferme | favorable, +1,5 dB |
|  ↑  | rouge | air de montée (avant, basse pression, convection): mélange les couches basses et casse les inversions et les conduits | défavorable, −2 dB |
|  S  | orange | Sirop: air chaud et sec du Sahara sur air frais | indice favorable, aucune correction: décider les couches |

Une zone pleine de vert ▼ sous un anticyclone est le signe classique d'une ouverture dure; Rouge ↑ flèches le long de la ligne disent que les couches se brisent, bien que la couleur de la cellule est encore bonne.

La boîte Corrections météo éteint ces corrections pour voir le modèle de 1.2.

Validation (paragraphe 14): sur les 593 connexions à 144 MHz entre le 29 décembre 2021 et le 2 janvier 2022, comparé aux mêmes itinéraires et deux semaines plus tard, l'indice de séparation (0,5 = zéro, 1 = parfait) est de 0,79 sans corrections et 0,91 avec corrections (avant le toit à 16 dB). La même comparaison montre la limite principale: le conduit sur la mer à basse altitude, qui à l'époque apportait les signes entre la Campanie et la Ligurie, n'est pas visible au niveau du GFS.

## 9 ter. News 1.3.2: deux estimations, plafond, haute pression, mer, stations, crêtes

Une estimation prudente et agressive. Le gain du gradient passe de 0 dB à la limite de la normale jusqu'à 10 dB jusqu'au seuil de super-réfraction. Dans la couche 1000-850, le seuil prudent est Bean et Dutton (100 N/km); le seuil agressif est le seuil classique de la super-réfraction de 79 N/km (k 2; P.453 utilise −100 N/km dans les 100 premiers m), qui avec k50 = 157/(157 - ΔN) est k 2 (k = 2,5 correspond à environ 94 N/km). Le calcul point-point montre les deux pertes flanquées; avec un conduit qui piège la bande coïncide. La carte utilise la prudence.

Le grenier de 2500 m. Les routes et les conduits ne sont recherchés que jusqu'à 2500 m au-dessus du sol: plus les antennes des radios amateurs ne les exploitent pas. Avec 1500 m la validation a perdu un point de séparation et 4% des connexions au-dessus de 16 dB, car en hiver les inversions de subsidence sont souvent entre 1500 et 2500 m.

Haute pression étendue. +1 dB où au moins 7 des 9 cellules autour ont réduit la pression à la mer de 1020 hPa ou plus et le vent est faible dans les couches basses (10 m sous 5 m/s, 850 hPa sous 8 m/s), sauf pour les mouvements ascendants.

Poids en mer. Dans le calcul point, le gain des couches est multiplié par 0,8 + 0,4 ·μ1, où μ1 est le facteur de l'UIT-R P.452 (eq. 51) sur la plus longue partie continue de la terre: 1 sur la haute mer, petite avec de longues étendues de terre.

Stations et conduites. Lorsqu'un conduit qui piège la bande ne couvre qu'une partie de l'itinéraire, la part de chaque antenne est comparée à la base et au sommet du conduit au début et à la fin de la section couverte; le gain est multiplié par 0,5 + 0,5· √(e^(−ΔhTX/300)·e^(−ΔhRX/300)), avec Δh la distance en mètres hors du conduit. Avec le conduit sur presque toute la route est le couplage de § 10.

Couche et crêtes. Si le sommet de la couche limite de la GFS (taux de sol du modèle le plus limite_couche_hauteur) dépasse les crêtes de l'itinéraire (locaux maximaux de plus de 300 m, si le plus haut dépasse 500 m), la couche passe au-dessus des montagnes sans rupture: +1,5 dB pour la fraction des crêtes couvertes. C'est l'observation de IT9GSF sur les ouvertures de la Sicile à l'Allemagne avec Tyrrhénien favorable.

Niveaux proches du sol sur la mer. Avec la source NOMADS le programme télécharge également la température de surface (sur la mer est le SST), la température et l'humidité à 80 m et dans la couche 30–0 hPa au-dessus du sol. Sur les points de mer (taux de modèle inférieur à 5 m), ces niveaux sont ajoutés au profil, sous le premier niveau de pression, pour voir le conduit à 100–200 m que les niveaux de pression ne résolvent pas. Sur le sol ne sont pas utilisés: les couches de nuit et de jour ne sont pas encore valides. Open-Meteo donne seulement la température de surface.

Tuyau d'évaporation. De SST, de la température et de l'humidité à 2 m est estimée la hauteur de la conduite d'évaporation avec le profil logarithmique neutre (comme dans le modèle de Paulus et Jeske, sans correction de stabilité): l'air aux cheveux de l'eau est saturé au SST (98% pour le sel), donc

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

limité entre 0 et 40 m. Pour piéger 144 MHz il faudrait environ 180 m (Kerr), puis à 144 MHz le canal d'évaporation renforce peu; de 432 MHz vers le haut, avec des antennes basses sur la mer, beaucoup (paragraphe 11 bis). Saisissez l'équation parabolique et dans la ligne Corrections 1.3.2.

Heure de la journée (visite). Le programme calcule la hauteur du soleil sur chaque point. De jour en jour sur la terre, avec le soleil de plus de 15° et la couche limite de plus de 800 m ou le vent de plus de 5 m/s, avertit que les conduits de terre sont remodelés; la nuit, avec le soleil de moins de 3°, le vent de moins de 3 m/s et la couche limite de moins de 300 m, avertit qu'avec le ciel serein sont possibles par irradiation, qui tombent après le lever du soleil. Pas sur la mer. Pour l'instant, ils sont repérés, pas des nombres: la validation de décembre n'a pas assez de jours d'été pour les calibrer. Ce sont deux points que l'auteur considère importants, à partir de l'observation quotidienne.

Profil normal CRPL (alternative). Au lieu de 40 N/km fixés, la diminution normale suit l'atmosphère de référence exponentielle CRPL: N(h) = Ns·e^(-c·h), avec −ΔN au premier km = 7,32·e^(0,00577·Ns) (Bean et Thayer 1959). Avec l'air humide (Ns haute) la normale est plus raide et sert un gradient plus fort pour donner un gain; en altitude est plus doux. Il s'avère mauvais jusqu'à ce que la validation dit que ça aide.

## 9 quater. Contraste air-mer et basse mer: l'Adriatique (à partir du 1.3.3)

Les ondes radio ne sentent pas le fond marin: ressentez le contraste entre l'air et la surface de l'eau. La profondeur compte parce qu'elle décide à quelle vitesse la température de la mer suit celle de l'air.

L'Adriatique est une mer basse. La profondeur moyenne est d'environ 250 m, mais le bassin nord dépasse rarement 100 m et le golfe de Trieste reste inférieur à 25 m; seulement au sud descend sur 1200 m. Une couche d'eau si fine se réchauffe et se refroidit en quelques jours, le Tyrrhénien en quelques semaines. Il y a trois situations typiques:

1. Printemps et début d'été, favorable. La mer est encore froide, même pour l'eau douce des rivières Alpine et Po; elle arrive à l'air chaud et sec de la plaine ou des Balkans. L'air chaud au-dessus de l'eau froide forme un renversement sur la mer (oléoduc d'advection): est l'état classique des ouvertures entre l'Italie, la Slovénie et la Croatie.
2. Automne et hiver avec bora, défavorable. L'air froid au-dessus d'une mer plus chaude: la couche basse devient instable, presse et le conduit se brise.
3. Après un bora, par chance. Juste parce que la mer est basse, un bora le refroidit quelques degrés en quelques jours. Si vous entrez dans un anticyclone à l'air chaud, le contraste air-mer est fort et le conduit est plus robuste que d'habitude.

L'Adriatique est également un chenal fermé entre Apennins et Dinari, de moins de 200 km de large: la couche de mer reste confinée, et les routes le long de l'axe (Puglia–Venezia–Trieste, environ 700 km) restent toutes sur la mer, la meilleure condition pour un long conduit.

Ce que le programme calcule. Sur les points de mer, avec les corrections météorologiques et la boîte «Contrasto aria-mare»:

| État | Correction |
| --- | --- |
| température potentielle à 925 hPa (ou 950) au-dessus du SST de 4 K | _ |
| au-delà de la SST de 7 K | +2 dB |
| avec humidité de l'air à 925 hPa (plus de 80%) | −0,5 dB sur ces primes |
| mer refroidie à 1,5 °C ou plus en 72 heures avant, avec un contraste favorable | _ |
| air plus chaud à 2 m de 2 °C (mélange) | −1,5 dB |
| mer plus chaude de 4 °C | −3 dB |

La correction totale reste entre -3 et +3 dB et, sans conduit, le gain reste sous le toit jaune. Les seuils sont temporaires: dans l'atmosphère standard, la température potentielle à 925 hPa dépasse celle de la mer d'environ 2–3 K, alors 4 K indique déjà un inverse.

La mémoire du SST. Les modèles maintiennent la température de la mer presque stable pendant la prédiction. La diminution récente est dérivée de l'analyse des trois jours précédents (Open-Meteo, seulement la température de surface, seulement les cellules marines): maximum 72 heures avant moins la valeur actuelle. Si le service ne répond pas, le bonus mémoire ne s'applique pas.

Limites. La SST des modèles sous la côte et dans la haute Adriatique est grossière; le stylo froid du Po, par exemple, est mal résolu. La batimétrie n'entre pas dans le calcul: les sources donnent toujours le SST, qui résume déjà l'effet du fond marin. Le contraste air-mer se chevauche en partie à la fin de la stabilité de Misme (§ 9 bis); pour cela les bonus sont faibles. Les seuils vont s'étaler avec les couloirs de la tache et le QSO à travers l'Adriatique.

## 10. Conduite sur l'ensemble de l'itinéraire

Si la couche piège la bande sur au moins 80% de la fraction utilisée et que les stations ne sont pas en vue, la perte dans le conduit est également calculée: espace libre, plus le couplage des antennes (3 dB si elles sont à l'intérieur du conduit, plus si elles sont au-dessus ou au-dessous), plus l'atténuation résiduelle γd = 5·10−5·ae·f^(1/3) dB/mrad de l'UIT-R P.452 eq. 49 [6]. Si elle est inférieure à la perte sans tropo devient la perte attendue.

Une bande ne reste dans le conduit que si sa longueur d'onde n'excède pas λmax = 2 √2·(2/3)· √(ΔM·10−6)·d (Kerr, eq. 17 [11]). Pour cela, la même couche peut piéger les 23 cm et 70 cm mais pas les 2 m. Cependant, la limite n'est pas nette: Kerr avertit que même les vagues souffrent plusieurs fois plus du conduit, et Hepburn et le livre VHF/UHF DX parlent de bords «sphérés» et de conduits «pardonnes». A partir de 1.3.1, le programme utilise un conduit souple: si λmax se situe entre λmax et 3·λmax, le gain passe progressivement de 15 dB (λ = 3·λmax) à le gain complet du conduit (λ = λmax), le facteur s = (λmax/λ − 1/3)/(2/3). Dans ce cas, les couleurs du conduit sont autorisées. Le log et la ligne Meteo le long de la route indiquent les points avec canal mou.

## 10 bis. Via Monte

Lorsque le correspondant est fermé derrière l'horizon, le programme cherche la meilleure direction pour pointer les antennes: la pente éclairée étend le signal comme cible radar. Il fait le cercle de l'horizon tous les 5 (sauf ±15° autour du correspondant) et dans chaque direction prend le point d'horizon éclairé, sur 100 m et entre 1 et 60 km. Une estimation rapide (espace libre, conversion bitactique et 10 dB pour chaque niveau d'horizon du diffuseur au correspondant) choisit les 4 plus prometteurs, au moins 15° l'un de l'autre; pour chacun:

- traiter 1, station → relief, dans l'espace libre;
- traite 2, relief → correspondant, calculé comme si le relief transmis à partir de 10 m au-dessus du sommet, avec la même prédiction (sans équation parabolique);
- conversion avec l'équation radar bistatique, avec ε = ε0·A, ε0 = −15 dB (montagne en VHF/UHF) et A la zone éclairée par le faisceau R1 (maximum 20 km2):

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

La ligne « Point recommandé » indique l'azimut de la meilleure route, si elle coûte moins que la ligne directe. À 144 MHz, avec un relief à 20 km et à 15° yagi, la route de montagne coûte environ 45 dB de plus que la route 2 en soi: elle ne se produit que lorsque le direct est fermé par des obstacles voisins. L'estimation est brute (±10 dB), en particulier pour ε0.


Sans perte pour la montagne (option, à partir de 1.3.2). Avec la boîte «Via monte senza perdite fino al monte», la chute entre la station et le point de diffraction est considérée comme nulle: c'est comme porter la station au sommet (antenne à 10 m au-dessus du point, même puissance, antenne et câble). L'horizon choisit alors le point avec l'horizon le plus libre vers le correspondant (à parité, le plus élevé) et le résultat montre la route du point vers le correspondant, avec signal et S/N, à côté du total avec la diffusion. C'est une limite optimiste: dit si la direction s'ouvre lorsque la montagne élève bien le signal, ou où aller portable. Chaque point est donné avec le dix caractères.

## 10 ter. Entrée d'un conduit éloigné

Si le conduit qui piège la bande commence loin, au-delà de l'horizon de la station, le signal doit arriver. Le programme amène le signal au point de départ, au milieu du conduit, pour la diffraction: le long du profil Copernic GLO-30 recherche le bord avec le paramètre Fresnel le plus élevé (ITU-R P.526), avec le gonflement de la terre équivalente,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

plus l'atténuation due à la courbure du sommet (obstacle arrondi, P.526-16 § 4.2), avec le rayon R estimé du sol Copernicus GLO-30 à ±1 km du bord,

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

(pour m·n > 4 est le formulaire 34b de la recommandation). Barsis (NBS, 1963) mesurée sur trois longues routes au-delà d'une montagne (Colorado, Alaska, Europe) pertes moyennes 10-20 dB plus haut que le bord idéal de Fresnel-Kirchhoff: l'arrondi explique beaucoup (Barsis et Kirby, 1961: Pikes Peak avec un rayon d'environ 11,5 km). Le résultat se compare à la diffraction sur un sol sphérique lisse; c'est le pire. Puis le signal se déplace dans le conduit: espace libre sur l'ensemble du parcours, atténuation résiduelle γd de P.452 sur la section dans le conduit et couplage du correspondant (3 dB à l'intérieur, 1 dB tous les 10 m à l'extérieur). Si cette perte est inférieure à la perte attendue, elle devient le mécanisme ("conduit (input by diffraction)"). C'est la prémisse de l'auteur sur la géographie italienne: avec un obstacle proche bien en dessous de la ligne, v est négatif et l'entrée coûte peu; avec un front dorsale coûte 15-30 dB.

## 10 quater. ITM (Longley-Rice) et sortie du conduit

De 1.3.2 le programme contient le modèle de terrain irrégulier 1.2.2 de NTIA/ITS (Longley et Rice, ESSA ERL 79-ITS 67, 1968; Hufford, Longley et Kissick, NTIA 82-100, 1982), traduit en C# par le code officiel du gouvernement américain, qui est exempt de droits. La traduction donne les mêmes nombres que l'original: la compilation automatique la compare à chaque fois avec le code NTIA C++ sur cinq cas de test (moins de 0,01 dB).

L'IMT calcule la perte dans l'atmosphère normale sur le profil réel Copernic GLO-30: vu avec réflexion du sol, diffraction (spigol et terre sphérique pesée sur la rugosité du sol Δh, c'est-à-dire la différence entre le 10ème et 90° percentile des quotas autour de la droite des carrés minimums) et le troposcatter. La réactivité au sol est réduite à la portion moyenne de la route, $N_s = N_0\,e^{-h/9460}$, et donne la courbure de la terre équivalente,

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

Le programme utilise le climat radio de la route choisie comme dans § 10 sexies (jusqu'à 1.3.4 toujours la mer tempérée, avec N0 320 sur la terre et 350 sur la mer), avec N0 de la réactivité moyenne du lieu (sol εr 15, ε 0,005 S/m; mer εr 81, ε 5 S/m), la polarisation horizontale et la variabilité au fil du temps sans celle du lieu (le profil est connu). Il montre la perte dépassée dans 50% du temps et celui que vous avez dans 10% et 1% du meilleur temps. ITM ne modélise pas les canaux: sa variabilité dans le temps est statistique, par climat. Il sert de deuxième calcul de la base de référence à côté de la P.452; la différence entre les deux donne une idée d'incertitude.

La seconde utilisation est l'étirement final après un conduit qui se termine avant celui correspondant. Au point de sortie, une antenne virtuelle est placée à mi-chemin du conduit et l'IMT calcule le tube à celui correspondant, en excès sur l'espace libre à 50% du temps. La perte totale est

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,	heta_{condotto} + A_{ITM}(d_{uscita} 	o RX)
```

où A_TX est le couplage de l'antenne (3 dB à l'intérieur, 1 dB tous les 10 m à l'extérieur) si le conduit commence près, sinon la diffraction d'entrée du § 10 ter. C'est la limite que l'auteur s'est donnée: la continuité de la couche plus, au maximum, l'atténuation résiduelle. Si elle est inférieure à la perte attendue, elle devient le mécanisme ("conduite (sortie et étirement final ITM)").

## 10 quinquies. Approfondissement: îles, détroits et discontinuité terre-mer

Un exemple du golfe de Naples (Anacapri, JN70CN) vers la Méditerranée occidentale montre où les routes traversent les îles:

| Destination | Azimut | Où il traverse les îles | Obstacle |
| --- | --- | --- | --- |
| Barcelone |  279°  | au nord de la Sardaigne (Gallura) à 41,05° N, entre 420 et 490 km | environ 30 km au sud de la Bocche di Bonifacio; la Limbara (1359 m) à environ 20 km de la ligne |
| Bocche di Bonifacio |  282°  | près de 41,3° N | environ 11 km |
| Marseille |  296°  | Corse centrale à 42,2° N | montagne jusqu'à 2700 m: un conduit de moins de 2 km il s'interrompt |
| Valence, Baléares |  260–265°  | Centre de la Sardaigne | des secours internes |

Pour Barcelone il suffit de pointer à 282° au lieu de 279° parce que l'axe passe de la bouche: 3ème, à l'intérieur du faisceau d'un yagi normal.

La bouche est un trait? Pour les conduits bas, sous quelques centaines de mètres, oui: les îles sont murées, et au-dessus de la terre, dans le jour, le chauffage brise l'inversion. Le détroit lui-même est assez large: environ 11 km, contre une première zone de Fresnel, moins d'un kilomètre de largeur de moitié d'une route de 1000 km en 144 MHz. Le problème, c'est la météo. Les bouches sont un plaisir pour le vent: La couche inférieure est accélérée et la couche inférieure est enlevée. Souvent le passage est ouvert comme géométrie mais perturbé comme atmosphère. Avec un anticyclone stable et peu de vent peut plutôt être le bon passage.

La disparité terre-mer. Ils comptent. Un conduit formé sur la côte Tyrrhénienne est interrompu: le signal sort du conduit (la section après la sortie, § 10 quater) et doit retourner à un autre conduit sur la mer des Baléares, payant à nouveau le couplage. Les conduits à altitude, au-dessus de 1500 m, passent au-dessus de la Gallura sans la réaliser; car le centre de la Corse ne s'applique pas.

Dans la pratique: si la route traverse une île montagneuse, essayez un certain degré de correction vers l'étendue de la mer libre, mais ne comptez pas sur le chemin avec vent fort dans l'étroit.

## 10 sexies. TN101 radio climat et variabilité du temps (à partir de 1.3.5)

Le TN101 § 10 [25] décrit comment la perte d'une route au-delà de l'horizon varie sur la longue période autour de la médiane, avec différentes courbes par climat: la fonction Y(q) indique combien de dB la perte diminue dans le q% du meilleur temps. L'IMC contient ces courbes pour sept climats. Le climat compte beaucoup: sur la mer dans le climat marin tempéré, à 144 MHz, dans 10% du temps vous gagnez beaucoup plus de dB que sur la terre dans le climat continental, parce que les canaux sont plus fréquents.

Le programme ne fixe plus le climat: Il le choisit pour chaque trait à partir du point moyen, donc il fonctionne partout dans le monde et pas seulement en Europe. Télécharger depuis Open-Meteo [26] trois ans de valeurs quotidiennes de la réanalyse ERA5 (température, humidité et pression du sol, pluie) pour la cellule de 0,5° du point moyen, une seule fois; les valeurs restent dans Data/CLIMA. Restauration des sols

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

jour par jour, puis la moyenne annuelle et l'excursion entre les moyennes mensuelles, l'humidité moyenne et la pluie annuelle. Les règles suivent les descriptions des climats du TN101 § 10.2; les limites entre un climat et l'autre sont un critère de l'auteur:

- plus de 60° de latitude: continental tempéré (le TN101 n'a pas de courbes pour le climat polaire);
- moins de 250 mm de pluie par an et humidité moyenne inférieure à 50%: désert (Ns typique 280);
- à 20° près de l'équateur, Ns d'au moins 350 et excursion jusqu'à 30: équatorial (Ns typique 360);
- dans les 32° et excursion d'au moins 60: continental subtropical, c'est-à-dire moussons (excursion typique 60–100);
- entre 10° et 32° avec Ns d'au moins 340: marine subtropicale (Ns typique 370);
- les deux horizons sur la mer, à plus de 20°: marin tempéré sur la mer (c'est la définition du TN101);
- excursion jusqu'à 30 près de la mer (mer sur la route ou humidité moyenne d'au moins 75%): mer tempérée sur terre;
- autrement, le climat continental est tempéré (excursion typique 20–40).

N0 pour ITM est la moyenne réduite au niveau de la mer, N0 = Ns·e^(h/9460). Sans réseau, vous utilisez une règle de réserve avec la latitude seule et la fraction de la mer, et la ligne le dit.

Essais avec données ERA5 2023-2025: Capri Ns 339 avec excursion 45 (continental tempéré sur terre, mer tempérée sur la mer lorsque les deux horizons sont sur la mer); Londres 327 et 16 Vienne 319 et 24 Abidjan 381 et 11 (équatorial); Miami 365 et 36 (mari subtropical); Delhi 337 et 82 (concontinental subtropical); Riad 278 et 37, pluie de 90 mm (désert). Au Chili: Antofagasta 341 et 19 (mari subtropical: la côte de l'Atacama, avec l'inverse presque bas du Pacifique), Calama 232 au sol, 2257 m, 14 mm de pluie (désert), Valparaíso 331 et 11 (semelle de température), Punta Arenas 313 et 5 (semelle de température).

La ligne « Radio climat ( TN101) » montre le climat, la raison, Ns avec la moitié de l'excursion et Y: combien de dB gagnent en 10% et 1% du temps et combien perdent en 90%. Il s'agit de statistiques à long terme et reste séparé de la prévision: la perte attendue suit le temps d'aujourd'hui avec les profils GFS, et la somme des deux choses compterait deux fois la même propagation anormale.

## 11. Comparaison avec l'UIT-R P.452

La ligne P.452 applique la méthode de l'UIT pour les conduits et la réflexion sur les couches (P.452-16 § 4.5 [6]) avec la médiane ΔN du GFS au lieu du climatologique et p = 20% comme constante d'étalonnage, comme dans l'étude CSIRO [10]. Il ne s'agit que d'une comparaison: la perte attendue demeure celle du paragraphe 9.

## 11 bis. équation parabolique (à partir de 1.3.1)

La ligne d'équation parabolique est un quatrième calcul, seulement de comparaison, fait comme des programmes professionnels pour les conduits (PETOOL; le cadre de propagation d'onde de M. Lytaev, licence MIT): au lieu d'estimer le gain des couches, il avance la vague le long de la route avec la méthode de fractionnement de Fourier dans le profil réfractaire modifié GFS M, pris du point de prévision plus près de chaque distance. Les niveaux de GFS ajoutent les sauts estimés des conduits potentiels (calculant A, 50 m en demi-couche). Le sol Copernicus GLO-30 entre en marche (sous le sol le champ est nul), le sol est conducteur et le faisceau de l'antenne est un gaussien avec l'ouverture estimée du gain. Le calcul voit les conduits, ceux qui perdent, la diffraction et le couplage des antennes; Il ne voit pas le troposcatter, qui est ajouté à part. Ensuite, le «chèque de profil»: le même avec atmosphère standard moins celui avec le profil attendu.

À partir de 1.3.2:

- le propulseur est à large angle, avec la phase exacte de l'espace libre:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- le fond de la mer et la plaine côtière à zéro altitude se reflètent avec le coefficient de Fresnel en polarisation horizontale, εc = εr − j·60·λ·ε (mer εr = 81 et ε = 5 S/m; pays 13 et 0,006 S/m), à l'angle clair de l'itinéraire:

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- la rugosité de la mer réduit la réflexion constante selon Miller et Brown, avec la hauteur effective des vagues du vent à 10 m (εh = 0,0051·U2):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- sur la mer le profil M comprend le conduit d'évaporation (paragraphe 9 ter) avec le profil logarithmique neutre:

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

Il teste avec le prototype (10 m d'antenne sur la mer, gagne sur l'atmosphère standard de 100 à 300 km): avec γ = De 10 à 30 m, le conduit d'évaporation donne de 1 à 3 dB à 144 MHz, de 8 à 37 dB à 432 MHz et de 21 à 66 dB à 1296 MHz. À 144 MHz en polarisation horizontale, la mer se reflète presque comme un métal (=>> plus de 0,99 à quelques dixièmes de degré) et la rugosité compte peu: le nouveau fond change quelques dixièmes de dB; il compte plus de 432 MHz en hausse.

Limites: au-dessus de la terre, le profil vertical reste celui des niveaux GFS, de sorte que les canaux minces restent invisibles; le sol aux marches est approximatif; le faisceau reste dans la 6ème.

## 12. antennes, signal et bruit

- Yagi DL6WU: gain 9,5 + 7,8·log10(boom/λ) dBd (2,35 dB pour le double boom, mesures DL6WU sur les communications VHF 3/1982), plus 2,15 pour [12] dBi.
- EIRP = puissance − câble + gain TX.
- Signal reçu = PREI − perte attendue + gain RX − câble RX. IARU S-mètre: S9 = −93 dBm, 6 dB par unité.
- Bruit extérieur selon UIT-R P.372 [13]: galactique (Fa = 52 - 23 log f) plus artificiel selon l'environnement (ville, résidentiel, rural, rural calme) ou une température d'antenne manuscrite.
- Tsys = Ta + Tpre + [290(Lc − 1) + 290(Fr − 1)·Lc] / Gpre aux bornes de l'antenne (formule Friis). A partir de 1.3.5 la station de réception a toujours un préamplificateur en antenne, paramètre fixe: numéro de bruit 0,8 dB (Tpre = 59 K) et gagne 20 dB. Derrière il y a le câble (perte Lc) et la radio, avec le numéro de bruit de base Fr 6 dB. Avec 1 dB de câble, le câble et l'entrée radio passent d'environ 1160 K à 12 K: le bruit décide du ciel, de l'environnement et du préampli. Le gain de 20 dB est une valeur typique choisie par le programme.
- S/N dans SSB, CW et FT8 dans la bande de chaque façon, par rapport au seuil fixé.
- Probabilité pour la période (à partir de 1.3.2): l'évanescence dans l'heure est une distribution normale du S/N autour de la valeur horaire moyenne prévue. Bray (QST, novembre 1961, p. 39, sur les courbes de Norton 1959) donne une amplitude de 13,5 dB sur environ 100 milles, c'est-à-dire environ 7 dB en dessous et 7 au-dessus de la moyenne, qui tombe presque tout droit sous 100 milles (3,5 dB à 50 milles). Prise ±7 dB comme ±2ε: ε = 3,4 dB sur 160 km, proportionnelle à la distance ci-dessous. La probabilité qu'une période dépasse le seuil de la voie est

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  avec fonction de distribution normale Φ: marge 0 dB = 50%, +3,4 dB = 84%, +7 dB (la marge 'solide' de Bray) = 98 %. Il n'inclut pas l'incertitude du modèle, qui est plus grand.
- Voies numériques: S/N en 2500 Hz comparé aux seuils mesurés à 50 % de décodage, sans information préalable (AP). FT8 −21 dB et FT4 −17,5 dB sur le bruit blanc; lorsque le mécanisme est troposcatter, utiliser les seuils avec évanescence "milieu de latitude perturbée", FT8 −18,6 dB et FT4 −15,2 dB [21]. Q65 −15C −22,2 dB, Q65 −30B −24,8 dB, Q65 −60A −27,6 dB Dans les avions, la dispersion du Doppler peut dépasser les tons de Q65 -60A (1,7 Hz): il y a mieux que 30B ou 15C. JT65A -25 dB [22]. Le décodage AP (nom du déjà connu ou de la fin de QSO) donne environ 2 dB supplémentaires pour FT8 et 1,5 à 2,6 dB pour Q65; il n'entre pas le seuil parce qu'il n'est pas valide pour toutes Les valeurs «−24 dB» qui sont souvent lues pour FT8 sont la limite des relations WSJT-X ou du décodage AP, et non le seuil à 50 %. [24]

## 13. Temps de réponse et de données

Chaque calcul mesure son propre temps: profils GFS (réseau, analyse, ruptures obligatoires de 20 s entre les lots pour la limite Open-Meteo), terrain GLO-30 de Copernicus (avec le téléchargement des vitres, 15 à 30 Mo chacun, seulement la première fois), calcul. La ligne de temps de réponse les résume; dans le registre, les lignes commencent par TEMPI. Lors de la mise à niveau de la grille, le registre indique également la course GFS utilisée, quand elle est devenue disponible et l'âge des données.

A partir de 1.3.2 vous pouvez également choisir ICON-2I, le modèle de l'Agence ItaliaMeteo et Arpae sur l'Italie à environ 2,2 km, à travers Open-Meteo (modèle italia_meteo_arpae_icon_2i, deux courses par jour, 3 jours). Il ne couvre que l'Italie et les mers voisines et a des niveaux moins bas du GFS (1000, 925, 850, 700, 500 hPa): pour les conduits à altitude n'est pas mieux que le GFS, mais la fine grille décrit les côtes de puits, les îles et la mer à 2 m, c'est-à-dire le conduit d'évaporation. Les variables qu'un modèle n'a pas (limite de strate, température de la surface...) se retirent de la demande. À partir de 1.3.2 la source automatique utilise uniquement NOMADS; Open-Meteo sert si NOMADS ne répond pas et pour les cas historiques. Avec la boîte "Mise à jour sur son propre" (illuminé par défaut) la prévision sera téléchargée sans clic: à l'ouverture, à chaque nouvelle course GFS sur NOMADS (environ 5 heures après le temps de la course) et lorsque la zone visible est hors de la grille pour plus d'un quart; au maximum une tentative toutes les 15 minutes, jamais avec un cas historique.

Les demandes à Open-Meteo ont des limites par minute, par heure et par jour pour chaque adresse Internet. Les profils téléchargés sont enregistrés sur le disque (Dati/cache-gfs) pour chaque course et modèle: avec la même course, qui change toutes les 6 heures, les points déjà téléchargés ne redemandent pas. Avec la source automatique, vous démarrez à partir de NOMADS et Open-Meteo ne servir que pour les points manquants; avec le seul Open-Meteo, à la limite de temps ou s'arrête immédiatement tous les jours et indique quand essayer à nouveau. Les cas historiques ne sont pas mis en cache.

## 14. Valider le modèle

Les prévisions devraient être comparées aux connexions réelles. Le journal de calcul conserve chaque numéro; l'archive du programme conserve les annonces et les décodages. Pour 432 et 1296 MHz, où les spots sont peu nombreux, ils ont besoin de rapports des opérateurs: qui a fait la connexion, à quelle heure, avec quel signal. Comparez pendant plusieurs heures le gain attendu avec le signal mesuré indique si les seuils doivent être déplacés.

## Sources

1. NOAA/NCEP, Global Forecast System (GFS), modèle mondial à 0,25°, quatre courses par jour.
2. Open-Meteo, API de prédiction (open-meteo.com), modèle gfs_semless, variables aux niveaux de pression et métadonnées de course.
3. Copernicus DEM GLO-30 (ESA; WorldDEM © DLR e.V. 2010-2014 e © Airbus Defence and Space GmbH 2014-2018, fourni dans le cadre de Copernicus par l'Union européenne et l'ESA), 1 deuxième arc, cadres GeoTIFF sur le registre ouvert des services web Amazon. Réserve: NASA/USGS Copernicus GLO-30 en Terrain Tiles "skadi" de l'AWS.
4. UIT-R P.453, L'indice de réfraction radio: sa formule et sa date de réfraction, avec des cartes de canaux numériques (Study Group 3, © UIT, utilisation personnelle et non commerciale).
5. B. R. Bean, E. J. Dutton, Radio Météorologie, NBS Monographie 92, 1966: Tableau 6.5 et § 4.4.
6. UIT-R P.452-16, Procédures de prévision pour l'évaluation des interférences entre stations à la surface de la Terre à des fréquences supérieures à 0,1 GHz environ: § 4.2 (diffraction), eq. 45 (troposcatter), § 4.5 et eq. 49 (conduit).
7. UIT-R P.834, Effets de la réfraction troposphérique sur la propagation des ondes radio.
8. UIT-R P.525, Calcul de l'atténuation de l'espace libre.
9. UIT-R P.526, Propagation par diffraction: eq. 31 (cutcheon) et § 3 (terre sphérique).
10. B. Indermuehle, H. Suzuki, Prédiction de la dérivation troposphérique basée sur les données du modèle GFS à Inyarrimanha Ilgari Bundara, Observatoire de la radioastronomie de Murchison CSIRO, Publications de la Société astronomique d'Australie, 2026 (arXiv 2608.12861).
11. D. E. Kerr, Propagation of Short Radio Waves, MIT Radiation Laboratory Series vol. 13, 1951: eq. 17 e 18.
12. G. Hoch DL6WU, «Extremely Long Yagi Antennas», VHF Communications 3/1982, p. 130 et suiv., fig. 1 (mesurée à 432 et 1296 MHz selon la longueur de la flèche).
13. UIT-R P.372, Bruit radio: bruit galactique et artificiel pour l'environnement.
14. NOAA/NCEP, NOMADS, filtre GRIB de la GFS 0.25° temps (nomads.ncep.noa.gov); format OMM G RIB2, emballage complexe avec différences spatiales.
15. Open-Meteo, API de prévisions historiques (archive de prévisions modèles depuis environ 2021).
16. LukiBox, TropoLink, conception de routes de troposcatter avec P.617, NBS TN101 et ITM flanqué (GitHub, licence Apache 2.0).
17. UIT-R P.617-5, Techniques de prédiction de propagation et données nécessaires à la conception des systèmes de relais radio transhorizontaux, 2019.
18. RSGB, Manuel VHF/UHF; mise en œuvre DL2FZN (andi-f/troposcatter sur GitHub, 2013).
19. W. F. Moler, D. B. Holden, «Prolifération de la dispersion troposphérique et circulation atmosphérique», Journal of Research of the NBS 64D, 1960, doi 10.6028/jres.064d.014: signal au-delà de l'horizon (1365 et 9380 MHz, sur la mer) et mouvement vertical de l'air.
20. Manuel ARRL 2022 (édition 99a), § 19.4.3-19.4.4: temps et propagation troposphérique.
21. S. Franke, B. Somerville, J. Taylor, Les protocoles de communication FT4 et FT8, QEX Juillet/août 2020.
22. W3SZ, seuils de modes numériques lents ( JT65, JT9, QRA64) et choix du mode selon la propagation, 2019.
23. QST, « Le monde au-dessus de 50 MHz », novembre 2022: ouverture du 14 au 15 août 2022 entre le Manitoba et le Kansas/Missouri.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Guide de démarrage rapide de Q65, 3 avril 2021 (wsjt.sourceforge.io).
25. P. L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis, Transmission Loss Predictions for Tropospheric Communication Circuits, NBS Technical Note 101, magazine 1967: § 9 (troposcatter, fonction F(γd), H0), § 10 et Annexe III (climats radio et variabilité dans le temps).
26. Open-Meteo, Historical Weather API (ECMWF ERA5 review depuis 1940; Copernicus Climate Change Service, CC BY 4.0 licence).
