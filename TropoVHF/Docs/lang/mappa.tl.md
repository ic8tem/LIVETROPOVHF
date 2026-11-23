NOBYEROPOVHF · teknikal na dokumento 2
# Pagtatayo ng mapa

> Awtomatikong salin ng Italyano: Kung pag - aalinlanganan ay sulit naman ang giyang Italyano.

Habang ang mapa ng tab Map at spot ay ipinanganak: ang heograpikal na base, ang mga projections, ang average ng Hepburn at F5LEN indices, ang isobare, ang mga batik at ang mga istasyon ay nakikinig, na pinagdugtong ng mga patong. Bersiyong 9 Nobyembre 2026.

## Bilang sumaryo

- Ang mapa ay dinisenyo ng programa sa walong patong, bawat isa ay may [8] box.
- Base: Natural Earth baybayin at hangganan sa dalawang antas [9] detalye; [10] Market at azimuth projections.
- Physics: inbersiyon, reaktibidad N at drop dN/dh, lead stand sa −157 N/km [3] [4] [7] [11].
- Throat index: calibated na pagbabasa ng Hepburn [1] at F5LEN [2] maps, karaniwang grid at average ng dalawang mapagkukunan.
- Isobare: GFS [12] pressure via Open-Meteo [15], linya kada 4 hPa, H at L centers.
- Spot, mga istasyong nakikinig (madidilim na bilog na kulay rosas na laging nakikita ang mga linyang kulay rosas) at personal na mga decoding na pula.
- Binabasa ang pagsasanib: mataas na presyon, mataas na mga duct, pagbaba ng presyon, paglago ng SNR, [3] [4] [5] [13] [14] mga ruta ng bundok.

## 1. Ang simulain: isang mapa na may patong

Ang mapa ay hindi isang pirmihang imahe: dinisenyo ng programa, patong-patong, kailanma't nagpapalit ka ng zoom, projection, banda, paghula ng oras o tema. Bawat layer ay may kani - kaniyang kahon sa itaas na Layer bar sa mapa, kaya maaari mo lamang hawakan kung ano ang kailangan mo at makita ang lahat ng bagay nang nais mo ang buong larawan. Ganito ang utos:

|  #  | Daan | Nasisiyahan | Kasolla |
|  1  | Pangunahing mapa | Dagat, lupain, baybayin at hangganan | Mayroon |
|  2  | Indise ng tropo | Media Hepburn + F5LEN, o iisang pinagmulan lamang; naibabagay na transparency | Mayroon |
|  3  | " Grill locator " | Mga bukid, liwasan at, kasama ng zoom, ay parisukat | Mayroon |
|  4  | Isobare | Mga linya ng presyon tuwing 4 hPa, mga sentrong H at L | Mayroon |
|  4a  | Katamtaman 850 hPa | Iba't iba ang pagtrato 4 °C | Mayroon |
|  4b  | Ang Matinding Pagkumberte | Kahel sampol kung saan ang hangin sa taas ay mas mainit kaysa sa lupa | Mayroon |
|  5  | Tagapag - ulat ng PSK | Tumanggap ng mga istasyon ng PSKReporter (madidilim na bilog na rosas) at, sa kulay rosas, ng kanilang naririnig | Mayroon |
|  6  | Hinango ko | Mga himpilang pang-cloding na may WSJT-X, JTDX o MSHV: Mga linyang Pula mula sa iyong QTH | Mayroon |
|  6a  | Ang QSO desunti dal ay nabubuhay | Ang mga malapit na koneksiyon ( RR73, 73, RRR) ay mababasa sa inyong mga decoding: berdeng linya sa pagitan ng dalawang istasyon | Mayroon |
|  7  | Pagtutok | Mga koneksyon: buong asul na linya, dilaw na natatakpan ng airscatter, dalawang bola | Mayroon |
|  7a  | Mga Pagsabog | Kasolla Inilarawan sa ilalim ng 100 km: ang pinakamaikling trapiko ay hindi naiguhit o naitatala. Kasolla Mahigit 1000 km: mga liwasan lamang (bituin): kung ang isa sa mga istasyon ay mahigit 1000 km mula sa iyong QTH, walang mga linya o bilog, tanging ang mga liwasang locator ng mga istasyong nasasangkot sa lilac na ginamot sa 45° | Mayroon |
|  8  | Ang TH at ang mga etiketa | Ang istasyon mo, ang pangalan ng napiling lugar | laging nakikita |

