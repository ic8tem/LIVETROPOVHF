LIVETROPOVHF · document technique 1
# Fonctionnement du programme

> Traduction automatique de l'italien : en cas de doute, le guide italien fait foi.

Ce que le programme fait, d'où il prend les données et comment il les transforme en ce que vous voyez sur la carte et dans la liste. Version du 9 novembre 2026, relative au projet en C# (.NET 10 + Avalonia).

## En résumé

- Objectif: prévision du tropo (Hepburn [3], F5LEN [4], isobare GFS [18]) et observation réelle de bandes dans la même fenêtre, selon la liste de contrôle LA0BY [1].
- Collecte de données: deux clusters DX, le réseau de Beacon inversé [9], le cluster dm4x et le flux MQTT PSKReporter [5], le décodage UDP de WSJT-X, JTDX et MSHV [8].
- Spot: segments de bande, filtre négatif, diffusion d'air en jaune pointillé, position par localisation ou CALL3.TXT [6] [7].
- Qui écoute: des cercles rose foncé avec des lignes roses toujours visibles vers les stations sentaient [5].
- Carte Spot: distance, azimut, index tropo sur le chemin et la perte troposcatter avec la méthode G3YGF [2].
- Langues Hepburn et F5LEN: [3] image et banc d'étalonnage [4].
- Regarder: Contrôle NTP sur 10 serveurs toutes les 3 minutes [10] [11].
- Rapports suspects: EME en Q65/JT65 reconnu avec la position de la Lune, locateurs tronqués, écumers, quarantaine et auditeurs, avec des règles apprises par le soleil [20].
- Critères de rejet et signification des couleurs résumées dans deux tableaux (sections 11 ter et 11 quater).
- Thèmes, 36 Langues [17], exporter des documents vers Word avec des formules modifiables [16], programme portable et multiplateforme [12] [13] [14] [15].

## 1. Objet

Le programme rassemble dans une seule fenêtre deux choses qui regardent habituellement différents sites: la prédiction de la propagation du troposfer (indice Tropo de Hepburn et F5LEN, isobare) et l'observation de ce qui se passe réellement sur les bandes (spot du cluster DX, Reverse Beacon Network et PSKReporter, plus la liste des stations qui écoutent). La bande de référence est de 144 MHz; les taches s'accumulent à partir de 50 MHz.

Le réglage suit la liste de contrôle proposée par LA0BY pour les événements de tropo extrêmes: les jours précédents vous regardez les prévisions Hepburn et F5LEN et les profils de température à l'altitude, tandis que l'événement est sur le cluster DX et sur le spot maps la preuve que l'ouverture commence [1]. LA0BY observe également que les seules cartes Hepburn/ F5LEN ne suffisent pas à prédire les connexions les plus longues [1]

## 2. La fenêtre et les onglets

La fenêtre est une et ne se divise pas en fenêtres mobiles. De haut en bas:

- Paramètres généraux ligne: Appeler et trouver à gauche; à droite l'horloge UTC (par exemple 08/11/2026 20:33:38 UTC, texte ambre) suivie de NTP:OK ou NTP:FAILED, le menu thème et celui de la langue.
- Nombre d'onglets en cinq groupes: Carte et spot; Prévisions (Propria β, la prévision calculée par le programme et décrite dans le Manuel des prévisions, puis Hepburn et F5LEN); GRAVES; Données et sources (cluster DX, PSKReporter, Inverse Beacon, UDP, Rendez-vous et rapports suspects); Guides (Opération du programme, Bâtiment de la carte, Comment façonner le tropo, Prévisions manuelles, Conseils à l'opérateur, avec une boîte de recherche qui compte dans laquelle guide le texte apparaît). Sous le faisceau, une barre indique les sous-langues du groupe; chaque groupe se souvient de la dernière utilisation.
- Contenu de l'onglet et, en bas, la barre d'état avec l'état des sources, la base de données des noms et l'heure de la dernière mise à jour.

Les paramètres de la ligne supérieure s'appliquent à tous les onglets: le thème choisi colore également la collecte de données et l'arrière-plan des documents; les pages de document restent aussi blanches qu'une feuille, donc elles s'impriment bien.

## 3. Appel et propriétaire

Le propriétaire de Maidenhead peut avoir 4 ou 6 caractères (par exemple JN70 ou JN70CN). Le programme le convertit aux coordonnées du centre du carré: champ de 20° × 10°, carré de 2° × 1er, sous-quadrate de 5′ × 2,5′. Si le texte n'est pas un localisateur valide, le bord de la case devient rouge et la station n'apparaît pas sur la carte. Le localisateur sert également de centre de la projection d'azimut et de point de départ pour les distances et l'azimut dans le panneau publicitaire.

L'Appel est le nom utilisé comme identifiant de cluster. Au premier départ, jusqu'à ce qu'il y ait une nomination valide, les clusters ne se connectent pas: une fenêtre demande le nom (et, en option, le propriétaire); confirmé, le nom est copié comme un nom d'utilisateur dans le champ Connexion de chaque cluster qui l'a vide et les clusters partent. Avec le bouton « Later » reste immobile, et le bouton « Connect » de chaque cluster nécessite une nouvelle nomination. MQTT, UDP et NTP n'utilisent pas la connexion et partent de toute façon.

## 4. Collecte de données: sources

Chaque source a sa propre section dans l'onglet Collecte de données, avec Connected/Disconnected, les boîtes reconnues et connectées au démarrage, statut, compteurs et registre des lignes reçues. Les lignes débandées ou éliminées du filtre n'apparaissent pas dans le registre: comptez seulement les compteurs de section. Les sections sont côte à côte avec deux ou deux.

| Source | Connexion | Ce qui nous amène |
| 1. Choix du cluster DX | sur0dxk.dyndns.org:8000 commande set/filter dxbm/pass 6 | Spot de radios amateurs, aussi SSB et CW. Sur ON0DXK, le VHF est filtré et activé avec la commande indiquée (poi pass 4, 2...). |
| 2. Deuxième source | dxmaps.com:7300 (DXMAPS, EA6VQ) et s50clx.si:41112 ( S50CLX, Slovénie) | DXMAPS collecte des spots VHF et donne également des spots d'échoueur RBN (réglage de jeu/piscine, déjà défini); S50CLX est un cluster DX Spider. Connectez-vous avec votre nom; adresses modifiables. |
| 3. Réseau de balises inversées | telnet.reversebeacon.net 7000 CW/RTTY · 7001 FT8 | Spot d'écume automatique. Connexion avec le nom uniquement [9]. |
| 4. Cluster avec PSKReporter | dm4x.ddns.net:8500 | Points numériques de PSKReporter au format cluster. |
| 5. PSKReporter MQTT | mqtt.pskreporter.info 1883 · 1884 avec TLS | Chaque rapport de réception en temps réel: qui transmet, qui écoute et leurs points [5]. Bandes 144, 222 et 432 MHz. |
| 6. Cartes météo et tropo | dxinfocentre.com/tropo_eur.html tropo.f5len.org/for-European modèle GFS | Hepburn [3] et F5LEN [4] Tropo index maps, pression au niveau de la mer pour isobare (GFS via Open-Meteo [18]). Hepburn et F5LEN doivent être autorisés aux auteurs. |

