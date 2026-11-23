# Tahmin manuel

> İtalyan tarafından otomatik çeviri: şüphesiz İtalyan kılavuzuna değer.

Tahmin sekmesi, tropopheric propagasyonunu kendi başına hesaplar: istasyonunuz ve buna karşılık gelen bir işaret haritası. Bu manuel her verileri açıklar, nasıl hesaplanır ve hangi kaynaktan gelir. Kare braketlerdeki rakamlar, alttaki kaynaklara atıfta bulunur; programın her verilerinin aynı kaynağı ve paragrafa atıfta bulunan bir çizgi (simbolo i) vardır.

## 1. Ne yaptığı ve ne yaptığı

GFS [1, 2] modelinin dikey sıcaklığı ve nem profillerinin tahminleri, hava katmanının tabaka tarafından reaktivitesini ve sinyal kazançlarının normal bir atmosfere kıyasla ne kadar arttığını tahmin eder. Yolda gerçek zemin (Copernicus GLO-30 [3]), diffraction, troposcatter, ducts ve alıcı gürültüyü ekliyor.

Bu bir ölçü değil: Yüzlerce metre uzakta olan bir hava modeli üzerinde yapılan bir tahmin. Durumları karşılaştırmaya hizmet eder (bugünden daha iyi, 70 cm 2 m'den daha iyi, bu bundan daha iyi davranır), bir bağlantı garanti etmemek. Paragraf 14 bunu doğrulamak için kanıt toplamak nasıl açıklıyor.


> "Proforasyon genelleştirilemez, ancak yerel morfoloji ile ilgili olarak incelenmiştir. »
> - Volker Grassmann, DF5AI, Flavio Egano, IK3XTV (Propagation on 2 metres) tarafından bildirilen

Noktasal hesaplamayı yönlendiren ilkedir: Her rota onun toprağı üzerinde hesaplanır, ufukları, denizleri ve rahatlamaları ile; harita genel resmi verir, cevap verir.

## 2. Dil komutları

- Band: 144, 432 veya 1296 MHz. Harita ve hesaplama seçilmiş grubu kullanır.
- Şimdi: cursor şimdi tahminleri 72 saat öncesine kadar hareket ediyor, 3 saat içinde.
- Güncelleme tahminleri (önemli bölge): GFS profillerini görünür haritayı kapsayan 240 puana kadar bir ızgara üzerine indirin. Ağ alanı (0.5° veya daha fazlası) magnification bağlıdır.
- Dur: Bir indirmeyi veya hesaplamayı kesmek.
- Haritanın sağ paneli: üç seviye, Essential, Normal ve Tamam, bir kutu grubuna dön; her kutu sonra el ile değişebilir. Kutular üç gruptadır:
  - Haritada: isolinee del Win, aynı zamanda 3-7 dB (terek), isobare, termal inversiyon, hava işaretleri S ↓ ↑, korelasyon kazanç / baskı, doğrudan görüş alanı (paragraflar 4, 5, 7 ve 9 bis). Renkler her zaman kalır.
  - sinyalin hesaplanmasında: hesaplamanın tüm hatları, düz Dünya profili, hesaplama log ve detaylı dosya kaydı.
  - Düzeltmeler ve canlı veriler: hava düzeltmeleri (Misme, sübvansiyon), hava deniz kontrastı, ortalama noktalar (korridoi), APRS 2 m atlar ve LoRa, dağın ötesinde (paragraflar 4 bis, 9 bis, 9 kuter ve 10 bis).
  - Temel olarak sadece izolinees, hava ve medya düzeltmeleri noktalarla yapılır; Normal ekin çemberi, inversiyon, hava işaretleri, doğrudan görüş, düz dünya, hava-sea ve APRS; Tamam onu döndürür. Seviye sadece gördüğünüzü ve hangi düzeltmelerin girildiğini değiştirir: hesaplamalar aynı kalır.
- Veri ve tarihsel vakanın kaynağı: profillerin nereden geldiği ve hangi gün baktığınız (Paragraflar 3 bis ve 3 ter).
- İstasyon ve muhabirim: Locator, antenler, güç, kablolar, gürültü (paragraflar 7 ve 12).
- Optiklerde sinyal ve kapsamı hesaplayın: zemindeki iki hesaplama (paragraflar 8–13).
- Hesap kaydı: Data / tahmin-calcoli.log dosyasında kullanılan sayılarla her adım.

## 3. GFS data to profil

Ağ ve her saat boyunca basınçı 2 m'de, rüzgar 10 m'de, limit katmanının yüksekliği, CAPE ve sıcaklık ve nem yaklaşık 1000, 975, 950, 925, 900, 800, 700, 600 ve 500 hPa.

Seviyeler ipsometri formülü ve sanal sıcaklık ile hesaplanır; zeminin altındaki seviyeleri azdır. Her seviye için:

- N = 77,6/T · (P + 4810·e/T), T in kelvin, P ve hPa ve ITU-R P.453-14, eq. 9, EF düzeltme faktörü (resmi metin üzerinde kontrol edilir) [4].
- M = N + 157·h, saat içinde h ile: değiştirilmiş reaktivite. M nereye giderse bir konduit vardır.

## 3 bis. Data source

Data Source menüsü, profillerin nereden geldiğini seçer:

- Otomatik (recommended): GFS, NOMADS [14] 'dan önce, modeli dağıtan NOAA servisinden: her an için program, G RIB2 formatındaki bölgenin iki kesimini sorar ve bunları kendi başına okur; NOMADS programına saygı duyan bir dakikaya sahiptir. Geniş ızgaralar ile (yukarıya 0,5 °'den fazla) her 3 saatte 0,5 ° veya 1. dosyaları kullanır, çok daha hafif. NOMADS'nin vermediği noktalar veya NOMADS yanıt vermezse, Open-Meteo [2] 'ya sorun. İki kaynak aynı modele ve aynı yarışa verir, böylece aynı ızgarada karıştırabilirsiniz.
- Open-Meteo'dan GFS: sadece Open-Meteo, 1.2'de olduğu gibi ( dakika başına sınır bekleniyor).
- NOAA NOMADS'dan GFS: sadece NOMADS. Slower indirmek için (saat başına iki dosya, bir istek arasında ikinci bir tane ve diğer NOAA limitini aşmamak), ancak Open-Meteo limitini tüketmiyor.
- ICON-EU (DWD) ve ECMWF IFS 0.25°, Open-Meteo'dan: Ağ boyunca uygulanan GFS'den başka modeller ve rotalar için. GFS ile karıştırmıyorlar: Aynı zamanda iki modeli karşılaştırıyor, tahminin nasıl belirsiz olduğunu görmek için basit bir yoldur. ICON-EU sadece Avrupa'yı kapsar; ECMWF tüm GFS basınç düzeylerine sahip değildir ( eksik seviyeler atılır) ve limit katmanına sahip olmayabilir.

Kullanılan kaynak, statü hattında ve sonuçların ilk satırında yazılır (Data source), her hizmetten kaç puan geldi ve önbellekten kaç tane daha geldi. Disk önbelleği model tarafından ayrılır.

## 3 ter. Tarihsel vakalar

Tarihi The Historical The Historical Vaka menüsü, Open-Meteo [15] arşiv GFS tahminleri ile belgelenmiş bir açılış inceler: harita dava alanına taşınır, grubun kendisini ve zaman cursor davanın ilk gününden ayrılır. Güncelleme tahminine göre, bu gün profillerini indirmeniz; sinyal hesaplaması mevcut tahminler için çalışır. Modeli gerçekten ne olduğu ile karşılaştırmaya hizmet eder.

İlk vaka 14-15 Ağustos 2022'de QST'de (Kayıt 2022, "Dünya Yukarıda 50 MHz" [23]) şöyle dedi: Manitoba ve Kansas/Missouri arasında yüksek bir baskı altında, 144 ila 1296 MHz'de neredeyse hepsi FT8 'da, Q65 'da 10 GHz'de ve 1941 km'de. İkincisi, 29 Aralık 2021 ve 2 Ocak 2022 tarihleri arasında Tyrrhenian ve batı Akdeniz'de, IC8TEM 'nun raporlarından: 29 Aralık 2021 ve 2 Ocak 2022 tarihleri arasında, İtalya, Sicilya ve İspanya arasında büyük bir açılış. Eski vakalar için (e.g. Eylül 2000) Open-Meteo arşivinin hiçbir verisi yoktur. Arşivde limit tabaka yüksekliği yoktur: düzeltmesi uygulanmaz. IT9GSF EB5EA

## 4. indeks ve renkler

Programın iki seviyesi arasındaki her katman, N'nin masa 6.5'inin eşi ve Dutton [5] 'nun eşiği ile düştüğünü karşılaştırır: örneğin 1000 ile 850 hPa arasında 60 N/km arasında bir düşüş normal, 100'den fazla N/km süper-refraction. Süper-refraktif bir tabaka, kazanç elde etmek için 10 ila 15 dB'ye değer; grubu 20 ila 25 dB'den 25 dB'ye kadar tuzağa düşüren bir konduit, M. Güçlü rüzgarın zemine göre, yüksek limit tabaka ve yüksek CAPE kazancı azaltır, çünkü tabakaları ortadan kaldırır veya kırarlar [5, § 4.4].

