التردد العالي جدا الوثيقة التقنية 2
# تشييد الخريطة

> ترجمة آلية من الإيطالية: عند الشك يُعتمد الدليل الإيطالي.

وعندما تولد خريطة خريطة الخانة والبقع: فالقاعدة الجغرافية، والإسقاطات، ومتوسط مؤشرات هيبورن و F5LEN ، والآيسوباري، والبقع، ومحطات الاستماع، تتداخل مع طبقات. Version of 9 November 2026.

## موجز

- الخريطة مصممة من قبل البرنامج في ثماني طبقات، كل منها مع صندوق [8].
- القاعدة: خط ساحل الأرض الطبيعي والحدود على مستويين من تفاصيل [9] ؛ والسوق [10] والإسقاطات الزامية.
- الفيزياء: التحويل، التفاعل N and gradient dN/dh, lead threshold at −157 N/km [3] [4] [7] [11].
- الرقم القياسي للثروت: القراءة المعايرة لخرائط هيبرن [1] و F5LEN [2] ، والشبكة المشتركة، ومتوسط المصدرين.
- Isobare: GFS [12] pressure via Open-Meteo [15], lines every 4 hPa, H and L centers.
- Spot, listen stations (dark unique cycles with always visibleزهر lines) and personal decodings in red.
- قراءة التداخل: ارتفاع الضغط، وارتفاع النوافذ، وهبوط الضغط، وتزايد SNR، و [3] [5] [13] من الطرق الجبلية. [4] [14]

## 1. المبدأ: خريطة مطبقة

الخريطة ليست صورة ثابتة: مصممة من قبل البرنامج، طبقة على الطبقة، عندما تغير الزوم، الإسقاط، الفرقة، وقت التنبؤ أو الموضوع. كل طبقة لديها صندوقها في أعلى حانة لاير على الخريطة، حتى تتمكن فقط من عقد ما تحتاج ورؤية كل شيء معا عندما تريد الصورة الكاملة. والأمر، من الأسفل، هو ما يلي:

|  #  | الطريق | المضمون | Casella |
|  1  | خريطة أساسية | البحر والأراضي والسواحل والحدود | نعم |
|  2  | الرقم القياسي | Media Hepburn + F5LEN, or only one source; adjustedable transparency | نعم |
|  3  | موقع غريل | حقول، مربعات، مع زوم، مربع | نعم |
|  4  | Isobare | خطوط الضغط كل 4 hPa، المراكز H و L | نعم |
|  4a  | درجة الحرارة 850 ساعة | المعالجة الحرارية لكل 4 درجة مئوية | نعم |
|  4b  | التحويل الحراري | العينة البرتقالية حيث الهواء على ارتفاع أدفأ من على الأرض | نعم |
|  5  | PSK Reporter | Receiving stations of PSKReporter (dark unique cycles) and, in unique, what they hear | نعم |
|  6  | قَدّمَني | محطات التخدير مع WSJT-X، JTDX أو MSHV: خطوط حمراء من QTH | نعم |
|  6a  | QSO desunti dal live | وصلات مغلقة ( RR73, 73, RRR) مستنسخة في رموزك: خط خضراء بين المحطتين | نعم |
|  7  | Spotlight | Connections: full blue line, zero covered for airscatter, two Croatia | نعم |
|  7a  | الاستثناءات | Casella Excluded under 100 km: the shortest traffic is neither drawn nor listed. Casella أكثر من 000 1 كيلومتر: مربعات فقط (مسارعة): إذا كانت إحدى المحطات على بعد أكثر من 1000 كيلومتر من QTH الخاص بك، لا خطوط أو دوائر، فقط المربعات المركّبة من المحطات الضالعة في الليلاك تعامل عند 45 درجة | نعم |
|  8  | QTH and labels | مركزك، أسماء الموقع المختار | دائماً مرئية |

فهرس التروبو أعلى مباشرة من القاعدة وأقل من أي شيء آخر: هو عينة ملونة، والوضع الأعلى سيغطي الخطوط والرموز. الشبكة تتخطى ذلك، لذا أنت دائما تعرف أين تقع منطقة ملونة. الـ(إيزوبيرز) هي خطوط رقيقة وتقرأ جيداً على العينات

