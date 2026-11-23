NOBYEROPOVHF · teknikal na dokumento 1
# Pag - opera ng programa

> Awtomatikong salin ng Italyano: Kung pag - aalinlanganan ay sulit naman ang giyang Italyano.

Kung ano ang ginagawa ng programa, mula sa kung saan nito kinukuha ang impormasyon at kung paano ito ginagawang kung ano ang nakikita mo sa mapa at sa talaan. Bersyon ng 9 Nobyembre 2026, kaugnay ng proyekto sa C# (.NET 10 + Avalonia).

## Bilang sumaryo

- Layunin: tropo propesiya (Hepburn [3], F5LEN [4], isobare GFS [18]) at real na panonood ng mga banda sa parehong window, ayon sa LA0BY [1] control list.
- Data koleksiyon: dalawang kumpol ng DX, Reverse Beacon Network [9], dm4x cluster at PSKReporter [5] MQT street, UDP decoding mula sa WSJT-X, JTDX at MSHV [8].
- Spot: mga bahagi ng banda, negatibong filter, airscatter in slow yellow, posisyon ng mga locator o C ALL3.TXT [6] [7].
- Sino ang nakikinig: itim na mga bilog na kulay rosas na may kulay rosas na mga linya na laging nakikita sa mga istasyon ay nakadarama ng [5].
- Spot card: distance, azimut, tropo index sa landas at troposcatter love sa pamamaraang G3YGF [2].
- Wikas Hepburn at F5LEN: [3] imahe at calibration bench [4].
- Bantay: Pagkontrol ng NTP sa 10 server bawat 3 minuto [10] [11].
- Naghihinalang mga ulat: EME sa Q65/JT65 na kinikilala sa posisyon ng Buwan, ang mga locators, mga advancers, mga quarant at mga tagapakinig, na may mga tuntuning natutuhan sa pamamagitan ng araw [20].
- Criteria para sa pagtatapon at kahulugan ng mga kulay na binuod sa dalawang talahanayan (seksiyon 11 ter at 11 quater).
- Sila, 36 [17] mga wika, mga dokumento sa pagluluwas sa Salita sa pamamagitan ng editable mga pormula [16], portable program at multiplatform [12] [14] X4Q [15]. [13]

## 1. Layunin

Ang programa ay naglalagay sa isang solong window ng dalawang bagay na karaniwang tumitingin sa iba't ibang lugar: ang prediksiyon ng tropofer propagation (Tropo index ng Hepburn at F5LEN, isobare) at ang obserbasyon ng kung ano talaga ang nangyayari sa mga banda (spot mula sa kumpol ng DX, Reverse Beacon Network at PSKReporter, at ang talaan ng mga istasyon na nakikinig). Ang pangkat na reperensiya ay 144 MHz; ang mga komersyal ay nagtitipon mula sa 50 MHz pataas.

Ang pagtatakda ay sumusunod sa listahan ng kontrol na iminungkahi ng LA0BY para sa mga sukdulang kaganapan ng tropo: sa mga nakaraang araw ay tinitingnan mo ang Hepburn at F5LEN propesiya at ang profile ng temperatura sa altitud, habang ang pangyayari ay nasa ilalim ngway na hinahanap mo ang kumpol ng DX at sa spot maps ang ebidensiya na ang pagbubukas ay nagsisimula sa [1]. Napansin din ng LA0BY na ang tanging mga mapa ng Hepburn/ F5LEN ay hindi sapat upang hulaan ang pinakamahabang mga koneksiyon [1]

## 2. Ang bintana at ang mga tab

Ang bintana ay isa at hindi nahahati sa mga bintana. Mula sa itaas hanggang sa ibaba:

- General setting row: Calle and Lountor sa kaliwa; sa kanan ng UTC clock (e.g. 08/11/2026 20:33:38 UTC, amber text) na sinundan ng NTP:OK o NTP:FAILED, ang temang menu at ng wika.
- Bilang ng mga tab sa limang grupo: Mapa at lugar; Mga hula (Propria β, ang hula na kinalkula ng programa at inilarawan sa Di - tiyak na Manwal, pagkatapos ay Hepburn at F5LEN); GRAVES; Data at mga pinagmumulan (DX cluster, PSKReporter, Reverse Beacon, UDP, Appointments at kahina - hinalang Reports); Guides (Program Operation, Map Building, Paano huhubog ang tropo, Manual prescription, Tips to the operator, na may kahon ng pananaliksik na lumilitaw sa teksto). Sa ilalim ng sinag ay makikita ang mga sub-wika ng grupo; naaalala ng bawat grupo ang huling ginamit.
- Nasisiyahan sa tab at, sa ibaba, ang status bar na may estado ng mga mapagkukunan, ang database ng mga pangalan at ang panahon ng huling update.

Ang itaas na mga setting ay kumakapit sa lahat ng mga tab: ang napiling tema ay nagkukulay din sa koleksiyon ng datos at pinagmulan ng mga dokumento; ang mga pahina ng dokumento ay nananatiling kasingputi ng isang pilyego, kaya ang mga ito ay naglilimbag nang mahusay.

## 3. Tawag at may - ari ng lupa

Ang kabesera ng Maidenhead ay maaaring magkaroon ng 4 o 6 na karakter (hal. JN70 o JN70CN). Inikukumberte ito ng programa sa mga coordinate ng sentro ng liwasan: larangan ng 20° × 10°, liwasan ng 2° × 1st, subquadrate ng 5° × 2,5′. Kung ang teksto ay hindi tanggap na locator ang gilid ng kahon ay nagiging pula at ang istasyon ay hindi lumilitaw sa mapa. Nagsisilbi rin ang locator bilang sentro ng azimuth projection at bilang simulang punto para sa mga distansiya at azimuth sa tablang ad.

Ang Tawag ay ang pangalang ginagamit bilang isang cluster login. Sa unang simula, hanggang sa magkaroon ng tanggap na nominasyon, ang mga kumpol ay hindi nakikipag-ugnayan: ang isang bintana ay humihingi ng pangalan (at, hindi sapilitan, ang kabesera); nakumpirma, ang pangalan ay kinokopya bilang isang username sa field ng Login ng bawat kumpol na mayroon nito na walang laman at kumpol ay umaalis. Sa “Later” ay nananatili pa rin, at ang connect button ng bawat kumpol ay nangangailangan muli ng nominasyon. Ang MQTT, UDP at NTP ay hindi gumagamit ng login at umalis sa paano man.

## 4. koleksiyon ng mga impormasyon: sources

Ang bawat source ay may sariling seksyon sa Data Collection task, na may connected/Disnected, ang mga kinikilala at nagdurugtong na kahon sa startup, status, metro at rehistro ng mga tinanggap na hanay. Hindi lumilitaw sa rehistro ang mga hindi nabaklas o itinapong linya mula sa pansala: bilangin lamang ang mga seksyon metro. Ang mga bahagi ay magkaagapay na may dalawa hanggang dalawa.

| Pinagmumulan | koneksyon | Kung ano ang nagdadala |
| 1. Pagpili ng kumpol ng DX | on0dxk.dyndns.org:8000 command set/fitter dxbm/pas 6 | Spot ng mga baguhang radyo, kasama rin ang SSB at CW. Sa ON0DXK ang VHF ay sinasala at ito ay pinangyayari sa ipinahiwatig na utos (pasa 4, 2.). |
| 2. Ikalawang pinagmumulan | dxmaps.com:7300 (DXMAPS, EA6VQ) at s50clx.si:41112 ( S50CLX, Slovenia) | Ang DXMAPE ay nangongolekta ng VHF spots at nagbibigay rin ng RBN flowr spots (set/skimmer control, itinakda na); ang S50CLX ay isang kumpol ng DX Spider. Login ang iyong pangalan; editable addresss. |
| 3. Baligtad na Beacon Network | Telenet.reversebeacon.net 7000 CW/RTY · 7001 FT8 | Awtomatikong lugar ng mga magnanakaw. Login na may pangalang [9] lamang. |
| 4. Cluster kasama ng PSKReporter | dm4x.dn.net:850000 | Digital Spot ng PSKReporter na naka-locate. |
| 5. PSKReporter MQTT | Mqtt.pskreporter.info 1883 · 1884 kasama ang TLS | Bawat real-time reception report: na naghahatid, nakikinig at ang kanilang mga [5] Locators. Bands 144, 222 at 432 MHz. |
| 6. Lagay ng Panahon at mga mapa ng tropo | dxinfocentre.com/tropo_eur.html tropo.f5len.org/forecasts-for-European modelong GFS | Hepburn [3] at F5LEN [4] tropo indexs, sea level pressure para sa isobare (GFS via Open-Meteo [18]). Ang Hepburn at F5LEN ay dapat payagan sa mga may akda. |

