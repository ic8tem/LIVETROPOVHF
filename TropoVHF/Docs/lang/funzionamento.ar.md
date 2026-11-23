التردد العالي جدا الوثيقة التقنية 1
# تشغيل البرنامج

> الترجمة الفورية من جانب الإيطاليين: في حالة الشك يستحق الدليل الإيطالي.

ما يفعله البرنامج من حيث يأخذ البيانات وكيف يحولها إلى ما تراه على الخريطة والقائمة Version of 9 November 2026, related to the project in C# (.NET 10 + Avalonia).

## موجز

- الغرض: توقعات التروبو (Hepburn [3], F5LEN [4], isobare GFS [18]) والرؤية الحقيقية للفرق في نفس النافذة، وفقا لقائمة المراقبة LA0BY [1].
- Data collection: two DX cluster, Reverse Beacon Network [9], dm4x cluster and PSKReporter [5] MQTT flow, UDP decoding from WSJT-X, JTDX and MSHV [8].
- Spot: band segments, negative filter, airscatter in dashedصف, position by locators or C ALL3.TXT [6] [7].
- الذين يستمعون: الدوائر الوردية المظلمة ذات الخطوط الوردية الوردية دائماً ما تكون مرئية نحو المحطات [5]
- Spot card: distance, azimut, tropo index on the path and troposcatter loss with the G3YGF [2] method.
- Languages Hepburn and F5LEN: [3] image and calibration bench [4].
- المراقبة: مراقبة NTP على 10 خواديم كل 3 دقائق [10] [11].
- التقارير المرتقبة: اعترفت المنظمة في Q65/JT65 بموقع القمر، والقاطرات المغمورة، والمزلاجات، والحجر، والمستمعين، مع قواعد مستفادة من الشمس [20].
- معايير للمرتجع ومعنى الألوان الموجزة في جدولين (الفرعان 11 مكررا ثانيا و 11 مكررا ثالثا).
- المواضيع، 36 [17] languages, export documents to Word with editable formulas [16], portable program and multiplatform [12] [13] [14] [15].

## 1. الغرض

The program puts together in a single window two things that usually look on different sites: the prediction of the tropofer propagation (Tropo index of Hepburn and F5LEN, isobare) and the observation of what really happening on bands (spot from DX cluster, Reverse Beacon Network and PSKReporter, plus the list of stations that are listen). The reference band is 144 MHz; the commercials collect from 50 MHz up.

ويتبع هذا الإطار قائمة المراقبة التي اقترحتها شركة LA0BY في أحداث التراب المتطرفة: ففي الأيام السابقة نظرتم إلى توقعات هيبورن و F5LEN وموجزات درجات الحرارة على ارتفاع، في حين أن الحدث قيد البحث في مجموعة الـ دي إكس، وعلى الموقع يرسم الدليل على أن الافتتاح يبدأ بـ [1]. LA0BY also observes that the only Hepburn/ F5LEN maps do not suffice to predict the longest connections [1]

## 2. النوافذ والقنوات

النافذة واحدة ولا تنقسم إلى نوافذ متحركة من أعلى إلى أسفل:

- General settings row: Call and Locator on the left; on the right the UTC hour (e.g. 08/11/2026 20:33:38 UTC, amber text) followed by NTP:OK or NTP:FAILED, the theme menu and that of the language.
- عدد المقذوفات في خمس مجموعات: الخريطة والموقع؛ والتنبؤات (Propria ß)، والتنبؤات المحسوبة في البرنامج والموصوفة في دليل التنبؤات، ثم هيبورن و F5LEN)؛ وGRAVES؛ والبيانات والمصادر (مجموعة DX، ومورد PSKReporter، وReverse Beacon، و UDP، والتعيينات، والتقارير المشبوهة)؛ ودليل (عملية البرامج، ومبنى الخرائط، وطريقة تشكيل صندوق البحث، والتنبؤات الدليل). وفي إطار الشعاع، تُظهر الحانه النجوم الفرعية للمجموعة؛ وتتذكر كل مجموعة آخر مجموعة مستعملة.
- Content of the tab and, at the bottom, the status bar with the state of the sources, the database of names and the time of the last update.

وتنطبق الأطر الرئيسية على جميع المقادير: فالموضوع المختار يلون أيضا جمع البيانات وخلفية الوثائق؛ ولا تزال صفحات الوثائق بيضاء كصحيفة، بحيث تطبع بشكل جيد.

## 3. Call and landlord

ويمكن أن يكون لمالك مادينهيد 4 أو 6 شخصيات (مثل JN70 أو JN70CN). The program converts it to the coordinates of the center of the square: field of 20° × 10°, square of 2° × 1st, subquadrate of 5 &apos; × 2,5 &apos;. وإذا لم يكن النص موقعا صحيحا، تصبح حافة الصندوق حمراء ولا تظهر المحطة على الخريطة. The locator also serves as a center of the azimuth projection and as a starting point for distances and azimuth in the ad board.

النداء هو الاسم المستخدم كشعار للمجموعة وفي البداية الأولى، إلى أن يكون هناك ترشيح صحيح، لا تتواصل المجموعات: فالنافذة تطلب الاسم (وبصورة اختيارية، مالك العقار)؛ مؤكدة أن الاسم مستنسخ كاسم مستعمل في حقل لوجين لكل مجموعة خالية وتغادر المجموعات. ومع &quot; لاتر &quot; لا يزال قائما، ويحتاج الزر المكون لكل مجموعة إلى الترشيح مرة أخرى. MQTT, UDP and NTP do not use login and leave any.

## 4. جمع البيانات: المصادر

ولكل مصدر فرعه الخاص في حساب جمع البيانات، مع ربط/ربطه، والصناديق المعترف بها والموصّلة عند بدء الصفوف المتلقاة ووضعها ومقاييسها وتسجيلها. ولا ترد في السجل أي خطوط غير مصفورة أو مهجورة من المرشّح: فهي تُحسب فقط مترات القسم. الأقسام جنبا إلى جنب مع اثنين إلى اثنين.

| المصدر | Connection | ما يجلب |
| 1 انتقاء المجموعات | on0dxk.dyndns.org:8000 مجموعة القيادة/المرشاة Dxbm/pass 6 | Spot of amateur radios, also SSB and CW. وفي ON0DXK ، يتم تزييف التردد العالي جداً وتمكّن من القيادة المشار إليها (عبر 4، 2...). |
| 2. المصدر الثاني | dxmaps.com:7300 (DXMAPS, EA6VQ) and s50clx.si:41112 ( S50CLX, Slovenia) | ويجمع جهاز DXMAPS البقع ذات التردد العالي جداً ويعطي أيضاً البقع المزخرفة من طراز RBN (مراقبة الزبدة/المهارة، التي تم تحديدها بالفعل)؛ وشركة S50CLX هي مجموعة من طراز DX سبايدر. إحملي اسمكِ، عناوين قابلة للتجزئة. |
| 3 Reverse Beacon Network | صافـيـة مـن الـغـيـنـيـن الـمـنـيـنـيـنـيـن الـمـنـيـنـيـنـيـنـيـنـيـنـيـنـيـة FT8 | بقعة المزلاج الآلي (لوجين) بالاسم فقط (إكس يو) [9] |
| 4 - المجموعة المُبلَّغ عنها | dm4x.ddns.net:8500 | Digital Spots of PSKReporter in cluster format. |
| 5. PSKReporter MQTT | Mqt.pskreporter.info 1883 · 1884 with TLS | كل تقرير إستقبال في الوقت الحقيقي: من يُرسل، من يستمع ويُصغّل مُراقبي الأشعة السينية. Bands 144, 222 and 432 MHz. [5] |
| 6 خرائط الطقس والقطارات | dxinfocentre.com/tropo_eur.html tropo.f5len.org/forecasts-for-European النموذج GFS | Hepburn [3] and F5LEN [4] tropo index maps, sea level pressure for isobare (GFS via Open-Meteo [18]). ويجب أن يُسمح لصاحبي البلاغ بالسماح لهما. F5LEN |

