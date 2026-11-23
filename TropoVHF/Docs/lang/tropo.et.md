LIVETROPOVH · tehniline dokument nr 3
# Kuidas moodustada trope 144 MHz ja kaugemal

> Automaatne tõlge itaalia keelest: kahtluse korral on see väärt Itaalia juhendit.

Mis juhtub õhus, kui 144, 432 ja mikrolained lähevad horisondi taha: murdumine, inversioon, kanalid, hajumine ja kuidas neid programmi kaardil lugeda. 9. novembri 2026. aasta versioon.

## Kokkuvõttes

- VHF lained ja kaugemal liiguvad atmosfääri esimestel kilomeetritel (troposfääris) ja painduvad, kus murdumisnäitaja muutub [3] [7] [11] -ga.
- Indeksit mõõdetakse murdumisteguriga N, mis sõltub rõhust, temperatuurist ja eelkõige [11] niiskusest.
- Tavalises atmosfääris on umbes 40 ühikut km kohta; kui rohkem kui 157 ühikut km kohta, on laine [7] [11] kanalisse lõksus.
- Kanal pärineb termilisest inversioonist, kus ülemine ja märg õhk on: antitsüklon, meri, rahulikud ööd, [3] [4] [5] [13] esiküljed.
- Mida kõrgem on sagedus, seda õhem on kanal: mikrolained avanevad sagedamini kui 144 [3] [7].
- Ilma kanalita jääb troposfääri hajumine: alati kohal, nõrk, mis langeb kaugusega [6].
- Programmis: kõrge rõhk + vastupidine + Hepburn/ F5LEN kõrge = võimalikud tingimused; laigud kinnitavad [1] [2] [13] [14].

## 1. Kus see juhtub: troposfäär

Troposfäär on atmosfääri madalaim kiht, kuni umbes 10–12 km. Trope'i jaoks loendatakse peamiselt esimesed 3 km: seal on veeaur ja inversioon ning seal läbivad raadioteed maapealsete jaamade vahel [4]. Normaalsetes tingimustes langeb temperatuur umbes 6, 5 °C km kohta [3]. Kui aga kihis tõuseb temperatuur koos kõrgusega, räägime termilisest inversioonist: see on peaaegu kõigi tugevate tropo avade lähtepunkt.

VHF-id ei peegeldu ionosfääris kui HF (salvesta ja sporaadiline, millel on teisi tunde ja muid kaugusi [5]): horisondi taha tulevad ainult siis, kui atmosfäär painutab neid maapinnale või levitab neid.

## 2. Murdumine ja murduv N

Raadiolaine paindub läbi õhu koos õhu murdumisnäitajaga teisega. Indeks n on väga lähedal 1-le, seega kasutatakse reaktiivsust N, st n − 1 korrutatuna miljoni [11] -ga:

```math
N = (n - 1) \cdot 10^{6} = \frac{77,6}{T} \left( P + 4810 \, \frac{e}{T} \right)
```

kus P on rõhk hPa, T temperatuur kelvinites ja veeauru rõhk hPas. N pinnas on tavaliselt 300–400. Teine termin, auru, kaalub palju: sel põhjusel loeb niiskus sama palju kui [3] [11] temperatuur.

See, mis lainet painutab, ei ole N väärtus, vaid see, kuidas see muutub kvoodiga, gradient dN/dh:

| dN/dh (N/km) | Olukord | Mõju lainele |
| 0 | Subrefraktsioon | Laine liigub maapinnast eemale, lühem horisont |
| 0 kuni −79 | Normaalne (standard −39) | Valguse painutamine maapinnale: raadiohorisont umbes 4/3 optilisest |
| alates −79 kuni −157 | Ülerefraktsioon | Laine paindub rohkem, suuremad voolukiirused |
| alla −157 | Sisu | Laine paindub sama palju kui Maa või rohkem ja jääb lõksu |

Lävi −157 N/km on punkt, kus raadiuse kõverus võrdub Maa [7] [11] omaga. Siit ka "ekvivalentne maakiir" 8497 km (4/3 tegelikust raadiusest), mida kasutati [6] hajumise arvutamisel.

## 3. Inversioonid: tropomootor

Gradient alla −157 N/km nõuab, et N variseks kokku mõne meetri kõrgusel. See juhtub siis, kui värske ja niiske õhu kihi kohal leitakse soojem ja kuiv: temperatuur tõuseb ja aur langeb kokku ja mõlemad vähendavad N [3] [5]. LA0BY [3]

Inversioonid tekivad erineval viisil:

