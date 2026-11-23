LIVETROPOVHF dokumen teknis 2
# Konstruksi peta

> Terjemahan otomatis oleh Italia: dalam kasus keraguan layak panduan Italia.

Sebagai peta tab dan titik lahir: basis geografis, proyeksi, rata-rata Hepburn dan indeks F5LEN, isobar, bintik-bintik dan stasiun-stasiun mendengarkan, kelebihan dengan lapisan. Versi 9 November 2026.

## Ringkasan

- Peta ini dirancang oleh program dalam delapan lapis, masing-masing dengan kotak [8].
- Dasar: Garis pantai bumi dan batas alam pada dua tingkat [9] detail; [10] Market dan proyeksi azimoth.
- Fisika: inversi, reaktivitas N dan gradien dN / dh, ambang batas memimpin pada September 157 N / km [3] [4] [7] [11].
- Indeks throat: terkalibrasi pembacaan Hepburn [1] dan F5LEN [2] map, kisi-kisi umum dan rata-rata dari dua sumber.
- Isobare: tekanan GFS [12] melalui Open- Meteo [15], baris setiap 4 hPa, H dan L pusat.
- Spot, stasiun mendengarkan (lingkaran merah muda gelap dengan selalu terlihat garis merah muda) dan decoding pribadi merah.
- Membaca tumpang tindih: tekanan tinggi, saluran tinggi, tekanan menurun, SNR, [3] [4] [5] [13] [14] rute pegunungan.

## 1. Prinsip: peta berlapis

Peta ini bukan gambar tetap: dirancang oleh program, lapis dibagi lapis, kapanpun Anda mengubah zoom, projection, band, meramalkan waktu atau tema. Setiap lapisan memiliki kotak di lapisan atas pada peta, sehingga Anda hanya dapat memegang apa yang Anda butuhkan dan melihat semuanya bersama-sama ketika Anda ingin gambaran penuh. Perintah, dari bawah ke atas, adalah ini:

|  #  | Jalan | Isi | Casella |
|  1  | Peta dasar | Laut, tanah, pantai dan perbatasan | Ya |
|  2  | tropo indeks | Media Hepburn + F5LEN, atau hanya satu sumber; transparansi yang dapat disesuaikan | Ya |
|  3  | Locator Grill | Fields, kuadrat dan, dengan zoom, kuadrat | Ya |
|  4  | Isobare | Tekanan garis setiap 4 hPa, pusat H dan L | Ya |
|  4a  | Suhu 850 hPa | Isotermal dirawat setiap 4 ° C | Ya |
|  4b  | Inversi Thermal | Contoh oranye di mana udara di ketinggian lebih hangat daripada di tanah | Ya |
|  5  | PSKReporter | Menerima stasiun PSKReporter (lingkaran merah muda gelap) dan, dalam pink, apa yang mereka dengar | Ya |
|  6  | Didecoded by me | Decoding stations with WSJT-X, JTDX or MSHV: Red lines from your QTH | Ya |
|  6a  | QSO desunti dal live | Koneksi tertutup ( RR73, 73, RRR) dibaca dalam decoding Anda: garis hijau antara dua stasiun | Ya |
|  7  | Spotlight | Koneksi: garis biru penuh, kuning tertutup untuk penyebaran udara, dua bola | Ya |
|  7a  | Ekstensi | Casella Terkecuali di bawah 100 km: lalu lintas terpendek tidak ditarik atau terdaftar. Casella Lebih dari 1000 km: hanya kotak (mulai): jika salah satu stasiun adalah lebih dari 1000 km dari QTH Anda, tidak ada garis atau lingkaran, hanya kotak locator stasiun yang terlibat dalam lilac diperlakukan pada 45 ° | Ya |
|  8  | QTH dan label | Stasiun Anda, nama-nama tempat yang dipilih | selalu terlihat |

Indeks tropo segera di atas dasar dan di bawah segalanya: adalah sampling berwarna, dan ditempatkan lebih tinggi akan menutupi garis dan simbol. Jaringan melewati itu, sehingga Anda selalu tahu di mana daerah berwarna jatuh. Isobares adalah garis tipis dan baik dibaca atas sampling.

## 2. Peta dasar

