# Hulaan ang manwal

> Awtomatikong salin ng Italyano: Kung pag - aalinlanganan ay sulit naman ang giyang Italyano.

Kinakalkula ng task ng propesiya ang tropopheric propagation sa ganang sarili: isang indiseng mapa sa nakikitang lugar at ang hudyat na inilalaan sa pagitan ng iyong istasyon at ng isang katumbas na larawan. Inilalarawan ng manwal na ito ang bawat datos, kung paano ito kinakalkula at kung alin ang pinagmulan nito. Ang mga numero sa square brackets ay tumutukoy sa mga pinagkunan sa ilalim; sa programa ang bawat datos ng mga resulta ay may komiks (simbolo i) na may parehong pinagmulan at pagtukoy sa parapo.

## 1. Kung ano ang kaniyang ginagawa at kung ano ang kaniyang ginagawa

Ang prediksiyon ng vertical temperature at shamity profile ng GFS [1, 2] model ay kumukuha ng reaktibidad ng layer ng hangin sa pamamagitan ng layer at pagtatantiya kung gaano kalaki ang pagtaas ng signal kumpara sa isang normal na atmospera. Sa ruta ay idinaragdag nito ang tunay na lupa (Copernicus GLO-30 [3]), diffraction, troposcatter, ducts at receiver ingay.

Hindi ito isang panukat: ito ay isang tantiya na ginawa sa isang modelo ng lagay ng panahon na may malalayong antas na daan - daang metro. Inihahambing nito ang mga kalagayan (sa ngayon ay mas mabuti kaysa kahapon, 70 cm na mas mabuti kaysa 2 m, ang paggamot na ito ay mas mabuti kaysa riyan), hindi upang garantiyahan ang kaugnayan. Ipinaliliwanag ng Parapo 14 kung paano kukuha ng ebidensiya para patunayan ito.


> " Ang pagpaparami ay hindi maaaring gawing pangkalahatan, subalit pinag - aralan may kaugnayan sa lokal na morpolohiya. »
> — Volker Grassmann, DF5AI, iniulat ni Flavio Egano, IK3XTV (Propagation on 2 metres)

Ito ang prinsipyong gumagabay sa punto-point na kalkulasyon: ang bawat ruta ay kinakalkula sa lupa nito, na may mga abot-tanaw, ang dagat nito at ang mga relief nito; ang mapa ay nagbibigay ng pangkalahatang larawan, ay tumatalakay sa sagot.

## 2. Mga utos ng wika

- Band: 144, 432 o 1296 MHz. Ginagamit sa mapa at kalkulasyon ang napiling banda.
- Ngayon: ang di - inaasahang pangyayari ay nag - aalis ng hula mula ngayon hanggang 72 oras na patiuna, sa loob ng 3 oras.
- Patiunang pagtaya (viviible zone): I - download ang mga profile ng GFS sa isang grid ng hanggang 240 puntos na sumasaklaw sa nakikitang mapa. Ang grid pitch (0.5° o higit pa) ay depende sa magnipikasyon.
- Stop: Huminto sa pag-download o pagkalkula.
- Mga detalye upang ipakita (tamang panel ng mapa): tatlong antas, Essential, Normal at Complete, buksan ang isang grupo ng mga kahon; ang bawat kahon ay maaaring magbago sa pamamagitan ng kamay. Ang mga kahon ay nasa tatlong grupo:
  - Sa mapa: isolinee del Gain, 3–7 dB (paglilinaw), isobare, thermal inversion, weather signs S ↓ ↑ ↑, correlation surge/pression, sona ng direktang pananaw (parapo 4, 5, 7 at 9 bis). Laging nananatili ang mga kulay.
  - Sa kalkulasyon ng signal: lahat ng linya ng kalkulasyon, flat Earth profile, pagkalkula ng log at detalyadong log ng file.
  - Mga transpormasyon at live na datos: mga pagtutuwid ng panahon (Misme, direksyon), pagkakaiba ng hangin-dagat, katamtamang may batik (corridoi), APRS 2 m jumps at LoRa, lampas sa bundok nang walang nawawala (parapo 4 bis, 9 bis, 9 quater at 10 bis).
  - Mahalaga lamang ang mga isoline, lagay ng panahon at media na may mga batik; ang Normal ay nagdaragdag ng isobare, inversion, weather signs, direktang pagtingin, patag na lupa, air-sea at APRS; Complete reverse ito. Binabago lamang ng antas ang nakikita mo at kung anong mga pagtutuwid ang pumapasok: ang mga kalkulasyon ay nananatiling pareho.
- Pinagmulan ng datos at kasong pangkasaysayan: mula sa kung saan nagmula ang profile at kung aling araw ka tumitingin (parapo 3 bis at 3 ter).
- Ang aking istasyon at ang kabalitaan: Loweror, antena, kuryente, kable, ingay (parapo 7 at 12).
- Tuusin ang signal at saklaw sa optika: ang dalawang kalkulasyon sa lupa (parapo 8–13).
- Pagtaya sa rehistro: bawat hakbang na may bilang na ginagamit, kasama na ang sa Data/paghula-calcoli.log file.

## 3. Mula sa GFS data hanggang sa profile

Sa bawat punto ng grid at bawat oras ay binabasa natin ang presyon sa lupa, temperatura at kaumiduhan sa 2 m, hangin sa 10 m, taas ng hangganang patong, ang CAPE at temperatura at halumigmig na may kaugnayan sa 1000, 975, 950, 925, 900, 850, 800, 700, 600 at 500 hPa. Mula 1.3.0 maaari mo ring basahin ang hangin hanggang 850 hPA at ang patayong bilis hanggang 850 at 700 hPA (parapo 9 bi.)

Ang mga antas ay kinakalkula gamit ang pormulang opsometric at ang bertikal na temperatura; kakaunti ang antas sa ilalim ng lupa. Sa bawat antas:

- N = 77,6/T · (P + 4810·e/T), kasama ang T sa kelvin, P at sa hPa, at sa pamamagitan ng pwersang pwersa ng ITU-R P.453-14, eq. 9, na may EF reflection factor (pinamamahala sa opisyal na teksto) [4].
- M = N + 157·h, may h sa km: ang binagong reaktibidad. Kung saan umaakyat ang M ay may isang padaluyan.

## 3 bis. Ang pinagmulan ng impormasyon

Pinipili ng Date Source menu kung saan nanggaling ang mga profile:

- Awtomatikong (isinaayos): GFS una mula sa NOMADS [14], ang serbisyo ng NOAA na namamahagi ng modelo: para sa bawat sandali ang programa ay humihingi ng dalawang cutouts ng lugar sa G RIB2 formation at binabasa ito nang mag-isa; ang NOMADS ay walang pang-araw-araw na limitasyon, isa lamang sa bawat minuto na iginagalang ng programa. Sa pamamagitan ng malalapad na grid (mula 0.5° hanggang sa itaas) gumagamit ito ng 0.5° o 1st files tuwing 3 oras, mas magaan. Ang mga punto na hindi ibinibigay ng NOMADS, o lahat kung hindi tumutugon ang NOMADS, ay nagtatanong ng Open-Meteo [2]. Ang dalawang reperensiya ay nagbibigay ng iisang modelo at iisang lahi, kaya maaari kang maghalo sa iisang grid.
- GFS mula sa Open-Meteo: tanging Open-Meteo, kagaya ng sa 1.2 (sa hangganan bawat minuto ay inaasahan).
- MGA GF mula sa NOAA NOMAD: tanging NOMAD. Mabagal na mag-download (dalawang files bawat oras, kalahating segundo sa pagitan ng isang kahilingan at ang isa pa ay hindi lalampas sa limitasyon ng NOAA), ngunit hindi nila nauubos ang limitasyong Open-Meteo.
- ICON-EU (DWD) at ECMWF IFES 0.25°, mula sa Open-Meteo: mga modelo maliban sa GFS, na kumakapit sa buong grid at para sa mga ruta. Ang mga ito ay hindi humahalo sa GFS: ang paghahambing ng dalawang modelo sa parehong panahon ay simpleng paraan upang makita kung paano ang paghula ay hindi tiyak. Ang ICON-EU ay sumasaklaw lamang sa Europa; ang ECMWF ay walang lahat ng GFS pressure levels (ang nawawalang antas ng pagtalon) at maaaring walang hangganang layer.

Ang pinagkunang ginagamit ay nakasulat sa linya ng katayuan at sa unang linya ng mga resulta (Data source), na may ilang puntos na nagmula sa bawat serbisyo at ilang mula sa cache. Ang disk cache ay pinaghihiwalay ng modelo.

## 3 ter. Makasaysayang mga kaso

Ang Makasaysayan Nirerepaso ng case menu ang isang dokumentadong pagbubukas sa pamamagitan ng Open-Meteo [15] archive GFS na mga hula: ang mapa ay lumilipat sa lugar ng kaso, ang banda ay nagtatakda ng sarili nito at ang panahon ay nagreresulta ng mga dahon mula sa unang araw ng kaso. Sa pamamagitan ng masusing pagsasabi ng Update na ida - download mo ang mga larawan ng mga araw na iyon; ang kalkulasyon ng signal ay gumagana kung tungkol sa kasalukuyang hula. Inihahambing nito ang modelo sa kung ano talaga ang nangyari.

Ang unang kaso ay sa 14–15 Agosto 2022 na isinaad sa QST (Nobyembre 2022, "The World Above 50 MHz" [23]): isang pagbubukas sa ilalim ng isang mataas na presyon sa pagitan ng Manitoba at Kansas/Missouri, mula 144 hanggang 1296 MHz halos lahat sa FT8, na may record na Canadian sa 10 GHz ng 1228,56 km sa Q65 at mga signal hanggang 1941 km. Ang ikalawa ay ang pagbubukas sa pagitan ng 29 Disyembre 2021 at 2 Enero 2022 sa Tyrrhenian at sa kanlurang Mediterraneo sa 144 MHz, mula sa mga ulat ng IC8TEM: unang pagbubukas sa 29 (hanggang 737 km), mas malawak sa 31 ( IT9GSF – EB5EA 1217 km), malaking pagbubukas sa 1 Enero sa pagitan ng Italya, Sicily at Espanya. Para sa mga mas lumang kaso (hal. Setyembre 2000) ang arkibong Open-Meteo ay walang datos. Sa arkibo ay walang hangganang taas ng patong: doon ang pagtutuwid nito ay hindi kumakapit.

## 4. Ang indise at mga kulay

Sa bawat patong sa pagitan ng dalawang antas ay inihahambing ng programa ang N d drop sa mga stands ng mesa 6.5 ng Bean at Dutton [5]: halimbawa sa pagitan ng 1000 at 850 hPa isang pagbaba na umaabot sa 60 N/km ay normal, mahigit 100 N/km ay super-refraction. Ang isang super-refractive layer ay nagkakahalaga ng 10 hanggang 15 dB ng pakinabang; isang daluyan na sumisilo sa banda mula 20 hanggang 25 dB, ayon sa deficit ng M. Malakas na hangin sa lupa, ang mataas na limitasyong layer at mataas na CAPE ay binabawasan ang pakinabang, dahil inaalis o sinisira nito ang mga patong [5, § 4.4].

Ang kulay ay ang tinatayang pakinabang sa dB, na may takdang sukat na pantay-pantay sa lahat ng mga tema:

| Pakinabang | Colour | Kahulugan |
| Sa Ilalim ng 7 dB | Walang Sinuman | halos normal na atmospera |
| 7–10 dB | asul | Patag sa normal na kalagayan |
| 10–13 dB | berde | super-refractive layer |
| 13–16 dB | dilaw | matibay na layer, ngunit walang duct para sa banda |
| 16–18.5 dB | kahel | Napakatibay na suson o napakanipis na daluyan |
| 18,5–21 dB | pula | Conduit Traping Band |
| mula 21 dB | Pinagpala | Matibay at malalim na daluyan |

Sa pamamagitan din ng display box 3–7 dB zones sa pagitan ng 3 at 7 dB, kung saan may nabubuo ngunit nananatili sa ilalim ng scale, makikita mo sa pamamagitan ng isang light gray tension; ang mga kulay ay hindi nagbabago. Ang lugar na natatakpan ng sarili nitong hula at nagpapakita ng pinakamalaking nakikitang pakinabang: kung ito ay nasa ilalim ng 7 dB ang mapa ay nananatiling walang kulay sapagkat ang atmospera ay halos normal, hindi dahil sa ang impormasyon ay nawawala. Kapag nag-download ng isang bagong klik sa Update age ay hindi sumasabad; upang ihinto ito ay top.

Ang mga kulay ay opaque sa 82%, kaya ang background ng tema ay binabago lamang ang mga ito. Ang locators grid at square na mga pangalan ay idinisenyo sa ibabaw ng mga kulay. Kung saan wala roon ang iyong hula subalit ang Hepburn o F5LEN ay nagbibigay ng 5 o higit pang mga kard ng chess ay lumilitaw na may kulay ng pinagmumulang iyon.

Ang mapa ang humahatol sa bawat punto. Sa halip ay kinokontrol ng hudyat na ang suson ay nagpapatuloy sa ruta (parapo 9): ang ruta na tumatawid sa napakadilaw ay maaaring magbigay sa iyo ng kaunting dilaw na mapa.

