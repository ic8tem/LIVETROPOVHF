# Pinagmulan at pormula

> Awtomatikong salin ng Italyano: Kung pag - aalinlanganan ay sulit naman ang giyang Italyano.

Gumagawang koleksiyon ng mga mapagkukunan na isinangguni para sa HILETROPOVHF tropo hinulaan (20–21 Nobyembre 2026): Pangunahing pisika, makukuhang datos, inilathalang mga indiks, pormula, karanasang radio amateur, fakultad at desisyon. Ang mga pormula ay nilagyan ng bilang gaya ng sa LaTeX, (1), (2),..., at sa teksto na natatandaan nila sa bilang ng mga bracket. Inilalarawan ng The Decree Manual kung paano ito ginagamit ng programa.


## 1. Lugar ng Awtor

IC8TEM mga obserbasyon na bumalangkas sa akda, na may feedback na matatagpuan sa mga mapagkukunan.

- Ang heograpiyang Italyano ay nangangailangan ng diffraction. Sa pamamagitan ng mga ridge, promontory at mga isla ilang dosenang kilometro mula sa mga istasyon, ang mga kontribusyon sa diffraction ay dapat kalkulahin na may Fresnel parameter sa pangunahing gilid, hindi lamang sa makinis na lupain. Pagpupulong: sa paghahambing ng iregular na kalupaan sa 563 MHz (Rio de Janeiro, 27 puntos sa pagitan ng 5 at 30 km), ang pinakamahusay na pamamaraan ay ang P.526 na may mga gilid ng talim, P.1812 at ang diffraktibong kalso, na may katamtamang pagkakamali sa pagitan ng −5 at +3 dB at distansya ng mga 9 dB; ang P.1546 ay ang pinaka pessimista (mula sa Costa, Rabanal Ramirez at Para sa matarik na mga dalisdis malapit sa istasyon ng PESOL na kanilang ipinagugunita ang malawak na propaktor at ang dalawang eksimalways.
- Upang makapasok sa isang malayong padaluyan, kailangan mo si Fresnel. Kung ang padaluyan ay nagsisimula sa kabila pa roon ng abot - tanaw, ang hudyat ay dumarating sa atin sa pamamagitan ng diffraction sa mga hadlang hanggang sa pasimula nito; mula 1.3.2 tinataya ito ng programa (Manual of the predictions, § 10 ter). Pagpupulong: Binabanggit ni Rohde & Schwarz (2023) na ang mga istasyon sa kahabaan ng isang mataas na duct ay kadalasang hindi pumapasok, samantalang yaong malapit sa dulo ay oo.
- Mabilis magbago ang mga kalagayan. Naroroon ang mga teoriya, subalit ang mga kalagayan ay nagbabago sa mga minuto, at walang sistema ng panghuhula ang kumukuha ng impormasyon sa tunay na panahon: ang GFS ay lumalabas tuwing 6 na oras, na may haba ng panahon at 0.25°, at ang isang hula ay maaaring mawalan ng halaga sa loob ng maikling panahon. Pagpupulong: Bray (QST, Nobyembre 1961) pagsukat ng orasang median reversions ng humigit-kumulang ±7 dB sa 100 milya; ang mga duct ay gumagalaw na may atmosperikong oras (Rohde & Schwarz); ang ating QSO rehistro ng 29/12/2021–2/1/2022 ay nagpapakita ng mga pagbubukas na lumalaki at bumabagsak sa loob ng mga oras.
- Ang pinakamahusay ngayon-casting ay nananatiling DX cluster–PSKReporter network. Upang malaman kung ano ang nangyayari sa ngayon, ang mga kumpol na batik, ang PSKReporter (at ang WSPR) ay pumapalo sa anumang padron: ang mga ito ay mga hakbang. Sinasabi ng hula kung saan titingin at kung gaano ito katagal; sinasabi ng mga komersiyal kung mayroon. Ang programa para rito ay nagtatakda at humuhula sa iisang mapa.

## 2. Layunin

Ang ideya ay isang ikatlong indise, na kinakalkula ng PHETROPOVHF mismo simula sa patayong profile ng isang weather model, na idadagdag sa Hepburn at F5LEN sa halip na palitan ito. Sa ngayon ay handa nang basahin ng programa ang kanilang mga mapa: sasabihin din ng isang indise na *bae* may bukasan (quota at kapal ng padaluyan, at maaaring mangyari ito sa mga batik na natipon na ng programa.

- Kung ano ang maibibigay mo: probabilidad ng padaluyan sa bawat punto at bawat oras, bahagi ng pinakapuno at tuktok, kapal, pinakakaunting frequency na nakulong, at sa kahabaan ng isang landas ang maliit na bahagi ng ruta sa loob ng isang padaluyan.
- Ang hindi maibibigay: ang manipis na mga duct (sa loob ng ilang sampung metro) na hindi nalulutas ng mga pandaigdigang modelo sa 0.25°, at ang mga daluyan ng pagsingaw sa dagat, na naglilingkod higit sa lahat mula sa GHz pataas.
- Unang praktikal na layunin: 144 at 432 MHz sa Europa, na may beripikasyon sa mga batik ng kumpol ng DX, PSKReporter at mga lokal na decoding.

## 3. Pangunahing mga Physic

Ang lahat ay nagsisimula sa refractive N kinalkula sa bawat profile level: ang daluyan ay kung saan binabawasan ng binagong reactivity M ang pag-akyat, doon bumabagsak ang dN/dh sa ibaba −157 N/km ([ITU-R P.453-13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)).

Refractivity (P at sa hPa, T sa kelvin):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Ang presyon ng singaw mula sa relatibong halumigmig H (%) at temperature t (°C), sa ibabaw ng tubig sa pagitan ng −40 at +50 °C:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

Modified reactivity (h sa km, o 0.157 · z na may z sa metro) at duct standard:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN/dh (N/km) | Regulasyon | Epekto ng 144 MHz |
| 0 | sub-refraction | nabawasang bilis ng pagdaloy |
| mula 0 hanggang 1079 | normal (mga 1040) | abot - tanaw sa radyo na katumbas ng radius 4/3 |
| mula −79 hanggang 10−157 | super-refraction | Mahabang daloy, "napakabuti" |
| sa ibaba −15 | Drake (pagbaba) | nakulong na mga hudyat, DX mahigit 1000 km |

Kung paano kukunin ang N mula sa isang modelo sa antas ng presyon: para sa bawat antas na kinukuha mo ang T, relatibong halumigmig at geopotential high, tinataya mo ang N at M, pagkatapos ay ang pagbabagu - bago sa pagitan ng kalapit na antas. Ang mga antas sa ilalim ng lupa ay dapat itapon (sa 1000 hPa sa Alps ay nasa ilalim ng lupa).

Minimum na nakulong na frequency: ang padaluyan ay pumapatnubay lamang sa mga alon kung ihahambing sa kapal nito. Ang Hepburn ay naglalathala ng kapal ng mesa → minimum na magagamit na frequency ([DX Info Centre](https://www.dxinfocentre.com/propagation/luf.htm)); ito ay isang soft limit, kung saan ang signal ay unti-unting bumababa.

| Pagiging Makapal | Hindi Sapat na dalas |
|  61 m  | 962 MHz |
|  91 m  | 464 MHz |
|  122 m  | 276 MHz |
|  152 m  | 185 MHz |
|  183 m  | 133 MHz |
|  213 m  | 101 MHz |
|  305 m  | 53 MHz |

Sa range ng VHF (kadalasang 120–300 m) ang mga halaga ng mesa ay sumusunod ng mahusay na batas tungkol sa konstante; mas mababa sa 100 m ang pormula ay nagpapababa sa dalas na umaabot sa 30–40%. Ibagay ito mismo (hindi isang pormula na inilathala ni Hepburn), na may f sa MHz at d sa metro:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

Sa pagsasanay: para sa 2 m nangangailangan ito ng isang padaluyan ay kadalasang hindi bababa sa 170–180 m, sapagkat ang 70 cm ito ay sapat na halos kalahati. Ang pormula na kumakalat sa net, λmax = 0.014 · d^1.5, ay galing sa Kerr (1951, Kabanata 1, eq. 18) at may bisang muskulo sa mga sentimetro at nakatayo, para sa isang padaluyan kung saan ang M ay bumabagsak na li halos: na ang mga yunit na ito ay nagbibigay para sa mga 2 m 180 m at para sa mga 70 cm 89 m, na parehong mga halaga ng mesa ng Hepburn, na nagmumula dito. Si Kerr ay nagbabala na hindi ito net cut: kahit na ang mga alon ay ilang ulit na mas matagal na dumaranas ng maraming daluyan ng tubig, at isang paraan na "wala" ang kaunti ay maaaring mapabuti ang pagtanggap kahit na ang mga istasyon ay lumabas sa daluyan ng tubig. Ang isang 2019 IEEE Access study ay nagpapatunay na ang mga klasikong pamamaraan (Kerr, Kukushkin) ay hindi naglalarawan ng tunay na hiwa ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)): ang stall ay dapat gamitin bilang pahiwatig, hindi bilang isang pader.

## 4. Makukuha nang libre ang mga datos

Ang mahinang punto ay patayong resolusyon: sa pagitan ng 1000 at 850 mga modelong hPa ay nagbibigay ng antas tuwing 25–50 hPa, iyan ay tuwing 200–450 m, kaya't nakikita nilang mabuti ang mga inversion hanggang 500–2000 m ngunit hindi ang mga manipis na duct malapit sa lupa. NOBYROPOVHF ay gumagamit na ng Open-Meteo upang isobare, temperatura hanggang 850 hPA at inversion: ang index ay maaaring magsimula mula sa parehong tawag.

| Pinagmumulan | Ang nagbibigay | Resolusyon | Gamitin ang indise |
| [Open-Meteo, GFS](https://open-meteo.com/en/docs/gfs-api) | T, relatibong kaumiduhan, popotetikal na taas sa pamamagitan ng antas (1000, 975, 950, 925, 900, 850 hPa...); taas ng hangganang patong | 0.25° sa antas ng presyon; 16 na araw, oras na umaabot sa 120 h, na binabago tuwing 6 h | profile N at M, sneak, base at itaas ng duct; nasa programme na |
| [Open-Meteo, ECMWF](https://open-meteo.com/en/docs/ecmwf-api) | Gayunding mga antas mula sa mga modelong open-data 0.25°; ang lupa ay nagbibigay ng puntong hamog |  0,25°  | Pangalawang opinyon, kapaki - pakinabang sa concordance sa pagitan ng mga modelo |
| [Open-Meteo, DWD ICON](https://open-meteo.com/en/docs/dwd-api) | T at relatibong halumigmig sa antas ng presyon; sa ICON- D2 ay may 800, 900 at 925 hPa | ICON-EU mga 7 km | ikatlong opinyon sa Europa |
| [Radiosunds, Univ. Wyoming](https://github.com/NCAR/RAOBget) | Nasukat na mga profile, dalawang launch sa bawat araw (00 at 12 UTC), text format sa bawat istasyon | malaking antas, kahit na sa bawat ilang dosenang metro | index verification; tingnan ang maninipis na duct na hindi nakikita ng modelo |
| [GROOS, NOAA NCEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive) | Ang makasaysayang arkibo ng pagbobrodkast, isang file bawat istasyon, ay mas hinangong parameter | gaya ng nasa itaas | Paglalarawan sa mga taon ng datos at paghahambing sa mga bahagi ng nakalipas |
| ERA5 (Copernicus) | Oral reanalysis mula noong 1940, 37 antas ng presyon | 28 km | Mga estadistika at calibration, hindi ang paghula |

- May Open-Meteo geopotential high ay nasa itaas ng antas ng dagat: ang mga antas na bumabagsak sa ilalim ng lupa ay dapat itapon ([Open-Meteo, itaas na hangin](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)).
- Sa lupa ay idinaragdag ang T at halumigmig sa ganap na 2 m: ang mga ito'y nagbibigay ng unang kahabaan ng bangin, ang isa kung saan ang mga kanal sa ibabaw ng gabi ay isinisilang sa kapatagan at sa dagat.
- Italian broadcasts sa University of Wyoming archive (weather.uwyo.edu/upperair/swick.html): mga istasyon at numero WMO sa mesa sa ibaba.

| Istasyon | W | Kabihasnan | Katagalan | Bahagi |
| Udine/Campoformido |  16044  |  46,03  |  13,18  |  94 m  |
| Milan/Linado |  16080  |  45,43  |  9,28  |  103 m  |
| Gawain sa Dagat |  16245  |  41,65  |  12,43  |  12 m  |
| Brindisi |  16320  |  40,65  |  17,95  |  10 m  |
| Trapani/Birgi |  16429  |  37,92  |  12,50  | mga 14 m |

Ang text format ng Wyoming ay nagbibigay para sa bawat antas na presyon, taas, temperatura, punto ng hamog at halumigmig: sapat para sa N at M. Wyoming ay walang opisyal na interface, ang mga script ay ginagamit bilang [RAOBget ng NCAR](https://github.com/NCAR/RAOBget); ang College of DuPAge ay nag-iingat ng mga kopya sa teksto sa pamamagitan ng istasyon ([e.g., 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)).](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. Mga Indise at inilathalang mga pamamaraan

Ang Hepburn at F5LEN ay nagsisimula mula sa parehong mga pisikal na sangkap ngunit hindi inilalathala ang kumpletong algorithm; ang mga siyentipikong pag-aaral ng 2008–2026 sa halip ay naglalarawan sa pamamaraang hakbang-pamahayan, at mula roon ay sulit na gayahin ang resipi.

| Pamamaraan | Simula ng datos | Kung ano ang tinataya | Kapaki - pakinabang na mga nota para sa atin |
| [Hepburn Tropo Index](https://www.dxinfocentre.com/propagation/hti.htm) | hilaw na mga modelo ng panahon ng Canada Meteorological Service (MSC, gaya ng nakasulat sa ilalim ng mga mapa nito; hindi GFS/NAM), mga mapang idinisenyo na may GRADS, proprietary algorithm ([storia](https://www.dxinfocentre.com/propagation/history.htm)) | 0–10 linyar na sukatan ng signal kurba: 0 normal, 1–2 super-refraction, 3–4 ilang duct, 5–6 difficated ducts; negatibong halaga = sub-refraction | ang sukat na dapat tularan, kaya ang ikatlong indise ay agad na maihahambing sa unang dalawa |
| [F5LEN, pandaigdig na paghula](https://tropo.f5len.org/WW/info.php) | para sa Europa ang modelong ARPEGE ng Météo-France, 7.5 km, mas maraming antas sa pagitan ng 2 m at 1500 m ([tone ng awtor na](https://tropo.f5len.org/author/f5len/)) | Katamtamang indise para sa may - ari ng lupa, 40 baitang mula sa 3 oras, 4 na kalkulasyon bawat araw, 28-day archive ([bagong bersiyon](https://tropo.f5len.org/?p=504)) | Tiyakin na ito'y nagsisilbi ng isang modelo na maraming mababang antas |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf) | 60 antas ng modelo ng ECMWF, mula sa lupa hanggang 3000 m | Hindi bababa sa dN/dz bawat kolum: isinasagawa sa ilalim ng −157 N/km, super-refraction sa ilalim ng −78.7 N/km; base ng patong ng bitag | klimatolohiya: Plain ng Padana ang lugar sa Europa na may mas maraming tubo sa buong taon; Mediteraneo sa tag - araw na malapit sa 100%, sa taglamig na wala pang 15%; Italya na mahigit 30% kung mga gabi ng tag - araw |
| [Indermuehle at Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861) | GFS 0.25°, 21 pressure level + field sa 2 m | M = N + 0.157 z; minimum na dM/dz na mababa sa 3 km, base at tuktok ng duct, lalim ng M deficit; wala pang 40 m ang itinapon | Pinatunayan sa loob ng 7 taon sa 25 channel mula 88 MHz hanggang 2,68 GHz; mahusay na kapasidad hanggang mga 3 araw patiuna (AUC humigit - kumulang 0,74), pagkatapos ay bumababa; ang GFS ay mas mabuti kaysa ERA5 |
| [Hämäläinen et al., IMF 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf) | Harmonie-AROME, 750 m o 2.5 km, 65 o 90 antas | Ang dM/dh < 0 sa model profile, kung ihahambing sa isang 4 hanggang 59 m beam | ay mas patayo kaysa pahalang na lunas; walang modelo ang nakakakita sa mga duct sa ilalim ng 15 m; ang halumigmig ang nangingibabaw sa temperatura ng spiral |

Ikatlong baguhang serbisyo sa radyo na aktibo na: [ToPoCst.EU](https://tropocast.eu/)of Artur SQ3A, para sa 2 m at 70 cm sa Europa. Mula sa panghuhulang pahina ([tropocast.eu/forescast](https://tropocast.eu/forecast)):

- Data: DWD ICON-EU (lampas sa 0.0625°, mga 7 km), default source; alternatibong Canadian GDPS (0.15°), hindi gaanong detalyado.
- Pamamaraan: N at M Ayon sa ITU-R P.453 mula sa profile sa pagitan ng patong na malapit sa lupa at 850 hPa; ang pagkilala sa mga patong, ang pagsasama ng mga duct sa lupa at mataas at ang mga pasukan ng mga kulay ay isang wastong kalibrasyon ng may akda, hindi inilalathala.
- Products: "kumpleto" view 0–3000 m, mas hiwalay na mga tanawin "sa lupa" at "mataas" (para sa ngayon lamang sa mga mapa upang maisama); iskor 0–100 sa siyam na klase (sa ilalim ng 38 background, 38–52 gilid, 53–58 mahina, 59–65 katamtaman, 66–72 mabuti, 73–80 napakabuti, 81–88 mataas, 89–95 napakalakas, 96–100 napakalakas); paghula hanggang + h72 sa loob ng isang oras, na may arkibo.
- Mga lugar: Europa, Poland, United Kingdom, Alemanya, Pransiya, Espanya, Italya, Czech Republic, Croatia; interface din sa Italyano.
- Reuse: Kasama sa mga mapa ang isang API na walang susing whime; mayroon ding dokumentadong API (OpenAPI at JONS schema). Ang lisensiya ay hindi ipinapakita: bago gamitin ito bilang ikatlong source na katabi ng Hepburn at F5LEN dapat mong itanong sa may akda.

Pagkakaiba mula sa aming proyekto: Ang TROPOCAST ay humihinto sa 850 hPA (mga 1.5 km), habang ang mga nakaangat na duct ay dumarating sa 2000 m at lampas pa ( LA0BY case ng 22/11/2018), at hindi nagbibigay ng indise para sa banda o ruta. Ang mga klase 0–100 ay maihahambing sa ating indise 0–10 sa parehong mga araw.

TROPOCAST istruktura ng imahe (mula sa link na inilaan ng may akda ng programa): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, kung saan ang n ang hakbang ng paghula. Ipinagbabawal ng site na ito ang awtomatikong pag - iwas sa mga robot, kaya hindi ko rin mabasa ang larawan; dahil din dito, bago ipakita ang mga mapa na ito sa WOMENTROVHF ay nagsisilbi sa pahintulot ng awtor.

Notebook: [DJ5AR, kategorya](https://dj5ar.de/?cat=174)(Magonza, 21 artikulo 2017–2023, 2 m–13 cm). Mga kasong inilarawan: isinagawa ilang linggo mula sa Irlanda (23 Mayo–2 Hunyo 2023), FT8 to Belarus (Setyembre 2020), Italya–Alemanya sa ibabaw ng Alps at Adriatico (Agosto 2017). Kilalanin ang kaso ng "napakataas" na duct (tignan hanggang 1000 m) na hindi mo magagamit mula sa lupa: isa pang pagpapatunay na kinakailangan upang ihambing ang bahagi ng padaluyan sa bahagi ng mga istasyon. Gumagamit ito ng mga radiophares bilang isang aperture measure (hanggang 1028 km) at mga patayong profile sa Paris, Bordeaux at DWD.

Bilang buod, ang karaniwang resipi: patayong profile → N at M kada antas → layer na may dM/dh negative → base, tuktok, kapal at puwersa ng duct → index. Ang "lihim" na bahagi ng Hepburn ay ang empirical calibration sa mga tunay na kaganapan, na maaari nating muling gawin sa mga batik.

## 6. Kapaki - pakinabang na mga Anyo Para sa programa

Bukod sa N at M (basikong seksyon ng Physics), ginagamit din ang mga ito sa paglipat mula patayo papunta sa puntos na bilang at pagkatapos ay sa landas. Ang mga pormula ng ITU ay muling binuo ng isang PDF na nabasa sa isang bahagi nang masama; noong 22 Nobyembre ay nirepaso sa mga opisyal na teksto ([ITU-R P.452-18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453-14, P.526-16, P.372-18) at naituwid sa kodigo: palaging ng troposcatter 190 (hindi 190,1) at ang pagsipsip ng mga gas na may ρ = 3 g/m3; α − −3, (− 55°); hangganan ng tro 2° 2°q logs = spherical) ay nasa ilalim ng lupa (− 2°N 2° = 1. Rumore P.372-18: table 1 at galactic magkatumbas; ang mga artipisyal na mga kurba ng ingay ay umaabot sa 250 MHz at ang galactic hanggang sa mga 100 MHz, sa ibabaw ng programa ay naggagawad ng parehong mga batas.

1. Kasunod ng dalawang antas ng modelo (h sa km, M sa unit M):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. Conduit na lakas at kapal: kapal d = itaas − base (para sa duct ng lupa, base = lupa); lakas bilang M kakulangan sa pagitan ng base at ibabaw.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3. Lupa paikot na katumbas ng factor (P.452-18, eq. 5 at 6a), na may ΔN = average N d drop sa unang km, positibo, sa N/km:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

Sa ΔN = 40 mayroon kang humigit-kumulang 4/3 k (standard environment); na may ΔN na lumalapit sa 157 k ang may tendensiyang maging infinity, samakatuwid nga, ang radyong Earth ay nagiging patag: ito ang simula ng duct.

4. Radyo abot - tanaw ng isang mataas na antena h metro, na may paikot na katumbas ng\_e sa km:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5. Ang probability of duct para sa latitud (P.452-18, eq. 2), persentahe ng oras; φ = latitud ng gitna ng ruta, μ1 at μ4 ay nagsasaalang alang kung gaano kalayo ito sa pamamagitan ng dagat:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

Sa 41° Ang N, lahat sa pamamagitan ng dagat (μ1 = μ4 = 1), ay nagbibigay ng halos 11% ng panahon: ay ang climatological base, kapaki-pakinabang bilang reperensiyang halaga kung saan ang inaasahang index ay naiiba.

6. Kawalan para sa troposcatter (P.452-18, eq. 45; f sa MHz, d sa km, θ ang anggulo ng landas sa marad, N0 ground reactivity sa gitna ng ruta):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

Ang terminong −0,15 · N0 ay nagpapakita kung bakit ang basang hangin ay tumutulong din nang walang duct: 50 ekstrang N units ay 7.5 dB mas mababa sa pagkawala. ANG liveTROPOVHF ay gumagamit na ng G3YGF na bersiyon ng nakakalat na kawalan.

7. Kawalan sa duct (P.452-18, eq. 46–51): tiyak na pagkawala ng kompleksidad at isang attenuasyon para sa single milliradiant, na lumalaki sa kubikong ugat ng frequency (f in GHz, a\_e sa km):

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. Mula sa Bean at Dutton (1966) at Kerr (1951)

Ang dalawang klasikong mga teksto ay nagbibigay ng tatlong mga bagay na kailangan kaagad sa indise: Ang mga hagdan sa gilid ay nagreresulta na sa parehong mga antas ng presyon ng GFS, kung ano ang halaga sa dB isang pagbabago ng reaktibidad, at kung anong mga kalagayan ng panahon ang lumilikha o sumisira sa mga dukto. *Radio Meteorology*, NBS Monography 92.

1. Profile classification para sa mga layer ng presyon (Bean at Dutton, tab. 6.5, p. 248; −dN/dh sa N/km). Dinisenyo para sa mga radio wavelength, ito ay kumakapit habang ito ay nasa huwarang antas:

| Daan | Mga bahagi tungkol sa | Sub-refraction | Hindi Pinamumunuan | Super-refraction |
| 1000–850 hPa | 0–1,46 km | −dN/dh < 0 | mula 20 hanggang 60 | mahigit 100 |
| 850–700 hPA | 1,46–3,01 km | −dN/dh < 0 | mula 20 hanggang 50 | mahigit na 80 |
| 700–600 hPa | 3,01–4,20 km | −dN/dh < 0 | mula 15 hanggang 40 | mahigit 70 |
| 600–500 | 4,20–5.57 km | −dN/dh < 0 | 10 hanggang 30 | mahigit 50 |

2. Gaano kalaki ang halaga sa dB (Bean at Dutton, cap. 6, p. 233–258), mga sukat mula 70 hanggang 180 MHz sa mga ruta sa ibayo ng abot-tanaw:

- Ang anumang patong (sa lupa o sa taas) kung ihahambing sa "smooth" profile ay nagpapataas ng signal na 10–25 dB sa lahat ng mga persentahe; ang mas malaking pakinabang ay nagmumula sa mataas na nakahilig na mga patong, malamang dahil sa nakatutok na epekto (course ng 200 km).
- Ang positibong reaksiyon Ang Ns at ang pagbaba sa unang km ΔN ay nauugnay sa buwanang median signal na may coficial sa pagitan ng 0.4 at 0,95, median 0,7 (20 ruta mula 130 hanggang 446 km, halos 100 MHz).
- Taunang siklo: 0.18 dB bawat yunit N; pang-araw-araw na siklo mula 0.2 hanggang 1.1 dB bawat yunit N, pinakamalaki sa mga ruta ng 175–200 km at taglamig.
- Ang compound parameter ng Misme ay tumatantiya sa field variety bilang 0.5 dB/(N/km) · (equivalent judge − 40) pati na ang isang thermal configuration term; ang parameter ng viral D (area sa pagitan ng tunay na profile at ang adiabatic one, 0–3 km) ay bumubuti lamang kumpara sa payak na ΔN. Konklusyon ng mga may akda: Ang ΔN ay nananatiling pinakamahusay na parametro para sa pagiging simple.

3. Reference profile at anamaly (Bean at Dutton, § 3.8 at 3.9). Ang eksponensiyal na atmospera ng reference CRPL ay nagbibigay ng "normal" na profile na nagsisimula sa Ns; ang yunit na A ay sumusukat kung gaano kalaki ang pagkakaiba ng tunay na profile sa bawat bahagi. Ito ang natural na paraan upang baguhin ang GFS profile tungo sa isang aomalya na ilagay sa mapa:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

Kasama ang Ns = 313 mayroon kang ΔN tungkol sa −42 N/km, iyan ang katumbas na radius 4/3. Ang −157 N/km bridge proclures ay nakuha rin dito ng batas Snell para sa isang pahalang na paikot (eq. 4.23), halos independiyente mula sa taas ng antena.

4. Saan at kapag ipinanganak ang mga padaluyan (Bean at Dutton § 4.4; Kerr cap. 3):

| Proseso | Kalagayan | Epekto |
| Babala | Ang mainit at tuyong hangin mula sa lupa na umaagos sa mas malamig na dagat (karaniwan nang sa manggas ng tag - araw pagkatapos ng mga araw ng mabuting panahon); ay pangunahin nang dahilan ng kakulangan ng halumigmig | Matinding padaluyan ng tubig sa dagat |
| Hindi Nakapagpapatubig | Mapayapang langit, mahinang hangin; mabababang ulap o halumigmig ang humahadlang dito | ay humahantong sa lupa na ipinanganganak sa paglubog ng araw, lumalaki sa gabi at mabilis na naglalaho pagsikat ng araw, lalo na sa loob |
| Ilalim | hangin na bumababa mula sa anticyclone, mainit at tuyo, sa ibabaw ng sariwa at mahalumigmig na hangin; madalas na pagbaha sa mga bundok at timog-Silangang sektor ng mga anticyclones | super-refractive layer o high duct; halos palaging mas mataas sa pamantayan sa baligtad |
| Dagat at lupa | baybayin, sirkulasyon na ilang daang metro | Mga tubo sa baybayin |
| Laban sa Agos | Pinaghalo: lupang pinainit ng araw sa pagitan ng kalagitnaan ng umaga at hapon, hangin sa lupa sa ibabaw ng ilang m/s sa baku-bakong kalupaan; ulap sa Gabi | Ang probabilidad ng padaluyan ay halos wala |

5. Dalawang bagong sangkap na dapat subukin. Ang patayong mosyon: ang pag-akyat ng mga daloy sa mga sonang konstruksyon ay nag-aalis at nagbura ng mga patong, ang refluence ay nagpapalakas ng signal (Moler and Holden: sa katamtaman ay halos 2.5 dB na higit sa pamamagitan ng hanging pataas, sa dagat sa band L at X). Ang open-Meteo ay nagbibigay na ng patayong bilis sa mga antas ng presyon. At ang kalakaran ng presyon: sa 140 km kurso sa 186 MHz bumaba ang signal nang bumaba ang presyon sa transmitter at sa gitna ng landas at bumalik sa mataas at walang evancence nang ito ay tumaas, na may 2 lamang (Bean at Dutton, igos. 6.9, p. 241).

6. Mula kay Kerr. Bukod sa pormulasyon ng sukdulang wavelength (base Physics section), nagbibigay si Kerr ng tinatayang pormula para sa M mula sa tanging temperatura, singaw at altitud (§ 3.9, na may lumang 79 at 4800 na mga konstante na noo'y pinapalitan ng 77,6 at 4810) at napansin na sa pagbaligtad ng hanging natabas ay maaaring lumikha ng mga alon na umiindayog sa taas ang dukto, posibleng sanhi ng malalim na pagtakas (§ 3.26). Ang Italyanong pinagkunan ay nag-ulat nina Bean at Dutton at maghanap: L. Bonavoglia (Disyembre 1958), "Korrelasyon sa pagitan ng mga pangyayaring meteorolohikal at proparasyon...".

## 8. Mula sa amateur na mga pagtatanghal sa radyo at teknikal na mga ulat (ikalawang serye)

Pitong dokumentong inilaan ng awtor ng programa. Tatlo ang tuwirang nagsisilbi sa indise: W3SZ para sa troposcatter "floor", LA0BY para sa mga kasong hindi inihula ni Hepburn, Hepburn (pagsalin ng LU7MC) para sa klasipikasyon ng lagay ng panahon ng mga dukto.

1. Troposcatter: ang hudyat na laging umiiral. R. Rehr W3SZ, "Tropospheric dinadaglat (Troposcatter) Propagasyon para sa VHF, UHF, Microwave and Frequencies", Pack Rats Conference 2014; J. R. Duffey KK6MC, "The Troposcatter Propagation Mode and How to Prophecy Coverage on the VHF/UHF Bands", CSVHF 2017.

- Kapaki-pakinabang mula sa mga 100 hanggang 700 km, mula 144 MHz hanggang 10 GHz; ang pagkawala ay lumalaki ng 30 troso f na may dalas.
- Higit sa lahat, ito ang bumubuo sa simulang anggulo patungo sa abot-tanaw: ang bawat ekstrang digri ay nagkakahalaga ng halos 9–12 dB ( M1CNK, Collins method).
- Sa tag - araw, ang signal ay sa katamtamang 10 dB na mas mataas kaysa sa taglamig; sa kalagitnaan ng hapon ay mga 5 dB na mas mababa kaysa sa madaling araw o sa gabi. Ang KK6MC ay nagpapakita ng kabuuang valarity ng 13–15 dB na may panahon, panahon at panahon.
- Yeh method, ang pinakamadaling iprograma (f sa MHz, d sa milya, θ ang anggulo ng pagsasabog sa digri, α heometrikong katamtaman ng mga butas ng dalawang bungkos):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Paghahambing ng Larsen (1968) sa 15 tunay na koneksiyon: average square error 5,1 dB para kay Collins, 7,4 para sa Yeh, 8.1 para sa NBS at CCIR, 16,8 para sa Rider. Ang lahat ng mga pamamaraan ay bumubuti kung ang Ns ay mananatiling matatag (Yeh drop sa 5.4 dB): dahil ang pagkalat ng refraction ng araw ay kaunti lamang ang halaga, kaya ang index ng tropo ay dapat tumingin sa mga layer, hindi lamang sa Ns. Ang mesa ng 15 mga koneksiyon, na may nasukat na kawalan (kabilang ang Feldberg–Monte Baldo, 322 km sa 957 MHz, 223,1 dB; Cyprus–Israel, 319 km sa 77,5 MHz, 163,33 dB), ay isang mahusay na test bench para sa ating kalkulasyon.
- Ang KK6MC ay nagbibigay ng kawalan na nagagawa ng isang karaniwang istasyon na lumampas sa 2 m at ang katumbas na distansiya, mula 184 dB (FT-817 at 3 elemento sa SSB, mga 130 km) hanggang 215 dB (mahahabang bung sa SSB, mga 430 km) at 236 dB (kW at yagi matd, mga 640 km). Ito ang paraan upang isalin ang index dB sa milyang kinita.
- Handang kodigo: W3SZ Yeh at Collins program (nitehawk.com/w3sz/YehCollins.zip) at Bob Atkins KA1GT calculator (bobatkins.com/radio/troposcatter.html).

Isa pang source sa dinadaglat: I. Roberts ZS6BTE, "Tropospheric dinadaglat Propagation", *Radio ZS*, Disyembre 1984, na may nota ng 2013 ([qsl.net/zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). Pangunahin na Kapaki - pakinabang na heometriya: ang anggulo ng pagsasabog θ = θ0 −1 −2 θ, na may θ0 = d/R, θ1 = (h1 − h−_ostacolo)/d1 + d1/2 R at pareho para sa isa pang istasyon, R = 8446 km (4/3 ng Lupa paikot). Ito ang piraso na ginagamit upang kalkulahin ang mga anggulo mula sa profile ng lupa sa troposcatter na "floor". Ang iba pang datos: ang bawat anggulo sa higit pang halaga na mga 10 dB; ang paglipas mula sa Ns 280 hanggang 300 ay nagpapabawas sa pagkawala ng halos 4 dB; ang nakakalat na dami ay nasa pagitan ng 300 at 2000 m para sa 150 km, sa pagitan ng 600 at 3000 m para sa 300 km; Ang oras sa nakakalat na volume ay nagpapataas ng signal ng mga 20 dB. Ang numerong halimbawa ng artikulo ay hindi dapat ibalik sa pamamagitan ng ibinigay na pormula (ang termino ng malayang espasyo sa 900 MHz at 872 km ay humigit-kumulang 150 dB, kaya't ang mga pormula ay dapat suriin sa X2. W3SZ

2. Mga uri ng padaluyan (W. R. Hepburn, "Tropospheric Propagation Modes", dxinfocentre.com/propagation/tr-modes.htm, saling Kastila ng LU7MC). Kumpletuhin ang mesa ng Bean at Dutton ng kapaki-pakinabang na mga kaso sa Italya:

| Uri | Kailan | Pansinin ang indise |
| Irradisyon | Tahimik at mapayapang mga gabi sa lupa | sundan ang lupa, agad na naglalaho pagsapit ng bukang - liwayway |
| Mataas na presyon (subsidence) | anticyclone | ay maaaring tumagal nang maghapon; sa gabi ang padaluyan mula sa radyasyon sa ibaba ay maaaring humadlang sa malayong mga hudyat, kaya ang araw ay maaaring bumuti |
| Harap | sa harap ng isang mainit na harapan, sa likod ng isang malamig na harapan, sa hilaga ng isang di - gumagalaw na harapan | ay maikli; ang masamang lagay ng panahon ay nakababalisa sa kaniya |
| Bundok pababa | föhn, bora: hanging bumababa, pag-init at pagpapatuyo | upang subukan ang bora sa Adriatico at föhn sa timog ng Alps |
| Babala | Ang mainit at tuyong hangin sa malamig at basang lupa, halimbawa pagkatapos ng ulan | inakay sa lupa |
| Libis | Humihinto sa libis ang mainit na hangin sa malamig na hangin | ay maaaring manatili sa araw |
| Marino | Mainit at tuyong hangin sa mas malamig na tubig; dumating sa baybayin taglay ang hangin | rurok sa hapon; tagsibol at maagang tag - araw ang pinakamagandang panahon |

Ang mga daluyan ng pagsingaw sa dagat (0–30 m) ay nagsisilbi lamang sa mga 3 GHz at hindi pumapasok sa hinterland: para sa 144 at 412 MHz maaari mong ipagwalang bahala. Ang matataas na bundok ay isang hadlang; ang mga disyerto ay napakatuyo.

3. Ang mga kasong hindi hinulaan ni Hepburn (S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", Nordic VUSHF 2019, mula JO59IX hanggang 500 m ng altitud):

- 22 Nobyembre 2018: 70 cm hanggang UA6 (mga 2600 km) sa loob ng 6–8 oras. Ipinahiwatig lamang ni Hepburn ang mga Estado ng Baltic at Poland. Ang padaluyan ay nakita sa mga mapa ng temperatura sa 900 at 1500 m at nagtapos sa ibabaw lamang ng 2000 m; mas mataas ito sa panig ng Norway, at LA0BY lamang sa burol ang pumasok. Sa lupa ang panahon ay parang isang trope: malamig at maulap sa tuktok.
- 11 Pebrero 2015: 2 m at 70 cm patungong 9A at S5 (mga 1500 km) sa pamamagitan ng Alps, hindi rin ito nakita ni Hepburn.
- 15 Oktubre 2011: 2 m na may EA1 (2135 km) sa mataas na duct, sa tatlong kahabaan ng dagat at tatlong lupa.
- Iba pang mga pangyayari: 6–11 Nobyembre 2003 sa silangan (2050 km), 10 Disyembre 2004 sa Pransiya.
- Mga direksiyon sa bukid: ulap sa kapatagan, libreng taluktok, kaunting hangin, basang lupa. Normal na temperatura −6,5 °C/km; sa isang tunay na kaso ( VK3KAQ) +7 °C sa 20 m.

Ang konklusyon ng awtor: Hindi sapat ang Hepburn at F5LEN, nagsisilbi sila sa mga profile sa altitud sa ruta. Para sa atin: Ang mga petsang ito ay mga kaso ng beripikasyon na irereporma sa makasaysayang impormasyon ng mga modelo, at ang indise sa landas ay dapat na ihambing ang kota ng duct sa takdang halaga ng mga istasyon.

4. karanasang Italyano (G. IV3GBO, "Types of VHF propagation SSB/CW", 2017): isinagawa sa pagitan ng 450 at 3000 km, na may mga patong sa pagitan ng 450 at 3000 m; ang ulap ay lumalawig bilang isang himaton; mga pagbubukas sa pamamagitan ng dagat sa Adriatiko patungo sa Puglia, Albania at Gresya. Kumalat mula 100 hanggang 500 km.

5. Introductions (P. Wilton M1CNK, "An Introduction to VHF/UHF Propagation", 2013): Ang 70 cm ay kadalasang mas mabuti sa 2 m; na may 10 m pole sa 144 MHz ang simulang anggulo ay mga 3°, na may 20 m humigit-kumulang 1.5°.

6. ulat ng NASA (E. Hirschmann, "Tropospheric and Ionosphere effects on Radio Frequency (VHF-SHF) Communication", Goddard X-731-67-89, 1967): ito ay may kinalaman sa mga pagkakamali sa layo sa mga satelayt, hindi sa DX. Nagbabalik ito ng parehong pormulang N at eksponential profile (ekonomiya sa Florida: Ns 370, ΔN −55, constant 0.161/km) at nagpapadala ng dalawang NBS sources upang maghanap: Bean at Thayer, "CRPL Exponential Reference Atmospheric", Monograph 4 (1959), at Bean, Horn and Ozanich, "Climatic Charts and Data of Radio Refractive Index for the United States and the World," (19 2260).

## 9. Mula sa VHF/UHF DX Book, Kabanata 2

I. White G3SEK (itinatag ni), * The VHF/UHF DX Book*, DIR Publishing 1995 (ISBN 0 9520468 0 6; digital replika 2017), kabanata 2 "VHF/UHF Propagation", p. 2-13, 2-22–2-29 at 4-11. Ito ang pinakamalapit na teksto sa karanasang radio amateur kabilang ang mga nakolekta; pagtibayin ang mga pintuan na ginagamit na at magdagdag ng mga bagong patakarang synoptiko.

Bilang

- Ang parehong pormula ng N (77.6 P/T + 3.73·10^5 e/T2); halimbawa: 1000 hPa, 7 °C, at = 10 hPa pinsala N = 324. Sa isang mahusay na atmospera, halimbawa pagkatapos ng isang pagkulog at pagkulog, ang N ay halos linear ng mga 39 yunit/km sa unang 2–3 km.
- Ang mga gradient sa pagitan ng 0 at −79 N/km ay normal, sa ibaba −79 ay may super-refraction, mahigit 0 sub-refraction. Iginigiit ng aklat na ang mahabang DX ay hindi nagmula sa payak na super-refraction, na dapat manatili sa buong landas, kundi mula sa mga duct.
- Kaunting kapal ng duct, medyo matalino kaysa kina Kerr at Hepburn:

| Band | Pinakamaliit na kapal |
| 50 MHz |  400 m  |
| 70 MHz |  300 m  |
| 144 MHz |  200 m  |
| 432 MHz |  100 m  |
| 1.3 GHz |  50 m  |

- Ang ratio ay hindi proporsiyonal sa wavelength (mga 65 muskulo sa 50 MHz, 220 λ sa 1.3 GHz) dahil ang mga gilid ng padaluyan ay "wala". Sa UK ang mga tubo ay madalas sa 144 MHz ngunit halos hindi kailanman sa 70 MHz o sa ibaba, at kadalasang ang 432 MHz ay malakas kapag ang 144 ay mahina.
- Sa pipeline ang signal ay bumababa bilang 1/d sa halip na 1/d2: hanggang 1000 km ang attenuation ay maaaring maging katulad ng sa free space.
- Capacity ng isang minimum na istasyon sa 2 m (100 W, 15 dBi yagi, 2 dB ingay): 217 dB, iyan ay humigit-kumulang 400–500 km sa SSB sa average na kondisyon; sa CW mga 20 dB sa higit pa (p. 4-7). Makipagkasundo sa talaan ng KK6MC.
- Nakalat: ang pagkawala ay tumataas ng mga 15 dB sa pamamagitan ng triple ang dalas at 10 dB para sa bawat antas ng nakakalat na anggulo ( Yeh producation; sa teksto na nakuha ang constant ay hindi mabasa, kaya ito ay dapat kunin mula sa bersyon ng W3SZ). Ang "up" na ikinakalat mula sa mga selulang convective ay karaniwang sumasaklaw hanggang sa mga 500 km, 1000 km na may mabubuting istasyon. Pagkatapos ng malakas na pag-ulan at pinalawig na kondisyon ay kakaunti sa lahat ng banda.

Mga uri ng duct

- Sa lupa: sa lupa ay lubhang lumalaganap ang mga ito sapagkat sinisipsip at kumakalat ang lupa; sa kalmadong dagat ay dumarating ang malayo (Scilly–Canarie, 2500 km, halos minsan sa isang taon sa 2 m). Ang mga daluyan ng pagsingaw ay kadalasang mga 15 m lamang ang taas at nagsisilbi ng mga microwave.
- Mataas: sa pagitan ng 450 at 2000 m, dahil sa mas mahabang DX sa lupa; ang signal na "high" sa itaas ng mga istasyon sa ibaba. Ang mga "lose" duct ay nagbibigay ng hindi gaanong malalakas na signal ngunit pumapayag din sa mga intermediate distance station.
- Ang mga padaluyan ay may eksaktong mga direksiyon at maaaring tumagal ng mga oras o mga araw, salungat sa pabagu - bagong E.

Bagong mga Alituntunin ng Synoptic Para sa Indise

- Panahon ng anticyclone: ang hangin ay bumababa sa gitna ng ilang metro sa bawat oras at ang pagbaligtad ay ibinababa habang ang mataas na presyon ay nagkakaroon; ang mas malalaking butas ay dumarating pagkatapos ng ilang araw ng propaksiyon ay mabuti na. Ang isang short-term anticyclone ay hindi sapat.
- Mataas na gilid ng presyon: ang patong ay simboryo at sa mga gilid ay mas mababa, kaya ang mga istasyon ay bumubuti.
- Tagubilin: Napansin na ang mas mahabang mga ruta ay tumatakbo sa mga isobare o sa pamamagitan ng mga dorsal (Flavell G3LTP, 1984), ngunit may mahuhusay na mga koneksiyon din sa radial na direksiyon mula sa gitna. Halimbawa ng 29–30 Agosto 1987: mula sa Inglatera ang pagbubukas ay umikot sa 180° sa loob ng dalawang araw (Espanya, Pransiya, Italya, Austria, patungong Norway) kasunod ng paglipat ng anti-siklone.
- Laban sa: makapal na isobare (malakas na hangin), ulap na stratocumuliform sa gilid ng patong (limit ang pinakamataas na frequency).
- Harap: ang malamig na harapan ay nagtutulak sa mainit na hangin at lumilikha ng pagbaligtad sa unahan; ang pinakamabuting direksiyon ay sa unahan habang dumaraan, subalit ito ay bahagya lamang.
- Season: Sa UK, mas madalas ang mga anticyclones sa pagtatapos ng tag-init at maagang taglagas.
- Mga palatandaan: Ang usok na nakulong sa isang madilim na linya sa itaas ng abot - tanaw; na may mataas na presyon, mga layercumule na ang pundasyon ay mababa sa 1000 m ay nagpapahiwatig ng isang duct sa lupa, na nasa itaas ng 1000 m ang isang mataas na daluyan.

Mga reperensiyang sinipi sa paghahanap: R. G. Flavel G3LTP, "Mga Pag-aaral ng Malawakang Anticyclonic Propagation Event...", *Radio Communication*, Pebrero 1984, p. 128; J. Gannaway G3YGF, "Tropospheric dinet Propagation", *Radio Communication*, Setyembre 1981, p. 710; P. W. Sollom G3BGL, "A Slutter Flus Flus Flugation" W6ZGN

## 10. Isang napatunayang amateur na modelo sa radyo: Biancomano at Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, "Practical Tropospheric Model for VHF/UHF Propagation Prediction", *National Weather Digest* 22:2, Hunyo 1998, p. 15–24, libre sa [PDF mula sa National Weather Association](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf).

- Data: mga radiosundation ng 00 at 12 UTC ng 47 istasyon sa silangang Estados Unidos, 1995–1997; ground analysis hanggang 700 hPa (sa simula hanggang 400), sa profile ng N, nang walang M. Excluding baybayin, mga isla at bundok.
- Para sa bawat surbey: ang pagkakaroon at pagbilang ng duct, ang sukdulang wavelength na maaaring umakay (kadalasang halos 100 ulit ng wavelength), ang sukdulang pagbaba ng potensiyal na temperatura sa pagitan ng kalapit na mga antas at sa pagitan ng isang antas at ng ikalawa sa itaas (para sa mas makapal na mga suson). Hindi ginagamit ang mga ground duct.
- Unang tuntunin, pagtitiyaga: katatagan indices (lifted index, K, mga kabuuang-administratibo, SWEAT) ay nananatili sa loob ng 3 yunit mula sa survey 12 oras bago nito, at ang antas ng presyon kung saan ang potensiyal na temperatura ay higit na nananatiling halos pareho. Kung gayon: Ang pagsulong ay humuhula sa susunod na 12 oras.
- Ikalawang tuntunin, dynamic controls: limitahan ang layer na hindi tumataas, CAPE, iba't ibang kapal hanggang 500 hPa sa tatlong survey, pagkakaiba-iba ng dalisdis sa loob ng hangganan.
- Buntong pasukan: isang padaluyan na may kakayahang maglulan ng 100 cm o higit pa ay nagpapahiwatig ng pagbubukas sa 432 MHz, halos tiyak sa 222 at kadalasang kahit sa 144.
- Space filter: mga istasyon sa isang hexagonal grid na mga 400–500 km; ang paghula ay halaga lamang kung hindi bababa sa 5 contigue stations ay positibo (kung makawala ka ng isa). Sa 5 istasyon ang probabilidad ng isang huwad na random positive ay nagiging bale wala.
- Veripikasyon laban sa mga pangyayari ng kolum na "The World Above 50 MHz" ng QST: noong 1996, 48 pangyayari ang nagplano sa 67 (71%); 12 sa 15 pangunahing pangyayari. Humigit-kumulang 70% sa 432 MHz at mga 60% sa 144 MHz sa loob ng 12 oras; ito ay nagbibigay ng halos doble ng aktuwal na mga pangyayari; 9 na pagkakamali sa 19 ay dumating nang huli, dahil maraming pagpapabuti ang nabubuo sa loob ng wala pang 12 oras.
- Karagdagan pa sa mga istasyon sa hilaga: ang umiikot na pag - akyat sa direksiyon ng hangin (mainit na pasimula) bilang karagdagang kondisyon.

Mula sa kumpletong PDF (isinalin ng may akda ng programa) ang tatlo pang detalye. Ang kanilang pamumunong "ika-7 = 100 beses na wavelength" ay nagbibigay ng 600 m para sa 6 m, 200 m para sa 2 m, 125 m para sa 1,25 m at 70 m para sa 70 cm (fig. 3): dahil ang 70 cm ay hindi gaanong malala kaysa kay Kerr at Hepburn (mga 89 m), para sa 2 m ito ay kasabay ng VHF/UHF DX Book. Ang hangin na umiikot sa orasan ay tumataas, ayon sa mga awtor, lalo na dahil sa nagdadala ito ng mas mainit na hangin sa taas, na siyang nagpapangyari sa pagbagsak ng N na lumaki na may presyon; ang halumigmig na dala nito ay waring hindi bumababa. Ang apendiks ay nakakakuha ng dN/dh sa saradong anyo para sa isang hindi nagbabagong mainit na atmospera na may mabilis na pagtaas ng init at halumigmig sa isang ibinigay na kota: kapaki-pakinabang bilang isang test case para sa ating code. Ang bakas ng dalisdis ng potensiyal na temperatura ay mula kay K. H. Jehn, "Ang paggamit ng potensiyal na refractive index sa synoptic scale radio meteorology", *J. Meteorology* 17, 1960, p. 264.

Kung ano ang dadalhin para sa aming indise: (1) isang kataga ng pagtitiyaga: ihambing ang profile sa 12 oras bago nito, na sa oras na paghula ay mayroon tayong libre; (2) ang pansala sa kalawakan: isang punto lamang ang mahalaga kung ang pagpapabuti ay sumasaklaw sa isang saklaw ng ilang daang km, na nagbabawas ng maling alarma; (3) katatagang indiks, na kinakalkula mula sa temperatura at hamog hanggang 850, 700 at 500 hPa (Open-Meteo ay nagbibigay na ng naangat na index at CAPE); (4) hangin na umiikot sa pagitan ng taas 1000 at 850 ha; (5) ang kanilang pamamaraan ay katulad ng nais natin sa programa.

Isa pang source sa parehong pakete, ng kaunting gamit para sa amin: P. K. Rastogi, "Stratosphere and Troposphere (S-T) Studies in Millstone Hill", NASA 1984 (dokumentaryo 19840019069): 440 MHz radar na sumusukat sa pagyanig ng malinaw na hangin sa pagitan ng 4 at 25 km, na may manipis na mga layer na nauugnay sa mga along gravity at wind cutting. Pinatutunayan nito na ang pagyanig na nagiging sanhi ng pagkalat ay nakasalansan, subalit ito'y lubhang nakababahala sa mga tubo.

## 11. Ikatlong round ng mga mapagkukunan (20 Nobyembre)

Mataas na mga kasong historikal. J. Macoun OK1VR, 2 m mula sa tuktok ng Snıžka (1602 mi kuw), taglagas 1958, artikulo ng 1959 na isinalin noong 2006 ([ok2kw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)). Noong 27–28 Oktubre 1958 una Czechoslovakia–England koneksyon sa 145 MHz ( G5YV, 1220 km) at marami pa sa pagitan ng 1100 at 1250 km, na may matatag na mga signal pagkatapos ng 23. Ipinalalagay ito ng awtor sa dalawang superimposed inversions, ang itaas ay mga 2200 m, na ang hudyat ay bumabalik sa lupa lamang kung saan ang ibabang suson ay natutunaw; ang kalagayan ay naiibigan ng isang mabagal na anticyclone mula sa Atlantiko tungo sa kontinente. Mula sa bahay (Prague) ang sukdulan nito ay 530 km: tanging ang mga istasyon na nasa taas ang pumapasok sa padaluyan. Isa pa itong kaso ng beripikasyon (tulad ng LA0BY 2018) at nagpapatunay na ang indise ay dapat na maghanap ng pinangangasiwaang higit sa 2000 m at ihambing ito sa parte ng istasyon.

Mga makasaysayang aklatan NBS (kapaki - pakinabang na hanapin ang mga klasikong akda, hindi ang mga pormula):

- W. Nupen, *Bibliography on Tropospheric Propagation of Radio Weals*, NBS Technical Note 304, 1965, 356 pahina, may indise para sa mga paksa (binabaybay ding "condition proclaimation") at sa pamamagitan ng bansa. Ang mga sangguniang Italyano ay nag - ulat: L. Sacco, "Ang koneksiyon ng radyo sa atmosperikong super-refraction na rehimen", *High Frequency* 24(6), Disyembre 1955 (Italian research: super-refraction sa ilalim ng 500 m madalas na araw at gabi); S. Selegna e Minorca da, *Annali di Geofisica* 8(1), 1955; J. M. Clara e A. Antinori, prova tra 23°N.00 a, *Anna di-H90le), kasunod ng araw-araw-araw-araw-araw na tag-araw na tag-araw na pag-araw na tag-araw na pag-oras, Fleclec.
- A. L. Abbott, *Bibliograpiya ng Tropospheric Radio Wave na kumakalat*, NBS, materyal hanggang Enero 1960, halos 670 mga bagay na inorder bawat taon: kapaki-pakinabang para sa bahaging troposcatter.

Disclosure and comments: F. W. Odenthal DK5EQ, "Ausbreitung 2m – Eine Einführung" (31/05/2024): halos optikal hanggang 200 km, tropo sa bawat duct o refraction sa kabila pa roon, na may mataas na presyon at sa tagsibol at taglagas; binabanggit ang VHF pahina ng DM2HB at ang mga tala ng DK5YA sa tropo at A. Johnson VK3DXE sa blog ng X4 (Disyembre 20F) Ang mga landas ng tag-init ay hindi lamang na natuklasan sa Australia ay ang mga landas ng VPR; ang mga daan ng WBR. Talakayan tungkol sa VHF marina at AIS ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)): saklaw 1,23·i√h(ft) nautical milya kada antena, dinadaglat hanggang sa mga 50 nm, tipikal na trope pagpapabuti 100–400 nm; ang mga padaluyan ay kinikilala dahil ang mga malalayong istasyon ay dumarating na kasinlakas ng mga kapitbahay. Chelmsford ARS News, Disyembre 1983: sa mga paligsahan sa 2 m ang mga istasyon sa silangang baybayin ng Inglatera ay paborito ng ruta sa dagat patungo sa kontinente. EI7GL

ARRL, *The Radio Amateur's VHF Manual*, 1972, cap. 2 "Propagation Beyond the Horizon" (pp. 17–19). Kilala na ang mga dulo ng operasyon (sumunod sa matataas na dako ng presyon na kumikilos nang dahan - dahan at higit sa lahat ng gilid ng kanilang likod; maagang taglagas ang pinakamabuting yugto; baybaying - dagat, malalaking lawa at libis ng mga ilog na gustung - gusto ng stratification). Dalawang bagong impormasyon: (1) igos. Pinaghahambing ng 2-4 ang pamantayang atmospera (katumbas ng kahalumigmigan na 70% hanggang 12000 ft, minimum na refraction) sa isang tipikal na radiasyon ng malakas na refraction, na may maliwanag na temperatura na nasisira at naghahalo ng ratio ng halos 4000 ft (mga 1200 m): ito ang lagda na dapat kilalanin ng ating kalkulasyon sa mga antas ng GFS; (2) ang tunay na mga ducto ay bihira sa ilalim ng 144 MHz at pinakamadalas sa UHF, ayon sa pinakakaunting kapal. Kapaki - pakinabang na bibliograpiya ng QST upang maghanap: Hull, "Air-Mass Confusions and the Brending of UHF Waves" (Hunyo 1935, Mayo 1937); Hoisington, "Painless Prediction of Two-Meter openings" (Oktubre 1949); Collier, "Upper-Air Constions for 2-Meter DX" (Setyembre 1955); Botts, "Isang Gabing Dapat Alalahanin" (Enero 1970, na isinagawa sa lupain).

PA0EHG, pahina "Tropo" ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): buod ng troposcatter mula sa artikulo ng G3YGF, nasa manwal na. Gumamit ng dalawang praktikal na tuntunin upang makontrol ang ating pagkalat: mga 10 dB ekstrang kawalan sa bawat anggulo sa abot - tanaw ng bawat istasyon, at mahigit na 100 km humigit - kumulang 9 dB plus bawat 100 kilometrong ruta (60 milya); ang karaniwang dami ay nagsisimula mula sa halos 600 m sa 100 km at mula sa halos 9 km sa 480 km. Hindi nito ginagamot ang mga duct, mga poll o refractionive na mga pasukan.

PA0EHG, PROPBER° ([pahinang](https://pa0ehg.com/propagat1.htm), programang propber.exe na inilaan ng may akda ng programa). Isa itong 36 na programang kB DOS na nakasulat sa BASIC at tinipon kasama ng Microsoft BASIC Compiler 5.60 (1985): tumatakbo lamang ito sa ilalim ng DOS o DOSBox. Mula sa mga panloob na teksto: tanong para sa frequency, distansiya (o ang dalawang locator, din sa lumang QRA format na "E-locator"), para sa bawat antenang istasyon ng taas, pigurang ingay, banda ng FI, kapangyarihan, isang hadlang (height and distance) at antenang nagkamit (0 = bertebrado ng diyametro, ekwasyong 50%). Nagbabalik ang pinakamalayong distansiya sa optika, ang tamang pagkawalang optikal para sa absorpsiyon ng singaw (0.1 dB/km mula sa 24 GHz), ang pagkawala ng troposcatter na "tuwid para sa mga lugar" (i.e. para sa mga abot-tanaw na anggulo na ibinigay ng mga hadlang) at ang tumbasang S/N sa dalawang mga diwa, gayundin "na may sukdulang abnormal na propaksiyon". Ang mga constant sa lipunan ay hindi mabasa sa tinipong mga dokumento. Ang iminungkahing paraan para sa mataas na mga banda ay kapaki-pakinabang din para sa atin: ang pagkokuwenta sa isang sukatan na kawing (diffence sa pagitan ng sinusukat at tinantiyang S/N) at paglalapat ng parehong pagkakaiba sa ibang banda. Ito'y katulad din ng ating kalkulasyon, subalit ginagamit nito ang profile ng S RTM1 sa halip na isa lamang hadlang at ang hula ng GFS sa halip na isang itinakdang gilid.

PROPBER, ang mga konstante ay nagbabasa sa produktibong (numero sa Microsoft Binary Format format). πr, sa talaan ng datos na katabi ng mga teksto: 8,68589 (= 20/ln 10, i.e. 20 troso na kinakalkula gamit ang likas na logarithm), 32.5 (malayang espasyo: 32.5 + 20 log f\[MHz\] + 20 log d\[km\]), 24000 at 0.1 plus 40000 at 0.3 (vapore: 0.1 dB/km mula sa 24 GHz, 0.3 d//k mula sa 40Hz) Paulit-ulit, dahil sa inuulit mo ngunit nasa loob ng kodigo: −69,5 (parehong kataga ng side-scatter na pormula ni Yeh sa VHF/UHF DX Book), 332 (soil refractionive), 8733 km (equivalent circle, k 1.37). Ang eksaktong pormula sa pagsasabog ay hindi maaaring buuin nang hindi inaalis ang programa. T0

PROPBER, mga pormulang binuong muli ng kodigo (diassimly of the tinipong bahagi; ang mga operasyon ay tinatawag sa aklatan ng BASIC na may mga operator sa memorya, na ang mga halaga sa dobleng prekwensiya ay nai-decode). Ang mga konstanteng −69,5, 332 at 8733 na mas naunang natagpuan ay maling positibo sa loob ng aklatan.

- Optikong distansiya: d = 5 · (√ + + + +) km (h sa m). Mas mapagbigay ito kaysa klasikong 4,12 (k = 4/3): ay katumbas ng k ≈ 2.
- Kawalan sa optika: L = 32.5 + 20 log f[[MHz[] + 20 log d\[km\], at 0.1 dB/km sa ibabaw ng 24 GHz at 0.3 dB/km sa itaas ng 40 GHz.
- Troposcatter: Lts = L + 23 + 10 log f + 0.0674·d, i.e. 55.5 + 30 log f + 20 log d + 0.0674·d (formula ng G3YGF na may constant 2 dB mas mataas). Walang anggulo o reactivity.
- Pagtutuwid "para sa mga site": ang programa ay humihingi ng hadlang at distansiya ngunit ang block na dapat gumamit nito ay walang laman; ang correction variable ay nananatiling 0, kaya ang "tuwid" na pagkawala ay katumbas ng sa pagsasabog.
- Ingay: T = 290·(10^(NF/10) − 1), N = −228,6 + 10 log T + 10 log B (dBW), walang temperature ng antena.
- Ang S/N = 10 logo P + G\_tx − L + G\_rx − N. "May sukdulang abnormal na propagation" ay gumagamit ng optical loos sa halip na ikalat, i.e. ang huwarang bubble case na walang pagkalugi na lampas sa malayang espasyo.

Paghahambing: 300 km at 144 Ang MHz PROPBER ay nagbibigay ng mga 189,9 dB, G3YGF 187,9, ang ating P.452 187,8 (na may sero abot-tanaw na anggulo): ang pagkalat ay nagkakasabay sa loob ng 2 dB. Idinaragdag ng ating kalkulasyon ang abot - tanaw na mga anggulo mula sa profile, temperatura ng antena, at sa halip ng "maximum" ang inaasahang suson sa pamamagitan ng muling pagpaparami at at attenuation.

Rohde & Schwarz, P. Denisowski, "Pag-unawa sa VHF Propagation", Educational Note, Abril 2023 (24 pahina, distribusyon). Pinatutunayan ang mga puntong natipon na (ginaganap bilang isang dalawang-dimensional na alongguide, hudyat na bumababa sa isang linyar na daan na may layo, hanggang 1500 km at lampas pa rito, tumatagal ng mga oras hanggang araw; mabuting lupa sa dagat at kakaunti sa lupa; nakaangat mula sa ilang daang metro hanggang ilang km). Dalawang kapaki-pakinabang na obserbasyon para sa index: (1) ebolusyon sa paglipas ng panahon: ang isang bagong tatag na padaluyan ay kadalasang nagdadala lamang ng pinakamataas na frequency at, ang pagkapal, ay nagsisimulang magdala kahit na ang pinakamababa; ang orasang paghula ay dapat samakatuwid ipakita ang 432 MHz "nakaabante" sa 144 MHz kapag ang patong ay lumalaki; (2) lokasyon ng mga istasyon: ang pinakamahusay na mga kondisyon ay nasa pagitan ng mga istasyon na malapit sa mga dulo ng padaluyan, habang ang mga nasa kahabaan ng landas ng isang mataas na padaluyan ay kadalasang nabibigong pumasok, maliban na isinagawa "nawala". Ang mga duct ay gumagalaw sa pamamagitan ng atmosperikong panahon.

Site PA0EHG, ibang pahina. Para sa tropo, PathProfile of M. Willis G0MJW ([schedda](https://pa0ehg.com/srtmp.htm), [download](http://www.mike-willis.com/Software/ASTERPathprofile.zip)): profile ng ruta mula sa inaasahang kabuuang pagkawala at mga files ng kota, hinuhusgahang maaasahan para sa troposcatter sa microwave; hindi ginagamot ang pagkalat mula sa mga eroplano o ulan. Kapaki - pakinabang bilang ikalawang reperensiya upang ihambing ang ating S RTM1 profile at kawalan. Ang natitirang bahagi ng site ay sumasaklaw sa EME, microwave, paliparan (AirScout, SM7LCB maps) at may sukat na 80 m.

PathProfile by G0MJW (Mike Willis), ASTER at SRTM packages at SiteData (pinalitan ng may akda ng programa; [](https://www.mike-willis.com/software.html)software page). Windows program ng 2018: profile from ASTER o SRTM to 1" o 3", pagkawala ng optiks, diffraction, troposcatter (valid hanggang 1000 km), duct at gasin, na may persentahe ng oras, k median ng ruta, maliit na bahagi ng landas sa dagat at layo mula sa baybayin: ang istruktura ay ang P.452/P.1812, ngunit ang tulong (compressed.m file) ay hindi mabasa dito. Ang SiteData ay kinumberte ang mga locator at nagbibigay ng parte mula sa GT OPO30.

Ang pinakakapaki - pakinabang na bahagi ay ang mga file ng datos na kabilang: ITU-R P.453 estadistikal na mga mapa sa pangglobong grid ng 1,5° (121 hilera mula 90°N hanggang 90°S, 241 haligi mula 0° hanggang silangan): Ang landscape dN/dz sa unang 65 m ay lumampas sa 1, 10, 50, 90 at 99% ng panahon; para sa mga ducto ng lupa ang persentahe ng presensiya, kapal at tindi; para sa mataas na presensiya, base, kapal, at sukdulang taas; at pati na ang term at temperatura. Inilakip ang taunang mga pamantayan:

| Lokasyon | Mga tubo ng lupa: % / kapal / tindi | Mataas na tubo: % / base / kapal / intensidad | Ang dN/dz ay lumampas sa 10% ng panahon |
| Naples |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | −380 N/km |
| Tyrrhenian (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | −674 N/km |
| Trapani |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | −520 N/km |
| Brindisi |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | −405 N/km |
| Adriatico (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | −399 N/km |
| Milan |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | −172 N/km |
| Udine |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | −176 N/km |

Para sa ating indise ay isang handa nang pagtukoy sa klima: ang kapal at tindi na karaniwan sa mga duct sa halip na ang 50 m na nakapirme sa potensiyal na duct, at ang anamalyang "kung ihahambing sa normal" bawat sona. ITU data: libre para sa di-komersiyal na paggamit, upang tingnan bago isama ang mga ito sa isang pampublikong bersyon. Ang ZO NES01.DEM file (58 MB, 2' grid) ay naglalaman ng mga sona ng klima sa loob/coast/sea; ang format ay hindi dokumentado at hindi ko ito ini-code. Ang ekwasyon ni Willis' ay naglalaman lamang ng pisikal na konstante, libreng espasyo at antena na nakukuha.

Ikaapat na round ng mga mapagkukunan (20 Nobyembre).

Indermuehle at Suzuki 2026, prediksiyon ng mga tubo ng GFS para sa obserbatoryo ng CSIRO ng Murchison (arXiv 2608.12861): ito ang pinakamalapit na mapagkukunan ng PHEROPOVHF. Mga profile ng N mula sa GFS hanggang 0.25° (surface hanggang 2 m, pagkatapos ay ang mga isobarikong antas, ang mga nasa ilalim ng lupang itinapon); para sa bawat minimum na kolum ng dM/dz sa ilalim ng 3 km, base at tuktok ng padaluyan, integrated M deficit; pagkatapos ay P.452-16 at P.1812-6 na may mga rutang GLO-90 25 mula 98 hanggang 1005 km, mula 88 MHz hanggang 2,68 GHz, bawat oras. Mga resulta: Median AUC 0,72 sa 25 channel; ang P.452 p time percentage ay dapat gamitin bilang isang calibration constant (choice p = 20%), dahil ang ITU model na may isang konstanteng ΔN ay hindi binibigyan ng kahulugan sa loob ng isang windping; ang kasanayan ay nananatiling halos patag hanggang 3 araw patiuna. Sa 162 MHz (AIS) ang mga tinanggap na ruta ay may pinakamataas na pragmento ng padaluyan na 67% (0.26 versus 0.16) at ang pinakamataas na duct na lalim na 49% (28 vs 19 M) na may kinalaman sa mga kontrol: sa dagat ang padaluyan ay halos palagi, ang nagpapasiya sa kahabaan ng lupa. Nakikita ng GFS ang kalahati ng panahon sa lupa kumpara sa ERA5 (4.5% laban sa 10.4% ng mga selula-hours), habang sa dagat ay mas nakikita ang (73% laban sa 56%). Ang isang "corridor" index (continuous pipelines sa kahabaan ng ruta, mga layer na may hindi bababa sa kapal na 40 m) ay nagpapabuti sa resulta: pagtibayin na ang pagpatuloy sa kahabaan ng ruta ang tamang pamantayan.

Longley and Rice, ESSA ERL 79-ITS 67 (1968): pamamaraan para sa iregular na kalupaan mula 20 MHz hanggang 20 GHz (pagkatapos ng ITM), na may mga diperensiya sa lupa Ang Δh, Ns mula 250 hanggang 400, katumbas ng paikot na 8497 km (4/3), mga abot-tanaw, diffraction, ay kumakalat pasulong at variable sa panahon at sa mga lugar para sa klima. Hindi biglaang pagtukoy sa estadistika.

ITU-R P.834-2: paikut-ikot na kurba, Lupang paikot na katumbas ng k·a, M = N + h/a (h sa m, sa libu-libong km), eksponensiyal na modelo; walang bago kaysa sa nagamit na.

AGARD CP-70 (1970), tropopheric propagation: mga manipis na layer mula 1 hanggang 100 m na may malalakas na shuffold ay karaniwan; ang repleksiyong coficial ng isang mataas na layer ay halos proporsiyonal sa wavelength, na may pasumalang pagkalat ng mga 14 dB. Mga sukat hanggang 90 MHz: pagbawas ng 9–10 dB na nakatali sa isang N jump na natuon sa halos 350 m, sa tuktok ng isang patong ng ulap. Inaalis ng pagdaan ng malamig na harapan ang mga suson: Ang diffraction ay nananatili.

Ulat ng U.S. URSI, Komisyon 2 (1960): katamtamang kapal ng 60 hanggang 210 m matataas na patong (binibigay na tumutugma sa P.453), minimum na spaghetti para sa 157–160 N/km duct.

Buscemi K2OVS, VHF propagation guide: karaniwang mga distansiya para sa pagbaligtad at duct para sa banda, mga duct na nakakabit sa mataas na hindi gumagalaw na presyon, sukdulan sa pagbubukang - liwayway.

Wolinsky-Mancini at 2024, AIS sa Kapuluang Canary: AIS radio pagbaha at receptions; ang mga duct ay mas madalas sa mga mainit na buwan (pagbaligtad ng mga aliseo).

Ang mga artikulo sa Nigeria at Malaysia (Ekah, Iloke, Mat, Igwe): mga lokal na korelasyon sa pagitan ng temperatura, kaumiduhan at nasusukat na kawalan, mahina at nagkakasalungatan (r −0,09 hanggang 0.27); ay kapaki-pakinabang lamang bilang patunay na ang sukat ng lupa lamang ay hindi sapat.

Herben 1984, ang tesis na TU Eindhoven (microonde): 30 GHz sa 8.2 km at OTS satellite sa 11.6 GHz; kislap para sa yanig, multi-course at ulan. Para sa mga microwave ang dinamikong pagkawala ay pinangingibabawan ng ulan at pagyanig, hindi ng mga duct.

Baars, The Parabolidal Reflectionor Antenna (microonde): pakinabang ng isang talinghaga G = η·(πD/λ)2 na may tipikal na kahusayan 0.5–0,65; pagkawala ng mga pagkakamali sa ibabaw ayon kay Ruze, mga glass = eksp(−(4πε/ile)2). Makatutulong kung idaragdag natin ang mga talinghaga bilang alternatibo sa Yagi DL6WU para sa 1296 MHz at lampas pa rito.

Sinusuri na o may hindi na nilalaman: Hirschmann NASA X-731-67-89 (doble), Reid's VHF ST radar (hangin at yanig, hindi gumagana), Scribd slide at generic notes sa troposphere.

Ikalimang round ng mga mapagkukunan (20 Nobyembre, gabi).

Roger Ang signal ay malaki ang nakasalalay sa base at ang Millet (pagtutuwid ng 0,81 hanggang 143 MHz para sa deficit), halos kahit papaano mula sa kapal (−0,17): ang kapal ay hindi makukuha mula sa signal. Para sa atin: Ang pagkalkula sa kapal ng B (climate cloud) ay hindi gaanong mahalaga kaysa sa kakulangan at pakikibahagi; at ang pamamaraan ang saligan para sa tanggapan sa likod, iyon ay upang makuha ang padaluyan mula sa maraming frequency.

Moler and Holden 1960, "Tropospheric dinadaglat ang propagasyon at sirkulasyon ng atmospera", J. Res. NBS 64D, p. 81 at sumusunod (orihinal noong 22/11/2026, doi 10.6028/jres.064d.014). Mga ruta sa dagat ng California patungong Point Loma (San Diego): Santa Barbara 190 milya, Point Mugu 144, San Clementee 78; 1365 MHz at 9380 MHz, hindi VHF; Enero–Abril 1958, katamtamang 1 minuto. Ang kaso ng 26 Marso: isang pagpapatibay ng mga 20 dB sa loob ng mga 3 oras patungo sa tanghali, kapag ang isang cyclonic vortex ay humihina at nagsisimulang humihip at humupa ang hangin sa dagat; sa 15 ang pagkakaisa ay binabago at ang hudyat ay nababawasan ng mga 22 dB mula sa sukdulan. Estadistika (Table 1, 500 hPa, 33 kaso): na may hangin na bumababa sa karaniwang signal ay mas mataas sa halos 2.5 dB kaysa sa pagtaas ng hangin, sa bandang L at X. Ang signal ay sumusunod sa pagkakaiba na kinakalkula sa 500 hPa sa tanda at amplitude ng mga pagkakaiba-iba, nang walang correlation cofity. Ang mabababang suson (sa ilalim ng mga 600 m) ay pabor sa bahagyang pagninilay - nilay sa pagsasabog. Ang mga anticyclones at fronts ay hindi ginagamot, at ang mga "excursions hanggang 60 dB" na nabanggit ay hindi: ang mga ito ay nagmula sa second-hand na buod. Para sa atin: ang patayong mosyon ay isang mahusay na tagapaghula, at ang +1.5 dB para sa daglat at ang −2 dB para sa pataas na hangin ay pareho ng order gaya ng katamtamang sukat na pagkakaiba (2.5 dB); ang 20 dB ay isang solong kaso, hindi isang tipikal na halaga, at nasa microwave.

Banafaa at Muqaibel 2025, IEEE Access: survey of the pipelines (evaporation models, parabolikong ekwasyon, radar taya, GNS at talukap) at klasipikasyon na may awtomatikong pag-aaral sa tunay na mga hakbang; Ang mga support vector machine ay nagbibigay ng pinakamahusay na resulta. Kapaki - pakinabang bilang isang bibliograpiya; ang impormasyon ay mula sa Persian Gulf.

ITU-R P.834-7 (2015): binagong bersyon ng rekomendasyon sa troposferal refraction; walang bagong pormula kaysa sa mga ginamit na.

Abbott, bibliograpiya ng NBS sa troposferous dinadaglat (GOVPUB- C13): nasa ikatlong round na. Slide Scrirbed sa mga duct: nilalamang generic. Ang artikulong ScienceDirect 002191698790034 ay hindi mabuksan.

Ika-anim na round (20 Nobyembre, gabi): Bean at Dutton, ARRL Handbook 2022 at 2023, Balanis.

Bean at Dutton, Radio Meteorology (cap. 6), mga bahagi na hindi pa ginagamit. Field sensitivity to Ns: 0.18 dB kada unit N on the year cycle; 0.14–0,24 dB/N in the day cycle (maximum sa madaling araw), hanggang 0.2–1,1 dB/N sa mga ruta ng 175–200 km sa taglamig; mga pagtutuwid 0.8–0.95. Parameter na binubuo ng Misme: M = a(g\_e − 40 + b·ΔW^n) na may = 0,5 dB sa bawat N/km, na pinagsasama ang katumbas na platform at ang thermal na katatagan ΔW (trabaho upang itaas ang hangin ng 1 km): sa Sahara sa 471 MHz sa 160 km ang tanging katatagan ay 20 dB sa pagitan ng Enero at Hunyo sa halos pantay-pantay na shud. Ang potensiyal na refraction index na K (N ay nag-ulat ng adiabatibong hanggang 1000 hPa): ay nagtatampok ng masa ng hangin at ang anticyclonic respirence (Flavell at Lane).

ARRL Handbook 2022 (99a ed.), § 19.4.3–19.4.4. Ang kapaki - pakinabang na mga pagbabago sa pagitan ng 250 at 2000 m; ang mga pagbaligtad ng radyasyon (sera, kalmadong hangin, tuyong lupa) ay bihirang maging totoo; ang mabagal na mga anticyclones ay nagbibigay ng pinakamalawak na mga duct sa bawat paghupa, na may mga pagbaligtad sa pagitan ng 500 at 3000 m, mas malakas sa gabi at sa umaga; sa hilagang hemispero ang pinakamahusay na mga ruta ay nasa timog ng mataas na sentro. Mainit na harapan: mabuting kalagayan ilang daang km sa unahan; Malamig na harapan: malakas ngunit mabuway na pagbaligtad sa likod ng harapan, parallel. Ang Foehn (na nasa Alps rin) at hanging panlupa (mainit na hangin sa lupa, mainit sa 200–300 m; umaabot sa 800 km sa kahabaan ng baybayin). Scrocco: mainit at tuyong hangin mula sa Sahara sa mas malamig na hangin sa dagat, napakalakas na mga pagbaligtad mula sa silangang Mediteraneo hanggang sa Gibraltar, malamang na sanhi ng mga rekord na mahigit 1500 km sa microwave. Troposcatter: teoretikal na limitasyon mga 800 km, sukdulang tipikal na kalahati. Ang ika-5 ed. (2002) ay may mas maikling teksto: ang mga inbersyon sa pagsikat at paglubog ng araw hanggang +20 dB at +20–50% na bilis ng daloy; 1200 milya sa Great Lakes, tipikal na 400–600; mungkahi ng Radio Mobile (ITM model) para sa pagsaklaw at mga mapa ng Hepburn.

Balanis, Antenna Theory (4a ed.), § 10.3.3: Viezbicke table (NBS TN 688, 1976) para sa Yagi optimisado sa 400 MHz: boom 0.4 / 0.8° / 3.2 / 3.2.2 / 4.2.2°N 1°02°W / 10.7.7.2°N 9.2°W / 14.2°N 1.2°E / 3.2°N___________________________________________. Sa tamang pormula sa orihinal (9.4 + 7.8 log10 boom/Grtil, mula 22 Nobyembre) ang natapon ay bumababa ng mababa sa 0.2 dB sa pagitan ng 1.2 at 4.2 λ (12.1 dBd hanggang 2.2 λ versus 12.25) at nananatiling 0.6 dB hanggang 0.8 λ. DL6WU DL6WU

Ikapitong round (Nobyembre 21,): QST, QEX, W3SZ, GitHub reposition.

QST 2000–2025 (14 bilang, 10 kama na may pagkilala sa teksto). Mga ulat lamang sa The World Above 50 MHz, na kapaki-pakinabang bilang mga kaso upang patunayan ang modelo: Setyembre 5–9, 2000, na isinagawa sa 144 MHz at lampas pa sa silangang kalahati ng USA; 14–15 Agosto 2022, pagbubukas ng Manitoba–Kansas/Misssouri sa ilalim ng mataas na presyon sa isang mainit na araw, mula 144 hanggang 1296 MHz halos lahat ng bagay sa FT8, Canadian record sa 10 GHz ng 1228.56 km ( XQ – XQ2 z) sa XQ3 z,;; APR kmspppppption hanggang sa lugar ding iyon; APR. Ang QST Nobyembre 2022 ay nagmumungkahi ng mga mapa ng Hepburn. Walang teknikal na artikulo tungkol sa paghula sa tropo. VE4MA N0OY Q65

QEX at K1JT, Franke at Taylor (QEX 2020) sa FT4/FT8 modes: FT8 decoding Clocks −20,8 dB sa 2500 Hz sa channel na walang evancence, −18,6 dB na may evancence sa medium latituds; FT4 −17,5 dB. Ang iba pang bilang ng QEX 2023 at 2026 ay hindi naggagamot sa tropo (tatlong numero 2023 ay di-nababasang scan). W3SZ 2019: slow mode clocks ( JT4A −23, JT9A −27, JT65A −25, QRA64A −26, FT8 −21 dB) at mga pagpipiliang batayan sa pagitan ng sensitivity at frequency shift tenrance.

GitHub. TropoLink (LukiBox, Apache-2.0): disenyo ng mga ruta ng troposcatter na may P.617-5 (na may N0 at ΔN), NBS TN101 at ITM na nakatapat, at ang pagkakaiba ng mga ito ay ipinapakita bilang hindi tiyak; P.617-5: L = F + 22 log f + 35 log + 17 d+ Lc, F =8·2 XQ (−), 07/3 +2 +0) +2 +2; +5) +0 θ θ θ θ θ θ θ θ θ θ θ θ θ θ + + + + θ + + + + + + + + + + + + + + + + + + + + + + + + + + + + +, + + + + + + + +, 0 Ati-f/troposcatter ( DL2FZN 2013, mula sa VHF/UHF Handbook RSGB): Lts = 24,44 + 4,283 ln f + 0.06656 d sa itaas ng malayang espasyo, dalisdis malapit sa PROPBER (0,0674 dB/km). QRadio Preedict: ITM at ITWOM 3.0 na may S RTM3, ang lupa ay gumagamit ng mga Corin at mga dayagram ng NEC. Hindi kaugnay: Tropos (apother app), opera-adt/tropo at troposim (Troposferic antala para sa InSAR), tgbot\_troposcatter (populasyong militar), sdr-troposcatter-gui (kontrol na panel), Troposcatter-Propagation-Delay ( ERA5 antala), Radio-Communication-Project (diactic simulator), PyLayLay). N0

Walong lap (21 Nobyembre): NTIA at ITU mga modelo sa GitHub. NTIA/itm (Longley-Rice, ITS): 20 MHz–20 GHz, free spacelost, diffraction at troposcatter na may profile ng lupa, 0.5–300 m terminals, pang-ibabaw reactivity N0 (minity na nabawasan sa antas ng dagat, 250–400), pitong klima, land constants at iba't ibang mode 1 view, 2 diffraction, 3 troposcatter; C+ na may FraTNE.TME.7 under.2. Hindi ito nagmomodelo ng mga duct: ito ay nagsisilbing reperensiya sa "normal na atmospera" na may empirikal na variable. NTIA/p528 at eeveetza/p528 (MATLAB): ITU-R P.528-5 para sa mga air-soil na koneksiyon, 100 MHz–30 GHz, 1.5–20 000 m terminals, p 1–99%;.NET 4.8 casing. Posibleng gamit: air diverse (dalawang ruta sa lupa–craft plus radar section). Eeveetza/p2001 (MATLAB): ITU-R P.2001-6, all-field model na may diffraction, troposcatter, abnormal propagation (conduct and layers, Lba) at Elastivity, persentahe ng oras 0–100%; ITU digital maps (DN\_Median, DN\_SupSlope, DN\_Sublope, dndz_0120; ITEXCli, h0°N_Median, w_MedStsp. Posibleng gamitin bilang paghahambing ng ITU sa halip ng P.452, na may GFS ΔN sa halip ng mga mapa gaya ng sa pag-aaral ng CSIRO; ang mga mapa ay dapat i-upload ng user bilang per P.453. Epektasyon 1.3.0 sa 593 QSO (29/12/2021–2/1/2022): pangyayari/comparison paghihiwalay 0,79 nang walang mga pagtutuwid, 0,91 na may Misme at di-gaano; hindi nakikita ng GFS ang padaluyan sa dagat sa 144 MHz (guadagno na walang mga pagtutuwid na hindi hihigit sa 15 dB).

Ninth round (Nobyembre 21,): IU3QEZ, NTIA, PyWaveProp, ACE High, factation 1.3.1. IU3QEZ (S. Fabris), presentasyon "Sa abot - tanaw sa VHF" (GEKO Magazine n. 69, Setyembre 2026): karaniwang tomo d2/(8·R\_eff) na may R−_eff na 8500 km (1,3 km hanggang 300, 500 km hanggang sa bawat abot - tanaw na 7°B para sa distansiyang 7°NH2; 3°B para sa bilis na 5°08; 1°N 4°0; 2°N 4°N 4°N 5°0. Bibliography of the representation to recovered: Norton 1959 (J. Res. NBS 63D, System Loss), Bray 1961, ARRL Antenna Aklat 2023 fig. 4.29, Rice-Longley-Norton-Barsis NBS TN 101, Bean at Thayer 1959, Barsis at Kirby 1961 (diffration on ridge), Deygout 1966, P.1812, AirScout XN3Q, P.83 N4TIL (ika: air air air), at LA LA LA, LA LA LA LA wi wi, wi wi; LA LA; LA LA, LA LA; LA, LA, LA; LA; LA, LA, LA, LA, LA LA, LA, LA, LA, LA LA, LA, LA, LA; LA, LA, LA, LA, LA, LA, LA, LA, LA, LA, LA PyWaveProp (Python): Parabolic equation methods with Padé approximations, multiple gilid diffraction, PEPOOL at RAM founds, troposferous at high ducts, iregular na lupa: posibleng base para sa isang field kalkulasyon na may GFS M(z profile, na susuriin. APRS PropView (Apache 2.0): VHF propagation maps from APRS packages, hourly statistics at opening alarms; posibleng source ng 144 MHz data para sa professionation. NATO troposcatter network sa Italya (fortifications.net): 900 MHz, 10 kW, 20 m talinghaga; mga istasyon kabilang ang Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Vulture, Monte Lauro; makasaysayang test case para sa P.617. Walang kaugnayan: Gupta 2021 (tratospheric transport), Batas 2023 (Arctic ozone), dolphin #590 at opera-adt/tropo (InSAR antala), tropo.ino (Arduino gas sensor), tropo-webapi (telefonia), ulaptools/troposphere (AWS). Epektasyon 1.3.1 noong 564 QSO tropo: AUC 0,79 (model 1.2), 0,80 (+ soft duct), 0,90 (+ misme and respirence), 0,90 (+ tiyaga); mga kulay ng padaluyan (≥ 16 dB) sa 46% ng mga ruta ng pagbubukas at 0% ng paghahambing. IQ3QC DL2ALF

Idagdag sa parehong round. Ang mikelytaev/wave-propagation (MIT, Python 3): Parabolikong ekwasyon na may mga tantiya ng Padé at Numerov, iregular na lupa, tropospherical ducts; pinangangasiwaan ang binagong refractive M(quota, lat, lon) mula sa NOAA GFS at nagtakda ng isang explosion split ng Monin-Obukhov; walang dokumentadong tungkulin na nagbabalik sa pagkawala ng landas. Siya ang pinakamahusay na kandidato para sa isang panghinaharap na field kalkulasyon kasama ang GFS M profile (ito ay magsisilbi ng isang panlabas na serbisyo o isang salin). RP-Sim (AGPL-3.0, JavaScript): Empirical models for radio networks (Hata, He, Ericsson, dalawang ray), walang tropo. RIMFading (GPL-3.0, OMNET++): mga diperensiya sa daloy para sa network recombinations, Weibull distribution, walang tropo. PyLayers: channel ng radyo sa loob ng bahay, hindi na isinasama. qradiopredict (kantooon, GPL v2): ITM at ITWOM v3.0 na may S RTM3 at Corine, nasa ikapitong round na bilang QRadio Predict.

Ika-sampung round (Nobyembre 21,): Antenna Book, Norton, Barsis, mga bagong pinagkukunan ng datos. ARRL Antenna Book (23a ed.), cap. 4 ( K9LA): Bray method (QST Nobyembre 1961) para sa VHF range: smooth Earth loop to 99% (300 milya, 144 MHz: 214 dB), kurba sa 50% at 99% (fig. 4.11): 195 dB na nagkakahalaga ng 100 milya hanggang 99% at mahigit 250 milya hanggang 50%; evancence −7 dB para sa 100 milya (−3,5 dB hanggang 50 milya), SSB 3 dB mas masahol pa sa CW, +B ng lupa, +3 dB para sa taas na higit sa 8/2 kilometro (−/2 milyang –202 milya); – 10°W/2; coupling/8/8.8/2 coupling/8/8/2 milya; coupling/8/2; 24°NNNNHW. Ang sikat ng araw at paglubog ng araw ay umaabot sa +20 dB at +20–50% na daloy ng tubig; mainit at tuyong hangin sa basang sariwang hangin sa Great Lakes hanggang sa 1200 milya, tipikal na 400–600; mga rehiyon na naimpluwensiyahan ng malalaking masa ng tubig ang pinakapabor; huling taglamig na may init ng malakas na inversions. Ang beripikasyon ng ating troposcatter: P.452 hanggang 50% sa 483 km at 144 MHz ay nagbibigay ng halos 204 dB, na tumutugma sa 214 dB ng Bray hanggang 99%. Norton 1959 (J. Res. NBS 63D, malaya sa NIST): pangunahing pagkawala, sistema at path antena na nakakamit; Reference environment Ns = 301; katumbas ng paikot mula sa Ns (250 → 4878 milya, 301 → 5280, 400 → 6997); L(99) mga talahanayan na may 30-foot na antena (100 MHz: 163 dB hanggang 100 milya, 208 dB hanggang 500 milya; 1000 MHz: 138 at 188 dB na may 28-foot na mga talinghaga); long-term variable V(p, π) depende sa anggulo (fig). Rice, Longley at Norton, NBS TN 15 (1959), libre sa NIST. Barsis at Kirby, NBS Report 6751 (1961): 223 km sa Colorado na may Pies Peak (4292 m) bilang gilid, 100 at 751 MHz; patuloy na pagkawala na may pabilog na gilid (humigit-kumulang 11.5 km circle); long-term evancence 11,7–14,3 dB verses 39–42 dB ng isang nakakalat na parehong haba; walang day cycle na minarkahan; ang four-ray model na inilaan 4–5 lobes ngunit isa lamang ang sinusukat (maxum hanggang 21 mB) na nakakalat mula sa mga landas ng bundok na mas malaki ang silbi kaysa sa cacto. " are" ARRL VHF Digital Handbook: pakete, APRS, D-STAR, WSJT (Scatter Method at EME), hindi bahagi sa tropo. Ang mga bagong napatunayang datos ay: ECMWF open data (IFS 0.25°, mga antas 1000, 925, 850, 700 hPa..., walang 975, 900 at 800; G RIB2 na may CCSDS compression, na ang ating mambabasa ay hindi nagkokokodigo; makukuha na sa pamamagitan ng Open-Meteo); DWD ICON-EU open data (G RIB2 staund bz2, isang file para sa variable at maraming kahilingan ng Canada); ang GPSCEACACACA&TF; nagbibigay ng pahintulot para sa mga Philippines para sa mga antas ng G&CACATF.

Ika-labing-isang round (Nobyembre 21,): Longley-Rice, GEKO 69. Bean at Thayer, "Models of the Atmospheric Radio Refractive Index", Proc. IRE Mayo 1959 (pp. 740 et seq.): Ang modelong 4/3 ay may tamang dalisdis sa unang km ngunit bumabagsak ng napakataas; 30% ng kurba ng isang paikot ay nangyayari sa unang 100 m at 60% sa unang km; average na relasyon sa pagitan ng Ns at nabawasan sa unang km: −N = 7,32·e^(0,005577·Ns) (Ns = 313 → ΔN bandang −42). Bean at Thayer, "C.R.P.L. Exponiential Reference Atmospheric", J. Res. NBS 63D n. 3 (1959): N(h) = Ns·e^(−c·h) na may c na itinakda ng Ns sa parehong ulat; Average Ns USA 313; beam tracking tables (katumbas na kurba 15.2 mrad na may exponential model vers 30.3 miradod kasama ang Earth 4/3). MATLAB refractionexp: c = 0.143859 km−1 para sa Ns = 313 (scale mga 6.95 km), na may bisang higit sa mga 1 km at 10 mrad. Gamitin para sa atin: ang "normal" na profile ng paghahambing ay maaaring ang CRPL sa lokal na Ns sa halip na M = 315 + 0.118·z ay nakapirme, at ang anamalya ng pagbaba sa unang km ay dapat na sukatin kung ihahambing sa −7,32·e^(0,005577·Ns) sa halip na −40°. NTIA Report 82-100 (Hufford, Longley, Kissick 1982), ITM guide in area mode: climi at Ns iminungkahi (equatorial 360, subtropical continental 320, subtropical marine 370, disyerto 280, katamtamang kontinental 301, katamtamang marino sa lupa 320, katamtamang dagat sa dagat 350); Ns = N0 ·e^(−hs/9,46 km); ginagamit mo ang buwanang pinakamababang bilang ng N dahil mas mababa ang impluwensiya ng super-refraction; N·1 –78 = km – 84. Rice, Longley at Norton, NBS TN 15 (1959), bahagi: paghula ng pormula ng pamamahagi sa paglipas ng panahon sa pamamagitan ng land at tropospheric wave, base ng TN 101. SoftWright, Wikipedia, Altair WinProp at MATLAB rfprop. LongleyRice: parehong impormasyon (bersion 1.2, 20 MHz–20 GHz o 40 GHz, 1–2000 km, Ns 301, pitong klima, avaribility of time, lugar at sitwasyon); walang sinuman ang humubog sa mga duct, tanging ang epektong estadistikal ng mga klima sa dagat ("napakataas na field sa 10% ng panahon o hindi"). GEKO Magazine n. 69 ( IU3QEZ): radio preview 4/3 ng 130 km mula 1000 m, 260 km dagdag na dalawang istasyon; troposcatter na may karaniwang volume sa pagitan ng 1–3 km at 10 km; dinadaglat mula sa sasakyang panghimpapawid hanggang 10 km na may +10–20 dB sa loob ng sampu-sampung segundo o dalawang minuto; mga kursong teoretikal na may 2×10 elemento sa bawat gilid: SSB QRP tungkol sa 440 km, SSB W humigit-kumulang 510 kilometro, CRPC. Ang mga hakbang na halos 640 km ay dapat na ang haba ng WB sa loob ng D&C.

## 12. Round table: mga mapagkukunan ng impormasyon upang patunayan ang modelo o ituwid ito (Nobyembre 21)

Sumaryo ang lahat ng iba't ibang mapagkukunan ng impormasyon, na ipinag - utos na gamitin. "State" ay nagsasabi kung ang pinagmulan ay nasa programa (1.3.1), ito man ay nasa pagsubok o kung ito ay gagawin.

### Dataang patunayan (kung ano talaga ang nangyari)

| Pinagmumulan | Ang nagbibigay | Gamitin | Kalagayan |
| Ang MMMonVHF ay nag - uulat mula sa awtor | 593 QSO sa 144 MHz, 29/12/2021–2/1/2022, panahon at locators | Pangyayarisyon laban sa paghahambing (+14 araw) | ginagamit: AUC 0,79 (1.2) → 0,90 (1.3.1) |
| Ang Daigdig ay Higit sa 50 MHz | dokumentadong mga kaso (14–15/8/2022 Manitoba–Kanas, 144–10 GHz) | Makasaysayang kaso | na ginagamit bilang isang makasaysayang kaso; upang idagdag sa bisa ng QSO |
| wspr.live (WSPR) | kompletong arkibo, 2 m at 70 cm, SQL | maraming mga pangyayari at maraming "no" (oras na walang batik) | sinubukan, ito ay dapat salain (mali sa 16–20 libong km) |
| APRS PropView / APRS-IS | ang pakiramdam ng mga istasyon, distansiya, oras | ang tanging bisa 144 MHz | gagawin |
| Baligtad na Beacon (arkibo) | batik - batik sa araw - araw | VHF | itinapon na ngayon |
| I GRA2 radiosondaggi (Brindisi 16320, Gawain ng Mare 16245, Trapani 16429, Udine, Milan) | Ang mga profile ay may sukat na 00 at 12 UTC | Pagkontrol sa profile at pagkumberte ng GFS | nahanap na mga file (Brindisi 46 MB), upang gamitin |
| Barsis at Kirby 1961, Barsis 1963 | Ang pag - akyat sa burol na sinusukat sa mga landas na may malaking hadlang | Pinatutunayan ang kategoryang "monte" (Vesuvio, Faito) | gagawin |
| Norton 1959, NBS TN 15, TN 101 | L(99) at mga pagkalugi ng media sa mesa | Pagkontrol sa pagkalat | (Tingnan ang 300 milya na hindi nagbabago) |

### Mga pagtutuwid na iminungkahi ng mga reperensiya

| Pagtutuwid | Pinagmumulan | Epekto na sinusukat sa pagiging totoo | Kalagayan |
| Hindi Matatag Termino | Bean at Dutton cap. 6 | AUC 0,79 → 0,90 kasama ang paghupa | sa 1.3.1 (Tittle 16 dB na walang padaluyan) |
| Paglubog mula sa patayong pagkilos | Moler at Holden 1960 (binabasa sa orihinal noong 22/11: +2.5 dB average sa pagitan ng hangin na bumababa at tumataas ang hangin, 1.4 at 9,4 GHz sa pamamagitan ng dagat) | gaya ng nasa itaas | sa 1.3.1 (+1.5 / −2 dB) |
| Soft pipeline (Gumagalaw ng hanggang 3· abusemax) | Kerr, Hepburn, VHF/UHF DX Book, Shi 2019 | lamang +1% ng AUC; dalhin ang mga kulay ng duct sa 46% ng mga ruta ng pangyayari at 0% ng paghahambing | sa loob |
| Pagtitiis 24 h. | VHF/UHF DX Book, Biancomano at Leyshon | Walang pagbabago, mas marami ang kinuha at mas maraming maling alarma | sa 1.3.1 |
| Normal na profile ng CRPL na may lokal na Ns (−ΔN = 7,32·e^(0,005577·Ns) sa halip na 40 N/km na nakapirme | Bean at Thayer 1959, Monograpiya ng NBS 4 | sinubok sa bisa | sa 1.3.2 bilang isang mapagpipilian, dahil sa depekto |
| Super-refraction stand 79 N/km (ITU) sa halip na 100 (Bean at Dutton, layer 1000–850) | P.453, Lopez 2008, Aklat ng DX | sinubok sa bisa | sa 1.3.2: Ang agresibong pagtantiya sa tabi ng maingat (ang mapa ay gumagamit ng pantas) |
| Ang klimatolohikal na kapal (calcula B) ay hindi gaanong mahalaga kaysa kakulangan | Rogers 1997 | upang subukin: gamitin ang A para sa pakinabang | gagawin |
| Parabolikong ekwasyon sa profile ng GFS | PESOOL, wave-propagation (Lytaev), Levy (aklat na hindi binabasa, pahapyaw lamang), Lindquist | unang bersyon na may field sa abot-tanaw na sakop ng numerong ingay (AUC 0.12–0,28): tama (domain 8 km, beam 6°); pagkatapos ng pagtutuwid AUC 0.54–0,66 | na ginagamit bilang ikalawang kalkulasyon, huwag magpasiya kung ano ang mangyayari |
| Pagiging gising: ±7 dB na mahigit 100 milya, margin 10–20 dB hanggang 90–99% | Bray 1961, Antenna Book, GEKO 69 | Mga pasimula ng mga paraan sa panggilid na mga kalagayan | ginagamit: probabilidad para sa yugto at climb FT8 −18,6 dB sa troposcatter |
| Karaniwang dami at katumbas na layo ng Bray | IU3QEZ, Bray 1961 | Impormasyon | sa loob |
| Troposcatter P.617 at RSGB bilang walang katiyakan | TropoLink, DL2FZN | Impormasyon | sa 1.3.0 |

### Kung ano ang wala pa sa mga modelo

Pinangasiwaan sa dagat sa mababang altitud (100–200 m) na hindi nakikita ng mga antas ng GFS (pag-aaral ng mga kasalukuyang pinagkukunan ng impormasyon); pagninilay-nilay at diffraction sa mga kalapit na bundok (Vesuvio, Faito): panukala para sa pagkalkula ng "via monte". Ang ITM/Longley-Rice ay nananatiling isang reperensiyang estadistikal na "normal na araw" (Ns 301, climi), na walang mga padaluyan. Nadiskaril sa pamamagitan ng desisyon ng may akda: AirScout, P.528), Elast (P.2001) at 50 MHz (servoir EIRP na iilan lamang ang mayroon at nalilito sa Paminsan-minsan).

### Mag - utos na magtrabaho

1. Extended deficiation: idagdag ang sinala WSPR (kahit na mga oras na walang batik) at QST 2022; ulitin para sa 432 MHz.
2. Subukin ang normal na profile ng CRPL at ang pasukan ng ITU na 79 N/km, isa-isa, na pinananatili lamang kung ano ang nagpapataas sa AUC.
3. Paghahambing ng profile ng GFS sa I GRA2 radio wave ng Brindisi at Practice of Sea noong panahon ng QSO.
4. Ibalik ang parabolikong ekwasyon pagkatapos ng pagtutuwid ng numero.

### Mga pasiya ng 21 Nobyembre (bersiyon 1.3.2)

- Naampon: maingat na pagtatantiya (Bean at Dutton, 100 N/km) at agresibo (ITU, 79 N/km) na magkakahilera; mga duct ay naghanap lamang sa ilalim ng 2500 m (unang 1500 m, na itinaas pagkatapos ng professionation); bonus na may mataas na extended pressure at mahinang hangin; bigat ng dagat na may μ1 ng P.452; bahagi ng mga istasyon na may paggalang din sa daluyan sa bahagyang influsion; sapin sa ibabaw ng mga umbok ( IT9GSF).
- Inampon bilang isang alternatibo, na pinatay ng depekto: normal na CRPL profile sa lokal na Ns.
- Tamang parabolikong ekwasyon Bilang: AUC mula 0.12 hanggang 0.54 (30 m antena) at mula 0.28 hanggang 0,66 (300 m).
- Lupain: Copernicus DEM GLO-30 Sa halip ng Terrain Tiles «skadi», na malapit sa baybayin ay may mga ghost relief na 80–100 m sa dagat (signation of IC8TEM, pinatutunayang punto por puntos sa Capri); ang skadi ay nananatiling isang reserba.
- Ang mga palatandaan ng lagay ng panahon sa mapa na may epekto nito: ↓ kaaya - ayang paghupa (Moler and Holden 1960, sa katamtaman ay mga 2.5 dB sa pagitan ng hangin na bumababa at tumataas; +1.5 dB), ↑ hanging paahon na hindi kaaya - aya (di - kanais - nais na lalim; −2 dB), S scirocco paboritong clue (ARL Handbook § 19.4, mga pagbaligtad na napakalakas sa Mediteraneo; walang pagtutuwid). Double symbol, berde/pula/orang mga kulay at komiks sa mapa.
- Pinagtibay ang ITM 1.2.2 (NTIA/itm, public domain), isinalin sa C#: ayon sa kalkulasyon ng baseline sa normal na atmospera sa tabi ng P.452 at pangwakas na kahabaan pagkatapos ng paglabas mula sa isang padaluyan na nagwawakas bago ang kabalitaan. Itapon ang qradiopredict code dahil sa ilalim ng GPL v2.

Walang kapaki - pakinabang na nilalaman para sa indise: *Communications Quarterly*, taglagas 1991 (walang artikulo tungkol sa tropo; ang artikulo ni Atkins tungkol sa pagkalat ay nasa taglamig 1991).

- Inaampon bilang prinsipyo ng mga manwal (Nobyembre 21,): ang 'propasyon ay hindi maaaring gawing pangkalahatan, ngunit pinag-aralan kaugnay ng lokal na morpolohiya' ( DF5AI, iniulat ng IK3XTV sa 'Propagation sa 2 metro'); na binanggit sa manwal ng paghula (§ 1) at sa mga konseho.
- Inampon (Nobyembre 21, pagkatapos ng 1.3.3) ang interpretasyon ng mga transalpine openings ng IK3XTV: hindi "mga selulang kombinatorika tulad ng mga lente", ngunit troposcatter na pinagtibay sa pamamagitan ng troposcatter sa ibabaw ng layer re-enacted kapag ito ay umabot sa bahagi ng karaniwang volume (manual of the proclamations, § 8 ter). Mula 1.3.4 pumapasok ito sa point-point na kalkulasyon bilang probisyonal na pagpapatibay ng troposcatter, sa karamihan ng 3 dB. Natatanggal ng parehong mga artikulo: ang "auto-amplification" ng mga hudyat na lampas sa Alps (ang isang walang kibong medium ay hindi lumalakas: ang mga inilarawang link ay nananatiling maipaliwanag sa pamamagitan ng diffraction at troposcatter; ang mga ipinahayag na kapangyarihan ay hindi ginagamit bilang ibinigay), ang niyebe na "nagpapatunay ng diffraction" (malamang na ang matatag na patong sa ibabaw ng niyebe, na hindi gaanong evancence).

- Kerr (1951) ang aklat (Nobyembre 22, PDF na inilaan ng awtor): § 1.5, eq. 17 at 18 at mesa 1.1. Ang mga bilang ng eq. 18 sa scan ay hindi mabasa, ngunit ang talahanayan 1.1 ay nagpapatunay: 20 ft → 1 cm, 80 ft → 10 cm, 400 ft → 100 cm, 2000 ft → 1000 cm, laban sa 1,25, 10, 112 at 1252 cm ng reformmax = 0.014·d_1.5 (mga Filisteo sa cm, d nakatayo; ang bilog na mesa, 'indicase'. Kerr: Sa loob ng padaluyan ang binagong indise ay bumababa ng 4·10−8 bawat paa, karaniwang halaga ng mabababang duct; sa mas mahinang mga tubo ay nangangailangan ng mas malaking kapal. Si Kerr ay nagbababala rin na ang mustromax ay hindi isang net cut: ang mga wavelength kung minsan mas mahaba ay maaari pa ring lubhang maimpluwensiyahan ng duct.

- Nirepaso ng DL6WU ang orihinal (Nobyembre 22, PDF ng VHF Communications 3/1982 na inilaan ng may akda): G. Hoch, «Extremely Long Yagi Antennas», p. 130 at sumusunod. Fig. 1: tamuhin sa dipole na may sukat na 432 at 1296 MHz sa isang propesyonal na polygon ng mga Post na Aleman, na may sampol na antenang EIA mula 7,7 dBd, tinatayang may katumpakang 0.5 dB; 2,35 dB para sa doble ng haba ng boom, walang staturiation na umaabot hanggang sa mga 20 λ. Ang programa ay gumamit ng 8.6 + 8.0.log10(L/must) dBd, i.e. mga 0.8 dB sa wala pang 1–2 λ; ngayon ay gumagamit ito ng 9,4 + 7.8·log10(L/Grp) dBd (ang quarter ay ang nakasaad, ang halaga sa 1 λ ay binabasa sa tsart, ± 0.3 dB). Ang pormulang 10·log10(7,8·L/Grt) ay kumalat sa net: ay magbibigay ng 3 dB para sa pagdoble, laban sa 2,35 na sukat.
- Booker at Gordon (1950): reference na pinatunayan ng "This Week's Citation Classic" (Mga Kwentong Nilalaman, 8 Pebrero 1982, komentaryo ni H. G. Booker): Proc. IRE 38, p. 401–412. Pinatutunayan ng komento ang dalawang puntong ginamit sa mga manwal: ang larangang natatanggap na lampas pa sa abot - tanaw sa pamamahagi ng Rayleigh, at ang eksponential autocorrelation function (non-Gaussian). Ang orihinal na artikulo ay hindi pa nababasa.

## 13. Mula sa mga artikulong IK3XTV

Ang dalawang artikulo ni Flavio Egano IK3XTV (Radio Rivista: "Propagasyon sa 2 metro" at "Propagasyong VHF – kombinatorikang mga selula at mga along grabitasyonal") ay nagdaragdag ng dalawang mga bagay na hindi sinasaklaw ng mga duktong modelo: ang mga butas na transalpine ng hapon dahil sa kompyuter, at mga praktikal na alituntunin kung saan at kailan nabuo ang mga tubo sa Italya. Ang mga ito ay mga obserbasyon mula sa istasyon, hindi mga hakbang: ang mga ito ay dapat ituring na mga palatandaan upang masuri ng mga batik.

| Komento ng awtor | Kung paano ito nagiging sangkap ng indise |
| Ang mga duct ay pangunahing nabubuo sa pagitan ng 100 at 1000 m, bihirang mas mataas; nagsisilbi ng mataas na presyon sa isang malawak na lugar, mahusay na insolasyon at kawalan ng hangin | Paghahanap sa daluyan sa ilalim lamang ng 1500 m; bonus na may matinding presyon at mahinang hangin sa mabababang suson |
| Sa mainit na mga buwan na paulit - ulit na isinasagawa sa Adriatico sa pagitan ng Veneto at Puglia, 700–800 km, kung minsan hanggang sa Gresya; ang mga ruta sa dagat ang paborito | Mas mabigat na bigat sa pragmento ng rutang pandagat (tulad ng μ1 ng ITU-R P.452) |
| Ang katumbas ay dapat na nasa loob o malapit sa padaluyan; na may matataas na antena at ilang metro ang layo sa hudyat ay hindi pumapasok | Ihambing ang bahagi ng pinakapuno ng padaluyan sa bahagi ng dalawang istasyon (nagsisilbi sa taas ng QTH) |
| Sa parehong panahon, parehong padaluyan: makinig mula sa Gargano isang istasyon noong 144 at isa sa 50 MHz | upang suriin: sang - ayon sa mesa ng Hepburn ang 50 MHz ay humihingi ng mahigit na 300 m tingga, kaya ang pangyayari ay nagpapahiwatig ng isang tubo nang napakadalas |
| Mga koneksyon Veneto – Timog Alemanya lampas pa sa Alps, walang nakikitang paningin, halos palaging sa hapon at may mataas na presyon; mabuway na mga hudyat na naglalaho at muling lumilitaw pagkaraan ng ilang minuto | "convective" index na nakahiwalay sa daluyan ng tubig, para sa mga ruta ng bundok |
| Ang mga selulang nag - uugnay ay isinisilang sa lupa at tumataas ng halos 1 km sa bawat 3 oras: sa gulang na 15 ang mga ito ay umaabot ng mga 3 km, ang taas na nagsisilbing panlulupig sa Alps | gamitin ang limitasyong layer tangkad (boundary\_layer\_height of Open-Meteo GFS) sa kahabaan ng landas: iskor kapag lumampas ito sa altitud ng mga tagaytay |
| Ang Alps ay lumilikha ng mga along grabitasyon na, pati na ng mga paggalaw na nag - uugnay, lumilikha ng mga depekto sa refractionive index | Mga teoriya: Malakas na hangin sa kawing hanggang 700 hPa bilang hudyat ng mga along ginto; subuking huling subukin |
| Ang napakataas na pagkumberte (hanggang 8 km sa natatanging mga kaso) ay nagbibigay ng mga 800 km, subalit sa gawain ay bihirang mahigit sa 300 km | ang saklaw: kapaki - pakinabang na mga impormasyon upang tantiyahin ang layo ng naghihintay |
| Sa tag - araw ang Lupa ay umiikot na katumbas ng halos 16000 km (2.5 ulit ng tunay), na may halos 60 dB sa wala pang 300 km | Kasuwato ng k50 = 157/(157 −N): k = 2,5 ay katumbas ng ΔN mga 94 N/km, na super-refraction |
| Mahigit sa 3000 km lamang sa 20–30° sa hilaga at timog ng ekwador, sa itaas ng dagat, na nakaugnay sa mga selulang Hadley; tuktok noong Agosto sa Aprika–Europe | sa labas ng Europa: kapaki - pakinabang na huwag magbigay ng matataas na indices sa Atlantiko nang walang feedback, at sa ruta ng Canary – Gran Britanya |

Iba pang kapaki - pakinabang na impormasyon mula sa mga artikulo ring iyon:

- Ang optikong saklaw sa pagitan ng dalawang h1 at h2 metro (d in km): d = 4,1 · (√ + + +), hanggang sa mga 20% para sa normal na refraction.
- Pagiging gising: mabagal at mababaw na kasama ng pagbaligtad; malakas at mabilis na may nakakalat at naghahalong mga selula. Maaaring gamitin ito ng programa upang uriin ang mga anunsiyo kung balang araw ito ay magkakaroon ng mga ulat ng SNR sa paglipas ng panahon.
- Para sa paminsan-minsang E noong 144 MHz iniulat ng awtor ang akda ng DF5AI at Davis at Johnson (Nature 435, 2005) tungkol sa gampanin sa pagitan ng mga temporal at Es: mula sa tropo, ngunit ang isang hiwalay na indibidwal bukas ay maaaring gumamit ng datos ng kidlat.
- Gander reflection sa double jump Es: sea K = 81 at 5 S/m, lalawigan at kakahuyan K = 13 at 0.006 S/m, urban areas K = 3 at 0.001 S/m.

## 14. ANG indise ng NOEROPOVHF

Simulang panukala, na pag-usapan: isang indise 0–10 sa parehong lawak ng Hepburn, kinalkula para sa bawat punto ng grid at bawat 3 oras mula sa GFS (noo'y ECMWF at ICON para sa paghahambing), at ikalawang bilang sa bawat ruta na nagsasabi kung gaano kalaking bahagi ng ruta ang dumadaan sa isang kapaki-pakinabang na dukto sa napiling banda. Ang mga pabigat sa ibaba ay ang aking mga palagay, na mababahiran ng mga batik.

Review kasunod ng Bean at Dutton at Kerr: ang mga profile clock ay nagmumula sa kanilang table 6.5 at scale mula sa kanilang mga sukat, kaya ang isang refractive layer ay nagkakahalaga ng 10–25 dB sa ibabaw ng normal na profile. Pagkatapos ang indise ay unang kinakalkula sa dB at pagkatapos ay kumukuha ng 0–10; ang mga halaga sa scissor na iyon ay nananatiling kalibihan ng mga batik.

Pagkalkula nang isa - isa, hakbang - hakbang

1. Ang pagsabog, relatibong kaumiduhan, geopotential tangkad at vertical speed sa 1000, 975, 950, 925, 900, 850, 800, 700, 600 at 500 hPa, pati na ang T at 2 m halumigmig, presyon ng lupa, layer taas at hangin sa 10 m.
2. Itapon ang mga antas sa ilalim ng lupa; kalkulahin ang N at M sa bawat antas (ITU-R P.453).
3. Pagkalkula −dN/dh sa bawat isa sa apat na patong ng Bean at Dutton (1000–850, 850–700, 700–600, 600–500 hPa) at ang tsart bilang sub-refractive, normal o super-refractive na may mga pasukan ng kanilang mesa; sa loob din ng unang patong ay gumagamit ng panggitnang antas upang mahanap ang pinakamalakas na quarto at ang parte nito.
4. Kung saan matatagpuan ng dM/dh < 0 ang base at ibabaw ng duct, ang kapal ng d at deficit ΔM.
5. Kung ihahambing sa pinakakaunting kapal para sa banda mula sa pormula ni Kerr (mga 180 m para sa 144 MHz, 89 m para sa 432 MHz, gaya sa mesa ng Hepburn); sa ilalim ng pasukan ang padaluyan ay mahalaga pa rin, subalit kaunti.
6. Pinasisigla ko ang pakinabang sa dB sa pamamagitan ng mesa sa ibaba (ninukuha mo ang pinakamabuting layer, hindi ang kabuuan ng mga suson, pati na ang termino ng analya) at binabago ito: index = dB / 2.5, sa pagitan ng 0 at 10, kaya ang 25 dB ay nagkakahalaga ng 10. Ang isang negatibong halaga (sub-refraction) ay nagpapakita kung paano 0 ngunit nananatili sa path data.

| Pinakamabuting kalagayan ng layer | Criteria | Tinatayang kita |
| Normal (hindi laminado) | −dN/dh sa saklaw ng tab. 6.5 (e.g. 20–60 N/km sa pagitan ng 1000 at 850 hPA) | 0 |
| Medyo mataas sa normal | sa pagitan ng itaas na hangganan ng normal na banda at ng super-refraction start | 0 hanggang 10 dB, ayon sa proporsiyon |
| Super-refraction | Paglampas ng hangganang patong (100, 80, 70, 50 N/km pag-akyat) ngunit sa ilalim ng 157 N/km | mula 10 hanggang 15 dB, sa proporsiyon na hanggang 157 N/km |
| Napakanipis na tubo para sa banda | dM/dh < 0, kapal sa ibaba Dulo ng Kerr | 15 |
| Kapaki - pakinabang na tagakabit para sa banda | dM/dh < 0, kapal sa itaas ng pasukan ng Kerr | 20 dB, +1 dB bawat 5 M yunit ng deficit, pinakamataas 25 |
| Sub-refraction | −dN/dh < 0 sa ibabang suson | −10 dB |
| Maingay na background (idinagdag mo) | Modelo Ns minus ITU map climatological Ns para sa buwang iyon | 0.2 dB bawat yunit N, sa pagitan ng −5 at +5 dB |

Pagkalkula sa pamamagitan ng ruta (sa pagitan ng dalawang locator): tikman ang sukdulang bilog sa bawat 10 km, gaya ng ginagawa na ng programa para sa indise sa landas, at kalkulahin ang maliit na bahagi ng mga punto sa kapaki - pakinabang na daluyan at ang katamtamang bahagi ng base. Natagpuan ng pag-aaral ng CSIRO ang kapaki-pakinabang na bahagi ng ruta sa tubo (sa mga ruta ng AIS sa 162 MHz ay 67% mas mataas kaysa sa mga kaso ng kontrol).

Pagdaragdag ng kalkulasyon sa bawat ruta (mula sa LA0BY at W3SZ): (1) mga istasyon ng dagat kumpara sa base at tuktok ng daluyan ng tubig, dahil ang isang mataas na padaluyan ay nagsisilbi lamang sa mga pumapasok; (2) isang troposcatter "floor" na may paraang Yeh, na may mga simulang sulok na kinuha mula sa lupang profile; ang pagkakamit ng trope sa dB ay inaalis mula sa pagkawalang iyon at ang resulta ay inihahambing sa pagkawala na nagagawa ng istasyon upang mapagtagumpayan ( KK6MC table). Kaya ang indise ay nagiging isang tantiya rin ng maaabot na distansiya.

Mga pagtutuwid upang subukan ang isa sa isang panahon (mula sa Bean at Dutton at LA0BY, IK3XTV, G4MCU na nasa manwal na), upang panatilihin lamang kung ang mga ito ay magpapabuti sa paghahambing sa mga batik:

- Paghahalo (importante ayon sa may akda, sa pamamagitan ng araw-araw na pagmamasid): araw sa lupa, sa pagitan ng kalagitnaan ng umaga at hapon na may maaliwalas na kalangitan, o sa pamamagitan ng hangin sa lupa sa ibabaw ng ilang m/s, ang mga duct sa lupa ay nawawala; ang kanilang kontribusyon ay dapat bawasan. Hindi sa dagat.
- Panahon ng araw (importante ayon sa may akda, sa pamamagitan ng araw-araw na pagmamasid): ang mga hindi makatwirang duct ay ipinanganak sa paglubog ng araw at bumabagsak pagkatapos ng pagsikat ng araw; ito ay isang 3-oras na kontrol sa paghula.
- Vertical motion: ang hangin ay tumataas sa 850–700 HPA remixes ang mga patong (negative recruit), hangin pababa sa ilalim ng isang anticyclone ay nagpapalakas sa mga ito (positive).
- Hilig ng presyon: pataas na presyon sa landas sa mga huling oras, mas mataas at matatag na hudyat; sa pagbawas, mas mababang hudyat (bastano 2 hPa).
- Synonym: mataas na presyon na higit sa 1025 hPa, temperatura sa 850 hPa sa ibabaw ng lupa, mainit at tuyong hangin na dumadaloy sa mas malamig na dagat, ruta sa pamamagitan ng dagat.
- Mountain paths: isang pangalawang "convective" index (section IK3XTV), na may isang hapon na limitasyong layer na mas mataas sa mga ridge sa kahabaan ng landas at mataas na presyon.

Mula sa DX ng VHF/UHF Aklat, iba pang mga pagtutuwid upang subukan: sunud-sunod na mga araw ng mataas na presyon sa punto (mas maraming araw, mas maraming puntos), posisyon kumpara sa sentro ng anticyclone (bonus sa mga gilid), presyon na dumadaan sa lupa bilang sukat ng hangin, malakas na ulan sa naunang 24 na oras (penalidad), mababang ulap sa gilid ng patong (penalidad lamang para sa mataas na mga bigkis). Para sa direksiyon ng mga ruta: ihambing ang azimuth ng ruta sa isobare.

Pagveripikasyon

- archive ng NOBROPOVHF: bawat komersyal na mahigit 500 km hanggang 144 o 432 MHz ay isang "punto". Inihahambing nito ang indise sa ruta sa dalas ng mga pangyayari, sa parehong oras ng araw at buwan (parehong paraan ng pag-aaral ng CSIRO).
- Kung ihahambing sa Hepburn at F5LEN sa parehong mga araw: kung saan ang tatlong indexs ay nag-iiba, ikaw ay mukha kung sino ang tama ayon sa mga batik.
- Radyo ng 00 at 12 UTC: upang masuri kung nakikita ng modelo ang mga pag-ikot na sinusukat ng probe.
- Ang mga makasaysayang kaso ng LA0BY (6–11/2003, 10/12/2004, 15/10/2011, 11/2/2015, 22/11/2018): ang index recalculed sa mga panahong iyon ay dapat makita ang mga duct na hindi ipinakita ni Hepburn.
- Mga limitasyong idedeklara sa manwal: isinasagawa sa ilalim ng 100–200 m na halos hindi nakikita sa antas ng presyon ng GFS; maaasahang prediksiyon sa loob ng 2–3 araw, pagkatapos ay kalakaran lamang.

## 15. Pinagmulan ng 21 Nobyembre (ikalawang bahagi)

- ITU-R P.1812 (MaTLAB/Octave pagpapatupad ng I. Stevanovic, OFCOM; GitHub eeveetza/p1812, Zenodo 10.5281/zenodo.611558). Espesipikong VHF/UHF area method of the storage, hango mula sa P.452: "delta-Bullington" diffraction, troposcatter, ducts at replection sa mga layer, lalamon sa lupa. Kapaki-pakinabang bilang ikatlong paghahambing na katabi ng P.452 sa punto-point na kalkulasyon; ang ΔN at N0 data ay puno ng mga produktong ITU, kaya ang gumagamit nito (bilang per P.453).
- mula sa Costa, Rabanal Ramirez, Dias, IET MAP 2018 (doi 10.1049/iet-map.2017.0992). 563 MHz, 27 ay nakaturo sa pagitan ng 5 at 30 km sa hindi regular na kalupaan na may kagubatan: P.526 na may 12 m ng pananim −4,3 dB at σ 8,8 dB; P.1812 na may 10 m ng encumbrance +2,6 dB at σ 8,9 dB; dielectric slike −4,8 dB at σ 9,2 dB; P.1546 +5,0 dB at σ 11,7. Para sa programa: ang stage error ng isang mahusay na diffraction method ay mga 9 dB, at ang taas ng mga encumbrance sa mga ridge (10–12 m) count.
- PESOOL (Ozgun e altri, Computer Physics Communications 182, 2011). Parabolikong ekwasyon split-left sa isa at dalawang ruta na may malawak na anggulo propagator (mga single hanggang 40–45°, laban sa 10–15° ng makitid); ibaba na may kawalan sa pamamagitan ng binagong Fourier na halo-halo; lupa hanggang sa mga hakbang. Pinatutunayan ang mga pagpipilian ng 1.3.2 (buong anggulo, lupang cofits) at nagpapahiwatig ng dalawang-way na kalkulasyon para sa matarik na reliefs.
- Mathworks. `tropopl`: pagkawala ng mga gas at pagkawala ng «lent» sa isang layered atmospera (P.835, P.676, P.453), walang ducts: na may mga spaghetti sa ibaba −157 N/km ang heometrikong optical model ay hindi kumakapit. Ang halimbawa ng radar propation ay gumagamit para sa refraction −°N = 7,32·e^(0,005577·Ns), ang parehong ulat ng CRPL na nasa programa na. `propagationModel` Longley-Rice: predefinated pahalang polarization na mga halaga, conductivity 0.005 S/m, permittivity 15, reactivity 301, katamtamang klimang kontinental. Walang modelong Mathworks ang tumatalakay sa mga duct.
- ITU-R, mga digital na produkto ng grupo 3. Data at programmes ng P.453, P.452, P.1812, P.2001, P.528, P.1546; 'integrated' data ay bahagi ng mga rekomendasyon; walang ipinahayag na lisensiya sa relatibidad: patuloy mo silang hinahayaang mag-download sa tagagamit.
- ESA Navipedia, troposferical antala. N = Nidrostica + Numada; ang tuyong zenital na pagkaantala ay mga 2.3 m at ang mahalumigmig na isang sampu ng cm, na mabilis na nagbabago. Para sa amin: ang wet antala na tinataya ng mga istasyon ng GNSS ay isang hindi direktang sukat ng integrated umivity, posibleng sa hinaharap na mapagkukunan ng ngayon-casting; sa ngayon ay hindi na magagamit.
- Bray, K2LMG, «A Method for Decreeting V.H.F. Station Capabilitiesić, QST Nobyembre 1961, p. 36–40 (basahin sa orihinal; rechecked noong Nobyembre 22 sa PDF ng bilang ng QST: lahat ng halaga sa ibaba ay katumbas). Ang awtor, si D. W. Bray (General Electric, Ithaca), ay nagsasabi na pinatunayan niya ang mga hula na may mga buwan ng nakatakdang mga koneksiyon sa K2GOI at W4LTU. Ang calcence sa loob ng panahon ng 13,5 dB sa higit sa 100 milya (7 dB sa ibaba at 7 sa itaas ng average sa oras), halos linear sa ibaba (3,5 dB hanggang 50 milya): para sa isang solidong koneksyon alisin 7 dB. Pagtutuwid ng abot - tanaw: ang bawat taas ng abot - tanaw ay nagkakahalaga ng 69 na milya (111 km) ng karagdagang distansiya (wala kung negatibo). Gabayin ang isang yagi noong mga 10 × boot/ilet (sukat ng kuryente), mas marami pa sa 4 dB ng pagninilay-nilay ng lupa; SSB −3 dB at AM −7 dB kumpara sa CW; 99% ng mga oras: mga 180 dB hanggang 60 milya, 190 hanggang 90, 200 hanggang 240 (2 m). Mula 1.3.2 ang probabilidad para sa panahon ay gumagamit ng σ = 3,4 dB (±7 dB bilang ±2°) sa halip ng 5 dB na tinatayang ikalawang kamay.
- Dalawang-way na kalkulasyon (try with prototy, 21 Nobyembre). Ang malaking anggulo na parabolikong ekwasyon na may reality S RTM1 lands mula sa Capri, 144 MHz, antena 10 m mula sa lupa, pamantayang atmospera; sukat: katamtamang lakas sa pagitan ng 20 at 400 m hanggang 80 km sa ruta patungo sa katumbas. Mula sa sentro ng Capri at Marina Grande ang kanluran ay sarado (harang sa +11–21°, Monte Solaro): ang pagtutok sa Faito (halos sa likod ng EA5 at EA6, 150–160° sa axis) ang signal na inilunsad sa Espanya ay umaakyat ng 10–29 dB; itinuturo ang Vesuvius (110–125° sa axis, pagkatapos ay mula sa plano ng kalkulasyon, tanging resonanve) ng 17–25 dB. Mula Anacapri ang kanluran ay malaya at ang direktang panalo ng 8–22 dB. Gayunman, ang ganap na pagkalugi sa 1000–1300 km ng prototype ay hindi maaasahan. EA3 EA5 EA6
- Barsis at Kirby, NBS Report 6751 (1961), Pokes Peak, 223 km, 100 at 751 MHz. Ang bundok ay isang karaniwang terminal ng dalawang ruta sa optika: ang distribusyon ng mga median sa oras ay convolution ng dalawang ruta. Long-term eksplorasyon (1–99%) ng mga 14 dB, laban sa 26–40 dB ng isang troposcatter path na pantay ang haba o anggulo; walang aplikadong pagbabago sa araw. Ang hudyat ay mas malakas kaysa sa troposcatter ngunit mas mahina kaysa sa huwarang gilid: ang tuktok ay dapat ituring na bilog (bagaman halos 11.5 km). Ang matataas na signal ay laging napapansin sa likod.
- Barsis, NBS Report 7652 (1963), tatlong ruta sa ibabaw ng isang bundok (Colorado, Alaska, Europa), 750–900 MHz. Katamtamang pagkawala 10–20 dB na mas mataas kaysa Fresnel-Kirchhoff na kalkulasyon nang walang lupa malapit sa mga terminal; ang evancence ay tinatayang sa pamamagitan ng pagbabalot ng dalawang mga linya sa optiks; ang mga repleksiyon ng lupa malapit sa mga terminal ay nagpapalala sa matagal na pagnipis. Mula 1.3.2 ang pagpasok sa padaluyan ay nagdaragdag ng pag-ikot ng P.526.
- Barsis at Johnson, NBS TN 88 (1961). Ang matagal na pag - aalis ng alon sa kalawakan, kahit na sa isang landas sa kabila pa roon ng isang bundok (§ 5).
- Norton, «System pagkawala sa radio wave propagation», J. Res. NBS 63D (1959). Ang mga kurba kung saan nakuha ni Bray ang mga nomogram; ang mga kahulugan ng pagkawala ng sistema, paghahatid at pangunahin, prosesong antena ay nakakamit.
- Rice, Longley, Norton at Barsis, NBS TN 101 (1965–67), Tomo 1 at 2. Ang isang gilid at hadlang ay pabilog kasama at walang repleksiyon (§ 7, na may halimbawa ng Pokes Peak), long-term variable V(0,5) at Y(q) para sa mga klima (tempered concontinental at iba pa). Para sa atin: ang paraan ng pabilog na hadlang at ang mga tungkulin ng pagiging maayos ay tumutukoy sa kawalang - katiyakan ng calibrate.
- ITU-R P.526-16 (11/2025). Formula (31) ng J(IS), katulad ng sa programa; Rounded lock (32)–(36), na ginagamit ngayon; mga pamamaraang multi-split (Bullington at buong pamamaraan § 4.5).
- ITU-R P.1812-8 (2025) at -7. Kumpletong teksto Nabawi: espesipikong pamamaraan ng 30 MHz hanggang 6 GHz (delta-Bullington, troposcatter, ducts at repleksiyon sa mga layer, ingombs). Ang -7 ay nagbabala na ang troposcatter (mula P.617-5) nito ay mali sa ilalim ng 66 km. Ito'y nananatiling isang posibleng ikatlong paghahambing.
- Paulus, «Plavatical application ng IREPS Expossion modelison, NOSC TR 966 (1984), at Radio Science 20 (1985). Ulat na mababasa sa 22/11/2026 (DTIC ADA146528, scan na may kinikilalang teksto, kung minsan ay hindi mabasa). Ang mga pagkakaibang panghimpapawid–dagat na sinusukat ng mga barko ay nagbabago tungo sa katatagan: mas mainit na hanging pandagat sa 31–35% ng mga datos ng barko laban 0.7–7% ng mga buhakag ng NOAA, para sa mga psicrometer na nagambala ng barko at SST na kinuha mula sa pinalamig na tubig (humigit-kumulang +1 °C). Pagkatapos ang modelong Jeske ay nagbibigay ng 40 m at higit sa 15% ng taon at taas na tumalon mula 10 hanggang 40 m sa 24 oras na may matatag na oras, hindi nakumpirma sa pamamagitan ng mga sukat ng radyo. Ang nakaposisyong pagtutuwid (subok na mga pasukan 0, −0,5 at −1 °C; ang −1 °C ay mas maiging sumasang-ayon sa mga sukat ng radyo noong Hunyo 1982): kung ang pagkakaiba ng hangin–dagat ay −1 °C o mas mababa pa sa sinusukat na pagkakaiba; kung hindi ay tinantiya mo ang taas na may −1 °C at may 0 °C at, kung ang may −1 °C ay mas mababa, ay hawak mo iyan. Ang mga pagtaas sa matatag na panig ay pinuputol, ang pag - unti ay nananatili. Noong Hunyo, ang Marsden 120 square: average na taas mula 16.8 m (climatology) hanggang 7.6 m. Ang mga ekwasyon ng modelo ay wala sa ulat (refer to Jeske and Hitney). Ang programa ay gumagamit ng neutral na profile, na walang matatag na pagmamalabis, at nagtatakda ng δ hanggang 40 m: ito ay tumutungo sa parehong direksiyon; sa mga SST ng mga modelo (hindi ng barko) ang pagkakamali ng pagsukat na ang mga Paulus ay nagreresulta ng mas mababa.
- Levy, «Parabolikong Pamamaraan ng Equation para sa Electromagnetikong Wave Propagationić (IE 2000). Sa Google Libri ay mayroon lamang patiunang pag - asa: Hindi ako nagbabasa.
- VK3FS (3fs.net.au). De - kalidad na pagpapakilala: matataas na tubo sa 450–2000 m para sa mas mahahabang butas sa lupa; mga pagbaligtad na 300–600 m sa mahinahon at mahinahong mga gabi, na naglalaho pagkatapos ng pagsikat ng araw; mas malalakas na mga version sa baybayin sa hapon; 800–1300 km karaniwan, 1400–1600 km madalas na isinasagawa. Kasuwato ng araw/gabi ang mga babala at kisame na 2500 m.

## 16. Pinagmumulan

Mga Alituntunin at mga pormula

- [ITU-R P.453-13 (2017), radio refractive index:](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)formulation at data
- [ITU-R P.452-18 (2023), prediksiyon ng pakikialam sa pagitan ng mga istasyon sa lupa sa mahigit 100 MHz: katumbas ng radius, β0, troposcatter,](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)ducts
- [ITU-R P.617-5, propagation sa kabila ng abot-tanaw para sa mga tulay ng radyo](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)
- [Shi et al., "A Comprehensive Study on Wave Maximum Dweight of Electromagnetic Propagation in Difference Evaporation Ducts", IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

Mga pag - aaral tungkol sa mga duct na may mga modelo ng lagay ng panahon

- [P. Lopez, "A 5-year 40-km resolution global climatology of super-refraction for ground-based radar meteorology", ECMWF Tech. Memo 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO), "Tropospheric Ducting Prediction batay sa GFS Model Data sa... ang CSIRO Murchison Radio-astronomy", Observatory arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (FMI), "Mga ekwasyon ng model resolution hinggil sa paghula ng mga kondisyong atmosperiko kasama ang Harmonie-AROME", EGUsphere 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

Mga hula sa radyo sa amateur

- [W. R. Hepburn, "The Hepburn Tropo Index (HTI)"](https://www.dxinfocentre.com/propagation/hti.htm)at [](https://www.dxinfocentre.com/propagation/history.htm)method history
- [W. R. Hepburn, "Lowest Usable Frequency vs. Mastness of Ducting Layer"](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, "Worldwide refractive index propesiya"](https://tropo.f5len.org/WW/info.php), [bagong bersyon 2026](https://tropo.f5len.org/?p=504), [not sa modelo ARPEGE](https://tropo.f5len.org/author/f5len/),

Data

- [Open-Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api), [note sa antas ng presyon](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api),
- [NOAA NCEI, Integrated Global Radiosonde Archive (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RAOBget, iskrip para sa baha sa radyo para sa University of Wyoming](https://github.com/NCAR/RAOBget)

Klasikal na mga teksto (ipinahihiwatig ng awtor ng programa, upang sumangguni para sa pangwakas na mga pormula)

- B. R. Bean, E. J. Dutton, *Radio Meteorology*, NBS Monograph 92, 1966, 435 mga pahina (reprint Dover 1968): mga patayong profile ng N at M, pag-ikot ng bitag sa ilalim ng −157 N/km, kumpara sa radio wave; kabanata 5 "Synoptic radio meteorology". Base para sa 2–4 hakbang ng kalkulasyon sa bawat punto. Malaya: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [NIST card, doi 10.6028/NBS.MONO.92](https://www.nist.gov/node/1770911), [Internet Archive](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf)copy.
- L. Boithias, *Radio Wave Propagation*, McGraw-Hill 1987 (isinalin ni D. Beeson mula sa Pranses, 330 mga pahina, ISBN 0070064334; gayundin ang North Oxford Academic): mga mekanismong lampas pa sa abot-tanaw, karaniwang dami ng pagsasabog at pagkawala ng vouzing antena-mezzo. Papel lamang ([library card](https://libcat.library.tamu.edu/Record/in00000014812/Description)).
- M. P. M. Hall, *Effects of the Troposphere on Radio Communication*, IEEE Electromagnetikong Alon Series n. 8, Peter Peregrinus 1979, 206 mga pahina (ISBN 0906048257; brossura 0863410863): synodikong mga sitwasyon at proplikasyon VHF/UHF. Base para sa mga sangkap na Synoptic. Kalakalan sa IET ([](https://libcat.library.tamu.edu/Record/in00000636385)card).
- H. G. Booker, W. E. Gordon, "Isang Teoriya ng Radyo na Kumakalat sa Troposphere", *Proceedings of the IRE*, vol. 38, n. 4, Abril 1950, p. 401–412: Scapping theory from the disogenty of the refractive index. Gitnang ideya: Ang kalat na lakas ay depende sa tindi at lawak ng maligalig na mga pagbabago ng refractive index (exponential correlation); malapit sa abot - tanaw ay umiiral ang diffraction, higit pang pagsasabog; Ayon sa mga awtor ang lawak ng pagyanig at ang mga pagkakaiba - iba ng indise na inaasahan ng meteorolohiya ay sapat na upang ipaliwanag ang sinusukat na mga larangan. Binanggit ng 328 mga artikulo (Scilit), walang libreng kopya. Card on Semantic Scholar ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); full text on IEE Xplore bayaran ([doi 10.1109/JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)).
- C. M. Crain, J. R. Gerhardt (Electrical Engineering Research Laboratory, University of Texas), "Measurements of the Parameters kasangkot sa Teoriya ng Radyo Pagsabog ng Radyo sa Troposphere", *Proceedings of the IRE*, vol. 40, n. 1, Enero 1952, p. 50–54, doi 10.1109/JRPROC.1952.199 Ang tunay na mga pagpapahalaga na ibinigay lamang ni Gordon at ni Gordon (tinatayang XQ.)](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)
- "The Constants in the Equation for Atmospheric Refractive Index in Radio Frequencies", *Proceedings of the IRE*, vol. 41, n. 8, Agosto 1953, p. 1035–1037: nagmumungkahi na repasuhin ang mga agos na ginagamit noon (79 K/mb at 4800 K) sa pormula N; mula rito ang mga pamantayang 77,6 at 3,73 · 10^5 ng pormulang ITU ([Q](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E. C. S. Megaw, "Fundamental radio wide propagation theory", *Proceedings of the IEE Part C: Monographs*, vol. 104, n. 6, Setyembre 1957, p. 441–455, doi 10.1049/pi-c.1957.0052: espesye ng mga pagbabago ng indise mula sa teoriyang epistemolohiya, ganap na pagtalunton ng pagkalat na lampas pa sa karaniwang abot-tanaw at epekto ng superpanya (ET) sa IET[SilQ.](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)
- *Proceedings of the IRE*, Oktubre 1955, espesyal na bilang sa pagsasabog ng propagation (ni K. A. Norton at J. B. Wiesner), kasama ang Bullington, Staras at iba pa: malaya sa PDF on [World Radio History](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf).
- D. E. Kerr (dinadaglat ni), *Propasyon ng Maikling Radyo Alonso*, MIT Radiation Laboratory Series vol. 13, McGraw-Hill 1951 (Restampy Dover 1965 at IEE 1987): teoriya ng pagsingaw at mga daluyan ng ibabaw sa dagat. Digital na kopya: [World Radio History](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); IET edisyon sa [IET Digital Library](https://shop.theiet.org/propag-short-radio-waves).

Ang kamakailang mga hakbang ng troposferical difusion sa VHF: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Australia), "A Study of VHF Troposcatter Communication Channels in South Australia", sa *Digital Signal Processing for Communication Systems*, Springer 1997, p. 103–112 ([Springer](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)). Mag - text para sa isang bayad; mula sa abstrakto ito ay may kinalaman sa diffication, hindi sa mga padaluyan. Kapaki - pakinabang lamang kung nais mong makuha ang termino ng troposcatter sa pamamagitan ng VHF channel data.

Mga modelong Numerikal at mga ruta sa dagat (ipinangak ng awtor ng programa)

- M. Levy, * Ang Equation Ways for Electromagnetic Wave Propagation*, IEEE Electromagnetic Wales Series #45, 2000, 336 mga pahina (ISBN 0852967640): parabolikong ekwasyon para sa refractive profiles na nagbabago sa kahabaan ng landas at para sa orograpiya; mga kabanata sa troposferous proplikasyon, mga sinag at paraan, proparasyon sa dagat. Ito'y magsisilbing isang kalkulasyon sa bukid sa kahabaan ng landas, lampas pa sa indise. Para sa pagbabayad, din sa electronic form mula sa IET ([IET card](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)).
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (University of Leicester): 2 GHz sukat sa Channel Islands at sa Channel. Maraming mga akda at thesis ang libre sa arkibo ng unibersidad: [long-term statistics on the pagsingaw duct, Radio Science 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434), [signature change sa tatlong ruta ng dagat, Radio Science 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710), [Tittles of N. Mufti 2012 na may mga ruta ng 50 at 140 km din sa VHF](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854), [tesis of I. Alam 2002 tungkol sa reactivitivity taya mula sa data ng panahon, 240 MHz at GH3 XQsi.](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus, "Practical applications of a explosion model", Radio Science vol. 20 n. 4, 1985, p. 887 (hindi IEE Transactions on Antennas and Propagation), doi 10.1029/RS0204p0000887 ([scheda](https://zendy.io/title/10.1029/rs020i004p00887)): taas ng explosion duct mula sa ibabaw ng data ng panahon. Free ang teknikal na ulat bago nito: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf).

T. Lindquist, *Wave Propagation Models in the Troposphere for Long-Range UHF/SHF Radio Connections*, master's thesis, University of Karlstad, 2 Hunyo 2020, 106 na mga pahina (dinadaglat ng may akda ng programa). Ito ay isang step-by-lef na gabay, libre, upang magsulat ng kalkulasyon na may parabolikong ekwasyon: split-pace solution (kahit na sa malawak na anggulo), hangganang kondisyon para sa lupa at dagat, lupa sa mga hakbang, beam tracking sa linear profile kung minsan, na may mga numerong tseke; banggitin ang PETOOL free tool para sa MATLAB. Dalawang kapaki - pakinabang na impormasyon nang minsanan: (1) N sensitivity to weather variables, δN 0.26 δP + 4,3 δ δ − − 1,4 δT (hPa at K, sa paligid ng 20 °C), i.e. isang pagkakamali ng 1 hPa sa presyon ng singaw ay tumitimbang ng tatlong ulit sa 1 °C error sa temperatura: ay ang halumigmig ng modelo na dapat munang suriin; (2) Katamtamang taas ng daluyan ng pagsingaw: 5–6 m sa North Sea– 13°14 m sa Mediterranean, para sa mas mababang Golpo MH32. Ang konklusyon ng awtor (frequencies 1 hanggang 10 GHz): lampas pa sa abot - tanaw ang maaasahang mga koneksiyon ay halos laging nangangailangan ng isang duct.

Propagation software sa profile ng lupa (para sa abot - tanaw at mga hadlang, hindi para sa inaasahang tropo)

- [Radio Mobile](https://ve2dbe.com/rme.html)ni Roger Coudé VE2DBE: walang bayad, Windows (binabaybay ding Linux at Mac na May Alak), Longley-Rice/ITM model, landscape profile na awtomatikong kinuha mula sa mga take data ([download](https://ve2dbe.com/download/download.html)).
- [SPLAT!](https://qsl.net/kd2bd/splat.html)ni John Magliacane KD2BD: GPL, 20 MHz–20 GHz, Longley-Rice at ITWOM v3.0 mga modelo, hadlang at mga ulat ng pagkawala sa teksto; pinakabagong bersyon 1.4.2 ng 2014.
- [ITM, NTIA/ITS](https://its.ntia.gov/software/itm)opisyal na kodigo: ang modelong Longley-Rice (20 MHz–20 GHz) na may pinagmulan at dokumentasyon, himpilan ng Radio Mobile at SPLAT! Ang kodigo ay libre at maaari mong idagdag sa kalkulasyon sa landas ng NovETROPOVHF (wala dahil sa lupa, na idaragdag sa indise ng tropo). Orihinal na ulat: A. G. Longley, P. L. Rice, "Prediction of Tropospheric Radio Transmission Loss Over Irreguular Terrain: A Computer Method – 1968", ESSA Technical Report ERL 79-ITS 67, Hulyo 1968 (NTIA card on historical software: X1 https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITU-R, software at digital data](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): libreng mga mapa ng ΔN at N0 taunang median sa grid 1,5° na ginamit ng P.452 at P.617 – kapaki-pakinabang bilang reperensiya sa mga halaga ng klima para sa index.
- Pathloss (Kontract Telecommunication Engineering): programang pangkomersiyo para sa mga tulay ng radyo; wala akong nasumpungang isang pinagmumulan ng impormasyon sa publiko na nagpapatunay kung aling mga kasangkapan ang inirerekomenda ng ITU, kaya upang suriin.

Reusableng Kodigo ng GitHub sa NERETROPOVHF

| Proyekto | Ano ang ginagawa mo | Wika | Gamitin ang posibleng paraan |
| [NTIA/ITM](https://github.com/NTIA/itm) | modelo Longley-Rice opisyal ng NTIA, na may mga pakete para sa. NET |  C++, C#  | pagkawala ng lupa sa landas: Ang mga bahagi mismo ng programa, na nasa C# |
| [fuzzzeee/itm](https://github.com/fuzzzeee/itm) | ITM C++ code C# salin |  C#  | Ang alternatibo ay walang mga katutubong aklatan, na kapaki-pakinabang din sa bersyon ng Windows 7 |
| [eeveetza/Py452](https://github.com/eeveetza/py452) | ITU-R P.452, kapareho ng bersiyong MATLAB/Octaba na inaprubahan ng ITU-R WP 3M; bersiyong MATLAB na inilathala ng Swiss Federal Communications Office | Python, MATLAB | tinutukoy ang pagsasalin C# ang pormula ng troposcatter at isinagawa, na may mga halimbawa ng refluentation |
| [bwinkel/pycraf](https://github.com/bwinkel/pycraf) | P.452 kumpleto, P.676, SRTM profiles; GPL v3, ngunit ang mga talaksang datos ng ITU kabilang ay hindi libre para sa komersyal na paggamit | ♪ | pagkontrol sa ating mga resulta |
| [ITU-Rpy](https://github.com/inigodelportillo/ITU-Rpy) | maraming rekomendasyon ng ITU-R, kabilang ang P.453-13, na pinagtibay ng mga halimbawa ng ITU | ♪ | beripikasyon ng pormulang N at mga mapa ng klima ng P.453 |
| [NCAR/RAOBget](https://github.com/NCAR/RAOBget) | download ang radio wave ng University of Wyoming sa format | ♪ | Larawang ida - download sa C# ang mga profile ng mga istasyon sa Italya |

Note: ang kasalukuyang bersyon ng P.453 ay ang [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), mada - download nang libre; sa dokumentong ginamit ko ang 13, upang maging reallined.

Sa mga manwal ng programa at muling magagamit dito: LA0BY (Nordic VUSHF 2019), IK3XTV ("Propagation on 2 metro" at "Propagation VHF – convective cells and gravity waves", Radio Magazine, inilaan ng awtor ng programa), DF5AI/DK5YA (2003), G3YGF (QST 1983), The VHF/UHF DX Book cap. 2, G4MCU (2003). KP4MD