(المصادر 1-4) وبعد الاتصال، ينتظر البرنامج طلب الاسم (النص الذي يحتوي على الشعار، الاتصال أو الاتصال أو الاتصال)؛ وإذا لم يصل في غضون 4 ثوان، يرجى إرسال السجل. ثم إرسال أوامر القسم، انفصلت عن نقطة و فاصلة، واحد كل 0.6 ثانية. الشخصيات المتحكمه بالهاتفونيه تم ازالتها قبل قراءة الخطوط إذا سقطت الاتصالات و(ريكونيتي) نشطة، حاول مرة أخرى بعد 10 ثواني (15 إذا لم يرد الخادم على الإطلاق) [6]

PSKReporter MQTT (source 5). The program enrolls in the topic pskr/filter/v2/{banda}/{modo}/{tx_call}/{rx_call}/ {tx_grid}/{rx_grid}/{tx_dxcc}/{rx_dx} عن طريق إصلاح الفرقة فقط وترك الحقول الأخرى مجانا مع الشخصية + [5]. The exact name with which the server indicates the bands (for example 2m, 1.25m, 70cm) must be check at the first connection.

## 5. من خط المجموعات إلى البقعة

One line is a spot when it has the form DX de SPOTTER: frequency DX comment hhmmZ. ويستمد البرنامج موقعا، ومحطة DX، وتواترا في KHZ، والتعليق، والآن UTC؛ والأسماء تزيل أكياس المزمار (-#، 1...). ثم تطبيق، في هذا الترتيب، القواعد التالية [6].

| Band | تم قبول الجزء |
| GRAVES (with 144) | 040 143 - 060 143 ميغاهيرتز |
| 144 ميغاهيرتز | 144 - 148 ميغاهيرتز |
| 222 ميغاهيرتز | 220 - 225 ميغاهيرتز (الإقليم الوحيد 2 للاتحاد الدولي للاتصالات والأمريكتين) |
| 432 MHz | 420-450 MHz |
| 1296 MHz | 1240 - 1300 MHz |
| 2320 MHz | 2300 - 2450 MHz |
| 10 GHz | 10,5 GHz |

1. الفرقة ويجب أن يندرج التواتر في أحد القطاعات المدارة؛ أما بقية القطاعات، ولا سيما كل إعلان تجاري يقل عن ٠,٩٠ ٤٩ كهرتز، فلا يظهر ويحسب على أنه &quot; خارج النطاق &quot;.
2. الطريق. One of the best known ways of commenting is FT8, FT4, Q65, JT65, ISCAT, CW, SSB, FM, RTTY, PSK31, MSK144, FSK441, JT6M); USB and LSB become SSB.
3. تصفية سلبية The spot is discarded if it contains the modes MSK144, FSK441, JT6M, JTMS, ISCAT or the tags OTAUE. ES. MIS. MIS. MIS. MIS. MIS. MISI. F2., with non-sensitive uppercases and even spaces within الأقواس. هذه هي الطرق والتشغيلات التي لا تربو: نيزك متقطع، أورورا، ومتقطع، F2.
4. مفرقعات الهواء If the comment contains AS. or ACS. (also mixed, e.g. As.) or declares the spot as airscatter (airscatter, air scatter, aircraft scatter), the spot remains but is drawn in zero with dotted line (dark wavy to stay readable on the clear map) and highlighted in the list.
5. موقع المحطات إذا كان هناك في التعليق اثنين من أجهزة تحديد المواقع (مثل JN54 JN83) الأول من أولئك الذين يصنعون البقعة والثاني من المحطة DX ما هو مفقود هو البحث في C ALL3. TXT, only with the exact name (for computers see below). وفي حالة بقاء محطة ما دون موقع، ترد في القائمة ملاحظة &quot; موقف غير معروف &quot; وليس على الخريطة.
6. مزدوج ويستعيض عن ذلك الإعلان التجاري الذي يحتوي على نفس الـ دي إكس، ونفس الموقّع، ونفس النطاق، ونفس المصدر للحاضر الحالي.

C ALL3.TXT is the name database. locator used by JTDX, in the format CALL,LOCATORE,,, The project file contains 1 048 847 names, all with valid locator; the header of the file still speaks of 34 323 names because it remained that of the first edition [7]. إنها محشوة في الخلفية في البداية لذا فالنافذة يمكن استخدامها فوراً الملف يتم تفتيشه بجانب البرنامج، في ملف البيانات، في الملف الذي بدأ منه البرنامج، وملف واحد أو اثنين من الملفات فوقه؛ وإذا لم يكن هناك، فالنافذة تُخبره عندما تبدأ. دائماً ما يستخدم إسمه 6 سمات القاعدة الوحيدة للملاحين: إذا كان الشخص المستلم (الموقع، MQTT، الزينة، QSO desunti) أقل من 6 شخصيات و C ALL3. TXT has one to 6, it is always worth the C ALL3.TXT; for computers (F/ IC8TEM, IC8TEM/P) is only an exact voice: without, the station remains without a landlord and is not designed elsewhere. وتنطبق هذه القاعدة أيضا وقت رسمها، ولذلك فإن البيانات التي يتم التوصل إليها قبل تحميل قاعدة البيانات ستسير على نفسها.

قاعدة بيانات واحدة: C ALL3.TXT. From 1.3.4 all locators are in one file, C ALL3.TXT, which remains in the form of JTDX and WSJT-X (CALL,LOCATORE,,,,), and can be used as it is also in WSJT-X, JTDX and MSHV. لا يوجد المزيد من الملفات منفصلة At the top of the file, after the header, there are two blocks marked by a comment line: / [LIVETROPOVHF-MANUALI] with the entries inserted by hand and // [LIVETROPOVHF-CLUSTER] with the corrections from the cluster; then // [LIVETROPOVHF-FONTI] with all other items in alphabetical order. كل ترشيح يظهر مرة In the sublingual Data and Sources. appointmentss (C ALL3) you look for a name, you write the landlord and you press Add / change: the voice goes in the block of the voices by hand and it is worth above all, even above the locator that the station itself sends.? يزيل الترشيح من C ALL3.TXT (يستأنف التحديث التالي الترشيح من المصادر، إذا كان لديهم). أضف ملفاً... يقرأ قائمة (كال) و(لوكتور) (وأنت تقبل أيضاً النقطة والتواصل والكتابة والحيز) وتكتبه بين الأصوات المصورة يدوياً؛ والملف المختار غير مستنسخ. اعادة تحميل اكس4Q.TXT من القرص بعد تحريره مع محرر النظام الكامل للمصادر: جهاز تحديد المواقع، أصوات اليد، تصويبات المجموعات، ما تبقى من C ALL3. TXT (sources, learnt live locators and G7VRD), received locator (which applies over the database only if it has 6 characters and does not end with LL or MM). ALL3

Update of C ALL3.TXT. In the same sub-language the Update blue now downloads the sources (C ALL3.TXT of WSJT-X improved, C ALL3.TXTME of W7GJ, grid_data.bin of JTDX), joins them (the first that has a nomination win), check that they are at least 50,000 names (oise nothing changes), saves the C ALL3. TXT C ALL3 السابقة ولا تزال قيدات TXT باقية، ولا يزال جهاز تحديد موقع 6 سمات حيث لا يملك المصدر سوى 4 من نفس المربع. ولا تزال الأصوات اليدوية والتصويبات من المجموعة في القمة دون تغيير. مع صندوق التحديث الخاص به كل يوم يتم الشيك في البداية The program rewrites C ALL3. TXT immediately after each voice by hand and, for learnt locators, at most every 10 minutes and at the closing; the C ALL3. الـ (تي إكس تي) من الرزمة يُقرأ فقط و تُزيل السمة نفسها (لينكس) لا يتعدى على (سي إكس 9Q) (تي إكس تي) موجود بالفعل و(ويندوز) سيثبته فقط إذا كان مفقوداً ALL3 ALL3