Cluster telnet (sources 1–4). Après la connexion, le programme attend la demande de nom (texte qui contient connexion, appel:, callsign ou votre appel); s'il n'arrive pas dans les 4 secondes, veuillez envoyer la connexion. Envoyez ensuite les commandes de la section, séparées de point et virgule, une toutes les 0,6 secondes. Les caractères de contrôle telnet sont supprimés avant de lire les lignes. Si la connexion tombe et que Riconnetti est active, réessayez après 10 secondes (15 si le serveur ne répond pas du tout) [6].

PSKReporter MQTT (source 5). Le programme s'inscrit dans le sujet pskr/filter/v2/{banda}/{modo}/{tx_call}/{rx_call}/{tx_grid}/{rx_grid}/{tx_dxcc}/{rx_dxcc} fixer uniquement la bande et laisser les autres champs libres avec le caractère + [5]. Le nom exact avec lequel le serveur indique les bandes (par exemple 2m, 1,25m, 70cm) doit être vérifié lors de la première connexion.

## 5. De la ligne de cluster au spot

Une ligne est un spot quand il a la forme DX de SPOTTER: fréquence DX comment hhmmZ. Le programme dérive spotter, station DX, fréquence en kHz, commentaire et maintenant UTC; les noms enlèvent les suffixes d'écume (-#, -1...). Puis appliquer, dans cet ordre, les règles suivantes [6].

| Bande | Débat accepté |
| GRAVES (avec 144) | 143,040 – 143,060 MHz |
| 144 MHz | 144 – 148 MHz |
| 222 MHz | 220 – 225 MHz (seulement Région 2 UIT, Amériques) |
| 432 MHz | 420 – 450 MHz |
| 1296 MHz | 1240 – 1300 MHz |
| 2320 MHz | 2300 – 2450 MHz |
| 10 GHz | 10,5 GHz |

1. Bande. La fréquence doit tomber dans l'un des segments gérés; tout le reste, et en particulier chaque commercial de moins de 49 990,0 kHz, n'est pas montré et compte comme hors de bande.
2. Bien sûr. Une des façons les plus connues de commenter est FT8, FT4, Q65, JT65, ISCAT, CW, SSB, FM, RTTY, PSK31, MSK144, FSK441, JT6M); USB et LSB deviennent SSB.
3. Filtre négatif. Le spot est rejeté s'il contient les modes MSK144, FSK441, JT6M, JTMS, ISCAT ou les étiquettes <AUE> <ES> <MS> <FAI> < F2 >, avec des majuscules non sensibles et même des espaces entre crochets. Ce sont des voies et des propagations qui ne sont pas tropos: Scatter météore, aurore, et sporadique, F2.
4. La diffusion d'air. Si le commentaire contient <AS> ou <ACS> (également mélangé, p. ex. <As>) ou déclare l'endroit comme une diffusion d'air (dispersion d'air, dispersion d'air, dispersion d'aéronef), l'endroit reste mais est dessiné en jaune avec une ligne pointillée (dark wavey pour rester lisible sur la carte claire) et surligné dans la liste.
5. Emplacement des stations. Si dans le commentaire il y a deux localisateurs (par exemple JN54 < JN83), le premier est de ceux qui font le spot et le second de la station DX; avec un localisateur, c'est de la station DX. Ce qui manque est recherché dans CALL3. TXT, seulement avec le nom exact (pour les ordinateurs portables voir ci-dessous). Si une station reste sans position, l'endroit apparaît dans la liste avec la note - position inconnue, mais pas sur la carte.
6. Deux. Une publicité avec le même DX, le même spotter, la même bande et la même source d'un déjà présent la remplace, au lieu d'ajouter.

CALL3.TXT est la base de données de noms → localisateur utilisé par JTDX, dans le format CALL,LOCATORE,,. Le fichier de projet contient 1 048 847 nominations, toutes avec un localisateur valide; l'en-tête du fichier parle encore de 34 323 noms parce qu'il est resté celui de la première édition [7]. Il est chargé en arrière-plan au démarrage, donc la fenêtre est immédiatement utilisable. Le fichier est recherché à côté du programme, dans le dossier Data, dans le dossier à partir duquel le programme a été lancé et un ou deux dossiers ci-dessus; S'il n'y a pas, une fenêtre l'indique lorsque vous démarrez et la barre d'état le signale en rouge. Son nom utilise toujours son 6 caractères. Seule règle pour les localisateurs: si celui reçu (spot, MQTT, décodage, QSO desunti) a moins de 6 caractères et CALL3. TXT a un à 6, il vaut toujours le CALL3.TXT; pour les ordinateurs portables (F/ IC8TEM, IC8TEM/P) est seulement une voix exacte: sans, la station reste sans propriétaire et n'est pas conçu ailleurs. La règle s'applique également au moment du dessin, de sorte que les données arrivées avant le chargement de la base de données s'exécuteront.

Une base de données: CALL3.TXT. De 1.3.4 tous les localisateurs sont dans un seul fichier, CALL3.TXT, qui reste dans le format de JTDX et WSJT-X (CALL, LOCATORE,,,,) et peut être utilisé comme il est aussi dans WSJT-X, JTDX et MSHV. Il n'y a plus de fichiers séparés. En haut du fichier, après l'en-tête, il y a deux blocs marqués par une ligne de commentaire: // [LIVETROPOVHF-MANUALI] avec les entrées insérées à la main et // [LIVETROPOVHF-CLUSTER] avec les corrections du cluster; puis // [LIVETROPOVHF-FONTI] avec tous les autres éléments dans l'ordre alphabétique. Chaque candidature apparaît une fois. Dans la sous-langue Données et sources › Rendez-vous (CALL3) vous recherchez un nom, vous écrivez le propriétaire et appuyez sur Ajouter / modifier: la voix va dans le bloc des voix de main et il vaut avant tout, même au-dessus du localisateur que la station elle-même envoie. Les Supprime la nomination de CALL3.TXT (la prochaine mise à jour la reprend des sources, si elles l'ont). Ajouter un fichier... lire une liste CALL, LOCATOR (vous acceptez aussi le point et la virgule, tabulation et espace) et l'écrivez entre des voix cueillies à la main; le fichier choisi n'est pas copié. Recharger la lecture CALL3.TXT à partir du disque, après l'avoir édité avec un éditeur. Ordre complet des sources: propre localisateur, voix de main, corrections de cluster, le reste de CALL3. TXT (sources, localisateurs en direct appris et G7VRD), localisateur reçu (qui ne s'applique sur la base de données que s'il a 6 caractères et ne se termine pas avec LL ou MM).

Mise à jour de CALL3.TXT. Dans le même sous-langue, le bouton Update télécharge maintenant les sources (CALL3.TXT de WSJT-X Improved, CALL3.TXT EME de W7GJ, grid_data.bin de JTDX), les rejoint (le premier qui a une victoire en nomination), vérifier qu'ils sont au moins 50 000 noms (autrement rien ne change), enregistre le CALL3. TXT Le précédent CALL3. Il reste des entrées TXT et un localisateur à 6 caractères où une source n'a que 4 du même carré. Les voix des mains et les corrections du cluster restent en haut, inchangées. Avec la boîte de mise à jour sur son propre jour, le chèque a lieu au début. Le programme réécrit CALL3. TXT immédiatement après chaque voix à la main et, pour les localisations apprises, au plus toutes les 10 minutes et à la fermeture; le CALL3. TXT du paquet est en lecture seule et l'attribut se supprime. Les installateurs Linux n'écrasent pas un CALL3. TXT déjà présent; que pour Windows l'installe seulement s'il manque. ALL3

