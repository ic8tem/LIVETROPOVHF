# 소스 및 수식

> 이탈리아어에서 기계 번역했습니다. 의심스러우면 이탈리아어 안내서를 기준으로 하세요.

LIVETROPOVHF tropo 예측에 관한 소스의 작업 컬렉션 (20–21 11월 2026): 기본 물리학, 사용 가능한 데이터, 출판 된 인덱스, 공식, 아마추어 라디오 경험, 검증 및 결정. 수식은 LaTeX, (1), (2),..., 그리고 브래킷의 숫자로 호출하는 텍스트에 숫자입니다. Forecast Manual은 해당 프로그램을 사용하는 방법을 설명합니다.


## 1. 저자의 구내

IC8TEM 관측은 소스에서 발견 된 피드백과 함께 작동합니다.

- diffraction에 대한 이탈리아 지리 통화. dorsal로, promontory와 섬은 역에서 몇몇 수십 킬로미터, diffraction 기여는 매끄럽게 땅과 같은 주요 가장자리에 Fresnel 매개변수로 계산됩니다. 회의: 563 MHz에 불규칙한 지형에 비교하여 (리오 데 자네이루, 5와 30 km 사이 27 점), 제일 방법은 잎 가장자리, P.1812 및 diffractive 쐐기를 가진 P.526, −5와 +3 dB 사이 평균 과 약 9 dB의 분산; P.1546는 가장 pesssimist ( Costa, Rabanal Ramirez 및 PETOOL 역 근처 가파른 사면을 위해 그들은 두 가지 완화제 (파웨이)를 반영하여 넓은 범위를 기억합니다.
- 먼 도관에 도착하려면 Fresnel이 필요합니다. 도관이 수평선을 넘어 시작하면 신호가 시작될 때까지 장애물에 대한 확산에 의해 우리에게 온다. 1.3.2에서 프로그램은 그것을 계산합니다 (프리비전 매뉴얼, § 10 ter). 회의: Rohde & Schwarz (2023) 높은 덕트를 따라 역은 종종 입력하지 못하지만, 그 끝을 닫습니다.
- 상태는 빨리 변화합니다. 이론은 거기, 그러나 조건은 분에 변화하고, 예측 체계는 순간에 자료를 가지고 갑니다: GFS는 매 6 시간, 시간 경간과 0.25°를 가진, 옵니다, 예측은 단기간 안에 가치를 잃을 수 있습니다. 회의: Bray (QST, 11월 1961)는 100 마일 이상 대략 ±7 dB의 적시 유동의 측정; 덕트는 대기 시간 (Rohde & Schwarz); 우리의 QSO 등록 29/12/2021–2/1/2022는 시간 안에 성장하고 떨어지는 것을 보여줍니다.
- 최고의 이제-캐스팅은 DX 클러스터–PSKReporter 네트워크에 남아 있습니다. 이제 무슨 일이 일어나는지 알고, 클러스터 스폿, PSKReporter (과 WSPR)는 어떤 패턴을 이길: 그들은 측정. 예측은 무엇을보고 얼마나 오래 지속 될 수 있다고 말합니다. 상업은 거기라면 말한다. 이 프로그램의 반점과 같은지도의 예측.

## 2. 제품정보

아이디어는 LIVETROPOVHF에 의해 계산 된 세 번째 인덱스이며 날씨 모델의 수직 프로파일에서 시작하여 Hepburn과 F5LEN 대신 교체합니다. 오늘 프로그램은 이미 준비된지도를 읽습니다: 색인은 또한 *why* 이 오프닝 (참고의 인용 및 간격, 최소한도 밴드 덫을 놓은)이고 프로그램이 이미 수집한 반점으로 일어날 수 있었습니다.

- 무엇을 줄 수 있습니다: 포인트 당 도관의 확률과 시간 당, 기본과 상단의 공유, 두께, 최소 주파수 트랩, 그리고 conduit 내부 경로의 분수를 따라.
- 무엇을 줄 수 없습니다: 얇은 덕트 (몇 10 미터 미만)는 0.25°에서 글로벌 모델이 해결되지 않으며 바다의 증발 덕트는 G h z에서 모든 봉사합니다.
- 첫 번째 실용적인 목표: 유럽의 144 및 432 MHz, DX 클러스터 지점에서 검증, PSKReporter 및 로컬 디코딩.

## 3. 기초 물리학

refractive에서 모든 시작 N는 각 단면도 수준에서 산출했습니다: 도관은 변화한 반응성 M가 상승을 감소하는 곳에, −157 N/km ([ITU-R P.453-13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf))의 밑에 dN/dh 가을인 곳에 입니다.

재현성 (P 및 hPa, 켈빈 T):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

상대 습도에서 증기 압력 H (%)와 온도 t (°C), −40와 +50 °C 사이 물의 위:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

수정된 반응성 (h km, 또는 0.157 · z m에 z) 및 덕트 선명도:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN/dh (N/km) | 인증 및 인증 | 144 MHz의 효력 |
| 0 댓글 | 관련 제품 | 감소된 흐름율 |
| 0에서 −79 | 정상 (표준 대략 −40) | 동등한 반경 4/3를 가진 라디오 수평선 |
| −79에서 −157 | 초경량 | 신장 흐름, "too good" |
| −15의 밑에 | 도관 (trapping) | 갇힌 신호, 1000 km 이상 DX |

압력 수준에서 N을 추출 하는 방법: 각 수준에 대 한 T, 상대 습도 및 지위 높이를 가지고, 당신은 N 및 M을 계산, 그 후 이웃 수준 사이 기온. 땅의 밑에 수준은 불멸되어야 합니다 (Alps에 1000 hPa는 지하입니다).

최소한도 덫을 놓는 빈도: conduit 가이드는 그것의 간격과 비교된 파만 짧습니다. Hepburn은 테이블 두께 → 최소 유용한 주파수 ([DX Info Centre](https://www.dxinfocentre.com/propagation/luf.htm)); 신호가 점차 하락하는 밑에 연약한 한계입니다.

| 지도된 간격 | 공급 능력 |
|  61 m  | 962 MHz의 |
|  91 m  | 464 MHz의 |
|  122 m  | 276 MHz의 |
|  152 m  | 185 MHz의 |
|  183 m  | 133 MHz의 |
|  213 m  | 1개 MHz |
|  305 m  | 5개 MHz |

VHF 범위에서 (120-300 m의) 테이블 값은 매우 잘 일정한 법률을 따릅니다; 100 m 미만 공식은 30-40%까지 주파수를 밑으로 나타냅니다. Adapting it own (Hepburn에 의해 출판 된 공식 없음), 와 f 에 MHz 과 d 에 미터:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

기본적으로, 2m의 경우, 도관은 종종 적어도 170 ~ 180m, 70cm의 경우 반 정도입니다. 그물에 순환하는 수식, λmax = 0.014 · d^1.5, Kerr에서 온다 (1951, 제 1, eq. 18) 그리고 λ에서 센티미터와 서로 유효하며, M이 선형으로 떨어지는 도관:이 단위는 약 2m 180m 및 약 70cm 89m을 제공합니다, 그것은에서 오는 Hepburn 테이블의 동일한 값입니다. Kerr는 그물 커트가 아닙니다 경고합니다: 몇몇 시간 더 긴 파도는 도관의 많음을 겪고, 약간이 도관에서 역과 조차 응접을 개량할 수 있는 방법. 2019 IEEE Access 연구는 고전적인 방법 (Kerr, Kukushkin)이 실제 절단 ([Shi et al. 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53))을 설명하지 않는다는 것을 확인합니다: 문턱은 벽으로 표시로 사용해야합니다.

## 4. 무료 데이터 제공

약한 점은 수직 해결책입니다: 1000년과 850 hPa 모형 사이 수준에게 각 25-50 hPa를 줍니다, 그래서 그들은 500-2000 m에 좋은 inversions를 보고 그러나 배경의 가까이에 얇은 덕트를 아닙니다. LIVETROPOVHF는 이미 Open-Meteo를 isobare, 온도에서 850 hPa 및 inversion에 사용합니다. 인덱스는 동일한 통화로 시작할 수 있습니다.

| 이름 * | 이름 * | 제품 설명 | 인덱스 사용 |
| [영업 개시, GFS](https://open-meteo.com/en/docs/gfs-api) | T의 상대 습도, 이슬점, 수평 (1000, 975, 950, 925, 900, 850 hPa...); 한계 층의 고도 | 압력 수준에 0.25°; 16 일, 120 h까지 시간, 각 6 h를 새롭게 하는 | 단면도 N와 M의 기온변화도, 기초 및 덕트의 정상; 프로그램에 이미 |
| [오픈 메테오, ECMWF](https://open-meteo.com/en/docs/ecmwf-api) | open-data 모형 0.25°에서 동일한 수준; 배경은 이슬점을 줍니다 |  0,25°  | 두 번째 의견, 모델 간의 일치에 유용합니다. |
| [Open-Meteo, DWD 아이콘](https://open-meteo.com/en/docs/dwd-api) | 압력 수준에 T와 상대 습도; ICON- D2 에는 800, 900 및 925 hPa가 있습니다 | ICON-EU 약 7km | 유럽의 세 번째 의견 |
| [Radiosunds, Univ. 와이오밍](https://github.com/NCAR/RAOBget) | 측정된 프로파일, 하루에 두 번 출시 (00 및 12 UTC), 역 당 텍스트 형식 | 몇 가지 중요한 수준, 심지어 모든 수십 미터 | 인덱스 검증; 모델이 보이지 않는 하위 덕트를 참조하십시오 |
| [GROANS, NOAA NCEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive) | 방송의 역사적인 아카이브, 역 당 파일, 더 많은 파생 된 매개 변수 | 현재 위치 | 과거의 데이터 및 비교에 대한 캘리브레이션 |
| ERA5 (수동) | 1940 년 이후 시간당 분해, 37 압력의 수준 | 24시간 | 통계 및 교정, 예측하지 |

- Open-Meteo geopotential 고도는 해수면의 위 입니다: 배경의 밑에 떨어지는 수준은 ([Open-Meteo, 위 공기](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api))를 쫓아야 합니다.
- 지상에는 2m에 T와 습도가 추가됩니다. 그들은 기온변화도의 첫 번째 기지를 주며 밤 표면 덕트가 평야와 바다에서 태어났습니다.
- Wyoming 아카이브 대학에서 이탈리아어 방송 (weather.uwyo.edu/upperair/sounding.html): 아래 테이블에 방송국과 번호 WMO.

| 역대역 | ₢ 킹 | 더 보기 | 경도 | 지원하다 |
| Udine/캠퍼스 |  16044  |  46,03  |  13,18  |  94 m  |
| 밀라노/Linate |  16080  |  45,43  |  9,28  |  103 m  |
| 바다의 연습 |  16245  |  41,65  |  12,43  |  12 m  |
| 한국어 |  16320  |  40,65  |  17,95  |  10 m  |
| Trapani / 버기 |  16429  |  37,92  |  12,50  | 약 14m |

Wyoming의 텍스트 형식은 각 레벨의 압력, 높이, 온도, 이슬점 및 습도를 제공합니다. N 및 M. Wyoming은 공식 인터페이스를 가지고 있지 않으며, 스크립트는 [NCAR](https://github.com/NCAR/RAOBget)의 RAOBget로 사용됩니다. DuPage 대학은 역 ([e.g., 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt))에 의해 텍스트를 유지합니다.](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. 인덱스 및 출판 방법

Hepburn과 F5LEN 는 동일한 물리적 성분에서 시작하지만 완전한 알고리즘을 게시하지 않습니다. 2008–2026의 과학 연구는 대신 단계로 방법을 설명하고, 그것은 조리법을 복사하는 것이 가장 좋습니다.

| 제품 설명 | 데이터 시작 | 계산 된 것 | 우리를 위한 유용한 노트 |
| [Hepburn Tropo 인덱스](https://www.dxinfocentre.com/propagation/hti.htm) | 캐나다 기상 서비스 (MSC, 그것의 지도에서 서면으로; GFS/NAM), GrADS로 설계된지도, 독점 알고리즘 ([storia](https://www.dxinfocentre.com/propagation/history.htm)) | 0–10 신호 curvature의 선형 가늠자: 0 정상, 1–2 최고 반복, 3–4 약간 덕트, 5–6 확산 덕트; 부정적인 값 = sub-refraction | imitated에 가늠자, 그래서 3개의 색인은 즉시 첫번째 2로 비교합니다 |
| [F5LEN, 세계 예측](https://tropo.f5len.org/WW/info.php) | 유럽의 ARPEGE 모델 Météo-France, 7.5 km, 2 m 및 1500 m 사이의 더 많은 레벨 ([저자](https://tropo.f5len.org/author/f5len/)에 의해 참조) | 주주 평균 지수, 3 시간에서 40 단계, 하루 4 계산, 28 일 아카이브 ([새로운 버전](https://tropo.f5len.org/?p=504)) | 그것은 많은 저수준을 가진 모형을 봉사한다는 것을 확인합니다 |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf) | 지상에서 3000 m에 60 ECMWF 모형 수준, | 컬럼 당 최소 dN/dz: −157 N/km의 밑에, −78.7 N/km의 밑에 최고 반복; 함정 층의 기초 | climatology: Po Valley는 매년 몇 개의 파이프 라인이있는 유럽 지역입니다. 15 % 미만의 겨울에 100 % 근처의 지중해; 여름 밤에 30 % 이상 이탈리아 |
| [Indermuehle 및 스즈키, CSIRO 2026](https://arxiv.org/pdf/2608.12861) | GFS 0.25°의 2개 m에 21의 압력 수준 + 분야 | M = N + 0.157 z; 최소 dM / dz 미만 3 km, 기본 및 덕트의 상단, M 적층의 깊이; 40 m의 카드 | 88 MHz에서 2,68 GHz의 25 채널에 7 년 동안 확인; 약 3 일 전에 좋은 용량 (AUC 약 0,74), 다음 가을; GFS ERA5 보다 더 나은 |
| [Hämäläinen 등, IMF 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf) | Harmonie-AROME, 750 m 또는 2.5 km, 65 또는 90 수준 | dM/dh < 0 on the model profile, 비교 4 ~ 59 m 빔 | 수평한 해결책 보다는 더 수직을 조사하십시오; 모형은 15 m의 밑에 덕트를 봅니다; 온도 gradient에 습기 지배 |

세 번째 아마추어 라디오 서비스 이미 활성: [TROPOCAST.EU](https://tropocast.eu/)of Artur SQ3A, 유럽에서 2 m 및 70 cm. 예측 페이지에서 ([tropocast.eu/forecast](https://tropocast.eu/forecast)):

- 데이터: DWD ICON-EU (pass 0.0625°, 약 7km), 기본 소스; 대안으로 캐나다 GDPS (0.15°), 더 적은 세부 사항.
- 방법: ITU-R P.453에 따라 N와 M 배경과 850 hPa의 층 사이 단면도에서; 층의 인식, 배경에 덕트의 조합 및 높은 색의 문턱은 저자의 적당한 구경측정, 간행하지 않습니다.
- 제품: "완전"보기 0–3000 m, 더 분리 된 전망 "지상"과 "높은"(지금 단지지도에서 통합); 점수 0–100 9 클래스에서 (38 배경, 38–52 마진, 53–58 약, 59–65 온건한, 66–72 좋은, 73–80 매우 좋은, 81-88 높은, 89–95 강한, 96–100 매우 강한); 시간당 +72 시간의 단계로 예측, 애니메이션 및 아카이브.
- 지역: 유럽, 폴란드, 영국, 독일, 프랑스, 스페인, 이탈리아, 체코, 크로아티아; 이탈리아에서 또한 공용영역.
- Reuse: maps는 API keyless iframe과 통합됩니다. 문서화 된 API (OpenAPI 및 JSON schema)도 있습니다. 라이센스는 표시되지 않습니다: Hepburn과 F5LEN 옆에 세 번째 소스로 사용하기 전에 저자를 요청해야합니다.

우리의 프로젝트의 차이: TROPOCAST는 850 hPa (약 1.5 km)에서 중지, 높은 덕트가 2000 m 이상에 도착하는 동안 ( LA0BY 의 경우 22/11/2018), 밴드 또는 경로에 대한 인덱스를 제공하지 않습니다. Classes 0–100은 같은 날에 인덱스 0–10과 비교할 수 있습니다.

TROPOCAST 이미지 구조 (프로그램의 저자에 의해 제공된 링크에서): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, n은 예측 단계입니다. 사이트 forbids 자동 인출 로봇에, 그래서 나는 이미지를 읽을 수 없었다; 또한, LIVETROPOVHF의이 맵을 보여주기 전에 저자의 권한을 봉사.

노트북: [DJ5AR, 카테고리](https://dj5ar.de/?cat=174)(Magonza, 21 기사 2017–2023, 2 m–13cm). 설명 된 케이스: 아일랜드에서 몇 주 실시 (23 5 월 - 2 6 월 2023), FT8 벨로루시 (2020 년 9 월), Alps와 Adriatic (8 월 2017)에 이탈리아 - 독일. 지상에서 사용할 수없는 "too high" 덕트 (1000m로 반전)의 경우를 인식: 역과 도관의 공유를 비교하는 데 필요한 또 다른 확인. Radiophares를 aperture 측정 (최대 1028 km) 및 파리의 수직 프로파일, 보르도 및 DWD.

요약에서, 일반적인 조리법: 층 당 수직 단면도 → N와 M → 층 dM/dh 부정적인 → 기초, 정상, 간격 및 덕트 → 색인의 힘. Hepburn의 "secret" 부분은 우리가 반점으로 remake 할 수 있는 실제 사건에 대한 경직적인 구경측정입니다.

## 6. 프로그램에 대한 유용한 정보

N 및 M (기본 물리학 섹션) 외에도 수직 프로파일에서 포인트 번호로 이동하고 경로로 이동합니다. ITUnu 수식은 PDF가 거의 읽은 부분으로 재구성되었습니다. 11월 22일에는 공식 텍스트 ([ITU-R P.452-18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453-14, P.526-16, P.372-18)에 검토되었으며 코드에서 수정되었습니다. troposcatter 190 (not 190,1)의 상수도와 ρ = 3 g/m3의 가스 흡수; α는 −3,4 (eq; s)의 α (fq; s)의 α (fq; s) 의 를 갖는다. Rumore P.372-18: 테이블 1 및 전술은 대응합니다; 인공 잡음 곡선은 250 MHz까지이고 프로그램에는 대략 100 MHz까지 전술은 동일한 법률을 확장합니다.

1. 명세 모델의 두 레벨 사이의 기온 (h km, M 단위 M):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. 명세 도관 힘과 간격: 간격 d = 정상 − 기초 (토양 덕트를 위해, 기초 = 토양); 기초와 정상 사이 M 부족으로 힘.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3. 명세 지구 반경 동등한 요인 (P.452-18, eq. 5 및 6a), ΔN = 평균 N 하락과 더불어 첫번째 km, N/km에서 긍정적인:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

ΔN = 40 당신은 대략 4/3 k (표준 대기권이 있습니다); 157 k 커튼 인피니티에 접근하는 ΔN로, 지구 라디오는 평평하게 됩니다: 도관의 시작입니다.

4. 명세 높은 안테나 h 미터의 라디오 수평선, 반경과 동등한 \_e km:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5. 명세 고도 (P.452-18, eq. 2), 시간의 비율을 위한 덕트의 확률; φ = 노선의 센터의 고도, μ1 및 μ4는 바다에 의하여 얼마나 멀리 있는 계정으로 가지고 갑니다:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

41°에서 N, 바다에 의해 모든 (μ1 = μ4 = 1), 시간의 약 11%를 제공합니다: 예상 인덱스가 다르다는 것을 참조 값으로 유용합니다.

6. 명세 troposcatter (P.452-18, eq. 45; f in MHz, d in km, mrad의 경로의 θ 각, 경로의 센터에 N0 지상 반응):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

−0,15 · N0 는 왜 젖은 공기도 덕트 없이 돕는지 보여줍니다: 50 여분 N 단위는 손실 보다는 더 적은 7.5 dB입니다. LIVETROPOVHF는 이미 분산 손실의 G3YGF 버전을 사용합니다.

7. 명세 duct (P.452-18, eq. 46–51)에 손실: 커플 링의 고정 손실과 각 밀리 라디언트에 대한 유혹, 주파수의 입방 루트 (G h z, a\_e km):

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. 콩과 도톤 (1966)과 케레 (1951)에서

두 개의 고전적인 텍스트는 인덱스에 즉시 필요한 세 가지 것들을 제공합니다: 그라디언트 임계 값은 GFS의 동일한 수준에 이미 관세, dB의 변화에 가치가있다, 어떤 날씨 상황 생성 또는 덕트를 파괴. *Radio Meteorology*, NBS Monograph 92.

1. 명세 압력 층 (Bean 및 Dutton, 탭에 대한 프로필 분류. 6.5, p. 248; N / km의 −dN / dh). 라디오 파장을 위해 디자인해, 그것은 모형 수준에 그것으로 적용합니다:

| 로드 중... | 관련 기사 | 공지사항 | 분류되지 않음 | 초경량 |
| 1000-850의 hPa | 24시간 | −dN/dh < 0=""> | 20에서 60까지 | 100 이상 |
| 850–700 hPa의 | 1,46–3,01 킬로미터 | −dN/dh < 0=""> | 20에서 50까지 | 80명 이상 |
| 700–600 hPa | 3,01–4,20 km | −dN/dh < 0=""> | 15에서 40까지 | 70 이상 |
| 600-500의 hPa | 4,20–5.57 km | −dN/dh < 0=""> | 10에서 30까지 | 50 이상 |

2. 명세 dB(Bean and Dutton, cap. 6, pp. 233–258)의 가치는 수평선을 넘어 노선에 70 ~ 180 MHz의 측정입니다.

- 어떤 층 (지상 또는 고도에)에 비교된 “smooth” 단면도는 모든 비율에 10-25 dB의 신호를 올립니다; 더 중대한 이익은 높은 경사한 층에서, 아마 집중 효과 (200 km의 코스) 때문에 옵니다.
- 지상 반응성 N 및 첫 번째 km ΔN의 쇠퇴는 0.4과 0,95 사이 계수를 가진 매달 매체 신호와 관련이 있습니다, 매체 0,7 (130에서 446 km, 대략 100 MHz).
- 연간 주기: 단위 N 당 0.18 dB; 단위 N 당 0.2에서 1.1 dB, 175-200 km 및 겨울의 노선에 최대.
- Misme의 화합물 모수는 0.5 dB/(N/km) · (동등한 gradient − 40)로 열 안정성의 기간을 예상합니다; turbulence D의 모수 (실제적인 단면도와 adiabatic 그들 사이, 0–3 km)는 다만 간단한 ΔN와 비교된 개량합니다. 저자의 결론: ΔN은 단순성을위한 최고의 매개 변수를 유지합니다.

3. 명세 참조 프로필 및 anomaly (Bean 및 Dutton, § 3.8 및 3.9). 참조 CRPL의 exponential 대기는 Ns에서 시작하는 "정상적인"프로파일을 제공합니다. 단위는 각 공유에 얼마나 많은 실제 프로필이 다릅니다. GFS 프로필을 anomaly로 변환하는 자연스러운 방법입니다.

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

Ns = 313 당신은 −42 N/km에 관하여 ΔN가, 그것 있습니다 동등한 반경 4/3. −157 N/km 도관 선회는 또한 안테나 고도에서 거의 독립적 인 수평한 반경 (eq. 4.23)를 위한 Snell 법률에서 여기에서 파생됩니다.

4. 명세 어디와 conduits가 발생했을 때 (Bean과 Dutton § 4.4; Kerr 모자. 3):

| 제품정보 | 관련 기사 | 제품 정보 |
| 제품정보 | 찬 바다에 흐르는 땅에서 뜨겁고 건조한 공기 (좋은 날씨의 일 후에 여름 소매의 전형); 주로 습도 적층을 조사합니다 | 바다에 강렬한 도관 |
| Nocturnal 방사선 | serene 하늘, 약한 바람; 낮은 구름 또는 많은 습도는 그것을 방지합니다 | 일몰에 태어난 땅에 주도, 저녁에 성장 하 고 해 일출 후 빨리 사라, 특히 내부 |
| 이름 * | 항cyclone에서 내려 오는 공기, 가열 및 건조, 신선하고 겸허한 공기; 항cyclone의 산과 남동 지역에서 자주 홍수 | 최고 민감하는 층 또는 높은 덕트; 거의 항상 역에 있는 표준 보다는 높이 |
| 바다와 땅 | 해안, 몇 백 미터의 순환 | 해안을 따라 지역 파이프라인 |
| 도관에 대하여 | 섞기: 중간 아침과 오후 사이 태양에 의해 가열된 지상은 거친 배경에 몇몇 m/s에 배경에 바람; 밤 안개 | 도관의 확률 거의 아무것도 |

5. 명세 시도하는 두 가지 새로운 재료. 수직 모션: 융합 영역의 전류를 제거하고 레이어를 삭제하고, 하위 측면은 신호를 강화 (모러와 홀렌: 평균 약 2.5 dB 이상, 밴드 L 및 X의 바다에서 언덕 공기와 함께). Open-Meteo는 이미 압력 수준에 수직 속도를 제공합니다. 그리고 압력 추세: 140 km의 경로에 186 MHz의 신호가 전송기에 떨어질 때 압력이 떨어지고 경로의 센터에 상승하고 높은 반환, 단지 2 (Bean과 Dutton, fig. 6.9, p. 241).

6. 명세 Kerr에서. 최대 파장 (기본 물리학 단면도)의 공식 이외에, Kerr는 단지 온도에서 M를 위한 대략 공식을, 증기 및 고도 (§ 3.9, 오래된 일정과 더불어 79 그리고 4800 그 후에 77,6 및 4810) 대체하고 바람의 절단을 반전하는 관찰하는 것을 관찰합니다 덕트의 고도에 그네를 생성할 수 있습니다, 깊은 탈출의 가능한 원인 (§ 3.26). 이탈리아어 소스 콩과 드로튼에 의해보고 검색: L. Bonavoglia (12 월 1958), "메테로리 페메나와 전파 사이의 상관 관계..."

## 8. 아마추어 라디오 발표 및 기술 보고서 (둘째 시리즈)

프로그램의 저자에 의해 제공되는 일곱 문서. 색인에 직접 3개의 봉사: W3SZ 를 위한 troposcatter “바닥”, 케이스 Hepburn를 위한 LA0BY 는, 덕트의 날씨 분류를 위한 Hepburn ( LU7MC) 예측하지 않았습니다.

1. 명세 Troposcatter: 신호는 항상 거기 있습니다. R. Rehr W3SZ, "Tropospheric Scatter (Troposcatter) VHF, UHF, 전자 레인지 및 주파수에 대한 전파", 패킷 Rats Conference 2014; J. R. Duffey KK6MC, " Troposcatter Propagation 모드 및 VHF / UHF 밴드에 대한 예측 적용 방법", CSVHF 2017.

- 약 100 ~ 700km, 144 MHz에서 10 GHz의; 손실은 주파수와 30 로그 f의 성장.
- 모두 위의, 그것은 수평선으로 시작 각도를 계산합니다. 각 추가 정도는 약 9-12dB ( M1CNK, 콜린스 방법).
- 여름에서 신호는 겨울보다 약 10dB가 높으며, 이른 아침이나 저녁에는 약 5dB가 낮습니다. KK6MC 는 시간, 시즌 및 시간을 가진 13-15 dB의 총 가변성을 나타냅니다.
- 예시 방법, 프로그램 (MHz의 F, 마일에서 d, 도의 θ 각도, 두 번의 오프닝의 α 기하학적 평균):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Larsen (1968)의 비교 15 실제 연결: 콜린스 평균 사각형 오류 5,1 dB, 예시 용 7,4, 8.1 NBS 및 CCIR, 16,8 라이더 용. Ns가 꾸준히 유지되는 경우 모든 방법 개선 (Yeh 방울 5.4 dB): 하루의 굴절률을 약간 감소시키기 위해, 그래서 tropo 색인은 층을보고해야합니다, 뿐만 아니라 Ns. 측정된 손실 (Feldberg–Monte Baldo 포함, 322 957 MHz, 223,1 dB; 키프로스–이스라엘, 77,5 MHz, 163,3 dB)에서 319km, 우리의 계산에 대한 좋은 테스트 벤치.
- KK6MC 는 전형적인 역이 2m를 초과하는 것을 관리하고 대응 거리, SSB에 있는 184 dB (FT-817와 3개의 성분, 대략 130 km)에서 215 dB (SSB에 있는 긴 bungs, 대략 430 km) 및 236 dB (kW 및 yagi는, 대략 640 km 결합했습니다)에 손실을 줍니다. 인덱스 dB를 얻은 킬로미터로 변환하는 방법입니다.
- 준비 코드: W3SZ 예와 콜린스 프로그램 (nitehawk.com/w3sz/YehCollins.zip)과 밥 Atkins KA1GT 계산기 (bobatkins.com/radio/troposcatter.html).

scatter에 또 다른 소스: I. 로버트 ZS6BTE, "Tropospheric Scatter Propagation", *Radio ZS *, 12 월 1984, 주의 사항 2013 ([qsl.net/zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). 이름 * 유용한 기하학: θ0 = d/R, θ1 = (h1 − h\_ostacolo)/d1 + d1/2와 더불어 θ1 − θ2의 각 다른 역과 동일 R = 8446km (4/3의 지구 반경). troposcatter "floor"의 배경의 프로파일에서 각도를 계산하는 데 사용되는 조각입니다. 다른 자료: 10 dB에 관하여 더 많은 비용에 있는 각; Ns 280에서 300에 전환은 대략 4 dB의 손실을 감소시킵니다; 흩어지기 양은 300를 위한 300 그리고 2000 m 사이에서, 600와 300 km를 위한 3000 m 사이 입니다; 흩어지기 양에 있는 폭풍은 대략 20 dB의 신호를 제기합니다. 기사의 숫자 예는 주어진 공식으로 돌려보내지 않습니다 (900 MHz에 자유로운 공간의 기간 및 872 km는 대략 150 dB, 그래서 그(것)들을 사용하여 검사되어야 합니다. W3SZ

2. 명세 도관의 유형 (W. R. Hepburn, "Tropospheric Propagation Modes", dxinfocentre.com/propagation/tr-modes.htm, LU7MC 의 스페인어 번역). 이탈리아의 유용한 케이스와 콩과 Dutton의 테이블을 완료:

| 제품정보 | 시간: | 인덱스 |
| 관련 기사 | 땅에 평온하고 평화로운 밤 | 땅을 따르고, 새벽 후 곧 사라집니다. |
| 고압 (subsidence) | 항cyclone | 매일 지속될 수 있습니다; 아래에 irradiation에서 도관은 먼 신호를 막을 수 있습니다, 그래서 주간은 더 나은 갈 수 있습니다 |
| 기타 | 프론트 앞, 추운 앞, 역 앞 북쪽 | 짧은; 나쁜 날씨는 그를 방해 |
| 산 descent | föhn, bora: 공기가 내려, 난방 및 건조 | 에 대한 시도 bora 에 a adriatic 과 föhn 남 의 a 알프스 |
| 제품정보 | 감기에 뜨겁고 건조한 공기 및 젖은 토양, 비 후에 예를 들면 | 지상에 지도하는 |
| 언어: 영어 | 찬 공기에 뜨거운 공기는 계곡에 멈춥니다 | 당일 숙박 가능 |
| 인기 카테고리 | 냉수에 뜨겁고 건조한 공기; 바람과 해안에 도착 | 오후에 피크; 봄과 초기 여름 최고의 계절 |

바다에 증발 덕트 (0–30 m)은 약 3 GHz를 봉사하고 hinterland를 입력하지 않습니다: 144 및 432 MHz의 경우 무시 할 수 있습니다. 높은 산은 장벽입니다; 사막은 너무 건조합니다.

3. 명세 이 경우 Hepburn은 예측되지 않았습니다 (S. Heck LA0BY, "144 MHz 및 Up의 Extreme Tropo propagation", JO59IX 에서 500m의 고도로 Nordic VUSHF 2019:

- 22 11 월 2018: 70cm ~ UA6 (약 2600km) 6 ~ 8 시간. Hepburn은 발트국과 폴란드에만 표시됩니다. 도관은 900 및 1500 m의 온도지도에서 보았으며 2000m 이상 종료되었습니다. 노르웨이 측에서 높았으며 언덕에 LA0BY 만 들어갔습니다. 지상 시간에 trope처럼 보이지 않았다: 상단에 감기와 안개.
- 2015년 2월 11일: 2m, 70cm, 9A, S5 (약 1500km), Alps를 통해, Hepburn이 금지하지 않습니다.
- 2011년 10월 15일: 높은 덕트에 있는 EA1 (2135 km)를 가진 2개 m, 바다와 3개의 배경에.
- 기타 행사: 2003 년 11 월 6 ~ 11 일 동쪽 (2050km), 2004 년 12 월 10 일 프랑스로.
- 필드 주소: 평야에 있는 안개, 자유로운 첨단, 작은 바람, 젖은 배경. 정상적인 온도 기온 기온 기온 −6,5 °C/km; 진짜 경우에 ( VK3KAQ) +7 20 m에 있는 °C.

저자의 결론: Hepburn과 F5LEN 는 충분하지 않습니다, 그들은 경로에 따라 고도에 프로필을 제공합니다. 저희의 경우: 이 날짜는 모델의 역사적인 데이터로 재 계산 될 수 있으며, 경로의 인덱스는 역의 할당량과 덕트의 할당량을 비교해야합니다.

4. 이탈리아 경험 (G. IV3GBO, "VVVV 전파 SSB / CW의 유형", 2017): 450 및 3000m 사이의 층과 함께 450 및 3000km 사이에서 실시; 안개가 절묘함에 따라 확장; Puglia, 알바니아 및 그리스를 향한 Adriatic에서 바다로 엽니 다. 100에서 500km 거리에 있습니다.

5. 명세 소개 (P. Wilton M1CNK, "VVV / UHF 추진에 대한 소개", 2013): 70 cm는 2 m 보다는 더 낫습니다; 144 MHz에 10 m 극으로 시작 각은 대략 3°, 20 m와 더불어 대략 1.5°.

6. NASA 보고서 (E. Hirschmann, "Tropospheric and Ionospheric Effects on Radio Frequency (VHF-SHF) Communication", Goddard X-731-67-89, 1967): DX가 아닌 위성에 거리 오류가 발생합니다. 그것은 같은 N 공식과 exponential 프로필을 다시 가져옵니다 (예: 플로리다에서: Ns 370, ΔN −55, 일정한 0.161/km) 그리고 검색에 두 NBS 소스를 보냅니다: 콩과 Thayer, "CRPL Exponential Reference Atmosphere", Monograph 4 (1959), 콩, Horn 및 Ozanich, "Climatic Charts and Data of Radio Refractive Index for the United States and World", Monograph 22 (1960).

## 9. VHF/UHF DX 책에서 2 장

I. White G3SEK (edited by), * VHF/UHF DX Book*, DIR Publishing 1995 (ISBN 0 9520468 0 6; 디지털 복제 2017), 2 장 "VHF/UHF 추진", pp. 2-13, 2-22–2-29 및 4-11. 그것은 수집 된 중 라디오 아마추어 경험에 가장 가까운 텍스트입니다; 이미 문턱을 확인하고 새로운 동기화 규칙을 추가합니다.

이름 *

- N의 동일한 공식 (77.6 P/T + 3.73·10^5 e/T2); 예를 들면: 1000 hPa, 7 °C 및 = 10 hPa 손상 N = 324. 잘 모양의 분위기에서, 예를 들어 천둥 후, N은 처음 2 ~ 3km의 39 단위 / km에 대한 거의 선형입니다.
- 0과 −79 N/km 사이 Gradients는 −79의 밑에 정상적인, 0 이하 반복 이상 super-refraction입니다. 긴 DX가 전체 경로에 맞을 수 있지만 덕트에서 간단한 슈퍼-refraction에서 오지 않는 책.
- 최소 덕트 두께, Kerr와 Hepburn 보다는 조금 더 prudent:

| 언어: 한국어 | 최소한도 간격 |
| 50 MHz의 |  400 m  |
| 70 MHz의 |  300 m  |
| 144 MHz의 |  200 m  |
| 432 MHz의 |  100 m  |
| 1.3 GHz의 |  50 m  |

- 비율은 파장에 비례가 없습니다 (약 65 λ에 50 MHz, 1.3 GHz의 220 λ에) 도관의 가장자리가 "분쇄"이기 때문에. 영국에서 파이프 라인은 144 MHz에 자주 있지만 거의 70 MHz 이하에서 결코, 종종 432 MHz가 강한 경우 144 약합니다.
- 1/d2 대신 1/d로 신호 방울을 파이프라인에서: 1000까지 kms묽게함은 자유로운 공간의 그것과 유사할 수 있습니다.
- 2 m (100 W, 15 dBi yagi, 2 dB 소음)에 최소 역 용량: 217 dB, 평균 조건에서 SSB에서 약 400-500km; CW에 대해 20 dB (p. 4-7). KK6MC 테이블에 동의.
- Scatter: 손실은 3배에 의하여 대략 15 dB에 의해 빈도를 증가하고 10 dB는 흩어져 각 (예 공식의 각 정도를 위해 상승합니다; 원본에서는 일정한 읽을 수 있습니다, 그래서 W3SZ 의 버전에서 가지고 가야 합니다). convective 세포에서 "up" scatter는 약 500km, 1000km의 좋은 역을 포함합니다. 무거운 비와 장시간 조건은 모든 밴드에 스카이스입니다.

덕트의 종류

- 지상에서: 지구에 그들은 토양이 흡수하고 퍼지기 때문에 악화합니다. 평온한 바다에 도착 (실리 - 카나리, 2500 km, 약 1 년 2 m). 증발 덕트는 종종 15m 높이에 불과하며 전자 레인지를 제공합니다.
- 높은: 지상에 더 긴 DX 때문에 450와 2000 m 사이; 아래에 역의 위 신호 “높은”. "lose" 덕트는 더 적은 강한 신호를 주고 또한 중간 거리 역을 허용합니다.
- 도관은 정확한 방향을 가지고 있으며 지난 시간 또는 일, sporadic E에 반대 할 수 있습니다.

인덱스의 새로운 Synoptic 규칙

- 항cyclone의 나이: 시간 당 몇몇 미터의 센터에 공기 방울 및 반전은 고압 발전으로 낮춰집니다; 더 중대한 오프닝은 이미 좋은 선전의 몇몇 일 후에 도착합니다. 단기 anticyclone은 충분하지 않습니다.
- 고압 가장자리: 층은 돔이고 가장자리에 더 낮습니다, 그래서 역은 잘 얻습니다.
- 방향: 그것은 더 긴 경로가 isobares를 따라 실행 또는 dorsals (Flavell G3LTP, 1984)을 통해 관찰되었지만 센터의 반경 방향에도 우수한 연결이 있습니다. 1987년 8월 29일–30일 예: 영국에서 열린 180°를 2일(스페인, 프랑스, 이탈리아, 오스트리아, 노르웨이)으로 회전하였습니다.
- Against: 두꺼운 isobare (강한 바람), 층의 가장자리에 stratocumuliform 구름 (최고의 빈도를 제한하십시오).
- 정면: 찬 정면은 열기를 위로 밀고 정면을 따라서 반전을 창조합니다; 제일 방향은 정면을 통과하는 동안, 그러나 약간 지속합니다.
- 시즌: 영국 anticyclones는 여름과 이른 가을의 끝에 더 빈번합니다.
- 시각적인 clues: 수평선의 어두운 선에서 덫을 놓은 연기; 1000 m 이하 기초를 가진 고압, layercumules로 지상 덕트, 1000 m 이상 기초 높은 덕트를 나타냅니다.

검색에 인용 된 참조: R. G. Flavell G3LTP, "Extensive Anticyclonic Propagation Event...", *Radio Communication*, February 1984, p. 128; J. Gannaway G3YGF, "Tropospheric Scatter Propagation", *Radio Communication*, September 1981, p. 710; P. W. Sollom G3BGL, "A Little Flutter W6ZGN

## 10. 확인 아마추어 라디오 모델: Biancomano 과 Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, "VVVV / UHF 전파 예측을위한 실용적인 Tropospheric 모델", * 국가 날씨 다이제스트 * 22 년 6 월 1998, pp. 15-24, 국가 기상 협회](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf)에서 [PDF의 무료.

- 데이터: 동부 미국의 47 방송국의 00 및 12 UTC의 라디오 파, 1995-1997; 700 hPa (400까지 시작)에 지상 분석, M. Excluding 해안, 섬 및 산없이 N 프로파일에.
- 각 설문 조사: 덕트의 존재와 공유, 지도할 수 있는 최대 파장 (100배 파장에 관하여), 이웃 수준 사이 잠재적인 온도의 최대 기온 및 1개의 수준 사이 두번째 위 (두껍게 층을 위해). 지상 덕트는 사용되지 않습니다.
- 첫 번째 규칙, 지속: 안정성 지수 (lifted index, K, totals-totals, SWEAT)는 조사 12 시간 전에 3 단위 이내에 남아 있으며 잠재적 인 온도가 더 많은 것을 남아있는 압력 수준이 동일합니다. 그렇다면: 다음 12 시간의 개선 예측.
- 두 번째 규칙, 동적 제어: 증가하지 않는 제한 층, CAPE, 두께의 변화는 500 hPa에 세 가지 조사, 한계 내의 사면의 변화.
- 밴드 임계 값: 100cm 이상 운반 할 수있는 도관은 432 MHz의 오프닝을 나타냅니다. 거의 222 및 종종 144.
- 공간 필터: 약 400 ~ 500km의 6각형 그리드의 역; 예측은 적어도 5 contigue 역이 긍정적 인 경우에만 가치가있다 (하나를 건너 뛸 수 있다면). 5개의 역으로 거짓 임의 긍정적인 확률은 무시할 수 없다.
- QST의 칼럼 "세계 50 MHz 이상"의 이벤트에 대한 검증: 1996, 67 (71%); 15 주요 이벤트의 12에 계획 된 48 이벤트. 432 MHz에 70%까지 정확도와 12 시간에 144 MHz에 관하여 60%; 그것은 거의 두 배 실제적인 사건을 제공합니다; 19의 9개의 과실은 12 시간 보다는 더 적은에서 많은 개선 모양이 도착하기 때문에, 예측 도착했습니다.
- 더 북쪽으로 역에 추가하십시오: 여분 상태로 바람 자전 시계방향 상승 (뜨거운 시작).

완전한 PDF에서 (프로그램의 저자에 의해 공급) 세 가지 세부 사항. 그들의 규칙 "두께 = 100 배의 파장"은 600 m 6 m, 200 m 2 m, 125 m 1,25 m 및 70 m 70 cm에 대 한 (fig. 3): 70 cm에 대 한 Kerr와 Hepburn (약 89 m) 보다 덜 심각 하 게, 2 m에 대 한 그것은 VHF/UHF DX 책으로 coincides. 시계 방향으로 회전하는 바람은 저자에 따라, 특히 고도에 더 따뜻한 공기를 제공하기 때문에, N의 가을은 압력으로 성장; 습기는 분해하지 않습니다. 부록은 주어진 할당량에 열 기온변화도 및 습도의 날카로운 도약을 가진 일정한 열 기온변화도를 위한 닫히는 모양에서 dN/dh를 얻습니다: 우리의 부호를 위한 시험 상자로 유용합니다. 잠재적 인 온도의 경사의 추적은 K에서 온다. H. 젠, "Scioptic Scale Radio meteorology의 잠재적 인 굴절률의 사용", *J. Meteorology* 17, 1960, p. 264.

우리의 색인을 위해 가지고 가는 무엇: (1) 지속의 용어: 12 시간 전에 프로파일을 비교, 우리가 무료 한 시간 동안 예측; (2) 공간 필터: 개선이 거짓 경보를 줄이기위한 몇 백 km의 영역을 다루면 포인트 수; (3) 안정성 지수, 온도 및 데우에서 계산 850, 700 및 500 hPa (Open-Meteo는 이미 상승 지수와 CAPE를 제공합니다); (4) 1000Pa 및 850Pa의 고도와 함께 회전 바람; 우리는 동일한 방법을 원하는대로, 우리는 같은 방법을 원하는대로.

같은 패키지의 또 다른 소스, 우리에게 약간의 사용: P. K. Rastogi, "Stratosphere and Troposphere (S-T) Millstone Hill의 연구", NASA 1984 (document 19840019069): 440 MHz 레이더는 중력 파 및 풍력 절단과 관련하여 얇은 층과 4 및 25km 사이의 맑은 공기의 turbulence를 측정합니다. 그것은 흩어지기를 생성하는 turbulence가 계층화되었지만 덕트에 너무 높은 확률이 우려한다.

## 11. 소스의 세 번째 라운드 (20 11 월)

높은 덕트 역사 사례. J. Macoun OK1VR, Sněžka의 정상 회담에서 2m (1602 m), 가을 1958, 1959 년 번역 된 기사 ([ok2kkw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)). 에 27–28 10월 1958 첫 체코슬로바키아어–영국 연결 145 MHz ( G5YV, 1220 km) 그리고 더 많은 사이에 1100 그리고 1250 km, 후 안정적인 신호와 함께 23. 저자는 2개의 superimposed inversions에 그것, 더 낮은 층이 녹이는 지상에 신호 반환과 더불어 2200 m에 관하여 갑피합니다; 대서양에서 대륙에 느린 anticyclone에 의해 호의를 베푸십시오. 집 (Prague)에서 최대 530km였다: 고도의 역은 도관을 입력했습니다. 검증의 또 다른 경우입니다 ( LA0BY 2018와 같은) 인덱스가 2000m 이상을보고 역의 공유에 비교한다는 것을 확인합니다.

역사 도서관 NBS (남성 작품에 적합, 공식에 대 한):

- W. Nupen, * Radio Waves*, NBS Technical Note 304, 1965, 356 페이지의 Tropospheric Propagation에 대한 방사형, 국가별로 주제에 대한 색인. 이탈리아의 항목보고: L. Sacco, " 대기 슈퍼 파괴 요법의 라디오 연결", * 높은 주파수 * 24 (6), 12 월 1955 (이탈리아 연구: 500 m의 빈번한 일과 밤 아래 super-refraction); S. Silleni, *Annali di Geofisica * 8(1), 1955; J. M. Clara e A. Antinori, prova tra Sardegna e Minorca da 238 a 297 MHz (390 * 390 *), 1956 * F.C.C., C.C., C.C., C.C., C.C.C.
- R. L. Abbott, * Tropospheric Radio Wave Scattering*, NBS, 재료는 1 월 1960 일까지, 약 670 항목은 연간 주문: troposcatter 부분에 유용합니다.

공시 및 관측: F. W. Odenthal DK5EQ, "Ausbreitung 2m - Eine Einführung" (31/05/2024): 거의 최대 200km, 덕트 당 tropo 또는 굴절, 고압 및 봄과 가을; DM2HB 의 VHF 페이지를 인용하고 tropo 및 Es의 DK5YA 의 노트. EI7GL 의 블로그에 Johnson VK3DXE ( 2021 년 12 월). VHF marina와 AIS에 대한 토론 ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)): 범위 1,23·√h (ft) 안테나 당 nautical 마일, 약 50 nm, 전형적인 trope 개선 100-400 nm까지 흩어져; 반대 역이 이웃만큼 강하기 때문에 도관이 인식됩니다. Chelmsford ARS, 1983년 12월: 2m 경연 대회에서 영국 동부 해안의 역은 대륙에 바다 노선에 의해 참조됩니다.

ARRL, * 라디오 아마추어 VHF 매뉴얼 *, 1972, 모자. 2 "고속도를 넘어 보호"(pp. 17–19). Already 알려진 운영 팁 (고압 지역을 천천히 이동하고 모든 뒤 가장자리 이상; 초기 가을은 가장 좋은 기간; 해안 지역, 큰 호수와 계곡의 강은 stratification에 의해 호평). 2개의 새로운 자료: (1) fig. 2-4는 표준 분위기 ( 상대 습도 70 % 최대 12000 ft, 최소 굴절)를 비교하여 강력한 굴절의 전형적인 반경으로, 맑은 온도 휴식과 약 4000 ft (약 1200 m)의 혼합 비율: 우리의 계산이 GFS 수준에 인식해야하는 서명입니다. (2) 진정한 덕트는 최소 두께에 따라 144 MHz와 가장 자주 UHF에서 드물게됩니다. 검색에 유용한 QST bibliography: 선체, "Air-Mass 조건 및 UHF 웨이브의 굽힘"(June 1935, May 1937); 호리스 톤, "두 미터 오프닝의 패리스 사전"(10 월 1949); 콜리, "2 미터 DX의 우퍼 공기 조건"(9 월 1955); Botts, "A Night to Remember"(1 월 1970, 토지에서 실시).

PA0EHG, 페이지 "Tropo"([pa0ehg.com](https://pa0ehg.com/tropo.htm)): G3YGF 기사에서 troposcatter의 요약, 이미 설명서. 우리의 흩어지기 계산을 통제하기 위하여 2개의 실제적인 규칙을 사용하십시오: 각 역의 수평선에 각 각 각을 위한 대략 10 dB 여분 손실, 그리고 대략 9 dB 플러스 각 100 km 노선 (60 마일); 일반적인 양은 대략 600 m에서 100 km 그리고 480 km에 대략 9 km 시작합니다. 덕트, 오염 또는 굴절 임계값을 치료하지 않습니다.

PA0EHG, PROPBER 계산기 ([page](https://pa0ehg.com/propagat1.htm), propber.exe 프로그램은 프로그램의 저자에 의해 제공). 그것은 BASIC에서 작성된 36 kB DOS 프로그램이며 Microsoft BASIC Compiler 5.60 (1985)로 컴파일됩니다. DOS 또는 DOSBox에서만 실행됩니다. 내부 텍스트에서: 주파수, 거리 (또는 오래된 QRA 형식 "E-locator")의 두 위치, 각 안테나 높이 역, 소음 수치, FI 밴드, 전력, 장애물 (높이 및 거리) 및 안테나 이득 (0 = parable 직경 계산, 효율성 50 %)에 대한 요청. 광학의 최대 거리를 반환, 증기 (0.1 dB / km 24 GHz에서)의 흡수에 대한 정확한 광학 손실, troposcatter의 손실 "사이트에 대한 정확한"(즉, 장애물에 의해 주어진 수평 각도에 대한) 그리고 두 가지 감각의 S / N 비율, 또한 "최대 이상적 추진과 함께". Numerical 상수는 컴파일된 실행할 수 없습니다. 높은 밴드에 대한 제안 된 방법은 또한 유용합니다: 측정 된 링크 (측정 및 계산 된 S / N)의 계산을 측정하고 다른 밴드와 동일한 차이를 적용. 우리의 계산의 동일한 패턴이지만, 그것은 단지 하나의 장애물 대신 S RTM1 프로필과 고정 마진 대신 GFS 예측을 사용합니다.

PROPBER, 상수는 executable (Microsoft 바이너리 형식의 숫자)에서 읽습니다. 텍스트 옆에 데이터 테이블에서 πre: 8,68589 (= 20/ln 10, i.e. 20 로그는 천연 로타리톰으로 계산), 32.5 (무료 공간: 32.5 + 20 로그 f\[MHz\] + 20 로그 d\[km\]), 24000 및 0.1 플러스 40000 및 0.3 (vapore: 24 GHz의 0.1 dB / km, 40 GHz의 0.3 dB / km) 코드를 반복하기 때문에 Probable: −69,5 (VVV / UHF DX Book의 Yeh's side-scatter 공식과 동일한 일정 기간), 332 (지상 refractive), 8733 km (동등 반경, k ≈ 1.37). 정확한 scatter 공식은 프로그램을 분해하지 않고 재구성 할 수 없습니다. T0

PROPBER는 코드 ( 컴파일 된 부분의 분해; 작업은 메모리의 연산자와 BASIC 라이브러리에 호출되며, 두 배 정밀도의 값은 디코딩되었습니다). 상수 −69,5, 332 및 8733은 이전 라이브러리 내에서 거짓 긍정적이었습니다.

- 광학 거리: d = 5 · (√h1 + √h2) km (m에서 h). 그것은보다 관대 한 4,12 (k = 4/3): k ≈ 2.와 동일
- 광학 손실: L = 32.5 + 20 로그 f\[MHz\] + 20 로그 d\[km\], 플러스 0.1 dB/km 이상 24 GHz와 0.3 dB/km 이상 40 GHz.
- Troposcatter: Lts = L + 23 + 10 로그 f + 0.0674·d, i.e. 55.5 + 30 로그 f + 20 로그 d + 0.0674·d ( G3YGF 의 일정한 2 dB 높은). 각 기간 또는 반응성 없음.
- 수정 "사이트의 경우": 프로그램은 장애물과 거리를 묻습니다. 그러나 사용해야하는 블록은 비어 있습니다. 보정 변수는 0이므로 "correct"손실은 사기꾼과 동일합니다.
- 소음: T = 290·(10^(NF/10) − 1), N = −228,6 + 10 로그 T + 10 로그 B (dBW), 안테나 온도 없이.
- S/N = 10 로그 P + G\_tx − L + G\_rx − N. "최대 이상 propagation"은 흩어지기 대신 광학 손실을 사용, 즉. 무료 공간을 넘어 손실없이 이상적인 도관 케이스.

비교: 300 km와 144 MHz PROPBER는 대략 189,9 dB, G3YGF 187,9의 우리의 P.452 187,8를 줍니다 (제로 수평선 각에): 2 dB 내의 흩어져 coincides. 우리의 계산은 단면도, 안테나 온도에서 수평선 각을 추가하고, "maximum" 대신에 잔여 성숙과 묽게함으로 예상한 층을 고쳤습니다.

Rohde & Schwarz, P. Denisowski, "Understanding VHF 전파", 교육 노트, 4 월 2023 (24 페이지, 해산). 이미 수집된 포인트를 확인합니다 (두 차원 파 가이드로, 거리와 선형 방식으로 떨어지는 신호, 시간에서 일까지 1500 km 및 저쪽에, 시간에서 내구; 바다에 좋은 배경 및 땅에 스카이스; 약간 100 미터에서 몇 km로 상승). 인덱스에 대한 두 가지 유용한 관찰: (1) 진화 시간: 새로 형성 된 도관은 종종 가장 높은 주파수를 운반하고, 두껍게, 가장 낮은 가져 오기 시작합니다; 시간별 예측은 따라서 레이어가 성장 할 때 144 MHz의 432 MHz "진행"을 보여야합니다. (2) 역 위치: 가장 좋은 조건은 도관의 끝에 가까운 역 사이이며, 높은 도관의 길을 따라 종종 입력하지 않는 한, "who 잃". 덕트는 대기 날씨로 이동합니다.

사이트 PA0EHG, 다른 페이지. trope의 경우, M. Willis G0MJW 의 PathProfile ([scheda](https://pa0ehg.com/srtmp.htm), [download](http://www.mike-willis.com/Software/ASTERPathprofile.zip)): 예상 총 손실과 할당량 파일에서 경로의 프로파일, 전자 레인지의 troposcatter에 대해 믿을 수; 비행기 또는 비에서 사기를 치료하지 않습니다. S RTM1 프로파일과 손실을 비교하는 두 번째 참조로 유용합니다. 이 사이트의 나머지는 EME, 전자 레인지, 에어 스트립 (AirScout, SM7LCB 맵) 및 80m의 측정을 포함합니다.

PathProfile by G0MJW (Mike Willis), ASTER 및 SRTM 패키지 및 SiteData (프로그램의 저자에 의해 공급; [](https://www.mike-willis.com/software.html)소프트웨어 페이지). 2018의 Windows 프로그램: ASTER 또는 SRTM에서 1" 또는 3"에 단면도는, 광학에 있는 손실, diffraction, troposcatter (1000 km까지 유효하십시오), 덕트 및 가스, 시간의 비율과 더불어, 노선의, 해안에서 바다 그리고 거리의 fraction: 구조는 P.452/P.1812의 그것입니다, 그러나 도움 (압축한.chm 파일)는 여기에서 읽을 수 없습니다. SiteData는 로케이터를 변환하고 GT OPO30 의 공유를 제공합니다.

가장 유용한 부분은 포함 된 데이터 파일입니다. ITU-R P.453 통계지도 1,5° (121 90°N에서 90°S, 241 의 열은 0 °에서 동쪽으로): 처음 65m의 gradient dN / dz는 1, 10, 50, 90 및 99 %의 시간 동안 초과; 토양 덕트의 존재, 두께 및 강도의 비율; 그 높은 존재, 기본 공유, 두께, 두께, 두께 및 최대 온도에 대한. 연간 값:

| - 연혁 | 땅 파이프라인: % / 두께 / 강도 | 높은 파이프라인: % / 기본 / 두께 / 강도 | dN/dz는 시간의 10%를 초과했습니다 |
| 주 메뉴 |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | −380의 N/km |
| 티로니아 (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | −674의 N/km |
| Trapani, 그리스 |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | −520의 N/km |
| 한국어 |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | −405의 N/km/분 |
| 중독 (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | −399의 N/km/분 |
| 담당자: Mr. Lu |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | −172의 N/km |
| 우딘 |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | −176의 N/km |

우리의 색인은 준비되어 있는 기후상 참고입니다: 50 m의 대신 덕트의 간격과 강렬, 그리고 지역 당 정상에 비교된 anomaly “today. ITU 자료: 비 상업적인 사용을 위해 무료, 공공 버전에서 그들을 포함하여 검사하기 전에. ZO NES01.DEM 파일 (58 MB, 2' 그리드)는 기후 영역 inland/coast/sea를 포함합니다; 형식은 문서화되지 않으며 나는 그것을 해독하지 않았습니다. Willis 방정식 페이지는 물리적 상수, 무료 공간 및 안테나 이득을 포함합니다.

소스의 4 라운드 (20 11 월).

Indermuehle과 스즈키 2026, Murchison (arXiv 2608.12861)의 CSIRO 관측에 대한 GFS 파이프라인의 예측: 그것은 LIVETROPOVHF에 가장 가까운 소스입니다. GFS에서 0.25°까지 N의 단면도 (대략 2 m, 그 후에 isobaric 수준, discarded 배경의 밑에 그들); 3 km의 밑에 dM/dz의 각 최소한도 란을 위해, 도관의 기초 그리고 정상, 통합 M 편향; 그 후에 P.452-16와 P.1812-6 GLO-90 25 길이 98에서 1005 km, 88 MHz에서 2,68 GHz에, 매 시간. 결과: 25 채널에서 미디어 AUC 0,72; P.452 p 시간 비율은 즉시 ΔN을 가진 ITU 모델이 도관 내에서 정의되지 않기 때문에 (초음 p = 20 %) 교정 일정으로 사용되어야합니다. 기술은 3 일 전에 거의 평평하게 남아 있습니다. 162 MHz (AIS) 수신 경로는 67% (0.26 versus 0.16)의 가장 높은 도관 fraction을 가지고 있으며 제어와 49% (28 대 19 M)의 가장 높은 덕트 깊이: 바다에 도관은 거의 항상, 땅의 기지를 결정합니다. GFS는 ERA5 (4.5% 세포 시간의 10.4%)와 비교된 땅에 시간의 반을, 바다에 보고합니다 더 많은 (73% versus 56%). "corridor" 인덱스 (도구를 따라 연속 파이프라인, 40m의 최소 두께 레이어) 결과 향상: 루트를 따라 연속성을 확인하는 것은 올바른 기준입니다.

Longley와 쌀, ESSA ERL 79-ITS 67 (1968): 20 MHz에서 20 GHz (ITM 이후)의 불규칙 지형에 대한 방법, 토양 불규칙성 Δh, 250에서 400까지 Ns, 동등한 반경 8497 km (4/3), 수평선, diffraction, scatter 앞으로 및 기후를 위한 장소에서 가변성. 통계 참고, 즉석이 아닙니다.

ITU-R P.834-2: 반경 curvature, 지구 반경 동등한 k·a, M = N + h/a (h m, 수천 km), exponential 모델; 이미 사용 된 것보다 새로운 것은 없습니다.

AGARD CP-70 (1970), troposferical propagation: 강한 gradients를 가진 1에서 100 m에서 얇은 층은 일반적입니다; 높은 층의 반사 계수는 대략 14 dB의 무작위 분산과 더불어 파장에 비례합니다. 90 MHz의 측정: 9–10 dB의 감소는 구름의 층의 위에 대략 350 m에 집중된 N 점프에, 집중했습니다. 찬 정면의 통행은 층을 삭제합니다: diffraction는 남아 있습니다.

U.S. URSI 보고서,위원회 2 (1960): 60에서 210m (P.453와 일관성있는), 157-160 N / km의 최소 기온.

Buscemi K2OVS, VHF propagation 가이드: 밴드를 위한 반전 그리고 덕트를 위한 전형적인 거리, 높은 정지되는 압력, 새벽에 최대 연결되는 덕트.

Wolinsky-Mancini 및 2024, Canary Islands의 AIS: AIS radiosundations 및 리셉션; 덕트는 따뜻한 달 (알리스의 전환)에서 훨씬 더 빈번합니다.

나이지리아와 말레이시아 기사 (Ekah, Iloke, Mat, Igwe): 온도, 습도 및 측정 된 손실, 약하고 금기 (r −0,09에서 0.27) 사이의 지역 상관 관계); 만 지상 크기가 충분하지 않다는 확인으로 유용합니다.

허브 1984, thesis TU Eindhoven (microonde): 30 GHz의 8.2 km 및 OTS 위성에 11.6 GHz; turbulence, 멀티 path 및 비를위한 불꽃. 전자 레인지 동적 손실은 비와 turbulence, 덕트에 의해 지배된다.

Baars, Paraboloidal 반사체 안테나 (microonde): 전형적인 효율성 0.5–0,65를 가진 parable G = η·(πD/λ)2의 이익을; Ruze에 따라 표면 오류의 손실, ηs = exp(−(4πε/λ)2). 우리는 1296 MHz를 위한 Yagi DL6WU 에 대안으로 parables를 추가하는 것을 도울 것입니다.

이미 분석 또는 가난한 내용: Hirschmann NASA X-731-67-89 (double), Reid's VHF ST 레이더 (바람과 turbulence, unconducted), troposphere에 스크리브 슬라이드 및 일반 노트.

소스의 다섯 번째 라운드 (20 11 월, 저녁).

로저스 1997, 라디오 과학 32(1): Bayesian 높은 덕트의 매개 변수의 추정 (기본 속도, M deficit, 두께) 수평선을 넘어 측정에서, 패러디 방정식 모델; 143, 263 및 375 MHz, 라디오 파 비교에서 캘리포니아의 VOCAR 실험. 신호는 기초에 많을 달려 있고 M 편향도 (0,81에서 143 MHz를 나누기), 간격에서 거의 (-0,17): 간격은 신호에서 얻어질 수 없습니다. 저희: 계산 B (climate thickness)는 수식과 공유 보다는 더 적은을 조사합니다; 그리고 방법은 뒤 사무실을 위한 기초입니다, 그것은 다수 빈도에서 도관을 얻는 입니다.

Moler and Holden 1960, "Tropospheric scatter propagation and atmospheric circulation", J. Res. NBS 64D, pp. 81 및 다음과 같은 (22/11/2026, doi 10.6028/jres.064d.014에 원본을 참조하십시오). 캘리포니아 바다 경로 포인트 로마 (샌디에고): 산타 바바라 190 마일, 포인트 Mugu 144, 산 클레멘 78; 1365 MHz 및 9380 MHz, VHF; 1 월–4 월 1958, 평균 1 분. 26 3 월의 경우: 약 20 dB의 보강, 사이클론 vortex 약점으로 약 3 시간 동안, 바다 바람과 잠수를 시작; 15의 융합은 개혁되고 최대의 22 dB에 대한 신호 방울. 통계 (테이블 1, 500 hPa, 33 케이스): 평균 신호가 밴드 L 및 X에서 공기 상승보다 2.5 dB보다 높습니다. 신호는 상관 계수 없이 변화의 표시 그리고 진폭에 있는 500 hPa에 산출된 다름을 따릅니다. 낮은 층 (약 600m 미만) 흩어져 부분 반사. 항cyclones와 정면은 대우되지 않으며, 위에 언급된 60 dB까지 "확장은 아닙니다: 그들은 초침 요약에서 왔습니다. 저희의 경우: 수직 모션은 좋은 예측기이며, +1.5 dB의 서브사이드 인과 −2 dB의 업힐 공기는 평균 측정 차이 (2.5 dB); 20 dB는 일반적인 값이 아닌 단일 케이스, 전자 레인지입니다.

Banafaa 및 Muqaibel 2025, IEEE Access: 파이프 라인 ( 증발 모델, parabolic 방정식, 레이다 견적, GNSS 및 lidar) 및 실제 측정에 대한 자동 학습 분류; 지원 벡터 기계는 최고의 결과를 제공합니다. 전기로 유용한; 데이터는 페르시아 만의 것입니다.

ITU-R P.834-7 (2015): troposferal 굴절에 대한 권고의 업데이트 된 버전; 이미 사용 된 것보다 새로운 공식 없음.

Abbott, troposferous scatter (GOVPUB- C13)의 NBS 전기: 이미 세 번째 라운드. 덕트에 스크리브를 슬라이드: 일반적인 내용. 기사 ScienceDirect 0021916987900304 열 수 없습니다.

여섯 번째 라운드 (20 11 월, 밤): 콩과 두튼, ARRL 핸드북 2022 및 2023, Balanis.

콩과 Dutton, 라디오 Meteorology (캡. 6), 아직 사용되지 않은 부품. Ns에 분야 감도: 연간 주기에 단위 N 당 0.18 dB; 0.14–0,24 dB/N (아침에 있는 최대), 겨울에 있는 175-200 km의 노선에 0.2~1,1 dB/N까지; 개정 0.8-0.95. Misme로 구성 된 매개 변수: M = a (g \_e − 40 + b · ΔW^n) = 0,5 dB N / km 당, 동등한 gradient와 열 안정성 ΔW (1km의 공기를 들어 올리는 일): 160km의 Sahara에서 유일한 안정성은 1 월과 6 월 사이에 20 dB입니다. 거의 동등한 gradient. 잠재적인 굴절 색인 K (N은 1000 hPa에 adiabatically 보고했습니다): 공기 질량과 anticyclonic subsidence (플라벨과 차선)를 강조합니다.

ARRL 핸드북 2022 (99a ed.), § 제 19.4.3–19.4.4. 250와 2000m 사이의 유용한 변환; 방사선 (세라, 평온한 공기, 건조 토양) seldom 리드 진실; 느린 항 사이클론은 500과 3000m 사이의 침략에 가장 광대 한 덕트를 제공합니다, 저녁과 아침; 북부 Hemisphere에서 최고의 노선은 높은 센터의 남쪽입니다. 뜨거운 정면: 좋은 조건은 100 km 앞서갑니다; 찬 정면:, 평행한 정면 뒤에 강한 그러나 불안정한 반전. 퐁 (Alps도)와 지구 바람 (지상에 차가운 공기, 200 ~ 300m에 뜨거운; 해안을 따라 800km 이상 뻗어). Scirocco: 냉각기 해양 공기에 Sahara에서 뜨거운 건조 공기, 동부 지중해에서 지브롤터까지 매우 강한 전환, 전자 레인지에서 1500km 이상의 레코드의 probable 원인. Troposcatter: 이론적인 한계 대략 800 km, 최대 전형적인 반. 100 번째 ed. (2023)는 더 짧은 텍스트가 있습니다. 해돋이와 일몰에서 +20 dB 및 +20-50%의 흐름율; 큰 호수에 1200 마일, 전형적인 400–600; 추천 라디오 모바일 (ITM 모델) Hepburn 적용 및지도.

Balanis, 안테나 이론 (4a ed.), § 10.3.3: Viezbicke 테이블 (NBS TN 688, 1976) 400 MHz에 최적화 된 Yagi에 대한: 붐 0.4 / 0.8 / 1.2 / 2.2 / 3.2 / 4.2 λ → 7,1 / 9,2 / 10,2 / 12,25 / 13,4 / 14,2 dBd. 우리의 DL6WU 공식 (8.6 + 8 log10 붐 / λ)은 0.8 ~ 1.7 dB를 제공하고 4.2 λ와 λ 사이의 dB를 제공합니다. 그리고 DL6WU 테이블에 대한 견적을 반환합니다. 원래 (9.4 + 7.8 log10 붐 / λ, 22 11 월)의 정형식으로, 카드는 1.2와 4.2 λ 사이 0.2 dB 미만 (12.1 dBd에서 2.2 λ versus 12.25) 남아 0.6 dB에서 0.8 λ.

일곱 번째 라운드 (11 월 21,): QST, QEX, W3SZ, GitHub 저장소.

QST 2000–2025 (14 개, 텍스트 인식이있는 10 개). 섹션에서 만 보고서 세계 50 MHz 이상, 모델을 검증하는 경우 유용합니다: 9 월 5 - 9, 2000, 미국 동부 절반 이상에 144 MHz에서 실시; 14-15 8 월 2022, 오프닝 Manitoba–Kansas/Missouri 뜨거운 날에 고압에서, 144에서 1296 MHz 거의 모든 FT8 에서, 캐나다 기록 10 GHz의 1228.56 km ( VE4MA – N0OY), 1KQ, 1K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4K, 4 QST 11월 2022일 Hepburn 지도를 추천합니다. tropo 예측에 기술 기사 없음. Q65

QEX 및 K1JT, Franke 및 Taylor (QEX 2020) FT4/FT8 모드: FT8 디코딩 임계값 −20,8 dB는 증발없이 채널에 2500 Hz에서, −18,6 dB 중간 경도에서 증발; FT4 −17,5 dB. QEX 2023 및 2026의 다른 숫자는 tropo (삼 번호 2023는 읽지 않은 검사)를 대우하지 않습니다. W3SZ 2019: 느린 형태 문턱 ( JT4A −23, JT9A −27, JT65A −25, QRA64A −26, FT8 −21 dB) 및 감도와 주파수 교대 사이 선택 기준.

GitHub. TropoLink (LukiBox, Apache-2.0): P.617-5 ( N0 및 ΔN 포함), NBS TN101 및 ITM 플랜지가 붙은 troposcatter 노선의 설계 및 불확실성으로 표시된 차이점; P.617-5: L = F + 22 로그 f + 35 로그 θ + 17 로그 d + Lc, F = 0.18 · N0 ·e^ (−hs/7,35) − 0.23; L = ΔN (0G). andi-f/troposcatter ( DL2FZN 2013년, VHF/UHF 핸드북 RSGB에서): Lts = 24,44 + 4,283 ln f + 0.06656 d 위에 자유로운 공간, PROPBER (0,0674 dB/km)의 사면. QRadioPredict: S RTM3, 토양 사용 Corins 및 NEC 다이어그램과 ITM 및 ITWOM 3.0. 관련 사항: Tropos ( 날씨 앱), 오페라 - 아트 / 트로포 및 트로포시 (InSAR에 대한 Troposferic 지연), tgbot \_troposcatter (밀리트 역), sdr-troposcatter-gui (제어판), Troposcatter-Propagation-Delay ( ERA5 지연), Radio-Communication-Project (전술 시뮬레이터), PyLayLers (채널).

8h 랩 (21 11 월): GitHub의 NTIA 및 ITU 모델. NTIA/itm (Longley-Rice, ITS): 토양 단면도, 0.5-3000 m 맨끝을 가진 20 MHz~20 GHz의 자유로운 공간 손실, diffraction 및 troposcatter, 지상 반응성 N0 (사양 수준, 250-400), 7개의 기후, 토양 일정한 및 각종 위치 형태 1 전망, 2 diffraction, 3 troposcatter;.NET Framework 4.7.2 울안을 가진 C++. 그것은 덕트를 모델링하지 않습니다: 통계적 가변성을 가진 참고 "정상적인 대기" 역할을합니다. NTIA/p528와 eeveetza/p528 (MATLAB): 공기 기름 연결을 위한 ITU-R P.528-5, 100 MHz–30 GHz, 1.5–20 000 m 맨끝, p 1–99%;.NET 4.8 케이싱. 가능한 사용: 공기 분산 균형 (2개의 지상 노선– 항공기 플러스 레이더 단면도). eeveetza/p2001 (MATLAB): ITU-R P.2001-6, diffraction, troposcatter, 비정상적인 propagation (콘텐츠 및 층, Lba) 및 E sporadic의 비율, 시간 0-100 %; ITU 디지털 맵 (DN\_Median, DN\_SupSlope, DN\_SubSlope, dndz\_01, TropoClim, h0, Ewsurfable. P.452 대신 ITU 비교로 사용할 수 있으며, CSIRO 연구로 지도 대신 GFS ΔN과 함께 사용할 수 있습니다. 지도는 P.453 당 사용자에 의해 다운로드되어야합니다. 유효성 1.3.0 밖으로 593 QSO (29/12/2021–2/1/2022): event/comparison 별거 0,79 보정 없이, 0,91 Misme와 subsidence; GFS는 144 MHz에 바다에 도관을 볼 수 없습니다 (보정 없이 guadagno 15 dB).

Ninth Round (구월 21,): IU3QEZ, NTIA, PyWaveProp, ACE High, validation 1.3.1. IU3QEZ (S. Fabris), 발표 "VVIP의 수평선 이상" (GEKO Magazine n. 69, September 2026): 일반적인 볼륨 d2/(8·R\_eff) R\_eff ≈ 8500 km (1,3 km d 300 km, 3,7 km에서 500 km (1,7 km)의 수평선, 69, 9 월 1,8 dB의 수평선, dB에 대 한 경향; 4 dB에 대 한 경향; 10 dB는 대략 100 km입니다; 3×5 성분, 120 QSO, 최대 788 km (Alpe Adria 2026)를 가진 역 IQ3QC 5 W. NBS 63D, System Loss), Bray 1961, ARRL Antenna Book 2023 fig. 4.29, Rice-Longley-Norton-Barsis NBS TN 101, Bean and Thayer 1959, Barsis and Kirby 1961 (댄스에 대한 불평), Deygout 1966, P.1812, AirScout X3, P.1812, AirScout X3, P.I.I.P.I.I.P.I.I.P.I.I.P.I.I.P.I.I.P.I.I.P.I.I.I.P.I.I.I.I.I.I.I.P.I.I.I.I.I.I.I.I.I.I.I.P.I.I.I.I.I.I.I.I.I.I.I.P.I.I.I.I.I.I.I.I.I.I.I.I.I.I.I.I.I.I.I.I.I. PyWaveProp (Python): 패레 대각선, 여러 가장자리 확산, PETOOL 및 RAM 인클로저, troposferous 및 높은 덕트, 불규칙한 토양: GFS M (z) 프로파일과 필드 계산을위한 가능한 기본, 검사됩니다. APRS PropView (아파치 2.0): APRS 패키지, 시간 통계 및 개방 경보의 VHF 전파지도; 검증을위한 144 MHz 데이터의 가능한 소스. 이탈리아의 NATO troposcatter 그물 (fortifications.net): 900 MHz, 10 kW, 20 m 패리스; Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Vulture, Monte Lauro를 포함한 역; P.617의 역사적인 테스트 케이스. 관련이 없습니다: Gupta 2021 ( stratospheric 수송), 법 2023 (Arctic ozone), 돌고래 #590 및 오페라 - 아트 / 라포 (InSAR 지연), tropo.ino (Arduino 가스 센서), tropo-webapi (telefonia), cloudtools/troposphere (AWS). 유효성 검사 564 QSO tropo에 1.3.1: AUC 0,79 (모형 1.2), 0,80 (+ 연약한 덕트), 0,90 (+ misme 및 subsidence), 0,90 (+ persistence); 오프닝 노선의 46%에 도관 색깔 (≥ 16 dB) 및 비교의 0%. DL2ALF

같은 라운드에 추가. mikelytaev/wave-propagation (MIT, Python 3): Padé와 Numerov, 불규칙한 토양, tropospherical 덕트의 대략적인 방정식; NOAA GFS에서 수정된 refractive M(quota, lat, lon)을 충전하고 Monin-Obukhov의 증발 도관을 설정; 경로에 손실을 반환하는 문서화 된 기능. 그는 GFS M 프로파일과 미래 필드 계산을위한 최고의 후보입니다 (외부 서비스 또는 번역을 제공 할 것입니다). RP-Sim (AGPL-3.0, 자바 스크립트): 라디오 네트워크 (Hata, He, Ericsson, two rays), tropo에 대한 일반 모델. RIMFading (GPL-3.0, OMNeT++): 네트워크 시뮬레이션, Weibull 배포, tropo의 흐름 불규칙성. PyLayers: 실내 라디오 채널, 이미 제외. qradiopredict (kantooon, GPL v2): S RTM3 및 코린과 ITM 및 ITWOM v3.0, 이미 QRadioPredict로 7 라운드에서.

Tenth Round (11 월 21,): 안테나 책, Norton, Barsis, 새로운 데이터 소스. ARRL 안테나 책 (23a ed.), 모자. 4 ( K9LA): VHF 범위를 위한 Bray 방법 (QST 11월 1961): 99% (300 마일, 144 MHz에 매끄러운 지구 손실: 214 dB), 50%와 99% (fig에 곡선. 4.11): 195 dB 가치 100 마일에서 99% 그리고 50%에 250 마일; 100 마일 이상 evanescence ±7 dB (-3,5 dB에 50 마일), SSB 3 dB는 CW, +4 dB 지상 반사, +3 dB에 대 한 dB; 2 / 50 마일에 대 한 dB; dB에 대 한 dB; /100 m h 안테나; dB에 대 한. +20 dB 및 +20-50% 교류까지 일출과 일몰; 1200 마일, 전형적인 400-600까지 중대한 호수에 젖은 신선한 공기에 뜨겁고 건조한 공기; 큰 물에 의해 영향을 받는 지구는 가장 호의를 베풉니다; 강한 침투의 가열을 가진 늦은 겨울. 우리의 troposcatter의 검증: 483 km에 P.452에 50% 및 144 MHz는 Bray의 214 dB로 일관되게 하는 대략 204 dB를, 99% 제공합니다. Norton 1959년 (J. Res. NBS 63D는, NIST에 해방합니다): 기본적인 손실, 체계 및 경로 안테나 이익; 참고 대기권 Ns = 301; Ns (250 → 4878 마일, 301 → 5280, 400 → 6997)에서 동등한 반경; 30 피트 안테나가있는 L (99) 테이블 (100 MHz: 163 dB에서 100 마일, 208 dB에서 500 마일; 1000 MHz: 138 및 188 dB 28 피트 패러블; 장기 가변성 V (p, θ) 각 거리 (fig. 13)에 따라. 쌀, Longley 및 Norton, NBS TN 15 (1959), NIST 무료. Barsis와 Kirby, NBS 보고서 6751 (1961): Pikes Peak (4292 m) 가장자리, 100 및 751 MHz와 콜로라도에서 223km; 둥근 가장자리 (대략 11.5 km 반경); 장기 evanescence 11,7–14,3 dB versus 39–42 dB 같은 길이의 흩어져; 표시된 일 주기; 4-ray 모델 제공 4–5 랍스터 하지만 하나의 단지 (최대 21 비스듬한 연결). ARRL VHF 디지털 핸드북: 포장, APRS, D-STAR, WSJT (스캐터 방법 및 EME), tropo에 부품 없음. 새로운 입증 또는 검색 된 데이터 소스: ECMWF 오픈 데이터 (IFS 0.25°, 레벨 1000, 925, 850, 700 hPa..., 975, 950, 900 및 800없이; G RIB2 CCSDS 압축, 그 독자는 디코딩하지 않습니다; 이미 Open-Meteo를 통해 사용 가능); DWD ICON-EU 오픈 데이터 (G RIB2 압축 bz2, 가변 및 레벨에 대한 파일: 많은 요청); MSC 데이터 마트와 GeoMet에 캐나다 GDPS (G)는 2021 년 2 월 31 일까지 유효합니다.

11월 21일(금): Longley-Rice, GEKO 69. 콩과 Thayer, " 대기 라디오 퇴직 지수의 모델", Proc. IRE 할 수있다 1959 (pp. 740 등 seq.): 모형 4/3에는 첫번째 km에 있는 적당한 경사가 있습니다 그러나 너무 높이 떨어지십시오; 반경의 곡률의 30%는 첫번째 km에 있는 첫번째 100 m 그리고 60%에서 발생합니다; 평균 관계 사이 Ns는 첫번째 km에서 감소합니다: −ΔN = 7,32·e^ (0,005577·Ns) (Ns = 313 → ΔN에 관하여 −42). Bean and Thayer, "C.R.P.L. Exponential Reference Atmosphere", J. Res. NBS 63D n. 3 (1959): N(h) = Ns·e^(−c·h)와 같은 보고서를 통해 Ns에 의해 고정; 평균 Ns USA 313; 빔 트래킹 테이블 (총 곡선 15.2mrad for exponential model versus 30.3 mrad with Earth 4/3). MATLAB refractionexp: C = 0.143859 km-1 for Ns = 313 (약 6.95 km), 약 1km 및 10mrad에 대한 유효한. 저희의 사용: 비교의 "정상적인"프로필은 M = 315 + 0.118·z 조정 대신 지역 Ns와 CRPL을 측정하고, 첫번째 km에 있는 감소의 anomaly는 −7,32·e^(0,005577·Ns) 대신 −40 조정에 비교되어야 합니다. NTIA 보고서 82-100 (Hufford, Longley, Kissick 1982), 지역 모드의 ITM 가이드: 조미료 및 Ns 제안 (기초 360, 중형 대륙 320, 중형 해양 370, 사막 280, 극성 대륙 301, 땅에 해병 320, 해병에 부드럽게 해병 350); Ns = N0 ·e^ (−hs/9,46 km); 당신은 Ns의 최소 사용 Ns의 Ns의 최소 사용 = Ns 497 및 subfraction에 영향을 미칩니다. 쌀, Longley 및 Norton, NBS TN 15 (1959), 부품 I: 토지 및 tropospheric 파, TN 101의 기초에 의해 시간 동안 배포의 예측 공식. SoftWright, Wikipedia, Altair WinProp 및 MATLAB rfprop. LongleyRice: 동일한 정보 (버전 1.2.2, 20 MHz–20 GHz 또는 40 GHz, 1–2000 km, Ns 301, 7 기후, 시간의 가변성, 장소 및 상황); 한 가지 모양은 바다 기후의 통계적 효과 ("시간의 10 %에 대한 높은 필드"). GEKO 매거진 n. 69 ( IU3QEZ): 1000m, 260km에서 130km의 방사성 4 / 3는 두 개의 역을 추가합니다. 1 ~ 3km와 10km 사이의 일반적인 볼륨이있는 troposcatter; 10km의 항공기에서 Scatter는 초 또는 몇 분 동안 +10 ~ 20dB; 측면 당 2 × 10 요소가있는 이론 과정: SSB QRP 약 440km, SSB 50 W 약 510km, CW에 대한 SSB 50 W는 약 600km, WW에서 500km에 이르지 않습니다.

## 12. 라운드 테이블: 모델을 검증하거나 수정하는 소스 (11 월 21)

모든 소스의 요약, 사용 명령. "State"는 소스가 이미 프로그램 (1.3.1)에 있는지, 테스트 또는 수행 여부.

### 유효한 데이터 (실제로 일어난 일)

| 이름 * | 이름 * | 제품 정보 | 주요 특징 |
| MMMonVHF 보고서 저자 | 144 MHz에 593 QSO, 29/12/2021–2/1/2022, 시간 및 거주자 | 비교에 대한 이벤트 검증 (+14 일) | 에서 사용: AUC 0,79 (1.2) → 0,90 (1.3.1) |
| 세계 50 MHz 이상 | 문서화 (14–15/8/2022 Manitoba–Kansas, 144–10 GHz) | 역사 사례 | 역사적인 경우로 사용; 유효성 QSO에 추가 |
| wspr.live (위) | 완전한 아카이브, 2m 및 70cm, SQL | 많은 이벤트와 많은 "no"(시간 없음) | 시도, 필터링되어야한다 (점심 16~20천km) |
| APRS 개요 / APRS-IS | 역 펠트, 거리, 시간 | 만 유효성 144 MHz의 | 으로 |
| 반전 비콘 (archive) | 매일 | 주파수 영역 | 지금 신청 |
| I GRA2 radiosondaggi (Brindisi 16320, Mare 16245의 연습, Trapani 16429, 유딘, 밀라노) | 측정된 프로필 00 및 12 UTC | GFS 프로파일 제어 및 inversions | 파일 (Brindisi 46 MB)를 찾아 사용 |
| 바시스와 키비 1961, 바시스 1963 | evanescence는 산악 장애물과 경로에 측정 | 범주 "monte"(Vesuvio, Faito) 유효 | 으로 |
| 노턴 1959, NBS TN 15, TN 101 | L(99) 및 테이블 미디어 손실 | Scatter 통제 | 부분적으로 (일부로 300 마일 참조) |

### 수정은 소스에 의해 제안

| - 연혁 | 이름 * | 유효성에 측정된 효과 | 주요 특징 |
| Misme 안정성 (주) | 콩과 Dutton 모자. 6개 | AUC 0,79 → 0,90과 함께 | 1.3.1에서 ( 도관 없이 16 dB) |
| 수직 동작에서 | Moler와 Holden 1960 (22/11에 원본에서 읽으십시오: +2.5 공기 사이 dB 평균 및 공기 상승, 1.4와 9,4 바다에 GHz) | 현재 위치 | 에서 1.3.1 (+1.5 / −2 dB) |
| 소프트 파이프 (λ 최대 3·λmax) | Kerr, Hepburn, VHF/UHF DX 책, 시 2019년 | 혼자 +1%의 AUC; 이벤트의 경로의 46%에 덕트의 색상을 가져와 비교의 0% | :: |
| 지속 24 ₢ 킹 | VHF/UHF DX 책, Biancomano 및 Leyshon | AUC, 더 많은 촬영 및 더 거짓 경보 | 1.3.1에서 |
| 일반 프로필 CRPL 로컬 Ns (−ΔN = 7,32·e^(0,005577·Ns)) 대신 40 N/km 고정 | 콩과 Thayer 1959, NBS Monograph 4 | 유효성 검사 | 1.3.2에서 대안으로, 결함으로 돌리기 |
| 100 (Bean 및 Dutton, Layer 1000-850) 대신 Super-refraction 문턱 79 N/km (ITU) | P.453, Lopez 2008, DX 책 | 유효성 검사 | in 1.3.2: 공격적인 esteem alongside prudent (지도 사용 prudent) |
| Climatological 간격 (calcle B) 적층보다 덜 중요 | 로저스 1997 | 시도: 이득을 위해 A 사용 | 으로 |
| GFS 프로파일의 패기식 | PETOOL, 파 추진 (Lytaev), Levy (책은 읽기, 만 미리보기), Lindquist | 수평선에 필드를 가진 첫 번째 버전은 수치 소음 (AUC 0.12–0,28)에 의해 지배되었습니다. (도메인 8km, 빔 6°); 수정 후 AUC 0.54–0,66 | 두 번째 계산으로는 예측을 결정하지 않습니다. |
| 증발: 100 마일 이상 ±7 dB, 마진 10-20 dB에 90-99% | Bray 1961, 안테나 책, GEKO 69 | 마진 상태의 방법의 문턱 | 사용: 기간 및 임계값 FT8 −18,6 dB에 대한 확률 |
| Bray의 일반적인 볼륨 및 동등한 거리 | IU3QEZ, 블레이 1961 | - 기타 | :: |
| Troposcatter P.617 및 RSGB는 uncertainty로 | 트로포 링크, DL2FZN | - 기타 | 1.3.0에서 |

### 모델에 남아있는 것

GFS 수준이 (현재 데이터 소스의 요약); 인근 산 (Vesuvio, Faito)의 반사 및 확산: 계산을위한 제안 "Monte" ITM/Longley-Rice는 conduits 없이 통계적인 참고 “정상적인 일” (Ns 301, climi), 남아 있습니다. 저자의 결정에 의해 해제: AirScout, P.528), E sporadic (P.2001) 및 50 MHz (통역 EIRP는 E sporadic와 함께 몇 가지 및 혼란).

### 작업 주문에 대한 제안

1. 확장 유효성: 필터링된 WSPR (지점 없이 일 시간) 및 QST 2022; 432 MHz의 반복.
2. 정상적인 CRPL 프로파일을 시도하고 ITU 임계 값 79 N / km, 한 번에, AUC를 제기하는 것을 유지.
3. 의 비교 GFS 프로필과 I GRA2 라디오 파의 Brindisi와 QSO의 일에서 바다의 연습.
4. 수치 보정 후 패기식 방정식을 회복합니다.

### 11월 21일(버전 1.3.2)의 결정

- 채택: prudent 추정 (Bean와 Dutton, 100 N/km) 및 공격적인 (ITU, 79 N/km) 플랜지; 덕트는 2500 m (첫 번째 1500 m, 유효성 후 제기); 높은 확장 압력과 약한 바람; P.452의 μ1를 가진 바다 무게; 부분 도관에 관하여 도관과 가진 역의 공유; ridges ( IT9GSF)의 위 경계 층.
- 대안으로 채택, 결점에 의해 꺼진: 국부적으로 Ns를 가진 정상적인 CRPL 단면도.
- 정확한 기하학적 방정식 간체: 0.12 ~ 0.54 (30m 안테나) 및 0.28 ~ 0.63 (300m)의 AUC.
- 랜드: Terrain 타일«skadi» 대신 Copernicus DEM GLO-30, 해안 근처는 바다에서 80-100m의 유령 구호가 있습니다 ( IC8TEM 의 서명, 카프리 포인트로 확인 된 포인트); Skadi는 예비로 남아.
- 그들의 효력을 가진 지도에 날씨 표시: ↓ 호의를 베푸는 subsidence (모어와 붙드는 1960년, 공기가 내리는 공기 사이 2.5 dB에 관하여; +1.5 dB), ↑ 공기 강습 (깊은 정착물; −2 dB), S 파열 호의를 베푸는 clue (ARRL Handbook § 19.4의 지중해에 강한 inversions; 개정 없음). 두 배 상징, 녹색/빨강/오렌지 색깔 및 지도에 만화 책.
- ITM 1.2.2 (NTIA/itm, 공중 도메인)를 채택하여 C#로 번역했습니다: P.452 옆에 있는 정상적인 대기권의 계산에 따라 서신의 앞에 끝나는 도관에서 출구 후에 마지막 뻗기. GPL v2의 밑에 때문에 qradiopredict 부호를 출력했습니다.

인덱스에 대한 유용한 내용 없이: *Communications Quarterly*, 가을 1991 (tropo 기사 없음; Scatter에 Atkins의 기사는 겨울 1991 문제입니다).

- 매뉴얼의 원칙으로 채택 (11 월 21,): ' 전파는 일반화 될 수 없습니다, 그러나 로컬 형형과 관련하여 연구' ( DF5AI, IK3XTV 에 의해보고 된 는 예측 설명서 (§ 1) 및 의회에서 인용 2 미터의 조사.
- 채택 (11 월 21 일 이후 1.3.3) IK3XTV 의 오후 transalpine 오프닝의 해석: "렌즈와 같은 관동 세포", 그러나 troposcatter는 일반적인 볼륨의 공유에 도달 할 때 층의 상단에 turbulence에 의해 강화 된 troposcatter (시각, § 8 ter). 1.3.4에서 그것은 가장 3 dB에서 troposcatter의 프로비저닝 보강으로 포인트 포인트 포인트 계산을 입력합니다. 동일한 기사에 의해 덧붙였다: Alps (수동 매체가 증폭되지 않는 한 신호의 "auto-amplification"은 diffraction 및 troposcatter와 설명 할 수 있습니다. 선언 된 전력은 데이터로 사용되지 않습니다), "betters diffraction"(눈에 안정된 층이 더 적은 증발).

- Kerr (1951) 책에 검토 (구월 22, 저자에 의해 제공 PDF): § 1.5, eq. 17 및 18 및 테이블 1.1. eq의 숫자. 스캔에서 18은 읽을 수 있지만, 테이블 1.1은 확인: 20 피트 → 1cm, 80 피트 → 10cm, 400 피트 → 100cm, 2000 피트 → 1000cm, 1,25, 10, 112 및 λmax의 1252cm = 0.014 · d^1.5 (cm, d 서; 라운드 테이블, '정보 값'. Kerr의 hypotheses: 발 당 4·10−8의 변경한 색인 하락 안쪽에, 낮은 덕트의 전형적인 가치; 약한 gradients로 더 중대한 간격을 필요로 합니다. Kerr는 또한 λmax가 그물 커트가 아닙니다 경고합니다: 때때로 파장은 아직도 덕트에 의해 강하게 영향을 미칠 수 있습니다.

- DL6WU 는 원래에 검토 (구월 22, 저자에 의해 제공 VHF 통신 3/1982의 PDF): G. 호흐, «Extremely Long Yagi 안테나», pp. 130 이상. Fig. 1: 7,7 dBd에서 표본 안테나 EIA와 더불어 독일 포스트의 직업적인 다각형에 있는 432와 1296 MHz에 측정된 극에 이익, 추정된 정확도 0.5 dB; 붐의 길이를 두 배를 위한 2,35 dB, 대략 20 λ까지 포화 없음. 이 프로그램은 8.6 + 8.0.log10(L/λ)를 사용했습니다. dBd, i.e. 약 0.8 dB 미만 1–2 λ; 이제는 9,4 + 7.8·log10(L/λ) dBd (그라디언트는 명시된 것, 1 λ 값은 차트에 읽습니다, ± 0.3 dB). 그물에 순환된 공식 10·log10 (7,8·L/λ): 측정된 2,35에 대하여 doubling를 위한 3 dB를, 줍니다.
- 서퍼 앤 고든 (1950): "이 주 인용 클래식"에 의해 확인 된 참조 (현재 내용, 8 2 월 1982, H. G. Booker의 논평): Proc. IRE 38, pp. 401-412. 댓글은 매뉴얼에서 사용 된 두 가지 점을 확인합니다: Rayleigh 배포와 수평선을 통과 한 필드, 불규칙성 (비 가스)의 폭발 기능. 원본 기사는 읽지 않았습니다.

## 13. IK3XTV 기사에서

Flavio Egano IK3XTV (Radio Rivista: "2m에 대한 조사"및 "Propagation VHF - 자궁 경관 세포 및 자궁 경관 파도")는 도관 모델이 커버하지 않는 두 가지 것들을 추가합니다: convection로 인해 오후의 transalpine 오프닝 및 이탈리아에서 형성 될 때 실제 규칙. 그들은 역의 관측, 측정하지 않습니다: 그들은 반점을 확인하기 위해 저하로 치료해야합니다.

| 저자의 관측 | 인덱스의 성분이 되는 방법 |
| 덕트는 주로 100와 1000 m 사이 형성, 드물게 더 높은; 넓은 지역에 고압, 좋은 팽창 및 바람의 부재를 봉사 | 1500 m 미만의 덕트에 대한 검색; 높은 확장 압력과 낮은 층에 약한 바람의 보너스 |
| 따뜻한 달에서 Veneto와 Puglia 사이의 Adriatic에 반복 실시, 700-800 km, 때로는 그리스까지; 바다 노선은 좋아하는 | 바다 노선의 분수에 더 큰 무게 (ITU-R P.452) |
| 대응은 도관의 가까이에 있어야 합니다; 높은 안테나와 몇몇 미터로 신호는 들어가지 않습니다 | 두 역의 공유와 도관의 기초의 공유를 비교하십시오 (QTH의 고도를 봉사할 것입니다) |
| 동일한 시간, 동일한 도관: 50 MHz에서 144 그리고 하나에 있는 Gargano 역에서 듣고 | 검사: Hepburn 테이블에 따라 50 MHz는 300 m 지도 이상 요구했습니다, 그래서 사건은 매우 자주 파이프라인을 나타냅니다 |
| Connections Veneto – 비주얼없이 Alps를 넘어, 거의 항상 오후와 고압; 약간 분 후에 사라지고 다시 나타나는 불안정한 신호 | "convective" 인덱스는 도관에서 분리, 산 노선 |
| 자궁 세포는 지상에서 태어 났고 약 1km에 올라 3 시간마다 올라갑니다. 15에서 그들은 약 3km에 도달하며 Alps를 과도하게합니다. | 한계 층 높이 (바운드 \_layer\_height of Open-Meteo GFS) 를 사용하여 경로: 능선의 고도를 초과 할 때 점수 |
| Alps는 gravitational 파도를 생성, convective 동의와 함께, refractive 색인의 불규칙성을 만듭니다 | hypotheses: 금파의 신호로 700 hPa에 사슬에 강한 바람 수직; 마지막 시도하기 위하여 |
| 매우 높은 inversion (특별한 경우 최대 8km)는 약 800km를 제공하지만 거의 300km 이상 연습 | inversion의 공유는 범위를 확장합니다: 대기 거리를 추정하는 유용한 데이터 |
| 여름 지구 반경 약 16000 km (실제 2.5 배)에 해당되며 약 60 dB 미만의 300 km 손실 | k50 = 157/(157 − ΔN): k = 2,5는 94 N/km에 관하여 ΔN에 대응합니다, super-refraction입니다 |
| 20-30 ° 범위의 북쪽과 equator의 남쪽에 불과 3000 km 이상, 하일리 셀에 연결; 아프리카에서 8 월 피크 – 유럽 | 유럽 외부: 피드백없이 대서양에 높은 지수를 제공하지 않는 유용한, 그리고 카나리아 노선에 대한 – 영국 |

같은 기사의 다른 유용한 자료:

- 2 h1와 h2 미터 사이 광학 범위 (km에서 d): d = 4,1 · (√h1 + √h2), 정상 회담을 위한 대략 20%까지.
- Evanescence: 느린 과 얕은 inversion; 강하고 흩어져 및 convective 세포로 빠른. 이 프로그램은 한 일 동안 광고를 분류 할 수 있습니다. SNR 시간이 지남에 따라 보고서가있을 것입니다.
- 144 MHz의 sporadic E에 대한 저자는 DF5AI 및 Davis 및 Johnson (Nature 435, 2005)의 작업이 임시 및 Es 사이의 결합에 대해보고했지만, 내일 별도의 지표는 번개 데이터를 사용할 수 있습니다.
- 두 배 점프 Es에 있는 지상 반사: 바다 K = 81와 5 S/m, countryside 및 나무 K = 13와 0.006 S/m의 도시 지역 K = 3와 0.001 S/m.

## 14. LIVETROPOVHF 인덱스 hypotheses

제안을 시작으로 논의 될 것입니다. 인덱스 0–10 같은 규모에서 Hepburn, 그리드의 각 지점을 계산하고 GFS (다음 ECMWF 및 ICON 비교)에서 3 시간마다 3 시간, 경로의 두 번째 숫자는 선택된 밴드에 유용한 덕트로 전달하는 방법을 말한다. 아래 무게는 내 가정이며, 반점과 교정 할 수 있습니다.

Bean 및 Dutton 및 Kerr 후 검토: 프로파일 임계 값은 테이블 6.5 및 측정에서 스케일로 제공되므로 refractive 층은 정상 프로파일에 10-25 dB의 가치가 있습니다. 인덱스는 dB에서 첫 번째를 계산하고 0–10에 걸립니다. 그 가위의 값은 반점으로 측정되어야합니다.

포인트로 계산, 단계별

1. T 출력, 상대 습도, 지위 높이 및 수직 속도 1000, 975, 950, 925, 900, 850, 800, 700, 600 및 500 hPa, 플러스 T 및 2 m 습도, 지상 압력, 층 높이 및 바람 10 m.
2. 지상의 밑에 수준을 삭제하십시오; 각 수준 (ITU-R P.453)에 N와 M을 계산하십시오.
3. 계산 −dN/dh 콩과 두튼의 4개의 층 (1000–850, 850–700, 700–600, 600–500 hPa) 및 테이블의 임계값과 정상 또는 super-refractive로 도표; 첫번째 층 안쪽에 또한 강한 기온변화 및 그것의 몫을 찾아내기 위하여 중간 수준을 이용합니다.
4. dM/dh < 0 나는 도관, 간격 d 및 deficit ΔM의 기초 그리고 정상을 찾아냅니다.
5. Kerr의 공식에서 밴드의 최소 두께와 비교 (144 MHz의 약 180 m, Hepburn 테이블에서 432 MHz의 경우 89 m); 임계값 아래 conduit 여전히 카운트, 하지만 덜.
6. 아래 표로 dB의 이익을 자극합니다 (당신은 층의 합이 아닌, 층의 정상을 가지고, 아노마의 용어) 그것을 변환: 인덱스 = dB / 2.5, 0과 10 사이, 그래서 25 dB 가치가 10. 부정적인 값 (sub-refraction)은 0으로 표시되지만 경로 데이터에 남아 있습니다.

| 제일 층 상황 | 이름 * | 예상 수입 |
| 정상 ( 박판으로 만들어진) | 탭 범위에서 −dN/dh. 6.5 (예: 1000 및 850 hPa 사이 20-60 N/km) | 0 댓글 |
| 정상의 위 Slightly | 정상적인 밴드의 상한 한계 사이 및 최고 반복 문턱 | 0에서 10 dB, 비율에서 |
| 초경량 | 층 임계 값 (100, 80, 70, 50 N / km 상승)을 초과하지만 157 N / km 이하 | 10에서 15까지 dB의 비율에서 157까지 N/km |
| Too 얇은 파이프라인 | dM/dh < 0=""> Kerr 임계 값 | 15 분 |
| 밴드를 위한 유용한 연결관 | dM/dh < 0, kerr 임계 값 위 두께 | 20 dB, +1 dB 각 5 M 단위, 최대 25 |
| 공지사항 | −dN/dh < 더 낮은 층에서 0 | −10 dB의 |
| 다양한 배경 (당신은 추가) | 모델 Ns minus ITU 지도 climatological 그 달 동안의 Ns | 단위 N 당 0.2 dB, −5와 +5 dB 사이 |

경로 (베트웨덴 두 위치자)에 의해 계산: 이미 경로에 인덱스 프로그램에 대한 프로그램을하고 유용한 덕트와 기본의 평균 공유와 포인트의 분수를 계산하여 10 km마다 최대 원을 샘플합니다. CSIRO 연구는 파이프 라인의 경로의 분수에 유용합니다 (AIS 경로에 162 MHz는 제어 케이스보다 67% 높았습니다).

( LA0BY 와 W3SZ 에서) 노선 당 계산을 추가: (1) 도관의 기초와 정상과 비교해, 높은 도관은 단지 입력하는 사람들을 봉사하기 때문에; (2) Yeh 방법과 troposcatter “floor”, 지상 단면도에서 가지고 가는 시작 구석과 더불어, dB에 있는 trope 이익은 그 손실에서 제거되고 결과는 역이 극복하기 위하여 관리하는 손실과 비교됩니다 ( KK6MC 테이블). 따라서 색인은 또한 도달 가능한 거리의 견적이 됩니다.

한 번에 시도하는 보정 (빈과 도톤과 LA0BY, IK3XTV, G4MCU 는 매뉴얼에서 이미), 그들은 반점과 비교를 개선하는 경우에만 유지:

- 믹싱 (매일 관측으로 저자에 따라 중요한 중요): 지구의 낮부터 낮, 맑은 하늘과 오후 사이에, 또는 몇 m / s의 배경에 바람과 함께, 땅에 덕트가 사라집니다; 그들의 기여는 감소해야합니다. 바다에.
- 하루의 시간 (일별 관측으로 저자에 따라 중요한 중요): 일몰과 해돋이 후 irraggiamento 덕트 발생; 3 시간 예측 제어입니다.
- 수직 운동: 850-700에 일어나는 공기 hPa는 층 (negative 보정)을 섞는다, anticyclone의 공기가 그들을 강화 (긍정).
- 압력 동향: 마지막 시간, 더 높은 안정되어 있는 신호에 있는 경로에 언덕 압력; 감소에서, 더 낮은 신호 (bastano 2 hPa).
- 동의어: 1025 hPa의 지상에 850 hPa에 온도, 뜨겁고 건조한 공기 흐르는 찬 바다에, 바다에 노선에 온도의 위 고압.
- 산악 경로: 두 번째 "convective" 인덱스 (구 IK3XTV), 경로와 고압을 따라 능선보다 높은 오후 한계 층.

VHF/UHF DX에서 다른 보정을 시도: 포인트 (일, 더 많은 포인트)에 고압의 연속 일, anticyclone ( 가장자리에 bonus)의 센터와 비교된 위치, 바람의 측정으로 지상에 압력 기온변화, 강한 비 24 시간 전에 (현실), 높은 밴드의 가장자리에 낮은 구름. 경로의 방향: isobare의 경로의 azimuth를 비교합니다.

인증 및 인증

- LIVETROPOVHF 아카이브: 모든 상업 500 km에서 144 또는 432 MHz는 "event"입니다. 그것은 이벤트의 빈도와 경로에 인덱스를 비교, 당일과 달의 동시에 (CSIRO 연구의 동일한 접근).
- 같은 날에 Hepburn 및 F5LEN 와 비교: 3개의 인덱스가 분리되는 곳에, 당신은 반점에 따라 맞은 사람을 찾습니다.
- 00 및 12 UTC의 방사능: 모델이 프로브 측정의 변환을 볼 수 있는지 확인하십시오.
- LA0BY 의 역사 사례 (6–11/11/2003, 10/12/2004, 15/10/2011, 11/2/2015, 22/11/2018): 그 일에 대한 지수는 Hepburn이 표시되지 않은 덕트를 참조하십시오.
- 수동으로 선언 할 수있는 한계: 거의 GFS 압력 수준에 보이지 않는 100 ~ 200m 미만의 수행; 2 ~ 3 일 동안 신뢰할 수있는 예측, 다음 추세 만.

## 15. 소스 21 11월 (둘째 부분)

- ITU-R P.1812 (MaTLAB/Octave I. Stevanovic, OFCOM; GitHub eeveetza/p1812, Zenodo 10.5281/zenodo.6141558). VHF/UHF는 P.452에서 파생된 노선의 특정한 방법: “delta-Bullington” diffraction, troposcatter, ducts 및 반영은, 배경에 encumbrance. 포인트 포인트 포인트 계산에서 P.452 옆에 세 번째 비교로 유용합니다. ΔN 및 N0 데이터는 전체 ITU 제품이므로 사용자는 다운로드 (P.453 당).
- Costa, Rabanal Ramirez, Dias, IET MAP 2018 (도이 10.1049/iet-map.2017.0992). 563 MHz, 숲을 가진 불규칙한 지형에 5 그리고 30 km 사이 27 점: 12 vegetation의 m를 가진 P.526 −4,3 dB 및 σ 8,8 dB; 10 dB의 encumbrance +2,6 dB 및 σ 8,9 dB; 절연성 −4,8 dB 및 σ 9,2 dB; P.1546 +5,0 dB 및 σ 11,7 dB. 프로그램: 좋은 diffraction 방법의 잔여 과실은 대략 9 dB이고, 능선 (10-12 m) 조사에 encumbrances의 고도입니다.
- PETOOL (Ozgun e altri, 컴퓨터 물리학 통신 182, 2011). 다양한 각도 propagator를 가진 1개 그리고 2개의 노선에 무작위 방정식 쪼개는 단계 (40~45°까지, 좁은 것의 10~15°에 대하여); 변형된 Fourier 혼합을 통해서 손실과 바닥; 단계에 배경. 1.3.2 (넓은 각도, 토양 계수)의 선택을 확인하고 가파른 구호에 대한 양방향 계산을 나타냅니다.
- 수학 작업. `tropopl`: 가스의 손실 및 층 대기 (P.835, P.676, P.453)의«lente»의 손실, 덕트없이: −157 N / km의 그리스어로, 기하학적 광학 모델은 유효하지 않습니다. radar propagation의 예는 refraction −ΔN = 7,32·e^ (0,005577·Ns)를 위해, 프로그램에서 이미 동일한 CRPL 보고 사용합니다. `propagationModel` Longley-Rice: 전 정의된 수평 편광 가치, 전도도 0.005 S/m의 허용력 15, 반응성 301의 부드럽게 한 대륙 기후. ducts로 MathWorks 모델 거래가 없습니다.
- ITU-R, 그룹 디지털 제품 3. P.453, P.452, P.1812, P.2001, P.528, P.1546의 데이터 및 프로그램; 'integrated' 자료는 권고의 일부입니다; 선언 된 적대 라이센스: 당신은 사용자에게 다운로드 할 수 있습니다.
- ESA Navipedia, troposferical 지연. N = Nidrostatica + Numida; 건조한 zenital 지연은 대략 2.3 m이고, 빨리 변화하는 cm의 humid 1개의 10s입니다. 우리: GNSS 역에 의해 추정된 젖은 지연은 통합된 습도의 간접적인 측정, 지금 던지기의 가능한 미래 근원입니다; 오늘 쓸모가 없습니다.
- Bray, K2LMG, «Determining V.H.F. Station Capabilities», QST 11 월 1961, pp. 36–40 (원래 읽기; QST의 PDF에서 22 11 월 재검사: 아래 모든 값). 저자, D. W. Bray (General Electric, Ithaca), 그는 K2GOI 및 W4LTU 와 예정된 연결의 달과 예측을 확인 말한다. 대략 100 마일 (7 dB 이하와 시간 평균의 위 7)에 13,5 dB의 시간 안에 증발, 거의 선형 (3,5 dB에 50 마일): 단단한 연결을 위해 7 dB를 제거합니다. 수평선의 수정: 수평선의 각 고도는 여분의 거리의 69 마일 (111 km)의 가치가 있습니다 (매우 부정적인 경우에). 10 × 붐 / λ (전력 비율)에 대한 yagi의 이익, 토양 반사의 4 dB에 대해 더; SSB −3 dB 및 AM −7 dB CW와 비교; 시간의 99 %: 약 180 dB ~ 60 마일, 190 ~ 90, 200 ~ 240 (2m). 1.3.2에서 기간 동안 확률은 σ = 3,4 dB (±7 dB ±2σ로) 대신 5 dB 추정된 초침을 사용합니다.
- 양방향 계산 (시제품, 21 11 월). 카프리, 144 MHz, 안테나에서 실제 S RTM1 지형을 가진 큰 각 기하학적인 방정식은 배경에서 10 m, 표준 대기권에서 측정합니다: 20와 400 m에서 대응하는 노선에 80 km 사이 평균 힘. 카프리와 마리나 그란데의 중심에서 서쪽이 닫힙니다 (+11–21°, Monte Solaro): Faito ( EA5 와 EA6, 축에서 150-160° 뒤에) 신호는 10-29 dB에 의해 스페인 상승으로 발사했습니다; Vesuvius (110–125° 축선에서, 그 후에 계산 계획에서, 단지 나타냅니다) 17–25 dB에 의하여. Anacapri에서 서쪽은 무료이며 8-22 dB의 직접 승리입니다. 프로토 타입의 1000-1300km에 절대 손실은 아니지만 신뢰할 수 없습니다. EA3 EA5 EA6
- Barsis 및 Kirby, NBS 보고서 6751 (1961), 피크 피크, 223km, 100 및 751 MHz. 산은 광학에 있는 2개의 노선의 일반적인 맨끝입니다: 적시 매체의 배급은 2개의 노선의 그들의 복잡한입니다. 대략 14 dB, 동등한 길이 또는 각의 troposcatter 경로의 26–40 dB에 대하여 장기 excursion (1-99%); 감사한 일 변화 없음. 신호는 troposcatter 보다는 더 강합니다 그러나 이상적인 가장자리 보다는 약한: 정상은 둥근 (약 11.5 km를 통해) 대우되어야 합니다. 높은 신호는 항상 도rsals 뒤에 관찰됩니다.
- Barsis, NBS 보고서 7652 (1963), 산 (콜로라도, 알래스카, 유럽), 750-900 MHz의 세 가지 경로. 평균 손실 10-20 dB는 맨끝의 가까이에 땅 없이 Fresnel-Kirchhoff 계산 보다는 더 높습니다; 증발은 광학에 있는 2개의 선을 감싸기에 의해 예상됩니다; 맨끝의 가까이에 토양의 반영은 머리말을 붙인 퇴색합니다. 1.3.2에서 conduit에 항목은 P.526의 라운드를 추가합니다.
- 바시스와 존슨, NBS TN 88 (1961). 산을 넘어 경로에 공간 파의 해결을 연장 (§ 5).
- 노턴, «무선 파 전파 전파 전파의 시스템 손실», J. Res. NBS 63D (1959). Bray가 노모그램을 얻은 곡선; 시스템 손실, 전송 및 기본의 정의, 프로세스 안테나 이득.
- 쌀, Longley, Norton 및 Barsis, NBS TN 101 (1965-67), 볼륨 1 및 2. 단일 가장자리와 장애물과 반사없이 (§ 7, Pikes Peak의 예), 장기 가변성 V (0,5) 및 Y (q) 기후에 대한. (임대 및 기타). 우리를 위해: 둥근 장애 및 가변성 기능의 방법은 uncertainty를 측정하는 참고입니다.
- ITU-R P.526-16 (11/2025). J의 공식 (31), 프로그램의 그와 동일; 라운드 장애물 (32)–(36), 지금 사용; 멀티 분할 방법 (Bullington 및 전체 방법 § 4.5).
- ITU-R P.1812-8 (2025) 및 -7. 복구 된 텍스트 완료: 30 MHz의 특정 방법 6 GHz (델타-Bullington, troposcatter, 덕트 및 레이어에 반사). -7는 troposcatter (P.617-5에서)가 66km 미만으로 잘못되어 있음을 경고합니다. 가능한 세 번째 비교입니다.
- Paulus, «IREPS 증발 모델의 실제 응용», NOSC TR 966 (1984), 라디오 과학 20 (1985). 보고서 읽기 22/11/2026 (DTIC ADA146528, 인식 된 텍스트 스캔, 때때로 읽을 수 있음). 선박에 의해 측정되는 공기 바다 다름은 안정성으로 교대됩니다: 선박의 31-35%에 있는 더 온난한 바다 공기는 NOAA boars의 0.7-7%를, psicrometers를 위해 찬물 (대략 +1 °C)에 의해 가지고 갔습니다. Jeske 모형은 그 후에 40 m를 제공하고 연중의 15% 이상 및 고도는 라디오 측정에 의해 확인되지 않는 안정되어 있는 시간을 가진 24 시간에서 10 40 m에서 점프합니다. 제안된 개정 (시험된 문턱 0, −0,5 및 −1 °C; −1 °C는 더 나은 6월 1982의 라디오 측정으로 동의됩니다): 공기 바다 다름이 −1 °C 또는 더 적은 측정한 것은 이용됩니다; 그렇지 않으면 −1 °C와 0 °C를 가진 고도를 산출하고, −1 °C가 낮을 경우에, 당신은 그 붙듭니다. 안정되어 있는 측에 증가는 커트, 감소 남아 있습니다. 6 월, Marsden 120 평방: 평균 높이 16.8m에서 7.6m로. 모델의 방정식은 보고서에 있지 않습니다 (Jske와 Hitney에 참조). 이 프로그램은 안정되어 있는 배설이 없는 중립적인 단면도를 이용하고, δ에 40 m를 제한합니다: 그것은 동일한 방향에서 갑니다; SST 모형 (배로 아닙니다)와 Paulus가 무게를 더 적은 정확한 측정 결점.
- Levy, « 전자기 파 전파 전파 전파 추진»에 대한 Parabolic Equation 방법 (IEE 2000). Google Libri에는 미리보기 만 있습니다. 나는 읽지 않습니다.
- VK3FS (3fs.net.au). 품질 소개: 지상에 더 긴 오프닝을 위한 450-2000 m에 높은 덕트; 해돋이 후에 사라지는 평온하고 평온한 밤에 있는 300-600 m의 변환; 오후에 있는 더 강한 해안 inversions; 800-1300 km 전형적인, 자주 실시되는 1400-1600 km. 하루/밤 알림 및 2500m의 천장과 일관성.

## 16. 이름 *

규칙 및 공식

- [ITU-R P.453-13 (2017), 방사성 지수:](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)공식 및 데이터
- [ITU-R P.452-18 (2023), 100 MHz 이상 지상 역 간섭 예측: 동등한 반경, β0, troposcatter,](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)덕트
- [ITU-R P.617-5, 라디오 브리지](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)의 수평선을 넘어 전파
- [시 외., "다른 증발 덕트의 전자기 전파의 최대 길이에 대한 포괄적 인 연구", IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

날씨 모델과 덕트 예측

- [P. Lopez, "A 5 년 40km 해상도의 글로벌 클라이마트론은 지상 기반 기상 레이더, ECMWF 기술에 대한 슈퍼 파괴의. 메모 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. 스즈키 (CSIRO), "Tropospheric Ducting Prediction based on GFS Model Data at... CSIRO Murchison Radio-astronomy", 전망대 arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (FMI), "Harmonie-AROME와 대기 덕트 조건을 예측하는 모델 해상도의 효과", EGUsphere 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

아마추어 라디오 예측

- [W. R. Hepburn, "The Hepburn Tropo Index (HTI)"](https://www.dxinfocentre.com/propagation/hti.htm)및 [](https://www.dxinfocentre.com/propagation/history.htm)방법 역사
- [W. R. Hepburn, "낮은 사용 가능한 주파수 대. 덕트 층의 두께"](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, "세계적인 굴절률 예측"](https://tropo.f5len.org/WW/info.php), [새로운 버전 2026](https://tropo.f5len.org/?p=504), [모델 ARPEGE](https://tropo.f5len.org/author/f5len/)에 기초

자료실

- [Open-Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api), [압력 레벨](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)에 기초
- [NOAA NCEI, 통합 글로벌 Radiosonde Archive (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RAOBget, Wyoming](https://github.com/NCAR/RAOBget)대학 라디오 홍수 스크립트

클래식 텍스트 (프로그램의 저자에 의해 제공, 최종 공식에 대해 상담)

- B. R. Bean, E. J. Dutton, *Radio Meteorology*, NBS Monograph 92, 1966, 435 페이지 (reprint Dover 1968): N 및 M의 수직 프로파일, −157 N / km, 라디오 파와 비교; 장 5 "Synoptic 라디오 meteorology". 점 당 계산의 2~4단계를 위한 기초. 무료: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [NIST 카드, 도이 10.6028/NBS.MONO.92](https://www.nist.gov/node/1770911), [인터넷 아카이브](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf)복사.
- L. Boithias, *Radio Wave Propagation*, McGraw-Hill 1987 (프랑스어, 330 페이지, ISBN 0070064334에서 D. Beeson에 의해 번역; 또한 노스 옥스퍼드 아카데미): 수평선을 넘어 메커니즘, 결합 안테나 반의 분산 및 손실. 종이 혼자 ([라이브러리 카드](https://libcat.library.tamu.edu/Record/in00000014812/Description)).
- M. P. M. Hall, * Radio Communication*, IEE 전자기파 시리즈 n. 8, Peter Peregrinus 1979, 206 페이지 (ISBN 0906048257; brossura 0863410863): 아날로그 상황 및 전파 VHF / UHF. Synoptic 성분을 위한 기초. IET에서 거래 ([](https://libcat.library.tamu.edu/Record/in00000636385)카드).
- H. G. Booker, W. E. Gordon, "트로포구에서 라디오 Scattering의 이론", *IRE*, vol. 38, n. 4, 4월 1950, pp. 401–412: 굴절률의 불균성 이론을 스크랩. 중앙 아이디어: 확산된 힘은 굴절률 (경력적인 상관 관계); 수평선 prevails diffraction의 가까이에, 더 흩어지기의 강렬 그리고 가늠자에 달려 있습니다; 저자에 따르면 저전도학에 의해 예상된 색인 변이 측정한 분야를 설명하는 충분합니다. 인용 328 기사 (Scilit), 무료 액세스 사본 없음. Semantic Scholar의 카드 ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); IEEE Xplore 유료의 전체 텍스트 ([doi 10.1109/JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)).
- C. M. Crain, J. R. Gerhardt (전기 공학 연구 실험실, 텍사스 대학), "Measurements of the Parameters Involved in the the Theory of Radio Scattering in the Troposphere", *IRE*, vol. 40, n. 1, 1, 1952, pp. 50-54, doi 10.1109/JRPROC.1952.2739는 Booker 및 Gordon (Ski)의 실제 값을 제공합니다. (Ski).](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)
- " 라디오 주파수의 대기 보수 지수에 대한 평등의 상황", *IRE *, vol. 41, n. 8, 8 월 1953, pp. 1035–1037: 공식에서 사용 (79 K/mb 및 4800 K)의 현재를 검토하는 제안 N; 값 77,6 및 3,73 · 10^5 공식 ITU (Q](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E. C. S. Megaw, "Fundamental radio scatter propagation magazine", *IEE Part C: Monographs*, Vol. 104, n. 6, september 1957, pp. 441–455, doi 10.1049/pi-c.1957.0052: turbulence 이론의 색인 변동의 스펙트럼, 일반적인 수평선 및 영향보다 분산의 완전한 추적 (IETT) X[C].](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)
- *IRE*, 10월 1955, 흩어져 전파에 특수 번호 (K. A. Norton 및 J. B. Wiesner), Bullington, Staras 및 기타: 무료 PDF [세계 라디오 역사](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf).
- D. E. Kerr (edited by), * Short Radio Waves*, MIT Radiation Laboratory Series vol. 13, McGraw-Hill 1951 (Dover 1965 및 IEE 1987): 바다에 증발 및 표면 덕트의 이론. 디지털 복사: [세계 라디오 역사](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); IET 판 [IET 디지털 라이브러리](https://shop.theiet.org/propag-short-radio-waves).

VHF의 troposferical 확산의 최근 측정: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Australia), "미국의 VHF Troposcatter 통신 채널의 연구", 통신 시스템 *, Springer 1997, pp. 103–112 ([Springer](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)). 수수료에 대한 텍스트; 추상에서 그것은 확산에 우려, 도관이 아닙니다. VHF 채널 데이터로 troposcatter의 용어를 측정하려는 경우에만 유용합니다.

숫자 모델과 바다 노선 (프로그램의 저자에 의해 제공)

- M. 레비, * 전자파 전파 전파 전파 추진 방법 *, IEE 전자파 시리즈 #45, 2000, 336 페이지 (ISBN 0852967640): 경로와 배전을 위해 변화하는 굴절 방지 프로파일에 대한 패기식 방정식; tropopheric 전파, 레이 및 방법의 장, 바다에 전파. 인덱스를 넘어 경로에 따라 필드 계산을 할 것입니다. 지불을 위해, 또한 IET ([IET 카드](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop))에서 전자 형태로.
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (리스터 대학교): 채널 제도와 채널에 2 GHz 측정. 많은 작품과 논문은 대학 아카이브에서 무료입니다: [증발 덕트에 장기 통계, 라디오 과학 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434), [세 바다 경로에 신호 변경, 라디오 과학 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710), [N. Mufti 2012의 경로와 50 및 140km의 경로와, [I의 테시스. Alam 2002 날씨 데이터, 240 MHz 및 2 GHz X3 UQ, [.](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854)](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus, " 증발 모델의 실제 응용", 라디오 과학 vol. 20 n. 4, 1985, p. 887 (안테나와 전파에 IEEE 트랜잭션이 아닙니다), doi 10.1029/RS020i004p00887 ([scheda](https://zendy.io/title/10.1029/rs020i004p00887)): 표면 기상 데이터에서 증발 덕트의 높이. 기술 보고서를 무료로 제공: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf).

T. Lindquist, * 장거리 UHF / SHF 라디오 연결을위한 Troposphere의 전파 모델 *, 마스터의 thesis, 칼레스타드 대학, 2 6 월 2020, 106 페이지 (프로그램의 저자에 의해 공급). 그것은 단계별 가이드, 무료, 파라다이스 방정식과 계산을 작성: 분할 단계 솔루션 (넓은 각도에서 조차), 땅과 바다를위한 경계 조건, 단계 배경, 숫자 검사와 함께 선형 프로파일에 대한 빔 추적; MATLAB에 대한 PETOOL 무료 도구를 인용. δN ≈ 0.26 δP + 4,3 δe − 1,4 δT (hPa와 K, 약 20 °C)에 대한 두 가지 유용한 데이터, 즉. 증기 압력에 1 hPa의 오류는 온도에 1 °C 오류를 무게: 먼저 검사해야하는 모델의 습기입니다; (2) 증발 덕트의 평균 높이: 5-6 m, 13 432, 20 MHz의 지중해, 20 MHz의 지중해, 20 MHz의 지중해, 20 MHz의 지중해에서. 저자의 결론 (1 ~ 10 GHz): 수평선을 넘어 신뢰할 수있는 연결은 거의 항상 덕트가 필요합니다.

토양 프로파일에 전파 소프트웨어 (지평선 및 장애물, 예상 tropo에 대한)

- [Radio Mobile](https://ve2dbe.com/rme.html)by Roger Coudé VE2DBE: 무료, Windows (또한 Linux 및 Mac with Wine), Longley-Rice/ITM 모델, 지형 프로파일은 할당량 데이터 ([download](https://ve2dbe.com/download/download.html))에서 자동으로 추출됩니다.
- [특전]](https://qsl.net/kd2bd/splat.html)by John Magliacane KD2BD: GPL, 20 MHz-20 GHz, Longley-Rice 및 ITWOM v3.0 모델, 장애 및 손실 보고서 텍스트; 최신 버전 1.4.2 of 2014.
- [ITM, NTIA/ITS](https://its.ntia.gov/software/itm)공식 코드: Longley-Rice 모델 (20 MHz–20 GHz) 소스와 문서, 라디오 모바일 및 SPLAT의 기초!. 코드는 무료이며, LIVETROPOVHF의 경로에 계산을 추가 할 수 있습니다 (토양으로 인해 광택, tropo 색인에 추가 될 수 있습니다). Original report: A. G. Longley, P. L. Rice, "Reregular Terrain에 Tropospheric Radio transmission Loss의 사전: 컴퓨터 방법 - 1968", ESSA 기술 보고서 ERL 79-ITS 67, 7 월 1968 (역사 소프트웨어의 NTIA 카드: X1 https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITU-R, 소프트웨어 및 디지털 데이터](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): 그리드에 ΔN 및 N0 연례 미디어의 무료지도 1,5° P.452 및 P.617에 의해 사용 — 지수에 대한 참조 기후 값으로 유용합니다.
- Pathloss (Contract Telecommunication Engineering): 라디오 브리지를 위한 상업적인 프로그램; 나는 ITU 권고가, 검사하는 것을 확인한 대중적인 근원을 찾아내지 않았습니다.

LIVETROPOVHF의 재사용 가능한 GitHub 코드

| 회사연혁 | 그는 무엇을 | * 이름 | 자주 묻는 질문 |
| [NTIA/ITM의](https://github.com/NTIA/itm) | NTIA의 모델 Longley-Rice 공식, 패키지. 사이트맵 |  C++, C#  | 경로의 토지 손실: C #에있는 프로그램으로 직접 통합 |
| [fuzzzeee/itm의](https://github.com/fuzzzeee/itm) | ITM C++ 코드 C# 번역 |  C#  | 기본 라이브러리없이 대안, Windows 7 버전에 유용 |
| [eeveetza/Py452 엑스0Q](https://github.com/eeveetza/py452) | ITU-R P.452는 ITU-R WP 3M에 의해 찬성된 MATLAB/Octave 버전과 동일합니다; 스위스 연방 통신 사무실에 의해 간행된 MATLAB 버전 | 파이썬, MATLAB | 번역을 참조 C# troposcatter 공식 및 실시, ITU 검증 예 |
| [bwinkel/피크라프](https://github.com/bwinkel/pycraf) | P.452 완료, P.676, SRTM 프로필; GPL v3, 하지만 포함 된 ITU 데이터 파일은 상업적 사용을 위해 무료입니다 | · | 우리의 결과의 통제 |
| [ITU-Rpy의](https://github.com/inigodelportillo/ITU-Rpy) | ITU 예제로 검증된 P.453-13을 포함한 많은 ITU-R 권고 | · | P.453의 N 공식 및 기후지도 검증 |
| [NCAR/RAOBget](https://github.com/NCAR/RAOBget)]를 사용 | 텍스트 형식으로 Wyoming 대학의 라디오 파를 다운로드 | · | C #에서 다운로드하는 다이어그램은 이탈리아 방송국의 프로필 |

참고: P.453의 현재 버전은 [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), 무료로 다운로드 할 수 있습니다. 문서에서 나는 13을 사용하여 실현합니다.

프로그램 매뉴얼에서 이미 여기 재사용: LA0BY (Nordic VUSHF 2019), IK3XTV ( "2 미터에 대한 조사"및 "Propagation VHF - convective cell and gravitational wave", 프로그램 저자가 제공하는 라디오 잡지), DF5AI/DK5YA (2003), G3YGF (QST), VHF / UHF DX Book cap. 2, G4MCU (2003) KP4MD
