# Manual forecast

> Terjemahan otomatis oleh Italia: dalam kasus keraguan layak panduan Italia.

Tab Forecast menghitung penyebaran tropopsic dengan sendirinya: peta indeks pada area yang terlihat dan sinyal yang disediakan antara stasiun Anda dan yang sesuai. Manual ini menjelaskan setiap data, bagaimana dihitung dan darimana asalnya. Angka-angka dalam kurung persegi mengacu ke sumber di bagian bawah; dalam program setiap data hasil memiliki komik (simbollo i) dengan sumber yang sama dan referensi ke paragraf.

## 1. Apa yang dia lakukan dan apa yang dia lakukan

Prediksi dari model GFS [1, 2] vertikal suhu dan profil kelembaban menghasilkan reaktivitas lapisan udara dengan lapisan dan memperkirakan berapa banyak sinyal mendapatkan dibandingkan dengan atmosfer normal. Pada rute itu menambahkan tanah nyata (Copernicus GLO-30 [3]), difraksi, troposcatter, saluran dan penerima kebisingan.

Ini bukan ukuran: ini adalah perkiraan yang dibuat pada model cuaca dengan tingkat jauh ratusan meter. Ini berfungsi untuk membandingkan situasi (hari ini lebih baik dari kemarin, 70 cm lebih baik dari 2 m, ini memperlakukan lebih baik daripada itu), tidak menjamin koneksi. Paragraf 14 menjelaskan bagaimana mengumpulkan bukti untuk memvalidasinya.


> "Prograsinya tidak dapat digeneralisasi, namun dipelajari dalam kaitannya dengan morfologi lokal."
> - Volker Grassmann, DF5AI, dilaporkan oleh Flavio Egano, IK3XTV (Propagasi pada 2 meter)

Ini adalah prinsip yang memandu perhitungan titik titik titik titik: setiap rute dihitung di tanah, dengan horizon, laut dan antenanya; peta memberikan gambaran umum, memperlakukan jawaban.

## 2. Perintah bahasa

- 144, 432 atau 1296 MHz. Peta dan perhitungan menggunakan band terpilih.
- Sekarang: kursor bergerak perkiraan dari sekarang sampai 72 jam ke depan, dalam waktu 3 jam.
- Perbarui perkiraan (zona terlihat): unduh profil GFS pada kisi-kisi hingga 240 poin yang meliputi peta yang terlihat. Pitch kisi (0,5 ° atau lebih) tergantung pada pembesaran.
- Stop: mengganggu download atau perhitungan.
- Detail untuk ditampilkan (panel kanan dari peta): tiga tingkat, eskripsi, normal dan Lengkap, nyalakan sekelompok kotak; tiap kotak kemudian dapat berubah dengan tangan. Kotak-kotak dalam tiga kelompok:
  - Pada peta: Isoinee del Gain, juga 3-7 dB (traktor), isobare, inversi termal, cuaca tanda-tanda S Genevieve, peningkatan hubungan / tekanan, zona tampilan langsung (paragraf 4, 5, 7 dan 9 bis). Warna selalu tinggal.
  - Dalam perhitungan sinyal: semua baris dari perhitungan, profil bumi rata, catatan perhitungan dan catatan berkas yang rinci.
  - Koreksi dan data langsung: koreksi cuaca (Misme, subsidence), kontras udara, rata-rata dengan bintik-bintik (koridor), APRS 2 m melompat dan LoRa, di luar gunung tanpa kerugian (paragraf 4 bis, 9 bis, 9 quater dan 10 bis).
  - Esensial hanya berisi isolasi, cuaca, dan media koreksi dengan bintik-bintik; normal menambahkan isobar, inversi, tanda-tanda cuaca, pandangan langsung, bumi datar, laut, dan APRS; sepenuhnya menyalakannya. Tingkat hanya mengubah apa yang Anda lihat dan apa yang masuk: perhitungan tetap sama.
- Sumber kasus data dan sejarah: dari mana profil datang dan hari apa Anda melihat (paragraf 3 bis dan 3 meter).
- Stasiun saya dan koresponden: lokasi, antena, daya, kabel, kebisingan (paragraf 7 dan 12).
- Hitung sinyal dan cakupan dalam optik: dua perhitungan di tanah (paragraf 8-13).
- Daftar perhitungan: setiap langkah dengan angka yang dipakai, termasuk dalam berkas Data / forecast- calcoli.log.

## 3. Dari data GFS ke profil

Untuk setiap titik dari grid dan setiap jam kita membaca tekanan ke tanah, suhu dan kelembaban pada 2 m, angin pada 10 m, tinggi lapisan batas, CAPE dan suhu dan kelembaban relatif terhadap 1000, 975, 925, 950, 850, 800, 600, dan 500 hPa. Dari 1.3.0 Anda juga dapat membaca angin ke 850 hPa dan kecepatan vertikal ke 850 dan 700 hbis.

Tingkat dihitung dengan rumus ipsometric dan suhu virtual; tingkat di bawah tanah langka. Untuk setiap tingkat:

- N = 77,6 / T [4]
- M = N + 157, dengan h di km: reaktivitas yang diubah. Dimana M naik ada saluran.

## 3 bis. Sumber data

Menu Sumber Data memilih dari mana profil berasal:

- Otomatis (disarankan): GFS pertama dari NOMADS [14], layanan NOAA yang mendistribusikan model: untuk setiap saat program meminta dua cutouts dari daerah dalam format G RIB2 dan membacanya sendiri; NOMADS tidak memiliki batas harian, hanya satu per menit bahwa program menghormati. Dengan grid lebar (dari 0.5 ° ke atas) ia menggunakan 0.5 ° atau berkas 1 setiap 3 jam, jauh lebih ringan. Poin yang tak diberikan NOMADS, atau semua jika NOMADS tak merespon, tanyakan Open- Meteo [2]. Kedua sumber memberikan model yang sama dan ras yang sama, sehingga Anda dapat mencampur dalam grid yang sama.
- GFS dari Open- Meteo: hanya Open- Meteo, seperti dalam 1.2 (pada batas per menit yang diharapkan).
- GFS dari NOAA NOMADS: hanya NOMADS. Lebih lambat untuk mengunduh (dua berkas per jam, setengah detik antara satu permintaan dan yang lain tidak melebihi batas NOAA), tetapi tidak mengkonsumsi batas Open- Meteo.
- ICON- EU (DWD) dan ECMWF IFS 0.25 °, dari Open- Meteo: model selain GFS, yang diterapkan di seluruh grid dan untuk rute. Mereka tidak bercampur dengan GFS: membandingkan dua model pada saat yang sama adalah cara sederhana untuk melihat bagaimana ramalan cuaca tidak pasti. ICON- EU hanya meliputi Eropa; ECMWF tidak memiliki semua tingkat tekanan GFS (lompatan tingkat yang hilang) dan mungkin tidak memiliki lapisan limit.

Sumber yang digunakan ditulis dalam baris status dan dalam baris pertama hasil (sumber data), dengan berapa banyak poin yang datang dari setiap layanan dan berapa banyak dari cache. Cache disk dipisahkan oleh model.

## 3 meter. Kasus historis

Sejarah Menu kasus mereview pembukaan yang didokumentasikan dengan arsip Open- Meteo [15] prakiraan GFS: peta bergerak ke area kasus ini, band menetapkan sendiri dan kursor waktu daun dari hari pertama kasus ini. Dengan menekan pembaruan perkiraan Anda mengunduh profil hari-hari itu; perhitungan sinyal bekerja untuk perkiraan saat ini. Ini berfungsi untuk membandingkan model dengan apa yang sebenarnya terjadi.

Kasus pertama adalah sekitar 14- 15 Agustus 2022 yang diceritakan di QST (November 2022), "Dunia di atas 50 MHz" [23]: pembukaan di bawah tekanan tinggi antara Manitoba dan Kansas / Missouri, dari 144 sampai 1296 MHz hampir semua di FT8, dengan rekor Kanada pada 10 GHz 128.56 km di Q65 dan sinyal sampai 1941 km. Yang kedua adalah pembukaan antara 29 Desember 2021 dan 2 Januari 2022 pada Tyrrhenian dan Mediterania Barat pada 144 MHz, dari laporan IC8TEM: pembukaan pertama pada 29 (sampai 737 km), lebih lebar pada 31 ( IT9GSF - EB5EA 1217 km), pembukaan besar pada 1 Januari antara Italia, Sisilia dan Spanyol. Untuk kasus yang lebih tua (misalnya September 2000) arsip Open- Meteo tidak memiliki data. Dalam arsip tidak ada lapisan batas tinggi: ada koreksi yang tidak berlaku.

## 4. Indeks dan warna

Untuk setiap lapisan antara dua tingkat Program membandingkan N drop dengan ambang tabel 6,5 dari Bean dan Dutton [5]: misalnya antara 1000 dan 850 hPa penurunan hingga 60 N / km normal, lebih dari 100 N / km adalah super- refraksi. Sebuah lapisan super- refractive bernilai 10 sampai 15 dB keuntungan; sebuah saluran yang menjebak band dari 20 sampai 25 dB, menurut defisit M. Kuat angin ke tanah, lapisan batas tinggi dan CAPE tinggi mengurangi keuntungan, karena mereka menghapus atau memecahkan lapisan [5, 4.4].

Warna adalah perkiraan keuntungan di dB, dengan skala tetap sama di semua tema:

| Gain | Warna | Artinya |
| Di bawah 7 dB | Nobody | atmosfer hampir normal |
| 7-10 dB | biru | gradien atas normal |
| 10-13 dB | hijau | layer super- refractive |
| 13- 16 dB | kuning | lapis kuat, tetapi tanpa saluran untuk band |
| 16- 18,5 dB | oranye | lapisan yang sangat kuat atau terlalu tipis |
| 18,5-21 dB | merah | Conduit Trapping Band |
| dari 21 dB | Ungu | Kuat dan dalam saluran |

