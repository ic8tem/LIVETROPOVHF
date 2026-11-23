# Prognoosi käsiraamat

> Automaatne tõlge itaalia keeles: kahtluse korral tasub itaaliakeelset juhendit.

Kaardil Forecast arvutatakse troposfääri levik ise: indeksikaart nähtaval alal ja signaal, mis edastatakse teie jaama ja vastava vahel. Käsiraamat kirjeldab kõiki andmeid, kuidas neid arvutatakse ja millisest allikast need pärinevad. Nurksulgudes olevad numbrid viitavad allservas olevatele allikatele; programmis on igal tulemuste andmetel koomiks (simbolo i), millel on sama allikas ja viide lõikele.

## 1. Mida ta teeb ja mida ta teeb

GFS [1, 2] mudeli vertikaalsete temperatuuri- ja niiskusprofiilide ennustus tuletab õhukihi reaktsioonivõime kihtide kaupa ja hindab, kui palju signaali juurdekasv võrreldes normaalse atmosfääriga on. Marsruudile lisab see tegeliku maapinna (Copernicus GLO-30 [3]), difraktsiooni, tropohaja, kanalid ja vastuvõtja müra.

See ei ole meede: see on hinnang, mis on tehtud kaugete tasandite sadu meetreid. See aitab võrrelda olukordi (täna parem kui eile, 70 cm parem kui 2 m, see kohtleb paremini), mitte tagada ühendust. Paragrahvis 14 selgitatakse, kuidas koguda tõendeid nende kinnitamiseks.


> Paljunemist ei saa üldistada, vaid uurida seoses kohaliku morfoloogiaga. »
> — Volker Grassmann, DF5AI, esitanud Flavio Egano, IK3XTV (Propagation on 2 meetrit)

See on põhimõte, mis suunab punktiarvutust: iga marsruut arvutatakse tema pinnasel koos horisontide, mere ja reljeefidega; kaart annab üldise pildi, käsitleb vastust.

## 2. Keelekäsud

- Sagedusala: 144, 432 või 1296 MHz. Kaart ja arvutus kasutavad valitud riba.
- Nüüd: kursor liigub prognoosi nüüd kuni 72 tundi ette, 3 tunni jooksul.
- Ajakohastatud prognoos (nähtav ala): GFS-profiilide allalaadimine kuni 240-punktilisele võrgule, mis katab nähtava kaardi. Ruutvõrgu samm (0,5° või rohkem) sõltub suurendusest.
- Stopp: katkestage allalaadimine või arvutamine.
- Näidatavad üksikasjad (kaardi parem paneel): kolm taset, Essential, Normal ja Complete, lülitage kastide rühm sisse; iga kasti saab seejärel käsitsi muuta. Lahtrid jagunevad kolme rühma:
  - Kaardil: isolinee del Gain, samuti 3–7 dB (tracting), isobare, termiline inversioon, ilmamärgid S ↑ ↑, korrelatsiooni suurenemine/pressimine, otsese vaate tsoon (punktid 4, 5, 7 ja 9 bis). Värvid jäävad alati.
  - Signaali arvutamisel: kõik arvutusliinid, lame Maa profiil, arvutuspäevik ja üksikasjalik failipäevik.
  - Parandused ja reaalajas andmed: ilmaparandused (Misme, vajumine), õhu-mere kontrastsus, keskmine täppidega (corridoi), APRS 2 m hüpped ja LoRa, mäe taga ilma kadudeta (paragrahvid 4 bis, 9 bis, 9 quater ja 10 bis).
  - Essential hoiab ainult isoliinid, ilm ja meedia korrektsioonid täppidega; Normal lisab isobare, inversioon, ilmamärgid, otsevaade, lame maa, õhk-meri ja APRS; Complete lülitab selle sisse. Tase muudab ainult seda, mida näete ja milliseid parandusi sisestate: arvutused jäävad samaks.
- Andmete allikas ja ajalooline juhtum: kust profiilid tulevad ja millisel päeval te vaatate (paragrahvid 3 bis ja 3 ter).
- Minu jaam ja korrespondent: Lokaator, antennid, võimsus, kaablid, müra (lõiked 7 ja 12).
- Arvutage signaal ja katvus optikas: kaks arvutust maapinnal (lõiked 8–13).
- Arvutusregister: iga samm koos kasutatud numbritega, sealhulgas failis Data/ forecast-calcoli.log.

## 3. GFS andmetest profiilini

Iga ruudustiku punkti ja tunni kohta loeme maapinna rõhku, temperatuuri ja niiskust 2 m juures, tuult 10 m juures, piirkihi kõrgust, CAPE-d ning temperatuuri ja niiskust 1000, 975, 950, 925, 900, 850, 800, 700, 600 ja 500 hPa suhtes. Alates 1.3.0 võib lugeda ka tuult 850 hPa-ni ja vertikaalkiirust 850 ja 700 hPa-ni (lõige 9 bis).

Tasemed arvutatakse ipsomeetrilise valemi ja virtuaalse temperatuuriga; maapinnast madalamaid tasemeid on vähe. Iga taseme kohta:

- N = 77,6/T · (P + 4810·e/T), kus T on kelvinites, P-s ja hPa-s ning ITU-R P.453-14 küllastusrõhuga, nt 9, EF parandusteguriga (kontrollitud ametlikus tekstis) [4].
- M = N + 157·h, h km-des: modifitseeritud reaktsioonivõime. Seal, kus M üles läheb, on kanal.

## 3 bis. Andmeallikas

Menüüs Andmeallikas saab valida, kust profiilid pärinevad:

- Automaatne (soovitatav): GFS kõigepealt NOMADS [14] -st, mudelit levitavast NOAA-teenusest: iga hetk küsib programm G RIB2 -vormingus kahte väljalõiget piirkonnast ja loeb neid ise; NOMADSil ei ole päevapiirangut, ainult üks minutis, mida programm austab. Laia võrguga (0,5° kuni üle selle) kasutab see 0,5° või 1. faili iga 3 tunni järel, palju kergem. Punktid, mida NOMADS ei anna või kõik, kui NOMADS ei reageeri, küsige Open-Meteo [2]. Kaks allikat annavad sama mudeli ja sama võistluse, nii et saate segada samas võrgus.
- GFS Open-Meteo'st: ainult Open-Meteo, nagu punktis 1.2 (eeldatav on minuti piir).
- GFS NOAA NOMADS: ainult NOMADS. Aeglasem allalaadimine (kaks faili tunnis, pool sekundit ühe taotluse ja teise vahel, et mitte ületada NOAA piiri), kuid ei tarbi Open Meteo limiiti.
- ICON-EU (DWD) ja ECMWF IFS 0,25°, Open-Meteo: muud mudelid kui GFS, mida kohaldatakse kogu võrgus ja marsruutidel. Need ei segune GFSiga: kahe mudeli võrdlemine samal ajal on lihtne viis näha, kuidas prognoos on ebakindel. ICON-EU hõlmab ainult Euroopat; ECMWFil ei ole kõiki GFSi rõhutasemeid (puuduvate tasemete hüpe) ja ei pruugi olla piirkihti.

Kasutatav allikas kirjutatakse olekureale ja tulemuste esimesele reale (andmeallikas), kus on kirjas, mitu punkti igast teenusest tuli ja kui palju vahemälust. Ketta vahemälu on eraldatud mudeliga.

## 3 ter. Ajaloolised juhtumid

Ajalooline Juhtumimenüü vaatab üle dokumenteeritud avamise Open-Meteo [15] arhiivi GFS prognoosidega: kaart liigub korpuse piirkonda, bänd seab ennast ja ajakursor lahkub juhtumi esimesest päevast. Vajutades värskenduse prognoosi, laadite alla nende päevade profiilid; signaali arvutamine toimib nagu praeguse prognoosi puhul. See aitab võrrelda mudelit sellega, mis tegelikult juhtus.

Esimene juhtum on 14.–15. augustil 2022, mida räägiti QST-s (november 2022, "Maailm üle 50 MHz" [23]): ava kõrge rõhu all Manitoba ja Kansas/Missouri vahel, 144–1296 MHz peaaegu kõik FT8 -s, Kanada rekord 10 GHz 1228,56 km Q65 -s ja signaalid kuni 1941 km. Teine on avamine 29. detsembrist 2021 kuni 2. jaanuarini 2022 Türreeni ja Vahemere lääneosa 144 MHz juures, IC8TEM teadetest: esimesed avamised 29 (kuni 737 km), laiem 31 ( IT9GSF – EB5EA 1217 km), suurepärane avamine 1. jaanuaril Itaalia, Sitsiilia ja Hispaania vahel. Vanemate juhtumite puhul (nt september 2000) Open Meteo arhiivi andmed puuduvad. Arhiivis puudub piirkihi kõrgus: seal ei kehti selle parandus.

## 4. Indeks ja värvid

Iga kahe tasandi vahelise kihi puhul võrdleb programm N-langust Beani ja Dutton [5] tabelis 6.5 esitatud künnistega: näiteks vahemikus 1000–850 hPa on langus kuni 60 N/km normaalne, üle 100 N/km on ülirefraktsioon. Ülirefraktsioonkihi väärtus on 10–15 dB; kanal, mis püüab riba 20 kuni 25 dB, vastavalt M-i puudujäägile. Tugev tuul maapinnale, kõrge piirkiht ja kõrge CAPE vähendavad kasvu, sest nad eemaldavad või purustavad kihid [5, § 4.4].

Värv on hinnanguline võimendus dB-des, fikseeritud skaala on võrdne kõigis teemades:

| Võida | Värv | Tähendab |
| Alla 7 dB | Mitte keegi | peaaegu normaalne atmosfäär |
| 7–10 dB | sinine | normaalgradient |
| 10–13 dB | roheline | ülirefraktiivne kiht |
| 13–16 dB | kollane | tugev kiht, kuid ilma ribakanalita |
| 16–18,5 dB | oranž | väga tugev kiht või liiga õhuke torujuhe |
| 18,5–21 dB | punane | Kanali lõksujäämise bänd |
| 21 dB | Purpur | Tugev ja sügav kanal |

Ekraanikastiga on ka 3–7 dB tsoonid vahemikus 3–7 dB, kus midagi moodustub, kuid jääb skaala alla, näete helehalli piiranguga; värvid ei muutu. Punktidega paneel piiritleb enda prognoosiga hõlmatud ala ja näitab maksimaalset nähtavat võimendust: kui see on alla 7 dB, jääb kaart värvideta, sest atmosfäär on peaaegu normaalne, mitte sellepärast, et andmed puuduvad. Uue klõpsu allalaadimine värskenduse prognoosil ei katke; selle peatamiseks on seiskumine.

Värvid on läbipaistmatud kuni 82%, nii et teema taust muudab neid lihtsalt. Lokaatorite võrk ja ruutnimed on kujundatud üle värvide. Kui teie ennustus ei ole olemas, kuid Hepburn või F5LEN annavad 5 või enam malekaarti selle allika värviga.