Biaya, batas dan tanah berasal dari data vektor bumi alami, domain publik [9], untuk skala 1: 50 juta untuk keseluruhan tampilan dan 1: 10 juta ketika diperbesar. Berkas-berkas berada dalam folder program: tidak ada layanan peta online. Warna tema Clear: laut biru muda # E2ECF8, pantai biru gelap, perbatasan abu-abu terang. Dalam tema Hitam dan Malam warna dasar yang gelap, dalam tema lain mereka mewarnai sedikit.

## 3. Dua proyeksi

Semua lapisan melewati fungsi proyeksi yang sama, sehingga mereka selalu selaras satu sama lain. Anda memilih antara dua [10] proyeksi (Averbujur, Kesulitan, R ray Bumi):

Pasar. Simpan lokal sudut dan bentuk; Jarak peregangan menuju kutub. Potong ke lintang 5.85 °.

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

Azimutale equidanti berpusat di alun-alun Anda. Pusat (ADE0, ADE0) adalah pusat dari persegi WWL Anda. Jarak dan arah yang diukur dari pusat adalah benar: sebuah stasiun dengan skala 1200 km, dan sudut dengan utara adalah azimuth yang menunjuk antena. Ketika Anda mengubah locator peta refokus.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

Dalam azimutale garis bahwa pada kertas lurus (meridian, paralel, tepi persegi) menjadi melengkung. Untuk alasan ini semua baris dibagi menjadi sifat pendek, setiap bagian diproyeksikan dan kemudian ciri-ciri bersatu; sama berlaku untuk tepi indeks tropo dan untuk isobare. Titik ke antipodes pusat tidak memiliki posisi yang pasti dan tetap tidak terlihat.

## 4. Locator Grill, zoom and move

Jaringan Maidenhead dirancang oleh program: bidang 20 ° + 10 dengan dua huruf besar dan pudar di tengah, kotak dari 2 ° 1, dan dengan zoom subqutrati yang cukup 5 × 2.5. Garis yang biru dan tipis, tidak untuk menutupi indeks tropo.

Perkecil dengan roda yang berpusat pada penunjuk (titik di bawah tetikus tetap stabil), bergerak menyeret, klik ganda untuk kembali ke seluruh tampilan, + dan mexkeys, dan kunci dengan persentase untuk seluruh tampilan. Detail pas dengan zoom: lebih halus pantai, bawah tanah, lebih tebal label isobis.

## 5. Mengapa inversi menekuk gelombang

Dalam suasana rendah suhu biasanya turun oleh sekitar 6,5 ° C per km; indeks refractif tergantung pada baik suhu dan kelembaban [3], dan di mana perubahan tajam dengan ketinggian gelombang membungkuk ke tanah dan tiba lebih jauh dari normal. Lapisan yang paling penting adalah bahwa hingga sekitar 3 km, di mana inversi, saluran dan konventif sel [4] terbentuk; jalur laut disukai karena massa air membentuk daerah besar dari inversi [4]. [3]

Ukuran yang digunakan untuk mengukur itu adalah refractive N, yaitu (n = 1) [11]

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

T adalah suhu di kelvin, P tekanan dan tekanan uap, baik dalam hPa.

Menghitung bervariasi N dengan share (dN / dh). Dalam atmosfer standar adalah sekitar Korin39 N / km; hingga Kore79 N / km refraksi dianggap normal, selain super- refraksi, dan dengan gradien positif subfraksi [7]. Di bawah 157 N / km gelombang dibengkokkan lebih dari kelengkungan bumi dan tetap terjebak: itu adalah saluran [11] (tancap). Ini adalah kondisi yang Hepburn dan F5LEN peta mencoba untuk memprediksi.

## 6. Indeks tropo: dari dua peta ke rata-rata

Hepburn dan F5LEN tab adalah meja kontrol: tampilkan gambar yang diunduh dari halaman dari setiap sumber, dengan waktu prakiraan berubah, dan berfungsi untuk mengkalibrasi pembacaan dengan mengklik titik referensi dan warna dari legenda. Lapisan indeks tropo tetap kosong sampai setidaknya satu dari dua kalibrasi tidak aktif dan lengkap.

Hepburn. Hepburn indeks mengukur berapa banyak gelombang diharapkan untuk dilipat lebih dari sebuah daerah, pada skala linier dari 0 sampai 10 yang juga dapat melampaui 10. 0 berarti atmosfer standar dan hanya tersebar; 1-2 marginal atau refraksi super- lemah; 3-4 super- refraksi dengan daerah saluran; indeks tidak memperhitungkan tropo menyebar dan tidak melihat pegunungan [1]. Peta ini mencakup 6 hari: 3-jam berlalu untuk 36 jam pertama, kemudian 6 jam, diperbarui setiap hari sampai 18 UTC [1]; data awal (suhu, kelembaban, tekanan) berasal dari model global dari layanan cuaca Kanada [1]. [1]