Dengan kotak layar juga 3-7 zona dB antara 3 dan 7 dB, di mana sesuatu sedang dibentuk tapi tetap di bawah skala, Anda dapat melihat dengan menahan diri abu-abu ringan; warna tidak berubah. Sebuah elemit panel bertitik daerah yang ditutupi oleh perkiraan sendiri dan menunjukkan hasil maksimum terlihat: jika di bawah 7 dB peta tetap tanpa warna karena atmosfer hampir normal, bukan karena data hilang. Ketika mengunduh klik baru pada perkiraan pemutakhiran tidak mengganggu; untuk menghentikannya ada Stop.

Warna buram menjadi 82%, jadi latar belakang tema hanya mengubahnya. Lokasi grid dan nama persegi dirancang di atas warna. Mana prediksi Anda tidak ada tapi Hepburn atau F5LEN memberikan 5 atau lebih kartu catur muncul dengan warna sumber itu.

Peta menilai setiap titik saja. Perhitungan sinyal sebaliknya mengontrol bahwa lapisan terus menerus sepanjang rute (paragraf 9): rute yang melintasi sangat kuning dapat memberikan kurang dari peta membuat Anda berpikir.

## 4 bis. Forecast dimediasi oleh iklan: koridor (dari 1.3.3)

Hak Cipta Kebijakan: Sebuah link saja tidak boleh menarik saluran. Sebuah penerimaan yang terisolasi dan singkat dapat menjadi penyebaran dari pesawat yang transisi (paket APRS sangat pendek), pembukaan dari beberapa menit atau sebuah kesalahan, dan akan memberikan "drop- melakukan" mereka menipu. Program ini mengakui koridor ketika, dalam band yang sama:

- setidaknya 3 pasang stasiun independen (tidak berbagi stasiun) bekerja pada rute setidaknya 300 km;
- rute paralel: azmut dalam = 15 °, rata-rata titik dalam 150 km dari sumbu umum;
- semua dalam 30 menit terakhir, dengan balok mengulangi pada jarak setidaknya 10 menit (ketekunan);
- untuk ARS, lompatan menghitung hanya jika lompatan yang sama diulang;
- dari 1.3.4, rute yang lebih dari 1500 km hanya jika balok berasal dari setidaknya dua sumber (misalnya PSKReporter dan cluster): dari satu sumber, ke jarak tersebut dalam 2 m, lebih sering EME, menyebarkan meteor atau locator yang salah daripada tropo.
- dari 1.3.4, jangan menghitung stasiun 'untuk ray': yang dalam satu jam terakhir memiliki setidaknya 5 koresponden lebih dari 1000 km tersebar atas penggemar setidaknya 150 °. Dalam 2 m trope adalah arah; penerima yang "bebas" bersama-sama Israel, Portugal, Inggris dan Italia dari 2000- 3700 km, sering dengan sinyal positif, telah hampir pasti frekuensi dikonfigurasi buruk. Pada 17- 22 / 11 / 2026 I5MZY Arsip 72% akomodasi dalam 2m lebih dari 1000 km berasal dari stasiun jadi, pada 22 / 11 pada 10: 53 UTC adalah OH6HLH, OZ1ITS, F4LUK, OM8ABC dan SP2JLR, dan dengan filter terpanjang koridor menurun dari 2175 km (regole 1.3.3) ke 615 km.

Sumber: Lokasi cluster (tidak AS / ACS), PSKReporter melalui MQTT, QSO dan WSJT-X / JTDX / MSHV, dan APRS melompat dalam 2 m dan LoRa 433 / 868 MHz.

APRS: 2 m dan LoRa. Program ini membaca APRS-IS dalam read - hanya (rotate.aprs2.net, filter di Eropa) dan mengekstrak radio melompat antara stasiun lokasi yang dikenal (sorgente glegipeater voucher IGate), seperti peta VHF DXView dan APRS PropView (RF- YVY, Apache 2.0). Tiga band bernilai: 2 m, LoRa 433 MHz (band koridor 432) dan Lora 868 MHz (koridor berarti 1296: saluran yang membawa frekuensi tertinggi juga 868 MHz). Band ini berasal dari LoRa tocall (aprs-deviceid list) dan dari frekuensi atau kata-kata dalam komentar; melompat hanya layak antara dua stasiun dari band yang sama.

Titik halus adalah repeater aktif: sinyal yang diterima pada 500 km dapat tiba dengan dua atau tiga transmisi berbeda. Sebuah lompatan dihitung oleh karena itu hanya jika antara dua transmitter nyata dan berturut-turut:

- jalan harus ditelusuri: setiap penggabungan yang telah ditularkan kembali telah menempatkan namanya, dan lompatan diukur antara satu dan berikutnya; sebuah alias yang digunakan tanpa nama (W IDE1 *, W IDE2 *, alias regional) memecah rantai dan peregangan yang tidak masuk hitungan;
- retransmisi tersembunyi: program mengingat path yang setiap sumber biasanya bertanya (misalnya W IDE2 -2). Jika paket datang dengan lompatan kurang (W IDE2 -1) dan tidak ada nama yang menjelaskannya, sebuah digipeater mengirimkannya kembali tanpa pelacakan dan paket dibuang; sumber belum pernah dilihat sebelum tidak dihitung;
- digipeater di tengah jalan: jika antara dua stasiun, lebih atau kurang di jalan, ada sebuah digipeater dari band aktif yang sama dalam tiga jam terakhir, lompatan dibuang, karena bisa telah ditularkan kembali (khususnya untuk digipeater Lora yang kembali tanpa mengubah jalur);
- paket-paket radio (qAC, qAX, TCPIP), mereka yang dioperasikan melalui internet dari IGate (pihak ketiga), HF dan frekuensi berbeda, stasiun di ketinggian lebih dari 1500 m (paloni, pesawat) dan melompat lebih dari 1500 km.

Efek pada peta (dari 1.3.4). Ini menarik hanya daerah di mana jaminan minimum valid: sebuah band dari tahun 60 km sekitar bagian tengah dari setiap rute (dari 15% sampai 85% panjang), tidak pada stasiun di ujung, di mana saluran mungkin tidak ada. Band-band dari semua koridor digambar bersama-sama, dalam warna netral (teks) dan semi-transparan: tumpang tindih tidak menambahkan, dan warna tidak bingung dengan ungu skala (21 dB dan seterusnya). Label (ciri, jarak rata-rata, keuntungan minimum) dipindai satu sama lain; lewat dengan mouse pada label rute koridor muncul.

Di daerah, dan hanya dekat waktu saat ini (± 90 menit), perkiraan tidak jatuh di bawah minimum. Alasannya adalah: jika stasiun benar-benar lulus, penyebaran ada, apapun model cuaca mengatakan.

Karena rute pendek kurang bernilai (dari 1.3,4). Dalam 2 m, antara 300 dan 600 km, dua stasiun yang baik (beberapa watt, antena, FT8 atau CW) terhubung hampir setiap hari bahkan dengan atmosfer normal, untuk troposcatter: sinyal difusi oleh ketidakaturan kecil udara dalam volume umum dua bundel. Tiga atau empat tempat di 500 km mengatakan bahwa band bekerja seperti biasa, tidak bahwa ada pembukaan: Majalah GEKO n. 69 ( IU3QEZ) memperkirakan troposcater dengan 2 x 10 elemen yang mengalir sekitar 440 km di SSB QRP, 510 km di SSB dengan 50 W, 600 km di CW QRP dan 640 km di SSB (sumber, 11 lap). Hanya lebih dari 600- 700 km troposcatter tidak lagi dan serangkaian koneksi paralel benar-benar menunjukkan saluran atau lapisan. Hingga 1.3.3 koridor dari 450- 600 km mengangkat perkiraan sampai 9- 12 dB, dan peta menunjukkan peningkatan yang tidak ada.

Nilai awal adalah dengan jarak rata-rata dari rute:

- hingga 400 km: 3 dB;
- dari 400 sampai 700 km: garam dengan cara linear dari 3 sampai 10 dB (500 km 5,3 dB; 600 km 7.7 dB);
- dari 700 sampai 900 km: dari 10 sampai 16 dB; lebih dari 900 km tetap ke 16 dB.

Di bawah 7 dB, yang berada di bawah awal skala, minimum tetap bahwa: koridor digambar dengan label, terlihat bahwa band bekerja, tetapi warna peta tidak berubah (dengan kotak juga menunjukkan 3- 7 dB abu-abu bertitik). Di atas 7 dB nilai ditimbang, setengah dan setengah, dengan jumlah rute independen (3 stroke az0.25; 6 atau lebih Aver1) dan dengan median S / N dari hubungan (met20 dB 0.5; 0 dB atau lebih GC1; tidak diketahui, seperti dalam cluster spot, az075), dimulai dari 7 dB. Contoh: 3 rute tanpa S / N pada 500 km 5,3 dB; 5 rute pada 615 km 7.7 dB; 3 rute tanpa S / N pada 900 km 11.5 dB; 3 rute ke Korean 20 dB pada 900 km 10.4 dB; 6 rute ke 0 dB pada 900 km 116 dB.

Jarak minimum tetap 300 km: menaikkan ke 500 km akan juga dihapus nyata terbuka jangkauan (misalnya saluran pada Tyrrhenian di 400 km, yang diakui oleh cuaca ramalan dan sinyal yang sangat kuat lebih dari jarak saja). Dalam arsip I5MZY, pada 22 / 11 / 2026 pada 10: 53 UTC, dengan aturan ini dan filter sinar yang lebih panjang koridor adalah 615 km (7.7 dB) dan 540 km (6.3 dB); dengan aturan 1.3.3 mencapai 2175 km dan 16 dB.