التصويبات الآلية من المجموعة. In the DM4X and RBN automatic spots the comment brings two locators, for example KN09tplate the KN09: the first is what the station that spots declares itself, to 6 characters; the second is that of the station felt, often only to 4. If the first has 6 characters (and does not end with LL or MM) and is different from that of the database, the program corrects C ALL3. TXT alone: the correction goes in the block // [LIVETROPOVHF-CLUSTER], with the date of the last confirmation in the third field, it is valid above the sources (under the hand voices) and appears between the suspicious signals updated as (C ALL3). إذاً فهو ينجو من (سي إكس 4Q) TXT تحديثات. ويفقد التصويب غير المؤكد لمدة سنتين الأسبقية ويظل بمثابة صوت عادي. ALL3

المصدر الأخير: G7VRD. إذا لم يكن لدى المرشّح 6 سمات في C ALL3. TXT, the program asks it in the background to the public API of G7VRD (api.g7vrd.co.uk, "locator by callsign"), which collects locators automatically from network traffic. لا تقبلون سوى 6 سمات ليست مركز المربع (LL أو MM)؛ الطلبات مجمّعة، بحد أقصى 40 مرشحاً في الدقيقة، وكل طلب ترشيحي يطلب مرة في اليوم، لأنه خدمة هوائية بدون ضمانات. لقد عثر على الموقع سي إكس 3Q TXT If the name is not there or has only 4 characters of the same square; the search turned off with the box in the name sublingual. ALL3

تعيينات مع البار In the PSKReporter MQTT topics, the bar separates the levels, then the compound names come with a point instead of the bar ( IV3. HB9CAT). The program puts the bar back and, if the message contains the full names, use those: IV3/HB9CAT remains IV3/HB9CAT on map, list and database.

الملوك تعلموا الحياة C ALL3. ولا تحتوي TXT على كل شيء: فعلى سبيل المثال، لا توجد SP9EMF و SP9NVY ، ومن ثم لا تزال آلة تحديد المواقع التي ترسلها شركة PSKReporter (البطاقة تقول ذلك). لتقليل هذه الحالات يكتب البرنامج في C ALL3 TXT every 6-character that comes from any source (PSKReporter, such as transmitter or as receiver, cluster, UDP decoding) for a nomination that C ALL3. TXT does not or has only 4 characters in the same square; never changes a voice to 6 characters. وحتى ١,٣,٣ كانت هذه أجهزة تحديد المواقع في البيانات/الأشعة السينية LOC6.TXT.

الانتقال من 1.3.3. في البداية الأولى من 1.3.4 الملفات بخلاف النسخ السابقة (Dati/C ALL3 -MANUALE.TXT, the Data/C ALL3 -AGIUNTE, Data/C ALL3 -CLUSTER. TXT, Data/ LOC6.TXT and Data/C ALL3 - G7VRD.TXT) are merged into C ALL3. TXT مع نفس القيود. تحرير الكتابة، الملفات القديمة تأخذ التمديد فوسو، كنت لا القراءة بعد الآن ويمكنك حذف.

جهاز استقبال آلي RBN, dm4x and PSKReporter often report names with a suffix indicating the receiver: -SDR, /SDR, -# (skimmer RBN) or -1, -2... (بالإضافة إلى أجهزة استقبال نفس المحطة). أما بالنسبة لمالك العقار فقد أُزيلت منه: فإكس0Q-SDR يأخذ OK2OZL JN89UE من C ALL3.TXT إذا لم ترسل المحطة بالفعل موقعاً للمفاعلين الستة. الإسم المبيّن لا يزال هو الذي يحمل الـ"سوفيكس". OK2OZL

قطع الملوك. ولا تعالج جميع المصادر توسيع نطاق ملاكها العقاري: فقد حددها العديد من المصادر إلى أربعة خصائص، وتتعمد بعض نظم النقل التلقائي فرز موقع المفاعل الأربعة، حتى تلك التي تحصل على البيانات من الاستماع الحقيقي إلى FT8 و FT4 و Q65 و JT65 ، أو تكملتها مع مركز المربع (L أو MM، مثل JN89MM). ولهذا السبب، يُستعاض عن المصنّع 4، أو 6 مصنّع ينتهي بL أو MM، برقم C ALL3. TXT من نفس مربع. وإذا لم يتزامن الموقع الصحيح ومركز المربع مع بضع عشرات كيلومترات، يظل الخطأ صغيرا مقارنة بمسافات تربو.

## 6. من يستمع

كل رسالة من شركة (بي إس كي) أبلغت عن اسم وموقع من يستقبلون (إكس تي) لذا لا تحتاج للبحث عنها في (سي إكس 1Q) وبالنسبة للفرقة التي اختيرت في المختار، يحتفظ البرنامج، لكل محطة استقبال: مالك العقار، الاستقبال الأخير، الطرق المتدهورة، عدد المحطات التي شعرت بها والمسافة القصوى. وتغادر المحطة القائمة عندما لا تتلقى أي شيء في النطاق المختار (3 أو 5 أو 10 أو 15 أو 30 أو 60 دقيقة). [5] ALL3

على الخريطة كل جهاز استقبال هو حلقة وردية سوداء فارغة، ومن كل بداية خطوط وردية كاملة نحو المحطات التي تشعر وترسل إلى الشبكة. تُرى الخطوط دائماً، بدون النقر. وتُكتب دائما على الخريطة تعيينات مراكز الاستماع، وأولئك الذين يُستمع إليهم، ورأسي كل بقعة، دون أن يضطر إلى النقر (يُكتب نفس الاسم في نفس النقطة فقط). من خلال نقر دائرة أصبحت خطوطها أكثر سمكاً وفتحها بالتفاصيل If the locator from MQTT has less than 6 characters, the program uses that of C ALL3.TXT (about one million names), provided it is consistent with the square received.

النيازك As with cluster spots, MQTT ratios in MSK144, FSK441, JT6M, JTMS or ISCAT are discarded and counted only; the same goes for UDP decodings in these ways.

ليس مستمعين حقيقيين Some stations declare to receive in 144 MHz but in reality the receiver is on another band (wrong frequency indication): their reports would design unexisting openings of thousands of km. البرنامج يفحصه بنفسه وتُتبع كل محطة يستمع إليها مستمع في التردد العالي جداً لمدة 30 دقيقة على جميع الفرق، مع تسجيل مخصص لشبكة MQTT (pskr/filter/v2/+/ {nominative}/ب) [5]. وإذا كان هناك على الأقل محطتان من المحطات التي يستمع إليها المستمع، في نفس الدقائق (10)، تعملان على نطاقات أخرى، فإن المستمع يعتبر غير حقيقي، ويتم التخلي عن جميع تقاريره ذات التردد العالي جدا؛ ويفيد سجل MQTT عن الاسم والمحطات التي خانته. In the list of spots, next to the meters, the MQTT blue shows the state of the flow (green leaf if connected, red if not) and how many listeners were discarded in the band and range chosen; by reving it opens the tab with the list of discards and, for each, stations found on other bands.

اتصال طويل A link over 1000 km (Clumbing Spot, MQTT listening, QSO Desunti) is not immediately shown: remains held, and the counter “held. 1000 km” says the number. ويكشف عن وجود صلة أخرى، بين مختلف المحطات والنصف على الأقل، تؤكدها مع أحد هذه المعايير: نفس المحطة في الرأس (على سبيل المثال نفس المستمع) والمحطة الأخرى في نفس الاتجاه في حدود 10 درجات؛ وممر مماثل، على أن يكون الرأسان على بعد 200 كيلومتر من الموصل الآخر؛ والطريق الموازي، مع الاتجاه في حدود 10 درجات وبُعد جانبي في حدود 200 كيلومتر، يتداخل على طول الطريق. كلاهما ظهر وسجل MQTT يظهر السبب. وتظهر رموزها المتعلقة بالتشرد غير الشامل للخدمة دائماً وتطبق بوصفها تأكيداً للآخرين. ولا تزال الاتصالات المحتفظ بها في الذاكرة لمدة 60 دقيقة.