Renk dB'de tahmin edilen kazanç, tüm temalarda sabit bir ölçek eşit:

| Kazandı | Renkli Renk | Anlam |
| 7 dB | Hiç kimse hiç kimse yok | Neredeyse normal atmosfer |
| 7-10 dB | mavi mavi mavi mavi mavi | gradient over normal |
| 10–13 dB | yeşil yeşil yeşil yeşil | süper-refraktif tabaka |
| 13-16 dB | sarı sarı sarı sarı | Güçlü tabaka, ama grup için giriş olmadan |
| 16-18.5 | portakal | Çok güçlü tabaka veya çok ince duct |
| 18,5-21 dB | kırmızı kırmızı kırmızı kırmızı | Conduit Trapping Band |
| 21 dB | mor | Güçlü ve derin dük |

Ekran kutusu ile 3-7 dB bölgeleri 3 ve 7 dB arasında, bir şeylerin oluştuğu ama ölçek altında kalabiliyorsunuz, ışık gri kısıtlaması ile görebilirsiniz; renkler değişmez. Kendi tahminleri ile kaplı olan ve en görünür kazanç gösteren alan bir pane delimitler: 7 dB altındaysa harita neredeyse normal, çünkü veriler eksik. Update tahminlerine yeni bir tıklama indirildiğinde kesinti yapmaz; bunu durdurmak için.

Renkler% 82'ye kadar tıkanır, bu yüzden temanın geçmişi sadece onları değiştirir. Kiracılar ızgara ve meydan isimleri renkler üzerinde tasarlanmıştır. Tahmininiz orada değil, Hepburn veya F5LEN, bu kaynağın rengiyle 5 veya daha fazla satranç kart ortaya çıkıyor.

Harita yargıçları her tek başına. Sinyal hesaplaması, katmanın rota boyunca sürekli olduğunu kontrol eder (paragraf 9): haritadan çok sarı olan bir rota sizi düşünmenizi sağlar.

## 4 bis. Ticariler tarafından medyaya yönelik tahminler: koridorlar ( 1.3.3)

Copyright Politika: Yalnız bir bağlantı asla bir konduit çekmemelidir. Bir izole ve kısa resepsiyon, bir geçiş uçağının ( APRS paketi çok kısa), birkaç dakika veya bir hatanın açılışı olabilir ve "kırıklama" aldatırlar. Program aynı grupta bir koridoru tanır:

- En az 3 çift bağımsız istasyon (toplan istasyon) en az 300 km yol üzerinde çalışır;
- Yollar paraleldir: ±15° içinde ortalama puan, ortak eksenin 150 km içinde;
- Son 30 dakikada, en az 10 dakika mesafede tekrarlanan kirişle (ürcülük);
- ARS için, bir at sadece aynı atlama tekrarlanırsa sayılır;
- 1.3.4'ten itibaren, sadece kiriş en az iki farklı kaynaktan geliyorsa (örneğin PSKReporter ve bir küme): tek bir kaynaktan, 2 m'deki bu mesafelere, daha sık EME, meteor veya yanlış yükleyiciler tropo'dan geliyor.
- 1.3.4'ten itibaren, istasyonları "toRay" saymayın: Geçen saat içinde en az 5 muhabirin 1000 km'den fazla bir fana yayılmıştır. 2 m'de trope yön verici; İsrail, Portekiz, İngiltere ve İtalya'yı 2000-3700 km'den bir araya getiren bir alıcı, çoğu zaman olumlu sinyallerle yapılandırıldı. 17-22/11/2026 I5MZY 1000 km'den fazla konaklamanın 72'si istasyonlardan geldi; 22/11'de 10:53 UTC OH6HLH, OZ1ITS, F4LUK, OM8ABC ve SP2JLR, ve filtre ile en uzun koridor 2175 km (regole 1.3.3)'den 615 km'ye indi.

Kaynaklar: küme noktası ( AS/ACS değil), PSKReporter via MQTT, QSO ve WSJT-X / JTDX /MSHV decodings ve APRS 2 m ve LoRa 433/868 MHz ile atlar.