Sampai 1,3.3 minimal berlaku pada semua persegi panjang koridor dan hanya bergantung pada jarak (dari 10 dB sampai 300 km sampai 16 km): dengan koridor panjang yang banyak bersama-sama (misalnya pada 22 / 11 / 2026, 12 koridor hingga 2620 km) mengangkat hampir semua Eropa ke 16 dB.

Kurva tidak lagi menggambar "gocce": pulau-pulau kecil dengan 220 km di kedua arah tetap berwarna tapi tanpa kurva tertutup, kecuali koridor menegaskan mereka. Ini switch off dengan "Prevision mediated by the spot".

## 5. Baris opsional dan perbandingan dengan tekanan dan inversi

Gain kurva: baris di 7, 10, 13, 16, 18,5 dan 21 dB, tepi skala, ditarik di lapangan halus dengan metode marching kuadrat, dengan label di dB.

Isobare: tekanan di permukaan laut setiap 4 dari GFS yang sama, sampai jam perkiraan, dengan pusat H dan L

Inversi Thermal: kurva ditutupi dengan berapa banyak udara pada 925 atau 850 hPa lebih hangat daripada itu pada 2 m, di + 1, + 3, + 5 dan + 8 ° C

Di bawah kontrol garis memberikan kesepakatan laba dengan tekanan dan dengan sebaliknya pada daerah yang terlihat: Pearson korelasi koefisien, 1x1 + 1. Saluran sering lahir dalam antisiklon (udara yang menjatuhkan pemanas dan bentuk sebaliknya), sehingga kesepakatan positif dengan tekanan adalah normal, tetapi di dalam antisiklon yang sama banyak perubahan keuntungan antara laut, pantai dan pegunungan. Kebalikan saja tidak cukup: tekuk gelombang hanya jika di atas udara juga pengering [5]. Garis berfungsi untuk melihat, sekarang, yang kedua terbaik menjelaskan daerah berwarna. Perbandingan yang sama ditulis dalam register.

## 6. Potensi jaringan pipa: perhitungan ganda

Tingkat GFS berkisar dari 200 sampai 1000 m: inversi tipis menyebar dan model tidak melihat saluran. Jika antara dua tingkat N melebihi normal (40 N / km), udara stabil (gradien termal di bawah 3 K / km) dan di atas adalah rasio kelembaban tertentu di bawah 0,8), program mempertimbangkan potensial saluran dan kalkulasikan dalam dua cara:

- Kalkulasi A: semua kelebihan konsentrasi turun dalam 50 m.
- Kalkulasi B: penurunan yang sama yang didistribusikan pada ketebalan pipa yang khas dari daerah itu, diambil dari peta iklim ITU- R P.453 [4] (di tanah atau tinggi).

Untuk defisit dan untuk maksimum terjebak panjang gelombang nilai terendah dipegang: itu adalah estimasi bijaksana.

ITU-R P.453 data adalah © ITU dan dapat digunakan untuk pribadi dan non-komersial tujuan dengan mengutip sumber, tetapi tidak dapat didistribusikan kembali: untuk ini saya tidak dalam paket program. Untuk mengaktifkan perhitungan B, hanya download P.453 peta dari ITU dalam tab: program, setelah menunjukkan kondisi penggunaan, download arsip dari jaringan pipa dari situs ITU (ditinggikan _ dan _ permukaan _ duct.zip) untuk penggunaan pribadi pengguna, ekstrak file dalam Data / P453 dan menghidupkan perhitungan tanpa reboot. Alternatif, unduh dari laman produk digital ITUR Study Group 3 (itu.int, ITUR Studi Grup 3, produk digital, suara P.453, data Surface dan data draft) dan tempatkan tujuh S _ OCCUR. TXT, S _ THICK. TXT, S _ STREN. TXT, E _ OCCUR.TXT, E _ base.T Tanpa berkas program hanya menggunakan perhitungan A dan menulisnya di tab dan di registry. Rasio antara defisit A dan intensitas khas P.453 adalah anomali: Berapa kali pipa hari ini lebih kuat dari biasanya. P453

## 7. Stasiun dan Locators

Pemilik bisa memiliki 4, 6, 8 atau 10 karakter. Dengan 8 atau 10 karakter posisi akurat dan saham tanah berasal dari Copernicus GLO-30: tinggi antena cukup dari tanah. Dengan 4 atau 6 karakter persegi terlalu besar: lebih baik untuk menulis kuota dari antena di laut; jika 0 masih perkiraan dengan tanah SRTM di pusat alun-alun ditambah tinggi dari tanah dan sinyal itu dalam hasil. Karena ketinggian standar yang sesuai adalah 3 m dari tanah; dengan laut share 0 digunakan SRTM tanah ditambah tinggi.

## 8. Tanah, difraksi dan troposcatter

Profil dari rute ini adalah sampel pada Copernicus GLO-30 setiap 30 m (lebih dari 120 km lebar). Radius Bumi yang ekuivalen adalah k = 157 / (157 + NIN), dengan penurunan dari N dalam km pertama dari profil GFS sepanjang rute [6, 7].

Agama dekat dengan stasiun (dari 1.3.2, kriteria penulis). P.452 membayar sudut cakrawala linear, 0.573 dB per mrad: bantuan dari beberapa puluhan meter untuk beberapa ratus meter dari antena puluhan dB, jauh lebih dari yang diamati. Untuk alasan ini, dalam radius 20 km dari setiap stasiun dapat menambahkan hingga 6 dB: sudut cakrawala karena mereka terbatas dengan yang paling jauh antrean ditambah 10,5 mrad. Ini berlaku untuk stasiunmu dan untuk koresponden. Garis "Near reliefs" mengatakan berapa banyak DB telah lepas landas.

Sumber tanah (dari 1.3.2). Kuota berasal dari Copernicus DEM GLO- 30 [3]: 1 detik busur, laut adalah 0 dan pantai jelas. Sampai 1.3.1, program menggunakan Terrain Tiles 'skadi' kotak AWS. Dekat pantai, kotak-kotak itu mencampur SSRTM dengan resolusi rendah seasbed data dan menyebarkan bagian pulau di laut: sekitar Capri membuat cincin hantu 80- 100 m antara 0,5 dan 1 km dari pantai. Pada 21 November 2026 IC8TEM melaporkan cakrawala yang tidak ada ke barat laut; titik - oleh perbandingan dengan Copernicus dikonfirmasi (94 m di laut ke barat laut Marina Grande vs. 0). Bahwa cakrawala palsu, dari + 2.6 °, biaya sekitar 26 dB troposcatter ke stasiun pesisir. Panes skadi tetap sebagai cadangan ketika Copernicus tidak merespon. Copernicus frame beratnya 15- 30 MB dan download sekali, mereka dari hanya laut tidak ada dan bernilai 0.

- Ruang kosong: Lfs = 92.44 + 20 log f (GHz) + 20 log d (km) [8].
- Diffraction: metode Bullington pada profil asli plus ITU-R P.452-16 = 4.2 [6] delta- Bullington tanah koreksi, dengan ITUR P.526 [9] kehilangan bola.
- Zona Fresnel pertama: radius; kini radius 1; bebas jika tanah meninggalkan setidaknya 0.6 dari radius. Untuk setiap hambatan, parameter [9] Knife Board V dan J (VV) ditampilkan..
- Dalam desain profil radius langsung hijau dalam pandangan optik dan merah jika melewati bawah tanah atau di bawah kelengkungan bumi; titik-titik merah adalah hambatan yang menyerang pertama daerah Fresnel (garis bertitik ungu). Di laut "titik merah" di ketinggian 0 m bukan gunung: itu adalah laut naik untuk kelengkungan. Melewati dengan mouse pada radius atau pada titik muncul komik dengan penjelasan dan nilai.
- Ketika bantuan di dekat antena menutup arah (misalnya cakrawala puluhan derajat), diffraction dan troposcatter memberikan kerugian ratusan dB: di atas 300 dB aplikasi menulis "> 300 dB" dan "arah ditutup oleh hambatan tetangga", sinyal "< 1230 dB" dan rasio S / N "MESIN 60 dB". Dalam hal ini dianjurkan untuk melihat "tanaman direkomendasikan" dan "Via monte".
- Troposcatter: ITUR P.452- 16 eq. 45 sampai 50% dari waktu [6], dengan jarak sudut dari cakrawala dan N0 dari profil.
- Tanpa tropo: jumlah dari kekuatan difraksi dan troposcatter, tidak pernah di bawah ruang bebas.

## 8 bis. Troposcatter: empat model dan ketidakpastian

Di samping P.452 program menghitung troposcater dengan tiga model lainnya, seperti halnya program TropoLink [16], dan menunjukkan perbedaan mereka sebagai ketidakpastian:

- NBS TN101 [25] 9 X1Q (dari 1.3.5), median: L = 30 log f analytical 20 log d + F (FOD) dengan koreksi F0 + H0 + LGP. F (AFD) adalah fungsi perhatian dari Annex III dalam bentuk analitis (Ns 301) dengan koreksi untuk Ns, H0 fungsi frekuensi kurang dari.9: Ns adalah bahwa dari profil GFS jam, cakrawala efektif dan ketinggian berasal dari profil Copernicus, Lgp = 0.07 Ini adalah bentuk yang sama bahwa ITM menggunakan dalam interior, tetapi dengan reaktivitas dari hari ini bukan iklim. Ini menguji pada bagian datar di 144 MHz dengan antena pada 20 m: dalam 2-4 dB dari ITM antara 150 dan 900 km dan sekitar 2 dB dalam bentuk yang disederhanakan 9.2b dari TN101, yang tidak memiliki H0.

