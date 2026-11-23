LIVETROPOVHF · teknik belge 2
# Haritanın inşaatı

> İtalyan tarafından otomatik çeviri: şüphesiz İtalyan kılavuzuna değer.

sekme haritası ve nokta haritası doğar: coğrafi baz, projeksiyonlar, herburn ve F5LEN endekslerinin ortalamaları, izobare, noktalar ve istasyonlar dinliyor, tabakalarla çakıştı. 9 Kasım 2026 versiyonu.

## Özet

- Harita sekiz katta program tarafından tasarlanmıştır, her biri [8] kutusu ile.
- Base: [9] detayının iki seviyesindeki doğal Dünya sahili ve sınırları; [10] Market ve azimuth projeksiyonları.
- Fizik: inverss, reaktivite N ve gradient dN /dh, -157 N/km [3] [4] [7] [11].
- Throat indeksi: Hepburn [1] ve F5LEN [2] haritaları, ortak ızgaralar ve iki kaynağın ortalamaları.
- Isobare: GFS [12], Open-Meteo [15] ile baskı, her 4 hPa, H ve L merkezleri.
- Spot, dinleme istasyonları (her zaman görünür pembe çizgilerle pembe çevreler) ve kırmızı kişisel örtüler.
- Büyüklüğü okuyun: yüksek basınç, yüksek hızlar, basınç düşüşü, SNR, [3] [4] [5] [13] [14] dağ rotaları.

## 1. Prensip: Bir tabakalı harita

Harita sabit bir görüntü değildir: program tarafından tasarlanmıştır, tabaka üzerinde tabaka, ne zaman zoom, projeksiyon, grup, tahmin zamanı veya tema değiştirirseniz. Her katmanın haritadaki üst Katman barında kutusu vardır, bu yüzden sadece ihtiyacınız olanı tutabilir ve tam resmi istediğinizde birlikte her şeyi görebilirsiniz. Sipariş, aşağıdan, bu:

|  #  | Road Road Road | Content Content Content | Casella |
|  1  | Temel harita haritası | Deniz, topraklar, kıyılar ve sınırları | Evet Evet Evet |
|  2  | Index tropo | Media Hepburn + F5LEN veya sadece bir kaynak; ayarlanabilir şeffaflık | Evet Evet Evet |
|  3  | Grill locator | Alanlar, meydanlar ve, zoom, meydanlar ile | Evet Evet Evet |
|  4  | Isobare | Her 4 hPa, merkezler H ve L | Evet Evet Evet |
|  4a  | Sıcaklık 850 hPa | Isoterm her şeyi tedavi etti 4 °C | Evet Evet Evet |
|  4b  | Termal inversiyon | Sudaki havanın zeminde olduğundan daha sıcak olduğu Orange örneği | Evet Evet Evet |
|  5  | PSKReporter | PSKReporter istasyonları (dark pembe çevreler) ve pembe, duydukları şey | Evet Evet Evet |
|  6  | Decoded Tarafından me | WSJT-X, JTDX veya MSHV ile istasyonlar: QTH'inizden Kırmızı hatları | Evet Evet Evet |
|  6a  | QSO desunti: canlı | Kapalı bağlantılar ( RR73, 73, RRR) iki istasyon arasındaki yeşil çizgi: | Evet Evet Evet |
|  7  | Spotlight | Bağlantılar: tam mavi çizgi, sarı hava için kaplı, iki top | Evet Evet Evet |
|  7a  | Exclusions | Casella 100 km'nin altında Excluded: En kısa trafik ne çizilir ne de listelenir. Casella 1000 km'den fazla: sadece kareler (başlangıç): Eğer istasyonlardan biri QTH'den 1000 km'den fazlaysa, çizgiler veya çevreler değil, sadece lilac'ta tedavi edilen istasyonların konumları 45°'de tedavi edildi | Evet Evet Evet |
|  8  | QTH ve etiketler | İstasyon, seçilmiş noktanın isimleri | Her zaman görünür görünür her zaman görünür görünür |

Tropo indeksi hemen her şeyden ötedir: renkli bir örnektir ve daha yüksek yer hatları ve sembolleri kapacaktır. Kafe bunu geçti, bu yüzden her zaman renkli bir alanın nereye düştüğünü biliyorsunuz. Isobares ince çizgilerdir ve örnekleme üzerinde iyi okunur.