APRS: 2 m ve LoRa. Program, APRS-IS'i sadece (bitki.aprs2.net, Avrupa'da filtre) okur ve bilinen konum istasyonları (sorgente → filipeater → IGate) arasında radyo atlıyor. Üç grup değerindedir: 2 m, LoRa 433 MHz (grup koridorları 432) ve LoRa 868 MHz (Komidoi 1296 için sayılıyor: 868 MHz'i aynı zamanda en yüksek frekansları getiriyor). Grup, LoRa'dan (aprs-deviceid listesini) ve yorumdaki frekanslardan veya kelimelerden geliyor; bir at sadece aynı grubun iki istasyonu arasında değer.

Hassas nokta aktif tekrarlayıcıdır: 500 km'de alınan bir sinyal iki veya üç filipeater retransmissions ile gelebilir. Bu nedenle, sadece iki gerçek ve ardışık verici arasındaysa:

- Yol izlenmelidir: yeniden yüklenen her filipeater adını koydu ve atlama bir tane ve bir sonraki arasında ölçülmelidir; isimsiz kullanılan bir alias (W IDE1 *, W IDE2 *, bölgesel alias) zinciri kırıyor ve bu da saymıyor;
- Gizli retransmisyonlar: program her kaynağın genellikle sorduğu yolu hatırlar (örneğin W IDE2 -2). Bir paket daha az bir sıçrama ile gelirse (W IDE2 - 1) ve hiçbir isim onu açıklamazsa, bir filipeater onu takip etmeden yeniden başlatır ve paket iptal edilir; kaynaklar asla saymadan görülmez;
- Yolun ortasında dikte: iki istasyon arasında, yolda daha fazla ya da daha az, son üç saatte aynı aktif grubun bir tanıtım salonu var, çünkü atlama silinmiş olabilir (özellikle de kazmak için).
- non-radio paketleri (qAC, qAX, TCPIP), bir IGate (üçüncü parti), HF ve farklı frekanslar, 1500 m (palloni, uçaklar) ve 1500 km'den fazla atlamak.

Haritaya Etkisi ( 1.3.4). Sadece minimum garantinin geçerli olduğu alan alanı çizer: her rotanın merkezi bir parçası etrafında ±60 km (uzunlukların% 15'inden% 85'inden fazlası), son zamanlarda istasyonlarda değil, konduit orada olmayabilir. Tüm koridorların grupları bir araya getirilir, tarafsız bir renkte (öpücü) ve yarı zamanlı ebeveyn: çakışmalar eklemez ve renk ölçekle karıştırmıyor (21 dB ve ötesinde). Etiketler (traits, ortalama mesafe, minimum kazanç) birbirleri ile taranıyor; koridorun rotalarını etiketle geçen fare ile geçiyor.

Bölgede ve sadece mevcut zamana yakın (±90 dakika), tahmin minimumun altına düşmez. Sebep şu: istasyonlar gerçekten geçerse, propagasyon orada, hava modeli ne diyorsa.

Çünkü kısa rotalar biraz değerindedir ( 1.3.4). 2 m, 300 ve 600 km arasında, iki iyi istasyon (bazı yüz watt, antenler, FT8 veya ABS) normal atmosferle bile hemen her gün, troposcatter için: iki paketteki havanın küçük düzensizlikleri tarafından dağınık sinyal. 500 km'de üç veya dört nokta, grubun her zamanki gibi çalıştığını, bir açılış olmadığını söylüyor: GEKO Magazine n. 69 ( IU3QEZ) SSB QRP'de yaklaşık 440 km, SSB'de 50 W ile 600 km (kaynaklar, 11 tur). Sadece 600-700 km troposcatter artık yeterli değildir ve bir dizi paralel bağlantı gerçekten bir konduit veya katmanı gösterir. 1.3.3'e kadar 450-600 km koridoru tahminleri 9-12 dB'ye yükseltti ve harita orada olmayan bir gelişme gösterdi.

Bu nedenle başlangıç değeri, rotaların ortalama mesafesiyle gider:

- 400 km'ye kadar: 3 dB;
- 400 ila 700 km: 3 ila 10 dB (500 km → 5,3 dB; 600 km → 7,7 dB);
- 700 ila 900 km: 10 ila 16 dB; 900 km boyunca 16 dB kalır.

7 dB'nin altında, ölçekin başlangıcı altında, minimum şu kalır: koridor etiketiyle çizilir, grubun çalıştığı görülüyor, ancak haritanın renkleri değişmez ( kutu ile 3-7 dB aynı zamanda gri dot gösterir). 7 dB'nin üzerinde değer ölçüldü, yarısı ve yarısı, bağımsız rota sayısı (3 vuruş → 0.25; 6 veya daha fazla → 1) ve medyan S/N ilişkileri ile (-20 dB → 0,5; 0 dB veya daha → 1; küme noktalarda olduğu gibi, → 0,75, 7 dB'den başlayarak. Örnekler: 500 km'de S/N olmadan 3 rota → 5,3 dB; 615 km → 7,7 dB; 900 km → 11.5 dB; 3 rotalar → 10,4 dB; 6 rotalar 0 dB 900 km → 16 dB.

En az mesafe 300 km'dir: 500 km'ye yükseltmek, aynı zamanda gerçek kısa menzilli açılışları da kaldıracaktır (örneğin Tyrrhenian'da 400 km, hava tahminleri ve çok güçlü sinyalleri tek başına fark edilen). I5MZY arşivinde, 22/11/2026'da 10:53 UTC'de, bu kural ve ışın filtre ile daha uzun koridorlar 615 km (7.7 dB) ve 540 km (6.3 dB); 1.3.3 kuralları ile 2175 km ve 16 dB'ye ulaştı.

1.3.3'e kadar, koridorun tüm dikdörtgeninde geçerliydi ve sadece mesafeye bağlıydı (10 dB'den 300 km'ye kadar 16 dB'den 900 km): Birlikte birçok uzun koridorla birlikte (örneğin 22/11/2026'da, on iki koridor neredeyse tüm Avrupa'yı 16 dB'ye kadar sürdü.

Gelir eğrileri artık «gocce» çizmiyor: Her iki yönde 220 km daha küçük adalar renkli kalır, ancak kapalı bir eğri olmadan, koridor onları doğrulamadığı sürece. Bu, "Ölümler tarafından tasarlanan" ile kapatıyor.

## 5. Seçmeli çizgiler ve baskı ve inversiyon ile karşılaştırma

Grafikler: 7, 10, 13 16, 18,5 ve 21 dB'deki çizgiler, yürüyüş meydanlarının yöntemiyle, dB'de etiketle incelendi.

Isobare: deniz seviyesindeki baskı her 4 aynı GFS'den, tahmin saatine, merkezlerle H ve L ile.

Termal inversiyon: 925 veya 850 hPa'da ne kadar hava ile kaplı eğriler, 2 m'de +3, +5 ve +8 °C'de olduğundan daha sıcaktır.

Kontroller altında bir çizgi, basınçla ve görünür alanda ters olan anlaşma verir: Pearson korelasyon katsayısı, -1 to +1. İndükler genellikle antisiklonlarda doğarlar (bu hava ısıları ve tersine çevirir), bu yüzden baskı ile olumlu bir anlaşma normaldir; ancak aynı antisiklopne içinde deniz, kıyı ve dağlar arasında çok fazla değişiklik yapar. Tek başına geri dönüş yeterli değildir: dalgaları sadece havanın üstündeyse de kurutma [5]. Hat şu an için, iki en iyinin renkli alanları açıklıyor. Aynı karşılaştırma kayıtta yazılır.

## 6. Potansiyel boru hatları: çift hesaplama

GFS seviyeleri 200 ila 1000 m arasında değişir: ince bir inversiyon yayılıyor ve model konduit görmüyor. İki seviye arasında N normal (40 N/km) aşıyorsa, hava 3 K/km altında stabil (termal gradient) ve yukarıdaki kuru (özellikle de 0,8'in altındaki özel nem oranı) ise, program iki şekilde tahmin eder:

- Hesaplama A: Tüm konsantre aşırı düşüş 50 m.
- Hesaplama B: Bu bölgenin boru hatlarının tipik kalınlığında dağıtılan aynı azalma, ITU-R P.453 [4] 'nun iklim haritalarından alındı.

Açıklık için ve en yüksek kapalı dalga için en düşük değer yapılır: bir prudent tahmindir.

ITU-R P.453 verileri © ITU ve kaynağı teşvik ederek kişisel ve ticari olmayan amaçlar için kullanılabilir, ancak yeniden dağıtılamaz: Bu program paketinde değilim. B'yi hesaplamak için, sadece P.453 haritalarını sekmedeki ITU'dan indirmek için: program, kullanım koşullarını göstermekten sonra, ITU sitesinden boru hatlarının arşivlerini indirin (elevated_and_surface_duct.zip) kullanıcının kişisel kullanımı için, Data/ P453 'daki dosyaları çıkarın ve yeniden başlatmadan sonra hesaplamaya dönün. Alternatif olarak, ITU-R Study Group 3 dijital ürün sayfası (itu.int, ITU-R Study Group 3, Digital products, voice P.453, Surface data and Elevated duct data) and put the Seven S_OCCUR. TXT, S_THICK.TXT, S_STREN. TXT, E_OCCUR.TXT, E_base.T Dosya olmadan program sadece hesaplama A kullanıyor ve sekmede ve kayıtta yazıyor. Açık A ve tipik yoğunluk P.453 arasındaki oran anomalidir: Bugünün boru hattının çoğu normalden daha güçlü. P453

## 7. İstasyonlar ve Locators

Ev sahibi 4, 6, 8 veya 10 karaktere sahip olabilir. 8 veya 10 karakterle pozisyon doğru ve toprak payı Copernicus GLO-30'dan geliyor: antenin yüksekliği zeminden yeterli. 4 veya 6 karakterle kare çok büyük: Denizdeki antenin kontenjanı yazmak daha iyidir; eğer 0 program karenin merkezindeki SRTM toprağı ile tahmini kalır ve sonuçlarda gösterir. Varsayılan yüksekliğin yerden 3 m olduğu için; deniz payı 0 ile SRTM toprağı artı yüksekliği kullanılır.

## 8. Toprak, difraksiyon ve troposcatter

Rotanın profili Copernicus GLO-30'da her 30 m (120 km genişliğinden daha fazla). eşdeğer Dünya Yarısı K = 157 / (157 + 345N), [6, 7] rotası boyunca GFS profillerinden ilk kilometrede N'nin geri çekilmesiyle.

İstasyonlara yakındır ( 1.3.2'den itibaren yazarın kriteri). P.452, dB açısı lineer olarak, mrad başına 0,573 dB öder: onlarca metreden birkaç yüz metre uzaklıktaki anten maliyetlerinden onlarca dB, gözlemlenenden çok daha fazlası. Bu nedenle her istasyondan 20 km içerisindeki rahatlamalar 6 dB'ye kadar ekleyebilir: onlar nedeniyle ufuk açısı en uzak rahatlamalar artı 10,5 mrad'ın bununla sınırlıdır. İstasyonunuz ve muhabiriniz için geçerlidir. Hat «Near rahatlamalar» birçok dB'nin nasıl alındığını söylüyor.

Arazi Kaynağı ( 1.3.2). Konpernicus DEM GLO-30 [3]: 1 ikinci yay, deniz 0 ve sahil açık. 1.3.1'e kadar, program Terrain Kaplamaları'nın 'skadi' kutularını kullandı. Bu meydanların yakınında, SSR'yi düşük çözünürlük denizli verilerle karıştırıyor ve adanın deniz üzerindeki payını yayıyor: Capri çevresindeki 80-100 m, kıyıdan 1 km uzaklıktadır. 21 Kasım 2026 IC8TEM, kuzeye doğru olmayan bir ufuk bildirdi; Copernicus ile nokta karşılaştırması bunu doğruladı (94 m at denizde Marina Grande vs.) Bu sahte ufuk, +2,6°, kıyı istasyonlarına 26 dB maliyeti. skadi panes sadece Copernicus yanıt vermediğinde bir rezerv olarak kalır. Copernicus çerçeveleri 15-30 MB ağırlığında ve bir kez indirmektedir; sadece denizler var değildir ve 0 değerindedir.

- Free space: Lfs = 92,44 + 20 log f(s) + 20 log d (km) [8].
- Diffraction: Bullington gerçek profil artı ITU-R P452-16 § 4.2 [6] delta-Bullington global arazi düzeltmesi, ITU-R P.526 [9] s global kaybı ile.
- İlk Fresnel bölgesi: yarı √ (λ·d1·d2/d); zeminin en az 0.6'sını terk ederse ücretsiz. Her engel için, [9] Bıçak Kurulu V ve J (VVV) parametresi gösterilir..
- Profilin tasarımında doğrudan yarıçap, zemin altında veya yeryüzünün eğriliği altında geçerse optik ve kırmızı görünümde yeşildir; kırmızı dotlar Fresnel 1. alanını işgal eden engellerdir. Denizde 0 m yükseklikte "kırmızı nokta" bir dağ değil: Korvature için yükselen deniz. Yarıda fare ile geçmek veya bir noktada açıklama ve değerler ile komik görünüyor.
- Antene yakın bir rahatlama (örneğin on derece bir ufuk için), diffraction ve troposcatter yüzlerce dB'ye zarar veriyor: 300 dB'nin üzerinde "> 300 dB" ve "yönetici engel", sinyal "< -230 dB" ve S/N oranı " -60 dB". Bu durumda, « Tavsiye edilen bitki» ve «Via Montene» bakmak tavsiye edilir.
- Troposcatter: ITU-R P.452-16 eq. 45 to 50% of the [6] time, with angular distance from the ufuks and N0 from the profile.
- Tropo olmadan: Difraksiyon ve troposcatter güçlerini bir araya getirin, asla özgür alanın altında değil.

## 8 bis. Troposcatter: dört model ve belirsizlik

P.452 programı, TropoLink [16] programı olduğu gibi diğer üç modelle troposcatter'ı hesaplar ve belirsizlik olarak farklarını gösterir:

- NBS TN101 § 9 [25] ( 1.3.5'ten), medyan: L = 30 log f - 20 log d + F (θd) - x2Q + H0 + Lgp. F (θd), ortak hacmin alt kısmındaki alanı ve 3-10 dB'nin kaybıyla karakterize eder. Ns, GFS profillerinin, etkili ufuklar ve yüksekliklerin Copernicus profilinden, Lgp = 0.07·e(0,055 (Gt+Gr) geliyor.) ITM'nin içte kullandığı aynı biçim, ancak iklimin bunun yerine bugün yenidenaktivite ile. 20 m'de antenlerle 144 MHz'deki düz bölümlerde testler: 150 ila 900 km arasında ITM'den 2-4 dB ve TN101 'nun 9.2b'sinde, H0 'ya sahip değil. F0 H0

- ITU-R P.617-5 [17] L = F + 22 log f + 35 log θ + 17 log d + 35 Lc, mm'de f ile, mrad'da,, d km, F = 0.18· N0 ·e(-hs/7,35) - 0.23·ΔN ve Lc = 0.07·e.(0,055 (Gt+Gr) N0 ve {{N genellikle ITU'nun iklim haritalarından alınır; Burada her saat GFS ( N0 ·e^ (-hs/7,35) ilk kilometredeki azalmayı takip eder, böylece troposcatter da zamanı takip eder.
- RSGB VHF/UHF Handbook Formula, DL2FZN [18]: L = Lfs + 24,44 + 4,283 ln f + 0.06656 d. Mesafeli eğim neredeyse PROPBER (0,067 dB/km) ile eşit.

Yaygın hacim ( 1.3.1): İki istasyonun yarıçapının geçtiği nokta, her biri ufuk açısına eğilimlidir, eşdeğer bir yarı yarıya = k·6371 km. TX'ten yarı yarıya z = hTX + x·θt + x2/(2·ae); Uzak x = (hRX - hTX + d^θr + d2/(2·ae) /.,. = d/ae +.t +.r. Aynı kotadaki düz ufuklar ve antenler ile, taban değer d2/(8·ae): yaklaşık 500 km, 9,4 km ila 800 km. Daha yüksek olan hacim, hava kuru ve nadir ve daha az yayılır: bu nedenle 800-1000 km troposcatter tükeniyor. Sonuçlar, temelin payı ve pozisyonu, reaktivite, GFS'nin o yükseklikteki nem ve Bray'nin eşdeğer mesafesi (QST, Kasım 1961): Her ekstra veya daha az ufuk, 111 km uzaklıktadır. Açıklama: IU3QEZ (GEKO Magazine n. 69).

Troposcatter hattı, diğer modeller en iyimser model ve en karamsar arasındaki farkın yarısını gösteriyor. Onlar sadece karşılaştırmalar: Tropo olmadan kaybı o P.452'dir.

## 8 ter. öğleden sonra rahatlamalar üzerine ( 1.3.4)

IK3XTV, Thiene ( JN55, 10 km'deki Alpler ile), yaz öğleden sonraları güney Almanya'da 400-500 km, güçlü sinyallerle, ancak hızlı ve derin bir yaşamla birlikte çalışır; yazar onlara "konvective hücrelere" bağlanır. Gözlem doğrulanır; programda kabul edilen açıklama, fiziksel olarak firmadır.

Öğleden sonra güneş havayı rahatlamalar ve limit tabaka 2-3 km'ye yükseltiyor. Aşağıdaki humid hava yukarıdaki kuru hava ile karşı karşıya: Refraktif indeksin ( troposcatter'ı yöneten boyut) maksimum. Bu üst rotanın ortak hacmine ulaştığında, 400-500 km yaklaşık 2-3 km, troposcatter güçlendirilir. Bu, öğleden sonra, yüksek basınç (sessiz gökyüzü, güçlü ısıtma) ve hızlı buharlık, difüzyon tipik ve düklerden değil. Convective hücreler lensler olarak hareket etmiyor: Türbübulence üretin ve sinyali yayan recast katmanının üst kısmındaki çalkantı budur.

Okuma tahminleri için eşitsizlikler:

- Yeryüzünde ilk öğleden sonra, düşük tavanlar ve tabakalar için en kötü zaman, ancak dağların ötesinde troposcatter için değil: iki şey ayrı okumalı;
- Program zaten hizmet eden iki veriye sahiptir, model tarafından sağlanan limit tabakasının yüksekliği ve rotanın ortak hacminin payı (sonunda "Common Volume" kayıt): Yakın olduklarında, öğleden sonra bir dağ tedavisi normal atmosferdeki hesaplamadan daha iyi gidebilir;
- 1.3.4'ten itibaren nokta hesaplama dikkate alınır: Eğer ortak hacim yeryüzünde ise, rotada en az 800 m'lik bir rahatlama vardır, güneş 800 m'den fazla ve limit tabakası 800 m'yi aşıyor. Hat «Troposcatter pomeridiano». Yol boyunca kar ile (en azından 10 cm ile zeminde modele göre, kar_Open-Meteo veya SNOD'nin NOMADS'den GFS'nin derinlemesine değişkeni) takviye uygulanmaz: Karda toprak soğuk kalır ve yeniden şekillendirilir. 3 dB'nin değeri geçicidir ve gerçek bağlantılarla kalibre edilmelidir; hava düzeltmeleri ile döner.

Kaynaklar: F. Egano IK3XTV (Radio Rivista e www.qsl.net/ik3xtv, «Propagazione sulle 2 metri», «Celle convettive.

## 9. Yol boyunca kurucu tabaka

sinyal hesaplamasının kalbidir. GFS profilleri her yaklaşık 25 km boyunca alınır. Her baskı katmanı için, aynı katmanın en uzun kısmı ardışık noktalarda bulunur; en az 40 m [10].

- Süreklilik: Hangi km'ye ve rotanın kesilmesine hangi kilometreden;
- Üst ve temel, ve m /km eğim;
- Zeminin içinde toprak: dağlar temelin üstünden nereye yükselir;
- Kapaklı toprak: Denizde konduit neredeyse her zaman, arazi [10] 'nun yayılmasına karar verir. Arazi rotanın% 10'unu aşıyorsa, kullanılan parçalar dünyanın sürekliliği ve kapsamı arasında en az.

Trope kazancı, kullanılan fraksiyonlar için katmanın kazanılmasıdır. Profillerin yarısından fazlası sub-refraktif ise kazanç negatiftir.

## 9 bis. Hava düzeltmeleri: istikrar, sübvansiyon, grev

1.3.0'ten itibaren, Segni'nin haritada görüldüğü üç meteorolojik bilgi de verir.

- Misme'nin İstikrarı [5, Bölüm 6]. Misme, ufkun ötesinde bir bileşik parametreye bağlanır: M = a· (ge - 40 + b·ΔW), N/km için = 0,5 dB, eşdeğer gradient ge ve termal stabilite 345W, 1 km havasını yükseltmek için çalışma. Program {{W = g. (. -.0)/ d0 dz yukarıda toprak ve 1 km arasında, the potansiyel sıcaklık ile; standart atmosfer yaklaşık 56 J/kg. Kazanana eklenen terim, standartın ötesinde J/kg için 0.07 dB, -4 ve +6 dB arasında sınırlı ve sadece konduit tuzaklarının grubun (eskiden) zaten Bean ve Dutton indeksinde olduğuna işaret etmektedir. Kalibrasyon geçicidir: oranlarla doğrulanmalıdır (paragraf 14).
- Subsidence ve yukarıhill hava [19]. Moler ve Holden, 1365 ve 9380 MHz'deki denize 125-305 km yollarında, havanın giderek artmakta olan hava ile ortalama 2.5 dB'nin en yüksek sinyalini buldu ve yaklaşık 20 dB'nin 1 saatliğine destek vermesi durumunda, havanın bir araya gelmesiyle 22 dB'nin azaltılmasıyla birlikte. GFS dikey hızı 850 ve 700 hPa verir. Her iki seviyede hava 1 cm /s'den daha fazla düşerse ve potansiyel reraktif indeks K'da keskin bir düşüş varsa (N, kuru ve sıcak havanın en iyi [5] 'dan ayrıldığını bildiren 1000 hPa'ya kadar, 1.5 dB eklenir; eğer hava 5 cm'den fazla yükselirse veya 5 cm'den fazla yükselirse 2.
- [20] rocco Sci. Sahara'dan sıcak ve kuru hava, doğu Akdeniz'den Gibraltar'a çok güçlü inverss verir. S işareti, rüzgarın en az 7 m/s ve sıcaklık en az 16 °C'den geldiğini gösteriyor. Bu sadece bir gösterge: kazanç profilin katmanlarına karar verir.

Bir konduit olmadan, grubun toplam kazancı 16 dB'yi geçmez, sarının sonu: düzeltmeler daha güçlü katmanlar daha belirgin hale getirir, ancak onları dükler (kırmızı ve mor tepelerde kalır).

- Persistence ( 1.3.1). En büyük açılışlar iyi yüksek basınçlı günler sonra gelir (VHF/UHF DX Kitabı; Biancomano ve Leyshon 1998, 12 saat kuralı; 29/12/2021-2/1/2022 ilk günden itibaren büyür). Her hücre için program, önceki 24 saatlerini en az 13 dB kazanır ve oranın 2 dB'ye kadar ekler, ancak kazanç zaten en az 10 dB.

Haritadaki işaretleri nasıl okunur (Değişim Hava İşaretleri; 1.3.2'den itibaren sembolü büyük bir çifttir ve bir işaret üzerinde fare ile bir çizgide hücrenin açıklaması ve değerleri ile geçer):

| Sign Sign Sign | Renkli Renk | Bu ne anlama geliyor? | Trope üzerindeki Etkisi |
|---|---|---|---|
|  ↓  | yeşil yeşil yeşil yeşil | sübvansiyon: hava damlaları (anticyclone), sıkıştırma ve kuru ısı için ısılar, tersine güçlendirir ve sağlam tutar | Olumlu, +1.5 dB |
|  ↑  | kırmızı kırmızı kırmızı kırmızı | hill hava (ön, düşük basınç, konveksiyon): düşük tabakaları karıştırır ve inversiyonları kırır ve dükler | Alamaz, 2 dB |
|  S  | portakal | Syrup: Sahara'dan sıcak ve kuru hava taze deniz havası | Olumlu ipucu, düzeltme yok: tabakalara karar verin |

Bir anticyclone altında yeşil. ile dolu bir alan zor bir açılışın klasik işaretidir; Kırmızı ^ oklar satır boyunca, tabakaların kırıldığını söylüyor, ancak hücrenin rengi hala iyi.

Hava düzeltmeleri kutusu bu düzeltmeleri 1.2 modeli görmek için çıkarır.

Geçerlilik (paragraf 14): 29 Aralık 2021 ve 2 Ocak 2022 arasında 593 bağlantıda, aynı rotalarla ve iki hafta sonra, ayrılık indeksi (0,5 = hiçbiri, 1 = mükemmel) 0,79 düzeltmeler ve 0,91 düzeltmeler olmadan. Aynı karşılaştırma, ana limiti gösteriyor: o günlerde Campania ve Liguria arasındaki işaretleri getiren düşük irtifada denizde konduit.

## 9 ter. News 1.3.2: İki tahmin, tavan, yüksek basınç, deniz, istasyonlar, ridges

prudent ve agresif bir tahmin. gradient'den elde edilen kazanç 0 dB'den normale kadar 10 dB'ye kadar süper-refksiyon eşine kadar uzanır. 1000-850 katmanında, prudent eşiği Bean ve Dutton (100 N/km), agresif eşik 79 N /km (k the 2; P.453 ilk 100 m) ile aynıdır; k50 = 157/(157 {{N) k Δ 2 (k = 2,5 N /km yaklaşık 94 N /km) Noktasal hesaplama, iki kayıp flanked gösterir; grubun çakıldığı bir konduit ile. Harita dikkatli kullanıyor.

2500 m. Yollar ve kanallar sadece zeminin üzerinde 2500 m'ye kadar arıyor: amatör radyoların antenleri onları kullanmaz. 1500 m ile geçerlilik 16 dB'nin üzerindeki bağlantıların% 4'ünü kaybetti, çünkü kış aylarında sübvansiyonlar genellikle 1500 ile 2500 m arasında.

Yüksek baskı genişletilmiştir. Etrafınızdaki 9 hücreden en az 7'si 1020 hPa'nın denize baskıyı azaltmış ve rüzgar düşük tabakalarda zayıftır (10 m/s, 850 hPa 8 m/s) çünkü yükselme hareketi hariç.

Deniz ağırlığı. Noktasal hesaplamada, katmanların kazanılması 0.8 + 0.4.1 ile çarpılır, μ1 ITU-R P.452 (eq. 51) en uzun sürekli arazinin uzunluğu: 1, açık deniz üzerinde, uzun topraklar ile küçük.

İstasyonlar ve yapıldı. Bir konduit bu tuzaklar grubun sadece çizginin bir kısmını kapsadığı zaman, her antenin payı, konduitin başında ve kaplanan bölümün sonunda karşılaştırılır; kazanç 0,5 + 0.5·√(e.(−ΔhTX/300) Neredeyse tüm rotada konduit ile § 10'un darbesidir.

Katman ve ridges. GFS'nin sınır tabakasının üst kısmında (en fazla sınır_kat_height modeli) rotanın sırt çantasını aşıyorsa (en yüksek 500 m'yi aşıyorsa, kat kırılmadan dağlara geçer: +1.5 dB. IT9GSF 'nun Sicilya'dan Almanya'ya olumlu Tyrrhenian ile olan açılışları gözlemliyor.

Denizdeki zemine yakın seviyeler. NOMADS kaynağı ile program ayrıca yüzey ısısını da indiriyor (Deniz SST), 80 m ısı ve nem 80 m ve zeminde 30-0 hPa. Deniz noktalarında (5 m altında model oranı) bu seviyeler, ilk baskı seviyesi altında, 100-200 m bu basınç seviyelerinin çözemediği 100-200 m. Yeryüzünde kullanılmaz: gece ve gün tabakaları henüz geçerli değildir. Open-Meteo sadece yüzey sıcaklığı verir.

Evaporation pipeline. SST'den, sıcaklık ve nemden 2 m'ye kadar, tahliyenin yüksekliğinin nötr logarithmik profili ile (Paulus ve Jeske modelinde olduğu gibi, stabilitenin düzeltilmesi olmadan): suyun saçına havanın SST'ye (yüzde 98'e kadar) doyduğu tahmin edilmektedir.

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

0 ile 40 m arasında sınırlı. 144 MHz'i tuzağa çıkarmak için yaklaşık 180 m (Kerr), sonra 144 MHz'de evaporasyonu biraz güçlendirecektir; 432 MHz'den, denizde düşük antenlerle, çok fazla (paragraf 11 bis). Parabolik denklemine girin ve satırda 1.3.2.

Günün zamanı (visit). Program her noktada güneşin yüksekliğini hesaplar. Yeryüzündeki günden güne, güneşle 15°'nin üzerinde ve sınır tabakası ile 5 m /s'in üzerinde, zemin düklerinin yeniden şekillendirildiğini uyarır; gece, 3 m/s ve limit tabakanın altında, serene gökyüzünün yeniden yapıldığı konusunda uyarır. Denizde değil. Şimdilik, sayılar değil: Aralık geçerliliği onları kalibre etmek için yeterince yaz günü yok. Bunlar yazarın önemli gördüğü iki noktadır, günlük gözlemden.

Normal profil CRPL (alternative). 40 N/km yerine normal düşüşün üstel referans atmosferi CRPL: N (h) = Ns·e (-c·h), −ΔN ile ilk km = 7,32·e^ (0,005577·N) (Bean ve Thayer 1959). ıslak hava ile (Ns yüksek) normal diktir ve kazanmak için daha güçlü bir gradient hizmet eder; yüksekliğe tatlıdır. Geçerlilik yardımcı olduğunu söyleyene kadar kötü çıkıyor.

## 9 quater. Contrast hava ve düşük denizler: Adriyatik ( 1.3.3)

Radyo dalgaları deniz yatağı hissetmez: hava ve su yüzeyi arasındaki kontrastı hissedin. Derinlik önemlidir, çünkü denizin sıcaklığının havanın bunu nasıl takip ettiğini karar verir.

Adriyatik düşük bir denizdir. Ortalama derinlik yaklaşık 250 m, ancak kuzey basin nadiren 100 m'yi aşıyor ve Trieste Körfezi 25 m altında kalıyor; sadece güneye 1200 m'den fazla. Birkaç gün içinde çok ince sıcak ve serin bir su katmanı, Tyrrhenian haftalar. Üç tipik durum vardır:

1. Bahar ve erken yaz, olumlu. Deniz hala soğuk, hatta Alp ve Po nehirlerinin taze suyu için; düz veya Balkanlardan sıcak ve kuru hava geliyor. Soğuk su üzerindeki sıcak hava, deniz üzerinde bir geri dönüş oluşturur (advection pipeline): İtalya, Slovenya ve Hırvatistan arasındaki açılışların klasik koşuludur.
2. Sonbahar ve kış boa, olumsuz. Soğuk bir deniz üzerinde soğuk hava: düşük tabaka kararsız, basınlar ve dük molalar haline gelir.
3. Bir boadan sonra, şans. Sadece deniz düşük olduğundan, bir boa birkaç gün içinde biraz derece serinliyor. Daha sonra sıcak hava ile bir anticyclone girerseniz, hava deniz kontrastı güçlüdür ve dük her zamankinden daha sağlamdır.

Adriyatik, Apennines ve Jakartai arasında, 200 km genişliğinden daha az kapalı bir kanaldır: deniz tabakası sınırlı kalır ve eksen boyunca rotalar (Puglia-Venezia-Trieste, yaklaşık 700 km) tüm deniz üzerinde kalır, en iyi koşul uzun bir giriş için.

Program ne hesaplamaktadır. Deniz noktalarında, hava düzeltmeleri ve kutu «Contrasto aria-mare»:

| Durum | Düzeltme |
| --- | --- |
| 925 hPa (veya 950) 4 KST üzerinde potansiyel sıcaklık | _ |
| 7 KST'nin ötesinde | +2 dB |
| Hava damp ile 925 hPa (% 80) | -0,5 dB bu bonuslarda |
| 1.5 °C veya daha fazla 72 saat önce, olumlu kontrast ile | _ |
| 2 °C (mixing) 2 m daha sıcak hava deniz | -1.5 dB |
| 4 °C daha sıcak deniz | -3 dB |

Toplam düzeltme -3 ve +3 dB arasında kalır ve konduit olmadan, kazanç sarı çatı altında kalır. Eşler geçicidir: Standart atmosferde, 925 hPa'ya potansiyel sıcaklık yaklaşık 2-3 K'ün denizinden geçer, sonra 4 K zaten bir ters işaret eder.

SST'nin hafızası. Modeller tahmin sırasında deniz sıcaklığını neredeyse sabit tutar. Son azalma, üç gün önceki analizlerden elde edilir (Open-Meteo, sadece yüzey sıcaklığı, sadece deniz hücreleri): Mevcut değerden en fazla 72 saat önce. Servis yanıt vermezse, hafıza bonusu uygulanmaz.

Sınırlar. Kıyının altındaki modellerin SST'si ve üst Adriyatik'te, örneğin Po'nun soğuk kalemi kötü bir şekilde çözülür. Batimetri hesaplamaya girmiyor: kaynaklar her zaman SST'ye veriyor, ki bu zaten deniz yatağının etkisini özetliyor. Hava deniz kontrastı, Misme istikrarının sonunda örtüşüyor (§ 9 bis); bu bonuslar küçük. Eşler, leke koridorları ve Adriyatik aracılığıyla QSO ile birlikte avlanacaktır.

## 10. Tüm rotada hareket etti

Eğer katman bandı en az% 80 oranında tuzağa düşürüyorsa ve istasyonlar göz önünde değil, konduitteki kaybı da hesaplanıyor: ücretsiz alan, artı antenlerin darbesi (3 dB eğer yukarıdaki veya aşağıdaysalar), artı değerleme = 5·10-5·ae Tropo olmadan kaybın daha az olması beklenen kaybı olur. [6]

Bir grup sadece dalga geçmese de konduit kalır.max = 2√2·(2/3)·√(0.10M· 10 -6) • (Kerr, eq. 17 [11]). Bu aynı katman 23 cm ve 70 cm'yi tuzağa düşürebilir, ancak 2 m değil. Bununla birlikte, sınır net değildir: Kerr, birkaç kez daha bile konduit çok acı çektiğini ve herburn ve VHF/UHF DX Kitabı "süre" kenarlar ve "forgive" dükleri konuşmaktadır. 1.3.1'ten itibaren program yumuşak bir konduit kullanır: λmax themax ve 3·λmax arasındaysa, kazanç yavaş 15 dB ( the = 3·λmax) tam konduit kazancına geçer ( the = λmax) Bu durumda dük renklere izin verilir. Yol boyunca giriş ve Meteo hattı, yumuşak kanalla noktaları gösterir.

## 10 bis. Via Monte

muhabir ufkun arkasında kapandığında, program antenleri işaret etmek için en iyi yön arıyor: Işıklı eğim sinyali radar hedefi olarak yayıyor. Her 5. ufkun çemberini yapar ( muhabirin etrafında ±15° hariç) ve her yönde aydınlatılmış ufk noktası 100 m ve 1 ile 60 km arasında. Hızlı bir tahmin (eski uzay, birazaktik dönüşüm ve 10 dB her seviyedeki ufuktan muhabire kadar) diğerinden en az 15° tercih eder; her biri için:

- 1, istasyon → rahatlama, ücretsiz alanda;
- 2, rahatlama → muhabir, üstten 10 m'den gelen rahatlama gibi hesaplandı, aynı tahminle (parabolik denklemi olmadan);
- Bistatik radar denklemi ile dönüşüm, the = the0·A, dB0 = -15 dB ( VHF/UHF) ve R1 kiriş tarafından aydınlatılan alan (maximum 20 km2):

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

"Fort Point" hattı, en iyi rotanın en azını gösterirse, doğrudan maliyetlerden daha az maliyeti varsa. 144 MHz'de, 20 km'de bir rahatlama ve 15°'den bir yagi ile, dağ yolu sadece 2 rotadan daha fazla maliyeti vardır: sadece komşu engeller tarafından kapalı olduğunda gerçekleşir. Tahmin ağırdır (±10 dB), özellikle.0 için.


Dağa kaybı olmadan (parlama, 1.3.2). «Via Montesen perdite up al Drupal» kutusu ile istasyon arasındaki düşüş ve difraksiyon noktası null olarak kabul edilir: istasyonu üste getirmek gibidir (aynı güç, anten ve kablo). Ufuk turu daha sonra muhabire karşı en özgür ufukla işaret eder (en yüksek) ve sonucun rotayı muhabire, sinyal ve S/N ile, diffüzyon ile birlikte gösterir. Bu iyimser bir limittir: Dağ iyi sinyal getirdiğinde ya da taşınabilir nereye gideceğinizi söyleyin. Her nokta on-character ile verilir.

## 10 ter. Uzak bir konduite giriş

Bu tuzakların grubun şimdiye kadar başladığını, istasyonun ufkunun ötesinde, sinyal gelmesi gerekir. Program başlangıç noktasına sinyal getiriyor, konduitin ortasında, diffraction için: Copernicus profili GloO-30 en yüksek Fresnel parametresi ile kenar aramaktadır (ITU-R P.526), eşdeğer arazinin şişliği ile,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

Üstin eğriliği nedeniyle daha fazla değer (parçalı engel, P.526-16 § 4.2), R yarıçak, Copernicus GLO-30 toprağından kenardan ±1 km'ye kadar tahmin etti.

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

( m·n > 4 tavsiyenin 34b'ı oluşturur). Barsis (NBS, 1963) bir dağın ötesinde üç uzun rota üzerinde ölçüldü (Renk, Alaska, Avrupa) ortalama 10-20 dB, Fresnel-Kirchhoff'un ideal kenarlarından daha yüksektir: yuvarlak bir çok şey açıklıyor (Barsis ve Kirby, 1961: Pikes Peak yaklaşık 11.5 km). Sonuç, düzgün toprakta difraksiyonla karşılaştırır; en kötüsüdür. O zaman sinyal konduit'te seyahat eder: tüm rotada ücretsiz alan, P.452'nin konduit ve muhabirin darbesi (3 dB içeride, 1 dB her 10 m. Bu kaybı beklenen kayıptan daha düşükse, mekanizma ("kondüktör) olur. Bu, yazarın İtalyan coğrafyasına yatkınlığı: hattın altında yakın bir engelle, v negatif ve giriş maliyetleri az; dorsal ön maliyetlerle 15-30 dB.

## 10 quater. ITM (Longley-Rice) ve dükten çıktı

1.3.2'den itibaren program, NTIA/ITS (Longley ve Rice, ESSA ERL 79-ITS 67, 1968; Hufford, Longley ve Kissick, NTIA 82-100, 1982), ABD hükümetinin resmi koduyla C#'ye tercüme edilmiştir. Çeviri orijinal olarak aynı sayıları verir: otomatik derleme, her seferinde NTIA C++ kodu ile beş test davasıyla karşılaştırır (aşağıdaki 0.01 dB).

ITM, normal atmosferdeki kaybı gerçek profil Copernicus GLO-30'da hesaplar: zeminden gelen yansıma ile görüldü, diffraction (spigol ve s global arazi {{h, bu, minimum karelerin etrafındaki kotaların yüzde 10'u arasındaki fark.) ve troposcatter. Ground reaktivite rotanın ortalama kısmına indirgenir, $N_s = N_0\,e^{-h/9460}$ ve eşdeğer arazinin eğriliğini verir,

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

Program, seçilen rotanın radyo iklimini § 10 sekste kullanır ( 1.3.4'e kadar her zaman ılıman deniz, N0 320 ile denizde), N0 ile yerdeki ortalama reaktiviteden tanır (soil εr 15,, 0.005 S/m; deniz ther 81, the 5 S/m) Bu, kaybın zamanın% 50'sinde aştığını ve en iyi zamanın% 10 ve% 1'inde sahip olduğunuzu gösteriyor. ITM, kanalları modellemez: zamanın üzerindeki değişkenliği iklim tarafından istatistikseldir. Bir sonraki P.452'ye ikinci bir hesaplama olarak hizmet eder; ikisi arasındaki fark bir belirsizlik fikri verir.

İkinci kullanım, karşılık gelenden önce sona eren bir konduitten sonra son bir sorundur. Çıkış noktasında, bir sanal anten konduit üzerinden yarıya yerleştirilir ve ITM, zamanın% 50'sinde ücretsiz alana kadar yol hesaplar. Toplam kayıptır

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,	heta_{condotto} + A_{ITM}(d_{uscita} 	o RX)
```

A_TX antenin darbesidir (3 dB içeride, 1 dB her 10 m dışarıda) eğer konduit yakınsa, aksi takdirde § 10 ter giriş. Yazarın kendisine verdiği sınırlamadır: katman artısının sürekliliği, çoğu, ikametsizlik. Beklenilen kayıptan daha düşükse, mekanizma (kondüktör ve son yol ITM) olur.

## 10 quinquies. Derinleşme: adalar, straits ve Earth-sea sonsuzluk

Napoli Körfezi'nden bir örnek (Anacapri, JN70CN) batı Akdeniz'e doğru yolların adaları geçtiği gösterir:

| Hedef | Azimut | Nereden geçti adalar | Obstacle |
| --- | --- | --- | --- |
| Barcelona Barcelona |  279°  | Sardinia'nın kuzeyinde (Gallura) 41,05° N, 420 ve 490 km arasında | Bocche di Bonifacio'nun yaklaşık 30 km güneyinde; Limbara (1359 m) hattın yaklaşık 20 km'si hattından |
| Bocche di Bonifacio |  282°  | 41,3° N | Yaklaşık 11 km |
| Marsilya |  296°  | Central Corsica 42,2° N | 2700 m'e kadar dağlar: 2 km'nin altında bir konduit |
| Valencia, Balearics |  260–265°  | Central Sardinia | İç rahatlamalar |

Barcelona için 279° yerine 282°'ye işaret etmek yeterlidir çünkü eksen ağızlardan geçer: 3rd, normal bir yaginin içinde.

Ağızlar bir strait midir? Düşük kanallar için, birkaç yüz metre altında, evet: adalar duvarlı ve dünyanın üstünde, gün ısıtma geri dönüşümleri kırıyor. strait'in kendisi yeterince geniştir: yaklaşık 11 km, ilk Fresnel bölgesine karşı, 144 MHz'de 1000 km yol kenarından daha az. Sorun havadır. Ağızlar rüzgar için eğlencelidir: Alt tabaka hızlandı ve alt tabaka kaldırıldı. Genellikle geçiş geometri olarak açıktır, ancak atmosfer olarak rahatsız edilir. Sürekli bir anticyclone ve küçük rüzgar ile bunun yerine iyi geçiş olabilir.

Earth-sea disparity. sayıyorlar. Tyrrhenian sahilinde bir konduit kesintiye uğradı: sinyal konduitten çıktı (sonradan sonra bölüm, § 10 quater) ve Balearics denizinde başka bir konduit geri dönmeli. Yüksek irtifada, 1500 m'den fazla, Gallura'yı fark etmeden geçer; merkezi Corsica uygulanmaz.

Uygulamada: Yol bir dağ adasına geçerse, serbest denize doğru bir düzeltme derecesi deneyin, ancak darda güçlü rüzgarla yolda saymayın.

## 10 seks. TN101 radyo iklimi ve zaman değişkenliği ( 1.3.5)

TN101 § 10 [25], ufkun ötesinde bir rotanın kaybının medyan etrafında uzun süre değiştiğini açıklar: Y (q) işlevi en iyi zamanın% 80'inde kaç dB'nin düştüğünü söyler. ITM, yedi iklim için bu eğrileri içeriyor. İklim çok önemli: ılıman deniz ikliminde, 144 MHz'de, kıtasal iklimdeki araziden çok daha fazla dB kazandınız, çünkü dükler daha sık.

Program artık iklimi düzeltmez: Bunu ortalama noktadan her tedavi için seçer, bu yüzden tüm dünyada çalışır ve sadece Avrupa'da değil. Open-Meteo [26] 'nun günlük ERA5 reaniyaliz değerlerinin üç yılı (sıcak, nem ve toprak basıncı, yağmur) ortalama noktanın hücresi için, bir kez sadece; değerler Data/CLIMA'da kalır. Toprak restorasyonu

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

gün, o zaman aylık ortalamalar, ortalama nem ve yıllık yağmur arasında yıllık ortalama ve gezi. Kurallar TN101 § 10.2 iklimin tanımlarını takip eder; bir iklim ve diğer arasındaki sınırlar yazarın kriteridir:

- 60° entitude üzerinde: sıtma kıtası ( TN101 kutup iklimi için eğrileri yoktur);
- Yılda 250 mm'den daha az yağmur ve ortalama nem% 50: Çöl (Ns tipik 280);
- 20° içinde equator, En az 350 ve 30'a kadar Ns of at: equatorial (Ns tipik 360);
- 32° ve en az 60 gezi: subtropikal kıta, bu monsoons (tipik gezi 60-100);
- 10° ve 32° arasında En az 340 Ns: subtropical deniz (Ns tipik 370);
- Her iki ufk denizde, 20°'den fazla: Denize üşüttü ( TN101 ’nun tanımıdır).
- Denizin yakınında 30'a kadar gezi (en azından% 75 olan rota veya ortalama nem üzerinde denizler): Dünya'da deniz muz;
- Aksi halde kıta (tipik gezi 20–40).

ITM için N0, deniz seviyesine ortalama azalır, N0 = Ns·e^ (h/9460). Ağ olmadan tek başına ve deniz fraksiyonu ile bir rezerv kuralı kullanırsınız ve çizgi bunu söylüyor.

Verilerle Testler ERA5 2023-2025: Capri Ns 339 gezi 45 (kahkadarda, deniz üzerinde her iki ufkun denizde olduğu gibi) deniz üzerine ısıtıldı; Londra 327 ve 16 Viyana 319 ve 24 Abidjan 381 ve 11 (paratoryel) Şili'de Antofagasta 341 ve 19 (subtropical koca: Atacama sahili, Pasifik'in neredeyse düşük geri dönüşü ile), Calama 232 zeminde, 2257 m, 14 mm yağmur (düşün), Valparaíso 331 ve 11 (süşük deniz)

"Radyo iklimi ( TN101)" hattı iklimi gösteriyor, neden Ns gezinin yarısı ve Y: zamanın% 10 ve% 1'inde kaç tane dB kazanıyor ve %90'da kaç kişi kaybediyor. Uzun vadeli istatistiklerdir ve tahminlerden ayrı kalır: beklenen kayıp bugün GFS profilleriyle takip eder ve iki şeyi aynı anormal yayılımı iki katına çıkaracaktır.

## 11. ITU-R P.452 ile Karşılaştırma

P.452 hattı, CSIRO [10] çalışmasında olduğu gibi ITU metodunu geçerlidir. Bu sadece bir karşılaştırmadır: beklenen kayıp 9. paragrafın altında kalır. [6]

## 11 bis. Parabolik denklemi ( 1.3.1)

Parabolik Equation Line dördüncü bir hesaplamadır, sadece karşılaştırma, GFS'nin dörtlü versiyonu için profesyonel programlar olarak yapılır; M. Lytaev, MIT lisansının dalgalandırılması: katmanlardan elde etmek yerine, her mesafeye daha yakın olan noktadan alınmıştır. GFS seviyeleri, potansiyel düklerin tahmin edilen atlarını ekliyor (A, 50 m'yi yarı tabakada hesaplayın). Toprak Copernicus GLO-30 adımlara girer (kahkanın bittiği yer altında), toprak iletkendir ve antenin kirişi, elde edilen açılışla Gaussian'dır. Hesaplama konduits'i görür, kaybedenleri, antenin difraksiyonunu ve darbesini kaybeder; Troposcatter'ı göremez, bu da ayrı eklenir. Sonraki, « profil cheque»: Beklenilen profille standart atmosferle aynı.

1.3.2:

- propagator, özgür alanın tam aşamasıyla geniş bir açıdadır:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- Denizdeki alt ve sıfır yükseklikteki sahil düz, rotadaki Fresnel katsayısı ile yansıtır, thec = εr jr = 81 ve σ = 5 S/m; ülke 13 ve 0.006 S/m)

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- Denizin kabalığı Miller ve Brown'a göre tutarlı yansımayı azaltır, rüzgardan 10 m'ye kadar dalgaların etkili yüksekliği ile (σh = 0,0051·U2):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- Denizde Profil M, nötr logaritmik profili ile buharlaştırmayı içerir:

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

Denizdeki prototiple (10 m antenle yapılan testler, 100-300 km standart atmosferde kazanır): δ = 10–30 m homeaporation duct, 1-3 dB'ye 144 MHz, 8–37 dB'ye 432 MHz ve 21-66 dB'ye 1296 MHz verir. Yatay kutuplaşmada 144 MHz, deniz neredeyse bir metal olarak yansıtır (|.| 0,99'dan fazla bir dereceye kadar) ve kabalık az sayılır: yeni alt birkaç onuncu dB değiştirir; 432 MHz'den daha fazlası.

Limitler: Dünya üzerindeki dikey profil GFS seviyelerinin o kadar kalır, bu yüzden ince kanallar görünmez kalır; adımlarla ilgili zemin yaklaşıklanır; kiriş 6'da kalır.

## 12. antenler, sinyal ve gürültü

- Yagi DL6WU: 9.5 + 7.8·log10 (boom/boo) dBd (2.35 dB for double boom, DL6WU önlemleri VHF Communications 3/1982), artı [12] dBi için 2.15.
- EIRP = güç - kablo + TX kazanır.
- Signal aldı = EIRP - beklenen kayıplar + RX - kablo RX. S-meter IARU: S9 = -93 dBm, birim başına 6 dB.
- ITU-R P.372 [13]: galaktik (Fa = 52 - 23 log f) çevreye göre daha yapay (şehir, konut, kırsal, sessiz kırsal) veya el yazılı bir anten sıcaklığı.
- Tsys = Ta + Tpre + [290(Lc - 1) + 290 (Fr - 1) •Lc] / Gpre to the terminals of the anten (Friis formülü). 1.3.5'ten itibaren alıcı istasyonu her zaman antende bir önyükleme vardır, sabit parametre: gürültü numarası 0.8 dB (Tpre = 59 K) ve 20 dB kazanır. Arkada kablo (loss Lc) ve radyo, temel gürültü numarası Fr 6 dB. Kablo ve radyo girişinin 1 dB'si yaklaşık 1160 K'dan 12 K'ye düşer: gürültü gökyüzüne, çevreye ve ön plana karar verir. 20 dB'nin kazancı, program tarafından seçilen tipik bir değerdir.
- SSB, Zheng ve FT8 'da her şekilde, eşiğine kıyasla.
- Dönemde Olasılık ( 1.3.2): Saat içindeki evsizlik, beklenen ortalama saat değeri etrafında S/N normal bir dağıtımdır. Bray (QST, Kasım 1961, p. 39, Norton 1959 eğrileri üzerinde), yaklaşık 100 mil boyunca 135 dB'yi bir 'solid' bağlantısının 7 dB'yi aldığı ortalamanın altında yaklaşık 7 dB'yi alır. ±7 dB ±2σ: dB = 3,4 dB aşağıdaki mesafeye göre. Bir dönemin yolun eşini aştı olasılığı

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  Tİ normal dağıtım fonksiyonu: marj 0 dB =% 50, +3,4 dB =% 84, +7 dB (solid' marjı Bray) = 98. Modelin belirsizliğini içermez, bu daha büyük.
- Digital Ways: S/N in 2500 Hz, daha önce bilgi olmadan% 50 oranında ölçülmüş eşlere kıyasla (AP). FT8 -21 dB ve FT4 -17,5 dB beyaz gürültüde; mekanizmasın tropos, Q65 -30B dB, FT8 dB,6 dB ve FT4 -15,2 dB [21]. Q65 -15C. Uçaklarda, Doppler Q65 tonlarını aşabilir -60A (1,7 Hz): 30B veya 15C. JT65A -25 dB [22]. The decoding AP ( zaten bilinen veya QSO’nun sonu) FT8 için 2 dB ekstra verir ve Q65 için 2.6 dB'ye girer; eşiğine girmiyor çünkü FT8 için sık sık okunan tüm dB24 dB değerleri geçerlidir. Q65 [24]

## 13. Yanıt ve veri süreleri

Her hesaplama kendi zamanını ölçer: GFS profilleri (network, analiz, açık-Meteo limitleri arasındaki 20 s zorunlu molaları), Copernicus GloO-30 arazisi (her biri, sadece ilk kez 15-30 MB ile birlikte), hesaplama. Yanıt Zaman çizgisi onları özetliyor; sıralarda TEMPI ile başlıyor. Kafei yükseltmede, kayıt aynı zamanda mevcut olduğunda ve verilerin ne kadar yaşlı olduğunu rapor eder.

1.3.2'den itibaren ICON-2I'yi, Ajans ItaliaMeteo ve Arpae'yi günde 2.2 km uzaklıkta, Open-Meteo (model italia_meteo_arpae_icon_2i, günde iki yarış) seçebilirsiniz. Sadece İtalya ve yakın denizleri kapsar ve GFS'nin daha az düşük seviyesine sahiptir (1000, 925, 850, 700, 500 hPa): yüksek irtifadaki konduits için GFS'den daha iyi değildir, ancak iyi denizler, adalar ve denizler 2 m'de açıklar. Bir modelin sahip olmadığı değişkenler (strate limit, yüzey sıcaklığı...) kendilerini istekten uzaklaştırır. 1.3.2'den itibaren otomatik kaynak sadece NOMADS kullanır; Open-Meteo, NOMADS'nin yanıt vermediği ve tarihsel durumlar için hizmet eder. "Kendi başına" kutusuyla (önemli olarak aydınlatılmış) tahminler, giriş olmadan indirilecektir: NOMADS'da her yeni GFS yarışında (ortalamadan yaklaşık 5 saat sonra) ve görünür alan ağdan daha fazlası için dışarı çıkarıldığında; en fazla 15 dakika bir denemede, asla tarihi bir durumda.

Open-Meteo'daki istekler, her internet adresi için saatte ve günde sınırları vardır. İndirilen profiller disk üzerinde kaydedilir (Dati/cache-gfs) her bir vuruş ve model için: her 6 saatte değişen aynı yarışla, puanlar zaten tekrar sormuyor. NOMADS ve Open-Meteo'dan başladığınız otomatik kaynak ile sadece eksik noktalar için hizmet eder; zaman sınırı veya günlük duraklarda, tekrar denemek için zaman sınırı veya günlük duraklar. Tarihsel vakalar önbellek değildir.

## 14. Modelin Geçerliliği

Tahmin gerçek bağlantılarla karşılaştırılmalıdır. Hesaplama girişi her sayıyı tutar; program arşivi reklamlar ve dekodings tutar. 432 ve 1296 MHz için, noktaların birkaçı nerede, operatörlerden raporlara ihtiyaç duyuyorlar: bağlantıyı kim yaptı, ne zaman sinyalle. Ölçümlenen sinyal ile beklenen birçok saat ile karşılaştırıldığında, eşlerin hareket etmesi gerektiğini söylüyor.

## Kaynaklar

1. NOAA/NCEP, Global Tahmin Sistemi (GFS), günde 4 yarış.
2. Open-Meteo, tahmin API (open-meteo.com), gfs_seamless model, basınç seviyelerindeki değişkenler ve metadata yarış.
3. Copernicus DEM GLO-30 (ESA; WorldDEM © DLR e.V. 2010-2014 e © Airbus Savunma ve Uzay GmbH 2014-2018, Avrupa Birliği ve ESA tarafından Copernicus'un bir parçası olarak tedarik edildi), Amazon Web Hizmetlerinin açık kaydı üzerine GeoTIFF çerçeveleri. Rezerv: NASA/USGS Copernicus GloO-30 in Terrain Kaplamaları "siyonel" AWS.
4. ITU-R P.453, Radyo reraktif indeks: formülü ve refraktivite tarihi, dijital kanal haritaları (Study Group 3, © ITU, kişisel ve ticari olmayan kullanım).
5. B. R. Bean, E. J. Dutton, Radio Meteoroloji, NBS Monograf 92, 1966: Masa 6.5 ve § 4.4.
6. ITU-R P.452-16, Prediction prosedürleri Dünya'nın yüzeyindeki frekanslarda 0.1 GHz: § 4.2 (diff), eq. 45 (troposcatter), § 4.5 ve eq. 49 (conducted).
7. ITU-R P.834, tropospheric reffraksiyonunun radyo dalgası yayılımına Etkileri.
8. ITU-R P.525, serbest uzayın ölçülmesi.
9. ITU-R P.526, difraksiyon tarafından yönlendirilme: eq. 31 (cutcheon) ve § 3 (üresel toprak).
10. B. Indermuehle, H. Suzuki, Tropospheric Ducting Prediction based on GFS Model Data at Inyarrimanha Ilgari Bundara, CSIRO Murchison Radio-astronomy Gözlemevi Yayınları, 2026 (arXiv 2608.12861).
11. D. E. Kerr, Kısa Radyo Dalgalarının yayılımı, MIT Radyasyon Laboratuvar Serisi vol. 13, 1951: eq. 17 e 18.
12. G. Hoch DL6WU, «Extremely Long Yagi Antennas», VHF Communications 3/1982, pp. 130 ve seq. 1 (measured 432 ve 1296 MHz patlamanın uzunluğuna göre).
13. ITU-R P.372, Radyo gürültüsü: Çevre için galaktik ve yapay gürültü.
14. NOAA/NCEP, NOMADS, GRIB GFS 0.25° zamanı (nomads.ncep.noa.gov); WMO G RIB2 format, mekansal farklılıklarla karmaşık.
15. Open-Meteo, Historical Tahmin API (model tahmin arşivi 2021'den bu yana).
16. LukiBox, TropoLink, P.617, NBS TN101 ve ITM flanked (GitHub, Apache 2.0 lisansı).
17. ITU-R P.617-5, Trans-horizon radyo-relay sistemlerinin tasarımı için gerekli tahmin teknikleri ve veriler 2019.
18. RSGB, VHF/UHF Handbook; DL2FZN uygulama (vei-f/troposcatter on GitHub, 2013).
19. W. F. Moler, D. B. Holden, «Tropospheric saç yayılımı ve atmosferik dolaşımlar», NBS 64D Dergisi, 1960, doi 10.6028/jres.064d.014: ufkun ötesinde sinyal (1365 ve 9380 MHz, deniz üzerinde) ve havanın dikey hareketi.
20. ARRL Handbook 2022 (99a ed.), § 19.4.3-4.4: hava ve troposical propagation.
21. S. Franke, B. Somerville, J. Taylor, FT4 ve FT8 İletişim Protokolü, QEX Temmuz / Ağustos 2020.
22. W3SZ, yavaş dijital modların eşiği ( JT65, JT9, QRA64) ve modellemeye göre mod seçimi, 2019.
23. QST, "The World Above 50 MHz", Kasım 2022: Manitoba ve Kansas/Missouri arasında 14-15 Ağustos 2022'nin açılışı.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Quick-Start Guide to Q65, 3 Nisan 2021 (wsjt.sourceforge.io).
25. P. L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis, Tropospheric İletişim Devreleri, NBS Technical Note 101, dergi 1967: § 9 (troposcatter, fonksiyon F (θd), H0), § 10 ve Ek III (zaman boyunca transistatif iklimler ve varyanabilite).
26. Open-Meteo, Historical Air API (ECMWF ERA5 incelemesi 1940 yılından beri; Copernicus Climate Change Service, CC BY 4.0 lisansı).