- Vajumine antitsüklonisse. Kõrgrõhul langeb õhk kõrgusel aeglaselt, see surub ja soojeneb, maapinnal jääb värske ja niiske õhk. See on pikemate ja pikemate avade põhjus, mis kestab päeva [3] [4] [13].
- Öine kiiritus. Rahuliku taeva ja nõrga tuulega jahtub muld öösel ja jahutab kontaktõhku: Madal tagurpidi, mis puruneb hommikupäikesega. Õhtused ja hommikused avaused, tüüpilised suvel ja sügisel.
- Merehoiatus. Kuum ja kuiv õhk maapinnast üle külmema mere: merekiht jääb allpool niiskeks ja jahedaks. Siit suured mereteed (Atlandi, Vahemere, Põhjamere) [4] [13].
- Fronts. Sooja esiosa ees voolab kuum õhk üle külma; ava on lühike ja järgneb otsaesisele.
- Aurustumine. Mere kohal, esimestel meetritel langeb niiskus kõrgusega kiiresti: väga õhuke kanal, mis on kasulik ainult mikrolainetele.

Märk, et vertikaalprofiilides (radiosondaggi) on näha vastupidist: temperatuuri tõus ja kastepunkt laskub samas venitamises [5]. Programm näitab selle vihjet termilise inversiooni kihiga (õhk 925 hPa soojem kui maapinnal) ja isotermilise 850 hPa juures.

## 4. Pinna- ja kõrged kanalid

Kanal on kiht, milles laine liigub põrkudes ülemise serva (inversioon) ja maapinna või alumise serva vahel [7]:

- Pinnajuhe: maismaa- või merejuhe. Madala kõrgusega jaamad, eriti rannikul, sisenevad otse.
- Kõrgjuhe: seisab kõrgusel, näiteks 500–2000 m. Tasandike jaamad ei pruugi tunda, samal ajal kui mäes või mäes asuvad kanalid. Selgitage linke, mis ületavad Alpe või teisi [3] reljeefi [4].

Kanali sisenemiseks peaks laine algama peaaegu horisontaalselt: madalad antennid horisondil ja puuduvad naabertakistused. Kanali sees on sumbumine palju väiksem kui vabas ruumis ja vahemaad ulatuvad 1000–2000 km ja kaugemale (näiteks ühendused Pürenee poolsaare, Kanaari saarte ja Inglismaa vahel või Skandinaavia ja Lõuna-Venemaa vahel) [3].

## 5. Sagedus: miks mikrolained avanevad sagedamini

Kanal püüab ainult piisavalt lühikesi laineid kui selle paksus. Pikad lained "tulevad välja" õhukesest kanalist; lühikesed mitte. Seejärel [3] [7]:

| Band | lainepikkust | Mida sa vajad? |
| 144 MHz |  2 m  | Paksud torujuhtmed (meetrite sentinaia), laiad ja hästi märgistatud inversioonid: harvemad, kuid väga ulatuslikud avaused |
| 432 MHz | 70 cm | Õhemad torujuhtmed: sageli avatakse, kui 144 on ainult "kaubad" |
| 1296 MHz | 23 cm | Vähesed nõrgad inversioonid; pikad marsruudid merel sageli suvel |
| 10 GHz | 3 cm | Lihtsalt aurustumiskanal merel; loeb ka vihma imendumist |

Sel põhjusel võib tagasihoidlik tropoindeks juba tähendada avasid UHF-is ja mikrolaineahjus, samas kui 144 nõuab kõrgemaid väärtusi. Teisest küljest on 144-l samas kanalis vähem kahjusid väljaspool kanalit ja paremini katta piirialasid.

## 6. Troposferi hajumine

Isegi ilma inversioonita ei ole troposfäär ühtlane: Väikesed temperatuuri ja niiskuse ebakorrapärasused levivad väikese osa energiast väljapoole horisonti. See on troposfääri hajumine, alati olemas, mis võimaldab 300–800 km ühendusi heade [6] jaamadega. Signaal on nõrk ja kõikuv; kaotus kasvab difusiooni nurgaga, see tähendab kauguse ja horisondi takistustega:

```math
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
```

θs difusiooninurgaga kraadides ja f MHz-des [6]. Programm kasutab seda valemit iga koha vahekaardil, et öelda, kas linki saab selgitada hajumisega või nõuab kanalit (vt dokument 1). Kõrge N väärtus maapinnal parandab [6] hajumist.

Mitte segi ajada õhuhajumisega (peegeldumine lennukitel: mõne sekundi signaalid, Doppler) ja hajumise meteooriga (peegeldumine meteooridel), mille programm tropost välja jätab.