Kaart hindab iga punkti üksi. Signaaliarvutus kontrollib hoopis seda, et kiht on marsruudil pidev (lõige 9): väga kollast läbiv tee võib anda vähem, kui kaart paneb mõtlema.

## 4 bis. Prognoos, mida vahendavad reklaamid: koridorid (alates 1.3.3)

Autoriõiguse poliitika: link üksi ei tohi kunagi kanalit tõmmata. Isoleeritud ja lühike vastuvõtt võib olla mööduva lennuki hajumine (APRS-pakett on väga lühike), mõne minuti avamine või viga ja annaks "drop-conduct", mida nad petavad. Programm tunneb ära koridori, kui samas sagedusalas:

- vähemalt 3 paari sõltumatuid jaamu (ilma ühisjaamata) töötavad vähemalt 300 km pikkustel marsruutidel;
- marsruudid on paralleelsed: asimuut ±15° piires, keskmised punktid 150 km kaugusel ühisteljest;
- kõik viimase 30 minuti jooksul, kui valgusvihk kordub vähemalt 10 minuti kaugusel (püsivus);
- ARS puhul loetakse hüpe ainult siis, kui sama hüpet korratakse;
- punktist 1.3.4 loetakse üle 1500 km pikkused marsruudid ainult juhul, kui kiire tuleb vähemalt kahest erinevast allikast (nt PSKReporter ja klaster): ühest allikast kuni nende kaugusteni 2 m on sagedamini EME, hajumeteoor või vale lokaator kui tropo.
- punktist 1.3.4 ei arvestata kiiritusjaamu: kellel on viimase tunni jooksul vähemalt 5 korrespondenti üle 1000 km, mis on jaotatud üle 150° ventilaatori. 2 m kaugusel on trope suunaline; vastuvõtjal, mis on koos Iisraeli, Portugali, Inglismaa ja Itaaliaga 2000–3700 km kaugusel "vaba", sageli positiivsete signaalidega, on peaaegu kindlasti sagedus halvasti konfigureeritud. 17–22/11/2026 I5MZY arhiiv 72% majutusest 2 m jooksul üle 1000 km tuli jaamadest, nii et 22/11 kell 10:53 UTC olid OH6HLH, OZ1ITS, F4LUK, OM8ABC ja SP2JLR ning filtriga kahaneb pikim koridor 2175 km-lt (regole 1.3.3) 615 km-ni.

Allikad: klastripunkt (mitte AS/ACS), PSKReporter MQTT kaudu, selle QSO ja WSJT-X/JTDX/MSHV dekodeerimised ning APRS hüpped 2 m ja LoRa 433/868 MHz.

APRS: 2 m ja LoRa. Programm loeb APRS-IS-i ainult kirjutuskaitstud kujul (rotate.aprs2.net, filter Euroopas) ja eraldab raadiohüppeid tuntud asukohajaamade vahel (sorgente → digipeater → IGate), näiteks DXView ja APRS PropView VHF-kaart (RF-YVY, Apache 2.0). Kolm sagedusriba on väärt: 2 m, LoRa 433 MHz (sageduskoridorid 432) ja LoRa 868 MHz (corridoi, mis loeb 1296: kanal, mis toob 868 MHz ka kõrgeimad sagedused). Bänd pärineb LoRa kõnest (aprs-deviceid list) ja kommentaari sagedustest või sõnadest; hüpe on väärt ainult sama bändi kahe jaama vahel.

Delikaatne punkt on aktiivsed repiiterid: 500 km kaugusel vastuvõetud signaali saab kahe või kolme digipeeter-taasedastusega. Hüpe loeb seega ainult siis, kui see asub kahe reaalse ja järjestikuse saatja vahel:

- rada peab olema jälgitav: iga uuesti edastatud digipeater on pannud oma nime ja hüpet mõõdetakse ühe ja teise vahel; ilma nimeta kasutatav varjunimi (W IDE1 *, W IDE2 *, piirkondlik varjunimi) murrab ahela ja see venitamine ei lähe arvesse;
- varjatud taasedastused: programm jätab meelde tee, mida iga allikas tavaliselt küsib (näiteks W IDE2 -2). Kui pakil on hüpe vähem (W IDE2 -1) ja ükski nimi seda ei seleta, edastas digipeater selle ilma jälgimiseta uuesti ja pakend visatakse ära; allikad, mida pole varem nähtud, ei lähe arvesse;
- Digipeater tee keskel: kui kahe jaama vahel, enam-vähem teel, on viimase kolme tunni jooksul sama aktiivse riba digipeater, siis hüpe jäetakse kõrvale, sest see oleks võinud uuesti edastada (see on eriti digipeater LoRa jaoks, mis edastab teed muutmata);
- raadioga mitteseotud pakette (qAC, qAX, TCPIP), neid, mida käitatakse interneti teel raadio teel IGate’ist (kolmandad isikud), HF-ist ja eri sagedustest, jaamadest, mis asuvad kõrgemal kui 1500 m (palloni, õhusõiduk) ja mis hüppavad üle 1500 km.

Mõju kaardile (alates 1.3.4). See tõmbab ainult ala, kus kehtib garanteeritud miinimum: ±60 km pikkune riba ümber iga marsruudi keskosa (15% kuni 85% selle pikkusest), mitte otstes asuvates jaamades, kus kanalit ei pruugi olla. Kõigi koridoride ribad on kokku joonistatud neutraalse värviga (tekst) ja poolläbipaistva värviga: kattumised ei klapi ja värv ei sega skaala lillaga (21 dB ja rohkem). Sildid (jooned, keskmine kaugus, minimaalne võimendus) skaneeritakse üksteisega; hiirega sildil ilmuvad koridori marsruudid.

Piirkonnas ja ainult praeguse aja lähedal (±90 minutit) ei lange prognoos miinimumist allapoole. Põhjendus on: kui jaamad tõesti läbivad, on levik olemas, olenemata ilmastikumudelist.

Sest lühikesed marsruudid on vähe väärt (alates 1.3.4). 2 m kaugusel, 300-600 km vahel, ühendavad kaks head jaama (umbes sada vatti, antennid, FT8 või CW) peaaegu iga päev isegi normaalse atmosfääriga, troposfääri hajumiseks: signaal, mida hajutavad väikesed õhu ebakorrapärasused kahe kimbu ühises mahus. Kolm-neli kohta 500 km juures ütlevad, et bänd töötab nagu tavaliselt, mitte et seal on avamine: GEKO Magazine n. 69 ( IU3QEZ) hinnangul on troposkatter 2×10 voolava elemendiga umbes 440 km SSB QRP-s, 510 km SSB-s 50 W-ga, 600 km CW QRP-s ja 640 km SSB-s 500 W-ga (allikad, üksteist ringi). Ainult üle 600–700 km tropohajumisest enam ei piisa ja rida paralleelseid ühendusi näitab tõepoolest kanalit või kihti. Kuni 1.3.3. aastani tõstis 450–600 km pikkune koridor prognoosi 9–12 dB-ni ja kaart näitas paranemist, mida seal ei olnud.

Lähteväärtus vastab seega marsruutide keskmisele vahemaale:

- kuni 400 km: 3 dB;
- 400–700 km: sool lineaarselt 3–10 dB (500 km → 5,3 dB; 600 km → 7,7 dB);
- 700 kuni 900 km: 10 kuni 16 dB; üle 900 km jääb 16 dB-ni.

7 dB all, mis on skaala alguses, jääb miinimumiks, et: koridor on joonistatud selle sildiga, on näha, et bänd töötab, kuid kaardi värvid ei muutu (kastiga näitab ka 3–7 dB halli punkti). Üle 7 dB kaalutakse väärtus, pool ja pool, sõltumatute marsruutide arvuga (3 lööki → 0,25; 6 või rohkem → 1) ja suhete keskmise S/N-iga (−20 dB → 0,5; 0 dB või rohkem → 1; ei ole teada, nagu klastripunktides, → 0,75), alustades 7 dB-st. Näited: 3 marsruuti ilma S/N-ita 500 km → 5,3 dB; 5 marsruuti 615 km → 7,7 dB; 3 marsruuti ilma S/N-ita 900 km → 11,5 dB; 3 marsruuti −20 dB-ni 900 km → 10,4 dB; 6 marsruuti 0 dB-ni 900 km → 16 dB.

Minimaalne kaugus on 300 km: selle tõstmine 500 km-ni oleks eemaldanud ka tegelikud lähiala avaused (näiteks kanal Türreenia 400 km kaugusel, mis on äratuntav ilmaprognoosi ja väga tugevate signaalide järgi rohkem kui ainult kaugusest). I5MZY arhiivis, 22/11/2026 kell 10:53 UTC, selle reegli ja kiirfiltriga olid pikemad koridorid 615 km (7,7 dB) ja 540 km (6,3 dB); reeglitega 1.3.3 jõudis see 2175 km ja 16 dB.

Kuni punktini 1.3.3 kehtis miinimum kogu koridori ristkülikul ja sõltus ainult kaugusest (10 dB kuni 300 km kuni 16 dB kuni 900 km): koos paljude pikkade koridoridega (näiteks 22/11/2026, kaksteist koridori kuni 2620 km) tõsteti peaaegu kogu Euroopa 16 dB-ni.

Kasumikõverad ei tõmba enam "gocce": 220 km väiksemad saared mõlemas suunas jäävad värviliseks, kuid ilma suletud kõverata, kui koridor neid ei kinnita. See lülitub välja "Prevision mediated by spots".

## 4 ter. Live sensorid: FM-DX veebiserver ja AIS vastuvõtja (alates 1.4)

Veel kaks reaalse vaatluse allikat, mis sisenevad 2 m vahemiku koridoridesse ja usaldusväärsuse hinnangusse (§ 10 septies). Nad lülitavad sisse ja välja vahekaardil Detailid.

Webserver FM-DX. Paljud kaugvastuvõtu entusiastid FM-is (88–108 MHz) hoiavad internetiühendusega vastuvõtjat FM-DX Webserveri tasuta programmis (GPL-3,0); Avalik nimekiri on servers.fmdx.org [28]. Iga server ütleb leheküljel /api häälestatud sagedust, signaali dBf-des ja RDS-i dekodeerimisel saatjat, mida tema andmebaas tunneb kauguse ja asimuutiga. Programm:

- laadige nimekiri alla iga 30 minuti järel ja hoidke 60 lähimat veebiserverit 1500 km kaugusel QTH-st;
- küsitleb neid ükshaaval, jagatuna 3 minutile, maksimaalselt 8 sekundit (ükski server ei saa iga 3 minuti järel rohkem kui ühe päringu);
- säilitab vastuvõtu, kui saatja on 150 km või rohkem ja signaal on vähemalt 10 dBf; saatja asukoht tuletatakse serveri asukohast, asimuutist ja kaugusest.

FM-sagedusala on madalam kui 2 m ja seda kandvad kanalid ei too alati 144 MHz; kuid FM-vastuvõtt 300–600 km kaugusel horisondist on peaaegu alati tropo (või suvel E sporaadiline, et 100 MHz juures ulatub üle 900 km: selle jaoks üle 1500 km nõuab koridoride reegel kahte erinevat allikat, § 4 bis). Kaardil on vastuvõtud lillad read "FM n km"; olekuribal on loend, koomiksis on nimekiri.