Corrections automatiques du cluster. Dans les spots automatiques DM4X et RBN, le commentaire apporte deux localisateurs, par exemple KN09tp<> KN09: la première est ce que la station que les spots se déclarent, à 6 caractères; la seconde est celle de la station ressentie, souvent seulement à 4. Si la première a 6 caractères (et ne se termine pas avec LL ou MM) et est différente de celle de la base de données, le programme corrige CALL3. TXT seul: la correction va dans le bloc // [LIVETROPOVHF-CLUSTER], avec la date de la dernière confirmation dans le troisième champ, elle est valide au-dessus des sources (sous les voix de la main) et apparaît entre les signaux suspects mis à jour sous le nom de "CALL3 ". Donc il survit CALL3. Mises à jour TXT. Une correction non confirmée pendant deux ans perd la priorité et reste une voix normale.

Dernière source: G7VRD. Si un candidat n'a pas de 6 caractères dans CALL3. TXT, le programme le demande en arrière-plan à l'API publique de G7VRD (api.g7vrd.co.uk, locator par callsign), qui recueille automatiquement les localisateurs du trafic réseau. Vous acceptez seulement un 6 caractères qui n'est pas le centre de la place (LL ou MM); les demandes sont groupées, au maximum 40 nominatifs par minute, et chaque nominatif demande une fois par jour, car c'est un service hobbyiste sans garantie. Le localisateur trouvé entre en CALL3. TXT si le nom n'est pas là ou n'a que 4 caractères du même carré; la recherche s'éteint avec la case dans le nom sublingual.

Noms avec le bar. Dans les sujets PSKReporter MQTT, la barre sépare les niveaux, puis les noms composés viennent avec un point au lieu de la barre ( IV3. HB9CAT). Le programme renvoie la barre et, si le message contient les noms complets, utilisez ceux-ci: IV3/HB9CAT reste IV3/HB9CAT sur la carte, la liste et la base de données.

Les indices appris en direct. CALL3. TXT ne contient pas tout: par exemple SP9EMF et SP9NVY ne sont pas là, et reste ensuite le localisateur 4 caractères envoyé par PSKReporter (la carte le dit). Pour réduire ces cas, le programme écrit en CALL3. TXT tous les 6 caractères qui proviennent de n'importe quelle source (PSKReporter, comme émetteur ou comme récepteur, groupe, décodage UDP) pour une nomination que CALL3. TXT n'a pas ou n'a que 4 caractères dans le même carré; ne change jamais de voix à 6 caractères. Jusqu'au 1.3.3, ces localisateurs étaient dans Data/ LOC6.TXT.

Passage de 1.3.3. Au premier début de 1.3.4 les fichiers en dehors des versions précédentes (Dati/CALL3 -MANUALE.TXT, le Data/CALL3 -AGGIUNTE, Data/CALL3 -CLUSTER. TXT, Data/ LOC6.TXT et Data/CALL3 - G7VRD.TXT) sont fusionnés en CALL3. TXT avec les mêmes entrées. Releasing écriture, anciens fichiers prennent l'extension.fuso, vous ne lisez plus et vous pouvez supprimer.

Récepteurs automatiques. RBN, dm4x et PSKReporter rapportent souvent des noms avec un suffixe indiquant le récepteur: -SDR, /SDR, -# (simmer RBN) ou -1, -2... (plus les récepteurs de la même station). Pour le propriétaire, le suffixe est retiré: OK2OZL -SDR prend OK2OZL JN89UE de CALL3.TXT si la station n'envoie pas déjà un localisateur à 6 caractères. Le nom indiqué reste celui avec le suffixe.

Des indices coupés. Toutes les sources ne guérissent pas l'extension de leur propriétaire: beaucoup le fixent à 4 caractères, et certains systèmes de report automatique tronquent délibérément le localisateur à 4 caractères, même ceux qui obtiennent les données de l'écoute réelle de FT8, FT4, Q65 et JT65, ou le complètent avec le centre de la place (LL ou MM, par exemple JN89MM). Pour cette raison, un 4 caractères, ou 6 caractères qui se termine par LL ou MM, est remplacé par le CALL3. TXT du même carré. Si l'emplacement correct et le centre de la place ne coïncident pas avec quelques dizaines de km, l'erreur reste petite par rapport aux distances de tropo.

## 6. Qui écoute

Chaque message MQTT de PSKReporter indique le nom et la localisation de ceux qui reçoivent [5], de sorte que vous n'avez pas besoin de le chercher dans CALL3.TXT. Pour la bande choisie dans le sélecteur, le programme détient, pour chaque station de réception: propriétaire, dernière réception, manières décodées, nombre de stations feutrées et distance maximale. Une station quitte la liste lorsqu'elle ne reçoit plus rien dans la gamme choisie (3, 5, 10, 15, 30 ou 60 minutes).

Sur la carte chaque récepteur est un cercle vide rose foncé, et de chaque départ des lignes pleines rose vers les stations qui se sentent et renvoie au filet. Les lignes sont toujours vues, sans cliquer. Les rendez-vous des stations d'écoute, ceux entendus et les deux têtes de chaque endroit sont toujours écrits sur la carte, sans avoir à cliquer (le même nom au même point est écrit une seule fois). En cliquant sur un cercle ses lignes deviennent encore plus épaisses et ouvre l'onglet avec des détails. Si le localisateur de MQTT a moins de 6 caractères, le programme utilise celui de CALL3.TXT (environ un million de noms), à condition qu'il soit compatible avec le carré reçu.

Des météorites éparpillées. Comme pour les clusters, les ratios MQTT dans MSK144, FSK441, JT6M, JTMS ou ISCAT sont rejetés et comptés seulement; il en va de même pour les décodages UDP de cette façon.

Pas vraiment. Certaines stations déclarent recevoir 144 MHz, mais en réalité le récepteur se trouve sur une autre bande (indication de fréquence erronée): leurs rapports permettraient de concevoir des ouvertures inexistantes de milliers de kilomètres. Le programme le vérifie tout seul. Chaque station qu'un auditeur dit entendre en VHF est suivie pendant 30 minutes sur toutes les bandes, avec une inscription MQTT dédiée (pskr/filter/v2/+/{nominative}/#) [5]. servent de preuve. Si au moins deux des stations entendues par un auditeur sont, dans les mêmes minutes (±10), actives sur d'autres bandes, l'auditeur est considéré comme non authentique et tous ses rapports VHF sont rejetés; le registre MQTT signale le nom et les stations qui l'ont trahi. Dans la liste des spots, à côté des compteurs, le bouton MQTT indique l'état de l'écoulement (feuille verte si connectée, rouge si non) et le nombre d'auditeurs rejetés dans la bande et la gamme choisie; en cliquant dessus, il ouvre l'onglet avec la liste des rejets et, pour chaque station trouvée sur d'autres bandes.