- ITU-R P.617-5 [17] L = F + 22 log f + 35 log AND + 17 log d + Lc, dengan f di MHz, Mrad, d dalam km, F = 0.1Q N0 dan AVN biasanya diambil dari peta iklim ITU; di sini kita gunakan setiap jam profil GFS ( N0) ^ (teorehs / 7.35) adalah reaktivitas ke tanah, ACN penurunan di km pertama), juga troposcatter mengikuti waktu. N0
- Buku Panduan RSGB VHF / UHF Formula, DL2FZN [18]: L = Lfs + 24.44 + 4.283 ln f + 0.06656 d. Lekukan dengan jarak hampir sama dengan PROPBER (0.067 dB / km).

Volume yang sama (dari 1.3.1): adalah titik di mana radius dari dua stasiun disilangkan, masing-masing cenderung ke sudut horizon, pada Bumi dengan radius yang setara dengan ae = k Dari TX radius naik sebagai z = hTX + x; Jarak x = (hRX enjuhTX + d ^ sybr + d2 / (2) / (2), dengan golok = d / ae + mist + Syprr. Dengan cakrawala datar dan antena pada kuota yang sama basis bernilai d2 / (8): sekitar 1.3 km 300 km, 3.7 km sampai 500 km, 9.4 km sampai 800 km. Semakin tinggi volume, semakin udara kering dan langka dan sedikit menyebar: karena alasan ini lebih dari 800- 1000 km troposcater habis. Pada hasilnya adalah pembagian dan posisi dasar, reaktivitas, gradien dan kelembaban GFS pada ketinggian itu, dan jarak yang setara dengan Bray (QST, November 1961): setiap horizon ekstra atau kurang bernilai sekitar 111 km jauhnya. Referensi disiplin: presentasi "Beyond the horizon in VHF" oleh IU3QEZ (GEKO Magazine n 69).

Garis Troposcatter, model lain menunjukkan dua kerugian dan ketidakpastian sebagai setengah perbedaan antara model yang paling optimis dan paling pesimis. Mereka hanya perbandingan: kerugian tanpa tropo tetap bahwa P.452.

## 8 ter. sore troposcatter atas reliefs (dari 1.3.4)

IK3XTV, dari Thiene ( JN55, dengan Alpen di 10 km), bekerja di sore hari di Jerman Selatan 400- 500 km, tanpa terlihat, dengan sinyal yang kuat tetapi dengan cepat dan dalam evanescence; penulis mengatasnamakan mereka untuk "sel-sel konverktif selaras sebagai kereta lensa". Pengamatan dikonfirmasi; penjelasan yang diadopsi dalam program ini adalah hal lain, lebih tegas secara fisik.

Pada sore hari matahari membangkitkan udara di atas antrean dan lapisan batas naik ke 2- 3 km. Pada puncaknya udara lembab di bawah ini memenuhi udara kering di atas: ada turbulensi dari indeks refractif (ukuran yang mengatur troposcater) maksimum. Ketika puncak mencapai volume umum rute, yang pada 400-500 km adalah sekitar 2-3 km, troposcatter diperkuat. Ini menjelaskan waktu sore, tekanan tinggi (langit yang tenang, pemanasan yang kuat) dan evanescence cepat, khas difusi dan bukan dari saluran. Sel-sel konventif tidak bertindak sebagai lensa: menghasilkan turbulensi, dan turbulensi pada bagian atas lapisan recast yang menyebarkan sinyal.

Konsekuensi untuk prakiraan membaca:

- siang pertama adalah waktu terburuk bagi saluran-saluran rendah dan lapisan-lapisan, tetapi bukan untuk troposcatter luar gunung: kedua hal harus dibaca secara terpisah;
- aplikasi telah memiliki dua data yang melayani, tinggi lapisan batas yang disediakan oleh model dan berbagi volume umum dari rute (register "Common Volume" di hasil): ketika mereka dekat, sore memperlakukan gunung dapat pergi lebih baik daripada perhitungan di atmosfer normal;
- dari 1.3.4 titik perhitungan mengambil ke account: jika volume umum adalah di bumi, sepanjang rute ada antrean setidaknya 800 m, matahari adalah lebih dari 15 ° dan lapisan batas melebihi 800 m, dan puncak lapisan membentuk kembali (model tingkat tanah + batas tinggi batas) adalah dalam 500 m dari dasar volume umum, troposcatter keuntungan 3 dB; antara 500 dan 1000 m peningkatan linear jatuh ke nol. Garis "Troposcatter pomeridiano". Dengan salju sepanjang rute (setidaknya 10 cm di tanah menurut model, variabel kedalaman salju dari Open-Meteo atau SNOD dari GFS dari NOMADS) penguatan tanah tidak berlaku: di salju tanah tetap dingin dan lapisan yang berbentuk kembali tidak tumbuh. Nilai dari 3 dB adalah sementara dan harus dikalibrasi dengan link nyata; mematikan dengan koreksi cuaca.

Sumber: pengamatan oleh F. Egano IK3XTV (Radio Rivista e www.qsl.net / ik3xtv, "Propagazione sulle 2 metri", "celle convecttive", "Troposcatter over the Alps"), sumber # 13.

## 9. Lapisan pendiri sepanjang rute

Ini adalah jantung perhitungan sinyal. Profil GFS diambil sepanjang rute setiap sekitar 25 km. Untuk setiap lapisan tekanan, peregangan terpanjang dari lapisan yang sama ditemukan pada titik-titik berturut-turut; dihitung sebagai hanya drag lapisan tebal setidaknya 40 m [10].

- kontinitas: dari apa km ke apa km, dan fraksi rute;
- atas dan dasar, dan kecenderungan di m / km;
- tanah di dalam lapisan: km di mana pegunungan naik di atas dasar;
- Tanah tertutup: di laut saluran hampir selalu, memutuskan peregangan tanah [10]. Jika tanah melebihi 10% dari rute, fraksi yang digunakan adalah minimum antara kontinuitas dan cakupan bumi.

Keuntungan trope adalah keuntungan lapisan untuk pecahan yang digunakan. Jika lebih dari setengah profil sub- refraptif hasilnya negatif.

## 9 bis. Pembenaran cuaca: stabilitas, subsidence, pemogokan

Dari 1.3.0 profil juga memberikan tiga informasi meteorologi, yang dapat dilihat di register (diberikan kembali "stabilitas (Misme)"), dalam garis Meteo sepanjang rute hasil dan, dengan kotak meteor Segni, di peta.

- Stabilitas Misme [5, Bab 6]. Misme mengikat bidang di luar cakrawala ke parameter senyawa: M = a (ge metro40 + b), dengan sebuah = 0,5 dB per N / km, gradien yang setara dan stabilitas termal yaitu, pekerjaan untuk meningkatkan udara 1 km. Program ini menghitung SKW = g Istilah ditambahkan ke keuntungan adalah 0.07 dB untuk J / kg di luar standar, terbatas antara September 4 dan + 6 dB, dan hanya menghitung di mana tidak ada konsol perangkap band (gradien sudah dalam indeks Bean dan Dutton). Kalibrasi bersifat sementara: itu harus diverifikasi dengan rasio (paragraf 14).
- Subsidence and uphill air [19]. Moler dan Holden, pada rute 125-305 km ke laut pada 1365 dan 9380 MHz, menemukan sinyal tertinggi sekitar 2,5 dB rata-rata dengan udara turun yang dengan kenaikan udara, dan dalam kasus penguatan sekitar 20 dB selama 3 jam di awal subsidensi, diikuti dengan penurunan 22 dB ketika udara kembali ke konverge. GFS memberikan kecepatan vertikal ke 850 dan 700 hPa. Jika pada kedua tingkat udara turun lebih dari 1 cm / s dan ada pembalikan atau penurunan tajam dalam indeks refractive potensial K (N dilaporkan diabocally ke 1000 hPa, yang menyoroti udara kering dan hangat turun dari atas [5]), 1.5 dB ditambahkan; jika udara naik lebih dari 5 cm / s jika mereka lepas landas 2.
- [20] Scirocco. Udara panas dan kering dari Sahara atas udara laut dingin memberikan inversi yang sangat kuat dari Mediterania timur ke Gibraltar. Tanda S muncul di mana pada 850 hPa angin berasal dari 100- 200 ° dengan setidaknya 7 m / s dan suhu setidaknya 16 ° C, di Mediterania. Ini hanya sebuah indikasi: memperoleh memutuskan lapisan profil.

Tanpa saluran yang menjebak band keuntungan total tidak melebihi 16 dB, akhir kuning: koreksi membuat lapisan yang lebih kuat lebih jelas tetapi tidak lulus dengan saluran (merah dan ungu tetap di saluran).

- Ketekunan (dari 1.3,1). Pembukaan terbesar datang setelah hari-hari tinggi (VHF / UHF DX Book; Biecoano dan Leyshon 1998, aturan 12 jam; laporan 29 / 12 / 2021-2 / 1 / 2022 tumbuh dari pertama sampai hari keempat). Untuk setiap sel program menghitung jam dari 24 sebelumnya dengan keuntungan setidaknya 13 dB dan menambahkan hingga 2 dB secara proporsi, hanya di mana keuntungan sudah setidaknya 10 dB.

Bagaimana untuk membaca tanda-tanda pada peta (Ubah Tanda Cuaca; dari 1.3.2 simbol adalah dua kali lebih besar dan melewati dengan mouse atas tanda muncul komik dengan penjelasan dan nilai-nilai sel):

| Tandatangani | Warna | Apa artinya? | Efek pada trope |
|---|---|---|---|
|  ↓  | hijau | subsidence: air tetes (antisiklon), panas untuk kompresi dan kering, memperkuat terbalik dan tetap kuat | menguntungkan, + 1.5 dB |
|  ↑  | merah | udara atas (depan, tekanan rendah, konveksi): mencampur lapisan rendah dan istirahat inversi dan saluran | tidak menguntungkan, _ BAR _ dB |
|  S  | oranye | Syrup: udara panas dan kering dari Sahara atas udara laut segar | Petunjuk menguntungkan, tidak ada koreksi: memutuskan lapisan |

Sebuah daerah yang penuh dengan zat hijau di bawah sebuah antisiklon adalah tanda klasik dari pembukaan yang keras; panah merah sepanjang garis mengatakan bahwa lapisan-lapisan yang melanggar, meskipun warna sel masih baik.

The Weather Corrections box mematikan koreksi ini untuk melihat model dari 1,2.

Validasi (paragraf 14): pada 593 koneksi ke 144 MHz antara 29 Desember 2021 dan 2 Januari 2022, dibandingkan dengan rute yang sama dan dua minggu kemudian, indeks pemisahan (0,5 = sempurna) adalah 0,79 tanpa koreksi dan 0,91 dengan koreksi (sebelum atap di 16 dB). Perbandingan yang sama menunjukkan batas utama: saluran di laut pada ketinggian rendah, yang pada masa itu membawa tanda-tanda antara Campania dan Liguria, tidak terlihat pada tingkat GFS.

## 9 meter. Berita 1.3.2: dua perkiraan, langit-langit, tekanan tinggi, laut, stasiun, pegunungan

bijaksana dan perkiraan agresif. Keuntungan dari gradien tumbuh dari 0 dB ke batas normal hingga 10 dB ke ambang dari refraksi super. Dalam lapisan 1000- 850, ambang batas bijaksana adalah Bean dan Dutton (100 N / km); ambang agresif adalah batas klasik Super- Refraksi dari 79 N / km (k; P.453 menggunakan Kore100 N / km di 100 m pertama), yang dengan k50 = 157 / (157 JUMN) adalah k = k = 2 (k = 2.5 sesuai dengan sekitar 94 N / km). Titik perhitungan menunjukkan dua kerugian diapit; dengan saluran yang menjebak band bertepatan. Peta ini menggunakan kewaspadaan.

Attic 2500 m. Jalan dan saluran hanya dicari untuk sampai 2500 m di atas tanah: semakin tinggi antena radio amatir tidak mengeksploitasi mereka. Dengan 1500 m validasi kehilangan titik pemisahan dan 4% dari koneksi di atas 16 dB, karena di musim dingin inversi dari subsidensi sering antara 1500 dan 2500 m.

Tekanan tinggi diperpanjang. + 1 dB mana setidaknya 7 dari 9 sel sekitar telah mengurangi tekanan ke laut 1020 hPa atau lebih dan angin lemah dalam lapisan rendah (10 m di bawah 5 m / s, 850 hPa di bawah 8 m / s), kecuali gerakan meningkat.

Berat laut. Dalam perhitungan titik, kenaikan lapisan dikalikan 0,8 + 0.4, di mana 1.1 adalah faktor dari ITU- R P.452 (eq. 51) pada peregangan lahan terpanjang: 1 di laut terbuka, kecil dengan panjang peregangan tanah.

Stasiun dan dilakukan. Ketika saluran bahwa perangkap band mencakup hanya bagian dari baris, bagian dari setiap antena dibandingkan dengan dasar dan atas saluran pada awal dan akhir bagian tertutup; keuntungan dikalikan dengan 0.5 + 0.5 Dengan saluran di hampir semua rute adalah kopling dari £10.

Lapisan dan pegunungan. Jika lapisan batas atas GFS (tingkat dasar dari paling batas _ lapis _ tinggi model) melebihi batas rute (maksimum tempat di atas 500 m), lapisan melewati pegunungan tanpa melanggar: + 1.5 dB untuk fraksi yang tertutup. Ini adalah pengamatan IT9GSF pada bukaan dari Sisilia ke Jerman dengan menguntungkan Tyrrhenian.

Levels dekat dengan tanah di laut. Dengan sumber NOMADS program ini juga mengunduh suhu permukaan (di laut adalah SST), suhu dan kelembaban pada suhu 80 m dan pada lapisan 30- 0 hPa di atas tanah. Pada titik-titik laut (kecepatan model di bawah 5 m) tingkat ini ditambahkan ke profil, di bawah tingkat pertama tekanan, untuk melihat saluran pada 100-200 m bahwa tingkat tekanan tidak memecahkan. Di bumi yang sama sekali tidak ada digunakan kecuali oleh orang-orang yang mengadakan perdamaian dan yang lewat di malam hari dan siang hari. Buka Meteo hanya memberikan suhu permukaan.

Jalur pembuangan. Dari SST, dari suhu dan kelembaban sampai 2 m diperkirakan tinggi dari saluran penguapan dengan profil logaritma netral (seperti dalam model Paulus dan Jeske, tanpa koreksi stabilitas): udara ke rambut air jenuh ke SST (98% untuk garam), karena itu

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

terbatas antara 0 dan 40 m. Untuk perangkap 144 MHz itu akan mengambil sekitar 180 m (Kerr), maka pada 144 MHz saluran penguapan memperkuat sedikit; dari 432 MHz up, dengan antena rendah di laut, banyak (paragraf 11 bis). Masukkan persamaan parabola dan dalam garis Corrections 1.3.2.

Sesungguhnya kamu, pada gilirannya, akan mendapatkan kemenangan seperti para nabi terdahulu. Program menghitung tinggi matahari pada setiap titik. Dari hari ke hari di bumi, dengan matahari lebih dari 15 ° dan lapisan batas lebih dari 800 m atau angin lebih dari 5 m / s, memperingatkan bahwa tanah saluran yang terbentuk kembali; pada malam hari, dengan matahari di bawah 13,°, angin di bawah 3 m / s dan lapisan batas di bawah 300 m, memperingatkan bahwa dengan langit tenang mungkin dilakukan oleh irradiation, yang jatuh setelah matahari terbit. Tidak di laut. Untuk saat ini mereka terlihat, bukan angka validasi Desember tidak memiliki cukup hari musim panas untuk mengkalibrasi mereka. Ini adalah dua poin yang menurut penulis penting, dari pengamatan sehari-hari.

Profil normal CRPL (alternatif). Alih-alih 40 N / km tetap penurunan normal mengikuti referensi atmosfer eksponensial CRPL: N (h) = Ns ^ (prestac), dengan tenaga kerja di km pertama = 7.32 Dengan udara basah (Ns tinggi) normal adalah curam dan melayani gradien yang lebih kuat untuk memberikan keuntungan; di ketinggian lebih manis. Ternyata buruk sampai validasi mengatakan itu membantu.

## 9 quater. Kontras udara - laut dan laut rendah: Adriatic (dari 1.3.3)

Gelombang radio tidak merasakan dasar laut: rasakan kontras antara udara dan permukaan air. Kedalaman penting karena memutuskan seberapa cepat suhu laut mengikuti yang udara.

The Adriatic adalah laut rendah. Kedalaman rata-rata adalah sekitar 250 m, tapi cekungan utara jarang melebihi 100 m dan Teluk Trieste tetap di bawah 25 m, hanya ke selatan turun lebih dari 1200 m. Lapisan air begitu tipis menghangatkan dan dingin dalam beberapa hari, yang Tyrrhenian dalam beberapa minggu. Ada tiga situasi khas:

1. Musim semi dan awal musim panas, menguntungkan. Laut masih dingin, bahkan untuk air segar Sungai Alpine dan Po; itu tiba panas dan kering udara dari dataran atau Balkan. Udara panas di atas air dingin membentuk pembalikan di laut (pipa suara): adalah kondisi klasik pembukaan antara Italia, Slovenia dan Kroasia.
2. Musim gugur dan musim dingin dengan bora, tidak menguntungkan. Udara dingin di atas laut yang lebih hangat: lapisan yang rendah menjadi tidak stabil, menekan dan istirahat saluran.
3. Setelah bora, kesempatan. Hanya karena laut rendah, sebuah bora mendingin itu beberapa derajat dalam beberapa hari. Jika Anda kemudian memasukkan antisiklon dengan udara panas, kontras udara kuat dan saluran lebih kuat dari biasanya.

The Adriatic juga merupakan saluran tertutup antara Apennines dan Dinari, kurang dari 200 km: lapisan laut tetap terbatas, dan rute sepanjang sumbu (Pugli- Venezia- Trieste, sekitar 700 km) tetap berada di laut, kondisi terbaik untuk saluran yang panjang.

Apa yang dikalkulasi program. Di titik laut, dengan koreksi cuaca dan kotak "Contrasto aria- mare":

| Kondisi | Koreksi |
| --- | --- |
| suhu potensial pada 925 hPa (atau 950) selama SST 4 K | _ |
| di luar SST dari 7 K | + 2 dB |
| dengan udara lembab di 925 hPa (lebih dari 80%) | Kore0,5 dB pada bonus ini |
| Laut dingin dari 1,5 ° C atau lebih dalam 72 jam sebelumnya, dengan kontras menguntungkan | _ |
| udara laut hangat di 2 m dari 2 ° C (pencampuran) | 1.5 dB |
| laut hangat 4 ° C | 3 dB |

Koreksi total tetap antara September 3 dan + 3 dB dan, tanpa saluran, keuntungan tetap di bawah atap kuning. Palet ini bersifat sementara: di atmosfer standar suhu potensial 925 hPa melebihi dari laut sekitar 2- 3 K, maka 4 K sudah menunjukkan sebaliknya.

Ingatan SST. Model menjaga suhu laut hampir stabil selama prediksi. Penurunan terbaru berasal dari analisis tiga hari sebelum (Open-Meteo, hanya suhu permukaan, hanya sel laut): maksimum 72 jam sebelum dikurangi nilai saat ini. Jika layanan tidak merespon, bonus memori tidak berlaku.

Batas. SST model di bawah pantai dan di atas Adriatic adalah kasar; pena dingin dari Po, misalnya, diselesaikan dengan buruk. Batimetry tidak memasukkan perhitungan: sumber-sumber selalu memberikan SST, yang telah merangkum efek dasar laut. Perbedaan udara-laut tumpang tindih sebagian pada akhir Stabilitas Misme (ffin9 bis), karena ini bonus kecil. Palet akan bergaris dengan koridor tempat dan QSO melalui Adriatic.

## 10. Dilakukan di seluruh rute

Jika lapisan perangkap band pada setidaknya 80% dari fraksi yang digunakan dan stasiun-stasiun tidak terlihat, kerugian dalam saluran juga dihitung: ruang bebas, ditambah kopling antena (3 dB) jika mereka berada di dalam saluran, lebih jika mereka berada di atas atau di bawah), ditambah residu peserta adalah = 5 = 5 ax05 Jika kurang dari kerugian tanpa tropo menjadi kerugian yang diharapkan. [6]

