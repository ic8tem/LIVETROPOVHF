# Sumber dan rumus

> Terjemahan otomatis oleh Italia: dalam kasus keraguan layak panduan Italia.

Koleksi sumber yang bekerja berkonsultasi untuk prediksi tropo LIVETROVHF (20- 21 November 2026): Fisika dasar, data yang tersedia, indeks diterbitkan, rumus, pengalaman amatir radio, validasi dan keputusan. Rumus dinomori seperti di LaTeX, (1), (2),..., dan dalam teks mereka ingat dengan nomor dalam kurung. Buku panduan Forecast menjelaskan bagaimana program menggunakan mereka.


## 1. Tempat penulis

pengamatan IC8TEM bahwa bingkai pekerjaan, dengan umpan balik ditemukan dalam sumber.

- geografi Italia panggilan untuk difraksi. Dengan ridges, menonjol dan pulau beberapa lusin kilometer dari stasiun, diffraction kontribusi harus dihitung dengan parameter Fresnel di tepi utama, tidak hanya dengan tanah halus. Pertemuan: dalam sebuah perbandingan tidak teratur pada medan di 563 MHz (Rio de Janeiro, 27 titik antara 5 dan 30 km), metode terbaik adalah P.526 dengan ujung pisau, P.1812 dan irisan minyak, dengan kesalahan rata-rata antara September 5 dan + 3 dB dan penyebaran sekitar 9 dB; P.1546 adalah yang paling pesimis (dari Costa, anal Ramirez dan Untuk Sprem slope dekat stasiun PEtool) ingatlah bahwa mereka melayani gelombang lebar-lebar.
- Untuk masuk ke saluran jauh, Anda perlu Fresnel. Jika saluran dimulai di luar cakrawala, sinyal datang kepada kita dengan difraksi atas hambatan sampai awal; dari 1.3.2 program kalkulasikan itu (Manual dari prakiraan, td 10 meter). Pertemuan: Rohde & Schwarz (2023) mencatat bahwa stasiun sepanjang saluran tinggi sering gagal untuk masuk, sementara yang dekat dengan ujung ya.
- Kondisi berubah dengan cepat. Teori-teori yang ada, tapi kondisi berubah dalam menit, dan tidak ada sistem prakiraan mengambil data secara real time: GFS keluar setiap 6 jam, dengan rentang waktu dan 0.25 °, dan perkiraan dapat kehilangan nilai dalam waktu singkat. Pertemuan: Bray (QST, November 1961) pengukuran fluktuasi per jam sekitar 5.7 dB lebih dari 100 mil; saluran bergerak dengan waktu atmosfer (Rohde & Schwarz); register kami QSO dari 29 / 12 / 2021- 2 / 1 / 2022 menunjukkan pembukaan tumbuh dan jatuh dalam hitungan jam.
- Sekarang casting terbaik tetap menjadi jaringan DX Cluster- PSKReporter. Untuk mengetahui apa yang terjadi sekarang, cluster spot, PSKRER (dan WSPR) mengalahkan pola apapun: mereka adalah ukuran. Ramalan mengatakan di mana harus melihat dan berapa lama bisa bertahan; iklan mengatakan jika ada. Program untuk ini menempatkan titik dan perkiraan pada peta yang sama.

## 2. Tujuan

Idenya adalah indeks ketiga, dihitung oleh LIVETROPOVHF sendiri dimulai dari profil vertikal model cuaca, untuk ditambahkan ke Hepburn dan F5LEN daripada menggantinya. Hari ini program membaca peta mereka sudah siap: indeks juga akan mengatakan * mengapa * ada membuka (kuota dan ketebalan dari saluran, band minimum terjebak) dan itu bisa terjadi dengan tempat-tempat bahwa program sudah mengumpulkan.

- Apa yang dapat Anda berikan: kemungkinan saluran per titik dan per jam, berbagi dasar dan atas, ketebalan, frekuensi minimum terjebak, dan sepanjang jalan pecahan dari rute di dalam saluran.
- Apa yang tidak dapat memberikan: saluran tipis (di bawah beberapa sepuluh meter) yang model global pada 0,25 ° tidak dapat menyelesaikan, dan saluran penguapan di laut, yang berfungsi di atas semua dari GHz.
- Tujuan praktis pertama: 144 dan 432 MHz di Eropa, dengan verifikasi pada titik DX cluster, PSKReporter dan lokal decoding.

## 3. Fisika Dasar

Semuanya dimulai dari refraktif N dikalkulasi pada setiap tingkat profil: saluran dimana reaktivitas M dimodifikasi menurun pendakian, di sanalah dN / dh jatuh di bawah Mesir 157 N / km ([ITUR P.53- 13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)).

Refraktivitas (P dan dalam hPa, T dalam kelvin):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Tekanan uap dari kelembaban relatif H (%) dan suhu t (° C), di atas air antara September 40 dan + 50 ° C:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

Diubah reaktivitas (h dalam km, atau 0.157) dengan z dalam meter dan kriteria:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN / dh (N / km) | Regulasi | Efek 144 MHz |
| 0 | sub-refraksi | laju aliran berkurang |
| dari 0 ke Kore79 | normal (standar sekitar Kore40) | cakrawala radio dengan radius yang sama 4 / 3 |
| Dari Kore79 sampai 157 | super- refraksi | aliran memanjang, "terlalu bagus" |
| di bawah 115 | saluran (trapping) | sinyal terjebak, DX lebih dari 1000 km |

Bagaimana mengambil N dari model pada tingkat tekanan: untuk setiap tingkat Anda mengambil T, kelembaban relatif dan tinggi geoponensial, Anda menghitung N dan M, maka gradien antara tingkat tetangga. Tingkat di bawah tanah harus dibuang (di 1000 hPa di Alpen bawah tanah).

Frekuensi terbatas: saluran yang memandu hanya gelombang yang cukup pendek dibandingkan ketebalan nya. Hepburn menerbitkan sebuah frekuensi yang berguna dengan ketebalan tabel ([DX Info Centre](https://www.dxinfocentre.com/propagation/luf.htm)); ini adalah batas lemah, di mana sinyal secara bertahap turun.

| Led Tebal | Frekuensi minimum yang dapat dibayar |
|  61 m  | 962 MHz |
|  91 m  | 464 MHz |
|  122 m  | 276 MHz |
|  152 m  | 185 MHz |
|  183 m  | 133 MHz |
|  213 m  | 101 MHz |
|  305 m  | 53 MHz |

Dalam rentang VHF (sering 120- 300 m) nilai tabel mengikuti cukup baik hukum tentang konstan; di bawah 100 m rumus meremehkan frekuensi hingga 30-40%. Beradaptasi sendiri (bukan rumus diterbitkan oleh Hepburn), dengan f di MHz dan d di meter:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

Dalam praktek: untuk 2 m itu mengambil saluran sering setidaknya 170-180 m, untuk 70 cm itu cukup sekitar setengah. Formula yang beredar di internet, 0.040 = 0.014 (1.5), berasal dari Kerr (1951, Bab 1, eq. 18) dan valid dalam cm dan berdiri, untuk saluran di mana M jatuh linear: dengan unit-unit ini memberikan sekitar 2m 180 m dan untuk sekitar 70 cm 89 m, itu adalah nilai yang sama dari tabel Hepburn, yang berasal dari itu. Kerr memperingatkan bahwa itu bukan potongan bersih: bahkan gelombang beberapa kali lebih lama menderita banyak saluran, dan cara bahwa "kehilangan" sedikit dapat meningkatkan penerimaan bahkan dengan stasiun keluar dari saluran. Studi Akses IEEE 2019 menegaskan bahwa metode klasik (Kerr, Kukushkin) tidak menggambarkan potongan sebenarnya ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)): ambang pintu seharusnya digunakan sebagai indikasi, bukan sebagai dinding.

## 4. Data tersedia secara gratis

Titik lemah adalah resolusi vertikal: antara 1000 dan 850 model hPa memberikan tingkat setiap 25- 50 hPa, yaitu setiap 200450 m, sehingga mereka melihat dengan baik inversi ke 500- 2000 m tetapi bukan saluran tipis dekat tanah. LIVETROPOVHF telah menggunakan Open- Meteo untuk isobare, suhu 850 hPa dan inversi: indeks dapat dimulai dari panggilan yang sama.