Ang indise ng tropo ay kaagad na nasa itaas ng base at nasa ibaba ng lahat ng iba pang mga bagay: ay isang may kulay na mga halimbawa, at ang mas mataas na lalagyan ay tatakip sa mga linya at simbolo. Dumaraan dito ang grid, kaya alam mo kung saan nahuhulog ang isang lugar na may kulay. Ang mga isobare ay maninipis na linya at nababasang mabuti sa mga sampol.

## 2. Ang pangunahing mapa

Ang mga gastos, hangganan at lupain ay nagmumula sa likas na datos ng ventor sa Lupa, ng public domain [9], na aabot sa 1:50 milyon para sa pangkalahatang pananaw at 1:10 milyon kapag pinalaki. Ang mga file ay nasa program folder: walang online map services. Mga kulay ng tema Clear: light blue sea # E2ECF8, dark blue baybayin, light grey hangganan. Sa mga temang Black and Night ang mga kulay ng base ay madilim, sa iba pang mga tema ay bahagyang nakukulayan nila.

## 3. Ang dalawang usli

Ang lahat ng mga suson ay dumaraan sa parehong tungkulin ng projection, kaya ang mga ito ay laging magkahanay. Pinipili mo ang sa pagitan ng dalawang [10] projection (alingtipiko, φ, R ray of the Earth):

Market. Itago ang lokal na mga sulok at hugis; Ang mga distansiya ay umaabot hanggang sa mga polo. Bawasan sa ±85° latitud.

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

Ang Azumile equidtanti ay nakasentro sa iyong liwasan. Ang sentro (φ0, λ0) ay ang sentro ng iyong liwasang WWL. Ang mga distansiya at direksiyon na sinusukat mula sa gitna ay totoo: ang isang istasyon na nasa 1200 km ay 1200 km ang lawak, at ang anggulo na may hilaga ay ang azimuth kung saan itututok ang antena. Kapag binago mo ang locotor ng mapa refocuss.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

Sa zimutatal ang mga linya na nasa papel ay tuwid (mga meridian, parallel, parisukat na gilid) ay nagiging kurba. Dahil dito ang lahat ng linya ay nahahati sa maiikling katangian, ang bawat bahagi ay tinataya at pagkatapos ang mga katangian ay nagkakaisa; ang parehong mga pagkapit sa mga gilid ng indise ng tropo at sa isobare. Ang punto sa mga antipode ng sentro ay walang tiyak na posisyon at nananatiling hindi nakikita.

## 4. Gill locator, zoom at lumipat

Ang Maidenhead grid ay dinisenyo ng programa: mga larangan ng 20° × 10° na may dalawang malalaki at kupas na titik sa gitna, mga parisukat ng 2° × 1st, at may sapat na zoom subquadrati ng 5° × 2,5′. Ang mga linya ay asul at manipis, hindi upang takpan ang indise ng tropo.

Ang zoom na may gulong na nakasentro sa pointer (ang punto sa ibaba ng mouse ay nananatiling hindi nagbabago), gumagalaw na hila, dobleng klik upang bumalik sa buong tanawin, + at − keys, at ang susi na may persentahe para sa buong tanawin. Ang mga detalye ay angkop sa zoom: mas pinong mga baybayin, mga sub-square, mas makapal na mga isobare label.

## 5. Kung Bakit ang mga pagbaligtad ay bumabaluktot sa mga alon

Sa mababang atmospera ang temperatura ay karaniwang bumababa ng mga 6.5 °C bawat km; Ang refractive index ay nakasalalay sa parehong temperatura at [3] halumigmig, at kung saan ito ay nagbabago nang malaki sa taas ang alon ay tumutungo sa lupa at higit pa sa normal. Ang pinakamahalagang patong ay ang mga 3 km, kung saan nabubuo ang mga inversion, duct at convective cell [4]; ang mga landas ng dagat ay pinapaboran dahil ang masa ng tubig ay bumubuo ng malalaking area ng [4] inversion. [3]

Ang sukat na ginagamit upang sukatin ito ay refractive N, i.e. (n − 1) · 106 [11]:

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

Ang T ay ang temperatura sa kelvin, P ang presyon at presyon ng singaw, parehong sa hPa.