F5LEN. Untuk Eropa F5LEN menerbitkan 31 peta dari + 3 ke + 192 jam (setiap 3 jam hingga + 48, setiap 6 sampai + 96, setiap 12), dengan tanggal dan waktu yang valid dalam judul, waktu produksi di kanan bawah dan legenda kelas: Disturb, Normal, Minor, lemah, baik, kuat, Vy kuat, duct [2]. Prediksi dunia F5LEN dihitung oleh indeks refractif udara di Unit N, pada 40 langkah dari 3 jam (sekitar 5 hari), diperbarui 4 kali sehari; klik pada peta menunjukkan nilai rata-rata dan locator titik [2].

Melanjutkan. Untuk setiap langkah prediksi program melakukan langkah-langkah ini:

1. Discharge. Gambar dari dua prakiraan tersebut diunduh dalam singgahan di sebelah executable, sekali untuk memperbarui sumber, bukan untuk mendesain ulang apapun. Sebelum versi terakhir, izin harus diminta untuk Hepburn dan F5LEN.
2. Georeferencing otomatis. Program ini menemukan bingkai (dua garis horizontal dan dua garis vertikal dengan warna yang sama) dan, pada Windows 10 / 11, dibaca dengan OCR terintegrasikan ke dalam sistem label axle (54N, 10W, 0, 5E...). Potong strip di sekeliling bingkai, memperbesar mereka, tes kedua tanda ketika huruf N / S / E / W tidak membaca dan menjaga garis di mana label yang paling jatuh: pembacaan yang salah tetap keluar. Dari sini Anda bisa mendapatkan yang ekstrim lat / lon frame dan jenis proyeksi (regular grill or Mercator). Ekstrim menyelamatkan diri mereka sendiri dan membaca ulang sendiri jika frame perubahan ukuran. Pada Mac dan Linux, tanpa OCR, ekstremitas pabrik atau tulisan tangan berlaku dalam tab. Alternatif, untuk peta yang tidak memiliki sumbu yang dapat dibaca: Setiap jenis peta (Hepburn Europa, F5LEN di seluruh dunia...) memiliki kalibrasi: beberapa titik kontrol dengan koordinat yang diketahui (salib meridian dan paralel, iga dikenali) menghubungkan piksel ke lintang dan bujur, dalam proyeksi yang digunakan oleh sumber tersebut. Kalibrasi dalam berkas di sebelah program dan Anda dapat membuat ulang jika format perubahan sumber.
3. Membaca warna. Setiap pixel dibandingkan dengan sumber legenda dan dikonversi ke dalam numerik terdekat nilai. Biaya, tulisan dan grid yang digambar pada peta asli diakui sebagai "tidak ada data" dan diisi dengan nilai-nilai di dekatnya.
4. Jaringan dunia. Setiap gambar, setelah frame ditemukan dan dikenal ekstrim, ditempatkan di Bumi, nilai pada setiap titik di dunia berasal dari gambar yang menutupinya. Jika lebih banyak daerah mencakup titik yang sama itu layak rinci (lebih piksel per derajat), asalkan waktunya adalah dalam 1,5 h dari pilihan itu, maka jam terdekat, maka ras terbaru. Dimana tidak ada gambar yang mencakup titik peta menunjukkan kerudung abu-abu: tidak ada data, yang berbeda dari "index 0". Di dalam bingkai, pixel tanpa warna legenda adalah 0 (atmosfer normal); pantai, perbatasan, dan tulisan melompat dengan melihat warna terdekat dalam 3 piksel.
5. Tangga yang sama. Hepburn sudah pada skala 0-10 [1]: latar belakang gelap = 0 dan sepuluh warna dalam urutan (ungu, biru, air hijau, kapur, kuning, ocher, oranye, merah, merah muda, cahaya pink) untuk 1... 10, ungu di luar. F5LEN menggunakan kelas (Disturb, Normal, Minor, Lemah, Baik, Kuat, Vy Kuat, Duct) [2], tetapi beberapa memiliki warna Hepburn persis: Lemah = 1, kuat = 5, Vy Kuat = 7, Duct = 8; Minor adalah 0.5 dan Baik (hijau) 3.5. Pengganda skala, dalam tab F5LEN, mengoreksi set untuk membuatnya sebanding dengan Hepburn.
6. Waktu yang sama. Timebar berjalan dari + 0 ke + 192 h di langkah 3 jam. Untuk setiap langkah yang Anda ambil dari setiap sumber perkiraan valid pada waktu terdekat; Jika sumber tidak memiliki langkah itu, media hanya menggunakan yang lain dan legenda menunjukkan itu.
7. Media. Dengan tombol media setiap titik bernilai (Hepburn + F5LEN) / 2; dengan Hepburn atau F5LEN Anda hanya melihat satu sumber, berguna untuk memahami di mana dua prakiraan tidak setuju.
8. Menggambar. Nilai berwarna dengan skala legenda (0 biru, 2 hijau, 4 kuning, 6 oranye, 8 merah, 10 ungu) dan diproyeksikan sebagai sampel di bawah grid. 0 adalah transparan, jadi di mana tidak ada yang terjadi Anda melihat peta dasar yang bersih; transparansi umum menyesuaikan dengan kursor di samping kotak.

