LIVETROPOVHF dokumen teknis 1
# Operasi Program

> Terjemahan mesin dari bahasa Italia: jika ragu, panduan berbahasa Italia yang berlaku.

Apa yang program lakukan, dari mana mengambil data dan bagaimana mengubahnya menjadi apa yang Anda lihat di peta dan dalam daftar. Versi 9 November 2026, terkait dengan proyek di C # (.NET 10 + Avalonia).

## Ringkasan

- Tujuan: ramalan tropo (Hepburn [3], F5LEN [4], isobare GFS [18]) dan melihat nyata band dalam jendela yang sama, menurut daftar kontrol LA0BY [1].
- Koleksi data: dua kumpulan DX, Reverse Beacon Network [9], dm4x cluster dan aliran PSKReporter [5] MQTT, UDP decoding dari WSJT-X, JTDX dan MSHV [8].
- Spot: segmen band, filter negatif, penyebaran udara dalam kuning berlari, posisi oleh lokasi atau CALL3.T [6] [7].
- Siapa yang mendengarkan: lingkaran merah muda gelap dengan garis merah muda selalu terlihat ke arah stasiun merasa [5].
- Spot card: jarak, azmut, indeks tropo di jalan dan kerugian troposcatter dengan G3YGF [2] metode.
- Bahasa Hepburn dan F5LEN: gambar [3] dan kalibrasi bangku [4].
- Kontrol NTP pada 10 server setiap 3 menit [10] [11].
- Dicurigai laporan: EMI di Q65/JT65 diakui dengan posisi Bulan, dipotong locators, skimer, karantina dan pendengar, dengan aturan yang dipelajari oleh matahari [20].
- Kriteria untuk membuang dan makna warna dirangkum dalam dua tabel (bagian 11 meter dan 11 quater).
- Tema, 36 [17] bahasa, ekspor dokumen ke Word dengan rumus yang dapat disunting [16], program portabel dan multiplatform [12] [13] [14] [15].

## 1. Tujuan

Program ini disatukan dalam satu jendela dua hal yang biasanya terlihat di situs yang berbeda: prediksi dari penyebaran tropoper (Indeks Tropo Hepburn dan F5LEN, isobare) dan pengamatan dari apa yang sebenarnya terjadi pada band (spot dari DX cluster, Reverse Beacon Network dan PSREW, ditambah daftar stasiun yang mendengarkan). Referensi band adalah 144 MHz; iklan mengumpulkan dari 50 MHz up.

Pengaturan berikut daftar kontrol diusulkan oleh LA0BY untuk peristiwa tropo ekstrim: di hari sebelumnya Anda melihat pada Hepburn dan F5LEN forecast dan profil suhu di ketinggian, sementara acara ini sedang berlangsung Anda mencari pada DX cluster dan di tempat peta bukti bahwa pembukaan mulai [1]. LA0BY juga mengamati bahwa satu-satunya peta Hepburn / F5LEN tidak cukup untuk memprediksi koneksi terpanjang [1]

## 2. Jendela dan tab

Jendela adalah satu dan tidak dibagi ke jendela mobile. Dari atas ke bawah:

- Baris pengaturan umum: Panggil dan Locator di sebelah kiri; di sebelah kanan jam UTC (mis 08 / 11 / 2026 20: 33: 38 UTC, teks UTC, NTP: OK atau NTP: GAGAL, menu tema dan bahwa bahasa.
- Jumlah tab dalam lima kelompok: Peta dan titik; Tanda depan (Propria Castrage, perkiraan dihitung oleh program dan dijelaskan dalam Manual Forecast, lalu Hepburn dan F5LEN); Graves; Data dan sumber (DX cluster, PSK, Beacon terbalik, UDP, Apcentments dan Laporan yang mencurigakan); Guides (Operasi Program, Pembuatan Peta, Cara membentuk tropo, perkiraan manual, Tips ke operator, dengan kotak pencarian yang menghitung panduan di mana teks muncul). Di bawah balok bar menunjukkan sub- bahasa kelompok; setiap kelompok mengingat yang terakhir digunakan.
- Isi tab dan, di bagian bawah, status bar dengan keadaan sumber, basis data nama dan waktu update terakhir.

Pengaturan baris atas diterapkan pada semua tab: tema yang dipilih juga mewarnai koleksi data dan latar belakang dokumen; Halaman dokumen tetap putih seperti lembaran, sehingga mereka mencetak dengan baik.

## 3. Panggil dan tuan tanah

Pemilik rumah Maidenhead dapat memiliki 4 atau 6 karakter (misalnya JN70 atau JN70CN). Program ini mengubahnya ke koordinat pusat kuadrat: field 20 ° 10 °, kuadrat dari 2 ° 1, subquadrat dari 5 × 2.5. Jika teks bukan locator yang valid tepi kotak menjadi merah dan stasiun tidak muncul di peta. Locator juga berfungsi sebagai pusat dari proyeksi azimuth dan sebagai titik awal untuk jarak dan azimuth di papan iklan.

Panggilan adalah nama yang digunakan sebagai login cluster. Pada awal mula, sampai ada nominasi yang valid, cluster tidak terhubung: jendela meminta nama (dan, mungkin, tuan tanah); dikonfirmasi, nama disalin sebagai nama pengguna dalam kolom log masuk setiap cluster yang memiliki kosong dan cluster pergi. Dengan "Kemudian" tetap diam, dan tombol koneksi setiap kelompok membutuhkan nominasi lagi. MQTT, UDP dan NTP tidak menggunakan login dan pergi pula.

## 4. Koleksi data: sumber

Setiap sumber memiliki bagian sendiri dalam tab Data Collection, dengan Terhubung / Terputus, yang Dikenal dan Terhubung kotak pada startup, status, meter dan register dari baris yang diterima. Unbanded atau dibuang baris dari filter tidak muncul di register: hanya menghitung meter bagian. Bagian adalah berdampingan dengan dua sampai dua.

| Sumber | Koneksi | Apa yang membawa |
| 1. Pilihan DX cluster | on0dxk.dyndns.org: 8000 perintah set / filter dxbm / pass 6 | Spot radio amatir, juga SSB dan CW. Pada ON0DXK VHF disaring dan diaktifkan dengan perintah yang ditunjukkan (poi lewat 4, 2...). |
| 2. Sumber kedua | dxmaps.com: 7300 (DXMAPS, EA6VQ) dan s50clitx.si: 41112 ( S50CLX, Slovenia) | DXMAPS mengumpulkan titik-titik VHF dan juga memberikan titik-titik skimmer RBN (set / skimmer kontrol, sudah diatur); S50CLX adalah cluster DX Spider. Login dengan nama Anda; alamat yang dapat disunting. |
| 3. Jaringan Beacon Balikkan | telnet.reversecon.net 7000 CW / RTTY FT8 | Titik skimmer otomatis. Login dengan nama hanya [9]. |
| Gugus dengan PSKreporter | dm4x.ddns.net: 8500 | Digital Spots dari PSKReporter dalam format cluster. |
| 5. | mqtt.psreporter.info 1883 | Setiap laporan penerimaan yang nyata: yang mentransmisikan, yang mendengarkan dan mereka [5] Locators. Bands 144, 222 dan 432 MHz. |
| 6. Peta cuaca dan tropo | dxinfocentre.com / tropo _ eur.html tropo.f5len.org / forecasts- for- Eropa GFS model | Hepburn [3] dan F5LEN [4] peta tropo, tekanan permukaan laut untuk isobar (GFS melalui Open- Meteo [18]). Hepburn dan F5LEN harus diijinkan untuk penulis. |

Cluster telnet (sumber 1-4). Setelah koneksi menunggu permintaan nama (teks yang memuat login, panggil:, callsign atau panggilan Anda); jika tidak tiba dalam waktu 4 detik, silakan kirim log masuk. Kemudian mengirim perintah dari bagian, dipisahkan dari titik dan koma, satu setiap 0,6 detik. Karakter kontrol telnet dihapus sebelum membaca baris. Jika koneksi terputus dan Riconetti aktif, coba lagi setelah 10 detik (15 jika server tidak merespon sama sekali) [6].

PSKReporter MQTT (sumber 5). Program ini menampilkan pskr / filter / v2 / {banda} / {mando} / {tx _ call} / {rx _ call} / {tx _ grid} / {rx _ grid} / {tx _ dxcc} / {rx _ dxcc} dengan memperbaiki hanya band dan meninggalkan bidang lain bebas dengan karakter + [5]. Nama yang tepat untuk server mengindikasikan bandnya (misalnya 2m, 1.25m, 70cm) harus diperiksa pada sambungan pertama.

## 5. Dari garis cluster ke tempat

Satu baris adalah titik ketika memiliki bentuk DX de SPOTTER: frekuensi DX komentar hhmmZ. Program ini menghasilkan spotter, DX stasiun, frekuensi di kHz, komentar dan sekarang UTC; nama melepas akhiran skimmer (- #, 1...). Kemudian menerapkan, dalam urutan ini, aturan berikut [6].

| Band | Segmen diterima |
| Graves (dengan 144) | 143.040 - 143,060 MHz |
| 144 MHz | 144 - 148 MHz |
| 222 MHz | 220 - 225 MHz (hanya wilayah 2 ITU, Amerika) |
| 432 MHz | 420 - 450 MHz |
| 1296 MHz | 1240 - 1300 MHz |
| 2320 MHz | 2300 - 2450 MHz |
| 10 GHz | 10,5 GHz |

1. Band. Frekuensi harus jatuh ke salah satu segmen dikelola, semua sisanya, dan khususnya setiap komersial di bawah 49 990,0 kHz, tidak ditampilkan dan dihitung sebagai "keluar dari band".
2. Way. Salah satu cara terbaik untuk berkomentar adalah FT8, FT4, Q65, JT65, ISCAT, CW, SSB, FM, RTTY, PSK31, MSK144, FSK441, JT6M, USB dan LSB menjadi SSB.
3. Filter negatif. Titik ini dibuang jika berisi mode MSK144, FSK441, JT6M, JTMS, ISCAT, atau tag < AUE > < MS > < FAI > < F2 >, < >, dengan uppercase tidak-sensitif dan bahkan ruang dalam kurung. Ini adalah cara dan penyebaran yang bukan tropo: Menyebarkan meteor, Aurora, dan sporadis, F2.
4. Airscatter. Jika komentar berisi < AS > atau < ACS > (juga dicampur, misalnya < As >) atau menyatakan tempat sebagai airscatter (airscatter, airscatter, airplane scatter), spot tetap tetapi digambar dalam kuning dengan garis bertitik gelap (dark wavy untuk tetap dapat dibaca pada peta jelas) dan disorot dalam daftar.
5. Lokasi stasiun. Jika dalam komentar ada dua locator (misalnya JN54 < JN83) yang pertama adalah dari mereka yang membuat tempat dan kedua dari stasiun DX; dengan satu locator, itu dari stasiun DX. Apa yang hilang dicari di CALL3. TXT, hanya dengan nama yang tepat (untuk laptop lihat di bawah). Jika stasiun tetap tanpa posisi, tempat muncul dalam daftar dengan catatan "posisi tidak diketahui" tapi tidak pada peta.
6. Dobel. Sebuah iklan dengan DX yang sama, pengintai yang sama, band yang sama dan sumber yang sama dari salah satu yang telah hadir menggantikan itu, bukan menambahkan.

CALL3.TXT adalah basis data nama Alatlocator yang digunakan oleh JTDX, dalam format CALL, LOCATORE,,,,,. Berkas proyek berisi 1 048 847 nama, semua dengan locator valid; header dari berkas masih berbicara dari 34 323 nama karena tetap bahwa dari edisi pertama [7]. Hal ini dimuat di latar belakang saat startup, sehingga jendela segera dapat digunakan. Berkas dicari di sebelah program, dalam folder Data, dalam folder dari mana program dimulai dan satu atau dua folder di atas; Jika tidak, jendela memberitahu ketika Anda mulai dan bilah status sinyal itu dalam merah. Namanya selalu menggunakan 6-karakter. Hanya aturan untuk pencari: jika yang diterima (titik, MQTT, decoding, QSO deburti) memiliki kurang dari 6 karakter dan CALL3. TXT memiliki satu sampai 6, selalu bernilai CALL3.TXX IC8TEM; untuk laptop (F /, IC8TEM/P) hanya suara yang tepat: tanpa, stasiun tetap tanpa tuan tanah dan tidak dirancang di tempat lain. Aturan juga berlaku pada saat menggambar, sehingga data tiba sebelum database dimuat akan berjalan sendiri.

Satu basis data: CALL3.TXT Dari 1.3.4 semua locator berada dalam satu berkas, CALL3.TX. yang tetap dalam format JTDX dan WSJT-X (CALL, LOCATORE,,,,,) dan dapat digunakan karena juga dalam WSJTT-X, JTDX dan MSHV. Tidak ada lagi file terpisah. Di bagian atas berkas, setelah header, ada dua blok ditandai oleh baris komentar: / / [LIVETROPOVHHF -MANUALI] dengan masukan dimasukkan dengan tangan dan / / [LIVEROPOVHF- CLUSTER] dengan koreksi dari cluster, kemudian / [LIVETROVHFF- FONTI] dengan semua item lain dalam urutan abjad. Setiap nominasi muncul sekali. Dalam Pertemuan Data dan Sumber Sublingual (CALL3) Anda mencari sebuah nama, Anda menulis pemilik dan Anda menekan Tambah / perubahan: suara masuk blok dari suara dengan tangan dan itu bernilai di atas semua, bahkan di atas locator yang dikirim stasiun itu sendiri. ♫ Hapus nominasi dari CALL3.TXT (update berikutnya dilanjutkan dari sumber, jika mereka memilikinya). Tambahkan sebuah berkas... baca daftar CALL, LOCATOR (Anda juga menerima titik dan koma, tab dan ruang) dan tuliskan itu di antara suara-suara yang dipilih; berkas yang dipilih tidak disalin. Muat ulang CALL3.TXT dari disk, setelah mengeditnya dengan penyunting. Urutan penuh sumber: locator sendiri, suara tangan, koreksi cluster, sisa CALL3. TXT (sumber, sumber belajar lokasi hidup dan G7VRD), menerima locator (yang berlaku hanya jika memiliki 6 karakter dan tidak berakhir dengan LL atau MM).

Perbarui CALL3.TXT Dalam bahasa yang sama tombol Update sekarang mengunduh sumber (CALL3.TXT. X-X yang diimplementasikan, CALL3.TXT EQ W7GJ, grid _ data.bin dari JTDX), bergabung dengan mereka (yang pertama yang memiliki nominasi menang), periksa bahwa mereka setidaknya 50.000 nama (jika tidak ada perubahan), simpan CALL3. TXT CALL3 sebelumnya. TXT entri tetap, dan 6-karakter locator tetap di mana sumber hanya 4 dari persegi yang sama. Suara tangan dan koreksi dari cluster tetap di atas, tidak berubah. Dengan kotak Update sendiri setiap hari, cek berlangsung di awal. Program ini menulis ulang CALL3. TXT segera setelah setiap suara dengan tangan dan, untuk belajar lokasi, di hampir setiap 10 menit dan pada penutupan; CALL3. TXT paket secara baca-saja dan atribut lepas landas sendiri. Linux installer jangan menimpa CALL3. TXT sudah ada, bahwa untuk Windows menginstal hanya jika hilang. ALL3

Koreksi otomatis dari cluster. Dalam DM4X dan RBN otomatis noda komentar membawa dua locator, misalnya KN09tp < > KN09: pertama adalah apa stasiun yang bintik menyatakan dirinya, untuk 6 karakter; yang kedua adalah bahwa stasiun merasa, sering hanya untuk 4. Jika karakter pertama memiliki 6 karakter (dan tidak diakhiri dengan LL atau MM) dan berbeda dari basis data tersebut, program mengoreksi CALL3. TXT saja: koreksi berjalan di blok / / [LIVEROPOVHF-CLUSTER], dengan tanggal konfirmasi terakhir di bidang ketiga, valid di atas sumber-sumber (di bawah suara tangan) dan muncul antara sinyal mencurigakan diperbarui sebagai "CALL3 ". Jadi itu bertahan CALL3. TXT update. Sebuah koreksi yang belum dikonfirmasi selama dua tahun kehilangan mendahului dan tetap sebagai suara normal.

Sumber terakhir: G7VRD. Jika nominasi tidak memiliki karakter 6 di CALL3. TXT, program ini menayangkannya di latar belakang API publik G7VRD (api.g7vr.co.uk, "locator oleh callsign"), yang mengumpulkan locators secara otomatis dari jaringan lalu lintas. Anda hanya menerima karakter 6 -karakter yang bukan pusat dari kuadrat (LL atau MM); permintaan dikelompokkan, pada 40 maksimum nominasi per menit, dan setiap nominasi bertanya sekali sehari, karena itu adalah layanan hobbistik tanpa jaminan. Locator yang ditemukan memasuki CALL3. TXT jika nama tidak ada atau hanya memiliki 4 karakter dari kuadrat yang sama; pencarian mematikan dengan kotak dalam nama sublinugal.

Pertemuan dengan bar. Dalam topik PSKReporter MQTT, bar memisahkan tingkat, maka nama senyawa datang dengan titik bukan bar ( IV3. HB9CAT). Program menempatkan bar kembali dan, jika pesan berisi nama lengkap, gunakan: IV3/HB9CAT tetap IV3/HB9CAT pada peta, daftar, dan basis data.

Pelacak belajar langsung. CALL3. TXT tidak berisi semua: misalnya SP9EMF dan SP9NVY tidak ada di sana, dan kemudian tetap 4-karakter locator yang dikirim oleh PSKER (kartu mengatakan demikian). Untuk mengurangi kasus-kasus ini program menulis di CALL3. TXT setiap 6 karakter yang datang dari sumber apapun (PSKRER, seperti pemancar atau penerima, cluster, UDP decoding) untuk nominasi yang CALL3. TXT tidak atau hanya memiliki 4 karakter dalam kuadrat yang sama; tidak pernah mengubah suara ke 6 karakter. Sampai 1.3.3 locator ini berada di Data / LOC6.TXT

Transisi dari 1.3.3. Pada awal pertama dari 1.3.4 berkas terpisah dari versi sebelumnya (Dati / CALL3 -MANUALE.TX ALL3, Data / C -AGGIUNTE, Data / CALL3 -CLUSTER. TXT, Data / LOC6.TXT dan Data / CALL3 - G7VRD.TXT) bergabung ke CALL3. TXT dengan entri yang sama. Melepaskan penulisan, berkas lama mengambil ekstensi.fuso, Anda tidak lagi membaca dan Anda dapat menghapus.

Penerima otomatis. RBN, dm4x dan PSKER sering melaporkan nama dengan akhiran mengindikasikan penerima: -SDR, / SDR, - # (skimmer RBN) atau -1, -2... (Dan demi rombongan yang menggiring dengan sebenar-benarnya) demi para malaikat yang menggiring atau mengarak awan. Untuk pemilik akhiran yang dihapus: OK2OZL -SDR mengambil OK2OZL JN89UE dari CALL3.TXT jika stasiun belum mengirim locator karakter 6-karakter. Nama yang ditunjukkan tetap satu dengan akhiran.

Lokasi dipotong. Tidak semua sumber memperlakukan ekstensi pemilik mereka: banyak yang mengaturnya ke 4 karakter, dan beberapa otomatis karry-atas sistem sengaja memotong locator 4-karakter, bahkan mereka yang memperoleh data dari mendengarkan nyata FT8, FT4, Q65 dan JT65, atau menyelesaikannya dengan pusat alun-alun (LL atau MM, misalnya JN89MM). Untuk alasan ini, karakter 4 atau 6 karakter yang berakhir dengan LL atau MM, digantikan oleh CALL3. TXT dari alun-alun yang sama. Jika lokasi yang benar dan pusat alun-alun tidak bertepatan dengan beberapa lusin km, kesalahan tetap kecil dibandingkan dengan jarak tropo.

## 6. Siapa yang mendengarkan

Setiap pesan MQTT dari PSKReporter melaporkan nama dan lokasi dari mereka yang menerima [5], jadi Anda tidak perlu mencarinya di CALL3.TXT Untuk band yang dipilih dalam pemilih program berlaku, untuk masing-masing menerima stasiun: pemilik, penerimaan terakhir, cara didekode, jumlah stasiun merasa dan jarak maksimum. Sebuah stasiun meninggalkan daftar ketika tidak lagi menerima apa pun dalam kisaran terpilih (3, 5, 10, 15, 30 atau 60 menit).

Pada peta setiap penerima adalah lingkaran kosong pink gelap, dan dari setiap mulai garis merah muda penuh terhadap stasiun yang merasa dan mengirimkan kembali ke bersih. Garis selalu terlihat, tanpa mengklik. Pertemuan stasiun mendengarkan, mereka mendengar dan dua kepala dari setiap tempat selalu ditulis pada peta, tanpa harus klik (nama yang sama pada titik yang sama hanya ditulis sekali). Dengan mengklik lingkaran garis menjadi lebih tebal dan membuka tab dengan rincian. Jika locator dari MQTT memiliki kurang dari 6 karakter, program menggunakan CALL3.TXT (sekitar satu juta nama), asalkan konsisten dengan kuadrat yang diterima.

Menyebarkan meteor. Seperti dengan bintik-bintik cluster, rasio MQTT dalam MSK144, FSK441, JT6M, JTMS atau ISCAT dibuang dan dihitung saja; hal yang sama berlaku untuk decoding UDP dalam cara ini.

Bukan pendengar asli. Beberapa stasiun menyatakan menerima di 144 MHz tapi pada kenyataannya penerima ada di band lain (indikasi frekuensi salah): laporan mereka akan merancang bukaan yang belum ada ribuan km. Program ini memeriksanya sendiri. Setiap stasiun yang didengar pendengar dalam VHF diikuti selama 30 menit pada semua band, dengan sebuah prasasti MQTT yang didedikasikan (pskr / filter / v2 / + / {nominasi} / #) [5]. Melayani sebagai bukti. Jika setidaknya dua dari stasiun mendengar oleh pendengar, dalam menit yang sama (= 10), aktif pada band lain, pendengar dianggap tidak asli dan semua VHF laporan yang dibuang, register MQTT melaporkan nama dan stasiun yang mengkhianatinya. Dalam daftar bintik-bintik, di samping meter, tombol MQTT menunjukkan keadaan aliran (daun hijau jika terhubung, merah jika tidak) dan berapa banyak pendengar yang dibuang dalam band dan jangkauan dipilih; dengan mengklik membuka tab dengan daftar diskon dan, untuk masing-masing, stasiun ditemukan pada band lain.

Koneksi panjang Cina. Sebuah link lebih dari 1000 km (Cluturing Spot, MQTT Mendengarkan, QSO Desunti) tidak segera ditampilkan: tetap ditahan, dan konter "diselenggarakan > 1000 km" mengatakan nomor tersebut. Ini diungkapkan ketika koneksi lain, antara stasiun yang berbeda dan setidaknya setengah bersama, menegaskan dengan salah satu kriteria ini: sama stasiun di kepala (misalnya pendengar yang sama) dan stasiun lain dalam arah yang sama dalam 10 °; koridor yang sama, dengan kedua kepala dalam 200 km dari jalur lain; rute paralel, dengan arah dalam 10 ° dan jarak sisi dalam 200 km, overlapped sepanjang rute. Keduanya muncul dan log MQTT menunjukkan alasannya. Dekoding UDP selalu terlihat dan diterapkan sebagai konfirmasi bagi orang lain. Koneksi tersimpan tetap dalam memori selama 60 menit.

Tidak ada di bawah 144 MHz. Program ini bekerja dari 144 MHz up: segala sesuatu di bawah 143 MHz dibuang. Pengecualian adalah GRAES, radar Perancis yang mentransmisikan dalam CW pada 143.050 MHz oleh Broye- Lès- Pesmes ( JN27SI): sebuah iklan antara 143.040 dan 143.060 MHz, atau dengan GRAVES dalam nama, muncul antara 144 MHz dengan radar sebagai pemancar. Papan iklan mengatakan jika penerima berada di dalam balok (sektor D, C, A dari 90 ° sampai 270 ° azmut terlihat dari radar, 6 langkah dari 7,5 ° per langkah; tab GRAVES, seperti di YES, menunjukkan langit dilihat dari radar: sektor, langkah bercahaya pada saat itu (sesenza PE1ITR 2021: 0,8 s per langkah, siklus 4.8 Data dan geometri berasal dari YAMES. Merasa itu di trope sulit, tetapi dapat terjadi.

Rehabilitasi dari kelompok manusia. Sebuah stasiun yang juga muncul di band lain tidak dihitung sebagai tersangka jika, dalam menit yang sama (= 30), DX gugus manusia ( ON0DXK, GB7RAU; bukan dm4x, RBN atau sumber otomatis lainnya) kerusakan aktif pada band tersebut, di tempat yang dibuat oleh nama lain: ini adalah kasus stasiun yang benar-benar bekerja pada beberapa band bersama-sama. Jadi pendengar yang dibuang kembali terlihat (register MQTT menulis "direhabilitasi") dan link lama ditahan untuk band rendah dapat dibuka.

Stasiun aktif di bawah 144 MHz. Dalam karantina koneksi panjang, stasiun yang dalam menit yang sama didengar pada band di bawah 144 MHz (dari MQTT laporan band lain) tidak mengkonfirmasi koneksi lain dan tidak terselubung, meskipun rute yang sama atau paralel akan mengkonfirmasi itu.

## 7. Daftar iklan

Ini menempati 35% kanan dari tab peta dan tempat. Judul: Judul, pemilih band (50, 70, 144, 222, 432, 1296, 2320 MHz, 10 GHz), range selector (terakhir 3, 5, 10, 15, 30, 60 menit) dan update key. Di bawah, empat meter: tempat terlihat, AS / ACS, dibuang dari filter, stasiun mendengarkan.

Setiap baris menunjukkan jenis baris, waktu UTC, DX GRODE, jalan, jarak di km dan, pada baris kedua, sumber, frekuensi dan komentar. Yang terbaru adalah di atas. Satu klik pada baris memilih titik: pada peta garis tumbuh dan dua nama muncul.

## 8. Spot kartu dan kerugian troposcater

Untuk tempat yang dipilih tab menunjukkan lokasi, jarak, azmut, frekuensi, modus, sumber, sekarang, komentar, rata-rata indeks tropo sepanjang path dan perkiraan kerugian rute untuk tropo tersebar.

Jarak dan azmut dihitung pada lingkaran maksimum dengan radius Bumi 6371 km (rumus emisenoverso untuk jarak, awal azmut dari stasiun DX untuk pengintai).

Kehilangan troposcatter. Kehilangan dasar, untuk dua stasiun di tanah yang mulus, adalah jumlah kehilangan ruang bebas dan penyebaran kehilangan: G3YGF [2]

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

Kerugian berada di dB, D adalah jarak di km, jika frekuensi di MHz, maka sudut difusi di derajat.

8497 km adalah radius setara Bumi (4 / 3 dari yang fisik). Setiap stasiun:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

dengan Ketinggiandarikendala yangmembuatdaricakrawala sehubungan stasiun dan jaraknya. Setiap tingkat cakrawala biaya sekitar 10 dB [2]. Program ini tidak tahu cakrawala stasiun, jadi ini menunjukkan kerugian dasar dengan CVA = Gibb = 0 dan menyatakan itu ("tanah halus"). G3YGF menambahkan istilah iklim, dimana N adalah reaktivitas terhadap tanah [2]; akan digunakan ketika N tersedia dari model cuaca:

```math
L_{N} = 0,2 \cdot (N - 310)
```

Verifikasi: untuk 110 km ke 144 MHz rumus memberikan 166,5 dB, terhadap sekitar 168 tempat tidur dB pada grafik artikel. Untuk 500 km ke 144 MHz memberikan sekitar 206 dB. Rasio sinyal / kebisingan yang diharapkan adalah kapasitas dari sistem (eirp + sensitivitas efektif dari penerima) minus kerugian: dengan G3YGF 144 MHz model sistem (198 dB) sebuah jalur 500 km akan memberikan sekitar By8 dB di SSB [2], itu adalah sambungan hanya dengan penyebaran yang lebih baik dari penyebaran murni.

## 9. Dekoding saya: UDP dari WSJT-X, JTDX dan MSHV

WSJT-X, JTDX dan MSHV dapat mengirim UDP setiap baris yang mereka pecahkan. Program ini mendengarkan pada 127.0.0.1: 2237 dan mengulangi paket yang sama, byte per byte, pada 127.0.1: 2238, sehingga program lain yang menggunakan port tersebut (GridTracker, JTAlert, sebuah logger) terus bekerja jika Anda memindahkannya pada 2238. Alamat dan pintu berubah pada sublinual UDP dari Data Collection; sebagai alamat mendengarkan Anda juga dapat menggunakan kelompok multicast (mis. 224.0.0.1), jika program decoding diatur seperti ini.

Paket mengikuti protokol jaringan WSJTX: sebuah nomor ajaib awal 0xADBCCBDA, nomor skema dan tipe pesan. Program terutama menggunakan tiga jenis [8]:

- Heartbeat: mengatakan program mana yang transmisi data dan dengan versi apa.
- Status: frekuensi dial dan mode; dari frekuensi yang Anda dapatkan band (dengan tabel yang sama dari bintik-bintik).
- Decode: Sekarang, SNR, buang waktu dan frekuensi, sopan dan teks pesan yang diterjemahkan.

Dari teks Anda mendapatkan stasiun Anda mendengar: di CQ 9 A2PQR JN83 adalah stasiun setelah CQ (menyimpan DX, EU, TEST...), dalam IK0JKL IT9MNO JM68 adalah yang kedua, yaitu yang mentransmisikan. Locator adalah bahwa pesan, jika ada (4 karakter, berbeda dari RR73); jika tidak, Anda cari dalam CALL3. TXT. Mereka mendengarkan baris: stasiun yang penerima Anda dengar sekarang. Pada peta mereka menjadi garis merah dari QTH Anda ke stasiun, dengan nama; lapisan menyala dengan kotak Decoded dari saya dan membutuhkan locator valid. Dalam daftar di sebelah kanan counter memberitahu berapa banyak stasiun yang Anda decoding pada band yang dipilih.

QSO devunti dal live. Ketika decoding adalah pesan penutup dalam bentuk C ALL1 C ALL2 RR73 (atau 73 atau RRR), misalnya IC8TEM IK3FHP RR73 atau IK3FKP SV8PEX 73, dua stasiun telah menyelesaikan link. Program ini mencatat sebagai QSO, dengan locator masing-masing diambil dari pesan terakhir di mana ia menyatakan atau, jika tidak, dari CALL3.TXT [7], dan menariknya di peta sebagai garis hijau antara dua stasiun (struktur "QSO devunti dal hidup"). Sepasang stasiun dihitung sekali setiap band; band dan jangkauan yang dipilih dalam daftar valid.

Dalam bahasa sub-yang sama, panel Debug menampilkan teks dari setiap paket yang diterima (tipe, waktu, nilai, dan pesan), pada permintaan juga heksadesimal bytes, dan sebuah baris untuk penerusan. It 's to check that the decoding program is actually sending the data.

## 9 bis. Bahasa Hepburn dan F5LEN

Masing-masing menunjukkan apa yang program mengambil dari sumber. Tombol "Baca halaman dan unduh semua gambar" membuka halaman yang diindikasikan (berdasarkan dxinfocentre.com / tropo _ eur.html untuk Hepburn [3] dan tropo.f5len.org / forecasts- for- efope untuk F5LEN [4]) dan mengunduh semua gambar cache di samping executable; Anda juga dapat menempelkan alamat dari satu gambar. Untuk setiap gambar ada kotak untuk mengecualikan dan lapangan dengan waktu meramalkan. 31 Eropa gambar F5LEN dinamai pada saat lomba model (03.png... 48.png setiap 3 jam, 54... 96 setiap 6 jam, 108... 192 setiap 12 jam) dan program membutuhkan waktu dari nama file [4]; karena Hepburn itu layak urutan halaman. Dalam tab F5LEN, sebuah menu memilih wilayah: Eropa (tropo.f5len.org / forecasts- for-erope), Australia (tropo.f5len.org / Australia, vk03.png... vk192.png) atau Atlantik antara Cape Verde dan Irlandia (tropo.f5len.org / atlantic-ocean / atlantic-d4ei, atl3.png... l0png... Setiap orang memiliki langkah yang sama.

Di bawah ini adalah konter kalibrasi. Satu klik pada gambar menambahkan titik kontrol (locator atau lat, lon: Anda memerlukan setidaknya 6) atau warna legenda dengan nilainya. Program ini menghitung pixel koordinat yang cocok dengan koordinat berikutnya dengan interpolasi polinomial pada kotak [19] terkecil, membaca warna dengan toleransi yang dipilih dan, untuk F5LEN, mengkonversi unit N dalam skala 0-10 dengan a + b. "Simpan kalibrasi dan coba baca" tulis berkas dalam folder Taratur dan periksa pada pixel mana locator Anda jatuh. Sebelum mengatur kalibrasi, izin harus diminta untuk penulis peta.

Regions. Setiap sumber memiliki lebih banyak wilayah, masing-masing dengan gambar nama yang diperbaiki: Maafkan aku. Tab dari setiap sumber menunjukkan pertama dari semua pratilik fusi: semua gambar diunduh disatukan seperti pada peta utama, dengan panel bertitik dan nama dari setiap daerah, kerudung abu-abu di mana tidak mencakup daerah dan sebuah slider untuk menggulung jam; ke kiri daftar daerah, masing-masing dengan kotak untuk menggunakannya, jumlah peta dibaca dan dari mana ekstrem datang (OCR, dengan tangan atau pabrik; jika OCR gagal teks ia muncul). Kursi kalibrasi manual tetap di belakang tombol homonymous. Menu Wilayah Bench memilih tampilan, kotak "Gunakan wilayah ini pada peta" termasuk dalam indeks. Bingkai ini ditemukan sendiri (dua garis horizontal dan dua garis vertikal dari warna yang sama); untuk menempatkan gambar ini melayani lat / lon frame ekstrim: untuk Hepburn Eropa (10 ° -40 ° E, 27 ° -56 ° N) dan F5LEN Eropa (15 ° W-40 ° E, 30 ° -65 ° -65 N) telah ada dalam program, untuk daerah lain yang mereka tulis dalam empat bidang O E S N membaca label sumbu. Pengganda skala membuat dua sumber yang sebanding: Nilai akhir = multilier × nilai legenda. [3] F5LEN [4] F5LEN

## 9 meter. Ekspor dalam Kata

Dua dokumen memiliki di atas tombol "Ekspor ke Word". Berkas tersebut ditulis dalam format XML Open Office; rumus tersebut menjadi persamaan kata (OMML), jadi mereka tetap dapat diedit dengan penyunting persamaan [16]. Bila jendela simpan tak tersedia berkas berakhir di folder Dokumen di sebelah executable.

## 9 quater. Who calls me

Jika sebuah pesan dikirim kepada anda pada UDP lokal (nama pertama adalah milik anda, misalnya IC8FAX IK3FHP JN55 atau IC8FAX IK3FHP -12), program lagu dari QTH anda ke stasiun yang menyebut anda garis hitam bertitik dan berkedip, dengan nama dan locator di sebelah anda. Pelacak berasal dari CALL3.TXT (termasuk suara tangan) atau pesan. Baris tetap sampai UDP melewati penutupan QSO dengan stasiun itu ( RR73, RRR, atau 73, di salah satu dari dua arah), paling 3 menit dari panggilan terakhir; Anda menggambar atas segala sesuatu dan tidak mengikuti kriteria exclusion, sehingga panggilan langsung tidak pernah lolos.

PSKReporter, melalui MQTT, tidak mengirimkan teks pesan: mengatakan hanya yang menerima, dengan frekuensi, cara dan SNR. Inilah sebabnya mengapa panggilan langsung hanya diakui oleh decoding UDP sendiri.

## 10. Kendali waktu (NTP)

Mode digital dan waktu tempat membutuhkan jam yang tepat. Setiap 3 menit program menginterogasi 10 server NTP (SNTP pada UDP 123): 0.pool.ntp.np.ntp.np.ntp.np.np.ntp.ntp..org, it.pool.ntp.np.ntp1.inrim.it, ntp2.inrim.it, pbtime.pbb.d.de, t.t.t.d.d.goul.com. [11]

Untuk setiap jawaban menghitung pembuangan dan penundaan pengembalian, membuang nilai-nilai abnormal dan mengambil Median, dengan perhitungan pembuangan dan penundaan yang disediakan oleh SNTP [10]. Dengan setidaknya 3 jawaban setuju jam UTC dari program ini selaras dan berikutnya muncul NTP: OK dalam warna hijau; jika jawabannya tidak cukup muncul NTP: Gagal dalam warna merah dan jam tetap yang dari komputer. Jam sistem tidak tersentuh, karena perizinan administrator akan diperlukan; semua jadwal program internal (bintik-bintik, 5- 60 menit jendela, decoding) gunakan waktu rata. Kontrol selalu berubah di latar belakang: dalam antarmuka yang Anda lihat hanya hasil sebelah jam; server, interval dan respon minimum berubah dalam LIVETROVHF.ini (ntp _ server, ntp _ minutes, ntp _ min).

## 10 bis. Kriteria exclusion umum

Dua kotak di batang lapisan menerapkan semua yang Anda gambar (spot, PSKReporter, decoding, QSO desunti):

- Terkecuali di bawah 100 km. Lalu lintas antara stasiun kurang dari 100 km mungkin tidak signifikan untuk tropo (gelombang tanah, jangkauan optik): dengan on- box Anda tidak menggambar dan tidak muncul dalam daftar. Awal tidak aktif.
- Lebih dari 1000 km: hanya kotak. Sekitar QTH (lokasi untuk memulai) ada mahkota 1000 km di segala arah. Jika hanya satu dari dua stasiun dari link di luar mahkota ini, koneksi tidak ditarik sebagai baris: di tempat mereka warna lilac, bertitik pada 45 °, locator kotak (4 karakter) dari stasiun yang terlibat. Hal yang sama berlaku untuk stasiun PSKRER mendengarkan lebih dari 1000 km, yang muncul sebagai persegi bukan sebagai lingkaran. Sejauh ini lalu lintas tanpa mengisi peta garis. Tanpa locator valid menghitung panjang koneksi. Alun-alun QTH Anda tidak pernah berwarna-warni dalam ungu, bahkan ketika Anda mendengarkan stasiun di luar mahkota: adalah locator yang dispesifikasikan, nama apapun yang menggunakan yang beroperasi dari stasiun. Radius mahkota dipilih di samping kotak: 1000, 700 atau 500 km. Kotak Corone 1000 / 700 / 500 km menarik tiga mahkota di sekitar QTH, ditarik dalam ungu, dengan pilihan yang paling ditandai dan jarak ditulis utara. Dalam daftar tempat jarak yang diikuti oleh.. Awal dimulai. Ciri ini berlaku setelah karantina: koneksi panjang masih dipertahankan bahkan tidak muncul sebagai kotak.

Apa kotak lilac ditutup pada 45 °. Lalu lintas jauh dari kepentingan Anda QTH Anda untuk memahami di mana penyebaran terbuka, tetapi garis, ratusan kilometer panjang dan sering puluhan, menutupi peta dan menyembunyikan apa yang penting bagi Anda: pembukaan Anda dapat mengeksploitasi dari stasiun Anda. Untuk alasan ini, di luar mahkota terpilih, program tidak menggambar garis tapi mewarnai kotak locator (2 ° × 1 °) dari stasiun yang terlibat. Hasilnya adalah peta dari "zona aktif": di mana banyak kotak lilac terdekat muncul, ada saluran atau pembukaan, bahkan jika tidak perhatian Anda secara langsung; Jika luas melebar ke arah Anda, itu adalah sinyal untuk menunjuk antena ke arah itu. Papan dashboard 45 ° memungkinkan Anda melihat di bawah indeks tropo dan kisi-kisi, jadi membandingkan sekilas perkiraan (warna bawah) dan lalu lintas nyata (illus). Kotak QTH Anda tidak pernah berwarna, karena ini adalah titik awal dan bukan daerah yang jauh. Hanya kotak yang semua di luar mahkota berwarna: sebuah persegi menyeberang oleh mahkota, atau di dalam, tetap bebas bahkan jika ada salah satu dari dua stasiun. Untuk melihat garis, hanya mematikan kotak atau memilih mahkota yang lebih besar.

## 11. Tema dan bahasa

Tema: the 18 common to IC8TEM program, sama seperti YADNEC dan E-LAYER ANALYZER: Clear, Night, Black, eight pastel (blue, orange, aquamarine, green, pink, lilac, indigo, brown), Scia meteorika (YES palet), tiga dari IP- TRX- CTRL (Chiaro, Notte, Nero), dan Blue, Purple, Green. Dari 1.3.4 Chiaro terlihat seperti YAMEC: latar belakang abu-abu terang, panel putih berbatasan, tab malam biru dan aksen biru. Dalam tema gelap juga peta dasar menjadi gelap. Skala warna perkiraan tetap sama dalam semua tema. Pada layar sempit (di bawah 1500 piksel, misalnya 1366 × 768) jam di atas hanya menampilkan jam, jadi tema, kursor teks dan bahasa tetap terlihat. Bahasa: Wiki ke-28 dari Yasa-MS [17] (Bahasa Inggris, Perjalanan, Deutchland, Nederlands, François, Español, Danch, Norsk, Svenska, Eesti, Lietuvi, Latviequou, Espaol, Dürmaishessss, Türkageshi, Terjemahan menyangkut antarmuka; dua dokumen teknis dan pesan dari bilah status tetap dalam bahasa Italia. Nama-nama hari berasal dari pengaturan internasional. NET untuk bahasa yang dipilih. [12]

Tema dan perubahan bahasa dari dua menu di kanan atas, di samping jam, dan segera berlaku, tanpa reboot program dan tanpa mengganggu koleksi data. Pilihan disimpan di LIVETROPOVHF.ini.

## 11 bis. Dicurigai dan laporan pembelajaran diri

Tab Laporan Dicurigai mengumpulkan segala sesuatu program kontrol menemukan aneh, dikelompokkan dengan tipe dan nama, dengan 24 jam terakhir terdaftar. Program ini belajar dengan sendirinya: untuk setiap pasangan (type, name) menghitung hasil positif (+) dan sebaliknya (requicent), dan setelah 3 hasil yang disepakati menciptakan aturan otomatis; hasil kebalikan memasukkannya ke dalam pertanyaan. Keputusan pengguna (Baik, Miskin) selalu berlaku; tombol pulsante mengembalikan keputusan ke program. Aturannya ada di data / APPRENDIMENTO. TXT, di samping program; yang otomatis tidak diperkuat selama satu tahun dilupakan.

| Tipe | Kapan | Keluar + / application | Efek Aturan |
| EME ( Q65/JT65) | Q65 atau JT65 lebih dari 1000 km dengan Bulan di atas cakrawala untuk kedua kepala, atau EME dalam komentar | Setiap kali Bulan adalah umum | Abaikan: stasiun EME, Q65/JT65 s yang panjang juga dikeluarkan tanpa bulan biasa. Bagus: diperlakukan sebagai tropo |
| Koneksi panjang | Lebih dari 1000 km, dalam karantina | + unveiled, predicated tanpa konfirmasi | Baik: panjang stasiun menunjukkan segera. Buang: tidak pernah |
| Pendengar | Penerima tamu yang mendengar stasiun aktif pada band lain | Dibatalkan, + direhabilitasi oleh cluster manusia | Bagus: tidak pernah dibuang. Buang: selalu |
| Pemilik yang berbeda | Lapangan menerima berbeda dari CALL3. TXT | + setiap kali Anda mengulanginya | Bagus: stasiun bergerak, layak diterima alun-alun. Buang: vale CALL3.TXT |
| Pusat Persegi | 6 karakter dengan LL atau MM |  —  | Hanya informasi |
| Penerima otomatis | Pertemuan dengan -SDR, / SDR, - # atau -n |  —  | Hanya informasi |

EME. Q65 dan JT65 adalah cara berkomunikasi melalui Luna: Secara teori mereka jatuh ke dalam klausa pengecualian seperti meteor menyebar, tetapi mereka juga digunakan dalam tropo. Program ini menghitung posisi Bulan dengan rumus presisi rendah dari Astronomi Almanac (sekitar 0.3 °) [20]: jika koneksi dalam Q65 atau JT65 melebihi 1000 km dan Bulan berada di atas cakrawala ke kedua kepala, itu hampir pasti EME dan dikecualikan (itu dihitung di antara yang dikecualikan). Jika Bulan tidak umum, koneksi tetap mungkin di trope dan melewati karantina koneksi panjang, tetapi sinyal tetap terlihat. Ini berlaku untuk cluster, MQTT dan decoding UDP nya.

## 11 meter. Kriteria abaikan kontak, satu per satu

Program ini ingin menunjukkan hanya penyebaran tropopsic di 144 MHz dan seterusnya. Setiap data yang masuk (Cluster Row, PSPReporter Report via MQTT, UDP decoding) melewati serangkaian filter, selalu dalam urutan yang sama. Sebuah data yang dibuang tidak hilang dalam ketiadaan: dihitung (kontak yang dikeluarkan di bar atas) dan, ketika alasannya diragukan, muncul dalam tab Laporan Suspended.

|  N.  | Criteria | Dimana itu? | Apa yang dibubarkan | Kau bisa ganti baju? |
|  1  | Band | cluster, MQTT, UDP | Semuanya dibawah 143 MHz (juga 50 dan 70 MHz), kecuali Graves 143.050 MHz. Band yang diselenggarakan adalah 144, 222, 432, 1296, 2320 MHz dan 10 GHz | Tidak. |
|  2  | Satelit QO-100 | cluster, MQTT, UDP | 2400,0-2400,5 MHz dan 10489.5-10490 MHz: Ini adalah satelit geostationary lalu lintas, bukan tropo | Tidak. |
|  3  | Meteor scatter | cluster, MQTT, UDP | JT6M dan ISCAT (juga ISCATA / B), komentar dengan label penyebaran "&" dan bukan-tropoxy (< AU >, < AUE >, < FAI >, < FAI >, < F2 >) selalu. MSK144, FSK441 dan JTMS (juga dengan < MS >) hanya dalam 250 km: dalam 250 km kami terima, karena pada jarak ini mode juga digunakan dalam tropo | Tidak. |
|  4  | EME (melalui Luna) | cluster, MQTT, UDP | EME ditulis dalam komentar; Q65 atau JT65 lebih dari 1000 km ketika bulan berada di atas cakrawala untuk kedua kepala; stasiun belajar sebagai EME | Ya: aturan yang baik dalam sinyal yang mencurigakan |
|  5  | Non- Genuine Listener | MQ | Penerima PSKReporter yang mengatakan untuk mendengar di 144 stasiun aktif pada waktu itu di band lain (setidaknya dua): Hubungannya dibuang. Kembali baik jika DX cluster manusia mengkonfirmasi stasiun yang sama | Ya: baik / Miskin |
|  6  | Karantina koneksi panjang | semua baris | Lebih dari 1000 km link tetap tersembunyi sampai menemukan konfirmasi: stasiun yang sama di arah yang sama, rute lain di koridor 200 km, atau rute paralel dalam 10 ° dan 200 km sepanjang setidaknya setengah. Tanpa konfirmasi berakhir setelah 60 menit | Baik / Pergi ke stasiun |
|  7  | Di bawah 100 km | baris dan daftar | Dengan on- kotak, koneksi terpendek dari 100 km (gelombang tanah, jangkauan optik) | Box, off di startup |
|  8  | Di balik mahkota dari QTH | baris | Jika sebuah stasiun lebih dari 1000, 700 atau 500 km (sesuai pilihan Anda) dari QTH, tidak ada baris: hanya kotak lilla yang ditutupi oleh dua stasiun. Kotak QTH anda tidak pernah berwarna | Kotak dan radius, pada 1000 km |
|  9  | Jendela waktu | semuanya | Tempat tua dari kisaran yang dipilih (dari 3 sampai 60 menit) | Menu di puncak |
|  10  | Ganda | cluster | Titik yang sama (DX sama, spotter sama, band sama, sumber yang sama) diulang | Tidak. |

Catatan pada kriteria 4 dan 6. Sebuah koneksi di Q65 atau JT65 lebih dari 1000 km tanpa Common Moon adalah mungkin di tropo: tidak dibuang oleh kriteria 4, tetapi dilewatkan untuk karantina dari kriteria 6 sebagai link panjang lainnya. Panggilan langsung untuk Anda (section 9 quater) adalah satu-satunya pengecualian untuk kriteria 7 dan 8: mereka selalu menggambar.

Apa yang tidak sia-sia. Koreksi locator (CALL3. TXT dengan tangan suara, koreksi dari cluster dan belajar lokasi, akhiran -SDR, pusat dari persegi LL / MM) perubahan di mana Anda menggambar stasiun, tidak membuangnya. Sebuah stasiun tanpa pemilik, di sisi lain, tidak dapat ditarik: tetap dalam daftar dengan jarak "-".

## 11 quater. Warna dan simbol

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

## 12. Pengaturan dan portabilitas

Jendela. Pada awalnya, jendela menempati 90% dari ruang berguna layar (memotong batang aplikasi), berpusat, sehingga pada notebook 15- 16 inci dengan 125% atau 150% mengubah ukuran judul tetap terlihat. Jika ruang yang berguna dibawah 1280 × 720 poin, bagian jendela dimaksimalkan. Ukuran, posisi dan status maksimal disimpan dalam LIVETROVHF. ini pada penutupan dan akhir di awal berikutnya, disediakan batang judul masih jatuh ke layar (misalnya setelah memisahkan monitor eksternal); Jika tidak, Anda kembali ke 90%. Ukuran minimum adalah 900 × 560.

Program ini portabel: tidak pernah menulis dalam folder AppData atau dalam registry. Semuanya ada di dalam folder yang dapat dieksekusi: LIVETROVHF.ini (Pengaturan, dibuat pada penutupan pertama), CALL3.TXT (dengan suara tulisan tangan, belajar koreksi dan locator), cache peta dan data cuaca, aturan belajar (Dati / APPRENDIMENTO.TXT) dan arsip LIVEROPOVHFDb. Anda juga dapat menyimpan pada USB stick, selama folder ditulis.

Arsip Ad (SQLite [14]). Setiap tempat yang diterima dan setiap laporan yang didengar PSKREPTersimpan di LIVETROPOVHFDB, database SQLite dalam satu file. Dalam memori hanya 60 menit terakhir tetap untuk daftar dan peta; arsip mempertahankan segalanya dan melayani untuk sejarawan dan statistik (juga decoding UDP), misalnya pembukaan untuk band dan per hari, jarak maksimum, kotak dicapai, dan perbandingan belakang antara indeks diharapkan dan titik nyata. Strip yang dibuang dari filter dihitung tapi jangan arsip.

## 12 bis. Pemasangan dan pemutakhiran otomatis

Dari versi 1.0 program telah diunduh siap dari halaman Rilis github.com / ic8tem / LIVEROPOVHF, dalam paket YES yang sama. MS: installer untuk Windows (64 bit, 32 bit dan ARM, install in C:\ LIVETROVVHF tanpa hak administrator) dan portabel zip; untuk Windows 7 SP1 dan 8.1 paket terpisah (-windows7x64-x.def) Dengan paket Linux, program disalin ke awal awal dalam ~ / LIVETROPOVHF, sehingga pengaturan dan data tetap di sebelah program; pada data Mac lahir di sebelah LIVETROVHFFAPp.

Setiap 30 menit (pemeriksaan pertama beberapa detik setelah awal, segera jika program terakhir ditutup karena kesalahan) programnya berbunyi sendiri, tanpa meminta apapun, rilis terakhir. Bila versi baru mengunduh paket portabel yang sesuai untuk sistem dan mengganti berkas program pada tempat: yang dipakai diberi nama telah diubah namanya *.old-update dan dihapus pada restart berikutnya. Pengaturan, arsip, folder data dan CALL3. TXT tidak tersentuh. Versi baru aktif dari awal berikutnya: pada instalasi selesai sebuah jendela mengusulkan untuk memulai kembali segera; untuk perbaikan mendesak, ditandai [wajib] dalam Rilis, reboot tidak dapat ditunda. Status kontrol dan versi yang digunakan ditulis di bagian bawah bilah status; untuk mematikan pemutakhiran = 0 dalam LIVEROPOVHF.ini. Pada Mac aplikasi ditandatangani dan tidak mengubah dirinya sendiri: program hanya melaporkan bahwa ada versi baru.

Gambar LIVETROPOVHF dengan nomor versi muncul di awal; di akhir program meminta konfirmasi dan terima kasih untuk digunakan.

## 12 meter. Log dan bilah status galat

Kesalahan tak terduga apapun berakhir di data / errori.log, bahkan dengan catatan perhitungan mati: tanggal dan waktu, versi, sistem, tab terbuka, kesalahan lengkap dan baris terakhir dari apa yang program lakukan. Galat antar muka tak lagi menutup program: dalam bilah status muncul galat "anno disorted" (klik: buka registry). Pada permulaan berikutnya program meminta apakah untuk mengirim laporan: membuka peramban pada sinyal GitHub yang telah dikompilasi, dengan nominasi, locator dan nama pengguna PC dihapus, yang Anda periksa dan kirim dengan akun GitHub gratis. Log juga berakhir dengan setiap pemeriksaan update.

Dalam kotak status bola sumber (DX cluster, RBN, PSKReporter, MQTT, UDP, UDP, UTT, X, JTDX atau MSHV. Satu klik pada nomor versi segera memeriksa pemutakhiran.

## 13. Platform

Koper buku eksternal: Avalonia untuk antarmuka [12], MQTTnet sebagai klien MQTT untuk aliran waktu nyata dari PSPReporter [13], Microsoft. Datas.qlite untuk arsip [14].

C # on.ET 10 (LTS) [15] dengan antarmuka Avalonia, diterbitkan sebagai eksekusi independen untuk Windows 64 dan 32 bit, Mac Intel dan Apple Silicon, Linux Debian dan Raspberry Pi (64 dan 32 bit). Batas Tercatat: Windows 10 / 11; macOS 14 atau selanjutnya, dengan aplikasi untuk menandatangani atau membuka dengan benar atau terbuka; Raspberry dari Pi 2 / Zero 2 W naik (model ARMv6 tidak didukung oleh.ET).

## Terima kasih
Berkat I5MZY Massimo Zannoni untuk pengujian pada Windows 7 dan Windows 8.

Airtights: ITUR P.453, produk digital Studi Group 3 (© ITU), digunakan untuk tujuan pribadi dan non-komersial.

## Sumber utama

1. S. Heck LA0BY, "Extreme Tropo Propagration on 144 MHz and up", presentasi di Pertemuan Nordic ke-41 VUSHF, Skjeberg 2019. Troposferical Ways, Event Control List (Prakiraan Hepburn / F5LEN, profil suhu tinggi dengan Windy, DX cluster sebagai ujian pembuka), pengamatan yang hanya memperkirakan peta tidak cukup untuk rute ekstrim.
2. J. N. Gannaway G3YGF, "Propagasi Perkembangan Troposfer", QST November 1983 (dari artikel epinymous pada Radio Communication, RSGB, Agustus 1981). Forlag kehilangan ruang bebas dan difusi, radius yang sama 8497 km, situs jangka 10 dB per cakrawala, istilah iklim 0.2 & gt; N = 310), kapasitas tanaman dan rasio sinyal / kebisingan, contoh pada 144 MHz.
3. W. R. Hepburn, DX Info Centre, "Tropospheric Ducting Forecast" (dxinfocentre.com / tropo.html) dan "The Hepburn Tropo Index (HTI)". Skala indeks dan nilai yang berarti, prediksi pada 6 hari.
4. F5LEN, perkiraan untuk Eropa (tropo.f5len.org / forecasts- for- Eropa: daftar gambar dalam script halaman, legenda kelas Disturb... Duct dalam gambar) dan ramalan dunia (tropo.f5len.org / WW) dan catat "Versi baru dari alat ramalan troposfir VHF di seluruh dunia". Indeks berdasarkan reaktivitas di unit N, 40 langkah dari 3 jam, update 4 kali sehari.
5. mqtt.psreporter.info, distribusi MQTT secara langsung dari iklan pskreporter.info (kurasi oleh M0LTE, aliran P. Gladstone digunakan dengan izin). Pintu 1883- 1886, pskr / filter / v2 skema topik.
6. Ringkasan proyek 8 / 11 / 2026 dan Tes Go (core.go, gui.go) disediakan oleh penulis program. Aturan Ad, filter negatif, segmen bandwidth, manajemen kelompok telnet, tema, bahasa dan portabilitas.
7. CALL3.TXT, basis data nominasi Az JTDX (I UA3DJY, A. ES1JA script, data JTAlert / hamspots.net). Format berkas dan jumlah nama yang diverifikasi pada berkas projek.
8. WSJT-X, kode sumber, berkas NetworkMese.hpp (deskripsi protokol UDP: Heartbeat, Status, Decode). Referensi umum, tidak annexed; JTDX dan MSHV menggunakan format yang sama.
9. Reverse Beacon Network (reversebear.net), halaman layanan dari titik telnet. Port 7000 (CW, RTY) dan 7001 ( FT8), hanya login dengan nama saja.
10. D. Mills, RFC 4330 "Protokol Waktu Jaringan Sederhana (SNTP) Versi 4," IETF 2006. Format paket, perhitungan limbah dan penundaan kembali. Referensi umum, tidak annexed.
11. Projek Kolam NTP (ntppool.org); INRIM, layanan waktu NTP (ntp1 / ntp2.inrim.it); PTBTI1.pb.de). Server yang digunakan untuk kontrol waktu. Referensi umum, tidak annexed.
12. Avalonia UI, dokumentasi resmi (docs.salmoniaui.net). Antarmuka multiplatform, tema dan tata letak dari kanan ke kiri. Referensi umum, tidak annexed.
13. MQTTnet, perpustakaan NET untuk MQTT (github.com / dotnet / MQTnet). Klien yang digunakan untuk aliran PSKRER. Referensi umum, tidak annexed.
14. SQLite (sqlite.org) dan Microsoft. Datas.qlite (learn.microsoft.com). Arsip dalam satu berkas. Referensi umum, tidak annexed.
15. Microsoft,.NET 10 - didukung sistem operasi dan publikasi otonom dalam satu berkas (learn.mixoft.com / dotnet). Peron dan batas ditunjukkan pada bagian 13. Referensi umum, tidak annexed.
16. ECMA- 376 "Formasi Berkas XML Buka Kantor", bagian 1, bagian 22.1 (Bahasa Markup Matematika Kantor). Struktur berkas.dox dan persamaan. Referensi umum, tidak annexed.
17. Daftar 28 bahasa YAMS Wiki, disediakan oleh penulis program.
18. Buka -Meteo, ramalan cuaca API (membuka-meteor.com), dengan data dari model GFS NOAA. Tekanan pada permukaan laut untuk isobar (lihat dokumen 2).
19. J. P. Snyder, "Projeksi Peta - Sebuah Manual Kerja", USGS Kertas Profesional 1395, 1987, dan standar metode transformasi polinomial untuk membuat gambar. Referensi umum, tidak annexed.
20. The Astronomical Almanac, bagian D, "Rumus presisi rendah untuk bulan" (posisi presisi sekitar 0,3 °). Digunakan untuk mengenali koneksi EME. Referensi umum, tidak annexed.