## 4 bis. Mga hulang ipinakilala ng mga komersiyal: mga pasilyo (mula 1.3.3)

Patakarang Pankopya: Ang isang kawing lamang ay hindi dapat gumuhit ng isang padaluyan. Ang isang nakabukod at maikling pagtanggap ay maaaring ang pagsasabog ng isang transiting plane (ang pakete ng APRS ay napakaikli), isang pagbubukas ng ilang minuto o isang pagkakamali, at magbibigay ng "drop-conduct" na kanilang dinadaya. Kinikilala ng programa ang isang pasilyo nang, sa banda ring iyon:

- Di - kukulangin sa 3 pares ng nagsasariling mga istasyon (walang kabahaging istasyon) ang nagtatrabaho sa mga ruta na di - kukulangin sa 300 km;
- ang mga ruta ay magkakahilera: azimut sa loob ng ±15°, katamtamang puntos sa loob ng 150 km ng karaniwang axis;
- lahat sa nakalipas na 30 minuto, na ang sinag ay umuulit sa layo na hindi kukulanging 10 minuto (persistensiya);
- Para sa mga ARS, ang pagtalon ay mahalaga lamang kung ang parehong pagtalon ay inuulit;
- mula sa 1.3.4, ang mga ruta sa ibabaw ng 1500 km na pagbilang lamang kung ang sinag ay nagmula sa hindi bababa sa dalawang magkaibang mga pinagmulan (e.g. PSKReporter at isang kumpol): mula sa isang pinagmulan, hanggang sa mga distansiyang iyon sa 2 m, ay mas madalas na EME, nakakalat na meteoro o maling locator kaysa tropo.
- mula 1.3.4, huwag bilangin ang mga istasyon 'to ray': na sa huling oras ay may hindi bababa sa 5 kabalitaan sa 1000 km na kumalat sa isang tagahanga ng hindi bababa sa 150°. Sa 2 m, ang trope ay direksiyonal; ang isang receiver na "malaya" na magkakasamang Israel, Portugal, Inglatera at Italya mula 2000–3700 km, na kadalasang may positibong mga hudyat, ay halos tiyak na ang dalas na hindi maayos. Sa 17–22/11/2026 I5MZY Ang arkibong 72% ng residence sa 2 m mahigit 1000 km ay nagmula sa mga istasyon kaya; sa 22/11 sa 10:53 UTC ay OH6HLH, OZ1ITS, F4LUK, OM8ABC at SP2JLR, at kasama ng filter ang pinakamahabang pasilyo na bumababa mula 2175 km (regole 1.3.3) hanggang 615 km.

Pinagmulan: cluster spot (hindi AS/ACS), PSKReporter via MQTT, ang QSO nito at WSJT-X/JTDX/MSHV decodings, at APRS ay tumalon sa 2 m at LoRa 433/868 MHz.

APRS: 2 m at LoRa. Ang programa ay kababasahan ng APRS-IS sa read-lamang (rotate.aprs2.net, filter sa Europa) at kumukuha ng radio jumps sa pagitan ng mga kilalang istasyon ng lokasyon (sorgente → dispipeater → IGate), tulad ng VHF mapa ng DXView at APRS PropView (RF-YVY, Apache 2.0). May halaga ang tatlong banda: 2 m, LoRa 433 MHz (bandang pasilyo 432) at LoRa 868 MHz (corridoi na may bilang na 1296: isang padaluyan na nagdadala rin ng 868 MHz na pinakamataas na frequency). Ang banda ay mula sa talaan ng LoRa tocal (aprs-deviceid) at mula sa frequency o mga salita sa komento; ang pagtalon ay nagkakahalaga lamang sa pagitan ng dalawang istasyon ng parehong banda.

Ang maselan na punto ay ang mga aktibong umuulit: isang signal na natanggap sa 500 km ay maaaring dumating na may dalawa o tatlong mga dispipeter retransmission. Kaya ang pagtalon ay mahalaga lamang kung ito ay sa pagitan ng dalawang tunay at magkasunod na transmiter:

- Kailangang matunton ang landas: bawat naghuhukay na may pangalang retranspender ay naglagay ng pangalan nito, at ang pagtalon ay sinusukat sa pagitan ng isa at kasunod; isang alyas na ginagamit nang walang pangalan (W IDE1 *, W IDE2 *, regional alyas) ay nababasag sa kadena at ang kahabaang iyon ay hindi nabibilang;
- Natatagong mga retransmission: naaalaala ng programa ang landas na karaniwang itinatanong ng bawat mapagkukunan (halimbawa ang W IDE2 -2). Kung ang isang pakete ay dumating na may lundag sa mas kaunti (W IDE2 -1) at walang pangalan ang nagpapaliwanag nito, muling kukunin ito ng isang tagahukay nang hindi sinusubaybayan at ang pakete ay itinatapon; ang mga mapagkukunan ay hindi kailanman nakita noon;
- Argipeater sa gitna ng daan: kung sa pagitan ng dalawang istasyon, humigit-kumulang sa daan, mayroong isang tagahukay ng parehong aktibong banda sa huling tatlong oras, ang pagtalon ay itinatapon, dahil maaari itong muling mag-transmit (ito ay lalo na para sa tagahukay na si LoRa na nagreresulta nang hindi binabago ang landas);
- Ang mga di-radio package (qAC, qAX, TCPIP), ang mga pinapatakbo ng internet sa radyo mula sa isang IGate (ikatlong partido), HF at iba't ibang frequency, mga istasyon sa taas na mahigit 1500 m (palloni, sasakyang panghimpapawid) at tumalon sa mahigit 1500 km.

Epekto sa mapa (mula 1.3.4). Hinahatak lamang nito ang lugar kung saan may bisa ang minimum na garantiya: isang pangkat ng ±60 km sa paligid ng gitnang bahagi ng bawat ruta (mula 15% hanggang 85% ng haba nito), hindi sa mga istasyon sa mga dulo, kung saan maaaring wala roon ang padaluyan. Ang mga bigkis ng lahat ng mga pasilyo ay magkakadugtong, sa isang neutral na kulay (ang teksto) at semi-transparent: ang mga magkasanib ay hindi nagdadagdag, at ang kulay ay hindi nakakalito sa ube ng sukat (21 dB at lampas pa). Ang mga etiketa (mga etiketa, katamtamang distansiya, pinakakaunting pakinabang) ay sinusuri sa isa't isa; lumilitaw ang mga ruta ng pasilyo na dala ng daga.

Sa lugar, at malapit lamang sa kasalukuyang panahon (imperyo90 minuto), ang paghula ay hindi nababawasan. Ang pangangatuwiran ay: kung ang mga istasyon ay talagang daraan, naroon ang propagation, anuman ang sabihin ng modelo ng panahon.

Dahil maliit lamang ang halaga ng mga maiikling ruta (mula 1.3.4). Sa 2 m, sa pagitan ng 300 at 600 km, ang dalawang mahuhusay na istasyon (mga daang watt, antena, FT8 o CW) ay nag - uugnay halos araw - araw kahit na sa normal na atmospera, para sa troposcatter: ang hudyat na ikinakalat ng maliliit na depekto ng hangin sa iisang lakas ng dalawang bungkos. Tatlo o apat na batik - batik sa 500 km ang nagsasabi na ang banda ay gumagana na gaya ng dati, hindi dahil sa may pambungad: Tinataya ng GEKO Magazine n. 69 ( IU3QEZ) para sa troposcatter na may 2×10 dumadaloy na elemento ng halos 440 km sa SSB QRP, 510 km sa SSB na may 50 W, 600 km sa CW QRP at 640 km sa SSB na may 500 W (sources, labing-isang lap). Mahigit lamang sa 600–700 km ang troposcatter ay hindi na sapat at ang isang serye ng mga magkakahanay na koneksiyon ay talagang nagpapahiwatig ng isang padaluyan o patong. Hanggang 1.3.3 ang isang pasilyo na may 450–600 km ay itinaas ang hinulaan sa 9–12 dB, at ang mapa ay nagpakita ng isang pagpapabuti na wala roon.

Ang halaga kung gayon ay depende sa katamtamang distansiya ng mga ruta:

- hanggang 400 km: 3 dB;
- mula 400 hanggang 700 km: asin sa linyang daan mula 3 hanggang 10 dB (500 km → 5,3 dB; 600 km → 7,7 dB);
- mula 700 hanggang 900 km: mula 10 hanggang 16 dB; mahigit 900 km ang natitira hanggang 16 dB.

Sa ilalim ng 7 dB, na nasa ilalim ng simula ng scale, ang minimum ay nananatili na: ang pasilyo ay iginuhit na may tatak nito, nakikita na ang banda ay gumagana, ngunit ang mga kulay ng mapa ay hindi nagbabago (sa pamamagitan din ng kahon ay nagpapakita ng 3–7 dB na kulay abong tuldok). Sa ibabaw ng 7 dB ang halaga ay tinitimbang, kalahati at kalahati, na may bilang ng mga independiyenteng ruta (3 stroke → 0.25; 6 o higit pa → 1) at kasama ang median S/N ng mga relasyon (−20 dB → 0.5; 0 dB o higit pa → 1; hindi kilala, tulad ng cluster spot, → 0,75), simula 7 dB. Mga halimbawa: 3 rutang walang S/N sa 500 km → 5,3 dB; 5 ruta sa 615 km → 7,7 dB; 3 ruta na walang S/N sa 900 km → 11.5 dB; 3 ruta sa −20 dB sa 900 km → 10,4 dB; 6 ruta sa 0 dB sa 900 km → 16 dB.

Ang minimum na distansiya ay nananatiling 300 km: ang pag-angat nito sa 500 km ay mag-aalis din ng tunay na short-range openings (halimbawa ang isang padaluyan sa Tyrhenian sa 400 km, na kinikilala sa pamamagitan ng weather age at napakalakas na mga signal higit sa mula sa malayo lamang). Sa arkibong I5MZY, sa 22/11/2026 sa 10:53 UTC, na may tuntuning ito at ang ray filter sa mas mahabang mga pasilyo ay 615 km (7.7 dB) at 540 km (6.3 dB); na may mga tuntuning 1.3.3 naabot nito ang 2175 km at 16 dB.

Hanggang 1.3.3 ang minimum ay tama sa lahat ng regulatory ng pasilyo at umaasa lamang sa layo (mula 10 dB hanggang 300 km hanggang 16 dB hanggang 900 km): na may maraming mahahabang pasilyo na magkakasama (halimbawa ay sa 22/11/2026, labindalawang koridor hanggang 2620 km) ang nag-angat sa halos lahat ng Europa hanggang 16 dB.

Ang natamong mga kurba ay hindi na nagdurulot ng «gocceić: ang mas maliit na mga pulo ng 220 km sa parehong direksiyon ay nananatiling may kulay ngunit walang nakasarang kurba, maliban na lamang kung ang isang pasilyo ay nagpapatunay sa mga ito. Ito ay nagreresulta sa pamamagitan ng "Prevision interced by the spots".

## 4 ter. Live sensor: FM-DX webserver at AIS receiver (mula 1.4)

Dalawa pang pinagmumulan ng tunay na mga obserbasyon, na pumapasok sa mga pasilyo ng banda ng 2 m at ang paghatol ng pagkamaaasahan (§ 10 septies). Binubuksan nila at binubuksan ang kahon ng tab Details.

Webserver FM-DX. Maraming remote reception extest sa FM (88–108 MHz) ay nag-iingat ng isang internet-nected receiver sa FM-DX Webserver free program (GPL-3.0); Ang talaang pampubliko ay mga server.fmdx.org [28]. Ang bawat server ay nagsasabi, sa pahina /api, ang naka-redirect na frequency, ang signal sa dBf at, kung ito ay nag-decode sa RDS, ang transmitter na kinikilala sa database nito na may layo at azimut. Ang programa:

- download ang listahan tuwing 30 minuto at panatilihin ang 60 pinakamalapit online servers sa loob ng 1500 km ng QTH;
- Isa - isang sinusuri ang mga ito, na kumakalat sa loob ng 3 minuto, na may sukdulang oras na 8 segundo (walang server na tumatanggap ng mahigit sa isang kahilingan sa bawat 3 minuto);
- panatilihin ang pagtanggap kapag ang transmitter ay 150 km o higit pa at ang signal ay hindi bababa sa 10 dBf; ang posisyon ng transmitter ay hinango mula sa server, azimuth at ang distansiya.

Ang banda ng FM ay mas mababa sa 2 m, at ang mga duct na nagdadala nito ay hindi palaging nagdadala ng 144 MHz; ngunit ang isang FM reception sa 300–600 km sa labas ng abot-tanaw ay halos palaging tropo (o, sa tag-init, Elast, na sa 100 MHz ay umaabot sa higit 900 km: dahil dito higit sa 1500 km ang pamamahala ng mga pasilyo ay nagtatanong ng dalawang magkaibang mapagkukunan, § 4 bis). Sa mapa ang mga reseptor ay mga linyang lila "FM n km"; sa status bar ay bar ang konde, na may listahan sa komiks.