Pada tab tempat, indeks sepanjang rute adalah rata-rata nilai-nilai grid kejuaraan setiap 10 km pada lingkaran maksimum antara dua stasiun.

## 7. Isobare

Tekanan tinggi stabil adalah sinyal pertama yang dicari untuk trope [3], dan koneksi di luar Alpen yang dianalisis IK3XTV juga telah dengan tekanan [4] tinggi.

1. Data. Tekanan pada permukaan laut dari GFS dari NOAA [12], dibaca melalui layanan Open- Meteo [15] gratis pada grid 3 ° 3 (dari 30 ° sampai 72 ° N, dari 30 ° W sampai 48 ° E), untuk 78 jam berikutnya. Program pembaruan data pada startup dan kemudian setiap 6 jam, seperti balap model.
2. Garis. Isobar diperoleh dari grid setiap 4 hPa (... 1012, 1016, 1020...) dengan algoritma kotak marching (marching kuadrat), maka mereka sedikit bulat dan diproyeksikan ditarik oleh saluran.
3. Centres. Maksimum lokal dan minimum yang paling ditandai ditandai ditandai dengan H (biru) dan L (merah) dan nilai dalam hPa.
4. Label. Nilai muncul sepanjang setiap isolara, pada latar belakang warna peta untuk tetap dapat dibaca di atas sampling.

Area dan langkah. Data tersebut diunduh pada area yang Anda lihat, diperluas oleh 25%, dengan langkah dari 1 sampai 20 ° yang dipilih untuk tetap berada di bawah 400 poin (Open-Meteo menghitung setiap titik sebagai permintaan: 600 per menit, 5000 per jam): dengan memperbesar atau memindahkan peta keluar dari daerah tersebut telah mengunduh program yang paling padat, paling banyak setiap 10 menit; jika pemutakhiran gagal data sebelumnya tetap dan itu akan mencoba lagi setelah 3 menit. Dengan cara ini isobar dan suhu bekerja di mana saja di dunia.

Suhu. Permintaan yang sama membawa suhu ke 2 m, ke 925 hPa dan ke 850 hPa. LA0BY menunjukkan bahwa peristiwa 22 / 11 / 2018 sampai UA6 terlihat pada peta suhu (900- 3000 m), tapi tidak dalam prediksi Hepburn [3]; DF5AI dan DK5YA menggunakan profil suhu vertikal untuk mengenali inversi [5]. Untuk ini ada dua lapisan:

- Suhu 850 hPa: isoterme memperlakukan setiap 4 ° C (merah di atas nol, biru di bawah, ungu di nol).
- Inversi termal: oranye sampling di mana udara pada 925 atau 850 hPa lebih hangat daripada itu pada 2 m; semakin kuat, semakin inversi kuat. Ini adalah kondisi fisik yang menekuk gelombang ke tanah [3] [11], dan membandingkannya dengan indeks tropo dan isobare menunjukkan jika ramalan memiliki dasar meteorologi.

## 8. Spots dan stasiun

Spot. Garis biru untuk titik normal, kuning ditutupi dengan tepi gelap untuk penyebaran udara (< AS >, < ACS > atau "airscatter" dalam komentar), bola untuk setiap stasiun, cahaya halo di bawah garis untuk melepaskan dari sampling. Tempat yang dipilih lebih sering dirancang untuk terakhir dan dengan dua nama.

