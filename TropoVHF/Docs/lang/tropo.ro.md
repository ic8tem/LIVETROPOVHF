LIVETROPOVHF · document tehnic 3
# Cum se formează tropa în 144 MHz și dincolo de aceasta

> Traducere automată de italiană: în caz de îndoială merită ghidul italian.

Ce se întâmplă în aer când 144, 432 şi microundele trec dincolo de orizont: refracţie, inversiune, conducte, dispersări şi cum să le citească pe harta programului. Versiunea din 9 noiembrie 2026.

## Pe scurt

- Valurile VHF și dincolo de călătorie în primii kilometri de atmosferă (troposfera) și îndoiți-vă unde indicele de refracție se schimbă cu [3] [7] [11].
- Indicele se măsoară cu N refractiv, care depinde de presiune, temperatură și mai ales de umiditatea [11].
- În atmosferă normală există aproximativ 40 de unități pe km; atunci când scade mai mult de 157 de unități pe km, valul rămâne blocat într-o conductă [7] [11].
- Conducta vine dintr-o inversie termica cu aer uscat deasupra si umed de mai jos: anticiclon, mare, nopti senine, [3] [4] [5] [13] fronturi.
- Cu cât frecvența este mai mare, cu atât conducta este mai subțire: microundele se deschid mai des decât 144 [3] [7].
- Fără conductă rămâne tropospher scatter: întotdeauna prezent, slab, care cade cu distanța [6].
- Pe program: presiune mare + inversare + Hepburn / F5LEN mare = condiții probabile; spoturile confirmă [1] [2] [13] [14].

## 1. Unde se întâmplă: troposfera

Troposfera este stratul cel mai de jos al atmosferei, până la aproximativ 10/12 km. Pentru trope conta în principal primii 3 km: există vapori de apă și inversiuni, și acolo trece rutele radio între stațiile de pe sol [4]. În condiții normale temperatura scade cu altitudinea de aproximativ 6,5 °C pe km [3]. Când, pe de altă parte, într-un strat, temperatura creşte odată cu altitudinea, este punctul de plecare al aproape tuturor deschiderilor puternice ale tropo-ului.

VHF nu sunt reflectate de ionosferă ca fiind HF (salvare și sporadic, care are alte ore și alte distanțe [5]): dincolo de orizont vin numai în cazul în care atmosfera le îndoaie la sol sau le extinde.

## 2. Refracție și refracție N

Unda radio se îndoaie prin aer cu un indice de refracţie a aerului cu altul. Indicele n este foarte aproape de 1, deci se utilizează reactivitatea N, adică n - 1 înmulțită cu un milion [11]:

```math
N = (n - 1) \cdot 10^{6} = \frac{77,6}{T} \left( P + 4810 \, \frac{e}{T} \right)
```

cu P presiunea în hPa, T temperatura în kelvin și presiunea vaporilor de apă în hPa. N sol este, de obicei 300 de ani. Al doilea termen, cel al aburului, cântărește foarte mult: din acest motiv umiditatea contează la fel de mult ca temperatura [3] [11].

Ceea ce îndoaie valul nu este valoarea N ci modul în care se schimbă cu cota, gradient dN/dh:

| dN/dh (N/km) | Situația | Efectul asupra valului |
| 0 | Subrefracție | Valul se îndepărtează de pământ, orizontul mai scurt |
| de la 0 la −79 | Normal (standard − 39) | Îndoaie lumina la sol: orizont radio aproximativ 4/3 din cel optic |
| de la -79 la -157 | Superrefracție | Valul se îndoaie mai mult, creşterea debitelor |
| sub -157 | Conţinut | Valul se îndoaie la fel de mult ca Pământul sau mai mult și rămâne blocat |

Prag de -157 N/km este punctul în care curbura razei este egală cu cea a Pământului [7] [11]. Prin urmare, "rază terestră echivalentă" de 8497 km (4/3 din raza reală) utilizată la calcularea Scatterului [6].

## 3. Inversiuni: motorul tropo

