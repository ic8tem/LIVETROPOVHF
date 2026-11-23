# Šaltiniai ir formulės

> Mašininis vertimas iš italų kalbos: kilus abejonių, galioja itališkas vadovas.

Duomenų rinkimas Pagrindinė fizika, turimi duomenys, paskelbti indeksai, formulės, radijo mėgėjų patirtis, patvirtinimas ir sprendimai. Formulės sunumeruotos kaip LaTeX 1, 2,..., o tekste, kurį jos primena, rašoma skliausteliuose. Prognozės vadove aprašoma, kaip programa juos naudoja.


## 1. Autoriaus patalpos

Name IC8TEM

- Italijos geografija reikalauja difrakcijos. Su keteros, promontory ir salos keliasdešimt kilometrų nuo stočių, difrakcijos įnašai turėtų būti apskaičiuojamas su Fresnel parametras ant pagrindinio krašto, ne tik su sklandžiai žemės. Susitikimas: lyginant netaisyklingą 563 MHz (Rio de Žaneiro, 27 taškai nuo 5 iki 30 km) teritoriją, geriausi metodai buvo P.526 su ašmenų kraštais, P.1812 ir difrakcijos pleištas, kurių vidutinė paklaida nuo -5 iki + 3 dB ir dispersija apie 9 dB; P.1546 buvo labiausiai pesimistas (iš Costa, Rabanal Ramirez ir Stačių šlaitų šalia PUOOL stoties, prisimena, kad jie tarnauja plačių kampinių propagatorių ir dviejų krypčių skaičiavimui (banga atspindi reljefų).
- Kad patektum į tolimą klubą, tau reikia Fresnelio. Jei kanalas prasideda už horizonto, signalas ateina pas mus difrakcijos virš kliūčių iki jo pradžios; nuo 1.3.2 programa apskaičiuoja (Vadovas prognozes, § 10 ter). Susitikimas: Rohde & Schwarz (2023) pažymi, kad stotys palei aukštos ortakio dažnai nepavyksta patekti, o tie arti baigiasi taip.
- Sąlygos greitai keičiasi. Teorijos yra ten, bet sąlygos keičiasi minutėmis, ir jokių prognozavimo sistema užima duomenis realiu laiku: GFS ateina kas 6 valandas, su laiko trukmė ir 0,25 °, ir prognozė gali prarasti vertę per trumpą laiką. Susitikimas: Bray (QST, November 1961) matavimo vidutinio valandinio svyravimo maždaug ± 7 dB per 100 mylių; ortakiai juda su atmosferos laiko (Rohde & Schwarz); mūsų QSO registras 29 / 12 / 2021-2 / 1 / 2022 rodo angas auga ir patenka per valandas.
- Geriausias dabar liejimo lieka DX cluster- PSKReporter tinklas. Norėdami sužinoti, kas vyksta dabar, klasterio dėmės, PSKReporter (ir WSPR) įveikti bet modelį: jie yra priemonės. Prognozė sako, kur ir kiek laiko ji gali trukti; reklamos sako, jei yra. Šio programos kelia vietoje ir prognozuoti tame pačiame žemėlapyje.

## 2. Tikslas

Idėja yra trečiasis indeksas, apskaičiuojamas pagal patį LIVETROPOVHF pradedant nuo vertikalių oro modelio profilių, kuris turi būti pridėtas prie Hepburn ir F5LEN vietoj jų pakeitimo. Šiandien programa skaito savo žemėlapius jau pasiruošę: indeksas taip pat sako * kodėl * yra atidarymo (kvotos ir storis, tinklo, minimalus juosta įstrigo) ir ji gali atsirasti su dėmės, kad programa renka jau.

- Ką jūs galite suteikti: tikimybė, kad vamzdynas per tašką ir per valandą, dalis pagrindo ir viršaus, storis, minimalus dažnis įkalinti, ir kelyje maršruto dalis viduje vamzdynas.
- Kas negali suteikti: ploni ortakiai (iki kelių dešimties metrų), kad pasauliniai modeliai 0,25 ° neišsprendžia, ir garavimo ortakiai jūroje, kurie tarnauja visų pirma iš GHz iki.
- Pirmasis praktinis tikslas: 144 ir 432 MHz Europoje, tikrinant DX klasterio taškus, PSSKReporter ir vietinius dekoderius.

## 3. Pagrindinė fizika

Viskas prasideda nuo refrakcijos N apskaičiuojamas kiekviename profilio lygyje: konduit - tai vieta, kur modifikuotas reaktyvumas M mažėja laipiojant, t. y. kai dN / dh sumažėja iki -157 N / km ([ITU- R P.453- 13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)).

Refrakcija (P ir hPa, T kelvinais):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Santykinio drėgnio H (%) ir t (° C) garų slėgis virš vandens nuo -40 iki + 50 ° C:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

Modifikuotas reaktyvumas (h km, arba 0,157 · z su z metrais) ir ortakio kriterijus:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN / dh (N / km) | Reglamentas | 144 MHz poveikis |
| 0 | subrefrakcijos | sumažintas srautas |
| nuo 0 iki -79 | normalus (standartas apie - 40) | 4 / 3 lygiaverčio spindulio radijo horizontas |
| nuo -79 iki -157 | superrefrakcijos | pailgėjęs srautas, "per geras" |
| žemiau − 15 | vamzdynas (spąstai) | užfiksuoti signalai, DX daugiau nei 1000 km |

Kaip išgauti N iš modelio slėgio lygių: kiekvienam lygiui jūs imtis T, santykinis drėgnumas ir geopotieninis aukštis, jūs apskaičiuojate N ir M, tada gradientas tarp kaimyninių lygių. Žemyn esantys lygiai turėtų būti išmesti (1000 hPa ant Alpių yra po žeme).

Minimalus įstrigo dažnis: vamzdynas veda tik bangos gana trumpas, palyginti su jos storis. Hepburn skelbia stalo storis → minimalus naudingas dažnis ([DX Info centras](https://www.dxinfocentre.com/propagation/luf.htm)); tai yra minkšta riba, pagal kurią signalas palaipsniui mažėja.

| Led Liga | Minimalus mokėjimo dažnumas |
|  61 m  | 962 MHz |
|  91 m  | 464 MHz |
|  122 m  | 276 MHz |
|  152 m  | 185 MHz |
|  183 m  | 133 MHz |
|  213 m  | 101 MHz |
|  305 m  | 53 MHz |

Be VHF diapazone (dažnai 120- 300 m) lentelėje vertės eiti gana gerai apie pastovus įstatymas; žemiau 100 m formulė nepakankamai įvertina dažnį iki 30- 40%. Prisitaikyti jį pats (ne Hepburn formulę), su f MHz ir d metrų:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

Praktikoje: už 2 m ji trunka konduit dažnai bent 170- 180 m, už 70 cm ji yra pakankamai apie pusę. Formulė cirkuliuoja ant tinklo, λmax = 0.014 · d ^ 1.5, ateina iš Kerr (1951, 1 skyrius, eq. 18) ir galioja su λ centimetrais ir stovi, už konduit, kurioje M patenka linijinis: su šiais vienetais suteikia apie 2 m 180 m ir apie 70 cm 89 m, tai yra tos pačios vertės Hepburn lentelės, kuri ateina iš jo. Kerr įspėja, kad tai nėra tinklo pjovimo: net bangos kelis kartus ilgiau kenčia nuo tinklo daug, ir būdas, kad "nuostolių" šiek tiek gali pagerinti priėmimo net stotys iš tinklo. 2019 m. IEEE Access tyrimas patvirtina, kad klasikiniai metodai (Kerr, Kukushchkin) neapibūdina realaus pjūvio ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)): riba turėtų būti naudojama kaip nuoroda, o ne kaip siena.

## 4. Duomenys prieinami nemokamai

Silpnas taškas yra vertikali rezoliucija: tarp 1000 ir 850 hPa modeliai suteikti lygį kas 25- 50 hPa, tai yra kas 200- 450 m, todėl jie mato gerai inversijos iki 500- 2000 m, bet ne plonas ortakiai netoli žemės. LIVETROPOVHF jau naudoja Open- Meteo izobare, temperatūra iki 850 hPa ir inversija: indeksas gali pradėti nuo tų pačių skambučių.

| Šaltinis | Kas suteikia | Skiriamoji geba | Naudojimas indeksui |
| [Open- Meteo, GFS](https://open-meteo.com/en/docs/gfs-api) | T, santykinis drėgnis, rasos taškas, geopozicinis aukštis pagal lygį (1000, 975, 950, 925, 900, 850 hPa...); ribinio sluoksnio aukštis | 0,25 ° pagal slėgio lygį; 16 dienų, iki 120 valandų, atnaujinama kas 6 valandas | N ir M profilis, gradientas, pagrindas ir ortakio viršus; jau programoje |
| [Open- Meteo, ECMWF](https://open-meteo.com/en/docs/ecmwf-api) | tokie patys lygiai nuo atvirųjų duomenų modeliai 0.25 °; žemės suteikia rasos taško |  0,25°  | antra nuomonė, naudinga siekiant suderinti modelius |
| [Open- Meteo, DWD ICON](https://open-meteo.com/en/docs/dwd-api) | T ir santykinė drėgmė slėgio lygiuose; ICON- D2 yra 800, 900 ir 925 hPa | ES apie 7 km | trečioji nuomonė dėl Europos |
| Vajomingas](https://github.com/NCAR/RAOBget) | matuojami profiliai, du išėjimai per dieną (00 ir 12 UTC), teksto formatas stočiai | daug reikšmingų lygių, net kas keletą tuzinų metrų | indekso patikra; žr. plonas ortakiai, kad modelis nemato |
| [GROANS, NOAA NCEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive) | istorinis transliavimo archyvas, kiekvienos stoties rinkmena, daugiau išvestų parametrų | kaip nurodyta anksčiau | metų duomenų kalibravimas ir palyginimas su praeities taškais |
| ERA5 (Copernicus) | Burnos reanalizė nuo 1940 m., 37 slėgio lygiai | apie 28 km | statistiniai duomenys ir kalibravimas, nenumatyti |

- Su Open- Meteo geotopogeninis aukštis yra virš jūros lygio: lygiai, kurie patenka po žeme turi būti išmesti ([Open- Meteo, viršutinis oras](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)).
- Ant žemės yra pridėta T ir drėgmė 2 m: jie suteikia pirmą atkarpą gradientas, vienas, kur naktinio paviršiaus kanalai gimsta ant lygumos ir ant jūros.
- Italijos transliacijos Vajomingo universiteto archyvo (weater.uwyo.edu / upperair / souming.html): stotys ir numeriai WMO toliau lentelėje.

| Stotis | W | Platuma | Ilguma | Dalys |
| Udine / Campoformido |  16044  |  46,03  |  13,18  |  94 m  |
| Milanas / Linatas |  16080  |  45,43  |  9,28  |  103 m  |
| Jūrų praktika |  16245  |  41,65  |  12,43  |  12 m  |
| BrindisiName |  16320  |  40,65  |  17,95  |  10 m  |
| Trapani / Birgi |  16429  |  37,92  |  12,50  | apie 14 m |

Tekstinis formatas Wyoming suteikia kiekvieno lygio slėgio, aukštis, temperatūra, rasos taškas ir drėgmė: pakankamai N ir M. Wyoming neturi oficialios sąsajos, scenarijai yra naudojami kaip [RAOBget NCAR](https://github.com/NCAR/RAOBget); DiuPage kolegija saugo kopijas tekstą pagal stotį ([pvz, 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)).](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. Indeksai ir paskelbti metodai

Hepburn ir F5LEN prasideda nuo tų pačių fizinių ingredientų, bet neskelbia visą algoritmą; mokslinių tyrimų 2008- 2026 vietoj apibūdinti metodą žingsnis po žingsnio, ir tai iš ten, kad verta kopijuoti receptą.

| Metodas | Pradiniai duomenys | Kas apskaičiuojama | Naudingos pastabos mums |
| [Hepburn Tropo Index](https://www.dxinfocentre.com/propagation/hti.htm) | žalios oro modeliai Kanados meteorologijos tarnyba (MSC, kaip parašyta jos žemėlapiuose; ne GFS / NAM), žemėlapiai, sukurti su GRADS, nuosavybės algoritmas ([storia](https://www.dxinfocentre.com/propagation/history.htm)) | 0 - 10 signalų išlinkio tiesinė skalė: 0 normalu, 1-2 superlūžis, 3-4 kai kurie ortakiai, 5 - 6 difuziniai ortakiai; neigiamos vertės = pusiau lūžis | indeksas turi būti imitacija, todėl trečiasis indeksas iš karto palyginti su pirmųjų dviejų |
| [F5LEN, pasaulinė prognozė](https://tropo.f5len.org/WW/info.php) | Europoje - ARPEGE Model of Météo-France, 7,5 km, more level from 2 m to 1500 m (pastaba autoriaus](https://tropo.f5len.org/author/f5len/)) | Vidutinis indeksas nuomotojas, 40 žingsnių nuo 3 valandų, 4 skaičiavimai per dieną, 28 dienų archyvas ([nauja versija](https://tropo.f5len.org/?p=504)) | patvirtinti, kad ji tarnauja modelis su daug žemo lygio |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf) | 60 ECMWF modelio lygiai nuo žemės iki 3000 m | mažiausia dN / dz kolonėlėje: atliekama pagal − 157 N / km, superrefrakcijos - pagal − 78,7 N / km; gaudyklės sluoksnio pagrindas | klimatologija: Padana plonas Europos teritorijoje su daugiau vamzdynų ištisus metus; Viduržemio jūros vasarą beveik 100%, žiemą mažiau nei 15%; Italija daugiau nei 30% vasaros naktį |
| [Indermuehle and Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861) | GFS 0,25 °, 21 slėgio lygis + laukai 2 m atstumu | M = N + 0,157 z; mažiausias dM / dz mažesnis nei 3 km, pagrindas ir ortakis, M trūkumo gylis; mažiau nei 40 m išmesti | 7 metus buvo tikrinamas 25 kanalų (nuo 88 MHz iki 2,68 GHz) atveju; geras pajėgumas iki maždaug 3 dienų iš anksto (AUC maždaug 0,74), tada nukrenta; GFS geresnis nei ERA5 |
| [Hämäläinen et al., TVF 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf) | Harmonija- AROME, 750 m arba 2,5 km, 65 arba 90 lygių | dM / dh < 0 modelio profilyje, palyginti su 4-59 m spinduliu | skaičiuoja daugiau vertikalios skiriamosios gebos negu horizontali skiriamoji geba; nė vienas modelis nemato ortakių žemiau 15 m; temperatūros gradiente vyrauja drėgmė |

Jau veikia trečioji radijo mėgėjų tarnyba: [TROPOCAST.EU](https://tropocast.eu/)of Artur SQ3A, 2 m ilgio ir 70 cm ilgio Europoje. Iš prognozės puslapio ([tropocast.eu / prognozė](https://tropocast.eu/forecast)):

- Duomenys: DWD ICON- EU (perėjimas 0.0625 °, apie 7 km), numatytasis šaltinis; arba Kanados GDPS (0,15 °), mažiau išsamus.
- Metodas: N ir M pagal ITU- R P.453 iš profilio tarp sluoksnio netoli žemės ir 850 hPa; sluoksnių pripažinimas, kanalų sąjunga prie žemės ir aukštos ir spalvų ribos yra tinkamas kalibravimas autoriaus, nepaskelbtas.
- Produktai: "pilnas" vaizdas 0- 3000 m, daugiau atskirų vaizdų "į žemę" ir "didelis" (šiuo metu tik žemėlapiai bus įtraukti); rezultatas 0- 100 į devynias klases (pagal 38 fone, 38- 52 marginal, 53- 58 silpnas, 59- 65 vidutinis, 66- 72 geras, 73- 80 labai geras, 81- 88 aukštas, 89- 95 stiprus, 96- 100 labai stiprus); prognozė iki + 72 h per valandą, su animacija ir archyvas.
- Teritorijos: Europa, Lenkija, Jungtinė Karalystė, Vokietija, Prancūzija, Ispanija, Italija, Čekija, Kroatija; sąsaja taip pat italų kalba.
- Pakartotinis naudojimas: žemėlapiai apima su API keyless iframe; taip pat yra dokumentais API (OpenAPI ir JSON schema). Licencija nėra nurodyta: prieš naudojant jį kaip trečią šaltinį šalia Hepburn ir F5LEN turėtumėte paklausti autoriaus.

Skirtumai nuo mūsų projekto: TROPOCAST sustoja 850 hPa (apie 1,5 km), o pakilę ortakiai pasiekia 2000 m ir toliau ( LA0BY atvejis 22 / 11 / 2018), ir nesuteikia juostos ar maršruto indeksas. 0-100 klasės gali būti lyginamos su 0-10 klasėmis tomis pačiomis dienomis.

TROPOCAST vaizdo struktūra (iš programos autoriaus pateiktos nuorodos): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, kur n yra prognozavimo žingsnis. Svetainė draudžia automatinį pasitraukimą į robotus, todėl aš negalėjau skaityti vaizdą; taip pat už tai, prieš rodant šiuos žemėlapius LIVETROPOVHF tarnauja autoriaus leidimą.

Notebook: [DJ5AR, kategorija](https://dj5ar.de/?cat=174)(Magonza, 21 straipsniai 2017- 2023, 2 m- 13 cm). Aprašytos bylos: kelias savaites iš Airijos (2023 m. gegužės 23 d.), iš FT8 į Baltarusiją (2020 m. rugsėjo mėn.), Italijos ir Vokietijos per Alpes ir Adrijos jūrą (2017 m. rugpjūčio mėn.). Pripažinti "per didelis" ortakio (reverse to 1000 m), kad jūs negalite naudoti iš žemės atveju: kitas patvirtinimas, kad reikia palyginti tinklo dalį su stočių. Jis naudoja radijo bangas kaip apertūros matavimo (iki 1028 km) ir vertikalių profilių Paryžiuje, Bordo ir DWD.

Apibendrinant, bendras receptas: vertikalus profilis → N ir M per lygį → sluoksnis su dM / dh neigiamas → bazė, viršuje, storis ir jėga ortakio → indeksas. "Slapta" dalis Hepburn yra empirinis kalibravimas realių įvykių, kuriuos mes galime perdaryti su dėmėmis.

## 6. Naudingos programos formulės

Be N ir M (pagrindinės fizikos skyrius), jie yra naudojami judėti iš vertikalaus į taško numerį ir tada į kelią. ITU formulės buvo rekonstruotos PDF formatu, kuris iš dalies skaitomas blogai; lapkričio 22 d. buvo peržiūrėtos oficialiuose tekstuose ([ITU-R P.452-18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453-14, P.526- 16, P.372-18) ir pataisytos kodu: konstanta su troposcatter 190 (ne 190,1) ir dujų absorbcija, esant optiniam srautui - 3 g / m3; α ne mažesnis kaip − 3,4 (eq. 55a); ortakių dujos, kai Δ- 7,5 + 2,5 · ω (eq. 9a); difrakcijos ant visą rutulio paviršių (eq. 23- 37: vidutinė sausumos - jūra, aukštis - 2 + 20 rąstų K, pagal optinį srautą - interpoliarizacija; Rumore P.372-18: 1 lentelė ir galaktika atitinka; Dirbtinio triukšmo kreivės yra iki 250 MHz ir galaktika iki maždaug 100 MHz, per programą išplečia tuos pačius įstatymus.

1. Gradientas tarp dviejų modelio lygių (h km, M - M):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. Stencils

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3. Žemės spindulio ekvivalentinis koeficientas (P.452-18, eq. 5 ir 6a), kai ΔN = vidutinis N kritimas per pirmąjį km, teigiamas, N / km:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

Su ΔN = 40 turite apie 4 / 3 k (standartinė atmosfera); su ΔN, kad artėja 157 k linkęs į begalybę, tai yra, Žemės radijas tampa plokščia: tai ortakio pradžia.

4. Aukštos antenos radijo horizontas h metrais, kurio spindulys lygus\ _ e kilometrais:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5. Tarpas platumai (P.452- 18, eq. 2), laiko procentinė dalis; Δ = maršruto centro platuma, μ1 ir μ4, atsižvelgiant į tai, kiek jis yra jūroje:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

Kai temperatūra 41 ° C N, visa jūra (μ1 = μ4 = 1), suteikia apie 11% laiko: yra klimatologinė bazė, naudinga kaip pamatinė vertė, nuo kurios skiriasi tikėtinas indeksas.

6. Troposcatter praradimas (P.452-18, eq. 45; f MHz, d km, θ kampas kelio mrad, N0 žemės reaktyvumas maršruto centre):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

Terminas − 0,15 · N0 rodo, kodėl drėgnas oras taip pat padeda be ortakio: 50 papildomų N vienetų yra 7,5 dB mažiau nei nuostoliai. LIVETROPOVHF jau naudoja sklaidos praradimo G3YGF versiją.

7. ortakis (P.452- 18, eq. 46- 51): fiksuotas sukabinimo įtaiso praradimas ir kampo miliradianto silpninimas, kuris auga su dažnių kubine šakna (f GHz, a\ _ e km):

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. Iš Bean and Dutton (1966) ir Kerr (1951)

Du klasikiniai tekstai suteikia tris dalykus, kurie yra reikalingi nedelsiant į indeksą: gradientas slenksčiai jau tartum ant to paties lygio slėgio GFS, ką verta dB reaktyvumo pokytis, ir ką oro situacijos sukurti arba sunaikinti ortakius. * Radijo meteorologija *, NBS monografija 92.

1. Profilių klasifikacija slėgio sluoksniams (Bean ir Dutton, tab. 6.5, p. 248; − dN / dh N / km). Skirtas radijo bangų ilgiams, jis taikomas, nes jis yra modelio lygiuose:

| Keliai | Akcijos apie | Subrefrakcijos | Nelaminuoti | Superrefrakcijos |
| 1000-850 hPa | 0- 1,46 km | − dN / dh < 0 | nuo 20 iki 60 | daugiau nei 100 |
| 850- 700 hPa | 1,46-3,01 km | − dN / dh < 0 | nuo 20 iki 50 | daugiau kaip 80 |
| 700- 600 hPa | 3 01- 4,20 km | − dN / dh < 0 | nuo 15 iki 40 | daugiau kaip 70 |
| 600-500 | 4,20-5,57 km | − dN / dh < 0 | nuo 10 iki 30 | daugiau nei 50 |

2. Kiek verta vertės dB (Bean and Dutton, cap. 6, pp. 233-258), priemonės 70-180 MHz maršrutuose už horizonto:

- Bet koks sluoksnis (ant žemės arba aukštyje), palyginti su "sklandžiai" profilis kelia signalą 10-25 dB į visus procentus; didesnis pelnas ateina iš aukšto pakreipti sluoksniai, tikriausiai dėl fokusavimo poveikį (kursas 200 km).
- Žemės reaktyvumas Ns ir pirmojo kilometro ΔN sumažėjimas yra susiję su mėnesio signalo medianiniu koeficientu nuo 0,4 iki 0,95, vidutinis 0,7 (20 maršrutų nuo 130 iki 446 km, apie 100 MHz).
- Metinis ciklas: 0,18 dB N vienetui; kasdienis ciklas nuo 0,2 iki 1,1 dB N vienetui, maksimalus maršrutuose 175- 200 km ir žiemą.
- Sudėtinis parametras Misme apskaičiavo lauko variaciją kaip 0,5 dB / (N / km) · (lygiavertis gradientas -40) plius terminio stabilumo terminas; Turbulence D parametras (plotas tarp realaus profilio ir adiabatic vienas, 0-3 km) tiesiog pagerina palyginti su paprasta ΔN. Autorių išvada: ΔN išlieka geriausias parametras paprastumui.

3. Referencinis profilis ir anomalija (Bean and Dutton, 3.8 ir 3.9 punktai). Atskaitos CRPL eksponentinė atmosfera suteikia "normalų" profilį pradedant nuo N; A vienetas matuoja, kiek skiriasi tikrasis profilis kiekvienoje akcijoje. Tai natūralus būdas transformuoti GFS profilį į anomaliją įdėti į žemėlapį:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

Kai N = 313, ΔN yra apie − 42 N / km, tai yra lygiavertis spindulys 4 / 3. - 157 N / km vamzdyno kriterijus taip pat gaunamas čia pagal Snell dėsnį horizontaliu spinduliu (eq. 4.23), beveik nepriklausomai nuo antenos aukščio.

4. Kur ir kada gimsta vamzdynai (Bean and Dutton § 4, 4; Kerr cap. 3):

| Procesas | Padėtis | Poveikis |
| Įspėjimas | karštas ir sausas oras iš šaltesne jūra tekančios žemės (būdingas vasaros rankovėms po gerų oro sąlygų dienų); daugiausia atsižvelgiama į drėgmės trūkumą | intensyvus kanalas ant jūros |
| Naktinis švitinimas | raminantis dangus, silpnas vėjas; maži debesys arba daug drėgmės užkirsti kelią tai | vedė prie žemės, kuri gimė saulėlydžio, auga vakare ir greitai išnyksta po saulėtekio, ypač viduje |
| Subsidence | oras, kuris kyla iš anticiklono, šildomas ir sausas, virš šviežio ir drėgno oro; dažni potvyniai kalnuose ir pietrytiniuose anticiklonų sektoriuose | viršutinis lūžio sluoksnis arba aukšto ortakio; beveik visada gradientas didesnis nei standartas atvirkštinėje padėtyje |
| Jūra ir žemė | pakrantė, cirkuliacija kelių šimtų metrų | vietiniai vamzdynai palei pakrantę |
| Prieš sujungimą | maišymas: žemė šildoma saulės tarp vidurdienio ryte ir po pietų, vėjas į žemę per keletą m / s neapdorotoje vietovėje; Naktinis rūkas | tikimybė, kad konduit beveik nieko |

5. Du nauji ingredientai išbandyti. Vertikalus judėjimas: kylančios srovės konvergencijos zonose pašalina ir ištrina sluoksnius, posidencija sustiprina signalą (Moler ir Holden: vidutiniškai apie 2,5 dB daugiau nei su įpūtimo oru, jūroje L ir X diapazone). Open- Meteo jau teikia vertikalų greitį slėgio lygių. Ir slėgio tendencija: 140 km kursas 186 MHz signalas nukrito, kai slėgis nukrito iki siųstuvo ir kelio centre ir grįžo aukštas ir be evanescence, kai jis pakilo, tik su 2 (Bean and Dutton, fig. 6.9, p. 241).

6. Iš Kerr. Be maksimalaus bangų ilgio formulės (pagrindinė fizika), Kerr suteikia apytikslę formulę M nuo vienintelės temperatūros, garo ir aukščio (§ 3.9, su senais 79 ir 4800 konstantų tada pakeistas 77,6 ir 4810) ir pastebi, kad į atvirkštinę vėjo pjovimo gali sukelti bangas, kad sūpynės aukštyje ortake, galima priežastis giliai pabėgti (§ 3.26). Italijos šaltinis pranešė Bean ir Dutton ir ieškoti: L. Bonavoglia (gruodis 1958), "Atitiktis tarp meteorologinių reiškinių ir dauginimo"...

## 8. Iš mėgėjų radijo prezentacijų ir techninių ataskaitų (antroji serija)

Septyni dokumentai pateikti programos autoriaus. Trys tarnauti tiesiai į indeksą: W3SZ už troposcatter "grindys", LA0BY atvejais, kad Hepburn nebuvo prognozuojama, Hepburn (vertimas LU7MC) už ortakių oro klasifikaciją.

1. Troposcatter: signalas, kuris visada egzistuoja. R. Rehr W3SZ, "Tropospheric Scatter (Troposcatter) Propagation for VHF, UHF, Mikrobangų ir dažnių", Pack Rats Conference 2014; J. R. Duffey KK6MC ", The Troposcatter Propagation Mode and How to Predict Coverage on the VHF / UHF Bands", CSVHF 2017.

- Naudinga nuo 100 iki 700 km, nuo 144 MHz iki 10 GHz; nuostolių 30 rąstų f su dažniu.
- Visų pirma jis skaičiuoja pradinį horizonto kampą: kiekvienas papildomas laipsnis kainuoja apie 9- 12 dB ( M1CNK, Collins metodas).
- Vasarą signalas vidutiniškai yra apie 10 dB didesnis nei žiemą; vidurdienio popietę 5 dB mažesnis nei anksti ryte arba vakare. KK6MC rodo bendrą 13-15 dB kintamumą laiko, sezono ir laiko atžvilgiu.
- Yeh metodas, lengviausia programuoti (f MHz, d mylių, θ kampas sklaidos laipsniais, α geometrinis abiejų ryšulių angų vidurkis):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Larsen (1968 m.) palyginimas 15 realių jungčių: vidutinė kvadratinė paklaida 5,1 dB Collins, 7,4 Yeh, 8,1 NBS ir CCIR, 16,8 Rider. Visi metodai pagerina, jei Ns išlaiko pastovus (Yeh lašai iki 5,4 dB): sklaidos dienos lūžio mažai skaičiuoja, todėl tropo indeksas turi pažvelgti į sluoksnius, ne tik N. 15 jungčių lentelė su išmatuotais nuostoliais (įskaitant Feldberg- Monte Baldo, 322 km 957 MHz, 223,1 dB; Izraelis 319 km, 77,5 MHz, 163,3 dB) yra geras bandymų stendas mūsų skaičiavimui.
- KK6MC nulemia nuostolį, kurį tipinė stotis sugeba viršyti 2 m, o atitinkamas atstumas - nuo 184 dB (FT- 817 ir 3 SSB elementai, apie 130 km) iki 215 dB (ilgi bunkeriai SSB, apie 430 km) ir 236 dB (kW ir su jagu, apie 640 km). Tai būdas išversti indeksas dBs į mylių uždirbo.
- Paruoštas kodas: W3SZ Yeh ir Collins programa (nitehawk.com / w3sz / YehCollins.zip) ir Bob Atkins KA1GT skaičiuoklė (bobatkins.com / radijas / troposcatter.html).

Kitas šaltinis dėl sklaidos: I. Roberts ZS6BTE, "Tropospheric Scatter Propagation", * Radio ZS *, gruodis 1984, su pastaba 2013 ([qsl.net / zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). Daugiausia Naudinga geometrija: sklaidos kampas θ = θ0 - θ1 - θ2, su θ0 = d / R, θ1 = (h1 − h\ _ ostacolo) / d1 + d1 / 2 R ir tas pats kitos stoties R = 8446 km (4 / 3 Žemės spindulio). Tai gabalas, kuris yra naudojamas apskaičiuoti kampus nuo žemės profilis į troposcatter "grindų". Kiti duomenys: kiekvienas kampas daugiau kainuoja apie 10 dB; perėjimas nuo N 280 iki 300 sumažina nuostolių apie 4 dB; sklaidos tūris yra tarp 300 ir 2000 m 150 km, tarp 600 ir 3000 m 300 km; laikas sklaidos apimtis kelia signalo apie 20 dB. skaitmeninis pavyzdys straipsnio negrįžta su nurodytomis formulėmis (laisvos erdvės 900 MHz ir 872 km terminas yra apie 150 dB, ne 125), todėl formulės turėtų būti patikrinti W3SZ prieš juos naudojant.

2. Konduit tipai (W. R. Hepburn "Tropospheric Propagation Modes", dxinfocent.com / distribution / tr- modes.htm, Ispanų vertimas LU7MC). Užpildykite Bean ir Dutton lentelę naudingais atvejais Italijoje:

| Tipas | Kada | Pastaba dėl indekso |
| Irradiacija | ramūs ir taikūs naktys žemėje | sekti žemę, dingsta netrukus po aušros |
| Aukštas slėgis (apakimas) | anticiklonas | gali trukti visą dieną; naktį nuo švitinimo žemiau vamzdynas gali blokuoti tolimus signalus, todėl dienos gali eiti geriau |
| Priekis | priešais šiltas priekyje, už šalto priekyje, į šiaurę nuo stacionaraus priekyje | trumpas; blogas oras sutrikdo jį |
| Kalnų nusileidimas | föhn, bora: oras krenta, atšilsta ir džiūsta | išbandyti bora Adrijos ir föhn į pietus nuo Alpių |
| Įspėjimas | karštas ir sausas oras virš šalto ir drėgno dirvožemio, pvz., po lietaus | vedė į žemę |
| Slėnis | karštas oras virš šalto oro sustoja slėnyje | gali likti dieną |
| Marinas | karštas ir sausas oras šaltesnio vandens; atvyksta į pakrantę su vėjas | piko popietę; pavasarį ir vasaros pradžioje geriausi sezonai |

Garavimo kanalai jūroje (0- 30 m) tarnauja tik apie 3 GHz ir nepatenka į atokų: 144 ir 432 MHz galite ignoruoti. Aukšti kalnai yra kliūtis; dykumos yra per sausas.

3. Hepburn 'o atvejai nebuvo numatyti (S. Heck LA0BY, Extreme Tropo Propagation 144 MHz ir daugiau, "Nordic VUSHF 2019, nuo JO59IX iki 500 m aukščio):

- 2018 m. lapkričio 22 d.: 70 cm iki UA6 (apie 2600 km) 6-8 val. Hepburn nurodė tik Baltijos valstybes ir Lenkiją. Kondutas buvo matomas 900 ir 1500 m temperatūros žemėlapiuose ir baigėsi šiek tiek aukščiau nei 2000 m; jis buvo aukštesnis Norvegijos pusėje, ir tik LA0BY ant kalvos įrašytas. Ant žemės laikas neatrodo kaip tropas: šaltas ir rūkas viršuje.
- 2015 m. vasario 11 d.: 2 m ir 70 cm link 9A ir S5 (apie 1500 km) per Alpes, taip pat tai, ko Hepburn nenumatė.
- 2011 m. spalio 15 d.: 2 m su EA1 (2135 km) aukštais ortakiais, trimis jūros ruožais ir trimis žeme.
- Kiti renginiai: 2003 m. lapkričio 6- 11 d. į rytus (2050 km), 2004 m. gruodžio 10 d. į Prancūziją.
- Lauko adresai: rūkymas lygaus, laisvo smailių, mažai vėjo, drėgna žemė. Normalus temperatūros gradientas − 6,5 ° C / km; realiu atveju ( VK3KAQ) + 7 ° C 20 m.

Autoriaus išvada: Hepburn ir F5LEN nepakanka, jie tarnauja profilius aukštyje palei maršrutą. Mums: šios datos yra tikrinimo atvejai, kurie turi būti perskaičiuoti iš ankstesnių modelių duomenų, o kelio indeksas turi palyginti srauto kvotą su stočių kvota.

4. Italijos patirtis (G. IV3GBO, "Tipai VHF dauginimo SSB / CW", 2017): atliktas nuo 450 iki 3000 km, su sluoksniais nuo 450 iki 3000 m; rūkas pratęstas kaip clue; angos jūra Adrijos jūros link Puglia, Albanija ir Graikija. Satter nuo 100 iki 500 km.

5. Įvadas (P. Wilton M1CNK "Įvadas į VHF / UHF propagavimą", 2013): 70 cm yra dažnai geriau nei 2 m; su 10 m stiebu 144 MHz atstumu pradinis kampas yra apie 3 °, o 20 m maždaug 1,5 °.

6. NASA ataskaita (E. Hirschmann, "Troposferos ir jonosferos poveikis radijo dažniui (VHF- SHF) Komunikacija", Goddard X- 731- 67- 89, 1967): tai susiję su atstumo paklaidomis palydovams, o ne DX. Jis grąžina tą pačią N formulę ir eksponentinį profilį (pavyzdžiui Floridoje: Ns 370, ΔN − 55, pastovus 0.161 / km) ir siunčia du NBS šaltinius ieškoti: Bean ir Thayer, "CRPL Exponential Reference Atmosfera", Monograph 4 (1959), ir Bean, Horn ir Ozanich, "Climatic Charts and Data of Radio Refractive Index for the United States and the World", Monograph 22 (1960).

## 9. VHF / UHF DX knyga, 2 skyrius

I. White G3SEK (redagavo), * The VHF / UHF DX Book *, DIR Publishing 1995 (ISBN 0 9520468 0 6; skaitmeninė kopija 2017), 2 skyrius "VHF / UHF propagavimas", p. 2-13, 2-22-2-29 ir 4-11. Jis yra arčiausiai surinktųjų radijo mėgėjų patirties; jis patvirtina jau naudojamas ribas ir prideda naujas sinoptines taisykles.

Numeris

- Ta pati formulė N (77,6 P / T + 3,73 · 10 ^ 5 e / T2); pvz.: 1000 hPa, 7 ° C ir = 10 hPa žala N = 324. Geros formos atmosferoje, pavyzdžiui, po perkūnijos, N yra beveik tiesinis apie 39 vienetų / km per pirmuosius 2-3 km.
- Gradientai nuo 0 iki -79 N / km yra normalūs, žemiau -79 yra viršrefrakcijos, daugiau nei 0 subrefrakcijos. Knyga reikalauja, kad ilgas DX ateina ne iš paprasto superrefrakcijos, kuri turėtų likti tiesiai ant viso kelio, bet iš kanalų.
- Mažiausias latakų storis, šiek tiek atsargesnis nei Kerr ir Hepburn:

| Grupė | Mažiausias storis |
| 50 MHz |  400 m  |
| 70 MHz |  300 m  |
| 144 MHz |  200 m  |
| 432 MHz |  100 m  |
| 1,3 GHz |  50 m  |

- Šis santykis nėra proporcingas bangos ilgiui (maždaug 65 λ esant 50 MHz, 220 λ esant 1,3 GHz dažniui), nes vamzdyno kraštai "perkeliami". Jungtinėje Karalystėje vamzdynai dažnai tiesiami 144 MHz dažniu, tačiau beveik niekada nėra naudojami 70 MHz ir žemiau, o 432 MHz dažniausia yra stiprūs, kai 144 MHz yra silpni.
- Sklandytuve signalas nukrenta 1 / d, o ne 1 / d2: iki 1000 km atstumas gali būti panašus į laisvosios erdvės.
- Mažiausios stoties talpa 2 m (100 W, 15 dBi yagi, 2 dB triukšmo): 217 dB, t. y. vidutiniškai apie 400-500 km SSB; CW - apie 20 dB daugiau (p. 4-7). Sutinku su KK6MC lentele.
- Scatter: nuostoliai padidėja apie 15 dB trigubu dažniu ir 10 dB kiekvienam sklaidos kampui (Yeh formulė; ištrauktame tekste konstanta yra neįskaitoma, todėl ji turi būti paimta iš W3SZ versijos). "Iki" sklaida iš konvekcinių ląstelių apima reguliariai iki maždaug 500 km, 1000 km su geromis stotimis. Po stipraus lietaus ir prailgintų sąlygų yra mažai visose juostose.

Ortakio tipai

- Ant žemės: Žemėje jie skleidžia blogai, nes dirvožemis sugeria ir plinta; ramioje jūroje atvykti toli (Scilly- Kanarie 2500 km, maždaug kartą per metus 2 m). Garavimo kanalai dažnai yra tik apie 15 m aukščio ir tarnauti mikrobangų bangų.
- Aukštas: tarp 450 ir 2000 m, dėl ilgesnio DX ant žemės; signalas "aukštas" virš stočių žemiau. "Prarasti" ortakiai ne taip stipriai signalizuoja, bet taip pat leidžia tarpines distancines stotis.
- Kondutai turi tikslius nurodymus ir gali trukti valandas ar dienas, priešingai nei sporadic E.

Naujos sinoptinės rodyklės taisyklės

- Amžius anticiklono: oro lašai į kelių metrų per valandą centrą ir atvirkštinė yra sumažintas aukšto slėgio vystosi; didesnės angos atvyksta po kelių dienų dauginimo jau gera. Trumpalaikio anticiklono nepakanka.
- Aukšto slėgio kraštas: sluoksnis yra kupolas ir kraštuose yra žemiau, todėl stotys gauti geriau.
- Kryptis: Buvo pastebėta, kad ilgesni maršrutai vyksta išilgai izobares arba per dorsals (Flavell G3LTP, 1984), bet yra puikus jungtys taip pat radialine kryptimi nuo centro. 1987 m. rugpjūčio 29-30 d. pavyzdys: po priešciklono perkėlimo iš Anglijos į Norvegiją, per dvi dienas (Ispanija, Prancūzija, Italija, Austrija, Vokietija) anga buvo 180 °.
- Prieš: stora izobare (stiprus vėjas), stratocumuliform debesys prie sluoksnio krašto (apriboti aukščiausią dažnį).
- Priekinės pusės: šalta priekinė dalis stumia karštą orą aukštyn ir sukuria atbulinę pusę; geriausia kryptis yra palei priekinę pusę praeinant, bet ji trunka mažai.
- Sezonas: Jungtinėje Karalystėje anticiklonai dažniau būna vasaros pabaigoje ir rudens pradžioje.
- Vizualios clues: dūmai užfiksuoti tamsioje linijoje virš horizonto; aukšto slėgio layercuules su pagrindu žemiau 1000 m rodo žemės ortakio, bazė virš 1000 m aukšto ortakio.

Nuoroda į paiešką: R. G. Flavell G3LTP, "Extensive Anticyclony Propagation Event", * Radio Communication *, 1984 m. vasario mėn., p. 128; J. Gennaway G3YGF, "Tropospheric Scatter Propagation", * Radio Communication *, 1981 m. rugsėjo mėn., p. 710; P. W. Sollom G3BGL, "A Little Flutter W6ZGN

## 10. Patikrintas radijo mėgėjų modelis: Biancomano ir Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, "Practical Tropospheric Model for VHF / UHF Propagation Prediction", * National Weather Digest * 22: 2, June 1998, pp. 15- 24, free in [PDF from the National Wather Association](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf).

- Duomenys: 00 ir 12 UTC 47 stočių rytinėje JAV, 1995- 1997; žemės analizė 700 hPa (pradžioje iki 400), N profilis, be M. Išskyrus pakrantes, salas ir kalnus.
- Kiekvieno tyrimo atveju: ortakio buvimas ir kvota, maksimalus bangos ilgis, kuris gali sukelti (dažnai apie 100 kartų bangos ilgio), didžiausias galimos temperatūros gradientas tarp kaimyninių lygių ir tarp vieno lygio ir antrojo aukščiau (storesnių sluoksnių). Žemės ortakiai nenaudojami.
- Pirmoji taisyklė, patvarumas: stabilumo indeksai (panaikintas indeksas, K, iš viso, SWEAT) lieka tarp 3 vienetų iš tyrimo 12 valandų prieš, o slėgio lygis, kai potenciali temperatūra auga daugiau lieka maždaug tas pats. Jei taip: tobulinimo prognozė per kitas 12 valandų.
- Antra taisyklė, dinaminiai valdikliai: ribinis sluoksnis, kuris nedidėja, CAPE, storio kitimas iki 500 hPa per tris tyrimus, nuolydžio kitimas ribose.
- Juostos slenkstis: 100 cm ar daugiau keliantis tinklas rodo, kad jis atsidaro 432 MHz dažniu, beveik visiškai atitinka 222 ir dažnai net 144 vertes.
- Kosmoso filtras: stotys šešiakampyje tinklelyje apie 400-500 km; prognozė yra verta tik jei bent 5 contigue stotys yra teigiamas (jei galite praleisti vieną). 5 stotyse klaidingai atsitiktinio teigiamo poveikio tikimybė tampa nereikšminga.
- Patikrinimas pagal QST stulpelio "Pasaulis virš 50 MHz" įvykius: 1996 m. 67 (71%) numatyti 48 įvykiai; 12 iš 15 svarbių įvykių. Tikslumas - iki 70% 432 MHz ir apie 60% 144 MHz dažniais per 12 valandų; tai užtikrina beveik dvigubai realius įvykius; 9 klaidos iš 19 yra prognozės, gautos per vėlai, nes daug patobulinimų įvyksta per mažiau nei 12 valandų.
- Pridėta prie stočių toliau į šiaurę: vėjo besisukantis pagal laikrodžio rodyklę kopimas (karšto starto) kaip papildoma sąlyga.

Nuo visiškai PDF (pateikė programos autorius) trys daugiau informacijos. Jų taisyklė "storis = 100 kartų bangos ilgis" suteikia 600 m 6 m, 200 m 2 m, 125 m 1,25 m ir 70 m 70 cm (3 pav.): 70 cm yra mažiau stiprus nei Kerr ir Hepburn (apie 89 m), 2 m jis sutampa su VHF / UHF DX knyga. Vėjas, kuris sukasi pagal laikrodžio rodyklę, pasak autorių, ypač dėl to, kad jis atneša šiltesnį orą aukštyje, kas daro N kritimas auga su spaudimu; drėgmė, kuri atneša su juo neatrodo destabilizuoti. Priedėlis gauna dN / dh uždara forma pastovaus šiluminio gradiento atmosferą su ryškiu šilumos gradiento šuolis ir drėgmė tam tikrą kvotą: naudinga kaip bandymo atveju mūsų kodą. Jų pėdsakai potencialios temperatūros nuolydžio ateina iš K. H. Jehn, "Potencialaus lūžio rodiklio naudojimas sinoptinėje radijo meteorologijoje", * J. Meteorologija * 17, 1960, p. 264.

Ką imtis mūsų indeksas: (1) patvarumo terminas: palyginkite profilį su 12 valandų prieš, kad valandos prognozėje mes turime nemokamai; (2) erdvės filtras: taškas skaičiuojamas tik tada, jei pagerinimas apima keletą šimtų km plotą, kuris sumažina netikrą pavojaus signalą; (3) stabilumo rodikliai, apskaičiuoti nuo temperatūros ir rasos iki 850, 700 ir 500 hPa (Open- Meteo jau suteikia atšauktą indeksą ir CAPE); (4) vėjas, kuris sukasi aukštyje nuo 1000 iki 850 hPa; (5) jų tikrinimo metodas, kuris yra toks pat, kaip mes norime daryti su programos skelbimais.

Kitas šaltinis toje pačioje pakuotėje, mažai naudingas mums: P. K. Rastogi, "Stratosfera ir Troposfera (S- T) Studies at Millstone Hill", NASA 1984 (dokumentas 19840019069): 440 MHz radaras, matuojantis skaidraus oro turbulenciją nuo 4 iki 25 km, su plonais sluoksniais, susijusiais su gravitacijos bangomis ir vėjo kirtimu. Tai patvirtina, kad turbulencija, kuri gamina sklaida yra sluoksniuotas, bet jis susijęs su per didelis šansai ortakiai.

## 11. Trečiasis šaltinių turas (lapkričio 20 d.)

Aukštų ortakių istoriniai atvejai. J. Macoun OK1VR, 2 m nuo Sněžka viršūnių susitikimo (1602 m), 1958 m. rudenį, 1959 m. straipsnis išverstas 2006 m. ([ok2kkw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)). 1958 m. spalio 27- 28 d. pirmasis Čekoslovakijos Anglijos jungtis 145 MHz ( G5YV, 1220 km) ir daug daugiau nuo 1100 iki 1250 km, su stabiliais signalais po 23. Autorius jį priskiria dviem vienas po kito einančioms inversijoms, kurių viršutinė riba apie 2200 m, kai signalas grįžta į žemę tik tada, kai apatinis sluoksnis ištirpsta; būklė, kurią palaiko lėtas anticiklonas iš Atlanto į žemyną. Iš namų (Praha) jo maksimalus dydis buvo 530 km: įėjo tik vietos aukštyje. Tai dar vienas patikrinimo atvejis (pvz., LA0BY 2018 m.) ir patvirtinama, kad indeksas turėtų atrodyti didesnis nei 2000 m ir palyginti jį su stoties dalimi.

Istorinės bibliotekos NBS (naudinga ieškoti klasikinių darbų, o ne formules):

- W. Nupenas, * Bibliografija Troposferos propagavimo radijo bangų *, NBS Techninė pastaba 304, 1965, 356 puslapiai, su indeksu temų (taip pat "sąlyga prognozė") ir pagal šalį. Pranešama apie itališkus įrašus: L. Saudis, "Radijo ryšys atmosferos superrefrakcijos režime", * Aukštas dažnis * 24 (6), 1955 m. gruodžio mėn. (Italijos moksliniai tyrimai: superrefrakcijos mažiau nei 500 m dažnai dieną ir naktį); S. Silleni, * Annali di Geofisica * 8 (1), 1955 m.; J. M. Clara e e A. Antinori, prova tra Sardegna e Minorca da 238 a 297 MHz (390 km), * Elektrinė komunikacija * 33 (2), 1956 m. A. F. Clarke (Fort Huachuca, 1956 m.) kasdien paskelbė maksimalaus bangų, užfiksuotų per kitą rytą, nuo drėgmės, temperatūros, slėgio ir vėjo prognozių prognozę.
- A. L. Abbott, * Bibliografija Tropospheric Radio Wave Scattering *, NBS, medžiaga iki 1960 m. sausio mėn., apie 670 elementų užsakyta per metus: naudinga troposferos dalis.

Atskleidimas ir pastabos: F. W. Odenthal DK5EQ, "Ausbreitung 2m - Eine Einführung" (31 / 05 / 2024): beveik optinis iki 200 km, tropo per ortakį arba refrakcijos už, su dideliu slėgiu ir pavasarį ir rudenį; cituoja VHF puslapį DM2HB ir į DK5YA ant tropo ir Es pastabas. A. Johnson VK3DXE dienoraštyje (gruodis 2021): WSPR VHF takai tūkstančių km Australijoje buvo atrasta; tropas ne tik vasarą. Diskusija apie VHF mariną ir AIS ([Cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)]): apimtis 1,23 h (pėdų) jūrmylių vienai antenai, išsibarstymas iki maždaug 50 nm, tipinis tropų pagerinimas 100- 400 nm; konduitai yra pripažįstami, nes tolimos stotys atvyksta kaip stiprios kaip kaimynai. Chelmsford ARS News, December 1983: per 2 m varžybose Anglijos rytinės pakrantės stotys pirmenybę teikia jūros maršrutas į žemyną. EI7GL

ARRL, * The Radio Amateur 's VHF Manual *, 1972, cap. 2 "Propagation Underthe the Horizon" (p. 17- 19). Jau žinomi darbiniai patarimai (sekite aukšto slėgio zonas, kurios juda lėtai ir visų pirma jų nugaros kraštus; rudens pradžioje geriausias laikotarpis; pakrančių zonas, didelius ežerus ir upių slėnius, kuriems palanki stratifikacija). Du nauji duomenys: (1) fig. 2-4 lygina standartinę atmosferą (santykinė drėgmė nuo 70% iki 12000 pėdų, mažiausias lūžis) su tipiniu stipraus lūžio spinduliu, su skaidria temperatūros pertrauka ir mišiniu santykis apie 4000 pėdų (apie 1200 m): tai yra parašas, kad mūsų apskaičiavimai turi pripažinti GFS lygių; 2) tikri ortakiai yra reti iki 144 MHz ir dažniausiai UHF, pagal minimalų storis. Naudinga QST bibliografija ieškoti: Hull "Air- Mass Conditions and the Boding of UHF Waws" (1935 m. birželio mėn., 1937 m. gegužės mėn.); Hoisington, "Two-Meter Openings" (1949 m. spalio mėn.); Collier, "Upper- Air Conditions for 2-Meter DX" (1955 m. rugsėjo mėn.); Botts, "A Night to Memory" (1970 m. sausio mėn., vykdoma sausumoje).

PA0EHG, puslapis "Tropo" ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): troposcatter santrauka iš straipsnio G3YGF, jau vadove. Naudokite dvi praktines taisykles, kad kontroliuotume sklaidos skaičiavimą: apie 10 dB papildomų nuostolių kiekvienam kampui kiekvienos stoties horizonte ir apie 100 km maždaug 9 dB plius kiekvienas 100 km maršrutas (60 mylių); bendras tūris prasideda nuo maždaug 600 m 100 km ir nuo maždaug 9 km 480 km. Jis negydo kanalų, apklausų ar lūžio slenksčių.

PA0EHG, PROPSER skaičiuoklė ([puslapis](https://pa0ehg.com/propagat1.htm), propber.exe programa pateikta programos autoriaus). Tai 36 kB DOS programa, parašyta BASIC ir sudaryta su Microsoft BASIC kompiliatoriumi 5.60 (1985): ji veikia tik pagal DOS arba DOSBox. Iš vidinių tekstų: prašo dažnių, atstumo (arba du lokatoriai, taip pat senuoju QRA formatu "E- locator"), kiekvienai antenos aukščio stočiai, triukšmo skaičius, FI juosta, galia, kliūtis (aukštis ir atstumas) ir antenos padidėjimas (0 = lygiagretaus skersmens apskaičiavimas, efektyvumas 50%). Atstato maksimalų atstumą optikoje, teisingą optinį garo sugerties praradimą (0,1 dB / km nuo 24 GHz), troposkaterio praradimą "tinka vietoms" (t. y. horizonto kampams, kuriuos sukelia kliūtys) ir S / N santykį dviem pojūčiais, taip pat "didžiausią neįprastą sklidimą". Suvestiniame vykdomajame dokumente skaitinės konstantos neperskaitomos. Siūlomas metodas, taikomas didelėms juostoms, taip pat naudingas mums: matavimo rezultatų kalibravimas pagal išmatuotą sąsają (išmatuotas ir apskaičiuotas S / N skirtumas) ir to paties skirtumo taikymas kitai juostai. Tai yra tas pats modelis mūsų skaičiavimo, bet ji naudoja S RTM1 profilis vietoj tik vieną kliūtį ir GFS prognozė vietoj fiksuotos maržos.

PROPSER, konstantos skaityti vykdomajame dokumente (numeriai Microsoft Binary Format formatu). πr, duomenų lentelėje šalia tekstų: 8,68589 (= 20 / ln 10, t. y. 20 rąstų, apskaičiuotų gamtiniu logaritmu), 32.5 (laisva erdvė: 32,5 + 20 log f\ [MHz\] + 20 log d\ [km\]), 24000 ir 0,1 plius 40000 ir 0,3 (garai: 0,1 dB / km nuo 24 GHz, 0,3 dB / km nuo 40 GHz). Tikėtinas, nes kartojate, bet viduje kodas: − 69,5 (tas pats pastovus terminas kaip Yeh side-scatter formule VHF / UHF DX Book), 332 (dirvožemio refrakcijos), 8733 km (lygiavertis spindulys, k). Tiksli sklaidos formulė nėra rekonstruojama neišardant programos. T0

PROPSER, formulės rekonstruotos pagal kodą (išardymas surinktą dalį; operacijos yra vadinamas BASIC bibliotekoje su operatorių atmintyje, kurių vertės dvigubos tikslumo buvo iškoduotas). Anksčiau rasti konstantos − 69,5, 332 ir 8733 buvo klaidingi teigiami bibliotekos viduje.

- Optinis nuotolis: d = 5 · km (h, m). Jis yra dosnesnis nei klasikinis 4,12 (k = 4 / 3): yra lygus k rėm.
- Optikos praradimas: L = 32,5 + 20 log f\ [MHz\] + 20 log d\ [km\], plius 0,1 dB / km virš 24 GHz ir 0,3 dB / km virš 40 GHz.
- Troposcatter: Lts = L + 23 + 10 log f + 0,0674 · d, t.y. 55,5 + 30 log f + 20 log d + 0,0674 · d (formulė G3YGF, kai pastovi vertė 2 dB didesnė). Jokio kampo ar reaktyvumo.
- Pataisa "svetainių": programa prašo kliūtis ir atstumą, bet blokas, kuris turėtų juos naudoti yra tuščias; korekcijos kintamasis lieka 0, todėl "teisinga" nuostolis yra lygus sklaidos.
- Triukšmas: T = 290 × (10 ^ (NF / 10) − 1), N = − 228,6 + 10 log T + 10 log B (dBW), be antenos temperatūros.
- S / N = 10 rąstų P + G\ _ tx − L + G\ _ rx - N. "Su maksimaliu nenormaliu sklidimu" naudoja optinius nuostolius, o ne sklaidą, t.y. idealų konduit be nuostolių už laisvos vietos.

Palyginimas: 300 km ir 144 MHz PROPBER suteikia apie 189,9 dB, G3YGF 187,9, mūsų P.452 187,8 (su nuliniu horizonto kampais): sklaida sutampa su 2 dB. Mūsų apskaičiavimai prideda horizonto kampus nuo profilio, antenos temperatūrą, ir vietoj "maksimalus" nustatytas laukiamas sluoksnis su liekamojo poravimosi ir silpninimo.

Rohde & Schwarz, P. Denisowski, "Understanding VHF Propagation", Education Note, April 2023 (24 psl., distribution). Patvirtina jau surinktus taškus (atliekamas kaip dvimatės banglentės, signalas, kuris nukrenta linijiniu būdu su atstumu, iki 1500 km ir toliau, trukmė nuo valandos iki dienos; geras žemės ant jūros ir mažai ant žemės; pakilo nuo kelių šimtų metrų iki kelių km). Du naudingi stebėjimai indeksui: (1) raida laiko atžvilgiu: naujai suformuotas tinklas dažnai turi tik aukščiausius dažnius ir, sustorėjimas, pradeda pasiekti net žemiausią lygį; todėl valandinėje prognozėje turėtų būti parodyta 432 MHz "iš anksto" 144 MHz, kai sluoksnis auga; (2) stočių vieta: geriausios sąlygos yra tarp stočių netoli tinklo galuose, tuo tarpu tie, kurie eina aukšto tinklo keliu, dažnai neįeina, nebent būtų "kurie pralaimi". Vamzdžiai juda su atmosferos oro.

Svetainė PA0EHG, kiti puslapiai. Tropo, PathProfile of M. Willis G0MJW ([Μa](https://pa0ehg.com/srtmp.htm), [download](http://www.mike-willis.com/Software/ASTERPathprofile.zip)): profilis maršruto iš tikėtino bendro nuostolių ir kvotų failus, laikomas patikimu troposcatter mikrobangų; negydo sklaidos iš lėktuvų ar lietaus. Naudinga kaip antra nuoroda palyginti mūsų S RTM1 profiliai ir nuostoliai. Likusi svetainės dalis apima EME, mikrobangų krosnelę, oro juostą (AirScout, SM7LCB žemėlapiai) ir priemones 80 m atstumu.

PathProfile by G0MJW (Mike Willis), ASTER ir SRTM paketai ir SiteData (pateikė programos autorius; [](https://www.mike-willis.com/software.html)programinės įrangos puslapis). Windows programa 2018: profilis iš ASTER arba SRTM į 1 "arba 3", nuostolių optikos, difrakcijos, troposcatter (galioja iki 1000 km), ortakio ir dujų, su laiko, k mediana maršruto, dalis kelio jūroje ir atstumas nuo kranto: struktūra yra P.452 / P.1812, bet pagalba (suspaustas.chm failas) negali būti čia. SiteData konvertuoja lokatorius ir suteikia dalį iš GT OPO30.

Naudingiausia dalis yra duomenų rinkmenos: ITU- R P.453 statistiniai žemėlapiai pasauliniame tinklelyje 1,5 ° (121 eilutė nuo 90 ° šiaurės platumos iki 90 ° pietų platumos, 241 stulpeliai nuo 0 ° rytų ilgumos): gradientas dN / dz per pirmuosius 65 m viršyta 1, 10, 50, 90 ir 99% laiko; dirvožemio latakuose procentinė buvimo, storio ir intensyvumo dalis; tiems, kurie yra dideli, bazinė dalis, storis, intensyvumas ir didžiausias aukštis; plius drėgnas terminas ir temperatūra. Tarpinės metinės vertės:

| Vieta | Dujotiekis žemėje:% / storis / intensyvumas | Aukštas vamzdynas:% / pagrindas / storis / intensyvumas | dN / dz viršija 10% laiko |
| Neapolis |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | 380 N / km |
| Tirėnų (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | - 674 N / km |
| Trapanis |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | 520 N / km |
| BrindisiName |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | - 405 N / km |
| Adrijos (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | - 399 N / km |
| Milanas |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | 172 N / km |
| Udinas |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | 176 N / km |

Mūsų indeksas yra paruoštas klimato nuoroda: storis ir intensyvumas tipiškas ortakiai vietoj 50 m nustatytas potencialus ortakio, ir anomalija "šiandien, palyginti su įprasta" kiekvienai zonai. ITU duomenys: nemokamai naudoti ne komerciniais tikslais, patikrinti prieš įtraukiant juos į viešą versiją. ZO NES01.DEM rinkmenoje (58 MB, 2 'tinklelis) yra klimato zonos vidaus / pakrantės / jūros; formatas nėra dokumentuojamas, o aš jo neiššifravau. Willis' lygtis puslapyje yra tik fizinės konstantos, laisvos vietos ir antenos padidėjimas.

Ketvirtasis šaltinių turas (lapkričio 20 d.).

Indermuehle ir Suzuki 2026, prognozuoti GFS vamzdynų į CSIRO observatorijos Murchison (arXiv 2608.12861): tai yra arčiausiai šaltinis LIVETROPOVHF. Profiliai, kurių N nuo GFS iki 0,25 ° (paviršiaus iki 2 m, tada izobarų lygiai, po išmesta žeme); kiekvienos mažiausios dM / dz kolonėlės žemiau 3 km, bazė ir antgalis, integruotas M trūkumas; tada P.452-16 ir P.1812- 6 su GLO- 90 25 maršrutais nuo 98 iki 1005 km, nuo 88 MHz iki 2,68 GHz, kiekvieną valandą. Rezultatai: AUC mediana 0,72 25 kanaluose; P.452 p laiko procentinė dalis turėtų būti naudojama kaip kalibravimo konstanta (pasirinkimas p = 20%), nes ITU modelis su akimirkine ΔN nėra apibrėžtas jungtyje; įgūdžiai išlieka beveik lygūs 3 dienoms iš anksto. Kai naudojama 162 MHz (AIS), priimtuose maršrutuose yra didžiausia jungtinė frakcija - 67% (0,26, palyginti su 0,16), o didžiausias ortakio gylis - 49% (28, palyginti su 19 M), atsižvelgiant į kontrolės priemones: jūroje kondutas beveik visada nulemia sausumos ruožą. GFS matyti pusę laiko vietoje, palyginti su ERA5 (4,5%, palyginti su 10,4% ląstelių-valandų), o jūroje - daugiau (73%, palyginti su 56%). "Koridoriaus" indeksas (ištisiniai vamzdynai maršrute, mažiausiai 40 m storio sluoksniai) pagerina rezultatą: patvirtina, kad maršruto tęstinumas yra teisingas kriterijus.

Longley and Rice, ESSA ERL 79- ITS 67 (1968): metodas netaisyklingam reljefui nuo 20 MHz iki 20 GHz (po ITM) su dirvožemio pažeidimais Δh, Ns nuo 250 iki 400, lygiavertis spindulys 8497 km (4 / 3), horizontai, difrakcijos, sklaida į priekį ir kintamumas laiko ir vietose klimato. Statistinė nuoroda, ne akimirkinis.

ITU- R P.834-2: spindulio kreivė, Žemės spindulio ekvivalentas k · a, M = N + h / a (h m, tūkstančiais km), eksponentinis modelis; nieko naujo, nei tai, kas jau buvo naudojama.

AGARD CP- 70 (1970), tropošerinis dauginimas: ploni 1-100 m sluoksniai su stipriais nuolydžiais yra bendri; aukšto sluoksnio atspindžio koeficientas yra maždaug proporcingas bangų ilgiui, o atsitiktinė dispersija yra apie 14 dB. 90 MHz priemonės: 9-10 dB sumažėjimas, susietas su N šuoliu, koncentruotu maždaug 350 m atstumu debesų sluoksnio viršuje. Šalto fronto praėjimas pašalina sluoksnius: Išlieka difrakcijos.

JAV URSI ataskaita, Komisija 2 (1960): vidutinis storis 60-210 m aukštų sluoksnių (atsižvelgiant į P.453), mažiausias gradientas 157- 160 N / km ortakio.

Buscemi K2OVS, VHF sklidimo vadovas: tipiniai atstumai atbulinės eigos ir ortakio juostoms, ortakiams, sujungtiems su aukštu stacionariuoju slėgiu, maksimalūs auštant.

Wolinsky- Mancini ir 2024 m., AIS Kanarų salose: AIS radijo užtvindymo ir priėmimo; ortakiai yra daug dažniau šiltais mėnesiais (apverčiant aliuzijų).

Nigerijos ir Malaizijos straipsniai (Ekah, Iloke, Mat, Igwe): vietos koreliacija tarp temperatūros, drėgmės ir išmatuotų nuostolių, silpna ir prieštaringa (r -0,09-0,27); naudinga tik kaip patvirtinimas, kad žemės dydžiai vien nepakanka.

Herben 1984, disertacija TU Eindhovenas (microonde): 30 GHz 8,2 km ir OTS palydovas 11,6 GHz; kibirkštis turbulence, multicourse ir lietaus. Mikrobangų bangų dinaminių nuostolių dominuoja lietus ir turbulencija, o ne kanalai.

Baarai, Paraboloidiniai Atspindžiai Antena (microonde): parabinio G = η · (πD / λ) 2 su būdingu veiksmingumu 0.5- 0,65; paviršiaus paklaidų praradimas pagal Ruze, ηs = exp (− (4πε / λ) 2). Tai padėtų, jei mes įtraukti parabolės kaip alternatyva Yagi DL6WU už 1296 MHz ir už jos ribų.

Jau analizuotas arba prastas turinys: Hirschmann NASA X- 731- 67- 89 (dvigubas), Reid 's VHF ST radaras (vėjai ir turbulencija, unvered), Scribd skaidrių ir generinių pastabų dėl troposferos.

Penktasis šaltinių turas (lapkričio 20 d., vakare).

Rogers 1997, Radio Science 32 (1): Bayestage evaluation of the parametrs of a high ortage (base rate, M design, storis) from distribution factors measured through the horizont, with a paraboline lygtis modely; VOCAR experiment in California at 143, 263 and 375 MHz, comparison with radio banga. Signalas daug kas priklauso nuo pagrindo ir M deficito (deficito korekcija 0,81-143 MHz), beveik iš viso nuo storio (− 0,17): storis negali būti gaunamas iš signalo. Mums: B apskaičiavimas (klimato storis) yra mažesnis nei deficitas ir dalis; ir metodas yra pagrindas back Office, tai yra gauti konduit iš daugelio dažnių.

Moler and Holden 1960, "Troposferos sklaidos ir atmosferos apytakos", J. Res. Kalifornijos jūrų maršrutai į Point Loma (San Diego): Santa Barbara 190 mylių, Point Mugu 144, San Clemente 78; 1365 MHz ir 9380 MHz, ne VHF; January-Balandis 1958, vidutiniškai 1 minutę. Kovo 26 d.: apie 20 dB sustiprinimas apie 3 valandas iki vidurdienio, kai cikloninis sūkurys silpsta ir prasideda jūros vėjas ir nuošalumas; 15-oje reformuojama konvergencija, o signalas sumažėja maždaug 22 dB. Statistika (1 lentelė, 500 hPa, 33 atvejai): esant vidutiniam signalui krintančiam orui, L ir X diapazonuose jis viršija 2,5 dB, palyginti su kylančiu oru. Signalas atitinka skirtumą, apskaičiuotą 500 hPa variacijos žymens ir amplitudės atžvilgiu, be koreliacijos koeficiento. Žemi sluoksniai (apie 600 m) skatina dalinius svarstymus apie išsibarstymą. Antikiklonai ir frontai netraktuojami, o pirmiau minėtos "ekskursijos iki 60 dB" nėra: jie buvo gauti iš antrinės santraukos. Mums: vertikalus judesys yra geras pranašas, ir + 1,5 dB subsidence ir - 2 dB aukštynkrypčiam orui yra tokios pat būklės kaip vidutinis išmatuotas skirtumas (2,5 dB); 20 dBs yra vienas atvejis, o ne tipinė vertė, ir mikrobangų krosnelėje.

Banafaa ir Muqaibel 2025, IEEE Access: tyrimas vamzdynai (garinimo modeliai, parabolinė lygtis, radarų įverčiai, GNSS ir lidaras) ir klasifikacija su automatiniu mokymusi apie realias priemones; Parama vektorių mašinos duoda geriausius rezultatus. Naudinga kaip bibliografija; Duomenys iš Persijos įlankos.

ITU- R P.834- 7 (2015): atnaujinta rekomendacijos versija dėl troposlaukinės refrakcijos; nėra naujos formulės, nei jau buvo naudojama.

Abbott, NBS bibliografija troposterius sklaida (GOVLU- C13): jau trečiame etape. Slide Scribd ant kanalų: generinis turinys. Straipsnis ScienceDirect 0021916987900304 Nepavyko atverti.

Šeštasis raundas (lapkričio 20 d., naktis): Bean and Dutton, ARRL Handbook 2022 and 2023, Balanis.

Bean and Dutton, Radio Meteorologija (cap. 6), dalys dar nenaudotos. Jautrumas lauko sąlygomis N: 0,18 dB N vienetui per metinį ciklą; 0,14- 0,24 dB / N dienos ciklui (daugiausiai anksti ryte), iki 0,2- 1,1 dB / N maršrutuose 175- 200 km žiemą; pataisos 0,8-0,95. Parametras, sudarytas iš Misme: M = a (g\ _ e − 40 + b · ΔW ^ n) su a = 0,5 dB / N / km, kuris sujungia ekvivalentinį gradientą ir šiluminį stabilumą ΔW (darbas pakelti orą 1 km): Sacharoje, 160 km atstumu, didžiausias stabilumas yra 20 dB nuo sausio iki birželio, beveik lygus gradientui. potencialus lūžio rodiklis K (N, apie kurį pranešta taikant adiabatiją iki 1000 hPa): išryškėja oro masė ir anticikloninė subsidencija (Flavell ir Lane).

ARRL vadovas 2022 (99a ed.), Išlyga IIIA-PT-12 Naudingi inversijos tarp 250 ir 2000 m; inversijos radiacijos (serumas, ramus oras, sausas dirvožemis) retai veda tiesa; lėtas anticiklonai suteikti plačiausius kanalus per subsidence, su inversijų tarp 500 ir 3000 m, stipresnis vakare ir ryte; šiaurės pusrutulyje geriausi keliai yra į pietus nuo aukšto centro. Karštas priekyje: geros sąlygos keletą šimtų km į priekį; Šaltas priekyje: stiprus, bet nestabilus atgal už priekinio, lygiagrečiai. Foehnas (taip pat Alpėse) ir žemės vėjas (vėsus oras ant žemės, karštas 200-300 m; tęsiasi daugiau nei 800 km palei pakrantę). Scirocco: karštas ir sausas oras iš Sacharos virš aušintuvas jūrų oro, labai stiprus inversija iš rytinės Viduržemio jūros dalies į Gibraltarą, tikėtina priežastis įrašų per 1500 km mikrobangų krosnelėje. Troposcatter: teorinė riba apie 800 km, didžiausia tipinė pusė. (2023) turi trumpesnį tekstą: inversijos saulėtekio ir saulėlydžio iki + 20 dB ir + 20- 50% srautas; 1200 mylių Didžiuosiuose ežeruose, tipinis 400- 600; rekomenduoja Radio Mobile (ITM modelis) Hepburn aprėptį ir žemėlapius.

Balanis, Antena teorija (4a ed.), § 10.3.3: Viezbicke lentelė (NBS TN 688, 1976) Yagi optimizuota 400 MHz: bum 0.4 / 0.8 / 1.2 / 2.2 / 3.2 / 4.2 λ → 7.1 / 9,2 / 10.2 / 12.25 / 13,4 / 14.2 dBd. Mūsų DL6WU formulė (8,6 + 8 log10 bum / λ) suteikia 1-1,7 dB mažiau 0,8-4,2 λ ir grįžta į eilutę su DL6WU lentelėse 9,5 λ: protingas įvertinimas. Su teisinga formule ant originalo (9,4 + 7,8 log10 bum / λ, nuo lapkričio 22 d.), į jūrą išmetamų teršalų kiekis yra mažesnis kaip 0,2 dB nuo 1,2 iki 4,2 λ (12,1 dBd iki 2,2 λ prieš 12,25) ir išlieka 0,6 dB iki 0,8 λ.

Septintasis raundas (lapkričio 21): QST, QEX, W3SZ, GitHub saugykla.

QST 2000-2025 (14 numerių, 10 lovų su teksto atpažinimu). Tik ataskaitos apie Pasaulį virš 50 MHz, naudinga kaip atvejai patvirtinti modelį: rugsėjo 5- 9, 2000, atliktas 144 MHz ir toliau rytinėje JAV pusėje; 14- 15 rugpjūčio 2022, atidarymo Manitoba- Kansas / Missouri esant aukštam spaudimui karštą dieną, nuo 144 iki 1296 MHz beveik viskas FT8, Kanados įrašas 10 GHz 1228.56 km ( VE4MA - N0OY) Q65, signalai iki 1941 km; APRS sklidimo žemėlapis parodė tą patį plotą. QST lapkričio 2022 rekomenduoja Hepburn žemėlapiai. Nėra techninių straipsnių tropo prognozė.

QEX ir K1JT, Franke ir Taylor (QEX 2020) FT4/FT8 režimuose: FT8 dekodavimo slenksčiai - 20,8 dB 2500 Hz kanale be evanescencijos, - 18,6 dB su evanescencija vidutiniuose latituduose; FT4 - 17,5 dB. Kiti QEX 2023 ir 2026 numeriai negydo tropo (trys skaičiai 2023 yra neskaitytos nuotraukos). W3SZ 2019: lėto režimo ribinės vertės ( JT4A -23, JT9A -27, JT65A -25, QRA64A -26, FT8 -21 dB) ir pasirinkimo kriterijai tarp jautrumo ir dažnio poslinkio tolerancijos.

GitHub. TropoLink (LukiBox, Apace- 2.0): troposcatter maršrutų projektavimas su P.617-5 (su N0 ir ΔN), NBS TN101 ir ITM flanked, o jų skirtumas rodomas kaip neapibrėžtis; P.617-5: L = F + 22 log f + 35 log θ + 17 log d + Lc, F = 0.18 × N0 · e ^ (− hs / 7,35) − 0.23 · ΔN, Lc = 0.07 · e ^ (0,055 (Gt + Gr)). Andi- f / troposcatter ( DL2FZN 2013, iš VHF / UHF Handbook RSGB): Lts = 24,44 + 4,283 ln f + 0,06656 d virš laisvos vietos, šlaitas netoli PROPSER (0,0674 dB / km). QRadioPredict: ITM ir ITWOM 3.0 su S RTM3, dirvožemiui naudoti skirtos juostos ir NEC diagramos. Netaikoma: Tropos (meteorologinė app), opera- adt / tropo ir troposim (Troposferos vėlavimas InSAR), tgbot\ _ troposcatter (karinė stotis), sdr- troposcatter- gui (valdymo pultas), Troposcatter- Propagation- Delay ( ERA5 vėlavimas), Radio- Communication - Project (didaktinis simuliatorius), PyLaels (kanalas).

Aštuntasis ratas (lapkričio 21 d.): NTIA ir ITU modeliai GitHub. NTIA / itm (Longley-Rice, ITS): 20 MHz- 20 GHz, laisvos erdvės praradimas, difrakcijos ir troposcatter su dirvožemio profiliu, 0.5- 3000 m terminalai, paviršiaus reaktyvumas N0 (minimumas sumažintas iki jūros lygio, 250- 400), septyni klimatai, dirvožemio konstantos ir įvairūs režimai 1 vaizdas, 2 difrakcijos, 3 troposcatter; C + su.NET sistema 4.7.2 aptvaras. Jis nemodeliuoja ortakių: jis naudojamas kaip etaloninis "normalus oras" su statistiniu kintamumu. NTIA / p528 ir eveetza / p528 (MATLAB): ITU- R P.528-5 oro ir dirvožemio jungtims, 100 MHz- 30 GHz, 1.5-20 000 m terminalai, p. 1-99%;.NET 4.8 korpusas. Galimas naudojimas: oro sklaidos balansas (du antžeminiai maršrutai- orlaivio ir radaro sekcija). eveetza / p2001 (MATLAB): ITU- R P.2001- 6, allolauko modelis su difrakcija, troposcter, nenormalus sklidimas (elgesys ir sluoksniai, Lba) ir E sporadic, laiko procentas 0- 100%; ITU skaitmeniniai žemėlapiai (DN\ _ Medium, DN\ _ SupSlope, DN\ _ SubSlope, dndz\ _ 01, TropoClim, h0, surfwv, Es) nėra perskirstomi. Galimas naudojimas kaip ITU palyginimas vietoj P.452, GFS ΔN vietoj žemėlapių, kaip nurodyta CSIRO tyrime; žemėlapius naudotojas turėtų parsisiųsti kaip P.453. Patvirtinimas 1.3.0 iš 593 QSO (29 / 12 / 2021-2 / 1 / 2022): įvykių / palyginimų atskyrimas 0,79 be pataisymų, 0,91 su Misme ir subsidence; GFS nemato kontūro jūroje 144 MHz (guadagno be pataisymų niekada ne daugiau kaip 15 dB).

Devintasis turas (lapkričio 21 d.): IU3QEZ, NTIA, PyWaveProp, ACE High, validation 1.3.1. IU3QEZ (S. Fabris), prezentacija "Per horizontą VHF" (GEKO Magazine n. 69, rugsėjo 2026): bendras tūris d2 / (8 · R\ _ eff) su R\ _ Eff 8500 km (nuo 1,3 km iki 300 km, nuo 3,7 km iki 500 km kiekvienam horizonto lygiui 69 mylių (111 km), 7 dB marža evanescencijai ir 4 dB antžeminiam atspindžiui; Tesė: 10 dB yra apie 100 km; stotis IQ3QC 5 su 3 × 5 elementais, 120 QSO, maksimalus 788 km (Alpe Adria 2026). Atkuriamo pateikimo bibliografija: Norton 1959 (J. Res. PyWaveProp (Python): Parabolinės lygties metodai su Padé aproksimacijos, kelių kraštų difrakcijos, PETOL ir RAM aptvarai, troposferos ir aukšti kanalai, nereguliarus dirvožemis: galima bazė lauko skaičiavimo su GFS M (z profilis, turi būti patikrintas. APRS PropView (Apache 2.0): VHF sklidimo žemėlapiai iš APRS paketų, valandinė statistika ir pavojaus signalas; galimas 144 MHz duomenų šaltinis patvirtinimui. NATO troposcatter tinklas Italijoje (fortifations.net): 900 MHz, 10 kW, 20 m parabatai; stotys, įskaitant Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Vulture, Monte Lauro; istorinis testas P.617. Netaikoma: Gupta 2021 (stratosferos transportas), įstatymas 2023 (Arkties ozonas), delfinas # 590 ir opera-adt / tropo (InSAR vėlavimas), tropo.ino (Arduino dujų jutikliai), tropo- webapi (telefonija), drumstikliai / troposfera (AWS). Patvirtinimas 1.3.1 564 QSO tropo: AUC 0,79 (1.2 modelis), 0,80 (+ minkštas ortakio), 0,90 (+ misme ir subsidence), 0,90 (+ restance); konduit spalvos (≥ 16 dB) 46% atidarymo maršrutų ir 0% palyginimo. DL2ALF

Pridėta prie to paties turo. Mikelytaev / bangų-sklidimo (MIT, Python 3): Parabolinė lygtis su apytiksliais Padé ir Numerov, nereguliarus dirvožemis, troposferiniai kanalai; įkrova modifikuotą refrakcijos M (kvotos, Lat, lon) iš NOAA GFS ir nustatyti garinimo konduit Monin- Obukhov; nėra dokumentais pagrįstą funkciją, kuri grąžina kelio nuostolius. Jis yra geriausias kandidatas ateities srityje skaičiavimo su GFS M profilis (tai tarnautų išorės tarnyba arba vertimas). RP- Sim (AGPL- 3.0, JavaScript): Empiriniai radijo tinklų modeliai (Hata, Jis, Ericsson, du spinduliai), nėra tropo. RIMFading (GPL- 3.0, OMNeT + +): srauto sutrikimai tinklo modeliavimui, Weibull paskirstymas, nėra tropo. PyLays: vidaus radijo kanalas, jau neįtraukti. qradiopredict (kantooon, GPL v2): ITM ir ITWOM v3.0 su S RTM3 ir Corine, jau septintajame raunde QRadioPredict.

Dešimtasis raundas (lapkričio 21): Antenos knyga, Nortonas, Barsis, nauji duomenų šaltiniai. ARRL Antena knyga (23a ed.), dangtelis 4 ( K9LA): Lenkimo metodas (QST 1961 m. lapkričio mėn.) VHF diapazone: sklandus žemės praradimas iki 99% (300 mylių, 144 MHz: 214 dB), kreivės iki 50% ir 99% (4.11 pav.): 195 dB vertė nuo 100 mylių iki 99% ir daugiau kaip 250 mylių; pakilimas ± 7 dB virš 100 mylių (nuo 3,5 dB iki 50 mylių), SSB 3 dB blogesnis už CW, + 4 dB antžeminio atspindžio, + 3 dB dviejų antenų sujungimo atveju; aukščio padidėjimas (15 m: 4 / 3 / 2 dB esant 10 / 50 / 100 mylių; 24 m: 8 / 6 / 4 dB); Jagi įgauna apie 10 kartų bumą λ (dB); 50 W, esant vidutinei antenai apie 180 mylių, + 10 dB iki 250 mylių. Saulėtekis ir saulėlydis iki + 20 dB ir + 20- 50% srauto; karštas ir sausas oras virš drėgno gryno oro Didžiuosiuose Ežeruose iki 1200 mylių, tipiškas 400- 600; regionai įtakos didelėms vandens masėms labiausiai patiko; vėlyva žiema su šildymo gali stiprių inversijų. Patikrinimas mūsų troposcatter: P.452 iki 50% 483 km ir 144 MHz suteikia apie 204 dB, atitinka 214 dB Bray 99%. Norton 1959 (J. Res NBS 63D, nemokamai NIST): pagrindinis nuostolis, sistema ir kelio antenos padidėjimas; Atskaitos atmosfera N = 301; lygiavertis spindulys nuo N (250 → 4878 mylios, 301 → 5280, 400 → 6997); L (99) lentelės su 30 pėdų antenomis (100 MHz: 163 dB iki 100 mylių, 208 dB iki 500 mylių; 1000 MHz: 138 ir 188 dB su 28 pėdų parabolėmis); ilgalaikis kintamumas V (p, θ) priklausomai nuo kampo atstumo (13 pav.). Ryžiai, Longley ir Norton, NBS TN 15 (1959), NIST neapmuitinami. Barsis and Kirby, NBS Report 6751 (1961): 223 km Colorado su Pikes Maxy (4292 m) kaip kraštas, 100 ir 751 MHz; pastovus nuostolis su užapvalintu kraštu (maždaug 11,5 km spindulio); ilgalaikis eganescence 11,7-14,3 dB palyginti su 39- 42 dB to paties ilgio sklaida; nėra dienos ciklas pažymėtas; keturių spindulių modelis suteikia 4-5 lobes, bet tik vienas yra matuojamas (ne daugiau kaip 21 m nuo žemės): takai su kalnų kliūtimi yra daug stabilesnis nei skirtumu, naudinga jungtys "per Vesuvius arba Faito". ARRL VHF skaitmeninis vadovas: paketas, APRS, D-STAR, WSJT (Scatter Method and EME), nėra ant tropo. Nauji patikrinti arba ieškomi duomenų šaltiniai: ECMWF atviri duomenys (IFS 0,25 °, 1000, 925, 850, 700 hPa..., be 975, 950, 900 ir 800; G RIB2 su CCSDS suspaudimo, kad mūsų skaitytuvas neturi iššifruoti; jau prieinama per Open- Meteo); DWD ICONES atviri duomenys (G RIB2 suspaustas bz2, kintamo ir lygio rinkmena: daug prašymų); Kanada GDPS MSC Dataamart ir Geomet (apie 15 km, iki 33 lygių; GeoMet leidžia pasėlių plotas; tai Hepburn žemėlapiai pagrindas); GFS NOAA archyvas AWS nuo 2021 m. vasario (pasaulinės rinkmenos, naudinga patvirtinti); APRS patvirtinimas tik dėl OpRS.

Vienuoliktas raundas (lapkričio 21): Longley- Rice, GEKO 69. Bean and Thayer ", Atmosferos radijo reaktyvinio indekso modeliai", Proc. IRE May 1959 (pp. 740 et seq.): 4 / 3 modelio nuolydis yra tinkamas pirmaisiais kilometrais, bet per didelis; 30% spindulio kreivio įvyksta pirmaisiais 100 m ir 60% pirmaisiais kilometrais; vidutinis santykis tarp N ir sumažėjimas per pirmąjį km: − ΔN = 7,32 · e ^ (0,005577 · Ns) (N = 313 → ΔN maždaug -42). Bean and Thayer, "C.R.P.L. Exponential Reference Atmosfera", J. Res. NBS 63D n. 3 (1959): N (h) = N (h) · e ^ (− c · h) su c nustatytas Ns per tą pačią ataskaitą; vidurkis Ns USA 313; spindulio sekimo lentelės (bendra kreivė 15,2 mrad su eksponentiniu modeliu ir 30,3 mrad su Žeme 4 / 3). MATLAB refraktionexp: c = 0,143859 km − 1 Ns = 313 (skalė apie 6,95 km), galioja virš 1 km ir 10 mrad. Naudojimas mums: "normalus" palyginimo profilis gali būti, kad CRPL su vietos Ns vietoj M = 315 + 0.118 · z fiksuotas, ir sumažėjimo per pirmąjį km anomalija turėtų būti matuojamas palyginti su -7,32 · e ^ (0,005577 · Ns) vietoj -40 nustatytas. NTIA Report 82-100 (Hufford, Longley, Kishey 1982), ITM gidas rajone režimas: climi ir Ns pasiūlė (Pusiaujo 360, subtropinis kontinentinis 320, subtropinis jūrų 370, dykuma 280, vidutinio kontinentinis 301, vidutinio jūros sausumos 320, vidutinio jūros jūroje 350); Ns = N0 · e ^ (- hs / 9,46 km); jūs naudojate mėnesinį minimumą Ns, nes mažiau įtakos superrefrakcija ir subrefrakcija; Ns = 301 atitinka ae = 8497 km; pagal 100 km Ns skaičius mažai. Ryžiai, Longley ir Norton, NBS TN 15 (1959), I dalis: prognozavimo formulė paskirstymo laikui bėgant sausumos ir troposferos bangų, bazė TN 101. SoftWright, Wikipedia, Altair WinProp ir MATLAB rfprop. LongleyRice: ta pati informacija (versija 1.2.2, 20 MHz-20 GHz arba 40 GHz, 1-2000 km, Ns 301, septyni klimatai, laiko, vietos ir situacijos kintamumas); niekas neformuoja ortakiai, tik statistinis poveikis jūrų klimatas ("labai aukštesni laukai 10% laiko ar ne"). GEKO Magazine n. 69 ( IU3QEZ): radijo horizontas 4 / 3 130 km nuo 1000 m, 260 km pridedant dvi stotis; troposcatter su bendru tūriu nuo 1-3 km iki 10 km; išsibarstymas iš orlaivio iki 10-20 dB dešimtims sekundžių arba kelioms minutėms; teoriniai kursai su 2 × 10 elementų per pusę: SSB QRP apie 440 km, SSB 50 W apie 510 km, CW apie 600 km, SSB 500 W apie 640 km (500 W SSB sudaro 40 km daugiau nei 5 W CW CW); 90-99% laiko riba 10-20 dB riba turėtų būti pašalinta; 5- 10 dB už horizontą ilgame ruože; kvotos dydis apie 440 km, apie 440 km, apie 50 W iki bendro ilgio, apie 600 km, CW - apie 600 km, SSB apie 640 km, apie 640 km (500 W SSB) apie 40 km daugiau nei 5 W SSB daugiau nei 5 km, o OB iki 10-20 dB, nepaisant 10-10-20 dB laiko ilgio ilgu atstumu ilgu ilgio ilgu ilgio ilgu ilgio ilgu ilgio ilgu ilgio ilgu ilgio ilgu atstumu

## 12. Apskritas stalas: modelio patvirtinimo ar pataisymo šaltiniai (lapkričio 21)

Santrauka visų šaltinių posūkiai, užsakyta naudoti. "Valstybė" sako, jei šaltinis jau yra programoje (1.3.1), ar tai yra bandymas, ar tai turi būti padaryta.

### Patvirtintini duomenys (kas iš tiesų įvyko)

| Šaltinis | Kas suteikia | Naudojimas | Būsena |
| MMMonVHF ataskaitos iš autoriaus | 593 QSO 144 MHz, 29 / 12 / 2021-2 / 1 / 2022, laikas ir lokatoriai | Įvykio patvirtinimas lyginant (+ 14 dienų) | naudojamas: AUC 0,79 (1,2) → 0,90 (1,3.1) |
| Pasaulis virš 50 MHz | dokumentais pagrįsti atvejai (14- 15 / 8 / 2022 Manitoba- Kansas, 144- 10 GHz) | istorinis atvejis | naudojamas kaip istorinis atvejis; turi būti pridėtas prie tinkamumo patvirtinimo tinkamumo patvirtinimo |
| wspr.live (WSPR) | pilnas archyvas, 2 m ir 70 cm, SQL | daug įvykių ir daug "ne" (valandos be dėmės) | bandoma, ji turėtų būti filtruojama (vietoje klaidinga 16- 20 tūkstančių km) |
| APRS PropView / APRS- IS | stotys jaučiamas, atstumai, valandos | Tik patvirtinimas 144 MHz | atlikti |
| Atvirkštinis švyturys (archive) | kasdien | VHF | dabar išmesti |
| I GRA2 radiozondaggi (Brindisi 16320, Mare 16245, Trapani 16429, Udine, Milan praktika) | Profiliai, išmatuoti 00 ir 12 UTC | GFS profilio valdymas ir inversijos | surasta naudotinų failų (Brindisi 46 MB) |
| Barsis and Kirby 1961, Barsis 1963 | kelio nuolydis, išmatuotas kalnuotomis kliūtimis | patvirtinti kategoriją "monte" (Vesuvio, Faito) | atlikti |
| Norton 1959, NBS TN 15, TN 101 | L (99) ir stalo žiniasklaidos nuostoliai | Spektro kontrolė | iš dalies (nuosekliai žr. 300 mylių) |

### Šaltinių siūlomos pataisos

| Korekcija | Šaltiniai | Atliekant patvirtinimą išmatuotas poveikis | Būsena |
| Misme Stability Terminas | Bean and Dutton kepurė. 6 | AUC 0,79 → 0,90 kartu su padugne | 1.3.1 (16 dB antraštinė dalis be kanalo) |
| Vertikalus poslinkis | Molerio ir Holdeno 1960 m. (skaityti originaliu 22 / 11: + 2,5 dB vidurkis tarp oro nusileidimo ir oro kilimo, 1,4-9,4 GHz iki jūros) | kaip nurodyta anksčiau | 1,3.1 (+ 1,5 / - 2 dB) |
| Minkštas vamzdynas (λ iki 3 · λmax) | Kerr, Hepburn, VHF / UHF DX Book, Shi 2019 | vienas + 1% AUC; suteikia ortakio spalvos 46% įvykio maršrutų ir 0% palyginimo | yra |
| Patvarumas 24 h | VHF / UHF DX knyga, Biancomano ir Leyshon | AUC nepakitęs, daugiau suvartotas ir daugiau klaidingų signalų | iš 1.3.1 |
| Normalus CRPL profilis su vietiniais Ns (− ΔN = 7,32 · e ^ (0,005577 · Ns)) vietoj nustatyto 40 N / km | Bean and Thayer 1959, NBS Monograph 4 | išbandytas tinkamumo patvirtinimo metu | 1.3.2 kaip alternatyva, išjungti dėl defekto |
| Viršrefrakcijos riba 79 N / km (ITU) vietoj 100 (Bean and Dutton, sluoksnis 1000- 850) | P.453, Lopez 2008, DX Book | išbandytas tinkamumo patvirtinimo metu | 1.3.2 punkte: agresyvus vertinimas kartu su apdairiu vertinimu (žemėlapis naudojamas apdairiai) |
| Klimato storis (apskaičiavimai B) mažiau svarbus nei deficitas | Rogers 1997 | pabandyti: naudoti A pelno | atlikti |
| Parabolinė lygtis GFS profilyje | PETOL, bangų sklidimas (Lytaev), Levy (knyga neskaityta, tik peržiūra), Lindquist | pirmoji versija, kurioje horizonte vyrauja skaitinis triukšmas (AUC 0,12-0,28): teisinga (domenas 8 km, sija 6 °); po korekcijos AUC 0,54-0,66 | naudojamas kaip antras skaičiavimas, nenusprendžia prognozė |
| Evanescencija: ± 7 dB virš 100 mylių, paraštė nuo 10- 20 dB iki 90- 99% | Bray 1961, Antena Book, GEKO 69 | kelių ribinės vertės | naudojamas: tikimybė laikotarpiu ir riba FT8 - 18,6 dB troposcatter |
| Bendras tūris ir lygiavertis atstumas Bray | IU3QEZ, Bray 1961 | Informacija | yra |
| Troposcatter P.617 ir RSGB kaip neapibrėžtis | TropoLink DL2FZN | Informacija | 1, 3 |

### Kas lieka iš modelių

Žemame jūros aukštyje (100-200 m), kurio GFS lygiai nemato (dabartinių duomenų šaltinių tyrimas); atspindys ir išsklaidymas gretimuose kalnuose (Vesuvio, Faito): pasiūlymas apskaičiuoti "per monte". ITM / Longley- Ryžiai išlieka statistinė nuoroda "įprasta diena" (Ns 301, climi), be kanalo. Nepaisoma autoriaus: AirScout, P.528), E sporadic (P.2001) ir 50 MHz (servoir EIRP, kad nedaugelis turi ir painioja su E sporadic).

### Pasiūlymas dėl darbo tvarkos

1. Išplėstinis patvirtinimas: pridėti filtruotą WSPR (net valandas be dėmės) ir QST 2022; pakartoti 432 MHz.
2. Pabandykite normalų CRPL profilį ir ITU slenkstį 79 N / km, vieną kartą, išlaikant tik tai, kas padidina AUC.
3. Palyginimas GFS profilių su I GRA2 radijo banga Brindisi ir praktika jūros QSO dienomis.
4. Iš naujo įvertinti parabolinę lygtį po skaitinės pataisos.

### Lapkričio 21 d. sprendimai (1.3.2 redakcija)

- Priimtas: apdairus įvertis (Bean and Dutton, 100 N / km) ir agresyvus (ITU, 79 N / km), pasiekti ortakiai tik iki 2500 m (pirmasis 1500 m, iškeltas po patvirtinimo); premija su dideliu padidintu slėgiu ir silpnas vėjas; jūros svoris su μ1 P.452; stočių dalis, atsižvelgiant į konduit taip pat iš dalies; ribinis sluoksnis virš keteros ( IT9GSF).
- Priimtas kaip alternatyva, išjungtas defektas: normalus CRPL profilis su vietos Ns.
- Tinkama parabolinė lygtis: AUC nuo 0,12 iki 0,54 (30 m antena) ir nuo 0,28 iki 0,66 (300 m).
- Žemė: Copernicus GLO- 30 DEM vietoj Terrain Tiles "sadi", kuris netoli kranto turi vaiduoklių redukcijos 80-100 m jūroje (ženklas IC8TEM, patikrintas taškas į Capri); sadi lieka rezervas.
- Orai žemėlapyje su jų poveikiu: ↓ palanki subsidence (Moler ir Holden 1960, vidutiniškai apie 2,5 dB tarp oro vyksta žemyn ir oro vyksta; + 1,5 dB), ↑ oro įkalnėje nepalankios (priklausomai giliai; − 2 dB), S scirocco palanki clue (ARRL vadovas § 19.4, inversijos labai stiprus Viduržemio jūros; nėra korekcija). Dvigubas simbolis, žalios / raudonos / oranžinės spalvos ir komiksų knyga žemėlapyje.
- Priimtas ITM 1.2.2 (NTIA / itm, viešasis domenas), išverstas į C #: pagal skaičiavimą atskaitos normalios atmosferos šalia P.452 ir galutinis ruožas po išėjimo iš tinklo, kuris baigiasi prieš korespondentą. Nebenaudojamas qradiopredict kodas, nes pagal GPL v2.

Be naudingo turinio indeksas: * Komunikacijų ketvirtis *, 1991 m. ruduo (nėra straipsnio apie tropo; Atkins straipsnis apie Scatter yra 1991 m. žiemą).

- Priimtas kaip vadovų principas (lapkričio 21 d.): "sklidimas negali būti apibendrinamas, bet tiriamas atsižvelgiant į vietinę morfologiją" ( DF5AI, pranešė IK3XTV "Propagation on 2 metrai"); cituojamas prognozės vadove (§ 1) ir tarybose.
- Priimtas (lapkričio 21, po 1.3.3) IK3XTV transalpinės angos interpretavimas: ne "konvekcinės ląstelės, pavyzdžiui, lęšiai", bet troposcatter sustiprintas turbulence į sluoksnio iš naujo perkelti, kai jis pasiekia bendro tūrio dalį (vadovas prognozes, § 8 ter). Nuo 1.3.4 ji patenka į taškinį skaičiavimą kaip laikinas troposcatter sustiprinimas, ne daugiau kaip 3 dB. Nebenaudojama tų pačių straipsnių: už Alpių ribų esančių signalų "autoamplifikacija" (pasyvi terpė nesustiprėja: aprašytos sąsajos išlieka paaiškinamos difrakcijos ir troposcatter; deklaruotos galios nenaudojamos taip, kaip nurodyta), sniegas, kuris "pagerina difrakcijos" (greičiausiai stabilus sluoksnis virš sniego, su mažesniu išsiveržimu).

- Kerr (1951) apžvelgė knygos (Lapkritis 22, PDF pateikė autorius): § 1.5, eq. 17 ir 18 ir lentelė 1.1. Skaičius eq. 18 skenuose yra neįskaitomi, tačiau 1.1 lentelėje patvirtinama: 20 pėdų → 1 cm, 80 pėdų → 10 cm, 400 pėdų → 100 cm, 2000 pėdų → 1000 cm, palyginti su 1,25, 10, 112 ir 1252 cm λmax = 0,014 · d ^ 1,5 (λ cm, d stovint; apvalusis stalas, "orientacinės vertės". Kerr hipotezės: viduje tinklo modifikuota indeksas lašai 4 · 10-8 per koją, tipiškas dydis žemų kanalų; su silpnesnių nuolydžių reikia didesnio storio. Kerr taip pat įspėja, kad λmax nėra grynasis pjūvis: bangų ilgiai kartais ilgiau vis dar gali būti stipriai paveikti ortakio.

- DL6WU review on the original (November 22, PDF VHF Communications 3 / 1982 provided by the autor): G. Hoch, "Extremely Long Yagi Antennas", pp. 130 and following. 1 pav. Dipolis, matuojamas tarp 432 ir 1296 MHz profesionalaus Vokietijos pašto poligono, su 7 dBd antena EIA, apskaičiuotas tikslumas 0,5 dB; 2,35 dB dvigubam bumo ilgiui, be soties iki maždaug 20 λ. Programa naudota 8.6 + 8.0.log10 (L / λ) dBd, t. y. maždaug 0,8 dB (mažiau kaip 1-2 λ); dabar jis naudoja 9,4 + 7,8 · log10 (L / λ) dBd (gradientas yra nurodytas, 1 λ vertė pavaizduota diagramoje, ± 0,3 dB). Atmesta formulė 10 · log10 (7,8 · L / λ), cirkuliuojanti tinkle: būtų 3 dB dvigubai, palyginti su 2,35 išmatuota.
- Booker and Gordon (1950): reference aprobed by "This Week 's Citation Classic" (Current Content, 8 February 1982, commentation by H. G. Booker): Proc. IRE 38, pp. 401-412. Komentaras patvirtina du vadovėliuose naudojamus punktus: laukas gautas už horizonto su Rayleigh paskirstymo, ir eksponentinė autocorrelation funkcija pažeidimų (non- Gaussian). Pradinis straipsnis nebuvo perskaitytas.

## 13. Iš IK3XTV straipsnių

Du Flavio Egano IK3XTV (Radijas Rivista: "Paplitimas 2 metrais" ir "Paplitimas VHF - kertančios ląstelės ir gravitacinės bangos") straipsniai papildo du dalykus, kurių ortakiai nedengia: popietės transalpines angas dėl konvekcijos, ir praktines taisykles, kur ir kada vamzdynai formuojami Italijoje. Tai stebėjimai iš stoties, o ne priemonės: jie turi būti traktuojami kaip hipotezės patikrinti su dėmėmis.

| Autoriaus pastabos | Kaip jis tampa indekso ingredientas |
| Vamzdžiai daugiausia susidaro nuo 100 iki 1000 m, retai didesnis; tarnauti didelį spaudimą didžiojoje srityje, geras insolation ir vėjo nebuvimas | ieškoti ortakio tik mažiau nei 1500 m; premija su dideliu padidintu slėgiu ir silpnas vėjas žemų sluoksnių |
| Per šiltus mėnesius vyko pasikartojantis Adrijos tarp Veneto ir Puglia, 700-800 km, kartais iki Graikijos; jūros maršrutai yra mėgstamiausi | didesnis svoris iki jūros maršruto dalies (pvz., ITU- R P.452 μ1) |
| Atitinkamas signalas turi būti vamzdyne arba šalia jo; su aukštomis antenomis ir keletu metrų nuo jo neturi patekti signalas | palyginti tinklo bazės dalį su abiejų stočių dalį (bus tarnauti QTH aukštis) |
| Tuo pačiu metu, tas pats kanalas: klausytis Gargano stoties 144 ir vienas 50 MHz | patikrinti: pagal Hepburn lentelę 50 MHz prašo daugiau nei 300 m švino, todėl įvykis rodytų vamzdyną labai dažnai |
| Jungtys Veneto - Pietų Vokietija už Alpių ribų, be vaizdo, beveik visada po pietų ir aukšto slėgio; nestabilūs signalai, kurie išnyksta ir vėl atsiranda po kelių minučių | nuo vamzdyno atskirtas indeksas, kalnų maršrutams |
| Konvekcinės ląstelės gimsta ant žemės ir kas 3 valandas kyla apie 1 km: 15 jų pasiekia apie 3 km, aukštis, kuris tarnauja aplenkti Alpes | naudoti ribinio sluoksnio aukštis (riba\ _ sluoksnis\ _ aukštis Open- Meteo GFS) išilgai kelio: rezultatas, kai jis viršija keterų aukštis |
| Alpės generuoja gravitacines bangas, kurios kartu su įprastais judesiais sukuria lūžio rodiklio pažeidimus. | hipotezes: stiprus vėjas statmenai grandinei 700 hPa kaip signalas aukso bangos; bandyti paskutinis |
| Labai aukštas inversija (iki 8 km išimtiniais atvejais) suteikia apie 800 km, bet praktiškai retai daugiau nei 300 km | inversija dalis išplečia taikymo sritį: naudingi duomenys įvertinti laukimo atstumą |
| Vasarą Žemės spindulys atitinka apie 16000 km (2,5 karto daugiau nei realus), su maždaug 60 dB mažiau nei 300 km | atitinka k50 = 157 / (157 − ΔN): k = 2,5 atitinka ΔN apie 94 N / km, tai yra superrefrakcijos |
| Daugiau nei 3000 km tik 20-30 ° diapazone į šiaurę ir į pietus nuo pusiaujo, virš jūrų, susijęs su Hadley ląstelių; didžiausia rugpjūčio Afrikoje-Europoje | už Europos ribų: naudinga ne suteikti aukštus indeksus Atlanto be grįžtamojo ryšio, ir Kanarų maršrutas - Didžioji Britanija |

Kiti naudingi duomenys iš tų pačių straipsnių:

- Optinis intervalas tarp dviejų h1 ir h2 metrų (d kilometrais): d = 4,1 · (Δ h1 + ^ h2), iki maždaug 20% normaliam refrakcijai.
- Evanescencija: lėta ir sekli su inversija; stipri ir greita su išsibarstymu ir įvaldančiomis ląstelėmis. Programa gali naudoti jį klasifikuoti skelbimus, jei vieną dieną ji turės SNR ataskaitas laikui bėgant.
- Dėl sporadinės E 144 MHz autorius praneša apie DF5AI ir Davis ir Johnson (Gamta 435, 2005) dėl ryšio tarp laiko ir E: iš tropo, bet atskiras rodiklis rytoj gali naudoti žaibo duomenis.
- Žemės atspindys dvipusio šuolio E: jūra K = 81 ir 5 S / m, kraštovaizdis ir miškai K = 13 ir 0,006 S / m, miesto vietovės K = 3 ir 0,001 S / m.

## 14. LIVETROPOVHF indekso hipotezės

Pradedamas pasiūlymas, kurį reikia aptarti: indeksas 0- 10 toje pačioje skalėje kaip Hepburn, apskaičiuojamas kiekvienam tinklo taškui ir kas 3 valandas nuo GFS (tuomet ECMWF ir ICON palyginimui), ir antras skaičius kiekvienam maršrutui, kuriame nurodoma, kiek maršruto dalis patenka į naudingą ortakį į pasirinktą juostą. Žemiau svarmenys yra mano prielaidos, turi būti kalibruotas su dėmės.

Apžvalga po Bean ir Dutton bei Kerr: profilio ribos yra iš jų lentelės 6.5 ir skalės pagal jų matavimus, todėl lūžio sluoksnis yra verta 10- 25 dB virš normalaus profilio. Tada indeksas apskaičiuojamas pirmiausia dB ir tada trunka 0-10; vertės, tame žirkle turi būti sukalibruotas su dėmės.

Apskaičiavimas po taško, žingsnis po žingsnio

1. T išskyros, santykinis drėgnis, geopozicinis aukštis ir vertikalusis greitis 1000, 975, 950, 925, 900, 850, 800, 700, 600 ir 500 hPa, plius T ir 2 m drėgnumas, žemės slėgis, sluoksnio aukštis ir vėjas 10 m.
2. Išmeskite žemutinius lygius; kiekvieno lygio N ir M apskaičiavimas (ITU- R P.453).
3. Apskaičiavimas − dN / dh kiekviename iš keturių sluoksnių Bean ir Dutton (1000-850, 850- 700, 700- 600, 600- 500 hPa) ir diagrama kaip subrefrakcijos, normalus arba superrefrakcijos su jų stalo slenksčių; viduje pirmojo sluoksnio taip pat naudoti tarpinius lygius rasti stipriausią gradientą ir jo dalį.
4. Kai dM / dh < 0 I rasti pagrindas ir ortakio viršuje, storis d ir trūkumas ΔM.
5. Palyginimas su mažiausiu juostos storiu pagal Kerr formulę (maždaug 180 m 144 MHz atveju, 89 m 432 MHz atveju, kaip Hepburn lentelėje); žemiau ribos konduit vis dar skaičiuoja, bet mažiau.
6. Aš stimuliuoju pelno dB lentelėje toliau (jums imtis geriausią sluoksnį, ne sluoksnių suma, plius anomalijos terminas) ir konvertuoti jį: indeksas = dB / 2.5, tarp 0 ir 10, todėl 25 dB verta 10. Neigiamas dydis (subrefrakcijos) rodo, kaip 0, bet lieka kelio duomenyse.

| Geriausia padėtis | Kriterijai | Numatomos pajamos |
| Normalus (nelaminuotas) | 6,5 (pvz., 20- 60 N / km nuo 1000 iki 850 hPa) | 0 |
| Šiek tiek daugiau nei įprasta | tarp viršutinės įprastos juostos ribos ir viršutinės lūžio ribos | 0-10 dB, proporcingai |
| Superrefrakcijos | virš sluoksnio slenksčio (100, 80, 70, 50 N / km laipiojimo), bet žemiau 157 N / km | 10-15 dB, proporcingai iki 157 N / km |
| Per plonas vamzdynas juostai | dM / dh < 0, storis mažesnis Kerr riba | 15 |
| Naudinga juosta jungtis | dM / dh < 0, storis virš Kerr ribos | 20 dB, + 1 dB kas 5 M deficito vienetus, ne daugiau kaip 25 |
| Subrefrakcijos | - dN / dh < 0 apatiniame sluoksnyje | - 10 dB |
| Nenormalus fonas (pridedate) | Pavyzdys Ns minus ITU žemėlapio klimatologinis To mėnesio N | 0,2 dB N vienetui, nuo -5 iki + 5 dB |

Apskaičiavimas pagal maršrutą (tarp dviejų lokatorių): imti didžiausią apskritimą kas 10 km, kaip jau veikia kelio indekso programa, ir apskaičiuoti taškų dalį su naudingu ortakiu ir vidutinę dalį bazės. CSIRO tyrime nustatyta, kad vamzdynas yra naudinga maršruto dalis (AIS maršrutai 162 MHz greičiu buvo 67% didesni nei kontrolės atvejais).

Pridėjus skaičiavimus kiekvienam maršrutui (nuo LA0BY ir W3SZ): 1) jūrų stotys, palyginti su baze ir antgaliu, nes didelis vamzdynas tarnauja tik tiems, kurie įvažiuoja; 2) troposcatter "grindys" su Yeh metodu, pradedant kampais, paimtais iš žemės profilio; tropas padidėja dB, ir rezultatas lyginamas su nuostoliu, kurį stotis sugeba įveikti ( KK6MC lentelė). Taigi indeksas taip pat tampa pasiekiamo atstumo įverčiu.

Koregavimai išbandyti vieną vienu metu (iš Bean ir Dutton ir LA0BY, IK3XTV, G4MCU jau vadove), turi būti laikomi tik jei jie pagerina palyginimą su dėmės:

- Maišymas (importante svarbu pagal autoriaus, dienos stebėjimo): diena žemėje, tarp vidurdienio ryte ir popietę su aiškiu dangumi, arba su vėjo ant žemės per keletą m / s, ortakiai ant žemės išnyksta; jų indėlis turi būti sumažintas. Ne jūroje.
- Dienos laikas (importante svarbu autoriaus nuomone, kasdien stebint): neracionalūs ortakiai gimsta saulėlydį ir krenta po saulėtekio; tai yra 3 valandų prognozavimo kontrolė.
- Vertikalus judėjimas: oras kyla iki 850- 700 HPa permaišo sluoksnius (neigiama pataisa), o oras nusileidžia po anticiklonu juos sustiprina (teigiamas).
- Slėgio tendencija: slėgis kelyje per pastarąsias valandas, didesnis ir stabilus signalas; mažėja, mažesnis signalas (bastano 2 hPa).
- Sinonimas: aukštas slėgis virš 1025 hPa, temperatūra 850 hPa daug virš žemės, karštas ir sausas oras teka šaltesnė jūra, maršrutas jūra.
- Kalnų keliai: antras "contative" indeksas ( IK3XTV skyrius), kurio popietinės ribos sluoksnis yra didesnis už kelio keteras ir aukštą slėgį.

Iš VHF / UHF DX Knyga, kiti pataisymai išbandyti: iš eilės dienų aukšto slėgio ant taško (daugiau dienų, daugiau taškų), padėtis, palyginti su anticiklono centre (premija kraštų), slėgio gradientas ant žemės kaip vėjo matu, stiprus lietus per pastarąsias 24 valandas (penalumas), mažas debesys sluoksnio krašto (penalumas tik didelių juostų). Dėl maršrutų kryptimi: palyginti maršruto azimuto su izobare.

Patikra

- LIVETROPOVHF archyvas: kiekvienas komercinis virš 500 km iki 144 MHz arba 432 MHz yra "renginys". Jame palyginamas maršruto indeksas ir reiškinių dažnis tuo pačiu paros ir mėnesio laiku (tas pats metodas CSIRO tyrime).
- Palyginimas su Hepburn ir F5LEN tomis pačiomis dienomis: kai trys indeksai atsiskiria, atrodo, kas buvo teisus pagal dėmes.
- 00 ir 12 UTC spinduliai: patikrinti, ar modelis mato inversijas, kad zondas matuoja.
- Istoriniai LA0BY atvejai (6-11 / 11 / 2003, 10 / 12 / 2004, 15 / 10 / 2011, 11 / 2 / 2015, 22 / 11 / 2018): tomis dienomis perskaičiuotas indeksas turi atitikti kanalus, kurių Hepburn nerodė.
- Ribos, kurios turi būti nurodytos vadove: atliekama 100-200 m beveik nematomas GFS slėgio lygių; patikimas prognozavimas 2-3 dienas, tada tik tendencija.

## 15. Lapkričio 21 d. šaltiniai (antroji dalis)

- ITU- R P.1812 (MaTLAB / Octave įgyvendinimas I. Stevanovic, OFCOM; GitHub eeveetza / p1812, Zenodo 10.5281 / zenodo.6141558). Savitasis maršruto VHF / UHF vietovės metodas, gautas iš P.452: "delta- Bullington" difrakcijos, troposcatter, ortakiai ir atspindys ant sluoksnių, engulfs ant žemės. Naudinga kaip trečias palyginimas šalia P.452 point skaičiavimo; ΔN ir N0 duomenys yra visiškai ITU produktai, todėl vartotojas juos parsisiunčia (pagal P.453).
- iš Costa, Rabanal Ramirez, Dias, IET MAP 2018 (doi 10.1049 / iet- map.2017.0992). 563 MHz, 27 taškai nuo 5 iki 30 km nuo netaisyklingos vietovės su mišku: P.526 su 12 m augmenija - 4,3 dB - 1,9 dB; P.1812 su 10 m įtempimo + 2,6 dB - 8,9 dB; dielektrinis pleištas - 4,8 dB - 9,2 dB; P.1546 + 5,0 dB - 11,7 dB. Programos atveju: liekamoji gero difrakcijos metodo paklaida yra apie 9 dB, o užtvindymo ant keterų aukštis (10-12 m) skaičiuojamas.
- PETOL (Ozgun e altri, Computer Physics Communications 182, 2011). Parabolinė lygtis split- žingsnis į vieną ir du maršrutus su plačiu kampo propagatorius (kampai iki 40- 45 °, prieš 10- 15 ° nuo siauras); apačioje su nuostoliais per transformuotas Furjė mišrus; žemės į žingsnius. Patvirtina pasirinkimą 1.3.2 (platus kampas, dirvožemio koeficientai) ir nurodo dvipusius apskaičiavimus stačių išlygų.
- MatWorks. `tropopl`: dujų nuostolių ir "lente" nuostolių sluoksniuotoje atmosferoje (P.835, P.676, P.453), be ortakių: kurių nuolydžiai mažesni nei -157 N / km, geometrinis optinis modelis netaikomas. Radiolokacinio sklidimo naudojimo refrakcijos atveju pavyzdys − ΔN = 7,32 · e ^ (0,005577 · Ns), tas pats CRPL pranešimas jau programoje. `propagationModel` ilgaamžiai ryžiai: iš anksto nustatytos horizontalios poliarizacijos vertės, laidumas 0,005 S / m, pralaidumas 15, reaktyvumas 301, vidutinis kontinentinis klimatas. Nėra MathWorks modelis užsiima ortakiais.
- ITU- R, grupės skaitmeniniai produktai 3. Duomenys ir programos P.453, P.452, P.1812, P.2001, P.528, P.1546; "integruoti" duomenys yra rekomendacijų dalis; nėra deklaruota perskirstymo licencija: jūs ir toliau leisti juos atsisiųsti vartotojui.
- EKA Navipedija, troposferinis vėlavimas. N = Nidrostatica + Numida; sausas Zenital vėlavimas yra apie 2,3 m ir drėgna viena dešimtys cm, kuris greitai kinta. Mums: GNSS stočių įvertintas drėgnasis vėlavimas yra netiesioginis integruotos drėgmės matas, galimas ateities liejimo šaltinis; šiandien jis nenaudojamas.
- Bray, K2LMG, "A Method for Determination V.H.F. Station Capabilities", QST November 1961, pp. 36-40 (read in original; recheck on November 22 on the PDF of the number of QST: all values below contact). Autorius, D. W. Bray (General Electric, Ithaca), sako, kad jis patikrino prognozes su mėnesiais reguliarių jungčių su K2GOI ir W4LTU. Per maždaug 100 mylių (7 dB žemiau ir 7 virš valandinio vidurkio) 13,5 dB nepralaidumas, beveik tiesinis (nuo 3,5 dB iki 50 mylių): standžiajam sujungimui pašalinti 7 dB. Horizonto korekcija: kiekvienas horizonto aukštis yra verta 69 mylių (111 km) papildomo atstumo (mažiau, jei neigiamas). Apie 10 × bum / λ (galios santykis), daugiau apie 4 dB dirvožemio atspindžio; SSB − 3 dB ir AM − 7 dB, palyginti su CW; 99% valandų: apie 180 dB - 60 mylių, 190 - 90, 200 - 240 (2 m). Nuo 1.3.2 laikotarpio tikimybė vietoj 5 dB apskaičiuotosios naudotos vertės, kai naudojamas laikotarpis, lygus 3,4 dB (± 7 dB, kai ± 2 dB).
- Dvipusis skaičiavimas (pabandykite su prototipu, lapkričio 21 d.). Didelis kampas parabolinė lygtis su realiu S RTM1 vietovės nuo Capri, 144 MHz, antena 10 m nuo žemės, standartinė atmosfera; matas: vidutinė galia nuo 20 iki 400 m iki 80 km maršrute link atitinkamo. Nuo Kapris ir Marina Grande vakaruose centro (kliūtis + 11-21 °, Monte Solaro): nurodant Faito (beveik už EA5 ir EA6, 150- 160 ° nuo ašies) signalą, paleistą į Ispaniją, užlipa 10-29 dB; nurodant Vesuvijus (110- 125 ° nuo ašies, tada iš skaičiavimo plano, tik orientacinis) 17- 25 dB. Iš Anacapri vakarai yra nemokama ir tiesiogiai laimi 8-22 dB. Tačiau absoliuti 1000-1300 km prototipo kaina nepatikima. EA3 EA5 EA6
- Be to, Komisija pažymi, kad, atsižvelgiant į tai, kad "Nouvel Areva NP" yra "Nouvel Areva NP", "Nouvel Areva NP", "Nouvel Areva NP" ir "Nouvel Areva NP", "Nouvel Areva NP", "Nouvel Areva NP", "Nouvel Areva NP", "Nouvel Areva NP", "Nouvel Areva NP", "Nouvel Areva NP", "Nouvel Areva NP", "Nouvel Areva NP", "Nouvel Areva NP", "Nouvel Areva NP", "Nouvel Areva NP", "Nouvel Areva", ". Kalnas yra bendras dviejų optikos maršrutų terminalas: valandinių medianų paskirstymas yra dviejų maršrutų sąsūka. Ilgas apie 14 dB nuokrypis (1-99%) nuo 26-40 dB vienodo ilgio ar kampo troposkacijos kelio; dienos pokyčių nėra. Signalas yra stipresnis nei troposcatter, bet silpnesnis nei idealus krašto: viršuje turėtų būti laikoma apvali (grubus apie 11,5 km). Už nugaros visada matomi dideli signalai.
- Barsis, NBS ataskaita 7652 (1963), trys maršrutai per kalną (Kolorado, Aliaska, Europa), 750- 900 MHz. Vidutinis nuostolis 10-20 dB didesnis už Fresnel-Kirchhoff skaičiavimą be žemės netoli terminalų; evanescencija yra vertinama suvyniojant dvi linijas į optiką; dirvos atspindžiai prie terminalų pablogina ilgalaikį blukimą. Nuo 1.3.2 įėjimas į vamzdyną prideda P.526 apvalinimą.
- Barsis and Johnson, NBS TN 88 (1961). Ilgalaikis kosmoso bangos tirpinimas, net ir kelyje už kalno (§ 5).
- Norton, "System loss in radio banga distribution", J. Res. NBS 63D (1959). Kreivės, iš kurių Bray gauna nomogramos; apibrėžtys sistemos nuostolių, perdavimo ir pagrindinio, proceso antenos padidėjimas.
- Ryžiai, Longley, Norton ir Barsis, NBS TN 101 (1965- 67), 1 ir 2 tomai. vienas kraštas ir kliūtis, suapvalinta su atspindžiais ir be jų (7 pastraipa, su Pikes smailės pavyzdžiu), ilgalaikis kintamumas V (0,5) ir Y (q), jei tai klimatas (tempti kontinentiniai ir kiti). Mums: suapvalintų kliūčių ir kintamumo funkcijų metodas yra nuoroda į kalibravimo neapibrėžtį.
- ITU- R P.526- 16 (11 / 2025). Formulė (31) J (ν), identiška programos; Suapvalintas hurdle (32) - (36), dabar naudojamas; multi- Split metodai (Bullington ir pilnas metodas § 4.5).
- ITU-R P.1812- 8 (2025) ir -7. Visas tekstas atgautas: specialus 30 MHz-6 GHz metodas (delta- Bullington, troposcatter, ortakiai ir atspindys ant sluoksnių, ingombs). Į -7 įspėja, kad jos troposcatter (nuo P.617-5) yra negerai iki 66 km. Tai tebėra galimas trečiasis palyginimas.
- Paulus, "IREPS garavimo modelio plastinis taikymas", NOSC TR 966 (1984) ir Radio Science 20 (1985). Ataskaita skaityti 22 / 11 / 2026 (DTIC ADA146528, nuskaityti su pripažintu tekstu, kartais neįskaitomas). Pagal laivus matuojami oro ir jūros skirtumai pasidaro stabilūs: šiltesnis jūros oras 31-35% laivų duomenų, palyginti su 0,7-7% NOAA šernų, taikomas laivų trikdytiems mikroskopams ir SST, paimtiems iš aušinimo vandens (maždaug + 1 ° C). Jeske modelis tada suteikia 40 m ir daugiau nei 15% metų ir aukščio, kad šokinėti nuo 10 iki 40 m per 24 valandas su stabilaus laiko, nepatvirtinta radijo priemonėmis. Siūloma pataisa (išbandytos ribinės vertės 0, - 0,5 ir - 1 ° C; 1 ° C geriau atitinka 1982 m. birželio mėn. radijo ryšio priemones): jei skirtumas tarp oro ir jūros yra − 1 ° C arba mažesnis už išmatuotą skirtumą; priešingu atveju apskaičiuokite aukštį - 1 ° C ir 0 ° C, o jei jis yra mažesnis - - 1 ° C, laikote tai. Stabili pusė didėja, mažėja. Birželio mėn. Marsdenas 120 kv. m: vidutinis aukštis nuo 16,8 m (klimatologija) iki 7,6 m. Modelio lygtys nėra nurodytos pranešime (žr. Jeske ir Hitney). Programa naudoja neutralų profilį, kuris neturi stabilaus perskirstymo, ir riboja δ iki 40 m: ji eina ta pačia kryptimi; su modelių SST (ne iš laivo) matavimo klaidą, kad Paulius ištaiso sveria mažiau.
- Levy, "Paraboliniai lyginimo metodai elektromagnetinių bangų propagavimui" (IEE 2000). "Google" bibliotekose yra tik peržiūra: Aš neskaitau.
- VK3FS (3fs.net.au). Kokybės įžanga: aukštos ortakiai 450- 2000 m ilgiau angų ant žemės; 300-600 m inversija ramioje ir ramioje naktimis, kuri išnyksta po saulėtekio; stipresni pakrančių inversijos popietę; 800- 1300 km tipiškas, 1400- 1600 km dažnai atliekami. Laikantis dienos / nakties perspėjimų ir 2500 m viršutinės ribos.

## 16. Šaltiniai

Taisyklės ir formulės

- [ITU- R P.453- 13 (2017), radijo lūžio rodiklis:](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)formulė ir duomenys
- [ITU- R P.452- 18 (2023), prognozuojant trukdžius tarp antžeminių stočių virš 100 MHz: lygiavertis spindulys, β0, troposcatter,](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)ortakiai
- [ITU- R P.617-5, sklidimas už horizonto radijo tiltams](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)
- [Shi et al., "A Complete Study on Wave Maximumlength of Elektromagnetic Propagation in different Evaporation Ducts", IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

Orų su oro modeliais prognozavimo tyrimai

- [P. Lopez, "5-metų 40-km rezoliucija pasaulinė klimatologija superrefrakcijos žemės pagrindu radiolokacinės meteorologijos", ECMWF Tech. Memo 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO), "Tropospheric Ducting Prediction based on GFS Model Data at... the CSIRO Murchison Radioastronomy", Observatory arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (FMI), "Effects of model resolution on predicting atmosferal tocting conditions with Harmonie- AROME", EGUfield 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

Mėgėjų radijo prognozės

- [W. R. Hepburn, "The Hepburn Tropo Index (HTI)"](https://www.dxinfocentre.com/propagation/hti.htm)ir [](https://www.dxinfocentre.com/propagation/history.htm)metodo istorija
- [W. R. Hepburn, "Mažiausiai naudojamas dažnis vs Storis Docting Layer"](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, "Worldwide refrakcijos indekso prognozės"](https://tropo.f5len.org/WW/info.php), [nauja 2026](https://tropo.f5len.org/?p=504)versija, [pastaba dėl pavyzdžio ARPEGE](https://tropo.f5len.org/author/f5len/)

Duomenys

- [Open- Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api), [pastaba dėl slėgio lygių](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)
- [NOAA NCEI, Integrated Global Radiosonde Archive (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RAOBget, radijo potvynių scenarijų Vajomingo universiteto](https://github.com/NCAR/RAOBget)

Klasikiniai tekstai (nurodo programos autorius, turi būti konsultuojamasi dėl galutinių formules)

- B. R. Bean, E. J. Dutton, * Radio Meteorologija *, NBS Monograph 92, 1966, 435 puslapiai (perspausdinti Dover 1968): vertikalūs profiliai N ir M, gradientas gaudymo pagal -157 N / km, palyginti su radijo bangų; 5 skyrius "Sinoptic radijo meteorologija". Bazė 2-4 pakopos apskaičiavimo pagal tašką. Nemokamai: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [NIST kortelė, doi 10. 6028 / NBS.MONO.92](https://www.nist.gov/node/1770911), [Interneto archyvas](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf)kopija.
- L. Boithias, * Radio bangų propagavimas *, McGraw- Hill 1987 (išversta D. Beeson iš prancūzų, 330 puslapių, ISBN 0070064334; taip pat Šiaurės Oksfordas Akademinis): mechanizmai už horizonto, bendras apimtis išsibarstymas ir prarasti prijungimo antenos-mezzo. Tik popierius ([bibliotekinė kortelė](https://libcat.library.tamu.edu/Record/in00000014812/Description)).
- M. P. M. Hall, * Troposferos poveikis radijo ryšiui *, IEE elektromagnetinių bangų serija Nr. 8, Peter Peregrinus 1979, 206 puslapiai (ISBN 0906048257; brossura 0863410863): sinoptinės situacijos ir sklidimas VHF / UHF. Pagrindas sinoptinės ingredientų. Prekyba IET ([](https://libcat.library.tamu.edu/Record/in00000636385)kortelė).
- H. G. Booker, W. E. Gordon, "A Theory of Radio Scattering in the Troposphere", * Proceedings of the IRE *, vol. 38, n. 4, April 1950, pp. 401- 412: Apgaubo teorija iš lūžio indekso disogeniškumo. Centrinė idėja: difuzinė galia priklauso nuo lūžio indekso turbulentinių svyravimų intensyvumo ir masto (eksponentinė koreliacija); netoli horizonto vyrauja difrakcija, toliau išsibarstymas; Autorių nuomone, audrų masto ir indekso variacijų, kurių tikimasi meteorologijos dėka, pakanka, kad būtų paaiškinti išmatuoti laukai. Nurodyta 328 straipsniai (Scilit), nėra nemokama prieiga kopija. Kortelė Semantic Scholar ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); visą tekstą apie IEEE Xplore mokama ([doi 10.1109 / JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)).
- C. M. Crain, J. R. Gerhardt (Elektros inžinerijos tyrimų laboratorija, Teksaso universitetas), "Matavimai Parametrų dalyvauja radijo tomografijos Troposferoje teorija", * IRE *, vol. 40, n. 1, 1952 m. sausio, pp. 50-54, doi 10.1109 / JRPROC.1952.2739 pateikti tikrąsias vertybes, kad Booker ir Gordon turėjo tik apskaičiuotas ([Skilit](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)kortelė).
- "The Constants in the Equation for Atmosferos refraktive Index at Radio Spectrates", * Proceedings of the IRE *, vol. 41, n. 8, August 1953, pp 1035- 1037: siūlo peržiūrėti sroves tada naudoti (79 K / mb ir 4800 K) į formulę N; tai iš čia, kad vertės 77,6 ir 3,73 · 10 ^ 5 ITU formulės ([Q](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E. C. S. Megaw, "Fundamental radio scatter distribution teorija", * Proceedings of the IEE Part C: Monographics *, vol. 104, n. 6, September 1957, pp. 441-455, doi 10.1049 / pi- c.1957.0052: spektras indekso svyravimų nuo turbulence teorijos, pilnas išsibarstymas už bendro horizonto ir geometrijos poveikis mokama IET ([Shilit](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)kortelė).
- * Procesai IRE *, spalis 1955, specialus numeris sklaidos (K. A. Norton ir J. B. Wiesner), su Bullington, Staras ir kiti: nemokamai PDF [World Radio Istorija](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf).
- D. E. Kerr (redagavo), * Trumpų radijo bangų populiarinimas *, MIT Radiacinės laboratorijos serija vol. 13, McGraw- Hill 1951 (Restampe Dover 1965 ir IEE 1987): garavimo teorija ir paviršiniai kanalai jūroje. Skaitmeninė kopija: [World Radio Istorija](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); IET leidimas [IET skaitmeninė biblioteka](https://shop.theiet.org/propag-short-radio-waves).

Naujausi VHF troposferinės difuzijos rodikliai: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Australija), "A Study of VHF Troposcatter Communication Channels in South Australia", in * Digital Signal Processing for Communication Systems *, Springer 1997, pp. 103-112 ([Springer](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)). Tekstas už mokestį; iš abstraktaus jis susijęs su sklaida, o ne tinklo. Naudinga tik tuo atveju, jei norite kalibruoti su VHF kanalo duomenų troposcatter terminą.

Skaitmeniniai modeliai ir jūros maršrutai (nurodo programos autorius)

- M. Levis, * Paraboliniai lyginimo metodai elektromagnetinių bangų modeliavimui *, IEE elektromagnetinių bangų serijai # 45, 2000, 336 puslapiai (ISBN 0852967640): parabolinė lygtis lūžio profiliams, kurie keičiasi kelyje ir orografijoje; skyriai apie troposferos sklidimą, spindulius ir kelius, sklidimas jūroje. Tai būtų naudinga lauko skaičiavimo kelyje, už indeksas. Mokėjimui taip pat elektronine forma iš IET ([IET kortelė](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)).
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (Leicester universitetas): 2 GHz matavimai Lamanšo sąsiaurio salose ir Kanale. Daug darbų ir disertacija yra nemokama universiteto archyve: [ilgalaikė statistika apie garavimo ortakio, Radio Science 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434), [parašo pokyčiai trijuose jūrų maršrutuose, Radio Science 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710), [Pavadinimas N. Mufti 2012 su maršrutais 50 ir 140 km taip pat VHF](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854), [tesis I. Alam 2002 dėl reaktyvumo apskaičiuojamas iš oro duomenų, 240 MHz ir 2 GHz X3 UQ, [tesi.](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus, "Praktinis garinimo modelio taikymas", Radio Science vol. 20 n. 4, 1985, p. 887 (ne IEEE Sandoriai dėl Anteno ir Propagation), doi 10.1029 / RS020i004p00887 ([įterpti](https://zendy.io/title/10.1029/rs020i004p00887)): garinimo latako aukštis nuo paviršiaus oro duomenų. Atlaisvinkite prieš jį esančią techninę ataskaitą: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf).

T. Lindquist, * Wave Propagation Models in the Troposfere for Long- Range UHF / SHF Radio Connections *, Master 's disertacija, University of Karlstad, 2 June 2020, 106 puslapiai (pateikė programos autorius). Tai žingsnis po žingsnio vadovas, nemokamai, rašyti su paraboline lygtimi skaičiavimą: split- žingsnis sprendimas (net plačiu kampu), ribos sąlygos sausumos ir jūros, įžeminti į žingsnius, spindulio stebėjimas linijinių profilių kartais, su skaitmeniniais patikrinimais; cituoja PETOL nemokamai įrankis MATLAB. Du naudingi duomenys vienu metu: (1) N jautrumas oro kintamiesiems, δN ≥ 0.26 δP + 4,3 δe - 1,4 δT (hPa ir K, apie 20 ° C), t. y. 1 hPa paklaida garų slėgiui tris kartus sveria 1 ° C paklaidą temperatūroje: modelio drėgmė, kuri turi būti tikrinama pirmiausia; (2) vidutinis garavimo ortakio aukštis: 5- 6 m Šiaurės jūroje, 13- 14 m Viduržemio jūroje, daugiau kaip 20 m Persijos įlankoje, visada per mažas 144 ir 432 MHz. Autoriaus išvada (1-10 GHz dažniai): už horizonto patikimiems ryšiams beveik visada reikia ortakio.

Paplitimo programinė įranga dirvožemio profilyje (horizontas ir kliūtys, o ne numatomas tropas)

- [Radio Mobile](https://ve2dbe.com/rme.html)iš Roger Coudé VE2DBE: nemokama, Windows (taip pat Linux ir Mac su vynu), Longley- Rice / ITM modelis, reljefo profilis automatiškai išgaunamas iš kvotų duomenų ([atsisiųsti](https://ve2dbe.com/download/download.html)).
- [SPLAT!](https://qsl.net/kd2bd/splat.html)John Magliacane KD2BD: GPL, 20 MHz- 20 GHz, Longley- Rice ir ITWOM v3.0 modeliai, kliūčių ir nuostolių ataskaitos tekste; naujausia versija 1.4.2 2014.
- [ITM, NTIA / ITS](https://its.ntia.gov/software/itm)oficialus kodas: Longley- Rice modelis (20 MHz- 20 GHz) su šaltiniu ir dokumentais, pagrindas Radio Mobile ir SPLAT!. Kodas yra nemokama ir jūs galite pridėti prie skaičiavimo LIVETROPOVHF kelyje (nuostolių dėl dirvožemio, turi būti pridėta prie tropo indeksas). Originali ataskaita: A. G. Longley, P. L. Rice, Troposferinio radijo perdavimo praradimo per nereguliarią terrazę prognozavimas: kompiuterinis metodas - 1968 ", ESSA Techninė ataskaita ERL 79- ITS 67, 1968 m. liepa (NTIA kortelė istorinei programinei įrangai: X1 https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITU- R, programinė įranga ir skaitmeniniai duomenys](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): nemokami ΔN ir N0 metinių medianų žemėlapiai tinklelyje 1,5 °, naudojami P.452 ir P.617 - naudingi kaip indekso pamatinės klimato vertės.
- Pathload (Contract Telecommunication Engineering): komercinė radijo tiltų programa; Aš neradau viešo šaltinio, kuris patvirtintų ITU rekomendacijas, todėl patikrinti.

Pakartotinio naudojimo gitHub kodas LIVETROPOVHF

| Projektas | Ką tu darai? | Kalba | Naudojimas galimas mums |
| [NTIA / ITM](https://github.com/NTIA/itm) | modelis Longley-Ryžių pareigūnas NTIA, su pakuotėmis. GRT |  C++, C#  | žemės praradimas kelyje: integruoja tiesiai į programą, kuri yra C # |
| [fuzzzeee / itm](https://github.com/fuzzzeee/itm) | ITM C + + kodas C # vertimas |  C#  | alternatyva be vietinių bibliotekų, taip pat naudinga Windows 7 versija |
| [eveetza / Py452](https://github.com/eeveetza/py452) | ITU- R P.452, identiška MATLAB / Octave versijai, kurią patvirtino ITU- R WP 3M; MATLAB versijai, paskelbtai Šveicarijos Federalinės ryšių tarnybos | Python, MATLAB | nuoroda į vertimą į C # troposcatter formulės ir atliktas, su ITU patvirtinimo pavyzdžiai |
| [bwinkel / pycraf](https://github.com/bwinkel/pycraf) | P.452 baigtas, P.676, SRTM profiliai; GPL v3, bet ITU duomenų failai yra ne nemokamai komerciniam naudojimui | # | mūsų rezultatų kontrolė |
| [ITU- Rpy](https://github.com/inigodelportillo/ITU-Rpy) | daug ITU-R rekomendacijų, įskaitant P.453-13, patvirtintų ITU pavyzdžiais | # | P.453 N formulės ir klimato žemėlapių patikrinimas |
| [NCAR / RAOBget](https://github.com/NCAR/RAOBget) | atsisiųsti radijo bangą Vajomingo universiteto teksto formatu | # | diagrama atsisiųsti C # Italijos stočių profilius |

Pastaba: dabartinė versija P.453 yra [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), atsisiųsti nemokamai; dokumente aš naudojau 13, kad būtų atšauktas.

Jau programos vadovai ir daugkartinio naudojimo čia: LA0BY (Nordic VUSHF 2019), IK3XTV ("Propagation on 2 metrai" ir "Propagation VHF - įtikinamų ląstelių ir gravitacinių bangų", Radio Magazine, pateikė programos autoriaus), DF5AI/DK5YA (2003), G3YGF (QST 1983), The VHF / UHF DX Book cap. 2, G4MCU (2003) KP4MD