Mendengarkan. Setiap PSKReporter [16] stasiun penerimaan adalah merah muda gelap lingkaran kosong. Dari setiap awal garis merah muda penuh terhadap stasiun yang telah diterjemahkan dan dikirim kembali ke jaringan, selalu terlihat tanpa mengklik, dengan nama selalu ditulis. Dengan memilih lingkaran garis yang lebih tebal. Spot dan mendengarkan hanya menampilkan band dan jangkauan yang dipilih dalam daftar: mengubah bandwidth peta kosong dan dirancang ulang dengan data yang telah dikumpulkan untuk band baru.

Memutuskan olehku. Setiap stasiun yang didekode dengan WSJT-X, JTDX atau MSHV (tanda terima melalui UDP) adalah garis merah penuh dari QTH ke stasiun, dengan bola dan nominasi. Ini adalah perbandingan paling langsung dengan perkiraan: menunjukkan di mana penerima Anda benar-benar datang sekarang.

## 8 bis. Warna dan simbol

Setiap warna hanya memiliki satu makna, sama dalam tema (hanya mengubah latar belakang).

| Warna dan tanda | Artinya |
| Garis biru terus-menerus | Spot dari DX cluster, RBN atau DM4X antara dua stasiun |
| Garis kuning dengan tepi gelap | Spot dengan komentar siaran udara (refleksi pada pesawat) |
| Lingkaran merah muda kosong | Mendengarkan stasiun di PSKReporter (MQTT) |
| Garis merah muda tipis dari lingkaran, dengan bola merah muda di bagian bawah | Stasiun yang didengar pendengar, dengan nama selalu ditulis; lebih tebal baris untuk pendengar yang dipilih |
| Garis merah dan nama merah | Dekoding UDP Anda (WSJT-X, JTDX, MSHV): Siapa kau? |
| Garis merah berkedip stroke | QSO sedang berlangsung dengan stasiun itu: Dia memanggil Anda dan penutupan belum tiba, atau adalah Panggilan DX dari WSJT-X / JTDX dengan transmisi aktif. Lanjutkan dengan RR73, RRR atau 73 |
| Garis hijau | QSO depunti dal live: penutupan RR73, RRR atau tampilan UDP 73 |
| Garis hitam dengan batas putih | Seseorang memanggil Anda: itu berlangsung sampai RR73/RRR / 73 atau sampai 3 menit |
| Kotak 45 ° | Lalu lintas dengan setidaknya satu stasiun di luar mahkota terpilih: Tidak ada garis, hanya kotak locator |
| Cari ballas dengan jarak ke utara | 1000, 700 dan 500 km di sekitar QTH Anda, bahwa pilihan lebih ditandai |
| Lingkaran dan titik dalam warna tema | QTH Anda, dengan nama dan pemilik |
| Bola jeruk dengan Graves 143.050 | Radar Graves ( JN27SI) |
| Contoh dari biru ke merah | Indeks trope Hepburn (0-10) atau kelas F5LEN, atau rata-rata mereka; warna yang lebih hangat, kondisi yang lebih baik |
| beludru abu-abu ringan | Area tanpa data tropo untuk waktu yang dipilih |
| Garis biru Grey- dengan nilai dalam hPa | Isoberate setiap 4 hPa; H dalam tekanan tinggi biru, L dalam tekanan rendah merah |
| Garis merah, biru dan ungu | Isothermia menjadi 850 hPa setiap 4 ° C: merah di atas nol, biru di bawah nol, ungu nol |
| Contoh oranye | Termal reversal: udara untuk 925 hPa lebih hangat daripada di tanah, lebih intens dengan inversi kuat |

Dalam Laporan Tersangka tepi setiap tab mengatakan aturan dalam kekuatan: hijau Baik, merah Scarta, abu-abu tidak ada aturan.