لا شيء تحت 144 ميغاهيرتز يعمل البرنامج من 144 ميغاهيرتز: كل شيء تحت 143 ميغاهيرتز يتم التخلص منه. والاستثناء هو &quot; GRAVES &quot; ، أما الرادار الفرنسي الذي يُنقل في CW في 050 143 ميغاهيرتز من قبل &quot; Broye-lès-Pesmes &quot; ( JN27SI) فيبين 040 143 و060 143 ميغاهيرتز، أو مع GRAVES بالاسم، فإنه يظهر بين 144 ميغاهرتز بالرادار كمرسل. The ad board says if the receiver is inside the beam (sectors D, C, B, A from 90° to 270° of azimut seen from the radar, 6 steps from 7,5° each) or out; the tab GRAVES, as in YA-MS, shows the sky seen from the radio: sectors, step illuminated at that time (sequenza PE1ITR 2021: 0,8 s الشعور به في التراب صعب لكنه يمكن أن يحدث

إعادة التأهيل من المجموعات البشرية. A station that also appears on other bands does not count as suspect if, in the same minutes (30), the DX human clusters ( ON0DXK, GB7RAU; not dm4x, RBN or other automatic sources) the active damage on the band in question, in spots made by other names: it is the case of stations that really work on multiple bands together. ومن ثم، فإن عودة المستمعين المختفين إلى الظهور (يكتب سجل MQTT &quot; إعادة التأهيل &quot;) كما يمكن فتح وصلة بعيدة المدى للفرق المتدنية.

محطات نشطة تحت 144 ميغاهيرتز. In the quarantine of long connections, a station that in the same minutes is heard on bands under 144 MHz (from the MQTT reports of other bands) does not confirm other connections and is not unveiled, although a similar or parallel route would confirm it.

## 7. قائمة بالإعلانات

ويحتل 35 في المائة من الحق في تابوت ونقطة. الموضوع: العنوان، مختار الفرق (50، 70، 144، 222، 432، 1296، 2320 ميغاهيرتز، 10 جيهرتز)، مختار نطاقات (آخر 3، 5، 10، 15، 30، 60 دقيقة) ومفتاح آخر. أسفل، أربعة أمتار: بقعة مرئية، AS/ACS، تم التخلص منها من مرشح، محطات الاستماع.

ويبين كل خط نوع الخط، والوقت الذي يمر به مركز التجارة الدولية، وDX. DE، والطريق، والمسافة في الكيلومترات، وعلى الخط الثاني، المصدر والتواتر والتعليق. الأحدث على القمة نقرة واحدة على الخط تختار البقعة: على الخريطة ينمو الخط ويظهر الاسمان.

## 8. فقدان البطاقات الاصطناعية

وبالنسبة للموقع المختار، يُظهر التابوت أجهزة تحديد المواقع، والمسافة، والزيموت، والتواتر، والطريقة، والمصدر، الآن، والتعليق، ومتوسط مؤشر التروبو على طول الطريق، وتقدير لفقدان الطريق في التراب المتناثر.

ويُحسب التفكك والزيموت على أقصى دائرة مع نصف قطر الأرض البالغ 6371 كيلومتراً (أشبه جزيرة اليميسينوفرسو للمسافة، والزيموت الأولي من محطة DX إلى الموقد).

(فقد (تروسكاتر والخسائر الأساسية، بالنسبة لمحطتين على أرض سلسة، هي مجموع الخسائر في الفضاء مجاناً والخسائر في النشر: G3YGF [2]

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

والخسائر في الـ دي بي، والـ دي هي المسافات في الكيلومترات، والتردد في ميغاهيرتز، وزاوية الانتشار في الدرجات.

8497 الكيلومترات هي نصف قطر الأرض المكافئ (4/3 من الجسد يُعتمد على أفق كل محطة:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

مع ارتفاع العقبة من الأفق فيما يتعلق بالمحطة ومسافتها كل مستوى من الآفاق يكلف حوالي 10 ميغاواط The program does not know the horizon of the stations, so it shows the basic loss with ÉA = ÉB = 0 and declares it ( &quot;smooth ground &quot;). G3YGF adds a climatic term, where N is the reactivity to the [2] soil; it will be used when N is available from the weather model: [2]

```math
L_{N} = 0,2 \cdot (N - 310)
```

التحقق: بالنسبة لـ 110 كيلومتراً إلى 144 ميغاهيرتز، تعطي الصيغة 5 166 دي بي، مقابل نحو 168 سريراً من سداسي البروم على خريطة المادة. من 500 كيلومتر إلى 144 ميغاهيرتز تعطي حوالي 206 دي بي. The expected signal/noise ratio is the capacity of the system (eirp + effective sensitivity of the receiver) minus the loss: with the G3YGF 144 MHz model system (198 dB) a 500 km path would give about −8 dB in SSB [2], that is the connection is only with better propagation of the pure scatter.

## 9. رموزي: UDP من WSJT-X, JTDX and MSHV

WSJT-X, JTDX and MSHV can send UDP every line they decode. The program listens on 127.0.0.1:2237 and repeats the same package, byte perte, on 127.0.0.1:2238, so the other programs that used that port (GridTracker, JTAlert, a logger) continue to work if you move them on 2238. وتتغيّر العناوين والأبواب في اللغة الفرعية لمجموعة البيانات، ويمكن أن تستخدموا أيضاً، كعنوان الاستماع، مجموعة متعددة البث (مثلاً 224.0.0.1)، إذا تم وضع برنامج التزيين على هذا النحو.

وتلي الحزم بروتوكول شبكة WSJT-X: وهو رقم سحري أولي رقم 0xADBCCBDA، ورقم الكيماوي، ونوع الرسالة. The programme mainly uses three [8] types:

- القلب يقول أي برنامج يرسل البيانات وبأي نسخة
- الحالة: تواتر الاتصال والأسلوب؛ من التردد تحصل على الفرقة (مع نفس الجدول من البقع).
- Decode: Now, SNR, time and frequency waste, manner and text of the decoded message.

مِنْ النصِ تَحْصلُ على المحطةِ سَمعتَ: في CQ 9 A2PQR JN83 is the station after CQ (saving DX, EU, TEST...), in IK0JKL IT9MNO JM68 is the second, i.e. who transmits. الموقع هو الرسالة، إذا كان هناك (4 شخصيات، مختلفة عن RR73) TXT. هم يَستمعونَ: المحطات التي يَسْمعُ متلقيكَ الآن. على الخريطة يصبحون خطوط حمراء من QTH الخاص بك إلى المحطة، مع الاسم؛ الطبقات تضيء مع الصندوق المقلد من لي وتتطلب موقعا صالحا. في القائمة على اليمين يُخبرُ a عدّة محطاتِ أنت تَحْدُّث على الفرقةِ المُختارةِ. ALL3

QSO desunti dal يعيش. وعندما يكون التزيين هو رسالة ختامية في الاستمارة C ALL1 C ALL2 RR73 (أو 73 أو RRR)، مثل IC8TEM IK3FHP RR73 أو IK3FKP SV8PEX 73، استكملت المحطتان وصلة. The program records it as QSO, with the locator of each taken from the last message in which he declared it or, if not, from C ALL3.TXT [7], and draws it on the map as a green line between the two stations (strate “QSO desunti dal live”). ويُعدّ زوج من المحطات مرة واحدة لكل فرقة؛ أما النطاق والمجموعة المختارين في القائمة فهي صالحة.

وفي نفس اللغة الفرعية، تُظهر &quot; ديبوغ بان &quot; نص كل حزمة مستلمة (نوع وزمان وقيم ورسالة)، بناء على الطلب، أيضاً بوصات سداسيّة، وخطاً لتقديمها. من أجل التحقق من أن برنامج التزيين يرسل البيانات

## 9 مكرر اللغات Hepburn and F5LEN

كُلّ يُظهرُ الذي البرنامج يَأْخذُ مِنْ المصدرِ. ويفتح الزر &quot; اعادوا الصفحة وحملوا جميع الصور &quot; الصفحة المشار إليها (استنادا إلى مركز dxinfocentre.com/tropo_eur.html من أجل Hepburn [3] و tropo.f5len.org/forecasts-for-europe for F5LEN [4]) ويحمّل جميع الصور المخبأة إلى جانب التنفيذ؛ ويمكنكم أيضا أن تجتازوا عنوان صورة واحدة. لكل صورة هناك صندوق لاستبعاده وميدان مع وقت التنبؤ أما الصور الأوروبية الـ 31 للمعيار F5LEN فتُسمَّى وقت السباق النموذجي (03.png... 48.png every 3 hours, 54... 96 every 6 hours, 108... 192 every 12 hours) ويستغرق البرنامج الوقت من اسم ملف [4]; for Hepburn it is worth the order of the page. In the F5LEN tab, a menu chooses the region: Europe (tropo.f5len.org/forecasts-for-europe), Australia (tropo.f5len.org/Australia, vk03.png... vk192.png) or Atlantic between Cape Verde and Ireland (tropo.f5len.org/atlantic-ocean/atlantic-d4-pei, atl03. الجميع لديه نفس الخطوات

أسفل هو مضاد المعايرة. نقرة واحدة على الصورة تضيف نقطة تحكم (الموقع أو السلالون: تحتاج على الأقل 6) أو لون أسطوري بقيمة The program calculates the pixel matching. coordinates with a second degree polynomial interpolation at the smallest [19] squares, reads the colors with the tolerance chosen and, for F5LEN, converts the units N in the 0-10 scale with a · N + b. وتكتب &quot; معايرة الإنقاذ ومحاولة القراءة &quot; الملف في ملف تاروت وتحقق من أي ملف يقع في موقعك. وقبل إنشاء معايرة، ينبغي طلب الإذن إلى صاحب الخرائط.

المناطق. ولكل مصدر المزيد من المناطق، ولكل منها صور لقب ثابت: أنا آسفة ويتبين من قائمة كل مصدر أولاً من جميع استعراضات الدمج: جميع الصور المحملة التي تم تجميعها على الخريطة الرئيسية، مع المقلاة المهبلة، واسم كل منطقة، وعجل رمادي لا توجد فيه صورة تغطي المنطقة، وزلاجة تخترق الساعات؛ وترك قائمة المناطق، وكل منها مع الصندوق الذي يستخدمها، وعدد الخرائط التي يقرأها، ومن أين تأتي المتطرفون (المصنع). ولا يزال مقعد المعايرة اليدوي وراء الزر المهجور. وتختار قائمة الطعام في منطقة مجلس الشيوخ النظرة التالية: &quot; استخدام هذه المنطقة على الخريطة &quot; يشملها الرقم القياسي. ويُعثر على الإطار وحده)خطان أفقيان وخطان عموديان من نفس اللون(؛ ووضع الصورة تخدم الرطل/اللون المتطرف للإطار: بالنسبة لأوروبا الهلبورن)١٠ درجات غرب و ٤٠ درجة شرقا و ٢٧ درجة و ٥٦ درجة شمالا( وأوروبا)١٥ درجة غربا - ٤٠ درجة شرقا و ٣٠ درجة - ٥٦ درجة مئوية(. N) are already in the program, for the other regions they write in the four O E S N fields reading the axis labels. ويجعل مضاعف الجدول المصدرين متشابهين: القيمة النهائية = القيمة المضاعفة × الأسطورة. [3] F5LEN [4] F5LEN F5LEN

## 9 مكررا ثانيا Export in Word

وتوجد الوثيقتان فوق زر &quot; التصدير إلى كلمة &quot;. The file is written in the Office Open XML format; the formulas become Word equations (OMML), so they remain editable with the [16] equiation editor. وإذا لم تكن نافذة الإنقاذ متاحة، ينتهي الملف في ملف الوثائق بجوار ملف التنفيذ.

## 9ربع. من يناديني

إذا أُرسلت رسالة إليكِ على شرطة (لوس أنجلس) المحلية (الاسم الأول لكِ، على سبيل المثال IC8FAX JN55 أو IC8FAX IK3FHP -12)، البرنامج يتعقبكِ من موقعكِ إلى المحطة التي تُسميكِ خطاً أسوداً ومضيء، مع الاسم والموقع بجواركِ. The locator comes from C ALL3.TXT (including hand voices) or message. The line remains until the UDP passes the closure of the QSO with that station ( RR73, RRR or 73, in one of the two directions), at most 3 minutes from the last call; you draw over everything and do not follow exclusion criteria, so a direct call never escapes. IK3FHP

PSKReporter, via MQTT, does not transmit the text of the messages: says only who received who, with frequency, way and SNR. وهذا هو السبب في أن الدعوة المباشرة لا تعترف بها سوى رموزها الخاصة بالحزب الديمقراطي الموحد.

## 10. مراقبة الوقت

النمط الرقمي والوقت المحدد يتطلب ساعة دقيقة ويستجوب البرنامج كل 3 دقائق 10 خواديم من طراز NTP (SNTP on UDP 123): 0.pool.ntp.org, 1.pool.ntp.org, 2.pool.ntp.org, it.pool.ntp.org, ntp1.inrim.it, ntp2.inrim.it, ptbtime.0 [11]

وفيما يتعلق بكل جواب يحسب المرتجع وتأخير العودة، ويتخلص من القيم الشاذة ويأخذ الوسط، مع حساب المرتجع والتأخير الذي تقدمه شركة SNTP [10]. With least 3 responses agree the UTC hour of the program aligns and next appears NTP:OK in green; if the answers are not enough appear NTP:FAILED in red and the hour remains that of the computer. ولم يتم لمس ساعة النظام، لأن هناك حاجة إلى إذن إداري؛ فجميع جداول البرامج الداخلية (البؤر، والنوافذ من 5 إلى 60 دقيقة، والزيارات) تستخدم وقتا متوائما. وتدور المراقبة دائما في الخلفية: ففي الواجهة لا ترى إلا النتيجة التي تقارب الساعة؛ والخواديم، والفترات، والحد الأدنى من الردود في LIVETROPOVHF.ini (المستودعات، ntp_دقائق، ntp_min).

## 10 مكرر. معايير الاستبعاد العامة

صندوقان في حانة الطبقات ينطبقان على كل ما ترسمه (المُصدر، المُقدّم (بي إس كرايتر

- باستثناء أقل من 100 كيلومتر. وقد لا يكون الاتجار بين المحطات التي تقل مساحتها عن 100 كيلومتر ذا أهمية بالنسبة للمجموعة (الموجة الأرضية، النطاق البصري): مع المجموعة التي لا ترسمها ولا تظهر في القائمة. البداية انتهت
- أكثر من 1000 كيلومتر: مربعات فقط. في جميع الاتجاهات هناك تاج على بعد 1000 كيلومتر وإذا كانت إحدى محطتي الرابط خارج هذا التاج، فإن الصلة لا تُرسم كخط: ففي مكانها تُلوّن في الليلاك، وتُسقط عند 45 درجة، فإن المربعات (4 سمات) من المحطات المعنية. وينطبق الشيء نفسه على محطات المبلِّغين التابعة لحزب العمال الكردستاني التي تُستمع إلى أكثر من 000 1 كيلومتر، والتي تبدو مربعة بدلا من كونها دائرة. حتى الآن حركة المرور ترى دون ملء خريطة الخطوط. فبدون وجود موقع صالح يحسب طول الصلة. مربع القوث الخاص بك لا يلون أبدا في ليلاك، حتى عندما تستمع إلى محطة خارج التاج: هو الموقع المحدد، أي استخدام الاسم الذي يعمل من المحطة. ويُختار نطاق التاج بجوار الصندوق: 1000 أو 700 أو 500 كيلومتر. The box Corone 1000/700/500 km draws the three dates around the QTH, drawn in lilac, with the most marked choice and the distance written north. في قائمة البقع تليها المسافة البداية بدأت وينطبق المعيار بعد الحجر الصحي: فالوصلات الطويلة التي لا تزال محتفظ بها لا تظهر حتى كمربع.

ما هي مربعات الليلاك التي تهبط عند 45 درجة. الإزدحام بعيداً عن قوثكِ يُثير اهتمامكِ لفهم مكان الإنتشار، لكن خطوطه، مئات الكيلومترات الطويلة والعشرات غالباً، ولهذا السبب، فبعد التاج المختار، لا يرسم البرنامج الخطوط بل يلوّن المربعات المركّبة (2° × 1°) من المحطات المعنية. والنتيجة هي خريطة &quot; المناطق النشطة &quot;: حيث يظهر العديد من مساحات الليلاك القريبة، توجد قناة أو فتحة، حتى وإن لم تكن تهمك مباشرة؛ وإذا اتسعت المنطقة نحوكم، فإن الإشارة تشير إلى الهوائي في هذا الاتجاه. لوح الـ 45 درجة يسمح لك برؤية تحت الرقم القياسي للمجموعات والشبكة، لذا مقارنة بالتوقعات (اللون القاع) والحركة الحقيقية (اللوتس). ملعبك لا يلون أبداً لأنه نقطة البداية وليس منطقة بعيدة فالمساحة فقط التي تتجاوز التاج ملونة: فالمربع الذي يعبره التاج، أو داخله، لا يزال حرا حتى لو كان هناك محطتان. لمشاهدة الخطوط، فقط اغلق الصندوق أو اختيار تاج أكبر.

## 11. المواضيع واللغات

Themes: the 18 common to IC8TEM programs, the same as YA-NEC and E-LAYER ANALYZER: Clear, night, Black, eight pastels (blue, Portuguese, aquamarine, green,زهر, lilac, indigo, Brown), Scia meteorica (YA-MS palette), the three of IP-TRX-CTRL (Chiaro, Notte, Nero) and Blue, Purple, Green. من 1.3.4 تشيارو يبدو مثل YA-NEC: الخلفية الرمادية الخفيفة، الألواح البيضاء المتاخمة، تابوت الليل الأزرق واللكنة الزرقاء. في مواضيع مظلمة أيضاً تصبح خريطة القاعدة مظلمة ولا يزال حجم الألوان في التوقعات هو نفسه في جميع المواضيع. على شاشات ضيقة (دون الساعة 1500 بيكسلز، على سبيل المثال 1366 ×768) لا تظهر سوى الساعة، لذا لا يزال موضوعها، وشعار النصوص واللغة مرئيا. Languages: the 28th wiki of YA-MS [17] (English, Travel, Deutsch, Nederlands, Français, Español, Português, Dansk, Norsk, Svenska, Íslenska, Eesti, Lietuvi, Latviešu, Суский,,кра window,нсвка, TürQ وتتعلق الترجمة بالتفاعل؛ ولا تزال الوثيقتان الفنيتان ورسائل شريط الحالة باللغة الإيطالية. وتأتي أسماء الأيام من الظروف الدولية. نيت لللغه المختاره [12]

Theme and language change from the two menus on the top right, next to the hour, and apply immediately, without rebooting the program and without interrupting the collection of data. الإختيار يتم إنقاذه في (ليفتروبوف)

## 11 مكررا. التقارير المشبوهة وتقارير التعلم الذاتي

مجموعة التقارير المشبوهة تجمع كل شيء يتحكم فيه البرنامج يجده غريباً متجمعاً حسب نوعه واسمه The program learns by itself: for each couple (type, name) counts the positive results (+) and contrary (−), and after 3 agreed outcomes creates an automatic rule; an contrary outcome puts it in question. قرارات المستعمل (جيدة، فقيرة) تُطبق دائماً؛ يُعيد زر (بولسنتي) القرار إلى البرنامج. وترد القواعد في نظام البيانات/النشرة. TXT, next to the program; the automatic ones not reinforced for a year are forgotten.

| النوع | عندما | Exit + / − | القاعدة |
| EME ( Q65/JT65) | Q65 or JT65 over 1000 km with the Moon above the horizon to both heads, or EME in comment | - في كل مرة يكون القمر مشترك | Discard: EME station, its long Q65/JT65 s are also excluded without common Moon. جيد: يعامل على أنه تراب |
| اتصال طويل | أكثر من 1000 كيلومتر في الحجر الصحي | + unveiled, − expired without confirmation | جيد, طول المحطة يظهر على الفور أبداً |
| الاستماع | جهاز استقبال للمراسلين الذين يستمعون إلى محطات نشطة على نطاقات أخرى | − discarded, + rehabilitated by human clusters | جيّد: أبداً مَرْفضُ. دائماً |
| مختلف ملاك الأراضي | Square received different from that of C ALL3. TXT | كلما تكررها | جيّد: إنتقلت المحطة، إنها تساوي المربع المستلم. Discard: vale C ALL3.TXT |
| Square Center | 6 مصنفات مع LL أو MM |  —  | المعلومات فقط |
| جهاز استقبال آلي | تعيين مع - SDR، /SDR، |  —  | المعلومات فقط |

EME. Q65 and JT65 are the ways of communication via Luna: من الناحية النظرية، فإنها تدخل في شرط الاستبعاد مثل نيزك النثري، لكنها تستخدم أيضا في التراب. The program calculates the position of the Moon with the low-precision formulas of the Almanac Astronomical (about 0.3°) [20]: If a connection in Q65 or JT65 exceeds 1000 km and the Moon is above the horizon to both heads, it is almost certainly EME and is excluded (it counts among the excluded). وإذا لم يكن القمر شائعاً، فإن الصلة لا تزال ممكنة في التراب وتمر عبر الحجر الصحي للوصلات الطويلة، ولكن الإشارة لا تزال مرئية. It applies to clusters, MQTT and its UDP decodings.

## 11 مكررا ثانيا معايير الاتصالات، واحد تلو الآخر

البرنامج يُريدُ فقط أَنْ يُشوّفَ الترابويجيَ فقط في 144 ميغاهيرتز وما بعده. وتمر كل بيانات واردة (Cluster Row, PSKReporter Report via MQTT, UDP decoding) عبر سلسلة من مرشحات، دائما بنفس الترتيب. ولا تختفي البيانات المهجورة في أي شيء: فهي تُحسب (في حالة المستبعدين في الحانة العليا) وعندما يكون السبب مشكوكاً فيه، ترد في التقارير المقيدة.

|  N.  | المعايير | أين هو؟ | ما تم التخلص منه | هل يمكنك أن تتغير؟ |
|  1  | Band | المجموعة، MQTT, UDP | All under 143 MHz (also 50 and 70 MHz), except GRAVES 143,050 MHz. The bands held are 144, 222, 432, 1296, 2320 MHz and 10 GHz | لا |
|  2  | ساتل QO-100 | المجموعة، MQTT, UDP | ٢٤٠٠,٠-٢٤٠٠,٥ MHz and 10489,5-10490 MHz: It is geostationary satellite traffic, not tropo | لا |
|  3  | مقطورة مائية | المجموعة، MQTT, UDP | JT6M and ISCAT (also ISCAT-A/B), comments with &quot; and non-tropoxy propagation labels (AU-, AUE-, AUE-, ES-, FAI-, MISI-) always. MSK144, FSK441 and JTMS (also with troMS-) only over 250 km: within 250 km we accepts, because at that distance F2 | لا |
|  4  | EME (via Luna) | المجموعة، MQTT, UDP | EME written in the comment; Q65 or JT65 over 1000 km when the Moon is above the horizon to both heads; stations learned as EME | نعم: حكم جيد في الإشارات المشبوهة |
|  5  | مستمع غير طبيعي | MQ | جهاز استقبال تابع للحزب الشيوعي النيبالي (PSKReporter) الذي يقول أن يستمع إلى 144 محطة تعمل في ذلك الوقت في فرق أخرى (على الأقل محطتين): علاقاته معطلة العودة جيدة إذا المجموعات البشرية DX تؤكد نفس المحطات | نعم: جيد/ فقير |
|  6  | Quarantine of long connections | كل الخطوط | ويظل هناك ما يزيد على 000 1 كيلومتر من الرابط مخبأا حتى يجد تأكيدا: نفس المحطة في نفس الاتجاه، وطريق آخر في ممر يبلغ 200 كيلومتر، أو طريق مواز في حدود 10 درجات و 200 كيلومتر على طول النصف على الأقل. بدون تأكيد ينتهي بعد 60 دقيقة | نعم: جيد/متجه إلى المحطة |
|  7  | أقل من 100 كيلومتر | الخطوط والقائمة | مع المجموعة، أقصر وصلات تبلغ 100 كيلومتر (الموجة الأرضية، النطاق البصري) | مربع، قبالة في البداية |
|  8  | أبعد من التاج من الرب | خطوط | إذا كانت المحطة أكثر من 1000 أو 700 أو 500 كيلومتر (على إختيارك) من القبعة، لا يوجد خط: فقط مربعات الليلا التي تغطيها المحطتان. مربعك لا يلون أبداً | المربع والسعة على بعد 1000 كيلومتر |
|  9  | نافذة الزمن | كل شيء | البقع القديمة من النطاق المختار (من 3 إلى 60 دقيقة) | مينو في القمة |
|  10  | مزدوج | المجموعة | نفس البقعة (سام دي إكس، نفس الموقّر، نفس الفرقة، نفس المصدر) | لا |

Note on criteria 4 and 6. A connection in Q65 or JT65 over 1000 km without Common Moon is possible in tropo: is not discarded by criterion 4, but passes for the quarantine of criterion 6 as any other long link. والمكالمات المباشرة الموجهة إليكم (القسم 9 مكررا ثالثا) هي الاستثناء الوحيد من المعيارين 7 و 8: فهي ترسم دائما.

ما هو ليس مضيعة. تصويبات الموقع (C ALL3). TXT بأصوات يدوية، تصحيحات من المجموعة والمقلّدين المتعلمين، تَغيّرُ - SDR، مركز LL/MM المربع حيث تَسْحبُ محطةَ، لا تَسْردُها. ومن ناحية أخرى، لا يمكن استدراج محطة بدون أي مالك، ولا تزال في القائمة &quot;... &quot;.

## 11ربع الألوان والرموز

ولكل لون معنى واحد فقط، متساو في جميع المواضيع (تغيير فقط الخلفية).

| اللون والتوقيع | معنى |
| الخط الأزرق المستمر | Spot from DX cluster, RBN or DM4X between two stations |
| الخط الأصفر مع الحافة المظلمة | Spot with airscatter comment (reflection on aircraft) |
| الحلقة الوردية | محطة للاستماع إلى شركة PSKReporter (MQTT) |
| خطوط وردية من الدائرة مع كرة وردية في الأسفل | المحطات التي سمعها المستمع، مع كتابة الاسم دائما؛ خطوط أكثر سمكا للمستمع المختار |
| الخط الأحمر والاسم الأحمر | زيادتك لـ (يو إس جي تي إكس)، (جي تي إكس) من أنت؟ |
| الخط الأحمر | QSO in progress with that station: لقد اتصل بكِ، ولم يصل الإقفال بعد، أو أنّ (دي إكس) من (واس جي تي إكس) الذهاب مع RR73 ، RRR أو 73 |
| الخط الأخضر | QSO desunti dal live: closing RR73, RRR or 73 UDP view |
| خط أسود مع الحدود البيضاء | شخص ما يَدْعوك: هو يَدْومُ إلى RR73/RRR /73 أَو حتى 3 دقائقِ |
| مساحة 45 درجة | حركة المرور بمحطة واحدة على الأقل خارج التاج المختار: لا خطوط، فقط مربعات |
| البحث عن ليلا مع مسافة إلى الشمال | 1000, 700 and 500 km around your QTH; that choice is more marked |
| دائرة ونقطة في لون الموضوع | الربو الخاص بك، مع الاسم والمالك |
| الكرة البرتقالية مع GRAVES 143,050 | الرادار GRAVES ( JN27SI) |
| العينات من الأزرق إلى الأحمر | فهرس تروب الهلبورن (0-10) أو فئة F5LEN أو متوسطها؛ أو لون أدفأ، أو ظروف أفضل |
| غطاء رمادي خفيف | المنطقة التي لا توجد فيها بيانات عن المجموعة للوقت المختار |
| Grey-blue lines with the value in hPa | Isoberate every 4 hPa; H in blue high pressure, L in red low pressure |
| الخطوط الحمراء والزرقاء والأرجوانية | Isothermia to 850 hPa every 4 °C: red above zero, blue under zero, purple the zero |
| برتقال العين | الانحدار الحراري: الهواء إلى 925 هبة أدف من الأرض، أكثر كثافة مع تحويل أقوى |

في التقارير المُشتبه بها، حافة كلّ تاب تقول القاعدة المُنفّذة: "بخير خضراء، سكارتا حمراء، بلا قاعدة"

## 12. التركيبات والقابلية للنقل

(ويندو) في البداية، تُشغل النافذة 90 في المائة من الشاشة المُفيدة (إغلاق حانة الطلب)، مُركّزةً، لذا على مُلاحظة 15-16 بوصة مع اثنا عشر في المئة أو 150% من مُعاداة اللقب لا تزال مرئية. إذا كان الحيز المفيد × أقل من 1280 نقطة 720 نقطة، تعظيم جزء النافذة. ويُحفظ الحجم والوضع والوضع الأقصى في التردد العالي جدا. Ini at the closing and end at the next start, provided the title bar still falls into a screen (for example after detaching an external monitor); otherwise you return to 90%. The minimum size is 900 × 560.

The program is portable: never writes in the AppData folder or in the registry. كل شيء في ملف التنفيذ LIVETROPOVHF.ini (Settings, created at the first closing), C ALL3.TXT (with handwritten voices, learned corrections and locators), map cache and weather data, rules learned (Dati/APPRENDIMENTO.TXT) and LIVETROPOVHF.db archive.db. يمكنكِ أيضاً أن تحافظي على عصا (يو إس بي) طالما أن الملف قابل للكتابة

محفوظات ويتم توفير كل بقعة مقبولة وكل تقرير للاستماع إلى المراسلين في LIVETROPOVHF.db, a SQLite database in one file. ولا تزال في الذاكرة سوى الـ 60 دقيقة الأخيرة بالنسبة للقائمة والخريطة؛ وتحتفظ المحفوظات بكل شيء وتخدم التاريخ والإحصاء (أيضاً الزينة الموحدة)، مثل فتحات النطاقات ويوماً، والحد الأقصى للمسافات، والمسارات التي تم التوصل إليها، والمقارنة الخلفية بين الرقم القياسي المتوقع والبقع الحقيقية. الشرائط التي تم التخلص منها من المرشّح تُحسب لكن لا تُسجل [14]

## 12 مكررا التركيب الآلي والتحديثات

من النسخة 1.0، يُحمّل البرنامج بالفعل جاهزاً من صفحة الإصدار من github.com/ic8tem/LIVETROPOVHF، في نفس الطرود من طراز YA-MS: installer for Windows (64 bit and ARM, install in C:\LIVETROPOV without administrator rights) and portable zipine With Linux packages, the program is copied to the first start in -/LIVETROPOVHF, so settings and data remain next to the program; on the Mac data is born next to LIVETROPOVHF.app. SP1

كل 30 دقيقة (الفحص الأول بعد بضع ثوان من البداية، فوراً إذا كانت آخر مرة أغلق فيها البرنامج لخطأ) إذا كانت النسخة جديدة تُحمّل الرزمة النقالة المناسبة للنظام وتستبدل ملفات البرنامج في الموقع: يعاد تسمية المستعملين *. Settings, archive, Data folder and C ALL3. TXT لم يلمس. والصيغة الجديدة ناشطة من البداية التالية: عند الانتهاء من التركيب، يقترح أن يعاد العمل فورا؛ أما بالنسبة للتصويبات العاجلة، التي تحمل علامة [التزام] في الإصدار، فلا يمكن تأجيل إعادة التشغيل. وتُكتب حالة المراقبة والنسخة المستخدمة في أسفل حانة الحالة؛ ولإغلاق المستجدات=0 في LIVETROPOVHF.ini. On the Mac the app is signed and does not change itself: the program only reports that there is a new version.

وتظهر في البداية صورة ليفتروف ذات تردد عال ورقم النسخة؛ وفي نهاية المطاف يطلب البرنامج تأكيده وشكره على استخدامه.

## 12 مكررا ثانيا سجل الرعب وحانة الحالة

وينتهي أي خطأ غير متوقع في البيانات/النسخة، حتى مع سجل الحساب: التاريخ والزمان، والنسخة، والنظام، والتبويب المفتوح، والخطأ الكامل، والخطوط الأخيرة لما يقوم به البرنامج. لم يعد هناك خطأ في الواجهة يغلق البرنامج: في حانة الحالة يظهر "خطأ مشروح" وفي البداية القادمة، يطلب البرنامج أن يرسل التقرير: يفتح المصفح على اشارة جيت هوب التي تم تجميعها بالفعل، مع تعيينات وموقع واسم مستخدمي PC أزيلت، التي تفحصها وترسلها مع حساب غيت هوب المجاني. وينتهي السجل أيضاً بكل فحص مستكمل.

In the status bar the source Croatia (DX cluster, RBN, PSKReporter, MQTT, UDP) are readed and brought to the source; the comic says why a source is off: rejected port, cluster node stationed on the server side, no response, or UDP listen but without packages from WSJT-X, JTDX or MSHV. نقرة واحدة على الرقم على الفور تحقق من المستجدات.

## 13. المنصات

External bookcases: Avalonia for the [12] interface, MQTTnet as a MQTT client for the real-time stream of PSKReporter [13], Microsoft. Data.Sqlite for the [14] archive.

C# on.NET 10 (LTS) [15] with Avalonia interface, published as an independent executable for Windows 64 and 32 bits, Mac Intel and Apple Silicon, Linux Debian and Raspberry Pi (64 and 32 bits). Limits Noted: Windows 10/11; macOS 14 or later, with apps to sign or to open right). open; Raspberry from Pi 2 / Zero 2 W up (ARMv6 models are not supported by.NET).

## شكراً لك
شكراً لـ (إكس تيمو زانوني) للاختبار على (ويندوز 7) و (ويندوز 8) I5MZY

Airtights: ITU-R P.453, digital products of Study Group 3 (ITU), used for personal and non-commercial purposes.

## المصادر الرئيسية

1. S. Heck LA0BY, “Extreme Tropo Propagation on 144 MHz and up”, presentation at the 41st Nordic VUSHF Meeting, Skjeberg 2019. Troposferical Ways, Event Control List (Hepburn/ F5LEN forecast, high temperature profiles with Windy, DX cluster as opening test), observation that only forecast maps are not enough for extreme routes.
2. J. N. Gannaway G3YGF, “Tropospheric Scatter Propagation”, QST November 1983 (from the eponymous article on Radio Communication, RSGB, August 1981). Formules of free space loss and diffusion, equivalent radius 8497 km, site term of 10 dB per horizon, climate term 0.2 · (N − 310), plant capacity and signal/noise ratio, example at 144 MHz.
3. W. R. Hepburn, DX Info Centre, “Tropospheric Ducting Forecast” (dxinfocentre.com/tropo.html) and “The Hepburn Tropo Index (HTI)”. مقياس الفهرس ومعنى القيمة، التنبؤ في 6 أيام.
4. F5LEN, forecast for Europe (tropo.f5len.org/forecasts-for-European: list of images in the page script, class Bishop Disturb...)أ( انظر: &quot; نسخة جديدة من أداة التنبؤ بالنشر في الغلاف الجوي العالمي ذات التردد العالي جدا &quot;. الرقم القياسي المستند إلى التفاعل في الوحدة N، 40 خطوة من 3 ساعات، تحديث 4 مرات في اليوم.
5. Mqt.pskreporter.info, distribution MQTT in real time of the commercials of pskreporter.info (curated by M0LTE, flow of P. Galstone used with permission). الأبواب ١٨٨٣-١٨٨٦، خطة المواضيع الخاصة بالسارق/المرشاة/الترميز/٥٢.
6. Project summary of 8/11/2026 and Go test (core.go, gui.go) provided by the author of the program. Ad Rules, negative filter, bandwidth segments, telnet cluster management, themes, languages and portability.
7. C ALL3.TXT, nomination database). JTDX landlord (I. UA3DJY, A. ES1JA script, JTAlert/hamspots.net data). File format and count of the names verified on the project file.
8. WSJT-X, source code, NetworkMessage.hpp file (description of UDP protocol: Heartbeat, Status, Decode). General reference, not annexed; JTDX and MSHV use the same format.
9. Reverse Beacon Network (reversebeacon.net), service pages of the telnet node. Ports 7000 (CW, RTTY) and 7001 ( FT8), login with the name only.
10. D. Mills, RFC 4330 “Simple Network Time Protocol (SNTP) Version 4”, IETF 2006. استمارة التعبئة وحساب النفايات وتأخر العودة General reference, not annexed.
11. NTP Pool Project (ntppool.org); INRIM, NTP time service (ntp1/ntp2.inrim.it); PTB (ptbtime1.ptb.de). الخادم اعتاد على التحكم بالزمن General reference, not annexed.
12. Avalonia UI, official documentation (docs.avaloniaui.net). واجهة متعددة ومواضيع ومخططات من اليمين إلى اليسار General reference, not annexed.
13. MQTTnet,.NET library for MQTT (github.com/dotnet/MQTTnet). العميل استعمل لتدفق المراسل General reference, not annexed.
14. SQLite (sqlite.org) and Microsoft. Data.Sqlite (learn.microsoft.com). المحفوظات في ملف واحد General reference, not annexed.
15. Microsoft,.NET 10 -- supported operating systems and autonomous publication in one file (learn.microsoft.com/dotnet). المنصات والحدود المبينة في الباب 13. General reference, not annexed.
16. ECMA-376 “Office Open XML File Formats”, part 1, section 22.1 (Office Math Markup Language). هيكل الملف والمعادلات General reference, not annexed.
17. List of the 28 languages of the YA-MS wiki, provided by the author of the program.
18. Open-Meteo, weather predict API (open-meteo.com), with data from the NOAAA GFS model. الضغط على مستوى البحر من أجل إيزوبير (انظر الوثيقة 2).
19. J. P. Snyder, “Map Projections – A Working Manual”, USGS Professional Paper 1395, 1987, and standard polynomial transformation methods for georeferencing images. General reference, not annexed.
20. (آلماناك) الفلكي، الفرع دال، &quot; صيغ الدقة الحالية للقمر &quot;)موقع دقيق يناهز ٠,٣ درجة(. اعتدت على التعرف على وصلات EME. General reference, not annexed.