## 7. Ilmapilt: kuidas seda kaardil lugeda

Tropo avaused järgivad meteoroloogiat. Tüüpilised olukorrad [3] [4] [13]:

- Stabiilne antitsüklon, kõrgrõhkkond (rohkem kui 1020–1025 hPa), lai isobare, vähe tuult: vajumise tagasipööramine. Parimad avad järgivad antitsükloni serva, eriti merel.
- Calo della pressione: inversioon katkeb. G4MCU järgis SK6VHF majakat, kuni rõhk langeb 1012 hPa [13] - ni.
- Kuum õhk kõrgusel (hüsotermia kuni 850 hPa tõus) maapinnal värske õhu kohal: vastupidi.
- Konvektiivsed rakud ja gravitatsioonilained (nt Alpide alltuul) võivad luua lokaalseid ja lühikesi avasid [4].
- Päeva aeg: kiiritamise inversioonid on tugevamad õhtul ja varahommikul; vajumine jääb ka päevaks.

Hepburn [1] ja F5LEN [2] indeksid panevad selle kõik kokku ilmamudelitest: Nad arvutavad N-i erinevate kvootide alusel ja muudavad gradiendid arvuks või klassiks. Ma ei garanteeri: LA0BY näitab LA- UA6 sündmust, mis on nähtav kõrgel temperatuuril, mitte Hepburn [3] -s. Selleks näitab programm koos indekseid, isobare, temperatuure ja tegelikke kohti: kui laigud joonduvad antitsükloni all kõrge indeksiga alaga, on avamine käimas. KP4MD leidis, et WSPR-i aruannete SNR 144 MHz juures kasvab otse Hepburn [14] poolt pakutavates akendes.

## 8. Praktiline nimekiri

1. Vaadake tropo indeksit oma piirkonnas ja teid huvitavas suunas järgmise paari tunni jooksul.
2. Kontroll isobare: kõrge rõhk ja lai isobare tee kohal või selle kõrval.
3. Lülitage sisse termiline inversioon ja T 850 hPa: kuum õhk tee kohal kõrgusel.
4. Eelistame mereteid ning õhtust ja hommikut.
5. Kui olete mäel, mõelge kõrgetele kanalitele; Kui sa oled rannikul, pinnal.
6. Proovige kõigepealt kõrgeimaid bände (432, 1296): avage esimene.
7. Kontrollige kohapeal ja PSKReporteriga: pikad jooned samas suunas kinnitavad kanalit.

## Allikad

1. W. R. Hepburn, DX Infokeskus, "Hepburn Tropo Index (HTI)" ja "Tropospheric Ducting Forecast" leheküljed (dxinfocentre.com).
2. F5LEN, Euroopa ja maailma prognoos VHF troposfääri levimise kohta (tropo.f5len.org).
3. S. Heck LA0BY, „Extreme Tropo Propagation on 144 MHz and up, 41° Nordic VUSHF Meeting, Skjeberg 2019. Normaalne gradient ja inversioonid, temperatuur ja niiskus, kõrge rõhk, LA- UA6 sündmus, ühendused mägede taga.
4. F. Egano IK3XTV, „Propagation VHF – The contribution of convective cells and atmospheric gravitational waves in tropo propagation. Esimesed 3 km, mereteed, konvektiivrakud, gravitatsioonilained, kõrgrõhk.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, “Very Long Distance Propagation in the 144 MHz Band”, 24/7/2003. Temperatuuri- ja kastepunktiprofiilid, inversioonid, eristamine E-st.
6. J. N. Gannaway G3YGF, „Tropospheric Scatter Propagation, QST November 1983. Maa raadiuse ekvivalent ja hajumise kadu.
7. VHF/UHF DX Book, peatükk 2 „Tropospheric propagation. murdumistsoonid vastavalt dN/dh-le ning pinna- ja kõrgkanali profiilid (ainult arvud).
11. Soovitus ITU-R P.453, raadio murdumisnäitaja. N valem ja kanali lävi −157 N/km juures. Üldine viide, ei ole lisatud.
13. G4MCU, "troposfääri" artiklid (g4mcu.net), 2018–2025. Kondotti kõrge rõhuga, eriti merel; SK6VHF ja väheneb 1012 hPa-ni.
14. C. F. Milazzo KP4MD, „VHF/UHF WSPR Propagation Study, 2012–2014. SNR WSPR 144 MHz ja Hepburni prognoos.

Allikate numbrid on samad, mis dokumendil nr 2.