Ibinibilang bilang iba't ibang N na may kabahagi (dN/dh). Sa pamantayang atmospera ay humigit-kumulang −39 N/km; hanggang −79 N/km ang refraction ay itinuturing na normal, bukod pa sa super-refraction, at may positibong mga split ng sub-refraction [7]. Ibabang −157 N/km ang alon ay binaluktot higit pa sa kurba ng lupa at nananatiling nakabinbin: ito ang [11] (ducting) duct. Ito ang mga kondisyon na sinisikap hulaan ng mga mapa ng Hepburn at F5LEN.

## 6. Ang indise ng tropo: mula sa dalawang mapa hanggang sa katamtaman

Ang mga tab ng hepburn at F5LEN ay ang control desk: ipakita ang mga larawang na-download mula sa pahina ng bawat pinagkunan, na may nagbabagong oras ng paghula, at magsilbi upang i-cilibrate ang pagbasa sa pamamagitan ng pagpindot sa mga puntong reperensiya at mga kulay ng alamat. Ang tropo index layer ay nananatiling walang laman hanggang sa hindi man lamang ang isa sa dalawang calibrations ay hindi aktibo at kumpleto.

Hepburn. Sinusukat ng indise ng Hepburn kung gaano karaming alon ang inaasahang itupi sa isang lugar, sa isang linear scale mula 0 hanggang 10 na maaari ring lumampas sa 10. Ang 0 ay nangangahulugang pamantayang atmospera at tanging nakakalat; 1–2 gilid o mahinang super-refraction; 3–4 super-refraction na may duct areas; Hindi isinasaalang-alang ng index ang dinadaglat na tropo at hindi nakikita ang mga bundok ng [1]. Ang mga mapa ay sumasaklaw sa 6 na araw: ang 3-oras ay dumadaan sa unang 36 oras, pagkatapos ay 6 oras, inaapruba araw-araw hanggang 18 UTC [1]; ang simulang datos (temperature, halumigmig, presyon) ay mula sa global na modelo ng serbisyo ng panahon sa Canada [1]. [1]

F5LEN. Para sa Europa ang F5LEN ay naglalathala ng 31 mapa mula +3 hanggang +192 oras (bawat 3 oras hanggang +48, bawat 6 hanggang +96, pagkatapos ay sa bawat 12), na may petsa at panahon na may bisa sa pamagat, ang oras ng produksiyon sa ibaba kanan at isang alamat ng klase: Bisaya, Normal, Minor, Mahina, Mabuti, Malakas, Vy Strong, Duct [2]. Ang pandaigdigang prediksiyon ng F5LEN ay kinakalkula ng air refractionive index sa Unit N, sa 40 hakbang na 3 oras (mga 5 araw), na ini-apruba nang 4 na beses sa isang araw; ang isang klik sa mapa ay nagpapakita ng average na halaga at ang locator ng puntong iyon [2].

Pagpapayo. At sa bawa't hula ay ginagawa ang mga hakbang na ito:

1. Pagkakautang. Ang mga larawan ng dalawang hula ay ini - download sa cache na katabi ng maaaring gamitin, minsan upang i - update ang pinagmulan, hindi sa anumang reconstructure. Bago ang huling bersyon, ang pahintulot ay dapat hilingin sa Hepburn at F5LEN.
2. Awtomatikong georeferencing. Nasumpungan ng programa ang balangkas (dalawang pahalang at dalawang patayong linya ng parehong kulay) at, sa Windows 10/11, basahin ito kasama ng OCR na isinama sa sistema ang mga etiketa ng ehe (54N, 10W, 0, 5E.). Hiwain ang mga strip sa paligid ng balangkas, palakihin ang mga ito, suriin ang parehong palatandaan kapag ang titik na N/S/E/W ay hindi binabasa at pinananatili ang linya kung saan ang karamihan ng mga tatak ay bumabagsak: ang maling pagbasa ay nananatiling wala. Mula rito ay makukuha mo ang labis na lat/lon ng balangkas at ang uri ng projection (regular trough o Mercator). Ang kalabisan ay nagliligtas sa kanilang sarili at muling bumabasa sa kanilang sarili kung ang balangkas ay nagbabago. Sa Mac at Linux, kung walang OCR, ang mga dulo ng pabrika o sulat - kamay ay mabisa sa tab. Sa ibang paraan, para sa mga mapa na walang nakikitang palakol: Ang bawat uri ng mapa (Hepburn Europa, F5LEN sa buong mundo...) ay may kalibrasyon: ang ilang mga control points na may alam na mga coordinate (mga krus ng mga meridian at mga parallel, mga kilalang tadyang) ay nag-uugnay ng mga pixel sa latitud at longhitud, sa projection na ginagamit ng source na iyon. Ang calibration ay nasa isang file sa tabi ng programa at magagawa mo muli kung magbago ang isang source format.
3. Pagbasa ng kulay. Ang bawat pixel ay inihahambing sa alamat ng pinagmulan at ginagawang pinakamalapit na halaga ng numero. Ang mga gastos, mga sulat at mga grid na iginuhit sa orihinal na mapa ay kinikilala bilang “no data” at napupuno ng mga kalapit na halaga.
4. Pandaigdig na grid. Ang bawat imahen, minsang matagpuan ang balangkas at malaman ang mga kalabisan nito, ay inilalagay sa Lupa; ang halaga sa anumang punto sa daigdig ay hinango mula sa mga imahen na tumatakip dito. Kung mas maraming rehiyon ang sumasaklaw sa parehong punto na ito ay nagkakahalaga ng detalye (higit na pixels sa bawat digri), kung ang oras nito ay nasa loob ng 1.5 h mula sa pagpiling iyon; pagkatapos ang pinakamalapit na oras, kung gayon ang pinakabagong karera. Kung saan walang imahen ang sumasaklaw sa punto na ipinapakita ng mapa ang isang kulay abong lambong: walang datos, na naiiba sa “index 0”. Sa loob ng balangkas, ang isang pixel na walang isang alamat na kulay ay 0 (normal na atmospera); mga baybayin, hangganan at mga kasulatan ay naitalon sa pamamagitan ng pagtingin sa pinakamalapit na kulay sa loob ng 3 pixels.
5. Gayunding hagdan. Ang Hepburn ay nasa scale 0–10 [1]: madilim na background = 0 at sampung kulay ayon sa pagkakasunud-sunod (violet, asul, tubig berde, apog, dilaw, ocher, kahel, pula, rosas, mapusyaw na rosas) para sa 1...10, lila sa kabila. Ang F5LEN ay gumagamit ng mga klase (Disturbo, Normal, Minor, Mahina, Mabuti, Malakas, Vy Strong, Duct) [2], ngunit ang ilan ay may eksaktong mga kulay na Hepburn: Mahina = 1, Malakas = 5, Vy Strong = 7, Duct = 8; Minor ay 0.5 at Mabuti (green) 3.5. Ang scale multiplier, sa F5LEN tab, ay nagreresulta sa set upang gawin itong maihahambing kay Hepburn.
6. Sa parehong panahon. Ang timebar ay mula +0 hanggang +192 h sa mga hakbang na 3 oras. Sa bawat hakbang na kinuha mo mula sa bawat reperensiya ang hula na may bisa sa pinakamalapit na panahon; kung ang isang pinagmulan ay walang gayong hakbang, ginagamit lamang ng media ang isa at ipinakikita ito ng alamat.
7. Media. Sa pamamagitan ng Media button ang bawat punto ay nagkakahalaga (Hepburn + F5LEN) / 2; na may Hepburn o F5LEN makikita mo lamang ang isang pinagmulan, kapaki-pakinabang upang maunawaan kung saan ang dalawang hula ay hindi magkasundo.
8. Drowing. Ang halaga ay may kulay na gaya ng sa alamat (0 asul, 2 berde, 4 na dilaw, 6 na kahel, 8 pula, 10 purpura) at inilalarawan bilang isang halimbawa sa ilalim ng grid. Ang 0 ay malinaw, kaya kung saan walang nangyayari ay hindi mo nakikita ang malinis na saligang mapa; ang pangkalahatang transparency ay nakikibagay sa butas sa tabi ng kahon.

Sa table tab, ang indise sa ruta ay ang katamtamang mga kampeonato sa grid values tuwing 10 km sa pinakamalaking bilog sa pagitan ng dalawang istasyon.

## 7. Isobare

Ang mataas na matatag na mga presyon ang unang hudyat na hinanap para sa [3] trope, at ang mga koneksiyon na lampas sa IK3XTV na sinuring Alps ay may mataas na presyon ng [4].