AIS teie vastuvõtjalt. Laevad edastavad oma asukoha 161,975 ja 162,025 MHz (ITU-R M.1371 [31]), peaaegu sama sagedusega kui 2 m. AIS-vastuvõtja (AIS-catcher, rtl-ais, spetsiaalne vastuvõtja), mis edastab fraase NMEA! AIVDM muudab UDP-s pordis 10110 iga laeva kanalianduriks: programm dekodeerib asukohateateid (tüübid 1, 2, 3, 4, 18, 19 ja 21, isegi mitmetel fragmentidel) ja võrdleb kaugust tavalise raadiohorisondiga.

```math
d_h = 4{,}12\,(\sqrt{h_{nave}} + \sqrt{h_{rx}})\ \text{km}
```

laeva antenni kõrgus liigi järgi (kaubalaevadel 20 m, reisijatel 30 m, kalapüügil ja lõbusõidul 6 m, teistel 10 m) ja vastuvõtuseadmete antenni kõrgus (10 m põhi). Üle 1,5-kordse horisondi ja üle 50-kilomeetrine laev on vastuvõtt kanali kaudu: see ilmub kaardil QTH sinise joonena möirgagaga «AIS n km». See on sama idee kui CSIRO [10] uuring, mis kasutas 162 MHz AIS-vastuvõttu, et kontrollida GFS-juhtmete prognoosimist. See on välja lülitatud elementaarne: see teenib õige vastuvõtja.

## 5. Valikulised jooned ning võrdlus rõhu ja inversiooniga

Kasukõverad: jooned 7, 10, 13, 16, 18,5 ja 21 dB juures, skaala servad, mis on joonistatud väljale marsiruutude meetodil, märgisega dB.

Isobare: rõhk merepinnal iga 4 kohta samast GFS-ist kuni prognoositunnini, tsentritega H ja L.

Termiline inversioon: kõverad, mis on kaetud sellega, kui palju õhku temperatuuril 925 või 850 hPa on soojem kui 2 m temperatuuril +1, +3, +5 ja +8 °C.

Juhtseadiste all annab joon võimenduskokkuleppe rõhu ja vastupidisega nähtavale alale: Pearsoni korrelatsioonikordaja, −1 kuni +1. Kanalid sünnivad sageli antitsüklonites (õhk, mis langeb soojust ja moodustab vastupidise), nii et positiivne kokkulepe rõhuga on normaalne; kuid sama antitsükloni sees muutub mere, ranniku ja mägede vahel palju. Ainult vastupidine ei ole piisav: painutage laineid ainult siis, kui õhu kohal on ka kuivati [5]. Liin aitab nüüd näha, milline kahest parimast selgitab värvilisi alasid. Samasugune võrdlus on kantud registrisse.

## 6. Võimalikud torujuhtmed: topeltarvutus

GFS tasemed jäävad vahemikku 200 kuni 1000 m: õhuke inversioon levib ja mudel ei näe kanalit. Kui kahe taseme vahel ületab N langus normaalse (40 N/km), õhk on stabiilne (termiline gradient alla 3 K/km) ja üle selle on kuiv (eriline niiskuse suhe alla 0,8), kaalub programm potentsiaalset kanalit ja arvutab selle kahel viisil:

- Arvutus A: kogu kontsentreeritud liiglangus 50 m.
- Arvutamine B: sama vähenemine, mis on jaotatud selle piirkonna torujuhtmete tüüpilisele paksusele, mis on võetud ITU-R P.453 [4] kliimakaartidelt (maapinnal või kõrgemal).

Puudujäägi ja maksimaalse lõksus oleva lainepikkuse puhul hoitakse madalaimat väärtust: see on usaldusväärne hinnang.

ITU-R P.453 andmed on © ITU ja neid saab kasutada isiklikel ja mitteärilistel eesmärkidel, viidates allikale, kuid neid ei saa edasi levitada: selleks ei ole ma programmipaketis. Arvutuse B sisselülitamiseks laadige vahekaardil lihtsalt alla ITU P.453 kaardid: pärast kasutustingimuste näitamist laadige ITU saidilt (elevated_and_surface_duct.zip) torujuhtmete arhiiv kasutaja isiklikuks kasutamiseks, eraldage failid Data/ P453 -s ja lülitage arvutus ilma taaskäivitamiseta sisse. Teise võimalusena laadige alla ITU-Ri uurimisrühma 3 digitaalse toote lehelt (itu.int, ITU-R uurimisrühm 3, Digitaalsed tooted, hääl P.453, pinnaandmed ja kõrgendatud kanali andmed) ja pange seitse S_OCCURi. TXT, S_THICK.TXT, S_STREN. TXT, E_OCCUR.TXT, E_base.T Ilma failideta kasutab programm ainult arvutust A ja kirjutab selle vahekaardile ja registrisse. Puudujäägi A ja tüüpilise intensiivsuse P.453 suhe on anomaalia: Mitu korda on tänane torujuhe tavalisest tugevam. P453

## 7. Jaamad ja lokaatorid

Üürileandjal võib olla 4, 6, 8 või 10 tähemärki. 8 või 10 tähemärgiga positsioon on täpne ja mullaosa pärineb Copernicuse GLO-30-st: antenni kõrgus on maapinnast piisav. 4 või 6 tähemärgiga ruut on liiga suur: parem on kirjutada antenni kvoot merel; kui 0 jääb programm hinnanguks, mille SRTM pinnas on ruutu keskel pluss kõrgus maapinnast ja annab tulemustes märku. Vastava puhul on vaikekõrgus 3 m maapinnast; mereosaga 0 kasutatakse SRTM mulda pluss kõrgust.

## 8. Maapind, difraktsioon ja troposfäärihajumine

Marsruudi profiilist võetakse proove Copernicuse GLO-30 iga 30 m tagant (laiem kui 120 km). Ekvivalentne Maa raadius on k = 157/(157 + ΔN), kusjuures ΔN on N vähenemine GFS-profiilidest esimese km jooksul piki [6, 7] -marsruuti.

Reljeefid jaamade lähedal (alates 1.3.2, autori kriteerium). P.452 maksab horisondinurga lineaarselt, 0,573 dB mrad kohta: mõnekümne meetri reljeef antennist mõne saja meetri kaugusel maksab kümneid dB, palju rohkem kui täheldatakse. Seetõttu võivad igast jaamast 20 km kaugusel asuvad reljeefid moodustada kuni 6 dB: neist tulenev horisondinurk on piiratud kõige kaugemate reljeefide omaga pluss 10,5 mrad. See kehtib teie jaama ja korrespondendi kohta. Rida "Lähedaste reljeefide" ütleb, mitu dB-d on startinud.

Maa päritolu (alates 1.3.2). Kvoodid pärinevad Copernicuse DEM GLO-30 [3] -st: 1 sekund kaar, meri on 0 ja rannik on puhas. Kuni 1.3.1. aastani kasutas programm AWS-i maastikuplaatide "skadi" kasti. Ranniku lähedal segavad need ruudud SSRTM-i madala resolutsiooniga merepõhja andmetega ja levitavad saare osa merel: Capri ümbruses luuakse 80–100 m suurune kummitusrõngas 0,5–1 km kaugusel kaldast. 21. novembril 2026 teatas IC8TEM loodes olematust horisondist; punkt-punkti võrdlus Copernicusega kinnitas seda (94 m merel Marina Grandest loodes vs 0). See valehorisont, +2,6°, maksis rannikujaamadele umbes 26 dB tropostri. Skadipaanid jäävad reserviks ainult siis, kui Copernicus ei reageeri. Copernicuse kaadrid kaaluvad 15–30 MB ja laadivad alla ühe korra; ainult mere kaadreid ei eksisteeri ja need on väärt 0.

- Vaba ruum: Lfs = 92,44 + 20 log f(GHz) + 20 log d(km) [8].
- Difraktsioon: Bullingtoni meetod tegelikul profiilil pluss ITU-R P.452-16 § 4.2 [6] delta-Bullingtoni sfääriline maaparandus koos ITU-R P.526 [9] sfäärilise kaoga.
- Fresneli esimene tsoon: raadius √(λ·d1·d2/d); vaba, kui maapind lahkub raadiusest vähemalt 0,6. Iga takistuse kohta esitatakse parameetrid [9] nugade laud V ja J(VV)..
- Profiili kujunduses on otsene raadius optiliselt roheline ja punane, kui see läbib maapinna all või maa kumeruse all; punased täpid on takistused, mis tungivad Fresneli 1. alale (lilla punktiirjoon). Merel ei ole "punane punkt" 0 m kõrgusel mägi: see on meri, mis tõuseb kõveruseks. Hiirega möödasõit raadiuses või punktis tundub selgituse ja väärtustega koomiks.
- Kui reljeef antenni lähedal sulgeb suuna (näiteks kümnete kraadide horisont), tekitab difraktsioon ja tropohajumine sadade dB-de kadu: üle 300 dB kirjutab programm "> 300 dB" ja "suund suletud naabertakistuse poolt", signaali "< −230 dB" ja S/N suhted "−60 dB". Sel juhul on soovitatav vaadata "Soovitatud taim" ja "Via monte".
- Troposkatter: ITU-R P.452-16 nt 45–50% [6] ajast, nurkkaugusega horisontidest ja N0 profiilidest.
- Ilma tropota: difraktsiooni- ja tropostrivõimsuse summa, mitte kunagi vaba ruumi all.

## 8 bis. Troposcatter: neli mudelit ja ebakindlus

P.452 kõrval arvutab programm troposkatteri kolme teise mudeliga, nagu ka TropoLink [16] programm, ja näitab nende erinevust ebakindlusena:

- NBS TN101 § 9 [25] (alates 1.3.5), mediaan: L = 30 log f − 20 log d + F(θd) − F0 + H0 + Lgp. F(θd) on III lisa sumbumisfunktsioon selle analüütilistes vormides (Ns 301), parandusega Ns, H0 § 9.2 sageduse juurdekasvu funktsioon: 144 MHz juures madalate antennide lainepikkustel kustutab maapinnalt peegeldumine osaliselt välja ühise ruumala alumises osas ja kaosoola 3–10 dB. Ns on GFS-i tunniprofiilide oma, efektiivsed horisondid ja kõrgused tulevad Copernicuse profiilist, Lgp = 0,07·e^(0,055(Gt+Gr)) on avamisühenduse kadu. See on sama vorm, mida ITM kasutab oma sisemuses, kuid tänapäeva reaktsioonivõimega kliima asemel. See testib lamedaid lõike 144 MHz juures antennidega 20 m kaugusel: 2–4 dB piires ITMist 150–900 km kaugusel ja umbes 2 dB lihtsustatud kujul 9,2b TN101 -st, millel puudub H0.