Cluster telnet (mga telce 1–4). Pagkatapos ng koneksyon ang programa ay naghihintay sa kahilingan ng pangalan (text na naglalaman ng login, tawag:, callsign o tawag mo); kung hindi ito dumating sa loob ng 4 segundo, pakisuyong ipadala ang login. Pagkatapos ay ipadala ang mga utos ng seksiyon, na nakahiwalay sa punto at komma, isa sa bawat 0.6 segundo. Ang mga telenet control character ay tinatanggal bago basahin ang mga linya. Kung ang koneksyon ay bumagsak at si Riconnetti ay aktibo, subukan muli pagkatapos ng 10 segundo (15 kung ang server ay hindi man lamang tumutugon) [6].

PSKREporter MQTT (oras 5). Ang mga nakatala sa programa sa paksang pskr/fiilter/v2/ifilbanaić/{modo}/{tx_call/{x_call/{/{tx_grid}/{rx_grid}/{x_dx_dx_dxccrx_dx_dx_dxccc sa pamamagitan ng pag-aayos lamang ng banda at pag-iwan sa iba pang mga field na libre kasama ang karakter na + [5]. Ang eksaktong pangalan na ipinapahiwatig ng server ang mga banda (halimbawa ay 2m, 1.25m, 70cm) ay dapat tingnan sa unang connection.

## 5. Mula sa cluster line hanggang sa lugar

Ang isang linya ay isang spot kapag ito ay may anyong DX de SPOTTER: frequency DX comment hhmmZ. Ang programa ay nakakakuha ng spotter, DX station, frequency sa kHz, comment at ngayon UTC; ang mga pangalan ay nag-aalis ng mga stuffixes (-#, -1.). Pagkatapos ay ikapit, sa ganitong pagkakasunud-sunod, ang mga sumusunod na tuntunin [6].

| Band | Tinanggap ang Segment |
| MGA GRAVE (may 144) | 143,040 – 143,060 MHz |
| 144 MHz | 144 – 148 MHz |
| 222 MHz | 220 – 225 MHz (Isa lamang Rehiyon 2 ITU, Amerika) |
| 432 MHz | 420 – 450 MHz |
| 1296 MHz | 1240 – 1300 MHz |
| 2320 MHz | 2300 – 2450 MHz |
| 10 GHz | 10,5 GHz |

1. Band. Ang frequency ay dapat mahulog sa isa sa mga pinangangasiwaang bahagi; lahat ng iba pa, at partikular na ang bawat komersyal sa ilalim ng 49 990,0 kHz, ay hindi ipinapakita at binibilang bilang “out ng band”.
2. Paraan. Isa sa mga kilalang paraan ng pagkokomento ay FT8, FT4, Q65, JT65, ISCAT, CW, SSB, FM, RTTY, PSK31, FSK441, JT6M, X7Q); USB at LSB ay naging SSB. MSK144
3. Negatibong pansala. Ang batik ay itinatapon kung ito ay naglalaman ng mga mode na MSK144, FSK441, JT6M, JTMS, ISCAT o ang mga tags <AUE> <MS> <FAI> < F2 >, na may mga non-sensitive uppercase at maging mga espasyo sa loob ng mga bracket. Ito ang mga paraan at prosagasyon na hindi tropo: Nagkalat ng meteoro, aurora, at paminsan-minsan, F2.
4. Hangin. Kung ang komento ay naglalaman ng <AS> o <ACS> (nahalo rin, e.g. <As>) o nagpapahayag ng lugar na iyon bilang airscatter (airscatter, hanging nakakalat, pagsabog ng eruplano), ang lugar ay nananatiling dilaw na may guhit na nakakalat (dark wavy upang manatiling nababasa sa malinaw na mapa) at itinatampok sa talaan.
5. Lokasyon ng mga istasyon. Kung sa komento ay may dalawang locator (e.g. JN54 < JN83) ang una ay sa mga gumagawa ng spot at ang pangalawa sa istasyon DX; na may isang locator, iyon ay mula sa istasyon ng DX. Ang kulang ay hinahanap sa C ALL3. TXT, na may eksaktong pangalan lamang (para sa mga laptop na makikita sa ibaba). Kung ang isang istasyon ay mananatiling walang posisyon, ang lugar ay lumilitaw sa talaan na may notang “un kilalang positioni” ngunit hindi sa mapa.
6. Dalawang beses. Pinapalitan ito ng isang komersyal na may parehong DX, parehong spotter, parehong pinanggagalingan ng isang naroroon na, sa halip na magdagdag.

Ang C ALL3.TXT ay ang pangalang database → locator na ginagamit ng JTDX, sa format na CALL,LOCATORE,,,,,. Ang talaksang proyekto ay naglalaman ng 1 048 847 na pangalan, lahat ay may tanggap na locator; ang header ng file ay nagsasalita pa rin ng 34 323 na pangalan dahil nanatili ito sa unang edisyon [7]. Nakakarga ito sa likuran sa simula, kaya ang bintana ay agad na magagamit. Ang file ay hinahanap sa tabi ng programa, sa Data folder, sa folder kung saan sinimulan ang programa at isa o dalawang folder sa itaas; Kung wala, ang isang bintana ay nagsasabi dito kapag ikaw ay nagsimula at ang status bar ay naghuhudyat dito sa pula. Laging ginagamit ng pangalan nito ang 6-character nito. Tanging tuntunin para sa mga locator: kung ang isa ay nakatanggap (spot, MQT, decoding, QSO desunti) ay may wala pang 6 na karakter at C ALL3. Ang TXT ay may isa hanggang 6, ito ay palaging nagkakahalaga ng C ALL3.TXT; para ang mga laptop (F/ IC8TEM, IC8TEM/P) ay isa lamang eksaktong tinig: kung wala, ang istasyon ay nananatiling walang kabesera at hindi dinisenyo sa ibang lugar. Ang tuntunin ay kumakapit din sa panahon ng pagguhit, kaya ang impormasyon ay dumating bago ikarga ang database.

Isang database: C ALL3.TXT. Mula 1.3.4 lahat ng mga locators ay nasa isang file, C ALL3.TXT, na nananatili sa format ng JTDX at WSJT-X (CALL,LOCATORE,,,,,) at maaaring gamitin din bilang sa WSJT-X, JTDX at MSHV. Wala nang hiwalay na talaksan. Sa tuktok ng file, pagkatapos ng header, may dalawang blocks na minarkahan ng comment line: // [LIVETROPOV-CLUSTER] na may mga pagtutuwid mula sa partikulo; pagkatapos ay // [LIVETROVF-FONTI] kasama ang lahat ng iba pang mga bagay na nasa alpabetong pagkakasunod-sunod. Ang bawat nominasyon ay lumilitaw minsan. Sa sublingual Data and Sources › Appointments (C ALL3) na hinahanap mo ang isang pangalan, isinusulat mo ang may-ari ng lupa at idiniriin mo ang Add / pagbabago: ang boses ay napupunta sa bloke ng mga tinig sa pamamagitan ng kamay at ito ay sulit higit sa lahat, kahit na sa ibabaw ng locator na ipinadadala mismo ng istasyon. ∫ Tinanggal ang nominasyon mula sa C ALL3.TXT (ang susunod na update ay muling kumukuha nito mula sa mga pinagkunan, kung mayroon sila nito). Magdagdag ng file... basahin ang isang listahan CALL, LOCATOR (tanggapin mo rin ang punto at komma, tabulasyon at espasyo) at isulat ito kabilang ang mga boses na may-picked; ang napiling file ay hindi kinokopya. Muling pagkarga muli ng C ALL3.TXT mula sa disk, pagkatapos na isaayos ito sa pamamagitan ng isang editor. Buong pagkakasunud-sunod ng mga mapagkukunan: sariling locator, mga tinig ng kamay, mga cluster recision, ang natitirang C ALL3. Ang TXT (sources, leart live locators at G7VRD), ay nakatanggap ng locator (na kumakapit lamang sa database kung ito ay may 6 na karakter at hindi nagtatapos sa LL o MM).

Updated ng C ALL3.TXT. Sa parehong sub-wika ang Update button ngayon download ang mga pinagkunan (C ALL3.TXT ng WSJT-X pinahusay, C ALL3.TXT EME ng W7GJ, grid_data.bin ng JTDX), ay sumasama sa kanila (ang una na may nominasyong panalo), suriin na ang mga ito ay hindi bababa sa 50,000 mga pangalan (ang iba ay walang pagbabago), ay nagliligtas sa C ALL3. TXT Ang nakaraang C ALL3. Nananatili ang mga entidad ng TXT, at isang 6-character locator ang nananatili kung saan ang isang pinagmulan ay mayroon lamang 4 ng parehong liwasan. Ang mga tinig at pagtutuwid ng kamay mula sa kumpol ay nananatili sa itaas, hindi nagbabago. Taglay ang kahon ng Update araw - araw, ang tseke ay nagaganap sa pasimula. Ang programa ay muling nagsusulat ng C ALL3. TXT kaagad pagkatapos ng bawat tinig sa pamamagitan ng kamay at, para sa mga mag-aaral locator, sa karamihan ng 10 minuto at sa pagsasara; ang C ALL3. Ang TXT ng pakete ay binabasa-lamang at ang attribute ay nag-aalis ng sarili nito. Ang mga installer ng Linux ay hindi dumadaig sa isang C ALL3. Ang TXT ay naroroon na; na para sa Windows ay naglalagay lamang nito kung wala ito. ALL3

Awtomatikong pagtutuwid mula sa kumpol. Sa DM4X at RBN automatic spot ang komento ay nagdadala ng dalawang locator, halimbawa ang KN09tp<> KN09: ang una ay ang ipinahayag ng istasyon na may batik sa sarili, sa 6 na karakter; ang pangalawa ay ang nararamdaman ng istasyon, kadalasan hanggang 4. Kung ang una ay may 6 na karakter (at hindi nagtatapos sa LL o MM) at naiiba sa database, ang programa ay nagtutuwid sa C ALL3. TXT lamang: ang pagtutuwid ay napupunta sa bloke // [LIVTROVHF-CLUSTER], na may petsa ng huling kumpil sa ikatlong larangan, ito ay may bisa sa ibabaw ng mga pinagkunan (sa ilalim ng mga tinig ng kamay) at lumilitaw sa pagitan ng kahina-hinalang mga hudyat na inaapruba bilang "C ALL3 ". Kaya ito ay nakaligtas sa C ALL3. TXT updates. Ang isang di - napatunayang pagtutuwid sa loob ng dalawang taon ay nawawala muna at nananatiling isang normal na tinig.

Huling reperensiya: G7VRD. Kung ang isang nomine ay walang 6-character sa C ALL3. TXT, tinatanong ito ng programa sa likuran ng publikong API ng G7VRD (api.g7vrd.co.uk, "locator by callsign"), na kusang nagtitipon ng mga locator mula sa trapiko ng network. Tinatanggap mo lamang ang isang 6-character na hindi sentro ng liwasan (LLL o MM); ang mga kahilingan ay pinagpangkat-pangkat, sa pinakamataas na 40 nominative sa bawat minuto, at bawat nominative ay nagtatanong minsan sa isang araw, dahil ito ay isang serbisyong hubbistiko na walang garantiya. Ang locator na natagpuan ay pumasok sa C ALL3. TXT kung ang pangalan ay wala roon o mayroon lamang 4 na karakter ng parehong liwasan; ang paghahanap ay nagtatapos sa kahon sa pangalang sublingual.

Mga kontrata sa bar. Sa mga paksa ng PSKREporter MQTT, ang bar ay naghihiwalay sa mga antas, pagkatapos ang mga pangalan ng compound ay may puntos sa halip ng bar ( IV3. HB9CAT). Ipinabalik ng programa ang barangay at, kung ang mensahe ay naglalaman ng mga buong pangalan, gamitin ang mga ito: IV3/HB9CAT nananatiling IV3/HB9CAT sa mapa, talaan at database.

Ang mga mangangaso ay natuto ng live. C ALL3. Ang TXT ay hindi naglalaman ng lahat: halimbawa SP9EMF at SP9NVY ay wala roon, at pagkatapos ay nananatiling 4-character locator na ipinadala ng PSKReporter (ang card ay nagsasabi ng gayon). Upang mabawasan ang mga kasong ito ang programa ay sumusulat sa C ALL3. TXT bawat 6-character na nagmula sa anumang source (PSKReporter, tulad ng transmitter o bilang receiver, cluster, UDP decoding) para sa isang nominasyon na C ALL3. Ang TXT ay wala o may 4 na karakter lamang sa parehong liwasan; hindi kailanman nagbabago ng boses sa 6 na karakter. Hanggang 1.3.3 ang mga locator na ito ay nasa Data/ LOC6.TXT.

Pagbabago mula 1.3.3. Sa unang simula ng 1.3.4 ang mga files bukod sa mga nakaraang bersyon (Dati/C ALL3 -MANUALE.TXT, ang Data/C ALL3 -AGIUNTE, Data/C ALL3 -CLUSTER. TXT, Data/ LOC6.TXT at Data/C ALL3 - G7VRD.TXT) ay pinagsama sa C ALL3. TXT na may parehong entidad. Habang muling isinusulat, kinukuha ng mga lumang file ang ekstensyon ng.fuso, hindi ka na nagbabasa at maaari mo nang i-delete.

Awtomatikong mga tagatanggap. Ang RBN, dm4x at PSKReporter ay kadalasang nag-uulat ng mga pangalan na may hulapi na nagpapahiwatig sa receiver: -SDR, /SDR, -# (skimmer RBN) o -1, -2... (mga tagatanggap ng parehong istasyon). Para sa kabesera ang suffix ay tinanggal: Ang OK2OZL -SDR ay kumukuha ng OK2OZL JN89UE mula sa C ALL3.TXT kung ang istasyon ay hindi pa nagpapadala ng 6-character locator. Nananatili ang pangalang ipinapakita na may hulapi.

Nagputol ang mga mangangaso. Hindi lahat ng mga pinagkukunan ay nagreresulta sa pagpapalawig ng kanilang kasera: marami ang nagtakda nito sa 4 na mga karakter, at ang ilang mga sistemang awtomatikong nagdadala-over ay sadyang nagreresulta sa 4-character locator, kahit na ang pagkuha ng datos mula sa tunay na pakikinig ng FT8, FT4, Q65 at JT65, o pagkumpleto nito sa sentro ng liwasan (L o MM, e.g. JN89MM). Dahil dito, ang isang 4-character, o 6-character na nagtatapos sa LL o MM, ay pinapalitan ng C ALL3. TXT ng parehong liwasan. Kung ang tamang lokasyon at ang gitna ng liwasan ay hindi kasabay ng ilang dosenang km, ang pagkakamali ay nananatiling maliit kung ihahambing sa mga distansiya ng tropo.

## 6. Sino ang nakikinig

Ang bawat mensaheng MQTT mula sa PSKReporter ay nag-uulat ng pangalan at loctor ng mga tumatanggap ng [5], kaya't hindi mo na kailangang hanapin ito sa C ALL3.TXT. Para sa banda na napili sa pilipino ang programa ay humahawak, para sa bawat tumatanggap na istasyon: kabesera, huling pagtanggap, decoded methods, bilang ng mga istasyon na nararamdaman at sukdulang layo. Ang isang istasyon ay umaalis sa listahan kapag hindi na ito tumatanggap ng anumang bagay sa napiling range (3, 5, 10, 15, 30 o 60 minuto).

Sa mapa ang bawat receiver ay isang madilim na bilog na kulay rosas, at mula sa bawat isa ay nagsisimula ang mga linya na kulay rosas patungo sa mga istasyon na nakadarama at nagpapadala pabalik sa lambat. Ang mga linya ay laging nakikita, nang walang tikpik. Ang mga pag - aatas ng mga istasyon ng pakikinig, yaong mga naririnig at ang dalawang ulo ng bawat lugar ay laging nakasulat sa mapa, nang hindi na kailangang mag - click (ang parehong pangalan sa iisang punto ay isinusulat minsan lamang). Sa pamamagitan ng pagpindot sa bilog ang mga linya nito ay nagiging mas makapal at binubuksan ang tab sa pamamagitan ng mga detalye. Kung ang locator mula sa MQT ay wala pang 6 na karakter, ang programa ay gumagamit ng C ALL3.TXT (mga isang milyong pangalan), kung ito ay tumutugma sa parisukat na natanggap.

Ikalat ang bulalakaw. Katulad ng mga cluster spot, ang mga ratio ng MQT sa MSK144, FSK441, JT6M, JTMS o ISCAT ay itinatapon at binibilang lamang; ang parehong pagtungo sa mga decoding ng UDP sa mga paraang ito.

Hindi tunay na mga tagapakinig. Ang ilang mga istasyon ay nagdedeklarang tatanggap sa 144 MHz ngunit sa katunayan ang receiver ay nasa ibang banda (maling frequency recoration): ang kanilang mga ulat ay magdidisenyo ng hindi nagreresulta na mga pagbubukas ng libu-libong km. Sinusuri ito ng programa sa ganang sarili. Bawat istasyon na sinasabi ng isang tagapakinig na dinggin sa VHF ay sinusundan sa loob ng 30 minuto sa lahat ng banda, na may dedikadong inskripsiyong MQTT (pskr/filter/v2/+/{nominative}/#) Ang [5]. ay nagsisilbing patunay. Kung ang hindi bababa sa dalawa sa mga himpilang narinig ng isang tagapakinig ay, sa parehong mga minuto (±10), aktibo sa ibang banda, ang tagapakinig ay itinuturing na hindi tunay at ang lahat ng mga ulat nito VHF ay itinatapon; ang MQT rehistro ay nag-uulat ng pangalan at mga istasyon na nagtatak sa kanya. Sa talaan ng mga batik, katabi ng mga metro, ipinapakita ng buton ng MQT ang estado ng daloy (green leaf kung konektado, pula kung hindi) at kung ilang mga tagapakinig ang itinapon sa banda at range na pinili; sa pamamagitan ng pagpindot nito ay binubuksan ang table sa pamamagitan ng listahan ng mga itinapon at, para sa bawat isa, mga istasyon na matatagpuan sa ibang banda.

Mahabang koneksyong porselana. Ang isang link na mahigit 1000 km (Clumbang Spot, MQT Pakikinig, QSO Desunti) ay hindi agad ipinapakita: nananatiling hawak, at ang counter “d > 1000 km” ay nagsasabi ng bilang. Isinisiwalat ito kapag ang isa pang koneksiyon, sa pagitan ng iba't ibang istasyon at di - kukulangin sa kalahati, ay nagpapatunay rito sa pamamagitan ng isa sa mga pamantayang ito: ang istasyon ding iyon sa isang ulo (halimbawa ang iisang tagapakinig) at ang isa pang istasyon sa iisang direksiyon sa loob ng 10°; ang katulad na pasilyo, na ang dalawang ulo ay nasa loob ng 200 km mula roon sa kabilang linya; ang magkatulad na ruta, na ang direksiyon ay nasa loob ng 10° at ang layo ay nasa loob ng 200 km, ay bumagtas sa ruta. Parehong lumitaw at ang logo ng MQT ay nagpapakita ng dahilan. Ang mga decoding ng UDP nito ay palaging nakikita at nailalapat bilang pagpapatunay sa iba. Nananatiling nasa alaala ang pinanatiling mga koneksiyon sa loob ng 60 minuto.

Walang anuman sa ilalim ng 144 MHz. Ang programa ay gumagana mula 144 MHz up: ang lahat sa ilalim ng 143 MHz ay itinatapon. Ang eksepsiyon ay GRAVES, ang French radar transmitting sa CW noong 143,050 MHz ni Broye-lès-Pesmes ( JN27SI): isang komersyal sa pagitan ng 143,040 at 143,060 MHz, o may GRAVES sa pangalan, ay lumilitaw sa pagitan ng 144 MHz na may radar bilang isang transmitter. Ang tablang ad ay nagsasabi kung ang receiver ay nasa loob ng sinag (sectors D, C, B, A mula 90° hanggang 270° na nakita mula sa radar, 6 na hakbang mula sa 7,5° bawat isa) o palabas; ang tab GRAVES, gaya sa YA-MS, ay nagpapakita ng kalangitan na nakikita mula sa radar: mga sektor, hakbang na maliwanag sa panahong iyon (sequenza PE1ITR 2021: 0,8 s bawat hakbang, siklo 4,8 s at ang Datack mula sa YM-S. Mahirap madama ito sa trope, ngunit maaari itong mangyari.

Rehabilitasyon mula sa mga kumpol ng tao. Ang isang istasyon na lumilitaw rin sa ibang banda ay hindi ibinibilang na suspek kung, sa parehong minuto (±30), ang mga kumpol ng tao ng DX ( ON0DXK, GB7RAU; hindi dm4x, RBN o iba pang mga awtomatikong mapagkukunan) ang aktibong pinsala sa banda na pinagtatalunan, sa mga batik na ginawa ng ibang pangalan: ito ay ang kaso ng mga istasyon na talagang gumagawa sa maraming mga banda nang magkakasama. Sa gayon ang isang itinapong tagapakinig ay nagbabalik na nakikita (ang MQT rehistro ay sumusulat ng “rehabilitated”) at ang isang mahabang-tatag na link para sa mga mababang banda ay maaaring i-uncreach.

Aktibong mga istasyon sa ilalim ng 144 MHz. Sa pagkukuwarentenas ng mga mahahabang koneksiyon, ang isang istasyon na sa parehong mga minuto ay naririnig sa mga banda sa ilalim ng 144 MHz (mula sa mga ulat ng MQTT ng iba pang mga banda) ay hindi nagpapatunay sa iba pang mga koneksiyon at hindi nailalantad, bagaman ang isang katulad o kahalintulad na ruta ay magpapatunay dito.

## 7. Listahan ng mga patalastas

Sinasakop nito ang 35% sa kanan ng Map tab at spot. Itaas: pamagat, band pilitor (50, 70, 144, 222, 432, 1296, 2320 MHz, 10 GHz), range selector (huling 3, 5, 10, 15, 30, 60 minuto) at Update key. Ibaba, apat na metro: nakikitang mga batik, AS/ACS, itinapon mula sa filter, mga istasyong nakikinig.

Ang bawat linya ay nagpapakita ng uri ng linya, ang oras na UTC, DX → DE, ang daan, ang layo sa km at, sa ikalawang linya, ang pinagmulan, dalas at komento. Ang pinakabago ay nasa itaas. Isang pindot sa linya ang pumipili ng lugar: sa mapa ay tumutubo ang linya at lumilitaw ang dalawang pangalan.

## 8. Pagtanggal ng spot card at troposcatter

Para sa napiling spot ang tab ay nagpapakita ng locators, distansiya, azimut, frequency, mode, source, ngayon, komento, ang karaniwang tropo index sa kahabaan ng landas at isang pagtatantiya ng pagkawala ng ruta para sa kalat-kalat na tropo.

Ang distansiya at azimut ay kinakalkula sa sukdulang bilog na may Earth circle na 6371 km (formula ng emisenoverso sa malayo, sa simula ay zimut mula sa istasyon ng DX hanggang sa spotter).

Nawala ang troposcatter. Ang pangunahing kawalan, para sa dalawang istasyon sa makinis na lugar, ay ang kabuuan ng malayang pagkawala ng kalawakan at kawalan ng pamamahagi: G3YGF [2]

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

Ang mga pagkalugi ay nasa dB, D ang layo sa km, f ang dalas sa MHz, θs ang anggulo ng diffsion sa digri.

8497 Ang km ay ang katumbas na radiasyon ng Lupa (4/3 ng pisikal na isa). Ang θA at θB ay depende sa abot - tanaw ng bawat istasyon:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

kasama Ang taas ng hadlang na mula sa abot - tanaw hinggil sa istasyon at sa layo nito. Ang bawat antas ng abot-tanaw ay nagkakahalaga ng halos 10 dB [2]. Hindi alam ng programa ang abot-tanaw ng mga istasyon, kaya't ipinapakita nito ang pangunahing pagkawala na may θ = θB = 0 at ipinapahayag ito ("mainit na lupa"). Ang G3YGF ay nagdaragdag ng terminong klima, kung saan ang N ay reactivity sa lupang [2]; ito ay gagamitin kapag ang N ay makukuha mula sa modelong weather:

```math
L_{N} = 0,2 \cdot (N - 310)
```

Veripikasyon: para sa 110 km hanggang 144 MHz ang pormula ay nagbibigay ng 166,5 dB, laban sa mga 168 dB kama sa tsart ng artikulo. Sa halagang 500 km hanggang 144 ang MHz ay nagbibigay ng halos 206 dB. Ang inaasahang signal/noise ratio ay ang kapasidad ng sistema (eirp + epektibong sensitivity ng receiver) minus ang pagkawala: na may G3YGF 144 MHz model system (198 dB) isang 500 km path ang magbibigay ng humigit-kumulang −8 dB sa SSB [2], na ang koneksyon ay lamang sa mas mahusay na proparasyon ng purong pagkalat.

## 9. Ang mga decoding ko: UDP mula sa WSJT-X, JTDX at MSHV

Ang WSJT-X, JTDX at MSHV ay maaaring magpadala ng UDP sa bawat linya na kanilang decode. Ang programa ay nakikinig sa 127.0.0.1:2237 at inuulit ang parehong pakete, byte per byte, sa 127.0.0.1:2238, kaya ang iba pang mga programa na gumamit ng port na iyon (GridTracker, JTAlert, isang logger) ay patuloy na gumagana kung ilipat mo ito noong 2238. Ang mga direksiyon at mga pinto ay nagbabago sa sublingual ng UDP ng Date Collection; bilang isang pahayag sa pakikinig ay maaari mo ring gamitin ang isang multicast group (e.g. 224.0.1), kung ang programa ng decoding ay nakatakda ng ganito.

Ang mga pakete ay sumusunod sa protocol ng WSJT-X network: isang paunang magic number 0xADBCBDA, numerong schema at tipo ng mensahe. Ang programa ay pangunahing gumagamit ng tatlong uri ng [8]:

- Pagtibok ng puso: sabi ng programang ito na naghahatid ng impormasyon at ng anong bersiyon.
- Katayuan: dalas ng pag-aalsa at mode; mula sa dalas na nakukuha mo ang banda (na may iisang mesa ng mga batik).
- Decode: Ngayon, SNR, panahon at dalas na pag - aaksaya, pag - uugali at teksto ng mensahe.

Mula sa teksto na nakuha mo sa istasyon na narinig mo: sa CQ 9 A2PQR Ang JN83 ay ang istasyon pagkatapos ng CQ (nagliligtas ng DX, EU, TEST...), sa IK0JKL IT9MNO JM68 ay ang ikalawa, i.e. na naghahatid. Ang locator ay ang mensahe, kung may (4 na karakter, na iba sa RR73); kung hindi ay naghahanap ka sa C ALL3. TXT. Sila'y nakikinig sa mga linya: mga istasyon na naririnig ng iyong tagatanggap ngayon. Sa mapa, ang mga ito ay nagiging pulang linya mula sa iyong QTH hanggang sa istasyon, na may pangalan; ang suson ay nagliliwanag sa pamamagitan ng Decoded box mula sa akin at nangangailangan ng isang tunay na locator. Sa talaan sa kanan ay isang counter ang nagsasabi kung gaano karaming istasyon ang inilulunsad mo sa napiling banda.

Ang QSO desunti dal ay nakatira. Kapag ang isang decoding ay isang pangwakas na mensahe sa anyong C ALL1 C ALL2 RR73 (o 73 o RRR), halimbawa IC8TEM IK3FHP RR73 o IK3FKP SV8PEX 73, ang dalawang istasyon ay nakapagtapos ng isang link. Ang programa ay nagtatala nito bilang QSO, na may loctor ng bawat kinuha mula sa huling mensahe kung saan ipinahayag niya ito o, kung hindi, mula sa C ALL3.TXT [7], at iginuguhit ito sa mapa bilang isang berdeng linya sa pagitan ng dalawang istasyon (strate “QSO desunti dal live”). Ang isang pares ng mga istasyon ay nagbibilang minsan sa bawat banda; ang banda at range na pinili sa talaan ay may bisa.

Sa parehong sub-wika, ang Debug pane ay nagpapakita ng teksto ng bawat natanggap na pakete (type, oras, halaga at mensahe), sa kahilingan din hexadecimal bytes, at isang linya para sa forwarding. Ito'y upang suriin na ang programa ng pag - aalis ng impormasyon ay aktuwal na nagpapadala ng impormasyon.

## 9 bis. Mga Wikang Hepburn at F5LEN

Bawat isa ay nagpapakita kung ano ang nakukuha sa programa. Ang buton “Basahin ang pahina at i-upload ang lahat ng mga imahe”Binubuksan ang pinapahiwatig na pahina (bababa sa dxinfocentre.com/tropo_eur.html para sa Hepburn [3] at tropo.f5len.org/forecasts-for-europe para sa F5LEN [4]) at i-upload ang lahat ng mga imahe ng cachemarket sa tabi ng recoutable; maaari mo ring idikit ang direksiyon ng isang imahe. Para sa bawat imahen ay may isang kahon upang ipuwera ito at isang bukid sa oras ng paghula. Ang 31 Europeong imahe ng F5LEN ay ipinangalan sa panahon ng modelong karera (03.png... 48.png bawat 3 oras, 54......26 bawat 6 na oras, 108... 192 bawat 12 oras) at ang programa ay kumukuha ng oras mula sa pangalan ng [4] file; para sa Hepburn ito ay sulit sa pagkakasunud-sunod ng pahina. Sa F5LEN tab, ang menu ay pumipili ng rehiyon: Europa (tropo.f5len.org/forecasts-for-europe), Australia (tropo.f5len.org/Australia, vk03.png... vk192.png) o Atlantiko sa pagitan ng Cape Verde at Irlanda (tropo.f5len.org/atlantlant-ocean/atic-d4-ei, atl03.png...pl2.png...p2.p.). Bawat isa ay may pare - parehong hakbang.

Nasa ibaba ang calibration counter. Ang isang pindot sa larawan ay nagdaragdag ng control point (locator o lat,lon: kailangan mo ng hindi bababa sa 6) o isang alamat na kulay na may halaga. Kinakalkula ng programa ang pixel na tumutugma sa → na nagtutugma sa ikalawang digring polynomial interpolation sa pinakamaliit na mga liwasang [19], binabasa ang mga kulay na may entrance na pinili at, para sa F5LEN, binabago ang mga unit na N sa 0–10 scale na may · N + b. “Save calibration at subukang basahin ang ” sa Tarature folder at tingnan kung aling pixel ang bumabagsak sa iyong locator. Bago magtatag ng isang calibration, dapat humingi ng pahintulot sa may akda ng mga mapa.

Mga rehiyon. Ang bawat source ay may higit pang mga rehiyon, bawat isa ay may mga nakapirmeng-name na imahe: Ikinalulungkot ko, Ikinalulungkot ko. Ang task ng bawat pinagkunan ay nagpapakita una sa lahat ng mga preview ng pagsasanib: lahat ng naka-download na mga imahe na pinagsama-sama gaya ng sa pangunahing mapa, na may dotted pane at pangalan ng bawat rehiyon, isang kulay abong belo kung saan walang imahe ang sumasaklaw sa lugar at isang slider upang makabuo ng mga oras; hanggang sa kaliwa ang talaan ng mga rehiyon, na ang bawat isa ay may kahon upang gamitin ito, ang bilang ng mga mapa ay binabasa at mula sa kung saan nanggagaling ang mga labis (OCR, sa pamamagitan ng kamay o pabrika; kung ang OCR ay hindi binabasa ang teksto). Ang manwal na calibration bench ay nananatili sa likod ng homoymous button. Ang Bench Region menu ay pumipili kung alin ang tingnan, ang kahon “Use Ang rehiyong ito sa map” ay isinama ito sa index. Ang balangkas ay matatagpuang mag-isa (dalawang pahalang na linya at dalawang patayong linya ng parehong kulay); upang ilagay ang imahen ay nagsisilbi sa mga sukdulang lat/lon ng balangkas: para sa Hepburn Europa (10° W–40° E, 27°–56°N) at F5LEN Europe (15° W–40° E, 30°–65° N) ay nasa programa na, sapagkat ang iba pang rehiyon na kanilang isinusulat sa apat na O E S N fields na nagbabasa ng axis labels. Ang scale multiplier ay gumagawa sa dalawang mga mapagkukunan na magkahalintulad: pangwakas na halaga = multiplier × aspeto ng alamat. [3] F5LEN [4] F5LEN

## 9 ter. Pag - uulat ng Salita

Ang dalawang dokumento ay nangunguna sa “Export to Wordiching button. Ang file ay nakasulat sa Office Open XML format; ang mga pormula ay nagiging Word equations (OML), kaya ito ay nananatiling editable sa [16] ekwasyon editor. Kung ang bintanang pang-ligtas ay hindi magagamit ang file ay nagtatapos sa Documents folder sa tabi ng produceutable.

## 9 quater. Sino ang tumatawag sa akin

Kung ang isang mensahe ay ipinadala sa iyo sa lokal na UDP (ang unang pangalan ay sa iyo, halimbawa IC8FAX JN55 X2Q o IC8FAX IK3FHP -12), ang programa ay bumabagtas mula sa iyong QTH tungo sa istasyon na tumatawag sa iyo na isang itim na guhit na nakakalat at kumikislap, na may pangalan at locator na katabi mo. Ang locator ay nagmula sa C ALL3.TXT (kasama ang mga boses ng kamay) o mensahe. Ang linya ay nananatili hanggang sa ang UDP ay pumasa sa pagsasara ng QSO sa istasyong iyon ( RR73, RRR o 73, sa isa sa dalawang direksiyon), sa karamihan ng 3 minuto mula sa huling tawag; ikaw ay kumukuha sa lahat ng bagay at hindi sumusunod sa mga batayan ng abstraksyon, kaya ang isang direktang pagtawag ay hindi kailanman nakatakas. IK3FHP

Ang PSKREporter, sa pamamagitan ng MQT, ay hindi naghahatid ng teksto ng mga mensahe: ay nagsasabi lamang kung sino ang tumanggap, na may dalas, paraan at SNR. Ito ang dahilan kung bakit ang isang direktang pagtawag ay kinikilala lamang ng sarili nitong mga decoding ng UDP.

## 10. Pagkontrol ng Oras (NTP)

Ang mga digital mode at spot time ay nangangailangan ng eksaktong orasan. Tuwing 3 minuto, ang programa ay nagrerecord ng 10 NTP servers (SNTP on UDP 123): 0.pool.ntp.org, 1.pool.ntp.org, 2.pool.ntp.org.org, 3.pool.ntp.org, ito.pool.ntp.org.org., 1.pool.gle, ntp1.inrim.it, ntp2.inrim.it, pttime1ptptptgo., optp.org.org., time.comle.comptfloud.comp., Xcrecloud.comreploud.comrefloud.comploud. [11]

Sa bawat kasagutan ay tinataya ang pagtatapon at pagkaantala ng pagbabalik, itinatapon ang di - normal na mga pamantayan at kinukuha ang median, na may kalkulasyon ng pagtatapon at pagkaantala na inilalaan ng SNTP [10]. Sa hindi bababa sa 3 sagot ay sumasang-ayon ang UTC na orasan ng programang paayon at ang susunod ay lumilitaw na NTP: OK sa berde; kung ang mga sagot ay hindi sapat na lumitaw NTP:FAILED sa pula at ang orasan ay nananatiling tulad ng sa kompyuter. Ang orasan ng sistema ay hindi nahahawakan, dahil ang mga pahintulot ng administrador ay kakailanganin; ang lahat ng mga iskedyul ng programang panloob (spots, 5–60 minutong bintana, decodings) ay gumagamit ng magkakahanay na oras. Ang kontrol ay laging nagbabago sa background: sa interface ay nakikita lamang ang kalalabasan sa tabi ng orasan; ang mga server, intervals at minimum na mga tugon ay nagbabago sa PLETROPOVHF.ini (ntp_servers, ntp_minutes, ntp_min).

## 10 bis. Pangkalahatang mga tuntunin ng hindi pagtanggap

Ang dalawang kahon sa layer bar ay kumakapit sa lahat ng bagay na iginuhit mo (spot, PSKReporter, decoding, QSO desunti):

- Lumabas sa ilalim ng 100 km. Ang trapiko sa pagitan ng mga istasyon na wala pang 100 km ay maaaring hindi mahalaga para sa tropo (ground wave, optical range): na may on-box na hindi mo iginuguhit at hindi makikita sa listahan. Nagsisimula na ang lahat.
- Mahigit 1000 km: mga liwasan lamang. Sa paligid ng iyong QTH (ang locator na nakatakdang magsimula) ay may koronang 1000 km sa lahat ng direksiyon. Kung isa lamang sa dalawang istasyon ng isang link ang nasa labas ng koronang ito, ang koneksyon ay hindi iginuguhit bilang isang linya: sa lugar nito sila ay nagkukulay sa lilac, nakakalat sa 45°, ang mga liwasang locator (4 na mga karakter) ng mga himpilang nasasangkot. Kapit din ito sa mga istasyon ng PSKReporter na nakikinig sa mahigit 1000 km, na lumilitaw bilang isang liwasan sa halip na isang bilog. Hanggang sa ngayon nakikita mo ang trapiko nang hindi pinupuno ang mapa ng mga linya. Kung walang tunay na locator ay mahalaga ang haba ng koneksyon. Ang liwasan ng inyong QTH ay hindi kailanman nagkukulay ng lilac, kahit na nakikinig ka sa isang istasyon sa kabila ng korona: ay ang itinakdang locator, anumang pangalan na gumagamit ng nagpapatakbo mula sa istasyon. Ang korona ay pinipili sa tabi ng kahon: 1000, 700 o 500 km. Ang kahon na Corone 1000/700/500 km ay humihila sa tatlong korona sa paligid ng QTH, na iginuhit sa lilac, na may pinaka-markang pagpili at ang distansiyang nakasulat sa hilaga. Sa talaan ng mga batik ang layo ay sinusundan ng. Nagsisimula na ang lahat. Ang pamantayan ay kumakapit pagkatapos ikukuwarentenas: ang mahahabang koneksiyon na nananatili pa rin ay hindi lumilitaw na parang mga liwasan.

Ang mga liwasan ng lila ay nakakalat sa 45°. Ang trapiko na malayo sa iyong QTH ay kawili - wiling maunawaan mo kung saan bukas ang propagation, subalit ang mga linya nito, daan - daang kilometro ang haba at kadalasa'y sampu - sampu, ay tumatakip sa mapa at itinatago ang mahalaga para sa iyo: ang mga butas na maaari mong samantalahin mula sa iyong istasyon. Dahil dito, lampas sa napiling korona, hindi naguguhitan ng programa ang mga linya kundi kulayan ang mga liwasang locator (2° × 1°) ng mga himpilang nasasangkot. Ang resulta ay isang mapa ng «active zonesić: kung saan lumilitaw ang maraming kalapit na mga liwasan ng lilac, may isang padaluyan o isang butas, kahit na hindi ito tuwirang nababahala sa iyo; kung ang lugar ay lumalawak patungo sa iyo, ito ang hudyat upang ituro ang antena sa direksiyong iyon. Ang 45 dashboard ng° ay nagpapangyari sa iyo na makita sa ilalim ng indise ng tropo at ng grid, kung kaya't ihambing sa isang sulyap ang hinulaan (kulay - kape) at ang tunay na trapiko (illus). Ang inyong liwasang QTH ay hindi kailanman nagkukulay, sapagkat ito ang simula at hindi isang malayong lugar. Tanging ang mga parisukat lamang na nasa kabila ng korona ang may kulay: isang parisukat na tinawid ng korona, o loob, ang nananatiling malaya kahit na mayroong isa sa dalawang istasyon. Upang makita ang mga linya, patayin mo lamang ang kahon o pumili ka ng isang mas malaking korona.

## 11. Mga Tema at mga wika

Themes: ang 18 karaniwan sa mga programa ng IC8TEM, katulad ng YA-NEC at E-LAYER ANIR: Malinaw, Gabi, Itim, walong pastel (blue, kahel, aquamarine, berde, rosas, lilac, indigo, kayumanggi), Scia meteorica (YA-MSEP), ang tatlo sa IP-TRX-CTRL (Chiaro, Nonte, Nero) at Blue, Purple, Green. Mula 1.3.4 ang Chiaro ay mukhang YA-NEC: mapusyaw na aboyor na background, may hangganang puting panel, asul na gabing tab at asul na tuldik. Sa madidilim na tema rin nagiging madilim ang base map. Ang antas ng kulay ng panghuhula ay pareho pa rin sa lahat ng tema. Sa makikitid na iskrin (sa ilalim ng 1500 pixels, halimbawa ay 1366×768) ang orasan sa itaas ay nagpapakita lamang ng oras, kaya ang tema, dami ng mga teksto at wika ay nananatiling nakikita. Mga wika: ang ika - 28 wiki ng YA-MS [17] (Ingles, Travel, Deutsch, Nederlands, Français, Español, Português, Dansk, Norsk, Svenska, Íslenska, Eeti, Lietuvi, Latviešu, Руский, Руский,кра, Türkaلרبيا May Arabe, Hebreo at Urdu ang buong bintana mula sa kaliwang bintana; bilang XQ. Ang mga pagsasalinwika ay tungkol sa interface; ang dalawang teknikal na dokumento at mensahe ng status bar ay nananatili sa Italyano. Ang mga pangalan ng mga araw ay nagmula sa internasyonal na settings ng. NET para sa piniling wika. [12]

Ang home at wika ay nagbabago mula sa dalawang mga menus sa itaas kanan, sa tabi ng orasan, at mag-play agad, nang hindi muling binubuklat ang programa at hindi sumasabad sa koleksiyon ng datos. Ang pagpili ay natitipid sa PHETROPOVHF.ini.

## 11 bis. Naghihinala at natututo - sa - sarili na mga ulat

Ang Spected Reports tab ay nangongolekta ng lahat ng bagay na nasusumpungan ng mga kontrol ng programa na kakaiba, na inipangkat sa pamamagitan ng tipo at pangalan, na may huling 24 oras na nakatala. Ang programa ay natututo sa ganang sarili: para sa bawat mag - asawa (type, pangalan) ang positibong mga resulta (+) at kabaligtaran (−), at pagkatapos ng 3 napagkasunduang mga resulta ay lumilikha ng isang awtomatikong tuntunin; isang kabaligtarang resulta ang nag - aalinlangan dito. Ang mga desisyon ng tagagamit (Good, Poor) ay palaging kumakapit; ang buntalang buton ay ibinabalik ang desisyon sa programa. Ang mga tuntunin ay nasa Data/APPRENDIMENTO. TXT, katabi ng programa; ang mga awtomatikong hindi pinagtibay sa loob ng isang taon ay nalilimutan.

| Uri | Kailan | Exit + / − | Epekto ng Pamamahala |
| EME ( Q65/JT65) | Q65 o JT65 mahigit 1000 km na ang Buwan ay nasa itaas ng abot - tanaw sa magkabilang ulo, o EME bilang komento | − tuwing pangkaraniwan ang Buwan | Discard: EME station, ang mahabang Q65/JT65 s nito ay hindi rin isinasama nang walang karaniwang buwan. Mabuti: itinuturing na isang tropo |
| Mahabang koneksyon | Mahigit na 1000 km, sa pagkukuwarentenas | + Walang takip, ang − ay natapos nang walang kumpil | Mabuti: ang haba ng palabas sa istasyon. Itapon: hindi kailanman |
| Tagapakinig | PSKReporter receiver na nakaririnig ng mga aktibong istasyon sa ibang banda | − itapon, + binago ng mga kumpol ng tao | Mabuti: huwag kailanman itapon. Nakatapon: laging |
| Naiibang kabesera | Square tinanggap iba sa C ALL3. TXT | + tuwing uulitin mo ito | Mabuti: ang istasyon ay lumipat, sulit ang pagtanggap sa liwasan. Discard: valve C ALL3.TXT |
| Sentro ng Liwasan | 6-character na may LL o MM |  —  | Impormasyon lamang |
| Awtomatikong tagatanggap | Pag - aatas na may -SDR, /SDR, -# o -n |  —  | Impormasyon lamang |

EME. Ang Q65 at JT65 ang mga paraan ng komunikasyon sa pamamagitan ni Luna: Sa teoriya ang mga ito ay nahuhulog sa distinct sugnay tulad ng nakakalat na meteoro, ngunit ginagamit din sa tropo. Kinakalkula ng programa ang posisyon ng Buwan sa pamamagitan ng mababang-presiyong pormula ng Almanac Astronomical (mga 0.3°) [20]: kung ang isang koneksyon sa Q65 o JT65 ay lumampas sa 1000 km at ang Buwan ay nasa itaas ng abot-tanaw sa parehong ulo, ito ay halos tiyak na EME at hindi isinama (ito ay mahalaga sa hindi isinama). Kung ang Buwan ay hindi karaniwan, ang koneksyon ay nananatiling posible sa trope at dumadaan sa pagkukuwarentenas ng mga mahahabang koneksiyon, ngunit ang hudyat ay nananatiling nakikita. Kumakapit ito sa mga kumpol, MQTT at sa mga decoding nitong UDP.

## 11 ter. Itapon ang mga pamantayan ng pakikipag - ugnayan, nang isa - isa

Nais ng programa na ipakita lamang ang tropopheric propagation sa 144 MHz at sa kabila. Ang bawat pumapasok na datos (Cluster Row, PSKReporter Report sa pamamagitan ng MQT, UDP decoding) ay dumadaan sa isang serye ng mga filter, laging sa parehong pagkakasunud-sunod. Ang isang itinapong datos ay hindi nawawala sa wala: ay nabibilang (contactect ng hindi kasali sa upper bar) at, kapag ang dahilan ay hindi tiyak, ay lumilitaw sa tab Sulumang Reports.

|  N.  | Criteria | Nasaan ito? | Kung Ano ang Napatalsik | Maaari ka bang magbago? |
|  1  | Band | cluster, MQTT, UDP | Lahat sa ilalim ng 143 MHz (binabaybay ding 50 at 70 MHz), maliban sa GRAVES 143,050 MHz. Ang mga bandang ginanap ay 144, 222, 432, 1296, 2320 MHz at 10 GHz | Hindi. |
|  2  | Satelayt QO-100 | cluster, MQTT, UDP | 2400,0–2400,5 MHz at 10489,5–10490 MHz: ito ay geostationary satellite traffic, hindi tropo | Hindi. |
|  3  | Kumalat ang Meteor | cluster, MQTT, UDP | Ang JT6M at ISCAT (binabaybay ding ISCAT-A/B), ay nagkokomento sa pamamagitan ng "FIL" at non-tropoxy propagation labels (<AU>, <AUE>, <FAI>, < F2 >) sa tuwina. MSK144, FSK441 at JTMS (na mayroon ding <MS>) na mahigit 250 km lamang: sa loob ng 250 km tinatanggap natin, sapagkat sa distansiyang iyon ginagamit din ang mga paraang ito sa tropo | Hindi. |
|  4  | EME (via Luna) | cluster, MQTT, UDP | EME na nakasulat sa komento; Q65 o JT65 mahigit 1000 km kapag ang Buwan ay nasa itaas ng abot-tanaw sa magkabilang ulo; ang mga istasyon ay natutunan bilang EME | Oo: Mabuting tuntunin sa kahina - hinalang mga hudyat |
|  5  | Hindi-Tunay na Tagapakinig | MQ | PSKReporter receiver na nagsasabing makarinig sa 144 na istasyon na aktibo sa oras na iyon sa ibang banda (hindi bababa sa dalawa): Itinapon ang kaniyang mga kaugnayan. Bumalik na mabuti kung ang mga kumpol ng tao sa DX ay magpapatunay sa mga istasyon ding iyon | Oo: Mabuti / Mahirap |
|  6  | Quarantine ng mahahabang koneksiyon | lahat ng linya | Mahigit 1000 km isang link ang nananatiling nakatago hanggang sa makahanap ito ng kumpil: ang parehong istasyon sa parehong direksiyon, isa pang ruta sa isang pasilyo ng 200 km, o kahalintulad na ruta sa loob ng 10° at 200 km sa kahabaan ng hindi bababa sa kalahati. Walang kumpil na natatapos pagkatapos ng 60 minuto | Oo: Mabuti /ff |
|  7  | Sa ilalim ng 100 km | mga linya at talaan | Sa on-box, ang pinakamaikling koneksiyon ng 100 km (ground wave, optical range) | Kahon, sa simula |
|  8  | Sa labas ng korona mula sa QTH | linya | Kung ang isang istasyon ay higit 1000, 700 o 500 km (sa iyong pagpili) mula sa iyong QTH, walang linya: tanging ang mga liwasan ng lilla lamang na sakop ng dalawang istasyon. Ang iyong QTH square ay hindi kailanman kulay | Kahon at paikot, sa 1000 km |
|  9  | Orasang bintana | lahat | Mas matatandang batik ng napiling range (mula 3 hanggang 60 minuto) | Si Menu sa itaas |
|  10  | Doble | cluster | Ang parehong spot (same DX, parehong spotter, parehong banda, parehong pinagmulan) ay paulit ulit | Hindi. |

Pansinin ang mga batayan 4 at 6. Ang isang koneksyon sa Q65 o JT65 na mahigit 1000 km na walang Karaniwang Buwan ay posible sa tropo: ay hindi itinatapon sa pamantayang 4, ngunit nagreresulta para sa pagkukuwarentenas ng pamantayan 6 bilang anumang ibang mahabang kawing. Ang tuwirang pagtawag sa iyo (seksiyon 9 quater) ang tanging eksepsiyon sa mga batayang 7 at 8: ang mga ito ay laging inilalapit.

Ang hindi pag - aaksaya. Ang mga pagtutuwid ng locator (C ALL3. TXT sa pamamagitan ng mga boses ng kamay, mga pagtutuwid mula sa kumpol at marunong locators, suffixes -SDR, sentro ng square LL/MM) pagbabago kung saan ka gumuhit ng isang istasyon, hindi ito itapon. Sa kabilang dako naman, ang isang istasyon na walang kasera ay hindi maiguhit: ang natitira sa listahan na may layong «i—i—.

## 11 quater. Mga Colour at simbolo

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

## 12. Mga Pagtatakda at Pakikipagsapalaran

Window. Sa simula, ang bintana ay sumasakop sa 90% ng kapaki-pakinabang na espasyo ng screen (pinaputol ang application bar), nakasentro, kaya sa isang 15-16 pulgadang kuwaderno na may 125% o 150% resize ang titulong bar ay nananatiling nakikita. Kung ang kapaki - pakinabang na espasyo ay wala pang 1280 × 720 puntos, ang bahaging bintana ay nakaangat. Ang laki, posisyon at katayuang maximum ay natitipid sa PHERTROPOVHF. Ini sa pagsasara at pagtatapos sa susunod na simula, bigyan ng title bar ang isang screen (halimbawa pagkatapos alisin ang isang panlabas na monitor); kung hindi ikaw ay babalik sa 90%. Ang minimum na sukat ay 900 × 560.

Ang programa ay nabibitbit: hindi kailanman sumusulat sa AppData folder o sa rehistro. Ang lahat ng bagay ay nasa kulungan ng mga bagay na maaaring alisin: NOBYEROPOVHF.ini (Settings, na nilikha sa unang pagsasara), C ALL3.TXT (na may mga boses na sulat-kamay, pinag-aaralan ang mga pagtutuwid at locator), mapa cache at weather data, mga tuntuning natutunan (Dati/APPRENDIMENTO.TXT) at varietyTROPOVHF.db archive. Maaari ka ring manatili sa isang USB stick, habang ang folder ay sinusulat.

Ad archive (SQLite [14]). Ang bawat spot na tinatanggap at bawat PSKReporter na nakikinig na ulat ay natitipid sa PHETROVHF.db, isang SQLite database sa isang file. Sa memorya lamang ang huling 60 minuto ang natitira para sa talaan at mapa; pinanatili ng arkibo ang lahat ng bagay at nagsisilbi para sa mananalaysay at estetika (binabaybay ding decodings ng UDP), halimbawa ang mga bukasan para sa banda at bawat araw, ang mga pinakamalayong distansiya, ang mga parisukat ay umaabot, at ang likurang paghahambing sa pagitan ng inaasahang indise at ang tunay na mga lugar. Kinabibilangan ang mga pirasong itinapon mula sa panulaan ngunit hindi ito arkibo.

## 12 bis. Awtomatikong instalasyon at mga update

Mula sa bersyon 1.0 ang programa ay nag-download na ng handa mula sa Release page ng github.com/ic8tem/LIVETROVHF, sa parehong mga pakete ng YA-MS: installer para sa Windows (64 bit, 32 bit at ARM, i-install sa C:\LIVETROVHF nang walang karapatan sa administrador) at portable stage; para sa Windows SP1 at 8.1 mga pakete (-window7-4x -ow-OVHF; sa pamamagitan ng mga paketeng Arch at pp pp, pp, pp, pp, pp, pp, at pp pp pp; at pp pp pp; at pp pp; at pp pp pp pp; at pp pp pp pp; at pp pp; at pp pp pp pp pp; at pp; at pp pp pp pp pp pp pp pp pp pp; at pp; at pp ( Sa pamamagitan ng mga paketeng Linux, ang programa ay kinokopya sa unang simula sa ~/LIVTROVHF, kaya ang mga setting at data ay nananatiling katabi ng programa; sa Mac data ay ipinanganak sa tabi ng PHENTROVHF.app.

Tuwing 30 minuto (ang unang tseke mga ilang segundo pagkatapos ng pasimula, agad - agad kung ang huling pagkakataon na ang programa ay nagsara para sa isang pagkakamali) ang programa ay nagbabasang mag - isa, nang hindi humihingi ng anuman, ang huling Paglaya. Kung ang bersyon ay mas bagong download ang nabibitbit na pakete na angkop para sa sistema at pinalitan ang mga file ng programa sa lugar: ang mga ginagamit ay muling pinangalanan *.old-update at inalis sa susunod na restart. Mga setting, arkibo, Data folder at C ALL3. Ang TXT ay hindi nahahawakan. Ang bagong bersyon ay aktibo mula sa susunod na simula: sa pagluklok ay natapos ang isang bintana na nagmumungkahing mag-restart kaagad; para sa mga apurahang pagtutuwid, minarkahan [obligatorya] sa Release, ang reboot ay hindi maaaring ipagpaliban. Ang control status at ang bersyong ginagamit ay nakasulat sa ilalim ng status bar; upang patayin ang mga updates=0 sa WOMETROPOVHF.ini. Sa Mac ang app ay nilagdaan at hindi binabago ang sarili nito: ang programa ay nag-uulat lamang na may bagong bersyon.

Ang imahe ng NOBYTROPOVHF na may version number ay lumilitaw sa simula; sa huli ang programa ay humihingi ng kumpil at salamat sa paggamit.

## 12 ter. Error sa pagbasa ng talaksang '%s': %s

Ang anumang hindi inaasahang pagkakamali ay nagtatapos sa Data/errori.log, kahit na sa pagkalkula ng log off: date at time, bersyon, sistema, open tab, kumpletong error at mga huling linya ng ginagawa ng programa. Ang isang interface error ay hindi na nagsasara ng programa: sa status bar ay lumilitaw ang "anno anno annotated error" (click: buksan ang register). Sa susunod na panimula ay itinatanong ng programa kung ipapadala ang ulat: buksan ang browser sa isang GitHub signal na natipon na, na may nominasyon, locator at username ng PC tinanggal, na inyong tinitingnan at ipinadadala na may libreng GitHub account. Nagtatapos din ang log sa bawat update check.

Sa status bar ang mga source balls (DX cluster, RBN, PSKReporter, MQT, UDP) ay pinipindot at dinadala sa source; ang komiks ay nagsasabi kung bakit ang isang pinagmulan ay naka-alis: tinanggihang port, cluster node nakahimpil sa server, walang tugon, o UDP nakikinig ngunit walang mga pakete mula sa WSJT-X, JTDX o MSHV. Ang isang pindot sa numero ng bersyon ay agad na sumusuri sa mga update.

## 13. Mga Plataporma

External bookcases: Avalonia para sa [12] interface, MQTTnet bilang MQT client para sa reality-time street ng PSKReporter [13], Microsoft. Data.Sqlite para sa arkibo ng [14].

C# on.NET 10 (LTS) [15] kasama ang Avalonia interface, na inilathala bilang isang independiyenteng productable para sa Windows 64 at 32 bits, Mac Intel at Apple Silicon, Linux Debian at Raspberry Pi (64 at 32 bit). Noted Limits: Windows 10/11; macOS 14 o mamaya, na may apps na lalagda o magbukas ng kanan → Open; Raspberry mula sa Pi 2 / Zero 2 W up (Ang mga modelong ARMv6 ay hindi suportado ng.NET).

## Salamat
Dahil sa I5MZY Massimo Zannoni sa pagsubok sa Windows 7 at Windows 8.

Airtights: ITU-R P.453, mga digital na produkto ng Study Group 3 (© ITU), na ginagamit para sa mga layuning personal at di-commercial.

## Pangunahing mga pinagmumulan

1. S. Heck LA0BY, “Extreme Tropo Propagation on 144 MHz at up”, presentasyon sa 41st Nordic VUSHF Pulong, Skjeberg 2019. Troposferical Ways, Event Control List (Hepburn/ F5LEN premise, mataas na profile ng temperatura na may Windy, DX cluster bilang panimulang pagsubok), pagmamasid na ang tanging paghula ng mga mapa ay hindi sapat para sa mga sukdulang ruta.
2. J. N. Gannaway G3YGF, “Tropospheric dinadaglat na Propagationić, QST Nobyembre 1983 (mula sa eponymous na artikulo tungkol sa Radyo Komunikasyon, RSGB, Agosto 1981). Mga tanawin ng malayang pagkawala ng kalawakan at difry, katumbas ng radius 8497 km, lugar na termino ng 10 dB sa bawat abot - tanaw, terminong klima 0.2 · (N − 310), kapasidad ng halaman at signal/noise ratio, halimbawa sa 144 MHz.
3. W. R. Hepburn, DX Info Centre, “Tropospheric Ducting EXOVić (dxinfocentre.com/tropo.html) at “ Ang Hepburn Tropo Index (HI) (. Indise ng sukat at halaga ng kahulugan, hula sa 6 na araw.
4. F5LEN, propesiya para sa Europa (tropo.f5len.org/forecasts-for-European: listahan ng mga imahe sa pahina script, class legend... Duct sa imahen) at pandaigdigang paghula (tropo.f5len.org/WW) at pansinin ang “Bagong bersyon ng pandaigdigang VHF tropospheric propation propesyunal na kasangkapang pang-ekonomiya. Indise batay sa yunit N, 40 hakbang mula sa 3 oras, update 4 na beses maghapon.
5. Mqtt.pskreporter.info, distribusyong MQTT sa tunay na panahon ng mga komersyal ng pskreporter.info (binibigkas ng M0LTE, daloy ng P. Gladstone na ginagamit nang may pahintulot). Doors 1883–1886, pskr/fitter/v2 top scheme.
6. Project buod ng 8/11/2026 at Go test (core.go, gui.go) na inilaan ng may akda ng programa. Ad Rules, negatibong pansala, mga bahagi ng bandwidth, telenet cluster management, tema, wika at portable.
7. C ALL3.TXT, nominasyon database → JTDX kabesera (I. UA3DJY, A. ES1JA script, JTAlert/hamspots.net data). Ang talaksang format at ang bilang ng mga pangalan ay napatunayan sa talaksan ng proyekto.
8. WSJT-X, source code, NetworkMessage.hpp file (paglilinaw ng protocol ng UDP: Heartbeat, Status, Decode). Pangkalahatang reperensiya, hindi isinanib; ginagamit ng JTDX at MSHV ang parehong format.
9. Reverse Beacon Network (reversebeacon.net), mga pahina ng serbisyo ng telenet node. Ports 7000 (CW, RTTY) at 7001 ( FT8), login na may pangalan lamang.
10. D. Mills, RFC 4330 “Simple Network Time Protocol (SNTP) Version 4”, IETF 2006. Ang format ng pakete, pagkalkula ng basura at pag - antala ng pagbabalik. Pangkalahatang reperensiya, hindi isinanib.
11. NTP Pool Project (ntppool.org); INRIM, NTP time service (ntp1/ntp2.inrim.it); PTB (ptbtime1.ptb.de). Ginagamit ang mga server para sa pagkontrol ng oras. Pangkalahatang reperensiya, hindi isinanib.
12. Avalonia UI, opisyal na dokumentasyon (docs.avaloniaui.net). Multipleatform interface, mga tema at plano mula kanan pakaliwa. Pangkalahatang reperensiya, hindi isinanib.
13. MQTTnet, aklatan ng.NET para sa MQTT (github.com/dotnet/MQTTnet). Client na ginagamit para sa daloy ng PSKReporter. Pangkalahatang reperensiya, hindi isinanib.
14. SQLite (sqlite.org) at Microsoft. Data.Sqlite (pag-aaral.microsoft.com). Archive sa isang file. Pangkalahatang reperensiya, hindi isinanib.
15. Microsoft,.NET 10 – suportado ang mga operating system at autonomous na publikasyon sa isang file (pag-aaral.microsoft.com/dotnet). Ang mga platform at limitasyon na ipinapakita sa seksiyon 13. Pangkalahatang reperensiya, hindi isinanib.
16. ECMA-376 “Office Open XML File Formats”, bahaging 1, seksiyon 22.1 (Office Math Markup Language). Structure ng talaksang.docx at mga ekwasyon. Pangkalahatang reperensiya, hindi isinanib.
17. Talaan ng 28 wika ng Wiki ng YA-MS, na inilaan ng may akda ng programa.
18. Open-Meteo, weather propetation API (bukas-meteo.com), na may datos mula sa modelong NOAA GFS. Panggigipit sa antas ng dagat para sa isobare (tingnan ang dokumento 2).
19. J. P. Snyder, “Map Projections – A Working Manualific, USGS Professional Paper 1395, 1987, at pamantayang polynomial transforming methods para sa georeferencing image. Pangkalahatang reperensiya, hindi isinanib.
20. The Astronomical Almanac, bahaging D, Ang “Low na prepekturang pormula para sa Moonisensiya (precision position ng mga 0.3°). Ginagamit upang kilalanin ang mga koneksiyong EME. Pangkalahatang reperensiya, hindi isinanib.