Sebuah band tetap berada di saluran hanya jika panjang gelombang tidak melebihi azas = 2 = 2 / 3) Untuk ini lapisan yang sama dapat perangkap 23 cm dan 70 cm tapi tidak 2 m. Namun, batasnya bukan bersih: Kerr memperingatkan bahwa gelombang-gelombang beberapa kali lebih lama menderita banyak saluran, dan Hepburn dan VHF / UHF DX Book berbicara tentang "spried" tepi dan "forgive" saluran. Dari 1.3.1 program ini menggunakan saluran lunak: jika Idmax adalah antara lemmax dan 3 Dalam hal ini warna saluran diperbolehkan. Log dan garis Meteo sepanjang rute menunjukkan titik-titik dengan saluran lunak. [11]

## 10 bis. Via Monte

Ketika koresponden ditutup di belakang cakrawala, program mencari arah terbaik untuk menunjuk antena: lereng diterangi menyebarkan sinyal sebagai target radar. Ini membuat lingkaran cakrawala setiap 5 (kecuali 5.15 ° di sekitar koresponden) dan dalam setiap arah mengambil titik cakrawala diterangi, lebih dari 100 m dan antara 1 dan 60 km. Sebuah estimasi cepat (ruang bebas, konversi bit dan 10 dB untuk setiap tingkat cakrawala dari difusi ke koresponden) memilih 4 yang paling menjanjikan, setidaknya 15 ° satu sama lain; untuk masing-masing:

- Perlakukan 1, station Affillian, di ruang kosong;
- Memperlakukan 2, lega operator, dihitung seolah-olah lega ditransmisikan dari 10 m di atas atas, dengan prediksi yang sama (tanpa persamaan parabola);
- konversi dengan persamaan radar bistatic, dengan positive 0 R1

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

Baris "Titik Disarankan" mengindikasikan azimuth dari rute terbaik, jika biaya kurang dari langsung. Pada 144 MHz, dengan bantuan pada 20 km dan yagi dari 15 °, jalan pegunungan biaya sekitar 45 dB lebih dari 2 rute saja: itu hanya terjadi ketika langsung ditutup oleh hambatan tetangga. Estimasi ini kotor (5.10 dB), terutama untuk Sydne0.


Tanpa kehilangan gunung (pilihan, dari 1.3.2). Dengan "Via monte senza perdite up al monte" kotak, jatuhnya antara stasiun dan titik difraksi dianggap kosong: seperti membawa stasiun ke atas (antena sampai 10 meter di atas titik, kekuatan yang sama, antena dan kabel). Putaran cakrawala kemudian memilih titik dengan cakrawala yang paling bebas menuju koresponden (pada parity, tertinggi) dan hasilnya menunjukkan rute dari titik ke koresponden, dengan sinyal dan S / N, di samping total dengan difusi. Ini adalah batas optimis: mengatakan jika arah terbuka ketika gunung menimbulkan sinyal dengan baik, atau di mana untuk pergi portabel. Setiap titik diberikan dengan karakter sepuluh.

## 10 meter. Masuk ke saluran jauh

Jika saluran yang perangkap band dimulai jauh, di luar cakrawala stasiun, sinyal harus tiba. Program ini membawa sinyal ke titik awal, di tengah saluran, untuk difraksi: sepanjang profil Copernicus GLO-30 pencarian tepi dengan parameter tertinggi Fresnel (ITU-R P.526), dengan pembengkakan lahan ekuivalen,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

