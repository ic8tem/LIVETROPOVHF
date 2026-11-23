## 0

Public FM-DX Webserver (servers.fmdx.org, open source project by M. Farkaš): le 60 le plus proche à moins de 1500 km, toutes les trois minutes, sur leur API publique. Si un serveur Web reçoit une station FM à plus de 150 km avec un signal d'au moins 10 dBf (transmetteur reconnu par le PI RDS), la réception apparaît sur la carte comme une ligne violette; ceux de plus de 300 km entrent dans les couloirs de la bande 144, parce que le tropo en FM (88–108 MHz) va de pair avec les 2 m.

## 1

AIS de votre récepteur (AIS-catcher, rtl-ais ou un récepteur de sortie NMEA): le programme écoute le! AIVDM phrases en UDP sur le port 10110 (ais_port en LIVETROPOVHF.ini) et compare la distance de chaque navire, station de base ou radiofaro avec l'horizon radio normal à 162 MHz, 4,12·( √h navire + √h antenne) km. Les navires entendus plus de 1,5 fois l'horizon et plus de 50 km apparaissent comme des lignes bleues de votre QTH: sur la mer sont le signe le plus direct d'un conduit. Je m'en vais jusqu'à ce que tu l'allumes. Manuel des prévisions, § 4 ter.

## 6

Comment faire confiance au gain attendu: élevé, moyen ou faible. C'est une somme de points, pas une probabilité: accord entre les modèles (perte prévue contre P.452 avec les conduits et l'équation parabolique, base contre ITM: jusqu'à 3 dB +1, plus de 8 dB -1), avance (dans les 24 heures +1, plus de 72 -1), continuité de la couche sur la route (70% +1, moins de 30% -1) et sa persistance (+1), vérification de la GFS avec l'onde radio la plus proche (±1) et confirmation en direct le long de la route dans la dernière heure et un demi gain (cluster, PSKReporter, APRS, FM-DX: 1–2 High de 3 points, moyenne de 0 à 2, faible en dessous de zéro. Manuel de prévisions, § 10 septies.

## 8

Comparaison des émissions réelles de la station la plus proche (archive NOAA IGRA v2, lancements 00 et 12 UTC des 15 derniers jours; l'archive est mise à jour tous les quelques jours) et de l'archive GFS en même temps et au même point (Open-Meteo, prévisions historiques), avec la même analyse du programme: combien de fois le GFS a vu un conduit observé (M qui tombe avec le quota sous 3000 m, au moins 2 unités M), de fausses alarmes et le rejet moyen du gradient de réactivité au premier km. Il dit à quel point le modèle voit bien les conduits ici de nos jours. Données dans le dossier Data/IGRA. Manuel de prévisions, § 10 septies.