Une longue liaison avec la Chine. Un lien de plus de 1000 km (Clumbing Spot, MQTT Listening, QSO Desunti) n'est pas montré immédiatement: reste retenu, et le compteur -détenu > 1000 km, dit le nombre. Il est révélé quand une autre connexion, entre différentes stations et au moins la moitié le long, le confirme avec l'un de ces critères: la même station à une tête (par exemple le même auditeur) et l'autre station dans la même direction à moins de 10°; couloir similaire, avec les deux têtes à moins de 200 km de celles de l'autre liaison; itinéraire parallèle, avec la direction à moins de 10° et la distance latérale à moins de 200 km, chevauché le long de la route. Les deux apparaissent et le journal MQTT montre la raison. Ses décodages UDP sont toujours visibles et s'appliquent comme confirmation pour les autres. Les connexions conservées restent en mémoire pendant 60 minutes.

Rien sous 144 MHz. Le programme fonctionne à partir de 144 MHz jusqu'à: tout est jeté sous 143 MHz. L'exception est GRAVES, le radar français émettant en CW sur 143 050 MHz par Broye-lès-Pesmes ( JN27SI): une publicité entre 143 040 et 143 060 MHz, ou avec GRAVES au nom, apparaît entre 144 MHz avec radar comme émetteur. Le panneau publicitaire indique si le récepteur est à l'intérieur du faisceau (secteurs D, C, B, A de 90° à 270° d'azimut vu du radar, 6 marches de 7,5° chacun) ou hors; l'onglet GRAVES, comme dans YA-MS, montre le ciel vu du radar: secteurs, pas illuminé à ce moment-là (séquence PE1ITR 2021: 0,8 s par étape, cycle 4,8 s Les données et la géométrie proviennent de YA-MS. Il est difficile de le sentir dans la trope, mais ça peut arriver.

Réhabilitation des groupes humains. Une station qui apparaît aussi sur d'autres bandes ne compte pas comme suspecte si, dans les mêmes minutes (±30), les groupes humains DX ( ON0DXK, GB7RAU; pas dm4x, RBN ou autres sources automatiques) les dommages actifs sur la bande en question, dans les taches faites par d'autres noms: c'est le cas des stations qui travaillent vraiment sur plusieurs bandes ensemble. Ainsi, un auditeur rejeté retourne visible (le registre MQTT écrit "Rehabilitated") et un lien de longue date pour les bandes basses peut être dévoilé.

Stations actives de moins de 144 MHz. En quarantaine de longues connexions, une station qui, dans la même minute, est entendue sur des bandes de moins de 144 MHz (d'après les rapports MQTT des autres bandes) ne confirme pas d'autres connexions et n'est pas dévoilée, même si une route semblable ou parallèle la confirmerait.

## 7. Liste des publicités

Il occupe 35 % à droite de l'onglet Map et spot. Haut de la page: titre, sélecteur de bande (50, 70, 144, 222, 432, 1296, 2320 MHz, 10 GHz), sélecteur de gamme (derniers 3, 5, 10, 15, 30, 60 minutes) et clé Mise à jour. Ci-dessous, quatre mètres: taches visibles, AS/ACS, jetés du filtre, stations d'écoute.

Chaque ligne indique le type de ligne, l'heure UTC, DX → DE, le chemin, la distance en km et, sur la deuxième ligne, la source, la fréquence et les commentaires. Le plus récent est en haut. Un clic sur la ligne sélectionne le spot: sur la carte la ligne grandit et les deux noms apparaissent.

## 8. Perte de carte et de troposcatter

Pour le spot sélectionné, l'onglet affiche les localisations, la distance, l'azimut, la fréquence, le mode, la source, maintenant, le commentaire, l'indice moyen de tropo le long du chemin et une estimation de la perte de route pour le tropo dispersé.

La distance et l'azimut sont calculés sur le cercle maximal avec un rayon de Terre de 6371 km (formule de l'émisenoverso pour la distance, azimut initial de la station DX au spotter).

Une perte de troposcatter. La perte de base, pour deux stations sur sol lisse, est la somme de la perte d'espace libre et de la perte de diffusion: G3YGF [2]

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

Les pertes sont en dB, D est la distance en km, f la fréquence en MHz, φ l'angle de diffusion en degrés.

8497 km est le rayon équivalent de la Terre (4/3 du rayon physique). Les valeurs A et B dépendent de l'horizon de chaque station:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

avec Δh hauteur de l'obstacle qui fait de l'horizon par rapport à la station et sa distance. Chaque niveau d'horizon coûte environ 10 dB [2]. Le programme ne connaît pas l'horizon des stations, il montre donc la perte de base avec φA = φB = 0 et la déclare ("sol lisse"). G3YGF ajoute un terme climatique, où N est la réactivité au sol [2]; il sera utilisé lorsque N est disponible à partir du modèle météorologique:

```math
L_{N} = 0,2 \cdot (N - 310)
```

Vérification: pour 110 km à 144 MHz, la formule donne 166,5 dB, contre environ 168 dB sur la carte de l'article. Pendant 500 km à 144 MHz donne environ 206 dB. Le rapport signal/bruit attendu est la capacité du système (sensibilité d'eirp + effective du récepteur) moins la perte: avec le modèle G3YGF 144 MHz (198 dB) une trajectoire de 500 km donnerait environ −8 dB dans SSB [2], c'est-à-dire la connexion n'est qu'avec une meilleure propagation de la dispersion pure.

## 9. Mes décodages: UDP de WSJT-X, JTDX et MSHV

WSJT-X, JTDX et MSHV peuvent envoyer UDP chaque ligne qu'ils décodent. Le programme écoute sur 127.0.0.1:2237 et répète le même paquet, octet par octet, sur 127.0.0.1:2238, de sorte que les autres programmes qui ont utilisé ce port (GridTracker, JTAlert, un enregistreur) continuent à fonctionner si vous les déplacez sur 2238. Les adresses et les portes changent dans le sous-lingual UDP de la collecte de données; comme adresse d'écoute, vous pouvez également utiliser un groupe multicast (p. ex. 224.0.0.1), si le programme de décodage est défini comme ceci.

Les paquets suivent le protocole réseau WSJT-X: un numéro de magie initial 0xADBCCBDA, numéro de schéma et type de message. Le programme utilise principalement trois types [8]:

- Heartbeat: indique quel programme transmet les données et avec quelle version.
- Statut: fréquence du cadran et du mode; à partir de la fréquence, vous obtenez la bande (avec la même table des taches).
- Décoder: Maintenant, SNR, temps et fréquence gaspillage, mode et texte du message décodé.

D'après le texte, vous obtenez la station que vous avez entendue: en CQ 9 A2PQR JN83 est la station après CQ (sauvegardant DX, EU, TEST...), dans IK0JKL IT9MNO JM68 est la seconde, c'est-à-dire qui transmet. Le localisateur est celui du message, s'il y a (4 caractères, différents de RR73); sinon vous recherchez dans CALL3. TXT. Ce sont des lignes d'écoute: des stations que votre récepteur entend maintenant. Sur la carte ils deviennent des lignes rouges de votre QTH à la station, avec le nom; la couche s'allume avec la boîte décodée de moi et nécessite un localisateur valide. Dans la liste à droite, un compteur indique combien de stations vous décodez sur la bande choisie.

