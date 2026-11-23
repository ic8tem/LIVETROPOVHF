LIVETROPOVHF · teknik belge 1
# Programın işleyişi

> İtalyancadan makine çevirisi: şüphe durumunda İtalyanca kılavuz geçerlidir.

Programın yaptığı şey, verileri nereye aldığı ve haritada ve listede gördüğünüz şeylere nasıl dönüştüğü. 9 Kasım 2026, C# projesi ile ilgili (.NET 10 + Avalonia).

## Özet

- Amaç: tropo tahminleri (Hepburn [3], F5LEN [4], isobare GFS [18]) ve LA0BY [1] kontrol listesine göre grupların gerçek görüntülenmesi.
- Data collection: Two DX kümesi, Ters Beacon Network [9], dm4x küme ve PSKReporter [5] MQTT akışı, UDP WSJT-X, JTDX ve MSHV [8].
- Spot: Grup segmentleri, negatif filtre, havalarcatter in dashed sarı, locators veya CALL3.TXT [6] [7].
- Kim dinliyor: pembe çizgilerle karanlık pembe çevreler her zaman istasyonlara görünür [5].
- Spot kartı: mesafe, azimut, tropo indeksi yolda ve G3YGF [2] yöntemiyle tropo kaybı.
- Diller Hepburn ve F5LEN: [3] görüntü ve kalibrasyon tezgah [4].
- Watch: 10 sunucuda her 3 dakika [10] [11].
- Suspected raporları: Q65/JT65 'da EME, Ay'ın pozisyonuyla tanınan, truncated locators, skimmers, quarantine ve dinleyiciler, güneş [20] tarafından öğrenilen kurallarla.
- İki tabloda özetlenen renklerin discard ve anlamı (bölüm 11 ter ve 11 kuter).
- Temalar, 36 [17] diller, [16], taşınabilir program ve çoklu platform [12] [13] [14] [15].

## 1. Amaç

