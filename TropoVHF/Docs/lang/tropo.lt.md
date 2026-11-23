LIVETROPOVHF · techninis dokumentas 3
# Kaip suformuoti tropą 144 MHz ir didesniu dažniu

> Mašininis vertimas iš italų kalbos: kilus abejonių, galioja itališkas vadovas.

Kas nutinka ore, kai 144, 432 ir mikrobangų bangos peržengia horizontą: refrakcijos, inversijos, ortakiai, barstytuvai, ir kaip juos perskaityti programos žemėlapyje. 2026 m. lapkričio 9 d. redakcija.

## Apibendrinant

- VHF bangos ir už kelionės į pirmuosius kilometrus atmosferos (troposfera) ir lenkimo kur lūžio indeksas keičiasi su [3] [7] [11].
- Indeksas matuojamas lūžiu N, kuris priklauso nuo slėgio, temperatūros ir visų pirma nuo [11] drėgmės.
- Normalioje aplinkoje yra apie 40 vienetų / km; kai jis nukrenta daugiau nei 157 vienetai / km, banga lieka įstrigusi [7] [11] konduit.
- Konduit kilęs iš terminio apėjimo sausu oru virš ir šlapiu žemiau: anticiklonu, jūra, serinėmis naktimis, [3] [4] [5] [13] frontais.
- Kuo didesnis dažnis, tuo plonesnis ortakis: mikrobangos atsidaro dažniau nei 144 [3] [7].
- Be konduit lieka troposferos sklaida: visada pateikti, silpnas, kad patenka su atstumu [6].
- Programos: aukštas slėgis + atvirkštinis + Hepburn / F5LEN aukštas = galimos sąlygos; dėmės patvirtina [1] [2] [13] [14].

## 1. Kur tai vyksta: troposfera

Troposfera yra žemiausias atmosferos sluoksnis, iki 10-12 km. Dėl tropas skaičiuoti daugiausia pirmuosius 3 km: yra vandens garų ir inversijos, ir ten eina radijo maršrutus tarp stočių ant žemės [4]. Įprastomis sąlygomis temperatūra krenta apie 6,5 ° C / km [3] aukštyje. Kai, kita vertus, sluoksnis, temperatūra kyla su aukščio, tai yra beveik visų stiprių tropo angų pradžios taškas.

VHF neatspindi jonosfera kaip HF (sūrus ir sporadinis, kuris turi kitas valandas ir kitus atstumus [5]): už horizonto tik tada, kai atmosfera juos įsmunka į žemę arba išplinta.

## 2. Refrakcija ir refrakcijos N

Radijo bangos slenka oru su oro refrakcijos indeksas su kitu. n indeksas yra labai artimas 1, taigi naudojamas N reaktyvumas, t. y. n − 1 padaugintas iš milijono [11]:

```math
N = (n - 1) \cdot 10^{6} = \frac{77,6}{T} \left( P + 4810 \, \frac{e}{T} \right)
```

su P slėgis hPa, T temperatūra kelvino ir vandens garų slėgis hPa. N dirvožemis paprastai yra 300-400. Antrasis terminas, kad garo, sveria daug: dėl šios priežasties drėgmės skaičiuoja tiek, kiek [3] [11] temperatūra.

Kas lenkia banga yra ne N vertė, bet kaip ji keičiasi su kvota, gradientas dN / dh:

| dN / dh (N / km) | Padėtis | Poveikis bangai |
| 0 | Subrefrakcijos | Banga juda nuo žemės, trumpesnis horizontas |
| nuo 0 iki -79 | Normalus (standartas − 39) | Šviesos lenkimas į žemę: radijo horizontas apie 4 / 3 optinio |
| nuo -79 iki -157 | Superrefrakcijos | Banga lenkia daugiau, padidėjęs srautas |
| mažesnis nei − 157 | Kiekis | Banga lenkia tiek, kiek Žemė ar daugiau ir lieka įstrigę |

- 157 N / km slenkstis yra taškas, kuriame spindulio kreivė yra lygi Žemės [7] [11]. Taigi "ekvivalentinis antžeminis spindulys" yra 8497 km (4 / 3 tikrojo spindulio), naudojamas apskaičiuojant [6] vektorių.

## 3. Inversion: tropo variklis

Jei nuolydis mažesnis nei − 157 N / km, N turi suirti keliuose metrais aukščio. Taip atsitinka, kai virš šviežio ir drėgno oro sluoksnis randamas šiltesnis ir sausesnis: temperatūra pakyla ir garas krenta kartu, ir tiek sumažinti N [3] [5]. LA0BY nurodo inversijas iki 10- 15 ° C / km, lyginant su įprastu 6,5 ° C [3] sumažėjimu.

Inversijos atsiranda skirtingais būdais:

- Anticiklonų vartojimas. Aukšto slėgio aukštyje oras lėtai krenta, jis suspaudžia ir sušildo, o ant žemės lieka šviežias ir drėgnas oras. Tai yra ilgesnių ir ilgesnių angų priežastis, kuri paskutines dienas [3] [4] [13].
- Naktinė radiacija. Su ramiu dangumi ir silpnu vėju naktį dirvožemis aušina, o kontaktinis oras - Žema reverse, kad lūžta su rytine saule. Vakaro ir ryto angos, tipiškos vasaros ir rudens.
- Įspėjimas apie jūrą. Karštas ir sausas oras iš žemės per šaltesnę jūrą: jūros sluoksnis išlieka drėgnas ir vėsus žemiau. Iš čia puikūs jūrų maršrutai (Atlanto, Viduržemio jūros, Šiaurės jūros) [4] [13].
- Priešai. Priešais šiltą priekį karšto oro teka per šaltą; anga yra trumpas ir po kaktos.
- Garinimas. Virš jūros, per pirmuosius metrus, drėgmė greitai krenta su aukščio: labai plonas ortakio, naudinga tik mikrobangų bangų.

Ženklas, kad atvirkštinis yra vertinamas vertikaliuose profiliuose (radiosondaggi): temperatūra kyla ir rasos taškas krinta tame pačiame [5] skyriuje. Programa rodo šio clue su Thermal Inversion sluoksnis (oras 925 hPa šiltesnis nei ant žemės) ir su izoterminis 850 hPa.

## 4. Paviršiaus ir aukšti ortakiai

Kondutas yra sluoksnis, kuriame banga sklinda šokinėdama tarp viršutinio krašto (apverstinis) ir žemės ar apatinio krašto [7]:

- Paviršiaus vamzdynas: požeminis arba jūros. Žemo aukščio stotys, ypač pakrantėje, įvažiuoja tiesiai.
- Aukštas vamzdynas: stovi aukštyje, pavyzdžiui, 500-2000 m. Lygumų stotys gali nejausti, o kalvos ar kalno stotys patenka į santaką. Paaiškinkite nuorodas, kad kirsti Alps ar kitų [3] redukcijos [4].

Norėdami patekti į vamzdyną, banga turėtų pradėti beveik horizontaliai: žemų antenų horizonte ir jokių kaimyninių kliūčių. Viduje vamzdynas yra daug mažesnis nei laisvoje erdvėje, ir atstumai pasiekia 1000-2000 km ir už jos ribų (pavyzdžiui, jungtys tarp Iberijos pusiasalio, Kanarų salų ir Anglijos, arba tarp Skandinavijos ir Pietų Rusijos) [3].

## 5. Dažnis: kodėl mikrobangų krosnelės atsidaro dažniau

Kondutas spėja tik gana trumpas bangas, palyginti su jos storis. Ilgos bangos "išeina" iš plono ortakio, trumpos neišeina. Tada [3] [7]:

| Grupė | Wavelthrough | Ko jums reikia |
| 144 MHz |  2 m  | Stori vamzdynų (cennaia metrų), platus ir gerai pažymėtos versijos: rečiau, bet labai plačios angos |
| 432 MHz | 70 cm | Plonos briaunos: dažnai atsidaro, kai 144 yra tik "prekės" |
| 1296 MHz | 23 cm | Keletas silpnų inversijų; ilgi maršrutai jūroje dažnai vasarą |
| 10 GHz | 3 cm | Tik garinimo kanalas ant jūros; taip pat skaičiuoja lietaus absorbciją |

Dėl šios priežasties kuklus tropo indeksas jau gali reikšti angos UHF ir mikrobangų krosnelėje, o 144 reikalauja didesnių verčių. Kita vertus, tuo pačiu metu 144 gyventojai patiria mažiau nuostolių už tinklo ribų ir geriau dengia pasienio zonas.

## 6. Troposfer sklaida

Net be inversija troposfera nėra vienodas: Maža temperatūra ir drėgnumas skleidžia nedidelę energijos dalį už horizonto. Tai troposferinis sklaida, visada pateikti, kuris leidžia jungtis 300- 800 km su gera [6] stotys. Signalas yra silpnas ir svyruojantis; nuostolis auga su difuzijos kampas, tai yra su atstumu ir su kliūtimis horizonte:

```math
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
```

Difuzijos kampas θs laipsniais ir f MHz [6]. Programa naudoja šią formulę kiekvienos vietos skirtuką pasakyti, ar nuoroda gali būti paaiškinta su sklaidos arba reikalauja konduit (žr. dokumentą 1). Didelė N vertė žemėje pagerina [6] sklaidą.

Negalima painioti su oro sklaida (atspindys lėktuvuose: signalai kelias sekundes, Dopleris) ir sklaidos meteoras (atspindys meteoritai), kuri programa atima iš tropo.