QSO desunti dal en direct. Lorsqu'un décodage est un message de fermeture dans le formulaire C ALL1 C ALL2 RR73 (ou 73 ou RRR), par exemple IC8TEM IK3FHP RR73 ou IK3FKP SV8PEX 73, les deux stations ont rempli un lien. Le programme l'enregistre comme QSO, avec le localisateur de chacun pris à partir du dernier message dans lequel il l'a déclaré ou, sinon, de CALL3.TXT [7], et l'attire sur la carte comme une ligne verte entre les deux stations (strate QSO desunti dal live). Une paire de stations compte une fois par bande; la bande et l'intervalle choisis dans la liste sont valides.

Dans la même sous-langue, le volet Debug affiche le texte de chaque paquet reçu (type, heure, valeurs et message), sur demande aussi des octets hexadécimaux, et une ligne pour la transmission. C'est pour vérifier que le programme de décodage envoie les données.

## 9 bis. Langues Hepburn et F5LEN

Chacun montre ce que le programme prend de la source. Le bouton « Lire la page et télécharger toutes les images » ouvre la page indiquée (base dxinfocentre.com/tropo_eur.html pour Hepburn [3] et tropo.f5len.org/forcasts-for-europe pour F5LEN [4]) et télécharge toutes les images dans le cache à côté de l'exécutable; vous pouvez également coller l'adresse d'une image. Pour chaque image il y a une boîte pour l'exclure et un champ avec le temps de prévision. Les 31 images européennes de F5LEN sont nommées par le temps de la course modèle (03.png... 48.png toutes les 3 heures, 54... 96 toutes les 6 heures, 108... 192 toutes les 12 heures) et le programme prend le temps du nom du fichier [4]; pour Hepburn il vaut l'ordre de la page. Dans l'onglet F5LEN, un menu choisit la région: Europe (tropo.f5len.org/forcasts-for-europe), Australie (tropo.f5len.org/Australia, vk03.png... vk192.png) ou Atlantique entre le Cap-Vert et l'Irlande (tropo.f5len.org/atlantic-ocean/atlantic-d4-ei, atl03.png... atl192.png). Tout le monde a les mêmes pas.

Ci-dessous se trouve le compteur d'étalonnage. Un clic sur l'image ajoute un point de contrôle (locateur ou lat,lon: au moins 6) ou une couleur de la légende avec sa valeur. Le programme calcule le pixel correspondant → coordonnées avec une interpolation polynôme de second degré aux plus petits carrés [19], lit les couleurs avec la tolérance choisie et, pour F5LEN, convertit les unités N dans l'échelle 0–10 avec un · N + b. Enregistrez l'étalonnage et essayez de lire en écriture le fichier dans le dossier Tarature et vérifiez sur quel pixel votre localisateur tombe. Avant de mettre en place un calibrage, il faut demander l'autorisation à l'auteur des cartes.

Régions. Chaque source a plus de régions, chacune avec ses images à noms fixes: Je suis désolé. L'onglet de chaque source montre d'abord l'aperçu de la fusion: toutes les images téléchargées réunies comme sur la carte principale, avec la vitre pointillée et le nom de chaque région, un voile gris où aucune image ne couvre la zone et un curseur pour faire défiler les heures; à gauche la liste des régions, chacune avec la boîte pour l'utiliser, le nombre de cartes lues et d'où viennent les extrêmes (OCR, à la main ou en usine; si l'OCR échoue le texte qu'il lit apparaît). Le banc d'étalonnage manuel reste derrière le bouton homonyme. Le menu de la région du banc choisit le look, la case «Utilisez cette région sur la carte» l'inclut dans l'index. Le cadre se trouve seul (deux lignes horizontales et deux lignes verticales de la même couleur); pour placer l'image sert les extrêmes lat/lon du cadre: pour Hepburn Europe (10° W–40° E, 27°–56° N) et F5LEN Europe (15° W–40° E, 30°–65° N) sont déjà dans le programme, pour les autres régions qu'ils écrivent dans les quatre champs O E S N lisant les étiquettes d'axe. Le multiplicateur d'échelle rend les deux sources comparables: valeur finale = valeur du multiplicateur × valeur légende. [3] F5LEN [4] F5LEN

## 9 ter. Exporter vers Word

Les deux documents ont en haut du bouton « Exporter vers Word ». Le fichier est écrit au format Office Open XML; les formules deviennent des équations Word (OMML), de sorte qu'elles restent modifiables avec l'éditeur d'équation [16]. Si la fenêtre de sauvegarde n'est pas disponible, le fichier se termine dans le dossier Documents à côté de l'exécutable.

## 9 quater. Qui m'appelle

Si un message vous est envoyé sur l'UDP local (le prénom est le vôtre, par exemple IC8FAX IK3FHP JN55 ou IC8FAX IK3FHP -12), le programme suit de votre QTH à la station qui vous appelle une ligne noire pointillée et clignotante, avec le nom et le localisateur à côté de vous. Le localisateur provient de CALL3.TXT (y compris les voix de main) ou de message. La ligne reste jusqu'à ce que l'UDP passe la fermeture du QSO avec cette station ( RR73, RRR ou 73, dans l'une des deux directions), au plus 3 minutes du dernier appel; vous tirez sur tout et ne suivez pas les critères d'exclusion, donc un appel direct ne s'échappe jamais.

PSKReporter, via MQTT, ne transmet pas le texte des messages: dit seulement qui a reçu qui, avec fréquence, chemin et SNR. C'est pourquoi un appel direct n'est reconnu que par ses propres codes UDP.

## 10. Contrôle du temps (NTP)

Les modes numériques et le temps de spot nécessitent une horloge précise. Toutes les 3 minutes, le programme interroge 10 serveurs NTP (SNTP sur UDP 123): 0.pool.ntp.org, 1.pool.ntp.org, 2.pool.ntp.org, 3.pool.ntp.org, it.pool.ntp.org, ntp1.inrim.it, ntp2.inrim.it, ptbtime1.ptb.de, time.google.com, time.cloudflare.com [11].

Pour chaque réponse, calcule le rejet et le retard de retour, rejette les valeurs anormales et prend la médiane, avec le calcul du rejet et du retard fourni par SNTP [10]. Avec au moins 3 réponses conviennent l'horloge UTC du programme s'aligne et la suivante apparaît NTP:OK en vert; si les réponses ne suffisent pas apparaissent NTP:FAILED en rouge et l'horloge reste celle de l'ordinateur. L'horloge système n'est pas touchée, car les autorisations de l'administrateur seraient nécessaires; Tous les programmes internes (spots, fenêtres de 5 à 60 minutes, décodages) utilisent le temps aligné. Le contrôle tourne toujours en arrière-plan: dans l'interface vous ne voyez que le résultat à côté de l'horloge; les serveurs, les intervalles et les réponses minimales changent dans LIVETROPOVHF.ini (ntp_servers, ntp_minutes, ntp_min).

## 10 bis. Critères généraux d'exclusion