## 2. Temel harita

Maliyetler, sınırlar ve topraklar doğal Dünya vektör verilerinden gelir, genel alan [9], genel görüş için 1:50 milyon ölçeklendirmek ve 1:10 milyon kez magnified. dosyalar program klasöründe: online harita hizmetleri yok. Temanın Renkleri: ışık mavi deniz # E2ECF8, karanlık mavi sahil, ışık gri sınırları. temalarda Black ve Night üssün renkleri karanlıktır, diğer temalarda biraz boyadılar.

## 3. İki projeksiyon

Tüm katmanlar aynı projeksiyon işlevinden geçiyor, bu yüzden her zaman birbirleriyle uyumludur. İki [10] projeksiyon arasında seçim yapıyorsunuz (uzunluk, {{ entitude, RRay of the Earth):

Pazar. Yerel köşeler ve şekiller; Mesafeler kutuplara doğru uzanır. ±85° entitude'a kesin.

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

Azimutale equidtanti meydanınızda merkeziydi. Merkez (TIS0,.0) WWL meydanınızın merkezidir. Merkezden ölçülen mesafeler ve yönler doğrudur: 1200 km'de bir istasyon ölçekde 1200 km ve kuzeye olan açı, antene işaret etmek için en azimuth. Kiracıyı değiştirirken harita refocuss.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

Azimutale'da kağıt üzerinde olan çizgiler düz (meridians, paraleller, kare kenarlar) eğri hale gelir. Bu nedenle tüm çizgiler kısa özelliklere yol açıyor, her bölüm hazırlandı ve sonra özellikler birleşiyor; aynısı tropo indeksinin kenarlarına ve isobare'ya da geçerlidir. Merkezin antipodes noktaları kesin bir pozisyona sahip değildir ve görüş dışında kalır.

## 4. Grill locator, zoom ve hareket

Maidenhead ızgara programı tarafından tasarlanmıştır: 20° × 10° alanları merkezdeki iki büyük ve silinmiş harfle, 2° × 1st kareleri ve 5 × 2,5 ile. Hatlar mavi ve ince, tropo indeksini kapsamaz.

Tüm görüşe geri dönmek için farenin altındaki nokta sabit kalır; + ve - anahtarlar ve tüm görüş için yüzde ile anahtar. Detaylar zoome uygundur: ince kıyılar, alt-squares, kalın isobare etiketleri.

## 5. Neden inverss dalgalar dalga geçiyor

Düşük atmosferde sıcaklık normalde 6,5 °C'ye kadar düşer; Refraktif indeks hem sıcaklık hem de [3] neziğe bağlıdır ve dalga eğimleri zemine keskin bir şekilde değiştirir ve normalden daha fazla gelir. En önemli tabaka yaklaşık 3 km'ye kadar, inverss, ducts ve convective hücreler [4] oluşturulur; deniz yolları iyileştirilir, çünkü su kütleleri [4] invers alanları oluşturur. [3]

Bunu ölçmek için kullanılan boyut refraktif N, i.e. (n - 1) · 106 [11]:

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

T, Kelvin'deki sıcaklık, P baskı ve buhar basıncı, hem hPa'da.

Çeşitli N'leri paylaşmakla (dN/dh). Standart atmosferde yaklaşık -39 N/km; -79 N/km'ye kadar, kırılmanın yanı sıra normal olarak kabul edilir ve sub-refraction [7] 'nun pozitif puanları ile. Aşağıda -157 N/km dalga, dünyanın eğriliğinden daha iyi değildir ve tuzağa düşer: [11] (dükleme) dük. Bunlar herburn ve F5LEN haritalarının tahmin etmeye çalıştığı koşullardır.

## 6. Tropo indeksi: iki haritadan ortalamaya

Hepburn ve F5LEN sekmeleri kontrol masasıdır: Her kaynağın sayfasından indirilen görüntüleri gösterir, zaman değişebilir ve efsanenin referans noktaları ve renklerine tıklayarak okumaya hizmet eder. Tropo indeks katmanı en az iki kalibreden biri aktif ve tamamlanmış olana kadar boş kalır.

Hepburn. Herburn indeksi, bir alanda ne kadar dalgaların katlanması beklendiğini, 0 ila 10 arasında lineer bir ölçek üzerinde de 10'u aşabilecektir. 0 standart atmosfer anlamına gelir ve sadece saçlar; 1-2 marjinal veya zayıf süper-refksiyon; 3-4 Süper giriş alanları ile etiketlenir; indeks saç tropoyu hesaba katmıyor ve [1] dağlarını görmüyor. Haritalar 6 gün kaplıyor: İlk 36 saat boyunca 3 saat geçiyor, o zaman 6 saat, her gün 18 UTC [1] 'ya güncellendi; başlangıç verileri (sıcak, nem, baskı) Kanada hava servisinin global modeli [1]. [1]

F5LEN. Avrupa F5LEN, 31 haritayı +3'ten +192 saate kadar yayınlar (her 3 saat içinde, her 6'ya kadar +96'ya kadar, o zaman her 12'ye kadar), en alt doğru ve bir sınıf efsanesinde geçerlidir: Disturb, Normal, Minor, Weak, Good, Güçlü, Vy Güçlü, Duct [2]. F5LEN 'nun dünya tahmini, Unit N'deki hava refraktif indeks tarafından hesaplanır, yaklaşık 5 gün boyunca (yaklaşık 5 gün), günde 4 kez güncellenir; haritada bir tıklama ortalama değeri ve [2] lokatörü gösterir.

Proceeding. Her tahmin için program bu adımları gerçekleştirir:

1. Discharge. İki tahminin görüntüleri, kaynağı güncellemek için bir kez kaynağı güncellemek için önbelleğe kaydedilir. Son versiyondan önce, izin herburn ve F5LEN için talep edilmelidir.
2. Otomatik georeferencing. Program çerçeveyi bulur (aynı renkte iki tane yatay ve iki dikey çizgi) ve Windows 10/11'de, OCR'nin sisteme entegre edilmiş axle etiketleri ile (54N, 10W, 0, 5E...). Çerçevenin çevresindeki şeritleri kes, onları enlar, N/S/E/W mektubunun ne zaman okuduğunu ve en çok etiketin düştüğü çizgiyi tutmadığını test edin: yanlış okumalar dışarıda kalır. Buradan, çerçevenin aşırı lat/lonunu ve projeksiyon türüni alabilirsiniz (regular ızgara veya Mercator). Aşırılar kendilerini kurtarır ve çerçeve değişiklikleri boyutunu yeniden hazırlar. Mac ve Linux'ta, OCR olmadan fabrika ekstremiteleri veya el yazısı olanlar sekmede geçerlidir. Alternatif olarak, yasal olmayan haritalar için: Her tür harita (Hepburn Europa, F5LEN dünya çapında...) bir kalibrasyonu vardır: bilinen koordinatlarla bazı kontrol noktaları (benimle paraleller, tanınamaz kaburgalar) bu kaynak tarafından kullanılan projeksiyonda pikselleri bağlar. Kalibrasyon, programa bir sonraki bir dosyada ve bir kaynak değişikliği formatını yeniden yapabilirsiniz.
3. Renk okuması. Her piksel kaynak efsanesine kıyasla ve en yakın sayısal değere dönüştürülür. Orijinal haritada çekilen masraflar, makaleler ve ızgaralar “hayır veri” olarak kabul edilir ve yakındaki değerlerle dolu.
4. Dünya ızgara. Her resim, çerçevenin bulunduğu ve aşırılarını bildiğinde, Dünya'ya yerleştirilir; dünyadaki herhangi bir noktada değer, onu kaplayan görüntülerden elde edilir. Daha fazla bölge aynı noktayı kapsadığında, zamanınızın bu seçimden 1.5 h olması gerekir; o zaman en yakın saat sonra en son yarış. Hiçbir görüntü haritanın gri veil olduğunu gösteriyor: “index 0”den farklı olan hiçbir veri yok. çerçevede, bir efsane rengi olmadan bir piksel 0 (normal atmosfer); kıyılar, sınırlar ve makaleler 3 piksel içinde en yakın renkte bakarak atılır.
5. Aynı merdiven. Hepburn zaten 0-10 [1] ölçeğindedir: karanlık arka plan = 0 ve on renk sipariş (violet, mavi, su yeşili, lime, sarı, ocher, portakal, kırmızı, pembe, ışık pembe) 1 için. F5LEN sınıfları kullanır (Disturb, Normal, Minor, Weak, Good, Güçlü, Vy Güçlü, Duct) [2], ancak bazıları tam olarak Hepburn renklere sahiptir: Weak = 1, Güçlü = 5, Vy Güçlü = 7, Duct = 8; Minor 0,5 ve İyi (yeşil) 3.5. F5LEN sekmede ölçek çok basit, herburn ile karşılaştırılabilir hale getirmek için seti düzeltin.
6. Aynı zaman. Zamanbar, 3 saat boyunca +0'dan +192 h'ye gider. Her bir adım için, tahminin en yakın zamanda geçerli olduğunu tahmin edin; Bir kaynağın bu adıma sahip değilse, medya sadece diğer ve efsaneyi kullanır.
7. Medya. Media düğmesi ile her nokta değer (Hepburn + F5LEN) / 2; Hepburn veya F5LEN ile sadece bir kaynağı görüyorsunuz, iki tahminin aynı fikirde olmadığını anlamak için kullanışlı.
8. Çizim. Değer efsane ölçeği (0 mavi, 2 yeşil, 4 sarı, 6 portakal, 8 kırmızı, 10 mor) ile renklenir ve ızgara altında bir örnek olarak projelenir. 0 şeffafdır, bu yüzden hiçbir şey temiz temel haritayı gördüğünüz yerde; genel şeffaflık kutunun yanındaki eğrilerle ayarlanır.

spot sekmede, rotadaki indeks, iki istasyon arasındaki maksimum dairedeki her 10 km boyunca grid değer şampiyonlarının ortalamalarıdır.

## 7. Isobare

Yüksek stabil baskılar [3] trope için aranan ilk sinyaldir ve IK3XTV analizinin ötesindeki bağlantılar da yüksek [4] basıncı ile olmuştur.

1. Data. NOAA [12] global model GFS'den deniz seviyesinde baskı, 3° × 3°'lik bir ağ üzerinde ücretsiz Open-Meteo [15] servisi aracılığıyla okuyun (30°'den 72°'ye kadar) N, 30° W'den 48° E'ye, sonraki 78 saat boyunca. Program başlangıçtaki verileri güncelliyor ve sonra her 6 saat, model yarış gibi.
2. Hatlar. The isobare her 4 hPa (...1012, 1016, 1020...) yürüyüş meydanlarının algoritması ile elde edilir (belirli kareler), sonra yol tarafından çizilir ve projelenirler.
3. Merkezler. En belirgin yerel maksimumlar ve minimumlar H (mavi) ve L (red) ile işaretlenir ve hPa'daki değer.
4. Etiketler. Değer her isobara boyunca görünür, örneklemenin üzerinde okunabilir kalmak için haritanın renkli geçmişi.

Alan ve adım. Veriler aradığınız alanda indirilir,% 25 oranında genişletilir, programdan en yoğun reload'a bir adımla 400 puan altında kalmayı tercih eder (Open-Meteo her noktada bir istek olarak sayır: dakikada 600, saatte 5000): alanın haritasını genişleterek veya hareket ettirerek, programın en yoğun reload'u, en fazla 10 dakika içinde döndürür; bir güncelleme başarısız olursa önceki verileri kalır ve 3 dakika sonra tekrar deneyecektir. Bu şekilde isobare ve sıcaklıklar dünyada her yerde çalışır.

Sıcaklıklar. Aynı istek, 2 m'ye, 925 hPa'ya ve 850 hPa'ya sıcaklık getiriyor. LA0BY, UA6 'ye 22.11.8'in X1Q'ye olan etkinliğinin sıcaklık haritalarında (900-3000 m), ancak Hepburn [3] tahminlerinde değil; DF5AI ve DK5YA, [5] 'nun X5Q'u tanıması için dikey sıcaklık profillerini kullanıyor. Bunun için iki tabaka vardır:

- Sıcaklık 850 hPa: her 4 °C'ye (aşağıdaki sıfır, maviye, sıfırda mor).
- Termal inversiyon: 925 veya 850 hPa'daki havanın 2 m'de olduğundan daha sıcak olduğu portakal örneği; daha yoğun, daha inversiyon güçlü. Dalgaları zemin [3] [11] 'ya yönlendiren fiziksel durumdur ve tropo indeksiyle karşılaştırır ve tahminlerin meteorolojik bir üssü olup olmadığını gösterir.

## 8. Spotlar ve istasyonlar

Spot. Her iki bilinen pozisyonla her nokta iki istasyon arasındaki en yüksek çember üzerinde bir çizgidir, bölümlere bölünmüş ve projelenmiş; Mercatore'de o zaman biraz eğri, azimutale'da, merkezden başlarsa. Normal nokta için dolu mavi çizgi, havalarcatter için karanlık kenarla kaplıdır (<AS>, <ACS> veya "airscatter" yorumda), her istasyon için bir top, örnekleme çizgisi altında ışık yarık. Seçilmiş nokta, son ve iki isim ile daha sık tasarlanmıştır.

Dinleyin. Her PSKReporter [16] istasyon almak karanlık bir pembe boş dairedir. Her biri kodlanmış olan istasyonlara tam pembe çizgilerle başlar ve ağa geri gönderilir, her zaman tıklamadan görünür, isimleri her zaman yazılır. Bir çemberi seçerek hatları kalın. Spot ve dinlemek sadece listede seçilen grubu gösterir: haritayı değiştirmek boş ve yeni grup için zaten toplanan verilerle yeniden tasarlanmıştır.

Bana karar verdi. WSJT-X, JTDX veya MSHV ile kodlanan her istasyon, QTH'den istasyona kadar dolu kırmızı bir çizgidir. Tahminlerle en doğrudan karşılaştırma: alıcınızın şu anda nereden geldiğini gösteriyor.

## 8 bis. Renkler ve semboller

Her renk tek bir anlamı vardır, tüm temalarda eşit (sadece arka plan değiştirmek).

| Renk ve işaret | Anlam |
| Sürekli mavi çizgi | DX kümesinden Spot, RBN veya DM4X iki istasyon arasında |
| karanlık kenar ile sarı çizgi | Havalarcatter yorumu ile nokta ( uçaktaki yansıma) |
| pembe boş çember | PSKReporter (MQTT) üzerinde dinlemek |
| Banyodan ince pembe çizgiler, altta pembe bir top ile | Dinleyicinin duyduğu İstasyonlar her zaman yazılır; seçilen dinleyiciler için kalın çizgiler |
| Kırmızı çizgi ve kırmızı isim | UDP'niz (WSJT-X, JTDX, MSHV): Kimsiniz |
| Red line flaş | QSO bu istasyonla ilerliyor: Sizi ve kapanışı henüz gelmedi ya da etkinleştirilen iletimle WSJT-X/JTDX’in DX Çağrısı. RR73, RR veya 73 ile devam et |
| Yeşil çizgi | QSO desunti: RR73, RR veya 73 UDP görüşü |
| Beyaz sınırla siyah çizgi | Birisi sizi çağırıyor: RR73/RRR /73 veya 3 dakika kadar sürüyor |
| 45° | Seçilmiş taç ötesinde en az bir istasyonla trafik: Hayır hatları, sadece meydans locator |
| kuzeye mesafe olan lillas için arama | QTH civarında 1000, 700 ve 500 km; bu seçim daha belirgindir |
| Çember ve nokta temanın rengi | Senin QTH, adı ve ev sahibi |
| GRAVES 143 ile portakal topu,050 | Radar GRAVES ( JN27SI) |
| maviden kırmızıya Örnek | Hepburn trope indeksi (0-10) veya F5LEN sınıfı, veya ortalamaları; daha sıcak renk, daha iyi koşullar |
| Işık gri velvet | Seçilen süre için tropo verileri olmayan alan |
| Grey-mavi hatları hPa'daki değerle | Her 4 hPa; mavi yüksek basınçta, L kırmızı düşük basınçta |
| Red, mavi ve mor çizgiler | Diğermia her 4 °C'ye 850 hPa: sıfırın üzerinde kırmızı, sıfırın altında mavi, mor the zero |
| Örnek portakal | Termal geri dönüş: zeminden daha sıcak 925'e hava, daha güçlü inversiyon ile daha yoğun. |

Suspected Reports the edge of each tab says the rule in force: green Good, red Scarta, gri no rule.

lilac meydanları 45°'de yok edilir. QTH ilgilerinizden uzak trafik, propagasyonun açık olduğunu anlamanızı sağlar, ancak hatları, yüzlerce kilometre uzunluğunda ve çoğu zaman onlarca, haritayı kaplayın ve sizin için neyin önemli olduğunu gizleyin: istasyondan kullanabileceğiniz açılışlar. Bu nedenle, seçilmiş taç ötesinde, program çizgileri çizmiyor, ancak locator meydanları (2° × 1°) dahil istasyonların renk. Sonuç, «aktif bölgelerin” bir haritasıdır: Yakındaki birçok lilac meydanının ortaya çıktığı yerde, bir konduit veya bir açılış var, doğrudan sizi endişelendirmezse bile; eğer bölge size karşı genişliyorsa, bu yönde anten işaret ediyor. 45° pano, tropo indeksinin ve ızgaranın altında görmenizi sağlar, bu yüzden tahmini (düşün rengi) ve gerçek trafik (not). QTH meydanınız asla renkli değil, çünkü başlangıç noktası ve uzak bir alan değil. Sadece tüm taçların ötesindeki meydanlar renklidir: taç tarafından geçen bir meydan, ya da içeride, iki istasyondan biri olsa bile özgür kalır. hatları görmek için, sadece kutuyu kapat veya daha büyük bir taç seçin.

Haritada ne olduğuna karar veren kriterler ve hangi kartpostallar 1 numaralı belgede bir tane tarafından açıklanmaktadır.

## 9. Zamanbar

Haritanın altında: Gösterilen tahminin tarihi ve zamanı, 6 saat içinde +0'dan +72 h'ye kadar, index efsanesi 0-10 seçilen kaynağın adı ve model ırkının zamanı. Trumpet ve isobare indeksi eğrileri takip eder; noktalar, istasyonlar dinlemeniz ve tebrikleriniz şu anda gözlemler ve her zaman bu “adex” olarak kalır. cursor +0'da olmadığı zaman, tahmin süresine ait olmadıklarını hatırlamak.

## 10. Nasıl okunacak

- Yüksek basınç + yüksek indeks + renkli alan boyunca nokta: açılış orada ve planlanan biridir. Bu yönde arama zamanı.
- Yüksek indeks ama bir nokta yok: veya kimse orada aktif değildir veya tahmin çok iyimserdir. Dinleme katmanı ayırt etmeye yardımcı olur: bölgede alıcılar varsa ve hiçbir şey hissetmez, ikinci hipotez daha muhtemel.
- Renkli alanların dışında uzun noktalar: yüksek hızlar, yüzey tahminleri LA- UA6 [3] etkinliğinde olduğu gibi, veya başka bir yayılım filtreden kaçtı (örneğin, etiket olmadan). DF5AI ve DK5YA ayrı tropo ve E sporadic da saatten ayrılır: Direnilen ve diffüz E sporadik ile birlikte sona eren açılışlar zordur [5] tropo.
- Hepburn ve F5LEN aynı fikirde değil: iki kaynağı düğmelerle karşılaştırın ve gerçek noktaların nereye düştüğünü görün.
- Baskı aşağıya: G4MCU, baskının düşmesine kadar SK6VHF beacon'da bir açılış izledi; 1012 hPa, son olay [13]. Zaman çubuğunu kaydırarak isobare izleyin: yüksek baskı çekilirse, açılış muhtemelen takip eder.
- Devam eden Signals: KP4MD 'nun 144 MHz'deki WSPR çalışmasında, sinyal / noise oranı, herburn'un bir gelişme tahmin ettiği pencerelerde doğru büyüdü ve tahmin [14] 'yu aldığında düştü. Aynı yolda büyürse, açılış oluşur.
- Dağ yolları: indeks [1] rahatlamalarını dikkate almaz, ancak Alplerin ötesindeki bağlantılar da atmosferik yerçekimi [4] ve S5 'nun atmosferini tahmin etmeden 9A ve X3Q ile birlikte çalışır. LA0BY [3]

## 11. Açık nokta

- Herburn ve F5LEN 'nun programdaki haritalarını kullanmasına izin verin.
- Dönüşüm katsayısı F5LEN → ölçek 0-10.
- Herhangi bir haritanın kontrol noktalarının Kalibrasyonu, kaynakları değiştirirse yeniden yapılır.
- GFS'den sıcaklık ile ilgili rota (850 hPa)

## Ana kaynaklar

1. W. R. Hepburn, DX Info Centre, "The Hepburn Tropo Index (HTI)" ve "Tropospheric Ducting Tahminleri" sayfaları (dxinfocentre.com). Index tanımı ve ölçek, negatif değerler ve 10'dan fazla, saç ve rahatlamaların dışlanması, 6 günde 3 ve 6 saat adım adım atılması, 18 UTC'ye yükseltme. Global Kanada modelinden verilerin kökeni: G. Meisse, AMSAT-BB, 27/08/2007.
2. F5LEN, Avrupa için tahmin (tropo.f5len.org/forecasts-for-Avrupa) ve VHF tropopheric propagation (tropo.f5len.org/WWWWW) ve " VHF tropospheric propagation tahmin aracının yeni versiyonunu" belirtti. Unit index N, günde 3 saat, 4 güncelleştirme, tıklama başına değer ve taşıyıcı.
3. S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", 41° Nordic VUSHF Meeting, Skjeberg 2019. Normal gradient 6.5 °C/km ve inversiyonlar 10-15 °C /km, sıcaklık ve nem rolü, yüksek stabil baskılar, LA- UA6 olayı yüksek sıcaklıkta görünür ve her yanıda değil, LA-9A / S5 bağlantıları dağların ötesinde.
4. F. Egano IK3XTV, “Propagation VHF – Tropo propagation’daki konvektif hücrelerin ve atmosferik çekim dalgaların katkısı.” İlk 3 km'nin önemi, deniz yolları, konvektif hücreler, Alplerdeki yerçekimi dalgaları, yüksek basınç bağlantıları, 3-4 km'ye kadar yaygın hacim.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, "Very Long Distance Propagation in the 144 MHz Band", 7/24/2003. Sıcaklık ve dew noktasının diversiyonlarını tanımak, Hepburn indeksinin kullanımı, tropo ve E sporadic arasındaki fark.
6. J. N. Gannaway G3YGF, "Tropospheric Scatter Propagation", QST Kasım 1983. Dünya Yarısı indeks ve nokta kartı için kullanılan yol kaybı (Belir 1).
7. VHF/UHF DX Kitabı, bölüm 2 "Tropospheric propagation", pp. 2-23...2-30. DN /dh'e göre refraktif bölgelerin Şekilleri (standart -39 N/km, limitler -79 N/km ve 0) ve yüzey ve yüksek profiller. Elde edilen kopyanın metni okunamaz: sadece rakamlar kullanılır.
8. Programın yazarı tarafından sağlanan 8/11/2026 proje özeti. Harita renkleri, gerekli projeksiyonlar, tabakalar, zamanbar.
9. Doğal Dünya, kıyıların ve sınırların vektör verileri, kamusal alan (doğal çevredata.com). Genel referans, eklenmedi.
10. J. P. Snyder, "Map Projections - A Working Manual", USGS Professional Paper 1395, 1987. Mercator projeksiyonları ve azimutale equidtanti. Genel referans, eklenmedi.
11. ITU-R P.453, radyo refraktif indeks. Refraktif formül N ve duct eşi -157 N /km. Genel referans, eklenmedi.
12. NOAA NCEP, Global Tahmin Sistemi (GFS). Günde dört yarış. Genel referans, eklenmedi.
13. G4MCU, “tropospheric” (g4mcu.net/tag/tropospheric), 2018-2025. Referasyon, konduit ve saç; yüksek basınçla daha sık girişler, özellikle de denizde; beacon SK6VHF 1012 hPa (Haziran 2019).
14. C. F. Milazzo KP4MD, "VHF/UHF WSPR Propagation Study" (qsl.net/kp4md/144_mhz_wspr.htm), 2012-2014. WSPR'nin SNR, Hepburn tarafından tahmin edilen ve tahminin ortadan kalktığında meydana gelen gelişme pencerelerinde büyüyen 144 MHz'e rapor veriyor.
15. Open-Meteo, hava tahmin API (open-meteo.com), ücretsiz rakip olmayan kullanım. GFS modelinin deniz seviyesinde tek bir istekte birden fazla noktada baskı.
16. mqt.pskreporter.info, Pskreporter'in ticarilerinin gerçek zamanında dağıtım MQTT. Bilgi: Alıcıların adaylığı ve ev sahibi (Belirli 1).