## 7. Orų vaizdas: kaip jį perskaityti žemėlapyje

Tropo angos seka meteorologiją. Tipiškos situacijos [3] [4] [13]:

- Stabilus anticiklonas, aukšto slėgio (daugiau nei 1020- 1025 hPa), platus izobaras, mažas vėjas: subsidence atstatymas. Geriausios angos eina prie anticiklono krašto, ypač ant jūros.
- Calo della pressione: inversion pertraukos. G4MCU po SK6VHF švyturio, kol slėgis sumažės iki 1012 hPa [13].
- Karštas oras aukštyje (kyla iki 850 hPa) virš gryno oro ant žemės: atvirkštinis.
- Konvekcinės ląstelės ir gravitacijos bangos (pvz., po vėju Alpėse) gali sukurti vietines ir trumpas [4] angas.
- Dienos laikas: švitinimo apraiškos yra stipresnės vakare ir anksti ryte; panieka taip pat lieka dienos.

Hepburn [1] ir F5LEN [2] indeksai sujungia visa tai iš oro modelių: Jie apskaičiuoja N pagal įvairias kvotas ir paverčia nuolydžius į skaičių ar klasę. Aš nesu garantija: LA0BY rodo LA- UA6 įvykis, kuris yra matomas aukštos temperatūros, o ne Hepburn [3]. Dėl šio programos rodo kartu indeksai, izobare, temperatūros ir realių dėmių: jei dėmės susilygiuoja su aukšto indekso plotą pagal anticiklonas, atidarymo vyksta. KP4MD nustatė, kad WSPR SNR yra 144 MHz ir auga tiesiai į Hepburn [14] pateiktus langus.

## 8. Praktinis sąrašas

1. Pažvelkite į tropo indeksą savo srityje ir išilgai kryptimi, kad jus domina, per kelias ateinančias valandas.
2. Patikrinkite izobare: aukštas slėgis ir platus izobare virš arba šalia kelio.
3. Įjunkite šiluminę indukciją ir T 850 hPa: karštas oras aukštyje virš kelio.
4. Geriausi maršrutai ant jūros ir vakare ir ryte valandų.
5. Jei esate ant kalno, galvoti apie aukštos ortakiai; Jei esate pakrantėje, ant paviršiaus.
6. Pirmiausia pabandykite aukščiausias juostas (432, 1296): atidaryti pirmą.
7. Patikrinti vietoje ir PSKReporter: ilgosios linijos ta pačia kryptimi patvirtinti ortakio.

## Šaltiniai

1. W. R. Hepburn, DX Info Centre, "The Hepburn Tropo Index (HTI)" ir "Tropospheric Ducting Forecast" puslapiai (dxinfocentre.com).
2. F5LEN, prognozė Europai ir pasaulinei VHF tropošerijos sklidimo prognozei (tropo.f5len.org).
3. S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", 41 ° Nordic VUSHF Meeting, Skjeberg 2019. Normalus gradientas ir inversijos, temperatūra ir drėgmė, didelis slėgis, LA- UA6 įvykis, jungtys už kalnų.
4. F. Egano IK3XTV, "Paplitimas VHF - Konvekcinių ląstelių ir atmosferos gravitacijos bangų indėlis į Tropo plitimą". Pirmieji 3 km, jūrų keliai, konvekcinės ląstelės, gravitacijos bangos, aukštas slėgis.
5. V. Grasmann DF5AI, U. Langenohl DK5YA, "Labai ilgas atstumas Propagation in the 144 MHz Band", 24 / 7 / 2003. Temperatūros ir rasos taško profiliai, inversijos, skirtumas nuo E sporadic.
6. J. N. Gannaway G3YGF, "Tropospheric Scatter Propagation", QST, 1983 m. lapkričio mėn. Žemės spindulys tolygus ir sklaidos praradimas.
7. VHF / UHF DX knyga, 2 skyrius "Troposferos sklidimas". Reaktyviosios zonos pagal dN / dh ir paviršiaus bei aukšto ortakio profilius (tik skaičiai).
11. Rekomendacija ITU- R P.453, radijo lūžio rodiklis. N formulė ir ortakio slenkstis - 157 N / km. Bendroji nuoroda, nepateikta priede.
13. G4MCU, "troposferos" straipsniai (g4mcu.net), 2018- 2025. Condotti su dideliu slėgiu, ypač ant jūros; SK6VHF ir sumažinti iki 1012 hPa.
14. C. F. Milazzo KP4MD, "VHF / UHF WSPR Propagation Study", 2012- 2014. SNR WSPR 144 MHz ir Hepburn prognozė.

Šaltinis numeriai yra tas pats kaip dokumentas 2, Žemėlapis pastatas.