1. Data. Ang presyon sa antas ng dagat mula sa global model GFS ng NOA [12], ay binasa sa pamamagitan ng free Open-Meteo [15] serbisyo sa isang grid ng 3° × 3° (mula 30° hanggang 72° N, mula 30° W hanggang 48° E), sa sumunod na 78 oras. Ini-update ng programa ang datos sa startup at pagkatapos ay tuwing 6 na oras, tulad ng modelong karera.
2. Mga linya. Ang isobare ay nakukuha sa grid tuwing 4 hPa (...1012, 1016, 1020...) na may algoritmo ng mga pagmamartsang liwasan (marching squares), pagkatapos ang mga ito ay bahagyang bilog at inaasahang iginuhit ng tract.
3. Mga Centre. Ang mga pinakamarkang lokal na maximum at minimum ay minarkahan ng H (blue) at L (pula) at ang halaga sa hPa.
4. Mga paputok. Ang halaga ay lumilitaw sa bawat isobara, sa isang kulay na likuran ng mapa upang manatiling mababasa sa itaas ng mga sampol.

Lugar at hakbang. Ang datos ay naka-download sa lugar na iyong tinitingnan, pinalawak ng 25%, na may isang hakbang mula 1st hanggang 20° pinili upang manatili sa ilalim ng 400 puntos (Open-Meteo ang bawat punto bilang isang kahilingan: 600 bawat minuto, 5000 kada oras): sa pamamagitan ng pag-abot o paglilipat ng mapa palabas ng lugar na naka-download na ang programa ng pinaka-makapal na reload, sa karamihan ng 10 minuto; kung ang isang update ay nabibigo sa nakaraang datos ay mananatili at ito ay masubukan muli pagkatapos ng 3 minuto. Ang ganitong paraan ay oobare at ang temperatura ay gumagana saanman sa daigdig.

Mga temperatura. Ang parehong kahilingan ay nagdadala ng temperatura sa 2 m, sa 925 hPa at hanggang 850 hPa. Ang LA0BY ay nagpapakita na ang kaganapan ng 22/11/2018 hanggang UA6 ay nakita sa mga mapa ng temperatura (900–300 m), ngunit hindi sa Hepburn [3] hula; DF5AI at DK5YA ay gumagamit ng mga vertical temperature profile upang kilalanin ang [5] inversions. Dahil dito ay may dalawang suson:

- Ang temperatura 850 hPa: isoterme treatment kada 4 °C (pula sa ibabaw ng sero, asul sa ibaba, lila sa sero).
- Angrmal inversion: orange na mga sampol kung saan ang hangin sa 925 o 850 hPA ay mas mainit kaysa roon sa 2 m; mientras mas matindi, mas malakas ang pagbaligtad. Ito ay ang pisikal na kondisyon na bumabaluktot sa mga alon sa lupa [3] [11], at ihambing ito sa tropo index at isobare na nagpapakita kung ang mga hula ay may meteorolohikal na base.

## 8. Mgapot at istasyon

Ang bawat batik na may parehong alam na posisyon ay isang linya sa pinakatugatog na bilog sa pagitan ng dalawang istasyon, na nahahati sa mga seksiyon at nakakalkula; Sa Mercatore ito ay lumilitaw noon nang bahagyang kurba, sa azimutale ito ay tuwid kung ito ay nagsisimula mula sa gitna. Blue line na punô ng normal na batik, dilaw na natatakpan ng madilim na gilid para sa airscatter (<AS>, <ACS> o “airscatterić sa komento), isang bola para sa bawat istasyon, light halo sa ilalim ng linya upang alisin ito sa mga sampol. Ang napiling lugar ay mas kadalasang idinisenyo para sa huli at may dalawang pangalan.

Pakikinig. Bawat Tagapag - ulat ng PSK Ang [16] receiving station ay isang madilim na kulay rosas na bilog na walang laman. Mula sa bawat simula ang mga linya na kulay rosas ay patungo sa mga istasyon na nag - alis at nagbalik sa network, na laging nakikita nang hindi pumipindot, na ang mga pangalan ay laging nakasulat. Sa pamamagitan ng pagpili ng bilog ang mga linya nito ay mas makapal. Makikita lamang sa spot at sa pakikinig ang banda at range na napili sa listahan: ang pagpapalit ng bandwidth ang mapa ay walang laman at recombined na may datos na natipon na para sa bagong banda.

Ipinasiya ko. Ang bawat istasyon na may mga decode na may WSJT-X, JTDX o MSHV (receipt sa pamamagitan ng UDP) ay isang pulang linya na puno mula sa iyong QTH hanggang sa istasyon, na may bola at nominasyon. Ito ang pinakatamang paghahambing sa paghula: ipakita kung saan talagang darating ngayon ang iyong receiver.

## 8 bis. Mga Colour at simbolo

Ang bawat kulay ay may iisang kahulugan lamang, pantay sa lahat ng tema (pagbabago lamang ng background).