| Sumber | Apa yang memberi | Resolusi | Gunakan untuk indeks |
| [Open-Meteo, GFS](https://open-meteo.com/en/docs/gfs-api) | T, kelembaban relatif, titik embun, tinggi geoponensial tingkat (1000, 975, 950, 925, 900, 850 hPa...); tinggi lapisan batas | 0.25 ° pada tingkat tekanan; 16 hari, jam sampai 120 h, diperbarui setiap 6 h | profil N dan M, gradien, dasar dan atas dari saluran, sudah dalam program |
| [Open-Meteo, ECMWF](https://open-meteo.com/en/docs/ecmwf-api) | tingkat yang sama dari model buka data 0.25 °; tanah memberikan titik embun |  0,25°  | pendapat kedua, berguna untuk koncordansi antara model |
| [Open- Meteo, DWRICON](https://open-meteo.com/en/docs/dwd-api) | T dan kelembaban relatif pada tingkat tekanan; di ICON- D2 ada 800, 900 dan 925 hPa | ICON- Uni sekitar 7 km | pendapat ketiga di Eropa |
| [Radiosunds, Univ.](https://github.com/NCAR/RAOBget) | ukuran profil, dua launches per hari (00 dan 12 UTC), format teks per stasiun | banyak tingkat signifikan, bahkan setiap beberapa lusin meter | verifikasi indeks; lihat saluran tipis bahwa model tidak melihat |
| [Erangan, NOAA NCEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive) | arsip historis penyiaran, sebuah berkas per stasiun, lebih turunan parameter | sebagai di atas | kalibrasi pada tahun data dan perbandingan dengan titik-titik masa lalu |
| ERA5 (Copernicus) | Reanalisis Oral sejak 1940, 37 tingkat tekanan | sekitar 28 km | statistik dan kalibrasi, bukan ramalan |

- Dengan tinggi geopotatif Open- Meteo di atas permukaan laut: tingkat yang jatuh di bawah tanah harus dibuang ([Open- Meteo, udara atas](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)).
- Di tanah ditambahkan T dan kelembaban pada 2 m: mereka memberikan peregangan pertama gradien, yang mana saluran permukaan malam lahir pada polos dan di laut.
- Siaran Italia di Universitas Wyoming archive (Weather.uwyo.edu / upperair / sound.html): stasiun dan nomor WMO di tabel di bawah ini.

| Stasiun | W | Garis lintang | Bujur | Berbagi |
| Udine / Campoformido |  16044  |  46,03  |  13,18  |  94 m  |
| Milan / Linate |  16080  |  45,43  |  9,28  |  103 m  |
| Praktek Laut |  16245  |  41,65  |  12,43  |  12 m  |
| Brindisi |  16320  |  40,65  |  17,95  |  10 m  |
| Trapani / Birgi |  16429  |  37,92  |  12,50  | sekitar 14 m |

Format teks Wyoming memberikan setiap tingkat tekanan, tinggi, suhu, titik embun dan kelembaban: cukup untuk N dan M. Wyoming tidak memiliki antarmuka resmi, skrip digunakan sebagai [RAOBget of NCAR](https://github.com/NCAR/RAOBget); Universitas DuPage menyimpan salinan dalam teks per stasiun ([misalnya, 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)).](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. Indeks dan metode yang diterbitkan

Hepburn dan F5LEN mulai dari bahan fisik yang sama tetapi tidak mempublikasikan algoritma lengkap; studi ilmiah dari 2008- 2026 melainkan menggambarkan metode langkah demi langkah, dan itu adalah dari sana bahwa itu layak menyalin resep.

| Metode | Memulai data | Apa yang diperhitungkan | Catatan yang berguna bagi kita |
| [Indeks Tropo Hepburn](https://www.dxinfocentre.com/propagation/hti.htm) | model cuaca mentah dari Layanan Meteorologi Kanada (MSC, seperti yang tertulis di bawah petanya; bukan GFS / NAM), peta yang dirancang dengan GRADS, algoritma proprietary ([etora](https://www.dxinfocentre.com/propagation/history.htm)) | 0-10 skala linier dari kelengkungan sinyal: 0 normal, 1-2 super- refraksi, 3-4 beberapa saluran, 5-6 difusi saluran; nilai negatif = sub-refraksi | skala yang akan diimplementasikan, sehingga indeks ketiga segera dibandingkan dengan dua pertama |
| [F5LEN, ramalan dunia](https://tropo.f5len.org/WW/info.php) | untuk Eropa model ARPEGE Météo- Perancis, 7.5 km, tingkat lebih antara 2 m dan 1500 m ([catatan oleh penulis](https://tropo.f5len.org/author/f5len/)) | Rata-rata indeks untuk tuan tanah, 40 langkah dari 3 jam, 4 perhitungan per hari, arsip 28 hari ([](https://tropo.f5len.org/?p=504)versi baru) | konfirmasi bahwa itu melayani model dengan banyak tingkat rendah |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf) | 60 ECMWF tingkat model, dari tanah ke 3000 m | minimal dN / dz per kolom: dilakukan di bawah September 157 N / km, super-refraksi di bawah mex78.7 N / km; basis dari lapisan trap | klimatologi: Padana Plain daerah Eropa dengan jaringan pipa lebih banyak sepanjang tahun; Mediterania di musim panas dekat 100%, di musim dingin di bawah 15%; Italia lebih dari 30% pada malam musim panas |
| [Indermuehle dan Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861) | GFS 0.25 °, 21 tingkat tekanan + bidang pada 2 m | M = N + 0.157 z; minimum dM / dz di bawah 3 km, basis dan atas saluran, kedalaman defisit M; di bawah 40 m dibuang | Diverifikasi selama 7 tahun di 25 saluran dari 88 MHz sampai 2.68 GHz; kapasitas yang baik sampai sekitar 3 hari di muka (AUC kira-kira 0,74), kemudian tetes; GFS lebih baik daripada ERA5 |
| [Hämäläinen et al., IMF 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf) | Harmonie- AROME, 750 m atau 2,5 km, 65 atau 90 tingkat | dM / dh < 0 pada profil model, perbandingan dengan 4 sampai 59 meter m | menghitung lebih vertikal daripada resolusi horisontal; tidak ada model melihat saluran di bawah 15 m; kelembaban mendominasi gradien suhu |

Layanan radio amatir ketiga sudah aktif: [TROPOCAST. EU](https://tropocast.eu/)dari Artur SQ3A, untuk 2 m dan 70 cm di Eropa. Dari halaman ramalan ([tropocaleu / prakiraan](https://tropocast.eu/forecast)):

- Data: DWRICON- EU (lulus 0.0625 °, sekitar 7 km), sumber baku; alternatif GDPS Kanada (0.15 °), kurang rinci.
- Metode: N dan M menurut ITU- R P.453 dari profil antara lapisan dekat tanah dan 850 hPa; pengakuan lapisan-lapisan, penyatuan saluran ke tanah dan tinggi dan ambang batas warna adalah kalibrasi yang tepat dari penulis, tidak diterbitkan.
- Produk: "complete" view 0- 3000 m, lebih terpisah tampilan "to the ground" dan "high" (for now only in the map to be incorporated); score 0- 100 in nine class (di bawah 38 background, 38-52 marginal, 53-58 lemah, 59-65, 6-65 baik, 73-80 sangat baik, tinggi, 89- 95, 96-100 kuat) dengan perkiraan, 72 jam yang sangat kuat; h, panjang, panjang, panjang, panjang, panjang, panjang, panjang, panjang, panjang, panjang, panjang, panjang, panjang, panjang, panjang, panjang, panjang, panjang, panjang.
- Eropa, Polandia, Inggris, Jerman, Perancis, Spanyol, Italia, Republik Ceko, Kroasia; antarmuka juga dalam bahasa Italia.
- Pakai ulang: peta yang dilengkapi dengan iframe tanpa kunci API; juga ada API terdokumentasi (skema OpenAPI dan JSON). Lisensi tidak diindikasikan: sebelum menggunakannya sebagai sumber ketiga di sebelah Hepburn dan F5LEN anda harus bertanya pada penulisnya.

Perbedaan dari proyek kami: TROPOCAST berhenti di 850 hPa (sekitar 1.5 km), sedangkan saluran tinggi tiba di 2000 m dan di luar (kasus LA0BY 22 / 11 / 2018), dan tidak memberikan indeks untuk band atau rute. Kelas 0-100 bisa dibandingkan dengan indeks kami 0-10 pada hari yang sama.

Struktur gambar TROPOCAST (dari link yang disediakan oleh penulis program): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, di mana n adalah langkah ramalan. Situs ini melarang penarikan otomatis ke robot, jadi saya tidak bisa membaca gambar; juga untuk ini, sebelum menampilkan peta ini dalam LIVETROVHF melayani izin penulis.

Catatan: [DJ5AR, kategori](https://dj5ar.de/?cat=174)(Magonza, 21 artikel 2017- 2023, 2 m-13 cm). Kasus ini dijelaskan: dilakukan beberapa minggu dari Irlandia (23 May-2 Juni 2023), FT8 ke Belarus (September 2020), Illy- Jerman melalui Alpen dan Adriatic (Agustus 2017). Kenali kasus saluran "terlalu tinggi" (terbalik menjadi 1000 m) yang tidak dapat Anda gunakan dari tanah: konfirmasi lain yang diperlukan untuk membandingkan share dari saluran dengan stasiun. Menggunakan radiophares sebagai ukuran aperture (hingga 1028 km) dan profil vertikal di Paris, Bordeaux dan DWD.

Kesimpulannya, resep yang umum: profil vertikal SymN dan M per tingkat pluglayer dengan dM / dh negatif Gibbase, atas, ketebalan dan gaya indeks lakban. Bagian "rahasia" dari Hepburn adalah kalibrasi empiris pada peristiwa nyata, yang kita dapat membuat ulang dengan titik.

## 6. Formula yang berguna untuk program ini

Selain N dan M (bagian Fisika dasar), ini digunakan untuk bergerak dari vertikal ke titik nomor dan kemudian ke jalur. Formula ITU telah direkonstruksi oleh PDF yang dibaca dengan buruk; pada 22 November telah ditinjau pada teks resmi ([ITUR P.52- 18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453- 14, P.526-16, P.372- 18) dan dikoreksi dalam kode: konstan dari troposcatter 190 (bukan 190,1), ditambah penyerapan gas dengan 3 g / 3 / 3; di bawah 5eq (5eq = 5) Rumore P.372- 18: meja 1 dan galaksi sesuai; Kurva kebisingan buatan adalah hingga 250 MHz dan galaksi sampai sekitar 100 MHz, selama program memperluas hukum yang sama.

1. Gradien antara dua tingkat dari model (h di km, M di unit M):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. Konduit strength and thickness: thickness d = top meconbase (for a ground drag, base = ground); strength as M deficiency between base and top.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3. Radius bumi ekuivalen faktor (P.452- 18, eq. 5 dan 6a), dengan NIN = rata-rata N drop di km pertama, positif, dalam N / km:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

Dengan NY = 40 Anda memiliki sekitar 4 / 3 k (atmosfer standar); dengan NIN yang mendekati 157 k cenderung tak terhingga, yaitu, radio bumi menjadi datar: itu adalah awal dari saluran.

4. Cakaran radio antena h meter tinggi, dengan radius yang setara dengan\ _ e dalam km:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5. Probabilitas saluran untuk lintang (P.452-18, eq. 2), persentase waktu; atriculity = lintang tengah rute, Alvar1 dan 1f 4 memperhitungkan seberapa jauh laut:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

Pada 41 ° N, semua oleh laut (Python 1 = 4 = 1), memberikan sekitar 11% dari waktu: adalah basis iklim, berguna sebagai nilai referensi dari indeks yang diharapkan berbeda.

6. Kehilangan troposcatter (P.452-18, eq. 45; f in MHz, d in km, vigangle of the path in mrad, N0 ground reaktivitas di pusat rute):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

Istilah Mediter015: LIVETROPOVHF sudah menggunakan G3YGF versi dari hilangnya penyebaran. N0

7. Kehilangan pada saluran (P.452- 18, eq. 46-51): kehilangan kopling ditambah pengangkatan untuk sudut semilau, yang tumbuh dengan akar kubik frekuensi (f in GHz, a\ _ e dalam km):

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. Dari Bean dan Dutton (1966) dan Kerr (1951)

Dua teks klasik memberikan tiga hal yang diperlukan segera untuk indeks: tingkat gradien telah bergaris pada tingkat yang sama tekanan GFS, apa yang layak dalam dB perubahan reaktivitas, dan apa situasi cuaca menciptakan atau menghancurkan saluran. * Radio Meteorologi *, NBS Monograph 92.

1. Klasifikasi profil untuk lapisan tekanan (Bean dan Dutton, tab. 6.5, p. 248; jourdN / dh dalam N / km). Dirancang untuk gelombang radio, itu berlaku seperti pada tingkat model:

| Jalan | Berbagi tentang | Sub- refraksi | Tidak dilaminasikan | Super-refraksi |
| 100- 850 hPa | 0- 1.46 km | TequdN / dh < 0 | dari 20 sampai 60 | lebih dari 100 |
| 850- 700 hPa | 1.463,01 km | TequdN / dh < 0 | dari 20 sampai 50 | lebih dari 80 |
| 700- 600 hPa | 3,01- 4.20 km | TequdN / dh < 0 | Dari 15 sampai 40 | lebih dari 70 |
| 600- 500 | 4,20-5.57 km | TequdN / dh < 0 | dari 10 sampai 30 | lebih dari 50 |

2. Berapa nilai dalam dB (Bean dan Dutton, cap. 6, pp. 23- 258), langkah-langkah dari 70 sampai 180 MHz pada rute di luar cakrawala:

- Lapisan apapun (di tanah atau di ketinggian) dibandingkan dengan profil "halus" meningkatkan sinyal 10- 25 dB ke semua persentase; keuntungan yang lebih besar datang dari lapisan miring tinggi, mungkin karena memfokuskan efek (kursus 200 km).
- Reaktivitas tanah Ns dan penurunan pada km pertama ada hubungannya dengan sinyal median bulanan dengan koefisien antara 0.4 dan 0,95, median 0,7 (20 rute dari 130 sampai 446 km, sekitar 100 MHz).
- Siklus tahunan: 0.18 dB per unit N; siklus sehari-hari dari 0.2 sampai 1,1 dB per unit N, maksimum pada rute 175-200 km dan musim dingin.
- Parameter senyawa dari Misme memperkirakan medan tersebut adalah 0.5 dB / (N / km) * (N / km) (gradien ekuivalen) ditambah sebuah istilah stabilitas termal; parameter turbulensi D (area antara profil sebenarnya dan satu adiabis, 0-3 km) hanya meningkatkan dibandingkan dengan WINN sederhana. Kesimpulan para penulis: Schun tetap parameter terbaik untuk kesederhanaan.

3. Referensi profil dan anomali (Bean and Dutton, # 3.8 dan 3.9). Suasana eksponensial dari referensi CRPL memberikan profil "normal" dimulai dari Ns; unit A mengukur berapa banyak profil nyata berbeda di setiap saham. Ini adalah cara alami untuk mengubah profil GFS menjadi anomali untuk diletakkan di peta:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

Dengan Ns = 313 Anda memiliki NURN tentang Kore42 N / km, itu adalah radius yang sama 4 / 3. Kriteria saluran 157 N / km juga diperoleh di sini oleh hukum Snell untuk radius horizontal (eq. 4.23), hampir independen dari tinggi antena.

4. Dimana dan ketika saluran lahir (Bean dan Dutton # 4.4; Kerr cap. 3):

| Proses | Situasi | Efek |
| Peringatan | udara panas dan kering dari tanah mengalir di laut dingin (khas lengan lengan musim panas setelah hari cuaca baik); terutama menghitung defisit kelembaban | saluran intens di laut |
| Nocturnal irradiation | langit tenang, angin lemah; awan rendah atau banyak kelembaban mencegahnya | dibawa ke tanah yang lahir saat matahari terbenam, tumbuh di malam hari dan menghilang cepat setelah matahari terbit, terutama di dalam |
| Subsidence | udara yang turun dari antisiklon, panas dan kering, lebih segar dan lembab udara; sering banjir di pegunungan dan sektor timur selatan antisiklon | lapisan super- refractive atau saluran tinggi; hampir selalu gradien lebih tinggi dari standar secara terbalik |
| Laut dan daratan | pantai, sirkulasi beberapa ratus meter | jalur pipa lokal sepanjang pantai |
| Terhadap saluran | pencampuran: tanah dipanaskan oleh matahari antara pertengahan pagi dan sore, angin ke tanah selama beberapa m / s pada medan kasar; kabut malam | kemungkinan saluran hampir tidak ada |

5. Dua bahan baru untuk dicoba. Gerakan vertikal: arus naik di zona konvergensi menghapus dan menghapus lapisan-lapisan, subsidence memperkuat sinyal (Moler dan Holden: rata-rata sekitar 2,5 dB lebih banyak daripada dengan udara menanjak, di laut di band L dan X). Buka Meteo telah menyediakan kecepatan vertikal pada tingkat tekanan. Dan tren tekanan: pada kursus 140 km di 186 MHz sinyal turun ketika tekanan turun ke pemancar dan ke pusat jalan dan kembali tinggi dan tanpa evanescence ketika naik, hanya dengan 2 (Bean dan Dutton, fig. 6.9, p. 241).

6. Dari Kerr. Sebagai tambahan rumus panjang gelombang maksimum (bagian fisika dasar), Kerr memberikan rumus perkiraan untuk M dari suhu satu-satunya, uap dan ketinggian (13.9, dengan 79 dan 4800 konstanta tua kemudian digantikan oleh 77,6 dan 4810) dan mengamati bahwa untuk membalikkan potongan angin dapat menghasilkan gelombang yang berayun di permukaan saluran, mungkin menyebabkan dalam melarikan diri (Kamis 3.26). Sumber Italia dilaporkan oleh Bean dan Dutton untuk mencari: L. Bonavoglia (Desember 1958), "Korelasi antara fenomena meteorologi dan penyebaran"...

## 8. Dari presentasi radio amatir dan laporan teknis (seri kedua)

Tujuh dokumen yang disediakan oleh penulis program. Tiga melayani langsung ke indeks: W3SZ untuk troposcatter "lantai", LA0BY untuk kasus-kasus bahwa Hepburn tidak diprediksi, Hepburn (terjemahan LU7MC) untuk klasifikasi cuaca dari saluran.

1. Troposcatter: sinyal yang selalu ada. R. Rehr W3SZ, "Pemintalan Troposcatter (Troposcatter) Propation untuk VHF, UHF, Microwave dan Frekuensi", Konferensi Tikus 2014; J. R. Duffey KK6MC, "Mode Propagasi Troposcatter dan Cara Memungkinkan Terselubung pada VHF / UHF / UHF Bands," CSVHF 2017.

- Berguna dari sekitar 100 sampai 700 km, dari 144 MHz sampai 10 GHz; kerugian tumbuh dari 30 log f dengan frekuensi.
- Di atas semua itu, ia menghitung sudut awal menuju cakrawala: setiap biaya tambahan sekitar 9- 12 dB ( M1CNK, Collins method).
- Pada musim panas rata-rata sinyal adalah pada sekitar 10 dB lebih tinggi daripada di musim dingin; di tengah sore sekitar 5 dB lebih rendah daripada di pagi hari atau di malam hari. KK6MC menunjukkan total variabilitas 13- 15 dB dengan waktu, musim dan waktu.
- Metode Yeh, termudah untuk program (f dalam MHz, d dalam mil, topangle penyebaran dalam derajat, rata-rata geometris dari bukaan dua bundles):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Perbandingan Larsen (1968) pada 15 koneksi nyata: rata-rata kesalahan persegi 5,1 dB untuk Collins, 7,4 untuk Yeh, 8.1 untuk NBS dan CCIR, 16,8 untuk Rider. Semua metode meningkatkan jika Ns terus stabil (Yeh jatuh ke 5.4 dB): untuk penyebaran pemecahan jumlah hari sedikit, sehingga indeks tropo harus melihat lapisan, tidak hanya Ns. Tabel 15 koneksi, dengan pengukuran kerugian (termasuk Feldberg-Monte Baldo, 322 km di 957 MHz, 231 dB; Cyprus- Israel, 319 km di 77.5 MHz, 163,3 dB), adalah bangku tes yang baik untuk perhitungan kita.
- KK6MC memberikan kerugian bahwa stasiun khas berhasil melebihi 2 m dan jarak yang berhubungan, dari 184 dB (FT-817 dan 3 elemen di SSB, sekitar 130 km) ke 215 dB (long bungs di SSB, sekitar 430 km) dan 236 dB (kW dan yagi digabungkan, sekitar 640 km). Ini adalah cara untuk menerjemahkan indeks dB ke mil diterima.
- Kode siap: W3SZ Program Yeh dan Collins (Notehawk.com / w3sz / YehCollins.zip) dan kalkulator Bob Atkins KA1GT (bobatkins.com / radio / troposcatter.html).

Sumber lain pada scatter: I. Roberts ZS6BTE, "Tropospheric Scatter Propagration", * Radio ZS *, Desember 1984, dengan catatan 2013 ([qs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). Terutama Geometri yang berguna: angle dari scatter ds0 = 1x2, with Sym0 = d / R, h1 = (h1 meash\ _ ostacolo) / d1 + d1 / 2 R dan yang sama untuk stasiun lain, R = 8446 km (4 / 3 dari radius Bumi). Ini adalah bagian yang digunakan untuk menghitung sudut dari profil tanah di "lantai troposcatter". Data lain: masing-masing sudut dalam biaya sekitar 10 dB; melewati dari Ns 280 sampai 300 mengurangi kehilangan sekitar 4 dB; volume penyebaran adalah antara 300 dan 2000 m untuk 150 km, antara 600 dan 3000 m untuk 300 km; Waktu dalam volume tersebar menimbulkan sinyal sekitar 20 dB. contoh numerik dari artikel tidak kembali dengan rumus tertentu (istilah ruang bebas di 900 MHz dan 872 km sekitar 150, tidak boleh dicentang mereka. W3SZ

2. Jenis saluran (W. R. Hepburn, "Tropospheric Propagration Modes", dxinfocentre.com / propagasi / tr-modes.htm, terjemahan Spanyol LU7MC). Lengkap meja Bean dan Dutton dengan kasus-kasus berguna di Italia:

| Tipe | Kapan | Catatan untuk indeks |
| Irradiation | tenang dan damai malam di bumi | Ikuti tanah, menghilang tak lama setelah fajar |
| Tekanan tinggi (subsidence) | antisiklon | dapat bertahan sepanjang hari; di malam hari saluran dari iradiasi bawah dapat memblokir sinyal jauh, sehingga siang hari bisa lebih baik |
| Depan | di depan hangat depan, di belakang depan dingin, utara dari depan stasioner | pendek; cuaca buruk mengganggunya |
| Turun gunung | Föhn, bora: udara turun, pemanasan dan pengeringan | untuk mencoba bora pada Adriatic dan föhn selatan Alpen |
| Peringatan | panas dan kering udara atas tanah dingin dan basah, misalnya setelah hujan | dibawa ke tanah |
| Valley | panas udara atas udara dingin berhenti di lembah | dapat tinggal di hari |
| Marino | panas dan kering udara di air dingin, tiba di pantai dengan angin | puncak di sore hari; musim semi dan awal musim panas terbaik |

Saluran penguapan di laut (0-30 m) hanya berfungsi sekitar 3 GHz dan tidak memasuki daerah pedalaman: selama 144 dan 432 MHz Anda dapat mengabaikannya. Gunung-gunung tinggi adalah penghalang, gurun terlalu kering.

3. Kasus Hepburn belum memperkirakan (S. Heck LA0BY, "Propadasi Tropo Ekstrim pada 144 MHz dan atas," Nordic VUSHF 2019, dari JO59IX sampai 500 m ketinggian):

- 22 November 2018: 70 cm sampai UA6 (sekitar 2600 km) selama 6-8 jam. Hepburn hanya menunjukkan Negara Baltik dan Polandia. Saluran ini terlihat pada peta suhu pada 900 dan 1500 m dan berakhir tepat di atas 2000 m; itu lebih tinggi di sisi Norwegia, dan hanya LA0BY di bukit yang masuk. Di tanah waktu tidak tampak seperti trope: dingin dan kabut di bagian atas.
- 11 Februari 2015: 2 m dan 70 cm menuju 9A dan S5 (sekitar 1500 km) melalui Alpen, juga ini tidak diramalkan oleh Hepburn.
- 15 Oktober 2011: 2 m dengan EA1 (2135 km) dalam saluran tinggi, pada tiga hamparan laut dan tiga tanah.
- Peristiwa lain: 6-11 November 2003 ke timur (2050 km), 10 Desember 2004 ke Perancis.
- Alamat lapangan: kabut dalam polos, puncak bebas, angin kecil, tanah basah. Gradien suhu normal 6,5 ° C / km; dalam kasus nyata ( VK3KAQ) + 7 ° C dalam 20 m.

Kesimpulan penulis: Hepburn dan F5LEN tidak cukup, mereka melayani profil di ketinggian sepanjang rute. Bagi kami: tanggal ini adalah kasus verifikasi untuk dihitung ulang dengan data sejarah model, dan indeks pada jalur harus membandingkan kuota dengan kuota stasiun.

Pengalaman Italia (G. IV3GBO, "Tipes of VHF menyebarkan SSB / CW", 2017): dilakukan antara 450 dan 3000 km, dengan lapisan antara 450 dan 3000 m; kabut diperluas sebagai petunjuk; pembukaan oleh laut di Adriatic menuju Puglia, Albania dan Yunani. Menyebar dari 100 sampai 500 km.

5. Perkenalan (P. Wilton M1CNK, "An Introduction to VHF / UHF Propadation", 2013): 70 cm lebih baik dari 2 m; dengan 10 m tiang di 144 MHz sudut awal adalah sekitar 3 °, dengan 20 m sekitar 1.5 °.

Laporan NASA (E. Hirschmann, "Tropospheric dan Ionospheric Efek terhadap Frekuensi Radio (VHF-SHF) Komunikasi," Goddard X-71- 67-89, 1967): ini menyangkut kesalahan jarak pada satelit, bukan DX. Ini membawa kembali profil N dan eksponensial yang sama (contoh di Florida: Ns 370, Monograph 4 (1959), konstan 0.161 / km) dan mengirimkan dua sumber NBS untuk mencari: Bean dan Thayer, "CRPL Exponensial Reference Atmosfer", Monograf 4 (1959), dan Bean, Horn dan Ozanich, "Climatic Charts dan Data Radio Refractive Index untuk Amerika dan Dunia",

## 9. Dari VHF / UHF DX Book, Bab 2

I. White G3SEK (diedit), * The VHF / UHF DX Book *, DIR Publishing 1995 (ISBN 0 9520468 0 6; replika digital 2017), bab 2 "VHF / UHF / UHF Propation", pp.2-13, 222- 29 dan 4-11. Ini adalah teks terdekat untuk radio pengalaman amatir di antara mereka yang dikumpulkan; menegaskan ambang batas sudah digunakan dan menambahkan aturan sinoptik baru.

Nomor

- Rumus yang sama dari N (77.6 P / T + 3.73); contoh: 1000 hPa, 7 ° C, dan kerusakan N = 324. Dalam suasana baik berbentuk, misalnya setelah badai, N hampir linear sekitar 39 unit / km di 2- 3 km pertama.
- Gradiasi antara 0 dan Kore79 N / km adalah normal, di bawah Kore79 ada super- refraksi, lebih dari 0 sub- refraksi. Buku ini menegaskan bahwa DX panjang tidak datang dari refraksi super- sederhana, yang harus tetap tepat di seluruh jalan, tetapi dari saluran.
- Tebal minimum, sedikit lebih bijaksana dari Kerr dan Hepburn:

| Band | Ketebalan minimum |
| 50 MHz |  400 m  |
| 70 MHz |  300 m  |
| 144 MHz |  200 m  |
| 432 MHz |  100 m  |
| 1,3 GHz |  50 m  |

- Rasio ini tidak proporsional dengan panjang gelombang (sekitar 65 gr pada 50 MHz, 220 ghoat 1.3 GHz) karena tepi saluran adalah "displaced". Di Inggris jaringan pipa sering pada 144 MHz tapi hampir tidak pernah pada 70 MHz atau di bawah, dan sering 432 MHz pergi kuat ketika 144 lemah.
- Dalam baris pipa sinyal tetes sebagai 1 / d bukan 1 / d2: sampai dengan 1000 km peserta dapat mirip dengan bahwa ruang bebas.
- Kapasitas stasiun minimum pada 2 m (100 W, 15 dBi yagi, 2 dB noise): 217 dB, sekitar 400-500 km di SSB dalam kondisi rata-rata; di CW sekitar 20 dB di lebih (p. 4-7). Setuju dengan meja KK6MC.
- Menyebar: kehilangan naik oleh sekitar 15 dB dengan tiga frekuensi dan 10 dB untuk setiap derajat sudut penyebaran (Yeh formula; dalam teks diekstrak konstan tidak dapat dibaca, sehingga harus diambil dari versi W3SZ). The "up" menyebar dari sel-sel konvektif mencakup rutin hingga sekitar 500 km, 1000 km dengan stasiun yang baik. Setelah hujan deras dan kondisi diperpanjang langka pada semua band.

Jenis saluran

- Di tanah: di bumi mereka menyebarkan buruk karena menyerap tanah dan menyebar; di laut yang tenang tiba jauh (Scilly-Canarie, 2500 km, sekitar setahun sekali pada 2 m). Saluran penguapan sering hanya sekitar 15 m dan melayani gelombang mikro.
- Tinggi: antara 450 dan 2000 m, karena DX lebih panjang di tanah; sinyal "tinggi" di atas stasiun di bawah. "kehilangan" saluran memberikan sinyal kurang kuat tetapi juga memungkinkan antar stasiun jarak.
- Saluran memiliki arah yang tepat dan dapat terakhir jam atau hari, bertentangan dengan sporadis E.

Aturan Sinoptik Baru untuk Indeks

- Usia antisiklon: air turun ke pusat dari beberapa meter per jam dan sebaliknya diturunkan sebagai peningkatan tekanan tinggi; pembukaan yang lebih besar tiba setelah beberapa hari penyebaran sudah baik. Sebuah antisiklon jangka pendek tidak cukup.
- Tepi tekanan tinggi: lapisan adalah kubah dan di tepi lebih rendah, sehingga stasiun menjadi lebih baik.
- Arah: Telah diamati bahwa rute yang lebih panjang berjalan sepanjang isobares atau melalui asrama (Flavell G3LTP, 1984), tetapi ada koneksi yang sangat baik juga dalam arah radial dari pusat. Contoh dari 29- 30 Agustus 1987: dari Inggris pembukaan diputar 180 ° dalam dua hari (Spanyol, Perancis, Austria, ke Norwegia) mengikuti pergeseran anti- topan.
- Terhadap: tebal isobar (angin kuat), stratocumuliform awan di tepi lapisan (membatasi frekuensi tertinggi).
- Front: front dingin mendorong udara panas dan menciptakan terbalik di sepanjang depan; arah terbaik adalah sepanjang depan sementara lewat, tetapi berlangsung sedikit.
- Musim: di Inggris antisiklon lebih sering pada akhir musim panas dan awal musim gugur.
- Petunjuk visual: asap terjebak dalam garis gelap di atas cakrawala; dengan tekanan tinggi, layerculagal dengan basis di bawah 1000 m menunjukkan saluran tanah, basis di atas 1000 m saluran tinggi.

Reference dikutip untuk pencarian: R. G. Flavell G3LTP, "Studies of an Extensive Anticyclonic Propagration Event", * Radio Communication *, Februari 1984, p. 128; J. Gannaway G3YGF, "Tropostical Scplaters Propagration", * Radio Communication *, September 1981, p. 710; P. G3BGL W6ZGN

## 10. Model radio amatir yang telah diverifikasi: Bizarcoano dan Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, "Model Tropospheric Praktis untuk Prediksi Propagasi VHF / UHF", * Cuaca Nasional Digest * 22: 2, Juni 1998, pp. 15-24, bebas dari Asosiasi Cuaca Nasional](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf).

- Data: radiosundasi dari 00 dan 12 UTC dari 47 stasiun di timur Amerika Serikat, 1995-1997; analisis tanah untuk 700 hPa (di awal 400), pada profil N, tanpa M. Excluding coasts, pulau dan pegunungan.
- Untuk setiap survey: kehadiran dan quota dari saluran, panjang gelombang maksimum yang dapat memimpin (sering sekitar 100 kali panjang gelombang), maksimum gradien dari suhu potensial antara tingkat tetangga dan antara satu tingkat dan kedua di atas (untuk lapisan tebal). Saluran tanah tidak digunakan.
- Aturan pertama, kegigihan: indeks stabilitas (indeks diangkat, K, totals-total- total, SWEAT) tetap dalam 3 unit dari survei 12 jam sebelumnya, dan tingkat tekanan di mana suhu potensial tumbuh lebih tetap sekitar sama. Jika demikian: perkiraan perbaikan dalam 12 jam ke depan.
- Aturan kedua, kontrol dinamis: lapisan batas yang tidak naik, CAPE, variasi ketebalan sampai 500 hPa dalam tiga survei, variasi kemiringan dalam batas.
- Ambang Band: saluran yang mampu membawa 100 cm atau lebih menunjukkan pembukaan pada 432 MHz, hampir pasti pada 222 dan sering bahkan pada 144.
- Penyaring ruang angkasa: stasiun pada kisi heksagonal sekitar 400-500 km; perkiraan hanya bernilai jika setidaknya 5 stasiun kontigue positif (jika Anda dapat melewatkan satu). Dengan 5 stasiun kemungkinan acak palsu positif menjadi diabaikan.
- Verifikasi terhadap peristiwa kolom "Dunia di atas 50 MHz" dari QST: pada tahun 1996, 48 peristiwa yang direncanakan pada 67 (71%); 12 dari 15 peristiwa utama. Akurasi hingga 70% pada 432 MHz dan sekitar 60% pada 144 MHz pada 12 jam; menyediakan hampir dua kali kejadian yang sebenarnya; 9 kesalahan dari 19 adalah prediksi tiba terlambat, karena banyak bentuk perbaikan dalam waktu kurang dari 12 jam.
- Ditambahkan ke stasiun di utara: angin berputar searah jarum jam (hot start) sebagai kondisi ekstra.

Dari PDF lengkap (disediakan oleh penulis program) tiga rincian lebih. Aturan mereka "ketebalan = 100 kali panjang gelombang" memberikan 600 m untuk 6 m, 200 m untuk 2 m, 125 m untuk 1.25 m dan 70 m untuk 70 cm (fig. 3): karena 70 cm kurang parah dari Kerr dan Hepburn (sekitar 89 m), untuk 2 m itu bertepatan dengan VHF / UHF DX Book. Angin yang berputar searah jarum jam naik, menurut penulis, terutama karena membawa udara lebih hangat di ketinggian, yaitu apa yang membuat jatuh N tumbuh dengan tekanan, kelembaban yang membawa dengan itu tampaknya tidak mendestabilisasi. Usus usus buntu ini memperoleh dN / dh dalam bentuk tertutup untuk suasana bergradien termal konstan dengan lompatan tajam gradien panas dan kelembaban pada kuota tertentu: berguna sebagai kasus tes untuk kode kita. Jejak mereka kemiringan dari suhu potensial berasal dari K H. Jehn, "Penggunaan indeks bias potensial dalam sinoptik skala meteorologi radio", * J. Meteorologi * 17, 1960, p. 264.

Apa yang harus diambil untuk indeks kami: (1) sebuah istilah dari ketekunan: bandingkan profil dengan 12 jam sebelumnya, bahwa dalam perkiraan per jam kita telah bebas; (2) filter ruang angkasa: sebuah poin hanya jika peningkatan mencakup area dari beberapa ratus km, yang mengurangi alarm palsu; (3) stabilitas indeks, dihitung dari suhu dan embun sampai 850, dan 500 hPa (Open- Meteo) telah memberikan indeks dan CAPE); (4) angin yang berputar antara suhu dan suhu bumi dengan 850 derajat.

Sumber lain dalam paket yang sama, sedikit bermanfaat bagi kita: P. K. Rastogi, "Stratosfer dan Troposfer (S-T) Studies di Millstone Hill", NASA 1984 (dokumen 19840019069): 440 MHz radar yang mengukur turbulensi udara bersih antara 4 dan 25 km, dengan lapisan tipis terkait dengan gelombang gravitasi dan pemotongan angin. Ini menegaskan bahwa turbulensi yang menghasilkan penyebaran yang berlapis, tetapi keprihatinan terlalu tinggi peluang untuk saluran.

## 11. Ronde ketiga dari sumber (20 November)

Kasus sejarah yang tinggi. J. Maboun OK1VR, 2 m dari puncak Snnouthika (1602 m), musim gugur 1958, artikel tahun 1959 diterjemahkan pada tahun 2006 ([ok2kkw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)). Pada 27- 28 Oktober 1958 pertama Cekoslowakai - Inggris koneksi pada 145 MHz ( G5YV, 1220 km) dan banyak lagi antara 1100 dan 1250 km, dengan sinyal stabil setelah 23. Penulis atribut itu untuk dua inversi superstressed, atas sekitar 2200 m, dengan sinyal kembali ke tanah hanya di mana lapisan bawah dilarutkan; kondisi disukai oleh antisiklon lambat dari Atlantik ke benua. Dari rumah (Praha) maksimum adalah 530 km: hanya stasiun di ketinggian memasuki saluran. Ini adalah kasus lain verifikasi (seperti LA0BY 2018) dan menegaskan bahwa indeks harus mencari dilakukan lebih dari 2000 m dan membandingkannya dengan share stasiun.

Pustaka sejarah NBS (berguna untuk mencari karya-karya klasik, bukan untuk rumus):

- W Nupen, * Bibliografi pada Propalasi Troposfer Radio Waves *, NBS Technical Note 304, 1965, 356 halaman, dengan indeks untuk topik (juga "ramalan kondisi") dan menurut negara. Masukan Italia melaporkan: L. Sacco, "Koneksi radio di atmosfer sangat refraksi rezim," * Frekuensi Tinggi * 24 (6), Desember 1955 (Penelitian Italia: refraksi di bawah 500 m sering siang dan malam); S. Silleni,
- A. L. Abbott, * Bibliografi dari Tropospheric Radio Wave Scattering *, NBS, material sampai Januari 1960, sekitar 670 item memerintahkan per tahun: berguna untuk bagian troposcatter.

Disclosure dan pengamatan: F. W. Odenthal DK5EQ, "Ausbreitung 2m - Eine Einfhrung" (31 / 05 / 2024): hampir optik sampai 200 km, tropo atau refraksi di luar, dengan tekanan tinggi dan pada musim semi dan musim gugur; cite halaman VHF DM2HB dan catatan DK5YA pada tropo dan Es. Johnson VK3DXE pada blog EI7GL (20 kali dari WtroF) telah ditemukan pada 20 kali dalam 20 kali ini dari Wtrop dalam 20 bulan panas telah ditemukan dalam 20 kali. Diskusi di VHF marina dan AIS ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)): scope 1.23 (ft) nautical mil per antena, tersebar sampai sekitar 50 nm, khas trope perbaikan 100- 400 nm; saluran dikenali karena stasiun jauh tiba sekuat tetangga. Chelmsford ARS News, Desember 1983: dalam kontes dalam 2 m stasiun pantai timur Inggris disukai oleh rute laut ke benua.

ARRL, * The Radio Amateur 's VHF Manual *, 1972, cap. 2 "Propadasi Beyond Horizon" (pp. 17- 19). Tips operasi sudah dikenal (ikuti daerah tekanan tinggi yang bergerak perlahan-lahan dan di atas semua tepi punggung mereka; awal musim gugur periode terbaik; daerah pesisir, danau besar dan lembah-lembah sungai yang disukai oleh stratifikasi). Dua data baru: (1) ara. 2-4 dibandingkan dengan atmosfer standar (relatif kelembaban 70% hingga 12000 kaki, refraksi minimum) dengan radius khas refraksi kuat, dengan rasio suhu yang jelas dan campuran sekitar 4000 kaki (sekitar 1200 meter): itu adalah tanda tangan bahwa perhitungan kita harus mengenali pada tingkat GFS; (2) saluran sebenarnya langka di bawah 144 MHz dan paling sering di UHF, menurut ketebalan minimum. Bibliografi QST yang berguna untuk dicari: Hull, "Kondisi Air- Mass dan Bending of UHF Waves" (Juni 1935, Mei 1937); Hoisington, "Prediksi Painess of Two-Meter Openings" (Oktober 1949); Collier, "Upper- Kondisi Udara untuk 2- Meter DX" (September 1955); Botts, "A Night to Remember" (Januari 1970, dilakukan di darat).

PA0EHG, halaman "Tropo" ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): ringkasan dari troposcatter dari artikel G3YGF, sudah dalam manual. Gunakan dua aturan praktis untuk mengendalikan perhitungan penyebaran kita: sekitar 10 dB kelebihan kerugian untuk setiap sudut di cakrawala setiap stasiun, dan lebih dari 100 km sekitar 9 dB ditambah setiap 100 km rute (60 mil); volume umum dimulai dari sekitar 600 m pada 100 km dan dari sekitar 9 km pada 480 km. Ini tidak memperlakukan saluran, jajak pendapat atau ambang batas refraktif.

PA0EHG, kalkulator PROPBER ([halaman](https://pa0ehg.com/propagat1.htm), program propber.exe disediakan oleh penulis program). Ini adalah program 36 kB DOS yang ditulis dalam BASIC dan dikompilasi dengan Kompailer Microsoft BASIC 5.60 (1985): ini hanya berjalan di bawah DOS atau DOSBox. Dari teks internal: meminta frekuensi, jarak (atau dua locators, juga dalam format lama QRA "E-locator"), untuk setiap antena tinggi stasiun, angka kebisingan, FI band, kekuatan, hambatan (tinggi dan jarak) dan antena gain (0 = perhitungan diameter perumpamaan, efisiensi 50%). Kembali jarak maksimum dalam optik, kerugian optik yang benar untuk penyerapan uap (0.1 dB / km dari 24 GHz), hilangnya troposcatter "benar untuk situs" (yaitu sudut cakrawala yang diberikan oleh hambatan) dan S / N rasio dalam dua indra, juga "dengan penyebaran normal maksimum". Konstanta numerik tidak dapat dibaca dalam executable yang dikompilasi. Metode yang disarankan untuk band-band tinggi juga berguna bagi kita: mengkalibrasi perhitungan pada link yang diukur (perbedaan antara S / N yang diukur dan dihitung) dan menerapkan perbedaan yang sama ke band lain. Ini adalah pola yang sama dari perhitungan kita, tetapi menggunakan profil S RTM1 bukan hanya satu kendala dan perkiraan GFS bukan margin tetap.

PROPBER, konstanta dibaca dalam executable (angka dalam format binary Microsoft). Di meja data sebelah teks: 8.68589 (= 20 / 10, yaitu 20 log dihitung dengan logaritma alami), 32,5 (ruang bebas: 32,5 + 20 log f\ [MHz\] + 20 log d\ [km\], 24000 dan 0.1 ditambah 4000 dan 0.3 (uap: 0.1 dB / km dari 24 GHz, 0.3 dB / km dari 40 / km) Mungkin, karena Anda ulangi tapi di dalam kode: 69.5 (persamaan yang sama dengan formula penyebaran Yeh di VHF / UHF DX Book), 332 (refractive tanah), 8733 km (radius yang sama, k 1,37). Formula penyebaran yang tepat tidak dapat direkonstruksi tanpa membongkar program. T0

PROPBER, formula direkonstruksi oleh kode (disasembly dari bagian yang dikompilasi; operasi dipanggil ke perpustakaan BASIC dengan operator dalam memori, yang nilainya dalam presisi ganda telah diterjemahkan). Konstanta 69.5, 332 dan 8733 sebelumnya ditemukan positif palsu dalam perpustakaan.

- Jarak Optik: d = 5 Hal ini lebih murah hati daripada klasik 4.12 (k = 4 / 3): sama dengan k Gib2.
- Kehilangan optik: L = 32,5 + 20 log f\ [MHz\] + 20 log d\ [km\], plus 0.1 dB / km diatas 24 GHz dan 0.3 dB / km diatas 40 GHz.
- Troposcatter: Lts = L + 23 + 10 log f + 0.0674, yaitu 55.5 + 30 log f + 20 log d + 0.0674 (formula G3YGF dengan konstanta 2 dB lebih tinggi). Tidak ada istilah sudut atau reaktivitas.
- Koreksi "untuk situs": program meminta hambatan dan jarak tapi blok yang harus menggunakan mereka kosong; variabel koreksi tetap 0, sehingga "benar" kerugian sama dengan yang tersebar.
- Suara bising: T = 290
- S / N = 10 log P + G\ _ tx enjuL + G\ _ rx n. "Dengan penyebaran abnormal maksimum" menggunakan kehilangan optik bukan scale, yaitu kasus konsit ideal tanpa rugi di luar ruang bebas.

Perbandingan: 300 km dan 144 MHz PROPBER memberikan sekitar 189,9 dB, G3YGF 187,9, P.452 187,8 (dengan sudut cakrawala nol): penyebaran terjadi dalam 2 dB. Perhitungan kami menambahkan sudut cakrawala dari profil, suhu antena, dan bukannya "maksimum" tetap lapisan yang diharapkan dengan sisa kawin dan pengangkatan.

Rohde & Schwarz, P. Denisowski, "Memahami Propagasi VHF", Catatan Pendidikan, April 2023 (24 halaman, penyebaran). Konfirmasi poin sudah dikumpulkan (dilakukan sebagai panduan gelombang dua dimensi, sinyal yang turun secara linier dengan jarak, hingga 1.500 km dan seterusnya, durasi dari jam ke hari; tanah yang baik di laut dan langka di darat; diangkat dari beberapa ratus meter ke beberapa km). Dua pengamatan berguna untuk indeks: (1) evolusi dalam waktu: saluran baru dibentuk sering membawa hanya frekuensi tertinggi dan, ketebalan, mulai membawa bahkan terendah; ramalan per jam karena itu harus menunjukkan 432 MHz "di muka pada 144 MHz ketika lapisan tumbuh; (2) stasiun lokasi: kondisi terbaik adalah di antara stasiun dekat dengan saluran, sementara mereka di sepanjang jalur dari saluran tinggi sering gagal untuk masuk, kecuali yang kehilangan". Saluran bergerak dengan cuaca atmosfer.

Situs PA0EHG, halaman lain. Untuk tropo, PathProfile dari M. Willis G0MJW ([scheda](https://pa0ehg.com/srtmp.htm), [download](http://www.mike-willis.com/Software/ASTERPathprofile.zip)): profil dari rute dari kerugian total yang diharapkan dan quota berkas, dinilai dapat diandalkan untuk troposcatter dalam microwave; tidak memperlakukan penyebaran dari pesawat atau hujan. Berguna sebagai referensi kedua untuk membandingkan profil S RTM1 dan kerugian. Sisa situs ini mencakup EME, microwave, bandara (AirScout, SM7LCB map) dan langkah-langkah dalam 80 m.

PathProfile oleh G0MJW (Mike Willis), ASTER dan paket SRTM dan SiteData (disediakan oleh penulis program; [](https://www.mike-willis.com/software.html)software page). Program Windows 2018: profil dari Aster atau SRTM sampai 1 "atau 3", kerugian pada optik, diffraction, troposcatch (valid hingga 1000 km), saluran dan gas, dengan persentase waktu, k median dari rute, pecahan jalur di laut dan jarak dari pantai: struktur adalah P.452 / P.1812, tetapi bantuan (terkompresi.m file) tidak dapat dibaca di sini. SeneData mengubah lokasi dan memberikan saham dari GT OPO30.

Bagian yang paling berguna adalah berkas data termasuk: ITU- R P.453 peta statistik di grid global sebesar 1,5 ° (121 baris dari 90 ° N sampai 90 ° S, 241 kolom dari 0 ° sampai timur): gradien dN / dz pada 65 pertama kali lebih lama untuk 1, 10, 50, 90, dan 99% dari waktu; untuk persentase keberadaan, ketebalan dan intensitas; untuk kehadiran yang tinggi, berbagi basis, ketebalan, dan batas maksimum; dan batas maksimum; Nilai tahunan yang interpolasi:

| Lokasi | Pipa darat:% / ketebalan / intensitas | Pipa tinggi:% / base / thickness / intensitas | dN / dz melebihi 10% dari waktu |
| Naples |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | Kore380 N / km |
| Tyrrhenian (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | Kore674 N / km |
| Trapani |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | Kore520 N / km |
| Brindisi |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | Sex405 N / km |
| Adriatic (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | $399 N / km |
| Milan |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | 172 N / km |
| Udine |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | 176 N / km |

Untuk indeks kami adalah referensi iklim siap: ketebalan dan intensitas tipikal saluran bukannya 50 meter tetap dari saluran potensial, dan anomali "hari ini dibandingkan dengan normal" per zona. Data ITU: bebas untuk penggunaan non-komersial, untuk memeriksa sebelumnya termasuk mereka dalam versi publik. Berkas ZO NES01.DEM (58 MB, 2 'grid) berisi zona iklim di pedalaman / pantai / laut; format tidak didokumentasikan dan saya tidak memecahkan kode itu. Halaman persamaan Willis hanya berisi konstanta fisik, ruang kosong dan keuntungan antena.

Keempat putaran sumber (20 November).

Indermuehle dan Suzuki 2026, memprediksi jaringan pipa GFS untuk observatorium CSIRO Murchison (arXiv 2608.12861): ini adalah sumber terdekat dari LIVETROVHF. Profiles N dari GFS sampai 0.25 ° (permukaan sampai 2 m, kemudian tingkat isbolaris, mereka di bawah tanah yang dibuang); untuk setiap kolom minimal dM / dz di bawah 3 km, basis dan atas konduksi, terintegrasi M defisit, kemudian P.4522- 16 dan P.1812-6 dengan GLO-90 25 rute dari 98 sampai 1005 km, dari 88 MHz sampai 2.68 GHz, setiap jam. Hasil: Median AUC 0,72 pada 25 saluran; persentase waktu P.452 p harus digunakan sebagai konstan kalibrasi (pilihan P = 20%), karena model ITU dengan instan SIN tidak didefinisikan dalam saluran; keterampilan tetap hampir datar sampai 3 hari di muka. Pada 162 MHz (AIS) rute yang diterima memiliki saluran tertinggi dari 67% (0.26 melawan 0.16) dan kedalaman tertinggi 49% (28 vs 19 M) untuk mengontrol: di laut saluran hampir selalu, memutuskan peregangan tanah. GFS melihat setengah waktu di lapangan dibandingkan dengan ERA5 (4.5% melawan 10,4% dari sel-jam), sementara di laut melihat lebih banyak (73% versus 56%). Indeks "koridor" (garis pipa terus menerus sepanjang rute, lapisan dengan ketebalan minimum 40 m) meningkatkan hasil: menegaskan bahwa kelanjutan sepanjang rute adalah kriteria yang tepat.

Longley dan Rice, ESSA ERL 79-ITS 67 (1968): metode untuk medan yang tidak teratur dari 20 MHz sampai 20 GHz (setelah ITM), dengan ketidakaturan tanah Ns, dari 250 sampai 400, radius yang sama 8497 km (4 / 3), cakrawala, difraksi, tersebar maju dan bervariasi dalam waktu dan tempat untuk iklim. Referensi statistik, tidak seketika.

ITUR P.834-2: kelengkungan radius, bumi radius ekuivalen k

AGARD CP-70 (1970), penyebaran tropopsic: lapisan tipis dari 1 sampai 100 m dengan gradien kuat adalah umum; koefisien refleksi dari lapisan tinggi adalah tentang proporsional ke panjang gelombang, dengan penyebaran acak sekitar 14 dB. Ukuran ke 90 MHz: penurunan 9- 10 dB diikat ke N melompat terkonsentrasi di sekitar 350 m, di atas lapisan awan. Bagian depan dingin menghilangkan lapisan: Pembedahan tetap.

Laporan URSI AS, Komisi 2 (1960): ketebalan rata-rata 60 sampai 210 m lapisan tinggi (diberikan konsisten dengan P.453), gradien minimum untuk 157- 160 N / km.

Buscemi K2OVS, VHF propaganda panduan: khas jarak untuk pembalikan dan saluran untuk band, saluran terkait dengan tekanan stasioner tinggi, maksimum saat fajar.

Wolinsky- Mancini dan 2024, AIS di Kepulauan Canary: AIS radio banjir dan penerimaan; saluran jauh lebih sering dalam bulan-bulan hangat (inversi aliseans).

Artikel Nigeria dan Malaysia (Ekah, Iloke, Mat, Igwe): hubungan lokal antara suhu, kelembaban, dan jumlah kerugian yang terukur, lemah, dan kontradiksi (r Korpor0,09 sampai 0.27); hanya berguna sebagai konfirmasi bahwa ukuran tanah saja tidak cukup.

Herben 1984, tesis TU Eindhoven (microonde): 30 GHz pada 8,2 km dan OTS satelit di 11.6 GHz; berkilau untuk turbulensi, multi- tentu saja dan hujan. Untuk kehilangan gelombang mikro dinamis didominasi oleh hujan dan turbulensi, bukan oleh saluran.

Baars, The Paraboidal Reflector Antenna (microonde): mendapatkan sebuah contoh G = Affairs) 2 dengan efisiensi khas 0.5-0,65; kehilangan kesalahan permukaan menurut Ruze, Aspes = exp (mores (41f / 112) 2). Ini akan membantu jika kita menambahkan perumpamaan sebagai alternatif untuk Yagi DL6WU untuk 1296 MHz dan seterusnya.

Sudah dianalisis atau konten buruk: Hirschmann NASA X-731-67-89 (ganda), radar VHF ST Reid (angin dan turbulensi, belum dilaksanakan), Scribd slide dan catatan umum pada troposfer.

Kelima putaran sumber (20 November, malam).

Rogers 1997, Radio Science 32 (1): perkiraan Bayesian dari parameter dari saluran yang tinggi (tingkat dasar, M defisit, ketebalan) dari faktor-faktor penyebaran yang diukur di luar cakrawala, dengan model persamaan parabola; percobaan VOCAR di California pada 143, 263 dan 375 MHz, dibandingkan dengan gelombang radio. Sinyal tergantung banyak pada dasar dan defisit M (koreksi 0,81 sampai 143 MHz untuk defisit), hampir sama sekali dari ketebalan (Kore0,17): ketebalan tidak dapat diperoleh dari sinyal. Bagi kita: perhitungan B (ketebalan iklim) jumlah kurang dari defisit dan berbagi; dan metode adalah dasar untuk kantor belakang, yaitu untuk mendapatkan saluran dari frekuensi ganda.

Moler dan Holden 1960, "penyebaran penyebaran penyebaran Troposfir dan sirkuit atmosfer", J. Ress. NBS 64D, pps. 81 dan berikut (asli pada 22 / 11 / 2026, doi 10.628 / jres.064.014). Rute laut California ke Point Loma (San Diego): Santa Barbara 190 mil, Point Mugu 144, San Clemente 78; 1365 MHz dan 9380 MHz, bukan VHF; January- April 1958, rata-rata 1 menit. Kasus 26 Maret: penguatan sekitar 20 dB selama sekitar 3 jam menuju siang, ketika pusaran cyclonic melemah dan mulai angin laut dan subsidence; pada 15 konvergensi direformasi dan sinyal menurun sekitar 22 dB dari maksimum. Statistik (Tabel 1, 500 hPa, 33 kasus): dengan udara yang turun rata-rata sinyal lebih tinggi daripada sekitar 2,5 dB daripada dengan kenaikan udara, di L dan X band. Sinyal berikut perbedaan dihitung untuk 500 hPa dalam tanda dan amplitudo variasi, tanpa korelasi koefisien. Lapisan rendah (di bawah 600 m) mendukung refleksi parsial pada penyebaran. Antisiklon dan front tidak diobati, dan "ekskursi hingga 60 dB" disebutkan di atas tidak: mereka datang dari ringkasan kedua-tangan. Bagi kita: gerak vertikal adalah presektor yang baik, dan + 1.5 dB untuk subsidence dan 120dB untuk udara atas adalah urutan yang sama seperti rata-rata diukur perbedaan (2,5 dB); 20 dB adalah satu kasus, bukan nilai khas, dan di microwave.

Banafaa dan Muqaibel 2025, IEEE Access: survei dari jaringan pipa (model penguapan, parabola, perkiraan radar, GNSS dan lidar) dan klasifikasi dengan pembelajaran otomatis pada langkah-langkah nyata; Dukungan mesin vektor memberikan hasil terbaik. Berguna sebagai bibliografi; data berasal dari Teluk Persia.

ITUR P.834-7 (2015): versi terbaru dari rekomendasi pada refraksi tropoferal; tidak ada formula baru dari yang sudah digunakan.

Abbott, NBS bibliography pada penyebaran tropoferous (GOVPUB- C13): sudah di ronde ketiga. Scribd slide pada saluran: isi generik. Artikel ScienceDirect 0021916987900304 tidak bisa dibuka.

Ronde keenam (20 November, malam): Bean and Dutton, ARRL Handbook 2022 dan 2023, Balanis.

Bean dan Dutton, Radio Meteorology (cap. 6), bagian yang belum digunakan. Sensitivitas lapangan untuk Ns: 0.18 dB per unit N pada siklus tahunan; 0.14-0,24 dB / N dalam siklus siang hari (maksimum dini hari), hingga 0.2- 1.1 dB / N pada rute 175- 200 km di musim dingin; koreksi 0.8- 0.95. Parameter terdiri dari Misme: M = a (g\ _ e metro40 + b) dengan sebuah 'W ^ n' = 0,5 dB per N / km, yang menggabungkan gradien yang setara dengan stabilitas termal (bekerja untuk mengangkat udara 1 km): di Sahara pada 471 MHz pada 160 km satu-satunya stabilitas adalah 20 dB antara Januari dan Juni pada hampir sama gradien. potential refraksi indeks K (N dilaporkan diabocally sampai 1000 hPa): menyoroti massa udara dan subsidence anticyclonic (Flavell dan Lane).

Buku Panduan ARRL 2022 (99a ed.), 19.4.3-19.4.4. Inversi yang berguna antara 250 dan 2000 m; inversi radiasi (sera, udara tenang, tanah kering) jarang menyebabkan benar; antisiklon lambat memberikan yang paling luas saluran per substansi, dengan inversi antara 500 dan 3000 m, lebih kuat di malam hari dan di pagi hari; di belahan utara rute terbaik adalah selatan pusat tinggi. Panas depan: kondisi yang baik beberapa ratus km depan; Dingin depan: kuat tapi tidak stabil membalikkan belakang depan, paralel. Foehn (juga di Alpen) dan angin bumi (udara dingin di tanah, panas pada 200- 300 m; membentang lebih dari 800 km sepanjang pantai). Scirocco: udara panas dan kering dari Sahara lebih dingin udara laut, sangat kuat inversi dari Mediterania timur ke Gibraltar, mungkin penyebab catatan lebih dari 1500 km di microwave. Troposcatter: batas teoritis sekitar 800 km, maksimum setengah khas. The 100 ed. (2023) memiliki teks yang lebih pendek: inversi saat matahari terbit dan matahari terbenam menjadi + 20 dB dan + 20- 50% aliran tingkat; 1200 mil di Great Lakes, khas 400- 600; merekomendasikan Radio Mobile (model ITM) untuk Hepburn cakupan dan peta.

Teori Antena (4a ed), tabel Viezbicke (NBS TN 688, 1976) untuk Yagi dioptimalkan 400 MHz: boom 0.4 / 0.8 / 2.2 / 3.2 / 4.2 / 4.2 7.1 / 9.2 / 102 / 10,2 / 12.25 / 13,2 dBd. Formula DL6WU kami (8.6 + 1 / 9 / 1 / 10,2 / 13,2 = 5 = 5 = 1,2 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = 5 = Dengan rumus yang benar pada asli (9.4 + 7.8 log10 boom / Aversus, dari 22 November) pembuangan jatuh di bawah 0.2 dB antara 1,2 dan 4.2 dBd menjadi 2.2 lawan 12.25) dan tetap 0.6 dB sampai 0.8. DL6WU

Ronde ketujuh (21 November): QST, QEX, W3SZ, GitHub repositori.

QST 2000- 2025 (14 angka, 10 tempat tidur dengan pengakuan teks). Satu-satunya laporan tentang Dunia di atas 50 MHz, berguna sebagai kasus untuk memvalidasi model: September 5- 9, 2000, dilakukan pada 144 MHz dan seterusnya di bagian timur Amerika Serikat; 14- 15 Agustus 2022, pembukaan Manitoba- Kansas / Missouri di bawah tekanan tinggi pada hari panas, dari 144 MHz sampai 1296 MHz hampir semua di FT8, Canada rekor di 1228.56 km ( N0OY -2Q) sinyal di X2Q, QST November 2022 merekomendasikan peta Hepburn. Tidak ada artikel teknis tentang ramalan tropo. VE4MA Q65

QEX dan K1JT, Franke dan Taylor (QEX 2020) pada modes FT4/FT8: FT8 decoding decoding methines 20,8 dB dalam saluran 2500 Hz tanpa evanescence, 18,6 dB dengan evanescence pada lintang medium; FT4 17,5 dB. Angka-angka lain dari QEX 2023 dan 2026 tidak memperlakukan tropo (tiga nomor 2023 adalah unread scan). W3SZ 2019: jalur mode lambat ( JT4A = 23, JT9A = 27, JT65A = 25, QRA64A = 26, FT8 =) dan kriteria pilihan antara sensitivitas dan frekuensi pergeseran toleransi.

GitHub. TropoLink (Lukas), Apache- 2.0): desain rute troposcater dengan P.617-5 (dengan N0 dan GlN), NBS TN101 dan ITM diapit, dan perbedaan yang ditunjukkan sebagai ketidakpastian; P.617-5: L = F + 22 log f + 35 log gd + Lc, F = 0.18 andi-f / troposcatter ( DL2FZN 2013, dari VHF / UHF Handbook RSGB): Lts = 24.44 + 4.283 ln f + 0.06656 d diatas ruang bebas, lekukan dekat PROPBER (0,0674 dB / km). QRadio Predict: ITM dan ITWOM 3.0 dengan S RTM3, tanah menggunakan diagram Corins dan NEC. Tidak relevan: Tropos (aplikasi Weather), operat -adt / tropo dan troposim (Troposferic delay untuk InSAR), tgbot\ _ troposcatter (stasiun militer), sdr-troposcatter-gui (panel kontrol), Troposcatter- Propation -Delay ( ERA5 delay), Radio- Communication- Project (didactilator), Pyyers (channel. N0

Delapan lap (21 November): NTIA dan model ITU di GitHub. NTIA / itm (Longley-Rice, ITS): 20 MHz-20 GHz, kehilangan ruang bebas, diffration dan troposcatter dengan profil tanah, 0,5-3000 m terminal, reaktivitas permukaan N0 (minimity dikurangi ke permukaan laut, 250- 400), tujuh iklim, konstan tanah dan berbagai modes 1 tampilan, 2 troposcatters, 3 + Fragets. Ini tidak model saluran: berfungsi sebagai referensi "atmosfer normal" dengan variabilitas statistik. NTIA / p528 dan eeveetza / p528 (MATLAB): ITU-R P.528-5 untuk koneksi tanah udara, 100 MHZ-30 GHz, 1.520 m terminal, p 1-99%; Kemungkinan menggunakan: saldo penyebaran udara (dua rutinitas darat - pesawat ditambah bagian radar). eveetza / p2001 (MATLAB): ITUR P.2001-6, semua model digital ITU dengan diffraction, troposcatch, propagasi yang abnormal (perilaku dan lapisan, Lba) dan E sporadic, persentase waktu 0-100%; ITU peta digital (DN\ _ Median, DN\ _ SupSlope, DN _ SubSlope, dz _ 01, Trophrej, 0, POH, 0 Kemungkinan menggunakan sebagai perbandingan ITU daripada P.452, dengan GFS ASN bukan peta seperti dalam studi CSIRO; peta harus diunduh oleh pengguna sebagai per P.453. jadi, apa yang terjadi? Validasi 1.3.0 dari 593 QSO (29 / 12 / 2021-2 / 1 / 2022): event / perbandingan pemisahan 0,79 tanpa koreksi, 0,91 dengan Misme dan subsidence; GFS tidak melihat saluran di laut pada 144 MHz (guadagno tanpa koreksi tidak pernah lebih dari 15 dB).

Putaran 9 (21 November): IU3QEZ, NTIA, PyWaveProp, ACE High, validasi 1.3.1. IU3QEZ (S. Fabris), presentasi "Selama cakrawala di VHF" (GEKO Magazn. 69, September 2026): volume umum d2 / (8) dengan R\ _ eff 8500 km (1.3 km sampai 300 km, 3.7 km / km / km per km) dengan kecepatan 2226km per km per km per km per km per km (26km. Bibliografi dari presentasi tersebut akan dipulihkan: Norton 1959 (J. Res. NBS 63D, Sistem Looss), Bray 1961, ARRL Antenna Book 2023 fig. 4.29, Rice-Longley- NBS NBS TN 101, Bean dan Thayer 1911, Barel and Kirby 1961 (diffration on ridge), Deygout-196, NBS TN 101, Bean, PyWaveProp (Python): Metode persamaan parabola dengan pendekatan Padé, beberapa diffration tepi, PETOOL dan RAM, saluran troposferous dan tinggi, tanah tidak teratur: kemungkinan basis untuk perhitungan lapangan dengan M GFS (profil z, akan diperiksa. APRS PropView (Apache 2.0): Peta penyebaran VHF dari paket APRS, statistik per jam dan alarm terbuka; kemungkinan sumber data 144 MHz untuk validasi. Jaringan troposcatter NATO di Italia (fortifications.net): 900 MHz, 10 kW, 20 m perumpamaan; stasiun termasuk Monte Ephero (Ipha), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Vulture, Monte Lauro; kasus tes sejarah untuk P.617. Tidak relevan: Gupta 2021 (transport stratospheric), Hukum 2023 (Zona Arktik), lumba-lumba # 590 dan opera- adt / tropo (InSAR delay), tropo.ino (Arduino gas sensor), tropo- webapi (telefonia), cloudtools / troposfer (AWS). Validasi 1.3.1 pada 564 QSO tropo: ABC 0,79 (model 1.2), 0,80 (+ saluran lunak), 0,90 (+ misme dan subsidence), 0,90 (+ ketekunan); konsit warna (Cheri16 dB) pada 46% dari rute pembukaan dan 0% perbandingan. IQ3QC DL2ALF

Ditambahkan ke putaran yang sama. mikelytaev / wave- propagasi (MIT, Python 3): Persamaan parabola dengan perkiraan Padé dan Numerov, tanah yang tidak teratur, saluran tropospherical; mengisi fungsi refraf yang dimodifikasi (quota, lat, lon) dari NOAA GFS dan mengatur konduksi penguapan dari Monin- Obukhov; tidak ada fungsi dokumentasi yang mengembalikan kerugian pada jalur. Dia adalah kandidat terbaik untuk perhitungan lapangan masa depan dengan profil GFS M (ia akan melayani layanan eksternal atau terjemahan). RP-Sim (AGPL-3.0, JavaScript): Model Epiris untuk jaringan radio (Hata, Dia, Ericsson, dua sinar), tidak ada tropo. RIMFading (GPL-3.0, OMNeT + +): aliran penyimpangan untuk simulasi jaringan, distribusi Weibull, tidak ada tropo. Pylayers: saluran radio dalam ruangan, sudah dikecualikan. qradiopredekrit (kantoun, GPL v2): ITM dan ITWOM v3.0 dengan S RTM3 dan Corine, sudah di putaran ketujuh sebagai QRadio Predicate.

Antena Book, Norton, Barsis, sumber data baru. ARRL Antenna Book (23a ed.), cap. 4 ( K9LA): Metode bray (QST November 1961) untuk kisaran VHF: mulus Bumi kehilangan 99% (300 mil, 144 MHz: 214 dB), lengkungan ke 50% dan 99% (fig. 4.B): 195 dB bernilai 100 mil menjadi 99% dan lebih dari 250 mil ke 50%; Evanescence BB: 7 dB lebih dari 100 mil (5 mil), S3 mil dB (4 mil) lebih buruk dari 5 kali lebih buruk dari B, 5 kali lipat (4 kali lipat (4 kali lipat) Matahari terbit dan terbenam sampai dengan + 20 dB dan + 20-50% aliran tingkat; udara panas dan kering atas udara segar basah di Great Lakes sampai 1200 mil, khas 400- 600; daerah dipengaruhi oleh massa air besar yang paling disukai; akhir musim dingin dengan pemanasan yang mampu inversi kuat. Verifikasi troposcatter kami: P.452 sampai 50% pada 483 km dan 144 MHz memberikan sekitar 204 dB, konsisten dengan 214 dB Bray sampai 99%. Norton 1959 (J. Res. NBS 63D, bebas pada NIST): kehilangan dasar, sistem dan antena jalur memperoleh; Referensi atmosfer Ns = 301; radius ekuivalen dari Ns (250 4878 mil, 301 5.5280, 400 46997); L (99) tabel dengan 30- kaki antena (100 MHz: 163 dB sampai 100 mil, 208 dB sampai 500 mil; 1000 MHz: 138 dB dengan 28-kaki contoh); panjang-tervariasi V (P, Average) tergantung pada jarak. Rice, Longley dan Norton, NBS TN 15 (1959), gratis untuk NIST. Barsis dan Kirby, NBS Report 6751 (1961): 223 km di Colorado dengan Pikes Peak (4292 meter) sebagai tepi, 100 dan 751 MHz; kehilangan konsisten dengan pinggiran (radius 1.5 km); jangka panjang evanescence 11.7-14.3 dB melawan 39- 42 dB dari jarak yang sama; tidak ada siklus yang ditandai; model 4 ray yang diberikan 45 kali lipat dengan kecepatan maksimum ". ARRL VHF Digital Handbook: paket, APRS, D-STAR, WSJT (Scparatter Method dan EME), tidak ada bagian dalam tropo. Sumber data baru yang terbukti atau dicari: ECMWF membuka data (kompresi CCSDS 0.25 °, tingkat 1000, 925, 850, 700 hPa..., tanpa 975, 950, 900 dan 800; G RIB2 dengan CCSDS kompresi, bahwa pembaca kami tidak memecahkan kode; telah tersedia melalui Open-Meteo, 850, 700 hPa..., tanpa 950, 900, G X1Q yang dikompresi bz2, bahwa pembaca kami telah tersedia melalui Operatur-Meteo-Meteo, RIB2

Ronde 11 (21 November): Longley- Rice, GEKO Bean dan Thayer, "Model Indeks Refractive Radio Atmospheric", Proc. model 4 / 3 memiliki lereng yang benar di km pertama tapi jatuh terlalu tinggi; 30% dari kelengkungan dari radius terjadi pada 100 pertama meter dan 60% di km pertama; rata-rata hubungan antara Ns dan penurunan di km pertama: countnN = 7.32 Bean dan Thayer, "C.R.P.L. Referensi Eksponensial Atmosfer", J. Res. NBS 63D n. 3 (1959): N (h) = Ns ^ (prevency) dengan c tetap oleh Ns melalui laporan yang sama; Rata-rata Ns USA 313; balok pelacakan tabel (total kurva 15.2 mrad dengan model eksponensial versus 30.3 mrad dengan 4 / 3). MATLAB refractionexp: c = 0.143859 km per Ns = 313 (skala sekitar 6,95 km), valid di atas sekitar 1 km dan 10 mrad. Gunakan untuk kami: profil "normal" perbandingan dapat bahwa CRPL dengan Ns lokal bukan M = 315 + 0.118, dan anomali penurunan di km pertama harus diukur dibandingkan dengan September 7.32 ^ (000577) daripada PT 40. Laporan NTIA 82- 100 (Hufford, Longley, Kissick 1982), panduan ITM dalam mode area: climi dan Ns menyarankan (equatorial 360, benua subtropis 320, laut subtropis 370, gurun 2980, iklim benua 301, iklim laut di darat 320, suhu laut di laut 350, Ns = N0 Nasi, Longley dan Norton, NBS TN 15 (1959), bagian I: meramalkan formula distribusi dari waktu ke waktu oleh tanah dan gelombang troposfir, basis TN 101. SoftWright, Wikipedia, Altair WinProp dan MATLAB rfprop. LongleyRice: informasi yang sama (versi 1.2.2, 20 MHz-20 GHz atau 40 GHz, 1-2000 km, Ns 301, tujuh iklim, variabilitas waktu, tempat dan situasi); tidak ada satu bentuk saluran, hanya efek statistik dari iklim laut ("sangat tinggi bidang selama 10% dari waktu atau tidak"). Majalah Geko. 69 ( IU3QEZ): cakrawala radio 4 / 3 dari 130 km dari 1000 m, 260 km menambahkan dua stasiun; troposcater dengan volume umum antara 1-3 dan 10 km; tersebar dari pesawat ke 10 km dengan + 10- 20 dB untuk puluhan detik atau beberapa menit; kursus teoritis dengan 2 x 10 elemen per sisi: SSB QRP sekitar 440 km, SSB kurang dari 510 km

## 12. Tabel bulat: sumber untuk memvalidasi model atau benar itu (21 November)

Ringkasan semua sumber, diperintahkan untuk digunakan. "Negara" mengatakan jika sumber sudah dalam program (1.3.1), apakah itu dalam tes atau apakah itu harus dilakukan.

### Data untuk memvalidasi (apa yang sebenarnya terjadi)

| Sumber | Apa yang memberi | Gunakan | Keadaan |
| Laporan MMMVHF dari penulis | 593 QSO di 144 MHz, 29 / 12 / 2021-2 / 1 / 2022, waktu dan lokasi | Validasi acara terhadap perbandingan (+ 14 hari) | Dalam penggunaan: ABC 0,79 (1.2) = 90 (1.3.1) |
| Dunia diatas 50 MHz | dokumen kasus (14- 15 / 8 / 2022 Manitoba- Kansas, 144-10 GHz) | kasus historis | dalam penggunaan sebagai kasus historis; untuk ditambahkan ke validasi QSOs |
| wspr.live (WSPR) | lengkap arsip, 2 m dan 70 cm, SQL | banyak kejadian dan banyak "tidak" (jam tanpa spot) | mencoba, itu harus disaring (tempat yang salah di 16- 20 ribu km) |
| APRS PropView / APRS- IS | stasiun merasa, jarak, jam | hanya validasi 144 MHz | untuk melakukan |
| Reverse Beacon (arsip) | tempat harian | VHF | dibuang untuk saat ini |
| Aku GRA2 radiosondaggi (Brindisi 16320, Praktek Mare 16245, Trapani 16429, Udine, Milan) | profil diukur 00 dan 12 UTC | GFS profile control dan inversi | ditemukan berkas (Brindisi 46 MB), yang akan digunakan |
| Barsis dan Kirby 1961, Barsis 1963 | evanescence diukur di jalan dengan rintangan pegunungan | validate kategori "monte" (Vesuvio, Faito) | untuk melakukan |
| Norton 1959, NBS TN 15, TN 101 | L (99) dan kerugian media tabel | Menyebar kontrol | Sebagian (melihat 300 mil konsisten) |

### Koreksi yang disarankan oleh sumber

| Koreksi | Sumber | Efek yang diukur pada validasi | Keadaan |
| Stabilitas Misme Masa | Bean dan Dutton Cap. 6 | AUC 0,79 = 90 = Dengan subsindensi | dalam 1.3.1 (Judul 16 dB tanpa saluran) |
| Subsidence dari gerak vertikal | Moler dan Holden 1960 (dibaca secara asli pada 22 / 11: + 2,5 dB rata-rata antara udara turun dan naik udara, 1.4 dan 9.4 GHz oleh laut) | sebagai di atas | dalam 1.3.1 (+ 1,5 / 13,2 dB) |
| Pipa lunak (sampai 3) | Kerr, Hepburn, VHF / UHF DX Book, Shi 2019 | hanya + 1% dari AUC; membawa warna dari saluran pada 46% dari rute acara dan 0% dari perbandingan | in |
| Ketekunan 24 h | VHF / UHF DX Book, Biancomano dan Leyshon | ABC tidak berubah, lebih diambil dan lebih salah alarm | dalam 1.3.1 |
| Profil CRPL normal dengan Ns lokal (KoresN = 7.32) daripada 40 N / km tetap | Bean dan Thayer 1959, NBS Monograph 4 | diuji dalam validasi | dalam 1.3.2 sebagai alternatif, off karena cacat |
| Super-refraksi threshold 79 N / km (ITU) daripada 100 (Bean dan Dutton, lapis 100- 850) | P.453, Lopez 2008, DX Book | diuji dalam validasi | dalam 1.3.2: estimasi agresif bersama bijaksana (peta menggunakan bijaksana) |
| Ketebalan iklim (kalkula B) kurang penting daripada defisit | Rogers 1997 | untuk mencoba: gunakan A untuk mendapatkan | untuk melakukan |
| Persamaan parabola dalam profil GFS | PEPOOL, wave- propagasi (Lytaev), Levy (buku tidak membaca, hanya pratinjau), Lindquist | versi pertama dengan bidang di atas cakrawala didominasi oleh numerik kebisingan (ABC 0.12-0,28): benar (domain 8 km, balok 6 °); setelah koreksi AUC 0.54-0,66 | digunakan sebagai perhitungan kedua, tidak memutuskan perkiraan |
| Evanescence: = 7 dB lebih dari 100 miles, margin 10- 20 dB untuk 90- 99% | Bray 1961, Antenna Book, GEKO 69 | Batas-batas cara dalam kondisi marjinal | digunakan: probabilitas untuk perioda dan threshold FT8 emper18,6 dB dalam troposcatter |
| Volume umum dan jarak yang setara dari Bray | IU3QEZ, Bray 1961 | Informasi | in |
| Troposcatter P.617 dan RSGB sebagai ketidakpastian | TropoLink, DL2FZN | Informasi | dalam 1.3.0 |

### Apa yang tersisa dari model

Dibangun di laut pada ketinggian rendah (100- 200 m) bahwa tingkat GFS tidak melihat (studi sumber data saat ini); refleksi dan penyimpangan di pegunungan terdekat (Vesuvio, Faito): proposal untuk perhitungan "via monte". ITM / Longley- Rice tetap referensi statistik "hari normal" (Ns 301, iklim), tanpa saluran. Discarded oleh keputusan penulis: AirScout, P.528), E sporadic (P.2001) dan 50 MHz (servoir EIRP yang dimiliki beberapa orang dan membingungkan dengan E sporadic).

### Proposal untuk urutan kerja

1. Tambahan Validasi: tambahkan WSPR disaring (bahkan jam tanpa spot) dan QST 2022; ulangi untuk 432 MHz.
2. Coba profil normal CRPL dan ambang batas ITU dari 79 N / km, satu per satu, menjaga hanya apa yang menimbulkan AUC.
3. Perbandingan profil GFS dengan gelombang radio I GRA2 dari Brindisi dan Praktek Laut di hari-hari tersebut.
4. Evaluasi ulang persamaan parabola setelah koreksi numerik.

### Keputusan 21 November (versi 1.3.2)

- Diadopsi: perkiraan bijaksana (Bean dan Dutton, 100 N / km) dan agresif (ITU, 79 N / km) diapit; saluran hanya dicari di bawah 2500 m (pertama 1500 m, mengangkat setelah validasi); bonus dengan tekanan yang diperpanjang tinggi dan angin lemah; berat laut dengan PIN 1 dari P.452; berbagi dari stasiun dengan saluran juga pada saluran parsial; lapisan di atas punggung ( IT9GSF).
- Diadopsi sebagai alternatif, dimatikan oleh cacat: profil CRPL normal dengan lokal Ns.
- ABC dari 0.12 ke 0.54 (antena 30 m) dan dari 0.28 sampai 0,66 (300 m).
- Daratan: Copernicus DEM GLO-30 daripada Terrain Tiles "skadi", yang dekat pantai memiliki antrean hantu 80- 100 meter di laut ( IC8TEM, titik diverifikasi secara langsung ke Capri); skadi tetap sebagai cadangan.
- Cuaca bertanda-tanda pada peta dengan efek mereka: substandence yang menguntungkan (Moler dan Holden 1960, rata-rata sekitar 2,5 dB antara udara turun dan udara naik; + 1.5 dB), air naik tidak menguntungkan (tergantung dalam; Secon2 dB), S scirocco petunjuk menguntungkan (ARRL Handbook lest 19.4, inversions sangat kuat di Mediterania; tidak ada koreksi). Simbol ganda, warna hijau / merah / oranye dan buku komik di peta.
- Diadopsi ITM 1.2.2 (NTIA / itm, domain publik), diterjemahkan ke C #: menurut perhitungan dari dasar dalam atmosfer normal di sebelah P.452 dan peregangan terakhir setelah keluar dari saluran yang berakhir sebelum koresponden. Discarded qradiopredekrit kode karena di bawah GPL v2.

Tanpa isi yang berguna untuk indeks: * Communications Quarterly *, musim gugur 1991 (tidak ada artikel tentang tropo; artikel Atkins di Scatter pada musim dingin 1991).

- Diadopsi sebagai prinsip manual (21 November): 'Propagasi tidak dapat digeneralisasi, tetapi dipelajari dalam kaitannya dengan morfologi lokal' ( DF5AI, dilaporkan oleh IK3XTV dalam 'Propagasi pada 2 meter'); dikutip dalam manual prakiraan (lx 1) dan di dewan.
- Adopsi (21 November, setelah 1.3.3) interpretasi pembukaan transalpine IK3XTV: bukan "sel konverktif seperti lensa", tetapi troposcatter diperkuat oleh turbulensi ke atas lapisan kembali berlaku ketika mencapai share dari volume umum (manual dari forecast, 138 meter). Dari 1,3,4 itu memasuki titik perhitungan sebagai penguatan sementara dari troposcatter, paling banyak 3 dB. Discarded oleh artikel yang sama: "self-amplification" dari sinyal di luar Alps (media pasif tidak menguatkan: link yang dijelaskan tetap dapat dijelaskan dengan diffraction dan troposcatter; kekuatan yang dinyatakan tidak digunakan), salju yang "improves diffraction" (kemungkinan besar lapisan stabil atas salju, dengan evanescence kurang).

- Kerr (1951) meninjau pada buku (22 November, PDF disediakan oleh penulis): Gambar eq. 18 dalam pemindaian tidak dapat dibaca, tapi tabel 1.1 menegaskan: 20 ft £1 cm, 80 ft £10 cm, 400 ft £100 cm, 2000 ft 1000 cm, terhadap 1.25, 112, dan 1252 cm Natsumax = 0.014 Hipotesis Kerr: di dalam saluran indeks yang diubah tetes 4 = 10 = 8 per kaki, nilai khas dari saluran rendah; dengan gradien lemah membutuhkan ketebalan yang lebih besar. Kerr juga memperingatkan bahwa Ludmax bukan potongan jaring: panjang gelombang kadang-kadang masih dapat sangat dipengaruhi oleh saluran.

- DL6WU tinjauan awal (22 November, PDF dari VHF Communications 3 / 1982 disediakan oleh penulis): G. Hoch, "Sangat Panjang Yagi Antennas", p.130 dan berikut. Fig. 1: mendapatkan pada dipole diukur pada 432 dan 1296 MHz dalam polygon profesional dari Jerman Posts, dengan antena contoh EIA dari 7,7 dBd, diperkirakan akurasi 0.5 dB; 2.35 db untuk dua kali lipat boom, tidak ada saturasi sampai sekitar 20. Program ini digunakan 8.6 + 8.0.log10 (L / 1f) dBd, yaitu sekitar 0.8 dB dalam kurang dari 1 -2; sekarang menggunakan 9.4 + 7,8 Discarded rumus 10 (7.8) beredar di internet: akan memberikan 3 dB untuk dua kali lipat, terhadap 2.35 kali diukur.
- Booker dan Gordon (1950): referensi dikonfirmasi oleh "Film Klasik Minggu Ini" (Isi Saat ini, 8 Februari 1982, komentar oleh H. G. Booker): Proc. Komentar tersebut menegaskan dua poin yang digunakan dalam manual: bidang yang diterima di luar cakrawala dengan distribusi Rayleigh, dan fungsi autokorelasi eksponensial dari penyimpangan (bukan-Gaussian). Artikel asli belum dibaca.

## 13. Dari artikel IK3XTV

Dua artikel yang ditulis oleh Flavio Egano IK3XTV (Radio Rivista: "Propagration on 2 meter" dan "Propagasi VHF - sel-sel konviktif dan gelombang gravitasi") menambahkan dua hal yang tidak ditutupi oleh model saluran: pembukaan transalpine pada sore hari karena perubahan, dan aturan praktis di mana dan kapan jaringan pipa terbentuk di Italia. Mereka adalah pengamatan dari stasiun, bukan ukuran: mereka harus diperlakukan sebagai hipotesis untuk memeriksa dengan bintik-bintik.

| Komentar penulis | Bagaimana menjadi bahan indeks |
| Saluran terutama terbentuk antara 100 dan 1000 m, jarang lebih tinggi; melayani tekanan tinggi di daerah yang luas, introlasi baik dan tidak adanya angin | cari untuk saluran hanya di bawah 1500 m; bonus dengan tekanan diperpanjang tinggi dan angin lemah di lapisan rendah |
| Dalam bulan-bulan hangat dilakukan berulang di Adriatic antara Veneto dan Puglia, 700- 800 km, kadang-kadang sampai Yunani, rute laut favorit | bobot lebih besar ke fraksi rute laut (seperti CV1 dari ITU-R P.452) |
| Yang berkaitan harus dalam atau dekat saluran, dengan antena tinggi dan beberapa meter memimpin sinyal tidak masuk | bandingkan bagian dasar saluran dengan bagian dari dua stasiun (akan melayani ketinggian.) |
| Waktu yang sama, saluran yang sama: dengarkan dari Gargano sebuah stasiun di 144 dan satu di 50 MHz | untuk memeriksa: menurut tabel Hepburn 50 MHz meminta lebih dari 300 m memimpin, sehingga acara akan menunjukkan sebuah pipa sangat sering |
| Connections Veneto - Jerman Selatan di luar Alpen, tanpa visual, hampir selalu di sore hari dan dengan tekanan tinggi; sinyal tidak stabil yang menghilang dan muncul kembali setelah beberapa menit | Indeks "converctive" dipisahkan dari saluran, untuk rute gunung |
| Sel-sel konverktif lahir di tanah dan naik sekitar 1 km setiap 3 jam: pada 15 mereka mencapai sekitar 3 km, ketinggian yang berfungsi untuk menimpa Alpen | gunakan tinggi lapis batas (batas\ _ layer\ _ tinggi dari Open-Meteo GFS) sepanjang jalur: skor ketika melebihi ketinggian dari ridges |
| Pegunungan Alpen menghasilkan gelombang gravitasi yang, bersama-sama dengan gerakan konvektif, menciptakan penyimpangan indeks refraktif | hipotesis: kuat angin tegak lurus ke rantai untuk 700 hPa sebagai sinyal gelombang emas; untuk mencoba terakhir |
| Sangat inversi tinggi (sampai 8 km dalam kasus yang luar biasa) memberikan sekitar 800 km, tetapi dalam praktek jarang lebih dari 300 km | share dari inversi memperluas lingkup: data berguna untuk memperkirakan jarak menunggu |
| Dalam radius musim panas bumi setara dengan sekitar 16000 km (2,5 kali nyata), dengan sekitar 60 dB dalam waktu kurang dari 300 km | konsisten dengan k50 = 157 / (157 cheonnN): k = 2.5 sesuai dengan JUN sekitar 94 N / km, itu super-refraksi |
| Lebih dari 3000 km hanya di 2030 ° kisaran utara dan selatan khatulistiwa, di atas laut, terkait dengan sel Hadley; puncak pada bulan Agustus di Afrika-Eropa | luar Eropa: berguna untuk tidak memberikan indeks tinggi di Atlantik tanpa umpan balik, dan untuk rute Canary - Inggris Raya |

Data berguna lainnya dari artikel yang sama:

- Jangkauan optik antara dua h1 dan h2 meter (d dalam km): d = 4.1
- Evanescence: lambat dan dangkal dengan terbalik; kuat dan cepat dengan penyebaran dan sel-sel konvektif. Program ini bisa menggunakannya untuk mengklasifikasikan iklan jika suatu hari itu akan memiliki laporan SNR dari waktu ke waktu.
- Untuk sporadic E dalam 144 MHz penulis melaporkan karya DF5AI dan Davis dan Johnson (Alam 435, 2005) pada obligasi antara temporals dan E: dari tropo, tetapi indikator terpisah besok dapat menggunakan data petir.
- Refleksi tanah dalam melompat ganda E: laut K = 81 dan 5 S / m, pedesaan dan hutan K = 13 dan 0.006 S / m, daerah perkotaan K = 3 dan 0.001 S / m.

## 14. Hipotesis indeks LIVETROVHF

Proposal awal, yang akan dibahas: indeks 0-10 pada skala yang sama dengan Hepburn, dihitung untuk setiap titik kisi dan setiap 3 jam dari GFS (kemudian ECMWF dan ICON untuk perbandingan), dan nomor kedua per rute yang mengatakan berapa banyak bagian dari rute yang masuk ke saluran yang dipilih untuk band. Berat di bawah ini adalah asumsi saya, akan dikalibrasi dengan bintik-bintik.

Ulasan setelah Bean dan Dutton dan Kerr: batas profil datang dari meja mereka 6.5 dan skala dari pengukuran mereka, sehingga lapisan refractive bernilai 10- 25 dB atas profil normal. Indeks kemudian dihitung pertama di dB dan kemudian mengambil 0-10; nilai-nilai dalam gunting yang tetap harus dikalibrasi dengan bintik-bintik.

Kalkulasi berdasarkan titik, langkah demi langkah

1. T debit, kelembaban relatif, tinggi geopotatif dan kecepatan vertikal pada 1000, 975, 950, 925, 900, 850, 800, 600 dan 500 hPa, ditambah T dan 2 m kelembaban, tekanan tanah, tinggi lapisan dan angin pada 10 m.
2. Buang tingkat di bawah tanah; perhitungan N dan M di setiap tingkat (ITUR P.453).
3. KalkuladN / dh dalam setiap empat lapisan Bean dan Dutton (100- 850, 850- 700, 700- 600, 600-500 hPa) dan bagan sebagai sub- refractive, normal atau super- refractive dengan ambang tabel mereka; di dalam lapisan pertama juga menggunakan tingkat menengah untuk menemukan gradien terkuat dan berbagi.
4. Dimana dM / dh < 0 saya menemukan dasar dan atas dari saluran, ketebalan d dan defisit pyM.
5. Perbandingan dengan ketebalan minimum untuk band dari rumus Kerr (sekitar 180 m untuk 144 MHz, 89 m untuk 432 MHz, seperti dalam tabel Hepburn); di bawah ambang saluran masih dihitung, tapi kurang.
6. Saya menstimulasi keuntungan di dB dengan tabel di bawah ini (Anda mengambil lapisan terbaik, bukan jumlah lapisan, ditambah jangka anomali) dan mengubahnya: indeks = dB / 2,5, antara 0 dan 10, jadi 25 dB bernilai 10. Nilai negatif (sub-refraksi) menunjukkan bagaimana 0 tetapi tetap dalam data path.

| Situasi lapisan terbaik | Criteria | Perkiraan pendapatan |
| Normal (tidak dilaminasi) | KoredN / dh dalam jangkauan tab. 6.5 (mis. 20- 60 N / km antara 1000 dan 850 hPa) | 0 |
| Sedikit di atas normal | antara batas atas dari band normal dan ambang refraksi super- | 0 sampai 10 dB, dalam proporsi |
| Super-refraksi | melampaui batas lapisan (100, 80, 70, 50 N / km pendakian) tetapi di bawah 157 N / km | dari 10 sampai 15 dB, proporsional hingga 157 N / km |
| Pipa terlalu tipis untuk band | dM / dh < 0, ketebalan di bawah Ambang kerr | 15 |
| Konektor berguna untuk band | dM / dh < 0, ketebalan di atas threshold Kerr | 20 dB, + 1 dB setiap 5 juta unit defisit, maksimal 25 |
| Sub- refraksi | MechandN / dh < 0 di lapisan bawah | 10 dB |
| Latar belakang anomali (Anda tambahkan) | Model Ns minus ITU peta iklim Ns untuk bulan itu | 0.2 dB per unit N, antara September 5 dan + 5 dB |

Menghitung berdasarkan rute (antara dua lokasi): contoh lingkaran maksimum setiap 10 km, seperti yang telah dilakukan program untuk indeks pada jalur, dan menghitung pecahan titik dengan saluran yang berguna dan pembagian rata-rata dari basis. Studi CSIRO menemukan bagian berguna dari jalur pipa (pada rute AIS di 162 MHz adalah 67% lebih tinggi daripada dalam kasus kontrol).

Menambahkan perhitungan per rute (dari LA0BY dan W3SZ): (1) stasiun laut dibandingkan dengan basis dan saluran atas, karena saluran tinggi hanya melayani mereka yang masuk, (2) sebuah troposcatter "lantai" dengan metode Yeh, dengan sudut awal diambil dari profil dasar; hasil trope adalah dihapus dari dB dan hasilnya dibandingkan dengan kerugian stasiun yang dikelola untuk mengatasi (tabel KK6MC). Jadi indeks juga menjadi perkiraan jarak yang terjangkau.

Corrections to try one at a time (from Bean and Dutton and LA0BY, IK3XTV, G4MCU already in the manual), to be kept only if they improve compare with spot:

- Mencampur (importasi penting menurut penulis, dengan pengamatan harian): hari di bumi, antara tengah pagi dan sore dengan langit yang cerah, atau dengan angin di atas tanah selama beberapa m / s, saluran di tanah menghilang; kontribusi mereka harus dikurangi. Tidak di laut.
- Waktu itu (importasi penting menurut penulis, menurut pengamatan sehari-hari): lubang irasional lahir saat matahari terbenam dan jatuh setelah matahari terbit; itu adalah kontrol ramalan 3 jam.
- Gerakan vertikal: udara naik ke 850- 700 hPa remix lapisan (koreksi negatif), udara turun di bawah antisiklon memperkuat mereka (positif).
- Stres trend: tekanan menanjak pada jalur dalam beberapa jam terakhir, sinyal yang lebih tinggi dan stabil; dalam penurunan, sinyal lebih rendah (bastano 2 hPa).
- Sinonim: tekanan tinggi di atas 1025 hPa, suhu 850 hPa jauh di atas yang di tanah, udara panas dan kering mengalir di laut dingin, rute oleh laut.
- Path pegunungan: indeks "convektif" kedua (bagian IK3XTV), dengan lapisan batas sore lebih tinggi dari ridges sepanjang jalan dan tekanan tinggi.

Dari VHF / UHF DX Buku, koreksi lain untuk mencoba: hari berturut-turut tekanan tinggi pada titik (lebih banyak hari, lebih banyak poin), posisi dibandingkan pusat dari antisiklon (bonus di tepi), gradien tekanan di tanah sebagai ukuran angin, hujan kuat dalam 24 jam sebelumnya (penalitas), awan rendah di tepi lapisan (denda hanya untuk band tinggi). Untuk arah rute: bandingkan azimuth dari rute dengan isobar.

Verifikasi

- Arsip LIVETROPOVHF: Setiap iklan lebih dari 500 km hingga 144 atau 432 MHz adalah "event". Ini membandingkan indeks pada rute dengan frekuensi peristiwa, pada waktu yang sama hari dan bulan (pendekatan yang sama dari penelitian CSIRO).
- Perbandingan dengan Hepburn dan F5LEN pada hari yang sama: di mana tiga indeks terputus, Anda melihat siapa yang benar menurut bintik-bintik.
- Radiosundasi dari 00 dan 12 UTC: untuk memeriksa apakah model melihat inversi bahwa ukuran probe.
- Kasus bersejarah LA0BY (6-11 / 11 / 2003, 10 / 12 / 2004, 15 / 10 / 2011, 11 / 2 / 2015, 22 / 11 / 2018): indeks dihitung ulang pada hari-hari itu harus melihat lubang yang Hepburn tidak tunjukkan.
- Batas yang akan dinyatakan dalam manual: dilakukan di bawah 100-200 m hampir tidak terlihat tingkat tekanan GFS; prediksi yang dapat diandalkan selama 2-3 hari, kemudian hanya tren.

## 15. Sumber dari 21 November (bagian kedua)

- ITUR P.1812 (MaTLAB / Octave implementasi I. Stesanovic, OFCOM; GitHub eeeetza / p1812, Zenodo 10.5281 / zenodo.6141558). Specific VHF / UHF metode area rute, berasal dari P.452: "delta- Bullington" diffraction, troposcatter, saluran dan refleksi pada lapisan, menelan tanah. Berguna sebagai perbandingan ketiga di sebelah P.452 dalam perhitungan titik; AVN dan N0 data penuh produk ITU, sehingga pengguna mengunduh mereka (sesuai dengan P.453).
- Dari Costa, Rabanal Ramirez, Dias, IET MAP 2018 (doi 1049 / ietmip.2017.0992). 563 MHz, 27 poin antara 5 dan 30 km pada medan yang tidak teratur dengan hutan: P.526 dengan 12 meter vegetasi 4.3 dB dan 7.8,8 dB; P.1812 dengan 10 m dari encumbrance + 2.6 dB dan Katne8.9 dB; dielectric weakers 4.8 dB dan 9.2 dB; P.1546 + 5.0 dB dan 11,7 dB. Untuk program: kesalahan residual dari metode diffration yang baik adalah sekitar 9 dB, dan tinggi dari encumbrances pada ridge (10-12 m) jumlah.
- PEPOOL (Ozgun e altri, Computer Fisika Komunikasi 182, 2011). Persamaan parabola menempel pada satu dan dua rute dengan proporator sudut lebar (sudut 40-45 °, terhadap 10- 15 ° dari yang sempit); bawah dengan kerugian melalui perubahan Fourier dicampur; ground ke langkah. Mengkonfirmasi pilihan dari 1.3.2 (sudut lebar, koefisien tanah) dan mengindikasikan perhitungan dua arah untuk antrean curam.
- MathWorks. `tropopl`: hilangnya gas dan kehilangan "lente" dalam suasana berlapis (P.835, P.676, P.453), tanpa saluran: dengan gradien di bawah Kore157 N / km model geometris optik tidak berlaku. Contoh dari penyebaran radar yang digunakan untuk refraksi JUMI = 7.32 `propagationModel` Longley- Rice: nilai polarisasi horizontal yang telah ditentukan, konduktivitas 0.005 S / m, permittivitas 15, reaktivitas 301, suhu iklim benua. Tidak ada model MathWorks berurusan dengan saluran.
- ITUR, produk digital dari grup 3. Data dan program dari P.453, P.452, P.1812, P.2001, P.528, P.1546; 'data terintegrasi adalah bagian dari rekomendasi; tidak ada lisensi redistribusi yang dideklarasikan: Anda terus membiarkan mereka mengunduh ke pengguna.
- ESA Navipedia, penundaan troposferical. N = Nidrostatica + Numida; keterlambatan zenital kering adalah sekitar 2,3 m dan kelembaban satu puluhan cm, yang bervariasi dengan cepat. Bagi kita: jeda basah diperkirakan oleh stasiun GNSS adalah ukuran tidak langsung dari kelembaban terpadu, kemungkinan sumber masa depan dari casting-sekarang; Hari ini tidak dapat digunakan.
- Bray, K2LMG, "A Method for Deterating V.HF Station Capabilities," QST November 1961, pp. 36-40 (baca aslinya), cek ulang pada 22 November pada PDF dari jumlah QST: semua nilai dibawah korespondensi). Penulis, D. W. Bray (Jenderal Electric, Ithaca), mengatakan ia telah memverifikasi prakiraan dengan bulan dijadwalkan koneksi dengan K2GOI dan W4LTU. Evanescence dalam waktu 13,5 dB lebih dari sekitar 100 mil (7 dB di bawah dan 7 di atas rata-rata per jam), hampir linear di bawah (3,5 dB sampai 50 mil): untuk sambungan yang solid menghapus 7 dB. Koreksi cakrawala: masing-masing ketinggian cakrawala bernilai 69 mil (111 km) jarak ekstra (kurang jika negatif). Gain of a yagi about 10 × boom / azone (rasio daya), lebih dari 4 dB dari refleksi tanah; SSB 13 dB dan AM 7dB dibandingkan dengan CW; 99% dari jam: sekitar 180 dB sampai 60 mil, 190 sampai 90, 200 ke 240 (2 m). Dari 1,3.2 kemungkinan untuk perioda menggunakan 3.4 dB (} 7 dB sebagai = = = 5 dB diperkirakan kedua tangan.
- Dua - cara perhitungan (mencoba dengan prototipe, 21 November). Besar sudut parabola persamaan dengan medan S RTM1 nyata dari Capri, 144 MHz, antena 10 m dari tanah, atmosfer standar; mengukur: daya rata-rata antara 20 dan 400 m sampai 80 km di rute menuju yang sesuai. Dari pusat Capri dan Marina Grande barat ditutup (hambatan ke + 11-21 °, Monte Solaro): menunjuk Faito (hampir di belakang EA5 dan EA6, 150-160 ° sumbu off) sinyal diluncurkan menuju Spanyol naik 10-29 dB; menunjuk Vesuvius (110-125 ° off sumbu, kemudian keluar dari rencana perhitungan, hanya indikasi dari 17- 25 dB. Dari Anacapri barat adalah gratis dan kemenangan langsung dari 8- 22 dB. Kerugian mutlak pada 10000-1300 km dari prototipe, bagaimanapun, tidak dapat diandalkan. EA3 EA5 EA6
- Barsis dan Kirby, NBS Report 6751 (1961), Pikes Peak, 223 km, 100 dan 751 MHz. Gunung ini merupakan terminal umum dari dua rute optik: distribusi medians per jam adalah komposisi dari dua rute. Ekskursi jangka panjang (1- 99%) dari sekitar 14 dB, terhadap 26- 40 dB dari jalur troposcater dengan panjang sama atau sudut; tidak ada perubahan hari yang dapat dihitung. Sinyal lebih kuat daripada troposcater tapi lebih lemah daripada sudut ideal: bagian atas harus diperlakukan bulat (kasar sekitar 115 km). Sinyal tinggi selalu diamati di belakang.
- Barsis, NBS Report 7652 (1963), tiga rute di atas gunung (Colorado, Alaska, Eropa), 750-900 MHz. Rata-rata kehilangan 10- 20 dB lebih tinggi daripada Fresnel- Kirchhoff perhitungan tanpa tanah di dekat terminal; evanescence diperkirakan dengan membungkus dua baris dalam optik; refleksi tanah dekat terminal lebih buruk memudar berkepanjangan. Dari 1.3.2 masuk ke saluran menambahkan pembulatan P.526,000.
- Barsis dan Johnson, NBS TN 88 (1961). Lama melarutkan gelombang ruang angkasa, bahkan di jalan di luar gunung (175).
- Norton, "Kehilangan sistem dalam penyebaran gelombang radio", J. Res. NBS 63D (1959). Kurva dari mana Bray mendapat nomogram; definisi kehilangan sistem, transmisi dan dasar, proses keuntungan antena.
- Rice, Longley, Norton dan Barsis, NBS TN 101 (1965-67), Volumes 1 dan 2. tepi tunggal dan kendala bulat dengan dan tanpa refleksi (that7, dengan contoh Pikes Peak), variabilitas jangka panjang V (0,5) dan Y (q) untuk iklim (marah conconcontinental dan lainnya). Bagi kami: metode dari fungsi rintangan dan keragaman bulat adalah referensi untuk mengkalibrasi ketidakpastian.
- ITUR P.526-16 (11 / 2025). Formula-la (31), identik dengan program ini; Runded hurdle (32) - (36), sekarang digunakan; multi- split methodes (Bullington dan full method F4.5).
- ITUR P.1812-8 (2025) dan -7. Teks lengkap pulih: metode spesifik dari 30 MHz sampai 6 GHz (delta- Bullington, troposcatter, saluran dan refleksi pada lapisan, ingombs). -7 memperingatkan bahwa troposcater (dari P.617-5) salah di bawah 66 km. Masih mungkin perbandingan ketiga.
- Paulus, "Aplikasi Plavakik dari model penguapan IREPS", NOSC TR 966 (1984), dan Radio Science 20 (1985). Laporan terbaca pada 22 / 11 / 2026 (DTIC ADA146528, scan dengan teks yang dikenali, terkadang tidak dapat dibaca). Perbedaan udara-laut diukur oleh kapal bergeser menuju stabilitas: udara laut hangat dalam 31- 35% dari data kapal dibandingkan 0.7-7% dari babi NOAA, untuk psicrometer terganggu oleh kapal dan SST diambil dari air pendingin (sekitar + 1 ° C). Model Jeske kemudian memberikan 40 m dan lebih dari 15% tahun dan ketinggian yang melompat dari 10 sampai 40 m dalam 24 jam dengan waktu yang stabil, tidak dikonfirmasi oleh ukuran radio. Koreksi yang telah direncanakan (batas udara 0, 13,5 dan 1° C; yang 1° C lebih baik setuju dengan ukuran radio Juni 1982): jika perbedaan udara adalah 1x1 ° C atau kurang dari perbedaan yang diukur; jika tidak, Anda menghitung tinggi dengan 1 ° C dan dengan 0 ° C dan, jika itu dengan September 1 ° C lebih rendah, Anda pegang itu. Peningkatan pada sisi stabil dipotong, penurunan tetap. Pada bulan Juni, Marsden 120 persegi: rata-rata tinggi dari 16,8 m (klimatologi) ke 7.6 m. Persamaan model tidak ada dalam laporan (mengacu pada Jeske dan Hitney). Program ini menggunakan profil netral, yang tidak memiliki kelebihan yang stabil, dan membatasi levelto 40 m: ia pergi ke arah yang sama; dengan SSTs model (bukan kapal) kesalahan pengukuran yang Paul koreksi berat badan.
- Levy, "Metode Equasi Parabola untuk Propagasi Gelombang Elektromagnetik" (IEE 2000). Pada Google Library hanya ada pratilik: Aku tidak membaca.
- VK3FS (3fs.net.au). Perkenalan kualitas: tinggi saluran di 450- 2000 m untuk membuka lebih lama di tanah; inversi 300- 600 m dalam tenang dan tenang malam, yang menghilang setelah matahari terbit; inversi pesisir yang lebih kuat di sore hari; 800- 1300 km khas, 1400- 1600 km sering dilakukan. Konsistensi dengan peringatan siang / malam dan langit-langit 2500 m.

## 16. Sumber

Aturan dan rumus

- Font color = "# 404040" ITUR P.453- 13 (2017), indeks refractive radio: Formula](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)dan data
- [ITUR P.452- 18 (2023), prediksi interferensi antar stasiun darat lebih dari 100 MHz: radius ekuivalen, HLU0, troposcatter, saluran](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)
- Font color = "# 404040" ITUR P.617-5, penyebaran luar cakrawala untuk jembatan radio](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)
- [Shi et al., "Studi Komprehensif tentang Wave Maximlength of Electromagnetic Propagration di Jalur Evakuasi Berbeda," IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

Belajar tentang prediksi saluran dengan model cuaca

- [P. Lopez, "Sebuah resolusi 5- tahun 40-km global klimatologi dari super-refraksi untuk meteorologi radar berbasis tanah," ECMWF Tech. Memo 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO), "Prediksi Pembuangan Troposfer berdasarkan GFS Model Data di... CSIRO Murchison Radio- astronomi," Observatory arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (FMI), "Efek resolusi model dalam memprediksi kondisi atmosfer dengan Harmonie- AROME," EGUsphere 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

Prakiraan radio amatir

- [W. R. Hepburn, "The Hepburn Tropo Indeks (HTI)"](https://www.dxinfocentre.com/propagation/hti.htm)dan [](https://www.dxinfocentre.com/propagation/history.htm)method history
- [W. R. Hepburn, "Frekuensi Terlemah Terrendah vs. Ketebalan Layer Ducting"](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, "predikat refraf seluruh dunia"](https://tropo.f5len.org/WW/info.php), [versi baru 2026](https://tropo.f5len.org/?p=504), [catatan pada model ARPEGE](https://tropo.f5len.org/author/f5len/)

Data

- [Open-Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWICON](https://open-meteo.com/en/docs/dwd-api), [catatan pada tingkat tekanan](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)
- [NOAA NCEI, Terpadu Global Radiosonde Archive (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RAOBget, script radio banjir untuk University of Wyoming](https://github.com/NCAR/RAOBget)

Teks klasik (yang ditunjukkan oleh penulis program, untuk berkonsultasi dengan rumus akhir)

- B. R. Bean, E. J. Dutton, * Radio Meteorology *, NBS Monograph 92, 1966, 435 halaman (cetak ulang Dover 1968): profil vertikal N dan M, gradien perangkap di bawah 157 N / km, dibandingkan dengan gelombang radio; Bab 5 "Sinoptic radio meteorologi". Dasar untuk 2- 4 langkah perhitungan per titik. Free: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [NIST card, doi 10.628 / NBS.MONO.92](https://www.nist.gov/node/1770911), [Internet Archive](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf)copy.
- L. Boithias, * Propagasi Radio Wave *, McGraw-Hill 1987 (diterjemahkan oleh D. Beeson dari Perancis, 330 halaman, ISBN 0070064334; juga North Oxford Akademik): Mekanisme di luar cakrawala, volume umum scatering dan kehilangan antena kopling-mezzo. Kertas saja ([kartu perpustakaan](https://libcat.library.tamu.edu/Record/in00000014812/Description)).
- M. P. Hall, * Efek dari Troposfer di Radio Communication *, IEE Electromagnetic Waves Series n. 8, Peter Peregrius 1979, 206 halaman (ISBN 096048257; brossura 0863410863): situasi sinoption dan penyebaran VHF / UHF. Dasar bahan sinoptik. Perdagangan di IET ([kartu](https://libcat.library.tamu.edu/Record/in00000636385)).
- H G. Booker, W. E. Gordon, "Teori Radio Scattering di Troposfer", * Proceedings dari IRE *, vol 38, n. 4, April 1950, pp.401- 412: Scapping teori dari ketidakpatuhan indeks refractive. Ide dasar: kekuatan difusi tergantung pada intensitas dan skala fluktuasi turbulensi dari indeks refractive (korelasi eksponensial); dekat cakrawala yang berlaku difraksi, penyebaran lebih lanjut; Menurut penulis skala turbulensi dan variasi indeks yang diharapkan oleh meteorologi cukup untuk menjelaskan bidang diukur. Cited oleh 328 artikel (Sclit), tidak ada salinan akses gratis. Kartu di Semantic Scholar ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); penuh teks pada pembayaran IEEE Xplore ([doi 10109 / JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)).
- C. M. Crain, J. R. Gerhardt (Electrical Engineering Research Laboratory, University of Texas), "Pengukuran Parameter Terdiri dalam Teori Radio Scattering di Troposfer", * Proceditings of the IRE *, VAL. 40, n 1, Januari 1952, p.50- 54, doi 10.119 / JRPROC.2739](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)
- "Konstanta dalam Equation for Atmospheric Refractive Index di Radio Frekuensi", N; itu adalah dari sini bahwa nilai-nilai 77,6 dan 3.73](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E. C. S. Megaw, "Teori penyebaran radio Fundamental tersebar", * Procedings dari IEE Part C: Monographs *, vol. 104, n. 6, September 1957, pp. 41- 455, doi 1049 / pi.1957: spektrum fluktuasi indeks dari teori turbulensi, lengkap tracing di luar cakrawala umum dan efek geometri yang dibayarkan di IET (Sil0)](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)
- * Procedings dari IRE *, Oktober 1955, angka khusus untuk penyebaran scatter (oleh K. A. Norton dan J. B. Wiesner), dengan Bullington, Staras dan lainnya: bebas di PDF pada [World Radio History](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf).
- D. E. Kerr (diedit), * Propagasi Radio Pendek Waves *, MIT Radiasi Laboratory Vol. 13, McGraw-Hill 1951 (Returpe Dover 1965 dan IEE 1987): teori penguapan dan saluran permukaan tanah di laut. Digital copy: [World Radio History](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); IET Edition in [IET Digital Library](https://shop.theiet.org/propag-short-radio-waves).

Langkah terbaru dari penyebaran troposferical di VHF: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Australia), "Sebuah Studi dari Saluran Komunikasi VHF Troposcatters di Australia Selatan", dalam * Digital signal Processing for Communication Systems *, Springer 1997, Pp3-112 ([Springer](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)). Teks untuk biaya; dari abstrak itu menyangkut difusi, bukan saluran. Hanya berguna jika Anda ingin mengkalibrasi istilah troposcater dengan data saluran VHF.

Model numerik dan rute laut (ditunjukkan oleh penulis program)

- M. Levy, * Metode Equasi Parabola untuk Propagasi Gelombang Elektromagnetik *, IEEElectromagnetic Waves Series # 45, 2000, 336 halaman (ISBN 082967640): persamaan parabola untuk profil refractive yang mengubah sepanjang jalan dan untuk orografi; bab pada penyebaran troposferous, sinar dan cara, penyebaran di laut. Ini akan berfungsi untuk perhitungan lapangan sepanjang jalan, di luar indeks. Untuk pembayaran, juga dalam bentuk elektronik dari IET ([kartu IET](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)).
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (University of Leicester): 2 GHz pengukuran di Kepulauan Channel dan Channel. Banyak yang bekerja dan tesis bebas dalam arsip universitas: [Statistik jangka panjang pada saluran penguapan, Radio Science 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434),](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710)](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854)](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus, "Aplikasi praktis dari model penguapan", Radio Science vol. 20 n. 4, 1985, p. 887 (bukan Transations IEEE pada Antennas dan Propagasi), doi 10.1029 / RS020i00400887 ([skema](https://zendy.io/title/10.1029/rs020i004p00887)): tinggi dari penguapan dari data cuaca permukaan. Bebaskan laporan teknis sebelumnya: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf).

T. Lindquist, * Model Propagasi Wave di Troposfer untuk Long Range UHF / SHF Radio Connections *, tesis master, Universitas Karlstad, 2 Juni 2020, 106 halaman (disediakan oleh penulis program). Ini adalah panduan langkah-demi-langkah, bebas, untuk menulis perhitungan dengan persamaan parabola: pemecahan-langkah solusi (bahkan pada sudut lebar), kondisi batas untuk darat dan laut, tanah ke langkah-langkah, balok pelacakan pada profil linear di kali, dengan pemeriksaan numerik; cite yang bebas alat PETOOL untuk MATLAB. Dua data yang berguna sekaligus: (1) N sensitivitas terhadap variabel cuaca, NARN 0.26 + 4.3 empere emper 1.4 (hPa dan K, sekitar 20 ° C), yaitu kesalahan 1 hPa pada tekanan uap: 5-6 m di Laut Utara: adalah kelembaban model yang harus diperiksa pertama; (2) Tinggi rata-rata dari sistem penguapan: 5-6 m di Laut Utara, -14 Mediterania, dan juga 40 Mediterania. Kesimpulan penulis (frekuensi 1 sampai 10 GHz): di luar cakrawala koneksi yang dapat diandalkan hampir selalu membutuhkan saluran.

Perangkat lunak propagasi pada profil tanah (untuk cakrawala dan hambatan, bukan untuk tropo yang diharapkan)

- [Radio Mobile](https://ve2dbe.com/rme.html)oleh Roger Coudé VE2DBE: free, Windows (also Linux and Mac with Wine), Longley- Rice / ITM model, profil medan secara otomatis diekstraksi dari data quota ([download](https://ve2dbe.com/download/download.html)).
- [SPLAT!](https://qsl.net/kd2bd/splat.html)oleh John Magliacane KD2BD: GPL, 20 MHz 20 GHz, Longley-Rice dan ITWOM v3.0 model, hambatan dan laporan kehilangan dalam teks; versi terbaru 1.4.2 dari 2014.
- [ITM, NTIA / ITS](https://its.ntia.gov/software/itm)kode resmi: Model Longley- Rice (20 MHz20 GHz) dengan sumber dan dokumentasi, basis Radio Mobile dan SPLAT! Kode ini gratis dan Anda bisa menambahkan perhitungan pada jalur LIVETROPOVHF (kerugian karena tanah, untuk ditambahkan ke indeks tropo). Laporan asli: A. G. Longley, P. L. Rice, "Prediksi Troposfir Radio Transmission Loss Over Irregular Terrain: A Computer Method - 1968," ESSA Technical Report ERL 79-ITS 67, Juli 1968 (NTIA card pada perangkat lunak sejarah: X1 https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITUR, perangkat lunak dan data digital](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): peta bebas dari MBN dan medians tahunan N0 pada grid 1.5 ° digunakan oleh P.452 dan P.617 - berguna sebagai referensi nilai iklim untuk indeks.
- Pathloss (Teknik Telekomunikasi Kontrak): program komersial untuk jembatan radio; saya belum menemukan sumber publik yang mengkonfirmasi rekomendasi ITU mana yang diterapkan, untuk diperiksa.

Gunakan Kode GitHub dalam LIVETROPOVHF

| Projek | Apa yang Anda lakukan | Bahasa | Gunakan kemungkinan bagi kita |
| [NTIA / ITM](https://github.com/NTIA/itm) | Model Longley- Rice resmi NTIA, dengan paket untuk. NET |  C++, C#  | kehilangan tanah di jalan: mengintegrasikan langsung ke program, yang di C # |
| [fuzzzeee / itm](https://github.com/fuzzzeee/itm) | ITM C + + code C # translation |  C#  | alternatif tanpa perpustakaan asli, juga berguna untuk Windows 7 versi |
| [eveetza / Py452](https://github.com/eeveetza/py452) | ITU- R P.452, identik dengan MATLAB / Octave versi yang disetujui oleh ITU- R WP 3M; MATLAB versi diterbitkan oleh Swiss Federal Communications Office | Python, MATLAB | referensi untuk diterjemahkan ke C # formula troposcatter dan dilakukan, dengan contoh validasi ITU |
| [bwinkel / pycraf](https://github.com/bwinkel/pycraf) | P.452 komplit, P.676, profil SRTM; GPL v3, tetapi berkas data ITU termasuk tidak bebas untuk penggunaan komersial | ♪ | kontrol dari hasil kami |
| [ITU- Rpy](https://github.com/inigodelportillo/ITU-Rpy) | banyak rekomendasi ITUR, termasuk P.453- 13, disahkan dengan contoh ITU | ♪ | verifikasi formula N dan peta iklim P.453 |
| [NCAR / RAOBget](https://github.com/NCAR/RAOBget) | unduh gelombang radio dari University of Wyoming dalam format teks | ♪ | diagram untuk diunduh di C # profil stasiun Italia |

Catatan: versi saat ini dari P.453 adalah [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), downloadable gratis; dalam dokumen yang saya gunakan 13, untuk direalisasikan.

Sudah dalam manual program dan dapat digunakan kembali di sini: LA0BY (Nordic VUSHF 2019), IK3XTV ("Propagation on 2 meters" dan "Propagation VHF / UHF DX Book Cap", Radio Magazine, DF5AI/DK5YA (2003), G3YGF (QST 1983), VHF / UHF DX DX, 2, G4MCU (2003) KP4MD
