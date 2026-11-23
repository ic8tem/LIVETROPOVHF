# المصادر والصيغ

> ترجمة آلية من الإيطالية: عند الشك يُعتمد الدليل الإيطالي.

جمع المعلومات من المصادر التي تم استشارتها من أجل التنبؤ بسلسلة الترددات العالية جدا)٢٠-٢١ تشرين الثاني/نوفمبر ٢٠٢٦(: الفيزياء الأساسية، البيانات المتاحة، المؤشرات المنشورة، الصيغ، تجارب الهواة الإذاعية، المصادقة والقرارات. والصيغ مرقمة كما هو الحال في لاتيكس، (1)، (2)،...، وفي النص الذي يتذكرونه مع عدد الأقواس. ويصف دليل التنبؤ كيف يستخدمه البرنامج.


## 1. مباني المؤلف

IC8TEM observations that frame the work, with feedback found in sources.

- الجغرافيا الإيطالية تدعو إلى الانتشار. ومع وجود الحواف والبرومونتورية والجزر على بعد بضع عشرات كيلومترات من المحطات، ينبغي حساب مساهمات الانتشار بمساحة النفق على الحافة الرئيسية، ليس فقط مع الأرض السلسة. Meeting: in a comparison on irregular terrain at 563 MHz (Rio de Janeiro, 27 points between 5 and 30 km), the best methods were P.526 with blade edges, P.1812 and the diffractive wedge, with average error between −5 and +3 dB and dispersion of about 9 dB; P.1546 was the most pessimist (from Costa, Rabanal Ramirez and For s
- للوصول إلى قناة بعيدة، تحتاج Fresnel. وإذا بدأت القناة بعد الأفق، تأتي الإشارة إلينا بالنشر على العقبات حتى بدايتها؛ ومن 1.3.2، يحسبها البرنامج (دليل التوقعات، الفقرتان 10 ثالثا). Meeting: Rohde &quot; Schwarz (2023) notes that stations along a high duct often fail to enter, while those close to the ends yes.
- الظروف تتغير بسرعة Theoryies are there, but the conditions change in minutes, and no forecasting system takes the data in real time: the GFS comes out every 6 hours, with time span and 0.25°, and a predict can lose value within a short time. Meeting: Bray (QST, November 1961) measurement of hourly median experiences of approximately 07 dB over 100 miles; the ducts move with atmospheric time (Rohde & Schwarz); our QSO register of 29/12/2021 -2/1/2022 shows openings growing and falling within hours.
- ولا يزال أفضل البث الآن هو شبكة &quot; دي إكس &quot; للموردين. To know what happening now, the cluster spots, PSKReporter (and WSPR) beat any pattern: they are measures. والتوقعات تقول أين ننظر وكم من الوقت يمكن أن تستمر؛ فالإعلانات تقول إذا كان هناك. برنامج هذا يضع البقعة والتوقعات على نفس الخريطة

## 2. الغرض

والفكرة هي فهرس ثالث، يُحسب بالترددات العالية جدا، بدءا من الملامح العمودية لنموذج الطقس، يضاف إلى Hepburn و F5LEN بدلا من استبدالها. اليوم يقرأ البرنامج خرائطهم جاهزة بالفعل: فهرس سيقول أيضاً: لماذا يوجد فتح (اقتباس وسماكة القناة، الحد الأدنى للفرقة المحصورة) ويمكن أن يحدث مع البقع التي يجمعها البرنامج بالفعل.

- ما يمكن أن تعطيه: إحتمال كوندوت في كل نقطة وساعة، حصة من القاعدة والأعلى، السميكة، الحد الأدنى من التردد المحصور، وعلى طول طريق الجزء من الطريق داخل قناة.
- ما الذي لا يمكن أن يعطيه: النوافذ الرقيقة (بدون 10 أمتار) التي لا تحلها النماذج العالمية عند 0.25 درجة، وخطابات التبخر في البحر، التي تخدم فوق كل شيء من محطة GHz.
- First practical objective: 144 and 432 MHz in Europe, with verification on DX cluster spots, PSKReporter and local decodings.

## 3. الفيزياء الأساسية

كل شيء يبدأ من تفاعلي N calculated at each profile level: the conduit is where the modified reactivity M decreases risinging, that is where dN/dh falls below −157 N/km ([ITU-R P.453-13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)).

Refractivity (P and in hPa, T in kelvin):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

ضغط البخار من الرطوبة النسبية H (%) ودرجة الحرارة t (oC) أعلى من الماء بين 40 و+50 درجة مئوية:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

(ح) معدل التفاعل (بكم، أو 0.157) z with z in meters) ومعيار الخط:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN/dh (N/km) | المادة | أثر 144 ميغاهيرتز |
| صفر | تحت الحمراء | انخفاض معدل التدفق |
| من صفر إلى 79 | عادي (المعيار نحو 40) | أفق إذاعي مكافئ 4/3 |
| من 79 إلى 157 | "الخوف الخارق" | التدفق البعيد، "جيد جدا" |
| أدناه - 15 | القناة (النقل) | إشارات محاصرة، أكثر من 1000 كيلومتر |

كَيفَ تَستخرجُ نِ من a نموذج على مستوياتِ الضغطِ: في كُلّ مستوى تَأْخذُ T، الرطوبة النسبية والطولِ الأرضيِ، تَحْسبُ N و M، ثمّ التدرج بين مستوياتِ الجيرانِ. The levels below the ground should be discarded (at 1000 hPa on the Alps is underground).

الحد الأدنى من التردد المحاصر: القناة لا ترشد سوى الموجات القصيرة نسبيا مقارنة بسمكها. Hepburn publishes a table fishness. minimum useful frequency ([DX Info Centre](https://www.dxinfocentre.com/propagation/luf.htm)); it is a soft limit, under which the signal gradually drops.

| العصى | الحد الأدنى للتواتر المستحق الدفع |
|  61 m  | 962 ميغاهيرتز |
|  91 m  | 464 MHz |
|  122 m  | 276 ميغاهيرتز |
|  152 m  | 185 ميغاهيرتز |
|  183 m  | 133 ميغاهيرتز |
|  213 m  | 101 MHz |
|  305 m  | 53 ميغاهيرتز |

وفي نطاق الترددات العالية جدا)التي تبلغ في كثير من الأحيان ١٢٠-٣٠٠ متر( تتبع قيم الجدول قانونا ثابتا تماما؛ وتقلل الصيغة من ١٠٠ متر من تقدير التردد إلى ٣٠-٤٠ في المائة. تعديله بنفسي (وليس بصيغة نشرها هيبورن) مع إتش في إم إتش و د في متر:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

In practice: for the 2 m it takes a conduit often at least 170-180 m, for the 70 cm it is enough about half. The formula circulating on the net, ikemax = 0.014 · d1.5, comes from Kerr (1951, Chapter 1, eq. 18) and is valid with ike centimeters and standing, for a conduit in which M falls linearly: with these units gives for about 2 m 180 m and for about 70 cm 89 m, that is the same values of the Hepburn table, which comes from it. (كير) يحذر من أنّه ليس جرحاً صافياً، حتى الأمواج أطول عدة مرات تعاني الكثير من القناة، وطريقة أنّ "الخسائر" قليلاً يمكنها تحسين الإستقبال حتى مع محطات خارج القناة. A 2019 IEEE Access study confirms that traditionalal methods (Kerr, Kukushkin) do not describe the real cut ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)): the threshold should be used as an indication, not as a wall.

## 4. البيانات المتاحة مجانا

والنقطة الضعيفة هي القرار الرأسي: ففي ما بين ٠٠٠ ١ و ٨٥٠ من نماذج هبأ يعطي مستوى كل ٢٥-٥٠ من الهكتار، أي كل ٢٠٠-٤٥٠ مترا، مما يجعلها ترى جيدا التحولات إلى ٥٠٠-٢٠٠٠ مترا، ولكن ليس القنوات الرقيقة القريبة من الأرض. ويستخدم التردد فوق العالي جداً من طراز LiveTROPOVHF بالفعل نظاماً مفتوحاً لـ &quot; isobare &quot; ، ودرجة الحرارة إلى 850 hPa، والتحويل: يمكن أن يبدأ المؤشر من نفس المكالمات.

| المصدر | ماذا؟ | القرار | استخدام الرقم القياسي |
| [Open-Meteo, GFS](https://open-meteo.com/en/docs/gfs-api) | T, relative humidity, dew point, geopotential altitude by level (1000, 975, 950, 925, 900, 850 hPa...); altitude of the limit layer | 0.25 درجة على مستويات الضغط؛ 16 يوما، ساعات تصل إلى 120 ساعة، تحديث كل 6 ح | نون و ميم، التدرج، الأساس وفوق القناة؛ بالفعل في البرنامج |
| [Open-Meteo, ECMWF](https://open-meteo.com/en/docs/ecmwf-api) | نفس المستويات من نماذج البيانات المفتوحة |  0,25°  | الرأي الثاني، مفيد للتوافق بين النماذج |
| [Open-Meteo, DWD ICON](https://open-meteo.com/en/docs/dwd-api) | الرطوبة النسبية على مستويات الضغط؛ وفي ICON- D2 يوجد 800 و900 و925 هبة | ICON-EU حوالي 7 km | رأي ثالث بشأن أوروبا |
| [Radiosunds, Univ. Wyoming](https://github.com/NCAR/RAOBget) | مواصفات قياسية، إطلاقان في اليوم (00 و 12 من طراز UTC)، شكل نص لكل محطة | العديد من المستويات الهامة، حتى كل بضعة عشرات متر | التحقق من الرقم القياسي؛ انظر القنوات الرقيقة التي لا يرى النموذج |
| [GROANS, NOAA NCEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive) | المحفوظات التاريخية للبث، ملف لكل محطة، بارامترات أكثر استخلاصا | أعلاه | معايرة سنوات البيانات والمقارنة مع بؤر الماضي |
| ERA5 (Copernicus) | إعادة تحليل الفم منذ عام 1940، 37 مستوى ضغط | حوالي 28 كيلومترا | الإحصاءات والمعايرة، وليس التوقعات |

- With Open-Meteo geopotential altitude is above sea level: the levels that fall under the ground must be discarded ([Open-Meteo, upper air](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)).
- وتضاف على الأرض T والرطوبة في 2 متر: فهي تعطي أول درجة من التدرج، التي تولد فيها النوافذ السطحية الليلية على السهول وعلى البحر.
- بث إيطالي في محفوظات جامعة ويومنغ (Weather.uwyo.edu/upperair/sounding.html): محطات وأرقام المنظمة العالمية للأرصاد الجوية في الجدول أدناه.

| محطة | W | خط العرض | الطول | Share |
| Udine/Campoformido |  16044  |  46,03  |  13,18  |  94 m  |
| ميلان/ليناتي |  16080  |  45,43  |  9,28  |  103 m  |
| ممارسة البحار |  16245  |  41,65  |  12,43  |  12 m  |
| برينديزي |  16320  |  40,65  |  17,95  |  10 m  |
| تراباني/بيرغي |  16429  |  37,92  |  12,50  | حوالي 14 متر |

The text format of Wyoming gives for each level pressure, altitude, temperature, dew point and humidity: enough for N and M. Wyoming does not have an official interface, scripts are used as [RAOBget of NCAR](https://github.com/NCAR/RAOBget); the College of DuPage keeps copies in text by station ([e.g. 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)).](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. الفهرس والأساليب المنشورة

وتبدأ &quot; هيبورن &quot; و &quot; F5LEN &quot; من نفس المكونات المادية، ولكنها لا تنشر الخوارزمية الكاملة؛ وتصف الدراسات العلمية للفترة 2008-2026، بدلا من ذلك، خطوة الطريقة بالخطوة، وتستحق من هناك نسخ الوصفة.

| المنهجية | بيانات بدء التشغيل | ما هو محسوب | ملاحظات مفيدة لنا |
| [Hepburn Tropo Index](https://www.dxinfocentre.com/propagation/hti.htm) | نماذج الطقس الخام لدائرة الأرصاد الجوية الكندية، على النحو المكتوب في خرائطها؛ وليس GFS/NAM، خرائط مصممة مع GrADS، خوارزمية الملكية ([storia](https://www.dxinfocentre.com/propagation/history.htm)) | مقياس خطي يتراوح بين صفر و 10 من فتحات الإشارة: صفر طبيعي، و 1 إلى 2 من الفائقة، و 3 إلى 4 بعض الخناق، و 5 إلى 6 قنوات موزعة؛ وقيم سلبية = نقصان | المقياس الذي سيقيد لذا الرقم الثالث يقارن على الفور بالاثنين الأولين |
| [F5LEN, world forecast](https://tropo.f5len.org/WW/info.php) | for Europe the ARPEGE model of Météo-France, 7.5 km, more levels between 2 m and 1500 m ([note by author](https://tropo.f5len.org/author/f5len/)) | متوسط مؤشر المالك، 40 خطوة من 3 ساعات، 4 حسابات في اليوم، محفوظات لمدة 28 يوما ([نسخة جديدة](https://tropo.f5len.org/?p=504)) | تؤكد أنها تخدم نموذجاً به مستويات منخفضة كثيرة |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf) | 60 من مستويات النموذج ECMWF، من الأرض إلى 3000 متر | الحد الأدنى للحمض النووي/الدز لكل عمود: أُجري تحت الرمز −157 N/km, super-refraction under −78.7 N/km; base of the pie layer | climatology: Padana Plain the European area with more pipelines all year round; Mediterranean in summer near 100%, in winter under 15%; Italy over 30% on summer nights |
| [Indermuehle and Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861) | 0.25 درجة مئوية، 21 درجة ضغط + حقول عند 2 متر | M = N + 0.157 z; minimum dM/dz below 3 km, base and top of the duct, depth of the M deficit; under 40 m discarded | تم التحقق منه لمدة 7 سنوات في 25 قناة من 88 ميغاهرتز إلى 68 2 غيغاهيرتز؛ وقدرة جيدة قبل حوالي 3 أيام (وحدة تحليل المواد الكيميائية حوالي 0,74) ثم قطرات؛ ونظام الأفضليات المعمم أفضل من ERA5 |
| [Hämäläinen et al., IMF 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf) | Harmonie-AROME, 750 m or 2.5 km, 65 or 90 levels | DM/dh Institut 0 on the model profile, comparison with a 4 to 59 m beam | يُحسب أكثر عمودية من القرار الأفقي؛ ولا يرى أي نموذج القناة التي تقل عن 15 مترا؛ ويهيمن الرطوبة على درجة الحرارة |

Third amateur radio service already active: [TROPOCAST.EU](https://tropocast.eu/)of Artur SQ3A, for 2 m and 70 cm in Europe. From the predict page ([tropocast.eu/forecast](https://tropocast.eu/forecast)]:

- Data: DWD ICON-EU (pass 0.0625°, about 7 km), default source; alternatively Canadian GDPS (0.15°), less detailed.
- Method: N and M according to ITU-R P.453 from the profile between the layer near the ground and 850 hPa; the recognition of the layers, the union of ducts to the ground and high and the thresholds of the colors are a proper calibration of the author, not published.
- المنتجات: نظرة &quot; كاملة &quot; 0-3000 متر، وجهات نظر أكثر انفصالاً &quot; إلى الأرض &quot; و &quot; عالي &quot; (والآن فقط في الخرائط التي ستدرج)؛ ورقم صفر-100 في تسع فئات (دون 38 خلفية، و38-52 هامشية، و53-58 ضعيفة، و59-65 معتدلة، و66-72 جيدة جداً، و81-88 درجة عالية، و89-95 درجة، و96-100 حرارة جداً)
- المجالات: أوروبا، بولندا، المملكة المتحدة، ألمانيا، فرنسا، إسبانيا، إيطاليا، الجمهورية التشيكية، كرواتيا؛ واجهة إيطالية أيضا.
- Reuse: maps incorporate with an API keyless iframe; there is also a documented API (OpenAPI and JSON schema). ولا يُشار إلى الترخيص: قبل استخدامه كمصدر ثالث إلى جانب شركة هيبورن وشركة F5LEN ، ينبغي أن تسأل صاحب البلاغ.

الاختلافات في مشروعنا: يتوقف نظام تروبوكاست عند 850 هكتاراً (حوالي 1.5 كيلومتراً)، في حين تصل القنوات المرتفعة إلى 2000 متراً وما بعده (حالة LA0BY 22/11/2018)، ولا تعطي مؤشراً للفرق أو المسار. الصفات من 0 إلى 100 يمكن مقارنتها بمؤشرنا 0 إلى 10 في نفس الأيام

تركيبة الصور (من الرابط الذي قدمه صاحب البرنامج): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}` ، حيث n هو خطوة التنبؤ. ويحظر الموقع الانسحاب التلقائي للآليين، حتى لا أتمكن من قراءة الصورة؛ أيضا لهذا، قبل أن تظهر هذه الخرائط في LIVETROPOVHF يخدم إذن صاحب البلاغ.

Notebook: [DJ5AR, category](https://dj5ar.de/?cat=174)(Magonza, 21 articles 2017-2023, 2 m-13 cm). Cases described: conducted several weeks from Ireland (23 May-2 June 2023), FT8 to Belarus (September 2020), Italy - Germany over the Alps and Adriatic (August 2017). نعترف بحالة قناة "المرتفعة جدا" التي لا يمكنك استخدامها من الأرض: تأكيد آخر أنه من الضروري مقارنة حصة القناة مع حصة المحطات. وهي تستخدم المناهج المشعة كتدبير منافذ البث (حتى 1028 كيلومترا) والملامح العمودية في باريس وبوردو ودي.

In summary, the common recipe: column. N and M per level. layer with dM/dh negative. base, top, fishness and force of the duct. index. جزء "الأميرة" من "هيببورن" هو المعايرة التجريبية للأحداث الحقيقية، والتي يمكن أن نستعيدها مع البقع.

## 6. شكل مفيد للبرنامج

In addition to N and M (basic Physics section), these are used to move from spiritual to point number and then to path. ITU formulas had been reconstructed by a PDF read in part badly; on 22 November were reviewed on official texts ([ITU-R P.452-18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453-14, P.526-16, P.372-18) and corrected in the code: constant of the troposcatter 190 (not 190,1) plus the absorption of gases with Governing = 3 g/m3. Rumore P.372-18: table 1 and galactic correspond; Artificial noise curves are up to 250 MHz and the galactic up to about 100 MHz, over the program extends the same laws.

1 التدرج بين مستويين من النموذج (ح في كل كيلومتر، ميم في الوحدة ميم):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. Conduit strength and fishness: fishness d = top − base (for a soil duct, base = soil); strength as M deficiency between base and top.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3 معامل معادِل نصف قطر الأرض (P.452-18, eq 5 and 6a) مع DIN = متوسط قطر N في الكيلومترات الأولى، إيجابي في N/km:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

With DIN = 40 you have about 4/3 k (standard atmosphere); with DIN that approaches 157 k tends to infinity, that is, the Earth radio becomes flat: it is the beginning of the duct.

4 الأفق الراديوي لمترات الهوائي عالية، مع نصف قطر مكافئ

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5 (ب) احتمال خط العرض (P.452-18, eq. 2)، النسبة المئوية من الوقت؛ / خط العرض في مركز الطريق، ميكروه1، ميكرو4 يأخذ في الاعتبار إلى أي مدى هو البحر:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

عند 41 درجة N, all by sea ( micro1 = chlor4 = 1), gives about 11% of the time: is the climatological base, useful as a reference value from which the expected index differs.

6 Loss for troposcatter (P.452-18, eq 45; f in MHz, d in km, angle of the path in mrad, N0 ground reactivity at the center of the route):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

ويظهر مصطلح &quot; N0 &quot; - 15 0Q السبب في أن الهواء الرطب يساعد أيضاً من دون قناة: 50 وحدة إضافية من الوحدات الوطنية تقل عن الخسارة بـ 7.5 وحدة. ويستخدم التردد العالي جداً بالفعل نسخة G3YGF من فقدان البعث.

7. Loss in duct (P.452-18, eq. 46–51): fixed loss of coupling plus a attenuation for angle milliradiant, which grows with the cubic root of frequency (f in GHz, a/e in km):

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. من بين ودوتون (1966) وكير (1951)

ويعطي النصان الكلاسيكيان ثلاثة أشياء مطلوبة على الفور للرقم القياسي، وهي: العتبات المتدرجة تُدرج بالفعل على نفس مستويات ضغط النظام العالمي لمراقبة المناخ، وما يستحق في سداسي البروم ثنائي الفينيل تغييراً في رد الفعل، وما هي الأوضاع المناخية التي تخلق أو تدمر الخناق. * Radio Meteorology*, NBS Monograph 92.

1 Profile classification for pressure layers (Bean and Dutton, tab. 65, p. 248; −dN/dh in N/km). مصممة لموجات إذاعية، وهي تنطبق كما هي على المستويات النموذجية:

| الطريق | الحصاد حول | الخصم الفرعي | غير مهيأة | انتهاك خارق |
| 1000-850 hPa | 0-1,46 km | −dN/dh 0 | من 20 إلى 60 | أكثر من 100 |
| 850-700 hPa | 1,46-301 km | −dN/dh 0 | من 20 إلى 50 | أكثر من 80 |
| 700-600 hPa | 301-420 km | −dN/dh 0 | من 15 إلى 40 | أكثر من 70 |
| ٦٠٠-٥٠٠ | ٤,٢٠-٥,٥٧ كيلومترا | −dN/dh 0 | من 10 إلى 30 | أكثر من 50 |

2. (ب) ما قيمته في الوثيقة (Bean and Dutton، السقف 6، الصفحات 233-258)، التدابير من 70 إلى 180 ميغاهيرتز على طرق تتجاوز الأفق:

- إن أي طبقة)على الأرض أو على ارتفاع( بالمقارنة مع ملامح &quot; الأسنان &quot; ترفع الإشارة إلى ١٠-٢٥ دي بي إلى جميع النسب؛ ويأتي الكسب الأكبر من الطبقة المرتفعة الملو َّثة، وربما بسبب التركيز)مجرى ٢٠٠ كيلومتر(.
- رد الفعل الأرضي Ns and the decline in the first km IDEN are related to the monthly median signal with coefficient between 0.4 and 0,95, median 0,7 (20 routes from 130 to 446 km, about 100 MHz).
- الدورة السنوية: 0.18 دي بي لكل وحدة من الوحدات الوطنية؛ دورة يومية من 0.2 إلى 1.1 درني لكل وحدة من الوحدات الوطنية، على أقصى حد على الطرق من 175 إلى 200 كيلومتر وشتاء.
- The compound parameter of Misme estimates the field variation as 0.5 dB/(N/km); (equivalent − 40) plus a thermal stability term; the parameter of turbulence D (area between the real profile and the adiabatic one, 0-3 km) just improves compared to the simple il. استنتاج أصحاب البلاغ: لا تزال شركة &quot; دي إن &quot; أفضل مقياس للبساطة.

3 الوصف المرجعي والشذوذ (البحرين ودوتون، الفقرتان 3-8 و 3-9). The exponential atmosphere of reference CRPL gives the "normal" profile starting from Ns; the A unit measures how much the real profile differs at each share. ومن الطبيعي أن تُحوّل لمحة عن البيانات المالية العالمية إلى حالة شاذة لوضعها على الخريطة:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

مع الـ (ن) = 313 لديكِ (د.ن) حول (42 ن/كوم) هذا هو نصف قطره 4/3 The −157 N/km conduit criterion is also obtained here by the Snell law for a horizontal radius (eq. 4.23), almost independent from the antenna altitude.

4 Where and when the conduits are born (Bean and Dutton § 4.4; Kerr cap. 3:

| العملية | الحالة | الأثر |
| تحذير | هواء حار وجاف من الأرض المتدفقة في البحر البارد (الشكل من الكمي الصيفي بعد أيام من الطقس الجيد)؛ ويُعزى أساسا إلى العجز في الرطوبة | قناة مكثفة على البحر |
| الإشعال الزحلي | السماء السيرينية، الرياح الضعيفة، الغيوم المنخفضة أو الرطوبة الكثيرة تمنعها | قدت الأرض التي تولد عند غروب الشمس تنمو في المساء وتختفي بسرعة بعد شروق الشمس |
| Subsidence | هواء ينحدر من مضادات الأعاصير، ومسخن وجاف، على الهواء الطازج والرطوبة؛ والفيضانات المتكررة في الجبال والقطاعات الجنوبية الشرقية من معاداة الأعاصير | الطبقة الفائقة الارتداد أو الموصلات العالية؛ وتدرج دائما تقريبا أعلى من المستوى العكسي |
| البحر والأرض | ساحل، تداول بضع مئات متر | خطوط الأنابيب المحلية على طول الساحل |
| ضد القناة | الخلط: تدفئة الأرض من الشمس بين منتصف الصباح وبعد الظهر، والرياح إلى الأرض على بضعة أمتار على التضاريس الخام؛ والضباب الليلي | إحتمال كوندوت لا شيء تقريبا |

5 عنصرين جديدين للمحاولة والحركة الرأسية: ضخ التيارات في مناطق التقارب يزيل ويمسح الطبقات، ويعزز التبعية الإشارة (Moler and Holden: في المتوسط، يزيد عن 2.5 dB بأكثر من الهواء المرتفع، في البحر في إطار النطاقين L و X). فبرنامج &quot; سيتيو &quot; المفتوح يوفر بالفعل سرعة عمودية على مستويات الضغط. واتجاه الضغط: على مسافة 140 كيلومتراً في 186 ميغاهيرتز، انخفضت الإشارة عندما انخفض الضغط إلى جهاز الإرسال وإلى مركز الطريق وعاد إلى مستوى عال ودون إجهاض عندما ارتفع، حيث بلغت الإشارة 2 فقط (البحرين والدوتون، الشكل 6-9، ص 241).

6 من كير وبالإضافة إلى صيغة الحد الأقصى للموجات (القسم المتعلق بالفيزياء الأساسية)، يقدم كير صيغة تقريبية ل M من درجة الحرارة والبخار والارتفاع الوحيدة (البند 3-9، مع استبدال الـ 79 و 4800 القديمة بـ 77 و 6 و 4810)، ويلاحظ أن خفض الرياح يمكن أن يولد موجات تترنح على ارتفاع القناة، وربما تسبب هروبا عميقا (المادة 3-26). المصدر الايطالي الذي أبلغ عنه بين ودوتون والبحث: ل. بونافوغليا)كانون الأول/ديسمبر ١٩٥٨(

## 8. من العروض الإذاعية والتقارير التقنية (السلسلة الثانية)

سبعة وثائق قدمها صاحب البرنامج. ثلاثة يخدمون بشكل مباشر إلى الرقم القياسي: W3SZ لجهاز تروسكاتر "فلور"، LA0BY للحالات التي لم يتوقعها هيبورن، هيبورن (ترجمة LU7MC) لتصنيف الطوابق.

1 إشارة موجودة دائماً R. Rehr W3SZ, &quot; Tropospheric Scatter (Troposcatter) Propagation for VHF, UHF, Microwave and Frequencies &quot;, Pack Rats Conference 2014; J. R. Duffey KK6MC, &quot; The Troposcatter Propagation Mode and How to Predict Coverage on the VHF/UHF Bands 2017, CS.

- مستفيدة من حوالي 100 إلى 700 كيلومتر، من 144 ميغاهيرتز إلى 10 جيهرتز؛ وتزداد الخسارة بمقدار 30 لوقاً واقياً بالتواتر.
- وفوق كل شيء، يُحتسب زاوية البداية نحو الأفق: فكل درجة إضافية تُكلف نحو ٩-١٢ د.ب)إكس-٠٤ وطريقة كولينز(. M1CNK
- وفي الصيف، تكون الإشارة في المتوسط أعلى بحوالي 10 دي بي عما كانت عليه في الشتاء؛ وفي منتصف الظهيرة، يقل حوالي 5 دي بي عما كان عليه في الصباح الباكر أو في المساء. وتشير KK6MC إلى وجود تباين إجمالي قدره 13-15 دي بي مع الوقت والموسم والزمن.
- طريقة يه، أسهل برنامج (و في MHz, d in miles, angle of scatter in degrees, Á geometric average of the openings of the two bundles):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Comparison of Larsen (1968) on 15 real connections: average square error 5,1 dB for Collins, 7,4 for Yeh, 8.1 for NBS and CCIR, 16,8 for Rider. كل الطرق تتحسّن إذا ظلّت (ينز) ثابتة (ينخفض يه إلى 5.4 دي بي): بالنسبة للنشرة، فإنّ رد الفعل في اليوم لا يُحسب إلاّ قليلاً، لذا يجب أن ينظر فهرس التروبو إلى الطبقات، ليس فقط نيس. إن جدول ١٥ وصلة، مع الخسائر المقاسة)بما في ذلك فيلدبرغ - مونتي بالدو، و ٣٢٢ كيلومترا عند ٩٥٧ ميغاهرتز، و ٢٢٣ ١ ميغاهيرتز، وقبرص - اسرائيل، و ٣١٩ كيلومترا عند ٧,٥ ميغاهرتز، و ٣٦١,٣ ميغاهرتز(، هو هيئة اختبار جيدة لحسابنا.
- وتعطي KK6MC الخسارة بأن محطة نموذجية تستطيع أن تتجاوز 2 متراً والمسافة المقابلة، من 184 درناً (FT-817 و3 عناصر في SSB، ونحو 130 كيلومتراً) إلى 215 درناً (أكواب طولها 430 كيلومتراً) و236 درناً (كواو وياغي مقترنة، حوالي 640 كيلومتراً). إنها الطريقة لترجمة الأرقام القياسية إلى أميال مكتسبة
- الرمز: W3SZ Yeh and Collins program (nitehawk.com/w3sz/YehCollins.zip) and Bob Atkins KA1GT calculator (bobatkins.com/radio/troposcatter.html).

Another source on the scatter: I. Roberts ZS6BTE, "Tropospheric Scatter Propagation", *Radio ZS*, December 1984, with note of 2013 ([qsl.net/zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). بشكل رئيسي Useful geometry: angle of scatter À = É0 − / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / R and the same for the other station, R = 8446 km (4/3 of the Earth radius). إنها القطعة التي تُستخدم لحساب الزوايا من لمحة الأرض في "تروسكاتر" Other data: each angle in more costs about 10 dB; passing from Ns 280 to 300 reduces the loss of about 4 dB; the scatter volume is between 300 and 2000 m for 150 km, between 600 and 3000 m for 300 km; Time in the scatter volume raises the signal of about 20 dB. the numerical example of the article does not return with the given formulas (the term 150B should 872 W3SZ

2. Types of conduit (W. R. Hepburn, "Tropospheric Propagation Modes", dxinfocentre.com/propagation/tr-modes.htm, Spanish translation of LU7MC). استكمال جدول بين ودوتون مع قضايا مفيدة في إيطاليا:

| النوع | عندما | ملاحظة بشأن المؤشر |
| الإشعال | ليالي هادئة وسلمية على الأرض | أتبعوا الأرض بعد الفجر بقليل |
| ارتفاع الضغط (التبعية) | anticyclone | يمكن أن تستمر طوال اليوم، في الليل القناة من الإشعال تحت يمكن أن تحجب الإشارات البعيدة، |
| الجبهة | أمام جبهة دافئة، خلف جبهة باردة، شمال واجهة ثابتة | قصير، الطقس السيئ يزعجه |
| المنحدر | الهواء ينزل ويدفئ ويجفف | لمحاولة (بورا) على (أدرياتيك) و(فون) جنوب (ألبس) |
| تحذير | هواء حار وجاف فوق التربة الباردة والرطبة، مثلا بعد المطر | أدى إلى الأرض |
| الوادي | الهواء الساخن فوق الهواء البارد في الوادي | يمكن أن يبقى في اليوم |
| مارينو | هواء ساخن وجاف على الماء البارد؛ يصل على الساحل مع النسيم | الذروة بعد الظهر؛ الربيع والصيف المبكر أفضل المواسم |

ولا تخدم قنوات التبخر في البحر)٠-٣٠ مترا( سوى نحو ٣ جيهرتز ولا تدخل الأراضي الداخلية: بالنسبة ل ١٤٤ و ٤٣٢ ميغاهرتز يمكنك تجاهلها. الجبال المرتفعة حاجز الصحراء جافة جداً

3 The cases Hepburn had not predicted (S. Heck LA0BY, "Extreme Tropo Propagation on 144 MHz and up", Nordic VUSHF 2019, from JO59IX to 500 m of altitude):

- 22 November 2018: 70 cm up to UA6 (about 2600 km) for 6-8 hours. وأشار هيبورن فقط إلى دول البلطيق وبولندا. The conduit was seen in the temperature maps at 900 and 1500 m and ended just above 2000 m; it was higher on the Norwegian side, and only LA0BY on the hill entered. في الوقت الأرضي لم يبدو مثل التراب: بارد وضباب في القمة.
- 11 February 2015: 2 m and 70 cm towards 9A and S5 (about 1500 km) through the Alps, also this not envisaged by Hepburn.
- 15 October 2011: 2 m with EA1 (2135 km) in high duct, on three extensiones of sea and three ground.
- Other events: 6 - 11 November 2003 to the east (2050 km), 10 December 2004 to France.
- عناوين الحقل: ضباب في الذروة، رياح صغيرة، أرض مبللة. درجة الحرارة العادية - 6.5 درجة مئوية/كغم؛ في حالة حقيقية ( VK3KAQ) +7 درجة مئوية في 20 متراً.

النتيجة: Hepburn and F5LEN are not enough, they serve the profiles at altitude along the route. وبالنسبة لنا: هذه التواريخ هي حالات التحقق التي ينبغي إعادة حسابها بالبيانات التاريخية للنماذج، ويجب أن يقارن الرقم القياسي على المسار حصة القناة بحصة المحطات.

4. Italian experience (G. IV3GBO, "Types of VHF propagation SSB/CW", 2017): conducted between 450 and 3000 km, with layers between 450 and 3000 m; fog extended as a clue; openings by sea on the Adriatic towards Puglia, Albania and Greece. Scatter from 100 to 500 km.

5 Introductions (P. Wilton M1CNK, "An Introduction to VHF/UHF Propagation", 2013: 70 cm are often better than 2 m; with 10 m pole at 144 MHz the starting angle is about 3°, with 20 m approximately 1.5°.

6. NASA report (E. Hirschmann, "Tropospheric and Ionospheric Effects on Radio Frequency (VHF-SHF) Communication), Goddard X-731-67-89, 1967): it concerns distance errors to satellites, not the DX. It brings back the same N formula and exponential profile (example in Florida: Ns 370, doN −55, constant 0.161/km) and sends two NBS sources to search: Bean and Thayer, "CRPL Exponential Reference Atmosphere", Monograph 4 (1959), and Bean, Horn and Ozanich, "Climatic Charts and Data of Radio Reactive States.

## 9. من كتاب التردد العالي جدا/التردد العالي جدا، الفصل 2

I. White G3SEK (edited by), *The VHF/UHF DX Book*, DIR Publishing 1995 (ISBN 0 9520468 0 6; digital replica 2017), chapter 2 "VHF/UHF Propagation", pp. 2-13, 2-22-2-29 and 4-11. وهو أقرب نص لتجربة الهواة الإذاعية فيما بين من تم جمعهم؛ ويؤكد العتبات التي استخدمت بالفعل ويضيف قواعد مبدئية جديدة.

العدد

- - نفس صيغة N (77.6 P/T + 3-73-105 e/T2)؛ ومثال ذلك: 1000 hPa, 7 °C, = 10 hPa damage N = 324. In a well-shaped atmosphere, for example after a thunderstorm, N is almost linear of about 39 units/km in the first 2-3 km.
- Gradients between 0 and −79 N/km are normal, below −79 there is super-refraction, over 0 sub-refraction. الكتاب يصر على أن الـ (دي إكس) الطويلة لا تأتي من الإحتقار البسيط الذي يجب أن يبقى على الطريق كله
- سماكة الحد الأدنى، أكثر حذرا قليلا من كير وهيببورن:

| Band | سمك الحد الأدنى |
| 50 ميغاهيرتز |  400 m  |
| 70 ميغاهيرتز |  300 m  |
| 144 ميغاهيرتز |  200 m  |
| 432 MHz |  100 m  |
| 1.3 GHz |  50 m  |

- النسبة ليست متناسبة مع الموجة (حوالي 65 كيلو متر مربع في 50 ميغاهرتز، 220 كيلو في 1.3 جيهرتز) لأن حواف القناة هي "متشردة". In the UK the pipelines are frequent on 144 MHz but almost never at 70 MHz or below, and often the 432 MHz go strong when the 144 are weak.
- In pipeline the signal drops as 1/d instead of 1/d2: up to 1000 km the attenuation can be similar to that of free space.
- Capacity of a minimum station on 2 m (100 W, 15 dBi yagi, 2 dB noise): 217 dB, that is about 400-500 km in SSB in average conditions; in CW about 20 dB in more (p 4-7). وافق على طاولة "إكس كي" KK6MC
- Scatter: The loss rises by about 15 dB by triple the frequency and 10 dB for each degree of scatter angle ( Yeh formula; in the text extracted the constant is unreadable, so it must be taken from the version of W3SZ). تحطم "إب" من الخلايا الملتوية يغطي بشكل روتيني حوالي 500 كيلومتر، 1000 كيلومتر مع محطات جيدة. بعد الأمطار الغزيرة والأوضاع الممددة نادرة على جميع الفرق.

أنواع القنوات

- On the ground: on the earth they propagate badly because the soil absorbs and spreads; on the cool sea arrive far (Scilly-Canarie, 2500 km, about once a year on 2 m). وغالباً ما تكون قنوات التبخر طولها 15 متراً فقط وتخدم الموجات الدقيقة.
- عالية: بين 450 و2000 متر، بسبب الـ "دي إكس" الأطول على الأرض، الإشارة "هاي" فوق المحطات قنوات "الخسائر" تعطي إشارات أقل قوة ولكن أيضاً تسمح بمحطة مسافات متوسطة
- وللكوندوات توجهات دقيقة ويمكنها أن تدوم ساعات أو أياماً، خلافاً للمتفرقة هاء.

New Synoptic Rules for Index

- Age of anticyclone: the air drops to the center of a few meters per hour and the reverse is lowered as the high pressure develops; the greater openings arrive after several days of propagation already good. معاداة قصيرة الأجل ليست كافية
- درجة الضغط المرتفعة: الطبقات هي القبة و الحواف أقل، لذا تتحسّن المحطات.
- التوجيه: It has been observed that the longer routes run along the isobares or through the dorsals (Flavell G3LTP, 1984), but there are excellent connections also in radial direction from the center. Example of 29 -30 August 1987: from England the opening rotated 180° in two days (Spain, France, Italy, Austria, to Norway) following the shift of the anti-cyclone.
- المعارضون: أيزوبير سميك (الرياح المتفشية)، الغيوم المزروعة على حافة الطبقة (حد أعلى تواتر).
- الجبهات: جبهة باردة تدفع الهواء الساخنة وتخلق عكسا على طول الجبهة؛ وأفضل اتجاه على طول الجبهة بينما يمر، ولكن لا يدوم إلا قليلا.
- Season: In the UK anticyclones are more frequent at the end of summer and early autumn.
- الدلائل الافتراضية: الدخان المحصور في خط مظلم فوق الأفق؛ مع ارتفاع الضغط، والطبقات المتراكمة بقاعدة تقل عن 1000 متر تشير إلى قناة أرضية، وقاعدة أعلى من 1000 متر من قناة عالية.

References quoted to search: R. G. Flavell G3LTP, "Studies of an Extensive Anticyclonic Propagation Event"... G3YGF G3BGL W6ZGN

## 10. A verified amateur radio model: Biancomano and Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, &quot; Practical Tropospheric Model for VHF/UHF Propagation Prediction &quot;, *National Weather Digest* 22:2, June 1998, pp. 15-24, free in [PDF from the National Weather Association](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf).

- البيانات: جمعيات إذاعية من طراز 00 و 12 UTC من 47 محطة في شرق الولايات المتحدة، 1995-1997؛ تحليل أرضي إلى 700 هكتار (في البداية حتى 400)، على صورة نون، بدون م.
- بالنسبة لكل دراسة استقصائية: وجود وحصة القناة، أقصى موجة يمكن أن تؤدي (في كثير من الأحيان نحو 100 مرة في الموجة)، أقصى درجات الحرارة المحتملة بين مستويات الجيران وبين مستوى وثاني أعلى (بالنسبة للطبقات الأكثر سمكا). ولا تستخدم القنوات الأرضية.
- القاعدة الأولى، الثبات: مؤشرات الاستقرار (الرقم القياسي المحسن، K، المجموع الكلي، SWEAT) لا تزال داخل 3 وحدات من المسح قبل 12 ساعة، ومستوى الضغط الذي تزداد فيه درجة الحرارة المحتملة أكثر من ذلك. وإذا كان الأمر كذلك، يتوقع حدوث تحسن في الـ 12 ساعة القادمة.
- Second rule, dynamic controls: limit layer that does not rise, CAPE, variation of the fishness to 500 hPa on three surveys, variation of the slope within limits.
- عتبة الضم: قناة قادرة على تحمل 100 سنتيمتر أو أكثر تشير إلى فتح 432 ميغاهيرتز، وبالتأكيد تقريبا على 222 بل وعلى 144 في كثير من الأحيان.
- مرشح الفضاء: محطات على شبكة سداسيّة تبلغ حوالي 400 إلى 500 كيلومتر؛ والتوقعات لا تساوي إلا إذا كانت 5 محطات متقاطعة على الأقل إيجابية (إذا أمكنك تخطي واحدة). مع 5 محطات، احتمال وجود إيجابي عشوائي زائف يصبح لا يُذكر.
- التحقق من أحداث العمود المعنون &quot; العالم أكثر من 50 ميغاهيرتز &quot; من QST: في عام 1996، كان من المقرر عقد 48 حدثا في 67 (71 في المائة)؛ و 12 من الأحداث الرئيسية الـ 15. Accuracy up to 70% on the 432 MHz and about 600% on the 144 MHz at 12 hours; it provides almost double the actual events; 9 errors out of 19 are predictions arrived late, because many improvements form in less than 12 hours.
- أضيفت إلى المحطات في الشمال: تسلق الرياح على مدار الساعة (بداية) كشرط إضافي.

من قوات الدفاع الشعبي الكاملة (التي استكملها مؤلف البرنامج) ثلاثة تفاصيل أخرى. وقاعدة &quot; العصيان = 100 مرة تلويث &quot; تعطي 600 متر لمدة 6 أمتار، 200 متر لمدة 2 متر، و 125 متراً مقابل 125 متراً و70 متراً لـ 70 سنتيمتراً (الشكل 3): بالنسبة لـ 70 سنتيمتراً أقل شدة من كير وهيبورن (نحو 89 متراً)، بالنسبة للمترين المتزامنين مع كتاب VHF/UHF DX. The wind that rotates clockwise goes up, according to the authors, especially because it brings warmer air at altitude, which is what makes the fall of N grow with pressure; the moisture that brings with it does not seem to destabilize. The appendix obtains dN/dh in closed form for a constant thermal gradient atmosphere with a sharp leap of heat gradient and humidity at a given quota: useful as a test case for our code. أثرهم لمنحدر الحرارة المحتملة يأتي من (كي) H. Jehn, The use of potential refractive index in synoptic scale radio meteorology", *J. Meteorology* 17, 1960, p. 264.

ما الذي سنأخذه من أجل مؤشرنا:)١( فترة ثبات: مقارنــة الملامحــة بمــدة ١٢ ساعة قبل ذلك، أنــه في الساعــة المتوقــع أنــا أحــرار؛)٢( مرشــح الفضاء: لا تحصي نقطة إلا إذا كان التحسن يغطي مساحة تبلغ بضع مئات من الكيلومترات، مما يقلل من الإنذارات الكاذبة؛)٣( أرقام قياسية للاستقرار، محسوبة من درجة الحرارة والوزة)٧٠٠٧٠٠٥( h-٥(

مصدر آخر في نفس الرزمة، قليل الفائدة بالنسبة لنا: P. K. Rastogi، (Stratosphere and Troposphere) Studies at Millstone Hill), NASA 1984 (document 19840019069): 440 MHz Radio that measures the turbulence of clear air between 4 and 25 km, with little layers linked to gravity waves and wind cutting. إنه يؤكد أن الإضطراب الذي يُنتج المبعثرة مُغطى بالطبقات، لكنه يتعلق باحتمالات عالية جداً للنقاش.

## 11. الجولة الثالثة من المصادر (20 تشرين الثاني/نوفمبر)

قضايا تاريخية عالية القناة. J. Macoun OK1VR, 2 m from the Summit of Snjižka (1602 m), autumn 1958, article of 1959 translated in 2006 ([ok2kw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)). وفي الفترة من ٢٧ إلى ٢٨ تشرين الأول/أكتوبر ١٩٥٨، كان أول اتصال تشيكوسلوفاكيا - إنغلاند على ١٤٥ ميغاهيرتز)المعيار ١٢٢٠ كيلومترا( وأكثر من ذلك بكثير بين ١٠٠ و ١٢٥٠ كيلومترا، مع إشارات ثابتة بعد ٢٣. وينسبها صاحب البلاغ إلى صيغتين مرجحتين، هما أعلى زهاء 200 2 متر، حيث لا تعود الإشارة إلى الأرض إلا إذا حلت الطبقات الدنيا؛ وكانت الحالة مفضلة بسبب إعصار بطيء من المحيط الأطلسي إلى القارة. ومن المنزل (براغ) كان الحد الأقصى له 530 كيلومترا: ولم تدخل سوى المحطات على ارتفاع القناة. وهي حالة أخرى من حالات التحقق (مثل LA0BY 2018)، وتؤكد أن الرقم القياسي ينبغي أن يبحث عن ما يزيد على 2000 متر وأن يقارنها بنصيب المحطة. G5YV

المكتبات التاريخية

- W. Nupen, *Bibliography on Tropospheric Propagation of Radio Waves*, NBS Technical Note 304, 1965, 356 pages, with index for topics (also "condition forecast" and by country. م. ساككو، &quot; الصلة اللاسلكية بنظام الخرق الفائق في الغلاف الجوي &quot; ، * التردد العالي* ٢٤)٦(، كانون الأول/ديسمبر ١٩٥٥)البحوث الإيطالية: التجاوزات الكبرى تحت ٥٠٠ متر في اليوم والليل(؛ S. Silleni، *Annali di Geofisica* ٨)١(، ١٩٥٥؛ J. M. Cltric e A.
- A. L. Abbott, *Bibliography of Tropospheric Radio Wave Scattering*, NBS, material until January 1960, about 670 items ordered per year: useful for the troposcatter part.

Disclosure and observations: F. W. Odenthal DK5EQ, "Ausbreitung 2m – Eine Einführung" (31/05/2024): Almost optical up to 200 km, tropo per duct or refraction beyond, with high pressure and in spring and autumn; cites the VHF page of DM2HB and the notes of DK5YA on tropo and Es. Discussion on VHF marina and AIS ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)): scope 1,23 √(ft) nautical miles per antenna, scatter up to about 50 nm, typical trope improvement 100-400 nm; the conduits are recognized because the remote stations arrive as strong as the neighbours. Chelmsford ARS News, December 1983: in the contests in 2 m the stations of the English Eastern coast are favoured by the sea route to the continent. VK3DXE EI7GL

ARRL, *The Radio Amateur VHF Manual*, 1972, cap. 2 "Propagation beyond the Horizon" (pp. 17-19). وبقايا التشغيل المعروفة بالفعل (تحت مناطق الضغط المرتفعة التي تتحرك ببطء وفوق كل حوافها الخلفية؛ في الخريف المبكر لأفضل فترة؛ والمناطق الساحلية والبحيرات الكبيرة ووادي الأنهار التي يفضلها التضخيم). Two new data: (1) fig. 2-4 يقارن الغلاف الجوي المعياري (الرطوبة النسبية 70 في المائة إلى 12000 رطب، الحد الأدنى للانتقام) مع نطاق نموذجي للانتقام القوي، مع نسبة واضحة من درجة الحرارة وزيج تبلغ نحو 4000 رطب (حوالي 1200 متر): إن التوقيع على حسابنا يجب أن يعترف به على مستويات النظام العالمي لرصد الغلاف الجوي؛ (2) أن تكون القنوات الحقيقية نادرة تحت 144 ميغاهرتز وأكثرها شيوعا في التردد العالي جدا. الببليوغرافيا ذات الجودة العالية المستخدمة للبحث: Hull, "Air-Mass Conditions and the Bending of UHF Waves" (June 1935, May 1937); Hoisington, Painless Prediction of Two-Meter Openings) (October 1949); Collier, "Upper-Air Conditions for 2-Meter DX" (September 1955); Botts, A night to remember) conducted (January).

PA0EHG, page "Tropo" ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): summary of the troposcatter from the article of G3YGF, already in the manual. استخدام قاعدتين عمليتين لمراقبة حسابنا المتناثر: حوالي 10 كيلوغرامات إضافية لكل زاوية في أفق كل محطة، وأكثر من 100 كيلومتر تقريبا 9 باء زائد كل 100 كيلومتر (60 ميلا)؛ ويبدأ الحجم المشترك من حوالي 600 متر على 100 كيلومتر ومن حوالي 9 كيلومترات على 480 كيلومترا. وهو لا يعالج الخناق أو الاقتراعات أو العتبات الرجعية.

PA0EHG, PROPBER calculator ([page](https://pa0ehg.com/propagat1.htm), propber.exe program provided by the author of the program). It is a 36 kB DOS program written in BASIC and compiled with the Microsoft BASIC Compiler 5.60 (1985): it only runs under DOS or DOSBox. From the internal texts: asks for frequency, distance (or the two locators, also in the old QRA format "E-locator"), for each antenna altitude station, noise figure, FI band, power, an obstacle (height and distance) and antenna gain (0 = parable diameter calculation, efficiency 50%). يَعُودُ أقصى مسافة في البصرياتِ، الخسارة البصرية الصحيحة لاستيعاب البخارِ (0.1 dB/km مِنْ 24 GHz)، فقدان تروسكاتر "صحيح للمواقع" (أي لزوايا الأفق التي تعطيها العقبات) ونسبة S/N في المنطقتين، أيضاً "مع أقصى انتشار غير طبيعي". والثبات العددية غير قابلة للقراءة في عملية التنفيذ المجمعة. والطريقة المقترحة للضمادات العالية مفيدة أيضا بالنسبة لنا: معايرة الحساب على الرابط المقاس (التفاوت بين S/N) المقيسة والمحتسبة وتطبيق نفس الفرق على مجموعة أخرى. إنه نفس النمط في حسابنا، لكنه يستعمل ملف S RTM1 بدلا من مجرد عقبة واحدة وتوقعات GFS بدلا من هامش ثابت.

ProPBER, constants read in the executable (numbers in Microsoft Binary Format format). 0r, in the data table next to the texts: 8,68589 (= 20/ln 10, i.e. 20 logs calculated with the natural logarithm), 32.5 (free space: 32.5 + 20 log f/[MHz] + 20 log d/[km]), 24000 and 0.1 plus 40000 and 0.3 (vapore: 0.1 dB/km from 24 GHz.3 محتمل، لأنّك تُكرّرُ لكن داخل الشفرةِ: - 69،5 (نفس المصطلحِ الثابتِ كصيغةِ ييه الجانبيةِ في VHF/UHF DX Book)، 332 (إشعاعِ التربةِ)، 8733 كيلومتراً (إشعاع معادلةِ، k œ 1-37). لا يمكن إعادة بنائها دون تفكك البرنامج T0

PROPBER, formulas reconstructed by the code (disassembly of the compiled part; the operations are called to the BASIC library with the operators in memory, whose values in double precision have been decoded). The constants −69,5, 332 and 8733 found earlier were false positives within the library.

- المسافة البصرية: d = 5 · (√h1 + √h2) km (h in m). وهو أكثر سخاء من الـ 412 الكلاسيكية (ك = 4/3): يعادل الـ 2 ك.
- Loss in optics: L = 32.5 + 20 log f/[MHz] + 20 log d/[k/], plus 0.1 dB/km above 24 GHz and 0.3 dB/km above 40 GHz.
- Troposcatter: Lts = L + 23 + 10 log f + 0.0674 -d, i.e. 55.5 + 30 log + 20 log d + 0.0674 -d (formula of G3YGF with the constant 2 dB higher). لا حدود أو ردة فعل
- تصحيح "المواقع" البرنامج يسأل عن العقبة والمسافة لكن العقبة التي يجب أن تستخدمها فارغة ومتغير التصحيح يبقى صفراً
- Noise: T = 290-(10(NF/10) − 1), N = − 228,6 + 10 log T + 10 log B (dBW), without antenna temperature.
- S/N = 10 logs P + G/Tx − L + G/_rx − N. " With maximum abnormal propagation" uses optical loss instead of scatter, i.e. the ideal conduit case without losses beyond free space.

Comparison: 300 km and 144 MHz PROPBER gives about 189,9 dB, G3YGF 187,9, our P.452 187,8 (with zero horizon angles): the scatter coincides within 2 dB. حسابنا يضيف زوايا الأفق من الملامح، درجة الحرارة الهوائيّة، وبدلاً من "ماكسيموم" أصلحت الطبقة المتوقّعة مع ما تبقى من التزاوج والتخفيف.

Rohde &quot; Schwarz, P. Denisowski, "Understanding VHF Propagation", Educational Note, April 2023 (24 pages, dissemination). يؤكد النقاط التي جُمعت بالفعل (التي اختُطفت على شكل موجة موجية ثنائية الأبعاد، وهي إشارة تنخفض بشكل خطي بمسافة تصل إلى 1500 كيلومتر وما بعدها، من ساعات إلى أيام؛ وأرض جيدة على البحر ونادرة على الأرض؛ وترتفع من بضع مئات متر إلى بضعة كيلومترات). ملاحظتان مفيدتان للرقم القياسي: (1) التطور في الوقت: كثيراً ما تكون القناة المنشأة حديثاً أعلى ترددات، وتبدأ السميكات في تحقيق أقل ما يمكن؛ ولذلك ينبغي للتوقعات في الساعة أن تبين الـ 432 ميغاهيرتز "متقدماً" على الـ 144 ميغاهيرتز عندما تنمو الطبقات؛ (2) موقع المحطات: أفضل الظروف بين محطات قريبة من نهايات الحلبة، في حين تفشل تلك التي تدور على الطريق المرتفع القناة تتحرك مع الطقس في الغلاف الجوي

Site PA0EHG, other pages. For the tropo, PathProfile of M. Willis G0MJW ([scheda](https://pa0ehg.com/srtmp.htm), [download](http://www.mike-willis.com/Software/ASTERPathprofile.zip)]: profile of the route from expected total loss and quota files, judged reliable for the troposcatter in microwave; does not treat the scatter from planes or rain. مفيد كإشارة ثانية لمقارنة مواصفاتنا والخسائر أما بقية الموقع فيشمل EME, microwave, airstrip (AirScout, SM7LCB maps) والتدابير في 80 مترا. RTM1

PathProfile by G0MJW (Mike Willis), ASTER and SRTM packages and SiteData (supplied by the author of the program; [](https://www.mike-willis.com/software.html)software page]. Windows program of 2018: profile from ASTER or SRTM to 1) or 3), losses in optics, diffraction, troposcatter (valid up to 1000 km), duct and gaseous, with percentage of time, k median of the route, fraction of path on the sea and distance from the coast: the structure is that of P.452/P.1812, but the help (compressed). SiteData converts the locators and gives the share from GT OPO30.

والجزء الأكثر فائدة هو ملفات البيانات التي شملت: الخرائط الإحصائية للاتحاد الدولي للاتصالات - R P.453 على الشبكة العالمية التي تبلغ 5 درجات (121 صفاً من 90 درجة شمالاً إلى 90 درجة شرقاً، و 241 عموداً من صفر درجة إلى شرقاً): تدرج الحمض النووي/الدز في الـ 65 متراً من أجل 1 و10 و50 و90 و99 في المائة من الوقت؛ وبالنسبة لنسبة وجود التربة وسمكها وكثتها؛ ودرجة الحرارة العالية، ونسبة مئوية. القيم السنوية المستقاة:

| الموقع | خط أنابيب الأراضي:٪/السمك/الكثافة | خط أنابيب مرتفع:٪/قاعدة/سمك/كثافة | (د) تجاوزت نسبة 10 في المائة من الوقت |
| نابولي |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | −380 N/km |
| تايرينيان (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | −674 N/km |
| Trapani |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | −520 N/km |
| برينديزي |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | −405 N/km |
| Adriatic (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | −399 N/km |
| ميلان |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | −172 N/km |
| أودين |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | −176 N/km |

بالنسبة لمؤشرنا هو مرجع مناخي جاهز: سموكة وشدة نمطية من القنوات بدلا من 50 متر ثابت من القناة المحتملة، ITU data: free for non-commercial use, to check before including them in a public version. ملف ZO NES01.DEM (58 MB، شبكة 2) يحتوي على المناطق المناخية داخل الأراضي/الكوست/البحر؛ والشكل غير موثق وأنا لم أفك شفرته. صفحة (ويليس) للمعادلات تحتوي فقط على ثابتات مادية و مساحة مجانية و مكسب هوائي

الجولة الرابعة من المصادر (20 تشرين الثاني/نوفمبر).

Indermuehle and Suzuki 2026, prediction of the GFS pipelines for the CSIRO observatory of Murchison (arXiv 2608.12861): it is the closest source to LIVETROPOVHF. Profiles of N from the GFS to 0.25° (surface to 2 m, then the isobaric levels, those under the discarded ground); for each minimum column of dM/dz under 3 km, base and top of the conduit, integrated M deficit; then P.452-16 and P.1812-6 with GLO-90 25 routes from 98 to 1005 km, from 88 MHz to 2,68H Results: Median AUC 0,72 on 25 channels; the P.452 p time percentage should be used as a calibration constant (choice p = 20%), because the ITU model with an moment loN is not defined within a conduit; skills remains almost flat until 3 days in advance. At 162 MHz (AIS) the received routes have the highest conduit fraction of 67% (0.26 against 0.16) and the highest duct depth of 49% (28 vs 19 M) with respect to controls: on the sea the conduit is almost always, decides the extension of land. The GFS sees half the time on the ground compared to ERA5 (4.5% against 10.4% of the cells-hours), while on the sea sees more (73% against 56%). فهرس "المرح" (خطوط الأنابيب المستمرة على طول الطريق، طبقات ذات سماكة دنيا تبلغ 40 متر) يحسن النتيجة:

Longley and Rice, ESSA ERL 79-ITS 67 (1968): method for irregular terrain from 20 MHz to 20 GHz (after ITM), with soil irregularities h, Ns from 250 to 400, equivalent radius 8497 km (4/3), horizons, diffraction, scatter forward and variability in time and in places for climate. مرجع إحصائي، ليس فوريا.

ITU-R P.834-2: radius curvature, Earth radius equivalent k;a, M = N + h/a (h in m, in thousands of km), exponential model; nothing new than what already used.

AGARD CP-70 (1970), tropopheric propagation: little layers from 1 to 100 m with strong gradients are common; the reflection coefficient of a high layer is about proportional to the wavelength, with a random dispersion of about 14 dB. التدابير الرامية إلى 90 ميغاهيرتز: نقصان قدره 9-10 ميغاهيرتز مرتبط بقفزة نون تركز على نحو 350 مترا، على قمة طبقة من الغيوم. إن مرور جبهة باردة يزيل الطبقات: ما زال الانتشار.

U.S. URSI Report, Commission 2 (1960): average fishness of the 60 to 210 m high layers (given consistent with P.453), minimum gradient for the 157-160 N/km duct.

Buscemi K2OVS, VHF propagation guide: typical distances for reversal and duct for band, ducts linked to high stationary pressure, maximum at emerged.

Wolinsky-Mancini and 2024, AIS in the Canary Islands: AIS radio flooding and receptions; the ducts are much more frequent in the warm months (inversion of the aliseans).

Newn and Malaysian articles (Ekah, Iloke, Mat, Igwe): local correlations between temperature, humidity and measured loss, weak and contradictory (r −0,09 to 0.27); useful only as confirmation that the ground sizes alone are not enough.

Herben 1984, thesis TU Eindhoven (microonde): 30 GHz on 8.2 km and OTS satellite at 11.6 GHz; sparkle for turbulence, multi-course and rain. وفيما يتعلق بالخسائر الدينامية للموجات الدقيقة تهيمن عليها الأمطار والاضطراب، وليس بالنواقل.

Baars, The Paraboloidal Reflector Antenna (microonde): gain of a parable G = · (SouthD/)2 with typical efficiency–0,65; loss of surface errors according to Ruze, thes = exp(−(((4, el e/)2). ومن شأن ذلك أن يساعد إذا ما أضفنا المصابيح كبديل لـ ياجي DL6WU لـ 1296 ميغاهيرتز وما بعده.

تم تحليله بالفعل أو لمحتوى ضعيف: هيرشمان ناسا X-731-67-89 (دوبل)، رادار ريد ذو ترددات عالية جداً (الرياح والاضطرابات، غير المختطفة)، الشريحة المزروعة والملاحظات العامة على الغلاف الجوي.

الجولة الخامسة من المصادر (20 تشرين الثاني/نوفمبر، مساء).

Rogers 1997, Radio Science 32(1): Bayesian estimate of the parameters of a high duct (base rate, M deficit, fishness) from propagation factors measured beyond the horizon, with a parabolic equation model; VOCAR experiment in California at 143, 263 and 375 MHz, comparison with radio wave. The signal depends a lot on the base and the M deficit (correcting 0,81 to 143 MHz for the deficit), almost at all from the fishness (−0,17): the fishness cannot be obtained from the signal. بالنسبة لنا: الحساب باء (السمكة الجلدية) يحسب أقل من العجز والنصيب؛ والطريقة هي أساس المكتب الخلفي، أي الحصول على القناة من الترددات المتعددة.

Moler and Holden 1960, "Tropospheric scatter propagation and atmospheric circulations", J. Res. NBS 64D, pp. 81 and following (original on 22/11/2026, doi 10.6028/jres.064d.014). مسارات بحر كاليفورنيا إلى نقطة لوما (سان دييغو): Santa Barbara 190 ميلا، بوينت موغو 144، سان كليمنتي 78؛ 1365 ميغاهيرتز و 9380 ميغاهيرتز، ليس بالتردد العالي جدا؛ كانون الثاني/يناير - نيسان/أبريل 1958، متوسطه دقيقة واحدة. The case of 26 March: a reinforcement of about 20 dB for about 3 hours towards noon, when a cyclonic vortex weakens and begin sea breeze and subsidence; at 15 the convergence is reformed and the signal decreases by about 22 dB from the maximum. الإحصاءات (الجدول 1، 500 ح ب أ، 33 حالة): مع الهواء الذي يسقط الإشارة المتوسطة يكون أعلى من 2.5 دبليوني تقريباً بالمقارنة مع ارتفاع الهواء، في قطاعي L و X. وتتبع الإشارة الفرق المحسوب إلى 500 هكتار في الإشارة وكمية التباينات، دون معامل ارتباط. وتفضّل الطبقات المتدنية (تحت حوالي 600 متر) التفكّر الجزئي في التفريق. لا تُعالج الأعاصير والجبهات، و "التجاوزات التي تصل إلى 60 دي بي" المشار إليها أعلاه ليست: وبالنسبة لنا: فالحركة الرأسية هي تنبؤ جيد، و+1.5 DB للهبوط و -2 DB للهواء المرتفع من نفس ترتيب الفرق المقاس المتوسط (2.5 دي بي)؛ و 20 دي بي هي حالة واحدة، وليس قيمة نموذجية، و على الموجات الدقيقة.

Banafaa and Muqaibel 2025, IEE Access: survey of the pipelines (evaporation models, parabolic equation, radio estimates, GNSS and lidar) and classification with automatic learning on real measures; Support vector machines give the best results. مفيدة كبليوغرافيا البيانات من الخليج الفارسي

ITU-R P.834-7 (2015): الصيغة المستكملة للتوصية المتعلقة بالانتقام العرضي؛ لا توجد صيغة جديدة عن الصيغة المستخدمة بالفعل.

Abbot, NBS bibliography on the troposferous scatter (GOVPUB- C13): already in the third round. Slide Scribd on the ducts: general content. The article ScienceDirect 0021916987900304 لا يمكن فتحه

الجولة السادسة (20 تشرين الثاني/نوفمبر، ليلا): بين ودوتون، كتيب ARRL 2022 و 2023، بالانس.

Bean and Dutton, Radio Meteorology (cap. 6), parts not yet used. الحساسية الميدانية للناقصات: 0.18 دي بي لكل وحدة نون في الدورة السنوية؛ 0.14-0،24 دي بي/ن في الدورة النهارية (في وقت مبكر من الصباح) حتى 0.2-1، 1 د.ب/ن على طرق 175-200 كم في الشتاء؛ التصويبات 0.8-0.95. Parameter composed of Misme: M = a(g/_e − 40 + b] dWn) with a = 0,5 dB per N/km, which combines the equivalent gradient and thermal stability desW (work to lift the air of 1 km): in the Sahara at 471 MHz on 160 km the only stability is 20 dB between January and June at almost equal gradient. الرقم القياسي للانتعاش المحتمل K (N reported adiabatically to 1000 hPa): يسلط الضوء على كتل الهواء وضواحي مضادات التقلبات الدورية (Flavell and Lane).

ARRL Handbook 2022 (99a ed.), § ونادراً ما تكون عمليات التحويل المفيدة بين 250 و2000 متراً؛ ونادراً ما تكون تحويلات الإشعاع (الميرا، والهواء الهادئ، والتربة الجافة) صحيحة؛ وتعطي الأعاصير البطيئة أشمل قنوات التخريب، مع تحويلات تتراوح بين 500 و3000 متر، أقوى في المساء وفي الصباح؛ وفي نصف الكرة الشمالي، تقع أفضل الطرق إلى الجنوب من المركز الرفيع. جبهة ساخنة: ظروف جيدة أمامها بضع مئات من الكيلومترات؛ جبهة باردة: عكس قوي غير مستقر خلف الجبهة، موازية. Foehn (also in the Alps) and earth breeze (cool air on the ground, hot at 200-300 m; extendes over 800 km along the coast). Scirocco: hot and dry air from Sahara over cooler marine air, very strong inversions from the eastern Mediterranean to Gibraltar, probable cause of records over 1500 km in microwave. Troposcatter: theoretical limit about 800 km, maximum typical half. The 100th ed. (2023) has a shorter text: inversions at sunrise and sunset up to +20 dB and +20–50% flow rate; 1200 miles on the Great Lakes, typical 400-600; recommends Radio Mobile (ITM model) for Hepburn coverage and maps.

Balanis, Antenna Theory (4a ed.), § 10.3: Viezbicke table (NBS TN 688, 1976) for Yagi optimized at 400 MHz: boom 0.4 / 0.8 / 1.2 / 3.2 / 4.2 / 4.2 / 4.2 / 4.2 / 4.2 / 9,1 / 9,2 / 10.2 / 12.25 With the correct formula on the original (9.4 + 7.8 log10 boom/PEC, from 22 November) the discard falls below 0.2 dB between 1.2 and 4.2 cho (12.1 dBd to 2.2 М against 12.25) and remains 0.6 dB to 0.8 М. DL6WU DL6WU

الجولة السابعة (تشرين الثاني/نوفمبر 21): QST, QEX, W3SZ, GitHub repository.

QST 2000-2025 (14 رقما، و 10 أسرة مع الاعتراف بالنص). Only reports on The World Above 50 MHz, useful as cases to validate the model: September 5-9 2000, conducted at 144 MHz and beyond on the eastern half of the USA; 14-15 August 2022, opening Manitoba-Kansas/Missouri under high pressure on a hot day, from 144 to 1296 MHz almost everything in FT8, Canadian record at 10 GHz of 1228Q1 km. QST November 2022 recommends Hepburn maps. لا توجد مقالة تقنية على توقعات التراب VE4MA N0OY Q65

QEX and K1JT, Franke and Taylor (QEX 2020) on FT4/FT8 modes: FT8 decoding thresholds −20,8 dB in 2500 Hz on channel without evanescence, −18,6 dB with evanescence at medium latitudes; FT4 −17,5 dB. The other numbers of QEX 2023 and 2026 do not treat the tropo (three numbers 2023 are unread scans). W3SZ 2019: slow mode thresholds ( JT4A −23, JT9A −27, JT65A −25, QRA64A −26, FT8 −21 dB) and choice criteria between sensitivity and frequency shift tolerance.

- GitHub. TropoLink (LukiBox, Apache-2.0): design of troposcatter routes with P.617-5 (with N0 and DIN), NBS TN101 and ITM flanked, and their difference shown as uncertainty; P.617-5: L + 22 log f + 35 log + 17 log d + Lc, F = 0.18 N0 andi-f/troposcatter ( DL2FZN 2013, from VHF/UHF Handbook RSGB): Lts = 24,44 + 4,283 ln f + 0.06656 d above free space, slope near PROPBER (0,0674 dB/km). QRadioPredict: ITM and ITWOM 3.0 with S RTM3, soil use Corins and NEC diagrams. Not relevant: Tropos ( Weather app), opera-adt/tropo and troposim (Troposferic delay for InSAR), tgbot/_troposcatter (military station), sdr-troposcatter-gui (control panel), Tropostter-Propagation-Delay ( ERA5 nel delay), Radio-Communication-Project (didactic simulator).

الصف الثامن (21 تشرين الثاني/نوفمبر): نماذج NTIA والاتحاد الدولي للاتصالات السلكية واللاسلكية في غيت هوب. NTIA/itm (Longley-Rice, ITS): 20 MHz-20 GHz, free space loss, diffraction and troposcatter with soil profile, 0.5–3000 m terminals, surface reactivity N0 (minimity reduced to sea level, 250-400), seven climates, soil constants and various modes 1 view, 2 diffraction, 3 troposcatter; C+ إنه لا يُعدّل النوافذ، بل يُستخدم كمرجع "جو طبيعي" مع التقلب الإحصائي. NTIA/p528 and eeveetza/p528 (MATLAB): ITU-R P.528-5 for air-soil connections, 100 MHz-30 GHz, 1.5–20 000 m terminals, p 1-99%;.NET 4.8 casing. الاستخدام المحتمل: رصيد المفرقعات الجوية)مساران أرضيان - طائرة بالإضافة إلى قسم الرادار(. eeveetza/p2001 (MATLAB): ITU-R P.2001-6, all-field model with diffraction, troposcatter, abnormal propagation (conduct and layers, Lba) and E sporadic, percentage of time 0-100%; ITU digital maps (DN_Median, DN/SupSlope, DN/Subz يمكن استخدامه كمقارنة مع الاتحاد الدولي للاتصالات بدلا من P.452، مع النظام العالمي لمراقبة فض الاشتباك بدلا من الخرائط كما هو الحال في دراسة CSIRO؛ وينبغي أن يقوم المستعمل بتنزيل الخرائط حسب P.453. Validation 1.3.0 out of 593 QSO (29/12/2021-2/1/2022): event/comparison separation 0,79 without corrections, 0,91 with Misme and subsidence; the GFS does not see the conduit on the sea at 144 MHz (guadagno without corrections never more than 15 dB).

الجولة التاسعة (تشرين الثاني/نوفمبر 21): IU3QEZ, NTIA, PyWaveProp, ACE High, validation 1.3.1. IU3QEZ (S. Fabris), presentation &quot;ver the horizon in VHF &quot; (GEKO Magazine n. 69, September 2026): common volume d2/(8);R/R/R_B_eff) with R/697)١( المرجع نفسه، المرفق الأول، المرفق الأول، المرفق الأول، المرفق الأول، المرفق الأول، المرفق الأول. PyWaveProp (Python): Parabolic equation methods with Padé approximations, multiple edge diffraction, PETOOL and RAM enclosures, troposferous and high ducts, irregular soil: possible base for a field calculation with the GFS M(z profile, to be check. Apache 2.0 VHF propagation maps from APRS packages, hourly statistics and opening alarms; possible source of 144 MHz data for validation. NATO troposcatter network in Italy (fortifications.net): 900 MHz, 10 kW, 20 m parables; stations including Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Vergine, Monte Vulture, Monte Lauro; historical test case for P.617. Not relevant: Gupta 2021 ( stratospheric transport), Law 2023 (Arctic ozone), dolphin #590 and opera-adt/tropo (InSAR delay), tropo.ino (Arduino gas sensors), tropo-webapi (telefonia), cloudtools/troposphere (AWS). Validation 1.3.1 on 564 QSO tropo: AUC 0,79 (model 1.2), 0,80 (+ soft duct), 0,90 (+ misme and subsidence), 0,90 (+ persistent); conduit colors (XVB) on 46% of opening routes and 0% of comparison. IQ3QC DL2ALF

أضيف إلى نفس الجولة mikelytaev/wave-propagation (MIT, Python 3): Parabolic equation with approximates of Padé and Numerov, irregular soil, tropospherical ducts; charge the modified refractive M( &quot;, lat, lon) from the NOAA GFS and set a evaporation conduit of Monin-Obukhov documented; وهو أفضل مرشح لإجراء حساب ميداني في المستقبل مع موجز بيانات نظام الأفضليات المعمم (سيخدم خدمة خارجية أو ترجمة). RP-Sim (AGPL-3.0, JavaScript): نماذج إمبراطورية للشبكات الإذاعية (هاتا، هو، إريكسون، شعاعين)، لا تربو. RIMFading (GPL-3.0, OMNeT++): flow irregularities for network simulations, Weibull distribution, no tropo. PyLayers: indoor radio channel, already excluded. qradiopredict (kantooon, GPL v2): ITM and ITWOM v3.0 with S RTM3 and Corine, already in the seventh round as QRadioPredict.

الجولة العاشرة (تشرين الثاني/نوفمبر 21): كتاب هوائي، نورتون، بارسيس، مصادر بيانات جديدة. ARRL Antenna Book (23a ed.), cap 4 ( K9LA): طريقة براي (QST November 1961) بالنسبة لنطاق التردد العالي جدا: خسارة أرضية سلسة إلى 99 في المائة (300 ميل، 144 ميغاهيرتز: 214 دب)، منحنى إلى 50 في المائة و99 في المائة (الشكل 4-11): 195 دي بي يساوي 100 ميل إلى 99 في المائة، وأكثر من 250 ميلا إلى 50 في المائة؛ التهرب + 7 دي بي أكثر من 100 ميل Sunrise and sunset up to +20 dB and +20-50% flow rate; hot and dry air over wet fresh air on the Great Lakes up to 1200 miles, typical 400-600; regions influenced by large water masses the most favored; late winter with heating capable of strong inversions. التحقق من تروسكاترنا: ف-452 إلى 50 في المائة عند 483 كيلومتراً و144 ميغاهيرتز يعطي حوالي 204 دي بي، بما يتسق مع الـ 214 دي بي من براي إلى 99 في المائة، نورتون 1959 (J. Res. NBS 63D، مجاناً على NIST): الخسارة الأساسية والنظام وكسب الهوائي؛ Ns = 301; equivalent radius from Ns (250. 4878 miles, 301 → 5280, 400 → 6997); L(99) tables with 30- feet antennas (100 MHz: 163 dB to 100 miles, 208 dB to 500 miles; 1000 MHz: 138 and 188 dB with 28- feet parables); long-term variability V(p, angle) Rice, Longley and Norton, NBS TN 15 (1959), free on NIST. Barsis and Kirby, NBS Report 6751 (1961): 223 km in Colorado with Pikes Peak (4292 m) as edge, 100 and 751 MHz; consistent loss with the rounded edge (approximately 11.5 km radius); long-term evanescence 11,7-14,3 dB against 39–42 dB of a scatters of same length; no day cycle measured ARRL VHF Digital Handbook: package, APRS, D-STAR, WSJT (Scatter Method and EME), no part on the tropo. New proven or searched data sources: ECMWF open data (IFS 0.25°, levels 1000, 925 850, 700 hPa... without 975, 990, 900 and 800; G1Q with CCSDS compression, that our reader does not decode; already available through Open-Meteo); DWD ICON-EU open data (G RIB2 compressed bz2) RIB2

الجولة الحادية عشرة (تشرين الثاني/نوفمبر 21): Longley-Rice, GEKO 69. Bean and Thayer, "Models of the Atmospheric Radio Refractive Index", Proc. IRE May 1959 (pp. 740 et seq.): ويتمتع النموذج 4/3 بالمنحدر الصحيح في الكيلومترات الأولى، ولكنه ينخفض أكثر من اللازم؛ ويحدث 30 في المائة من انحسار نصف قطره في أول 100 متر و60 في المائة في الكيلومترات الأولى؛ ويحدث متوسط العلاقة بين متوسط الفارق بين الطول الأول Ns and decrease in the first km: −N = 7,32; e(0,005577 Ns) (Ns = 313. Δ lo −42). Bean and Thayer, "C.R.P.L. Exponential Reference Atmosphere", J. Res. NBS 63D n. 3 (1959): N(h) = Ns;e(−c); c fixed by Ns through the same report; average Ns USA 313; beam tracking tables (total curve 15.2 mrad with Earth exponential model against 30/3) MATLAB refractionexp: c = 0.143859 km - 1 for Ns = 313 (scale about 6.95 km), valid above about 1 km and 10 mrad. (ب) استخدام لنا: يمكن أن تكون مواصفات المقارنة &quot; الطبيعية &quot; أن CRPL مع الناموسيات المحلية بدلاً من م = 315 + 0.118؛ وأن يقاس الشذوذ في النقصان في الكيلومترات الأولى مقارنة بـ 0.7,32 (005577) شمالاً بدلاً من أن يُثبت −40. NTIA Report 82-100 (Hufford, Longley, Kissick 1982), ITM guide in area mode: climi and Ns suggested (equatorial 360, subtropical continental 320, subtropical marine 370, desert 280, temperate continental 301, temperate marine on land 320, temperate marine on sea 350); Nction = N0 ·e(−hs/9,46 km) Rice, Longley and Norton, NBS TN 15 (1959), part I: forecasting formula of distribution over time by land and tropospheric wave, base of TN 101. برمجيات، ويكيبيديا، وألتير وينبروب، و MATLAB. LongleyRice: same information (version 1.2.2, 20 MHz-20 GHz or 40 GHz, 1–2000 km, Ns 301, seven climates, variability of time, place and situation); no one shapes the ducts, only the statistical effect of the marine climates (very higher fields for 10% of the time or not). SKO Magazine n. 69 ( IU3QEZ): radio horizon 4/3 of 130 km from 1000 m, 260 km add two stations; troposcatter with common volume between 1-3 km and 10 km; scatter from aircraft to 10 km with +10-20 dB for tens of seconds or a couple of minutes; theoretical courses with 2 x10 elements per side: SSB QRP about 440 km

## 12. اجتماع المائدة المستديرة: مصادر للتحقق من النموذج أو تصحيحه (تشرين الثاني/نوفمبر 21)

Summary of all turn of sources, ordered for use. "الدولة" تقول إذا كان المصدر موجود بالفعل في البرنامج (1.3.1) سواء كان في الاختبار أو ما إذا كان يجب القيام به.

### بيانات للتحقق (ما حدث فعلا)

| المصدر | ماذا؟ | الاستخدام | الدولة |
| MMonVHF reports from the author | 593 QSO at 144 MHz, 29/12/2021-2/1/2022, time and locators | المصادقة على الحدث مقارنة (+14 يوما) | مستعمل: |
| The World Above 50 MHz | الحالات الموثقة)١٤-١٥/٨/٢٠٢٢ مانيتوبا - كانساس، ١٤٤-١٠ غيغاهيرتز( | الحالة التاريخية | مستخدماً كحالة تاريخية؛ يضاف إلى المصادقة |
| wspr.live (WSPR) | محفوظات كاملة، 2 متر و 70 سم، SQL | العديد من الأحداث والكثير من "لا" (الساعات بدون مكان) | وينبغي أن يُجرَب، بل ينبغي أن يُلطَّخ (بُقع مزورة بـ 16 إلى 20 ألف كيلومتر) |
| APRS PropView / APRS-IS | محطات تشعر، مسافات، ساعات | التصديق فقط 144 ميغاهيرتز | القيام به |
| البيكون العكسي (الفوضى) | يومية | VHF | مهجورة الآن |
| I GRA2 radiosondaggi (Brindisi 16320, Practice of Mare 16245, Trapani 16429, Udine, Milan) | ملامح قياسية تبلغ 00 و 12 ميغاواط | مراقبة البيانات المالية العالمية والتحويلات | (Brindisi 46 MB), to be used |
| Barsis and Kirby 1961, Barsis 1963 | evanescence measured on paths with mountainous obstacle | تحقق من فئة "المونت" (فيسوفيو، فييتو) | القيام به |
| Norton 1959, NBS TN 15, TN 101 | L(99) and table media losses | التحكم في النفايات | جزئيا (انظر 300 ميل ثابت) |

### التصويبات المقترحة من المصادر

| تصحيح | المصادر | الأثر المقيس على التصديق | الدولة |
| Misme Stability مدة العضوية | قبعة (بين) و(دوتون) 6 | AU 0,79 → 0,90 جنبا إلى جنب مع التخفيض | 1.3.1 (Title 16 dB without conduit) |
| التخلف عن الحركة العمودية | Moler and Holden 1960 (read in original on 22/11: +2.5 dB average between air coming down and air rising, 1.4 and 9,4 GHz by the sea) | أعلاه | in 1.3.1 (+1.5 /-2 dB) |
| خط الأنابيب الطفيفة (الارتفاع إلى 3 درجات مئوية) | Kerr, Hepburn, VHF/UHF DX Book, Shi 2019 | (ب) +1 في المائة من مفوضية الاتحاد الأفريقي؛ وأحضر ألوان القناة على 46 في المائة من مسارات الحدث و0 في المائة من المقارنة | في |
| الثبات 24 h | VHF/UHF DX Book, Biancomano and Leyshon | الاتحاد الأفريقي لم يطرأ عليه أي تغيير، بل أُطلق عليه إنذارات كاذبة | 1-3-1 |
| Normal CRPL profile with local Ns (−N = 7,32);e(0,005577 Ns) instead of 40 N/km fixed | Bean and Thayer 1959, NBS Monograph 4 | تم اختبارها في المصادقة | في ١-٣-٢ كبديل، بسبب العيوب |
| عتبة &quot; الخصم &quot; ٧٩ ن/كم)الاتحاد الدولي للاتصالات( بدلا من ١٠٠)البحرين والدتون، الطبقة ١٠٠٠-٨٥٠( | P.453, Lopez 2008, DX Book | تم اختبارها في المصادقة | في 1-3-2: تقدير عدواني إلى جانب الحكمة (تستخدم الخريطة بحذر) |
| سُماكة الكائنات الحية (السمك باء) أقل أهمية من العجز | Rogers 1997 | محاولة: استخدام ألف للكسب | القيام به |
| المعادلة الشاذة في موجز البيانات المالية العالمية | PETOOL, wave-propagation (Lytaev), Levy (book not read, only preview), Lindquist | الصيغة الأولى التي تحتوي على حقل على أفق يهيمن عليه الضوضاء العددية (AUC 0.12-028): صحيح (يبقى على 8 كيلومترات، الشعاع 6 درجات)؛ بعد تصحيح تركيز تركيز الاتحاد الأفريقي 0.54-066 | في حساب ثان، لا يقرر التوقعات |
| إيفانس: 07 دي بي أكثر من 100 ميل، الهامش 10-20 دي بي إلى 90-99٪ | Bray 1961, Antenna Book, GEKO 69 | عتبات الطرق في الظروف الهامشية | in use: probability for period and threshold FT8 −18,6 dB in troposcatter |
| الحجم المشترك والمسافة المقابلة من براي | IU3QEZ, Bray 1961 | المعلومات | في |
| النشرة P.617 و RSGB باعتبارها عدم يقين | TropoLink, DL2FZN | المعلومات | في 1.3.0 |

### ما تبقى خارج النماذج

أجريت في البحر على ارتفاع منخفض)١٠٠-٢٠٠ متر( أن مستويات النظام العالمي لمراقبة المناخ لا ترى)دراسة مصادر البيانات الحالية(؛ والتفكير والنشر في الجبال القريبة)فيسوفيو، فييتو(: اقتراح لحساب &quot; مونت &quot;. ITM/Longley-Rice remains a statistical reference "normal day" (Ns 301, climi), without conduits. مهجورة بقرار من صاحب البلاغ: AirScout, P.528), E sporadic (P.2001) and 50 MHz (servoir EIRP that few have and confuse with the E sporadic).

### اقتراح بشأن نظام العمل

1. تقييم موسع: إضافـة مصـادلـة مميتـة مـن إعـادة الرصيـد العالمي (حتى ساعات بدون مكان) ورقم QST 2022؛
2. جربي الملامح العادية لـ CRPL و عتبة الاتحاد الدولي للمواصلات السلكية واللاسلكية التي تبلغ 79 كيلوغراماً، واحد في كل مرة،
3. Comparison of the GFS profiles with the I GRA2 radio wave of برينديزي and Practice of Sea in the days of the QSO.
4. Reevaluate the parabolic equation after numerical correction.

### قرارات 21 تشرين الثاني/نوفمبر (التحويل 1-3-2)

- اعتُمد: تقدير حصيف (البحرين والدوتون، 100 ن/كم) وعدوان (الاتحاد الدولي للمواصلات، 79 ن/ك م) مرن؛ ولم تُلتمس أي قنوات سوى أقل من 2500 متر (الأول 1500 متر بعد التصديق)؛ ومكافآت ذات ضغط مرتفع وريح ضعيفة؛ ووزن بحري مع ميكروه من P.452؛ وحصة المحطات فيما يتعلق بالكوندات الجزئية أيضا؛ IT9GSF
- اعتُمد كبديل، وأُطفِل عن طريق العيوب: صورة CRPL طبيعية مع ن.
- معادلة متناقضة صحيحة رقميا: مفوضية الاتحاد الأفريقي من 0.12 إلى 0.54 (30 m antenna) ومن 0.28 إلى 0,66 (300 m).
- الأرض: Copernicus DEM GLO-30 بدلا من &quot; Tiles &quot; &quot; ، التي تقع بالقرب من الساحل عمليات غوثية شبحية تتراوح بين 80 و 100 متر في البحر (توقيع IC8TEM ، تم التحقق من نقطة إلى Capri)؛ ولا يزال السكادي احتياطيا.
- Weather signs on the map with their effect:). favorable subsidence (Moler and Holden 1960, on average about 2.5 dB between air going down and air going up; +1.5 dB),). air uphill unfavorable (depending deep; −2 dB), S scirocco favourable clue (ARRL Handbook § 19.4, inversions very strong on the Mediterranean; no correction). رمز مزدوج، ألوان خضراء/متربة وكتاب مصورة على الخريطة
- اعتمد المعيار 1-2-2-1 (NTIA/itm, public domain), translated into C#: وفقا لحساب خط الأساس في الغلاف الجوي العادي بجوار P.452 والتمديد النهائي بعد الخروج من قناة تنتهي قبل المراسل. تم التخلص من الشفرة المضغية لأن تحت GPL v2.

بدون محتوى مفيد للرقم القياسي:

- اعتُمد كمبدأ الكتيبات (21 تشرين الثاني/نوفمبر): لا يمكن تعميم نشرها، بل يمكن دراستها فيما يتعلق بعلم المورفولوجيا المحلي ( DF5AI)، التي أبلغت عنها IK3XTV في مجال الزرع على مترين)؛ واستشهد بها الدليل المتوقع (البند 1) وفي المجالس.
- اعتُمد (تشرين الثاني/نوفمبر 21، بعد 1.3.3) تفسير فتحات أشعة إكس0Q: ليس "الخلايا الاصطناعية مثل العدسات"، بل التروسكات التي تعززها الاضطرابات في أعلى الطبقة التي أعيد تصنيعها عندما تصل إلى الحصة من الحجم المشترك (رسم التوقعات، الفقرة 8 ثالثا). من 1.3.4 يدخل حساب نقطة النقطه كتعزيز مؤقت لجهاز التروسكاتر، في أكثر من 3 دي بي. مَرْدُوب من نفس المقالاتِ: "التَغَيُّل" مِنْ الإشاراتِ خارج جبال الألب (واسطة سلبية لا تَصْهرُ: الروابط الموصوفة ما زالت مُوضَّحة بالنشر والتروستات؛ القوى المُعلنة لا تُستخدم كما يُعطى)، الثلج الذي يُعَدّدُ "الإرتفاع" (أرجح أنّ طبقة مستقرة على الجليد، مع القليل من التهرب). IK3XTV

- Kerr (1951) reviewed on the book (November 22, PDF provided by the author): § 1.5, eq. 17 and 18 and table 1.1. الرقم 18 في المسح غير قابل للقراءة، لكن الجدول 1-1 يؤكد: 20 قدماً 1 سنتيمتراً، 80 قدماً 10 سنتيمتر، 400 ft → 100 cm، 2000 ft → 1000 cm، مقابل 1,25، 10، 112، 1252 cm of →mx = 0.014d1.5 افتراضات كير: داخل القناة انخفاضات الرقم القياسي المعدل من 4 إلى 10 إلى 8 للقدم، قيمة نموذجية للوصلات المنخفضة، مع انخفاض التدرجات تحتاج إلى سميك أكبر. ويحذر كير أيضاً من أن (كاتماكس) ليس قطعاً صافياً: فالأصابع الموجية أحياناً أطول يمكن أن تتأثر بقوة بالنقاش.

- DL6WU reviewed on the original (November 22, PDF of VHF Communications 3/1982 provided by the author): G. Hoch, ”Extremely Long Yagi Antennas“, pp. 130 and following. Fig. 1: gain on the dipole measured at 432 and 1296 MHz in a professional polygon of the German Posts, with a sample antenna EIA from 7,7 dBd, estimated accuracy 0.5 dB; 2,35 dB for double the length of the boom, no saturation up to about 20 IX. The program used 8.6 + 8.0.log10(L) dBd, i.e. about 0.8 dB in less than 1 -2 PEC; now it uses 9,4 + 7.8;log10(L/34) dBd (the gradient is the stated one, the value to 1 child is read on the chart, 0.3 dB).:: حذفت الصيغة 10 - 8 - 7 - 8 - - - - لا، التي عممت على الشبكة: من شأنها أن تعطي 3 دي بي للمضاعفة، مقابل 335 2 مقاسة.
- Booker and Gordon (1950): reference confirmed by " This Week's Citation Classic" (Current Contents, 8 February 1982, comment by H. G. Booker): Proc. IRE 38, pp. 401-412. The comment confirms the two points used in the manuals: field received beyond the horizon with Rayleigh distribution, and exponential autocorrelation function of irregularities (non-Gausian). لم تقرأ المادة الأصلية.

## 13. من المواد IK3XTV

The two articles by Flavio Egano IK3XTV (Radio Rivista: "Propagation on 2 meters" and "Propagation VHF - convective cells and gravitational waves") add two things that the duct models do not cover: the transalpine openings of the afternoon due to convection, and practical rules on where and when the pipelines are formed in Italy. وهي ملاحظات من المحطة، لا تدابير: يجب معاملتها على أنها فرضيات للتحقق من البقع.

| تعليقات صاحب البلاغ | كيف يصبح مكوّناً للمؤشر |
| The ducts are mainly formed between 100 and 1000 m, rarely higher; serve high pressure on a vast area, good insolation and absence of wind | البحث عن القناة في أقل من 1500 متر فقط؛ مكافأة ذات ضغط مرتفع وريح ضعيفة في طبقات منخفضة |
| وفي الأشهر الدافئة التي جرت مرة أخرى في الأدرياتيك بين فينتو وبوغليا، على بعد ٧٠٠-٨٠٠ كم، وأحيانا إلى اليونان؛ والطرق البحرية هي المفضلة | وزن أكبر لجزء من مسار البحر (مثل ميكروه 1 من الاتحاد الدولي للاتصالات - R P.452) |
| يجب أن يكون المقابل في القناة أو بالقرب منها؛ مع وجود هوائيات عالية وبضعة أمتار تقود الإشارة لا تدخل | مقارنة حصة قاعدة القناة بحصة المحطتين (سوف تخدم ارتفاع QTH) |
| في نفس الوقت، نفس القناة: الاستماع من غارغانو محطة في 144 وواحدة في 50 ميغاهيرتز | لفحص: وفقاً لطاولة هيبورن، يطلب 50 ميغاهيرتز أكثر من 300 ميل، لذا فإن الحدث سيشير في كثير من الأحيان إلى خط أنابيب |
| Connections Veneto - South Germany beyond the Alps, without visual, almost always in the afternoon and with high pressure; unstable signals that disappear and reappear after a few minutes | فهرس "محقق" مُنفصل عن القناة، للطرق الجبلية |
| The convective cells are born on the ground and rise about 1 km every 3 hours: at 15 they reach about 3 km, the altitude that serves to override the Alps | Use the limit layer altitude (boundary/_layer/_height of Open-Meteo GFS) along the path: score when it exceeds the altitude of the ridges |
| The Alps generate gravitational waves that, together with convective motions, create irregularities of the refractive index | الفرضيات: الريح القوية المنتشرة في السلسلة إلى 700 هكتار كإشارة للموجات الذهبية؛ |
| وتعطي نسبة عالية جدا من التخريب (حتى 8 كيلومترات في حالات استثنائية) حوالي 800 كيلومتر، ولكن في الممارسة العملية نادرا ما تتجاوز 300 كيلومتر | توسيع نطاق حصة التحويل: بيانات مفيدة لتقدير مسافة الانتظار |
| في نصف قطر الأرض الصيفي الذي يعادل حوالي 000 16 كيلومتر (2.5 مرة في الواقع)، مع وجود حوالي 60 دي بي في أقل من 300 كيلومتر | متوافق مع 50 كيلو غراماً = 157/(157 - □ □ / / / / / / / / / / / / / / |
| على بعد أكثر من ٠٠٠ ٣ كيلومتر فقط من مسافة ٢٠ إلى ٣٠ درجة شمالا وجنوبا من خط الاستواء، فوق البحار، والمرتبطة بخلايا هادلي؛ وبلغت ذروتها في آب/أغسطس في افريقيا وأوروبا | خارج أوروبا: من المفيد عدم إعطاء مؤشرات عالية على المحيط الأطلسي دون ترددات، ولطريق الكناري - بريطانيا العظمى |

Other useful data from the same articles:

- - المدى الضوئي بين مترين من طراز h1 و h2 (d in km): d = 4,1 · (H.1 + h2)، أي ما يصل إلى حوالي 20 في المائة للانتعاش الطبيعي.
- Evanescence: slow and shallow with inversion; strong and fast with scatter and convective cells. يمكن للبرنامج أن يستخدمه لتصنيف الإعلانات إذا كان يوماً ما سيكون لديه تقارير SNR مع مرور الوقت
- وفيما يتعلق بالفصلية هاء في 144 ميغاهرتز، يبلغ صاحب البلاغ عن العمل الذي قامت به شركة DF5AI ودافيس وجونسون (المركز 435، 2005) بشأن الصلة بين الزمالات والإيس: من أصل التراب، ولكن مؤشراً منفصلاً غداً يمكن أن يستخدم بيانات البرق.
- التأمل الأرضي في القفز المزدوج Es: sea K = 81 and 5 S/m, countryside and woods K = 13 and 0.006 S/m, urban areas K = 3 and 0.001 S/m.

## 14. افتراضات الرقم القياسي للترددات العالية جدا

اقتراح البدء، الذي سيناقش: فهرس يتراوح بين صفر و10 على نفس نطاق هيبورن، محسوبا لكل نقطة من نقاط الشبكة وكل 3 ساعات من النظام العالمي لمراقبة المناخ (عندئذ يقارن ECMWF و ICON)، ورقم ثان لكل طريق يقول كم يمر جزء من الطريق إلى قناة مفيدة للفرقة المختارة. الأوزان الواردة أدناه هي افتراضاتي، ليتم معايرة مع البقع.

Review after Bean and Dutton and Kerr: profile thresholds come from their table 6.5 and scale from their measurements, so a refractive layer is worth 10–25 dB over the normal profile. ثم يُحسب المؤشر أولاً في سداسي البروم ثنائي الفينيل، ثم يأخذ في ٠-١٠؛ ولا تزال القيم في ذلك المقص معيرة بالبقع.

الحساب حسب النقطة، خطوة بخطوة

1. T discharge, relative humidity, geopotential altitude and spiritual speed at 1000, 975, 9950, 900, 850, 800, 700, 600 and 500 hPa, plus T and 2 m humidity, ground pressure, layer altitude and wind at 10 m.
2. Discard the levels below the ground; calculation N and M at each level (ITU-R P.453).
3. Calculation −dN/dh in each of the four layers of Bean and Dutton (1000–850, 850-700, 700–600, 600-500 hPa) and the chart as sub-refractive, normal or super-refractive with the thresholds of their table; inside the first layer also use medium levels to find the strongest gradient and its share.
4. حيث الـ دي إم / / / / / / / / / / / / / / / / / / / / / / / / / / / وجدت قاعدة و قمة القناة، سميكة و عجز / / /
5. Comparison of with the minimum fishness for the band from the formula of Kerr (about 180 m for 144 MHz, 89 m for 432 MHz, as in the Hepburn table); under the threshold the conduit still counts, but less.
6. أنا أحفز المكسب في الـ دي بي مع الجدول أدناه (تأخذ أفضل طبقة، وليس مجموع الطبقات، بالإضافة إلى فترة الشذوذ) وأحوله: الرقم القياسي = / 2.5، بين صفر و 10، لذا 25 دي بي يساوي 10. وتبين القيمة السلبية (الانتقاص من الممتلكات) أن البيانات المتعلقة بالمسار لا تزال موجودة.

| أفضل طبقات | المعايير | الإيرادات التقديرية |
| عادي (غير مهيمن) | −dN/dh in the tab range. 6.5 (e.g. 20–60 N/km between 1000 and 850 hPa) | صفر |
| قليلا فوق طبيعي | بين الحد الأعلى للفرقه العاديه و عتبة الإنتحار | صفر إلى 10 دي بي، بنسبة |
| انتهاك خارق | خارج عتبة الطبقة (100، 80، 70، 50 ن/ك متسلق) ولكن دون 157 ن/كم | من 10 إلى 15 دي بي، بنسبة تصل إلى 157 ن/كم |
| خط الأنابيب رقيق جداً | / / / / / / / / / / / / / / / / / / / / / / / / / / / / عتبة كير | 15 |
| موصل مفيد للفرقة | / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / | 20 دي بي، +1 دي بي كل 5 وحدات عجز، كحد أقصى 25 |
| الخصم الفرعي | −dN/dh 0 in the lower layer | −10 dB |
| الخلفية الشاذة (تضيف) | النموذج Ns ناقص خريطة الاتحاد الدولي للاتصالات هذا الشهر | 0.2 dB per unit N, between −5 and +5 dB |

الحساب بالطريق (بين اثنين من أجهزة تحديد المواقع): عينة الدائرة القصوى كل 10 كيلومترات، كما هو الحال بالنسبة لبرنامج الرقم القياسي على الطريق، وحساب الجزء من النقاط التي لها قنوات مفيدة ومتوسط حصة القاعدة. The CSIRO study found a useful part of the route in the pipeline (on AIS routes at 162 MHz was 67% higher than in control cases).

إضافة حساب كل طريق (من LA0BY و W3SZ): (1) محطات بحرية مقارنة بقاعدة وفوق القناة، لأن قناة عالية لا تخدم سوى من يدخلون؛ (2) تروسكاتر (اللوور) بأسلوب يه، مع زوايا البداية التي أخذت من الملامح الأرضية؛ ويُزال الكسب من الـ دي بي من تلك الخسارة، وتُقارن النتيجة بالخسارة التي تُديرها المحطة (X2). وهكذا يصبح المؤشر أيضا تقديرا للمسافات التي يمكن الوصول إليها. KK6MC

التصويبات لتجربة واحدة في وقت واحد (من بين ودوتون و LA0BY و IK3XTV و G4MCU في الدليل) لا تُحتفظ بها إلا إذا تحسنت مقارنة مع النقاط:

- الخلط (المهم وفقاً لصاحبة البلاغ، عن طريق المراقبة اليومية): اليوم على الأرض، بين منتصف الصباح وبعد الظهر بسماء واضحة، أو الرياح على الأرض على بضعة أمتار، تختفي القنوات على الأرض؛ ويجب تخفيض مساهمتها. ليس في البحر
- وقت اليوم (المهم حسب صاحب البلاغ، عن طريق المراقبة اليومية): تولد القنوات غير المنطقية عند غروب الشمس وتسقط بعد شروق الشمس؛ وهي مراقبة متوقعة لمدة ثلاث ساعات.
- حركة صوتية: ارتفاع الهواء إلى ٨٥٠-٧٠٠ hPa remixes the layers (negative correction), air descending under an anticyclone strengthens them (positive).
- اتجاه الضغط: ارتفاع الضغط على المسار في الساعات الأخيرة، وارتفاع وثبات الإشارة، وانخفاض الإشارة (باستانو 2 هب ب).
- Synonym: high pressure above 1025 hPa, temperature at 850 hPa much above that on the ground, hot and dry air flowing on the colder sea, route by sea.
- مسارات الجبال: فهرس ثانٍ "مُقنع" (القسم IK3XTV) مع طبقة حدية بعد الظهر أعلى من الحواف على طول الطريق والضغط العالي.

من طراز VHF/UHF DX Book, other corrections to try: consecutive days of high pressure on the point (more days, more points), position compared to the center of the anticyclone (bonus at the edges), pressure gradient on the ground as a measure of the wind, strong rain in the previous 24 hours (penality), low clouds at the edge of the layer (penality only for the high bands). For the direction of the routes: comparison the azimuth of the route with that of the isobare.

التحقق

- محفوظات ذات ترددات عالية جداً: كل إعلان تجاري يزيد على 500 كيلومتر إلى 144 أو 432 ميغاهيرتز هو "حدث". وهو يقارن الرقم القياسي على الطريق مع تواتر الأحداث، في نفس الوقت من النهار والشهر (نفس النهج المتبع في دراسة &quot; سيكرو &quot;).
- مقارنة مع (هيببورن) و(إكس كي) في نفس الأيام حيث تفصل المؤشرات الثلاثة، تبدو من كان على حق وفقاً للمواقع F5LEN
- Radiosundations of 00 and 12 UTC: to check if the model sees the inversions that the probe measures.
- Historical cases of LA0BY (6-11/11/2003, 10/12/2004, 15/10/2011, 11/2/2015, 22/11/2018): The index recalculated on those days must see the ducts that Hepburn did not show.
- Limits to be declared in the manual: conducted under 100-200 m almost visible to GFS pressure levels; reliable prediction for 2-3 days, then only trend.

## 15. مصادر 21 تشرين الثاني/نوفمبر (الجزء الثاني)

- ITU-R P.1812 (MaTLAB/Octave implementation of I. Stevanovic, OFCOM; GitHub eeveetza/p1812, Zenodo 10.5281/zenodo.6141558). طريقة محددة ذات تردد عال جدا/تردد فوق عال جدا من الطريق، مستمدة من P.452: نشرة دلتا - بولنغتون، وتروسكاتتر، وخناق، وانعكاس على الطبقات، والأغلفة على الأرض. ويستخدم كمقارنة ثالثة إلى جانب P.452 في حساب النقاط المرجعية؛ وبيانات الـ دي إن و N0 هي منتجات كاملة من الاتحاد الدولي للاتصالات السلكية واللاسلكية، وبذلك يقوم المستخدم بتنزيلها (حسب P.453).
- From Costa, Rabanal Ramirez, Dias, IET MAP 2018 (doi 10.1049/iet-map.2017.0992). 563 MHz, 27 points between 5 and 30 km on irregular terrain with forest: P.526 with 12 m of vegetation −4,3 dB and 8,8 dB; P.1812 with 10 m of encumbrance +2,6 dB and 8,000,9 dB; dielectric wedge −4,8 dB and 9,2 dB; P.1546 +5,0 d وفيما يتعلق بالبرنامج، فإن الخطأ المتبقي في طريقة الانتشار الجيدة يبلغ حوالي ٩ ميغابايت، وطول التراكمات على الحواف)١٠-١٢ م( يحسب.
- PETOOL (Ozgun e altri, Computer Physics Communications 182, 2011). - انقسام المعادلة الموازية إلى طريق واحد وطريقين مع محرك ذي زاوية واسعة (زوارق تصل إلى 40 إلى 45 درجة، مقابل 10 إلى 15 درجة من القاع الضيقة)؛ وخسائر من خلال المزيج الرابع المحوّل؛ والأرض إلى خطوات. يؤكد خيارات 1.3.2 (الزاوية على نطاق واسع، ومعاملات التربة) ويشير إلى حساب الاتجاهين لعمليات الإغاثة الشديدة.
- الرياضيات `tropopl`: loss of gases and loss of ”lente” in a layered atmosphere (P.835, P.676, P.453), without ducts: with gradients below −157 N/km the geometric optical model does not apply. The example of radio propagation uses for refraction − loN = 7,32;e(0,005577)Ns), the same CRPL report already in the program. `propagationModel` Longley-Rice: predefined horizontal polarization values, conductivity 0.005 S/m, permittivity 15, reactivity 301, temperate continental climate. لا يوجد نموذج الرياضيات يتعامل مع القنوات
- ITU-R, digital products of group 3. Data and programmes of P.453, P.452, P.1812, P.2001, P.528, P.1546; 'integrated' data are part of the recommendations; no declared redistribution license: you continue to let them download to the user.
- (إيسا نافيبيديا) التأخير التسلسلي N = Nidrostatica + Numida; the dry zenital delay is about 2.3 m and the humid one tens of cm, which varies quickly. وبالنسبة لنا: فالتأخر الرطب الذي تقدره محطات النظم العالمية لسواتل الملاحة هو مقياس غير مباشر للرطوبة المتكاملة، والمصدر المحتمل للتنبؤ الآن في المستقبل؛ واليوم غير قابل للاستخدام.
- Bray, K2LMG, ”A Method for Determining V.H.F. Station Capabilities“, QST November 1961, pp. 36-40 (read in original; re checked on November 22 on the PDF of the number of QST: all values below correspond). ويقول صاحب البلاغ، د. و. براي (الكهرباء العامة، إيثاكا)، إنه تحقق من التوقعات مع أشهر من الاتصالات المقررة مع K2GOI و W4LTU. Evanescence within the time of 13,5 dB over about 100 miles (7 dB below and 7 above the hourly average), almost linear below (3,5 dB to 50 miles): for a solid connection remove 7 dB. تصحيح الأفق: يبلغ كل ارتفاع في الأفق 69 ميلا (111 كيلومترا) من مسافة بعيدة (أقل إن كانت سلبية). Gain of a x yagi about 10 boom/uc (power ratio), more about 4 dB of soil reflection; SSB −3 dB and AM −7 dB compared to CW; 99% of the hours: about 180 dB to 60 miles, 190 to 90, 200 to 240 (2 m). من 1.3.2 يُحتمل أن يُستخدم في هذه الفترة التلاعب بـ 3.4 دي بي (7 دي بي كـ2) بدلاً من 5 د.
- الحساب في اتجاهين (عمل مع النموذج الأولي، 21 تشرين الثاني/نوفمبر). Large angle parabolic equation with real S RTM1 terrain from Capri, 144 MHz, antenna 10 m from the ground, standard atmosphere; measure: average power between 20 and 400 m to 80 km on the route towards the corresponding. ومن مركز كابري ومارينا غراندي في الغرب مغلق)عائق إلى + ١١-٢١ درجة، مونتي سولارو(: يشير إلى الفايتو)مؤخرا EA5 و EA6 ، ١٥٠-١٦٠ درجة شرقا( والإشارة التي أطلقت نحو اسبانيا ترتفع إلى ١٠-٢٩ درجة مئوية؛ وتشير إلى &quot; فيسوفيوس &quot;)١١٠-١٢٥ درجة من المحور، ثم إلى ١٧٢٥ باء(. من "أناكابري" الغرب حر و يفوز مباشرة من 8 إلى 22 دي بي غير أن الخسائر المطلقة التي تقع على بعد ٠٠٠ ١ إلى ٠٠٣١ كيلومتر من النموذج الأولي ليست موثوقة. EA3 EA5 EA6
- Barsis and Kirby, NBS Report 6751 (1961), Pikes Peak, 223 km, 100 and 751 MHz. إن الجبل هو محطة طرفية مشتركة من طريقين متبصرين: إن توزيع الوسطاء في الساعة هو تطور مساري الطريقين.)١-٩٩ في المائة( من حوالي ١٤ درنا، مقابل ٢٦-٤٠ درنا من مسار &quot; تروسكاتر &quot; المتساوي طولا أو زاويا؛ لا تغيير يذكر في اليوم. The signal is stronger than the troposcatter but weaker than the ideal edge: the top should be treated as rounded (rough about 11.5 km). وتُلاحظ دائما إشارات عالية وراء ظهورها.
- Barsis, NBS Report 7652 (1963), three routes over a mountain (Colorado, Alaska, Europe), 750-900 MHz. ومتوسط الخسائر من 10 إلى 20 دي بي أعلى من حساب Fresnel-Kirchhoff دون وجود أرض بالقرب من المحطات الطرفية؛ ويُقدَّر التهرب بربط الخطين بالصور الضوئية؛ كما أن انعكاسات التربة القريبة من المحطات الطرفية تزيد من تفاقم التلاشي المطول. من 1.3.2 يضيف الدخول إلى القناة تقريب P.526.
- Barsis and Johnson, NBS TN 88 (1961). إطالة حل الموجة الفضائية، حتى على طريق ما وراء جبل (المادة 5).
- Norton, ”System loss in radio wave propagation“, J. Res. NBS 63D (1959). المنحنيات التي يحصل منها (براي) على الأشعة، وتعاريف فقدان النظام، والإرسال، وكسب الهوائي الأساسي.
- Rice, Longley, Norton and Barsis, NBS TN 101 (1965-67), Volumes 1 and 2. (ب) حافة وعائق وحيدين متجمعين مع انعكاسات (الباب 7، مع مثال بيكس بيك)، والتفاوت الطويل الأجل 5 (0) و(ف) للمناخ (الملوثات المعاصرة وغيرها). وبالنسبة لنا: إن طريقة العقبة المتجمعة ووظائف التقلب هي الإشارة إلى عدم التيقن المعايرة.
- ITU-R P.526-16 (11/2025) - Formula (31) of J(ve), similar to that of the program; rounded hurdle (32)–(36), now used; multi-split methods (Bullington and full method § 4.5).
- ITU-R P.1812-8 (2025) and -7. استرجع النص الكامل: طريقة محددة من 30 ميغاهيرتز إلى 6 غيغاهيرتز (ديلتا - بولنغتون، والتروسكاتر، والخناق، والتفكير في الطبقات، والغوغلوب). The -7 warnings that its troposcatter (from P.617-5) is wrong under 66 km. ولا تزال مقارنة ثالثة ممكنة.
- Paulus, “Plavatical application of the IREPS evaporation model”, NOSC TR 966 (1984), and Radio Science 20 (1985). يُقرأ التقرير في 22/11/2026 (DTIC ADA146528, scan with recognized text, sometimes unreadable). وتتحول الاختلافات الجوية - البحرية التي تقاس بالسفن إلى الاستقرار: وهواء بحري أدفأ في ٣١-٣٥ في المائة من بيانات السفن مقابل ٠,٧-٧ في المائة من الخنازير غير الملاحية، وذلك بالنسبة للمصابيح المزعجة بالسفينة وSST المأخوذة من مياه التبريد)تقريبا + ١ درجة مئوية(. The Jeske model then gives 40 m and more than 15% of the year and altitudes that jump from 10 to 40 m in 24 hours with stable time, not confirmed by radio measures. التصويب المقترح (العتبات الاختبارية صفر و0 و5 و1 درجة مئوية؛ و1 درجة مئوية يتفق عليها على نحو أفضل مع التدابير الإذاعية التي اتخذت في حزيران/يونيه 1982): إذا كان الفرق بين الهواء والبحري - 1 درجة مئوية أو أقل من الفرق المقاس؛ وخلافاً لذلك، تحسب الارتفاع بـ - 1 درجة مئوية و0 درجة مئوية، وإذا كان ذلك مع - 1 درجة مئوية أقل، فإنكم تحملون ذلك. والزيادات في الجانب المستقر تخفض، ولا تزال هناك انخفاضات. In June, Marsden 120 square: average altitude from 16.8 m (climatology) to 7.6 m. ولا ترد معادلة النموذج في التقرير (يرجى الرجوع إلى جيسكي وهتني). The program uses the neutral profile, which does not have stable exaggeration, and limits tru to 40 m: it goes in the same direction; with the SSTs of the models (not of ship) the fault of measurement that Paulus corrects weights less.
- Levy, ”Parabolic Equation Methods for Electromagnetic Wave Propagation“ (IEE 2000). On Google Libri there is only preview: أنا لا أقرأ
- VK3FS (3fs.net.au). إدخال الجودة: ارتفاع الطوافات عند ٤٥٠-٢٠٠٠ متر للفتحات الأطول على الأرض؛ وتحولات تبلغ ٣٠٠-٦٠٠ متر في ليال هادئة وهادئة، وتختفي بعد شروق الشمس؛ وتحولات ساحلية أقوى بعد ظهر اليوم؛ و٨٠٠-١٣٠٠ كم نموذجية، و٠٠٤-٠٠٦ كيلومتر في كثير من الأحيان. متوافقة مع الإنذارات النهارية/الليلى والحد الأقصى 2500 متر

## 16. المصادر

القواعد والصيغ

- [ITU-R P.453-13 (2017), radio refractive index:](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)formula and data
- [ITU-R P.452-18 (2023)، التنبؤ بالتدخل بين المحطات الأرضية التي تزيد على 100 ميغاهيرتز: نصف قطري معاد، بيتا0، تروسكاتر، خواص](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)
- [ITU-R P.617-5, propagation beyond the horizon for radio bridges](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)
- [Shi et al., "دراسة شاملة عن الموجة القصوى من البروجات الكهرومغناطيسية في مختلف دوقات الإجلاء"](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

دراسات عن التنبؤ بالنقاشات مع نماذج الطقس

- [P. Lopez, "A 5-year 40-km resolution global climatology of super-refraction for ground-based radio meteorology", ECMWF Tech. Memo 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO), "Tropospheric Ducting Prediction based on GFS Model Data at... the CSIRO Murchison Radio-astronomy", Observatory arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (FMI), EGUsphere 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

توقعات إذاعية للهواة

- [W. R. Hepburn, "The Hepburn Tropo Index (HTI)"](https://www.dxinfocentre.com/propagation/hti.htm)and [](https://www.dxinfocentre.com/propagation/history.htm)method history
- [W. R. Hepburn, "Lowest Usable Frequency vs. Thickness of Ducting Layer"](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, "Worldwide refractive index forecasts"](https://tropo.f5len.org/WW/info.php), [new version 2026](https://tropo.f5len.org/?p=504), [note on model ARPEGE](https://tropo.f5len.org/author/f5len/)

البيانات

- [Open-Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api), [note on pressure levels](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)
- [NOAA NCEI, Integrated Global Radiosonde Archive (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RAOBget, radio flood script for the University of Wyoming](https://github.com/NCAR/RAOBget)

نصوص كلاسيكية (يذكرها مقدم البرنامج، يُستشار للصيغ النهائية)

- B. R. Bean, E. J. Dutton, *Radio Meteorology*, NBS Monograph 92, 1966, 435 pages (reprint Dover 1968): capital profiles of N and M, gradient of fish under −157 N/km, comparison with radio wave; chapter 5 "Synoptic radio meteorology". القاعدة ٢-٤ خطوات حساب لكل نقطة. Free: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [NIST card, doi 10.6028/NBS.MONO.92](https://www.nist.gov/node/1770911), [Internet Archive](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf)copy.
- L. Boithias, *Radio Wave Propagation*, McGraw-Hill 1987 (translated by D. Beeson from French, 330 pages, ISBN 0070064334; also North Oxford Academic): mechanisms beyond the horizon, common volume of scattering and loss of coupling antenna-mezzo. Paper only ([library card](https://libcat.library.tamu.edu/Record/in00000014812/Description)].
- M. P. M. Hall, *Effects of the Troposphere on Radio Communication*, IE Electromagnetic Waves Series n. 8, Peter Peregrinus 1979, 206 pages (ISBN 0906048257; brosura 0863410863): synoptic situations and propagation VHF/UHF. قاعدة المكونات الضوئية Trade at IET ([](https://libcat.library.tamu.edu/Record/in00000636385)card].
- H. G. Booker, W. E. Gordon, "A Theory of Radio Scattering in the Troposphere", *Proceedings of the IRE*, vol. 38, n. 4, April 1950, pp. 401-412: قص نظرية من عدم تجانس الرقم القياسي للتفاعل. الفكرة المركزية: تتوقف القوة المنتشرة على كثافة وحجم التقلبات المضطربة في الرقم القياسي للانحلال (الترابط التلقائي)؛ وقرب الأفق يسوده الانتشار، ويزيد من التشت؛ ووفقا للمؤلفين، فإن حجم الاضطراب والتفاوتات في المؤشرات التي يتوقعها الأرصاد الجوية تكفي لشرح الحقول المقيسة. متذرعاً بـ 328 مادة (سكاليت)، لا نسخة دخول مجانية. Card on Semantic Scholar ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); full text on IEE Xplore paid ([doi 10.1109/JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)].
- C. M. Crain, J. R. Gerhardt (Electrical Engineering Research Laboratory, University of Texas), (Measurements of the Parameters Involved in the Theory of Radio Scattering in the Troposphere) *Proceedings of the IRE*, vol. 40, n. 1, January 1952, pp. 50-1954, doi 10.1109/JR27](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)
- The Constants in the Equation for Atmospheric Refractive Index at Radio Frequencies, *Proceedings of the IRE*, vol. 41, n. 8, August 1953, pp. 1035-1037: proposes to review the currents then in use (79 K/mb and 4800 K) in the formula N; It is from here that the values 77,6 and 3,73 · 105 of the formula ITU ([Q](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E. C. S. Megaw, "Fundamental radio scatter propagation theory", *Proceedings of the IE Part C: Monographs*, vol. 104, n. 6, September 1957, pp. 441-455, doi 10.1049/pi-c.1957.0052: spectrum of indexizing from turbulence the paid cardon complete trace of scatter beyond the](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)
- * Proceedings of the IRE*, October 1955, special number on scatter propagation (by K. A. Norton and J. B. Wiesner), with Bullington, Staras and others: free in PDF on [World Radio History](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf).
- D. E. Kerr (edited by), *Propagation of Short Radio Waves*, MIT Radiation Laboratory Series vol. 13, McGraw-Hill 1951 (Restampe Dover 1965 and IE 1987): theory of evaporation and surface ducts on the sea. نسخة رقمية: [الرقم القياسي للتاريخ الراديوي العالمي؛ طبعة IET في [IET Digital Library](https://shop.theiet.org/propag-short-radio-waves).](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf)

التدابير الأخيرة للنشر العرضي في التردد العالي جدا: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Australia), "A Study of VHF Troposcatter Communication Channels in South Australia", in *Digital Signal Processing for Communication Systems*, Springer 1997, pp. 103-112 ([Springer](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)). النص مقابل رسم؛ ومن خلاصة الأمر يتعلق بالنشر، وليس بالعقيدات. مفيد فقط إذا كنت ترغب في معايرة فترة التروسكاتر مع بيانات قناة VHF.

عدد من النماذج والطرق البحرية (يذكرها مقدم البرنامج)

- M. Levy, * طرق معادلة بارابكية لاستخلاص الموجات الكهرمغنطيسية*، IE Electromagnetic Waves Series #45, 2000, 336 pages (ISBN 0852967640): معادلات شبه انتكاسية تتغيّر على طول المسار و for orography; chapters on troposferous propagation, rays and ways, propagation on sea. وسيعمل على حساب ميداني على طول الطريق، بما يتجاوز الرقم القياسي. For payment, also in electronic form from IET ([IET card](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)].
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (University of Leicester): 2 GHz measurements on the Channel Islands and the Channel. والعديد من الأشغال والإطارات مجانية في محفوظات الجامعة: [الإحصاءات الطويلة الأجل بشأن قناة التبخر، وعلم الراديو 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434)، و [تغييرات في ثلاث طرق بحرية، وعلم الراديو 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710)، و[Titles of N. Mufti 2012 التي تبلغ مسالكها 50 و140 كيلومتراً أيضاً في VHF](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854)، و[تركيز I. Alam 2002 على التفاعل المحسوب من البيانات الجوية 240 MzH3.](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus, &quot; Practical application of an evaporation model &quot;, Radio Science vol. 20 n. 4, 1985, p. 887 (not IEEE Transactions on Antennas and Propagation), doi 10.1029/RS020i004p00887 ([scheda](https://zendy.io/title/10.1029/rs020i004p00887)): Higher of the evaporation duct from surface weather data. Free the technical report preceding it: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf).

T. Lindquist, *Wave Propagation Models in the Troposphere for Long-Range UHF/SHF Radio Connections*, master's thesis, University of Karlstad, 2 June 2020, 106 pages (supplied by the author of the program). It is a step-by-step guide, free, to write a calculation with the parabolic equation: split-step solution (even at wide angles), boundary conditions for land and sea, ground to steps, beam tracking on linear profiles at times, with numerical checks; cites the PETOOL free tool for MATLAB. بيانات مفيدة في وقت واحد: (1) الحساسية تجاه متغيرات الطقس، والناموسيات النيوزيلندية: 0.26 حامض + 4،3 حامض - 1،4 كيلوغرامات (ح م، حوالي 20 درجة مئوية)، أي خطأ قدره 1 hPa على ضغط البخار يزن ثلاث مرات خطأ وزنه 1 درجة مئوية على درجة الحرارة: هو رطوبة النموذج الذي يجب فحصه أولاً: استنتاج المؤلف (التكاليف من 1 إلى 10 جي هز): بعد الأفق، الاتصالات الموثوقة تقريباً تحتاج دائماً إلى قناة.

برمجيات بروباغيت على ملامح التربة (للأفق والعقبات، ليس للمجموعة المتوقعة)

- [Radio Mobile](https://ve2dbe.com/rme.html)by Roger Coudé VE2DBE: free, Windows (also Linux and Mac with Wine), Longley-Rice/ITM model, terrain profile automatically extracted from quota data ([download](https://ve2dbe.com/download/download.html)).
- !](https://qsl.net/kd2bd/splat.html)by John Magliacane KD2BD: GPL, 20 MHz-20 GHz, Longley-Rice and ITWOM v3.0 models, obstacle and loss reports in text; latest version 1.4.2 of 2014.
- [ITM, NTIA/ITS](https://its.ntia.gov/software/itm)official code: the Longley-Rice model (20 MHz–20 GHz) with source and documentation, base of Radio Mobile and SPLAT! الرمز حر ويمكنك أن تضيف إلى الحساب على طريق ليفتر دوف (الخسائر بسبب التربة، على أن يضاف إلى الرقم القياسي للتروتو). Original report: A. G. Longley, P. L. Rice, "Prediction of Tropospheric Radio Transmission Loss over Irregular Terrain: A Computer Method – 1968", ESSA Technical Report ERL 79-ITS 67, July 1968 (NTIA card on historical software: X1 https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITU-R, software and digital data](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): free maps of hin and N0 annual medians on grid 1,5° used by P.452 and P.617 -- useful as reference climate values for the index.
- Pathloss (Contract Telecommunication Engineering): commercial program for radio bridges; I have not found a public source that confirms which ITU recommendations implements, so to check.

Reusable GitHub Code in LIVETROPOVHF

| المشروع | ماذا تفعل؟ | اللغة | استخدام ممكن لنا |
| [NTIA/ITM](https://github.com/NTIA/itm) | نموذج لونغلي - رايس الرسمي في NTIA، مع حزم ل. صافي |  C++, C#  | فقدان الأراضي على الطريق: يدمج مباشرة في البرنامج، وهو في C# |
| [الجوزي/الرمز](https://github.com/fuzzzeee/itm) | ترجمة |  C#  | بديل بدون مكتبات محلية، مفيد أيضاً لنسخة ويندوز 7 |
| [eeveetza/Py452](https://github.com/eeveetza/py452) | ITU-R P.452, similar to the MATLAB/Octave version approved by ITU-R WP 3M; MATLAB version published by the Swiss Federal Communications Office | Python, MATLAB | الإحالة إلى C# The troposcatter formulas and conducted, with ITU validation examples |
| [Bwinkel/pycraf](https://github.com/bwinkel/pycraf) | P.452 complete, P.676, SRTM profiles; GPL v3, but ITU data files included are not free for commercial use | ♪ | مراقبة نتائجنا |
| [ITU-Rpy](https://github.com/inigodelportillo/ITU-Rpy) | العديد من توصيات الاتحاد الدولي للاتصالات، بما في ذلك P.453-13، المصادق عليها مع أمثلة الاتحاد الدولي للاتصالات | ♪ | التحقق من الصيغة الوطنية وخرائط المناخ P.453 |
| [NCAR/RAOBget](https://github.com/NCAR/RAOBget) | تحميل الموجة الإذاعية لجامعة ويومنغ في شكل نص | ♪ | رسم بياني للتنزيل في C# |

ملاحظة: النسخة الحالية من P.453 هي [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), downloadable free of charge; in the document I used the 13, to be reallined.

ويوجد بالفعل في كتيبات البرنامج والمعاد استخدامها هنا: LA0BY (Nordic VUSHF 2019)، IK3XTV ( &quot; Propagation on 2 meters) و &quot;Propagation VHF - convective cells and gravitational waves &quot; ، مجلة إذاعة، التي قدمها مؤلف البرنامج)، DF5AI/DK5YA (2003)، G3YGF (QST 1983)، The VHF/UHF DX Book cap. 2. G4MCU KP4MD