| Kulay at karatula | Kahulugan |
| Patuloy na asul na linya | Spot mula sa kumpol ng DX, RBN o DM4X sa pagitan ng dalawang istasyon |
| Dilaw na linya na may madilim na gilid | Nakasuot ng airscatter comment (paglalarawan sa eroplano) |
| Matingkad na rosas na bilog na walang laman | Listahan ng Pakikinig sa PSKReporter (MQTT) |
| Manipis na kulay rosas na mga guhit mula sa bilog, na may kulay rosas na bola sa ilalim | Mga istasyon na narinig ng tagapakinig, na laging nakasulat ang pangalan; mas makapal na linya para sa napiling tagapakinig |
| Pulang linya at pulang pangalan | Ang iyong UDP decodings (WSJT-X, JTDX, MSHV): sino ka |
| Pulang linya na kumikislap | Ang QSO ay nagaganap sa istasyong iyon: Tinawag ka niya at ang pagsasara ay hindi pa dumarating, o kaya ay ang DX Calle ng WSJT-X/JTDX na may kakayahang paghahatid. Patuloy na gumamit ng RR73, RRR o 73 |
| Luntiang linya | Ang QSO desunti dal ay nabubuhay: pagsasara ng RR73, RRR o 73 UDP view |
| Itim na linya na may puting hangganan | May tumatawag sa iyo: tumatagal ito hanggang RR73/RRR /73 o hanggang 3 minuto |
| Liwasan ng 45° | Ang trapiko na may di - kukulangin sa isang istasyon sa labas ng napiling korona: Walang mga linya, mga liwasan lamang ang nakaladlad |
| Hanapin ang mga lilya na may kalayuan sa hilaga | 1000, 700 at 500 km sa paligid ng iyong QTH; ang pagpiling iyan ay mas kapansin - pansin |
| Bilugan at punto sa kulay ng tema | Ang iyong QTH, na may pangalan at may - ari ng lupa |
| Kahel ball na may GRAVES 143,050 | Ang radar GRAVES ( JN27SI) |
| Timplahan mula asul hanggang pula | Hepburn trope index (0–10) o F5LEN class, o ang kanilang katamtaman; mas mainit na kulay, mas mabuting mga kondisyon |
| Liwanag na kulay abong pelus | Walang datos ng tropo sa napiling panahon |
| Grey-blue linya na may halaga sa hPa | Isobberate tuwing 4 hPa; H sa asul na mataas na presyon, L sa pulang mababang presyon |
| Pula, asul at lilang guhit | Isothermia hanggang 850 hPA bawat 4 °C: pula sa ibabaw ng sero, asul sa ilalim ng sero, lila ang sero |
| Sample orange | Thermal reversal: hangin hanggang 925 hPa mas mainit kaysa sa lupa, mas matindi na may mas malakas na pagbaligtad |

Sa Spected Reports ang gilid ng bawat tab ay nagsasabi ng tuntunin sa bisa: berdeng Mabuti, pulang Scarta, abore no rule.

Ang mga liwasan ng lila ay nakakalat sa 45°. Ang trapiko na malayo sa iyong QTH ay kawili - wiling maunawaan mo kung saan bukas ang propagation, subalit ang mga linya nito, daan - daang kilometro ang haba at kadalasa'y sampu - sampu, ay tumatakip sa mapa at itinatago ang mahalaga para sa iyo: ang mga butas na maaari mong samantalahin mula sa iyong istasyon. Dahil dito, lampas sa napiling korona, hindi naguguhitan ng programa ang mga linya kundi kulayan ang mga liwasang locator (2° × 1°) ng mga himpilang nasasangkot. Ang resulta ay isang mapa ng «active zonesić: kung saan lumilitaw ang maraming kalapit na mga liwasan ng lilac, may isang padaluyan o isang butas, kahit na hindi ito tuwirang nababahala sa iyo; kung ang lugar ay lumalawak patungo sa iyo, ito ang hudyat upang ituro ang antena sa direksiyong iyon. Ang 45 dashboard ng° ay nagpapangyari sa iyo na makita sa ilalim ng indise ng tropo at ng grid, kung kaya't ihambing sa isang sulyap ang hinulaan (kulay - kape) at ang tunay na trapiko (illus). Ang inyong liwasang QTH ay hindi kailanman nagkukulay, sapagkat ito ang simula at hindi isang malayong lugar. Tanging ang mga parisukat lamang na nasa kabila ng korona ang may kulay: isang parisukat na tinawid ng korona, o loob, ang nananatiling malaya kahit na mayroong isa sa dalawang istasyon. Upang makita ang mga linya, patayin mo lamang ang kahon o pumili ka ng isang mas malaking korona.

