LIVETROPOVHF · document technique 3
# Comment former le trope en 144 MHz et au-delà

> Traduction automatique de l'italien : en cas de doute, le guide italien fait foi.

Ce qui se passe dans l'air quand les 144, 432 et micro-ondes vont au-delà de l'horizon: réfraction, inversions, canaux, dispersions, et comment les lire sur la carte du programme. Version du 9 novembre 2026.

## En résumé

- Les ondes VHF et au-delà voyagent dans les premiers kilomètres de l'atmosphère (troposphère) et plient où l'indice de réfraction change avec le [3] [7] [11].
- L'indice est mesuré avec du N réfractaire, qui dépend de la pression, de la température et surtout de l'humidité [11].
- Dans l'atmosphère normale, il y a environ 40 unités par km; lorsqu'il tombe plus de 157 unités par km, la vague reste piégée dans un conduit [7] [11].
- Le conduit provient d'une inversion thermique avec de l'air sec au-dessus et humide au-dessous: anticyclone, mer, nuits sereines, [3] [4] [5] [13] fronts.
- Plus la fréquence est élevée, plus le conduit est mince: les micro-ondes s'ouvrent plus souvent que le 144 [3] [7].
- Sans conduit reste la dispersion troposphérique: toujours présent, faible, qui tombe avec la distance [6].
- Au programme: haute pression + inverse + Hepburn/ F5LEN haute = conditions probables; les taches confirment [1] [2] [13] [14].

## 1. Où cela se produit: la troposphère

La troposphère est la couche la plus basse de l'atmosphère, jusqu'à environ 10-12 km. Pour le compte de trope principalement les 3 premiers km: il y a la vapeur d'eau et les inversions, et il passe les routes radio entre les stations sur le sol [4]. Dans des conditions normales, la température chute avec une altitude d'environ 6,5 °C par km [3]. Lorsque, par contre, dans une couche, la température monte avec l'altitude, on parle d'inversion thermique: c'est le point de départ de presque toutes les ouvertures de tropo fortes.

Les VHF ne sont pas reflétés par l'ionosphère comme le HF (sauvez et sporadique, qui a d'autres heures et d'autres distances [5]): au-delà de l'horizon vient seulement si l'atmosphère les plient au sol ou les propage.

## 2. Réfraction et réfraction N

Une onde radio se penche dans l'air avec un indice de réfraction de l'air avec un autre. L'indice n est très proche de 1, donc la réactivité N est utilisée, c'est-à-dire n - 1 multiplié par un million [11]:

```math
N = (n - 1) \cdot 10^{6} = \frac{77,6}{T} \left( P + 4810 \, \frac{e}{T} \right)
```

avec P la pression dans l'hPa, T la température dans le kelvin et la pression de la vapeur d'eau dans l'hPa. Le sol en N est généralement de 300 à 400. Le second terme, celui de la vapeur, pèse beaucoup: pour cette raison, l'humidité compte autant que la température [3] [11].

Ce qui incline la vague n'est pas la valeur de N, mais comment elle change avec le quota, le gradient dN/dh:

| dN/dh (N/km) | Situation actuelle | Effet sur la vague |
| 0 | Sous-fraction | La vague s'éloigne du sol, l'horizon plus court |
| de 0 à −79 | Normal (standard −39) | Légère flexion au sol: horizon radio environ 4/3 de l'optique |
| de −79 à −157 | Superréfraction | Les vagues se plient davantage, augmentant les débits |
| inférieure à −157 | Contenu | La vague fléchit autant que la Terre ou plus et reste piégée |

Le seuil de −157 N/km est le point où la courbure du rayon est égale à celle de la Terre [7] [11]. D'où le "faisceau terrestre équivalent" de 8497 km (4/3 du rayon réel) utilisé dans le calcul de l'Éparpillement [6].

## 3. Inversions: le moteur de tropo

Un gradient inférieur à −157 N/km exige que N s'effondre en quelques mètres d'altitude. Il se produit quand au-dessus d'une couche d'air frais et humide se trouve plus chaud et sec: la température augmente et la vapeur tombe ensemble, et les deux faire descendre N [3] [5]. LA0BY indique des inversions allant jusqu'à 10-15 °C par km contre la diminution normale de 6,5 °C [3].

Les inversions se produisent de différentes manières:

- Subsidence dans les anticyclones. En haute pression, l'air à l'altitude baisse lentement, il se compresse et se réchauffe, tandis que sur le sol reste l'air frais et humide. C'est la cause des ouvertures plus longues et plus longues, qui durent les jours [3] [4] [13].
- Rayonnement nocturne. Avec le ciel serein et le vent faible, le sol refroidit la nuit et refroidit l'air de contact: Bas revers qui rompt avec le soleil du matin. Ouvertures du soir et du matin, typiques de l'été et de l'automne.
- Avertissement maritime. Air chaud et sec provenant du sol au-dessus d'une mer plus froide: la couche de mer reste humide et fraîche en dessous. D'ici les grandes routes maritimes (Atlantique, Méditerranée, Mer du Nord) [4] [13].
- Les fronts. Devant un front chaud, l'air chaud coule sur le front froid; l'ouverture est courte et suit l'avant.
- Évaporation. Au-dessus de la mer, dans les premiers mètres, l'humidité diminue rapidement avec l'altitude: conduit très mince, utile uniquement aux micro-ondes.

Le signe qu'un inverse est vu dans les profils verticaux (radiosondaggi): la température montante et le point de rosée descendant dans le même [5] stretch. Le programme montre un indice de ceci avec la couche d'inversion thermique (air à 925 hPa plus chaud que sur le sol) et avec l'isotherme à 850 hPa.

## 4. Voies de surface et voies hautes

Un conduit est une couche dans laquelle l'onde se déplace en rebondissant entre le bord supérieur (inversion) et le bord inférieur ou sol [7]:

- Pipeline de surface: sol ou mer. Les stations de basse altitude, surtout sur la côte, entrent directement.
- Pipeline haute: se trouve à l'altitude, par exemple entre 500 et 2000 m. Les stations dans les plaines peuvent ne pas sentir, tandis que ceux dans la colline ou dans la montagne entrent dans le conduit. Expliquer les liens qui traversent les Alpes ou d'autres reliefs [3] [4].

Pour entrer dans le conduit, l'onde devrait démarrer presque horizontalement: des antennes basses à l'horizon et aucun obstacle voisin. À l'intérieur du conduit l'atténuation est beaucoup plus faible que dans l'espace libre, et les distances atteignent 1000 à 2000 km et au-delà (par exemple les liaisons entre la péninsule ibérique, les îles Canaries et l'Angleterre, ou entre la Scandinavie et le sud de la Russie) [3].

## 5. Fréquence: pourquoi les micro-ondes s'ouvrent plus souvent

Un conduit ne piège que des vagues assez courtes par rapport à son épaisseur. Les vagues longues sortent d'un canal mince; les petites ne sortent pas. Puis [3] [7]:

| Bande | Longueur d'onde | De quoi avez-vous besoin? |
| 144 MHz |  2 m  | Pipelines épaisses (centinaïa de mètres), inversions larges et bien marquées: moins fréquentes mais très étendues |
| 432 MHz | 70 cm | Plus minces pipelines: s'ouvre souvent quand 144 sont seulement -goods |
| 1296 MHz | 23 cm | Peu d'inversions faibles; de longues routes sur la mer sont fréquentes en été |
| 10 GHz | 3 cm | Juste le conduit d'évaporation sur la mer; compte aussi l'absorption de la pluie |

Pour cette raison, un indice de tropo modeste peut déjà signifier des ouvertures en UHF et micro-ondes, tandis que les 144 appellent des valeurs plus élevées. D'autre part, dans le même conduit, les 144 ont moins de pertes à l'extérieur du conduit et mieux couvrir les zones frontalières.

## 6. Le Troposfer se disperse

Même sans inversion, la troposphère n'est pas uniforme: De petites irrégularités de température et d'humidité répandent une petite partie de l'énergie au-delà de l'horizon. C'est la dispersion troposphérique, toujours présente, qui permet des connexions de 300 à 800 km avec de bonnes stations [6]. Le signal est faible et fluctuant; la perte augmente avec l'angle de diffusion, c'est-à-dire avec la distance et les obstacles à l'horizon:

```math
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
```

avec l'angle de diffusion en degrés et f en MHz [6]. Le programme utilise cette formule dans l'onglet de chaque endroit pour savoir si un lien peut être expliqué avec la diffusion ou nécessite un conduit (voir document 1). Une valeur N élevée sur le sol améliore la diffusion [6].

