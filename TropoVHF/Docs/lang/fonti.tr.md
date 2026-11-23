# Kaynaklar ve formüller

> İtalyancadan makine çevirisi: şüphe durumunda İtalyanca kılavuz geçerlidir.

LIVETROPOVHF tropo tahminlerine danışan kaynakların çalışma koleksiyonu (20-21 Kasım 2026): Temel fizik, mevcut veriler, indeksler, formüller, radyo amatör deneyimler, geçerlilik ve kararlar. Formüller LaTeX'te, (1), (2),... ve metinde para birimleriyle hatırlıyorlar. Kılavuz, programın bunları nasıl kullandığını açıklar.


## 1. Yazarın binası

Çalışmayı çerçeveleyen IC8TEM gözlemleri, kaynaklarda bulunan geri bildirimlerle.

- İtalyan coğrafyası difraksiyon çağrısı yapıyor. Köprüler ile, promontory ve adalar istasyonlardan birkaç düzine kilometre uzakta, difraksiyon katkıları ana kenardaki Fresnel parametresi ile hesaplanmalıdır, sadece düzgün topraklarla değil. Toplantı: 563 MHz'de düzensiz arazi üzerinde karşılaştırma (Rio de Janeiro, 5 ve 30 km arasında 27 puan), en iyi yöntemler P.526 bıçak kenarlarıyla, P.1812 ve diffractive wedge, ortalama hata ile -5 ve +3 dB arasında ve iki yönlü hesaplamaya hizmet ettiğini hatırlıyor; P.1546 en karamsar (P.an Ramirez ve PETOOL istasyonunun yakınında dik yamaçlar için.
- Uzak bir konduit içine girmek için, Fresnel'e ihtiyacınız var. Konduit ufkun ötesine geçerse, sinyal bize başlangıçlarına kadar engeli kırarak gelir; 1.3.2 program bunu hesaplar ( tahminlerin Manualı, § 10 ter). Toplantı: Rohde & Schwarz (2023), yüksek tavan boyunca istasyonların giremediği notlar, bu son evet'e yakınken.
- Koşullar hızla değişir. Teoriler oradadır, ancak koşullar birkaç dakika içinde değişir ve tahmin sistemi gerçek zamanlı olarak verileri alır: GFS her 6 saatte gelir ve zaman aralığı ve 0.25° ile bir tahmin kısa sürede değer kaybedebilir. Toplantı: Bray (QST, Kasım 1961) Yaklaşık ±7 dB'nin 100 mil boyunca yaklaşık ±7 dB dalgalanmalarının ölçümü; dükler atmosferik bir zamanla hareket eder (Rohde & Schwarz); QSO kayıt 29/12/2021-2/1/2022 saat içinde büyüyor ve düşüyor.
- En iyi now-casting, DX küme-PSKReporter ağı olmaya devam ediyor. Şimdi ne olduğunu bilmek için, küme noktaları, PSKReporter (ve WSPR) herhangi bir model yendi: önlemlerdir. Tahminler nereye bakacağını ve ne kadar sürebileceğini söylüyor; ticariler orada olup olmadığını söylüyor. Bu program aynı haritada nokta ve tahminler getiriyor.

## 2. Amaç

Fikir, LIVETROPOVHF tarafından hesaplanan üçüncü bir indeksdir, bir hava modelinin dikey profillerinden başlayarak, herburn ve F5LEN 'ya onları yerine eklenecektir. Bugün program haritalarını zaten hazır okuyor: bir indeks aynı zamanda *why* açılıyor (konuşun bir kısmı ve kalınlığı, minimum grup sıkışıyor) ve programın zaten topladığı noktalarla gerçekleşebilir.

- Ne verebilirsin: Bir noktada ve saatte konduit olasılığı, temel ve üst, kalınlık, minimum frekans kapanmış ve bir konduit içindeki rotanın kesilmesi için bir yol boyunca.
- Ne veremez: ince dükler (birkaç metre altında) küresel modeller 0.25°'de çözülemez ve denizdeki buharlaşma iyonları, her şeyden önce GHz'den hizmet eder.
- İlk pratik amaç: Avrupa'da 144 ve 432 MHz, DX küme noktalarında doğrulama ile, PSKReporter ve yerel dekodings.

## 3. Temel Fizik Temel Fizik

Her şey refraktif olmaktan başlar N her profil seviyesinde hesaplandı: Konduit, değiştirilmiş reaktivite M'nin tırmanmasını azalttığı yerdir, bu, dN/dh'nin -157 N/km ([ITU-R P.453-13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008 x1Q).](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)

Refraktivite (P ve hPa, T in kelvin):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Karşılaştırmalı nem H (%) ve sıcaklık t (°C), -40 ve +50 °C arasındaki su:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

Değiştirilmiş reaktivite (h km veya 0.157 · metrede z ile z) ve dük kriteri:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN/dh (N/km) | Yönetmelik | 144 MHz Etkisi |
| 0 0 0 0 | sub-refraction | azalan akış oranı oranı oranı oranı |
| 0 ila 79 | Normal (standart hakkında standart) | Radyo ufak 4/3 ile eşdeğer |
| -79-157 | super-refraction | elongated flow, "too good" |
| Aşağıda -15 | Konduit (trapping) | Kapalı sinyaller, DX 1000 km |

N'yi basınç seviyelerinde bir modelden nasıl çıkarın: Her seviye için T, göreceli nem ve geopotansiyel yükseklik alırsınız, N ve M'yi hesaplarsınız, sonra komşu seviyeler arasında gradient. Zeminin altındaki seviyeleri ortadan kaldırılmalıdır ( Alplerde 1000 hPa yeraltı).

Asgari kapalı frekans: Konduit sadece kalınlığına kıyasla oldukça kısa. Hepburn bir masa kalınlığını yayınlar → minimum kullanışlı frekans ([DX Info Centre](https://www.dxinfocentre.com/propagation/luf.htm)); bu, sinyalin kademeli olarak düştüğü bir yumuşak sınırdır.

| Led Kalınlık | Asgari ödeme frekansı |
|  61 m  | 962 MHz |
|  91 m  | 464 MHz |
|  122 m  | 276 MHz |
|  152 m  | 185 MHz |
|  183 m  | 133 MHz |
|  213 m  | 101 MHz |
|  305 m  | 53 MHz |

VHF aralığında (120-300 m) tablo değerleri sürekli olarak iyi bir yasa takip eder; 100 m altında formülün% 30-40'a kadar frekansı hafife alır. Kendi kendime (Heburn tarafından yayınlanan bir formül değil), milimetre ve d metrede f:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

Uygulamada: 2 m için bir konduit genellikle en az 170-180 m alır, çünkü 70 cm yaklaşık yarısıdır. net, Themax = 0.014 · d^1.5, Kerr (1951, Bölüm 1, eq. 18) ve. ile ayakta kalmak için geçerlidir, çünkü bu birimlerde yaklaşık 2 m 180 m ve 70 cm değerindedir, bu da herburn masasında aynı değerdir. Kerr, net bir kesme olmadığı konusunda uyarıyor: Birkaç kez daha bile konduit çok acı çekiyor ve küçük bir "büyük" konduittan istasyonlarla bile resepsiyonu iyileştirebiliyor. A 2019 IEEE Access study, klasik yöntemlerin (Kerr, Kukushkin) gerçek kesmeyi tarif etmediğini doğrulamaktadır ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)): Eşin bir duvar olarak kullanılmaması gerekir.

## 4. Ücretsiz için mevcut olan veriler ücretsiz

Zayıf nokta dikey çözünürlüktir: 1000 ve 850 hPa modelleri her 25-50 hPa'ya bir seviye veriyor, bu yüzden inverssları 500-2000 m'e kadar iyi görüyorlar, ancak zeminin yakınında ince kanallar değil. LIVETROPOVHF zaten Open-Meteo'yu isobare, sıcaklık 850 hPa ve inversiyona kullanıyor: indeks aynı çağrılardan başlayabilir.