Ang mga batayan na nagpapasiya kung ano ang nasa mapa at kung ano ang itinatapon ay ipinaliliwanag nang isa - isa sa dokumento 1, seksiyon 11 ter.

## 9. Ang timebar

Ibaba sa mapa: petsa at panahon ng prediksiyon na ipinapakita, humigit-kumulang mula +0 hanggang +72 h sa 6 oras, index legend 0–10 na may pangalan ng napiling pinagmulan, at panahon ng modelong lahi para sa isobare. Ang mga indise ng trumpeta at isobare ay sumusunod sa mistis; ang mga dako, istasyon na nakikinig at ang inyong mga decoding ay mga obserbasyon sa sandaling iyon at laging nananatili sa “adex”. Kapag ang census ay wala sa +0 ang mga ito ay intenuated, upang tandaan na ang mga ito ay hindi kabilang sa oras ng paghula.

## 10. Kung paano babasahin ang pagsasanib

- Mataas na presyon + mataas na index + spot sa kahabaan ng may kulay na lugar: ang pagbubukas ay naroon at siya ang binalak. Panahon na upang tumawag sa direksiyong iyan.
- Mataas na indise ngunit walang batik: o walang sinuman ang aktibo roon, o ang paghula ay lubhang optimistiko. Ang suson ng pakikinig ay tumutulong upang makilala: kung may mga receiver sa dakong iyon at wala itong nararamdaman, ang ikalawang haka - haka ay mas malamang.
- Mahabang batik sa labas ng mga lugar na may kulay: mataas na duct, na hindi nakikita ng ibabaw, gaya ng sa pangyayaring LA– UA6 [3]; o ibang propagation ang nakatakas mula sa pansala (e.g. Elektibong walang tag). Ang DF5AI at DK5YA ay nagtatangi rin ng tropo at Elastiko mula sa oras: mga bukasan na ipinanganak at nagwawakas kasama ang isang difficial Etendent ay hindi [5] tropo.
- Hindi magkasundo ang Hepburn at F5LEN: ihambing ang dalawang mapagkukunan sa mga buton at tingnan kung saan nahuhulog ang mga tunay na batik.
- Ang pressure down: G4MCU ay sumunod sa isang opening sa SK6VHF parola hanggang sa bumaba ang presyon; 1012 hPa ang nagturing sa tapos na pangyayari [13]. Masdan mo ang isobare sa pamamagitan ng pag-claim ng timebar: kung ang mataas na presyon ay umurong, malamang na kasunod nito ang pagbubukas.
- Signals na tumataas: sa pag-aaral ng WSPR sa 144 MHz ng KP4MD ang signal/noise ratio ay lumaki sa mga bintana kung saan hinulaan ni Hepburn ang isang pagpapabuti, at bumaba nang ang propesiya ay kumuha ng [14]. kung sa ganoon ding landas lumalaki, nabubuo ang butas.
- Mga landas sa kabundukan: hindi isinasaalang-alang ng index ang [1] reliefs, ngunit ang mga koneksiyon sa kabila ng Alps ay umiiral rin dahil sa mga convective cell at alon ng atmospheric gravity [4], at ang LA0BY ay gumagana ng 9A at S5 sa pamamagitan ng mga bundok nang hindi ito inihuhula ni Hepburn na [3].

## 11. Buksan ang punto

- Pagpapahintulot ng Hepburn at F5LEN na gamitin ang kanilang mga mapa sa programa.
- Calibration of Conversion codiences F5LEN → scale 0–10.
- Calibration ng mga control points ng anumang uri ng mapa, upang muling gawin kung ang mga mapagkukunan ay nagbabago ng graphics.
- Mapagpipiliang ruta na may temperatura sa taas (850 hPA) mula sa GFS.

## Pangunahing mga pinagmumulan