Apa kotak lilac ditutup pada 45 °. Lalu lintas jauh dari kepentingan Anda QTH Anda untuk memahami di mana penyebaran terbuka, tetapi garis, ratusan kilometer panjang dan sering puluhan, menutupi peta dan menyembunyikan apa yang penting bagi Anda: pembukaan Anda dapat mengeksploitasi dari stasiun Anda. Untuk alasan ini, di luar mahkota terpilih, program tidak menggambar garis tapi mewarnai kotak locator (2 ° × 1 °) dari stasiun yang terlibat. Hasilnya adalah peta dari "zona aktif": di mana banyak kotak lilac terdekat muncul, ada saluran atau pembukaan, bahkan jika tidak perhatian Anda secara langsung; Jika luas melebar ke arah Anda, itu adalah sinyal untuk menunjuk antena ke arah itu. Papan dashboard 45 ° memungkinkan Anda melihat di bawah indeks tropo dan kisi-kisi, jadi membandingkan sekilas perkiraan (warna bawah) dan lalu lintas nyata (illus). Kotak QTH Anda tidak pernah berwarna, karena ini adalah titik awal dan bukan daerah yang jauh. Hanya kotak yang semua di luar mahkota berwarna: sebuah persegi menyeberang oleh mahkota, atau di dalam, tetap bebas bahkan jika ada salah satu dari dua stasiun. Untuk melihat garis, hanya mematikan kotak atau memilih mahkota yang lebih besar.

Kriteria yang menentukan apa yang ada di peta dan apa yang dibuang dijelaskan satu per satu dalam dokumen 1, bagian 11 meter.

## 9. Bilah waktu

Di bawah peta: tanggal dan waktu dari prediksi ditunjukkan, kursor dari + 0 ke + 72 h pada 6 jam, legenda indeks 0-10 dengan nama sumber yang dipilih, dan waktu lomba model untuk isobar. Terompet dan indeks isobare mengikuti kursor; titik, stasiun mendengarkan dan decoding Anda adalah pengamatan saat ini dan selalu tetap mereka "adex". Ketika kursor tidak pada + 0 mereka ditekan, untuk mengingat bahwa mereka tidak termasuk ke waktu perkiraan.

## 10. Bagaimana membaca tumpang tindih

- Tekanan tinggi + indeks tinggi + titik sepanjang daerah berwarna: pembukaan ada dan yang direncanakan. Sudah waktunya untuk memanggil ke arah itu.
- Indeks tinggi tapi tidak ada titik: atau tidak ada yang aktif di sana, atau perkiraan terlalu optimis. Lapisan mendengarkan membantu membedakan: jika ada penerima di daerah tersebut dan tidak merasakan apa-apa, hipotesis kedua lebih mungkin.
- Titik lama di luar wilayah berwarna: saluran tinggi, yang prakiraan permukaan tidak melihat, seperti dalam peristiwa LA- UA6 [3]; atau penyebaran lain melarikan diri dari filter (misalnya E sporadic tanpa tag). DF5AI dan DK5YA membedakan tropo dan E sporadic juga dari jam: bukaan yang lahir dan berakhir bersama-sama dengan sporadis E hampir tidak [5] tropo.
- Hepburn dan F5LEN tidak setuju: membandingkan dua sumber dengan tombol dan melihat di mana titik-titik nyata jatuh.
- Tekanan menurun: G4MCU mengikuti pembukaan suar SK6VHF sampai tekanan menurun; 1012 hPa mempertimbangkan event [13] selesai. Perhatikan isobar dengan menggulung timebar: jika tekanan tinggi berhenti menarik, pembukaan mungkin mengikutinya.
- Sinyal yang naik: dalam studi WSPR di 144 MHz dari rasio sinyal / kebisingan tumbuh tepat di jendela di mana Hepburn memprediksi perbaikan, dan jatuh ketika perkiraan mengambil [14]. jika di jalan yang sama tumbuh, pembukaan terbentuk. KP4MD
- Mountain paths: indeks tidak memperhitungkan reliefs [1], tetapi koneksi di luar Alpen juga ada berkat sel konverktif dan gelombang gravitasi atmosfer [4], dan LA0BY bekerja 9A dan S5 melalui pegunungan tanpa Hepburn memprediksinya [3].

## 11. Buka titik

- Izin Hepburn dan F5LEN untuk menggunakan peta mereka dalam program.
- Kalibrasi koefisien konversi F5LEN skala 0-10.
- Kalibrasi titik kontrol dari segala jenis peta, untuk dibuat ulang jika sumber berubah grafis.
- Rute opsional dengan suhu di ketinggian (850 hPa) dari GFS.

## Sumber utama