- ITU-R P.617-5 [17] L = F + 22 log f + 35 log θ + 17 log d + Lc, kus f on MHz, θ on mrad, d km, F = 0,18· N0 ·e^(−hs/7,35) − 0,23·ΔN ja Lc = 0,07·e^(0,055(Gt+Gr)). N0 ja ΔN on tavaliselt võetud ITU kliimakaartidelt; siin me kasutame iga tund profiile GFS ( N0 ·e^(−hs/7,35) on reaktiivsus maapinnale, ΔN vähenemine esimesel kilomeetril), nii et ka troposkander järgib aega.
- RSGB VHF/UHF käsiraamat valem, DL2FZN [18]: L = Lfs + 24,44 + 4,283 ln f + 0,06656 d. Kalle kaugusega on peaaegu võrdne PROPBERi tõusuga (0,067 dB/km).

Ühine ruumala (alates 1.3.1): on punkt, kus kahe jaama raadius, kumbki oma horisondinurga suhtes kaldu, ristub Maa ekvivalentraadiusega ae = k·6371 km. TX-ist tõuseb raadius kujul z = hTX + x·θt + x2/(2·ae); Kaugus x = (hRX − hTX + d^θr + d2/(2·ae) / θ, kus θ = d/ae + θt + θr. Lamedate horisontide ja sama kvoodiga antennide baasi väärtus on d2/(8·ae): umbes 1,3 km kuni 300 km, 3,7 km kuni 500 km, 9,4 km kuni 800 km. Mida suurem on maht, seda rohkem on õhk kuiv ja haruldasem ning seda vähem levib: sel põhjusel jookseb troposhajumine üle 800–1000 km. Tulemustes on baasi osakaal ja asukoht, GFSi reaktiivsus, gradient ja niiskus sellel kõrgusel ning Bray ekvivalentne kaugus (QST, november 1961): iga täiendav või väiksem horisont on väärt umbes 111 km kaugusel. Distsiplinaarviide: IU3QEZ (GEKO Magazine n. 69) ettekanne "Beyond the horizon in VHF".

Troposcatteri rida, teised mudelid näitavad kahte kaotust ja ebakindlust poole võrra kõige optimistlikuma ja kõige pessimistlikuma mudeli vahel. Need on vaid võrdlused: kaotus ilma tropota jääb P.452 kaotuseks.

## 8 ter. pärastlõunane troposhajumine üle reljeefide (alates 1.3.4)

IK3XTV, mis pärineb Thiene'ist ( JN55, Alpidega 10 km kaugusel), töötab suve pärastlõunal Lõuna-Saksamaal 400–500 km kaugusel, ilma nägemiseta, tugevate signaalidega, kuid kiire ja sügava vananemisega; autor omistas need "konvektiivsetele rakkudele, mis on joondatud läätsede rongina". Tähelepanek on kinnitatud; programmis vastuvõetud selgitus on teine, füüsiliselt kindlam.

Pärastlõunal segab päike õhku üle reljeefide ja piirkiht tõuseb 2–3 km-ni. Selle ülaosas kohtub all olev niiske õhk ülal asuva kuiva õhuga: seal on murdumisnäitaja turbulents (suurus, mis reguleerib troposfääri) maksimaalne. Kui see tipp jõuab liini ühise mahuni, mis 400–500 km peal on umbes 2–3 km, tugevdatakse tropohajumist. See selgitab pärastlõunast aega, kõrget rõhku (vaikne taevas, tugev kuumenemine) ja kiiret vananemist, mis on tüüpiline difusioonile, mitte kanalitele. Konvektiivsed rakud ei toimi läätsedena: tekitavad turbulentsi ja see on turbulents uuesti sõnastamise kihi ülaosas, mis levib signaali.

Prognooside lugemise tagajärjed:

- Maal on esimene pärastlõuna madalate kanalite ja kihtide jaoks halvim aeg, kuid mitte tropostri jaoks mägede taga: neid kahte asja tuleks lugeda eraldi;
- programmil on juba olemas kaks teenindavat teavet, mudeli poolt pakutava piirkihi kõrgus ja marsruudi ühise mahu osakaal (registreerige "Ühine maht" tulemustes): kui nad on lähedal, võib pärastlõunane mägine maiuspala minna paremini kui arvutus normaalses atmosfääris;
- punktist 1.3.4 võetakse punktiarvutuses arvesse: kui ühine ruumala on maa peal, siis marsruudil on reljeefid vähemalt 800 m, päike on üle 15° ja piirkiht üle 800 m ning kihi ümberkujunemise ülemine osa (mudelmulla kiirus + piirkihi kõrgus) jääb 500 m piiresse ühise ruumala alusest, troposkander suureneb 3 dB; vahemikus 500–1000 m langeb lineaarne võimendus nullini. Rida „Troposcatter pomeridiano. Lumi mööda marsruuti (mudeli järgi vähemalt 10 cm maapinnal, Open-Meteo lumesügavuse muutuja või NOMADSi GFS-i SNOD) tugevdus ei kehti: lumel jääb pinnas külmaks ja ümberkujundatud kiht ei kasva. Väärtus 3 dB on ajutine ja seda tuleb kalibreerida reaalsete linkidega; see lülitub välja ilmakorrektsioonidega.

Allikad: F. Egano IK3XTV (Radio Rivista e www.qsl.net/ik3xtv, „Propagazione sulle 2 metri, „Celle convettive, „Troposcatter over the Alps) tähelepanekud, allikad § 13.

## 9. Asutamiskiht mööda teed

See on signaali arvutamise tuum. GFS-profiilid võetakse marsruudil iga 25 km järel. Iga rõhukihi puhul leitakse sama kihi pikim venitamine järjestikustest punktidest; kanalina loetakse ainult paksud kihid vähemalt 40 m [10].

- pidevus: mis kilomeetrist millise kilomeetrini ja marsruudi osa;
- ülemine ja alumine osa ning kalle (m/km);
- kihi sees olev pinnas: km, mil mäed tõusevad alusest kõrgemale;
- Kaetud maa: merel on kanal peaaegu alati, otsustab maa pindala [10]. Kui maa ületab 10% marsruudist, on kasutatav murdosa minimaalne pidevuse ja maapinna katvuse vahel.

Trope võimendus on kasutatud fraktsiooni kihi võimendus. Kui üle poole profiilidest on murdumispiirist madalamad, on võimendus negatiivne.

## 9 bis. Ilmaparandused: stabiilsus, vajumine, streik

Alates 1.3.0 annab profiil ka kolm meteoroloogilist teavet, mida saab näha registris (regave «stabiilsus (Misme)»), Meteo joonel tulemuste marsruudil ja Segni meteo kastiga kaardil.

- Misme’i stabiilsus [5, 6. peatükk]. Misme seob välja horisondi taga ühendparameetriga: M = a·(ge − 40 + b·ΔW), a = 0,5 dB N/km kohta, ekvivalentgradient ge ja termiline stabiilsus ΔW, töö õhu tõstmiseks 1 km. Programm arvutab ΔW = g·∫(θ − θ0)/θ0 dz pinnase ja 1 km kõrguse vahel, θ potentsiaalse temperatuuriga; standardne atmosfäär on umbes 56 J/kg. Kasvule lisatav termin on 0,07 dB J/kg standardist kõrgemal, piiratud −4 ja +6 dB vahel ning loeb ainult siis, kui ükski kanal ei jää riba vahele (gradient on juba Beani ja Duttoni indeksis). Kalibreerimine on ajutine: seda tuleb kontrollida suhtarvudega (punkt 14).
- Vajumine ja ülesmäge õhk [19]. Moler ja Holden leidsid 125–305 km marsruutidel merele 1365 ja 9380 MHz, kõrgeima signaali keskmiselt umbes 2,5 dB, kui õhk langeb alla, kui õhk tõuseb, ja juhul tugevnemine umbes 20 dB 3 tunni jooksul vajumise alguses, millele järgnes 22 dB vähenemine, kui õhk pöördus tagasi koonduma. GFS annab vertikaalkiiruseks 850 ja 700 hPa. Kui mõlemal tasandil langeb õhk rohkem kui 1 cm/s ja potentsiaalne murdumisnäitaja K pöördub või langeb järsult (N registreeritakse adiabaatiliselt 1000 hPa-ni, mis toob esile ülemisest [5] -st langenud kuiva ja sooja õhu), lisatakse 1,5 dB; kui õhk tõuseb õhku tõustes rohkem kui 5 cm/s. 2.
- [20] Scirocco. Kuum ja kuiv õhk Saharast üle jahedama mereõhu annab väga tugeva inversiooni Vahemere idaosast Gibraltarini. S-märk ilmub, kus 850 hPa juures tuleb tuul 100–200° juures vähemalt 7 m/s ja temperatuur on vähemalt 16 °C, Vahemerel. See on lihtsalt viide: kasu otsustab profiili kihid.

Ilma riba püüdva kanalita ei ületa kogu võimendus 16 dB, kollase lõppu: parandused muudavad tugevamad kihid ilmsemaks, kuid ei läbi neid kanalite kaudu (punane ja lilla jäävad kanalitesse).

- Püsivus (alates 1.3.1). Suurimad avaused tulevad pärast häid kõrgsurvepäevi (VHF/UHF DX Book; Biancomano ja Leyshon 1998, reegel 12 tundi; aruanded 29/12/2021–2/1/2022 kasvavad esimesest kuni neljanda päevani). Iga lahtri puhul loeb programm eelneva 24 tunnid võimendusega vähemalt 13 dB ja lisab proportsionaalselt kuni 2 dB, ainult siis, kui võimendus on juba vähemalt 10 dB.

Kuidas lugeda märke kaardil (Muuda ilmamärke; alates 1.3.2 on sümbol suur kahekordne ja möödub hiirega üle märgi ilmub koomiks lahtri selgituse ja väärtustega):

| Signaal | Värv | Mida see tähendab? | Mõju tropeedile |
|---|---|---|---|
|  ↓  | roheline | vajumine: õhutilgad (antitsüklon), kuumutab kokkusurumiseks ja kuivaks, tugevdab vastupidist ja hoiab seda kindlalt | soodne, +1,5 dB |
|  ↑  | punane | ülesmäge minev õhk (eesmine, madal rõhk, konvektsioon): segab madalaid kihte ning katkestab inversioonid ja kanalid | ebasoodsas suunas, −2 dB |
|  S  | oranž | Siirup: kuum ja kuiv õhk Saharast värske mereõhu kohal | Positiivne vihje, korrektsioon puudub: otsusta kihid |

Antitsükloni all olev roheline ↓ on klassikaline kõva avause märk; Punased ↑ nooled piki joont ütlevad, et kihid murduvad, kuigi raku värv on endiselt hea.

Ilmaparanduste kast lülitab need parandused välja, et näha mudelit 1.2.

Valideerimine (punkt 14): 593 ühendusel 144 MHz-ga ajavahemikul 29. detsembrist 2021 kuni 2. jaanuarini 2022, võrreldes samade marsruutidega ja kaks nädalat hiljem, on eraldusindeks (0,5 = puudub, 1 = täiuslik) 0,79 ilma parandusteta ja 0,91 koos parandustega (enne katust 16 dB juures). Sama võrdlus näitab peamist piiri: mere kanal madalal kõrgusel, mis tol ajal tõi märke Campania ja Liguuria vahel, ei ole GFS-i tasemel nähtav.

## 9 ter. Uudis 1.3.2: kaks hinnangut, lagi, kõrgrõhkkond, meri, jaamad, mäeharjad

ettevaatlik ja agressiivne hinnang. Gradiendist saadav kasu kasvab 0 dB-lt normipiirini kuni 10 dB-ni superrefraktsiooni läveni. 1000–850 kihis on ettevaatlik lävi Bean ja Dutton (100 N/km); agressiivne lävi on klassikaline superrefraktsiooni lävi 79 N/km (k ≈ 2; P.453 kasutab −100 N/km esimese 100 m jooksul), mis k50 = 157/(157 − ΔN) puhul on k ≈ 2 (k = 2,5 vastab umbes 94 N/km). Punktarvutus näitab kahte kadu külgnevat; kanaliga, mis tõmbab riba kokku. Kaart kasutab ettevaatust.

2500 m pööning. Maanteid ja kanaleid otsitakse ainult kuni 2500 m kõrgusel maapinnast: mida kõrgemal amatöörraadiote antennid neid ei kasuta. 1500 m valideerimisel kadus eralduspunkt ja 4% ühendustest üle 16 dB, sest talvel jäävad vajumisest tulenevad inversioonid sageli 1500 ja 2500 m vahele.

Kõrgrõhk laienes. +1 dB, kui 9st ümberkaudsest küvetist vähemalt 7 on vähendanud mererõhku 1020 hPa või rohkem ja tuul on nõrk madalates kihtides (10 m alla 5 m/s, 850 hPa alla 8 m/s), välja arvatud tõusvate liikumiste korral.

Merekaal. Punktiarvutuses korrutatakse kihtide võimendus 0,8 + 0,4·μ1, kus μ1 on ITU-R P.452 tegur (eq 51) pikimal katkematul maa-alal: 1 avamerel, väike pikkade maa-aladega.

Jaamad ja dirigeeritud. Kui kanal, mis haarab riba, katab ainult osa liinist, võrreldakse iga antenni osakaalu kanali aluse ja ülemise osaga kaetud lõigu alguses ja lõpus; võimendus korrutatakse 0,5 + 0,5·√(e^(−ΔhTX/300)·e^(−ΔhRX/300)), kusjuures Δh on kaugus meetrites kanalist välja. Juhtmega peaaegu kogu marsruudil on § 10 ühendamine.

Kihid ja vallid. Kui GFSi piirkihi ülemine osa (kõige boundary_layer_height mudeli maapealne määr) ületab marsruudi harusid (maksimaalsed ruumid üle 300 m, kui kõrgeim ületab 500 m), läheb kiht üle mägede purunemata: +1,5 dB kaetud harjade murde korral. See on IT9GSF vaatlus avaustel Sitsiiliast Saksamaale soodsa türreeniaga.

Tasemed maapinna lähedal merel. NOMADS-i allikaga laadib programm alla ka pinnatemperatuuri (merel on SST), temperatuuri ja niiskuse 80 m ja kihis 30–0 hPa üle maapinna. Merepunktides (mudeli kiirus alla 5 m) lisatakse need tasemed profiilile esimese rõhutaseme all, et näha kanalit 100–200 m kõrgusel, mida rõhutasemed ei lahenda. Maal ei kasutata: öö- ja päevakihid ei kehti veel. Avatud meteo annab ainult pinna temperatuuri.

Aurustamistorustik. Alates SST-st, temperatuurist ja niiskusest kuni 2 meetrini, hinnatakse aurumiskanali δ kõrgust neutraalse logaritmilise profiiliga (nagu Pauluse ja Jeske mudelis, ilma stabiilsust korrigeerimata): õhk vee karvadesse küllastub SST-ga (soolal 98%).

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

piiratud 0–40 m. 144 MHz püüdmiseks kuluks umbes 180 m (Kerr), siis 144 MHz juures tugevdaks aurustamiskanal vähe; 432 MHz-st ülespoole, madalate antennidega merel, palju (punkt 11 bis). Sisestage paraboolvõrrand ja rida Parandused 1.3.2.

Päeva kellaaeg (külastus). Programm arvutab päikese kõrguse igas punktis. Maa peal, kus päike on üle 15° ja piirkiht üle 800 m või tuul üle 5 m/s, hoiatab päevast päeva, et maapinna kanalid on ümber kujundatud; öösel, kui päike on alla −3°, tuul alla 3 m/s ja piirkiht alla 300 m, hoiatab, et rahuliku taevaga on võimalik läbi viia kiiritamine, mis langeb pärast päikesetõusu. Mitte merel. Praegu on need täpilised, mitte numbrid: detsembri valideerimisel ei ole piisavalt suvepäevi nende kalibreerimiseks. Need on kaks punkti, mida autor peab oluliseks igapäevasest vaatlusest.

Normaalne profiil CRPL (alternatiivne). Fikseeritud 40 N/km asemel järgneb normaallangus eksponentsiaalsele võrdlusatmosfäärile CRPL: N(h) = Ns·e^(−c·h), kusjuures −ΔN esimesel km = 7,32·e^(0,005577·Ns) (Bean and Thayer 1959). Niiske õhuga (Ns kõrge) on normaalne järsem ja teenib tugevamat gradienti, et anda kasu; kõrgusel on magusam. See on halb, kuni valideerimine ütleb, et see aitab.

## 9 quater. Kontrastne õhk-meri ja madalmeri: Aadria meri (alates 1.3.3)

Raadiolained ei tunne merepõhja: tunnevad õhu ja veepinna kontrasti. Sügavus loeb, sest see otsustab, kui kiiresti mere temperatuur järgneb õhu temperatuurile.

Aadria meres on madal meri. Keskmine sügavus on umbes 250 m, kuid põhjabassein ületab harva 100 m ja Trieste laht jääb alla 25 m; ainult lõunasse laskub üle 1200 m. Nii õhuke veekiht soojeneb ja jahtub mõne päevaga, türanlane nädalatega. On kolm tüüpilist olukorda:

1. Kevad ja suve algus, soodne. Meri on ikka veel külm, isegi Alpi ja Po jõgede magevee jaoks; ta jõuab tasandikult või Balkanilt kuuma ja kuiva õhku. Kuum õhk külma vee kohal moodustab merel ümberpööramise (advection pipeline): on Itaalia, Sloveenia ja Horvaatia vaheliste avauste klassikaline seisund.
2. Sügis ja talv boraga, ebasoodne. Külm õhk soojema mere kohal: madal kiht muutub ebastabiilseks, pressib ja kanal puruneb.
3. Pärast bora, võimalus. Lihtsalt sellepärast, et meri on madal, jahutab bora seda mõne päeva jooksul mõne kraadi võrra. Kui sisenete seejärel kuuma õhuga antitsüklonisse, on õhu-mere kontrast tugev ja kanal on tavalisest tugevam.

Aadria meri on ka suletud kanal Apenniinide ja Dinari vahel, vähem kui 200 km laiune: merekiht jääb piiratuks ja marsruudid piki telge (Puglia–Venezia–Trieste, umbes 700 km) jäävad kõik merele, mis on parim tingimus pika kanali jaoks.

Mida programm arvutab. Merepunktides, ilmakorrektsioonidega ja lahtriga «Contrasto aria-mare»:

| Seisund | Parandus |
| --- | --- |
| potentsiaalne temperatuur 925 hPa (või 950) SST 4 K juures | _ _BAR_ |
| üle 7 K SST | +2 dB |
| õhuniiskusega 925 hPa (üle 80%) juures | −0,5 dB nende boonuste pealt |
| jahutatud meri 1,5 °C või rohkem 72 tunni jooksul, soodsa kontrastiga | _ _BAR_ |
| soojem õhumeri 2 m 2 °C juures (segamine) | −1,5 dB |
| soojem merevesi 4 °C | −3 dB |

Kogukorrektsioon jääb vahemikku −3 kuni +3 dB ja ilma kanalita jääb võimendus kollase katuse alla. Künnised on ajutised: standardatmosfääris ületab potentsiaalne temperatuur 925 hPa umbes 2–3 K mere oma, siis 4 K näitab juba vastupidist.

SST mälu. Mudelid hoiavad meretemperatuuri ennustamise ajal peaaegu stabiilsena. Hiljutine vähenemine on tuletatud kolme eelneva päeva analüüsist (Open-Meteo, ainult pinnatemperatuur, ainult mererakud): maksimaalselt 72 tundi enne praeguse väärtuse mahaarvamist. Kui teenus ei reageeri, ei kehti mälu boonus.

Piirangud. Mudelite SST allpool rannikut ja Aadria mere ülaosas on jäme; näiteks Po külm pliiats on halvasti lahendatud. Batimetry arvutusse ei lähe: allikad annavad alati SST, mis võtab juba kokku merepõhja mõju. Õhu ja mere kontrastsus kattub osaliselt Misme stabiilsuse lõpus (§ 9 bis); selle puhul on boonused väikesed. Künnised lähevad kokku kohakoridoridega ja QSO-ga läbi Aadria mere.

## 10. Läbi viidud kogu marsruudil

Kui kiht püüab riba kinni vähemalt 80% kasutatud fraktsioonist ja jaamad ei ole nähtavad, arvutatakse ka kanali kadu: vaba ruum, pluss antennide ühendus (3 dB, kui need on kanali sees, rohkem, kui need on üleval- või allpool), pluss jääksumbuvus γd = 5·10−5·ae·f^(1/3) dB/mrad ITU-R P.452 eq. 49 [6]. Kui see on väiksem kui kaotus ilma tropota, muutub see oodatavaks kaotuseks.

Riba jääb kanalisse ainult siis, kui selle lainepikkus ei ületa λmax = 2√2·(2/3)·√(ΔM·10−6)·d (Kerr, eq. 17 [11]). Selleks võib sama kiht püüda 23 cm ja 70 cm, kuid mitte 2 m. Piir ei ole siiski võrgus: Kerr hoiatab, et isegi lained mitu korda pikemad kannatavad palju kanalit ning Hepburn ja VHF / UHF DX Book räägivad "sfäärilistest" servadest ja "andestatud" kanalitest. Alates punktist 1.3.1 kasutab programm pehmet kanalit: kui λmax jääb λmax ja 3·λmax vahele, läheb võimendus järk-järgult 15 dB-lt (λ = 3·λmax) üle täieliku kanali võimenduseni (λ = λmax), kusjuures tegur s = (λmax/λ − 1/3)/(2/3). Sellisel juhul on lubatud kanali värvid. Palk ja Meteo liin marsruudil näitavad pehme kanaliga punkte.

## 10 bis. Monte

Kui korrespondent on horisondi taga suletud, otsib programm antennide suunamiseks parimat suunda: valgustatud kalle levitab signaali radari sihtmärgina. See teeb horisondi ringi iga 5. koha järel (välja arvatud ±15° ümber korrespondendi) ja võtab igas suunas valgustatud horisondipunkti, üle 100 m ja vahemikus 1–60 km. Kiire hinnang (vaba ruum, bitaktiline muundamine ja 10 dB iga horisondi taseme kohta difuusorist korrespondendini) valib 4 kõige paljutõotavamat, vähemalt 15° üksteisest; igaühe jaoks:

- ravi 1, jaam → reljeef vabas ruumis;
- käsitleb 2, reljeef → korrespondente, arvutatuna nii, nagu oleks reljeef üle 10 m ülalt, sama ennustusega (ilma paraboolvõrrandita);
- teisendamine bistaatilise radari võrrandiga, kus σ = σ0·A, σ0 = −15 dB (mägi VHF/UHF) ja A R1 -kiirega valgustatud ala (maksimaalselt 20 km2):

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

"Soovitatud punkt" rida näitab parima marsruudi asimuuti, kui see maksab vähem kui otsene. 144 MHz, reljeefiga 20 km ja yagi alates 15°, maksab mägine tee umbes 45 dB rohkem kui ainult 2 marsruut: see juhtub ainult siis, kui otse on suletud naabertakistustega. Hinnanguline väärtus on bruto (±10 dB), eriti σ0 puhul.


Ilma kaotuseta mäele (valik, alates 1.3.2). Kastiga "Via monte senza perdite up al monte" peetakse jaama ja difraktsioonipunkti vahelist kukkumist nulliks: see on nagu jaama ülesviimine (antenn 10 m kõrgusele punktist, sama võimsus, antenn ja kaabel). Seejärel valib horisondiring punkti, millel on korrespondendi suunas kõige vabam horisont (pariteet, kõrgeim) ja tulemus näitab marsruuti punktist korrespondendini koos signaali ja S/N-iga, difusiooniga kogusumma kõrval. See on optimistlik piir: ütleb, kui suund avaneb, kui mägi tõstab signaali hästi või kuhu minna kaasaskantavana. Iga punkt on antud kümne märgiga.

## 10 ter. Sissepääs kaugesse kanalisse

Kui kanal, mis lindi kinni püüab, algab kaugel, väljaspool jaama horisonti, peab signaal jõudma. Programm toob signaali lähtepunkti, kanali keskele, difraktsiooni jaoks: mööda Copernicuse profiili GLO-30 otsib kõrgeima Fresneli parameetriga serva (ITU-R P.526) koos samaväärse maa paisumisega,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

rohkem ülaosa kumerusest tingitud sumbumist (ümar takistus, P.526-16, § 4.2), kusjuures R-raadiust hinnatakse Copernicuse GLO-30 pinnasest kuni ±1 km kaugusele servast;

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

(kus m·n > 4 on soovituse vorm 34b). Barsis (NBS, 1963) mõõdetuna kolmel pikal mäetagusel marsruudil (Colorado, Alaska, Euroopa) on keskmine kadu 10–20 dB kõrgem kui Fresnel-Kirchhoffi ideaalne serv: ümardamine selgitab palju (Barsis ja Kirby, 1961: Pikes Peak raadiusega umbes 11,5 km). Tulemus on võrreldav difraktsiooniga siledal sfäärilisel pinnasel; see on halvim. Seejärel liigub signaal kanalis: vaba ruum kogu marsruudil, P.452 jääksumbuvus γd kanali lõigul ja korrespondendi ühendus (3 dB sees, 1 dB iga 10 m väljas). Kui see kaotus on oodatust väiksem, muutub see mehhanismiks ("käitumine (sisend difraktsiooni teel)"). See on autori eeldus Itaalia geograafia kohta: lähedal asuva takistusega joonest tunduvalt allpool on v negatiivne ja sissepääs maksab vähe; seljaosaga maksab 15–30 dB.

## 10 quater. ITM (Longley-Rice) ja väljumine kanalist

Alates 1.3.2 sisaldab programm NTIA/ITS-i (Longley ja Rice, ESSA ERL 79-ITS 67, 1968; Hufford, Longley ja Kissick, NTIA 82-100, 1982) ebaregulaarset maastikumudelit 1.2.2, mis on tõlgitud C #-ks USA valitsuse ametliku koodiga, mis on õigustest vaba. Tõlge annab samad numbrid kui originaal: automaatne kompilatsioon võrdleb seda iga kord NTIA C++ koodiga viiel katsejuhul (alla 0,01 dB).

ITM arvutab välja normaalse atmosfääri kao tegelikul profiilil Copernicus GLO-30: vaadeldakse maapinnalt peegeldudes, difraktsioon (spigol ja kerakujuline maa, mis on kaalutud mulla kareduse Δh suhtes, st erinevus kvootide 10. ja 90° protsentiili vahel minimaalsete ruutude sirge ümber) ja troposkander. Maa reageerimisvõimet vähendatakse marsruudi keskmise osani $N_s = N_0\,e^{-h/9460}$ ja see annab samaväärse maa kõveruse;

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

Programmis kasutatakse valitud marsruudi raadiokliimat nagu paragrahvis 10 sexies (kuni 1.3.4 alati parasvöötme meri, N0 320 maapinnal ja 350 merel), N0 koha keskmisest reaktsioonivõimest (pinnas εr 15, σ 0,005 S/m; meri εr 81, σ 5 S/m), horisontaalne polarisatsioon ja varieeruvus ajas ilma kohata (profiil on teada). See näitab, et kaotus ületab 50% ajast ja see, mis teil on 10% ja 1% parimast ajast. ITM ei modelleeri kanaleid: selle varieeruvus ajas on statistiline, kliima järgi. See on teine baastaseme arvutus P.452 kõrval; nende kahe erinevus annab aimu ebakindlusest.

Teine kasutus on viimane venitamine pärast kanalit, mis lõpeb enne vastavat. Väljumispunktis paigutatakse virtuaalne antenn kanali poolele teele ja ITM arvutab välja trakti kuni vastavani, ületades vaba ruumi 50% ajast. Kogukahju

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,\theta_{condotto} + A_{ITM}(d_{uscita} \to RX)
```

kus A_TX on antenni ühenduskoht (3 dB sees, 1 dB iga 10 m väljas), kui kanal algab lähedalt, vastasel juhul § 10 ter sisenev difraktsioon. See on piir, mille autor on endale andnud: kihi järjepidevus pluss maksimaalselt jääksumbumine. Kui see on väiksem kui eeldatav kahjum, muutub see mehhanismiks ("käivitus (väljumine ja lõplik trakt ITM)").

## 10 kvinquies. Süvenemine: saared, väinad ja maa-mere tegevuse katkemine

Näide Napoli lahest (Anacapri, JN70CN) Vahemere lääneosa suunas näitab, kus marsruudid ületavad saari:

| Sihtkoht | Azimut | Seal, kus ta ületab saared | Takistuse |
| --- | --- | --- | --- |
| Barcelona |  279°  | Sardiiniast (Gallura) põhja pool kuni 41,05° N, 420–490 km | umbes 30 km Bocche di Bonifaciost lõunas; Limbara (1359 m) umbes 20 km kaugusel joonest |
| Bocche di Bonifacio |  282°  | 41,3° N lähedal | 11 km |
| Marseille |  296°  | Korsika keskosa 42,2° N | kuni 2700 m kõrgused mäed: kanal alla 2 km seal katkestab |
| Valencia, Baleaarid |  260–265°  | Kesk-Sardiinia | sisereljeefid |

Barcelona puhul piisab 279° asemel 282°-st, sest telg möödub suudmest: 3., tavalise yagi tala sees.

Kas suud on väin? Madalate kanalite puhul, mis on mõnesaja meetri kaugusel, jah: saared on seinaga ja maa peal, päeval katkestab küte inversiooni. Väin ise on piisavalt lai: umbes 11 km, Fresneli esimese ala vastu, vähem kui ühe kilomeetri laiune 1000 km marsruudi keskel 144 MHz. Probleem on ilmas. Suud on tuule jaoks lõbus: Alumine kiht kiirendatakse ja alumine kiht eemaldatakse. Sageli on läbipääs avatud geomeetriana, kuid häiritud kui atmosfäär. Püsiva antitsükloni ja vähese tuulega võib selle asemel olla hea läbipääs.

Maa ja mere vaheline ebavõrdsus. Nad loevad. Türreeni rannikul moodustunud kanal katkeb: signaal väljub kanalist (lõige pärast väljumist, § 10 quater) ja peab naasma teise kanalisse Baleaaride merel, makstes uuesti sideme. Üle 1500 m kõrgusel asuvad kanalid läbivad Gallura ilma seda teadmata; Kesk-Korsika ei kehti.

Praktikas: kui marsruut ületab mägise saare, proovige mõnevõrra korrigeerida vaba mere osa, kuid ärge arvestage teed tugeva tuulega kitsas.

## 10 seksikat. TN101 raadiokliima ja ajaline varieeruvus (alates 1.3.5)

TN101 § 10 [25] kirjeldab, kuidas horisondi taha jääva marsruudi kadu varieerub pika perioodi jooksul mediaani ümber, kliima järgi erinevad kõverad: Y(q) funktsioon ütleb, mitu dB kadu langeb q% parima aja jooksul. ITM sisaldab neid kõveraid seitsme kliima jaoks. Kliima loeb palju: merel parasvöötme merekliimas 144 MHz juures, 10% ajast teenite palju rohkem dB kui mandrilise kliima maal, sest kanalid on sagedasemad.

Programm ei paranda enam kliimat: Ta valib selle iga maiuse jaoks keskmisest punktist, nii et see toimib kogu maailmas ja mitte ainult Euroopas. Laadige alla Open Meteo [26] kolm aastat ERA5 kordusanalüüsi päevaväärtusi (temperatuur, niiskus ja mullarõhk, vihm) lahtris 0,5° keskmisest punktist, ainult üks kord; väärtused jäävad Data / CLIMA-sse. Mulla taastamine

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

päev päeva järel, siis aasta keskmine ja igakuiste keskmiste, keskmise niiskuse ja aastase vihma vahel. Reeglid järgivad TN101 § 10.2 kliima kirjeldusi; piirid ühe ja teise kliima vahel on autori kriteerium:

- üle 60° laiuskraadi: parasvöötme mandriline ( TN101 -l puuduvad polaarkliima kõverad);
- alla 250 mm vihma aastas ja keskmine õhuniiskus alla 50%: kõrbes (Ns tüüpiline 280);
- ekvaatorist kuni 20° kaugusel, Ns vähemalt 350 ja ekskursioon kuni 30: ekvatoriaalne (Ns tüüpiline 360);
- vahemikus 32° ja ekskursioon vähemalt 60: subtroopiline mandriline, st mussoonid (tüüpiline ekskursioon 60–100);
- 10° kuni 32°, Ns vähemalt 340: lähistroopiline mereline (Ns, tüüpiline 370);
- mõlemad horisondid merel, üle 20°: merel karastatud (see on TN101 määratlus);
- ekskursioon kuni 30-ni mere lähedal (meri marsruudil või keskmine õhuniiskus vähemalt 75%): merekalorsusega maa peal;
- muidu mõõdukas mandriline (tüüpiline ekskursioon 20–40).

N0 on keskmine taandatud merepinnale, N0 = Ns·e^(h/9460). Ilma võrguta kasutate reservi reeglit ainult laiuskraadi ja mereosaga ning joon ütleb seda.

Katsed andmetega ERA5 2023–2025: Capri Ns 339 ekskursiooniga 45 (mandri karastatud maal, mere parasvöötme merel, kui mõlemad horisondid on merel); London 327 ja 16 Viin 319 ja 24 Abidjan 381 ja 11 (ekvatoriaalne); Miami 365 ja 36 (subtroopiline abikaasa); Delhi 337 ja 82 (subtroopiline kontinentaalne); Riad 278 ja 37, 90 mm vihm (kõrb). Tšiilis: Antofagasta 341 ja 19 (subtroopiline abikaasa: Atacama rannik, Vaikse ookeani peaaegu fikseeritud madal tagurpidi), Calama 232 maapinnal, 2257 m, 14 mm vihma (kõrb), Valparaíso 331 ja 11 (temperatuuriõmblus), Punta Arenas 313 ja 5 (temperatuuriõmblus).

Rida "Raadiokliima ( TN101)" näitab kliimat, põhjust, Ns poole ekskursiooniga ja Y: kui palju dBs teenib 10% ja 1% ajast ja kui paljud kaotavad 90%. See on pikaajaline statistika ja jääb prognoosist eraldatuks: oodatav kahjum järgneb GFS-profiilidega tänasele ajale ja kokku võtaks kaks asja kaks korda sama ebanormaalse leviku.

## 10 septi. Prognoosimise ja radiosonidega kontrollimise usaldusväärsus (alates 1.4)

Radioson kontroll. GFS näeb kanaleid vähem hästi kui tundub: Selle vertikaalne eraldusvõime maapinna lähedal on paarkümmend meetrit ja tagurpidi kiht on sageli õhem. Et teada saada, kui palju usaldada mudelit siin ja nendel päevadel, võrdleb programm seda lähima jaama tegelike raadiolainetega, NOAA [27] arhiivist IGRA v2 (avalik domeen):

- võtab jaamade loetelust 600 km raadiuses lähima aktiivse jaamade nimekirja (liigid Capri, Practice of Mare, ITM00016245);
- alla laadida jooksva aasta fail (uuendatakse iga paari päeva tagant) ja hoiab 00 ja 12 UTC käivitamist viimase 15 päeva jooksul;
- iga stardi puhul tuletatakse reaktiivsus N tase taseme järgi (koos kastepunkti niiskuse või rõhuga), nende tasemete kõrgused, millel neid ei ole, ipsomeetrilise võrrandiga alates jaama kõrgusest, esimese km gradiendiga ja tugevaima kanaliga alla 3000 m (M, mis langeb vähemalt 2 ühiku osakaaluga);
- laadida alla Open Meteo [15] ajalooliste prognooside teenusest sama punkti GFS-profiilid samal ajal ja analüüsida neid sama prognoosikoodiga;
- Loendab GFSi poolt nähtunud jälgitavaid kanaleid, puuduvaid ja valehäireid ning gradiendi keskmist ja ruuthälvet.

Kontrollimine toimub esimese raviarvutuse taustal, kõige rohkem iga 12 tunni järel; failid jäävad Data/IGRA-sse ja arvutamisregistrisse. Maailma ilmavõrgust (TEMP) tulevatel raadiosonde sõnumitel on ainult standardne ja märkimisväärne tase: ka väga õhukesed kanalid pääsevad nendeni ja võrdlus ütleb eelkõige, kas GFS on ühes või teises mõttes väga vale.

Usaldusväärsus. "Affidability" rida annab oodatava kasu kohta suure, keskmise või madala hinnangu. See ei ole kalibreeritud tõenäosus, mis nõuaks aastatepikkust võrdlust reaalsete linkidega (§ 14), vaid punktide summa selle kohta, kuidas sõltumatud vihjed (autori kood):

| Aadress | Punktid |
|---|---|
| Mudelite kokkulangevus: keskmine hälve oodatava kahjumi ja kanalite P.452 vahel, paraboolvõrrand ning nulljoone ja ITMi vahel | kuni 3 dB +1; üle 8 dB −1 |
| Eelprognoosi prognoos | 24 tunni jooksul +1; rohkem kui 72 tundi −1 |
| Asutamiskihi püsivus töötlemisel | vähemalt 70% +1; alla 30% −1 |
| Kihi püsimine naabertundidel | vähemalt 70% +1 |
| Radiosondi kontrollimine | GFS vähemalt 3 korda 4 + 1-st; vähem kui üks kahest −1; vale gradient üle 15 N/km −1 (maksimaalselt −1 kokku) |
| Otsene kinnitus marsruudil (klaster, PSKReporter, APRS, FM-DX) ainult jooksval ajal | 1–2 paari jaamu +1; 3 või rohkem +2; avad eeldatava võimendusega alla 2 dB −1 |

Kõrge 3 punkti, keskmine 0 kuni 2, madal alla nulli. Reaalajas kinnitus loetakse erinevate jaamade paariks, mis asub marsruudist 20° kaugusel ja mille keskmine punkt jääb 150 km kaugusele liinist viimase pooleteise tunni jooksul. Arhiivi puhul (§ 3 ter) ei ole raadiosonte ega elavaid kinnitusi.

## 11. Võrdlus ITU-R P.452-ga

Joonega P.452 rakendatakse ITU meetodit kanalite ja kihtidel peegeldumise jaoks (P.452 § 4.5 [6]) klimatoloogilise asemel GFSi mediaaniga ΔN ja kalibreerimiskonstandina p = 20%, nagu CSIRO [10] uuringus. See hõlmab empiirilise sideme kadu Alf alla 0,5 GHz (umbes 28 dB kuni 144 MHz, 3,532) Tegemist on vaid võrdlusega: oodatav kahju jääb lõikes 9 esitatule.

Alates 1.4: P.452-18 täidetud ja kontrollitud. Võrdluses kasutatakse nüüd kõiki P.452-18 [29] põhimeetodeid (punktid 4.1–4.6): vaba ruum gaasi ja multitee abil, difraktsioon sfäärilise maaga ja Bullingtoni profiil takistuste õigel siledal pinnal, troposhajumine, kanalid ja peegeldus kihtidel ning nende kombinatsioon. ΔN ja N0 pärinevad GFS-ist ja kohast ITU kaartide asemel. Koodi on kontrollitud ITU uurimisrühma 3 (kogutud projektis Py452 [29]) ametlike katsejuhtumitega: 194 juhul kuni 2 GHz ilma maapinda neelamata on keskmine kõrvalekalle kogukaost 0,06 dB, maksimaalne 0,54 dB; difraktsioon keskmiselt 0,10 dB, troposkander 0,002 dB. Kontrollimise käigus on parandatud eelmiste versioonide kaks viga: sagedust MHz-des, mitte GHz-s (umbes 60 dB liiga palju, kompenseeritud mujal), ja difraktsioon lahknes juba piiratud sileda pinna kvootidest, vigadega kuni 15 dB teatud profiilidel.

ITM-i (§ 10 quater) on võrreldud ka NTIA [30] -ga (punktpunktpunkt): ametliku faili viis osa annavad samad väärtused dB sentile.

## 11 bis. Paraboolvõrrand (alates 1.3.1)

Paraboolvõrrandijoon on neljas arvutus, ainult võrdlusest, mis on tehtud kanalite professionaalsete programmidena (PETOOL; M. Lytaevi lainete levimise raamistik, MIT litsents): kihtidest saadava kasu hindamise asemel liigub see laine mööda marsruuti Fourier' split-step meetodiga GFSi M modifitseeritud murdumisprofiilis, mis on võetud prognoosipunktist lähemale igale kaugusele. GFS tasemed lisavad võimalike kanalite hinnangulised hüpped (arvutades A, 50 m poolkihis). Pinnas Copernicus GLO-30 siseneb astmetesse (maa all on väli null), muld on juht ja antenni tala on Gaussi oma eeldatava avausega. Arvutuses on näha kanalid, need, kes kaotavad, antennide difraktsioon ja sidumine; Ta ei näe troposhajumist, mis lisatakse peale. Järgmine on "profiili tšekk": sama standardse atmosfääriga, vähem oodatud profiiliga.

Alates 1.3.2:

- levik on laia nurga all, vaba ruumi täpse faasiga:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- mere ja ranniku tasandiku põhi nullkõrgusel peegeldub Fresneli koefitsiendiga horisontaalses polarisatsioonis, εc = εr − j·60·λ·σ (meri εr = 81 ja σ = 5 S/m; riik 13 ja 0,006 S/m), marsruudi selge nurga all:

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- mere karedus vähendab Milleri ja Browni järgi järjepidevat peegeldust, kui lainete efektiivne kõrgus tuulest on 10 m (σh = 0,0051·U2):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- merel sisaldab profiil M neutraalse logaritmilise profiiliga aurustuskanalit (punkt 9 ter):

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

See katsetab prototüübiga (10 m antenn merel, võimendus standardatmosfääril 100–300 km): δ = 10–30 m aurustamiskanalis on 1–3 dB kuni 144 MHz, 8–37 dB kuni 432 MHz ja 21–66 dB kuni 1296 MHz. 144 MHz horisontaalse polarisatsiooni korral peegeldub meri peaaegu metallina (|η| rohkem kui 0,99 kuni mõni kümnendik kraadi) ja karedus loeb vähe: uus põhi muutub mõne kümnendiku dB-st; see loeb rohkem 432 MHz-lt üles.

Piirväärtused: Maa kohal jääb vertikaalne profiil GFS-i tasemete omaks, nii et õhukesed kanalid jäävad nähtamatuks; maapinnast astmetesse on ligikaudne; tala jääb 6.

## 12. antennid, signaal ja müra

- Yagi DL6WU: võimendus 9,5 + 7,8·log10(boom/λ) dBd (2,35 dB topeltbuumi korral, DL6WU mõõde VHF Communications 3/1982), pluss 2,15 [12] dBi puhul.
- EIRP = võimsus − kaabel + TX võimendus.
- Saadud signaal = EIRP − eeldatav kahjum + kasum RX − kaabel RX. S-meeter IARU: S9 = −93 dBm, 6 dB ühiku kohta.
- Välismüra ITU-R P.372 [13] järgi: galaktiline (Fa = 52 − 23 log f) tehislikum vastavalt keskkonnale (linn, elamu, maa, vaikne maa) või käsitsi kirjutatud antenni temperatuurile.
- Tsys = Ta + Tpre + [290(Lc − 1) + 290(Fr − 1)·Lc] / Gpre antenni klemmidele (Friis valem). Alates 1.3.5 on vastuvõtujaamas alati antenni eelvõimendi, fikseeritud parameeter: müraarv 0,8 dB (Tpre = 59 K) ja võimendus 20 dB. Selle taga on kaabel (kaotus Lc) ja raadio, mille põhiline müraarv on Fr 6 dB. 1 dB kaabliga langeb kaabel ja raadiosisend umbes 1160 K-lt 12 K-le: müra otsustab taeva, keskkonna ja eelampi. Võimsus 20 dB on programmi poolt valitud tüüpiline väärtus.
- S/N SSB-des, CW-des ja FT8 -des igas suunas, võrreldes kehtestatud künnisega.
- Tõenäosus perioodi kohta (alates 1.3.2): kadumine tunnis on S/N normaalne jaotus eeldatava keskmise tunniväärtuse ümber. Bray (QST, november 1961, lk 39, Nortoni 1959 kõveratel) annab amplituudi 13,5 dB üle umbes 100 miili, st umbes 7 dB allapoole ja 7 üle keskmise, mis langeb peaaegu otse alla 100 miili (3,5 dB kuni 50 miili); "tahke" link võtab eelarvest 7 dB. Võetakse ±7 dB täpsusega ±2σ: σ = 3,4 dB 160 km jooksul, mis on proportsionaalne allpool oleva kaugusega. Tõenäosus, et periood ületab tee künnise, on

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  Φ normaaljaotusfunktsiooniga: marginaal 0 dB = 50%, +3,4 dB = 84%, +7 dB (Bray 'tahke' marginaal) = 98%. See ei hõlma mudeli ebakindlust, mis on suurem.
- Digitaalsed viisid: S / N 2500-s Hz võrreldes 50 % dekodeerimisel mõõdetud künnistega ilma eelneva teabeta (AP). FT8 −21 dB ja FT4 −17,5 dB valgel müral; kui mehhanism on troposfäärihajuv, kasutage lävendit, millel on vananemisaeg "häiritud laiuskraad", FT8 −18,6 dB ja FT4 −15,2 dB [21]. Q65 -15C −22,2 dB, Q65 -30B −24,8 dB, Q65 -60A −27,6 dB Lennuki hajumise korral võib Doppler ületada Q65 -60A (1,7 Hz) toonid: on paremaid 30B või 15C. JT65A −25 dB [22]. Dekodeeriv AP (mis on juba tuntud või QSO lõpu nimi) annab FT8 -le umbes 2 dB lisa ja Q65 -le 1,5 kuni 2,6 dB; see ei sisene künnisesse, sest see ei kehti kõigi FT8 -le sageli loetavate "−24 dB" väärtuste puhul on WSJT-X suhete piir või AP dekodeerimine, mitte künnis 50%. [24]

## 13. Vastamise ja andmete esitamise aeg

Iga arvutus mõõdab oma aega: GFS-profiilid (võrk, analüüs, kohustuslikud 20-sekundilised pausid partiide vahel avatud Meteo piiri jaoks), Copernicuse GLO-30 maastik (paneelide allalaadimisega 15–30 MB igaüks, ainult esimest korda), arvutus. Reageerimisaja rida võtab need kokku; registris algavad read TEMPI-ga. Võrgu uuendamisel annab register teada ka kasutatud GFS-võistlusest, sellest, millal see kättesaadavaks sai ja kui vanad on andmed.

Alates 1.3.2 saate valida ka ICON-2I, agentuuri ItaliaMeteo ja Arpae mudeli Itaalias umbes 2,2 km kaugusel, läbi Open-Meteo (mudel italia_meteo_arpae_icon_2i, kaks võistlust päevas, 3 päeva). See hõlmab ainult Itaaliat ja lähedalasuvaid meresid ning sellel on vähem GFS-i tasemeid (1000, 925, 850, 700, 500 hPa): kanalite jaoks kõrgusel ei ole GFS parem kui GFS, kuid peenes võrgus kirjeldatakse kaevu rannikut, saari ja merd 2 m kaugusel, see on aurustumise kanal. Muutujad, mida mudelil ei ole (astmepiir, pinna temperatuur...), eemaldavad end taotlusest. Alates 1.3.2 kasutab automaatne allikas ainult NOMADS-i; Open-Meteo teenib siis, kui NOMADS ei reageeri ja ajalooliste juhtumite korral. Kastiga "Uuendamine omal" (vaikimisi valgustatud) laaditakse prognoos alla ilma klõpsamata: avamisel, igal uuel GFS-i võistlusel NOMADSis (umbes 5 tundi pärast võistluse aega) ja kui nähtav ala on võrgust väljas rohkem kui veerand; maksimaalselt iga 15 minuti tagant, mitte kunagi ajaloolise juhtumiga.

Open Meteo päringutel on iga internetiaadressi jaoks piirangud minuti, tunni ja päeva kohta. Allalaaditud profiilid salvestatakse kettale (Dati/cache-gfs) iga löögi ja mudeli jaoks: sama võistlusega, mis muutub iga 6 tunni järel, ei küsi juba allalaaditud punktid uuesti. Automaatse lähtekoodiga alustate NOMADSist ja Open-Meteo teenindab ainult puuduvate punktide jaoks; ainsa Open-Meteoga peatub ajapiirangu või igapäevase piiranguga kohe ja ütleb, millal uuesti proovida. Ajaloolised juhtumid ei ole vahemälus.

## 14. Mudeli valideerimine

Prognoosi tuleks võrrelda reaalsete ühendustega. Arvutuslogi säilitab iga numbri; programmi arhiiv hoiab reklaame ja dekodeerimisi. 432 ja 1296 MHz puhul, kus laike on vähe, vajavad nad operaatorite aruandeid: kes ühenduse tegi, millise ajaga, millise signaaliga. Võrrelge mitme tunni jooksul oodatavat kasu mõõdetud signaaliga, mis ütleb, kas künniseid tuleb liigutada.

P.452 ja ITM jaoks kasutatavad testprogrammid on lähtekoodi tööriistade kaustas; ITU testjuhtumid tuleb eraldi alla laadida, sest programm ei levita ITU andmeid.

## Allikad

1. NOAA/NCEP, Global Forecast System (GFS), globaalne mudel 0,25°, neli võistlust päevas.
2. Open-Meteo, ennustus API (open-meteo.com), gfs_seamless mudel, muutujad rõhutasemel ja võidusõidu metaandmed.
3. Copernicus DEM GLO-30 (ESA; WorldDEM © DLR e.V. 2010–2014 e © Airbus Defence and Space GmbH 2014–2018, tarnitud Copernicuse raames Euroopa Liidu ja ESA poolt), 1 kaar, GeoTIFF kaadrid Amazoni veebiteenuste avatud registris. Reserv: NASA/USGS Copernicus GLO-30 AWS-i maastikuplaatidel "skadi".
4. ITU-R P.453, Raadio murdumisnäitaja: selle valem ja murdumiskuupäev koos digitaalkanalite kaartidega (uuringurühm 3, © ITU, isiklik ja mitteäriline kasutamine).
5. B. R. Bean, E. J. Dutton, Radio Meteorology, NBS Monograph 92, 1966: Table 6.5 and § 4.4.
6. ITU-R P.452-16, Prediction procedures for the evaluation of interference between stations on the surface of Earth at frequencys over ca 0,1 GHz: § 4.2 (diffraction), nt 45 (troposcatter), § 4.5 and eq. 49 (conducted).
7. ITU-R P.834, Effects of tropospheric refraction on radiowave propagation.
8. ITU-R P.525, Vabaruumi sumbumise arvutamine.
9. ITU-R P.526, Paljundamine difraktsiooni teel: nt 31 (tagioon) ja § 3 (sfääriline maa).
10. B. Indermuehle, H. Suzuki, Tropospheric Ducting Prediction based on GFS Model Data at Inyarrimanha Ilgari Bundara, the CSIRO Murchison Radio-astronomy Observatory, Publications of the Astronomical Society of Australia, 2026 (arXiv 2608.12861).
11. D. E. Kerr, Propagation of Short Radio Waves, MIT Radiation Laboratory Series vol. 13, 1951: eq 17 e 18.
12. G. Hoch DL6WU, „Extremely Long Yagi Antennas, VHF Communications 3/1982, lk 130 jj, viigimarjad. 1 (mõõdetud 432 ja 1296 MHz vastavalt buumi pikkusele).
13. ITU-R P.372, Raadiomüra: galaktiline ja tehismüra keskkonnale.
14. NOAA/NCEP, NOMADS, GRIB filter GFS 0,25° ajast (nomads.ncep.noa.gov); WMO G RIB2 formaat, ruumiliste erinevustega kompleksne pakendamine.
15. Open-Meteo, Historical Forecast API (näidisprognoosi arhiiv umbes 2021. aastast).
16. LukiBox, TropoLink, Troposcatter marsruutide disain koos P.617, NBS TN101 ja ITM külgnevatega (GitHub, Apache 2.0 litsents).
17. ITU-R P.617-5, Propagation prediction techniques and data required for the design of trans-horizon radio-relay systems, 2019.
18. RSGB, VHF/UHF Handbook; DL2FZN implementation (andi-f/troposcatter on GitHub, 2013).
19. W. F. Moler, D. B. Holden, „Tropospheric scatter propagation and atmospheric circulations, NBS 64D teadusajakiri, 1960, doi 10.6028/jres.064d.014: signal beyond the horizon (1365 and 9380 MHz, on the sea) and vertical movement of the air.
20. ARRL Handbook 2022 (99a trükk), § 19.4.3–19.4.4: ilm ja tropoferical propagation.
21. S. Franke, B. Somerville, J. Taylor, The FT4 and FT8 Communication Protocols, QEX July/August 2020.
22. W3SZ, aeglaste digitaalrežiimide künnised ( JT65, JT9, QRA64) ja režiimi valik vastavalt levimisele, 2019.
23. QST, "Maailm üle 50 MHz", november 2022: avamine 14.–15. augustil 2022 Manitoba ja Kansas/Missouri vahel.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Quick-Start Guide to Q65, 3. aprill 2021 (wsjt.sourceforge.io).
25. P. L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis, Transmission Loss Predictions for Tropospheric Communication Circuits, NBS Technical Note 101, ajakiri 1967: § 9 (troposcatter, funktsioon F(θd), H0), § 10 ja III lisa (raadiokliima ja varieeruvus aja jooksul).
26. Open Meteo, Historical Weather API (ECMWF ERA5 läbivaatamine alates 1940; Copernicus Climate Change Service, CC BY 4.0 litsents).
27. I. Durre, X. Yin, R. S. Vose, S. Applequist, J. Arnfield, Integrated Global Radiosonde Archive (IGRA), version 2, NOAA National Centers for Environmental Information, doi 10.7289/ V5X63K0Q (public domain): igra2-data-format.txt data format and station list.
28. FM-DX Webserver (NoobishSVK ja koostööpartnerid, litsents GPL-3.0) ja serverite avalik nimekiri.fmdx.org.
29. ITU-R P.452-18, Prediction procedures for the evaluation of interference between stations on the surface of Earth at frequencys over ca 0,1 GHz, 2023; test cases of the study group 3 (validation examples), as collection in I. Stevanovic, Py452 (GitHub).
30. NTIA/ITS, ebaregulaarne maastikumudel (ITM) 1.2.2, viitekood ja punktikatse juhtumid (GitHub NTIA/itm).
31. ITU-R M.1371, Technical characteristics for an automatic identification system using time division multiple access in the VHF maritime mobile frequency band.
