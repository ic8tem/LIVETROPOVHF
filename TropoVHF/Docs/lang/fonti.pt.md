# Fontes e fórmulas

> Tradução automática por italiano: em caso de dúvida vale o guia italiano.

Recolha de trabalho de fontes consultadas para a previsão do Tropo LIVETROPOVHF (20–21 de novembro de 2026): Física básica, dados disponíveis, índices publicados, fórmulas, experiências de rádio amador, validação e decisões. As fórmulas são numeradas como em LaTeX, (1), (2),..., e no texto que eles lembram com o número entre parênteses. O Manual de Previsão descreve como o programa os usa.


## 1. Instalações do autor

Observações IC8TEM que enquadram o trabalho, com feedback encontrado em fontes.

- A geografia italiana exige difração. Com cumes, promontório e ilhas a poucas dezenas de quilômetros das estações, as contribuições de difração devem ser calculadas com o parâmetro Fresnel na borda principal, não só com a terra lisa. Encontro: em uma comparação de terreno irregular a 563 MHz (Rio de Janeiro, 27 pontos entre 5 e 30 km), os melhores métodos foram P.526 com bordas de lâmina, P.1812 e a cunha difrativa, com erro médio entre −5 e +3 dB e dispersão de cerca de 9 dB; P.1546 foi o mais pessimista (de Costa, Rabanal Ramirez e A Para encostas íngremes perto da estação PETOOL lembrar que servem propagadores de ângulo largo e o cálculo bidirecional (onda refletida pelos relevos).
- Para entrar numa conduta distante, precisa de Fresnel. Se o conduíte começa além do horizonte, o sinal vem até nós por difração sobre obstáculos até seu início; desde 1.3.2 o programa o calcula (Manual de Previsão, § 10 ter). Encontro: Rohde & Schwarz (2023) observa que estações ao longo de um ducto alto muitas vezes não conseguem entrar, enquanto aqueles perto das extremidades sim.
- As condições mudam rapidamente. As teorias estão lá, mas as condições mudam em minutos, e nenhum sistema de previsão leva os dados em tempo real: o GFS sai a cada 6 horas, com tempo e 0,25°, e uma previsão pode perder valor em um curto espaço de tempo. Reunião: Bray (QST, novembro de 1961) medição de flutuações médias horárias de aproximadamente ±7 dB sobre 100 milhas; os dutos se movem com o tempo atmosférico (Rohde & Schwarz); nosso registro QSO de 29/12/2021-2/2/2022 mostra aberturas crescendo e caindo dentro de horas.
- A melhor versão agora permanece a rede DX cluster–PSKReporter. Para saber o que acontece agora, os pontos de cluster, PSKReporter (e WSPR) bater qualquer padrão: eles são medidas. A previsão diz onde olhar e quanto tempo pode durar; comerciais dizem se houver. O programa para isso coloca ponto e previsão no mesmo mapa.

## 2. Objecto

A ideia é um terceiro índice, calculado pelo próprio LIVETROPOVHF a partir dos perfis verticais de um modelo meteorológico, a ser adicionado a Hepburn e F5LEN em vez de substituí-los. Hoje o programa lê seus mapas já prontos: um índice também diria *por que* há abertura (quota e espessura do conduíte, faixa mínima presa) e poderia ocorrer com os pontos que o programa já coleta.

- O que você pode dar: probabilidade de conduíte por ponto e por hora, parte da base e topo, espessura, frequência mínima presa, e ao longo de um caminho a fração da rota dentro de um conduíte.
- O que não pode dar: os dutos finos (abaixo de alguns dez metros) que os modelos globais a 0,25° não resolvem, e os dutos de evaporação no mar, que servem acima de tudo a partir dos GHz para cima.
- Primeiro objetivo prático: 144 e 432 MHz na Europa, com verificação em pontos de cluster DX, PSKReporter e decodificações locais.

## 3. Física Básica

Tudo começa a partir da refração N calculado em cada nível de perfil: o canal é onde a reatividade modificada M diminui escalando, ou seja, onde dN/dh cai abaixo de −157 N/km ([ITU-R P.453-13](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf), [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)).

Refractividade (P e em hPa, T em kelvin):

```math
N = 77,6\,\frac{P}{T} - 5,6\,\frac{e}{T} + 3,75\times10^{5}\,\frac{e}{T^{2}} \;\approx\; \frac{77,6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

Pressão do vapor da humidade relativa H (%) e temperatura t (°C), acima da água entre -40 e +50 °C:

```math
e = \frac{H\,e_s}{100}, \qquad e_s = 6,1121\,\exp\left[\left(18,678 - \frac{t}{234,5}\right)\frac{t}{257,14 + t}\right]
```

Reatividade modificada (h em km, ou 0,157 · z com z em metros) e critério de conduta:

```math
M = N + 157\,h, \qquad \frac{dM}{dh} < 0 \iff \frac{dN}{dh} < -157\ \mathrm{N/km}
```

| dN/dh (N/km) | Regulamento | Efeito de 144 MHz |
| 0 | sub- refração | caudal reduzido |
| de 0 a −79 | normal (padrão aproximadamente -40) | horizonte de rádio com raio equivalente 4/3 |
| de −79 a −157 | super-refracção | fluxo alongado, "muito bom" |
| abaixo de −15 | Conduta (trapagem) | sinais presos, DX mais de 1000 km |

Como extrair N de um modelo em níveis de pressão: para cada nível você toma T, umidade relativa e altura geopotencial, você calcula N e M, em seguida, o gradiente entre os níveis vizinhos. Os níveis abaixo do solo devem ser descartados (em 1000 hPa nos Alpes é subterrâneo).

Frequência mínima presa: o conduíte guia apenas as ondas bastante curtas em comparação com a sua espessura. Hepburn publica uma espessura de tabela → frequência mínima útil ([DX Info Centre](https://www.dxinfocentre.com/propagation/luf.htm)); é um limite suave, sob o qual o sinal gradualmente cai.

| Espessura Liderada | Frequência mínima a pagar |
|  61 m  | 962 MHz |
|  91 m  | 464 MHz |
|  122 m  | 276 MHz |
|  152 m  | 185 MHz |
|  183 m  | 133 MHz |
|  213 m  | 101 MHz |
|  305 m  | 53 MHz |

Na faixa VHF (frequentemente 120-300 m) os valores da tabela seguem muito bem uma lei sobre constante; abaixo de 100 m a fórmula subestima a frequência até 30-40%. Adaptando-o eu mesmo (não uma fórmula publicada por Hepburn), com f em MHz e d em metros:

```math
f_{min} \approx \frac{3,3\times10^{5}}{d^{3/2}} \ \mathrm{MHz} \quad\Rightarrow\quad d_{144} \approx 175\ \mathrm{m}, \quad d_{432} \approx 85\ \mathrm{m}
```

Na prática: para os 2 m, um conduíte é frequentemente pelo menos 170-180 m, para os 70 cm é cerca de metade. A fórmula que circula na rede, λmax = 0,014 · d^1.5, vem de Kerr (1951, capítulo 1, eq. 18) e é válida com λ em centímetros e em pé, para um conduíte em que M cai linearmente: com estas unidades dá por cerca de 2 m 180 m e por cerca de 70 cm 89 m, que é os mesmos valores da tabela Hepburn, que vem dela. Kerr adverte que não é um corte de rede: mesmo ondas várias vezes mais sofrer muito do conduíte, e uma maneira que "perda" um pouco pode melhorar a recepção mesmo com estações fora do conduíte. Um estudo do IEEE Access 2019 confirma que os métodos clássicos (Kerr, Kukushkin) não descrevem o corte real ([Shi et al., 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)): o limiar deve ser usado como uma indicação, não como uma parede.

## 4. Dados disponíveis gratuitamente

O ponto fraco é a resolução vertical: entre 1000 e 850 hPa modelos dar um nível a cada 25-50 hPa, ou seja, a cada 200-450 m, de modo que eles vêem bem as inversões para 500-2000 m, mas não os dutos finos perto do solo. LIVETROPOVHF já usa Open-Meteo para isobar, temperatura para 850 hPa e inversão: o índice pode começar a partir das mesmas chamadas.

| Origem | O que se passa? | Resolução | Utilização para o índice |
| [Meteo aberto, GFS](https://open-meteo.com/en/docs/gfs-api) | T, humidade relativa, ponto de orvalho, altura geopotencial por nível (1000, 975, 950, 925, 900, 850 hPa...); altura da camada limite | 0,25° em níveis de pressão; 16 dias, horas até 120 h, atualizado a cada 6 h | perfil N e M, gradiente, base e topo do canal; já no programa |
| [Meteo aberto, ECMWF](https://open-meteo.com/en/docs/ecmwf-api) | os mesmos níveis dos modelos de dados abertos 0,25°; o solo dá ponto de orvalho |  0,25°  | segunda opinião, útil para a concordância entre modelos |
| [Meteo aberto, DWD ICON](https://open-meteo.com/en/docs/dwd-api) | T e umidade relativa em níveis de pressão; em ICON- D2 há 800, 900 e 925 hPa | ICON-UE cerca de 7 km | Terceiro parecer sobre a Europa |
| [Radiosunds, Univ. Wyoming](https://github.com/NCAR/RAOBget) | perfis medidos, dois lançamentos por dia (00 e 12 UTC), formato de texto por estação | muitos níveis significativos, mesmo a cada poucas dezenas de metros | verificação do índice; ver as condutas finas que o modelo não vê |
| [Gemidos, NOAA NCEI](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive) | arquivo histórico de transmissão, um arquivo por estação, mais parâmetros derivados | como acima | calibração em anos de dados e comparação com pontos passados |
| ERA5 (Copernicus) | Reanálise oral desde 1940, 37 níveis de pressão | cerca de 28 km | estatística e calibração, não prevista |

- Com altura geopotencial Open-Meteo é acima do nível do mar: os níveis que caem sob o solo devem ser descartados ([Open-Meteo, ar superior](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)).
- No solo são adicionados T e umidade em 2 m: eles dão o primeiro trecho de gradiente, que onde os dutos de superfície noturna nascem na planície e no mar.
- Transmissões italianas no arquivo da Universidade de Wyoming (weather.uwio.edu/upperair/sounding.html): estações e números WMO na tabela abaixo.

| Estação | W | Latitude | Longitude | Partilha |
| Udina/Campoformido |  16044  |  46,03  |  13,18  |  94 m  |
| Milão/Linato |  16080  |  45,43  |  9,28  |  103 m  |
| Prática do mar |  16245  |  41,65  |  12,43  |  12 m  |
| Brindisi |  16320  |  40,65  |  17,95  |  10 m  |
| Trapani/Birgi |  16429  |  37,92  |  12,50  | cerca de 14 m |

O formato de texto de Wyoming dá para cada nível de pressão, altura, temperatura, ponto de orvalho e umidade: suficiente para N e M. Wyoming não tem uma interface oficial, scripts são usados como [RAOBget de NCAR](https://github.com/NCAR/RAOBget); o Colégio de DuPage mantém cópias em texto por estação ([e.g., 14430 Zara](https://weather.cod.edu/cdata/raob/14430/text/14430.20250612.12.txt)).](https://artefacts.ceda.ac.uk/badc_datadocs/radiosonde/countries/Italy.html)

## 5. Índices e métodos publicados

Hepburn e F5LEN começam a partir dos mesmos ingredientes físicos, mas não publicam o algoritmo completo; os estudos científicos de 2008-2026 em vez de descrever o método passo a passo, e é a partir daí que vale a pena copiar a receita.

| Método | Dados iniciais | O que é calculado | Notas úteis para nós |
| [Hepburn Tropo Index](https://www.dxinfocentre.com/propagation/hti.htm) | Modelos meteorológicos brutos do Serviço Meteorológico do Canadá (MSC, conforme escrito sob seus mapas; não GFS/NAM), mapas projetados com GRADS, algoritmo proprietário ([storia](https://www.dxinfocentre.com/propagation/history.htm)) | Escala linear 0–10 de curvatura do sinal: 0 normal, 1–2 superrrefracção, 3–4 algumas condutas, 5–6 condutas difusas; valores negativos = subrrefracção | a escala a ser imitada, assim o terceiro índice compara-se imediatamente com os dois primeiros |
| [F5LEN, previsão mundial](https://tropo.f5len.org/WW/info.php) | Reatividade na unidade N; para a Europa o modelo ARPEGE de Météo-França, 7,5 km, mais níveis entre 2 m e 1500 m ([nota do autor](https://tropo.f5len.org/author/f5len/)) | Índice médio para proprietário, 40 passos de 3 horas, 4 cálculos por dia, 28 dias de arquivo ([nova versão](https://tropo.f5len.org/?p=504)) | confirmar que serve um modelo com muitos níveis baixos |
| [Lopez, ECMWF 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf) | 60 níveis do modelo ECMWF, desde o solo até 3000 m | mínimo de dN/dz por coluna: realizado em −157 N/km, super-refracção em −78,7 N/km; base da camada de armadilha | climatologia: Padana Planície da Europa com mais oleodutos durante todo o ano; Mediterrâneo no verão perto de 100%, no inverno abaixo de 15%; Itália mais de 30% nas noites de verão |
| [Indermuehle e Suzuki, CSIRO 2026](https://arxiv.org/pdf/2608.12861) | GFS 0,25°, 21 níveis de pressão + campos a 2 m | M = N + 0,157 z; dM/dz mínimo inferior a 3 km, base e topo do canal, profundidade do défice de M; menos de 40 m descartados | verificado durante 7 anos em 25 canais de 88 MHz a 2,68 GHz; boa capacidade até cerca de 3 dias de antecedência (AUC cerca de 0,74), em seguida, cai; GFS melhor que ERA5 |
| [Hämäläinen et al., FMI 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf) | Harmonie-AROME, 750 m ou 2,5 km, 65 ou 90 níveis | dM/dh < 0 no perfil do modelo, comparação com uma viga de 4 a 59 m | conta mais vertical do que a resolução horizontal; nenhum modelo vê os dutos abaixo de 15 m; a umidade domina no gradiente de temperatura |

Terceiro serviço de rádio amador já ativo: [TROPOCAST.EU](https://tropocast.eu/)de Artur SQ3A, para 2 m e 70 cm na Europa. A partir da página de previsão ([tropocast.eu/preview](https://tropocast.eu/forecast)):

- Dados: DWD ICON-EU (passagem 0,0625°, cerca de 7 km), fonte padrão; alternativamente, GDPS canadense (0,15°), menos detalhado.
- Método: N e M de acordo com ITU-R P.453 do perfil entre a camada próxima ao solo e 850 hPa; o reconhecimento das camadas, a união dos dutos ao solo e alta e os limiares das cores são uma calibração adequada do autor, não publicada.
- Produtos: visão "completa" 0–3000 m, visões mais separadas "para o chão" e "alta" (por enquanto apenas em mapas a serem incorporados); pontuação 0–100 em nove classes (sob 38 fundo, 38–52 marginal, 53–58 fraco, 59–65 moderado, 66–72 bom, 73–80 muito bom, 81–88 alto, 89–95 forte, 96–100 muito forte); previsão até +72 h em uma hora, com animação e arquivo.
- Áreas: Europa, Polónia, Reino Unido, Alemanha, França, Espanha, Itália, República Checa, Croácia; interface também em italiano.
- Reuse: maps incorpore com um iframe sem chaves de API; há também uma API documentada (OpenAPI e esquema JSON). A licença não está indicada: Antes de usá-lo como uma terceira fonte ao lado de Hepburn e F5LEN você deve perguntar ao autor.

Diferenças em relação ao nosso projeto: o TROPOCAST para em 850 hPa (cerca de 1,5 km), enquanto os dutos elevados chegam a 2000 m e mais (caso LA0BY de 22/11/2018), e não dá um índice para banda ou rota. Classes 0–100 podem ser comparadas ao nosso índice 0–10 nos mesmos dias.

TROPOCAST estrutura de imagem (a partir do link fornecido pelo autor do programa): `tropocast.eu/api/v1/tropo/frames/{n}.png?run={corsa del modello, es. 2026-11-19T18:00:00Z}&model={full|surface|elevated}&area={europe|italy|…}&source={icon-eu|gdps}`, onde n é o passo de previsão. O site proíbe a seleção automática de robôs, então eu não pude ler a imagem; também para isso, antes de mostrar esses mapas no LIVETROPOVHF serve a permissão do autor.

Nota: [DJ5AR, categoria](https://dj5ar.de/?cat=174)(Magonza, 21 artigos 2017–2023, 2 m–13 cm). Casos descritos: conduzidos várias semanas a partir da Irlanda (23 de maio-2 de junho de 2023), FT8 para a Bielorrússia (setembro de 2020), Itália-Alemanha sobre os Alpes e Adriático (agosto de 2017). Reconheça o caso do ducto "muito alto" (reverso a 1000 m) que você não pode usar a partir do solo: outra confirmação de que é necessário comparar a parte do conduto com a das estações. Utiliza radiofácies como medida de abertura (até 1028 km) e perfis verticais em Paris, Bordeaux e DWD.

Em resumo, a receita comum: perfil vertical → N e M por nível → camada com dM/dh negativo → base, topo, espessura e força do ducto → índice. A parte "secreta" de Hepburn é a calibração empírica em eventos reais, que podemos refazer com os pontos.

## 6. Fórmulas Úteis para o programa

Além de N e M (seção física básica), estes servem para mover de número vertical para número de ponto e, em seguida, por caminho. As fórmulas da ITU foram reconstruídas por um PDF lido em parte; em 22 de novembro, foram revisadas em textos oficiais ([ITU-R P.452-18](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf), P.453-14, P.526-16, P.372-18) e corrigidas no código: constante do troposcatter 190 (não 190,1) mais a absorção de gases com ρ = 3 g/m3; α não abaixo de −3,4 (eq. 55a); gás dos ductos com ρ = 7,5 + 2,5·ω (eq. 9a); difração em terra esférica total (eq. 23–37: média terra-mar, função de altura com limite 2 + 20 logs K, interpolação sob o fluxo óptico); pressão de saturação de vapor de P.453-14 (eq. 9, com EF) em vez da fórmula antiga de Magnus. Rumore P.372-18: tabela 1 e correspondência galáctica; curvas de ruído artificial são de até 250 MHz e a galáctica até cerca de 100 MHz, ao longo do programa estende as mesmas leis.

1. Gradiente entre dois níveis do modelo (h em km, M na unidade M):

```math
\frac{dM}{dh}\Big|_{i} = \frac{M_{i+1} - M_{i}}{h_{i+1} - h_{i}}, \qquad \text{condotto se } \frac{dM}{dh} < 0
```

2. Resistência e espessura do conduto: espessura d = topo − base (para um ducto de solo, base = solo); resistência como deficiência M entre base e topo.

```math
d = h_{cima} - h_{base}, \qquad \Delta M = M_{base} - M_{min}
```

3. Fator equivalente ao raio da Terra (P.452-18, p. 5 e 6a), com ΔN = queda média de N no primeiro km, positiva, em N/km:

```math
k_{50} = \frac{157}{157 - \Delta N}, \qquad a_e = k_{50}\cdot 6371\ \mathrm{km}
```

Com ΔN = 40 você tem cerca de 4/3 k (a atmosfera padrão); com ΔN que se aproxima de 157 k tende ao infinito, ou seja, o rádio da Terra torna-se plano: é o início do ducto.

4. horizonte de rádio de uma antena alta h metros, com raio equivalente a\_e em km:

```math
d_{oriz} \approx \sqrt{2\,a_e\,h/1000}\ \ \mathrm{km}\quad(\text{con } k = 4/3:\ d \approx 4,12\sqrt{h})
```

5. Probabilidade da conduta para a latitude (P.452-18, eq. 2), percentagem do tempo; ♦ = latitude do centro da rota, μ1 e μ4 ter em conta a distância por mar:

```math
\beta_0 = 10^{\,-0,015|\varphi| + 1,67}\ \mu_1\,\mu_4\ \%\qquad (|\varphi| \le 70^\circ)
```

A 41° N, todos por mar (μ1 = μ4 = 1), dá cerca de 11% do tempo: é a base climatológica, útil como um valor de referência do qual o índice esperado difere.

6. Perda de troposcatter (P.452-18, eq. 45; f em MHz, d em km, ângulo do trajeto em mrad, reatividade do solo N0 no centro da rota):

```math
L_{bs} = 190 + L_f + 20\log_{10} d + 0,573\,\theta - 0,15\,N_0 + L_c + A_g - 10,1\left[-\log_{10}(p/50)\right]^{0,7}
```

```math
L_f = 25\log_{10} f - 2,5\left[\log_{10}(f/2)\right]^2
```

O termo −0,15 · N0 mostra porque o ar úmido também ajuda sem ducto: 50 unidades extras N são 7,5 dB menos do que perda. LIVETROPOVHF já usa a versão G3YGF da perda de dispersão.

7. Perda do canal (P.452-18, p. 46–51): perda fixa do acoplamento mais uma atenuação do miliradiante angular, que cresce com a raiz cúbica de frequência (f em GHz, a\_e em km):

```math
L_{ba} = A_f + A_d(p) + A_g, \qquad \gamma_d = 5\times10^{-5}\,a_e\,f^{1/3}\ \ \mathrm{dB/mrad}
```

## 7. De Bean e Dutton (1966) e Kerr (1951)

Os dois textos clássicos dão três coisas que são necessárias imediatamente para o índice: limiares de gradiente já tarato nos mesmos níveis de pressão de GFS, o que vale em dB uma mudança de reatividade, e que situações meteorológicas criam ou destroem os dutos. <br clear=left>== Ligações externas ==

1. Classificação do perfil para camadas de pressão (Bean e Dutton, tab. 6.5, p. 248; −dN/dh em N/km). Projetado para comprimentos de onda de rádio, aplica-se como está em níveis de modelo:

| Estrada | Partilhas sobre | Sub- refração | Não laminados | Super- refração |
| 1000–850 hPa | 0–1,46 km | −dN/dh < 0 | de 20 a 60 | mais de 100 |
| 850–700 hPa | 1,46–3,01 km | −dN/dh < 0 | de 20 a 50 | mais de 80 |
| 700–600 hPa | 3,01–4,20 km | −dN/dh < 0 | de 15 a 40 | mais de 70 |
| 600–500 hPa | 4,20–5,57 km | −dN/dh < 0 | de 10 a 30 | mais de 50 |

2. Quanto vale em dB (Bean e Dutton, cap. 6, pp. 233–258), medidas de 70 a 180 MHz em rotas para além do horizonte:

- Qualquer camada (no solo ou na altitude) em comparação com o perfil "suave" eleva o sinal de 10-25 dB para todas as percentagens; o maior ganho vem das camadas elevadas inclinadas, provavelmente devido ao efeito de focagem (curso de 200 km).
- A reatividade do solo Ns e o declínio no primeiro km ΔN estão relacionados com o sinal médio mensal com coeficiente entre 0,4 e 0,95, mediana 0,7 (20 rotas de 130 para 446 km, cerca de 100 MHz).
- Ciclo anual: 0,18 dB por unidade N; ciclo diário de 0,2 a 1,1 dB por unidade N, máximo em rotas de 175 a 200 km e inverno.
- O parâmetro composto de Misme estima a variação do campo em 0,5 dB/(N/km) · (gradiente equivalente − 40) mais um termo de estabilidade térmica; o parâmetro de turbulência D (área entre o perfil real e o adiabático, 0-3 km) simplesmente melhora em comparação com o simples ΔN. Conclusão dos autores: ΔN permanece o melhor parâmetro para a simplicidade.

3. Perfil de referência e anomalia (Bean e Dutton, § 3.8 e 3.9). A atmosfera exponencial de referência CRPL dá o perfil "normal" a partir de Ns; a unidade A mede o quanto o perfil real difere em cada ação. É a maneira natural de transformar um perfil GFS em uma anomalia para colocar no mapa:

```math
-\Delta N = 7,32\,e^{0,005577\,N_s}, \qquad c_e = \ln\frac{N_s}{N_s + \Delta N}\ \ (\mathrm{km^{-1}}), \qquad N_{rif}(h) = N_s\,e^{-c_e (h - h_s)}
```

```math
\delta A(h) = N(h) - N_s\,e^{-c_e h}\qquad (\delta A > 0:\ \text{più rifrattività del normale a quella quota})
```

Com Ns = 313 você tem ΔN cerca de -42 N/km, que é o raio equivalente 4/3. O critério de conduíte −157 N/km também é derivado da lei Snell para um raio horizontal (eq. 4.23), quase independente da altura da antena.

4. Onde e quando nascem as condutas (Bean e Dutton § 4.4; Kerr cap. 3):

| Processo | Situação | Efeito |
| Atenção | ar quente e seco da terra que flui no mar mais frio (típico de manga de verão após dias de bom tempo); conta principalmente o déficit de umidade | canal intenso no mar |
| Irradiação noturna | céu sereno, vento fraco; nuvens baixas ou muita umidade o impedem | levou ao solo que nasce ao pôr do sol, cresce à noite e desaparece rapidamente após o nascer do sol, especialmente dentro |
| Subsidência | ar que desce do anticiclone, aquecido e seco, sobre o ar fresco e úmido; inundações frequentes nas montanhas e setores sudeste do anticiclone | camada super-refractiva ou ducto alto; quase sempre gradiente superior ao padrão em sentido inverso |
| Mar e terras | costa, circulação de algumas centenas de metros | oleodutos locais ao longo da costa |
| Contra a conduta | mistura: terra aquecida pelo sol entre o meio da manhã e da tarde, vento para o chão sobre alguns m/s em terreno áspero; Névoa noturna | probabilidade de conduta quase nada |

5. Dois novos ingredientes para experimentar. O movimento vertical: correntes ascendentes nas zonas de convergência removem e apagam as camadas, a subsidência fortalece o sinal (Moler e Holden: em média cerca de 2,5 dB mais do que com o ar ascendente, no mar nas bandas L e X). O Open-Meteo já fornece velocidade vertical nos níveis de pressão. E a tendência de pressão: em um caminho de 140 km para 186 MHz o sinal desceu quando a pressão caiu para o transmissor e para o centro do caminho e retornou alto e sem evanescência quando subiu, com apenas 2 (Bean e Dutton, fig. 6.9, p. 241).

6. De Kerr. Além da fórmula do comprimento de onda máximo (seção física de base), Kerr dá uma fórmula aproximada para M a partir da única temperatura, vapor e altitude (§ 3.9, com as constantes 79 e 4800 antigas então substituídas por 77,6 e 4810) e observa que para um corte de vento reverso pode gerar ondas que oscilam na altitude do ducto, possível causa de fugas profundas (§ 3.26). Fonte italiana relatada por Bean e Dutton e para pesquisar: L. Bonavoglia (Dezembro de 1958), "Correlação entre fenômenos meteorológicos e propagação..."

## 8. De apresentações de rádio amador e relatórios técnicos (segunda série)

Sete documentos fornecidos pelo autor do programa. Três servem diretamente ao índice: W3SZ para o troposcatter "chão", LA0BY para os casos que Hepburn não havia previsto, Hepburn (tradução LU7MC) para a classificação meteorológica dos ductos.

1. Troposcatter: o sinal que sempre existe. R. Rehr W3SZ, "Tropospheric Scatter (Troposcatter) Propagation for VHF, UHF, Microwave and Frequencies", Pack Rats Conference 2014; J. R. Duffey KK6MC, "The Troposcatter Propagation Mode and How to Predict Coverage on the VHF/UHF Bands", CSVHF 2017.

- Útil de cerca de 100 a 700 km, de 144 MHz a 10 GHz; a perda cresce de 30 logs f com frequência.
- Acima de tudo, conta o ângulo de partida para o horizonte: cada grau extra custa cerca de 9-12 dB ( M1CNK, método Collins).
- No verão, o sinal é, em média, cerca de 10 dB maior do que no inverno; no meio da tarde, cerca de 5 dB menor do que no início da manhã ou à noite. KK6MC indica uma variabilidade total de 13-15 dB com tempo, estação e tempo.
- Método Yeh, o mais fácil de programar (f em MHz, d em milhas, ângulo de dispersão em graus, α média geométrica das aberturas dos dois feixes):

```math
L = 30\log f + 20\log d + 10\,\theta - 0,2\,(N_s - 310) + L_c + 57, \qquad L_c \approx 2,5 + 1,8\,\frac{\theta}{\alpha} - 0,063\left(\frac{\theta}{\alpha}\right)^2
```

- Comparação de Larsen (1968) em 15 conexões reais: erro quadrado médio 5,1 dB para Collins, 7,4 para Yeh, 8,1 para NBS e CCIR, 16,8 para Rider. Todos os métodos melhoram se Ns mantém-se estável (Yeh cai para 5,4 dB): para a dispersão a refração do dia conta pouco, então o índice de tropo deve olhar para as camadas, não apenas Ns. A tabela de 15 conexões, com perdas medidas (incluindo Feldberg-Monte Baldo, 322 km a 957 MHz, 223,1 dB; Chipre-Israel, 319 km a 77,5 MHz, 163,3 dB), é uma boa bancada de teste para o nosso cálculo.
- KK6MC dá a perda de que uma estação típica pode exceder em 2 m e a distância correspondente, de 184 dB (FT-817 e 3 elementos em SSB, cerca de 130 km) a 215 dB (pontas longas em SSB, cerca de 430 km) e 236 dB (kW e yagi acoplados, cerca de 640 km). É a maneira de traduzir o índice dBs em milhas ganhas.
- Código pronto: W3SZ Programa Yeh e Collins (nitehawk.com/w3sz/YehCollins.zip) e calculadora Bob Atkins KA1GT (bobatkins.com/radio/troposcatter.html).

Outra fonte sobre a dispersão: I. Roberts ZS6BTE, "Propagação Troposférica de Dispersão", *Radio ZS*, dezembro de 1984, com nota de 2013 ([qsl.net/zs6bte](https://www.qsl.net/zs6bte/Tropospheric%20Scatter%20Propogation.htm)). Principalmente Geometria útil: ângulo de dispersão ω = ω0 δ1 δ2, com ω0 = d/R, ω1 = (h1 − h\_ostacolo)/d1 + d1/2 R e o mesmo para a outra estação, R = 8446 km (4/3 do raio da Terra). É a peça que é usada para calcular os ângulos a partir do perfil do solo no troposcatter "chão". Outros dados: cada ângulo em mais custos cerca de 10 dB; passar de Ns 280 para 300 reduz a perda de cerca de 4 dB; o volume de dispersão está entre 300 e 2000 m para 150 km, entre 600 e 3000 m para 300 km; O tempo no volume de dispersão eleva o sinal de cerca de 20 dB. o exemplo numérico do artigo não retorna com as fórmulas dadas (o prazo de espaço livre a 900 MHz e 872 km é cerca de 150 dB, não 125), por isso as fórmulas devem ser verificadas no W3SZ antes de usá-las.

2. Tipos de conduíte (W. R. Hepburn, "Modos de Propagação Troposféricos", dxinfocentre.com/propagação/tr-modes.htm, Tradução em espanhol de LU7MC). Complete a tabela de Bean e Dutton com casos úteis em Itália:

| Tipo | Quando | Nota para índice |
| Irradiação | noites calmas e pacíficas na terra | seguir o chão, desaparece pouco depois do amanhecer |
| Alta pressão (subsistência) | anticiclona | pode durar todo o dia; à noite o conduíte da irradiação abaixo pode bloquear sinais distantes, para que o dia possa ir melhor |
| Frente | na frente de uma frente quente, atrás de uma frente fria, ao norte de uma frente estacionária | curto; o mau tempo o perturba |
| Descendente de montanha | föhn, bora: ar descendo, aquecendo e secando | para tentar bora no Adriático e föhn sul dos Alpes |
| Atenção | ar quente e seco sobre o solo frio e húmido, por exemplo após a chuva | levou ao chão |
| Vale | ar quente sobre o ar frio pára no vale | pode ficar no dia |
| Marino | ar quente e seco em água mais fria; chega à costa com a brisa | pico à tarde; primavera e início do verão as melhores estações |

Os dutos de evaporação no mar (0-30 m) servem apenas cerca de 3 GHz e não entram no interior: para 144 e 432 MHz você pode ignorar. As montanhas altas são uma barreira; desertos são muito secos.

3. Os casos Hepburn não tinham previsto (S. Heck LA0BY, "Propagação Extrema Tropo em 144 MHz e acima", VUSHF Nordic 2019, de JO59IX a 500 m de altitude):

- 22 de novembro de 2018: 70 cm até UA6 (cerca de 2600 km) durante 6-8 horas. Hepburn indicou apenas os Estados Bálticos e a Polónia. O conduíte foi visto nos mapas de temperatura a 900 e 1500 m e terminou pouco acima de 2000 m; foi maior no lado norueguês, e apenas LA0BY na colina entrou. No tempo de terra não parecia um trope: frio e nevoeiro no topo.
- 11 de fevereiro de 2015: 2 m e 70 cm para 9A e S5 (cerca de 1500 km) através dos Alpes, também isso não previsto por Hepburn.
- 15 de outubro de 2011: 2 m com EA1 (2135 km) em dutos altos, em três trechos de mar e três terrenos.
- Outros eventos: 6-11 de novembro de 2003 a leste (2050 km), 10 de dezembro de 2004 a França.
- Endereços de campo: nevoeiro em picos simples, livres, pouco vento, chão molhado. Gradiente de temperatura normal –6,5 °C/km; num caso real ( VK3KAQ) +7 °C em 20 m.

Conclusão do autor: Hepburn e F5LEN não são suficientes, eles servem os perfis em altitude ao longo da rota. Para nós: estas datas são casos de verificação a serem recalculados com os dados históricos dos modelos, e o índice no caminho deve comparar a cota do ducto com a cota das estações.

4. Experiência italiana (G. IV3GBO, "Tipos de propagação VHF SSB/CW", 2017): realizada entre 450 e 3000 km, com camadas entre 450 e 3000 m; nevoeiro estendido como uma pista; aberturas por mar no Adriático para Puglia, Albânia e Grécia. Espalhe de 100 a 500 km.

5. Apresentações (P. Wilton M1CNK, "Uma Introdução à Propagação VHF/UHF", 2013): 70 cm são frequentemente melhores que 2 m; com 10 m de pólo a 144 MHz o ângulo inicial é de cerca de 3°, com 20 m aproximadamente 1,5°.

6. Relatório da NASA (E. Hirschmann, "Efeitos Troposféricos e Ionosféricos sobre a Radiofrequência (VHF-SHF) Comunicação", Goddard X-731-67-89, 1967): diz respeito a erros de distância para satélites, não o DX. Ele traz de volta a mesma fórmula N e perfil exponencial (por exemplo, na Flórida: Ns 370, ΔN −55, constante 0,161 km) e envia duas fontes NBS para pesquisar: Bean e Thayer, "CRPL Exponential Reference Atmosfera", Monógrafo 4 (1959) e Bean, Horn e Ozanich, "Climatic Charts and Data of Radio Refrative Index for the United States and the World", Monography 22 (1960).

## 9. Do livro VHF/UHF DX, Capítulo 2

I. White G3SEK (editado por), *The VHF/UHF DX Book*, DIR Publishing 1995 (ISBN 0 9520468 0 6; réplica digital 2017), capítulo 2 "VHF/UHF Propagation", pp. 2-13, 2-22-2-29 e 4-11. É o texto mais próximo da experiência radioamador entre aqueles coletados; confirma os limiares já utilizados e acrescenta novas regras sinóticas.

Número

- A mesma fórmula de N (77,6 P/T + 3,73·10^5 e/T2); exemplo: 1000 hPa, 7 °C, e = 10 hPa dano N = 324. Em uma atmosfera bem-formada, por exemplo, após uma tempestade, N é quase linear cerca de 39 unidades/km nos primeiros 2-3 km.
- Gradientes entre 0 e −79 N/km são normais, abaixo de −79 há super-refração, mais de 0 sub-refração. O livro insiste que o longo DX não vem da super-refracção simples, que deve permanecer em todo o caminho, mas dos dutos.
- Espessura mínima do canal, um pouco mais prudente que Kerr e Hepburn:

| Banda | Espessura mínima |
| 50 MHz |  400 m  |
| 70 MHz |  300 m  |
| 144 MHz |  200 m  |
| 432 MHz |  100 m  |
| 1.3 GHz |  50 m  |

- A razão não é proporcional ao comprimento de onda (cerca de 65 λ a 50 MHz, 220 λ a 1,3 GHz) porque as bordas do conduíte são "divididas". No Reino Unido, os gasodutos são frequentes em 144 MHz, mas quase nunca em 70 MHz ou abaixo, e muitas vezes os 432 MHz são fortes quando os 144 são fracos.
- Em pipeline o sinal cai como 1/d em vez de 1/d2: até 1000 km a atenuação pode ser semelhante à do espaço livre.
- Capacidade de uma estação mínima em 2 m (100 W, 15 dBi yagi, 2 dB ruído): 217 dB, ou seja, cerca de 400–500 km em SSB em condições médias; em CW cerca de 20 dB em mais (p. 4-7). Concordo com a tabela KK6MC.
- Dispersão: a perda aumenta cerca de 15 dB por triplicar a frequência e 10 dB para cada grau de ângulo de dispersão ( fórmula Yeh; no texto extraído a constante é ilegível, por isso deve ser retirado da versão do W3SZ). A dispersão "on-go" de células convectivas cobre rotineiramente até cerca de 500 km, 1000 km com boas estações. Depois de fortes chuvas e condições prolongadas são escassas em todas as bandas.

Tipos de condutas

- No solo: na terra propagam-se mal porque o solo absorve e espalha-se; no mar calmo chegam longe (Scily-Canarie, 2500 km, cerca de uma vez por ano em 2 m). Os dutos de evaporação têm frequentemente apenas cerca de 15 m de altura e servem microondas.
- Alta: entre 450 e 2000 m, devido ao maior DX no solo; o sinal "alto" acima das estações abaixo. Os dutos "perdem" dão sinais menos fortes, mas também permitem estações de distância intermediárias.
- Os conduítes têm direções precisas e podem durar horas ou dias, ao contrário do E esporádico.

Novas Regras Sinópticas para o Índice

- Idade do anticiclone: o ar cai para o centro de alguns metros por hora e o reverso é reduzido à medida que a alta pressão se desenvolve; as aberturas maiores chegam após vários dias de propagação já bom. Um anticiclone de curto prazo não é suficiente.
- Borda de alta pressão: a camada é cúpula e bordas é menor, para que as estações melhorar.
- Direcção: Observa-se que as rotas mais longas percorrem os isobares ou os dorsais (Flavell G3LTP, 1984), mas há excelentes conexões também na direção radial do centro. Exemplo de 29-30 de agosto de 1987: da Inglaterra a abertura girou 180° em dois dias (Espanha, França, Itália, Áustria, para a Noruega) após a mudança do anti-ciclone.
- Contra: isobare espesso (vento forte), nuvens estratocumuliformes na borda da camada (limitar a maior frequência).
- Frentes: uma frente fria empurra o ar quente para cima e cria um reverso ao longo da frente; a melhor direção é ao longo da frente enquanto passa, mas dura pouco.
- Temporada: No Reino Unido os anticiclones são mais frequentes no final do verão e início do outono.
- Pistas visuais: fumo preso em uma linha escura acima do horizonte; com alta pressão, acumulados de camadas com base abaixo de 1000 m indicam um ducto de terra, base acima de 1000 m um ducto de alta.

Referências citadas à pesquisa: R. G. Flavell G3LTP, "Estudos de um Evento de Propagação Anticiclônica Extensa...", * Radio Communication*, Fevereiro de 1984, p. 128; J. Gannaway G3YGF, "Tropospheric Scatter Propagation", * Radio Communication*, Setembro de 1981, p. 710; P. W. Sollom G3BGL, "A Little Flutter" W6ZGN

## 10. Um modelo de rádio amador verificado: Biancomano e Leyshon (1998)

V. J. Biancomano, S. W. Leyshon, "Practical Tropospheric Model for VHF/UHF Propagation Prediction", *National Weather Digest* 22:2, Junho 1998, pp. 15–24, gratuitamente em [PDF from the National Weather Association](https://nwafiles.nwas.org/file/nwafiles/digest/papers/1998/Vol22No2/Pg15-Biancomano.pdf).

- Dados: radiosundações das 00 e 12 UTC de 47 estações no leste dos Estados Unidos, 1995-1997; análise do solo para 700 hPa (no início até 400), no perfil de N, sem M. Excluindo costas, ilhas e montanhas.
- Para cada levantamento: presença e cota do ducto, comprimento de onda máximo que pode levar (frequentemente cerca de 100 vezes o comprimento de onda), gradiente máximo da temperatura potencial entre os níveis vizinhos e entre um nível e o segundo acima (para camadas mais espessas). As condutas de terra não são utilizadas.
- Primeira regra, persistência: os índices de estabilidade (índice elevado, K, totais-total, SWEAT) permanecem dentro de 3 unidades da pesquisa 12 horas antes, e o nível de pressão onde a temperatura potencial cresce mais permanece aproximadamente o mesmo. Em caso afirmativo: previsão de melhoria nas próximas 12 horas.
- Segunda regra, controles dinâmicos: camada limite que não sobe, CAPE, variação da espessura para 500 hPa em três inquéritos, variação da inclinação dentro dos limites.
- Limiar da banda: um conduíte capaz de transportar 100 cm ou mais indica abertura em 432 MHz, quase certo em 222 e muitas vezes até mesmo em 144.
- Filtro de espaço: estações em uma grade hexagonal de cerca de 400-500 km; a previsão só vale se pelo menos 5 estações contíguas forem positivas (se você puder pular uma). Com 5 estações a probabilidade de um falso positivo aleatório torna-se insignificante.
- Verificação contra os eventos da coluna "O Mundo Acima de 50 MHz" do QST: em 1996, 48 eventos planejados em 67 (71%); 12 dos 15 grandes eventos. Precisão de até 70% nos 432 MHz e cerca de 60% nos 144 MHz às 12 horas; fornece quase o dobro dos eventos reais; 9 de 19 erros são previsões chegaram tarde, porque muitas melhorias se formam em menos de 12 horas.
- Adicionado às estações mais ao norte: o vento girando no sentido horário escalada (início quente) como condição extra.

A partir do PDF completo (fornecido pelo autor do programa) mais três detalhes. A sua regra "grossa = 100 vezes comprimento de onda" dá 600 m para 6 m, 200 m para 2 m, 125 m para 1,25 m e 70 m para 70 cm (fig. 3): para os 70 cm é menos grave do que Kerr e Hepburn (cerca de 89 m), para os 2 m coincide com o Livro DX VHF/UHF. O vento que gira no sentido horário sobe, segundo os autores, especialmente porque traz ar mais quente à altitude, que é o que faz a queda de N crescer com pressão; a umidade que traz consigo não parece desestabilizar. O apêndice obtém dN/dh em forma fechada para uma atmosfera de gradiente térmico constante com um salto agudo de gradiente de calor e umidade em uma determinada cota: útil como um caso de teste para o nosso código. Seu traço da inclinação da temperatura potencial vem de K. H. Jehn, "O uso do índice de refração potencial na meteorologia de rádio em escala sinótica", *J. Meteorology* 17, 1960, p. 264.

O que tomar para o nosso índice: (1) um termo de persistência: comparar o perfil com o de 12 horas antes, que na previsão horária temos livre; (2) o filtro de espaço: um ponto conta apenas se a melhoria cobre uma área de algumas centenas de km, o que reduz falsos alarmes; (3) índices de estabilidade, calculados de temperatura e orvalho para 850, 700 e 500 hPa (Open-Meteo já dá índice elevado e CAPE); (4) o vento que gira com a altitude entre 1000 e 850 hPa; (5) seu método de verificação, que é o mesmo que queremos fazer com os anúncios do programa.

Outra fonte no mesmo pacote, de pouco uso para nós: P. K. Rastogi, "Estudo da estratosfera e troposfera (S-T) em Millstone Hill", NASA 1984 (documento 19840019069): radar de 440 MHz que mede a turbulência do ar claro entre 4 e 25 km, com camadas finas ligadas às ondas de gravidade e ao corte de vento. Confirma que a turbulência que produz a dispersão está em camadas, mas diz respeito a probabilidades demasiado elevadas para os dutos.

## 11. Terceira ronda de fontes (20 de Novembro)

Casos históricos de conduta alta. J. Macoun OK1VR, links em 2 m da cúpula de Sněžka (1602 m), outono de 1958, artigo de 1959 traduzido em 2006 ([ok2kkw.com](https://www.ok2kkw.com/snezka/ok1vr_1_en.htm)). Em 27-28 de outubro de 1958, a primeira ligação Tchecoslováquia-Inglaterra em 145 MHz ( G5YV, 1220 km) e muitos mais entre 1100 e 1250 km, com sinais estáveis após 23. O autor atribui-o a duas inversões sobrepostas, as mais altas cerca de 2200 m, com o sinal retornando ao solo apenas onde a camada inferior se dissolve; condição favorecida por um anticiclone lento do Atlântico para o continente. A partir de casa (Praga) seu máximo foi de 530 km: apenas as estações em altitude entraram no conduto. Trata-se de outro caso de verificação (como o LA0BY 2018) e confirma que o índice deve procurar por mais de 2000 m e compará-lo com a participação da estação.

Bibliotecas históricas NBS (útil para procurar obras clássicas, não para fórmulas):

- W. Nupen, *Bibliografia sobre Propagação Troposférica de Ondas de Rádio*, NBS Technical Note 304, 1965, 356 páginas, com índice para tópicos (também "previsão condição") e por país. As entradas italianas reportaram: L. Sacco, "The radio connection in atmosphere super-refraction regime", *High Frequency* 24(6), dezembro de 1955 (investigação italiana: super-refraction under 500 m frequent dia and night); S. Silleni, *Annali di Geofisica* 8(1), 1955; J. M. Clara e A. Antinori, prova tra Sardegna e Minorca da 238 a 297 MHz (390 km), *Eletrical Communication* 33(2), 1956. A. F. Clarke (Fort Huachuca, 1956) emitiu diariamente a previsão do comprimento de onda máximo preso para a manhã seguinte, a partir de umidade, temperatura, pressão e previsão do vento.
- A. L. Abbott, *Bibliografia de Tropospheric Radio Wave Scattering*, NBS, material até janeiro de 1960, cerca de 670 itens encomendados por ano: útil para a parte troposcatter.

Divulgação e observações: F. W. Odenthal DK5EQ, "Ausbreitung 2m – Eine Einführung" (31/05/2024): quase óptico até 200 km, tropo por ducto ou refração além, com alta pressão e na primavera e outono; cita a página VHF do DM2HB e as notas do DK5YA sobre tropo e Es. A. Johnson VK3DXE no blog do EI7GL (Dezembro de 2021): com WSPR VHF caminhos de milhares de km na Austrália foram descobertos; O trope não é apenas verão. Discussão sobre a marina VHF e AIS ([cruisersforum](https://www.cruisersforum.com/forums/f13/vhf-and-ais-radiowave-propagation-and-vhf-and-ais-radio-range-149499.html)): escopo 1,23·√h(ft) milhas náuticas por antena, dispersão até cerca de 50 nm, melhoria típica de tropos 100–400 nm; os conduítes são reconhecidos porque as estações distantes vêm tão fortes quanto os vizinhos. Chelmsford ARS News, Dezembro de 1983: nas competições em 2 m as estações da costa oriental inglesa são favorecidas pela rota marítima para o continente.

ARRL, *The Radio Amateur's VHF Manual*, 1972, cap. 2 "Propagation Beyond the Horizon" (pp. 17-19). Dicas de operação já conhecidas (seguir áreas de alta pressão que se movem lentamente e acima de todas as suas bordas traseiras; início do outono o melhor período; áreas costeiras, grandes lagos e vales dos rios favorecidos pela estratificação). Dois novos dados: (1) fig. 2-4 compara a atmosfera padrão (umidade relativa 70% até 12000 pés, refração mínima) com um raio típico de refração forte, com uma relação clara de quebra de temperatura e mistura de cerca de 4000 pés (cerca de 1200 m): é a assinatura que o nosso cálculo deve reconhecer nos níveis GFS; (2) dutos verdadeiros são raros abaixo de 144 MHz e mais frequentes em UHF, de acordo com espessuras mínimas. Bibliografia QST útil para pesquisar: Hull, "Air-Mass Conditions and the Donder of UHF Waves" (Junho de 1935, Maio de 1937); Hoisington, "Painless Prediction of Two-Meter Openings" (Outubro de 1949); Collier, "Upper-Air Conditions for 2-Meter DX" (Setembro de 1955); Botts, "A Night to Remember" (Janeiro de 1970, realizado em terra).

PA0EHG, página "Tropo" ([pa0ehg.com](https://pa0ehg.com/tropo.htm)): resumo do troposcatter do artigo G3YGF, já no manual. Use duas regras práticas para controlar nosso cálculo de dispersão: cerca de 10 dB de perda extra para cada ângulo no horizonte de cada estação, e mais de 100 km aproximadamente 9 dB em mais a cada 100 km de rota (60 milhas); o volume comum começa de cerca de 600 m em 100 km e de cerca de 9 km em 480 km. Não trata condutas, sondagens ou limiares de refração.

PA0EHG, calculadora PROPBER ([página](https://pa0ehg.com/propagat1.htm), programa propber.exe fornecido pelo autor do programa). É um programa DOS de 36 kB escrito em BASIC e compilado com o Microsoft BASIC Compiler 5.60 (1985): ele só é executado sob DOS ou DOSBox. A partir dos textos internos: pede frequência, distância (ou os dois localizadores, também no antigo formato QRA "E-localizador"), para cada estação de altura da antena, figura de ruído, banda FI, potência, obstáculo (altura e distância) e ganho de antena (0 = cálculo do diâmetro da parábola, eficiência 50%). Retorna a distância máxima em óptica, a perda óptica correta para a absorção de vapor (0,1 dB/km de 24 GHz), a perda de troposcatter "correta para os locais" (ou seja, para os ângulos do horizonte dados por obstáculos) e a relação S/N nos dois sentidos, também "com a propagação anormal máxima". As constantes numéricas não são legíveis no executável compilado. O método sugerido para bandas altas também é útil para nós: calibrando o cálculo em um link medido (diferença entre medido e calculado S/N) e aplicando a mesma diferença em outra banda. É o mesmo padrão do nosso cálculo, mas usa o perfil S RTM1 em vez de apenas um obstáculo e a previsão GFS em vez de uma margem fixa.

PROPBER, constantes lidas no executável (números no formato Microsoft Binary Format). πr, na tabela de dados ao lado dos textos: 8.68589 (= 20/ln 10, ou seja, 20 logs calculados com o logaritmo natural), 32.5 (espaço livre: 32,5 + 20 log f\[MHz\] + 20 log d\[km\]), 24000 e 0,1 mais 40000 e 0,3 (vapore: 0,1 dB/km de 24 GHz, 0,3 dB/km de 40 GHz) Provavelmente, porque você repete, mas dentro do código: −69,5 (o mesmo termo constante que a fórmula lateral de Yeh em VHF/UHF DX Book), 332 (refracção de solo), 8733 km (radio equivalente, k.1,37). A fórmula exata de dispersão não é reconstruível sem desmontar o programa. T0

PROPBER, fórmulas reconstruídas pelo código (desmontagem da parte compilada; as operações são chamadas para a biblioteca BASIC com os operadores em memória, cujos valores em dupla precisão foram decodificados). As constantes −69,5, 332 e 8733 encontradas anteriormente eram falsos positivos dentro da biblioteca.

- Distância óptica: d = 5 · (?h1 +?h2) km (h em m). É mais generoso do que o clássico 4,12 (k = 4/3): é equivalente a k 2.
- Perda óptica: L = 32,5 + 20 log f\[MHz\] + 20 log d\[km\], mais 0,1 dB/km acima de 24 GHz e 0,3 dB/km acima de 40 GHz.
- Troposcatter: Lts = L + 23 + 10 log f + 0,0674·d, ou seja, 55,5 + 30 log f + 20 log d + 0,0674·d (formula de G3YGF com a constante 2 dB mais elevada). Sem termo de ângulo ou reatividade.
- Correção "para os sites": o programa pede obstáculo e distância, mas o bloco que deve usá-los está vazio; a variável de correção permanece 0, então a perda "correta" é igual à perda de dispersão.
- Ruído: T = 290·(10^(NF/10) − 1), N = −228,6 + 10 log T + 10 log B (dBW), sem temperatura da antena.
- S/N = 10 logs P + G\_tx − L + G\_rx − N. "Com propagação anômala máxima" usa perda óptica em vez de dispersão, ou seja, o caso de conduíte ideal sem perdas além do espaço livre.

Comparação: 300 km e 144 MHz PROPBER dá cerca de 189,9 dB, G3YGF 187,9, nosso P.452 187,8 (com ângulos de horizonte zero): a dispersão coincide dentro de 2 dB. Nosso cálculo adiciona os ângulos do horizonte a partir do perfil, a temperatura da antena, e em vez do "máximo" fixou a camada esperada com acasalamento residual e atenuação.

Rohde & Schwarz, P. Denisowski, "Compreendendo Propagação VHF", Nota Educacional, abril de 2023 (24 páginas, divulgação). Confirma os pontos já coletados (conduzidos como guia de onda bidimensional, sinal que cai de forma linear com a distância, até 1500 km e além, duração de horas a dias; bom terreno no mar e escasso em terra; elevado de algumas centenas de metros para alguns km). Duas observações úteis para o índice: (1) evolução no tempo: um conduíte recém-formado muitas vezes carrega apenas as frequências mais altas e, espessando-se, começa a trazer até mesmo as menores; a previsão horária deve, portanto, mostrar os 432 MHz "com antecedência" nos 144 MHz quando a camada cresce; (2) localização das estações: as melhores condições estão entre as estações próximas às extremidades do conduíte, enquanto aqueles ao longo do caminho de um conduíte alto muitas vezes não conseguem entrar, a menos que conduzido "quem perde". As condutas movem-se com o tempo atmosférico.

Site PA0EHG, outras páginas. Para o trope, PathProfile de M. Willis G0MJW ([scheda](https://pa0ehg.com/srtmp.htm), [download](http://www.mike-willis.com/Software/ASTERPathprofile.zip)): perfil da rota de perda total esperada e arquivos de quota, julgado confiável para o troposcatter em microondas; não trata a dispersão de aviões ou chuva. Útil como segunda referência para comparar nossos perfis e perdas do S RTM1. O resto do site abrange EME, microondas, pista de pouso (AirScout, mapas SM7LCB) e medidas em 80 m.

PathProfile by G0MJW (Mike Willis), pacotes ASTER e SRTM e SiteData (fornecido pelo autor do programa; [](https://www.mike-willis.com/software.html)página de software). Programa Windows de 2018: perfil da ASTER ou SRTM para 1" ou 3", perdas em óptica, difração, troposcatter (válido até 1000 km), ducto e gasoso, com porcentagem de tempo, k mediana da rota, fração de caminho no mar e distância da costa: a estrutura é a de P.452/P.1812, mas a ajuda (arquivo.chm comprimido) não poderia ser lida aqui. SiteData converte os localizadores e dá a parte de GT OPO30.

A parte mais útil são os arquivos de dados incluídos: ITU-R P.453 mapas estatísticos em grade global de 1,5° (121 fileiras de 90°N a 90°S, 241 colunas de 0° a leste): gradiente dN/dz nos primeiros 65 m excedidos por 1, 10, 50, 90 e 99% do tempo; para dutos de solo porcentagem de presença, espessura e intensidade; para aqueles de alta presença, participação de base, espessura, intensidade e altitude máxima; além do termo úmido e temperatura. Valores anuais interpolados:

| Localização | Oleoduto: % / espessura / intensidade | Oleoduto alto:% / base / espessura / intensidade | dN/dz excedeu 10% do tempo |
| Nápoles |  15% / 126 m / 15 M  |  18% / 786 m / 222 m / 10,7 M  | −380 N/km |
| Tirreno (40N 12E) |  20% / 118 m / 15 M  |  18% / 763 m / 228 m / 10,9 M  | −674 N/km |
| Trapani |  26% / 113 m / 16 M  |  19% / 767 m / 234 m / 11,1 M  | −520 N/km |
| Brindisi |  13% / 131 m / 15 M  |  17% / 832 m / 209 m / 10 M  | −405 N/km |
| Adriático (43N 15E) |  12% / 113 m / 13 M  |  13% / 917 m / 203 m / 9,5 M  | −399 N/km |
| Milão |  9% / 78 m / 9 M  |  6% / 1017 m / 178 m / 7,5 M  | −172 N/km |
| Udine |  10% / 78 m / 9 M  |  6% / 1079 m / 175 m / 7,6 M  | −176 N/km |

Para o nosso índice são uma referência climática pronta: espessura e intensidade típicas dos ductos em vez dos 50 m fixos do ducto potencial, e uma anomalia "hoje em relação ao normal" por zona. Dados da ITU: gratuitos para uso não comercial, para verificar antes de incluí-los em uma versão pública. O arquivo ZO NES01.DEM (58 MB, grade 2') contém as zonas climáticas interior/costa/mar; o formato não está documentado e eu não decodificado. A página de equações de Willis contém apenas constantes físicas, espaço livre e ganho de antena.

Quarta ronda de fontes (20 de Novembro).

Indermuehle e Suzuki 2026, predição dos gasodutos GFS para o observatório CSIRO de Murchison (arXiv 2608.12861): é a fonte mais próxima de LIVETROPOVHF. Perfis de N do GFS a 0,25° (superfície a 2 m, em seguida, os níveis isobáricos, aqueles sob o solo descartado); para cada coluna mínima de dM/dz abaixo de 3 km, base e topo da conduta, déficit integrado de M; em seguida, P.452-16 e P.1812-6 com GLO-90 25 rotas de 98 a 1005 km, de 88 MHz a 2,68 GHz, a cada hora. Resultados: AUC mediana 0,72 em 25 canais; a porcentagem de tempo de P.452 p deve ser usada como constante de calibração (escolha p = 20%), pois o modelo ITU com ΔN instantâneo não é definido dentro de um conduto; a habilidade permanece quase plana até 3 dias de antecedência. Em 162 MHz (AIS) as rotas recebidas têm a maior fração de conduíte de 67% (0,26 versus 0,16) e a maior profundidade de ducto de 49% (28 versus 19 M) em relação aos controles: no mar o conduíte é quase sempre, decide o trecho de terra. O GFS vê metade do tempo no solo em comparação com ERA5 (4,5% versus 10,4% das células-horas), enquanto no mar vê mais (73% versus 56%). Um índice de "corredor" (oleodutos contínuos ao longo da rota, camadas com uma espessura mínima de 40 m) melhora o resultado: confirma que a continuidade ao longo da rota é o critério certo.

Longley e Rice, ESSA ERL 79-ITS 67 (1968): método para terrenos irregulares de 20 MHz a 20 GHz (após ITM), com irregularidades no solo Δh, Ns de 250 a 400, raio equivalente 8497 km (4/3), horizontes, difração, dispersão para a frente e variabilidade no tempo e nos locais para o clima. Referência estatística, não instantânea.

ITU-R P.834-2: curvatura do raio, raio da Terra equivalente k·a, M = N + h/a (h em m, em milhares de km), modelo exponencial; nada novo do que já foi utilizado.

AGARD CP-70 (1970), propagação troposferical: camadas finas de 1 a 100 m com gradientes fortes são comuns; o coeficiente de reflexão de uma camada alta é aproximadamente proporcional ao comprimento de onda, com dispersão aleatória de cerca de 14 dB. Medidas para 90 MHz: diminuição de 9-10 dB ligada a um salto N concentrado em cerca de 350 m, no topo de uma camada de nuvens. A passagem de uma frente fria elimina as camadas: permanece a difração.

Relatório URSI dos EUA, Comissão 2 (1960): espessura média das camadas 60 a 210 m de altura (dado consistente com P.453), gradiente mínimo para o ducto 157–160 N/km.

Buscemi K2OVS, guia de propagação VHF: distâncias típicas para reversão e ducto para banda, dutos ligados a alta pressão estacionária, máximo ao amanhecer.

Wolinsky-Mancini e outros 2024, AIS nas Ilhas Canárias: rádio AIS e recepção; os dutos são muito mais frequentes nos meses quentes (inversão dos aliseanos).

Artigos nigerianos e malaios (Ekah, Iloke, Mat, Igwe): correlações locais entre temperatura, umidade e perda medida, fraca e contraditória (r -0,09 a 0,27); útil apenas como confirmação de que os tamanhos do solo por si só não são suficientes.

Herben 1984, tese TU Eindhoven (microonde): 30 GHz em 8,2 km e satélite OTS em 11,6 GHz; brilho para turbulência, multi-caminho e chuva. Para micro-ondas, as perdas dinâmicas são dominadas pela chuva e turbulência, não por dutos.

Baars, A Antena Refletor Parabolóide (microonde): ganho de uma parábola G = η·(πD/λ)2 com eficiência típica 0,5–0,65; perda de erros de superfície de acordo com Ruze, ηs = exp(−(4πε/λ)2). Ajudaria se adicionássemos as parábolas como uma alternativa ao Yagi DL6WU para 1296 MHz e além.

Já analisado ou de conteúdo pobre: Hirschmann NASA X-731-67-89 (duplo), radar VHF ST de Reid (ventos e turbulência, sem condução), slide Scribd e notas genéricas sobre a troposfera.

Quinta rodada de fontes (20 de novembro, noite).

Rogers 1997, Radio Science 32(1): Estimativa bayesiana dos parâmetros de um ducto elevado (taxa de base, déficit de M, espessura) a partir de fatores de propagação medidos além do horizonte, com um modelo de equação parabólica; experimento VOCAR na Califórnia em 143, 263 e 375 MHz, comparação com onda de rádio. O sinal depende muito da base e do déficit M (correção de 0,81 a 143 MHz para o déficit), quase que a partir da espessura (−0,17): a espessura não pode ser obtida a partir do sinal. Para nós: o cálculo B (espessura climática) conta menos que o déficit e a participação; e o método é a base para o back office, ou seja, obter o conduíte a partir das múltiplas frequências.

Moler and Holden 1960, "Tropospheric scatter propagation and atmospheric circulations", J. Res. NBS 64D, pp. 81 e seguintes (leia-se em original em 22/11/2026, doi 10.6028/jres.064d.014). Rotas marítimas da Califórnia para Point Loma (San Diego): Santa Barbara 190 milhas, Point Mugu 144, San Clemente 78; 1365 MHz e 9380 MHz, não VHF; Janeiro-Abril 1958, média de 1 minuto. O caso de 26 de março: um reforço de cerca de 20 dB por cerca de 3 horas em direção ao meio-dia, quando um vórtice ciclônico enfraquece e começa a brisa e a subsidência do mar; em 15 a convergência é reformada e o sinal cai cerca de 22 dB do máximo. Estatísticas (Tabela 1, 500 hPa, 33 casos): com ar que cai o sinal médio é maior que cerca de 2,5 dB do que com ar subindo, na banda L e X. O sinal segue a diferença calculada para 500 hPa no sinal e amplitude das variações, sem um coeficiente de correlação. Camadas baixas (menos de 600 m) favorecem a reflexão parcial sobre espalhamento. Os anticiclones e frontes não são tratados, e as "excursões até 60 dB" mencionadas acima não são: vieram de um resumo em segunda mão. Para nós: o movimento vertical é um bom preditor, e +1,5 dB para a subsidência e -2 dB para o ar ascendente são da mesma ordem que a diferença média medida (2,5 dB); 20 dBs são um único caso, não um valor típico, e em microondas.

Banafaa e Muqaibel 2025, IEEE Access: levantamento dos oleodutos (modelos de evaporação, equação parabólica, estimativas de radar, GNSS e lidora) e classificação com aprendizado automático em medidas reais; Máquinas vetoriais de suporte dão os melhores resultados. Útil como uma bibliografia; os dados são do Golfo Pérsico.

ITU-R P.834-7 (2015): versão atualizada da recomendação sobre refração troposferal; nenhuma fórmula nova do que as já utilizadas.

Abbott, NBS bibliografia sobre a dispersão troposferosa (GOVPUB- C13): já na terceira rodada. Slide Scribd nas condutas: conteúdo genérico. O artigo ScienceDirect 002191698790030304 não foi possível abrir.

Sexta rodada (20 de novembro de noite): Bean e Dutton, ARRL Handbook 2022 e 2023, Balanis.

Feijão e Dutton, Radio Meteorologia (cap. 6), partes ainda não utilizadas. Sensibilidade do campo aos Ns: 0,18 dB por unidade N no ciclo anual; 0,14–0,24 dB/N no ciclo diurno (máximo no início da manhã), até 0,2–1,1 dB/N nas rotas de 175–200 km no inverno; correções 0,8–0,95. Parâmetro composto por Misme: M = a(g\_e − 40 + b·ΔW^n) com a = 0,5 dB por N/km, que combina o gradiente equivalente e a estabilidade térmica ΔW (trabalho para levantar o ar de 1 km): no Saara a 471 MHz em 160 km a única estabilidade é 20 dB entre janeiro e junho em gradiente quase igual. índice de refração potencial K (N reportado adiabaticamente a 1000 hPa): destaca as massas de ar e o subsídio anticiclônico (Flavell e Lane).

ARRL Handbook 2022 (99a ed.), § 19.4.3-19.4.4. Inversões úteis entre 250 e 2000 m; inversões de radiação (sera, ar calmo, solo seco) raramente levam verdadeiro; anticiclones lentos dão os dutos mais extensos por subsidência, com inversão entre 500 e 3000 m, mais forte à noite e de manhã; no hemisfério norte as melhores rotas estão ao sul do centro alto. Frente quente: boas condições algumas centenas de km à frente; Frente fria: forte mas instável inverter atrás da frente, paralelo. Foehn (também nos Alpes) e brisa terrestre (ar fresco no chão, quente a 200-300 m; estende-se mais de 800 km ao longo da costa). Scirocco: ar quente e seco do Saara sobre ar marinho mais fresco, inversões muito fortes do Mediterrâneo oriental para Gibraltar, provável causa de registros mais de 1500 km em microondas. Troposcatter: limite teórico de cerca de 800 km, metade típica máxima. A 100a ed. (2023) tem um texto mais curto: inversões ao nascer do sol e pôr do sol até +20 dB e +20-50% de vazão; 1200 milhas nos Grandes Lagos, típico 400-600; recomenda Radio Mobile (modelo ITM) para cobertura de Hepburn e mapas.

Balanis, Teoria da Antena (4a ed.), § 10.3.3: Tabela de Viezbicke (NBS TN 688, 1976) para Yagi otimizado a 400 MHz: boom 0.4 / 0.8 / 1.2 / 2.2 / 3.2 / 4.2 / 4.2 λ → 7.1 / 9.2 / 10.2 / 12.25 / 13,4 / 14.2 dBd. Nossa fórmula DL6WU (8.6 + 8 log10 boom/λ) dá 1–1.7 dB menos entre 0.8 e 4.2 λ e retorna em linha com as tabelas DL6WU para 9.5 λ: estimativa prudente. Com a fórmula correta no original (9,4 + 7,8 log10 boom/λ, a partir de 22 de novembro) o descarte cai abaixo de 0,2 dB entre 1,2 e 4,2 λ (12,1 dBd para 2,2 λ versus 12,25) e permanece 0,6 dB para 0,8 λ.

Sétima rodada (21 de novembro): QST, QEX, W3SZ, repositório GitHub.

QST 2000-2025 (14 números, 10 camas com reconhecimento de texto). Apenas reporta sobre o World Above 50 MHz, útil como casos para validar o modelo: 5-9 de setembro de 2000, realizado em 144 MHz e mais além na metade oriental dos EUA; 14-15 de agosto de 2022, abrindo Manitoba-Kansas/Missouri sob alta pressão em um dia quente, de 144 a 1296 MHz quase tudo no FT8, registro canadense em 10 GHz de 1228,56 km ( VE4MA – N0OY) no Q65, sinaliza até 1941 km; APRS mapa de propagação mostrou a mesma área. QST Novembro 2022 recomenda mapas de Hepburn. Nenhum artigo técnico sobre a previsão de tropo.

QEX e K1JT, Franke e Taylor (QEX 2020) nos modos FT4/FT8: limiares de decodificação FT8 −20,8 dB em 2500 Hz no canal sem evanescência, −18,6 dB com evanescência em latitudes médias; FT4 −17,5 dB. Os outros números de QEX 2023 e 2026 não tratam o tropo (três números 2023 são exames não lidos). W3SZ 2019: limiares de modo lento ( JT4A −23, JT9A −27, JT65A −25, QRA64A −26, FT8 −21 dB) e critérios de escolha entre sensibilidade e tolerância ao deslocamento de frequência.

GitHub. TropoLink (LukiBox, Apache-2.0): desenho de rotas de troposcatter com P.617-5 (com N0 e ΔN), NBS TN101 e ITM flanqueados, e sua diferença mostrada como incerteza; P.617-5: L = F + 22 log f + 35 log ω + 17 log d + Lc, F = 0,18· N0 ·e^(−hs/7,35) - 0,23·ΔN, Lc = 0,07·e^(0,055(Gt+Gr)). Andi-f/troposcatter ( DL2FZN 2013, do Manual VHF/UHF RSGB): Lts = 24,44 + 4,283 ln f + 0,06656 d acima do espaço livre, declive perto do PROPBER (0,0674 dB/km). QRadioPredict: ITM e ITWOM 3.0 com S RTM3, uso do solo Corins e diagramas NEC. Não relevante: Tropos (aplicativo meteorológico), opera-adt/tropo e troposim (atraso troposférico para InSAR), tgbot\_troposcatter (estação militar), sdr-troposcatter-gui (painel de controle), Troposcatter-Propagation-Delay (atraso ERA5), Radio-Communication-Project (simulador didático), PyLayers (canal).

Oitava volta (21 de Novembro): modelos NTIA e ITU no GitHub. NTIA/itm (Longley-Rice, ITS): 20 MHz–20 GHz, perda de espaço livre, difração e troposcatter com perfil de solo, terminais de 0,5–3000 m, reatividade de superfície N0 (minimidade reduzida ao nível do mar, 250–400), sete climas, constantes de solo e vários modos 1 visão, 2 difração, 3 troposcatter; C++ com.NET Framework 4.7.2 gabinete. Não modela os dutos: serve como referência "a atmosfera normal" com variabilidade estatística. NTIA/p528 e eeveetza/p528 (MATLAB): ITU-R P.528-5 para ligações ar-solo, 100 MHz–30 GHz, 1,5–20 000 m terminais, p 1–99%;.NET 4.8 invólucro. Possível utilização: balanço de dispersão de ar (duas rotas terrestres – aeronave mais secção radar). eeveetza/p2001 (MATLAB): ITU-R P.2001-6, modelo de campo completo com difração, troposcatter, propagação anormal (conduta e camadas, Lba) e E esporádico, porcentagem de tempo 0–100%; mapas digitais ITU (DN\_Median, DN\_SupSlope, DN\_SubSlope, dndz\_01, TropoClim, h0, surfwv, Es) não são redistribuíveis. Possível uso como comparação ITU em vez de P.452, com o GFS ΔN em vez de mapas como no estudo CSIRO; os mapas devem ser baixados pelo usuário de acordo com P.453. Validação 1.3.0 de 593 QSO (29/12/2021–2/1/2022): separação evento/comparação 0,79 sem correções, 0,91 com Misme e subsidência; o GFS não vê o conduto no mar a 144 MHz (guadagno sem correções nunca mais de 15 dB).

Nona ronda (21 de Novembro): IU3QEZ, NTIA, PyWaveProp, ACE High, validação 1.3.1. IU3QEZ (S. Fabris), apresentação "Over the horizonte in VHF" (GEKO Magazine n. 69, Setembro 2026): volume comum d2/(8·R\_eff) com R\_eff □ 8500 km (1,3 km a 300 km, 3,7 km a 500 km cada nível de horizonte no valor de 69 milhas (111 km), margem de 7 dB para evanescência e 4 dB para reflexão no solo; Tese: 10 dB é de cerca de 100 km; estação IQ3QC 5 W com 3×5 elementos, 120 QSO, máximo 788 km (Alpe Adria 2026). Bibliografia da apresentação a recuperar: Norton 1959 (J. Res. NBS 63D, System Loss), Bray 1961, ARRL Antenna Book 2023 fig. 4.29, Rice-Longley-Norton-Barsis NBS TN 101, Bean and Thayer 1959, Barsis and Kirby 1961 (diffration on ridge), Deygout 1966, P.1812, AirScout DL2ALF, P.834 NTIA PropLib (wiki): eHata, ITM, LFMF, P.528, P.2108 (lose de encumbrance), com C++,.NET, MATLAB e Python; nenhum modelo de conduíte; página ITS: também IF-77 (air-solo) e MPM (1 GHz-1 THz). PyWaveProp (Python): Métodos de equação parabólica com aproximações de Padé, difração de bordas múltiplas, gabinetes PETOOL e RAM, dutos troposféricos e altos, solo irregular: possível base para um cálculo de campo com o perfil GFS M(z, a ser verificado. APRS PropView (Apache 2.0): Mapas de propagação VHF de pacotes APRS, estatísticas horárias e alarmes de abertura; possível fonte de dados de 144 MHz para validação. Rede troposcatter OTAN em Itália (fortifications.net): 900 MHz, 10 kW, 20 m parábolas; estações incluindo Monte Epomeo (Ischia), Monte Giogo, Allumiere, Monte Cavo, Monte Vergine, Monte Abuture, Monte Lauro; caso de teste histórico para P.617. Não relevante: Gupta 2021 (transporte estratosférico), Lei 2023 (ozônio ártico), golfinho #590 e ópera-adt/tropo (atraso InSAR), tropo.ino (sensores de gás Arduino), tropo-webapi (telefonia), ferramentas de nuvem/troposfera (AWS). Validação 1.3.1 em 564 Tropo QSO: AUC 0,79 (modelo 1.2), 0,80 (+ ducto mole), 0,90 (+ misme e subsidência), 0,90 (+ persistência); cores do conduto (≥ 16 dB) em 46% das vias de abertura e 0% da comparação.

Adicionado à mesma rodada. Mikelytaev/onda-propagação (MIT, Python 3): equação parabólica com aproximados de Padé e Numerov, solo irregular, dutos troposféricos; carregar o refrativo modificado M(quota, lat, lon) do NOAA GFS e definir um conduíte de evaporação de Monin-Obukhov; nenhuma função documentada que retorna a perda no caminho. Ele é o melhor candidato para um futuro cálculo de campo com o perfil GFS M (ele serviria um serviço externo ou uma tradução). RP- Sim (AGPL-3.0, JavaScript): Modelos empíricos para redes de rádio (Hata, He, Ericsson, dois raios), no tropo. RIMFading (GPL-3.0, OMNeT++): irregularidades de fluxo para simulações de rede, distribuição de Weibull, sem tropo. PyLayers: canal de rádio interior, já excluído. qradiopredict (kantooon, GPL v2): ITM e ITWOM v3.0 com S RTM3 e Corine, já na sétima rodada como QRadioPredict.

Décima rodada (21 de novembro): Antena Book, Norton, Barsis, novas fontes de dados. Livro Antena de ARRL (23a ed.), cap. 4 ( K9LA): Método Bray (QST Novembro 1961) para a gama VHF: perda suave da Terra para 99% (300 milhas, 144 MHz: 214 dB), curvas para 50% e 99% (fig. 4.11): 195 dB no valor de 100 milhas a 99% e mais de 250 milhas a 50%; evanescência ±7 dB sobre 100 milhas (−3,5 dB a 50 milhas), SSB 3 dB pior que o CW, +4 dB de reflexão no solo, +3 dB para acoplamento de duas antenas; ganho de altura (15 m: 4/3/2 dB a 10/50/100 milhas; 24 m: 8/6/4 dB); ganho Yagi cerca de 10 vezes o boom em λ (dBi); estação 50 W com antena média: cerca de 180 dB, 100 milhas, +10 dB até 250 milhas. Nascer do sol e pôr-do-sol até +20 dB e +20–50% de vazão; ar quente e seco sobre o ar fresco molhado nos Grandes Lagos até 1200 milhas, típico 400–600; regiões influenciadas por grandes massas de água as mais favorecidas; inverno tardio com aquecimento capaz de fortes inversões. Verificação de nosso troposcatter: P.452 a 50% a 483 km e 144 MHz dá cerca de 204 dB, consistente com os 214 dB de Bray a 99%. Norton 1959 (J. Res. NBS 63D, livre em NIST): perda básica, sistema e ganho de antena de caminho; atmosfera de referência Ns = 301; raio equivalente de Ns (250 → 4878 milhas, 301 → 5280, 400 → 6997); L(99) tabelas com antenas de 30 pés (100 MHz: 163 dB a 100 milhas, 208 dB a 500 milhas; 1000 MHz: 138 e 188 dB com parábolas de 28 pés); variabilidade de longo prazo V(p, Δ) dependendo da distância angular (Fig. 13). Rice, Longley e Norton, NBS TN 15 (1959), livre no NIST. Barsis e Kirby, NBS Report 6751 (1961): 223 km no Colorado com Pikes Peak (4292 m) como borda, 100 e 751 MHz; perda consistente com a borda arredondada (aproximadamente 11,5 km de raio); evanescência de longo prazo 11,7–14,3 dB versus 39–42 dB de uma dispersão do mesmo comprimento; nenhum ciclo dia marcado; o modelo de quatro raios fornecido 4–5 lobos, mas apenas um é medido (máximo a cerca de 21 m do solo): os caminhos com obstáculos de montanha são muito mais estáveis do que o espalhamento, útil para as conexões "via Vesúvio ou Faito". ARRL VHF Digital Handbook: pacote, APRS, D-STAR, WSJT (Scatter Method e EME), nenhuma parte no tropo. Novas fontes de dados comprovadas ou pesquisadas: ECMWF dados abertos (IFS 0,25°, níveis 1000, 925, 850, 700 hPa..., sem 975, 950, 900 e 800; G RIB2 com compressão CCSDS, que o nosso leitor não decodifica; já disponível através de Open-Meteo); DWD ICON-EU dados abertos (G RIB2 compactado bz2, um arquivo para variável e nível: muitos pedidos); Canadá GDPS em MSC Datamart e GeoMet (cerca de 15 km, até 33 níveis; GeoMet permite a colheita da área; é a base de mapas de Hepburn); GFS NOAA arquivo em AWS de fevereiro de 2021 (global arquivos, útil para validação); APRS PropView apenas para validação.

Décima primeira volta (21 de novembro): Longley-Rice, GEKO 69. Bean e Thayer, "Models of the Atmospheric Radio Refrative Index", Proc. IRE Maio 1959 (pp. 740 e segs.): o modelo 4/3 tem a inclinação direita no primeiro km mas cai muito mais; 30% da curvatura de um raio ocorre nos primeiros 100 m e 60% no primeiro km; relação média entre Ns e diminuição no primeiro km: −ΔN = 7,32·e^(0,005577·Ns) (Ns = 313 → ΔN aproximadamente −42). Bean e Thayer, "C.R.P.L. Exponential Reference Atmosphere", J. Res. NBS 63D n. 3 (1959): N(h) = Ns·e^(−c·h) com c fixado por Ns através do mesmo relatório; Média Ns EUA 313; tabelas de rastreamento de vigas (curva total 15,2 mrad com modelo exponencial versus 30,3 mrad com Terra 4/3). MATLAB refractionexp: c = 0,143859 km−1 para Ns = 313 (escala cerca de 6,95 km), válido acima de cerca de 1 km e 10 mrad. Uso para nós: o perfil "normal" de comparação pode ser que o CRPL com Ns locais em vez de M = 315 + 0,118·z fixo, e a diminuição anômala no primeiro km deve ser medida em comparação com -7,32·e^(0,005577·Ns) em vez de -40 fixo. NTIA Report 82-100 (Hufford, Longley, Kissick 1982), guia ITM no modo de área: climi e Ns sugeridos (equatorial 360, subtropical continental 320, subtropical marine 370, deserto 280, temperado continental 301, temperado marinho em terra 320, temperado marinho no mar 350); Ns = N0 ·e^(−hs/9,46 km); você usa o mínimo mensal de Ns porque menos influenciado por super-refração e sub-refração; Ns = 301 corresponde a ae = 8497 km; menos de 100 km Ns conta pouco. Arroz, Longley e Norton, NBS TN 15 (1959), parte I: fórmula de previsão de distribuição ao longo do tempo por terra e onda troposférica, base de TN 101. SoftWright, Wikipedia, Altair WinProp e MATLAB rfprop. LongleyRice: mesma informação (versão 1.2.2, 20 MHz–20 GHz ou 40 GHz, 1–2000 km, Ns 301, sete climas, variabilidade do tempo, local e situação); ninguém molda os conduítes, apenas o efeito estatístico dos climas marinhos ("campos mais elevados para 10% do tempo ou não"). Revista GEKO n. 69 ( IU3QEZ): horizonte de rádio 4/3 de 130 km a partir de 1000 m, 260 km adicionando duas estações; troposcatter com volume comum entre 1-3 km e 10 km; dispersão de aeronaves para 10 km com +10-20 dB por dezenas de segundos ou um par de minutos; cursos teóricos com 2×10 elementos por lado: SSB QRP cerca de 440 km, SSB 50 W cerca de 510 km, CW QRP cerca de 600 km, SSB 500 W cerca de 640 km (500 W em SSB fazer 40 km a mais de 5 W em CW); 90–99% do tempo deve ser removido uma margem de 10–20 dB; 5–10 dB por grau horizonte em longos trechos; a quota conta para libertar o horizonte, para não se aproximar do volume comum; Alpe Adria 2026: 99% do troposcatter QSO, quase um por ar apesar de AirScout.

## 12. Mesa redonda: fontes para validar ou corrigir o modelo (21 de novembro)

Resumo de todas as voltas de fontes, ordenado para uso. "Estado" diz se a fonte já está no programa (1.3.1), se está em julgamento ou se é para ser feito.

### Dados a validar (o que realmente aconteceu)

| Origem | O que se passa? | Utilização | Estado |
| Relatórios MMMonVHF do autor | 593 QSO a 144 MHz, 29/12/2021–2/1/2022, tempo e localizadores | Validação do evento contra comparação (+14 dias) | em uso: AUC 0,79 (1.2) → 0,90 (1.3) |
| O mundo acima de 50 MHz | casos documentados (14–15/8/2022 Manitoba–Kansas, 144–10 GHz) | caso histórico | em uso como um caso histórico; a ser adicionado à validação QSOs |
| wspr.live (WSPR) | arquivo completo, 2 m e 70 cm, SQL | muitos eventos e muitos "não" (horas sem lugar) | Tentado, deve ser filtrado (ponto falso de 16-20 mil km) |
| APRS PropView / APRS-IS | estações feltro, distâncias, horas | apenas validação 144 MHz | a fazer |
| Beacon Reverso (arquivo) | local diário | VHF | descartado por enquanto |
| I GRA2 radiosondaggi (Brindisi 16320, Prática de Mare 16245, Trapani 16429, Udine, Milão) | Perfis medidos em 00 e 12 UTC | Controle de perfil GFS e inversões | arquivos encontrados (Brindisi 46 MB), a serem usados |
| Barsis e Kirby 1961, Barsis 1963 | evanescência medida em caminhos com obstáculo montanhoso | validar a categoria "monte" (Vesuvio, Faito) | a fazer |
| Norton 1959, NBS TN 15, TN 101 | L(99) e perdas de meios de comunicação social do quadro | Controlo de dispersão | em parte (ver 300 milhas consistente) |

### Correcções sugeridas por fontes

| Correcção | Fontes | Efeito medido na validação | Estado |
| Estabilidade do Misme Termo | Feijão e boné Dutton. 6 | AUC 0,79 → 0,90 juntamente com o subsídio | em 1.3.1 (título 16 dB sem conduta) |
| Subsistência do movimento vertical | Moler e Holden 1960 (ler em original em 22/11: +2,5 dB média entre o ar descendo e o ar subindo, 1,4 e 9,4 GHz pelo mar) | como acima | em 1.3.1 (+1,5 / −2 dB) |
| Oleoduto macio (λ até 3·λmax) | Kerr, Hepburn, VHF/UHF DX Book, Shi 2019 | sozinho +1% da AUC; traz as cores do ducto em 46% das rotas do evento e 0% da comparação | em |
| Persistência 24 h | Livro VHF/UHF DX, Whiteman e Leyshon | AUC inalterada, mais tomadas e mais alarmes falsos | em 1.3.1 |
| Perfil CRL normal com Ns locais (−ΔN = 7,32·e^(0,005577·Ns)) em vez de 40 N/km fixo | Bean e Thayer 1959, NBS Monograph 4 | testado na validação | em 1.3.2 como alternativa, off devido a defeito |
| Limiar de superrrefracção 79 N/km (ITU) em vez de 100 (Feijão e Dutton, camada 1000–850) | P.453, Lopez 2008, DX Book | testado na validação | em 1.3.2: estimativa agressiva ao lado prudente (o mapa utiliza prudente) |
| Espessura climatológica (cálculo B) menos importante do que o défice | Rogers 1997 | para tentar: use A para ganhar | a fazer |
| Equação parabólica no perfil GFS | PETOOL, propagação de ondas (Lytaev), Levy (livro não lido, apenas antevisão), Lindquist | primeira versão com campo sobre o horizonte dominado pelo ruído numérico (AUC 0,12–0,28): correcto (domínio 8 km, viga 6°); após correcção AUC 0,54–0,66 | em uso como segundo cálculo, não decide a previsão |
| Evanescência: ±7 dB ao longo de 100 milhas, margem 10–20 dB a 90–99% | Bray 1961, Antenna Book, GEKO 69 | limiares de formas em condições marginais | em uso: probabilidade para o período e limiar FT8 −18,6 dB em troposcatter |
| Volume comum e distância equivalente de Bray | IU3QEZ, Bray 1961 | Informação | em |
| Troposcatter P.617 e RSGB como incerteza | TropoLink, DL2FZN | Informação | em 1.3.0 |

### O que resta dos modelos

Conduit para o mar em baixa altitude (100-200 m) que os níveis de GFS não vêem (estudo de fontes de dados atuais); reflexão e difração nas montanhas próximas (Vesuvio, Faito): proposta de cálculo "via monte". ITM/Longley-Rice continua sendo uma referência estatística "dia normal" (Ns 301, climi), sem conduítes. Descartado por decisão do autor: AirScout, P.528), E esporádico (P.2001) e 50 MHz (servoir EIRP que poucos têm e confundem com o E esporádico).

### Proposta de ordem de trabalho

1. Validação estendida: adicionar WSPR filtrado (mesmo horas sem mancha) e QST 2022; repetir para 432 MHz.
2. Tente o perfil normal de LCR e o limiar de ITU de 79 N/km, um de cada vez, mantendo apenas o que aumenta a AUC.
3. Comparação dos perfis GFS com a onda de rádio I GRA2 de Brindisi e Prática do Mar nos dias do QSO.
4. Reavaliar a equação parabólica após correção numérica.

### Decisões de 21 de Novembro (versão 1.3.2)

- Adotado: estimativa prudente (Bean e Dutton, 100 N/km) e agressiva (ITU, 79 N/km) flanqueada; dutos procurados apenas abaixo de 2500 m (primeiro 1500 m, levantada após a validação); bônus com alta pressão estendida e vento fraco; peso do mar com o μ1 de P.452; parcela de estações com relação ao conduíte também em conduíte parcial; camada limite acima dos cumes ( IT9GSF).
- Adotada como alternativa, desligada por defeito: perfil CRPL normal com Ns locais.
- Equação parabólica correta numericamente: AUC de 0,12 a 0,54 ( antena de 30 m) e de 0,28 a 0,66 (300 m).
- Terreno: Copérnico DEM GLO-30 em vez de Terrenos «skadi», que perto da costa têm relevos fantasma de 80-100 m no mar (sinal de IC8TEM, verificado ponto a ponto para Capri); skadi permanece como reserva.
- Sinais meteorológicos no mapa com seus efeitos: ↓ subsídio favorável (Moler e Holden 1960, em média cerca de 2,5 dB entre o ar descendo e o ar subindo; +1,5 dB), ↑ ar ascendente desfavorável (fixuras profundas; −2 dB), S pista favorável scirocco (ARRL Handbook § 19.4, fortes inversões no Mediterrâneo; sem correção). Símbolo duplo, cores verde/vermelho/laranja e banda desenhada no mapa.
- Adotou-se o ITM 1.2.2 (NTIA/itm, domínio público), traduzido em C#: de acordo com o cálculo da linha de base em atmosfera normal próxima a P.452 e o alongamento final após a saída de um conduíte que termina antes do correspondente. Foi descartado o código qradiopredict porque está sob GPL v2.

Sem conteúdo útil para o índice: *Comunications Quarterly*, outono de 1991 (nenhum artigo sobre o tropo; artigo de Atkins sobre o Dispersão é no inverno de 1991).

- Adotado como princípio dos manuais (21 de novembro): 'a propagação não pode ser generalizada, mas estudada em relação à morfologia local' ( DF5AI, relatado por IK3XTV em 'Propagação em 2 metros'); citado no manual de previsão (§ 1) e nos conselhos.
- Adoptada (21 de novembro, depois de 1.3.3) a interpretação das aberturas transalpinas do IK3XTV: não "células convectivas como lentes", mas troposcatter reforçado pela turbulência ao topo da camada reformulada quando atinge a parcela do volume comum (manual de previsões, § 8 ter). De 1.3.4 entra no cálculo ponto-ponto como reforço provisório do troposcatter, no máximo 3 dB. Descartado pelos mesmos artigos: a "autoamplificação" dos sinais para além dos Alpes (um meio passivo não amplifica: os links descritos permanecem explicáveis com difração e troposcatter; os poderes declarados não são usados como dado), a neve que "melhora a difração" (mais provavelmente a camada estável sobre a neve, com menos evanescência).

- Kerr (1951) revisado no livro (22 de novembro, PDF fornecido pelo autor): § 1.5, eq. 17 e 18 e tabela 1.1. As figuras do eq. 18 no scan são ilegíveis, mas a tabela 1.1 confirma: 20 pés → 1 cm, 80 pés → 10 cm, 400 pés → 100 cm, 2000 pés → 1000 cm, contra 1,25, 10, 112 e 1252 cm de λmax = 0,014·d^1.5 (λ em cm, d em pé; a mesa redonda, 'valores indicativos'. As hipóteses de Kerr: dentro do conduíte o índice modificado cai de 4-10-8 por pé, valor típico dos ductos baixos; com gradientes mais fracos precisa de uma maior espessura. Kerr também adverte que λmax não é um corte líquido: comprimentos de onda às vezes mais longos ainda podem ser fortemente influenciados pelo ducto.

- DL6WU revisado no original (22 de novembro, PDF da VHF Communications 3/1982 fornecido pelo autor): G. Hoch, «Extremely Long Yagi Antenas», pp. 130 e segs. Fig. 1: ganho no dipolo medido em 432 e 1296 MHz em polígono profissional dos Posts alemães, com antena de amostra EIA de 7,7 dBd, precisão estimada 0,5 dB; 2,35 dB para o dobro do comprimento do boom, sem saturação até cerca de 20 λ. O programa usou 8.6 + 8.0.log10(L/λ) dBd, ou seja, cerca de 0,8 dB em menos de 1-2 λ; agora usa 9,4 + 7,8·log10(L/λ) dBd (o gradiente é o indicado, o valor de 1 λ é lido no gráfico, ± 0,3 dB). Descartado a fórmula 10·log10(7,8·L/λ) circulado na rede: daria 3 dB para dobrar, contra 2,35 medido.
- Booker e Gordon (1950): referência confirmada por "This Week's Citation Classic" (Conteúdo atual, 8 de fevereiro de 1982, comentário de H. G. Booker): Proc. IRE 38, pp. 401-412. O comentário confirma os dois pontos utilizados nos manuais: campo recebido além do horizonte com distribuição Rayleigh, e função de autocorrelação exponencial de irregularidades (não-Gaussian). O artigo original não foi lido.

## 13. De artigos IK3XTV

Os dois artigos de Flavio Egano IK3XTV (Radio Magazine: "Propagação em 2 metros" e "Propagação VHF – células convectivas e ondas gravitacionais") acrescentam duas coisas que os modelos de conduítes não cobrem: as aberturas transalpinas da tarde devido à convecção, e regras práticas sobre onde e quando os conduítes são formados na Itália. São observações da estação, não medidas: devem ser tratadas como uma hipótese para verificar com os pontos.

| Comentários do autor | Como se torna um ingrediente do índice |
| Os dutos são formados principalmente entre 100 e 1000 m, raramente superiores; servem alta pressão em uma área ampla, boa insolação e ausência de vento | busca do ducto apenas abaixo de 1500 m; bônus com alta pressão estendida e vento fraco nas camadas baixas |
| Nos meses quentes conduzidos recorrentes no Adriático entre Veneto e Puglia, 700-800 km, às vezes até a Grécia; as rotas por mar são favorecidas | maior peso para a fracção da rota marítima (como o μ1 da ITU-R P.452) |
| O correspondente deve estar dentro ou perto da conduta; com antenas altas e alguns metros de chumbo o sinal não entra | comparar a parte da base da conduta com a parte das duas estações (serverá a altitude do QTH) |
| Mesmo tempo, mesma conduta: ouça do Gargano uma estação em 144 e uma em 50 MHz | para verificar: de acordo com a tabela Hepburn o 50 MHz pedir mais de 300 m de chumbo, de modo que o evento indicaria um gasoduto com muita frequência |
| Ligações Veneto – Sul da Alemanha além dos Alpes, sem visual, quase sempre à tarde e com alta pressão; sinais instáveis que desaparecem e reaparecem após alguns minutos | índice "convectivo" separado da conduta, para rotas de montanha |
| As células convectivas nascem no solo e sobem cerca de 1 km a cada 3 horas: aos 15 chegam a cerca de 3 km, a altitude que serve para sobrepor os Alpes | usar a altura da camada limite (limítrofe\_layer\_altura do Open-Meteo GFS) ao longo do caminho: marcar quando exceder a altitude dos cumes |
| Os Alpes geram ondas gravitacionais que, juntamente com movimentos convectivos, criam irregularidades do índice de refração | hipóteses: vento forte perpendicular à corrente de 700 hPa como sinal de ondas de ouro; para tentar por último |
| Muito alta inversão (até 8 km em casos excepcionais) dá cerca de 800 km, mas na prática raramente mais de 300 km | a parcela da inversão amplia o escopo: dados úteis para estimar a distância de espera |
| No verão raio Terra equivalente a cerca de 16000 km (2,5 vezes o real), com cerca de 60 dB em menos de 300 km perda | consistente com k50 = 157/(157 - ΔN): k = 2,5 corresponde a ΔN cerca de 94 N/km, ou seja, super-refracção |
| Mais de 3000 km apenas na faixa de 20-30° norte e sul do equador, acima dos mares, ligado às células de Hadley; pico em agosto na África-Europa | fora da Europa: útil para não dar altos índices no Atlântico sem feedback, e para a rota Canário – Grã-Bretanha |

Outros dados úteis dos mesmos artigos:

- Alcance óptico entre dois h1 e h2 metros (d em km): d = 4,1 · (?h1 +?h2), até cerca de 20% para refração normal.
- Evanescência: lenta e superficial com inversão; forte e rápida com dispersão e células convectivas. O programa poderia usá-lo para classificar anúncios se um dia ele terá relatórios SNR ao longo do tempo.
- Para o E esporádico em 144 MHz o autor relata o trabalho de DF5AI e Davis e Johnson (Natureza 435, 2005) sobre a ligação entre temporal e Es: fora do tropo, mas um indicador separado amanhã poderia usar dados de raios.
- Reflexão do solo no salto duplo Es: mar K = 81 e 5 S/m, campo e bosque K = 13 e 0,006 S/m, áreas urbanas K = 3 e 0,001 S/m.

## 14. Hipóteses do índice LIVETROPOVHF

Proposta de partida, a ser discutida: um índice 0-10 na mesma escala que Hepburn, calculado para cada ponto da grade e a cada 3 horas do GFS (então ECMWF e ICON para comparação), e um segundo número por rota que diz quanto parte da rota passa para um ducto útil para a banda escolhida. Os pesos abaixo são minhas suposições, para ser calibrado com os pontos.

Revisão após Bean e Dutton e Kerr: limiares de perfil vêm de sua tabela 6.5 e escala de suas medidas, então uma camada de refração vale 10–25 dB sobre o perfil normal. O índice é então calculado primeiro em dB e depois assume 0–10; os valores em que a tesoura permanece para ser calibrada com os pontos.

Cálculo por ponto, passo a passo

1. Descarga T, umidade relativa, altura geopotencial e velocidade vertical a 1000, 975, 950, 925, 900, 850, 800, 700, 600 e 500 hPa, mais umidade T e 2 m, pressão do solo, altura da camada e vento a 10 m.
2. Eliminar os níveis abaixo do solo; calcular N e M em cada nível (ITU-R P.453).
3. Cálculo −dN/dh em cada uma das quatro camadas de Bean e Dutton (1000–850, 850–700, 700–600, 600–500 hPa) e o gráfico como sub-refrativo, normal ou super-refrativo com os limiares de sua tabela; dentro da primeira camada também usar níveis intermediários para encontrar o gradiente mais forte e sua participação.
4. Onde dM/dh < 0 encontro base e topo da conduta, espessura d e déficit ΔM.
5. Comparação da espessura mínima para a banda da fórmula de Kerr (cerca de 180 m para 144 MHz, 89 m para 432 MHz, como na tabela de Hepburn); sob o limiar o conduíte ainda conta, mas menos.
6. Estimo o ganho em dB com a tabela abaixo (você pega a melhor camada, não a soma das camadas, mais o termo de anomalia) e converto-a: Índice = dB / 2,5, entre 0 e 10, então 25 dB vale 10. Um valor negativo (sub-refração) é mostrado como 0 mas permanece nos dados do caminho.

| Melhor situação de camada | Critérios | Lucros estimados |
| Normal (não laminado) | −dN/dh no intervalo de tabulação. 6.5 (por exemplo, 20–60 N/km entre 1000 e 850 hPa) | 0 |
| Um pouco acima do normal | entre o limite superior da banda normal e o limiar de superrrefracção | 0 a 10 dB, na proporção |
| Super- refração | para além do limiar da camada (100, 80, 70, 50 N/km de escalada) mas abaixo de 157 N/km | de 10 a 15 dB, em proporção até 157 N/km |
| Oleoduto muito fino para banda | dM/dh < 0, espessura abaixo Limiar Kerr | 15 |
| Conector útil para banda | dM/dh < 0, espessura acima do limiar de Kerr | 20 dB, +1 dB cada 5 M unidades de déficit, máximo 25 |
| Sub- refração | −dN/dh < 0 na camada inferior | −10 dB |
| Fundo anômalo (você adiciona) | Modelo Ns menos ITU mapa climatológico Ns para esse mês | 0,2 dB por unidade N, entre −5 e +5 dB |

Cálculo por rota (entre dois localizadores): amostrar o círculo máximo a cada 10 km, como já faz o programa para o índice no caminho, e calcular a fração de pontos com ducto útil e a participação média da base. O estudo CSIRO encontrou útil a fração da rota no gasoduto (nas rotas AIS de 162 MHz foi 67% maior do que nos casos de controle).

Adicionando o cálculo por rota (de LA0BY e W3SZ): (1) estações marítimas comparadas com base e topo do conduíte, porque um conduíte alto só serve aqueles que entram; (2) um troposcatter "chão" com o método Yeh, com os cantos iniciais retirados do perfil do solo; o ganho de trope em dB é removido dessa perda e o resultado é comparado com a perda que a estação consegue superar (Tabela KK6MC). Assim, o índice também se torna uma estimativa da distância alcançável.

Correções para tentar um de cada vez (de Bean e Dutton e LA0BY, IK3XTV, G4MCU já no manual), a ser mantido apenas se melhorar a comparação com os pontos:

- Mistura (importante de acordo com o autor, por observação diária): dia na terra, entre meados da manhã e tarde com céu claro, ou com vento no chão sobre alguns m/s, os dutos no solo desaparecem; sua contribuição deve ser reduzida. Não no mar.
- Hora do dia (importante de acordo com o autor, por observação diária): dutos irracionais nascem ao pôr do sol e caem após o nascer do sol; é um controle de previsão de 3 horas.
- Movimento vertical: ar subindo para 850–700 hPa remixa as camadas (correção negativa), o ar que desce sob um anticiclone as fortalece (positivo).
- Tendência de pressão: pressão ascendente no caminho nas últimas horas, sinal mais alto e estável; em diminuição, sinal mais baixo (bastano 2 hPa).
- Sinônimo: alta pressão acima de 1025 hPa, temperatura a 850 hPa muito acima do que no solo, ar quente e seco que flui sobre o mar mais frio, rota por mar.
- Caminhos montanhosos: um segundo índice "convectivo" (seção IK3XTV), com uma camada limite da tarde superior aos cumes ao longo do caminho e alta pressão.

De VHF/UHF DX Livro, outras correções para tentar: dias consecutivos de alta pressão sobre o ponto (mais dias, mais pontos), posição comparada ao centro do anticiclone (bonus nas bordas), gradiente de pressão no chão como medida do vento, chuva forte nas últimas 24 horas (penalidade), nuvens baixas na borda da camada (penalidade apenas para as bandas altas). Para a direção das rotas: comparar o azimute da rota com o do isobare.

Verificação

- Arquivo LIVETROPOVHF: cada comercial com mais de 500 km a 144 ou 432 MHz é um "evento". Compara o índice na rota com a frequência de eventos, na mesma hora do dia e do mês (a mesma abordagem do estudo CSIRO).
- Comparação com Hepburn e F5LEN nos mesmos dias: onde os três índices se desconectam, você olha quem estava certo de acordo com os pontos.
- Radiosundações de 00 e 12 UTC: para verificar se o modelo vê as inversões que a sonda mede.
- Casos históricos de LA0BY (6-11/11/2003, 10/12/2004, 15/10/2011, 11/2/2015, 22/11/2018): o índice recalculado naqueles dias deve ver os dutos que Hepburn não mostrou.
- Limites a serem declarados no manual: conduzido sob 100-200 m quase invisível aos níveis de pressão GFS; previsão confiável para 2-3 dias, então apenas tendência.

## 15. Fontes de 21 de Novembro (segunda parte)

- ITU-R P.1812 (MaTLAB/Octave I. Stevanovic, OFCOM; GitHub eeveetza/p1812, Zenodo 10.5281/zenodo.6141558). Método específico de área VHF/UHF da rota, derivado de P.452: difração de "delta-Bullington", troposcatter, dutos e reflexão sobre camadas, encumprimento no solo. Útil como uma terceira comparação ao lado de P.452 no cálculo ponto-ponto; dados ΔN e N0 são produtos ITU completos, então o usuário os baixa (como por P.453).
- de Costa, Rabanal Ramirez, Dias, IET MAP 2018 (doi 10.1049/iet-map.2017.0992). 563 MHz, 27 pontos entre 5 e 30 km em terreno irregular com floresta: P.526 com 12 m de vegetação −4,3 dB e σ 8,8 dB; P.1812 com 10 m de encombre +2,6 dB e σ 8,9 dB; cunha dielétrica −4,8 dB e σ 9,2 dB; P.1546 +5,0 dB e σ 11,7 dB. Para o programa: o erro residual de um bom método de difração é de cerca de 9 dB, e a altura dos obstáculos nos cumes (10-12 m) conta.
- PETOOL (Ozgun e altri, Computer Physics Communications 182, 2011). Equação parabólica split-step para uma e duas rotas com propagador de ângulo largo (ângulos até 40–45°, contra 10–15° do estreito); fundo com perdas através da mistura transformada Fourier; terreno para etapas. Confirma as escolhas de 1.3.2 (ângulo largo, coeficientes de solo) e indica o cálculo bidirecional para relevos íngremes.
- MathWorks. `tropopl`: perda de gases e perda de «lente» numa atmosfera em camadas (P.835, P.676, P.453), sem condutas: com gradientes inferiores a -157 N/km o modelo óptico geométrico não se aplica. O exemplo de propagação de radar usa para refração −ΔN = 7,32·e^(0,005577·Ns), o mesmo relatório CRPL já no programa. `propagationModel` Longley-Rice: valores de polarização horizontal predefinidos, condutividade 0,005 S/m, permissividade 15, reatividade 301, clima continental temperado. Nenhum modelo MathWorks lida com os dutos.
- ITU-R, produtos digitais do grupo 3. Os dados e programas de P.453, P.452, P.1812, P.2001, P.528, P.1546; dados 'integrais' fazem parte das recomendações; nenhuma licença de redistribuição declarada: você continua a deixá-los baixar para o usuário.
- ESA Navipedia, atraso troposférico. N = Nidrostatica + Numida; o atraso zenital seco é de cerca de 2,3 m e o úmido dezenas de cm, que varia rapidamente. Para nós: o atraso molhado estimado pelas estações GNSS é uma medida indireta da umidade integrada, possível fonte futura de agora-casting; Hoje não é utilizável.
- Bray, K2LMG, «A Method for Determining V.H.F. Station Capacities», QST Novembro 1961, pp. 36–40 (leia-se em original; verificou-se novamente em 22 de novembro no PDF do número de QST: todos os valores abaixo correspondem). O autor, D. W. Bray (General Electric, Ithaca), diz ter verificado as previsões com meses de conexões programadas com K2GOI e W4LTU. Evanescência dentro do tempo de 13,5 dB sobre cerca de 100 milhas (7 dB abaixo e 7 acima da média horária), quase linear abaixo (3,5 dB a 50 milhas): para uma conexão sólida remover 7 dB. Correção do horizonte: cada elevação do horizonte vale 111 km de distância extra (menos se negativo). Ganho de um yagi cerca de 10 × boom/λ (taxa de potência), mais cerca de 4 dB de reflexão do solo; SSB −3 dB e AM −7 dB em comparação com CW; 99% das horas: cerca de 180 dB a 60 milhas, 190 a 90, 200 a 240 (2 m). A partir de 1.3.2 a probabilidade para o período usa σ = 3,4 dB (±7 dB como ±2σ) em vez dos 5 dB estimados de segunda mão.
- Cálculo bidirecional (tentar com protótipo, 21 de novembro). Equação parabólica de grande ângulo com terreno real S RTM1 de Capri, 144 MHz, antena a 10 m do solo, atmosfera padrão; medida: potência média entre 20 e 400 m a 80 km na rota para a correspondente. Do centro de Capri e Marina Grande o oeste está fechado (obstáculo para +11–21°, Monte Solaro): apontando o Faito (quase atrás do EA5 e EA6, 150–160° fora do eixo) o sinal lançado em direção à Espanha sobe 10–29 dB; apontando o Vesúvio (110–125° fora do eixo, em seguida, fora do plano de cálculo, apenas indicativo) de 17–25 dB. De Anacapri o oeste é livre e as vitórias diretas de 8-22 dB. As perdas absolutas em 1000–1300 km do protótipo, no entanto, não são confiáveis. EA3 EA5 EA6
- Barsis e Kirby, NBS Report 6751 (1961), Pikes Peak, 223 km, 100 e 751 MHz. A montanha é um terminal comum de duas rotas em óptica: a distribuição das medianas horárias é a convolução das duas rotas. Excursão de longo prazo (1–99%) de cerca de 14 dB, contra 26–40 dB de um trajeto de troposcatter de igual comprimento ou ângulo; nenhuma mudança significativa do dia. O sinal é mais forte do que o troposcatter, mas mais fraco do que a borda ideal: o topo deve ser tratado como arredondado (aproximadamente 11,5 km). Sinais elevados são sempre observados atrás dos dorsais.
- Barsis, NBS Report 7652 (1963), três rotas sobre uma montanha (Colorado, Alasca, Europa), 750–900 MHz. Perda média 10-20 dB superior ao cálculo de Fresnel-Kirchhoff sem terra perto dos terminais; a evanescência é estimada por envolver as duas linhas em óptica; as reflexões do solo próximo aos terminais pioram o desbotamento prolongado. A partir de 1.3.2 a entrada no conduíte adiciona o arredondamento de P.526.
- Barsis e Johnson, NBS TN 88 (1961). Dissolução prolongada da onda espacial, mesmo num caminho além de uma montanha (§ 5).
- Norton, «Perda de sistema na propagação de ondas de rádio», J. Res. NBS 63D (1959). As curvas a partir das quais Bray recebe os nomogramas; definições de perda de sistema, transmissão e básico, ganho de antena de processo.
- Arroz, Longley, Norton e Barsis, NBS TN 101 (1965–67), Volumes 1 e 2. borda única e obstáculo arredondado com e sem reflexos (§ 7, com o exemplo do Pico de Pikes), variabilidade de longo prazo V(0,5) e Y(q) para climas (tempo concontinental e outros). Para nós: o método das funções de obstáculos arredondados e variabilidade são a referência à incerteza calibrada.
- ITU-R P.526-16 (11/2025). Fórmula (31) de J(ν), idêntica à do programa; Arredondamento de obstáculos (32)–(36), agora utilizado; métodos multi-split (Bullington e método completo § 4.5).
- ITU-R P.1812-8 (2025) e -7. Texto completo recuperado: método específico dos 30 MHz a 6 GHz (delta-Bullington, troposcatter, dutos e reflexão sobre camadas, ingombs). O -7 adverte que seu troposcatter (de P.617-5) está errado abaixo de 66 km. Continua a ser uma terceira comparação possível.
- Paulus, «Aplicação plástica do modelo de evaporação IREPS», NOSC TR 966 (1984) e Radio Science 20 (1985). Relatório lido em 22/11/2026 (DTIC ADA146528, varredura com texto reconhecido, às vezes ilegível). As diferenças ar-mar medidas pelos navios são deslocadas para a estabilidade: ar marinho mais quente em 31–35% dos dados do navio versus 0,7–7% dos javalis NOAA, para os psicrómetros perturbados pelo navio e SST retirados da água de arrefecimento (cerca de +1 °C). O modelo Jeske dá então 40 m e mais de 15% do ano e alturas que saltam de 10 para 40 m em 24 horas com tempo estável, não confirmado por medidas de rádio. Correção proposta (limiares testados 0, −0,5 e −1 °C; o −1 °C é melhor acordado com as medidas de rádio de junho de 1982): se a diferença ar-mar é −1 °C ou menos é usado o medido; caso contrário, você calcula a altura com −1 °C e com 0 °C e, se isso com −1 °C é menor, você segura isso. Os aumentos no lado estável são cortados, as diminuições permanecem. Em junho, Marsden 120 quadrado: altura média de 16,8 m (climatologia) a 7,6 m. As equações do modelo não estão no relatório (ver Jeske e Hitney). O programa usa o perfil neutro, que não tem exagero estável, e limita δ a 40 m: ele vai na mesma direção; com os SSTs dos modelos (não de navio) a falha de medição que Paulus corrige pesa menos.
- Levy, «Métodos de Equação Parabólica para Propagação de Ondas Electromagnéticas» (IEE 2000). No Google Books há apenas visualização: Eu não leio.
- VK3FS (3fs.net.au). Introdução de qualidade: dutos altos de 450 a 2000 m para aberturas mais longas no solo; inversões de 300 a 600 m em noites calmas e calmas, que desaparecem após o nascer do sol; inversões costeiras mais fortes à tarde; 800 a 1300 km típico, 1400 a 1600 km frequentemente conduzidos. Consistente com alertas de dia/noite e teto de 2500 m.

## 16. Fontes

Regras e fórmulas

- [ITU-R P.453-13 (2017), índice de refração de rádio: fórmula](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.453-13-201712-S!!PDF-E.pdf)e dados
- [ITU-R P.452-18 (2023), previsão de interferência entre estações terrestres de 100 MHz: raio equivalente, β0, troposcatter, condutas](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.452-18-202310-I!!PDF-E.pdf)
- [ITU-R P.617-5, propagação para além do horizonte para as pontes de rádio](https://www.itu.int/dms_pubrec/itu-r/rec/p/R-REC-P.617-5-201908-I!!MSW-E.docx)
- [Shi et al., "Um estudo abrangente sobre o comprimento máximo da onda de propagação eletromagnética em diferentes dutos de evaporação", IEEE Access 2019](https://doaj.org/article/d431fc16b2104524860927ac92dc1c53)

Estudos de predição de dutos com modelos meteorológicos

- [P. Lopez, "Uma climatologia global de 40 km de resolução de 5 anos de super-refracção para radar de meteorologia terrestre", ECMWF Tech. Memo 549, 2008](https://www.ecmwf.int/sites/default/files/elibrary/2008/10803-5-year-40km-resolution-global-climatology-super-refraction-ground-based-radar-meteorology.pdf)
- [B. Indermuehle, H. Suzuki (CSIRO), "Previsão de Dutação Troposférica baseada em dados do modelo GFS em... a Rádio-Astronomia CSIRO Murchison", Observatório arXiv 2608.12861](https://arxiv.org/pdf/2608.12861)
- [K. Hämäläinen et al. (FMI), "Efeitos da resolução do modelo na previsão das condições atmosféricas de ducto com Harmonie-AROME", EGUSphere 2026](https://egusphere.copernicus.org/preprints/2026/egusphere-2026-1629/egusphere-2026-1629.pdf)

Previsão de rádio amador

- [W. R. Hepburn, "The Hepburn Tropo Index (HTI)"](https://www.dxinfocentre.com/propagation/hti.htm)e [](https://www.dxinfocentre.com/propagation/history.htm)method history
- [W. R. Hepburn, "Frequência de Uso Loost vs. Espessura da Camada de Ductação"](https://www.dxinfocentre.com/propagation/luf.htm)
- [F5LEN, "Previsões de índice de refração mundial"](https://tropo.f5len.org/WW/info.php), [nova versão 2026](https://tropo.f5len.org/?p=504), [nota sobre o modelo ARPEGE](https://tropo.f5len.org/author/f5len/)

Dados

- [Open-Meteo, API GFS](https://open-meteo.com/en/docs/gfs-api), [API ECMWF](https://open-meteo.com/en/docs/ecmwf-api), [API DWD ICON](https://open-meteo.com/en/docs/dwd-api), [nota sobre níveis de pressão](https://openmeteo.substack.com/p/upper-air-weather-forecasts-via-api)
- [NOAA NCEI, Integrated Global Radiosonde Archive (IGRA)](https://www.ncei.noaa.gov/products/weather-balloon/integrated-global-radiosonde-archive)
- [NCAR RAOBget, roteiro de inundação de rádio para a Universidade de Wyoming](https://github.com/NCAR/RAOBget)

Textos clássicos (indicados pelo autor do programa, a serem consultados para fórmulas finais)

- B. R. Bean, E. J. Dutton, *Radio Meteorology*, NBS Monograph 92, 1966, 435 páginas (reprint Dover 1968): perfis verticais de N e M, gradiente de armadilha sob −157 N/km, comparação com a onda de rádio; capítulo 5 "Meteorologia de rádio sinóptica". Base para 2-4 etapas de cálculo por ponto. Grátis: [PDF NIST](https://nvlpubs.nist.gov/nistpubs/Legacy/MONO/nbsmonograph92.pdf), [NIST card, doi 10.6028/NBS.MONO.92](https://www.nist.gov/node/1770911), [Arquivo de Internet](https://www.archive.org/download/radiometeorology00bean/radiometeorology00bean.pdf)cópia.
- L. Boithias, *Radio Wave Propagation*, McGraw-Hill 1987 (traduzido por D. Beeson de francês, 330 páginas, ISBN 0070064334; também norte de Oxford Acadêmico): mecanismos além do horizonte, volume comum de dispersão e perda de antena de acoplamento-mezzo. Somente papel ([cartão de biblioteca](https://libcat.library.tamu.edu/Record/in00000014812/Description)).
- M. P. M. Hall, *Effects of the Troposfera on Radio Communication*, IEE Electromagnetic Waves Series n. 8, Peter Peregrinus 1979, 206 páginas (ISBN 0906048257; brossura 0863410863): situações sinóticas e propagação VHF/UHF. Base para ingredientes sinópticos. Comércio na IET ([Cartão](https://libcat.library.tamu.edu/Record/in00000636385)).
- H. G. Booker, W. E. Gordon, "A Theory of Radio Scattering in the Troposfera", *Proceedings of the IRE*, vol. 38, n. 4, abril 1950, pp. 401–412: Tirando a teoria da desogenidade do índice de refração. Ideia central: a potência difusa depende da intensidade e da escala das flutuações turbulentas do índice de refração (correlação exponencial); próximo ao horizonte prevalece a difração, o espalhamento adicional; Segundo os autores, a escala de turbulência e as variações de índice esperadas pela meteorologia são suficientes para explicar os campos medidos. Citado por 328 artigos (Scilit), sem cópia de acesso gratuito. Cartão de estudante semântico ( https://www.semanticscholar.org/paper/A-Theory-of-Radio-Scattering-in-the-Troposphere-Booker-Gordon/1c33f0743758861c1715b2fb9c0d007fada07fed); texto integral sobre IEEE Xplore pago ([doi 10.1109/JRPROC.1950.231435](https://scilit.net/publications/6ddb38dbeb95693c95c5beac6d8a3e78)).
- C. M. Crain, J. R. Gerhardt (Eletrical Engineering Research Laboratory, University of Texas), "Measurements of the Parameters Involved in the Theory of Radio Sprattering in the Troposfera", *Proceedings of the IRE*, vol. 40, n. 1, January 1952, pp. 50–54, doi 10.1109/JRPOC.1952.2739 dar os valores reais que Booker e Gordon tinham apenas estimado ([Skilit](https://scilit.com/publications/9ad54a3129c466a37ca594cc95d0a8f4)tab]).
- "As Constantes na Equação do Índice de Refração Atmosférica nas Frequências de Rádio", *Proceedings of the IRE*, vol. 41, n. 8, agosto 1953, pp. 1035-1037: propõe rever as correntes em uso (79 K/mb e 4800 K) na fórmula N; é daqui que os valores 77,6 e 3,73 · 10^5 da fórmula ITU ([Q](https://scilit.net/publications/3b7ea0638b2ac3278d731f50efb203b4)
- E. C. S. Megaw, "Teoria de propagação de dispersão de rádio fundamental", *Procedimentos do IEE Parte C: Monografias*, vol. 104, n. 6, Setembro 1957, pp. 441–455, doi 10.1049/pi-c.1957.0052: espectro de flutuações de índice da teoria da turbulência, traçado completo de dispersão para além do horizonte comum e efeito da geometria paga no IET ([Skilit](https://scilit.com/publications/737f56f24b08794a602696b50531a0ac)card).
- *Procedimentos do IRE*, Outubro de 1955, número especial sobre propagação de dispersão (por K. A. Norton e J. B. Wiesner), com Bullington, Staras e outros: livre em PDF em [World Radio History](https://www.worldradiohistory.com/Archive-IRE/50s/IRE-1955-10.pdf).
- D. E. Kerr (editado por), * Propagation of Short Radio Waves*, MIT Radiation Laboratory Series vol. 13, McGraw-Hill 1951 (Restampe Dover 1965 e IEE 1987): teoria da evaporação e dutos de superfície no mar. Cópia digital: [World Radio History](https://www.worldradiohistory.com/BOOKSHELF-ARH/Technology/Technology-Radio/Propagation-of-Short-Waves-Kerr-1951-22-06-06.pdf); Edição IET em [IET Digital Library](https://shop.theiet.org/propag-short-radio-waves).

Medidas recentes de difusão troposférica em FVH: W. Zhang, J. A. Hackworth, W. R. Schoff (DSTO, Austrália), "Um estudo de canais de comunicação VHF Troposcatter na Austrália do Sul", em * Processamento de Sinal Digital para Sistemas de Comunicação*, Springer 1997, pp. 103-112 ([Springer](https://link.springer.com/chapter/10.1007/978-1-4615-6119-4_12)). Texto por taxa; do resumo diz respeito à difusão, não aos conduítes. Útil somente se você quiser calibrar o termo troposcatter com dados de canal VHF.

Modelos numéricos e rotas marítimas (indicados pelo autor do programa)

- M. Levy, * Métodos de Equação Parabólica para Propagação de Ondas Electromagnéticas*, Série IEE de Ondas Electromagnéticas #45, 2000, 336 páginas (ISBN 0852967640): equação parabólica para perfis de refração que mudam ao longo do caminho e para a orografia; capítulos sobre propagação troposferosa, raios e formas, propagação no mar. Serviria para um cálculo de campo ao longo do caminho, além do índice. Para pagamento, também em formato eletrônico da IET ([IET card](https://shop.theiet.org/parabol-eqn-meth-em-wave-prop)).
- S. D. Gunashekar, D. R. Siddle, E. M. Warrington (Universidade de Leicester): Medições de 2 GHz nas Ilhas do Canal e no Canal. Muitos trabalhos e teses são gratuitos no arquivo universitário: [estatísticas de longo prazo sobre o ducto de evaporação, Radio Science 2010](https://figshare.le.ac.uk/articles/journal_contribution/Long-term_statistics_related_to_evaporation_duct_propagation_of_2_GHz_radio_waves_in_the_English_Channel/10098434), [alterações de sinal em três rotas marítimas, Radio Science 2007](https://figshare.le.ac.uk/articles/journal_contribution/Signal_strength_variations_at_2_GHz_for_three_sea_paths_in_the_British_Channel_Islands_observations_and_statistical_analysis_/10089710), [Títulos de N. Mufti 2012 com rotas de 50 e 140 km também em VHF](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_Effects_of_the_Troposphere_on_Vhf_and_Uhf_Radio_Propagation_and_Interference_between_Co-Frequency_Fixed_Links/10107854), [tese de I. Alam 2002 sobre reatividade calculada a partir dos dados meteorológicos, 240 MHz e 2 GHz X3 UQ, [tesi..](https://figshare.le.ac.uk/articles/thesis/Investigation_into_the_effect_of_refractivity_on_propagation_at_uhf_and_vhf_frequencies_modelled_and_experimental_analysis/10170959)](https://figshare.le.ac.uk/articles/thesis/The_propagation_of_VHF_and_UHF_radio_waves_over_sea_paths/10090925)
- R. A. Paulus, "Aplicação prática de um modelo de evaporação", Radio Science vol. 20 n. 4, 1985, p. 887 (não IEEE Transactions on Antenas and Propagation), doi 10.1029/RS020i004p00887 ([scheda](https://zendy.io/title/10.1029/rs020i004p00887)): altura do canal de evaporação a partir de dados meteorológicos de superfície. Livre o relatório técnico que o precede: [NOSC TR 966, 1984](https://apps.dtic.mil/sti/pdfs/ADA146528.pdf).

T. Lindquist, *Wave Propagation Models in the Troposfera for Long-Range UHF/SHF Radio Connections*, dissertação de mestrado da Universidade de Karlstad, 2 de junho de 2020, 106 páginas (fornecidas pelo autor do programa). É um guia passo-a-passo, livre, para escrever um cálculo com a equação parabólica: solução de split-step (mesmo em ângulos largos), condições de fronteira para terra e mar, terreno a degraus, rastreamento de feixe em perfis lineares às vezes, com verificações numéricas; cita a ferramenta livre de PETOOL para MATLAB. Dois dados úteis ao mesmo tempo: (1) sensibilidade N às variáveis meteorológicas, δN 0,26 δP + 4,3 δe − 1,4 δT (hPa e K, em torno de 20 °C), ou seja, um erro de 1 hPa na pressão do vapor pesa três vezes um erro de 1 °C na temperatura: é a umidade do modelo que deve ser verificado primeiro; (2) altura média do ducto de evaporação: 5–6 m no mar do Norte, 13–14 m no Mediterrâneo, mais de 20 m no Golfo Pérsico, sempre muito baixo para 144 e 432 MHz. Conclusão do autor (frequências de 1 a 10 GHz): além do horizonte as conexões confiáveis quase sempre requerem um conduíte.

Programa de propagação do perfil do solo (para horizontes e obstáculos, não para o tropo esperado)

- [Radio Mobile](https://ve2dbe.com/rme.html)por Roger Coudé VE2DBE: livre, Windows (também Linux e Mac com vinho), modelo Longley-Rice/ITM, perfil de terra extraído automaticamente de dados de quota ([download](https://ve2dbe.com/download/download.html)).
- Splat!](https://qsl.net/kd2bd/splat.html)de John Magliacane KD2BD: GPL, 20 MHz–20 GHz, Longley-Rice e ITWOM v3.0, relatórios de obstáculos e perda em texto; última versão 1.4.2 de 2014.
- [ITM, NTIA/ITS](https://its.ntia.gov/software/itm)código oficial: o modelo Longley-Rice (20 MHz-20 GHz) com fonte e documentação, base de Radio Mobile e SPLAT!. O código é livre e você pode adicionar ao cálculo no caminho de LIVETROPOVHF (perda devido ao solo, para ser adicionado ao índice de tropo). Relatório original: A. G. Longley, P. L. Rice, "Previsão da Perda Troposférica de Transmissão de Rádio Sobre Terras Irregulares: Um Método Computador – 1968", Relatório Técnico ERL 79-ITS 67, julho 1968 (cartão NIA em software histórico: X1 https://its.ntia.gov/software/legacy-radio-propagation-software/).
- [ITU-R, software e dados digitais](https://www.itu.int/en/ITU-R/study-groups/rsg3/Pages/iono-tropo-spheric.aspx): mapas livres das medianas anuais ΔN e N0 na grelha 1,5° utilizada por P.452 e P.617 — úteis como valores climáticos de referência para o índice.
- Pathloss (Contract Telecommunications Engineering): programa comercial para pontes de rádio; Eu não encontrei uma fonte pública que confirma quais recomendações da ITU implementa, para verificar.

Código GitHub reutilizável em LIVETROPOVHF

| Projeto | O que você faz? | Língua | Use possível para nós |
| [NTIA/ITM](https://github.com/NTIA/itm) | modelo Longley-Rice oficial da NTIA, com pacotes para. NET |  C++, C#  | perda de terra no caminho: integra-se diretamente no programa, que está em C# |
| [fuzzzeee/itm](https://github.com/fuzzzeee/itm) | ITM C++ code C# translation |  C#  | alternativa sem bibliotecas nativas, também útil para a versão do Windows 7 |
| [eeveetza/Py452](https://github.com/eeveetza/py452) | ITU-R P.452, idêntico à versão MATLAB/Octave aprovada pela ITU-R WP 3M; versão MATLAB publicada pelo Escritório de Comunicação Federal da Suíça | Python, MATLAB | referência para traduzir em C# as fórmulas troposcatter e conduzidas, com exemplos de validação ITU |
| [bwinkel/pycraf](https://github.com/bwinkel/pycraf) | P.452 completo, P.676, perfis SRTM; GPL v3, mas os arquivos de dados ITU incluídos não são gratuitos para uso comercial | ♪ ♪ | controle de nossos resultados |
| [ITU-Rpy](https://github.com/inigodelportillo/ITU-Rpy) | muitas recomendações ITU-R, incluindo P.453-13, validadas com exemplos ITU | ♪ ♪ | verificação da fórmula N e dos mapas climáticos de P.453 |
| [NCAR/RAOBget](https://github.com/NCAR/RAOBget) | baixar a onda de rádio da Universidade de Wyoming em formato de texto | ♪ ♪ | diagrama para baixar em C# os perfis das estações italianas |

Nota: a versão atual do P.453 é o [P.45](https://www.itu.int/rec/R-REC-P.453-14-201908-I)

[3-14 \
(2019)](https://www.itu.int/rec/R-REC-P.453-14-201908-I), disponível gratuitamente; no documento usei o 13, para ser reallined.

Já nos manuais do programa e reutilizáveis aqui: LA0BY (Nordic VUSHF 2019), IK3XTV ("Propagação em 2 metros" e "Propagação VHF – células convectivas e ondas gravitacionais", Radio Magazine, fornecido pelo autor do programa), DF5AI/DK5YA (2003), G3YGF (QST 1983), The VHF/UHF DX Book cap. 2, G4MCU (2003) KP4MD