À ne pas confondre avec la dispersion de l'air (réflexion sur les plans: signaux de quelques secondes, Doppler) et le météore (réflexion sur les météores), que le programme exclut du tropo.

## 7. La météo: comment la lire sur la carte

Les ouvertures du tropo suivent la météorologie. Situations typiques [3] [4] [13]:

- Anticyclone stable, haute pression (plus de 1020-1025 hPa), large isobare, peu de vent: renversement de la subsidence. Les meilleures ouvertures suivent le bord de l'anticyclone, surtout sur la mer.
- Calo della pressione: ruptures d'inversion. G4MCU a suivi la balise SK6VHF jusqu'à ce que la pression tombe à 1012 hPa [13].
- Air chaud à l'altitude (isotherme à 850 hPa en montée) au-dessus de l'air frais au sol: inversement.
- Les cellules convectifs et les ondes de gravité (par exemple sous le vent dans les Alpes) peuvent créer des ouvertures locales et courtes [4].
- Heure de la journée: les inversions de l'irradiation sont plus fortes le soir et tôt le matin; la subsidence reste aussi du jour.

Les indices Hepburn [1] et F5LEN [2] ont rassemblé tout cela à partir de modèles météorologiques: Ils calculent l'azote à divers quotas et transforment les gradients en un nombre ou une classe. Je ne suis pas une garantie: LA0BY montre un événement LA– UA6 visible à haute température et non dans Hepburn [3]. Pour cela, le programme montre ensemble des index, des isobares, des températures et des spots réels: si les spots s'alignent sur une zone d'index élevée sous un anticyclone, l'ouverture est en cours. KP4MD a trouvé le SNR des rapports WSPR à 144 MHz croissant à droite dans les fenêtres fournies par Hepburn [14].

## 8. Liste pratique

1. Regardez l'index du tropo sur votre région et le long de la direction qui vous intéresse, pour les prochaines heures.
2. Vérifier l'isobare: haute pression et large isobare sur ou à côté du sentier.
3. Allumez l'inversion thermique et le T 850 hPa: air chaud à l'altitude au-dessus du sentier.
4. Préférez les itinéraires sur la mer, le soir et le matin.
5. Si vous êtes sur la colline, pensez aux conduits hauts; Si vous êtes sur la côte, à la surface.
6. Essayez d'abord les bandes les plus élevées (432, 1296): ouvrez d'abord.
7. Vérifier avec spot et PSKReporter: les longues lignes dans la même direction confirment le conduit.

## Sources

1. W. R. Hepburn, Centre d'information DX, Index du tropo Hepburn (HTI) et pages de prévision de la duplication troposphérique (dxinfocentre.com).
2. F5LEN, prévisions pour l'Europe et prédiction mondiale de la propagation tropophérique VHF (tropo.f5len.org).
3. S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", Réunion Nordic VUSHF 41°, Skjeberg 2019. gradient normal et inversions, température et humidité, pressions élevées, événement LA– UA6, connexions au-delà des montagnes.
4. F. Egano IK3XTV, La propagation VHF – La contribution des cellules convectives et des ondes gravitationnelles atmosphériques dans la propagation du Tropo. Premiers 3 km, chemins marins, cellules convectives, ondes de gravité, haute pression.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, "Propagation très longue distance dans la bande 144 MHz", 24/7/2003. Profils de température et de point de rosée, inversions, distinction avec E sporadique.
6. J. N. Gannaway G3YGF, "Propagation de la dispersion troposphérique", QST Novembre 1983. Rayon de la Terre équivalent et perte de dispersion.
7. Le livre VHF/UHF DX, chapitre 2 - propagation troposphérique. Zones réfractives selon dN/dh et profilés de surface et de gaines élevées (seulement les chiffres).
11. Recommandation UIT-R P.453, indice de réfraction radio. Formule N et seuil du conduit à −157 N/km. Référence générale, non annexée.
13. G4MCU, articles troposphériques (g4mcu.net), 2018-2025. Condotti à haute pression, surtout sur la mer; SK6VHF et diminuer à 1012 hPa.
14. C. F. Milazzo KP4MD, Étude de propagation VHF/UHF WSPR, 2012-2014. SNR WSPR à 144 MHz et prévisions Hepburn.

Les numéros de source sont les mêmes que ceux du document 2, Bâtiment de la carte.