Deux cases dans la barre des calques s'appliquent à tout ce que vous dessinez (spot, PSKReporter, décodage, QSO desunti):

- Exclure moins de 100 km. La circulation entre les stations de moins de 100 km peut ne pas être significative pour le tropo (onde au sol, plage optique): avec la boîte que vous ne dessinez pas et n'apparaissez pas dans la liste. Le départ est annulé.
- Plus de 1000 km: seulement carrés. Autour de votre QTH (le localisateur de départ) il y a une couronne de 1000 km dans toutes les directions. Si une seule des deux stations d'un lien est en dehors de cette couronne, la connexion n'est pas tracée comme une ligne: à sa place, elles colorent en lilas, pointillé à 45°, les carrés de localisation (4 caractères) des stations concernées. Il en va de même pour les stations PSKReporter écoutant plus de 1000 km, qui apparaissent comme un carré au lieu d'un cercle. Jusqu'ici le trafic que vous voyez sans remplir la carte des lignes. Sans un localisateur valide compte la longueur de la connexion. Le carré de votre QTH ne colore jamais en lilas, même lorsque vous écoutez une station au-delà de la couronne: est le localisateur spécifié, n'importe quel nom utilise qui opère de la station. Le rayon de la couronne est choisi à côté de la boîte: 1000, 700 ou 500 km. La boîte Corone 1000/700/500 km dessine les trois couronnes autour de la QTH, dessinée en lilas, avec le choix le plus marqué et la distance écrite au nord. Dans la liste des endroits, la distance est suivie de.. C'est parti. Le critère s'applique après la quarantaine: les longues connexions conservées n'apparaissent même pas en carrés.

Quels sont les carrés de lilas couverts à 45°? Le trafic éloigné de votre QTH vous intéresse à comprendre où la propagation est ouverte, mais ses lignes, des centaines de kilomètres de long et souvent des dizaines, couvrent la carte et cachent ce qui compte pour vous: les ouvertures que vous pouvez exploiter de votre station. Pour cette raison, au-delà de la couronne choisie, le programme ne dessine pas les lignes mais colore les carrés de localisation (2° × 1°) des stations concernées. Le résultat est une carte des «zones actives»: où apparaissent de nombreux carrés de lilas proches, il y a un conduit ou une ouverture, même si cela ne vous concerne pas directement; Si la zone s'élargit vers vous, c'est le signal pour pointer l'antenne dans cette direction. Le tableau de bord à 45° vous permet de voir sous l'index du tropo et la grille, alors comparez en un coup d'oeil la prévision (couleur de fond) et le trafic réel (illus). Votre QTH carré ne colore jamais, parce que c'est le point de départ et non une zone éloignée. Seuls les carrés qui sont tous au-delà de la couronne sont colorés: un carré traversé par la couronne, ou à l'intérieur, reste libre même s'il y a une des deux stations. Pour voir les lignes, il suffit d'éteindre la boîte ou de choisir une couronne plus grande.

## 11. Thèmes et langues

Thèmes: les 18 programmes communs à IC8TEM, comme YA-NEC et E-LAYER ANALYZER: Clair, nuit, noir, huit pastels (bleu, orange, aquamarine, vert, rose, lilas, indigo, brun), Scia météorica ( palette YA-MS), les trois de IP-TRX-CTRL (Chiaro, Notte, Nero) et bleu, violet, vert. De 1.3.4 le Chiaro ressemble à YA-NEC: fond gris clair, panneaux blancs bordés, onglets de nuit bleus et accent bleu. Dans les thèmes sombres aussi la carte de base devient sombre. L'échelle de couleur de la prévision reste la même dans tous les thèmes. Sur les écrans étroits (moins de 1500 pixels, par exemple 1366×768), l'horloge en haut ne montre que l'heure, donc le thème, le curseur des textes et la langue restent visibles. Langues: le 28e wiki de YA-MS [17] (Anglais, Voyage, Deutsch, Nederlands, Français, Español, Port本ês, Dansk, Norsk, Svenska, Íslenska, Eesti, Lietuvi, Latviešu, Руский,,кра,нська, Türkaلبيا Avec l'arabe, l'hébreu et l'ourdou, la fenêtre entière miroirs de droite à gauche; la carte reste comme [12]. Les traductions concernent l'interface; les deux documents techniques et les messages de la barre d'état restent en italien. Les noms des jours viennent des cadres internationaux de. NET pour la langue choisie.

Le thème et la langue changent depuis les deux menus en haut à droite, à côté de l'horloge, et s'appliquent immédiatement, sans redémarrer le programme et sans interrompre la collecte de données. Le choix est enregistré dans LIVETROPOVHF.ini.

## 11 bis. Rapports suspects et auto-apprentissage

L'onglet Rapports suspects recueille tout ce que les contrôles du programme trouvent étrange, groupé par type et nom, avec les dernières 24 heures listées. Le programme apprend par lui-même: pour chaque couple (type, nom) compte les résultats positifs (+) et contraires (-), et après 3 résultats convenus crée une règle automatique; un résultat opposé le met en question. Les décisions de l'utilisateur (bon, mauvais) s'appliquent toujours; le bouton pulsante renvoie la décision au programme. Les règles sont dans Data/APPRENDIMENTO. TXT, à côté du programme; les automatiques non renforcés pendant un an sont oubliés.

| Type | Quand | Sortie +/− | Effet de la règle |
| EME ( Q65/JT65) | Q65 ou JT65 sur 1000 km avec la Lune au-dessus de l'horizon pour les deux têtes, ou EME en commentaire | - chaque fois que la Lune est commune | Disparition: station EME, ses longues Q65/JT65 sont également exclues sans lune commune. Bien: traité comme un tropo |
| Connexion longue | Plus de 1000 km, en quarantaine | + dévoilé, − expiré sans confirmation | Bon: les longueurs de la station montrent immédiatement. Jamais |
| Auditeur | PSKReporter récepteur qui entend des stations actives sur d'autres bandes | − rejetés, + réhabilités par des groupes humains | Bien: jamais rejeté. Jeter: toujours |
| Différents propriétaires | Square reçu différent de celui de CALL3. TXT | + chaque fois que vous le répétez | Bien: la station a déménagé, elle vaut la place reçue. Jeter: vale CALL3.TXT |
| Centre Square | 6 caractères avec LL ou MM |  —  | Information seulement |
| Récepteur automatique | Rendez-vous avec -SDR, /SDR, -# ou -n |  —  | Information seulement |

EME. Q65 et JT65 sont les moyens de communication via Luna: En théorie, ils tombent dans la clause d'exclusion comme le météore scatter, mais ils sont également utilisés en tropo. Le programme calcule la position de la Lune avec les formules de basse précision de l'astronomie d'Almanac (environ 0,3°) [20]: si une connexion en Q65 ou JT65 dépasse 1000 km et que la Lune est au-dessus de l'horizon des deux têtes, elle est presque certainement EME et est exclue (elle compte parmi les exclus). Si la Lune n'est pas commune, la connexion reste possible dans la trompe et passe par la quarantaine de longues connexions, mais le signal reste visible. Elle s'applique aux grappes, au MQTT et à ses codes UDP.

## 11 ter. Critères de rejet de contact, un par un