ISIS mula sa iyong receiver. Ang mga barko ay naghahatid ng kanilang posisyon sa 161,975 at 162,025 MHz (ITU-R M.1371 [31]), halos kapareho ng 2 m. Isang AIS receiver (A, rl-ais, isang dedikadong receiver) na pasulong sa mga pariralang NMEA! Binabago ng AIVDM sa UDP sa daungan 10110 ang bawat barko sa malayo tungo sa isang windping sensor: ang programang decodes position message (type 1, 2, 3, 4, 18, 19 at 21, kahit na sa maraming piraso) at inihahambing ang distansiya sa normal na abot - tanaw ng radyo

```math
d_h = 4{,}12\,(\sqrt{h_{nave}} + \sqrt{h_{rx}})\ \text{km}
```

na may taas ng antena ng barko na tinatayang mula sa tipo (20 m para sa mga barkong pangkargamento, 30 m para sa mga pasahero, 6 m para sa pangingisda at kasiyahan, 10 m para sa iba) at ng antena ng receiver set (basic 10 m). Ang isang barko na higit sa 1.5 beses sa abot-tanaw at higit sa 50 km ay isang respektibong sa pamamagitan ng padaluyan: ito ay lumilitaw sa mapa bilang isang asul na linya mula sa QTH na may isang ugong «AIS n km». Ito rin ang ideya ng pag-aaral ng CSIRO [10], na gumamit ng 162 MHz AIS receptions upang matiyak ang prediksiyon ng GFS pipelines. Ito'y pinapatay nang pangunahin: ito'y nagsisilbi sa isang wastong tagatanggap.

## 5. Mga mapagpipiliang linya at paghahambing sa presyon at pagbaligtad

Magtamo ng mga kurba: mga linya sa 7, 10, 13, 16, 18,5 at 21 dB, ang mga gilid ng sukatan, na iginuhit sa larangan na pinakinis ng pamamaraan ng mga liwasang pangmartsa, na may tatak sa dB.

Isobare: presyon sa antas ng dagat tuwing 4 mula sa parehong GFS, hanggang sa oras ng paghula, na may mga sentrong H at L.

Thermal inversion: ang mga kurba ay natatakpan ng kung gaano karaming hangin sa 925 o 850 hPA ay mas mainit kaysa sa 2 m, sa +1, +3, +5 at +8 °C.

Sa ilalim ng mga kontrol ang isang linya ay nagbibigay ng kasunduan sa pagkakamit sa presyon at sa pagbaligtad sa nakikitang lugar: Pearson correlation codict, −1 hanggang +1. Ang mga duct ay kadalasang ipinanganganak sa mga antisiklono (ang hangin na nagpapababa ng mga init at bumubuo ng baligtad), kaya ang isang positibong kasunduan sa presyon ay normal; ngunit sa loob ng parehong anticyclone ang pakinabang ay nagbabago nang malaki sa pagitan ng dagat, baybayin at mga bundok. Ang baligtad lamang ay hindi sapat: ibaluktot lamang ang mga alon kung sa ibabaw ng hangin ay din ang dryer [5]. Nakikita na ngayon ng linya kung alin sa dalawa ang pinakamahusay na nagpapaliwanag sa mga lugar na may kulay. Ang katulad na paghahambing ay nakasulat sa rehistro.

## 6. Posibleng mga tubo: dobleng kalkulasyon

Ang antas ng GFS ay mula 200 hanggang 1000 m: ang isang manipis na inversion ay kumakalat at ang modelo ay hindi nakikita ang padaluyan. Kung sa pagitan ng dalawang antas ang N collection ay lumampas sa normal (40 N/km), ang hangin ay matatag (thermal judge sa ilalim ng 3 K/km) at sa itaas ay tuyo (specific shadience ratite sa ibaba 0.8), isinasaalang-alang ng programa ang isang potensiyal na duct at kinakalkula ito sa dalawang paraan:

- Pagkalkula S: ang lahat ng labis na pagbaba sa 50 m.
- Pagkalkula B: ang parehong pagbawas na ipinamahagi sa karaniwang kapal ng mga tubo ng lugar na iyon, na kinuha mula sa mga mapa ng klima ng ITU-R P.453 [4] (sa lupa o nakaangat).

Para sa deficit at para sa sukdulang nabitag na wavelength ang pinakamababang halaga ay ginaganap: ito ay isang matalinong tantiya.

Ang ITU-R P.453 data ay © ITU at maaaring gamitin para sa personal at di-commercial na mga layunin sa pamamagitan ng pagbanggit sa pinagmulan, ngunit hindi maaaring muling i-istributed: dahil dito wala ako sa pakete ng programa. Upang buksan ang kalkulasyong B, i-upload lamang ang mga mapa ng P.453 mula sa ITU sa tab: ang programa, pagkatapos ipakita ang mga kondisyon ng paggamit, i-upload ang arkibo ng mga tubo mula sa ITU site (elevated_and_surface_duct.zip) para sa personal na gamit ng gumagamit, kunin ang mga file sa Data/ P453 at buksan ang kalkulasyon nang walang rebooting. Sa alternatibong paraan, download mula sa ITU-R Study Group 3 digital product page (itu.int, ITU-R Study Group 3, Digital na mga produkto, voice P.453, Surface data at standing duct data) at ilagay ang pitong S_OCCUR. TXT, S_THICK.TXT, S_STREN. TXT, E_OCUR.TXT, E_base.T Kung wala ang mga file, ang programa ay gumagamit lamang ng kalkulasyong A at isinusulat ito sa tab at sa rehistro. Ang katumbasan sa pagitan ng deficit A at tipikal na intensity P.453 ay anomaly: Kung ilang beses na mas malakas ang tubo sa ngayon kaysa sa normal. P453

## 7. Mga Istasyon at mga Lugar

Ang kabesera ay maaaring magkaroon ng 4, 6, 8 o 10 karakter. Sa 8 o 10 karakter ang posisyon ay tumpak at ang lupang kabahagi ay nagmula kay Copernicus GLO-30: ang taas ng antena ay sapat na mula sa lupa. May 4 o 6 na karakter ang liwasan ay napakalaki: mas mainam na isulat ang koleksyon ng antena sa dagat; kung 0 ang programa ay nananatiling taya na may lupang SRTM sa gitna ng liwasan pati na ang taas nito mula sa lupa at hudyat ito sa mga resulta. Para sa katumbas ang default na taas ay 3 m mula sa lupa; na ang sea share 0 ay ginagamit SRTM na lupa at taas.

## 8. Lupa, diffraction at troposcatter

Ang profile ng ruta ay sampol sa Copernicus GLO-30 kada 30 m (mahigit sa 120 km ang lapad). Ang katumbas na Earth circle ay k = 157/(157 + ΔN), na may ΔN ang pagbaba ng N sa unang km mula sa profile ng GFS sa kahabaan ng ruta ng [6, 7].

Mga relief na malapit sa mga istasyon (mula 1.3.2, pamantayan ng awtor). Ang P.452 ay nagbabayad sa abot - tanaw na anggulo na li halos, 0.573 dB sa bawat mrad: ang ginhawa ng ilang sampu - sampung metro hanggang ilang daang metro mula sa antena ay nagkakahalaga ng sampu - sampung bahagi ng dB, higit pa sa nakikita. Dahil dito ang mga relief sa loob ng 20 km mula sa bawat istasyon ay maaaring magdagdag hanggang 6 dB: ang abot-tanaw na anggulo dahil sa mga ito ay limitado lamang sa pinakamalayong reliefs plus 10,5 mrad. Ito ay may bisa para sa iyong istasyon at para sa kabalitaan. Ang linya na « « « ay nagsasabi kung ilang dBs ang umalis.

Pinagmulan ng lupa (mula 1.3.2). Ang mga kota ay galing kay Copernicus DEM GLO-30 [3]: 1 pangalawang arc, ang dagat ay 0 at ang baybayin ay malinaw. Hanggang 1.3.1, ginamit ng programa ang mga kahon ng AWS sa Terrain Tiales. Malapit sa baybayin ang mga liwasang iyon ay humahalo sa SSRTM sa mababang resolution seashore data at nagpalaganap ng bahagi ng isla sa dagat: sa paligid ng Capri ay lumilikha ng multong singsing na 80–100 m sa pagitan ng 0.5 at 1 km mula sa dalampasigan. Noong 21 Nobyembre 2026 ang IC8TEM ay nag-ulat ng isang hindi maasahang abot-tanaw sa hilagang-kanluran; ang punto-by-point na paghahambing kay Copernicus ay nagpatunay nito (94 m sa dagat sa hilagang-kanluran ng Marina Grande vs. 0). Ang maling abot - tanaw na iyan, ng +2,6°, ay nagkakahalaga ng mga 26 dB ng troposcatter sa mga istasyon sa baybayin. Ang skadi panes ay nananatiling reserba lamang kapag hindi tumugon si Copernicus. Ang mga balangkas ni Copernicus ay may timbang na 15–30 MB at na-download ng isang beses; ang tanging dagat ay hindi umiiral at nagkakahalaga ng 0.

- Libreng espasyo: Lfs = 92,44 + 20 log f(GHz) + 20 log d(km) [8].
- Diffraction: Bullington method sa tunay na profile platform ITU-R P.452-16 § 4.2 [6] delta-Bullington round land refruction, na may ITU-R P.526 [9] circle Los.
- Unang sonang Fresnel: reclusion √(λ·d1·d2/d); malaya kung ang lupa ay nag-iiwan ng hindi bababa sa 0.6 ng radiasyon. Sa bawat hadlang, ang [9] Knife Board V at J(VV) parameter ay ipinapakita..
- Sa disenyo ng profile ang direktang paikot ay berde sa optical at pulang tanawin kung ito ay dumaraan sa ilalim ng lupa o sa ilalim ng kurba ng lupa; ang mga pulang tuldok ay ang mga hadlang na sumasalakay sa 1st area ng Fresnel (puple dotuted line). Sa dagat ang "red point" sa taas na 0 m ay hindi isang bundok: ito ang dagat na tumataas para sa kurba. Ang pagpapasa ng daga sa paikot o sa isang punto ay lumilitaw na isang komiks na may paliwanag at mga pamantayan.
- Kapag ang isang relief malapit sa antena ay nagsara ng direksiyon (halimbawa ang isang abot-tanaw ng sampu-sampung digri), ang diffraction at troposcatter ay nagbibigay ng pagkawala ng daan-daang dB: sa ibabaw ng 300 dB ang programa ay nagsusulat ng "> 300 dB" at "direction na isinara ng isang kalapit na hadlang", ang signal na "< −230 dB" at ang S/N ratiolates "−60 dB". Kung gayon, makabubuting tingnan ang «Recommended plantific at «Via monte».
- Troposcatter: ITU-R P.452-16 eq. 45 hanggang 50% ng oras ng [6], na mayular na distansiya mula sa mga abot-tanaw at N0 mula sa mga profile.
- Walang tropo: kabuuan ng mga kapangyarihan ng diffraction at troposcatter, hindi kailanman sa ilalim ng malayang kalawakan.

## 8 bis. Troposcatter: Apat na modelo at kawalang - katiyakan

Kasunod ng P.452 kinakalkula ng programa ang troposcatter na may tatlong iba pang modelo, gayon din ang programang TropoLink [16], at ipinapakita ang pagkakaiba ng mga ito bilang kawalang katiyakan:

- NBS TN101 § 9 [25] (mula 1.3.5), median: L = 30 log f − 20 log d + F(θ) − F0 + H0 + Lgp. F(b.gd) ay ang attenuation function ng Annex III sa kanyang mababang analog na mga anyo (Ns 301) na may reflection para sa Ns, H0 ang madalas na pagkakamit ng § 9.2: sa 144 MHz, na may mga mababang antena sa mga wavelength, na may repleksiyon mula sa mga bahagyang distribuhostikal na ds ng field sa 310. Ang Ns ay ang oras na GFS profile, ang epektibong mga abot-tanaw at taas ay nagmula sa profile ni Copernicus, Lgp = 0.07·e^(0,055(Gt+Gr)) ay ang panimulang episodyong pagkawala. Ito rin ang anyo na ginagamit ng ITM sa loob nito, ngunit may reaksyon sa ngayon sa halip na sa klima. Sinusuri nito ang mga patag na seksiyon sa 144 MHz na may antena sa 20 m: sa loob ng 2–4 dB mula ITM sa pagitan ng 150 at 900 km at mga 2 dB sa pinasimpleng anyo na 9.2b ng TN101, na walang H0.