| Kaynak Kaynağı | Ne verir | Karar | Index için kullanın |
| [Açık-Meteo, GFS](https://open-meteo.com/en/docs/gfs-api) | T, göreceli nem, dew noktası, geopotential yükseklik seviyesi (1000, 975, 950, 925, 900, 850 hPa...); limit katmanının yüksekliği | Basınç seviyelerindeki 0.25°; 16 gün, 120'ye kadar saat, her 6 h güncellendi | Profil N ve M, gradient, taban ve en üst giriş; zaten programda |
| [Açık-Meteo, ECMWF](https://open-meteo.com/en/docs/ecmwf-api) | Açık-data modellerinin aynı seviyeleri 0.25°; zemin dew point verir |  0,25°  | İkinci görüş, modeller arasındaki koncordance için faydalı |
| [Açık-Meteo, DWD ICON](https://open-meteo.com/en/docs/dwd-api) | Basınç seviyelerinde T ve göreceli nem; ICON- D2 'da 800, 900 ve 925 hPa var | ICON-EU yaklaşık 7 km | Avrupa hakkında üçüncü görüş |
| [Radiosunds, Univ. Wyoming](https://github.com/NCAR/RAOBget) | Ölçülen profiller, günde iki fırlatma (00 ve 12 UTC), istasyon başına metin formatı | Birçok önemli seviye, her birkaç düzine metre bile | indeks doğrulama; Modelin göremediğine dair ince alıntıları görün |
| [GROANS, NOAA NCEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive) | Yayının tarihsel arşivleri, istasyon başına bir dosya, daha fazla türetilmiş parametre | Yukarıda olduğu gibi | Yıllar boyunca veri ve karşılaştırma geçmişteki noktalarla karşılaştırın |
| ERA5 (Copernicus) | 1940'tan beri oral reaniz, 37 baskı seviyesi | yaklaşık 28 km | İstatistikler ve kalibrasyon, tahmin değil |

- Open-Meteo geopotential yükseklik deniz seviyesinin üstündedir: zeminin altında düşen seviyeler atılmalıdır ([Open-Meteo, üst hava](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)).
- Zeminde 2 m'de T ve nem eklenmektedir: ilk derece gradient'e verirler, gece yüzeyi düklerinin düz ve deniz üzerinde doğduğu yer.
- İtalyan Wyoming arşiv Üniversitesi'nde yayınlar (kart.uwyo.edu/upperair/sounding.html): Aşağıdaki masada istasyonlar ve sayılar WMO.

| İstasyon İstasyonu | W | hayali | Uzunlık | Share Share Share |
| Udine/Campoformido |  16044  |  46,03  |  13,18  |  94 m  |
| Milano/Linate |  16080  |  45,43  |  9,28  |  103 m  |
| Deniz Deniz Uygulamaları |  16245  |  41,65  |  12,43  |  12 m  |
| Brindisi |  16320  |  40,65  |  17,95  |  10 m  |
| Tuzani/Birgi |  16429  |  37,92  |  12,50  | yaklaşık 14 m |

Wyoming'in metin formatı her seviye basıncı, yükseklik, sıcaklık, dew noktası ve nem için verir: N ve M. Wyoming resmi bir arayüze sahip değildir, senaryolar [RAOBget of NCAR](https://github.com/NCAR/RAOBget); The College of DuPage kopyaları istasyonda tutar ([e.g., 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)).](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. Indexes and published methods

Hepburn ve F5LEN aynı fiziksel malzemelerden başlar, ancak tam algoritmayı yayınlamaz; 2008-2026'nın bilimsel çalışmaları adım adım atarak, ve bu, tarif etmeye değer.

| Yöntem Yöntemi | Verilere başlayın | Hesaplanan nedir | Bizim için kullanışlı notlar |
| [Hepburn Tropo Index](https://www.dxinfocentre.com/propagation/hti.htm) | Kanada Meteorolojik Servisinin ham hava modelleri (MSC, haritaları altında yazılır; GFS/NAM), GrADS ile tasarlanmış haritalar, özel bir algoritma ([storia](https://www.dxinfocentre.com/propagation/history.htm)) | 0-10 sinyal eğriliğinin doğrusal ölçeği: 0 normal, 1-2 süper-refksiyon, 3-4 bazı alıntılar, 5-6 diffüz dükler; negatif değerler = alt-refksiyonlar; | taklit edilecek ölçek, bu yüzden üçüncü indeks hemen ilk iki ile karşılaştırır |
| [F5LEN, dünya tahmin](https://tropo.f5len.org/WW/info.php) | Avrupa için ARPEGE, Météo-Fransa modeli, 7.5 km, 2 m ve 1500 m arasında daha fazla seviye (Yazan](https://tropo.f5len.org/author/f5len/)) | Ev sahibi için ortalama indeks, günde 3 saat, günde 4 hesaplama, 28 günlük arşiv ([yeni sürüm](https://tropo.f5len.org/?p=504)) | Birçok düşük seviyede bir modele hizmet ettiğini doğrulayın |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf) | 60 ECMWF model seviyeleri, zeminden 3000 m | sütun başına minimum dN /dz: -157 N /km altında, -78.7 N /km altında süper-refksiyon; tuzak katmanının üssü | iklimoloji: Padana Oturan Avrupa Bölgesi tüm yıl boyunca daha fazla boru hattı ile; yaz aylarında Akdeniz% 100'e yakın,% 15'te; İtalya yaz geceleri% 30'da |
| [Indermuehle ve Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861) | GFS 0.25°, 21 basınç seviyesi + 2 m | M = N + 0.157 z; minimum dM /dz aşağıda 3 km, M açığının en üst, 40 m altında parladı | 88 MHz'den 2,68 GHz'ye 25 kanalda 7 yıl boyunca doğrulandı; önceden yaklaşık 3 güne kadar iyi kapasite (AUC yaklaşık 0,74), sonra düşer; GFS ERA5 'dan daha iyi; |
| [Hämäläinen et al., IMF 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf) | Harmonie-AROME, 750 m veya 2.5 km, 65 veya 90 seviye | dM/dh < 0 model profilinde, 4 ila 59 m kirişle karşılaştırma | yatay karardan daha fazla dikey sayın; hiçbir model 15 m altında dükleri görmüyor; Sıcaklık gradientine hükmediyorum |

Üçüncü amatör radyo servisi zaten aktif: [TROPOCAST.EU](https://tropocast.eu/)of Artur SQ3A, Avrupa'da 2 m ve 70 cm için. Tahmin sayfasından ([tropocast.eu/forecast](https://tropocast.eu/forecast)):

- Data: DWD ICON-EU (pass 0.0625°, yaklaşık 7 km), varsayılan kaynak; alternatif olarak Kanada GSYİHS (0.15°), daha az ayrıntılı.
- Yöntem: N ve M, ITU-R P.453'e göre, zemin ve 850 hPa yakınlarındaki katmanlar arasındaki profilden; katmanların tanınması, zemine ve yüksekliğe ve renklerin eşiği, yayınlanmamış bir kalibrasyondur.
- Ürünler: "Tamam" manzara 0-3000 m, daha fazla ayrı görüş " zemin" ve "yüksek" (şimdi sadece haritalarda dahil olmak üzere); puan 0-100 dokuz sınıf (38 alt, 38-52 marjinal, 53-58 zayıf, 59-65 orta, 66-72 iyi, 73-80 çok iyi, 81-88 yüksek, 89-95 güçlü; 96-100 $) + bir saat içinde, animasyon ve arşiv ile.
- Alanlar: Avrupa, Polonya, Birleşik Krallık, Almanya, Fransa, İspanya, İtalya, Çek Cumhuriyeti, Hırvatistan; arayüz de İtalyan.
- Reuse: API anahtarsız iframe ile birlikte haritalar da belgelenmiş bir API (OpenAPI ve JSON şeması). Lisans belirtilmemektedir: Onu herburn ve F5LEN 'ye bir sonraki üçüncü kaynak olarak kullanmadan önce yazara sormanız gerekir.

Projemizden gelen farklar: TROPOCAST 850 hPa'da durur (yaklaşık 1.5 km), yüksek tavanlar 2000 m ve ötesinde ( LA0BY 22.11.8) var ve grup veya rota için bir indeks vermiyor. Sınıflar 0-100 aynı günlerde indeks 0-10 ile karşılaştırılabilir.

TROPOCAST görüntü yapısı (programın yazarı tarafından sağlanan bağlantıdan): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, n'un tahmin aşaması nerede. Site robotlara otomatik geri çekilmeyi yasaklar, bu yüzden görüntüyü okumadım; Ayrıca, LIVETROPOVHF'deki bu haritaları göstermeden önce yazarın iznine hizmet edebilirim.

Defter: [DJ5AR, kategori](https://dj5ar.de/?cat=174)(Magonza, 21 makale 2017-2023, 2 m-13 cm). Vakalar tarif edildi: İrlanda'dan birkaç hafta (23 Mayıs-2 Haziran 2023), FT8 to Belarus (Eylül 2020), İtalya-Almanya Alpler ve Adriyatik ( Ağustos 2017). Yerden yararlanamayacağınız “çok yüksek” kanalının durumunu kabul edin: istasyonlarla birlikte konduit payını karşılaştırmak için gerekli olan başka bir onay. Radyofazları bir aperture ölçüm olarak kullanır (1028 km'ye kadar) ve Paris, Bordeaux ve DWD'deki dikey profiller.

Özetle, ortak tarif: dikey profil → N ve M per level → dM /dh negatif → temel, üst, kalınlık ve dük → indeks. Hepburn'un "gizli" kısmı gerçek olaylarda ampirasyondur, ki biz noktalarla yeniden canlandırabiliriz.

## 6. Program için kullanışlı Formulaler

N ve M (basic Physics bölümü) ek olarak, bunlar dikey numaradan ve sonra yol için hareket etmek için kullanılır. ITU formülleri bir PDF'de kötü bir şekilde yeniden inşa edildi; 22 Kasım'da resmi metinler üzerinde incelendi ([ITU-R P.452-18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453-14, P.526-16, P.372-18) ve kodda düzeltildi: tromboz 190'da sabit değil (pişmanlık) 2,3'ün altındaki gazların absorpsiyonu; α'nın altında 3,4 - (eq. 55a); Rumore P.372-18: tablo 1 ve galaktik karşılık; Yapay gürültü eğrileri 250 MHz'e kadar ve program üzerinden yaklaşık 100 MHz'e kadar, program aynı yasaları genişletmektedir.

1. Modelin iki seviyesi arasında Gradient (h km, M biriminde):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. Conduit gücü ve kalınlığı: kalınlık d = üst - temel (bir toprak girişi için, temel = toprak); temel ve üst arasındaki M eksikliği olarak güç.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3. Dünya yarık eşdeğer faktörü (P.452-18, eq. 5 ve 6a), 345N = ilk kilometrede ortalama N düşüş, N/km:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

{{N = 40 ile yaklaşık 4/3 k (standart atmosfer); {{N ile bu yaklaşımları 157 k, bu, Dünya radyosu düz hale gelir: düklerin başlangıcıdır.

4. Radyo yüksek bir anten h metre, yarı yarıya eşdeğer ile:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5. Latitude (P.452-18, eq. 2), zamanın yüzdesi; {{ = rotanın merkezi, μ1 ve μ4, deniz tarafından ne kadar uzakta olduğunu dikkate alır:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

41° N, deniz tarafından (μ1 = μ4 = 1), zamanın% 11'ini verir: beklenen indeksin farklı olduğu referans değeri olarak faydalıdır.

6. Troposcatter için kaybı (P.452-18, eq. 45; mm'de f, D km, mrad'daki yolun açısı, rotanın merkezinde N0 zemin reaktivite:

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

·0,15 · N0, ıslak havanın neden girilmeden yardımcı olduğunu gösteriyor: 50 ekstra N birim kaybından 7.5 dB daha az. LIVETROPOVHF zaten saç kaybının G3YGF versiyonunu kullanıyor.

7. Girişte kaybı (P.452-18, eq. 46-51): akrep miliradiant için sabit bir düşüş, ki bu da frekans kökü ile büyür (f in GHz, a\_e).

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. Bean ve Dutton (1966) ve Kerr (1951)

İki klasik metin, indekse hemen ihtiyaç duyan üç şeyi verir: gradient eşleri zaten GFS'nin aynı seviyede avlanır, dB'de yenidenaktivite değişikliğine değer nedir ve hava durumlarını yok eder veya yok eder. * Radyo Meteoroloji *, NBS Monograf 92.

1. Basınç tabakaları için profil sınıflandırması (Bean ve Dutton, sekme. 6.5, p. 248; -N/dh in N/km). Radyo dalgaları için tasarlanmış, model seviyelerinde olduğu gibi geçerlidir:

| Road Road Road | Shares about Shares about | Sub-refraction | laminat değil | Super-refraction |
| 1000-850 hPa | 0–1,46 km | -dN /dh < 0 | 20 ila 60 | 100'den fazla |
| 850-700 hPa | 1,46-3,01 km | -dN /dh < 0 | 20 ila 50 | 80 over 80 |
| 700-600 hPa | 3,01-4,20 km | -dN /dh < 0 | 15 ila 40 | 70 |
| 600-500 | 4,20-5.57 km | -dN /dh < 0 | 10 ila 30 | 50'den fazla |

2. dB'de ne kadar değer (Bean ve Dutton, cap. 6, pp. 233-258), ufuk ötesinde 70 ila 180 MHz'den alınan önlemler:

- Herhangi bir tabaka (küre ya da yüksek irtifada) "smooth" profiline kıyasla, 10-25 dB'nin tüm yüzdelere sinyalini yükseltir; büyük kazanç yüksek eğimli tabakalardan gelir, muhtemelen odaklanma etkisi nedeniyle (200 km).
- Zemin reaktivite Ns ve ilk km {{N'deki düşüş, 0,95, median 0,7 (20 rotadan 446 km'ye kadar, yaklaşık 100 MHz'ye kadar.
- Yıllık döngü: N birimi başına 0.18 dB; N. başına 0.2 ila 1.1 dB, 175-200 km ve kış rotalarında maksimum.
- Misme'nin bileşik parametresi, alan varyasyonunu 0,5 dB/(N/km) olarak tahmin eder. · (uygun gradient - 40) artı bir termal istikrar terimi; gerçek profil ve adiabatik arasında parametresi, 0-3 km) sadece basit 345. Yazarların Sonuç: {{N basitliği için en iyi parametre olmaya devam ediyor.

3. Referans profili ve anomali (Bean ve Dutton, § 3.8 ve 3.9). Üstel referans atmosferi CRPL, Ns'ten başlayan "normal" profili verir; Bir birim, gerçek profilin her payda ne kadar farklılık gösterir. Bir GFS profilini haritaya koymak için dönüştürmenin doğal yoludur:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

Ns = 313 ile yaklaşık {{N var -42 N/km, bu eşdeğer yarıçap 4/3. -157 N/km konduit kriteri, burada, Snell yasası tarafından yatay bir yarı için elde edilir (eq. 4.23), anten yüksekliğinden neredeyse bağımsızdır.

4. Nerede ve konduits doğduğunda (Bean ve Dutton § 4.4; Kerr cap. 3):

| Süreç Süreci | Durum durumu | Etkisi Etkisi Etkisi |
| Uyarı Uyarı Uyarı Uyarı | Soğuk deniz kenarından sıcak ve kuru hava (günlük iyi havadan sonra yaz aylarından atılır); temel olarak nem açığını tutar. | Denizde yoğun konduit |
| Nocturnal irradiation | serene gökyüzü, zayıf rüzgar; düşük bulutlar veya çok fazla nem onu önlemek | Gün batıkında doğan zemine yol açtı, akşam büyür ve özellikle de güneş battıktan sonra hızla kaybolur. |
| Subsidence | Anticyclone, ısıtmalı ve kuru, taze ve nemli havadan inen hava; dağlarda sık sık sık selsiyon ve güney-doğu anticyclones sektörleri | süper-refraktif tabaka veya yüksek dük; neredeyse her zaman tersine, standarttan daha yüksek |
| Deniz ve toprak | Sahil, birkaç yüz metre dolaşım | sahil boyunca yerel boru hatları |
| Konduit'e karşı | Mix: Orta sabah ve öğleden sonra güneş tarafından ısıtmak, birkaç m / kaba arazi üzerinde yere rüzgar; Gece fog | Konduit olasılığı neredeyse hiçbir şey |

5. denemek için iki yeni malzeme. dikey hareket: yakınlık bölgelerindeki akımlar ortadan kalktı ve tabakaları sildi, sübvansiyon sinyali güçlendiriyor (Moler ve Holden: ortalama 2,5 dB daha fazla, bant L ve X'de deniz üzerinde. Open-Meteo zaten baskı seviyelerinde dikey hız sağlar. Ve baskı trendi: 186 MHz'de 140 km derste, baskı vericiye düştü ve yolun merkezine geri döndü ve sadece 2 (Bean ve Dutton) ile birlikte, 6.9.

6. Kerr'dan. En yüksek dalga (base Fizik bölümü) formülüne ek olarak, Kerr yüksek sıcaklıktan M için yaklaşık bir formül verir (§ 3.9, eski 79 ve 4800 sabitleriyle, sonra 77,6 ve 4810) ile değiştirilir ve rüzgar kestiği dalgalara göre, derin kaçışların olası nedeni (§ 3.26). İtalyan kaynağı, Bean ve Dutton tarafından bildirilen ve aramak için: L. Bonavoglia (Aralık 1958), "Finansal fenomenler ve propagasyonlar"

## 8. amatör radyo sunumlarından ve teknik raporlardan (ikinci seri)

Programın yazarı tarafından sağlanan yedi belge. Üçü doğrudan indekse hizmet eder: W3SZ for the troposcatter "floor", LA0BY for case that Hepburn'un tahmin etmediği durumlar için, Hepburn (translation LU7MC) düklerin hava sınıflandırması için.

1. Troposcatter: her zaman var olan sinyal. R. Rehr W3SZ, "Tropospheric Scatter (Troposcatter) VHF,195, Mikrodalga ve Frequencies", Pack Rats Konferansı 2014; J. R. Duffey KK6MC, "The Troposor

- Yaklaşık 100 ila 700 km, 144 MHz'den 10 GHz'ye kadar kullanışlı; kaybı frekansı olan 30 günlük f büyür.
- Her şeyden önce, ufuka yönelik başlangıç açısına dikkat eder: 9-12 dB ( M1CNK, Collins yöntemi).
- Yaz aylarında sinyal kıştan 10 dB daha yüksektir; sabah erkenden 5 dB daha düşük veya akşam. KK6MC, 13-15 dB'nin zaman, sezon ve zaman ile toplam değişkenliğini gösterir.
- Yeh yöntemi, program için en kolay (f in MHz, d mil, derecelerde saç açısı, iki paketin açılışlarının α geometrik ortalaması):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Larsen'in Karşılaştırması (1968) 15 gerçek bağlantı: ortalama kare hatası 5,1 dB for Collins, 7,4 for Yeh, 8.1 for NBS and CCIR, 16,8 for Rider. Tüm yöntemler, Ns sabit tutarsa (Yeh 5.4 dB'ye düşer): günün reraktifliği biraz sayılır, bu yüzden tropo indeksi sadece Ns değil, katmanlara bakmak zorundadır. 15 bağlantı masası, ölçülen kayıplarla ( Feldberg-Monte Baldo, 957 MHz, 223,1 dB; Kıbrıs-İsrail, 77,5 MHz, 163,3 dB dahil), bizim hesaplamamız için iyi bir test tezgahtır.
- KK6MC, SSB'deki tipik bir istasyonun 2 m'yi aşmasını ve buna karşılık gelen mesafeyi, 184 dB'den (FT-817 ve 3 elementten yaklaşık 130 km) yaklaşık 215 dB'ye (günümüzdeki otobüs yaklaşık 430 km) ve 236 dB (kW ve yagi çifti, yaklaşık 640 km). indeks dB'leri mil kazandıklarına çevirmenin yolu.
- Hazır kod: W3SZ Yeh ve Collins programı (nihawk.com/w3sz/YehCollins.zip) ve Bob Atkins KA1GT hesaplayıcı (bobatkins.com/radio/troposcatter.html).

Diğer bir kaynak: I. Roberts ZS6BTE, "Tropospheric Scatter Propagation", * Radyo ZS*, Aralık 1984, 2013 notuyla ([qsl.net/zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). Mainly Faydalı geometri: Saç uzunluğu = h0 - θ1 -,2,,0 = d/R, θ1 = (h1 - h\_ostacolo)/d1 + d1/2 R ve diğer istasyon için de aynı, R = 8446 km (Dünya Yarısı) Troposcatter'daki zemin profilinden gelen açıları hesaplamak için kullanılan parçadır. Diğer veriler: 10 dB hakkında daha fazla maliyet; Ns 280'den 300'e kadar geçen her açı, yaklaşık 4 dB'nin kaybını azaltır; Saç hacmi yaklaşık 150 km, yaklaşık 600 ile 3000 m arasında, 300 m arasında değil; Saç hacminde saat 20 dB'yi kullanarak kontrol edilmelidir. W3SZ

2. Konduit türü (W. R. Hepburn, "Tropospheric Propagation Modes", dxcentre.com/propagation/tr-modes.htm, LU7MC İspanyolca çevirisi). İtalya'daki faydalı vakalarla Bean ve Dutton masasını tamamlayın:

| Tipi Tipi Tipi Tipi | When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When | Index için Not for index |
| Irradiation | Yeryüzünde sakin ve huzurlu geceler | Yeri takip edin, şafaktan kısa bir süre sonra kaybolur |
| Yüksek basınç (subsidence) | anticyclone | Her gün sürebilir; gece, aşağıdaki irradiasyondan gelen konduit uzak sinyalleri engelleyebilir, bu yüzden gün daha iyi gidebilir |
| Front Front Front Front Front Front | Sıcak bir cephe önünde, soğuk bir cephenin arkasında, bir istasyonun kuzeyi | Kısa; kötü hava onu rahatsız eder |
| Dağ | föhn, boa: hava aşağı geliyor, ısınma ve kurutma | Alplerin güneyindeki Adriyatik ve föhn güneyinde boa denemek |
| Uyarı Uyarı Uyarı Uyarı | Soğuk ve ıslak toprak üzerinde sıcak ve kuru hava, örneğin yağmurdan sonra | Yere yol açtı |
| Valley | Sıcak hava soğuk vadide durur | Gün içinde kalabilir |
| Marino Marino | Soğuk su üzerinde sıcak ve kuru hava; Gökle sahile geliyor | Öğleden sonra zirve; ilkbahar ve erken yaz en iyi mevsimler |

Denizdeki buharlaşma (0-30 m) sadece 3 GHz'ye hizmet eder ve ipucuna girmezler: 144 ve 432 MHz için göz ardı edebilirsiniz. Yüksek dağlar bir bariyer; Çöller çok kuru.

3. Herburn vakaları tahmin edilmedi (S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", Kuzey VUSHF 2019, JO59IX 'dan 500 m'e kadar:

- 22 Kasım 2018: UA6 'ye 70 cm (yaklaşık 2600 km) 6–8 saat boyunca. Hepburn sadece Baltık Devletleri ve Polonya'yı belirtti. Konduit 900 ve 1500 m'deki sıcaklık haritalarında görüldü ve sadece 2000 m üzerinde sona erdi; Norveç tarafında daha yüksekti ve tepedeki sadece LA0BY girdi. Zemin zamanında bir trope gibi görünmedi: soğuk ve üstte fog.
- 11 Şubat 2015: 2 m ve 70 cm 9A ve S5 (yaklaşık 1500 km) Alpler aracılığıyla, aynı zamanda Hepburn tarafından öngörülemeyen bu.
- 15 Ekim 2011: EA1 (2135 km) ile 2 m üst katta, üç deniz ve üç zeminde.
- Diğer olaylar: 6–11 Kasım 2003 doğuya (2050 km), 10 Aralık 2004 Fransa'ya.
- Alan adresleri: düz, ücretsiz zirveler, küçük rüzgar, ıslak zemin. Normal sıcaklık gradient -6,5 °C/km; gerçek bir durumda ( VK3KAQ) +7 °C 20 m.

Yazarın sonucu: Hepburn ve F5LEN yeterli değil, rota boyunca profillere hizmet ediyorlar. Bizim için: Bu tarihler modellerin tarihsel verileri ile yeniden hesaplanması için doğrulama vakaları ve yoldaki indeks istasyonların kontenjanlarıyla karşılaştırılmalıdır.

4. İtalyan deneyimi (G. IV3GBO, "Types of VHF propagation SSB/CW", 2017): 450 ve 3000 km arasında, 450 ve 3000 m arasındaki katmanlar ile; bir ipucu olarak uzatıldı; Puglia, Arnavutluk ve Yunanistan'a doğru deniz kenarında. 100 ila 500 km arasında Scatter.

5. Girişler (P. Wilton M1CNK, "An Introduction to VHF/UHF Propagation", 2013): 70 cm genellikle 2 m'den daha iyidir; başlangıç açısında 10 m kutup yaklaşık 3°'dir, yaklaşık 20 m.

6. NASA raporu (E. Hirschmann, "Tropospheric ve Ionospheric Effects on Radio Frekans (VHF-SHF) İletişim", Goddard X-731-67-89, 1967): Uydulara mesafe hataları, değil. Aynı N formülü ve üstel profili geri getiriyor (örneğin Florida: Ns 370, {{N -55, sürekli 0.161/km) ve iki NBS kaynağını aramak için gönderir: Bean ve Thayer, "CRPL Exponative Referans Atmosphere", Monograf 4 (1959), ve Bean, Horn ve Ozanich, "Climatic Charts and Data of Radio Refractive Index for the United States and World", Monograf 22 (1960).

## 9. VHF/UHF DX Kitabı, Bölüm 2

I. White G3SEK (edited by), * VHF/UHF DX Book*, DIR Publishing 1995 (ISBN 0 9520468 0 6; dijital kopya 2017), bölüm 2 "VHF/UHF Propagation", pp. 2-13, 2-22-2-29 ve 4-11. Bu toplananlar arasında radyo amatör deneyimine en yakın metindir; zaten kullanılan eşleri onaylar ve yeni sin optik kuralları ekler.

Sayı Numarası Sayı Numarası

- N'nin aynı formülü (77.6 P/T + 373·10^5 e/T2); örneğin: 1000 hPa, 7 °C ve = 10 hPa hasar N = 324. İyi şekilli bir atmosferde, örneğin fırtınadan sonra, N ilk 2-3 km'de yaklaşık 39 birim /km doğrusaldır.
- 0 ve -79 N /km arasında Gradients normal, aşağıda -79 süper-refksiyon, 0 alt-refksiyon vardır. Kitap, uzun DX'in basit süper geri dönüşten gelmediğini ısrar ediyor, bu da tüm yolda haklı kalmalıdır, ancak düklerden.
- Asgari dük kalınlığı, Kerr ve Hepburn'den biraz daha titiz:

| Band | Minimum kalınlık |
| 50 MHz |  400 m  |
| 70 MHz |  300 m  |
| 144 MHz |  200 m  |
| 432 MHz |  100 m  |
| 1.3 GHz |  50 m  |

- Oran, dalga boyun eğlendirici değildir (yaklaşık 65, 50 MHz, 220 λ 1.3 GHz'de) çünkü konduit'in kenarları "displaced"dir. İngiltere'de boru hatları 144 MHz'de sıkılır, ancak neredeyse 70 MHz'de ya da aşağıda asla ve genellikle 432 MHz, 144'ün zayıf olduğu zaman güçlü olur.
- Boru hattında sinyal 1/d2 yerine 1/d2 olarak azalır: intenuation ücretsiz alana benzer olabilir.
- 2 m (100 W, 15 dBi yagi, 2 dB gürültü kapasitesi): 217 dB, ortalama koşullarda SSB'de yaklaşık 400-500 km; daha fazla (p. 4-7). KK6MC masası ile birlikte.
- Scatter: kaybı yaklaşık 15 dB tarafından her bir saç açısı için frekansı ve 10 dB ile yükselir ( Yeh formülü; metinde sürekli olarak kullanılamaz, bu yüzden W3SZ versiyonundan alınmalıdır.) Konvektif hücrelerden gelen "up", iyi istasyonlarla yaklaşık 500 km, 1000 km'ye kadar rutin olarak kapsar. Ağır yağmurlar ve genişletilmiş koşullar tüm gruplar üzerinde yetersizdir.

Türleri

- Yerde: Yeryüzünde kötü bir şekilde yayıyorlar, çünkü toprak absorbsiyonları ve yayılır; sakin denizler çok fazla gelir (Scilly-Canarie, 2500 km, yaklaşık 2 m). Evaporasyon kanalları genellikle sadece 15 m boyunda ve mikrodalgalara hizmet etmektedir.
- Yüksek: 450 ve 2000 m arasında, zemindeki daha uzun DX nedeniyle; aşağıdaki istasyonların üzerindeki sinyal "yüksek". "kahramanlar daha az güçlü sinyaller veriyor ancak aynı zamanda orta mesafe istasyonlarına da izin veriyor.
- Konduits kesin yönlere sahiptir ve son saatler veya günler, sporadik E’ye aykırıdır.

Index için Yeni Syn Optik Kurallar

- Anticyclone Çağı: hava saatte birkaç metre merkezine düşer ve geri dönüş yüksek basınç geliştirir; daha büyük açılışlar birkaç gün sonra zaten iyi gelir. Kısa vadeli bir antisiklopne yeterli değildir.
- Yüksek basınç kenarı: katman dome ve kenarlarda daha düşük, bu yüzden istasyonlar daha iyi hale geliyor.
- Yön: Daha uzun rotalar kanallar boyunca veya dorsals (Flavell G3LTP, 1984), ancak merkezden gelen mükemmel bağlantılar da var. 29-30 Ağustos 1987: İngiltere'den açılış iki günde 180° döndü (Spain, Fransa, İtalya, Avusturya, Norveç'e) anti-cyclone geçişini takiben.
- Karşı: kalın isobare (güçlü rüzgar), tabakanın kenarındaki stratocumuliform bulutları (en yüksek frekansı)
- Fronts: Soğuk bir cephe sıcak havayı yukarı itiyor ve cephede bir ters yaratıyor; en iyi yön geçen süre boyunca, ama biraz sürüyor.
- Sezon: İngiltere anticyclones yaz ve sonbaharın sonunda daha sık.
- Görsel ipuçları: ufkun üzerinde karanlık bir çizgide sıkışıp kalmış duman; yüksek basınçla, tabakalılar 1000 m altındaki bir zemine işaret ediyor, 1000 m'nin üzerinde yüksek bir dük.

Aramanın alıntısı: R. G. Flavell G3LTP, "Studies of an Extensive Anticyclonic Propagation Event...", * Radio Communication*, Şubat 1984, p. 128; J. Gannaway G3YGF, "Tropospheric Scatter Propagation", * Radyo İletişim*, Eylül 1981, p. 710; P. W. Sollom G3BGL, "A Little Flutter, J. Gannaway, "Tropospheospheric Scatter Propagation", * W6ZGN

## 10. Doğrulanmış bir amatör radyo modeli: Biancomano ve Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, "Practical Tropospheric Model for VHF/UHF Propagation Prediction", * National Air Digest* 22:2, Haziran 1998, pp. 15-24, Free in [PDF from National Air Association](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf).

- Data: Doğu Amerika Birleşik Devletleri'nde 47 istasyondan oluşan 00 ve 12 UTC, 1995-1997; zemin analizi 700 hPa'ye kadar, M. Excluding sahilleri, adalar ve dağlar olmadan N profiline.
- Her anket için: Girişin varlığı ve kotası, maksimum dalga boyun eğebilir (yaklaşık 100 kez dalgalanır), komşu seviyeler ve bir seviye ile ikinci arasında (daha kalın tabakalar için). Zemin dükleri kullanılmaz.
- İlk kural, kalıcılık: istikrar endeksleri (yükleme indeksi, K, toplamlar-toplamlar, SWEAT) 12 saat önce anketden 3 birim içinde kalır ve potansiyel sıcaklığın aynı şekilde büyüdüğü basınç seviyesi. Eğer öyleyse: önümüzdeki 12 saatte gelişmeler.
- İkinci kural, dinamik kontroller: yükselmeyen limit katmanı, CAPE, üç ankette kalınlığın varyasyonu, eğimin sınırları içinde varyasyonu.
- Band eşi: 100 cm veya daha fazla açma yeteneğine sahip bir konduit, 432 MHz'de, 222'de neredeyse kesin ve hatta 144'te.
- Uzay filtresi: Yaklaşık 400-500 km bir hexagonal bir ızgaradaki istasyonlar; tahminler sadece en az 5 kontigue istasyonu olumluysa (birini atlatabiliyorsanız). 5 istasyonla, yanlış rastgele bir pozitif olasılığı ihmal edilebilir hale gelir.
- QST'nin sütununa karşı Doğrulama: 1996'da 48 olay 67 (yüzde 71), 15 büyük olayın 12'sini planladı. 432 MHz'de% 70'e ve 12 saat içinde 144 MHz'de yaklaşık% 60'a kadar doğruluk; 19'dan neredeyse iki kat daha fazla hata geç geldi, çünkü birçok gelişme formu 12 saatten daha az.
- İstasyonlara daha kuzeye eklendi: rüzgar geri dönüşümlü saat tırmanma (hot başlangıç) ekstra bir koşul olarak.

Tamam PDF'den (programın yazarı tarafından yapılan) üç daha detay. Onların kuralı "stickness = 100 kat boyun" 6 m için 600 m, 2 m için 200 m, 70 cm için 125 m (fig. 3): 70 cm için Kerr ve Hepburn (yaklaşık 89 m), VHF/UHF Book ile çakıştı. Saatbilce dönen rüzgar, yazarlara göre, özellikle de yüksek irtifada daha sıcak hava getiriyor, bu da N'nin baskıyla büyüdüğü şeydir; beraberinde getirdiğim şey istikrarsız görünmüyor. Appendix, belirli bir kotadaki keskin ve nemle sabit bir termal gradient atmosferi için kapalı bir form elde eder: kodumuz için test davası olarak faydalı. Potansiyel sıcaklığın eğiminin izlerinin K'dan geliyor. H. Jehn, "Kim optik ölçek radyo meteorolojisinde potansiyel reraktif indeksin kullanımı", *J. Meteoroloji* 17, 1960, 264.

indeksimiz için ne almak: (1) kalıcılık bir terim: profili 12 saat önce karşılaştırın, saat içinde özgürleştiğimizi tahmin edin; (2) uzay filtresi: iyileşme sadece birkaç yüz km bir alanı kapsarsa, bu da yanlış alarmları azaltır; (3) istikrar endeksleri, sıcaklıktan hesaplanan ve 850'ye kadar.

Aynı paketteki başka bir kaynak, bizim için küçük kullanım: P. K. Rastogi, "Stratosphere and Troposphere (S-T) Millstone Hill'deki Çalışmalar", NASA 1984 (document 19840019069): 440 MHz radarı, 4 ila 25 km arasındaki açık havanın çalkantısını ölçen ince tabakaları ölçtü. Bu, saçı üreten turbulence'ın tabakalı olduğunu doğrular, ancak girişler için çok yüksek oranlarla ilgilidir.

## 11. Üçüncü kaynaklar (20 Kasım)

Yüksek giriş tarihi vakalar. J. Macoun OK1VR, Snkwka (1602 m) zirveden 2 m), Sonbahar 1958, 1959 yılında tercüme edilmiştir ([ok2kkw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)). 27-28 Ekim 1958 tarihinde ilk Çekoslovakya-England 145 MHz ( G5YV, 1220 km) ve 23'ten sonra sabit sinyallerle 1100 ve 1250 km arasında daha fazla bağlantı. Yazar, Atlantik'ten kıtaya kadar yavaş bir antisiklope tarafından reddedilen yere geri döndüğü yerde, 2200 m üsttedir. Evden (Prague) en yüksek 530 km idi: sadece yükseklikteki istasyonlar konduit girdi. Başka bir doğrulama vakası ( LA0BY 2018 gibi) ve indeksin 2000 m üzerinde yapılması gerektiğini ve bunu istasyon payıyla karşılaştırdığını onaylar.

Tarihsel kütüphaneler NBS ( klasik eserler aramak, formüller için değil):

- W. Nupen, * Tropospheric Propagation of Radio Waves*, NBS Technical Note 304, 1965, 356 sayfalar, konular için indeks (ayrıca "kanı tahmin") ve ülke tarafından. İtalyan girişleri rapor edildi: L. Sacco, "Ağuk bir süper-refksiyon rejiminde radyo bağlantısı", * Yüksek Frekans* 24/6), Aralık 1955 (İtalyan araştırması: 500 m sık gün ve gece); S. Silleni, *Annali di Geosica* 8(1), 1955; J. M. Clara e A. Antinori, kanıt Sardegna e Minorca da 238 bir 297 MHz (390 km), *Elektrikli İletişim* 33(2), 1956.
- A. L. Abbott, * Tropospheric Radio Wave Scattering *, NBS, Ocak 1960'a kadar malzeme: yılda sipariş edilen yaklaşık 670 ürün: tropos parçası için faydalı.

Açıklama ve gözlemler: F. W. Odenthal DK5EQ, "Ausbreitung 2m - Eine Einführung" (31.20: EI7GL blogunda neredeyse optik yukarı 200 km, tropo, ilkbaharda ve sonbaharda yüksek baskı ve çürük yollarında; DM2HB 'nun VHF sayfasını ve Es. A. Johnson VK3DXE 'nun notlarını sadece yazmıyor. VHF marina ve AIS ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)) hakkında tartışma: Anten başına 1.23 nah (ft) nautical mil, yaklaşık 50 nm, tipik trope iyileştirme 100-400 nm; konduits komşular kadar güçlü. Chelmsford ARS News, Aralık 1983: İngilizce doğu kıyılarının istasyonları kıtaya deniz yolu tarafından destekleniyor. DK5YA

ARRL, * The Radio Amatör's VHF Manual*, 1972, cap. 2 "Propagation Beyond the Horizon" (pp. 17-19). Zaten işletim ipuçları ( yavaşça ve tüm sırt kenarlarını yukarı doğru hareket eden yüksek baskı alanları takip edin; sonbaharın en iyi dönemi; kıyı alanları, büyük göller ve nehirlerin vadileri stratification tarafından tercih edildi). İki yeni veriler: (1) İnci. 2-4 standart atmosferi karşılaştırır (yaklaşık 1200 m): Hesaplamamızın GFS seviyelerinin farkında olması gerekir; (2) doğru alıntılar minimum plaksiyonun tipik bir yarıçapı ile, yaklaşık 4000 ft (yaklaşık 1200 m): Bu, GFS seviyelerinin kabul edilmesi gereken imzadır; (2) doğru iyonlar minimum kalınlıklara göre 144 MHz ve çoğu sıklıkta görülür. aramak için kullanışlı QST bibliography: Hull, "Air-Mass conditions and the correct of Zika Waves" (Haziran, Mayıs 1937), Hoisington, "Painless Prediction of Two-Meter Aboves" (Ekim 1949); Collier, "Upper-Air conditions for 2Meter DX" (Eylül 1955); Botts, "A Night to Remember" (Ocak 1970, toprak üzerinde yapılan bir gece).

PA0EHG, sayfa "Tropo" ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): G3YGF 'nun makalesinden alıntı, zaten manuel olarak. Saç hesaplamamızı kontrol etmek için iki pratik kural kullanın: her istasyondaki ufukta her bir açı için yaklaşık 10 dB ekstra kaybı ve 100 km boyunca yaklaşık 9 dB artı her 100 km (60 mil); ortak hacim yaklaşık 600 m'den başlıyor ve 480 km. İndükleri, anketleri veya refraktif eşleri tedavi etmiyor.

PA0EHG, PROPBER hesaplayıcı ([sayfa](https://pa0ehg.com/propagat1.htm), programın yazarı tarafından sağlanan propber.exe programı). BASIC'de yazılmış 36 kB DOS programı ve Microsoft BASIC Compiler 560 138 ile derlenmiştir: sadece DOS veya DOSBox altında çalışır. İç metinlerden: frekans, mesafe (veya iki lokatörü, aynı zamanda eski QRA formatında "E-locator", her anten yüksek istasyon için, gürültü rakam, FI grubu, güç, bir engel (height ve mesafe) ve anten kazancı (0 = parable çapında hesaplama, verimlilik% 50). Optiklerde en yüksek mesafeyi döndürür, ikisinde de S/N oranı için doğru optik kaybı (toplam 24 GHz'den 0.1 dB/km), Troposcatter'ın kaybı "bölgeler için uygun" (örneğin, engeller tarafından verilen ufuk açıları için) ve S/N oranı iki, "en fazla anormal propagasyonla". Sayısal sabitler derlemede okunabilir değildir. Yüksek gruplar için önerilen yöntem de bizim için kullanışlıdır: Ölçmek ve hesaplanan S/N) ile aynı farkı başka bir gruba uygulayın. Hesaplamamızın aynı modelidir, ancak sabit bir marj yerine S RTM1 profili kullanır.

PROPBER, sabitler gecikebilir (Microsoft İkili Biçim formatındaki numaralar). }}r, metinlere bir sonraki veri masasında: 8,68589 (= 20/ln 10, i.e. 20 logs doğal logarithm ile hesaplandı), 32.5 (ücretsiz alan: 32.5 + 20 log f\[80) + 20 log d\[km) Probable, ancak kod içinde tekrarlanırsınız: -69,5 (Haf/UHF DX Kitabı'nda Yeh'nin yan yana formülü olarak aynı sabit dönem), 332 (petrol reraktif), 8733 km (bitivalenti, k. 1.37). Tam saç formülü programı bozmadan yeniden yapılandırılamaz. T0

PROPBER, kod tarafından yeniden inşa edilen formüller (sözlü kısmın bir araya gelmesi; Operasyonlar BASIC kütüphanesine hafızadaki operatörlerle çağrılmaktadır, iki hassasta değerler dekod olmuştur). Sürekliler -69,5, 332 ve 8733 daha önce kütüphane içinde yanlış pozitif bulundu.

- Optik mesafe: d = 5 · ( UTCh1 + √h2) km ( m) Klasik 4,12 (k = 4/3): k ≈ 2.
- Optiklerde kaybı: L = 32.5 + 20 log f\[MHz\] + 20 log d\[km], artı 0.1 dB/km Yukarıdaki 24 GHz ve 0.3 dB/km 40 GHz.
- Troposcatter: Lts = L + 23 + 10 log f + 0.0674·d, i.e. 55.5 + 30 log f + 20 log d + 0.0674·d (eski 2 dB daha yüksek olan G3YGF) açısal bir terim veya reaktivite yok.
- Düzeltme " siteler için": program engel ve mesafe sorar, ancak bunları kullanmaları gereken blok boş; düzeltme değişkeni 0 kalır, bu yüzden "korrect" kaybı bu saça eşittir.
- Gürültü: T = 290 • (10^ (NF/10) - 1), N = -228,6 + 10 log T + 10 log B (dBW), anten sıcaklığı olmadan.
- S/N = 10 logs P + G\_tx - L + G\_rx - N. "En yüksek anormal propagation", saç yerine optik kaybı kullanır, i.e. ideal konduit davası ücretsiz alanın ötesinde kayıplar olmadan.

Karşılaştırma: 300 km ve 144 MHz PROPBER yaklaşık 189,9 dB, G3YGF 187,9, bizim P.452 187,8 (sırası ufuk açılarıyla): 2 dB içinde saç çakıları. Hesaplamamız, profilden ufuk açılarını, anten ısısını ve yerine "maksimum" tabakasını canlı mat ve intenuation ile sabitledi.

Rohde & Schwarz, P. Denisowski, "Profesyonel Not, Nisan 2023 (24 sayfa, yayılma). Puanları zaten topladı (iki boyutlu bir dalga kılavuzu olarak, mesafe ile lineer bir şekilde düşüren sinyal, günde 1500 km ve ötesinde, saatlerden güne kadar; arazide iyi bir zemin; birkaç yüz metreden birkaç kilometreye kadar yükseldi. Dizin için iki yararlı gözlem: (1) Zaman içinde evrim: Yeni bir oluşturulan konduit genellikle sadece en yüksek frekansları ve kalınlaştırma, en düşükleri getirmeye başlar; saat tahminleri bu nedenle 432 MHz'yi "önceden" göstermeli; (2) istasyonlar konum: en iyi koşullar, yüksek bir konduitin yolundan yakın istasyonlar arasında, bu nedenle sık sık sık sık sık sık sık sık girilmemelidir. İndükler atmosferik hava ile hareket eder.

Site PA0EHG, diğer sayfalar. Tropo için, M. Frederick G0MJW ([scheda](https://pa0ehg.com/srtmp.htm)) PathProfile of M. Willis ([scheda) için rotanın profili, beklenen toplam kayıp ve kota dosyalarının profili, mikrodalgada tropos için güvenilir bir şekilde yargılandı; uçaktan veya yağmurdan saçını tedavi etmiyor. S RTM1 profillerimizi ve kayıplarımızı karşılaştırmak için ikinci bir referans olarak kullanışlı. Sitenin geri kalanı EME, mikrodalga, havastrip (AirScout, SM7LCB haritaları) ve 80 m'deki önlemleri kapsar.](http://www.mike-willis.com/Software/ASTERPathprofile.zip)

PathProfile by G0MJW (Mike Willis), ASTER ve SRTM paketleri ve SiteData (programın yazarı tarafından yapılan; [](https://www.mike-willis.com/software.html)yazılım sayfası). Windows 2018 programı: ASTER veya SRTM'den 1'e kadar profili" veya 3'e kadar, optik, difraksiyon, troposcatter (daha 1000 km'ye kadar), dük ve gazlı, rotanın yüzdeleri ile, sahildeki yolun bir kısmı: yapı P.452/P.1812'nin olmasıdır, ancak yardım (konuş dosyası). SiteData taşıyıcıları dönüştürür ve GT OPO30 'dan pay verir.

En kullanışlı kısım, dahil edilen veri dosyalarıdır: ITU-R P.453 İstatistik haritaları küresel 1,5° (121 satırları 90°N'den 90°S'ye kadar, doğuya 241 sütunu): 1.500 m'den fazla olan ilk 65 m'de gradient dN /dz, 10, 50, 90 ve% 99'luk bir varlık, kalınlık ve yoğunluk yüzdesi; bu yüksek varlık için, temel pay, kalınlık, yoğunluk ve maksimum yükseklik için; artı ve sıcaklık. Yıllık değerler:

| Konum Location | Arazi boru hatları: % / kalınlığı / yoğunluk | Yüksek boru hattı: % / temel / kalınlık / yoğunluk | dN /dz zamanın% 10'unu aştı |
| Napoli Napoli |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | -380 N /km |
| Tyrrhenian (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | -674 N /km |
| Tuzani |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | -520 N /km |
| Brindisi |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | -405 N/km |
| Adriyatik (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | - N/km |
| Milan Milan Milan Milan Milan Milan Milan Milan Milan |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | -172 N /km |
| Udine Udine |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | -176 N /km |

indeksimiz hazır bir iklim referansı: 50 m sabit olası düklerin yerine düklerin kalınlığı ve yoğunluğu ve yoğunluğu, ve bir anomaly "bugün normale kıyasla normal". ITU verileri: rakip olmayan kullanım için ücretsiz, onları halka açık bir versiyonda dahil etmeden önce kontrol etmek. ZO NES01.DEM dosyası (58 MB, 2'nin ızgarası) iç / denizdeki iklim bölgeleri içeriyor; format belgelenmedi ve ben deşifre etmedim. Willis'in denklemleri sayfası sadece fiziksel sabitler, ücretsiz uzay ve anten kazanır.

Dördüncü kaynaklar turu (20 Kasım).

Indermuehle ve Suzuki 2026, CSIRO observatory of Murchison (arXiv 2608.12861) için GFS boru hatlarının tahminidir. N'nin GFS'den 0.25°'ye (yaklaşık 2 m'ye kadar), o zaman isobaric seviyeleri, diskotuvar zeminin altındakiler; her saat boyunca dM/dz sütunu için 3 km, taban ve üst, entegre M açığı; o zaman P.452-16 ve P.1812-6 ile GLO-90 25 rotaları 98 ila 1005 km, 88 MHz'den 2,68 GHz'ye kadar, her saat. Sonuçlar: Median AUC 0,72 25 kanalda; P.452 p zaman yüzdesi bir kalibrasyon sabiti olarak kullanılmalıdır (koice p =% 20), çünkü ITU modeli anında 345 ile tanımlanır; yetenek önceden 3 gün öncesine kadar neredeyse düz kalır. 162 MHz'de (AIS) alınan rotalar,% 67'nin en yüksek yüzde (0.26 ile 0.16) ve % 49'un en yüksek yüzde 28'lik derinliğine sahiptir. GFS, ERA5 'ye kıyasla yarı zamanı görür (4.5%, hücrelerin% 10,4'üne karşı), denizde% 56'ya kıyasla% 73'ü görür. Bir "korridor" indeksi (yol boyunca yoğun boru hatları, minimum 40 m kalınlığı ile tabakalar) sonucu geliştirir: Yol boyunca sürekliliğin doğru kriter olduğunu onaylar.

Longley ve Rice, ESSA ERL 79-ITS 67 (1968): 20 MHz'den 20 GHz'ye kadar düzensiz arazi için yöntem (ITM'den sonra), toprak düzensizlikleri ile {{h, 250'den 400'e kadar Ns, eşdeğer yarıçap 8497 km (4/3), ufuklar, difraksiyon, zaman ve iklim için yerler. İstatistiksel referans, anında değil.

ITU-R P.834-2: İkizler curvature, Dünya yarı eşdeğer k·a, M = N + h/a (h in m, on binlerce km), Üstel model; zaten kullanılanlardan yeni bir şey.

AGARD CP-70 (1970), tropopheric propagasyon: güçlü gradients ile 1 ila 100 m'den ince tabakalar yaygındır; yüksek bir tabakanın yansıma katsayısı yaklaşık 14 dB'nin rastgele dağılımı ile ilgilidir. 90 MHz'ye kadar ölçüler: 9-10 dB'nin azalması yaklaşık 350 m'de yoğunlaştı, bulutların bir katmanında. Soğuk bir cephenin geçişi katmanları ortadan kaldırır: Diffraction kalır.

ABD URSI Raporu, Komisyon 2 (1960): 60 ila 210 m yüksek tabakaların ortalama kalınlığı (P.453), 157-160 N/km duct için minimum gradient.

Buscemi K2OVS, VHF propagation rehberi: grup için geri dönme ve giriş için tipik mesafeler, yüksek istasyonlu baskı ile bağlantılı, en fazla şafak.

Wolinsky-Mancini ve 2024, Canary Adaları'nda AIS: AIS radyo selleme ve resepsiyonlar; dükler sıcak aylarda çok daha sıktır (hazırdaların geri dönüşümü).

Nijeryalı ve Malezyalı makaleler (Ekah, Iloke, Mat, Igwe): sıcaklık, nem ve ölçüm kaybı, zayıf ve çelişkili (r-0,09 ila 027); sadece zemin boyutlarının yeterli olmadığını onaylayın.

Herben 1984, tez TU Eindhoven (mikroonde): 8.2 km ve OTS uydu 11.6 GHz'de; türbülans, multi-söz ve yağmur için. Mikrodalgalar için dinamik kayıplar yağmur ve turbulence tarafından yönetilir, değil.

Baars, The Paraboloidal Reflector Antenna (mikroonde): Parable G =.(TYD/λ)2 tipik verimlilik 0,5-0,65; yüzey hatalarının kaybı Ruze'ye göre,.s = exp(-(4πε/.)2). Parableleri 1296 MHz ve ötesinde Yagi DL6WU 'ye alternatif olarak eklememize yardımcı olacaktır.

Daha şimdiden analiz edilmiş veya fakir içerik: Hirschmann NASA X-731-67-89 (çift), Reid'in VHF ST radarı (harlar ve çalkantısız), Scd kaydırık ve genel notlar troposphere.

Beşinci kaynaklar turu (20 Kasım, akşam).

Rogers 1997, Radio Science 32(1): Bayesian yüksek bir kanal parametrelerini tahmin ediyor (ortalama oranı, M eksikliği, kalınlık) ufkun ötesinde ölçülmüş, parabolik denklemi modeli ile; VOCAR deneyi 143, 263 ve 375 MHz, radyo dalgasıyla karşılaştırıldı. sinyal temele ve M açığına çok bağlıdır (fark için 0,81 ila 143 MHz'e kadar), neredeyse tüm kalınlıktan (-0,17): kalınlık sinyalden elde edilemez. Bizim için: B (climate kalınlığı) açıktan daha az tutar ve paylaşır; ve yöntem arka ofis için temeldir, bu birden çok frekanstan konduit almaktır.

Moler ve Holden 1960, "Tropospheric saç yayılımı ve atmosferik dolaşımlar", J. Res. NBS 64D, pp. 81 ve takip ( 22/11/2026 tarihinde, doi 10.6028/jres.064d.01). California deniz yolları Point Loma (San Diego): Santa Barbara 190 mil, Point Mugu 144, San Clemente 78; 1365 MHz ve 9380 MHz, VHF değil; Ocak-Nisan 1958, ortalama 1 dakika. 26 Mart vakası: Noon'a yaklaşık 3 saat boyunca yaklaşık 20 dB'nin bir takviyesi, bir cyclonik vortex zayıflar ve deniz esintisi ve sübvansiyon başlar; 15 yakınlık reformdur ve sinyal maksimumdan 22 dB'ye kadar azalır. İstatistikler (Table 1, 500 hPa, 33 vaka): ortalama sinyali düşüren hava ile L ve X bandında yaklaşık 2.5 dB daha yüksektir. sinyal, işarette 500 hPa ile hesaplanan farkı takip eder ve bir korelasyon katsayısı olmadan varyasyonların amplitüdünü izler. Low katmanlar (yaklaşık 600 m) saçılma üzerinde kısmi yansımayı tercih eder. Anticyclones ve cepheler tedavi edilmez ve yukarıda belirtilen 60 dB'ye kadar "ekiz" değildir: ikinci el özetinden geldiler. Bizim için: dikey hareket iyi bir tahminor ve +1.5 dB sübvansiyon ve -2 dB için ortalama ölçülen fark (2.5 dB); 20 dBs tek bir durumdur, tipik bir değer değildir ve mikrodalgada.

Banafaa ve Muqaibel 2025, IEEE Access: Boru hatlarının incelenmesi (evaporasyon modelleri, parabolik denklemi, radar tahminleri, GNSS ve capar) ve otomatik öğrenme ile sınıflandırma; Destek vektör makineleri en iyi sonuçlar verir. Bibliography olarak kullanışlı; Veriler Farsça Körfezi'ndendir.

ITU-R P.834-7 (2015): Troposferal refraksiyonu hakkındaki önerinin güncellenmiş versiyonu; zaten kullanılanlardan yeni bir formül yok.

Abbott, NBS bibliography on the troposferous saç (GOVPUB- C13): zaten üçüncü turda. Slide Scribd in the ducts: jenerik içerik. Makale ScienceDirect 0021916987304 açılamazdı.

Altıncı tur (20 Kasım, gece): Bean ve Dutton, ARRL Handbook 2022 ve 2023, Balanis.

Bean ve Dutton, Radyo Meteoroloji (cap. 6) henüz kullanılmadı. Ns için alan duyarlılığı: 0.18 dB per Unit N on the year cycle; 0.14-0,24 dB/N in the daytime cycle (maximum early in the morning), up to 0.2-1,1 dB/N in the ways of 175-200 km in the shower; düzeltmeler 0.8-0.95. Misme'den oluşan parametre: M = a(g\_e - 40 + b·ΔW^) Bir = 0,5 dB, bu da eşdeğer gradient ve termal istikrar {{W'yi bir araya getiriyor: Sahara'da 471 MHz'de 160 km'de sadece istikrar 20 dB'dir. Potansiyel refraksiyon indeksi K (N bildirildi adiabatically to 1000 hPa): Hava kütlelerini ve anticyklinik sübvansiyonunu vurgulamaktadır (Flavell ve Lane).

ARRL Handbook 2022 (99a ed.) § 19.4.3-19.4.4. 250 ve 2000 m arasında kullanışlı inversiyonlar; radyasyonun inversiyonları (sera, sakin hava, kuru toprak) nadiren doğru yol açar; yavaş antisiklonlar, sübvansiyonlara en çok 500 ila 3000 m arasında, akşam ve sabah daha güçlü, kuzey hemi en iyi rotalar yüksek merkezdedir. Sıcak cephe: Önümüzdeki birkaç yüz km iyi koşullar; Soğuk cephe: cephenin arkasında güçlü ama dengesiz bir ters. Foehn (aynı zamanda Alpler'de) ve Dünya Parlak hava, 200-300 m'de sıcak; sahil boyunca 800 km uzar). Scirocco: daha serin deniz havasında Sahra'dan sıcak ve kuru hava, Doğu Akdeniz'den Gibraltar'a kadar çok güçlü inversiyonlar, mikrodalgada 1500 km'den fazla kayıt olası nedeni. Troposcatter: Yaklaşık 800 km, maksimum tipik yarı. 100. ed. (2023) daha kısa bir metine sahiptir: güneş battı ve +20 dB ve +20-50% akış oranına kadar batıklar; Büyük Göllerde 1200 mil, tipik 400-600; Hepburn kapsaması ve haritaları tavsiye eder.

Balanis, Anten Teorisi (4a ed.), § 10.3.3: Viezbicke masası (NBS TN 688, 1976) Yagi için 400 MHz'de optimize edildi: Patlama 0.4 / 0.8 / 1.2 / 3.2 / 4.2 / dB λ → 7.1 / 9,2 / 10.2 / 12.25 / 13,4 / 14.2 dBd. DL6WU formülümüz (8.6 + 8 log10 boks/ boom) 0.8 ile 1-1.7 dB daha az verir. Orijinal (9.4 + 7.8 log10 boom/., 22 Kasım'dan) doğru formülü ile discard 1.2 ve 4.2 dB (12.1 dBd to 2.2 dB karşı 12.25) ve 0.6 dB kalır. DL6WU

Seventh round ( 21 Kasım): QST, QEX, W3SZ, GitHub repository.

QST 2000-2025 (14 sayı, metin tanıma ile 10 yatak). Sadece 50 MHz'in üzerindeki raporlar, modeli doğrulamak için vakalar olarak kullanışlıdır: 5–9 Eylül 2000, ABD'nin doğusundaki 144 MHz'de ve ötesinde; 14-15 Ağustos 2022'de Manitoba-Kansas/Missouri'yi sıcak bir günde, 144 ila 1296 MHz'de neredeyse her şeyi yaptı. QST Kasım 2022 Hepburn haritalarını önerir. Tropo tahminlerinde teknik bir makale yok. FT8 VE4MA N0OY Q65

QEX ve K1JT, Franke ve Taylor (QEX 2020) FT4/FT8 modları üzerinde: FT8 decoding eşleri -20,8 dB, 2500 Hz kanalda evlenebilirlik olmadan, dB; x3Q 17,5 dB. QEX 2023 ve 2026'nın diğer sayıları tropo'yu tedavi etmiyor (üç sayı 2023'ü kapalı taramalar). W3SZ 2019: yavaş mod eşleri ( JT4A -23, JT9A -27, JT65A -25, QRA64A --26, FT8 -21 dB) ve duyarlılık ve frekans değiştirme toleransı arasındaki seçim kriterleri. FT4

GitHub. TropoLink (LukiBox, Apache-2.0): P.617-5 ( N0 ve {{N ile), NBS TN101 ve ITM flanked, ve onların farkları belirsizlik olarak gösterdi; P.617-5: Lc = F + 22 log f + 35 log, + 17 log d + Lc, F = 0.18· N0 · ^ (hs/7, Lc = 0.07| andi-f/troposcatter ( DL2FZN 2013, VHF/UHF Handbook RSGB): Lts = 24,44 + 4,283 ln f + 0.066 d above free space, Falls near PROPBER (0,0674 dB/km) Q RadioPredict: ITM ve ITWOM 3.0 S RTM3 ile, toprak Corins ve NEC diyagramları kullanır. İlgili değil: Tropos (hava uygulaması), opera-adt/tropo ve troposim (Troposferik InSAR için gecikme), tgbot\_troposcatter (as station), sdr-troposcatter-gui (kontrol paneli), Troposcatter-Propagation-Delay ( ERA5 gecikme), Radio-Communication-Project (didactic Simulator)

Sekizinci tur (21 Kasım): NTIA ve ITU modelleri GitHub. NTIA/itm (Longley-Rice, ITS): 20 MHz-20 GHz, ücretsiz uzay kaybı, difraksiyon ve troposs toprak profili, 0,5-3000 m terminalleri, yüzey reaktivite N0 (minimity to sea level, 250-400), yedi iklim, toprak sabitleri ve çeşitli modlar 1 görünüm, 2 difraksiyon, 3 tropos; C++ with.NET Framework.2 İndükleri modellemez: istatistik değişkenliği ile referans "normal atmosfer" olarak hizmet eder. NTIA/p528 ve eeveetza/p528 (MATLAB): ITU-R P.528-5 hava kirliliği bağlantıları için, 100 MHz-30 GHz, 1.5-20 000 m terminalleri, p 1-99%;.NET 4.8 Olası kullanım: hava saç dengesi (iki zemin rotaları – uçak artı radar bölümü). eeveetza/p2001 (MATLAB): ITU-R P.2001-6, all-field modeli diffraction, troposcatter, anormal propagation (conduct and katmanları, Lba) ve E sportsadic, zamanın% 0–100; ITU dijital haritalar (DN\_Median, DN\_Slope, dndz\_01, TropoClim, h0, sörfwv, Es) yeniden dağıtılamaz. P.452 yerine bir ITU karşılaştırması olarak, CSIRO çalışmasında olduğu gibi GFS 345 ile; haritalar P.453 olarak kullanıcı tarafından indirilmelidir. 593 QSO'nun 1.3.0'u (%29): olay / ilişki ayrımı 0,79 Yanlış ve sübvansiyon olmadan; GFS, 144 MHz'de denizde konduit görmüyor (daha fazla düzeltme olmaksızın)

Ninth round (Kas 21,): IU3QEZ, NTIA, PyWaveProp, ACE High, validasyon 1.3.1. IU3QEZ (S. Fabris), "PoF'de ufk değeri 69 mil (GEKO Magazine n. 69, Eylül 2026): ortak hacim d2/(8·R\_eff) R\_eff dB 8500 km (1,3 km ila 300 km Sunumun İncili: Norton 1959 (J. Res. NBS 63D, Sistem Kayıp), Bray 1961, ARRL Book 2023 Numaralı 4.29, Rice-Longley-Norton-Barsis NBS TN 101, Bean ve Thayer 1959, Barsis ve Kirby 1961 (diffration on ridge), Deygout 1966, P.1812, AirScout DL2ALF, P.834 NTIA PropLib (wiki): eHata, ITM, LFMF, P.528, P.2108 PyWaveProp (Python): Parabolik denklem yöntemleri Padé imations, multiple edge diffraction, PETOOL ve RAM Cookings, troposferous and high ducts, düzensiz toprak: GFS (z profili ile bir alan hesabı için mümkün temel, kontrol edilmesi. APRS PropView (Apache 2.0): VHF, APRS paketlerinden gelen haritaları, saatli istatistikler ve açılış alarmları; geçerlilik için 144 MHz veri kaynağı. İtalya'daki NATO troposcatter ağı (fortifications.net): 900 MHz, 10 kW, 20 m parables; Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Vulture, Monte Lauro; P.617 için tarihi test davası. İlgili değil: Gupta 2021 (stratospheric ulaşım), Law 2023 (Arctic ozon), yunus #590 ve opera-adt/tropo (InSAR gecikme), tropo.ino (Arduino gaz sensörleri), tropo-webapi (Uyo) Geçerlilik 1.3.1 564 QSO tropo: AUC 0,79 (model 1.2), 0,80 (+ yumuşak duct), 0,90 (+ yanlış ve sübvansiyon), 0,90 (+ devam etme); konduit renkler (% 16 dB) açılış rotalarının% 46'sında ve karşılaştırmanın% 0'ı. IQ3QC

Aynı tura eklendi. mikelytaev / dalga-propagation (MIT, Python 3): Yaklaşık Padé ve Numerov, düzensiz toprak, tropos global dükleri; değiştirilmiş reraktif M (quota, lat, lon) NOAA GFS ve Monin-Obukhov'un bir evaporasyon konduitini kurdu; yolda kaybı geri döndüren bir işlev. GFS M profili ile gelecekteki alan hesaplaması için en iyi adaydır ( dış bir hizmet veya çeviriye hizmet eder). RP-Sim (AGPL-3.0, JavaScript): Radyo ağları için Empirical modeller (Hata, He, Ericsson, iki ışınları), tropo. RIMFading (GPL-3.0, OMNeT++): ağ simülasyonları için düzensizlikler, Weibull dağıtım, tropo. PisaLayers: kapalı radyo kanalı, zaten dışlanmış. qradiopredict (kantooon, GPL v2): S RTM3 ve Corine ile ITM ve ITWOM v3.0, zaten Q RadioPredict olarak yedinci turda.

Tenth round ( 21 Kasım): Anten Kitabı, Norton, Barsis, yeni veri kaynakları. ARRL Antenna Book (23a ed.), cap. 4 ( K9LA): Bray yöntemi (QST Kasım 1961) VHF aralığı için: 100 mil boyunca Dünya kaybı (300 mil, 144 MHz: 214 dB) eğrileri 50% ve% 99 (fig. 411): 195 dB iki antenin darbesi için 100 mil değerinde; yüksekliğe sahip dB, 100 milden fazla (-3,5 dB) Sunrise ve +20-50% akış oranına kadar günbatımı; Büyük Göllerde 1200 mil, tipik 400-600; büyük su kütleleri tarafından etkilenen bölgeler, en güçlü inversiyonlarla kış. Troposcatter: P.452% 50 483 km ve 144 MHz yaklaşık 204 dB, Bray'nin 214 dB'si ile% 99. Norton 1959 (J. Res. NBS 63D, NIST'de ücretsiz): temel kaybı, sistem ve yol anteni kazanır; Referans atmosferi Ns = 301; Ns'ten eşdeğer yarıçap (250 → 4878 mil, 301 → 5280, 400 → 6997); L(99) 30 Ayaklı antenler ile tablolar (100 MHz: 163 dB 100 mil, 208 dB 500 mil; 1000 MHz: 138 ve 188 dB 28 ayak parables; uzun vadeli yetim V(p,,) açılara bağlı olarak (fig. 13). Rice, Longley ve Norton, NBS TN 15 (1959), NIST'de ücretsiz. Barsis ve Kirby, NBS Raporu 6751 (1961): Pikes Peak 4292 m ile Colorado'da 223 km) kenar, 100 ve 751 MHz; yuvarlak kenar ile tutarlı bir kayıp (yaklaşık 11.5 km yarıçap); zeminden yaklaşık 21 m: Dağ engeli olan yollar aynı uzunluktan çok daha istikrarlı; gün döngüsü; 4-5 loblar sağlanmış, ancak sadece bir tane ölçülüyor (maksimum) ARRL VHF Digital Handbook: paket, APRS, D-STAR, WSJT (Scatter Method ve EME), tropo'da hiçbir parça yok. Yeni kanıtlanmış veya veri kaynakları aradı: ECMWF açık veriler (IFS 0.25°, seviye 1000, 925, 850, 700 hPa..., 975, 950, 900 ve 800; G RIB2 ile CCSDS sıkıştırması, okuyucumuz kodlamaz; zaten açık-Meteo aracılığıyla kullanılabilir; DWD ICON-EU açık veriler (G RIB2 sıkıştırılmış bz2, değişken ve seviye için bir dosya: birçok istek; Kanada GSYİHS ile MSC Datamart ve GeoMet (yaklaşık 15 km, 33 dereceye kadar, alan için ücretsiz olarak kullanılabilir; GeoMet, GAA dosyaları için geçerli değildir);

11. tur ( 21 Kasım): Longley-Rice, GEKO 69. Bean ve Thayer, "A atmosferik Radyo Refraktif Index", Proc. IRE Mayıs 1959 (470 ve seq). Model 4/3 ilk kilometrede doğru eğime sahiptir, ancak çok daha yüksek düşüyor; bir yarının eğriliği ilk 100 m ve% 60'ta gerçekleşir; ortalama ilişki arasındaki ilişki. Ns ve ilk kilometrede azalma: ΔN = 7,32·e^ (0,005577·Ns) (Ns = 313 → {{N yaklaşık 42). Bean ve Thayer, "C.R.P.L. Exponential Reference Atmosphere", J. Res. NBS 63D n. 3 (1959): N(h) = Ns·e^ (-c·h) with c sabit by Ns through the same report; Ortalama Ns USA 313; kiriş izleme masaları (toplayıcı modelle 30.3 mrad with Earth 4/3). MATLAB refraksiyonexp: c = 0.143859 km - 1 for Ns = 313 (yaklaşık 6.95 km), 1 km ve 10 mrad hakkında geçerli. Bizim için kullanın: "normal" karşılaştırma profili, M = 315 + 0.118·z yerine yerel Ns ile CRPL'nin ve ilk kmdaki azalmanın anomalisi -7,32|e^(0,005577·Ns) yerine -40 sabit. NTIA Report 82-100 (Hufford, Longley, Kissick 1982), ITM bölgede kılavuz: kimi ve Ns önerdi (toplayıcı 360, subtropical klas 320, subtropical deniz 370, çöl 280, ılık kıtada 320, ılıman deniz; deniz üzerinde 320, ılıman deniz; Ns = N0 · · ^ (-hs/9,46 km); you use the month minimum of Ns because less impact by super-reflet; Ns = 301. Rice, Longley ve Norton, NBS TN 15 (1959), bölüm I: Arazi ve tropospheric dalgası, TN 101 üssü tarafından zamanla dağıtım formülü tahmin ediyorum. SoftWright, Wikipedia, Altair WinProp ve MATLAB rfprop. LongleyRice: aynı bilgi (vers 1.2.2, 20 MHz-20 GHz veya 40 GHz, 1–2000 km, Ns 301, yedi iklim, zamanın değişkenliği, yer ve durum); deniz iklimlerinin sadece istatistiksel etkisi (zamanın% 10'u için daha yüksek alanlar). GEKO Magazine n. 69 ( IU3QEZ): radyo ufuk 4/3 of 130 km 1000 m, 260 km iki istasyon ekledi; 1-3 km ve 10 km arasında ortak bir hacime sahip, SSB 500 W'den 10 km'ye kadar, SSB'de yaklaşık 40 km'e kadar yaklaşık 10 km'e kadar; Havadaki 2×10 elemente sahip teorik dersler: SSB 50 W dB'nin yaklaşık% 10'u serbest bırakmamalı;

## 12. Yuvarlak masa: Modeli doğrulama veya düzeltme kaynakları (Ka 21 Kasım)

Tüm kaynakların Özeti, kullanım için sipariş. “Devlet”, kaynağın zaten programda (1.3.1) olup olmadığını veya yapılması gerektiğini söylüyor.

### Data to validate (gerçek ne oldu)

| Kaynak Kaynağı | Ne verir | Use Use Use Use Use | Devlet Devleti |
| MMMonVHF yazardan raporlar | 593 QSO, 144 MHz, 29/12/2021-2/1/2022, zaman ve lokatörler | Olay karşılaştırmaya karşı geçerlilik (+14 gün) | kullanımda: AUC 0,79 (1.2) → 0,90 (1.3.1) |
| The World Above 50 MHz | Belgelenmiş vakalar (14–15/8/2022 Manitoba-Kans, 144-10 GHz) | Tarihsel vaka | Tarihsel bir durum olarak kullanılır; geçerliliğe eklenecek QSOs |
| wspr.live (WSPR) | Tamam arşiv, 2 m ve 70 cm, SQL | Birçok olay ve birçok "hayır" (kahkahasız saatler) | Deneyin, filtrelenmesi gerekir (16-20 bin km’de yanlış). |
| APRS PropView / APRS-IS | İstasyonlar hissettim, mesafeler, saatler | Sadece geçerlilik 144 MHz | Bunu yapmak için |
| Ters Beacon (arşist) | günlük nokta | VHF | Şu an için iptal edildi |
| I GRA2 radyosondaggi (Brindisi 16320, Mare 16245 Uygulama, Tuzani 16429, Udine, Milano) | Profiller 00 ve 12 UTC ölçtü | GFS profili kontrolü ve inverss | kullanılmış dosyalar (Brindisi 46 MB), kullanılır |
| Barsis ve Kirby 1961, Barsis 1963 | İhanetler dağlık engeliyle yollarda ölçüldü | Kategoriyi "monte" (Vesuvio, Faito) doğrulayın | Bunu yapmak için |
| Norton 1959, NBS TN 15, TN 101 | L(99) ve masa medyası kayıpları | Scatter control | kısmen (yaklaşık 300 mil tutarlı) |

### Yanlışlar kaynaklar tarafından önerilen

| Düzeltme | Kaynaklar | Geçerlilik üzerinde ölçülen Etkisi | Devlet Devleti |
| Misme Stability Term Term Term Term | Bean ve Dutton kap. 6 6 | AUC 0,79 → 0,90 destekle birlikte | 1.3.1 (Onsuz 16 dB) |
| dikey hareketten subsidence | Moler ve Holden 1960 ( 22/11'de orijinal olarak okunur: +2.5 dB hava yükselir ve deniz tarafından 1.4 ve 9,4 GHz). | Yukarıda olduğu gibi | 1.3.1 (+1.5 / -2 dB) |
| Yumuşak boru hattı ( 3maxmax'a kadar) | Kerr, Hepburn, VHF/UHF DX Kitabı, Shi 2019 | AUC'nun sadece +1'i; olayın rotalarının% 46'sını ve yüzde 0'ını karşılaştırır. | in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in |
| Persistence 24 h | VHF/UHF DX Kitabı, Biancomano ve Leyshon | AUC değişmeden, daha fazla alındı ve daha sahte alarmlar | 1.3.1 |
| Yerel Ns ile Normal CRPL profili (−ΔN = 7,32·e^ (0,005577·Ns) yerine 40 N /km sabit sabit | Bean ve Thayer 1959, NBS Monograf 4 | validasyonda test edildi | 1.3.2'de bir alternatif olarak, kusur nedeniyle |
| Super-refraction 79 N/km (ITU) yerine 100 (Bean ve Dutton, 1000-850) | P.453, Lopez 2008 DX Book | validasyonda test edildi | 1.3.2: prudent ile birlikte agresif bir tahmin ( harita prudent kullanır) |
| Climatolojik kalınlık ( hesap B) açıktan daha az önemlidir | Rogers 1997 | denemek için: A kullanmak | Bunu yapmak için |
| GFS profilindeki Parabolik denklemi | PETOOL, dalga-propagation (Lytaev), Levy (kitap okumak değil, sadece önizleme), Lindquist | ufkun üzerinde alan ilk sürüm sayısal gürültü (AUC 0.12-0,28): Doğru (bölge 8 km, kiriş 6°); AUC 0,54-0,66 | İkinci bir hesaplama olarak kullanımda, tahminlere karar vermez |
| Evanescence: ±7 dB 100 milden fazla, 10-20 dB% 90-99% | Bray 1961, Anten Kitabı, GEKO 69 | marjinal koşullarda yolların eşiği | Kullanımda: dönem ve eşi FT8 -18,6 dB için troposcatter |
| Ortak hacim ve Bray'nin eşdeğer mesafe | IU3QEZ, Bray 1961 | Bilgi Bilgileri | in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in in |
| Troposcatter P.617 ve RSGB belirsizlik olarak | TropoLink, DL2FZN | Bilgi Bilgileri | 1.3.0 |

### Modellerden ne kaldı

Denizde düşük yüksek irtifada (100-200 m) GFS seviyelerinin görmediği (şu anki veri kaynaklarının şekeri); yakındaki dağlardaki yansıma ve kırılma (Vesuvio, Faito): hesaplama için teklif ITM/Longley-Rice, konduits olmadan istatistiksel bir referans olmaya devam ediyor. Yazarın kararıyla kartpostal: AirScout, P.528), E sporadic (P.2001) ve 50 MHz (servoir EIRP bu birkaçının E sporadic ile karıştırdığı ve karıştırdığı)

### Bir çalışma düzeni için

1. Genişletilmiş Geçerlilik: filtrelenmiş WSPR (sadece saatler olmadan) ve QST 2022; 432 MHz için tekrarlayın.
2. Normal CRPL profiline ve 79 N/km'nin ITU eşine bir kez deneyin, sadece AUC'yi yükselten şeyi tut.
3. I GRA2 radyo dalgası ile GFS profillerinin karşılaştırılması ve QSO günlerinde Deniz Uygulaması.
4. Parabolik denklemi sayısal düzeltmeden sonra yeniden değerlendirin.

### 21 Kasım Kararları (vers 1.3.2)

- Kabul edildi: prudent tahmin (Bean ve Dutton, 100 N/km) ve agresif (ITU, 79 N/km) flanked; dükler sadece 2500 m (ilk 1500 m, geçerlilik sonrası yükseltildi); yüksek genişletilmiş baskı ve zayıf rüzgar ile bonus; P.452 μ1 ile ağırlık; ayrıca konduit ile istasyonların payı da kısmi konduitle; ridges ( IT9GSF).
- Alternatif olarak kabul edildi, hatayla çıktı: yerel Ns ile normal CRPL profili.
- Doğru parabolik denklemi sayısal olarak: 0.12 ila 0.54 (30 m anten) ve 0,66 (300 m).
- Land: Copernicus DEM GloO-30 yerine Terrain Kaplamaları «skadi», sahilin 80-100 m denizde hayalet rahatlamaları var ( IC8TEM ’nun imzalanması, Capri'ye göre doğrulanmış nokta); skadi bir rezerv olarak kalır.
- Hava etkileri ile haritada hava işaretleri: ↓ olumlu sübvansiyon (Moler ve Holden 1960, havadaki ortalama 2.5 dB; +1.5 dB), ^ air uphill unfavorable (eski; dB) Çift sembolü, haritadaki yeşil/red/orange renkler ve komik kitap.
- ITM 1.2.2 (NTIA/itm, halk domain), muhabirden çıkıştan sonra normal atmosferdeki üssü hesaplamaya göre CULL'ye tercüme edildi. Discarded qradiopredict code because under GPL v2.

indeks için faydalı bir içerik olmadan: * İletişim Quarterly *, sonbahar 1991 (partipo'da makale yok; Atkins'in Scatter'deki makalesi 1991'dedir).

- Ellerin prensibi olarak kabul edildi ( 21 Kasım): “Uygunluk genelleştirilemez, ancak yerel morfoloji ile ilgili olarak incelenmiştir ( DF5AI, IK3XTV tarafından "Propagation on 2 metres"; tahmin kılavuzunda (§ 1) ve konseylerde belirtilir.
- Kabul edildi (Kas 21, 1.3.3) IK3XTV 'nun transalpine açılışlarının yorumlanması: lensler gibi değerli hücreler değil, ancak troposcatter, ortak hacmin payına ulaştığında yeniden etkinleştirildi (görüler 8 ter). 1.3.4'ten itibaren troposcatter'in geçici bir takviyesel takviyesi olarak noktaya girer, çoğu 3 dB. Aynı makalelere izin verin: Alplerin ötesindeki sinyallerin "oto-amplification" (daha az evsellik) açıklanamaz.

- Kerr (1951), yazar tarafından sağlanan kitap üzerinde gözden geçirildi: § 1.5, eq. 17 ve 18 ve tablo 1.1. Taramadaki eq. 18 rakamları açıklanamaz, ancak tablo 1.1 doğrulanır: 20 ft → 1 cm, 80 ft → 10 cm, 400 ft → 100 cm, 2000 ft → 1000 cm, 1,25, 10, 112 ve 1252 cm'ye karşı.max = 0.014·d ^1.5 (Düzücü değerler) Kerr'in hipotezleri: Konduit içinde 4·10 -8 ayak başına, düşük düklerin tipik değeri; daha zayıf gradientler daha büyük bir kalınlığına ihtiyaç duyar. Kerr ayrıca λmax'ın net bir kesme olmadığı konusunda uyarıyor: dalgalar bazen daha uzun zaman dükten etkilenebilir.

- DL6WU, yazar tarafından sağlanan orijinal (Kablo 22 PDF) üzerinde incelendi: G. Hoch, «Extremely Long Yagi Antennas», pp. 130 ve sonrasında. Fig. 1: 432 ve 1296 MHz'de Alman yazılarının profesyonel bir poligonunda ölçüldü, 7,7 dBd'den örnek bir antenle, tahmini doğruluk 0,5 dB; 2,35 dB patlamanın uzunluğuna kadar, yaklaşık 20 λ. Program 8.6 + 8.0.log10 (L/λ) kullandı. dBd, i.e. about 0.8 dB in less than 1-2 dB; şimdi 9,4 + 7.8·log10 (L/λ) dBd (gradient, 1 dB değeri tabloda okunur, ± 0.3 dB). 10·log10 (7,8·L/ the) net olarak dolaşıma alındı: 2,35'e karşı 3 dB verecekti.
- Booker ve Gordon (1950): "Bu Hafta Citation Classic" (Current Contents, 8 Şubat 1982, H. G. Booker tarafından yorumlandı: Proc. IRE 38, pp. 401-412. Yorum, kılavuzlarda kullanılan iki noktayı doğrulamaktadır: alan, Rayleigh dağılımı ile ufuktan öteye alındı ve düzensizliklerin üstel otokorelasyon fonksiyonu (Gaussian). Orijinal makale okumadı.

## 13. IK3XTV makalelerinden

Flavio Egano IK3XTV (Radyo Rivista: "Propagation on 2 metre" ve "Propagation VHF - convective hücreler ve çekim dalgaları") iki şeyi ekliyor: transalpine açılışları, İtalya'da nerede ve ne zaman kuruldu? İstasyondan gözlemler, önlemler değil: noktalarla kontrol etmek için hipotezler olarak muamele edilmelidirler.

| Yazarın sözleri | indeksin bir bileşeni nasıl olur? |
| Girişler çoğunlukla 100 ile 1000 m arasında oluşturulur; geniş bir alanda yüksek baskıya hizmet eder, rüzgarın iyi olması ve rüzgar yokluğu | Sadece 1500 m altında dük arama; düşük tabakalarda yüksek genişletilmiş baskı ve zayıf rüzgar ile bonus |
| Veneto ve Puglia arasında tekrarlanan sıcak aylarda, 700-800 km, bazen Yunanistan'a kadar; deniz rotaları en sevdiği | Deniz rotasının (örneğin ITU-R P.452) |
| Buna karşılık konduit veya yakın olmalıdır; yüksek antenler ve birkaç metre yol sinyali girmiyor | Konduit üssünün payını iki istasyonla karşılaştırın ( QTH'nin yüksekliğe hizmet edecek) |
| Aynı zamanda, aynı konduit: Gargano'dan 144 ve 50 MHz'de bir istasyondan dinlemek | Kontrol etmek: Hepburn masasına göre 50 MHz 300 m'den fazla liderlik istiyor, bu yüzden olay çok sık bir boru hattı işaret eder. |
| Bağlantılar Veneto – Güney Almanya Alplerin ötesinde, görsel olmayan, neredeyse her zaman öğleden sonra ve yüksek baskı ile; birkaç dakika sonra kaybolur ve yeniden ortaya çıkan dengesiz sinyaller | "konvective" indeksi konduitten ayrı, dağ rotaları için |
| Konvektif hücreler zeminde doğar ve her 3 saatte 1 km yükselir: 15'te yaklaşık 3 km'ye ulaşırlar, Alplere hizmet eden yükseklik | Yol boyunca Open-Meteo GFS'nin limit katmanını kullanın: Kutuların yüksekliğini aştığında puan |
| Alpler, konvektif hareketlerle birlikte, refraktif indekslerin düzensizlikleri yaratır. | hipotezler: zincire güçlü rüzgar, altın dalgaların sinyali olarak 700 hPa'ya kadar; sonları denemek için |
| Çok yüksek invers ( Olağanüstü durumlarda 8 km) yaklaşık 800 km verir, ancak pratikte nadiren 300 km | İnversiyonun payı kapsamı genişletir: bekleme mesafeyi tahmin etmek için faydalı veriler |
| Yaz aylarında Dünya yarıçapı yaklaşık 16000 km (2.5 kez gerçek), yaklaşık 60 dB ile 300 km | K50 = 157 / (157 - {{N): k = 2,5, 94 N/km hakkında {{N'ye karşılık geliyor, bu süper-refksiyondur |
| Sadece 20-30 ° aralığında kuzey ve denizlerin güneyinde, Hadley hücreleri ile bağlantılı, Ağustos ayında Afrika-Avrupa'da zirve; | Avrupa dışında: geri bildirim olmadan Atlantik'te yüksek indeksler vermek ve Canary rotası için - Büyük Britanya |

Aynı makalelerden diğer kullanışlı veriler:

- İki h1 ve h2 metre arasında optik aralığı (d km): d = 4,1 · (√h1 + √h2), normal redüksiyon için yaklaşık% 20'ye kadar.
- Evanescence: Yavaş ve invers ile sığ; saç ve konvektif hücrelerle güçlü ve hızlı. Program, bir gün SNR raporlarını zamanla sınıflandırmak için kullanabilir.
- Sporadik E için 144 MHz Yazar DF5AI ve Davis ve Johnson (Doğa 435, 2005) zamanında ve Es arasındaki bağda şunları bildiriyor: tropo dışında, ancak ayrı bir gösterge yıldırım verilerini kullanabilir.
- Çifte atlama Es: deniz K = 81 ve 5 S /m, kırsal ve ahşaplar K = 13 ve 0.006 S /m, kentsel alanlar K = 3 ve 0.001 S /m.

## 14. LIVETROPOVHF indeksi hipotezler

Öneriye başlamak için: Her bir işaret için hesaplanan ve GFS'den her 3 saat boyunca hesaplanan rotada bir indeks 0-10. sırada, rotanın bir parçasının seçilmiş gruba ne kadar yararlı bir giriş yaptığını söylüyor. Aşağıdaki ağırlıklar benim varsayımlarımdır, noktalarla kalibre edilmelidir.

Bean ve Dutton ve Kerr: Profil eşleri masa 6.5'inden gelir ve ölçümlerinden ölçeklenir, bu yüzden refraktif bir tabaka normal profil üzerinde 10-25 dB değerindedir. indeks daha sonra dB'de ilk hesaplanır ve sonra 0-10'da alır; bu scissordaki değerler noktalarla kalibre edilir.

Nokta ile hesaplama, adım adım

1. T deşarj, göreceli nem, geopotential yükseklik ve dikey hız 1000, 975, 950, 925, 900, 850, 800, 700, 600 ve 500 hPa, artı T ve 2 m nem, zemin basıncı, tabaka ve rüzgar 10 m.
2. Zeminin altındaki seviyeleri sil; N ve M'yi her seviyede hesaplar (ITU-R P.453).
3. Her bir dört Bean ve Dutton (1000-850, 850-700, 700-600, 600-500 hPa) ve tablonun alt-refraktif, normal veya süper-refraktif olarak, masalarının eşleri ile aynı zamanda en güçlü gradient ve payını bulmak için orta düzeyde kullanır.
4. Nerede dM/dh < 0 Girin, kalınlık d ve açık {{M.
5. Kerr formülünden grup için minimum kalınlıkla karşılaştırıldığında (yaklaşık 180 m, 432 MHz için 89 m, herburn masasında olduğu gibi); konduit hala sayılıyor, ancak daha az.
6. Aşağıdaki tabloyla dB'de kazanıyorum (en iyi tabakayı almazsınız, katmanların toplamı değil, artı anomaly'nin terimi) ve dönüştürme: index = dB / 2.5, 0 ve 10 arasında, bu yüzden 25 dB değer 10. Olumsuz bir değer (başlangıç) 0 ama yol verilerinde nasıl kaldığını gösterir.

| En iyi katman durumu | Kriterleri Kriterleri | Tahmin edilen kazanç tahmini |
| Normal (zenfekte değil) | -dN /dh sekme aralığında. 6.5 (e.g. 20–60 N /km 1000 ile 850 hPa) | 0 0 0 0 |
| Slightly above normal | Normal grubun üst sınırı ve süper devre eşiği arasındaki | 0 to 10 dB, in rate |
| Super-refraction | Katman eşinin ötesinde (100, 80, 70, 50 N/km tırmanma) ama 157 N /km | 10 ila 15 dB'den, 157 N /km'ye kadar |
| Grup için çok ince boru hattı | dM/dh < 0, kalınlık aşağıda Kerr eşi | 15 15 |
| Grup için kullanışlı bağlantı | dM/dh < 0, Kerr eşinin üzerindeki kalınlık | 20 dB, +1 dB her 5 M açığın birimleri, maksimum 25 |
| Sub-refraction | -dN /dh < 0 alt tabakada | - 10 dB |
| Anomalous arka plan (You add) | Model Ns eksi ITU harita iklimolojik harita O ay için Ns | N birimi başına 0.2 dB, -5 ve +5 dB |

Yolla Hesaplama (iki yataklı arasında): her 10 km, zaten yolda indeks için program yaptığı gibi, ve temelin ortalama payı ile noktaları hesaplamak. CSIRO çalışması, hattın (162 MHz'deki AIS rotaları kontrol vakalarından% 67 daha yüksekti).

Yol başına hesaplama eklemek ( LA0BY ve W3SZ): (1) Deniz istasyonları, konduitin tabanıyla karşılaştırılır, çünkü yüksek bir konduit sadece giriş yapanlara hizmet eder; (2) Yeh yöntemiyle, başlangıç köşeleri ile, dB'de kazanılır; bu kaybı ve sonucun kazanılması, istasyonun üstesinden gelmek için yönetir ( KK6MC masası). Böylece indeks aynı zamanda erişilebilir mesafenin tahmini haline gelir.

Bir seferde denemek için düzeltmeler ( Bean ve Dutton ve LA0BY, IK3XTV, G4MCU zaten manuel olarak), sadece noktalarla kıyaslamalarını sağlamak için tutulur:

- Mix ( yazara göre önemli önemli, günlük gözleme göre): gün, orta sabah ve öğleden sonra açık bir gökyüzü ile veya birkaç m /s üzerindeki rüzgarla, zemindeki endükleri azaltılmalıdır; katkıları azaltılmalıdır. Denizde değil.
- Günün zamanı ( yazara göre önemli olan önemli olan, günlük gözlem: irrasyonel girişler gün batımında doğar ve güneş battırılır; 3 saatlik bir tahmin kontrolüdir.
- Dikey hareket: hava 850-700'e yükseldi hPamixes the katmanları (negative düzeltme), hava onları güçlendiriyor (pozitif).
- Basınç trendi: son saatlerde yolda, daha yüksek ve istikrarlı sinyal; azalır, daha düşük sinyal (bastano 2 hPa).
- Synonym: 1025 hPa'nın üzerindeki yüksek basınç, zeminde, soğuk denizde akan sıcak ve kuru hava, deniz yoluyla.
- Dağ yolları: ikinci bir "konuşma" indeksi (bölüm IK3XTV), yol ve yüksek basınç boyunca sırt çantalarından daha yüksek.

VHF/UHF DX Kitap, diğer düzeltmeler denemek için: önceki 24 saat içinde yüksek basınç (daha fazla gün, daha fazla puan), sadece yüksek gruplar için karşılaştırıldığında pozisyon.) Yolların yönü için: Isobare ile rotanın azmini karşılaştırın.

Verification Verification

- LIVETROPOVHF arşivi: Her ticari yaklaşık 500 km ila 144 veya 432 MHz bir "event"dir. Olayların frekansıyla ilgili indeksi karşılaştırır, aynı gün ve ay ( CSIRO çalışmasının aynı yaklaşımı).
- Hepburn ve F5LEN ile aynı günlerde karşılaştırıldığında: Üç indeksin birbirine bağlı olduğu yerde, kimlerin yerlere göre doğru olduğunu görürsünüz.
- 00 ve 12 UTC'nin Radyosuları: Model, prob önlemlerini görürse kontrol etmek.
- LA0BY 'nun tarihi vakaları (6–11/11/2003, 10/12/2004, 15/10/2011, 11/2/2015, 22/11/2018): Bu günlerde endeks, herburn'un göstermediğini görmek zorundadır.
- Kılavuzda ilan edilecek limitler: GFS basınç seviyelerinde neredeyse görünmez 100-200 m altında yapılır; 2-3 gün boyunca güvenilir bir tahmin, sonra sadece trend.

## 15. 21 Kasım'ın kaynakları (ikinci bölüm)

- ITU-R P.1812 (MaTLAB/Octave Uygulaması I. Stevanovic, OFCOM; GitHub eeveetza/p1812, Zenodo 10.5281/zenodo.6141558). güzergahın özel VHF/UHF alanı yöntemi, P.452: "delta-Bullington" diffraction, troposcatter, ducts and reflect on katmanlar, engulfs on zemin. P.452'ye bir sonraki üçüncü karşılaştırma olarak, nokta hesaplaması; {{N ve N0 verileri tam ITU ürünleridir, bu yüzden kullanıcı onları indirir (P.453).
- Costa, Rabanal Ramirez, Dias, IET MAP 2018 (doi 10.1049/iet-map.2017.0992). 563 MHz, ormanla düzensiz arazide 5 ve 30 km arasında 27 puan: P.526 bitki örtüsü ile 12 m -4,3 dB ve dB 8,8 dB; P.1812 10 m dB ve dB 8,9 dB; dielektrik dB ve dB 9,2 dB; P.1546 +5,0 dB ve dB. Program için: İyi bir difraksiyon yönteminin kalıntı hatası yaklaşık 9 dB ve ridges (10-12 m) sayıları.
- PETOOL (Ozgun e sunri, Computer Physics Communications 182, 2011). Parabolik denklemi, geniş bir açı ile bir ve iki rotaya bölündü ( 40-45°'ye kadar, 10-15°'ye karşı en dar olanı); dönüştürülmüş Fourier karışıklığı ile kayıpların altında; adımlara zemin. 1.3.2 seçeneği onaylayın ( çapında açı, toprak katları) ve dik rahatlamalar için iki yönlü hesaplamayı gösterir.
- MathWorks. `tropopl`: Gaz kaybı ve «lente» bir tabakalı atmosferde kaybı (P.835, P.676, P.453), dükleri olmadan: gradients aşağıda -157 N/km geometrik optik model uygulanmaz. Radar yayılımı örneği,.N = 7,32·e^ (0,005577·Ns), programda zaten aynı CRPL raporu. `propagationModel` Longley-Rice: önceden tanımlanmış yatay kutuplaşma değerleri, iletkenlik 0.005 S /m, olasılık 15, reaktivite 301, ılıman kıta iklimi. Hiçbir MatematikWorks modeli ducts ile ilgilidir.
- ITU-R, grup dijital ürünleri 3. P.453, P.452, P.1812, P.2001, P.528, P.1546; "tegrated" verileri tavsiyelerin bir parçasıdır; hiçbir yeniden dağıtım lisansı ilan etmediniz: kullanıcıya indirmelerine izin vermeye devam edin.
- ESA Navipedia, troposferical gecikme. N = Nidrostatika + Numida; kuru zenital gecikme yaklaşık 2.3 m ve on cm olan humid'dir. Bizim için: GNSS istasyonları tarafından tahmin edilen ıslak gecikme, şimdi yayınlanan bir nemin dolaylı bir ölçüsüdür; Bugün kullanılabilir değildir.
- Bray, K2LMG, «A Method for Determining V.H.F. Station Capivity», QST Kasım 1961, pp. 36-40 (ilk olarak hazır; 22 Kasım'da QST sayısı üzerinde yeniden kontrol edildi: aşağıdaki tüm değerler karşılık gelir). Yazar, D. W. Bray (General Electric, Ithaca), K2GOI ve W4LTU ile planlanan bağlantıların aylarca doğrulandığını söylüyor. 13.5 dB zamanında yaklaşık 100 mil boyunca (1 dB aşağıda ve 7 saat üstü) neredeyse lineer aşağıda (3,5 dB 50 mil): sağlam bir bağlantı için 7 dB. ufkun düzeltilmesi: ufkun her yüksekliği 69 mil değerinde (111 km) ekstra mesafe (daha az negatifse). Yaklaşık 10 × boom/, (güç oranı), yaklaşık 4 dB toprak yansımasını elde edin; SSB dB3 dB ve AM -7 dB saat boyunca karşılaştırılır: 180 dB ile 60 mil, 90, 200 ila 240 (2 m). 1.3.2 dönem için olasılık. = 3,4 dB (±7 dB ±2.) yerine 5 dB tahmin edilen ikinci el.
- İki yönlü hesaplama ( prototip, 21 Kasım). Gerçek S RTM1 arazisi ile büyük açı parabolik denklemi Capri, 144 MHz, zeminden 10 m, standart atmosfer; ölçü: yoldaki rotadaki 20 ila 400 m arasındaki ortalama güç. Omur ve Marina Grande'nin merkezi kapalıdır ( +11-21°, Monte Solaro): Faito'yu işaret eder (yaklaşık EA5 ve EA6, İspanya'ya yönelik sinyal 10-29 dB'ye doğru fırlatılır; Vesuvius'u işaret eder (daha sonra hesaplama planından sadece indicatif olarak) 17-25 dB. Anacapri'den batı özgürdür ve 8-22 dB'nin doğrudan kazanır. Ancak prototipin 1000-1300 km üzerindeki mutlak kayıplar güvenilir değil. EA3 EA5 EA6
- Barsis ve Kirby, NBS Report 6751 (1961), Pikes Peak, 223 km, 100 ve 751 MHz. Dağ optikte iki rotanın ortak bir terminalidir: saatlik medyanın dağıtımları, iki rotanın kapatılmasıdır. Yaklaşık 14 dB'nin yaklaşık% 99'u, eşit uzunluk veya açının 26-40 dB'ye karşı; hiçbir uygulanabilir gün değişikliği yok. sinyal, troposcatter'dan daha güçlüdür, ancak ideal kenardan daha zayıftır: üst yuvarlak olarak tedavi edilmelidir (yaklaşık 11.5 km). Yüksek sinyaller her zaman arkaların arkasında gözlemlenir.
- Barsis, NBS Report 7652 (1963), bir dağ üzerinde üç rota (Renkado, Alaska, Avrupa), 750-900 MHz. Ortalama kayıp 10-20 dB, terminallere yakın olmayan Fresnel-Kirchhoff hesaplamasından daha yüksek; Evansanlık, optiklerdeki iki çizgiyi kapatarak tahmin edilir; terminallerin yakınındaki toprağın yansımaları uzun süre bozulmaz. 1.3.2'den itibaren konduit, P.526'nın turunu ekliyor.
- Barsis ve Johnson, NBS TN 88 (1961). Uzay dalgasının çözülmesi, hatta bir dağın ötesinde bir yol üzerinde (§ 5).
- Norton, « radyo dalga yayılımında sistem kaybı», J. Res. NBS 63D (1959). Bray'nin nomogramları aldığı eğriler; sistem kaybı, iletimi ve temel tanımları, süreç anteni kazanır.
- Rice, Longley, Norton ve Barsis, NBS TN 101 (1965–67), Volumes 1 ve 2. Tek kenar ve engel, ve yansıma olmadan (§ 7, Pikes Peak örneği ile), uzun vadeli yetim V (0,5) ve Y (q) iklimler için (tamperyasal ve diğerleri için). Bizim için: yuvarlak engel ve değişkenlik işlevleri yöntemi kalibre belirsizliğe referanstır.
- ITU-R P.526-16 (11/2025). J ( the), programın aynısı; Rounded engel (32) (32)-(36), şimdi kullanılmış; multi-split yöntemleri (Bullington ve tam yöntem § 4.5).
- ITU-R P.1812-8 (2025) ve -7. Tamamlanan metin: 30 MHz'in 6 GHz'ye özel yöntemi (delta-Bullington, troposcatter, ducts and yansımaları, ingombs). 7, troposcatter'in (P.617-5) 66 km altında yanlış olduğunu uyarır. Mümkün bir üçüncü karşılaştırma kalır.
- Paulus, «Plavatical application of the IREPS evaporation model», NOSC TR 966 (1994) ve Radio Science 20 138. Rapor 22/11/2026 (DTIC ADA146528, tanınmış metin ile tarama, bazen silinemez). Gemiler tarafından ölçülen hava- deniz farklılıkları istikrara doğru kaydırılır: Gemi verilerinin% 31-35'inde daha sıcak deniz havası NOAA boarlarının% 0.7-7'si, gemi ve SST tarafından soğutma suyu (yaklaşık +1 °C) tarafından rahatsız edilir. Jeske modeli daha sonra 40 m ve yılın %15'inden fazlasını verir ve 10 ila 40 m'tan 24 saat içinde sabit zamanlı atlayan yükseklikler, radyo önlemleriyle doğrulanmadı. Teklif edilen düzeltme (parçalı eşler 0, -0,5 ve -1 °C; -1 °C, Haziran 1982'nin radyo önlemleri ile daha iyi kabul edilir): hava- deniz farkı -1 °C veya daha az ölçülmüş fark; aksi takdirde yüksekliği -1 °C ile ve 0 °C ile hesaplarsınız, eğer -1 °C daha düşükse, bunu tutarsınız. istikrarlı tarafta artışlar kesildi, azalır. Haziran ayında, Marsden 120 kare: ortalama 16.8 m (climatoloji) 7.6 m. Modelin denklemleri raporda değildir ( Jeske ve Hitney’e uygulanır). Program, istikrarlı exaggerasyona sahip olmayan tarafsız profili kullanır ve 40 m'ye sınırlar: aynı yönde gider; modellerin SST'leri ile (önemli değil) Paulus'un daha az ağırlık verdiği ölçüm hatasıyla.
- Levy, «Parabolik Equation Methods for Electromagnetic Wave Propagation» (IEE 2000). Google Libri'de sadece önizleme vardır: Okumuyorum.
- VK3FS (3fs.net.au). Kalite girişi: 450-2000 m'de daha uzun süre açıklığa kavuşturulur; sakin ve sakin gecelerde 300-600 m inversiyonlar; öğleden sonra daha güçlü kıyı inversleri; 800-1300 km tipik, 1400-1600 km sık sık yapılır. Gün/ gece uyarıları ve 2500 m tavanı ile tutarlı.

## 16. Kaynaklar

Kurallar ve formüller

- [ITU-R P.453-13 (2017), radyo refraktif indeks:](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)formülü ve veriler
- [ITU-R P.452-18 (2023), 100 MHz'in üzerinde yer istasyonları arasındaki müdahale tahmini: eşdeğer yarı, LR0, troposcatter,](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)ductss
- [ITU-R P.617-5, radyo köprüleri için ufkunun ötesine geçiyor](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)
- [Shi et al., "Farklı Evaporation Ducts", IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)Q

Hava modelleri ile giriş tahminleri üzerine çalışmalar

- [P. Lopez, "Yer tabanlı radar meteoroloji için süper-refksiyon global iklimolojisi", ECMWF Tech. Memo 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO), "Tropospheric Ducting Prediction based on GFS Model Data at... CSIRO Murchison Radio-astronomy", Gözlemevi arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen ve al. (FMI), "AROME ile atmosferik ürperasyon koşullarını tahmin etmek için model kararının etkisi", EGUsphere 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

Amatör radyo tahminleri

- [W. R. Hepburn, "The Hepburn Tropo Index (HTI)"](https://www.dxinfocentre.com/propagation/hti.htm)ve [](https://www.dxinfocentre.com/propagation/history.htm)yöntemi tarihi
- [W. R. Hepburn, "En Düşük Usable Frekans vs. Ducting Katmanının Kalınlıkı"](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, "Dünya çapında refraktif indeks tahminleri"](https://tropo.f5len.org/WW/info.php), [yeni versiyon 2026](https://tropo.f5len.org/?p=504), [model ARPEGE](https://tropo.f5len.org/author/f5len/)]

Data Data Data Data

- [Açık-Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api), [baslık seviyelerinde değil](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)
- [NOAA NCEI, Tümleşik Global Radiosonde Archive (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RAOBget, Wyoming](https://github.com/NCAR/RAOBget)

Klasik metinler (programın yazarı tarafından kanıtlandı, son formüller için danışılmalıdır)

- B. R. Bean, E. J. Dutton, * Radyo Meteoroloji*, NBS Monograf 92, 1966, 435 sayfalar (bas Dover 1968): N ve M, gradient of trap under-157 N/km, radyo dalgası ile kıyaslama; bölüm 5 "Syn optik radyo meteoroloji". Base for 2-4 steps of hesaplama per point. Free: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [NIST kartı, doi 10.6028/NBS.MONO.92](https://www.nist.gov/node/1770911), [İnternet Archive](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf)kopyası.
- L. Boithias, * Radio Wave Propagation*, McGraw-Hill 1987 (Fransızca, 330 sayfa, ISBN 0070064334; aynı zamanda Kuzey Oxford Akademik): Komling anten-mezzo'nın ufkunun ufkunun ötesindeki mekanizmalar. Sadece (Kitap Kartı](https://libcat.library.tamu.edu/Record/in00000014812/Description))
- M. P. M. Hall, * The Troposphere'ın Radyo İletişimi Üzerine Etkisi*, IEE Elektromanyetik Dalgalar Serisi n. 8, Peter Peregrinus 1979, 206 sayfalar (ISBN 0906048257; brosura 08634108: sin optik durumlar ve propagation VHF/UHF. Syn optik malzemeler için Base. IET'de Ticaret ([](https://libcat.library.tamu.edu/Record/in00000636385)kart).
- H. G. Booker, W. E. Gordon, "Bir Radyo Scattering in Troposphere", * IRE*, vol. 38, n. 4 Nisan 1950, pp. 401-412: Reraktif indeksin disogenity of the disogenity of the refractive index. Orta fikir: Kısıtlanmış güç, reraktif indeksin (exponsal korelasyon); ufukta üstünler difraksiyonuna, daha fazla yayılabilir; Yazarlara göre, meteoroloji tarafından beklenen indeks varyasyonları ölçülmüş alanları açıklamak için yeterlidir. 328 makale (Scilite), ücretsiz erişim kopyası değil. Semantic Scholar ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); tam metin IEEE Xplore ödedi ([doi 10.1109/JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)).
- C. M. Claw, J. R. Gerhardt (Elektrikli Mühendislik Araştırma Laboratuarı, Texas Üniversitesi), "Matematik Teorisinde Yapılan Parametrelerin Kabul Edilmesi", * IRE*, vol. 40, n. 1, Ocak 1952, pp. 50-54, doi 10.1109/JRPROC.1952.2739, Booker ve Gordon'un sadece tahmin ettiği gerçek değerleri verdi ([Skilit](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)kartı).
- "Atmospheric Refractive Index at Radio Frequencies", * IRE*, vol. 41, n. 8, Ağustos 1953, pp. 1035-1037: o zaman kullanım için (79 K/mb ve 4800 K) yükseltmeyi teklif eder. N; buradan 77,6 ve 3,73 · ITU ([Q) formülün 10.5'i ([Q) değerleridir.](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E. C. S. Megaw, "Fundamental radyo saç yayılımı teorisi", * IEE Part C: Monographs*, vol. 104, n. 6, Eylül 1957, pp. 441-455, doi 10.1049/pi-c.1957.0052: Türbülans teorisinden gelen indeks dalgalanmaları, ortak ufuktan ve geometrinin etkisi](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)
- * IRE*, Ekim 1955, saç yayılımı üzerine özel numara (K. A. Norton ve J. B. Wiesner), Bullington, Staras ve diğerleri ile: PDF'de ücretsiz](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf)
- D. E. Kerr (edited by), * Kısa Radyo Dalgalarının Değerlendirilmesi*, MIT Radyasyon Laboratuvar Serisi vol. 13, McGraw-Hill 1951 (Restampe Dover 1965 ve IEE 1987): Denizde buharlaşma ve yüzey iyonları teorisi. Dijital kopya: [Dünya Radyo Tarihi](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); [IET Digital Library](https://shop.theiet.org/propag-short-radio-waves).

VHF'de troposal difüzyonun son önlemleri: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Avustralya), "Güney Avustralya'daki VHF Troposcatter İletişim Kanalları", * İletişim Sistemleri için Dijital Sinyal İşleme*, Springer 1997, pp. 103-112 (Springer](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)). Bir ücret için metin; soyutlamadan diffüzyon, konduits değil. Sadece VHF kanalı verileri ile troposcatter terimini kalibre etmek istiyorsanız.

Numerical modeller ve deniz rotaları (programın yazarı tarafından belgelendi)

- M. Levy, * Elektromanyetik Dalga yayılımı için parabolik Equasyon Yöntemleri *, IEE Elektromanyetik Dalgalar Serisi #45, 2000, 336 sayfa (ISBN 0852967640): yol boyunca değişen ve orografi için parabolik denklemi; Troposous propagasyon, ışınları ve yolları, deniz üzerinde yayılım. Yol boyunca bir alan hesaplaması için hizmet eder, indeksin ötesinde. Ödeme için, IET'den elektronik formda ([IET kartı](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)).
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (University of Leicester): Kanal Adaları ve Kanal üzerinde 2 GHz ölçümler. Birçok eser ve tez üniversite arşivinde ücretsizdir: [Vücudyumda uzun vadeli istatistikler, Radyo Bilimi 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434), [üç deniz rotalarında anlık değişiklikler, Radio Science 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710), [Titles of N. Mufti 2012, 50 ve 140 km aynı zamanda VHF](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854), [I. Alam 2002'de hava verileri, 240 MHz ve 2 GHz X3 UQ, [tesi.](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus, "Evaporasyon modelinin pratik uygulaması", Radio Science vol. 20 n. 4, 1985, p. 887 (A. IEEE Transactions on Antennas and Propagation), doi 10.1029/RS020i004p00887 ([scheda](https://zendy.io/title/10.1029/rs020i004p00887)). Teknik raporu daha önce serbest bırakın: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf).

T. Lindquist, * TheWave Propagation Models in the Troposphere for Long-Range BL/SHF Radio Connections*, master's tez, University of Karlstad, 2 Haziran 2020, 106 sayfa (programın yazarı tarafından alıntı). Parabolik denklemi ile bir hesaplama yazmak için bir adım kılavuzdur: bölünmüş adım çözümü ( geniş açılarda bile), arazi ve deniz için sınır koşulları, zamanlarda lineer profilleri takip etmek için zemin, sayısal çeklerle; MATLAB için PETOOL ücretsiz araç. Bir kerede iki yararlı veri: (1) Hava değişkenlerine karşı hassaslık, theN the 0.26.P + 4,3.e - 1.4 theT (hPa ve K, yaklaşık 20 °C), yani buhar basıncındaki 1 hPa'nın bir hatası, ilk önce kontrol edilmesi gereken modelin ne kadar düşük olduğunu: (2) Kuzey Denizi'ndeki 5-6 m. Yazarın sonucuna (10 GHz'ye 1 ila 10 GHz): ufkun ötesinde, güvenilir bağlantıları neredeyse her zaman bir dük gerektirir.

Toprak profilinde (ufk ve engeller için beklenmeyen tropo için değil)

- [Radio Mobile](https://ve2dbe.com/rme.html)by Roger Coudé VE2DBE: ücretsiz, Windows (ayrıca Linux ve Mac ile Wine), Longley-Rice/ITM modeli, arazi profili otomatik olarak kota verileri ([download](https://ve2dbe.com/download/download.html)) satın alındı.
- [SPLAT!](https://qsl.net/kd2bd/splat.html)John Magliacane KD2BD: GPL, 20 MHz-20 GHz, Longley-Rice ve ITWOM v3.0 modelleri, metindeki engel ve kayıp raporları; 2014 yılının 1.4.2.
- [ITM, NTIA/ITS](https://its.ntia.gov/software/itm)resmi kodu: Longley-Rice modeli (20 MHz GHz-20) kaynak ve dokümantasyon ile, Radio Mobile ve SPLAT üssü! Kod ücretsizdir ve LIVETROPOVHF'nin yolundaki hesaplamaya ekleyebilirsiniz (toprak nedeniyle, tropo indeksine eklenecek). Orijinal rapor: A. G. Longley, P. L. Rice, " Tropospheric Radio Transmission Over Irregular Terrain: A Computer Method – 1968", ESSA Technical Report ERL 79-ITS 67, Temmuz 1968 (tarihi yazılımdaNTIA kartı: X1 https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITU-R, yazılım ve dijital veriler](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): P.452 ve P.617 tarafından kullanılan grid 1,5°'deki ücretsiz {{N ve N0 yıllık medya haritaları - indeks için referans iklim değerleri olarak yararlı.
- Pathloss (Contract Ttelekomünikasyon Mühendisliği): Radyo köprüleri için ticari program; ITU tavsiyelerinin uygulandığı bir kamu kaynağı bulamadım, bu yüzden kontrol etmek için.

Reusable GitHub Code in LIVETROPOVHF

| Project Project Project Project | Ne yapıyorsun | Dil Dili | Bizim için mümkün kullanım |
| [NTIA/ITM](https://github.com/NTIA/itm) | Model Longley-Rice NTIA resmi, paketler için. NET |  C++, C#  | Yolda arazi kaybı: Doğrudan programa entegre edilir, C# |
| [fuzzzee/itm](https://github.com/fuzzzeee/itm) | ITM C++ kodu C# çeviri |  C#  | Yerel kütüphaneler olmadan alternatif, Windows 7 versiyonu için de yararlı |
| [eeveetza/Py452 x0Q](https://github.com/eeveetza/py452) | ITU-R P.452, ITU-R WP 3M tarafından onaylanan MATLAB/Octave versiyonuna benzer; İsviçre Federal İletişim Ofisi tarafından yayınlanan MATLAB versiyonu | Python MATLAB | tercüme etmek için referans C# troposcatter formülleri ve ITU doğrulama örnekleri ile yapılır |
| [bwinkel/pycraf](https://github.com/bwinkel/pycraf) | P.452 tam, P.676, SRTM profilleri; GPL v3, ancak dahil edilen İTÜ veri dosyaları ticari kullanım için ücretsiz değildir |  | Sonuçlarımızın kontrolü |
| [ITU-Rpy](https://github.com/inigodelportillo/ITU-Rpy) | P.453-13 dahil olmak üzere birçok ITU-R tavsiye, ITU örnekleriyle doğrulandı |  | P.453'ün N formülü ve iklim haritalarının doğrulanması |
| [NCAR/RAOBget](https://github.com/NCAR/RAOBget) | University of Wyoming'in metin formatındaki radyo dalgasını indirmek |  | C# İtalyan istasyonlarının profillerini indirmek için diyagramlar |

Not: P.453'ün mevcut versiyonu [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), ücretsiz olarak şarj edilebilir; Belgede 13'ü gerçek olmak için kullandım.

Zaten program kılavuzlarında ve yeniden kullanılabilir: LA0BY (Nordic VUSHF 2019), IK3XTV ("Propagation VHF - convective hücreler ve çekim dalgaları", Radyo Magazine, G3YGF (QST 1983), VHF/UHF Book cap. 2, G4MCU (2003) DF5AI/DK5YA KP4MD