## 2. الخريطة الأساسية

وتأتي التكاليف والحدود والأراضي من بيانات ناقلات الأرض الطبيعية، والمناطق العامة [9] ، إلى الجدول 1:50 مليون للرأي العام، و 1:10 ملايين عندما تكون متضخمة. الملفات في ملف البرنامج: لا توجد خدمات خريطة على الإنترنت. Colors of the topic Clear: light blue sea # E2ECF8, dark blue coast, light grey borders. في موضوعي (بلاك) و (ليل) تظلم ألوان القاعدة في المواضيع الأخرى التي تصبغها قليلاً

## 3. الإسقاطان

جميع الطبقات تمر من خلال نفس المهمة الإسقاطية، لذلك فهي دائما متوافقة مع بعضها البعض. تختار بين إسقاطين اكس Q (الخط الطول، خط العرض، ري الأرض): [10]

السوق تخزين زوايا وأشكال محلية؛ المسافات تمتد نحو القطبين اقطع الى خط العرض

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

بعوضة (أزيموتيل) ركّزت على مربعك المركز (الموسم، (إيكو والاختلافات والتوجهات المقيسة من المركز صحيحة: فالمحطة التي تبلغ مساحتها 1200 كيلومتر تبلغ مساحتها 1200 كيلومتر، والزاوية مع الشمال هي الزمية التي توجه إليها الهوائي. عندما تُغيّرُ الموقعَ تُعيدُ تركيز الخريطةَ.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

In the azimutale the lines that on the paper are straight (meridians, parallels, square edges) become curved. ولهذا السبب، تقسم جميع الخطوط إلى صفات قصيرة، ويُتوقع أن يُقسم كل قسم، ثم تُوحَّد الصفات؛ وينطبق الشيء نفسه على حواف مؤشر التروبو وعلى الإعصار. النقطة إلى مضادات المركز ليس لها موقع محدد ولا تزال بعيدة المنال.

## 4. مقطورة خضراء، زوم، تحرك

The Maidenhead grid is designed by the program: fields of 20° × 10° with the two large and faded letters in the center, squares of 2° × 1st, and with sufficient zoom subquadrati of 5 &apos; × 2,5 &apos;. الخطوط زرقاء و رقيقة لا تغطي الرقم القياسي

وتركيز الازدواج مع العجلة على المؤشر (النقطة التي تحت الفأر لا تزال ثابتة)، والانتقال إلى السحب، والنقرة المزدوجة للعودة إلى كل المنظر، + و - المفاتيح، والمفتاح بنسبة المنظر بأكمله. التفاصيل تتناسب مع الزوم: السواحل الراقية، المناطق الفرعية، البطاقات الأكثر سماكة.

## 5. لِمَ تنحني الحركات

وفي الغلاف الجوي المنخفض، تنخفض درجة الحرارة عادة بحوالي 6.5 درجة مئوية لكل كيلومتر؛ ويتوقف الرقم القياسي للتكرار على درجة الحرارة ورطوبة [3] على حد سواء، ويتغير بشكل حاد مع ارتفاع نحل الموجات إلى الأرض ويصل إلى أبعد من المعتاد. The most important layer is that up to about 3 km, where inversions, ducts and convective cells [4] are formed; sea paths are favored because the water masses form large areas of [4] inversion. [3]

The size used to measure it is refractive N, i.e. (n - 1) · 106 [11]:

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

(تي) هي درجة الحرارة في (كيلفين) وضغط الضغط وضغط البخار كلاهما في (إتش بي)

(د) عدّة نون متفاوتة مع حصة (DN/dh). In the standard atmosphere is approximately −39 N/km; up to −79 N/kmm the refraction is considered normal, besides super-refraction, and with positive gradients of sub-refraction [7]. وتهبط هذه الموجة إلى ما دون 157 ن/كم فوق منعطف الأرض وتظل محاصرة: فهي قناة [11] (اختطاف). هذه هي الظروف التي تحاول خريطة هيبورن و F5LEN التنبؤ بها.

## 6. الرقم القياسي للترسب: من خريطة إلى المتوسط

ويُعد مكتب المراقبة من مسلسل &quot; هيبورن &quot; (Hpburn) و &quot; F5LEN tabs &quot; (Hpburn) و &quot; &quot; (Hpburn) و &quot; &quot; ، حيث يُظهران الصور التي تم تنزيلها من صفحة كل مصدر، مع تغير وقت التنبؤ، ويعملان على معايرة القراءة بالنقرة على النقاط المرجعية والألوون الأسطورة. ولا تزال طبقة فهرس التروبو فارغة حتى لا يكون أحد معايرتي التروبو على الأقل نشطا وكاملا.

هيبورن ويقيس مؤشر هيبورن كمية الأمواج التي يتوقع أن تطوي على منطقة، على نطاق خطي يتراوح بين صفر و 10 موجات يمكن أن تتجاوز أيضا 10. لا يعني أي غلاف جوي عادي ولا ينشر إلا؛ و1-2، و1-2، و1-2، و1-2، و1-2، و1-4، و1-4، الخرق فوق مناطق النقاش؛ والرقم القياسي لا يأخذ في الحسبان ترابوت الارتطام ولا يرى جبال [1]. وتغطي الخرائط ستة أيام: مرور ثلاث ساعات خلال ال ٣٦ ساعة الأولى، ثم ست ساعات، يجري تحديثها كل يوم إلى ١٨ مركبة من طراز UTC [1] ؛ وتأتي بيانات البداية)الطبيعة، الرطوبة، الضغط( من النموذج العالمي للخدمة الجوية الكندية [1]. [1]

F5LEN. وفيما يتعلق بأوروبا، تنشر F5LEN 31 خريطة من +3 إلى +192 ساعة (ثلاث ساعات إلى +48، كل 6 ساعات إلى +96، ثم كل 12) مع التاريخ والزمان اللذين ينطبقان على العنوان، وزمن الإنتاج في اليمين السفلي، وأسطورة درجة: الاضطرابات، والطبيعية، والصغر، والفق، والبقر، والجيد، والقوية، والفي سترونغ، والدكت [2]. The world prediction of F5LEN is calculated by the air refractive index in Unit N, on 40 steps of 3 hours (about 5 days), updated 4 times a day; a read on the map shows the average value and the locator of that point [2].

تقدم لكل خطوة من خطوات التنبأ يقوم البرنامج بهذه الخطوات

1. التخلص The images of the two forecasts are downloaded in the cache next to the executable, once to update the source, not to any redesign. وقبل النص النهائي، ينبغي طلب الإذن إلى شركة هيبورن وشركة F5LEN.
2. أوتوماتيكيا The program finds the frame (two horizontal and two spiritual lines of the same colour) and, on Windows 10/11, reads with the OCR integrated into the system the axle labels (54N, 10W, 0, 5E...). اقطعوا الشرائط حول الإطار، ووسعوها، واختبروا كلتا العلامتين عندما لا تقرأ الرسالة N/S/E/W وتبقي الخط الذي تسقط عليه أكثر العلامات: فالقراءات الخاطئة لا تزال خارج. من هنا يمكنك الحصول على الرباط/اللون المتطرف من الإطار ونوع الإسقاط المتطرفون ينقذون أنفسهم ويعيدون قراءة أنفسهم إذا تغير الإطار حجمه On Mac and Linux, without OCR, factory extremities or handwritten ones are valid in the tab. وكبديل عن ذلك، بالنسبة للخرائط التي لا توجد لها محاور قانونية: ولكل نوع من الخرائط (Hepburn Europa, F5LEN worldwide...) معايرة: بعض نقاط المراقبة ذات الإحداثيات المعروفة (عبر الوسطاء والموازاة، والأضلاع المعترف بها) تربط بين البكازات في خط العرض والطول، في الإسقاط الذي يستخدمه ذلك المصدر. المعايرة في ملف بجانب البرنامج ويمكنك إعادة تشكيله إذا تغير شكل المصدر
3. قراءة الألوان ويقارن كل بيكسل بأسطورة المصدر ويتحول إلى أقرب قيمة رقمية. وتُعترف بالتكاليف والكتابات والشبكات المستقاة من الخريطة الأصلية بأنها &quot; لا بيانات &quot; وتُملأ بالقيم المجاورة.
4. الشبكة العالمية فكل صورة، بمجرد العثور على الإطار ومعرفة متطرفاته، توضع على الأرض؛ وتستمد القيمة في أي مرحلة من العالم من الصور التي تغطيه. وإذا غطّت مناطق أخرى نفس النقطة، فإن الأمر يستحق التفصيل (مزيد من البكسل في الدرجة)، شريطة أن يكون وقتها في غضون 1.5 ساعة من ذلك الاختيار؛ ثم أقرب ساعة، ثم آخر سباق. حيث لا توجد صورة تغطي النقطة التي تظهر فيها الخريطة حجابا رماديا: لا توجد بيانات تختلف عن &quot; المؤشر صفر &quot;. وفي داخل الإطار، يقفز بيكسل بلا لون أسطورة إلى صفر (جو عادي)؛ وتقفز السواحل والحدود والكتابات بالنظر إلى أقرب لون في 3 بيكسلات.
5. نفس السلم Hepburn is already on the scale 0-10 [1]: dark background = 0 and ten colors in order (violet, blue, water green, lime,صفر, ocher, Portuguese, red,زهري,ضوء وردي) for 1..10, lilac beyond. F5LEN uses classes (Disturb, Normal, Minor, Weak, Good, powerful, Vy Str, Duct) [2], but some have exactly Hepburn colors: Weak = 1, powerful = 5, Vy Str = 7, Duct = 8; Minor is 0.5 and Good (green) 3.5. ويصحح مضاعف المقياس، الوارد في الوثيقة F5LEN ، المجموعة التي تجعله قابلا للمقارنة مع Hepburn.
6. في نفس الوقت The timebar goes from +0 to +192 h at steps of 3 hours. وبالنسبة لكل خطوة تتخذونها من كل مصدر، تكون التوقعات سارية في أقرب وقت؛ وإذا لم يكن لدى المصدر تلك الخطوة، فإن وسائل الإعلام تستخدم الأخرى فقط، وتشير الأسطورة إليها.
7. وسائل الإعلام ومع زر وسائط الإعلام، فإن كل نقطة تستحق (Hepburn + F5LEN) / 2؛ ومع شركة Hepburn أو F5LEN ، فإنكم لا ترى سوى مصدر واحد، مفيد لفهم المكان الذي يختلف فيه التوقعان.
8. رسم. والقيمة ملونة بمقياس الأسطورة (0 أزرق، 2 أخضر، 4 أصفر، 6 برتقالي، 8 أحمر، 10 أرجواني) ويتوقع أن تكون عينة تحت الشبكة. الـ صفر شفاف، لذا حيث لا يحدث شيء ترى الخريطة الأساسية النظيفة؛ الشفافية العامة تتأقلم مع الرافعة بجوار الصندوق.

وفي الموقع، فإن الرقم القياسي على طول الطريق هو متوسط بطولات قيم الشبكة كل 10 كيلومترات على أقصى دائرة بين المحطتين.

## 7. Isobare

والضغوط المرتفعة المستقرة هي أول إشارة تُلتمس لسجادة [3] ، كما أن الصلات التي تتعدى أجهزة حلل الاكس 1Q قد تعرضت لضغوط عالية من [4]. IK3XTV

1. بيانات الضغط على مستوى سطح البحر من النموذج العالمي GFS للأشعة فوق البنفسجية (NOA [12])، يُقرأ عن طريق الخدمة الحرة المفتوحة -Meteo [15] على شبكة 3 درجات × 3 درجات (من 30 درجة إلى 72 درجة) N, from 30° W to 48° E), for the next 78 hours. البرنامج يستكمل البيانات في البداية ثم كل 6 ساعات مثل سباق النماذج
2. خطوط ويتم الحصول على الإيسبوار من الشبكة كل 4 hPa (... 1012, 1016, 1020...) مع خوارزمية المربعات المسيرة (المربعات) ثم يتم تقريبها بشكل طفيف وسحبها عن طريق الطيف.
3. مراكز. The most marked local maximums and minimums are marked with H (blue) and L (red) and the value in hPa.
4. لابلز وتظهر القيمة على طول كل هوبرة، على خلفية لون من الخريطة لتبقى قابلة للقراءة فوق العينة.

المنطقة والخطوة. وتُحمَّل البيانات على المنطقة التي تنظرون إليها، وتُوسَّع بنسبة 25 في المائة، وخطوة من درجة حرارة واحدة إلى 20 درجة اختيرت لبقائها تحت 400 نقطة (يحسب أوبن - ميتيو كل نقطة كطلب: 600 في الدقيقة، 5000 في الساعة): عن طريق التوسع في الخريطة أو نقلها من المنطقة التي حملت بالفعل البرنامج أكثرها كثافة، في معظم الـ 10 دقائق؛ وإذا أخفق تحديث البيانات السابقة مرة أخرى. هذا الطريقِ إسبوري ودرجات الحرارةِ تَعْملُ في أي مكان في العالمِ.

درجات الحرارة The same request brings the temperature to 2 m, to 925 hPa and to 850 hPa. وتبيّن LA0BY أن حالة 22/11/2018 إلى UA6 قد شوهدت في خرائط درجات الحرارة (900-3000 متر)، ولكنها لم تُشاهد في التنبؤات المتعلقة بتقديرات Hepburn [3] ؛ و DF5AI و DK5YA تستخدمان مواصفات درجات الحرارة العمودية للتعرف على تحويلات [5]. لهذا هناك طبقتان:

- Temperature 850 hPa: isoterme treat every 4 °C (red above zero, blue below, purple on zero).
- التخريب الحراري: أخذ عينات برتقالية حيث الهواء في 925 أو 850 هبغا أدف من ذلك في 2 متر؛ وكلما زاد الارتداد، كلما كان التحول أقوى. وهي الحالة المادية التي تنحني الأمواج إلى الأرض [3] [11] ، وتقارنها بمؤشر التروبو، وتظهر إيسوباري إذا كانت التوقعات لها قاعدة الأرصاد الجوية.

## 8. Spots and stations

Spot. Each spot with both known positions is a line on the maximum cycle between the two stations, divided into sections and projected; In Mercatore it appears then slightly curved, in the azimutale it is straight if it starts from the center. خط أزرق ممتلئ بالبقعة العادية، الأصفر المشحون بالحافة المظلمة للهواءات (AS., TRAACS. or “airscatter” in the comment), a ball for each station, light halo under the line to detach it from the sampling. وكثيراً ما يُصمم الموقع المختار للأخير وله اسمان.

الاستماع. كل مستورد من طراز PSK محطة إستقبال (إكس تي) هي دائرة وردية سوداء فارغة من بداية كل خط وردي كامل نحو المحطات التي تآكلت وأعادت إلى الشبكة، مرئية دائماً دون نقر، مع كتابة الأسماء دائماً. باختيار دائرة تكون خطوطها أكثر سمكاً Spot and listen only show the band and range chosen in the list: changing bandwidth the map is empty and redesigned with the data already collected for the new band. [16]

قررت من قبلي كل محطة تتفكك مع WSJT-X, JTDX or MSHV (receipt via UDP) هي خط أحمر كامل من QTH إلى المحطة، مع الكرة والترشيح. إنها أكثر مقارنة مباشرة بالتوقعات تظهر أين يأتي متلقيك الآن

## 8 مكررا. الألوان والرموز

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

ما هي مربعات الليلاك التي تهبط عند 45 درجة. الإزدحام بعيداً عن قوثكِ يُثير اهتمامكِ لفهم مكان الإنتشار، لكن خطوطه، مئات الكيلومترات الطويلة والعشرات غالباً، ولهذا السبب، فبعد التاج المختار، لا يرسم البرنامج الخطوط بل يلوّن المربعات المركّبة (2° × 1°) من المحطات المعنية. والنتيجة هي خريطة &quot; المناطق النشطة &quot;: حيث يظهر العديد من مساحات الليلاك القريبة، توجد قناة أو فتحة، حتى وإن لم تكن تهمك مباشرة؛ وإذا اتسعت المنطقة نحوكم، فإن الإشارة تشير إلى الهوائي في هذا الاتجاه. لوح الـ 45 درجة يسمح لك برؤية تحت الرقم القياسي للمجموعات والشبكة، لذا مقارنة بالتوقعات (اللون القاع) والحركة الحقيقية (اللوتس). ملعبك لا يلون أبداً لأنه نقطة البداية وليس منطقة بعيدة فالمساحة فقط التي تتجاوز التاج ملونة: فالمربع الذي يعبره التاج، أو داخله، لا يزال حرا حتى لو كان هناك محطتان. لمشاهدة الخطوط، فقط اغلق الصندوق أو اختيار تاج أكبر.

وتوضح المعايير التي تقرر ما يأتي على الخريطة وما يتم التخلص منه، معيارا واحدا في الفرع 11 مكررا ثانيا من الوثيقة 1.

## 9. الحد الزمني

Below on the map: date and time of the prediction shown, cursor from +0 to +72 h at 6 hours, index Bishop 0-10 with the name of the chosen source, and time of the model race for isobare. فهرس الترامبي ومؤشر هروباري يتبعان الضمائر؛ والبقع والمحطات التي تصغين وتزيينك هي ملاحظات لللحظة وتظل دائما تلك &quot; الإضافة &quot;. عندما لا يكون الشرايين على +0 هم مخففون، للتذكر أنهم لا ينتمون إلى الوقت المتوقع.

## 10. كيفية قراءة التداخل

- ارتفاع الضغط + ارتفاع المؤشر + البقعة على طول المنطقة الملوّنة: الافتتاح هناك وهو المخطط له. حان الوقت للاتصال في هذا الاتجاه
- مؤشر عالي ولكن لا يوجد مكان: أو لا أحد نشط هناك، أو أن التوقعات متفائلة جدا. وتساعد طبقة الاستماع على التمييز: إذا كانت هناك أجهزة استقبال في المنطقة ولا تشعر بشيء، فإن الفرضية الثانية أكثر احتمالا.
- بؤر طويلة خارج المناطق الملونة: نثر عالي، لا ترى التنبؤات السطحية، كما هو الحال في حدث L- UA6 [3] ؛ أو نفد آخر من المرشّح (مثلاً E متقطع دون علامة). DF5AI and DK5YA distinguish tropo and E sporadic also from the hour: openings that are born and end up together with a diffuse E sporadic are hardly [5] tropo.
- وتختلف شركة هيبورن وشركة F5LEN على ما يلي: مقارنة المصدرين بالأزرار ومعرفة المكان الذي تسقط فيه البقع الحقيقية.
- ضغط منخفض: أعقب G4MCU فتحة على منارة SK6VHF إلى أن انخفض الضغط؛ ونظرت 1012 في الحدث النهائي [13]. راقب الـ(إيزوبر) من خلال سحق الحاجز الزمني: إذا انسحب الضغط العالي، الافتتاح ربما يتبعه.
- الإشارات التي ترتفع: في دراسة استعراض الأداء الشامل الذي أجري في 144 ميغاهيرتز من KP4MD ، ارتفعت نسبة الإشارة/الإعلام مباشرة في النوافذ التي توقع فيها هيبورن حدوث تحسن، وانخفضت عندما أخذت التوقعات [14]. إذا كان على نفس المسار ينمو، الافتتاح هو تشكيل.
- الممرات الجبلية: لا يأخذ الرقم القياسي في الاعتبار إغاثات [1] ، ولكن توجد أيضا وصلات خارج جبال الألب بفضل خلايا وموجات من الجاذبية في الغلاف الجوي [4] ، و LA0BY عملت 9A و S5 من خلال الجبال دون أن تنبأ هيبورن بها [3].

## 11. نقطة مفتوحة

- تفويض (هيببورن) و(إكس تي) لاستخدام خرائطهم في البرنامج F5LEN
- معايرة معامل التحويل F5LEN
- معايرة نقاط المراقبة من أي نوع من الخرائط، لكي يعاد ترتيبها إذا غيرت المصادر الرسوم البيانية.
- الطريق الاختياري مع درجة الحرارة عند الارتفاع (850 ساعة) من النظام العالمي لمراقبة المناخ.

## المصادر الرئيسية

1. W. R. Hepburn, DX Info Centre, “The Hepburn Tropo Index (HTI)” and “Tropospheric Ducting Forecast” pages (dxinfocentre.com).:: تعريف وحجم المؤشرات، والقيم السلبية، وأكثر من 10، واستبعاد المفرقعات والإغاثة، وخطوات مدتها 3 و 6 ساعات في 6 أيام، ورفعت إلى 18 مركزا تجاريا. Origin of data from the global Canadian model: G. Meisse, AMSAT-BB, 27/08/2007.
2. F5LEN, forecast for Europe (tropo.f5len.org/forecasts-for-European) and world prediction of the VHF tropopheric propagation (tropo.f5len.org/WW) and noted “New version of the VHF tropospheric propagation forecast tool”. الرقم القياسي للوحدة N، 40 خطوة من 3 ساعات، 4 تحديثات في اليوم، القيمة والموقع لكل نقرة.
3. S. Heck LA0BY, “Extreme Tropo Propagation on 144 MHz and up”, 41° Nordic VUSHF Meeting, Skjeberg 2019. درجة حرارة طبيعية ٦,٥ درجة مئوية/كغم وتحولات تصل إلى ١٠-١٥ درجة مئوية/كغم، ودرجة الحرارة ودور الرطوبة، وضغوط مستقرة عالية، وحدث درجة حرارة عالية في درجة حرارة لوس أنجلوس - UA6 ، وليس في وصلات هيبورن، ورقم L-9A/ S5 خارج الجبال.
4. F. Egano IK3XTV, “Propagation VHF – The contribution of convective cells and atmospheric gravitational waves in the Tropo propagation.” Importance of the first 3 km, marine paths, convective cells, gravity waves on the Alps, high pressure connections, common volume up to 3-4 km.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, “Very Long Distance Propagation in the 144 MHz Band”, 24/7/2003. وتشير النبذات الفيزيائية لدرجات الحرارة والوزن إلى الاعتراف بالتحويلات، واستخدام مؤشر هيبورن، والتمييز بين التروبو و E المتفرقة حسب الساعة.
6. J. N. Gannaway G3YGF, “Tropospheric Scatter Propagation”, QST November 1983. مكافئات نصف قطر الأرض وفقدان المسار المستخدم للرقم القياسي وبطاقة البقعة (انظر الوثيقة 1).
7. The VHF/UHF DX Book, chapter 2 “Tropospheric propagation”, pp. 2-23...2-30. Figure of refractive zones according to dN/dh (standard −39 N/km, limits −79 N/km and 0) and surface and high duct profiles. The text of the supplied copy is not readable: only the figures were used.
8. Project summary of 8/11/2026, provided by the author of the program. ألوان الخرائط، الإسقاطات المطلوبة، طبقات، مقياس زمني
9. Natural Earth, vector data of coasts and borders, public domain (naturalearthdata.com). General reference, not annexed.
10. J. P. Snyder, “Map Projections – A Working Manual”, USGS Professional Paper 1395, 1987. Formules of Mercator projections and azimutale equidtanti. General reference, not annexed.
11. Recommendation ITU-R P.453, radio refractive index. صيغة تفاعلية N and duct threshold at −157 N/km. General reference, not annexed.
12. NOAAA NCEP, Global Forecast System (GFS). الضغط على مستوى البحر عند 0.25 درجة، وأربعة سباقات في اليوم. General reference, not annexed.
13. G4MCU, articles with tag “tropospheric” (g4mcu.net/tag/tropospheric), 2018-2025. Refration, conduit and scatter; more frequent ducts with high pressure, especially on the sea; beacon SK6VHF followed up to the drop in pressure at 1012 hPa (June 2019).
14. C. F. Milazzo KP4MD, “VHF/UHF WSPR Propagation Study” (qsl.net/kp4md/144_mhz_wspr.htm), 2012-2014. SNR of the WSPR reports to 144 MHz growing in the improvement windows predicted by Hepburn and falling when the forecast disappears.
15. Open-Meteo, weather predict API (open-meteo.com), free non-com. الضغط على مستوى سطح البحر لنموذج GFS على نقاط متعددة في طلب واحد.
16. Mqt.pskreporter.info, distribution MQTT in real time of the commercials of pskreporter. info: nomination and landlord of recipients (see document 1).