- ITU-R P.617-5 [17] L = F + 22 log f + 35 log θ + 17 log d + Lc, na may f sa MHz, θ sa marad, d sa km, F = 0.18· N0 ·e^(−hs/7,35) − 0.23·ΔN at Lc = 0.07·e^(0,055(Gt+Gr). Ang N0 at ΔN ay karaniwang kinukuha mula sa mga mapa ng klima ng ITU; dito ay ginagamit natin ang mga profile ng GFS ( N0 ·e^(−hs/7,35) ay ang reactivity sa lupa, ΔN ang pagbaba sa unang km), kaya ang troposcatter rin ang sumusunod sa panahon.
- RSGB VHF/UHF Handbook Formula, DL2FZN [18]: L = Lfs + 24,44 + 4,283 ln f + 0.06656 d. Ang dalisdis na may layo ay halos katumbas ng PROPBER (0,067 dB/km).

Ang karaniwang tomo (mula 1.3.1): ay ang punto kung saan ang paikot ng dalawang istasyon ay tumatawid, bawat isa ay nakahilig sa abot - tanaw na anggulo nito, sa isang Lupa na katumbas ng paikot na ae = k·6371 km. Mula sa TX ang radiasyon ay tumataas bilang z = hTX + x·θt + x2/(2·ae); Ang distansiyang x = (hRX − hTX + d^ θ + d2/(2·ae) / θ, na may θ = d/ae + θ + +r. May patag na abot-tanaw at antena sa parehong kota ang base ay nagkakahalaga ng d2/(8·ae): mga 1.3 km hanggang 300 km, 3.7 km hanggang 500 km, 9,4 km hanggang 800 km. Mientras mas mataas ang volume, mas maraming tuyo at bihira ang hangin at mas kaunti ang kumakalat ito: sa kadahilanang ito mahigit 800–1000 km ay nauubos ang troposcatter. Sa mga resulta ay ang parte at posisyon ng base, reactivity, ang scaping at ang kaumiduhan ng GFS sa taas na iyon, at ang katumbas na layo ng Bray (QST, Nobyembre 1961): ang bawat ekstra o hindi gaanong abot-tanaw ay nagkakahalaga ng mga 111 km ang layo. Dependiyaryong reperensiya: ang representasyong "Beyend the preview in VHF" ni IU3QEZ (GEKO Magazine n. 69).

Ang Troposcatter line, ang iba pang mga modelo ay nagpapakita ng dalawang pagkalugi at kawalang katiyakan bilang kalahati ng pagkakaiba sa pagitan ng pinaka-optimikong modelo at ang pinaka pesimista. Ang mga ito ay mga paghahambing lamang: ang kawalan na walang tropo ay nananatiling sa P.452.

## 8 ter. hapon troposcatter sa mga relief (mula 1.3.4)

Ang IK3XTV, mula sa Thiene ( JN55, na may Alps sa 10 km), ay gumagana sa mga hapon ng tag-init sa timog-silangang Alemanya sa 400–500 km, nang hindi nakikita, na may malakas na mga hudyat ngunit may mabilis at malalim na evancence; ang mga ito ay ipinalagay ng may akda na ang mga ito ay mula sa "convective cells na nakahanay bilang isang tren ng mga lente". Ang obserbasyon ay pinatutunayan; ang paliwanag na pinagtibay sa programa ay isa pa, mas matatag sa pisikal.

Sa hapon ay pinupukaw ng araw ang hangin sa ibabaw ng mga relief at ang limitasyong layer ay tumataas sa 2–3 km. Sa itaas nito ang mahalumigmig na hangin sa ibaba ay nagtatagpo sa tuyong hangin sa itaas: doon ang pagyanig ng refractionive index (ang laki na namamahala sa troposcatter) ay sukdulan. Kapag ang tuktok na iyon ay umabot sa karaniwang dami ng ruta, na sa 400–500 km ay halos 2–3 km, ang troposcatter ay pinatitibay. Ipinaliliwanag nito ang oras sa hapon, mataas na presyon (tahimik na langit, matinding pagpapainit) at mabilis na paglaki, na karaniwan sa diffup at hindi sa mga duct. Ang mga selulang nag - iingat ay hindi nagsisilbing mga lente: lumilikha ng pagyanig, at ang pagyanig sa ibabaw ng recast layer ang siyang nagkakalat ng signal.

Mga resulta ng pagbabasa ng mga hula:

- Sa lupa ang unang hapon ay siyang pinakamasamang panahon para sa mga mababang dukto at mga patong-patong, nguni't hindi para sa troposcatter sa kabila ng mga bundok: ang dalawang bagay ay dapat basahing magkahiwalay;
- ang programa ay mayroon nang dalawang datos na nagsisilbi, ang taas ng limitasyong layer na inilaan ng modelo at ang bahagi ng karaniwang dami ng ruta (register "Common Volume" sa mga resulta): Pagka sila'y malapit na, ang isang pagkain sa bundok sa hapon ay mas mabuti kaysa kalkulasyon sa normal na atmospera;
- mula sa 1.3.4 ang punto-point na kalkulasyon ay isinasaalang-alang: kung ang karaniwang volume ay nasa lupa, sa kahabaan ng ruta ay may mga relief ng hindi bababa sa 800 m, ang araw ay higit sa 15° at ang hangganang layer ay lampas sa 800 m, at ang tuktok ng layer reshaping (model soil + + limitasyong patong na taas) ay nasa loob ng 500 m mula sa base ng common volume, ang troposcatter ay nakakaabot sa 3B; sa pagitan ng 500 at 1000 m ang linya ay bumababa sa sero. Ang linya «Troposcatter pomeridiano». Dahil sa niyebe sa kahabaan ng ruta (hindi bababa sa 10 cm sa lupa ayon sa modelo, ang niyebe_depth variable ng Open-Meteo o SNOD ng GFS mula sa NOMADS) ay hindi kumakapit: sa niyebe ang lupa ay nananatiling malamig at ang re-shaped layer ay hindi lumalaki. Ang halaga ng 3 dB ay probisyonal at dapat na may kasamang tunay na mga kawing; ito'y nagsisimula sa mga pagtutuwid sa lagay ng panahon.

Mga napagkunan: mga obserbasyon ni F. Egano IK3XTV (Radio Rivista e www.qsl.net/ik3xtv, «Propagazione sulle 2 metriich, «Celle convetiveić, «Troposcatter sa Alps»), mga pinagkukunang § 13.

## 9. Ang unang suson sa ruta

Ito ang pinaka - puso ng pagtaya sa hudyat. Ang mga profile ng GFS ay dinadala sa ruta tuwing mga 25 km. Sa bawat patong ng presyon, ang pinakamahabang kahabaan ng parehong patong ay matatagpuan sa mga magkasunod na puntos; binibilang bilang duct lamang ang mga makapal na patong ng hindi bababa sa 40 m [10].

- Pagpapatuloy: mula sa kung anong km hanggang sa anong km, at ang maliit na bahagi ng ruta;
- Itaas at ibaba, at hilig sa m/km;
- lupa sa loob ng patong: ang km kung saan ang mga bundok ay tumataas sa ibabaw ng patungan;
- Takpan ang lupa: sa dagat ang padaluyan ay halos palagi, magpasiya sa kahabaan ng lupa [10]. Kung ang lupa ay lumampas sa 10% ng ruta, ang pragmentong ginagamit ay ang pinakamaliit sa pagitan ng tuluy-tuloy at sakop ng lupa.

Ang trope na pakinabang ay ang pakinabang ng patong para sa ginagamit na praksiyon. Kung ang higit sa kalahati ng mga profile ay sub-refractive ang pakinabang ay negatibo.

## 9 bis. Mga pagtutuwid sa lagay ng panahon: katatagan, paghupa, pagsalakay

Mula 1.3.0 ang profile ay nagbibigay rin ng tatlong meteorolohikal na impormasyon, na makikita sa rehistro (reveve «stability (Misme)»), sa Meteo line sa ruta ng mga resulta at, kasama ang Segni meteo box, sa mapa.

- Katatagan ng Misme 5, Kabanata 6]. Ang misme ay nagbibigkis sa field na lampas pa sa abot - tanaw tungo sa isang compound parameter: M = a·(ge − 40 + b·ΔW), na may = 0,5 dB sa bawat N/km, ang katumbas na quarter ge at ang thermal na katatagan ΔW, ang gawain upang itaas ang hangin ng 1 km. Kinakalkula ng programa ang ΔW = g·∫(θ −)/θ0 dz sa pagitan ng lupa at 1 km sa itaas, na may θ potensiyal na temperatura; ang pamantayang atmospera ay mga 56 J/kg. Ang termino na idinagdag sa pakinabang ay 0.07 dB para sa J/kg na lampas sa pamantayan, limitado sa pagitan ng −4 at +6 dB, at mahalaga lamang kung saan walang probinsyang sumisilo sa banda (ang tsonggo ay nasa indise na ng Bean at Dutton). Pansamantala lamang ang calibration: dapat itong mapatunayan sa mga tumbasan (parapo 14).
- Subsidence at uphill air [19]. Moler at Holden, sa mga rutang 125–305 km patungo sa dagat sa edad na 1365 at 9380 MHz, ay natagpuan ang pinakamataas na signal ng mga 2.5 dB sa katamtaman na may hanging bumababa na may pagtaas ng hangin, at sa isang kaso ay isang pagpapatibay ng mga 20 dB sa loob ng 3 oras sa simula ng paghupa, na sinundan ng pagbaba ng 22 dB kapag ang hangin ay bumalik upang magtipon. Ang GFS ay nagbibigay ng patayong bilis sa 850 at 700 hPa. Kung sa parehong antas ang hangin ay bumababa ng higit sa 1 cm/s at may reversal o matalas na pagbaba sa potensiyal na refractive index K (Ang N ay iniulat na adyabatly sa 1000 hPa, na nagtatampok sa tuyo at mainit na hangin na bumababa mula sa itaas na [5]), 1.5 dB ay idinaragdag; kung ang hangin ay tumaas ng higit sa 5 cm/s kung ang mga ito ay lumipad. 2.
- [20] Scirocco. Ang mainit at tuyong hangin mula sa Sahara sa mas malamig na hangin sa dagat ay nagbibigay ng napakalakas na pagbaligtad mula sa silangang Mediteraneo tungo sa Gibraltar. Ang S sign ay lumilitaw kung saan sa 850 hPA ang hangin ay nagmumula sa 100–200° na may hindi bababa sa 7 m/s at ang temperatura ay hindi bababa sa 16 °C, sa Mediteraneo. Ito'y pahiwatig lamang: ang pakinabang ang siyang nagpapasiya sa mga suson ng profile.

Kung walang padaluyan na sumisilo sa banda ang kabuuang pakinabang ay hindi hihigit sa 16 dB, ang dulo ng dilaw: ang mga pagtutuwid ay gumagawa sa mas matibay na mga suson na mas maliwanag ngunit hindi ito ipinapasa sa pamamagitan ng mga duct (mapula at lila ay nananatili sa mga dukto).

- Ang pagtitiyaga (mula 1.3.1). Ang pinakamalaking mga pagbubukas ay dumarating pagkatapos ng mga mahusay na high-pressure days (VHF/UHF DX Book; Biancomano at Leyshon 1998, pamamahala ng 12 oras; ang mga ulat ng 29/12/2021–2/1/2022 ay lumago mula sa una hanggang ikaapat na araw). Sa bawat selula binibilang ng programa ang mga oras ng naunang 24 na may pakinabang na hindi bababa sa 13 dB at nagdaragdag ng hanggang 2 dB ayon sa proporsiyon, kung saan lamang ang pakinabang ay hindi bababa sa 10 dB.

Kung paano babasahin ang mga tanda sa mapa (Change Weather Signs; mula 1.3.2 ang sagisag ay malaki na doble at ang pagpasa sa mouse sa isang karatula ay lumilitaw na isang komiks na may paliwanag at mga pagpapahalaga ng selula):

| Tanda | Colour | Ano ang ibig sabihin niyan? | Epekto sa trope |
|---|---|---|---|
|  ↓  | berde | Paghupa: Ang mga patak ng hangin (anticyclone), init para sa compression at dry, ay nagpapalakas sa pagbaligtad at nagpapanatili ritong matatag | pagsang - ayon, +1.5 dB |
|  ↑  | pula | hangin sa itaas (harap, mababang presyon, convection): paghaluin ang mabababang suson at basagin ang mga version at duct | Hindi kanais - nais, −2 dB |
|  S  | kahel | Syrup: mainit at tuyong hangin mula sa Sahara sa ibabaw ng sariwang hangin sa dagat | mabuting himaton, walang pagtutuwid: magpasiya sa mga suson |

Ang isang lugar na punô ng berdeng ↓ sa ilalim ng isang anticyclone ay ang klasikong tanda ng isang matigas na butas; ang mga Red ↑ pana sa kahabaan ng linya ay nagsasabi na ang mga patong ay nasisira, bagaman ang kulay ng selula ay mabuti pa rin.

Binitiwan ng kahon ng Weather Correctles ang mga pagtutuwid na ito upang makita ang modelo ng 1.2.

Defessionation (parapo 14): sa 593 koneksiyon sa 144 MHz sa pagitan ng 29 Disyembre 2021 at 2 Enero 2022, kumpara sa parehong mga ruta at dalawang linggo mamaya, ang indise ng paghihiwalay (0,5 = wala, 1 = perpekto) ay 0,79 walang mga pagtutuwid at 0,91 na may mga pagtutuwid (bago ang bubong sa 16 dB). Ang parehong paghahambing ay nagpapakita ng pangunahing hangganan: ang padaluyan sa dagat sa mababang altitud, na noong mga panahong iyon ay nagdala ng mga palatandaan sa pagitan ng Campania at Liguria, ay hindi nakikita sa mga antas ng GFS.

## 9 ter. Balita 1.3.2: dalawang tantiya, kisame, mataas na presyon, dagat, istasyon, ridge

Matalino at agresibong pagtaya. Ang pakinabang mula sa scaffold ay lumalaki mula 0 dB hanggang sa hangganan ng normal hanggang 10 dB hanggang sa pagsisimula ng super-refraction. Sa 1000–850 patong, ang maingat na pasukan ay Bean at Dutton (100 N/km); ang agresibong pasukan ay ang klasikong Super-Refraction stand ng 79 N/km (k ≈ 2; ang P.453 ay gumagamit ng −100 N/km sa unang 100 m), na may k50 = 157/(157 −) ay k≈ 2 (k = 2,5 ay katumbas ng mga 94 N/km). Ang punto-point na kalkulasyon ay nagpapakita ng dalawang pagkalugi na magkatapat; na may isang padaluyan na sumisilo sa banda na magkatapat. Ang mapa ay gumagamit ng pag - iingat.

Attic ng 2500 m. Ang mga daan at duct ay hinahanap lamang hanggang 2500 m sa ibabaw ng lupa: ang mas mataas na antena ng mga baguhang radyo ay hindi nagsasamantala sa mga ito. Dahil sa 1500 m ang ekwasyon ay nawalan ng punto ng paghihiwalay at 4% ng mga koneksiyon sa itaas ng 16 dB, dahil sa taglamig ang mga inberhensiya mula sa pagpapahupa ay kadalasang nasa pagitan ng 1500 at 2500 m.

Umabot ang matinding panggigipit. +1 dB kung saan ang hindi bababa sa 7 ng 9 na selula sa paligid ay nabawasan ang presyon sa dagat ng 1020 hPa o higit pa at ang hangin ay mahina sa mababang mga patong (10 m sa ilalim ng 5 m/s, 850 hPa sa ilalim ng 8 m/s), maliban sa pag-akyat ng mga galaw.

Bigat ng dagat. Sa stain-point kalkulasyon, ang pakinabang ng mga patong ay pinarami ng 0.8 + 0.4·μ1, kung saan ang μ1 ay ang salik ng ITU-R P.452 (eq. 51) sa pinakamahabang tuluy-tuloy na kahabaan ng lupa: 1 sa laot ng dagat, maliit na may mahabang kahabaan ng lupain.

Istasyon at pangangasiwa. Kapag ang isang padaluyan na sumisimbolo sa banda ay sumasakop lamang sa bahagi ng linya, ang bahagi ng bawat antena ay inihahambing sa base at tuktok ng padaluyan sa simula at dulo ng bahaging natatakpan; ang pakinabang ay pinarami ng 0.5 + 0.5·isensiya(e^(−ΔhTX/300)·e^(−ΔhRX/300)), na may Δh ang layo sa metro mula sa dukto. Kasama ng padaluyan sa halos lahat ng ruta ay ang pagsasanib ng § 10.

Layer at mga tagaytay. Kung ang tuktok ng hangganang patong ng GFS (ground rate ng pinaka hangganan_layer_height model) ay lumampas sa mga ridge ng ruta (maximum areas sa mahigit 300 m, kung ang pinakamataas ay lumampas sa 500 m), ang layer ay dumadaan sa ibabaw ng mga bundok nang hindi nasisira: +1.5 dB para sa pragmento ng mga natatakpang ridge. Ito ang obserbasyon ng IT9GSF sa mga pagbubukas mula Sicilia hanggang Alemanya na may kaaya-ayang Tyrrhenian.

Mga taas na malapit sa lupa sa dagat. Sa NOMADS ay pinanggagalingan din ng programa ang temperatura sa ibabaw (sa dagat ay ang SST), temperatura at halumigmig sa 80 m at sa patong 30–0 hPa sa ibabaw ng lupa. Sa mga sea point (model rate sa ilalim ng 5 m) ang mga antas na ito ay idinaragdag sa profile, sa ilalim ng unang antas ng presyon, upang makita ang duct sa 100–200 m na hindi nalulutas ng mga antas ng presyon. Sa lupa ay hindi ginagamit: ang mga suson ng gabi at araw ay hindi pa mabisa. Ang open-Meteo ay nagbibigay lamang ng temperatura sa ibabaw.

Mga tubo para sa paglikas. Mula sa SST, mula sa temperatura at halumigmig hanggang sa 2 m ay tinatayang ang δ na taas ng daluyan ng pagsingaw na may neutral na logarithmic profile (gaya ng sa modelo ni Paulus at Jeske, nang walang pagtutuwid ng katatagan): kaya ang hangin hanggang sa buhok ng tubig ay tigmak sa SST (98% para sa asin), samakatuwid ang hangin hanggang sa buhok ng asin),

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