1. W. R. Hepburn, DX Info Centre, “ Ang Hepburn Tropo Index (HI)” at “Tropospheric Ducting EX Forecastić page (dxinfocentre.com). Indise ng kahulugan at aspeto, negatibong mga pagpapahalaga at higit sa 10, hindi pag-aalis ng mga kalat at ginhawa, mga hakbang na 3 at 6 oras sa 6 na araw, upgrade sa 18 UTC. Pinagmulan ng datos mula sa global Canadian model: G. Meisse, AMSAT-BB, 27/08/2007.
2. F5LEN, propesiya para sa Europa (tropo.f5len.org/forecasts-for-European) at pandaigdigang prediksiyon ng VHF tropopheric propagation (tropo.f5len.org/WWW) at kilalang “Bagong bersyon ng VHF tropospheric propagation equiption instructor na pang-ekonomiks. Unit index N, 40 hakbang mula sa 3 oras, 4 na updates bawat araw, halaga at locator bawat pindot.
3. S. Heck LA0BY, “Extreme Tropo Propagation on 144 MHz and upiler, 41°N NoN VUSHF Pagpupulong, Skjeberg 2019. Normal na lebel na 6.5 °C/km at inversions hanggang 10–15 °C/km, temperatura at tansyang papel, mataas na matatag na presyon, LA– UA6 pangyayari nakikita sa mataas na temperatura at hindi sa Hepburn, LA–9A/ S5 mga koneksiyon lampas sa mga bundok.
4. F. Egano IK3XTV, “Propagation VHF – Ang kontribusyon ng mga selulang convective at atmospheric gravity waves sa Tropo propagation.” Kahalagahan ng unang 3 km, mga landas sa dagat, mga convective cell, mga gravity wave sa Alps, mataas na mga koneksiyon ng presyon, karaniwang volume hanggang 3–4 km.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, “Very Long Distance Propagation sa 144 MHz Bandić, 24/7/2003. Ang mga vertical profile ng temperatura at hamog ay tumuturo upang makilala ang mga inversions, paggamit ng Hepburn index, pagkakaiba sa pagitan ng tropo at Etendensya ayon sa oras.
6. J. N. Gannaway G3YGF, “Tropospheric dinadaglat na Propagation”, QST Nobyembre 1983. Ang lupa ay umiikot na katumbas at ang pagkawala ng landas na ginagamit sa index at spot card (tingnan ang dokumento 1).
7. Ang VHF/UHF DX Book, kabanata 2 “Tropospheric propagation”, p. 2-23...2-30. Larawan ng refractive zones ayon sa dN/dh (standard −39 N/km, limits −79 N/km at 0) at ibabaw at mataas duct profile. Ang teksto ng ibinigay na kopya ay hindi mabasa: tanging ang mga bilang lamang ang ginamit.
8. Project buod ng 8/11/2026, na inilaan ng may akda ng programa. Ang mga kulay ng mapa, ay nangangailangan ng mga usli, patong, timebar.
9. Likas na Lupa, ventor data ng mga baybayin at hangganan, public domain (natural Earthdata.com). Pangkalahatang reperensiya, hindi isinanib.
10. J. P. Snyder, “Map Projections – A Working Manual”, USGS Professional Paper 1395, 1987. Mga larawan ni Mercator at ng azimutale equidtanti. Pangkalahatang reperensiya, hindi isinanib.
11. Mungkahi ITU-R P.453, radio refractive index. Refractive na pormula Ang N at duct carrier sa −157 N/km. Pangkalahatang reperensiya, hindi isinanib.
12. NOAA NCEP, Global Eventation System (GFS). Ang presyon sa antas ng dagat sa 0.25°, apat na karera bawat araw. Pangkalahatang reperensiya, hindi isinanib.
13. G4MCU, mga artikulong may tag “tropospheric” (g4mcu.net/tag/tropospheric), 2018–2025. Ang refration, bidyo at pagsasabog; mas madalas na mga duct na may mataas na presyon, lalo na sa dagat; ang parola SK6VHF ay sumunod hanggang sa pagbaba ng presyon sa 1012 hPa (Hunyo 2019).
14. C. F. Milazzo KP4MD, “VHF/UHF WSPR Propagation Studyić (qsl.net/kp4md/144_mhz_wspr.htm), 2012–2014. Ang SNR ng WSPR ay nag-uulat sa 144 MHz na lumalaki sa mga pagpapabuti ng mga bintana na hinulaan ni Hepburn at bumabagsak kapag ang hinulaan ay naglaho.
15. Open-Meteo, weather hinulaang API (bukas-meteo.com), malayang di-commercial na paggamit. Ang presyon sa antas ng dagat ng modelong GFS sa maramihang puntos sa isang kahilingan.
16. Mqtt.pskreporter.info, distribusyong MQTT sa tunay na panahon ng mga patalastas ng pskreporter. Info: nominasyon at kasera ng mga tumatanggap (tingnan ang dokumento 1).