lebih banyak perhatian karena kelengkungan dari atas (batas bulat, P.526-16 '4.2), dengan radius R diperkirakan dari tanah Copernicus GLO- 30 ke 5.1 km dari tepi,

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

(untuk m > 4 adalah bentuk 34b rekomendasi). Barsis (NBS, 1963) mengukur tiga rute panjang di luar gunung (Colorado, Alaska, Eropa) rata-rata kerugian 10-20 dB lebih tinggi daripada tepi ideal Fresnel- Kirchhoff: pembulatan menjelaskan banyak (Barsis dan Kirby, 1961: Pikes Peak dengan radius sekitar 11,5 km). Hasilnya dibandingkan dengan difraksi pada tanah halus bulat; itu adalah yang terburuk. Kemudian sinyal bergerak di saluran: ruang bebas di seluruh rute, residu peserta dari P.452 di bagian dalam saluran dan kopling dari koresponden (3 dB dalam, 1 dB setiap 10 m di luar). Jika kerugian ini lebih rendah dari kerugian yang diharapkan itu menjadi mekanisme ("conduct (input by diffraction)"). Ini adalah dugaan penulis tentang geografi Italia: dengan hambatan dekat jauh di bawah garis, v negatif dan biaya masuk sedikit; dengan biaya depan dorsal 15-30 dB.

## 10 quater. ITM (Longley-Rice) dan keluar dari saluran

Dari 1.3.2 program berisi Irregular Terrain Model 1.2.2 NTIA / ITS (Longley dan Rice, ESSA ERL 67, 1968; Hufford, Longley dan Kissick, NTIA 82- 100, 1982), diterjemahkan ke C # oleh kode resmi pemerintah AS, yang bebas dari hak-hak. Terjemahan memberikan nomor yang sama dengan aslinya: kompilasi otomatis dibandingkan setiap kali dengan kode NTIA C + + pada lima kasus uji (dibawah 0.01 dB).

ITM menghitung kerugian di atmosfer normal pada profil nyata Copernicus GLO-30: terlihat dengan refleksi dari tanah, diffration (spigol dan lahan bulat yang ditimbang pada kekejaman tanah Schurah, itu adalah perbedaan antara 10 dan 90 ° percentile dari kuota sekitar lurus kotak minimum) dan troposcatter. Reaktivitas tanah berkurang rata-rata ke bagian rata-rata dari rute, $N_s = N_0\,e^{-h/9460}$, dan memberikan kelengkungan tanah yang setara,

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

Program ini menggunakan iklim radio dari rute yang dipilih sebagai dalam sexies # 10 (sampai 1.3.4 selalu beriklim laut, dengan N0 320 di tanah dan 350 di laut), dengan N0 dari reaktivitas rata-rata tempat itu (tanah Averzor 15, 1305 S / m; sea Averr 81, 195 S / m), polarisasi horisontal dan variabilitas dari waktu ke waktu (profil diketahui). Ini menunjukkan kerugian melebihi 50% dari waktu dan satu yang Anda miliki dalam 10% dan 1% dari waktu terbaik. ITM tidak model saluran: variabilitas dari waktu ke waktu adalah statistik, oleh iklim. Ini berfungsi sebagai perhitungan kedua dari dasar di sebelah P.452; perbedaan antara keduanya memberikan ide ketidakpastian.

Penggunaan kedua adalah peregangan akhir setelah saluran yang berakhir sebelum yang sesuai. Pada titik keluar, antena maya ditempatkan setengah jalan melalui saluran dan ITM menghitung saluran hingga yang sesuai, sebagai kelebihan atas ruang bebas di 50% dari waktu. Kehilangan total adalah

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,	heta_{condotto} + A_{ITM}(d_{uscita} 	o RX)
```

dimana A _ TX adalah kopling dari antena (3 dB di dalam, 1 dB setiap 10 m di luar) jika saluran dimulai dekat, atau difraksi entry dari # 10 meter. Ini adalah batas yang penulis telah berikan sendiri: kelanjutan lapisan ditambah, paling banyak, residual attenuation. Jika lebih rendah dari kerugian yang diharapkan itu menjadi mekanisme ("conduct (concect ITM)").

## 10 pertanyaan. Deepening: pulau, selat dan bumi-laut discontinuity

Sebuah contoh dari Teluk Napoli (Anacapri, JN70CN) menuju acara Mediterania barat di mana rute melintasi pulau-pulau:

| Tujuan | Azmut | Dimana melintasi pulau-pulau | Halangan |
| --- | --- | --- | --- |
| Barcelona |  279°  | utara Sardinia (Gallura) hingga 41.05 ° N, antara 420 dan 490 km | sekitar 30 km selatan Bocche di Bonifacho; Limbara (1359 m) sekitar 20 km dari garis |
| Bocche di Bonifacho |  282°  | mendekati 41,3 ° N | sekitar 11 km |
| Marseille |  296°  | Corsica Pusat pada 42.2 ° N | gunung sampai 2700 m: saluran di bawah 2 km ada interupsi |
| Valencia, Balearics |  260–265°  | Sardinia Tengah | reliefs internal |

Untuk Barcelona itu cukup untuk menunjuk ke 282 ° bukannya 279 ° karena sumbu melewati dari mulut: 3, di dalam balok yagi normal.

Apakah mulut selat? Untuk saluran rendah, di bawah beberapa ratus meter, ya: pulau-pulau yang berdinding, dan di atas bumi, pada hari, pemanasan istirahat terbalik. Selat itu sendiri cukup lebar: sekitar 11 km, melawan area pertama Fresnel, kurang dari satu kilometer di tengah rute 1000 km di 144 MHz. Masalahnya adalah cuaca. The Mouths adalah menyenangkan bagi angin: Lapisan bawah dipercepat dan lapisan bawah dihapus. Seringkali bagian terbuka sebagai geometri tetapi terganggu sebagai atmosfer. Dengan antisiklon yang stabil dan sedikit angin bisa menjadi bagian yang baik.

Bumi-laut disparitas. Mereka menghitung. Sebuah saluran terbentuk di pantai Tyrrhenian terganggu: sinyal keluar dari saluran (bagian setelah keluar, td 10 quater) dan harus kembali ke saluran lain di laut Balearik, membayar lagi kopling. Saluran di ketinggian, di atas 1500 m, melewati Gallura tanpa disadari; untuk pusat Corsica tidak berlaku.

Dalam praktek: jika rute melintasi sebuah pulau pegunungan, mencoba beberapa derajat koreksi terhadap peregangan laut bebas, tetapi tidak mengandalkan jalan dengan angin yang kuat di sempit.

## 10 Sexy. TN101 iklim radio dan waktu variabilitas (dari 1.3.5)

TN101 = 10 [25] menjelaskan bagaimana hilangnya rute di luar cakrawala bervariasi selama periode panjang di sekitar median, dengan kurva yang berbeda dengan iklim: fungsi Y (q) mengatakan berapa banyak dB yang hilang jatuh di dalam Q% dari waktu terbaik. ITM berisi mereka kurva untuk tujuh iklim. Iklim menghitung banyak: di laut dalam iklim laut beriklim beriklim iklim laut, pada 144 MHz, dalam 10% dari waktu Anda mendapatkan lebih banyak dB daripada pada lahan iklim benua, karena saluran lebih sering.

Program ini tidak lagi memperbaiki iklim: Dia memilih untuk setiap memperlakukan dari titik rata-rata, sehingga bekerja di seluruh dunia dan tidak hanya di Eropa. Unduh dari Open- Meteo [26] tiga tahun nilai harian dari reanalisis ERA5 (suhu, kelembaban dan tekanan tanah, hujan) untuk sel 0.5 ° dari titik rata-rata, hanya sekali; nilai tetap dalam Data / CLIMA. Restorasi tanah

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

hari demi hari, maka rata-rata tahunan dan ekskursi antara rata-rata bulanan, kelembaban rata-rata dan hujan tahunan. Aturan-aturan mengikuti deskripsi iklim dari TN101 10.2; batas-batas antara satu iklim dan lainnya adalah kriteria penulis:

- lebih dari 60 ° lintang: suhu benua ( TN101 tidak memiliki kurva untuk iklim kutub);
- kurang dari 250 mm hujan per tahun dan kelembaban rata-rata di bawah 50%: gurun (Ns khas 280);
- dalam 20 ° dari khatulistiwa, Ns dari setidaknya 350 dan ekskursi hingga 30: khatulistiwa (Ns khas 360);
- dalam 32 ° dan ekskursi setidaknya 60: benua subtropis, yaitu monsoon (ekskursi khas 60- 100);
- antara 10 ° dan 32 ° dengan N dari setidaknya 340: laut subtropis (Ns khas 370);
- Kedua cakrawala di laut, lebih dari 20 °: laut marah di laut (itu adalah definisi TN101);
- Ekskursi hingga 30 dekat laut (laut di rute atau rata-rata kelembaban setidaknya 75%): maritim marah di bumi;
- jika tidak beriklim benua (ekskursi khas 20- 40).

N0 untuk ITM adalah rata-rata berkurang ke permukaan laut, N0 = Ns Tanpa jaringan Anda menggunakan aturan cadangan dengan garis lintang saja dan pecahan laut, dan garis mengatakan itu.

Tes dengan data ERA5 2023- 2025: Capri Ns 339 dengan excursion 45 (continental temperate on land, ocean temperate on the sea); London 327 dan 16 Wina 319 dan 24 Abidjan 381 (equatorial); Miami 365 dan 36 (suami subtropis); Delhi 337 dan 82 (conconcontinental subtropis); Riad 278 dan 37, 90 mm hujan (gurun). Di Chili: Antofagasta 341 dan 19 (suami subtropis: pantai Atacama, dengan reverse rendah yang hampir tetap dari Pasifik), Calama 232 di lapangan, 2257 m, 14 mm hujan (gurun), Valparíso 331 dan 11 (jahitan suhu), Punta Arena 313 dan 5 (seam suhu).

Garis "iklim radio ( TN101)" menunjukkan iklim, alasannya, Ns dengan setengah dari ekskursi dan Y: berapa banyak dB mendapatkan 10% dan 1% dari waktu dan berapa banyak kerugian dalam 90%. Ini adalah statistik jangka panjang dan tetap terpisah dari perkiraan: kerugian yang diharapkan mengikuti waktu hari ini dengan profil GFS, dan jumlah dua hal akan dihitung dua kali lipat dari penyebaran abnormal yang sama.

## 11. Perbandingan dengan ITUR P.452

Baris P.452 berlaku metode ITU untuk saluran dan refleksi pada lapisan (P.452- 16 = 4.5 [6]) dengan Median dari GFS bukan satu iklim dan p = 20% sebagai konstanta kalibrasi, seperti dalam CSIRO [10] studi. Ini hanya perbandingan: kerugian yang diharapkan tetap bahwa paragraf 9.

## 11 bis. Persamaan parabola (dari 1.3.1)

Garis Equasi Parabola adalah perhitungan keempat, hanya perbandingan, dibuat sebagai program profesional untuk saluran (PETOL; kerangka penyebaran gelombang dari M. Lytaev, MIT lisensi): alih-alih memperkirakan hasil dari lapisan, itu meningkatkan gelombang sepanjang rute dengan metode Split-step Fourier dalam profil refractive M yang diubah dari GFS, diambil dari titik perkiraan lebih dekat ke jarak tertentu. Tingkat GFS menambahkan perkiraan lompatan dari saluran potensial (menghitung A, 50 m dalam lapisan setengah). Tanah Copernicus GLO-30 memasuki langkah (di bawah tanah lapangan adalah kosong), tanah konduktor dan balok antena adalah Gaussian dengan perkiraan pembukaan dari keuntungan. Perhitungan melihat saluran, yang kehilangan, penyimpangan dan penghubung antena. Dia tidak melihat tropocater, yang ditambahkan. Berikutnya adalah "cek profil": sama dengan atmosfer standar kurang satu dengan profil yang diharapkan.

Dari 1.3.2:

- Progrator berada pada sudut lebar, dengan fase yang tepat dari ruang bebas:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- dasar laut dan dataran pesisir pada ketinggian nol mencerminkan dengan koefisien dari Fresnel dalam polarisasi horizontal, Schulc = Pelanch = 60

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- kekasaran laut mengurangi refleksi konsisten menurut Miller dan Brown, dengan ketinggian efektif gelombang dari angin sampai 10 m (Achrah = 00051):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- di laut profil M termasuk saluran penguapan (paragraf 9) dengan profil logaritma netral:

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

Ini diuji dengan prototipe (antena 10 m di laut, memperoleh pada atmosfer standar di 100- 300 km): dengan None = 10-30 m saluran penguapan memberikan 1-3 dB ke 144 MHz, 8- 37 dB ke 432 MHz dan 21- 66 dB ke 1296 MHz. Pada 144 MHz dalam polarisasi horizontal laut mencerminkan hampir sebagai logam (level124) 124; lebih dari 0,99 untuk beberapa derajat) dan kasar menghitung sedikit: dasar baru perubahan beberapa puluhan dB; itu menghitung lebih dari 432 MHz up.

Batas: di atas bumi profil vertikal tetap bahwa tingkat GFS, sehingga saluran tipis tetap tidak terlihat; tanah untuk langkah-langkah yang mendekati, balok tetap dalam 6.

## 12. antena, sinyal dan kebisingan

- Yagi DL6WU: mendapatkan 9.5 + 7.8 dBd (2.35 dB untuk booming ganda, DL6WU mengukur VHF Komunikasi 3 / 1982), ditambah 2.15 untuk [12] dBi.
- EIRP = power procecable + TX gain.
- Sinyal diterima = EIRP impected loss + gain RX rececable RX. S9 = bet93 dBm, 6 dB per unit.
- Suara eksternal menurut ITUR P.372 [13]: galaksi (Fa = 52 log f) lebih buatan menurut lingkungan (kota, perumahan, pedesaan, pedesaan tenang) atau suhu antena ditulis tangan.
- Tsys = Ta + Tpre + [290 (Lc emper1) + 290 (Fr 1x1) Dari 1.3.5 stasiun penerima selalu memiliki preamplifier dalam antena, parameter tetap: nomor kebisingan 0.8 dB (Tpre = 59) dan mendapatkan 20 dB. Di belakang ada kabel (kehilangan Lc) dan radio, dengan nomor kebisingan dasar Fr 6 dB. Dengan 1 dB kabel dan masukan radio tetes dari sekitar 1160 K sampai 12 K: kebisingan memutuskan langit, lingkungan dan preamp. Hasil dari 20 dB adalah nilai khas yang dipilih oleh program.
- S / N dalam SSB, CW dan FT8 dalam band dari setiap cara, dibandingkan dengan threshold set.
- Probabilitas untuk periode (dari 1.3.2): evanescence dalam satu jam adalah distribusi normal dari S / N sekitar nilai rata-rata per jam yang diharapkan. Bray (QST, November 1961, P. 39, pada kurva Norton 1959) memberikan amplitudo dari 13,5 dB selama sekitar 100 mil, yaitu sekitar 7 dB di bawah dan 7 di atas rata-rata, yang jatuh hampir lurus di bawah 100 mil (3,5 dB sampai 50 mil); untuk link 'solid' mengambil 7 dB dari anggaran. Diambil SS7 DB sebagai: Kemungkinan bahwa sebuah periode melebihi ambang batas jalan adalah

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  dengan fungsi distribusi normal: margin 0 dB = 50%, + 3.4 dB = 84%, + 7 dB ('solid' margin Bray) = 98%. Ini tidak termasuk ketidakpastian model, yang lebih besar.
- Digital Ways: S / N dalam 2500 Hz dibandingkan dengan ambang batas diukur pada 50% decoding, tanpa informasi sebelumnya (AP). FT8 1x21 dB dan FT4 17,5 dB pada kebisingan putih; ketika mekanismenya adalah troposcatter gunakan ambang batas dengan evanescence "interrupt lintang medium", FT8 18,6 dB dan FT4 15.2 dB [21]. Q65 -15C 22.2 dB, Q65 -30B 24,8 dB, Q65 -607Q -67Q 276 Dalam penyebaran pesawat, Doppler dapat melampaui nada Q65 -60A (1.7 Hz): Ada lebih baik 30B atau 15C. AP decoding (nama yang sudah dikenal atau akhir dari QSO) memberikan sekitar 2 dB ekstra untuk FT8 dan 1.5 sampai 2.6 dB untuk Q65; tidak masuk ke ambang batas karena tidak valid untuk semua nilai "reven24 dB" yang sering dibaca untuk FT8 adalah batas dari hubungan WSJT- X atau AP decoding, bukan batas di 50%. [24] JT65A [22]

## 13. Respon dan waktu data

Setiap perhitungan mengukur waktunya sendiri: GFS profil (jaringan, analisis, istirahat wajib dari 20 s antara batch antara batas Open-Meteo), medan Copernicus GLO-30 (dengan download panel, 15- 30 MB masing-masing, hanya untuk pertama kalinya), perhitungan. Garis Waktu Response merangkum mereka, dalam register baris-baris dimulai dengan TEMPI. Ketika meningkatkan grid, register juga melaporkan ras GFS yang digunakan, ketika menjadi tersedia dan berapa umur data tersebut.

Dari 1.3.2 Anda juga dapat memilih ICON-2I, model Badan Italious Meteo dan Arpae di Italia sekitar 2,2 km, melalui Open- Meteo (model isalia _ meteoro _ arpae _ icon _ 2i, dua balapan per hari, 3 hari). Ini mencakup hanya Italia dan laut terdekat dan memiliki tingkat yang lebih rendah dari GFS (1000, 925, 850, 500, 500 hPa): untuk saluran di ketinggian tidak lebih baik daripada GFS, tetapi jaringan halus menggambarkan baik pantai, pulau dan laut pada 2 m, yaitu saluran penguapan. Variabel bahwa model tidak memiliki (batas struktur, suhu permukaan...) menghapus diri dari permintaan. Dari 1.3.2 sumber otomatis hanya menggunakan NOMADS; Open-Meteo melayani jika NOMADS tidak merespon dan untuk kasus-kasus historis. Dengan "Update on its own box" (lighted by default) prakiraan akan diunduh tanpa klik: di pembukaan, di setiap GFS ras baru di NOMADS (sekitar 5 jam setelah waktu perlombaan) dan ketika area terlihat keluar untuk lebih dari seperempat dari grid; di sebagian besar upaya setiap 15 menit, tidak pernah dengan kasus sejarah.

Permintaan di Open- Meteo memiliki batas per menit, per jam dan per hari untuk setiap alamat internet. Profil yang diunduh disimpan pada disk (Dati / cache-gfs) untuk setiap stroke dan model: dengan perlombaan yang sama, yang berubah setiap 6 jam, poin yang sudah diunduh tidak bertanya lagi. Dengan sumber otomatis Anda mulai dari NOMADS dan Open- Meteo hanya berfungsi untuk poin yang hilang; dengan satu-satunya Open-Meteo, pada batas waktu atau harian berhenti segera dan mengatakan kapan harus mencoba lagi. Kasus bersejarah tidak di cache.

## 14. Validasi model

Perkiraan harus dibandingkan dengan koneksi nyata. Catatan perhitungan menyimpan setiap nomor; arsip program menyimpan iklan dan decoding. Untuk 432 dan 1296 MHz, di mana bintik-bintik adalah sedikit, mereka membutuhkan laporan dari operator: yang membuat koneksi, untuk apa waktu, dengan sinyal apa. Bandingkan selama berjam-jam keuntungan yang diharapkan dengan sinyal diukur mengatakan jika ambang batas harus dipindahkan.

## Sumber

1. NOAA / NCEP, Global Forecast System (GFS), model global pada 0,25 °, empat balapan per hari.
2. Buka -Meteo, prediksi API (membuka-meteor.com), model gfs _ seamless, variabel pada tingkat tekanan dan metadata balap.
3. Copernicus DEM GLO-30 (ESA; WorlDEM © DLR E.V. 2010- 2014 e © Airbus Detation and Space GMBH 2018, disediakan sebagai bagian dari Copernicus oleh Uni Eropa dan ESA), 1 detik busur, frame GeoTIFF pada open register Amazon Web Services. Cadangan: NASA / USGS Copernicus GLO- 30 di Terrain Tiles "skadi" AWS.
4. ITUR P.453, Indeks refractive radio: formulanya dan tanggal refractivity, dengan peta saluran digital (Studi Grup 3, © ITU, penggunaan pribadi dan non- komersial).
5. B. R. Bean, E. J. Dutton, Radio Meteorology, NBS Monograph 92, 1966: Tabel 6.5 dan 404.
6. ITUR P.452-16, Prosedur Prediksi untuk evaluasi interferensi antara stasiun-stasiun di permukaan Bumi pada frekuensi di atas 0.1 GHz:
7. ITUR P.834, Efek refraksi troposfir pada penyebaran gelombang radio.
8. ITUR P.525, Kalkulasi perhatian ruang bebas.
9. ITUR P.526, Propagasi oleh diffraction: eq. 31 (cutcheon) dan # 3 (tanah bulat).
10. B. Indermuehle, H. Suzuki, Tropospheric Ducting Prediksi berdasarkan GFS Model Data di Inyarrimanha Ilgari Bungara, CSIRO Murchison Radio- astronomi Observatorium, Publikasi dari Astronomical Society Australia, 2026 (arXiv 2608.12861).
11. D. E. Kerr, Propaging dari Short Radio Waves, MIT Radiasi Laboratorium Series vol. 13, 1951: eq. 17 e 18.
12. G. Hoch DL6WU, "Sangat Panjang Yagi Antennas," VHF Komunikasi 3 / 1982, p.130 et seq., ara. 1 (diukur 432 dan 1296 MHz menurut panjang boom).
13. ITU- R P.372, Radio noise: galaksi dan buatan kebisingan untuk lingkungan.
14. NOAA / NCEP, NOMADS, filter GRIB dari GFS 0.25 ° time (nomads.ncea.noa.gov); format WMO G RIB2, kompleks kemasan dengan perbedaan spasial.
15. Open- Meteo, Historical Forrcast API (model prakiraan arsip sejak sekitar 2021).
16. Lukas Box, TropoLink, desain rute troposcatter dengan P.617, NBS TN101 dan ITM diapit (GitHub, Apache 2.0 lisense).
17. ITUR P.617-5, Teknik prediksi Propagasi dan data diperlukan untuk desain sistem transhorizon radiorelay, 2019.
18. RSGB, Buku Panduan VHF / UHF; implementasi DL2FZN (andi- f / troposcatter di GitHub, 2013).
19. WF Moler, D. B. Holden, "penyebaran penyebaran penyebaran Troposfir dan sirkuit atmosfer," Journal of Research of the NBS 64D, 1960, doi 10.6028 / jres.064.014: sinyal di luar cakrawala (1365 dan 9380 MHz, di laut) dan gerakan vertikal udara.
20. Buku Panduan ARRL 2022 (99a ed), 194.3-194.4: cuaca dan penyebaran troposferical.
21. S. Franke, B. Somerville, J. Taylor, The FT4 dan Protokol Komunikasi FT8, QEX Juli / Agustus 2020.
22. W3SZ, ambang batas mode digital lambat ( JT65, JT9, QRA64) dan pilihan mode menurut penyebaran, 2019.
23. QST, "Dunia di atas 50 MHz", November 2022: pembukaan 14-15 Agustus 2022 antara Manitoba dan Kansas / Missouri.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Quick- Mulai Panduan ke Q65, 3 April 2021 (wsjt.sourceforge.io).
25. P. L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis, Transmission Loss Predictions for Troposfiric Communication Circuits, NBS Technical Note 101, magazine 1967: H0
26. Open-Meteo, API Cuaca Historical (ECMWF ERA5 ulasan sejak 1940; Layanan Perubahan Iklim Copernicus, CC BY 4.0).