sa pagitan ng 0 at 40 m. Upang mabitag 144 MHz ito ay gugugol ng mga 180 m (Kerr), pagkatapos sa 144 MHz ang daluyan ng pagsingaw ay hindi gaanong nagpapatibay; mula 432 MHz pataas, na may mababang antena sa dagat, malaki (parapo 11 bis). Ipasok ang parabolikong ekwasyon at sa linya ay magrereregula ng 1.3.2.

Oras ng araw (visit). Kinakalkula ng programa ang taas ng araw sa bawat punto. Sa araw - araw sa lupa, na ang araw ay mahigit na 15° at nililimitahan ang suson na mahigit na 800 m o hangin na mahigit sa 5 m/s, ay nagbababala na ang mga duct sa lupa ay nai - re-shape; sa gabi, na ang araw ay nasa ilalim ng −3°, hangin sa ilalim ng 3 m/s at limitadong layer sa ilalim ng 300 m, ay nagbababala na ang tahimik na kalangitan ay posibleng pinangangasiwaan ng impradiation, na bumabagsak pagkatapos sumikat ang araw. Hindi sa dagat. Sa ngayon ang mga ito ay batikan, hindi ang mga numero: Ang bersalidad sa Disyembre ay walang sapat na mga araw ng tag-init upang i-cilibrate ang mga ito. Ito ang dalawang punto na itinuturing ng awtor na mahalaga, mula sa araw - araw na pagmamasid.

Normal na profile CRPL (alternatibo). Sa halip na 40 N/km ay nagtakda ng normal na pagbawas ay sumusunod sa exponential reference atmospera CRPL: N(h) = Ns·e^(−c·h), na may −N sa unang km = 7,32·e^(0,005577·Ns) (Bean at Thayer 1959). Sa pamamagitan ng basang hangin (Ns high) ang normal ay mas matarik at nagsisilbi ng mas malakas na dalisdis upang magbigay ng pakinabang; sa taas ay mas matamis. Isagawa ang masama hanggang sa sabihin ng afession na nakatutulong ito.

## 9 quater. Ihambing ang hangin-dagat at mababang dagat: ang Adriatiko (mula 1.3.3)

Hindi nararamdaman ng mga radio wave ang pinakasahig ng dagat: madama ang pagkakaiba ng hangin at ng ibabaw ng tubig. Ang lalim ay mahalaga sapagkat ito ang nagpapasiya kung gaano kabilis sinusundan ng temperatura ng dagat ang hangin.

Ang Adriatiko ay isang mababang dagat. Ang katamtamang lalim ay halos 250 m, ngunit ang hilagang lunas ay bihirang lumampas sa 100 m at ang Golpo ng Trieste ay nananatiling mas mababa sa 25 m; hanggang sa timog lamang ay bumababa ng higit sa 1200 m. Ang isang suson ng tubig na napakanipis ay umiinit at lumalamig sa loob ng ilang araw, ang Tyrrhenian sa loob ng ilang linggo. May tatlong karaniwang situwasyon:

1. Tagsibol at maagang tag - araw, kaaya - aya. Ang dagat ay malamig pa rin, kahit na para sa tubig - tabang ng mga ilog ng Alpino at Po; dumarating ito na mainit at tuyong hangin mula sa kapatagan o sa Balkans. Ang mainit na hangin sa ibabaw ng malamig na tubig ay bumubuo ng isang pagbaligtad sa dagat (advection pipeline): ay ang klasikong kondisyon ng mga butas sa pagitan ng Italya, Slovenia at Croatia.
2. Taglagas at taglamig na may bora, hindi kaaya - aya. Malamig na hangin sa ibabaw ng mas mainit na dagat: ang mababang patong ay nagiging mabuway, ang mga press at ang duct ay nasisira.
3. Pagkatapos ng isang bora, pagkakataon. Dahil lamang sa mababa ang dagat, pinalalamig ito ng isang bora sa ilang antas sa loob ng ilang araw. Kung sa gayon ay papasok ka sa isang anticyclone na may mainit na hangin, ang pagkakaiba ng hangin-dagat ay malakas at ang duct ay mas matibay kaysa karaniwan.

Ang Adriatiko ay isa ring saradong lagusan sa pagitan ng Apennines at Dinari, wala pang 200 km ang lapad: ang patong-dagat ay nananatiling nakakulong, at ang mga ruta sa kahabaan ng axis (Puglia–Venezia–Trieste, mga 700 km) ay nananatiling lahat sa dagat, ang pinakamahusay na kondisyon para sa isang mahabang dukto.

Ang tinataya ng programa. Sa mga points ng dagat, na ang weather refrections on at ang kahon «Contrasto aria-mareific:

| Kalagayan | Pagtutuwid |
| --- | --- |
| Posibleng temperatura sa 925 hPa (o 950) sa SST ng 4 K | _ |
| Lampas sa SST ng 7 K | +2 dB |
| na may hanging mamasa - masa sa 925 hPa (mahigit na 80%) | −0,5 dB sa mga bonus na ito |
| Malamig na dagat na 1.5 °C o higit pa sa 72 oras bago nito, na may mabuting pagkakaiba | _ |
| Mas mainit na dagat na himpapawid sa 2 m ng 2 °C (mixing) | −1.5 dB |
| Mas mainit na dagat na 4 °C | −3 dB |

Ang kabuuang pagtutuwid ay nananatili sa pagitan ng −3 at +3 dB at, kung walang padaluyan, ang pakinabang ay nananatili sa ilalim ng dilaw na bubong. Pansamantala lamang ang mga pasukan: sa pamantayang atmospera ang potensiyal na temperatura hanggang 925 hPa ay nakahihigit sa dagat ng mga 2–3 K, pagkatapos ay 4 K na ang nagpapakita ng pagbaligtad.

Ang alaala ng SST. Pinananatili ng mga modelo ang temperatura ng dagat na halos hindi nagbabago sa panahon ng paghula. Ang kamakailang pagbaba ay hinango mula sa analisis ng tatlong araw bago (Open-Meteo, tanging temperatura sa ibabaw, tanging mga selula ng dagat): sukdulang 72 oras bago ang minus ng kasalukuyang halaga. Kung ang serbisyo ay hindi tumugon, ang bonus sa memorya ay hindi kumakapit.

Mga limitasyon. Ang SST ng mga modelo sa ibaba ng baybayin at sa itaas na Adriatico ay magaspang; halimbawa, ang malamig na panulat ng Po ay hindi malutas. Hindi pumapasok ang bakterya sa kalkulasyon: laging ibinibigay ng mga pinagkukunan ang SST, na bumubuo sa epekto ng sahig ng dagat. Ang air-sea contrast coups sa isang bahagi sa dulo ng Misme katatagan (§ 9 bis); dahil dito ang mga bonus ay maliit. Ang mga pasukan ay matatalunton sa mga pasilyo ng lugar at ang QSO ay daraan sa Adriatiko.

## 10. Namumuhay sa buong ruta