Un gradient sub -157 N/km necesită N să se prăbușească în câțiva metri de altitudine. Se întâmplă atunci când deasupra unui strat de aer proaspăt și umed se găsește cald și uscat: temperatura crește și aburul cade împreună, și ambele aduc în jos N [3] [5]. LA0BY indică inversări de până la 10 [3]

Inversiuni apar în moduri diferite:

- Subsidence în anticiclone. În presiunea ridicată aerul de la altitudine scade încet, se comprimă și se încălzește, în timp ce pe sol rămâne aer proaspăt și umed. Este cauza deschiderilor mai lungi și mai lungi, care durează zile [3] [4] [13].
- Radiaţii nocturne. Cu cer senin și vânt slab solul se răcește noaptea și răcește aerul de contact: Inversare joasă care se rupe cu soarele dimineața. Deschise seara şi dimineaţa, tipic verii şi toamnei.
- Avertizare la mare. Aerul cald și uscat care vine de la sol peste o mare mai rece: stratul mării rămâne umed și rece dedesubt. De aici marile rute marine (atlantice, mediteraneene, Marea Nordului) [4] [13].
- Fronturi. În fața unui front cald aerul cald curge peste cel rece; deschiderea este scurtă și urmează fruntea.
- Evaporare. Deasupra marii, in primii metri, umiditatea scade rapid cu altitudinea: conducta foarte subtire, utila doar microundelor.

Semnul că un revers este văzut în profilurile verticale (radiosondaggi): temperatura în creștere și punctul de rouă merge în jos în aceeași secțiune [5]. Programul arată un indiciu de acest lucru cu stratul de Inversiune termică (aer la 925 hPa mai cald decât pe sol) și cu izotermală la 850 hPa.

## 4. Suprafețe și conducte înalte

O conductă este un strat în care valul se deplasează prin viguros între marginea superioară (inversiune) și partea de jos sau la sol [7]:

- Conductă de suprafață: sol sau mare. Staţiile de altitudine scăzută, în special pe coastă, intră direct.
- Conductă înaltă: stă la altitudine, de exemplu între 500 și 2000 m. Este posibil ca staţiile din câmpii să nu se simtă, în timp ce cele din deal sau din munte intră în conductă. Explică legăturile care traversează Alpii sau alte reliefuri [3] [4].

Pentru a intra în conductă valul ar trebui să înceapă aproape orizontal: antene mici la orizont și nici un obstacol vecin. În interiorul conductei, reducerea este mult mai mică decât în spațiul liber, iar distanțele ajung la 1 000.2000 km și dincolo (de exemplu, conexiunile dintre Peninsula Iberică, Insulele Canare și Anglia, sau între Scandinavia și sudul Rusiei) [3].

## 5. Frecvenţa: de ce microundele se deschid mai des

O conductă capturează doar unde scurte în comparaţie cu grosimea ei. Valurile lungi ies dintr-o conductă subţire; cele scurte nu. Apoi [3] [7]:

| Bandă | Lungimea valului | Ce ai nevoie |
| 144 MHz |  2 m  | Conducte groase (centinaia de metri), versiuni largi și bine marcate: deschideri mai puțin frecvente, dar foarte extinse |
| 432 MHz | 70 cm | Margini subtiri: de multe ori se deschide atunci când 144 sunt doar |
| 1296 MHz | 23 cm | Puţine inversiune slabe; trasee lungi pe mare frecvente vara |
| 10 GHz | 3 cm | Doar conducta de evaporare de pe mare; de asemenea, contează absorbția ploii |

Din acest motiv, un indice tropo modest poate însemna deja deschideri în UHF și cuptor cu microunde, în timp ce 144 solicită valori mai mari. Pe de altă parte, la aceeași conductă, cei 144 au mai puține pierderi în afara conductei și acoperă mai bine zonele de frontieră.

## 6. Troposferul se împrăştie.

Chiar şi fără inversiune troposfera nu este uniformă: Iregularităţi minore de temperatură şi umiditate împrăştie o mică parte din energie dincolo de orizont. Este troposferic scatter, întotdeauna prezent, care permite conexiuni de 300 Semnalul este slab și fluctuant; pierderea crește cu unghiul de difuzie, care este cu distanța și cu obstacolele la orizont: [6]