Program genellikle farklı sitelere bakmakta olan tek bir pencerede bir araya gelir: tropofer propagasyon (Tropo index of Hepburn and F5LEN, isobare) ve gerçekten ne olduğunu gözlemleyin (Break küme, Ters Ağ ve PSKReporter'den gelen istasyonlar, artı dinleyen istasyonların listesi). Referans grubu 144 MHz; Ticariler 50 MHz'den toplanıyor.

Set, LA0BY tarafından aşırı tropo olayları için önerilen kontrol listesini takip eder: önceki günlerde Hepburn ve F5LEN tahminlerine ve yükseklikteki sıcaklık profillerine bakarsınız, olay şu anda DX kümesine ve spot haritaları üzerinde arama yaparken, açılış [1] 'ya başlıyor olduğuna dair kanıtlara bakın. LA0BY, sadece Hepburn / F5LEN haritalarının en uzun bağlantıları tahmin etmek için yeterli olmadığını da gözlemliyor. [1]

## 2. Pencere ve sekmeler

Pencere bir tane ve mobil pencerelere bölünmüyor. En üstten alta:

- Genel ayarlar satır: Soldaki Call ve Locator; Sağdaki UTC saati (e.g. 08/11/2026 20:33:38 UTC, amber text) takip ettiği gibi:Tamam veya NTP:FAILED, tema menü ve dili.
- Beş grupta sekme sayısı: Harita ve nokta; Tahminler (Propria LR, program tarafından hesaplanan ve Tahmin El Kitabı'nda tarif edilen tahminler, o zaman Hepburn ve F5LEN); GRAVES; Data ve kaynaklar (DX Beacon küme, PSKReporter, Ters, UDP, Randevular ve şüpheli Raporlar); Rehberler (Program Operasyon, Harita Binası, tropo'yu nasıl şekillendirir, operatöre ipuçları, bu metinde hangi kılavuz görünen bir kutu ile). Bir barın altında grubun alt dillerini gösterir; her grup en son kullanılanı hatırlar.
- sekmenin içeriği ve altta, kaynakların durumu ile durum barı, isimlerin veritabanı ve son güncellemenin zamanı.

En üst hat ayarları tüm sekmelere uygulanır: seçilen tema aynı zamanda veri toplama ve belgelerin arka planını renklendirir; Doküman sayfaları bir sayfaya kadar beyaz kalır, bu yüzden de yazdırırlar.

## 3. Çağrı ve ev sahibi

Maidenhead ev sahibi 4 veya 6 karaktere sahip olabilir (örneğin JN70 veya JN70CN). Program onu karenin merkezine dönüştürür: 20° × 10° alanı, 2° × 1st kare, 5' × 2,5'in alt alanına dönüştürür. Eğer metin geçerli bir taşıyıcı değilse kutunun kenarı kırmızı olur ve istasyon haritada görünmüyor. Kiracı aynı zamanda azimuth projeksiyon merkezi olarak ve reklam kurulunda mesafeler ve azimuth için başlangıç noktası olarak hizmet vermektedir.

Çağrı bir küme girişi olarak kullanılan isimdir. İlk başta, geçerli bir aday olana kadar, kümeler bağlantı kurmuyor: bir pencere ismi (ve isteğe bağlı olarak, ev sahibi); doğrulanmış, adı boş ve kümeleri olan her kümede bir kullanıcı olarak kopyalanır. “Later” hala kalır ve her kümenin Connect düğmesi tekrar aday gerektirir. MQTT, UDP ve NTP giriş kullanmıyor ve yine de ayrılıyor.

## 4. Veri toplama: kaynaklar

Her kaynağın Data Collection sekmesinde kendi bölümü vardır, Connected/Dis bağlantılı, başlangıç, statü, metre ve alınan sıraların kaydı. Filtreden gelen dağınık veya dağınık çizgiler kayıtta görünmüyor: sadece bölüm metre say. bölümler iki ile iki ile yan yana.

| Kaynak Kaynağı | Bağlantı Bağlantısı | Ne getiriyor |
| 1. DX küme seçimi | 0dxk.dyndns.org:8000 komut set/filter dxbm/pass 6 | amatör radyoların yanı sıra SSB ve Zheng. ON0DXK 'da VHF filtrelenir ve belirtilen komut ile etkinleştirilir (poi 4, 2...). |
| 2. İkinci kaynak | dxmaps.com:7300 (DXMAPS, EA6VQ) ve s50clx.si:41112 ( S50CLX, Slovenya) | DXMAPS VHF puanlarını toplar ve ayrıca RBN kayak noktaları verir (set/skimmer kontrolü, zaten set); S50CLX bir DX Spider kümesidir. Adınızla giriş; düzenlenebilir adresler. |
| 3. Ters Beacon Network | telnet.reversebeacon.net 7000 CW/RTTY · 7001 FT8 | Otomatik kayak nokta. Yalnızca [9] ile giriş. |
| 4. PSKReporter ile küme | dm4x.ddns.net:8500 | PSKReporter'in küme formatındaki dijital noktaları. |
| 5. PSKReporter MQTT | mqt.pskreporter.info 1883 · TLS ile 1884 | Her gerçek zamanlı resepsiyon raporu: kim gönderiyor, kim dinliyor ve [5] Locators. Bands 144, 222 ve 432 MHz. |
| 6. Hava ve tropo haritaları | dxinfocentre.com/tropo_eur.html tropo.f5len.org/forecasts-For-Avrupa Model GFS | Hepburn [3] ve F5LEN [4] tropo indeks haritaları, isobare için deniz seviyesi basıncı (GFS via Open-Meteo [18]). Hepburn ve F5LEN yazarlarına izin verilmelidir. |

Cluster telnet (kaynaks 1–4). Program adı isteği için beklemeden sonra ( giriş içeren metin, çağrı: çağrısign veya çağrı; 4 saniye içinde gelmezse lütfen giriş gönderin. Sonra bölümün komutlarını gönderin, noktadan ve komünden ayrı, her 0.6 saniye. Telnet kontrol karakterleri hatları okumadan önce kaldırıldı. Bağlantı düşüyorsa ve Riconnetti aktifse, 10 saniye sonra tekrar deneyin (15 eğer sunucu yanıt vermezse) [6].

PSKReporter MQTT (kaynak 5). Program konu pskr/filter/v2/{banda}/{modo}/{tx_call}/{tx_call}/{tx_grid}/{rx_dxcc}/{tx_dx_dxcc}/{rx_dx_dxcc} Sadece grubu düzelterek ve diğer alanları karakter + [5] ile ücretsiz bırakarak. Sunucunun grubun işaret ettiği tam isim (örneğin 2m, 1.25m, 70cm) ilk bağlantıda kontrol edilmelidir.

## 5. Set çizgisinden noktaya

One line is a spot when it has the form DX de SPOTTER: Frekans DX yorumu hhmmZ. Program spotter, DX istasyonu, kHz'de frekans, yorum ve şimdi UTC; isimler kayakçı su eklerini alır (-#, -1...). Sonra, bu sırayla, aşağıdaki kurallar [6].

| Band | Segment kabul edildi |
| GRAVES (14 ile) | 143,040 - 143,060 MHz |
| 144 MHz | 144 – 148 MHz |
| 222 MHz | 220 - 225 MHz (sadece Bölge 2 ITU, America) |
| 432 MHz | 420 – 450 MHz |
| 1296 MHz | 1240 - 1300 MHz |
| 2320 MHz | 2300 - 2450 MHz |
| 10 GHz | 10,5 GHz |

1. Band. Frekans, yönetilen segmentlerden birine girmelidir; tüm geri kalanlar ve özellikle 49 990,0 kHz altında her ticari gösteriliyor ve “grup dışı” olarak sayılıyor.
2. Yol. Yorumlamanın en iyi bilinen yollarından biri FT8, FT4, Q65, JT65, ISCAT, CT, SSB, FM, RTTY, PSK31, MSK144, FSK441, JT6M; USB ve LSB SSB oldu.
3. Olumsuz filtre. Nokta, MSK144, FSK441, JT6M, JTMS, ISCAT veya etiketler <AUE> <ES> <MS> <FAI> < F2 >, hassas olmayan üst köşeler ve hatta boşluklar içinde. Bunlar tropo olmayan yollar ve yönlendirmeler: Scatter meteor, aurora ve sporadic, F2.
4. Airscatter. Eğer yorum <AS> veya <ACS> (aynı zamanda karışık, e.g. <As>) içeriyorsa veya yerde havalarcatter (airscatter, hava saçı, uçak saçı), nokta kalır ama sarı renkte okunabilir bir çizgide çizilir (daha net haritada okunabilir).
5. İstasyonların yeri. Eğer yorumda iki taşıyıcı vardır (e.g. JN54 < JN83) ilk olarak istasyondaki yeri ve istasyonun ikincisini yapanlarındır; bir locator ile, bu da DX istasyonundan. Eksik olan şey CALL3 'da aramalıdır. TXT, sadece tam adı ile (aşağıdaki dizüstü bilgisayarlar için). Bir istasyon bir pozisyon olmadan kalırsa, nokta listede “gelen pozisyon” notuyla görünür, ancak haritada değil.
6. Çiftler. Aynı DX ile bir ticari, aynı nokta, aynı grup ve bir kişinin aynı kaynağı zaten bunu değiştirir, ekleme yerine.

CALL3.TXT, JTDX tarafından kullanılan isim veritabanıdır, CALL,LOCAT formatında,. Proje dosyası 1 048 847 ismi içeriyor, tüm geçerli lokatörü ile; Dosyanın başlığı hala 34 323 isimden bahsediyor çünkü ilk sürüm [7]. Başlangıçta arka planda yüklenir, bu yüzden pencere hemen kullanılabilir. Dosya, programa bir sonraki olarak, Data klasöründe, programın başladığı ve yukarıda bir veya iki klasörün bulunduğu klasörde; Eğer bir pencere, başlangıçta ve durumu bar sinyalleri kırmızıda söylediğinde bunu söyler. Onun adı her zaman 6character kullanır. Sadece taşıyıcılar için kural: Eğer bir tanesi (spot, MQTT, decoding, QSO desunti) 6 karakterden daha az ve CALL3. TXT'in 6'ya sahip olması, her zaman CALL3.TXT'e değerdir; dizüstü bilgisayarlar için (F/ IC8TEM, IC8TEM/P) sadece tam bir sestir: olmadan istasyon bir ev sahibi olmadan kalır ve başka yerde tasarlanmamıştır. Kural ayrıca çizim zamanında da geçerlidir, bu yüzden veri veritabanı yüklenmeden önce geldi.

Bir veritabanı: CALL3.TXT. 1.3.4'ten itibaren tüm taşıyıcılar tek bir dosyada, CALL3.TXT, bu da JTDX ve WSJT-X (CALL,LOCATUVE,, WSJT-X'te olduğu gibi kullanılabilir. Ayrı bir dosya yok. Dosyanın üst kısmında, başlıktan sonra, bir yorum hattı tarafından işaret edilen iki blok vardır: // [LIVETROPOVHF-MANUALI] el ve // [LIVETROPOVHF-CLUSTER] tarafından kodlanan diğer tüm öğelerle birlikte. Her aday bir kez görünür. Sublingual Data ve Sources › Randevular (CALL3) Bir isim ararsınız, ev sahibini yazın ve basın Add / değişikliği: ses, el tarafından sesin blokunda gider ve her şeyden önemlisi, istasyonun kendisinin gönderdiği varsayar. CALL3.TXT'dan adaylığı ortadan kaldırır (bir sonraki güncelleme bunu kaynaklardan geri döndürürse). Bir dosyayı ekleyin... bir liste CALL, LOCAT (aynı zamanda noktayı ve komünü, sekmeyi ve alanı kabul edersiniz) ve el seçilmiş sesler arasında yaz; seçilen dosya kopyalanmaz. Reload, CALL3.TXT from the disk, after editing it with an editör. Tüm kaynaklar: kendi taşıyıcı, el sesleri, küme düzeltmeleri, CALL3 'nun geri kalanı. TXT (kaynaklar, canlı taşıyıcılar ve G7VRD), sadece 6 karaktere sahip olup LL veya MM ile bitmezse veritabanı üzerinde geçerlidir.)

CALL3 'nun Güncellemesi.TXT. Aynı sub-dilde Güncelleme düğmesi şimdi kaynakları indiriyor (CALL3.TXT of WSJT-X Improv, CALL3.TXT EME of W7GJ, grid_data.bin of JTDX), onlara katılın (en azından 50.000 isim olduğunu kontrol edin) CALL3.TXT EME of, grid_data.bin of JTDX), CX'i kurtarır.bin of JTDX) TXT Önceki CALL3. TXT girişler kalır ve 6character locator bir kaynağın aynı karenin sadece 4'ü olduğu kalır. Setten gelen sesler ve düzeltmeler en üstte kalır, değişmeden kalır. Güncelleme kutusu ile her gün kendi başına, çek başlangıçta gerçekleşir. Program CALL3 'yu yeniden yazmaktadır. Her sesten hemen sonra el ve, her 10 dakikada ve kapanışta, CALL3. Paketin TXT sadece okunur ve özellik kendini alır. Linux yükleyicileri CALL3 'u aşırılaştırmaz. TXT zaten mevcut; Windows için sadece eksikse. ALL3

Setten otomatik düzeltmeler. DM4X ve RBN otomatik noktalarında, örneğin KN09tp<> KN09: ilk olarak, 6 karaktere işaret eden istasyondur; ikincisi istasyonda sadece 4. İlkin 6 karakteri varsa (ve LL veya MM ile bitmiyor) ve veritabanından farklıysa, program doğruları CALL3. Sadece TXT: düzeltme blok // [LIVETROPOVHF-CLUSTER], üçüncü alanda son onayın tarihi ile, kaynakların üzerinde (el seslerin altında) geçerli ve şüpheli sinyaller arasında "CALL3 " olarak güncellendi. Bu yüzden CALL3 'u hayatta kalır. TXT güncelleştirmeler. İki yıl boyunca onaylanmamış bir düzeltme öncekilüğü kaybeder ve normal bir ses olarak kalır.

Son kaynak: G7VRD. Bir aday CALL3 'da 6character yoksa. TXT, program G7VRD 'nun halka açık API'si (api.g7vrd.co.uk, "locator by callsign") otomatik olarak ağ trafiğinden toplamaktadır. Sadece meydanın merkezi olmayan bir 6character'i kabul edersiniz (LL veya MM); istekler gruplandırılmıştır, en fazla 40 nominatives per minute ve her bir nominative günde bir kez sorar, çünkü garantisiz bir hizmettir. Kiracı CALL3 'ya girer. TXT eğer isim orada değilse veya aynı karenin sadece 4 karakteri varsa; arama altta kutu ile çıkıyor.

Barla Randevular. PSKReporter MQTT konularında, bar seviyeleri ayırır, sonra bileşik isimler bar yerine bir noktaya gelir ( IV3. HB9CAT). Program barı geri getiriyor ve mesaj tam isimleri içeriyorsa, bunları kullanın: IV3/HB9CAT haritada IV3/HB9CAT kalır, liste ve veritabanı.

Locators canlı öğrendi. CALL3. TXT her şeyi içermiyor: örneğin SP9EMF ve SP9NVY orada değil ve sonra PSKReporter tarafından gönderilen 4-character locator kalır (kart bunu söylüyor). Bu vakaları azaltmak için program CALL3 'da yazıyor. Herhangi bir kaynaktan gelen her 6character (PSKReporter, verici veya alıcı olarak, küme, UDP decoding) CALL3 'nun adaylığı için. TXT aynı meydanda sadece 4 karaktere sahip değildir; asla 6 karaktere bir ses değiştirmez. 1.3.3'e kadar bu taşıyıcılar Data/ LOC6.TXT'daydı.

1.3.3'ten geçiş. 1.3.4 önceki sürümlerden ayrı dosyaların (Dati/CALL3 -MANUALE.TXT, Data/CALL3 -AGGIUNTE, Data/CALL3 -CLUSTER. TXT, Data/ LOC6.TXT ve Data/CALL3 - G7VRD.TXT) CALL3. Aynı girişlerle TXT. Yeniden kiralama yazısı, eski dosyalar.fuso uzantısını alır, artık okumazsınız ve silebilirsiniz.

Otomatik alıcılar. RBN, dm4x ve PSKReporter genellikle alıcıyı gösteren bir ekle isimleri rapor eder: -SDR, /SDR, - # (skimmer RBN) veya -1, -2... (Aynı istasyondaki alıcılar). Ev sahibi için ek kaldırıldı: OK2OZL -SDR, istasyon zaten 6character locator göndermezse OK2OZL JN89UE 'u CALL3 'dan alır. Gösterilen isim, suffix ile kalır.

Locators kesti. Tüm kaynaklar ev sahibilerinin uzatmasını tedavi etmiyor: birçok kişi 4 karaktere ayarlanıyor ve bazı otomatik taşıma sistemleri kasıtlı olarak 4character locator, hatta verileri FT8, FT4, Q65 ve JT65 'nun gerçek dinlendiricisinden elde edenler bile, karenin merkezi (LL veya MM, e.g. JN89MM) ile tamamlıyorlar. Bu nedenle, LL veya MM ile sona eren 4-character, CALL3 tarafından değiştirildi. Aynı meydanın TXT. Doğru yer ve meydanın merkezi birkaç düzine km ile örtüşmezse, hata tropo mesafelerine kıyasla küçük kalır.

## 6. Kim dinliyor

PSKReporter'dan gelen her MQTT mesajı, [5] 'u alanların adını ve taşıyıcısını bildiriyor, bu yüzden CALL3.TXT. Seçicide seçilen grup için, her istasyon için: ev sahibi, son resepsiyon, kodlanmış yollar, istasyon sayısı hissetti ve maksimum mesafe. Bir istasyon artık seçilmiş aralıkta bir şey aldığında listeden ayrılır (3, 5, 10, 15, 30 veya 60 dakika).

Haritada her bir alıcı karanlık pembe boş bir dairedir ve her birinden duygulanan istasyonlara karşı tam pembe çizgiler başlar ve nete gönderir. Hatlar her zaman görülür, tıklayarak. Dinleme istasyonlarının Randevuları, her noktanın iki başları her zaman haritada yazılır, tıklamadan (aynı anda aynı isim sadece yazılır). Bir daireye tıklayarak hatları daha da kalın hale gelir ve sekmeyi ayrıntılarla açar. MQTT'den gelen locator 6 karakterden daha azsa, program CALL3.TXT (yaklaşık bir milyon isim), alınan kare ile tutarlıdır.

Scatter meteorları. Set noktalarına gelince, MSK144, FSK441, JT6M, JTMS veya ISCAT'deki MQTT oranları, aynı şekilde UDP decodings için de geçerlidir.

Gerçek dinleyiciler değil. Bazı istasyonlar 144 MHz'de almayı ilan eder, ancak gerçekte alıcı başka bir grupta (wrong frekansı göstergesi): raporları binlerce km açılmaz. Program bunu kendi başına kontrol eder. Bir dinleyicinin VHF'de duymasını söylediği her istasyon, özel bir MQTT yazıt (pskr/filter/v2/+/{nominative}/#) ile 30 dakika boyunca takip edilir. [5]. kanıt olarak hizmet eder. Bir dinleyicinin duyduğu istasyonlardan en az ikisi aynı dakikada (±10), diğer gruplarda aktif olarak görülüyorsa, dinleyici gerçek değildir ve tüm VHF raporları silinir; MQTT kaydı ona ihanet eden adı ve istasyonları bildiriyor. Bir sonraki noktalar listesinde, MQTT düğmesi, ağın durumunu gösterir (bağlansa yeşil yaprak, kırmızı değilse) ve kaç dinleyicinin grupta ve aralıkta tercih edildiğini gösterir; her biri için diski açar ve diğer gruplar için bulunur.

Long connection China. 1000 km'nin üzerinde bir bağlantı (Clumbing Spot, MQTT Dinleme, QSO Desunti) hemen gösterilmez: “Selam > 1000 km” sayıyı söylüyor. Başka bir bağlantı, farklı istasyonlar ve en az yarı boyunca, bu kriterlerden biriyle doğrulanır: aynı istasyonda bir kafa (örneğin aynı dinleyici) ve diğer istasyonda 10° içinde aynı yönde; benzer koridor, diğer bağlantıdan 200 km; paralel rota içinde 10° ve 200 km mesafedeki yön ile rota. Her ikisi de ortaya çıkıyor ve MQTT log nedenini gösteriyor. UDP decodings her zaman görünür ve başkaları için onay olarak uygulanır. Korumalı bağlantılar 60 dakika boyunca hafızada kalır.

144 MHz altında hiçbir şey. Program 144 MHz'den oluşuyor: 143 MHz altındaki her şey kartsız. The exception is GRAVES, Fransız radarı Zheng'de 143,050 MHz'de Broye-lès-Pesmes ( JN27SI) tarafından aktarılıyor: 143,040 ve 143,060 MHz arasında bir ticari, ya da adı altında GRAVES ile bir verici olarak görünen. Reklam kurulu, alıcının kirişin içinde olup olmadığını söylüyor (s. D, C, B, 90°'den 20.5'e kadar, 7.5 °'den her biri için 6 adım, YA-MS'de olduğu gibi, gökyüzünden görüldüğünü gösteriyor: Sektörler, o dönemde adım aydınlatıldı (sequenza PE1ITR 2021: 0,8 s per step, döngü 4,8 s Data ve geometri, YA-MS'den geliyor. Trope'de hissetmek zordur, ancak olabilir.

İnsan kümelerinden rehabilitasyon. Diğer gruplarda da görünen bir istasyon, diğer isimler tarafından yapılan noktalarda şüpheli değildir (±30), insan kümeleri ( ON0DXK, GB7RAU; dm4x, RBN veya diğer otomatik kaynaklar), gruptaki aktif hasar, diğer isimlerle yapılır: birlikte birden çok grup üzerinde çalışan istasyonlar durumundadır. Böylece bir kartpostal dinleyici görünür geri döner (MQTT kaydı “rehabiliteed”) ve düşük gruplar için uzun zamandır kullanılan bir bağlantı ortaya çıkabilir.

144 MHz altındaki aktif istasyonlar. Uzun bağlantıların karantinasında, aynı dakikadaki bir istasyon, 144 MHz'in altındaki gruplar üzerinde (Diğer gruplardan MQTT raporlarından) diğer bağlantıları doğrulamıyor ve açıklanmadı, ancak benzer veya paralel bir rota doğrulanmadı.

## 7. Ticarilerin Listesi

Harita sekmesi ve noktanın% 35'ini kaplar. Top: başlık, grup seçici veya (50, 70, 144, 222, 432, 1296, 2320 MHz, 10 GHz), aralık seçici (son 3, 5, 10, 15, 30, 60 dakika) ve Update anahtarı. Aşağıda, dört metre: görünür noktalar, AS/ACS, filtre, istasyonlar dinledi.

Her çizgi, çizginin türünü gösterir, zaman UTC, DX → DE, yol, ikinci çizgide, kaynak, frekans ve yorum. En son en üstte. Çizgide bir tanesi noktayı seçer: Haritada çizgi büyür ve iki isim görünür.

## 8. Spot kartı ve troposcatter kaybı

Seçilmiş nokta için sekme taşıyıcıları, mesafe, azimut, frekans, mod, kaynak, şimdi, yorum, yol boyunca ortalama tropo indeksi ve dağınık tropo için rota kaybının tahmini.

Uzaktan ve azimut, 6371 km Dünya Yarısı ile maksimum çember üzerinde hesaplanır ( mesafe için emisenoverso, ilk azimut, DX istasyonundan perdeye kadar).

Kayıp troposcatter. Temel kaybı, düzgün zemindeki iki istasyon için, ücretsiz uzay kaybı ve yayılma kaybının toplamıdır: G3YGF [2]

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

Kayıplar dB'de, D km mesafede, mm'deki frekans, θs the diffüzyon açısı derecelerde.

8497 km, Dünya'nın eşdeğer yarının (4/3 fiziksel bir tane). theA ve θB her istasyonun ufukuna bağlıdır:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

ile birlikte {{h istasyonu ve mesafesini ufuktan yapan engelin yüksekliği. Her bir ufuk seviyesi yaklaşık 10 dB [2]. Program istasyonların ufukunu bilmiyor, bu yüzden theA = θB = 0 ile temel kaybı gösteriyor ve bunu ("smooth zemin") ilan ediyor. G3YGF, N'nin [2] toprağına reaktivite olduğu bir iklim terimini ekliyor; N'nin hava modelinden mevcut olduğu zaman kullanılacak:

```math
L_{N} = 0,2 \cdot (N - 310)
```

Doğrulama: formülün 110 km ila 144 MHz için, makalenin grafiğinde yaklaşık 168 dB yatağına karşı 166,5 dB verir. 144 MHz'e 500 km boyunca yaklaşık 206 dB veriyor. Beklenilen sinyal / oran, sistemin kapasitesidir (eirp + alıcının etkili hassasiyeti) eksi kaybı: G3YGF 144 MHz model sistemi (198 dB) ile 500 km yol SSB [2] 'de dB, bağlantı sadece saf saçın daha iyi yayılması ile ilgilidir.

## 9. My decodings: UDP from WSJT-X, JTDX ve MSHV

WSJT-X, JTDX ve MSHV, UDP'yi her çizgide kodlayabilir. Program 127.0.0.1:2237'de dinler ve aynı paketi tekrarlar, byte, 127.0.0.1:2238'de, bu port (Grider, JTAlert, a logger) kullanan diğer programlar 2238'de hareket ederseniz çalışmaya devam eder. UDP'de yer alan adresler ve kapılar Data Collection'ın sublingual'ında değişir; Bir dinleme adresi olarak da çok yönlü bir grup kullanabilirsiniz (örneğin 224.0.0.1), eğer dekoding programı bu şekilde ayarlanmışsa.

Paketler WSJT-X ağ protokolünü takip eder: ilk sihirli sayı 0xADBCDA, şema numarası ve mesaj türü. Program esas olarak üç [8] tipi kullanır:

- Heartbeat: hangi program verileri ve hangi sürümle aktardığını söylüyor.
- Durum: arama ve mod frekansı; grup aldığınız frekanstan (aynı noktalarda).
- Decode: Şimdi, SNR, zaman ve frekans kaybı, kodlanmış mesajın metni.

Duyduğunuz istasyondan: CQ 9 A2PQR JN83, CQ'dan sonra istasyondur (Bx, EU, TEST...), IK0JKL IT9MNO JM68 'da ikinci, yani gönderen. Kiracı mesajın olmasıdır, eğer 4 karakter varsa, RR73 'dan farklı; aksi takdirde CALL3 'da aramanız gerekir. TXT. Dinliyorlar: Alıcınızın şimdi duyduğu istasyonlar. Haritada QTH'den istasyona kırmızı çizgiler haline geldiler, adı ile; Decoded kutu ile plakalar ve geçerli bir lokatörü gerektirir. Doğru bir karşıdaki listede, seçilmiş grupta kaç istasyon olduğunu söyler.

QSO desunti: Yaşamak. Bir dekoding C ALL1 C ALL2 RR73 (veya 73 veya RR) şeklinde bir kapanış mesajı olduğunda, örneğin IC8TEM IK3FHP RR73 veya IK3FKP SV8PEX 73, iki istasyon bir bağlantı tamamladı. Program QSO olarak kaydeder ve iki istasyon arasında yeşil bir çizgi çizer (strate “QSO desunti: canlı). Bir çift istasyon grup başına bir kez sayılır; listedeki grup ve aralık geçerlidir. ALL3 [7]

Aynı alt dilde, Debug pane, her bir paketin metnini gösterir (tip, zaman, değerler ve mesaj), talep üzerine hexadecimal bytes ve ileriye dönük bir çizgi. Dekoding programının aslında verileri gönderdiğini kontrol etmek.

## 9 bis. Diller Hepburn ve F5LEN

Her biri programın kaynaktan ne aldığını gösterir. “ Sayfayı okuyun ve tüm resimleri indirin”, belirtilen sayfayı (dxinfocentre.com/tropo_eur.html for Hepburn [3] and tropo.f5len.org/forecasts-for-europe for F5LEN [4]) açar ve tüm önbellek görüntüleri bir görüntünün adresini indirebilirsiniz. Her görüntü için onu dışlamak için bir kutu var ve tahmin süresi ile bir alan var. F5LEN 'nun 31 Avrupalı görüntüsü, model ırkının zamanı tarafından adlandırılmaktadır (03.txt... 48.txt her 3 saat, 54... 96 her 6 saat, 108... her 12 saat) ve program [4] dosyasının adını alır; çünkü her yanı sayfanın siparişine değer. F5LEN sekmede, bir menü bölgeyi seçer: Avrupa (tropo.f5len.org/forecasts-for-europe), Avustralya (tropo.f5len.org/Australia, vk03.txt... vk192.txt) veya Cape Verde ve İrlanda arasındaki Atlantik (tropo.f5len.org/atlantic-ocean/atlantic-d4-ei, atl03.txt... Herkesin aynı adımları vardır.

Aşağıda, kalibrasyon sayacı. Resimde bir tıklama bir kontrol noktası (locator veya lat,lon: en az 6) veya değeri ile bir efsane rengine ihtiyacınız var. Program, en küçük [19] meydanlarında ikinci derece polinomal interpolasyonla koordinatları hesaplar, F5LEN için seçilen ve için renkleri okur, birim N'i 0-10 ölçeğinde bir · N + b. “Kaynak kalibrasyonunu ve okumaya çalışın”, Tarature klasöründeki dosyayı yazar ve hangi Pixel your locator düşer. Bir kalibrasyon yapmadan önce, haritaların yazarı için izin talep edilmelidir.

Bölgeler. Her kaynağın daha fazla bölgesi var, her biri sabit isim görüntüleri ile: Üzgünüm, üzgünüm. Her kaynağın sekmesi ilk olarak füzyonun önbellekini gösteriyor: tüm indirilmiş görüntüler ana haritada, her bölgenin adı ve her bölgenin adıyla birlikte bir araya geliyor; eğer OCR, hiçbir görüntünün bölgeye ve bir kaydırıcıya kapsadığı metinden kaçınırsa; her biri onu kullanmak için kutuya, aşırıların nereden geldiğini ve nereden geldiğini okuyun. El kalibrasyon bankı homonymous düğmenin arkasında kalır. Bench Bölgesi menüsü hangi görünümleri seçer, kutu “Bu bölgeyi haritada kullanın” indeksde içerir. Çerçeve tek başına bulunur (aynı renkte iki yatay çizgi ve iki dikey çizgi), görüntünün çerçevenin aşırı lat/lonuna hizmet etmesi için: Hepburn Europe (10° W–40 E, 27°-56° N) ve F5LEN Europe (15° W–40 ° E, 30°65–65° N) zaten programda, dört O E S N alanında yazdığı diğer bölgeler için eksenli etiketleri okuyorlar. Ölçek multiplier, iki kaynağı karşılaştırılabilir yapar: son değer = multiplier × efsane değeri. [3] F5LEN [4] F5LEN

## 9 ter. Word

İki belgenin "Export to Word" düğmesine basması gerekir. Dosya Office Open XML formatında yazılır; formüller Word denklemleri haline gelir (OMML), bu nedenle [16] denklem editörü ile düzenlenebilirler. Eğer tasarruf penceresi mevcut değilse, dosya eklenebilirliğin yanındaki Belge klasöründe sona erer.

## 9 quater. Kim beni çağırıyor

Eğer bir mesaj yerel UDP'de size gönderilirse (ilk adı sizindir, örneğin IC8FAX IK3FHP JN55 veya IC8FAX IK3FHP -12), programınızı QTH'den gelen istasyona, size bir kara dotted ve flashing hattını çağıran istasyona izler. Kiracı CALL3.TXT (El sesleri dahil) veya mesajdan gelir. Bu istasyon, UDP'nin QSO'nun kapatılmasına kadar kalır ( RR73, RR veya 73, iki yönden birinde), son çağrıdan en 3 dakika içinde; dışlama kriterlerini takip edemezsiniz, bu yüzden doğrudan bir çağrı asla kaçmıyor.

PSKReporter, MQTT aracılığıyla, mesajların metnini iletmez: sadece kimin aldığını, frekansı, yolu ve SNR ile kim aldığını söyler. Bu yüzden doğrudan bir çağrı sadece kendi UDP decodings tarafından tanınır.

## 10. Zaman kontrolü (NTP)

Dijital modlar ve nokta zamanı kesin bir saat gerektirir. Her 3 dakika program, 10 NTP sunucularını sorgular (SNTP on UDP 123): 0.pool.ntp.org, 2.pool.ntp.org, 3.pool.ntp.org, ntp.ntp.com. [11]

Her cevap için kartpostalları ve geri dönüş gecikmesini hesaplar, anormal değerleri ortadan kaldırır ve kartpostalları ile ve SNTP [10] tarafından sağlanan gecikmeyi alır. En az 3 cevap, programın UTC saatlerini aynı fikirde ve bir sonraki görünüşe göre NTP:OK yeşil görünüyor; eğer cevaplar yeterince VR görünmüyorsa: Kırmızı ve saat içinde bilgisayardan kalır. Sistem saati dokunmaz, çünkü yönetici izinler gerekli olacaktır; tüm iç program programları (spots, 5–60 dakika pencereler, decodings) uyumlu zaman kullanır. Kontrol her zaman arka planda döner: arayüzde sadece saat sonraki sonucu görüyorsunuz; sunucular, aralıklar ve LIVETROPOVHF.ini (ntp_servers, ntp_minutes, ntp_min).

## 10 bis. Genel dışlama kriterleri

Katman barında iki kutu çizdiğiniz her şeye uygulanır (spot, PSKReporter, decoding, QSO desunti):

- 100 km altında Exclude. 100 km'den daha az istasyonlar arasındaki trafik, tropo için önemli olmayabilir (köpek dalga, optik aralığı): liste listesinde görünmediğiniz ve görünmediğiniz kutu ile. Baş kapalıdır.
- 1000 km'den fazla: sadece kareler. QTH çevresinde (Başlangıç seti) tüm yönlerde 1000 km bir taç var. Bir bağlantının iki istasyondan sadece biri bu taç dışındaysa, bağlantı bir çizgi olarak çizilmez: 45°'de renkliler, 45°'de çarpıştılar, istasyonlar (4 karakter). Aynısı, bir daire olarak yerine bir kare olarak görünen PSKReporter istasyonları için de geçerlidir. Şimdiye kadar, hatları doldurmadan gördüğünüz trafik. Geçerli bir taşıyıcı olmadan bağlantının uzunluğu sayılır. QTH meydanı asla lilac'da renkli değil, taç ötesinde bir istasyona dinlerseniz bile: belirtilen taşıyıcıdır, istasyondan çalışan herhangi bir isim kullanır. Kutunun yanında taç ikizi seçilir: 1000, 700 veya 500 km. Kutu Corone 1000/700/500 km QTH etrafında üç taç çiziyor, en işaretli seçimle ve kuzeydeki mesafeyi çekiyor. Noktaların listesinde mesafe takip edilir. Baş bitti. kriter karantinadan sonra uygulanır: uzun bağlantılar hala meydanlar olarak görünmemektedir.

lilac meydanları 45°'de yok edilir. QTH ilgilerinizden uzak trafik, propagasyonun açık olduğunu anlamanızı sağlar, ancak hatları, yüzlerce kilometre uzunluğunda ve çoğu zaman onlarca, haritayı kaplayın ve sizin için neyin önemli olduğunu gizleyin: istasyondan kullanabileceğiniz açılışlar. Bu nedenle, seçilmiş taç ötesinde, program çizgileri çizmiyor, ancak locator meydanları (2° × 1°) dahil istasyonların renk. Sonuç, «aktif bölgelerin” bir haritasıdır: Yakındaki birçok lilac meydanının ortaya çıktığı yerde, bir konduit veya bir açılış var, doğrudan sizi endişelendirmezse bile; eğer bölge size karşı genişliyorsa, bu yönde anten işaret ediyor. 45° pano, tropo indeksinin ve ızgaranın altında görmenizi sağlar, bu yüzden tahmini (düşün rengi) ve gerçek trafik (not). QTH meydanınız asla renkli değil, çünkü başlangıç noktası ve uzak bir alan değil. Sadece tüm taçların ötesindeki meydanlar renklidir: taç tarafından geçen bir meydan, ya da içeride, iki istasyondan biri olsa bile özgür kalır. hatları görmek için, sadece kutuyu kapat veya daha büyük bir taç seçin.

## 11. Temalar ve diller

Temalar: IC8TEM programlarına 18 ortak, YA-NEC ve E-LAYER ANALYZER olarak aynıdır: Clear, Night, Black, sekiz pastels (mavi, portakal, aquamarine, yeşil, pembe, lilac, indigo, kahverengi), Scia meteorica (YA-MS paleti), IP-TRX-CTRL (Chiaro, Notte, Nero) ve Blue, Mor, Yeşil. 1.3.4'ten itibaren Chiaro, YA-NEC gibi görünüyor: ışık gri geçmişi, sınırlanmış beyaz paneller, mavi gece sekmeleri ve mavi aksan gibi görünüyor. Karanlık temalarda da temel harita karanlık hale gelir. Tahminin renk ölçeği tüm temalarda aynı kalır. Dar ekranlarda (örneğin 1500 piksel altında, örneğin 1366×768) saat sadece saat gösterir, bu yüzden tema, metinler ve dil eğrileri görünür. Diller: YA-MS [17] 28. wiki (İngilizce, Seyahat, Deutsch, Nederlands, Français, Español, Português, Dansk, Norsk, Svenska,.slenska, Eesti, Lietuvi, Latviešu,.,, La, Türkçe, Arapça, İbranice ve Urdu tüm pencere aynaları sağdan sola; harita [12] olarak kalır. Çeviriler arayüzü endişelendirir; statü çubuğunun iki teknik belge ve mesajı İtalyan'da kalır. Günlerin isimleri uluslararası ayarlardan gelir. Seçilen dil için NET.

Tema ve dil, en üstteki iki menüden diğerine değişir ve programı yeniden başlatmadan ve verileri toplamadan hemen uygulayın. Seçim LIVETROPOVHF'de kaydedilir.

## 11 bis. Suspected ve kendi öğrenme raporları

Suspected Reports sekmesi, program kontrollerinin her şeyi garip bulduğunu, türü ve adı ile listelenen son 24 saat ile toplamaktadır. Program kendi başına öğrenir: her bir çift için (tip, isim) olumlu sonuçları sayar (+) ve aksine (-), ve 3 kabul edilen sonuçlar otomatik bir kural yaratır; tam tersi bir sonuç bunu sorgulamaktadır. Kullanıcı kararları (İyi, Zavallı) her zaman uygulanır; pulsante düğmesine karar verir. Kurallar Data/APPRENDIMENTO'da. Programa bir sonraki TXT; bir yıl için takviye edilen otomatikler unutuluyor.

| Tipi Tipi Tipi Tipi | When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When When | Çıkış + / | Kural Etkisi |
| EME ( Q65/JT65) | Q65 veya JT65, ufuktan her iki kafaya ya da EME'ye 1000 km üzerinde, | Ay her zaman yaygındır | Discard: EME istasyonu, uzun Q65/JT65 s aynı zamanda ortak ay olmadan dışlanmış durumda. İyi: Tropo olarak tedavi |
| Uzun bağlantı | 1000 km'den fazla, kurantine | + ortaya çıktı, - onay olmadan sona erdi | İyi: istasyonun uzunluğu hemen gösteriyor. Discard: Asla |
| Diner | Diğer gruplarda aktif istasyonları duyan PSKReporter alıcı | «Uncarded, + insan kümeleri tarafından rehabilite edildi | İyi: Hiçbir zaman kartsızlanmadı. Discard: Her zaman |
| Farklı ev sahibi | Square CALL3 'dan farklı aldı. TXT | + Her seferinde tekrar edersiniz | İyi: istasyon taşındı, alınan meydana değer. Discard: vale CALL3.TXT |
| Square Center | 6character with LL or MM |  —  | Bilgi sadece bilgi |
| Otomatik alıcı | Randevu -SDR, /SDR, - # veya -n |  —  | Bilgi sadece bilgi |

EME. Q65 ve JT65, Luna aracılığıyla iletişim yollarıdır: Teoride, saç meteorları gibi dışlanmış maddeye düşerler, ancak aynı zamanda tropo'da da kullanılırlar. Program Ay'ın konumunu Almanac Astronomical (yaklaşık 0.3°) düşük seviyeli formüllerle hesaplar. [20]: Q65 veya JT65 'de bir bağlantı 1000 km'yi aşıyorsa ve Ay her iki kafaya da ufkun üzerindedir, neredeyse kesinlikle EME ve dışlanmıştır ( hariç). Ay yaygın değilse, bağlantı trope'de mümkün kalır ve uzun bağlantıların karantinasını geçer, ancak sinyal görünür. Setler için geçerlidir, MQTT ve UDP decodings.

## 11 ter. İletişim discard kriteri, biri tarafından

Program sadece 144 MHz'de ve ötesindeki Tropopheric propagasyon göstermek istiyor. Her gelen veriler (Cluster Row, PSKReporter Report via MQTT, UDP decoding) bir dizi filtreden geçer, her zaman aynı sırayla. Bir kartpostal veri hiçbir şeyde yok değildir: sayılıyor ( üst barda dışlanmışların teması) ve neden şüpheli olduğunda, sekmede Suspended Reports görünür.

|  N.  | Kriterleri Kriterleri | Nerede? | What Discarded | Değiştirebilir misiniz? |
|  1  | Band | Set, MQTT, UDP | Tüm 143 MHz (ayrıca 50 ve 70 MHz), GRAVES 143,050 MHz hariç. Kurulan gruplar 144, 222, 432, 1296, 2320 MHz ve 10 GHz | Hayır. |
|  2  | Uydu QO-100 | Set, MQTT, UDP | 2400,0-2400,5 MHz ve 10489,5-10490 MHz: geostationary uydu trafiği, tropo değil | Hayır. |
|  3  | Meteor saçı | Set, MQTT, UDP | JT6M ve ISCAT (aynı zamanda ISCAT-A/B), "&" ve non-tropoxy propagasyon etiketleri ile yorum (<AU>, <AUE>, <ES>, <FAI>, < F2 >) her zaman MSK144, FSK441 ve JTMS (ayrıca <MS>) sadece 250 km'den fazla kabul ediyoruz, çünkü bu modlarda da tropo'da kullanılmaktadır. | Hayır. |
|  4  | EME (via Luna) | Set, MQTT, UDP | EME, yorumda yazdı; Q65 veya JT65, Ay'ın her iki kafa için ufukta olduğu 1000 km üzerinde; istasyonlar EME olarak öğrenildi | Evet: Şüpheli sinyallerin İyi Kuralı |
|  5  | Gerçek olmayan Diner | MQ | Diğer gruplarda aktif olan 144 istasyonda duymayı söyleyen PSKReporter alıcı (en azından iki): Onun ilişkileri iptal edilir. DX insan kümeleri aynı istasyonları doğrularsa geri iyi | Evet: İyi / Zavallı |
|  6  | Uzun bağlantıların Quarantine | Tüm çizgiler | 1000 km'den fazla bir bağlantı bir onay bulana kadar saklı kalır: Aynı yönde aynı istasyon, 200 km koridorda başka bir rota veya en az yarısı boyunca 10° ve 200 km içinde paralel rota. Onay olmadan 60 dakika sonra sona erer | Evet: Good / Off to station |
|  7  | 100 km | çizgiler ve liste | On-box ile, 100 km'nin en kısa bağlantıları (var dalga, optik aralığı) | Box, başlangıçtan itibaren |
|  8  | STH'den taç ötesinde | çizgiler hatları | Eğer bir istasyon QTH'den 1000, 700 veya 500 km'den fazla ise, hiçbir çizgi: sadece iki istasyon tarafından kaplı olan lilla meydanları. QTH meydanınız asla renkli değil | Box ve ikiz, 1000 km |
|  9  | Zaman penceresi | Her şey her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her şeyi her | Seçilen aralığın eski noktaları (üç ila 60 dakika) | Menu at the top |
|  10  | Çift Çift Çift | küme kümesi | Aynı nokta (same DX, aynı nokta, aynı grup, aynı kaynak) tekrarlandı | Hayır. |

Kriterleri 4 ve 6. Q65 veya JT65 'de, Common Moon olmadan 1000 km'de bir bağlantı mümkün değildir: 4 kriter tarafından iptal edilmez, ancak diğer uzun bağlantı olarak 6. kriter için geçer. Size doğrudan çağrılar (bölüm 9 kuter) 7 ve 8 kriter için tek istisnadır: her zaman çizilirler.

Bir atık değil. Kiracının düzeltmeleri (CALL3. El sesleriyle, kümeden düzeltmeler ve taşıyıcılar öğrendiler, ekler -SDR, bir istasyon çizdiğin kare LL/MM merkezi. Herhangi bir ev sahibi olmadan bir istasyon çizilemez: “-” mesafe ile listede kalır.

## 11 kuter. Renkler ve semboller

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

## 12. Ayarlar ve portability

Pencere. Başlangıçta, pencere ekranın kullanışlı alanının% 90'ını kaplar (Uygulama çubuğunu kapat), merkezi bir şekilde, bu nedenle başlık çubuğunun% 125 veya% 150'sini yeniden boyutlandırır. Yararlı alan 1280 × 720 puan altındaysa, pencere kısmı en üstlenmiştir. Boyut, pozisyon ve maksimum durum LIVETROPOVHF'de kaydedilir. Bir sonraki başlangıçta kapanış ve sonunda, başlık çubuğu hala bir ekrana düşer (örneğin dış bir monitörden sonra); Aksi takdirde %90'a geri dönersiniz. Minimum boyut 900 × 560.

Program taşınabilir: AppData klasöründe veya kayıtta asla yazmaz. Her şey eklenebilirliğin klasöründedir: LIVETROPOVHF.ini (İlk kapanışda yaratılanlar), CALL3.TXT (el yazılı sesler, düzeltmeler ve lokatörlerle), harita önbellek ve hava verileri, kurallar öğrendi (Dati/APPRENDIMENTO.TXT) ve LIVETROPOVHF. Ayrıca bir USB çubuğunda tutabilirsiniz, klasörün yazılabilir olduğu sürece.

Ad arşivi (SQLite [14]). Kabul edilen her nokta ve her PSKReporter dinleme raporu LIVETROPOVHF'de kaydedilir. hafızada sadece son 60 dakika liste ve harita için kalır; arşiv her şeyi tutar ve tarihçi ve istatistikler için hizmet eder (aynı zamanda UDP decodings), örneğin grup ve günde maksimum mesafeler, meydanlar ulaştı ve gerçek noktalar arasındaki arka karşılaştırma. Filtreden gelen şeritler sayılıyor ancak arşiv değil.

## 12 bis. Otomatik yükleme ve güncelleştirme

1.0 sürümden itibaren program zaten Quihub.com/ic8tem/LIVETROPOVHF'nin çıkış sayfasından hazırlanmaktadır: Windows için yükleme (64 bit, 32 bit ve ARM, C: LIVETROPOVHF yönetici hakları olmadan) ve portatif zip; Windows 7 SP1 ve 8.1 için ayrı paketler (-windows7-x64 ve -windows Linux paketleri ile, program ilk başlangıç için kopyalanır -/LIVETROPOVHF, bu yüzden ayarlar ve veriler programa bir sonraki kalır; Mac verileri LIVETROPOVHFapp.

Her 30 dakika (Başlangıçtan birkaç saniye sonra ilk kontrol, bir hata için son kez kapalı olsaydı) program tek başına okur, hiçbir şey sormadan, son Salı. Eğer sürüm sistem için uygun portatif paketi indirin ve ekranda program dosyalarını değiştirirse: kullanımdakiler yeniden adlandırılır *.old-update ve bir sonraki yeniden silinir. Ayarlar, arşiv, Data klasörü ve CALL3. TXT dokunmuyor. Yeni versiyon bir sonraki başlangıçtan aktiftir: kurulumda bir pencere hemen yeniden başlatmayı teklif eder; acil düzeltmeler için, salıverilen [o Yüklülük] işaret eder, yeniden başlatılamaz. Kontrol durumu ve kullanımdaki sürüm, durum çubuğunun alt kısmında yazılır; LIVETROPOVHF'de güncelleme =0'ı kapatmak için. Mac'de uygulama imzalanır ve kendisini değiştirmez: program sadece yeni bir sürüm olduğunu bildiriyor.

LIVETROPOVHF görüntü sürüm numarası ile başlangıçta görünüyor; sonunda program onay ve kullanım için teşekkürler.

## 12 ter. Hata log ve durum bar

Herhangi bir öngörülemeyen hata Data / Terrori.log'da bile hesaplama loguyla sona eriyor: tarih ve zaman, sürüm, sistem, açık sekme, programın ne yaptığının tam hatası ve son hatları. Bir arayüz hatası artık programı kapatmıyor: Durum çubuğunda “önemli bir hata” (toplama: kayıt açın). Bir sonraki aşamada program raporu göndermek isteyip istemediğini sorar: bir GitHub sinyalinde tarayıcıyı zaten derledi, adaylık, locator ve PC kullanıcı adı kaldırıldı, ücretsiz bir GitHub hesabı ile kontrol edin ve gönderirsiniz. Giriş aynı zamanda her güncelleme kontrolü ile de biter.

Durum çubuğunda kaynak topları (DX kümesi, RBN, PSKReporter, MQTT, UDP) tıklanır ve kaynağa getirilir; komik bir kaynak neden kapalı olduğunu söylüyor: sunucu tarafında istasyon Node sabitlendi, yanıt veya UDP dinlemedi, WSJT-X, JTDX veya MSHV’den paketler olmadan. Bir sürüm numarasına hemen erişim güncellemelerini kontrol edin.

## 13. Platformlar

Dış kitaplıklar: [12] arayüzü için Avalonia, MQTTnet, PSKReporter [13], Microsoft'un gerçek zamanlı akışı için bir MQTT müşteri olarak. [14] arşivi için veri.Sqlite.

C#.NET 10 (LTS) [15] ile Avalonia arayüzü, Windows 64 ve 32 bit için bağımsız bir kesinti olarak yayınlandı, Mac Intel ve Apple Silikon, Linux Debian ve Raspberry Pi (64 ve 32 bit) Limitler: Windows 10/11; Mac 14 veya daha sonra, doğru → Açık; Pi 2 / Zero 2 W up (ARMv6 modelleri.NET tarafından desteklenmez).

## Teşekkür ederim teşekkür ederim
Windows 7 ve Windows 8'de test için I5MZY Massimo Zannoni sayesinde 8.

Airtights: ITU-R P.453, Study Group 3 (© ITU), kişisel ve ticari olmayan amaçlar için kullanılır.

## Ana kaynaklar

1. S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", 41. Troposferical Ways, Event Control List (Hepburn/ F5LEN tahminleri, Windy ile yüksek sıcaklık profilleri, DX seti açılış testi olarak), yalnızca tahminlerin aşırı rotalar için yeterli olmadığını gözlemleyin.
2. J. N. Gannaway G3YGF, “Tropospheric Scatter Propagation”, QST Kasım 1983 ( Radio Communication, RSGB, Ağustos 1981'deki eponymous makaleden). Ücretsiz uzay kaybı ve difüzyon, eşdeğer yarıçap 8497 km, ufukta 10 dB, iklim terimi 0.2 · (N-- 310), bitki kapasitesi ve sinyal / düğüm oranı, örneğin 144 MHz.
3. W. R. Hepburn, DX Info Centre, “Tropospheric Ducting Tahmini” (dxinfocentre.com/tropo.html) ve “Heburn Tropo Index (HTI).” Index ölçek ve değer anlamı, 6 günde tahmin.
4. F5LEN, Avrupa için tahmin (tropo.f5len.org/forecasts-for-Avrupa: Sayfadaki görüntülerin listesi, sınıf efsanesi Disturb... Görüntüde Duct ve dünya tahminleri (tropo.f5len.org/WWW) ve “Dünya VHF tropospheric propagation tahmin aracının yeni versiyonu.” Ünite N, 40 adımlarını günde 4 kez güncellemek.
5. mqt.pskreporter.info, dağıtım MQTT gerçek zamanlı olarak pskreporter.info ( M0LTE tarafından satın alındı, P. Gladstone akışı izinle kullanıldı). Kapılar 1883-1886, pskr/filter/v2 konu planı.
6. 8/11/2026 ve Go testi (core.go, gui.go) programının yazarı tarafından sağlanan 8/11/2026 ve Go testi (core.go, gui.go) Ad Kuralları, negatif filtre, bant genişliği segmentleri, telnet küme yönetimi, temalar, diller ve portability.
7. CALL3.TXT, aday veritabanı → JTDX ev sahibi (I. UA3DJY, A. ES1JA senaryo, JTAlert/hamspots.net verileri). Proje dosyasında doğrulanan isimlerin dosyaları ve sayısı.
8. WSJT-X, kaynak kodu, NetworkMessage.hpp dosyası (UDP protokolünin yazarı: Kalpbeat, Status, Decode). Genel referans, eklenme; JTDX ve MSHV aynı formatı kullanıyor.
9. Ters Beacon Network (reversebeacon.net), telnet düğümünün servis sayfaları. Ports 7000 (CW, RTTY) ve 7001 ( FT8) sadece isimle giriş.
10. D. Mills, RFC 4330 "Simple Network Time Protocol (SNTP) Version 4", IETF 2006. Paket formatı, atık hesaplaması ve geri dönüş gecikmesi. Genel referans, eklenmedi.
11. Honda Pool Project (ntppool.org); INRIM, NTP zaman servisi (ntp1/ntp2.inrim.it); PTB (ptbtime1.ptb.de). Servers zaman kontrolü için kullanılır. Genel referans, eklenmedi.
12. Aval UIonia, resmi belgeler (docs.avaloniaui.net). Çok platform arayüzü, temalar ve düzeni sağdan sağa sola. Genel referans, eklenmedi.
13. MQTTnet, MQTT için.NET kütüphanesi (github.com/dotnet/MQTTnet). PSKReporter akışı için kullanılan müşteri. Genel referans, eklenmedi.
14. SQLite (sqlite.org) ve Microsoft. Data.Sqlite (öğrenme.microsoft.com). Bir dosyada Archive. Genel referans, eklenmedi.
15. Microsoft,.NET 10 – bir dosyada işletim sistemleri ve özerk yayınları destekledi (dok.com/dotnet). 13.Bölümde belirtilen platformlar ve sınırlar 13. Genel referans, eklenmedi.
16. ECMA-376 "Office Open XML File Formats", bölüm 1, bölüm 22.1 (Office Math Markup Language)..docx dosyasının ve denklemlerin yapısı. Genel referans, eklenmedi.
17. Programın yazarı tarafından sağlanan YA-MS wiki'nin 28 dilinin listesi.
18. Open-Meteo, hava tahmin API (open-meteo.com), NOAA GFS modelinden verilerle. Deniz seviyesinde basınç isobare (bakım 2).
19. J. P. Snyder, "Map Projections - A Working Manual", USGS Profesyonel Paper 1395, 1987 ve georeferencing görüntüleri için standart polinom dönüşüm yöntemleri. Genel referans, eklenmedi.
20. Astronomical Almanac, Bölüm D, “Ay için düşük hassasiyet formülü” (yaklaşık 0.3 °) EME bağlantılarını tanımak için kullanılır. Genel referans, eklenmedi.