Kung ang patong ay sumisilo sa banda sa hindi bababa sa 80% ng ginagamit na seleksiyon at ang mga istasyon ay wala sa paningin, ang pagkawala sa daluyan ng tubig ay kinakalkula rin: libreng espasyo, pati na ang pagsasama ng mga antena (3 dB kung ang mga ito ay nasa loob ng padaluyan, mas marami kung ang mga ito ay nasa itaas o nasa ibaba), pati na rin ang restorasyong attenuation πd = 5·10−5·ae·f^(1/3 dB/mrad ng ITU-R.452 eqQ. Kung ito ay mas mababa sa kawalan na walang tropo ang inaasahang kawalan. [6]

Ang isang banda ay nananatili lamang sa daluyan ng tubig kung ang wavelength nito ay hindi hihigit sa mustro = 2°2··(2/3)·√(ΔM·10−)·d (Kerr, eq. 17 [11]). Dahil dito ang parehong patong ay maaaring sumilo sa 23 cm at 70 cm ngunit hindi ang 2 m. Gayunman, ang limitasyon ay hindi net: Si Kerr ay nagbabala na kahit ang mga alon ay ilang beses na mas matagal na dumaranas ng maraming daluyan ng tubig, at ang Hepburn at ang VHF/UHF DX Book ay bumabanggit tungkol sa mga "sphered" na gilid at "patawad" na mga duct. Mula sa 1.3.1 ang programme ay gumagamit ng isang malambot na padaluyan: kung ang λmax ay nasa pagitan ng λmax at 3·λmax, ang pakinabang ay unti-unting nagreresulta mula 15 dB (λ = 3·λmax) hanggang sa buong daluyan ng tubig (λ = λ). Sa kasong iyan ang mga kulay sa duct ay ipinahihintulot. Ang troso at ang linyang Meteo sa kahabaan ng ruta ay nagpapahiwatig ng mga punto na may malambot na duct.

## 10 bis. Via Monte

Kapag ang kabalitaan ay isinara sa likuran ng abot - tanaw, ang programa ay naghahanap ng pinakamabuting direksiyon upang ituro ang mga antena: ang maliwanag na dalisdis ay nagkakalat ng hudyat bilang isang target ng radar. Ginagawa nitong pabilog ang abot - tanaw tuwing ika - 5 (maliban sa ±15° sa palibot ng kabalitaan) at sa bawat direksiyon ay gugugol ng maliwanag na abot - tanaw, mahigit na 100 m at sa pagitan ng 1 at 60 km. Ang isang mabilis na pagtaya (free space, bitactic transbersiyon at 10 dB para sa bawat antas ng abot - tanaw mula sa tagakalat hanggang sa kabalitaan) ay pumipili ng 4 na lubhang maaasahan, hindi kukulanging 15° mula sa isa; para sa bawat isa:

- Ilagay ang 1, station → relief, sa libreng lugar;
- ang 2, ginhawa → kabalitaan, kinalkula na para bang ang ginhawa na inihatid mula 10 m sa itaas, na may gayunding prediksiyon (nang walang parabolikong ekwasyon);
- Pagkumberte sa ekwasyon ng bistatikong radar, na may σ = σ0·A, σ0 = −15 dB (bundok sa VHF/UHF) at A ang lugar na tinatanglawan ng sinag ng R1 (maxium 20 km2):

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

Ang linya ng "Recommended Point" ay nagpapahiwatig ng zimuth ng pinakamahusay na ruta, kung ito ay nagkakahalaga ng mas mababa sa direkta. Sa 144 MHz, na may ginhawa sa 20 km at isang yagi mula 15°, ang daan sa bundok ay nagkakahalaga ng halos 45 dB ng higit sa 2 ruta lamang: nangyayari lamang ito kapag ang direkta ay isinara ng mga kalapit na balakid. Ang tantiya ay malubha (±10 dB), lalo na para sa σ0.


Walang nawawala sa bundok (option, mula 1.3.2). Sa «Via monte senza perdite up al monte» box, ang pagbagsak sa pagitan ng istasyon at ng diffraction point ay itinuturing na null: ay tulad ng pagdadala ng istasyon sa tuktok (ang antena sa 10 m sa itaas ng punto, parehong lakas, antena at kable). Pagkatapos ay pinipili ng abot-tanaw ang punto na may pinaka-malayang abot-tanaw patungo sa kabalitaan (sa parsiyal, ang pinakamataas) at ang resulta ay nagpapakita ng ruta mula sa punto hanggang sa kabalitaan, na may hudyat at S/N, katabi ang kabuuang may diffication. Ito ay isang optimistikong hangganan: sabi ng kung ang direksiyon ay nagsisimula kapag itinataas ng bundok ang hudyat nang mahusay, o kung saan pupunta na nabibitbit. Ang bawat punto ay ibinibigay gamit ang sampung-character.

## 10 ter. Patungo sa isang malayong padaluyan

Kung ang padaluyan na sumisilo sa banda ay nagsisimula sa malayo, sa dako pa roon ng istasyon, dapat na dumating ang hudyat. Dinadala ng programa ang hudyat sa panimulang punto, sa gitna ng padaluyan, para sa diffraction: sa kahabaan ng Copernico profile GLO-30 search sa gilid na may pinakamataas na Fresnel parameter (ITU-R P.526), na may pamamaga ng katumbas na lupain,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

ay mas marami pang attenuation dahil sa kurba ng tuktok (rounded block, P.526-16 § 4.2), na may Rcluving na tinatayang mula sa lupang Copernicus GLO-30 hanggang ±1 km mula sa gilid,

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

(Para sa m·n > 4 ang anyong 34b ng rekomendasyon). Si Barsis (NBS, 1963) na may sukat na tatlong mahabang ruta sa kabila ng isang bundok (Colorado, Alaska, Europa) ay katamtamang nawalan ng 10–20 dB na mas mataas kaysa sa ulirang gilid ng Fresnel-Kirchhoff: ang pag-ikot ay nagpapaliwanag ng maraming (Barsis at Kirby, 1961: Pikes Peak na may radiasyon na halos 11.5 km). Ang resulta ay maihahambing sa diffraction sa makinis na bilog na lupa; ito ang pinakamasama. Pagkatapos ang hudyat ay naglalakbay sa daluyan ng tubig: malayang espasyo sa buong ruta, na pinananatili ang attenuation γον ng P.452 sa bahaging nasa daluyan ng tubig at pinagdugtong ng kabalitaan (3 dB sa loob, 1 dB sa bawat 10 m sa labas). Kung ang pagkawalang ito ay mas mababa kaysa sa inaasahang kawalan ito ay nagiging mekanismo ("conduct (industriya sa pamamagitan ng diffraction)"). Ito ay ang batayan ng may akda sa heograpiyang Italyano: na may kalapit na hadlang na mas mababa sa linya, ang v ay negatibo at ang pasukan ay kaunti lamang ang gastos; na may dorsal na front ay nagkakahalaga ng 15–30 dB.

## 10 quater. ITM (Longley-Rice) at labasan mula sa duct

Mula 1.3.2 ang programa ay naglalaman ng Irreguular Terrain Model 1.2 ng NTIA/ITS (Longley and Rice, ESSA ERL 79-ITS 67, 1968; Hufford, Longley and Kissick, NTIA 82-100, 1982), isinalin sa C# sa pamamagitan ng opisyal na kodigo ng pamahalaan ng US, na malaya mula sa mga karapatan. Ang salin ay nagbibigay ng parehong bilang gaya ng orihinal: ang koleksyon ng kotse ay naghahambing nito sa bawat pagkakataon sa NTIA C++ code sa limang test cases (baba sa 0.01 dB).

Tinataya ng ITM ang pagkawala sa normal na atmospera sa tunay na profile na Copernicus GLO-30: na nakikita na may pagninilay-nilay mula sa lupa, diffraction (makapal at pabilog na lupain na may timbang sa pagiging magaspang ng lupa Δ, iyan ang pagkakaiba ng ika-10 at 90° porsyento ng mga kota sa paligid ng tuwid ng mga minimum na parisukat) at troposcatter. Ang pagkilos ng lupa ay nababawasan sa katamtamang bahagi ng ruta, ang $N_s = N_0\,e^{-h/9460}$, at nagbibigay ng kurbada ng katumbas na lupain,

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

Ang programa ay gumagamit ng radio climate ng napiling ruta gaya ng sa § 10 sexies (hanggang 1.3.4 laging ang katamtamang dagat, na may N0 320 sa lupa at 350 sa dagat), na may N0 mula sa average na reactivity ng lugar (soil εbr 15, σ 0,005 S/m; dagat ε 81, σ 5 S/m), pahalang na polarization at variable sa paglipas ng panahon na wala ang lugar (ang profile ay kilala). Ipinapakita nito ang kawalan na lumagpas sa 50% ng panahon at ang isa na mayroon ka sa 10% at 1% ng pinakamahusay na panahon. Ang ITM ay hindi nagmomodelo ng mga duct: ang pagiging madaling gamitin nito sa paglipas ng panahon ay estadistikal, sa pamamagitan ng klima. Nagsisilbi itong pangalawang kalkulasyon ng baseline na katabi ng P.452; ang pagkakaiba ng dalawa ay nagbibigay ng ideya ng kawalang katiyakan.

Ang ikalawang gamit ay ang pangwakas na banat pagkatapos ng isang padaluyan na nagtatapos bago ang katumbas na bahagi. Sa labasan, ang isang virtual na antena ay nakalagay sa kalagitnaan sa daluyan ng tubig at ang ITM ay tumatantiya sa tract hanggang sa katumbas na isa, bilang labis sa malayang espasyo sa 50% ng panahon. Ganap na kawalan

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,\theta_{condotto} + A_{ITM}(d_{uscita} \to RX)
```

Kung saan ang A_TX ay ang pagsasanib ng antena (3 dB sa loob, 1 dB sa bawat 10 m sa labas) kung ang daluyan ay nagsisimula malapit, kung hindi ang pagpasok ng § 10 ter. Ito ang limitasyon na ibinigay ng awtor sa kaniyang sarili: ang pagpapatuloy ng suson at, sa kalakhang bahagi, ang pananatili sa attenuation. Kung ito ay mas mababa kaysa sa inaasahang kawalan ito ay nagiging mekanismo ("conduct (exit at final tract ITM)").

## 10 quinquies. Pagpapalalim: mga isla, mga kipot at earth-sea Diversity

Isang halimbawa mula sa Golpo ng Naples (Anacapri, JN70CN) patungo sa kanlurang Mediterraneo ay nagpapakita kung saan ang mga ruta ay tumatawid sa mga pulo:

| Pag - aalis | Azimut | Kung saan ito tumatawid sa mga isla | Obstacle |
| --- | --- | --- | --- |
| Barcelona |  279°  | hilaga ng Sardinia (Galura) hanggang 41,05° N, sa pagitan ng 420 at 490 km | mga 30 km timog ng Bocche di Bonifacio; ang Limbara (135 m) mga 20 km mula sa linya |
| Bocche di Bonifacio |  282°  | malapit sa 41,3° N | mga 11 km |
| Marseille |  296°  | Central Corsica sa edad na 42,2° N | bundok hanggang 2700 m: isang padaluyan sa ilalim ng 2 km doon ay sumasabat |
| Valencia, Baleariko |  260–265°  | Gitnang Sardinia | panloob na mga ginhawa |

Para sa Barcelona ay sapat na upang tukuyin ang 282° sa halip na 279° dahil ang axis ay dumadaan mula sa mga bibig: 3rd, sa loob ng sinag ng isang normal na yagi.

Ang mga bibig ba ay isang kipot? Para sa mabababang tubo, sa ilalim ng ilang daang metro, oo: ang mga isla ay napapaderan, at sa ibabaw ng lupa, sa araw, binabasag ng init ang pagbaligtad. Ang kipot mismo ay sapat ang lapad: mga 11 km, laban sa unang lugar ng Fresnel, wala pang isang kilometre ang lapad sa gitna ng 1000 km ruta sa 144 MHz. Ang problema ay lagay ng panahon. Ang mga intuwisyon ay nakatutuwa sa hangin: Pinabilis ang pang-ibabang patong at inaalis ang pang-ibabang patong. Kadalasang bukás ang daanan bilang heometriya subalit maligalig na gaya ng atmospera. Sa halip, sa pamamagitan ng isang patuloy na anticyclone at kaunting hangin ay maaaring maging ang mabuting daanan.

Ang Earth-sea division. Kinabibilangan nila ito. Isang padaluyan ng tubig na nabuo sa baybayin ng Tyrrhenian ay natigil: ang hudyat ay lumalabas mula sa padaluyan (ang bahagi pagkatapos ng labasan, § 10 quater) at dapat bumalik sa isa pang padaluyan sa dagat ng Balearics, na muling nagbabayad sa pagkakahati. Ang mga padaluyan sa taas, sa itaas ng 1500 m, ay dumaraan sa ibabaw ng Gallura nang hindi namamalayan; sapagkat ang gitnang Corsica ay hindi kumakapit.

Sa pagsasagawa: kung ang ruta ay tumatawid sa isang bulubunduking pulo, subukan ang ilang antas ng pagtutuwid patungo sa kahabaan ng malayang dagat, ngunit huwag umasa sa landas na may malakas na hangin sa makipot.

## 10 sekso. TN101 radio climate at time variable (mula 1.3.5)

Ang TN101 § 10 [25] ay naglalarawan kung paanong ang pagkawala ng isang ruta na lampas sa abot-tanaw ay nag-iiba sa kahabaan ng median, na may iba't ibang kurba sa pamamagitan ng klima: ang tungkuling Y(q) ay nagsasabi kung gaano karaming dB ang nawawala sa q% ng pinakamainam na panahon. Ang ITM ay naglalaman ng mga kurbang iyon para sa pitong klima. Ang klima ay mahalaga: sa dagat sa katamtamang klima ng dagat, sa 144 MHz, sa 10% ng panahon na ikaw ay kumikita ng mas maraming dB kaysa sa lupa sa klima ng kontinente, dahil ang mga duct ay mas madalas.

Hindi na naaayos ng programa ang klima: Pinipili niya ito sa bawat paggamot mula sa katamtamang punto, kaya nagtatrabaho ito sa buong daigdig at hindi lamang sa Europa. Download mula sa Open-Meteo [26] tatlong taon ng araw-araw na halaga ng ERA5 reanalysis (temperature, halumigmig at presyon ng lupa, ulan) para sa selula ng 0.5° ng karaniwang puntos, minsan lamang; ang mga halaga ay nananatili sa Data/CLIMA. Pagbabalik ng lupa

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

araw - araw, pagkatapos ang taunang katamtamang bilang at ekstinksiyon sa pagitan ng buwanang katamtamang dami, katamtamang halumigmig at taunang ulan. Sinusunod ng mga tuntunin ang mga paglalarawan ng mga klima ng TN101 § 10.2; ang mga hangganan sa pagitan ng isang klima at ng isa pa ay isang pamantayan ng may akda:

- mahigit na 60 latitud ng°: katamtamang kontinental (ang TN101 ay walang mga kurba para sa klima ng polo);
- Wala pang 250 mm na ulan bawat taon at katamtamang halumigmig na mababa sa 50%: disyerto (Ns karaniwang 280);
- sa loob ng 20° N ng hindi bababa sa 350 at ekstinksiyon hanggang sa 30: ekuwador (Ns karaniwang 360);
- sa loob ng 32° at ekstinksiyon ng hindi bababa sa 60: subtropikal na kontinental, na mga monsoon (karaniwang ekstinksiyon 60–100);
- sa pagitan ng 10° at 32° Ns ng hindi bababa sa 340: subtropikal na pandagat (Ns karaniwang 370);
- parehong abot-tanaw sa dagat, mahigit 20°: sea wide sa dagat (ito ang depinisyon ng TN101);
- umaabot sa 30 malapit sa dagat (dagat na nasa ruta o katamtamang kaumiduhan na hindi bababa sa 75%): Maamong dagat sa lupa;
- Kung hindi ay katamtamang kontinental (karaniwang ekstinksiyon 20–40).

Ang N0 for ITM ay ang katamtamang nabawasan sa antas ng dagat, N0 = Ns·e^(h/9460). Kung walang network ay gumagamit ka ng reserbang tuntunin na ang latitud lamang at ang maliliit na bahagi ng dagat, at sinasabi ito ng linya.

Mga pagsubok na may datos ERA5 2023–2025: Capri Ns 339 na may ekstinksiyong 45 (continental tempered sa lupa, sea temperate sa dagat kapag parehong nasa dagat ang parehong abot-tanaw); London 327 at 16 Vienna 319 at 24 Abidjan 381 at 11 (equatorial); Miami 365 at 36 (stropikal na asawa); Delhi 337 at 82 (stropical concontinental); Riad 278 at 37, 90 mm ulan (desert). Sa Chile: Antofagasta 341 at 19 (stropikal na asawa: ang baybayin ng Atacama, na may halos nakapirmeng mababang baligtad ng Pasipiko), Calama 232 sa lupa, 2257 m, 14 m ng ulan (desert), Valparaíso 331 at 11 (temperature seam), Punta Arenas 313 at 5 (temperature seam).

Ang linyang "Radio climate ( TN101)" ay nagpapakita ng klima, ang dahilan, Ns na may kalahati ng ekstinksiyon at Y: gaano karaming dB ang kumikita sa 10% at 1% ng panahon at kung gaano karami ang nawawala sa 90%. Ito ay long-term statistics at nananatiling hiwalay sa propesiya: ang inaasahang pagkawala ay sumusunod sa panahon ngayon na may profile ng GFS, at ang kabuuan ng dalawang bagay ay bibilang ng dalawang beses sa parehong abnormal na propaksiyon.

## 10 septies. Muling makikita ang prediksiyon at beripikasyon sa pamamagitan ng mga radioson (mula 1.4)

Pagsusuri sa Radioson. Hindi gaanong nakikita ng GFS ang mga duct kaysa sa wari: Ang patayong resolusyon nito malapit sa lupa ay ilang sampung metro, at ang suson ng isang baligtad ay kadalasang mas manipis. Upang malaman kung gaano ang pagtitiwala sa modelo rito at sa mga panahong ito, inihahambing ito ng programa sa tunay na radio waves ng pinakamalapit na istasyon, mula sa arkibong IGRA v2 ng NOAA [27] (public domain):

- mula sa talaan ng mga istasyon ang pinakamalapit na aktibo sa loob ng 600 km (para sa Capri, Gawain ng Mare, ITM00016245);
- I-upload ang kasalukuyang talaksang taon (updates kada ilang araw) at panatilihin ang mga paglulunsad ng 00 at 12 UTC na tumatagal ng 15 araw;
- para sa bawat paglunsad ay nakukuha ang reactivity N level sa pamamagitan ng antas (na may kaumiduhan o depresyon ng punto ng hamog), ang taas ng mga antas na walang mga ito na may impometric ekwasyon na nagsisimula sa taas ng istasyon, ang spiral ng unang km at ang pinakamalakas na padaluyan sa ilalim ng 3000 m (M na bumabagsak na may bahagi ng hindi bababa sa 2 yunit);
- Mag-download mula sa serbisyo ng mga makasaysayang propesiya ng Open-Meteo [15] ang mga profile ng GFS ng parehong punto sa parehong oras at suriin ang mga ito ng parehong kodigo ng paghula;
- ang naobserbahang mga duct na nakita ng GFS, ang nawawala at maling mga alarma, at ang katamtaman at parisukat na paglihis ng groove.

Ang beripikasyon ay nasa likuran ng unang kalkulasyon ng isang gamot, sa karamihan ng 12 oras; ang mga file ay nananatili sa Data/IGRA at ang mga numero sa rehistro ng kalkulasyon. Ang mga mensaheng radiosonde na nagmumula sa pandaigdigang network ng lagay ng panahon (TEMP) ay mayroon lamang pamantayan at mahahalagang antas: ang napakanipis na mga padaluyan ay nakakatakas din sa mga ito, at ang paghahambing ay nagsasabi nang higit sa lahat kung ang GFS ay masyadong mali sa isang diwa o iba pa.

Madaling tanggapin. Ang "Affibility" line ay nagbibigay ng mataas, katamtaman o mababang awdisyon sa inaasahang pakinabang. Hindi ito isang napakalaking probabilidad, na mangangailangan ng mga taon ng paghahambing sa tunay na mga kaugnayan (imperyo 14), kundi isang kabuuan ng mga punto sa kung paanong ang independiyenteng mga himaton (ang kodigo ng Maylikha):

| Pangungusap | Mga Punto |
|---|---|
| Kasunduan sa pagitan ng mga modelo: katamtamang paglihis sa pagitan ng inaasahang pagkawala at P.452 na may mga duct, ang parabolikong ekwasyon, at sa pagitan ng baseline at ITM | hanggang 3 dB +1; mahigit 8 dB −1 |
| Patiunang pagtaya | sa loob ng 24 oras +1; mahigit 72 oras −1 |
| Patuloy na pagtatayo ng suson sa paggamot | Hindi bababa sa 70% +1; wala pang 30% −1 |
| Pagpupumilit ng suson sa kalapit na mga oras | Hindi bababa sa 70% +1 |
| Pag - unlad ng Radiosonde | Ang GFS ay hindi bababa sa 3 beses lamang sa 4 +1; mas mababa sa isa sa dalawa −1; maling frayf ng higit sa 15 N/km −1 (maximum −1 sa lahat) |
| Mabuhay kumpil sa kahabaan ng ruta (cluster, PSKReporter, APRS, FM-DX), para lamang sa kasalukuyang panahon | 1–2 pares ng mga istasyon +1; 3 o higit pa +2; mga bukasan na may inaasahang pakinabang sa ilalim ng 2 dB −1 |

Mataas mula sa 3 puntos, katamtaman mula 0 hanggang 2, mababa sa sero. Ang isang live na kumpanya ay tumataya bilang isang pares ng iba't ibang istasyon, sa direksiyon sa loob ng 20° mula sa ruta at may katamtamang punto sa loob ng 150 km mula sa linya nito, sa huling oras at kalahati. Sa kaso ng arkibo (§ 3 ter) ay walang mga radioson o live na kumpil.

## 11. Paghahambing sa ITU-R P.452

Ang linya ng P.452 ay naglalapat ng paraan ng ITU para sa mga duct at repleksiyon sa mga layer (P.452 § 4.5 [6]) kasama ang ΔN median ng GFS sa halip ng climatological at p = 20% bilang isang calibration constant, tulad ng sa CSIRO [10] study. Kabilang dito ang pagkawala ng empirikal na pagsasanib na Alf sa ilalim ng 0.5 GHz (mga 28 dB hanggang 144 MHz, 3.532 Isa lamang itong paghahambing: ang inaasahang kawalan ay nananatili sa parapo 9.

Mula 1.4: P.452-18 kumpleto at napatunayan. Ang paghahambing ngayon ay gumagamit ng lahat ng pangunahing pamamaraan ng P.452-18 [29] (§ 4.1–4.6): malayang espasyo na may gas at multipath, diffraction na may bilog na lupa at profile ni Bullington sa tamang makinis na ibabaw para sa mga balakid, troposcatter, ducts at repleksiyon sa mga layer, at ang kanilang kombinasyon. Ang ΔN at ang N0 ay nagmula sa GFS at mula sa lugar sa halip na sa mga mapa ng ITU. Ang code ay napatunayan sa pamamagitan ng opisyal na test cases ng grupong ITU pag-aaral 3 (iniipon sa proyektong Py452 [29]): sa 194 kaso hanggang 2 GHz nang hindi nilalamon ang lupa ang average na paglihis sa kabuuang kawalan ay 0.06 dB, ang sukdulang 0.54 dB; diffraction 0.10 dB sa katamtaman, troposcatter 0.002 dB. Ang beripikasyon ay nagtuwid ng dalawang pagkakamali ng mga naunang bersyon: ang katagang pag-iisa ng mga duct ay gumamit ng dalas sa MHz sa halip na sa GHz (mga 60 dB ng labis, na nagreresulta sa ibang lugar), at ang diffraction ay humiwalay mula sa mga limitado nang makinis na mga stack sa ibabaw, na may mga pagkakamali hanggang 15 dB sa ilang profile.

Inihambing din ang ITM (§ 10 quater) sa NTIA [30] (point-point): ang limang bahagi ng opisyal na talaksan ay nagbibigay ng parehong mga halaga sa dB cent.

## 11 bis. Parabolikong ekwasyon (mula 1.3.1)

Ang Parabolikong Equation Line ay isang pang-apat na kalkulasyon, na paghahambing lamang, na ginawa bilang mga propesyonal na programa para sa mga duct (PETOOL; ang wave-propagation frameion ng M. Lytaev, lisensiyang MIT): sa halip na i-estimula ang pakinabang mula sa mga patong, ito ay nagreresulta sa alon sa kahabaan ng ruta sa pamamagitan ng split-sap na pamamaraan ng Fourier sa loob ng binagong repractive profile ng GFS, na kinuha mula sa punto na mas malapit sa bawat distansiya. Ang mga antas ng GFS ay nagdaragdag ng tinatayang mga pagtalon ng mga potensiyal na duct (pagsukat ng A, 50 m sa kalahating layer). Ang lupang si Copernicus GLO-30 ay pumapasok sa mga hakbang (sa ilalim ng lupa ang field ay null), ang lupa ay konduktor at ang sinag ng antena ay isang Gaussian na may tinatayang bukas mula sa pakinabang. Nakikita ng kalkulasyon ang mga padaluyan, yaong mga natatalo, ang diffraction at ang pagsanib ng mga antena; Hindi Niya nakikita ang troposcatter, na hinahati. Kasunod nito ang «profile cheque»: katulad ng sa pamantayang atmospera ay mas mababa ang isa na may inaasahang profile.

Mula 1.3.2:

- Ang propator ay nasa malapad na anggulo, na may eksaktong yugto ng malayang espasyo:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- ang ilalim sa dagat at ang baybaying kapatagan sa sero altitud ay sumasalamin sa codibilidad ng Fresnel sa pahalang na polarisasyon, anc = ε − − − − −·λ (sea εr = 81 at σ = 5 S/m; bansa 13 at 0.006 S/m), sa malinaw na anggulo ng ruta:

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- Ang pagiging magaspang ng dagat ay nagbabawas ng hindi nagbabagong pagninilay-nilay ayon kina Miller at Brown, na may epektibong taas ng alon mula sa hangin hanggang 10 m (σh = 0,0051·U2):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- sa dagat, kasama sa profile M ang tubo ng pagsingaw (parapo 9 ter) na may neutral na logarithmic profile:

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

Sinusuri nito ang modelo (10 m antena sa dagat, nakikinabang sa pamantayang atmospera sa 100–300 km): may δ = 10–30 m ang daluyan ng pagsingaw ay nagbibigay ng 1–3 dB sa 144 MHz, 8–37 dB hanggang 432 MHz at 21–66 dB sa 1296 MHz. Sa 144 MHz sa pahalang na polarisasyon ang dagat ay sumasalamin halos bilang isang metal (| | | higit sa 0,99 hanggang sa ilang ikasampung bahagi ng digri) at magaspang ay may kaunting halaga: ang bagong ilalim ay nagbabago ng ilang ikasampu ng dB; ito ay may halaga ng higit mula 432 MHz pataas.

Mga hangganan: sa ibabaw ng lupa ang patayong profile ay nananatili na gaya ng sa mga antas ng GFS, kaya ang manipis na mga duct ay nananatiling hindi nakikita; ang lupa hanggang sa mga hakbang ay tinataya; ang sinag ay nananatili sa loob ng ika - 6.

## 12. Mga antena, hudyat at ingay

- Yagi DL6WU: tamuhin ang 9.5 + 7.8·log10(boom/Ginto) dBd (2.35 dB para sa double boom, ang DL6WU ay sumusukat sa VHF Communications 3/1982), at karagdagang 2.15 para sa [12] dBi.
- EIRP = kuryente − kable + TX pakinabang.
- Signal natanggap = EIRP − inaasahang pagkawala + makakakuha ng RX − cable RX. S-meter IARU: S9 = −93 dBm, 6 dB kada yunit.
- External na ingay ayon sa ITU-R P.372 [13]: galactic (Fa = 52 − 23 log f) na mas artipisyal ayon sa kapaligiran (city, residential, rural, tahimik na rural) o isang sulat-kamay na temperatura ng antena.
- Tsys = Ta + Tpre + [290(Lc − 1) + 290(Fr − 1)·Lc] / Gpre hanggang sa mga terminal ng antena (Friis force). Mula 1.3.5 ang istasyon ng receiver ay palaging may preamplifier sa antena, nakapirmeng parameter: ingay numero 0.8 dB (Tpre = 59 K) at nagkamit ng 20 dB. Sa likod ay naroon ang kable (los Lc) at ang radyo, na may pangunahing bilang ng ingay na Fr 6 dB. Sa 1 dB ng kable ang kable at radio input ay bumababa mula mga 1160 K hanggang 12 K: ang ingay ang nagpapasiya sa kalangitan, sa kapaligiran at sa preamp. Ang pagkakamit ng 20 dB ay karaniwang halaga na pinipili ng programa.
- S/N sa SSB, CW at FT8 sa banda ng bawat paraan, kumpara sa stand set.
- Ang probability for period (mula 1.3.2): ang evancence sa loob ng oras ay isang normal na distribusyon ng S/N sa paligid ng inaasahang katamtamang oras na halaga. Ang Bray (QST, Nobyembre 1961, p. 39, sa mga kurba ng Norton 1959) ay nagbibigay ng amplude na 13,5 dB na mahigit sa 100 milya, i.e. mga 7 dB na mababa at 7 na mas mataas sa katamtaman, na bumabagsak ng halos diretsong mababa sa 100 milya (3,5 dB hanggang 50 milya); sapagkat ang isang 'solid' link ay kumukuha ng 7 dB mula sa badyet. Nakuha ±7 dB bilang ±2σ: σ = 3,4 dB mahigit 160 km, proporsiyonal sa layo sa ibaba. Ang probabilidad na ang isang yugto ay nakahihigit sa bungad ng daan ay

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  na may Φ normal na tungkuling pamamahagi: margin 0 dB = 50%, +3,4 dB = 84%, +7 dB (ang 'solid' na gilid ng Bray) = 98%. Hindi kasama rito ang kawalang katiyakan ng modelo, na mas malaki.
- Digital na mga Paraan: S/N noong 2500 Ang Hz ay inihambing sa mga stage na may sukat na 50% decoding, nang walang patiunang impormasyon (AP). FT8 −21 dB at FT4 −17,5 dB sa puting ingay; kapag ang mekanismo ay troposcatter ay gumagamit ng mga gateway na may evancence na "disturbated latitud medium", FT8 −18,6 dB at FT4 −15,2 dB [21]. Q65 -15C −22,2 dB, Q65 -30B −24, dB, XB, XQ60 -−27, XQ6 −27. Sa sasakyang panghimpapawid, ang Doppler ay maaaring lumagpas sa mga tono ng Q65 -60A (1,7 Hz): mayroong mas mainam na 30B o 15C. JT65A −25 dB [22]. Ang decoding AP (pangalan ng alam na o dulo ng QSO) ay nagbibigay ng halos 2 dB ekstra para sa FT8 at 1.5 hanggang 2.6 dB para sa Q65; hindi ito pumapasok sa pasukan dahil hindi ito tama para sa lahat ng Ang «−24 dB» na kadalasang binabasa para sa FT8 ay ang hangganan ng mga ugnayang WSJT-X o AP decoding, hindi ang pasukan sa 50%. Q65 [24]

## 13. Sagot at mga panahong datos

Ang bawat kalkulasyon ay sumusukat sa sarili nitong panahon: GFS profile (network, analysis, integral na breaks ng 20 s sa pagitan ng mga pundamental para sa Open-Meteo limit), Copernicus GLO-30 kalupaan (na may download ng mga panes, 15–30 MB ang bawat isa, sa unang pagkakataon lamang), kalkulasyon. Binubuod sila ng Response Time line; sa rehistro nagsisimula ang mga hanay sa TEMPI. Kapag ini-upgrade ang grid, iniuulat din ng rehistro ang GFS race na ginagamit, kapag ito ay naging available at kung gaano katanda ang datos.

Mula 1.3.2 maaari mo ring piliin ang ICON-2I, ang modelo ng Agency ItaliaMeteo at Arpae sa Italya sa halos 2.2 km, sa pamamagitan ng Open-Meteo (model it_meteo_arpae_icon_2i, dalawang lahi kada araw, 3 araw). Ito ay sumasaklaw lamang sa Italya at sa mga kalapit na dagat at may hindi gaanong mababang antas ng GFS (1000, 925, 850, 700, 500 hPa): para sa mga padaluyan sa taas ay hindi mas mabuti kaysa sa GFS, ngunit ang pinong grid ay naglalarawan ng mahusay na mga baybayin, mga isla at dagat sa 2 m, na ang daluyan ng pagsingaw. Ang mga variable na ang isang modelo ay walang (strate limit, temperatura ng ibabaw...) ay nag-aalis ng kanilang mga sarili mula sa kahilingan. Mula 1.3.2 ang awtomatikong pinanggalingan ay gumagamit lamang ng NOMADS; nagsisilbi ang Open-Meteo kung hindi tumutugon ang NOMADS at para sa mga kasong pangkasaysayan. Sa pamamagitan ng "Update sa sarili nitong" kahon (ipinailaw sa pamamagitan ng default) ang paghula ay i-download nang walang pag-eeeksperimento: sa pagbubukas, sa bawat bagong GFS race sa NOMDS (mga 5 oras pagkatapos ng panahon ng karera) at kapag ang nakikitang lugar ay out para sa higit sa sangkapat mula sa grid; sa karamihan ng isang pagtatangka sa bawat 15 minuto, hindi kailanman may makasaysayang kaso.

Ang mga kahilingan sa Open-Meteo ay may hangganan sa bawat minuto, bawat oras at bawat araw para sa bawat internet address. Ang mga naka-download na profile ay natitipid sa disk (Dati/cache-gfs) para sa bawat stroke at modelo: na may parehong lahi, na nagbabago tuwing 6 oras, ang mga punto na na-download na ay hindi na muli. Sa awtomatikong pag-uumpisa mo mula sa NOMADS at Open-Meteo nagsisilbi lamang para sa mga nawawalang puntos; sa tanging Open-Meteo, sa itinakdang oras o araw-araw na paghinto agad at sinasabi kung kailan muling susubukin. Ang makasaysayang mga kaso ay hindi kinakupkop.

## 14. Gumawa ng tamang modelo

Ang paghula ay dapat ihambing sa tunay na mga koneksiyon. Pinananatili ng talaan ng kalkulasyon ang bawat numero; iniingatan ng arkibo ng programa ang mga anunsiyo at mga decoding. Para sa 432 at 1296 MHz, kung saan kakaunti ang mga batik, kailangan nila ng mga ulat mula sa mga operator: sino ang gumawa ng koneksyon, sa anong oras, sa anong hudyat. Ihambing sa loob ng maraming oras ang inaasahang pakinabang sa panukat na hudyat ay nagsasabi kung ang mga pasukan ay ililipat.

Ang mga test program na ginagamit para sa P.452 at ITM ay nasa source code tools folder; ITU test cases ay dapat i-download ng hiwalay, dahil ang programa ay hindi redistribote ITU data.

## Pinagmumulan

1. NOAA/NCEP, Global Eventation System (GFS), global model sa 0.25°, apat na karera kada araw.
2. Open-Meteo, propesiya API (bukas-meteo.com), gfs_seamless model, variables sa mga antas ng presyon at karera metadata.
3. Copernicus DEM GLO-30 (ESA; WorldDEM © DLR e.V. 2010–2014 e © Airbus Defence and Space GmbH 2014–2018, na inilaan bilang bahagi ng Copernicus ng Unyong Europeo at ESA), 1 pangalawang arc, GeoTIFF frames sa bukas na rehistro ng Amazon Web Services. Reserve: NASA/USGS Copernicus GLO-30 sa Terrain Tiles "skadi" ng AWS.
4. ITU-R P.453, Ang radio refractive index: ang pormula at refractivity na petsa nito, na may digital channel maps (School Group 3, © ITU, personal at hindi-commercial na gamit).
5. B. R. Bean, E. J. Dutton, Radio Meteorology, NBS Monography 92, 1966: Table 6.5 at § 4.4.
6. ITU-R P.452-16, mga pamamaraan ng paghula para sa pagtatasa ng interbensyon sa pagitan ng mga istasyon sa ibabaw ng Lupa sa mga frequency sa itaas ng mga 0.1 GHz: § 4.2 (diffraction), eq. 45 (troposcatter), § 4.5 at eq. 49 (conducted).
7. ITU-R P.834, Mga Epekto ng tropospherikong refraction sa radiowave propagation.
8. ITU-R P.525, kalkulasyon ng free-space attenuation.
9. ITU-R P.526, Propagasyon sa pamamagitan ng diffraction: eq. 31 (cutcheon) at § 3 (sphereng lupain).
10. B. Indermuehle, H. Suzuki, Tropospheric Ducting Prediction batay sa GFS Model Data sa Inyarrimanha Ilgari Bundara, ang CSIRO Murchison Radio-astronomy Observatory, Publications ng Astronomical Society of Australia, 2026 (arXiv 2608.12861).
11. D. E. Kerr, Propagation of Short Radio Alones, MIT Radiation Laboratory Series vol. 13, 1951: eq. 17 e 18.
12. G. Hoch DL6WU, «Extremely Long Yagi Antennas», VHF Communications 3/1982, p. 130 et seq., igos. 1 (Tumaas 432 at 1296 MHz ayon sa haba ng boom).
13. ITU-R P.372, ingay sa radyo: mala-aero at artipisyal na ingay para sa kapaligiran.
14. NOAA/NCEP, NOMADS, GRIB filter of the GFS 0.25° time (nomads.ncep.noa.gov); WMO G RIB2 format, komplikadong pag-impake na may mga pagkakaiba-iba sa spray.
15. Open-Meteo, Historical propesiya API (model presect archive mula pa noong mga 2021).
16. LukiBox, TropoLink, disenyo ng mga ruta ng troposcatter na may lisensiyang P.617, NBS TN101 at ITM sideed (GitHub, Apache 2.0).
17. ITU-R P.617-5, Propagation promision techniques at data na kailangan para sa disenyo ng trans-horizon radio-relay system, 2019.
18. RSGB, VHF/UHF Handbook; DL2FZN pagpapatupad (andi-f/troposcatter on GitHub, 2013).
19. W. F. Moler, D. B. Holden, «Tropospheric dinadaglat ang propagation at at atmospheric circulations», Journal of Research of the NBS 64D, 1960, doi 10.6028/jres.064d.014: signal na lampas pa sa abot-tanaw (1365 at 9380 MHz, sa dagat) at patayong paggalaw ng hangin.
20. ARRL Handbook 2022 (99a ed.), § 19.4.3–19.4.4: weather at troposferical propagation.
21. S. Franke, B. Somerville, J. Taylor, The FT4 and FT8 Communication Protocols, QEX Hulyo/Agosto 2020.
22. W3SZ, mga pagsisimula ng mabagal na digital modes ( JT65, JT9, QRA64) at pagpili ng mode ayon sa propagation, 2019.
23. QST, "The World Abody 50 MHz", Nobyembre 2022: pagbubukas ng 14–15 Agosto 2022 sa pagitan ng Manitoba at Kansas/Missouri.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Quick-Start Guide to Q65, 3 Abril 2021 (wsjt.sourceforge.io).
25. P. L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis, Transmission Loss For Tropospheric Communication Circuits, NBS Technical Note 101, magasin 1967: § 9 (troposcatter, function F(θ), H0), § 10 at Annex III (radio klima at variable sa paglipas ng panahon).
26. Open-Meteo, Historical Weather API (ECMWF ERA5 review mula 1940; Copernicus Climate Change Service, CC BY 4.0 lisensiya).
27. I. Durre, X. Yin, R. S. Vose, S. Applequist, J. Arnfield, Integrated Global Radiosonde Archive (IGRA), bersyon 2, NOAA National Centers for Environmental Information, doi 10.7289/ V5X63K0Q (public domain): igra2-data-format.txt data format at station list.
28. FM-DX Webserver (NobishSVK at cooperators, lisensiya GPL-3.0) at talaang pampubliko ng mga server.fmdx.org.
29. ITU-R P.452-18, ang mga pamamaraan ng paghula para sa pagtatasa ng interbensiyon sa pagitan ng mga istasyon sa ibabaw ng Lupa sa mga frequency sa itaas ng mga 0.1 GHz, 2023; mga test case ng grupong pag-aaral 3 (validation cavies), gaya ng nakolekta sa I. Stevanovic, Py452 (GitHub).
30. NTIA/ITS, Irreguular Terrain Model (ITM) 1.2.2, reference code at point-point test cases (GitHub NTIA/itm).
31. ITU-R M.1371, Technical characters para sa isang automatic identification system gamit ang time division multiple access sa VHF maritime mobile frequency band.