1. W. R. Hepburn, DX Info Centre, "The Hepburn Tropo Index (HTI)" dan "Tropospheric Ducting Forecast" halaman (dxinfocentre.com). Definisi indeks dan skala, nilai-nilai negatif dan lebih dari 10, pengecilan scatters dan ketergantungan, langkah 3 dan 6 jam pada 6 hari, upgrade ke 18 UTC. Asal data dari model Kanada global: G. Meisse, AMSAT-BB, 27 / 08 / 2007.
2. F5LEN, prakiraan untuk Eropa (tropo.f5len.org / forecasts-for- Eropa) dan prediksi dunia terhadap propagasi tropopepsic VHF (tropo.f5len.org / WW) dan dicatat "Versi baru dari alat ramalan troposfir VHF". Unit indeks N, 40 langkah dari 3 jam, 4 update per hari, nilai dan locator per klik.
3. S. Heck LA0BY, "Extreme Tropo Propagration on 144 MHz and up," 41 ° Nordic VUSHF Meeting, Skjeberg 2019. Gradien normal 6.5 ° C / km dan inversi hingga 10- 15 ° C / km, suhu dan kelembaban peran, tekanan tinggi stabil, LA- UA6 acara terlihat dalam suhu tinggi dan tidak dalam Hepburn, LA- 9A / S5 koneksi di luar pegunungan.
4. F. Egano IK3XTV, "Propagation VHF - kontribusi konverktif sel dan atmosfer gravitasi gelombang dalam penyebaran Tropo". Impor dari 3 km pertama, jalur laut, sel-sel konvektif, gelombang gravitasi di Alpen, koneksi tekanan tinggi, volume umum hingga 3- 4 km.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, "Propadasi Jarak Sangat Panjang di 144 MHz Band", 24 / 7 / 2003. Profil vertikal dari suhu dan titik embun untuk mengenali inversi, penggunaan indeks Hepburn, perbedaan antara tropo dan E sporadic menurut jam.
6. J. N. Gannaway G3YGF, "Propagasi Perkembangan Troposfer", QST November 1983. Radius bumi setara dan kehilangan jalur yang digunakan untuk indeks dan kartu titik (lihat dokumen 1).
7. The VHF / UHF DX Book, bab 2 "Proporasi Troposfir", pp. 2- 23... 2- 30. Gambar dari zona refraktif menurut dN / dh (standar Kore39 N / km, membatasi 179 N / km dan 0) dan permukaan dan profil lakban tinggi. Teks salinan yang diberikan tidak dapat dibaca: hanya angka yang digunakan.
8. Ringkasan proyek 8 / 11 / 2026, disediakan oleh penulis program. Warna peta, proyeksi yang diperlukan, lapisan, batang waktu.
9. Natural Earth, vektor data pantai dan perbatasan, domain publik (naturalearthdata.com). Referensi umum, tidak annexed.
10. J. P. Snyder, "Projeksi Peta - Sebuah Manual Kerja", USGS Kertas Profesional 1395, 1987. Forlag of Mercator proyeksi dan peralatan ashimutale. Referensi umum, tidak annexed.
11. Rekomendasi ITUR P.453, indeks refractive radio. Refraktif formula N dan ambang batas saluran pada 157 N / km. Referensi umum, tidak annexed.
12. NOAA NCEP, Global Forecast System (GFS). Tekanan di permukaan laut pada 0,25 °, empat balapan per hari. Referensi umum, tidak annexed.
13. G4MCU, artikel dengan tag "tropospheric" (g4mcu.net / tag / tropospheric), 2018- 2025. Penolakan, saluran dan penyebaran; lebih sering saluran dengan tekanan tinggi, terutama di laut; suar SK6VHF diikuti sampai penurunan tekanan di 1012 hPa (Juni 2019).
14. C. F. Milazzo KP4MD, "VHF / UHF WSPR Propagation Studi" (qsl.net / kp4md / 144 _ mhz _ wspr.htm), 2012- 2014. SNR dari WSPR melaporkan kepada 144 MHz yang tumbuh dalam perbaikan jendela yang diramalkan oleh Hepburn dan jatuh ketika perkiraan menghilang.
15. Buka -Meteo, ramalan cuaca API (membuka-meteor.com), bebas tidak-komersial gunakan. Tekanan pada permukaan laut dari model GFS pada beberapa titik dalam satu permintaan.
16. mqtt.psreporter.info, distribusi MQTT secara langsung dari iklan pskreporter. info: nominasi dan pemilik penerima (lihat dokumen 1).