```math
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
```

cu θ unghiul de difuzie în grade și f în MHz [6]. Programul utilizează această formulă în fila fiecărui loc pentru a spune dacă un link poate fi explicat cu scatter sau necesită o conductă (a se vedea documentul 1). O valoare N mare pe teren îmbunătățește împrăștierea [6].

A nu se confunda cu airscatter (reflecție pe avioane: semnale de câteva secunde, Doppler) și meteoritul scatter (reflecție pe meteoriți), pe care programul îl exclude din tropo.

## 7. Imaginea meteo: cum se citeşte pe hartă

Deschiderile tropo urmează meteorologia. Situaţii tipice [3] [4] [13]:

- Anticiclon stabil, presiune mare (mai mult de 1020 Cele mai bune deschideri urmează marginea anticiclonului, în special pe mare.
- Calo della pressione: inversiunea se rupe. G4MCU a urmat baliza SK6VHF până când presiunea scade la 1012 hPa [13].
- Aerul cald la altitudine (este altul la 850 hPa în creștere) peste aerul proaspăt pe sol: invers.
- Celulele convective și undele gravitaționale (de exemplu, sub vânt în Alpi) pot crea deschideri locale și scurte [4].
- Timpul zilei: inversiunea iradierii este mai puternică seara şi dimineaţa devreme; subsidenţa rămâne şi a zilei.

Indicii Hepburn [1] și F5LEN [2] pun toate acestea împreună de la modele meteo: Ele calculează N la diferite cote și transformă gradienții într-un număr sau o clasă. Nu sunt o garantie: LA0BY prezinta un eveniment LA Pentru aceasta programul prezinta impreuna indexuri, izobare, temperaturi si spoturi reale: daca spoturile se aliniază la un indice ridicat sub un anticiclon, deschiderea este in curs de desfăşurare. KP4MD a găsit SNR de rapoarte WSPR la 144 MHz în creștere chiar în ferestrele furnizate de Hepburn [14]. UA6 [3]

## 8. Lista practică

1. Uită-te la indexul tropo pe zona ta şi de-a lungul direcţiei care te interesează, pentru următoarele câteva ore.
2. Verificați izobrare: presiune mare și izobrare larg peste sau lângă calea.
3. Porniți Inversiunea termică și T 850 hPa: aer cald la altitudinea de deasupra căii.
4. Preferă rutele pe mare şi seara şi dimineaţa.
5. Dacă sunteţi pe deal, gândiţi-vă la conductele înalte; Dacă eşti pe coastă, la suprafaţă.
6. Încearcă mai întâi cele mai înalte benzi (432, 1296): deschide primul.
7. Verificați cu spot și PSKReporter: linii lungi în aceeași direcție confirmă conducta.

## Surse

1. W. R. Hepburn, Centrul de Informaţii DX,
2. F5LEN, prognoza pentru Europa și predicția mondială a propagării tropopherice a VHF (tropo.f5len.org).
3. S. Heck LA0BY, Gradient normal și inversii, temperatura și umiditatea, presiuni mari, LA UA6
4. F. Egano IK3XTV, Primii 3 km, căi marine, celule convective, unde gravitaţionale, presiune ridicată.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, Profiluri de temperatură și puncte de rouă, inversiune, distincție de E sporadic.
6. J. N. Gannaway G3YGF, Echivalentă cu raza Pământului şi pierdere prin împrăştiere.
7. Cartea VHF/UHF DX, capitolul 2 Zone reactive în conformitate cu dN/dh și profilele de suprafață și de înaltă conductă (numai cifrele).
11. Recomandarea UIT-R P.453, indice de refracție radio. N formula și pragul conductei la −157 N/km. Referinţă generală, neanexată.
13. G4MCU, Condotti cu presiune mare, in special pe mare; SK6VHF si scade la 1012 hPa.
14. C. F. Milazzo KP4MD, SNR WSPR la 144 MHz și prognoza Hepburn.

Numerele sursei sunt identice cu documentul 2, Harta Building.