Le programme veut montrer seulement la propagation tropophérique dans 144 MHz et au-delà. Chaque donnée entrante (Cluster Row, PSKReporter Report via MQTT, décodage UDP) passe par une série de filtres, toujours dans le même ordre. Une donnée supprimée ne disparaît en rien: est comptée (contact de l'exclu dans la barre supérieure) et, lorsque la raison est douteuse, apparaît dans l'onglet Rapports suspendus.

|  N.  | Critères | Où est-il? | Ce qui a été rejeté | Tu peux te changer? |
|  1  | Bande | groupe, MQTT, UDP | Toutes les fréquences inférieures à 143 MHz (aussi 50 et 70 MHz), sauf GRAVES 143 050 MHz. Les bandes détenues sont 144, 222, 432, 1296, 2320 MHz et 10 GHz | C'est pas vrai. |
|  2  | Satellite QO-100 | groupe, MQTT, UDP | 2400,0–240,5 MHz et 10489,5–10490 MHz: c'est du trafic géostationnaire par satellite, pas tropo | C'est pas vrai. |
|  3  | Dispersion du météore | groupe, MQTT, UDP | JT6M et ISCAT (aussi ISCAT-A/B), commentaires avec des étiquettes de propagation "&" et non-tropoxy (<AU>, <AUE>, <ES>, <FAI>, < F2 >) toujours. MSK144, FSK441 et JTMS (également avec <MS>) seulement sur 250 km: dans les 250 km que nous acceptons, car à cette distance ces modes sont également utilisés en tropo | C'est pas vrai. |
|  4  | EME (via Luna) | groupe, MQTT, UDP | EME écrit dans le commentaire; Q65 ou JT65 sur 1000 km lorsque la Lune est au-dessus de l'horizon pour les deux têtes; stations apprises comme EME | Oui: Bonne règle pour les signaux suspects |
|  5  | Auditeur non authentique | MQ | PSKReporter récepteur qui dit entendre sur 144 stations actives à ce moment sur d'autres bandes (au moins deux): Ses relations sont écartées. En arrière bon si les grappes humaines DX confirment les mêmes stations | Oui: bon / mauvais |
|  6  | Quarantine de longues connexions | toutes lignes | Plus de 1000 km un lien reste caché jusqu'à ce qu'il trouve une confirmation: la même station dans la même direction, un autre itinéraire dans un couloir de 200 km, ou un itinéraire parallèle dans les 10° et 200 km le long d'au moins la moitié. Sans confirmation expire après 60 minutes | Oui: Bon / En dehors de la station |
|  7  | Moins de 100 km | lignes et liste | Avec la boîte d'accès, les connexions les plus courtes de 100 km (onde au sol, portée optique) | Boîte, éteinte au démarrage |
|  8  | Au-delà de la couronne de la QTH | lignes | Si une station est à plus de 1000, 700 ou 500 km (à votre choix) de votre QTH, pas de ligne: seulement les lillas carrés couverts par les deux stations. Votre QTH Square Never Colors | Boîte et rayon, sur 1000 km |
|  9  | Fenêtre temporelle | tout | spots plus anciens de la gamme choisie (de 3 à 60 minutes) | Menu en haut |
|  10  | Double | Groupe | Le même spot (même DX, même spotter, même bande, même source) répété | C'est pas vrai. |

Note sur les critères 4 et 6. Une connexion en Q65 ou JT65 sur 1000 km sans lune commune est possible en tropo: n'est pas écarté par le critère 4, mais passe pour la quarantaine du critère 6 comme tout autre long lien. Les appels directs à vous (section 9 quater) sont la seule exception aux critères 7 et 8: ils dessinent toujours.

Ce n'est pas un gaspillage. Les corrections du localisateur (CALL3. TXT avec des voix de main, des corrections du cluster et des localisateurs appris, suffixes -SDR, centre de la place LL/MM) changent où vous dessinez une station, pas la jeter. Une station sans propriétaire, en revanche, ne peut pas être tirée: reste dans la liste avec la distance «—».

## 11 quater. Couleurs et symboles

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

## 12. Configuration et portabilité

Fenêtre. Au début, la fenêtre occupe 90% de l'espace utile de l'écran (coupez la barre d'application), centré, donc sur un cahier de 15-16 pouces avec un 125% ou 150% redimensionner la barre de titre reste visible. Si l'espace utile est inférieur à 1280 × 720 points, la partie fenêtre maximise. La taille, la position et le statut maximal sont enregistrés dans LIVETROPOVHF. ini à la fermeture et à la fin au prochain départ, à condition que la barre de titre tombe toujours dans un écran (par exemple après avoir détaché un moniteur externe); sinon nous revenons à 90%. La taille minimale est 900 × 560.

Le programme est portable: n'écrit jamais dans le dossier AppData ou dans le registre. Tout est dans le dossier de l'exécutable: LIVETROPOVHF.ini (ensembles, créés à la première fermeture), CALL3.TXT (avec voix manuscrites, corrections apprises et localisateurs), cache de carte et données météorologiques, règles apprises (Dati/APPRENDIMENTO.TXT) et archive LIVETROPOVHF.db. Vous pouvez également le garder sur une clé USB, tant que le dossier est lisible.

Archives publicitaires (SQLite [14]). Chaque endroit accepté et chaque rapport d'écoute PSKReporter est enregistré dans LIVETROPOVHF.db, une base de données SQLite dans un fichier. En mémoire seulement les 60 dernières minutes restent pour la liste et la carte; l'archive conserve tout et sert pour l'historien et les statistiques (aussi les décodages UDP), par exemple les ouvertures pour la bande et par jour, les distances maximales, les carrés atteints, et la comparaison arrière entre l'index attendu et les spots réels. Les bandes éliminées du filtre sont comptées mais ne sont pas archivées.

## 12 bis. Installation automatique et mises à jour

À partir de la version 1.0, le programme télécharge déjà prêt à partir de la page de publication de github.com/ic8tem/LIVETROPOVHF, dans les mêmes paquets de YA-MS: installateur pour Windows (64 bits, 32 bits et ARM, installer en C:\LIVETROPOVHF sans droits d'administrateur) et zip portable; pour Windows 7 SP1 et 8.1 paquets séparés (-windows7-x64 et -window.deb,.rpm, Arch, Slackware, AppImage et tar.gz pour Linux; paquets pour Raspberry Pi (64 et 32 bits) et Alpine;.dmg et apps.zip pour Mac (Apple Silicon et Intel). Avec les paquets Linux, le programme est copié au premier démarrage dans ~/LIVETROPOVHF, de sorte que les paramètres et les données restent à côté du programme; sur les données Mac est né à côté de LIVETROPOVHF.app.

Toutes les 30 minutes (le premier contrôle quelques secondes après le début, immédiatement si la dernière fois que le programme a fermé pour une erreur) le programme lit seul, sans rien demander, la dernière version. Si la version est plus récente, téléchargez le paquet portable adapté au système et remplacez les fichiers du programme sur place: ceux qui sont utilisés sont renommés *.old-update et supprimés au prochain redémarrage. Paramètres, archive, dossier de données et CALL3. TXT ne sont pas touchés. La nouvelle version est active dès le prochain départ: à l'installation terminée, une fenêtre propose de redémarrer immédiatement; pour les corrections urgentes, marquées [obligatoire] dans Release, le redémarrage ne peut pas être reporté. L'état de contrôle et la version utilisée sont écrits au bas de la barre d'état; pour désactiver les mises à jour=0 dans LIVETROPOVHF.ini. Sur le Mac, l'application est signée et ne se change pas: le programme indique seulement qu'il y a une nouvelle version.

L'image LIVETROPOVHF avec le numéro de version apparaît au début; à la fin le programme demande confirmation et remercie pour l'utilisation.

## 12 ter. Journal des erreurs et barre d'état

Toute erreur imprévue se termine dans Data/errori.log, même avec la date et l'heure d'arrêt du calcul, version, système, onglet ouvert, erreur complète et dernières lignes de ce que le programme faisait. Une erreur d'interface ne ferme plus le programme: dans la barre d'état apparaît "erreur anno annotée" (cliquez: ouvrez le registre). Au prochain démarrage, le programme demande si envoyer le rapport: ouvre le navigateur sur un signal GitHub déjà compilé, avec la nomination, localisateur et nom d'utilisateur du PC supprimé, que vous vérifiez et envoyez avec un compte GitHub gratuit. Le journal se termine également par chaque vérification de mise à jour.

Dans la barre d'état, les boules source (cluster DX, RBN, PSKReporter, MQTT, UDP) sont cliquées et amenées à la source; la bande dessinée explique pourquoi une source est désactivée: port rejeté, nœud de cluster stationné sur le côté serveur, aucune réponse, ou écoute UDP mais sans paquets de WSJT-X, JTDX ou MSHV. Un clic sur le numéro de la version vérifie immédiatement les mises à jour.

## 13. Plateformes

Librairies externes: Avalonia pour l'interface [12], MQTTnet comme client MQTT pour le flux en temps réel de PSKReporter [13], Microsoft. Data.Sqlite pour l'archive [14].

C# sur.NET 10 (LTS) [15] avec interface Avalonia, publié comme exécutable indépendant pour Windows 64 et 32 bits, Mac Intel et Apple Silicon, Linux Debian et Raspberry Pi (64 et 32 bits). Limites notées: Windows 10/11; macOS 14 ou plus tard, avec des applications pour signer ou ouvrir à droite → Ouvrir; Raspberry de Pi 2 / Zero 2 W up (modèles ARMv6 ne sont pas pris en charge par.NET).

## Je vous remercie.
Merci à I5MZY Massimo Zannoni pour les tests sur Windows 7 et Windows 8.

Étanches: UIT-R P.453, produits numériques du groupe d'étude 3 (© UIT), utilisés à des fins personnelles et non commerciales.

## Principales sources

1. S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", présentation à la 41ème Réunion Nordique VUSHF, Skjeberg 2019. Troposferical Ways, Event Control List (Hepburn/ F5LEN, profils à haute température avec Windy, DX cluster comme preuve d'ouverture), observation que seules les cartes de prévisions ne sont pas suffisantes pour les itinéraires extrêmes.
2. J. N. Gannaway G3YGF, "Propagation de la dispersion troposphérique" Novembre 1983 (article éponyme sur la radiocommunication, RSGB, août 1981). Formes de perte d'espace libre et de diffusion, rayon équivalent 8497 km, terme du site de 10 dB par horizon, terme climatique 0,2 · (N − 310), capacité de la centrale et rapport signal/bruit, par exemple à 144 MHz.
3. W. R. Hepburn, Centre d'information DX, Prévisions de duplication troposphérique (dxinfocentre.com/tropo.html) et Indice du tropo Hepburn (HTI) (en anglais seulement). Échelle de l'indice et signification de la valeur, prédiction sur 6 jours.
4. F5LEN, prévision pour l'Europe (tropo.f5len.org/previews-for-European: liste des images dans le script de page, légende de classe Disturb... Duct dans l'image) et la prévision mondiale (tropo.f5len.org/WW) et note: Nouvelle version de l'outil mondial de prévision de propagation troposphérique VHF. Indice basé sur la réactivité dans l'unité N, 40 étapes de 3 heures, mise à jour 4 fois par jour.
5. mqtt.pskreporter.info, distribution MQTT en temps réel des publicités de pskreporter.info (curées par M0LTE, flux de P. Gladstone utilisé avec autorisation). Portes 1883-1886, pskr/filter/v2 thème schéma.
6. Résumé du projet du 8/11/2026 et Go test (core.go, gui.go), fourni par l'auteur du programme. Règles publicitaires, filtre négatif, segments de bande passante, gestion de clusters telnet, thèmes, langues et portabilité.
7. CALL3.TXT, base de données de nomination → propriétaire JTDX (I. UA3DJY, A. ES1JA script, JTAlert/hamspots.net données). Format du fichier et nombre des noms vérifiés dans le fichier du projet.
8. WSJT-X, code source, fichier NetworkMessage.hpp (description du protocole UDP: Heartbeat, Status, Decode). Référence générale, non annexée; JTDX et MSHV utilisent le même format.
9. Réseau de balises inversées (reversebeacon.net), pages de service du nœud telnet. Ports 7000 (CW, RTTY) et 7001 ( FT8), connexion avec le nom seulement.
10. D. Mills, RFC 4330, version 4 de l'IETF 2006. Format du paquet, calcul des déchets et délai de retour. Référence générale, non annexée.
11. NTP Pool Project (ntppool.org); INRIM, NTP time service (ntp1/ntp2.inrim.it); PTB (ptbtime1.ptb.de). Serveurs utilisés pour le contrôle du temps. Référence générale, non annexée.
12. Avalonia UI, documentation officielle (docs.avaloniaui.net). Interface multiplateforme, thèmes et mise en page de droite à gauche. Référence générale, non annexée.
13. MQTTnet, bibliothèque.NET pour MQTT (github.com/dotnet/MQTTnet). Client utilisé pour le flux PSKReporter. Référence générale, non annexée.
14. SQLite (sqlite.org) et Microsoft. Données.Sqlite (learn.microsoft.com). Archive dans un seul fichier. Référence générale, non annexée.
15. Microsoft,.NET 10 – systèmes d'exploitation supportés et publication autonome dans un seul fichier (learn.microsoft.com/dotnet). Plateformes et limites indiquées à la section 13. Référence générale, non annexée.
16. ECMA-376 -Open Office Formats de fichiers XML, partie 1, section 22.1 (Langage de balisage en mathématiques de bureau). Structure du fichier.docx et des équations. Référence générale, non annexée.
17. Liste des 28 langues du wiki YA-MS, fournie par l'auteur du programme.
18. Open-Meteo, API de prévisions météorologiques (open-meteo.com), avec les données du modèle NOAA GFS. Pression au niveau de la mer pour les isobares (voir document 2).
19. J. P. Snyder, Projections Map – A Working Manual, USGS Professional Paper 1395, 1987, et méthodes de transformation polynôme standard pour les images géoréférencées. Référence générale, non annexée.
20. L'Almanac astronomique, section D, Formules de faible précision pour la Lune (position de précision d'environ 0,3°). Utilisé pour reconnaître les connexions EME. Référence générale, non annexée.
